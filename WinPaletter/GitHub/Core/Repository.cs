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
        public static async Task<List<Branch>> GetBranchesAsync()
        {
            IReadOnlyList<Branch> branches = await Program.GitHub.Client.Repository.Branch.GetAll(_owner, repositoryName);
            return [.. branches];
        }

        /// <summary>
        /// Creates a new branch from a base branch and returns the created branch.
        /// </summary>
        /// <param name="newBranchName">Name of the new branch.</param>
        /// <param name="baseBranchName">Base branch to branch from (default is current _branch).</param>
        /// <returns>The created <see cref="Branch"/> if successful, null otherwise.</returns>
        public static async Task<Branch> CreateBranchAsync(string newBranchName, string baseBranchName = "main")
        {
            try
            {
                // Get the reference for the base branch
                var baseRef = await Program.GitHub.Client.Git.Reference.Get(_owner, repositoryName, $"heads/{baseBranchName}");

                // Create the new branch reference
                var newRef = new NewReference($"refs/heads/{newBranchName}", baseRef.Object.Sha);
                await Program.GitHub.Client.Git.Reference.Create(_owner, repositoryName, newRef);

                // Fetch and return the newly created branch
                Branch createdBranch = await Program.GitHub.Client.Repository.Branch.Get(_owner, repositoryName, newBranchName);
                return createdBranch;
            }
            catch
            {
                return null;
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

        public static async Task<bool> IsUpdatedAsync(Branch branch, string originalBranch = "main")
        {
            if (!Program.IsNetworkAvailable) return false;

            try
            {
                // Use provided branch or fetch by name
                Branch forkBranch = branch ?? await _client.Repository.Branch.Get(_owner, repositoryName, branch.Name).ConfigureAwait(false);
                if (forkBranch?.Commit == null)
                {
                    Program.Log?.Write(LogEventLevel.Warning, $"Source branch '{branch.Name}' not found in {_owner}/{repositoryName}.");
                    return false;
                }

                // Try to get target branch
                Branch upstreamBranch = null;
                try
                {
                    upstreamBranch = await _client.Repository.Branch.Get(originalOwner, repositoryName, originalBranch).ConfigureAwait(false);
                }
                catch (Octokit.NotFoundException)
                {
                    Program.Log?.Write(LogEventLevel.Information, $"Target branch '{originalBranch}' not found in {originalOwner}/{repositoryName}. Treating source branch as updated.");
                    return true; // Treat as updated if target branch does not exist
                }

                // Compare (base = upstream, head = fork)
                CompareResult compare = await _client.Repository.Commit.Compare(originalOwner, repositoryName, upstreamBranch.Commit.Sha, forkBranch.Commit.Sha);

                bool updated = compare.BehindBy == 0;

                Program.Log?.Write(LogEventLevel.Information, $"IsUpdated({branch.Name}) = {updated} | AheadBy={compare.AheadBy} | BehindBy={compare.BehindBy}");
                return updated;
            }
            catch (Exception ex)
            {
                Program.Log?.Write(LogEventLevel.Error, $"Error checking update state for {_owner}/{repositoryName}", ex);
                return false;
            }
        }

        public static async Task<bool> IsUpdatedAsync(string branch, string originalBranch = "main")
        {
            if (!Program.IsNetworkAvailable) return false;

            try
            {
                // Get fork/source branch
                Branch forkBranch = await _client.Repository.Branch.Get(_owner, repositoryName, branch).ConfigureAwait(false);
                if (forkBranch?.Commit == null)
                {
                    Program.Log?.Write(LogEventLevel.Warning, $"Source branch '{branch}' not found in {_owner}/{repositoryName}.");
                    return false;
                }

                // Try to get target branch
                Branch upstreamBranch = null;
                try
                {
                    upstreamBranch = await _client.Repository.Branch.Get(originalOwner, repositoryName, originalBranch).ConfigureAwait(false);
                }
                catch (Octokit.NotFoundException)
                {
                    Program.Log?.Write(LogEventLevel.Information, $"Target branch '{originalBranch}' not found in {originalOwner}/{repositoryName}. Treating source branch as updated.");
                    return true; // Treat as updated if target branch does not exist
                }

                // Correct Compare order: base = upstream, head = fork
                CompareResult compare = await _client.Repository.Commit.Compare(originalOwner, repositoryName, upstreamBranch.Commit.Sha, forkBranch.Commit.Sha);

                // If BehindBy == 0, source branch is up-to-date (may be equal or ahead)
                bool updated = compare.BehindBy == 0;

                Program.Log?.Write(LogEventLevel.Information, $"IsUpdated = {updated} | AheadBy={compare.AheadBy} | BehindBy={compare.BehindBy}");
                return updated;
            }
            catch (Exception ex)
            {
                Program.Log?.Write(LogEventLevel.Error, $"Error checking update state for {_owner}/{repositoryName}", ex);
                return false;
            }
        }

        /// <summary>
        /// Syncs a branch in a forked repository with a branch in the upstream repository.
        /// </summary>
        /// <param name="forkBranch">Branch in the fork to sync.</param>
        /// <param name="upstreamBranch">Branch in the upstream repo to sync from.</param>
        /// <returns>True if sync succeeds or already up-to-date; false otherwise.</returns>
        public static async Task<bool> SyncBranchAsync(string forkBranch = "main", string upstreamBranch = "main")
        {
            if (!Program.IsNetworkAvailable) return false;

            try
            {
                // Get upstream branch
                var upstream = await Program.GitHub.Client.Repository.Branch.Get(originalOwner, repositoryName, upstreamBranch);
                if (upstream?.Commit == null) throw new Exception($"Upstream branch '{upstreamBranch}' not found.");

                // Get fork branch
                var fork = await Program.GitHub.Client.Repository.Branch.Get(FileSystem._owner, repositoryName, forkBranch);
                if (fork?.Commit == null) throw new Exception($"Fork branch '{forkBranch}' not found.");

                // Already up-to-date?
                if (fork.Commit.Sha == upstream.Commit.Sha)
                {
                    Program.Log?.Write(LogEventLevel.Information, $"Branch '{forkBranch}' is already up-to-date with '{originalOwner}/{upstreamBranch}'.");
                    return true;
                }

                // Try merge-upstream if branch names match
                if (forkBranch == upstreamBranch)
                {
                    try
                    {
                        Uri endpoint = new($"repos/{FileSystem._owner}/{repositoryName}/merge-upstream", UriKind.Relative);
                        object payload = new { branch = forkBranch };
                        await Program.GitHub.Client.Connection.Put<object>(endpoint, payload, "application/json");
                        Program.Log?.Write(LogEventLevel.Information, $"merge-upstream: {FileSystem._owner}/{forkBranch} synced with {originalOwner}/{upstreamBranch}.");
                        return true;
                    }
                    catch (Octokit.ApiValidationException)
                    {
                        Program.Log?.Write(LogEventLevel.Information, $"merge-upstream: {FileSystem._owner}/{forkBranch} already up-to-date.");
                        return true;
                    }
                    catch (Octokit.NotFoundException)
                    {
                        Program.Log?.Write(LogEventLevel.Warning, $"merge-upstream not available for {FileSystem._owner}/{forkBranch}, falling back to Merge API.");
                    }
                }

                // Fallback: Merge API for cross-branch or different branch names
                var mergeRequest = new Octokit.NewMerge(forkBranch, $"{originalOwner}:{upstreamBranch}")
                {
                    CommitMessage = $"Merge {originalOwner}/{upstreamBranch} into {forkBranch}"
                };

                var mergeResult = await Program.GitHub.Client.Repository.Merging.Create(FileSystem._owner, repositoryName, mergeRequest);
                Program.Log?.Write(LogEventLevel.Information, $"Merged {originalOwner}/{upstreamBranch} into {FileSystem._owner}/{forkBranch}: {mergeResult.Sha}");
                return true;
            }
            catch (Octokit.ApiException ex) when (ex.HttpResponse?.StatusCode == System.Net.HttpStatusCode.Conflict)
            {
                Program.Log?.Write(LogEventLevel.Warning, $"Merge conflict when syncing {FileSystem._owner}/{forkBranch} with {originalOwner}/{upstreamBranch}");
                return false;
            }
            catch (Exception ex)
            {
                Program.Log?.Write(LogEventLevel.Error, $"Error syncing {FileSystem._owner}/{forkBranch} with {originalOwner}/{upstreamBranch}", ex);
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