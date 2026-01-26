using Devcorp.Controls.VisualStyles;
using libmsstyle;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using WinPaletter.NativeMethods;
using WinPaletter.Templates;
using WinPaletter.Theme;
using WinPaletter.UI.Controllers;
using WinPaletter.UI.Retro;
using static WinPaletter.PreviewHelpers;

namespace WinPaletter
{
    public partial class Metrics_Fonts
    {
        public Metrics_Fonts()
        {
            InitializeComponent();
        }

        private void Metrics_Fonts_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            Process.Start(Links.Wiki.MetricsAndFonts);
        }

        private void LoadFromWPTH(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new() { Filter = Program.Filters.WinPaletterTheme, Title = Program.Localization.Strings.Extensions.OpenWinPaletterTheme })
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

        private void LoadFromTHEME(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new() { Filter = Program.Filters.Themes, Title = Program.Localization.Strings.Extensions.OpenTheme })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    using (INI ini = new(dlg.FileName))
                    using (Manager TMx = Default.FromOS(Program.WindowStyle))
                    {
                        string metricsStr = ini.Read("Metrics", "NonclientMetrics");
                        string iconsmetricsStr = ini.Read("Metrics", "IconMetrics");

                        if (!string.IsNullOrWhiteSpace(metricsStr))
                        {
                            List<byte> bytes = [];
                            foreach (string s in metricsStr.Split(' '))
                            {
                                if (byte.TryParse(s, out byte result))
                                {
                                    bytes.Add(result);
                                }
                            }

                            User32.NONCLIENTMETRICS ncm = new([.. bytes]);

                            TMx.MetricsFonts.CaptionWidth = ncm.iCaptionWidth;
                            TMx.MetricsFonts.CaptionHeight = ncm.iCaptionHeight;
                            TMx.MetricsFonts.SmCaptionWidth = ncm.iSMCaptionWidth;
                            TMx.MetricsFonts.SmCaptionHeight = ncm.iSMCaptionHeight;
                            TMx.MetricsFonts.BorderWidth = ncm.iBorderWidth;
                            TMx.MetricsFonts.PaddedBorderWidth = ncm.iPaddedBorderWidth;
                            TMx.MetricsFonts.CaptionFont = ncm.lfCaptionFont.ToFont();
                            TMx.MetricsFonts.SmCaptionFont = ncm.lfSMCaptionFont.ToFont();

                            TMx.MetricsFonts.MenuHeight = ncm.iMenuHeight;
                            TMx.MetricsFonts.MenuWidth = ncm.iMenuWidth;
                            TMx.MetricsFonts.MenuFont = ncm.lfMenuFont.ToFont();

                            TMx.MetricsFonts.ScrollHeight = ncm.iScrollHeight;
                            TMx.MetricsFonts.ScrollWidth = ncm.iScrollWidth;

                            TMx.MetricsFonts.StatusFont = ncm.lfStatusFont.ToFont();
                            TMx.MetricsFonts.MessageFont = ncm.lfMessageFont.ToFont();
                        }

                        if (!string.IsNullOrWhiteSpace(iconsmetricsStr))
                        {
                            List<byte> bytes = [];
                            foreach (string s in iconsmetricsStr.Split(' '))
                            {
                                if (byte.TryParse(s, out byte result))
                                {
                                    bytes.Add(result);
                                }
                            }

                            User32.ICONMETRICS icm = new([.. bytes]);

                            TMx.MetricsFonts.IconSpacing = icm.iHorzSpacing;
                            TMx.MetricsFonts.IconVerticalSpacing = icm.iVertSpacing;
                            TMx.MetricsFonts.IconFont = icm.lfFont.ToFont();
                        }

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

        private void LoadFromMSSTYLES(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new() { Filter = Program.Filters.VisualStyles_And_Themes, Title = Program.Localization.Strings.Extensions.OpenVisualStyle })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    string theme = dlg.FileName;

                    try
                    {
                        //Newer versions of msstyles
                        using (VisualStyle visualStyle = new(theme))
                        {
                            using (Manager TMx = new(Manager.Source.Empty) { MetricsFonts = visualStyle.MetricsFonts() })
                            {
                                LoadFromTM(TMx);
                            }
                        }
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
                                    LoadMetrics(vs.Metrics);
                                }
                            }
                        }
                        catch
                        {
                            MsgBox(Program.Localization.Strings.Messages.InvalidTheme, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void LoadIntoCurrentTheme(object sender, EventArgs e)
        {
            ApplyToTM(Program.TM);
            Close();
        }

        private void SaveAsTHEME(object sender, EventArgs e)
        {
            using (SaveFileDialog dlg = new() { Filter = Program.Filters.Themes, Title = Program.Localization.Strings.Extensions.SaveTheme })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    using (Manager TMx = new(Manager.Source.Empty))
                    {
                        ApplyToTM(TMx);
                        try
                        {
                            File.WriteAllText(dlg.FileName, TMx.MetricsFonts.ToString(sender is not ToolStripMenuItem ? Program.TM.Win32 : null));
                        }
                        catch (Exception ex)
                        {
                            Forms.BugReport.Throw(ex);
                        }
                    }
                }
            }
        }

        private void Apply(object sender, EventArgs e)
        {
            if (Program.Settings.AspectsControl.Enabled && !Program.Settings.AspectsControl.MetricsFonts)
            {
                MsgBox(Program.Localization.Strings.Aspects.Disabled_Apply_0, MessageBoxButtons.OK, MessageBoxIcon.Warning, Program.Localization.Strings.Aspects.Disabled_Apply_1);
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
            Program.TM.MetricsFonts.Apply();

            Cursor = Cursors.Default;
        }

        private void ModeSwitched(object sender, EventArgs e)
        {
            if (AdvancedMode)
            {
                tablessControl1.SelectedIndex = 0;
            }
            else
            {
                tablessControl1.SelectedIndex = 1;
            }
        }

        private void MetricsFonts_Load(object sender, EventArgs e)
        {
            DesignerData data = new(this)
            {
                AspectName = Program.Localization.Strings.Aspects.MetricsFonts,
                Enabled = Program.TM.MetricsFonts.Enabled,
                Import_theme = true,
                Import_msstyles = true,
                GeneratePalette = false,
                GenerateMSTheme = true,
                Import_preset = false,
                CanOpenColorsEffects = false,

                OnLoadIntoCurrentTheme = LoadIntoCurrentTheme,
                OnApply = Apply,
                OnImportFromDefault = LoadFromDefault,
                OnImportFromWPTH = LoadFromWPTH,
                OnImportFromMSSTYLES = LoadFromMSSTYLES,
                OnImportFromTHEME = LoadFromTHEME,
                OnImportFromCurrentApplied = LoadFromCurrent,
                OnSaveAsMSTheme = SaveAsTHEME,
                OnSaveAsMSTheme_OneAspect = SaveAsTHEME,

                OnModeAdvanced = ModeSwitched,
                OnModeSimple = ModeSwitched,
            };

            LoadData(data);

            AdvancedMode = Program.Settings.AspectsControl.MetricsFonts_Advanced;

            windowMetrics1.BackgroundImage = Program.Wallpaper;
            Desktop_icons.BackgroundImage = Program.Wallpaper;

            alertBox1.Visible = Program.WindowStyle == PreviewHelpers.WindowStyle.W11 || Program.WindowStyle == PreviewHelpers.WindowStyle.W12;

            comboBox1.Items.Clear();
            comboBox1.Items.AddRange([.. Schemes.Metrics.Split('\n').Select(f => f.Split('|')[0])]);
            comboBox1.SelectedIndex = 0;

            LoadFromTM(Program.TM);
            LoadDefaultValues();
        }

        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (TabControl1.SelectedIndex)
            {
                case 0:
                    {
                        tabs_preview.SelectedIndex = 0;
                        windowMetrics1.ShowMenuSection = false;
                        windowMetrics1.ShowAsMenu = true;
                        break;
                    }
                case 1:
                    {
                        tabs_preview.SelectedIndex = 1;
                        windowMetrics1.ShowMenuSection = false;
                        windowMetrics1.ShowAsMenu = false;
                        break;
                    }
                case 2:
                    {
                        windowMetrics1.ShowMenuSection = true;
                        windowMetrics1.ShowAsMenu = true;
                        tabs_preview.SelectedIndex = 0;
                        break;
                    }
                case 3:
                    {
                        windowMetrics1.ShowMenuSection = true;
                        windowMetrics1.ShowAsMenu = false;
                        tabs_preview.SelectedIndex = 0;
                        break;
                    }
            }
        }

        public void LoadFromTM(Manager TM)
        {
            AspectEnabled = TM.MetricsFonts.Enabled;

            Label1.Font = TM.MetricsFonts.CaptionFont;

            Label1.Text = TM.MetricsFonts.CaptionFont.Name;

            Label2.Font = TM.MetricsFonts.IconFont;

            Label2.Text = TM.MetricsFonts.IconFont.Name;

            Label3.Font = TM.MetricsFonts.MenuFont;
            Label3.Text = TM.MetricsFonts.MenuFont.Name;

            Label5.Font = TM.MetricsFonts.SmCaptionFont;
            Label5.Text = TM.MetricsFonts.SmCaptionFont.Name;

            Label4.Font = TM.MetricsFonts.MessageFont;
            Label4.Text = TM.MetricsFonts.MessageFont.Name;

            Label6.Font = TM.MetricsFonts.StatusFont;
            Label6.Text = TM.MetricsFonts.StatusFont.Name;

            TextBox1.Text = TM.MetricsFonts.FontSubstitute_MSShellDlg;
            TextBox2.Text = TM.MetricsFonts.FontSubstitute_MSShellDlg2;
            TextBox3.Text = TM.MetricsFonts.FontSubstitute_SegoeUI;

            CheckBox1.Checked = TM.MetricsFonts.Fonts_SingleBitPP;

            trackBarX1.Value = TM.MetricsFonts.CaptionHeight;
            trackBarX2.Value = TM.MetricsFonts.CaptionWidth;
            trackBarX3.Value = TM.MetricsFonts.BorderWidth;
            trackBarX4.Value = TM.MetricsFonts.PaddedBorderWidth;
            trackBarX5.Value = TM.MetricsFonts.SmCaptionHeight;
            trackBarX6.Value = TM.MetricsFonts.SmCaptionWidth;
            trackBarX7.Value = TM.MetricsFonts.IconVerticalSpacing;
            trackBarX8.Value = TM.MetricsFonts.IconSpacing;
            trackBarX9.Value = TM.MetricsFonts.DesktopIconSize;
            trackBarX10.Value = TM.MetricsFonts.ShellIconSize;
            trackBarX11.Value = TM.MetricsFonts.ShellSmallIconSize;
            trackBarX12.Value = TM.MetricsFonts.MenuHeight;
            trackBarX13.Value = TM.MetricsFonts.MenuWidth;
            trackBarX14.Value = TM.MetricsFonts.ScrollHeight;
            trackBarX15.Value = TM.MetricsFonts.ScrollWidth;

            windowMetrics1.LoadFromTM(TM);
            Desktop_icons.LoadMetrics(TM);
        }

        public void ApplyToTM(Manager TM)
        {
            TM.MetricsFonts.Enabled = AspectEnabled;

            TM.MetricsFonts.CaptionFont = Label1.Font;
            TM.MetricsFonts.IconFont = Label2.Font;
            TM.MetricsFonts.MenuFont = Label3.Font;
            TM.MetricsFonts.MessageFont = Label4.Font;
            TM.MetricsFonts.SmCaptionFont = Label5.Font;
            TM.MetricsFonts.StatusFont = Label6.Font;

            TM.MetricsFonts.CaptionHeight = (int)trackBarX1.Value;
            TM.MetricsFonts.CaptionWidth = (int)trackBarX2.Value;
            TM.MetricsFonts.BorderWidth = (int)trackBarX3.Value;
            TM.MetricsFonts.PaddedBorderWidth = (int)trackBarX4.Value;
            TM.MetricsFonts.SmCaptionHeight = (int)trackBarX5.Value;
            TM.MetricsFonts.SmCaptionWidth = (int)trackBarX6.Value;
            TM.MetricsFonts.IconVerticalSpacing = (int)trackBarX7.Value;
            TM.MetricsFonts.IconSpacing = (int)trackBarX8.Value;
            TM.MetricsFonts.DesktopIconSize = (int)trackBarX9.Value;
            TM.MetricsFonts.ShellIconSize = (int)trackBarX10.Value;
            TM.MetricsFonts.ShellSmallIconSize = (int)trackBarX11.Value;
            TM.MetricsFonts.MenuHeight = (int)trackBarX12.Value;
            TM.MetricsFonts.MenuWidth = (int)trackBarX13.Value;
            TM.MetricsFonts.ScrollHeight = (int)trackBarX14.Value;
            TM.MetricsFonts.ScrollWidth = (int)trackBarX15.Value;

            TM.MetricsFonts.Fonts_SingleBitPP = CheckBox1.Checked;

            TM.MetricsFonts.FontSubstitute_MSShellDlg = TextBox1.Text;
            TM.MetricsFonts.FontSubstitute_MSShellDlg2 = TextBox2.Text;
            TM.MetricsFonts.FontSubstitute_SegoeUI = TextBox3.Text;
        }

        public void LoadMetrics(VisualStyleMetrics vs)
        {
            trackBarX3.Value = vs.Sizes.CaptionBarHeight;
            trackBarX14.Value = vs.Sizes.ScrollbarHeight;
            trackBarX15.Value = vs.Sizes.ScrollbarWidth;
            trackBarX5.Value = vs.Sizes.SMCaptionBarHeight;
            trackBarX7.Value = vs.Sizes.SMCaptionBarWidth;

            Label1.Font = vs.Fonts.CaptionFont;
            windowMetrics1.CaptionFont = vs.Fonts.CaptionFont;

            Label2.Font = vs.Fonts.IconTitleFont;
            Desktop_icons.Font = vs.Fonts.IconTitleFont;
            Label2.Text = vs.Fonts.IconTitleFont.Name;

            Label3.Font = vs.Fonts.MenuFont;
            windowMetrics1.MenuFont = vs.Fonts.CaptionFont;
            Label3.Text = vs.Fonts.MenuFont.Name;

            Label5.Font = vs.Fonts.SmallCaptionFont;
            windowMetrics1.SmCaptionFont = vs.Fonts.CaptionFont;
            Label5.Text = vs.Fonts.SmallCaptionFont.Name;


            Label4.Font = vs.Fonts.MsgBoxFont;
            windowMetrics1.MessageFont = vs.Fonts.CaptionFont;
            Label4.Text = vs.Fonts.MsgBoxFont.Name;

            Label6.Font = vs.Fonts.StatusFont;
            windowMetrics1.StatusFont = vs.Fonts.CaptionFont;
            Label6.Text = vs.Fonts.StatusFont.Name;

            using (Manager TMx = new(Manager.Source.Empty))
            {
                ApplyToTM(TMx);
                LoadFromTM(TMx);
            }
        }

        void LoadDefaultValues()
        {
            using (Manager @default = Default.FromOS(Program.WindowStyle))
            {
                trackBarX1.DefaultValue = @default.MetricsFonts.CaptionHeight;
                trackBarX2.DefaultValue = @default.MetricsFonts.CaptionWidth;
                trackBarX3.DefaultValue = @default.MetricsFonts.BorderWidth;
                trackBarX4.DefaultValue = @default.MetricsFonts.PaddedBorderWidth;
                trackBarX5.DefaultValue = @default.MetricsFonts.SmCaptionHeight;
                trackBarX6.DefaultValue = @default.MetricsFonts.SmCaptionWidth;
                trackBarX7.DefaultValue = @default.MetricsFonts.IconVerticalSpacing;
                trackBarX8.DefaultValue = @default.MetricsFonts.IconSpacing;
                trackBarX9.DefaultValue = @default.MetricsFonts.DesktopIconSize;
                trackBarX10.DefaultValue = @default.MetricsFonts.ShellIconSize;
                trackBarX11.DefaultValue = @default.MetricsFonts.ShellSmallIconSize;
                trackBarX12.DefaultValue = @default.MetricsFonts.MenuHeight;
                trackBarX13.DefaultValue = @default.MetricsFonts.MenuWidth;
                trackBarX14.DefaultValue = @default.MetricsFonts.ScrollHeight;
                trackBarX15.DefaultValue = @default.MetricsFonts.ScrollWidth;
            }
        }

        public void LoadFromWinThemeString(string DB, string ThemeName)
        {
            if (string.IsNullOrWhiteSpace(DB) || !DB.Contains("|") || string.IsNullOrWhiteSpace(ThemeName)) return;

            string SelectedTheme = string.Empty;

            bool Found = false;

            foreach (string theme in DB.Split('\n'))
            {
                if ((theme.Split('|')[0].ToLower() ?? string.Empty) == (ThemeName.ToLower() ?? string.Empty))
                {
                    SelectedTheme = theme;
                    Found = true;
                    break;
                }
            }

            if (!Found) return;

            string metrics = SelectedTheme.Split('|').Where(s => s.ToLower().StartsWith("nonclientmetrics")).FirstOrDefault();
            string icon = SelectedTheme.Split('|').Where(s => s.ToLower().StartsWith("iconmetrics")).FirstOrDefault();

            using (Manager TM_Default = Default.FromOS(Program.WindowStyle))
            using (Manager TMx = Program.TM.Clone())
            {
                TMx.MetricsFonts = TM_Default.MetricsFonts;
                TMx.MetricsFonts.Enabled = AspectEnabled;

                if (!string.IsNullOrWhiteSpace(metrics) && metrics.Contains("="))
                {
                    List<byte> bytes = [];
                    foreach (string s in metrics.Split('=')[1].Split(' '))
                    {
                        if (byte.TryParse(s, out byte result))
                        {
                            bytes.Add(result);
                        }
                    }

                    User32.NONCLIENTMETRICS ncm = new([.. bytes]);

                    TMx.MetricsFonts.CaptionWidth = ncm.iCaptionWidth;
                    TMx.MetricsFonts.CaptionHeight = ncm.iCaptionHeight;
                    TMx.MetricsFonts.SmCaptionWidth = ncm.iSMCaptionWidth;
                    TMx.MetricsFonts.SmCaptionHeight = ncm.iSMCaptionHeight;
                    TMx.MetricsFonts.BorderWidth = ncm.iBorderWidth;
                    TMx.MetricsFonts.PaddedBorderWidth = ncm.iPaddedBorderWidth;
                    TMx.MetricsFonts.CaptionFont = ncm.lfCaptionFont.ToFont();
                    TMx.MetricsFonts.SmCaptionFont = ncm.lfSMCaptionFont.ToFont();

                    TMx.MetricsFonts.MenuHeight = ncm.iMenuHeight;
                    TMx.MetricsFonts.MenuWidth = ncm.iMenuWidth;
                    TMx.MetricsFonts.MenuFont = ncm.lfMenuFont.ToFont();

                    TMx.MetricsFonts.ScrollHeight = ncm.iScrollHeight;
                    TMx.MetricsFonts.ScrollWidth = ncm.iScrollWidth;

                    TMx.MetricsFonts.StatusFont = ncm.lfStatusFont.ToFont();
                    TMx.MetricsFonts.MessageFont = ncm.lfMessageFont.ToFont();
                }

                if (!string.IsNullOrWhiteSpace(icon) && icon.Contains("="))
                {
                    List<byte> bytes = [];
                    foreach (string s in icon.Split('=')[1].Split(' '))
                    {
                        if (byte.TryParse(s, out byte result))
                        {
                            bytes.Add(result);
                        }
                    }

                    User32.ICONMETRICS icm = new([.. bytes]);

                    TMx.MetricsFonts.IconSpacing = icm.iHorzSpacing;
                    TMx.MetricsFonts.IconVerticalSpacing = icm.iVertSpacing;
                    TMx.MetricsFonts.IconFont = icm.lfFont.ToFont();
                }

                if (checkBox2.Checked) TMx.MetricsFonts.Fonts_SingleBitPP = comboBox1.SelectedIndex >= 4;

                LoadFromTM(TMx);
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            using (FontDialog dlg = new() { Font = Label1.Font })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    Label1.Font = dlg.Font;
                    windowMetrics1.CaptionFont = dlg.Font;
                    Label1.Text = dlg.Font.Name;
                }
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            using (FontDialog dlg = new() { Font = Label2.Font })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    Label2.Font = dlg.Font;
                    Desktop_icons.Font = dlg.Font;
                    Label2.Text = dlg.Font.Name;
                }
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            using (FontDialog dlg = new() { Font = Label3.Font })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    windowMetrics1.MenuFont = dlg.Font;
                    Label3.Font = dlg.Font;
                    Label3.Text = dlg.Font.Name;
                }
            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            using (FontDialog dlg = new() { Font = Label4.Font })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    windowMetrics1.MessageFont = dlg.Font;
                    Label4.Font = dlg.Font;
                    Label4.Text = dlg.Font.Name;
                }
            }
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            using (FontDialog dlg = new() { Font = Label5.Font })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    windowMetrics1.SmCaptionFont = dlg.Font;
                    Label5.Font = dlg.Font;
                    Label5.Text = dlg.Font.Name;
                }
            }
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            using (FontDialog dlg = new() { Font = Label6.Font })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    windowMetrics1.StatusFont = dlg.Font;
                    Label6.Font = dlg.Font;
                    Label6.Text = dlg.Font.Name;
                }
            }
        }

        private void Label1_FontChanged(object sender, EventArgs e)
        {
            ((Label)sender).Text = ((Label)sender).Font.FontFamily.Name;
        }

        private void Metrics_Fonts_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.Style.TextRenderingHint = Program.TM.MetricsFonts.Fonts_SingleBitPP ? TextRenderingHint.SingleBitPerPixelGridFit : TextRenderingHint.ClearTypeGridFit;

            Program.Settings.AspectsControl.MetricsFonts_Advanced = AdvancedMode;
            Program.Settings.AspectsControl.Save();
        }

        private void Button14_Click_1(object sender, EventArgs e)
        {
            using (Font F = new(TextBox1.Text, 9f, FontStyle.Regular))
            using (FontDialog dlg = new() { Font = F })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    TextBox1.Text = dlg.Font.Name;
                }
            }
        }

        private void Button15_Click_1(object sender, EventArgs e)
        {
            using (Font F = new(TextBox2.Text, 9f, FontStyle.Regular))
            using (FontDialog dlg = new() { Font = F })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    TextBox2.Text = dlg.Font.Name;
                }
            }
        }

        private void Button17_Click(object sender, EventArgs e)
        {
            using (Font F = new(TextBox3.Text, 9f, FontStyle.Regular))
            using (FontDialog dlg = new() { Font = F })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    TextBox3.Text = dlg.Font.Name;
                }
            }
        }

        private void Button16_Click_1(object sender, EventArgs e)
        {
            TextBox1.Text = "Microsoft Sans Serif";
        }

        private void Button18_Click(object sender, EventArgs e)
        {
            TextBox2.Text = "Tahoma";
        }

        private void Button19_Click(object sender, EventArgs e)
        {
            TextBox3.Text = string.Empty;
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            if (Fonts.Exists(((UI.WP.TextBox)sender).Text.ToString(), FontStyle.Regular))
            {
                ((UI.WP.TextBox)sender).Font = new(((UI.WP.TextBox)sender).Text.ToString(), 9f, FontStyle.Regular);
            }
            else
            {
                ((UI.WP.TextBox)sender).Font = new("Microsoft Sans Serif", 9f, FontStyle.Regular);
            }
        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {
            if (Fonts.Exists(((UI.WP.TextBox)sender).Text.ToString(), FontStyle.Regular))
            {
                ((UI.WP.TextBox)sender).Font = new(((UI.WP.TextBox)sender).Text.ToString(), 9f, FontStyle.Regular);
            }
            else
            {
                ((UI.WP.TextBox)sender).Font = new("Tahoma", 9f, FontStyle.Regular);
            }
        }

        private void TextBox3_TextChanged(object sender, EventArgs e)
        {
            if (Fonts.Exists(((UI.WP.TextBox)sender).Text.ToString(), FontStyle.Regular))
            {
                ((UI.WP.TextBox)sender).Font = new(((UI.WP.TextBox)sender).Text.ToString(), 9f, FontStyle.Regular);
            }
            else
            {
                ((UI.WP.TextBox)sender).Font = new("Segoe UI", 9f, FontStyle.Regular);
            }
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            Program.Style.TextRenderingHint = CheckBox1.Checked ? TextRenderingHint.SingleBitPerPixelGridFit : TextRenderingHint.ClearTypeGridFit;
            windowMetrics1.Refresh();
        }

        private void EditorInvoker(object sender, EditorEventArgs e)
        {
            if (e.PropertyName == nameof(windowMetrics1.CaptionFont))
            {
                Label1.Font = windowMetrics1.CaptionFont;
                Label1.Text = windowMetrics1.CaptionFont.Name;
            }
            else if (e.PropertyName == nameof(windowMetrics1.SmCaptionFont))
            {
                Label5.Font = windowMetrics1.SmCaptionFont;
                Label5.Text = windowMetrics1.SmCaptionFont.Name;
            }
            else if (e.PropertyName == nameof(windowMetrics1.CaptionHeight))
            {
                trackBarX1.Value = windowMetrics1.CaptionHeight;
            }
            else if (e.PropertyName == nameof(windowMetrics1.CaptionWidth))
            {
                trackBarX2.Value = windowMetrics1.CaptionWidth;
            }
            else if (e.PropertyName == nameof(windowMetrics1.BorderWidth))
            {
                trackBarX3.Value = windowMetrics1.BorderWidth;
            }
            else if (e.PropertyName == nameof(windowMetrics1.PaddedBorderWidth))
            {
                trackBarX4.Value = windowMetrics1.PaddedBorderWidth;
            }
            else if (e.PropertyName == nameof(windowMetrics1.SmCaptionHeight))
            {
                trackBarX5.Value = windowMetrics1.SmCaptionHeight;
            }
            else if (e.PropertyName == nameof(windowMetrics1.SmCaptionWidth))
            {
                trackBarX6.Value = windowMetrics1.SmCaptionWidth;
            }
            else if (e.PropertyName == nameof(windowMetrics1.MenuHeight))
            {
                trackBarX12.Value = windowMetrics1.MenuHeight;
            }
            else if (e.PropertyName == nameof(windowMetrics1.MenuFont))
            {
                Label3.Font = windowMetrics1.MenuFont;
                Label3.Text = windowMetrics1.MenuFont.Name;
            }
            else if (e.PropertyName == nameof(windowMetrics1.ScrollWidth))
            {
                trackBarX15.Value = windowMetrics1.ScrollWidth;
            }
            else if (e.PropertyName == nameof(windowMetrics1.ScrollHeight))
            {
                trackBarX14.Value = windowMetrics1.ScrollHeight;
            }
            else if (e.PropertyName == nameof(windowMetrics1.MessageFont))
            {
                Label4.Font = windowMetrics1.MessageFont;
                Label4.Text = windowMetrics1.MessageFont.Name;
            }
            else if (e.PropertyName == nameof(windowMetrics1.StatusFont))
            {
                Label6.Font = windowMetrics1.StatusFont;
                Label6.Text = windowMetrics1.StatusFont.Name;
            }
        }

        private void Desktop_icons_EditorInvoker(object sender, EditorEventArgs e)
        {
            if (e.PropertyName == nameof(Desktop_icons.IconSize))
            {
                trackBarX9.Value = Desktop_icons.IconSize;
            }
            else if (e.PropertyName == nameof(Desktop_icons.IconSpacing_Horizontal))
            {
                trackBarX8.Value = Desktop_icons.IconSpacing_Horizontal;
            }
            else if (e.PropertyName == nameof(Desktop_icons.IconSpacing_Vertical))
            {
                trackBarX7.Value = Desktop_icons.IconSpacing_Vertical;
            }
            else if (e.PropertyName == nameof(Desktop_icons.IconFont))
            {
                Label2.Font = Desktop_icons.IconFont;
                Desktop_icons.Font = Desktop_icons.IconFont;
                Label2.Text = Desktop_icons.IconFont.Name;
            }
        }

        private void trackBarX1_ValueChanged(object sender, EventArgs e)
        {
            windowMetrics1.CaptionHeight = (int)(sender as TrackBarX).Value;
        }

        private void trackBarX2_ValueChanged(object sender, EventArgs e)
        {
            windowMetrics1.CaptionWidth = (int)(sender as TrackBarX).Value;
        }

        private void trackBarX3_ValueChanged(object sender, EventArgs e)
        {
            windowMetrics1.BorderWidth = (int)(sender as TrackBarX).Value;
        }

        private void trackBarX4_ValueChanged(object sender, EventArgs e)
        {
            windowMetrics1.PaddedBorderWidth = (int)(sender as TrackBarX).Value;
        }

        private void trackBarX5_ValueChanged(object sender, EventArgs e)
        {
            windowMetrics1.SmCaptionHeight = (int)(sender as TrackBarX).Value;
        }

        private void trackBarX6_ValueChanged(object sender, EventArgs e)
        {
            windowMetrics1.SmCaptionWidth = (int)(sender as TrackBarX).Value;
        }

        private void trackBarX7_ValueChanged(object sender, EventArgs e)
        {
            Desktop_icons.IconSpacing_Vertical = (int)((TrackBarX)sender).Value;
        }

        private void trackBarX8_ValueChanged(object sender, EventArgs e)
        {
            Desktop_icons.IconSpacing_Horizontal = (int)((TrackBarX)sender).Value;
        }

        private void trackBarX9_ValueChanged(object sender, EventArgs e)
        {
            Desktop_icons.IconSize = (int)((TrackBarX)sender).Value;
        }

        private void trackBarX12_ValueChanged(object sender, EventArgs e)
        {
            windowMetrics1.MenuHeight = (int)(sender as TrackBarX).Value;
        }

        private void trackBarX14_ValueChanged(object sender, EventArgs e)
        {
            windowMetrics1.ScrollHeight = (int)(sender as TrackBarX).Value;
        }

        private void trackBarX15_ValueChanged(object sender, EventArgs e)
        {
            windowMetrics1.ScrollWidth = (int)(sender as TrackBarX).Value;
        }

        private void undo_Click(object sender, EventArgs e)
        {
            using (Manager TM = Default.FromOS(Program.WindowStyle))
            {
                Label1.Font = TM.MetricsFonts.CaptionFont;
                Label1.Text = Label1.Font.Name;
                windowMetrics1.CaptionFont = TM.MetricsFonts.CaptionFont;
            }
        }

        private void button21_Click(object sender, EventArgs e)
        {
            using (Manager TM = Default.FromOS(Program.WindowStyle))
            {
                Label5.Font = TM.MetricsFonts.SmCaptionFont;
                Label5.Text = Label5.Font.Name;
                windowMetrics1.SmCaptionFont = TM.MetricsFonts.SmCaptionFont;
            }
        }

        private void button22_Click(object sender, EventArgs e)
        {
            using (Manager TM = Default.FromOS(Program.WindowStyle))
            {
                Label2.Font = TM.MetricsFonts.IconFont;
                Label2.Text = Label2.Font.Name;
                Desktop_icons.IconFont = TM.MetricsFonts.IconFont;
            }
        }

        private void button23_Click(object sender, EventArgs e)
        {
            using (Manager TM = Default.FromOS(Program.WindowStyle))
            {
                Label3.Font = TM.MetricsFonts.MenuFont;
                Label3.Text = Label3.Font.Name;
                windowMetrics1.MenuFont = TM.MetricsFonts.MenuFont;
            }
        }

        private void button24_Click(object sender, EventArgs e)
        {
            using (Manager TM = Default.FromOS(Program.WindowStyle))
            {
                Label4.Font = TM.MetricsFonts.MessageFont;
                Label4.Text = Label4.Font.Name;
                windowMetrics1.MessageFont = TM.MetricsFonts.MessageFont;
            }
        }

        private void button25_Click(object sender, EventArgs e)
        {
            using (Manager TM = Default.FromOS(Program.WindowStyle))
            {
                Label6.Font = TM.MetricsFonts.StatusFont;
                Label6.Text = Label6.Font.Name;
                windowMetrics1.StatusFont = TM.MetricsFonts.StatusFont;
            }
        }

        private void button26_Click(object sender, EventArgs e)
        {
            using (Manager TM = Default.FromOS(Program.WindowStyle))
            {
                CheckBox1.Checked = TM.MetricsFonts.Fonts_SingleBitPP;
            }
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            windowMetrics1.Classic = !windowMetrics1.Classic;
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            TabControl1.SelectedIndex = 0;
        }

        private void button9_Click_1(object sender, EventArgs e)
        {
            TabControl1.SelectedIndex = 1;
        }

        private void button10_Click_1(object sender, EventArgs e)
        {
            TabControl1.SelectedIndex = 2;
        }

        private void button11_Click_1(object sender, EventArgs e)
        {
            TabControl1.SelectedIndex = 3;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsShown) LoadFromWinThemeString(Schemes.Metrics, comboBox1.SelectedItem.ToString());
        }
    }
}