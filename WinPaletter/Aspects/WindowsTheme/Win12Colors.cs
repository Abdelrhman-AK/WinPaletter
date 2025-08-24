﻿using Devcorp.Controls.VisualStyles;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WinPaletter.NativeMethods;
using WinPaletter.Templates;
using WinPaletter.Theme;
using WinPaletter.UI.Controllers;
using static WinPaletter.PreviewHelpers;
using static WinPaletter.TypesExtensions.ColorsExtensions;

namespace WinPaletter
{
    public partial class Win12Colors : AspectsTemplate
    {
        public Win12Colors()
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
                TMx.Windows12.Apply("12");

                if (VS_ReplaceColors.Checked) TMx.Win32.Apply();
                if (VS_ReplaceMetrics.Checked) TMx.MetricsFonts.Apply();

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

        private void Win10Colors_Load(object sender, EventArgs e)
        {
            DesignerData data = new(this)
            {
                AspectName = string.Format(Program.Lang.Strings.Aspects.WinTheme, OS.Name),
                Enabled = Program.TM.Windows12.Enabled,
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

            ToolStripMenuItem item0 = new(string.Format(Program.Lang.Strings.General.CopycatFrom, Program.Lang.Strings.Windows.W10));
            ToolStripMenuItem item1 = new(string.Format(Program.Lang.Strings.General.CopycatFrom, Program.Lang.Strings.Windows.W11));
            item0.Click += Item0_Click;
            item1.Click += Item1_Click;
            easy_generator.Menu.Items.Add(item0);
            easy_generator.Menu.Items.Add(item1);
        }

        private void Item0_Click(object sender, EventArgs e)
        {
            // Copycat from Windows 10 colors
            using (Manager TMx = new(Manager.Source.Empty))
            {
                TMx.Windows12 = (Theme.Structures.Windows10x)Program.TM.Windows10.Clone();
                LoadFromTM((Manager)TMx.Clone());

                Program.ToolTip.Show(easy_generator, Program.Lang.Strings.General.Done, string.Empty, null, new Point(2, easy_generator.Height + 2));
            }
        }

        private void Item1_Click(object sender, EventArgs e)
        {
            // Copycat from Windows 11 colors
            using (Manager TMx = new(Manager.Source.Empty))
            {
                TMx.Windows12 = (Theme.Structures.Windows10x)Program.TM.Windows11.Clone();
                LoadFromTM((Manager)TMx.Clone());

                Program.ToolTip.Show(easy_generator, Program.Lang.Strings.General.Done, string.Empty, null, new Point(2, easy_generator.Height + 2));
            }
        }

        public void LoadFromTM(Manager TM)
        {
            AspectEnabled = TM.Windows12.Enabled;

            winmode_light.Checked = TM.Windows12.WinMode_Light;
            winmode_dark.Checked = !TM.Windows12.WinMode_Light;
            appmode_light.Checked = TM.Windows12.AppMode_Light;
            appmode_dark.Checked = !TM.Windows12.AppMode_Light;

            Transparency_Toggle.Checked = TM.Windows12.Transparency;
            ShowAccentOnTitlebarAndBorders_Toggle.Checked = TM.Windows12.ApplyAccentOnTitlebars;
            Accent_None.Checked = TM.Windows12.ApplyAccentOnTaskbar == Theme.Structures.Windows10x.AccentTaskbarLevels.None;
            Accent_Taskbar.Checked = TM.Windows12.ApplyAccentOnTaskbar == Theme.Structures.Windows10x.AccentTaskbarLevels.Taskbar;
            Accent_StartTaskbar.Checked = TM.Windows12.ApplyAccentOnTaskbar == Theme.Structures.Windows10x.AccentTaskbarLevels.Taskbar_Start_AC;

            TActive.BackColor = TM.Windows12.Titlebar_Active;
            TInactive.BackColor = TM.Windows12.Titlebar_Inactive;
            C7.BackColor = TM.Windows12.StartMenu_Accent;
            C6.BackColor = TM.Windows12.Color_Index2;
            C8.BackColor = TM.Windows12.Color_Index6;
            C3.BackColor = TM.Windows12.Color_Index1;
            C4.BackColor = TM.Windows12.Color_Index4;
            C1.BackColor = TM.Windows12.Color_Index5;
            C2.BackColor = TM.Windows12.Color_Index0;
            C5.BackColor = TM.Windows12.Color_Index3;
            C9.BackColor = TM.Windows12.Color_Index7;

            toggle1.Checked = TM.Windows12.VisualStyles.Enabled;

            if (TM.Windows12.VisualStyles.VisualStylesType == Theme.Structures.VisualStyles.DefaultVisualStyles.AeroLite) theme_aerolite.Checked = true;
            else if (TM.Windows12.VisualStyles.VisualStylesType == Theme.Structures.VisualStyles.DefaultVisualStyles.Custom) radioImage1.Checked = true;
            else theme_aero.Checked = true;
            groupBox4.Visible = radioImage1.Checked;

            VS_textbox.Text = TM.Windows12.VisualStyles.ThemeFile;
            VS_ColorsList.SelectedItem = TM.Windows12.VisualStyles.ColorScheme;

            if (TM.Windows12.VisualStyles.SizeScheme.ToLower() == "normalsize" && VS_SizesList.Items.Contains("Normal")) TM.Windows12.VisualStyles.SizeScheme = "Normal";
            else if (TM.Windows12.VisualStyles.SizeScheme.ToLower() == "normalsize" && VS_SizesList.Items.Contains("normal")) TM.Windows12.VisualStyles.SizeScheme = "normal";

            VS_SizesList.SelectedItem = TM.Windows12.VisualStyles.SizeScheme;

            VS_ReplaceColors.Checked = TM.Windows12.VisualStyles.OverrideColors;
            VS_ReplaceMetrics.Checked = TM.Windows12.VisualStyles.OverrideSizes;

            windowsDesktop1.HookedTM = TM;
            windowsDesktop1.LoadFromTM(TM);
            UpdateLegends();
        }

        public void ApplyToTM(Manager TM)
        {
            TM.Windows12.Enabled = AspectEnabled;

            TM.Windows12.WinMode_Light = winmode_light.Checked;
            TM.Windows12.AppMode_Light = appmode_light.Checked;
            TM.Windows12.Transparency = Transparency_Toggle.Checked;
            TM.Windows12.ApplyAccentOnTitlebars = ShowAccentOnTitlebarAndBorders_Toggle.Checked;

            if (Accent_None.Checked) TM.Windows12.ApplyAccentOnTaskbar = Theme.Structures.Windows10x.AccentTaskbarLevels.None;
            else if (Accent_Taskbar.Checked) TM.Windows12.ApplyAccentOnTaskbar = Theme.Structures.Windows10x.AccentTaskbarLevels.Taskbar;
            else if (Accent_StartTaskbar.Checked) TM.Windows12.ApplyAccentOnTaskbar = Theme.Structures.Windows10x.AccentTaskbarLevels.Taskbar_Start_AC;

            TM.Windows12.Titlebar_Active = TActive.BackColor;
            TM.Windows12.Titlebar_Inactive = TInactive.BackColor;
            TM.Windows12.StartMenu_Accent = C7.BackColor;
            TM.Windows12.Color_Index2 = C6.BackColor;
            TM.Windows12.Color_Index6 = C8.BackColor;
            TM.Windows12.Color_Index1 = C3.BackColor;
            TM.Windows12.Color_Index4 = C4.BackColor;
            TM.Windows12.Color_Index5 = C1.BackColor;
            TM.Windows12.Color_Index0 = C2.BackColor;
            TM.Windows12.Color_Index3 = C5.BackColor;
            TM.Windows12.Color_Index7 = C9.BackColor;

            TM.Windows12.VisualStyles.Enabled = toggle1.Checked;
            if (theme_aerolite.Checked) TM.Windows12.VisualStyles.VisualStylesType = Theme.Structures.VisualStyles.DefaultVisualStyles.AeroLite;
            else if (radioImage1.Checked) TM.Windows12.VisualStyles.VisualStylesType = Theme.Structures.VisualStyles.DefaultVisualStyles.Custom;
            else TM.Windows12.VisualStyles.VisualStylesType = Theme.Structures.VisualStyles.DefaultVisualStyles.Aero;

            TM.Windows12.VisualStyles.ThemeFile = VS_textbox.Text;
            TM.Windows12.VisualStyles.ColorScheme = (VS_ColorsList.SelectedItem ?? "").ToString();
            TM.Windows12.VisualStyles.SizeScheme = (VS_SizesList.SelectedItem ?? "").ToString().ToLower() == "normal" ? "NormalSize" : (VS_SizesList.SelectedItem ?? "").ToString();
            TM.Windows12.VisualStyles.OverrideColors = VS_ReplaceColors.Checked;
            TM.Windows12.VisualStyles.OverrideSizes = VS_ReplaceMetrics.Checked;

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
            using (Manager DefTM = Theme.Default.Get(WindowStyle.W10))
            {
                TActive.DefaultBackColor = DefTM.Windows12.Titlebar_Active;
                TInactive.DefaultBackColor = DefTM.Windows12.Titlebar_Inactive;
                C7.DefaultBackColor = DefTM.Windows12.StartMenu_Accent;
                C6.DefaultBackColor = DefTM.Windows12.Color_Index2;
                C8.DefaultBackColor = DefTM.Windows12.Color_Index6;
                C3.DefaultBackColor = DefTM.Windows12.Color_Index1;
                C4.DefaultBackColor = DefTM.Windows12.Color_Index4;
                C1.DefaultBackColor = DefTM.Windows12.Color_Index5;
                C2.DefaultBackColor = DefTM.Windows12.Color_Index0;
                C5.DefaultBackColor = DefTM.Windows12.Color_Index3;
                C9.DefaultBackColor = DefTM.Windows12.Color_Index7;
            }
        }

        private void UpdateLegends()
        {
            using (Manager TMx = new(Manager.Source.Empty))
            {
                ApplyToTM(TMx);
                ApplyWin10xLegends(TMx, WindowStyle.W11, lbl1, lbl2, lbl3, lbl4, lbl5, lbl6, lbl7, lbl8, lbl9, pic1, pic2, pic3, pic4, pic5, pic6, pic7, pic8, pic9);
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
        }

        private void Accent_None_CheckedChanged(object sender, EventArgs e)
        {
            if (IsShown & ((UI.WP.RadioImage)sender).Checked)
            {
                windowsDesktop1.AccentLevel = Theme.Structures.Windows10x.AccentTaskbarLevels.None;
            }

            Accent_None.Checked = ((UI.WP.RadioImage)sender).Checked;

            UpdateLegends();
        }

        private void Accent_Taskbar_CheckedChanged(object sender, EventArgs e)
        {
            if (IsShown & ((UI.WP.RadioImage)sender).Checked)
            {
                windowsDesktop1.AccentLevel = Theme.Structures.Windows10x.AccentTaskbarLevels.Taskbar;
            }

            Accent_Taskbar.Checked = ((UI.WP.RadioImage)sender).Checked;

            UpdateLegends();
        }

        private void Accent_StartTaskbar_CheckedChanged(object sender, EventArgs e)
        {
            if (IsShown & ((UI.WP.RadioImage)sender).Checked)
            {
                windowsDesktop1.AccentLevel = Theme.Structures.Windows10x.AccentTaskbarLevels.Taskbar_Start_AC;
            }

            Accent_StartTaskbar.Checked = ((UI.WP.RadioImage)sender).Checked;

            UpdateLegends();
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


        private void Button8_Click(object sender, EventArgs e)
        {
            Program.ToolTip.ToolTipText = Program.Lang.Strings.Tips.TitlebarColorNotice;
            Program.ToolTip.ToolTipTitle = Program.Lang.Strings.General.Tip;
            Program.ToolTip.Image = Assets.Notifications.Info;

            Point location = new(-Program.ToolTip.Size.Width - 2, (((Control)sender).Height - Program.ToolTip.Size.Height) / 2 - 1);

            Program.ToolTip.Show((Control)sender, Program.ToolTip.ToolTipTitle, Program.ToolTip.ToolTipText, Program.ToolTip.Image, location, 5000);
        }

        private void windowsDesktop1_EditorInvoker(object sender, UI.Retro.EditorEventArgs e)
        {
            if (e.PropertyName.ToLower() == nameof(windowsDesktop1.DarkMode_App).ToLower())
            {
                appmode_light.Checked = !appmode_light.Checked;
                appmode_dark.Checked = !appmode_light.Checked;
            }

            else if (e.PropertyName.ToLower() == nameof(windowsDesktop1.TitlebarColor_Active).ToLower() || e.PropertyName.ToLower() == nameof(windowsDesktop1.TitlebarColor_Inactive).ToLower())
            {
                Dictionary<Control, string[]> CList = new() { { windowsDesktop1, new string[] { e.PropertyName } } };

                if (e.PropertyName.ToLower() == nameof(windowsDesktop1.TitlebarColor_Active).ToLower())
                {
                    if (AdvancedMode)
                    {
                        CList.Add(TActive, [nameof(TActive.BackColor)]);
                    }
                    else
                    {
                        CList.Add(TActive, [nameof(TActive.BackColor)]);
                    }
                }
                else if (e.PropertyName.ToLower() == nameof(windowsDesktop1.TitlebarColor_Inactive).ToLower())
                {
                    if (AdvancedMode)
                    {
                        CList.Add(TInactive, [nameof(TInactive.BackColor)]);
                    }
                    else
                    {
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
                Dictionary<Control, string[]> CList = [];

                if (winmode_dark.Checked)
                {
                    CList.Add(windowsDesktop1, [nameof(windowsDesktop1.Color1)]);

                    if (AdvancedMode)
                    {
                        CList.Add(C1, [nameof(C1.BackColor)]);
                    }
                    else
                    {
                        CList.Add(C1, [nameof(C1.BackColor)]);
                    }
                }
                else
                {
                    CList.Add(windowsDesktop1, [nameof(windowsDesktop1.Color2)]);

                    if (AdvancedMode)
                    {
                        CList.Add(C2, [nameof(C2.BackColor)]);
                    }
                    else
                    {
                        CList.Add(C2, [nameof(C2.BackColor)]);
                    }
                }

                Color C = Forms.ColorPickerDlg.Pick(CList);

                windowsDesktop1.SetProperty(e.PropertyName ?? "BackColor", C);
                ((Control)sender).SetProperty(e.PropertyName ?? "BackColor", C);

                CList.ElementAt(CList.Count - 1).Key.SetProperty(CList.ElementAt(CList.Count - 1).Value.FirstOrDefault(), C);

                CList.Clear();
            }

            else if (e.PropertyName.ToLower() == "Windows10x_TaskbarColor".ToLower())
            {
                Dictionary<Control, string[]> CList = new() { { windowsDesktop1, new string[] { nameof(windowsDesktop1.Color1) } } };

                if (AdvancedMode)
                {
                    CList.Add(C1, [nameof(C1.BackColor)]);
                }
                else
                {
                    CList.Add(C1, [nameof(C1.BackColor)]);
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

                if (winmode_dark.Checked)
                {
                    CList.Add(windowsDesktop1, [nameof(windowsDesktop1.Color3)]);

                    if (AdvancedMode)
                    {
                        CList.Add(C3, [nameof(C3.BackColor)]);
                    }
                    else
                    {
                        CList.Add(C3, [nameof(C3.BackColor)]);
                    }
                }
                else
                {
                    CList.Add(windowsDesktop1, [nameof(windowsDesktop1.Color4)]);

                    if (AdvancedMode)
                    {
                        CList.Add(C4, [nameof(C4.BackColor)]);
                    }
                    else
                    {
                        CList.Add(C4, [nameof(C4.BackColor)]);
                    }
                }

                Color C = Forms.ColorPickerDlg.Pick(CList);

                windowsDesktop1.SetProperty(e.PropertyName ?? "BackColor", C);
                ((Control)sender).SetProperty(e.PropertyName ?? "BackColor", C);

                CList.ElementAt(CList.Count - 1).Key.SetProperty(CList.ElementAt(CList.Count - 1).Value.FirstOrDefault(), C);

                CList.Clear();
            }

            else if (e.PropertyName.ToLower() == WindowsDesktop.prop_settingsIconsColor.ToLower())
            {
                Dictionary<Control, string[]> CList = new() { { windowsDesktop1, new string[] { nameof(windowsDesktop1.Color5) } } };

                if (AdvancedMode)
                {
                    CList.Add(C5, [nameof(C5.BackColor)]);
                }
                else
                {
                    CList.Add(C5, [nameof(C5.BackColor)]);
                }

                Color C = Forms.ColorPickerDlg.Pick(CList);

                windowsDesktop1.SetProperty(e.PropertyName ?? "BackColor", C);
                ((Control)sender).SetProperty(e.PropertyName ?? "BackColor", C);

                CList.ElementAt(CList.Count - 1).Key.SetProperty(CList.ElementAt(CList.Count - 1).Value.FirstOrDefault(), C);

                CList.Clear();
            }

            else if (e.PropertyName.ToLower() == WindowsDesktop.prop_linksColor.ToLower())
            {
                Dictionary<Control, string[]> CList = [];

                if (winmode_dark.Checked)
                {
                    CList.Add(windowsDesktop1, [nameof(windowsDesktop1.Color2)]);

                    if (AdvancedMode)
                    {
                        CList.Add(C2, [nameof(C2.BackColor)]);
                    }
                    else
                    {
                        CList.Add(C2, [nameof(C2.BackColor)]);
                    }
                }
                else
                {
                    CList.Add(windowsDesktop1, [nameof(windowsDesktop1.Color1)]);

                    if (AdvancedMode)
                    {
                        CList.Add(C1, [nameof(C1.BackColor)]);
                    }
                    else
                    {
                        CList.Add(C1, [nameof(C1.BackColor)]);
                    }
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
            HSL mainHSL = TActive.BackColor.ToHSL();

            HSL hsl1 = mainHSL + new HSL(8, 0f, -0.1294118f);
            HSL hsl2 = mainHSL + new HSL(-14, 0f, 0.3901961f);
            HSL hsl3 = mainHSL + new HSL(-6, 0f, 0.2333333f);
            HSL hsl4 = mainHSL + new HSL(1, 0f, -0.03921568f);
            HSL hsl5 = mainHSL + new HSL(0, 0f, 0f);
            HSL hsl6 = mainHSL + new HSL(-2, 0f, 0.07058823f);
            HSL hsl7 = mainHSL + new HSL(1, 0f, -0.03921568f);
            HSL hsl8 = mainHSL + new HSL(19, 0f, -0.2117647f);
            HSL hsl9 = mainHSL + new HSL(-184, -0.06374502f, 0.09215686f);

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

        private void theme_aero_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as UI.WP.RadioImage).Checked) windowsDesktop1.VisualStyles = Theme.Structures.VisualStyles.DefaultVisualStyles.Aero;
        }

        private void theme_aerolite_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as UI.WP.RadioImage).Checked) windowsDesktop1.VisualStyles = Theme.Structures.VisualStyles.DefaultVisualStyles.AeroLite;

            alertBox1.Visible = theme_aerolite.Checked && winmode_dark.Checked;
        }

        private void radioImage1_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as UI.WP.RadioImage).Checked)
            {
                windowsDesktop1.VisualStyles = Theme.Structures.VisualStyles.DefaultVisualStyles.Custom;
            }

            groupBox4.Visible = (sender as UI.WP.RadioImage).Checked;
        }

