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
        public bool FixLanguageDarkModeBug = true;

        private void Form_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            Process.Start($"{Properties.Resources.Link_Wiki}/Edit-WinPaletter-application-theme");
        }

        public ApplicationThemer()
        {
            InitializeComponent();
        }

        private void LoadFromWPTH(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new() { Filter = Program.Filters.WinPaletterTheme, Title = Program.Lang.Filter_OpenWinPaletterTheme })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    using (Theme.Manager TMx = new(Theme.Manager.Source.File, dlg.FileName))
                    {
                        LoadFromTM(TMx);
                    }
                }
            }
        }

        private void LoadFromCurrent(object sender, EventArgs e)
        {
            Theme.Manager TMx = new(Theme.Manager.Source.Registry);
            LoadFromTM(TMx);
            TMx.Dispose();
        }

        private void LoadFromDefault(object sender, EventArgs e)
        {
            Theme.Manager TMx = Theme.Default.Get(Program.WindowStyle);
            LoadFromTM(TMx);
            TMx.Dispose();
        }

        private void LoadIntoCurrentTheme(object sender, EventArgs e)
        {
            ApplyToTM(Program.TM);
            Close();
        }
        private void Apply(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            using (Theme.Manager TMx = new(Theme.Manager.Source.Registry))
            {
                ApplyToTM(TMx);
                ApplyToTM(Program.TM);
                TMx.AppTheme.Apply();
            }

            Cursor = Cursors.Default;
        }

        private void ApplicationThemer_Editor_Load(object sender, EventArgs e)
        {
            DesignerData data = new(this)
            {
                AspectName = Program.Lang.Store_Toggle_AppTheme,
                Enabled = Program.TM.AppTheme.Enabled,
                Import_theme = false,
                Import_msstyles = false,
                GeneratePalette = false,
                GenerateMSTheme = false,
                Import_preset = false,
                CanSwitchMode = false,

                OnLoadIntoCurrentTheme = LoadIntoCurrentTheme,
                OnApply = Apply,
                OnImportFromDefault = LoadFromDefault,
                OnImportFromWPTH = LoadFromWPTH,
                OnImportFromCurrentApplied = LoadFromCurrent,
            };

            LoadData(data);

            LoadFromTM(Program.TM);
            AdjustPreview();
        }

        private void ApplicationThemer_FormClosing(object sender, FormClosingEventArgs e)
        {
            FixLanguageDarkModeBug = true;
        }

        public void LoadFromTM(Theme.Manager TM)
        {
            AspectEnabled = TM.AppTheme.Enabled;
            appearance_dark.Checked = TM.AppTheme.DarkMode;
            RoundedCorners.Checked = TM.AppTheme.RoundCorners;
            BackColorPick.BackColor = TM.AppTheme.BackColor;
            AccentColor.BackColor = TM.AppTheme.AccentColor;
            checkBox1.Checked = TM.AppTheme.Animations;
            colorItem1.BackColor = TM.AppTheme.SecondaryColor;
            colorItem2.BackColor = TM.AppTheme.TertiaryColor;
            colorItem3.BackColor = TM.AppTheme.DisabledColor;
            colorItem4.BackColor = TM.AppTheme.DisabledBackColor;
        }

        public void ApplyToTM(Theme.Manager TM)
        {
            TM.AppTheme.Enabled = AspectEnabled;
            TM.AppTheme.DarkMode = appearance_dark.Checked;
            TM.AppTheme.RoundCorners = RoundedCorners.Checked;
            TM.AppTheme.BackColor = BackColorPick.BackColor;
            TM.AppTheme.AccentColor = AccentColor.BackColor;
            TM.AppTheme.Animations = checkBox1.Checked;
            TM.AppTheme.SecondaryColor = colorItem1.BackColor;
            TM.AppTheme.TertiaryColor = colorItem2.BackColor;
            TM.AppTheme.DisabledColor = colorItem3.BackColor;
            TM.AppTheme.DisabledBackColor = colorItem4.BackColor;
        }

        public void AdjustPreview()
        {
            if (FixLanguageDarkModeBug)
                return;

            Config.Scheme scheme = new(AccentColor.BackColor, BackColorPick.BackColor, appearance_dark.Checked);
            testControl1.Scheme = scheme;
            testControl4.Scheme = scheme;
            testControl5.Scheme = scheme;
            testControl2.Scheme = scheme;
            testControl3.Scheme = scheme;

            Config.Scheme scheme_sec = new(colorItem1.BackColor, BackColorPick.BackColor, appearance_dark.Checked);
            testControl7.Scheme = scheme_sec;
            testControl6.Scheme = scheme_sec;

            Config.Scheme scheme_ter = new(colorItem2.BackColor, BackColorPick.BackColor, appearance_dark.Checked);
            testControl9.Scheme = scheme_ter;
            testControl8.Scheme = scheme_ter;

            Config.Scheme scheme_dis = new(colorItem3.BackColor, colorItem4.BackColor, appearance_dark.Checked);
            testControl14.Scheme = scheme_dis;
            testControl11.Scheme = scheme_dis;
            testControl10.Scheme = scheme_dis;
            testControl13.Scheme = scheme_dis;
            testControl12.Scheme = scheme_dis;


            foreach (Control ctrl in Controls)
                ctrl.Invalidate();

            foreach (Control ctrl in titlebarExtender1.Controls)
                ctrl.Invalidate();
        }

        private void AccentColor_BackColorPick_DragDrop(object sender, DragEventArgs e)
        {
            AdjustPreview();
        }

        private void CheckedChanged(object sender, EventArgs e)
        {
            AdjustPreview();
        }

        private void Appearance_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsShown)
            {
                switch (appearance_list.SelectedItem.ToString().ToLower() ?? string.Empty)
                {
                    case var @case when @case == ("Default Dark".ToLower() ?? string.Empty):
                        {
                            appearance_dark.Checked = true;
                            RoundedCorners.Checked = OS.W12 || OS.W11 || OS.W7;
                            AccentColor.BackColor = DefaultColors.PrimaryColor;
                            BackColorPick.BackColor = DefaultColors.BackColorDark;
                            break;
                        }

                    case var case1 when case1 == ("Default Light".ToLower() ?? string.Empty):
                        {
                            appearance_dark.Checked = false;
                            RoundedCorners.Checked = OS.W12 || OS.W11 || OS.W7;
                            AccentColor.BackColor = DefaultColors.PrimaryColor;
                            BackColorPick.BackColor = DefaultColors.BackColorLight;
                            break;
                        }

                    case var case2 when case2 == ("AMOLED".ToLower() ?? string.Empty):
                        {
                            appearance_dark.Checked = true;
                            RoundedCorners.Checked = OS.W12 || OS.W11 || OS.W7;
                            AccentColor.BackColor = Color.FromArgb(0, 77, 193);
                            BackColorPick.BackColor = Color.Black;
                            break;
                        }

                    case var case3 when case3 == ("Extreme White".ToLower() ?? string.Empty):
                        {
                            appearance_dark.Checked = false;
                            RoundedCorners.Checked = OS.W12 || OS.W11 || OS.W7;
                            AccentColor.BackColor = DefaultColors.PrimaryColor;
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
                            AccentColor.BackColor = Color.FromArgb(216, 49, 3);
                            BackColorPick.BackColor = Color.FromArgb(242, 242, 242);
                            break;
                        }

                    case var case8 when case8 == ("Discord Dark".ToLower() ?? string.Empty):
                        {
                            appearance_dark.Checked = true;
                            RoundedCorners.Checked = false;
                            AccentColor.BackColor = Color.FromArgb(35, 69, 117);
                            BackColorPick.BackColor = Color.FromArgb(32, 34, 38);
                            break;
                        }

                    case var case9 when case9 == ("Discord Light".ToLower() ?? string.Empty):
                        {
                            appearance_dark.Checked = false;
                            RoundedCorners.Checked = false;
                            AccentColor.BackColor = Color.FromArgb(130, 132, 135);
                            BackColorPick.BackColor = Color.FromArgb(255, 255, 255);
                            break;
                        }

                }

                AdjustPreview();
            }
        }

        private void AccentColor_Click(object sender, EventArgs e)
        {
            if (e is DragEventArgs)
            {
                AdjustPreview();
                return;
            }

            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                AccentColor.BackColor = Forms.SubMenu.ShowMenu(AccentColor);
                AdjustPreview();
                return;
            }

            Dictionary<Control, string[]> CList = new()
            {
                { AccentColor, new string[] { nameof(AccentColor.BackColor) } }
            };

            Forms.ColorPickerDlg.Pick(CList);
            CList.Clear();

            AdjustPreview();
        }

        private void BackColorPick_Click(object sender, EventArgs e)
        {
            if (e is DragEventArgs)
            {
                AdjustPreview();
                return;
            }

            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                BackColorPick.BackColor = Forms.SubMenu.ShowMenu(BackColorPick);
                AdjustPreview();
                return;
            }

            Dictionary<Control, string[]> CList = new()
            {
                { BackColorPick, new string[] { nameof(BackColorPick.BackColor) } }
            };

            Forms.ColorPickerDlg.Pick(CList);
            CList.Clear();

            AdjustPreview();
        }

        private void colorItem1_Click(object sender, EventArgs e)
        {
            if (e is DragEventArgs)
            {
                AdjustPreview();
                return;
            }

            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                colorItem1.BackColor = Forms.SubMenu.ShowMenu(colorItem1);
                AdjustPreview();
                return;
            }

            Dictionary<Control, string[]> CList = new()
            {
                { colorItem1, new string[] { nameof(colorItem1.BackColor) } }
            };

            Forms.ColorPickerDlg.Pick(CList);
            CList.Clear();

            AdjustPreview();
        }

        private void colorItem2_Click(object sender, EventArgs e)
        {
            if (e is DragEventArgs)
            {
                AdjustPreview();
                return;
            }

            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                colorItem2.BackColor = Forms.SubMenu.ShowMenu(colorItem2);
                AdjustPreview();
                return;
            }

            Dictionary<Control, string[]> CList = new()
            {
                { colorItem2, new string[] { nameof(colorItem2.BackColor) } }
            };

            Forms.ColorPickerDlg.Pick(CList);
            CList.Clear();

            AdjustPreview();
        }

        private void colorItem3_Click(object sender, EventArgs e)
        {
            if (e is DragEventArgs)
            {
                AdjustPreview();
                return;
            }

            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                colorItem3.BackColor = Forms.SubMenu.ShowMenu(colorItem3);
                AdjustPreview();
                return;
            }

            Dictionary<Control, string[]> CList = new()
            {
                { colorItem3, new string[] { nameof(colorItem3.BackColor) } }
            };

            Forms.ColorPickerDlg.Pick(CList);
            CList.Clear();

            AdjustPreview();
        }

        private void colorItem4_Click(object sender, EventArgs e)
        {
            if (e is DragEventArgs)
            {
                AdjustPreview();
                return;
            }

            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                colorItem4.BackColor = Forms.SubMenu.ShowMenu(colorItem4);
                AdjustPreview();
                return;
            }

            Dictionary<Control, string[]> CList = new()
            {
                { colorItem4, new string[] { nameof(colorItem4.BackColor) } }
            };

            Forms.ColorPickerDlg.Pick(CList);
            CList.Clear();

            AdjustPreview();
        }
    }
}