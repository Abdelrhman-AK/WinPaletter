using Newtonsoft.Json.Linq;
using Octokit;
using Serilog.Events;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinPaletter.NativeMethods;

namespace WinPaletter.GitHub
{
    public static partial class FileSystem
    {
        [Flags]
        public enum EnumerateType
        {
            Files = 1,
            Directories = 2,
            Both = Files | Directories
        }

        private static string GetRelativePath(string fullPath)
        {
            if (string.IsNullOrEmpty(fullPath)) return fullPath;

            // Normalize to forward slashes and trim trailing slashes for comparison
            string full = fullPath.Replace('\\', '/').TrimEnd('/');
            string root = (_root ?? string.Empty).Replace('\\', '/').TrimEnd('/');

            // If exactly the root, return empty (relative root)
            if (string.Equals(full, root, StringComparison.OrdinalIgnoreCase)) return string.Empty;

            // If full starts with root + '/', return substring after root
            string prefix = root + "/";
            if (!string.IsNullOrEmpty(root) && full.StartsWith(prefix, StringComparison.OrdinalIgnoreCase)) return full.Substring(prefix.Length);

            // Otherwise return the normalized full path
            return full;
        }

        private static string GetAbsolutePath(string relativePath)
        {
            // Normalize root
            string root = (_root ?? string.Empty).Replace('\\', '/').TrimEnd('/');

            if (string.IsNullOrEmpty(relativePath)) return root;

            // Normalize relative and remove any leading slashes
            string rel = relativePath.Replace('\\', '/').TrimStart('/');

            // If root is empty, return rel; otherwise join with single '/'
            return string.IsNullOrEmpty(root) ? rel : (root + "/" + rel);
        }

        public static async Task<List<string>> GetFilesAsync(string path, CancellationTokenSource cts = null) => await GetEntriesAsync(path, includeFiles: true, includeDirs: false, cts);

        public static async Task<List<string>> GetFilesAsync(string path, CancellationTokenSource cts = null, Action<int> reportProgress = null) => await GetEntriesAsync(path, includeFiles: true, includeDirs: false, cts, reportProgress);

        public static async Task<List<string>> GetDirectoriesAsync(string path, CancellationTokenSource cts = null) => await GetEntriesAsync(path, includeFiles: false, includeDirs: true, cts);

        public static async Task<List<string>> GetDirectoriesAsync(string path, CancellationTokenSource cts = null, Action<int> reportProgress = null) => await GetEntriesAsync(path, includeFiles: false, includeDirs: true, cts, reportProgress);

        public static async Task<List<string>> SearchFilesAsync(string pattern, string path = null, ListView list = null, CancellationTokenSource cts = null)
        {
            cts ??= new();

            // Normalize wildcard for GitHub Query API
            string githubPattern = pattern.Replace("\\", "/");

            cts.Token.ThrowIfCancellationRequested();

            SearchCodeRequest request = new($"repo:{_owner}/{_repo} filename:{githubPattern}");

            List<string> resultList = [];

            SearchCodeResult search = await Program.GitHub.Client.Search.SearchCode(request);

            cts.Token.ThrowIfCancellationRequested();

            foreach (var item in search.Items)
            {
                cts.Token.ThrowIfCancellationRequested();
                // GitHub returns paths relative to repo root already.
                string relative = item.Path.Replace("\\", "/");
                resultList.Add(relative);
            }

            // Optional ListView output
            if (list != null)
            {
                list.BeginUpdate();
                list.Items.Clear();
                foreach (string file in resultList)
                {
                    cts.Token.ThrowIfCancellationRequested();
                    list.Items.Add(new ListViewItem(file) { Tag = file });
                }
                list.EndUpdate();
            }

            return resultList;
        }

        public static async Task<bool> FileExistsAsync(string path, CancellationTokenSource cts = null)
        {
            cts ??= new();
            cts.Token.ThrowIfCancellationRequested();

            var file = await GetInfoCachedAsync(path, forceRefresh: true, cts); // always check online
            return file?.Type == ElementType.File;
        }

        public static async Task<bool> DirectoryExistsAsync(string path, CancellationTokenSource cts = null)
        {
            cts ??= new();
            cts.Token.ThrowIfCancellationRequested();

            var dir = await GetInfoCachedAsync(path, forceRefresh: true, cts); // always check online
            return dir?.Type == ElementType.Dir;
        }

        public static async Task<string> ReadFileAsync(string path, Encoding encoding = null, CancellationTokenSource cts = null)
        {
            cts ??= new();
            encoding ??= Encoding.UTF8;

            var entry = await GetInfoCachedAsync(path, forceRefresh: true, cts);
            if (entry?.Type != ElementType.File || string.IsNullOrEmpty(entry.Content?.Content))
                return null;

            cts.Token.ThrowIfCancellationRequested();
            byte[] bytes = Convert.FromBase64String(entry.Content.Content);
            return encoding.GetString(bytes);
        }

