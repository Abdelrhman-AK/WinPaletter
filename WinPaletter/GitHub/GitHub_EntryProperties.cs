using Octokit;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinPaletter
{
    public partial class GitHub_EntryProperties : UI.WP.Form
    {
        private Octokit.RepositoryContent rc;
        private ListViewItem boundListViewItem;

        // Pending commit fetch for the single-entry tab; set in Load_Core, consumed in Shown event.
        private string _pendingCommitPath;
        // Pending commit fetches for the multi-entry tab; set in Load_Entries, consumed in Shown event.
        private List<(string path, string displayName)> _pendingMultiCommitPaths;

        // Cancels any in-flight fetch when the form is re-opened before the previous one finishes.
        private CancellationTokenSource _fetchCts;

        public GitHub_EntryProperties()
        {
            InitializeComponent();
            Shown += GitHub_EntryProperties_Shown;
        }

        private void GitHub_EntryProperties_Load(object sender, EventArgs e)
        {
            textBox2.Font = Fonts.ConsoleMedium;
        }

        // Fires every time ShowDialog() brings the form into its modal loop — guaranteed to run
        // after the UI is fully visible, so Invoke/BeginInvoke are safe on every open.
        private async void GitHub_EntryProperties_Shown(object sender, EventArgs e)
        {
            // Cancel any previous in-flight fetch (e.g. user closed and re-opened before it finished).
            _fetchCts?.Cancel();
            _fetchCts = new CancellationTokenSource();
            CancellationToken ct = _fetchCts.Token;

            if (_pendingCommitPath is not null)
            {
                await FetchAndApplySingleCommitAsync(_pendingCommitPath, ct);
                _pendingCommitPath = null;
            }
            else if (_pendingMultiCommitPaths is not null)
            {
                await FetchAndApplyMultiCommitsAsync(_pendingMultiCommitPaths, ct);
                _pendingMultiCommitPaths = null;
            }
        }

        private async Task FetchAndApplySingleCommitAsync(string path, CancellationToken ct)
        {
            progressBar1.Visible = true;

            try
            {
                IReadOnlyList<GitHubCommit> commits = await Program.GitHub.Client.Repository.Commit.GetAll(
                    GitHub.Repository.Owner,
                    GitHub.Repository.Name,
                    new CommitRequest { Path = path, Sha = GitHub.Repository.Branch.Name });

                if (ct.IsCancellationRequested) return;

                GitHubCommit latestCommit = commits.FirstOrDefault();

                if (latestCommit is not null)
                {
                    label9.Text = latestCommit.Commit.Author.Name;
                    label11.Text = Forms.GitHub_Mgr.ToFriendlyString(latestCommit.Commit.Author.Date);
                    textBox2.Text = latestCommit.Commit.Message;
                    AddPropertiesRecursive(latestCommit, listView1);
                }
                else
                {
                    label9.Text = Program.Localization.Strings.General.Unknown;
                    label11.Text = Program.Localization.Strings.General.Unknown;
                    textBox2.Text = Program.Localization.Strings.General.Unknown;
                }
            }
            catch (OperationCanceledException) { }
            finally
            {
                if (!ct.IsCancellationRequested) progressBar1.Visible = false;
            }
        }

        private async Task FetchAndApplyMultiCommitsAsync(List<(string path, string displayName)> entries, CancellationToken ct)
        {
            progressBar1.Visible = true;

            try
            {
                foreach ((string path, string displayName) in entries)
                {
                    if (ct.IsCancellationRequested) return;

                    IReadOnlyList<GitHubCommit> commits = await Program.GitHub.Client.Repository.Commit.GetAll(
                        GitHub.Repository.Owner,
                        GitHub.Repository.Name,
                        new CommitRequest { Path = path, Sha = GitHub.Repository.Branch.Name });

                    if (ct.IsCancellationRequested) return;

                    GitHubCommit latestCommit = commits.FirstOrDefault();
                    if (latestCommit is not null) AddPropertiesRecursive(latestCommit, listView2, displayName);
                }
            }
            catch (OperationCanceledException) { }
            finally
            {
                if (!ct.IsCancellationRequested) progressBar1.Visible = false;
            }
        }

        public void Load_Entries(List<ListViewItem> listViewItems)
        {
            if (listViewItems is null || listViewItems.Count == 0) return;
            if (listViewItems.Count == 1)
            {
                Load_Entry(listViewItems[0]);
                return;
            }

            progressBar1.Visible = false;
            tablessControl1.SelectedIndex = 1;

            Icon?.Dispose();
            Icon = Assets.GitHubMgr.MultipleFiles;

            listView2.Items.Clear();

            int files = listViewItems.Count(i => i.Tag is RepositoryContent rc && rc.Type == ContentType.File);
            int dirs = listViewItems.Count(i => i.Tag is RepositoryContent rc && rc.Type == ContentType.Dir);

            label29.Text = $"{files} {Program.Localization.Strings.Extensions.Files}, {dirs} {Program.Localization.Strings.Extensions.Folders}";

            long size = 0;
            foreach (ListViewItem item in listViewItems)
            {
                if (item.Tag is RepositoryContent rc)
                {
                    size += GitHub.FileSystem.Cache.GetSize(rc.Path);
                    AddPropertiesRecursive(rc, listView2, rc.Name);
                }
            }

            label21.Text = $"{size} {Program.Localization.Strings.General.BytesSize} ({size.ToStringFileSize()})";

            if (listViewItems[0].Tag is RepositoryContent rc_default)
            {
                label22.Text = GitHub.FileSystem.GetParent(rc_default.Path);
            }

            // Schedule commit fetches; the Shown event will execute them once the modal loop is running.
            _pendingCommitPath = null;
            _pendingMultiCommitPaths = listViewItems
                .Where(i => i.Tag is RepositoryContent)
                .Select(i => (path: ((RepositoryContent)i.Tag).Path, displayName: ((RepositoryContent)i.Tag).Name))
                .ToList();

            ShowDialog();
        }

        // Shared core loader used by both Load_Entry and Load_Folder_From_RepositoryContent.
        // image: the large icon shown in pictureBox1. boundItem: the ListView row to update on rename, or null.
        private void Load_Core(Octokit.RepositoryContent content, System.Drawing.Image image, ListViewItem boundItem, bool readOnly)
        {
            tablessControl1.SelectedIndex = 0;
            boundListViewItem = boundItem;
            rc = content;

            this.Text = string.Format(Program.Localization.Strings.General.Properties_Entry, content.Name);

            pictureBox1.Image?.Dispose();
            pictureBox1.Image = image;

            textBox1.Enabled = !readOnly;
            checkBox1.Enabled = !readOnly;

            Icon?.Dispose();

            if (content.Type == ContentType.File)
            {
                Icon = NativeMethods.Shell32.GetIconFromExtension(GitHub.FileSystem.GetExtension(content.Name), NativeMethods.Shell32.IconSize.Small);
                label4.Text = GitHub.FileSystem.GetExtension(content.Name);
                label3.Text = GitHub.FileSystem.FileTypeProvider.Invoke(content) ?? Program.Localization.Strings.General.Unknown;
            }
            else if (content.Type == ContentType.Dir)
            {
                Icon = Assets.GitHubMgr.folder_web_16.ToIcon();
                label4.Text = Program.Localization.Strings.Extensions.Folder;
                label3.Text = Program.Localization.Strings.Extensions.Folder;
            }

            textBox1.Text = content.Name.Remove(0, content.Name.StartsWith(".") ? 1 : 0);
            label6.Text = content.Path;
            label15.Text = content.Sha;
            label18.Text = content.Encoding ?? Program.Localization.Strings.General.Unknown;
            label5.Text = $"{GitHub.FileSystem.Cache.GetSize(content.Path)} {Program.Localization.Strings.General.BytesSize} ({GitHub.FileSystem.Cache.GetSize(content.Path).ToStringFileSize()})";

            checkBox1.Checked = content.Name.StartsWith(".");

            // Reset commit fields before the async fetch so stale data from a previous open is never shown.
            label9.Text = string.Empty;
            label11.Text = string.Empty;
            textBox2.Text = string.Empty;

            listView1.Items.Clear();
            AddPropertiesRecursive(content, listView1);

            // Schedule commit fetch for this path; the Shown event will execute it once the modal loop is running.
            _pendingMultiCommitPaths = null;
            _pendingCommitPath = content.Path;

            ShowDialog();
        }

        public void Load_Folder_From_RepositoryContent(Octokit.RepositoryContent content, bool readOnly = false)
        {
            Load_Core(content, Assets.GitHubMgr.folder_web_48, null, readOnly);
        }

        public void Load_Entry(ListViewItem listViewItem, bool readOnly = false)
        {
            if (listViewItem is null) return;
            if (listViewItem.Tag is not Octokit.RepositoryContent content) return;

            System.Drawing.Image image = listViewItem.ListView.LargeImageList.Images[listViewItem.ImageKey.Replace("ghost_", string.Empty)];
            Load_Core(content, image, listViewItem, readOnly);
        }

        static void AddPropertiesRecursive(object obj, ListView listView, string prefix = "")
        {
            if (obj == null) return;
            Type type = obj.GetType();

            foreach (PropertyInfo prop in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (prop.GetIndexParameters().Length > 0) continue;

                object value;
                try { value = prop.GetValue(obj); }
                catch { continue; }

                if (value == null) continue;

                string name = string.IsNullOrEmpty(prefix) ? prop.Name : $"{prefix}.{prop.Name}";

                if (value is string || value.GetType().IsValueType)
                {
                    listView.Items.Add(new ListViewItem([name, value.ToString()]));
                }
                else if (value is IEnumerable enumerable && value is not string)
                {
                    var enumerator = enumerable.GetEnumerator();
                    if (enumerator.MoveNext())
                        listView.Items.Add(new ListViewItem([name, $"Collection[{enumerable.Cast<object>().Count()}]"]));
                }
                else
                {
                    AddPropertiesRecursive(value, listView, name);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _fetchCts?.Cancel();
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            await Apply(rc.Type == ContentType.Dir);
            DialogResult = DialogResult.OK;
            Close();
        }

        private async Task Apply(bool isADir = false)
        {
            Cursor = Cursors.WaitCursor;

            string oldName = rc.Name;
            string newName = (checkBox1.Checked ? "." : string.Empty) + textBox1.Text.Trim();

            if (oldName != newName)
            {
                string parent = GitHub.FileSystem.GetParent(rc.Path);
                string fullPath = string.IsNullOrEmpty(parent) ? newName : $"{parent}/{newName}";

                GitHub.FileSystem.Entry result = !isADir ?
                    await GitHub.FileSystem.MoveFileAsync(rc.Path, fullPath, $"{(checkBox1.Checked ? "Hide" : "Unhide")} '{rc.Path}' by {GitHub.Repository.Owner}") :
                    await GitHub.FileSystem.MoveDirectoryAsync(rc.Path, fullPath, $"{(checkBox1.Checked ? "Hide" : "Unhide")} '{rc.Path}' by {GitHub.Repository.Owner}");

                if (result is not null && boundListViewItem is not null)
                {
                    rc = result.Content;

                    boundListViewItem.Text = result.Name;
                    boundListViewItem.Tag = result.Content;

                    string imageKey = boundListViewItem.ImageKey.Replace("ghost_", string.Empty);
                    if (checkBox1.Checked) imageKey = "ghost_" + imageKey;
                    boundListViewItem.ImageKey = imageKey;
                }
            }

            Cursor = Cursors.Default;
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            await Apply(rc.Type == ContentType.Dir);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dlg = new() { Filter = Program.Filters.JSON, Title = Program.Localization.Strings.Extensions.SaveJSON })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    System.IO.File.WriteAllText(dlg.FileName, listView1.ToJson());
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(listView1.ToJson());
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            Clipboard.SetText(listView1.SelectedItems[0].SubItems[1].Text);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dlg = new() { Filter = Program.Filters.JSON, Title = Program.Localization.Strings.Extensions.SaveJSON })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    System.IO.File.WriteAllText(dlg.FileName, listView2.ToJson());
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(listView2.ToJson());
        }
    }
}