using Octokit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WinPaletter.GitHub
{
    public static partial class FileSystem
    {
        public static async Task UploadFileAsync(string githubPath, string localFilePath, string commitMessage = null, CancellationToken ct = default, Action<int> reportProgress = null)
        {
            commitMessage ??= $"Uploaded `{localFilePath}` into `{githubPath}` by {_owner}";
            reportProgress?.Invoke(0);

            var client = Program.GitHub.Client;

            using FileStream fs = new(localFilePath, System.IO.FileMode.Open, FileAccess.Read, FileShare.Read, 81920, true);
            long total = fs.Length;
            long read = 0;

            StringBuilder base64Builder = new();
            byte[] buffer = new byte[81920];
            int bytesRead;

            while ((bytesRead = await fs.ReadAsync(buffer.AsMemory(0, buffer.Length), ct)) > 0)
            {
                ct.ThrowIfCancellationRequested();
                string chunkBase64 = Convert.ToBase64String(buffer, 0, bytesRead);
                base64Builder.Append(chunkBase64);

                read += bytesRead;
                reportProgress?.Invoke((int)(read * 100L / total));
            }

            var blob = new Octokit.NewBlob
            {
                Content = base64Builder.ToString(),
                Encoding = Octokit.EncodingType.Base64
            };
            var blobRef = await client.Git.Blob.Create(_owner, _repo, blob);

            Reference masterRef = await client.Git.Reference.Get(_owner, _repo, $"heads/{_branch}");
            Commit latestCommit = await client.Git.Commit.Get(_owner, _repo, masterRef.Object.Sha);
            TreeResponse baseTree = await client.Git.Tree.Get(_owner, _repo, latestCommit.Tree.Sha);

            NewTree newTree = new() { BaseTree = baseTree.Sha };
            newTree.Tree.Add(new()
            {
                Path = githubPath,
                Mode = "100644",
                Type = TreeType.Blob,
                Sha = blobRef.Sha
            });

            TreeResponse createdTree = await client.Git.Tree.Create(_owner, _repo, newTree);
            NewCommit newCommit = new(commitMessage, createdTree.Sha, latestCommit.Sha);
            Commit commit = await client.Git.Commit.Create(_owner, _repo, newCommit);
            await client.Git.Reference.Update(_owner, _repo, $"heads/{_branch}", new(commit.Sha));

            _infoCache.TryRemove(githubPath, out _);
            reportProgress?.Invoke(100);
        }

        public static async Task UploadDirectoryAsync(string localPath, string githubPath, int maxParallel = 4, CancellationTokenSource cts = null, Action<int> reportProgress = null)
        {
            cts ??= new();
            string[] files = Directory.GetFiles(localPath, "*", SearchOption.AllDirectories);
            int total = files.Length;
            int processed = 0;

            using SemaphoreSlim throttler = new(maxParallel);

            IEnumerable<Task> tasks = files.Select(async file =>
            {
                await throttler.WaitAsync(cts.Token);
                try
                {
                    string relative = file.Substring(localPath.Length).TrimStart('\\', '/').Replace("\\", "/");
                    string githubFile = $"{githubPath}/{relative}";
                    await UploadFileAsync(githubFile, file, $"Upload `{relative}`", cts.Token, progress => { });

                    Interlocked.Increment(ref processed);
                    reportProgress?.Invoke((int)((long)processed * 100 / total));
                }
                finally
                {
                    throttler.Release();
                }
            });

            await Task.WhenAll(tasks);
            reportProgress?.Invoke(100);
        }

        public static async Task DownloadFileAsync(string githubPath, string localSavePath, CancellationTokenSource cts = null, Action<int> reportProgress = null, Action onCompleted = null, Action<Exception> onError = null, Action onCancelled = null)
        {
            cts ??= new();
            string url = $"https://raw.githubusercontent.com/{_owner}/{_repo}/{_branch}/{githubPath}";

            using DownloadManager dm = new();

            dm.DownloadProgressChanged += (s, e) => reportProgress?.Invoke((int)e.ProgressPercentage);
            dm.DownloadFileCompleted += (s, e) =>
            {
                if (cts.Token.IsCancellationRequested) onCancelled?.Invoke();
                else onCompleted?.Invoke();
            };
            dm.DownloadErrorOccurred += (s, msg) => onError?.Invoke(new Exception(msg));

            try
            {
                await dm.DownloadFileAsync(url, localSavePath, cts);
            }
            catch (OperationCanceledException)
            {
                onCancelled?.Invoke();
            }
            catch (Exception ex)
            {
                onError?.Invoke(ex);
            }
        }

        public static async Task DownloadDirectoryAsync(
           string githubPath,
           string localPath,
           CancellationTokenSource cts = null,
           Action<int> overallProgress = null,
           Action<string> fileCompleted = null,
           Action<string, Exception> fileError = null,
           Action<string> fileCancelled = null,
           Action onCompleted = null,
           Action onCancelled = null)
        {
            cts ??= new();

            long totalBytes = 0;
            long downloadedBytes = 0;

            var allEntries = new List<Entry>();
            await foreach (var entry in EnumerateAsync<Entry>(githubPath, recursive: true, ct: cts.Token))
            {
                allEntries.Add(entry);
                if (entry.Type == ElementType.File && entry.Size > 0) totalBytes += entry.Size;
            }

            foreach (var entry in allEntries)
            {
                cts.Token.ThrowIfCancellationRequested();

                string relativePath = entry.Path.Substring(githubPath.Length).TrimStart('/', '\\');
                string localFile = Path.Combine(localPath, relativePath.Replace("/", "\\"));

                try
                {
                    if (entry.Type == ElementType.Dir)
                    {
                        Directory.CreateDirectory(localFile);
                    }
                    else
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(localFile));

                        using DownloadManager dm = new();
                        dm.DownloadProgressChanged += (s, e) =>
                        {
                            long fileDownloaded = e.BytesReceived;
                            long prevDownloaded = Interlocked.Read(ref downloadedBytes);
                            Interlocked.Add(ref downloadedBytes, fileDownloaded - prevDownloaded);
                            overallProgress?.Invoke(totalBytes > 0 ? (int)(downloadedBytes * 100 / totalBytes) : 0);
                        };
                        dm.DownloadFileCompleted += (s, e) => fileCompleted?.Invoke(localFile);
                        dm.DownloadErrorOccurred += (s, msg) => fileError?.Invoke(localFile, new Exception(msg));

                        await dm.DownloadFileAsync($"https://raw.githubusercontent.com/{_owner}/{_repo}/{_branch}/{entry.Path}", localFile, cts);

                        Interlocked.Add(ref downloadedBytes, entry.Size);
                        overallProgress?.Invoke(totalBytes > 0 ? (int)(downloadedBytes * 100 / totalBytes) : 0);
                    }
                }
                catch (OperationCanceledException)
                {
                    fileCancelled?.Invoke(localFile);
                }
                catch (Exception ex)
                {
                    fileError?.Invoke(localFile, ex);
                }
            }

            if (cts.Token.IsCancellationRequested) onCancelled?.Invoke();
            else onCompleted?.Invoke();
        }
    }
}