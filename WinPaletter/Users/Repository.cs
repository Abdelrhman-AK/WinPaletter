using Octokit;
using Serilog.Events;
using System;
using System.Threading.Tasks;

namespace WinPaletter.GitHub
{
    /// <summary>
    /// Provides high-level helper methods for interacting with GitHub repositories
    /// in the context of WinPaletter, including checking existence, forking,
    /// syncing upstream changes, and creating pull requests.
    /// </summary>
    public class Repository
    {
        /// <summary>
        /// Gets the globally configured <see cref="GitHubClient"/> instance.
        /// </summary>
        private static GitHubClient _client => Program.GitHub.Client;

        /// <summary>
        /// Gets the authenticated user login name (repository owner).
        /// </summary>
        private static string _owner => User.GitHub.Login;

        /// <summary>
        /// The default repository name for WinPaletter Store.
        /// </summary>
        private const string _repo = "WinPaletter-Store";

        /// <summary>
        /// Checks whether a GitHub repository exists for the current authenticated user.
        /// </summary>
        /// <param name="repository">The repository name to check. Defaults to WinPaletter Store repo.</param>
        /// <returns>
        /// <c>true</c> if the repository exists; otherwise <c>false</c>.
        /// </returns>
        /// <remarks>
        /// Network availability is checked before executing the request.
        /// </remarks>
        public static async Task<bool> ExistsAsync(string repository = _repo)
        {
            if (!Program.IsNetworkAvailable) return false;

            try
            {
                Octokit.Repository repo = await _client.Repository.Get(_owner, repository).ConfigureAwait(false);
                Program.Log?.Write(LogEventLevel.Information, $"Repository found: {repo.FullName}");
                return true;
            }
            catch (NotFoundException)
            {
                Program.Log?.Write(LogEventLevel.Information, $"Repository not found: {_owner}/{repository}");
                return false;
            }
            catch (Exception ex)
            {
                Program.Log?.Write(LogEventLevel.Error, $"Error checking repository {_owner}/{repository}", ex);
                return false;
            }
        }

        /// <summary>
        /// Creates a fork of the specified repository under the current authenticated user's account.
        /// </summary>
        /// <param name="repository">The name of the repository to fork.</param>
        /// <returns>
        /// The forked <see cref="Octokit.Repository"/> instance, or <c>null</c> if the operation fails.
        /// </returns>
        /// <remarks>
        /// GitHub API handles asynchronous fork creation; the returned repository
        /// may not yet be fully ready for operations.
        /// </remarks>
        public static async Task<Octokit.Repository> ForkAsync(string repository = _repo)
        {
            if (!Program.IsNetworkAvailable) return null;

            try
            {
                Octokit.Repository forked = await _client.Repository.Forks.Create(_owner, repository, new()).ConfigureAwait(false);
                Program.Log?.Write(LogEventLevel.Information, $"Forked repository: {forked.FullName}");
                return forked;
            }
            catch (Exception ex)
            {
                Program.Log?.Write(LogEventLevel.Error, $"Error forking repository {_owner}/{repository}", ex);
                return null;
            }
        }

        /// <summary>
        /// Creates a pull request from a fork (head) to the upstream repository (base).
        /// </summary>
        /// <param name="originalOwner">The upstream repository owner.</param>
        /// <param name="repoName">The repository name.</param>
        /// <param name="head">The head branch (e.g., "YourUser:main").</param>
        /// <param name="baseBranch">The base branch to merge into (e.g., "main").</param>
        /// <param name="title">PR title.</param>
        /// <param name="body">PR description.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="Octokit.ApiException">Thrown when GitHub API rejects the request.</exception>
        public static async Task CreatePullRequestAsync(string originalOwner, string repoName, string head, string baseBranch, string title, string body)
        {
            if (!Program.IsNetworkAvailable) return;

            try
            {
                NewPullRequest newPR = new(title, head, baseBranch) { Body = body };
                PullRequest pr = await _client.PullRequest.Create(originalOwner, repoName, newPR).ConfigureAwait(false);
                Program.Log?.Write(LogEventLevel.Information, $"Pull request created: {pr.HtmlUrl}");
            }
            catch (Exception ex)
            {
                Program.Log?.Write(LogEventLevel.Error, $"Error creating pull request to {originalOwner}/{repoName}", ex);
            }
        }

