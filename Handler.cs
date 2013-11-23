namespace sync
{
    abstract class Handler
    {
        private Handler next;
        private bool terminate;

        public bool Execute(string input)
        {
            if (input != null)
            {
                input = input.Trim();
            }

            if (ExecuteCore(input))
            {
                return terminate;
            }
            else
            {
                return next.Execute(input);
            }
        }

        public Handler(Handler next, bool terminate)
        {
            this.next = next;
            this.terminate = terminate;
        }

        protected abstract bool ExecuteCore(string input);
    }
}