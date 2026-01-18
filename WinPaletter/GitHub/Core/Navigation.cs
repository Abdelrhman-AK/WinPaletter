using Octokit;
using Ookii.Dialogs.WinForms;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.ListView;

namespace WinPaletter.GitHub
{
    public static partial class FileSystem
    {
        #region Fields

        private static UI.WP.ContextMenuStrip contextMenu_all = new();
        private static UI.WP.ContextMenuStrip contextMenu_item = new();
        private static List<ListViewItem> cutItems;
        private static List<ListViewItem> copiedItems;
        private static ListViewItem itemBeingEdited;
        private static CancellationTokenSource cts = new();
        private static SoundPlayer SP = new();
        private static bool AltPlayingMethod = false;
        public static ActionQueue ActionQueue { get; private set; } = new();

        #region Properties

        /// <summary>
        /// Gets or sets the current repository path being viewed.
        /// </summary>
        public static string CurrentPath { get; set; } = _root;

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
        public static bool CanGoUp => !string.IsNullOrEmpty(CurrentPath) && CurrentPath != _root;

        private static bool CanPaste => (cutItems?.Count ?? 0) > 0 || (copiedItems?.Count ?? 0) > 0;

        public static bool ShowHiddenFiles
        {
            get => showHiddenFiles;
            set
            {
                if (showHiddenFiles != value)
                {
                    showHiddenFiles = value;
                    Task.Run(() => PopulateListViewAsync(CurrentPath));
                }
            }
        }
        private static bool showHiddenFiles = true;

        public static bool FilesOperationsLinking { get; set; } = true;

        #endregion

        #region Data functions

        private class NavigationState
        {
            public string Path { get; set; }
            public string SelectedItemPath { get; set; }
        }

        /// <summary>
        /// Provides a function to determine the file type description for a given repository content entry.
        /// </summary>
        public static Func<RepositoryContent, string> FileTypeProvider { get; set; } = entry =>
        {
            if (entry.Type == Octokit.ContentType.Dir) return Program.Lang.Strings.GitHubStrings.Explorer_Type_Folder;
            if (entry.Name.EndsWith(".wpth", StringComparison.OrdinalIgnoreCase)) return Program.Lang.Strings.Extensions.WinPaletterThemeFile;
            if (entry.Name.EndsWith(".wptp", StringComparison.OrdinalIgnoreCase)) return Program.Lang.Strings.Extensions.WinPaletterResourcesPack;
            if (entry.Name.EndsWith(".gitkeep", StringComparison.OrdinalIgnoreCase)) return ".gitkeep file";
            return NativeMethods.Shell32.GetExtensionDescription(GetExtension(entry.Name));
        };

        #endregion

        #region Bound Controls

        /// <summary>
        /// The ListView currently bound to the FileSystem for navigation and display.
        /// </summary>
        private static UI.WP.ListView _boundList;

        /// <summary>
        /// The TreeView currently bound to the FileSystem for navigation and display.
        /// </summary>
        private static UI.WP.TreeView _boundTree;

        /// <summary>
        /// The Breadcrumb currently bound to the FileSystem for navigation and display.
        /// </summary>
        private static UI.WP.Breadcrumb _boundbreadcrumbControl;

        #endregion

        #region ImageLists

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

        #endregion

        #region History stacks and data

        /// <summary>
        /// Stores the navigation history for backward navigation within the application.
        /// </summary>
        private static Stack<NavigationState> backStack = new();

        /// <summary>
        /// Stores the collection of URLs available for forward navigation in the browser history stack.
        /// </summary>
        /// <remarks>This stack is used to manage forward navigation after a user navigates backward. It
        /// is cleared when a new navigation occurs that is not a forward action.</remarks>
        private static Stack<NavigationState> forwardStack = new();

        /// <summary>
        /// Gets the root path of the repository.
        /// </summary>
        public static string _root = $"Themes/{Repository.Owner}";

        /// <summary>
        /// To remember the last entered folder for navigation purposes.
        /// </summary>
        private static string _lastEnteredFolderPath;

        #endregion

        #region Context Menus Fields

        // Views list
        public static readonly List<(string label, Bitmap icon, Bitmap glyph, View view)> Views =
        [
            (Program.Lang.Strings.GitHubStrings.Explorer_View_LargeIcons, Assets.GitHubMgr.Icons_Large, Assets.GitHubMgr.Glyph_View_Large, View.LargeIcon),
            (Program.Lang.Strings.GitHubStrings.Explorer_View_SmallIcons, Assets.GitHubMgr.Icons_Small, Assets.GitHubMgr.Glyph_View_Small, View.SmallIcon),
            (Program.Lang.Strings.GitHubStrings.Explorer_View_List, Assets.GitHubMgr.Icons_List, Assets.GitHubMgr.Glyph_View_List, View.List),
            (Program.Lang.Strings.GitHubStrings.Explorer_View_Details, Assets.GitHubMgr.Icons_Details, Assets.GitHubMgr.Glyph_View_Details, View.Details),
            (Program.Lang.Strings.GitHubStrings.Explorer_View_Tiles, Assets.GitHubMgr.Icons_Tile, Assets.GitHubMgr.Glyph_View_Tile, View.Tile)
        ];

        // Global menu items
        private static ToolStripMenuItem menu_view;
        private static ToolStripSeparator separator_0;
        private static ToolStripMenuItem menu_paste;
        private static ToolStripSeparator separator_1;
        private static ToolStripMenuItem menu_newItem;
        private static ToolStripMenuItem menu_newTheme;
        private static ToolStripMenuItem menu_newFolder;
        private static ToolStripSeparator separator_2;
        private static ToolStripMenuItem menu_properties;

        // Item menu
        private static ToolStripMenuItem menu_Open;
        private static ToolStripMenuItem menu_Download;
        private static ToolStripSeparator separator_item_1;
        private static ToolStripMenuItem menu_CopyPath;
        private static ToolStripMenuItem menu_CopyURL;
        private static ToolStripMenuItem menu_Copy;
        private static ToolStripMenuItem menu_Cut;
        private static ToolStripSeparator separator_item_2;
        private static ToolStripMenuItem menu_Delete;
        private static ToolStripMenuItem menu_Rename;
        private static ToolStripSeparator separator_item_3;
        private static ToolStripMenuItem menu_item_properties;

        #endregion

        #endregion

        #region Branch setting and repository population

        /// <summary>
        /// Set the branch to use for repository access.
        /// </summary>
        /// <param name="branch"></param>
        public static async void SetBranch(string branch)
        {
            GitHub.Repository.Branch.Name = branch;
            Cache.Clear();
        }

