using Devcorp.Controls.VisualStyles;
using libmsstyle;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using WinPaletter.NativeMethods;
using WinPaletter.Theme;
using WinPaletter.UI.Controllers;
using WinPaletter.UI.Retro;
using WinPaletter.UI.WP;
using static WinPaletter.PreviewHelpers;
using static WinPaletter.TypesExtensions.ColorsExtensions;

namespace WinPaletter
{
    public partial class WinVistaColors : AspectsTemplate
    {

        public WinVistaColors()
        {
            InitializeComponent();
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
            Program.TM.WindowsVista.Apply();

            ApplyToTM(Program.TM_Original);

            Cursor = Cursors.Default;
        }

        private void WinVistaColors_Load(object sender, EventArgs e)
        {
            DesignerData data = new(this)
            {
                AspectName = string.Format(Program.Localization.Strings.Aspects.WinTheme, OS.Name),
                Enabled = Program.TM.WindowsVista.Enabled,
                GeneratePalette = true,
                GenerateMSTheme = false,
                Import_preset = false,
                CanSwitchMode = false,

                OnLoadIntoCurrentTheme = LoadIntoCurrentTheme,
                OnApply = Apply,
                OnImportFromDefault = LoadFromDefault,
                OnImportFromWPTH = LoadFromWPTH,
                OnImportFromCurrentApplied = LoadFromCurrent
            };

            LoadData(data);

            LoadFromTM(Program.TM);
            ApplyDefaultTMValues();
        }

        /// <summary>
        /// Updates the preview of the desktop window with the current color settings.
        /// </summary>
        /// <remarks>This method applies the current color values from the associated UI elements to the
        /// preview of the desktop window. It updates the active and inactive title bar colors, as well as a series of
        /// additional color properties.</remarks>
        public void UpdatePreview()
        {
            windowsDesktop1.TitlebarColor_Active = ColorizationColor_pick.BackColor;
            windowsDesktop1.TitlebarColor_Inactive = ColorizationColor_pick.BackColor;
        }

        public void LoadFromTM(Manager TM)
        {
            AspectEnabled = TM.WindowsVista.Enabled;
            ColorizationColor_pick.BackColor = TM.WindowsVista.ColorizationColor;
            ColorizationColorBalance_bar.Value = TM.WindowsVista.Alpha;

            toggle1.Checked = TM.WindowsVista.VisualStyles.Enabled;

            if (TM.WindowsVista.VisualStyles.VisualStylesType == Theme.Structures.VisualStyles.DefaultVisualStyles.Custom) theme_custom_check.Checked = true;
            else if (TM.WindowsVista.VisualStyles.VisualStylesType == Theme.Structures.VisualStyles.DefaultVisualStyles.Classic) theme_classic.Checked = true;
            else if (TM.WindowsVista.VisualStyles.VisualStylesType == Theme.Structures.VisualStyles.DefaultVisualStyles.Basic) theme_basic.Checked = true;
            else if (TM.WindowsVista.VisualStyles.VisualStylesType == Theme.Structures.VisualStyles.DefaultVisualStyles.AeroOpaque) theme_aeroopaque.Checked = true;
            else theme_aero.Checked = true;

            groupBox4.Visible = theme_custom_check.Checked;

            VS_textbox.Text = TM.WindowsVista.VisualStyles.ThemeFile;
            VS_ColorsList.SelectedItem = TM.WindowsVista.VisualStyles.ColorScheme;

            if (TM.WindowsVista.VisualStyles.SizeScheme.ToLower() == "normalsize" && VS_SizesList.Items.Contains("Normal")) TM.WindowsVista.VisualStyles.SizeScheme = "Normal";
            else if (TM.WindowsVista.VisualStyles.SizeScheme.ToLower() == "normalsize" && VS_SizesList.Items.Contains("normal")) TM.WindowsVista.VisualStyles.SizeScheme = "normal";

            VS_SizesList.SelectedItem = TM.WindowsVista.VisualStyles.SizeScheme;

            VS_ReplaceColors.Checked = TM.WindowsVista.VisualStyles.OverrideColors;
            VS_ReplaceMetrics.Checked = TM.WindowsVista.VisualStyles.OverrideSizes;

            windowsDesktop1.HookedTM = TM;
            windowsDesktop1.LoadFromTM(TM);
        }

