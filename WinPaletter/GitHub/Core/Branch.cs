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

            public static async Task<List<Octokit.Branch>> GetBranchesAsync(bool excludeMain = false)
            {
                Program.Log?.Write(LogEventLevel.Debug, $"GetBranchesAsync: owner='{Owner}', repo='{Repository.Name}', excludeMain={excludeMain}");
                IReadOnlyList<Octokit.Branch> branches = await Helpers.Do(() => Program.GitHub.Client.Repository.Branch.GetAll(Owner, Repository.Name));
                if (branches == null)
                {
                    Program.Log?.Write(LogEventLevel.Warning, "GetBranchesAsync: returned null (rate-limited or network failure).");
                    return [];
                }
                Program.Log?.Write(LogEventLevel.Debug, $"GetBranchesAsync: got {branches.Count} branches ('main' is hidden for protection.)");
                return [.. branches.Where(b => !excludeMain || !string.Equals(b.Name, "main", StringComparison.OrdinalIgnoreCase))];
            }

            public static async Task<Octokit.Branch> CreateBranchAsync(string newBranchName, string baseBranchName)
            {
                Program.Log?.Write(LogEventLevel.Debug, $"CreateBranchAsync: new='{newBranchName}', base='{baseBranchName}', repo='{Repository.Name}'");
                try
                {
                    Reference baseRef = await Helpers.Do(() => Program.GitHub.Client.Git.Reference.Get(Owner, Repository.Name, $"heads/{baseBranchName}"));
                    if (baseRef == null)
                    {
                        Program.Log?.Write(LogEventLevel.Warning, $"CreateBranchAsync: base ref 'heads/{baseBranchName}' returned null.");
                        return null;
                    }
                    Program.Log?.Write(LogEventLevel.Debug, $"CreateBranchAsync: base SHA='{baseRef.Object.Sha}'.");

                    NewReference newRef = new($"refs/heads/{newBranchName}", baseRef.Object.Sha);
                    Reference createdRef = await Helpers.Do(() => Program.GitHub.Client.Git.Reference.Create(Owner, Repository.Name, newRef));
                    if (createdRef == null)
                    {
                        Program.Log?.Write(LogEventLevel.Warning, $"CreateBranchAsync: Reference.Create returned null for '{newBranchName}'.");
                        return null;
                    }
                    Program.Log?.Write(LogEventLevel.Debug, $"CreateBranchAsync: ref created at '{createdRef.Url}'.");

                    var result = await Helpers.Do(() => Program.GitHub.Client.Repository.Branch.Get(Owner, Repository.Name, newBranchName));
                    Program.Log?.Write(LogEventLevel.Information, $"CreateBranchAsync: branch '{newBranchName}' created successfully.");
                    return result;
                }
                catch (Exception ex)
                {
                    Program.Log?.Write(LogEventLevel.Error, $"CreateBranchAsync: failed. {ex.Message}");
                    return null;
                }
            }

            public static async Task<bool> DeleteBranchAsync(string branchName)
            {
                Program.Log?.Write(LogEventLevel.Debug, $"DeleteBranchAsync: branch='{branchName}', repo='{Repository.Name}'");
                try
                {
                    await Helpers.Do(async () =>
                    {
                        await Program.GitHub.Client.Git.Reference.Delete(Owner, Repository.Name, $"heads/{branchName}");
                        return true;
                    });
                    Program.Log?.Write(LogEventLevel.Information, $"DeleteBranchAsync: branch '{branchName}' deleted.");
                    return true;
                }
                catch (Exception ex)
                {
                    Program.Log?.Write(LogEventLevel.Error, $"DeleteBranchAsync: failed. {ex.Message}");
                    return false;
                }
            }

            public static async Task<Octokit.Branch> GetBranch(string branchName)
            {
                Program.Log?.Write(LogEventLevel.Debug, $"GetBranch: branch='{branchName}', repo='{Repository.Name}'");
                try
                {
                    var result = await Helpers.Do(() => Program.GitHub.Client.Repository.Branch.Get(Owner, Repository.Name, branchName));
                    Program.Log?.Write(LogEventLevel.Debug, $"GetBranch: '{branchName}' → {(result == null ? "null" : "found")}");
                    return result;
                }
                catch (Exception ex)
                {
                    Program.Log?.Write(LogEventLevel.Warning, $"GetBranch: '{branchName}' not found or error. {ex.Message}");
                    return null;
                }
            }

            public static async Task<Octokit.Branch> RenameBranchAsync(string oldBranchName, string newBranchName)
            {
                Program.Log?.Write(LogEventLevel.Debug, $"RenameBranchAsync: '{oldBranchName}' → '{newBranchName}', repo='{Repository.Name}'");
                try
                {
                    if (await GetBranch(newBranchName) is not null)
                    {
                        Program.Log?.Write(LogEventLevel.Warning, $"RenameBranchAsync: target branch '{newBranchName}' already exists. Aborting.");
                        return null;
                    }

                    Reference oldRef = await Helpers.Do(() => Program.GitHub.Client.Git.Reference.Get(Owner, Repository.Name, $"heads/{oldBranchName}"));
                    if (oldRef == null)
                    {
                        Program.Log?.Write(LogEventLevel.Warning, $"RenameBranchAsync: old ref 'heads/{oldBranchName}' returned null.");
                        return null;
                    }
                    Program.Log?.Write(LogEventLevel.Debug, $"RenameBranchAsync: old SHA='{oldRef.Object.Sha}'.");

                    NewReference newRef = new($"refs/heads/{newBranchName}", oldRef.Object.Sha);
                    Reference createdRef = await Helpers.Do(() => Program.GitHub.Client.Git.Reference.Create(Owner, Repository.Name, newRef));
                    if (createdRef == null)
                    {
                        Program.Log?.Write(LogEventLevel.Warning, $"RenameBranchAsync: failed to create '{newBranchName}'.");
                        return null;
                    }
                    Program.Log?.Write(LogEventLevel.Debug, $"RenameBranchAsync: new ref created. Deleting old branch '{oldBranchName}'...");

                    await Helpers.Do(async () =>
                    {
                        await Program.GitHub.Client.Git.Reference.Delete(Owner, Repository.Name, $"heads/{oldBranchName}");
                        return true;
                    });
                    Program.Log?.Write(LogEventLevel.Information, $"RenameBranchAsync: '{oldBranchName}' → '{newBranchName}' complete.");

                    return await GetBranch(newBranchName);
                }
                catch (Exception ex)
                {
                    Program.Log?.Write(LogEventLevel.Error, $"RenameBranchAsync: failed. {ex.Message}");
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

            public static async Task<Octokit.Branch> SetBranchProtectionAsync(string branchName, bool protect)
            {
                Program.Log?.Write(LogEventLevel.Debug, $"SetBranchProtectionAsync: branch='{branchName}', protect={protect}, repo='{Repository.Name}'");

                string message = protect
                    ? string.Format(Program.Localization.Strings.GitHubStrings.BranchProtection_ProtectConfirm, branchName)
                    : string.Format(Program.Localization.Strings.GitHubStrings.BranchProtection_UnprotectConfirm, branchName);

                string details = protect
                    ? Program.Localization.Strings.GitHubStrings.BranchProtection_ProtectDetails
                    : Program.Localization.Strings.GitHubStrings.BranchProtection_UnprotectDetails;

                if (MsgBox(message, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, details) != DialogResult.Yes)
                {
                    Program.Log?.Write(LogEventLevel.Information, $"SetBranchProtectionAsync: user cancelled.");
                    return null;
                }

                try
                {
                    if (protect)
                    {
                        Program.Log?.Write(LogEventLevel.Debug, $"SetBranchProtectionAsync: applying protection to '{branchName}'...");
                        BranchProtectionSettingsUpdate protectionSettings = new(
                            requiredStatusChecks: null,
                            enforceAdmins: true,
                            requiredPullRequestReviews: null,
                            restrictions: null);
                        await Helpers.Do(() => Program.GitHub.Client.Repository.Branch.UpdateBranchProtection(Owner, Repository.Name, branchName, protectionSettings));
                    }
                    else
                    {
                        Program.Log?.Write(LogEventLevel.Debug, $"SetBranchProtectionAsync: removing protection from '{branchName}'...");
                        await Helpers.Do(() => Program.GitHub.Client.Repository.Branch.DeleteBranchProtection(Owner, Repository.Name, branchName));
                    }

                    var result = await Helpers.Do(() => Program.GitHub.Client.Repository.Branch.Get(Owner, Repository.Name, branchName));
                    Program.Log?.Write(LogEventLevel.Information, $"SetBranchProtectionAsync: '{branchName}' protection={protect} applied. Protected={result?.Protected}");
                    return result;
                }
                catch (Exception ex)
                {
                    Program.Log?.Write(LogEventLevel.Error, $"SetBranchProtectionAsync: failed. {ex.Message}");
                    string errorMessage = protect
                        ? string.Format(Program.Localization.Strings.GitHubStrings.BranchProtection_ProtectFailed, branchName, ex.Message)
                        : string.Format(Program.Localization.Strings.GitHubStrings.BranchProtection_UnprotectFailed, branchName, ex.Message);
                    MsgBox(errorMessage, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }

            public static async Task<bool> IsUpdatedAsync(string branch, string originalBranch)
            {
                Program.Log?.Write(LogEventLevel.Debug, $"IsUpdatedAsync: fork='{Owner}/{Repository.Name}/{branch}', upstream='{OriginalOwner}/{Repository.Name}/{originalBranch}'");
                try
                {
                    Octokit.Branch forkBranch = await Helpers.Do(() => Client.Repository.Branch.Get(Owner, Repository.Name, branch));
                    if (forkBranch?.Commit == null)
                    {
                        Program.Log?.Write(LogEventLevel.Warning, $"IsUpdatedAsync: fork branch '{branch}' not found in '{Owner}/{Repository.Name}'.");
                        return false;
                    }
                    Program.Log?.Write(LogEventLevel.Debug, $"IsUpdatedAsync: fork commit='{forkBranch.Commit.Sha}'");

                    Octokit.Branch upstreamBranch = null;
                    try
                    {
                        upstreamBranch = await Helpers.Do(() => Client.Repository.Branch.Get(OriginalOwner, Repository.Name, originalBranch));
                    }
                    catch (Octokit.NotFoundException)
                    {
                        Program.Log?.Write(LogEventLevel.Information, $"IsUpdatedAsync: upstream branch '{originalBranch}' not found in '{OriginalOwner}/{Repository.Name}'. Treating as updated.");
                        return true;
                    }

                    if (upstreamBranch?.Commit == null)
                    {
                        Program.Log?.Write(LogEventLevel.Warning, $"IsUpdatedAsync: upstream branch commit is null. Treating as updated.");
                        return true;
                    }
                    Program.Log?.Write(LogEventLevel.Debug, $"IsUpdatedAsync: upstream commit='{upstreamBranch.Commit.Sha}'");

                    CompareResult compare = await Helpers.Do(() => Client.Repository.Commit.Compare(OriginalOwner, Repository.Name, upstreamBranch.Commit.Sha, forkBranch.Commit.Sha));
                    if (compare == null)
                    {
                        Program.Log?.Write(LogEventLevel.Warning, "IsUpdatedAsync: compare returned null.");
                        return false;
                    }

                    bool updated = compare.BehindBy == 0;
                    Program.Log?.Write(LogEventLevel.Information, $"IsUpdatedAsync: IsUpdated={updated} | AheadBy={compare.AheadBy} | BehindBy={compare.BehindBy}");
                    return updated;
                }
                catch (Exception ex)
                {
                    Program.Log?.Write(LogEventLevel.Error, $"IsUpdatedAsync: error for '{Owner}/{Repository.Name}'. {ex.Message}");
                    return false;
                }
            }

            public static async Task<bool> SyncBranchAsync(string forkBranch, string upstreamBranch)
            {
                Program.Log?.Write(LogEventLevel.Information, "SyncBranchAsync Started");
                Program.Log?.Write(LogEventLevel.Information, $"Fork    : {Repository.Owner}/{Repository.Name} @ {forkBranch}");
                Program.Log?.Write(LogEventLevel.Information, $"Upstream: {OriginalOwner}/{Repository.Name} @ {upstreamBranch}");

                try
                {
                    // Verify fork relationship
                    Program.Log?.Write(LogEventLevel.Debug, "SyncBranchAsync: verifying fork relationship...");
                    Octokit.Repository forkRepo = await Helpers.Do(() => Program.GitHub.Client.Repository.Get(Repository.Owner, Repository.Name));
                    if (forkRepo == null)
                    {
                        Program.Log?.Write(LogEventLevel.Warning, "SyncBranchAsync: could not fetch fork repo info. Aborting.");
                        return false;
                    }
                    Program.Log?.Write(LogEventLevel.Information, $"SyncBranchAsync: IsFork={forkRepo.Fork}, Parent={forkRepo.Parent?.FullName ?? "null"}, DefaultBranch={forkRepo.DefaultBranch}");

                    // Resolve default upstream branch
                    if (string.IsNullOrWhiteSpace(upstreamBranch))
                    {
                        Octokit.Repository upstreamRepo = await Helpers.Do(() => Program.GitHub.Client.Repository.Get(OriginalOwner, Repository.Name));
                        if (upstreamRepo == null)
                        {
                            Program.Log?.Write(LogEventLevel.Warning, "SyncBranchAsync: upstream repo returned null. Aborting.");
                            return false;
                        }
                        upstreamBranch = upstreamRepo.DefaultBranch;
                        Program.Log?.Write(LogEventLevel.Debug, $"SyncBranchAsync: resolved default upstream branch='{upstreamBranch}'.");
                    }

                    // Get upstream ref
                    Program.Log?.Write(LogEventLevel.Debug, $"SyncBranchAsync: fetching upstream ref 'heads/{upstreamBranch}' from '{OriginalOwner}/{Repository.Name}'...");
                    Reference upstreamRef = await Helpers.Do(() => Program.GitHub.Client.Git.Reference.Get(OriginalOwner, Repository.Name, $"heads/{upstreamBranch}"));
                    if (upstreamRef == null)
                    {
                        Program.Log?.Write(LogEventLevel.Warning, "SyncBranchAsync: upstream ref returned null. Aborting.");
                        return false;
                    }
                    Program.Log?.Write(LogEventLevel.Information, $"SyncBranchAsync: upstream SHA='{upstreamRef.Object.Sha}'.");

                    // Get fork ref
                    Program.Log?.Write(LogEventLevel.Debug, $"SyncBranchAsync: fetching fork ref 'heads/{forkBranch}' from '{Repository.Owner}/{Repository.Name}'...");
                    Reference forkRef;
                    try
                    {
                        forkRef = await Helpers.Do(() => Program.GitHub.Client.Git.Reference.Get(Repository.Owner, Repository.Name, $"heads/{forkBranch}"));
                        Program.Log?.Write(LogEventLevel.Information, $"SyncBranchAsync: fork SHA='{forkRef?.Object?.Sha}'.");
                    }
                    catch (Octokit.NotFoundException)
                    {
                        Program.Log?.Write(LogEventLevel.Warning, $"SyncBranchAsync: fork branch '{forkBranch}' not found. Creating at upstream SHA...");
                        forkRef = await Helpers.Do(() => Program.GitHub.Client.Git.Reference.Create(
                            Repository.Owner, Repository.Name,
                            new NewReference($"refs/heads/{forkBranch}", upstreamRef.Object.Sha)));
                        if (forkRef == null)
                        {
                            Program.Log?.Write(LogEventLevel.Error, "SyncBranchAsync: failed to create fork branch. Aborting.");
                            return false;
                        }
                        Program.Log?.Write(LogEventLevel.Information, $"SyncBranchAsync: fork branch '{forkBranch}' created.");
                        return true;
                    }

                    if (forkRef?.Object == null)
                    {
                        Program.Log?.Write(LogEventLevel.Warning, "SyncBranchAsync: fork ref null. Aborting.");
                        return false;
                    }

                    // Already up-to-date
                    if (forkRef.Object.Sha == upstreamRef.Object.Sha)
                    {
                        Program.Log?.Write(LogEventLevel.Information, $"SyncBranchAsync: '{forkBranch}' already up-to-date.");
                        return true;
                    }

                    Program.Log?.Write(LogEventLevel.Information, $"SyncBranchAsync: differs. Fork={forkRef.Object.Sha.Substring(0, 7)}, Upstream={upstreamRef.Object.Sha.Substring(0, 7)}.");

                    // Compare branches to understand relationship
                    try
                    {
                        var comparison = await Helpers.Do(() => Program.GitHub.Client.Repository.Commit.Compare(
                            OriginalOwner,
                            Repository.Name,
                            upstreamBranch,
                            $"{Repository.Owner}:{forkBranch}"));

                        Program.Log?.Write(LogEventLevel.Information,
                            $"SyncBranchAsync: Comparison result - ahead_by={comparison.AheadBy}, behind_by={comparison.BehindBy}, total_commits={comparison.TotalCommits}");

                        // If fork is behind upstream
                        if (comparison.BehindBy > 0)
                        {
                            Program.Log?.Write(LogEventLevel.Information, $"SyncBranchAsync: Fork is behind upstream by {comparison.BehindBy} commits. Attempting to sync...");

                            // Try merge-upstream first (GitHub's preferred method)
                            bool mergeUpstreamSuccess = false;
                            try
                            {
                                Uri endpoint = new Uri($"repos/{Repository.Owner}/{Repository.Name}/merge-upstream", UriKind.Relative);
                                await Helpers.Do(() => Program.GitHub.Client.Connection.Post<object>(
                                    endpoint,
                                    new { branch = forkBranch },
                                    "application/vnd.github+json",
                                    "application/json"));
                                Program.Log?.Write(LogEventLevel.Information, "SyncBranchAsync: merge-upstream succeeded.");
                                mergeUpstreamSuccess = true;
                            }
                            catch (Octokit.ApiValidationException ex)
                            {
                                // Log the actual exception message to see why it's failing
                                Program.Log?.Write(LogEventLevel.Warning, $"SyncBranchAsync: merge-upstream failed with: {ex.Message}");

                                // Don't treat "already up-to-date" as success when we know we're behind
                                if (ex.Message.Contains("already up-to-date"))
                                {
                                    Program.Log?.Write(LogEventLevel.Warning, "SyncBranchAsync: GitHub incorrectly reports 'already up-to-date' despite being behind. Will use alternative method.");
                                }
                                // Continue to fallback methods
                            }
                            catch (Exception ex)
                            {
                                Program.Log?.Write(LogEventLevel.Warning, $"SyncBranchAsync: merge-upstream failed ({ex.GetType().Name}: {ex.Message}).");
                            }

                            // If merge-upstream didn't succeed, try manual merge
                            if (!mergeUpstreamSuccess)
                            {
                                try
                                {
                                    Program.Log?.Write(LogEventLevel.Debug, "SyncBranchAsync: attempting manual merge...");
                                    var mergeResult = await Helpers.Do(() => Program.GitHub.Client.Repository.Merging.Create(
                                        Repository.Owner, Repository.Name,
                                        new NewMerge(forkBranch, upstreamRef.Object.Sha)
                                        {
                                            CommitMessage = $"Sync '{forkBranch}' with '{OriginalOwner}/{upstreamBranch}'"
                                        }));

                                    if (mergeResult != null)
                                    {
                                        Program.Log?.Write(LogEventLevel.Information, $"SyncBranchAsync: Manual merge succeeded. New SHA='{mergeResult.Sha.Substring(0, 7)}'.");

                                        // Verify the sync was successful
                                        var verifyComparison = await Helpers.Do(() => Program.GitHub.Client.Repository.Commit.Compare(
                                            OriginalOwner,
                                            Repository.Name,
                                            upstreamBranch,
                                            $"{Repository.Owner}:{forkBranch}"));

                                        if (verifyComparison.BehindBy == 0)
                                        {
                                            Program.Log?.Write(LogEventLevel.Information, "SyncBranchAsync: Verification confirms branch is now up-to-date.");
                                            return true;
                                        }
                                        else
                                        {
                                            Program.Log?.Write(LogEventLevel.Warning, $"SyncBranchAsync: After merge, branch still behind by {verifyComparison.BehindBy} commits.");
                                        }
                                    }
                                }
                                catch (Exception mergeEx)
                                {
                                    Program.Log?.Write(LogEventLevel.Warning, $"SyncBranchAsync: Manual merge failed ({mergeEx.GetType().Name}: {mergeEx.Message}).");
                                }
                            }
                            else
                            {
                                // merge-upstream reported success, but let's verify
                                var verifyForkRef = await Helpers.Do(() => Program.GitHub.Client.Git.Reference.Get(Repository.Owner, Repository.Name, $"heads/{forkBranch}"));
                                if (verifyForkRef?.Object?.Sha == upstreamRef.Object.Sha)
                                {
                                    Program.Log?.Write(LogEventLevel.Information, "SyncBranchAsync: Verified branch is now up-to-date.");
                                    return true;
                                }
                                else
                                {
                                    Program.Log?.Write(LogEventLevel.Warning, $"SyncBranchAsync: merge-upstream reported success but SHA didn't change. Fork={verifyForkRef?.Object?.Sha?.Substring(0, 7)}, Upstream={upstreamRef.Object.Sha.Substring(0, 7)}");
                                }
                            }
                        }
                    }
                    catch (Exception compareEx)
                    {
                        Program.Log?.Write(LogEventLevel.Warning, $"SyncBranchAsync: Comparison failed ({compareEx.GetType().Name}: {compareEx.Message}). Proceeding with force update...");
                    }

                    // If all else fails, force update the branch to match upstream exactly
                    Program.Log?.Write(LogEventLevel.Warning, "SyncBranchAsync: Performing force update to make branch identical to upstream...");

                    try
                    {
                        // First, try to update with force
                        Reference updated = await Helpers.Do(() => Program.GitHub.Client.Git.Reference.Update(
                            Repository.Owner, Repository.Name,
                            $"heads/{forkBranch}",
                            new ReferenceUpdate(upstreamRef.Object.Sha, force: true)));

                        if (updated != null)
                        {
                            Program.Log?.Write(LogEventLevel.Information, $"SyncBranchAsync: Force update succeeded. SHA='{updated.Object.Sha}'.");

                            // Verify the update
                            var verifyForkRef = await Helpers.Do(() => Program.GitHub.Client.Git.Reference.Get(Repository.Owner, Repository.Name, $"heads/{forkBranch}"));
                            if (verifyForkRef?.Object?.Sha == upstreamRef.Object.Sha)
                            {
                                Program.Log?.Write(LogEventLevel.Information, "SyncBranchAsync: Verified branch is now identical to upstream.");
                                return true;
                            }
                        }

                        Program.Log?.Write(LogEventLevel.Warning, "SyncBranchAsync: Force update returned null or verification failed.");

                        // Last resort: delete and recreate
                        Program.Log?.Write(LogEventLevel.Warning, "SyncBranchAsync: Attempting delete and recreate...");

                        try
                        {
                            await Helpers.Do(async () => Program.GitHub.Client.Git.Reference.Delete(
                                Repository.Owner,
                                Repository.Name,
                                $"heads/{forkBranch}"));
                        }
                        catch (Exception deleteEx)
                        {
                            Program.Log?.Write(LogEventLevel.Debug, $"SyncBranchAsync: Delete failed (may not exist): {deleteEx.Message}");
                        }

                        Reference recreated = await Helpers.Do(() => Program.GitHub.Client.Git.Reference.Create(
                            Repository.Owner,
                            Repository.Name,
                            new NewReference($"refs/heads/{forkBranch}", upstreamRef.Object.Sha)));

                        if (recreated != null)
                        {
                            Program.Log?.Write(LogEventLevel.Information, $"SyncBranchAsync: Delete/recreate succeeded. SHA='{recreated.Object.Sha}'.");
                            return true;
                        }

                        return false;
                    }
                    catch (Exception ex)
                    {
                        Program.Log?.Write(LogEventLevel.Error, $"SyncBranchAsync: Force update failed ({ex.GetType().Name}: {ex.Message}).");
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    Forms.BugReport.Throw(ex);
                    Program.Log?.Write(LogEventLevel.Error, $"SyncBranchAsync: unhandled error. {ex.Message}");
                    return false;
                }
            }

            public static async Task<List<string>> GetChangedFilesAsync(string baseBranch = "main")
            {
                Program.Log?.Write(LogEventLevel.Debug, $"GetChangedFilesAsync: base='{baseBranch}', branch='{Name}', repo='{Repository.Name}'");
                CompareResult comparison = await Helpers.Do(() => Client.Repository.Commit.Compare(Owner, Repository.Name, baseBranch, Name));
                if (comparison == null)
                {
                    Program.Log?.Write(LogEventLevel.Warning, "GetChangedFilesAsync: compare returned null.");
                    return [];
                }
                Program.Log?.Write(LogEventLevel.Debug, $"GetChangedFilesAsync: {comparison.Files.Count} changed files.");
                return [.. comparison.Files.Select(f => f.Filename)];
            }

            public static async Task<bool> IsNewFileAsync(string filePath, string baseBranch = "main")
            {
                Program.Log?.Write(LogEventLevel.Debug, $"IsNewFileAsync: file='{filePath}', base='{baseBranch}', branch='{Name}', repo='{Repository.Name}'");
                CompareResult comparison = await Helpers.Do(() => Client.Repository.Commit.Compare(Owner, Repository.Name, baseBranch, Name));
                if (comparison == null)
                {
                    Program.Log?.Write(LogEventLevel.Warning, "IsNewFileAsync: compare returned null.");
                    return false;
                }
                GitHubCommitFile file = comparison.Files.FirstOrDefault(f => f.Filename == filePath);
                bool isNew = file != null && file.Status.Equals("added", StringComparison.OrdinalIgnoreCase);
                Program.Log?.Write(LogEventLevel.Debug, $"IsNewFileAsync: '{filePath}' isNew={isNew}.");
                return isNew;
            }
        }
    }
}