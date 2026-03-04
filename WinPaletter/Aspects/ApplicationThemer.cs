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
        private bool _applyingScheme = false;

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
            using (OpenFileDialog dlg = new() { Filter = Program.Filters.WinPaletterTheme, Title = Program.Localization.Strings.Extensions.OpenWinPaletterTheme })
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
                AspectName = Program.Localization.Strings.Aspects.AppTheme,
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

            string last = Program.Settings.Appearance.LastUsedScheme;
            if (!string.IsNullOrEmpty(last))
            {
                int idx = appearance_list.FindStringExact(last);
                if (idx >= 0)
                {
                    _applyingScheme = true;
                    appearance_list.SelectedIndex = idx;
                    _applyingScheme = false;
                }
            }
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

            foreach (Control ctrl in Controls) ctrl.Invalidate();
            foreach (Control ctrl in titlebarExtender1.Controls) ctrl.Invalidate();
        }

        private void AccentColor_BackColorPick_DragDrop(object sender, DragEventArgs e)
        {
            if (_applyingScheme) return;
            Program.Settings.Appearance.LastUsedScheme = string.Empty;

            AdjustPreview();
        }

        private void Appearance_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!IsShown) return;

            _applyingScheme = true;

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

                // ── Code Editor Themes ────────────────────────────────────────────

                case var case10 when case10 == ("Dracula".ToLower() ?? string.Empty):
                    {
                        appearance_dark.Checked = true;
                        RoundedCorners.Checked = true;
                        AccentColor.BackColor = Color.FromArgb(189, 147, 249);   // purple
                        BackColorPick.BackColor = Color.FromArgb(40, 42, 54);
                        SecColor.BackColor = Color.FromArgb(68, 71, 90);
                        TerColor.BackColor = Color.FromArgb(98, 114, 164);
                        DisabledBackColor.BackColor = Color.FromArgb(33, 34, 44);
                        DisabledColor.BackColor = Color.FromArgb(25, 26, 33);
                        break;
                    }

                case var case11 when case11 == ("Nord".ToLower() ?? string.Empty):
                    {
                        appearance_dark.Checked = true;
                        RoundedCorners.Checked = true;
                        AccentColor.BackColor = Color.FromArgb(136, 192, 208);   // frost blue
                        BackColorPick.BackColor = Color.FromArgb(46, 52, 64);
                        SecColor.BackColor = Color.FromArgb(59, 66, 82);
                        TerColor.BackColor = Color.FromArgb(67, 76, 94);
                        DisabledBackColor.BackColor = Color.FromArgb(36, 41, 51);
                        DisabledColor.BackColor = Color.FromArgb(29, 33, 41);
                        break;
                    }

                case var case12 when case12 == ("Monokai".ToLower() ?? string.Empty):
                    {
                        appearance_dark.Checked = true;
                        RoundedCorners.Checked = false;
                        AccentColor.BackColor = Color.FromArgb(249, 38, 114);    // pink-red
                        BackColorPick.BackColor = Color.FromArgb(39, 40, 34);
                        SecColor.BackColor = Color.FromArgb(62, 61, 50);
                        TerColor.BackColor = Color.FromArgb(117, 113, 94);
                        DisabledBackColor.BackColor = Color.FromArgb(30, 31, 28);
                        DisabledColor.BackColor = Color.FromArgb(20, 20, 17);
                        break;
                    }

                case var case13 when case13 == ("Solarized Dark".ToLower() ?? string.Empty):
                    {
                        appearance_dark.Checked = true;
                        RoundedCorners.Checked = true;
                        AccentColor.BackColor = Color.FromArgb(38, 139, 210);    // blue
                        BackColorPick.BackColor = Color.FromArgb(0, 43, 54);
                        SecColor.BackColor = Color.FromArgb(7, 54, 66);
                        TerColor.BackColor = Color.FromArgb(88, 110, 117);
                        DisabledBackColor.BackColor = Color.FromArgb(0, 30, 38);
                        DisabledColor.BackColor = Color.FromArgb(0, 19, 25);
                        break;
                    }

                case var case14 when case14 == ("Solarized Light".ToLower() ?? string.Empty):
                    {
                        appearance_dark.Checked = false;
                        RoundedCorners.Checked = true;
                        AccentColor.BackColor = Color.FromArgb(38, 139, 210);
                        BackColorPick.BackColor = Color.FromArgb(253, 246, 227);
                        SecColor.BackColor = Color.FromArgb(238, 232, 213);
                        TerColor.BackColor = Color.FromArgb(147, 161, 161);
                        DisabledBackColor.BackColor = Color.FromArgb(253, 246, 227);
                        DisabledColor.BackColor = Color.FromArgb(238, 232, 213);
                        break;
                    }

                case var case15 when case15 == ("One Dark".ToLower() ?? string.Empty):
                    {
                        appearance_dark.Checked = true;
                        RoundedCorners.Checked = true;
                        AccentColor.BackColor = Color.FromArgb(97, 175, 239);    // blue
                        BackColorPick.BackColor = Color.FromArgb(40, 44, 52);
                        SecColor.BackColor = Color.FromArgb(53, 59, 69);
                        TerColor.BackColor = Color.FromArgb(62, 68, 81);
                        DisabledBackColor.BackColor = Color.FromArgb(33, 37, 43);
                        DisabledColor.BackColor = Color.FromArgb(24, 26, 31);
                        break;
                    }

                case var case16 when case16 == ("Gruvbox Dark".ToLower() ?? string.Empty):
                    {
                        appearance_dark.Checked = true;
                        RoundedCorners.Checked = false;
                        AccentColor.BackColor = Color.FromArgb(250, 189, 47);    // yellow
                        BackColorPick.BackColor = Color.FromArgb(40, 40, 40);
                        SecColor.BackColor = Color.FromArgb(60, 56, 54);
                        TerColor.BackColor = Color.FromArgb(80, 73, 69);
                        DisabledBackColor.BackColor = Color.FromArgb(29, 32, 33);
                        DisabledColor.BackColor = Color.FromArgb(20, 22, 23);
                        break;
                    }

                case var case17 when case17 == ("Gruvbox Light".ToLower() ?? string.Empty):
                    {
                        appearance_dark.Checked = false;
                        RoundedCorners.Checked = false;
                        AccentColor.BackColor = Color.FromArgb(175, 58, 3);      // orange-red
                        BackColorPick.BackColor = Color.FromArgb(251, 241, 199);
                        SecColor.BackColor = Color.FromArgb(235, 219, 178);
                        TerColor.BackColor = Color.FromArgb(213, 196, 161);
                        DisabledBackColor.BackColor = Color.FromArgb(249, 245, 215);
                        DisabledColor.BackColor = Color.FromArgb(242, 229, 188);
                        break;
                    }

                case var case18 when case18 == ("Tokyo Night".ToLower() ?? string.Empty):
                    {
                        appearance_dark.Checked = true;
                        RoundedCorners.Checked = true;
                        AccentColor.BackColor = Color.FromArgb(122, 162, 247);   // blue
                        BackColorPick.BackColor = Color.FromArgb(26, 27, 38);
                        SecColor.BackColor = Color.FromArgb(36, 40, 59);
                        TerColor.BackColor = Color.FromArgb(65, 72, 104);
                        DisabledBackColor.BackColor = Color.FromArgb(19, 20, 31);
                        DisabledColor.BackColor = Color.FromArgb(13, 14, 23);
                        break;
                    }

                case var case19 when case19 == ("Catppuccin Mocha".ToLower() ?? string.Empty):
                    {
                        appearance_dark.Checked = true;
                        RoundedCorners.Checked = true;
                        AccentColor.BackColor = Color.FromArgb(203, 166, 247);   // mauve
                        BackColorPick.BackColor = Color.FromArgb(30, 30, 46);
                        SecColor.BackColor = Color.FromArgb(49, 50, 68);
                        TerColor.BackColor = Color.FromArgb(69, 71, 90);
                        DisabledBackColor.BackColor = Color.FromArgb(24, 24, 37);
                        DisabledColor.BackColor = Color.FromArgb(17, 17, 27);
                        break;
                    }

                case var case20 when case20 == ("Catppuccin Latte".ToLower() ?? string.Empty):
                    {
                        appearance_dark.Checked = false;
                        RoundedCorners.Checked = true;
                        AccentColor.BackColor = Color.FromArgb(136, 57, 239);    // mauve
                        BackColorPick.BackColor = Color.FromArgb(239, 241, 245);
                        SecColor.BackColor = Color.FromArgb(230, 233, 239);
                        TerColor.BackColor = Color.FromArgb(204, 208, 218);
                        DisabledBackColor.BackColor = Color.FromArgb(242, 244, 248);
                        DisabledColor.BackColor = Color.FromArgb(230, 233, 239);
                        break;
                    }

                case var case21 when case21 == ("Kanagawa".ToLower() ?? string.Empty):
                    {
                        appearance_dark.Checked = true;
                        RoundedCorners.Checked = true;
                        AccentColor.BackColor = Color.FromArgb(126, 156, 216);   // wave blue
                        BackColorPick.BackColor = Color.FromArgb(31, 31, 40);
                        SecColor.BackColor = Color.FromArgb(42, 42, 55);
                        TerColor.BackColor = Color.FromArgb(54, 54, 70);
                        DisabledBackColor.BackColor = Color.FromArgb(22, 22, 29);
                        DisabledColor.BackColor = Color.FromArgb(13, 13, 18);
                        break;
                    }

                case var case22 when case22 == ("Rose Pine".ToLower() ?? string.Empty):
                    {
                        appearance_dark.Checked = true;
                        RoundedCorners.Checked = true;
                        AccentColor.BackColor = Color.FromArgb(235, 188, 186);   // rose
                        BackColorPick.BackColor = Color.FromArgb(25, 23, 36);
                        SecColor.BackColor = Color.FromArgb(31, 29, 46);
                        TerColor.BackColor = Color.FromArgb(38, 35, 58);
                        DisabledBackColor.BackColor = Color.FromArgb(17, 15, 28);
                        DisabledColor.BackColor = Color.FromArgb(10, 9, 21);
                        break;
                    }

                case var case23 when case23 == ("Everforest Dark".ToLower() ?? string.Empty):
                    {
                        appearance_dark.Checked = true;
                        RoundedCorners.Checked = true;
                        AccentColor.BackColor = Color.FromArgb(167, 192, 128);   // green
                        BackColorPick.BackColor = Color.FromArgb(39, 46, 51);
                        SecColor.BackColor = Color.FromArgb(46, 56, 60);
                        TerColor.BackColor = Color.FromArgb(55, 65, 69);
                        DisabledBackColor.BackColor = Color.FromArgb(30, 35, 38);
                        DisabledColor.BackColor = Color.FromArgb(20, 27, 30);
                        break;
                    }

                // ── Platform / Brand Themes ───────────────────────────────────────

                case var case24 when case24 == ("Twitter Dark".ToLower() ?? string.Empty):
                    {
                        appearance_dark.Checked = true;
                        RoundedCorners.Checked = true;
                        AccentColor.BackColor = Color.FromArgb(29, 155, 240);    // Twitter blue
                        BackColorPick.BackColor = Color.FromArgb(21, 32, 43);
                        SecColor.BackColor = Color.FromArgb(30, 39, 50);
                        TerColor.BackColor = Color.FromArgb(38, 50, 64);
                        DisabledBackColor.BackColor = Color.FromArgb(15, 24, 33);
                        DisabledColor.BackColor = Color.FromArgb(10, 17, 24);
                        break;
                    }

                case var case25 when case25 == ("Twitter Light".ToLower() ?? string.Empty):
                    {
                        appearance_dark.Checked = false;
                        RoundedCorners.Checked = true;
                        AccentColor.BackColor = Color.FromArgb(29, 155, 240);
                        BackColorPick.BackColor = Color.FromArgb(255, 255, 255);
                        SecColor.BackColor = Color.FromArgb(239, 243, 244);
                        TerColor.BackColor = Color.FromArgb(207, 217, 222);
                        DisabledBackColor.BackColor = Color.FromArgb(247, 249, 249);
                        DisabledColor.BackColor = Color.FromArgb(239, 243, 244);
                        break;
                    }

                case var case26 when case26 == ("Slack Dark".ToLower() ?? string.Empty):
                    {
                        appearance_dark.Checked = true;
                        RoundedCorners.Checked = true;
                        AccentColor.BackColor = Color.FromArgb(74, 21, 75);      // aubergine
                        BackColorPick.BackColor = Color.FromArgb(26, 29, 33);
                        SecColor.BackColor = Color.FromArgb(34, 37, 42);
                        TerColor.BackColor = Color.FromArgb(44, 48, 54);
                        DisabledBackColor.BackColor = Color.FromArgb(18, 20, 23);
                        DisabledColor.BackColor = Color.FromArgb(12, 14, 16);
                        break;
                    }

                case var case27 when case27 == ("Visual Studio Dark".ToLower() ?? string.Empty):
                    {
                        appearance_dark.Checked = true;
                        RoundedCorners.Checked = false;
                        AccentColor.BackColor = Color.FromArgb(0, 122, 204);     // VS blue
                        BackColorPick.BackColor = Color.FromArgb(37, 37, 38);
                        SecColor.BackColor = Color.FromArgb(45, 45, 48);
                        TerColor.BackColor = Color.FromArgb(63, 63, 70);
                        DisabledBackColor.BackColor = Color.FromArgb(30, 30, 30);
                        DisabledColor.BackColor = Color.FromArgb(20, 20, 20);
                        break;
                    }

                case var case28 when case28 == ("Visual Studio Light".ToLower() ?? string.Empty):
                    {
                        appearance_dark.Checked = false;
                        RoundedCorners.Checked = false;
                        AccentColor.BackColor = Color.FromArgb(0, 122, 204);
                        BackColorPick.BackColor = Color.FromArgb(245, 245, 245);
                        SecColor.BackColor = Color.FromArgb(232, 232, 232);
                        TerColor.BackColor = Color.FromArgb(204, 206, 219);
                        DisabledBackColor.BackColor = Color.FromArgb(250, 250, 250);
                        DisabledColor.BackColor = Color.FromArgb(236, 236, 236);
                        break;
                    }

                case var case29 when case29 == ("Notion Dark".ToLower() ?? string.Empty):
                    {
                        appearance_dark.Checked = true;
                        RoundedCorners.Checked = true;
                        AccentColor.BackColor = Color.FromArgb(35, 131, 226);
                        BackColorPick.BackColor = Color.FromArgb(25, 25, 25);
                        SecColor.BackColor = Color.FromArgb(37, 37, 37);
                        TerColor.BackColor = Color.FromArgb(55, 55, 55);
                        DisabledBackColor.BackColor = Color.FromArgb(18, 18, 18);
                        DisabledColor.BackColor = Color.FromArgb(12, 12, 12);
                        break;
                    }

                case var case30 when case30 == ("Notion Light".ToLower() ?? string.Empty):
                    {
                        appearance_dark.Checked = false;
                        RoundedCorners.Checked = true;
                        AccentColor.BackColor = Color.FromArgb(35, 131, 226);
                        BackColorPick.BackColor = Color.FromArgb(255, 255, 255);
                        SecColor.BackColor = Color.FromArgb(247, 247, 245);
                        TerColor.BackColor = Color.FromArgb(227, 226, 224);
                        DisabledBackColor.BackColor = Color.FromArgb(251, 251, 250);
                        DisabledColor.BackColor = Color.FromArgb(241, 241, 239);
                        break;
                    }

                // ── Creative / Special Themes ─────────────────────────────────────

                case var case31 when case31 == ("Cyberpunk".ToLower() ?? string.Empty):
                    {
                        appearance_dark.Checked = true;
                        RoundedCorners.Checked = false;
                        AccentColor.BackColor = Color.FromArgb(0, 255, 249);     // cyan
                        BackColorPick.BackColor = Color.FromArgb(10, 0, 18);
                        SecColor.BackColor = Color.FromArgb(20, 0, 42);
                        TerColor.BackColor = Color.FromArgb(45, 0, 96);
                        DisabledBackColor.BackColor = Color.FromArgb(7, 0, 13);
                        DisabledColor.BackColor = Color.FromArgb(4, 0, 8);
                        break;
                    }

                case var case32 when case32 == ("Synthwave".ToLower() ?? string.Empty):
                    {
                        appearance_dark.Checked = true;
                        RoundedCorners.Checked = true;
                        AccentColor.BackColor = Color.FromArgb(249, 42, 173);    // hot pink
                        BackColorPick.BackColor = Color.FromArgb(19, 12, 32);
                        SecColor.BackColor = Color.FromArgb(36, 23, 52);
                        TerColor.BackColor = Color.FromArgb(56, 32, 80);
                        DisabledBackColor.BackColor = Color.FromArgb(13, 8, 22);
                        DisabledColor.BackColor = Color.FromArgb(7, 5, 16);
                        break;
                    }

                case var case33 when case33 == ("Midnight Ocean".ToLower() ?? string.Empty):
                    {
                        appearance_dark.Checked = true;
                        RoundedCorners.Checked = true;
                        AccentColor.BackColor = Color.FromArgb(0, 180, 216);     // ocean blue
                        BackColorPick.BackColor = Color.FromArgb(3, 4, 94);
                        SecColor.BackColor = Color.FromArgb(2, 62, 138);
                        TerColor.BackColor = Color.FromArgb(0, 119, 182);
                        DisabledBackColor.BackColor = Color.FromArgb(2, 3, 62);
                        DisabledColor.BackColor = Color.FromArgb(1, 2, 40);
                        break;
                    }

                case var case34 when case34 == ("Forest".ToLower() ?? string.Empty):
                    {
                        appearance_dark.Checked = true;
                        RoundedCorners.Checked = true;
                        AccentColor.BackColor = Color.FromArgb(118, 196, 66);    // leaf green
                        BackColorPick.BackColor = Color.FromArgb(13, 31, 13);
                        SecColor.BackColor = Color.FromArgb(22, 43, 22);
                        TerColor.BackColor = Color.FromArgb(30, 58, 30);
                        DisabledBackColor.BackColor = Color.FromArgb(8, 15, 8);
                        DisabledColor.BackColor = Color.FromArgb(4, 8, 4);
                        break;
                    }

                case var case35 when case35 == ("Sunset".ToLower() ?? string.Empty):
                    {
                        appearance_dark.Checked = true;
                        RoundedCorners.Checked = true;
                        AccentColor.BackColor = Color.FromArgb(255, 112, 67);    // orange
                        BackColorPick.BackColor = Color.FromArgb(26, 10, 0);
                        SecColor.BackColor = Color.FromArgb(45, 18, 0);
                        TerColor.BackColor = Color.FromArgb(74, 32, 0);
                        DisabledBackColor.BackColor = Color.FromArgb(17, 7, 0);
                        DisabledColor.BackColor = Color.FromArgb(10, 4, 0);
                        break;
                    }

                case var case36 when case36 == ("Cherry Blossom".ToLower() ?? string.Empty):
                    {
                        appearance_dark.Checked = false;
                        RoundedCorners.Checked = true;
                        AccentColor.BackColor = Color.FromArgb(220, 90, 108);    // sakura pink
                        BackColorPick.BackColor = Color.FromArgb(255, 245, 247);
                        SecColor.BackColor = Color.FromArgb(255, 224, 230);
                        TerColor.BackColor = Color.FromArgb(255, 192, 203);
                        DisabledBackColor.BackColor = Color.FromArgb(255, 250, 251);
                        DisabledColor.BackColor = Color.FromArgb(255, 240, 244);
                        break;
                    }

                case var case37 when case37 == ("Lavender".ToLower() ?? string.Empty):
                    {
                        appearance_dark.Checked = false;
                        RoundedCorners.Checked = true;
                        AccentColor.BackColor = Color.FromArgb(103, 80, 164);    // deep purple
                        BackColorPick.BackColor = Color.FromArgb(248, 245, 255);
                        SecColor.BackColor = Color.FromArgb(237, 231, 254);
                        TerColor.BackColor = Color.FromArgb(212, 200, 244);
                        DisabledBackColor.BackColor = Color.FromArgb(251, 249, 255);
                        DisabledColor.BackColor = Color.FromArgb(240, 235, 252);
                        break;
                    }

                case var case38 when case38 == ("Mint".ToLower() ?? string.Empty):
                    {
                        appearance_dark.Checked = false;
                        RoundedCorners.Checked = true;
                        AccentColor.BackColor = Color.FromArgb(0, 121, 107);     // teal
                        BackColorPick.BackColor = Color.FromArgb(240, 250, 248);
                        SecColor.BackColor = Color.FromArgb(204, 240, 235);
                        TerColor.BackColor = Color.FromArgb(153, 224, 216);
                        DisabledBackColor.BackColor = Color.FromArgb(245, 252, 251);
                        DisabledColor.BackColor = Color.FromArgb(232, 248, 245);
                        break;
                    }

                // ── Minimal / Monochrome Themes ───────────────────────────────────

                case var case39 when case39 == ("Obsidian".ToLower() ?? string.Empty):
                    {
                        appearance_dark.Checked = true;
                        RoundedCorners.Checked = false;
                        AccentColor.BackColor = Color.FromArgb(158, 158, 158);   // neutral gray
                        BackColorPick.BackColor = Color.FromArgb(10, 10, 10);
                        SecColor.BackColor = Color.FromArgb(20, 20, 20);
                        TerColor.BackColor = Color.FromArgb(30, 30, 30);
                        DisabledBackColor.BackColor = Color.FromArgb(5, 5, 5);
                        DisabledColor.BackColor = Color.FromArgb(2, 2, 2);
                        break;
                    }

                case var case40 when case40 == ("Paper".ToLower() ?? string.Empty):
                    {
                        appearance_dark.Checked = false;
                        RoundedCorners.Checked = false;
                        AccentColor.BackColor = Color.FromArgb(92, 64, 51);      // ink brown
                        BackColorPick.BackColor = Color.FromArgb(248, 244, 238);
                        SecColor.BackColor = Color.FromArgb(237, 232, 224);
                        TerColor.BackColor = Color.FromArgb(217, 208, 194);
                        DisabledBackColor.BackColor = Color.FromArgb(248, 244, 238);
                        DisabledColor.BackColor = Color.FromArgb(237, 232, 224);
                        break;
                    }

                case var case41 when case41 == ("Slate".ToLower() ?? string.Empty):
                    {
                        appearance_dark.Checked = false;
                        RoundedCorners.Checked = false;
                        AccentColor.BackColor = Color.FromArgb(21, 101, 192);    // slate blue
                        BackColorPick.BackColor = Color.FromArgb(241, 245, 249);
                        SecColor.BackColor = Color.FromArgb(226, 232, 240);
                        TerColor.BackColor = Color.FromArgb(203, 213, 225);
                        DisabledBackColor.BackColor = Color.FromArgb(248, 250, 252);
                        DisabledColor.BackColor = Color.FromArgb(241, 245, 249);
                        break;
                    }

                case var case42 when case42 == ("Charcoal".ToLower() ?? string.Empty):
                    {
                        appearance_dark.Checked = true;
                        RoundedCorners.Checked = false;
                        AccentColor.BackColor = Color.FromArgb(239, 83, 80);     // red
                        BackColorPick.BackColor = Color.FromArgb(33, 33, 33);
                        SecColor.BackColor = Color.FromArgb(46, 46, 46);
                        TerColor.BackColor = Color.FromArgb(61, 61, 61);
                        DisabledBackColor.BackColor = Color.FromArgb(26, 26, 26);
                        DisabledColor.BackColor = Color.FromArgb(17, 17, 17);
                        break;
                    }
            }

            Program.Settings.Appearance.LastUsedScheme = appearance_list.SelectedItem?.ToString() ?? string.Empty;

            AdjustPreview();
            _applyingScheme = false;
        }

        private void AccentColor_Click(object sender, EventArgs e)
        {
            if (_applyingScheme) return;
            Program.Settings.Appearance.LastUsedScheme = string.Empty;

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
            if (_applyingScheme) return;
            Program.Settings.Appearance.LastUsedScheme = string.Empty;

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
            if (_applyingScheme) return;
            Program.Settings.Appearance.LastUsedScheme = string.Empty;

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
            if (_applyingScheme) return;
            Program.Settings.Appearance.LastUsedScheme = string.Empty;

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
            if (_applyingScheme) return;
            Program.Settings.Appearance.LastUsedScheme = string.Empty;

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
            if (_applyingScheme) return;
            Program.Settings.Appearance.LastUsedScheme = string.Empty;

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
            if (_applyingScheme) return;
            Program.Settings.Appearance.LastUsedScheme = string.Empty;

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
            if (_applyingScheme) return;
            Program.Settings.Appearance.LastUsedScheme = string.Empty;

            AdjustPreview();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (_applyingScheme) return;
            Program.Settings.Appearance.LastUsedScheme = string.Empty;
        }

        private void ApplicationThemer_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.Settings.Appearance.Save();
        }
    }
}