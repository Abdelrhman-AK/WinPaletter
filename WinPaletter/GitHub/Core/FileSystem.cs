using Octokit;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static WinPaletter.GitHub.FileSystem;

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
            path = path.TrimEnd('/');
            int index = path.LastIndexOf('/');
            return index < 0 ? path : path.Substring(index + 1);
        }

        public static string ParentDirectoryName(string path)
        {
            if (string.IsNullOrEmpty(path)) return string.Empty;
            path = path.TrimEnd('/');                  // remove trailing slash
            int lastSlash = path.LastIndexOf('/');
            if (lastSlash < 0) return string.Empty;   // no parent
            int secondLastSlash = path.LastIndexOf('/', lastSlash - 1);
            if (secondLastSlash < 0) return path.Substring(0, lastSlash); // root-level folder
            return path.Substring(secondLastSlash + 1, lastSlash - secondLastSlash - 1);
        }

        /// <summary>
        /// Gets the correct root to be used in navigation
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string UppermostRoot(string path)
        {
            if (string.IsNullOrEmpty(path) || path == _root)
                return null;

            int lastSlash = path.LastIndexOf('/');
            if (lastSlash < _root.Length) return _root; // important: < instead of <=

            return path.Substring(0, lastSlash);
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

        public static string NormalizePath(string path, bool trimEnd = true, bool trimStart = true)
        {
            if (string.IsNullOrEmpty(path)) return string.Empty;

            string result = path.Replace('\\', '/');

            if (trimStart) result = result.TrimStart('/');
            if (trimEnd) result = result.TrimEnd('/');

            return result;
        }

        public static string GetParent(string path)
        {
            int i = path.LastIndexOf('/');
            string result = i < 0 ? string.Empty : path.Substring(0, i);

            return result;
        }

        /// <summary>
        /// Extracts the extension (including the dot) from a file path.
        /// </summary>
        /// <param name="filePath">The full file path or GitHub file path.</param>
        /// <returns>The file extension including the dot (e.g., ".txt"), or empty string if none exists.</returns>
        public static string GetExtension(string filePath)
        {
            if (string.IsNullOrEmpty(filePath)) return string.Empty;

            int lastSlash = filePath.LastIndexOfAny(['/', '\\']);
            int lastDot = filePath.LastIndexOf('.');

            // No dot or dot is before last slash -> no extension
            if (lastDot < 0 || lastDot < lastSlash) return string.Empty;

            return filePath.Substring(lastDot);
        }

        public static bool IsValidGitWindowsUrlSafeName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return false;

            if (InvalidFileNames.Contains(name, StringComparer.OrdinalIgnoreCase)) return false;
            if (name.StartsWith(" ") || name.EndsWith(" ") || name.StartsWith(".") || name.EndsWith(".")) return false;

            // Convert string array of single-character strings to char[]
            char[] invalidChars = [.. InvalidFileNameChars.Select(s => s[0])];
            return name.IndexOfAny(invalidChars) < 0;
        }

        public static readonly string[] InvalidFileNameChars = ["<", ">", ":", "\"", "/", "\\", "|", "?", "*", "\0"];

        public static readonly string[] InvalidFileNames =
        [
            ".", "..", ".lock",
            "CON","PRN","AUX","NUL",
            "COM1","COM2","COM3","COM4","COM5","COM6","COM7","COM8","COM9",
            "LPT1","LPT2","LPT3","LPT4","LPT5","LPT6","LPT7","LPT8","LPT9"
        ];

        public enum FileConflictAction { ReplaceAll, Skip, Filter }

        public enum FileFilterAction { Continue, Skip, Cancel }

        public sealed class FileFilterInfo
        {
            public string SourcePath;
            public int Index;
            public int TotalCount;
        }

        public sealed class FileConflictInfo
        {
            public int TotalCount;
            public string SourcePath;
            public string DestinationPath;
            public long SourceSize;
            public long DestinationSize;
            public List<Entry> SourceFiles { get; set; } = [];
            public List<Entry> DestinationFiles { get; set; } = [];
            public Dictionary<string, bool> ReplaceMap { get; set; } = [];
        }

        internal static class FileOperationDialogs
        {
            public static Func<FileConflictInfo, FileConflictAction> ResolveConflict;
            public static Func<FileFilterInfo, FileFilterAction> FilterFile;
        }

        public static string InvalidCharsToolTip => $"{Program.Lang.Strings.GitHubStrings.Explorer_NotAllowedChars}: {string.Join(" ", InvalidFileNameChars.Select(c => c == "\0" ? "\\0" : $"'{c}'"))}";

        public static string InvalidNamesToolTip => $"{Program.Lang.Strings.GitHubStrings.Explorer_ReversedWords}: {string.Join(", ", InvalidFileNames)}";

        public static async Task<bool> FileExistsAsync(string path, CancellationTokenSource cts = null)
        {
            cts ??= new();
            try
            {
                cts?.Token.ThrowIfCancellationRequested();
                var info = await GetInfoFastAsync(path, cts);
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
            var info = await GetInfoRefreshAsync(path, recursive: false, cts: cts);
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
                var info = await FileSystem.GetInfoRefreshAsync(path, recursive: false, cts: cts);
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

        public static async Task<Entry> WriteFileAsync(string githubPath, string contentOrPath, bool isLocalFile = false, string commitMessage = null, CancellationTokenSource cts = null, Action<int> reportProgress = null)
        {
            cts ??= new();
            reportProgress?.Invoke(0);
            commitMessage ??= $"Updated `{githubPath}` by {_owner}";

            string normalizedPath = NormalizePath(githubPath);
            string workingDir = ParentDirectoryName(normalizedPath);

            // Read local file if needed
            string contentToSend = contentOrPath;
            if (isLocalFile)
            {
                byte[] fileBytes = File.ReadAllBytes(contentOrPath);
                contentToSend = Encoding.UTF8.GetString(fileBytes);
            }

            IRepositoryContentsClient client = Program.GitHub.Client.Repository.Content;

            try
            {
                cts.Token.ThrowIfCancellationRequested();

                // Fetch existing entry
                var existing = await FileSystem.GetInfoRefreshAsync(normalizedPath, recursive: false, cts: cts);

                if (existing == null)
                {
                    await client.CreateFile(
                        _owner,
                        GitHub.Repository.repositoryName,
                        normalizedPath,
                        new CreateFileRequest(commitMessage, contentToSend) { Branch = GitHub.Repository.branch });
                }
                else
                {
                    string sha = existing.Content?.Sha;

                    if (string.IsNullOrEmpty(sha))
                    {
                        var remote = await client.GetAllContentsByRef(_owner, GitHub.Repository.repositoryName, normalizedPath, GitHub.Repository.branch);
                        sha = remote[0].Sha;
                    }

                    await client.UpdateFile(
                        _owner,
                        GitHub.Repository.repositoryName,
                        normalizedPath,
                        new UpdateFileRequest(commitMessage, contentToSend, sha) { Branch = GitHub.Repository.branch });
                }

                // Update cache
                var updatedContent = await client.GetAllContentsByRef(_owner, GitHub.Repository.repositoryName, normalizedPath, GitHub.Repository.branch);

                if (updatedContent.Count > 0)
                {
                    Entry newEntry = await Entry.FromRepositoryContent(updatedContent[0]);
                    Cache.Add(normalizedPath, newEntry);

                    reportProgress?.Invoke(100);

                    if (workingDir.Equals(CurrentPath, StringComparison.OrdinalIgnoreCase)) await UpdateExplorerView(workingDir);

                    return newEntry;
                }
            }
            catch (OperationCanceledException)
            {
                reportProgress?.Invoke(0);
                throw;
            }

            if (workingDir.Equals(CurrentPath, StringComparison.OrdinalIgnoreCase)) await UpdateExplorerView(workingDir);

            return null;
        }

        public static async Task<Entry> CreateDirectoryAsync(string path, CancellationTokenSource cts = null)
        {
            cts ??= new();
            cts.Token.ThrowIfCancellationRequested();

            string normalizedPath = NormalizePath(path);
            string workingDir = ParentDirectoryName(normalizedPath);

            cts.Token.ThrowIfCancellationRequested();

            bool exists = await GetEntryCachedAsync(normalizedPath, forceRefresh: true, cts: cts) != null;
            if (exists) return null;
            Entry fileEntry = await WriteFileAsync($"{normalizedPath}/.gitkeep", string.Empty, false, $"Create directory {normalizedPath} by {_owner}", cts);

            Entry dirEntry = await Entry.FromRepositoryContent(await GetRepositoryContentAsync(GetParent(fileEntry.Path)));
            Cache.Add(dirEntry);
            if (workingDir.Equals(CurrentPath, StringComparison.OrdinalIgnoreCase)) await UpdateExplorerView(workingDir);

            Program.Log?.Write(LogEventLevel.Information, $"Directory {normalizedPath} created");
            return dirEntry;
        }

        public static async Task CopyFileAsync(string sourcePath, string destDirectory, CancellationTokenSource cts = null, Action<int> reportProgress = null)
        {
            await CopyFilesAsync([sourcePath], destDirectory, cts, reportProgress);
        }
        public static async Task CopyFilesAsync(IReadOnlyList<string> sourcePaths, string destDirectory, CancellationTokenSource cts = null, Action<int> reportProgress = null)
        {
            if (sourcePaths == null || sourcePaths.Count == 0) throw new ArgumentException(nameof(sourcePaths));

            cts ??= new();
            reportProgress?.Invoke(0);

            string destDir = NormalizePath(destDirectory).TrimEnd('/');
            bool replaceAll = false;
            bool skipAll = false;

            if (FileOperationDialogs.ResolveConflict == null)
            {
                FileOperationDialogs.ResolveConflict = info =>
                {
                    Forms.GitHub_FileConflict.ShowInfo(info, GitHub_FileConflict.Operation.Copy);
                    if (Forms.GitHub_FileConflict.ShowDialog() != DialogResult.OK) return FileConflictAction.Skip;
                    return Forms.GitHub_FileConflict.Action;
                };
            }

            var masterRef = await Program.GitHub.Client.Git.Reference.Get(_owner, GitHub.Repository.repositoryName, $"heads/{GitHub.Repository.branch}");
            var latestCommit = await Program.GitHub.Client.Git.Commit.Get(_owner, GitHub.Repository.repositoryName, masterRef.Object.Sha);
            var newTree = new NewTree { BaseTree = latestCommit.Tree.Sha };

            int processed = 0;

            List<Entry> sourceEntries = new();
            List<Entry> destEntries = new();
            var entriesToCopy = new List<(Entry srcEntry, string destPath, Entry destEntry)>();

            // Gather all entries and detect initial conflicts
            foreach (var srcPath in sourcePaths)
            {
                cts.Token.ThrowIfCancellationRequested();

                FileFilterAction filterAction = FileOperationDialogs.FilterFile?.Invoke(new FileFilterInfo
                {
                    SourcePath = srcPath,
                    Index = processed,
                    TotalCount = sourcePaths.Count
                }) ?? FileFilterAction.Continue;

                if (filterAction == FileFilterAction.Cancel) throw new OperationCanceledException();
                if (filterAction == FileFilterAction.Skip) continue;

                string src = NormalizePath(srcPath);
                Entry srcEntry = await GetInfoRefreshAsync(src, false, cts: cts);
                if (srcEntry == null || srcEntry.Type != EntryType.File) continue;

                string dest = destDir + "/" + FileName(src);
                Entry destEntry = await GetInfoRefreshAsync(dest, false, cts: cts);

                entriesToCopy.Add((srcEntry, dest, destEntry));
                sourceEntries.Add(srcEntry);
                if (destEntry != null) destEntries.Add(destEntry);
            }

            // Resolve conflicts and apply Windows-style numbering
            foreach (var (srcEntry, originalDestPath, destEntry) in entriesToCopy)
            {
                cts.Token.ThrowIfCancellationRequested();
                string destPath = originalDestPath;

                if (destEntry != null)
                {
                    if (skipAll) continue;

                    FileConflictAction action;
                    if (!replaceAll)
                    {
                        var conflictInfo = new FileConflictInfo
                        {
                            TotalCount = entriesToCopy.Count,
                            SourcePath = srcEntry.Path,
                            DestinationPath = destPath,
                            SourceSize = srcEntry.Content.Size,
                            DestinationSize = destEntry.Content.Size,
                            SourceFiles = [.. sourceEntries],
                            DestinationFiles = [.. destEntries]
                        };

                        action = FileOperationDialogs.ResolveConflict(conflictInfo);

                        if (action == FileConflictAction.Skip) { skipAll = true; continue; }
                        if (action == FileConflictAction.ReplaceAll) replaceAll = true;

                        if (action == FileConflictAction.Filter)
                        {
                            string name = FileName(srcEntry.Path);
                            if (conflictInfo.ReplaceMap != null && conflictInfo.ReplaceMap.TryGetValue(name, out bool replace) && !replace) continue;
                        }
                    }

                    // If source and destination content are identical, add numbering
                    if (destEntry.ContentSha == srcEntry.ContentSha)
                    {
                        string nameOnly = Path.GetFileNameWithoutExtension(destPath);
                        string ext = Path.GetExtension(destPath);
                        int count = 1;
                        string numberedDest = destPath;
                        while (await GetInfoRefreshAsync(numberedDest, false, cts: cts) != null)
                        {
                            numberedDest = $"{destDir}/{nameOnly} ({count}){ext}";
                            count++;
                        }
                        destPath = numberedDest;
                    }
                }

                // Add to Git tree
                newTree.Tree.Add(new NewTreeItem { Path = destPath, Mode = "100644", Type = TreeType.Blob, Sha = srcEntry.Content.Sha });

                Entry copiedEntry = await GetInfoRefreshAsync(destPath, false, cts: cts);
                Cache.Add(destPath, copiedEntry);
                if (destDir.Equals(CurrentPath, StringComparison.OrdinalIgnoreCase)) await UpdateExplorerView(destDir);

                processed++;
                reportProgress?.Invoke(processed * 100 / entriesToCopy.Count);
            }

            if (processed == 0) return;

            // Commit tree
            TreeResponse createdTree = await Program.GitHub.Client.Git.Tree.Create(_owner, GitHub.Repository.repositoryName, newTree);
            NewCommit commit = new($"Copy {processed} file(s) to `{destDir}` by {_owner}", createdTree.Sha, latestCommit.Sha);
            Commit createdCommit = await Program.GitHub.Client.Git.Commit.Create(_owner, GitHub.Repository.repositoryName, commit);
            await Program.GitHub.Client.Git.Reference.Update(_owner, GitHub.Repository.repositoryName, $"heads/{GitHub.Repository.branch}", new ReferenceUpdate(createdCommit.Sha));

            reportProgress?.Invoke(100);
        }

        public static async Task CopyDirectoryAsync(string sourceDir, string destDirectory, CancellationTokenSource cts = null, Action<int> reportProgress = null) => await CopyDirectoriesAsync([sourceDir], destDirectory, cts, reportProgress);
        public static async Task CopyDirectoriesAsync(IReadOnlyList<string> sourceDirs, string destDirectory, CancellationTokenSource cts = null, Action<int> reportProgress = null)
        {
            if (sourceDirs == null || sourceDirs.Count == 0) throw new ArgumentException(nameof(sourceDirs));

            cts ??= new();
            reportProgress?.Invoke(0);

            string destDirRoot = NormalizePath(destDirectory).TrimEnd('/');
            bool replaceAll = false;
            bool skipAll = false;

            if (FileOperationDialogs.ResolveConflict == null)
            {
                FileOperationDialogs.ResolveConflict = info =>
                {
                    Forms.GitHub_FileConflict.ShowInfo(info, GitHub_FileConflict.Operation.Copy);
                    if (Forms.GitHub_FileConflict.ShowDialog() != DialogResult.OK) return FileConflictAction.Skip;
                    return Forms.GitHub_FileConflict.Action;
                };
            }

            var masterRef = await Program.GitHub.Client.Git.Reference.Get(_owner, GitHub.Repository.repositoryName, $"heads/{GitHub.Repository.branch}");
            var latestCommit = await Program.GitHub.Client.Git.Commit.Get(_owner, GitHub.Repository.repositoryName, masterRef.Object.Sha);
            var newTree = new NewTree { BaseTree = latestCommit.Tree.Sha };

            int totalDirs = sourceDirs.Count;
            int processedDirs = 0;

            foreach (var sourceDir in sourceDirs)
            {
                cts.Token.ThrowIfCancellationRequested();

                string src = NormalizePath(sourceDir).TrimEnd('/');
                string dstFull = $"{destDirRoot}/{FileName(src)}";

                // Enumerate all entries recursively
                var entries = new List<Entry>();
                await foreach (var entry in EnumerateEntriesAsync(src, EnumerateType.Both, recursive: true, ct: cts.Token))
                    entries.Add(entry);

                if (entries.Count == 0)
                {
                    await CreateDirectoryAsync(dstFull, cts: cts);
                    Cache.Add(ParentDirectoryName(dstFull), new Entry { Path = dstFull, Type = EntryType.Dir });

                    processedDirs++;
                    reportProgress?.Invoke(processedDirs * 100 / totalDirs);
                    continue;
                }

                // Prepare for conflict resolution
                List<Entry> sourceEntries = new();
                List<Entry> destEntries = new();
                var entriesToCopy = new List<(Entry srcEntry, string destPath, Entry destEntry)>();

                foreach (var entry in entries)
                {
                    string relative = entry.Path.Substring(src.Length).TrimStart('/', '\\');
                    string targetPath = NormalizePath($"{dstFull}/{relative}");
                    Entry destEntry = entry.Type == EntryType.File ? await GetInfoRefreshAsync(targetPath, false, cts: cts) : null;

                    entriesToCopy.Add((entry, targetPath, destEntry));
                    if (entry.Type == EntryType.File) sourceEntries.Add(entry);
                    if (destEntry != null) destEntries.Add(destEntry);
                }

                foreach (var (srcEntry, originalDestPath, destEntry) in entriesToCopy)
                {
                    cts.Token.ThrowIfCancellationRequested();
                    string destPath = originalDestPath;

                    if (srcEntry.Type == EntryType.File)
                    {
                        if (destEntry != null)
                        {
                            if (skipAll) continue;

                            FileConflictAction action = replaceAll ? FileConflictAction.ReplaceAll :
                                FileOperationDialogs.ResolveConflict(new FileConflictInfo
                                {
                                    TotalCount = entries.Count,
                                    SourcePath = srcEntry.Path,
                                    DestinationPath = destPath,
                                    SourceSize = srcEntry.Content.Size,
                                    DestinationSize = destEntry.Content.Size,
                                    SourceFiles = [.. sourceEntries],
                                    DestinationFiles = [.. destEntries]
                                });

                            if (action == FileConflictAction.Skip) { skipAll = true; continue; }
                            if (action == FileConflictAction.ReplaceAll) replaceAll = true;

                            if (action == FileConflictAction.Filter)
                            {
                                string name = FileName(srcEntry.Path);
                                if (FileOperationDialogs.ResolveConflict != null && FileOperationDialogs.ResolveConflict.Invoke(new FileConflictInfo { SourceFiles = [.. sourceEntries], DestinationFiles = [.. destEntries] }) == FileConflictAction.Skip)
                                    continue;
                            }

                            // If source and destination content are identical, add numbering
                            if (destEntry.ContentSha == srcEntry.ContentSha)
                            {
                                string nameOnly = Path.GetFileNameWithoutExtension(destPath);
                                string ext = Path.GetExtension(destPath);
                                int count = 1;
                                string numberedDest = destPath;
                                while (await GetInfoRefreshAsync(numberedDest, false, cts: cts) != null)
                                {
                                    numberedDest = $"{ParentDirectoryName(destPath)}/{nameOnly} ({count}){ext}";
                                    count++;
                                }
                                destPath = numberedDest;
                            }
                        }

                        // Copy file to Git tree
                        newTree.Tree.Add(new NewTreeItem { Path = destPath, Mode = "100644", Type = TreeType.Blob, Sha = srcEntry.Content.Sha });
                    }
                }

                processedDirs++;
                reportProgress?.Invoke(processedDirs * 100 / totalDirs);
            }

            // Commit all changes at once
            var createdTree = await Program.GitHub.Client.Git.Tree.Create(_owner, GitHub.Repository.repositoryName, newTree);
            var commitMessage = $"Copy {sourceDirs.Count} directories to `{destDirRoot}` by {_owner}";
            var newCommit = new NewCommit(commitMessage, createdTree.Sha, latestCommit.Sha);
            var commit = await Program.GitHub.Client.Git.Commit.Create(_owner, GitHub.Repository.repositoryName, newCommit);
            await Program.GitHub.Client.Git.Reference.Update(_owner, GitHub.Repository.repositoryName, $"heads/{GitHub.Repository.branch}", new ReferenceUpdate(commit.Sha));

            // Update cache
            foreach (var sourceDir in sourceDirs)
            {
                string destDirFull = destDirRoot + "/" + FileName(sourceDir);

                // Add destination directory and all children
                Entry dirCopied = new() { Path = destDirFull, Type = EntryType.Dir, Content = await GetRepositoryContentAsync(destDirFull) };
                Cache.Add(dirCopied);

                await foreach (Entry entry in EnumerateEntriesAsync(destDirFull, recursive: true))
                {
                    Cache.Add(entry);
                    if (destDirRoot.Equals(CurrentPath, StringComparison.OrdinalIgnoreCase)) await UpdateExplorerView(destDirRoot);
                }
            }

            reportProgress?.Invoke(100);
        }

        public static async Task MoveFilesAsync(IReadOnlyList<string> sourcePaths, string destDirectory, CancellationTokenSource cts = null, Action<int> reportProgress = null)
        {
            if (sourcePaths == null || sourcePaths.Count == 0) throw new ArgumentException(nameof(sourcePaths));

            if (cts == null) cts = new();
            reportProgress?.Invoke(0);

            string destDir = NormalizePath(destDirectory).TrimEnd('/');
            bool replaceAll = false;
            bool skipAll = false;

            Reference masterRef = await Program.GitHub.Client.Git.Reference.Get(_owner, GitHub.Repository.repositoryName, $"heads/{GitHub.Repository.branch}");
            Commit latestCommit = await Program.GitHub.Client.Git.Commit.Get(_owner, GitHub.Repository.repositoryName, masterRef.Object.Sha);
            NewTree newTree = new() { BaseTree = latestCommit.Tree.Sha };

            if (FileOperationDialogs.ResolveConflict == null)
            {
                FileOperationDialogs.ResolveConflict = info =>
                {
                    Forms.GitHub_FileConflict.ShowInfo(info, GitHub_FileConflict.Operation.Move);
                    if (Forms.GitHub_FileConflict.ShowDialog() != DialogResult.OK) return FileConflictAction.Skip;
                    return Forms.GitHub_FileConflict.Action;
                };
            }

            int processed = 0;
            List<Entry> sourceEntries = new();
            List<Entry> destEntries = new();

            // Gather all entries
            List<(Entry srcEntry, string destPath, Entry destEntry)> entriesToMove = new();
            for (int i = 0; i < sourcePaths.Count; i++)
            {
                cts.Token.ThrowIfCancellationRequested();

                FileFilterAction filterAction = FileOperationDialogs.FilterFile?.Invoke(new FileFilterInfo
                {
                    SourcePath = sourcePaths[i],
                    Index = i,
                    TotalCount = sourcePaths.Count
                }) ?? FileFilterAction.Continue;

                if (filterAction == FileFilterAction.Cancel) throw new OperationCanceledException();
                if (filterAction == FileFilterAction.Skip) continue;

                string src = NormalizePath(sourcePaths[i]);
                Entry srcEntry = await GetInfoRefreshAsync(src, false, cts: cts);
                if (srcEntry == null || srcEntry.Type != EntryType.File) continue;

                string dest = destDir + "/" + FileName(src);
                Entry destEntry = await GetInfoRefreshAsync(dest, false, cts: cts);

                entriesToMove.Add((srcEntry, dest, destEntry));
                sourceEntries.Add(srcEntry);

                if (destEntry != null)
                {
                    destEntries.Add(destEntry);
                }
            }

            // Check for conflicts if any destination already exists
            foreach (var (srcEntry, destPath, destEntry) in entriesToMove)
            {
                if (cts.Token.IsCancellationRequested) throw new OperationCanceledException();

                if (destEntry != null)
                {
                    if (skipAll) continue;

                    if (!replaceAll)
                    {
                        FileConflictInfo conflictInfo = new()
                        {
                            TotalCount = sourcePaths.Count,
                            SourcePath = srcEntry.Path,
                            DestinationPath = destPath,
                            SourceSize = srcEntry.Content.Size,
                            DestinationSize = destEntry.Content.Size,
                            SourceFiles = [.. sourceEntries],
                            DestinationFiles = [.. destEntries]
                        };

                        FileConflictAction action = FileOperationDialogs.ResolveConflict(conflictInfo);

                        if (action == FileConflictAction.Skip) { skipAll = true; continue; }
                        if (action == FileConflictAction.ReplaceAll) replaceAll = true;

                        if (action == FileConflictAction.Filter)
                        {
                            string name = FileName(srcEntry.Path);
                            if (conflictInfo.ReplaceMap != null && conflictInfo.ReplaceMap.TryGetValue(name, out bool replace) && !replace)
                                continue;
                        }
                    }
                }

                // Add to new tree
                newTree.Tree.Add(new NewTreeItem { Path = destPath, Mode = "100644", Type = TreeType.Blob, Sha = srcEntry.Content.Sha });
                newTree.Tree.Add(new NewTreeItem { Path = srcEntry.Path, Mode = "100644", Type = TreeType.Blob, Sha = string.Empty });

                processed++;
                reportProgress?.Invoke(processed * 100 / entriesToMove.Count);
            }

            if (processed > 0)
            {
                TreeResponse createdTree = await Program.GitHub.Client.Git.Tree.Create(_owner, GitHub.Repository.repositoryName, newTree);
                NewCommit commit = new($"Move {processed} file(s) to `{destDir}` by {_owner}", createdTree.Sha, latestCommit.Sha);
                Commit createdCommit = await Program.GitHub.Client.Git.Commit.Create(_owner, GitHub.Repository.repositoryName, commit);
                await Program.GitHub.Client.Git.Reference.Update(_owner, GitHub.Repository.repositoryName, $"heads/{GitHub.Repository.branch}", new ReferenceUpdate(createdCommit.Sha));

                // Update cache for all moved files
                foreach (var sourceDir in sourcePaths)
                {
                    string src = NormalizePath(sourceDir).TrimEnd('/');
                    string dest = destDirectory + "/" + FileName(src);

                    // Remove source file from cache
                    Cache.Remove(src);

                    Entry updatedEntry = await GetInfoRefreshAsync(dest, false);
                    Cache.Add(dest, updatedEntry);
                    if (destDir.Equals(CurrentPath, StringComparison.OrdinalIgnoreCase)) await UpdateExplorerView(destDir);
                }
            }

            reportProgress?.Invoke(100);
        }
        public static async Task<Entry> MoveFileAsync(string sourcePath, string destPath, CancellationTokenSource cts = null, Action<int> reportProgress = null)
        {
            if (string.IsNullOrWhiteSpace(sourcePath)) throw new ArgumentException(nameof(sourcePath));
            if (string.IsNullOrWhiteSpace(destPath)) throw new ArgumentException(nameof(destPath));

            if (cts == null) cts = new();
            reportProgress?.Invoke(0);

            string src = NormalizePath(sourcePath);
            string dest = NormalizePath(destPath);
            string workingDir = ParentDirectoryName(dest);

            bool replaceAll = false;
            bool skipAll = false;

            Reference masterRef = await Program.GitHub.Client.Git.Reference.Get(_owner, GitHub.Repository.repositoryName, $"heads/{GitHub.Repository.branch}");
            Commit latestCommit = await Program.GitHub.Client.Git.Commit.Get(_owner, GitHub.Repository.repositoryName, masterRef.Object.Sha);
            NewTree newTree = new() { BaseTree = latestCommit.Tree.Sha };

            if (FileOperationDialogs.ResolveConflict == null)
            {
                FileOperationDialogs.ResolveConflict = info =>
                {
                    Forms.GitHub_FileConflict.ShowInfo(info, GitHub_FileConflict.Operation.Move);
                    if (Forms.GitHub_FileConflict.ShowDialog() != DialogResult.OK) return FileConflictAction.Skip;
                    return Forms.GitHub_FileConflict.Action;
                };
            }

            cts.Token.ThrowIfCancellationRequested();

            Entry srcEntry = await GetInfoRefreshAsync(src, false, cts: cts);
            if (srcEntry == null || srcEntry.Type != EntryType.File) return srcEntry;

            Entry destEntry = await GetInfoRefreshAsync(dest, false, cts: cts);

            if (destEntry != null)
            {
                if (!skipAll && !replaceAll)
                {
                    FileConflictInfo conflictInfo = new()
                    {
                        TotalCount = 1,
                        SourcePath = srcEntry.Path,
                        DestinationPath = dest,
                        SourceSize = srcEntry.Content.Size,
                        DestinationSize = destEntry.Content.Size,
                        SourceFiles = [srcEntry],
                        DestinationFiles = [destEntry]
                    };

                    FileConflictAction action = FileOperationDialogs.ResolveConflict(conflictInfo);

                    if (action == FileConflictAction.Skip) return destEntry;
                    if (action == FileConflictAction.ReplaceAll) replaceAll = true;

                    if (action == FileConflictAction.Filter)
                    {
                        string name = FileName(srcEntry.Path);
                        if (conflictInfo.ReplaceMap != null && conflictInfo.ReplaceMap.TryGetValue(name, out bool replace) && !replace)
                            return destEntry;
                    }
                }
            }

            newTree.Tree.Add(new NewTreeItem { Path = dest, Mode = "100644", Type = TreeType.Blob, Sha = srcEntry.Content.Sha });
            newTree.Tree.Add(new NewTreeItem { Path = srcEntry.Path, Mode = "100644", Type = TreeType.Blob, Sha = string.Empty });

            Cache.Remove(src);

            reportProgress?.Invoke(50);

            TreeResponse createdTree = await Program.GitHub.Client.Git.Tree.Create(_owner, GitHub.Repository.repositoryName, newTree);
            NewCommit commit = new($"Move file `{FileName(src)}` to `{ParentDirectoryName(dest)}` by {_owner}", createdTree.Sha, latestCommit.Sha);
            Commit createdCommit = await Program.GitHub.Client.Git.Commit.Create(_owner, GitHub.Repository.repositoryName, commit);
            await Program.GitHub.Client.Git.Reference.Update(_owner, GitHub.Repository.repositoryName, $"heads/{GitHub.Repository.branch}", new ReferenceUpdate(createdCommit.Sha));

            reportProgress?.Invoke(100);

            // This adds the new entry in cache too
            Entry updatedEntry = await GetInfoRefreshAsync(dest, false, cts: cts);
            Cache.Add(updatedEntry);
            if (workingDir.Equals(CurrentPath, StringComparison.OrdinalIgnoreCase)) await UpdateExplorerView(workingDir);

            return updatedEntry;
        }

        public static async Task MoveDirectoriesAsync(IReadOnlyList<string> sourceDirs, string destDirectory, CancellationTokenSource cts = null, Action<int> reportProgress = null)
        {
            if (sourceDirs == null || sourceDirs.Count == 0) throw new ArgumentException("No source directories provided.");

            cts ??= new();
            reportProgress?.Invoke(0);

            string destDirRoot = NormalizePath(destDirectory).TrimEnd('/');

            if (Program.GitHub.Client == null) throw new InvalidOperationException("GitHub client is null.");
            if (string.IsNullOrEmpty(_owner)) throw new InvalidOperationException("_owner is null or empty.");
            if (string.IsNullOrEmpty(GitHub.Repository.repositoryName)) throw new InvalidOperationException("Repository name is null or empty.");
            if (string.IsNullOrEmpty(GitHub.Repository.branch)) throw new InvalidOperationException("Branch is null or empty.");

            if (FileOperationDialogs.ResolveConflict == null)
            {
                FileOperationDialogs.ResolveConflict = info =>
                {
                    Forms.GitHub_FileConflict.ShowInfo(info, GitHub_FileConflict.Operation.Move);
                    if (Forms.GitHub_FileConflict.ShowDialog() != DialogResult.OK) return FileConflictAction.Skip;
                    return Forms.GitHub_FileConflict.Action;
                };
            }

            // Get latest commit
            var masterRef = await Program.GitHub.Client.Git.Reference.Get(_owner, GitHub.Repository.repositoryName, $"heads/{GitHub.Repository.branch}");
            var latestCommit = await Program.GitHub.Client.Git.Commit.Get(_owner, GitHub.Repository.repositoryName, masterRef.Object.Sha);
            var newTree = new NewTree { BaseTree = latestCommit.Tree.Sha };

            bool replaceAll = false;
            bool skipAll = false;

            // 1. Collect all files recursively from source directories
            var allEntries = new List<(string srcPath, string destPath, Entry destEntry)>();

            foreach (var sourceDir in sourceDirs)
            {
                cts.Token.ThrowIfCancellationRequested();

                string srcDir = NormalizePath(sourceDir).TrimEnd('/');
                string destRootFull = destDirRoot + "/" + FileName(srcDir);

                await foreach (var file in EnumerateEntriesAsync(srcDir, EnumerateType.Files, recursive: true, ct: cts.Token))
                {
                    if (file == null) continue;

                    string relative = file.Path.Substring(srcDir.Length).TrimStart('/', '\\');
                    string destPath = NormalizePath($"{destRootFull}/{relative}");
                    Entry destEntry = await GetInfoRefreshAsync(destPath, false, cts: cts);

                    allEntries.Add((file.Path, destPath, destEntry));
                }
            }

            int totalItems = allEntries.Count;
            int processedItems = 0;

            // 2. Handle conflicts and prepare Git tree
            foreach (var (srcPath, destPath, destEntry) in allEntries)
            {
                cts.Token.ThrowIfCancellationRequested();

                Entry freshSrcEntry = await GetInfoRefreshAsync(srcPath, false, cts: cts);
                if (freshSrcEntry == null || freshSrcEntry.Type != EntryType.File)
                {
                    processedItems++;
                    reportProgress?.Invoke(processedItems * 100 / totalItems);
                    continue;
                }

                // Conflict handling
                if (destEntry != null)
                {
                    if (skipAll) { processedItems++; reportProgress?.Invoke(processedItems * 100 / totalItems); continue; }

                    if (!replaceAll)
                    {
                        var conflictInfo = new FileConflictInfo
                        {
                            TotalCount = totalItems,
                            SourcePath = freshSrcEntry.Path,
                            DestinationPath = destPath,
                            SourceSize = freshSrcEntry.Content.Size,
                            DestinationSize = destEntry.Content.Size,
                            SourceFiles = allEntries.Select(e => Cache.TryGetEntry(e.srcPath, out var entry) ? entry : null).Where(e => e != null).ToList(),
                            DestinationFiles = allEntries.Where(e => e.destEntry != null).Select(e => e.destEntry).ToList()
                        };

                        FileConflictAction action = FileOperationDialogs.ResolveConflict(conflictInfo);

                        if (action == FileConflictAction.Skip) { skipAll = true; processedItems++; reportProgress?.Invoke(processedItems * 100 / totalItems); continue; }
                        if (action == FileConflictAction.ReplaceAll) replaceAll = true;

                        if (action == FileConflictAction.Filter)
                        {
                            string name = FileName(freshSrcEntry.Path);
                            if (conflictInfo.ReplaceMap != null && conflictInfo.ReplaceMap.TryGetValue(name, out bool replace) && !replace)
                            {
                                processedItems++;
                                reportProgress?.Invoke(processedItems * 100 / totalItems);
                                continue;
                            }
                        }
                    }
                }

                // Add move and delete operations to Git tree
                newTree.Tree.Add(new NewTreeItem
                {
                    Path = destPath,
                    Mode = "100644",
                    Type = TreeType.Blob,
                    Sha = freshSrcEntry.Content.Sha
                });
                newTree.Tree.Add(new NewTreeItem
                {
                    Path = freshSrcEntry.Path,
                    Mode = "100644",
                    Type = TreeType.Blob,
                    Sha = string.Empty
                });

                processedItems++;
                reportProgress?.Invoke(processedItems * 100 / totalItems);
            }

            // 3. Commit all changes at once and update cache
            if (allEntries.Count > 0)
            {
                // Commit
                var createdTree = await Program.GitHub.Client.Git.Tree.Create(_owner, GitHub.Repository.repositoryName, newTree);
                var commit = new NewCommit($"Move {allEntries.Count} item(s) to `{destDirRoot}` by {_owner}", createdTree.Sha, latestCommit.Sha);
                var createdCommit = await Program.GitHub.Client.Git.Commit.Create(_owner, GitHub.Repository.repositoryName, commit);
                await Program.GitHub.Client.Git.Reference.Update(_owner, GitHub.Repository.repositoryName, $"heads/{GitHub.Repository.branch}", new ReferenceUpdate(createdCommit.Sha));

                // Update cache
                foreach (var sourceDir in sourceDirs)
                {
                    string srcDir = NormalizePath(sourceDir).TrimEnd('/');
                    string destDirFull = destDirRoot + "/" + FileName(srcDir);

                    // Remove source directory from cache
                    Cache.Remove(srcDir);

                    // Add destination directory and all children
                    Entry rootMoved = new() { Path = destDirFull, Type = EntryType.Dir, Content = await GetRepositoryContentAsync(destDirFull) };
                    Cache.Add(rootMoved);

                    await foreach (Entry entry in EnumerateEntriesAsync(destDirFull, recursive: true))
                    {
                        Cache.Add(entry);
                        if (destDirRoot.Equals(CurrentPath, StringComparison.OrdinalIgnoreCase)) await UpdateExplorerView(destDirRoot);
                    }
                }
            }

            reportProgress?.Invoke(100);
        }
        public static async Task<Entry> MoveDirectoryAsync(string sourceDir, string destDirectory, CancellationTokenSource cts = null, Action<int> reportProgress = null)
        {
            if (string.IsNullOrWhiteSpace(sourceDir)) throw new ArgumentException(nameof(sourceDir));
            if (string.IsNullOrWhiteSpace(destDirectory)) throw new ArgumentException(nameof(destDirectory));

            cts ??= new();
            reportProgress?.Invoke(0);

            string srcDir = NormalizePath(sourceDir).TrimEnd('/');
            string dstRootFull = NormalizePath(destDirectory).TrimEnd('/');

            var masterRef = await Program.GitHub.Client.Git.Reference.Get(_owner, GitHub.Repository.repositoryName, $"heads/{GitHub.Repository.branch}");
            var latestCommit = await Program.GitHub.Client.Git.Commit.Get(_owner, GitHub.Repository.repositoryName, masterRef.Object.Sha);
            var newTree = new NewTree { BaseTree = latestCommit.Tree.Sha };

            bool replaceAll = false;
            bool skipAll = false;

            if (FileOperationDialogs.ResolveConflict == null)
            {
                FileOperationDialogs.ResolveConflict = info =>
                {
                    Forms.GitHub_FileConflict.ShowInfo(info, GitHub_FileConflict.Operation.Move);
                    if (Forms.GitHub_FileConflict.ShowDialog() != DialogResult.OK) return FileConflictAction.Skip;
                    return Forms.GitHub_FileConflict.Action;
                };
            }

            // Enumerate all files in directory recursively
            var allEntries = new List<(Entry srcEntry, string destPath, Entry destEntry)>();
            await foreach (var file in EnumerateEntriesAsync(srcDir, EnumerateType.Files, recursive: true, ct: cts.Token))
            {
                if (file == null) continue;

                string relative = file.Path.Substring(srcDir.Length).TrimStart('/', '\\');
                string destPath = NormalizePath($"{dstRootFull}/{relative}");
                Entry destEntry = await GetInfoRefreshAsync(destPath, false, cts: cts);

                allEntries.Add((file, destPath, destEntry));
            }

            int totalItems = allEntries.Count;
            int processedItems = 0;

            foreach (var (srcEntry, destPath, destEntry) in allEntries)
            {
                cts.Token.ThrowIfCancellationRequested();

                if (destEntry != null)
                {
                    if (skipAll) { processedItems++; continue; }

                    if (!replaceAll)
                    {
                        var conflictInfo = new FileConflictInfo
                        {
                            TotalCount = totalItems,
                            SourcePath = srcEntry.Path,
                            DestinationPath = destPath,
                            SourceSize = srcEntry.Content.Size,
                            DestinationSize = destEntry.Content.Size,
                            SourceFiles = [srcEntry],
                            DestinationFiles = [destEntry]
                        };

                        FileConflictAction action = FileOperationDialogs.ResolveConflict(conflictInfo);

                        if (action == FileConflictAction.Skip) { processedItems++; continue; }
                        if (action == FileConflictAction.ReplaceAll) replaceAll = true;

                        if (action == FileConflictAction.Filter)
                        {
                            string name = FileName(srcEntry.Path);
                            if (conflictInfo.ReplaceMap != null && conflictInfo.ReplaceMap.TryGetValue(name, out bool replace) && !replace)
                            {
                                processedItems++;
                                continue;
                            }
                        }
                    }
                }

                // Add to Git tree
                newTree.Tree.Add(new NewTreeItem { Path = destPath, Mode = "100644", Type = TreeType.Blob, Sha = srcEntry.Content.Sha });
                newTree.Tree.Add(new NewTreeItem { Path = srcEntry.Path, Mode = "100644", Type = TreeType.Blob, Sha = string.Empty });

                // Update cache
                Cache.Remove(srcEntry.Path);
                Cache.Add(destPath, new Entry { Path = destPath, Type = EntryType.File, Content = srcEntry.Content, FetchedAt = DateTime.UtcNow });

                processedItems++;
                reportProgress?.Invoke(processedItems * 100 / totalItems);
            }

            // Commit changes
            if (totalItems > 0)
            {
                TreeResponse createdTree = await Program.GitHub.Client.Git.Tree.Create(_owner, GitHub.Repository.repositoryName, newTree);
                NewCommit commit = new($"Move directory `{FileName(srcDir)}` to `{destDirectory}` by {_owner}", createdTree.Sha, latestCommit.Sha);
                Commit createdCommit = await Program.GitHub.Client.Git.Commit.Create(_owner, GitHub.Repository.repositoryName, commit);
                await Program.GitHub.Client.Git.Reference.Update(_owner, GitHub.Repository.repositoryName, $"heads/{GitHub.Repository.branch}", new ReferenceUpdate(createdCommit.Sha));
            }

            Cache.Remove(srcDir);
            Entry updatedEntry = await Entry.FromRepositoryContent(await GetRepositoryContentAsync(dstRootFull));
            Cache.Add(updatedEntry);
            if (dstRootFull.Equals(CurrentPath, StringComparison.OrdinalIgnoreCase)) await UpdateExplorerView(dstRootFull);

            reportProgress?.Invoke(100);

            // Return the new directory entry
            return updatedEntry;
        }

        public static async Task DeleteFileAsync(string path, CancellationTokenSource cts = null, Action<int> reportProgress = null) => await DeleteFilesAsync([path], cts, reportProgress);
        public static async Task DeleteFilesAsync(IReadOnlyList<string> paths, CancellationTokenSource cts = null, Action<int> reportProgress = null)
        {
            if (paths == null || paths.Count == 0) throw new ArgumentException("No paths provided.");

            cts ??= new();
            reportProgress?.Invoke(0);

            int total = paths.Count;
            int processed = 0;

            foreach (var path in paths)
            {
                cts.Token.ThrowIfCancellationRequested();

                string normalizedPath = NormalizePath(path);
                string workingDir = ParentDirectoryName(normalizedPath);

                try
                {
                    var file = await GetInfoRefreshAsync(normalizedPath, recursive: false, cts: cts);
                    if (file?.Type != EntryType.File)
                    {
                        processed++;
                        reportProgress?.Invoke(processed * 100 / total);
                        continue;
                    }

                    DeleteFileRequest deleteRequest = new($"{_owner} deleted `{file.Path}`", file.Content.Sha)
                    {
                        Branch = GitHub.Repository.branch
                    };

                    await Program.GitHub.Client.Repository.Content.DeleteFile(_owner, GitHub.Repository.repositoryName, file.Path, deleteRequest);

                    Cache.Remove(normalizedPath);
                    if (workingDir.Equals(CurrentPath, StringComparison.OrdinalIgnoreCase)) await UpdateExplorerView(workingDir);

                    processed++;
                    reportProgress?.Invoke(processed * 100 / total);
                }
                catch (OperationCanceledException)
                {
                    reportProgress?.Invoke(0);
                    throw;
                }
                catch (Exception ex)
                {
                    Forms.BugReport.ThrowError(ex);
                    Program.Log?.Write(LogEventLevel.Error, $"DeleteFileAsync failed for `{normalizedPath}`", ex);
                    processed++;
                    reportProgress?.Invoke(processed * 100 / total);
                }

                if (workingDir.Equals(CurrentPath, StringComparison.OrdinalIgnoreCase)) await UpdateExplorerView(workingDir);
            }

            reportProgress?.Invoke(100);
        }

        public static async Task DeleteDirectoryAsync(string dir, CancellationTokenSource cts = null, Action<int> reportProgress = null) => await DeleteDirectoriesAsync([dir], cts, reportProgress);
        public static async Task DeleteDirectoriesAsync(IReadOnlyList<string> dirs, CancellationTokenSource cts = null, Action<int> reportProgress = null)
        {
            if (dirs == null || dirs.Count == 0)
                throw new ArgumentException("No directories provided.");

            cts ??= new();
            reportProgress?.Invoke(0);

            int total = dirs.Count;
            int processed = 0;

            // Normalize all directory paths
            var normalizedDirs = dirs.Select(d => NormalizePath(d)).ToList();
            var allFilesToDelete = new List<NewTreeItem>();

            try
            {
                if (Program.GitHub.Client == null) throw new InvalidOperationException("GitHub client is null.");
                if (string.IsNullOrEmpty(_owner)) throw new InvalidOperationException("_owner is null or empty.");
                if (string.IsNullOrEmpty(GitHub.Repository.repositoryName)) throw new InvalidOperationException("Repository name is null or empty.");
                if (string.IsNullOrEmpty(GitHub.Repository.branch)) throw new InvalidOperationException("Branch is null or empty.");

                // Get latest commit
                var branchRef = await Program.GitHub.Client.Git.Reference.Get(_owner, GitHub.Repository.repositoryName, $"heads/{GitHub.Repository.branch}");
                var latestCommit = await Program.GitHub.Client.Git.Commit.Get(_owner, GitHub.Repository.repositoryName, branchRef.Object.Sha);

                // Get full tree recursively
                var tree = await Program.GitHub.Client.Git.Tree.GetRecursive(_owner, GitHub.Repository.repositoryName, latestCommit.Tree.Sha);

                // Collect files to delete from all directories
                foreach (var dir in normalizedDirs)
                {
                    cts.Token.ThrowIfCancellationRequested();
                    string workingDir = ParentDirectoryName(dir);

                    var files = tree.Tree
                        .Where(t => t.Type == TreeType.Blob && NormalizePath(t.Path).StartsWith(dir, StringComparison.OrdinalIgnoreCase))
                        .Select(f => new NewTreeItem
                        {
                            Path = f.Path,
                            Mode = f.Mode,
                            Type = TreeType.Blob,
                            Sha = string.Empty
                        })
                        .ToList();

                    allFilesToDelete.AddRange(files);

                    // Remove cache regardless of whether directory had files
                    Cache.Remove(dir);
                    await UpdateExplorerView(workingDir);

                    processed++;
                    reportProgress?.Invoke(processed * 100 / total);
                }

                // Create single commit if there are files to delete
                if (allFilesToDelete.Any())
                {
                    var newTree = new NewTree { BaseTree = latestCommit.Tree.Sha };
                    foreach (var item in allFilesToDelete)
                        newTree.Tree.Add(item);

                    var createdTree = await Program.GitHub.Client.Git.Tree.Create(_owner, GitHub.Repository.repositoryName, newTree);
                    var commitMessage = $"Delete directories recursively by {_owner}";
                    var newCommit = new NewCommit(commitMessage, createdTree.Sha, latestCommit.Sha);
                    var commit = await Program.GitHub.Client.Git.Commit.Create(_owner, GitHub.Repository.repositoryName, newCommit);

                    await Program.GitHub.Client.Git.Reference.Update(_owner, GitHub.Repository.repositoryName, $"heads/{GitHub.Repository.branch}", new ReferenceUpdate(commit.Sha));
                }
            }
            catch (OperationCanceledException)
            {
                await UpdateExplorerView(CurrentPath);
                reportProgress?.Invoke(0);
                throw;
            }
            catch (Exception ex)
            {
                await UpdateExplorerView(CurrentPath);
                Forms.BugReport.ThrowError(ex);
                Program.Log?.Write(LogEventLevel.Error, $"DeleteDirectoriesAsync failed", ex);
            }
            finally
            {
                await UpdateExplorerView(CurrentPath);
                reportProgress?.Invoke(100);
            }
        }
        public static async IAsyncEnumerable<Entry> EnumerateEntriesAsync(string path, EnumerateType type = EnumerateType.Both, string searchPattern = "*", bool recursive = false, [EnumeratorCancellation] CancellationToken ct = default)
        {
            if (Program.GitHub.Client == null) throw new InvalidOperationException("GitHub client is null.");
            if (string.IsNullOrEmpty(_owner) || string.IsNullOrEmpty(GitHub.Repository.repositoryName) || string.IsNullOrEmpty(GitHub.Repository.branch)) throw new InvalidOperationException("Owner, repository, or branch not set.");

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
                     (regex == null || regex.IsMatch(FileName(entry.Path)))) ||
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