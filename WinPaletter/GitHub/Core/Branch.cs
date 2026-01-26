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
                IReadOnlyList<Octokit.Branch> branches = await Program.GitHub.Client.Repository.Branch.GetAll(Owner, Repository.Name);
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
                    var baseRef = await Program.GitHub.Client.Git.Reference.Get(Owner, Name, $"heads/{baseBranchName}");

                    // Create the new branch reference
                    var newRef = new NewReference($"refs/heads/{newBranchName}", baseRef.Object.Sha);
                    await Program.GitHub.Client.Git.Reference.Create(Owner, Name, newRef);

                    // Fetch and return the newly created branch
                    Octokit.Branch createdBranch = await Program.GitHub.Client.Repository.Branch.Get(Owner, Name, newBranchName);
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
                    await Program.GitHub.Client.Git.Reference.Delete(Owner, Name, $"heads/{branchName}");
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
                    return await Program.GitHub.Client.Repository.Branch.Get(Owner, Name, branchName);
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
                    if (await GetBranch(newBranchName) is not null) return null; // new branch already exists

                    var oldRef = await Program.GitHub.Client.Git.Reference.Get(Owner, Name, $"heads/{oldBranchName}");
                    var newRef = new NewReference($"refs/heads/{newBranchName}", oldRef.Object.Sha);
                    await Program.GitHub.Client.Git.Reference.Create(Owner, Name, newRef);
                    await Program.GitHub.Client.Git.Reference.Delete(Owner, Name, $"heads/{oldBranchName}");
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
                    var client = Program.GitHub.Client;

                    if (protect)
                    {
                        BranchProtectionSettingsUpdate protectionSettings = new(requiredStatusChecks: null, enforceAdmins: true, requiredPullRequestReviews: null, restrictions: null);

                        await client.Repository.Branch.UpdateBranchProtection(Owner, Name, branchName, protectionSettings);
                    }
                    else
                    {
                        await client.Repository.Branch.DeleteBranchProtection(Owner, Name, branchName);
                    }

                    Octokit.Branch updatedBranch = await client.Repository.Branch.Get(Owner, Name, branchName);
                    return updatedBranch;
                }
                catch (Exception ex)
                {
                    string errorMessage = protect ? string.Format(Program.Localization.Strings.GitHubStrings.BranchProtection_ProtectFailed, branchName, ex.Message)
                                                  : string.Format(Program.Localization.Strings.GitHubStrings.BranchProtection_UnprotectFailed, branchName, ex.Message);

                    MsgBox(errorMessage, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }

            public static async Task<bool> IsUpdatedAsync(Octokit.Branch branch, string originalBranch)
            {
                if (!Program.IsNetworkAvailable) return false;

                try
                {
                    // Use provided branch or fetch by name
                    Octokit.Branch forkBranch = branch ?? await _client.Repository.Branch.Get(Owner, Name, branch.Name).ConfigureAwait(false);
                    if (forkBranch?.Commit == null)
                    {
                        Program.Log?.Write(LogEventLevel.Warning, $"Source branch '{branch.Name}' not found in {Owner}/{Name}.");
                        return false;
                    }

                    // Try to get target branch
                    Octokit.Branch upstreamBranch = null;
                    try
                    {
                        upstreamBranch = await _client.Repository.Branch.Get(OriginalOwner, Name, originalBranch).ConfigureAwait(false);
                    }
                    catch (Octokit.NotFoundException)
                    {
                        Program.Log?.Write(LogEventLevel.Information, $"Target branch '{originalBranch}' not found in {OriginalOwner}/{Name}. Treating source branch as updated.");
                        return true; // Treat as updated if target branch does not exist
                    }

                    // Compare (base = upstream, head = fork)
                    CompareResult compare = await _client.Repository.Commit.Compare(OriginalOwner, Name, upstreamBranch.Commit.Sha, forkBranch.Commit.Sha);

                    bool updated = compare.BehindBy == 0;

                    Program.Log?.Write(LogEventLevel.Information, $"IsUpdated({branch.Name}) = {updated} | AheadBy={compare.AheadBy} | BehindBy={compare.BehindBy}");
                    return updated;
                }
                catch (Exception ex)
                {
                    Program.Log?.Write(LogEventLevel.Error, $"Error checking update state for {Owner}/{Name}", ex);
                    return false;
                }
            }

            public static async Task<bool> IsUpdatedAsync(string branch, string originalBranch)
            {
                if (!Program.IsNetworkAvailable) return false;

                try
                {
                    // Get fork/source branch
                    Octokit.Branch forkBranch = await _client.Repository.Branch.Get(Owner, Name, branch).ConfigureAwait(false);
                    if (forkBranch?.Commit == null)
                    {
                        Program.Log?.Write(LogEventLevel.Warning, $"Source branch '{branch}' not found in {Owner}/{Name}.");
                        return false;
                    }

                    // Try to get target branch
                    Octokit.Branch upstreamBranch = null;
                    try
                    {
                        upstreamBranch = await _client.Repository.Branch.Get(OriginalOwner, Name, originalBranch).ConfigureAwait(false);
                    }
                    catch (Octokit.NotFoundException)
                    {
                        Program.Log?.Write(LogEventLevel.Information, $"Target branch '{originalBranch}' not found in {OriginalOwner}/{Name}. Treating source branch as updated.");
                        return true; // Treat as updated if target branch does not exist
                    }

                    // Correct Compare order: base = upstream, head = fork
                    CompareResult compare = await _client.Repository.Commit.Compare(OriginalOwner, Name, upstreamBranch.Commit.Sha, forkBranch.Commit.Sha);

                    // If BehindBy == 0, source branch is up-to-date (may be equal or ahead)
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
                if (!Program.IsNetworkAvailable) return false;

                try
                {
                    var client = Program.GitHub.Client;

                    if (string.IsNullOrWhiteSpace(upstreamBranch))
                    {
                        Octokit.Repository upstreamRepo = await client.Repository.Get(OriginalOwner, Name);
                        upstreamBranch = upstreamRepo.DefaultBranch;
                    }

                    Reference upstreamRef = await client.Git.Reference.Get( OriginalOwner, Name, $"heads/{upstreamBranch}");

                    Reference forkRef;
                    try
                    {
                        forkRef = await client.Git.Reference.Get(Repository.Owner, Name, $"heads/{forkBranch}");
                    }
                    catch (Octokit.NotFoundException)
                    {
                        forkRef = await client.Git.Reference.Create(Repository.Owner, Name, new NewReference($"refs/heads/{forkBranch}", upstreamRef.Object.Sha));
                    }

                    if (forkRef.Object.Sha == upstreamRef.Object.Sha)
                    {
                        Program.Log?.Write(LogEventLevel.Information, $"Branch '{forkBranch}' already up-to-date.");
                        return true;
                    }

                    if (forkBranch == upstreamBranch)
                    {
                        try
                        {
                            Uri endpoint = new($"repos/{Repository.Owner}/{Name}/merge-upstream", UriKind.Relative);
                            await client.Connection.Put<object>(endpoint, new { branch = forkBranch }, "application/json");
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
                    await client.Git.Reference.Update( Repository.Owner, Name, $"heads/{forkBranch}", new ReferenceUpdate(upstreamRef.Object.Sha, true));

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
                CompareResult comparison = await _client.Repository.Commit.Compare(Owner, Repository.Name, baseBranch, Name);
                List<string> files = [.. comparison.Files.Select(f => f.Filename)];
                return files;
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
                CompareResult comparison = await _client.Repository.Commit.Compare(Owner, Repository.Name, baseBranch, Name);
                GitHubCommitFile file = comparison.Files.FirstOrDefault(f => f.Filename == filePath);
                if (file != null)
                {
                    return file.Status.Equals("added", System.StringComparison.OrdinalIgnoreCase);
                }
                return false;
            }
        }
    }
}
