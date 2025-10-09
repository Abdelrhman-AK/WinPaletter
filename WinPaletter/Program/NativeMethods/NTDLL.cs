using System;
using System.Runtime.InteropServices;

namespace WinPaletter.NativeMethods
{
    public static class NTDLL
    {
        [DllImport("ntdll.dll", CharSet = CharSet.Unicode)]
        public static extern int RtlGetVersion(ref OSVERSIONINFOEX lpVersionInformation);

        [StructLayout(LayoutKind.Sequential)]
        public struct OSVERSIONINFOEX
        {
            public int dwOSVersionInfoSize;
            public int dwMajorVersion;
            public int dwMinorVersion;
            public int dwBuildNumber;
            public int dwPlatformId;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string szCSDVersion;
        }

        public static Version GetOSVersion()
        {
            OSVERSIONINFOEX v = new();
            v.dwOSVersionInfoSize = Marshal.SizeOf(v);
            RtlGetVersion(ref v);

            return new Version(v.dwMajorVersion, v.dwMinorVersion, v.dwBuildNumber);
        }
    }
}
