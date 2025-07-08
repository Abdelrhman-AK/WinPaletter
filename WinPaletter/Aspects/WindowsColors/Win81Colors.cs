using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinPaletter.NativeMethods;
using WinPaletter.Theme;
using WinPaletter.UI.Controllers;
using static WinPaletter.PreviewHelpers;
using static WinPaletter.TypesExtensions.ColorsExtensions;

namespace WinPaletter
{
    public partial class Win81Colors : AspectsTemplate
    {
        bool canChangeColor = true;

        public int LogonUI_ID;
        public int StartBackground_ID;

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
                TMx.Windows81.Apply(TMx);
                ApplyToTM(Program.TM);
                ApplyToTM(Program.TM_Original);
            }

            Cursor = Cursors.Default;
        }

        private void ModeSwitched(object sender, EventArgs e)
        {
            tablessControl1.SelectedIndex = AdvancedMode ? 0 : 1;
        }

        private void GeneratePalette_Image(object sender, EventArgs e)
        {
            Forms.PaletteGenerateFromImage.ShowDialog();
        }

        private void GeneratePalette_Color(object sender, EventArgs e)
        {
            Forms.PaletteGenerateFromColor.ShowDialog();
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

            switch (TM.Windows81.Theme)
            {
                case Theme.Structures.Windows7.Themes.Aero:
                    {
                        theme_aero.Checked = true;
                        break;
                    }

                case Theme.Structures.Windows7.Themes.AeroLite:
                    {
                        theme_aerolite.Checked = true;
                        break;
                    }
            }

            StartBackground_ID = TM.Windows81.Start;

            ApplyMetroStartToButton(StartBackground_ID, personalcls_background_pick.BackColor, start);
            ApplyMetroStartToButton(StartBackground_ID, personalcls_background_pick.BackColor, easy_start);
            easy_start.Image = easy_start.Image.Resize(32, 32);

            windowsDesktop1.HookedTM = TM;
            windowsDesktop1.LoadFromTM(TM);

            RefreshDWM();
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

            TM.Windows81.Start = StartBackground_ID;

            if (theme_aero.Checked)
            {
                TM.Windows81.Theme = Theme.Structures.Windows7.Themes.Aero;
            }
            else if (theme_aerolite.Checked)
            {
                TM.Windows81.Theme = Theme.Structures.Windows7.Themes.AeroLite;
            }
        }

        public void ApplyDefaultTMValues()
        {
            using (Manager DefTM = Theme.Default.Get(WindowStyle.W81))
            {
                ColorizationColor_pick.DefaultBackColor = DefTM.Windows81.ColorizationColor;
                ColorizationColorBalance_bar.DefaultValue = DefTM.Windows81.ColorizationColorBalance;
                start_pick.DefaultBackColor = DefTM.Windows81.StartColor;
                accent_pick.DefaultBackColor = DefTM.Windows81.AccentColor;
                personalcls_background_pick.DefaultBackColor = DefTM.Windows81.PersonalColors_Background;
                personalcolor_accent_pick.DefaultBackColor = DefTM.Windows81.PersonalColors_Accent;
                easy_background.DefaultBackColor = DefTM.Windows81.PersonalColors_Background;
                easy_foreground.DefaultBackColor = DefTM.Windows81.PersonalColors_Accent;
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

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            groupBox3.Visible = checkBox2.Checked;
        }

        private void colorItem1_Click(object sender, EventArgs e)
        {
            canChangeColor = false;

            HSL hSL = ((ColorItem)sender).BackColor.ToHSL();
            colorBarX1.Value = hSL.H;
            colorBarX2.Value = (int)(hSL.S * 100);
            colorBarX3.Value = (int)(hSL.L * 100);

            ColorizationColorBalance_bar.Value = 85;

            canChangeColor = true;
            ColorizationColor_pick.BackColor = hSL.ToRGB();
            windowsDesktop1.TitlebarColor_Active = hSL.ToRGB();
            windowsDesktop1.AfterGlowColor_Active = hSL.ToRGB();
            windowsDesktop1.TitlebarColor_Inactive = hSL.ToRGB();
            windowsDesktop1.AfterGlowColor_Inactive = hSL.ToRGB();
        }

        private void colorItem2_Click(object sender, EventArgs e)
        {
            canChangeColor = false;

            HSL hSL = ((ColorItem)sender).BackColor.ToHSL();
            colorBarX1.Value = hSL.H;
            colorBarX2.Value = (int)(hSL.S * 100);
            colorBarX3.Value = (int)(hSL.L * 100);

            ColorizationColorBalance_bar.Value = 85;

            canChangeColor = true;
            ColorizationColor_pick.BackColor = hSL.ToRGB();
            windowsDesktop1.TitlebarColor_Active = hSL.ToRGB();
            windowsDesktop1.AfterGlowColor_Active = hSL.ToRGB();
            windowsDesktop1.TitlebarColor_Inactive = hSL.ToRGB();
            windowsDesktop1.AfterGlowColor_Inactive = hSL.ToRGB();
        }

        private void colorItem3_Click(object sender, EventArgs e)
        {
            canChangeColor = false;

            HSL hSL = ((ColorItem)sender).BackColor.ToHSL();
            colorBarX1.Value = hSL.H;
            colorBarX2.Value = (int)(hSL.S * 100);
            colorBarX3.Value = (int)(hSL.L * 100);

            ColorizationColorBalance_bar.Value = 85;

            canChangeColor = true;
            ColorizationColor_pick.BackColor = hSL.ToRGB();
            windowsDesktop1.TitlebarColor_Active = hSL.ToRGB();
            windowsDesktop1.AfterGlowColor_Active = hSL.ToRGB();
            windowsDesktop1.TitlebarColor_Inactive = hSL.ToRGB();
            windowsDesktop1.AfterGlowColor_Inactive = hSL.ToRGB();
        }

        private void colorItem4_Click(object sender, EventArgs e)
        {
            canChangeColor = false;

            HSL hSL = ((ColorItem)sender).BackColor.ToHSL();
            colorBarX1.Value = hSL.H;
            colorBarX2.Value = (int)(hSL.S * 100);
            colorBarX3.Value = (int)(hSL.L * 100);

            ColorizationColorBalance_bar.Value = 85;

            canChangeColor = true;
            ColorizationColor_pick.BackColor = hSL.ToRGB();
            windowsDesktop1.TitlebarColor_Active = hSL.ToRGB();
            windowsDesktop1.AfterGlowColor_Active = hSL.ToRGB();
            windowsDesktop1.TitlebarColor_Inactive = hSL.ToRGB();
            windowsDesktop1.AfterGlowColor_Inactive = hSL.ToRGB();
        }

        private void colorItem5_Click(object sender, EventArgs e)
        {
            canChangeColor = false;

            HSL hSL = ((ColorItem)sender).BackColor.ToHSL();
            colorBarX1.Value = hSL.H;
            colorBarX2.Value = (int)(hSL.S * 100);
            colorBarX3.Value = (int)(hSL.L * 100);

            ColorizationColorBalance_bar.Value = 85;

            canChangeColor = true;
            ColorizationColor_pick.BackColor = hSL.ToRGB();
            windowsDesktop1.TitlebarColor_Active = hSL.ToRGB();
            windowsDesktop1.AfterGlowColor_Active = hSL.ToRGB();
            windowsDesktop1.TitlebarColor_Inactive = hSL.ToRGB();
            windowsDesktop1.AfterGlowColor_Inactive = hSL.ToRGB();
        }

        private void colorItem8_Click(object sender, EventArgs e)
        {
            canChangeColor = false;

            HSL hSL = ((ColorItem)sender).BackColor.ToHSL();
            colorBarX1.Value = hSL.H;
            colorBarX2.Value = (int)(hSL.S * 100);
            colorBarX3.Value = (int)(hSL.L * 100);

            ColorizationColorBalance_bar.Value = 85;

            canChangeColor = true;
            ColorizationColor_pick.BackColor = hSL.ToRGB();
            windowsDesktop1.TitlebarColor_Active = hSL.ToRGB();
            windowsDesktop1.AfterGlowColor_Active = hSL.ToRGB();
            windowsDesktop1.TitlebarColor_Inactive = hSL.ToRGB();
            windowsDesktop1.AfterGlowColor_Inactive = hSL.ToRGB();
        }

        private void colorItem6_Click(object sender, EventArgs e)
        {
            canChangeColor = false;

            HSL hSL = ((ColorItem)sender).BackColor.ToHSL();
            colorBarX1.Value = hSL.H;
            colorBarX2.Value = (int)(hSL.S * 100);
            colorBarX3.Value = (int)(hSL.L * 100);

            ColorizationColorBalance_bar.Value = 85;

            canChangeColor = true;
            ColorizationColor_pick.BackColor = hSL.ToRGB();
            windowsDesktop1.TitlebarColor_Active = hSL.ToRGB();
            windowsDesktop1.AfterGlowColor_Active = hSL.ToRGB();
            windowsDesktop1.TitlebarColor_Inactive = hSL.ToRGB();
            windowsDesktop1.AfterGlowColor_Inactive = hSL.ToRGB();
        }

        private void colorItem7_Click(object sender, EventArgs e)
        {
            canChangeColor = false;

            HSL hSL = ((ColorItem)sender).BackColor.ToHSL();
            colorBarX1.Value = hSL.H;
            colorBarX2.Value = (int)(hSL.S * 100);
            colorBarX3.Value = (int)(hSL.L * 100);

            ColorizationColorBalance_bar.Value = 85;

            canChangeColor = true;
            ColorizationColor_pick.BackColor = hSL.ToRGB();
            windowsDesktop1.TitlebarColor_Active = hSL.ToRGB();
            windowsDesktop1.AfterGlowColor_Active = hSL.ToRGB();
            windowsDesktop1.TitlebarColor_Inactive = hSL.ToRGB();
            windowsDesktop1.AfterGlowColor_Inactive = hSL.ToRGB();
        }

        private void colorItem16_Click(object sender, EventArgs e)
        {
            canChangeColor = false;

            HSL hSL = ((ColorItem)sender).BackColor.ToHSL();
            colorBarX1.Value = hSL.H;
            colorBarX2.Value = (int)(hSL.S * 100);
            colorBarX3.Value = (int)(hSL.L * 100);

            ColorizationColorBalance_bar.Value = 85;

            canChangeColor = true;
            ColorizationColor_pick.BackColor = hSL.ToRGB();
            windowsDesktop1.TitlebarColor_Active = hSL.ToRGB();
            windowsDesktop1.AfterGlowColor_Active = hSL.ToRGB();
            windowsDesktop1.TitlebarColor_Inactive = hSL.ToRGB();
            windowsDesktop1.AfterGlowColor_Inactive = hSL.ToRGB();
        }

        private void colorItem14_Click(object sender, EventArgs e)
        {
            canChangeColor = false;

            HSL hSL = ((ColorItem)sender).BackColor.ToHSL();
            colorBarX1.Value = hSL.H;
            colorBarX2.Value = (int)(hSL.S * 100);
            colorBarX3.Value = (int)(hSL.L * 100);

            ColorizationColorBalance_bar.Value = 85;

            canChangeColor = true;
            ColorizationColor_pick.BackColor = hSL.ToRGB();
            windowsDesktop1.TitlebarColor_Active = hSL.ToRGB();
            windowsDesktop1.AfterGlowColor_Active = hSL.ToRGB();
            windowsDesktop1.TitlebarColor_Inactive = hSL.ToRGB();
            windowsDesktop1.AfterGlowColor_Inactive = hSL.ToRGB();
        }

        private void colorItem15_Click(object sender, EventArgs e)
        {
            canChangeColor = false;

            HSL hSL = ((ColorItem)sender).BackColor.ToHSL();
            colorBarX1.Value = hSL.H;
            colorBarX2.Value = (int)(hSL.S * 100);
            colorBarX3.Value = (int)(hSL.L * 100);

            ColorizationColorBalance_bar.Value = 85;

            canChangeColor = true;
            ColorizationColor_pick.BackColor = hSL.ToRGB();
            windowsDesktop1.TitlebarColor_Active = hSL.ToRGB();
            windowsDesktop1.AfterGlowColor_Active = hSL.ToRGB();
            windowsDesktop1.TitlebarColor_Inactive = hSL.ToRGB();
            windowsDesktop1.AfterGlowColor_Inactive = hSL.ToRGB();
        }

        private void colorItem13_Click(object sender, EventArgs e)
        {
            canChangeColor = false;

            HSL hSL = ((ColorItem)sender).BackColor.ToHSL();
            colorBarX1.Value = hSL.H;
            colorBarX2.Value = (int)(hSL.S * 100);
            colorBarX3.Value = (int)(hSL.L * 100);

            ColorizationColorBalance_bar.Value = 85;

            canChangeColor = true;
            ColorizationColor_pick.BackColor = hSL.ToRGB();
            windowsDesktop1.TitlebarColor_Active = hSL.ToRGB();
            windowsDesktop1.AfterGlowColor_Active = hSL.ToRGB();
            windowsDesktop1.TitlebarColor_Inactive = hSL.ToRGB();
            windowsDesktop1.AfterGlowColor_Inactive = hSL.ToRGB();
        }

        private void colorItem12_Click(object sender, EventArgs e)
        {
            canChangeColor = false;

            HSL hSL = ((ColorItem)sender).BackColor.ToHSL();
            colorBarX1.Value = hSL.H;
            colorBarX2.Value = (int)(hSL.S * 100);
            colorBarX3.Value = (int)(hSL.L * 100);

            ColorizationColorBalance_bar.Value = 85;

            canChangeColor = true;
            ColorizationColor_pick.BackColor = hSL.ToRGB();
            windowsDesktop1.TitlebarColor_Active = hSL.ToRGB();
            windowsDesktop1.AfterGlowColor_Active = hSL.ToRGB();
            windowsDesktop1.TitlebarColor_Inactive = hSL.ToRGB();
            windowsDesktop1.AfterGlowColor_Inactive = hSL.ToRGB();
        }

        private void colorItem10_Click(object sender, EventArgs e)
        {
            canChangeColor = false;

            HSL hSL = ((ColorItem)sender).BackColor.ToHSL();
            colorBarX1.Value = hSL.H;
            colorBarX2.Value = (int)(hSL.S * 100);
            colorBarX3.Value = (int)(hSL.L * 100);

            ColorizationColorBalance_bar.Value = 85;

            canChangeColor = true;
            ColorizationColor_pick.BackColor = hSL.ToRGB();
            windowsDesktop1.TitlebarColor_Active = hSL.ToRGB();
            windowsDesktop1.AfterGlowColor_Active = hSL.ToRGB();
            windowsDesktop1.TitlebarColor_Inactive = hSL.ToRGB();
            windowsDesktop1.AfterGlowColor_Inactive = hSL.ToRGB();
        }

        private void colorItem11_Click(object sender, EventArgs e)
        {
            canChangeColor = false;

            HSL hSL = ((ColorItem)sender).BackColor.ToHSL();
            colorBarX1.Value = hSL.H;
            colorBarX2.Value = (int)(hSL.S * 100);
            colorBarX3.Value = (int)(hSL.L * 100);

            ColorizationColorBalance_bar.Value = 85;

            canChangeColor = true;
            ColorizationColor_pick.BackColor = hSL.ToRGB();
            windowsDesktop1.TitlebarColor_Active = hSL.ToRGB();
            windowsDesktop1.AfterGlowColor_Active = hSL.ToRGB();
            windowsDesktop1.TitlebarColor_Inactive = hSL.ToRGB();
            windowsDesktop1.AfterGlowColor_Inactive = hSL.ToRGB();
        }

        private void colorItem9_Click(object sender, EventArgs e)
        {
            canChangeColor = false;

            HSL hSL = ((ColorItem)sender).BackColor.ToHSL();
            colorBarX1.Value = hSL.H;
            colorBarX2.Value = (int)(hSL.S * 100);
            colorBarX3.Value = (int)(hSL.L * 100);

            ColorizationColorBalance_bar.Value = 85;

            canChangeColor = true;
            ColorizationColor_pick.BackColor = hSL.ToRGB();
            windowsDesktop1.TitlebarColor_Active = hSL.ToRGB();
            windowsDesktop1.AfterGlowColor_Active = hSL.ToRGB();
            windowsDesktop1.TitlebarColor_Inactive = hSL.ToRGB();
            windowsDesktop1.AfterGlowColor_Inactive = hSL.ToRGB();
        }

        private void colorBarX_ValueChanged(object sender, EventArgs e)
        {
            HSL hSL = new()
            {
                H = colorBarX1.Value,
                S = colorBarX2.Value / 100f,
                L = colorBarX3.Value / 100f
            };

            if (canChangeColor)
            {
                ColorizationColor_pick.BackColor = hSL.ToRGB();
                windowsDesktop1.TitlebarColor_Active = hSL.ToRGB();
                windowsDesktop1.AfterGlowColor_Active = hSL.ToRGB();
                windowsDesktop1.TitlebarColor_Inactive = hSL.ToRGB();
                windowsDesktop1.AfterGlowColor_Inactive = hSL.ToRGB();
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

            Program.Settings.AspectsControl.WinColors_Advanced = AdvancedMode;
            Program.Settings.AspectsControl.Save();
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

        private void theme_aero_CheckedChanged_1(object sender, EventArgs e)
        {
            if (IsShown && theme_aero.Checked)
            {
                RefreshDWM();
                windowsDesktop1.Windows_7_8_Theme = Theme.Structures.Windows7.Themes.Aero;
            }
        }

        private void theme_aerolite_CheckedChanged(object sender, EventArgs e)
        {
            if (IsShown && theme_aerolite.Checked)
            {
                RefreshDWM();
                windowsDesktop1.Windows_7_8_Theme = Theme.Structures.Windows7.Themes.AeroLite;
            }
        }

        private void start_Click(object sender, EventArgs e)
        {
            if (Forms.Start81Selector.ShowDialog() == DialogResult.OK)
            {
                ApplyMetroStartToButton(StartBackground_ID, personalcls_background_pick.BackColor, start);
                ApplyMetroStartToButton(StartBackground_ID, personalcls_background_pick.BackColor, easy_start);
                easy_start.Image = easy_start.Image.Resize(32, 32);
            }
        }

        private void personalcolor_accent_pick_BackColorChanged(object sender, EventArgs e)
        {
            easy_foreground.BackColor = personalcolor_accent_pick.BackColor;
        }

        private void personalcls_background_pick_BackColorChanged(object sender, EventArgs e)
        {
            easy_background.BackColor = personalcls_background_pick.BackColor;
        }

        private void personalcolor_accent_pick_Click(object sender, EventArgs e)
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

        private void personalcls_background_pick_Click(object sender, EventArgs e)
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

        private void start_pick_Click(object sender, EventArgs e)
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

        private void easy_foreground_Click(object sender, EventArgs e)
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
                { personalcolor_accent_pick, new string[] { nameof(personalcolor_accent_pick.BackColor) }},
                { accent_pick, new string[] { nameof(accent_pick.BackColor) }},
            };

            Color C = Forms.ColorPickerDlg.Pick(CList);

            colorItem.BackColor = C;
            colorItem.Invalidate();

            CList.Clear();
        }

        private void easy_background_Click(object sender, EventArgs e)
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
                { personalcls_background_pick, new string[] { nameof(personalcls_background_pick.BackColor) }},
                { start_pick, new string[] { nameof(start_pick.BackColor) }},
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
    }
}