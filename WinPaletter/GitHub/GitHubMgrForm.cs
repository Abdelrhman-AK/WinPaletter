using Newtonsoft.Json;
using Octokit;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinPaletter.GitHub;
using static System.Windows.Forms.ListView;
using static WinPaletter.Theme.Structures.WinTerminal;

namespace WinPaletter
{
    public partial class GitHubMgrForm : Form
    {
        CancellationTokenSource cts = new();
        UI.WP.ContextMenuStrip contextMenu_all = new();
        UI.WP.ContextMenuStrip contextMenu_item = new();
        List<ListViewItem> cutItems;
        List<ListViewItem> copiedItems;

        ListViewItem itemBeingEdited;

        private bool canPaste => (cutItems?.Count ?? 0) > 0 || (copiedItems?.Count ?? 0) > 0;

        public GitHubMgrForm()
        {
            InitializeComponent();
        }

        private async void GitHubManager_Load(object sender, EventArgs e)
        {
            ApplyStyle(this);
            this.LoadLanguage();

            AddViewsToButton();
            InitializeMenus();

            await UpdateGitHubLoginData();

            bool forked = await GitHub.Repository.ExistsAsync();

            label8.Text = forked ? Program.Lang.Strings.GitHubStrings.ExplorerStatus_Forked : $"{Program.Lang.Strings.GitHubStrings.ExplorerStatus_NotForked} {Program.Lang.Strings.GitHubStrings.ExplorerStatus_SyncAndForkToManage}";
            groupBox6.Enabled = forked;

            if (forked)
            {
                List<string> branches = await GitHub.Repository.GetBranchesAsync();
                branches.Remove("main");
                comboBox1.Items.AddRange([.. branches]);
            }

            GitHub.FileSystem.Navigated += FileSystem_Navigated;

            toggle1.Checked = GitHub.FileSystem.ShowHiddenFiles;

            // After populating the tree for the first time
            UpdateExplorerLayout();
        }

        #region Context Menus

        #region Context Menus Fields

        // Views list
        private readonly List<(string label, Bitmap icon, Bitmap glyph, View view)> views = new()
        {
            (Program.Lang.Strings.GitHubStrings.Explorer_View_LargeIcons, Assets.GitHubMgr.Icons_Large, Assets.GitHubMgr.Glyph_View_Large, View.LargeIcon),
            (Program.Lang.Strings.GitHubStrings.Explorer_View_SmallIcons, Assets.GitHubMgr.Icons_Small, Assets.GitHubMgr.Glyph_View_Small, View.SmallIcon),
            (Program.Lang.Strings.GitHubStrings.Explorer_View_List, Assets.GitHubMgr.Icons_List, Assets.GitHubMgr.Glyph_View_List, View.List),
            (Program.Lang.Strings.GitHubStrings.Explorer_View_Details, Assets.GitHubMgr.Icons_Details, Assets.GitHubMgr.Glyph_View_Details, View.Details),
            (Program.Lang.Strings.GitHubStrings.Explorer_View_Tiles, Assets.GitHubMgr.Icons_Tile, Assets.GitHubMgr.Glyph_View_Tile, View.Tile)
        };

        // Global menu items
        private ToolStripMenuItem menu_view;
        private ToolStripSeparator separator_0;
        private ToolStripMenuItem menu_paste;
        private ToolStripSeparator separator_1;
        private ToolStripMenuItem menu_newItem;
        private ToolStripMenuItem menu_newTheme;
        private ToolStripMenuItem menu_newFolder;
        private ToolStripSeparator separator_2;
        private ToolStripMenuItem menu_properties;

        // Item menu
        private ToolStripMenuItem menu_Open;
        private ToolStripMenuItem menu_Download;
        private ToolStripSeparator separator_item_1;
        private ToolStripMenuItem menu_CopyPath;
        private ToolStripMenuItem menu_CopyURL;
        private ToolStripMenuItem menu_Copy;
        private ToolStripMenuItem menu_Cut;
        private ToolStripSeparator separator_item_2;
        private ToolStripMenuItem menu_Delete;
        private ToolStripMenuItem menu_Rename;
        private ToolStripSeparator separator_item_3;
        private ToolStripMenuItem menu_item_properties;

        #endregion

        #region Context Menus Initialization

        private void InitializeMenus()
        {
            InitializeMenu_Global();
            InitializeMenu_Item();
        }

