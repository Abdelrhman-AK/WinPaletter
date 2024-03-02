using Devcorp.Controls.VisualStyles;
using libmsstyle;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using WinPaletter.Theme;
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

        private void LoadFromMSSTYLES(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new() { Filter = Program.Filters.VisualStyles_And_Themes, Title = Program.Lang.Filter_OpenVisualStyle })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    string theme = dlg.FileName;

                    try
                    {
                        //Newer versions of msstyles
                        using (libmsstyle.VisualStyle visualStyle = new(theme))
                        {
                            using (Theme.Manager TMx = new(Manager.Source.Empty) { MetricsFonts = visualStyle.MetricsFonts() })
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
                            if (System.IO.Path.GetExtension(theme).ToLower() == ".msstyles")
                            {
                                System.IO.File.WriteAllText($@"{PathsExt.appData}\VisualStyles\Luna\win32uischeme.theme", $"[VisualStyles]{"\r\n"}Path={theme}{"\r\n"}ColorStyle=NormalColor{"\r\n"}Size=NormalSize");
                                theme = $@"{PathsExt.appData}\VisualStyles\Luna\win32uischeme.theme";
                            }

                            if (!string.IsNullOrEmpty(theme) && System.IO.File.Exists(theme))
                            {
                                using (VisualStyleFile vs = new(theme))
                                {
                                    LoadMetrics(vs.Metrics);
                                }
                            }
                        }
                        catch (Exception ex) { throw ex; }
                    }
                }
            }
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
                ApplyToTM(TMx);
                ApplyToTM(Program.TM);
                TMx.MetricsFonts.Apply();
            }

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

        private void EditFonts_Load(object sender, EventArgs e)
        {
            DesignerData data = new(this)
            {
                AspectName = Program.Lang.MetricsFonts,
                Enabled = Program.TM.MetricsFonts.Enabled,
                Import_theme = false,
                Import_msstyles = true,
                GeneratePalette = false,
                GenerateMSTheme = false,
                Import_preset = true,

                OnLoadIntoCurrentTheme = LoadIntoCurrentTheme,
                OnApply = Apply,
                OnImportFromDefault = LoadFromDefault,
                OnImportFromWPTH = LoadFromWPTH,
                OnImportFromMSSTYLES = LoadFromMSSTYLES,
                OnImportFromCurrentApplied = LoadFromCurrent,
                OnImportFromScheme_11 = ImportWin11Preset,
                OnImportFromScheme_10 = ImportWin10Preset,
                OnImportFromScheme_81 = ImportWin81Preset,
                OnImportFromScheme_7 = ImportWin7Preset,
                OnImportFromScheme_Vista = ImportWinVistaPreset,
                OnImportFromScheme_XP = ImportWinXPPreset,

                OnModeAdvanced = ModeSwitched,
                OnModeSimple = ModeSwitched,
            };

            LoadData(data);

            AdvancedMode = Program.Settings.AspectsControl.MetricsFonts_Advanced;

            windowMetrics1.BackgroundImage = Program.Wallpaper;
            Desktop_icons.BackgroundImage = Program.Wallpaper;

            LoadFromTM(Program.TM);
            LoadDefaultValues();
        }

        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (TabControl1.SelectedIndex)
            {
                case 0:
                    {
                        tabs_preview_1.SelectedIndex = 0;
                        windowMetrics1.ShowMenuSection = false;
                        windowMetrics1.ShowAsMenu = true;
                        break;
                    }
                case 1:
                    {
                        tabs_preview_1.SelectedIndex = 1;
                        windowMetrics1.ShowMenuSection = false;
                        windowMetrics1.ShowAsMenu = false;
                        break;
                    }
                case 2:
                    {
                        windowMetrics1.ShowMenuSection = true;
                        windowMetrics1.ShowAsMenu = true;
                        tabs_preview_1.SelectedIndex = 0;
                        break;
                    }
                case 3:
                    {
                        windowMetrics1.ShowMenuSection = true;
                        windowMetrics1.ShowAsMenu = false;
                        tabs_preview_1.SelectedIndex = 0;
                        break;
                    }
            }
        }

        public void LoadFromTM(Theme.Manager TM)
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

            windowMetrics1.LoadMetrics(TM);
            Desktop_icons.LoadMetrics(TM);
        }

        public void ApplyToTM(Theme.Manager TM)
        {
            TM.MetricsFonts.Enabled = AspectEnabled;

            TM.MetricsFonts.CaptionFont = Label1.Font;
            TM.MetricsFonts.IconFont = Label2.Font;
            TM.MetricsFonts.MenuFont = Label3.Font;
            TM.MetricsFonts.MessageFont = Label4.Font;
            TM.MetricsFonts.SmCaptionFont = Label5.Font;
            TM.MetricsFonts.StatusFont = Label6.Font;

            TM.MetricsFonts.CaptionHeight = trackBarX1.Value;
            TM.MetricsFonts.CaptionWidth = trackBarX2.Value;
            TM.MetricsFonts.BorderWidth = trackBarX3.Value;
            TM.MetricsFonts.PaddedBorderWidth = trackBarX4.Value;
            TM.MetricsFonts.SmCaptionHeight = trackBarX5.Value;
            TM.MetricsFonts.SmCaptionWidth = trackBarX6.Value;
            TM.MetricsFonts.IconVerticalSpacing = trackBarX7.Value;
            TM.MetricsFonts.IconSpacing = trackBarX8.Value;
            TM.MetricsFonts.DesktopIconSize = trackBarX9.Value;
            TM.MetricsFonts.ShellIconSize = trackBarX10.Value;
            TM.MetricsFonts.ShellSmallIconSize = trackBarX11.Value;
            TM.MetricsFonts.MenuHeight = trackBarX12.Value;
            TM.MetricsFonts.MenuWidth = trackBarX13.Value;
            TM.MetricsFonts.ScrollHeight = trackBarX14.Value;
            TM.MetricsFonts.ScrollWidth = trackBarX15.Value;

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

            using (Theme.Manager TMx = new(Manager.Source.Empty))
            {
                ApplyToTM(TMx);
                LoadFromTM(TMx);
            }
        }

        void LoadDefaultValues()
        {
            using (Theme.Manager @default = Default.Get(Program.WindowStyle))
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
            Program.Style.RenderingHint = Program.TM.MetricsFonts.Fonts_SingleBitPP ? TextRenderingHint.SingleBitPerPixelGridFit : TextRenderingHint.ClearTypeGridFit;

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
            Program.Style.RenderingHint = CheckBox1.Checked ? TextRenderingHint.SingleBitPerPixelGridFit : TextRenderingHint.ClearTypeGridFit;
            windowMetrics1.Refresh();
        }

        private void EditorInvoker(object sender, UI.Retro.EditorEventArgs e)
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

        private void Desktop_icons_EditorInvoker(object sender, UI.Retro.EditorEventArgs e)
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
            windowMetrics1.CaptionHeight = Conversions.ToInteger(((UI.Controllers.TrackBarX)sender).Value);
        }

        private void trackBarX2_ValueChanged(object sender, EventArgs e)
        {
            windowMetrics1.CaptionWidth = Conversions.ToInteger(((UI.Controllers.TrackBarX)sender).Value);
        }

        private void trackBarX3_ValueChanged(object sender, EventArgs e)
        {
            windowMetrics1.BorderWidth = Conversions.ToInteger(((UI.Controllers.TrackBarX)sender).Value);
        }

        private void trackBarX4_ValueChanged(object sender, EventArgs e)
        {
            windowMetrics1.PaddedBorderWidth = Conversions.ToInteger(((UI.Controllers.TrackBarX)sender).Value);
        }

        private void trackBarX5_ValueChanged(object sender, EventArgs e)
        {
            windowMetrics1.SmCaptionHeight = Conversions.ToInteger(((UI.Controllers.TrackBarX)sender).Value);
        }

        private void trackBarX6_ValueChanged(object sender, EventArgs e)
        {
            windowMetrics1.SmCaptionWidth = Conversions.ToInteger(((UI.Controllers.TrackBarX)sender).Value);
        }

        private void trackBarX7_ValueChanged(object sender, EventArgs e)
        {
            Desktop_icons.IconSpacing_Vertical = ((UI.Controllers.TrackBarX)sender).Value;
        }

        private void trackBarX8_ValueChanged(object sender, EventArgs e)
        {
            Desktop_icons.IconSpacing_Horizontal = ((UI.Controllers.TrackBarX)sender).Value;
        }

        private void trackBarX9_ValueChanged(object sender, EventArgs e)
        {
            Desktop_icons.IconSize = ((UI.Controllers.TrackBarX)sender).Value;
        }

        private void trackBarX12_ValueChanged(object sender, EventArgs e)
        {
            windowMetrics1.MenuHeight = Conversions.ToInteger(((UI.Controllers.TrackBarX)sender).Value);
        }

        private void trackBarX14_ValueChanged(object sender, EventArgs e)
        {
            windowMetrics1.ScrollHeight = Conversions.ToInteger(((UI.Controllers.TrackBarX)sender).Value);
        }

        private void trackBarX15_ValueChanged(object sender, EventArgs e)
        {
            windowMetrics1.ScrollWidth = Conversions.ToInteger(((UI.Controllers.TrackBarX)sender).Value);
        }

        private void undo_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TM = Default.Get(Program.WindowStyle))
            {
                Label1.Font = TM.MetricsFonts.CaptionFont;
                Label1.Text = Label1.Font.Name;
                windowMetrics1.CaptionFont = TM.MetricsFonts.CaptionFont;
            }
        }

        private void button21_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TM = Default.Get(Program.WindowStyle))
            {
                Label5.Font = TM.MetricsFonts.SmCaptionFont;
                Label5.Text = Label5.Font.Name;
                windowMetrics1.SmCaptionFont = TM.MetricsFonts.SmCaptionFont;
            }
        }

        private void button22_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TM = Default.Get(Program.WindowStyle))
            {
                Label2.Font = TM.MetricsFonts.IconFont;
                Label2.Text = Label2.Font.Name;
                Desktop_icons.IconFont = TM.MetricsFonts.IconFont;
            }
        }

        private void button23_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TM = Default.Get(Program.WindowStyle))
            {
                Label3.Font = TM.MetricsFonts.MenuFont;
                Label3.Text = Label3.Font.Name;
                windowMetrics1.MenuFont = TM.MetricsFonts.MenuFont;
            }
        }

        private void button24_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TM = Default.Get(Program.WindowStyle))
            {
                Label4.Font = TM.MetricsFonts.MessageFont;
                Label4.Text = Label4.Font.Name;
                windowMetrics1.MessageFont = TM.MetricsFonts.MessageFont;
            }
        }

        private void button25_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TM = Default.Get(Program.WindowStyle))
            {
                Label6.Font = TM.MetricsFonts.StatusFont;
                Label6.Text = Label6.Font.Name;
                windowMetrics1.StatusFont = TM.MetricsFonts.StatusFont;
            }
        }

        private void button26_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TM = Default.Get(Program.WindowStyle))
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
    }
}