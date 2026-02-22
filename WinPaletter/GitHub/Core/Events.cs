using Serilog.Events;
using System;

namespace WinPaletter.GitHub
{
    public static class Events
    {
        public static event EventHandler<bool> CanPasteChanged;

        public static event EventHandler<bool> CanDoIOChanged;

        public static event EventHandler<string> Navigated;

        public static event EventHandler<string> StatusLabelChanged;
        public static event EventHandler<System.Windows.Forms.View> ViewChanged;

        public static event EventHandler GitHubAvatarUpdated;

        /// <summary>
        /// Occurs when a rate limit has been exceeded, providing the timestamp of the event.
        /// </summary>
        /// <remarks>Subscribers can use this event to handle scenarios where API or resource usage
        /// exceeds allowed limits. The event is raised with the time at which the rate limit was breached. Event
        /// handlers should avoid long-running operations to prevent blocking the event invocation thread.</remarks>
        public static event EventHandler<DateTime> RateLimitExceeded;

        /// <summary>
        /// Occurs when the network connection is lost.
        /// </summary>
        /// <remarks>Subscribers can use this event to respond to network disconnection scenarios, such as attempting to
        /// reconnect or notifying users. The event is raised whenever the network becomes unavailable. Handlers should execute
        /// quickly to avoid blocking the event invocation thread.</remarks>
        public static event EventHandler NetworkLost;

        public static void OnCanPasteChanged(bool value) => CanPasteChanged?.Invoke(null, value);
        public static void OnCanDoIOChanged(bool value) => CanDoIOChanged?.Invoke(null, value);
        public static void OnNavigated(string url) => Navigated?.Invoke(null, url);
        public static void OnStatusLabelChanged(string status) => StatusLabelChanged?.Invoke(null, status);
        public static void OnViewChanged(System.Windows.Forms.View view) => ViewChanged?.Invoke(null, view);
        public static void OnGitHubAvatarUpdated() => GitHubAvatarUpdated?.Invoke(null, new());
        public static void OnRateLimitExceeded(DateTime resetTime) => RateLimitExceeded?.Invoke(null, resetTime);
        public static void OnNetworkLost() => NetworkLost?.Invoke(null, new());

        /// <summary>
        /// Event args that have data of GitHub user login statue change event
        /// </summary>
        public class GitHubUserChangeEventArgs : EventArgs
        {
            public bool IsLoggedIn { get; set; } = false;
        }

        /// <summary>
        /// Delegate for user change event
        /// </summary>
        /// <param name="e"></param>
        public delegate void GitHubUserChangeEventHandler(GitHubUserChangeEventArgs e);

        /// <summary>
        /// Event for user change
        /// </summary>
        public static event GitHubUserChangeEventHandler GitHubUserSwitch;

        /// <summary>
        /// Void method occurs on GitHub user change event
        /// </summary>
        /// <param name="e"></param>
        public async static void OnGitHubUserSwitch(GitHubUserChangeEventArgs e)
        {
            try
            {
                Program.Log?.Write(LogEventLevel.Information, $"GitHub user switch event triggered. {nameof(e.IsLoggedIn)}: {e.IsLoggedIn}");

                if (e.IsLoggedIn)
                {
                    if (!Program.IsNetworkAvailable)
                    {
                        Program.Log?.Write(LogEventLevel.Warning, "Network unavailable. Cannot load GitHub user.");
                        return;
                    }

                    User.GitHub = await Program.GitHub.Client.User.Current().ConfigureAwait(false);
                    Program.Log?.Write(LogEventLevel.Information, $"GitHub user loaded: {User.GitHub.Login}");

                    User.GitHub_Avatar?.Dispose();
                    await User.DownloadAvatarInternalAsync().ConfigureAwait(false);
                    Program.Log?.Write(LogEventLevel.Information, "GitHub avatar has been loaded successfully.");
                }
                else
                {
                    User.GitHub = null;
                    User.GitHub_Avatar?.Dispose();
                    Program.Log?.Write(LogEventLevel.Information, "GitHub user logged out. Avatar disposed.");
                }

                GitHubUserSwitch?.Invoke(e);
            }
            catch (Exception ex)
            {
                Program.Log?.Write(LogEventLevel.Error, "Error during GitHub user switch.", ex);
            }
        }

