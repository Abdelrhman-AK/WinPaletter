using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;
using WinPaletter.Assets;
using WinPaletter.Theme;
using WinPaletter.UI.Simulation;
using static WinPaletter.PreviewHelpers;
using static WinPaletter.Theme.Manager;

namespace WinPaletter
{
    public partial class AltTabEditor
    {
        private void Form_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            Process.Start(Links.Wiki.AltTab);
        }

        public AltTabEditor()
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
            Manager TMx = Default.Get(Program.WindowStyle);
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
            if (Program.Settings.AspectsControl.Enabled && !Program.Settings.AspectsControl.AltTab)
            {
                MsgBox(Program.Lang.Strings.Aspects.Disabled_Apply_0, MessageBoxButtons.OK, MessageBoxIcon.Warning, Program.Lang.Strings.Aspects.Disabled_Apply_1);
                return;
            }

            Cursor = Cursors.WaitCursor;

            using (Manager TMx = new(Source.Registry))
            {
                if (Program.Settings.BackupTheme.Enabled && Program.Settings.BackupTheme.AutoBackupOnApplySingleAspect)
                {
                    string filename = Program.GetUniqueFileName($"{Program.Settings.BackupTheme.BackupPath}\\OnAspectApply", $"{TMx.Info.ThemeName}_{DateTime.Now.Hour}.{DateTime.Now.Minute}.{DateTime.Now.Second}.wpth");
                    TMx.Save(Source.File, filename);
                }

                ApplyToTM(TMx);
                ApplyToTM(Program.TM);
                ApplyToTM(Program.TM_Original);
                TMx.AltTab.Apply();
            }

