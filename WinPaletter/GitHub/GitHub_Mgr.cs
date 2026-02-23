using FluentTransitions;
using Octokit;
using Ookii.Dialogs.WinForms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinPaletter.GitHub;

namespace WinPaletter
{
    public partial class GitHub_Mgr : UI.WP.Form
    {
        int previousIndex = -1;
        Octokit.Repository repo;
        Branch upstreamBranch;
        bool pendingBranchesFetches = false;
        private readonly SemaphoreSlim _initLock = new(1, 1);

        public GitHub_Mgr()
        {
            InitializeComponent();

            GitHub.Events.GitHubAvatarUpdated += User_GitHubAvatarUpdated;
            GitHub.Events.GitHubUserSwitch += User_GitHubUserSwitch;
            GitHub.Events.OnTokenLoaded += Events_OnTokenLoaded;
            GitHub.Events.NetworkLost += Events_NetworkLost;
            GitHub.Events.RateLimitExceeded += Events_RateLimitExceeded;
            GitHub.Events.Navigated += FileSystem_Navigated;
            GitHub.Events.CanPasteChanged += FileSystem_CanPasteChanged;
            GitHub.Events.StatusLabelChanged += FileSystem_StatusLabelChanged;
            GitHub.Events.CanDoIOChanged += FileSystem_CanDoIOChanged;
            GitHub.Events.ViewChanged += Events_ViewChanged;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Unsubscribe from events
                GitHub.Events.GitHubAvatarUpdated -= User_GitHubAvatarUpdated;
                GitHub.Events.GitHubUserSwitch -= User_GitHubUserSwitch;
                GitHub.Events.OnTokenLoaded -= Events_OnTokenLoaded;
                GitHub.Events.NetworkLost -= Events_NetworkLost;
                GitHub.Events.RateLimitExceeded -= Events_RateLimitExceeded;
                GitHub.Events.Navigated -= FileSystem_Navigated;
                GitHub.Events.CanPasteChanged -= FileSystem_CanPasteChanged;
                GitHub.Events.StatusLabelChanged -= FileSystem_StatusLabelChanged;
                GitHub.Events.CanDoIOChanged -= FileSystem_CanDoIOChanged;
                GitHub.Events.ViewChanged -= Events_ViewChanged;

                // Dispose other managed resources if any
                components?.Dispose();
            }

            base.Dispose(disposing);
        }

        private async void GitHub_Mgr_Load(object sender, EventArgs e)
        {
            groupBox6.UseDecorationPattern = true;

            previousIndex = -1;
            tablessControl1.SelectedIndex = 5;

            created_lbl.Font = Fonts.ConsoleMedium;
            updated_lbl.Font = Fonts.ConsoleMedium;

            AddViewsToButton();

            await LoadInternal();
        }

        private async Task Init()
        {
            if (!await _initLock.WaitAsync(0)) return;

            try
            {
                UpdateGitHubLoginData();

                bool forked = await GitHub.Repository.ExistsAsync(GitHub.Repository.Name);

                label8.Text = forked
                    ? Program.Localization.Strings.GitHubStrings.ExplorerStatus_Forked
                    : $"{Program.Localization.Strings.GitHubStrings.ExplorerStatus_NotForked} {Program.Localization.Strings.GitHubStrings.ExplorerStatus_SyncAndForkToManage}";
                button9.Enabled = !forked;
                tablessControl2.SelectedIndex = 0;

                if (forked)
                {
                    await GetBranches();
                }

                // After populating the tree for the first time
                UpdateExplorerLayout();
            }
            catch (Exception ex)
            {
                Forms.BugReport.Throw(ex);
                throw;
            }
            finally
            {
                _initLock.Release();
            }
        }

        private async Task LoadInternal()
        {
            if (!IsHandleCreated)
            {
                return;
            }

            if (!Program.IsNetworkAvailable)
            {
                ShowTab(3, false);
                return;
            }

            var (remainingTrials, whenWillReset) = await GitHub.Helpers.RemainingTrials();

            if (remainingTrials == 0)
            {
                labelAlt3.Text = string.Format(Program.Localization.Strings.GitHubStrings.API_RateLimited, whenWillReset.ToLocalTime());
                ShowTab(4, false);
                return;
            }

            if (!User.GitHub_LoggedIn)
            {
                ShowTab(2, false);
                return;
            }

            ShowTab(0, false);

            await Init();
        }

