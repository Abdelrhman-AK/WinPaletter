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
    public partial class Repository
    {
        public class Branch
        {
            /// <summary>
            /// Gets the branch name of the GitHub repository being accessed.
            /// </summary>
            public static string Name { get; set; } = "main";

            /// <summary>
            /// Lists all branches in the repository.
            /// </summary>
            /// <param name="excludeMain">If true, excludes the 'main' branch from the result.</param>
            /// <returns>A list of branch objects.</returns>
            public static async Task<List<Octokit.Branch>> GetBranchesAsync(bool excludeMain = false)
            {
                IReadOnlyList<Octokit.Branch> branches = await Helpers.Do(() => Program.GitHub.Client.Repository.Branch.GetAll(Owner, Repository.Name));

                if (branches == null) return []; // rate-limited or network failure

                return [.. branches.Where(b => !excludeMain || !string.Equals(b.Name, "main", StringComparison.OrdinalIgnoreCase))];
            }

            /// <summary>
            /// Creates a new branch from a base branch and returns the created branch.
            /// </summary>
            /// <param name="newBranchName">Name of the new branch.</param>
            /// <param name="baseBranchName">Base branch to branch from (default is current _branch).</param>
            /// <returns>The created <see cref="Branch"/> if successful, null otherwise.</returns>
            public static async Task<Octokit.Branch> CreateBranchAsync(string newBranchName, string baseBranchName)
            {
                try
                {
                    // Get the reference for the base branch
                    Reference baseRef = await Helpers.Do(() => Program.GitHub.Client.Git.Reference.Get(Owner, Name, $"heads/{baseBranchName}"));

                    if (baseRef == null) return null; // rate-limited or network failure

                    // Create the new branch reference
                    NewReference newRef = new($"refs/heads/{newBranchName}", baseRef.Object.Sha);
                    Reference createdRef = await Helpers.Do(() => Program.GitHub.Client.Git.Reference.Create(Owner, Name, newRef));

                    if (createdRef == null) return null; // rate-limited or network failure

                    // Fetch and return the newly created branch
                    Octokit.Branch createdBranch = await Helpers.Do(() => Program.GitHub.Client.Repository.Branch.Get(Owner, Name, newBranchName));

                    return createdBranch; // could still be null if rate-limited
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
                    // Use ExecuteGitHubActionSafeAsync with Task as T
                    await Helpers.Do(async () =>
                    {
                        await Program.GitHub.Client.Git.Reference.Delete(Owner, Name, $"heads/{branchName}");
                        return true; // Return a value just to satisfy generic
                    });

                    return true;
                }
                catch
                {
                    return false;
                }
            }

            /// <summary>
            /// Checks whether a branch exists in the repository.
            /// </summary>
            /// <param name="branchName">Branch name to check.</param>
            /// <returns>True if the branch exists, false otherwise.</returns>
            public static async Task<Octokit.Branch> GetBranch(string branchName)
            {
                try
                {
                    Octokit.Branch branch = await Helpers.Do(() => Program.GitHub.Client.Repository.Branch.Get(Owner, Name, branchName));
                    return branch; // could be null if rate-limited or network lost
                }
                catch
                {
                    return null;
                }
            }

            /// <summary>
            /// Renames a branch by creating a new branch with the same SHA and deleting the old branch.
            /// Aborts if the target branch already exists.
            /// </summary>
            /// <param name="oldBranchName">Existing branch name.</param>
            /// <param name="newBranchName">New branch name.</param>
            /// <returns>True if renamed successfully, false otherwise.</returns>
            public static async Task<Octokit.Branch> RenameBranchAsync(string oldBranchName, string newBranchName)
            {
                try
                {
                    // Check if new branch already exists
                    if (await GetBranch(newBranchName) is not null) return null;

                    // Get old branch reference
                    Reference oldRef = await Helpers.Do(() => Program.GitHub.Client.Git.Reference.Get(Owner, Name, $"heads/{oldBranchName}"));

                    if (oldRef == null) return null; // rate-limited or network failure

                    // Create new branch reference
                    NewReference newRef = new($"refs/heads/{newBranchName}", oldRef.Object.Sha);
                    Reference createdRef = await Helpers.Do(() => Program.GitHub.Client.Git.Reference.Create(Owner, Name, newRef));

                    if (createdRef == null) return null; // rate-limited or network failure

                    // Delete old branch
                    await Helpers.Do(async () => Program.GitHub.Client.Git.Reference.Delete(Owner, Name, $"heads/{oldBranchName}"));

                    // Return the newly created branch
                    return await GetBranch(newBranchName);
                }
                catch
                {
                    return null;
                }
            }

            public static string SanitizeBranchName(string input)
            {
                if (string.IsNullOrWhiteSpace(input)) return string.Empty;
                string s = Regex.Replace(input.ToLowerInvariant(), @"[^a-z0-9\-_/]", "-");
                s = Regex.Replace(s, @"-+", "-");
                s = Regex.Replace(s, @"//+", "/");
                return s.Trim('-', '/');
            }

            public static bool IsValidBranchName(string name)
            {
                if (string.IsNullOrWhiteSpace(name)) return false;
                if (name == "." || name == "..") return false;

                if (name.StartsWith("/") || name.EndsWith("/")) return false;
                if (name.EndsWith(".lock", StringComparison.Ordinal)) return false;

                if (name.Contains("..", StringComparison.Ordinal)) return false;
                if (name.Contains("//", StringComparison.Ordinal)) return false;
                if (name.Contains("@{", StringComparison.Ordinal)) return false;

                foreach (char c in name)
                {
                    if (c <= 0x20 || c == 0x7F) return false;
                    if ("~^:?*[\\]".IndexOf(c) >= 0) return false;
                }

                return true;
            }

            /// <summary>
            /// Protects or unprotects a branch after user confirmation using localized messages.
            /// </summary>
            /// <param name="branchName">The branch to protect or unprotect.</param>
            /// <param name="protect">True to protect, false to unprotect.</param>
            /// <returns>The updated Branch if successful, null if cancelled or failed.</returns>
            public static async Task<Octokit.Branch> SetBranchProtectionAsync(string branchName, bool protect)
            {
                string message = protect
                    ? string.Format(Program.Localization.Strings.GitHubStrings.BranchProtection_ProtectConfirm, branchName)
                    : string.Format(Program.Localization.Strings.GitHubStrings.BranchProtection_UnprotectConfirm, branchName);

                string details = protect
                    ? Program.Localization.Strings.GitHubStrings.BranchProtection_ProtectDetails
                    : Program.Localization.Strings.GitHubStrings.BranchProtection_UnprotectDetails;

                if (MsgBox(message, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, details) != DialogResult.Yes) return null;

                try
                {
                    if (protect)
                    {
                        BranchProtectionSettingsUpdate protectionSettings = new(requiredStatusChecks: null, enforceAdmins: true, requiredPullRequestReviews: null, restrictions: null);

                        await Helpers.Do(() => Program.GitHub.Client.Repository.Branch.UpdateBranchProtection(Owner, Name, branchName, protectionSettings));
                    }
                    else
                    {
                        await Helpers.Do(() => Program.GitHub.Client.Repository.Branch.DeleteBranchProtection(Owner, Name, branchName));
                    }

                    Octokit.Branch updatedBranch = await Helpers.Do(() => Program.GitHub.Client.Repository.Branch.Get(Owner, Name, branchName));

                    return updatedBranch; // could be null if rate-limited or network failure
                }
                catch (Exception ex)
                {
                    string errorMessage = protect
                        ? string.Format(Program.Localization.Strings.GitHubStrings.BranchProtection_ProtectFailed, branchName, ex.Message)
                        : string.Format(Program.Localization.Strings.GitHubStrings.BranchProtection_UnprotectFailed, branchName, ex.Message);

                    MsgBox(errorMessage, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }

            public static async Task<bool> IsUpdatedAsync(string branch, string originalBranch)
            {
                try
                {
                    // Get fork/source branch
                    Octokit.Branch forkBranch = await Helpers.Do(() => Client.Repository.Branch.Get(Owner, Name, branch));

                    if (forkBranch?.Commit == null)
                    {
                        Program.Log?.Write(LogEventLevel.Warning, $"Source branch '{branch}' not found in {Owner}/{Name}.");
                        return false;
                    }

                    // Try to get target branch
                    Octokit.Branch upstreamBranch = null;
                    try
                    {
                        upstreamBranch = await Helpers.Do(() => Client.Repository.Branch.Get(OriginalOwner, Name, originalBranch));
                    }
                    catch (Octokit.NotFoundException)
                    {
                        Program.Log?.Write(LogEventLevel.Information, $"Target branch '{originalBranch}' not found in {OriginalOwner}/{Name}. Treating source branch as updated.");
                        return true; // Treat as updated if target branch does not exist
                    }

                    if (upstreamBranch?.Commit == null) return true;

                    // Compare commits: base = upstream, head = fork
                    CompareResult compare = await Helpers.Do(() => Client.Repository.Commit.Compare(OriginalOwner, Name, upstreamBranch.Commit.Sha, forkBranch.Commit.Sha));

                    if (compare == null) return false; // rate-limited or network failure

                    // BehindBy == 0 → source branch is up-to-date
                    bool updated = compare.BehindBy == 0;

                    Program.Log?.Write(LogEventLevel.Information, $"IsUpdated = {updated} | AheadBy={compare.AheadBy} | BehindBy={compare.BehindBy}");
                    return updated;
                }
                catch (Exception ex)
                {
                    Program.Log?.Write(LogEventLevel.Error, $"Error checking update state for {Owner}/{Name}", ex);
                    return false;
                }
            }

            /// <summary>
            /// Syncs a branch in a forked repository with a branch in the upstream repository.
            /// </summary>
            /// <param name="forkBranch">Branch in the fork to sync.</param>
            /// <param name="upstreamBranch">Branch in the upstream repo to sync from.</param>
            /// <returns>True if sync succeeds or already up-to-date; false otherwise.</returns>
            public static async Task<bool> SyncBranchAsync(string forkBranch, string upstreamBranch)
            {
                try
                {
                    // Get default upstream branch if not provided
                    if (string.IsNullOrWhiteSpace(upstreamBranch))
                    {
                        Octokit.Repository upstreamRepo = await Helpers.Do(() => Program.GitHub.Client.Repository.Get(OriginalOwner, Name));

                        if (upstreamRepo == null) return false; // rate-limited or network failure

                        upstreamBranch = upstreamRepo.DefaultBranch;
                    }

                    // Get upstream reference
                    Reference upstreamRef = await Helpers.Do(() => Program.GitHub.Client.Git.Reference.Get(OriginalOwner, Name, $"heads/{upstreamBranch}"));

                    if (upstreamRef == null) return false;

                    // Get or create fork branch reference
                    Reference forkRef;
                    try
                    {
                        forkRef = await Helpers.Do(() => Program.GitHub.Client.Git.Reference.Get(Repository.Owner, Name, $"heads/{forkBranch}"));
                    }
                    catch (Octokit.NotFoundException)
                    {
                        forkRef = await Helpers.Do(() => Program.GitHub.Client.Git.Reference.Create(Repository.Owner, Name, new NewReference($"refs/heads/{forkBranch}", upstreamRef.Object.Sha)));
                    }

                    if (forkRef == null) return false;

                    // Already up-to-date
                    if (forkRef.Object.Sha == upstreamRef.Object.Sha)
                    {
                        Program.Log?.Write(LogEventLevel.Information, $"Branch '{forkBranch}' already up-to-date.");
                        return true;
                    }

                    // If syncing the same branch as upstream
                    if (forkBranch == upstreamBranch)
                    {
                        try
                        {
                            Uri endpoint = new($"repos/{Repository.Owner}/{Name}/merge-upstream", UriKind.Relative);
                            await Helpers.Do(() => Program.GitHub.Client.Connection.Put<object>(endpoint, new { branch = forkBranch }, "application/json"));

                            Program.Log?.Write(LogEventLevel.Information, $"merge-upstream succeeded.");
                            return true;
                        }
                        catch (Octokit.ApiValidationException)
                        {
                            Program.Log?.Write(LogEventLevel.Information, $"merge-upstream: already up-to-date.");
                            return true;
                        }
                        catch (Octokit.NotFoundException)
                        {
                            Program.Log?.Write(LogEventLevel.Warning, $"merge-upstream unavailable, forcing ref sync.");
                        }
                    }

                    // Hard reset fork branch to upstream
                    await Helpers.Do(() => Program.GitHub.Client.Git.Reference.Update(Repository.Owner, Name, $"heads/{forkBranch}", new ReferenceUpdate(upstreamRef.Object.Sha, true)));

                    Program.Log?.Write(LogEventLevel.Information, $"Force-synced {forkBranch} to {OriginalOwner}/{upstreamBranch}.");
                    return true;
                }
                catch (Exception ex)
                {
                    Forms.BugReport.Throw(ex);
                    Program.Log?.Write(LogEventLevel.Error, $"Error syncing branch.", ex);
                    return false;
                }
            }

            /// <summary>
            /// Get all changed files in the branch compared to base branch
            /// </summary>
            /// <param name="baseBranch"></param>
            /// <returns></returns>
            public static async Task<List<string>> GetChangedFilesAsync(string baseBranch = "main")
            {
                CompareResult comparison = await Helpers.Do(() => Client.Repository.Commit.Compare(Owner, Repository.Name, baseBranch, Name));

                if (comparison == null) return []; // rate-limited or network failure

                return [.. comparison.Files.Select(f => f.Filename)];
            }

            /// <summary>
            /// Check if a file is newly added in this branch
            /// </summary>
            /// <param name="branchName"></param>
            /// <param name="filePath"></param>
            /// <param name="baseBranch"></param>
            /// <returns></returns>
            public static async Task<bool> IsNewFileAsync(string filePath, string baseBranch = "main")
            {
                CompareResult comparison = await Helpers.Do(() => Client.Repository.Commit.Compare(Owner, Repository.Name, baseBranch, Name));

                if (comparison == null) return false; // rate-limited or network failure

                GitHubCommitFile file = comparison.Files.FirstOrDefault(f => f.Filename == filePath);
                return file != null && file.Status.Equals("added", StringComparison.OrdinalIgnoreCase);
            }
        }
    }
}