        /// <summary>
        /// Set the branch to use for repository access.
        /// </summary>
        /// <param name="branch"></param>
        public static async Task SetBranch(string branch, UI.WP.TreeView tree, UI.WP.ListView list, UI.WP.Breadcrumb breadCrumb)
        {
            SetBranch(branch);
            await PopulateRepositoryAsync(tree, list, breadCrumb);
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
        public static async Task PopulateRepositoryAsync(UI.WP.TreeView tree, UI.WP.ListView list, UI.WP.Breadcrumb breadCrumb, Action<int> reportProgress = null)
        {
            Program.Log?.Write(LogEventLevel.Information, "PopulateRepositoryAsync started");

            try
            {
                Program.Log?.Write(LogEventLevel.Information, "Resetting Tree and List controls");

                _boundbreadcrumbControl = breadCrumb;
                _boundList = list;
                _boundTree = tree;

                ResetTree();
                InitializeImageLists();

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

                    Cache.Add(e);
                }

                void UpdateUI()
                {
                    try
                    {
                        InitializeMenus();

                        Program.Log?.Write(LogEventLevel.Information, "Building TreeView");
                        BuildTree();

                        tree.Nodes[0].Expand();

                        Program.Log?.Write(LogEventLevel.Information, "Populating ListView");
                        _ = PopulateListViewAsync(_root);

                        Program.Log?.Write(LogEventLevel.Information, "Hooking Tree/List events");
                        HookEvents();

                        breadCrumb?.FinishLoadingAnimation();
                        breadCrumb.BoundTreeView = _boundTree;

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
        /// Clears all caches, fetches repository data again, rebuilds the directory tree,
        /// and restores the previously selected path.
        /// </summary>
        /// <param name="tree">TreeView to refresh.</param>
        /// <param name="list">ListView to refresh.</param>
        /// <param name="breadCrumb">Breadcrumb control to update.</param>
        /// <param name="cts">Optional cancellation token source.</param>
        /// <returns>A task representing the refresh operation.</returns>
        public static async Task RefreshAsync()
        {
            Program.Log?.Write(LogEventLevel.Information, "RefreshAsync started");

            string pathBeforeRefresh = CurrentPath;

            // Clear caches first
            Cache.Clear();

            _boundbreadcrumbControl?.StartMarquee();

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

                    Cache.Add(e);
                }

                Program.Log?.Write(LogEventLevel.Information, $"RefreshAsync: fetched {entries.Count} entries");

                if (_boundTree.InvokeRequired)
                {
                    Program.Log?.Write(LogEventLevel.Information, "Invoking BuildTree on UI thread");
                    _boundTree.Invoke(new MethodInvoker(() => BuildTree()));
                }
                else
                {
                    Program.Log?.Write(LogEventLevel.Information, "Building tree on same thread");
                    BuildTree();
                }

                Program.Log?.Write(LogEventLevel.Information, $"Navigating back to '{pathBeforeRefresh}'");
                await NavigateTo(pathBeforeRefresh, false);
            }
            catch (Exception ex)
            {
                Program.Log?.Write(LogEventLevel.Error, "RefreshAsync failed", ex);
                throw;
            }
            finally
            {
                Program.Log?.Write(LogEventLevel.Information, "RefreshAsync UI finalization");

                _boundbreadcrumbControl?.StopMarquee();
                _boundbreadcrumbControl?.FinishLoadingAnimation();
                _boundbreadcrumbControl.BoundTreeView = _boundTree;

                HookEvents();

                _ = PopulateListViewAsync(pathBeforeRefresh);

                if (_boundTree.Nodes[0] is not null)
                {
                    _boundTree.SelectedNode = _boundTree.Nodes[0];
                }
                else
                {
                    Program.Log?.Write(LogEventLevel.Warning, "Tree is empty after refresh; clearing UI lists");
                    _boundTree.Nodes.Clear();
                    _boundList.Items.Clear();
                }

                Program.Log?.Write(LogEventLevel.Information, "RefreshAsync completed");
            }
        }

        #endregion

        #region Images helpers

        /// <summary>
        /// Adds a predefined set of folder and file icons to the specified image list at the given size.
        /// </summary>
        /// <remarks>This method adds both standard and theme-specific icons to the image list. The set of
        /// icons and their appearance may vary depending on the specified size.</remarks>
        /// <param name="imgList">The image list to which the icons will be added. Must not be null.</param>
        /// <param name="size">The size, in pixels, of the icons to add. Typically 16 or 48.</param>
        private static void AddStockIcons(ImageList imgList)
        {
            imgList.Images.Clear();

            if (imgList.ImageSize.Width < 24)
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
        }

        private static void AddSpecialIcons(ImageList imgList, int size)
        {
            using (Icon ico = Properties.Resources.fileextension.FromSize(size)) imgList.AddWithAlpha("wpth", ico.ToBitmap());
            using (Icon ico = Properties.Resources.ThemesResIcon.FromSize(size)) imgList.AddWithAlpha("wptp", ico.ToBitmap());
        }

        private static void ProcessGhostIcons(ImageList imgList)
        {
            int count = imgList.Images.Count;

            for (int i = 0; i < count; i++)
            {
                string baseKey = imgList.Images.Keys[i];
                string ghostKey = $"ghost_{baseKey}";

                if (imgList.Images.ContainsKey(ghostKey)) continue;

                if (imgList.Images[i] is not Bitmap bmp) continue;

                Bitmap ghost = bmp.Ghost();
                imgList.AddWithAlpha(ghostKey, ghost);
            }
        }

        #endregion

        #region List/Tree views operations

        public static async Task UpdateExplorerView(string path)
        {
            await PopulateListViewAsync(path);
            UpdateTreeNode(path, false);
        }

        /// <summary>
        /// Populates the ListView with the files and directories inside a specified repository path.
        /// </summary>
        /// <param name="list">The ListView to fill.</param>
        /// <param name="path">The GitHub repository directory path.</param>
        /// <param name="cts">Optional cancellation token source.</param>
        /// <returns>A task representing the asynchronous population operation.</returns>
        public static async Task PopulateListViewAsync(string path)
        {
            Program.Log?.Write(LogEventLevel.Information, $"PopulateListViewAsync started for '{path}'");

            if (_boundList.InvokeRequired)
            {
                _boundList.Invoke(new Func<Task>(() => PopulateListViewAsync(path)));
                return;
            }

            try
            {
                _boundList.Cursor = Cursors.WaitCursor;
                _boundList.BeginUpdate();

                // Store selected items before clearing
                var selectedItemsInfo = _boundList.SelectedItems.Cast<ListViewItem>().Where(i => i.Tag is RepositoryContent rc).Select(i => new { Path = GitHub.FileSystem.GetParent(((RepositoryContent)i.Tag).Path), Name = i.Text }).ToList();

                if (_boundList.Columns.Count == 0)
                {
                    Program.Log?.Write(LogEventLevel.Information, "PopulateListViewAsync initialized columns");

                    _boundList.Columns.Add(Program.Lang.Strings.GitHubStrings.Explorer_DetailsHeader_Name, 230);
                    _boundList.Columns.Add(Program.Lang.Strings.GitHubStrings.Explorer_DetailsHeader_Type, 200);
                    _boundList.Columns.Add(Program.Lang.Strings.GitHubStrings.Explorer_DetailsHeader_Size, 80);
                    _boundList.Columns.Add("MD5", 120);
                    _boundList.Columns.Add(Program.Lang.Strings.GitHubStrings.Explorer_DetailsHeader_URL, 220);
                }

                if (!Cache.Contains(path))
                {
                    Program.Log?.Write(LogEventLevel.Warning, $"PopulateListViewAsync: DirectoryMap has no entry for '{path}'");
                    _boundList.Items.Clear();
                    return;
                }

                _boundList.Items.Clear();

                int count = 0;
                foreach (Entry entry in Cache.GetSubEntries(NormalizePath(path), sort: Cache.EntrySort.Default))
                {
                    if (entry.Content is null) continue;

                    if (cts is not null && cts.Token.IsCancellationRequested) return;

                    bool isHidden = entry.Name.StartsWith(".");
                    if (!isHidden || isHidden && showHiddenFiles)
                    {
                        ListViewItem item = new(entry.Name) { Tag = entry.Content };

                        item.SubItems.Add(FileTypeProvider?.Invoke(entry.Content) ?? Program.Lang.Strings.Extensions.File);

                        long size = entry.Type == EntryType.Dir && Cache.Contains(entry.Path) ? Cache.GetSize(entry.Path) : entry.Size;

                        item.SubItems.Add(size.ToStringFileSize());
                        item.SubItems.Add(Cache.ShaToMd5(entry.Content.Sha).ToUpper());
                        item.SubItems.Add(entry.Content.HtmlUrl);

                        if (entry.Type == EntryType.Dir) item.ImageKey = "folder";
                        else if (entry.Name.EndsWith(".wpth", StringComparison.OrdinalIgnoreCase)) item.ImageKey = "wpth";
                        else if (entry.Name.EndsWith(".wptp", StringComparison.OrdinalIgnoreCase)) item.ImageKey = "wptp";
                        else if (!string.IsNullOrWhiteSpace(entry.Name))
                        {
                            string ext = GetExtension(entry.Name);
                            if (!string.IsNullOrWhiteSpace(ext))
                            {
                                if (!_boundList.SmallImageList.Images.ContainsKey(ext))
                                {
                                    using (Icon ico = NativeMethods.Shell32.GetIconFromExtension(ext, NativeMethods.Shell32.IconSize.Small))
                                    using (Icon ico_resized = ico?.FromSize(16))
                                    using (Bitmap bmp = ico_resized?.ToBitmap())
                                    {
                                        _boundList.AddImagesToSmallImageList(new List<(Image, string)> { (bmp, ext) });
                                    }
                                    ProcessGhostIcons(_boundList.SmallImageList);
                                }

                                if (!_boundList.LargeImageList.Images.ContainsKey(ext))
                                {
                                    using (Icon ico = NativeMethods.Shell32.GetIconFromExtension(ext))
                                    using (Icon ico_resized = ico?.FromSize(48))
                                    {
                                        _boundList.LargeImageList.Images.Add(ext, ico_resized?.ToBitmap());
                                        ProcessGhostIcons(_boundList.LargeImageList);
                                    }
                                }

                                item.ImageKey = ext;
                            }
                            else
                            {
                                item.ImageKey = "file";
                            }
                        }
                        else item.ImageKey = "file";

                        if (isHidden) item.ImageKey = $"ghost_{item.ImageKey}";

                        _boundList.Items.Add(item);

                        // Restore selection if matches old path & name (with blue focus)
                        if (selectedItemsInfo.Any(si => string.Equals(si.Path, GitHub.FileSystem.GetParent(entry.Path), StringComparison.OrdinalIgnoreCase) && string.Equals(si.Name, entry.Name, StringComparison.OrdinalIgnoreCase)))
                        {
                            item.Selected = true;
                            item.EnsureVisible();
                            _boundList.HideSelection = false;
                            _boundList.Focus();
                        }
                    }

                    count++;
                    if (count % 50 == 0) await Task.Yield();
                }
            }
            catch (Exception ex)
            {
                Forms.BugReport.ThrowError(ex);
                Program.Log?.Write(LogEventLevel.Error, $"PopulateListViewAsync failed for '{path}'", ex);
            }
            finally
            {
                _boundList.HideSelection = false;
                _boundList?.EndUpdate();
                _boundList?.Cursor = Cursors.Default;
                Program.Log?.Write(LogEventLevel.Information, $"PopulateListViewAsync finished for '{path}'");
            }
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
        public static void UpdateTreeNode(string path, bool selectAfterUpdate)
        {
            try
            {
                if (_boundTree.Nodes.Count == 0) return;

                TreeNode rootNode = _boundTree.Nodes[0];
                if (rootNode.Tag == null) rootNode.Tag = _root;

                path = path?.Trim().TrimEnd('/');

                if (string.Equals(path, _root, StringComparison.OrdinalIgnoreCase))
                {
                    PruneDeletedNodes(rootNode);
                    _boundTree.SelectedNode = rootNode;
                    rootNode.EnsureVisible();
                    return;
                }

                if (!path.StartsWith(_root + "/", StringComparison.OrdinalIgnoreCase)) path = _root + "/" + path.TrimStart('/');

                string relativePath = path.Substring(_root.Length + 1);
                string[] segments = relativePath.Split('/');

                TreeNode currentNode = rootNode;

                PruneDeletedNodes(rootNode);

                foreach (string segment in segments)
                {
                    foreach (TreeNode child in currentNode.Nodes.Cast<TreeNode>().ToList())
                    {
                        string childPath = child.Tag as string;
                        if (!Cache.Contains(childPath))
                        {
                            currentNode.Nodes.Remove(child);
                            Program.Log?.Write(LogEventLevel.Information, $"[Tree] Removed deleted node '{childPath}'");
                        }
                    }

                    TreeNode childNode = currentNode.Nodes.Cast<TreeNode>().FirstOrDefault(n => string.Equals(n.Text, segment, StringComparison.OrdinalIgnoreCase));

                    if (childNode == null)
                    {
                        string parentPath = currentNode.Tag as string ?? _root;
                        string fullPath = parentPath + "/" + segment;

                        childNode = new TreeNode(segment) { Tag = fullPath, ImageKey = "folder", SelectedImageKey = "folder" };
                        currentNode.Nodes.Add(childNode);

                        Program.Log?.Write(LogEventLevel.Information, $"[Tree] Created node '{fullPath}'");
                    }

                    currentNode.Expand();
                    currentNode = childNode;
                }

                PruneDeletedNodes(rootNode);

                if (selectAfterUpdate)
                {
                    _boundTree.SelectedNode = currentNode;
                    Program.Log?.Write(LogEventLevel.Information, $"[Tree] Selected '{currentNode.Tag}'");
                }

                currentNode.EnsureVisible();
            }
            catch (Exception ex)
            {
                Program.Log?.Write(LogEventLevel.Error, "[Tree] UpdateTreeNode failed", ex);
                throw;
            }
        }

        #endregion

        #region List/Tree views helpers

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
        /// Initializes the specified tree view control with nodes representing the hierarchical structure of the root
        /// object.
        /// </summary>
        /// <remarks>Call this method to refresh the tree view's contents to reflect the current state of
        /// the root object. The method clears any existing nodes before rebuilding the tree. The operation is performed
        /// within a batch update to minimize UI flicker.</remarks>
        /// <param name="tree">The tree view control to populate with nodes. Must not be null.</param>
        private static void BuildTree()
        {
            Program.Log?.Write(LogEventLevel.Information, "BuildTree started");

            _boundTree.BeginUpdate();
            _boundTree.Nodes.Clear();

            // Use last segment of _root as the root node text
            string rootText = _root.Split('/').Last();
            TreeNode root = new(rootText)
            {
                Tag = _root, // full path for navigation
                ImageKey = "home",
                SelectedImageKey = "home"
            };
            _boundTree.Nodes.Add(root);

            // Add children starting from parent of last segment
            string parentPath = string.Join("/", _root.Split('/').Take(_root.Split('/').Length - 1));
            AddChildren(root, _root); // still pass full _root, AddChildren will now add only children

            _boundTree.EndUpdate();
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

            parentPath = NormalizePath(parentPath).TrimEnd('/');

            if (!Cache.Contains(parentPath))
            {
                Program.Log?.Write(LogEventLevel.Information, $"AddChildren: no entries for '{parentPath}'");
                return;
            }

            foreach (var dir in Cache.GetSubEntries(NormalizePath(parentPath), Cache.EntryFilter.DirectoriesOnly))
            {
                TreeNode node = new(dir.Name)
                {
                    Tag = dir.Path,
                    ImageKey = "folder",
                    SelectedImageKey = "folder"
                };
                parentNode.Nodes.Add(node);
                AddChildren(node, dir.Path); // recurse
            }
        }

        /// <summary>
        /// Resets the specified tree view and list view by clearing all nodes and items, and deselecting any selected
        /// node in the tree view.
        /// </summary>
        /// <param name="tree">The tree view control whose nodes will be cleared and selection reset.</param>
        /// <param name="list">The list view control whose items will be cleared.</param>
        static void ResetTree()
        {
            _boundTree.Nodes.Clear();
            _boundTree.SelectedNode = null;
            _boundList.Items.Clear();
        }

        private static void PruneDeletedNodes(TreeNode node)
        {
            foreach (TreeNode child in node.Nodes.Cast<TreeNode>().ToList())
            {
                string childPath = child.Tag as string;
                if (!Cache.Contains(childPath))
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

        #endregion

        #region  Initializers

        /// <summary>
        /// Initializes and assigns image lists for the specified list and tree controls, configuring icon sizes for
        /// each control type.
        /// </summary>
        /// <remarks>This method sets up image lists with appropriate icon sizes for list and tree
        /// controls. After calling this method, the controls will display icons according to the assigned image lists.
        /// Existing image lists on the controls will be replaced.</remarks>
        /// <param name="list">The list view control to which small and large image lists will be assigned.</param>
        /// <param name="tree">The tree view control to which the image list will be assigned.</param>
        private static void InitializeImageLists()
        {
            int small = 16;
            int large = 48;

            _smallIcons = new ImageList { ImageSize = new Size(small, small), ColorDepth = ColorDepth.Depth32Bit };
            _largeIcons = new ImageList { ImageSize = new Size(large, large), ColorDepth = ColorDepth.Depth32Bit };
            _treeIcons = new ImageList { ImageSize = new Size(small, small), ColorDepth = ColorDepth.Depth32Bit };

            AddStockIcons(_smallIcons);
            AddStockIcons(_largeIcons);
            AddStockIcons(_treeIcons);

            AddSpecialIcons(_smallIcons, small);
            AddSpecialIcons(_largeIcons, large);

            ProcessGhostIcons(_smallIcons);
            ProcessGhostIcons(_largeIcons);

            _boundList.SmallImageList = _smallIcons;
            _boundList.LargeImageList = _largeIcons;
            _boundTree.ImageList = _treeIcons;
        }

        public static void Init_NewDirectory()
        {
            ListViewItem item = new(GetAvailableItemText(Program.Lang.Strings.Extensions.NewFolder))
            {
                ImageKey = "folder",
                Tag = "NEWFOLDER_PENDING",
                Selected = true,
                Focused = true
            };

            _boundList.Items.Add(item);
            item?.Selected = true;
            item?.Focused = true;
            item?.BeginEdit();
        }

        public static void Init_Cut()
        {
            if (_boundList.SelectedItems.Count == 0) return;

            copiedItems?.Clear();
            cutItems?.Clear();

            // Start with selected items
            var itemsToCut = new HashSet<ListViewItem>(_boundList.SelectedItems.Cast<ListViewItem>());

            // Build lookup for all files in ListView
            var allFiles = _boundList.Items.Cast<ListViewItem>().Where(i => i.Tag is RepositoryContent rc && rc.Type == ContentType.File).ToDictionary(i => i.Text, i => i, StringComparer.OrdinalIgnoreCase);

            foreach (ListViewItem item in _boundList.SelectedItems)
            {
                if (item.Tag is not RepositoryContent rc || rc.Type != ContentType.File) continue;

                string ext = Path.GetExtension(rc.Name).ToLowerInvariant();
                if (ext != ".wpth" && ext != ".wptp") continue;

                string baseName = Path.GetFileNameWithoutExtension(rc.Name);
                string linkedExt = ext == ".wpth" ? ".wptp" : ".wpth";
                string linkedName = baseName + linkedExt;

                if (allFiles.TryGetValue(linkedName, out var linkedItem))
                {
                    // Skip if already selected / included
                    if (!itemsToCut.Contains(linkedItem))
                    {
                        // Confirmation or Auto-apply
                        if (FilesOperationsLinking && (Program.Settings.UsersServices.GitHub_AutoOperateOnLinkedFiles || Forms.GitHub_LinkedFilesConfirmation.ShowDialog(baseName, GitHub_LinkedFilesConfirmation.Operation.Cut) == DialogResult.Yes))
                        {
                            itemsToCut.Add(linkedItem);
                        }
                    }
                }
            }

            cutItems = [.. itemsToCut];

            // Update ghost icons for cut items
            foreach (var cutItem in cutItems)
            {
                if (cutItem != null && !cutItem.ImageKey.StartsWith("ghost", StringComparison.OrdinalIgnoreCase)) cutItem.ImageKey = $"ghost_{cutItem.ImageKey}";
            }

            // Restore ghost icons for items that are not cut
            foreach (var notCutItem in _boundList.Items.Cast<ListViewItem>().Where(i => !cutItems.Contains(i) && !i.Text.StartsWith(".") && i.ImageKey.StartsWith("ghost", StringComparison.OrdinalIgnoreCase)))
            {
                notCutItem.ImageKey = notCutItem.ImageKey.Replace("ghost_", string.Empty);
            }

            CanPasteChanged?.Invoke(null, CanPaste);
        }

        public static void Init_Copy()
        {
            // Restore ghost icons
            foreach (ListViewItem item in _boundList.Items.Cast<ListViewItem>().Where(i => !i.Text.StartsWith(".") && i.ImageKey.StartsWith("ghost", StringComparison.OrdinalIgnoreCase)))
            {
                item.ImageKey = item.ImageKey.Replace("ghost_", string.Empty);
            }

            if (_boundList.SelectedItems.Count > 0)
            {
                cutItems?.Clear();
                copiedItems?.Clear();

                // Start with selected items
                var itemsToCopy = new HashSet<ListViewItem>(_boundList.SelectedItems.Cast<ListViewItem>());

                // Build lookup for all files in ListView
                var allFiles = _boundList.Items.Cast<ListViewItem>().Where(i => i.Tag is RepositoryContent rc && rc.Type == ContentType.File).ToDictionary(i => i.Text, i => i, StringComparer.OrdinalIgnoreCase);

                foreach (ListViewItem item in _boundList.SelectedItems)
                {
                    if (item.Tag is not RepositoryContent rc || rc.Type != ContentType.File) continue;

                    string ext = Path.GetExtension(rc.Name).ToLowerInvariant();
                    if (ext != ".wpth" && ext != ".wptp") continue;

                    string baseName = Path.GetFileNameWithoutExtension(rc.Name);
                    string linkedExt = ext == ".wpth" ? ".wptp" : ".wpth";
                    string linkedName = baseName + linkedExt;

                    if (allFiles.TryGetValue(linkedName, out var linkedItem))
                    {
                        // Skip if already selected / included
                        if (!itemsToCopy.Contains(linkedItem))
                        {
                            // Confirmation or Auto-apply
                            if (FilesOperationsLinking && (Program.Settings.UsersServices.GitHub_AutoOperateOnLinkedFiles || Forms.GitHub_LinkedFilesConfirmation.ShowDialog(baseName, GitHub_LinkedFilesConfirmation.Operation.Copy) == DialogResult.Yes))
                            {
                                itemsToCopy.Add(linkedItem);
                            }
                        }
                    }
                }

                copiedItems = [.. itemsToCopy];
            }

            CanPasteChanged?.Invoke(null, CanPaste);
        }

        #region Context Menus Initialization

        private static void InitializeMenus()
        {
            InitializeMenu_Global();
            InitializeMenu_Item();
        }

        private static void InitializeMenu_Global()
        {
            // Create view menu items dynamically
            menu_view = new ToolStripMenuItem(Program.Lang.Strings.GitHubStrings.Explorer_View)
            {
                DropDown = new UI.WP.ContextMenuStrip() { ShowImageMargin = true }
            };

            foreach (var view in Views)
            {
                ToolStripMenuItem item = new(view.label, view.icon)
                {
                    CheckOnClick = true,
                    Checked = _boundList.View == view.view,
                    Tag = view
                };

                item.Click -= Menu_ViewItem_Click;
                item.Click += Menu_ViewItem_Click;

                menu_view.DropDown.Items.Add(item);
            }

            separator_0 = new ToolStripSeparator();
            separator_1 = new ToolStripSeparator();
            separator_2 = new ToolStripSeparator();

            menu_paste = new ToolStripMenuItem(Program.Lang.Strings.General.Paste) { Enabled = false };
            menu_paste.Click -= Menu_paste_Click;
            menu_paste.Click += Menu_paste_Click;

            menu_newFolder = new ToolStripMenuItem(Program.Lang.Strings.Extensions.Folder, Assets.GitHubMgr.folder_web_48.Resize(16, 16));
            menu_newFolder.Click -= Menu_NewFolder_Click;
            menu_newFolder.Click += Menu_NewFolder_Click;

            using (Icon ico = Properties.Resources.fileextension.FromSize(20))
            {
                menu_newTheme = new ToolStripMenuItem(Program.Lang.Strings.Extensions.WinPaletterThemeFile, ico.ToBitmap());
            }

            menu_newItem = new ToolStripMenuItem(Program.Lang.Strings.General.New)
            {
                DropDown = new UI.WP.ContextMenuStrip() { ShowImageMargin = true }
            };
            menu_newItem.DropDown.Items.AddRange([menu_newFolder, menu_newTheme]);
            menu_newTheme.Click -= Menu_newTheme_Click;
            menu_newTheme.Click += Menu_newTheme_Click;

            menu_properties = new ToolStripMenuItem(Program.Lang.Strings.GitHubStrings.Explorer_Properties);

            contextMenu_all.Items.AddRange(
            [
                menu_view,
                separator_0,
                menu_paste,
                separator_1,
                menu_newItem,
                separator_2,
                menu_properties
            ]);
        }

        private static void InitializeMenu_Item()
        {
            menu_Open = new ToolStripMenuItem(Program.Lang.Strings.General.Open, Assets.GitHubMgr.folder_web_16);
            menu_Open.Click -= Menu_Open_Click;
            menu_Open.Click += Menu_Open_Click;

            menu_Download = new ToolStripMenuItem(Program.Lang.Strings.General.Download, Assets.GitHubMgr.ContextMenu_Download);
            menu_Download.Click -= Menu_Download_Click;
            menu_Download.Click += Menu_Download_Click;

            separator_item_1 = new ToolStripSeparator();

            menu_CopyPath = new ToolStripMenuItem(Program.Lang.Strings.General.Copy_AsPath);
            menu_CopyPath.Click -= Menu_CopyPath_Click;
            menu_CopyPath.Click += Menu_CopyPath_Click;

            menu_CopyURL = new ToolStripMenuItem(Program.Lang.Strings.General.Copy_URL);
            menu_CopyURL.Click -= Menu_CopyURL_Click;
            menu_CopyURL.Click += Menu_CopyURL_Click;

            menu_Copy = new ToolStripMenuItem(Program.Lang.Strings.General.Copy);
            menu_Copy.Click -= Menu_Copy_Click;
            menu_Copy.Click += Menu_Copy_Click;

            menu_Cut = new ToolStripMenuItem(Program.Lang.Strings.General.Cut);
            menu_Cut.Click -= Menu_Cut_Click;
            menu_Cut.Click += Menu_Cut_Click;

            separator_item_2 = new ToolStripSeparator();

            menu_Delete = new ToolStripMenuItem(Program.Lang.Strings.General.Delete);
            menu_Delete.Click -= Menu_Delete_Click;
            menu_Delete.Click += Menu_Delete_Click;

            menu_Rename = new ToolStripMenuItem(Program.Lang.Strings.General.Rename);
            menu_Rename.Click -= Menu_Rename_Click;
            menu_Rename.Click += Menu_Rename_Click;

            separator_item_3 = new ToolStripSeparator();

            menu_item_properties = new ToolStripMenuItem(Program.Lang.Strings.GitHubStrings.Explorer_Properties);
            menu_item_properties.Click -= Menu_ItemProperties_Click;
            menu_item_properties.Click += Menu_ItemProperties_Click;

            contextMenu_item.Items.AddRange(
            [
                menu_Open,
                menu_Download,
                separator_item_1,
                menu_CopyPath,
                menu_CopyURL,
                menu_Copy,
                menu_Cut,
                separator_item_2,
                menu_Delete,
                menu_Rename,
                separator_item_3,
                menu_item_properties
            ]);
        }

        #endregion

        #endregion

        #region Events

        public static event EventHandler<bool> CanPasteChanged;

        public static event EventHandler<bool> CanDoIOChanged;

        public static event EventHandler<string> Navigated;

        public static event EventHandler<string> StatusLabelChanged;

        /// <summary>
        /// Attaches event handlers to the specified tree and list controls for navigation and interaction.
        /// </summary>
        /// <param name="tree"></param>
        /// <param name="list"></param>
        private static void HookEvents()
        {
            _boundTree.AfterSelect -= Tree_AfterSelect;
            _boundList.DoubleClick -= List_DoubleClick;
            _boundList.MouseUp -= List_MouseUp;
            _boundList.BeforeLabelEdit -= List_BeforeLabelEdit;
            _boundList.AfterLabelEdit -= List_AfterLabelEdit;
            _boundList.KeyUp -= List_KeyUp;
            _boundList.SelectedIndexChanged -= ListView_SelectedIndexChanged;
            _boundList.ItemChecked -= ListView_ItemChecked;
            _boundList.ItemActivate -= ListView_ItemActivate;

            _boundTree.AfterSelect += Tree_AfterSelect;
            _boundList.DoubleClick += List_DoubleClick;
            _boundList.MouseUp += List_MouseUp;
            _boundList.BeforeLabelEdit += List_BeforeLabelEdit;
            _boundList.AfterLabelEdit += List_AfterLabelEdit;
            _boundList.KeyUp += List_KeyUp;
            _boundList.SelectedIndexChanged += ListView_SelectedIndexChanged;
            _boundList.ItemChecked += ListView_ItemChecked;
            _boundList.ItemActivate += ListView_ItemActivate;

            Program.Log?.Write(LogEventLevel.Information, "HookEvents attached");
        }

        private static async void Tree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node == null || _boundList == null || _boundTree == null) return;

            string newPath = e.Node.Tag as string;
            if (string.IsNullOrEmpty(newPath)) return;

            // If the user selects the currently shown folder, do nothing
            if (newPath == CurrentPath) return;

            // Navigate properly: update history
            await NavigateTo(newPath, true);
        }

        public static async void List_DoubleClick(object sender, EventArgs e)
        {
            if (_boundList == null || _boundTree == null) return;
            if (_boundList.SelectedItems.Count == 0) return;
            if (_boundList.SelectedItems[0].Tag is not RepositoryContent entry) return;
            if (entry.Type.Value != Octokit.ContentType.Dir) return;

            _lastEnteredFolderPath = entry.Path;

            // Navigate normally, history will be updated inside NavigateTo
            await NavigateTo(entry.Path, addToHistory: true);
        }

        private static void List_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                // Hit test to check if mouse is over an item
                var info = _boundList.HitTest(e.Location);
                if (info.Item != null)
                {
                    // Right-click on an item
                    if (_boundList.SelectedItems.Count > 0)
                    {
                        RepositoryContent rc = _boundList.SelectedItems[0]?.Tag as RepositoryContent;
                        menu_Open.Enabled = rc?.Type == ContentType.Dir;
                    }
                    else
                    {
                        menu_Open.Enabled = false;
                    }
                    contextMenu_item.Show(_boundList, e.Location);
                }
                else
                {
                    // Right-click on empty space
                    menu_paste.Enabled = CanPaste;
                    contextMenu_all.Show(_boundList, e.Location);
                }
            }
        }

        private static async void List_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            ListViewItem item = _boundList.Items[e.Item];

            string newName;
            if (e.Label == null)
            {
                // return will cancel the operation
                // but Windows continues creating the folder with the same name
                // so we will simulate what Windows does

                newName = item.Text;
            }
            else
            {
                newName = e.Label.Trim();
            }

            if (string.IsNullOrWhiteSpace(newName))
            {
                e.CancelEdit = true;
                return;
            }
            else if (!GitHub.FileSystem.IsValidGitWindowsUrlSafeName(newName))
            {
                Program.ToolTip.Show(_boundTree, Program.Lang.Strings.GitHubStrings.Explorer_InvalidCharToolTip,
                    $"{GitHub.FileSystem.InvalidCharsToolTip}\r\n{GitHub.FileSystem.InvalidNamesToolTip}",
                    Properties.Resources.checker_disabled, item.Bounds.Location);

                item?.Remove();
                e.CancelEdit = true;
                return;
            }
            else if (_boundList.Items.Cast<ListViewItem>().Any(i => i != item && string.Equals(i.Text, newName, StringComparison.OrdinalIgnoreCase)))
            {
                Program.ToolTip.Show(_boundTree, Program.Lang.Strings.GitHubStrings.Explorer_EntryExists, string.Empty, Properties.Resources.checker_disabled, item.Bounds.Location);

                item?.Remove();
                e.CancelEdit = true;
                return;
            }

            if (item.Tag is string str_tag && str_tag == "NEWFOLDER_PENDING")
            {
                item.Text = newName;
                NewDirectory(item, e);
            }
            else
            {
                item.Text = newName;
            }

            if (item.Tag is RepositoryContent content)
            {
                if (content.Type == ContentType.Dir)
                {
                    RenameDirectory(item, e);
                }
                else if (content.Type == ContentType.File)
                {
                    string oldText = content.Name;
                    string oldBaseName = Path.GetFileNameWithoutExtension(oldText);
                    string newBaseName = Path.GetFileNameWithoutExtension(newName);
                    string ext = Path.GetExtension(oldText);
                    string parentDirPath = FileSystem.CurrentPath;

                    // Determine linked extension
                    string linkedExt = ext.Equals(".wpth", StringComparison.OrdinalIgnoreCase) ? ".wptp"
                                     : ext.Equals(".wptp", StringComparison.OrdinalIgnoreCase) ? ".wpth"
                                     : null;

                    if (linkedExt != null)
                    {
                        // Find linked item BEFORE any rename
                        ListViewItem linkedItem = FilesOperationsLinking ? _boundList.Items.Cast<ListViewItem>()
                            .FirstOrDefault(i =>
                            {
                                if (i.Tag is RepositoryContent rc)
                                {
                                    string rcBase = Path.GetFileNameWithoutExtension(rc.Name);
                                    string rcExt = Path.GetExtension(rc.Name);
                                    bool match = rcBase.Equals(oldBaseName, StringComparison.OrdinalIgnoreCase) && rcExt.Equals(linkedExt, StringComparison.OrdinalIgnoreCase);
                                    if (match) return match;
                                }
                                return false;
                            }) : null;

                        item.Text = newName;
                        await RenameFile(item, parentDirPath, e);

                        if (linkedItem is not null && newName != oldText && (Program.Settings.UsersServices.GitHub_AutoOperateOnLinkedFiles || Forms.GitHub_LinkedFilesConfirmation.ShowDialog(oldBaseName, newBaseName, GitHub_LinkedFilesConfirmation.Operation.Rename) == DialogResult.Yes))
                        {
                            string linkedNewName = $"{newBaseName}{linkedExt}";
                            linkedItem.Text = linkedNewName;
                            await RenameFile(linkedItem, parentDirPath);
                        }
                    }
                    else
                    {
                        // Just rename main item
                        item.Text = newName;
                        await RenameFile(item, parentDirPath, e);
                    }
                }
            }
        }