        private async void Events_OnTokenLoaded(object sender, string e)
        {
            if (IsHandleCreated) await LoadInternal();
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
                        if (other != item)
                        {
                            other.Checked = false;
                        }
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
                    if (item.Tag is PendingBranch)
                    {
                        branchesView.Items[e.Item].Remove();
                    }

                    return;
                }
                else if (!GitHub.Repository.Branch.IsValidBranchName(newName))
                {
                    MsgBox(
                        Program.Localization.Strings.GitHubStrings.Branch_InvalidName,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error,
                        Program.Localization.Strings.GitHubStrings.Branch_NamingRules);
                    e.CancelEdit = true;
                    if (item.Tag is PendingBranch)
                    {
                        item.Remove();
                    }

                    return;
                }
                else if (item.Tag is PendingBranch pb)
                {
                    progressBar1.Visible = true;

                    if (await GitHub.Repository.Branch.GetBranch(newName) is not null)
                    {
                        MsgBox(
                            Program.Localization.Strings.GitHubStrings.NewBranch_AlreadyExists,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        e.CancelEdit = true;
                        item.Remove();
                        return;
                    }

                    Cursor previous = Cursor;
                    Cursor = Cursors.WaitCursor;

                    try
                    {
                        bool syncOk = await GitHub.Repository.Branch.IsUpdatedAsync(pb.BaseBranch, "main") ||
                            await GitHub.Repository.Branch.SyncBranchAsync(pb.BaseBranch, "main");
                        if (!syncOk)
                        {
                            MsgBox(
                                Program.Localization.Strings.GitHubStrings.NewBranch_Error,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                            item.Remove();
                            return;
                        }

                        Branch branch = await GitHub.Repository.Branch.CreateBranchAsync(newName, pb.BaseBranch);
                        if (branch == null)
                        {
                            MsgBox(
                                string.Format(Program.Localization.Strings.GitHubStrings.NewBranch_Error, newName),
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                            item.Remove();
                            return;
                        }

                        if (branch.Protected)
                        {
                            branch = await GitHub.Repository.Branch.SetBranchProtectionAsync(branch.Name, false);
                        }

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

                    if (oldName.Equals(newName, StringComparison.Ordinal))
                    {
                        return;
                    }

                    if (newName.Equals("main", StringComparison.OrdinalIgnoreCase))
                    {
                        MsgBox(
                            string.Format(
                                Program.Localization.Strings.GitHubStrings.Branch_CannotDoOperation_Protected,
                                "main"),
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        e.CancelEdit = true;
                        return;
                    }
                    if (existingBranch.Protected)
                    {
                        MsgBox(
                            string.Format(
                                Program.Localization.Strings.GitHubStrings.Branch_CannotDoOperation_Protected,
                                existingBranch.Name),
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
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
                            MsgBox(
                                Program.Localization.Strings.GitHubStrings.Branch_Rename_AlreadyExists,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
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

        private async void branchesView_DoubleClick(object sender, EventArgs e)
        {
            if (branchesView.SelectedItems.Count == 0)
            {
                return;
            }

            ListViewItem item = branchesView.SelectedItems[0];

            if (item.Tag is Branch branch && branch.Protected)
            {
                MsgBox(
                    string.Format(Program.Localization.Strings.GitHubStrings.Branch_CannotAccess_Protected, branch.Name),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    Program.Localization.Strings.GitHubStrings.Branch_CannotAccess_Protected_Tip);
                return;
            }

            Cursor = Cursors.WaitCursor;

            Program.Animator.HideSync(tablessControl1);
            tablessControl1.SelectedIndex = 1;

            groupBox4.Enabled = false;
            groupBox1.Enabled = false;

            Program.Animator.ShowSync(tablessControl1);

            GitHub.FileSystem.ShowHiddenFiles = Program.Settings.GitHub.ShowHiddenFiles;

            listView1.View = Program.Settings.GitHub.DefaultView;
            GitHub.FileSystem.View = Program.Settings.GitHub.DefaultView;

            await GitHub.FileSystem.SetBranch(item.Text, treeView1, listView1, breadcrumbControl1);

            groupBox4.Enabled = true;
            groupBox1.Enabled = true;

            Cursor = Cursors.Default;
        }

        private void branchesView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (branchesView.SelectedItems.Count > 0)
            {
                button14.Enabled = true;
                button10.Enabled = true;
                button11.Enabled = true;
                button12.Enabled = true;
                button17.Enabled = true;
            }
            else
            {
                button14.Enabled = false;
                button10.Enabled = false;
                button11.Enabled = false;
                button12.Enabled = false;
                button17.Enabled = false;
            }
        }

        private void breadcrumbControl1_StopRequested() { FileSystem.CancelCurrentOperation(); }

        private void btn_copy_Click(object sender, EventArgs e) { FileSystem.Init_Copy(); }

        private void btn_cut_Click(object sender, EventArgs e) { FileSystem.Init_Cut(); }

        private async void btn_delete_Click(object sender, EventArgs e)
        { FileSystem.ActionQueue.Enqueue(FileSystem.DeleteSelectedElementsAsync); }

        private void btn_new_Click(object sender, EventArgs e) { Forms.GitHub_ThemeUpload.ShowDialog(); }

        private void btn_new_MouseEnter(object sender, EventArgs e)
        {
            Transition.With(label15, nameof(label15.Text), ((sender as UI.WP.Button).Tag ?? string.Empty).ToString())
                .CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration_Quick));
        }

        private void btn_new_MouseLeave(object sender, EventArgs e)
        {
            Transition.With(label15, nameof(label15.Text), string.Empty)
                .CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration_Quick));
        }

        private void btn_paste_Click(object sender, EventArgs e) { FileSystem.ActionQueue.Enqueue(FileSystem.Paste); }

        private void btn_rename_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count < 1)
            {
                return;
            }

            listView1.SelectedItems[0]?.BeginEdit();
        }

        static string BuildAheadBar(int ahead, int behind, int maxBlocks = 7)
        {
            int total = ahead + behind;
            if (total == 0 || ahead == 0)
            {
                return "0";
            }

            // Compute proportion of total for Ahead
            int blocks = (int)Math.Round((double)ahead / total * maxBlocks, MidpointRounding.AwayFromZero);
            if (blocks == 0 && ahead > 0)
            {
                blocks = 1; // ensure visibility
            }

            string bar = new string('█', blocks).PadRight(maxBlocks);
            return ahead + " " + bar;
        }

        static string BuildBehindBar(int ahead, int behind, int maxBlocks = 7)
        {
            int total = ahead + behind;
            if (total == 0 || behind == 0)
            {
                return "0";
            }

            // Compute proportion of total for Behind
            int blocks = (int)Math.Round((double)behind / total * maxBlocks, MidpointRounding.AwayFromZero);
            if (blocks == 0 && behind > 0)
            {
                blocks = 1; // ensure visibility
            }

            string bar = new string('█', blocks).PadRight(maxBlocks);
            return bar + " " + behind;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GitHub.FileSystem.Cache.Clear();
            Program.Animator.HideSync(tablessControl1);
            tablessControl1.SelectedIndex = 0;
            Program.Animator.ShowSync(tablessControl1);
        }

        private async void button10_Click(object sender, EventArgs e)
        {
            if (branchesView.SelectedItems.Count == 0)
            {
                return;
            }

            ListViewItem item = branchesView?.SelectedItems[0];
            string branchName = branchesView?.SelectedItems[0].Text ?? string.Empty;

            if (string.IsNullOrEmpty(branchName))
            {
                return;
            }

            if (item.Tag is Branch branch && branch.Protected)
            {
                MsgBox(
                    string.Format(
                        Program.Localization.Strings.GitHubStrings.Branch_CannotDoOperation_Protected,
                        branch.Name),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    Program.Localization.Strings.GitHubStrings.Branch_CannotAccess_Protected_Tip);
                return;
            }

            if (MsgBox(
                    string.Format(Program.Localization.Strings.GitHubStrings.Branch_Delete, branchName),
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) ==
                DialogResult.Yes)
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
                    MsgBox(
                        string.Format(Program.Localization.Strings.GitHubStrings.Branch_Delete_Error, branchName),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }

                groupBox5.Enabled = true;
                branchesView.Enabled = true;
                progressBar1.Visible = false;
                Cursor = Cursors.Default;
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

                        Branch newBranch = await GitHub.Repository.Branch
                            .SetBranchProtectionAsync(branch.Name, !branch.Protected);

                        if (newBranch != null)
                        {
                            CompareResult compare = await Program.GitHub.Client.Repository.Commit
                                .Compare(
                                    GitHub.Repository.OriginalOwner,
                                    GitHub.Repository.Name,
                                    upstreamBranch.Commit.Sha,
                                    branch.Commit.Sha);
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

        private async void button13_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            breadcrumbControl1.StartMarquee();

            bool canCreatePullRequest = await GitHub.Repository.CanCreatePullRequestAsync();

            if (canCreatePullRequest &&
                Forms.GitHub_ConfirmPR.ShowDialog(branch: GitHub.Repository.Branch.Name) == DialogResult.OK)
            {
                // Example: get list of changed theme files in the branch
                List<string> changedFiles = [.. (await GitHub.Repository.Branch.GetChangedFilesAsync())];
                List<string> changedThemes = [.. changedFiles.Where(f => f.EndsWith(".wpth"))];
                List<string> changedPacks = [.. changedFiles.Where(f => f.EndsWith(".wptp"))];

                // Check which themes are newly added
                IEnumerable<Task<bool>> themeTasks = changedThemes.Select(
                    async f => await GitHub.Repository.Branch.IsNewFileAsync(f));
                bool[] themeResults = await Task.WhenAll(themeTasks);
                int addedThemes = themeResults.Count(r => r);
                int modifiedThemes = themeResults.Length - addedThemes;

                // Check which resource packs are newly added
                IEnumerable<Task<bool>> packTasks = changedPacks.Select(
                    async f => await GitHub.Repository.Branch.IsNewFileAsync(f));
                bool[] packResults = await Task.WhenAll(packTasks);
                int addedPacks = packResults.Count(r => r);
                int modifiedPacks = packResults.Length - addedPacks;

                string title = $"[Themes Publication] {User.GitHub.Login} – {GitHub.Repository.Branch.Name} ({changedThemes.Count} theme file{(changedThemes.Count != 1 ? "s" : string.Empty)}, {changedPacks.Count} pack{(changedPacks.Count != 1 ? "s" : string.Empty)} changed)";

                string body = $@"
### Pull Request Summary
This PR automatically updates {changedThemes.Count} theme file{(changedThemes.Count != 1 ? "s" : string.Empty)} and {changedPacks.Count} resource pack{(changedPacks.Count != 1 ? "s" : string.Empty)}.

**Author:** {User.GitHub.Login}  
**Branch:** {GitHub.Repository.Branch.Name}  
**WinPaletter Version:** {Program.Version}  
**WinPaletter Beta:** {Program.IsBeta}  
**UTC Date:** {DateTime.UtcNow:yyyy-MM-dd HH:mm}  

**Changes Breakdown (Themes):**  
- Added: {addedThemes} file{(addedThemes != 1 ? "s" : string.Empty)}  
- Modified: {modifiedThemes} file{(modifiedThemes != 1 ? "s" : string.Empty)}  

**Changes Breakdown (Resource Packs):**  
- Added: {addedPacks} pack{(addedPacks != 1 ? "s" : string.Empty)}  
- Modified: {modifiedPacks} pack{(modifiedPacks != 1 ? "s" : string.Empty)}  

**Theme Files:**  
{string.Join("\n", changedThemes.Select(f => $"- {f}"))}

**Resource Packs:**  
{string.Join("\n", changedPacks.Select(f => $"- {f}"))}

Generated automatically by WinPaletter. Please review the changes before merging.";

                PullRequest pr = await GitHub.Repository.CreatePullRequestAsync(title, body);

                if (pr is not null)
                {
                    Process.Start(pr.HtmlUrl);
                }
                else
                {
                    MsgBox(
                        Program.Localization.Strings.Messages.CouldntCreatePR,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }

            breadcrumbControl1.StopMarquee();
            Cursor = Cursors.Default;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (branchesView.SelectedItems.Count > 0)
            {
                ListViewItem item_selected = branchesView.SelectedItems[0];
                string branchName = GitHub.Repository.Branch
                    .SanitizeBranchName(GetAvailableItemText($"cloned-branch-from-{item_selected.Text}"));

                ListViewItem item = new()
                {
                    Text = branchName,
                    ImageKey = item_selected.ImageKey,
                    Tag = new PendingBranch { BaseBranch = (item_selected.Tag as Branch).Name }
                };
                branchesView.Items.Add(item);
                item.BeginEdit();
            }
        }

        private void button14_MouseEnter(object sender, EventArgs e)
        {
            Transition.With(label12, nameof(label12.Text), ((sender as UI.WP.Button).Tag ?? string.Empty).ToString())
                .CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration_Quick));
        }

        private void button14_MouseLeave(object sender, EventArgs e)
        {
            Transition.With(label12, nameof(label12.Text), string.Empty)
                .CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration_Quick));
        }

        private void button15_Click(object sender, EventArgs e)
        {
            Program.Animator.HideSync(tablessControl2);
            tablessControl2.SelectedIndex = 1;
            Program.Animator.ShowSync(tablessControl2);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            Program.Animator.HideSync(tablessControl2);
            tablessControl2.SelectedIndex = 0;
            Program.Animator.ShowSync(tablessControl2);
        }

        private async void button17_Click(object sender, EventArgs e)
        {
            string selectedPath = string.Empty;

            if (!OS.WXP)
            {
                using (VistaFolderBrowserDialog FD = new())
                {
                    if (FD.ShowDialog() == DialogResult.OK)
                    {
                        selectedPath = FD.SelectedPath;
                    }
                }
            }
            else
            {
                using (FolderBrowserDialog FD = new())
                {
                    if (FD.ShowDialog() == DialogResult.OK)
                    {
                        selectedPath = FD.SelectedPath;
                    }
                }
            }

            if (!string.IsNullOrEmpty(selectedPath) && !Directory.Exists(selectedPath))
            {
                Directory.CreateDirectory(selectedPath);
            }

            await FileSystem.DownloadSelectedItemsAsync(selectedPath);
        }

        private async void button17_Click_1(object sender, EventArgs e)
        {
            if (MsgBox(
                    Program.Localization.Strings.Messages.SyncingTip,
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    Program.Localization.Strings.Messages.ConfirmSync) ==
                DialogResult.Yes)
            {
                Cursor = Cursors.WaitCursor;

                if (await GitHub.Repository.Branch.SyncBranchAsync(GitHub.Repository.Branch.Name, "main"))
                {
                    await GetBranches();
                    MsgBox(
                        Program.Localization.Strings.Messages.SyncCompleted,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                else
                {
                    MsgBox(Program.Localization.Strings.Messages.SyncFailed, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                Cursor = Cursors.Default;
            }
        }

        private async void button18_Click(object sender, EventArgs e)
        {
            string branchName = GitHub.Repository.Branch.SanitizeBranchName(GetAvailableItemText("new-themes-branch"));

            ListViewItem item = new()
            {
                Text = branchName,
                ImageKey = "Branch_Unprotected_Unupdated",
                Tag = new PendingBranch { BaseBranch = "main" }
            };
            branchesView.Items.Add(item);
            item.BeginEdit();
        }

        private async void button19_Click(object sender, EventArgs e)
        {
            if (MsgBox(
                    Program.Localization.Strings.Messages.SyncingTip,
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    Program.Localization.Strings.Messages.ConfirmSync) ==
                DialogResult.Yes)
            {
                Cursor = Cursors.WaitCursor;

                if (await GitHub.Repository.Branch.SyncBranchAsync(GitHub.Repository.Branch.Name, "main"))
                {
                    await GetBranches();
                    await GitHub.FileSystem
                        .SetBranch(GitHub.Repository.Branch.Name, treeView1, listView1, breadcrumbControl1);
                    MsgBox(
                        Program.Localization.Strings.Messages.SyncCompleted,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                else
                {
                    MsgBox(Program.Localization.Strings.Messages.SyncFailed, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                Cursor = Cursors.Default;
            }
        }

        private async void button2_Click(object sender, EventArgs e) { await GitHub.FileSystem.GoBack(); }

        private async void button21_Click(object sender, EventArgs e)
        {
            if (Forms.GitHub_Login.ShowDialog() == DialogResult.OK && IsHandleCreated)
            {
                await LoadInternal();
            }
        }

        private void button22_Click(object sender, EventArgs e) { Process.Start(Links.GitHubProfileSettings); }

        private async void button23_Click(object sender, EventArgs e)
        {
            bool isLoggedIn = await Program.GitHub.IsLoggedInAsync();
            User.UpdateGitHubLoginStatus(isLoggedIn);

            if (!isLoggedIn)
            {
                MsgBox(
                    Program.Localization.Strings.Messages.GitHub_NotSignedUp,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            ShowTab(5);
        }

        private void button24_Click(object sender, EventArgs e) { Close(); }

        private void button25_Click(object sender, EventArgs e) { ShowTab(previousIndex > -1 ? previousIndex : 0); }

        private async void button4_Click(object sender, EventArgs e) { await GitHub.FileSystem.GoForward(); }

        private async void button5_Click(object sender, EventArgs e) { await GitHub.FileSystem.GoUp(); }

        private async void button6_Click(object sender, EventArgs e)
        { await GitHub.FileSystem.UpdateExplorerView(FileSystem.CurrentPath); }

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
                    Program.Settings.GitHub.DefaultView = view.view;
                    Program.Settings.GitHub.SaveViewOnly();
                }
                else
                {
                    item.Checked = false;
                }
            }
        }

        private async void button8_Click(object sender, EventArgs e)
        {
            breadcrumbControl1.StartMarquee();
            await GitHub.FileSystem.SearchAsync(textBox1.Text);
            breadcrumbControl1.StopMarquee();
        }

        private async void button9_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            breadcrumbControl1.StartMarquee();

            groupBox3.Enabled = false;

            bool forked = await GitHub.Repository.ExistsAsync(GitHub.Repository.Name);
            if (!forked)
            {
                forked = await GitHub.Repository.ForkAsync(GitHub.Repository.Name) is not null;
            }

            label8.Text = forked
                ? Program.Localization.Strings.GitHubStrings.ExplorerStatus_Forked
                : $"{Program.Localization.Strings.GitHubStrings.ExplorerStatus_NotForked} {Program.Localization.Strings.GitHubStrings.ExplorerStatus_SyncAndForkToManage}";
            if (forked)
            {
                await GetBranches();
            }
            button9.Enabled = !forked;

            groupBox3.Enabled = true;

            breadcrumbControl1.StopMarquee();
            Cursor = Cursors.Default;
        }

        private void Events_NetworkLost(object sender, EventArgs e)
        {
            if (IsHandleCreated)
            {
                ShowTab(3);
            }
        }

        private void Events_RateLimitExceeded(object sender, DateTime e)
        {
            if (IsHandleCreated)
            {
                Program.Animator.HideSync(tablessControl1);
                labelAlt3.Text = string.Format(
                    Program.Localization.Strings.GitHubStrings.API_RateLimited,
                    e.ToLocalTime());
                previousIndex = tablessControl1.SelectedIndex;
                tablessControl1.SelectedIndex = 4;
                Program.Animator.ShowSync(tablessControl1);
            }
        }

        private void Events_ViewChanged(object sender, View e)
        {
            UpdateViewButton(e);
        }

        private void FileSystem_CanDoIOChanged(object sender, bool e)
        {
            if (IsHandleCreated)
            {
                btn_cut.Enabled = e;
                btn_copy.Enabled = e;
                btn_rename.Enabled = e;
                btn_delete.Enabled = e;
                btn_download.Enabled = e;
            }
        }

        private void FileSystem_CanPasteChanged(object sender, bool e)
        {
            if (IsHandleCreated)
            {
                btn_paste.Enabled = e;
            }
        }

        private void FileSystem_Navigated(object sender, string path)
        {
            if (IsHandleCreated)
            {
                UpdateExplorerLayout();
            }
        }

        private void FileSystem_StatusLabelChanged(object sender, string e)
        {
            if (IsHandleCreated)
            {
                status_lbl.Text = e;
            }
        }

        private string GetAvailableItemText(string baseName)
        {
            string name = baseName;
            int i = 1;

            bool Exists(string text)
            {
                foreach (ListViewItem it in branchesView.Items)
                {
                    if (string.Equals(it.Text, text, StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                }

                return false;
            }

            while (Exists(name))
            {
                i++;
                name = $"{baseName}-{i}";
            }

            return name;
        }

        async Task GetBranches()
        {
            if (pendingBranchesFetches) return;
            pendingBranchesFetches = true;

            branchesView.Cursor = Cursors.WaitCursor;
            groupBox5.Enabled = false;
            button18.Enabled = false;

            branchesView.BeginUpdate();
            branchesView.Columns.Clear();
            branchesView.SmallImageList = imageList1;
            branchesView.Columns.Add(Program.Localization.Strings.GitHubStrings.Branch, 200);
            branchesView.Columns.Add(Program.Localization.Strings.GitHubStrings.LastUpdated, 220);
            branchesView.Columns.Add(Program.Localization.Strings.GitHubStrings.Branch_Ahead, 120, HorizontalAlignment.Right);
            branchesView.Columns.Add(Program.Localization.Strings.GitHubStrings.Branch_Behind, 120);
            branchesView.Columns.Add(Program.Localization.Strings.GitHubStrings.Committer, 140);
            branchesView.Columns.Add(Program.Localization.Strings.GitHubStrings.LastCommitMsg, 380);
            branchesView.Columns.Add("SHA", 70);
            branchesView.EndUpdate();

            IReadOnlyList<Branch> branches = await GitHub.Repository.Branch.GetBranchesAsync(true);

            repo = await Program.GitHub.Client.Repository.Get(GitHub.Repository.Owner, GitHub.Repository.Name);
            upstreamBranch = await Program.GitHub.Client.Repository.Branch.Get(GitHub.Repository.OriginalOwner, GitHub.Repository.Name, "main");

            SemaphoreSlim gate = new(6);
            List<Task<ListViewItem>> tasks = new(branches.Count);

            foreach (Branch branch in branches)
            {
                tasks.Add(
                    Task.Run(
                        async () =>
                        {
                            await gate.WaitAsync();
                            try
                            {
                                return await NewItemFromBranch(repo, branch, upstreamBranch);
                            }
                            finally
                            {
                                gate.Release();
                            }
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

            pendingBranchesFetches = false;
            button18.Enabled = true;
            groupBox5.Enabled = true;
            branchesView.Cursor = Cursors.Default;
        }

        private void GitHub_Mgr_ParentChanged(object sender, EventArgs e)
        {
            if (Parent != null && Parent is TabPage)
            {
                pin_button.Visible = false;
            }
            else
            {
                pin_button.Visible = true;
            }
        }

        string ImageKey(Branch branch, bool updated)
        {
            if (branch.Protected && updated)
            {
                return "Branch_Protected_Updated";
            }

            if (branch.Protected && !updated)
            {
                return "Branch_Protected_Unupdated";
            }

            if (!branch.Protected && updated)
            {
                return "Branch_Unprotected_Updated";
            }

            return "Branch_Unprotected_Unupdated";
        }

        async Task<ListViewItem> NewItemFromBranch(Octokit.Repository repo, Branch branch, Branch upstreamBranch)
        {
            GitHubClient client = Program.GitHub.Client;

            GitHubCommit commit = await client.Repository.Commit.Get(repo.Id, branch.Commit.Sha);
            CompareResult compare = await client.Repository.Commit
                .Compare(
                    GitHub.Repository.OriginalOwner,
                    GitHub.Repository.Name,
                    upstreamBranch.Commit.Sha,
                    branch.Commit.Sha);

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

        private void noNetworkPanel1_CloseClicked(object sender, EventArgs e) { Close(); }

        private async void noNetworkPanel1_RetryClicked(object sender, EventArgs e) { await LoadInternal(); }

        private void pin_button_Click(object sender, EventArgs e)
        { Forms.MainForm.tabsContainer1.AddFormIntoTab(this); }

        private void ShowTab(int index, bool animate = true)
        {
            if (animate)
            {
                Program.Animator.HideSync(tablessControl1);
            }

            tablessControl1.SelectedIndex = index;
            if (animate)
            {
                Program.Animator.ShowSync(tablessControl1);
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        { explorer_controls.Visible = tabControl1.SelectedIndex == 1 && tablessControl1.SelectedIndex == 1; }

        private async void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                await GitHub.FileSystem.UpdateExplorerView(FileSystem.CurrentPath);
            }
        }

        private void UpdateExplorerLayout()
        {
            button2.Enabled = GitHub.FileSystem.CanGoBack;
            button4.Enabled = GitHub.FileSystem.CanGoForward;
            button5.Enabled = GitHub.FileSystem.CanGoUp;

            button7.ImageGlyph = FileSystem.Views.Find(v => v.view == listView1.View).glyph;

            status_lbl.Text = FileSystem.StatusLabel;
        }

        private async Task UploadInternal(List<string> list)
        {
            breadcrumbControl1.Value = 0f;
            breadcrumbControl1.StartMarquee();

            float total = list.Count;
            float count = 0f;

            foreach (string file in list)
            {
                if (File.Exists(file))
                {
                    bool uploaded = await FileSystem.Upload(file);

                    if (uploaded)
                    {
                        await Task.Delay(500);
                    }
                }

                count++;
                breadcrumbControl1.Value = (count / total) * 100f;
                await Task.Yield();
            }

            breadcrumbControl1.FinishLoadingAnimation();
            breadcrumbControl1.Value = 0f;
        }

        private void url_lbl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string url = url_lbl.Text;
            if (!url.StartsWith("https://") || !url.StartsWith("http://"))
            {
                url = $"https://{url}";
            }

            Process.Start(url);
        }

        private void User_GitHubAvatarUpdated(object sender, EventArgs e)
        {
            UpdateAvatar();
        }

        private void UpdateAvatar()
        {
            if (IsHandleCreated)
            {
                Image old = pictureBox1.Image;
                using (Bitmap temp0 = new(User.GitHub_Avatar))
                using (Bitmap temp1 = temp0?.Resize(pictureBox1.Size))
                using (Bitmap temp2 = temp1?.ToCircular())
                {
                    pictureBox1.Image = new Bitmap(temp2);
                }
                old?.Dispose();
            }
        }

        private async void User_GitHubUserSwitch(Events.GitHubUserChangeEventArgs e)
        {
            if (IsHandleCreated)
            {
                await LoadInternal();
            }
        }

        public string ToFriendlyString(DateTimeOffset dateTime)
        {
            DateTimeOffset now = DateTimeOffset.Now;
            TimeSpan span = now - dateTime;

            if (span.TotalSeconds < 60)
            {
                return $"{(int)span.TotalSeconds} {((int)span.TotalSeconds > 1 ? Program.Localization.Strings.GitHubStrings.Timing_Seconds : Program.Localization.Strings.GitHubStrings.Timing_Second)} {Program.Localization.Strings.GitHubStrings.Timing_Ago}";
            }

            if (span.TotalMinutes < 60)
            {
                return $"{(int)span.TotalMinutes} {((int)span.TotalMinutes > 1 ? Program.Localization.Strings.GitHubStrings.Timing_Minutes : Program.Localization.Strings.GitHubStrings.Timing_Minute)} {Program.Localization.Strings.GitHubStrings.Timing_Ago}";
            }

            if (span.TotalHours < 24)
            {
                return $"{(int)span.TotalHours} {((int)span.TotalHours > 1 ? Program.Localization.Strings.GitHubStrings.Timing_Hours : Program.Localization.Strings.GitHubStrings.Timing_Hour)} {Program.Localization.Strings.GitHubStrings.Timing_Ago}";
            }

            int dayDiff = (now.Date - dateTime.Date).Days;
            if (dayDiff == 0)
            {
                return $"{Program.Localization.Strings.GitHubStrings.Timing_Today} {dateTime:hh:mm tt}";
            }

            if (dayDiff == 1)
            {
                return $"{Program.Localization.Strings.GitHubStrings.Timing_Yesterday} {dateTime:hh:mm tt}";
            }

            if (dayDiff == -1)
            {
                return $"{Program.Localization.Strings.GitHubStrings.Timing_Tomorrow} {dateTime:hh:mm tt}";
            }

            return dateTime.ToString("f");
        }

        public void UpdateGitHubLoginData()
        {
            if (User.GitHub_LoggedIn)
            {
                UpdateAvatar();

                label1.Text = User.GitHub.Login;
                url_lbl.Text = User.GitHub.HtmlUrl;

                followers_count_lbl.Font = Fonts.ConsoleLarge;
                following_count_lbl.Font = Fonts.ConsoleLarge;

                followers_count_lbl.Text = User.GitHub.Followers.ToString();
                following_count_lbl.Text = User.GitHub.Following.ToString();

                email_lbl.Text = User.GitHub.Email ?? Program.Localization.Strings.GitHubStrings.Overview_NoEmail;
                contry_lbl.Text = User.GitHub.Location ?? Program.Localization.Strings.GitHubStrings.Overview_NoLocation;

                created_lbl.Text = ToFriendlyString(User.GitHub.CreatedAt);
                updated_lbl.Text = ToFriendlyString(User.GitHub.UpdatedAt);
            }
        }

        public void UpdateViewButton(View view)
        {
            if (IsHandleCreated)
            {
                button7.ImageGlyph = FileSystem.Views.FirstOrDefault(x => x.view == view).glyph;
            }
        }

        public async Task UploadListAsync(List<string> list)
        { FileSystem.ActionQueue.Enqueue(async () => await UploadInternal(list)); }

        sealed class PendingBranch
        {
            public string BaseBranch { get; set; } = "main";
        }

        private void button26_Click(object sender, EventArgs e)
        {
            if (Forms.GitHub_FolderOptions.ShowDialog() == DialogResult.OK)
            {
                FileSystem.ShowHiddenFiles = Program.Settings.GitHub.ShowHiddenFiles;
                FileSystem.View = Program.Settings.GitHub.DefaultView;
            }
        }
    }
}