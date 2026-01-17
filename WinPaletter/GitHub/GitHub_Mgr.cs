using FluentTransitions;
using Octokit;
using Ookii.Dialogs.WinForms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinPaletter.GitHub;

namespace WinPaletter
{
    public partial class GitHub_Mgr : Form
    {
        sealed class PendingBranch
        {
            public string BaseBranch { get; set; } = "main";
        }

        public GitHub_Mgr()
        {
            InitializeComponent();
        }

        Octokit.Repository repo;
        Branch upstreamBranch;
        public ActionQueue actionQueue = new();

        private async void GitHubManager_Load(object sender, EventArgs e)
        {
            ApplyStyle(this);
            this.LoadLanguage();

            AddViewsToButton();

            await UpdateGitHubLoginData();

            bool forked = await GitHub.Repository.ExistsAsync(GitHub.Repository.Name);

            label8.Text = forked ? Program.Lang.Strings.GitHubStrings.ExplorerStatus_Forked : $"{Program.Lang.Strings.GitHubStrings.ExplorerStatus_NotForked} {Program.Lang.Strings.GitHubStrings.ExplorerStatus_SyncAndForkToManage}";
            groupBox6.Enabled = forked;
            tablessControl2.SelectedIndex = 0;
            groupBox4.UseSharpStyle = true;
            groupBox1.UseSharpStyle = true;

            if (forked)
            {
                await GetBranches();
            }

            FileSystem.Navigated += FileSystem_Navigated;
            FileSystem.CanPasteChanged += FileSystem_CanPasteChanged;
            FileSystem.StatusLabelChanged += FileSystem_StatusLabelChanged;
            FileSystem.CanDoIOChanged += FileSystem_CanDoIOChanged;

            toggle1.Checked = GitHub.FileSystem.ShowHiddenFiles;

            // After populating the tree for the first time
            UpdateExplorerLayout();
        }

        async Task GetBranches()
        {
            branchesView.Cursor = Cursors.WaitCursor;
            groupBox5.Enabled = false;

            branchesView.BeginUpdate();
            branchesView.Columns.Clear();
            branchesView.SmallImageList = imageList1;
            branchesView.Columns.Add(Program.Lang.Strings.GitHubStrings.Branch, 200);
            branchesView.Columns.Add(Program.Lang.Strings.GitHubStrings.LastUpdated, 220);
            branchesView.Columns.Add(Program.Lang.Strings.GitHubStrings.Branch_Ahead, 120, HorizontalAlignment.Right);
            branchesView.Columns.Add(Program.Lang.Strings.GitHubStrings.Branch_Behind, 120);
            branchesView.Columns.Add(Program.Lang.Strings.GitHubStrings.Committer, 140);
            branchesView.Columns.Add(Program.Lang.Strings.GitHubStrings.LastCommitMsg, 380);
            branchesView.Columns.Add("SHA", 70);
            branchesView.EndUpdate();

            IReadOnlyList<Branch> branches = await GitHub.Repository.Branch.GetBranchesAsync(true);

            SemaphoreSlim gate = new(6);
            List<Task<ListViewItem>> tasks = new(branches.Count);

            foreach (Branch branch in branches)
            {
                tasks.Add(Task.Run(async () =>
                {
                    await gate.WaitAsync();
                    try { return await NewItemFromBranch(repo, branch, upstreamBranch); }
                    finally { gate.Release(); }
                }));
            }

            ListViewItem[] items = await Task.WhenAll(tasks);

            branchesView.BeginUpdate();
            branchesView.Items.AddRange(items);
            branchesView.EndUpdate();

            progressBar1.Visible = false;

            if (items.Count() > 0)
            {
                if (tablessControl2.SelectedIndex == 0)
                {
                    Program.Animator.HideSync(tablessControl2);
                    tablessControl2.SelectedIndex = 1;
                    Program.Animator.ShowSync(tablessControl2);
                }
            }
            else
            {
                tablessControl2.SelectedIndex = 0;
            }

            groupBox5.Enabled = true;
            branchesView.Cursor = Cursors.Default;
        }

