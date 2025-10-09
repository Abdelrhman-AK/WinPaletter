using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Media;
using System.Windows.Forms;

namespace WinPaletter
{
    public partial class PE_Warning
    {

        private string PE_File;

        public PE_Warning()
        {
            InitializeComponent();
        }

        private void BugReport_Load(object sender, EventArgs e)
        {
            this.LoadLanguage();
            ApplyStyle(this);
            Color c = PictureBox1.Image.AverageColor().CB(Program.Style.DarkMode ? -0.35f : 0.35f);
            AnimatedBox1.BackColor = c;
            CheckBox1.Checked = Program.Settings.ThemeApplyingBehavior.Ignore_PE_Modify_Alert;

            TreeView1.Font = Fonts.ConsoleMedium;

            Forms.GlassWindow.Show();

            SystemSounds.Exclamation.Play();

            BringToFront();
        }

        public DialogResult NotifyAction(string SourceFile, string ResourceType, int ID, ushort LangID = 1033)
        {

            DialogResult result;

            TreeView1.Nodes.Clear();

            PE_File = SourceFile;

            TreeView1.Nodes.Add(Program.Lang.Strings.PE.FileTypeDescription).Nodes.Add(Path.GetFullPath(SourceFile));

            {
                TreeNode temp = TreeView1.Nodes.Add(Program.Lang.Strings.PE.ReplacedResourceProperties);
                temp.Nodes.Add(Program.Lang.Strings.PE.ResourceType).Nodes.Add(ResourceType);
                temp.Nodes.Add(Program.Lang.Strings.PE.ResourceID).Nodes.Add(ID.ToString());
                temp.Nodes.Add(Program.Lang.Strings.PE.ResourceLanguageCode).Nodes.Add(LangID.ToString());
            }

            {
                TreeNode temp1 = TreeView1.Nodes.Add(Program.Lang.Strings.PE.RunSFCinCMD_Node);
                temp1.Nodes.Add($"sfc /scanfile=\"{Path.GetFullPath(SourceFile)}\"");
                temp1.Nodes.Add(Program.Lang.Strings.PE.DontForgetToRestart);
            }

            TreeView1.ExpandAll();

            result = ShowDialog();

            return result;
        }


        private void Button2_Click(object sender, EventArgs e)
        {
            Program.Settings.ThemeApplyingBehavior.Ignore_PE_Modify_Alert = CheckBox1.Checked;
            Program.Settings.Save(Settings.Source.Registry);
            DialogResult = DialogResult.Cancel;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Program.Settings.ThemeApplyingBehavior.Ignore_PE_Modify_Alert = CheckBox1.Checked;
            Program.Settings.Save(Settings.Source.Registry);
            Close();
            DialogResult = DialogResult.OK;
        }

        private void TreeView1_DoubleClick(object sender, EventArgs e)
        {
            if (TreeView1.SelectedNode is not null) Clipboard.SetText(TreeView1.SelectedNode.Text);
        }

        private void PE_Warning_FormClosing(object sender, FormClosingEventArgs e)
        {
            Forms.GlassWindow.Close();
            Program.Settings.ThemeApplyingBehavior.Ignore_PE_Modify_Alert = CheckBox1.Checked;
            Program.Settings.Save(Settings.Source.Registry);
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Process.Start(Links.Wiki.AntivirusIssue);
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            Process.Start(Links.Wiki.PE);
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            Reg_IO.SFC(PE_File);
            MsgBox($"{Program.Lang.Strings.General.Done}. {Program.Lang.Strings.PE.DontForgetToRestart}.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Cursor = Cursors.Default;
        }
    }
}