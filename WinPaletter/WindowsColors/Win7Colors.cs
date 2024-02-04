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

namespace WinPaletter.WindowsColors
{
    public partial class Win7Colors : AspectsTemplate
    {
        bool canChangeColor = true;

        public Win7Colors()
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

                try { TMx.Windows7.Apply(TMx); } catch { }
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
                AspectName = string.Format(Program.Lang.WindowsColors, OS.Name),
                Enabled = Program.TM.Windows7.Enabled,
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
            AspectEnabled = TM.Windows7.Enabled;
            ColorizationColor_pick.BackColor = TM.Windows7.ColorizationColor;
            ColorizationAfterglow_pick.BackColor = TM.Windows7.ColorizationAfterglow;
            ColorizationColorBalance_bar.Value = TM.Windows7.ColorizationColorBalance;
            ColorizationAfterglowBalance_bar.Value = TM.Windows7.ColorizationAfterglowBalance;
            ColorizationBlurBalance_bar.Value = TM.Windows7.ColorizationBlurBalance;
            ColorizationGlassReflectionIntensity_bar.Value = TM.Windows7.ColorizationGlassReflectionIntensity;
            EnableAeroPeek_toggle.Checked = TM.Windows7.EnableAeroPeek;
            AlwaysHibernateThumbnails_Toggle.Checked = TM.Windows7.AlwaysHibernateThumbnails;
            switch (TM.Windows7.Theme)
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

            checkBox1.Checked = TM.Windows7.Theme == Theme.Structures.Windows7.Themes.Aero;

            windowsDesktop1.HockedTM = TM;
            windowsDesktop1.LoadFromTM(TM);

            RefreshDWM();
        }

        public void ApplyToTM(Theme.Manager TM)
        {
            TM.Windows7.Enabled = AspectEnabled;
            TM.Windows7.ColorizationColor = ColorizationColor_pick.BackColor;
            TM.Windows7.ColorizationAfterglow = ColorizationAfterglow_pick.BackColor;
            TM.Windows7.ColorizationColorBalance = ColorizationColorBalance_bar.Value;
            TM.Windows7.ColorizationAfterglowBalance = ColorizationAfterglowBalance_bar.Value;
            TM.Windows7.ColorizationBlurBalance = ColorizationBlurBalance_bar.Value;
            TM.Windows7.ColorizationGlassReflectionIntensity = ColorizationGlassReflectionIntensity_bar.Value;
            TM.Windows7.EnableAeroPeek = EnableAeroPeek_toggle.Checked;
            TM.Windows7.AlwaysHibernateThumbnails = AlwaysHibernateThumbnails_Toggle.Checked;

            if (theme_aero.Checked)
            {
                TM.Windows7.Theme = Theme.Structures.Windows7.Themes.Aero;
            }
            else if (theme_aeroopaque.Checked)
            {
                TM.Windows7.Theme = Theme.Structures.Windows7.Themes.AeroOpaque;
            }
            else if (theme_basic.Checked)
            {
                TM.Windows7.Theme = Theme.Structures.Windows7.Themes.Basic;
            }
            else if (theme_classic.Checked)
            {
                TM.Windows7.Theme = Theme.Structures.Windows7.Themes.Classic;
            }

        }

