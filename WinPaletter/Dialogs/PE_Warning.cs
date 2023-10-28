using System;
using System.Diagnostics;
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
            var c = PictureBox1.Image.AverageColor().CB((float)(Program.Style.DarkMode ? -0.35d : 0.35d));
            AnimatedBox1.BackColor = c;
            CheckBox1.Checked = Program.Settings.ThemeApplyingBehavior.Ignore_PE_Modify_Alert;

            TreeView1.Font = Fonts.ConsoleMedium;

            try { Forms.BK.Close(); } catch { }
            try { Forms.BK.Show(); } catch { }

            Program.Computer.Audio.PlaySystemSound(System.Media.SystemSounds.Exclamation);

            BringToFront();
        }

        public DialogResult NotifyAction(string SourceFile, string ResourceType, int ID, ushort LangID = 1033)
        {

            DialogResult result;

            TreeView1.Nodes.Clear();

            PE_File = SourceFile;

            TreeView1.Nodes.Add(Program.Lang.PE_Systemfile).Nodes.Add(System.IO.Path.GetFullPath(SourceFile));

            {
                var temp = TreeView1.Nodes.Add(Program.Lang.PE_ReplacedResourceProperties);
                temp.Nodes.Add(Program.Lang.PE_ResourceType).Nodes.Add(ResourceType);
                temp.Nodes.Add(Program.Lang.PE_ResourceID).Nodes.Add(ID.ToString());
                temp.Nodes.Add(Program.Lang.PE_ResourceLanguageCode).Nodes.Add(LangID.ToString());
            }

            {
                var temp1 = TreeView1.Nodes.Add(Program.Lang.PE_RunSFCinCMD_Node);
                temp1.Nodes.Add("sfc /scanfile=\"" + System.IO.Path.GetFullPath(SourceFile) + "\"");
                temp1.Nodes.Add(Program.Lang.PE_DontForgetToRestart);
            }

            TreeView1.ExpandAll();

            result = ShowDialog();

            return result;
        }


        private void Button2_Click(object sender, EventArgs e)
        {
            Program.Settings.ThemeApplyingBehavior.Ignore_PE_Modify_Alert = CheckBox1.Checked;
            Program.Settings.Save(WPSettings.Mode.Registry);
            DialogResult = DialogResult.Cancel;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Program.Settings.ThemeApplyingBehavior.Ignore_PE_Modify_Alert = CheckBox1.Checked;
            Program.Settings.Save(WPSettings.Mode.Registry);
            Close();
            DialogResult = DialogResult.OK;
        }

        private void TreeView1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (TreeView1.SelectedNode is not null)
                    Clipboard.SetText(TreeView1.SelectedNode.Text);
            }
            catch
            {
            }
        }

        private void PE_Warning_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program.Settings.ThemeApplyingBehavior.Ignore_PE_Modify_Alert = CheckBox1.Checked;
            Program.Settings.Save(WPSettings.Mode.Registry);
            try { Forms.BK.Close(); } catch { }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Resources.Link_Wiki + "/Antiviruses-or-browsers-download-issue");
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Resources.Link_Wiki + "/Advanced-options-to-patch-PE-files");
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            Reg_IO.SFC(PE_File);
            MsgBox(string.Format("{0}. {1}.", Program.Lang.Done, Program.Lang.PE_DontForgetToRestart), MessageBoxButtons.OK, MessageBoxIcon.Information);
            Cursor = Cursors.Default;
        }
    }
}