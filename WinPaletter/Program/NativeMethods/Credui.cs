using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace WinPaletter.NativeMethods
{
    internal class Credui
    {
        /// <summary>
        /// Struct to hold credential UI information.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct CREDUI_INFO
        {
            /// <summary>
            /// Size of the structure (used by the Windows API).
            /// </summary>
            public int cbSize;

            /// <summary>
            /// Handle to the parent window for the dialog (optional, can be null).
            /// </summary>
            public IntPtr hwndParent;

            /// <summary>
            /// The message to display inside the dialog.
            /// </summary>
            public string pszMessageText;

            /// <summary>
            /// The caption/title text to display on the dialog.
            /// </summary>
            public string pszCaptionText;

            /// <summary>
            /// Handle to a custom banner image (optional, can be null).
            /// </summary>
            public IntPtr hbmBanner;
        }

        /// <summary>
        /// Enum for return codes from the CredUIPromptForWindowsCredentials API.
        /// </summary>
        public enum CredUIReturnCodes
        {
            /// <summary>
            /// No error occurred.
            /// </summary>
            NO_ERROR = 0,

            /// <summary>
            /// The user clicked the Cancel button on the dialog.
            /// </summary>
            ERROR_CANCELLED = 1223,

            /// <summary>
            /// No such logon session.
            /// </summary>
            ERROR_NO_SUCH_LOGON_SESSION = 1312
        }

        /// <summary>
        /// P/Invoke declaration for the CredUIPromptForCredentials function, which is used for Windows XP and earlier.
        /// </summary>
        [DllImport("credui.dll", CharSet = CharSet.Unicode)]
        public static extern CredUIReturnCodes CredUIPromptForCredentials(
            ref CREDUI_INFO pUiInfo,
            string pszTargetName,
            IntPtr Reserved,
            int dwAuthError,
            StringBuilder pszUserName,
            int ulUserNameMaxChars,
            StringBuilder pszPassword,
            int ulPasswordMaxChars,
            [MarshalAs(UnmanagedType.Bool)] ref bool pfSave,
            int dwFlags
        );


        /// <summary>
        /// P/Invoke declaration for the CredUIPromptForWindowsCredentials function, which creates the credentials dialog.
        /// </summary>
        [DllImport("credui.dll", CharSet = CharSet.Unicode)]
        public static extern CredUIReturnCodes CredUIPromptForWindowsCredentials(
            ref CREDUI_INFO notUsedHere,
            int authError,
            ref uint authPackage,
            IntPtr inAuthBuffer,
            uint inAuthBufferSize,
            out IntPtr outAuthBuffer,
            out uint outAuthBufferSize,
            ref bool save,
            int flags
        );

        /// <summary>
        /// P/Invoke declaration for the CredUnPackAuthenticationBuffer function, which unpacks the credentials buffer.
        /// </summary>
        [DllImport("credui.dll", CharSet = CharSet.Unicode)]
        public static extern bool CredUnPackAuthenticationBuffer(
            int dwFlags,
            IntPtr pAuthBuffer,
            uint cbAuthBuffer,
            StringBuilder pszUserName,
            ref int pcchMaxUserName,
            StringBuilder pszDomainName,
            ref int pcchMaxDomainName,
            StringBuilder pszPassword,
            ref int pcchMaxPassword
        );

        /// <summary>
        /// P/Invoke declaration for the CredPackAuthenticationBuffer function, which packs the username and password into a buffer.
        /// </summary>
        [DllImport("credui.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool CredPackAuthenticationBuffer(
            int dwFlags,
            string pszUserName,
            string pszPassword,
            IntPtr pPackedCredentials,
            ref int pcbPackedCredentials
        );

        /// <summary>
        /// This function creates a modern credential dialog, allowing the user to enter their credentials.
        /// </summary>
        /// <param name="parent">The parent window handle.</param>
        /// <param name="caption">The caption to display on the dialog.</param>
        /// <param name="message">The message to display inside the dialog.</param>
        /// <param name="domain">The default domain (optional).</param>
        /// <param name="username">The default username (optional).</param>
        /// <returns>A tuple containing domain, username, password, and a boolean indicating success.</returns>
        private static (string domain, string username, string password, bool success) ShowCredentialDialog(IntPtr parent, string caption, string message, string domain, string username)
        {
            CREDUI_INFO credui = new()
            {
                cbSize = Marshal.SizeOf(typeof(CREDUI_INFO)),
                pszCaptionText = caption,
                pszMessageText = message,
                hwndParent = parent,
                hbmBanner = IntPtr.Zero
            };

            if (!OS.WXP)
            {
                uint authPackage = 0;
                IntPtr inCredBuffer = IntPtr.Zero, outCredBuffer;
                uint inCredSize = 0, outCredSize;
                bool savePassword = false;

                string fullUsername = string.IsNullOrEmpty(domain) ? username : $"{domain}\\{username}";

                if (!string.IsNullOrEmpty(fullUsername))
                {
                    int size = 0;
                    CredPackAuthenticationBuffer(0, fullUsername, string.Empty, IntPtr.Zero, ref size);
                    inCredBuffer = Marshal.AllocCoTaskMem(size);
                    if (!CredPackAuthenticationBuffer(0, fullUsername, string.Empty, inCredBuffer, ref size))
                    {
                        Marshal.FreeCoTaskMem(inCredBuffer);
                        inCredBuffer = IntPtr.Zero;
                    }
                    inCredSize = (uint)size;
                }

                const int flags = 0x00000001 | 0x00000020; // CREDUIWIN_GENERIC | CREDUIWIN_IN_CRED_ONLY

                CredUIReturnCodes result = CredUIPromptForWindowsCredentials(
                    ref credui,
                    0,
                    ref authPackage,
                    inCredBuffer,
                    inCredSize,
                    out outCredBuffer,
                    out outCredSize,
                    ref savePassword,
                    flags
                );

                if (inCredBuffer != IntPtr.Zero)
                    Marshal.FreeCoTaskMem(inCredBuffer);

                if (result == CredUIReturnCodes.NO_ERROR)
                {
                    int maxUser = 0, maxDomain = 0, maxPassword = 0;
                    CredUnPackAuthenticationBuffer(0, outCredBuffer, outCredSize, null, ref maxUser, null, ref maxDomain, null, ref maxPassword);

                    StringBuilder result_userName = new(maxUser);
                    StringBuilder result_domain = new(maxDomain);
                    StringBuilder result_password = new(maxPassword);

                    if (CredUnPackAuthenticationBuffer(0, outCredBuffer, outCredSize, result_userName, ref maxUser, result_domain, ref maxDomain, result_password, ref maxPassword))
                    {
                        Marshal.FreeCoTaskMem(outCredBuffer);

                        string userNameWithDomain = result_userName.ToString();
                        string password = result_password.ToString();
                        string _domain = string.Empty, _userName = string.Empty;

                        int idx = userNameWithDomain.IndexOf('\\');
                        if (idx >= 0)
                        {
                            _domain = userNameWithDomain.Substring(0, idx);
                            _userName = userNameWithDomain.Substring(idx + 1);
                        }
                        else
                        {
                            _userName = userNameWithDomain;
                        }

                        return (_domain, _userName, password, true);
                    }
                    Marshal.FreeCoTaskMem(outCredBuffer);
                }

                return (null, null, null, false);
            }
            else
            {
                StringBuilder user = new(100);
                StringBuilder pass = new(100);
                bool save = false;

                const int flags = 0x40000 | 0x80; // CREDUI_FLAGS_GENERIC_CREDENTIALS | CREDUI_FLAGS_ALWAYS_SHOW_UI

                CredUIReturnCodes result = CredUIPromptForCredentials(
                    ref credui,
                    Environment.MachineName,
                    IntPtr.Zero,
                    0,
                    user,
                    user.Capacity,
                    pass,
                    pass.Capacity,
                    ref save,
                    flags
                );

                if (result == CredUIReturnCodes.NO_ERROR)
                {
                    string userNameWithDomain = user.ToString();
                    string password = pass.ToString();
                    string _domain = string.Empty, _userName = string.Empty;

                    int idx = userNameWithDomain.IndexOf('\\');
                    if (idx >= 0)
                    {
                        _domain = userNameWithDomain.Substring(0, idx);
                        _userName = userNameWithDomain.Substring(idx + 1);
                    }
                    else
                    {
                        _userName = userNameWithDomain;
                    }

                    return (_domain, _userName, password, true);
                }

                return (null, null, null, false);
            }
        }

        /// <summary>
        /// Main method to prompt the user for their credentials and validate them.
        /// </summary>
        /// <param name="parent">The parent window handle.</param>
        /// <param name="caption">The caption for the credentials dialog.</param>
        /// <param name="submessage">The message displayed in the dialog.</param>
        /// <param name="domain">The default domain for the credentials dialog.</param>
        /// <param name="userName">The default username for the credentials dialog.</param>
        /// <returns>A tuple containing the domain, username, password, validation result, and cancellation status.</returns>
        public static (string domain, string username, string password, bool isPasswordCorrect, bool canceled) Login(IntPtr parent, string caption, string submessage, string domain, string userName)
        {
            (string domain, string username, string password, bool success) result = ShowCredentialDialog(parent, caption, submessage, domain, userName);

            // If the credentials dialog was canceled, return early
            if (!result.success)
            {
                return (null, null, null, false, true);
            }

            bool isPasswordCorrect = false;
            bool canceled = false;

            try
            {
                // Attempt to validate the entered password
                if (result.password != null && User.UpdateToken(result.domain, result.username, result.password))
                {
                    isPasswordCorrect = true;
                }
                else
                {
                    canceled = true;
                    throw new User.LogonFailureException($"ERROR_LOGON_FAILURE ({Marshal.GetLastWin32Error()}): {Program.Lang.Strings.Users.ERROR_LOGON_FAILURE}");
                }
            }
            catch (Exception ex)
            {
                // Catch any exceptions related to user login failures
                if (ex is not User.LogonFailureException) MsgBox(ex.Message.Split(':')[0], MessageBoxButtons.OK, MessageBoxIcon.Error, ex.Message.Split(':')[1].Trim());
            }

            canceled = false; // Mark as not canceled
            return (result.domain, result.username, result.password, isPasswordCorrect, canceled);
        }
    }
}