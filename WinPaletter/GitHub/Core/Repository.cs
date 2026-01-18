using Octokit;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinPaletter.GitHub
{
    /// <summary>
    /// Provides high-level helper methods for interacting with GitHub repositories
    /// in the context of WinPaletter, including checking existence, forking,
    /// syncing upstream changes, and creating pull requests.
    /// </summary>
    public partial class Repository
    {
        /// <summary>
        /// Gets the globally configured <see cref="GitHubClient"/> instance.
        /// </summary>
        private static GitHubClient _client => Program.GitHub.Client;

        /// <summary>
        /// Gets the authenticated user login name (repository owner).
        /// </summary>
        public static string Owner => User.GitHub.Login;

        /// <summary>
        /// Original owner of WinPaletter Themes repository
        /// </summary>
        public static string OriginalOwner { get; } = "Abdelrhman-AK";

        /// <summary>
        /// The default repository name for WinPaletter Store.
        /// </summary>
        public static string Name { get; } = "WinPaletter-Store";

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
        public static async Task<bool> ExistsAsync(string repository = null)
        {
            if (!Program.IsNetworkAvailable) return false;

            try
            {
                Octokit.Repository repo = await _client.Repository.Get(Owner, repository).ConfigureAwait(false);
                Program.Log?.Write(LogEventLevel.Information, $"Repository found: {repo.FullName}");
                return true;
            }
            catch (NotFoundException)
            {
                Program.Log?.Write(LogEventLevel.Information, $"Repository not found: {Owner}/{repository}");
                return false;
            }
            catch (Exception ex)
            {
                Program.Log?.Write(LogEventLevel.Error, $"Error checking repository {Owner}/{repository}", ex);
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
        public static async Task<Octokit.Repository> ForkAsync(string repository = null)
        {
            if (!Program.IsNetworkAvailable) return null;

            try
            {
                Octokit.Repository forked = await _client.Repository.Forks.Create(OriginalOwner, repository, new()).ConfigureAwait(false);
                Program.Log?.Write(LogEventLevel.Information, $"Forked repository: {forked.FullName}");
                return forked;
            }
            catch (Exception ex)
            {
                Program.Log?.Write(LogEventLevel.Error, $"Error forking repository {Owner}/{repository}", ex);
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
                NewPullRequest newPR = new(title, $"{Owner}:{Branch.Name}", "main") { Body = body };
                PullRequest pr = await _client.PullRequest.Create(OriginalOwner, Name, newPR).ConfigureAwait(false);
                Program.Log?.Write(LogEventLevel.Information, $"Pull request created: {pr.HtmlUrl}");
            }
            catch (Exception ex)
            {
                Program.Log?.Write(LogEventLevel.Error, $"Error creating pull request to {OriginalOwner}/{Name}", ex);
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
                    Owner,
                    Name,
                    $"heads/{Branch.Name}"
                ).ConfigureAwait(false);

                // Get latest commit
                var commit = await _client.Git.Commit.Get(
                    Owner,
                    Name,
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
                        Owner,
                        Name,
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
                    Owner,
                    Name,
                    newTree
                ).ConfigureAwait(false);

                // Create commit
                var newCommit = await _client.Git.Commit.Create(
                    Owner,
                    Name,
                    new NewCommit(
                        message,
                        createdTree.Sha,
                        new[] { commit.Sha }
                    )
                ).ConfigureAwait(false);

                // Update branch ref
                await _client.Git.Reference.Update(
                    Owner,
                    Name,
                    $"heads/{Branch.Name}",
                    new ReferenceUpdate(newCommit.Sha)
                ).ConfigureAwait(false);

                Program.Log?.Write(LogEventLevel.Information, $"Commit created on {Branch.Name}: {newCommit.Sha}");
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

            if (string.IsNullOrWhiteSpace(Branch.Name) || Branch.Name.Equals("main", StringComparison.OrdinalIgnoreCase))
            {
                Program.Log?.Write(LogEventLevel.Error, "Invalid branch for PR.");
                return false;
            }

            try
            {
                // Branch must exist on fork
                await _client.Git.Reference.Get(
                    Owner,
                    Name,
                    $"heads/{Branch.Name}"
                ).ConfigureAwait(false);

                // Must have commits
                var compare = await _client.Repository.Commit.Compare(
                    OriginalOwner,
                    Name,
                    "main",
                    $"{Owner}:{Branch.Name}"
                ).ConfigureAwait(false);

                if (compare.TotalCommits == 0)
                {
                    Program.Log?.Write(LogEventLevel.Warning, "No commits to merge.");
                    return false;
                }

                // No duplicate PR
                var prs = await _client.PullRequest.GetAllForRepository(
                    OriginalOwner,
                    Name,
                    new PullRequestRequest
                    {
                        State = ItemStateFilter.Open,
                        Head = $"{Owner}:{Branch.Name}",
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