        private void InitializeMenu_Global()
        {
            // Create view menu items dynamically
            menu_view = new ToolStripMenuItem(Program.Lang.Strings.GitHubStrings.Explorer_View)
            {
                DropDown = new UI.WP.ContextMenuStrip() { ShowImageMargin = true }
            };

            foreach (var view in views)
            {
                ToolStripMenuItem item = new(view.label, view.icon)
                {
                    CheckOnClick = true,
                    Checked = listView1.View == view.view,
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
            menu_paste.Click += Menu_paste_Click;

            menu_newFolder = new ToolStripMenuItem(Program.Lang.Strings.Extensions.Folder, Assets.GitHubMgr.folder_web_48.Resize(16, 16));
            menu_newFolder.Click -= Menu_NewFolder_Click;
            menu_newFolder.Click += Menu_NewFolder_Click;

            using (Icon ico = Properties.Resources.fileextension.FromSize(20))
            {
                menu_newTheme = new ToolStripMenuItem(Program.Lang.Strings.Extensions.WinPaletterTheme, ico.ToBitmap());
            }

            menu_newItem = new ToolStripMenuItem(Program.Lang.Strings.General.New)
            {
                DropDown = new UI.WP.ContextMenuStrip() { ShowImageMargin = true }
            };
            menu_newItem.DropDown.Items.AddRange([menu_newFolder, menu_newTheme]);

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

        private void InitializeMenu_Item()
        {
            menu_Open = new ToolStripMenuItem("Open", Assets.GitHubMgr.folder_web_16);
            menu_Open.Click -= Menu_Open_Click;
            menu_Open.Click += Menu_Open_Click;

            menu_Download = new ToolStripMenuItem("Download", Assets.GitHubMgr.ContextMenu_Download);

            separator_item_1 = new ToolStripSeparator();

            menu_CopyPath = new ToolStripMenuItem("Copy as path");
            menu_CopyPath.Click -= Menu_CopyPath_Click;
            menu_CopyPath.Click += Menu_CopyPath_Click;

            menu_CopyURL = new ToolStripMenuItem("Copy URL");
            menu_CopyURL.Click -= Menu_CopyURL_Click;
            menu_CopyURL.Click += Menu_CopyURL_Click;

            menu_Copy = new ToolStripMenuItem("Copy");
            menu_Copy.Click -= Menu_Copy_Click;
            menu_Copy.Click += Menu_Copy_Click;

            menu_Cut = new ToolStripMenuItem("Cut");
            menu_Cut.Click -= Menu_Cut_Click;
            menu_Cut.Click += Menu_Cut_Click;

            separator_item_2 = new ToolStripSeparator();

            menu_Delete = new ToolStripMenuItem("Delete");
            menu_Delete.Click -= Menu_Delete_Click;
            menu_Delete.Click += Menu_Delete_Click;

            menu_Rename = new ToolStripMenuItem("Rename");
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

        #region Context Menus Event Handlers

        private void Menu_ViewItem_Click(object sender, EventArgs e)
        {
            if (sender is not ToolStripMenuItem item) return;

            foreach (ToolStripMenuItem other in menu_view.DropDown.Items) if (other != item) other.Checked = false;

            var viewData = ((string, Bitmap, Bitmap, View))item.Tag;
            listView1.View = viewData.Item4;
            button7.ImageGlyph = viewData.Item3;
            item.Checked = true;
        }

        private void Menu_NewFolder_Click(object sender, EventArgs e) => Init_NewDirectory();

        private void Menu_paste_Click(object sender, EventArgs e)
        {
            Paste();
        }

        private void Menu_Open_Click(object sender, EventArgs e) => FileSystem.List_DoubleClick(listView1, new());

        private void Menu_CopyPath_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                var rc = listView1.SelectedItems[0].Tag as RepositoryContent;
                Clipboard.SetText(rc.Path);
            }
        }

        private void Menu_CopyURL_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                var rc = listView1.SelectedItems[0].Tag as RepositoryContent;
                Clipboard.SetText(rc.Url);
            }
        }

        private void Menu_Copy_Click(object sender, EventArgs e) => Init_Copy();

        private void Menu_Cut_Click(object sender, EventArgs e) => Init_Cut();

        private void Menu_Delete_Click(object sender, EventArgs e) => DeleteElementsAsync(listView1.SelectedItems, cts);

        private void Menu_Rename_Click(object sender, EventArgs e) => listView1.SelectedItems[0]?.BeginEdit();

        private void Menu_ItemProperties_Click(object sender, EventArgs e)
        {
            // TODO: Show properties
        }

        #endregion
        
        #endregion

        public async Task UpdateGitHubLoginData()
        {
            Bitmap avatar = null;

            if (User.GitHub_LoggedIn)
            {
                Invoke(() =>
                {
                    label1.Text = User.GitHub.Login;
                    label2.Text = User.GitHub.Login;

                    url_lbl.Text = User.GitHub.HtmlUrl;
                    bio_lbl.Text = User.GitHub.Bio;

                    followers_count_lbl.Font = Fonts.ConsoleLarge;
                    following_count_lbl.Font = Fonts.ConsoleLarge;
                    created_lbl.Font = Fonts.ConsoleMedium;
                    updated_lbl.Font = Fonts.ConsoleMedium;

                    followers_count_lbl.Text = User.GitHub.Followers.ToString();
                    following_count_lbl.Text = User.GitHub.Following.ToString();

                    email_lbl.Text = User.GitHub.Email ?? Program.Lang.Strings.GitHubStrings.Overview_NoEmail;
                    contry_lbl.Text = User.GitHub.Location ?? Program.Lang.Strings.GitHubStrings.Overview_NoLocation;

                    created_lbl.Text = ToFriendlyString(User.GitHub.CreatedAt);
                    updated_lbl.Text = ToFriendlyString(User.GitHub.UpdatedAt);
                });

                // Wait for avatar to exist
                if (User.GitHub_Avatar is null)
                {
                    await User.DownloadAvatarAsync();
                }

                if (User.GitHub_Avatar != null)
                {
                    avatar = User.GitHub_Avatar;
                }
            }

            if (avatar is null)
            {
                avatar = Properties.Resources.GitHub_SignInForFeatures.Clone() as Bitmap;
            }

            Invoke(() =>
            {
                using (Bitmap avatar_resized = avatar.Resize(pictureBox1.Size))
                using (Bitmap avatar_resized_2 = avatar.Resize(pictureBox2.Size))
                {
                    pictureBox1.Image = avatar_resized.ToCircular();
                    pictureBox2.Image = avatar_resized_2.ToCircular();
                }
            });
        }

