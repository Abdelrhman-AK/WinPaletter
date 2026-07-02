using Octokit;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace WinPaletter.GitHub
{
    public static class Helpers
    {
        private static DateTime? _rateLimitReset;

        public static bool IsRateLimited => _rateLimitReset.HasValue && _rateLimitReset.Value > DateTime.UtcNow;

        public static DateTime? RateLimitReset => _rateLimitReset;

        public static async Task<(int? remainingTrials, DateTime? whenWillReset)> RemainingTrials()
        {
            if (IsRateLimited) return (0, _rateLimitReset);

            try
            {
                MiscellaneousRateLimit limits = await Program.GitHub.Client.RateLimit.GetRateLimits();
                RateLimit core = limits.Resources.Core;

                if (core.Remaining <= 0)
                {
                    _rateLimitReset = core.Reset.UtcDateTime;
                    return (0, _rateLimitReset);
                }

                _rateLimitReset = null;
                return (core.Remaining, core.Reset.UtcDateTime);
            }
            catch
            {
                // Unknown state.
                // Do not assume rate limit.
                return (null, null);
            }
        }

        /// <summary>
        /// Executes a GitHub API action safely.
        /// </summary>
        public static async Task<T> Do<T>(Func<Task<T>> action)
        {
            if (!Program.IsNetworkAvailable)
            {
                Events.OnNetworkLost();
                return default;
            }

            if (IsRateLimited)
            {
                Events.OnRateLimitExceeded(_rateLimitReset.Value);
                return default;
            }

            try
            {
                T result = await action();

                if (_rateLimitReset.HasValue &&
                    _rateLimitReset.Value <= DateTime.UtcNow)
                {
                    _rateLimitReset = null;
                }

                return result;
            }
            catch (NotFoundException)
            {
                throw;
            }
            catch (RateLimitExceededException ex)
            {
                _rateLimitReset = ex.Reset.UtcDateTime;

                Events.OnRateLimitExceeded(_rateLimitReset.Value);
                return default;
            }
            catch (AbuseException)
            {
                DateTime reset = DateTime.UtcNow.AddMinutes(1);

                _rateLimitReset = reset;

                Events.OnRateLimitExceeded(reset);
                return default;
            }
            catch (AuthorizationException ex)
            {
                Events.OnAuthorizationFailureEvent(ex.Message);
                return default;
            }
            catch (ApiException ex) when (IsServerError(ex))
            {
                Events.OnServerUnavailable();
                return default;
            }
            catch (Exception ex) when (IsNetworkException(ex))
            {
                Program.Log?.Write(Serilog.Events.LogEventLevel.Warning, $"GitHub call failed due to network interruption ({ex.GetType().Name}).", ex);
                Events.OnNetworkLost();
                return default;
            }
        }

        private static bool IsServerError(ApiException ex)
        {
            if (ex == null) return false;

            int code = (int)ex.StatusCode;
            return code >= 500 && code <= 599;
        }

        /// <summary>
        /// Determines whether an exception (or any exception in its InnerException chain)
        /// represents a network connectivity failure rather than an application defect.
        /// Covers both the modern HttpClient stack (HttpRequestException wrapping
        /// SocketException/WebException) and the legacy HttpWebRequest stack
        /// (raw WebException, SocketException, and the ObjectDisposedException/
        /// InvalidOperationException race conditions thrown by ServicePoint/Connection
        /// internals when a connection is torn down mid-request).
        /// </summary>
        internal static bool IsNetworkException(Exception ex)
        {
            while (ex is not null)
            {
                if (ex is HttpRequestException
                    or WebException
                    or System.Net.Sockets.SocketException
                    or TaskCanceledException)
                {
                    return true;
                }

                if (ex is ObjectDisposedException && ex.Message.Contains("NetworkStream"))
                {
                    return true;
                }

                if (ex is ObjectDisposedException && ex.Message.Contains("Safe handle"))
                {
                    return true;
                }

                if (ex is InvalidOperationException && ex.Message.Contains("asynchronous result"))
                {
                    return true;
                }

                ex = ex.InnerException;
            }

            return false;
        }
    }
}