        private static async void List_KeyUp(object sender, KeyEventArgs e)
        {
            ListViewItem item = _boundList.SelectedItems.Count > 0 ? _boundList.SelectedItems[0] : null;

            if (e.KeyCode == Keys.Enter)
            {
                if (itemBeingEdited is not null && item is not null)
                {
                    item?.EndEdit();
                    e.Handled = true; // prevent beep sound
                }
                else if (_boundList.SelectedItems.Count > 0 && item.Tag is RepositoryContent rc && rc.Type == ContentType.Dir)
                {
                    // Navigate into directory
                    await GitHub.FileSystem.NavigateTo(rc.Path);
                }

                itemBeingEdited = null;
            }
            else if (e.KeyCode == Keys.F2 || (e.Control && e.KeyCode == Keys.R))
            {
                // Rename selected file or directory
                item?.BeginEdit();
            }
            else if (e.KeyCode == Keys.Back || (e.Alt && e.KeyCode == Keys.Left))
            {
                // Navigate backward
                if (FileSystem.CanGoBack) await GitHub.FileSystem.GoBack();
            }
            else if (e.Alt && e.KeyCode == Keys.Right)
            {
                // Navigate forward
                if (FileSystem.CanGoForward) await GitHub.FileSystem.GoForward();
            }
            else if (e.Control && e.KeyCode == Keys.A)
            {
                // Select all items
                foreach (ListViewItem childItem in _boundList.Items) childItem.Selected = true;
            }
            else if (e.Control && e.KeyCode == Keys.N)
            {
                // Initiate new directory creation
                Init_NewDirectory();
            }
            else if (e.KeyCode == Keys.Delete)
            {
                // Delete selected file or directory
                ActionQueue.Enqueue(DeleteSelectedElementsAsync);
            }
            else if (e.KeyCode == Keys.F5)
            {
                // Refresh current directory
                await RefreshAsync();
            }
            else if (e.Control && e.KeyCode == Keys.C)
            {
                // Copy selected item(s)
                Init_Copy();
            }
            else if (e.Control && e.KeyCode == Keys.X)
            {
                // Cut selected item(s)
                Init_Cut();
            }
            else if (e.Control && e.KeyCode == Keys.V)
            {
                // Paste item(s)
                ActionQueue.Enqueue(Paste);
            }
            else if (e.Control && e.KeyCode == Keys.Shift && e.KeyCode == Keys.N)
            {
                // Create new file
            }
            else if (e.Alt && e.KeyCode == Keys.Enter)
            {
                // Show properties/details of selected item
            }
            else if (e.Control && e.KeyCode == Keys.F)
            {
                // Search in current directory
            }
        }