        public void ApplyDefaultTMValues()
        {
            using (Theme.Manager DefTM = Theme.Default.Get(WindowStyle.W7))
            {
                ColorizationColor_pick.DefaultBackColor = DefTM.Windows7.ColorizationColor;
                ColorizationAfterglow_pick.DefaultBackColor = DefTM.Windows7.ColorizationAfterglow;
                ColorizationColorBalance_bar.DefaultValue = DefTM.Windows7.ColorizationColorBalance;
                ColorizationAfterglowBalance_bar.DefaultValue = DefTM.Windows7.ColorizationAfterglowBalance;
                ColorizationBlurBalance_bar.DefaultValue = DefTM.Windows7.ColorizationBlurBalance;
                ColorizationGlassReflectionIntensity_bar.DefaultValue = DefTM.Windows7.ColorizationGlassReflectionIntensity;
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dlg = new() { Filter = Program.Filters.PNG, Title = Program.Lang.Filter_SavePNG })
            {
                if (dlg.ShowDialog() == DialogResult.OK) windowsDesktop1.ToBitmap().Save(dlg.FileName);
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

            ColorizationColorBalance_bar.Value = 8;
            ColorizationAfterglowBalance_bar.Value = 43;
            ColorizationBlurBalance_bar.Value = 49;
            ColorizationGlassReflectionIntensity_bar.Value = 50;

            canChangeColor = true;
            ColorizationColor_pick.BackColor = hSL.ToRGB();
            ColorizationAfterglow_pick.BackColor = hSL.ToRGB();
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

            ColorizationColorBalance_bar.Value = 56;
            ColorizationAfterglowBalance_bar.Value = 11;
            ColorizationBlurBalance_bar.Value = 33;
            ColorizationGlassReflectionIntensity_bar.Value = 50;

            canChangeColor = true;
            ColorizationColor_pick.BackColor = hSL.ToRGB();
            ColorizationAfterglow_pick.BackColor = hSL.ToRGB();
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

            ColorizationColorBalance_bar.Value = 5;
            ColorizationAfterglowBalance_bar.Value = 45;
            ColorizationBlurBalance_bar.Value = 50;
            ColorizationGlassReflectionIntensity_bar.Value = 50;

            canChangeColor = true;
            ColorizationColor_pick.BackColor = hSL.ToRGB();
            ColorizationAfterglow_pick.BackColor = hSL.ToRGB();
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

            ColorizationColorBalance_bar.Value = 24;
            ColorizationAfterglowBalance_bar.Value = 32;
            ColorizationBlurBalance_bar.Value = 44;
            ColorizationGlassReflectionIntensity_bar.Value = 50;

            canChangeColor = true;
            ColorizationColor_pick.BackColor = hSL.ToRGB();
            ColorizationAfterglow_pick.BackColor = hSL.ToRGB();
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

            ColorizationColorBalance_bar.Value = 56;
            ColorizationAfterglowBalance_bar.Value = 11;
            ColorizationBlurBalance_bar.Value = 33;
            ColorizationGlassReflectionIntensity_bar.Value = 50;

            canChangeColor = true;
            ColorizationColor_pick.BackColor = hSL.ToRGB();
            ColorizationAfterglow_pick.BackColor = hSL.ToRGB();
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

            ColorizationColorBalance_bar.Value = 5;
            ColorizationAfterglowBalance_bar.Value = 45;
            ColorizationBlurBalance_bar.Value = 50;
            ColorizationGlassReflectionIntensity_bar.Value = 50;

            canChangeColor = true;
            ColorizationColor_pick.BackColor = hSL.ToRGB();
            ColorizationAfterglow_pick.BackColor = hSL.ToRGB();
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

            ColorizationColorBalance_bar.Value = 5;
            ColorizationAfterglowBalance_bar.Value = 35;
            ColorizationBlurBalance_bar.Value = 60;
            ColorizationGlassReflectionIntensity_bar.Value = 50;

            canChangeColor = true;
            ColorizationColor_pick.BackColor = hSL.ToRGB();
            ColorizationAfterglow_pick.BackColor = hSL.ToRGB();
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

            ColorizationColorBalance_bar.Value = 24;
            ColorizationAfterglowBalance_bar.Value = 32;
            ColorizationBlurBalance_bar.Value = 44;
            ColorizationGlassReflectionIntensity_bar.Value = 50;

            canChangeColor = true;
            ColorizationColor_pick.BackColor = hSL.ToRGB();
            ColorizationAfterglow_pick.BackColor = hSL.ToRGB();
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

            ColorizationColorBalance_bar.Value = 5;
            ColorizationAfterglowBalance_bar.Value = 45;
            ColorizationBlurBalance_bar.Value = 50;
            ColorizationGlassReflectionIntensity_bar.Value = 50;

            canChangeColor = true;
            ColorizationColor_pick.BackColor = hSL.ToRGB();
            ColorizationAfterglow_pick.BackColor = hSL.ToRGB();
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

            ColorizationColorBalance_bar.Value = 12;
            ColorizationAfterglowBalance_bar.Value = 40;
            ColorizationBlurBalance_bar.Value = 48;
            ColorizationGlassReflectionIntensity_bar.Value = 50;

            canChangeColor = true;
            ColorizationColor_pick.BackColor = hSL.ToRGB();
            ColorizationAfterglow_pick.BackColor = hSL.ToRGB();
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

            ColorizationColorBalance_bar.Value = 29;
            ColorizationAfterglowBalance_bar.Value = 29;
            ColorizationBlurBalance_bar.Value = 42;
            ColorizationGlassReflectionIntensity_bar.Value = 50;

            canChangeColor = true;
            ColorizationColor_pick.BackColor = hSL.ToRGB();
            ColorizationAfterglow_pick.BackColor = hSL.ToRGB();
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

            ColorizationColorBalance_bar.Value = 5;
            ColorizationAfterglowBalance_bar.Value = 34;
            ColorizationBlurBalance_bar.Value = 61;
            ColorizationGlassReflectionIntensity_bar.Value = 50;

            canChangeColor = true;
            ColorizationColor_pick.BackColor = hSL.ToRGB();
            ColorizationAfterglow_pick.BackColor = hSL.ToRGB();
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

            ColorizationColorBalance_bar.Value = 5;
            ColorizationAfterglowBalance_bar.Value = 45;
            ColorizationBlurBalance_bar.Value = 50;
            ColorizationGlassReflectionIntensity_bar.Value = 50;

            canChangeColor = true;
            ColorizationColor_pick.BackColor = hSL.ToRGB();
            ColorizationAfterglow_pick.BackColor = hSL.ToRGB();
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

            ColorizationColorBalance_bar.Value = 56;
            ColorizationAfterglowBalance_bar.Value = 11;
            ColorizationBlurBalance_bar.Value = 33;
            ColorizationGlassReflectionIntensity_bar.Value = 50;

            canChangeColor = true;
            ColorizationColor_pick.BackColor = hSL.ToRGB();
            ColorizationAfterglow_pick.BackColor = hSL.ToRGB();
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

            ColorizationColorBalance_bar.Value = 24;
            ColorizationAfterglowBalance_bar.Value = 32;
            ColorizationBlurBalance_bar.Value = 44;
            ColorizationGlassReflectionIntensity_bar.Value = 50;

            canChangeColor = true;
            ColorizationColor_pick.BackColor = hSL.ToRGB();
            ColorizationAfterglow_pick.BackColor = hSL.ToRGB();
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

            ColorizationColorBalance_bar.Value = 5;
            ColorizationAfterglowBalance_bar.Value = 35;
            ColorizationBlurBalance_bar.Value = 60;
            ColorizationGlassReflectionIntensity_bar.Value = 50;

            canChangeColor = true;
            ColorizationColor_pick.BackColor = hSL.ToRGB();
            ColorizationAfterglow_pick.BackColor = hSL.ToRGB();
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
                ColorizationAfterglow_pick.BackColor = hSL.ToRGB();
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

        private void ColorizationAfterglow_pick_Click(object sender, EventArgs e)
        {
            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                Forms.SubMenu.ShowMenu((UI.Controllers.ColorItem)sender);
                if (ColorClipboard.Event == ColorClipboard.MenuEvent.Cut | ColorClipboard.Event == ColorClipboard.MenuEvent.Paste | ColorClipboard.Event == ColorClipboard.MenuEvent.Override)
                {
                    windowsDesktop1.AfterGlowColor_Active = ((UI.Controllers.ColorItem)sender).BackColor;
                    windowsDesktop1.AfterGlowColor_Inactive = ((UI.Controllers.ColorItem)sender).BackColor;
                }
                return;
            }

            ColorItem colorItem = (ColorItem)sender;
            Dictionary<Control, string[]> CList = new()
            {
                { colorItem, new string[] { nameof(colorItem.BackColor) } },
                { windowsDesktop1, new string[] { nameof(windowsDesktop1.AfterGlowColor_Active), nameof(windowsDesktop1.AfterGlowColor_Inactive) } }
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

        private void ColorizationAfterglowBalance_bar_ValueChanged(object sender, EventArgs e)
        {
            if (IsShown)
            {
                RefreshDWM();
                windowsDesktop1.Win7GlowBal = ColorizationAfterglowBalance_bar.Value;
            }
        }

        private void ColorizationGlassReflectionIntensity_bar_ValueChanged(object sender, EventArgs e)
        {
            if (IsShown)
            {
                RefreshDWM();
                windowsDesktop1.Win7Noise = ColorizationGlassReflectionIntensity_bar.Value;
            }
        }

        private void theme_classic_CheckedChanged(object sender, EventArgs e)
        {
            if (IsShown && theme_classic.Checked)
            {
                RefreshDWM();
                windowsDesktop1.WindowsTheme = Theme.Structures.Windows7.Themes.Classic;
            }
        }

        private void theme_basic_CheckedChanged(object sender, EventArgs e)
        {
            if (IsShown && theme_basic.Checked)
            {
                RefreshDWM();
                windowsDesktop1.WindowsTheme = Theme.Structures.Windows7.Themes.Basic;
            }
        }

        private void theme_aeroopaque_CheckedChanged(object sender, EventArgs e)
        {
            if (IsShown && theme_aeroopaque.Checked)
            {
                RefreshDWM();
                windowsDesktop1.WindowsTheme = Theme.Structures.Windows7.Themes.AeroOpaque;
            }
        }

        private void theme_aero_CheckedChanged(object sender, EventArgs e)
        {
            if (IsShown && theme_aero.Checked)
            {
                RefreshDWM();
                windowsDesktop1.WindowsTheme = Theme.Structures.Windows7.Themes.Aero;
            }
        }

        private void ColorizationBlurBalance_bar_ValueChanged(object sender, EventArgs e)
        {
            if (IsShown)
            {
                RefreshDWM();
                windowsDesktop1.Win7Alpha = ColorizationBlurBalance_bar.Value;
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

            else if (e.PropertyName.ToLower() == nameof(windowsDesktop1.AfterGlowColor_Active).ToLower())
            {
                Dictionary<Control, string[]> CList = new()
                {
                    { windowsDesktop1, new string[] { nameof(windowsDesktop1.AfterGlowColor_Active), nameof(windowsDesktop1.AfterGlowColor_Inactive) } },
                    { ColorizationAfterglow_pick, new string[] { nameof(ColorizationAfterglow_pick.BackColor) } }
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
            if (IsShown)
            {
                if (((TrackBarX)sender).Value < 40)
                {
                    ColorizationColorBalance_bar.Value = 5;
                    ColorizationAfterglowBalance_bar.Value = Math.Max(((TrackBarX)sender).Value, 5);
                }
                else
                {
                    ColorizationColorBalance_bar.Value = ((TrackBarX)sender).Value - 15;
                    ColorizationAfterglowBalance_bar.Value = 105 - ((TrackBarX)sender).Value;
                }
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
            if (Program.Settings.Miscellaneous.Win7LivePreview && AspectEnabled)
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

                                        clrAfterGlow = (uint)ColorizationAfterglow_pick.BackColor.ToArgb(),
                                        clrAfterGlowBalance = (uint)ColorizationAfterglowBalance_bar.Value,

                                        clrBlurBalance = (uint)ColorizationBlurBalance_bar.Value,
                                        clrGlassReflectionIntensity = (uint)ColorizationGlassReflectionIntensity_bar.Value,
                                        fOpaque = theme_aeroopaque.Checked,
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
                windowsDesktop1.AfterGlowColor_Active = ColorizationAfterglow_pick.BackColor;
                windowsDesktop1.AfterGlowColor_Inactive = ColorizationAfterglow_pick.BackColor;
            }
        }
    }
}