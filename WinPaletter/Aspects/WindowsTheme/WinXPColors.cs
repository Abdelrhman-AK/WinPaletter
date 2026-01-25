using Devcorp.Controls.VisualStyles;
using libmsstyle;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using WinPaletter.Theme;
using WinPaletter.UI.WP;
using static WinPaletter.PreviewHelpers;
using static WinPaletter.TypesExtensions.ColorsExtensions;

namespace WinPaletter
{
    public partial class WinXPColors : AspectsTemplate
    {
        Manager backup_TM;

        public WinXPColors()
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
            using (Manager TMx = new(Manager.Source.Registry)) { LoadFromTM(TMx); }
        }

        private void LoadFromDefault(object sender, EventArgs e)
        {
            using (Manager TMx = Default.FromOS(Program.WindowStyle)) { LoadFromTM(TMx); }
        }

        private void LoadIntoCurrentTheme(object sender, EventArgs e)
        {
            ApplyToTM(Program.TM);
            Close();
        }

        private void Apply(object sender, EventArgs e)
        {
            if (Program.Settings.AspectsControl.Enabled && !Program.Settings.AspectsControl.WinColors)
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
            Program.TM.WindowsXP.Apply();

            ApplyToTM(Program.TM_Original);

            Cursor = Cursors.Default;
        }

        private void WinXPColors_FormClosed(object sender, FormClosedEventArgs e)
        {
            backup_TM?.Dispose();
        }

        private void WinXPColors_Load(object sender, EventArgs e)
        {
            DesignerData data = new(this)
            {
                AspectName = string.Format(Program.Lang.Strings.Aspects.WinTheme, OS.Name),
                Enabled = Program.TM.WindowsXP.Enabled,
                GeneratePalette = false,
                GenerateMSTheme = false,
                Import_preset = false,
                CanSwitchMode = false,
                CanOpenColorsEffects = false,

                OnLoadIntoCurrentTheme = LoadIntoCurrentTheme,
                OnApply = Apply,
                OnImportFromDefault = LoadFromDefault,
                OnImportFromWPTH = LoadFromWPTH,
                OnImportFromCurrentApplied = LoadFromCurrent
            };

            windowsDesktop1.BackgroundImage = Program.Wallpaper;

            LoadData(data);

            LoadFromTM(Program.TM);
        }

        public void LoadFromTM(Manager TM)
        {
            backup_TM = TM.Clone();

            AspectEnabled = TM.WindowsXP.Enabled;

            if (TM.WindowsXP.VisualStyles.VisualStylesType == Theme.Structures.VisualStyles.DefaultVisualStyles.LunaBlue) WXP_Luna_Blue.Checked = true;
            else if (TM.WindowsXP.VisualStyles.VisualStylesType == Theme.Structures.VisualStyles.DefaultVisualStyles.LunaOlive) WXP_Luna_OliveGreen.Checked = true;
            else if (TM.WindowsXP.VisualStyles.VisualStylesType == Theme.Structures.VisualStyles.DefaultVisualStyles.LunaSilver) WXP_Luna_Silver.Checked = true;
            else if (TM.WindowsXP.VisualStyles.VisualStylesType == Theme.Structures.VisualStyles.DefaultVisualStyles.Classic) WXP_Classic.Checked = true;
            else if (TM.WindowsXP.VisualStyles.VisualStylesType == Theme.Structures.VisualStyles.DefaultVisualStyles.Custom) theme_custom_check.Checked = true;
            else WXP_Luna_Blue.Checked = true;

            VS_textbox.Text = TM.WindowsXP.VisualStyles.ThemeFile;
            VS_ColorsList.SelectedItem = TM.WindowsXP.VisualStyles.ColorScheme;

            if (TM.WindowsXP.VisualStyles.SizeScheme.ToLower() == "normalsize" && VS_SizesList.Items.Contains("Normal")) TM.WindowsXP.VisualStyles.SizeScheme = "Normal";
            else if (TM.WindowsXP.VisualStyles.SizeScheme.ToLower() == "normalsize" && VS_SizesList.Items.Contains("normal")) TM.WindowsXP.VisualStyles.SizeScheme = "normal";

            VS_SizesList.SelectedItem = TM.WindowsXP.VisualStyles.SizeScheme;

            VS_ReplaceColors.Checked = TM.WindowsXP.VisualStyles.OverrideColors;
            VS_ReplaceMetrics.Checked = TM.WindowsXP.VisualStyles.OverrideSizes;

            windowsDesktop1.HookedTM = TM;
            windowsDesktop1.LoadFromTM(TM);

            windowsDesktop1.HookedTM = TM;
            windowsDesktop1.LoadFromTM(TM);
        }

