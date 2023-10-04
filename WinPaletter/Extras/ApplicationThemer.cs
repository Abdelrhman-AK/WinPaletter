using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter
{

    public partial class ApplicationThemer
    {

        private WPSettings BackupSettings;
        private bool _Shown = false;
        private bool CloseAndApply = false;
        public bool FixLanguageDarkModeBug = true;

        public ApplicationThemer()
        {
            InitializeComponent();
        }

        private void ApplicationThemer_Editor_Load(object sender, EventArgs e)
        {
            _Shown = false;
            BackupSettings = new WPSettings(WPSettings.Mode.Registry);
            this.LoadLanguage();
            WPStyle.ApplyStyle(this);
            Button12.Image = My.MyProject.Forms.MainFrm.Button20.Image.Resize(16, 16);
            ApplyFromTM(My.Env.TM);
            AdjustPreview();
            CloseAndApply = false;
        }

        private void ApplicationThemer_Shown(object sender, EventArgs e)
        {
            _Shown = true;
        }

        private void ApplicationThemer_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (!CloseAndApply) // Restore previous settings
            {
                {
                    ref var temp = ref My.Env.Settings.Appearance;
                    temp.CustomColors = BackupSettings.Appearance.CustomColors;
                    temp.CustomTheme = BackupSettings.Appearance.CustomTheme;
                    temp.RoundedCorners = BackupSettings.Appearance.RoundedCorners;
                    temp.BackColor = BackupSettings.Appearance.BackColor;
                    temp.AccentColor = BackupSettings.Appearance.AccentColor;
                    temp.Save();
                }

                WPStyle.FetchDarkMode();
                WPStyle.ApplyStyle();
            }

            FixLanguageDarkModeBug = true;
        }

        public void ApplyFromTM(Theme.Manager TM)
        {
            {
                ref var temp = ref TM.AppTheme;
                AppThemeEnabled.Checked = temp.Enabled;
                appearance_dark.Checked = temp.DarkMode;
                RoundedCorners.Checked = temp.RoundCorners;
                BackColorPick.BackColor = temp.BackColor;
                AccentColor.BackColor = temp.AccentColor;
            }

        }

        public void ApplyToTM(Theme.Manager TM)
        {
            {
                ref var temp = ref TM.AppTheme;
                temp.Enabled = AppThemeEnabled.Checked;
                temp.DarkMode = appearance_dark.Checked;
                temp.RoundCorners = RoundedCorners.Checked;
                temp.BackColor = BackColorPick.BackColor;
                temp.AccentColor = AccentColor.BackColor;
            }
        }

        public void AdjustPreview()
        {
            if (FixLanguageDarkModeBug)
                return;

            {
                ref var temp = ref My.Env.Settings.Appearance;
                temp.CustomColors = true;
                temp.CustomTheme = appearance_dark.Checked;
                temp.RoundedCorners = RoundedCorners.Checked;
                temp.BackColor = BackColorPick.BackColor;
                temp.AccentColor = AccentColor.BackColor;
            }

            WPStyle.ApplyStyle(this);

            foreach (Control ctrl in Controls)
                ctrl.Invalidate();

            foreach (Control ctrl in GroupBox12.Controls)
                ctrl.Invalidate();
        }

        private void Button11_Click(object sender, EventArgs e)
        {
            if (OpenFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var TMx = new Theme.Manager(Theme.Manager.Source.File, OpenFileDialog1.FileName);
                ApplyFromTM(TMx);
                AdjustPreview();
                TMx.Dispose();
            }
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            var TMx = new Theme.Manager(Theme.Manager.Source.Registry);
            ApplyFromTM(TMx);
            AdjustPreview();
            TMx.Dispose();
        }

        private void Button12_Click(object sender, EventArgs e)
        {
            using (var _Def = Theme.Default.From(My.Env.PreviewStyle))
            {
                ApplyFromTM(_Def);
                AdjustPreview();
            }
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            ApplyToTM(My.Env.TM);
            CloseAndApply = false;
            Close();
        }

        private void Button10_Click_1(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            var TMx = new Theme.Manager(Theme.Manager.Source.Registry);
            ApplyToTM(TMx);
            ApplyToTM(My.Env.TM);
            TMx.AppTheme.Apply();
            CloseAndApply = true;
            BackupSettings = new WPSettings(WPSettings.Mode.Registry);
            TMx.Dispose();
            Cursor = Cursors.Default;
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            CloseAndApply = false;
            Close();
        }

        private void AppThemeEnabled_CheckedChanged(object sender, EventArgs e)
        {
            checker_img.Image = Conversions.ToBoolean(((UI.WP.Toggle)sender).Checked) ? Properties.Resources.checker_enabled : Properties.Resources.checker_disabled;
            AdjustPreview();
        }

        private void AccentColor_BackColorPick_DragDrop(object sender, DragEventArgs e)
        {
            AdjustPreview();
        }

        private void AccentColor_Click(object sender, EventArgs e)
        {

            if (e is DragEventArgs)
                return;

            {
                ref var temp = ref My.Env.Settings.Appearance;
                temp.CustomColors = BackupSettings.Appearance.CustomColors;
                temp.CustomTheme = BackupSettings.Appearance.CustomTheme;
                temp.RoundedCorners = BackupSettings.Appearance.RoundedCorners;
                temp.BackColor = BackupSettings.Appearance.BackColor;
                temp.AccentColor = BackupSettings.Appearance.AccentColor;
            }

            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                AccentColor.BackColor = My.MyProject.Forms.SubMenu.ShowMenu(AccentColor);
                AdjustPreview();
                return;
            }
            var clist = new List<Control>() { AccentColor };
            My.MyProject.Forms.ColorPickerDlg.Pick(clist);
            clist.Clear();

            AdjustPreview();
        }

        private void BackColorPick_Click(object sender, EventArgs e)
        {
            {
                ref var temp = ref My.Env.Settings.Appearance;
                temp.CustomColors = BackupSettings.Appearance.CustomColors;
                temp.CustomTheme = BackupSettings.Appearance.CustomTheme;
                temp.RoundedCorners = BackupSettings.Appearance.RoundedCorners;
                temp.BackColor = BackupSettings.Appearance.BackColor;
                temp.AccentColor = BackupSettings.Appearance.AccentColor;
            }

            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                BackColorPick.BackColor = My.MyProject.Forms.SubMenu.ShowMenu(BackColorPick);
                AdjustPreview();
                return;
            }

            var clist = new List<Control>() { BackColorPick, this };
            My.MyProject.Forms.ColorPickerDlg.Pick(clist);
            clist.Clear();

            AdjustPreview();
        }

        private void CheckedChanged(object sender)
        {
            AdjustPreview();
        }

        private void Appearance_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_Shown)
            {
                switch (appearance_list.SelectedItem.ToString().ToLower() ?? "")
                {
                    case var @case when @case == ("Default Dark".ToLower() ?? ""):
                        {
                            appearance_dark.Checked = true;
                            RoundedCorners.Checked = My.Env.W11 | My.Env.W7;
                            AccentColor.BackColor = My.Env.DefaultAccent;
                            BackColorPick.BackColor = My.Env.DefaultBackColorDark;
                            break;
                        }

                    case var case1 when case1 == ("Default Light".ToLower() ?? ""):
                        {
                            appearance_dark.Checked = false;
                            RoundedCorners.Checked = My.Env.W11 | My.Env.W7;
                            AccentColor.BackColor = My.Env.DefaultAccent;
                            BackColorPick.BackColor = My.Env.DefaultBackColorLight;
                            break;
                        }

                    case var case2 when case2 == ("AMOLED".ToLower() ?? ""):
                        {
                            appearance_dark.Checked = true;
                            RoundedCorners.Checked = My.Env.W11 | My.Env.W7;
                            AccentColor.BackColor = My.Env.DefaultAccent;
                            BackColorPick.BackColor = Color.Black;
                            break;
                        }

                    case var case3 when case3 == ("Extreme White".ToLower() ?? ""):
                        {
                            appearance_dark.Checked = false;
                            RoundedCorners.Checked = My.Env.W11 | My.Env.W7;
                            AccentColor.BackColor = My.Env.DefaultAccent;
                            BackColorPick.BackColor = Color.White;
                            break;
                        }

                    case var case4 when case4 == ("GitHub Dark".ToLower() ?? ""):
                        {
                            appearance_dark.Checked = true;
                            RoundedCorners.Checked = true;
                            AccentColor.BackColor = Color.FromArgb(19, 35, 58);
                            BackColorPick.BackColor = Color.FromArgb(13, 17, 23);
                            break;
                        }

                    case var case5 when case5 == ("GitHub Light".ToLower() ?? ""):
                        {
                            appearance_dark.Checked = false;
                            RoundedCorners.Checked = true;
                            AccentColor.BackColor = Color.FromArgb(31, 111, 235);
                            BackColorPick.BackColor = Color.FromArgb(246, 248, 250);
                            break;
                        }

                    case var case6 when case6 == ("Reddit Dark".ToLower() ?? ""):
                        {
                            appearance_dark.Checked = true;
                            RoundedCorners.Checked = true;
                            AccentColor.BackColor = Color.FromArgb(255, 70, 0);
                            BackColorPick.BackColor = Color.FromArgb(9, 9, 9);
                            break;
                        }

                    case var case7 when case7 == ("Reddit Light".ToLower() ?? ""):
                        {
                            appearance_dark.Checked = false;
                            RoundedCorners.Checked = true;
                            AccentColor.BackColor = Color.FromArgb(255, 70, 0);
                            BackColorPick.BackColor = Color.FromArgb(242, 242, 242);
                            break;
                        }

                    case var case8 when case8 == ("Discord Dark".ToLower() ?? ""):
                        {
                            appearance_dark.Checked = true;
                            RoundedCorners.Checked = false;
                            AccentColor.BackColor = Color.FromArgb(65, 71, 78);
                            BackColorPick.BackColor = Color.FromArgb(32, 34, 38);
                            break;
                        }

                    case var case9 when case9 == ("Discord Light".ToLower() ?? ""):
                        {
                            appearance_dark.Checked = false;
                            RoundedCorners.Checked = false;
                            AccentColor.BackColor = Color.FromArgb(138, 140, 143);
                            BackColorPick.BackColor = Color.FromArgb(255, 255, 255);
                            break;
                        }

                }

                AdjustPreview();
            }
        }

        private void ApplicationThemer_BackColorChanged(object sender, EventArgs e)
        {
            foreach (Control ctrl in Controls)
                ctrl.Invalidate();

            foreach (Control ctrl in GroupBox12.Controls)
                ctrl.Invalidate();
        }

        private void Form_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            Process.Start(Properties.Resources.Link_Wiki + "/Edit-WinPaletter-application-theme");
        }

    }
}