        public void ApplyToTM(Manager TM)
        {
            TM.WindowsVista.Enabled = AspectEnabled;
            TM.WindowsVista.ColorizationColor = ColorizationColor_pick.BackColor;
            TM.WindowsVista.Alpha = (byte)ColorizationColorBalance_bar.Value;

            TM.WindowsVista.VisualStyles.Enabled = toggle1.Checked;

            if (theme_custom_check.Checked) TM.WindowsVista.VisualStyles.VisualStylesType = Theme.Structures.VisualStyles.DefaultVisualStyles.Custom;
            else if (theme_classic.Checked) TM.WindowsVista.VisualStyles.VisualStylesType = Theme.Structures.VisualStyles.DefaultVisualStyles.Classic;
            else if (theme_basic.Checked) TM.WindowsVista.VisualStyles.VisualStylesType = Theme.Structures.VisualStyles.DefaultVisualStyles.Basic;
            else if (theme_aeroopaque.Checked) TM.WindowsVista.VisualStyles.VisualStylesType = Theme.Structures.VisualStyles.DefaultVisualStyles.AeroOpaque;
            else TM.WindowsVista.VisualStyles.VisualStylesType = Theme.Structures.VisualStyles.DefaultVisualStyles.Aero;

            TM.WindowsVista.VisualStyles.ThemeFile = VS_textbox.Text;
            TM.WindowsVista.VisualStyles.ColorScheme = (VS_ColorsList.SelectedItem ?? string.Empty).ToString();
            TM.WindowsVista.VisualStyles.SizeScheme = (VS_SizesList.SelectedItem ?? string.Empty).ToString().ToLower() == "normal" ? "NormalSize" : (VS_SizesList.SelectedItem ?? string.Empty).ToString();
            TM.WindowsVista.VisualStyles.OverrideColors = VS_ReplaceColors.Checked;
            TM.WindowsVista.VisualStyles.OverrideSizes = VS_ReplaceMetrics.Checked;

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
                        MsgBox(Program.Localization.Strings.Messages.InvalidTheme, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        MsgBox(Program.Localization.Strings.Messages.InvalidTheme, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        public void ApplyDefaultTMValues()
        {
            using (Manager DefTM = Default.FromOS(WindowStyle.W7))
            {
                ColorizationColor_pick.DefaultBackColor = DefTM.WindowsVista.ColorizationColor;
                ColorizationColorBalance_bar.DefaultValue = DefTM.WindowsVista.Alpha;
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dlg = new() { Filter = Program.Filters.PNG, Title = Program.Localization.Strings.Extensions.SavePNG })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    using (Bitmap bmp_thumbnail = new(windowsDesktop1.ToBitmap())) { bmp_thumbnail?.Save(dlg.FileName); }
                }
            }
        }

        private void ColorizationColor_pick_Click(object sender, EventArgs e)
        {
            ColorItem colorItem = (ColorItem)sender;
            Dictionary<Control, string[]> CList = new()
            {
                { colorItem, new string[] { nameof(colorItem.BackColor) } },
                { windowsDesktop1, new string[] { nameof(windowsDesktop1.TitlebarColor_Active), nameof(windowsDesktop1.TitlebarColor_Inactive) } }
            };

            Color C = Forms.ColorPickerDlg.Pick(CList);

            colorItem.BackColor = C;
            colorItem.Invalidate();

            CList.Clear();
        }

        private void ColorizationColorBalance_bar_ValueChanged(object sender, EventArgs e)
        {
            if (IsShown)
            {
                windowsDesktop1.Win7ColorBal = (int)(ColorizationColorBalance_bar.Value / 255f * 100f);
                windowsDesktop1.Win7Alpha = 100 - (int)(ColorizationColorBalance_bar.Value / 255f * 100f);
            }
        }

        private void windowsDesktop1_EditorInvoker(object sender, EditorEventArgs e)
        {
            if (e.PropertyName.ToLower() == nameof(windowsDesktop1.TitlebarColor_Active).ToLower())
            {
                Dictionary<Control, string[]> CList = new()
                {
                    { windowsDesktop1, new string[] { nameof(windowsDesktop1.TitlebarColor_Active), nameof(windowsDesktop1.TitlebarColor_Inactive) } },
                    { ColorizationColor_pick, new string[] { nameof(ColorizationColor_pick.BackColor) } }
                };

                Color C = Forms.ColorPickerDlg.Pick(CList);

                windowsDesktop1.SetProperty(e.PropertyName ?? "BackColor", C);
                ((Control)sender).SetProperty(e.PropertyName ?? "BackColor", C);

                CList.ElementAt(CList.Count - 1).Key.SetProperty(CList.ElementAt(CList.Count - 1).Value.FirstOrDefault(), C);

                CList.Clear();
            }
        }

        private void Pickers_DragDrop(object sender, DragEventArgs e)
        {
            if (((ColorItem)sender).AllowDrop)
            {
                windowsDesktop1.TitlebarColor_Active = ColorizationColor_pick.BackColor;
                windowsDesktop1.TitlebarColor_Inactive = ColorizationColor_pick.BackColor;
            }
        }

        private void WinVistaColors_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            Process.Start(Links.Wiki.WinVistaColors);
        }

