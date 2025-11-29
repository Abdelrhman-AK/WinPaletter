using Octokit;
using Serilog.Events;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace WinPaletter
{
    public class GitHubRepoManager
    {
        private GitHubClient _client => Program.GitHub.Client;
        
        #region Check if Repository Exists or Forked

        public async Task<bool> RepositoryExistsAsync(string owner, string repoName, Action<double>? progress = null)
        {
            if (!Program.IsNetworkAvailable) return false;

            try
            {
                progress?.Invoke(0);
                var repo = await _client.Repository.Get(owner, repoName).ConfigureAwait(false);
                Program.Log?.Write(LogEventLevel.Information, $"Repository found: {repo.FullName}");
                progress?.Invoke(100);
                return true;
            }
            catch (NotFoundException)
            {
                Program.Log?.Write(LogEventLevel.Information, $"Repository not found: {owner}/{repoName}");
                progress?.Invoke(100);
                return false;
            }
            catch (Exception ex)
            {
                Program.Log?.Write(LogEventLevel.Error, $"Error checking repository {owner}/{repoName}", ex);
                progress?.Invoke(100);
                return false;
            }
        }

        #endregion

        #region Fork Repository

        public async Task<Repository?> ForkRepositoryAsync(string owner, string repoName, Action<double>? progress = null)
        {
            if (!Program.IsNetworkAvailable) return null;

            try
            {
                progress?.Invoke(0);
                Repository forked = await _client.Repository.Forks.Create(owner, repoName, new NewRepositoryFork()).ConfigureAwait(false);
                Program.Log?.Write(LogEventLevel.Information, $"Forked repository: {forked.FullName}");
                progress?.Invoke(100);
                return forked;
            }
            catch (Exception ex)
            {
                Program.Log?.Write(LogEventLevel.Error, $"Error forking repository {owner}/{repoName}", ex);
                progress?.Invoke(100);
                return null;
            }
        }

        #endregion

        #region Read File Content

        public async Task<string?> ReadFileAsync(string owner, string repoName, string filePath, string branch = "main", Action<double>? progress = null)
        {
            if (!Program.IsNetworkAvailable) return null;

            try
            {
                progress?.Invoke(0);
                var file = await _client.Repository.Content.GetAllContentsByRef(owner, repoName, filePath, branch).ConfigureAwait(false);
                if (file.Count > 0)
                {
                    Program.Log?.Write(LogEventLevel.Information, $"Read file {filePath} from {owner}/{repoName}");
                    progress?.Invoke(100);
                    return file[0].Content;
                }
            }
            catch (NotFoundException)
            {
                Program.Log?.Write(LogEventLevel.Warning, $"File {filePath} not found in {owner}/{repoName}");
            }
            catch (Exception ex)
            {
                Program.Log?.Write(LogEventLevel.Error, $"Error reading file {filePath} in {owner}/{repoName}", ex);
            }
            progress?.Invoke(100);
            return null;
        }

        #endregion

        #region Upload / Download File with Progress & Cancellation

        public async Task UploadFileAsync(string owner, string repoName, string filePath, string localFilePath,
            string commitMessage, string branch = "main", CancellationToken? cancellationToken = null, Action<double>? progress = null)
        {
            if (!Program.IsNetworkAvailable) return;

            try
            {
                cancellationToken ??= CancellationToken.None;
                progress?.Invoke(0);

                byte[] content;
                using (FileStream fs = new FileStream(localFilePath, System.IO.FileMode.Open, FileAccess.Read, FileShare.Read, 4096, true))
                {
                    content = new byte[fs.Length];
                    await fs.ReadAsync(content, 0, (int)fs.Length, cancellationToken.Value).ConfigureAwait(false);
                }

                string base64 = Convert.ToBase64String(content);

                RepositoryContent? existing = null;
                try
                {
                    existing = await _client.Repository.Content.GetAllContentsByRef(owner, repoName, filePath, branch)
                        .ContinueWith(t => t.Result.Count > 0 ? t.Result[0] : null, cancellationToken.Value);
                }
                catch (NotFoundException) { }

                if (cancellationToken.Value.IsCancellationRequested) return;

                if (existing == null)
                {
                    var newFile = new CreateFileRequest(commitMessage, base64, branch);
                    await _client.Repository.Content.CreateFile(owner, repoName, filePath, newFile).ConfigureAwait(false);
                    Program.Log?.Write(LogEventLevel.Information, $"Created file {filePath} in {owner}/{repoName}");
                }
                else
                {
                    var updateFile = new UpdateFileRequest(commitMessage, base64, existing.Sha, branch);
                    await _client.Repository.Content.UpdateFile(owner, repoName, filePath, updateFile).ConfigureAwait(false);
                    Program.Log?.Write(LogEventLevel.Information, $"Updated file {filePath} in {owner}/{repoName}");
                }

                progress?.Invoke(100);
            }
            catch (Exception ex)
            {
                Program.Log?.Write(LogEventLevel.Error, $"Error uploading file {filePath} to {owner}/{repoName}", ex);
                progress?.Invoke(100);
            }
        }

        public async Task DownloadFileAsync(string owner, string repoName, string filePath, string localSavePath,
            string branch = "main", CancellationToken? cancellationToken = null, Action<double>? progress = null)
        {
            if (!Program.IsNetworkAvailable) return;

            try
            {
                cancellationToken ??= CancellationToken.None;
                progress?.Invoke(0);

                var file = await _client.Repository.Content.GetAllContentsByRef(owner, repoName, filePath, branch).ConfigureAwait(false);
                if (file.Count > 0)
                {
                    byte[] bytes = Convert.FromBase64String(file[0].Content);
                    using (FileStream fs = new(localSavePath, System.IO.FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        int total = bytes.Length;
                        int chunkSize = 4096;
                        for (int i = 0; i < total; i += chunkSize)
                        {
                            cancellationToken.Value.ThrowIfCancellationRequested();
                            int size = Math.Min(chunkSize, total - i);
                            await fs.WriteAsync(bytes, i, size, cancellationToken.Value).ConfigureAwait(false);
                            progress?.Invoke((i + size) * 100.0 / total);
                        }
                    }
                    Program.Log?.Write(LogEventLevel.Information, $"Downloaded file {filePath} to {localSavePath}");
                }
                else
                {
                    Program.Log?.Write(LogEventLevel.Warning, $"File {filePath} not found in {owner}/{repoName}");
                }
                progress?.Invoke(100);
            }
            catch (Exception ex)
            {
                Program.Log?.Write(LogEventLevel.Error, $"Error downloading file {filePath} from {owner}/{repoName}", ex);
                progress?.Invoke(100);
            }
        }

        #endregion

        #region Create Pull Request

        public async Task CreatePullRequestAsync(string originalOwner, string repoName, string head, string baseBranch, string title, string body, Action<double>? progress = null)
        {
            if (!Program.IsNetworkAvailable) return;

            try
            {
                progress?.Invoke(0);
                var newPR = new NewPullRequest(title, head, baseBranch) { Body = body };
                var pr = await _client.PullRequest.Create(originalOwner, repoName, newPR).ConfigureAwait(false);
                Program.Log?.Write(LogEventLevel.Information, $"Pull request created: {pr.HtmlUrl}");
                progress?.Invoke(100);
            }
            catch (Exception ex)
            {
                Program.Log?.Write(LogEventLevel.Error, $"Error creating pull request to {originalOwner}/{repoName}", ex);
                progress?.Invoke(100);
            }
        }

        #endregion
    }
}