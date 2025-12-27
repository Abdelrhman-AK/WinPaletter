using Octokit;
using Serilog.Events;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static WinPaletter.GitHub.FileSystem;

namespace WinPaletter.GitHub
{
    /// <summary>
    /// Provides caching utilities for repository entries, directories, and files with TTL, SHA/MD5 validation, 
    /// folder size tracking, and change detection.
    /// </summary>
    public static class Cache
    {
        /// <summary>
        /// Represents a cached entry with its last fetched timestamp.
        /// </summary>
        public class CacheData
        {
            /// <summary>
            /// The repository entry being cached (file or directory).
            /// </summary>
            public Entry Entry { get; set; }

            /// <summary>
            /// The UTC timestamp when this entry was fetched or cached.
            /// </summary>
            public DateTime Fetched { get; set; }
        }

        public static List<CacheData> Values => [.. _cache.Select(c => c.Value)];

        /// <summary>
        /// Thread-safe cache for repository entries with fetch timestamp.
        /// </summary>
        private static readonly ConcurrentDictionary<string, CacheData> _cache = new();

        /// <summary>
        /// Thread-safe cache for directory contents and their SHA.
        /// </summary>
        private static readonly ConcurrentDictionary<string, (IReadOnlyList<RepositoryContent> contents, string sha)> DirectoryCache = new();

        /// <summary>
        /// Cached folder sizes for fast repeated access.
        /// </summary>
        private static readonly ConcurrentDictionary<string, long> FolderSizeMap = new();

        /// <summary>
        /// Thread-safe cache for computed SHA/MD5 hash values keyed by string.
        /// </summary>
        private static readonly ConcurrentDictionary<string, string> ShaMd5Cache = new();

        /// <summary>
        /// TTL duration for cached entries.
        /// </summary>
        public static readonly TimeSpan CacheTTL = TimeSpan.FromSeconds(30);

        private static string _lastRepoTreeSha;

        private static async Task<bool> RepositoryTreeChangedAsync()
        {
            var reference = await Program.GitHub.Client.Git.Reference.Get(_owner, GitHub.Repository.repositoryName, $"heads/{GitHub.Repository.branch}");
            string currentSha = reference.Object.Sha;

            if (_lastRepoTreeSha == currentSha) return false;

            _lastRepoTreeSha = currentSha;
            DirectoryCache.Clear();
            return true;
        }

        public static void Add(string path, Entry entry)
        {
            if (string.IsNullOrEmpty(path) || entry == null) return;

            path = NormalizePath(path);
            entry.Path = path;
            entry.FetchedAt = DateTime.UtcNow;

            // Remove old entry if exists to avoid duplicates
            _cache.TryRemove(path, out _);

            // Insert fresh entry
            _cache[path] = new CacheData { Entry = entry, Fetched = DateTime.UtcNow };

            // Recursively add children if directory
            if (entry.Type == EntryType.Dir && entry.Children != null)
            {
                foreach (var child in entry.Children)
                {
                    Add(child); // recursively replaces old child entries
                }
            }

            // Invalidate cached size for this path and all parent directories
            InvalidateSize(path);
        }

        /// <summary>
        /// Stores or updates an Entry recursively in cache, replacing existing entries by path.
        /// Prevents duplicates.
        /// </summary>
        public static void Add(Entry entry)
        {
            if (entry == null) return;

            entry.FetchedAt = DateTime.UtcNow;

            string path = NormalizePath(entry.Path);

            // Remove old entry if exists
            if (_cache.ContainsKey(path))
                _cache.TryRemove(path, out _);

            // Insert fresh entry
            _cache[path] = new CacheData { Entry = entry, Fetched = DateTime.UtcNow };

            // Recursively add children (fully replace old children)
            if (entry.Type == EntryType.Dir && entry.Children != null)
            {
                foreach (var child in entry.Children)
                {
                    Add(child); // recursive, replaces old child entries
                }
            }

            // Invalidate cached size for this path and all parent directories
            InvalidateSize(path);
        }

        /// <summary>
        /// Removes an entry (file or directory) from the cache and updates directory maps and folder sizes.
        /// </summary>
        /// <param name="path">The path of the entry to remove.</param>
        public static void Remove(string path)
        {
            if (string.IsNullOrEmpty(path)) return;

            path = NormalizePath(path);

            // Remove from _cache recursively (file or directory)
            foreach (var key in _cache.Keys
                .Where(k => k.Equals(path, StringComparison.OrdinalIgnoreCase) || k.StartsWith(path + "/", StringComparison.OrdinalIgnoreCase))
                .ToList())
            {
                _cache.TryRemove(key, out _);
            }

            // Remove from DirectoryCache recursively
            foreach (var key in DirectoryCache.Keys
                .Where(k => k.Equals(path, StringComparison.OrdinalIgnoreCase) || k.StartsWith(path + "/", StringComparison.OrdinalIgnoreCase))
                .ToList())
            {
                DirectoryCache.TryRemove(key, out _);
            }
        }

