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
        ///// <summary>
        ///// Uploads a single file to the GitHub repository at the specified path.
        ///// </summary>
        ///// <param name="githubPath">The target path inside the GitHub repository (normalized automatically).</param>
        ///// <param name="localFilePath">The absolute path to the local file to upload.</param>
        ///// <param name="commitMessage">Optional commit message. If null, a default message is generated.</param>
        ///// <param name="ct">Cancellation token used to cancel the upload process.</param>
        ///// <param name="reportProgress">Optional progress callback receiving values from 0–100.</param>
        ///// <returns>A task representing the asynchronous upload operation.</returns>
        ///// <remarks>
        ///// The method streams the file, converts it to Base64, creates a Git blob,
        ///// attaches it to a new tree, commits the changes, updates the branch reference,
        ///// and refreshes the internal cache and directory mapping.
        ///// </remarks>
        //public static async Task UploadFileAsync(string githubPath, string localFilePath, string commitMessage = null, CancellationToken ct = default, Action<int> reportProgress = null)
        //{
        //    commitMessage ??= $"Uploaded `{localFilePath}` into `{githubPath}` by {_owner}";
        //    reportProgress?.Invoke(0);

        //    githubPath = NormalizePath(githubPath);

        //    var client = Program.GitHub.Client;

        //    using var fs = new FileStream(localFilePath, System.IO.FileMode.Open, FileAccess.Read, FileShare.Read, 81920, true);
        //    using var ms = new MemoryStream();

        //    long total = fs.Length;
        //    long read = 0;
        //    byte[] buffer = new byte[81920];
        //    int bytesRead;

        //    while ((bytesRead = await fs.ReadAsync(buffer, 0, buffer.Length, ct)) > 0)
        //    {
        //        if (ct.IsCancellationRequested) return;
        //        await ms.WriteAsync(buffer, 0, bytesRead, ct);
        //        read += bytesRead;
        //        reportProgress?.Invoke((int)(read * 100L / total));
        //    }

        //    string base64Content = Convert.ToBase64String(ms.ToArray());

        //    var blob = new Octokit.NewBlob { Content = base64Content, Encoding = Octokit.EncodingType.Base64 };
        //    var blobRef = await client.Git.Blob.Create(_owner, GitHub.Repository.repositoryName, blob);

        //    var masterRef = await client.Git.Reference.Get(_owner, GitHub.Repository.repositoryName, $"heads/{GitHub.Repository.Branch.Name}");
        //    var latestCommit = await client.Git.Commit.Get(_owner, GitHub.Repository.repositoryName, masterRef.Object.Sha);
        //    var baseTree = await client.Git.Tree.Get(_owner, GitHub.Repository.repositoryName, latestCommit.Tree.Sha);

        //    var newTree = new Octokit.NewTree { BaseTree = baseTree.Sha };
        //    newTree.Tree.Add(new Octokit.NewTreeItem
        //    {
        //        Path = githubPath,
        //        Mode = "100644",
        //        Type = Octokit.TreeType.Blob,
        //        Sha = blobRef.Sha
        //    });

        //    var createdTree = await client.Git.Tree.Create(_owner, GitHub.Repository.repositoryName, newTree);
        //    var newCommit = new Octokit.NewCommit(commitMessage, createdTree.Sha, latestCommit.Sha);
        //    var commit = await client.Git.Commit.Create(_owner, GitHub.Repository.repositoryName, newCommit);
        //    await client.Git.Reference.Update(_owner, GitHub.Repository.repositoryName, $"heads/{GitHub.Repository.Branch.Name}", new Octokit.ReferenceUpdate(commit.Sha));

        //    // Update cache and maps using helper
        //    var content = await client.Repository.Content.GetAllContentsByRef(_owner, GitHub.Repository.repositoryName, githubPath, GitHub.Repository.Branch.Name);
        //    var entry = new Entry { Path = githubPath, Type = EntryType.File, Content = content[0], FetchedAt = DateTime.UtcNow };

        //    Cache.Add(githubPath, entry);
        //    string directory = Path.GetDirectoryName(githubPath)?.Replace('\\', '/') ?? string.Empty;
        //    Cache.Add(directory, entry);

        //    reportProgress?.Invoke(100);
        //}

        ///// <summary>
        ///// Uploads an entire local directory (recursive) into the GitHub repository.
        ///// </summary>
        ///// <param name="localPath">The local directory to upload.</param>
        ///// <param name="githubPath">The target directory path inside the repository.</param>
        ///// <param name="cts">Optional cancellation token source.</param>
        ///// <param name="reportProgress">Callback invoked with total progress (0–100).</param>
        ///// <returns>A task representing the directory upload process.</returns>
        ///// <remarks>
        ///// This method enumerates all files recursively, converts each file to a Git blob,
        ///// builds a new Git tree, commits the entire structure, updates the branch reference,
        ///// and updates the internal cache and directory maps for each uploaded file.
        ///// </remarks>
        //public static async Task UploadDirectoryAsync(string localPath, string githubPath, CancellationTokenSource cts = null, Action<int> reportProgress = null)
        //{
        //    cts ??= new();
        //    reportProgress?.Invoke(0);

        //    githubPath = NormalizePath(githubPath);
        //    var client = Program.GitHub.Client;

        //    string[] files = Directory.GetFiles(localPath, "*", SearchOption.AllDirectories);
        //    if (files.Length == 0)
        //    {
        //        reportProgress?.Invoke(100);
        //        return;
        //    }

        //    var masterRef = await client.Git.Reference.Get(_owner, GitHub.Repository.repositoryName, $"heads/{GitHub.Repository.Branch.Name}");
        //    var latestCommit = await client.Git.Commit.Get(_owner, GitHub.Repository.repositoryName, masterRef.Object.Sha);
        //    var baseTree = await client.Git.Tree.Get(_owner, GitHub.Repository.repositoryName, latestCommit.Tree.Sha);
        //    var newTree = new Octokit.NewTree { BaseTree = baseTree.Sha };

        //    int totalFiles = files.Length;
        //    int processedFiles = 0;

        //    foreach (string file in files)
        //    {
        //        if (cts is not null && cts.Token.IsCancellationRequested) return;

        //        string relative = NormalizePath(file.Substring(localPath.Length));
        //        string targetPath = $"{githubPath}/{relative}";

        //        byte[] buffer;
        //        using (var fs = new FileStream(file, System.IO.FileMode.Open, FileAccess.Read, FileShare.Read, 81920, true))
        //        using (var ms = new MemoryStream())
        //        {
        //            int bytesRead;
        //            while ((bytesRead = await fs.ReadAsync(buffer = new byte[81920], 0, buffer.Length, cts.Token)) > 0)
        //            {
        //                if (cts is not null && cts.Token.IsCancellationRequested) return;
        //                await ms.WriteAsync(buffer, 0, bytesRead, cts.Token);
        //            }
        //            buffer = ms.ToArray();
        //        }

        //        string base64 = Convert.ToBase64String(buffer);
        //        var blob = new Octokit.NewBlob { Content = base64, Encoding = Octokit.EncodingType.Base64 };
        //        var blobRef = await client.Git.Blob.Create(_owner, GitHub.Repository.repositoryName, blob);

        //        newTree.Tree.Add(new Octokit.NewTreeItem
        //        {
        //            Path = targetPath,
        //            Mode = "100644",
        //            Type = Octokit.TreeType.Blob,
        //            Sha = blobRef.Sha
        //        });

        //        // Update cache and maps
        //        var content = await client.Repository.Content.GetAllContentsByRef(_owner, GitHub.Repository.repositoryName, targetPath, GitHub.Repository.Branch.Name);
        //        var entry = new Entry { Path = targetPath, Type = EntryType.File, Content = content[0], FetchedAt = DateTime.UtcNow };

        //        Cache.Add(targetPath, entry);

        //        string directory = ParentDirectoryName(targetPath);

        //        Cache.Add(directory, entry);

        //        processedFiles++;
        //        reportProgress?.Invoke((int)((long)processedFiles * 100 / totalFiles));
        //    }

        //    var createdTree = await client.Git.Tree.Create(_owner, GitHub.Repository.repositoryName, newTree);
        //    var newCommit = new Octokit.NewCommit($"Upload directory `{githubPath}` by {_owner}", createdTree.Sha, latestCommit.Sha);
        //    var commit = await client.Git.Commit.Create(_owner, GitHub.Repository.repositoryName, newCommit);
        //    await client.Git.Reference.Update(_owner, GitHub.Repository.repositoryName, $"heads/{GitHub.Repository.Branch.Name}", new Octokit.ReferenceUpdate(commit.Sha));

        //    reportProgress?.Invoke(100);
        //}
    }
}