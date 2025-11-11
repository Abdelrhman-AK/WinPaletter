using Octokit;
using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinPaletter
{
    public class GitHubLoginManager : IDisposable
    {
        /// <summary>
        /// Client used for GitHub API interactions.
        /// </summary>
        public GitHubClient Client { get; private set; }
        private bool _disposed = false;
        private const string ClientId = "db4a27d1c8b8ba156eb5"; // WinPaletter's GitHub OAuth App
        private static readonly string TokenFile = Path.Combine(SysPaths.appData, "github_token.dat");
        private OauthDeviceFlowResponse deviceFlow;
        private CancellationTokenSource _cancellationTokenSource;

        /// <summary>
        /// Creates a new instance of the <see cref="GitHubLoginManager"/> class.
        /// </summary>
        public GitHubLoginManager()
        {
            Client = new GitHubClient(new ProductHeaderValue(System.Windows.Forms.Application.ProductName));
        }

        #region Events

        /// <summary>
        /// Raised when the device flow is initiated successfully.
        /// Provides the user code, expiration time in seconds, and the verification URL to open in a browser.
        /// </summary>
        /// <remarks>Subscribe in your UI to display the user code and verification URL to the user.</remarks>
        public event Action<string, int, string> OnDeviceFlowInitiated;

        /// <summary>
        /// Raised when the login code has been copied to the clipboard.
        /// </summary>
        /// <remarks>Use this to notify the user that the code is ready for pasting on GitHub.</remarks>
        public event Action OnCopyCodeToClipboard;

        /// <summary>
        /// Raised when the verification URL is opened in the browser.
        /// </summary>
        /// <remarks>Use this to notify the user that the browser has been opened.</remarks>
        public event Action OnOpenBrowser;

        /// <summary>
        /// Raised when the countdown for device flow starts.
        /// Provides the total expiration time in seconds.
        /// </summary>
        /// <remarks>Subscribe in your UI to initialize a countdown progress bar or timer display.</remarks>
        public event Action<int> OnCountdownStarted;

        /// <summary>
        /// Raised periodically during the countdown for device flow.
        /// Provides the remaining seconds.
        /// </summary>
        /// <remarks>Subscribe to update countdown display or progress bar in the UI.</remarks>
        public event Action<int> OnCountdownTick;

        /// <summary>
        /// Raised when the countdown has finished.
        /// </summary>
        /// <remarks>Use this to indicate in the UI that the login code has expired.</remarks>
        public event Action OnCountdownEnded;

        /// <summary>
        /// Raised when GitHub returns "authorization_pending", indicating the user has not yet approved the request.
        /// </summary>
        /// <remarks>Use this to show a "Waiting for user approval" message in the UI.</remarks>
        public event Action OnAuthorizationPending;

        /// <summary>
        /// Raised when the user successfully authorizes the device flow.
        /// Provides the authenticated GitHub user object.
        /// </summary>
        /// <remarks>Subscribe to update the UI with user info, profile picture, or proceed to the main app.</remarks>
        public event Action<Octokit.User> OnAuthorizationSuccess;

        /// <summary>
        /// Raised when authorization fails.
        /// Provides an error message describing the failure.
        /// </summary>
        /// <remarks>Subscribe to show an error message to the user.</remarks>
        public event Action<string> OnAuthorizationFailure;

        /// <summary>
        /// Raised when initiating the device flow fails.
        /// </summary>
        /// <remarks>Use this to notify the user that the device flow could not start.</remarks>
        public event Action OnDeviceFlowInitiationFailed;

        /// <summary>
        /// Raised when no device flow has been initiated but a poll or other action was attempted.
        /// </summary>
        /// <remarks>Use this to show that the device flow was never started.</remarks>
        public event Action OnDeviceFlowNotInitiated;

        /// <summary>
        /// Raised when the user cancels the login process.
        /// </summary>
        /// <remarks>Subscribe to show cancellation confirmation in the UI.</remarks>
        public event Action OnLoginCanceled;

        /// <summary>
        /// Raised when the login attempt times out without user authorization.
        /// </summary>
        /// <remarks>Use this to notify the user that the code has expired and they need to restart login.</remarks>
        public event Action OnLoginTimedOut;

        /// <summary>
        /// Raised when a saved token is loaded from local storage.
        /// Provides the token string.
        /// </summary>
        /// <remarks>Subscribe if you want to verify the token or pre-fill some UI info.</remarks>
        public event Action<string> OnTokenLoaded;

        /// <summary>
        /// Raised when a loaded token is invalid or expired.
        /// </summary>
        /// <remarks>Use this to trigger re-login in the UI.</remarks>
        public event Action OnTokenInvalid;

        /// <summary>
        /// Raised when a local token is deleted or revoked.
        /// </summary>
        /// <remarks>Use this to clear any cached credentials from the UI.</remarks>
        public event Action OnTokenRevoked;

        /// <summary>
        /// Raised when the device flow fails due to insufficient permissions (e.g., bad verification code).
        /// </summary>
        /// <remarks>Use this to notify the user they need to adjust GitHub OAuth app permissions.</remarks>
        public event Action OnInsufficientPermissions;

        /// <summary>
        /// Raised when a network error occurs during login or token operations.
        /// Provides the error message.
        /// </summary>
        /// <remarks>Subscribe to show a network error alert in the UI.</remarks>
        public event Action<string> OnNetworkError;

        /// <summary>
        /// Raised when GitHub's rate limit is exceeded.
        /// Provides the number of seconds to wait until the limit resets.
        /// </summary>
        /// <remarks>Subscribe to show a "Rate limit exceeded" message and optionally a countdown.</remarks>
        public event Action<int> OnRateLimitExceeded;

        /// <summary>
        /// Raised when an unexpected error occurs during login or token operations.
        /// Provides the error message.
        /// </summary>
        /// <remarks>Subscribe to display unexpected errors to the user for troubleshooting.</remarks>
        public event Action<string> OnUnexpectedError;

        /// <summary>
        /// Raised when GitHub services are unavailable.
        /// </summary>
        /// <remarks>Subscribe to inform the user about GitHub downtime or maintenance.</remarks>
        public event Action OnServiceUnavailable;

        /// <summary>
        /// Raised when a login attempt starts (e.g., user triggers device flow login).
        /// </summary>
        /// <remarks>Use this to show a "Login in progress" indicator in the UI.</remarks>
        public event Action OnLoginInProgress;

        /// <summary>
        /// Raised when a retry is performed due to GitHub's "slow_down" response.
        /// </summary>
        /// <remarks>Subscribe to show a "Retrying..." message or spinner in the UI.</remarks>
        public event Action OnLoginRetry;

        /// <summary>
        /// Raised when the user successfully signs out.
        /// </summary>
        /// <remarks>Use this to clear UI data and return to the login screen.</remarks>
        public event Action OnSignedOut;

        /// <summary>
        /// Raised when sign out fails.
        /// Provides an error message describing the failure.
        /// </summary>
        /// <remarks>Subscribe to show a sign out error message to the user.</remarks>
        public event Action<string> OnSignOutFailed;

        #endregion

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
            string token = LoadToken();
            if (string.IsNullOrEmpty(token)) return false;

            Client.Credentials = new Credentials(token);
            OnTokenLoaded?.Invoke(token);

            try
            {
                var user = await Client.User.Current().ConfigureAwait(false);
                return true;
            }
            catch (AuthorizationException)
            {
                OnTokenInvalid?.Invoke();
                DeleteToken();
                return false;
            }
            catch (Exception ex)
            {
                OnUnexpectedError?.Invoke(ex.Message);
                return false;
            }
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
            OnLoginInProgress?.Invoke();
            await InitiateDeviceFlowLoginAsync().ConfigureAwait(false);
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
            _cancellationTokenSource?.Cancel();
            OnLoginCanceled?.Invoke();
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
                var deviceFlowRequest = new OauthDeviceFlowRequest(ClientId)
                {
                    Scopes = { "repo" }
                };

                deviceFlow = await Client.Oauth.InitiateDeviceFlow(deviceFlowRequest).ConfigureAwait(false);

                // Copy code to clipboard
                Clipboard.SetText(deviceFlow.UserCode);
                OnCopyCodeToClipboard?.Invoke();

                // Open verification URL, maximized, focused and brought to top
                ProcessStartInfo pci = new() { FileName = deviceFlow.VerificationUri, UseShellExecute = true };
                Process process = Process.Start(pci);

                if (process != null)
                {
                    // Wait a bit for the window to appear
                    await Task.Delay(1000).ConfigureAwait(false);

                    // Bring to front using Win32 API
                    NativeMethods.User32.SetForegroundWindow(process.Handle);
                }

                OnOpenBrowser?.Invoke();

                OnDeviceFlowInitiated?.Invoke(deviceFlow.UserCode, deviceFlow.ExpiresIn, deviceFlow.VerificationUri);
                OnCountdownStarted?.Invoke(deviceFlow.ExpiresIn);

                _cancellationTokenSource = new CancellationTokenSource();
                await PollForAuthorizationAsync(_cancellationTokenSource.Token).ConfigureAwait(false);

                return true;
            }
            catch (Exception ex)
            {
                OnDeviceFlowInitiationFailed?.Invoke();
                OnUnexpectedError?.Invoke(ex.Message);
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
                OnDeviceFlowNotInitiated?.Invoke();
                return;
            }

            OauthToken token = null;
            int remaining = deviceFlow.ExpiresIn;

            // Start countdown task
            var countdownTask = Task.Run(async () =>
            {
                while (remaining > 0 && !cancellationToken.IsCancellationRequested)
                {
                    OnCountdownTick?.Invoke(remaining);
                    await Task.Delay(1000, cancellationToken).ConfigureAwait(false);
                    remaining--;
                }
                OnCountdownTick?.Invoke(0);
            }, cancellationToken);

            // Polling loop
            while (remaining > 0 && !cancellationToken.IsCancellationRequested)
            {
                try
                {
                    token = await Client.Oauth.CreateAccessTokenForDeviceFlow(ClientId, deviceFlow).ConfigureAwait(false);

                    if (!string.IsNullOrEmpty(token.AccessToken))
                    {
                        Client.Credentials = new Credentials(token.AccessToken);
                        Octokit.User user = await Client.User.Current().ConfigureAwait(false);
                        OnAuthorizationSuccess?.Invoke(user);

                        SaveToken(token.AccessToken);
                        return;
                    }
                }
                catch (AuthorizationException ex)
                {
                    if (ex.Message.Contains("authorization_pending")) OnAuthorizationPending?.Invoke();
                    else if (ex.Message.Contains("slow_down")) await Task.Delay(deviceFlow.Interval * 2000, cancellationToken).ConfigureAwait(false);
                    else if (ex.Message.Contains("bad_verification_code"))
                    {
                        OnInsufficientPermissions?.Invoke();
                        return;
                    }
                    else
                    {
                        OnAuthorizationFailure?.Invoke(ex.Message);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    OnUnexpectedError?.Invoke(ex.Message);
                    return;
                }

                // Wait the GitHub interval before next poll
                await Task.Delay(deviceFlow.Interval * 1000, cancellationToken).ConfigureAwait(false);
            }

            if (cancellationToken.IsCancellationRequested) OnLoginCanceled?.Invoke();
            else OnLoginTimedOut?.Invoke();
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
            try
            {
                // Cancel ongoing login if any
                _cancellationTokenSource?.Cancel();

                // Simulate async operation (optional, e.g., if you later want server calls)
                await Task.Delay(100).ConfigureAwait(false);

                // Delete local token
                DeleteToken();

                // Reset credentials
                Client.Credentials = Credentials.Anonymous;

                // Open GitHub applications settings page for user to revoke token manually, maximized, focused and brought to top
                ProcessStartInfo pci = new() { FileName = "https://github.com/settings/applications", UseShellExecute = true };
                Process process = Process.Start(pci);

                if (process != null)
                {
                    // Wait a bit for the window to appear
                    await Task.Delay(1000).ConfigureAwait(false);

                    // Bring to front using Win32 API
                    NativeMethods.User32.SetForegroundWindow(process.Handle);
                }

                // Fire signed out event
                OnSignedOut?.Invoke();
            }
            catch (Exception ex)
            {
                OnSignOutFailed?.Invoke(ex.Message);
            }
        }

        #endregion

        #region Token Storage (DPAPI)

        /// <summary>
        /// Saves the GitHub OAuth token securely using DPAPI (CurrentUser scope).
        /// </summary>
        /// <param name="token">The OAuth token to save.</param>
        /// <remarks>
        /// The token is encrypted and written to a file located at <see cref="TokenFile"/>.
        /// If an exception occurs during saving, <see cref="OnUnexpectedError"/> is raised with the error message.
        /// </remarks>
        private void SaveToken(string token)
        {
            try
            {
                var bytes = Encoding.UTF8.GetBytes(token);
                var encrypted = ProtectedData.Protect(bytes, null, DataProtectionScope.CurrentUser);

                Directory.CreateDirectory(Path.GetDirectoryName(TokenFile));
                File.WriteAllBytes(TokenFile, encrypted);
            }
            catch (Exception ex)
            {
                OnUnexpectedError?.Invoke(ex.Message);
            }
        }

        /// <summary>
        /// Loads the GitHub OAuth token from secure storage.
        /// </summary>
        /// <returns>The decrypted OAuth token, or <c>null</c> if the token file does not exist or cannot be decrypted.</returns>
        /// <remarks>
        /// If loading or decryption fails, the method returns <c>null</c> without throwing an exception.
        /// </remarks>
        private string LoadToken()
        {
            if (!File.Exists(TokenFile)) return null;

            try
            {
                var encrypted = File.ReadAllBytes(TokenFile);
                var decrypted = ProtectedData.Unprotect(encrypted, null, DataProtectionScope.CurrentUser);
                return Encoding.UTF8.GetString(decrypted);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Deletes the locally saved GitHub OAuth token file.
        /// </summary>
        /// <remarks>
        /// After deletion, <see cref="OnTokenRevoked"/> is raised. If the file does not exist, no action is taken.
        /// </remarks>
        public void DeleteToken()
        {
            if (File.Exists(TokenFile)) File.Delete(TokenFile);
            OnTokenRevoked?.Invoke();
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

        ~GitHubLoginManager()
        {
            Dispose(false);
        }
        #endregion
    }
}