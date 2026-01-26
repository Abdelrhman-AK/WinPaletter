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
        /// Displays a dialog box to prompt the user for credentials.
        /// </summary>
        /// <remarks>This method is a P/Invoke wrapper for the CredUIPromptForCredentials function in the
        /// Windows Credential UI API.  It displays a standard Windows dialog box to collect user credentials, such as a
        /// username and password.</remarks>
        /// <param name="pUiInfo">A reference to a <see cref="CREDUI_INFO"/> structure that contains information about the dialog box, such as
        /// its title and message.</param>
        /// <param name="pszTargetName">The name of the target for which the credentials are being requested. This is typically a server or resource
        /// name.</param>
        /// <param name="Reserved">Reserved for future use. Must be set to <see cref="IntPtr.Zero"/>.</param>
        /// <param name="dwAuthError">An authentication error code that determines the error message displayed in the dialog box. Use 0 if no
        /// error message is required.</param>
        /// <param name="pszUserName">A <see cref="StringBuilder"/> to receive the user name entered by the user. The buffer must be pre-allocated
        /// with a size of at least <paramref name="ulUserNameMaxChars"/> characters.</param>
        /// <param name="ulUserNameMaxChars">The maximum number of characters that can be written to <paramref name="pszUserName"/>.</param>
        /// <param name="pszPassword">A <see cref="StringBuilder"/> to receive the password entered by the user. The buffer must be pre-allocated
        /// with a size of at least <paramref name="ulPasswordMaxChars"/> characters.</param>
        /// <param name="ulPasswordMaxChars">The maximum number of characters that can be written to <paramref name="pszPassword"/>.</param>
        /// <param name="pfSave">A reference to a boolean value that indicates whether the "Save my credentials" checkbox is selected. The
        /// initial value determines the checkbox state, and the updated value reflects the user's selection.</param>
        /// <param name="dwFlags">A set of flags that control the behavior and appearance of the dialog box. These flags are defined in the
        /// CredUI API documentation.</param>
        /// <returns>A <see cref="CredUIReturnCodes"/> value indicating the result of the operation. Possible values include <see
        /// cref="CredUIReturnCodes.NO_ERROR"/> if the user successfully entered credentials, or other error codes if
        /// the operation failed or was canceled.</returns>
        [DllImport("credui.dll", CharSet = CharSet.Unicode)]
        public static extern CredUIReturnCodes CredUIPromptForCredentials(ref CREDUI_INFO pUiInfo, string pszTargetName, IntPtr Reserved, int dwAuthError, StringBuilder pszUserName, int ulUserNameMaxChars, StringBuilder pszPassword, int ulPasswordMaxChars, [MarshalAs(UnmanagedType.Bool)] ref bool pfSave, int dwFlags);

        /// <summary>
        /// Displays a Windows Security dialog box to prompt the user for credentials.
        /// </summary>
        /// <remarks>This method is a P/Invoke wrapper for the CredUIPromptForWindowsCredentials function in the Windows
        /// Credential UI API. It allows applications to securely prompt users for credentials in a standardized Windows dialog
        /// box.</remarks>
        /// <param name="notUsedHere">A reference to a <see cref="CREDUI_INFO"/> structure that contains information about the dialog box. This parameter
        /// is not used in this implementation.</param>
        /// <param name="authError">An authentication error code to display in the dialog box. Pass 0 if no error message is required.</param>
        /// <param name="authPackage">A reference to a variable that specifies the authentication package. This value is updated with the selected
        /// authentication package.</param>
        /// <param name="inAuthBuffer">A pointer to a buffer containing pre-filled authentication information. Pass <see cref="IntPtr.Zero"/> if no
        /// pre-filled information is provided.</param>
        /// <param name="inAuthBufferSize">The size, in bytes, of the buffer pointed to by <paramref name="inAuthBuffer"/>.</param>
        /// <param name="outAuthBuffer">An output pointer to a buffer that receives the user's credentials. The caller must free this buffer using <see
        /// cref="Marshal.FreeCoTaskMem"/>.</param>
        /// <param name="outAuthBufferSize">The size, in bytes, of the buffer returned in <paramref name="outAuthBuffer"/>.</param>
        /// <param name="save">A reference to a boolean value that indicates whether the "Save my credentials" checkbox is selected. The value is
        /// updated based on the user's selection.</param>
        /// <param name="flags">A combination of flags that control the behavior and appearance of the dialog box. See the
        /// CredUIPromptForWindowsCredentials documentation for valid flag values.</param>
        /// <returns>A <see cref="CredUIReturnCodes"/> value indicating the result of the operation. Possible values include <see
        /// cref="CredUIReturnCodes.NO_ERROR"/> for success or other error codes for failure.</returns>
        [DllImport("credui.dll", CharSet = CharSet.Unicode)]
        public static extern CredUIReturnCodes CredUIPromptForWindowsCredentials(ref CREDUI_INFO notUsedHere, int authError, ref uint authPackage, IntPtr inAuthBuffer, uint inAuthBufferSize, out IntPtr outAuthBuffer, out uint outAuthBufferSize, ref bool save, int flags);

        /// <summary>
        /// Unpacks the authentication buffer into its constituent user name, domain name, and password components.
        /// </summary>
        /// <remarks>This function is typically used to extract user credentials from an authentication
        /// buffer for further processing. The caller is responsible for ensuring that the buffers provided for
        /// <paramref name="pszUserName"/>, <paramref name="pszDomainName"/>, and <paramref name="pszPassword"/> are
        /// large enough to hold the unpacked data.</remarks>
        /// <param name="dwFlags">Flags that control the behavior of the function. This parameter can be used to specify options for unpacking
        /// the buffer.</param>
        /// <param name="pAuthBuffer">A pointer to the authentication buffer containing the packed credentials. This buffer is typically obtained
        /// from a previous call to a credential-related API.</param>
        /// <param name="cbAuthBuffer">The size, in bytes, of the authentication buffer pointed to by <paramref name="pAuthBuffer"/>.</param>
        /// <param name="pszUserName">A <see cref="StringBuilder"/> that receives the unpacked user name. The buffer must be pre-allocated by the
        /// caller.</param>
        /// <param name="pcchMaxUserName">A reference to an integer that specifies the size, in characters, of the <paramref name="pszUserName"/>
        /// buffer. On return, this value is updated to the actual number of characters written, including the null
        /// terminator.</param>
        /// <param name="pszDomainName">A <see cref="StringBuilder"/> that receives the unpacked domain name. The buffer must be pre-allocated by
        /// the caller.</param>
        /// <param name="pcchMaxDomainName">A reference to an integer that specifies the size, in characters, of the <paramref name="pszDomainName"/>
        /// buffer. On return, this value is updated to the actual number of characters written, including the null
        /// terminator.</param>
        /// <param name="pszPassword">A <see cref="StringBuilder"/> that receives the unpacked password. The buffer must be pre-allocated by the
        /// caller.</param>
        /// <param name="pcchMaxPassword">A reference to an integer that specifies the size, in characters, of the <paramref name="pszPassword"/>
        /// buffer. On return, this value is updated to the actual number of characters written, including the null
        /// terminator.</param>
        /// <returns><see langword="true"/> if the function succeeds; otherwise, <see langword="false"/>. If the function fails,
        /// use <c>Marshal.GetLastWin32Error</c> to retrieve the error code.</returns>
        [DllImport("credui.dll", CharSet = CharSet.Unicode)]
        public static extern bool CredUnPackAuthenticationBuffer(int dwFlags, IntPtr pAuthBuffer, uint cbAuthBuffer, StringBuilder pszUserName, ref int pcchMaxUserName, StringBuilder pszDomainName, ref int pcchMaxDomainName, StringBuilder pszPassword, ref int pcchMaxPassword);

        /// <summary>
        /// Packs the specified user name and password into a credential buffer for use with authentication.
        /// </summary>
        /// <remarks>This method is a P/Invoke wrapper for the CredPackAuthenticationBuffer function in the Windows
        /// Credential UI API. It is used to prepare credentials for use in authentication scenarios. Ensure that the buffer
        /// size specified by <paramref name="pcbPackedCredentials"/> is sufficient to hold the packed credentials.</remarks>
        /// <param name="dwFlags">Flags that modify the behavior of the function. This parameter can be a combination of predefined values.</param>
        /// <param name="pszUserName">The user name to be packed into the credential buffer. This cannot be <see langword="null"/>.</param>
        /// <param name="pszPassword">The password to be packed into the credential buffer. This cannot be <see langword="null"/>.</param>
        /// <param name="pPackedCredentials">A pointer to a buffer that receives the packed credentials. The buffer must be allocated by the caller.</param>
        /// <param name="pcbPackedCredentials">On input, specifies the size, in bytes, of the buffer pointed to by <paramref name="pPackedCredentials"/>. On
        /// output, receives the size, in bytes, of the packed credentials.</param>
        /// <returns><see langword="true"/> if the credentials were successfully packed; otherwise, <see langword="false"/>. Call <see
        /// cref="Marshal.GetLastWin32Error"/> to retrieve extended error information if the function fails.</returns>
        [DllImport("credui.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool CredPackAuthenticationBuffer(int dwFlags, string pszUserName, string pszPassword, IntPtr pPackedCredentials, ref int pcbPackedCredentials);

        /// <summary>
        /// Displays a credential dialog to prompt the user for domain, username, and password credentials.
        /// </summary>
        /// <remarks>This method uses the Windows Credential UI API to display a dialog for collecting user credentials. 
        /// The dialog behavior may vary depending on the operating system version. On Windows XP, a simpler dialog is
        /// used.</remarks>
        /// <param name="parent">A handle to the parent window for the dialog. Can be <see cref="IntPtr.Zero"/> if no parent window is specified.</param>
        /// <param name="caption">The caption text displayed in the dialog.</param>
        /// <param name="message">The message text displayed in the dialog to provide context to the user.</param>
        /// <param name="domain">The initial domain to prepopulate in the dialog. Can be an empty string if no domain is specified.</param>
        /// <param name="username">The initial username to prepopulate in the dialog. Can be an empty string if no username is specified.</param>
        /// <returns>A tuple containing the following values: <list type="bullet"> <item><description><c>domain</c>: The domain entered
        /// by the user, or <c>null</c> if the operation was unsuccessful.</description></item>
        /// <item><description><c>username</c>: The username entered by the user, or <c>null</c> if the operation was
        /// unsuccessful.</description></item> <item><description><c>password</c>: The password entered by the user, or
        /// <c>null</c> if the operation was unsuccessful.</description></item> <item><description><c>success</c>: <see
        /// langword="true"/> if the user successfully provided credentials; otherwise, <see
        /// langword="false"/>.</description></item> </list></returns>
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

                CredUIReturnCodes result = CredUIPromptForWindowsCredentials(ref credui, 0, ref authPackage, inCredBuffer, inCredSize, out outCredBuffer, out outCredSize, ref savePassword, flags);

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

                CredUIReturnCodes result = CredUIPromptForCredentials(ref credui, Environment.MachineName, IntPtr.Zero, 0, user, user.Capacity, pass, pass.Capacity, ref save, flags);

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
        /// Displays a credentials dialog to the user, validates the entered credentials, and returns the result.
        /// </summary>
        /// <remarks>This method displays a credentials dialog to the user, allowing them to enter a domain, username, and
        /// password. The entered credentials are validated, and the result is returned as a tuple. If the dialog is canceled,
        /// the returned tuple will contain <see langword="null"/> for the domain, username, and password, and <c>canceled</c>
        /// will be <see langword="true"/>.</remarks>
        /// <param name="parent">A handle to the parent window for the credentials dialog. Can be <see cref="IntPtr.Zero"/> if no parent window is
        /// specified.</param>
        /// <param name="caption">The caption text displayed in the credentials dialog.</param>
        /// <param name="submessage">The submessage text providing additional context in the credentials dialog.</param>
        /// <param name="domain">The default domain to prepopulate in the credentials dialog. Can be <see langword="null"/> or empty.</param>
        /// <param name="userName">The default username to prepopulate in the credentials dialog. Can be <see langword="null"/> or empty.</param>
        /// <returns>A tuple containing the following values: <list type="bullet"> <item><description><c>domain</c>: The domain entered
        /// by the user, or <see langword="null"/> if the dialog was canceled.</description></item>
        /// <item><description><c>username</c>: The username entered by the user, or <see langword="null"/> if the dialog was
        /// canceled.</description></item> <item><description><c>password</c>: The password entered by the user, or <see
        /// langword="null"/> if the dialog was canceled.</description></item> <item><description><c>isPasswordCorrect</c>: <see
        /// langword="true"/> if the entered password was successfully validated; otherwise, <see
        /// langword="false"/>.</description></item> <item><description><c>canceled</c>: <see langword="true"/> if the user
        /// canceled the credentials dialog; otherwise, <see langword="false"/>.</description></item> </list></returns>
        public static (string domain, string username, string password, bool isPasswordCorrect, bool canceled) Login(IntPtr parent, string caption, string submessage, string domain, string userName)
        {
            // First, try to update the token with existing credentials without password
            if (User.UpdateToken(domain, userName, null, true))
            {
                return (domain, userName, null, true, false);
            }

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
                    throw new User.LogonFailureException($"ERROR_LOGON_FAILURE ({Marshal.GetLastWin32Error()}): {Program.Localization.Strings.Users.ERROR_LOGON_FAILURE}");
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