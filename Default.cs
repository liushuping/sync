namespace sync
{
    class Default : Handler
    {
        public Default(Handler next)
            : base(next, false)
        {
        }

        protected override bool ExecuteCore(string input)
        {
            Util.PrintLine("Invalid command!");
            return true;
        }
    }
}