        private void Win10xColors_HelpButtonClicked(object sender, System.ComponentModel.CancelEventArgs e)
        {
            System.Diagnostics.Process.Start(Links.Wiki.Win10xColors);
        }

        private void winmode_dark_CheckedChanged(object sender, EventArgs e)
        {
            if (IsShown)
            {
                windowsDesktop1.DarkMode_Win = (sender as UI.WP.RadioImage).Checked;
                UpdateLegends();
            }
        }

        private void winmode_light_CheckedChanged(object sender, EventArgs e)
        {
            if (IsShown)
            {
                windowsDesktop1.DarkMode_Win = !(sender as UI.WP.RadioImage).Checked;
                UpdateLegends();
            }
        }

        private void appmode_light_CheckedChanged(object sender, EventArgs e)
        {
            if (IsShown)
            {
                windowsDesktop1.DarkMode_App = !(sender as UI.WP.RadioImage).Checked;
                UpdateLegends();
            }
        }

        private void appmode_dark_CheckedChanged(object sender, EventArgs e)
        {
            if (IsShown)
            {
                windowsDesktop1.DarkMode_App = (sender as UI.WP.RadioImage).Checked;
                UpdateLegends();
            }
        }

        private void TActive_BackColorChanged(object sender, EventArgs e)
        {
            //logonui_screen.BackColor = (sender as ColorItem).BackColor;
        }

        private void VS_Browse_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new() { FileName = VS_textbox.Text, Filter = Program.Filters.VisualStyles_And_Themes, Title = Program.Lang.Strings.Extensions.OpenVisualStyle })
            {
                if (dlg.ShowDialog() == DialogResult.OK) VS_textbox.Text = libmsstyle.VisualStyle.GetCorrectMSStyles(dlg.FileName);
            }
        }

        private void VS_textbox_TextChanged(object sender, EventArgs e)
        {
            VS_ColorsList.Items.Clear();
            VS_SizesList.Items.Clear();

            string currentVS = UxTheme.GetCurrentVS().Item1;

            if (System.IO.File.Exists(VS_textbox.Text))
            {
                setVS(VS_textbox.Text);
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

        private void button21_Click(object sender, EventArgs e)
        {
            VS_textbox.Text = SysPaths.MSSTYLES_Aero_Win;
        }
    }
}
