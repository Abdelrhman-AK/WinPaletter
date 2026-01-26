using System;
using System.Media;
using System.Windows.Forms;
using static WinPaletter.GitHub.FileSystem;

namespace WinPaletter
{
    public partial class GitHub_FileConflict : Form
    {
        public FileConflictAction Action { get; private set; }
        FileConflictInfo _info = null;
        public enum Operation { Copy, Move };

        public GitHub_FileConflict()
        {
            InitializeComponent();
        }

        private void GitHub_FileConflict_Load(object sender, EventArgs e)
        {
            ApplyStyle(this);
            this.Localize();
            CustomSystemSounds.Exclamation.Play();
        }

        public void ShowInfo(FileConflictInfo info, Operation operation)
        {
            _info = info;

            int count = info.TotalCount;
            string sourceDir = ParentDirectoryName(info.SourcePath);
            string destDir = ParentDirectoryName(info.DestinationPath);
            string fileName = FileName(info.SourcePath);

            // Label1: operation info
            if (count == 1)
                label1.Text = $"{(operation == Operation.Copy ? Program.Localization.Strings.GitHubStrings.Explorer_Conflict_Copying : Program.Localization.Strings.GitHubStrings.Explorer_Conflict_Moving)} 1 {Program.Localization.Strings.GitHubStrings.Explorer_File} {Program.Localization.Strings.GitHubStrings.Explorer_Conflict_From} {sourceDir} {Program.Localization.Strings.GitHubStrings.Explorer_Conflict_To} {destDir}";
            else
                label1.Text = $"{(operation == Operation.Copy ? Program.Localization.Strings.GitHubStrings.Explorer_Conflict_Copying : Program.Localization.Strings.GitHubStrings.Explorer_Conflict_Moving)} {count} {Program.Localization.Strings.GitHubStrings.Explorer_Files} {Program.Localization.Strings.GitHubStrings.Explorer_Conflict_From} {sourceDir} {Program.Localization.Strings.GitHubStrings.Explorer_Conflict_To} {destDir}";

            // Label2: conflict description
            if (count == 1) label2.Text = string.Format(Program.Localization.Strings.GitHubStrings.Explorer_Conflict_DestHasFile, fileName);
            else label2.Text = string.Format(Program.Localization.Strings.GitHubStrings.Explorer_Conflict_DestHasFiles, count);

            // Button texts
            if (count == 1)
            {
                button1.Text = Program.Localization.Strings.GitHubStrings.Explorer_Conflict_Replace2Files;
                button2.Text = Program.Localization.Strings.GitHubStrings.Explorer_Conflict_Skip2Files;
                button3.Text = Program.Localization.Strings.GitHubStrings.Explorer_Conflict_Compare2Files;
            }
            else
            {
                button1.Text = Program.Localization.Strings.GitHubStrings.Explorer_Conflict_ReplaceFiles;
                button2.Text = Program.Localization.Strings.GitHubStrings.Explorer_Conflict_SkipFiles;
                button3.Text = Program.Localization.Strings.GitHubStrings.Explorer_Conflict_CompareFiles;
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            Action = FileConflictAction.ReplaceAll;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Action = FileConflictAction.Skip;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Forms.GitHub_FilesCompare.ShowInfo(_info);

            if (Forms.GitHub_FilesCompare.ShowDialog() == DialogResult.OK)
            {
                Action = FileConflictAction.Filter;
                _info.ReplaceMap = Forms.GitHub_FilesCompare.ReplaceMap;
                DialogResult = DialogResult.OK;
            }
            else
            {
                Action = FileConflictAction.Skip;
                DialogResult = DialogResult.OK;
            }

            Close();
        }
    }
}