        private void theme_custom_check_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as RadioImage).Checked)
            {
                windowsDesktop1.VisualStyles = Theme.Structures.VisualStyles.DefaultVisualStyles.Custom;
            }

            groupBox4.Visible = (sender as RadioImage).Checked;
        }

        private void theme_aeroopaque_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as RadioImage).Checked)
            {
                windowsDesktop1.VisualStyles = Theme.Structures.VisualStyles.DefaultVisualStyles.AeroOpaque;
            }
        }

        private void theme_aero_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as RadioImage).Checked)
            {
                windowsDesktop1.VisualStyles = Theme.Structures.VisualStyles.DefaultVisualStyles.Aero;
            }
        }

        private void theme_basic_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as RadioImage).Checked)
            {
                windowsDesktop1.VisualStyles = Theme.Structures.VisualStyles.DefaultVisualStyles.Basic;
            }
        }

        private void theme_classic_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as RadioImage).Checked)
            {
                windowsDesktop1.VisualStyles = Theme.Structures.VisualStyles.DefaultVisualStyles.Classic;
            }
        }

        private void VS_Browse_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new() { FileName = VS_textbox.Text, Filter = Program.Filters.VisualStyles_And_Themes, Title = Program.Localization.Strings.Extensions.OpenVisualStyle })
            {
                if (dlg.ShowDialog() == DialogResult.OK) VS_textbox.Text = VisualStyle.GetCorrectMSStyles(dlg.FileName);
            }
        }

        private void button21_Click(object sender, EventArgs e)
        {
            VS_textbox.Text = SysPaths.MSSTYLES_Aero_Win;
        }

        private void VS_textbox_TextChanged(object sender, EventArgs e)
        {
            setVS(VS_textbox.Text);
        }

        void setVS(string theme)
        {
            VS_ColorsList.Items.Clear();
            VS_SizesList.Items.Clear();

            if (!File.Exists(theme)) theme = UxTheme.GetCurrentVS().Item1;

            try
            {
                using (VisualStyle vs = new(theme))
                {
                    foreach (StyleClass @class in vs.Classes.Values)
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
                if (Path.GetExtension(theme).ToLower() == ".msstyles")
                {
                    File.WriteAllText($@"{SysPaths.appData}\VisualStyles\Luna\win32uischeme.theme", $"[VisualStyles]{"\r\n"}Path={theme}{"\r\n"}ColorStyle=NormalColor{"\r\n"}Size=NormalSize");
                    theme = $@"{SysPaths.appData}\VisualStyles\Luna\win32uischeme.theme";
                }

                if (File.Exists(theme))
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

        private void button2_Click(object sender, EventArgs e)
        {
            Process.Start(Links.UxTheme_multi_patcher);
        }

        private void toggle1_CheckedChanged(object sender, EventArgs e)
        {
            groupBox5.Enabled = (sender as Toggle).Checked;
        }

        private void ColorizationColor_pick_ContextMenuMadeColorChangeInvoker(object sender, ColorItem.ContextMenuMadeColorChangeEventArgs e)
        {
            windowsDesktop1.TitlebarColor_Active = e.Color;
            windowsDesktop1.TitlebarColor_Inactive = e.Color;
        }
    }
}