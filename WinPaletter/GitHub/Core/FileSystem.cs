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

        public static string InvalidCharsToolTip => $"{Program.Localization.Strings.GitHubStrings.Explorer_NotAllowedChars}: {string.Join(" ", InvalidFileNameChars.Select(c => c == "\0" ? "\\0" : $"'{c}'"))}";

        public static string InvalidNamesToolTip => $"{Program.Localization.Strings.GitHubStrings.Explorer_ReversedWords}: {string.Join(", ", InvalidFileNames)}";

        public static async Task<bool> FileExistsAsync(string path, CancellationTokenSource cts = null)
        {
            cts ??= new();
            try
            {
                if (cts is not null && cts.Token.IsCancellationRequested) return false;
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
                if (cts is not null && cts.Token.IsCancellationRequested) return false;
                var entries = await GetEntriesCachedAsync(path, forceRefresh: true);
                return entries != null && entries.Count > 0;
            }
            catch { return false; }
        }

        public static async Task<byte[]> ReadFileBytesAsync(string path, CancellationTokenSource cts = null)
        {
            cts ??= new();
            if (cts.Token.IsCancellationRequested) return null;

            // Get fresh file info without recursion
            Entry info = await FileSystem.GetInfoRefreshAsync(path, recursive: false, cts: cts);
            if (info == null || info.Type != EntryType.File)
                throw new FileNotFoundException($"File `{path}` does not exist on GitHub.");

            IReadOnlyList<RepositoryContent> content = await Helpers.Do(() =>
                Program.GitHub.Client.Repository.Content.GetAllContentsByRef(
                    Repository.Owner,
                    GitHub.Repository.Name,
                    path,
                    GitHub.Repository.Branch.Name));

            if (content == null || !content.Any()) return null; // rate-limited or network failure

            return Convert.FromBase64String(content[0].Content);
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
            commitMessage ??= $"Updated `{githubPath}` by {Repository.Owner}";

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

            if (cts.Token.IsCancellationRequested)
            {
                reportProgress?.Invoke(0);
                return null;
            }

            // Fetch existing entry
            Entry existing = await FileSystem.GetInfoRefreshAsync(normalizedPath, recursive: false, cts: cts);

            if (existing == null)
            {
                await Helpers.Do(() =>
                    client.CreateFile(
                        Repository.Owner,
                        GitHub.Repository.Name,
                        normalizedPath,
                        new CreateFileRequest(commitMessage, contentToSend) { Branch = GitHub.Repository.Branch.Name }));
            }
            else
            {
                string sha = existing.Content?.Sha;

                if (string.IsNullOrEmpty(sha))
                {
                    IReadOnlyList<RepositoryContent> remote = await Helpers.Do(() =>
                        client.GetAllContentsByRef(Repository.Owner, GitHub.Repository.Name, normalizedPath, GitHub.Repository.Branch.Name));

                    if (remote == null || !remote.Any()) return null; // rate-limited or network failure
                    sha = remote[0].Sha;
                }

                await Helpers.Do(() =>
                    client.UpdateFile(
                        Repository.Owner,
                        GitHub.Repository.Name,
                        normalizedPath,
                        new UpdateFileRequest(commitMessage, contentToSend, sha) { Branch = GitHub.Repository.Branch.Name }));
            }

            // Update cache
            IReadOnlyList<RepositoryContent> updatedContent = await Helpers.Do(() =>
                client.GetAllContentsByRef(Repository.Owner, GitHub.Repository.Name, normalizedPath, GitHub.Repository.Branch.Name));

            if (updatedContent != null && updatedContent.Count > 0)
            {
                Entry newEntry = await Entry.FromRepositoryContent(updatedContent[0]);
                Cache.Add(normalizedPath, newEntry);

                reportProgress?.Invoke(100);

                if (workingDir.Equals(CurrentPath, StringComparison.OrdinalIgnoreCase)) await UpdateExplorerView(workingDir);

                return newEntry;
            }

            if (workingDir.Equals(CurrentPath, StringComparison.OrdinalIgnoreCase)) await UpdateExplorerView(workingDir);

            return null;
        }

        public static async Task<Entry> CreateDirectoryAsync(string path, CancellationTokenSource cts = null)
        {
            cts ??= new();

            string normalizedPath = NormalizePath(path);
            string workingDir = ParentDirectoryName(normalizedPath);

            if (cts is not null && cts.Token.IsCancellationRequested) return null;

            bool exists = await GetEntryCachedAsync(normalizedPath, forceRefresh: true, cts: cts) != null;
            if (cts is not null && cts.Token.IsCancellationRequested) return null;

            if (exists) return null;
            Entry fileEntry = await WriteFileAsync($"{normalizedPath}/.gitkeep", string.Empty, false, $"Create directory {normalizedPath} by {Repository.Owner}", cts);
            if (cts is not null && cts.Token.IsCancellationRequested) return null;

            Entry dirEntry = await Entry.FromRepositoryContent(await GetRepositoryContentAsync(GetParent(fileEntry.Path)));
            if (cts is not null && cts.Token.IsCancellationRequested) return null;

            Cache.Add(dirEntry);
            if (workingDir.Equals(CurrentPath, StringComparison.OrdinalIgnoreCase)) await UpdateExplorerView(workingDir);
            if (cts is not null && cts.Token.IsCancellationRequested) return null;

            Program.Log?.Write(LogEventLevel.Information, $"Directory {normalizedPath} created");
            return dirEntry;
        }

        /// <summary>
        /// Uploads or updates a single file in the GitHub repository using the Contents API.
        /// Automatically creates parent directories if they do not exist.
        /// </summary>
        /// <param name="githubPath">The target path inside the GitHub repository.</param>
        /// <param name="localFilePath">The absolute local file path.</param>
        /// <param name="commitMessage">Optional commit message.</param>
        /// <param name="cts">Optional CancellationTokenSource.</param>
        public static async Task<Entry> UploadFileAsync(string githubPathDirectory, string localFilePath, string commitMessage = null, CancellationTokenSource cts = null)
        {
            if (!System.IO.File.Exists(localFilePath)) return null;

            cts ??= new();

            githubPathDirectory = NormalizePath(githubPathDirectory);
            commitMessage ??= $"Uploaded `{Path.GetFileName(localFilePath)}` into `{githubPathDirectory}` by {Repository.Owner}";

            if (cts.IsCancellationRequested) return null;

            var client = Program.GitHub.Client;

            // 1. Read file
            byte[] fileBytes;
            using (FileStream fs = new(localFilePath, System.IO.FileMode.Open, FileAccess.Read, FileShare.Read, 81920, useAsync: true))
            {
                using var ms = new MemoryStream();
                await fs.CopyToAsync(ms, 81920, cts.Token);
                fileBytes = ms.ToArray();
            }

            if (cts.IsCancellationRequested) return null;

            string githubPath = string.Join("/", githubPathDirectory, Path.GetFileName(localFilePath));

            // 2. Detect if file is binary
            bool isBinary = fileBytes.Take(8000).Any(b => b == 0);

            if (!isBinary)
            {
                // 3. TEXT FILE → Contents API
                string content = Encoding.UTF8.GetString(fileBytes);

                RepositoryContent existingContent = await Helpers.Do(async () =>
                {
                    try
                    {
                        IReadOnlyList<RepositoryContent> existing = await client.Repository.Content.GetAllContentsByRef(Repository.Owner, GitHub.Repository.Name, githubPath, GitHub.Repository.Branch.Name);
                        return existing.FirstOrDefault();
                    }
                    catch (NotFoundException) { return null; }
                });

                if (cts.IsCancellationRequested) return null;

                if (existingContent == null)
                {
                    CreateFileRequest create = new(commitMessage, content, GitHub.Repository.Branch.Name);
                    await Helpers.Do(() => client.Repository.Content.CreateFile(Repository.Owner, GitHub.Repository.Name, githubPath, create));
                }
                else
                {
                    UpdateFileRequest update = new(commitMessage, content, existingContent.Sha, GitHub.Repository.Branch.Name);
                    await Helpers.Do(() => client.Repository.Content.UpdateFile(Repository.Owner, GitHub.Repository.Name, githubPath, update));
                }
            }
            else
            {
                // 4. BINARY FILE → Git Data API
                if (cts.IsCancellationRequested) return null;

                int maxRetries = 5;
                for (int attempt = 0; attempt < maxRetries; attempt++)
                {
                    BlobReference blobRef = await Helpers.Do(() =>
                    {
                        NewBlob blob = new()
                        {
                            Content = Convert.ToBase64String(fileBytes),
                            Encoding = Octokit.EncodingType.Base64
                        };
                        return client.Git.Blob.Create(Repository.Owner, GitHub.Repository.Name, blob);
                    });

                    Reference branchRef = await Helpers.Do(() => client.Git.Reference.Get(Repository.Owner, GitHub.Repository.Name, $"heads/{GitHub.Repository.Branch.Name}"));

                    Commit latestCommit = await Helpers.Do(() => client.Git.Commit.Get(Repository.Owner, GitHub.Repository.Name, branchRef.Object.Sha));

                    TreeResponse baseTree = await Helpers.Do(() => client.Git.Tree.Get(Repository.Owner, GitHub.Repository.Name, latestCommit.Tree.Sha));

                    NewTree newTree = new Octokit.NewTree { BaseTree = baseTree.Sha };
                    newTree.Tree.Add(new Octokit.NewTreeItem
                    {
                        Path = githubPath,
                        Mode = "100644",
                        Type = Octokit.TreeType.Blob,
                        Sha = blobRef.Sha
                    });

                    TreeResponse createdTree = await Helpers.Do(() => client.Git.Tree.Create(Repository.Owner, GitHub.Repository.Name, newTree));

                    NewCommit newCommit = new Octokit.NewCommit(commitMessage, createdTree.Sha, latestCommit.Sha);
                    Commit commit = await Helpers.Do(() => client.Git.Commit.Create(Repository.Owner, GitHub.Repository.Name, newCommit));

                    try
                    {
                        await Helpers.Do(() => client.Git.Reference.Update(Repository.Owner, GitHub.Repository.Name, $"heads/{GitHub.Repository.Branch.Name}", new Octokit.ReferenceUpdate(commit.Sha)));
                        break; // success
                    }
                    catch (Octokit.ApiValidationException ex) when (ex.Message.Contains("fast forward") && attempt < maxRetries - 1)
                    {
                        continue; // retry
                    }
                }
            }

            if (cts.IsCancellationRequested) return null;

            // 5. Refresh cache
            IReadOnlyList<RepositoryContent> refreshed = await Helpers.Do(() => client.Repository.Content.GetAllContentsByRef(Repository.Owner, GitHub.Repository.Name, githubPath, GitHub.Repository.Branch.Name));

            if (refreshed == null || !refreshed.Any()) return null;

            Entry entry = new()
            {
                Path = githubPath,
                Type = EntryType.File,
                Content = refreshed[0],
                FetchedAt = DateTime.UtcNow
            };

            Cache.Add(githubPath, entry);

            return entry;
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

            FileOperationDialogs.ResolveConflict ??= info =>
                {
                    Forms.GitHub_FileConflict.ShowInfo(info, GitHub_FileConflict.Operation.Copy);
                    if (Forms.GitHub_FileConflict.ShowDialog() != DialogResult.OK) return FileConflictAction.Skip;
                    return Forms.GitHub_FileConflict.Action;
                };

            Reference masterRef = await Helpers.Do(() => Program.GitHub.Client.Git.Reference.Get(Repository.Owner, GitHub.Repository.Name, $"heads/{GitHub.Repository.Branch.Name}"));

            Commit latestCommit = await Helpers.Do(() => Program.GitHub.Client.Git.Commit.Get(Repository.Owner, GitHub.Repository.Name, masterRef.Object.Sha));

            NewTree newTree = new NewTree { BaseTree = latestCommit.Tree.Sha };

            int processed = 0;
            List<Entry> sourceEntries = [];
            List<Entry> destEntries = [];
            List<(Entry srcEntry, string destPath, Entry destEntry)> entriesToCopy = [];

            // Gather entries and detect initial conflicts
            foreach (string srcPath in sourcePaths)
            {
                if (cts.Token.IsCancellationRequested) return;

                FileFilterAction filterAction = FileOperationDialogs.FilterFile?.Invoke(new()
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

            // Resolve conflicts and add files to tree
            foreach (var (srcEntry, originalDestPath, destEntry) in entriesToCopy)
            {
                if (cts.Token.IsCancellationRequested) return;

                string destPath = originalDestPath;

                if (destEntry != null && !replaceAll && !skipAll)
                {
                    FileConflictInfo conflictInfo = new()
                    {
                        TotalCount = entriesToCopy.Count,
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
                        if (conflictInfo.ReplaceMap != null && conflictInfo.ReplaceMap.TryGetValue(name, out bool replace) && !replace) continue;
                    }
                }

                // Add numbering if contents are identical
                if (destEntry != null && destEntry.ContentSha == srcEntry.ContentSha)
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

                newTree.Tree.Add(new NewTreeItem { Path = destPath, Mode = "100644", Type = TreeType.Blob, Sha = srcEntry.Content.Sha });

                Entry copiedEntry = await GetInfoRefreshAsync(destPath, false, cts: cts);
                Cache.Add(destPath, copiedEntry);

                if (destDir.Equals(CurrentPath, StringComparison.OrdinalIgnoreCase)) await UpdateExplorerView(destDir);

                processed++;
                reportProgress?.Invoke(processed * 100 / entriesToCopy.Count);
            }

            if (processed == 0) return;

            // Commit tree safely
            TreeResponse createdTree = await Helpers.Do(() => Program.GitHub.Client.Git.Tree.Create(Repository.Owner, GitHub.Repository.Name, newTree));

            NewCommit commit = new($"Copy {processed} file(s) to `{destDir}` by {Repository.Owner}", createdTree.Sha, latestCommit.Sha);
            Commit createdCommit = await Helpers.Do(() => Program.GitHub.Client.Git.Commit.Create(Repository.Owner, GitHub.Repository.Name, commit));

            await Helpers.Do(() => Program.GitHub.Client.Git.Reference.Update(Repository.Owner, GitHub.Repository.Name, $"heads/{GitHub.Repository.Branch.Name}", new ReferenceUpdate(createdCommit.Sha)));

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

            FileOperationDialogs.ResolveConflict ??= info =>
                {
                    Forms.GitHub_FileConflict.ShowInfo(info, GitHub_FileConflict.Operation.Copy);
                    if (Forms.GitHub_FileConflict.ShowDialog() != DialogResult.OK) return FileConflictAction.Skip;
                    return Forms.GitHub_FileConflict.Action;
                };

            Reference masterRef = await Helpers.Do(() => Program.GitHub.Client.Git.Reference.Get(Repository.Owner, GitHub.Repository.Name, $"heads/{GitHub.Repository.Branch.Name}"));

            Commit latestCommit = await Helpers.Do(() => Program.GitHub.Client.Git.Commit.Get(Repository.Owner, GitHub.Repository.Name, masterRef.Object.Sha));

            NewTree newTree = new() { BaseTree = latestCommit.Tree.Sha };

            int totalDirs = sourceDirs.Count;
            int processedDirs = 0;

            foreach (string sourceDir in sourceDirs)
            {
                if (cts.Token.IsCancellationRequested) return;

                string src = NormalizePath(sourceDir).TrimEnd('/');
                string dstFull = $"{destDirRoot}/{FileName(src)}";

                // Enumerate all entries recursively
                List<Entry> entries = [];
                await foreach (Entry entry in EnumerateEntriesAsync(src, EnumerateType.Both, recursive: true, ct: cts.Token)) entries.Add(entry);

                if (entries.Count == 0)
                {
                    await CreateDirectoryAsync(dstFull, cts: cts);
                    Cache.Add(new() { Path = dstFull, Type = EntryType.Dir });

                    processedDirs++;
                    reportProgress?.Invoke(processedDirs * 100 / totalDirs);
                    continue;
                }

                // Prepare conflict resolution
                IEnumerable<Entry> sourceEntries = [.. entries.Where(e => e.Type == EntryType.File)];
                List<Entry> destEntries = [];
                List<(Entry srcEntry, string destPath, Entry destEntry)> entriesToCopy = [];

                foreach (Entry entry in entries)
                {
                    string relative = entry.Path.Substring(src.Length).TrimStart('/', '\\');
                    string targetPath = NormalizePath($"{dstFull}/{relative}");
                    Entry destEntry = entry.Type == EntryType.File ? await GetInfoRefreshAsync(targetPath, false, cts: cts) : null;

                    entriesToCopy.Add((entry, targetPath, destEntry));
                    if (destEntry != null) destEntries.Add(destEntry);
                }

                // Resolve conflicts and add to tree
                foreach (var (srcEntry, originalDestPath, destEntry) in entriesToCopy)
                {
                    if (cts.Token.IsCancellationRequested) return;

                    string destPath = originalDestPath;
                    if (srcEntry.Type != EntryType.File) continue;

                    if (destEntry != null)
                    {
                        if (skipAll) continue;

                        FileConflictAction action = replaceAll ? FileConflictAction.ReplaceAll :
                            FileOperationDialogs.ResolveConflict(new()
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
                            if (destEntry != null && !FileOperationDialogs.ResolveConflict.Invoke(new FileConflictInfo { SourceFiles = [.. sourceEntries], DestinationFiles = [.. destEntries] }).Equals(FileConflictAction.ReplaceAll))
                                continue;
                        }

                        // Numbering for identical files
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

                    newTree.Tree.Add(new() { Path = destPath, Mode = "100644", Type = TreeType.Blob, Sha = srcEntry.Content.Sha });
                }

                processedDirs++;
                reportProgress?.Invoke(processedDirs * 100 / totalDirs);
            }

            // Commit all changes safely
            TreeResponse createdTree = await Helpers.Do(() => Program.GitHub.Client.Git.Tree.Create(Repository.Owner, GitHub.Repository.Name, newTree));

            string commitMessage = $"Copy {sourceDirs.Count} directories to `{destDirRoot}` by {Repository.Owner}";
            NewCommit newCommit = new NewCommit(commitMessage, createdTree.Sha, latestCommit.Sha);
            Commit commit = await Helpers.Do(() => Program.GitHub.Client.Git.Commit.Create(Repository.Owner, GitHub.Repository.Name, newCommit));

            await Helpers.Do(() => Program.GitHub.Client.Git.Reference.Update(Repository.Owner, GitHub.Repository.Name, $"heads/{GitHub.Repository.Branch.Name}", new ReferenceUpdate(commit.Sha)));

            // Update cache for directories
            foreach (string sourceDir in sourceDirs)
            {
                string destDirFull = destDirRoot + "/" + FileName(sourceDir);
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

        public static async Task MoveFilesAsync(IReadOnlyList<string> sourcePaths, string destDirectory, string message = null, CancellationTokenSource cts = null, Action<int> reportProgress = null)
        {
            if (sourcePaths == null || sourcePaths.Count == 0) throw new ArgumentException(nameof(sourcePaths));

            cts ??= new();
            reportProgress?.Invoke(0);

            string destDir = NormalizePath(destDirectory).TrimEnd('/');
            bool replaceAll = false;
            bool skipAll = false;

            Reference masterRef = await Helpers.Do(() => Program.GitHub.Client.Git.Reference.Get(Repository.Owner, GitHub.Repository.Name, $"heads/{GitHub.Repository.Branch.Name}"));

            Commit latestCommit = await Helpers.Do(() => Program.GitHub.Client.Git.Commit.Get(Repository.Owner, GitHub.Repository.Name, masterRef.Object.Sha));

            NewTree newTree = new() { BaseTree = latestCommit.Tree.Sha };

            FileOperationDialogs.ResolveConflict ??= info =>
                {
                    Forms.GitHub_FileConflict.ShowInfo(info, GitHub_FileConflict.Operation.Move);
                    if (Forms.GitHub_FileConflict.ShowDialog() != DialogResult.OK) return FileConflictAction.Skip;
                    return Forms.GitHub_FileConflict.Action;
                };

            int processed = 0;
            List<Entry> sourceEntries = [];
            List<Entry> destEntries = [];
            List<(Entry srcEntry, string destPath, Entry destEntry)> entriesToMove = [];

            // Gather all entries and detect existing destinations
            foreach (string srcPath in sourcePaths)
            {
                if (cts.Token.IsCancellationRequested) return;

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

                entriesToMove.Add((srcEntry, dest, destEntry));
                sourceEntries.Add(srcEntry);
                if (destEntry != null) destEntries.Add(destEntry);
            }

            // Resolve conflicts, add to Git tree
            foreach (var (srcEntry, destPath, destEntry) in entriesToMove)
            {
                if (cts.Token.IsCancellationRequested) throw new OperationCanceledException();

                string finalDest = destPath;

                if (destEntry != null)
                {
                    if (skipAll) continue;

                    FileConflictAction action = replaceAll ? FileConflictAction.ReplaceAll :
                        FileOperationDialogs.ResolveConflict(new()
                        {
                            TotalCount = entriesToMove.Count,
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
                        if (destEntry != null && !FileOperationDialogs.ResolveConflict.Invoke(new FileConflictInfo
                        {
                            SourceFiles = [.. sourceEntries],
                            DestinationFiles = [.. destEntries]
                        }).Equals(FileConflictAction.ReplaceAll))
                            continue;
                    }

                    // Numbering if identical
                    if (destEntry.ContentSha == srcEntry.ContentSha)
                    {
                        string nameOnly = Path.GetFileNameWithoutExtension(finalDest);
                        string ext = Path.GetExtension(finalDest);
                        int count = 1;
                        string numberedDest = finalDest;
                        while (await GetInfoRefreshAsync(numberedDest, false, cts: cts) != null)
                        {
                            numberedDest = $"{ParentDirectoryName(finalDest)}/{nameOnly} ({count}){ext}";
                            count++;
                        }
                        finalDest = numberedDest;
                    }
                }

                // Add move operation to tree: create new path, delete old path
                newTree.Tree.Add(new NewTreeItem { Path = finalDest, Mode = "100644", Type = TreeType.Blob, Sha = srcEntry.Content.Sha });
                newTree.Tree.Add(new NewTreeItem { Path = srcEntry.Path, Mode = "100644", Type = TreeType.Blob, Sha = string.Empty });

                processed++;
                reportProgress?.Invoke(processed * 100 / entriesToMove.Count);
            }

            if (processed > 0)
            {
                TreeResponse createdTree = await Helpers.Do(() => Program.GitHub.Client.Git.Tree.Create(Repository.Owner, GitHub.Repository.Name, newTree));

                NewCommit commit = new(message ?? $"Move {processed} file(s) to `{destDir}` by {Repository.Owner}", createdTree.Sha, latestCommit.Sha);
                Commit createdCommit = await Helpers.Do(() => Program.GitHub.Client.Git.Commit.Create(Repository.Owner, GitHub.Repository.Name, commit));

                await Helpers.Do(() => Program.GitHub.Client.Git.Reference.Update(Repository.Owner, GitHub.Repository.Name, $"heads/{GitHub.Repository.Branch.Name}", new ReferenceUpdate(createdCommit.Sha)));

                // Update cache
                foreach (string srcPath in sourcePaths)
                {
                    string src = NormalizePath(srcPath).TrimEnd('/');
                    string dest = destDir + "/" + FileName(src);

                    Cache.Remove(src);

                    Entry updatedEntry = await GetInfoRefreshAsync(dest, false);
                    Cache.Add(dest, updatedEntry);

                    if (destDir.Equals(CurrentPath, StringComparison.OrdinalIgnoreCase)) await UpdateExplorerView(destDir);
                }
            }

            reportProgress?.Invoke(100);
        }
        public static async Task<Entry> MoveFileAsync(string sourcePath, string destPath, string message = null, CancellationTokenSource cts = null, Action<int> reportProgress = null)
        {
            if (string.IsNullOrWhiteSpace(sourcePath)) throw new ArgumentException(nameof(sourcePath));
            if (string.IsNullOrWhiteSpace(destPath)) throw new ArgumentException(nameof(destPath));

            cts ??= new();
            reportProgress?.Invoke(0);

            string src = NormalizePath(sourcePath);
            string dest = NormalizePath(destPath);
            string workingDir = ParentDirectoryName(dest);

            bool replaceAll = false;
            bool skipAll = false;

            if (cts.Token.IsCancellationRequested) return null;

            Reference masterRef = await Helpers.Do(() => Program.GitHub.Client.Git.Reference.Get(Repository.Owner, GitHub.Repository.Name, $"heads/{GitHub.Repository.Branch.Name}"));

            Commit latestCommit = await Helpers.Do(() => Program.GitHub.Client.Git.Commit.Get(Repository.Owner, GitHub.Repository.Name, masterRef.Object.Sha));

            NewTree newTree = new() { BaseTree = latestCommit.Tree.Sha };

            FileOperationDialogs.ResolveConflict ??= info =>
                {
                    Forms.GitHub_FileConflict.ShowInfo(info, GitHub_FileConflict.Operation.Move);
                    if (Forms.GitHub_FileConflict.ShowDialog() != DialogResult.OK) return FileConflictAction.Skip;
                    return Forms.GitHub_FileConflict.Action;
                };

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
                        if (conflictInfo.ReplaceMap != null && conflictInfo.ReplaceMap.TryGetValue(name, out bool replace) && !replace) return destEntry;
                    }
                }
            }

            // Add move to tree
            newTree.Tree.Add(new NewTreeItem { Path = dest, Mode = "100644", Type = TreeType.Blob, Sha = srcEntry.Content.Sha });
            newTree.Tree.Add(new NewTreeItem { Path = srcEntry.Path, Mode = "100644", Type = TreeType.Blob, Sha = string.Empty });

            Cache.Remove(src);
            reportProgress?.Invoke(50);

            TreeResponse createdTree = await Helpers.Do(() => Program.GitHub.Client.Git.Tree.Create(Repository.Owner, GitHub.Repository.Name, newTree));

            NewCommit commit = new(message ?? $"Move file `{FileName(src)}` to `{ParentDirectoryName(dest)}` by {Repository.Owner}", createdTree.Sha, latestCommit.Sha);

            Commit createdCommit = await Helpers.Do(() => Program.GitHub.Client.Git.Commit.Create(Repository.Owner, GitHub.Repository.Name, commit));

            await Helpers.Do(() => Program.GitHub.Client.Git.Reference.Update(Repository.Owner, GitHub.Repository.Name, $"heads/{GitHub.Repository.Branch.Name}", new ReferenceUpdate(createdCommit.Sha)));

            reportProgress?.Invoke(100);

            Entry updatedEntry = await GetInfoRefreshAsync(dest, false, cts: cts);
            Cache.Add(updatedEntry);

            if (workingDir.Equals(CurrentPath, StringComparison.OrdinalIgnoreCase)) await UpdateExplorerView(workingDir);

            return updatedEntry;
        }

        public static async Task MoveDirectoriesAsync(IReadOnlyList<string> sourceDirs, string destDirectory, string message = null, CancellationTokenSource cts = null, Action<int> reportProgress = null)
        {
            if (sourceDirs == null || sourceDirs.Count == 0) throw new ArgumentException("No source directories provided.");

            cts ??= new();
            reportProgress?.Invoke(0);

            string destDirRoot = NormalizePath(destDirectory).TrimEnd('/');

            if (Program.GitHub.Client == null) throw new InvalidOperationException("GitHub client is null.");
            if (string.IsNullOrEmpty(Repository.Owner)) throw new InvalidOperationException("Repository.Owner is null or empty.");
            if (string.IsNullOrEmpty(GitHub.Repository.Name)) throw new InvalidOperationException("Repository name is null or empty.");
            if (string.IsNullOrEmpty(GitHub.Repository.Branch.Name)) throw new InvalidOperationException("Branch is null or empty.");

            FileOperationDialogs.ResolveConflict ??= info =>
                {
                    Forms.GitHub_FileConflict.ShowInfo(info, GitHub_FileConflict.Operation.Move);
                    if (Forms.GitHub_FileConflict.ShowDialog() != DialogResult.OK) return FileConflictAction.Skip;
                    return Forms.GitHub_FileConflict.Action;
                };

            // 1. Get latest commit safely
            Reference masterRef = await Helpers.Do(() => Program.GitHub.Client.Git.Reference.Get(Repository.Owner, GitHub.Repository.Name, $"heads/{GitHub.Repository.Branch.Name}"));
            Commit latestCommit = await Helpers.Do(() => Program.GitHub.Client.Git.Commit.Get(Repository.Owner, GitHub.Repository.Name, masterRef.Object.Sha));
            NewTree newTree = new() { BaseTree = latestCommit.Tree.Sha };

            bool replaceAll = false;
            bool skipAll = false;

            // 2. Collect all files recursively from source directories
            List<(string srcPath, string destPath, Entry destEntry)> allEntries = [];

            foreach (string sourceDir in sourceDirs)
            {
                if (cts.Token.IsCancellationRequested) return;

                string srcDir = NormalizePath(sourceDir).TrimEnd('/');
                string destRootFull = destDirRoot + "/" + FileName(srcDir);

                await foreach (Entry file in EnumerateEntriesAsync(srcDir, EnumerateType.Files, recursive: true, ct: cts.Token))
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

            // 3. Handle conflicts and prepare Git tree
            foreach (var (srcPath, destPath, destEntry) in allEntries)
            {
                if (cts.Token.IsCancellationRequested) return;

                Entry freshSrcEntry = await GetInfoRefreshAsync(srcPath, false, cts: cts);
                if (freshSrcEntry == null || freshSrcEntry.Type != EntryType.File)
                {
                    processedItems++;
                    reportProgress?.Invoke(processedItems * 100 / totalItems);
                    continue;
                }

                if (destEntry != null)
                {
                    if (skipAll) { processedItems++; reportProgress?.Invoke(processedItems * 100 / totalItems); continue; }

                    if (!replaceAll)
                    {
                        FileConflictInfo conflictInfo = new()
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

                newTree.Tree.Add(new NewTreeItem { Path = destPath, Mode = "100644", Type = TreeType.Blob, Sha = freshSrcEntry.Content.Sha });
                newTree.Tree.Add(new NewTreeItem { Path = freshSrcEntry.Path, Mode = "100644", Type = TreeType.Blob, Sha = string.Empty });

                processedItems++;
                reportProgress?.Invoke(processedItems * 100 / totalItems);
            }

            // 4. Commit all changes safely
            if (allEntries.Count > 0)
            {
                TreeResponse createdTree = await Helpers.Do(() => Program.GitHub.Client.Git.Tree.Create(Repository.Owner, GitHub.Repository.Name, newTree));
                NewCommit commit = new NewCommit(message ?? $"Move {allEntries.Count} item(s) to `{destDirRoot}` by {Repository.Owner}", createdTree.Sha, latestCommit.Sha);
                Commit createdCommit = await Helpers.Do(() => Program.GitHub.Client.Git.Commit.Create(Repository.Owner, GitHub.Repository.Name, commit));
                await Helpers.Do(() => Program.GitHub.Client.Git.Reference.Update(Repository.Owner, GitHub.Repository.Name, $"heads/{GitHub.Repository.Branch.Name}", new ReferenceUpdate(createdCommit.Sha)));

                // Update cache
                foreach (string sourceDir in sourceDirs)
                {
                    string srcDir = NormalizePath(sourceDir).TrimEnd('/');
                    string destDirFull = destDirRoot + "/" + FileName(srcDir);

                    Cache.Remove(srcDir);

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
        public static async Task<Entry> MoveDirectoryAsync(string sourceDir, string destDirectory, string message = null, CancellationTokenSource cts = null, Action<int> reportProgress = null)
        {
            if (string.IsNullOrWhiteSpace(sourceDir)) throw new ArgumentException(nameof(sourceDir));
            if (string.IsNullOrWhiteSpace(destDirectory)) throw new ArgumentException(nameof(destDirectory));

            cts ??= new();
            reportProgress?.Invoke(0);

            string srcDir = NormalizePath(sourceDir).TrimEnd('/');
            string dstRootFull = NormalizePath(destDirectory).TrimEnd('/');

            // Get latest commit safely
            Reference masterRef = await Helpers.Do(() => Program.GitHub.Client.Git.Reference.Get(Repository.Owner, GitHub.Repository.Name, $"heads/{GitHub.Repository.Branch.Name}"));
            Commit latestCommit = await Helpers.Do(() => Program.GitHub.Client.Git.Commit.Get(Repository.Owner, GitHub.Repository.Name, masterRef.Object.Sha));
            NewTree newTree = new() { BaseTree = latestCommit.Tree.Sha };

            bool replaceAll = false;
            bool skipAll = false;

            FileOperationDialogs.ResolveConflict ??= info =>
                {
                    Forms.GitHub_FileConflict.ShowInfo(info, GitHub_FileConflict.Operation.Move);
                    if (Forms.GitHub_FileConflict.ShowDialog() != DialogResult.OK) return FileConflictAction.Skip;
                    return Forms.GitHub_FileConflict.Action;
                };

            // Enumerate all files in directory recursively
            List<(Entry srcEntry, string destPath, Entry destEntry)> allEntries = [];
            await foreach (Entry file in EnumerateEntriesAsync(srcDir, EnumerateType.Files, recursive: true, ct: cts.Token))
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
                if (cts is not null && cts.Token.IsCancellationRequested) return null;

                if (destEntry != null)
                {
                    if (skipAll) { processedItems++; continue; }

                    if (!replaceAll)
                    {
                        FileConflictInfo conflictInfo = new()
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

            // Commit changes safely
            if (totalItems > 0)
            {
                TreeResponse createdTree = await Helpers.Do(() => Program.GitHub.Client.Git.Tree.Create(Repository.Owner, GitHub.Repository.Name, newTree));
                NewCommit commit = new(message ?? $"Move directory `{FileName(srcDir)}` to `{destDirectory}` by {Repository.Owner}", createdTree.Sha, latestCommit.Sha);
                Commit createdCommit = await Helpers.Do(() => Program.GitHub.Client.Git.Commit.Create(Repository.Owner, GitHub.Repository.Name, commit));
                await Helpers.Do(() => Program.GitHub.Client.Git.Reference.Update(Repository.Owner, GitHub.Repository.Name, $"heads/{GitHub.Repository.Branch.Name}", new ReferenceUpdate(createdCommit.Sha)));
            }

            Cache.Remove(srcDir);
            Entry updatedEntry = await Entry.FromRepositoryContent(await GetRepositoryContentAsync(dstRootFull));
            Cache.Add(updatedEntry);
            if (dstRootFull.Equals(CurrentPath, StringComparison.OrdinalIgnoreCase)) await UpdateExplorerView(dstRootFull);

            reportProgress?.Invoke(100);

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
                if (cts is not null && cts.Token.IsCancellationRequested) return;

                string normalizedPath = NormalizePath(path);
                string workingDir = ParentDirectoryName(normalizedPath);

                try
                {
                    Entry file = await GetInfoRefreshAsync(normalizedPath, recursive: false, cts: cts);
                    if (file?.Type != EntryType.File)
                    {
                        processed++;
                        reportProgress?.Invoke(processed * 100 / total);
                        continue;
                    }

                    DeleteFileRequest deleteRequest = new($"{Repository.Owner} deleted `{file.Path}`", file.Content.Sha)
                    {
                        Branch = GitHub.Repository.Branch.Name
                    };

                    // Execute DeleteFile safely
                    await Helpers.Do(async () => Program.GitHub.Client.Repository.Content.DeleteFile(Repository.Owner, GitHub.Repository.Name, file.Path, deleteRequest));

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
                    Forms.BugReport.Throw(ex);
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
            if (dirs == null || dirs.Count == 0) throw new ArgumentException("No directories provided.");

            cts ??= new();
            reportProgress?.Invoke(0);

            int total = dirs.Count;
            int processed = 0;

            List<string> normalizedDirs = [.. dirs.Select(d => NormalizePath(d))];
            List<NewTreeItem> allFilesToDelete = [];

            try
            {
                if (Program.GitHub.Client == null) throw new InvalidOperationException("GitHub client is null.");
                if (string.IsNullOrEmpty(Repository.Owner)) throw new InvalidOperationException("Repository.Owner is null or empty.");
                if (string.IsNullOrEmpty(GitHub.Repository.Name)) throw new InvalidOperationException("Repository name is null or empty.");
                if (string.IsNullOrEmpty(GitHub.Repository.Branch.Name)) throw new InvalidOperationException("Branch is null or empty.");

                Reference branchRef = await Helpers.Do(() => Program.GitHub.Client.Git.Reference.Get(Repository.Owner, GitHub.Repository.Name, $"heads/{GitHub.Repository.Branch.Name}"));
                Commit latestCommit = await Helpers.Do(() => Program.GitHub.Client.Git.Commit.Get(Repository.Owner, GitHub.Repository.Name, branchRef.Object.Sha));
                TreeResponse tree = await Helpers.Do(() => Program.GitHub.Client.Git.Tree.GetRecursive(Repository.Owner, GitHub.Repository.Name, latestCommit.Tree.Sha));

                foreach (string dir in normalizedDirs)
                {
                    if (cts is not null && cts.Token.IsCancellationRequested) return;

                    string workingDir = ParentDirectoryName(dir);

                    List<NewTreeItem> files = [.. tree.Tree
                        .Where(t => t.Type == TreeType.Blob && NormalizePath(t.Path).StartsWith(dir, StringComparison.OrdinalIgnoreCase))
                        .Select(f => new NewTreeItem
                        {
                            Path = f.Path,
                            Mode = f.Mode,
                            Type = TreeType.Blob,
                            Sha = string.Empty
                        })];

                    allFilesToDelete.AddRange(files);

                    Cache.Remove(dir);
                    await UpdateExplorerView(workingDir);

                    processed++;
                    reportProgress?.Invoke(processed * 100 / total);
                }

                if (allFilesToDelete.Any())
                {
                    NewTree newTree = new() { BaseTree = latestCommit.Tree.Sha };
                    foreach (var item in allFilesToDelete) newTree.Tree.Add(item);

                    TreeResponse createdTree = await Helpers.Do(() => Program.GitHub.Client.Git.Tree.Create(Repository.Owner, GitHub.Repository.Name, newTree));
                    string commitMessage = $"Delete directories recursively by {Repository.Owner}";
                    NewCommit newCommit = new NewCommit(commitMessage, createdTree.Sha, latestCommit.Sha);
                    Commit commit = await Helpers.Do(() => Program.GitHub.Client.Git.Commit.Create(Repository.Owner, GitHub.Repository.Name, newCommit));
                    await Helpers.Do(() => Program.GitHub.Client.Git.Reference.Update(Repository.Owner, GitHub.Repository.Name, $"heads/{GitHub.Repository.Branch.Name}", new ReferenceUpdate(commit.Sha)));
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
                Forms.BugReport.Throw(ex);
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
            if (string.IsNullOrEmpty(Repository.Owner) || string.IsNullOrEmpty(GitHub.Repository.Name) || string.IsNullOrEmpty(GitHub.Repository.Branch.Name)) throw new InvalidOperationException("Owner, repository, or branch not set.");

            path = NormalizePath(path);

            Regex regex = null;
            if (!string.IsNullOrEmpty(searchPattern) && searchPattern != "*") regex = new("^" + Regex.Escape(searchPattern).Replace("\\*", ".*").Replace("\\?", ".") + "$", RegexOptions.IgnoreCase);

            IReadOnlyList<RepositoryContent> contents;
            try
            {
                contents = await Helpers.Do(() => Program.GitHub.Client.Repository.Content.GetAllContentsByRef(Repository.Owner, GitHub.Repository.Name, path, GitHub.Repository.Branch.Name));
            }
            catch (Octokit.NotFoundException)
            {
                yield break;
            }

            foreach (var item in contents)
            {
                if (ct.IsCancellationRequested) yield break;

                if (item == null || string.IsNullOrEmpty(item.Path)) continue;

                EntryType entryType = item.Type == Octokit.ContentType.Dir ? EntryType.Dir : EntryType.File;
                Entry entry = new() { Path = NormalizePath(item.Path), Type = entryType, Content = item, FetchedAt = DateTime.UtcNow };

                bool match = (entry.Type == EntryType.File && type.HasFlag(EnumerateType.Files) && (regex == null || regex.IsMatch(FileName(entry.Path)))) || (entry.Type == EntryType.Dir && type.HasFlag(EnumerateType.Directories));

                if (match) yield return entry;

                if (recursive && entry.Type == EntryType.Dir)
                {
                    await foreach (var sub in EnumerateEntriesAsync(entry.Path, type, searchPattern, true, ct)) yield return sub;
                }
            }
        }
    }
}