        private static void List_BeforeLabelEdit(object sender, LabelEditEventArgs e)
        {
            itemBeingEdited = _boundList.Items[e.Item];
            SelectLabelEditWithoutExtension(itemBeingEdited);
        }

        private static void ListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            StatusLabelChanged?.Invoke(null, FileSystem.StatusLabel);
            CanDoIOChanged?.Invoke(null, _boundList.SelectedItems.Count > 0);
        }

        private static void ListView_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            StatusLabelChanged?.Invoke(null, FileSystem.StatusLabel);
        }

        private static void ListView_ItemActivate(object sender, EventArgs e)
        {
            StatusLabelChanged?.Invoke(null, FileSystem.StatusLabel);
        }

        #region Context Menus Event Handlers

        private static void Menu_ViewItem_Click(object sender, EventArgs e)
        {
            if (sender is not ToolStripMenuItem item) return;

            foreach (ToolStripMenuItem other in menu_view.DropDown.Items) if (other != item) other.Checked = false;

            (string label, Bitmap icon, Bitmap glyph, View view) viewData = ((string label, Bitmap icon, Bitmap glyph, View view))item.Tag;
            _boundList.View = viewData.view;
            Forms.GitHub_Mgr.UpdateViewButton(viewData);
            item.Checked = true;
        }

        private static void Menu_NewFolder_Click(object sender, EventArgs e) => Init_NewDirectory();

        private static void Menu_paste_Click(object sender, EventArgs e) => ActionQueue.Enqueue(Paste);

        private static void Menu_Open_Click(object sender, EventArgs e) => FileSystem.List_DoubleClick(_boundList, new());

        private static void Menu_CopyPath_Click(object sender, EventArgs e)
        {
            if (_boundList.SelectedItems.Count > 0)
            {
                var rc = _boundList.SelectedItems[0].Tag as RepositoryContent;
                Clipboard.SetText(rc.Path);
            }
        }

        private static void Menu_CopyURL_Click(object sender, EventArgs e)
        {
            if (_boundList.SelectedItems.Count > 0)
            {
                var rc = _boundList.SelectedItems[0].Tag as RepositoryContent;
                Clipboard.SetText(rc.HtmlUrl);
            }
        }

        private static void Menu_Copy_Click(object sender, EventArgs e) => Init_Copy();

        private static void Menu_Cut_Click(object sender, EventArgs e) => Init_Cut();

        private static async void Menu_Delete_Click(object sender, EventArgs e) => ActionQueue.Enqueue(DeleteSelectedElementsAsync);

        private static void Menu_Rename_Click(object sender, EventArgs e) => _boundList.SelectedItems[0]?.BeginEdit();

        private static async void Menu_ItemProperties_Click(object sender, EventArgs e)
        {
            if (_boundList.SelectedItems.Count > 0)
            {
                Forms.GitHub_EntryProperties.Load_Entry(_boundList.SelectedItems[0]);
            }
        }

        private async static void Menu_Download_Click(object sender, EventArgs e)
        {
            string selectedPath = string.Empty;

            if (!OS.WXP)
            {
                using (VistaFolderBrowserDialog FD = new())
                {
                    if (FD.ShowDialog() == DialogResult.OK) selectedPath = FD.SelectedPath;
                }
            }
            else
            {
                using (FolderBrowserDialog FD = new())
                {
                    if (FD.ShowDialog() == DialogResult.OK) selectedPath = FD.SelectedPath;
                }
            }

            if (!string.IsNullOrEmpty(selectedPath) && !Directory.Exists(selectedPath)) { Directory.CreateDirectory(selectedPath); }

            await DownloadSelectedItemsAsync(selectedPath);
        }

        private static void Menu_newTheme_Click(object sender, EventArgs e)
        {
            Forms.GitHub_ThemeUpload.ShowDialog();
        }

        #endregion

        #endregion

        #region Files\Directories operations

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
        public static async Task NavigateTo(string newPath, bool addToHistory = true)
        {
            if (string.IsNullOrEmpty(newPath)) return;
            bool willPlayAudio = CurrentPath is not null && !CurrentPath.Equals(newPath, StringComparison.OrdinalIgnoreCase);

            // Ensure path starts with root
            if (!newPath.StartsWith(_root, StringComparison.OrdinalIgnoreCase))
                newPath = _root + "/" + newPath.TrimStart('/');

            string resolvedPath = newPath;
            while (!Cache.Contains(resolvedPath) && !string.Equals(resolvedPath, _root, StringComparison.OrdinalIgnoreCase))
                resolvedPath = UppermostRoot(resolvedPath) ?? _root;

            if (string.Equals(resolvedPath, CurrentPath, StringComparison.OrdinalIgnoreCase))
            {
                if (addToHistory) forwardStack.Clear();
                return;
            }

            // Remember currently selected item's Path
            string selectedPath = _boundList.SelectedItems.Count > 0 && _boundList.SelectedItems[0].Tag is RepositoryContent rc
                ? rc.Path
                : null;

            if (addToHistory)
            {
                backStack.Push(new NavigationState
                {
                    Path = CurrentPath,
                    SelectedItemPath = selectedPath
                });
                forwardStack.Clear();
            }

            CurrentPath = resolvedPath;
            Navigated?.Invoke(null, CurrentPath);

            await PopulateListViewAsync(CurrentPath);

            // Restore previously selected item by Path
            if (!string.IsNullOrEmpty(selectedPath))
            {
                ListViewItem itemToSelect = _boundList.Items
                    .Cast<ListViewItem>()
                    .FirstOrDefault(i => i.Tag is RepositoryContent rc && rc.Path == selectedPath);

                if (itemToSelect != null)
                {
                    itemToSelect.Selected = true;
                    itemToSelect.EnsureVisible();
                    _boundList.HideSelection = false;
                    _boundList.Focus();
                }
            }

            UpdateTreeNode(CurrentPath, true);

            if (willPlayAudio) CustomSystemSounds.Navigation.Play();
        }

        /// <summary>
        /// Go back in history.
        /// </summary>
        public static async Task GoBack()
        {
            if (!CanGoBack) return;

            var previousState = backStack.Pop();
            forwardStack.Push(new NavigationState
            {
                Path = CurrentPath,
                SelectedItemPath = _boundList.SelectedItems.Count > 0 && _boundList.SelectedItems[0].Tag is RepositoryContent rc
                    ? rc.Path
                    : null
            });

            await NavigateTo(previousState.Path, addToHistory: false);

            // Restore selection by Path
            if (!string.IsNullOrEmpty(previousState.SelectedItemPath))
            {
                ListViewItem itemToSelect = _boundList.Items
                    .Cast<ListViewItem>()
                    .FirstOrDefault(i => i.Tag is RepositoryContent rc && rc.Path == previousState.SelectedItemPath);

                if (itemToSelect != null)
                {
                    itemToSelect.Selected = true;
                    itemToSelect.EnsureVisible();
                    _boundList.HideSelection = false;
                    _boundList.Focus();
                }
            }


        }

        /// <summary>
        /// Go forward in history.
        /// </summary>
        public static async Task GoForward()
        {
            if (!CanGoForward) return;

            var nextState = forwardStack.Pop();
            backStack.Push(new NavigationState
            {
                Path = CurrentPath,
                SelectedItemPath = _boundList.SelectedItems.Count > 0 && _boundList.SelectedItems[0].Tag is RepositoryContent rc
                    ? rc.Path
                    : null
            });

            await NavigateTo(nextState.Path, addToHistory: false);

            // Restore selection by Path
            if (!string.IsNullOrEmpty(nextState.SelectedItemPath))
            {
                ListViewItem itemToSelect = _boundList.Items
                    .Cast<ListViewItem>()
                    .FirstOrDefault(i => i.Tag is RepositoryContent rc && rc.Path == nextState.SelectedItemPath);

                if (itemToSelect != null)
                {
                    itemToSelect.Selected = true;
                    itemToSelect.EnsureVisible();
                    _boundList.HideSelection = false;
                    _boundList.Focus();
                }
            }
        }

        /// <summary>
        /// Navigate to parent directory.
        /// </summary>
        public static async Task GoUp()
        {
            if (!CanGoUp) return;

            string parent = GetParent(CurrentPath) ?? _root;

            // Push current state to back stack
            backStack.Push(new NavigationState
            {
                Path = CurrentPath,
                SelectedItemPath = _boundList.SelectedItems.Count > 0 && _boundList.SelectedItems[0].Tag is RepositoryContent rc
                    ? rc.Path
                    : null
            });

            forwardStack.Clear();

            await NavigateTo(parent, addToHistory: false);

            // After moving up, select the folder we came from using full path
            if (!string.IsNullOrEmpty(_lastEnteredFolderPath))
            {
                ListViewItem itemToSelect = _boundList.Items
                    .Cast<ListViewItem>()
                    .FirstOrDefault(i => i.Tag is RepositoryContent rc && rc.Path == _lastEnteredFolderPath);

                if (itemToSelect != null)
                {
                    itemToSelect.Selected = true;
                    itemToSelect.EnsureVisible();
                    _boundList.HideSelection = false;
                    _boundList.Focus();
                }
            }
        }

        static async void NewDirectory(ListViewItem item, LabelEditEventArgs e = null)
        {
            _boundbreadcrumbControl.StartMarquee();

            string initPath = FileSystem.CurrentPath;
            string itemText = item.Text.Trim();
            string path = $"{GitHub.FileSystem.CurrentPath}/{itemText}";

            try
            {
                RepositoryContent dirContent = (await GitHub.FileSystem.CreateDirectoryAsync(path, cts)).Content;
                item.Tag = dirContent;

                if (initPath.Equals(FileSystem.CurrentPath, StringComparison.OrdinalIgnoreCase))
                {
                    await GitHub.FileSystem.UpdateExplorerView(CurrentPath);

                    _boundList.Items.Cast<ListViewItem>().Where(i => i.Tag is RepositoryContent && i.Text.Equals(itemText, StringComparison.OrdinalIgnoreCase)).ToList()
                        .ForEach(i => i.Selected = true);
                }
            }
            catch (Exception ex)
            {
                Forms.BugReport.ThrowError(ex);
                e?.CancelEdit = true;
            }
            finally
            {
                _boundbreadcrumbControl.StopMarquee();
            }
            return;
        }

        static async void RenameDirectory(ListViewItem item, LabelEditEventArgs e)
        {
            string initPath = FileSystem.CurrentPath;
            string oldPath = (item.Tag as RepositoryContent).Path;
            string itemText = item.Text.Trim();
            string newPath = oldPath.Substring(0, oldPath.LastIndexOf('/') + 1) + itemText;

            if (oldPath.Equals(newPath, StringComparison.OrdinalIgnoreCase)) return;

            _boundbreadcrumbControl.StartMarquee();

            try
            {
                item.Tag = (await GitHub.FileSystem.MoveDirectoryAsync(oldPath, newPath, $"Rename directory `{oldPath}` into `{itemText}` by {Repository.Owner}", cts)).Content;

                if (initPath.Equals(FileSystem.CurrentPath, StringComparison.OrdinalIgnoreCase))
                {
                    await GitHub.FileSystem.UpdateExplorerView(FileSystem.CurrentPath);

                    _boundList.Items.Cast<ListViewItem>().Where(i => i.Tag is RepositoryContent && i.Text.Equals(itemText, StringComparison.OrdinalIgnoreCase)).ToList()
                        .ForEach(i => i.Selected = true);
                }
            }
            catch
            {
                e.CancelEdit = true;
            }
            finally
            {
                _boundbreadcrumbControl.StopMarquee();
            }
        }

        static async Task RenameFile(ListViewItem item, string parentDirPath, LabelEditEventArgs e = null)
        {
            string oldPath = (item.Tag as RepositoryContent).Path;
            string itemText = item.Text.Trim();
            string newPath = $"{GitHub.FileSystem.GetParent(oldPath)}/{itemText}";

            if (oldPath.Equals(newPath, StringComparison.OrdinalIgnoreCase)) return;

            _boundbreadcrumbControl.StartMarquee();

            try
            {
                item.Tag = (await GitHub.FileSystem.MoveFileAsync(oldPath, newPath, $"Rename file `{oldPath}` into `{itemText}` by {Repository.Owner}", cts)).Content;

                if (parentDirPath.Equals(FileSystem.CurrentPath, StringComparison.OrdinalIgnoreCase))
                {
                    await GitHub.FileSystem.UpdateExplorerView(FileSystem.CurrentPath);

                    _boundList.Items.Cast<ListViewItem>().Where(i => i.Tag is RepositoryContent && i.Text.Equals(itemText, StringComparison.OrdinalIgnoreCase)).ToList()
                        .ForEach(i => i.Selected = true);
                }
            }
            catch (Exception ex)
            {
                Forms.BugReport.ThrowError(ex);
                e?.CancelEdit = true;
                Program.Log?.Write(Serilog.Events.LogEventLevel.Error, $"Error in renaming file {oldPath} to {itemText}", ex);
            }
            finally
            {
                _boundbreadcrumbControl.StopMarquee();
            }
        }

        public static async Task Paste()
        {
            if ((copiedItems?.Count ?? 0) > 0 || (cutItems?.Count ?? 0) > 0)
            {
                string initPath = GitHub.FileSystem.CurrentPath;
                string destDir = GitHub.FileSystem.NormalizePath(initPath);

                _boundbreadcrumbControl.Value = 0;
                _boundbreadcrumbControl.StartMarquee();

                // Helper to process items
                async Task ProcessItemsAsync(IList<ListViewItem> items, bool isCopy)
                {
                    _boundbreadcrumbControl.Value = 0;
                    _boundbreadcrumbControl.StartMarquee();

                    // Add initial placeholder items
                    List<ListViewItem> placeholderItems = [];
                    foreach (ListViewItem item in items)
                    {
                        ListViewItem fakeItem = new() { Text = item.Text, Tag = null, ImageKey = item.ImageKey };
                        placeholderItems.Add(fakeItem);
                        _boundList?.Items.Add(fakeItem);
                    }

                    // Separate files and directories
                    string[] filePaths = [.. items.Select(i => i.Tag as RepositoryContent).Where(rc => rc != null && rc.Type == ContentType.File).Select(rc => rc.Path)];
                    string[] dirPaths = [.. items.Select(i => i.Tag as RepositoryContent).Where(rc => rc != null && rc.Type == ContentType.Dir).Select(rc => rc.Path)];

                    // Files
                    if (filePaths.Length > 0)
                    {
                        if (isCopy)
                        {
                            await GitHub.FileSystem.CopyFilesAsync(filePaths, destDir, cts, progress =>
                            {
                                if (_boundbreadcrumbControl.IsMarquee) _boundbreadcrumbControl.StopMarquee();
                                _boundbreadcrumbControl.Value = progress;
                            });

                            if (!_boundbreadcrumbControl.IsMarquee) _boundbreadcrumbControl.StartMarquee();
                        }
                        else
                        {
                            await GitHub.FileSystem.MoveFilesAsync(filePaths, destDir, null, cts, progress =>
                            {
                                if (_boundbreadcrumbControl.IsMarquee) _boundbreadcrumbControl.StopMarquee();
                                _boundbreadcrumbControl.Value = progress;
                            });

                            if (!_boundbreadcrumbControl.IsMarquee) _boundbreadcrumbControl.StartMarquee();
                        }
                    }

                    // Directories
                    if (dirPaths.Length > 0)
                    {
                        if (isCopy)
                        {
                            await GitHub.FileSystem.CopyDirectoriesAsync(dirPaths, destDir, cts, progress =>
                            {
                                if (_boundbreadcrumbControl.IsMarquee) _boundbreadcrumbControl.StopMarquee();
                                _boundbreadcrumbControl.Value = progress;
                            });

                            if (!_boundbreadcrumbControl.IsMarquee) _boundbreadcrumbControl.StartMarquee();
                        }
                        else
                        {
                            await GitHub.FileSystem.MoveDirectoriesAsync(dirPaths, destDir, null, cts, progress =>
                            {
                                if (_boundbreadcrumbControl.IsMarquee) _boundbreadcrumbControl.StopMarquee();
                                _boundbreadcrumbControl.Value = progress;
                            });

                            if (!_boundbreadcrumbControl.IsMarquee) _boundbreadcrumbControl.StartMarquee();
                        }
                    }

                    // Update ListView items
                    foreach (ListViewItem item in items)
                    {
                        if (item.Tag is RepositoryContent rc)
                        {
                            string newPath = GitHub.FileSystem.NormalizePath($"{destDir}/{FileSystem.FileName(rc.Path)}");
                            var entry = await GitHub.FileSystem.GetInfoRefreshAsync(newPath, cts: cts);
                            item.Tag = entry?.Content;
                        }

                        if (!item.Text.StartsWith(".")) item.ImageKey = item.ImageKey.Replace("ghost_", string.Empty);
                        if (!isCopy) item.Remove();
                    }

                    // Remove placeholders that remain
                    foreach (var ph in placeholderItems.Where(p => p.Tag == null).ToList()) ph.Remove();

                    _boundbreadcrumbControl.StopMarquee();
                }

                // Process copied items
                if ((copiedItems?.Count ?? 0) > 0) await ProcessItemsAsync(copiedItems, isCopy: true);

                // Process cut items
                if ((cutItems?.Count ?? 0) > 0) await ProcessItemsAsync(cutItems, isCopy: false);


                if (initPath.Equals(FileSystem.CurrentPath, StringComparison.OrdinalIgnoreCase))
                {
                    await GitHub.FileSystem.UpdateExplorerView(FileSystem.CurrentPath);

                    HashSet<string> copiedNames =
                    [
                        .. (copiedItems ?? [])
                        .Select(i => i.Text)
                        .Where(t => !string.IsNullOrEmpty(t))
                    ];

                    HashSet<string> cutNames =
                    [
                        .. (cutItems ?? [])
                        .Select(i => i.Text)
                        .Where(t => !string.IsNullOrEmpty(t))
                    ];

                    _boundList.Items
                        .Cast<ListViewItem>()
                        .Where(i => i.Tag is RepositoryContent rc && (copiedNames.Contains(i.Text) || cutNames.Contains(i.Text)))
                        .ToList()
                        .ForEach(i => i.Selected = true);
                }

                // Remove cut items from memory
                if ((cutItems?.Count ?? 0) > 0) cutItems.Clear();

                CanPasteChanged?.Invoke(null, CanPaste);

                _boundbreadcrumbControl.FinishLoadingAnimation();
            }
        }

        public static async Task<bool> Upload(string filePath)
        {
            if (File.Exists(filePath))
            {
                _boundbreadcrumbControl.StartMarquee();

                string initPath = GitHub.FileSystem.CurrentPath;
                string destDir = GitHub.FileSystem.NormalizePath(initPath);

                Entry uploadedEntry = await UploadFileAsync(destDir, filePath, cts: cts);

                if (uploadedEntry is not null && initPath.Equals(FileSystem.CurrentPath, StringComparison.OrdinalIgnoreCase))
                {
                    await GitHub.FileSystem.UpdateExplorerView(FileSystem.CurrentPath);
                    // Select uploaded item
                    if (uploadedEntry?.Content != null)
                    {
                        _boundList.Items
                            .Cast<ListViewItem>()
                            .Where(i => i.Tag is RepositoryContent rc && rc.Path.Equals(uploadedEntry.Content.Path, StringComparison.OrdinalIgnoreCase))
                            .ToList()
                            .ForEach(i => i.Selected = true);
                    }
                }

                _boundbreadcrumbControl.FinishLoadingAnimation();

                return uploadedEntry is not null;
            }
            else
            {
                return false;
            }
        }

        public static async Task DeleteSelectedElementsAsync()
        {
            var items = _boundList.SelectedItems;
            if (items == null || items.Count == 0) return;

            string initPath = GitHub.FileSystem.CurrentPath;
            string destDir = GitHub.FileSystem.NormalizePath(initPath);

            // Snapshot items (SelectedListViewItemCollection is not safe to mutate)
            List<ListViewItem> snapshot = items.Cast<ListViewItem>().ToList();

            try
            {
                _boundbreadcrumbControl.Value = 0;
                _boundbreadcrumbControl.StartMarquee();

                // Get all files from snapshot, skip .gitkeep
                List<RepositoryContent> allFiles = [.. snapshot.Select(i => i.Tag as RepositoryContent).Where(rc => rc != null && rc.Type != ContentType.Dir && !rc.Name.Equals(".gitkeep", StringComparison.OrdinalIgnoreCase))];

                // --- Step 1: Include linked files if needed ---
                if (FilesOperationsLinking)
                {
                    var allFileDict = _boundList.Items.Cast<ListViewItem>().Where(i => i.Tag is RepositoryContent rc && rc.Type != ContentType.Dir).ToDictionary(i => i.Text, i => i.Tag as RepositoryContent, StringComparer.OrdinalIgnoreCase);

                    List<RepositoryContent> toAdd = [];

                    foreach (var rc in allFiles)
                    {
                        string ext = Path.GetExtension(rc.Name).ToLowerInvariant();
                        if (ext != ".wpth" && ext != ".wptp") continue;

                        string baseName = Path.GetFileNameWithoutExtension(rc.Name);
                        string linkedExt = ext == ".wpth" ? ".wptp" : ".wpth";
                        string linkedName = baseName + linkedExt;

                        if (allFileDict.TryGetValue(linkedName, out var linkedRc) && !allFiles.Contains(linkedRc))
                        {
                            // Ask user or auto-apply
                            bool includeLinked = Program.Settings.UsersServices.GitHub_AutoOperateOnLinkedFiles || Forms.GitHub_LinkedFilesConfirmation.ShowDialog(baseName, GitHub_LinkedFilesConfirmation.Operation.Delete) == DialogResult.Yes;

                            if (includeLinked) toAdd.Add(linkedRc);
                        }
                    }

                    allFiles.AddRange(toAdd);
                }

                // --- Step 2: Ask for confirmation for files ---
                List<string> filePaths = [];
                if (allFiles.Count > 1)
                {
                    int totalSize = allFiles.Sum(rc => rc.Size);
                    DialogResult result = Forms.GitHub_FileAction.ConfirmFilesDelete(allFiles.Count, totalSize);
                    if (result == DialogResult.Yes) filePaths = allFiles.Select(rc => rc.Path).ToList();
                }
                else
                {
                    foreach (var rc in allFiles)
                    {
                        var lvi = snapshot.FirstOrDefault(x => (x.Tag as RepositoryContent)?.Path == rc.Path);
                        Bitmap icon = null;
                        if (lvi != null && lvi.ImageKey != null && _boundList.LargeImageList.Images.ContainsKey(lvi.ImageKey))
                            icon = _boundList.LargeImageList.Images[lvi.ImageKey] as Bitmap;

                        if (Forms.GitHub_FileAction.ConfirmFileDelete(rc.Name, GitHub.FileSystem.FileTypeProvider?.Invoke(rc) ?? Program.Lang.Strings.Extensions.File, rc.Size, icon) == DialogResult.Yes)
                        {
                            filePaths.Add(rc.Path);
                        }
                    }
                }

                // --- Step 3: Ask for confirmation for directories ---
                List<RepositoryContent> allDirs = [.. snapshot.Select(i => i.Tag as RepositoryContent).Where(rc => rc != null && rc.Type == ContentType.Dir)];

                List<string> dirPaths = [];
                if (allDirs.Count > 1)
                {
                    DialogResult result = Forms.GitHub_FileAction.ConfirmFoldersDelete(allDirs.Count, allDirs.Sum(rc => FileSystem.Cache.GetSize(rc.Path)));
                    if (result == DialogResult.Yes) dirPaths = allDirs.Select(rc => rc.Path).ToList();
                }
                else
                {
                    foreach (RepositoryContent rc in allDirs)
                    {
                        if (Forms.GitHub_FileAction.ConfirmFolderDelete(rc.Name, FileSystem.Cache.GetSize(rc.Path)) == DialogResult.Yes) dirPaths.Add(rc.Path);
                    }
                }

                // --- Step 4: Process deletion ---
                async Task ProcessDeletionAsync(List<string> paths, bool isDirectory)
                {
                    if (!paths.Any()) return;

                    Action<int> progressCallback = progress =>
                    {
                        if (_boundbreadcrumbControl.IsMarquee) _boundbreadcrumbControl.StopMarquee();
                        _boundbreadcrumbControl.Value = progress;
                    };

                    if (isDirectory)
                        await GitHub.FileSystem.DeleteDirectoriesAsync(paths, cts, progressCallback);
                    else
                        await GitHub.FileSystem.DeleteFilesAsync(paths, cts, progressCallback);

                    foreach (string path in paths)
                    {
                        var lvi = snapshot.FirstOrDefault(i => (i.Tag as RepositoryContent)?.Path == path);
                        lvi?.Remove();
                    }

                    if (isDirectory) GitHub.FileSystem.UpdateTreeNode(GitHub.FileSystem.CurrentPath, true);
                }

                foreach (ListViewItem itemToDel in _boundList.SelectedItems.Cast<ListViewItem>().ToArray())
                {
                    if (itemToDel.Tag is RepositoryContent rc && rc.Name.Equals(".gitkeep", StringComparison.OrdinalIgnoreCase)) continue; // skip .gitkeep

                    itemToDel.Remove();
                }

                if (filePaths.Count > 0) await ProcessDeletionAsync(filePaths, false);
                if (dirPaths.Count > 0) await ProcessDeletionAsync(dirPaths, true);

                items.Clear();

                if (initPath.Equals(FileSystem.CurrentPath, StringComparison.OrdinalIgnoreCase))
                    await GitHub.FileSystem.UpdateExplorerView(FileSystem.CurrentPath);
            }
            catch (Exception ex)
            {
                Forms.BugReport.ThrowError(ex);
            }
            finally
            {
                _boundbreadcrumbControl.FinishLoadingAnimation();
                _boundbreadcrumbControl.StopMarquee();
                _boundbreadcrumbControl.Value = 0;

                StopAudio();
                PlayAudio(Program.TM.Sounds.Snd_Explorer_EmptyRecycleBin);
            }
        }

        public static async Task DownloadSelectedItemsAsync(string directory)
        {
            if (!Directory.Exists(directory)) return;

            var downloads_files = new List<(string url, string saveAs, long size, Bitmap icon)>();

            // --- Step 1: Gather all files in the ListView ---
            var selectedFiles = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var availableFiles = new Dictionary<string, RepositoryContent>(StringComparer.OrdinalIgnoreCase);

            foreach (ListViewItem item in _boundList.Items)
            {
                if (item.Tag is RepositoryContent rc && rc.Type == ContentType.File)
                {
                    availableFiles[rc.Name] = rc;
                    if (item.Selected) selectedFiles.Add(rc.Name);
                }
            }

            // --- Step 2: Handle selected items ---
            foreach (ListViewItem item in _boundList.SelectedItems)
            {
                if (cts?.Token.IsCancellationRequested ?? false) return;

                if (item.Tag is not RepositoryContent rc) continue;

                if (rc.Type == ContentType.File)
                {
                    string saveAs = Path.Combine(directory, rc.Name);
                    string parentDir = Path.GetDirectoryName(saveAs);
                    if (!Directory.Exists(parentDir)) Directory.CreateDirectory(parentDir);

                    Bitmap icon = item.ImageList?.Images[item.ImageKey] as Bitmap;
                    downloads_files.Add((rc.DownloadUrl, saveAs, rc.Size, icon));
                }
                else if (rc.Type == ContentType.Dir)
                {
                    // --- Step 2a: Enumerate all entries in the directory ---
                    var entries = new List<Entry>();
                    await foreach (Entry entry in EnumerateEntriesAsync(rc.Path, recursive: true, ct: cts.Token)) entries.Add(entry);

                    string topFolderName = rc.Name; // folder to strip parent paths

                    foreach (Entry entry in entries)
                    {
                        if (cts?.Token.IsCancellationRequested ?? false) return;

                        string normalizedPath = GitHub.FileSystem.NormalizePath(entry.Path);
                        string[] parts = normalizedPath.Split('/');
                        int topFolderIndex = Array.FindIndex(parts, p => p.Equals(topFolderName, StringComparison.OrdinalIgnoreCase));
                        if (topFolderIndex < 0) continue;

                        string relativePath = Path.Combine(parts.Skip(topFolderIndex).ToArray());
                        string localFile = Path.Combine(directory, relativePath);

                        if (entry.Type == EntryType.Dir)
                        {
                            Directory.CreateDirectory(localFile);
                            continue;
                        }

                        string parentDir = Path.GetDirectoryName(localFile);
                        if (!Directory.Exists(parentDir)) Directory.CreateDirectory(parentDir);

                        Bitmap icon = null;
                        if (_boundList.SmallImageList != null && _boundList.SmallImageList.Images.ContainsKey(entry.Name))
                            icon = _boundList.SmallImageList.Images[entry.Name] as Bitmap;

                        downloads_files.Add((entry.Content.DownloadUrl, localFile, entry.Size, icon));
                    }
                }
            }

            // --- Step 3: Handle paired files (.wpth / .wptp) ---
            foreach (var fileName in selectedFiles.ToList())
            {
                string baseName = Path.GetFileNameWithoutExtension(fileName);
                string ext = Path.GetExtension(fileName).ToLower();

                string pairedExt = ext switch
                {
                    ".wpth" => ".wptp",
                    ".wptp" => ".wpth",
                    _ => null
                };
                if (pairedExt == null) continue;

                string pairedFileName = baseName + pairedExt;

                if (availableFiles.TryGetValue(pairedFileName, out var pairedRc) && !selectedFiles.Contains(pairedFileName))
                {
                    if (FilesOperationsLinking && (Program.Settings.UsersServices.GitHub_AutoOperateOnLinkedFiles || Forms.GitHub_LinkedFilesConfirmation.ShowDialog(baseName, GitHub_LinkedFilesConfirmation.Operation.Download) == DialogResult.Yes))
                    {
                        selectedFiles.Add(pairedFileName);

                        string saveAs = Path.Combine(directory, pairedFileName);
                        string parentDir = Path.GetDirectoryName(saveAs);
                        if (!Directory.Exists(parentDir)) Directory.CreateDirectory(parentDir);

                        Bitmap icon = null;
                        if (_boundList.SmallImageList != null && _boundList.SmallImageList.Images.ContainsKey(pairedFileName))
                            icon = _boundList.SmallImageList.Images[pairedFileName] as Bitmap;

                        downloads_files.Add((pairedRc.DownloadUrl, saveAs, pairedRc.Size, icon));
                    }
                }
            }

            // --- Step 4: Start download ---
            Forms.DownloadManager_Dlg.DownloadFile(downloads_files);
        }

        #endregion

        #region Files\Directories helpers

        public static string StatusLabel
        {
            get
            {
                if (_boundList is null) return null;

                if (_boundList?.Items.Count == 0)
                {
                    return $"0 {Program.Lang.Strings.GitHubStrings.Explorer_Item} |";
                }

                int totalItems = _boundList.Items.Count;
                int selectedItems = _boundList.SelectedItems.Count;
                long selectedSize = 0;

                foreach (ListViewItem item in _boundList.SelectedItems)
                {
                    if (item.Tag is RepositoryContent entry)
                    {
                        if (entry.Type == ContentType.File)
                        {
                            selectedSize += entry.Size;
                        }
                        else if (entry.Type == ContentType.Dir)
                        {
                            selectedSize += FileSystem.Cache.GetSize(entry.Path);
                        }
                    }
                }


                string itemsText = $"{totalItems} {(totalItems > 1 ? Program.Lang.Strings.GitHubStrings.Explorer_Items : Program.Lang.Strings.GitHubStrings.Explorer_Item)} |";

                if (selectedItems == 0)
                {
                    return itemsText;
                }
                else
                {
                    string selectedText = $"{selectedItems} {(selectedItems > 1 ? Program.Lang.Strings.GitHubStrings.Explorer_Items : Program.Lang.Strings.GitHubStrings.Explorer_Item)} {Program.Lang.Strings.GitHubStrings.Explorer_Selected}";
                    return $"{itemsText} {selectedText} {selectedSize.ToStringFileSize()} |";
                }
            }
        }

        private static string GetAvailableItemText(string baseName)
        {
            string name = baseName;
            int i = 1;

            bool Exists(string text)
            {
                foreach (ListViewItem it in _boundList.Items)
                    if (string.Equals(it.Text, text, StringComparison.OrdinalIgnoreCase))
                        return true;
                return false;
            }

            while (Exists(name))
            {
                i++;
                name = $"{baseName} ({i})";
            }

            return name;
        }

        private static void SelectLabelEditWithoutExtension(ListViewItem item)
        {
            if (_boundList == null || item == null) return;

            _boundList.BeginInvoke((Action)(() =>
            {
                IntPtr hEdit = NativeMethods.User32.FindEditControl(_boundList.Handle);
                if (hEdit == IntPtr.Zero) return;

                string text = item.Text;
                int dot = text.LastIndexOf('.');
                if (dot <= 0) return;

                NativeMethods.User32.SendMessage(hEdit, NativeMethods.User32.EM_SETSEL, IntPtr.Zero, new IntPtr(dot));
            }));
        }

        public static void CancelCurrentOperation()
        {
            cts?.Cancel();
            cts = new();
        }

        #endregion

        #region Audio helpers

        private static void PlayAudio(string snd)
        {
            AltPlayingMethod = false;

            if (File.Exists(snd))
            {
                if (SP is not null)
                {
                    SP?.Stop();
                    SP?.Dispose();
                }

                try
                {
                    using (FileStream FS = new(snd, System.IO.FileMode.Open, FileAccess.Read))
                    {
                        SP = new(FS);
                        SP?.Load();
                        SP?.Play();
                    }
                }
                catch // Use method #2
                {
                    AltPlayingMethod = true;
                    NativeMethods.Helpers.PlayAudio(snd);
                }
            }

            else
            {
                if (AltPlayingMethod)
                    NativeMethods.Helpers.StopAudio();

                if (SP is not null)
                {
                    SP?.Stop();
                    SP?.Dispose();
                }
            }
        }

        private static void StopAudio()
        {
            if (AltPlayingMethod) NativeMethods.Helpers.StopAudio();

            if (SP is not null)
            {
                SP?.Stop();
                SP?.Dispose();
            }
        }

        #endregion

        #region Search

        /// <summary>
        /// Asynchronously searches cached repository entries under the current path
        /// for entries whose names match the specified keyword pattern. Supports wildcards '*' and '?'.
        /// </summary>
        /// <param name="list">The ListView control to populate with search results.</param>
        /// <param name="keyword">The search keyword. Can include '*' and '?' as wildcards.</param>
        /// <param name="cts">Optional cancellation token source to cancel the search.</param>
        /// <returns>A task representing the asynchronous search operation.</returns>
        public static async Task SearchAsync(string keyword)
        {
            Program.Log?.Write(LogEventLevel.Information, $"SearchAsync started for keyword='{keyword}' in currentPath='{CurrentPath}'");

            if (string.IsNullOrWhiteSpace(keyword))
            {
                // Reset view to current path
                await PopulateListViewAsync(CurrentPath);
                return;
            }

            if (_boundList.InvokeRequired)
            {
                _boundList.Invoke(new Func<Task>(() => SearchAsync(keyword)));
                return;
            }

            try
            {
                _boundList.Cursor = Cursors.WaitCursor;
                _boundList.BeginUpdate();

                if (_boundList.Columns.Count == 0)
                {
                    _boundList.Columns.Add(Program.Lang.Strings.GitHubStrings.Explorer_DetailsHeader_Name, 230);
                    _boundList.Columns.Add(Program.Lang.Strings.GitHubStrings.Explorer_DetailsHeader_Type, 200);
                    _boundList.Columns.Add(Program.Lang.Strings.GitHubStrings.Explorer_DetailsHeader_Size, 80);
                    _boundList.Columns.Add("MD5", 120);
                    _boundList.Columns.Add(Program.Lang.Strings.GitHubStrings.Explorer_DetailsHeader_URL, 220);
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

                        foreach (var entry in Cache.Values.Select(v => v.Entry.Content))
                        {
                            if (cts is not null && cts.Token.IsCancellationRequested) return;

                            if (entry == null) continue;

                            string name = entry.Name;

                            if ((rx != null && rx.IsMatch(name)) || (rx == null && name.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0))
                                matches.Add(entry);
                        }
                    }
                    else
                    {
                        foreach (var entry in Cache.Values.Select(v => v.Entry.Content))
                        {
                            if (cts is not null && cts.Token.IsCancellationRequested) return;

                            if (entry == null) continue;

                            if (entry.Name.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0)
                                matches.Add(entry);
                        }
                    }
                });

                matches = matches
                    .Where(e => e.Path.StartsWith(CurrentPath, StringComparison.OrdinalIgnoreCase))
                    .OrderBy(e => e.Type == Octokit.ContentType.Dir ? 0 : 1)
                    .ThenBy(e => e.Name)
                    .ToList();

                Program.Log?.Write(LogEventLevel.Information, $"SearchAsync found {matches.Count} matching entries under '{CurrentPath}'");

                _boundList.Items.Clear();

                int count = 0;
                foreach (var entry in matches)
                {
                    if (cts is not null && cts.Token.IsCancellationRequested) return;

                    bool isHidden = entry.Name.StartsWith(".");
                    if (!isHidden || isHidden && showHiddenFiles)
                    {
                        ListViewItem item = new(entry.Name) { Tag = entry };

                        item.SubItems.Add(FileTypeProvider?.Invoke(entry) ?? Program.Lang.Strings.Extensions.File);

                        long size = entry.Type == ContentType.Dir && Cache.Contains(entry.Path) ? Cache.GetSize(entry.Path) : entry.Size;

                        item.SubItems.Add(size.ToStringFileSize());
                        item.SubItems.Add(Cache.ShaToMd5(entry.Sha).ToUpper());
                        item.SubItems.Add(entry.HtmlUrl);

                        if (entry.Type == ContentType.Dir) item.ImageKey = "folder";
                        else if (entry.Name.EndsWith(".wpth", StringComparison.OrdinalIgnoreCase)) item.ImageKey = "wpth";
                        else if (entry.Name.EndsWith(".wptp", StringComparison.OrdinalIgnoreCase)) item.ImageKey = "wptp";
                        else if (!string.IsNullOrWhiteSpace(entry.Name))
                        {
                            // Get icon from Windows and add it into image lists
                            string ext = GetExtension(entry.Name);
                            if (!string.IsNullOrWhiteSpace(ext))
                            {
                                if (!_boundList.SmallImageList.Images.ContainsKey(ext))
                                {
                                    using (Icon ico = NativeMethods.Shell32.GetIconFromExtension(ext, NativeMethods.Shell32.IconSize.Small))
                                    using (Icon ico_resized = ico?.FromSize(16))
                                    using (Bitmap bmp = ico_resized?.ToBitmap())
                                    {
                                        _boundList.AddImagesToSmallImageList(new List<(Image, string)>
                                {
                                    (bmp, ext)
                                });
                                    }

                                    ProcessGhostIcons(_boundList.SmallImageList);
                                }

                                if (!_boundList.LargeImageList.Images.ContainsKey(ext))
                                {
                                    using (Icon ico = NativeMethods.Shell32.GetIconFromExtension(ext))
                                    using (Icon ico_resized = ico?.FromSize(48))
                                    {
                                        _boundList.LargeImageList.Images.Add(ext, ico_resized?.ToBitmap());
                                        ProcessGhostIcons(_boundList.LargeImageList);
                                    }
                                }

                                item.ImageKey = ext;
                            }
                            else
                            {
                                item.ImageKey = "file";
                            }
                        }
                        else item.ImageKey = "file";

                        if (isHidden) item.ImageKey = $"ghost_{item.ImageKey}";

                        _boundList.Items.Add(item);
                    }

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
                _boundList.EndUpdate();
                _boundList.Cursor = Cursors.Default;
                Program.Log?.Write(LogEventLevel.Information, $"SearchAsync finished for keyword='{keyword}' in currentPath='{CurrentPath}'");
            }
        }

        #endregion
    }
}