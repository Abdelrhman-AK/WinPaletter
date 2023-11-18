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
            ApplyStyle(this);
            ApplyFromTM(Program.TM);
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
                    ref WPSettings.Structures.Appearance appearance = ref Program.Settings.Appearance;
                    appearance.CustomColors = BackupSettings.Appearance.CustomColors;
                    appearance.CustomTheme = BackupSettings.Appearance.CustomTheme;
                    appearance.RoundedCorners = BackupSettings.Appearance.RoundedCorners;
                    appearance.BackColor = BackupSettings.Appearance.BackColor;
                    appearance.AccentColor = BackupSettings.Appearance.AccentColor;
                    appearance.Save();
                }

                GetDarkMode();
                GetRoundedCorners();
                ApplyStyle();
            }

            FixLanguageDarkModeBug = true;
        }

        public void ApplyFromTM(Theme.Manager TM)
        {
            {
                ref Theme.Structures.AppTheme AppTheme = ref TM.AppTheme;
                AppThemeEnabled.Checked = AppTheme.Enabled;
                appearance_dark.Checked = AppTheme.DarkMode;
                RoundedCorners.Checked = AppTheme.RoundCorners;
                BackColorPick.BackColor = AppTheme.BackColor;
                AccentColor.BackColor = AppTheme.AccentColor;
            }

        }

        public void ApplyToTM(Theme.Manager TM)
        {
            TM.AppTheme.Enabled = AppThemeEnabled.Checked;
            TM.AppTheme.DarkMode = appearance_dark.Checked;
            TM.AppTheme.RoundCorners = RoundedCorners.Checked;
            TM.AppTheme.BackColor = BackColorPick.BackColor;
            TM.AppTheme.AccentColor = AccentColor.BackColor;
        }

        public void AdjustPreview()
        {
            if (FixLanguageDarkModeBug)
                return;

            {
                ref WPSettings.Structures.Appearance Appearance = ref Program.Settings.Appearance;
                Appearance.CustomColors = true;
                Appearance.CustomTheme = appearance_dark.Checked;
                Appearance.RoundedCorners = RoundedCorners.Checked;
                Appearance.BackColor = BackColorPick.BackColor;
                Appearance.AccentColor = AccentColor.BackColor;
            }

            ApplyStyle(this);

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
            using (var _Def = Theme.Default.Get(Program.PreviewStyle))
            {
                ApplyFromTM(_Def);
                AdjustPreview();
            }
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            ApplyToTM(Program.TM);
            CloseAndApply = false;
            Close();
        }

        private void Button10_Click_1(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            var TMx = new Theme.Manager(Theme.Manager.Source.Registry);
            ApplyToTM(TMx);
            ApplyToTM(Program.TM);
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
                ref WPSettings.Structures.Appearance Appearance = ref Program.Settings.Appearance;
                Appearance.CustomColors = BackupSettings.Appearance.CustomColors;
                Appearance.CustomTheme = BackupSettings.Appearance.CustomTheme;
                Appearance.RoundedCorners = BackupSettings.Appearance.RoundedCorners;
                Appearance.BackColor = BackupSettings.Appearance.BackColor;
                Appearance.AccentColor = BackupSettings.Appearance.AccentColor;
            }

            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                AccentColor.BackColor = Forms.SubMenu.ShowMenu(AccentColor);
                AdjustPreview();
                return;
            }
            var clist = new List<Control>() { AccentColor };
            Forms.ColorPickerDlg.Pick(clist);
            clist.Clear();

            AdjustPreview();
        }

        private void BackColorPick_Click(object sender, EventArgs e)
        {
            {
                ref WPSettings.Structures.Appearance Appearance = ref Program.Settings.Appearance;
                Appearance.CustomColors = BackupSettings.Appearance.CustomColors;
                Appearance.CustomTheme = BackupSettings.Appearance.CustomTheme;
                Appearance.RoundedCorners = BackupSettings.Appearance.RoundedCorners;
                Appearance.BackColor = BackupSettings.Appearance.BackColor;
                Appearance.AccentColor = BackupSettings.Appearance.AccentColor;
            }

            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                BackColorPick.BackColor = Forms.SubMenu.ShowMenu(BackColorPick);
                AdjustPreview();
                return;
            }

            var clist = new List<Control>() { BackColorPick, this };
            Forms.ColorPickerDlg.Pick(clist);
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
                switch (appearance_list.SelectedItem.ToString().ToLower() ?? string.Empty)
                {
                    case var @case when @case == ("Default Dark".ToLower() ?? string.Empty):
                        {
                            appearance_dark.Checked = true;
                            RoundedCorners.Checked = OS.W12 || OS.W11 || OS.W7;
                            AccentColor.BackColor = DefaultColors.Accent;
                            BackColorPick.BackColor = DefaultColors.BackColorDark;
                            break;
                        }

                    case var case1 when case1 == ("Default Light".ToLower() ?? string.Empty):
                        {
                            appearance_dark.Checked = false;
                            RoundedCorners.Checked = OS.W12 || OS.W11 || OS.W7;
                            AccentColor.BackColor = DefaultColors.Accent;
                            BackColorPick.BackColor = DefaultColors.BackColorLight;
                            break;
                        }

                    case var case2 when case2 == ("AMOLED".ToLower() ?? string.Empty):
                        {
                            appearance_dark.Checked = true;
                            RoundedCorners.Checked = OS.W12 || OS.W11 || OS.W7;
                            AccentColor.BackColor = DefaultColors.Accent;
                            BackColorPick.BackColor = Color.Black;
                            break;
                        }

                    case var case3 when case3 == ("Extreme White".ToLower() ?? string.Empty):
                        {
                            appearance_dark.Checked = false;
                            RoundedCorners.Checked = OS.W12 || OS.W11 || OS.W7;
                            AccentColor.BackColor = DefaultColors.Accent;
                            BackColorPick.BackColor = Color.White;
                            break;
                        }

                    case var case4 when case4 == ("GitHub Dark".ToLower() ?? string.Empty):
                        {
                            appearance_dark.Checked = true;
                            RoundedCorners.Checked = true;
                            AccentColor.BackColor = Color.FromArgb(19, 35, 58);
                            BackColorPick.BackColor = Color.FromArgb(13, 17, 23);
                            break;
                        }

                    case var case5 when case5 == ("GitHub Light".ToLower() ?? string.Empty):
                        {
                            appearance_dark.Checked = false;
                            RoundedCorners.Checked = true;
                            AccentColor.BackColor = Color.FromArgb(31, 111, 235);
                            BackColorPick.BackColor = Color.FromArgb(246, 248, 250);
                            break;
                        }

                    case var case6 when case6 == ("Reddit Dark".ToLower() ?? string.Empty):
                        {
                            appearance_dark.Checked = true;
                            RoundedCorners.Checked = true;
                            AccentColor.BackColor = Color.FromArgb(255, 70, 0);
                            BackColorPick.BackColor = Color.FromArgb(9, 9, 9);
                            break;
                        }

                    case var case7 when case7 == ("Reddit Light".ToLower() ?? string.Empty):
                        {
                            appearance_dark.Checked = false;
                            RoundedCorners.Checked = true;
                            AccentColor.BackColor = Color.FromArgb(255, 70, 0);
                            BackColorPick.BackColor = Color.FromArgb(242, 242, 242);
                            break;
                        }

                    case var case8 when case8 == ("Discord Dark".ToLower() ?? string.Empty):
                        {
                            appearance_dark.Checked = true;
                            RoundedCorners.Checked = false;
                            AccentColor.BackColor = Color.FromArgb(65, 71, 78);
                            BackColorPick.BackColor = Color.FromArgb(32, 34, 38);
                            break;
                        }

                    case var case9 when case9 == ("Discord Light".ToLower() ?? string.Empty):
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