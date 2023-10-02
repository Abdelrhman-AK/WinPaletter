using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
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
            WPStyle.ApplyStyle(this);
            var c = PictureBox1.Image.AverageColor().CB((float)(My.Env.Style.DarkMode ? -0.35d : 0.35d));
            AnimatedBox1.BackColor = c;
            CheckBox1.Checked = My.Env.Settings.ThemeApplyingBehavior.Ignore_PE_Modify_Alert;

            TreeView1.Font = My.MyProject.Application.ConsoleFontMedium;

            try
            {
                My.MyProject.Forms.BK.Close();
            }
            catch
            {
            }
            try
            {
                My.MyProject.Forms.BK.Show();
            }
            catch
            {
            }

            foreach (var lbl in AnimatedBox1.Controls.OfType<Label>())
                lbl.ForeColor = Color.White;

            My.MyProject.Computer.Audio.PlaySystemSound(System.Media.SystemSounds.Exclamation);
        }

        public DialogResult NotifyAction(string SourceFile, string ResourceType, int ID, ushort LangID = 1033)
        {

            DialogResult result;

            TreeView1.Nodes.Clear();

            PE_File = SourceFile;

            TreeView1.Nodes.Add(My.Env.Lang.PE_Systemfile).Nodes.Add(System.IO.Path.GetFullPath(SourceFile));

            {
                var temp = TreeView1.Nodes.Add(My.Env.Lang.PE_ReplacedResourceProperties);
                temp.Nodes.Add(My.Env.Lang.PE_ResourceType).Nodes.Add(ResourceType);
                temp.Nodes.Add(My.Env.Lang.PE_ResourceID).Nodes.Add(ID.ToString());
                temp.Nodes.Add(My.Env.Lang.PE_ResourceLanguageCode).Nodes.Add(LangID.ToString());
            }

            {
                var temp1 = TreeView1.Nodes.Add(My.Env.Lang.PE_RunSFCinCMD_Node);
                temp1.Nodes.Add("sfc /scanfile=\"" + System.IO.Path.GetFullPath(SourceFile) + "\"");
                temp1.Nodes.Add(My.Env.Lang.PE_DontForgetToRestart);
            }

            TreeView1.ExpandAll();

            result = ShowDialog();

            My.MyProject.Forms.BK.Close();

            return result;
        }


        private void Button2_Click(object sender, EventArgs e)
        {
            My.Env.Settings.ThemeApplyingBehavior.Ignore_PE_Modify_Alert = CheckBox1.Checked;
            My.Env.Settings.Save(WPSettings.Mode.Registry);

            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            My.Env.Settings.ThemeApplyingBehavior.Ignore_PE_Modify_Alert = CheckBox1.Checked;
            My.Env.Settings.Save(WPSettings.Mode.Registry);

            DialogResult = DialogResult.OK;
            Close();
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
            My.Env.Settings.ThemeApplyingBehavior.Ignore_PE_Modify_Alert = CheckBox1.Checked;
            My.Env.Settings.Save(WPSettings.Mode.Registry);
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Process.Start(My.Resources.Link_Wiki + "/Antiviruses-or-browsers-download-issue");
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            Process.Start(My.Resources.Link_Wiki + "/Advanced-options-to-patch-PE-files");
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            Reg_IO.SFC(PE_File);
            WPStyle.MsgBox(string.Format("{0}. {1}.", My.Env.Lang.Done, My.Env.Lang.PE_DontForgetToRestart), MessageBoxButtons.OK, MessageBoxIcon.Information);
            Cursor = Cursors.Default;
        }
    }
}