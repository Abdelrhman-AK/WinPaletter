using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WinPaletter
{
    public partial class TerminalInfo
    {
        public ProfilesList Profile = new ProfilesList();

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
                TerName.Text = "";
                TerTabTitle.Text = "";
                TerTabIcon.Text = "";
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

            if (Forms.WindowsTerminal.TerProfiles.Items.Contains(TerName.Text) & !((Forms.WindowsTerminal.TerProfiles.SelectedItem.ToString().ToLower() ?? "") == (TerName.Text.ToLower() ?? "")))
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
                    var temp = Forms.WindowsTerminal.TerProfiles.SelectedIndex == 0 ? Forms.WindowsTerminal._Terminal.DefaultProf : Forms.WindowsTerminal._Terminal.Profiles[Forms.WindowsTerminal.TerProfiles.SelectedIndex - 1];
                    temp.TabColor = TerTabColor.BackColor;
                }

                Forms.WindowsTerminal.ApplyPreview(Forms.WindowsTerminal._Terminal);
                return;
            }

            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                var cx = Forms.SubMenu.ShowMenu((UI.Controllers.ColorItem)sender, true);

                {
                    var temp1 = Forms.WindowsTerminal.TerProfiles.SelectedIndex == 0 ? Forms.WindowsTerminal._Terminal.DefaultProf : Forms.WindowsTerminal._Terminal.Profiles[Forms.WindowsTerminal.TerProfiles.SelectedIndex - 1];
                    temp1.TabColor = cx;
                }

                Forms.WindowsTerminal.ApplyPreview(Forms.WindowsTerminal._Terminal);

                return;
            }

            var CList = new List<Control>() { (Control)sender, Forms.WindowsTerminal.Terminal1 };

            var _Conditions = new Conditions() { Terminal_TabColor = true };

            var C = Forms.ColorPickerDlg.Pick(CList, _Conditions);

            Forms.WindowsTerminal.ApplyPreview(Forms.WindowsTerminal._Terminal);

            ((UI.Controllers.ColorItem)sender).BackColor = C;
            ((UI.Controllers.ColorItem)sender).Invalidate();

            CList.Clear();
        }


    }
}