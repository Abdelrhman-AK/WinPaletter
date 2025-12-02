using Octokit;
using Serilog.Events;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinPaletter.GitHub
{
    /// <summary>
    /// Provides static methods and properties for navigating, displaying, and managing the contents of a GitHub
    /// repository in a tree and list view interface. Supports repository traversal, caching, and UI integration for
    /// efficient browsing and manipulation of repository files and folders.
    /// </summary>
    /// <remarks>The FileSystem class is designed for use in applications that present a hierarchical view of a
    /// GitHub repository, such as file explorers or resource managers. It maintains internal caches to optimize
    /// repeated access to repository data and provides navigation methods (back, forward, up) for user-friendly
    /// traversal. Thread safety is ensured for concurrent operations involving repository data. Most members are
    /// intended to be used in conjunction with UI controls such as TreeView, ListView, and Breadcrumb, and require
    /// proper initialization and event handling for correct operation.</remarks>
    public static partial class FileSystem
    {
        public static IReadOnlyList<RepositoryContent> CachedEntries { get; private set; }
        private static readonly Dictionary<string, List<RepositoryContent>> DirectoryMap = new(StringComparer.OrdinalIgnoreCase);
        private static readonly Dictionary<string, long> FolderSizeMap = new(StringComparer.OrdinalIgnoreCase);
        private static readonly ConcurrentDictionary<string, IReadOnlyList<RepositoryContent>> DirectoryContentCache = new();
        private static readonly Dictionary<string, string> DirectoryShaCache = [];
        private static readonly SemaphoreSlim _semaphore = new(5);
        private static readonly ConcurrentDictionary<string, Entry> _fileCache = new();
        private static readonly ConcurrentDictionary<string, Entry> _dirCache = new();
        private static readonly ConcurrentDictionary<string, string> _dirShaCache = new();
        private static readonly ConcurrentDictionary<string, (Entry entry, DateTime fetched)> _infoCache = new();
        private static readonly ConcurrentDictionary<string, (List<Entry> entries, DateTime fetched)> _dirsCache = new();
        private static readonly TimeSpan CacheTTL = TimeSpan.FromSeconds(30); // short TTL for update checks

        private static UI.WP.ListView _boundList;
        private static UI.WP.TreeView _boundTree;

        private static string _owner => User.GitHub.Login;
        private const string _repo = "WinPaletter-Store";
        private const string _branch = "main";
        private const string _root = "Themes";

        private static ImageList _smallIcons;
        private static ImageList _largeIcons;
        private static ImageList _treeIcons;

        private static Stack<string> backStack = new();
        private static Stack<string> forwardStack = new();
        private static string currentPath = _root;

        private static readonly ConcurrentDictionary<string, string> ShaMd5Cache = new();
        public static bool CanGoBack => backStack.Count > 0;
        public static bool CanGoForward => forwardStack.Count > 0;
        public static bool CanGoUp => !string.IsNullOrEmpty(currentPath) && currentPath != _root;
        public enum ElementType { File, Dir, Symlink, Submodule, Unknown }

        static void ResetTree(UI.WP.TreeView tree, UI.WP.ListView list)
        {
            tree.Nodes.Clear();
            tree.SelectedNode = null;

            list.Items.Clear();
        }

        private static void SelectTreeNode(UI.WP.ListView list, UI.WP.TreeView tree, string path)
        {
            Program.Log?.Write(LogEventLevel.Information, $"SelectTreeNode called for path='{path}'");

            if (tree.Nodes.Count == 0)
            {
                Program.Log?.Write(LogEventLevel.Warning, "SelectTreeNode aborted: tree has no nodes");
                return;
            }

            string[] segments = path.Split('/');
            TreeNode currentNode = tree.Nodes[0];

            if (!string.Equals(currentNode.Tag as string, segments[0], StringComparison.OrdinalIgnoreCase))
            {
                Program.Log?.Write(LogEventLevel.Warning, $"SelectTreeNode: root mismatch (expected '{segments[0]}')");
                return;
            }

            string fullPath = segments[0];

            for (int i = 1; i < segments.Length; i++)
            {
                fullPath += "/" + segments[i];

                TreeNode childNode = currentNode.Nodes.Cast<TreeNode>().FirstOrDefault(n => string.Equals(n.Tag as string, fullPath, StringComparison.OrdinalIgnoreCase));

                if (childNode == null)
                {
                    Program.Log?.Write(LogEventLevel.Information, $"Creating missing node '{fullPath}' dynamically");

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

            tree.SelectedNode = currentNode;
            currentNode.EnsureVisible();

            Program.Log?.Write(LogEventLevel.Information, $"SelectTreeNode completed, selected '{currentNode.Tag}'");
        }

        private static async Task NavigateTo(string newPath, UI.WP.ListView list, UI.WP.TreeView tree, bool updateStacks = true)
        {
            Program.Log?.Write(LogEventLevel.Information, $"NavigateTo called for '{newPath}'");

            if (string.IsNullOrEmpty(newPath) || newPath == currentPath)
            {
                Program.Log?.Write(LogEventLevel.Information, "NavigateTo aborted: path null/empty or unchanged");
                return;
            }

            if (updateStacks && !string.IsNullOrEmpty(currentPath))
            {
                backStack.Push(currentPath);
                forwardStack.Clear();

                Program.Log?.Write(LogEventLevel.Information, $"NavigateTo: pushed '{currentPath}' to backStack and cleared forwardStack");
            }

            currentPath = newPath;

            try
            {
                await PopulateListViewAsync(list, currentPath);
                SelectTreeNode(list, tree, currentPath);

                Program.Log?.Write(LogEventLevel.Information, $"Navigation complete: now at '{currentPath}'");
            }
            catch (Exception ex)
            {
                Program.Log?.Write(LogEventLevel.Error, $"NavigateTo failed for '{newPath}'", ex);
                throw;
            }
        }

        public static async Task GoBack(UI.WP.TreeView tree, UI.WP.ListView list)
        {
            Program.Log?.Write(LogEventLevel.Information, "GoBack invoked");

            if (backStack.Count == 0)
            {
                Program.Log?.Write(LogEventLevel.Information, "GoBack aborted: backStack empty");
                return;
            }

            forwardStack.Push(currentPath);
            string previousPath = backStack.Pop();

            Program.Log?.Write(LogEventLevel.Information, $"GoBack: navigating to '{previousPath}'");

            await NavigateTo(previousPath, list, tree, updateStacks: false);
        }

        public static async Task GoForward(UI.WP.TreeView tree, UI.WP.ListView list)
        {
            Program.Log?.Write(LogEventLevel.Information, "GoForward invoked");

            if (forwardStack.Count == 0)
            {
                Program.Log?.Write(LogEventLevel.Information, "GoForward aborted: forwardStack empty");
                return;
            }

            backStack.Push(currentPath);
            string nextPath = forwardStack.Pop();

            Program.Log?.Write(LogEventLevel.Information, $"GoForward: navigating to '{nextPath}'");

            await NavigateTo(nextPath, list, tree, updateStacks: false);
        }

        public static async Task GoUp(UI.WP.TreeView tree, UI.WP.ListView list)
        {
            Program.Log?.Write(LogEventLevel.Information, "GoUp invoked");

            if (string.IsNullOrEmpty(currentPath) || currentPath == _root)
            {
                Program.Log?.Write(LogEventLevel.Information, "GoUp aborted: at root or no currentPath");
                return;
            }

            string parent = GetParent(currentPath);

            if (!string.IsNullOrEmpty(parent))
            {
                Program.Log?.Write(LogEventLevel.Information, $"GoUp: navigating to parent '{parent}'");
                forwardStack.Clear();
                await NavigateTo(parent, list, tree);
            }
            else
            {
                Program.Log?.Write(LogEventLevel.Warning, $"GoUp failed: no parent found for '{currentPath}'");
            }
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
            if (size == 16)
            {
                imgList.AddWithAlpha("folder", Properties.Resources.folder_web_16);
                imgList.AddWithAlpha("file", Properties.Resources.file_16);
            }
            else
            {
                imgList.AddWithAlpha("folder", Properties.Resources.folder_web_48);
                imgList.AddWithAlpha("file", Properties.Resources.file_48);
            }

            using (Icon ico = Properties.Resources.fileextension.FromSize(size)) imgList.AddWithAlpha("wpth", ico.ToBitmap());
            using (Icon ico = Properties.Resources.ThemesResIcon.FromSize(size)) imgList.AddWithAlpha("wptp", ico.ToBitmap());
        }

        private static void BuildTree(UI.WP.TreeView tree)
        {
            Program.Log?.Write(LogEventLevel.Information, "BuildTree started");

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

            Program.Log?.Write(LogEventLevel.Information, "BuildTree finished");
        }

        private static void AddChildren(TreeNode parentNode, string parentPath)
        {
            Program.Log?.Write(LogEventLevel.Information, $"AddChildren called for '{parentPath}'");

            if (!DirectoryMap.ContainsKey(parentPath))
            {
                Program.Log?.Write(LogEventLevel.Information, $"AddChildren: no entries for '{parentPath}'");
                return;
            }

            foreach (RepositoryContent dir in DirectoryMap[parentPath].Where(d => d.Type == Octokit.ContentType.Dir).OrderBy(d => d.Name))
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

        private static string GetParent(string path)
        {
            Program.Log?.Write(LogEventLevel.Information, $"GetParent called for '{path}'");

            int i = path.LastIndexOf('/');
            string result = i < 0 ? string.Empty : path.Substring(0, i);

            Program.Log?.Write(LogEventLevel.Information, $"GetParent: result='{result}'");

            return result;
        }

        private static string ShaToMd5(string sha)
        {
            if (string.IsNullOrEmpty(sha)) return string.Empty;

            if (ShaMd5Cache.TryGetValue(sha, out string cached)) return cached;

            using MD5 md5 = MD5.Create();
            byte[] hashBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(sha));
            string result = string.Concat(hashBytes.Select(b => b.ToString("x2")));
            ShaMd5Cache[sha] = result;
            return result;
        }

        private static void HookEvents(UI.WP.TreeView tree, UI.WP.ListView list)
        {
            Program.Log?.Write(LogEventLevel.Information, "HookEvents attaching");

            tree.AfterSelect -= Tree_AfterSelect;
            list.DoubleClick -= List_DoubleClick;

            _boundList = list;
            _boundTree = tree;

            tree.AfterSelect += Tree_AfterSelect;
            list.DoubleClick += List_DoubleClick;

            Program.Log?.Write(LogEventLevel.Information, "HookEvents attached");
        }

        private static async void Tree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node == null || _boundList == null)
            {
                Program.Log?.Write(LogEventLevel.Warning, "Tree_AfterSelect: Node or list is null.");
                return;
            }

            string newPath = e.Node.Tag as string;

            if (string.IsNullOrEmpty(newPath))
            {
                Program.Log?.Write(LogEventLevel.Warning, "Tree_AfterSelect: Path is null or empty.");
                return;
            }

            if (newPath == currentPath)
            {
                return;
            }

            backStack.Push(currentPath);
            forwardStack.Clear();
            currentPath = newPath;

            await PopulateListViewAsync(_boundList, newPath);
        }

        private static async void List_DoubleClick(object sender, EventArgs e)
        {
            if (_boundList == null || _boundTree == null)
            {
                Program.Log?.Write(LogEventLevel.Warning, "List_DoubleClick: List or tree is null.");
                return;
            }

            if (_boundList.SelectedItems.Count == 0)
            {
                Program.Log?.Write(LogEventLevel.Information, "List_DoubleClick: No item selected.");
                return;
            }

            if (_boundList.SelectedItems[0].Tag is not RepositoryContent entry)
            {
                Program.Log?.Write(LogEventLevel.Warning, "List_DoubleClick: Selected item has no RepositoryContent tag.");
                return;
            }

            if (entry.Type.Value != Octokit.ContentType.Dir) return;

            await NavigateTo(entry.Path, _boundList, _boundTree);
        }

        private static TreeNode FindNode(TreeNode node, string path)
        {
            if (node == null) return null;

            if ((node.Tag as string) == path) return node;

            foreach (TreeNode child in node.Nodes)
            {
                TreeNode match = FindNode(child, path);
                if (match != null)
                {
                    return match;
                }
            }

            return null;
        }

        private static Func<RepositoryContent, string> FileTypeProvider { get; set; } = entry =>
        {
            if (entry.Type == Octokit.ContentType.Dir) return Program.Lang.Strings.GitHubStrings.Explorer_Type_Folder;
            if (entry.Name.EndsWith(".wpth", StringComparison.OrdinalIgnoreCase)) return Program.Lang.Strings.Extensions.WinPaletterTheme;
            if (entry.Name.EndsWith(".wptp", StringComparison.OrdinalIgnoreCase)) return Program.Lang.Strings.Extensions.WinPaletterResourcesPack;
            return NativeMethods.Shlwapi.GetFriendlyTypeName(entry.Name);
        };

        public static async Task PopulateRepositoryAsync(UI.WP.TreeView tree, UI.WP.ListView list, UI.WP.Breadcrumb breadCrumb, CancellationTokenSource cts = null, Action<int> reportProgress = null)
        {
            Program.Log?.Write(LogEventLevel.Information, "PopulateRepositoryAsync started");

            cts ??= new();

            try
            {
                Program.Log?.Write(LogEventLevel.Information, "Resetting Tree and List controls");

                ResetTree(tree, list);
                InitializeImageLists(list, tree);

                breadCrumb?.StartMarquee();
                breadCrumb.Value = breadCrumb.Minimum;

                Program.Log?.Write(LogEventLevel.Information, "Fetching repository data recursively");

                List<RepositoryContent> entries = [];

                await FetchRecursive(_root, entries, null, cts);

                Program.Log?.Write(LogEventLevel.Information, $"Fetched {entries.Count} entries");

                CachedEntries = entries;

                Program.Log?.Write(LogEventLevel.Information, "Building directory map");
                BuildDirectoryMap();

                Program.Log?.Write(LogEventLevel.Information, "Calculating folder sizes");
                BuildFolderSizes(_root);

                void UpdateUI()
                {
                    try
                    {
                        Program.Log?.Write(LogEventLevel.Information, "Building TreeView");
                        BuildTree(tree);

                        tree.Nodes[0].Expand();

                        Program.Log?.Write(LogEventLevel.Information, "Populating ListView");
                        _ = PopulateListViewAsync(list, _root, cts);

                        Program.Log?.Write(LogEventLevel.Information, "Hooking Tree/List events");
                        HookEvents(tree, list);

                        breadCrumb?.FinishLoadingAnimation();
                        breadCrumb.BoundTreeView = tree;

                        Program.Log?.Write(LogEventLevel.Information, "UI updated successfully");
                    }
                    catch (Exception ex)
                    {
                        Program.Log?.Write(LogEventLevel.Error, "Error while updating UI", ex);
                        throw;
                    }
                }

                if (tree.InvokeRequired)
                {
                    tree.Invoke((MethodInvoker)UpdateUI);
                }
                else
                {
                    UpdateUI();
                }

                Program.Log?.Write(LogEventLevel.Information, "PopulateRepositoryAsync completed successfully");
            }
            catch (OperationCanceledException ex)
            {
                Program.Log?.Write(LogEventLevel.Warning, "PopulateRepositoryAsync cancelled", ex);

                breadCrumb?.FinishLoadingAnimation();
                breadCrumb.Value = breadCrumb.Minimum;
            }
            catch (Exception ex)
            {
                Program.Log?.Write(LogEventLevel.Error, "Unhandled error in PopulateRepositoryAsync", ex);

                breadCrumb?.FinishLoadingAnimation();
                breadCrumb.Value = breadCrumb.Minimum;

                throw;
            }
        }

        private static async Task<IReadOnlyList<RepositoryContent>> GetContentsCachedAsync(string path)
        {
            if (DirectoryContentCache.TryGetValue(path, out var cached))
            {
                try
                {
                    var currentContents = await Program.GitHub.Client.Repository.Content.GetAllContentsByRef(_owner, _repo, path, _branch);
                    var currentSha = currentContents.FirstOrDefault()?.Sha ?? string.Empty;

                    if (DirectoryShaCache.TryGetValue(path, out var cachedSha) && cachedSha == currentSha)
                        return cached;

                    DirectoryContentCache[path] = currentContents;
                    DirectoryShaCache[path] = currentSha;
                    return currentContents;
                }
                catch
                {
                    return cached;
                }
            }

            var contents = await Program.GitHub.Client.Repository.Content.GetAllContents(_owner, _repo, path);
            DirectoryContentCache[path] = contents;
            DirectoryShaCache[path] = contents.FirstOrDefault()?.Sha ?? string.Empty;
            return contents;
        }

        private static async Task FetchRecursive(string path, List<RepositoryContent> output, Action<RepositoryContent> reportProgress = null, CancellationTokenSource cts = default, int maxDepth = -1, int currentDepth = 0)
        {
            if (cts.IsCancellationRequested) return;
            if (maxDepth >= 0 && currentDepth > maxDepth) return;

            IReadOnlyList<RepositoryContent> items;

            try
            {
                await _semaphore.WaitAsync(cts.Token); // wait for slot
                items = await GetContentsCachedAsync(path);   // cached fetch
            }
            catch
            {
                return; // ignore failures
            }
            finally
            {
                _semaphore.Release();
            }

            var subDirs = new List<string>();

            foreach (var entry in items)
            {
                if (cts.IsCancellationRequested) return;

                output.Add(entry);
                reportProgress?.Invoke(entry);

                if (entry.Type == Octokit.ContentType.Dir)
                    subDirs.Add(entry.Path);
            }

            var tasks = subDirs.Select(subDir => FetchRecursive(subDir, output, reportProgress, cts, maxDepth, currentDepth + 1));

            await Task.WhenAll(tasks);
        }

        private static async Task FetchRecursive(string path, List<string> output, CancellationTokenSource cts, bool onlyDirs, Action<int> reportProgress = null, int maxDepth = -1, int currentDepth = 0)
        {
            cts ??= new();
            if (maxDepth >= 0 && currentDepth > maxDepth) return;

            IReadOnlyList<RepositoryContent> items;
            try { items = await Program.GitHub.Client.Repository.Content.GetAllContents(_owner, _repo, path); }
            catch { return; }

            int total = items.Count;
            int processed = 0;
            foreach (var entry in items)
            {
                cts.Token.ThrowIfCancellationRequested();

                if ((onlyDirs && entry.Type == Octokit.ContentType.Dir) || (!onlyDirs && entry.Type != Octokit.ContentType.Dir))
                    output.Add(entry.Path);

                if (entry.Type == Octokit.ContentType.Dir)
                    await FetchRecursive(entry.Path, output, cts, onlyDirs, reportProgress, maxDepth, currentDepth + 1);

                processed++;
                reportProgress?.Invoke((int)((processed * 100L) / total));
            }
        }

        public static async Task RefreshAsync(UI.WP.TreeView tree, UI.WP.ListView list, UI.WP.Breadcrumb breadCrumb, CancellationTokenSource cts = null)
        {
            Program.Log?.Write(LogEventLevel.Information, "RefreshAsync started");

            cts ??= new();

            string pathBeforeRefresh = currentPath;

            CachedEntries = null;
            DirectoryMap.Clear();
            FolderSizeMap.Clear();

            breadCrumb?.StartMarquee();

            try
            {
                Program.Log?.Write(LogEventLevel.Information, "Refreshing: FetchRecursive started");

                List<RepositoryContent> entries = [];
                await FetchRecursive(_root, entries, null, cts);
                CachedEntries = entries;

                Program.Log?.Write(LogEventLevel.Information, $"RefreshAsync: fetched {entries.Count} entries");

                Program.Log?.Write(LogEventLevel.Information, "Rebuilding directory map");
                BuildDirectoryMap();

                Program.Log?.Write(LogEventLevel.Information, "Rebuilding folder sizes");
                BuildFolderSizes(_root);

                if (tree.InvokeRequired)
                {
                    Program.Log?.Write(LogEventLevel.Information, "Invoking BuildTree on UI thread");
                    tree.Invoke(new MethodInvoker(() => BuildTree(tree)));
                }
                else
                {
                    Program.Log?.Write(LogEventLevel.Information, "Building tree on same thread");
                    BuildTree(tree);
                }

                Program.Log?.Write(LogEventLevel.Information, $"Navigating back to '{pathBeforeRefresh}'");
                await NavigateTo(pathBeforeRefresh, list, tree, updateStacks: false);
            }
            catch (Exception ex)
            {
                Program.Log?.Write(LogEventLevel.Error, "RefreshAsync failed", ex);
                throw;
            }
            finally
            {
                Program.Log?.Write(LogEventLevel.Information, "RefreshAsync UI finalization");

                breadCrumb?.StopMarquee();
                breadCrumb?.FinishLoadingAnimation();
                breadCrumb.BoundTreeView = tree;

                HookEvents(tree, list);

                _ = PopulateListViewAsync(list, pathBeforeRefresh, cts);

                if (tree.Nodes[0] is not null)
                {
                    tree.SelectedNode = tree.Nodes[0];
                }
                else
                {
                    Program.Log?.Write(LogEventLevel.Warning, "Tree is empty after refresh; clearing UI lists");
                    tree.Nodes.Clear();
                    list.Items.Clear();
                }

                Program.Log?.Write(LogEventLevel.Information, "RefreshAsync completed");
            }
        }

        private static void BuildDirectoryMap()
        {
            Program.Log?.Write(LogEventLevel.Information, "BuildDirectoryMap started");

            DirectoryMap.Clear();

            foreach (RepositoryContent entry in CachedEntries)
            {
                string parent = GetParent(entry.Path);
                if (!DirectoryMap.ContainsKey(parent)) DirectoryMap[parent] = [];

                DirectoryMap[parent].Add(entry);
            }

            Program.Log?.Write(LogEventLevel.Information, $"BuildDirectoryMap completed: {DirectoryMap.Count} directory entries");
        }

        private static long BuildFolderSizes(string path)
        {
            Program.Log?.Write(LogEventLevel.Information, $"BuildFolderSizes started for '{path}'");

            if (!DirectoryMap.ContainsKey(path))
            {
                Program.Log?.Write(LogEventLevel.Information, $"BuildFolderSizes: no entries for '{path}'");
                return 0;
            }

            long total = 0;

            foreach (RepositoryContent entry in DirectoryMap[path])
            {
                if (entry.Type == Octokit.ContentType.Dir)
                {
                    total += BuildFolderSizes(entry.Path);
                }
                else
                {
                    total += entry.Size;
                }
            }

            FolderSizeMap[path] = total;

            Program.Log?.Write(LogEventLevel.Information, $"BuildFolderSizes completed for '{path}', size={total}");

            return total;
        }

        public static async Task PopulateListViewAsync(UI.WP.ListView list, string path, CancellationTokenSource cts = null)
        {
            Program.Log?.Write(LogEventLevel.Information, $"PopulateListViewAsync started for '{path}'");

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

                if (list.Columns.Count == 0)
                {
                    Program.Log?.Write(LogEventLevel.Information, "PopulateListViewAsync initialized columns");

                    list.Columns.Add(Program.Lang.Strings.GitHubStrings.Explorer_DetailsHeader_Name, 230);
                    list.Columns.Add(Program.Lang.Strings.GitHubStrings.Explorer_DetailsHeader_Type, 200);
                    list.Columns.Add(Program.Lang.Strings.GitHubStrings.Explorer_DetailsHeader_Size, 80);
                    list.Columns.Add("MD5", 120);
                    list.Columns.Add(Program.Lang.Strings.GitHubStrings.Explorer_DetailsHeader_URL, 120);
                }

                if (!DirectoryMap.ContainsKey(path))
                {
                    Program.Log?.Write(LogEventLevel.Warning, $"PopulateListViewAsync: DirectoryMap has no entry for '{path}'");
                    list.Items.Clear();
                    return;
                }

                List<RepositoryContent> entries = [.. DirectoryMap[path].OrderBy(e => e.Type == Octokit.ContentType.Dir ? 0 : 1).ThenBy(e => e.Name)];

                Program.Log?.Write(LogEventLevel.Information, $"PopulateListViewAsync: {entries.Count} entries for '{path}'");

                list.Items.Clear();

                int count = 0;
                foreach (RepositoryContent entry in entries)
                {
                    cts.Token.ThrowIfCancellationRequested();

                    ListViewItem item = new(entry.Name) { Tag = entry };

                    item.SubItems.Add(FileTypeProvider?.Invoke(entry) ?? Program.Lang.Strings.Extensions.File);

                    long size = entry.Type == Octokit.ContentType.Dir && FolderSizeMap.ContainsKey(entry.Path) ? FolderSizeMap[entry.Path] : entry.Size;

                    item.SubItems.Add(size.ToStringFileSize());
                    item.SubItems.Add(ShaToMd5(entry.Sha).ToUpper());
                    item.SubItems.Add(entry.HtmlUrl);
                    item.SubItems.Add(entry.Content);

                    if (entry.Type == Octokit.ContentType.Dir) item.ImageKey = "folder";
                    else if (entry.Name.EndsWith(".wpth", StringComparison.OrdinalIgnoreCase)) item.ImageKey = "wpth";
                    else if (entry.Name.EndsWith(".wptp", StringComparison.OrdinalIgnoreCase)) item.ImageKey = "wptp";
                    else item.ImageKey = "file";

                    list.Items.Add(item);

                    count++;
                    if (count % 50 == 0) await Task.Yield();
                }
            }
            catch (Exception ex)
            {
                Program.Log?.Write(LogEventLevel.Error, $"PopulateListViewAsync failed for '{path}'", ex);
            }
            finally
            {
                list.EndUpdate();
                list.Cursor = Cursors.Default;
                Program.Log?.Write(LogEventLevel.Information, $"PopulateListViewAsync finished for '{path}'");
            }
        }

        private static async Task<Entry> GetFromCacheValidatedAsync(string path)
        {
            // Check file cache first
            if (_fileCache.TryGetValue(path, out var fileEntry))
            {
                try
                {
                    var latest = (await Program.GitHub.Client.Repository.Commit
                        .GetAll(_owner, _repo, new CommitRequest { Path = path }))
                        .FirstOrDefault();

                    if (latest != null && latest.Sha == fileEntry.CommitSha)
                        return fileEntry;

                    ClearCache(path);
                }
                catch
                {
                    // fallback to cache on API error
                    return fileEntry;
                }
                return null;
            }

            // Check directory cache
            if (_dirCache.TryGetValue(path, out var dirEntry))
            {
                if (dirEntry.Children == null || dirEntry.Children.Count == 0)
                    return dirEntry; // empty directory

                // Validate child SHAs in bulk
                var childPaths = dirEntry.Children.Select(c => c.Path).ToList();
                var latestShas = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

                try
                {
                    foreach (var childPath in childPaths)
                    {
                        var latest = (await Program.GitHub.Client.Repository.Commit
                            .GetAll(_owner, _repo, new CommitRequest { Path = childPath }))
                            .FirstOrDefault();
                        if (latest != null) latestShas[childPath] = latest.Sha;
                    }
                }
                catch
                {
                    return dirEntry; // fallback on API error
                }

                foreach (var child in dirEntry.Children)
                {
                    if (child.Type == ElementType.Dir)
                    {
                        if (!_dirCache.TryGetValue(child.Path, out var cachedChild) ||
                            (latestShas.TryGetValue(child.Path, out var latestSha) && cachedChild.CommitSha != latestSha))
                        {
                            ClearCache(path);
                            return null;
                        }
                    }
                    else
                    {
                        if (!_fileCache.TryGetValue(child.Path, out var cachedChild) ||
                            (latestShas.TryGetValue(child.Path, out var latestSha) && cachedChild.CommitSha != latestSha))
                        {
                            ClearCache(path);
                            return null;
                        }
                    }
                }

                return dirEntry;
            }

            return null;
        }

        private static async Task<Entry> GetInfoAsync(string path, int maxDepth = 5, bool useCache = true)
        {
            if (string.IsNullOrEmpty(path)) return null;

            if (useCache)
            {
                var cached = await GetFromCacheValidatedAsync(path);
                if (cached != null) return cached;
            }

            IReadOnlyList<RepositoryContent> contents;
            try { contents = await Program.GitHub.Client.Repository.Content.GetAllContents(_owner, _repo, path); }
            catch { return null; }
            if (contents.Count == 0) return null;

            var firstItem = contents.First();
            Entry entry = await Entry.FromRepositoryContent(firstItem, path);

            if (entry.Type == ElementType.Dir && maxDepth > 0)
            {
                var children = new List<Entry>();
                var childPaths = contents.Select(c => c.Path).ToList();
                var childShas = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

                // Bulk fetch all latest commits for child paths in parallel
                try
                {
                    var commitTasks = childPaths.Select(p => Program.GitHub.Client.Repository.Commit.GetAll(_owner, _repo, new CommitRequest { Path = p }));
                    var commitResults = await Task.WhenAll(commitTasks);
                    for (int i = 0; i < childPaths.Count; i++)
                    {
                        var latest = commitResults[i].FirstOrDefault();
                        if (latest != null) childShas[childPaths[i]] = latest.Sha;
                    }
                }
                catch { /* fallback silently */ }

                foreach (var item in contents)
                {
                    Entry childEntry = null;

                    if (useCache)
                    {
                        if (item.Type == Octokit.ContentType.Dir)
                            _dirCache.TryGetValue(item.Path, out childEntry);
                        else
                            _fileCache.TryGetValue(item.Path, out childEntry);

                        if (childEntry != null &&
                            childShas.TryGetValue(item.Path, out var latestSha) &&
                            childEntry.CommitSha != latestSha)
                        {
                            childEntry = null;
                        }
                    }

                    if (childEntry == null)
                    {
                        if (item.Type == Octokit.ContentType.Dir)
                            childEntry = await GetInfoAsync(item.Path, maxDepth - 1, useCache);
                        else
                            childEntry = await Entry.FromRepositoryContent(item, item.Path);
                    }

                    if (childEntry != null)
                        children.Add(childEntry);
                }

                entry.Children = children;
            }

            if (useCache) StoreInCache(entry);
            return entry;
        }

        public static async Task<Entry> GetInfoCachedAsync(string path, bool forceRefresh = false, CancellationTokenSource cts = null)
        {
            cts ??= new();
            cts.Token.ThrowIfCancellationRequested();

            if (!forceRefresh && _infoCache.TryGetValue(path, out var cached) && DateTime.UtcNow - cached.fetched < CacheTTL)
                return cached.entry;

            try
            {
                var contents = await Task.Run(() =>
                {
                    cts.Token.ThrowIfCancellationRequested();
                    return Program.GitHub.Client.Repository.Content.GetAllContentsByRef(_owner, _repo, path);
                }, cts.Token);

                var content = contents.FirstOrDefault();
                var entry = new Entry
                {
                    Content = content,
                    Type = content?.Type == Octokit.ContentType.Dir ? ElementType.Dir : ElementType.File
                };

                if (entry != null)
                    _infoCache[path] = (entry, DateTime.UtcNow);

                return entry;
            }
            catch
            {
                _infoCache.TryRemove(path, out _);
                return null;
            }
        }

        public static async Task<List<Entry>> GetEntriesCachedAsync(string path, bool forceRefresh = false, CancellationTokenSource cts = null)
        {
            cts ??= new();
            cts.Token.ThrowIfCancellationRequested();

            if (!forceRefresh && _dirsCache.TryGetValue(path, out var cached) && DateTime.UtcNow - cached.fetched < CacheTTL)
                return cached.entries;

            try
            {
                var contents = await Task.Run(() =>
                {
                    cts.Token.ThrowIfCancellationRequested();
                    return Program.GitHub.Client.Repository.Content.GetAllContentsByRef(_owner, _repo, path);
                }, cts.Token);

                var entries = contents.Select(c => new Entry
                {
                    Content = c,
                    Type = c.Type == Octokit.ContentType.Dir ? ElementType.Dir : ElementType.File
                }).ToList();

                _dirsCache[path] = (entries, DateTime.UtcNow);
                return entries;
            }
            catch
            {
                _dirsCache.TryRemove(path, out _);
                return new List<Entry>();
            }
        }

        private static async Task CollectEntriesRecursive(string path, bool includeFiles, bool includeDirs, List<string> output, CancellationTokenSource cts, Action<int> reportProgress, int maxDepth, int currentDepth)
        {
            if (cts.IsCancellationRequested || (maxDepth >= 0 && currentDepth > maxDepth)) return;

            var entry = await GetInfoAsync(path, maxDepth: 1, useCache: true);
            if (entry?.Children == null) return;

            int total = entry.Children.Count;
            int processed = 0;

            foreach (var child in entry.Children)
            {
                cts.Token.ThrowIfCancellationRequested();

                if ((includeFiles && child.Type == ElementType.File) ||
                    (includeDirs && child.Type == ElementType.Dir))
                {
                    output.Add(child.Path);
                }

                if (child.Type == ElementType.Dir)
                    await CollectEntriesRecursive(child.Path, includeFiles, includeDirs, output, cts, reportProgress, maxDepth, currentDepth + 1);

                processed++;
                reportProgress?.Invoke(total > 0 ? (int)((processed * 100L) / total) : 100);
            }
        }

        public static async Task<List<string>> GetEntriesAsync(string path, bool includeFiles = true, bool includeDirs = true, CancellationTokenSource cts = null, Action<int> reportProgress = null, int maxDepth = -1)
        {
            cts ??= new();
            var output = new List<string>();
            await CollectEntriesRecursive(path, includeFiles, includeDirs, output, cts, reportProgress, maxDepth, 0);
            return output;
        }

        private static void StoreInCache(Entry entry, bool storeChildren = true)
        {
            if (entry == null) return;

            if (entry.Type == ElementType.File)
                _fileCache[entry.Path] = entry;
            else if (entry.Type == ElementType.Dir)
                _dirCache[entry.Path] = entry;

            if (storeChildren && entry.Children != null)
            {
                foreach (var child in entry.Children)
                    StoreInCache(child, storeChildren: false);
            }
        }

        private static void ClearCache(string path = null)
        {
            if (path == null)
            {
                _fileCache.Clear();
                _dirCache.Clear();
                _dirShaCache.Clear();
            }
            else
            {
                _fileCache.TryRemove(path, out _);
                _dirCache.TryRemove(path, out _);
                _dirShaCache.TryRemove(path, out _);

                foreach (var key in _fileCache.Keys.Where(k => k.StartsWith(path + "/")).ToList())
                    _fileCache.TryRemove(key, out _);

                foreach (var key in _dirCache.Keys.Where(k => k.StartsWith(path + "/")).ToList())
                    _dirCache.TryRemove(key, out _);
            }
        }
    }
}