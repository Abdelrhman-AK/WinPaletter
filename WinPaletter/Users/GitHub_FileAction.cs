using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinPaletter
{
    public partial class GitHub_FileAction : Form
    {
        public GitHub_FileAction()
        {
            InitializeComponent();
        }

        private void GitHub_FileAction_Load(object sender, EventArgs e)
        {
            ApplyStyle(this);
            this.LoadLanguage();
        }

        public DialogResult ConfirmFileDelete(string name, string description, long size, Bitmap icon)
        {
            this.Text = Program.Lang.Strings.GitHubStrings.Explorer_Confirmation_Title_DeleteFile;
            label1.Text = Program.Lang.Strings.GitHubStrings.Explorer_Confirmation_DeleteFile;
            pictureBox1.Image = Properties.Resources.IO_Dlg_Delete_File;

            pictureBox2.Image = icon;
            label2.Text = name;
            label5.Text = description;
            label6.Text = size.ToStringFileSize();

            return this.ShowDialog();
        }

        public DialogResult ConfirmFolderDelete(string name, long size)
        {
            this.Text = Program.Lang.Strings.GitHubStrings.Explorer_Confirmation_Title_DeleteFolder;
            label1.Text = Program.Lang.Strings.GitHubStrings.Explorer_Confirmation_DeleteFolder;
            pictureBox1.Image = Properties.Resources.IO_Dlg_Delete_Folder;

            pictureBox2.Image = Assets.GitHubMgr.folder_web_48;
            label2.Text = name;
            label5.Text = Program.Lang.Strings.Extensions.Folder;
            label6.Text = size.ToStringFileSize();

            return this.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            Close();
        }
    }
}