        public static async Task<byte[]> ReadFileBytesAsync(string path, CancellationTokenSource cts = null)
        {
            cts ??= new();

            var entry = await GetInfoCachedAsync(path, forceRefresh: true, cts);
            if (entry?.Type != ElementType.File || string.IsNullOrEmpty(entry.Content?.Content))
                return null;

            cts.Token.ThrowIfCancellationRequested();
            return Convert.FromBase64String(entry.Content.Content);
        }

        public static async Task WriteFileAsync(string githubPath, string contentOrPath, bool isLocalFile = false, string commitMessage = null, CancellationTokenSource cts = null, Action<int> reportProgress = null)
        {
            cts ??= new();
            reportProgress?.Invoke(0);
            commitMessage ??= $"Updated `{githubPath}` by {_owner}";

            try
            {
                IRepositoryContentsClient client = Program.GitHub.Client.Repository.Content;
                var existing = await GetInfoCachedAsync(githubPath, forceRefresh: true, cts);

                string base64Content;

                if (isLocalFile)
                {
                    using FileStream fs = new(contentOrPath, System.IO.FileMode.Open, FileAccess.Read, FileShare.Read);
                    long total = fs.Length;
                    long readBytes = 0;
                    const int chunkSize = 81920; // 80 KB chunks
                    var buffer = new byte[chunkSize];
                    var sb = new StringBuilder();

                    int bytesRead;
                    while ((bytesRead = await fs.ReadAsync(buffer, 0, buffer.Length, cts.Token)) > 0)
                    {
                        byte[] actual = buffer;
                        if (bytesRead != buffer.Length)
                        {
                            actual = new byte[bytesRead];
                            Array.Copy(buffer, actual, bytesRead);
                        }
                        sb.Append(Convert.ToBase64String(actual));
                        readBytes += bytesRead;
                        reportProgress?.Invoke((int)(readBytes * 100L / total));
                    }

                    base64Content = sb.ToString();
                }
                else
                {
                    base64Content = Convert.ToBase64String(Encoding.UTF8.GetBytes(contentOrPath));
                    reportProgress?.Invoke(100);
                }

                if (existing?.Content == null)
                {
                    await client.CreateFile(_owner, _repo, githubPath, new CreateFileRequest(commitMessage, base64Content));
                }
                else
                {
                    await client.UpdateFile(_owner, _repo, githubPath, new UpdateFileRequest(commitMessage, base64Content, existing.Content.Sha));
                }

                _infoCache.TryRemove(githubPath, out _);
                var parent = Path.GetDirectoryName(githubPath)?.Replace('\\', '/');
                if (!string.IsNullOrEmpty(parent)) _dirsCache.TryRemove(parent, out _);

                reportProgress?.Invoke(100);
            }
            catch (OperationCanceledException) { throw; }
            catch (Exception ex)
            {
                Program.Log?.Write(LogEventLevel.Error, $"WriteFileAsync failed for {githubPath}", ex);
                reportProgress?.Invoke(0);
                throw;
            }
        }

        public static async Task CreateDirectoryAsync(string path, CancellationTokenSource cts = null, Action<int> reportProgress = null)
        {
            cts ??= new();
            reportProgress?.Invoke(0);

            try
            {
                string normalizedPath = path.TrimEnd('\\', '/').Replace('\\', '/');
                string placeholder = $"{normalizedPath}/.gitkeep";

                cts.Token.ThrowIfCancellationRequested();

                // Only create the placeholder if it doesn't exist on GitHub
                if (!await FileExistsAsync(placeholder, cts))
                {
                    // Write empty file directly online
                    await WriteFileAsync(
                        placeholder,
                        string.Empty,
                        isLocalFile: false,
                        commitMessage: $"Create directory `{normalizedPath}` by {_owner}",
                        cts: cts,
                        reportProgress: progress => reportProgress?.Invoke(progress)
                    );
                }

                reportProgress?.Invoke(100);
            }
            catch (OperationCanceledException)
            {
                reportProgress?.Invoke(0);
            }
            catch (Exception ex)
            {
                Program.Log?.Write(LogEventLevel.Error, $"CreateDirectoryAsync failed for {path}", ex);
                reportProgress?.Invoke(0);
                throw;
            }
        }

