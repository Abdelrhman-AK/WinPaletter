using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;
using WinPaletter.Theme;
using WinPaletter.UI.Controllers;
using WinPaletter.UI.WP;
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

        private void LoadFromWPTH(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new() { Filter = Program.Filters.WinPaletterTheme, Title = Program.Lang.Strings.Extensions.OpenWinPaletterTheme })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    using (Manager TMx = new(Manager.Source.File, dlg.FileName))
                    {
                        LoadFromTM(TMx);
                    }
                }
            }
        }

        private void LoadFromCurrent(object sender, EventArgs e)
        {
            Manager TMx = new(Manager.Source.Registry);
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

        private void ImportWin12Preset(object sender, EventArgs e)
        {
            using (Manager TMx = Default.FromOS(WindowStyle.W12)) { LoadFromTM(TMx); }
        }

        private void ImportWin11Preset(object sender, EventArgs e)
        {
            using (Manager TMx = Default.FromOS(WindowStyle.W11)) { LoadFromTM(TMx); }
        }

        private void ImportWin10Preset(object sender, EventArgs e)
        {
            using (Manager TMx = Default.FromOS(WindowStyle.W10)) { LoadFromTM(TMx); }
        }

        private void ImportWin81Preset(object sender, EventArgs e)
        {
            using (Manager TMx = Default.FromOS(WindowStyle.W81)) { LoadFromTM(TMx); }
        }

        private void ImportWin8Preset(object sender, EventArgs e)
        {
            using (Manager TMx = Default.FromOS(WindowStyle.W8)) { LoadFromTM(TMx); }
        }

        private void ImportWin7Preset(object sender, EventArgs e)
        {
            using (Manager TMx = Default.FromOS(WindowStyle.W7)) { LoadFromTM(TMx); }
        }

        private void ImportWinVistaPreset(object sender, EventArgs e)
        {
            using (Manager TMx = Default.FromOS(WindowStyle.WVista)) { LoadFromTM(TMx); }
        }

        private void ImportWinXPPreset(object sender, EventArgs e)
        {
            using (Manager TMx = Default.FromOS(WindowStyle.WXP)) { LoadFromTM(TMx); }
        }

        private void Apply(object sender, EventArgs e)
        {
            if (Program.Settings.AspectsControl.Enabled && !Program.Settings.AspectsControl.Effects)
            {
                MsgBox(Program.Lang.Strings.Aspects.Disabled_Apply_0, MessageBoxButtons.OK, MessageBoxIcon.Warning, Program.Lang.Strings.Aspects.Disabled_Apply_1);
                return;
            }

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

            Program.TM.WindowsEffects.Apply();
            Program.TM.Win32.Broadcast_UPM_ToDefUsers();

            Cursor = Cursors.Default;
        }

        private void WinEffecter_Load(object sender, EventArgs e)
        {
            DesignerData data = new(this)
            {
                AspectName = Program.Lang.Strings.Aspects.WinEffects,
                Enabled = Program.TM.WindowsEffects.Enabled,
                Import_theme = false,
                Import_msstyles = false,
                GeneratePalette = false,
                GenerateMSTheme = false,
                Import_preset = true,
                CanSwitchMode = false,
                CanOpenColorsEffects = false,

                OnLoadIntoCurrentTheme = LoadIntoCurrentTheme,
                OnApply = Apply,
                OnImportFromDefault = LoadFromDefault,
                OnImportFromWPTH = LoadFromWPTH,
                OnImportFromCurrentApplied = LoadFromCurrent,
                OnImportFromScheme_12 = ImportWin11Preset,
                OnImportFromScheme_11 = ImportWin11Preset,
                OnImportFromScheme_10 = ImportWin10Preset,
                OnImportFromScheme_81 = ImportWin81Preset,
                OnImportFromScheme_8 = ImportWin8Preset,
                OnImportFromScheme_7 = ImportWin7Preset,
                OnImportFromScheme_Vista = ImportWinVistaPreset,
                OnImportFromScheme_XP = ImportWinXPPreset,
            };
            LoadData(data);

            LoadFromTM(Program.TM);
            SetClassicButtonColors(Program.TM, ButtonR1);
        }

        public void LoadFromTM(Manager TM)
        {
            Theme.Structures.WinEffects Effects = TM.WindowsEffects;
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

            Panel2.Width = (int)Effects.Caret;
        }

        public void ApplyToTM(Manager TM)
        {
            Theme.Structures.WinEffects Effects = TM.WindowsEffects;
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
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            Panel2.Visible = !Panel2.Visible;
        }

        private void trackBarX1_ValueChanged(object sender, EventArgs e)
        {
            ButtonR1.FocusRectWidth = (sender as TrackBarX).Value;
        }

        private void trackBarX2_ValueChanged(object sender, EventArgs e)
        {
            ButtonR1.FocusRectHeight = (sender as TrackBarX).Value;
        }

        private void trackBarX1_ValueChanged_1(object sender, EventArgs e)
        {
            Panel2.Width = (sender as TrackBarX).Value;
        }

        private void button21_Click(object sender, EventArgs e)
        {
            using (Manager TM = Default.FromOS(Program.WindowStyle))
            {
                CheckBox1.Checked = TM.WindowsEffects.WindowAnimation;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (Manager TM = Default.FromOS(Program.WindowStyle))
            {
                CheckBox27.Checked = TM.WindowsEffects.AnimateControlsInsideWindow;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (Manager TM = Default.FromOS(Program.WindowStyle))
            {
                CheckBox2.Checked = TM.WindowsEffects.WindowShadow;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (Manager TM = Default.FromOS(Program.WindowStyle))
            {
                CheckBox3.Checked = TM.WindowsEffects.WindowUIEffects;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (Manager TM = Default.FromOS(Program.WindowStyle))
            {
                CheckBox11.Checked = TM.WindowsEffects.ShowWinContentDrag;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            using (Manager TM = Default.FromOS(Program.WindowStyle))
            {
                CheckBox21.Checked = TM.WindowsEffects.ShakeToMinimize;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            using (Manager TM = Default.FromOS(Program.WindowStyle))
            {
                CheckBox4.Checked = TM.WindowsEffects.IconsShadow;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            using (Manager TM = Default.FromOS(Program.WindowStyle))
            {
                CheckBox10.Checked = TM.WindowsEffects.IconsDesktopTranslSel;
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            using (Manager TM = Default.FromOS(Program.WindowStyle))
            {
                CheckBox6.Checked = TM.WindowsEffects.MenuAnimation;
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            using (Manager TM = Default.FromOS(Program.WindowStyle))
            {
                ComboBox1.SelectedIndex = TM.WindowsEffects.MenuFade == Theme.Structures.WinEffects.MenuAnimType.Fade ? 0 : 1;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            using (Manager TM = Default.FromOS(Program.WindowStyle))
            {
                CheckBox5.Checked = TM.WindowsEffects.MenuSelectionFade;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            using (Manager TM = Default.FromOS(Program.WindowStyle))
            {
                CheckBox12.Checked = TM.WindowsEffects.KeyboardUnderline;
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            using (Manager TM = Default.FromOS(Program.WindowStyle))
            {
                CheckBox8.Checked = TM.WindowsEffects.ComboBoxAnimation;
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            using (Manager TM = Default.FromOS(Program.WindowStyle))
            {
                CheckBox7.Checked = TM.WindowsEffects.ListBoxSmoothScrolling;
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            using (Manager TM = Default.FromOS(Program.WindowStyle))
            {
                CheckBox9.Checked = TM.WindowsEffects.TooltipAnimation;
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            using (Manager TM = Default.FromOS(Program.WindowStyle))
            {
                ComboBox2.SelectedIndex = TM.WindowsEffects.TooltipFade == Theme.Structures.WinEffects.MenuAnimType.Fade ? 0 : 1;
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            using (Manager TM = Default.FromOS(Program.WindowStyle))
            {
                CheckBox17.Checked = TM.WindowsEffects.BalloonNotifications;
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            using (Manager TM = Default.FromOS(Program.WindowStyle))
            {
                CheckBox13.Checked = TM.WindowsEffects.AWT_Enabled;
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            using (Manager TM = Default.FromOS(Program.WindowStyle))
            {
                CheckBox14.Checked = TM.WindowsEffects.AWT_BringActivatedWindowToTop;
            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            using (Manager TM = Default.FromOS(Program.WindowStyle))
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
            using (Manager TM = Default.FromOS(Program.WindowStyle))
            {
                CheckBox23.Checked = TM.WindowsEffects.DisableNavBar;
            }
        }

        private void button24_Click(object sender, EventArgs e)
        {
            using (Manager TM = Default.FromOS(Program.WindowStyle))
            {
                CheckBox16.Checked = TM.WindowsEffects.Win11ClassicContextMenu;
            }
        }

        private void button23_Click(object sender, EventArgs e)
        {
            using (Manager TM = Default.FromOS(Program.WindowStyle))
            {
                CheckBox22.Checked = TM.WindowsEffects.Win11BootDots;
            }
        }

        private void button31_Click(object sender, EventArgs e)
        {
            using (Manager TM = Default.FromOS(Program.WindowStyle))
            {
                CheckBox15.Checked = TM.WindowsEffects.SnapCursorToDefButton;
            }
        }

        private void button30_Click(object sender, EventArgs e)
        {
            using (Manager TM = Default.FromOS(Program.WindowStyle))
            {
                CheckBox19.Checked = TM.WindowsEffects.ShowSecondsInSystemClock;
            }
        }

        private void button29_Click(object sender, EventArgs e)
        {
            using (Manager TM = Default.FromOS(Program.WindowStyle))
            {
                CheckBox18.Checked = TM.WindowsEffects.PaintDesktopVersion;
            }
        }

        private void button28_Click(object sender, EventArgs e)
        {
            using (Manager TM = Default.FromOS(Program.WindowStyle))
            {
                CheckBox25.Checked = TM.WindowsEffects.FullScreenStartMenu;
            }
        }

        private void button27_Click(object sender, EventArgs e)
        {
            using (Manager TM = Default.FromOS(Program.WindowStyle))
            {
                CheckBox24.Checked = TM.WindowsEffects.AutoHideScrollBars;
            }
        }

        private void button26_Click(object sender, EventArgs e)
        {
            using (Manager TM = Default.FromOS(Program.WindowStyle))
            {
                CheckBox26.Checked = TM.WindowsEffects.ClassicVolMixer;
            }
        }

        private void button32_Click(object sender, EventArgs e)
        {
            using (Manager TM = Default.FromOS(Program.WindowStyle))
            {
                checkBox28.Checked = TM.WindowsEffects.EnableAeroPeek;
            }
        }

        private void button33_Click(object sender, EventArgs e)
        {
            using (Manager TM = Default.FromOS(Program.WindowStyle))
            {
                checkBox29.Checked = TM.WindowsEffects.AlwaysHibernateThumbnails;
            }
        }

        private void WinEffecter_AspectEnabledChanged(object sender, EventArgs e)
        {
            if (IsShown && AspectEnabled && Program.Settings.ThemeApplyingBehavior.Show_WinEffects_Alert && Forms.WinEffectsAlert.ShowDialog(Forms.MainForm) != DialogResult.OK)
            {
                AspectEnabled = false;
            }
        }
    }
}