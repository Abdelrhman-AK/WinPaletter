using Devcorp.Controls.VisualStyles;
using System;
using System.Drawing;
using System.Windows.Forms;
using WinPaletter.Theme;
using static WinPaletter.TypesExtensions.ColorsExtensions;

namespace WinPaletter
{
    public partial class WinXPColors : AspectsTemplate
    {
        Theme.Manager backup_TM;

        public WinXPColors()
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
            using (Manager TMx = new(Theme.Manager.Source.Registry)) { LoadFromTM(TMx); }
        }

        private void LoadFromDefault(object sender, EventArgs e)
        {
            using (Manager TMx = Theme.Default.Get(Program.WindowStyle)) { LoadFromTM(TMx); }
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
                TMx.WindowsXP.Apply();
            }

            Cursor = Cursors.Default;
        }

        private void ModeSwitched(object sender, EventArgs e)
        {
            groupBox51.Visible = AdvancedMode;
        }

        private void GeneratePalette_Image(object sender, EventArgs e)
        {
            Forms.PaletteGenerateFromImage.ShowDialog();
        }

        private void GeneratePalette_Color(object sender, EventArgs e)
        {
            Forms.PaletteGenerateFromColor.ShowDialog();
        }

        private void WinXPColors_FormClosed(object sender, FormClosedEventArgs e)
        {
            backup_TM.Dispose();

            Program.Settings.AspectsControl.WinColors_Advanced = AdvancedMode;
            Program.Settings.AspectsControl.Save();
        }

        private void WinXPColors_Load(object sender, EventArgs e)
        {
            DesignerData data = new(this)
            {
                AspectName = string.Format(Program.Lang.WindowsColors, OS.Name),
                Enabled = Program.TM.WindowsXP.Enabled,
                GeneratePalette = false,
                GenerateMSTheme = false,
                Import_preset = false,

                OnLoadIntoCurrentTheme = LoadIntoCurrentTheme,
                OnApply = Apply,
                OnImportFromDefault = LoadFromDefault,
                OnImportFromWPTH = LoadFromWPTH,
                OnImportFromCurrentApplied = LoadFromCurrent,
                OnGeneratePaletteFromImage = GeneratePalette_Image,
                OnGeneratePaletteFromColor = GeneratePalette_Color,

                OnModeAdvanced = ModeSwitched,
                OnModeSimple = ModeSwitched,
            };

            windowsDesktop1.BackgroundImage = Program.Wallpaper;

            LoadData(data);

            AdvancedMode = Program.Settings.AspectsControl.WinColors_Advanced;

            LoadFromTM(Program.TM);
        }

        public void LoadFromTM(Theme.Manager TM)
        {
            backup_TM = (Manager)TM.Clone();

            AspectEnabled = TM.WindowsXP.Enabled;
            switch (TM.WindowsXP.Theme)
            {
                case Theme.Structures.WindowsXP.Themes.LunaBlue:
                    {
                        WXP_Luna_Blue.Checked = true;
                        break;
                    }

                case Theme.Structures.WindowsXP.Themes.LunaOliveGreen:
                    {
                        WXP_Luna_OliveGreen.Checked = true;
                        break;
                    }

                case Theme.Structures.WindowsXP.Themes.LunaSilver:
                    {
                        WXP_Luna_Silver.Checked = true;
                        break;
                    }

                case Theme.Structures.WindowsXP.Themes.Custom:
                    {
                        WXP_CustomTheme.Checked = true;
                        break;
                    }

                case Theme.Structures.WindowsXP.Themes.Classic:
                    {
                        WXP_Classic.Checked = true;
                        break;
                    }

            }
            WXP_VS_textbox.Text = TM.WindowsXP.ThemeFile;
            if (WXP_VS_ColorsList.Items.Contains(TM.WindowsXP.ColorScheme))
                WXP_VS_ColorsList.SelectedItem = TM.WindowsXP.ColorScheme;

            windowsDesktop1.HookedTM = TM;
            windowsDesktop1.LoadFromTM(TM);
        }

        public void ApplyToTM(Theme.Manager TM)
        {
            TM.WindowsXP.Enabled = AspectEnabled;

            if (WXP_Luna_Blue.Checked)
            {
                TM.WindowsXP.Theme = Theme.Structures.WindowsXP.Themes.LunaBlue;
            }
            else if (WXP_Luna_OliveGreen.Checked)
            {
                TM.WindowsXP.Theme = Theme.Structures.WindowsXP.Themes.LunaOliveGreen;
            }
            else if (WXP_Luna_Silver.Checked)
            {
                TM.WindowsXP.Theme = Theme.Structures.WindowsXP.Themes.LunaSilver;
            }
            else if (WXP_CustomTheme.Checked)
            {
                TM.WindowsXP.Theme = Theme.Structures.WindowsXP.Themes.Custom;
            }
            else if (WXP_Classic.Checked)
            {
                TM.WindowsXP.Theme = Theme.Structures.WindowsXP.Themes.Classic;
            }

            TM.WindowsXP.ThemeFile = WXP_VS_textbox.Text;
            TM.WindowsXP.ColorScheme = WXP_VS_ColorsList.SelectedItem.ToString();


            if (WXP_Classic.Checked || (!WXP_VS_ReplaceColors.Checked && !WXP_VS_ReplaceMetrics.Checked))
            {
                TM.Win32 = (Theme.Structures.Win32UI)(backup_TM.Win32.Clone());
                TM.MetricsFonts = (Theme.Structures.MetricsFonts)(backup_TM.MetricsFonts.Clone());
            }
            else if (WXP_VS_ReplaceMetrics.Checked || WXP_VS_ReplaceColors.Checked)
            {
                string theme;

                if (WXP_Luna_Blue.Checked)
                {
                    System.IO.File.WriteAllText($@"{PathsExt.appData}\VisualStyles\Luna\win32uischeme.theme", $"[VisualStyles]{"\r\n"}Path={PathsExt.MSTheme_Luna_theme}{"\r\n"}ColorStyle=NormalColor{"\r\n"}Size=NormalSize");
                    theme = $@"{PathsExt.appData}\VisualStyles\Luna\win32uischeme.theme";
                }
                else if (WXP_Luna_OliveGreen.Checked)
                {
                    System.IO.File.WriteAllText($@"{PathsExt.appData}\VisualStyles\Luna\win32uischeme.theme", $"[VisualStyles]{"\r\n"}Path={PathsExt.MSTheme_Luna_theme}{"\r\n"}ColorStyle=HomeStead{"\r\n"}Size=NormalSize");
                    theme = $@"{PathsExt.appData}\VisualStyles\Luna\win32uischeme.theme";
                }
                else if (WXP_Luna_Silver.Checked)
                {
                    System.IO.File.WriteAllText($@"{PathsExt.appData}\VisualStyles\Luna\win32uischeme.theme", $"[VisualStyles]{"\r\n"}Path={PathsExt.MSTheme_Luna_theme}{"\r\n"}ColorStyle=Metallic{"\r\n"}Size=NormalSize");
                    theme = $@"{PathsExt.appData}\VisualStyles\Luna\win32uischeme.theme";
                }
                else if (WXP_Classic.Checked)
                {
                    theme = string.Empty;
                }
                else
                {
                    theme = WXP_VS_textbox.Text;
                }

                if (System.IO.File.Exists(theme))
                {
                    try
                    {
                        if (System.IO.Path.GetExtension(theme).ToLower() == ".msstyles")
                        {
                            System.IO.File.WriteAllText($@"{PathsExt.appData}\VisualStyles\Luna\win32uischeme.theme", $"[VisualStyles]{"\r\n"}Path={theme}{"\r\n"}ColorStyle={WXP_VS_ColorsList.SelectedItem}{"\r\n"}Size=NormalSize");
                            theme = $@"{PathsExt.appData}\VisualStyles\Luna\win32uischeme.theme";
                        }

                        if (!string.IsNullOrEmpty(theme) && System.IO.File.Exists(theme))
                        {
                            using (VisualStyleFile vs = new(theme))
                            {
                                if (WXP_VS_ReplaceColors.Checked)
                                {
                                    TM.Win32.EnableTheming = true;
                                    TM.Win32.EnableGradient = true;
                                    TM.Win32.ActiveTitle = vs.Metrics.Colors.ActiveCaption;
                                    TM.Win32.Background = vs.Metrics.Colors.Background;
                                    TM.Win32.ButtonDkShadow = vs.Metrics.Colors.DkShadow3d;
                                    TM.Win32.ButtonFace = vs.Metrics.Colors.Btnface;
                                    TM.Win32.ButtonHilight = vs.Metrics.Colors.BtnHighlight;
                                    TM.Win32.ButtonLight = vs.Metrics.Colors.Light3d;
                                    TM.Win32.ButtonShadow = vs.Metrics.Colors.BtnShadow;
                                    TM.Win32.ButtonText = vs.Metrics.Colors.WindowText;
                                    TM.Win32.GradientActiveTitle = vs.Metrics.Colors.GradientActiveCaption;
                                    TM.Win32.GradientInactiveTitle = vs.Metrics.Colors.GradientInactiveCaption;
                                    TM.Win32.GrayText = vs.Metrics.Colors.GrayText;
                                    TM.Win32.InactiveTitle = vs.Metrics.Colors.InactiveCaption;
                                    TM.Win32.InactiveTitleText = vs.Metrics.Colors.InactiveCaptionText;
                                    TM.Win32.TitleText = vs.Metrics.Colors.CaptionText;
                                    TM.Win32.Window = vs.Metrics.Colors.Window;
                                    TM.Win32.WindowText = vs.Metrics.Colors.WindowText;
                                }
                                else
                                {
                                    TM.Win32 = (Theme.Structures.Win32UI)(backup_TM.Win32.Clone());
                                }

                                if (WXP_VS_ReplaceMetrics.Checked)
                                {
                                    TM.MetricsFonts.Overwrite_Metrics(vs.Metrics);
                                    TM.MetricsFonts.Overwrite_Fonts(vs.Metrics);
                                }
                                else
                                {
                                    TM.MetricsFonts = (Theme.Structures.MetricsFonts)(backup_TM.MetricsFonts.Clone());
                                }
                            }
                        }
                    }
                    catch
                    {
                        if (WXP_VS_ReplaceColors.Checked)
                        {
                            TM.Win32 = (Theme.Structures.Win32UI)(backup_TM.Win32.Clone());
                        }

                        if (WXP_VS_ReplaceMetrics.Checked)
                        {
                            TM.MetricsFonts = (Theme.Structures.MetricsFonts)(backup_TM.MetricsFonts.Clone());
                        }
                    }
                }
                else
                {
                    if (WXP_VS_ReplaceColors.Checked)
                    {
                        TM.Win32 = (Theme.Structures.Win32UI)(backup_TM.Win32.Clone());
                    }

                    if (WXP_VS_ReplaceMetrics.Checked)
                    {
                        TM.MetricsFonts = (Theme.Structures.MetricsFonts)(backup_TM.MetricsFonts.Clone());
                    }
                }

            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dlg = new() { Filter = Program.Filters.PNG, Title = Program.Lang.Filter_SavePNG })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    using (Bitmap bmp_thumbnail = new(windowsDesktop1.ToBitmap())) { bmp_thumbnail?.Save(dlg.FileName); }
                }
            }
        }


        private void WXP_Luna_Blue_CheckedChanged(object sender, EventArgs e)
        {
            if (WXP_Luna_Blue.Checked)
            {
                windowsDesktop1.WindowsXPTheme = Theme.Structures.WindowsXP.Themes.LunaBlue;
                ReplaceColors();
                ReplaceMetrics();
            }
        }

        private void WXP_Luna_OliveGreen_CheckedChanged(object sender, EventArgs e)
        {
            if (WXP_Luna_OliveGreen.Checked)
            {
                windowsDesktop1.WindowsXPTheme = Theme.Structures.WindowsXP.Themes.LunaOliveGreen;
                ReplaceColors();
                ReplaceMetrics();
            }
        }

        private void WXP_Luna_Silver_CheckedChanged(object sender, EventArgs e)
        {
            if (WXP_Luna_Silver.Checked)
            {
                windowsDesktop1.WindowsXPTheme = Theme.Structures.WindowsXP.Themes.LunaSilver;
                ReplaceColors();
                ReplaceMetrics();
            }
        }

        private void WXP_CustomTheme_CheckedChanged(object sender, EventArgs e)
        {
            if (WXP_CustomTheme.Checked)
            {
                windowsDesktop1.WindowsXPTheme = Theme.Structures.WindowsXP.Themes.Custom;
                ReplaceColors();
                ReplaceMetrics();
            }

            GroupBox48.Enabled = WXP_CustomTheme.Checked;
        }

        private void WXP_Classic_CheckedChanged(object sender, EventArgs e)
        {
            if (WXP_Classic.Checked)
            {
                windowsDesktop1.WindowsXPTheme = Theme.Structures.WindowsXP.Themes.Classic;
                ReplaceColors();
                ReplaceMetrics();
            }
        }

        private void WXP_VS_textbox_TextChanged(object sender, EventArgs e)
        {
            string theme = WXP_VS_textbox.Text;

            if (System.IO.File.Exists(theme))
            {
                if (System.IO.Path.GetExtension(theme).ToLower() == ".msstyles")
                {
                    System.IO.File.WriteAllText($@"{PathsExt.appData}\VisualStyles\Luna\win32uischeme.theme", $"[VisualStyles]{"\r\n"}Path={theme}{"\r\n"}ColorStyle=NormalColor{"\r\n"}Size=NormalSize");
                    theme = $@"{PathsExt.appData}\VisualStyles\Luna\win32uischeme.theme";
                }

                windowsDesktop1.WindowsXPThemePath = theme;

                if (!string.IsNullOrEmpty(theme) && System.IO.File.Exists(theme))
                {
                    using (VisualStyleFile vs = new(theme))
                    {
                        WXP_VS_ColorsList.Items.Clear();

                        try
                        {
                            foreach (VisualStyleScheme x in vs.ColorSchemes) WXP_VS_ColorsList.Items.Add(x.Name);
                        }
                        catch { } // Couldn't load visual styles file, so no scheme will be added

                        if (WXP_VS_ColorsList.Items.Count > 0)
                            WXP_VS_ColorsList.SelectedIndex = 0;
                    }
                }

                ReplaceColors();
                ReplaceMetrics();
            }
        }

        private void WXP_VS_Browse_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new() { Filter = Program.Filters.VisualStyles_And_Themes, Title = Program.Lang.Filter_OpenVisualStyle })
            {
                if (dlg.ShowDialog() == DialogResult.OK) WXP_VS_textbox.Text = dlg.FileName;
            }
        }

        private void WXP_VS_ColorsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            windowsDesktop1.WindowsXPThemeColorScheme = WXP_VS_ColorsList.SelectedItem.ToString();
        }

        private void ReplaceColors()
        {
            if (!WXP_VS_ReplaceColors.Checked || WXP_Classic.Checked)
            {
                windowsDesktop1.LoadClassicColors(backup_TM.Win32);
            }
            else
            {
                string theme;

                if (WXP_Luna_Blue.Checked)
                {
                    System.IO.File.WriteAllText($@"{PathsExt.appData}\VisualStyles\Luna\win32uischeme.theme", $"[VisualStyles]{"\r\n"}Path={PathsExt.MSTheme_Luna_theme}{"\r\n"}ColorStyle=NormalColor{"\r\n"}Size=NormalSize");
                    theme = $@"{PathsExt.appData}\VisualStyles\Luna\win32uischeme.theme";
                }
                else if (WXP_Luna_OliveGreen.Checked)
                {
                    System.IO.File.WriteAllText($@"{PathsExt.appData}\VisualStyles\Luna\win32uischeme.theme", $"[VisualStyles]{"\r\n"}Path={PathsExt.MSTheme_Luna_theme}{"\r\n"}ColorStyle=HomeStead{"\r\n"}Size=NormalSize");
                    theme = $@"{PathsExt.appData}\VisualStyles\Luna\win32uischeme.theme";
                }
                else if (WXP_Luna_Silver.Checked)
                {
                    System.IO.File.WriteAllText($@"{PathsExt.appData}\VisualStyles\Luna\win32uischeme.theme", $"[VisualStyles]{"\r\n"}Path={PathsExt.MSTheme_Luna_theme}{"\r\n"}ColorStyle=Metallic{"\r\n"}Size=NormalSize");
                    theme = $@"{PathsExt.appData}\VisualStyles\Luna\win32uischeme.theme";
                }
                else if (WXP_Classic.Checked)
                {
                    theme = string.Empty;
                }
                else
                {
                    theme = WXP_VS_textbox.Text;
                }

                if (System.IO.File.Exists(theme))
                {
                    try
                    {
                        if (System.IO.Path.GetExtension(theme).ToLower() == ".msstyles")
                        {
                            System.IO.File.WriteAllText($@"{PathsExt.appData}\VisualStyles\Luna\win32uischeme.theme", $"[VisualStyles]{"\r\n"}Path={theme}{"\r\n"}ColorStyle={WXP_VS_ColorsList.SelectedItem}{"\r\n"}Size=NormalSize");
                            theme = $@"{PathsExt.appData}\VisualStyles\Luna\win32uischeme.theme";
                        }

                        if (!string.IsNullOrEmpty(theme) && System.IO.File.Exists(theme))
                        {
                            using (VisualStyleFile vs = new(theme)) { windowsDesktop1.LoadClassicColors(vs.Metrics); }
                        }
                    }
                    catch
                    {
                        windowsDesktop1.LoadClassicColors(backup_TM.Win32);
                    }
                }
                else
                {
                    windowsDesktop1.LoadClassicColors(backup_TM.Win32);
                }
            }
        }

        private void ReplaceMetrics()
        {
            if (!WXP_VS_ReplaceMetrics.Checked || WXP_Classic.Checked)
            {
                windowsDesktop1.LoadMetricsFonts(backup_TM);
            }
            else
            {
                string theme;

                if (WXP_Luna_Blue.Checked)
                {
                    System.IO.File.WriteAllText($@"{PathsExt.appData}\VisualStyles\Luna\win32uischeme.theme", $"[VisualStyles]{"\r\n"}Path={PathsExt.MSTheme_Luna_theme}{"\r\n"}ColorStyle=NormalColor{"\r\n"}Size=NormalSize");
                    theme = $@"{PathsExt.appData}\VisualStyles\Luna\win32uischeme.theme";
                }
                else if (WXP_Luna_OliveGreen.Checked)
                {
                    System.IO.File.WriteAllText($@"{PathsExt.appData}\VisualStyles\Luna\win32uischeme.theme", $"[VisualStyles]{"\r\n"}Path={PathsExt.MSTheme_Luna_theme}{"\r\n"}ColorStyle=HomeStead{"\r\n"}Size=NormalSize");
                    theme = $@"{PathsExt.appData}\VisualStyles\Luna\win32uischeme.theme";
                }
                else if (WXP_Luna_Silver.Checked)
                {
                    System.IO.File.WriteAllText($@"{PathsExt.appData}\VisualStyles\Luna\win32uischeme.theme", $"[VisualStyles]{"\r\n"}Path={PathsExt.MSTheme_Luna_theme}{"\r\n"}ColorStyle=Metallic{"\r\n"}Size=NormalSize");
                    theme = $@"{PathsExt.appData}\VisualStyles\Luna\win32uischeme.theme";
                }
                else if (WXP_Classic.Checked)
                {
                    theme = string.Empty;
                }
                else
                {
                    theme = WXP_VS_textbox.Text;
                }

                if (System.IO.File.Exists(theme))
                {
                    try
                    {
                        if (System.IO.Path.GetExtension(theme).ToLower() == ".msstyles")
                        {
                            System.IO.File.WriteAllText($@"{PathsExt.appData}\VisualStyles\Luna\win32uischeme.theme", $"[VisualStyles]{"\r\n"}Path={theme}{"\r\n"}ColorStyle={WXP_VS_ColorsList.SelectedItem}{"\r\n"}Size=NormalSize");
                            theme = $@"{PathsExt.appData}\VisualStyles\Luna\win32uischeme.theme";
                        }

                        if (!string.IsNullOrEmpty(theme) && System.IO.File.Exists(theme))
                        {
                            using (VisualStyleFile vs = new(theme)) { windowsDesktop1.LoadMetricsFonts(vs.Metrics); }
                        }
                    }
                    catch
                    {
                        windowsDesktop1.LoadClassicColors(backup_TM.Win32);
                    }
                }
                else
                {
                    windowsDesktop1.LoadClassicColors(backup_TM.Win32);
                }
            }
        }

        private void WXP_VS_ReplaceColors_CheckedChanged(object sender, EventArgs e)
        {
            ReplaceColors();
        }

        private void WXP_VS_ReplaceMetrics_CheckedChanged(object sender, EventArgs e)
        {
            ReplaceMetrics();
        }

        private void WinXPColors_HelpButtonClicked(object sender, System.ComponentModel.CancelEventArgs e)
        {
            System.Diagnostics.Process.Start(Links.Wiki.WinXPThemes);
        }
    }
}