using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace WinPaletter
{
    public partial class GitHub_FolderOptions : WinPaletter.UI.WP.Form
    {
        public GitHub_FolderOptions()
        {
            InitializeComponent();
        }

        private void GitHub_FolderOptions_Load(object sender, EventArgs e)
        {
            radioImage1.Text = GitHub.FileSystem.Views.FirstOrDefault(x => x.view == View.Details).label;
            radioImage2.Text = GitHub.FileSystem.Views.FirstOrDefault(x => x.view == View.List).label;
            radioImage3.Text = GitHub.FileSystem.Views.FirstOrDefault(x => x.view == View.Tile).label;
            radioImage5.Text = GitHub.FileSystem.Views.FirstOrDefault(x => x.view == View.SmallIcon).label;
            radioImage4.Text = GitHub.FileSystem.Views.FirstOrDefault(x => x.view == View.LargeIcon).label;

            LoadSettings();
        }

        private void LoadSettings()
        {
            checkBox1.Checked = Program.Settings.GitHub.ShowHiddenFiles;

            radioButton1.Checked = Program.Settings.GitHub.FilesLinking == Settings.Structures.GitHub.FilesLinkingMode.AskBeforeExecute;
            radioButton2.Checked = Program.Settings.GitHub.FilesLinking == Settings.Structures.GitHub.FilesLinkingMode.AlwaysExecute;
            radioButton3.Checked = Program.Settings.GitHub.FilesLinking == Settings.Structures.GitHub.FilesLinkingMode.NeverExecute;

            switch (Program.Settings.GitHub.DefaultView)
            {
                case View.Details:
                    radioImage1.Checked = true;
                    break;
                case View.List:
                    radioImage2.Checked = true; break;
                case View.Tile:
                    radioImage3.Checked = true; break;
                case View.SmallIcon:
                    radioImage5.Checked = true; break;
                case View.LargeIcon:
                    radioImage4.Checked = true; break;
                default:
                    radioImage1.Checked = true;
                    break;
            }
        }

        private void SaveSettings()
        {
            if (radioImage1.Checked) Program.Settings.GitHub.DefaultView = View.Details;
            else if (radioImage2.Checked) Program.Settings.GitHub.DefaultView = View.List;
            else if (radioImage3.Checked) Program.Settings.GitHub.DefaultView = View.Tile;
            else if (radioImage5.Checked) Program.Settings.GitHub.DefaultView = View.SmallIcon;
            else if (radioImage4.Checked) Program.Settings.GitHub.DefaultView = View.LargeIcon;
            else Program.Settings.GitHub.DefaultView = View.Details;

            Program.Settings.GitHub.ShowHiddenFiles = checkBox1.Checked;

            if (radioButton1.Checked) Program.Settings.GitHub.FilesLinking = Settings.Structures.GitHub.FilesLinkingMode.AskBeforeExecute;
            else if (radioButton2.Checked) Program.Settings.GitHub.FilesLinking = Settings.Structures.GitHub.FilesLinkingMode.AlwaysExecute;
            else if (radioButton3.Checked) Program.Settings.GitHub.FilesLinking = Settings.Structures.GitHub.FilesLinkingMode.NeverExecute;
            else Program.Settings.GitHub.FilesLinking = Settings.Structures.GitHub.FilesLinkingMode.AskBeforeExecute;

            Program.Settings.GitHub.Save();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SaveSettings();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
            DialogResult = DialogResult.Cancel;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveSettings();
            Close();
            DialogResult = DialogResult.OK;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Program.Settings.GitHub = new();
            LoadSettings();
        }
    }
}