using System;
using System.Runtime.InteropServices;

namespace sync
{
    static class NativeMethods
    {
        [DllImport("kernel32.dll")]
        public static extern bool SetConsoleMode(IntPtr hConsoleHandle, int mode);

        [DllImport("kernel32.dll")]
        public static extern bool GetConsoleMode(IntPtr hConsoleHandle, out int mode);

        [DllImport("kernel32.dll")]
        public static extern IntPtr GetStdHandle(int handle);
    }
}
