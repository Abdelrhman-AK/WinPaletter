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
            catch (HttpRequestException)
            {
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
    }
}