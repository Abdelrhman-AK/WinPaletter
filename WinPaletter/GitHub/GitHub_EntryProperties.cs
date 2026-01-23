using Newtonsoft.Json;
using Octokit;
using Ressy.HighLevel.Versions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinPaletter.NativeMethods;
using static WinPaletter.Theme.Structures.WinTerminal;

namespace WinPaletter
{
    public partial class GitHub_EntryProperties : Form
    {
        private Octokit.RepositoryContent rc;
        private bool previousReadOnlyState;
        private ListViewItem boundListViewItem;

        public GitHub_EntryProperties()
        {
            InitializeComponent();
        }

        private void GitHub_EntryProperties_Load(object sender, EventArgs e)
        {
            ApplyStyle(this);
            this.LoadLanguage();

            textBox2.Font = Fonts.ConsoleMedium;
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

            label29.Text = $"{files} {Program.Lang.Strings.Extensions.Files}, {dirs} {Program.Lang.Strings.Extensions.Folders}";

            long size = 0;
            foreach (var item in listViewItems)
            {
                if (item.Tag is RepositoryContent rc)
                {
                    size += GitHub.FileSystem.Cache.GetSize(rc.Path);
                    AddPropertiesRecursive(rc, listView2, rc.Name);
                }
            }

            label21.Text = $"{size} {Program.Lang.Strings.General.BytesSize} ({size.ToStringFileSize()})";

            if (listViewItems[0].Tag is RepositoryContent rc_default)
            {
                label22.Text = GitHub.FileSystem.GetParent(rc_default.Path);
            }

            Task.Run(async () =>
            {
                Invoke(() => progressBar1.Visible = true);

                foreach (var item in listViewItems)
                {
                    if (item.Tag is RepositoryContent rcx)
                    {
                        IReadOnlyList<GitHubCommit> commits = await Program.GitHub.Client.Repository.Commit.GetAll(GitHub.Repository.Owner, GitHub.Repository.Name, new CommitRequest { Path = rcx.Path, Sha = GitHub.Repository.Branch.Name });
                        GitHubCommit latestCommit = commits.First();

                        Invoke(() =>
                        {
                            if (latestCommit is not null) AddPropertiesRecursive(latestCommit, listView2, rcx.Name);
                        });
                    }
                }
            });

            ShowDialog();
        }

        public void Load_Entry(ListViewItem listViewItem)
        {
            if (listViewItem is null) return;
            if (listViewItem.Tag is not Octokit.RepositoryContent content) return;
            boundListViewItem = listViewItem;

            tablessControl1.SelectedIndex = 0;

            this.rc = content;
            this.Text = string.Format(Program.Lang.Strings.General.Properties_Entry, content.Name);

            pictureBox1.Image?.Dispose();
            pictureBox1.Image = listViewItem.ListView.LargeImageList.Images[listViewItem.ImageKey.Replace("ghost_", "")];

            Icon?.Dispose();

            if (content.Type == ContentType.File)
            {
                Icon = NativeMethods.Shell32.GetIconFromExtension(GitHub.FileSystem.GetExtension(content.Name), NativeMethods.Shell32.IconSize.Small);
                label4.Text = GitHub.FileSystem.GetExtension(content.Name);
                label3.Text = GitHub.FileSystem.FileTypeProvider.Invoke(content) ?? Program.Lang.Strings.General.Unknown;
            }
            else if (content.Type == ContentType.Dir)
            {
                Icon = Assets.GitHubMgr.folder_web_16.ToIcon();
                label4.Text = Program.Lang.Strings.Extensions.Folder;
                label3.Text = Program.Lang.Strings.Extensions.Folder;
            }

            textBox1.Text = content.Name.Remove(0, content.Name.StartsWith(".") ? 1 : 0);
            label6.Text = content.Path;
            label15.Text = content.Sha;
            label18.Text = content.Encoding ?? Program.Lang.Strings.General.Unknown;
            label5.Text = $"{GitHub.FileSystem.Cache.GetSize(content.Path)} {Program.Lang.Strings.General.BytesSize} ({GitHub.FileSystem.Cache.GetSize(content.Path).ToStringFileSize()})";

            checkBox1.Checked = content.Name.StartsWith(".");
            previousReadOnlyState = checkBox1.Checked;

            label9.Text = string.Empty;
            label11.Text = string.Empty;
            textBox2.Text = string.Empty;

            // Advanced details
            listView1.Items.Clear();
            AddPropertiesRecursive(content, listView1);

            Task.Run(async () =>
            {
                Invoke(() => progressBar1.Visible = true);

                IReadOnlyList<GitHubCommit> commits = await Program.GitHub.Client.Repository.Commit.GetAll(GitHub.Repository.Owner, GitHub.Repository.Name, new CommitRequest { Path = content.Path, Sha = GitHub.Repository.Branch.Name });
                GitHubCommit latestCommit = commits.First();

                Invoke(() => 
                {
                    if (latestCommit is not null)
                    {
                        label9.Text = latestCommit.Commit.Author.Name;
                        label11.Text = Forms.GitHub_Mgr.ToFriendlyString(latestCommit.Commit.Author.Date);
                        textBox2.Text = latestCommit.Commit.Message;
                    }
                    else
                    {
                        label9.Text = Program.Lang.Strings.General.Unknown;
                        label11.Text = Program.Lang.Strings.General.Unknown;
                        textBox2.Text = Program.Lang.Strings.General.Unknown;
                    }

                    progressBar1.Visible = false;

                    if (latestCommit is not null) AddPropertiesRecursive(latestCommit, listView1);
                });
            });

            ShowDialog();
        }