        async Task<ListViewItem> NewItemFromBranch(Octokit.Repository repo, Branch branch, Branch upstreamBranch)
        {
            GitHubClient client = Program.GitHub.Client;

            GitHubCommit commit = await client.Repository.Commit.Get(repo.Id, branch.Commit.Sha);
            CompareResult compare = await client.Repository.Commit.Compare(GitHub.Repository.originalOwner, GitHub.Repository.Name, upstreamBranch.Commit.Sha, branch.Commit.Sha);

            ListViewItem item = new() { Text = branch.Name, Tag = branch };

            DateTimeOffset lastUpdated = commit.Commit.Committer.Date;
            bool updated = compare.BehindBy == 0;
            bool isVerified = commit.Commit.Verification != null && commit.Commit.Verification.Verified;
            int ahead = compare.AheadBy;
            int behind = compare.BehindBy;
            int total = ahead + behind;

            item.SubItems.Add(ToFriendlyString(lastUpdated));
            item.SubItems.Add(BuildAheadBar(ahead, behind));
            item.SubItems.Add(BuildBehindBar(ahead, behind));
            item.SubItems.Add(commit.Committer != null ? commit.Committer.Login : commit.Commit.Committer.Name);
            item.SubItems.Add(commit.Commit.Message?.Split('\n')[0] ?? string.Empty);
            item.SubItems.Add(commit.Sha);

            item.ImageKey = ImageKey(branch, updated);

            return item;
        }

        string ImageKey(Branch branch, bool updated)
        {
            if (branch.Protected && updated) return "Branch_Protected_Updated";
            if (branch.Protected && !updated) return "Branch_Protected_Unupdated";
            if (!branch.Protected && updated) return "Branch_Unprotected_Updated";
            return "Branch_Unprotected_Unupdated";
        }

        static string BuildAheadBar(int ahead, int behind, int maxBlocks = 7)
        {
            int total = ahead + behind;
            if (total == 0 || ahead == 0) return "0";

            // Compute proportion of total for Ahead
            int blocks = (int)Math.Round((double)ahead / total * maxBlocks, MidpointRounding.AwayFromZero);
            if (blocks == 0 && ahead > 0) blocks = 1; // ensure visibility

            string bar = new string('█', blocks).PadRight(maxBlocks);
            return ahead + " " + bar;
        }

