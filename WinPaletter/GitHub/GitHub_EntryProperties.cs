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
        }

        public async void Load_Entry(ListViewItem listViewItem)
        {
            if (listViewItem is null) return;
            if (listViewItem.Tag is not Octokit.RepositoryContent content) return;
            boundListViewItem = listViewItem;

            this.rc = content;
            this.Text = string.Format(Program.Lang.Strings.General.Properties_Entry, content.Name);

            IReadOnlyList<GitHubCommit> commits = await Program.GitHub.Client.Repository.Commit.GetAll(GitHub.Repository.Owner, GitHub.Repository.Name, new CommitRequest { Path = content.Path, Sha = GitHub.Repository.Branch.Name });
            GitHubCommit latestCommit = commits.First();

            pictureBox1.Image?.Dispose();
            pictureBox1.Image = listViewItem.ListView.LargeImageList.Images[listViewItem.ImageKey.Replace("ghost_", "")];

            Icon?.Dispose();
            Icon = NativeMethods.Shell32.GetIconFromExtension(GitHub.FileSystem.GetExtension(content.Name), NativeMethods.Shell32.IconSize.Small) ?? NativeMethods.Helpers.GetSystemIcon(Shell32.SHSTOCKICONID.FOLDER, Shell32.SHGSI.ICON);

            textBox1.Text = content.Name.Remove(0, content.Name.StartsWith(".") ? 1 : 0);
            label6.Text = content.Path;
            label15.Text = content.Sha;
            label18.Text = content.Encoding ?? Program.Lang.Strings.General.Unknown;
            label5.Text = $"{content.Size} {Program.Lang.Strings.General.BytesSize} ({content.Size.ToStringFileSize()})";
            label4.Text = GitHub.FileSystem.GetExtension(content.Name);
            label3.Text = GitHub.FileSystem.FileTypeProvider.Invoke(content) ?? Program.Lang.Strings.General.Unknown;

            if (latestCommit is not null)
            {
                label9.Text = latestCommit.Commit.Author.Name;
                label11.Text = Forms.GitHub_Mgr.ToFriendlyString(latestCommit.Commit.Author.Date);
                textBox2.Text = latestCommit.Commit.Message.Split(['\r', '\n'], StringSplitOptions.RemoveEmptyEntries)[0];
            }
            else
            {
                label9.Text = Program.Lang.Strings.General.Unknown;
                label11.Text = Program.Lang.Strings.General.Unknown;
                textBox2.Text = Program.Lang.Strings.General.Unknown;
            }

            checkBox1.Checked = content.Name.StartsWith(".");
            previousReadOnlyState = checkBox1.Checked;

            // Advanced details
            listView1.Items.Clear();
            AddPropertiesRecursive(content, listView1);
            if (latestCommit is not null) AddPropertiesRecursive(latestCommit, listView1);

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
    }
}
