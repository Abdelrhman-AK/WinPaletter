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
using static WinPaletter.Theme.Structures.WinTerminal;

namespace WinPaletter
{
    public partial class GitHubMgrForm : Form
    {
        CancellationTokenSource cts = new();

        private readonly List<(string, Bitmap, Bitmap, View)> views = new()
            {
                { (Program.Lang.Strings.GitHubStrings.Explorer_View_LargeIcons, Assets.GitHubMgr.Icons_Large, Assets.GitHubMgr.Glyph_View_Large, View.LargeIcon) },
                { (Program.Lang.Strings.GitHubStrings.Explorer_View_SmallIcons, Assets.GitHubMgr.Icons_Small, Assets.GitHubMgr.Glyph_View_Small, View.SmallIcon) },
                { (Program.Lang.Strings.GitHubStrings.Explorer_View_List, Assets.GitHubMgr.Icons_List, Assets.GitHubMgr.Glyph_View_List, View.List) },
                { (Program.Lang.Strings.GitHubStrings.Explorer_View_Details, Assets.GitHubMgr.Icons_Details, Assets.GitHubMgr.Glyph_View_Details, View.Details) },
                { (Program.Lang.Strings.GitHubStrings.Explorer_View_Tiles, Assets.GitHubMgr.Icons_Tile, Assets.GitHubMgr.Glyph_View_Tile, View.Tile) }
            };

        UI.WP.ContextMenuStrip contextMenu_all = new();
        UI.WP.ContextMenuStrip contextMenu_item = new();
        List<ListViewItem> cutItems;
        List<ListViewItem> copiedItems;

        ListViewItem itemBeingEdited;

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

            // After populating the tree for the first time
            UpdateExplorerLayout();
        }

        private void FileSystem_Navigated(object sender, string path)
        {
            UpdateExplorerLayout();
        }

        void AddViewsToButton()
        {
            button7.Menu.Items.Clear();

            // Create ToolStripMenuItems as radio buttons
            foreach ((string, Bitmap, Bitmap, View) view in views)
            {
                ToolStripMenuItem item = new(view.Item1)
                {
                    CheckOnClick = true,
                    Image = view.Item2,
                    Checked = listView1.View == view.Item4,
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
                    listView1.View = (((string, Bitmap, Bitmap, View))item.Tag).Item4;
                    button7.ImageGlyph = (((string, Bitmap, Bitmap, View))item.Tag).Item3;

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
                (string, Bitmap, Bitmap, View) view = ((string, Bitmap, Bitmap, View))item.Tag;
                if (view.Item4 == nextView)
                {
                    item.Checked = true;
                    button7.ImageGlyph = view.Item3;
                }
                else
                {
                    item.Checked = false;
                }
            }
        }

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

                if (User.GitHub_LoggedIn)
                {
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
            await GitHub.FileSystem.RefreshAsync(treeView1, listView1, breadcrumbControl1, cts);
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
            if (string.IsNullOrEmpty(textBox1.Text)) await GitHub.FileSystem.PopulateListViewAsync(listView1, GitHub.FileSystem.currentPath);
        }

        /// <summary>
        /// Update status label after selection status change
        /// </summary>
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
                        selectedSize += WinPaletter.GitHub.Cache.GetSize(entry.Path);
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
            GitHub.FileSystem.SetBranch(comboBox1.SelectedItem?.ToString(), treeView1, listView1, breadcrumbControl1, cts);
            Program.Animator.ShowSync(tablessControl1);

            Cursor = Cursors.Default;
        }

        private async void button12_Click(object sender, EventArgs e)
        {
        Retry:
            string branchName = InputBox("Create a new themes upload session branch", string.Empty, "Name your new branch for this theme upload. You can continue editing this branch until your Pull Request is approved.");
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
                        MsgBox($"Couldn't create branch `{branchName}`. Try creating it manually in your browser.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    groupBox6.Enabled = true;
                }
                else
                {
                    MsgBox("Your forked repository already has this branch. Try again with another branch name.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    goto Retry;
                }
            }
        }