        private void FileSystem_Navigated(object sender, string path)
        {
            UpdateExplorerLayout();
        }

        void AddViewsToButton()
        {
            button7.Menu.Items.Clear();

            // Create ToolStripMenuItems as radio buttons
            foreach ((string label, Bitmap icon, Bitmap glyph, View view) view in views)
            {
                ToolStripMenuItem item = new(view.label)
                {
                    CheckOnClick = true,
                    Image = view.icon,
                    Checked = listView1.View == view.view,
                    Tag = view // store the View in Tag for easy access
                };

                // Click handler
                item.Click += (s, e) =>
                {
                    // Uncheck all other items
                    foreach (ToolStripMenuItem other in button7.Menu.Items)
                    {
                        if (other != item) other.Checked = false;
                    }

                    // Set the ListView view
                    listView1.View = (((string label, Bitmap icon, Bitmap glyph, View view))item.Tag).view;
                    button7.ImageGlyph = (((string label, Bitmap icon, Bitmap glyph, View view))item.Tag).glyph;

                    // Ensure this one is checked
                    item.Checked = true;
                };

                button7.Menu.Items.Add(item);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            // Define the ordered views
            View[] views = [View.LargeIcon, View.SmallIcon, View.List, View.Details, View.Tile];

            // Find the index of the current view
            int currentIndex = Array.IndexOf(views, listView1.View);
            int nextIndex = (currentIndex + 1) % views.Length;

            // Set the next view
            View nextView = views[nextIndex];
            listView1.View = nextView;

            // Update the menu items to match
            foreach (ToolStripMenuItem item in button7.Menu.Items)
            {
                (string label, Bitmap icon, Bitmap glyph, View view) view = ((string label, Bitmap icon, Bitmap glyph, View view))item.Tag;
                if (view.view == nextView)
                {
                    item.Checked = true;
                    button7.ImageGlyph = view.glyph;
                }
                else
                {
                    item.Checked = false;
                }
            }
        }

        public string ToFriendlyString(DateTimeOffset dateTime)
        {
            var now = DateTimeOffset.Now;
            var span = now - dateTime;

            if (span.TotalSeconds < 60)
                return $"{(int)span.TotalSeconds} {((int)span.TotalSeconds > 1 ? Program.Lang.Strings.GitHubStrings.Timing_Seconds : Program.Lang.Strings.GitHubStrings.Timing_Second)} {Program.Lang.Strings.GitHubStrings.Timing_Ago}";
            if (span.TotalMinutes < 60)
                return $"{(int)span.TotalMinutes} {((int)span.TotalMinutes > 1 ? Program.Lang.Strings.GitHubStrings.Timing_Minutes : Program.Lang.Strings.GitHubStrings.Timing_Minute)} {Program.Lang.Strings.GitHubStrings.Timing_Ago}";
            if (span.TotalHours < 24)
                return $"{(int)span.TotalHours} {((int)span.TotalHours > 1 ? Program.Lang.Strings.GitHubStrings.Timing_Hours : Program.Lang.Strings.GitHubStrings.Timing_Hour)} {Program.Lang.Strings.GitHubStrings.Timing_Ago}";

            var dayDiff = (now.Date - dateTime.Date).Days;
            if (dayDiff == 0)
                return $"{Program.Lang.Strings.GitHubStrings.Timing_Today} {dateTime:hh:mm tt}";
            if (dayDiff == 1)
                return $"{Program.Lang.Strings.GitHubStrings.Timing_Yesterday} {dateTime:hh:mm tt}";
            if (dayDiff == -1)
                return $"{Program.Lang.Strings.GitHubStrings.Timing_Tomorrow} {dateTime:hh:mm tt}";

            return dateTime.ToString("f");
        }

        private void url_lbl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string url = url_lbl.Text;
            if (!url.StartsWith("https://") || !url.StartsWith("http://")) url = $"https://{url}";
            Process.Start(url);
        }

        private void UpdateExplorerLayout()
        {
            button2.Enabled = GitHub.FileSystem.CanGoBack;
            button4.Enabled = GitHub.FileSystem.CanGoForward;
            button5.Enabled = GitHub.FileSystem.CanGoUp;

            button7.ImageGlyph = views.Find(v => v.Item4 == listView1.View).Item3;

            UpdateStatusSelections();
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            await GitHub.FileSystem.GoBack(treeView1, listView1);
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            await GitHub.FileSystem.GoForward(treeView1, listView1);
        }

        private async void button5_Click(object sender, EventArgs e)
        {
            await GitHub.FileSystem.GoUp(treeView1, listView1);
        }

        private async void button6_Click(object sender, EventArgs e)
        {
            await GitHub.FileSystem.UpdateExplorerView(treeView1, listView1, FileSystem.CurrentPath, cts);
        }

