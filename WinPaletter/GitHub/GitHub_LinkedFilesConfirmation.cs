using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinPaletter
{
    public partial class GitHub_LinkedFilesConfirmation : Form
    {
        public GitHub_LinkedFilesConfirmation()
        {
            InitializeComponent();
        }

        private void GitHub_LinkedFilesConfirmation_Load(object sender, EventArgs e)
        {
            ApplyStyle(this);
            this.LoadLanguage();
            CheckBox1.Checked = Program.Settings.UsersServices.GitHub_AutoOperateOnLinkedFiles;

            label3.Font = Fonts.ConsoleMedium;
            label4.Font = Fonts.ConsoleMedium;
            label6.Font = Fonts.ConsoleMedium;

            CustomSystemSounds.Exclamation.Play();
        }

        public enum Operation
        {
            Copy,
            Cut,
            Delete,
            Rename,
            Download
        }

        public DialogResult ShowDialog(string fileNameWithoutExtension, Operation operation) => ShowDialog(fileNameWithoutExtension, fileNameWithoutExtension, operation);

        public DialogResult ShowDialog(string oldFileNameWithoutExtension, string newFileNameWithoutExtension, Operation operation)
        {
            if (oldFileNameWithoutExtension != newFileNameWithoutExtension)
            {
                label4.Text = $"{oldFileNameWithoutExtension}.wpth → {newFileNameWithoutExtension}.wpth";
                label3.Text = $"{oldFileNameWithoutExtension}.wptp → {newFileNameWithoutExtension}.wptp";
            }
            else
            {
                label4.Text = $"{oldFileNameWithoutExtension}.wpth";
                label3.Text = $"{oldFileNameWithoutExtension}.wptp";
            }

            switch (operation)
            {
                case Operation.Copy:
                    label6.Text = Program.Lang.Strings.General.Copy;
                    break;
                case Operation.Cut:
                    label6.Text = Program.Lang.Strings.General.Cut;
                    break;
                case Operation.Delete:
                    label6.Text = Program.Lang.Strings.General.Delete;
                    break;
                case Operation.Rename:
                    label6.Text = Program.Lang.Strings.General.Rename;
                    break;
                case Operation.Download:
                    label6.Text = Program.Lang.Strings.General.Download;
                    break;
            }

            return this.ShowDialog();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Yes;
            this.Close();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
            this.Close();
        }

        private void GitHub_LinkedFilesConfirmation_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.Settings.UsersServices.GitHub_AutoOperateOnLinkedFiles = CheckBox1.Checked;
            Program.Settings.UsersServices.Save();
        }
    }
}
