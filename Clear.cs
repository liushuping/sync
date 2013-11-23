namespace sync
{
    class Clear : Handler
    {
        public Clear(Handler next)
            : base(next, false)
        {
        }

        protected override bool ExecuteCore(string input)
        {
            if (input.ToLower() == "clear")
            {
                Util.ClearConsole();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}