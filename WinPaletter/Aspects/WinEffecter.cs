using Microsoft.VisualBasic.CompilerServices;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using WinPaletter.Theme;
using static WinPaletter.PreviewHelpers;

namespace WinPaletter
{
    public partial class WinEffecter
    {
        private void Form_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            Process.Start(Links.Wiki.WindowsEffects);
        }

        public WinEffecter()
        {
            InitializeComponent();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            PictureBox33.Image?.Dispose();
            PictureBox32.Image?.Dispose();
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

        private void ImportWin11Preset(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Theme.Default.Get(WindowStyle.W11)) { LoadFromTM(TMx); }
        }

        private void ImportWin10Preset(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Theme.Default.Get(WindowStyle.W10)) { LoadFromTM(TMx); }
        }

        private void ImportWin81Preset(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Theme.Default.Get(WindowStyle.W81)) { LoadFromTM(TMx); }
        }

        private void ImportWin7Preset(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Theme.Default.Get(WindowStyle.W7)) { LoadFromTM(TMx); }
        }

        private void ImportWinVistaPreset(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Theme.Default.Get(WindowStyle.WVista)) { LoadFromTM(TMx); }
        }

        private void ImportWinXPPreset(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Theme.Default.Get(WindowStyle.WXP)) { LoadFromTM(TMx); }
        }

        private void Apply(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            using (Theme.Manager TMx = new(Theme.Manager.Source.Registry))
            {
                if (Program.Settings.BackupTheme.Enabled && Program.Settings.BackupTheme.AutoBackupOnApplySingleAspect)
                {
                    string filename = Program.GetUniqueFileName($"{Program.Settings.BackupTheme.BackupPath}\\OnAspectApply", $"{TMx.Info.ThemeName}_{DateTime.Now.Hour}.{DateTime.Now.Minute}.{DateTime.Now.Second}.wpth");
                    TMx.Save(Theme.Manager.Source.File, filename);
                }

                ApplyToTM(TMx);
                ApplyToTM(Program.TM);
                ApplyToTM(Program.TM_Original);

                TMx.WindowsEffects.Apply();
                TMx.Win32.Broadcast_UPM_ToDefUsers();
            }

            Cursor = Cursors.Default;
        }

        private void WinEffecter_Load(object sender, EventArgs e)
        {
            DesignerData data = new(this)
            {
                AspectName = Program.Lang.Store_Toggle_WindowsEffects,
                Enabled = Program.TM.WindowsEffects.Enabled,
                Import_theme = false,
                Import_msstyles = false,
                GeneratePalette = false,
                GenerateMSTheme = false,
                Import_preset = true,
                CanSwitchMode = false,

                OnLoadIntoCurrentTheme = LoadIntoCurrentTheme,
                OnApply = Apply,
                OnImportFromDefault = LoadFromDefault,
                OnImportFromWPTH = LoadFromWPTH,
                OnImportFromCurrentApplied = LoadFromCurrent,
                OnImportFromScheme_11 = ImportWin11Preset,
                OnImportFromScheme_10 = ImportWin10Preset,
                OnImportFromScheme_81 = ImportWin81Preset,
                OnImportFromScheme_7 = ImportWin7Preset,
                OnImportFromScheme_Vista = ImportWinVistaPreset,
                OnImportFromScheme_XP = ImportWinXPPreset,
            };
            LoadData(data);

            LoadFromTM(Program.TM);
            SetClassicButtonColors(Program.TM, ButtonR1);
        }

        public void LoadFromTM(Theme.Manager TM)
        {
            ref Theme.Structures.WinEffects Effects = ref TM.WindowsEffects;
            AspectEnabled = Effects.Enabled;
            CheckBox1.Checked = Effects.WindowAnimation;
            CheckBox2.Checked = Effects.WindowShadow;
            CheckBox3.Checked = Effects.WindowUIEffects;
            CheckBox6.Checked = Effects.MenuAnimation;
            CheckBox27.Checked = Effects.AnimateControlsInsideWindow;
            ComboBox1.SelectedIndex = Effects.MenuFade == Theme.Structures.WinEffects.MenuAnimType.Fade ? 0 : 1;

            CheckBox5.Checked = Effects.MenuSelectionFade;
            Trackbar1.Value = (int)Effects.MenuShowDelay;
            CheckBox8.Checked = Effects.ComboBoxAnimation;
            CheckBox7.Checked = Effects.ListBoxSmoothScrolling;
            CheckBox9.Checked = Effects.TooltipAnimation;
            ComboBox2.SelectedIndex = Effects.TooltipFade == Theme.Structures.WinEffects.MenuAnimType.Fade ? 0 : 1;

            CheckBox4.Checked = Effects.IconsShadow;
            CheckBox10.Checked = Effects.IconsDesktopTranslSel;
            CheckBox11.Checked = Effects.ShowWinContentDrag;
            CheckBox12.Checked = Effects.KeyboardUnderline;
            Trackbar5.Value = Effects.NotificationDuration;
            Trackbar2.Value = (int)Effects.FocusRectWidth;
            Trackbar3.Value = (int)Effects.FocusRectHeight;
            Trackbar4.Value = (int)Effects.Caret;
            CheckBox13.Checked = Effects.AWT_Enabled;
            CheckBox14.Checked = Effects.AWT_BringActivatedWindowToTop;
            Trackbar6.Value = Effects.AWT_Delay;
            CheckBox15.Checked = Effects.SnapCursorToDefButton;
            CheckBox16.Checked = Effects.Win11ClassicContextMenu;
            CheckBox17.Checked = Effects.BalloonNotifications;
            CheckBox20.Checked = Effects.SysListView32;
            CheckBox19.Checked = Effects.ShowSecondsInSystemClock;
            CheckBox18.Checked = Effects.PaintDesktopVersion;
            CheckBox21.Checked = Effects.ShakeToMinimize;
            CheckBox22.Checked = Effects.Win11BootDots;
            CheckBox26.Checked = Effects.ClassicVolMixer;

            RadioButton1.Checked = Effects.Win11ExplorerBar == Theme.Structures.WinEffects.ExplorerBar.Default;
            RadioButton2.Checked = Effects.Win11ExplorerBar == Theme.Structures.WinEffects.ExplorerBar.Ribbon;
            RadioButton3.Checked = Effects.Win11ExplorerBar == Theme.Structures.WinEffects.ExplorerBar.Bar;
            CheckBox23.Checked = Effects.DisableNavBar;
            CheckBox24.Checked = Effects.AutoHideScrollBars;
            CheckBox25.Checked = Effects.FullScreenStartMenu;
            checkBox28.Checked = Effects.EnableAeroPeek;
            checkBox29.Checked = Effects.AlwaysHibernateThumbnails;

            if (!Effects.ColorFilter_Enabled)
            {
                RadioImage1.Checked = true;
            }
            else
            {
                switch (Effects.ColorFilter)
                {
                    case Theme.Structures.WinEffects.ColorFilters.Grayscale:
                        {
                            RadioImage5.Checked = true;
                            break;
                        }

                    case Theme.Structures.WinEffects.ColorFilters.Inverted:
                        {
                            RadioImage7.Checked = true;
                            break;
                        }

                    case Theme.Structures.WinEffects.ColorFilters.GrayscaleInverted:
                        {
                            RadioImage6.Checked = true;
                            break;
                        }

                    case Theme.Structures.WinEffects.ColorFilters.RedGreen_deuteranopia:
                        {
                            RadioImage2.Checked = true;
                            break;
                        }

                    case Theme.Structures.WinEffects.ColorFilters.RedGreen_protanopia:
                        {
                            RadioImage3.Checked = true;
                            break;
                        }

                    case Theme.Structures.WinEffects.ColorFilters.BlueYellow:
                        {
                            RadioImage4.Checked = true;
                            break;
                        }

                    default:
                        {
                            RadioImage1.Checked = true;
                            break;
                        }

                }
            }

            Panel2.Width = (int)Effects.Caret;
        }

        public void ApplyToTM(Theme.Manager TM)
        {
            ref Theme.Structures.WinEffects Effects = ref TM.WindowsEffects;
            Effects.Enabled = AspectEnabled;
            Effects.WindowAnimation = CheckBox1.Checked;
            Effects.WindowShadow = CheckBox2.Checked;
            Effects.WindowUIEffects = CheckBox3.Checked;
            Effects.AnimateControlsInsideWindow = CheckBox27.Checked;
            Effects.MenuAnimation = CheckBox6.Checked;
            Effects.MenuFade = ComboBox1.SelectedIndex == 0 ? Theme.Structures.WinEffects.MenuAnimType.Fade : Theme.Structures.WinEffects.MenuAnimType.Scroll;

            Effects.MenuSelectionFade = CheckBox5.Checked;
            Effects.MenuShowDelay = (uint)Trackbar1.Value;
            Effects.ComboBoxAnimation = CheckBox8.Checked;
            Effects.ListBoxSmoothScrolling = CheckBox7.Checked;
            Effects.TooltipAnimation = CheckBox9.Checked;
            Effects.TooltipFade = ComboBox2.SelectedIndex == 0 ? Theme.Structures.WinEffects.MenuAnimType.Fade : Theme.Structures.WinEffects.MenuAnimType.Scroll;

            Effects.IconsShadow = CheckBox4.Checked;
            Effects.IconsDesktopTranslSel = CheckBox10.Checked;
            Effects.ShowWinContentDrag = CheckBox11.Checked;
            Effects.KeyboardUnderline = CheckBox12.Checked;
            Effects.NotificationDuration = Trackbar5.Value;
            Effects.FocusRectWidth = (uint)Trackbar2.Value;
            Effects.FocusRectHeight = (uint)Trackbar3.Value;
            Effects.Caret = (uint)Trackbar4.Value;
            Effects.AWT_Enabled = CheckBox13.Checked;
            Effects.AWT_BringActivatedWindowToTop = CheckBox14.Checked;
            Effects.AWT_Delay = Trackbar6.Value;
            Effects.SnapCursorToDefButton = CheckBox15.Checked;
            Effects.Win11ClassicContextMenu = CheckBox16.Checked;
            Effects.BalloonNotifications = CheckBox17.Checked;
            Effects.SysListView32 = CheckBox20.Checked;
            Effects.ShowSecondsInSystemClock = CheckBox19.Checked;
            Effects.PaintDesktopVersion = CheckBox18.Checked;
            Effects.ShakeToMinimize = CheckBox21.Checked;
            Effects.Win11BootDots = CheckBox22.Checked;
            Effects.ClassicVolMixer = CheckBox26.Checked;
            Effects.EnableAeroPeek = checkBox28.Checked;
            Effects.AlwaysHibernateThumbnails = checkBox29.Checked;

            if (RadioButton1.Checked)
            {
                Effects.Win11ExplorerBar = Theme.Structures.WinEffects.ExplorerBar.Default;
            }

            else if (RadioButton2.Checked)
            {
                Effects.Win11ExplorerBar = Theme.Structures.WinEffects.ExplorerBar.Ribbon;
            }

            else if (RadioButton3.Checked)
            {
                Effects.Win11ExplorerBar = Theme.Structures.WinEffects.ExplorerBar.Bar;

            }

            Effects.DisableNavBar = CheckBox23.Checked;
            Effects.AutoHideScrollBars = CheckBox24.Checked;
            Effects.FullScreenStartMenu = CheckBox25.Checked;

            if (RadioImage1.Checked)
            {
                Effects.ColorFilter_Enabled = false;
            }

            else if (RadioImage5.Checked)
            {
                Effects.ColorFilter_Enabled = true;
                Effects.ColorFilter = Theme.Structures.WinEffects.ColorFilters.Grayscale;
            }

            else if (RadioImage7.Checked)
            {
                Effects.ColorFilter_Enabled = true;
                Effects.ColorFilter = Theme.Structures.WinEffects.ColorFilters.Inverted;
            }

            else if (RadioImage6.Checked)
            {
                Effects.ColorFilter_Enabled = true;
                Effects.ColorFilter = Theme.Structures.WinEffects.ColorFilters.GrayscaleInverted;
            }

            else if (RadioImage2.Checked)
            {
                Effects.ColorFilter_Enabled = true;
                Effects.ColorFilter = Theme.Structures.WinEffects.ColorFilters.RedGreen_deuteranopia;
            }

            else if (RadioImage3.Checked)
            {
                Effects.ColorFilter_Enabled = true;
                Effects.ColorFilter = Theme.Structures.WinEffects.ColorFilters.RedGreen_protanopia;
            }

            else if (RadioImage4.Checked)
            {
                Effects.ColorFilter_Enabled = true;
                Effects.ColorFilter = Theme.Structures.WinEffects.ColorFilters.BlueYellow;

            }
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            Panel2.Visible = !Panel2.Visible;
        }

        private void RadioImage1_CheckedChanged(object sender, EventArgs e)
        {
            if (Conversions.ToBoolean(((UI.WP.RadioImage)sender).Checked))
            {
                PictureBox33.Image = Assets.Accessibility_Vision.CF_Img_Normal;
                PictureBox32.Image = Assets.Accessibility_Vision.CF_Pie_Normal;

                R1.BackColor = Color.FromArgb(204, 50, 47);
                R2.BackColor = Color.FromArgb(233, 80, 63);
                R3.BackColor = Color.FromArgb(239, 142, 133);

                O1.BackColor = Color.FromArgb(220, 96, 44);
                O2.BackColor = Color.FromArgb(239, 153, 58);
                O3.BackColor = Color.FromArgb(247, 193, 114);

                Y1.BackColor = Color.FromArgb(231, 181, 64);
                Y2.BackColor = Color.FromArgb(248, 205, 72);
                Y3.BackColor = Color.FromArgb(250, 224, 121);

                G1.BackColor = Color.FromArgb(57, 122, 47);
                G2.BackColor = Color.FromArgb(117, 213, 113);
                G3.BackColor = Color.FromArgb(163, 228, 166);

                B1.BackColor = Color.FromArgb(29, 65, 211);
                B2.BackColor = Color.FromArgb(55, 119, 245);
                B3.BackColor = Color.FromArgb(118, 170, 248);

                P1.BackColor = Color.FromArgb(165, 39, 200);
                P2.BackColor = Color.FromArgb(195, 156, 219);
                P3.BackColor = Color.FromArgb(217, 195, 233);
            }
        }

        private void RadioImage5_CheckedChanged(object sender, EventArgs e)
        {
            if (Conversions.ToBoolean(((UI.WP.RadioImage)sender).Checked))
            {
                PictureBox33.Image = Assets.Accessibility_Vision.CF_Img_Grayscale;
                PictureBox32.Image = Assets.Accessibility_Vision.CF_Pie_Grayscale;

                R1.BackColor = Color.FromArgb(93, 93, 93);
                R2.BackColor = Color.FromArgb(122, 122, 122);
                R3.BackColor = Color.FromArgb(169, 169, 169);

                O1.BackColor = Color.FromArgb(126, 126, 126);
                O2.BackColor = Color.FromArgb(166, 166, 166);
                O3.BackColor = Color.FromArgb(200, 200, 200);

                Y1.BackColor = Color.FromArgb(183, 182, 183);
                Y2.BackColor = Color.FromArgb(202, 202, 202);
                Y3.BackColor = Color.FromArgb(220, 220, 220);

                G1.BackColor = Color.FromArgb(93, 93, 93);
                G2.BackColor = Color.FromArgb(172, 172, 172);
                G3.BackColor = Color.FromArgb(202, 202, 202);

                B1.BackColor = Color.FromArgb(70, 70, 70);
                B2.BackColor = Color.FromArgb(113, 113, 113);
                B3.BackColor = Color.FromArgb(163, 163, 163);

                P1.BackColor = Color.FromArgb(93, 93, 93);
                P2.BackColor = Color.FromArgb(175, 174, 175);
                P3.BackColor = Color.FromArgb(206, 206, 206);
            }
        }

        private void RadioImage7_CheckedChanged(object sender, EventArgs e)
        {
            if (Conversions.ToBoolean(((UI.WP.RadioImage)sender).Checked))
            {
                PictureBox33.Image = Assets.Accessibility_Vision.CF_Img_Normal.Invert();
                PictureBox32.Image = Assets.Accessibility_Vision.CF_Pie_Normal.Invert();

                R1.BackColor = Color.FromArgb(53, 208, 211);
                R2.BackColor = Color.FromArgb(28, 174, 193);
                R3.BackColor = Color.FromArgb(22, 111, 120);

                O1.BackColor = Color.FromArgb(39, 158, 214);
                O2.BackColor = Color.FromArgb(22, 101, 199);
                O3.BackColor = Color.FromArgb(16, 63, 139);

                Y1.BackColor = Color.FromArgb(29, 73, 192);
                Y2.BackColor = Color.FromArgb(15, 52, 184);
                Y3.BackColor = Color.FromArgb(13, 35, 132);

                G1.BackColor = Color.FromArgb(200, 131, 211);
                G2.BackColor = Color.FromArgb(136, 45, 140);
                G3.BackColor = Color.FromArgb(90, 32, 88);

                B1.BackColor = Color.FromArgb(231, 191, 47);
                B2.BackColor = Color.FromArgb(202, 134, 18);
                B3.BackColor = Color.FromArgb(135, 84, 15);

                P1.BackColor = Color.FromArgb(89, 220, 57);
                P2.BackColor = Color.FromArgb(61, 98, 40);
                P3.BackColor = Color.FromArgb(42, 61, 28);
            }
        }

        private void RadioImage6_CheckedChanged(object sender, EventArgs e)
        {
            if (Conversions.ToBoolean(((UI.WP.RadioImage)sender).Checked))
            {
                PictureBox33.Image = Assets.Accessibility_Vision.CF_Img_Grayscale.Invert();
                PictureBox32.Image = Assets.Accessibility_Vision.CF_Pie_Grayscale.Invert();

                R1.BackColor = Color.FromArgb(160, 160, 160);
                R2.BackColor = Color.FromArgb(131, 131, 131);
                R3.BackColor = Color.FromArgb(85, 85, 85);

                O1.BackColor = Color.FromArgb(127, 127, 127);
                O2.BackColor = Color.FromArgb(88, 88, 88);
                O3.BackColor = Color.FromArgb(57, 57, 57);

                Y1.BackColor = Color.FromArgb(73, 73, 73);
                Y2.BackColor = Color.FromArgb(55, 55, 55);
                Y3.BackColor = Color.FromArgb(39, 39, 39);

                G1.BackColor = Color.FromArgb(160, 160, 160);
                G2.BackColor = Color.FromArgb(82, 82, 82);
                G3.BackColor = Color.FromArgb(55, 55, 55);

                B1.BackColor = Color.FromArgb(186, 186, 186);
                B2.BackColor = Color.FromArgb(140, 140, 140);
                B3.BackColor = Color.FromArgb(90, 90, 90);

                P1.BackColor = Color.FromArgb(160, 160, 160);
                P2.BackColor = Color.FromArgb(80, 80, 80);
                P3.BackColor = Color.FromArgb(51, 51, 51);
            }
        }

        private void RadioImage2_CheckedChanged(object sender, EventArgs e)
        {
            if (Conversions.ToBoolean(((UI.WP.RadioImage)sender).Checked))
            {
                PictureBox33.Image = Assets.Accessibility_Vision.CF_Img_Red_green_green_weak_deuteranopia;
                PictureBox32.Image = Assets.Accessibility_Vision.CF_Pie_Red_green_green_weak_deuteranopia;

                R1.BackColor = Color.FromArgb(255, 50, 20);
                R2.BackColor = Color.FromArgb(255, 80, 35);
                R3.BackColor = Color.FromArgb(255, 142, 113);

                O1.BackColor = Color.FromArgb(255, 96, 22);
                O2.BackColor = Color.FromArgb(255, 153, 42);
                O3.BackColor = Color.FromArgb(255, 193, 103);

                Y1.BackColor = Color.FromArgb(229, 181, 54);
                Y2.BackColor = Color.FromArgb(239, 205, 62);
                Y3.BackColor = Color.FromArgb(241, 224, 114);

                G1.BackColor = Color.FromArgb(16, 122, 58);
                G2.BackColor = Color.FromArgb(60, 213, 130);
                G3.BackColor = Color.FromArgb(124, 228, 176);

                B1.BackColor = Color.FromArgb(40, 65, 218);
                B2.BackColor = Color.FromArgb(50, 119, 255);
                B3.BackColor = Color.FromArgb(111, 170, 255);

                P1.BackColor = Color.FromArgb(255, 39, 172);
                P2.BackColor = Color.FromArgb(224, 156, 210);
                P3.BackColor = Color.FromArgb(234, 195, 227);
            }
        }

        private void RadioImage3_CheckedChanged(object sender, EventArgs e)
        {
            if (Conversions.ToBoolean(((UI.WP.RadioImage)sender).Checked))
            {
                PictureBox33.Image = Assets.Accessibility_Vision.CF_Img_Red_green_red_weak_protanopia;
                PictureBox32.Image = Assets.Accessibility_Vision.CF_Pie_Red_green_red_weak_protanopia;

                R1.BackColor = Color.FromArgb(204, 121, 137);
                R2.BackColor = Color.FromArgb(233, 151, 151);
                R3.BackColor = Color.FromArgb(239, 188, 190);

                O1.BackColor = Color.FromArgb(220, 152, 110);
                O2.BackColor = Color.FromArgb(239, 190, 97);
                O3.BackColor = Color.FromArgb(247, 215, 138);

                Y1.BackColor = Color.FromArgb(231, 200, 81);
                Y2.BackColor = Color.FromArgb(248, 219, 84);
                Y3.BackColor = Color.FromArgb(250, 231, 126);

                G1.BackColor = Color.FromArgb(57, 87, 0);
                G2.BackColor = Color.FromArgb(117, 162, 51);
                G3.BackColor = Color.FromArgb(163, 195, 123);

                B1.BackColor = Color.FromArgb(29, 53, 201);
                B2.BackColor = Color.FromArgb(55, 93, 215);
                B3.BackColor = Color.FromArgb(118, 149, 222);

                P1.BackColor = Color.FromArgb(165, 105, 255);
                P2.BackColor = Color.FromArgb(195, 176, 249);
                P3.BackColor = Color.FromArgb(217, 207, 251);
            }
        }

        private void RadioImage4_CheckedChanged(object sender, EventArgs e)
        {
            if (Conversions.ToBoolean(((UI.WP.RadioImage)sender).Checked))
            {
                PictureBox33.Image = Assets.Accessibility_Vision.CF_Img_Blue_yellow_tritanopia;
                PictureBox32.Image = Assets.Accessibility_Vision.CF_Pie_Blue_yellow__tritanopia;

                R1.BackColor = Color.FromArgb(160, 60, 47);
                R2.BackColor = Color.FromArgb(180, 85, 63);
                R3.BackColor = Color.FromArgb(208, 146, 133);

                O1.BackColor = Color.FromArgb(150, 87, 44);
                O2.BackColor = Color.FromArgb(151, 126, 58);
                O3.BackColor = Color.FromArgb(179, 169, 114);

                Y1.BackColor = Color.FromArgb(138, 145, 64);
                Y2.BackColor = Color.FromArgb(145, 161, 72);
                Y3.BackColor = Color.FromArgb(174, 191, 121);

                G1.BackColor = Color.FromArgb(26, 91, 47);
                G2.BackColor = Color.FromArgb(77, 171, 113);
                G3.BackColor = Color.FromArgb(140, 202, 166);

                B1.BackColor = Color.FromArgb(132, 110, 211);
                B2.BackColor = Color.FromArgb(152, 156, 245);
                B3.BackColor = Color.FromArgb(181, 193, 248);

                P1.BackColor = Color.FromArgb(244, 101, 200);
                P2.BackColor = Color.FromArgb(227, 179, 219);
                P3.BackColor = Color.FromArgb(237, 210, 233);
            }
        }

        private void trackBarX1_ValueChanged(object sender, EventArgs e)
        {
            ButtonR1.FocusRectWidth = Conversions.ToInteger(((UI.Controllers.TrackBarX)sender).Value);
        }

        private void trackBarX2_ValueChanged(object sender, EventArgs e)
        {
            ButtonR1.FocusRectHeight = Conversions.ToInteger(((UI.Controllers.TrackBarX)sender).Value);
        }

        private void trackBarX1_ValueChanged_1(object sender, EventArgs e)
        {
            Panel2.Width = Conversions.ToInteger(((UI.Controllers.TrackBarX)sender).Value);
        }

        private void button21_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TM = Default.Get(Program.WindowStyle))
            {
                CheckBox1.Checked = TM.WindowsEffects.WindowAnimation;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TM = Default.Get(Program.WindowStyle))
            {
                CheckBox27.Checked = TM.WindowsEffects.AnimateControlsInsideWindow;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TM = Default.Get(Program.WindowStyle))
            {
                CheckBox2.Checked = TM.WindowsEffects.WindowShadow;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TM = Default.Get(Program.WindowStyle))
            {
                CheckBox3.Checked = TM.WindowsEffects.WindowUIEffects;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TM = Default.Get(Program.WindowStyle))
            {
                CheckBox11.Checked = TM.WindowsEffects.ShowWinContentDrag;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TM = Default.Get(Program.WindowStyle))
            {
                CheckBox21.Checked = TM.WindowsEffects.ShakeToMinimize;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TM = Default.Get(Program.WindowStyle))
            {
                CheckBox4.Checked = TM.WindowsEffects.IconsShadow;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TM = Default.Get(Program.WindowStyle))
            {
                CheckBox10.Checked = TM.WindowsEffects.IconsDesktopTranslSel;
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TM = Default.Get(Program.WindowStyle))
            {
                CheckBox6.Checked = TM.WindowsEffects.MenuAnimation;
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TM = Default.Get(Program.WindowStyle))
            {
                ComboBox1.SelectedIndex = TM.WindowsEffects.MenuFade == Theme.Structures.WinEffects.MenuAnimType.Fade ? 0 : 1;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TM = Default.Get(Program.WindowStyle))
            {
                CheckBox5.Checked = TM.WindowsEffects.MenuSelectionFade;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TM = Default.Get(Program.WindowStyle))
            {
                CheckBox12.Checked = TM.WindowsEffects.KeyboardUnderline;
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TM = Default.Get(Program.WindowStyle))
            {
                CheckBox8.Checked = TM.WindowsEffects.ComboBoxAnimation;
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TM = Default.Get(Program.WindowStyle))
            {
                CheckBox7.Checked = TM.WindowsEffects.ListBoxSmoothScrolling;
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TM = Default.Get(Program.WindowStyle))
            {
                CheckBox9.Checked = TM.WindowsEffects.TooltipAnimation;
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TM = Default.Get(Program.WindowStyle))
            {
                ComboBox2.SelectedIndex = TM.WindowsEffects.TooltipFade == Theme.Structures.WinEffects.MenuAnimType.Fade ? 0 : 1;
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TM = Default.Get(Program.WindowStyle))
            {
                CheckBox17.Checked = TM.WindowsEffects.BalloonNotifications;
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TM = Default.Get(Program.WindowStyle))
            {
                CheckBox13.Checked = TM.WindowsEffects.AWT_Enabled;
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TM = Default.Get(Program.WindowStyle))
            {
                CheckBox14.Checked = TM.WindowsEffects.AWT_BringActivatedWindowToTop;
            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TM = Default.Get(Program.WindowStyle))
            {
                CheckBox20.Checked = TM.WindowsEffects.SysListView32;
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            RadioButton1.Checked = true;
        }

        private void button22_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TM = Default.Get(Program.WindowStyle))
            {
                CheckBox23.Checked = TM.WindowsEffects.DisableNavBar;
            }
        }

        private void button24_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TM = Default.Get(Program.WindowStyle))
            {
                CheckBox16.Checked = TM.WindowsEffects.Win11ClassicContextMenu;
            }
        }

        private void button23_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TM = Default.Get(Program.WindowStyle))
            {
                CheckBox22.Checked = TM.WindowsEffects.Win11BootDots;
            }
        }

        private void button25_Click(object sender, EventArgs e)
        {
            RadioImage1.Checked = true;
        }

        private void button31_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TM = Default.Get(Program.WindowStyle))
            {
                CheckBox15.Checked = TM.WindowsEffects.SnapCursorToDefButton;
            }
        }

        private void button30_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TM = Default.Get(Program.WindowStyle))
            {
                CheckBox19.Checked = TM.WindowsEffects.ShowSecondsInSystemClock;
            }
        }

        private void button29_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TM = Default.Get(Program.WindowStyle))
            {
                CheckBox18.Checked = TM.WindowsEffects.PaintDesktopVersion;
            }
        }

        private void button28_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TM = Default.Get(Program.WindowStyle))
            {
                CheckBox25.Checked = TM.WindowsEffects.FullScreenStartMenu;
            }
        }

        private void button27_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TM = Default.Get(Program.WindowStyle))
            {
                CheckBox24.Checked = TM.WindowsEffects.AutoHideScrollBars;
            }
        }

        private void button26_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TM = Default.Get(Program.WindowStyle))
            {
                CheckBox26.Checked = TM.WindowsEffects.ClassicVolMixer;
            }
        }

        private void button32_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TM = Default.Get(Program.WindowStyle))
            {
                checkBox28.Checked = TM.WindowsEffects.EnableAeroPeek;
            }
        }

        private void button33_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TM = Default.Get(Program.WindowStyle))
            {
                checkBox29.Checked = TM.WindowsEffects.AlwaysHibernateThumbnails;
            }
        }

        private void WinEffecter_AspectEnabledChanged(object sender, EventArgs e)
        {
            if ((sender as UI.WP.Toggle).Checked
                && Program.Settings.ThemeApplyingBehavior.Show_WinEffects_Alert
                && Forms.WinEffectsAlert.ShowDialog() != DialogResult.OK)
            {
                AspectEnabled = false;
                (sender as UI.WP.Toggle).Checked = false;
            }
        }
    }
}