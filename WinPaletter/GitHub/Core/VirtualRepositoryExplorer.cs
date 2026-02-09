using Octokit;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WinPaletter.GitHub
{
    /// <summary>
    /// Provides static methods and properties for navigating, displaying, and managing the contents of a GitHub
    /// repository in a tree and list view interface. Supports repository traversal, caching, and UI integration for
    /// efficient browsing and manipulation of repository files and folders.
    /// </summary>
    /// <remarks>The FileSystem class is designed for use in applications that present a hierarchical view of a
    /// GitHub repository, such as file explorers or resource managers. It maintains internal caches to optimize
    /// repeated access to repository data and provides navigation methods (back, forward, up) for user-friendly
    /// traversal. Thread safety is ensured for concurrent operations involving repository data. Most members are
    /// intended to be used in conjunction with UI controls such as TreeView, ListView, and Breadcrumb, and require
    /// proper initialization and event handling for correct operation.</remarks>
    public static partial class FileSystem
    {
        /// <summary>
        /// SemaphoreSlim to limit concurrent GitHub API requests for repository data fetching.
        /// </summary>
        private static readonly SemaphoreSlim _semaphore = new(5);
        private static readonly SemaphoreSlim _treeLock = new(1, 1);
        private static string _cachedHeadSha;
        private static IReadOnlyList<TreeItem> _cachedTree;

        private static async Task<IReadOnlyList<TreeItem>> GetCachedTreeAsync(CancellationToken token = default)
        {
            // Safe call for getting the branch reference
            Reference reference = await Helpers.ExecuteGitHubActionSafeAsync(async () => await Program.GitHub.Client.Git.Reference.Get(Repository.Owner, Repository.Name, $"heads/{Repository.Branch.Name}").ConfigureAwait(false));

            if (reference == null)
            {
                Program.Log?.Write(LogEventLevel.Warning, "Failed to get Git reference due to network, rate limit, or server error.");
                return null;
            }

            string currentSha = reference.Object.Sha;

            if (_cachedTree != null && _cachedHeadSha == currentSha) return _cachedTree;

            await _treeLock.WaitAsync(token);
            try
            {
                // Double-check inside lock
                if (_cachedTree != null && _cachedHeadSha == currentSha) return _cachedTree;

                // Safe call for getting the recursive tree
                TreeResponse tree = await Helpers.ExecuteGitHubActionSafeAsync(async () => await Program.GitHub.Client.Git.Tree.GetRecursive(Repository.Owner, Repository.Name, currentSha));

                if (tree == null)
                {
                    Program.Log?.Write(LogEventLevel.Warning, "Failed to get Git tree due to network, rate limit, or server error.");
                    return null;
                }

                _cachedHeadSha = currentSha;
                _cachedTree = tree.Tree;

                return _cachedTree;
            }
            finally
            {
                _treeLock.Release();
            }
        }

        /// <summary>
        /// Recursively fetches repository contents starting at the specified path,
        /// optionally reporting progress and respecting cancellation and depth limits.
        /// </summary>
        /// <param name="path">The starting path inside the repository.</param>
        /// <param name="output">A list receiving all discovered entries.</param>
        /// <param name="reportProgress">Optional callback reporting each discovered entry.</param>
        /// <param name="cts">Optional cancellation token source.</param>
        /// <param name="maxDepth">Optional maximum recursion depth; -1 means unlimited.</param>
        /// <param name="currentDepth">The current recursion depth.</param>
        /// <returns>A task representing the recursive fetch.</returns>
        private static async Task FetchRecursive(string path, List<RepositoryContent> output, Action<RepositoryContent> reportProgress = null, CancellationTokenSource cts = default, int maxDepth = -1, int currentDepth = 0)
        {
            cts ??= new();
            if (cts.Token.IsCancellationRequested) return;

            try
            {
                IReadOnlyList<TreeItem> tree = await GetCachedTreeAsync(cts.Token);

                if (tree == null)
                {
                    Program.Log?.Write(LogEventLevel.Warning, "FetchRecursive aborted: Git tree unavailable due to network, rate limit, or server error.");
                    return;
                }

                string normalizedPath = string.IsNullOrEmpty(path) ? string.Empty : path.Trim('/');

                foreach (TreeItem item in tree)
                {
                    if (cts.Token.IsCancellationRequested) return;

                    if (!string.IsNullOrEmpty(normalizedPath))
                    {
                        if (!item.Path.StartsWith(normalizedPath + "/", StringComparison.OrdinalIgnoreCase) && !item.Path.Equals(normalizedPath, StringComparison.OrdinalIgnoreCase))
                            continue;
                    }

                    int depth = item.Path.Count(c => c == '/');
                    if (maxDepth >= 0 && depth > maxDepth) continue;

                    // Generate URLs for the repository
                    string htmlUrl = GenerateHtmlUrl(item.Path);
                    string apiUrl = GenerateApiUrl(item.Path);
                    string gitUrl = GenerateGitUrl(item.Path);
                    string downloadUrl = GenerateDownloadUrl(item.Path);

                    RepositoryContent content = new(
                        name: System.IO.Path.GetFileName(item.Path),
                        path: item.Path,
                        sha: item.Sha,
                        size: item.Size,
                        type: ConvertTreeType(item.Type.Value),
                        downloadUrl: downloadUrl,
                        url: apiUrl,
                        gitUrl: gitUrl,
                        htmlUrl: htmlUrl,
                        encoding: null,
                        encodedContent: null,
                        target: null,
                        submoduleGitUrl: null
                    );

                    output.Add(content);
                    reportProgress?.Invoke(content);
                }
            }
            catch (Exception ex)
            {
                Program.Log?.Write(LogEventLevel.Error, "FetchRecursive failed.", ex);
                return;
            }
        }

        // Helper methods to generate proper URLs
        private static string GenerateHtmlUrl(string path)
        {
            if (string.IsNullOrEmpty(path))
                return $"https://github.com/{Repository.Owner}/{GitHub.Repository.Name}";

            return $"https://github.com/{Repository.Owner}/{GitHub.Repository.Name}/blob/{GitHub.Repository.Branch.Name}/{path}";
        }

        private static string GenerateApiUrl(string path)
        {
            var encodedPath = Uri.EscapeDataString(path);
            return $"https://api.github.com/repos/{Repository.Owner}/{GitHub.Repository.Name}/contents/{encodedPath}?ref={GitHub.Repository.Branch.Name}";
        }

        private static string GenerateGitUrl(string path)
        {
            var encodedPath = Uri.EscapeDataString(path);
            return $"https://api.github.com/repos/{Repository.Owner}/{GitHub.Repository.Name}/git/trees/{encodedPath}";
        }

        private static string GenerateDownloadUrl(string path)
        {
            if (string.IsNullOrEmpty(path))
                return null;

            var encodedPath = Uri.EscapeDataString(path);
            return $"https://raw.githubusercontent.com/{Repository.Owner}/{GitHub.Repository.Name}/{GitHub.Repository.Branch.Name}/{encodedPath}";
        }

        public static async Task<RepositoryContent> GetRepositoryContentAsync(string path)
        {
            if (string.IsNullOrEmpty(path)) return null;

            try
            {
                IReadOnlyList<TreeItem> tree = await GetCachedTreeAsync();

                if (tree == null)
                {
                    Program.Log?.Write(LogEventLevel.Warning, $"GetRepositoryContentAsync aborted: Git tree unavailable due to network, rate limit, or server error for path '{path}'.");
                    return null;
                }

                TreeItem item = tree.FirstOrDefault(t => t.Path.Equals(path.Trim('/'), StringComparison.OrdinalIgnoreCase));

                if (item == null) return null;

                string htmlUrl = GenerateHtmlUrl(item.Path);
                string apiUrl = GenerateApiUrl(item.Path);
                string gitUrl = GenerateGitUrl(item.Path);
                string downloadUrl = GenerateDownloadUrl(item.Path);

                return new RepositoryContent(
                    System.IO.Path.GetFileName(item.Path),
                    item.Path,
                    item.Sha,
                    item.Size,
                    ConvertTreeType(item.Type.Value),
                    downloadUrl: downloadUrl,
                    url: apiUrl,
                    gitUrl: gitUrl,
                    htmlUrl: htmlUrl,
                    encoding: null,
                    encodedContent: null,
                    target: null,
                    submoduleGitUrl: null
                );
            }
            catch (NotFoundException)
            {
                return null;
            }
            catch (Exception ex)
            {
                Program.Log?.Write(LogEventLevel.Error, $"GetRepositoryContentAsync failed for path '{path}'.", ex);
                return null;
            }
        }
        private static ContentType ConvertTreeType(TreeType treeType)
        {
            return treeType switch
            {
                TreeType.Blob => ContentType.File,
                TreeType.Tree => ContentType.Dir,
                TreeType.Commit => ContentType.Submodule,
                _ => ContentType.File
            };
        }

        /// <summary>
        /// Retrieves a cached Entry for the specified path and validates it against the latest commits.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private static async Task<Entry> GetFromCacheValidatedAsync(string path)
        {
            if (!Cache.Contains(path)) return null;

            Cache.TryGetEntry(path, out Entry cachedEntry);

            try
            {
                // Helper to fetch the latest commit for a path safely
                async Task<GitHubCommit> GetLatestCommitAsync(string p) =>
                    await Helpers.ExecuteGitHubActionSafeAsync(async () =>
                        (await Program.GitHub.Client.Repository.Commit.GetAll(Repository.Owner, GitHub.Repository.Name, new CommitRequest { Path = p }))
                        .FirstOrDefault()
                    );

                // File entry validation
                if (cachedEntry.Type == EntryType.File)
                {
                    GitHubCommit latestCommit = await GetLatestCommitAsync(path);
                    if (latestCommit != null && latestCommit.Sha == cachedEntry.CommitSha) return cachedEntry;

                    Cache.Remove(path);
                    return null;
                }

                // Directory entry validation
                if (cachedEntry.Type == EntryType.Dir && cachedEntry.Children != null)
                {
                    List<string> childPaths = [.. cachedEntry.Children.Select(c => c.Path)];
                    Dictionary<string, string> latestShas = new(StringComparer.OrdinalIgnoreCase);

                    // Fetch latest SHAs for all children safely
                    foreach (string childPath in childPaths)
                    {
                        GitHubCommit latest = await GetLatestCommitAsync(childPath);
                        if (latest != null) latestShas[childPath] = latest.Sha;
                    }

                    // Compare with cached SHAs
                    foreach (Entry child in cachedEntry.Children)
                    {
                        if (!Cache.TryGetValue(child.Path, out Cache.CacheData cachedChildTuple) ||
                            (latestShas.TryGetValue(child.Path, out string latestSha) && cachedChildTuple.Entry.CommitSha != latestSha))
                        {
                            Cache.Remove(path);
                            return null;
                        }
                    }

                    return cachedEntry;
                }
            }
            catch
            {
                // Fallback to cache if API fails
                return cachedEntry;
            }

            return null;
        }

        /// <summary>
        /// Unified repository entry retrieval.
        /// Supports recursion, SHA/TTL caching, and force refresh.
        /// </summary>
        /// <param name="path">Repository path to retrieve.</param>
        /// <param name="recursive">Whether to fetch children recursively.</param>
        /// <param name="useShaValidation">Validate cached entries against commit SHAs.</param>
        /// <param name="useTtlCache">Use TTL-based caching.</param>
        /// <param name="forceRefresh">Ignore cache and fetch fresh.</param>
        /// <param name="maxDepth">Max recursion depth (-1 = unlimited).</param>
        /// <param name="token">Optional cancellation token.</param>
        /// <returns>The repository Entry for the path.</returns>
        public static async Task<Entry> GetInfoAsync(string path, bool recursive = true, bool useShaValidation = true, bool useTtlCache = true, bool forceRefresh = false, int maxDepth = 20, CancellationTokenSource cts = default)
        {
            if (string.IsNullOrEmpty(path)) return null;
            if (cts is not null && cts.Token.IsCancellationRequested) return null;

            // 1. Check cache
            if (!forceRefresh && Cache.TryGetValue(path, out Cache.CacheData cachedTuple))
            {
                bool valid = true;

                // TTL check
                if (useTtlCache && DateTime.UtcNow - cachedTuple.Fetched >= Cache.CacheTTL) valid = false;

                // SHA validation
                if (useShaValidation && valid)
                {
                    Entry validated = await GetFromCacheValidatedAsync(path);
                    if (validated == null) valid = false;
                }

                if (valid) return cachedTuple.Entry;
            }

            // 2. Fetch contents from GitHub safely
            IReadOnlyList<RepositoryContent> contents = await Helpers.ExecuteGitHubActionSafeAsync(async () =>
                await Program.GitHub.Client.Repository.Content.GetAllContentsByRef(
                    Repository.Owner,
                    GitHub.Repository.Name,
                    path,
                    GitHub.Repository.Branch.Name).ConfigureAwait(false)
            );

            if (contents == null)
            {
                Cache.Remove(path);
                Program.Log?.Write(LogEventLevel.Warning, $"GetInfoAsync: Failed to fetch contents for '{path}' due to network, rate limit, or server issues.");
                return null;
            }

            RepositoryContent firstContent = contents.FirstOrDefault();
            if (firstContent == null) return null;

            // 3. Build Entry for current path
            Entry entry = await Entry.FromRepositoryContent(firstContent, path);

            // 4. Recursively process children if requested
            if (recursive && entry.Type == EntryType.Dir && maxDepth != 0)
            {
                List<Entry> children = [];

                foreach (RepositoryContent item in contents)
                {
                    Entry childEntry = null;

                    // Try cached child if allowed
                    if (useShaValidation && Cache.TryGetValue(item.Path, out Cache.CacheData cachedChildTuple))
                    {
                        Entry cachedChild = cachedChildTuple.Entry;

                        // Safe fetch latest commit for SHA validation
                        GitHubCommit latest = await Helpers.ExecuteGitHubActionSafeAsync(async () =>
                            (await Program.GitHub.Client.Repository.Commit.GetAll(Repository.Owner, GitHub.Repository.Name, new CommitRequest { Path = item.Path }))
                            .FirstOrDefault()
                        );

                        if (latest != null && cachedChild.CommitSha == latest.Sha) childEntry = cachedChild;
                    }

                    // Fetch fresh child if needed
                    if (childEntry == null)
                    {
                        if (item.Type == Octokit.ContentType.Dir)
                            childEntry = await GetInfoAsync(item.Path, true, useShaValidation, useTtlCache, forceRefresh, maxDepth - 1, cts);
                        else
                            childEntry = await Entry.FromRepositoryContent(item, item.Path);
                    }

                    if (childEntry != null) children.Add(childEntry);
                }

                entry.Children = children;
            }

            // 5. Store in cache
            Cache.Add(entry);

            return entry;
        }

        /// <summary>
        /// Quick shallow lookup of a single path with TTL caching.
        /// </summary>
        public static Task<Entry> GetInfoFastAsync(string path, CancellationTokenSource cts = default) => GetInfoAsync(path, recursive: false, useShaValidation: false, useTtlCache: true, forceRefresh: false, maxDepth: 0, cts: cts);

        /// <summary>
        /// Force refresh the entry with optional recursion.
        /// </summary>
        public static Task<Entry> GetInfoRefreshAsync(string path, bool recursive = true, int maxDepth = 20, CancellationTokenSource cts = default) => GetInfoAsync(path, recursive, useShaValidation: true, useTtlCache: false, forceRefresh: true, maxDepth: maxDepth, cts: cts);

        private static async Task<Entry> GetEntryCachedAsync(string path, bool forceRefresh = false, int maxDepth = 5, CancellationTokenSource cts = null)
        {
            if (string.IsNullOrEmpty(path)) return null;

            cts ??= new();
            if (cts.Token.IsCancellationRequested) return null;

            // Return cached entry if fresh
            if (!forceRefresh && Cache.TryGetValue(path, out Cache.CacheData cachedTuple) && DateTime.UtcNow - cachedTuple.Fetched < Cache.CacheTTL)
            {
                return cachedTuple.Entry;
            }

            Entry entry = null;

            try
            {
                // Safe fetch directory contents
                IReadOnlyList<RepositoryContent> contents = await Helpers.ExecuteGitHubActionSafeAsync(async () =>
                    await Program.GitHub.Client.Repository.Content.GetAllContentsByRef(
                        Repository.Owner,
                        GitHub.Repository.Name,
                        path,
                        GitHub.Repository.Branch.Name
                    )
                );

                if (contents != null && contents.Count > 0)
                {
                    // Directory entry
                    entry = new()
                    {
                        Path = path,
                        Type = EntryType.Dir,
                        FetchedAt = DateTime.UtcNow,
                        Children = []
                    };

                    if (maxDepth > 0)
                    {
                        foreach (var item in contents)
                        {
                            if (cts.Token.IsCancellationRequested) return null;
                            Entry childEntry = await GetEntryCachedAsync(item.Path, forceRefresh, maxDepth - 1, cts);
                            if (childEntry != null) entry.Children.Add(childEntry);
                        }
                    }
                }
            }
            catch (Octokit.NotFoundException)
            {
                // Try as a file if directory not found
                IReadOnlyList<RepositoryContent> fileContents = await Helpers.ExecuteGitHubActionSafeAsync(async () =>
                    await Program.GitHub.Client.Repository.Content.GetAllContentsByRef(
                        Repository.Owner,
                        GitHub.Repository.Name,
                        path,
                        GitHub.Repository.Branch.Name
                    )
                );

                if (fileContents != null && fileContents.Count == 1)
                {
                    entry = new()
                    {
                        Path = path,
                        Type = EntryType.File,
                        Content = fileContents[0],
                        FetchedAt = DateTime.UtcNow
                    };
                }
            }

            // Store in cache
            if (entry != null) Cache.Add(entry);

            return entry;
        }

        private static async Task<List<Entry>> GetEntriesCachedAsync(string path, bool forceRefresh = false, CancellationTokenSource cts = null)
        {
            cts ??= new();
            if (cts.Token.IsCancellationRequested) return null;

            var result = new List<Entry>();

            // Try to get cached entry
            Entry entry = await GetEntryCachedAsync(path, forceRefresh, maxDepth: 0, cts);

            // If not cached, create a directory entry manually
            entry ??= new()
                {
                    Path = path,
                    Type = EntryType.Dir,
                    Content = null,
                    FetchedAt = DateTime.UtcNow
                };

            // Add the directory itself
            result.Add(entry);

            // Fetch children safely from GitHub
            IReadOnlyList<RepositoryContent> contents = await Helpers.ExecuteGitHubActionSafeAsync(async () =>
                await Program.GitHub.Client.Repository.Content.GetAllContentsByRef(
                    Repository.Owner,
                    GitHub.Repository.Name,
                    path,
                    GitHub.Repository.Branch.Name
                )
            );

            if (contents == null)
            {
                Program.Log?.Write(LogEventLevel.Warning, $"GetEntriesCachedAsync: Failed to fetch contents for '{path}' due to network, rate limit, or server issues.");
                return result; // fallback to containing directory only
            }

            foreach (var item in contents)
            {
                if (cts.Token.IsCancellationRequested) break;

                Entry childEntry = new()
                {
                    Path = item.Path,
                    Type = item.Type == ContentType.Dir ? EntryType.Dir : EntryType.File,
                    Content = item,
                    FetchedAt = DateTime.UtcNow
                };

                result.Add(childEntry);

                // Recurse if child is a directory
                if (childEntry.Type == EntryType.Dir)
                {
                    var subChildren = await GetEntriesCachedAsync(item.Path, forceRefresh, cts);
                    if (subChildren != null && subChildren.Count > 1)
                    {
                        result.AddRange(subChildren.Skip(1)); // skip subdirectory itself, already added
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Recursively collects directory and/or file entries starting from the specified path, reporting progress.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="includeFiles"></param>
        /// <param name="includeDirs"></param>
        /// <param name="output"></param>
        /// <param name="cts"></param>
        /// <param name="reportProgress"></param>
        /// <param name="maxDepth"></param>
        /// <param name="currentDepth"></param>
        /// <returns></returns>
        private static async Task CollectEntriesRecursive(string path, bool includeFiles, bool includeDirs, List<string> output, CancellationTokenSource cts, Action<int> reportProgress, int maxDepth, int currentDepth)
        {
            if (cts.IsCancellationRequested || (maxDepth >= 0 && currentDepth > maxDepth)) return;

            // Get directory entry from cache (or fetch fresh)
            Entry entry = await GetEntryCachedAsync(path, forceRefresh: false, maxDepth: 1, cts);
            if (entry?.Children == null) return;

            int total = entry.Children.Count;
            int processed = 0;

            foreach (Entry child in entry.Children)
            {
                if (cts is not null && cts.Token.IsCancellationRequested) return;

                if ((includeFiles && child.Type == EntryType.File) || (includeDirs && child.Type == EntryType.Dir))
                {
                    output.Add(child.Path);
                }

                if (child.Type == EntryType.Dir) await CollectEntriesRecursive(child.Path, includeFiles, includeDirs, output, cts, reportProgress, maxDepth, currentDepth + 1);

                processed++;
                reportProgress?.Invoke(total > 0 ? (int)((processed * 100L) / total) : 100);
            }
        }
    }
}