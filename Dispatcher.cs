using System.Collections.Generic;

namespace sync
{
    class Dispatcher
    {
        private Handler handler;

        public Dispatcher()
        {
            handler = new Discard(
                new Quit(
                    new Clear(
                        new Sync(
                            new Default(null)
                        )
                    )
                )
            );
        }

        public bool Dispatch(string input)
        {
            return !handler.Execute(input);
        }
    }
}
