using Octokit;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinPaletter.TypesExtensions;

namespace WinPaletter.GitHub
{
    public static partial class FileSystem
    {
        /// <summary>
        /// Provides a function to determine the file type description for a given repository content entry.
        /// </summary>
        public static Func<RepositoryContent, string> FileTypeProvider { get; set; } = entry =>
        {
            if (entry.Type == Octokit.ContentType.Dir) return Program.Lang.Strings.GitHubStrings.Explorer_Type_Folder;
            if (entry.Name.EndsWith(".wpth", StringComparison.OrdinalIgnoreCase)) return Program.Lang.Strings.Extensions.WinPaletterTheme;
            if (entry.Name.EndsWith(".wptp", StringComparison.OrdinalIgnoreCase)) return Program.Lang.Strings.Extensions.WinPaletterResourcesPack;
            if (entry.Name.EndsWith(".gitkeep", StringComparison.OrdinalIgnoreCase)) return ".gitkeep file";
            return NativeMethods.Shell32.GetExtensionDescription(GetExtension(entry.Name));
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
        /// Gets the root path of the repository.
        /// </summary>
        public static string _root = $"Themes/{_owner}";

        /// <summary>
        /// Gets or sets the current repository path being viewed.
        /// </summary>
        public static string currentPath = _root;

        /// <summary>
        /// To remember the last entered folder for navigation purposes.
        /// </summary>
        private static string _lastEnteredFolder;

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
        /// Event triggered whenever navigation occurs.
        /// </summary>
        public static event EventHandler<string> Navigated;

        /// <summary>
        /// Invokes the Navigated event.
        /// </summary>
        /// <param name="path">The path that was navigated to.</param>
        private static void OnNavigated(string path)
        {
            Navigated?.Invoke(null, path);
        }

        /// <summary>
        /// Set the branch to use for repository access.
        /// </summary>
        /// <param name="branch"></param>
        public static async void SetBranch(string branch)
        {
            GitHub.Repository.branch = branch;
            ClearAllCaches();
        }

        /// <summary>
        /// Set the branch to use for repository access.
        /// </summary>
        /// <param name="branch"></param>
        public static async void SetBranch(string branch, UI.WP.TreeView tree, UI.WP.ListView list, UI.WP.Breadcrumb breadCrumb, CancellationTokenSource cts = null)
        {
            SetBranch(branch);
            await PopulateRepositoryAsync(tree, list, breadCrumb, cts);
        }

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

                bool rootExists = await DirectoryExistsAsync(_root, cts);
                if (!rootExists) await CreateDirectoryAsync(_root, cts: cts);

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
        public static void UpdateTreeNode(UI.WP.TreeView tree, string path, bool selectAfterUpdate)
        {
            Program.Log?.Write(LogEventLevel.Information, $"[UpdateTreeNode] Called for path='{path}'");

            if (tree.Nodes.Count == 0)
            {
                Program.Log?.Write(LogEventLevel.Information, "[UpdateTreeNode] Tree has no nodes, exiting");
                return;
            }

            TreeNode rootNode = tree.Nodes[0];
            if (rootNode.Tag == null) rootNode.Tag = _root;

            // Normalize path first
            if (!path.StartsWith(_root + "/", StringComparison.OrdinalIgnoreCase))
                path = _root + "/" + path.TrimStart('/');

            Program.Log?.Write(LogEventLevel.Information, $"[UpdateTreeNode] Normalized path='{path}'");

            // Check if path is exactly root
            if (string.Equals(path, _root, StringComparison.OrdinalIgnoreCase))
            {
                tree.SelectedNode = rootNode;
                rootNode.EnsureVisible();
                Program.Log?.Write(LogEventLevel.Information, "[UpdateTreeNode] Path is root, selected root node");
                // Prune deleted nodes even if root is selected
                PruneDeletedNodes(rootNode);
                return;
            }

            string relativePath = path.Substring(_root.Length + 1);
            string[] segments = relativePath.Split('/');

            TreeNode currentNode = rootNode;

            // Initial pruning before traversal
            Program.Log?.Write(LogEventLevel.Information, "[UpdateTreeNode] Pruning deleted nodes before traversal");
            PruneDeletedNodes(rootNode);

            foreach (var segment in segments)
            {
                Program.Log?.Write(LogEventLevel.Information, $"[UpdateTreeNode] Processing segment='{segment}'");

                // Remove child nodes that no longer exist in cache
                foreach (TreeNode child in currentNode.Nodes.Cast<TreeNode>().ToList())
                {
                    string childPath = child.Tag as string;
                    if (!_cache.ContainsKey(childPath))
                    {
                        Program.Log?.Write(LogEventLevel.Information, $"[UpdateTreeNode] Removing deleted node '{childPath}'");
                        currentNode.Nodes.Remove(child);
                    }
                }

                // Find existing child or create dynamically
                TreeNode childNode = currentNode.Nodes.Cast<TreeNode>()
                    .FirstOrDefault(n => string.Equals(n.Text, segment, StringComparison.OrdinalIgnoreCase));

                if (childNode == null)
                {
                    string parentTag = currentNode.Tag as string ?? _root;
                    string fullTag = parentTag + "/" + segment;

                    childNode = new TreeNode(segment)
                    {
                        Tag = fullTag,
                        ImageKey = "folder",
                        SelectedImageKey = "folder"
                    };

                    currentNode.Nodes.Add(childNode);
                    Program.Log?.Write(LogEventLevel.Information, $"[UpdateTreeNode] Created missing node '{fullTag}' dynamically");
                }

                currentNode.Expand();
                currentNode = childNode;
            }

            // Final pruning to remove any nodes deleted during traversal
            Program.Log?.Write(LogEventLevel.Information, "[UpdateTreeNode] Pruning deleted nodes after traversal");
            PruneDeletedNodes(rootNode);

            if (selectAfterUpdate)
            {
                tree.SelectedNode = currentNode;
                Program.Log?.Write(LogEventLevel.Information, $"[UpdateTreeNode] Selected node '{currentNode.Tag}'");
            }

            currentNode.EnsureVisible();
            Program.Log?.Write(LogEventLevel.Information, "[UpdateTreeNode] UpdateTreeNode completed");
        }

        private static void PruneDeletedNodes(TreeNode node)
        {
            foreach (TreeNode child in node.Nodes.Cast<TreeNode>().ToList())
            {
                string childPath = child.Tag as string;
                if (!_cache.ContainsKey(childPath))
                {
                    Program.Log?.Write(LogEventLevel.Information, $"[PruneDeletedNodes] Pruning deleted node '{childPath}'");
                    node.Nodes.Remove(child);
                }
                else
                {
                    PruneDeletedNodes(child);
                }
            }
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
        /// <summary>
        /// Central method to navigate to a new path. Handles stack updates consistently.
        /// </summary>
        /// Central method to navigate to a new path. Handles stack updates consistently.
        /// </summary>
        public static async Task NavigateTo(string newPath, UI.WP.ListView list, UI.WP.TreeView tree, bool addToHistory = true)
        {
            if (string.IsNullOrEmpty(newPath)) return;

            // Normalize
            if (!newPath.StartsWith(_root, StringComparison.OrdinalIgnoreCase))
                newPath = _root + "/" + newPath.TrimStart('/');

            // Resolve
            string resolvedPath = newPath;
            while (!DirectoryMap.ContainsKey(resolvedPath) && !string.Equals(resolvedPath, _root, StringComparison.OrdinalIgnoreCase))
                resolvedPath = UppermostRoot(resolvedPath) ?? _root;

            // Already at target
            if (string.Equals(resolvedPath, currentPath, StringComparison.OrdinalIgnoreCase))
            {
                if (addToHistory) forwardStack.Clear(); // re-click: clear forward
                return;
            }

            // Update stacks
            if (addToHistory)
            {
                backStack.Push(currentPath);   // previous path goes to back
                forwardStack.Clear();          // new navigation clears forward
            }

            currentPath = resolvedPath;

            // Fire event
            OnNavigated(currentPath);

            await PopulateListViewAsync(list, currentPath);
            UpdateTreeNode(tree, currentPath, true);
        }

        /// <summary>
        /// Go back in history.
        /// </summary>
        public static async Task GoBack(UI.WP.TreeView tree, UI.WP.ListView list)
        {
            if (!CanGoBack) return;

            string previous = backStack.Pop();
            forwardStack.Push(currentPath); // current path goes to forwardStack

            await NavigateTo(previous, list, tree, addToHistory: false);
        }

        /// <summary>
        /// Go forward in history.
        /// </summary>
        public static async Task GoForward(UI.WP.TreeView tree, UI.WP.ListView list)
        {
            if (!CanGoForward) return;

            string next = forwardStack.Pop();
            backStack.Push(currentPath);  // current path goes to backStack

            await NavigateTo(next, list, tree, addToHistory: false);
        }

        /// <summary>
        /// Navigate to parent directory.
        /// </summary>
        public static async Task GoUp(UI.WP.TreeView tree, UI.WP.ListView list)
        {
            if (!CanGoUp) return;

            string parent = GetParent(currentPath) ?? _root;

            backStack.Push(currentPath);
            forwardStack.Clear();

            await NavigateTo(parent, list, tree, addToHistory: false);
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
            imgList.Images.Clear();

            if (size == 16)
            {
                imgList.AddWithAlpha("folder", Assets.GitHubMgr.folder_web_16);
                imgList.AddWithAlpha("file", Properties.Resources.file_16);
                imgList.AddWithAlpha("home", Assets.GitHubMgr.home_16);
            }
            else
            {
                imgList.AddWithAlpha("folder", Assets.GitHubMgr.folder_web_48);
                imgList.AddWithAlpha("file", Properties.Resources.file_48);
                imgList.AddWithAlpha("home", Assets.GitHubMgr.home_48);
            }

            using (Icon ico = Properties.Resources.fileextension.FromSize(size)) imgList.AddWithAlpha("wpth", ico.ToBitmap());
            using (Icon ico = Properties.Resources.ThemesResIcon.FromSize(size)) imgList.AddWithAlpha("wptp", ico.ToBitmap());

            int count = imgList.Images.Count;
            for (int i = 0; i < count; i++)
            {
                string key = $"ghost_{imgList.Images.Keys[i]}";
                Bitmap ghost = (imgList.Images[i] as Bitmap).Ghost();
                imgList.AddWithAlpha(key, ghost);
            }
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

            // Use last segment of _root as the root node text
            string rootText = _root.Split('/').Last();
            TreeNode root = new(rootText)
            {
                Tag = _root, // full path for navigation
                ImageKey = "home",
                SelectedImageKey = "home"
            };
            tree.Nodes.Add(root);

            // Add children starting from parent of last segment
            string parentPath = string.Join("/", _root.Split('/').Take(_root.Split('/').Length - 1));
            AddChildren(root, _root); // still pass full _root, AddChildren will now add only children

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
            if (e.Node == null || _boundList == null || _boundTree == null) return;

            string newPath = e.Node.Tag as string;
            if (string.IsNullOrEmpty(newPath)) return;

            // If the user selects the currently shown folder, do nothing
            if (newPath == currentPath) return;

            // Navigate properly: update history
            await NavigateTo(newPath, _boundList, _boundTree, true);
        }

        /// <summary>
        /// Handles the DoubleClick event of the ListView control to navigate into directories
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static async void List_DoubleClick(object sender, EventArgs e)
        {
            if (_boundList == null || _boundTree == null) return;
            if (_boundList.SelectedItems.Count == 0) return;
            if (_boundList.SelectedItems[0].Tag is not RepositoryContent entry) return;
            if (entry.Type.Value != Octokit.ContentType.Dir) return;

            _lastEnteredFolder = entry.Name;

            // Navigate normally, history will be updated inside NavigateTo
            await NavigateTo(entry.Path, _boundList, _boundTree, addToHistory: true);
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
                await NavigateTo(pathBeforeRefresh, list, tree, false);
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

        /// <summary>
        /// Asynchronously searches cached repository entries under the current path
        /// for entries whose names match the specified keyword pattern. Supports wildcards '*' and '?'.
        /// </summary>
        /// <param name="list">The ListView control to populate with search results.</param>
        /// <param name="keyword">The search keyword. Can include '*' and '?' as wildcards.</param>
        /// <param name="cts">Optional cancellation token source to cancel the search.</param>
        /// <returns>A task representing the asynchronous search operation.</returns>
        public static async Task SearchAsync(UI.WP.ListView list, string keyword, CancellationTokenSource cts = null)
        {
            Program.Log?.Write(LogEventLevel.Information, $"SearchAsync started for keyword='{keyword}' in currentPath='{currentPath}'");

            if (string.IsNullOrWhiteSpace(keyword))
            {
                // Reset view to current path
                await PopulateListViewAsync(list, currentPath, cts);
                return;
            }

            cts ??= new();

            if (list.InvokeRequired)
            {
                list.Invoke(new Func<Task>(() => SearchAsync(list, keyword, cts)));
                return;
            }

            try
            {
                list.Cursor = Cursors.WaitCursor;
                list.BeginUpdate();

                if (list.Columns.Count == 0)
                {
                    list.Columns.Add(Program.Lang.Strings.GitHubStrings.Explorer_DetailsHeader_Name, 230);
                    list.Columns.Add(Program.Lang.Strings.GitHubStrings.Explorer_DetailsHeader_Type, 200);
                    list.Columns.Add(Program.Lang.Strings.GitHubStrings.Explorer_DetailsHeader_Size, 80);
                    list.Columns.Add("MD5", 120);
                    list.Columns.Add(Program.Lang.Strings.GitHubStrings.Explorer_DetailsHeader_URL, 120);
                }

                bool hasWildcard = keyword.Contains("*") || keyword.Contains("?");

                List<RepositoryContent> matches = new();

                await Task.Run(() =>
                {
                    if (hasWildcard)
                    {
                        string pattern = "^" + Regex.Escape(keyword).Replace(@"\*", ".*").Replace(@"\?", ".") + "$";
                        Regex rx = null;
                        try { rx = new Regex(pattern, RegexOptions.IgnoreCase); } catch { }

                        foreach (var entry in _cache.Values.Select(v => v.entry.Content))
                        {
                            cts.Token.ThrowIfCancellationRequested();
                            if (entry == null) continue;

                            string name = entry.Name;

                            if ((rx != null && rx.IsMatch(name)) || (rx == null && name.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0))
                                matches.Add(entry);
                        }
                    }
                    else
                    {
                        foreach (var entry in _cache.Values.Select(v => v.entry.Content))
                        {
                            cts.Token.ThrowIfCancellationRequested();
                            if (entry == null) continue;

                            if (entry.Name.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0)
                                matches.Add(entry);
                        }
                    }
                });

                matches = matches
                    .Where(e => e.Path.StartsWith(currentPath, StringComparison.OrdinalIgnoreCase))
                    .OrderBy(e => e.Type == Octokit.ContentType.Dir ? 0 : 1)
                    .ThenBy(e => e.Name)
                    .ToList();

                Program.Log?.Write(LogEventLevel.Information, $"SearchAsync found {matches.Count} matching entries under '{currentPath}'");

                list.Items.Clear();

                int count = 0;
                foreach (var entry in matches)
                {
                    cts.Token.ThrowIfCancellationRequested();

                    ListViewItem item = new(entry.Name) { Tag = entry };
                    item.SubItems.Add(FileTypeProvider?.Invoke(entry) ?? Program.Lang.Strings.Extensions.File);

                    long size = entry.Type == Octokit.ContentType.Dir && FolderSizeMap.ContainsKey(entry.Path)
                        ? FolderSizeMap[entry.Path]
                        : entry.Size;

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
            catch (OperationCanceledException)
            {
                Program.Log?.Write(LogEventLevel.Warning, $"SearchAsync cancelled for keyword='{keyword}'");
            }
            catch (Exception ex)
            {
                Program.Log?.Write(LogEventLevel.Error, $"SearchAsync failed for keyword='{keyword}'", ex);
            }
            finally
            {
                list.EndUpdate();
                list.Cursor = Cursors.Default;
                Program.Log?.Write(LogEventLevel.Information, $"SearchAsync finished for keyword='{keyword}' in currentPath='{currentPath}'");
            }
        }
    }
}