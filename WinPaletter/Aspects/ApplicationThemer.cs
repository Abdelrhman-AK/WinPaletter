using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using WinPaletter.Theme;
using static WinPaletter.Theme.Manager;

namespace WinPaletter
{
    public partial class ApplicationThemer
    {
        public bool FixLanguageDarkModeBug = true;

        private void Form_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            Process.Start(Links.Wiki.WinPaletterApplicationTheme);
        }

        public ApplicationThemer()
        {
            InitializeComponent();
        }

        private void LoadFromWPTH(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new() { Filter = Program.Filters.WinPaletterTheme, Title = Program.Lang.Strings.Extensions.OpenWinPaletterTheme })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    using (Manager TMx = new(Source.File, dlg.FileName))
                    {
                        LoadFromTM(TMx);
                    }
                }
            }
        }

        private void LoadFromCurrent(object sender, EventArgs e)
        {
            Manager TMx = new(Source.Registry);
            LoadFromTM(TMx);
            TMx.Dispose();
        }

        private void LoadFromDefault(object sender, EventArgs e)
        {
            Manager TMx = Default.FromOS(Program.WindowStyle);
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

            if (Program.Settings.BackupTheme.Enabled && Program.Settings.BackupTheme.AutoBackupOnApplySingleAspect)
            {
                using (Manager TMx = new(Manager.Source.Registry))
                {
                    string filename = Program.GetUniqueFileName($"{Program.Settings.BackupTheme.BackupPath}\\OnAspectApply", $"{TMx.Info.ThemeName}_{DateTime.Now.Hour}.{DateTime.Now.Minute}.{DateTime.Now.Second}.wpth");
                    TMx.Save(Manager.Source.File, filename);
                }
            }

            ApplyToTM(Program.TM);
            ApplyToTM(Program.TM_Original);
            Program.TM.AppTheme.Apply();

            Cursor = Cursors.Default;
        }

        private void ApplicationThemer_Editor_Load(object sender, EventArgs e)
        {
            DesignerData data = new(this)
            {
                AspectName = Program.Lang.Strings.Aspects.AppTheme,
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

            BackColorPick.DefaultBackColor = DefaultColors.BackColor_Dark;
            AccentColor.DefaultBackColor = DefaultColors.PrimaryColor_Dark;
            SecColor.DefaultBackColor = DefaultColors.SecondaryColor_Dark;
            TerColor.DefaultBackColor = DefaultColors.TertiaryColor_Dark;
            DisabledColor.DefaultBackColor = DefaultColors.DisabledColor_Dark;
            DisabledBackColor.DefaultBackColor = DefaultColors.DisabledBackColor_Dark;

            LoadData(data);

            LoadFromTM(Program.TM);
            AdjustPreview();
        }

        private void ApplicationThemer_FormClosing(object sender, FormClosingEventArgs e)
        {
            FixLanguageDarkModeBug = true;
        }

        public void LoadFromTM(Manager TM)
        {
            AspectEnabled = TM.AppTheme.Enabled;
            appearance_dark.Checked = TM.AppTheme.DarkMode;
            RoundedCorners.Checked = TM.AppTheme.RoundCorners;
            BackColorPick.BackColor = TM.AppTheme.BackColor;
            AccentColor.BackColor = TM.AppTheme.AccentColor;
            checkBox1.Checked = TM.AppTheme.Animations;
            SecColor.BackColor = TM.AppTheme.SecondaryColor;
            TerColor.BackColor = TM.AppTheme.TertiaryColor;
            DisabledColor.BackColor = TM.AppTheme.DisabledColor;
            DisabledBackColor.BackColor = TM.AppTheme.DisabledBackColor;
        }

        public void ApplyToTM(Manager TM)
        {
            TM.AppTheme.Enabled = AspectEnabled;
            TM.AppTheme.DarkMode = appearance_dark.Checked;
            TM.AppTheme.RoundCorners = RoundedCorners.Checked;
            TM.AppTheme.BackColor = BackColorPick.BackColor;
            TM.AppTheme.AccentColor = AccentColor.BackColor;
            TM.AppTheme.Animations = checkBox1.Checked;
            TM.AppTheme.SecondaryColor = SecColor.BackColor;
            TM.AppTheme.TertiaryColor = TerColor.BackColor;
            TM.AppTheme.DisabledColor = DisabledColor.BackColor;
            TM.AppTheme.DisabledBackColor = DisabledBackColor.BackColor;
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

            Config.Scheme scheme_sec = new(SecColor.BackColor, BackColorPick.BackColor, appearance_dark.Checked);
            testControl7.Scheme = scheme_sec;
            testControl6.Scheme = scheme_sec;

            Config.Scheme scheme_ter = new(TerColor.BackColor, BackColorPick.BackColor, appearance_dark.Checked);
            testControl9.Scheme = scheme_ter;
            testControl8.Scheme = scheme_ter;

            Config.Scheme scheme_dis = new(DisabledColor.BackColor, DisabledBackColor.BackColor, appearance_dark.Checked);
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


        private void Appearance_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsShown)
            {
                switch (appearance_list.SelectedItem.ToString().ToLower() ?? string.Empty)
                {
                    case var @case when @case == ("Default Dark".ToLower() ?? string.Empty):
                        {
                            appearance_dark.Checked = true;
                            RoundedCorners.Checked = OS.W12 || OS.W11 || OS.W7 || OS.WVista;
                            AccentColor.BackColor = DefaultColors.PrimaryColor_Dark;
                            SecColor.BackColor = DefaultColors.SecondaryColor_Dark;
                            TerColor.BackColor = DefaultColors.TertiaryColor_Dark;
                            BackColorPick.BackColor = DefaultColors.BackColor_Dark;
                            DisabledBackColor.BackColor = DefaultColors.DisabledBackColor_Dark;
                            DisabledColor.BackColor = DefaultColors.DisabledColor_Dark;
                            break;
                        }

                    case var case1 when case1 == ("Default Light".ToLower() ?? string.Empty):
                        {
                            appearance_dark.Checked = false;
                            RoundedCorners.Checked = OS.W12 || OS.W11 || OS.W7 || OS.WVista;
                            AccentColor.BackColor = DefaultColors.PrimaryColor_Light;
                            SecColor.BackColor = DefaultColors.SecondaryColor_Light;
                            TerColor.BackColor = DefaultColors.TertiaryColor_Light;
                            BackColorPick.BackColor = DefaultColors.BackColor_Light;
                            DisabledBackColor.BackColor = DefaultColors.DisabledBackColor_Light;
                            DisabledColor.BackColor = DefaultColors.DisabledColor_Light;
                            break;
                        }

                    case var case2 when case2 == ("AMOLED".ToLower() ?? string.Empty):
                        {
                            appearance_dark.Checked = true;
                            RoundedCorners.Checked = OS.W12 || OS.W11 || OS.W7 || OS.WVista;
                            AccentColor.BackColor = Color.FromArgb(0, 32, 81);
                            BackColorPick.BackColor = Color.Black;
                            DisabledBackColor.BackColor = Color.FromArgb(10, 10, 10);
                            DisabledColor.BackColor = Color.FromArgb(26, 26, 26);
                            SecColor.BackColor = Color.FromArgb(81, 0, 13);
                            TerColor.BackColor = Color.FromArgb(0, 61, 23);

                            break;
                        }

                    case var case3 when case3 == ("Extreme White".ToLower() ?? string.Empty):
                        {
                            appearance_dark.Checked = false;
                            RoundedCorners.Checked = OS.W12 || OS.W11 || OS.W7 || OS.WVista;
                            AccentColor.BackColor = DefaultColors.PrimaryColor_Light;
                            SecColor.BackColor = DefaultColors.SecondaryColor_Light;
                            TerColor.BackColor = DefaultColors.TertiaryColor_Light;
                            BackColorPick.BackColor = Color.FromArgb(247, 247, 247);
                            DisabledBackColor.BackColor = DefaultColors.DisabledBackColor_Light;
                            DisabledColor.BackColor = DefaultColors.DisabledColor_Light;
                            break;
                        }

                    case var case4 when case4 == ("GitHub Dark".ToLower() ?? string.Empty):
                        {
                            appearance_dark.Checked = true;
                            RoundedCorners.Checked = true;
                            AccentColor.BackColor = Color.FromArgb(18, 106, 200);
                            BackColorPick.BackColor = Color.FromArgb(13, 17, 23);

                            SecColor.BackColor = DefaultColors.SecondaryColor_Dark;
                            TerColor.BackColor = DefaultColors.TertiaryColor_Dark;
                            DisabledBackColor.BackColor = DefaultColors.DisabledBackColor_Dark;
                            DisabledColor.BackColor = DefaultColors.DisabledColor_Dark;
                            break;
                        }

                    case var case5 when case5 == ("GitHub Light".ToLower() ?? string.Empty):
                        {
                            appearance_dark.Checked = false;
                            RoundedCorners.Checked = true;
                            AccentColor.BackColor = Color.FromArgb(13, 76, 139);
                            BackColorPick.BackColor = Color.FromArgb(246, 248, 250);

                            SecColor.BackColor = DefaultColors.SecondaryColor_Light;
                            TerColor.BackColor = DefaultColors.TertiaryColor_Light;
                            DisabledBackColor.BackColor = DefaultColors.DisabledBackColor_Light;
                            DisabledColor.BackColor = DefaultColors.DisabledColor_Light;
                            break;
                        }

                    case var case6 when case6 == ("Reddit Dark".ToLower() ?? string.Empty):
                        {
                            appearance_dark.Checked = true;
                            RoundedCorners.Checked = true;
                            AccentColor.BackColor = Color.FromArgb(193, 51, 0);
                            BackColorPick.BackColor = Color.FromArgb(9, 9, 9);

                            SecColor.BackColor = DefaultColors.SecondaryColor_Dark;
                            TerColor.BackColor = DefaultColors.TertiaryColor_Dark;
                            DisabledBackColor.BackColor = DefaultColors.DisabledBackColor_Dark;
                            DisabledColor.BackColor = DefaultColors.DisabledColor_Dark;

                            break;
                        }

                    case var case7 when case7 == ("Reddit Light".ToLower() ?? string.Empty):
                        {
                            appearance_dark.Checked = false;
                            RoundedCorners.Checked = true;
                            AccentColor.BackColor = Color.FromArgb(195, 41, 2);
                            BackColorPick.BackColor = Color.FromArgb(242, 242, 242);

                            SecColor.BackColor = DefaultColors.SecondaryColor_Light;
                            TerColor.BackColor = DefaultColors.TertiaryColor_Light;
                            DisabledBackColor.BackColor = DefaultColors.DisabledBackColor_Light;
                            DisabledColor.BackColor = DefaultColors.DisabledColor_Light;

                            break;
                        }

                    case var case8 when case8 == ("Discord Dark".ToLower() ?? string.Empty):
                        {
                            appearance_dark.Checked = true;
                            RoundedCorners.Checked = false;
                            AccentColor.BackColor = Color.FromArgb(35, 69, 117);
                            BackColorPick.BackColor = Color.FromArgb(32, 34, 38);

                            SecColor.BackColor = DefaultColors.SecondaryColor_Dark;
                            TerColor.BackColor = DefaultColors.TertiaryColor_Dark;
                            DisabledBackColor.BackColor = DefaultColors.DisabledBackColor_Dark;
                            DisabledColor.BackColor = DefaultColors.DisabledColor_Dark;

                            break;
                        }

                    case var case9 when case9 == ("Discord Light".ToLower() ?? string.Empty):
                        {
                            appearance_dark.Checked = false;
                            RoundedCorners.Checked = false;
                            AccentColor.BackColor = Color.FromArgb(89, 91, 93);
                            BackColorPick.BackColor = Color.FromArgb(240, 240, 240);

                            SecColor.BackColor = DefaultColors.SecondaryColor_Light;
                            TerColor.BackColor = DefaultColors.TertiaryColor_Light;
                            DisabledBackColor.BackColor = DefaultColors.DisabledBackColor_Light;
                            DisabledColor.BackColor = DefaultColors.DisabledColor_Light;

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
                AdjustPreview();
                return;
            }

            Dictionary<Control, string[]> CList = new()
            {
                { SecColor, new string[] { nameof(SecColor.BackColor) } }
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
                AdjustPreview();
                return;
            }

            Dictionary<Control, string[]> CList = new()
            {
                { TerColor, new string[] { nameof(TerColor.BackColor) } }
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
                AdjustPreview();
                return;
            }

            Dictionary<Control, string[]> CList = new()
            {
                { DisabledColor, new string[] { nameof(DisabledColor.BackColor) } }
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
                AdjustPreview();
                return;
            }

            Dictionary<Control, string[]> CList = new()
            {
                { DisabledBackColor, new string[] { nameof(DisabledBackColor.BackColor) } }
            };

            Forms.ColorPickerDlg.Pick(CList);
            CList.Clear();

            AdjustPreview();
        }

        private void appearance_dark_CheckedChanged(object sender, EventArgs e)
        {
            AdjustPreview();

            if ((sender as UI.WP.CheckBox).Checked)
            {
                BackColorPick.DefaultBackColor = DefaultColors.BackColor_Dark;
                AccentColor.DefaultBackColor = DefaultColors.PrimaryColor_Dark;
                SecColor.DefaultBackColor = DefaultColors.SecondaryColor_Dark;
                TerColor.DefaultBackColor = DefaultColors.TertiaryColor_Dark;
                DisabledColor.DefaultBackColor = DefaultColors.DisabledColor_Dark;
                DisabledBackColor.DefaultBackColor = DefaultColors.DisabledBackColor_Dark;
            }
            else
            {
                BackColorPick.DefaultBackColor = DefaultColors.BackColor_Light;
                AccentColor.DefaultBackColor = DefaultColors.PrimaryColor_Light;
                SecColor.DefaultBackColor = DefaultColors.SecondaryColor_Light;
                TerColor.DefaultBackColor = DefaultColors.TertiaryColor_Light;
                DisabledColor.DefaultBackColor = DefaultColors.DisabledColor_Light;
                DisabledBackColor.DefaultBackColor = DefaultColors.DisabledBackColor_Light;
            }
        }

        private void RoundedCorners_CheckedChanged(object sender, EventArgs e)
        {
            AdjustPreview();
        }
    }
}