        private async void button10_Click(object sender, EventArgs e)
        {
            string branchName = comboBox1.SelectedItem.ToString();
            if (MsgBox($"Are you sure you want to delete branch `{branchName}`?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
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
                    MsgBox($"Couldn't delete branch `{branchName}`. Try deleting it manually in your browser.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                breadcrumbControl1.StopMarquee();
                Cursor = Cursors.Default;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            groupBox5.Enabled = comboBox1.SelectedItem is not null;
        }

        private async void button14_Click(object sender, EventArgs e)
        {
            await GitHub.Repository.CommitAsync("newCommit", GitHub.Cache.BuildChanges());
            GitHub.Cache.Clear();
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

                }
                else
                {
                    // Right-click on empty space
                    contextMenu_all.Show(listView1, e.Location);
                }
            }
        }

        void InitializeMenus()
        {
            (string, Bitmap, Bitmap, View) view = views.Where(v => v.Item4 == View.LargeIcon).FirstOrDefault();
            ToolStripMenuItem menu_largeIconsView = new(view.Item1, view.Item2)
            {
                CheckOnClick = true,
                Checked = listView1.View == view.Item4,
                Tag = view
            };

            view = views.Where(v => v.Item4 == View.SmallIcon).FirstOrDefault();
            ToolStripMenuItem menu_smallIconsView = new(view.Item1, view.Item2)
            {
                CheckOnClick = true,
                Checked = listView1.View == view.Item4,
                Tag = view
            };

            view = views.Where(v => v.Item4 == View.List).FirstOrDefault();
            ToolStripMenuItem menu_listView = new(view.Item1, view.Item2)
            {
                CheckOnClick = true,
                Checked = listView1.View == view.Item4,
                Tag = view
            };

            view = views.Where(v => v.Item4 == View.Details).FirstOrDefault();
            ToolStripMenuItem menu_detailsView = new(view.Item1, view.Item2)
            {
                CheckOnClick = true,
                Checked = listView1.View == view.Item4,
                Tag = view
            };

            view = views.Where(v => v.Item4 == View.Tile).FirstOrDefault();
            ToolStripMenuItem menu_tileView = new(view.Item1, view.Item2)
            {
                CheckOnClick = true,
                Checked = listView1.View == view.Item4,
                Tag = view
            };

            ToolStripMenuItem menu_view = new("View")
            {
                DropDown = new UI.WP.ContextMenuStrip() { ShowImageMargin = true }
            };

            menu_view.DropDown.Items.AddRange(
            [
                menu_largeIconsView,
                menu_smallIconsView,
                menu_listView,
                menu_detailsView,
                menu_tileView
            ]);

            foreach (ToolStripMenuItem item in menu_view.DropDown.Items)
            {
                // Click handler
                item.Click += (s, e) =>
                {
                    // Uncheck all other items
                    foreach (ToolStripMenuItem other in menu_view.DropDown.Items)
                    {
                        if (other != item) other.Checked = false;
                    }
                    // Set the ListView view
                    listView1.View = (((string, Bitmap, Bitmap, View))item.Tag).Item4;
                    button7.ImageGlyph = (((string, Bitmap, Bitmap, View))item.Tag).Item3;
                    // Ensure this one is checked
                    item.Checked = true;
                };
            }

            ToolStripSeparator separator_0 = new();

            ToolStripMenuItem menu_paste = new("Paste") { Enabled = false };

            ToolStripSeparator separator_1 = new();

            ToolStripMenuItem menu_newItem = new("New")
            {
                DropDown = new UI.WP.ContextMenuStrip() { ShowImageMargin = true }
            };

            ToolStripMenuItem menu_newTheme;
            using (Icon ico = Properties.Resources.fileextension.FromSize(20))
            {
                menu_newTheme = new(Program.Lang.Strings.Extensions.WinPaletterTheme, ico.ToBitmap());
            }

            ToolStripMenuItem menu_newFolder = new(Program.Lang.Strings.Extensions.Folder, Assets.GitHubMgr.folder_web_48.Resize(20, 20));

            menu_newFolder.Click += async (s, e) => Init_NewDirectory();

            menu_newItem.DropDown.Items.AddRange(
            [
                menu_newFolder,
                menu_newTheme,
            ]);

            ToolStripSeparator separator_2 = new();

            ToolStripMenuItem menu_properties = new("Properties");

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
                    //string filename = content.Name;
                    //string filenameWithoutExt = Path.GetFileNameWithoutExtension(filename);
                    //string ext = Path.GetExtension(filename);
                    //string dir = GitHub.FileSystem.GetParent(content.Path);

                    //if (string.Equals(".wpth", ext, StringComparison.OrdinalIgnoreCase))
                    //{
                    //    string wptp = $"{dir}/{filenameWithoutExt}.wptp";
                    //    if (await GitHub.FileSystem.FileExistsAsync(wptp, cts))
                    //    {
                    //        ListViewItem wptpItem = listView1.Items.Cast<ListViewItem>().Where(i => i.Text.ToLower() == $"{filenameWithoutExt.ToLower()}.wptp").FirstOrDefault();
                    //        if (wptpItem != null)
                    //        {
                    //            wptpItem.Text = $"{filenameWithoutExt}.wptp";
                    //            RenameFile(wptpItem, new(listView1.Items.IndexOf(wptpItem)));
                    //        }
                    //    }
                    //}
                    //else if (string.Equals(".wptp", ext, StringComparison.OrdinalIgnoreCase))
                    //{
                    //    string wpth = $"{dir}/{filenameWithoutExt}.wpth";
                    //    if (await GitHub.FileSystem.FileExistsAsync(wpth, cts))
                    //    {
                    //        ListViewItem wpthItem = listView1.Items.Cast<ListViewItem>().Where(i => i.Text.ToLower() == $"{filenameWithoutExt.ToLower()}.wpth").FirstOrDefault();
                    //        if (wpthItem != null)
                    //        {
                    //            wpthItem.Text = $"{filenameWithoutExt}.wpth";
                    //            RenameFile(wpthItem, new(listView1.Items.IndexOf(wpthItem)));
                    //        }
                    //    }
                    //}

                    RenameFile(item, e);
                }
            }
        }

        /// <summary>
        /// Initiate creation of a new directory with beginning of label edit
        /// </summary>
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
            item.Selected = true;
            item.Focused = true;
            item.BeginEdit();
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

                foreach (ListViewItem notCutItem in listView1.Items.Cast<ListViewItem>().Where(i => !i.Selected && i.ImageKey.StartsWith("ghost", StringComparison.OrdinalIgnoreCase)))
                {
                    notCutItem.ImageKey = notCutItem.ImageKey.Replace("ghost_", string.Empty);
                }
            }
        }

        void Init_Copy()
        {
            foreach (ListViewItem item in listView1.Items.Cast<ListViewItem>().Where(i => i.ImageKey.StartsWith("ghost", StringComparison.OrdinalIgnoreCase)))
            {
                item.ImageKey = item.ImageKey.Replace("ghost_", string.Empty);
            }

            if (listView1.SelectedItems.Count > 0)
            {
                cutItems?.Clear();
                copiedItems?.Clear();
                copiedItems = [.. listView1.SelectedItems.Cast<ListViewItem>()];
            }
        }

        async void NewDirectory(ListViewItem item, LabelEditEventArgs e = null)
        {
            Cursor = Cursors.WaitCursor;
            breadcrumbControl1.StartMarquee();

            string path = $"{GitHub.FileSystem.currentPath}/{item.Text.Trim()}";

            try
            {
                await GitHub.FileSystem.CreateDirectoryAsync(path, item, cts);
                GitHub.FileSystem.UpdateTreeNode(treeView1, path, false);
            }
            catch
            {
                e?.CancelEdit = true;
            }
            finally
            {
                breadcrumbControl1.StopMarquee();
                Cursor = Cursors.Default;
            }
            return;
        }

        async void RenameDirectory(ListViewItem item, LabelEditEventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            breadcrumbControl1.StartMarquee();

            string oldPath = (item.Tag as RepositoryContent).Path;
            string newPath = oldPath.Substring(0, oldPath.LastIndexOf('/') + 1) + item.Text.Trim();

            try
            {
                await GitHub.FileSystem.MoveDirectoryAsync(oldPath, newPath, cts);

                RepositoryContent entry = (await GitHub.FileSystem.GetDirectoryInfo(newPath, cts)).Content;
                item.Tag = entry.Content;

                await GitHub.FileSystem.PopulateListViewAsync(listView1, GitHub.FileSystem.currentPath, cts);
                GitHub.FileSystem.UpdateTreeNode(treeView1, GitHub.FileSystem.currentPath, false);
            }
            catch
            {
                e.CancelEdit = true;
            }
            finally
            {
                breadcrumbControl1.StopMarquee();
                Cursor = Cursors.Default;
            }
        }

        async void RenameFile(ListViewItem item, LabelEditEventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            breadcrumbControl1.StartMarquee();

            string oldPath = (item.Tag as RepositoryContent).Path;
            string newPath = $"{GitHub.FileSystem.GetParent(oldPath)}/{item.Text.Trim()}";

            try
            {
                await GitHub.FileSystem.MoveFileAsync(oldPath, newPath, cts);

                RepositoryContent entry = (await GitHub.FileSystem.GetFileInfo(newPath, cts)).Content;
                item.Tag = entry.Content;

                await GitHub.FileSystem.PopulateListViewAsync(listView1, GitHub.FileSystem.currentPath, cts);
                GitHub.FileSystem.UpdateTreeNode(treeView1, GitHub.FileSystem.currentPath, false);
            }
            catch
            {
                e.CancelEdit = true;
            }
            finally
            {
                breadcrumbControl1.StopMarquee();
                Cursor = Cursors.Default;
            }
        }

        async Task DeleteElementsAsync(IList<ListViewItem> items, CancellationTokenSource cts = null)
        {
            if (items == null || items.Count == 0) return;

            cts ??= new();

            string currentPath = GitHub.FileSystem.currentPath;
            string destDir = GitHub.FileSystem.NormalizePath(currentPath);

            try
            {
                // Separate files and directories
                var filePaths = items
                    .Select(i => i.Tag as RepositoryContent)
                    .Where(rc => rc != null && rc.Type != ContentType.Dir)
                    .Where(rc => Forms.GitHub_FileAction.ConfirmFileDelete(
                        rc.Name,
                        GitHub.FileSystem.FileTypeProvider?.Invoke(rc) ?? Program.Lang.Strings.Extensions.File,
                        rc.Size,
                        listView1.LargeImageList.Images[items.FirstOrDefault(x => (x.Tag as RepositoryContent)?.Path == rc.Path)?.ImageKey] as Bitmap
                    ) == DialogResult.Yes)
                    .Select(rc => rc.Path)
                    .ToList();

                var dirPaths = items
                    .Select(i => i.Tag as RepositoryContent)
                    .Where(rc => rc != null && rc.Type == ContentType.Dir)
                    .Where(rc => Forms.GitHub_FileAction.ConfirmFolderDelete(
                        rc.Name,
                        GitHub.Cache.GetSize(rc.Path)
                    ) == DialogResult.Yes)
                    .Select(rc => rc.Path)
                    .ToList();

                // Helper to process deletion with progress
                async Task ProcessDeletionAsync(List<string> paths, bool isDirectory)
                {
                    int total = paths.Count;
                    int processed = 0;

                    if (total == 0) return;

                    Cursor = Cursors.WaitCursor;
                    breadcrumbControl1.Value = 0;
                    breadcrumbControl1.StartMarquee();

                    Action<int> progressCallback = progress =>
                    {
                        if (breadcrumbControl1.IsMarquee) breadcrumbControl1.StopMarquee();
                        breadcrumbControl1.Value = progress;
                    };

                    if (isDirectory)
                        await GitHub.FileSystem.DeleteDirectoriesAsync(paths, cts, progressCallback);
                    else
                        await GitHub.FileSystem.DeleteFilesAsync(paths, cts, progressCallback);

                    foreach (var path in paths)
                    {
                        var item = items.FirstOrDefault(i => (i.Tag as RepositoryContent)?.Path == path);
                        item?.Remove();
                        processed++;
                        progressCallback(processed * 100 / total);
                    }

                    if (isDirectory) GitHub.FileSystem.UpdateTreeNode(treeView1, GitHub.FileSystem.currentPath, true);

                    breadcrumbControl1.StopMarquee();
                    Cursor = Cursors.Default;
                }

                // Delete files first
                if (filePaths.Count > 0) await ProcessDeletionAsync(filePaths, isDirectory: false);

                // Then delete directories
                if (dirPaths.Count > 0) await ProcessDeletionAsync(dirPaths, isDirectory: true);

                items.Clear();

                // Refresh UI
                await GitHub.FileSystem.PopulateListViewAsync(listView1, currentPath, cts);
                GitHub.FileSystem.UpdateTreeNode(treeView1, currentPath, false);
            }
            finally
            {
                breadcrumbControl1.StopMarquee();
                Cursor = Cursors.Default;
            }
        }

        async void Paste()
        {
            if ((copiedItems?.Count ?? 0) > 0 || (cutItems?.Count ?? 0) > 0)
            {
                string targetPath = GitHub.FileSystem.currentPath;
                string destDir = GitHub.FileSystem.NormalizePath(targetPath);

                // Helper to process items
                async Task ProcessItemsAsync(IList<ListViewItem> items, bool isCopy)
                {
                    Cursor = Cursors.WaitCursor;
                    breadcrumbControl1.Value = 0;
                    breadcrumbControl1.StartMarquee();

                    // Separate files and directories
                    var filePaths = items
                        .Select(i => i.Tag as RepositoryContent)
                        .Where(rc => rc != null && rc.Type == ContentType.File)
                        .Select(rc => rc.Path)
                        .ToArray();

                    var dirPaths = items
                        .Select(i => i.Tag as RepositoryContent)
                        .Where(rc => rc != null && rc.Type == ContentType.Dir)
                        .Select(rc => rc.Path)
                        .ToArray();

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
                        }
                        else
                        {
                            await GitHub.FileSystem.MoveFilesAsync(filePaths, destDir, cts, progress =>
                            {
                                if (breadcrumbControl1.IsMarquee) breadcrumbControl1.StopMarquee();
                                breadcrumbControl1.Value = progress;
                            });
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
                        }
                        else
                        {
                            await GitHub.FileSystem.MoveDirectoriesAsync(dirPaths, destDir, cts, progress =>
                            {
                                if (breadcrumbControl1.IsMarquee) breadcrumbControl1.StopMarquee();
                                breadcrumbControl1.Value = progress;
                            });
                        }
                    }

                    // Update ListView items
                    foreach (ListViewItem item in items)
                    {
                        if (item.Tag is RepositoryContent rc)
                        {
                            string newPath = GitHub.FileSystem.NormalizePath($"{destDir}/{Path.GetFileName(rc.Path)}");
                            var entry = await GitHub.FileSystem.GetInfoRefreshAsync(newPath, cts: cts);
                            item.Tag = entry?.Content;
                        }

                        item.ImageKey = item.ImageKey.Replace("ghost_", string.Empty);
                        if (!isCopy) item.Remove();
                    }

                    breadcrumbControl1.StopMarquee();
                    Cursor = Cursors.Default;
                }

                // Process copied items
                if ((copiedItems?.Count ?? 0) > 0) await ProcessItemsAsync(copiedItems, isCopy: true);

                // Process cut items
                if ((cutItems?.Count ?? 0) > 0)
                {
                    await ProcessItemsAsync(cutItems, isCopy: false);
                    cutItems.Clear();
                }

                // Refresh UI
                await GitHub.FileSystem.PopulateListViewAsync(listView1, GitHub.FileSystem.currentPath, cts);
                GitHub.FileSystem.UpdateTreeNode(treeView1, GitHub.FileSystem.currentPath, false);
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
                await DeleteElementsAsync(listView1.SelectedItems as IList<ListViewItem>, cts);
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

        private async void button15_Click(object sender, EventArgs e)
        {

        }

        private void listView1_BeforeLabelEdit(object sender, LabelEditEventArgs e)
        {
            itemBeingEdited = listView1.Items[e.Item];
        }
    }
}
