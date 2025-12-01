using Octokit;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinPaletter.GitHub.IO
{
    /// <summary>
    /// Combines GitHub repository exploration and GitHub.IO.Path operations.
    /// Provides async methods to explore, read, download, upload, and manage files/directories in GitHub.
    /// </summary>
    public static class Path
    {
        public static IReadOnlyList<RepositoryContent> CachedEntries { get; private set; }

        private static readonly Dictionary<string, List<RepositoryContent>> DirectoryMap = new(StringComparer.OrdinalIgnoreCase);
        private static readonly Dictionary<string, long> FolderSizeMap = new(StringComparer.OrdinalIgnoreCase);

        private static string _owner => User.GitHub.Login;
        private const string _repo = "WinPaletter-Store";
        private const string _root = "Themes";

        private static ImageList _smallIcons;
        private static ImageList _largeIcons;
        private static ImageList _treeIcons;

        private static Stack<string> backStack = new();
        private static Stack<string> forwardStack = new();
        private static string currentPath = Path._root; // initial root path

        public static bool CanGoBack => backStack.Count > 0;
        public static bool CanGoForward => forwardStack.Count > 0;
        public static bool CanGoUp => !string.IsNullOrEmpty(currentPath) && currentPath != _root;

        private static Func<RepositoryContent, string> FileTypeProvider { get; set; } = entry =>
        {
            if (entry.Type == ContentType.Dir) return Program.Lang.Strings.GitHubStrings.Explorer_Type_Folder;
            if (entry.Name.EndsWith(".wpth", StringComparison.OrdinalIgnoreCase)) return Program.Lang.Strings.Extensions.WinPaletterTheme;
            if (entry.Name.EndsWith(".wptp", StringComparison.OrdinalIgnoreCase)) return Program.Lang.Strings.Extensions.WinPaletterResourcesPack;
            return NativeMethods.Shlwapi.GetFriendlyTypeName(entry.Name);
        };

        public static async Task PopulateRepositoryAsync(UI.WP.TreeView tree, UI.WP.ListView list, UI.WP.Breadcrumb breadCrumb, CancellationTokenSource cts = null, Action<int> reportProgress = null)
        {
            cts ??= new();

            ResetTree(tree, list);
            InitializeImageLists(list, tree);

            breadCrumb?.StartMarquee();

            List<RepositoryContent> entries = [];

            try
            {
                breadCrumb?.StartMarquee();
                breadCrumb.Value = breadCrumb.Minimum;

                await FetchRecursive(_root, entries, null, cts);

                CachedEntries = entries;

                BuildDirectoryMap();
                BuildFolderSizes(_root);

                void UpdateUI()
                {
                    BuildTree(tree);
                    tree.Nodes[0].Expand();
                    _ = PopulateListViewAsync(list, _root, cts);
                    HookEvents(tree, list);
                    breadCrumb?.FinishLoadingAnimation();
                    breadCrumb.BoundTreeView = tree;
                }

                if (tree.InvokeRequired) tree.Invoke((MethodInvoker)UpdateUI);
                else UpdateUI();
            }
            catch (OperationCanceledException) 
            {
                breadCrumb?.FinishLoadingAnimation();
                breadCrumb.Value = breadCrumb.Minimum;
            }
        }

        static void ResetTree(UI.WP.TreeView tree, UI.WP.ListView list)
        {
            tree.Nodes.Clear();
            tree.SelectedNode = null;

            list.Items.Clear();
        }

        private static async Task FetchRecursive(string path, List<RepositoryContent> output, Action<RepositoryContent> reportProgress = null, CancellationTokenSource cts = null)
        {
            cts ??= new();
            cts.Token.ThrowIfCancellationRequested();

        retry:
            IReadOnlyList<RepositoryContent> items;
            try
            {
                items = await Program.GitHub.Client.Repository.Content.GetAllContents(_owner, _repo, path);
            }
            catch (RateLimitExceededException)
            {
                await Task.Delay(1500, cts.Token);
                goto retry;
            }
            catch (NotFoundException)
            {
                return;
            }

            foreach (RepositoryContent entry in items)
            {
                output.Add(entry);
                reportProgress?.Invoke(entry);
            }

            foreach (RepositoryContent dir in items.Where(i => i.Type == ContentType.Dir))
            {
                cts.Token.ThrowIfCancellationRequested();
                await FetchRecursive(dir.Path, output, reportProgress, cts);
            }
        }

        private static async Task NavigateTo(string newPath, UI.WP.ListView list, UI.WP.TreeView tree, bool updateStacks = true)
        {
            if (string.IsNullOrEmpty(newPath) || newPath == currentPath) return;

            if (updateStacks && !string.IsNullOrEmpty(currentPath))
            {
                backStack.Push(currentPath);
                forwardStack.Clear();
            }

            currentPath = newPath;
            await PopulateListViewAsync(list, currentPath);
            SelectTreeNode(list, tree, currentPath);
        }

        private static void SelectTreeNode(UI.WP.ListView list, UI.WP.TreeView tree, string path)
        {
            if (tree.Nodes.Count == 0) return;

            string[] segments = path.Split('/');
            TreeNode currentNode = tree.Nodes[0];

            // Ensure root node matches
            if (!string.Equals(currentNode.Tag as string, segments[0], StringComparison.OrdinalIgnoreCase)) return;

            // Walk segments and create missing nodes
            string fullPath = segments[0];
            for (int i = 1; i < segments.Length; i++)
            {
                fullPath += "/" + segments[i];
                TreeNode childNode = currentNode.Nodes.Cast<TreeNode>().FirstOrDefault(n => string.Equals(n.Tag as string, fullPath, StringComparison.OrdinalIgnoreCase));

                if (childNode == null)
                {
                    // Node doesn't exist yet, create it dynamically
                    childNode = new TreeNode(segments[i])
                    {
                        Tag = fullPath,
                        ImageKey = "folder",
                        SelectedImageKey = "folder"
                    };
                    currentNode.Nodes.Add(childNode);
                }

                currentNode.Expand();
                currentNode = childNode;
            }

            // Select the final node
            tree.SelectedNode = currentNode;
            currentNode.EnsureVisible();
        }

        public static async Task GoBack(UI.WP.TreeView tree, UI.WP.ListView list)
        {
            if (backStack.Count == 0) return;

            forwardStack.Push(currentPath);
            string previousPath = backStack.Pop();
            await NavigateTo(previousPath, list, tree, updateStacks: false);
        }

        public static async Task GoForward(UI.WP.TreeView tree, UI.WP.ListView list)
        {
            if (forwardStack.Count == 0) return;

            backStack.Push(currentPath);
            string nextPath = forwardStack.Pop();
            await NavigateTo(nextPath, list, tree, updateStacks: false);
        }

        public static async Task GoUp(UI.WP.TreeView tree, UI.WP.ListView list)
        {
            if (string.IsNullOrEmpty(currentPath) || currentPath == _root) return;

            string parent = GetParent(currentPath);
            if (!string.IsNullOrEmpty(parent))
            {
                forwardStack.Clear();
                await NavigateTo(parent, list, tree);
            }
        }

        public static async Task RefreshAsync(UI.WP.TreeView tree, UI.WP.ListView list, UI.WP.Breadcrumb breadCrumb, CancellationTokenSource cts = null)
        {
            cts ??= new();

            // Remember current path
            string pathBeforeRefresh = currentPath;

            // Clear cached entries
            CachedEntries = null;
            DirectoryMap.Clear();
            FolderSizeMap.Clear();

            // Optionally reset UI elements
            breadCrumb?.StartMarquee();

            try
            {
                // Re-fetch all repository entries
                List<RepositoryContent> entries = [];
                await FetchRecursive(_root, entries, null, cts);
                CachedEntries = entries;

                // Rebuild directory map and folder sizes
                BuildDirectoryMap();
                BuildFolderSizes(_root);

                // Rebuild tree
                if (tree.InvokeRequired) tree.Invoke(new MethodInvoker(() => BuildTree(tree)));
                else BuildTree(tree);

                // Navigate back to current path
                await NavigateTo(pathBeforeRefresh, list, tree, updateStacks: false);
            }
            finally
            {
                breadCrumb?.StopMarquee();
                breadCrumb?.FinishLoadingAnimation();
                breadCrumb.BoundTreeView = tree;
                HookEvents(tree, list);
                _ = PopulateListViewAsync(list, pathBeforeRefresh, cts);
                if (tree.Nodes[0] is not null) tree.SelectedNode = tree.Nodes[0];
                else
                {
                    tree.Nodes.Clear();
                    list.Items.Clear();
                }
            }
        }

        private static void BuildDirectoryMap()
        {
            DirectoryMap.Clear();
            foreach (RepositoryContent entry in CachedEntries)
            {
                string parent = GetParent(entry.Path);
                if (!DirectoryMap.ContainsKey(parent)) DirectoryMap[parent] = [];
                DirectoryMap[parent].Add(entry);
            }
        }

        private static string GetParent(string path)
        {
            int i = path.LastIndexOf('/');
            return i < 0 ? string.Empty : path.Substring(0, i);
        }

        private static long BuildFolderSizes(string path)
        {
            if (!DirectoryMap.ContainsKey(path)) return 0;
            long total = 0;
            foreach (RepositoryContent entry in DirectoryMap[path])
            {
                if (entry.Type == ContentType.Dir) total += BuildFolderSizes(entry.Path);
                else total += entry.Size;
            }
            FolderSizeMap[path] = total;
            return total;
        }

        private static void InitializeImageLists(UI.WP.ListView list, UI.WP.TreeView tree)
        {
            _smallIcons = new ImageList { ImageSize = new Size(16, 16), ColorDepth = ColorDepth.Depth32Bit };
            _largeIcons = new ImageList { ImageSize = new Size(48, 48), ColorDepth = ColorDepth.Depth32Bit };
            _treeIcons = new ImageList { ImageSize = new Size(16, 16), ColorDepth = ColorDepth.Depth32Bit };

            AddIcons(_smallIcons, _smallIcons.ImageSize.Width);
            AddIcons(_largeIcons, _largeIcons.ImageSize.Width);
            AddIcons(_treeIcons, _treeIcons.ImageSize.Width);

            list.SmallImageList = _smallIcons;
            list.LargeImageList = _largeIcons;
            tree.ImageList = _treeIcons;
        }

        private static void AddIcons(ImageList imgList, int size)
        {
            using (Icon ico = Properties.Resources.Folder.FromSize(size)) imgList.Images.Add("folder", ico.ToBitmap());
            using (Icon ico = Properties.Resources.FileIcon.FromSize(size)) imgList.Images.Add("file", ico.ToBitmap());
            using (Icon ico = Properties.Resources.fileextension.FromSize(size)) imgList.Images.Add("wpth", ico.ToBitmap());
            using (Icon ico = Properties.Resources.ThemesResIcon.FromSize(size)) imgList.Images.Add("wptp", ico.ToBitmap());
        }

        private static void BuildTree(UI.WP.TreeView tree)
        {
            tree.BeginUpdate();
            tree.Nodes.Clear();

            TreeNode root = new(_root)
            {
                Tag = _root,
                ImageKey = "folder",
                SelectedImageKey = "folder"
            };
            tree.Nodes.Add(root);

            AddChildren(root, _root);
            tree.EndUpdate();
        }

        private static void AddChildren(TreeNode parentNode, string parentPath)
        {
            if (!DirectoryMap.ContainsKey(parentPath)) return;

            foreach (RepositoryContent dir in DirectoryMap[parentPath].Where(d => d.Type == ContentType.Dir).OrderBy(d => d.Name))
            {
                TreeNode node = new(dir.Name)
                {
                    Tag = dir.Path,
                    ImageKey = "folder",
                    SelectedImageKey = "folder"
                };
                parentNode.Nodes.Add(node);
                AddChildren(node, dir.Path);
            }
        }

        public static async Task PopulateListViewAsync(UI.WP.ListView list, string path, CancellationTokenSource cts = null)
        {
            cts ??= new();

            if (list.InvokeRequired)
            {
                list.Invoke(new Func<Task>(() => PopulateListViewAsync(list, path, cts)));
                return;
            }

            try
            {
                list.Cursor = Cursors.WaitCursor;
                list.BeginUpdate();

                // Initialize columns once
                if (list.Columns.Count == 0)
                {
                    list.Columns.Add(Program.Lang.Strings.GitHubStrings.Explorer_DetailsHeader_Name, 230);
                    list.Columns.Add(Program.Lang.Strings.GitHubStrings.Explorer_DetailsHeader_Type, 200);
                    list.Columns.Add(Program.Lang.Strings.GitHubStrings.Explorer_DetailsHeader_Size, 80);
                    list.Columns.Add("MD5", 120);
                    list.Columns.Add(Program.Lang.Strings.GitHubStrings.Explorer_DetailsHeader_APIURL, 120);
                }

                if (!DirectoryMap.ContainsKey(path))
                {
                    list.Items.Clear();
                    return;
                }

                // Sort: directories first, then files, alphabetically
                List<RepositoryContent> entries = [.. DirectoryMap[path].OrderBy(e => e.Type == ContentType.Dir ? 0 : 1).ThenBy(e => e.Name)];

                list.Items.Clear();

                int count = 0;
                foreach (RepositoryContent entry in entries)
                {
                    cts.Token.ThrowIfCancellationRequested();

                    ListViewItem item = new(entry.Name) { Tag = entry };

                    // Subitems for Details view
                    item.SubItems.Add(FileTypeProvider?.Invoke(entry) ?? Program.Lang.Strings.Extensions.File);

                    long size = entry.Type == ContentType.Dir && FolderSizeMap.ContainsKey(entry.Path) ? FolderSizeMap[entry.Path] : entry.Size;

                    item.SubItems.Add(size.ToStringFileSize());
                    item.SubItems.Add(ShaToMd5(entry.Sha).ToUpper());
                    item.SubItems.Add(entry.Url);

                    // Image assignment
                    if (entry.Type == ContentType.Dir) item.ImageKey = "folder";
                    else if (entry.Name.EndsWith(".wpth", StringComparison.OrdinalIgnoreCase)) item.ImageKey = "wpth";
                    else if (entry.Name.EndsWith(".wptp", StringComparison.OrdinalIgnoreCase)) item.ImageKey = "wptp";
                    else item.ImageKey = "file";

                    list.Items.Add(item);

                    // Yield occasionally to avoid UI freezing
                    count++;
                    if (count % 50 == 0) await Task.Yield();
                }
            }
            finally
            {
                list.EndUpdate();
                list.Cursor = Cursors.Default;
            }
        }

        private static string ShaToMd5(string sha)
        {
            if (string.IsNullOrEmpty(sha)) return string.Empty;
            using MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.UTF8.GetBytes(sha);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            StringBuilder sb = new();
            foreach (byte b in hashBytes) sb.Append(b.ToString("x2"));
            return sb.ToString();
        }

        private static UI.WP.ListView _boundList;
        private static UI.WP.TreeView _boundTree;

        private static void HookEvents(UI.WP.TreeView tree, UI.WP.ListView list)
        {
            // Remove previous handlers
            tree.AfterSelect -= Tree_AfterSelect;
            list.DoubleClick -= List_DoubleClick;

            // Store references
            _boundList = list;
            _boundTree = tree;

            // Add handlers
            tree.AfterSelect += Tree_AfterSelect;
            list.DoubleClick += List_DoubleClick;
        }

        private static async void Tree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node == null || _boundList == null) return;

            string newPath = e.Node.Tag as string;
            if (string.IsNullOrEmpty(newPath) || newPath == currentPath) return;

            backStack.Push(currentPath);
            forwardStack.Clear();
            currentPath = newPath;

            await PopulateListViewAsync(_boundList, newPath);
        }

        private static async void List_DoubleClick(object sender, EventArgs e)
        {
            if (_boundList == null || _boundTree == null || _boundList.SelectedItems.Count == 0) return;

            if (_boundList.SelectedItems[0].Tag is not RepositoryContent entry) return;
            if (entry.Type != ContentType.Dir) return;

            await NavigateTo(entry.Path, _boundList, _boundTree);
        }

        private static TreeNode FindNode(TreeNode node, string path)
        {
            if ((node.Tag as string) == path) return node;
            foreach (TreeNode child in node.Nodes)
            {
                TreeNode match = FindNode(child, path);
                if (match != null) return match;
            }
            return null;
        }

        public static async Task<RepositoryContent> GetFileInfoAsync(string path)
        {
            try
            {
                IEnumerable<RepositoryContent> contents = await Program.GitHub.Client.Repository.Content.GetAllContents(_owner, _repo, path).ConfigureAwait(false);
                return contents.FirstOrDefault();
            }
            catch { return null; }
        }

        public static async Task<IReadOnlyList<RepositoryContent>> GetDirectoryInfoAsync(string path)
        {
            try
            {
                return await Program.GitHub.Client.Repository.Content.GetAllContents(_owner, _repo, path).ConfigureAwait(false);
            }
            catch { return []; }
        }

        public static async Task<List<string>> GetFilesAsync(string path, CancellationTokenSource cts = null, Action<int> reportProgress = null)
        {
            cts ??= new();
            List<string> result = [];
            await FetchRecursive(path, result, cts, false, reportProgress);
            return result;
        }

        public static async Task<List<string>> GetDirectoriesAsync(string path, CancellationTokenSource cts = null, Action<int> reportProgress = null)
        {
            cts ??= new();
            List<string> result = [];
            await FetchRecursive(path, result, cts, true, reportProgress);
            return result;
        }

        private static async Task FetchRecursive(string path, List<string> output, CancellationTokenSource cts, bool onlyDirs, Action<int> reportProgress = null)
        {
            IReadOnlyList<RepositoryContent> items;
            try
            {
                items = await Program.GitHub.Client.Repository.Content.GetAllContents(_owner, _repo, path).ConfigureAwait(false);
            }
            catch { return; }

            int total = items.Count;
            int processed = 0;

            try
            {
                foreach (RepositoryContent entry in items)
                {
                    cts.Token.ThrowIfCancellationRequested();

                    if ((onlyDirs && entry.Type == ContentType.Dir) || (!onlyDirs && entry.Type != ContentType.Dir)) output.Add(entry.Path);

                    if (entry.Type == ContentType.Dir) await FetchRecursive(entry.Path, output, cts, onlyDirs, reportProgress);

                    processed++;
                    reportProgress?.Invoke((int)((processed * 100L) / total));
                }
            }
            catch (OperationCanceledException) { /* ignore if canceled */ }
        }

        public static async Task<string> ReadFileAsync(string path)
        {
            RepositoryContent file = await GetFileInfoAsync(path);
            if (file == null || file.Type != ContentType.File) return null;

            byte[] contentBytes = Convert.FromBase64String(file.Content);
            return Encoding.UTF8.GetString(contentBytes);
        }

        public static async Task UploadFileAsync(string githubPath, string localFilePathOrContent, bool isLocalFile = true, string commitMessage = "Upload via GitHub.IO.Path", CancellationTokenSource cts = null, Action<int> reportProgress = null)
        {
            cts ??= new();
            reportProgress?.Invoke(0);

            try
            {
                // Read content (raw string or file)
                string base64;

                if (isLocalFile)
                {
                    using (FileStream fs = new(localFilePathOrContent, System.IO.FileMode.Open, FileAccess.Read, FileShare.Read, 4096, true))
                    {
                        byte[] buff = new byte[fs.Length];
                        int total = buff.Length;
                        int read = 0;

                        // Progressive file read
                        while (read < total)
                        {
                            cts.Token.ThrowIfCancellationRequested();

                            int chunk = await fs.ReadAsync(buff, read, total - read, cts.Token).ConfigureAwait(false);

                            if (chunk == 0) break;
                            read += chunk;

                            reportProgress?.Invoke((int)(read * 100L / total));
                        }

                        base64 = Convert.ToBase64String(buff);
                    }
                }
                else
                {
                    base64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(localFilePathOrContent));
                }

                IRepositoryContentsClient client = Program.GitHub.Client.Repository.Content;
                RepositoryContent existing = await GetFileInfoAsync(githubPath);

                if (existing == null)
                {
                    await client.CreateFile(_owner, _repo, githubPath, new(commitMessage, base64));
                }
                else
                {
                    await client.UpdateFile(_owner, _repo, githubPath, new(commitMessage, base64, existing.Sha));
                }

                reportProgress?.Invoke(100);
            }
            catch
            {
                reportProgress?.Invoke(0);
            }
        }

        public static async Task DownloadFileAsync(string githubPath, string localSavePath, CancellationTokenSource cts = null, Action<int> reportProgress = null)
        {
            cts ??= new();
            reportProgress?.Invoke(0);

            try
            {
                RepositoryContent file = await GetFileInfoAsync(githubPath);
                if (file == null || file.Type != ContentType.File) return;

                if (!string.IsNullOrEmpty(file.DownloadUrl))
                {
                    // Use DownloadManager (progress built-in)
                    using DownloadManager dm = new();
                    dm.DownloadProgressChanged += (s, e) => reportProgress?.Invoke((int)e.ProgressPercentage);
                    dm.DownloadFileCompleted += (s, e) => reportProgress?.Invoke(100);
                    dm.DownloadErrorOccurred += (s, e) => reportProgress?.Invoke(0);

                    await dm.DownloadFileAsync(file.DownloadUrl, localSavePath, cts);
                    return;
                }

                // Fallback: base64 decode manually
                byte[] bytes = Convert.FromBase64String(file.Content);

                using (FileStream fs = new(localSavePath, System.IO.FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    int total = bytes.Length;
                    int written = 0;
                    int chunkSize = 4096;

                    while (written < total)
                    {
                        cts.Token.ThrowIfCancellationRequested();

                        int size = Math.Min(chunkSize, total - written);
                        await fs.WriteAsync(bytes, written, size, cts.Token).ConfigureAwait(false);
                        written += size;

                        reportProgress?.Invoke((int)(written * 100L / total));
                    }
                }

                reportProgress?.Invoke(100);
            }
            catch
            {
                reportProgress?.Invoke(0);
            }
        }

        public static async Task<bool> FileExistsAsync(string path, CancellationTokenSource cts = null)
        {
            RepositoryContent file = await GetFileInfoAsync(path);
            return file != null && file.Type == ContentType.File;
        }

        public static async Task<bool> DirectoryExistsAsync(string path, CancellationTokenSource cts = null)
        {
            IReadOnlyList<RepositoryContent> dir = await GetDirectoryInfoAsync(path);
            return dir != null && dir.Count > 0;
        }

        public static async Task DeleteFileAsync(string path, CancellationTokenSource cts = null, Action<int> reportProgress = null)
        {
            RepositoryContent file = await GetFileInfoAsync(path);
            if (file != null)
            {
                await Program.GitHub.Client.Repository.Content.DeleteFile(_owner, _repo, path, new($"{_owner} deleted `{path}`", file.Sha));
                reportProgress?.Invoke(100);
            }
        }

        public static async Task DeleteDirectoryAsync(string path, CancellationTokenSource cts = null, Action<int> reportProgress = null)
        {
            IReadOnlyList<RepositoryContent> items = await GetDirectoryInfoAsync(path);
            int total = items.Count;
            int processed = 0;

            try
            {
                foreach (RepositoryContent entry in items)
                {
                    cts.Token.ThrowIfCancellationRequested();

                    if (entry.Type == ContentType.Dir) await DeleteDirectoryAsync(entry.Path, cts, reportProgress);
                    else await DeleteFileAsync(entry.Path, cts, reportProgress);

                    processed++;
                    reportProgress?.Invoke((int)((processed * 100L) / total));
                }
            }
            catch (OperationCanceledException) { /* ignore if canceled */ }
        }
    }
}