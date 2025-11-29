using Octokit;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinPaletter
{
    /// <summary>
    /// Provides functionality to explore the WinPaletter GitHub repository, 
    /// displaying its contents in a <see cref="TreeView"/> and <see cref="ListView"/> asynchronously.
    /// Caches all entries to avoid repeated API calls and calculates folder sizes.
    /// </summary>
    public static class GitHubRepoExplorer
    {
        #region Public Cached Data

        public static IReadOnlyList<RepositoryContent> CachedEntries { get; private set; }

        #endregion

        #region Private Fields

        private static readonly Dictionary<string, List<RepositoryContent>> DirectoryMap =
            new(StringComparer.OrdinalIgnoreCase);

        private static readonly Dictionary<string, long> FolderSizeMap =
            new(StringComparer.OrdinalIgnoreCase);

        private static string _owner;
        private const string _repo = "WinPaletter-Store";
        private const string _root = "Themes";

        private static ImageList _smallIcons;
        private static ImageList _largeIcons;
        private static ImageList _treeIcons;

        #endregion

        #region Custom Type Provider

        public static Func<RepositoryContent, string> FileTypeProvider { get; set; } = entry =>
        {
            if (entry.Type == ContentType.Dir)
                return "File folder";

            if (entry.Name.EndsWith(".wpth", StringComparison.OrdinalIgnoreCase))
                return "WinPaletter Theme file";

            if (entry.Name.EndsWith(".wptp", StringComparison.OrdinalIgnoreCase))
                return "WinPaletter Theme Resources Pack";

            return NativeMethods.Shlwapi.GetFriendlyTypeName(entry.Name);
        };

        #endregion

        #region Initialization

        public static async Task InitializeAsync(TreeView tree, ListView list)
        {
            _owner = User.GitHub.Login;

            InitializeImageLists(list, tree);

            var entries = await Task.Run(async () =>
            {
                var content = new List<RepositoryContent>();
                await FetchRecursive(_root, content);
                return content;
            });

            CachedEntries = entries;

            BuildDirectoryMap();
            BuildFolderSizes(_root); // Precompute folder sizes

            void UpdateUI()
            {
                BuildTree(tree);
                tree.Nodes[0].Expand();
                _ = PopulateListViewAsync(list, _root); // async population
                HookEvents(tree, list);
            }

            if (tree.InvokeRequired)
                tree.Invoke((MethodInvoker)UpdateUI);
            else
                UpdateUI();
        }

        #endregion

        #region Fetch Repository Content

        private static async Task FetchRecursive(string path, List<RepositoryContent> output)
        {
        retry:
            IReadOnlyList<RepositoryContent> items;
            try
            {
                items = await Program.GitHub.Client.Repository.Content
                    .GetAllContents(_owner, _repo, path);
            }
            catch (RateLimitExceededException)
            {
                await Task.Delay(1500);
                goto retry;
            }
            catch (NotFoundException)
            {
                return;
            }

            output.AddRange(items);

            foreach (var dir in items.Where(i => i.Type == ContentType.Dir))
                await FetchRecursive(dir.Path, output);
        }

        #endregion

        #region Directory Map & Folder Sizes

        private static void BuildDirectoryMap()
        {
            DirectoryMap.Clear();
            foreach (var entry in CachedEntries)
            {
                string parent = GetParent(entry.Path);
                if (!DirectoryMap.ContainsKey(parent))
                    DirectoryMap[parent] = new List<RepositoryContent>();

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

            foreach (var entry in DirectoryMap[path])
            {
                if (entry.Type == ContentType.Dir)
                    total += BuildFolderSizes(entry.Path);
                else
                    total += entry.Size;
            }

            FolderSizeMap[path] = total;
            return total;
        }

        #endregion

        #region Image List Initialization

        private static void InitializeImageLists(ListView list, TreeView tree)
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
            using (Icon ico = Properties.Resources.Folder.FromSize(size))
                imgList.Images.Add("folder", ico.ToBitmap());
            using (Icon ico = Properties.Resources.FileIcon.FromSize(size))
                imgList.Images.Add("file", ico.ToBitmap());
            using (Icon ico = Properties.Resources.fileextension.FromSize(size))
                imgList.Images.Add("wpth", ico.ToBitmap());
            using (Icon ico = Properties.Resources.ThemesResIcon.FromSize(size))
                imgList.Images.Add("wptp", ico.ToBitmap());
        }

        #endregion

        #region TreeView Methods

        private static void BuildTree(TreeView tree)
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

            foreach (var dir in DirectoryMap[parentPath].Where(d => d.Type == ContentType.Dir).OrderBy(d => d.Name))
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

        #endregion

        #region ListView Methods (Async)

        public static async Task PopulateListViewAsync(ListView list, string path)
        {
            if (list.InvokeRequired)
            {
                list.Invoke(new Func<Task>(() => PopulateListViewAsync(list, path)));
                return;
            }

            try
            {
                list.Cursor = Cursors.WaitCursor;
                list.BeginUpdate();
                list.Items.Clear();

                if (list.View == View.Details && list.Columns.Count == 0)
                {
                    list.Columns.Add("Name", 230);
                    list.Columns.Add("Type", 200);
                    list.Columns.Add("Size", 80);
                    list.Columns.Add("MD5", 120);
                    list.Columns.Add("URL", 120);
                }

                if (!DirectoryMap.ContainsKey(path)) return;

                var entries = DirectoryMap[path]
                    .OrderBy(e => e.Type == ContentType.Dir ? 0 : 1)
                    .ThenBy(e => e.Name)
                    .ToList();

                foreach (var entry in entries)
                {
                    ListViewItem item;
                    if (list.View == View.Details)
                    {
                        item = new ListViewItem(entry.Name);
                        item.SubItems.Add(FileTypeProvider?.Invoke(entry) ?? "File");

                        long size = entry.Type == ContentType.Dir && FolderSizeMap.ContainsKey(entry.Path)
                            ? FolderSizeMap[entry.Path]
                            : entry.Size;
                        item.SubItems.Add(size.ToStringFileSize());

                        item.SubItems.Add(ShaToMd5(entry.Sha).ToUpper());
                        item.SubItems.Add(entry.Url);
                    }
                    else
                        item = new(entry.Name);

                    if (entry.Type == ContentType.Dir)
                        item.ImageKey = "folder";
                    else if (entry.Name.EndsWith(".wpth", StringComparison.OrdinalIgnoreCase))
                        item.ImageKey = "wpth";
                    else if (entry.Name.EndsWith(".wptp", StringComparison.OrdinalIgnoreCase))
                        item.ImageKey = "wptp";
                    else
                        item.ImageKey = "file";

                    item.Tag = entry;
                    list.Items.Add(item);

                    // Allow UI to update every 50 items to keep it responsive
                    if (list.Items.Count % 50 == 0)
                        await Task.Yield();
                }
            }
            finally
            {
                list.EndUpdate();
                list.Cursor = Cursors.Default;
            }
        }

        public static string ShaToMd5(string sha)
        {
            if (string.IsNullOrEmpty(sha)) return string.Empty;

            using var md5 = MD5.Create();
            byte[] inputBytes = Encoding.UTF8.GetBytes(sha);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            StringBuilder sb = new();
            foreach (var b in hashBytes) sb.Append(b.ToString("x2"));
            return sb.ToString();
        }

        #endregion

        #region Events

        private static void HookEvents(TreeView tree, ListView list)
        {
            tree.AfterSelect += async (s, e) =>
            {
                string path = e.Node.Tag as string;
                await PopulateListViewAsync(list, path);
            };

            list.DoubleClick += async (s, e) =>
            {
                if (list.SelectedItems.Count == 0) return;
                RepositoryContent entry = list.SelectedItems[0].Tag as RepositoryContent;
                if (entry == null) return;

                if (entry.Type == ContentType.Dir)
                {
                    try
                    {
                        tree.Cursor = Cursors.WaitCursor;
                        list.Cursor = Cursors.WaitCursor;

                        TreeNode target = FindNode(tree.Nodes[0], entry.Path);
                        if (target != null)
                            tree.SelectedNode = target;

                        await Task.Delay(1); // allow cursor refresh
                    }
                    finally
                    {
                        tree.Cursor = Cursors.Default;
                        list.Cursor = Cursors.Default;
                    }
                }
            };
        }

        private static TreeNode FindNode(TreeNode node, string path)
        {
            if ((node.Tag as string) == path) return node;

            foreach (TreeNode child in node.Nodes)
            {
                var match = FindNode(child, path);
                if (match != null) return match;
            }

            return null;
        }

        #endregion
    }
}