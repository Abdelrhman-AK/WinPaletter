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
        /// Represents an entry in a file system, such as a file, directory, symbolic link, or submodule, including
        /// metadata and hierarchical relationships.
        /// </summary>
        /// <remarks>A Entry provides access to metadata such as name, size, type, path, commit
        /// information, and author. For directory entries, the Children property contains the immediate child entries.
        /// For file entries, the Content property provides additional file-specific details. This type is commonly used
        /// to model file system structures retrieved from a repository, such as those from GitHub.</remarks>
        public class Entry
        {
            public string Name => Content?.Name ?? System.IO.Path.GetFileName(Path);
            public long Size => Content?.Size ?? 0;
            public string Path { get; set; }
            public ElementType Type { get; set; } // File, Dir, Symlink, Submodule
            public RepositoryContent Content { get; set; } // Only for files
            public string CommitSha { get; set; }
            public string Author { get; set; }
            public DateTimeOffset? LastModified { get; set; }
            public IReadOnlyList<Entry> Children { get; set; } // Only for directories

            public static async Task<Entry> FromRepositoryContent(RepositoryContent content, string path)
            {
                GitHubCommit latestCommit = (await Program.GitHub.Client.Repository.Commit.GetAll(_owner, _repo, new CommitRequest { Path = path })).FirstOrDefault();

                ElementType type = content.Type.StringValue.ToLowerInvariant() switch
                {
                    "file" => ElementType.File,
                    "dir" => ElementType.Dir,
                    "symlink" => ElementType.Symlink,
                    "submodule" => ElementType.Submodule,
                    _ => ElementType.Unknown
                };

                Entry entry = new()
                {
                    Path = content.Path,
                    Type = type,
                    Content = content.Type == Octokit.ContentType.File ? content : null,
                    CommitSha = latestCommit?.Sha,
                    Author = latestCommit?.Commit.Author?.Name,
                    LastModified = latestCommit?.Commit.Author?.Date
                };

                return entry;
            }
        }
    }
}