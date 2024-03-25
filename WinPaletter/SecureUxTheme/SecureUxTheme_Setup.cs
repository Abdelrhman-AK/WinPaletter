using System;
using System.Windows.Forms;

namespace WinPaletter
{
    public partial class SecureUxTheme_Setup : Form
    {
        public SecureUxTheme_Setup()
        {
            InitializeComponent();
        }

        private void SecureUxTheme_Setup_Load(object sender, EventArgs e)
        {
            this.LoadLanguage();
            ApplyStyle(this);

            LoadStatue();
            LoadHooks();
        }

        void LoadStatue()
        {
            label14.Text = SecureUxTheme.Wrapper.IsSecureUxThemeInstalled ? Program.Lang.Yes : Program.Lang.No;
            label13.Text = SecureUxTheme.Wrapper.IsSecureUxThemeRunning ? Program.Lang.Yes : Program.Lang.No;
            pictureBox9.Image = SecureUxTheme.Wrapper.IsExplorerHooked ? Properties.Resources.on : Properties.Resources.off;
            pictureBox10.Image = SecureUxTheme.Wrapper.IsSystemSettingsHooked ? Properties.Resources.on : Properties.Resources.off;
            pictureBox11.Image = SecureUxTheme.Wrapper.IsLogonUIHooked ? Properties.Resources.on : Properties.Resources.off;
        }

        void LoadHooks()
        {
            toggle20.Checked = SecureUxTheme.Wrapper.IsExplorerHooked;
            toggle1.Checked = SecureUxTheme.Wrapper.IsSystemSettingsHooked;
            toggle2.Checked = SecureUxTheme.Wrapper.IsLogonUIHooked;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            if (OS.W10 && toggle20.Checked && MsgBox(Program.Lang.SecureUxTheme_HookExplorerWarning, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No) return;

            SecureUxTheme.Wrapper.Install(toggle20.Checked, toggle1.Checked, toggle2.Checked, toggle3.Checked);
            LoadStatue();

            if (SecureUxTheme.Wrapper.IsSecureUxThemeInstalled)
            {
                MsgBox(Program.Lang.SecureUxTheme_Installed, MessageBoxButtons.OK, MessageBoxIcon.Information, Program.Lang.SecureUxTheme_Restart);
            }
            else
            {
                MsgBox(Program.Lang.SecureUxThemeNotInstalled0, MessageBoxButtons.OK, MessageBoxIcon.Error, Program.Lang.SecureUxThemeNotInstalled1, Program.Lang.CollapseNote, Program.Lang.ExpandNote, Links.SecureUxThemeRepository);
            }

            Cursor = Cursors.Default;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            SecureUxTheme.Wrapper.Uninstall();
            LoadStatue();
            MsgBox(Program.Lang.SecureUxTheme_Uninstalled, MessageBoxButtons.OK, MessageBoxIcon.Information, Program.Lang.SecureUxTheme_Restart);
            Cursor = Cursors.Default;
        }

        private void Label3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(Links.SecureUxThemeRepository);
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(Links.SecureUxThemeReleases);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