        /// <summary>
        /// Occurs when the server becomes unavailable, allowing subscribers to respond to connectivity loss or service
        /// interruption.
        /// </summary>
        /// <remarks>Subscribers should handle this event to implement recovery strategies or notify users
        /// when the server cannot be reached. The event is raised whenever the server is detected as unavailable, which
        /// may be due to network issues, server downtime, or other connectivity failures.</remarks>
        public static event EventHandler ServerUnavailable;

        /// <summary>
        /// Raised when the device flow is initiated successfully.
        /// Provides the user code, expiration time in seconds, and the verification URL to open in a browser.
        /// </summary>
        /// <remarks>Subscribe in your UI to display the user code and verification URL to the user.</remarks>
        public static event Action<string, int, string> OnDeviceFlowInitiated;

        /// <summary>
        /// Raised when the countdown for device flow starts.
        /// Provides the total expiration time in seconds.
        /// </summary>
        /// <remarks>Subscribe in your UI to initialize a countdown progress bar or timer display.</remarks>
        public static event EventHandler<int> OnCountdownStarted;

        /// <summary>
        /// Raised periodically during the countdown for device flow.
        /// Provides the remaining seconds.
        /// </summary>
        /// <remarks>Subscribe to update countdown display or progress bar in the UI.</remarks>
        public static event EventHandler<int> OnCountdownTick;

        /// <summary>
        /// Raised when the countdown has finished.
        /// </summary>
        /// <remarks>Use this to indicate in the UI that the login code has expired.</remarks>
        public static event EventHandler OnCountdownEnded;

        /// <summary>
        /// Raised when GitHub returns "authorization_pending", indicating the user has not yet approved the request.
        /// </summary>
        /// <remarks>Use this to show a "Waiting for user approval" message in the UI.</remarks>
        public static event EventHandler OnAuthorizationPending;

        /// <summary>
        /// Raised when the user successfully authorizes the device flow.
        /// Provides the authenticated GitHub user object.
        /// </summary>
        /// <remarks>Subscribe to update the UI with user info, profile picture, or proceed to the main app.</remarks>
        public static event EventHandler<Octokit.User> OnAuthorizationSuccess;

        /// <summary>
        /// Raised when authorization fails.
        /// Provides an error message describing the failure.
        /// </summary>
        /// <remarks>Subscribe to show an error message to the user.</remarks>
        public static event EventHandler<string> OnAuthorizationFailure;

        /// <summary>
        /// Raised when initiating the device flow fails.
        /// </summary>
        /// <remarks>Use this to notify the user that the device flow could not start.</remarks>
        public static event EventHandler OnDeviceFlowInitiationFailed;

        /// <summary>
        /// Raised when no device flow has been initiated but a poll or other action was attempted.
        /// </summary>
        /// <remarks>Use this to show that the device flow was never started.</remarks>
        public static event EventHandler OnDeviceFlowNotInitiated;

        /// <summary>
        /// Raised when the user cancels the login process.
        /// </summary>
        /// <remarks>Subscribe to show cancellation confirmation in the UI.</remarks>
        public static event EventHandler OnLoginCanceled;

        /// <summary>
        /// Raised when the login attempt times out without user authorization.
        /// </summary>
        /// <remarks>Use this to notify the user that the code has expired and they need to restart login.</remarks>
        public static event EventHandler OnLoginTimedOut;

        /// <summary>
        /// Raised when a saved token is loaded from local storage.
        /// Provides the token string.
        /// </summary>
        /// <remarks>Subscribe if you want to verify the token or pre-fill some UI info.</remarks>
        public static event EventHandler<string> OnTokenLoaded;

        /// <summary>
        /// Raised when a loaded token is invalid or expired.
        /// </summary>
        /// <remarks>Use this to trigger re-login in the UI.</remarks>
        public static event EventHandler OnTokenInvalid;

        /// <summary>
        /// Raised when a local token is deleted or revoked.
        /// </summary>
        /// <remarks>Use this to clear any cached credentials from the UI.</remarks>
        public static event EventHandler OnTokenRevoked;

        /// <summary>
        /// Raised when the device flow fails due to insufficient permissions (e.g., bad verification code).
        /// </summary>
        /// <remarks>Use this to notify the user they need to adjust GitHub OAuth app permissions.</remarks>
        public static event EventHandler OnInsufficientPermissions;

