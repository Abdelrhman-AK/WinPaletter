using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace WinPaletter.NativeMethods
{
    /// <summary>
    /// Provides P/Invoke declarations for functions in the advapi32.dll library.
    /// </summary>
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

        /// <summary>
        /// Represents the default logon provider constant.
        /// </summary>
        public const int LOGON32_PROVIDER_DEFAULT = 0;

        /// <summary>
        /// Logs a user on to the system.
        /// </summary>
        /// <param name="pszUserName">The name of the user.</param>
        /// <param name="pszDomain">The domain or server whose account database contains the account.</param>
        /// <param name="pszPassword">The plaintext password for the user account.</param>
        /// <param name="dwLogonType">The type of logon operation.</param>
        /// <param name="dwLogonProvider">The logon provider.</param>
        /// <param name="phToken">A handle to the newly created access token.</param>
        /// <returns>True if the function succeeds; otherwise, false.</returns>
        [DllImport("advapi32.dll", SetLastError = true, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool LogonUser(
              [MarshalAs(UnmanagedType.LPStr)] string pszUserName,
              [MarshalAs(UnmanagedType.LPStr)] string pszDomain,
              [MarshalAs(UnmanagedType.LPStr)] string pszPassword,
              int dwLogonType,
              int dwLogonProvider,
              ref IntPtr phToken);

        /// <summary>
        /// Impersonates a logged-on user.
        /// </summary>
        /// <param name="hToken">A handle to a primary or impersonation access token.</param>
        /// <returns>True if the function succeeds; otherwise, false.</returns>
        [DllImport("advapi32.dll", SetLastError = true)]
        public static extern bool ImpersonateLoggedOnUser(IntPtr hToken);

        /// <summary>
        /// Ends the impersonation of a client.
        /// </summary>
        /// <returns>True if the function succeeds; otherwise, false.</returns>
        [DllImport("advapi32.dll", SetLastError = true)]
        public static extern bool RevertToSelf();
        #endregion

        #region Privileges
        internal const int SE_PRIVILEGE_ENABLED = 2;
        internal const int SE_PRIVILEGE_DISABLED = 0;
        internal const int TOKEN_QUERY = 0x0008;  // Use hexadecimal notation for constants
        internal const int TOKEN_ADJUST_PRIVILEGES = 0x0020;

        /// <summary>
        /// Represents a locally unique identifier (LUID) structure.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TokPriv1Luid
        {
            /// <summary>
            /// The number of privileges to adjust.
            /// </summary>
            public int Count;

            /// <summary>
            /// The locally unique identifier (LUID) of the privilege.
            /// </summary>
            public long Luid;

            /// <summary>
            /// The attributes of the privilege.
            /// </summary>
            public int Attr;
        }

        /// <summary>
        /// Adjust the privileges of a token.
        /// </summary>
        /// <param name="htok"></param>
        /// <param name="disall"></param>
        /// <param name="newst"></param>
        /// <param name="len"></param>
        /// <param name="prev"></param>
        /// <param name="relen"></param>
        /// <returns></returns>
        [DllImport("advapi32.dll", SetLastError = true)]
        public static extern bool AdjustTokenPrivileges(IntPtr htok, bool disall, ref TokPriv1Luid newst, int len, IntPtr prev, IntPtr relen);

        /// <summary>
        /// Open the access token associated with a process.
        /// </summary>
        /// <param name="h"></param>
        /// <param name="acc"></param>
        /// <param name="phtok"></param>
        /// <returns></returns>
        [DllImport("advapi32.dll", SetLastError = true)]
        public static extern bool OpenProcessToken(IntPtr h, int acc, ref IntPtr phtok);

        /// <summary>
        /// Look up the locally unique identifier (LUID) for a privilege.
        /// </summary>
        /// <param name="host"></param>
        /// <param name="name"></param>
        /// <param name="pluid"></param>
        /// <returns></returns>
        [DllImport("advapi32.dll", SetLastError = true)]
        public static extern bool LookupPrivilegeValue(string host, string name, ref long pluid);

        /// <summary>
        /// Enable a privilege provided by its string name.
        /// </summary>
        /// <param name="privilege"></param>
        /// <param name="disable"></param>
        /// <returns></returns>
        public static bool EnablePrivilege(string privilege, bool disable)
        {
            IntPtr hProcess = Process.GetCurrentProcess().Handle;
            IntPtr hToken = IntPtr.Zero;

            try
            {
                if (!OpenProcessToken(hProcess, TOKEN_ADJUST_PRIVILEGES | TOKEN_QUERY, ref hToken))
                    return false;

                TokPriv1Luid newst = new()
                {
                    Count = 1,
                    Luid = 0L,
                    Attr = disable ? SE_PRIVILEGE_DISABLED : SE_PRIVILEGE_ENABLED
                };

                if (!LookupPrivilegeValue(null, privilege, ref newst.Luid))
                    return false;

                return AdjustTokenPrivileges(hToken, false, ref newst, 0, IntPtr.Zero, IntPtr.Zero);
            }
            finally
            {
                if (hToken != IntPtr.Zero)
                    Kernel32.CloseHandle(hToken);  // Close the token handle to prevent resource leaks
            }
        }

        #endregion
    }
}