        static string BuildBehindBar(int ahead, int behind, int maxBlocks = 7)
        {
            int total = ahead + behind;
            if (total == 0 || behind == 0) return "0";

            // Compute proportion of total for Behind
            int blocks = (int)Math.Round((double)behind / total * maxBlocks, MidpointRounding.AwayFromZero);
            if (blocks == 0 && behind > 0) blocks = 1; // ensure visibility

            string bar = new string('█', blocks).PadRight(maxBlocks);
            return bar + " " + behind;
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
            btn_download.Enabled = e;
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

                repo = await Program.GitHub.Client.Repository.Get(FileSystem._owner, GitHub.Repository.Name);
                upstreamBranch = await Program.GitHub.Client.Repository.Branch.Get(GitHub.Repository.originalOwner, GitHub.Repository.Name, "main");
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

            bool forked = await GitHub.Repository.ExistsAsync(GitHub.Repository.Name);
            if (!forked) forked = await GitHub.Repository.ForkAsync(GitHub.Repository.Name) is not null;

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

        private async void button10_Click(object sender, EventArgs e)
        {
            if (branchesView.SelectedItems.Count == 0) return;

            ListViewItem item = branchesView?.SelectedItems[0];
            string branchName = branchesView?.SelectedItems[0].Text ?? string.Empty;

            if (string.IsNullOrEmpty(branchName)) return;

            if (item.Tag is Branch branch && branch.Protected)
            {
                MsgBox(string.Format(Program.Lang.Strings.GitHubStrings.Branch_CannotDoOperation_Protected, branch.Name), MessageBoxButtons.OK, MessageBoxIcon.Error, Program.Lang.Strings.GitHubStrings.Branch_CannotAccess_Protected_Tip);
                return;
            }

            if (MsgBox(string.Format(Program.Lang.Strings.GitHubStrings.Branch_Delete, branchName), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Cursor = Cursors.WaitCursor;
                progressBar1.Visible = true;
                groupBox5.Enabled = false;
                branchesView.Enabled = false;

                bool success = await GitHub.Repository.Branch.DeleteBranchAsync(branchName);

                if (success)
                {
                    branchesView?.Items.Remove(item);
                }
                else
                {
                    MsgBox(string.Format(Program.Lang.Strings.GitHubStrings.Branch_Delete_Error, branchName), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                groupBox5.Enabled = true;
                branchesView.Enabled = true;
                progressBar1.Visible = false;
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

        private async void btn_delete_Click(object sender, EventArgs e)
        {
            await FileSystem.DeleteSelectedElementsAsync();
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

            if (item.Tag is Branch branch && branch.Protected)
            {
                MsgBox(string.Format(Program.Lang.Strings.GitHubStrings.Branch_CannotAccess_Protected, branch.Name), MessageBoxButtons.OK, MessageBoxIcon.Error, Program.Lang.Strings.GitHubStrings.Branch_CannotAccess_Protected_Tip);
                return;
            }

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

        private async void button18_Click(object sender, EventArgs e)
        {
            string branchName = GitHub.Repository.Branch.SanitizeBranchName(GetAvailableItemText("new-themes-branch"));

            ListViewItem item = new() { Text = branchName, ImageKey = "Branch_Unprotected_Unupdated", Tag = new PendingBranch() { BaseBranch = "main" } };
            branchesView.Items.Add(item);
            item.BeginEdit();
        }

        private async void branchesView_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            try
            {
                ListViewItem item = branchesView.Items[e.Item];
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

                newName = GitHub.Repository.Branch.SanitizeBranchName(newName);

                if (string.IsNullOrWhiteSpace(newName))
                {
                    e.CancelEdit = true;
                    if (item.Tag is PendingBranch) branchesView.Items[e.Item].Remove();
                    return;
                }
                else if (!GitHub.Repository.Branch.IsValidBranchName(newName))
                {
                    MsgBox(Program.Lang.Strings.GitHubStrings.Branch_InvalidName, MessageBoxButtons.OK, MessageBoxIcon.Error, Program.Lang.Strings.GitHubStrings.Branch_NamingRules);
                    e.CancelEdit = true;
                    if (item.Tag is PendingBranch) item.Remove();
                    return;
                }
                else if (item.Tag is PendingBranch pb)
                {
                    progressBar1.Visible = true;

                    if (await GitHub.Repository.Branch.GetBranch(newName) is not null)
                    {
                        MsgBox(Program.Lang.Strings.GitHubStrings.NewBranch_AlreadyExists, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        e.CancelEdit = true;
                        item.Remove();
                        return;
                    }

                    Cursor previous = Cursor;
                    Cursor = Cursors.WaitCursor;

                    try
                    {
                        bool syncOk = await GitHub.Repository.Branch.IsUpdatedAsync(pb.BaseBranch, "main") || await GitHub.Repository.Branch.SyncBranchAsync(pb.BaseBranch, "main");
                        if (!syncOk)
                        {
                            MsgBox(Program.Lang.Strings.GitHubStrings.NewBranch_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            item.Remove();
                            return;
                        }

                        Branch branch = await GitHub.Repository.Branch.CreateBranchAsync(newName, pb.BaseBranch);
                        if (branch == null)
                        {
                            MsgBox(string.Format(Program.Lang.Strings.GitHubStrings.NewBranch_Error, newName), MessageBoxButtons.OK, MessageBoxIcon.Error);
                            item.Remove();
                            return;
                        }

                        if (branch.Protected) branch = await GitHub.Repository.Branch.SetBranchProtectionAsync(branch.Name, false);

                        branchesView.Items[e.Item] = await NewItemFromBranch(repo, branch, upstreamBranch);
                    }
                    finally
                    {
                        progressBar1.Visible = false;
                        Cursor = previous;
                    }

                    return;
                }
                else if (item.Tag is Branch existingBranch)
                {
                    string oldName = item.Text;

                    if (oldName.Equals(newName, StringComparison.Ordinal)) return;

                    if (newName.Equals("main", StringComparison.OrdinalIgnoreCase))
                    {
                        MsgBox(string.Format(Program.Lang.Strings.GitHubStrings.Branch_CannotDoOperation_Protected, "main"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        e.CancelEdit = true;
                        return;
                    }
                    if (existingBranch.Protected)
                    {
                        MsgBox(string.Format(Program.Lang.Strings.GitHubStrings.Branch_CannotDoOperation_Protected, existingBranch.Name), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        e.CancelEdit = true;
                        return;
                    }

                    progressBar1.Visible = true;
                    groupBox5.Enabled = false;
                    branchesView.Enabled = false;

                    try
                    {

                        if (await GitHub.Repository.Branch.GetBranch(newName) is not null)
                        {
                            MsgBox(Program.Lang.Strings.GitHubStrings.Branch_Rename_AlreadyExists, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            e.CancelEdit = true;
                            return;
                        }

                        Branch renamed = await GitHub.Repository.Branch.RenameBranchAsync(oldName, newName);
                        if (renamed == null)
                        {
                            e.CancelEdit = true;
                            item.Text = oldName;
                            return;
                        }

                        branchesView.Items[e.Item] = await NewItemFromBranch(repo, renamed, upstreamBranch);
                        branchesView.Items[e.Item].Selected = true;
                        branchesView.Items[e.Item].EnsureVisible();
                        branchesView.HideSelection = false;
                        branchesView.Focus();
                    }
                    finally
                    {
                        groupBox5.Enabled = true;
                        branchesView.Enabled = true;
                        progressBar1.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MsgBox(ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetAvailableItemText(string baseName)
        {
            string name = baseName;
            int i = 1;

            bool Exists(string text)
            {
                foreach (ListViewItem it in branchesView.Items) if (string.Equals(it.Text, text, StringComparison.OrdinalIgnoreCase)) return true;
                return false;
            }

            while (Exists(name))
            {
                i++;
                name = $"{baseName}-{i}";
            }

            return name;
        }

        private async void button12_Click(object sender, EventArgs e)
        {
            if (branchesView.SelectedItems.Count > 0)
            {
                ListViewItem item = branchesView.SelectedItems[0];
                if (item.Tag is Branch branch)
                {
                    try
                    {
                        Cursor = Cursors.WaitCursor;
                        progressBar1.Visible = true;
                        groupBox5.Enabled = false;
                        branchesView.Enabled = false;

                        Branch newBranch = await GitHub.Repository.Branch.SetBranchProtectionAsync(branch.Name, !branch.Protected);

                        if (newBranch != null)
                        {
                            CompareResult compare = await Program.GitHub.Client.Repository.Commit.Compare(GitHub.Repository.originalOwner, GitHub.Repository.Name, upstreamBranch.Commit.Sha, branch.Commit.Sha);
                            bool updated = compare.BehindBy == 0;

                            branchesView.SelectedItems[0].Tag = newBranch;
                            branchesView.SelectedItems[0].ImageKey = ImageKey(newBranch, updated);
                            branchesView.SelectedItems[0].Selected = true;
                            branchesView.SelectedItems[0].EnsureVisible();
                            branchesView.HideSelection = false;
                            branchesView.Focus();
                        }
                    }
                    finally
                    {
                        groupBox5.Enabled = true;
                        branchesView.Enabled = true;
                        progressBar1.Visible = false;
                        Cursor = Cursors.Default;
                    }
                }
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (branchesView.SelectedItems.Count > 0)
            {
                ListViewItem item_selected = branchesView.SelectedItems[0];
                string branchName = GitHub.Repository.Branch.SanitizeBranchName(GetAvailableItemText($"cloned-branch-from-{item_selected.Text}"));

                ListViewItem item = new() { Text = branchName, ImageKey = item_selected.ImageKey, Tag = new PendingBranch() { BaseBranch = (item_selected.Tag as Branch).Name } };
                branchesView.Items.Add(item);
                item.BeginEdit();
            }
        }

        private void branchesView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (branchesView.SelectedItems.Count > 0)
            {
                button14.Enabled = true;
                button10.Enabled = true;
                button11.Enabled = true;
                button12.Enabled = true;
            }
            else
            {
                button14.Enabled = false;
                button10.Enabled = false;
                button11.Enabled = false;
                button12.Enabled = false;
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (branchesView.SelectedItems.Count > 0)
            {
                ListViewItem item = branchesView.SelectedItems[0];
                item?.BeginEdit();
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            Program.Animator.HideSync(tablessControl2);
            tablessControl2.SelectedIndex = 0;
            Program.Animator.ShowSync(tablessControl2);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            Program.Animator.HideSync(tablessControl2);
            tablessControl2.SelectedIndex = 1;
            Program.Animator.ShowSync(tablessControl2);
        }

        private void button14_MouseEnter(object sender, EventArgs e)
        {
            Transition.With(label12, nameof(label12.Text), ((sender as UI.WP.Button).Tag ?? string.Empty).ToString()).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration_Quick));
        }

        private void button14_MouseLeave(object sender, EventArgs e)
        {
            Transition.With(label12, nameof(label12.Text), string.Empty).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration_Quick));
        }

        private void toggle2_CheckedChanged(object sender, EventArgs e)
        {
            GitHub.FileSystem.FilesOperationsLinking = toggle2.Checked;
        }

        private async void button17_Click(object sender, EventArgs e)
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

            await FileSystem.DownloadSelectedItemsAsync(selectedPath);
        }

        private void btn_new_Click(object sender, EventArgs e)
        {
            Forms.GitHub_ThemeUpload.ShowDialog();
        }
    }
}