using Octokit;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace WinPaletter.GitHub
{
    public static partial class FileSystem
    {
        internal static class Cache
        {
            internal class CacheData
            {
                public Entry Entry { get; set; }
                public DateTime Fetched { get; set; }
                public List<RepositoryContent> DirectoryContents { get; set; } = new();
                public long? DirectorySize { get; set; }
                public string Md5 { get; set; }
            }

            private static readonly ConcurrentDictionary<string, CacheData> _cache = new(StringComparer.OrdinalIgnoreCase);

            #region Entry Cache

            public static void Add(Entry entry)
            {
                if (entry == null || string.IsNullOrEmpty(entry.Path)) return;

                _cache.AddOrUpdate(entry.Path,
                    _ => new CacheData { Entry = entry, Fetched = DateTime.UtcNow },
                    (_, old) =>
                    {
                        old.Entry = entry;
                        old.Fetched = DateTime.UtcNow;
                        return old;
                    });

                UpdateDirectoryMapping(entry);
                InvalidateSize(entry.Path);
            }

            public static void Add(string path, Entry entry)
            {
                if (string.IsNullOrEmpty(path) || entry == null) return;
                entry.Path = path;

                _cache.AddOrUpdate(path,
                    _ => new CacheData { Entry = entry, Fetched = DateTime.UtcNow },
                    (_, old) =>
                    {
                        old.Entry = entry;
                        old.Fetched = DateTime.UtcNow;
                        return old;
                    });

                UpdateDirectoryMapping(entry);
                InvalidateSize(path);
            }

            public static bool Remove(string path)
            {
                if (string.IsNullOrEmpty(path)) return false;

                if (!_cache.TryRemove(path, out var removed)) return false;

                RemoveDirectoryMapping(removed.Entry);
                InvalidateSize(path);

                return true;
            }

            public static void Clear() => _cache.Clear();

            public static Entry Get(string path) => _cache.TryGetValue(path, out var data) ? data.Entry : null;

            public static CacheData GetData(string path) => _cache.TryGetValue(path, out var data) ? data : null;

            public static bool Contains(string path) => _cache.ContainsKey(path);

            public static IReadOnlyList<Entry> ToList() => _cache.Values.Select(d => d.Entry).ToList().AsReadOnly();

            #endregion

            #region Directory Mapping

            private static void UpdateDirectoryMapping(Entry entry)
            {
                if (entry == null) return;

                string parent = GetParent(entry.Path);
                var parentData = _cache.GetOrAdd(parent, _ => new CacheData());

                if (entry.Content != null)
                {
                    var list = parentData.DirectoryContents;
                    lock (list) // ensure thread safety
                    {
                        list.RemoveAll(c => string.Equals(c.Path, entry.Path, StringComparison.OrdinalIgnoreCase));
                        list.Add(entry.Content);
                    }
                }

                if (entry.Type == EntryType.Dir && entry.Children != null)
                {
                    foreach (var child in entry.Children)
                        UpdateDirectoryMapping(child);
                }
            }

            private static void RemoveDirectoryMapping(Entry entry)
            {
                if (entry == null) return;

                string parent = GetParent(entry.Path);
                if (_cache.TryGetValue(parent, out var parentData))
                    parentData.DirectoryContents.RemoveAll(c => string.Equals(c.Path, entry.Path, StringComparison.OrdinalIgnoreCase));

                if (entry.Type == EntryType.Dir && entry.Children != null)
                    foreach (var child in entry.Children) RemoveDirectoryMapping(child);

                _cache.TryRemove(entry.Path, out _);
            }

            public static IReadOnlyList<RepositoryContent> GetDirectoryContents(string path, bool recursive = true)
            {
                if (string.IsNullOrEmpty(path)) path = string.Empty;

                if (!_cache.TryGetValue(path, out var rootData) || rootData.DirectoryContents.Count == 0)
                    return Array.Empty<RepositoryContent>();

                if (!recursive) return rootData.DirectoryContents.AsReadOnly();

                var allContents = new List<RepositoryContent>();
                var queue = new Queue<CacheData>();
                queue.Enqueue(rootData);

                while (queue.Count > 0)
                {
                    var currentData = queue.Dequeue();

                    foreach (var rc in currentData.DirectoryContents)
                    {
                        allContents.Add(rc);
                        if (_cache.TryGetValue(rc.Path, out var childData) && childData.Entry?.Type == EntryType.Dir)
                            queue.Enqueue(childData);
                    }
                }

                return allContents.AsReadOnly();
            }

            #endregion

            #region Size Cache

            private static void InvalidateSize(string path)
            {
                if (string.IsNullOrEmpty(path)) return;

                if (_cache.TryGetValue(path, out var data))
                    data.DirectorySize = null;

                string parent = GetParent(path);
                while (!string.IsNullOrEmpty(parent))
                {
                    if (_cache.TryGetValue(parent, out var parentData))
                        parentData.DirectorySize = null;

                    parent = GetParent(parent);
                }
            }

            public static long GetDirectorySize(string path)
            {
                if (string.IsNullOrEmpty(path)) return 0;

                if (!_cache.TryGetValue(path, out var rootData)) return 0;
                if (rootData.DirectorySize.HasValue) return rootData.DirectorySize.Value;

                long total = 0;
                var contents = GetDirectoryContents(path);
                foreach (var rc in contents)
                {
                    if (_cache.TryGetValue(rc.Path, out var rcData) && rcData.Entry?.Type == EntryType.File)
                        total += rc.Size;
                }

                rootData.DirectorySize = total;
                return total;
            }

            #endregion

            #region MD5 / SHA

            public static void AddMd5(string sha)
            {
                if (string.IsNullOrEmpty(sha)) return;

                if (_cache.TryGetValue(sha, out var data) && !string.IsNullOrEmpty(data.Md5)) return;

                using var md5 = MD5.Create();
                byte[] hashBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(sha));
                string result = string.Concat(hashBytes.Select(b => b.ToString("x2")));

                if (_cache.TryGetValue(sha, out data))
                    data.Md5 = result;
                else
                    _cache[sha] = new CacheData { Md5 = result };
            }

            public static bool TryGetMd5(string sha, out string md5)
            {
                if (_cache.TryGetValue(sha, out var data) && !string.IsNullOrEmpty(data.Md5))
                {
                    md5 = data.Md5;
                    return true;
                }
                md5 = null;
                return false;
            }

            public static bool RemoveMd5(string sha)
            {
                if (_cache.TryGetValue(sha, out var data))
                {
                    data.Md5 = null;
                    return true;
                }
                return false;
            }

            #endregion

            #region Content Helpers

            /// <summary>
            /// Returns all file paths in the cache.
            /// </summary>
            public static IReadOnlyList<string> AllFiles()
            {
                return _cache.Values
                    .Where(d => d.Entry?.Type == EntryType.File)
                    .Select(d => d.Entry.Path)
                    .ToList()
                    .AsReadOnly();
            }

            /// <summary>
            /// Returns all RepositoryContent objects in the cache (files and directories).
            /// </summary>
            public static IReadOnlyList<RepositoryContent> AllRepositoryContents()
            {
                var allContents = new List<RepositoryContent>();

                foreach (var data in _cache.Values)
                {
                    if (data.DirectoryContents != null)
                        allContents.AddRange(data.DirectoryContents);
                    else if (data.Entry?.Content != null)
                        allContents.Add(data.Entry.Content);
                }

                return allContents.AsReadOnly();
            }

            /// <summary>
            /// Returns all cached Entry objects (files and directories).
            /// </summary>
            public static IReadOnlyList<Entry> All()
            {
                return _cache.Values
                    .Select(d => d.Entry)
                    .Where(e => e != null)
                    .ToList()
                    .AsReadOnly();
            }

            /// <summary>
            /// Returns all cached directories (paths) in the repository.
            /// </summary>
            public static IReadOnlyList<string> AllDirectories()
            {
                return _cache.Values
                    .Where(d => d.Entry?.Type == EntryType.Dir)
                    .Select(d => d.Entry.Path)
                    .ToList()
                    .AsReadOnly();
            }

            /// <summary>
            /// Returns immediate or all subdirectories under a given path.
            /// </summary>
            public static IReadOnlyList<string> GetDirectories(string path, bool subDirectories = true)
            {
                if (string.IsNullOrEmpty(path)) path = string.Empty;

                if (!_cache.TryGetValue(path, out var rootData) || rootData.DirectoryContents.Count == 0)
                    return Array.Empty<string>();

                var directories = new List<string>();

                if (!subDirectories)
                {
                    // Only immediate subdirectories
                    directories.AddRange(rootData.DirectoryContents
                        .Where(c => _cache.TryGetValue(c.Path, out var cd) && cd.Entry?.Type == EntryType.Dir)
                        .Select(c => c.Path));
                }
                else
                {
                    // Recursive: BFS through all children
                    var queue = new Queue<CacheData>();
                    queue.Enqueue(rootData);

                    while (queue.Count > 0)
                    {
                        var currentData = queue.Dequeue();

                        foreach (var rc in currentData.DirectoryContents)
                        {
                            if (_cache.TryGetValue(rc.Path, out var childData) && childData.Entry?.Type == EntryType.Dir)
                            {
                                directories.Add(rc.Path);
                                queue.Enqueue(childData);
                            }
                        }
                    }
                }

                return directories.AsReadOnly();
            }

            #endregion

            #region Utilities

            private static string GetParent(string path)
            {
                if (string.IsNullOrEmpty(path)) return string.Empty;
                int idx = path.LastIndexOf('/');
                return idx < 0 ? string.Empty : path.Substring(0, idx);
            }

            #endregion
        }
    }
}