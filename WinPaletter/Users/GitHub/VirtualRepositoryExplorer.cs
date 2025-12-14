using Octokit;
using Serilog.Events;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
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
        /// Provides a mapping of directory names to their associated repository contents, using case-insensitive string
        /// comparison.
        /// </summary>
        /// <remarks>Directory names are compared using ordinal, case-insensitive semantics. This ensures
        /// that lookups for directory names are not affected by letter casing.</remarks>
        private static readonly Dictionary<string, List<RepositoryContent>> DirectoryMap = new(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Provides a mapping between folder names and their corresponding sizes in bytes, using case-insensitive
        /// string comparison.
        /// </summary>
        /// <remarks>This dictionary uses <see cref="StringComparer.OrdinalIgnoreCase"/> to ensure that
        /// folder names are compared without regard to case. This is useful when folder names may vary in casing but
        /// should be treated equivalently.</remarks>
        public static readonly Dictionary<string, long> FolderSizeMap = new(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Provides a thread-safe cache for directory contents and their associated SHA values, keyed by directory
        /// path.
        /// </summary>
        /// <remarks>The cache stores the contents of each directory as an immutable list, along with the
        /// corresponding SHA identifier. This enables efficient retrieval of previously accessed directory data and
        /// reduces redundant repository queries. The cache is shared across all usages within the application and is
        /// safe for concurrent access.</remarks>
        private static readonly ConcurrentDictionary<string, (IReadOnlyList<RepositoryContent> Contents, string Sha)> DirectoryCache = new();

        /// <summary>
        /// Stores cached entries along with their fetch timestamps for thread-safe access within the application.
        /// </summary>
        /// <remarks>This dictionary enables efficient retrieval and expiration of cached data by
        /// associating each entry with the time it was fetched. Access to the cache is safe for concurrent read and
        /// write operations from multiple threads.</remarks>
        private static readonly ConcurrentDictionary<string, (Entry entry, DateTime fetched)> _cache = new();

        /// <summary>
        /// Provides a thread-safe cache for storing computed SHA and MD5 hash values associated with string keys.
        /// </summary>
        /// <remarks>This dictionary enables efficient retrieval of previously computed hash values,
        /// reducing redundant calculations in concurrent scenarios. Access to the cache is safe for multiple
        /// threads.</remarks>
        private static readonly ConcurrentDictionary<string, string> ShaMd5Cache = new();

        /// <summary>
        /// SemaphoreSlim to limit concurrent GitHub API requests for repository data fetching.
        /// </summary>
        private static readonly SemaphoreSlim _semaphore = new(5);

        /// <summary>
        /// Specifies the duration for which cached data remains valid before expiration.
        /// </summary>
        /// <remarks>This value determines the time-to-live (TTL) for items stored in the cache. After
        /// this period elapses, cached entries are considered stale and may be refreshed or removed. Adjust this value
        /// to balance cache freshness and performance based on application requirements.</remarks>
        private static readonly TimeSpan CacheTTL = TimeSpan.FromSeconds(30);

        /// <summary>
        /// Gets the GitHub login name of the current user.
        /// </summary>
        private static string _owner => User.GitHub.Login;

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
                items = await GetContentsCachedAsync(path);   // cached fetch
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

        /// <summary>
        /// Creates an internal lookup table mapping each directory path to its immediate children.
        /// </summary>
        private static void BuildDirectoryMap()
        {
            Program.Log?.Write(LogEventLevel.Information, "BuildDirectoryMap started");

            DirectoryMap.Clear();

            foreach ((Entry, DateTime) tuple in _cache.Values)
            {
                Entry entry = tuple.Item1;
                if (entry.Content == null) continue; // skip entries without RepositoryContent

                string parent = GetParent(entry.Path);
                if (!DirectoryMap.ContainsKey(parent)) DirectoryMap[parent] = [];

                DirectoryMap[parent].Add(entry.Content);
            }

            Program.Log?.Write(LogEventLevel.Information, $"BuildDirectoryMap completed: {DirectoryMap.Count} directory entries");
        }

        /// <summary>
        /// Computes and stores the total byte size of each directory based on its contents.
        /// </summary>
        /// <param name="path">The directory path to evaluate.</param>
        private static long GetFoldersSize(string path, bool addToFolderSizeMap = true)
        {
            Program.Log?.Write(LogEventLevel.Information, $"BuildFolderSizes started for '{path}'");

            if (!DirectoryMap.ContainsKey(path))
            {
                Program.Log?.Write(LogEventLevel.Information, $"BuildFolderSizes: no entries for '{path}'");
                return 0;
            }

            long total = 0;

            foreach (RepositoryContent entry in DirectoryMap[path])
            {
                if (entry.Type == Octokit.ContentType.Dir)
                {
                    total += GetFoldersSize(entry.Path);
                }
                else
                {
                    total += entry.Size;
                }
            }

            if (addToFolderSizeMap) FolderSizeMap[path] = total;

            Program.Log?.Write(LogEventLevel.Information, $"BuildFolderSizes completed for '{path}', size={total}");

            return total;
        }

        /// <summary>
        /// Retrieves a cached Entry for the specified path and validates it against the latest commits.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private static async Task<Entry> GetFromCacheValidatedAsync(string path)
        {
            if (!_cache.TryGetValue(path, out (Entry, DateTime) cached)) return null;

            Entry cachedEntry = cached.Item1;

            try
            {
                // Fetch latest commits for this path
                GitHubCommit latestCommit = (await Program.GitHub.Client.Repository.Commit.GetAll(_owner, GitHub.Repository.repositoryName, new CommitRequest { Path = path })).FirstOrDefault();

                // File entry validation
                if (cachedEntry.Type == EntryType.File)
                {
                    if (latestCommit != null && latestCommit.Sha == cachedEntry.CommitSha) return cachedEntry;

                    _cache.TryRemove(path, out _);
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
                        if (!_cache.TryGetValue(child.Path, out (Entry, DateTime) cachedChildTuple) || (latestShas.TryGetValue(child.Path, out string latestSha) && cachedChildTuple.Item1.CommitSha != latestSha))
                        {
                            _cache.TryRemove(path, out _);
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
        public static async Task<Entry> GetInfoAsync(string path, bool recursive = true, bool useShaValidation = true, bool useTtlCache = true, bool forceRefresh = false, int maxDepth = 20, CancellationToken token = default)
        {
            if (string.IsNullOrEmpty(path)) return null;
            token.ThrowIfCancellationRequested();

            // 1. Check cache
            if (!forceRefresh && _cache.TryGetValue(path, out (Entry, DateTime) cachedTuple))
            {
                bool valid = true;

                // TTL check
                if (useTtlCache && DateTime.UtcNow - cachedTuple.Item2 >= CacheTTL) valid = false;

                // SHA validation
                if (useShaValidation && valid)
                {
                    Entry validated = await GetFromCacheValidatedAsync(path);
                    if (validated == null) valid = false;
                }

                if (valid) return cachedTuple.Item1;
            }

            // 2. Fetch contents from GitHub
            IReadOnlyList<RepositoryContent> contents;
            try
            {
                contents = await Program.GitHub.Client.Repository.Content.GetAllContentsByRef(_owner, GitHub.Repository.repositoryName, path, GitHub.Repository.branch);
            }
            catch
            {
                _cache.TryRemove(path, out _);
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
                    if (useShaValidation && _cache.TryGetValue(item.Path, out (Entry, DateTime) cachedChildTuple))
                    {
                        Entry cachedChild = cachedChildTuple.Item1;
                        GitHubCommit latest = (await Program.GitHub.Client.Repository.Commit.GetAll(_owner, GitHub.Repository.repositoryName, new CommitRequest { Path = item.Path })).FirstOrDefault();

                        if (latest != null && cachedChild.CommitSha == latest.Sha) childEntry = cachedChild;
                    }

                    // Fetch fresh child if needed
                    if (childEntry == null)
                    {
                        if (item.Type == Octokit.ContentType.Dir)
                            childEntry = await GetInfoAsync(item.Path, true, useShaValidation, useTtlCache, forceRefresh, maxDepth - 1, token);
                        else
                            childEntry = await Entry.FromRepositoryContent(item, item.Path);
                    }

                    if (childEntry != null) children.Add(childEntry);
                }
                entry.Children = children;
            }

            // 5. Store in cache
            _cache[entry.Path] = (entry, DateTime.UtcNow);

            return entry;
        }

        /// <summary>
        /// Quick shallow lookup of a single path with TTL caching.
        /// </summary>
        public static Task<Entry> GetInfoFastAsync(string path, CancellationToken token = default) => GetInfoAsync(path, recursive: false, useShaValidation: false, useTtlCache: true, forceRefresh: false, maxDepth: 0, token: token);

        /// <summary>
        /// Accurate full recursive fetch with SHA validation.
        /// </summary>
        public static Task<Entry> GetInfoAccurateAsync(string path, int maxDepth = 20, CancellationToken token = default) => GetInfoAsync(path, recursive: true, useShaValidation: true, useTtlCache: false, forceRefresh: true, maxDepth: maxDepth, token: token);

        /// <summary>
        /// Force refresh the entry with optional recursion.
        /// </summary>
        public static Task<Entry> GetInfoRefreshAsync(string path, bool recursive = true, int maxDepth = 20, CancellationToken token = default) => GetInfoAsync(path, recursive, useShaValidation: true, useTtlCache: false, forceRefresh: true, maxDepth: maxDepth, token: token);

        /// <summary>
        /// Recursively retrieves repository entry information for the specified path with caching and TTL validation.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="forceRefresh"></param>
        /// <param name="maxDepth"></param>
        /// <param name="cts"></param>
        /// <returns></returns>
        private static async Task<Entry> GetEntryCachedAsync(string path, bool forceRefresh = false, int maxDepth = 5, CancellationTokenSource cts = null)
        {
            if (string.IsNullOrEmpty(path)) return null;

            cts ??= new();
            cts?.Token.ThrowIfCancellationRequested();

            // Return cached entry if fresh
            if (!forceRefresh && _cache.TryGetValue(path, out (Entry, DateTime) cachedTuple) && DateTime.UtcNow - cachedTuple.Item2 < CacheTTL)
            {
                return cachedTuple.Item1;
            }

            IReadOnlyList<RepositoryContent> contents;
            try
            {
                contents = await Program.GitHub.Client.Repository.Content.GetAllContentsByRef(_owner, GitHub.Repository.repositoryName, path, GitHub.Repository.branch);
            }
            catch (Octokit.NotFoundException)
            {
                return null; // Path does not exist
            }

            RepositoryContent firstItem = contents.FirstOrDefault();
            if (firstItem == null) return null;

            Entry entry = await Entry.FromRepositoryContent(firstItem, path);
            entry.FetchedAt = DateTime.UtcNow;

            // Recursively process directories
            if (entry.Type == EntryType.Dir && maxDepth > 0)
            {
                List<Entry> children = [];
                foreach (RepositoryContent item in contents)
                {
                    Entry child = await GetEntryCachedAsync(item.Path, forceRefresh, maxDepth - 1, cts);
                    if (child != null) children.Add(child);
                }
                entry.Children = children;
            }

            // Store in _infoCache
            _cache[entry.Path] = (entry, DateTime.UtcNow);
            return entry;
        }

        /// <summary>
        /// Retrieves repository entry information for the specified path with caching and TTL validation.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="forceRefresh"></param>
        /// <param name="cts"></param>
        /// <returns></returns>
        private static async Task<List<Entry>> GetEntriesCachedAsync(string path, bool forceRefresh = false, CancellationTokenSource cts = null)
        {
            Entry entry = await GetEntryCachedAsync(path, forceRefresh, 1, cts);
            return (entry?.Children ?? []) as List<Entry>;
        }

        /// <summary>
        /// Retrieves GitHub directory contents with SHA validation and automatic cache refresh
        /// when upstream data changes.
        /// </summary>
        /// <param name="path">Repository path to retrieve.</param>
        /// <returns>A read-only list of repository contents.</returns>
        private static async Task<IReadOnlyList<RepositoryContent>> GetContentsCachedAsync(string path)
        {
            if (DirectoryCache.TryGetValue(path, out (IReadOnlyList<RepositoryContent>, string) cached))
            {
                try
                {
                    IReadOnlyList<RepositoryContent> currentContents = await Program.GitHub.Client.Repository.Content.GetAllContentsByRef(_owner, GitHub.Repository.repositoryName, path, GitHub.Repository.branch);
                    string currentSha = currentContents.FirstOrDefault()?.Sha ?? string.Empty;

                    if (cached.Item2 == currentSha) return cached.Item1;

                    DirectoryCache[path] = (currentContents, currentSha);
                    return currentContents;
                }
                catch
                {
                    return cached.Item1;
                }
            }

            IReadOnlyList<RepositoryContent> contents = await Program.GitHub.Client.Repository.Content.GetAllContentsByRef(_owner, GitHub.Repository.repositoryName, path, GitHub.Repository.branch);
            string sha = contents.FirstOrDefault()?.Sha ?? string.Empty;
            DirectoryCache[path] = (contents, sha);
            return contents;
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
        }

        /// <summary>
        /// Stores an Entry and its children recursively in the cache with the current timestamp.
        /// </summary>
        /// <param name="entry"></param>
        private static void StoreEntryInCache(Entry entry)
        {
            if (entry == null) return;

            entry.FetchedAt = DateTime.UtcNow; // TTL tracking

            _cache[entry.Path] = (entry, DateTime.UtcNow);

            if (entry.Type == EntryType.Dir && entry.Children != null)
            {
                foreach (Entry child in entry.Children) StoreEntryInCache(child);
            }
        }

        /// <summary>
        /// Clears cached entries for the specified path and its subpaths.
        /// </summary>
        /// <param name="path"></param>
        private static void ClearEntryCache(string path = null)
        {
            if (path == null)
            {
                _cache.Clear();
                return;
            }

            _cache.TryRemove(path, out _);
            foreach (string key in _cache.Keys.Where(k => k.StartsWith(path + "/")).ToList()) _cache.TryRemove(key, out _);
        }

        /// <summary>
        /// Clears all cached data in the FileSystem, including entries, directory maps, folder sizes, and SHA/MD5 caches.
        /// Intended to be called before switching branches to prevent stale data usage.
        /// </summary>
        public static void ClearAllCaches()
        {
            // Clear main entry cache
            _cache.Clear();

            // Clear directory map and folder size map
            DirectoryMap.Clear();
            FolderSizeMap.Clear();

            // Clear directory content cache
            DirectoryCache.Clear();

            // Clear SHA/MD5 hash cache
            ShaMd5Cache.Clear();

            // Optional: log the cache clearing
            Program.Log?.Write(LogEventLevel.Information, "All FileSystem caches have been cleared.");
        }

        /// <summary>
        /// Updates internal directory maps when an entry is added or modified.
        /// </summary>
        /// <param name="directory"></param>
        /// <param name="entry"></param>
        private static void UpdateDirectoryMaps(string directory, Entry entry)
        {
            directory = NormalizePath(directory);

            // Get or create the directory list
            if (!DirectoryMap.TryGetValue(directory, out List<RepositoryContent> list))
            {
                list = [];
                DirectoryMap[directory] = list;
            }

            if (entry.Type == EntryType.File && entry.Content != null)
            {
                // Check if file already exists and remove it
                RepositoryContent existing = list.FirstOrDefault(c => string.Equals(c.Path, entry.Path, StringComparison.OrdinalIgnoreCase));
                if (existing != null)
                {
                    // Subtract old file size from folder size
                    FolderSizeMap[directory] = Math.Max(0, FolderSizeMap.TryGetValue(directory, out long size) ? size - existing.Size : 0);

                    list.Remove(existing);
                }

                // Add the new file
                list.Add(entry.Content);

                // Add new file size
                FolderSizeMap[directory] = FolderSizeMap.TryGetValue(directory, out long current) ? current + entry.Content.Size : entry.Content.Size;
            }

            if (entry.Type == EntryType.Dir)
            {
                // Ensure FolderSizeMap entry exists
                if (!FolderSizeMap.ContainsKey(directory)) FolderSizeMap[directory] = 0;
            }

            // Update DirectoryCache
            DirectoryCache[directory] = (list.AsReadOnly(), entry.Content?.Sha);
        }

        /// <summary>
        /// Removes an entry from internal directory maps.
        /// </summary>
        /// <param name="directory"></param>
        /// <param name="entry"></param>
        private static void RemoveDirectoryMaps(string directory, Entry entry)
        {
            directory = directory?.TrimEnd('/') ?? string.Empty;

            if (entry.Type == EntryType.File)
            {
                if (DirectoryMap.TryGetValue(directory, out List<RepositoryContent> list))
                {
                    // Find the file to remove and adjust the folder size
                    int removed = list.RemoveAll(c => string.Equals(c.Path, entry.Path, StringComparison.OrdinalIgnoreCase));
                    if (removed > 0)
                    {
                        FolderSizeMap[directory] = Math.Max(0, FolderSizeMap.TryGetValue(directory, out long size) ? size - (entry.Content?.Size ?? 0) : 0);
                    }
                }
            }

            if (entry.Type == EntryType.Dir)
            {
                // Optionally, remove the folder size if directory is empty
                FolderSizeMap.Remove(directory);
            }

            // Remove from DirectoryCache
            DirectoryCache.TryRemove(directory, out _);
        }

        /// <summary>
        /// Builds a dictionary of changes from the FileSystem cache.
        /// Key = repository path, Value = new content (null if deleted)
        /// </summary>
        public static Dictionary<string, string> BuildChangesFromCache()
        {
            var changes = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            foreach (var kv in FileSystem._cache)
            {
                Entry entry = kv.Value.entry;

                if (entry.Type == EntryType.File)
                {
                    string currentContent = entry.Content?.Content;
                    string lastSha = entry.CommitSha;

                    if (currentContent == null)
                    {
                        // File removed from UI
                        changes[entry.Path] = null;
                    }
                    else
                    {
                        string contentSha = ComputeSha1(currentContent);

                        // New file or modified content
                        if (string.IsNullOrEmpty(lastSha) || lastSha != contentSha)
                            changes[entry.Path] = currentContent;
                    }
                }
                else if (entry.Type == EntryType.Dir && entry.Children != null)
                {
                    // Recurse into children
                    foreach (Entry child in entry.Children)
                        RecurseEntry(child, changes);
                }
            }

            return changes;
        }

        private static void RecurseEntry(Entry entry, Dictionary<string, string> changes)
        {
            if (entry.Type == EntryType.File)
            {
                string currentContent = entry.Content?.Content;
                string lastSha = entry.CommitSha;

                if (currentContent == null)
                {
                    changes[entry.Path] = null;
                }
                else
                {
                    string contentSha = ComputeSha1(currentContent);
                    if (string.IsNullOrEmpty(lastSha) || lastSha != contentSha)
                        changes[entry.Path] = currentContent;
                }
            }
            else if (entry.Type == EntryType.Dir && entry.Children != null)
            {
                foreach (Entry child in entry.Children)
                    RecurseEntry(child, changes);
            }
        }

        private static string ComputeSha1(string content)
        {
            if (string.IsNullOrEmpty(content)) return string.Empty;

            using (SHA1 sha1 = SHA1.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(content);
                byte[] hash = sha1.ComputeHash(bytes);
                return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
            }
        }
    }
}