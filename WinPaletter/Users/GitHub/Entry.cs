using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WinPaletter.GitHub
{
    public static partial class FileSystem
    {
        /// <summary>
        /// Represents an entry in a repository's file system, which can be a file, directory, symbolic link, or submodule.
        /// Provides metadata, hierarchical structure, and Git commit information.
        /// </summary>
        /// <remarks>
        /// This class is useful for modeling repository contents retrieved from GitHub via the Octokit library.
        /// <para>
        /// - For directories, the <see cref="Children"/> property contains the immediate child entries.
        /// - For files, the <see cref="Content"/> property contains detailed file information, such as size and download URL.
        /// - Metadata such as <see cref="CommitSha"/>, <see cref="Author"/>, and <see cref="LastModified"/> are derived from the latest commit affecting the entry.
        /// </para>
        /// </remarks>
        /// <example>
        /// <code>
        /// // Fetch a file entry from the repository
        /// var content = await Program.GitHub.Client.Repository.Content.GetAllContents(_owner, _repo, "path/to/file.txt");
        /// Entry entry = await Entry.FromRepositoryContent(content.First(), "path/to/file.txt");
        ///
        /// Console.WriteLine($&quot;File: {entry.Name}, Size: {entry.Size}, Author: {entry.Author}&quot;);
        /// </code>
        /// </example>
        /// <seealso cref="Octokit.RepositoryContent"/>
        /// <seealso cref="Octokit.GitHubCommit"/>
        public class Entry
        {
            /// <summary>
            /// Gets the name of the entry.
            /// </summary>
            /// <remarks>
            /// If the <see cref="Content"/> object exists, its name is returned; otherwise, the file name is derived from <see cref="Path"/>.
            /// </remarks>
            public string Name => Content?.Name ?? System.IO.Path.GetFileName(Path);

            /// <summary>
            /// Gets the size of this element.
            /// </summary>
            /// <remarks>
            /// If the element is a file, its size is returned.  
            /// If it is a folder, the method falls back to calculating the total size of all
            /// contained subfolders and files.
            /// </remarks>
            public long Size => Type == EntryType.File ? Content?.Size ?? GetFoldersSize(Path, false) : 0;

            /// <summary>
            /// Gets or sets the full path of the entry within the repository.
            /// </summary>
            public string Path { get; set; }

            /// <summary>
            /// Gets or sets the type of the entry (file, directory, symbolic link, or submodule).
            /// </summary>
            public EntryType Type { get; set; }

            /// <summary>
            /// Gets or sets the content details for file entries.
            /// </summary>
            /// <remarks>
            /// Only populated for files; null for directories, symbolic links, or submodules.
            /// Contains properties such as file size, download URL, and encoding.
            /// </remarks>
            /// <seealso cref="Octokit.RepositoryContent"/>
            public RepositoryContent Content { get; set; }

            /// <summary>
            /// Gets or sets the content information for this entry.
            /// </summary>
            public RepositoryContentInfo ContentInfo { get; set; }

            /// <summary>
            /// Gets or sets the SHA of the latest commit affecting this entry.
            /// </summary>
            /// <seealso cref="Octokit.GitHubCommit"/>
            public string CommitSha { get; set; }

            /// <summary>
            /// Gets or sets the author of the latest commit affecting this entry.
            /// </summary>
            public string Author { get; set; }

            /// <summary>
            /// Gets or sets the timestamp of the last modification (from the latest commit) affecting this entry.
            /// </summary>
            public DateTimeOffset? LastModified { get; set; }

            /// <summary>
            /// Gets or sets the child entries for directories.
            /// </summary>
            /// <remarks>
            /// Populated only for directory entries. Each child is an <see cref="Entry"/> representing files or subdirectories.
            /// </remarks>
            public IReadOnlyList<Entry> Children { get; set; }

            /// <summary>
            /// Gets or sets the SHA of the content itself.
            /// </summary>
            /// <remarks>
            /// Useful for caching purposes when <see cref="RepositoryContent"/> cannot be instantiated.
            /// </remarks>
            /// <seealso cref="Octokit.RepositoryContent.Sha"/>
            public string ContentSha { get; set; }

            /// <summary>
            /// Gets or sets the UTC timestamp when this entry was fetched from the repository.
            /// </summary>
            public DateTime FetchedAt { get; set; } = DateTime.UtcNow;

            /// <summary>
            /// Creates an <see cref="Entry"/> from a given <see cref="RepositoryContent"/> object and path.
            /// </summary>
            /// <param name="content">The Octokit <see cref="RepositoryContent"/> object representing the file or directory.</param>
            /// <param name="path">The path to the content within the repository.</param>
            /// <returns>
            /// A <see cref="Task{TResult}"/> representing the asynchronous operation.
            /// The task result contains the fully populated <see cref="Entry"/> object including commit metadata.
            /// </returns>
            /// <remarks>
            /// This method retrieves the latest commit for the given path to populate <see cref="CommitSha"/>, <see cref="Author"/>, 
            /// and <see cref="LastModified"/>. It also maps the Octokit <see cref="ContentType"/> to the <see cref="EntryType"/>.
            /// </remarks>
            /// <example>
            /// <code>
            /// // Convert RepositoryContent to Entry asynchronously
            /// var content = await Program.GitHub.Client.Repository.Content.GetAllContents(_owner, _repo, "folder/file.txt");
            /// Entry fileEntry = await Entry.FromRepositoryContent(content.First(), "folder/file.txt");
            /// Console.WriteLine($&quot;File Name: {fileEntry.Name}, Commit SHA: {fileEntry.CommitSha}&quot;);
            /// 
            /// // For directories, fetch children recursively
            /// var folderContent = await Program.GitHub.Client.Repository.Content.GetAllContents(_owner, _repo, "folder");
            /// Entry folderEntry = await Entry.FromRepositoryContent(folderContent.First(), "folder");
            /// if (folderEntry.Children != null)
            /// {
            ///     foreach (var child in folderEntry.Children)
            ///         Console.WriteLine($&quot;Child: {child.Name}, Type: {child.Type}&quot;);
            /// }
            /// </code>
            /// </example>
            /// <seealso cref="Octokit.RepositoryContent"/>
            /// <seealso cref="Octokit.GitHubCommit"/>
            public static async Task<Entry> FromRepositoryContent(RepositoryContent content, string path)
            {
                GitHubCommit latestCommit = (await Program.GitHub.Client.Repository.Commit
                    .GetAll(_owner, GitHub.Repository.repositoryName, new CommitRequest { Path = path }))
                    .FirstOrDefault();

                EntryType type = content.Type.StringValue.ToLowerInvariant() switch
                {
                    "file" => EntryType.File,
                    "dir" => EntryType.Dir,
                    "symlink" => EntryType.Symlink,
                    "submodule" => EntryType.Submodule,
                    _ => EntryType.Unknown
                };

                Entry entry = new()
                {
                    Path = content.Path,
                    Type = type,
                    Content = content.Type == Octokit.ContentType.File ? content : null,
                    ContentInfo = new RepositoryContentInfo(content.Name, content.Path, content.Sha, content.Size, content.Type.Value, content.DownloadUrl, content.Url, content.GitUrl, content.HtmlUrl),
                    CommitSha = latestCommit?.Sha,
                    Author = latestCommit?.Commit.Author?.Name,
                    LastModified = latestCommit?.Commit.Author?.Date,
                    ContentSha = content?.Sha ?? string.Empty,
                };

                return entry;
            }
        }

        /// <summary>
        /// Specifies the type of an element in a file system or repository, such as a file, directory, symbolic link,
        /// submodule, or an unknown type.
        /// </summary>
        /// <remarks>Use this enumeration to identify and handle different element types when working with
        /// file system or repository structures. The values correspond to common element categories encountered in
        /// version control systems and file management operations.</remarks>
        public enum EntryType
        {
            /// <summary>
            /// Type representing a file in the file system.
            /// </summary>
            File,

            /// <summary>
            /// Type representing a directory in the file system.
            /// </summary>
            Dir,

            /// <summary>
            /// Represents a symbolic link within the file system, allowing access to a target file or directory through
            /// an alternative path.
            /// </summary>
            /// <remarks>A symbolic link, or symlink, is a special type of file that points to another
            /// file or directory. Operations performed on the symlink are typically redirected to its target. Symlinks
            /// can be used to create shortcuts, organize files, or provide compatibility across different directory
            /// structures. The behavior of symlinks may vary depending on the underlying operating system and file
            /// system.</remarks>
            Symlink,

            /// <summary>
            /// Represents a distinct functional unit within a larger module or system.
            /// </summary>
            /// <remarks>A submodule typically encapsulates related functionality and may interact
            /// with other submodules or the parent module. Use this class to organize code into manageable, reusable
            /// components within a modular architecture.</remarks>
            Submodule,

            /// <summary>
            /// Represents an unknown or unspecified value.
            /// </summary>
            Unknown
        }
    }
}
