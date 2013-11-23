using System;

namespace sync
{
    class Program
    {
        static void Main(string[] args)
        {
            Initialize();
            Run();
        }

        static void Initialize()
        {
            Util.EnableConsoleQuickEditMode();
            Console.Title = "sync";
        }

        static void Run()
        {
            var dispatcher = new Dispatcher();
            while (dispatcher.Dispatch(Util.ReadLine())) ;
        }
    }
}