        /// <summary>
        /// Determines whether a forked repository branch is up-to-date with its upstream branch.
        /// </summary>
        /// <param name="originalOwner">The upstream repository owner.</param>
        /// <param name="repoName">The repository name.</param>
        /// <param name="branch">Branch name to compare. Defaults to <c>main</c>.</param>
        /// <returns>
        /// <c>true</c> if both branches share the same commit SHA; otherwise <c>false</c>.
        /// </returns>
        public static async Task<bool> IsSyncedAsync(string originalOwner, string repoName, string branch = "main")
        {
            if (!Program.IsNetworkAvailable) return false;

            try
            {
                Branch upstreamBranch = await _client.Repository.Branch.Get(originalOwner, repoName, branch).ConfigureAwait(false);
                Branch forkBranch = await _client.Repository.Branch.Get(_owner, repoName, branch).ConfigureAwait(false);

                bool synced = upstreamBranch.Commit.Sha == forkBranch.Commit.Sha;

                Program.Log?.Write(LogEventLevel.Information, $"IsSynced = {synced} | upstream={upstreamBranch.Commit.Sha} | fork={forkBranch.Commit.Sha}");

                return synced;
            }
            catch (Exception ex)
            {
                Program.Log?.Write(LogEventLevel.Error, $"Error checking sync state for {_owner}/{repoName}", ex);
                return false;
            }
        }

        /// <summary>
        /// Syncs a forked repository branch with its upstream repository using GitHub's
        /// <c>merge-upstream</c> API endpoint.
        /// </summary>
        /// <param name="originalOwner">The upstream repository owner.</param>
        /// <param name="repoName">The repository name.</param>
        /// <param name="branch">The branch to sync. Defaults to <c>main</c>.</param>
        /// <returns>
        /// <c>true</c> if the sync succeeds or the fork is already up-to-date; otherwise <c>false</c>.
        /// </returns>
        /// <remarks>
        /// This method uses a raw <see cref="GitHubClient.Connection"/> call because Octokit
        /// does not provide a wrapper for <c>merge-upstream</c>.
        /// </remarks>
        public static async Task<bool> SyncAsync(string originalOwner, string repoName, string branch = "main")
        {
            if (!Program.IsNetworkAvailable) return false;

            try
            {
                Uri endpoint = new($"repos/{_owner}/{repoName}/merge-upstream", UriKind.Relative);

                object payload = new
                {
                    branch = branch
                };

                // Raw HTTP PUT because Octokit does not yet expose merge-upstream API.
                IApiResponse<MergeUpstreamResponse> response = await _client.Connection.Put<MergeUpstreamResponse>(endpoint, payload, "application/json").ConfigureAwait(false);

                Program.Log?.Write(LogEventLevel.Information, $"Fork synced: {_owner}/{repoName} → {response.Body?.MergeCommitSha}");

                return true;
            }
            catch (ApiValidationException)
            {
                Program.Log?.Write(LogEventLevel.Information, $"Fork already up-to-date: {_owner}/{repoName}");
                return true;
            }
            catch (Exception ex)
            {
                Program.Log?.Write(LogEventLevel.Error, $"Error syncing fork {_owner}/{repoName}", ex);
                return false;
            }
        }

        /// <summary>
        /// Represents the response schema from GitHub's <c>merge-upstream</c> API.
        /// </summary>
        public class MergeUpstreamResponse
        {
            /// <summary>
            /// Gets or sets the merge strategy or result type.
            /// </summary>
            public string MergeType { get; set; }

            /// <summary>
            /// Gets or sets the SHA of the resulting merge commit.
            /// </summary>
            public string MergeCommitSha { get; set; }
        }
    }
}