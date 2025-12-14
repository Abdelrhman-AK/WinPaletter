using Octokit;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

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

            if (string.IsNullOrWhiteSpace(path)) path = string.Empty; // root

            path = NormalizePath(path);

            try
            {
                var contents = await Program.GitHub.Client.Repository.Content.GetAllContentsByRef(_owner, GitHub.Repository.repositoryName, path, Repository.branch);

                if (contents.Count == 0) return null;

                string name = string.IsNullOrEmpty(path) ? GitHub.Repository.repositoryName : path.Substring(path.LastIndexOf('/') + 1);
                var first = contents[0];

                // GitHub does not return a directory object, so synthesize it

                RepositoryContent rc = new(
                    name: name,
                    path: path,
                    sha: first.Sha,
                    size: 0,
                    type: ContentType.Dir,
                    downloadUrl: null,
                    url: first.Url,
                    gitUrl: first.GitUrl,
                    htmlUrl: first.HtmlUrl,
                    encoding: null,
                    encodedContent: null,
                    target: null,
                    submoduleGitUrl: null);

                return await Entry.FromRepositoryContent(rc, path);
            }
            catch (NotFoundException)
            {
                return null; // path does not exist or is a file
            }
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

                var content = await Program.GitHub.Client.Repository.Content.GetAllContentsByRef(_owner, GitHub.Repository.repositoryName, path, GitHub.Repository.branch);
                return Convert.FromBase64String(content[0].Content);
            }
            catch (OperationCanceledException) { throw; }
        }

        public static async Task<string> ReadFileAsync(string path, CancellationTokenSource cts = null)
        {
            var bytes = await ReadFileBytesAsync(path, cts);
            return Encoding.UTF8.GetString(bytes);
        }

        public static Task WriteTextAsync(string githubPath, string content, string commitMessage = null, CancellationTokenSource cts = null, Action<int> reportProgress = null)
        {
            return WriteFileAsync(githubPath, content, isLocalFile: false, commitMessage: commitMessage, cts: cts, reportProgress: reportProgress);
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

                // Only read bytes for local files
                byte[] fileBytes = null;
                string contentToSend = contentOrPath;

                if (isLocalFile)
                {
                    using var fs = new FileStream(contentOrPath, System.IO.FileMode.Open, FileAccess.Read, FileShare.Read);
                    fileBytes = new byte[fs.Length];
                    int offset = 0;
                    while (offset < fs.Length)
                    {
                        int read = await fs.ReadAsync(fileBytes, offset, (int)(fs.Length - offset), cts.Token);
                        if (read == 0) break;
                        offset += read;
                    }

                    // Convert local file bytes to string for GitHub
                    contentToSend = Encoding.UTF8.GetString(fileBytes);
                }

                // Then pass the string directly:
                if (existing == null)
                {
                    await client.CreateFile(
                        _owner,
                        GitHub.Repository.repositoryName,
                        normalizedPath,
                        new CreateFileRequest(commitMessage, contentToSend)
                        {
                            Branch = GitHub.Repository.branch
                        });
                }
                else
                {
                    string sha = existing.Content.Sha;
                    if (string.IsNullOrEmpty(sha))
                    {
                        var remote = await client.GetAllContentsByRef(
                            _owner,
                            GitHub.Repository.repositoryName,
                            normalizedPath,
                            GitHub.Repository.branch);
                        sha = remote[0].Sha;
                    }

                    await client.UpdateFile(
                        _owner,
                        GitHub.Repository.repositoryName,
                        normalizedPath,
                        new UpdateFileRequest(commitMessage, contentToSend, sha)
                        {
                            Branch = GitHub.Repository.branch
                        });
                }

                // Fetch updated content and update cache
                var updatedContent = await client.GetAllContentsByRef(_owner, GitHub.Repository.repositoryName, normalizedPath, GitHub.Repository.branch);
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
            //catch (Exception ex)
            //{
            //    Program.Log?.Write(LogEventLevel.Error, $"WriteFileAsync failed for `{normalizedPath}`", ex);
            //    reportProgress?.Invoke(0);
            //    throw;
            //}
        }

        public static async Task CreateDirectoryAsync(
       string path,
       ListViewItem listViewItem = null,
       CancellationTokenSource cts = null)
        {
            cts ??= new();
            cts.Token.ThrowIfCancellationRequested();

            string normalizedPath = NormalizePath(path);
            string parentDir = GetParent(normalizedPath);

            try
            {
                // 1️⃣ Check if directory already exists in cache
                if (await GetEntryCachedAsync(normalizedPath, forceRefresh: true, cts: cts) != null)
                    return;

                // 2️⃣ Create via .gitkeep
                await WriteFileAsync(
                    $"{normalizedPath}/.gitkeep",
                    string.Empty,
                    false,
                    $"Create directory {normalizedPath} by {_owner}",
                    cts
                );

                // 3️⃣ Invalidate ONLY fetch caches
                ClearEntryCache(normalizedPath);
                DirectoryCache.TryRemove(parentDir, out _);

                // 4️⃣ Fetch fresh RepositoryContent
                IReadOnlyList<RepositoryContent> parentContents = await GetContentsCachedAsync(parentDir);
                RepositoryContent dirContent = parentContents
                    .FirstOrDefault(c => c.Type == ContentType.Dir && string.Equals(c.Path, normalizedPath, StringComparison.OrdinalIgnoreCase));

                if (dirContent == null)
                    throw new InvalidOperationException("GitHub did not return new directory.");

                // 5️⃣ Patch DirectoryMap (DO NOT REMOVE)
                if (!DirectoryMap.TryGetValue(parentDir, out var list))
                {
                    list = new List<RepositoryContent>();
                    DirectoryMap[parentDir] = list;
                }
                if (!list.Any(c => c.Path.Equals(dirContent.Path, StringComparison.OrdinalIgnoreCase)))
                    list.Add(dirContent);

                // 6️⃣ Store Entry
                StoreEntryInCache(new Entry
                {
                    Path = normalizedPath,
                    Type = EntryType.Dir,
                    Content = dirContent,
                    Children = new List<Entry>(),
                    FetchedAt = DateTime.UtcNow
                });

                if (listViewItem is not null)
                    listViewItem.Tag = dirContent;

                Program.Log?.Write(LogEventLevel.Information, $"Directory {normalizedPath} created");
            }
            catch (OperationCanceledException)
            {
                // Ignore cancellations
            }
            catch (Exception ex)
            {
                Program.Log?.Write(LogEventLevel.Error, $"CreateDirectoryAsync failed for {normalizedPath}", ex);
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
                var masterRef = await Program.GitHub.Client.Git.Reference.Get(_owner, GitHub.Repository.repositoryName, $"heads/{GitHub.Repository.branch}");
                var latestCommit = await Program.GitHub.Client.Git.Commit.Get(_owner, GitHub.Repository.repositoryName, masterRef.Object.Sha);

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
                var createdTree = await Program.GitHub.Client.Git.Tree.Create(_owner, GitHub.Repository.repositoryName, newTree);
                var newCommit = new NewCommit($"Copy `{sourceNormalized}` → `{destNormalized}` by {_owner}", createdTree.Sha, latestCommit.Sha);
                var commit = await Program.GitHub.Client.Git.Commit.Create(_owner, GitHub.Repository.repositoryName, newCommit);

                // Update branch reference
                await Program.GitHub.Client.Git.Reference.Update(_owner, GitHub.Repository.repositoryName, $"heads/{GitHub.Repository.branch}", new ReferenceUpdate(commit.Sha));

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

                var masterRef = await Program.GitHub.Client.Git.Reference.Get(_owner, GitHub.Repository.repositoryName, $"heads/{GitHub.Repository.branch}");
                var latestCommit = await Program.GitHub.Client.Git.Commit.Get(_owner, GitHub.Repository.repositoryName, masterRef.Object.Sha);
                var newTree = new NewTree { BaseTree = latestCommit.Tree.Sha };

                int totalFiles = entries.Count(e => e.Type == EntryType.File);
                int processed = 0;

                foreach (var entry in entries)
                {
                    string relative = entry.Path.Substring(sourceNormalized.Length).TrimStart('/', '\\');
                    string targetPath = NormalizePath($"{destNormalized}/{relative}");

                    if (entry.Type == EntryType.File)
                    {
                        var content = await Program.GitHub.Client.Repository.Content.GetAllContentsByRef(_owner, GitHub.Repository.repositoryName, entry.Path, GitHub.Repository.branch);
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

                var createdTree = await Program.GitHub.Client.Git.Tree.Create(_owner, GitHub.Repository.repositoryName, newTree);
                var newCommit = new NewCommit($"Copy directory `{sourceNormalized}` → `{destNormalized}` by {_owner}", createdTree.Sha, latestCommit.Sha);
                var commit = await Program.GitHub.Client.Git.Commit.Create(_owner, GitHub.Repository.repositoryName, newCommit);
                await Program.GitHub.Client.Git.Reference.Update(_owner, GitHub.Repository.repositoryName, $"heads/{GitHub.Repository.branch}", new ReferenceUpdate(commit.Sha));

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

                var masterRef = await Program.GitHub.Client.Git.Reference.Get(_owner, GitHub.Repository.repositoryName, $"heads/{GitHub.Repository.branch}");
                var latestCommit = await Program.GitHub.Client.Git.Commit.Get(_owner, GitHub.Repository.repositoryName, masterRef.Object.Sha);

                var newTree = new NewTree { BaseTree = latestCommit.Tree.Sha };
                newTree.Tree.Add(new NewTreeItem { Path = destNormalized, Mode = "100644", Type = TreeType.Blob, Sha = file.Content.Sha });
                newTree.Tree.Add(new NewTreeItem { Path = sourceNormalized, Mode = "100644", Type = TreeType.Blob, Sha = null }); // delete old

                var createdTree = await Program.GitHub.Client.Git.Tree.Create(_owner, GitHub.Repository.repositoryName, newTree);
                var newCommit = new NewCommit($"Move `{sourceNormalized}` → `{destNormalized}` by {_owner}", createdTree.Sha, latestCommit.Sha);
                var commit = await Program.GitHub.Client.Git.Commit.Create(_owner, GitHub.Repository.repositoryName, newCommit);

                await Program.GitHub.Client.Git.Reference.Update(_owner, GitHub.Repository.repositoryName, $"heads/{GitHub.Repository.branch}", new ReferenceUpdate(commit.Sha));

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

            if (Program.GitHub.Client == null) throw new InvalidOperationException("GitHub client is null.");
            if (string.IsNullOrEmpty(_owner)) throw new InvalidOperationException("_owner is null or empty.");
            if (string.IsNullOrEmpty(GitHub.Repository.repositoryName)) throw new InvalidOperationException("Repository name is null or empty.");
            if (string.IsNullOrEmpty(GitHub.Repository.branch)) throw new InvalidOperationException("Branch is null or empty.");

            try
            {
                cts?.Token.ThrowIfCancellationRequested();

                // 1️⃣ Enumerate all entries recursively
                var entries = new List<Entry>();
                await foreach (var entry in EnumerateEntriesAsync(sourceNormalized, EnumerateType.Both, recursive: true, ct: cts.Token))
                {
                    if (entry != null && !string.IsNullOrEmpty(entry.Path))
                        entries.Add(entry);
                }

                if (entries.Count == 0)
                {
                    await CreateDirectoryAsync(destNormalized, cts: cts);
                    reportProgress?.Invoke(100);
                    return;
                }

                // 2️⃣ Get latest commit and tree
                var masterRef = await Program.GitHub.Client.Git.Reference.Get(_owner, GitHub.Repository.repositoryName, $"heads/{GitHub.Repository.branch}");
                var latestCommit = await Program.GitHub.Client.Git.Commit.Get(_owner, GitHub.Repository.repositoryName, masterRef.Object.Sha);

                // 3️⃣ Build new tree including moved files and deletion of old paths
                var newTree = new NewTree { BaseTree = latestCommit.Tree.Sha };
                int totalFiles = entries.Count(e => e.Type == EntryType.File);
                int processed = 0;

                foreach (var entry in entries)
                {
                    string relative = entry.Path.Substring(sourceNormalized.Length).TrimStart('/', '\\');
                    string targetPath = NormalizePath($"{destNormalized}/{relative}");

                    if (entry.Type == EntryType.File)
                    {
                        // Get existing file SHA
                        var contents = await Program.GitHub.Client.Repository.Content.GetAllContentsByRef(_owner, GitHub.Repository.repositoryName, entry.Path, GitHub.Repository.branch);
                        if (contents.Count == 0) continue;

                        // Add new path with SHA
                        newTree.Tree.Add(new NewTreeItem { Path = targetPath, Mode = "100644", Type = TreeType.Blob, Sha = contents[0].Sha });
                        // Remove old path
                        newTree.Tree.Add(new NewTreeItem { Path = entry.Path, Mode = "100644", Type = TreeType.Blob, Sha = string.Empty });

                        processed++;
                        reportProgress?.Invoke(processed * 100 / totalFiles);
                    }
                    else
                    {
                        // Just create directory in cache / maps
                        var dirEntry = new Entry { Path = targetPath, Type = EntryType.Dir, Content = null, FetchedAt = DateTime.UtcNow };
                        _cache[targetPath] = (dirEntry, DateTime.UtcNow);
                        UpdateDirectoryMaps(Path.GetDirectoryName(targetPath), dirEntry);

                        // Remove old directory from cache / maps
                        RemoveDirectoryMaps(Path.GetDirectoryName(entry.Path), entry);
                        _cache.TryRemove(entry.Path, out _);
                    }
                }

                // 4️⃣ Commit new tree
                var createdTree = await Program.GitHub.Client.Git.Tree.Create(_owner, GitHub.Repository.repositoryName, newTree);
                var newCommit = new NewCommit($"Move directory `{sourceNormalized}` → `{destNormalized}` by {_owner}", createdTree.Sha, latestCommit.Sha);
                var commit = await Program.GitHub.Client.Git.Commit.Create(_owner, GitHub.Repository.repositoryName, newCommit);

                // 5️⃣ Update branch reference
                await Program.GitHub.Client.Git.Reference.Update(_owner, GitHub.Repository.repositoryName, $"heads/{GitHub.Repository.branch}", new ReferenceUpdate(commit.Sha));

                reportProgress?.Invoke(100);
            }
            catch (OperationCanceledException)
            {
                reportProgress?.Invoke(0);
                throw;
            }
            catch (Exception ex)
            {
                Forms.BugReport.ThrowError(ex);
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
                    GitHub.Repository.repositoryName,
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

            string normalizedPath = NormalizePath(path);

            if (Program.GitHub.Client == null) throw new InvalidOperationException("GitHub client is null.");
            if (string.IsNullOrEmpty(_owner)) throw new InvalidOperationException("_owner is null or empty.");
            if (string.IsNullOrEmpty(GitHub.Repository.repositoryName)) throw new InvalidOperationException("Repository name is null or empty.");
            if (string.IsNullOrEmpty(GitHub.Repository.branch)) throw new InvalidOperationException("Branch is null or empty.");

            try
            {
                cts.Token.ThrowIfCancellationRequested();

                // 1️⃣ Get the branch reference and latest commit
                var branchRef = await Program.GitHub.Client.Git.Reference.Get(_owner, GitHub.Repository.repositoryName, $"heads/{GitHub.Repository.branch}");
                var latestCommit = await Program.GitHub.Client.Git.Commit.Get(_owner, GitHub.Repository.repositoryName, branchRef.Object.Sha);

                // 2️⃣ Get the full repository tree recursively
                var tree = await Program.GitHub.Client.Git.Tree.GetRecursive(_owner, GitHub.Repository.repositoryName, latestCommit.Tree.Sha);

                // 3️⃣ Collect all files under the directory to delete
                var filesToDelete = tree.Tree
                    .Where(t => t.Type == TreeType.Blob && NormalizePath(t.Path).StartsWith(normalizedPath, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                if (!filesToDelete.Any())
                {
                    reportProgress?.Invoke(100);
                    return; // nothing to delete
                }

                int total = filesToDelete.Count;
                int processed = 0;

                // 4️⃣ Build new tree entries with SHA = null to delete files
                var newTree = new NewTree { BaseTree = latestCommit.Tree.Sha };
                foreach (var file in filesToDelete)
                {
                    newTree.Tree.Add(new NewTreeItem
                    {
                        Path = file.Path,
                        Mode = file.Mode,
                        Type = TreeType.Blob,
                        Sha = string.Empty,
                    });

                    processed++;
                    reportProgress?.Invoke(processed * 100 / total);
                }

                // 5️⃣ Create the new tree
                var createdTree = await Program.GitHub.Client.Git.Tree.Create(_owner, GitHub.Repository.repositoryName, newTree);

                // 6️⃣ Create a commit pointing to the new tree
                var commitMessage = $"Delete directory `{normalizedPath}` recursively by {_owner}";
                var newCommit = new NewCommit(commitMessage, createdTree.Sha, latestCommit.Sha);
                var commit = await Program.GitHub.Client.Git.Commit.Create(_owner, GitHub.Repository.repositoryName, newCommit);

                // 7️⃣ Update branch reference to the new commit
                await Program.GitHub.Client.Git.Reference.Update(
                    _owner,
                    GitHub.Repository.repositoryName,
                    $"heads/{GitHub.Repository.branch}",
                    new ReferenceUpdate(commit.Sha)
                );

                reportProgress?.Invoke(100);
            }
            catch (OperationCanceledException)
            {
                reportProgress?.Invoke(0);
                throw;
            }
            catch (Exception ex)
            {
                Forms.BugReport.ThrowError(ex);
                Program.Log?.Write(LogEventLevel.Error, $"DeleteDirectoryAsync failed for {path}", ex);
                reportProgress?.Invoke(0);
                throw;
            }
        }

        public static async IAsyncEnumerable<Entry> EnumerateEntriesAsync(
            string path,
            EnumerateType type = EnumerateType.Both,
            string searchPattern = "*",
            bool recursive = false,
            [EnumeratorCancellation] CancellationToken ct = default)
        {
            if (Program.GitHub.Client == null)
                throw new InvalidOperationException("GitHub client is null.");
            if (string.IsNullOrEmpty(_owner) || string.IsNullOrEmpty(GitHub.Repository.repositoryName) || string.IsNullOrEmpty(GitHub.Repository.branch))
                throw new InvalidOperationException("Owner, repository, or branch not set.");

            path = NormalizePath(path); // normalize slashes

            // Convert searchPattern to regex if needed
            Regex regex = null;
            if (!string.IsNullOrEmpty(searchPattern) && searchPattern != "*")
                regex = new Regex("^" + Regex.Escape(searchPattern).Replace("\\*", ".*").Replace("\\?", ".") + "$", RegexOptions.IgnoreCase);

            IReadOnlyList<RepositoryContent> contents;
            try
            {
                // GitHub API: get contents at path
                contents = await Program.GitHub.Client.Repository.Content.GetAllContentsByRef(
                    _owner, GitHub.Repository.repositoryName, path, GitHub.Repository.branch);
            }
            catch (Octokit.NotFoundException)
            {
                // path does not exist
                yield break;
            }

            foreach (var item in contents)
            {
                ct.ThrowIfCancellationRequested();
                if (item == null || string.IsNullOrEmpty(item.Path)) continue;

                EntryType entryType = item.Type == Octokit.ContentType.Dir ? EntryType.Dir : EntryType.File;
                var entry = new Entry { Path = NormalizePath(item.Path), Type = entryType, Content = item, FetchedAt = DateTime.UtcNow };

                // Filter by type and search pattern
                bool match =
                    (entry.Type == EntryType.File && type.HasFlag(EnumerateType.Files) &&
                     (regex == null || regex.IsMatch(Path.GetFileName(entry.Path)))) ||
                    (entry.Type == EntryType.Dir && type.HasFlag(EnumerateType.Directories));

                if (match)
                    yield return entry;

                // Recurse into directories
                if (recursive && entry.Type == EntryType.Dir)
                {
                    await foreach (var sub in EnumerateEntriesAsync(entry.Path, type, searchPattern, true, ct))
                    {
                        yield return sub;
                    }
                }
            }
        }
    }
}
