using System;
using System.Runtime.InteropServices;

namespace WinPaletter.NativeMethods
{
    internal class Shlwapi
    {
        private const string _shlwapi = "shlwapi.dll";

        [DllImport(_shlwapi, CharSet = CharSet.Unicode)]
        public static extern bool PathCompactPath(IntPtr hDC, System.Text.StringBuilder lpszPath, uint dwWidth);
    }
}
