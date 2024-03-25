using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WinPaletter.Theme;
using WinPaletter.UI.Controllers;
using static WinPaletter.PreviewHelpers;
using static WinPaletter.TypesExtensions.ColorsExtensions;

namespace WinPaletter
{
    public partial class WinVistaColors : AspectsTemplate
    {
        bool canChangeColor = true;

        public WinVistaColors()
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
            if (Program.Settings.AspectsControl.Enabled && !Program.Settings.AspectsControl.WinColors)
            {
                MsgBox(Program.Lang.AspectDisabled_Apply_0, MessageBoxButtons.OK, MessageBoxIcon.Warning, Program.Lang.AspectDisabled_Apply_1);
                return;
            }

            Cursor = Cursors.WaitCursor;

            using (Theme.Manager TMx = new(Theme.Manager.Source.Registry))
            {
                if (Program.Settings.BackupTheme.Enabled && Program.Settings.BackupTheme.AutoBackupOnApplySingleAspect)
                {
                    string filename = Program.GetUniqueFileName($"{Program.Settings.BackupTheme.BackupPath}\\OnAspectApply", $"{TMx.Info.ThemeName}_{DateTime.Now.Hour}.{DateTime.Now.Minute}.{DateTime.Now.Second}.wpth");
                    TMx.Save(Theme.Manager.Source.File, filename);
                }

                ApplyToTM(TMx);
                TMx.WindowsVista.Apply();
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

        private void WinVistaColors_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.Settings.AspectsControl.WinColors_Advanced = AdvancedMode;
            Program.Settings.AspectsControl.Save();
        }