        public static async Task MoveDirectoryAsync(string sourcePath, string destPath, Action<int> reportProgress = null, CancellationTokenSource cts = null)
        {
            cts ??= new();
            reportProgress?.Invoke(0);

            try
            {
                // Enumerate all entries once (files + directories)
                var entries = new List<Entry>();
                await foreach (var entry in EnumerateAsync<Entry>(sourcePath, EnumerateType.Both, recursive: true, ct: cts.Token))
                {
                    entries.Add(entry);
                }

                int total = entries.Count;
                int processed = 0;

                foreach (var entry in entries)
                {
                    cts.Token.ThrowIfCancellationRequested();
                    string relative = entry.Path.Substring(sourcePath.Length).TrimStart('/', '\\');
                    string targetPath = $"{destPath}/{relative}";

                    if (entry.Type == ElementType.Dir)
                        await CreateDirectoryAsync(targetPath);
                    else
                        await CopyFileAsync(entry.Path, targetPath, null, cts);

                    processed++;
                    reportProgress?.Invoke((int)((long)processed * 100 / total));
                }

                // Delete the source directory recursively
                await DeleteDirectoryAsync(sourcePath, reportProgress, cts);

                reportProgress?.Invoke(100);
            }
            catch (OperationCanceledException) { throw; }
            catch (Exception ex)
            {
                Program.Log?.Write(LogEventLevel.Error, $"MoveDirectoryAsync failed from {sourcePath} to {destPath}", ex);
                reportProgress?.Invoke(0);
                throw;
            }
        }

        public static async Task MoveFileAsync(string sourcePath, string destPath, Action<int> reportProgress = null, CancellationTokenSource cts = null)
        {
            cts ??= new();
            reportProgress?.Invoke(0);

            await CopyFileAsync(sourcePath, destPath, reportProgress, cts);

            cts?.Token.ThrowIfCancellationRequested();

            await DeleteFileAsync(sourcePath, cts, reportProgress);
        }

        public static async Task CopyFileAsync(string sourcePath, string destPath, Action<int> reportProgress = null, CancellationTokenSource cts = null)
        {
            // 1. Get the file info (SHA)
            var file = await Program.GitHub.Client.Repository.Content.GetAllContentsByRef(_owner, _repo, sourcePath, _branch);
            string sha = file[0].Sha;

            // 2. Get latest commit and tree
            var masterRef = await Program.GitHub.Client.Git.Reference.Get(_owner, _repo, $"heads/{_branch}");
            var latestCommit = await Program.GitHub.Client.Git.Commit.Get(_owner, _repo, masterRef.Object.Sha);

            // 3. Create a new tree with the same SHA at new path
            var newTree = new NewTree { BaseTree = latestCommit.Tree.Sha };
            newTree.Tree.Add(new NewTreeItem { Path = destPath, Mode = "100644", Type = TreeType.Blob, Sha = sha });

            var createdTree = await Program.GitHub.Client.Git.Tree.Create(_owner, _repo, newTree);

            // 4. Commit
            var newCommit = new NewCommit($"Copy `{sourcePath}` → `{destPath}` by {_owner}", createdTree.Sha, latestCommit.Sha);
            var commit = await Program.GitHub.Client.Git.Commit.Create(_owner, _repo, newCommit);

            // 5. Update branch
            await Program.GitHub.Client.Git.Reference.Update(_owner, _repo, $"heads/{_branch}", new ReferenceUpdate(commit.Sha));
        }

        public static async Task CopyDirectoryAsync(string sourcePath, string destPath, Action<int> reportProgress = null, CancellationTokenSource cts = null)
        {
            cts ??= new();
            reportProgress?.Invoke(0);

            try
            {
                // Enumerate all entries once
                var entries = new List<Entry>();
                await foreach (var entry in EnumerateAsync<Entry>(sourcePath, EnumerateType.Both, recursive: true, ct: cts.Token))
                    entries.Add(entry);

                int total = entries.Count;
                int processed = 0;

                foreach (var entry in entries)
                {
                    cts.Token.ThrowIfCancellationRequested();
                    string relative = entry.Path.Substring(sourcePath.Length).TrimStart('/', '\\');
                    string targetPath = $"{destPath}/{relative}";

                    if (entry.Type == ElementType.Dir)
                    {
                        await CreateDirectoryAsync(targetPath);
                    }
                    else
                    {
                        await CopyFileAsync(entry.Path, targetPath, null, cts);
                    }

                    processed++;
                    reportProgress?.Invoke((int)((long)processed * 100 / total));
                }

                reportProgress?.Invoke(100);
            }
            catch (OperationCanceledException) { throw; }
            catch (Exception ex)
            {
                Program.Log?.Write(LogEventLevel.Error, $"CopyDirectoryAsync failed from {sourcePath} to {destPath}", ex);
                reportProgress?.Invoke(0);
                throw;
            }
        }

