using Octokit;
using Serilog.Events;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        /// <summary>
        /// Gets the GitHub login name of the current user.
        /// </summary>
        public static string _owner => User.GitHub.Login;

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
            if (cts.IsCancellationRequested) return;
            if (maxDepth >= 0 && currentDepth > maxDepth) return;

            IReadOnlyList<RepositoryContent> items;

            try
            {
                await _semaphore.WaitAsync(cts.Token); // wait for slot
                items = await Cache.GetDirectoryRecursiveAsync(path);   // cached fetch
            }
            catch
            {
                return; // ignore failures
            }
            finally
            {
                _semaphore.Release();
            }

            List<string> subDirs = [];

            foreach (RepositoryContent entry in items)
            {
                if (cts.IsCancellationRequested) return;

                output.Add(entry);
                reportProgress?.Invoke(entry);

                if (entry.Type == Octokit.ContentType.Dir) subDirs.Add(entry.Path);
            }

            IEnumerable<Task> tasks = subDirs.Select(subDir => FetchRecursive(subDir, output, reportProgress, cts, maxDepth, currentDepth + 1));

            await Task.WhenAll(tasks);
        }

        /// <summary>
        /// Recursively enumerates directories or files, depending on <paramref name="onlyDirs"/>,
        /// and reports incremental progress.
        /// </summary>
        /// <param name="path">The GitHub path to enumerate.</param>
        /// <param name="output">A list receiving matching directory or file paths.</param>
        /// <param name="cts">Cancellation token source.</param>
        /// <param name="onlyDirs">If true, only directories are returned; otherwise only files.</param>
        /// <param name="reportProgress">Optional progress reporter (0-100%).</param>
        /// <param name="maxDepth">Maximum recursion depth; -1 for unlimited.</param>
        /// <param name="currentDepth">Internal recursion depth counter.</param>
        /// <returns>A task representing the recursive enumeration.</returns>
        private static async Task FetchRecursive(string path, List<string> output, CancellationTokenSource cts, bool onlyDirs, Action<int> reportProgress = null, int maxDepth = -1, int currentDepth = 0)
        {
            cts ??= new();
            if (maxDepth >= 0 && currentDepth > maxDepth) return;

            IReadOnlyList<RepositoryContent> items;
            try { items = await Program.GitHub.Client.Repository.Content.GetAllContentsByRef(_owner, GitHub.Repository.repositoryName, path, GitHub.Repository.branch); }
            catch { return; }

            int total = items.Count;
            int processed = 0;
            foreach (RepositoryContent entry in items)
            {
                cts?.Token.ThrowIfCancellationRequested();

                if ((onlyDirs && entry.Type == Octokit.ContentType.Dir) || (!onlyDirs && entry.Type != Octokit.ContentType.Dir))
                    output.Add(entry.Path);

                if (entry.Type == Octokit.ContentType.Dir)
                    await FetchRecursive(entry.Path, output, cts, onlyDirs, reportProgress, maxDepth, currentDepth + 1);

                processed++;
                reportProgress?.Invoke((int)((processed * 100L) / total));
            }
        }

        public static async Task<RepositoryContent> GetRepositoryContentAsync(string path)
        {
            if (string.IsNullOrEmpty(path)) return null;

            try
            {
                // Get the contents of the parent directory
                string parent = Path.GetDirectoryName(path)?.Replace("\\", "/") ?? "";
                string name = Path.GetFileName(path);

                IReadOnlyList<RepositoryContent> parentContents =
                    await Program.GitHub.Client.Repository.Content.GetAllContentsByRef(
                        _owner, Repository.repositoryName, parent, Repository.branch);

                // Find the exact item in the parent folder
                return parentContents.FirstOrDefault(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            }
            catch (Octokit.NotFoundException)
            {
                return null; // path does not exist
            }
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
                // Fetch latest commits for this path
                GitHubCommit latestCommit = (await Program.GitHub.Client.Repository.Commit.GetAll(_owner, GitHub.Repository.repositoryName, new CommitRequest { Path = path })).FirstOrDefault();

                // File entry validation
                if (cachedEntry.Type == EntryType.File)
                {
                    if (latestCommit != null && latestCommit.Sha == cachedEntry.CommitSha) return cachedEntry;

                    Cache.Remove(path);
                    return null;
                }

                // Directory entry validation
                if (cachedEntry.Type == EntryType.Dir && cachedEntry.Children != null)
                {
                    List<string> childPaths = [.. cachedEntry.Children.Select(c => c.Path)];
                    Dictionary<string, string> latestShas = new(StringComparer.OrdinalIgnoreCase);

                    // Fetch latest SHAs for all children
                    foreach (string childPath in childPaths)
                    {
                        GitHubCommit latest = (await Program.GitHub.Client.Repository.Commit.GetAll(_owner, GitHub.Repository.repositoryName, new CommitRequest { Path = childPath })).FirstOrDefault();
                        if (latest != null) latestShas[childPath] = latest.Sha;
                    }

                    // Compare with cached SHAs
                    foreach (Entry child in cachedEntry.Children)
                    {
                        if (!Cache.TryGetValue(child.Path, out Cache.CacheData cachedChildTuple) || (latestShas.TryGetValue(child.Path, out string latestSha) && cachedChildTuple.Entry.CommitSha != latestSha))
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
                // fallback to cache if API fails
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
            cts?.Token.ThrowIfCancellationRequested();

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

            // 2. Fetch contents from GitHub
            IReadOnlyList<RepositoryContent> contents;
            try
            {
                contents = await Program.GitHub.Client.Repository.Content.GetAllContentsByRef(_owner, GitHub.Repository.repositoryName, path, GitHub.Repository.branch);
            }
            catch
            {
                Cache.Remove(path);
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
                        GitHubCommit latest = (await Program.GitHub.Client.Repository.Commit.GetAll(_owner, GitHub.Repository.repositoryName, new CommitRequest { Path = item.Path })).FirstOrDefault();

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
            cts.Token.ThrowIfCancellationRequested();

            // Return cached entry if fresh
            if (!forceRefresh && Cache.TryGetValue(path, out Cache.CacheData cachedTuple) &&
                DateTime.UtcNow - cachedTuple.Fetched < Cache.CacheTTL)
            {
                return cachedTuple.Entry;
            }

            Entry entry = null;

            try
            {
                // Attempt to get directory contents
                IReadOnlyList<RepositoryContent> contents = await Program.GitHub.Client.Repository.Content.GetAllContentsByRef(_owner, GitHub.Repository.repositoryName, path, GitHub.Repository.branch);

                // If the path is empty or contains multiple children → directory
                entry = new Entry
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
                        cts.Token.ThrowIfCancellationRequested();
                        Entry childEntry = await GetEntryCachedAsync(item.Path, forceRefresh, maxDepth - 1, cts);
                        if (childEntry != null) entry.Children.Add(childEntry);
                    }
                }
            }
            catch (Octokit.NotFoundException)
            {
                // If not found as directory, try as file
                try
                {
                    var fileContents = await Program.GitHub.Client.Repository.Content.GetAllContentsByRef(_owner, GitHub.Repository.repositoryName, path, GitHub.Repository.branch);

                    if (fileContents.Count == 1)
                    {
                        entry = new Entry
                        {
                            Path = path,
                            Type = EntryType.File,
                            Content = fileContents[0],
                            FetchedAt = DateTime.UtcNow
                        };
                    }
                }
                catch (Octokit.NotFoundException)
                {
                    // Path does not exist
                    return null;
                }
            }

            // Store in cache
            if (entry != null) Cache.Add(entry);

            return entry;
        }

        private static async Task<List<Entry>> GetEntriesCachedAsync(string path, bool forceRefresh = false, CancellationTokenSource cts = null)
        {
            cts ??= new();
            cts.Token.ThrowIfCancellationRequested();

            var result = new List<Entry>();

            // Try to get cached entry
            Entry entry = await GetEntryCachedAsync(path, forceRefresh, maxDepth: 0, cts);

            // If not cached, create a directory entry manually
            if (entry == null)
            {
                entry = new Entry
                {
                    Path = path,
                    Type = EntryType.Dir,
                    Content = null,
                    FetchedAt = DateTime.UtcNow
                };
            }

            // Add the directory itself
            result.Add(entry);

            // Fetch children from GitHub
            IReadOnlyList<RepositoryContent> contents;
            try
            {
                contents = await Program.GitHub.Client.Repository.Content.GetAllContentsByRef(_owner, GitHub.Repository.repositoryName, path, GitHub.Repository.branch);
            }
            catch (Octokit.NotFoundException)
            {
                return result; // directory exists but empty
            }

            foreach (var item in contents)
            {
                var childEntry = new Entry
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
                    result.AddRange(subChildren.Skip(1)); // skip subdirectory itself, already added
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
                cts?.Token.ThrowIfCancellationRequested();

                if ((includeFiles && child.Type == EntryType.File) || (includeDirs && child.Type == EntryType.Dir))
                {
                    output.Add(child.Path);
                }

                if (child.Type == EntryType.Dir) await CollectEntriesRecursive(child.Path, includeFiles, includeDirs, output, cts, reportProgress, maxDepth, currentDepth + 1);

                processed++;
                reportProgress?.Invoke(total > 0 ? (int)((processed * 100L) / total) : 100);
            }
            //}
        }
    }
}