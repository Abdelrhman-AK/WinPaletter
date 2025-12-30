using Octokit;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
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
        /// Original owner of WinPaletter Themes repository
        /// </summary>
        public const string originalOwner = "Abdelrhman-AK";

        /// <summary>
        /// The default repository name for WinPaletter Store.
        /// </summary>
        public const string repositoryName = "WinPaletter-Store";

        /// <summary>
        /// Gets the branch name of the GitHub repository being accessed.
        /// </summary>
        public static string branch = "main";

        /// <summary>
        /// Lists all branches in the repository.
        /// </summary>
        /// <returns>A list of branch names.</returns>
        public static async Task<List<string>> GetBranchesAsync()
        {
            IReadOnlyList<Branch> branches = await Program.GitHub.Client.Repository.Branch.GetAll(_owner, repositoryName);
            return [.. branches.Select(b => b.Name)];
        }

        /// <summary>
        /// Creates a new branch from a base branch.
        /// </summary>
        /// <param name="newBranchName">Name of the new branch.</param>
        /// <param name="baseBranchName">Base branch to branch from (default is current _branch).</param>
        /// <returns>True if created successfully, false otherwise.</returns>
        public static async Task<bool> CreateBranchAsync(string newBranchName, string baseBranchName = "main")
        {
            try
            {
                // Get the reference for the base branch
                var baseRef = await Program.GitHub.Client.Git.Reference.Get(_owner, repositoryName, $"heads/{baseBranchName}");

                // Create the new branch
                var newRef = new NewReference($"refs/heads/{newBranchName}", baseRef.Object.Sha);
                await Program.GitHub.Client.Git.Reference.Create(_owner, repositoryName, newRef);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Deletes a branch.
        /// </summary>
        /// <param name="branchName">The name of the branch to delete.</param>
        /// <returns>True if deleted successfully, false otherwise.</returns>
        public static async Task<bool> DeleteBranchAsync(string branchName)
        {
            try
            {
                await Program.GitHub.Client.Git.Reference.Delete(_owner, repositoryName, $"heads/{branchName}");
                return true;
            }
            catch
            {
                return false;
            }
        }

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
        public static async Task<bool> ExistsAsync(string repository = repositoryName)
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
        public static async Task<Octokit.Repository> ForkAsync(string repository = repositoryName)
        {
            if (!Program.IsNetworkAvailable) return null;

            try
            {
                Octokit.Repository forked = await _client.Repository.Forks.Create(originalOwner, repository, new()).ConfigureAwait(false);
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
        public static async Task CreatePullRequestAsync(string title, string body)
        {
            if (!Program.IsNetworkAvailable) return;

            try
            {
                NewPullRequest newPR = new(title, $"{_owner}:{branch}", "main") { Body = body };
                PullRequest pr = await _client.PullRequest.Create(originalOwner, repositoryName, newPR).ConfigureAwait(false);
                Program.Log?.Write(LogEventLevel.Information, $"Pull request created: {pr.HtmlUrl}");
            }
            catch (Exception ex)
            {
                Program.Log?.Write(LogEventLevel.Error, $"Error creating pull request to {originalOwner}/{repositoryName}", ex);
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
        public static async Task<bool> IsSyncedAsync(string originalOwner = originalOwner, string repoName = repositoryName, string branch = "main")
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
        public static async Task<bool> SyncAsync(string originalOwner = originalOwner, string repoName = repositoryName, string branch = "main")
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

        /// <summary>
        /// Creates a commit on the current branch in the forked repository.
        /// </summary>
        /// <param name="message">Commit message.</param>
        /// <param name="changes">
        /// Dictionary of file paths and their content.
        /// Use null content to delete a file.
        /// </param>
        /// <returns>True if commit succeeded.</returns>
        public static async Task<bool> CommitAsync(string message, Dictionary<string, string> changes)
        {
            if (!Program.IsNetworkAvailable) return false;
            if (changes == null || changes.Count == 0) return false;

            try
            {
                // Get current branch reference
                var branchRef = await _client.Git.Reference.Get(
                    _owner,
                    repositoryName,
                    $"heads/{branch}"
                ).ConfigureAwait(false);

                // Get latest commit
                var commit = await _client.Git.Commit.Get(
                    _owner,
                    repositoryName,
                    branchRef.Object.Sha
                ).ConfigureAwait(false);

                // Create blobs
                var treeItems = new List<NewTreeItem>();

                foreach (var kv in changes)
                {
                    if (kv.Value == null)
                    {
                        treeItems.Add(new NewTreeItem
                        {
                            Path = kv.Key,
                            Mode = "100644",
                            Type = TreeType.Blob,
                            Sha = null
                        });
                        continue;
                    }

                    var blob = await _client.Git.Blob.Create(
                        _owner,
                        repositoryName,
                        new NewBlob
                        {
                            Content = kv.Value,
                            Encoding = EncodingType.Utf8
                        }
                    ).ConfigureAwait(false);

                    treeItems.Add(new NewTreeItem
                    {
                        Path = kv.Key,
                        Mode = "100644",
                        Type = TreeType.Blob,
                        Sha = blob.Sha
                    });
                }

                // Create tree
                var newTree = new NewTree
                {
                    BaseTree = commit.Tree.Sha
                };

                foreach (var item in treeItems)
                {
                    newTree.Tree.Add(item);
                }

                var createdTree = await _client.Git.Tree.Create(
                    _owner,
                    repositoryName,
                    newTree
                ).ConfigureAwait(false);

                // Create commit
                var newCommit = await _client.Git.Commit.Create(
                    _owner,
                    repositoryName,
                    new NewCommit(
                        message,
                        createdTree.Sha,
                        new[] { commit.Sha }
                    )
                ).ConfigureAwait(false);

                // Update branch ref
                await _client.Git.Reference.Update(
                    _owner,
                    repositoryName,
                    $"heads/{branch}",
                    new ReferenceUpdate(newCommit.Sha)
                ).ConfigureAwait(false);

                Program.Log?.Write(LogEventLevel.Information, $"Commit created on {branch}: {newCommit.Sha}");
                return true;
            }
            catch (Exception ex)
            {
                Program.Log?.Write(LogEventLevel.Error, "Commit failed", ex);
                return false;
            }
        }

        /// <summary>
        /// Checks whether a pull request can be created for the current branch.
        /// </summary>
        /// <returns>True if PR creation is valid.</returns>
        public static async Task<bool> CanCreatePullRequestAsync()
        {
            if (!Program.IsNetworkAvailable) return false;

            if (string.IsNullOrWhiteSpace(branch) ||
                branch.Equals("main", StringComparison.OrdinalIgnoreCase))
            {
                Program.Log?.Write(LogEventLevel.Error, "Invalid branch for PR.");
                return false;
            }

            try
            {
                // Branch must exist on fork
                await _client.Git.Reference.Get(
                    _owner,
                    repositoryName,
                    $"heads/{branch}"
                ).ConfigureAwait(false);

                // Must have commits
                var compare = await _client.Repository.Commit.Compare(
                    originalOwner,
                    repositoryName,
                    "main",
                    $"{_owner}:{branch}"
                ).ConfigureAwait(false);

                if (compare.TotalCommits == 0)
                {
                    Program.Log?.Write(LogEventLevel.Warning, "No commits to merge.");
                    return false;
                }

                // No duplicate PR
                var prs = await _client.PullRequest.GetAllForRepository(
                    originalOwner,
                    repositoryName,
                    new PullRequestRequest
                    {
                        State = ItemStateFilter.Open,
                        Head = $"{_owner}:{branch}",
                        Base = "main"
                    }
                ).ConfigureAwait(false);

                if (prs.Any())
                {
                    Program.Log?.Write(LogEventLevel.Warning, "Pull request already exists.");
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                Program.Log?.Write(LogEventLevel.Error, "PR validation check failed", ex);
                return false;
            }
        }
    }
}