        public static async Task DeleteFileAsync(string path, CancellationTokenSource cts = null, Action<int> reportProgress = null)
        {
            cts ??= new();
            reportProgress?.Invoke(0);

            try
            {
                cts.Token.ThrowIfCancellationRequested();
                var file = await GetInfoCachedAsync(path, forceRefresh: true, cts);

                if (file == null || file.Type != ElementType.File)
                {
                    reportProgress?.Invoke(100);
                    return;
                }

                await Program.GitHub.Client.Repository.Content.DeleteFile(_owner, _repo, path,
                    new DeleteFileRequest($"{_owner} deleted `{path}`", file.Content.Sha));

                _infoCache.TryRemove(path, out _);

                // Invalidate parent directory cache
                var parent = Path.GetDirectoryName(path)?.Replace('\\', '/');
                if (!string.IsNullOrEmpty(parent)) _dirsCache.TryRemove(parent, out _);

                reportProgress?.Invoke(100);
            }
            catch (OperationCanceledException) { throw; }
            catch (Exception ex)
            {
                Program.Log?.Write(LogEventLevel.Error, $"DeleteFileAsync failed for {path}", ex);
                reportProgress?.Invoke(0);
                throw;
            }
        }

        public static async Task DeleteDirectoryAsync(string path, Action<int> reportProgress = null, CancellationTokenSource cts = null)
        {
            cts ??= new();
            reportProgress?.Invoke(0);

            try
            {
                // Enumerate all entries recursively
                var entries = new List<Entry>();
                await foreach (var entry in EnumerateAsync<Entry>(path, EnumerateType.Both, recursive: true, ct: cts.Token)) entries.Add(entry);

                // Delete files first (leaves to root)
                var files = entries.Where(e => e.Type == ElementType.File).OrderByDescending(e => e.Path.Length).ToList();
                int total = files.Count;
                int processed = 0;

                foreach (var file in files)
                {
                    cts.Token.ThrowIfCancellationRequested();
                    await DeleteFileAsync(file.Path, cts, progress => { });
                    processed++;
                    reportProgress?.Invoke((int)((long)processed * 100 / total));
                }

                // Optionally remove placeholder file in the directory itself
                var placeholder = $"{path.TrimEnd('/', '\\')}/.gitkeep";
                if (await FileExistsAsync(placeholder, cts))
                    await DeleteFileAsync(placeholder, cts, progress => { });

                // Invalidate directory cache
                _dirsCache.TryRemove(path, out _);

                reportProgress?.Invoke(100);
            }
            catch (OperationCanceledException) { throw; }
            catch (Exception ex)
            {
                Program.Log?.Write(LogEventLevel.Error, $"DeleteDirectoryAsync failed for {path}", ex);
                reportProgress?.Invoke(0);
                throw;
            }
        }

        public static async Task<IAsyncEnumerable<Entry>> EnumerateEntriesAsync(string path, EnumerateType type = EnumerateType.Both, string searchPattern = "*", bool recursive = false, [EnumeratorCancellation] CancellationToken ct = default, bool forceRefresh = true)
            => EnumerateAsync<Entry>(path, type, searchPattern, recursive, ct, forceRefresh);

        public static async IAsyncEnumerable<T> EnumerateAsync<T>(string path, EnumerateType type = EnumerateType.Both, string searchPattern = "*", bool recursive = false, [EnumeratorCancellation] CancellationToken ct = default, bool forceRefresh = true) where T : class
        {
            Regex regex = null;
            if (!string.IsNullOrEmpty(searchPattern) && searchPattern != "*") regex = new("^" + Regex.Escape(searchPattern).Replace("\\*", ".*").Replace("\\?", ".") + "$", RegexOptions.IgnoreCase);

            List<Entry> entries = await GetEntriesCachedAsync(path, forceRefresh: forceRefresh);

            foreach (var entry in entries)
            {
                ct.ThrowIfCancellationRequested();

                bool isFile = entry.Type == ElementType.File;
                bool isDir = entry.Type == ElementType.Dir;
                string fileName = Path.GetFileName(entry.Path);

                if ((isFile && (type.HasFlag(EnumerateType.Files)) && (regex == null || regex.IsMatch(fileName))) || (isDir && type.HasFlag(EnumerateType.Directories)))
                {
                    if (typeof(T) == typeof(string)) yield return entry.Path as T;
                    else if (typeof(T) == typeof(Entry)) yield return entry as T;
                    else throw new InvalidOperationException($"Unsupported type {typeof(T)} for EnumerateAsync.");
                }

                if (recursive && isDir) await foreach (var sub in EnumerateAsync<T>(entry.Path, type, searchPattern, true, ct, forceRefresh)) yield return sub;
            }
        }
    }
}