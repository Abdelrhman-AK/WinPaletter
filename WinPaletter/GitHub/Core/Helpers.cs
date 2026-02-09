using Octokit;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;

namespace WinPaletter.GitHub
{
    public static class Helpers
    {
        // Cached rate limit to avoid multiple API calls
        private static (int remaining, DateTime reset) _cachedRateLimit;
        private static DateTime _lastRateLimitCheck;

        public static async Task<(int remainingTrials, DateTime whenWillReset)> RemainingTrials()
        {
            // Use cached rate limit if checked in last 20 seconds
            if ((DateTime.UtcNow - _lastRateLimitCheck).TotalSeconds < 20)
                return _cachedRateLimit;

            try
            {
                MiscellaneousRateLimit rateLimits = await Program.GitHub.Client.RateLimit.GetRateLimits();
                RateLimit coreLimit = rateLimits.Resources.Core;
                _cachedRateLimit = (coreLimit.Remaining, coreLimit.Reset.UtcDateTime);
                _lastRateLimitCheck = DateTime.UtcNow;
                return _cachedRateLimit;
            }
            catch
            {
                // On failure, assume 0 remaining to prevent API abuse
                return (0, DateTime.UtcNow.AddMinutes(1));
            }
        }

        /// <summary>
        /// Executes a GitHub API action safely, handling network issues, rate limits, and server errors.
        /// </summary>
        public static async Task<T> Do<T>(Func<Task<T>> action)
        {
            if (!Program.IsNetworkAvailable)
            {
                Events.OnNetworkLost();
                return default;
            }

            var (remaining, reset) = await RemainingTrials();
            if (remaining <= 0)
            {
                Events.OnRateLimitExceeded(reset);
                return default;
            }

            try
            {
                return await action();
            }
            catch (Octokit.ApiException ex) when (IsRateLimitException(ex))
            {
                Events.OnRateLimitExceeded(DateTime.UtcNow.AddMinutes(1));
                return default;
            }
            catch (Octokit.ApiException ex) when (IsServerError(ex))
            {
                Events.OnServerUnavailable();
                return default;
            }
            catch (Octokit.NotFoundException)
            {
                // Resource missing → treat as null
                return default;
            }
            catch (Octokit.AuthorizationException ex)
            {
                Events.OnAuthorizationFailureEvent(ex.Message);
                return default;
            }
            catch (HttpRequestException)
            {
                Events.OnNetworkLost();
                return default;
            }
            catch (Exception)
            {
                // Only rethrow truly unexpected exceptions
                throw;
            }
        }

        private static bool IsRateLimitException(ApiException ex)
        {
            if (ex == null) return false;
            if (ex.StatusCode != HttpStatusCode.Forbidden) return false;
            string msg = ex.ApiError?.Message ?? ex.Message;
            if (string.IsNullOrEmpty(msg)) return false;
            string[] keywords = { "rate limit", "throttled", "abuse" };
            return keywords.Any(k => msg.IndexOf(k, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        private static bool IsServerError(ApiException ex)
        {
            if (ex == null) return false;
            return ex.StatusCode == HttpStatusCode.BadGateway || // 502
                   ex.StatusCode == HttpStatusCode.ServiceUnavailable; // 503
        }
    }
}