        private void breadcrumbControl1_StopRequested()
        {
            cts?.Cancel();
            cts = new();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            explorer_controls.Visible = tabControl1.SelectedIndex == 1 && tablessControl1.SelectedIndex == 1;
        }

        private async void button8_Click(object sender, EventArgs e)
        {
            breadcrumbControl1.StartMarquee();
            await GitHub.FileSystem.SearchAsync(listView1, textBox1.Text);
            breadcrumbControl1.StopMarquee();
        }

        private async void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text)) await GitHub.FileSystem.PopulateListViewAsync(listView1, GitHub.FileSystem.CurrentPath);
        }

        public void UpdateStatusSelections()
        {
            if (listView1.Items.Count == 0)
            {
                status_lbl.Text = $"0 {Program.Lang.Strings.GitHubStrings.Explorer_Item} |";
                return;
            }

            int totalItems = listView1.Items.Count;
            int selectedItems = listView1.SelectedItems.Count;
            long selectedSize = 0;

            foreach (ListViewItem item in listView1.SelectedItems)
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
                status_lbl.Text = itemsText;
            }
            else
            {
                string selectedText = $"{selectedItems} {(selectedItems > 1 ? Program.Lang.Strings.GitHubStrings.Explorer_Items : Program.Lang.Strings.GitHubStrings.Explorer_Item)} {Program.Lang.Strings.GitHubStrings.Explorer_Selected}";
                status_lbl.Text = $"{itemsText} {selectedText} {selectedSize.ToStringFileSize()} |";
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateStatusSelections();

            bool hasSelectedItems = listView1.SelectedItems.Count > 0;

            btn_cut.Enabled = hasSelectedItems;
            btn_copy.Enabled = hasSelectedItems;
            btn_rename.Enabled = hasSelectedItems;
            btn_delete.Enabled = hasSelectedItems;
        }

        private void listView1_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            UpdateStatusSelections();
        }

        private void listView1_ItemActivate(object sender, EventArgs e)
        {
            UpdateStatusSelections();
        }

        private async void button9_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            breadcrumbControl1.StartMarquee();

            groupBox3.Enabled = false;

            bool forked = await GitHub.Repository.ExistsAsync();
            if (!forked) await GitHub.Repository.ForkAsync();
            forked = await GitHub.Repository.ExistsAsync();

            label8.Text = forked ? Program.Lang.Strings.GitHubStrings.ExplorerStatus_Forked : $"{Program.Lang.Strings.GitHubStrings.ExplorerStatus_NotForked} {Program.Lang.Strings.GitHubStrings.ExplorerStatus_SyncAndForkToManage}";
            if (forked)
            {
                List<string> branches = await GitHub.Repository.GetBranchesAsync();
                branches.Remove("main");
                comboBox1.Items.AddRange([.. branches]);
            }
            groupBox6.Enabled = forked;

            groupBox3.Enabled = true;

            breadcrumbControl1.StopMarquee();
            Cursor = Cursors.Default;
        }

        private async void button11_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            Program.Animator.HideSync(tablessControl1);
            tablessControl1.SelectedIndex = 1;

            groupBox4.Enabled = false;
            groupBox1.Enabled = false;

            Program.Animator.ShowSync(tablessControl1);

            await GitHub.FileSystem.SetBranch(comboBox1.SelectedItem?.ToString(), treeView1, listView1, breadcrumbControl1, cts);

            groupBox4.Enabled = true;
            groupBox1.Enabled = true;

            Cursor = Cursors.Default;
        }

        private async void button12_Click(object sender, EventArgs e)
        {
        Retry:
            string branchName = InputBox(Program.Lang.Strings.GitHubStrings.NewBranch, string.Empty, Program.Lang.Strings.GitHubStrings.NewBranch_Instructions);
            branchName = Regex.Replace(branchName.ToLowerInvariant(), @"[^a-z0-9\-_/]", "-");

            if (!string.IsNullOrWhiteSpace(branchName))
            {
                if (!comboBox1.Items.Contains(branchName))
                {
                    Cursor = Cursors.WaitCursor;
                    breadcrumbControl1.StartMarquee();
                    groupBox6.Enabled = false;

                    // Main should be synched before each new branch
                    bool syncSuccess = await GitHub.Repository.IsSyncedAsync() || await GitHub.Repository.SyncAsync();
                    bool createBranchSuccess = await GitHub.Repository.CreateBranchAsync(branchName);
                    bool success = syncSuccess && createBranchSuccess;

                    breadcrumbControl1.StopMarquee();
                    Cursor = Cursors.Default;

                    if (success)
                    {
                        comboBox1.Items.Add(branchName);
                        comboBox1.SelectedItem = branchName;
                    }
                    else
                    {
                        MsgBox(string.Format(Program.Lang.Strings.GitHubStrings.NewBranch_Error, success), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    groupBox6.Enabled = true;
                }
                else
                {
                    MsgBox(Program.Lang.Strings.GitHubStrings.NewBranch_AlreadyExists, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    goto Retry;
                }
            }
        }

        private async void button10_Click(object sender, EventArgs e)
        {
            string branchName = comboBox1.SelectedItem.ToString();
            if (MsgBox(string.Format(Program.Lang.Strings.GitHubStrings.Branch_Delete, branchName), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Cursor = Cursors.WaitCursor;
                breadcrumbControl1.StartMarquee();

                bool success = await GitHub.Repository.DeleteBranchAsync(branchName);

                if (success)
                {
                    comboBox1.Items.Remove(branchName);
                }
                else
                {
                    MsgBox(string.Format(Program.Lang.Strings.GitHubStrings.Branch_Delete_Error, branchName), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                breadcrumbControl1.StopMarquee();
                Cursor = Cursors.Default;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            groupBox5.Enabled = comboBox1.SelectedItem is not null;
        }

        private async void button13_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            breadcrumbControl1.StartMarquee();

            bool canCreatePullRequest = await GitHub.Repository.CanCreatePullRequestAsync();

            if (canCreatePullRequest)
            {
                string title = $"Update theme: {User.GitHub.Login} ({DateTime.Now:yyyy-MM-dd HH:mm})";

                string body = $@"
**Author:** {User.GitHub.Login}
**WinPaletter Version:** {Program.Version}
Generated automatically by WinPaletter.";

                await GitHub.Repository.CreatePullRequestAsync(title, body);
            }

            breadcrumbControl1.StopMarquee();
            Cursor = Cursors.Default;
        }

        private static string GetAvailableItemText(string baseName, ListView listView)
        {
            string name = baseName;
            int i = 1;

            bool Exists(string text)
            {
                foreach (ListViewItem it in listView.Items)
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

        private void listView1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                // Hit test to check if mouse is over an item
                var info = listView1.HitTest(e.Location);
                if (info.Item != null)
                {
                    // Right-click on an item
                    if (listView1.SelectedItems.Count > 0)
                    {
                        RepositoryContent rc = listView1.SelectedItems[0]?.Tag as RepositoryContent;
                        menu_Open.Enabled = rc?.Type == ContentType.Dir;
                    }
                    else
                    {
                        menu_Open.Enabled = false;
                    }
                    contextMenu_item.Show(listView1, e.Location);
                }
                else
                {
                    // Right-click on empty space
                    menu_paste.Enabled = canPaste;
                    contextMenu_all.Show(listView1, e.Location);
                }
            }
        }

        private async void listView1_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            ListViewItem item = listView1.Items[e.Item];

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
                Program.ToolTip.Show(treeView1, Program.Lang.Strings.GitHubStrings.Explorer_InvalidCharToolTip,
                    $"{GitHub.FileSystem.InvalidCharsToolTip}\r\n{GitHub.FileSystem.InvalidNamesToolTip}",
                    Properties.Resources.checker_disabled, item.Bounds.Location);

                item?.Remove();
                e.CancelEdit = true;
                return;
            }
            else if (listView1.Items.Cast<ListViewItem>().Any(i => i != item && string.Equals(i.Text, newName, StringComparison.OrdinalIgnoreCase)))
            {
                Program.ToolTip.Show(treeView1, Program.Lang.Strings.GitHubStrings.Explorer_EntryExists, string.Empty, Properties.Resources.checker_disabled, item.Bounds.Location);

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
                        ListViewItem linkedItem = listView1.Items.Cast<ListViewItem>()
                            .FirstOrDefault(i =>
                            {
                                if (i.Tag is RepositoryContent rc)
                                {
                                    string rcBase = Path.GetFileNameWithoutExtension(rc.Name);
                                    string rcExt = Path.GetExtension(rc.Name);
                                    bool match = rcBase.Equals(oldBaseName, StringComparison.OrdinalIgnoreCase)
                                                 && rcExt.Equals(linkedExt, StringComparison.OrdinalIgnoreCase);
                                    if (match) return match;
                                }
                                return false;
                            });

                        if (linkedItem is not null)
                        {
                            // Rename linked item after main item
                            item.Text = newName;
                            await RenameFile(item, parentDirPath, e);

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

        void Init_NewDirectory()
        {
            ListViewItem item = new(GetAvailableItemText(Program.Lang.Strings.Extensions.NewFolder, listView1))
            {
                ImageKey = "folder",
                Tag = "NEWFOLDER_PENDING",
                Selected = true,
                Focused = true
            };

            listView1.Items.Add(item);
            item?.Selected = true;
            item?.Focused = true;
            item?.BeginEdit();
        }

        void Init_Cut()
        {
            if (listView1.SelectedItems.Count > 0)
            {
                copiedItems?.Clear();
                cutItems?.Clear();
                cutItems = [.. listView1.SelectedItems.Cast<ListViewItem>()];

                foreach (ListViewItem cutItem in listView1.SelectedItems)
                {
                    if (cutItem is not null && !cutItem.ImageKey.StartsWith("ghost", StringComparison.OrdinalIgnoreCase))
                    {
                        cutItem?.ImageKey = $"ghost_{cutItem?.ImageKey}";
                    }
                }

                foreach (ListViewItem notCutItem in listView1.Items.Cast<ListViewItem>().Where(i => !i.Selected && !i.Text.StartsWith(".") && i.ImageKey.StartsWith("ghost", StringComparison.OrdinalIgnoreCase)))
                {
                    notCutItem.ImageKey = notCutItem.ImageKey.Replace("ghost_", string.Empty);
                }
            }

            btn_paste.Enabled = canPaste;
        }

        void Init_Copy()
        {
            foreach (ListViewItem item in listView1.Items.Cast<ListViewItem>().Where(i => !i.Text.StartsWith(".") && i.ImageKey.StartsWith("ghost", StringComparison.OrdinalIgnoreCase)))
            {
                item.ImageKey = item.ImageKey.Replace("ghost_", string.Empty);
            }

            if (listView1.SelectedItems.Count > 0)
            {
                cutItems?.Clear();
                copiedItems?.Clear();
                copiedItems = [.. listView1.SelectedItems.Cast<ListViewItem>()];
            }

            btn_paste.Enabled = canPaste;
        }

        async void NewDirectory(ListViewItem item, LabelEditEventArgs e = null)
        {
            breadcrumbControl1.StartMarquee();

            string initPath = FileSystem.CurrentPath;
            string itemText = item.Text.Trim();
            string path = $"{GitHub.FileSystem.CurrentPath}/{itemText}";

            try
            {
                RepositoryContent dirContent = (await GitHub.FileSystem.CreateDirectoryAsync(path, cts)).Content;
                item.Tag = dirContent;

                if (initPath.Equals(FileSystem.CurrentPath, StringComparison.OrdinalIgnoreCase))
                {
                    await GitHub.FileSystem.UpdateExplorerView(treeView1, listView1, FileSystem.CurrentPath, cts);

                    listView1.Items.Cast<ListViewItem>().Where(i => i.Tag is RepositoryContent && i.Text.Equals(itemText, StringComparison.OrdinalIgnoreCase)).ToList()
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
                breadcrumbControl1.StopMarquee();
            }
            return;
        }

        async void RenameDirectory(ListViewItem item, LabelEditEventArgs e)
        {
            string initPath = FileSystem.CurrentPath;
            string oldPath = (item.Tag as RepositoryContent).Path;
            string itemText = item.Text.Trim();
            string newPath = oldPath.Substring(0, oldPath.LastIndexOf('/') + 1) + itemText;

            if (oldPath.Equals(newPath, StringComparison.OrdinalIgnoreCase)) return;

            breadcrumbControl1.StartMarquee();

            try
            {
                item.Tag = (await GitHub.FileSystem.MoveDirectoryAsync(oldPath, newPath, cts)).Content;

                if (initPath.Equals(FileSystem.CurrentPath, StringComparison.OrdinalIgnoreCase))
                {
                    await GitHub.FileSystem.UpdateExplorerView(treeView1, listView1, FileSystem.CurrentPath, cts);

                    listView1.Items.Cast<ListViewItem>().Where(i => i.Tag is RepositoryContent && i.Text.Equals(itemText, StringComparison.OrdinalIgnoreCase)).ToList()
                        .ForEach(i => i.Selected = true);
                }
            }
            catch
            {
                e.CancelEdit = true;
            }
            finally
            {
                breadcrumbControl1.StopMarquee();
            }
        }

        async Task RenameFile(ListViewItem item, string parentDirPath, LabelEditEventArgs e = null)
        {
            string oldPath = (item.Tag as RepositoryContent).Path;
            string itemText = item.Text.Trim();
            string newPath = $"{GitHub.FileSystem.GetParent(oldPath)}/{itemText}";

            if (oldPath.Equals(newPath, StringComparison.OrdinalIgnoreCase)) return;

            breadcrumbControl1.StartMarquee();

            try
            {
                item.Tag = (await GitHub.FileSystem.MoveFileAsync(oldPath, newPath, cts)).Content;

                if (parentDirPath.Equals(FileSystem.CurrentPath, StringComparison.OrdinalIgnoreCase))
                {
                    await GitHub.FileSystem.UpdateExplorerView(treeView1, listView1, FileSystem.CurrentPath, cts);

                    listView1.Items.Cast<ListViewItem>().Where(i => i.Tag is RepositoryContent && i.Text.Equals(itemText, StringComparison.OrdinalIgnoreCase)).ToList()
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
                breadcrumbControl1.StopMarquee();
            }
        }

        async void DeleteElementsAsync(SelectedListViewItemCollection items, CancellationTokenSource cts = null)
        {
            if (items == null || items.Count == 0) return;

            cts ??= new();

            string initPath = GitHub.FileSystem.CurrentPath;
            string destDir = GitHub.FileSystem.NormalizePath(initPath);

            // Snapshot items (SelectedListViewItemCollection is not safe to mutate)
            List<ListViewItem> snapshot = [.. items.Cast<ListViewItem>()];

            try
            {
                breadcrumbControl1.Value = 0;
                breadcrumbControl1.StartMarquee();

                // Get all files from snapshot
                List<RepositoryContent> allFiles = [.. snapshot.Select(i => i.Tag as RepositoryContent).Where(rc => rc != null && rc.Type != ContentType.Dir)];

                List<string> filePaths;

                if (allFiles.Count > 1)
                {
                    // Ask once for multiple files
                    var totalSize = allFiles.Sum(rc => rc.Size);
                    var result = Forms.GitHub_FileAction.ConfirmFilesDelete(allFiles.Count, totalSize);

                    if (result == DialogResult.Yes)
                    {
                        filePaths = allFiles.Select(rc => rc.Path).ToList();
                    }
                    else
                    {
                        filePaths = [];
                    }
                }
                else
                {
                    // Single file: ask normally
                    filePaths = [.. allFiles
                        .Where(rc =>
                        {
                            ListViewItem lvi = snapshot.FirstOrDefault(x => (x.Tag as RepositoryContent)?.Path == rc.Path);
                            Bitmap icon = null;

                            if (lvi != null && lvi.ImageKey != null && listView1.LargeImageList.Images.ContainsKey(lvi.ImageKey))
                                icon = listView1.LargeImageList.Images[lvi.ImageKey] as Bitmap;

                            return Forms.GitHub_FileAction.ConfirmFileDelete(rc.Name, GitHub.FileSystem.FileTypeProvider?.Invoke(rc) ?? Program.Lang.Strings.Extensions.File, rc.Size, icon) == DialogResult.Yes;
                        }).Select(rc => rc.Path)];
                }

                // Get all directories from snapshot
                List<RepositoryContent> allDirs = [.. snapshot.Select(i => i.Tag as RepositoryContent).Where(rc => rc != null && rc.Type == ContentType.Dir)];

                List<string> dirPaths;

                if (allDirs.Count > 1)
                {
                    // Ask once for multiple directories
                    var result = Forms.GitHub_FileAction.ConfirmFoldersDelete(allDirs.Count, allDirs.Sum(rc => FileSystem.Cache.GetSize(rc.Path)));

                    if (result == DialogResult.Yes)
                    {
                        dirPaths = [.. allDirs.Select(rc => rc.Path)];
                    }
                    else
                    {
                        dirPaths = [];
                    }
                }
                else
                {
                    // Single directory: ask normally
                    dirPaths = [.. allDirs.Where(rc => Forms.GitHub_FileAction.ConfirmFolderDelete(rc.Name, FileSystem.Cache.GetSize(rc.Path)) == DialogResult.Yes).Select(rc => rc.Path)];
                }

                async Task ProcessDeletionAsync(List<string> paths, bool isDirectory)
                {
                    if (paths.Count == 0) return;

                    Action<int> progressCallback = progress =>
                    {
                        if (breadcrumbControl1.IsMarquee) breadcrumbControl1.StopMarquee();
                        breadcrumbControl1.Value = progress;
                    };

                    if (isDirectory)
                        await GitHub.FileSystem.DeleteDirectoriesAsync(paths, cts, progressCallback);
                    else
                        await GitHub.FileSystem.DeleteFilesAsync(paths, cts, progressCallback);

                    foreach (string path in paths)
                    {
                        ListViewItem lvi = snapshot.FirstOrDefault(i => (i.Tag as RepositoryContent)?.Path == path);
                        lvi?.Remove();
                    }

                    if (isDirectory) GitHub.FileSystem.UpdateTreeNode(treeView1, GitHub.FileSystem.CurrentPath, true);
                }

                if (filePaths.Count > 0) await ProcessDeletionAsync(filePaths, false);
                if (dirPaths.Count > 0) await ProcessDeletionAsync(dirPaths, true);

                items.Clear();

                if (initPath.Equals(FileSystem.CurrentPath, StringComparison.OrdinalIgnoreCase))
                    await GitHub.FileSystem.UpdateExplorerView(treeView1, listView1, FileSystem.CurrentPath, cts);
            }
            catch (Exception ex)
            {
                Forms.BugReport.ThrowError(ex);
            }
            finally
            {
                breadcrumbControl1.FinishLoadingAnimation();
                breadcrumbControl1.StopMarquee();
                breadcrumbControl1.Value = 0;
            }
        }

        async void Paste()
        {
            if ((copiedItems?.Count ?? 0) > 0 || (cutItems?.Count ?? 0) > 0)
            {
                string initPath = GitHub.FileSystem.CurrentPath;
                string destDir = GitHub.FileSystem.NormalizePath(initPath);

                breadcrumbControl1.Value = 0;
                breadcrumbControl1.StartMarquee();

                // Helper to process items
                async Task ProcessItemsAsync(IList<ListViewItem> items, bool isCopy)
                {
                    breadcrumbControl1.Value = 0;
                    breadcrumbControl1.StartMarquee();

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
                                if (breadcrumbControl1.IsMarquee) breadcrumbControl1.StopMarquee();
                                breadcrumbControl1.Value = progress;
                            });

                            if (!breadcrumbControl1.IsMarquee) breadcrumbControl1.StartMarquee();
                        }
                        else
                        {
                            await GitHub.FileSystem.MoveFilesAsync(filePaths, destDir, cts, progress =>
                            {
                                if (breadcrumbControl1.IsMarquee) breadcrumbControl1.StopMarquee();
                                breadcrumbControl1.Value = progress;
                            });

                            if (!breadcrumbControl1.IsMarquee) breadcrumbControl1.StartMarquee();
                        }
                    }

                    // Directories
                    if (dirPaths.Length > 0)
                    {
                        if (isCopy)
                        {
                            await GitHub.FileSystem.CopyDirectoriesAsync(dirPaths, destDir, cts, progress =>
                            {
                                if (breadcrumbControl1.IsMarquee) breadcrumbControl1.StopMarquee();
                                breadcrumbControl1.Value = progress;
                            });

                            if (!breadcrumbControl1.IsMarquee) breadcrumbControl1.StartMarquee();
                        }
                        else
                        {
                            await GitHub.FileSystem.MoveDirectoriesAsync(dirPaths, destDir, cts, progress =>
                            {
                                if (breadcrumbControl1.IsMarquee) breadcrumbControl1.StopMarquee();
                                breadcrumbControl1.Value = progress;
                            });

                            if (!breadcrumbControl1.IsMarquee) breadcrumbControl1.StartMarquee();
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

                    breadcrumbControl1.StopMarquee();
                }

                // Process copied items
                if ((copiedItems?.Count ?? 0) > 0) await ProcessItemsAsync(copiedItems, isCopy: true);

                // Process cut items
                if ((cutItems?.Count ?? 0) > 0)
                {
                    await ProcessItemsAsync(cutItems, isCopy: false);
                }

                if (initPath.Equals(FileSystem.CurrentPath, StringComparison.OrdinalIgnoreCase))
                {
                    await GitHub.FileSystem.UpdateExplorerView(treeView1, listView1, FileSystem.CurrentPath, cts);

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

                    listView1.Items
                        .Cast<ListViewItem>()
                        .Where(i => i.Tag is RepositoryContent rc && (copiedNames.Contains(i.Text) || cutNames.Contains(i.Text)))
                        .ToList()
                        .ForEach(i => i.Selected = true);
                }

                // Remove cut items from memory
                if ((cutItems?.Count ?? 0) > 0) cutItems.Clear();

                btn_paste.Enabled = canPaste;

                breadcrumbControl1.FinishLoadingAnimation();
            }
        }

        private async void listView1_KeyUp(object sender, KeyEventArgs e)
        {
            ListViewItem item = listView1.SelectedItems.Count > 0 ? listView1.SelectedItems[0] : null;

            if (e.KeyCode == Keys.Enter)
            {
                if (itemBeingEdited is not null && item is not null)
                {
                    item?.EndEdit();
                    e.Handled = true; // prevent beep sound
                }
                else if (listView1.SelectedItems.Count > 0 && item.Tag is RepositoryContent rc)
                {
                    // Navigate into directory
                    await GitHub.FileSystem.NavigateTo(rc.Path, listView1, treeView1);
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
                if (FileSystem.CanGoBack) await GitHub.FileSystem.GoBack(treeView1, listView1);
            }
            else if (e.Alt && e.KeyCode == Keys.Right)
            {
                // Navigate forward
                if (FileSystem.CanGoForward) await GitHub.FileSystem.GoForward(treeView1, listView1);
            }
            else if (e.Control && e.KeyCode == Keys.A)
            {
                // Select all items
                foreach (ListViewItem childItem in listView1.Items) childItem.Selected = true;
            }
            else if (e.Control && e.KeyCode == Keys.N)
            {
                // Initiate new directory creation
                Init_NewDirectory();
            }
            else if (e.KeyCode == Keys.Delete)
            {
                // Delete selected file or directory
                DeleteElementsAsync(listView1.SelectedItems, cts);
            }
            else if (e.KeyCode == Keys.F5)
            {
                // Refresh current directory
                await GitHub.FileSystem.RefreshAsync(treeView1, listView1, breadcrumbControl1, cts);
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
                Paste();
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
            else if (e.KeyCode == Keys.Escape)
            {
                // Cancel current operation or clear selection
                cts?.Cancel();
            }
        }

        private void listView1_BeforeLabelEdit(object sender, LabelEditEventArgs e)
        {
            itemBeingEdited = listView1.Items[e.Item];
            SelectLabelEditWithoutExtension(listView1, itemBeingEdited);
        }

        private void toggle1_CheckedChanged(object sender, EventArgs e)
        {
            GitHub.FileSystem.ShowHiddenFiles = toggle1.Checked;
        }

        private void btn_rename_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count < 1) return;
            listView1.SelectedItems[0]?.BeginEdit();
        }

        private void btn_copy_Click(object sender, EventArgs e)
        {
            Init_Copy();
        }

        private void btn_cut_Click(object sender, EventArgs e)
        {
            Init_Cut();
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            DeleteElementsAsync(listView1.SelectedItems, cts);
        }

        private void btn_paste_Click(object sender, EventArgs e)
        {
            Paste();
        }

        private void SelectLabelEditWithoutExtension(ListView listView, ListViewItem item)
        {
            if (listView == null || item == null) return;

            listView.BeginInvoke((Action)(() =>
            {
                IntPtr hEdit = NativeMethods.User32.FindEditControl(listView.Handle);
                if (hEdit == IntPtr.Zero) return;

                string text = item.Text;
                int dot = text.LastIndexOf('.');
                if (dot <= 0) return;

                NativeMethods.User32.SendMessage(hEdit, NativeMethods.User32.EM_SETSEL, IntPtr.Zero, new IntPtr(dot));
            }));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GitHub.FileSystem.Cache.Clear();
            Program.Animator.HideSync(tablessControl1);
            tablessControl1.SelectedIndex = 0;
            Program.Animator.ShowSync(tablessControl1);
        }
    }
}