            Cursor = Cursors.Default;
        }

        private void AltTabEditor_Load(object sender, EventArgs e)
        {
            DesignerData data = new(this)
            {
                AspectName = Program.Lang.Strings.Aspects.AltTab,
                Enabled = Program.TM.AltTab.Enabled,
                Import_theme = false,
                Import_msstyles = false,
                GeneratePalette = false,
                GenerateMSTheme = false,
                Import_preset = false,
                CanSwitchMode = false,
                CanOpenColorsEffects = false,

                OnLoadIntoCurrentTheme = LoadIntoCurrentTheme,
                OnApply = Apply,
                OnImportFromDefault = LoadFromDefault,
                OnImportFromWPTH = LoadFromWPTH,
                OnImportFromCurrentApplied = LoadFromCurrent,
            };

            LoadData(data);

            LoadFromTM(Program.TM);

            switch (Program.WindowStyle)
            {
                case WindowStyle.W12:
                    {
                        RadioImage1.Image = WinLogos.Win12;
                        break;
                    }

                case WindowStyle.W11:
                    {
                        RadioImage1.Image = WinLogos.Win11;
                        break;
                    }

                case WindowStyle.W10:
                    {
                        RadioImage1.Image = WinLogos.Win10;
                        break;
                    }

                case WindowStyle.W81:
                    {
                        RadioImage1.Image = WinLogos.Win8_1;
                        break;
                    }

                case WindowStyle.W7:
                    {
                        RadioImage1.Image = WinLogos.Win7;
                        break;
                    }

                case WindowStyle.WVista:
                    {
                        RadioImage1.Image = WinLogos.WinVista;
                        break;
                    }

                case WindowStyle.WXP:
                    {
                        RadioImage1.Image = WinLogos.WinXP;
                        break;
                    }

                default:
                    {
                        RadioImage1.Image = WinLogos.Win12;
                        break;
                    }
            }

            RadioImage2.Image = WinLogos.WinXP;

            pnl_preview1.BackgroundImage = Program.Wallpaper;
            Classic_Preview1.BackgroundImage = Program.Wallpaper;

            SetClassicPanelRaisedRColors(Program.TM, PanelRRaised1);
            SetClassicPanelColors(Program.TM, PanelR1);

            Panel1.BackColor = Program.TM.Win32.Hilight;

            switch (Program.WindowStyle)
            {
                case WindowStyle.W12:
                    {
                        WinElement1.Style = WinElement.Styles.AltTab11;
                        WinElement1.DarkMode = !Program.TM.Windows12.WinMode_Light;
                        break;
                    }

                case WindowStyle.W11:
                    {
                        WinElement1.Style = WinElement.Styles.AltTab11;
                        WinElement1.DarkMode = !Program.TM.Windows11.WinMode_Light;
                        break;
                    }

                case WindowStyle.W10:
                    {
                        WinElement1.Style = WinElement.Styles.AltTab10;
                        WinElement1.DarkMode = !Program.TM.Windows10.WinMode_Light;
                        break;
                    }

                case WindowStyle.W81:
                    {
                        if (Program.TM.Windows81.VisualStyles.VisualStylesType == Theme.Structures.VisualStyles.DefaultVisualStyles.AeroLite)
                        {
                            WinElement1.Style = WinElement.Styles.AltTab8AeroLite;
                            WinElement1.Background = Program.TM.Win32.Window;
                            WinElement1.Background2 = Program.TM.Win32.Hilight;
                            WinElement1.LinkColor = Program.TM.Win32.ButtonText;
                            WinElement1.ForeColor = Program.TM.Win32.WindowText;
                        }
                        else
                        {
                            WinElement1.Style = WinElement.Styles.AltTab8Aero;
                            WinElement1.Background = Program.TM.Windows81.PersonalColors_Background;
                            WinElement1.Background2 = Program.TM.Windows81.PersonalColors_Background;
                        }

                        break;
                    }

                case WindowStyle.W7:
                    {
                        if (Program.TM.Windows7.VisualStyles.VisualStylesType == Theme.Structures.VisualStyles.DefaultVisualStyles.AeroOpaque)
                        {
                            WinElement1.Style = WinElement.Styles.AltTab7Opaque;
                        }
                        else if (Program.TM.Windows7.VisualStyles.VisualStylesType == Theme.Structures.VisualStyles.DefaultVisualStyles.Basic)
                        {
                            WinElement1.Style = WinElement.Styles.AltTab7Basic;
                        }
                        else
                        {
                            WinElement1.Style = WinElement.Styles.AltTab7Aero;
                        }

                        WinElement1.Background = Program.TM.Windows7.ColorizationColor;
                        WinElement1.Background2 = Program.TM.Windows7.ColorizationAfterglow;
                        WinElement1.BackColorAlpha = Program.TM.Windows7.ColorizationBlurBalance;
                        WinElement1.Win7ColorBal = Program.TM.Windows7.ColorizationColorBalance;
                        WinElement1.Win7GlowBal = Program.TM.Windows7.ColorizationAfterglowBalance;
                        WinElement1.NoisePower = Program.TM.Windows7.ColorizationGlassReflectionIntensity;
                        WinElement1.Shadow = Program.TM.WindowsEffects.WindowShadow;
                        break;
                    }
            }

            Panel2.BackColor = PanelRRaised1.BackColor;
            LabelR1.Font = Program.TM.MetricsFonts.CaptionFont;

            GroupBox4.Enabled = WinElement1.Style == WinElement.Styles.AltTab10 || ExplorerPatcher.CanBeUsed;

            if (ExplorerPatcher.CanBeUsed)
            {
                try
                {
                    if (Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer", "AltTabSettings", 0)) == 3)
                    {
                        WinElement1.Style = WinElement.Styles.AltTab10;
                        WinElement1.DarkMode = !Program.TM.Windows11.WinMode_Light;
                    }
                }
                finally
                {
                    Registry.CurrentUser.Close();
                }
            }

            if (WinElement1.Style == WinElement.Styles.AltTab7Basic)
            {
                WinElement1.Size = new(360, 100);
            }
            else
            {
                WinElement1.Size = new(450, 150);
            }

            WinElement1.Left = (int)Math.Round((WinElement1.Parent.Width - WinElement1.Width) / 2d);
            WinElement1.Top = (int)Math.Round((WinElement1.Parent.Height - WinElement1.Height) / 2d);

            tabs_preview_1.DoubleBuffer();
        }

        public void LoadFromTM(Manager TM)
        {
            AspectEnabled = TM.AltTab.Enabled;
            RadioImage1.Checked = TM.AltTab.Style == Theme.Structures.AltTab.Styles.Default | TM.AltTab.Style == Theme.Structures.AltTab.Styles.EP_Win10;
            RadioImage2.Checked = TM.AltTab.Style == Theme.Structures.AltTab.Styles.ClassicNT;
            Trackbar1.Value = TM.AltTab.Win10Opacity;
        }

        public void ApplyToTM(Manager TM)
        {
            TM.AltTab.Enabled = AspectEnabled;
            TM.AltTab.Style = RadioImage1.Checked ? Theme.Structures.AltTab.Styles.Default : Theme.Structures.AltTab.Styles.ClassicNT;
            if (ExplorerPatcher.CanBeUsed & WinElement1.Style == WinElement.Styles.AltTab10)
                TM.AltTab.Style = Theme.Structures.AltTab.Styles.EP_Win10;
            TM.AltTab.Win10Opacity = Trackbar1.Value;
        }

        private void RadioImage2_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioImage2.Checked)
                tabs_preview_1.SelectedIndex = 1;
        }

        private void RadioImage1_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioImage1.Checked)
                tabs_preview_1.SelectedIndex = 0;
        }

        private void trackBarX1_ValueChanged(object sender, EventArgs e)
        {
            if (WinElement1.Style == WinElement.Styles.AltTab10) { WinElement1.BackColorAlpha = Trackbar1.Value; }
        }
    }
}