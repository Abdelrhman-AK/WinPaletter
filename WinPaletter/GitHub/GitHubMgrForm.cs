using Octokit;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinPaletter.GitHub;
using WinPaletter.NativeMethods;

namespace WinPaletter
{
    public partial class GitHubMgrForm : Form
    {
        public GitHubMgrForm()
        {
            InitializeComponent();
        }

        private async void GitHubManager_Load(object sender, EventArgs e)
        {
            ApplyStyle(this);
            this.LoadLanguage();

            AddViewsToButton();

            await UpdateGitHubLoginData();

            bool forked = await GitHub.Repository.ExistsAsync();

            label8.Text = forked ? Program.Lang.Strings.GitHubStrings.ExplorerStatus_Forked : $"{Program.Lang.Strings.GitHubStrings.ExplorerStatus_NotForked} {Program.Lang.Strings.GitHubStrings.ExplorerStatus_SyncAndForkToManage}";
            groupBox6.Enabled = forked;

            if (forked)
            {
                await GetBranches();
            }

            FileSystem.Navigated += FileSystem_Navigated;
            FileSystem.CanPasteChanged += FileSystem_CanPasteChanged;
            FileSystem.StatusLabelChanged += FileSystem_StatusLabelChanged;
            FileSystem.CanDoIOChanged += FileSystem_CanDoIOChanged
                ;
            toggle1.Checked = GitHub.FileSystem.ShowHiddenFiles;

            // After populating the tree for the first time
            UpdateExplorerLayout();
        }

        async Task GetBranches()
        {
            branchesView.BeginUpdate();
            branchesView.Columns.Clear();

            branchesView.SmallImageList = imageList1;

            branchesView.Columns.Add("Branch", 250);
            branchesView.Columns.Add("Protected", 60, HorizontalAlignment.Center);
            branchesView.Columns.Add("Updated", 60, HorizontalAlignment.Center);
            branchesView.Columns.Add("Ahead", 80, HorizontalAlignment.Right);
            branchesView.Columns.Add("Behind", 80);
            branchesView.Columns.Add("SHA", 70);
            branchesView.Columns.Add("Last updated", 220);
            branchesView.Columns.Add("Committer", 140);
            branchesView.Columns.Add("Last commit message", 380);
            branchesView.Columns.Add("Files count", 80, HorizontalAlignment.Center);
            branchesView.Columns.Add("Verified", 80, HorizontalAlignment.Center);

            branchesView.EndUpdate();

            Octokit.Repository repo = await Program.GitHub.Client.Repository.Get(FileSystem._owner, GitHub.Repository.repositoryName);
            Branch upstreamBranch = await Program.GitHub.Client.Repository.Branch.Get(GitHub.Repository.originalOwner, GitHub.Repository.repositoryName, "main");

            branchesView.Cursor = Cursors.WaitCursor;

            foreach (Branch branch in await GitHub.Repository.GetBranchesAsync())
            {
                //if (branch.Name != "main")
                //{
                    branchesView.Items.Add(await NewBranch(repo, branch, upstreamBranch));
                //}
            }

            branchesView.Cursor = Cursors.Default;
        }

        async Task<ListViewItem> NewBranch(Octokit.Repository repo, Branch branch, Branch upstreamBranch)
        {
            GitHubCommit commit = await Program.GitHub.Client.Repository.Commit.Get(repo.Id, branch.Commit.Sha);
            CompareResult compare = await Program.GitHub.Client.Repository.Commit.Compare(GitHub.Repository.originalOwner, GitHub.Repository.repositoryName, upstreamBranch.Commit.Sha, branch.Commit.Sha);

            ListViewItem item = new()
            {
                Text = branch.Name,
                Tag = branch,
                ImageKey = "Branch"
            };

            DateTimeOffset lastUpdated = commit.Commit.Committer.Date;
            bool updated = compare.BehindBy == 0;

            item.SubItems.Add(branch.Protected ? Program.Lang.Strings.General.Yes : Program.Lang.Strings.General.No);
            item.SubItems.Add(updated ? Program.Lang.Strings.General.Yes : Program.Lang.Strings.General.No);
            item.SubItems.Add(compare.AheadBy.ToString());
            item.SubItems.Add(compare.BehindBy.ToString());
            item.SubItems.Add(commit.Sha.Substring(0, 7));
            item.SubItems.Add(ToFriendlyString(lastUpdated));
            item.SubItems.Add(commit.Committer != null ? commit.Committer.Login : commit.Commit.Committer.Name);
            item.SubItems.Add(commit.Commit.Message?.Split('\n')[0] ?? string.Empty);
            item.SubItems.Add(commit.Files.Count.ToString());

            /* Verified (GPG) */
            item.SubItems.Add(commit.Commit.Verification != null && commit.Commit.Verification.Verified ? Program.Lang.Strings.General.Yes : Program.Lang.Strings.General.No);

            return item;
        }

        private void FileSystem_CanPasteChanged(object sender, bool e)
        {
            btn_paste.Enabled = e;
        }

        private void FileSystem_Navigated(object sender, string path)
        {
            UpdateExplorerLayout();
        }

        private void FileSystem_StatusLabelChanged(object sender, string e)
        {
            status_lbl.Text = e;
        }

        private void FileSystem_CanDoIOChanged(object sender, bool e)
        {
            btn_cut.Enabled = e;
            btn_copy.Enabled = e;
            btn_rename.Enabled = e;
            btn_delete.Enabled = e;
        }

