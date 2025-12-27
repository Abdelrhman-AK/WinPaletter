using Octokit;
using Serilog.Events;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
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
        /// Thread-safe cache for computed SHA/MD5 hash values keyed by string.
        /// </summary>
        private static readonly ConcurrentDictionary<string, string> ShaMd5Cache = new();

        /// <summary>
        /// Mapping of directory paths to their immediate children.
        /// </summary>
        private static readonly Dictionary<string, List<RepositoryContent>> DirectoryMap = new(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Mapping of directory paths to total byte size of their contents.
        /// </summary>
        private static readonly Dictionary<string, long> FolderSizeMap = new(StringComparer.OrdinalIgnoreCase);

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

            entry.Path = path;

            // Add or update cache
            _cache.AddOrUpdate(path,
                _ => new CacheData { Entry = entry, Fetched = DateTime.UtcNow },
                (_, old) =>
                {
                    old.Entry = entry;
                    old.Fetched = DateTime.UtcNow;
                    return old;
                });

            // Update directory mapping for parent and children
            UpdateDirectoryMap(entry);

            // Invalidate cached size for this path and all parents
            InvalidateSize(path);
        }

        /// <summary>
        /// Stores or updates an Entry recursively in cache.
        /// </summary>
        public static void Add(Entry entry)
        {
            if (entry == null) return;

            entry.FetchedAt = DateTime.UtcNow;
            _cache[entry.Path] = new() { Entry = entry, Fetched = DateTime.UtcNow };

            if (entry.Type == EntryType.Dir && entry.Children != null)
                foreach (var child in entry.Children)
                    Add(child);

            UpdateDirectoryMap(entry);
        }

        /// <summary>
        /// Removes an entry (file or directory) from the cache and updates directory maps and folder sizes.
        /// </summary>
        /// <param name="path">The path of the entry to remove.</param>
        public static void Remove(string path)
        {
            if (string.IsNullOrEmpty(path)) return;

            foreach (var key in _cache.Keys
                .Where(k => k.Equals(path, StringComparison.OrdinalIgnoreCase) || k.StartsWith(path + "/", StringComparison.OrdinalIgnoreCase))
                .ToList())
            {
                _cache.TryRemove(key, out _);
            }

            foreach (var key in DirectoryCache.Keys
                .Where(k => k.Equals(path, StringComparison.OrdinalIgnoreCase) || k.StartsWith(path + "/", StringComparison.OrdinalIgnoreCase))
                .ToList())
            {
                DirectoryCache.TryRemove(key, out _);
            }

            foreach (var key in FolderSizeMap.Keys
                .Where(k => k.Equals(path, StringComparison.OrdinalIgnoreCase) || k.StartsWith(path + "/", StringComparison.OrdinalIgnoreCase))
                .ToList())
            {
                FolderSizeMap.Remove(key);
            }

            foreach (var key in DirectoryMap.Keys
                .Where(k => k.Equals(path, StringComparison.OrdinalIgnoreCase) || k.StartsWith(path + "/", StringComparison.OrdinalIgnoreCase))
                .ToList())
            {
                DirectoryMap.Remove(key);
            }
        }

        /// <summary>
        /// Retrieves the cached size of an entry. Returns 0 if entry is not tracked.
        /// </summary>
        /// <param name="path">The entry path.</param>
        /// <returns>The total size in bytes.</returns>
        public static long GetSize(string path)
        {
            if (string.IsNullOrEmpty(path)) return 0;

            path = NormalizePath(path);
            return FolderSizeMap.TryGetValue(path, out long size) ? size : 0;
        }

        /// <summary>
        /// Clears all caches.
        /// </summary>
        public static void Clear()
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
        /// Updates DirectoryMap and FolderSizeMap after storing or modifying an entry.
        /// </summary>
        private static void UpdateDirectoryMap(Entry entry)
        {
            string directory = NormalizePath(GetParent(entry.Path));

            if (!DirectoryMap.TryGetValue(directory, out var list))
                DirectoryMap[directory] = list = new();

            if (entry.Type == EntryType.File && entry.Content != null)
            {
                var existing = list.FirstOrDefault(c => string.Equals(c.Path, entry.Path, StringComparison.OrdinalIgnoreCase));
                if (existing != null)
                {
                    list.Remove(existing);
                    FolderSizeMap[directory] = Math.Max(0, FolderSizeMap.TryGetValue(directory, out long size) ? size - existing.Size : 0);
                }

                list.Add(entry.Content);
                FolderSizeMap[directory] = FolderSizeMap.TryGetValue(directory, out long current) ? current + entry.Content.Size : entry.Content.Size;
            }

            if (entry.Type == EntryType.Dir)
            {
                if (!FolderSizeMap.ContainsKey(entry.Path)) FolderSizeMap[entry.Path] = 0;
            }

            DirectoryCache[directory] = (list.AsReadOnly(), entry.Content?.Sha);
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
        /// Retrieves directory contents from cache if SHA matches.
        /// </summary>
        public static IReadOnlyList<RepositoryContent> GetDirectory(string path)
        {
            if (DirectoryMap.TryGetValue(path, out var cached))
            {
                return cached;
            }
            return null;
        }

        /// <summary>
        /// Moves an entry (file or directory) from oldPath to newPath in the cache and updates directory maps.
        /// Optionally provide the Entry fetched from Octokit to avoid lookup.
        /// </summary>
        public static void MoveEntry(string oldPath, string newPath, Entry entry = null)
        {
            if (string.IsNullOrEmpty(oldPath) || string.IsNullOrEmpty(newPath)) return;
            if (string.Equals(oldPath, newPath, StringComparison.OrdinalIgnoreCase)) return;

            entry ??= GetEntry(oldPath);
            if (entry == null) return;

            // Remove old entry from maps/cache
            Remove(oldPath);

            // Update path
            entry.Path = newPath;

            // Recursively update children's paths for directories
            if (entry.Type == EntryType.Dir && entry.Children != null)
            {
                foreach (var child in entry.Children)
                {
                    string childTargetPath = newPath + child.Path.Substring(oldPath.Length);
                    MoveEntry(child.Path, childTargetPath, child);
                }
            }

            // Store updated entry and update maps
            Add(entry);
            UpdateDirectoryMaps(GetParent(newPath), entry);
        }

        /// <summary>
        /// Retrieves an Entry from the cache by path.
        /// Returns null if the entry is not cached.
        /// </summary>
        /// <param name="path">Path of the file or directory in the repository.</param>
        /// <returns>The cached Entry or null if not found.</returns>
        private static Entry GetEntry(string path)
        {
            if (string.IsNullOrEmpty(path)) return null;

            if (_cache.TryGetValue(path, out var cached))
                return cached.Entry;

            return null;
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
        /// Checks if an entry exists in the cache (file or directory).
        /// </summary>
        /// <param name="path">Path of the file or directory.</param>
        /// <returns>True if cached, false otherwise.</returns>
        public static bool ContainsInDirectoryMap(string path)
        {
            if (string.IsNullOrEmpty(path)) return false;
            path = NormalizePath(path);

            return DirectoryMap.ContainsKey(path);
        }

        /// <summary>
        /// Creates an internal lookup table mapping each directory path to its immediate children.
        /// </summary>
        public static void BuildDirectoryMap()
        {
            Program.Log?.Write(LogEventLevel.Information, "BuildDirectoryMap started");

            DirectoryMap.Clear();

            foreach (CacheData tuple in _cache.Values)
            {
                Entry entry = tuple.Entry;
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
        public static long GetFoldersSize(string path, bool addToFolderSizeMap = true)
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
                FolderSizeMap.Remove(path);
                path = GetParent(path);
            }
        }

        /// <summary>
        /// Updates internal directory maps when an entry is added or modified.
        /// </summary>
        /// <param name="directory"></param>
        /// <param name="entry"></param>
        private static void UpdateDirectoryMaps(string directory, Entry entry)
        {
            directory = NormalizePath(directory);

            if (!DirectoryMap.TryGetValue(directory, out List<RepositoryContent> list))
                DirectoryMap[directory] = list = new();

            if (entry.Type == EntryType.File && entry.Content != null)
            {
                // Only add files that belong to this directory directly
                if (GetParent(entry.Path) == directory)
                {
                    var existing = list.FirstOrDefault(c => string.Equals(c.Path, entry.Path, StringComparison.OrdinalIgnoreCase));
                    if (existing != null)
                    {
                        list.Remove(existing);
                        FolderSizeMap[directory] = Math.Max(0, FolderSizeMap.TryGetValue(directory, out long size) ? size - existing.Size : 0);
                    }

                    list.Add(entry.Content);
                    FolderSizeMap[directory] = FolderSizeMap.TryGetValue(directory, out long current) ? current + entry.Content.Size : entry.Content.Size;
                }
            }

            if (entry.Type == EntryType.Dir)
            {
                // Ensure FolderSizeMap entry exists, but do not add children to the list
                if (!FolderSizeMap.ContainsKey(directory)) FolderSizeMap[directory] = 0;

                // Only add this directory as a reference in parent
                string parent = GetParent(entry.Path);
                if (!string.IsNullOrEmpty(parent) && DirectoryMap.TryGetValue(parent, out var parentList))
                {
                    if (!parentList.Any(c => string.Equals(c.Path, entry.Path, StringComparison.OrdinalIgnoreCase)))
                    {
                        // Use the existing cached content of the directory
                        if (entry.Content != null) parentList.Add(entry.Content);
                    }
                }
            }

            // Update DirectoryCache
            DirectoryCache[directory] = (list.AsReadOnly(), entry.Content?.Sha);
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
        /// Normalizes a path for consistent storage.
        /// </summary>
        private static string NormalizePath(string path) => path?.Replace("\\", "/").TrimEnd('/') ?? string.Empty;

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

        /// <summary>
        /// Gets the parent directory of a path.
        /// </summary>
        private static string GetParent(string path)
        {
            if (string.IsNullOrEmpty(path)) return string.Empty;
            int idx = path.LastIndexOf('/');
            return idx <= 0 ? string.Empty : path.Substring(0, idx);
        }
    }
}