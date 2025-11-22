using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace WinPaletter.NativeMethods
{
    /// <summary>
    /// Provides P/Invoke declarations for functions in the advapi32.dll library.
    /// </summary>
    public class ADVAPI
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
        public static extern bool LogonUser([MarshalAs(UnmanagedType.LPStr)] string pszUserName, [MarshalAs(UnmanagedType.LPStr)] string pszDomain, [MarshalAs(UnmanagedType.LPStr)] string pszPassword, int dwLogonType, int dwLogonProvider, ref IntPtr phToken);

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

        /// <summary>
        /// Converts a security descriptor string (SDDL) into a valid Windows security descriptor structure in memory.
        /// </summary>
        /// <param name="StringSecurityDescriptor">
        /// The SDDL string representing the security descriptor to convert.
        /// Example: "O:BAG:SYD:(A;;CCLCRC;;;IU)(A;;CCDCLCSWRPSDRCWDWO;;;SY)".
        /// </param>
        /// <param name="StringSDRevision">
        /// The revision level of the security descriptor string. Usually 1 (SE_SD_REVISION_1).
        /// </param>
        /// <param name="SecurityDescriptor">
        /// Outputs a pointer to the resulting security descriptor structure in memory.
        /// Must be freed with <see cref="Marshal.FreeHGlobal"/> if manually allocated.
        /// </param>
        /// <param name="SecurityDescriptorSize">
        /// Outputs the size, in bytes, of the security descriptor structure.
        /// </param>
        /// <returns>
        /// True if the conversion succeeds; otherwise, false. Use <see cref="Marshal.GetLastWin32Error"/> to get error information.
        /// </returns>
        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern bool ConvertStringSecurityDescriptorToSecurityDescriptor(string StringSecurityDescriptor, int StringSDRevision, out IntPtr SecurityDescriptor, out int SecurityDescriptorSize);

        /// <summary>
        /// Converts a Windows security descriptor structure in memory into a string in Security Descriptor Definition Language (SDDL) format.
        /// </summary>
        /// <param name="SecurityDescriptor">
        /// A pointer to the security descriptor structure to convert.
        /// </param>
        /// <param name="RequestedStringSDRevision">
        /// The revision level of the string security descriptor. Usually 1 (SE_SD_REVISION_1).
        /// </param>
        /// <param name="SecurityInformation">
        /// A bitmask that specifies which components of the security descriptor to convert.
        /// Commonly DACL_SECURITY_INFORMATION (0x4) to include DACL information.
        /// </param>
        /// <param name="StringSecurityDescriptor">
        /// Outputs a pointer to the resulting SDDL string in memory. Must be freed with <see cref="Marshal.FreeHGlobal"/> when no longer needed.
        /// </param>
        /// <param name="StringSecurityDescriptorLen">
        /// Outputs the length, in characters, of the SDDL string.
        /// </param>
        /// <returns>
        /// True if the conversion succeeds; otherwise, false. Use <see cref="Marshal.GetLastWin32Error"/> for error information.
        /// </returns>
        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern bool ConvertSecurityDescriptorToStringSecurityDescriptor(IntPtr SecurityDescriptor, int RequestedStringSDRevision, uint SecurityInformation, out IntPtr StringSecurityDescriptor, out int StringSecurityDescriptorLen);

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

        #region Credentials

        /// <summary>
        /// Represents a credential stored in the Windows Credential Manager.
        /// </summary>
        /// <remarks>
        /// This struct maps directly to the native Windows CREDENTIAL structure.  
        /// It is used with P/Invoke functions such as <see cref="CredRead"/>, <see cref="CredWrite"/>, and <see cref="CredDelete"/>.
        /// Ensure <see cref="CharSet.Unicode"/> is specified to properly marshal string fields.
        /// </remarks>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct CREDENTIAL
        {
            /// <summary>
            /// Flags for the credential. Typically 0.
            /// </summary>
            public int Flags;

            /// <summary>
            /// The type of credential. Use <see cref="CRED_TYPE_GENERIC"/> for generic credentials.
            /// </summary>
            public int Type;

            /// <summary>
            /// The target name for the credential (used as the lookup key).
            /// </summary>
            public string TargetName;

            /// <summary>
            /// Optional comment for the credential.
            /// </summary>
            public string Comment;

            /// <summary>
            /// The last write time for the credential.
            /// </summary>
            public System.Runtime.InteropServices.ComTypes.FILETIME LastWritten;

            /// <summary>
            /// Size, in bytes, of the <see cref="CredentialBlob"/>.
            /// </summary>
            public int CredentialBlobSize;

            /// <summary>
            /// Pointer to the credential secret (e.g., password or token) in unmanaged memory.
            /// </summary>
            public IntPtr CredentialBlob;

            /// <summary>
            /// Persistence type. Use <see cref="CRED_PERSIST_LOCAL_MACHINE"/> for local machine persistence.
            /// </summary>
            public int Persist;

            /// <summary>
            /// Number of custom attributes. Usually 0.
            /// </summary>
            public int AttributeCount;

            /// <summary>
            /// Pointer to custom attributes. Usually <c>IntPtr.Zero</c>.
            /// </summary>
            public IntPtr Attributes;

            /// <summary>
            /// Optional alias for the target. Can be <c>null</c>.
            /// </summary>
            public string TargetAlias;

            /// <summary>
            /// The user name associated with the credential.
            /// </summary>
            public string UserName;
        }

        /// <summary>
        /// Credential type for generic credentials.  
        /// Use with <see cref="CREDENTIAL.Type"/> and P/Invoke functions.
        /// </summary>
        public const int CRED_TYPE_GENERIC = 1;

        /// <summary>
        /// Credential persistence type: local machine.  
        /// Stored credentials are available to all users on the machine.
        /// </summary>
        public const int CRED_PERSIST_LOCAL_MACHINE = 2;

        /// <summary>
        /// Writes a credential to the Windows Credential Manager.
        /// </summary>
        /// <param name="userCredential">The credential struct to write.</param>
        /// <param name="flags">Reserved. Pass 0.</param>
        /// <returns><c>true</c> if the operation succeeds; otherwise, <c>false</c>.</returns>
        /// <remarks>
        /// Use <see cref="Marshal.GetLastWin32Error"/> to retrieve the error code if the call fails.
        /// </remarks>
        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern bool CredWrite([In] ref CREDENTIAL userCredential, [In] uint flags);

        /// <summary>
        /// Reads a credential from the Windows Credential Manager.
        /// </summary>
        /// <param name="target">The target name of the credential to retrieve.</param>
        /// <param name="type">The credential type. Typically <see cref="CRED_TYPE_GENERIC"/>.</param>
        /// <param name="reservedFlag">Reserved. Pass 0.</param>
        /// <param name="credentialPtr">Receives a pointer to the unmanaged CREDENTIAL struct.</param>
        /// <returns><c>true</c> if the credential is found; otherwise, <c>false</c>.</returns>
        /// <remarks>
        /// The returned pointer must be freed using <see cref="CredFree"/> to avoid memory leaks.
        /// </remarks>
        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern bool CredRead(string target, int type, int reservedFlag, out IntPtr credentialPtr);

        /// <summary>
        /// Frees unmanaged memory allocated by <see cref="CredRead"/>.
        /// </summary>
        /// <param name="buffer">Pointer to the credential struct returned by <see cref="CredRead"/>.</param>
        [DllImport("advapi32.dll", SetLastError = true)]
        public static extern void CredFree([In] IntPtr buffer);

        /// <summary>
        /// Deletes a credential from the Windows Credential Manager.
        /// </summary>
        /// <param name="target">The target name of the credential to delete.</param>
        /// <param name="type">The credential type. Typically <see cref="CRED_TYPE_GENERIC"/>.</param>
        /// <param name="flags">Reserved. Pass 0.</param>
        /// <returns><c>true</c> if the deletion succeeds; otherwise, <c>false</c>.</returns>
        /// <remarks>
        /// Use <see cref="Marshal.GetLastWin32Error"/> to retrieve the error code if the call fails.
        /// </remarks>
        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern bool CredDelete(string target, int type, int flags);

        #endregion
    }
}