using Devcorp.Controls.VisualStyles;
using libmsstyle;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinPaletter.Templates;
using WinPaletter.Theme;
using WinPaletter.UI.Controllers;
using WinPaletter.UI.Retro;

namespace WinPaletter
{
    public partial class Win32UI
    {
        public Win32UI()
        {
            InitializeComponent();
        }

        private Color _btn_shadow;
        private Color _btn_dkshadow;
        private Color _btn_hilight;
        private Color _btn_light;

        private void LoadFromTHEME(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new() { Filter = Program.Filters.Themes, Title = Program.Lang.Strings.Extensions.OpenTheme })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    using (Manager _Def = Default.FromOS(Program.WindowStyle))
                    {
                        LoadFromWin9xTheme(dlg.FileName, _Def.Win32);
                    }
                }
            }
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
            Manager TMx = new(Manager.Source.Registry);
            LoadFromTM(TMx);
            TMx.Dispose();
        }

        private void LoadFromDefault(object sender, EventArgs e)
        {
            Manager TMx = Default.FromOS(Program.WindowStyle);
            LoadFromTM(TMx);
            TMx.Dispose();
        }

        private void LoadFromMSSTYLES(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new() { Filter = Program.Filters.VisualStyles_And_Themes, Title = Program.Lang.Strings.Extensions.OpenVisualStyle })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    string theme = dlg.FileName;

                    try
                    {
                        // Newer versions of msstyles
                        using (VisualStyle visualStyle = new(theme)) { LoadFromWin32UI(visualStyle.ClassicColors()); }
                        ApplyRetroPreview();
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
                                using (VisualStyleFile vs = new(theme)) { LoadColors(vs.Metrics); }
                                ApplyRetroPreview();
                            }
                        }
                        catch
                        {
                            MsgBox(Program.Lang.Strings.Messages.InvalidTheme, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void LoadIntoCurrentTheme(object sender, EventArgs e)
        {
            ApplyToTM(Program.TM);
            Close();
        }

        private void Apply(object sender, EventArgs e)
        {
            if (Program.Settings.AspectsControl.Enabled && !Program.Settings.AspectsControl.ClassicColors)
            {
                MsgBox(Program.Lang.Strings.Aspects.Disabled_Apply_0, MessageBoxButtons.OK, MessageBoxIcon.Warning, Program.Lang.Strings.Aspects.Disabled_Apply_1);
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
            ApplyToTM(Program.TM_Original);

            Program.TM.Win32.Apply();
            Program.TM.Win32.Broadcast_UPM_ToDefUsers();

            Cursor = Cursors.Default;
        }

        private void SaveAsTHEME(object sender, EventArgs e)
        {
            using (SaveFileDialog dlg = new() { Filter = Program.Filters.Themes, Title = Program.Lang.Strings.Extensions.SaveTheme })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    Theme.Structures.Win32UI win32UI = new();
                    ApplyToWin32UI(win32UI);

                    try
                    {
                        File.WriteAllText(dlg.FileName, win32UI.ToString(sender is not ToolStripMenuItem ? Program.TM.MetricsFonts : null));
                    }
                    catch (Exception ex)
                    {
                        Forms.BugReport.ThrowError(ex);
                    }
                }
            }
        }

        private void ModeSwitched(object sender, EventArgs e)
        {
            if (AdvancedMode)
            {
                preview.Width = preview.Right - (TabControl1.Right + 10);
                preview.Left = TabControl1.Right + 10;
                TabControl1.Visible = true;
            }
            else
            {
                preview.Width = preview.Right - TabControl1.Left;
                preview.Left = TabControl1.Left;
                TabControl1.Visible = false;
            }
        }

        private void Win32UI_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.Settings.AspectsControl.ClassicColors_Advanced = AdvancedMode;
            Program.Settings.AspectsControl.Save();
        }

        private void Win32UI_Load(object sender, EventArgs e)
        {
            DesignerData data = new(this)
            {
                AspectName = Program.Lang.Strings.Aspects.ClassicColors,
                Enabled = Program.TM.Win32.Enabled,
                Import_theme = true,
                Import_msstyles = true,
                GeneratePalette = true,
                GenerateMSTheme = true,
                Import_preset = false,

                OnLoadIntoCurrentTheme = LoadIntoCurrentTheme,
                OnApply = Apply,
                OnSaveAsMSTheme = SaveAsTHEME,
                OnSaveAsMSTheme_OneAspect = SaveAsTHEME,
                OnImportFromDefault = LoadFromDefault,
                OnImportFromTHEME = LoadFromTHEME,
                OnImportFromWPTH = LoadFromWPTH,
                OnImportFromMSSTYLES = LoadFromMSSTYLES,
                OnImportFromCurrentApplied = LoadFromCurrent,

                OnModeAdvanced = ModeSwitched,
                OnModeSimple = ModeSwitched,
            };

            LoadData(data);

            AdvancedMode = Program.Settings.AspectsControl.ClassicColors_Advanced;

            splitContainer1.Panel2Collapsed = !checkBox1.Checked;

            ComboBox1.Items.Clear();
            ComboBox1.Items.AddRange(Schemes.ClassicColors.Split('\n').Select(f => f.Split('|')[0]).ToArray());
            ComboBox1.SelectedIndex = 0;

            ApplyDefaultTMValues();

            LoadFromTM(Program.TM);
            retroDesktopColors1.LoadMetrics(Program.TM);

            this.DoubleBuffer();

            foreach (ColorItem ColorItem in this.GetAllControls().OfType<ColorItem>())
            {
                ColorItem.Click += ColorItem_Click;
                ColorItem.ContextMenuMadeColorChangeInvoker += ColorItem_ContextMenuMadeColorChangeInvoker;
                ColorItem.DragDrop += ColorItem_DragDrop;
            }
        }

        private void Win32UI_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (ColorItem ColorItem in this.GetAllControls().OfType<ColorItem>())
            {
                ColorItem.Click -= ColorItem_Click;
                ColorItem.ContextMenuMadeColorChangeInvoker -= ColorItem_ContextMenuMadeColorChangeInvoker;
                ColorItem.DragDrop -= ColorItem_DragDrop;
            }
        }


        public void LoadFromTM(Manager TM)
        {
            LoadFromWin32UI(TM.Win32);
        }

        public void LoadFromWin32UI(Theme.Structures.Win32UI win32ui)
        {
            AspectEnabled = win32ui.Enabled;
            Toggle1.Checked = win32ui.EnableTheming;
            Toggle2.Checked = win32ui.EnableGradient;
            ActiveBorder_pick.BackColor = win32ui.ActiveBorder;
            activetitle_pick.BackColor = win32ui.ActiveTitle;
            AppWorkspace_pick.BackColor = win32ui.AppWorkspace;
            background_pick.BackColor = win32ui.Background;
            btnaltface_pick.BackColor = win32ui.ButtonAlternateFace;
            btndkshadow_pick.BackColor = win32ui.ButtonDkShadow;
            btnface_pick.BackColor = win32ui.ButtonFace;
            btnhilight_pick.BackColor = win32ui.ButtonHilight;
            btnlight_pick.BackColor = win32ui.ButtonLight;
            btnshadow_pick.BackColor = win32ui.ButtonShadow;
            btntext_pick.BackColor = win32ui.ButtonText;
            GActivetitle_pick.BackColor = win32ui.GradientActiveTitle;
            GInactivetitle_pick.BackColor = win32ui.GradientInactiveTitle;
            GrayText_pick.BackColor = win32ui.GrayText;
            hilighttext_pick.BackColor = win32ui.HilightText;
            hottracking_pick.BackColor = win32ui.HotTrackingColor;
            InactiveBorder_pick.BackColor = win32ui.InactiveBorder;
            InactiveTitle_pick.BackColor = win32ui.InactiveTitle;
            InactivetitleText_pick.BackColor = win32ui.InactiveTitleText;
            InfoText_pick.BackColor = win32ui.InfoText;
            InfoWindow_pick.BackColor = win32ui.InfoWindow;
            menu_pick.BackColor = win32ui.Menu;
            menubar_pick.BackColor = win32ui.MenuBar;
            menutext_pick.BackColor = win32ui.MenuText;
            Scrollbar_pick.BackColor = win32ui.Scrollbar;
            TitleText_pick.BackColor = win32ui.TitleText;
            Window_pick.BackColor = win32ui.Window;
            Frame_pick.BackColor = win32ui.WindowFrame;
            WindowText_pick.BackColor = win32ui.WindowText;
            hilight_pick.BackColor = win32ui.Hilight;
            menuhilight_pick.BackColor = win32ui.MenuHilight;
            desktop_pick.BackColor = win32ui.Desktop;

            _btn_shadow = win32ui.ButtonShadow;
            _btn_dkshadow = win32ui.ButtonDkShadow;
            _btn_hilight = win32ui.ButtonHilight;
            _btn_light = win32ui.ButtonLight;
            trackBarX1.Value = 100;

            retroDesktopColors1.LoadColors(win32ui);

            Retro3DPreview1.BackColor = win32ui.ButtonFace;
            Retro3DPreview1.ButtonDkShadow = win32ui.ButtonDkShadow;
            Retro3DPreview1.WindowFrame = win32ui.WindowFrame;
            Retro3DPreview1.ButtonHilight = win32ui.ButtonHilight;
            Retro3DPreview1.ButtonLight = win32ui.ButtonLight;
            Retro3DPreview1.ButtonShadow = win32ui.ButtonShadow;
            Retro3DPreview1.ForeColor = win32ui.ButtonText;
            Retro3DPreview1.Refresh();
        }

        public void LoadFromRetroPreview(RetroDesktopColors retroDesktopColors)
        {
            Toggle1.Checked = retroDesktopColors.EnableTheming;
            Toggle2.Checked = retroDesktopColors.EnableGradient;
            ActiveBorder_pick.BackColor = retroDesktopColors.ActiveBorder;
            activetitle_pick.BackColor = retroDesktopColors.ActiveTitle;
            AppWorkspace_pick.BackColor = retroDesktopColors.AppWorkspace;
            background_pick.BackColor = retroDesktopColors.Background;
            btnaltface_pick.BackColor = retroDesktopColors.ButtonAlternateFace;
            btndkshadow_pick.BackColor = retroDesktopColors.ButtonDkShadow;
            btnface_pick.BackColor = retroDesktopColors.ButtonFace;
            btnhilight_pick.BackColor = retroDesktopColors.ButtonHilight;
            btnlight_pick.BackColor = retroDesktopColors.ButtonLight;
            btnshadow_pick.BackColor = retroDesktopColors.ButtonShadow;
            btntext_pick.BackColor = retroDesktopColors.ButtonText;
            GActivetitle_pick.BackColor = retroDesktopColors.GradientActiveTitle;
            GInactivetitle_pick.BackColor = retroDesktopColors.GradientInactiveTitle;
            GrayText_pick.BackColor = retroDesktopColors.GrayText;
            hilighttext_pick.BackColor = retroDesktopColors.HilightText;
            hottracking_pick.BackColor = retroDesktopColors.HotTrackingColor;
            InactiveBorder_pick.BackColor = retroDesktopColors.InactiveBorder;
            InactiveTitle_pick.BackColor = retroDesktopColors.InactiveTitle;
            InactivetitleText_pick.BackColor = retroDesktopColors.InactiveTitleText;
            InfoText_pick.BackColor = retroDesktopColors.InfoText;
            InfoWindow_pick.BackColor = retroDesktopColors.InfoWindow;
            menu_pick.BackColor = retroDesktopColors.Menu;
            menubar_pick.BackColor = retroDesktopColors.MenuBar;
            menutext_pick.BackColor = retroDesktopColors.MenuText;
            Scrollbar_pick.BackColor = retroDesktopColors.Scrollbar;
            TitleText_pick.BackColor = retroDesktopColors.TitleText;
            Window_pick.BackColor = retroDesktopColors.Window;
            Frame_pick.BackColor = retroDesktopColors.WindowFrame;
            WindowText_pick.BackColor = retroDesktopColors.WindowText;
            hilight_pick.BackColor = retroDesktopColors.Hilight;
            menuhilight_pick.BackColor = retroDesktopColors.MenuHilight;
            desktop_pick.BackColor = retroDesktopColors.Desktop;

            _btn_shadow = retroDesktopColors.ButtonShadow;
            _btn_dkshadow = retroDesktopColors.ButtonDkShadow;
            _btn_hilight = retroDesktopColors.ButtonHilight;
            _btn_light = retroDesktopColors.ButtonLight;
            trackBarX1.Value = 100;

            Retro3DPreview1.BackColor = retroDesktopColors.ButtonFace;
            Retro3DPreview1.ButtonDkShadow = retroDesktopColors.ButtonDkShadow;
            Retro3DPreview1.WindowFrame = retroDesktopColors.WindowFrame;
            Retro3DPreview1.ButtonHilight = retroDesktopColors.ButtonHilight;
            Retro3DPreview1.ButtonLight = retroDesktopColors.ButtonLight;
            Retro3DPreview1.ButtonShadow = retroDesktopColors.ButtonShadow;
            Retro3DPreview1.ForeColor = retroDesktopColors.ButtonText;
            Retro3DPreview1.Refresh();
        }

        public void LoadColors(VisualStyleMetrics vs)
        {
            Toggle1.Checked = vs.FlatMenus;
            //ActiveBorder_pick.BackColor = vs.Palette.ActiveBorder;
            activetitle_pick.BackColor = vs.Colors.ActiveCaption;
            AppWorkspace_pick.BackColor = vs.Colors.AppWorkspace;
            background_pick.BackColor = vs.Colors.Background;
            //btnaltface_pick.BackColor = vs.Palette.ButtonAlternativeFace;
            btndkshadow_pick.BackColor = vs.Colors.DkShadow3d;
            btnface_pick.BackColor = vs.Colors.Btnface;
            btnhilight_pick.BackColor = vs.Colors.BtnHighlight;
            btnlight_pick.BackColor = vs.Colors.Light3d;
            btnshadow_pick.BackColor = vs.Colors.BtnShadow;
            btntext_pick.BackColor = vs.Colors.WindowText;
            GActivetitle_pick.BackColor = vs.Colors.GradientActiveCaption;
            GInactivetitle_pick.BackColor = vs.Colors.GradientInactiveCaption;
            GrayText_pick.BackColor = vs.Colors.GrayText;
            hilighttext_pick.BackColor = vs.Colors.HighlightText;
            hottracking_pick.BackColor = vs.Colors.HotTracking;
            //InactiveBorder_pick.BackColor = vs.Palette.InactiveBorder;
            InactiveTitle_pick.BackColor = vs.Colors.InactiveCaption;
            InactivetitleText_pick.BackColor = vs.Colors.InactiveCaptionText;
            //InfoText_pick.BackColor = vs.Palette.InfoText;
            //InfoWindow_pick.BackColor = vs.Palette.InfoBk;
            menu_pick.BackColor = vs.Colors.Menu;
            menubar_pick.BackColor = vs.Colors.MenuBar;
            menutext_pick.BackColor = vs.Colors.MenuText;
            //Scrollbar_pick.BackColor = vs.Palette.ScrollBar;
            TitleText_pick.BackColor = vs.Colors.CaptionText;
            Window_pick.BackColor = vs.Colors.Window;
            //Frame_pick.BackColor = vs.Palette.WindowFrame;
            WindowText_pick.BackColor = vs.Colors.WindowText;
            hilight_pick.BackColor = vs.Colors.Highlight;
            menuhilight_pick.BackColor = vs.Colors.MenuHilight;
            desktop_pick.BackColor = vs.Colors.Background;
        }

        public void ApplyDefaultTMValues()
        {
            using (Manager DefTM = Default.FromCurrentOS)
            {
                ActiveBorder_pick.DefaultBackColor = DefTM.Win32.ActiveBorder;
                activetitle_pick.DefaultBackColor = DefTM.Win32.ActiveTitle;
                AppWorkspace_pick.DefaultBackColor = DefTM.Win32.AppWorkspace;
                background_pick.DefaultBackColor = DefTM.Win32.Background;
                btnaltface_pick.DefaultBackColor = DefTM.Win32.ButtonAlternateFace;
                btndkshadow_pick.DefaultBackColor = DefTM.Win32.ButtonDkShadow;
                btnface_pick.DefaultBackColor = DefTM.Win32.ButtonFace;
                btnhilight_pick.DefaultBackColor = DefTM.Win32.ButtonHilight;
                btnlight_pick.DefaultBackColor = DefTM.Win32.ButtonLight;
                btnshadow_pick.DefaultBackColor = DefTM.Win32.ButtonShadow;
                btntext_pick.DefaultBackColor = DefTM.Win32.ButtonText;
                GActivetitle_pick.DefaultBackColor = DefTM.Win32.GradientActiveTitle;
                GInactivetitle_pick.DefaultBackColor = DefTM.Win32.GradientInactiveTitle;
                GrayText_pick.DefaultBackColor = DefTM.Win32.GrayText;
                hilighttext_pick.DefaultBackColor = DefTM.Win32.HilightText;
                hottracking_pick.DefaultBackColor = DefTM.Win32.HotTrackingColor;
                InactiveBorder_pick.DefaultBackColor = DefTM.Win32.InactiveBorder;
                InactiveTitle_pick.DefaultBackColor = DefTM.Win32.InactiveTitle;
                InactivetitleText_pick.DefaultBackColor = DefTM.Win32.InactiveTitleText;
                InfoText_pick.DefaultBackColor = DefTM.Win32.InfoText;
                InfoWindow_pick.DefaultBackColor = DefTM.Win32.InfoWindow;
                menu_pick.DefaultBackColor = DefTM.Win32.Menu;
                menubar_pick.DefaultBackColor = DefTM.Win32.MenuBar;
                menutext_pick.DefaultBackColor = DefTM.Win32.MenuText;
                Scrollbar_pick.DefaultBackColor = DefTM.Win32.Scrollbar;
                TitleText_pick.DefaultBackColor = DefTM.Win32.TitleText;
                Window_pick.DefaultBackColor = DefTM.Win32.Window;
                Frame_pick.DefaultBackColor = DefTM.Win32.WindowFrame;
                WindowText_pick.DefaultBackColor = DefTM.Win32.WindowText;
                hilight_pick.DefaultBackColor = DefTM.Win32.Hilight;
                menuhilight_pick.DefaultBackColor = DefTM.Win32.MenuHilight;
                desktop_pick.DefaultBackColor = DefTM.Win32.Desktop;
            }
        }

        public void ApplyToTM(Manager TM)
        {
            ApplyToWin32UI(TM.Win32);
        }

        public void ApplyToWin32UI(Theme.Structures.Win32UI Win32)
        {
            Win32.Enabled = AspectEnabled;
            Win32.EnableTheming = Toggle1.Checked;
            Win32.EnableGradient = Toggle2.Checked;
            Win32.ActiveBorder = ActiveBorder_pick.BackColor;
            Win32.ActiveTitle = activetitle_pick.BackColor;
            Win32.AppWorkspace = AppWorkspace_pick.BackColor;
            Win32.Background = background_pick.BackColor;
            Win32.ButtonAlternateFace = btnaltface_pick.BackColor;
            Win32.ButtonDkShadow = btndkshadow_pick.BackColor;
            Win32.ButtonFace = btnface_pick.BackColor;
            Win32.ButtonHilight = btnhilight_pick.BackColor;
            Win32.ButtonLight = btnlight_pick.BackColor;
            Win32.ButtonShadow = btnshadow_pick.BackColor;
            Win32.ButtonText = btntext_pick.BackColor;
            Win32.GradientActiveTitle = GActivetitle_pick.BackColor;
            Win32.GradientInactiveTitle = GInactivetitle_pick.BackColor;
            Win32.GrayText = GrayText_pick.BackColor;
            Win32.HilightText = hilighttext_pick.BackColor;
            Win32.HotTrackingColor = hottracking_pick.BackColor;
            Win32.InactiveBorder = InactiveBorder_pick.BackColor;
            Win32.InactiveTitle = InactiveTitle_pick.BackColor;
            Win32.InactiveTitleText = InactivetitleText_pick.BackColor;
            Win32.InfoText = InfoText_pick.BackColor;
            Win32.InfoWindow = InfoWindow_pick.BackColor;
            Win32.Menu = menu_pick.BackColor;
            Win32.MenuBar = menubar_pick.BackColor;
            Win32.MenuText = menutext_pick.BackColor;
            Win32.Scrollbar = Scrollbar_pick.BackColor;
            Win32.TitleText = TitleText_pick.BackColor;
            Win32.Window = Window_pick.BackColor;
            Win32.WindowFrame = Frame_pick.BackColor;
            Win32.WindowText = WindowText_pick.BackColor;
            Win32.Hilight = hilight_pick.BackColor;
            Win32.MenuHilight = menuhilight_pick.BackColor;
            Win32.Desktop = desktop_pick.BackColor;
        }

        private void ColorItem_Click(object sender, EventArgs e)
        {
            if (e is DragEventArgs) return;

            ColorItem colorItem = (ColorItem)sender;
            Dictionary<Control, string[]> CList = new()
            {
                { colorItem, new string[] { nameof(colorItem.BackColor) }},
            };

            Color C = colorItem.BackColor;

            string CtrlName = ((ColorItem)sender).Name.ToString().ToLower();

            if (CtrlName == "activetitle_pick".ToLower())
                CList.Add(retroDesktopColors1, [nameof(retroDesktopColors1.ActiveTitle)]);

            else if (CtrlName == "GActivetitle_pick".ToLower())
                CList.Add(retroDesktopColors1, [nameof(retroDesktopColors1.GradientActiveTitle)]);

            else if (CtrlName == "TitleText_pick".ToLower())
                CList.Add(retroDesktopColors1, [nameof(retroDesktopColors1.TitleText)]);

            else if (CtrlName == "InactiveTitle_pick".ToLower())
                CList.Add(retroDesktopColors1, [nameof(retroDesktopColors1.InactiveTitle)]);

            else if (CtrlName == "GInactivetitle_pick".ToLower())
                CList.Add(retroDesktopColors1, [nameof(retroDesktopColors1.GradientInactiveTitle)]);

            else if (CtrlName == "InactivetitleText_pick".ToLower())
                CList.Add(retroDesktopColors1, [nameof(retroDesktopColors1.InactiveTitleText)]);

            else if (CtrlName == "ActiveBorder_pick".ToLower())
                CList.Add(retroDesktopColors1, [nameof(retroDesktopColors1.ActiveBorder)]);

            else if (CtrlName == "InactiveBorder_pick".ToLower())
                CList.Add(retroDesktopColors1, [nameof(retroDesktopColors1.InactiveBorder)]);

            else if (CtrlName == "Frame_pick".ToLower())
            {
                CList.Add(retroDesktopColors1, [nameof(retroDesktopColors1.WindowFrame)]);
                CList.Add(Retro3DPreview1, [nameof(Retro3DPreview1.WindowFrame)]);
            }

            else if (CtrlName == "btnface_pick".ToLower())
            {
                CList.Add(retroDesktopColors1, [nameof(retroDesktopColors1.ButtonFace)]);
                CList.Add(Retro3DPreview1, [nameof(Retro3DPreview1.BackColor)]);
            }

            else if (CtrlName == "btndkshadow_pick".ToLower())
            {
                CList.Add(retroDesktopColors1, [nameof(retroDesktopColors1.ButtonDkShadow)]);
                CList.Add(Retro3DPreview1, [nameof(Retro3DPreview1.ButtonDkShadow)]);
            }

            else if (CtrlName == "btnhilight_pick".ToLower())
            {
                CList.Add(retroDesktopColors1, [nameof(retroDesktopColors1.ButtonHilight)]);
                CList.Add(Retro3DPreview1, [nameof(Retro3DPreview1.ButtonHilight)]);
            }

            else if (CtrlName == "btnlight_pick".ToLower())
            {
                CList.Add(retroDesktopColors1, [nameof(retroDesktopColors1.ButtonLight)]);
                CList.Add(Retro3DPreview1, [nameof(Retro3DPreview1.ButtonLight)]);
            }

            else if (CtrlName == "btnshadow_pick".ToLower())
            {
                CList.Add(retroDesktopColors1, [nameof(retroDesktopColors1.ButtonShadow)]);
                CList.Add(Retro3DPreview1, [nameof(Retro3DPreview1.ButtonShadow)]);
            }

            else if (CtrlName == "btntext_pick".ToLower())
            {
                CList.Add(retroDesktopColors1, [nameof(retroDesktopColors1.ButtonText)]);
                CList.Add(Retro3DPreview1, [nameof(Retro3DPreview1.ForeColor)]);
            }

            else if (CtrlName == "AppWorkspace_pick".ToLower())
                CList.Add(retroDesktopColors1, [nameof(retroDesktopColors1.AppWorkspace)]);

            else if (CtrlName == "background_pick".ToLower())
                CList.Add(retroDesktopColors1, [nameof(retroDesktopColors1.Background)]);

            else if (CtrlName == "menu_pick".ToLower())
                CList.Add(retroDesktopColors1, [nameof(retroDesktopColors1.Menu)]);

            else if (CtrlName == "menubar_pick".ToLower())
                CList.Add(retroDesktopColors1, [nameof(retroDesktopColors1.MenuBar)]);

            else if (CtrlName == "hilight_pick".ToLower())
                CList.Add(retroDesktopColors1, [nameof(retroDesktopColors1.Hilight)]);

            else if (CtrlName == "menuhilight_pick".ToLower())
                CList.Add(retroDesktopColors1, [nameof(retroDesktopColors1.MenuHilight)]);

            else if (CtrlName == "menutext_pick".ToLower())
                CList.Add(retroDesktopColors1, [nameof(retroDesktopColors1.MenuText)]);

            else if (CtrlName == "hilighttext_pick".ToLower())
                CList.Add(retroDesktopColors1, [nameof(retroDesktopColors1.HilightText)]);

            else if (CtrlName == "GrayText_pick".ToLower())
                CList.Add(retroDesktopColors1, [nameof(retroDesktopColors1.GrayText)]);

            else if (CtrlName == "Window_pick".ToLower())
                CList.Add(retroDesktopColors1, [nameof(retroDesktopColors1.Window)]);

            else if (CtrlName == "WindowText_pick".ToLower())
                CList.Add(retroDesktopColors1, [nameof(retroDesktopColors1.WindowText)]);

            else if (CtrlName == "InfoWindow_pick".ToLower())
                CList.Add(retroDesktopColors1, [nameof(retroDesktopColors1.InfoWindow)]);

            else if (CtrlName == "InfoText_pick".ToLower())
                CList.Add(retroDesktopColors1, [nameof(retroDesktopColors1.InfoText)]);

            C = Forms.ColorPickerDlg.Pick(CList);

            foreach (KeyValuePair<Control, string[]> item in CList)
            {
                foreach (string prop in item.Value)
                {
                    item.Key.SetProperty(prop, C);
                }
            }

            CList.Clear();
        }

        private void ColorItem_DragDrop(object sender, DragEventArgs e)
        {
            ApplyRetroPreview();
        }

        public void LoadFromWin9xTheme(string File, Theme.Structures.Win32UI _DefWin32)
        {
            if (System.IO.File.Exists(File))
            {
                using (INI _ini = new(File))
                {
                    string Section = @"Control Panel\Colors";

                    TitleText_pick.BackColor = _ini.Read(Section, "TitleText", _DefWin32.TitleText.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true)).ToColorFromWin32();
                    InactivetitleText_pick.BackColor = _ini.Read(Section, "InactiveTitleText", _DefWin32.InactiveTitleText.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true)).ToColorFromWin32();
                    ActiveBorder_pick.BackColor = _ini.Read(Section, "ActiveBorder", _DefWin32.ActiveBorder.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true)).ToColorFromWin32();
                    InactiveBorder_pick.BackColor = _ini.Read(Section, "InactiveBorder", _DefWin32.InactiveBorder.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true)).ToColorFromWin32();
                    activetitle_pick.BackColor = _ini.Read(Section, "ActiveTitle", _DefWin32.ActiveTitle.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true)).ToColorFromWin32();
                    InactiveTitle_pick.BackColor = _ini.Read(Section, "InactiveTitle", _DefWin32.InactiveTitle.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true)).ToColorFromWin32();

                    Color GA = _ini.Read(Section, "GradientActiveTitle").ToColorFromWin32();
                    Color GI = _ini.Read(Section, "GradientInactiveTitle").ToColorFromWin32();

                    if (GA != Color.Empty)
                    {
                        GActivetitle_pick.BackColor = GA;
                    }
                    else
                    {
                        GActivetitle_pick.BackColor = activetitle_pick.BackColor;
                    }

                    if (GI != Color.Empty)
                    {
                        GInactivetitle_pick.BackColor = GA;
                    }
                    else
                    {
                        GInactivetitle_pick.BackColor = InactiveTitle_pick.BackColor;
                    }

                    btnface_pick.BackColor = _ini.Read(Section, "ButtonFace", _DefWin32.ButtonFace.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true)).ToColorFromWin32();
                    btnshadow_pick.BackColor = _ini.Read(Section, "ButtonShadow", _DefWin32.ButtonShadow.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true)).ToColorFromWin32();
                    btntext_pick.BackColor = _ini.Read(Section, "ButtonText", _DefWin32.ButtonText.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true)).ToColorFromWin32();
                    btnhilight_pick.BackColor = _ini.Read(Section, "ButtonHilight", _DefWin32.ButtonHilight.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true)).ToColorFromWin32();
                    btndkshadow_pick.BackColor = _ini.Read(Section, "ButtonDkShadow", _DefWin32.ButtonDkShadow.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true)).ToColorFromWin32();
                    btnlight_pick.BackColor = _ini.Read(Section, "ButtonLight", _DefWin32.ButtonLight.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true)).ToColorFromWin32();
                    btnaltface_pick.BackColor = _ini.Read(Section, "ButtonAlternateFace", _DefWin32.ButtonAlternateFace.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true)).ToColorFromWin32();
                    background_pick.BackColor = _ini.Read(Section, "Background", _DefWin32.Background.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true)).ToColorFromWin32();
                    hilight_pick.BackColor = _ini.Read(Section, "Hilight", _DefWin32.Hilight.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true)).ToColorFromWin32();
                    hilighttext_pick.BackColor = _ini.Read(Section, "HilightText", _DefWin32.HilightText.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true)).ToColorFromWin32();
                    Window_pick.BackColor = _ini.Read(Section, "Window", _DefWin32.Window.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true)).ToColorFromWin32();
                    WindowText_pick.BackColor = _ini.Read(Section, "WindowText", _DefWin32.WindowText.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true)).ToColorFromWin32();
                    Scrollbar_pick.BackColor = _ini.Read(Section, "ScrollBar", _DefWin32.Scrollbar.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true)).ToColorFromWin32();
                    menu_pick.BackColor = _ini.Read(Section, "Menu", _DefWin32.Menu.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true)).ToColorFromWin32();
                    Frame_pick.BackColor = _ini.Read(Section, "WindowFrame", _DefWin32.WindowFrame.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true)).ToColorFromWin32();
                    menutext_pick.BackColor = _ini.Read(Section, "MenuText", _DefWin32.MenuText.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true)).ToColorFromWin32();
                    AppWorkspace_pick.BackColor = _ini.Read(Section, "AppWorkspace", _DefWin32.AppWorkspace.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true)).ToColorFromWin32();
                    GrayText_pick.BackColor = _ini.Read(Section, "GrayText", _DefWin32.GrayText.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true)).ToColorFromWin32();
                    InfoText_pick.BackColor = _ini.Read(Section, "InfoText", _DefWin32.InfoText.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true)).ToColorFromWin32();
                    InfoWindow_pick.BackColor = _ini.Read(Section, "InfoWindow", _DefWin32.InfoWindow.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true)).ToColorFromWin32();
                    hottracking_pick.BackColor = _ini.Read(Section, "HotTrackingColor", _DefWin32.HotTrackingColor.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true)).ToColorFromWin32();
                    menubar_pick.BackColor = _ini.Read(Section, "MenuBar", _DefWin32.MenuBar.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true)).ToColorFromWin32();
                    menuhilight_pick.BackColor = _ini.Read(Section, "MenuHilight", _DefWin32.MenuHilight.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true)).ToColorFromWin32();
                    desktop_pick.BackColor = _ini.Read(Section, "Desktop", _DefWin32.Desktop.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true)).ToColorFromWin32();

                    _btn_shadow = _ini.Read(Section, "ButtonShadow", _DefWin32.ButtonShadow.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true)).ToColorFromWin32();
                    _btn_hilight = _ini.Read(Section, "ButtonHilight", _DefWin32.ButtonHilight.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true)).ToColorFromWin32();
                    _btn_light = _ini.Read(Section, "ButtonLight", _DefWin32.ButtonLight.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true)).ToColorFromWin32();
                    _btn_dkshadow = _ini.Read(Section, "ButtonDkShadow", _DefWin32.ButtonDkShadow.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true)).ToColorFromWin32();
                    trackBarX1.Value = 100;
                }
            }

            ApplyRetroPreview();
        }

        public void ApplyRetroPreview()
        {
            retroDesktopColors1.Visible = false;

            retroDesktopColors1.EnableGradient = Toggle2.Checked;
            retroDesktopColors1.InactiveTitle = InactiveTitle_pick.BackColor;
            retroDesktopColors1.GradientInactiveTitle = GInactivetitle_pick.BackColor;
            retroDesktopColors1.InactiveTitleText = InactivetitleText_pick.BackColor;
            retroDesktopColors1.InactiveBorder = InactiveBorder_pick.BackColor;
            retroDesktopColors1.ActiveTitle = activetitle_pick.BackColor;
            retroDesktopColors1.GradientActiveTitle = GActivetitle_pick.BackColor;
            retroDesktopColors1.TitleText = TitleText_pick.BackColor;
            retroDesktopColors1.ActiveBorder = ActiveBorder_pick.BackColor;

            retroDesktopColors1.ButtonFace = btnface_pick.BackColor;
            retroDesktopColors1.ButtonDkShadow = btndkshadow_pick.BackColor;
            retroDesktopColors1.ButtonHilight = btnhilight_pick.BackColor;
            retroDesktopColors1.ButtonLight = btnlight_pick.BackColor;
            retroDesktopColors1.ButtonShadow = btnshadow_pick.BackColor;
            retroDesktopColors1.ButtonText = btntext_pick.BackColor;
            retroDesktopColors1.WindowFrame = Frame_pick.BackColor;

            retroDesktopColors1.Window = Window_pick.BackColor;
            retroDesktopColors1.WindowText = WindowText_pick.BackColor;

            retroDesktopColors1.Menu = menu_pick.BackColor;
            retroDesktopColors1.MenuBar = menubar_pick.BackColor;
            retroDesktopColors1.MenuText = menutext_pick.BackColor;
            retroDesktopColors1.MenuHilight = menuhilight_pick.BackColor;
            retroDesktopColors1.Hilight = hilight_pick.BackColor;
            retroDesktopColors1.HilightText = hilighttext_pick.BackColor;
            retroDesktopColors1.Scrollbar = Scrollbar_pick.BackColor;
            retroDesktopColors1.GrayText = GrayText_pick.BackColor;

            retroDesktopColors1.AppWorkspace = AppWorkspace_pick.BackColor;
            retroDesktopColors1.Background = background_pick.BackColor;
            retroDesktopColors1.Desktop = desktop_pick.BackColor;
            retroDesktopColors1.InfoText = InfoText_pick.BackColor;
            retroDesktopColors1.InfoWindow = InfoWindow_pick.BackColor;
            retroDesktopColors1.HotTrackingColor = hottracking_pick.BackColor;

            retroDesktopColors1.Visible = true;

            Retro3DPreview1.BackColor = btnface_pick.BackColor;
            Retro3DPreview1.ButtonDkShadow = btndkshadow_pick.BackColor;
            Retro3DPreview1.WindowFrame = Frame_pick.BackColor;
            Retro3DPreview1.ButtonHilight = btnhilight_pick.BackColor;
            Retro3DPreview1.ButtonLight = btnlight_pick.BackColor;
            Retro3DPreview1.ButtonShadow = btnshadow_pick.BackColor;
            Retro3DPreview1.ForeColor = btntext_pick.BackColor;
            Retro3DPreview1.Refresh();
        }

        private void Toggle1_CheckedChanged(object sender, EventArgs e)
        {
            retroDesktopColors1.EnableTheming = Toggle1.Checked;
        }

        private void Toggle2_CheckedChanged(object sender, EventArgs e)
        {
            retroDesktopColors1.EnableGradient = Toggle2.Checked;
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ComboBox1.SelectedItem.ToString())) return;

            bool condition0 = ComboBox1.SelectedIndex <= 7;
            bool condition1 = ComboBox1.SelectedItem.ToString().StartsWith("Windows Classic (3.1)");
            bool condition2 = ComboBox1.SelectedItem.ToString().StartsWith("Windows 3.1 - ");

            Toggle1.Checked = condition0 | condition1 | condition2;
            Toggle2.Checked = true;

            retroDesktopColors1.LoadFromWinThemeString(Schemes.ClassicColors, ComboBox1.SelectedItem.ToString());

            LoadFromRetroPreview(retroDesktopColors1);
        }


        private void Form_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            Process.Start(Links.Wiki.ClassicColors);
        }

        public void retroDesktopColors1_EditorInvoker(object sender, EditorEventArgs e)
        {
            Dictionary<Control, string[]> CList = new()
            {
                { retroDesktopColors1, new string[] { e.PropertyName }}
            };

            if (sender != retroDesktopColors1)
            {
                CList.Add((Control)sender, [e.PropertyName]);
            }

            if (Forms.Win32UI_Fullscreen?.retroDesktopColors1 != null && sender != Forms.Win32UI_Fullscreen?.retroDesktopColors1)
            {
                CList.Add(Forms.Win32UI_Fullscreen.retroDesktopColors1, [e.PropertyName]);
            }

            if (e.PropertyName.ToLower() == nameof(retroDesktopColors1.ActiveTitle).ToLower())
                CList.Add(activetitle_pick, [nameof(activetitle_pick.BackColor)]);

            else if (e.PropertyName.ToLower() == nameof(retroDesktopColors1.GradientActiveTitle).ToLower())
                CList.Add(GActivetitle_pick, [nameof(GActivetitle_pick.BackColor)]);

            else if (e.PropertyName.ToLower() == nameof(retroDesktopColors1.TitleText).ToLower())
                CList.Add(TitleText_pick, [nameof(TitleText_pick.BackColor)]);

            else if (e.PropertyName.ToLower() == nameof(retroDesktopColors1.InactiveTitle).ToLower())
                CList.Add(InactiveTitle_pick, [nameof(InactiveTitle_pick.BackColor)]);

            else if (e.PropertyName.ToLower() == nameof(retroDesktopColors1.GradientInactiveTitle).ToLower())
                CList.Add(GInactivetitle_pick, [nameof(GInactivetitle_pick.BackColor)]);

            else if (e.PropertyName.ToLower() == nameof(retroDesktopColors1.InactiveTitleText).ToLower())
                CList.Add(InactivetitleText_pick, [nameof(InactivetitleText_pick.BackColor)]);

            else if (e.PropertyName.ToLower() == nameof(retroDesktopColors1.ActiveBorder).ToLower())
                CList.Add(ActiveBorder_pick, [nameof(ActiveBorder_pick.BackColor)]);

            else if (e.PropertyName.ToLower() == nameof(retroDesktopColors1.InactiveBorder).ToLower())
                CList.Add(InactiveBorder_pick, [nameof(InactiveBorder_pick.BackColor)]);

            else if (e.PropertyName.ToLower() == nameof(retroDesktopColors1.WindowFrame).ToLower())
            {
                CList.Add(Retro3DPreview1, [nameof(Retro3DPreview1.WindowFrame)]);
                CList.Add(Frame_pick, [nameof(Frame_pick.BackColor)]);
            }

            else if (e.PropertyName.ToLower() == nameof(retroDesktopColors1.ButtonFace).ToLower())
            {
                CList.Add(Retro3DPreview1, [nameof(Retro3DPreview1.BackColor)]);
                CList.Add(btnface_pick, [nameof(btnface_pick.BackColor)]);
            }

            else if (e.PropertyName.ToLower() == nameof(retroDesktopColors1.ButtonDkShadow).ToLower())
            {
                CList.Add(Retro3DPreview1, [nameof(Retro3DPreview1.ButtonDkShadow)]);
                CList.Add(btndkshadow_pick, [nameof(btndkshadow_pick.BackColor)]);
            }

            else if (e.PropertyName.ToLower() == nameof(retroDesktopColors1.ButtonHilight).ToLower())
            {
                CList.Add(Retro3DPreview1, [nameof(Retro3DPreview1.ButtonHilight)]);
                CList.Add(btnhilight_pick, [nameof(btnhilight_pick.BackColor)]);
            }

            else if (e.PropertyName.ToLower() == nameof(retroDesktopColors1.ButtonLight).ToLower())
            {
                CList.Add(Retro3DPreview1, [nameof(Retro3DPreview1.ButtonLight)]);
                CList.Add(btnlight_pick, [nameof(btnlight_pick.BackColor)]);
            }

            else if (e.PropertyName.ToLower() == nameof(retroDesktopColors1.ButtonShadow).ToLower())
            {
                CList.Add(Retro3DPreview1, [nameof(Retro3DPreview1.ButtonShadow)]);
                CList.Add(btnshadow_pick, [nameof(btnshadow_pick.BackColor)]);
            }

            else if (e.PropertyName.ToLower() == nameof(retroDesktopColors1.ButtonText).ToLower())
            {
                CList.Add(Retro3DPreview1, [nameof(Retro3DPreview1.ForeColor)]);
                CList.Add(btntext_pick, [nameof(btntext_pick.BackColor)]);
            }

            else if (e.PropertyName.ToLower() == nameof(retroDesktopColors1.AppWorkspace).ToLower())
                CList.Add(AppWorkspace_pick, [nameof(AppWorkspace_pick.BackColor)]);

            else if (e.PropertyName.ToLower() == nameof(retroDesktopColors1.Background).ToLower())
                CList.Add(background_pick, [nameof(background_pick.BackColor)]);

            else if (e.PropertyName.ToLower() == nameof(retroDesktopColors1.Menu).ToLower())
                CList.Add(menu_pick, [nameof(menu_pick.BackColor)]);

            else if (e.PropertyName.ToLower() == nameof(retroDesktopColors1.MenuBar).ToLower())
                CList.Add(menubar_pick, [nameof(menubar_pick.BackColor)]);

            else if (e.PropertyName.ToLower() == nameof(retroDesktopColors1.Hilight).ToLower())
                CList.Add(hilight_pick, [nameof(hilight_pick.BackColor)]);

            else if (e.PropertyName.ToLower() == nameof(retroDesktopColors1.MenuHilight).ToLower())
                CList.Add(menuhilight_pick, [nameof(menuhilight_pick.BackColor)]);

            else if (e.PropertyName.ToLower() == nameof(retroDesktopColors1.HilightText).ToLower())
                CList.Add(hilighttext_pick, [nameof(hilighttext_pick.BackColor)]);

            else if (e.PropertyName.ToLower() == nameof(retroDesktopColors1.HotTrackingColor).ToLower())
                CList.Add(hottracking_pick, [nameof(hottracking_pick.BackColor)]);

            else if (e.PropertyName.ToLower() == nameof(retroDesktopColors1.Scrollbar).ToLower())
                CList.Add(Scrollbar_pick, [nameof(Scrollbar_pick.BackColor)]);

            else if (e.PropertyName.ToLower() == nameof(retroDesktopColors1.Desktop).ToLower())
                CList.Add(desktop_pick, [nameof(desktop_pick.BackColor)]);

            else if (e.PropertyName.ToLower() == nameof(retroDesktopColors1.Window).ToLower())
                CList.Add(Window_pick, [nameof(Window_pick.BackColor)]);

            else if (e.PropertyName.ToLower() == nameof(retroDesktopColors1.WindowText).ToLower())
                CList.Add(WindowText_pick, [nameof(WindowText_pick.BackColor)]);

            else if (e.PropertyName.ToLower() == nameof(retroDesktopColors1.GrayText).ToLower())
                CList.Add(GrayText_pick, [nameof(GrayText_pick.BackColor)]);

            else if (e.PropertyName.ToLower() == nameof(retroDesktopColors1.InfoWindow).ToLower())
                CList.Add(InfoWindow_pick, [nameof(InfoWindow_pick.BackColor)]);

            else if (e.PropertyName.ToLower() == nameof(retroDesktopColors1.InfoText).ToLower())
                CList.Add(InfoText_pick, [nameof(InfoText_pick.BackColor)]);

            if (CList.ElementAt(CList.Count - 1).Key is ColorItem colorItem)
            {
                if (colorItem.Parent != null && colorItem.Parent is TabPage)
                {
                    TabControl1.SelectedIndex = TabControl1.TabPages.IndexOf((TabPage)colorItem.Parent);
                }

                else if (colorItem.Parent != null && colorItem.Parent is UI.WP.GroupBox && colorItem.Parent.Parent != null & colorItem.Parent.Parent is TabPage)
                {
                    TabControl1.SelectedIndex = TabControl1.TabPages.IndexOf((TabPage)colorItem.Parent.Parent);
                }
            }

            Color C = Forms.ColorPickerDlg.Pick(CList);

            retroDesktopColors1.SetProperty(e.PropertyName ?? "BackColor", C);
            ((Control)sender).SetProperty(e.PropertyName ?? "BackColor", C);

            if (Forms.Win32UI_Fullscreen?.retroDesktopColors1 != null)
            {
                Forms.Win32UI_Fullscreen.retroDesktopColors1.SetProperty(e.PropertyName ?? "BackColor", C);
            }

            CList.ElementAt(CList.Count - 1).Key.SetProperty(CList.ElementAt(CList.Count - 1).Value.FirstOrDefault(), C);

            CList.Clear();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            magnifier1.Enabled = checkBox1.Checked;
            splitContainer1.Panel2Collapsed = !checkBox1.Checked;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            string result = Forms.Win32UI_Gallery.PickATheme();

            if (!string.IsNullOrWhiteSpace(result) && ComboBox1.Items.Contains(result))
            {
                ComboBox1.SelectedItem = result;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                Forms.Win32UI_Fullscreen.ShowDialog();
                checkBox2.Checked = false;
            }
        }

        private async Task<Task> Change3DStyle()
        {
            return Task.Run(() =>
            {
                if (trackBarX1.Value == 100)
                {
                    retroDesktopColors1.ButtonDkShadow = _btn_dkshadow;
                    retroDesktopColors1.ButtonHilight = _btn_hilight;
                    retroDesktopColors1.ButtonLight = _btn_light;
                    retroDesktopColors1.ButtonShadow = _btn_shadow;
                }
                else
                {
                    if (radioButton2.Checked)
                    {
                        if (trackBarX1.Value > 100f)
                        {
                            float amount = 1f - ((trackBarX1.Value - 100f) / 100f);
                            retroDesktopColors1.ButtonDkShadow = _btn_dkshadow.Blend(_btn_dkshadow.DarkDark(), amount);
                            retroDesktopColors1.ButtonShadow = _btn_shadow.Blend(_btn_shadow.DarkDark(), amount);
                            retroDesktopColors1.ButtonHilight = _btn_hilight.Blend(_btn_hilight.Dark(), amount);
                            retroDesktopColors1.ButtonLight = _btn_light.Blend(_btn_light.Dark(), amount);
                        }
                        else if (trackBarX1.Value < 100f)
                        {
                            float amount = trackBarX1.Value / 100f;
                            retroDesktopColors1.ButtonDkShadow = _btn_dkshadow.Blend(_btn_hilight, amount);
                            retroDesktopColors1.ButtonShadow = _btn_shadow.Blend(_btn_light, amount);
                            retroDesktopColors1.ButtonHilight = _btn_hilight.Blend(_btn_dkshadow, amount);
                            retroDesktopColors1.ButtonLight = _btn_light.Blend(_btn_shadow, amount);
                        }
                    }
                    else
                    {
                        if (trackBarX1.Value > 100f)
                        {
                            float amount = 1f - ((trackBarX1.Value - 100f) / 100f);
                            retroDesktopColors1.ButtonDkShadow = _btn_dkshadow.Blend(_btn_shadow, amount);
                            retroDesktopColors1.ButtonShadow = _btn_shadow.Blend(retroDesktopColors1.ButtonFace, amount);
                            retroDesktopColors1.ButtonHilight = _btn_hilight.Blend(_btn_shadow, amount);
                            retroDesktopColors1.ButtonLight = _btn_light.Blend(retroDesktopColors1.ButtonFace, amount);
                        }
                        else if (trackBarX1.Value < 100f)
                        {
                            float amount = trackBarX1.Value / 100f;
                            retroDesktopColors1.ButtonDkShadow = _btn_dkshadow.Blend(retroDesktopColors1.ButtonFace, amount);
                            retroDesktopColors1.ButtonShadow = _btn_shadow.Blend(retroDesktopColors1.ButtonFace, amount);
                            retroDesktopColors1.ButtonHilight = _btn_hilight.Blend(retroDesktopColors1.ButtonFace, amount);
                            retroDesktopColors1.ButtonLight = _btn_light.Blend(retroDesktopColors1.ButtonFace, amount);
                        }
                    }
                }

                btnshadow_pick.BackColor = retroDesktopColors1.ButtonShadow;
                btndkshadow_pick.BackColor = retroDesktopColors1.ButtonDkShadow;
                btnhilight_pick.BackColor = retroDesktopColors1.ButtonHilight;
                btnlight_pick.BackColor = retroDesktopColors1.ButtonLight;

                Retro3DPreview1.ButtonDkShadow = retroDesktopColors1.ButtonDkShadow;
                Retro3DPreview1.ButtonHilight = retroDesktopColors1.ButtonHilight;
                Retro3DPreview1.ButtonLight = retroDesktopColors1.ButtonLight;
                Retro3DPreview1.ButtonShadow = retroDesktopColors1.ButtonShadow;
            });
        }

        private async void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            await Change3DStyle();
        }

        private async void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            await Change3DStyle();
        }

        private void ColorItem_ContextMenuMadeColorChangeInvoker(object sender, ColorItem.ContextMenuMadeColorChangeEventArgs e)
        {
            ApplyRetroPreview();
        }

        private async void trackBarX1_ValueChanged(object sender, EventArgs e)
        {
            await Change3DStyle();
        }
    }
}