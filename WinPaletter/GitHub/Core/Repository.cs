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
    public partial class Repository
    {
        /// <summary>
        /// Gets the globally configured <see cref="GitHubClient"/> instance.
        /// </summary>
        private static GitHubClient Client => Program.GitHub.Client;

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
            try
            {
                Octokit.Repository repo = await Helpers.Do(() =>
                {
                    return Client.Repository.Get(Owner, repository);
                });

                // If rate-limited, helper returns default => null
                if (repo == null) return false;

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
            try
            {
                Octokit.Repository forked = await Helpers.Do(() =>
                {
                    return Client.Repository.Forks.Create(OriginalOwner, repository, new());
                });

                if (forked == null) return null; // rate-limited

                Program.Log?.Write(LogEventLevel.Information, $"Forked repository: {forked.FullName}");
                return forked;
            }
            catch (NotFoundException)
            {
                Program.Log?.Write(LogEventLevel.Warning, $"Original repository not found: {OriginalOwner}/{repository}");
                return null;
            }
            catch (Exception ex)
            {
                Program.Log?.Write(LogEventLevel.Error, $"Error forking repository {OriginalOwner}/{repository}", ex);
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
        public static async Task<PullRequest> CreatePullRequestAsync(string title, string body)
        {
            try
            {
                NewPullRequest newPR = new(title, $"{Owner}:{Branch.Name}", "main") { Body = body };

                PullRequest pr = await Helpers.Do(() =>
                {
                    return Client.PullRequest.Create(OriginalOwner, Name, newPR);
                });

                if (pr == null) return null; // rate-limited

                Program.Log?.Write(LogEventLevel.Information, $"Pull request created: {pr.HtmlUrl}");
                return pr;
            }
            catch (NotFoundException)
            {
                Program.Log?.Write(LogEventLevel.Warning, $"Repository not found: {OriginalOwner}/{Name}");
                return null;
            }
            catch (Exception ex)
            {
                Program.Log?.Write(LogEventLevel.Error, $"Error creating pull request to {OriginalOwner}/{Name}", ex);
                return null;
            }
        }

        /// <summary>
        /// Checks whether a pull request can be created for the current branch.
        /// </summary>
        /// <returns>True if PR creation is valid.</returns>
        public static async Task<bool> CanCreatePullRequestAsync()
        {
            if (string.IsNullOrWhiteSpace(Branch.Name) || Branch.Name.Equals("main", StringComparison.OrdinalIgnoreCase))
            {
                Program.Log?.Write(LogEventLevel.Error, "Invalid branch for PR.");
                return false;
            }

            try
            {
                // Branch must exist on fork
                Reference reference = await Helpers.Do(() => Client.Git.Reference.Get(Owner, Name, $"heads/{Branch.Name}"));

                if (reference == null) return false; // rate-limited

                // Must have commits
                CompareResult compare = await Helpers.Do(() => Client.Repository.Commit.Compare(OriginalOwner, Name, "main", $"{Owner}:{Branch.Name}"));

                if (compare == null) return false; // rate-limited

                if (compare.TotalCommits == 0)
                {
                    Program.Log?.Write(LogEventLevel.Warning, "No commits to merge.");
                    return false;
                }

                // No duplicate PR
                IReadOnlyList<PullRequest> prs = await Helpers.Do(() => Client.PullRequest.GetAllForRepository(OriginalOwner, Name,
                        new PullRequestRequest
                        {
                            State = ItemStateFilter.Open,
                            Head = $"{Owner}:{Branch.Name}",
                            Base = "main"
                        }));

                if (prs == null) return false; // rate-limited

                if (prs.Any())
                {
                    Program.Log?.Write(LogEventLevel.Warning, "Pull request already exists.");
                    return false;
                }

                return true;
            }
            catch (NotFoundException)
            {
                Program.Log?.Write(LogEventLevel.Warning, $"Branch or repository not found: {Owner}/{Name}");
                return false;
            }
            catch (Exception ex)
            {
                Program.Log?.Write(LogEventLevel.Error, "PR validation check failed", ex);
                return false;
            }
        }
    }
}