        private void WinVistaColors_Load(object sender, EventArgs e)
        {
            DesignerData data = new(this)
            {
                AspectName = string.Format(Program.Lang.WindowsColors, OS.Name),
                Enabled = Program.TM.WindowsVista.Enabled,
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

        public void LoadFromTM(Theme.Manager TM)
        {
            AspectEnabled = TM.WindowsVista.Enabled;
            ColorizationColor_pick.BackColor = TM.WindowsVista.ColorizationColor;
            ColorizationColorBalance_bar.Value = TM.WindowsVista.Alpha;

            switch (TM.WindowsVista.Theme)
            {
                case Theme.Structures.Windows7.Themes.Aero:
                    {
                        theme_aero.Checked = true;
                        break;
                    }

                case Theme.Structures.Windows7.Themes.AeroOpaque:
                    {
                        theme_aeroopaque.Checked = true;
                        break;
                    }

                case Theme.Structures.Windows7.Themes.Basic:
                    {
                        theme_basic.Checked = true;
                        break;
                    }

                case Theme.Structures.Windows7.Themes.Classic:
                    {
                        theme_classic.Checked = true;
                        break;
                    }
            }

            checkBox1.Checked = TM.WindowsVista.Theme == Theme.Structures.Windows7.Themes.Aero;

            windowsDesktop1.HookedTM = TM;
            windowsDesktop1.LoadFromTM(TM);
        }

        public void ApplyToTM(Theme.Manager TM)
        {
            TM.WindowsVista.Enabled = AspectEnabled;
            TM.WindowsVista.ColorizationColor = ColorizationColor_pick.BackColor;
            TM.WindowsVista.Alpha = (byte)ColorizationColorBalance_bar.Value;

            if (theme_aero.Checked)
            {
                TM.WindowsVista.Theme = Theme.Structures.Windows7.Themes.Aero;
            }
            else if (theme_aeroopaque.Checked)
            {
                TM.WindowsVista.Theme = Theme.Structures.Windows7.Themes.AeroOpaque;
            }
            else if (theme_basic.Checked)
            {
                TM.WindowsVista.Theme = Theme.Structures.Windows7.Themes.Basic;
            }
            else if (theme_classic.Checked)
            {
                TM.WindowsVista.Theme = Theme.Structures.Windows7.Themes.Classic;
            }

        }

        public void ApplyDefaultTMValues()
        {
            using (Theme.Manager DefTM = Theme.Default.Get(WindowStyle.W7))
            {
                ColorizationColor_pick.DefaultBackColor = DefTM.WindowsVista.ColorizationColor;
                ColorizationColorBalance_bar.DefaultValue = DefTM.WindowsVista.Alpha;
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

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            groupBox3.Visible = checkBox2.Checked;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                theme_aero.Checked = true;
            }
            else
            {
                theme_aeroopaque.Checked = true;
            }
        }

        private void colorItem1_Click(object sender, EventArgs e)
        {
            canChangeColor = false;

            HSL hSL = ((ColorItem)sender).BackColor.ToHSL();
            colorBarX1.Value = hSL.H;
            colorBarX2.Value = (int)(hSL.S * 100);
            colorBarX3.Value = (int)(hSL.L * 100);

            trackBarX5.Value = 200;

            canChangeColor = true;
            ColorizationColor_pick.BackColor = hSL.ToRGB();

            windowsDesktop1.TitlebarColor_Active = hSL.ToRGB();
            windowsDesktop1.TitlebarColor_Inactive = hSL.ToRGB();
        }

        private void colorItem2_Click(object sender, EventArgs e)
        {
            canChangeColor = false;

            HSL hSL = ((ColorItem)sender).BackColor.ToHSL();
            colorBarX1.Value = hSL.H;
            colorBarX2.Value = (int)(hSL.S * 100);
            colorBarX3.Value = (int)(hSL.L * 100);

            trackBarX5.Value = 200;

            canChangeColor = true;
            ColorizationColor_pick.BackColor = hSL.ToRGB();

            windowsDesktop1.TitlebarColor_Active = hSL.ToRGB();
            windowsDesktop1.TitlebarColor_Inactive = hSL.ToRGB();
        }

        private void colorItem3_Click(object sender, EventArgs e)
        {
            canChangeColor = false;

            HSL hSL = ((ColorItem)sender).BackColor.ToHSL();
            colorBarX1.Value = hSL.H;
            colorBarX2.Value = (int)(hSL.S * 100);
            colorBarX3.Value = (int)(hSL.L * 100);

            trackBarX5.Value = 200;

            canChangeColor = true;
            ColorizationColor_pick.BackColor = hSL.ToRGB();

            windowsDesktop1.TitlebarColor_Active = hSL.ToRGB();
            windowsDesktop1.TitlebarColor_Inactive = hSL.ToRGB();
        }

        private void colorItem4_Click(object sender, EventArgs e)
        {
            canChangeColor = false;

            HSL hSL = ((ColorItem)sender).BackColor.ToHSL();
            colorBarX1.Value = hSL.H;
            colorBarX2.Value = (int)(hSL.S * 100);
            colorBarX3.Value = (int)(hSL.L * 100);

            trackBarX5.Value = 200;

            canChangeColor = true;
            ColorizationColor_pick.BackColor = hSL.ToRGB();

            windowsDesktop1.TitlebarColor_Active = hSL.ToRGB();
            windowsDesktop1.TitlebarColor_Inactive = hSL.ToRGB();
        }

        private void colorItem5_Click(object sender, EventArgs e)
        {
            canChangeColor = false;

            HSL hSL = ((ColorItem)sender).BackColor.ToHSL();
            colorBarX1.Value = hSL.H;
            colorBarX2.Value = (int)(hSL.S * 100);
            colorBarX3.Value = (int)(hSL.L * 100);

            trackBarX5.Value = 200;

            canChangeColor = true;
            ColorizationColor_pick.BackColor = hSL.ToRGB();

            windowsDesktop1.TitlebarColor_Active = hSL.ToRGB();
            windowsDesktop1.TitlebarColor_Inactive = hSL.ToRGB();
        }

        private void colorItem8_Click(object sender, EventArgs e)
        {
            canChangeColor = false;

            HSL hSL = ((ColorItem)sender).BackColor.ToHSL();
            colorBarX1.Value = hSL.H;
            colorBarX2.Value = (int)(hSL.S * 100);
            colorBarX3.Value = (int)(hSL.L * 100);

            trackBarX5.Value = 200;

            canChangeColor = true;
            ColorizationColor_pick.BackColor = hSL.ToRGB();

            windowsDesktop1.TitlebarColor_Active = hSL.ToRGB();
            windowsDesktop1.TitlebarColor_Inactive = hSL.ToRGB();
        }

        private void colorItem6_Click(object sender, EventArgs e)
        {
            canChangeColor = false;

            HSL hSL = ((ColorItem)sender).BackColor.ToHSL();
            colorBarX1.Value = hSL.H;
            colorBarX2.Value = (int)(hSL.S * 100);
            colorBarX3.Value = (int)(hSL.L * 100);

            trackBarX5.Value = 200;

            canChangeColor = true;
            ColorizationColor_pick.BackColor = hSL.ToRGB();

            windowsDesktop1.TitlebarColor_Active = hSL.ToRGB();
            windowsDesktop1.TitlebarColor_Inactive = hSL.ToRGB();
        }

        private void colorItem7_Click(object sender, EventArgs e)
        {
            canChangeColor = false;

            HSL hSL = ((ColorItem)sender).BackColor.ToHSL();
            colorBarX1.Value = hSL.H;
            colorBarX2.Value = (int)(hSL.S * 100);
            colorBarX3.Value = (int)(hSL.L * 100);

            trackBarX5.Value = 200;

            canChangeColor = true;
            ColorizationColor_pick.BackColor = hSL.ToRGB();

            windowsDesktop1.TitlebarColor_Active = hSL.ToRGB();
            windowsDesktop1.TitlebarColor_Inactive = hSL.ToRGB();
        }

        private void colorItem16_Click(object sender, EventArgs e)
        {
            canChangeColor = false;

            HSL hSL = ((ColorItem)sender).BackColor.ToHSL();
            colorBarX1.Value = hSL.H;
            colorBarX2.Value = (int)(hSL.S * 100);
            colorBarX3.Value = (int)(hSL.L * 100);

            trackBarX5.Value = 200;

            canChangeColor = true;
            ColorizationColor_pick.BackColor = hSL.ToRGB();

            windowsDesktop1.TitlebarColor_Active = hSL.ToRGB();
            windowsDesktop1.TitlebarColor_Inactive = hSL.ToRGB();
        }

        private void colorItem14_Click(object sender, EventArgs e)
        {
            canChangeColor = false;

            HSL hSL = ((ColorItem)sender).BackColor.ToHSL();
            colorBarX1.Value = hSL.H;
            colorBarX2.Value = (int)(hSL.S * 100);
            colorBarX3.Value = (int)(hSL.L * 100);

            trackBarX5.Value = 200;

            canChangeColor = true;
            ColorizationColor_pick.BackColor = hSL.ToRGB();

            windowsDesktop1.TitlebarColor_Active = hSL.ToRGB();
            windowsDesktop1.TitlebarColor_Inactive = hSL.ToRGB();
        }

        private void colorItem15_Click(object sender, EventArgs e)
        {
            canChangeColor = false;

            HSL hSL = ((ColorItem)sender).BackColor.ToHSL();
            colorBarX1.Value = hSL.H;
            colorBarX2.Value = (int)(hSL.S * 100);
            colorBarX3.Value = (int)(hSL.L * 100);

            trackBarX5.Value = 200;

            canChangeColor = true;
            ColorizationColor_pick.BackColor = hSL.ToRGB();

            windowsDesktop1.TitlebarColor_Active = hSL.ToRGB();
            windowsDesktop1.TitlebarColor_Inactive = hSL.ToRGB();
        }

        private void colorItem13_Click(object sender, EventArgs e)
        {
            canChangeColor = false;

            HSL hSL = ((ColorItem)sender).BackColor.ToHSL();
            colorBarX1.Value = hSL.H;
            colorBarX2.Value = (int)(hSL.S * 100);
            colorBarX3.Value = (int)(hSL.L * 100);

            trackBarX5.Value = 200;

            canChangeColor = true;
            ColorizationColor_pick.BackColor = hSL.ToRGB();

            windowsDesktop1.TitlebarColor_Active = hSL.ToRGB();
            windowsDesktop1.TitlebarColor_Inactive = hSL.ToRGB();
        }

        private void colorItem12_Click(object sender, EventArgs e)
        {
            canChangeColor = false;

            HSL hSL = ((ColorItem)sender).BackColor.ToHSL();
            colorBarX1.Value = hSL.H;
            colorBarX2.Value = (int)(hSL.S * 100);
            colorBarX3.Value = (int)(hSL.L * 100);

            trackBarX5.Value = 200;

            canChangeColor = true;
            ColorizationColor_pick.BackColor = hSL.ToRGB();

            windowsDesktop1.TitlebarColor_Active = hSL.ToRGB();
            windowsDesktop1.TitlebarColor_Inactive = hSL.ToRGB();
        }

        private void colorItem10_Click(object sender, EventArgs e)
        {
            canChangeColor = false;

            HSL hSL = ((ColorItem)sender).BackColor.ToHSL();
            colorBarX1.Value = hSL.H;
            colorBarX2.Value = (int)(hSL.S * 100);
            colorBarX3.Value = (int)(hSL.L * 100);

            trackBarX5.Value = 200;

            canChangeColor = true;
            ColorizationColor_pick.BackColor = hSL.ToRGB();

            windowsDesktop1.TitlebarColor_Active = hSL.ToRGB();
            windowsDesktop1.TitlebarColor_Inactive = hSL.ToRGB();
        }

        private void colorItem11_Click(object sender, EventArgs e)
        {
            canChangeColor = false;

            HSL hSL = ((ColorItem)sender).BackColor.ToHSL();
            colorBarX1.Value = hSL.H;
            colorBarX2.Value = (int)(hSL.S * 100);
            colorBarX3.Value = (int)(hSL.L * 100);

            trackBarX5.Value = 200;

            canChangeColor = true;
            ColorizationColor_pick.BackColor = hSL.ToRGB();

            windowsDesktop1.TitlebarColor_Active = hSL.ToRGB();
            windowsDesktop1.TitlebarColor_Inactive = hSL.ToRGB();
        }

        private void colorItem9_Click(object sender, EventArgs e)
        {
            canChangeColor = false;

            HSL hSL = ((ColorItem)sender).BackColor.ToHSL();
            colorBarX1.Value = hSL.H;
            colorBarX2.Value = (int)(hSL.S * 100);
            colorBarX3.Value = (int)(hSL.L * 100);

            trackBarX5.Value = 200;

            canChangeColor = true;
            ColorizationColor_pick.BackColor = hSL.ToRGB();

            windowsDesktop1.TitlebarColor_Active = hSL.ToRGB();
            windowsDesktop1.TitlebarColor_Inactive = hSL.ToRGB();
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
                windowsDesktop1.TitlebarColor_Inactive = hSL.ToRGB();
            }
        }

        private void ColorizationColor_pick_Click(object sender, EventArgs e)
        {
            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                Forms.SubMenu.ShowMenu((UI.Controllers.ColorItem)sender);
                if (ColorClipboard.Event == ColorClipboard.MenuEvent.Cut | ColorClipboard.Event == ColorClipboard.MenuEvent.Paste | ColorClipboard.Event == ColorClipboard.MenuEvent.Override)
                {
                    windowsDesktop1.TitlebarColor_Active = ((UI.Controllers.ColorItem)sender).BackColor;
                    windowsDesktop1.TitlebarColor_Inactive = ((UI.Controllers.ColorItem)sender).BackColor;
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
                windowsDesktop1.Win7ColorBal = (int)(ColorizationColorBalance_bar.Value / 255f * 100f);
                windowsDesktop1.Win7Alpha = 100 - (int)(ColorizationColorBalance_bar.Value / 255f * 100f);
            }
        }

        private void theme_classic_CheckedChanged(object sender, EventArgs e)
        {
            if (IsShown && theme_classic.Checked)
            {
                windowsDesktop1.Windows_7_8_Theme = Theme.Structures.Windows7.Themes.Classic;
            }
        }

        private void theme_basic_CheckedChanged(object sender, EventArgs e)
        {
            if (IsShown && theme_basic.Checked)
            {
                windowsDesktop1.Windows_7_8_Theme = Theme.Structures.Windows7.Themes.Basic;
            }
        }

        private void theme_aeroopaque_CheckedChanged(object sender, EventArgs e)
        {
            if (IsShown && theme_aeroopaque.Checked)
            {
                windowsDesktop1.Windows_7_8_Theme = Theme.Structures.Windows7.Themes.AeroOpaque;
            }
        }

        private void theme_aero_CheckedChanged(object sender, EventArgs e)
        {
            if (IsShown && theme_aero.Checked)
            {
                windowsDesktop1.Windows_7_8_Theme = Theme.Structures.Windows7.Themes.Aero;
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

        private void trackBarX5_ValueChanged(object sender, EventArgs e)
        {
            if (IsShown) ColorizationColorBalance_bar.Value = ((TrackBarX)sender).Value;
        }

        private void Pickers_DragDrop(object sender, DragEventArgs e)
        {
            if (((ColorItem)sender).AllowDrop)
            {
                windowsDesktop1.TitlebarColor_Active = ColorizationColor_pick.BackColor;
                windowsDesktop1.TitlebarColor_Inactive = ColorizationColor_pick.BackColor;
            }
        }

        private void WinVistaColors_HelpButtonClicked(object sender, System.ComponentModel.CancelEventArgs e)
        {
            System.Diagnostics.Process.Start(Links.Wiki.WinVistaColors);
        }
    }
}