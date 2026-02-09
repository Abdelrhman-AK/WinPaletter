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
        public static async Task<(int remainingTrials, DateTime whenWillReset)> RemainingTrials()
        {
            MiscellaneousRateLimit rateLimits = await Program.GitHub.Client.RateLimit.GetRateLimits();
            RateLimit coreLimit = rateLimits.Resources.Core;
            return (coreLimit.Remaining, coreLimit.Reset.UtcDateTime);
        }

        public static async Task<T> ExecuteGitHubActionSafeAsync<T>(Func<Task<T>> action)
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
            catch (HttpRequestException)
            {
                Events.OnNetworkLost();
                return default;
            }
            catch (Exception)
            {
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
