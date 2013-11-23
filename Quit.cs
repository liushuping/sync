using System;

namespace sync
{
    class Quit : Handler
    {
        public Quit(Handler next)
            : base(next, true)
        {
        }

        protected override bool ExecuteCore(string input)
        {
            return input.ToLower() == "q";
        }
    }
}
