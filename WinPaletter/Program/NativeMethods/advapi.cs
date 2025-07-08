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

        /// <summary>
        /// Opens the access token associated with a specified process.
        /// </summary>
        /// <remarks>The caller is responsible for closing the token handle returned in <paramref
        /// name="TokenHandle"/> using the <see cref="CloseHandle"/> function to avoid resource leaks.</remarks>
        /// <param name="ProcessHandle">A handle to the process whose access token is to be opened. This handle must have the <see
        /// langword="PROCESS_QUERY_INFORMATION"/> or <see langword="PROCESS_QUERY_LIMITED_INFORMATION"/> access right.</param>
        /// <param name="DesiredAccess">A bitmask specifying the desired access rights for the token. For example, use <see langword="TOKEN_QUERY"/>
        /// or <see langword="TOKEN_DUPLICATE"/>.</param>
        /// <param name="TokenHandle">When the method returns, contains a handle to the newly opened access token if the operation succeeds.</param>
        /// <returns><see langword="true"/> if the function succeeds; otherwise, <see langword="false"/>. Call <see
        /// cref="Marshal.GetLastWin32Error"/> to retrieve extended error information if the function fails.</returns>
        [DllImport("advapi32.dll", SetLastError = true)]
        public static extern bool OpenProcessToken(IntPtr ProcessHandle, uint DesiredAccess, out IntPtr TokenHandle);

        /// <summary>
        /// Creates a duplicate of an existing access token with specified access rights, attributes, impersonation
        /// level, and token type.
        /// </summary>
        /// <remarks>The caller is responsible for closing the handle returned in <paramref
        /// name="phNewToken"/> using the <see cref="CloseHandle"/> function.</remarks>
        /// <param name="hExistingToken">A handle to the existing access token to duplicate. This handle must have the <see
        /// langword="TOKEN_DUPLICATE"/> access right.</param>
        /// <param name="dwDesiredAccess">Specifies the access rights for the new token. Use standard access rights or a combination of access masks.</param>
        /// <param name="lpTokenAttributes">A pointer to a <see cref="SECURITY_ATTRIBUTES"/> structure that specifies the security attributes for the
        /// new token. Can be <see langword="IntPtr.Zero"/> for default attributes.</param>
        /// <param name="ImpersonationLevel">Specifies the impersonation level for the new token. Use a value from the <see
        /// cref="SECURITY_IMPERSONATION_LEVEL"/> enumeration.</param>
        /// <param name="TokenType">Specifies the type of token to create. Use a value from the <see cref="TOKEN_TYPE"/> enumeration.</param>
        /// <param name="phNewToken">When the method returns, contains a handle to the newly created token if the operation succeeds.</param>
        /// <returns><see langword="true"/> if the token was successfully duplicated; otherwise, <see langword="false"/>. Call
        /// <see cref="Marshal.GetLastWin32Error"/> to retrieve extended error information if the method fails.</returns>
        [DllImport("advapi32.dll", SetLastError = true)]
        public static extern bool DuplicateTokenEx(IntPtr hExistingToken, uint dwDesiredAccess, IntPtr lpTokenAttributes, int ImpersonationLevel, int TokenType, out IntPtr phNewToken);

        /// <summary>
        /// Creates a new process and its primary thread using the specified token, application name, and command line.
        /// </summary>
        /// <remarks>This method is a P/Invoke wrapper for the Windows API function
        /// <c>CreateProcessWithTokenW</c>. It requires the calling process to have the necessary privileges to use the
        /// specified token.</remarks>
        /// <param name="hToken">A handle to the primary token that represents a user. This handle must have the <see
        /// langword="TOKEN_ASSIGN_PRIMARY"/> and <see langword="TOKEN_DUPLICATE"/> access rights.</param>
        /// <param name="dwLogonFlags">The logon option flags. Use 0 for default behavior or <see langword="LOGON_WITH_PROFILE"/> to load the
        /// user's profile.</param>
        /// <param name="lpApplicationName">The name of the module to execute. Can be <see langword="null"/> if the module name is included in <paramref
        /// name="lpCommandLine"/>.</param>
        /// <param name="lpCommandLine">The command line to execute. If <paramref name="lpApplicationName"/> is <see langword="null"/>, the module
        /// name must be the first token in this string.</param>
        /// <param name="dwCreationFlags">The flags that control the priority class and creation of the process. For example, <see
        /// langword="CREATE_NEW_CONSOLE"/> or <see langword="CREATE_SUSPENDED"/>.</param>
        /// <param name="lpEnvironment">A pointer to the environment block for the new process. Use <see langword="IntPtr.Zero"/> to inherit the
        /// environment of the calling process.</param>
        /// <param name="lpCurrentDirectory">The full path to the current directory for the process. Use <see langword="null"/> to use the current
        /// directory of the calling process.</param>
        /// <param name="lpStartupInfo">A reference to a <see cref="STARTUPINFO"/> structure that specifies the window station, desktop, standard
        /// handles, and appearance of the main window for the new process.</param>
        /// <param name="lpProcessInformation">When the method returns, contains a <see cref="PROCESS_INFORMATION"/> structure with information about the
        /// newly created process and its primary thread.</param>
        /// <returns><see langword="true"/> if the process and its primary thread are successfully created; otherwise, <see
        /// langword="false"/>. Call <see cref="Marshal.GetLastWin32Error"/> to retrieve extended error information if
        /// the method fails.</returns>
        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern bool CreateProcessWithTokenW(IntPtr hToken, int dwLogonFlags, string lpApplicationName, string lpCommandLine, int dwCreationFlags, IntPtr lpEnvironment, string lpCurrentDirectory, [In] ref STARTUPINFO lpStartupInfo, out PROCESS_INFORMATION lpProcessInformation);

        /// <summary>
        /// Contains information about a newly created process and its primary thread.
        /// </summary>
        /// <remarks>This structure is typically used with process creation functions, such as <see
        /// cref="CreateProcess"/>,  to retrieve handles and identifiers for the new process and its primary thread. 
        /// The caller is responsible for closing the handles when they are no longer needed by using  <see
        /// cref="CloseHandle"/> to avoid resource leaks.</remarks>
        [StructLayout(LayoutKind.Sequential)]
        public struct PROCESS_INFORMATION
        {
            /// <summary>
            /// A handle to the process associated with this instance.
            /// </summary>
            /// <remarks>This handle can be used to perform operations on the associated process, such
            /// as reading or writing memory, or querying process information. Ensure proper permissions are granted for
            /// the intended operations.</remarks>
            public IntPtr hProcess;

            /// <summary>
            /// A handle to the thread represented as an <see cref="IntPtr"/>.
            /// </summary>
            /// <remarks>This handle can be used to interact with the thread at a low level, such as
            /// for thread management or synchronization. Ensure proper usage and disposal to avoid resource
            /// leaks.</remarks>
            public IntPtr hThread;

            /// <summary>
            /// Represents the unique identifier of a process.
            /// </summary>
            /// <remarks>This field stores the process ID as an unsigned integer. It is typically used
            /// to identify a specific process in the system.</remarks>
            public uint dwProcessId;

            /// <summary>
            /// Represents the unique identifier of a thread.
            /// </summary>
            /// <remarks>This field stores the thread ID as an unsigned 32-bit integer. It is
            /// typically used to identify a specific thread within a process.</remarks>
            public uint dwThreadId;
        }

        /// <summary>
        /// Represents the startup information passed to a new process when using the Windows API to create a process.
        /// </summary>
        /// <remarks>This structure is used with functions such as <see cref="CreateProcess"/> to specify
        /// details about the appearance and behavior of the new process's main window, as well as its standard input,
        /// output, and error handles.  Callers should initialize the <see cref="cb"/> field to the size of this
        /// structure before using it in API calls. Other fields can be set as needed to customize the process startup
        /// behavior.</remarks>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct STARTUPINFO
        {
            /// <summary>
            /// Gets or sets the size, in bytes, of a buffer or data structure.
            /// </summary>
            public int cb;

            /// <summary>
            /// Reserved for future use. This field is not currently used.
            /// </summary>
            public string lpReserved;

            /// <summary>
            /// Gets or sets the name of the desktop associated with the process.
            /// </summary>
            /// <remarks>This property specifies the desktop that the process will use. If the value
            /// is not set,  the process will use the default desktop.</remarks>
            public string lpDesktop;

            /// <summary>
            /// Gets or sets the title associated with the object.
            /// </summary>
            public string lpTitle;

            /// <summary>
            /// Represents the X-coordinate of a point in a 2D space.
            /// </summary>
            public int dwX;

            /// <summary>
            /// Represents the Y-coordinate of a point in a 2D space.
            /// </summary>
            public int dwY;

            /// <summary>
            /// Represents the horizontal size of an object or area, in device units.
            /// </summary>
            /// <remarks>This field typically specifies the width of an object or area in a graphical
            /// context. The value is measured in device-specific units, such as pixels.</remarks>
            public int dwXSize;

            /// <summary>
            /// Represents the vertical size of an object, typically in pixels or units.
            /// </summary>
            /// <remarks>The specific meaning and unit of measurement for this value depend on the
            /// context in which it is used.</remarks>
            public int dwYSize;

            /// <summary>
            /// Gets or sets the number of character cells in the horizontal direction of the console screen buffer.
            /// </summary>
            public int dwXCountChars;

            /// <summary>
            /// Gets or sets the number of character rows in the console screen buffer.
            /// </summary>
            public int dwYCountChars;

            /// <summary>
            /// Represents the fill attribute for a console screen buffer.
            /// </summary>
            /// <remarks>This value specifies the foreground and background color attributes used to
            /// fill a console screen buffer. The value is typically a combination of color constants defined in the
            /// console API.</remarks>
            public int dwFillAttribute;

            /// <summary>
            /// Represents a set of flags as an integer value.
            /// </summary>
            /// <remarks>The meaning of the flags is determined by the specific context in which this
            /// field is used. Refer to the associated documentation for details on the possible values and their
            /// effects.</remarks>
            public int dwFlags;

            /// <summary>
            /// Specifies the command to be sent to the window to determine how it is to be shown.
            /// </summary>
            public short wShowWindow;

            /// <summary>
            /// Reserved for future use. This field is not currently used.
            /// </summary>
            public short cbReserved2;

            /// <summary>
            /// Reserved for system use. This field is not intended to be used directly in application code.
            /// </summary>
            public IntPtr lpReserved2;

            /// <summary>
            /// Represents a handle to the standard input stream.
            /// </summary>
            /// <remarks>This field typically holds a pointer to the standard input stream for a
            /// process. It is commonly used in interop scenarios or when working with unmanaged code.</remarks>
            public IntPtr hStdInput;

            /// <summary>
            /// Represents a handle to the standard output stream.
            /// </summary>
            /// <remarks>This field typically holds a pointer to the standard output stream for a
            /// process. It is commonly used in interop scenarios or when working with unmanaged code.</remarks>
            public IntPtr hStdOutput;

            /// <summary>
            /// Represents a handle to the standard error output stream for a process.
            /// </summary>
            /// <remarks>This field typically holds a pointer to the standard error stream used by a
            /// process.  It is commonly used in scenarios where process input/output redirection is required.</remarks>
            public IntPtr hStdError;
        }

        /// <summary>
        /// Represents the access right that allows a token to be duplicated.
        /// </summary>
        /// <remarks>This constant is used when specifying access rights for token-related operations,
        /// such as duplicating a security token.</remarks>
        public const uint TOKEN_DUPLICATE = 0x0002;

        /// <summary>
        /// Represents the access right required to assign the primary token of a process.
        /// </summary>
        /// <remarks>This constant is used in security-related operations to specify the permission needed
        /// to assign a primary token to a process. It is typically used in conjunction with Windows API functions that
        /// manage process tokens.</remarks>
        public const uint TOKEN_ASSIGN_PRIMARY = 0x0001;

        /// <summary>
        /// Represents the access right required to adjust the default owner, primary group, or default discretionary
        /// access control list (DACL) of a token.
        /// </summary>
        /// <remarks>This constant is used in security-related operations involving access tokens, such as
        /// when modifying token privileges or attributes.</remarks>
        public const uint TOKEN_ADJUST_DEFAULT = 0x0080;

        /// <summary>
        /// Represents the constant value used to specify the adjustment of a session ID in token-related operations.
        /// </summary>
        /// <remarks>This constant is typically used in conjunction with Windows API functions that
        /// require token access rights.</remarks>
        public const uint TOKEN_ADJUST_SESSIONID = 0x0100;

        /// <summary>
        /// Represents the security impersonation level used in access control operations.
        /// </summary>
        /// <remarks>The value of <see langword="2"/> corresponds to the "SecurityImpersonation" level, 
        /// which allows a server process to impersonate the security context of a client.</remarks>
        public const int SecurityImpersonation = 2;

        /// <summary>
        /// Represents the primary token identifier used for internal processing.
        /// </summary>
        public const int TokenPrimary = 1;

        /// <summary>
        /// Represents the flag used to specify the creation of a new console when starting a process.
        /// </summary>
        /// <remarks>This constant is typically used in process creation scenarios, such as when invoking
        /// native APIs like <c>CreateProcess</c>, to indicate that a new console should be created for the
        /// process.</remarks>
        public const int CREATE_NEW_CONSOLE = 0x00000010;

        /// <summary>
        /// Represents the flag used to specify that the logon operation should load the user's profile.
        /// </summary>
        /// <remarks>This constant is typically used in conjunction with logon-related functions to
        /// indicate that the user's profile  should be loaded during the logon process. The value of this constant is
        /// <c>0x00000001</c>.</remarks>
        public const int LOGON_WITH_PROFILE = 0x00000001;

        #endregion
    }
}