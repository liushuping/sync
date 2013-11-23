using System;

namespace sync
{
    class Discard : Handler
    {
        public Discard(Handler next) : base(next, false)
        {
        }

        protected override bool ExecuteCore(string input)
        {
            return string.IsNullOrWhiteSpace(input);
        }
    }
}