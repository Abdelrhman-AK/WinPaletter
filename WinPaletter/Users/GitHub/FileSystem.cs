using Octokit;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace WinPaletter.GitHub
{
    public static partial class FileSystem
    {
        [Flags]
        public enum EnumerateType { Files = 1, Directories = 2, Both = Files | Directories }

        public static string Extension(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) return string.Empty;
            int index = fileName.LastIndexOf('.');
            if (index < 0 || index == fileName.Length - 1) return string.Empty;
            return fileName.Substring(index + 1);
        }

        public static string FileName(string path)
        {
            if (string.IsNullOrEmpty(path)) return string.Empty;
            int index = path.LastIndexOf('/');
            if (index < 0) return path;
            return path.Substring(index + 1);
        }

        public static string DirectoryName(string path)
        {
            if (string.IsNullOrEmpty(path)) return string.Empty;
            int index = path.LastIndexOf('/');
            if (index < 0) return string.Empty;
            return path.Substring(0, index);
        }

        public static string CombinePath(params string[] parts)
        {
            if (parts == null || parts.Length == 0) return string.Empty;
            StringBuilder sb = new();
            foreach (var part in parts)
            {
                if (string.IsNullOrEmpty(part)) continue;
                string normalizedPart = part.Replace('\\', '/').Trim('/');
                if (sb.Length > 0)
                    sb.Append('/');
                sb.Append(normalizedPart);
            }
            return sb.ToString();
        }

        private static string NormalizePath(string path, bool trimEnd = true, bool trimStart = true)
        {
            if (string.IsNullOrEmpty(path)) return string.Empty;

            string result = path.Replace('\\', '/');

            if (trimStart) result = result.TrimStart('/');
            if (trimEnd) result = result.TrimEnd('/');

            return result;
        }

        /// <summary>
        /// Gets the parent path of the specified repository path by removing the last segment after the final '/'.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private static string GetParent(string path)
        {
            int i = path.LastIndexOf('/');
            string result = i < 0 ? string.Empty : path.Substring(0, i);

            return result;
        }

        public static async Task<bool> FileExistsAsync(string path, CancellationTokenSource cts = null)
        {
            cts ??= new();
            try
            {
                cts?.Token.ThrowIfCancellationRequested();
                var info = await GetInfoFastAsync(path, cts.Token);
                return info != null && info.Type == EntryType.File;
            }
            catch { return false; }
        }

        public static async Task<bool> DirectoryExistsAsync(string path, CancellationTokenSource cts = null)
        {
            cts ??= new();
            try
            {
                cts?.Token.ThrowIfCancellationRequested();
                var entries = await GetEntriesCachedAsync(path, forceRefresh: true);
                return entries != null && entries.Count > 0;
            }
            catch { return false; }
        }

        public static async Task<Entry> GetFileInfo(string path, CancellationTokenSource cts = null)
        {
            cts ??= new();
            var info = await GetInfoRefreshAsync(path, recursive: false, token: cts.Token);
            if (info == null || info.Type != EntryType.File) return null;
            return info;
        }

        public static async Task<Entry> GetDirectoryInfo(string path, CancellationTokenSource cts = null)
        {
            cts ??= new();
            var entries = await GetEntriesCachedAsync(path, forceRefresh: true);
            if (entries == null || entries.Count == 0) return null;

            return new Entry
            {
                Path = path,
                Type = EntryType.Dir,
                Content = null
            };
        }

        public static async Task<byte[]> ReadFileBytesAsync(string path, CancellationTokenSource cts = null)
        {
            cts ??= new();
            try
            {
                cts?.Token.ThrowIfCancellationRequested();

                // Get fresh file info without recursion
                var info = await FileSystem.GetInfoRefreshAsync(path, recursive: false, token: cts.Token);
                if (info == null || info.Type != EntryType.File)
                    throw new FileNotFoundException($"File `{path}` does not exist on GitHub.");

                var content = await Program.GitHub.Client.Repository.Content.GetAllContentsByRef(_owner, _repo, path, _branch);
                return Convert.FromBase64String(content[0].Content);
            }
            catch (OperationCanceledException) { throw; }
        }

        public static async Task<string> ReadFileAsync(string path, CancellationTokenSource cts = null)
        {
            var bytes = await ReadFileBytesAsync(path, cts);
            return Encoding.UTF8.GetString(bytes);
        }

        public static async Task WriteFileAsync(string githubPath, string contentOrPath, bool isLocalFile = false, string commitMessage = null, CancellationTokenSource cts = null, Action<int> reportProgress = null)
        {
            cts ??= new();
            reportProgress?.Invoke(0);
            commitMessage ??= $"Updated `{githubPath}` by {_owner}";

            string normalizedPath = NormalizePath(githubPath);

            try
            {
                cts?.Token.ThrowIfCancellationRequested();

                IRepositoryContentsClient client = Program.GitHub.Client.Repository.Content;

                // Fetch existing entry if present
                var existing = await FileSystem.GetInfoRefreshAsync(normalizedPath, recursive: false, token: cts.Token);

                // Prepare file bytes
                byte[] fileBytes;
                if (!isLocalFile)
                {
                    fileBytes = Encoding.UTF8.GetBytes(contentOrPath);
                }
                else
                {
                    using var fs = new FileStream(contentOrPath, System.IO.FileMode.Open, FileAccess.Read, FileShare.Read);
                    fileBytes = new byte[fs.Length];
                    int read = 0;
                    while (read < fs.Length)
                    {
                        int n = await fs.ReadAsync(fileBytes, read, (int)(fs.Length - read), cts.Token);
                        if (n == 0) break;
                        read += n;
                    }
                }

                string base64Content = Convert.ToBase64String(fileBytes);

                // Create or update file on GitHub
                if (existing?.Content == null)
                {
                    await client.CreateFile(_owner, _repo, normalizedPath, new CreateFileRequest(commitMessage, base64Content));
                }
                else
                {
                    await client.UpdateFile(_owner, _repo, normalizedPath, new UpdateFileRequest(commitMessage, base64Content, existing.Content.Sha));
                }

                // Fetch updated content and update cache
                var updatedContent = await client.GetAllContentsByRef(_owner, _repo, normalizedPath, _branch);
                if (updatedContent.Count > 0)
                {
                    var entry = new Entry
                    {
                        Path = normalizedPath,
                        Type = EntryType.File,
                        Content = updatedContent[0],
                        FetchedAt = DateTime.UtcNow
                    };

                    // Update unified cache
                    _cache[normalizedPath] = (entry, DateTime.UtcNow);
                    UpdateDirectoryMaps(Path.GetDirectoryName(normalizedPath), entry);

                    // Invalidate parent directory cache
                    string parentDir = NormalizePath(Path.GetDirectoryName(normalizedPath));
                    if (!string.IsNullOrEmpty(parentDir))
                        ClearEntryCache(parentDir);
                }

                reportProgress?.Invoke(100);
            }
            catch (OperationCanceledException)
            {
                reportProgress?.Invoke(0);
                throw;
            }
            catch (Exception ex)
            {
                Program.Log?.Write(LogEventLevel.Error, $"WriteFileAsync failed for `{normalizedPath}`", ex);
                reportProgress?.Invoke(0);
                throw;
            }
        }

        public static async Task CreateDirectoryAsync(string path, CancellationTokenSource cts = null, Action<int> reportProgress = null)
        {
            cts ??= new();
            reportProgress?.Invoke(0);

            string normalizedPath = NormalizePath(path);

            try
            {
                cts?.Token.ThrowIfCancellationRequested();

                // Check if directory already exists
                var entry = await GetEntryCachedAsync(normalizedPath, forceRefresh: true, cts: cts);
                if (entry == null)
                {
                    // Create placeholder file to ensure directory exists in GitHub
                    string placeholder = $"{normalizedPath}/.gitkeep";
                    await WriteFileAsync(placeholder, string.Empty, commitMessage: $"Create directory `{normalizedPath}` by {_owner}", cts: cts);

                    // Add new directory entry to cache
                    var dirEntry = new Entry
                    {
                        Path = normalizedPath,
                        Type = EntryType.Dir,
                        Content = null,
                        FetchedAt = DateTime.UtcNow
                    };
                    _cache[normalizedPath] = (dirEntry, DateTime.UtcNow);
                    UpdateDirectoryMaps(Path.GetDirectoryName(normalizedPath), dirEntry);

                    // Invalidate parent directory cache so next fetch sees the new child
                    string parentDir = NormalizePath(Path.GetDirectoryName(normalizedPath));
                    if (!string.IsNullOrEmpty(parentDir))
                        ClearEntryCache(parentDir);
                }

                reportProgress?.Invoke(100);
            }
            catch (OperationCanceledException)
            {
                reportProgress?.Invoke(0);
                throw;
            }
            catch (Exception ex)
            {
                Program.Log?.Write(LogEventLevel.Error, $"CreateDirectoryAsync failed for `{normalizedPath}`", ex);
                reportProgress?.Invoke(0);
                throw;
            }
        }

        public static async Task CopyFileAsync(string sourcePath, string destPath, Action<int> reportProgress = null, CancellationTokenSource cts = null)
        {
            cts ??= new();
            reportProgress?.Invoke(0);

            string sourceNormalized = NormalizePath(sourcePath);
            string destNormalized = NormalizePath(destPath);

            try
            {
                cts?.Token.ThrowIfCancellationRequested();

                // Get source file info (force refresh, accurate)
                var file = await GetInfoRefreshAsync(sourceNormalized, recursive: false, token: cts.Token);
                if (file == null || file.Type != EntryType.File)
                    throw new FileNotFoundException($"File not found: {sourceNormalized}");

                // Get latest commit on branch
                var masterRef = await Program.GitHub.Client.Git.Reference.Get(_owner, _repo, $"heads/{_branch}");
                var latestCommit = await Program.GitHub.Client.Git.Commit.Get(_owner, _repo, masterRef.Object.Sha);

                // Create a new tree item pointing to the source blob SHA
                var newTree = new NewTree { BaseTree = latestCommit.Tree.Sha };
                newTree.Tree.Add(new NewTreeItem
                {
                    Path = destNormalized,
                    Mode = "100644",
                    Type = TreeType.Blob,
                    Sha = file.Content.Sha
                });

                // Create tree and commit
                var createdTree = await Program.GitHub.Client.Git.Tree.Create(_owner, _repo, newTree);
                var newCommit = new NewCommit($"Copy `{sourceNormalized}` → `{destNormalized}` by {_owner}", createdTree.Sha, latestCommit.Sha);
                var commit = await Program.GitHub.Client.Git.Commit.Create(_owner, _repo, newCommit);

                // Update branch reference
                await Program.GitHub.Client.Git.Reference.Update(_owner, _repo, $"heads/{_branch}", new ReferenceUpdate(commit.Sha));

                // Update caches
                var newEntry = new Entry { Path = destNormalized, Type = EntryType.File, Content = file.Content, FetchedAt = DateTime.UtcNow };
                _cache[destNormalized] = (newEntry, DateTime.UtcNow);
                UpdateDirectoryMaps(Path.GetDirectoryName(destNormalized), newEntry);

                // Invalidate parent directory cache
                string parentDir = NormalizePath(Path.GetDirectoryName(destNormalized));
                if (!string.IsNullOrEmpty(parentDir))
                    ClearEntryCache(parentDir);

                reportProgress?.Invoke(100);
            }
            catch (OperationCanceledException)
            {
                reportProgress?.Invoke(0);
                throw;
            }
            catch (Exception ex)
            {
                Program.Log?.Write(LogEventLevel.Error, $"CopyFileAsync failed from `{sourceNormalized}` to `{destNormalized}`", ex);
                reportProgress?.Invoke(0);
                throw;
            }
        }

        public static async Task CopyDirectoryAsync(string sourcePath, string destPath, Action<int> reportProgress = null, CancellationTokenSource cts = null)
        {
            cts ??= new();
            reportProgress?.Invoke(0);

            string sourceNormalized = NormalizePath(sourcePath);
            string destNormalized = NormalizePath(destPath);

            try
            {
                cts?.Token.ThrowIfCancellationRequested();

                var entries = new List<Entry>();
                await foreach (var entry in EnumerateEntriesAsync(sourceNormalized, EnumerateType.Both, recursive: true, ct: cts.Token))
                    entries.Add(entry);

                if (entries.Count == 0)
                {
                    await CreateDirectoryAsync(destNormalized, cts: cts);
                    UpdateDirectoryMaps(Path.GetDirectoryName(destNormalized), new Entry { Path = destNormalized, Type = EntryType.Dir, Content = null });
                    reportProgress?.Invoke(100);
                    return;
                }

                var masterRef = await Program.GitHub.Client.Git.Reference.Get(_owner, _repo, $"heads/{_branch}");
                var latestCommit = await Program.GitHub.Client.Git.Commit.Get(_owner, _repo, masterRef.Object.Sha);
                var newTree = new NewTree { BaseTree = latestCommit.Tree.Sha };

                int totalFiles = entries.Count(e => e.Type == EntryType.File);
                int processed = 0;

                foreach (var entry in entries)
                {
                    string relative = entry.Path.Substring(sourceNormalized.Length).TrimStart('/', '\\');
                    string targetPath = NormalizePath($"{destNormalized}/{relative}");

                    if (entry.Type == EntryType.File)
                    {
                        var content = await Program.GitHub.Client.Repository.Content.GetAllContentsByRef(_owner, _repo, entry.Path, _branch);
                        newTree.Tree.Add(new NewTreeItem { Path = targetPath, Mode = "100644", Type = TreeType.Blob, Sha = content[0].Sha });

                        var newEntry = new Entry { Path = targetPath, Type = EntryType.File, Content = content[0], FetchedAt = DateTime.UtcNow };
                        _cache[targetPath] = (newEntry, DateTime.UtcNow);

                        // Update maps
                        UpdateDirectoryMaps(Path.GetDirectoryName(targetPath), newEntry);

                        processed++;
                        reportProgress?.Invoke(processed * 100 / totalFiles);
                    }
                    else // Directory
                    {
                        await CreateDirectoryAsync(targetPath, cts: cts);
                        var dirEntry = new Entry { Path = targetPath, Type = EntryType.Dir, Content = null, FetchedAt = DateTime.UtcNow };
                        _cache[targetPath] = (dirEntry, DateTime.UtcNow);

                        // Update maps for directory
                        UpdateDirectoryMaps(Path.GetDirectoryName(targetPath), dirEntry);
                    }
                }

                var createdTree = await Program.GitHub.Client.Git.Tree.Create(_owner, _repo, newTree);
                var newCommit = new NewCommit($"Copy directory `{sourceNormalized}` → `{destNormalized}` by {_owner}", createdTree.Sha, latestCommit.Sha);
                var commit = await Program.GitHub.Client.Git.Commit.Create(_owner, _repo, newCommit);
                await Program.GitHub.Client.Git.Reference.Update(_owner, _repo, $"heads/{_branch}", new ReferenceUpdate(commit.Sha));

                reportProgress?.Invoke(100);
            }
            catch (OperationCanceledException)
            {
                reportProgress?.Invoke(0);
                throw;
            }
            catch (Exception ex)
            {
                Program.Log?.Write(LogEventLevel.Error, $"CopyDirectoryAsync failed from `{sourceNormalized}` to `{destNormalized}`", ex);
                reportProgress?.Invoke(0);
                throw;
            }
        }

        public static async Task MoveFileAsync(string sourcePath, string destPath, CancellationTokenSource cts = null, Action<int> reportProgress = null)
        {
            cts ??= new();
            reportProgress?.Invoke(0);

            string sourceNormalized = NormalizePath(sourcePath);
            string destNormalized = NormalizePath(destPath);

            try
            {
                cts?.Token.ThrowIfCancellationRequested();

                // Get source file info (force refresh, accurate)
                var file = await GetInfoRefreshAsync(sourceNormalized, recursive: false, token: cts.Token);
                if (file == null || file.Type != EntryType.File)
                    throw new FileNotFoundException($"File `{sourceNormalized}` does not exist.");

                var masterRef = await Program.GitHub.Client.Git.Reference.Get(_owner, _repo, $"heads/{_branch}");
                var latestCommit = await Program.GitHub.Client.Git.Commit.Get(_owner, _repo, masterRef.Object.Sha);

                var newTree = new NewTree { BaseTree = latestCommit.Tree.Sha };
                newTree.Tree.Add(new NewTreeItem { Path = destNormalized, Mode = "100644", Type = TreeType.Blob, Sha = file.Content.Sha });
                newTree.Tree.Add(new NewTreeItem { Path = sourceNormalized, Mode = "100644", Type = TreeType.Blob, Sha = null }); // delete old

                var createdTree = await Program.GitHub.Client.Git.Tree.Create(_owner, _repo, newTree);
                var newCommit = new NewCommit($"Move `{sourceNormalized}` → `{destNormalized}` by {_owner}", createdTree.Sha, latestCommit.Sha);
                var commit = await Program.GitHub.Client.Git.Commit.Create(_owner, _repo, newCommit);

                await Program.GitHub.Client.Git.Reference.Update(_owner, _repo, $"heads/{_branch}", new ReferenceUpdate(commit.Sha));

                // Remove old cache
                _cache.TryRemove(sourceNormalized, out _);

                // Add new cache
                var newEntry = new Entry { Path = destNormalized, Type = EntryType.File, Content = file.Content, FetchedAt = DateTime.UtcNow };
                _cache[destNormalized] = (newEntry, DateTime.UtcNow);
                RemoveDirectoryMaps(Path.GetDirectoryName(sourceNormalized), file);
                UpdateDirectoryMaps(Path.GetDirectoryName(destNormalized), newEntry);

                reportProgress?.Invoke(100);
            }
            catch (OperationCanceledException)
            {
                reportProgress?.Invoke(0);
                throw;
            }
            catch (Exception ex)
            {
                Program.Log?.Write(LogEventLevel.Error, $"MoveFileAsync failed for `{sourceNormalized}` → `{destNormalized}`", ex);
                reportProgress?.Invoke(0);
                throw;
            }
        }

        public static async Task MoveDirectoryAsync(string sourcePath, string destPath, CancellationTokenSource cts = null, Action<int> reportProgress = null)
        {
            cts ??= new();
            reportProgress?.Invoke(0);

            string sourceNormalized = NormalizePath(sourcePath);
            string destNormalized = NormalizePath(destPath);

            try
            {
                cts?.Token.ThrowIfCancellationRequested();

                var entries = new List<Entry>();
                await foreach (var entry in EnumerateEntriesAsync(sourceNormalized, EnumerateType.Both, recursive: true, ct: cts.Token))
                    entries.Add(entry);

                if (entries.Count == 0)
                {
                    await CreateDirectoryAsync(destNormalized, cts: cts);
                    UpdateDirectoryMaps(Path.GetDirectoryName(destNormalized), new Entry { Path = destNormalized, Type = EntryType.Dir, Content = null });
                    reportProgress?.Invoke(100);
                    return;
                }

                var masterRef = await Program.GitHub.Client.Git.Reference.Get(_owner, _repo, $"heads/{_branch}");
                var latestCommit = await Program.GitHub.Client.Git.Commit.Get(_owner, _repo, masterRef.Object.Sha);

                var newTree = new NewTree { BaseTree = latestCommit.Tree.Sha };
                int totalFiles = entries.Count(e => e.Type == EntryType.File);
                int processed = 0;

                foreach (var entry in entries)
                {
                    string relative = entry.Path.Substring(sourceNormalized.Length).TrimStart('/', '\\');
                    string targetPath = NormalizePath($"{destNormalized}/{relative}");

                    if (entry.Type == EntryType.File)
                    {
                        var f = await Program.GitHub.Client.Repository.Content.GetAllContentsByRef(_owner, _repo, entry.Path, _branch);

                        // Add new file and delete old file in Git tree
                        newTree.Tree.Add(new NewTreeItem { Path = targetPath, Mode = "100644", Type = TreeType.Blob, Sha = f[0].Sha });
                        newTree.Tree.Add(new NewTreeItem { Path = entry.Path, Mode = "100644", Type = TreeType.Blob, Sha = null });

                        // Update cache
                        _cache.TryRemove(entry.Path, out _);
                        var newEntry = new Entry { Path = targetPath, Type = EntryType.File, Content = f[0], FetchedAt = DateTime.UtcNow };
                        _cache[targetPath] = (newEntry, DateTime.UtcNow);

                        // Update maps
                        UpdateDirectoryMaps(Path.GetDirectoryName(targetPath), newEntry);
                        RemoveDirectoryMaps(Path.GetDirectoryName(entry.Path), entry);

                        processed++;
                        reportProgress?.Invoke(processed * 100 / totalFiles);
                    }
                    else
                    {
                        await CreateDirectoryAsync(targetPath, cts: cts);
                        var dirEntry = new Entry { Path = targetPath, Type = EntryType.Dir, Content = null, FetchedAt = DateTime.UtcNow };
                        _cache[targetPath] = (dirEntry, DateTime.UtcNow);

                        // Update maps for directory
                        UpdateDirectoryMaps(Path.GetDirectoryName(targetPath), dirEntry);
                        RemoveDirectoryMaps(Path.GetDirectoryName(entry.Path), entry);
                    }
                }

                var createdTree = await Program.GitHub.Client.Git.Tree.Create(_owner, _repo, newTree);
                var newCommit = new NewCommit($"Move directory `{sourceNormalized}` → `{destNormalized}` by {_owner}", createdTree.Sha, latestCommit.Sha);
                var commit = await Program.GitHub.Client.Git.Commit.Create(_owner, _repo, newCommit);
                await Program.GitHub.Client.Git.Reference.Update(_owner, _repo, $"heads/{_branch}", new ReferenceUpdate(commit.Sha));

                reportProgress?.Invoke(100);
            }
            catch (OperationCanceledException)
            {
                reportProgress?.Invoke(0);
                throw;
            }
            catch (Exception ex)
            {
                Program.Log?.Write(LogEventLevel.Error, $"MoveDirectoryAsync failed for `{sourceNormalized}` → `{destNormalized}`", ex);
                reportProgress?.Invoke(0);
                throw;
            }
        }

        public static async Task DeleteFileAsync(string path, CancellationTokenSource cts = null, Action<int> reportProgress = null)
        {
            cts ??= new();
            reportProgress?.Invoke(0);

            string normalizedPath = NormalizePath(path);

            try
            {
                cts?.Token.ThrowIfCancellationRequested();

                // Get source file info (force refresh, accurate)
                var file = await GetInfoRefreshAsync(normalizedPath, recursive: false, token: cts.Token);
                if (file?.Type != EntryType.File)
                {
                    reportProgress?.Invoke(100);
                    return;
                }

                await Program.GitHub.Client.Repository.Content.DeleteFile(
                    _owner,
                    _repo,
                    normalizedPath,
                    new DeleteFileRequest($"{_owner} deleted `{normalizedPath}`", file.Content.Sha)
                );

                // Remove file from _infoCache
                _cache.TryRemove(normalizedPath, out _);
                RemoveDirectoryMaps(Path.GetDirectoryName(normalizedPath), file);

                reportProgress?.Invoke(100);
            }
            catch (OperationCanceledException)
            {
                reportProgress?.Invoke(0);
                throw;
            }
            catch (Exception ex)
            {
                Program.Log?.Write(LogEventLevel.Error, $"DeleteFileAsync failed for `{normalizedPath}`", ex);
                reportProgress?.Invoke(0);
                throw;
            }
        }

        public static async Task DeleteDirectoryAsync(string path, Action<int> reportProgress = null, CancellationTokenSource cts = null)
        {
            cts ??= new();
            reportProgress?.Invoke(0);

            string normalized = NormalizePath(path);

            try
            {
                cts?.Token.ThrowIfCancellationRequested();

                var entries = new List<Entry>();
                await foreach (var entry in EnumerateEntriesAsync(normalized, EnumerateType.Both, recursive: true, ct: cts.Token))
                    entries.Add(entry);

                if (entries.Count == 0)
                {
                    // Nothing to delete, just remove root from caches
                    var rootEntry = new Entry { Path = normalized, Type = EntryType.Dir, Content = null };
                    RemoveDirectoryMaps(NormalizePath(Path.GetDirectoryName(normalized)), rootEntry);
                    _cache.TryRemove(normalized, out _);
                    reportProgress?.Invoke(100);
                    return;
                }

                var masterRef = await Program.GitHub.Client.Git.Reference.Get(_owner, _repo, $"heads/{_branch}");
                var latestCommit = await Program.GitHub.Client.Git.Commit.Get(_owner, _repo, masterRef.Object.Sha);

                var newTree = new NewTree { BaseTree = latestCommit.Tree.Sha };
                int totalFiles = entries.Count(e => e.Type == EntryType.File);
                int processed = 0;

                // Remove files
                foreach (var file in entries.Where(e => e.Type == EntryType.File))
                {
                    newTree.Tree.Add(new NewTreeItem { Path = file.Path, Mode = "100644", Type = TreeType.Blob, Sha = null });
                    RemoveDirectoryMaps(NormalizePath(Path.GetDirectoryName(file.Path)), file);
                    _cache.TryRemove(file.Path, out _);

                    processed++;
                    reportProgress?.Invoke(processed * 100 / totalFiles);
                }

                // Remove directories, deepest first
                foreach (var dir in entries.Where(e => e.Type == EntryType.Dir).OrderByDescending(d => d.Path.Length))
                {
                    // Optionally remove .gitkeep if it exists
                    newTree.Tree.Add(new NewTreeItem { Path = dir.Path + "/.gitkeep", Mode = "100644", Type = TreeType.Blob, Sha = null });
                    RemoveDirectoryMaps(NormalizePath(Path.GetDirectoryName(dir.Path)), dir);
                    _cache.TryRemove(dir.Path, out _);
                }

                // Remove the root directory itself
                var rootDirEntry = new Entry { Path = normalized, Type = EntryType.Dir, Content = null };
                RemoveDirectoryMaps(NormalizePath(Path.GetDirectoryName(normalized)), rootDirEntry);
                _cache.TryRemove(normalized, out _);

                var createdTree = await Program.GitHub.Client.Git.Tree.Create(_owner, _repo, newTree);
                var newCommit = new NewCommit($"Delete directory `{normalized}` by {_owner}", createdTree.Sha, latestCommit.Sha);
                var commit = await Program.GitHub.Client.Git.Commit.Create(_owner, _repo, newCommit);
                await Program.GitHub.Client.Git.Reference.Update(_owner, _repo, $"heads/{_branch}", new ReferenceUpdate(commit.Sha));

                reportProgress?.Invoke(100);
            }
            catch (OperationCanceledException)
            {
                reportProgress?.Invoke(0);
                throw;
            }
            catch (Exception ex)
            {
                Program.Log?.Write(LogEventLevel.Error, $"DeleteDirectoryAsync failed for {path}", ex);
                reportProgress?.Invoke(0);
                throw;
            }
        }

        public static async IAsyncEnumerable<Entry> EnumerateEntriesAsync(string path, EnumerateType type = EnumerateType.Both, string searchPattern = "*", bool recursive = false, [EnumeratorCancellation] CancellationToken ct = default, bool forceRefresh = true)
        {
            Regex regex = searchPattern != "*" && !string.IsNullOrEmpty(searchPattern)
                ? new("^" + Regex.Escape(searchPattern).Replace("\\*", ".*").Replace("\\?", ".") + "$", RegexOptions.IgnoreCase)
                : null;

            var entries = await GetEntriesCachedAsync(path, forceRefresh: forceRefresh);
            foreach (var entry in entries)
            {
                ct.ThrowIfCancellationRequested();

                if ((entry.Type == EntryType.File && type.HasFlag(EnumerateType.Files) && (regex == null || regex.IsMatch(Path.GetFileName(entry.Path)))) ||
                    (entry.Type == EntryType.Dir && type.HasFlag(EnumerateType.Directories)))
                {
                    yield return entry;
                }

                if (recursive && entry.Type == EntryType.Dir)
                {
                    await foreach (var sub in EnumerateEntriesAsync(entry.Path, type, searchPattern, true, ct, forceRefresh))
                        yield return sub;
                }
            }
        }

        public static async IAsyncEnumerable<string> EnumeratePathsAsync(string path, EnumerateType type = EnumerateType.Both, string searchPattern = "*", bool recursive = false, [EnumeratorCancellation] CancellationToken ct = default, bool forceRefresh = true)
        {
            await foreach (var entry in EnumerateEntriesAsync(path, type, searchPattern, recursive, ct, forceRefresh))
                yield return entry.Path;
        }
    }
}
