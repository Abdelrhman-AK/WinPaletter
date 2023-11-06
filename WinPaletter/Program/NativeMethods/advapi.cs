using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace WinPaletter.NativeMethods
{
    public class advapi
    {
        #region Users
        /// <summary>
        /// Logon as a user on the local machine
        /// </summary>
        public const int LOGON32_LOGON_INTERACTIVE = 2;

        /// <summary>
        /// Logon using network credentials
        /// </summary>
        public const int LOGON32_LOGON_NETWORK = 3;

        /// <summary>
        /// Logon as a batch job (used for scheduled tasks)
        /// </summary>
        public const int LOGON32_LOGON_BATCH = 4;

        /// <summary>
        /// Logon as a service (used for Windows services)
        /// </summary>
        public const int LOGON32_LOGON_SERVICE = 5;

        /// <summary>
        /// Unlock a previously locked workstation
        /// </summary>
        public const int LOGON32_LOGON_UNLOCK = 7;

        /// <summary>
        /// Logon with plaintext credentials over the network (not recommended for security reasons)
        /// </summary>
        public const int LOGON32_LOGON_NETWORK_CLEARTEXT = 8;

        /// <summary>
        /// Logon with new credentials (revalidate the user)
        /// </summary>
        public const int LOGON32_LOGON_NEW_CREDENTIALS = 9;

        public const int LOGON32_PROVIDER_DEFAULT = 0;

        [DllImport("advapi32.dll", SetLastError = true, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool LogonUser(
              [MarshalAs(UnmanagedType.LPStr)] string pszUserName,
              [MarshalAs(UnmanagedType.LPStr)] string pszDomain,
              [MarshalAs(UnmanagedType.LPStr)] string pszPassword,
              int dwLogonType,
              int dwLogonProvider,
              ref IntPtr phToken);

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool ImpersonateLoggedOnUser(IntPtr hToken);

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool RevertToSelf();
        #endregion

        #region Privileges
        internal const int SE_PRIVILEGE_ENABLED = 2;
        internal const int SE_PRIVILEGE_DISABLED = 0;
        internal const int TOKEN_QUERY = 8;
        internal const int TOKEN_ADJUST_PRIVILEGES = 32;

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TokPriv1Luid
        {
            public int Count;
            public long Luid;
            public int Attr;
        }

        [DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern bool AdjustTokenPrivileges(IntPtr htok, bool disall, ref TokPriv1Luid newst, int len, IntPtr prev, IntPtr relen);

        [DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern bool OpenProcessToken(IntPtr h, int acc, ref IntPtr phtok);

        [DllImport("advapi32.dll", SetLastError = true)]
        public static extern bool LookupPrivilegeValue(string host, string name, ref long pluid);

        public static bool EnablePrivilege(string privilege, bool disable)
        {
            long value = Process.GetCurrentProcess().Handle.ToInt32();
            var h = new IntPtr(value);
            var phtok = IntPtr.Zero;
            bool flag = OpenProcessToken(h, 40, ref phtok);
            TokPriv1Luid newst = default;
            newst.Count = 1;
            newst.Luid = 0L;
            newst.Attr = disable ? 0 : 2;
            flag = LookupPrivilegeValue(null, privilege, ref newst.Luid);
            return AdjustTokenPrivileges(phtok, disall: false, ref newst, 0, IntPtr.Zero, IntPtr.Zero);
        }
    }
    #endregion
}