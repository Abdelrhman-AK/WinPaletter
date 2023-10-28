using System;
using System.Runtime.InteropServices;
using System.Text;

namespace WinPaletter.NativeMethods
{
    public class Winmm
    {
        [DllImport("winmm.dll")]
        public static extern int mciSendString(string command, StringBuilder buffer, int bufferSize, IntPtr hwndCallback);
    }
}
