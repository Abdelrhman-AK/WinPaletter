using Devcorp.Controls.VisualStyles;
using libmsstyle;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
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
    public partial class Win81Colors : AspectsTemplate
    {

        public Win81Colors()
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

            using (Manager TMx = new(Manager.Source.Registry))
            {
                if (Program.Settings.BackupTheme.Enabled && Program.Settings.BackupTheme.AutoBackupOnApplySingleAspect)
                {
                    string filename = Program.GetUniqueFileName($"{Program.Settings.BackupTheme.BackupPath}\\OnAspectApply", $"{TMx.Info.ThemeName}_{DateTime.Now.Hour}.{DateTime.Now.Minute}.{DateTime.Now.Second}.wpth");
                    TMx.Save(Manager.Source.File, filename);
                }

                ApplyToTM(TMx);
                TMx.Windows81.Apply(TMx);
                ApplyToTM(Program.TM);
                ApplyToTM(Program.TM_Original);
            }

            Cursor = Cursors.Default;
        }

        private void Win7Colors_Load(object sender, EventArgs e)
        {
            DesignerData data = new(this)
            {
                AspectName = string.Format(Program.Lang.Strings.Aspects.WinTheme, OS.Name),
                Enabled = Program.TM.Windows81.Enabled,
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

            windowsDesktop1.BackgroundImage = Program.Wallpaper;

            img20.Image = Program.Wallpaper.Resize(40, 40);

            LoadData(data);

            LoadFromTM(Program.TM);
            ApplyDefaultTMValues();
        }

        public void LoadFromTM(Manager TM)
        {
            AspectEnabled = TM.Windows81.Enabled;
            ColorizationColor_pick.BackColor = TM.Windows81.ColorizationColor;
            ColorizationColorBalance_bar.Value = TM.Windows81.ColorizationColorBalance;

            start_pick.BackColor = TM.Windows81.StartColor;
            accent_pick.BackColor = TM.Windows81.AccentColor;
            personalcls_background_pick.BackColor = TM.Windows81.PersonalColors_Background;
            personalcolor_accent_pick.BackColor = TM.Windows81.PersonalColors_Accent;

            switch (TM.Windows81.Start)
            {
                case 1:
                    {
                        img1.Checked = true;
                        break;
                    }
                case 2:
                    {
                        img2.Checked = true;
                        break;
                    }
                case 3:
                    {
                        img3.Checked = true;
                        break;
                    }
                case 4:
                    {
                        img4.Checked = true;
                        break;
                    }
                case 5:
                    {
                        img5.Checked = true;
                        break;
                    }
                case 6:
                    {
                        img6.Checked = true;
                        break;
                    }
                case 7:
                    {
                        img7.Checked = true;
                        break;
                    }
                case 8:
                    {
                        img8.Checked = true;
                        break;
                    }
                case 9:
                    {
                        img9.Checked = true;
                        break;
                    }
                case 10:
                    {
                        img10.Checked = true;
                        break;
                    }
                case 11:
                    {
                        img11.Checked = true;
                        break;
                    }
                case 12:
                    {
                        img12.Checked = true;
                        break;
                    }
                case 13:
                    {
                        img13.Checked = true;
                        break;
                    }
                case 14:
                    {
                        img14.Checked = true;
                        break;
                    }
                case 15:
                    {
                        img15.Checked = true;
                        break;
                    }
                case 16:
                    {
                        img16.Checked = true;
                        break;
                    }
                case 17:
                    {
                        img17.Checked = true;
                        break;
                    }
                case 18:
                    {
                        img18.Checked = true;
                        break;
                    }
                case 19:
                    {
                        img19.Checked = true;
                        break;
                    }
                case 20:
                    {
                        img20.Checked = true;
                        break;
                    }

                default:
                    {
                        img1.Checked = true;
                        break;
                    }
            }

            toggle1.Checked = TM.Windows81.VisualStyles.Enabled;

            if (TM.Windows81.VisualStyles.VisualStylesType == Theme.Structures.VisualStyles.DefaultVisualStyles.AeroLite) theme_aerolite.Checked = true;
            else if (TM.Windows81.VisualStyles.VisualStylesType == Theme.Structures.VisualStyles.DefaultVisualStyles.Custom) theme_custom_check.Checked = true;
            else theme_aero.Checked = true;
            groupBox4.Visible = theme_custom_check.Checked;

            VS_textbox.Text = TM.Windows81.VisualStyles.ThemeFile;
            VS_ColorsList.SelectedItem = TM.Windows81.VisualStyles.ColorScheme;

            if (TM.Windows81.VisualStyles.SizeScheme.ToLower() == "normalsize" && VS_SizesList.Items.Contains("Normal")) TM.Windows81.VisualStyles.SizeScheme = "Normal";
            else if (TM.Windows81.VisualStyles.SizeScheme.ToLower() == "normalsize" && VS_SizesList.Items.Contains("normal")) TM.Windows81.VisualStyles.SizeScheme = "normal";

            VS_SizesList.SelectedItem = TM.Windows81.VisualStyles.SizeScheme;

            VS_ReplaceColors.Checked = TM.Windows81.VisualStyles.OverrideColors;
            VS_ReplaceMetrics.Checked = TM.Windows81.VisualStyles.OverrideSizes;

            windowsDesktop1.HookedTM = TM;
            windowsDesktop1.LoadFromTM(TM);

            RefreshDWM();
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

        public void ApplyToTM(Manager TM)
        {
            TM.Windows81.Enabled = AspectEnabled;
            TM.Windows81.ColorizationColor = ColorizationColor_pick.BackColor;
            TM.Windows81.ColorizationColorBalance = ColorizationColorBalance_bar.Value;

            TM.Windows81.StartColor = start_pick.BackColor;
            TM.Windows81.AccentColor = accent_pick.BackColor;
            TM.Windows81.PersonalColors_Background = personalcls_background_pick.BackColor;
            TM.Windows81.PersonalColors_Accent = personalcolor_accent_pick.BackColor;

            TM.Windows81.Start = img1.Checked ? 1 :
                                img2.Checked ? 2 :
                                img3.Checked ? 3 :
                                img4.Checked ? 4 :
                                img5.Checked ? 5 :
                                img6.Checked ? 6 :
                                img7.Checked ? 7 :
                                img8.Checked ? 8 :
                                img9.Checked ? 9 :
                                img10.Checked ? 10 :
                                img11.Checked ? 11 :
                                img12.Checked ? 12 :
                                img13.Checked ? 13 :
                                img14.Checked ? 14 :
                                img15.Checked ? 15 :
                                img16.Checked ? 16 :
                                img17.Checked ? 17 :
                                img18.Checked ? 18 :
                                img19.Checked ? 19 :
                                img20.Checked ? 20 : 1;

            TM.Windows81.VisualStyles.Enabled = toggle1.Checked;
            if (theme_aerolite.Checked) TM.Windows81.VisualStyles.VisualStylesType = Theme.Structures.VisualStyles.DefaultVisualStyles.AeroLite;
            else if (theme_custom_check.Checked) TM.Windows81.VisualStyles.VisualStylesType = Theme.Structures.VisualStyles.DefaultVisualStyles.Custom;
            else TM.Windows81.VisualStyles.VisualStylesType = Theme.Structures.VisualStyles.DefaultVisualStyles.Aero;

            TM.Windows81.VisualStyles.ThemeFile = VS_textbox.Text;
            TM.Windows81.VisualStyles.ColorScheme = (VS_ColorsList.SelectedItem ?? string.Empty).ToString();
            TM.Windows81.VisualStyles.SizeScheme = (VS_SizesList.SelectedItem ?? string.Empty).ToString().ToLower() == "normal" ? "NormalSize" : (VS_SizesList.SelectedItem ?? string.Empty).ToString();
            TM.Windows81.VisualStyles.OverrideColors = VS_ReplaceColors.Checked;
            TM.Windows81.VisualStyles.OverrideSizes = VS_ReplaceMetrics.Checked;

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

        public void ApplyDefaultTMValues()
        {
            using (Manager DefTM = Default.FromOS(WindowStyle.W81))
            {
                ColorizationColor_pick.DefaultBackColor = DefTM.Windows81.ColorizationColor;
                ColorizationColorBalance_bar.DefaultValue = DefTM.Windows81.ColorizationColorBalance;
                start_pick.DefaultBackColor = DefTM.Windows81.StartColor;
                accent_pick.DefaultBackColor = DefTM.Windows81.AccentColor;
                personalcls_background_pick.DefaultBackColor = DefTM.Windows81.PersonalColors_Background;
                personalcolor_accent_pick.DefaultBackColor = DefTM.Windows81.PersonalColors_Accent;
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
                RefreshDWM();
                windowsDesktop1.Win7ColorBal = ColorizationColorBalance_bar.Value;
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

        private void ColorizationColor_pick_BackColorChanged(object sender, EventArgs e)
        {
            if (IsShown) RefreshDWM();
        }

        private void Win7Colors_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.RefreshDWM(Program.TM);
        }

        private void RefreshDWM()
        {
            if (AspectEnabled && Program.Settings.Miscellaneous.Win7LivePreview)
            {
                Task.Run(() =>
                {
                    try
                    {
                        using (WindowsImpersonationContext wic = User.Identity.Impersonate())
                        {
                            if (DWMAPI.IsCompositionEnabled())
                            {
                                if (OS.W8x)
                                {
                                    DWMAPI.DWM_COLORIZATION_PARAMS temp = new()
                                    {
                                        clrColor = (uint)ColorizationColor_pick.BackColor.ToArgb(),
                                        nIntensity = (uint)ColorizationColorBalance_bar.Value,
                                    };

                                    DWMAPI.DwmSetColorizationParameters(ref temp, false);
                                }

                                else if (OS.W7)
                                {
                                    DWMAPI.DWM_COLORIZATION_PARAMS temp = new()
                                    {
                                        clrColor = (uint)ColorizationColor_pick.BackColor.ToArgb(),
                                        nIntensity = (uint)ColorizationColorBalance_bar.Value,
                                    };

                                    DWMAPI.DwmSetColorizationParameters(ref temp, false);
                                }
                            }
                            wic.Undo();
                        }
                    }
                    catch { }
                });
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

        //private void theme_aero_CheckedChanged_1(object sender, EventArgs e)
        //{
        //    if (IsShown && theme_aero.Checked)
        //    {
        //        RefreshDWM();
        //        windowsDesktop1.Windows_7_8_Theme = Theme.Structures.Windows7.Themes.Aero;
        //    }
        //}

        //private void theme_aerolite_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (IsShown && theme_aerolite.Checked)
        //    {
        //        RefreshDWM();
        //        windowsDesktop1.Windows_7_8_Theme = Theme.Structures.Windows7.Themes.AeroLite;
        //    }
        //}

        private void personalcolor_accent_pick_Click(object sender, EventArgs e)
        {
            ColorItem colorItem = (ColorItem)sender;
            Dictionary<Control, string[]> CList = new()
            {
                { colorItem, new string[] { nameof(colorItem.BackColor) }},
            };

            Color C = Forms.ColorPickerDlg.Pick(CList);

            colorItem.BackColor = C;
            colorItem.Invalidate();

            CList.Clear();
        }

        private void accent_pick_Click(object sender, EventArgs e)
        {
            ColorItem colorItem = (ColorItem)sender;
            Dictionary<Control, string[]> CList = new()
            {
                { colorItem, new string[] { nameof(colorItem.BackColor) }},
            };

            Color C = Forms.ColorPickerDlg.Pick(CList);

            colorItem.BackColor = C;
            colorItem.Invalidate();

            CList.Clear();
        }

        private void personalcls_background_pick_Click(object sender, EventArgs e)
        {
            ColorItem colorItem = (ColorItem)sender;
            Dictionary<Control, string[]> CList = new()
            {
                { colorItem, new string[] { nameof(colorItem.BackColor) }},
            };

            Color C = Forms.ColorPickerDlg.Pick(CList);

            colorItem.BackColor = C;
            colorItem.Invalidate();

            CList.Clear();
        }

        private void start_pick_Click(object sender, EventArgs e)
        {
            ColorItem colorItem = (ColorItem)sender;
            Dictionary<Control, string[]> CList = new()
            {
                { colorItem, new string[] { nameof(colorItem.BackColor) }},
            };

            Color C = Forms.ColorPickerDlg.Pick(CList);

            colorItem.BackColor = C;
            colorItem.Invalidate();

            CList.Clear();
        }

        private void Win81Colors_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            Process.Start(Links.Wiki.Win81Colors);
        }

        private void personalcls_background_pick_BackColorChanged(object sender, EventArgs e)
        {
            img19.Image?.Dispose();
            img19.Image = (sender as ColorItem).BackColor.ToBitmap(new Size(40, 40));
        }

        private void VS_Browse_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new() { FileName = VS_textbox.Text, Filter = Program.Filters.VisualStyles_And_Themes, Title = Program.Lang.Strings.Extensions.OpenVisualStyle })
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

        private void theme_aero_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as RadioImage).Checked) windowsDesktop1.VisualStyles = Theme.Structures.VisualStyles.DefaultVisualStyles.Aero;
        }

        private void theme_aerolite_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as RadioImage).Checked) windowsDesktop1.VisualStyles = Theme.Structures.VisualStyles.DefaultVisualStyles.AeroLite;
        }

        private void theme_custom_check_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as RadioImage).Checked)
            {
                windowsDesktop1.VisualStyles = Theme.Structures.VisualStyles.DefaultVisualStyles.Custom;

                groupBox4.Visible = (sender as RadioImage).Checked;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Process.Start(Links.SecureUxThemeReleases);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Process.Start(Links.SecureUxThemeLoginLoopIssue);
        }

        private void toggle1_CheckedChanged(object sender, EventArgs e)
        {
            groupBox22.Enabled = (sender as Toggle).Checked;
        }

        private void ColorizationColor_pick_ContextMenuMadeColorChangeInvoker(object sender, ColorItem.ContextMenuMadeColorChangeEventArgs e)
        {
            windowsDesktop1.TitlebarColor_Active = e.Color;
            windowsDesktop1.TitlebarColor_Inactive = e.Color;
        }
    }
}