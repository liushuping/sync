using System;
using System.Threading;


namespace sync
{
    static class Util
    {
        const string prelude = "sync>";
        const int STD_INPUT_HANDLE = -10;
        const int ENABLE_QUICK_EDIT_MODE = 0x40 | 0x80;

        static int lastPreludePosY;
        static readonly int mainThreadId = Thread.CurrentThread.ManagedThreadId;

        public static void EnableConsoleQuickEditMode()
        {
            int mode;
            var handle = NativeMethods.GetStdHandle(STD_INPUT_HANDLE);
            NativeMethods.GetConsoleMode(handle, out mode);
            mode |= ENABLE_QUICK_EDIT_MODE;
            NativeMethods.SetConsoleMode(handle, mode);
        }

        // not thread safe
        public static void PrintLine(string message)
        {
            lock (typeof(Util))
            {
                var buffer = string.Format("{0}{1}", prelude, message);
                if (IsMainThread())
                {
                    Console.WriteLine(buffer);
                }
                else
                {
                    var lines = (int)Math.Ceiling((double)buffer.Length / Console.BufferWidth);
                    var x = Console.CursorLeft;
                    var y = Console.CursorTop;
                    Console.MoveBufferArea(0, lastPreludePosY, Console.BufferWidth, y - lastPreludePosY + 1, 0, lastPreludePosY + lines);
                    Console.SetCursorPosition(0, lastPreludePosY);
                    Console.WriteLine(buffer);
                    Console.SetCursorPosition(x, y + lines);
                    lastPreludePosY += lines;
                }
            }
        }

        // only for main thread
        public static string ReadLine()
        {
            lastPreludePosY = Console.CursorTop;
            Console.Write(prelude);
            return Console.ReadLine();
        }

        // only for main thread
        public static void ClearConsole()
        {
            Console.Clear();
        }

        private static bool IsMainThread()
        {
            return mainThreadId == Thread.CurrentThread.ManagedThreadId;
        }
    }
}