        /// <summary>
        /// Raised when a network error occurs during login or token operations.
        /// Provides the error message.
        /// </summary>
        /// <remarks>Subscribe to show a network error alert in the UI.</remarks>
        public static event EventHandler<string> OnNetworkError;

        /// <summary>
        /// Raised when an unexpected error occurs during login or token operations.
        /// Provides the error message.
        /// </summary>
        /// <remarks>Subscribe to display unexpected errors to the user for troubleshooting.</remarks>
        public static event EventHandler<string> OnUnexpectedError;

        /// <summary>
        /// Raised when GitHub services are unavailable.
        /// </summary>
        /// <remarks>Subscribe to inform the user about GitHub downtime or maintenance.</remarks>
        public static event EventHandler OnServiceUnavailable;

        /// <summary>
        /// Raised when a login attempt starts (e.g., user triggers device flow login).
        /// </summary>
        /// <remarks>Use this to show a "Login in progress" indicator in the UI.</remarks>
        public static event EventHandler OnLoginInProgress;

        /// <summary>
        /// Raised when a retry is performed due to GitHub's "slow_down" response.
        /// </summary>
        /// <remarks>Subscribe to show a "Retrying..." message or spinner in the UI.</remarks>
        public static event EventHandler OnLoginRetry;

        /// <summary>
        /// Raised when the user successfully signs out.
        /// </summary>
        /// <remarks>Use this to clear UI data and return to the login screen.</remarks>
        public static event EventHandler OnSignedOut;

        /// <summary>
        /// Raised when sign out fails.
        /// Provides an error message describing the failure.
        /// </summary>
        /// <remarks>Subscribe to show a sign out error message to the user.</remarks>
        public static event EventHandler<string> OnSignOutFailed;

        public static void OnServerUnavailable() => ServerUnavailable?.Invoke(null, new());
        public static void OnDeviceFlowInitiatedResolver(string userCode, int expiresIn, string verificationUrl) => OnDeviceFlowInitiated?.Invoke(userCode, expiresIn, verificationUrl);
        public static void OnCountdownStartedEvent(int totalSeconds) => OnCountdownStarted?.Invoke(null, totalSeconds);
        public static void OnCountdownTickEvent(int remainingSeconds) => OnCountdownTick?.Invoke(null, remainingSeconds);
        public static void OnCountdownEndedEvent() => OnCountdownEnded?.Invoke(null, new());
        public static void OnAuthorizationPendingEvent() => OnAuthorizationPending?.Invoke(null, new());
        public static void OnAuthorizationSuccessEvent(Octokit.User user) => OnAuthorizationSuccess?.Invoke(null, user);
        public static void OnAuthorizationFailureEvent(string errorMessage) => OnAuthorizationFailure?.Invoke(null, errorMessage);
        public static void OnDeviceFlowInitiationFailedEvent() => OnDeviceFlowInitiationFailed?.Invoke(null, new());
        public static void OnDeviceFlowNotInitiatedEvent() => OnDeviceFlowNotInitiated?.Invoke(null, new());
        public static void OnLoginCanceledEvent() => OnLoginCanceled?.Invoke(null, new());
        public static void OnLoginTimedOutEvent() => OnLoginTimedOut?.Invoke(null, new());
        public static void OnTokenLoadedEvent(string token) => OnTokenLoaded?.Invoke(null, token);
        public static void OnTokenInvalidEvent() => OnTokenInvalid?.Invoke(null, new());
        public static void OnTokenRevokedEvent() => OnTokenRevoked?.Invoke(null, new());
        public static void OnInsufficientPermissionsEvent() => OnInsufficientPermissions?.Invoke(null, new());
        public static void OnNetworkErrorEvent(string message) => OnNetworkError?.Invoke(null, message);
        public static void OnUnexpectedErrorEvent(string message) => OnUnexpectedError?.Invoke(null, message);
        public static void OnServiceUnavailableEvent() => OnServiceUnavailable?.Invoke(null, new());
        public static void OnLoginInProgressEvent() => OnLoginInProgress?.Invoke(null, new());
        public static void OnLoginRetryEvent() => OnLoginRetry?.Invoke(null, new());
        public static void OnSignedOutEvent() => OnSignedOut?.Invoke(null, new());
        public static void OnSignOutFailedEvent(string message) => OnSignOutFailed?.Invoke(null, message);
    }
}