        public enum EntryFilter
        {
            Both,
            FilesOnly,
            DirectoriesOnly
        }

        public enum EntrySort
        {
            /// <summary>
            /// Directories first, then files, then by name
            /// </summary>
            Default,
            NameAsc,
            NameDesc,
            SizeAsc,
            SizeDesc,
            Custom // Use a provided comparison delegate: var custom = GetSubEntries(path, EntryFilter.Both, EntrySort.Custom, (a, b) => a.Name.Length.CompareTo(b.Name.Length));
        }

        /// <summary>
        /// Gets direct child entries from cache with filtering and sorting options.
        /// </summary>
        /// <param name="path">Parent directory path.</param>
        /// <param name="filter">Filter for files, folders, or both.</param>
        /// <param name="sort">Sorting option.</param>
        /// <param name="customSort">Optional custom comparison if sort is EntrySort.Custom.</param>
        /// <returns>List of filtered and sorted entries.</returns>
        public static List<Entry> GetSubEntries(string path, EntryFilter filter = EntryFilter.Both,EntrySort sort = EntrySort.Default, Comparison<Entry> customSort = null)
        {
            if (string.IsNullOrEmpty(path)) return new List<Entry>();

            string normalizedPath = NormalizePath(path).TrimEnd('/');

            var entries = _cache
                .Select(c => c.Value.Entry)
                .Where(e => e != null && e.Content != null)
                .Where(e =>
                {
                    string parent = GetParent(NormalizePath(e.Path).TrimEnd('/'));
                    return parent != null && parent.Equals(normalizedPath, StringComparison.OrdinalIgnoreCase);
                });

            // Apply filtering
            entries = filter switch
            {
                EntryFilter.FilesOnly => entries.Where(e => e.Type == EntryType.File),
                EntryFilter.DirectoriesOnly => entries.Where(e => e.Type == EntryType.Dir),
                _ => entries
            };

            // Apply sorting
            List<Entry> result = entries.ToList();
            switch (sort)
            {
                case EntrySort.Default:
                    result = [.. result.OrderBy(e => e.Type == EntryType.Dir ? 0 : 1).ThenBy(e => e.Name)];
                    break;
                case EntrySort.NameAsc:
                    result = result.OrderBy(e => e.Name).ToList();
                    break;
                case EntrySort.NameDesc:
                    result = result.OrderByDescending(e => e.Name).ToList();
                    break;
                case EntrySort.SizeAsc:
                    result = result.OrderBy(e => e.Size).ToList();
                    break;
                case EntrySort.SizeDesc:
                    result = result.OrderByDescending(e => e.Size).ToList();
                    break;
                case EntrySort.Custom:
                    if (customSort != null) result.Sort(customSort);
                    break;
            }

            return result;
        }

