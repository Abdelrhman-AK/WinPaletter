using Octokit;
using Serilog.Events;
using System;
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
    public static partial class FileSystem
    {
        /// <summary>
        /// Provides a function to determine the file type description for a given repository content entry.
        /// </summary>
        private static Func<RepositoryContent, string> FileTypeProvider { get; set; } = entry =>
        {
            if (entry.Type == Octokit.ContentType.Dir) return Program.Lang.Strings.GitHubStrings.Explorer_Type_Folder;
            if (entry.Name.EndsWith(".wpth", StringComparison.OrdinalIgnoreCase)) return Program.Lang.Strings.Extensions.WinPaletterTheme;
            if (entry.Name.EndsWith(".wptp", StringComparison.OrdinalIgnoreCase)) return Program.Lang.Strings.Extensions.WinPaletterResourcesPack;
            return NativeMethods.Shlwapi.GetFriendlyTypeName(entry.Name);
        };

        /// <summary>
        /// The ListView currently bound to the FileSystem for navigation and display.
        /// </summary>
        private static UI.WP.ListView _boundList;

        /// <summary>
        /// The TreeView currently bound to the FileSystem for navigation and display.
        /// </summary>
        private static UI.WP.TreeView _boundTree;

        /// <summary>
        /// ImageList containing small icons for ListView and TreeView items.
        /// </summary>
        private static ImageList _smallIcons;

        /// <summary>
        /// ImageList containing large icons for ListView items.
        /// </summary>
        private static ImageList _largeIcons;

        /// <summary>
        /// ImageList containing icons for TreeView items.
        /// </summary>
        private static ImageList _treeIcons;

        /// <summary>
        /// Stores the navigation history for backward navigation within the application.
        /// </summary>
        private static Stack<string> backStack = new();

        /// <summary>
        /// Stores the collection of URLs available for forward navigation in the browser history stack.
        /// </summary>
        /// <remarks>This stack is used to manage forward navigation after a user navigates backward. It
        /// is cleared when a new navigation occurs that is not a forward action.</remarks>
        private static Stack<string> forwardStack = new();

        /// <summary>
        /// Gets or sets the current repository path being viewed.
        /// </summary>
        private static string currentPath = _root;

        /// <summary>
        /// Gets whether backward navigation is possible.
        /// </summary>
        public static bool CanGoBack => backStack.Count > 0;

        /// <summary>
        /// Gets whether forward navigation is possible.
        /// </summary>
        public static bool CanGoForward => forwardStack.Count > 0;

        /// <summary>
        /// Gets whether navigation to the parent directory is possible.
        /// </summary>
        public static bool CanGoUp => !string.IsNullOrEmpty(currentPath) && currentPath != _root;

        /// <summary>
        /// Converts a GitHub SHA string into a deterministic MD5 hash for UI and caching purposes.
        /// </summary>
        /// <param name="sha">The SHA string.</param>
        /// <returns>An MD5 hex string, or empty string if input is null.</returns>
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

        /// <summary>
        /// Fetches the entire repository hierarchy recursively, builds internal caches,
        /// constructs the TreeView structure, and populates the ListView with the root directory.
        /// </summary>
        /// <param name="tree">TreeView control to populate.</param>
        /// <param name="list">ListView control to populate.</param>
        /// <param name="breadCrumb">Breadcrumb control used for displaying paths.</param>
        /// <param name="cts">Optional cancellation token source.</param>
        /// <param name="reportProgress">Optional callback reporting fetch progress.</param>
        /// <returns>A task that completes when repository data is fully loaded.</returns>
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

                // After this, store entries in _infoCache
                foreach (var entry in entries)
                {
                    if (entry == null) continue;

                    var e = new Entry
                    {
                        Path = entry.Path,
                        Content = entry,
                        Type = entry.Type == Octokit.ContentType.Dir ? FileSystem.EntryType.Dir : FileSystem.EntryType.File,
                        FetchedAt = DateTime.UtcNow
                    };
                    _cache[e.Path] = (e, DateTime.UtcNow);
                }

                Program.Log?.Write(LogEventLevel.Information, "Building directory map");
                BuildDirectoryMap();

                Program.Log?.Write(LogEventLevel.Information, "Calculating folder sizes");
                GetFoldersSize(_root);

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

        /// <summary>
        /// Resets the specified tree view and list view by clearing all nodes and items, and deselecting any selected
        /// node in the tree view.
        /// </summary>
        /// <param name="tree">The tree view control whose nodes will be cleared and selection reset.</param>
        /// <param name="list">The list view control whose items will be cleared.</param>
        static void ResetTree(UI.WP.TreeView tree, UI.WP.ListView list)
        {
            tree.Nodes.Clear();
            tree.SelectedNode = null;

            list.Items.Clear();
        }

        /// <summary>
        /// Selects the tree node in the specified tree view that corresponds to the given path, creating any missing
        /// nodes along the path if necessary.
        /// </summary>
        /// <remarks>If the root node of the tree does not match the first segment of the path, no
        /// selection is performed. Newly created nodes use the 'folder' image key and are expanded to ensure
        /// visibility. The method does not modify the list view parameter.</remarks>
        /// <param name="list">The list view associated with the tree view. This parameter is not modified by the method.</param>
        /// <param name="tree">The tree view in which to select the node. Must contain at least one node; otherwise, no selection is made.</param>
        /// <param name="path">The hierarchical path, delimited by '/', identifying the node to select. If nodes along the path do not
        /// exist, they are created dynamically.</param>
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

        /// <summary>
        /// Navigates to the specified repository path, updates navigation history,
        /// repopulates the ListView, and selects the corresponding TreeView node.
        /// </summary>
        /// <param name="newPath">The target repository path.</param>
        /// <param name="list">The ListView to update.</param>
        /// <param name="tree">The TreeView to update.</param>
        /// <param name="updateStacks">Whether to update back/forward navigation history.</param>
        /// <returns>A task representing the navigation operation.</returns>
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

        /// <summary>
        /// Navigates to the previously visited repository path, if available.
        /// </summary>
        /// <param name="tree">The TreeView bound to the explorer.</param>
        /// <param name="list">The ListView bound to the explorer.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
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

        /// <summary>
        /// Navigates forward in history to the next repository path, if available.
        /// </summary>
        /// <param name="tree">The TreeView bound to the explorer.</param>
        /// <param name="list">The ListView bound to the explorer.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
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

        /// <summary>
        /// Navigates to the parent directory of the current repository path.
        /// </summary>
        /// <param name="tree">The TreeView instance to update.</param>
        /// <param name="list">The ListView instance to update.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
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

        /// <summary>
        /// Initializes and assigns image lists for the specified list and tree controls, configuring icon sizes for
        /// each control type.
        /// </summary>
        /// <remarks>This method sets up image lists with appropriate icon sizes for list and tree
        /// controls. After calling this method, the controls will display icons according to the assigned image lists.
        /// Existing image lists on the controls will be replaced.</remarks>
        /// <param name="list">The list view control to which small and large image lists will be assigned.</param>
        /// <param name="tree">The tree view control to which the image list will be assigned.</param>
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

        /// <summary>
        /// Adds a predefined set of folder and file icons to the specified image list at the given size.
        /// </summary>
        /// <remarks>This method adds both standard and theme-specific icons to the image list. The set of
        /// icons and their appearance may vary depending on the specified size.</remarks>
        /// <param name="imgList">The image list to which the icons will be added. Must not be null.</param>
        /// <param name="size">The size, in pixels, of the icons to add. Typically 16 or 48.</param>
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

        /// <summary>
        /// Initializes the specified tree view control with nodes representing the hierarchical structure of the root
        /// object.
        /// </summary>
        /// <remarks>Call this method to refresh the tree view's contents to reflect the current state of
        /// the root object. The method clears any existing nodes before rebuilding the tree. The operation is performed
        /// within a batch update to minimize UI flicker.</remarks>
        /// <param name="tree">The tree view control to populate with nodes. Must not be null.</param>
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

        /// <summary>
        /// Recursively adds child directory nodes to the specified parent node based on the directory structure mapped
        /// to the given path.
        /// </summary>
        /// <remarks>Only directories present in the directory map for the specified path are added. Child
        /// nodes are added in alphabetical order by name.</remarks>
        /// <param name="parentNode">The parent <see cref="TreeNode"/> to which child directory nodes will be added.</param>
        /// <param name="parentPath">The path representing the directory whose child directories will be added as nodes. Must correspond to a key
        /// in the directory map.</param>
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

        /// <summary>
        /// Attaches event handlers to the specified tree and list controls for navigation and interaction.
        /// </summary>
        /// <param name="tree"></param>
        /// <param name="list"></param>
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

        /// <summary>
        /// Handles the AfterSelect event of the TreeView control to navigate to the selected node's path
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Handles the DoubleClick event of the ListView control to navigate into directories
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Recursively searches for a TreeNode with the specified path tag starting from the given node.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="path"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Populates the ListView with the files and directories inside a specified repository path.
        /// </summary>
        /// <param name="list">The ListView to fill.</param>
        /// <param name="path">The GitHub repository directory path.</param>
        /// <param name="cts">Optional cancellation token source.</param>
        /// <returns>A task representing the asynchronous population operation.</returns>
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
                    cts?.Token.ThrowIfCancellationRequested();

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

        /// <summary>
        /// Clears all caches, fetches repository data again, rebuilds the directory tree,
        /// and restores the previously selected path.
        /// </summary>
        /// <param name="tree">TreeView to refresh.</param>
        /// <param name="list">ListView to refresh.</param>
        /// <param name="breadCrumb">Breadcrumb control to update.</param>
        /// <param name="cts">Optional cancellation token source.</param>
        /// <returns>A task representing the refresh operation.</returns>
        public static async Task RefreshAsync(UI.WP.TreeView tree, UI.WP.ListView list, UI.WP.Breadcrumb breadCrumb, CancellationTokenSource cts = null)
        {
            Program.Log?.Write(LogEventLevel.Information, "RefreshAsync started");

            cts ??= new();

            string pathBeforeRefresh = currentPath;

            // Clear caches first
            _cache.Clear();
            DirectoryMap.Clear();
            FolderSizeMap.Clear();
            ClearEntryCache();

            breadCrumb?.StartMarquee();

            try
            {
                Program.Log?.Write(LogEventLevel.Information, "Refreshing: FetchRecursive started");

                List<RepositoryContent> entries = [];
                await FetchRecursive(_root, entries, null, cts);

                // After this, store entries in _infoCache
                foreach (var entry in entries)
                {
                    if (entry == null) continue;

                    var e = new Entry
                    {
                        Path = entry.Path,
                        Content = entry,
                        Type = entry.Type == Octokit.ContentType.Dir ? FileSystem.EntryType.Dir : FileSystem.EntryType.File,
                        FetchedAt = DateTime.UtcNow
                    };
                    _cache[e.Path] = (e, DateTime.UtcNow);
                }

                Program.Log?.Write(LogEventLevel.Information, $"RefreshAsync: fetched {entries.Count} entries");

                Program.Log?.Write(LogEventLevel.Information, "Rebuilding directory map");
                BuildDirectoryMap();

                Program.Log?.Write(LogEventLevel.Information, "Rebuilding folder sizes");
                GetFoldersSize(_root);

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
    }
}
