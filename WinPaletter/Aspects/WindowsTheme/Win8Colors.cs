using Devcorp.Controls.VisualStyles;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinPaletter.NativeMethods;
using WinPaletter.Templates;
using WinPaletter.Theme;
using WinPaletter.UI.Controllers;
using WinPaletter.UI.WP;
using static WinPaletter.PreviewHelpers;
using static WinPaletter.TypesExtensions.ColorsExtensions;

namespace WinPaletter
{
    public partial class Win8Colors : AspectsTemplate
    {
        bool canChangeColor = true;

        public Win8Colors()
        {
            InitializeComponent();
        }

        private void LoadFromWPTH(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new() { Filter = Program.Filters.WinPaletterTheme, Title = Program.Lang.Strings.Extensions.OpenWinPaletterTheme })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    using (Manager TMx = new(Theme.Manager.Source.File, dlg.FileName))
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
            if (Program.Settings.AspectsControl.Enabled && !Program.Settings.AspectsControl.WinColors)
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
                    TMx.Save(Theme.Manager.Source.File, filename);
                }

                ApplyToTM(TMx);
                TMx.Windows8.Apply(TMx);
                ApplyToTM(Program.TM);
                ApplyToTM(Program.TM_Original);
            }

            Cursor = Cursors.Default;
        }

        private void GeneratePalette_Image(object sender, EventArgs e)
        {
            Forms.PaletteGenerateFromImage.ShowDialog();
        }

        private void GeneratePalette_Color(object sender, EventArgs e)
        {
            Forms.PaletteGenerateFromColor.ShowDialog();
        }

        private void Win8Colors_Load(object sender, EventArgs e)
        {
            DesignerData data = new(this)
            {
                AspectName = string.Format(Program.Lang.Strings.Aspects.WinTheme, OS.Name),
                Enabled = Program.TM.Windows8.Enabled,
                GeneratePalette = true,
                GenerateMSTheme = false,
                Import_preset = false,
                CanSwitchMode = false,

                OnLoadIntoCurrentTheme = LoadIntoCurrentTheme,
                OnApply = Apply,
                OnImportFromDefault = LoadFromDefault,
                OnImportFromWPTH = LoadFromWPTH,
                OnImportFromCurrentApplied = LoadFromCurrent,
                OnGeneratePaletteFromImage = GeneratePalette_Image,
                OnGeneratePaletteFromColor = GeneratePalette_Color,
            };

            windowsDesktop1.BackgroundImage = Program.Wallpaper;

            LoadData(data);

            LoadFromTM(Program.TM);
            ApplyDefaultTMValues();

            using (Bitmap back0 = Color.FromArgb(37, 37, 37).ToBitmap(new Size(32, 32)))
            using (Bitmap accent0 = Color.FromArgb(244, 179, 0).ToBitmap(new Size(16, 16)))
            using (Graphics g = Graphics.FromImage(back0))
            {
                Rectangle center = new((back0.Width - accent0.Width) / 2, (back0.Height - accent0.Height) / 2, accent0.Width, accent0.Height);
                g.DrawImage(accent0, new Rectangle(8, 8, 16, 16));

            }

            radioImage1.Image = AccentTemplate(Color.FromArgb(37, 37, 37), Color.FromArgb(244, 179, 0));
            radioImage2.Image = AccentTemplate(Color.FromArgb(37, 37, 37), Color.FromArgb(120, 186, 0));
            radioImage3.Image = AccentTemplate(Color.FromArgb(37, 37, 37), Color.FromArgb(38, 115, 236));
            radioImage4.Image = AccentTemplate(Color.FromArgb(37, 37, 37), Color.FromArgb(174, 17, 61));
            radioImage5.Image = AccentTemplate(Color.FromArgb(46, 23, 0), Color.FromArgb(99, 47, 0));
            radioImage6.Image = AccentTemplate(Color.FromArgb(78, 0, 0), Color.FromArgb(176, 30, 0));
            radioImage7.Image = AccentTemplate(Color.FromArgb(78, 0, 56), Color.FromArgb(193, 0, 79));
            radioImage8.Image = AccentTemplate(Color.FromArgb(45, 0, 78), Color.FromArgb(114, 0, 172));
            radioImage9.Image = AccentTemplate(Color.FromArgb(31, 0, 104), Color.FromArgb(70, 23, 180));
            radioImage10.Image = AccentTemplate(Color.FromArgb(0, 30, 78), Color.FromArgb(0, 106, 193));
            radioImage11.Image = AccentTemplate(Color.FromArgb(0, 77, 96), Color.FromArgb(0, 130, 135));
            radioImage12.Image = AccentTemplate(Color.FromArgb(0, 74, 0), Color.FromArgb(25, 153, 0));
            radioImage13.Image = AccentTemplate(Color.FromArgb(21, 153, 42), Color.FromArgb(0, 193, 63));
            radioImage14.Image = AccentTemplate(Color.FromArgb(229, 108, 25), Color.FromArgb(255, 152, 29));
            radioImage15.Image = AccentTemplate(Color.FromArgb(184, 27, 27), Color.FromArgb(255, 46, 18));
            radioImage16.Image = AccentTemplate(Color.FromArgb(184, 27, 108), Color.FromArgb(255, 29, 119));
            radioImage17.Image = AccentTemplate(Color.FromArgb(105, 27, 184), Color.FromArgb(170, 64, 255));
            radioImage18.Image = AccentTemplate(Color.FromArgb(27, 88, 184), Color.FromArgb(31, 174, 255));
            radioImage19.Image = AccentTemplate(Color.FromArgb(86, 156, 227), Color.FromArgb(86, 197, 255));
            radioImage20.Image = AccentTemplate(Color.FromArgb(0, 170, 170), Color.FromArgb(0, 216, 204));
            radioImage21.Image = AccentTemplate(Color.FromArgb(131, 186, 31), Color.FromArgb(145, 209, 0));
            radioImage22.Image = AccentTemplate(Color.FromArgb(211, 157, 9), Color.FromArgb(225, 183, 0));
            radioImage23.Image = AccentTemplate(Color.FromArgb(224, 100, 183), Color.FromArgb(255, 118, 188));
            radioImage24.Image = AccentTemplate(Color.FromArgb(105, 105, 105), Color.FromArgb(0, 164, 164));
            radioImage25.Image = AccentTemplate(Color.FromArgb(105, 105, 105), Color.FromArgb(255, 125, 35));
        }

        Bitmap AccentTemplate(Color background, Color Foreground, int size = 24)
        {
            Bitmap bmp = new(size, size);

            SizeF accentRectSize = new(size / 3, size / 3);
            RectangleF accentRect = new((size - accentRectSize.Width) / 2, (size - accentRectSize.Height) / 2, accentRectSize.Width, accentRectSize.Height);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(background);
                using (Brush b = new SolidBrush(Foreground))
                {
                    g.FillRectangle(b, accentRect);
                }
            }

            return bmp;
        }

        public void LoadFromTM(Manager TM)
        {
            AspectEnabled = TM.Windows8.Enabled;
            ColorizationColor_pick.BackColor = TM.Windows8.ColorizationColor;
            ColorizationColorBalance_bar.Value = TM.Windows8.ColorizationColorBalance;
          
            switch (TM.Windows8.ColorSet_Version3)
            {
                case 0:
                    {
                        radioImage1.Checked = true;
                        break;
                    }
                case 1:
                    {
                        radioImage2.Checked = true;
                        break;
                    }
                case 2:
                    {
                        radioImage3.Checked = true;
                        break;
                    }
                case 3:
                    {
                        radioImage4.Checked = true;
                        break;
                    }
                case 4:
                    {
                        radioImage5.Checked = true;
                        break;
                    }
                case 5:
                    {
                        radioImage6.Checked = true;
                        break;
                    }
                case 6:
                    {
                        radioImage7.Checked = true;
                        break;
                    }
                case 7:
                    {
                        radioImage8.Checked = true;
                        break;
                    }
                case 8:
                    {
                        radioImage9.Checked = true;
                        break;
                    }
                case 9:
                    {
                        radioImage10.Checked = true;
                        break;
                    }
                case 10:
                    {
                        radioImage11.Checked = true;
                        break;
                    }
                case 11:
                    {
                        radioImage12.Checked = true;
                        break;
                    }
                case 12:
                    {
                        radioImage13.Checked = true;
                        break;
                    }
                case 13:
                    {
                        radioImage14.Checked = true;
                        break;
                    }
                case 14:
                    {
                        radioImage15.Checked = true;
                        break;
                    }
                case 15:
                    {
                        radioImage16.Checked = true;
                        break;
                    }
                case 16:
                    {
                        radioImage17.Checked = true;
                        break;
                    }
                case 17:
                    {
                        radioImage18.Checked = true;
                        break;
                    }
                case 18:
                    {
                        radioImage19.Checked = true;
                        break;
                    }
                case 19:
                    {
                        radioImage20.Checked = true;
                        break;
                    }
                case 20:
                    {
                        radioImage21.Checked = true;
                        break;
                    }
                case 21:
                    {
                        radioImage22.Checked = true;
                        break;
                    }
                case 22:
                    {
                        radioImage23.Checked = true;
                        break;
                    }
                case 23:
                    {
                        radioImage24.Checked = true;
                        break;
                    }
                case 24:
                    {
                        radioImage25.Checked = true;
                        break;
                    }
                default:
                    {
                        radioImage9.Checked = true;
                        break;
                    }
            }

            switch (TM.Windows8.StartBackground)
            {
                case 100:
                    {
                        start_1.Checked = true;
                        break;
                    }
                case 101:
                    {
                        start_2.Checked = true;
                        break;
                    }
                case 102:
                    {
                        start_3.Checked = true;
                        break;
                    }
                case 103:
                    {
                        start_4.Checked = true;
                        break;
                    }
                case 104:
                    {
                        start_5.Checked = true;
                        break;
                    }
                case 105:
                    {
                        start_6.Checked = true;
                        break;
                    }
                case 106:
                    {
                        start_7.Checked = true;
                        break;
                    }
                case 107:
                    {
                        start_8.Checked = true;
                        break;
                    }
                case 108:
                    {
                        start_9.Checked = true;
                        break;
                    }
                case 109:
                    {
                        start_10.Checked = true;
                        break;
                    }
                case 110:
                    {
                        start_11.Checked = true;
                        break;
                    }
                case 111:
                    {
                        start_12.Checked = true;
                        break;
                    }
                case 112:
                    {
                        start_13.Checked = true;
                        break;
                    }
                case 113:
                    {
                        start_14.Checked = true;
                        break;
                    }
                case 114:
                    {
                        start_15.Checked = true;
                        break;
                    }
                case 115:
                    {
                        start_16.Checked = true;
                        break;
                    }
                case 116:
                    {
                        start_17.Checked = true;
                        break;
                    }
                case 117:
                    {
                        start_18.Checked = true;
                        break;
                    }
                case 118:
                    {
                        start_19.Checked = true;
                        break;
                    }
                case 119:
                    {
                        start_20.Checked = true;
                        break;
                    }
                default:
                    {
                        start_1.Checked = true;
                        break;
                    }
            }

            toggle1.Checked = TM.Windows8.VisualStyles.Enabled;

            if (TM.Windows8.VisualStyles.VisualStylesType == Theme.Structures.VisualStyles.DefaultVisualStyles.AeroLite) theme_aerolite.Checked = true;
            else if (TM.Windows8.VisualStyles.VisualStylesType == Theme.Structures.VisualStyles.DefaultVisualStyles.Custom) theme_custom_check.Checked = true;
            else theme_aero.Checked = true;
            groupBox4.Visible = theme_custom_check.Checked;

            VS_textbox.Text = TM.Windows8.VisualStyles.ThemeFile;
            VS_ColorsList.SelectedItem = TM.Windows8.VisualStyles.ColorScheme;

            if (TM.Windows8.VisualStyles.SizeScheme.ToLower() == "normalsize" && VS_SizesList.Items.Contains("Normal")) TM.Windows8.VisualStyles.SizeScheme = "Normal";
            else if (TM.Windows8.VisualStyles.SizeScheme.ToLower() == "normalsize" && VS_SizesList.Items.Contains("normal")) TM.Windows8.VisualStyles.SizeScheme = "normal";

            VS_SizesList.SelectedItem = TM.Windows8.VisualStyles.SizeScheme;

            VS_ReplaceColors.Checked = TM.Windows8.VisualStyles.OverrideColors;
            VS_ReplaceMetrics.Checked = TM.Windows8.VisualStyles.OverrideSizes;

            windowsDesktop1.HookedTM = TM;
            windowsDesktop1.LoadFromTM(TM);

            RefreshDWM();
        }

        public void ApplyToTM(Manager TM)
        {
            TM.Windows8.Enabled = AspectEnabled;
            TM.Windows8.ColorizationColor = ColorizationColor_pick.BackColor;
            TM.Windows8.ColorizationColorBalance = ColorizationColorBalance_bar.Value;

            TM.Windows8.ColorSet_Version3 = radioImage1.Checked ? 0 :
                                           radioImage2.Checked ? 1 :
                                           radioImage3.Checked ? 2 :
                                           radioImage4.Checked ? 3 :
                                           radioImage5.Checked ? 4 :
                                           radioImage6.Checked ? 5 :
                                           radioImage7.Checked ? 6 :
                                           radioImage8.Checked ? 7 :
                                           radioImage9.Checked ? 8 :
                                           radioImage10.Checked ? 9 :
                                           radioImage11.Checked ? 10 :
                                           radioImage12.Checked ? 11 :
                                           radioImage13.Checked ? 12 :
                                           radioImage14.Checked ? 13 :
                                           radioImage15.Checked ? 14 :
                                           radioImage16.Checked ? 15 :
                                           radioImage17.Checked ? 16 :
                                           radioImage18.Checked ? 17 :
                                           radioImage19.Checked ? 18 :
                                           radioImage20.Checked ? 19 :
                                           radioImage21.Checked ? 20 :
                                           radioImage22.Checked ? 21 :
                                           radioImage23.Checked ? 22 :
                                           radioImage24.Checked ? 23 : 24;

            TM.Windows8.StartBackground = start_1.Checked ? 100 :
                                          start_2.Checked ? 101 :
                                          start_3.Checked ? 102 :
                                          start_4.Checked ? 103 :
                                          start_5.Checked ? 104 :
                                          start_6.Checked ? 105 :
                                          start_7.Checked ? 106 :
                                          start_8.Checked ? 107 :
                                          start_9.Checked ? 108 :
                                          start_10.Checked ? 109 :
                                          start_11.Checked ? 110 :
                                          start_12.Checked ? 111 :
                                          start_13.Checked ? 112 :
                                          start_14.Checked ? 113 :
                                          start_15.Checked ? 114 :
                                          start_16.Checked ? 115 :
                                          start_17.Checked ? 116 :
                                          start_18.Checked ? 117 :
                                          start_19.Checked ? 118 :
                                          start_20.Checked ? 119 : 100;

            TM.Windows8.VisualStyles.Enabled = toggle1.Checked;
            if (theme_aerolite.Checked) TM.Windows8.VisualStyles.VisualStylesType = Theme.Structures.VisualStyles.DefaultVisualStyles.AeroLite;
            else if (theme_custom_check.Checked) TM.Windows8.VisualStyles.VisualStylesType = Theme.Structures.VisualStyles.DefaultVisualStyles.Custom;
            else TM.Windows8.VisualStyles.VisualStylesType = Theme.Structures.VisualStyles.DefaultVisualStyles.Aero;

            TM.Windows8.VisualStyles.ThemeFile = VS_textbox.Text;
            TM.Windows8.VisualStyles.ColorScheme = (VS_ColorsList.SelectedItem ?? "").ToString();
            TM.Windows8.VisualStyles.SizeScheme = (VS_SizesList.SelectedItem ?? "").ToString().ToLower() == "normal" ? "NormalSize" : (VS_SizesList.SelectedItem ?? "").ToString();
            TM.Windows8.VisualStyles.OverrideColors = VS_ReplaceColors.Checked;
            TM.Windows8.VisualStyles.OverrideSizes = VS_ReplaceMetrics.Checked;

            if (VS_ReplaceColors.Checked)
            {
                string theme = VS_textbox.Text;

                try
                {
                    // Newer versions of msstyles
                    using (libmsstyle.VisualStyle visualStyle = new(theme)) { TM.Win32 = visualStyle.ClassicColors(); }
                }
                catch
                {
                    // Old msstyles (Windows XP)
                    try
                    {
                        if (System.IO.Path.GetExtension(theme).ToLower() == ".msstyles")
                        {
                            System.IO.File.WriteAllText($@"{SysPaths.appData}\VisualStyles\Luna\win32uischeme.theme", $"[VisualStyles]{"\r\n"}Path={theme}{"\r\n"}ColorStyle=NormalColor{"\r\n"}Size=NormalSize");
                            theme = $@"{SysPaths.appData}\VisualStyles\Luna\win32uischeme.theme";
                        }

                        if (!string.IsNullOrEmpty(theme) && System.IO.File.Exists(theme))
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
                    using (libmsstyle.VisualStyle visualStyle = new(theme)) { TM.MetricsFonts = visualStyle.MetricsFonts(); }
                }
                catch
                {
                    // Old msstyles (Windows XP)
                    try
                    {
                        if (System.IO.Path.GetExtension(theme).ToLower() == ".msstyles")
                        {
                            System.IO.File.WriteAllText($@"{SysPaths.appData}\VisualStyles\Luna\win32uischeme.theme", $"[VisualStyles]{"\r\n"}Path={theme}{"\r\n"}ColorStyle=NormalColor{"\r\n"}Size=NormalSize");
                            theme = $@"{SysPaths.appData}\VisualStyles\Luna\win32uischeme.theme";
                        }

                        if (!string.IsNullOrEmpty(theme) && System.IO.File.Exists(theme))
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
            using (Manager DefTM = Theme.Default.Get(WindowStyle.W81))
            {
                ColorizationColor_pick.DefaultBackColor = DefTM.Windows8.ColorizationColor;
                ColorizationColorBalance_bar.DefaultValue = DefTM.Windows8.ColorizationColorBalance;

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
            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                Forms.SubMenu.ShowMenu((ColorItem)sender);
                if (ColorClipboard.Event == ColorClipboard.MenuEvent.Cut | ColorClipboard.Event == ColorClipboard.MenuEvent.Paste | ColorClipboard.Event == ColorClipboard.MenuEvent.Override)
                {
                    windowsDesktop1.TitlebarColor_Active = ((ColorItem)sender).BackColor;
                    windowsDesktop1.TitlebarColor_Inactive = ((ColorItem)sender).BackColor;
                }
                return;
            }

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

        private void windowsDesktop1_EditorInvoker(object sender, UI.Retro.EditorEventArgs e)
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
                                DWMAPI.DwmGetColorizationParameters(out DWMAPI.DWM_COLORIZATION_PARAMS colorizationParams);

                                colorizationParams.clrColor = (uint)ColorizationColor_pick.BackColor.ToArgb();
                                colorizationParams.nIntensity = (uint)ColorizationColorBalance_bar.Value;

                                DWMAPI.DwmSetColorizationParameters(ref colorizationParams, false);
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

        private void start_color_pick_Click(object sender, EventArgs e)
        {
            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                Forms.SubMenu.ShowMenu((ColorItem)sender);
                return;
            }

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
            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                Forms.SubMenu.ShowMenu((ColorItem)sender);
                return;
            }

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

        private void background_pick_Click(object sender, EventArgs e)
        {
            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                Forms.SubMenu.ShowMenu((ColorItem)sender);
                return;
            }

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

        private void Win81Colors_HelpButtonClicked(object sender, System.ComponentModel.CancelEventArgs e)
        {
            System.Diagnostics.Process.Start(Links.Wiki.Win81Colors);
        }

        private void VS_Browse_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new() { FileName = VS_textbox.Text, Filter = Program.Filters.VisualStyles_And_Themes, Title = Program.Lang.Strings.Extensions.OpenVisualStyle })
            {
                if (dlg.ShowDialog() == DialogResult.OK) VS_textbox.Text = libmsstyle.VisualStyle.GetCorrectMSStyles(dlg.FileName);
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

            if (!System.IO.File.Exists(theme)) theme = UxTheme.GetCurrentVS().Item1;

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

        private void theme_aero_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as UI.WP.RadioImage).Checked) windowsDesktop1.VisualStyles = Theme.Structures.VisualStyles.DefaultVisualStyles.Aero;
        }

        private void theme_aerolite_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as UI.WP.RadioImage).Checked) windowsDesktop1.VisualStyles = Theme.Structures.VisualStyles.DefaultVisualStyles.AeroLite;
        }

        private void theme_custom_check_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as RadioImage).Checked)
            {
                windowsDesktop1.VisualStyles = Theme.Structures.VisualStyles.DefaultVisualStyles.Custom;
            }

            groupBox4.Visible = (sender as UI.WP.RadioImage).Checked;
        }
    }
}