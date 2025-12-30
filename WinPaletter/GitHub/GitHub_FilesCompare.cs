using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using static WinPaletter.GitHub.FileSystem;

namespace WinPaletter
{
    public partial class GitHub_FilesCompare : Form
    {
        public FileConflictAction Action { get; private set; }
        public Dictionary<string, bool> ReplaceMap { get; private set; } = [];

        public GitHub_FilesCompare()
        {
            InitializeComponent();
        }

        public void ShowInfo(FileConflictInfo info)
        {
            Action = FileConflictAction.Skip;
            ReplaceMap.Clear();

            int count = info.TotalCount;
            string sourceDir = Path.GetDirectoryName(info.SourcePath);
            string destDir = Path.GetDirectoryName(info.DestinationPath);
            string fileName = Path.GetFileName(info.SourcePath);

            radioButton1.Text = $"{Program.Lang.Strings.GitHubStrings.Explorer_Compare_FilesFrom} {sourceDir}";
            radioButton2.Text = $"{Program.Lang.Strings.GitHubStrings.Explorer_Compare_FilesAlreadyIn} {destDir}";

            listViewSource.Items.Clear();
            listViewDestination.Items.Clear();

            // Source ListView (the files being moved/copied)
            if (count == 1)
            {
                ListViewItem item_source = new([fileName, info.SourceSize.ToStringFileSize()]) { Checked = true };
                listViewSource.Items.Add(item_source);

                ListViewItem item_destination = new([fileName, info.DestinationSize.ToStringFileSize()]) { Checked = false };
                listViewDestination.Items.Add(item_destination);
            }
            else
            {
                foreach (var f in info.SourceFiles)
                {
                    ListViewItem item = new([f.Name, f.Size.ToStringFileSize()]) { Checked = true };
                    listViewSource.Items.Add(item);
                }

                foreach (var f in info.DestinationFiles)
                {
                    ListViewItem item = new([f.Name, f.Size.ToStringFileSize()]) { Checked = true };
                    listViewDestination.Items.Add(item);
                }
            }
        }


        private void GitHub_FilesCompare_Load(object sender, EventArgs e)
        {
            ApplyStyle(this);
            this.LoadLanguage();
        }

        private void listViewSource_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (e.Item.Checked && listViewDestination.Items.Count > e.Item.Index) listViewDestination.Items[e.Item.Index].Checked = false;
        }

        private void listViewDestination_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (e.Item.Checked && listViewSource.Items.Count > e.Item.Index) listViewSource.Items[e.Item.Index].Checked = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ReplaceMap.Clear();

            for (int i = 0; i < listViewSource.Items.Count; i++)
            {
                bool srcChecked = listViewSource.Items[i].Checked;
                bool dstChecked = listViewDestination.Items[i].Checked;

                if (!srcChecked && !dstChecked)
                {
                    MsgBox(Program.Lang.Strings.GitHubStrings.Explorer_Compare_SelectFiles);
                    return;
                }

                string fileName = listViewSource.Items[i].Text;

                // source checked = replace
                ReplaceMap[fileName] = srcChecked;
            }

            Action = FileConflictAction.Filter;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked) { for (int i = 0; i < listViewSource.Items.Count; i++) { listViewSource.Items[i].Checked = true; } }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked) { for (int i = 0; i < listViewDestination.Items.Count; i++) { listViewDestination.Items[i].Checked = true; } }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