        static void AddPropertiesRecursive(object obj, ListView listView, string prefix = "")
        {
            if (obj == null) return;
            Type type = obj.GetType();

            foreach (var prop in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (prop.GetIndexParameters().Length > 0) continue; // skip indexers

                object value;
                try { value = prop.GetValue(obj); }
                catch { continue; }

                if (value == null) continue;

                string name = string.IsNullOrEmpty(prefix) ? prop.Name : $"{prefix}.{prop.Name}";

                // Skip generic collections or unhelpful types
                if (value is string || value.GetType().IsValueType)
                {
                    listView.Items.Add(new ListViewItem([name, value.ToString()]));
                }
                else if (value is IEnumerable enumerable && value is not string)
                {
                    // If collection has items, show count
                    var enumerator = enumerable.GetEnumerator();
                    if (enumerator.MoveNext())
                        listView.Items.Add(new ListViewItem([name, $"Collection[{enumerable.Cast<object>().Count()}]"]));
                }
                else
                {
                    // Recursively explore complex types
                    AddPropertiesRecursive(value, listView, name);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            await Apply();
            DialogResult = DialogResult.OK;
            Close();
        }

        private async Task Apply()
        {
            Cursor = Cursors.WaitCursor;

            string oldName = rc.Name;
            string newName = (checkBox1.Checked ? "." : "") + textBox1.Text.Trim();

            if (oldName != newName)
            {
                string parent = GitHub.FileSystem.GetParent(rc.Path);
                string fullPath = string.IsNullOrEmpty(parent) ? newName : $"{parent}/{newName}";

                GitHub.FileSystem.Entry result = await GitHub.FileSystem.MoveFileAsync(rc.Path, fullPath, $"{(checkBox1.Checked ? "Hide" : "Unhide")} '{rc.Path}' by {GitHub.Repository.Owner}");

                if (result is not null && boundListViewItem is not null)
                {
                    rc = result.Content;

                    // Update properties
                    boundListViewItem.Text = result.Name;
                    boundListViewItem.Tag = result.Content;

                    string imageKey = boundListViewItem.ImageKey.Replace("ghost_", "");
                    if (checkBox1.Checked) imageKey = "ghost_" + imageKey;
                    boundListViewItem.ImageKey = imageKey;
                }
            }

            Cursor = Cursors.Default;
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            await Apply();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dlg = new() { Filter = Program.Filters.JSON, Title = Program.Lang.Strings.Extensions.SaveJSON })
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
            using (SaveFileDialog dlg = new() { Filter = Program.Filters.JSON, Title = Program.Lang.Strings.Extensions.SaveJSON })
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
