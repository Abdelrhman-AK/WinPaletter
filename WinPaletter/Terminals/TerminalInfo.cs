using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter
{
    public partial class TerminalInfo
    {
        public TProfile Profile = new();

        public TerminalInfo()
        {
            InitializeComponent();
        }

        private void TerminalInfo_Load(object sender, EventArgs e)
        {
            this.LoadLanguage();
            ApplyStyle(this);
            Icon = Forms.WindowsTerminal.Icon;
        }

        public DialogResult OpenDialog(bool IsDefault = false)
        {
            TerName.Text = Profile.Name;
            TerTabTitle.Text = Profile.TabTitle;
            TerTabIcon.Text = Profile.Icon;
            TerTabColor.BackColor = Profile.TabColor;
            TerAcrylic.Checked = Program.TM.TerminalPreview.UseAcrylicInTabRow;

            if (IsDefault)
            {
                TerName.Text = string.Empty;
                TerTabTitle.Text = string.Empty;
                TerTabIcon.Text = string.Empty;
                TerName.Enabled = false;
                TerTabTitle.Enabled = false;
                TerTabIcon.Enabled = false;
            }
            else
            {
                TerName.Enabled = true;
                TerTabTitle.Enabled = true;
                TerTabIcon.Enabled = true;
            }

            return ShowDialog();
        }

        private void Button1_Click(object sender, EventArgs e)
        {

            if (Forms.WindowsTerminal.TerProfiles.Items.Contains(TerName.Text) & !((Forms.WindowsTerminal.TerProfiles.SelectedItem.ToString().ToLower() ?? string.Empty) == (TerName.Text.ToLower() ?? string.Empty)))
            {
                MsgBox(Program.Lang.Terminal_alreadyset, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Profile.Name = TerName.Text;
            Profile.TabTitle = TerTabTitle.Text;
            Profile.Icon = TerTabIcon.Text;
            Profile.TabColor = TerTabColor.BackColor;
            Program.TM.TerminalPreview.UseAcrylicInTabRow = TerAcrylic.Checked;
            DialogResult = DialogResult.OK;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void TerminalInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult != DialogResult.OK)
                DialogResult = DialogResult.Cancel;
        }

        private void TerTabColor_Click(object sender, EventArgs e)
        {

            if (e is DragEventArgs)
            {
                {
                    TProfile temp = Forms.WindowsTerminal.TerProfiles.SelectedIndex == 0 ? Forms.WindowsTerminal._Terminal.DefaultProf : Forms.WindowsTerminal._Terminal.Profiles[Forms.WindowsTerminal.TerProfiles.SelectedIndex - 1];
                    temp.TabColor = TerTabColor.BackColor;
                }

                Forms.WindowsTerminal.ApplyPreview(Forms.WindowsTerminal._Terminal);
                return;
            }

            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                Color cx = Forms.SubMenu.ShowMenu((UI.Controllers.ColorItem)sender, true);

                {
                    TProfile temp1 = Forms.WindowsTerminal.TerProfiles.SelectedIndex == 0 ? Forms.WindowsTerminal._Terminal.DefaultProf : Forms.WindowsTerminal._Terminal.Profiles[Forms.WindowsTerminal.TerProfiles.SelectedIndex - 1];
                    temp1.TabColor = cx;
                }

                Forms.WindowsTerminal.ApplyPreview(Forms.WindowsTerminal._Terminal);

                return;
            }

            List<Control> CList = new() { (Control)sender, Forms.WindowsTerminal.Terminal1 };

            Conditions _conditions = new() { Terminal_TabColor = true };

            Color C = Forms.ColorPickerDlg.Pick(CList, _conditions);

            Forms.WindowsTerminal.ApplyPreview(Forms.WindowsTerminal._Terminal);

            ((UI.Controllers.ColorItem)sender).BackColor = C;
            ((UI.Controllers.ColorItem)sender).Invalidate();

            CList.Clear();
        }
    }
}