        public void UpdateViewButton((string label, Bitmap icon, Bitmap glyph, View view) data)
        {
            button7.ImageGlyph = data.glyph;
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

        void AddViewsToButton()
        {
            button7.Menu.Items.Clear();

            // Create ToolStripMenuItems as radio buttons
            foreach ((string label, Bitmap icon, Bitmap glyph, View view) view in FileSystem.Views)
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

            button7.ImageGlyph = FileSystem.Views.Find(v => v.view == listView1.View).glyph;

            status_lbl.Text = FileSystem.StatusLabel;
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            await GitHub.FileSystem.GoBack();
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            await GitHub.FileSystem.GoForward();
        }

        private async void button5_Click(object sender, EventArgs e)
        {
            await GitHub.FileSystem.GoUp();
        }

        private async void button6_Click(object sender, EventArgs e)
        {
            await GitHub.FileSystem.UpdateExplorerView(FileSystem.CurrentPath);
        }

        private void breadcrumbControl1_StopRequested()
        {
            FileSystem.CancelCurrentOperation();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            explorer_controls.Visible = tabControl1.SelectedIndex == 1 && tablessControl1.SelectedIndex == 1;
        }

        private async void button8_Click(object sender, EventArgs e)
        {
            breadcrumbControl1.StartMarquee();
            await GitHub.FileSystem.SearchAsync(textBox1.Text);
            breadcrumbControl1.StopMarquee();
        }

        private async void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text)) await GitHub.FileSystem.UpdateExplorerView(FileSystem.CurrentPath);
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
               await GetBranches();
            }
            groupBox6.Enabled = forked;

            groupBox3.Enabled = true;

            breadcrumbControl1.StopMarquee();
            Cursor = Cursors.Default;
        }

        private async void button12_Click(object sender, EventArgs e)
        {
        Retry:
            string branchName = InputBox(Program.Lang.Strings.GitHubStrings.NewBranch, string.Empty, Program.Lang.Strings.GitHubStrings.NewBranch_Instructions);
            branchName = Regex.Replace(branchName.ToLowerInvariant(), @"[^a-z0-9\-_/]", "-");

            if (!string.IsNullOrWhiteSpace(branchName))
            {
                if (!branchesView.Items.Cast<ListViewItem>().Any(i => i.Text == branchName))
                {
                    Cursor = Cursors.WaitCursor;
                    breadcrumbControl1.StartMarquee();
                    groupBox6.Enabled = false;

                    bool syncSuccess = await GitHub.Repository.IsUpdatedAsync("main") || await GitHub.Repository.SyncBranchAsync();
                    Branch branch = await GitHub.Repository.CreateBranchAsync(branchName);
                    bool success = syncSuccess && branch is not null;

                    breadcrumbControl1.StopMarquee();
                    Cursor = Cursors.Default;

                    if (success)
                    {
                        Octokit.Repository repo = await Program.GitHub.Client.Repository.Get(FileSystem._owner, GitHub.Repository.repositoryName);
                        Branch upstreamBranch = await Program.GitHub.Client.Repository.Branch.Get(GitHub.Repository.originalOwner, GitHub.Repository.repositoryName, "main");
                        branchesView.Items.Add(await NewBranch(repo, branch, upstreamBranch));
                    }
                    else
                    {
                        MsgBox(string.Format(Program.Lang.Strings.GitHubStrings.NewBranch_Error, branchName), MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (branchesView.SelectedItems.Count == 0) return;

            ListViewItem item = branchesView?.SelectedItems[0];
            string branchName = branchesView?.SelectedItems[0].Text ?? string.Empty;

            if (string.IsNullOrEmpty(branchName)) return;

            if (MsgBox(string.Format(Program.Lang.Strings.GitHubStrings.Branch_Delete, branchName), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Cursor = Cursors.WaitCursor;
                breadcrumbControl1.StartMarquee();

                bool success = await GitHub.Repository.DeleteBranchAsync(branchName);

                if (success)
                {
                    branchesView?.Items.Remove(item);
                }
                else
                {
                    MsgBox(string.Format(Program.Lang.Strings.GitHubStrings.Branch_Delete_Error, branchName), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                breadcrumbControl1.StopMarquee();
                Cursor = Cursors.Default;
            }
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
            FileSystem.Init_Copy();
        }

        private void btn_cut_Click(object sender, EventArgs e)
        {
            FileSystem.Init_Cut();
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            FileSystem.DeleteSelectedElementsAsync();
        }

        private void btn_paste_Click(object sender, EventArgs e)
        {
            FileSystem.Paste();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GitHub.FileSystem.Cache.Clear();
            Program.Animator.HideSync(tablessControl1);
            tablessControl1.SelectedIndex = 0;
            Program.Animator.ShowSync(tablessControl1);
        }

        private async void branchesView_DoubleClick(object sender, EventArgs e)
        {
            if (branchesView.SelectedItems.Count == 0) return;
            ListViewItem item = branchesView.SelectedItems[0];

            Cursor = Cursors.WaitCursor;

            Program.Animator.HideSync(tablessControl1);
            tablessControl1.SelectedIndex = 1;

            groupBox4.Enabled = false;
            groupBox1.Enabled = false;

            Program.Animator.ShowSync(tablessControl1);

            await GitHub.FileSystem.SetBranch(item.Text, treeView1, listView1, breadcrumbControl1);

            groupBox4.Enabled = true;
            groupBox1.Enabled = true;

            Cursor = Cursors.Default;
        }
    }
}