using Devcorp.Controls.VisualStyles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using WinPaletter.NativeMethods;
using WinPaletter.Theme;
using static WinPaletter.PreviewHelpers;
using static WinPaletter.Theme.Manager;

namespace WinPaletter
{
    public partial class VisualStyles
    {
        private void Form_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            //Process.Start(Links.Wiki.AltTab);
        }

        public VisualStyles()
        {
            InitializeComponent();
        }

        private string GetOSName(WindowStyle windowStyle)
        {
            return windowStyle switch
            {
                PreviewHelpers.WindowStyle.W12 => Program.Lang.Strings.Windows.W12,
                PreviewHelpers.WindowStyle.W11 => Program.Lang.Strings.Windows.W11,
                PreviewHelpers.WindowStyle.W10 => Program.Lang.Strings.Windows.W10,
                PreviewHelpers.WindowStyle.W81 => Program.Lang.Strings.Windows.W81,
                PreviewHelpers.WindowStyle.W7 => Program.Lang.Strings.Windows.W7,
                PreviewHelpers.WindowStyle.WVista => Program.Lang.Strings.Windows.WVista,
                PreviewHelpers.WindowStyle.WXP => Program.Lang.Strings.Windows.WXP,
                _ => Program.Lang.Strings.Windows.Undefined,
            };
        }

        /// <summary>
        /// Visual Styles instance to be modified
        /// </summary>
        public Theme.Structures.VisualStyles visualStyles = new();

