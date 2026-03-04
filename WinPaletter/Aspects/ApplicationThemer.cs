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
        private bool OsRounded = OS.W12 || OS.W11 || OS.W7 || OS.WVista;

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

            string selected = appearance_list.SelectedItem?.ToString() ?? string.Empty;

            bool Is(string name) => selected.Equals(name, StringComparison.OrdinalIgnoreCase);

            if (Is("Default Dark")) ApplyScheme(true, OsRounded, DefaultColors.PrimaryColor_Dark, DefaultColors.SecondaryColor_Dark, DefaultColors.TertiaryColor_Dark, DefaultColors.BackColor_Dark, DefaultColors.DisabledBackColor_Dark, DefaultColors.DisabledColor_Dark);
            else if (Is("Default Light")) ApplyScheme(false, OsRounded, DefaultColors.PrimaryColor_Light, DefaultColors.SecondaryColor_Light, DefaultColors.TertiaryColor_Light, DefaultColors.BackColor_Light, DefaultColors.DisabledBackColor_Light, DefaultColors.DisabledColor_Light);
            else if (Is("AMOLED")) ApplyScheme(true, OsRounded, Color.FromArgb(0, 32, 81), Color.FromArgb(81, 0, 13), Color.FromArgb(0, 61, 23), Color.FromArgb(0, 0, 0), Color.FromArgb(10, 10, 10), Color.FromArgb(26, 26, 26));
            else if (Is("Extreme White")) ApplyScheme(false, OsRounded, DefaultColors.PrimaryColor_Light, DefaultColors.SecondaryColor_Light, DefaultColors.TertiaryColor_Light, Color.FromArgb(247, 247, 247), DefaultColors.DisabledBackColor_Light, DefaultColors.DisabledColor_Light);
            // ── Platform / Brand ───────────────────────────────────────────────────────────
            else if (Is("GitHub Dark")) ApplyScheme(true, true, Color.FromArgb(18, 106, 200), Color.FromArgb(248, 81, 73), Color.FromArgb(35, 197, 94), Color.FromArgb(13, 17, 23), DefaultColors.DisabledBackColor_Dark, DefaultColors.DisabledColor_Dark);
            else if (Is("GitHub Light")) ApplyScheme(false, true, Color.FromArgb(13, 76, 139), Color.FromArgb(207, 34, 46), Color.FromArgb(31, 136, 61), Color.FromArgb(246, 248, 250), DefaultColors.DisabledBackColor_Light, DefaultColors.DisabledColor_Light);
            else if (Is("Reddit Dark")) ApplyScheme(true, true, Color.FromArgb(193, 51, 0), Color.FromArgb(255, 85, 0), Color.FromArgb(255, 180, 0), Color.FromArgb(9, 9, 9), DefaultColors.DisabledBackColor_Dark, DefaultColors.DisabledColor_Dark);
            else if (Is("Reddit Light")) ApplyScheme(false, true, Color.FromArgb(195, 41, 2), Color.FromArgb(220, 0, 0), Color.FromArgb(200, 130, 0), Color.FromArgb(242, 242, 242), DefaultColors.DisabledBackColor_Light, DefaultColors.DisabledColor_Light);
            else if (Is("Discord Dark")) ApplyScheme(true, false, Color.FromArgb(35, 69, 117), Color.FromArgb(240, 71, 71), Color.FromArgb(250, 168, 26), Color.FromArgb(32, 34, 38), DefaultColors.DisabledBackColor_Dark, DefaultColors.DisabledColor_Dark);
            else if (Is("Discord Light")) ApplyScheme(false, false, Color.FromArgb(89, 91, 93), Color.FromArgb(210, 40, 40), Color.FromArgb(200, 140, 20), Color.FromArgb(240, 240, 240), DefaultColors.DisabledBackColor_Light, DefaultColors.DisabledColor_Light);
            else if (Is("Twitter Dark")) ApplyScheme(true, true, Color.FromArgb(29, 155, 240), Color.FromArgb(244, 33, 46), Color.FromArgb(255, 212, 0), Color.FromArgb(21, 32, 43), Color.FromArgb(15, 24, 33), Color.FromArgb(10, 17, 24));
            else if (Is("Twitter Light")) ApplyScheme(false, true, Color.FromArgb(29, 155, 240), Color.FromArgb(244, 33, 46), Color.FromArgb(255, 180, 0), Color.FromArgb(255, 255, 255), Color.FromArgb(247, 249, 249), Color.FromArgb(239, 243, 244));
            else if (Is("Slack Dark")) ApplyScheme(true, true, Color.FromArgb(74, 21, 75), Color.FromArgb(224, 28, 56), Color.FromArgb(54, 197, 160), Color.FromArgb(26, 29, 33), Color.FromArgb(18, 20, 23), Color.FromArgb(12, 14, 16));
            else if (Is("Visual Studio Dark")) ApplyScheme(true, false, Color.FromArgb(0, 122, 204), Color.FromArgb(252, 90, 90), Color.FromArgb(78, 201, 176), Color.FromArgb(37, 37, 38), Color.FromArgb(30, 30, 30), Color.FromArgb(20, 20, 20));
            else if (Is("Visual Studio Light")) ApplyScheme(false, false, Color.FromArgb(0, 122, 204), Color.FromArgb(196, 32, 32), Color.FromArgb(16, 124, 16), Color.FromArgb(245, 245, 245), Color.FromArgb(250, 250, 250), Color.FromArgb(236, 236, 236));
            else if (Is("Notion Dark")) ApplyScheme(true, true, Color.FromArgb(35, 131, 226), Color.FromArgb(235, 87, 87), Color.FromArgb(240, 185, 50), Color.FromArgb(25, 25, 25), Color.FromArgb(18, 18, 18), Color.FromArgb(12, 12, 12));
            else if (Is("Notion Light")) ApplyScheme(false, true, Color.FromArgb(35, 131, 226), Color.FromArgb(211, 73, 73), Color.FromArgb(210, 160, 30), Color.FromArgb(255, 255, 255), Color.FromArgb(251, 251, 250), Color.FromArgb(241, 241, 239));
            else if (Is("Spotify Dark")) ApplyScheme(true, true, Color.FromArgb(30, 215, 96), Color.FromArgb(229, 57, 53), Color.FromArgb(30, 215, 96), Color.FromArgb(18, 18, 18), Color.FromArgb(10, 10, 10), Color.FromArgb(6, 6, 6));
            else if (Is("YouTube Dark")) ApplyScheme(true, false, Color.FromArgb(255, 0, 0), Color.FromArgb(200, 0, 0), Color.FromArgb(255, 186, 0), Color.FromArgb(15, 15, 15), Color.FromArgb(10, 10, 10), Color.FromArgb(6, 6, 6));
            else if (Is("YouTube Light")) ApplyScheme(false, false, Color.FromArgb(255, 0, 0), Color.FromArgb(180, 0, 0), Color.FromArgb(220, 160, 0), Color.FromArgb(255, 255, 255), Color.FromArgb(249, 249, 249), Color.FromArgb(241, 241, 241));
            else if (Is("WhatsApp Dark")) ApplyScheme(true, false, Color.FromArgb(0, 168, 132), Color.FromArgb(235, 66, 66), Color.FromArgb(0, 168, 132), Color.FromArgb(11, 20, 26), Color.FromArgb(7, 13, 17), Color.FromArgb(4, 8, 10));
            else if (Is("WhatsApp Light")) ApplyScheme(false, false, Color.FromArgb(18, 140, 126), Color.FromArgb(220, 50, 50), Color.FromArgb(18, 140, 126), Color.FromArgb(236, 229, 221), Color.FromArgb(245, 245, 245), Color.FromArgb(230, 230, 230));
            else if (Is("Telegram Dark")) ApplyScheme(true, true, Color.FromArgb(42, 171, 238), Color.FromArgb(237, 74, 74), Color.FromArgb(245, 185, 55), Color.FromArgb(23, 33, 43), Color.FromArgb(15, 22, 29), Color.FromArgb(9, 13, 17));
            else if (Is("Telegram Light")) ApplyScheme(false, true, Color.FromArgb(42, 171, 238), Color.FromArgb(210, 55, 55), Color.FromArgb(220, 165, 30), Color.FromArgb(255, 255, 255), Color.FromArgb(245, 250, 255), Color.FromArgb(230, 243, 255));
            else if (Is("Firefox Dark")) ApplyScheme(true, false, Color.FromArgb(255, 100, 0), Color.FromArgb(255, 80, 80), Color.FromArgb(255, 189, 0), Color.FromArgb(28, 27, 34), Color.FromArgb(18, 18, 21), Color.FromArgb(11, 11, 13));
            else if (Is("Edge Dark")) ApplyScheme(true, true, Color.FromArgb(0, 120, 212), Color.FromArgb(232, 80, 80), Color.FromArgb(16, 196, 105), Color.FromArgb(32, 32, 32), Color.FromArgb(22, 22, 22), Color.FromArgb(14, 14, 14));
            else if (Is("Edge Light")) ApplyScheme(false, true, Color.FromArgb(0, 120, 212), Color.FromArgb(196, 43, 28), Color.FromArgb(16, 150, 72), Color.FromArgb(255, 255, 255), Color.FromArgb(249, 249, 249), Color.FromArgb(240, 240, 240));
            else if (Is("Steam Dark")) ApplyScheme(true, false, Color.FromArgb(102, 192, 244), Color.FromArgb(229, 80, 57), Color.FromArgb(144, 186, 60), Color.FromArgb(27, 40, 56), Color.FromArgb(19, 28, 40), Color.FromArgb(12, 18, 26));
            else if (Is("Twitch Dark")) ApplyScheme(true, false, Color.FromArgb(145, 70, 255), Color.FromArgb(235, 75, 75), Color.FromArgb(255, 170, 0), Color.FromArgb(14, 14, 18), Color.FromArgb(9, 9, 12), Color.FromArgb(5, 5, 7));
            else if (Is("LinkedIn Light")) ApplyScheme(false, false, Color.FromArgb(10, 102, 194), Color.FromArgb(204, 50, 50), Color.FromArgb(40, 163, 94), Color.FromArgb(242, 242, 242), Color.FromArgb(249, 249, 249), Color.FromArgb(236, 236, 236));
            // ── Code Editor ────────────────────────────────────────────────────────────────
            else if (Is("Dracula")) ApplyScheme(true, true, Color.FromArgb(189, 147, 249), Color.FromArgb(255, 85, 85), Color.FromArgb(241, 250, 140), Color.FromArgb(40, 42, 54), Color.FromArgb(33, 34, 44), Color.FromArgb(25, 26, 33));
            else if (Is("Nord")) ApplyScheme(true, true, Color.FromArgb(136, 192, 208), Color.FromArgb(191, 97, 106), Color.FromArgb(235, 203, 139), Color.FromArgb(46, 52, 64), Color.FromArgb(36, 41, 51), Color.FromArgb(29, 33, 41));
            else if (Is("Monokai")) ApplyScheme(true, false, Color.FromArgb(249, 38, 114), Color.FromArgb(255, 80, 80), Color.FromArgb(166, 226, 46), Color.FromArgb(39, 40, 34), Color.FromArgb(30, 31, 28), Color.FromArgb(20, 20, 17));
            else if (Is("Solarized Dark")) ApplyScheme(true, true, Color.FromArgb(38, 139, 210), Color.FromArgb(220, 50, 47), Color.FromArgb(133, 153, 0), Color.FromArgb(0, 43, 54), Color.FromArgb(0, 30, 38), Color.FromArgb(0, 19, 25));
            else if (Is("Solarized Light")) ApplyScheme(false, true, Color.FromArgb(38, 139, 210), Color.FromArgb(220, 50, 47), Color.FromArgb(133, 153, 0), Color.FromArgb(253, 246, 227), Color.FromArgb(253, 246, 227), Color.FromArgb(238, 232, 213));
            else if (Is("One Dark")) ApplyScheme(true, true, Color.FromArgb(97, 175, 239), Color.FromArgb(224, 108, 117), Color.FromArgb(152, 195, 121), Color.FromArgb(40, 44, 52), Color.FromArgb(33, 37, 43), Color.FromArgb(24, 26, 31));
            else if (Is("Gruvbox Dark")) ApplyScheme(true, false, Color.FromArgb(250, 189, 47), Color.FromArgb(251, 73, 52), Color.FromArgb(184, 187, 38), Color.FromArgb(40, 40, 40), Color.FromArgb(29, 32, 33), Color.FromArgb(20, 22, 23));
            else if (Is("Gruvbox Light")) ApplyScheme(false, false, Color.FromArgb(175, 58, 3), Color.FromArgb(204, 36, 29), Color.FromArgb(152, 151, 26), Color.FromArgb(251, 241, 199), Color.FromArgb(249, 245, 215), Color.FromArgb(242, 229, 188));
            else if (Is("Tokyo Night")) ApplyScheme(true, true, Color.FromArgb(122, 162, 247), Color.FromArgb(247, 118, 142), Color.FromArgb(158, 206, 106), Color.FromArgb(26, 27, 38), Color.FromArgb(19, 20, 31), Color.FromArgb(13, 14, 23));
            else if (Is("Catppuccin Mocha")) ApplyScheme(true, true, Color.FromArgb(203, 166, 247), Color.FromArgb(243, 139, 168), Color.FromArgb(166, 227, 161), Color.FromArgb(30, 30, 46), Color.FromArgb(24, 24, 37), Color.FromArgb(17, 17, 27));
            else if (Is("Catppuccin Latte")) ApplyScheme(false, true, Color.FromArgb(136, 57, 239), Color.FromArgb(210, 15, 57), Color.FromArgb(64, 160, 43), Color.FromArgb(239, 241, 245), Color.FromArgb(242, 244, 248), Color.FromArgb(230, 233, 239));
            else if (Is("Kanagawa")) ApplyScheme(true, true, Color.FromArgb(126, 156, 216), Color.FromArgb(228, 104, 118), Color.FromArgb(118, 148, 106), Color.FromArgb(31, 31, 40), Color.FromArgb(22, 22, 29), Color.FromArgb(13, 13, 18));
            else if (Is("Rose Pine")) ApplyScheme(true, true, Color.FromArgb(235, 188, 186), Color.FromArgb(235, 111, 146), Color.FromArgb(246, 193, 119), Color.FromArgb(25, 23, 36), Color.FromArgb(17, 15, 28), Color.FromArgb(10, 9, 21));
            else if (Is("Everforest Dark")) ApplyScheme(true, true, Color.FromArgb(167, 192, 128), Color.FromArgb(230, 126, 128), Color.FromArgb(167, 192, 128), Color.FromArgb(39, 46, 51), Color.FromArgb(30, 35, 38), Color.FromArgb(20, 27, 30));
            else if (Is("Material Dark")) ApplyScheme(true, true, Color.FromArgb(130, 170, 255), Color.FromArgb(255, 85, 114), Color.FromArgb(195, 232, 141), Color.FromArgb(15, 23, 42), Color.FromArgb(10, 16, 30), Color.FromArgb(7, 10, 21));
            else if (Is("Material Light")) ApplyScheme(false, true, Color.FromArgb(80, 120, 200), Color.FromArgb(213, 0, 0), Color.FromArgb(56, 142, 60), Color.FromArgb(250, 252, 255), Color.FromArgb(240, 245, 252), Color.FromArgb(224, 233, 246));
            else if (Is("Palenight")) ApplyScheme(true, true, Color.FromArgb(199, 146, 234), Color.FromArgb(255, 85, 114), Color.FromArgb(195, 232, 141), Color.FromArgb(41, 45, 62), Color.FromArgb(30, 33, 46), Color.FromArgb(20, 22, 31));
            else if (Is("Ayu Dark")) ApplyScheme(true, false, Color.FromArgb(255, 180, 84), Color.FromArgb(255, 51, 51), Color.FromArgb(186, 230, 126), Color.FromArgb(10, 14, 20), Color.FromArgb(7, 9, 14), Color.FromArgb(4, 5, 9));
            else if (Is("Ayu Light")) ApplyScheme(false, false, Color.FromArgb(255, 160, 0), Color.FromArgb(255, 51, 51), Color.FromArgb(134, 179, 0), Color.FromArgb(250, 248, 240), Color.FromArgb(252, 251, 246), Color.FromArgb(244, 241, 230));
            else if (Is("Ayu Mirage")) ApplyScheme(true, false, Color.FromArgb(255, 204, 102), Color.FromArgb(255, 51, 51), Color.FromArgb(186, 230, 126), Color.FromArgb(31, 34, 45), Color.FromArgb(20, 23, 31), Color.FromArgb(13, 15, 21));
            else if (Is("Cobalt2")) ApplyScheme(true, false, Color.FromArgb(255, 203, 0), Color.FromArgb(255, 80, 80), Color.FromArgb(128, 203, 196), Color.FromArgb(19, 41, 61), Color.FromArgb(11, 25, 38), Color.FromArgb(7, 16, 25));
            else if (Is("Night Owl")) ApplyScheme(true, true, Color.FromArgb(127, 219, 202), Color.FromArgb(255, 88, 116), Color.FromArgb(173, 219, 103), Color.FromArgb(1, 22, 39), Color.FromArgb(0, 14, 25), Color.FromArgb(0, 8, 15));
            else if (Is("Shades of Purple")) ApplyScheme(true, false, Color.FromArgb(250, 208, 0), Color.FromArgb(255, 94, 94), Color.FromArgb(165, 236, 236), Color.FromArgb(30, 19, 60), Color.FromArgb(20, 12, 40), Color.FromArgb(12, 7, 27));
            else if (Is("Panda")) ApplyScheme(true, true, Color.FromArgb(255, 117, 144), Color.FromArgb(255, 80, 80), Color.FromArgb(25, 249, 216), Color.FromArgb(25, 28, 35), Color.FromArgb(16, 19, 24), Color.FromArgb(10, 12, 15));
            else if (Is("Horizon Dark")) ApplyScheme(true, true, Color.FromArgb(232, 101, 107), Color.FromArgb(240, 80, 80), Color.FromArgb(9, 200, 142), Color.FromArgb(28, 21, 25), Color.FromArgb(19, 14, 17), Color.FromArgb(12, 9, 11));
            else if (Is("Iceberg Dark")) ApplyScheme(true, false, Color.FromArgb(132, 160, 198), Color.FromArgb(224, 108, 117), Color.FromArgb(180, 210, 115), Color.FromArgb(22, 28, 45), Color.FromArgb(14, 19, 30), Color.FromArgb(9, 12, 20));
            // ── Creative / Special ─────────────────────────────────────────────────────────
            else if (Is("Cyberpunk")) ApplyScheme(true, false, Color.FromArgb(0, 255, 249), Color.FromArgb(255, 0, 80), Color.FromArgb(255, 220, 0), Color.FromArgb(10, 0, 18), Color.FromArgb(7, 0, 13), Color.FromArgb(4, 0, 8));
            else if (Is("Synthwave")) ApplyScheme(true, true, Color.FromArgb(249, 42, 173), Color.FromArgb(255, 60, 100), Color.FromArgb(255, 210, 50), Color.FromArgb(19, 12, 32), Color.FromArgb(13, 8, 22), Color.FromArgb(7, 5, 16));
            else if (Is("Midnight Ocean")) ApplyScheme(true, true, Color.FromArgb(0, 180, 216), Color.FromArgb(240, 80, 80), Color.FromArgb(0, 230, 180), Color.FromArgb(3, 4, 94), Color.FromArgb(2, 3, 62), Color.FromArgb(1, 2, 40));
            else if (Is("Forest")) ApplyScheme(true, true, Color.FromArgb(118, 196, 66), Color.FromArgb(220, 80, 60), Color.FromArgb(196, 220, 66), Color.FromArgb(13, 31, 13), Color.FromArgb(8, 15, 8), Color.FromArgb(4, 8, 4));
            else if (Is("Sunset")) ApplyScheme(true, true, Color.FromArgb(255, 112, 67), Color.FromArgb(220, 50, 50), Color.FromArgb(255, 195, 60), Color.FromArgb(26, 10, 0), Color.FromArgb(17, 7, 0), Color.FromArgb(10, 4, 0));
            else if (Is("Cherry Blossom")) ApplyScheme(false, true, Color.FromArgb(220, 90, 108), Color.FromArgb(200, 40, 60), Color.FromArgb(255, 182, 193), Color.FromArgb(255, 245, 247), Color.FromArgb(255, 250, 251), Color.FromArgb(255, 240, 244));
            else if (Is("Lavender")) ApplyScheme(false, true, Color.FromArgb(103, 80, 164), Color.FromArgb(186, 50, 80), Color.FromArgb(149, 117, 205), Color.FromArgb(248, 245, 255), Color.FromArgb(251, 249, 255), Color.FromArgb(240, 235, 252));
            else if (Is("Mint")) ApplyScheme(false, true, Color.FromArgb(0, 121, 107), Color.FromArgb(200, 50, 60), Color.FromArgb(0, 168, 132), Color.FromArgb(240, 250, 248), Color.FromArgb(245, 252, 251), Color.FromArgb(232, 248, 245));
            // ── Minimal / Monochrome ───────────────────────────────────────────────────────
            else if (Is("Obsidian")) ApplyScheme(true, false, Color.FromArgb(158, 158, 158), Color.FromArgb(200, 70, 70), Color.FromArgb(130, 170, 100), Color.FromArgb(10, 10, 10), Color.FromArgb(5, 5, 5), Color.FromArgb(2, 2, 2));
            else if (Is("Paper")) ApplyScheme(false, false, Color.FromArgb(92, 64, 51), Color.FromArgb(180, 50, 40), Color.FromArgb(170, 140, 60), Color.FromArgb(248, 244, 238), Color.FromArgb(248, 244, 238), Color.FromArgb(237, 232, 224));
            else if (Is("Slate")) ApplyScheme(false, false, Color.FromArgb(21, 101, 192), Color.FromArgb(198, 40, 40), Color.FromArgb(69, 140, 180), Color.FromArgb(241, 245, 249), Color.FromArgb(248, 250, 252), Color.FromArgb(241, 245, 249));
            else if (Is("Charcoal")) ApplyScheme(true, false, Color.FromArgb(239, 83, 80), Color.FromArgb(220, 50, 50), Color.FromArgb(150, 200, 100), Color.FromArgb(33, 33, 33), Color.FromArgb(26, 26, 26), Color.FromArgb(17, 17, 17));
            // ── OS / Desktop Themes ────────────────────────────────────────────────────────
            else if (Is("macOS Dark")) ApplyScheme(true, true, Color.FromArgb(10, 132, 255), Color.FromArgb(255, 69, 58), Color.FromArgb(255, 214, 10), Color.FromArgb(28, 28, 30), Color.FromArgb(18, 18, 20), Color.FromArgb(10, 10, 12));
            else if (Is("macOS Light")) ApplyScheme(false, true, Color.FromArgb(0, 122, 255), Color.FromArgb(255, 59, 48), Color.FromArgb(255, 204, 0), Color.FromArgb(242, 242, 247), Color.FromArgb(249, 249, 251), Color.FromArgb(235, 235, 240));
            else if (Is("Windows 11 Dark")) ApplyScheme(true, true, Color.FromArgb(76, 194, 255), Color.FromArgb(255, 99, 97), Color.FromArgb(108, 203, 95), Color.FromArgb(32, 32, 32), Color.FromArgb(22, 22, 22), Color.FromArgb(14, 14, 14));
            else if (Is("Windows 11 Light")) ApplyScheme(false, true, Color.FromArgb(0, 103, 192), Color.FromArgb(196, 43, 28), Color.FromArgb(16, 137, 62), Color.FromArgb(243, 243, 243), Color.FromArgb(249, 249, 249), Color.FromArgb(238, 238, 238));
            else if (Is("Ubuntu")) ApplyScheme(true, false, Color.FromArgb(233, 84, 32), Color.FromArgb(222, 72, 72), Color.FromArgb(230, 175, 55), Color.FromArgb(44, 0, 30), Color.FromArgb(30, 0, 20), Color.FromArgb(18, 0, 12));
            else if (Is("KDE Breeze Dark")) ApplyScheme(true, true, Color.FromArgb(61, 174, 233), Color.FromArgb(237, 21, 21), Color.FromArgb(39, 174, 96), Color.FromArgb(35, 38, 41), Color.FromArgb(23, 26, 29), Color.FromArgb(14, 16, 18));
            else if (Is("KDE Breeze Light")) ApplyScheme(false, true, Color.FromArgb(61, 174, 233), Color.FromArgb(218, 20, 20), Color.FromArgb(39, 174, 96), Color.FromArgb(239, 240, 241), Color.FromArgb(247, 248, 249), Color.FromArgb(234, 236, 238));
            // ── Nature / Seasonal ─────────────────────────────────────────────────────────
            else if (Is("Arctic")) ApplyScheme(false, true, Color.FromArgb(86, 182, 194), Color.FromArgb(210, 80, 80), Color.FromArgb(144, 202, 214), Color.FromArgb(236, 244, 248), Color.FromArgb(245, 250, 252), Color.FromArgb(228, 238, 244));
            else if (Is("Desert")) ApplyScheme(false, false, Color.FromArgb(196, 107, 36), Color.FromArgb(200, 60, 40), Color.FromArgb(210, 160, 30), Color.FromArgb(245, 232, 210), Color.FromArgb(250, 242, 228), Color.FromArgb(238, 222, 195));
            else if (Is("Autumn")) ApplyScheme(true, false, Color.FromArgb(214, 93, 14), Color.FromArgb(200, 50, 30), Color.FromArgb(210, 160, 30), Color.FromArgb(45, 18, 4), Color.FromArgb(30, 12, 2), Color.FromArgb(18, 7, 1));
            else if (Is("Deep Sea")) ApplyScheme(true, true, Color.FromArgb(0, 210, 190), Color.FromArgb(220, 80, 80), Color.FromArgb(0, 180, 160), Color.FromArgb(0, 18, 38), Color.FromArgb(0, 11, 25), Color.FromArgb(0, 6, 15));
            else if (Is("Aurora")) ApplyScheme(true, true, Color.FromArgb(0, 230, 118), Color.FromArgb(235, 80, 100), Color.FromArgb(80, 200, 230), Color.FromArgb(5, 10, 30), Color.FromArgb(3, 6, 20), Color.FromArgb(1, 4, 12));
            else if (Is("Volcano")) ApplyScheme(true, false, Color.FromArgb(255, 64, 0), Color.FromArgb(220, 30, 30), Color.FromArgb(255, 180, 0), Color.FromArgb(30, 6, 0), Color.FromArgb(18, 4, 0), Color.FromArgb(10, 2, 0));
            else if (Is("Tundra")) ApplyScheme(false, false, Color.FromArgb(96, 139, 148), Color.FromArgb(190, 70, 60), Color.FromArgb(130, 180, 190), Color.FromArgb(224, 234, 236), Color.FromArgb(236, 244, 246), Color.FromArgb(212, 226, 230));
            else if (Is("Sakura Night")) ApplyScheme(true, true, Color.FromArgb(255, 150, 180), Color.FromArgb(240, 80, 100), Color.FromArgb(255, 182, 185), Color.FromArgb(25, 10, 22), Color.FromArgb(16, 6, 14), Color.FromArgb(10, 3, 9));
            // ── Retro / Vintage ───────────────────────────────────────────────────────────
            else if (Is("Terminal Green")) ApplyScheme(true, false, Color.FromArgb(0, 255, 0), Color.FromArgb(255, 60, 60), Color.FromArgb(0, 200, 100), Color.FromArgb(0, 10, 0), Color.FromArgb(0, 5, 0), Color.FromArgb(0, 2, 0));
            else if (Is("Terminal Amber")) ApplyScheme(true, false, Color.FromArgb(255, 176, 0), Color.FromArgb(255, 80, 0), Color.FromArgb(255, 220, 0), Color.FromArgb(12, 8, 0), Color.FromArgb(7, 5, 0), Color.FromArgb(4, 3, 0));
            else if (Is("C64")) ApplyScheme(true, false, Color.FromArgb(122, 113, 218), Color.FromArgb(200, 80, 80), Color.FromArgb(160, 160, 100), Color.FromArgb(64, 49, 141), Color.FromArgb(48, 36, 112), Color.FromArgb(32, 24, 84));
            else if (Is("DOS")) ApplyScheme(true, false, Color.FromArgb(170, 170, 170), Color.FromArgb(210, 0, 0), Color.FromArgb(210, 210, 0), Color.FromArgb(0, 0, 170), Color.FromArgb(0, 0, 120), Color.FromArgb(0, 0, 80));
            else if (Is("Game Boy")) ApplyScheme(false, false, Color.FromArgb(15, 56, 15), Color.FromArgb(48, 98, 48), Color.FromArgb(139, 172, 15), Color.FromArgb(155, 188, 15), Color.FromArgb(172, 203, 56), Color.FromArgb(136, 170, 10));
            else if (Is("Sepia")) ApplyScheme(false, false, Color.FromArgb(112, 66, 20), Color.FromArgb(180, 60, 40), Color.FromArgb(170, 130, 60), Color.FromArgb(240, 224, 192), Color.FromArgb(246, 236, 216), Color.FromArgb(232, 214, 176));
            // ── High Contrast / Accessibility ─────────────────────────────────────────────
            else if (Is("High Contrast Dark")) ApplyScheme(true, false, Color.FromArgb(255, 255, 0), Color.FromArgb(255, 50, 50), Color.FromArgb(0, 255, 100), Color.FromArgb(0, 0, 0), Color.FromArgb(5, 5, 5), Color.FromArgb(2, 2, 2));
            else if (Is("High Contrast Light")) ApplyScheme(false, false, Color.FromArgb(0, 0, 0), Color.FromArgb(180, 0, 0), Color.FromArgb(0, 128, 0), Color.FromArgb(255, 255, 255), Color.FromArgb(240, 240, 240), Color.FromArgb(220, 220, 220));
            else if (Is("Neon Dark")) ApplyScheme(true, false, Color.FromArgb(57, 255, 20), Color.FromArgb(255, 0, 80), Color.FromArgb(0, 255, 180), Color.FromArgb(5, 0, 10), Color.FromArgb(3, 0, 6), Color.FromArgb(1, 0, 3));
            else if (Is("Deuteranopia")) ApplyScheme(false, true, Color.FromArgb(0, 114, 178), Color.FromArgb(213, 94, 0), Color.FromArgb(240, 185, 50), Color.FromArgb(248, 248, 248), Color.FromArgb(236, 240, 245), Color.FromArgb(220, 228, 240));

            Program.Settings.Appearance.LastUsedScheme = appearance_list.SelectedItem?.ToString() ?? string.Empty;

            AdjustPreview();
            _applyingScheme = false;
        }

        private void ApplyScheme(bool dark, bool rounded, Color accent, Color secondary, Color tertiary, Color back, Color disabledBack, Color disabled)
        {
            appearance_dark.Checked = dark;
            RoundedCorners.Checked = rounded;
            AccentColor.BackColor = accent;
            SecColor.BackColor = secondary;
            TerColor.BackColor = tertiary;
            BackColorPick.BackColor = back;
            DisabledBackColor.BackColor = disabledBack;
            DisabledColor.BackColor = disabled;
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