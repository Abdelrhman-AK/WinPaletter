﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WinPaletter.Templates;
using WinPaletter.Theme;
using WinPaletter.UI.Controllers;
using static WinPaletter.PreviewHelpers;
using static WinPaletter.TypesExtensions.ColorsExtensions;

namespace WinPaletter
{
    public partial class Win10Colors : AspectsTemplate
    {
        public Win10Colors()
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
                TMx.Windows10.Apply("10");
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

        private void Win10Colors_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.Settings.AspectsControl.WinColors_Advanced = AdvancedMode;
            Program.Settings.AspectsControl.Save();
        }

        private void Win10Colors_Load(object sender, EventArgs e)
        {
            DesignerData data = new(this)
            {
                AspectName = string.Format(Program.Lang.Strings.Aspects.WinTheme, OS.Name),
                Enabled = Program.TM.Windows10.Enabled,
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

            ToolStripMenuItem item = new(string.Format(Program.Lang.Strings.General.CopycatFrom, Program.Lang.Strings.Windows.W11));
            item.Click += Item_Click;
            easy_generator.Menu.Items.Add(item);
        }

        private void Item_Click(object sender, EventArgs e)
        {
            // Copycat from Windows 11 colors
            using (Manager TMx = new(Manager.Source.Empty))
            {
                TMx.Windows10 = (Theme.Structures.Windows10x)Program.TM.Windows11.Clone();
                LoadFromTM((Manager)TMx.Clone());

                Program.ToolTip.Show(easy_generator, Program.Lang.Strings.General.Done, string.Empty, null, new Point(2, easy_generator.Height + 2));
            }
        }

        public void LoadFromTM(Manager TM)
        {
            AspectEnabled = TM.Windows10.Enabled;
            theme_aero.Checked = TM.Windows10.Theme == Theme.Structures.Windows10x.Themes.Aero;
            theme_aerolite.Checked = TM.Windows10.Theme == Theme.Structures.Windows10x.Themes.AeroLite;
            theme_skip.Checked = TM.Windows10.Theme == Theme.Structures.Windows10x.Themes.Skip;

            WinMode_Toggle.Checked = !TM.Windows10.WinMode_Light;
            AppMode_Toggle.Checked = !TM.Windows10.AppMode_Light;
            Transparency_Toggle.Checked = TM.Windows10.Transparency;
            ShowAccentOnTitlebarAndBorders_Toggle.Checked = TM.Windows10.ApplyAccentOnTitlebars;
            Accent_None.Checked = TM.Windows10.ApplyAccentOnTaskbar == Theme.Structures.Windows10x.AccentTaskbarLevels.None;
            Accent_Taskbar.Checked = TM.Windows10.ApplyAccentOnTaskbar == Theme.Structures.Windows10x.AccentTaskbarLevels.Taskbar;
            Accent_StartTaskbar.Checked = TM.Windows10.ApplyAccentOnTaskbar == Theme.Structures.Windows10x.AccentTaskbarLevels.Taskbar_Start_AC;
            IncreastTBTransparency.Checked = TM.Windows10.IncreaseTBTransparency;
            TBBlur.Checked = TM.Windows10.TB_Blur;

            easy_appmode_dark.Checked = !TM.Windows10.AppMode_Light;
            easy_appmode_light.Checked = TM.Windows10.AppMode_Light;
            easy_winmode_dark.Checked = !TM.Windows10.WinMode_Light;
            easy_winmode_light.Checked = TM.Windows10.WinMode_Light;
            easy_transparent.Checked = TM.Windows10.Transparency;
            easy_opaque.Checked = !TM.Windows10.Transparency;
            easy_colortitle.Checked = TM.Windows10.ApplyAccentOnTitlebars;
            easy_nocolortitle.Checked = !TM.Windows10.ApplyAccentOnTitlebars;
            easy_level_none.Checked = TM.Windows10.ApplyAccentOnTaskbar == Theme.Structures.Windows10x.AccentTaskbarLevels.None;
            easy_level_tb.Checked = TM.Windows10.ApplyAccentOnTaskbar == Theme.Structures.Windows10x.AccentTaskbarLevels.Taskbar;
            easy_level_all.Checked = TM.Windows10.ApplyAccentOnTaskbar == Theme.Structures.Windows10x.AccentTaskbarLevels.Taskbar_Start_AC;

            TActive.BackColor = TM.Windows10.Titlebar_Active;
            TInactive.BackColor = TM.Windows10.Titlebar_Inactive;
            C7.BackColor = TM.Windows10.StartMenu_Accent;
            C6.BackColor = TM.Windows10.Color_Index2;
            C8.BackColor = TM.Windows10.Color_Index6;
            C3.BackColor = TM.Windows10.Color_Index1;
            C4.BackColor = TM.Windows10.Color_Index4;
            C1.BackColor = TM.Windows10.Color_Index5;
            C2.BackColor = TM.Windows10.Color_Index0;
            C5.BackColor = TM.Windows10.Color_Index3;
            C9.BackColor = TM.Windows10.Color_Index7;

            windowsDesktop1.HookedTM = TM;
            windowsDesktop1.LoadFromTM(TM);
            UpdateLegends();
        }

        public void ApplyToTM(Manager TM)
        {
            TM.Windows10.Enabled = AspectEnabled;

            if (theme_skip.Checked) TM.Windows10.Theme = Theme.Structures.Windows10x.Themes.Skip;
            else if (theme_aero.Checked) TM.Windows10.Theme = Theme.Structures.Windows10x.Themes.Aero;
            else if (theme_aerolite.Checked) TM.Windows10.Theme = Theme.Structures.Windows10x.Themes.AeroLite;

            TM.Windows10.WinMode_Light = !WinMode_Toggle.Checked;
            TM.Windows10.AppMode_Light = !AppMode_Toggle.Checked;
            TM.Windows10.Transparency = Transparency_Toggle.Checked;
            TM.Windows10.ApplyAccentOnTitlebars = ShowAccentOnTitlebarAndBorders_Toggle.Checked;
            TM.Windows10.IncreaseTBTransparency = IncreastTBTransparency.Checked;
            TM.Windows10.TB_Blur = TBBlur.Checked;

            if (Accent_None.Checked) TM.Windows10.ApplyAccentOnTaskbar = Theme.Structures.Windows10x.AccentTaskbarLevels.None;
            else if (Accent_Taskbar.Checked) TM.Windows10.ApplyAccentOnTaskbar = Theme.Structures.Windows10x.AccentTaskbarLevels.Taskbar;
            else if (Accent_StartTaskbar.Checked) TM.Windows10.ApplyAccentOnTaskbar = Theme.Structures.Windows10x.AccentTaskbarLevels.Taskbar_Start_AC;

            TM.Windows10.Titlebar_Active = TActive.BackColor;
            TM.Windows10.Titlebar_Inactive = TInactive.BackColor;
            TM.Windows10.StartMenu_Accent = C7.BackColor;
            TM.Windows10.Color_Index2 = C6.BackColor;
            TM.Windows10.Color_Index6 = C8.BackColor;
            TM.Windows10.Color_Index1 = C3.BackColor;
            TM.Windows10.Color_Index4 = C4.BackColor;
            TM.Windows10.Color_Index5 = C1.BackColor;
            TM.Windows10.Color_Index0 = C2.BackColor;
            TM.Windows10.Color_Index3 = C5.BackColor;
            TM.Windows10.Color_Index7 = C9.BackColor;
        }

        public void ApplyDefaultTMValues()
        {
            using (Manager DefTM = Theme.Default.Get(WindowStyle.W10))
            {
                TActive.DefaultBackColor = DefTM.Windows10.Titlebar_Active;
                TInactive.DefaultBackColor = DefTM.Windows10.Titlebar_Inactive;
                C7.DefaultBackColor = DefTM.Windows10.StartMenu_Accent;
                C6.DefaultBackColor = DefTM.Windows10.Color_Index2;
                C8.DefaultBackColor = DefTM.Windows10.Color_Index6;
                C3.DefaultBackColor = DefTM.Windows10.Color_Index1;
                C4.DefaultBackColor = DefTM.Windows10.Color_Index4;
                C1.DefaultBackColor = DefTM.Windows10.Color_Index5;
                C2.DefaultBackColor = DefTM.Windows10.Color_Index0;
                C5.DefaultBackColor = DefTM.Windows10.Color_Index3;
                C9.DefaultBackColor = DefTM.Windows10.Color_Index7;

                easy_activetitle.DefaultBackColor = TActive.DefaultBackColor;
                easy_inactivetitle.DefaultBackColor = TInactive.DefaultBackColor;
                e1.DefaultBackColor = C1.DefaultBackColor;
                e2.DefaultBackColor = C2.DefaultBackColor;
                e3.DefaultBackColor = C3.DefaultBackColor;
                e4.DefaultBackColor = C4.DefaultBackColor;
                e5.DefaultBackColor = C5.DefaultBackColor;
                e6.DefaultBackColor = C6.DefaultBackColor;
                e7.DefaultBackColor = C7.DefaultBackColor;
                e8.DefaultBackColor = C8.DefaultBackColor;
                e9.DefaultBackColor = C9.DefaultBackColor;
            }
        }

        private void UpdateLegends()
        {
            using (Manager TMx = new(Manager.Source.Empty))
            {
                ApplyToTM(TMx);
                ApplyWin10xLegends(TMx, WindowStyle.W10, lbl1, lbl2, lbl3, lbl4, lbl5, lbl6, lbl7, lbl8, lbl9, pic1, pic2, pic3, pic4, pic5, pic6, pic7, pic8, pic9);
            }
        }


        private void TActive_pick_Click(object sender, EventArgs e)
        {
            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                Forms.SubMenu.ShowMenu((ColorItem)sender);
                if (ColorClipboard.Event == ColorClipboard.MenuEvent.Cut | ColorClipboard.Event == ColorClipboard.MenuEvent.Paste | ColorClipboard.Event == ColorClipboard.MenuEvent.Override)
                {
                    windowsDesktop1.TitlebarColor_Active = ((ColorItem)sender).BackColor;
                }
                return;
            }

            ColorItem colorItem = (ColorItem)sender;
            Dictionary<Control, string[]> CList = new()
            {
                { colorItem, new string[] { nameof(colorItem.BackColor) } },
                { windowsDesktop1, new string[] { nameof(windowsDesktop1.TitlebarColor_Active) } }
            };

            Color C = Forms.ColorPickerDlg.Pick(CList);

            colorItem.BackColor = C;
            colorItem.Invalidate();

            CList.Clear();
        }