        private void LoadFromWPTH(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new() { Filter = Program.Filters.WinPaletterTheme, Title = Program.Lang.Strings.Extensions.OpenWinPaletterTheme })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    using (Manager TMx = new(Theme.Manager.Source.File, dlg.FileName))
                    {
                        switch (Program.WindowStyle)
                        {
                            case WindowStyle.W12:
                                LoadFromTM(TMx.VisualStyles_12); break;
                            case WindowStyle.W11:
                                LoadFromTM(TMx.VisualStyles_11); break;
                            case WindowStyle.W10:
                                LoadFromTM(TMx.VisualStyles_10); break;
                            case WindowStyle.W81:
                                LoadFromTM(TMx.VisualStyles_81); break;
                            case WindowStyle.W7:
                                LoadFromTM(TMx.VisualStyles_7); break;
                            case WindowStyle.WVista:
                                LoadFromTM(TMx.VisualStyles_Vista); break;
                            case WindowStyle.WXP:
                                LoadFromTM(TMx.VisualStyles_XP); break;
                        }
                    }
                }
            }
        }

        private void LoadFromCurrent(object sender, EventArgs e)
        {
            using (Manager TMx = new(Theme.Manager.Source.Registry))
            {
                switch (Program.WindowStyle)
                {
                    case WindowStyle.W12:
                        visualStyles = TMx.VisualStyles_12; break;
                    case WindowStyle.W11:
                        visualStyles = TMx.VisualStyles_11; break;
                    case WindowStyle.W10:
                        visualStyles = TMx.VisualStyles_10; break;
                    case WindowStyle.W81:
                        visualStyles = TMx.VisualStyles_81; break;
                    case WindowStyle.W7:
                        visualStyles = TMx.VisualStyles_7; break;
                    case WindowStyle.WVista:
                        visualStyles = TMx.VisualStyles_Vista; break;
                    case WindowStyle.WXP:
                        visualStyles = TMx.VisualStyles_XP; break;
                }

                LoadFromTM(visualStyles);
            }
        }

        private void LoadFromDefault(object sender, EventArgs e)
        {
            using (Manager TMx = Theme.Default.Get(Program.WindowStyle))
            {
                switch (Program.WindowStyle)
                {
                    case WindowStyle.W12:
                        visualStyles = TMx.VisualStyles_12; break;
                    case WindowStyle.W11:
                        visualStyles = TMx.VisualStyles_11; break;
                    case WindowStyle.W10:
                        visualStyles = TMx.VisualStyles_10; break;
                    case WindowStyle.W81:
                        visualStyles = TMx.VisualStyles_81; break;
                    case WindowStyle.W7:
                        visualStyles = TMx.VisualStyles_7; break;
                    case WindowStyle.WVista:
                        visualStyles = TMx.VisualStyles_Vista; break;
                    case WindowStyle.WXP:
                        visualStyles = TMx.VisualStyles_XP; break;
                }

                LoadFromTM(visualStyles);
            }
        }

        private void LoadIntoCurrentTheme(object sender, EventArgs e)
        {
            switch (Program.WindowStyle)
            {
                case WindowStyle.W12:
                    ApplyToTM(ref Program.TM.VisualStyles_12); break;
                case WindowStyle.W11:
                    ApplyToTM(ref Program.TM.VisualStyles_11); break;
                case WindowStyle.W10:
                    ApplyToTM(ref Program.TM.VisualStyles_10); break;
                case WindowStyle.W81:
                    ApplyToTM(ref Program.TM.VisualStyles_81); break;
                case WindowStyle.W7:
                    ApplyToTM(ref Program.TM.VisualStyles_7); break;
                case WindowStyle.WVista:
                    ApplyToTM(ref Program.TM.VisualStyles_Vista); break;
                case WindowStyle.WXP:
                    ApplyToTM(ref Program.TM.VisualStyles_XP); break;
            }

            Close();
        }

        private void Apply(object sender, EventArgs e)
        {
            if (Program.Settings.AspectsControl.Enabled && !Program.Settings.AspectsControl.VisualStyles)
            {
                MsgBox(Program.Lang.Strings.Aspects.Disabled_Apply_0, MessageBoxButtons.OK, MessageBoxIcon.Warning, Program.Lang.Strings.Aspects.Disabled_Apply_1);
                return;
            }

            Cursor = Cursors.WaitCursor;

            using (Manager TMx = new(Theme.Manager.Source.Registry))
            {
                if (Program.Settings.BackupTheme.Enabled && Program.Settings.BackupTheme.AutoBackupOnApplySingleAspect)
                {
                    string filename = Program.GetUniqueFileName($"{Program.Settings.BackupTheme.BackupPath}\\OnAspectApply", $"{TMx.Info.ThemeName}_{DateTime.Now.Hour}.{DateTime.Now.Minute}.{DateTime.Now.Second}.wpth");
                    TMx.Save(Source.File, filename);
                }

                ApplyToTM(ref visualStyles);

                switch (Program.WindowStyle)
                {
                    case WindowStyle.W12:
                        ApplyToTM(ref Program.TM.VisualStyles_12);
                        ApplyToTM(ref Program.TM_Original.VisualStyles_12);
                        break;
                    case WindowStyle.W11:
                        ApplyToTM(ref Program.TM.VisualStyles_11);
                        ApplyToTM(ref Program.TM_Original.VisualStyles_11);
                        break;
                    case WindowStyle.W10:
                        ApplyToTM(ref Program.TM.VisualStyles_10);
                        ApplyToTM(ref Program.TM_Original.VisualStyles_10);
                        break;
                    case WindowStyle.W81:
                        ApplyToTM(ref Program.TM.VisualStyles_81);
                        ApplyToTM(ref Program.TM_Original.VisualStyles_81);
                        break;
                    case WindowStyle.W7:
                        ApplyToTM(ref Program.TM.VisualStyles_7);
                        ApplyToTM(ref Program.TM_Original.VisualStyles_7);
                        break;
                    case WindowStyle.WVista:
                        ApplyToTM(ref Program.TM.VisualStyles_Vista);
                        ApplyToTM(ref Program.TM_Original.VisualStyles_Vista);
                        break;
                    case WindowStyle.WXP:
                        ApplyToTM(ref Program.TM.VisualStyles_XP);
                        ApplyToTM(ref Program.TM_Original.VisualStyles_XP);
                        break;
                }

                if (OS.W12) visualStyles.Apply("12");
                else if (OS.W11) visualStyles.Apply("11");
                else if (OS.W10) visualStyles.Apply("10");
                else if (OS.W81) visualStyles.Apply("8.1");
                else if (OS.W7) visualStyles.Apply("7");
                else if (OS.WVista) visualStyles.Apply("Vista");
                else if (OS.WXP) visualStyles.Apply("XP");

                if (visualStyles.Enabled)
                {
                    if (visualStyles.OverrideColors) Program.TM.Win32.Apply();
                    if (visualStyles.OverrideSizes) Program.TM.MetricsFonts.Apply();
                }
            }

            Cursor = Cursors.Default;
        }

        private void VisualStyles_Load(object sender, EventArgs e)
        {
            DesignerData data = new(this)
            {
                AspectName = Program.Lang.Strings.Aspects.VisualStyles,
                Enabled = visualStyles.Enabled,
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

            LoadData(data);

            button28.Visible = !OS.WXP && !OS.WVista && !OS.W7 && !OS.W8;
            button29.Visible = button28.Visible;

            alertBox2.Text = string.Format(Program.Lang.Strings.Tips.WallpaperTone_Notice, GetOSName(Program.WindowStyle));

            switch (Program.WindowStyle)
            {
                case WindowStyle.W12:
                    visualStyles = Program.TM.VisualStyles_12; break;
                case WindowStyle.W11:
                    visualStyles = Program.TM.VisualStyles_11; break;
                case WindowStyle.W10:
                    visualStyles = Program.TM.VisualStyles_10; break;
                case WindowStyle.W81:
                    visualStyles = Program.TM.VisualStyles_81; break;
                case WindowStyle.W7:
                    visualStyles = Program.TM.VisualStyles_7; break;
                case WindowStyle.WVista:
                    visualStyles = Program.TM.VisualStyles_Vista; break;
                case WindowStyle.WXP:
                    visualStyles = Program.TM.VisualStyles_XP; break;
            }

            LoadFromTM(visualStyles);
        }

        public void LoadFromTM(Theme.Structures.VisualStyles vs)
        {
            AspectEnabled = vs.Enabled;
            VS_textbox.Text = vs.ThemeFile;
            VS_ColorsList.SelectedItem = vs.ColorScheme;

            if (vs.SizeScheme.ToLower() == "normalsize" && VS_SizesList.Items.Contains("Normal")) vs.SizeScheme = "Normal";
            else if (vs.SizeScheme.ToLower() == "normalsize" && VS_SizesList.Items.Contains("normal")) vs.SizeScheme = "normal";

            VS_SizesList.SelectedItem = vs.SizeScheme;

            VS_ReplaceColors.Checked = vs.OverrideColors;
            VS_ReplaceMetrics.Checked = vs.OverrideSizes;
        }

        public void ApplyToTM(ref Theme.Structures.VisualStyles vs)
        {
            vs.Enabled = AspectEnabled;
            vs.ThemeFile = VS_textbox.Text;
            vs.ColorScheme = VS_ColorsList.SelectedItem.ToString();
            vs.SizeScheme = VS_SizesList.SelectedItem.ToString().ToLower() == "normal" ? "NormalSize" : VS_SizesList.SelectedItem.ToString();
            vs.OverrideColors = VS_ReplaceColors.Checked;
            vs.OverrideSizes = VS_ReplaceMetrics.Checked;
        }

        private void VS_Browse_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new() { Filter = Program.Filters.VisualStyles_And_Themes, Title = Program.Lang.Strings.Extensions.OpenVisualStyle })
            {
                if (dlg.ShowDialog() == DialogResult.OK) VS_textbox.Text = dlg.FileName;
            }
        }

        private void VS_textbox_TextChanged(object sender, EventArgs e)
        {
            VS_ColorsList.Items.Clear();
            VS_SizesList.Items.Clear();

            string currentVS = UxTheme.GetCurrentVS().Item1;

            if (System.IO.File.Exists(VS_textbox.Text))
            {
                KeyValuePair<bool, string> result = isCorrectVSPlatform(VS_textbox.Text);

                if (!result.Key)
                {
                    if (!string.IsNullOrWhiteSpace(result.Value))
                    {
                        alertBox1.Visible = true;
                        alertBox1.Text = string.Format(Program.Lang.Strings.Tips.VisualStyles_WrongPlatform, result.Value, GetOSName(Program.WindowStyle));
                    }
                }
                else
                {
                    alertBox1.Visible = false;
                }

                setVS(VS_textbox.Text);
            }
        }

        KeyValuePair<bool, string> isCorrectVSPlatform(string theme)
        {
            try
            {
                using (libmsstyle.VisualStyle vs = new(theme))
                {
                    // Let's assume that W12 is identical to Win11 until official release !
                    if (vs.Platform == libmsstyle.Platform.Win11 && Program.WindowStyle != WindowStyle.W11 && Program.WindowStyle != WindowStyle.W12)
                    {
                        return new(false, Program.Lang.Strings.Windows.W11);
                    }
                    else if (vs.Platform == libmsstyle.Platform.Win10 && Program.WindowStyle != WindowStyle.W10)
                    {
                        return new(false, Program.Lang.Strings.Windows.W10);
                    }
                    else if (vs.Platform == libmsstyle.Platform.Win81 && Program.WindowStyle != WindowStyle.W81)
                    {
                        return new(false, Program.Lang.Strings.Windows.W81);
                    }
                    else if (vs.Platform == libmsstyle.Platform.Win7 && Program.WindowStyle != WindowStyle.W7)
                    {
                        return new(false, Program.Lang.Strings.Windows.W7);
                    }
                    else if (vs.Platform == libmsstyle.Platform.Vista && Program.WindowStyle != WindowStyle.WVista)
                    {
                        return new(false, Program.Lang.Strings.Windows.WVista);
                    }
                    else
                    {
                        return new(true, string.Empty);
                    }
                }
            }
            catch // Couldn't load visual styles File by libmsstyles, so we will assume that it is a Windows WXP theme
            {
                try
                {
                    if (System.IO.Path.GetExtension(theme).ToLower() == ".msstyles")
                    {
                        System.IO.File.WriteAllText($@"{SysPaths.appData}\VisualStyles\Luna\win32uischeme.theme", $"[VisualStyles]{"\r\n"}Path={theme}{"\r\n"}ColorStyle=NormalColor{"\r\n"}Size=NormalSize");
                        theme = $@"{SysPaths.appData}\VisualStyles\Luna\win32uischeme.theme";
                    }

                    if (System.IO.File.Exists(theme))
                    {
                        using (VisualStyleFile vs = new(theme))
                        {
                            if (Program.WindowStyle != WindowStyle.WXP)
                            {
                                return new(false, Program.Lang.Strings.Windows.WXP);
                            }
                            else
                            {
                                return new(true, string.Empty);
                            }
                        }
                    }
                    else
                    {
                        return new(false, string.Empty);
                    }
                }
                catch // Couldn't load visual styles by any method.
                {
                    return new(false, string.Empty);
                }
            }
        }

        void setVS(string theme)
        {
            VS_ColorsList.Items.Clear();
            VS_SizesList.Items.Clear();

            if (System.IO.File.Exists(theme))
            {
                try
                {
                    using (libmsstyle.VisualStyle vs = new(theme))
                    {
                        foreach (libmsstyle.StyleClass @class in vs.Classes.Values)
                        {
                            if (@class.ClassName.StartsWith("colorvariant.", StringComparison.OrdinalIgnoreCase))
                            {
                                VS_ColorsList.Items.Add(@class.ClassName.Remove(0, "colorvariant.".Count()));
                                if (VS_ColorsList.Items.Count > 0) VS_ColorsList.SelectedIndex = 0; else VS_ColorsList.SelectedIndex = -1;
                            }
                            else if (@class.ClassName.StartsWith("sizevariant.", StringComparison.OrdinalIgnoreCase) && @class.ClassName.ToLower() != "sizevariant.default")
                            {
                                VS_SizesList.Items.Add(@class.ClassName.Remove(0, "sizevariant.".Count()));
                                if (VS_SizesList.Items.Count > 0) VS_SizesList.SelectedIndex = 0; else VS_SizesList.SelectedIndex = -1;
                            }
                        }
                    }
                }
                catch
                {
                    if (System.IO.Path.GetExtension(theme).ToLower() == ".msstyles")
                    {
                        System.IO.File.WriteAllText($@"{SysPaths.appData}\VisualStyles\Luna\win32uischeme.theme", $"[VisualStyles]{"\r\n"}Path={theme}{"\r\n"}ColorStyle=NormalColor{"\r\n"}Size=NormalSize");
                        theme = $@"{SysPaths.appData}\VisualStyles\Luna\win32uischeme.theme";
                    }

                    if (System.IO.File.Exists(theme))
                    {
                        using (VisualStyleFile vs = new(theme))
                        {
                            try
                            {
                                foreach (VisualStyleScheme x in vs.ColorSchemes) VS_ColorsList.Items.Add(x.Name);
                                foreach (VisualStyleSize x in vs.Sizes) VS_SizesList.Items.Add(x.DisplayName);
                            }
                            catch { } // Couldn't load visual styles File, so no scheme will be added

                            if (VS_ColorsList.Items.Count > 0) VS_ColorsList.SelectedIndex = 0; else VS_ColorsList.SelectedIndex = -1;
                            if (VS_SizesList.Items.Count > 0) VS_SizesList.SelectedIndex = 0; else VS_SizesList.SelectedIndex = -1;
                        }
                    }
                }
            }
        }

        private void button29_Click(object sender, EventArgs e)
        {
            Process.Start(Links.SecureUxThemeReleases);
        }

        private void button28_Click(object sender, EventArgs e)
        {
            Forms.SecureUxTheme_Setup.ShowDialog();
        }

        private void button21_Click(object sender, EventArgs e)
        {
            using (Manager TMx = Default.Get())
            {
                switch (Program.WindowStyle)
                {
                    case WindowStyle.W12:
                        VS_textbox.Text = TMx.VisualStyles_12.ThemeFile;
                        break;
                    case WindowStyle.W11:
                        VS_textbox.Text = TMx.VisualStyles_11.ThemeFile;
                        break;
                    case WindowStyle.W10:
                        VS_textbox.Text = TMx.VisualStyles_10.ThemeFile;
                        break;
                    case WindowStyle.W81:
                        VS_textbox.Text = TMx.VisualStyles_81.ThemeFile;
                        break;
                    case WindowStyle.W7:
                        VS_textbox.Text = TMx.VisualStyles_7.ThemeFile;
                        break;
                    case WindowStyle.WVista:
                        VS_textbox.Text = TMx.VisualStyles_Vista.ThemeFile;
                        break;
                    case WindowStyle.WXP:
                        VS_textbox.Text = TMx.VisualStyles_XP.ThemeFile;
                        break;
                }
            }
        }
    }
}