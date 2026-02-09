using Octokit;
using Serilog.Events;
using System;
using System.Diagnostics;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WinPaletter.GitHub
{
    public class LoginManager : IDisposable
    {
        /// <summary>
        /// Client used for GitHub API interactions.
        /// </summary>
        public GitHubClient Client { get; private set; }
        private bool _disposed = false;
        private const string ClientId = "db4a27d1c8b8ba156eb5"; // WinPaletter's GitHub OAuth App
        private OauthDeviceFlowResponse deviceFlow;
        private CancellationTokenSource _cancellationTokenSource;

        /// <summary>
        /// Creates a new instance of the <see cref="LoginManager"/> class.
        /// </summary>
        public LoginManager()
        {
            Client = new(new ProductHeaderValue(System.Windows.Forms.Application.ProductName));
        }

        #region Public Login Methods

        /// <summary>
        /// Checks if there is a valid saved GitHub token and attempts to log in silently.
        /// </summary>
        /// <returns>
        /// <c>true</c> if a valid token exists and the login is successful; otherwise, <c>false</c>.
        /// </returns>
        /// <remarks>
        /// This method will:
        /// - Load the token from local storage.
        /// - Set it as the current <see cref="Client"/> credentials.
        /// - Attempt to fetch the current GitHub user to verify validity.
        /// 
        /// Events triggered:
        /// - <see cref="OnTokenLoaded"/> when a token is loaded successfully.
        /// - <see cref="OnTokenInvalid"/> if the token is expired or invalid.
        /// - <see cref="OnUnexpectedError"/> if any unexpected exception occurs during the process.
        /// 
        /// Use this method to determine if the user is already logged in without prompting for credentials.
        /// </remarks>
        public async Task<bool> IsLoggedInAsync()
        {
            // Check network first
            if (!Program.IsNetworkAvailable)
            {
                Program.Log?.Write(LogEventLevel.Warning, "Cannot check login: Network unavailable.");
                Events.OnNetworkLost();
                return false;
            }

            string token = LoadToken();
            if (string.IsNullOrEmpty(token))
            {
                Program.Log?.Write(LogEventLevel.Information, "No saved GitHub token found.");
                return false;
            }

            Client.Credentials = new(token);
            Events.OnTokenLoadedEvent(token);
            Program.Log?.Write(LogEventLevel.Information, "Token loaded from local storage.");

            // Wrap the GitHub call with the safe helper
            bool result = await Helpers.Do(async () =>
            {
                try
                {
                    Octokit.User user = await Client.User.Current();
                    Program.Log?.Write(LogEventLevel.Information, $"Logged in as {user.Login}.");
                    return true;
                }
                catch (AuthorizationException)
                {
                    Program.Log?.Write(LogEventLevel.Warning, "Saved token invalid or expired.");
                    Events.OnTokenInvalidEvent();
                    DeleteToken();
                    return false;
                }
            });

            // Optional: handle cases where the helper returned default due to network/server/rate-limit
            if (!result)
            {
                Program.Log?.Write(LogEventLevel.Warning, "Login check could not complete due to network, rate limit, or server issues.");
            }

            return result;
        }

        /// <summary>
        /// Starts the GitHub Device Flow login process asynchronously.
        /// </summary>
        /// <remarks>
        /// This method will:
        /// - Trigger the <see cref="OnLoginInProgress"/> event to indicate that the login has started.
        /// - Initiate the device flow login by calling <see cref="InitiateDeviceFlowLoginAsync"/>,
        ///   which opens the verification URL in the browser and copies the user code to the clipboard.
        /// 
        /// Use this method when the user needs to log in or re-authenticate.
        /// </remarks>
        public async Task StartLoggingInAsync()
        {
            if (!Program.IsNetworkAvailable)
            {
                Program.Log?.Write(LogEventLevel.Warning, "Cannot start login: Network unavailable.");
                Events.OnNetworkLost();
                return;
            }

            Events.OnNetworkLost();
            Program.Log?.Write(LogEventLevel.Information, "Starting GitHub Device Flow login.");
            await InitiateDeviceFlowLoginAsync();
        }

        /// <summary>
        /// Cancels any ongoing GitHub Device Flow login operation.
        /// </summary>
        /// <remarks>
        /// This method signals the internal <see cref="CancellationTokenSource"/> to cancel
        /// the current login polling task. If a login is in progress, the <see cref="OnLoginCanceled"/> 
        /// event will be triggered.
        /// </remarks>
        public void CancelLogin()
        {
            if (_cancellationTokenSource != null)
            {
                Program.Log?.Write(LogEventLevel.Information, "Cancelling ongoing GitHub Device Flow login...");

                _cancellationTokenSource.Cancel();
                _cancellationTokenSource.Dispose();
                _cancellationTokenSource = null;

                Program.Log?.Write(LogEventLevel.Information, "GitHub Device Flow login cancelled successfully.");
                Events.OnNetworkLost();
            }
            else
            {
                Program.Log?.Write(LogEventLevel.Warning, "CancelLogin called but no ongoing login was found.");
            }
        }

        #endregion

        #region Device Flow Login

        /// <summary>
        /// Initiates the GitHub Device Flow login process.
        /// </summary>
        /// <remarks>
        /// This method performs the following steps:
        /// 1. Sends a request to GitHub to start a device flow.
        /// 2. Copies the user code to the clipboard and raises <see cref="OnCopyCodeToClipboard"/>.
        /// 3. Opens the verification URL in the default browser and raises <see cref="OnOpenBrowser"/>.
        /// 4. Raises <see cref="OnDeviceFlowInitiated"/> with the user code, expiration, and verification URL.
        /// 5. Starts a countdown and raises <see cref="OnCountdownStarted"/>.
        /// 6. Polls for user authorization and triggers the corresponding events based on the result.
        ///
        /// If any exception occurs, <see cref="OnDeviceFlowInitiationFailed"/> and <see cref="OnUnexpectedError"/> are invoked.
        /// </remarks>
        /// <returns>
        /// A <see cref="Task{Boolean}"/> that resolves to <c>true</c> if the device flow initiation succeeds; 
        /// otherwise, <c>false</c>.
        /// </returns>
        private async Task<bool> InitiateDeviceFlowLoginAsync()
        {
            try
            {
                // Wrap the GitHub API call with the safe helper
                deviceFlow = await Helpers.Do(async () =>
                {
                    OauthDeviceFlowRequest deviceFlowRequest = new(ClientId) { Scopes = { "repo" } };
                    return await Client.Oauth.InitiateDeviceFlow(deviceFlowRequest).ConfigureAwait(false);
                });

                if (deviceFlow == null)
                {
                    Program.Log?.Write(LogEventLevel.Warning, "Device Flow initiation could not complete due to network, rate limit, or server issues.");
                    Events.OnNetworkLost();
                    return false;
                }

                // Open verification URL
                ProcessStartInfo pci = new() { FileName = deviceFlow.VerificationUri, UseShellExecute = true };
                using (Process process = Process.Start(pci))
                {
                    if (process != null)
                    {
                        await Task.Delay(1000).ConfigureAwait(false);
                        NativeMethods.User32.SetForegroundWindow(process.Handle);
                        Program.Log?.Write(LogEventLevel.Information, $"Opened verification URL: {deviceFlow.VerificationUri}");
                    }
                }

                Events.OnDeviceFlowInitiatedResolver(deviceFlow.UserCode, deviceFlow.ExpiresIn, deviceFlow.VerificationUri);
                Events.OnCountdownStartedEvent(deviceFlow.ExpiresIn);

                using (_cancellationTokenSource = new CancellationTokenSource())
                {
                    await PollForAuthorizationAsync(_cancellationTokenSource.Token).ConfigureAwait(false);
                }

                return true;
            }
            catch (OperationCanceledException)
            {
                Program.Log?.Write(LogEventLevel.Information, "Device Flow login canceled by user.");
                Events.OnLoginCanceledEvent();
                return false;
            }
            catch (Exception ex)
            {
                Program.Log?.Write(LogEventLevel.Error, "Device Flow initiation failed.", ex);
                Events.OnDeviceFlowInitiationFailedEvent();
                Events.OnUnexpectedErrorEvent(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Polls GitHub for user authorization after initiating a Device Flow login.
        /// </summary>
        /// <remarks>
        /// This method continuously polls GitHub to check whether the user has authorized the device login.
        /// It raises events to notify the UI or calling code about the login progress and status:
        /// - <see cref="OnCountdownTick"/>: remaining seconds of the device code expiration.
        /// - <see cref="OnAuthorizationPending"/>: user has not yet authorized; poll continues.
        /// - <see cref="OnAuthorizationSuccess"/>: user has authorized; provides the authenticated GitHub user.
        /// - <see cref="OnAuthorizationFailure"/>: other authorization errors.
        /// - <see cref="OnInsufficientPermissions"/>: user denied access or bad verification code.
        /// - <see cref="OnUnexpectedError"/>: other unexpected exceptions.
        /// - <see cref="OnLoginCanceled"/>: the login process was canceled via the provided cancellation token.
        /// - <see cref="OnLoginTimedOut"/>: the device code expired before the user authorized.
        ///
        /// The method also runs a countdown task that updates <see cref="OnCountdownTick"/> every second.
        /// </remarks>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> used to cancel the polling operation prematurely.
        /// </param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous polling operation.
        /// </returns>
        public async Task PollForAuthorizationAsync(CancellationToken cancellationToken)
        {
            if (deviceFlow == null)
            {
                Program.Log?.Write(LogEventLevel.Warning, "PollForAuthorizationAsync called but deviceFlow is null.");
                Events.OnDeviceFlowNotInitiatedEvent();
                return;
            }

            OauthToken token = null;
            int remaining = deviceFlow.ExpiresIn;

            // Countdown task
            Task countdownTask = Task.Run(async () =>
            {
                while (remaining > 0 && !cancellationToken.IsCancellationRequested)
                {
                    Events.OnCountdownTickEvent(remaining);
                    await Task.Delay(1000, cancellationToken).ConfigureAwait(false);
                    remaining--;
                }

                if (!cancellationToken.IsCancellationRequested) Events.OnCountdownTickEvent(0);
            }, cancellationToken);

            try
            {
                while (remaining > 0 && !cancellationToken.IsCancellationRequested)
                {
                    try
                    {
                        // Safe GitHub call for token creation
                        token = await Helpers.Do(async () =>
                        {
                            return await Client.Oauth.CreateAccessTokenForDeviceFlow(ClientId, deviceFlow);
                        });

                        if (token == null)
                        {
                            Program.Log?.Write(LogEventLevel.Warning, "Token request failed due to network, rate limit, or server issues.");
                            await Task.Delay(deviceFlow.Interval * 1000, cancellationToken).ConfigureAwait(false);
                            continue;
                        }

                        if (!string.IsNullOrEmpty(token.AccessToken))
                        {
                            // Safe GitHub call for user info
                            Octokit.User user = await Helpers.Do(async () =>
                            {
                                Client.Credentials = new Credentials(token.AccessToken);
                                return await Client.User.Current().ConfigureAwait(false);
                            });

                            if (user != null)
                            {
                                Events.OnAuthorizationSuccessEvent(user);
                                Program.Log?.Write(LogEventLevel.Information, $"Authorization successful for {user.Login}.");
                                SaveToken(token.AccessToken);
                                return;
                            }
                        }
                    }
                    catch (AuthorizationException ex)
                    {
                        if (ex.Message.Contains("authorization_pending"))
                        {
                            Events.OnAuthorizationPendingEvent();
                            Program.Log?.Write(LogEventLevel.Information, "Authorization pending.");
                        }
                        else if (ex.Message.Contains("slow_down"))
                        {
                            Events.OnLoginRetryEvent();
                            Program.Log?.Write(LogEventLevel.Information, "GitHub requested slow_down, retrying...");
                            await Task.Delay(deviceFlow.Interval * 2000, cancellationToken).ConfigureAwait(false);
                        }
                        else if (ex.Message.Contains("bad_verification_code"))
                        {
                            Events.OnInsufficientPermissionsEvent();
                            Program.Log?.Write(LogEventLevel.Warning, "Bad verification code: insufficient permissions.");
                            return;
                        }
                        else
                        {
                            Events.OnAuthorizationFailureEvent(ex.Message);
                            Program.Log?.Write(LogEventLevel.Error, "Authorization failed.", ex);
                            return;
                        }
                    }

                    await Task.Delay(deviceFlow.Interval * 1000, cancellationToken).ConfigureAwait(false);
                }

                if (!cancellationToken.IsCancellationRequested)
                {
                    Events.OnLoginTimedOutEvent();
                    Program.Log?.Write(LogEventLevel.Warning, "Device Flow login timed out.");
                }
                else
                {
                    Events.OnLoginCanceledEvent();
                    Program.Log?.Write(LogEventLevel.Information, "Device Flow login canceled.");
                }
            }
            catch (OperationCanceledException)
            {
                Events.OnLoginCanceledEvent();
                Program.Log?.Write(LogEventLevel.Information, "Polling canceled.");
            }
            finally
            {
                try { await countdownTask.ConfigureAwait(false); } catch { /* Ignore cancellation */ }
            }
        }

        /// <summary>
        /// Signs the user out of the application.
        /// </summary>
        /// <remarks>
        /// This method performs the following steps asynchronously:
        /// 1. Cancels any ongoing Device Flow login.
        /// 2. Deletes the locally saved OAuth token.
        /// 3. Resets the GitHub client credentials to anonymous.
        /// 4. Opens the GitHub applications settings page in the default browser, allowing the user
        ///    to manually revoke access if desired.
        /// 5. Fires <see cref="OnSignedOut"/> if sign-out succeeds, or <see cref="OnSignOutFailed"/>
        ///    if an exception occurs.
        ///
        /// This method does not attempt to revoke the token on GitHub automatically; it relies on the
        /// user to revoke access manually if needed.
        /// </remarks>
        /// <returns>A <see cref="Task"/> representing the asynchronous sign-out operation.</returns>
        public async Task SignOutAsync()
        {
            if (MsgBox(Program.Localization.Strings.Messages.GitHub_SignoutMsg, System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question, Program.Localization.Strings.Messages.GitHub_SignoutMsg2) == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    Program.Log?.Write(LogEventLevel.Information, "Attempting GitHub sign out...");

                    // Check network availability
                    if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
                    {
                        Program.Log?.Write(LogEventLevel.Warning, "Network unavailable. Sign-out will continue locally.");
                    }

                    // Cancel ongoing login if any
                    if (_cancellationTokenSource != null)
                    {
                        Program.Log?.Write(LogEventLevel.Information, "Cancelling any ongoing GitHub login before sign-out...");
                        _cancellationTokenSource.Cancel();
                        _cancellationTokenSource.Dispose();
                        _cancellationTokenSource = null;
                    }

                    // Simulate async operation
                    await Task.Delay(100).ConfigureAwait(false);

                    // Delete local token
                    Program.Log?.Write(LogEventLevel.Information, "Deleting local GitHub OAuth token...");
                    DeleteToken();

                    // Reset credentials
                    Client.Credentials = Credentials.Anonymous;
                    Program.Log?.Write(LogEventLevel.Information, "GitHub client credentials reset to anonymous.");

                    // Open GitHub applications settings page for user to revoke token manually, maximized, focused and brought to top
                    ProcessStartInfo pci = new() { FileName = "https://github.com/settings/applications", UseShellExecute = true };
                    using (Process process = Process.Start(pci))
                    {
                        if (process != null)
                        {
                            // Wait a bit for the window to appear
                            await Task.Delay(1000);

                            // Bring to front using Win32 API
                            NativeMethods.User32.SetForegroundWindow(process.Handle);
                            Program.Log?.Write(LogEventLevel.Information, "Opened GitHub applications settings page in browser.");
                        }
                    }

                    // Update user info
                    User.UpdateGitHubLoginStatus(false);
                    User.GitHub = null;

                    // Fire signed out event
                    Program.Log?.Write(LogEventLevel.Information, "GitHub sign out completed successfully.");
                    Events.OnSignedOutEvent();
                }
                catch (Exception ex)
                {
                    Program.Log?.Write(LogEventLevel.Error, "GitHub sign out failed.", ex);
                    Events.OnSignOutFailedEvent(ex.Message);
                }
            }
        }

        #endregion

        #region GitHub OAuth Token Manager

        /// <summary>
        /// The target name used in Windows Credential Manager for storing the GitHub OAuth token.
        /// </summary>
        private const string Target = $"WinPaletter_GitHubToken";

        /// <summary>
        /// Saves the GitHub OAuth token securely in Windows Credential Manager using a generic credential type.
        /// </summary>
        /// <param name="token">The OAuth token string to store.</param>
        /// <exception cref="Exception">
        /// Thrown if the underlying <see cref="NativeMethods.ADVAPI.CredWrite"/> call fails.
        /// The Win32 error code can be retrieved via <see cref="Marshal.GetLastWin32Error"/>.
        /// </exception>
        /// <remarks>
        /// This method allocates unmanaged memory for the credential blob, writes it to Credential Manager, 
        /// and then frees the unmanaged memory. The token is persisted in the local machine scope.
        /// </remarks>
        public static void SaveToken(string token)
        {
            Program.Log?.Write(LogEventLevel.Information, "Saving GitHub OAuth token to Windows Credential Manager...");

            byte[] byteArray = Encoding.Unicode.GetBytes(token);
            IntPtr blob = Marshal.AllocHGlobal(byteArray.Length);
            Marshal.Copy(byteArray, 0, blob, byteArray.Length);

            NativeMethods.ADVAPI.CREDENTIAL credential = new()
            {
                TargetName = Target,
                Type = NativeMethods.ADVAPI.CRED_TYPE_GENERIC,
                Persist = NativeMethods.ADVAPI.CRED_PERSIST_LOCAL_MACHINE,
                CredentialBlobSize = byteArray.Length,
                CredentialBlob = blob,
                UserName = "GitHubToken"
            };

            try
            {
                if (!NativeMethods.ADVAPI.CredWrite(ref credential, 0))
                {
                    int error = Marshal.GetLastWin32Error();
                    Program.Log?.Write(LogEventLevel.Error, $"Failed to save GitHub token. Win32 Error Code: {error}");
                    throw new Exception("Failed to save credential. Error code: " + error);
                }

                Program.Log?.Write(LogEventLevel.Information, "GitHub OAuth token saved successfully.");
            }
            finally
            {
                Marshal.FreeHGlobal(blob);
            }
        }

        /// <summary>
        /// Loads the GitHub OAuth token from Windows Credential Manager.
        /// </summary>
        /// <returns>
        /// The OAuth token string if found; otherwise, <c>null</c> if the credential does not exist or cannot be read.
        /// </returns>
        /// <remarks>
        /// This method calls <see cref="NativeMethods.ADVAPI.CredRead"/> to retrieve the credential pointer,
        /// marshals the unmanaged data into a managed byte array, converts it to a string, and frees the unmanaged memory.
        /// </remarks>
        public static string LoadToken()
        {
            Program.Log?.Write(LogEventLevel.Information, "Attempting to load GitHub OAuth token from Windows Credential Manager...");

            if (NativeMethods.ADVAPI.CredRead(Target, NativeMethods.ADVAPI.CRED_TYPE_GENERIC, 0, out IntPtr credPtr))
            {
                try
                {
                    NativeMethods.ADVAPI.CREDENTIAL cred = (NativeMethods.ADVAPI.CREDENTIAL)Marshal.PtrToStructure(credPtr, typeof(NativeMethods.ADVAPI.CREDENTIAL));
                    byte[] blob = new byte[cred.CredentialBlobSize];
                    Marshal.Copy(cred.CredentialBlob, blob, 0, cred.CredentialBlobSize);
                    string token = Encoding.Unicode.GetString(blob);

                    Program.Log?.Write(LogEventLevel.Information, "GitHub OAuth token loaded successfully.");
                    return token;
                }
                catch (Exception ex)
                {
                    Program.Log?.Write(LogEventLevel.Error, "Error loading GitHub OAuth token.", ex);
                    return null;
                }
                finally
                {
                    NativeMethods.ADVAPI.CredFree(credPtr);
                }
            }

            Program.Log?.Write(LogEventLevel.Warning, "No GitHub OAuth token found in Windows Credential Manager.");
            return null;
        }

        /// <summary>
        /// Deletes the stored GitHub OAuth token from Windows Credential Manager.
        /// </summary>
        /// <remarks>
        /// This method calls <see cref="NativeMethods.ADVAPI.CredDelete"/> to remove the credential.
        /// If the credential does not exist, no exception is thrown.
        /// </remarks>
        public static void DeleteToken()
        {
            try
            {
                Program.Log?.Write(LogEventLevel.Information, "Attempting to delete GitHub OAuth token from Windows Credential Manager...");

                bool result = NativeMethods.ADVAPI.CredDelete(Target, NativeMethods.ADVAPI.CRED_TYPE_GENERIC, 0);

                if (result)
                    Program.Log?.Write(LogEventLevel.Information, "GitHub OAuth token deleted successfully.");
                else
                    Program.Log?.Write(LogEventLevel.Warning, "GitHub OAuth token not found or could not be deleted.");
            }
            catch (Exception ex)
            {
                Program.Log?.Write(LogEventLevel.Error, "Error occurred while deleting GitHub OAuth token.", ex);
            }
        }

        #endregion

        #region IDisposable Support
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Cancel any ongoing login
                    _cancellationTokenSource?.Cancel();
                    _cancellationTokenSource?.Dispose();
                    _cancellationTokenSource = null;
                }

                // Note: dispose unmanaged resources here

                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~LoginManager()
        {
            Dispose(false);
        }
        #endregion
    }
}