        public void ApplyToTM(Manager TM)
        {
            TM.WindowsXP.VisualStyles.Enabled = true;

            if (theme_custom_check.Checked) TM.WindowsXP.VisualStyles.VisualStylesType = Theme.Structures.VisualStyles.DefaultVisualStyles.Custom;
            else if (WXP_Luna_Blue.Checked) TM.WindowsXP.VisualStyles.VisualStylesType = Theme.Structures.VisualStyles.DefaultVisualStyles.LunaBlue;
            else if (WXP_Luna_OliveGreen.Checked) TM.WindowsXP.VisualStyles.VisualStylesType = Theme.Structures.VisualStyles.DefaultVisualStyles.LunaOlive;
            else if (WXP_Luna_Silver.Checked) TM.WindowsXP.VisualStyles.VisualStylesType = Theme.Structures.VisualStyles.DefaultVisualStyles.LunaSilver;
            else if (WXP_Classic.Checked) TM.WindowsXP.VisualStyles.VisualStylesType = Theme.Structures.VisualStyles.DefaultVisualStyles.Classic;
            else TM.WindowsXP.VisualStyles.VisualStylesType = Theme.Structures.VisualStyles.DefaultVisualStyles.LunaBlue;

            TM.WindowsXP.VisualStyles.ThemeFile = VS_textbox.Text;
            TM.WindowsXP.VisualStyles.ColorScheme = (VS_ColorsList.SelectedItem ?? string.Empty).ToString();
            TM.WindowsXP.VisualStyles.SizeScheme = (VS_SizesList.SelectedItem ?? string.Empty).ToString().ToLower() == "normal" ? "NormalSize" : (VS_SizesList.SelectedItem ?? string.Empty).ToString();
            TM.WindowsXP.VisualStyles.OverrideColors = VS_ReplaceColors.Checked;
            TM.WindowsXP.VisualStyles.OverrideSizes = VS_ReplaceMetrics.Checked;

            if (VS_ReplaceColors.Checked)
            {
                string theme = VS_textbox.Text;

                try
                {
                    // Newer versions of msstyles
                    using (VisualStyle visualStyle = new(theme)) { TM.Win32 = visualStyle.ClassicColors(); }
                }
                catch
                {
                    // Old msstyles (Windows XP)
                    try
                    {
                        if (Path.GetExtension(theme).ToLower() == ".msstyles")
                        {
                            File.WriteAllText($@"{SysPaths.appData}\VisualStyles\Luna\win32uischeme.theme", $"[VisualStyles]{"\r\n"}Path={theme}{"\r\n"}ColorStyle=NormalColor{"\r\n"}Size=NormalSize");
                            theme = $@"{SysPaths.appData}\VisualStyles\Luna\win32uischeme.theme";
                        }

                        if (!string.IsNullOrEmpty(theme) && File.Exists(theme))
                        {
                            using (VisualStyleFile vs = new(theme)) { TM.Win32.Load(Theme.Structures.Win32UI.Sources.VisualStyles, vs.Metrics); }
                        }
                    }
                    catch
                    {
                        MsgBox(Program.Lang.Strings.Messages.InvalidTheme, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

            if (VS_ReplaceMetrics.Checked)
            {
                string theme = VS_textbox.Text;

                try
                {
                    // Newer versions of msstyles
                    using (VisualStyle visualStyle = new(theme)) { TM.MetricsFonts = visualStyle.MetricsFonts(); }
                }
                catch
                {
                    // Old msstyles (Windows XP)
                    try
                    {
                        if (Path.GetExtension(theme).ToLower() == ".msstyles")
                        {
                            File.WriteAllText($@"{SysPaths.appData}\VisualStyles\Luna\win32uischeme.theme", $"[VisualStyles]{"\r\n"}Path={theme}{"\r\n"}ColorStyle=NormalColor{"\r\n"}Size=NormalSize");
                            theme = $@"{SysPaths.appData}\VisualStyles\Luna\win32uischeme.theme";
                        }

                        if (!string.IsNullOrEmpty(theme) && File.Exists(theme))
                        {
                            using (VisualStyleFile vs = new(theme))
                            {
                                TM.MetricsFonts.Overwrite_Metrics(vs.Metrics);
                                TM.MetricsFonts.Overwrite_Fonts(vs.Metrics);
                            }
                        }
                    }
                    catch
                    {
                        MsgBox(Program.Lang.Strings.Messages.InvalidTheme, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dlg = new() { Filter = Program.Filters.PNG, Title = Program.Lang.Strings.Extensions.SavePNG })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    using (Bitmap bmp_thumbnail = new(windowsDesktop1.ToBitmap())) { bmp_thumbnail?.Save(dlg.FileName); }
                }
            }
        }

        private void VS_ColorsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            windowsDesktop1.VisualStylesColorScheme = VS_ColorsList.SelectedItem.ToString();
        }

        private void ReplaceColors()
        {
            if (!VS_ReplaceColors.Checked || WXP_Classic.Checked)
            {
                windowsDesktop1.LoadClassicColors(backup_TM.Win32);
            }
            else
            {
                string theme;

                if (WXP_Luna_Blue.Checked)
                {
                    File.WriteAllText($@"{SysPaths.appData}\VisualStyles\Luna\win32uischeme.theme", $"[VisualStyles]{"\r\n"}Path={SysPaths.MSSTYLES_Luna_WP}{"\r\n"}ColorStyle=NormalColor{"\r\n"}Size=NormalSize");
                    theme = $@"{SysPaths.appData}\VisualStyles\Luna\win32uischeme.theme";
                }
                else if (WXP_Luna_OliveGreen.Checked)
                {
                    File.WriteAllText($@"{SysPaths.appData}\VisualStyles\Luna\win32uischeme.theme", $"[VisualStyles]{"\r\n"}Path={SysPaths.MSSTYLES_Luna_WP}{"\r\n"}ColorStyle=HomeStead{"\r\n"}Size=NormalSize");
                    theme = $@"{SysPaths.appData}\VisualStyles\Luna\win32uischeme.theme";
                }
                else if (WXP_Luna_Silver.Checked)
                {
                    File.WriteAllText($@"{SysPaths.appData}\VisualStyles\Luna\win32uischeme.theme", $"[VisualStyles]{"\r\n"}Path={SysPaths.MSSTYLES_Luna_WP}{"\r\n"}ColorStyle=Metallic{"\r\n"}Size=NormalSize");
                    theme = $@"{SysPaths.appData}\VisualStyles\Luna\win32uischeme.theme";
                }
                else if (WXP_Classic.Checked)
                {
                    theme = string.Empty;
                }
                else
                {
                    theme = VS_textbox.Text;
                }

                if (File.Exists(theme))
                {
                    try
                    {
                        if (Path.GetExtension(theme).ToLower() == ".msstyles")
                        {
                            File.WriteAllText($@"{SysPaths.appData}\VisualStyles\Luna\win32uischeme.theme", $"[VisualStyles]{"\r\n"}Path={theme}{"\r\n"}ColorStyle={VS_ColorsList.SelectedItem}{"\r\n"}Size=NormalSize");
                            theme = $@"{SysPaths.appData}\VisualStyles\Luna\win32uischeme.theme";
                        }

                        if (File.Exists(theme))
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
            if (!VS_ReplaceMetrics.Checked || WXP_Classic.Checked)
            {
                windowsDesktop1.LoadMetricsFonts(backup_TM);
            }
            else
            {
                string theme;

                if (WXP_Luna_Blue.Checked)
                {
                    File.WriteAllText($@"{SysPaths.appData}\VisualStyles\Luna\win32uischeme.theme", $"[VisualStyles]{"\r\n"}Path={SysPaths.Theme_Luna_WP}{"\r\n"}ColorStyle=NormalColor{"\r\n"}Size=NormalSize");
                    theme = $@"{SysPaths.appData}\VisualStyles\Luna\win32uischeme.theme";
                }
                else if (WXP_Luna_OliveGreen.Checked)
                {
                    File.WriteAllText($@"{SysPaths.appData}\VisualStyles\Luna\win32uischeme.theme", $"[VisualStyles]{"\r\n"}Path={SysPaths.Theme_Luna_WP}{"\r\n"}ColorStyle=HomeStead{"\r\n"}Size=NormalSize");
                    theme = $@"{SysPaths.appData}\VisualStyles\Luna\win32uischeme.theme";
                }
                else if (WXP_Luna_Silver.Checked)
                {
                    File.WriteAllText($@"{SysPaths.appData}\VisualStyles\Luna\win32uischeme.theme", $"[VisualStyles]{"\r\n"}Path={SysPaths.Theme_Luna_WP}{"\r\n"}ColorStyle=Metallic{"\r\n"}Size=NormalSize");
                    theme = $@"{SysPaths.appData}\VisualStyles\Luna\win32uischeme.theme";
                }
                else if (WXP_Classic.Checked)
                {
                    theme = string.Empty;
                }
                else
                {
                    theme = VS_textbox.Text;
                }

                if (File.Exists(theme))
                {
                    try
                    {
                        if (Path.GetExtension(theme).ToLower() == ".msstyles")
                        {
                            File.WriteAllText($@"{SysPaths.appData}\VisualStyles\Luna\win32uischeme.theme", $"[VisualStyles]{"\r\n"}Path={theme}{"\r\n"}ColorStyle={VS_ColorsList.SelectedItem}{"\r\n"}Size=NormalSize");
                            theme = $@"{SysPaths.appData}\VisualStyles\Luna\win32uischeme.theme";
                        }

                        if (!string.IsNullOrEmpty(theme) && File.Exists(theme))
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

        private void VS_ReplaceColors_CheckedChanged(object sender, EventArgs e)
        {
            ReplaceColors();
        }

        private void VS_ReplaceMetrics_CheckedChanged(object sender, EventArgs e)
        {
            ReplaceMetrics();
        }

        private void WinXPColors_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            Process.Start(Links.Wiki.WinXPThemes);
        }

        private void VS_textbox_TextChanged(object sender, EventArgs e)
        {
            VS_ColorsList.Items.Clear();
            VS_SizesList.Items.Clear();

            string theme = VS_textbox.Text;

            if (File.Exists(theme))
            {
                if (Path.GetExtension(theme).ToLower() == ".msstyles")
                {
                    File.WriteAllText($@"{SysPaths.appData}\VisualStyles\Luna\win32uischeme.theme", $"[VisualStyles]{"\r\n"}Path={theme}{"\r\n"}ColorStyle=NormalColor{"\r\n"}Size=NormalSize");
                    theme = $@"{SysPaths.appData}\VisualStyles\Luna\win32uischeme.theme";
                }

                windowsDesktop1.VisualStylesPath = theme;

                if (!string.IsNullOrEmpty(theme) && File.Exists(theme))
                {
                    using (VisualStyleFile vs = new(theme))
                    {
                        VS_ColorsList.Items.Clear();

                        try
                        {
                            foreach (VisualStyleScheme x in vs.ColorSchemes) VS_ColorsList.Items.Add(x.Name);
                        }
                        catch { } // Couldn't load visual styles File, so no scheme will be added

                        if (VS_ColorsList.Items.Count > 0) VS_ColorsList.SelectedIndex = 0;

                        VS_SizesList.Items.Clear();

                        try
                        {
                            foreach (VisualStyleSize x in vs.Sizes) VS_SizesList.Items.Add(x.Size);
                        }
                        catch { } // Couldn't load visual styles File, so no scheme will be added

                        if (VS_SizesList.Items.Count > 0) VS_SizesList.SelectedIndex = 0;
                    }
                }

                ReplaceColors();
                ReplaceMetrics();
            }
        }

        private void VS_Browse_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new() { Filter = Program.Filters.VisualStyles_And_Themes, Title = Program.Lang.Strings.Extensions.OpenVisualStyle })
            {
                if (dlg.ShowDialog() == DialogResult.OK) VS_textbox.Text = dlg.FileName;
            }
        }

        private void button21_Click(object sender, EventArgs e)
        {
            VS_textbox.Text = SysPaths.MSSTYLES_Luna_Win;
        }

        private void WXP_Luna_Blue_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as RadioImage).Checked)
            {
                VS_textbox.Text = File.Exists(SysPaths.MSSTYLES_Luna_Win) ? SysPaths.MSSTYLES_Luna_Win : SysPaths.MSSTYLES_Luna_WP;
                VS_ColorsList.SelectedItem = "NormalColor" ?? null;

                windowsDesktop1.VisualStyles = Theme.Structures.VisualStyles.DefaultVisualStyles.LunaBlue;
                ReplaceColors();
                ReplaceMetrics();
            }
        }

        private void WXP_Luna_OliveGreen_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as RadioImage).Checked)
            {
                VS_textbox.Text = File.Exists(SysPaths.MSSTYLES_Luna_Win) ? SysPaths.MSSTYLES_Luna_Win : SysPaths.MSSTYLES_Luna_WP;
                VS_ColorsList.SelectedItem = "HomeStead" ?? null;

                windowsDesktop1.VisualStyles = Theme.Structures.VisualStyles.DefaultVisualStyles.LunaOlive;
                ReplaceColors();
                ReplaceMetrics();
            }
        }

        private void WXP_Luna_Silver_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as RadioImage).Checked)
            {
                VS_textbox.Text = File.Exists(SysPaths.MSSTYLES_Luna_Win) ? SysPaths.MSSTYLES_Luna_Win : SysPaths.MSSTYLES_Luna_WP;
                VS_ColorsList.SelectedItem = "Metallic" ?? null;

                windowsDesktop1.VisualStyles = Theme.Structures.VisualStyles.DefaultVisualStyles.LunaSilver;
                ReplaceColors();
                ReplaceMetrics();
            }
        }

        private void WXP_Classic_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as RadioImage).Checked)
            {
                windowsDesktop1.VisualStyles = Theme.Structures.VisualStyles.DefaultVisualStyles.Classic;
                ReplaceColors();
                ReplaceMetrics();
            }
        }

        private void theme_custom_check_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as RadioImage).Checked)
            {
                windowsDesktop1.VisualStyles = Theme.Structures.VisualStyles.DefaultVisualStyles.Custom;
                windowsDesktop1.VisualStylesColorScheme = VS_ColorsList.SelectedItem?.ToString() ?? string.Empty;
                ReplaceColors();
                ReplaceMetrics();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Process.Start(Links.UxTheme_multi_patcher);
        }
    }
}