        /// <summary>
        /// Retrieves the size of a file or folder by summing all cached entries under the path.
        /// </summary>
        /// <param name="path">The entry path.</param>
        /// <returns>Total size in bytes.</returns>
        public static long GetSize(string path)
        {
            if (string.IsNullOrEmpty(path)) return 0;

            path = NormalizePath(path);

            // Return cached folder size if available
            if (FolderSizeMap.TryGetValue(path, out long cachedSize))
                return cachedSize;

            long size = 0;

            if (TryGetEntry(path, out Entry entry))
            {
                if (entry.Type == EntryType.File)
                {
                    size = entry.Size;
                }
                else if (entry.Type == EntryType.Dir)
                {
                    // Sum all files in cache whose paths are this directory or subdirectories
                    string prefix = path.EndsWith("/") ? path : path + "/";
                    size = _cache.Values
                        .Select(c => c.Entry)
                        .Where(e => e != null && e.Type == EntryType.File)
                        .Where(e => NormalizePath(e.Path).Equals(path, StringComparison.OrdinalIgnoreCase) ||
                                    NormalizePath(e.Path).StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                        .Sum(e => e.Size);
                }
            }

            // Cache the computed folder size
            FolderSizeMap[path] = size;

            return size;
        }

        /// <summary>
        /// Clears all caches.
        /// </summary>
        public static void Clear()
        {
            // Clear main entry cache
            _cache.Clear();

            // Clear folder size map
            FolderSizeMap.Clear();

            // Clear directory content cache
            DirectoryCache.Clear();

            // Clear SHA/MD5 hash cache
            ShaMd5Cache.Clear();

            // Optional: log the cache clearing
            Program.Log?.Write(LogEventLevel.Information, "All FileSystem caches have been cleared.");
        }

        /// <summary>
        /// Retrieves a cached entry if it exists and is valid based on TTL.
        /// </summary>
        public static bool TryGetEntry(string path, out Entry entry)
        {
            entry = null;
            if (_cache.TryGetValue(path, out var cached))
            {
                if (DateTime.UtcNow - cached.Fetched < CacheTTL)
                {
                    entry = cached.Entry;
                    return true;
                }
            }
            return false;
        }

        public static bool TryGetValue(string path, out CacheData cacheData)
        {
            cacheData = null;
            if (string.IsNullOrEmpty(path)) return false;
            path = NormalizePath(path);
            return _cache.TryGetValue(path, out cacheData);
        }

        /// <summary>
        /// Checks if an entry exists in the cache (file or directory).
        /// </summary>
        /// <param name="path">Path of the file or directory.</param>
        /// <returns>True if cached, false otherwise.</returns>
        public static bool Contains(string path)
        {
            if (string.IsNullOrEmpty(path)) return false;
            path = NormalizePath(path);

            return _cache.ContainsKey(path);
        }

        /// <summary>
        /// Stores an Entry and its children recursively in the cache with the current timestamp.
        /// </summary>
        /// <param name="entry"></param>
        private static void StoreEntryInCache(Entry entry)
        {
            if (entry == null) return;

            entry.FetchedAt = DateTime.UtcNow; // TTL tracking

            _cache[entry.Path] = new() { Entry = entry, Fetched = DateTime.UtcNow };

            if (entry.Type == EntryType.Dir && entry.Children != null)
            {
                foreach (Entry child in entry.Children) StoreEntryInCache(child);
            }
        }

        /// <summary>
        /// Invalidates folder size for a directory and all parent directories.
        /// </summary>
        private static void InvalidateSize(string path)
        {
            path = NormalizePath(path);

            while (!string.IsNullOrEmpty(path))
            {
                FolderSizeMap.TryRemove(path, out _);
                path = GetParent(path);
            }
        }

        /// <summary>
        /// Builds a dictionary of changes from the FileSystem cache.
        /// Key = repository path, Value = new content (null if deleted)
        /// </summary>
        public static Dictionary<string, string> BuildChanges()
        {
            var changes = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            foreach (var kv in _cache)
            {
                Entry entry = kv.Value.Entry;

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

        /// <summary>
        /// Retrieves GitHub directory contents with SHA validation and automatic cache refresh
        /// when upstream data changes.
        /// </summary>
        /// <param name="path">Repository path to retrieve.</param>
        /// <returns>A read-only list of repository contents.</returns>
        public static async Task<IReadOnlyList<RepositoryContent>> GetContentsCachedAsync(string path)
        {
            await RepositoryTreeChangedAsync();

            path = NormalizePath(path);

            if (DirectoryCache.TryGetValue(path, out var cached))
                return cached.contents;

            List<RepositoryContent> contents = await GetDirectoryRecursiveAsync(path);

            DirectoryCache[path] = (contents.AsReadOnly(), _lastRepoTreeSha);

            return contents;
        }

        public static async Task<List<RepositoryContent>> GetDirectoryRecursiveAsync(string path)
        {
            List<RepositoryContent> result = new();

            IReadOnlyList<RepositoryContent> contents =
                await Program.GitHub.Client.Repository.Content.GetAllContentsByRef(
                    _owner,
                    GitHub.Repository.repositoryName,
                    path,
                    GitHub.Repository.branch);

            foreach (RepositoryContent item in contents)
            {
                result.Add(item);

                if (item.Type == ContentType.Dir)
                {
                    List<RepositoryContent> sub = await GetDirectoryRecursiveAsync(item.Path);
                    result.AddRange(sub);
                }
            }

            return result;
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

        /// <summary>
        /// Converts a GitHub SHA string into a deterministic MD5 hash for UI and caching purposes.
        /// </summary>
        /// <param name="sha">The SHA string.</param>
        /// <returns>An MD5 hex string, or empty string if input is null.</returns>
        public static string ShaToMd5(string sha)
        {
            if (string.IsNullOrEmpty(sha)) return string.Empty;

            if (ShaMd5Cache.TryGetValue(sha, out string cached)) return cached;

            using MD5 md5 = MD5.Create();
            byte[] hashBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(sha));
            string result = string.Concat(hashBytes.Select(b => b.ToString("x2")));
            ShaMd5Cache[sha] = result;
            return result;
        }
    }
}