        private void TInactive_pick_Click(object sender, EventArgs e)
        {
            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                Forms.SubMenu.ShowMenu((ColorItem)sender);
                if (ColorClipboard.Event == ColorClipboard.MenuEvent.Cut | ColorClipboard.Event == ColorClipboard.MenuEvent.Paste | ColorClipboard.Event == ColorClipboard.MenuEvent.Override)
                {
                    windowsDesktop1.TitlebarColor_Inactive = ((ColorItem)sender).BackColor;
                }
                return;
            }

            ColorItem colorItem = (ColorItem)sender;
            Dictionary<Control, string[]> CList = new()
            {
                { colorItem, new string[] { nameof(colorItem.BackColor) } },
                { windowsDesktop1, new string[] { nameof(windowsDesktop1.TitlebarColor_Inactive) } }
            };

            Color C = Forms.ColorPickerDlg.Pick(CList);

            colorItem.BackColor = C;
            colorItem.Invalidate();

            CList.Clear();
        }

        private void WinMode_Toggle_CheckedChanged(object sender, EventArgs e)
        {
            bool _checked = false;

            if (sender is UI.WP.Toggle)
            {
                _checked = ((UI.WP.Toggle)sender).Checked;
            }
            else if (sender is UI.WP.RadioImage)
            {
                _checked = ((UI.WP.RadioImage)sender).Checked;
            }

            if (IsShown)
            {
                windowsDesktop1.DarkMode_Win = _checked;
                UpdateLegends();
            }

            easy_winmode_dark.Checked = _checked;
            easy_winmode_light.Checked = !_checked;
        }

        private void AppMode_Toggle_CheckedChanged(object sender, EventArgs e)
        {
            bool _checked = false;

            if (sender is UI.WP.Toggle)
            {
                _checked = ((UI.WP.Toggle)sender).Checked;
            }
            else if (sender is UI.WP.RadioImage)
            {
                _checked = ((UI.WP.RadioImage)sender).Checked;
            }

            if (IsShown)
            {
                windowsDesktop1.DarkMode_App = _checked;
            }

            easy_appmode_dark.Checked = _checked;
            easy_appmode_light.Checked = !_checked;

            alertBox1.Visible = theme_aerolite.Checked && AppMode_Toggle.Checked;
        }

        private void Transparency_Toggle_CheckedChanged(object sender, EventArgs e)
        {
            bool _checked = false;

            if (sender is UI.WP.Toggle)
            {
                _checked = ((UI.WP.Toggle)sender).Checked;
            }
            else if (sender is UI.WP.RadioImage)
            {
                _checked = ((UI.WP.RadioImage)sender).Checked;
            }

            if (IsShown)
            {
                windowsDesktop1.Transparency = _checked;
                UpdateLegends();
            }

            easy_transparent.Checked = _checked;
            easy_opaque.Checked = !_checked;
        }

        private void AccentOnTitlebar_CheckedChanged(object sender, EventArgs e)
        {
            bool _checked = false;

            if (sender is UI.WP.Toggle)
            {
                _checked = ((UI.WP.Toggle)sender).Checked;
            }
            else if (sender is UI.WP.RadioImage)
            {
                _checked = ((UI.WP.RadioImage)sender).Checked;
            }

            if (IsShown)
            {
                windowsDesktop1.TitlebarColor_Enabled = _checked;
            }

            easy_colortitle.Checked = _checked;
            easy_nocolortitle.Checked = !_checked;
        }

        private void Accent_None_CheckedChanged(object sender, EventArgs e)
        {
            if (IsShown & ((UI.WP.RadioImage)sender).Checked)
            {
                windowsDesktop1.AccentLevel = Theme.Structures.Windows10x.AccentTaskbarLevels.None;
            }

            easy_level_none.Checked = ((UI.WP.RadioImage)sender).Checked;
            Accent_None.Checked = ((UI.WP.RadioImage)sender).Checked;

            UpdateLegends();
        }

        private void Accent_Taskbar_CheckedChanged(object sender, EventArgs e)
        {
            if (IsShown & ((UI.WP.RadioImage)sender).Checked)
            {
                windowsDesktop1.AccentLevel = Theme.Structures.Windows10x.AccentTaskbarLevels.Taskbar;
            }

            easy_level_tb.Checked = ((UI.WP.RadioImage)sender).Checked;
            Accent_Taskbar.Checked = ((UI.WP.RadioImage)sender).Checked;

            UpdateLegends();
        }

        private void Accent_StartTaskbar_CheckedChanged(object sender, EventArgs e)
        {
            if (IsShown & ((UI.WP.RadioImage)sender).Checked)
            {
                windowsDesktop1.AccentLevel = Theme.Structures.Windows10x.AccentTaskbarLevels.Taskbar_Start_AC;
            }

            easy_level_all.Checked = ((UI.WP.RadioImage)sender).Checked;
            Accent_StartTaskbar.Checked = ((UI.WP.RadioImage)sender).Checked;

            UpdateLegends();
        }


        private void TActive_BackColorChanged(object sender, EventArgs e)
        {
            easy_activetitle.BackColor = TActive.BackColor;
        }

        private void TInactive_BackColorChanged(object sender, EventArgs e)
        {
            easy_inactivetitle.BackColor = TInactive.BackColor;
        }


        private void ColorPicker_DragDrop(object sender, DragEventArgs e)
        {
            if (((ColorItem)sender).AllowDrop)
            {
                windowsDesktop1.Color1 = C1.BackColor;
                windowsDesktop1.Color2 = C2.BackColor;
                windowsDesktop1.Color3 = C3.BackColor;
                windowsDesktop1.Color4 = C4.BackColor;
                windowsDesktop1.Color5 = C5.BackColor;
                windowsDesktop1.Color6 = C6.BackColor;
                windowsDesktop1.Color7 = C7.BackColor;
                windowsDesktop1.Color8 = C8.BackColor;
                windowsDesktop1.Color9 = C9.BackColor;
                windowsDesktop1.TitlebarColor_Active = TActive.BackColor;
                windowsDesktop1.TitlebarColor_Inactive = TInactive.BackColor;
            }
        }

        private void C1_Click(object sender, EventArgs e)
        {
            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                Forms.SubMenu.ShowMenu((ColorItem)sender);
                if (ColorClipboard.Event == ColorClipboard.MenuEvent.Cut | ColorClipboard.Event == ColorClipboard.MenuEvent.Paste | ColorClipboard.Event == ColorClipboard.MenuEvent.Override)
                {
                    windowsDesktop1.Color1 = ((ColorItem)sender).BackColor;
                }
                return;
            }

            ColorItem colorItem = (ColorItem)sender;
            Dictionary<Control, string[]> CList = new()
            {
                { colorItem, new string[] { nameof(colorItem.BackColor) } },
                { windowsDesktop1, new string[] { nameof(windowsDesktop1.Color1) } }
            };

            Color C = Forms.ColorPickerDlg.Pick(CList);

            colorItem.BackColor = C;
            colorItem.Invalidate();

            CList.Clear();
        }

        private void C2_Click(object sender, EventArgs e)
        {
            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                Forms.SubMenu.ShowMenu((ColorItem)sender);
                if (ColorClipboard.Event == ColorClipboard.MenuEvent.Cut | ColorClipboard.Event == ColorClipboard.MenuEvent.Paste | ColorClipboard.Event == ColorClipboard.MenuEvent.Override)
                {
                    windowsDesktop1.Color2 = ((ColorItem)sender).BackColor;
                }
                return;
            }

            ColorItem colorItem = (ColorItem)sender;
            Dictionary<Control, string[]> CList = new()
            {
                { colorItem, new string[] { nameof(colorItem.BackColor) } },
                { windowsDesktop1, new string[] { nameof(windowsDesktop1.Color2) } }
            };

            Color C = Forms.ColorPickerDlg.Pick(CList);

            colorItem.BackColor = C;
            colorItem.Invalidate();

            CList.Clear();
        }

        private void C3_Click(object sender, EventArgs e)
        {
            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                Forms.SubMenu.ShowMenu((ColorItem)sender);
                if (ColorClipboard.Event == ColorClipboard.MenuEvent.Cut | ColorClipboard.Event == ColorClipboard.MenuEvent.Paste | ColorClipboard.Event == ColorClipboard.MenuEvent.Override)
                {
                    windowsDesktop1.Color3 = ((ColorItem)sender).BackColor;
                }
                return;
            }

            ColorItem colorItem = (ColorItem)sender;
            Dictionary<Control, string[]> CList = new()
            {
                { colorItem, new string[] { nameof(colorItem.BackColor) } },
                { windowsDesktop1, new string[] { nameof(windowsDesktop1.Color3) } }
            };

            Color C = Forms.ColorPickerDlg.Pick(CList);

            colorItem.BackColor = C;
            colorItem.Invalidate();

            CList.Clear();
        }

        private void C4_Click(object sender, EventArgs e)
        {
            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                Forms.SubMenu.ShowMenu((ColorItem)sender);
                if (ColorClipboard.Event == ColorClipboard.MenuEvent.Cut | ColorClipboard.Event == ColorClipboard.MenuEvent.Paste | ColorClipboard.Event == ColorClipboard.MenuEvent.Override)
                {
                    windowsDesktop1.Color4 = ((ColorItem)sender).BackColor;
                }
                return;
            }

            ColorItem colorItem = (ColorItem)sender;
            Dictionary<Control, string[]> CList = new()
            {
                { colorItem, new string[] { nameof(colorItem.BackColor) } },
                { windowsDesktop1, new string[] { nameof(windowsDesktop1.Color4) } }
            };

            Color C = Forms.ColorPickerDlg.Pick(CList);

            colorItem.BackColor = C;
            colorItem.Invalidate();

            CList.Clear();
        }

        private void C5_Click(object sender, EventArgs e)
        {
            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                Forms.SubMenu.ShowMenu((ColorItem)sender);
                if (ColorClipboard.Event == ColorClipboard.MenuEvent.Cut | ColorClipboard.Event == ColorClipboard.MenuEvent.Paste | ColorClipboard.Event == ColorClipboard.MenuEvent.Override)
                {
                    windowsDesktop1.Color5 = ((ColorItem)sender).BackColor;
                }
                return;
            }

            ColorItem colorItem = (ColorItem)sender;
            Dictionary<Control, string[]> CList = new()
            {
                { colorItem, new string[] { nameof(colorItem.BackColor) } },
                { windowsDesktop1, new string[] { nameof(windowsDesktop1.Color5) } }
            };

            Color C = Forms.ColorPickerDlg.Pick(CList);

            colorItem.BackColor = C;
            colorItem.Invalidate();

            CList.Clear();
        }

        private void C6_Click(object sender, EventArgs e)
        {
            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                Forms.SubMenu.ShowMenu((ColorItem)sender);
                if (ColorClipboard.Event == ColorClipboard.MenuEvent.Cut | ColorClipboard.Event == ColorClipboard.MenuEvent.Paste | ColorClipboard.Event == ColorClipboard.MenuEvent.Override)
                {
                    windowsDesktop1.Color6 = ((ColorItem)sender).BackColor;
                }
                return;
            }

            ColorItem colorItem = (ColorItem)sender;
            Dictionary<Control, string[]> CList = new()
            {
                { colorItem, new string[] { nameof(colorItem.BackColor) } },
                { windowsDesktop1, new string[] { nameof(windowsDesktop1.Color6) } }
            };

            Color C = Forms.ColorPickerDlg.Pick(CList);

            colorItem.BackColor = C;
            colorItem.Invalidate();

            CList.Clear();
        }

        private void C7_Click(object sender, EventArgs e)
        {
            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                Forms.SubMenu.ShowMenu((ColorItem)sender);
                if (ColorClipboard.Event == ColorClipboard.MenuEvent.Cut | ColorClipboard.Event == ColorClipboard.MenuEvent.Paste | ColorClipboard.Event == ColorClipboard.MenuEvent.Override)
                {
                    windowsDesktop1.Color7 = ((ColorItem)sender).BackColor;
                }
                return;
            }

            ColorItem colorItem = (ColorItem)sender;
            Dictionary<Control, string[]> CList = new()
            {
                { colorItem, new string[] { nameof(colorItem.BackColor) } },
                { windowsDesktop1, new string[] { nameof(windowsDesktop1.Color7) } }
            };

            Color C = Forms.ColorPickerDlg.Pick(CList);

            colorItem.BackColor = C;
            colorItem.Invalidate();

            CList.Clear();
        }

        private void C8_Click(object sender, EventArgs e)
        {
            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                Forms.SubMenu.ShowMenu((ColorItem)sender);
                if (ColorClipboard.Event == ColorClipboard.MenuEvent.Cut | ColorClipboard.Event == ColorClipboard.MenuEvent.Paste | ColorClipboard.Event == ColorClipboard.MenuEvent.Override)
                {
                    windowsDesktop1.Color8 = ((ColorItem)sender).BackColor;
                }
                return;
            }

            ColorItem colorItem = (ColorItem)sender;
            Dictionary<Control, string[]> CList = new()
            {
                { colorItem, new string[] { nameof(colorItem.BackColor) } },
                { windowsDesktop1, new string[] { nameof(windowsDesktop1.Color8) } }
            };

            Color C = Forms.ColorPickerDlg.Pick(CList);

            colorItem.BackColor = C;
            colorItem.Invalidate();

            CList.Clear();
        }

        private void C9_Click(object sender, EventArgs e)
        {
            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                Forms.SubMenu.ShowMenu((ColorItem)sender);
                if (ColorClipboard.Event == ColorClipboard.MenuEvent.Cut | ColorClipboard.Event == ColorClipboard.MenuEvent.Paste | ColorClipboard.Event == ColorClipboard.MenuEvent.Override)
                {
                    windowsDesktop1.Color9 = ((ColorItem)sender).BackColor;
                }
                return;
            }

            ColorItem colorItem = (ColorItem)sender;
            Dictionary<Control, string[]> CList = new()
            {
                { colorItem, new string[] { nameof(colorItem.BackColor) } },
                { windowsDesktop1, new string[] { nameof(windowsDesktop1.Color9) } }
            };

            Color C = Forms.ColorPickerDlg.Pick(CList);

            colorItem.BackColor = C;
            colorItem.Invalidate();

            CList.Clear();
        }


        private void C1_BackColorChanged(object sender, EventArgs e)
        {
            e1.BackColor = C1.BackColor;
        }

        private void C2_BackColorChanged(object sender, EventArgs e)
        {
            e2.BackColor = C2.BackColor;
        }

        private void C3_BackColorChanged(object sender, EventArgs e)
        {
            e3.BackColor = C3.BackColor;
        }

        private void C4_BackColorChanged(object sender, EventArgs e)
        {
            e4.BackColor = C4.BackColor;
        }

        private void C5_BackColorChanged(object sender, EventArgs e)
        {
            e5.BackColor = C5.BackColor;
        }

        private void C6_BackColorChanged(object sender, EventArgs e)
        {
            e6.BackColor = C6.BackColor;
        }

        private void C7_BackColorChanged(object sender, EventArgs e)
        {
            e7.BackColor = C7.BackColor;
        }

        private void C8_BackColorChanged(object sender, EventArgs e)
        {
            e8.BackColor = C8.BackColor;
        }

        private void C9_BackColorChanged(object sender, EventArgs e)
        {
            e9.BackColor = C9.BackColor;
        }


        private void e1_BackColorChanged(object sender, EventArgs e)
        {
            C1.BackColor = e1.BackColor;
        }

        private void e2_BackColorChanged(object sender, EventArgs e)
        {
            C2.BackColor = e2.BackColor;
        }

        private void e3_BackColorChanged(object sender, EventArgs e)
        {
            C3.BackColor = e3.BackColor;
        }

        private void e4_BackColorChanged(object sender, EventArgs e)
        {
            C4.BackColor = e4.BackColor;
        }

        private void e5_BackColorChanged(object sender, EventArgs e)
        {
            C5.BackColor = e5.BackColor;
        }

        private void e6_BackColorChanged(object sender, EventArgs e)
        {
            C6.BackColor = e6.BackColor;
        }

        private void e7_BackColorChanged(object sender, EventArgs e)
        {
            C7.BackColor = e7.BackColor;
        }

        private void e8_BackColorChanged(object sender, EventArgs e)
        {
            C8.BackColor = e8.BackColor;
        }

        private void e9_BackColorChanged(object sender, EventArgs e)
        {
            C9.BackColor = e9.BackColor;
        }

        private void easy_activetitle_BackColorChanged(object sender, EventArgs e)
        {
            TActive.BackColor = easy_activetitle.BackColor;
        }

        private void easy_inactivetitle_BackColorChanged(object sender, EventArgs e)
        {
            TInactive.BackColor = easy_inactivetitle.BackColor;
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            Program.ToolTip.ToolTipText = Program.Lang.Strings.Tips.TitlebarColorNotice;
            Program.ToolTip.ToolTipTitle = Program.Lang.Strings.General.Tip;
            Program.ToolTip.Image = Assets.Notifications.Info;

            Point location = new(-Program.ToolTip.Size.Width - 2, (((Control)sender).Height - Program.ToolTip.Size.Height) / 2 - 1);

            Program.ToolTip.Show((Control)sender, Program.ToolTip.ToolTipTitle, Program.ToolTip.ToolTipText, Program.ToolTip.Image, location, 5000);
        }

        private void easy_appmode_light_CheckedChanged(object sender, EventArgs e)
        {
            AppMode_Toggle.Checked = !((UI.WP.RadioImage)sender).Checked;
        }

        private void easy_winmode_light_CheckedChanged(object sender, EventArgs e)
        {
            WinMode_Toggle.Checked = !((UI.WP.RadioImage)sender).Checked;
        }

        private void easy_opaque_CheckedChanged(object sender, EventArgs e)
        {
            Transparency_Toggle.Checked = !((UI.WP.RadioImage)sender).Checked;
        }

        private void easy_nocolortitle_CheckedChanged(object sender, EventArgs e)
        {
            ShowAccentOnTitlebarAndBorders_Toggle.Checked = !((UI.WP.RadioImage)sender).Checked;
        }

        private void windowsDesktop1_EditorInvoker(object sender, UI.Retro.EditorEventArgs e)
        {
            if (e.PropertyName.ToLower() == nameof(windowsDesktop1.DarkMode_App).ToLower())
            {
                AppMode_Toggle.Checked = !AppMode_Toggle.Checked;
            }

            else if (e.PropertyName.ToLower() == nameof(windowsDesktop1.TitlebarColor_Active).ToLower() || e.PropertyName.ToLower() == nameof(windowsDesktop1.TitlebarColor_Inactive).ToLower())
            {
                Dictionary<Control, string[]> CList = new() { { windowsDesktop1, new string[] { e.PropertyName } } };

                if (e.PropertyName.ToLower() == nameof(windowsDesktop1.TitlebarColor_Active).ToLower())
                {
                    if (AdvancedMode)
                    {
                        CList.Add(TActive, [nameof(TActive.BackColor)]);
                        CList.Add(easy_activetitle, [nameof(easy_activetitle.BackColor)]);
                    }
                    else
                    {
                        CList.Add(easy_activetitle, [nameof(easy_activetitle.BackColor)]);
                        CList.Add(TActive, [nameof(TActive.BackColor)]);
                    }
                }
                else if (e.PropertyName.ToLower() == nameof(windowsDesktop1.TitlebarColor_Inactive).ToLower())
                {
                    if (AdvancedMode)
                    {
                        CList.Add(TInactive, [nameof(TInactive.BackColor)]);
                        CList.Add(easy_inactivetitle, [nameof(easy_inactivetitle.BackColor)]);
                    }
                    else
                    {
                        CList.Add(easy_inactivetitle, [nameof(easy_inactivetitle.BackColor)]);
                        CList.Add(TInactive, [nameof(TInactive.BackColor)]);
                    }
                }

                Color C = Forms.ColorPickerDlg.Pick(CList);

                windowsDesktop1.SetProperty(e.PropertyName ?? "BackColor", C);
                ((Control)sender).SetProperty(e.PropertyName ?? "BackColor", C);

                CList.ElementAt(CList.Count - 1).Key.SetProperty(CList.ElementAt(CList.Count - 1).Value.FirstOrDefault(), C);

                CList.Clear();
            }

            else if (e.PropertyName.ToLower() == "Windows10x_StartColor".ToLower() || e.PropertyName.ToLower() == "Windows10x_ActionCenterColor".ToLower())
            {
                Dictionary<Control, string[]> CList = new() { { windowsDesktop1, new string[] { nameof(windowsDesktop1.Color4) } } };

                if (AdvancedMode)
                {
                    CList.Add(C4, [nameof(C4.BackColor)]);
                    CList.Add(e4, [nameof(e4.BackColor)]);
                }
                else
                {
                    CList.Add(e4, [nameof(e4.BackColor)]);
                    CList.Add(C4, [nameof(C4.BackColor)]);
                }

                Color C = Forms.ColorPickerDlg.Pick(CList);

                windowsDesktop1.SetProperty(e.PropertyName ?? "BackColor", C);
                ((Control)sender).SetProperty(e.PropertyName ?? "BackColor", C);

                CList.ElementAt(CList.Count - 1).Key.SetProperty(CList.ElementAt(CList.Count - 1).Value.FirstOrDefault(), C);

                CList.Clear();
            }

            else if (e.PropertyName.ToLower() == "Windows10x_TaskbarColor".ToLower())
            {
                Dictionary<Control, string[]> CList = [];

                if (Transparency_Toggle.Checked)
                {
                    CList.Add(windowsDesktop1, [nameof(windowsDesktop1.Color8)]);

                    if (AdvancedMode)
                    {
                        CList.Add(C8, [nameof(C8.BackColor)]);
                        CList.Add(e8, [nameof(e8.BackColor)]);
                    }
                    else
                    {
                        CList.Add(e8, [nameof(e8.BackColor)]);
                        CList.Add(C8, [nameof(C8.BackColor)]);
                    }
                }
                else
                {
                    CList.Add(windowsDesktop1, [nameof(windowsDesktop1.Color1)]);

                    if (AdvancedMode)
                    {
                        CList.Add(C1, [nameof(C1.BackColor)]);
                        CList.Add(e1, [nameof(e1.BackColor)]);
                    }
                    else
                    {
                        CList.Add(e1, [nameof(e1.BackColor)]);
                        CList.Add(C1, [nameof(C1.BackColor)]);
                    }
                }

                Color C = Forms.ColorPickerDlg.Pick(CList);

                windowsDesktop1.SetProperty(e.PropertyName ?? "BackColor", C);
                ((Control)sender).SetProperty(e.PropertyName ?? "BackColor", C);

                CList.ElementAt(CList.Count - 1).Key.SetProperty(CList.ElementAt(CList.Count - 1).Value.FirstOrDefault(), C);

                CList.Clear();
            }

            else if (e.PropertyName.ToLower() == "Windows10x_TaskbarAppUnderlineColor".ToLower())
            {
                Dictionary<Control, string[]> CList = [];

                if (WinMode_Toggle.Checked || (!WinMode_Toggle.Checked && !Accent_None.Checked))
                {
                    CList.Add(windowsDesktop1, [nameof(windowsDesktop1.Color3)]);

                    if (AdvancedMode)
                    {
                        CList.Add(C3, [nameof(C3.BackColor)]);
                        CList.Add(e3, [nameof(e3.BackColor)]);
                    }
                    else
                    {
                        CList.Add(e3, [nameof(e3.BackColor)]);
                        CList.Add(C3, [nameof(C3.BackColor)]);
                    }
                }
                else
                {
                    CList.Add(windowsDesktop1, [nameof(windowsDesktop1.Color5)]);

                    if (AdvancedMode)
                    {
                        CList.Add(C5, [nameof(C5.BackColor)]);
                        CList.Add(e5, [nameof(e5.BackColor)]);
                    }
                    else
                    {
                        CList.Add(e5, [nameof(e5.BackColor)]);
                        CList.Add(C5, [nameof(C5.BackColor)]);
                    }
                }

                Color C = Forms.ColorPickerDlg.Pick(CList);

                windowsDesktop1.SetProperty(e.PropertyName ?? "BackColor", C);
                ((Control)sender).SetProperty(e.PropertyName ?? "BackColor", C);

                CList.ElementAt(CList.Count - 1).Key.SetProperty(CList.ElementAt(CList.Count - 1).Value.FirstOrDefault(), C);

                CList.Clear();
            }

            else if (e.PropertyName.ToLower() == "Windows10_TaskbarAppColor".ToLower())
            {
                Dictionary<Control, string[]> CList = [];

                if (Transparency_Toggle.Checked)
                {
                    CList.Add(windowsDesktop1, [nameof(windowsDesktop1.Color5)]);

                    if (AdvancedMode)
                    {
                        CList.Add(C5, [nameof(C5.BackColor)]);
                        CList.Add(e5, [nameof(e5.BackColor)]);
                    }
                    else
                    {
                        CList.Add(e5, [nameof(e5.BackColor)]);
                        CList.Add(C5, [nameof(C5.BackColor)]);
                    }
                }
                else
                {
                    CList.Add(windowsDesktop1, [nameof(windowsDesktop1.Color4)]);

                    if (AdvancedMode)
                    {
                        CList.Add(C4, [nameof(C4.BackColor)]);
                        CList.Add(e4, [nameof(e4.BackColor)]);
                    }
                    else
                    {
                        CList.Add(e4, [nameof(e4.BackColor)]);
                        CList.Add(C4, [nameof(C4.BackColor)]);
                    }
                }

                Color C = Forms.ColorPickerDlg.Pick(CList);

                windowsDesktop1.SetProperty(e.PropertyName ?? "BackColor", C);
                ((Control)sender).SetProperty(e.PropertyName ?? "BackColor", C);

                CList.ElementAt(CList.Count - 1).Key.SetProperty(CList.ElementAt(CList.Count - 1).Value.FirstOrDefault(), C);

                CList.Clear();
            }

            else if (e.PropertyName.ToLower() == "Windows10_ActionCenterLinkColor".ToLower())
            {
                Dictionary<Control, string[]> CList = [];

                if (WinMode_Toggle.Checked || !WinMode_Toggle.Checked && Accent_StartTaskbar.Checked)
                {
                    CList.Add(windowsDesktop1, [nameof(windowsDesktop1.Color2)]);

                    if (AdvancedMode)
                    {
                        CList.Add(C2, [nameof(C2.BackColor)]);
                        CList.Add(e2, [nameof(e2.BackColor)]);
                    }
                    else
                    {
                        CList.Add(e2, [nameof(e2.BackColor)]);
                        CList.Add(C2, [nameof(C2.BackColor)]);
                    }
                }
                else
                {
                    CList.Add(windowsDesktop1, [nameof(windowsDesktop1.Color8)]);

                    if (AdvancedMode)
                    {
                        CList.Add(C8, [nameof(C8.BackColor)]);
                        CList.Add(e8, [nameof(e8.BackColor)]);
                    }
                    else
                    {
                        CList.Add(e8, [nameof(e8.BackColor)]);
                        CList.Add(C8, [nameof(C8.BackColor)]);
                    }
                }

                Color C = Forms.ColorPickerDlg.Pick(CList);

                windowsDesktop1.SetProperty(e.PropertyName ?? "BackColor", C);
                ((Control)sender).SetProperty(e.PropertyName ?? "BackColor", C);

                CList.ElementAt(CList.Count - 1).Key.SetProperty(CList.ElementAt(CList.Count - 1).Value.FirstOrDefault(), C);

                CList.Clear();
            }

            else if (e.PropertyName.ToLower() == "Windows10_ActionCenterButtonColor".ToLower())
            {
                Dictionary<Control, string[]> CList = new()
                {
                    { windowsDesktop1, new string[] { nameof(windowsDesktop1.Color5) } }
                };

                if (AdvancedMode)
                {
                    CList.Add(C5, [nameof(C5.BackColor)]);
                    CList.Add(e5, [nameof(e5.BackColor)]);
                }
                else
                {
                    CList.Add(e5, [nameof(e5.BackColor)]);
                    CList.Add(C5, [nameof(C5.BackColor)]);
                }

                Color C = Forms.ColorPickerDlg.Pick(CList);

                windowsDesktop1.SetProperty(e.PropertyName ?? "BackColor", C);
                ((Control)sender).SetProperty(e.PropertyName ?? "BackColor", C);

                CList.ElementAt(CList.Count - 1).Key.SetProperty(CList.ElementAt(CList.Count - 1).Value.FirstOrDefault(), C);

                CList.Clear();
            }

            else if (e.PropertyName.ToLower() == "Windows10_StartButtonColor".ToLower())
            {
                Dictionary<Control, string[]> CList = new()
                {
                    { windowsDesktop1, new string[] { nameof(windowsDesktop1.Color6) } }
                };

                if (AdvancedMode)
                {
                    CList.Add(C6, [nameof(C6.BackColor)]);
                    CList.Add(e6, [nameof(e6.BackColor)]);
                }
                else
                {
                    CList.Add(e6, [nameof(e6.BackColor)]);
                    CList.Add(C6, [nameof(C6.BackColor)]);
                }

                Color C = Forms.ColorPickerDlg.Pick(CList);

                windowsDesktop1.SetProperty(e.PropertyName ?? "BackColor", C);
                ((Control)sender).SetProperty(e.PropertyName ?? "BackColor", C);

                CList.ElementAt(CList.Count - 1).Key.SetProperty(CList.ElementAt(CList.Count - 1).Value.FirstOrDefault(), C);

                CList.Clear();
            }

            else if (e.PropertyName.ToLower() == WindowsDesktop.prop_settingsIconsColor.ToLower() || e.PropertyName.ToLower() == WindowsDesktop.prop_linksColor.ToLower())
            {
                Dictionary<Control, string[]> CList = new() { { windowsDesktop1, new string[] { nameof(windowsDesktop1.Color5) } } };

                if (AdvancedMode)
                {
                    CList.Add(C5, [nameof(C5.BackColor)]);
                    CList.Add(e5, [nameof(e5.BackColor)]);
                }
                else
                {
                    CList.Add(e5, [nameof(e5.BackColor)]);
                    CList.Add(C5, [nameof(C5.BackColor)]);
                }

                Color C = Forms.ColorPickerDlg.Pick(CList);

                windowsDesktop1.SetProperty(e.PropertyName ?? "BackColor", C);
                ((Control)sender).SetProperty(e.PropertyName ?? "BackColor", C);

                CList.ElementAt(CList.Count - 1).Key.SetProperty(CList.ElementAt(CList.Count - 1).Value.FirstOrDefault(), C);

                CList.Clear();
            }
        }

        private void easy_generator_Click(object sender, EventArgs e)
        {
            HSL mainHSL = easy_activetitle.BackColor.ToHSL();

            HSL hsl1 = mainHSL + new HSL(0, 0f, -0.1921569f);
            HSL hsl2 = mainHSL + new HSL(0, 0f, 0.4039216f);
            HSL hsl3 = mainHSL + new HSL(0, -0.2322581f, 0.2745098f);
            HSL hsl4 = mainHSL + new HSL(-1, 0f, -0.1117647f);
            HSL hsl5 = mainHSL + new HSL(0, 0f, 0f);
            HSL hsl6 = mainHSL + new HSL(0, -0.2580645f, 0.1529412f);
            HSL hsl7 = mainHSL + new HSL(-1, 0f, -0.1117647f);
            HSL hsl8 = mainHSL + new HSL(-1, 0f, -0.2921569f);
            HSL hsl9 = mainHSL + new HSL(-184, -0.06374502f, 0.0862745f);

            C1.BackColor = hsl1.ToRGB();
            C2.BackColor = hsl2.ToRGB();
            C3.BackColor = hsl3.ToRGB();
            C4.BackColor = hsl4.ToRGB();
            C5.BackColor = hsl5.ToRGB();
            C6.BackColor = hsl6.ToRGB();
            C7.BackColor = hsl7.ToRGB();
            C8.BackColor = hsl8.ToRGB();
            C9.BackColor = hsl9.ToRGB();

            windowsDesktop1.Color1 = C1.BackColor;
            windowsDesktop1.Color2 = C2.BackColor;
            windowsDesktop1.Color3 = C3.BackColor;
            windowsDesktop1.Color4 = C4.BackColor;
            windowsDesktop1.Color5 = C5.BackColor;
            windowsDesktop1.Color6 = C6.BackColor;
            windowsDesktop1.Color7 = C7.BackColor;
            windowsDesktop1.Color8 = C8.BackColor;
            windowsDesktop1.Color9 = C9.BackColor;

            Program.ToolTip.Show((UI.WP.Button)sender, Program.Lang.Strings.General.Done, Program.Lang.Strings.Messages.ReadjustColor, null, new Point(2, ((UI.WP.Button)sender).Height + 2));
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

        private void toggle2_CheckedChanged(object sender, EventArgs e)
        {
            windowsDesktop1.TB_Blur = TBBlur.Checked;
        }

        private void toggle1_CheckedChanged(object sender, EventArgs e)
        {
            windowsDesktop1.IncreaseTBTransparency = IncreastTBTransparency.Checked;
        }

        private void theme_aero_CheckedChanged(object sender, EventArgs e)
        {
            windowsDesktop1.Windows_10x_Theme = Theme.Structures.Windows10x.Themes.Aero;
        }

        private void theme_aerolite_CheckedChanged(object sender, EventArgs e)
        {
            windowsDesktop1.Windows_10x_Theme = Theme.Structures.Windows10x.Themes.AeroLite;

            alertBox1.Visible = theme_aerolite.Checked && AppMode_Toggle.Checked;
        }

        private void Win10Colors_HelpButtonClicked(object sender, System.ComponentModel.CancelEventArgs e)
        {
            System.Diagnostics.Process.Start(Links.Wiki.Win10xColors);
        }

        private void theme_skip_CheckedChanged(object sender, EventArgs e)
        {
            windowsDesktop1.Windows_10x_Theme = Theme.Structures.Windows10x.Themes.Aero;
        }
    }
}
