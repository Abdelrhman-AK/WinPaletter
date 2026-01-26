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
using WinPaletter.Theme.Structures;
using WinPaletter.UI.Controllers;
using static WinPaletter.PreviewHelpers;

namespace WinPaletter
{
    public partial class WindowsTerminal
    {
        public WinTerminal.Version Mode;
        public WinTerminal _Terminal;
        public WinTerminal _TerminalDefault;
        public WinTerminal.Version SaveState;
        public string CCat;

        private void Form_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            Process.Start(Links.Wiki.Terminals);
        }

        public WindowsTerminal()
        {
            SaveState = Mode;
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
                        _Terminal = Mode == WinTerminal.Version.Stable ? TMx.Terminal : TMx.TerminalPreview;
                        Load_FromTerminal();
                    }
                }
            }
        }

        private void LoadFromCurrent(object sender, EventArgs e)
        {
            Manager TMx = new(Manager.Source.Registry);
            _Terminal = Mode == WinTerminal.Version.Stable ? TMx.Terminal : TMx.TerminalPreview;
            Load_FromTerminal();
            TMx.Dispose();
        }

        private void LoadFromDefault(object sender, EventArgs e)
        {
            Manager TMx = Default.FromOS(Program.WindowStyle);
            _Terminal = Mode == WinTerminal.Version.Stable ? TMx.Terminal : TMx.TerminalPreview;
            Load_FromTerminal();
            TMx.Dispose();
        }

        private void LoadIntoCurrentTheme(object sender, EventArgs e)
        {
            switch (Mode)
            {
                case WinTerminal.Version.Stable:
                    {
                        Program.TM.Terminal.Enabled = AspectEnabled;
                        Program.TM.Terminal = _Terminal;
                        break;
                    }

                case WinTerminal.Version.Preview:
                    {
                        Program.TM.TerminalPreview.Enabled = AspectEnabled;
                        Program.TM.TerminalPreview = _Terminal;
                        break;
                    }
            }

            DialogResult = DialogResult.OK;

            Close();
        }

        private void ImportFromJSON(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new() { Filter = Program.Filters.JSON, Title = Program.Localization.Strings.Extensions.OpenJSON })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        if (Mode == WinTerminal.Version.Stable)
                        {
                            _Terminal = new(dlg.FileName, WinTerminal.Mode.JSONFile);
                        }
                        else if (Mode == WinTerminal.Version.Preview)
                        {
                            _Terminal = new(dlg.FileName, WinTerminal.Mode.JSONFile, WinTerminal.Version.Preview);
                        }
                    }
                    catch (Exception ex)
                    {
                        Forms.BugReport.Throw(ex);
                    }
                    finally
                    {
                        Load_FromTerminal();
                    }
                }
            }
        }

        private void Apply(object sender, EventArgs e)
        {
            if (Program.Settings.AspectsControl.Enabled && !Program.Settings.AspectsControl.WinTerminals)
            {
                MsgBox(Program.Localization.Strings.Aspects.Disabled_Apply_0, MessageBoxButtons.OK, MessageBoxIcon.Warning, Program.Localization.Strings.Aspects.Disabled_Apply_1);
                return;
            }

            Cursor = System.Windows.Forms.Cursors.WaitCursor;

            if (AspectEnabled)
            {
                if (Program.Settings.BackupTheme.Enabled && Program.Settings.BackupTheme.AutoBackupOnApplySingleAspect)
                {
                    using (Manager TMx = new(Manager.Source.Registry))
                    {
                        string filename = Program.GetUniqueFileName($"{Program.Settings.BackupTheme.BackupPath}\\OnAspectApply", $"{TMx.Info.ThemeName}_{DateTime.Now.Hour}.{DateTime.Now.Minute}.{DateTime.Now.Second}.wpth");
                        TMx.Save(Manager.Source.File, filename);
                    }
                }

                if (OS.W12 || OS.W11 || OS.W10)
                {
                    Cursor = System.Windows.Forms.Cursors.WaitCursor;

                    string TerDir;
                    string TerPreDir;

                    if (!Program.Settings.WindowsTerminals.Path_Deflection)
                    {
                        TerDir = SysPaths.TerminalJSON;
                        TerPreDir = SysPaths.TerminalPreviewJSON;
                    }
                    else
                    {
                        if (File.Exists(Program.Settings.WindowsTerminals.Terminal_Stable_Path))
                        {
                            TerDir = Program.Settings.WindowsTerminals.Terminal_Stable_Path;
                        }
                        else
                        {
                            TerDir = SysPaths.TerminalJSON;
                        }

                        if (File.Exists(Program.Settings.WindowsTerminals.Terminal_Preview_Path))
                        {
                            TerPreDir = Program.Settings.WindowsTerminals.Terminal_Preview_Path;
                        }
                        else
                        {
                            TerPreDir = SysPaths.TerminalPreviewJSON;
                        }
                    }

                    if (File.Exists(TerDir) && Mode == WinTerminal.Version.Stable)
                    {
                        _Terminal.Save(TerDir, WinTerminal.Mode.JSONFile);
                    }
                    else if (File.Exists(TerPreDir) && Mode == WinTerminal.Version.Preview)
                    {
                        _Terminal.Save(TerPreDir, WinTerminal.Mode.JSONFile, WinTerminal.Version.Preview);
                    }

                    Cursor = System.Windows.Forms.Cursors.Default;
                }
            }
            else
            {
                MsgBox(Program.Localization.Strings.Aspects.Consoles.CMD_Enable, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Cursor = System.Windows.Forms.Cursors.Default;
        }

        private void WindowsTerminal_Load(object sender, EventArgs e)
        {
            DesignerData data = new(this)
            {
                AspectName = Mode == WinTerminal.Version.Stable ? Program.Localization.Strings.Aspects.TerminalStable : Program.Localization.Strings.Aspects.TerminalPreview,
                Enabled = Mode == WinTerminal.Version.Stable ? Program.TM.Terminal.Enabled : Program.TM.TerminalPreview.Enabled,
                Import_theme = false,
                Import_msstyles = false,
                GeneratePalette = false,
                GenerateMSTheme = false,
                Import_preset = false,
                Import_JSON = true,
                CanSwitchMode = false,

                OnLoadIntoCurrentTheme = LoadIntoCurrentTheme,
                OnApply = Apply,
                OnImportFromDefault = LoadFromDefault,
                OnImportFromWPTH = LoadFromWPTH,
                OnImportFromCurrentApplied = LoadFromCurrent,
                OnImportFromJSON = ImportFromJSON,
            };

            LoadData(data);

            toggle1.Checked = Program.Settings.WindowsTerminals.ListAllFonts;

            switch (Mode)
            {
                case WinTerminal.Version.Stable:
                    {
                        _Terminal = Program.TM.Terminal;
                        _TerminalDefault = Program.TM.Terminal;
                        Text = Program.Localization.Strings.Aspects.TerminalStable;
                        AspectEnabled = Program.TM.Terminal.Enabled;
                        break;
                    }

                case WinTerminal.Version.Preview:
                    {
                        _Terminal = Program.TM.TerminalPreview;
                        _TerminalDefault = Program.TM.TerminalPreview;

                        Text = Program.Localization.Strings.Aspects.TerminalPreview;
                        AspectEnabled = Program.TM.TerminalPreview.Enabled;
                        break;
                    }

            }

            Load_FromTerminal();
        }

        protected override void OnDragOver(DragEventArgs e)
        {
            if (e.Data.GetData(typeof(ColorItem).FullName) is ColorItem)
            {
                Focus();
                BringToFront();
            }
            else
            {
                return;
            }

            base.OnDragOver(e);
        }

        public void Load_FromTerminal()
        {
            AspectEnabled = _Terminal.Enabled;

            FillTerminalSchemes(_Terminal, TerSchemes);
            FillTerminalThemes(_Terminal, TerThemes);
            FillTerminalProfiles(_Terminal, TerProfiles);

            TerProfiles.SelectedIndex = 0;

            Terminal1.PreviewVersion = Mode == WinTerminal.Version.Preview;
            Terminal2.PreviewVersion = Mode == WinTerminal.Version.Preview;

            if (_Terminal.Theme != null)
            {
                if (_Terminal.Theme.ToLower() == "dark")
                {
                    TerThemes.SelectedIndex = 1;
                    TerTitlebarActive.BackColor = default;
                    TerTitlebarInactive.BackColor = default;
                    TerTabActive.BackColor = default;
                    TerTabInactive.BackColor = default;
                    TerMode.Checked = true;
                    Terminal1.Light = false;
                    Terminal2.Light = false;
                }

                else if (_Terminal.Theme.ToLower() == "light")
                {
                    TerThemes.SelectedIndex = 2;
                    TerTitlebarActive.BackColor = default;
                    TerTitlebarInactive.BackColor = default;
                    TerTabActive.BackColor = default;
                    TerTabInactive.BackColor = default;
                    TerMode.Checked = false;
                    Terminal1.Light = true;
                    Terminal2.Light = true;
                }

                else if (_Terminal.Theme.ToLower() == "system")
                {
                    TerThemes.SelectedIndex = 3;
                    TerTitlebarActive.BackColor = default;
                    TerTitlebarInactive.BackColor = default;
                    TerTabActive.BackColor = default;
                    TerTabInactive.BackColor = default;

                    switch (Program.WindowStyle)
                    {
                        case WindowStyle.W12:
                            {
                                TerMode.Checked = !Program.TM.Windows12.AppMode_Light;
                                Terminal1.Light = Program.TM.Windows12.AppMode_Light;
                                Terminal2.Light = Program.TM.Windows12.AppMode_Light;
                                break;
                            }

                        case WindowStyle.W11:
                            {
                                TerMode.Checked = !Program.TM.Windows11.AppMode_Light;
                                Terminal1.Light = Program.TM.Windows11.AppMode_Light;
                                Terminal2.Light = Program.TM.Windows11.AppMode_Light;
                                break;
                            }

                        case WindowStyle.W10:
                            {
                                TerMode.Checked = !Program.TM.Windows10.AppMode_Light;
                                Terminal1.Light = Program.TM.Windows10.AppMode_Light;
                                Terminal2.Light = Program.TM.Windows10.AppMode_Light;
                                break;
                            }

                        default:
                            {
                                TerMode.Checked = !Program.TM.Windows12.AppMode_Light;
                                Terminal1.Light = Program.TM.Windows12.AppMode_Light;
                                Terminal2.Light = Program.TM.Windows12.AppMode_Light;
                                break;
                            }
                    }
                }

                else if (TerThemes.Items.Contains(_Terminal.Theme))
                {
                    TerThemes.SelectedItem = _Terminal.Theme;
                    TerThemesContainer.Enabled = true;

                    WinTerminal.Types.Theme temp = _Terminal.Themes[TerThemes.SelectedIndex - 4];
                    TerTitlebarActive.BackColor = temp.TabRow.Background;
                    TerTitlebarInactive.BackColor = temp.TabRow.UnfocusedBackground;
                    TerTabActive.BackColor = temp.Tab.Background;
                    TerTabInactive.BackColor = temp.Tab.UnfocusedBackground;
                    TerMode.Checked = !(temp.Window.ApplicationTheme.ToLower() == "light");
                    Terminal1.Light = !(temp.Window.ApplicationTheme.ToLower() == "light");
                    Terminal2.Light = !(temp.Window.ApplicationTheme.ToLower() == "light");
                }
            }

            ApplyPreview(_Terminal);
        }

        public void FillTerminalSchemes(WinTerminal Terminal, UI.WP.ComboBox Combobox)
        {
            Combobox.Items.Clear();
            Combobox.Items.Add($"({Program.Localization.Strings.General.Default})");

            if (Terminal.Schemes.Count > 0)
            {
                for (int x = 0, loopTo = Terminal.Schemes.Count - 1; x <= loopTo; x++)
                    Combobox.Items.Add(Terminal.Schemes[x].Name);
            }
        }

        public void FillTerminalThemes(WinTerminal Terminal, UI.WP.ComboBox Combobox)
        {
            Combobox.Items.Clear();

            Combobox.Items.Add($"({Program.Localization.Strings.General.Default})");
            Combobox.Items.Add($"{Program.Localization.Strings.General.Dark}");
            Combobox.Items.Add($"{Program.Localization.Strings.General.Light}");
            Combobox.Items.Add($"{Program.Localization.Strings.General.System}");

            if (Terminal.Themes.Count > 0)
            {
                for (int x = 0, loopTo = Terminal.Themes.Count - 1; x <= loopTo; x++) Combobox.Items.Add(Terminal.Themes[x].Name);
            }
        }

        public void FillTerminalProfiles(WinTerminal Terminal, UI.WP.ComboBox Combobox)
        {
            Combobox.Items.Clear();
            Combobox.Items.Add($"{Program.Localization.Strings.General.Defaults}");

            if (Terminal.Profiles.List.Count > 0)
            {
                for (int x = 0, loopTo = Terminal.Profiles.List.Count - 1; x <= loopTo; x++) Combobox.Items.Add(Terminal.Profiles.List[x].Name);
            }
        }

        private void TerSchemes_SelectedIndexChanged(object sender, EventArgs e)
        {
            TerDeleteScheme.Enabled = TerSchemes.SelectedIndex > 0;
            TerEditScheme.Enabled = TerSchemes.SelectedIndex > 0;

            if (TerSchemes.SelectedIndex > -1)
            {
                SetDefaultsToScheme(TerSchemes.SelectedItem.ToString());

                WinTerminal.Types.Scheme temp = new();

                if (TerSchemes.SelectedIndex == 0 && TerProfiles.SelectedIndex > 0)
                {
                    temp = _Terminal.Schemes.Where(s => s.Name.ToLower() == (_Terminal.Profiles.Defaults.ColorScheme.ToString() ?? string.Empty).ToLower()).FirstOrDefault() ?? _Terminal.Schemes.FirstOrDefault();
                    _Terminal.Profiles.List[TerProfiles.SelectedIndex - 1].ColorScheme = null;
                }
                else if (TerSchemes.SelectedIndex > 0)
                {
                    if (TerProfiles.SelectedIndex == 0)
                    {
                        _Terminal.Profiles.Defaults.ColorScheme = TerSchemes.SelectedItem.ToString();
                    }
                    else if (TerProfiles.SelectedIndex > 0)
                    {
                        _Terminal.Profiles.List[TerProfiles.SelectedIndex - 1].ColorScheme = TerSchemes.SelectedItem.ToString();
                    }

                    temp = _Terminal.Schemes[TerSchemes.SelectedIndex - 1];
                }

                TerBackground.BackColor = temp.Background;
                TerForeground.BackColor = temp.Foreground;
                TerSelection.BackColor = temp.SelectionBackground;
                TerCursor.BackColor = temp.CursorColor;

                TerBlack.BackColor = temp.Black;
                TerBlue.BackColor = temp.Blue;
                TerGreen.BackColor = temp.Green;
                TerCyan.BackColor = temp.Cyan;
                TerRed.BackColor = temp.Red;
                TerPurple.BackColor = temp.Purple;
                TerYellow.BackColor = temp.Yellow;
                TerWhite.BackColor = temp.White;

                TerBlackB.BackColor = temp.BrightBlack;
                TerBlueB.BackColor = temp.BrightBlue;
                TerGreenB.BackColor = temp.BrightGreen;
                TerCyanB.BackColor = temp.BrightCyan;
                TerRedB.BackColor = temp.BrightRed;
                TerPurpleB.BackColor = temp.BrightPurple;
                TerYellowB.BackColor = temp.BrightYellow;
                TerWhiteB.BackColor = temp.BrightWhite;

                if (IsShown) ApplyPreview(_Terminal);
            }
        }

        private void TerProfiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            WinTerminal.Types.Profile profile = TerProfiles.SelectedIndex == 0 ? _Terminal.Profiles.Defaults : _Terminal.Profiles.List[TerProfiles.SelectedIndex - 1];

            string schemeName = (profile.ColorScheme ?? string.Empty).ToString();
            if (string.IsNullOrWhiteSpace(schemeName))
            {
                if (TerProfiles.SelectedIndex == 0 && TerSchemes.Items.Count > 1)
                    TerSchemes.SelectedIndex = 1;
                else
                    TerSchemes.SelectedIndex = 0;
            }
            else if (TerSchemes.Items.Contains(schemeName)) TerSchemes.SelectedItem = schemeName;
            else if (TerSchemes.Items.Count > 1) TerSchemes.SelectedIndex = 1; else TerSchemes.SelectedIndex = 0;

            string themeName = (_Terminal.Theme ?? string.Empty).ToString();
            if (string.IsNullOrWhiteSpace(themeName)) TerThemes.SelectedIndex = 0;
            else if (TerThemes.Items.Contains(themeName)) TerThemes.SelectedItem = themeName;
            else if (TerThemes.Items.Count > 1) TerThemes.SelectedIndex = 1; else TerThemes.SelectedIndex = 0;

            TerBackImage.Text = profile.BackgroundImage;
            TerImageOpacity.Value = (int)(profile.BackgroundImageOpacity * 100f);

            TerCursorStyle.SelectedIndex = (int)profile.CursorShape;
            TerCursorHeightBar.Value = profile.CursorHeight;

            TerFontName.Text = profile.Font.Face;
            GDI32.LogFont fx = new();

            using (Font f_cmd = new(profile.Font.Face, profile.Font.Size))
            {
                f_cmd.ToLogFont(fx);
                fx.lfWeight = (int)profile.Font.Weight * 100;

                using (Font f_cmd_x = new(f_cmd.Name, f_cmd.Size, Font.FromLogFont(fx).Style))
                {
                    TerFontName.Font = new(f_cmd_x.Name, 9f, f_cmd_x.Style);
                }
            }

            TerFontSizeBar.Value = (int)profile.Font.Size;
            TerFontWeight.SelectedIndex = (int)profile.Font.Weight;

            TerAcrylic.Checked = profile.UseAcrylic;
            TerOpacityBar.Value = profile.Opacity;

            Terminal1.Opacity = profile.Opacity;
            Terminal1.OpacityBackImage = (float)profile.BackgroundImageOpacity * 100f;

            if (!string.IsNullOrEmpty(profile.TabTitle))
            {
                Terminal1.TabTitle = profile.TabTitle;
            }
            else if (!string.IsNullOrEmpty(profile.Name))
            {
                Terminal1.TabTitle = profile.Name;
            }
            else if (TerProfiles.SelectedIndex == 0)
            {
                Terminal1.TabTitle = Program.Localization.Strings.General.Default;
            }
            else
            {
                Terminal1.TabTitle = Program.Localization.Strings.General.Untitled;
            }

            if (File.Exists(profile.Icon))
            {
                Terminal1.TabIcon = BitmapMgr.Load(profile.Icon);
            }

            else
            {
                IntPtr intPtr = IntPtr.Zero;
                Kernel32.Wow64DisableWow64FsRedirection(ref intPtr);
                string path = string.Empty;
                if (profile.Commandline is not null)
                    path = profile.Commandline.Replace("%SystemRoot%", SysPaths.Windows);
                Kernel32.Wow64RevertWow64FsRedirection(IntPtr.Zero);

                if (File.Exists(path))
                {
                    Terminal1.TabIcon = ((Icon)NativeMethods.Helpers.ExtractSmallIcon(path)).ToBitmap();
                }
                else
                {
                    Terminal1.TabIcon = null;
                    Terminal1.TabIconButItIsString = "";
                }
            }

            ApplyPreview(_Terminal);
        }

        private void TerCursorStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            Terminal1.CursorType = (UI.Simulation.WinTerminal.CursorShape_Enum)TerCursorStyle.SelectedIndex;

            if (!IsShown) return;

            WinTerminal.Types.Profile temp = TerProfiles.SelectedIndex == 0 ? _Terminal.Profiles.Defaults : _Terminal.Profiles.List[TerProfiles.SelectedIndex - 1];
            temp.CursorShape = (WinTerminal.Types.CursorShape)TerCursorStyle.SelectedIndex;
        }

        public void SetDefaultsToScheme(string Scheme)
        {
            switch (Scheme.ToLower() ?? string.Empty)
            {
                case var @case when @case == ("Campbell".ToLower() ?? string.Empty):
                    {
                        TerBackground.DefaultBackColor = Color.FromArgb(12, 12, 12);
                        TerBlack.DefaultBackColor = Color.FromArgb(12, 12, 12);
                        TerBlue.DefaultBackColor = Color.FromArgb(0, 55, 218);
                        TerBlackB.DefaultBackColor = Color.FromArgb(118, 118, 118);
                        TerBlueB.DefaultBackColor = Color.FromArgb(59, 120, 255);
                        TerCyanB.DefaultBackColor = Color.FromArgb(97, 214, 214);
                        TerGreenB.DefaultBackColor = Color.FromArgb(22, 198, 12);
                        TerPurpleB.DefaultBackColor = Color.FromArgb(180, 0, 158);
                        TerRedB.DefaultBackColor = Color.FromArgb(231, 72, 86);
                        TerWhiteB.DefaultBackColor = Color.FromArgb(242, 242, 242);
                        TerYellowB.DefaultBackColor = Color.FromArgb(249, 241, 165);
                        TerCursor.DefaultBackColor = Color.FromArgb(255, 255, 255);
                        TerCyan.DefaultBackColor = Color.FromArgb(58, 150, 221);
                        TerForeground.DefaultBackColor = Color.FromArgb(204, 204, 204);
                        TerGreen.DefaultBackColor = Color.FromArgb(19, 161, 14);
                        TerPurple.DefaultBackColor = Color.FromArgb(136, 23, 152);
                        TerRed.DefaultBackColor = Color.FromArgb(197, 15, 31);
                        TerSelection.DefaultBackColor = Color.FromArgb(255, 255, 255);
                        TerWhite.DefaultBackColor = Color.FromArgb(204, 204, 204);
                        TerYellow.DefaultBackColor = Color.FromArgb(196, 156, 0);
                        break;
                    }

                case var case1 when case1 == ("Campbell Powershell".ToLower() ?? string.Empty):
                    {
                        TerBackground.DefaultBackColor = Color.FromArgb(1, 36, 86);
                        TerBlack.DefaultBackColor = Color.FromArgb(12, 12, 12);
                        TerBlue.DefaultBackColor = Color.FromArgb(0, 55, 218);
                        TerBlackB.DefaultBackColor = Color.FromArgb(118, 118, 118);
                        TerBlueB.DefaultBackColor = Color.FromArgb(59, 120, 255);
                        TerCyanB.DefaultBackColor = Color.FromArgb(97, 214, 214);
                        TerGreenB.DefaultBackColor = Color.FromArgb(22, 198, 12);
                        TerPurpleB.DefaultBackColor = Color.FromArgb(180, 0, 158);
                        TerRedB.DefaultBackColor = Color.FromArgb(231, 72, 86);
                        TerWhiteB.DefaultBackColor = Color.FromArgb(242, 242, 242);
                        TerYellowB.DefaultBackColor = Color.FromArgb(249, 241, 165);
                        TerCursor.DefaultBackColor = Color.FromArgb(255, 255, 255);
                        TerCyan.DefaultBackColor = Color.FromArgb(58, 150, 221);
                        TerForeground.DefaultBackColor = Color.FromArgb(204, 204, 204);
                        TerGreen.DefaultBackColor = Color.FromArgb(19, 161, 14);
                        TerPurple.DefaultBackColor = Color.FromArgb(136, 23, 152);
                        TerRed.DefaultBackColor = Color.FromArgb(197, 15, 31);
                        TerSelection.DefaultBackColor = Color.FromArgb(255, 255, 255);
                        TerWhite.DefaultBackColor = Color.FromArgb(204, 204, 204);
                        TerYellow.DefaultBackColor = Color.FromArgb(196, 156, 0);
                        break;
                    }

                case var case2 when case2 == ("One Half Dark".ToLower() ?? string.Empty):
                    {
                        TerBackground.DefaultBackColor = Color.FromArgb(40, 44, 52);
                        TerBlack.DefaultBackColor = Color.FromArgb(40, 44, 52);
                        TerBlue.DefaultBackColor = Color.FromArgb(97, 175, 239);
                        TerBlackB.DefaultBackColor = Color.FromArgb(90, 99, 116);
                        TerBlueB.DefaultBackColor = Color.FromArgb(97, 175, 239);
                        TerCyanB.DefaultBackColor = Color.FromArgb(86, 182, 194);
                        TerGreenB.DefaultBackColor = Color.FromArgb(152, 195, 121);
                        TerPurpleB.DefaultBackColor = Color.FromArgb(198, 120, 221);
                        TerRedB.DefaultBackColor = Color.FromArgb(224, 108, 117);
                        TerWhiteB.DefaultBackColor = Color.FromArgb(220, 223, 228);
                        TerYellowB.DefaultBackColor = Color.FromArgb(229, 192, 123);
                        TerCursor.DefaultBackColor = Color.FromArgb(255, 255, 255);
                        TerCyan.DefaultBackColor = Color.FromArgb(86, 182, 194);
                        TerForeground.DefaultBackColor = Color.FromArgb(220, 223, 228);
                        TerGreen.DefaultBackColor = Color.FromArgb(152, 195, 121);
                        TerPurple.DefaultBackColor = Color.FromArgb(198, 120, 221);
                        TerRed.DefaultBackColor = Color.FromArgb(224, 108, 117);
                        TerSelection.DefaultBackColor = Color.FromArgb(255, 255, 255);
                        TerWhite.DefaultBackColor = Color.FromArgb(220, 223, 228);
                        TerYellow.DefaultBackColor = Color.FromArgb(229, 192, 123);
                        break;
                    }

                case var case3 when case3 == ("One Half Light".ToLower() ?? string.Empty):
                    {
                        TerBackground.DefaultBackColor = Color.FromArgb(250, 250, 250);
                        TerBlack.DefaultBackColor = Color.FromArgb(56, 58, 66);
                        TerBlue.DefaultBackColor = Color.FromArgb(1, 132, 188);
                        TerBlackB.DefaultBackColor = Color.FromArgb(79, 82, 93);
                        TerBlueB.DefaultBackColor = Color.FromArgb(97, 175, 239);
                        TerCyanB.DefaultBackColor = Color.FromArgb(86, 181, 193);
                        TerGreenB.DefaultBackColor = Color.FromArgb(152, 195, 121);
                        TerPurpleB.DefaultBackColor = Color.FromArgb(197, 119, 221);
                        TerRedB.DefaultBackColor = Color.FromArgb(223, 108, 117);
                        TerWhiteB.DefaultBackColor = Color.FromArgb(255, 255, 255);
                        TerYellowB.DefaultBackColor = Color.FromArgb(228, 196, 122);
                        TerCursor.DefaultBackColor = Color.FromArgb(79, 82, 93);
                        TerCyan.DefaultBackColor = Color.FromArgb(9, 151, 179);
                        TerForeground.DefaultBackColor = Color.FromArgb(56, 58, 66);
                        TerGreen.DefaultBackColor = Color.FromArgb(80, 161, 79);
                        TerPurple.DefaultBackColor = Color.FromArgb(166, 38, 164);
                        TerRed.DefaultBackColor = Color.FromArgb(228, 86, 73);
                        TerSelection.DefaultBackColor = Color.FromArgb(255, 255, 255);
                        TerWhite.DefaultBackColor = Color.FromArgb(250, 250, 250);
                        TerYellow.DefaultBackColor = Color.FromArgb(193, 131, 1);
                        break;
                    }

                case var case4 when case4 == ("Solarized Dark".ToLower() ?? string.Empty):
                    {
                        TerBackground.DefaultBackColor = Color.FromArgb(0, 43, 54);
                        TerBlack.DefaultBackColor = Color.FromArgb(0, 43, 54);
                        TerBlue.DefaultBackColor = Color.FromArgb(38, 139, 210);
                        TerBlackB.DefaultBackColor = Color.FromArgb(7, 54, 66);
                        TerBlueB.DefaultBackColor = Color.FromArgb(131, 148, 150);
                        TerCyanB.DefaultBackColor = Color.FromArgb(147, 161, 161);
                        TerGreenB.DefaultBackColor = Color.FromArgb(88, 110, 117);
                        TerPurpleB.DefaultBackColor = Color.FromArgb(108, 113, 196);
                        TerRedB.DefaultBackColor = Color.FromArgb(203, 75, 22);
                        TerWhiteB.DefaultBackColor = Color.FromArgb(253, 246, 227);
                        TerYellowB.DefaultBackColor = Color.FromArgb(101, 123, 131);
                        TerCursor.DefaultBackColor = Color.FromArgb(255, 255, 255);
                        TerCyan.DefaultBackColor = Color.FromArgb(42, 161, 152);
                        TerForeground.DefaultBackColor = Color.FromArgb(131, 148, 150);
                        TerGreen.DefaultBackColor = Color.FromArgb(133, 153, 0);
                        TerPurple.DefaultBackColor = Color.FromArgb(211, 54, 130);
                        TerRed.DefaultBackColor = Color.FromArgb(220, 50, 47);
                        TerSelection.DefaultBackColor = Color.FromArgb(255, 255, 255);
                        TerWhite.DefaultBackColor = Color.FromArgb(238, 232, 213);
                        TerYellow.DefaultBackColor = Color.FromArgb(181, 137, 0);
                        break;
                    }

                case var case5 when case5 == ("Solarized Light".ToLower() ?? string.Empty):
                    {
                        TerBackground.DefaultBackColor = Color.FromArgb(253, 246, 227);
                        TerBlack.DefaultBackColor = Color.FromArgb(0, 43, 54);
                        TerBlue.DefaultBackColor = Color.FromArgb(38, 139, 210);
                        TerBlackB.DefaultBackColor = Color.FromArgb(7, 54, 66);
                        TerBlueB.DefaultBackColor = Color.FromArgb(131, 148, 150);
                        TerCyanB.DefaultBackColor = Color.FromArgb(147, 161, 161);
                        TerGreenB.DefaultBackColor = Color.FromArgb(88, 110, 117);
                        TerPurpleB.DefaultBackColor = Color.FromArgb(108, 113, 196);
                        TerRedB.DefaultBackColor = Color.FromArgb(203, 75, 22);
                        TerWhiteB.DefaultBackColor = Color.FromArgb(253, 246, 227);
                        TerYellowB.DefaultBackColor = Color.FromArgb(101, 123, 131);
                        TerCursor.DefaultBackColor = Color.FromArgb(0, 43, 54);
                        TerCyan.DefaultBackColor = Color.FromArgb(42, 161, 152);
                        TerForeground.DefaultBackColor = Color.FromArgb(101, 123, 131);
                        TerGreen.DefaultBackColor = Color.FromArgb(133, 153, 0);
                        TerPurple.DefaultBackColor = Color.FromArgb(211, 54, 130);
                        TerRed.DefaultBackColor = Color.FromArgb(220, 50, 47);
                        TerSelection.DefaultBackColor = Color.FromArgb(255, 255, 255);
                        TerWhite.DefaultBackColor = Color.FromArgb(238, 232, 213);
                        TerYellow.DefaultBackColor = Color.FromArgb(181, 137, 0);
                        break;
                    }

                case var case6 when case6 == ("Tango Dark".ToLower() ?? string.Empty):
                    {
                        TerBackground.DefaultBackColor = Color.FromArgb(0, 0, 0);
                        TerBlack.DefaultBackColor = Color.FromArgb(0, 0, 0);
                        TerBlue.DefaultBackColor = Color.FromArgb(52, 101, 164);
                        TerBlackB.DefaultBackColor = Color.FromArgb(85, 87, 83);
                        TerBlueB.DefaultBackColor = Color.FromArgb(114, 159, 207);
                        TerCyanB.DefaultBackColor = Color.FromArgb(52, 226, 226);
                        TerGreenB.DefaultBackColor = Color.FromArgb(138, 226, 52);
                        TerPurpleB.DefaultBackColor = Color.FromArgb(173, 127, 168);
                        TerRedB.DefaultBackColor = Color.FromArgb(239, 41, 41);
                        TerWhiteB.DefaultBackColor = Color.FromArgb(238, 238, 236);
                        TerYellowB.DefaultBackColor = Color.FromArgb(252, 233, 79);
                        TerCursor.DefaultBackColor = Color.FromArgb(255, 255, 255);
                        TerCyan.DefaultBackColor = Color.FromArgb(6, 152, 154);
                        TerForeground.DefaultBackColor = Color.FromArgb(211, 215, 207);
                        TerGreen.DefaultBackColor = Color.FromArgb(78, 154, 6);
                        TerPurple.DefaultBackColor = Color.FromArgb(117, 80, 123);
                        TerRed.DefaultBackColor = Color.FromArgb(204, 0, 0);
                        TerSelection.DefaultBackColor = Color.FromArgb(255, 255, 255);
                        TerWhite.DefaultBackColor = Color.FromArgb(211, 215, 207);
                        TerYellow.DefaultBackColor = Color.FromArgb(196, 160, 0);
                        break;
                    }

                case var case7 when case7 == ("Tango Light".ToLower() ?? string.Empty):
                    {
                        TerBackground.DefaultBackColor = Color.FromArgb(255, 255, 255);
                        TerBlack.DefaultBackColor = Color.FromArgb(0, 0, 0);
                        TerBlue.DefaultBackColor = Color.FromArgb(52, 101, 164);
                        TerBlackB.DefaultBackColor = Color.FromArgb(85, 87, 83);
                        TerBlueB.DefaultBackColor = Color.FromArgb(114, 159, 207);
                        TerCyanB.DefaultBackColor = Color.FromArgb(52, 226, 226);
                        TerGreenB.DefaultBackColor = Color.FromArgb(138, 226, 52);
                        TerPurpleB.DefaultBackColor = Color.FromArgb(173, 127, 168);
                        TerRedB.DefaultBackColor = Color.FromArgb(239, 41, 41);
                        TerWhiteB.DefaultBackColor = Color.FromArgb(238, 238, 236);
                        TerYellowB.DefaultBackColor = Color.FromArgb(252, 233, 79);
                        TerCursor.DefaultBackColor = Color.FromArgb(0, 0, 0);
                        TerCyan.DefaultBackColor = Color.FromArgb(6, 152, 154);
                        TerForeground.DefaultBackColor = Color.FromArgb(85, 87, 83);
                        TerGreen.DefaultBackColor = Color.FromArgb(78, 154, 6);
                        TerPurple.DefaultBackColor = Color.FromArgb(117, 80, 123);
                        TerRed.DefaultBackColor = Color.FromArgb(204, 0, 0);
                        TerSelection.DefaultBackColor = Color.FromArgb(255, 255, 255);
                        TerWhite.DefaultBackColor = Color.FromArgb(211, 215, 207);
                        TerYellow.DefaultBackColor = Color.FromArgb(196, 160, 0);
                        break;
                    }

                case var case8 when case8 == ("Vintage".ToLower() ?? string.Empty):
                    {
                        TerBackground.DefaultBackColor = Color.FromArgb(0, 0, 0);
                        TerBlack.DefaultBackColor = Color.FromArgb(0, 0, 0);
                        TerBlue.DefaultBackColor = Color.FromArgb(0, 0, 128);
                        TerBlackB.DefaultBackColor = Color.FromArgb(128, 128, 128);
                        TerBlueB.DefaultBackColor = Color.FromArgb(0, 0, 255);
                        TerCyanB.DefaultBackColor = Color.FromArgb(0, 255, 255);
                        TerGreenB.DefaultBackColor = Color.FromArgb(0, 255, 0);
                        TerPurpleB.DefaultBackColor = Color.FromArgb(255, 0, 255);
                        TerRedB.DefaultBackColor = Color.FromArgb(255, 0, 0);
                        TerWhiteB.DefaultBackColor = Color.FromArgb(255, 255, 255);
                        TerYellowB.DefaultBackColor = Color.FromArgb(255, 255, 0);
                        TerCursor.DefaultBackColor = Color.FromArgb(255, 255, 255);
                        TerCyan.DefaultBackColor = Color.FromArgb(0, 128, 128);
                        TerForeground.DefaultBackColor = Color.FromArgb(192, 192, 192);
                        TerGreen.DefaultBackColor = Color.FromArgb(0, 128, 0);
                        TerPurple.DefaultBackColor = Color.FromArgb(128, 0, 128);
                        TerRed.DefaultBackColor = Color.FromArgb(128, 0, 0);
                        TerSelection.DefaultBackColor = Color.FromArgb(255, 255, 255);
                        TerWhite.DefaultBackColor = Color.FromArgb(192, 192, 192);
                        TerYellow.DefaultBackColor = Color.FromArgb(128, 128, 0);
                        break;
                    }

                default:
                    {
                        TerBackground.DefaultBackColor = Color.FromArgb(12, 12, 12);
                        TerBlack.DefaultBackColor = Color.FromArgb(12, 12, 12);
                        TerBlue.DefaultBackColor = Color.FromArgb(0, 55, 218);
                        TerBlackB.DefaultBackColor = Color.FromArgb(118, 118, 118);
                        TerBlueB.DefaultBackColor = Color.FromArgb(59, 120, 255);
                        TerCyanB.DefaultBackColor = Color.FromArgb(97, 214, 214);
                        TerGreenB.DefaultBackColor = Color.FromArgb(22, 198, 12);
                        TerPurpleB.DefaultBackColor = Color.FromArgb(180, 0, 158);
                        TerRedB.DefaultBackColor = Color.FromArgb(231, 72, 86);
                        TerWhiteB.DefaultBackColor = Color.FromArgb(242, 242, 242);
                        TerYellowB.DefaultBackColor = Color.FromArgb(249, 241, 165);
                        TerCursor.DefaultBackColor = Color.FromArgb(255, 255, 255);
                        TerCyan.DefaultBackColor = Color.FromArgb(58, 150, 221);
                        TerForeground.DefaultBackColor = Color.FromArgb(204, 204, 204);
                        TerGreen.DefaultBackColor = Color.FromArgb(19, 161, 14);
                        TerPurple.DefaultBackColor = Color.FromArgb(136, 23, 152);
                        TerRed.DefaultBackColor = Color.FromArgb(197, 15, 31);
                        TerSelection.DefaultBackColor = Color.FromArgb(255, 255, 255);
                        TerWhite.DefaultBackColor = Color.FromArgb(204, 204, 204);
                        TerYellow.DefaultBackColor = Color.FromArgb(193, 156, 0);
                        break;
                    }

            }
        }

        private void Button12_Click(object sender, EventArgs e)
        {
            string s = InputBox(Program.Localization.Strings.Aspects.Terminals.TypeSchemeName, $"{Program.Localization.Strings.General.NewScheme} #{TerSchemes.Items.Count - 1}");
            if (string.IsNullOrWhiteSpace(s)) return;
            _Terminal.Schemes.Add(new WinTerminal.Types.Scheme() { Name = s });
            FillTerminalSchemes(_Terminal, TerSchemes);
            TerSchemes.SelectedIndex = TerSchemes.Items.Count - 1;
        }

        private void ColorClick(object sender, EventArgs e)
        {
            if (e is DragEventArgs) return;

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

        private void ColorMainsClick(object sender, EventArgs e)
        {
            WinTerminal.Types.Scheme scheme = new();
            WinTerminal.Types.Theme theme = new();

            if (TerProfiles.SelectedIndex == 0)
            {
                scheme = _Terminal.Schemes
                    .Where(s => s.Name.ToLower() == (_Terminal.Profiles.Defaults.ColorScheme.ToString() ?? string.Empty).ToLower()).FirstOrDefault();
            }
            else if (TerProfiles.SelectedIndex > 0)
            {
                scheme = _Terminal.Schemes
                    .Where(s => s.Name.ToLower() == _Terminal.Profiles.List[TerProfiles.SelectedIndex - 1].ColorScheme.ToString().ToLower()).FirstOrDefault();
            }

            if (TerThemes.SelectedIndex > 3)
            {
                theme = _Terminal.Themes[TerThemes.SelectedIndex - 4] ?? new();
            }

            if (e is DragEventArgs)
            {
                if (((ColorItem)sender).Name.ToString().ToLower().Contains(TerBackground.Name.ToLower()))
                {
                    scheme.Background = ((ColorItem)sender).BackColor;
                }

                if (((ColorItem)sender).Name.ToString().ToLower().Contains(TerForeground.Name.ToLower()))
                {
                    scheme.Foreground = ((ColorItem)sender).BackColor;
                }

                if (((ColorItem)sender).Name.ToString().ToLower().Contains(TerSelection.Name.ToLower()))
                {
                    scheme.SelectionBackground = ((ColorItem)sender).BackColor;
                }

                if (((ColorItem)sender).Name.ToString().ToLower().Contains(TerCursor.Name.ToLower()))
                {
                    scheme.CursorColor = ((ColorItem)sender).BackColor;
                }

                if (((ColorItem)sender).Name.ToString().ToLower().Contains(TerTabActive.Name.ToLower()))
                {
                    theme.Tab.Background = ((ColorItem)sender).BackColor;
                }

                if (((ColorItem)sender).Name.ToString().ToLower().Contains(TerTabInactive.Name.ToLower()))
                {
                    theme.Tab.UnfocusedBackground = ((ColorItem)sender).BackColor;
                }

                if (((ColorItem)sender).Name.ToString().ToLower().Contains(TerTitlebarActive.Name.ToLower()))
                {
                    theme.TabRow.Background = ((ColorItem)sender).BackColor;
                }

                if (((ColorItem)sender).Name.ToString().ToLower().Contains(TerTitlebarInactive.Name.ToLower()))
                {
                    theme.TabRow.UnfocusedBackground = ((ColorItem)sender).BackColor;
                }

                ApplyPreview(_Terminal);
                return;
            }

            ColorItem colorItem = (ColorItem)sender;
            Dictionary<Control, string[]> CList = new()
            {
                { colorItem, new string[] { nameof(colorItem.BackColor) } },
            };

            if (((ColorItem)sender).Name.ToString().ToLower().Contains(TerBackground.Name.ToLower()))
            {
                CList.Add(Terminal1, [nameof(Terminal1.Color_Background)]);
                CList.Add(Terminal2, [nameof(Terminal2.Color_Background)]);
            }

            if (((ColorItem)sender).Name.ToString().ToLower().Contains(TerForeground.Name.ToLower()))
            {
                CList.Add(Terminal1, [nameof(Terminal1.Color_Foreground)]);
                CList.Add(Terminal2, [nameof(Terminal2.Color_Foreground)]);
            }

            if (((ColorItem)sender).Name.ToString().ToLower().Contains(TerSelection.Name.ToLower()))
            {
                CList.Add(Terminal1, [nameof(Terminal1.Color_Selection)]);
                CList.Add(Terminal2, [nameof(Terminal2.Color_Selection)]);
            }

            if (((ColorItem)sender).Name.ToString().ToLower().Contains(TerCursor.Name.ToLower()))
            {
                CList.Add(Terminal1, [nameof(Terminal1.Color_Cursor)]);
                CList.Add(Terminal2, [nameof(Terminal2.Color_Cursor)]);
            }

            if (((ColorItem)sender).Name.ToString().ToLower().Contains(TerTabActive.Name.ToLower()))
            {
                CList.Add(Terminal1, [nameof(Terminal1.TabColor)]);
                CList.Add(Terminal2, [nameof(Terminal2.TabColor)]);
            }

            if (((ColorItem)sender).Name.ToString().ToLower().Contains(TerTabInactive.Name.ToLower()))
            {
                CList.Add(Terminal1, [nameof(Terminal1.Color_TabUnFocused)]);
                CList.Add(Terminal2, [nameof(Terminal2.Color_TabUnFocused)]);
            }

            if (((ColorItem)sender).Name.ToString().ToLower().Contains(TerTitlebarActive.Name.ToLower()))
                CList.Add(Terminal1, [nameof(Terminal1.Color_Titlebar)]);

            if (((ColorItem)sender).Name.ToString().ToLower().Contains(TerTitlebarInactive.Name.ToLower()))
                CList.Add(Terminal2, [nameof(Terminal2.Color_Titlebar_Unfocused)]);

            Color C = Forms.ColorPickerDlg.Pick(CList);

            if (((ColorItem)sender).Name.ToString().ToLower().Contains(TerBackground.Name.ToLower()))
            {
                scheme.Background = C;
            }

            if (((ColorItem)sender).Name.ToString().ToLower().Contains(TerForeground.Name.ToLower()))
            {
                scheme.Foreground = C;
            }

            if (((ColorItem)sender).Name.ToString().ToLower().Contains(TerSelection.Name.ToLower()))
            {
                scheme.SelectionBackground = C;
            }

            if (((ColorItem)sender).Name.ToString().ToLower().Contains(TerCursor.Name.ToLower()))
            {
                scheme.CursorColor = C;
            }

            if (((ColorItem)sender).Name.ToString().ToLower().Contains(TerTabActive.Name.ToLower()))
            {
                theme.Tab.Background = C;
            }

            if (((ColorItem)sender).Name.ToString().ToLower().Contains(TerTabInactive.Name.ToLower()))
            {
                theme.Tab.UnfocusedBackground = C;
            }

            if (((ColorItem)sender).Name.ToString().ToLower().Contains(TerTitlebarActive.Name.ToLower()))
            {
                theme.TabRow.Background = C;
            }

            if (((ColorItem)sender).Name.ToString().ToLower().Contains(TerTitlebarInactive.Name.ToLower()))
            {
                theme.TabRow.UnfocusedBackground = C;
            }

            ApplyPreview(_Terminal);

            colorItem.BackColor = C;
            colorItem.Refresh();
            CList.Clear();
        }

        public void ApplyPreview(WinTerminal Terminal)
        {
            Terminal1.UseAcrylicOnTitlebar = Terminal.UseAcrylicInTabRow;

            if (TerProfiles.SelectedIndex == 0)
            {
                Terminal1.TabColor = Terminal.Profiles.Defaults.TabColor;
            }
            else if (Terminal.Profiles.List[TerProfiles.SelectedIndex - 1].TabColor == Color.FromArgb(0, 0, 0, 0))
            {
                Terminal1.TabColor = Terminal.Profiles.Defaults.TabColor;
            }
            else
            {
                Terminal1.TabColor = Terminal.Profiles.List[TerProfiles.SelectedIndex - 1].TabColor;
            }

            WinTerminal.Types.Scheme temp = new();

            if (TerSchemes.SelectedIndex == 0)
            {
                if (TerProfiles.SelectedIndex == 0)
                {
                    temp = _Terminal.Schemes.FirstOrDefault();
                }
                else if (TerProfiles.SelectedIndex > 0)
                {
                    temp = _Terminal.Schemes.Where(s => s.Name.ToLower() == (_Terminal.Profiles.Defaults.ColorScheme.ToString() ?? string.Empty).ToLower()).FirstOrDefault() ?? _Terminal.Schemes.FirstOrDefault();
                }
            }
            else if (TerSchemes.SelectedIndex > 0)
            {
                temp = _Terminal.Schemes[TerSchemes.SelectedIndex - 1];
            }

            if (temp == null) temp = new();

            Terminal1.Color_Background = temp.Background;
            Terminal1.Color_Foreground = temp.Foreground;
            Terminal1.Color_Selection = temp.SelectionBackground;
            Terminal1.Color_Cursor = temp.CursorColor;

            //if (Terminal1.TabColor == null || Terminal1.TabColor == Color.EmptyError || Terminal1.TabColor == Color.FromArgb(0, 0, 0, 0)) Terminal1.TabColor = profile.Background;
            Terminal2.Color_Background = Terminal1.Color_Background;
            Terminal2.Color_Foreground = Terminal1.Color_Foreground;
            Terminal2.Color_Selection = Terminal1.Color_Selection;
            Terminal2.Color_Cursor = Terminal1.Color_Cursor;

            if (TerThemesContainer.Enabled)
            {
                WinTerminal.Types.Theme theme = _Terminal.Themes[TerThemes.SelectedIndex - 4] ?? new();
                Terminal1.Color_TabFocused = theme.Tab.Background;
                Terminal1.Color_TabUnFocused = theme.Tab.UnfocusedBackground;
                Terminal1.Color_Titlebar = theme.TabRow.Background;
                Terminal2.Color_Titlebar_Unfocused = theme.TabRow.UnfocusedBackground;
            }
            else
            {
                Terminal1.Color_TabFocused = Color.FromArgb(0, 0, 0, 0);
                Terminal1.Color_Titlebar = Color.FromArgb(0, 0, 0, 0);
                Terminal2.Color_Titlebar_Unfocused = Color.FromArgb(0, 0, 0, 0);
                Terminal1.Color_TabUnFocused = Color.FromArgb(0, 0, 0, 0);
                Terminal2.Color_Titlebar_Unfocused = Color.FromArgb(0, 0, 0, 0);
            }

            if (TerThemes.SelectedItem is not null)
            {
                if (TerThemes.SelectedItem.ToString().ToLower() == "dark")
                {
                    Terminal1.Light = false;
                    Terminal2.Light = false;
                }

                else if (TerThemes.SelectedItem.ToString().ToLower() == "light")
                {
                    Terminal1.Light = true;
                    Terminal2.Light = true;
                }

                else if (TerThemes.SelectedItem.ToString().ToLower() == "system")
                {
                    switch (Program.WindowStyle)
                    {
                        case WindowStyle.W12:
                            {
                                Terminal1.Light = Program.TM.Windows12.AppMode_Light;
                                Terminal2.Light = Program.TM.Windows12.AppMode_Light;
                                break;
                            }

                        case WindowStyle.W11:
                            {
                                Terminal1.Light = Program.TM.Windows11.AppMode_Light;
                                Terminal2.Light = Program.TM.Windows11.AppMode_Light;
                                break;
                            }

                        case WindowStyle.W10:
                            {
                                Terminal1.Light = Program.TM.Windows10.AppMode_Light;
                                Terminal2.Light = Program.TM.Windows10.AppMode_Light;
                                break;
                            }

                        default:
                            {
                                Terminal1.Light = Program.TM.Windows12.AppMode_Light;
                                Terminal2.Light = Program.TM.Windows12.AppMode_Light;
                                break;
                            }
                    }
                }

                else
                {
                    Terminal1.Light = !TerMode.Checked;
                    Terminal2.Light = !TerMode.Checked;
                }
            }

            WinTerminal.Types.Profile temp_p = TerProfiles.SelectedIndex == 0 ? _Terminal.Profiles.Defaults : _Terminal.Profiles.List[TerProfiles.SelectedIndex - 1];
            GDI32.LogFont fx = new();
            Font f_cmd = new(temp_p.Font.Face, temp_p.Font.Size);
            f_cmd.ToLogFont(fx);
            fx.lfWeight = (int)temp_p.Font.Weight * 100;
            f_cmd = new(f_cmd.Name, f_cmd.Size, Font.FromLogFont(fx).Style);
            Terminal1.Font = f_cmd;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            string s = InputBox(Program.Localization.Strings.Aspects.Terminals.TypeSchemeName, $"{Program.Localization.Strings.General.NewTheme} #{TerThemes.Items.Count - 4}");
            if (string.IsNullOrWhiteSpace(s)) return;
            _Terminal.Themes.Add(new() { Name = s });
            FillTerminalThemes(_Terminal, TerThemes);
            TerThemes.SelectedIndex = TerThemes.Items.Count - 1;
        }

        private void TerFontWeight_SelectedIndexChanged(object sender, EventArgs e)
        {
            GDI32.LogFont fx = new();
            Font f_cmd = new(Terminal1.Font.Name, Terminal1.Font.Size, Terminal1.Font.Style);
            f_cmd.ToLogFont(fx);
            fx.lfWeight = TerFontWeight.SelectedIndex * 100;
            {
                Font temp = Font.FromLogFont(fx);
                f_cmd = new(Terminal1.Font.Name, Terminal1.Font.Size, temp.Style);
            }
            Terminal1.Font = f_cmd;

            WinTerminal.Types.Profile temp1 = TerProfiles.SelectedIndex == 0 ? _Terminal.Profiles.Defaults : _Terminal.Profiles.List[TerProfiles.SelectedIndex - 1];
            temp1.Font.Weight = (WinTerminal.Types.FontWeight)TerFontWeight.SelectedIndex;
        }

        private void TerThemes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!IsShown) return;

            TerDeleteTheme.Enabled = TerThemes.SelectedIndex > 3;
            TerEditThemeName.Enabled = TerThemes.SelectedIndex > 3;

            if (TerThemes.SelectedIndex > 3)
            {
                TerThemesContainer.Enabled = true;
                WinTerminal.Types.Theme theme = _Terminal.Themes[TerThemes.SelectedIndex - 4] ?? new();

                TerTitlebarActive.BackColor = theme.TabRow.Background;
                TerTitlebarInactive.BackColor = theme.TabRow.UnfocusedBackground;
                TerTabActive.BackColor = theme.Tab.Background;
                TerTabInactive.BackColor = theme.Tab.UnfocusedBackground;
                TerMode.Checked = !((theme.Window.ApplicationTheme ?? "system").ToLower() == "light");
            }

            else
            {
                TerThemesContainer.Enabled = false;

                TerTitlebarActive.BackColor = Color.FromArgb(0, 0, 0, 0);
                TerTitlebarInactive.BackColor = Color.FromArgb(0, 0, 0, 0);
                TerTabActive.BackColor = Color.FromArgb(0, 0, 0, 0);
                TerTabInactive.BackColor = Color.FromArgb(0, 0, 0, 0);

                if (TerThemes.SelectedIndex == 1)
                    TerMode.Checked = true;
                if (TerThemes.SelectedIndex == 2)
                    TerMode.Checked = false;

                switch (Program.WindowStyle)
                {
                    case WindowStyle.W12:
                        {
                            if (TerThemes.SelectedIndex == 3)
                                TerMode.Checked = !Program.TM.Windows12.AppMode_Light;
                            break;
                        }

                    case WindowStyle.W11:
                        {
                            if (TerThemes.SelectedIndex == 3)
                                TerMode.Checked = !Program.TM.Windows11.AppMode_Light;
                            break;
                        }

                    case WindowStyle.W10:
                        {
                            if (TerThemes.SelectedIndex == 3)
                                TerMode.Checked = !Program.TM.Windows10.AppMode_Light;
                            break;
                        }

                    default:
                        {
                            if (TerThemes.SelectedIndex == 3)
                                TerMode.Checked = !Program.TM.Windows12.AppMode_Light;
                            break;
                        }
                }
            }

            if (TerThemes.SelectedIndex == 0)
            {
                _Terminal.Theme = null;
            }
            else if (TerThemes.SelectedIndex == 1)
            {
                _Terminal.Theme = "dark";
            }
            else if (TerThemes.SelectedIndex == 2)
            {
                _Terminal.Theme = "light";
            }
            else if (TerThemes.SelectedIndex == 3)
            {
                _Terminal.Theme = "system";
            }
            else
            {
                _Terminal.Theme = TerThemes.SelectedItem.ToString();
            }

            ApplyPreview(_Terminal);
        }

        private void TerEditThemeName_Click(object sender, EventArgs e)
        {
            if (TerThemes.SelectedIndex > 3)
            {
                string s = InputBox(Program.Localization.Strings.Aspects.Terminals.TypeSchemeName, TerThemes.SelectedItem.ToString());
                if ((s ?? string.Empty) != (TerThemes.SelectedItem.ToString() ?? string.Empty) & !string.IsNullOrEmpty(s) & !TerThemes.Items.Contains(s))
                {
                    int i = TerThemes.SelectedIndex;
                    TerThemes.Items.RemoveAt(i);
                    TerThemes.Items.Insert(i, s);
                    TerThemes.SelectedIndex = i;
                    _Terminal.Themes[i - 4].Name = s;
                }
            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            string s = InputBox(Program.Localization.Strings.Aspects.Terminals.TypeSchemeName, TerSchemes.SelectedItem.ToString());
            if ((s ?? string.Empty) != (TerSchemes.SelectedItem.ToString() ?? string.Empty) & !string.IsNullOrEmpty(s) & !TerSchemes.Items.Contains(s))
            {
                int i = TerSchemes.SelectedIndex;
                TerSchemes.Items.RemoveAt(i);
                TerSchemes.Items.Insert(i, s);
                TerSchemes.SelectedIndex = i;
                _Terminal.Schemes[i - 1].Name = s;
            }
        }

        private void Button14_Click(object sender, EventArgs e)
        {
            Forms.TerminalInfo.Profile = TerProfiles.SelectedIndex == 0 ? _Terminal.Profiles.Defaults : _Terminal.Profiles.List[TerProfiles.SelectedIndex - 1];

            if (Forms.TerminalInfo.OpenDialog(TerProfiles.SelectedIndex == 0) == DialogResult.OK)
            {
                WinTerminal.Types.Profile temp = TerProfiles.SelectedIndex == 0 ? _Terminal.Profiles.Defaults : _Terminal.Profiles.List[TerProfiles.SelectedIndex - 1];
                temp.Name = Forms.TerminalInfo.Profile.Name;
                temp.TabTitle = Forms.TerminalInfo.Profile.TabTitle;
                temp.Icon = Forms.TerminalInfo.Profile.Icon;
                temp.TabColor = Forms.TerminalInfo.Profile.TabColor;

                int i = TerProfiles.SelectedIndex;
                FillTerminalProfiles(_Terminal, TerProfiles);
                TerProfiles.SelectedIndex = i;

                ApplyPreview(_Terminal);
            }
        }

        private void TerAcrylic_CheckedChanged(object sender, EventArgs e)
        {
            Terminal1.UseAcrylic = TerAcrylic.Checked;

            if (!IsShown) return;

            WinTerminal.Types.Profile temp = TerProfiles.SelectedIndex == 0 ? _Terminal.Profiles.Defaults : _Terminal.Profiles.List[TerProfiles.SelectedIndex - 1];
            temp.UseAcrylic = TerAcrylic.Checked;
        }

        private void Button13_Click(object sender, EventArgs e)
        {
            _Terminal.Profiles.List.Add(new() { Name = $"{Program.Localization.Strings.General.NewProfile} #{TerProfiles.Items.Count}", ColorScheme = _Terminal.Profiles.Defaults.ColorScheme });
            FillTerminalProfiles(_Terminal, TerProfiles);
            TerProfiles.SelectedIndex = TerProfiles.Items.Count - 1;
        }

        private void Button15_Click(object sender, EventArgs e)
        {
            string TerDir;
            string TerPreDir;

            if (!Program.Settings.WindowsTerminals.Path_Deflection)
            {
                TerDir = SysPaths.TerminalJSON;
                TerPreDir = SysPaths.TerminalPreviewJSON;
            }
            else
            {
                if (File.Exists(Program.Settings.WindowsTerminals.Terminal_Stable_Path))
                {
                    TerDir = Program.Settings.WindowsTerminals.Terminal_Stable_Path;
                }
                else
                {
                    TerDir = SysPaths.TerminalJSON;
                }

                if (File.Exists(Program.Settings.WindowsTerminals.Terminal_Preview_Path))
                {
                    TerPreDir = Program.Settings.WindowsTerminals.Terminal_Preview_Path;
                }
                else
                {
                    TerPreDir = SysPaths.TerminalPreviewJSON;
                }
            }

            switch (Mode)
            {
                case WinTerminal.Version.Stable:
                    {
                        if (File.Exists(TerDir)) Program.SendCommand(@$"{SysPaths.Explorer} shell:appsFolder\Microsoft.WindowsTerminal_8wekyb3d8bbwe!App", false);
                        break;
                    }

                case WinTerminal.Version.Preview:
                    {
                        if (File.Exists(TerPreDir)) Program.SendCommand(@$"{SysPaths.Explorer} shell:appsFolder\Microsoft.WindowsTerminalPreview_8wekyb3d8bbwe!App", false);
                        break;
                    }
            }
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            TerBackImage.Text = "desktopWallpaper";
        }

        private void TerBackImage_TextChanged(object sender, EventArgs e)
        {
            if (TerBackImage.Text == "desktopWallpaper")
            {
                Terminal1.BackImage = Program.Wallpaper;
            }
            else if (File.Exists(TerBackImage.Text))
            {
                Terminal1.BackImage = BitmapMgr.Load(TerBackImage.Text).FillInSize(new(Terminal1.Width - 2, Terminal1.Height - 32));
            }

            else
            {
                Terminal1.BackImage = null;
            }

            if (!IsShown) return;

            WinTerminal.Types.Profile temp = TerProfiles.SelectedIndex == 0 ? _Terminal.Profiles.Defaults : _Terminal.Profiles.List[TerProfiles.SelectedIndex - 1];
            temp.BackgroundImage = TerBackImage.Text;

            Terminal1.Invalidate();
        }

        private void Button16_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new() { Filter = Program.Filters.Images, Title = Program.Localization.Strings.Extensions.OpenImages })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    TerBackImage.Text = dlg.FileName;
                }
            }
        }

        private void TerMode_CheckedChanged(object sender, EventArgs e)
        {
            if (TerThemes.SelectedIndex > 3)
            {
                _Terminal.Themes[TerThemes.SelectedIndex - 4].Window.ApplicationTheme = !TerMode.Checked ? "light" : "dark";
            }

            if (IsShown) ApplyPreview(_Terminal);
        }

        private void WindowsTerminal_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult != DialogResult.OK)
            {
                switch (Mode)
                {
                    case WinTerminal.Version.Stable:
                        {
                            Program.TM.Terminal = _TerminalDefault;
                            break;
                        }

                    case WinTerminal.Version.Preview:
                        {
                            Program.TM.TerminalPreview = _TerminalDefault;
                            break;
                        }

                }
            }

            DialogResult = DialogResult.Cancel;
        }

        private void Button11_Click(object sender, EventArgs e)
        {
            if (OS.W12 || OS.W11 || OS.W10)
            {
                string TerDir;
                string TerPreDir;

                TerDir = SysPaths.TerminalJSON;
                TerPreDir = SysPaths.TerminalPreviewJSON;


                if (File.Exists(TerDir) & Mode == WinTerminal.Version.Stable)
                {
                    Process.Start(TerDir);
                }

                if (File.Exists(TerPreDir) & Mode == WinTerminal.Version.Preview)
                {
                    Process.Start(TerPreDir);
                }

            }
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            if (OS.W12 || OS.W11 || OS.W10)
            {
                using (SaveFileDialog dlg = new() { Filter = Program.Filters.JSON, Title = Program.Localization.Strings.Extensions.SaveJSON })
                {
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {

                        string TerDir;
                        string TerPreDir;

                        TerDir = SysPaths.TerminalJSON;
                        TerPreDir = SysPaths.TerminalPreviewJSON;

                        if (File.Exists(TerDir) & Mode == WinTerminal.Version.Stable)
                        {
                            File.Copy(TerDir, dlg.FileName);
                        }

                        if (File.Exists(TerPreDir) & Mode == WinTerminal.Version.Preview)
                        {
                            File.Copy(TerPreDir, dlg.FileName);
                        }
                    }
                }
            }
        }

        private void Button17_Click(object sender, EventArgs e)
        {
            WinTerminal.Types.Scheme scheme = new()
            {
                Name = $"{TerSchemes.SelectedItem} Clone #{TerSchemes.Items.Count - 1}",
                Background = _Terminal.Schemes[TerSchemes.SelectedIndex - 1].Background,
                Black = _Terminal.Schemes[TerSchemes.SelectedIndex - 1].Black,
                Blue = _Terminal.Schemes[TerSchemes.SelectedIndex - 1].Blue,
                BrightBlack = _Terminal.Schemes[TerSchemes.SelectedIndex - 1].BrightBlack,
                BrightBlue = _Terminal.Schemes[TerSchemes.SelectedIndex - 1].BrightBlue,
                BrightCyan = _Terminal.Schemes[TerSchemes.SelectedIndex - 1].BrightCyan,
                BrightGreen = _Terminal.Schemes[TerSchemes.SelectedIndex - 1].BrightGreen,
                BrightPurple = _Terminal.Schemes[TerSchemes.SelectedIndex - 1].BrightPurple,
                BrightRed = _Terminal.Schemes[TerSchemes.SelectedIndex - 1].BrightRed,
                BrightWhite = _Terminal.Schemes[TerSchemes.SelectedIndex - 1].BrightWhite,
                BrightYellow = _Terminal.Schemes[TerSchemes.SelectedIndex - 1].BrightYellow,
                CursorColor = _Terminal.Schemes[TerSchemes.SelectedIndex - 1].CursorColor,
                Cyan = _Terminal.Schemes[TerSchemes.SelectedIndex - 1].Cyan,
                Foreground = _Terminal.Schemes[TerSchemes.SelectedIndex - 1].Foreground,
                Green = _Terminal.Schemes[TerSchemes.SelectedIndex - 1].Green,
                Purple = _Terminal.Schemes[TerSchemes.SelectedIndex - 1].Purple,
                Red = _Terminal.Schemes[TerSchemes.SelectedIndex - 1].Red,
                SelectionBackground = _Terminal.Schemes[TerSchemes.SelectedIndex - 1].SelectionBackground,
                White = _Terminal.Schemes[TerSchemes.SelectedIndex - 1].White,
                Yellow = _Terminal.Schemes[TerSchemes.SelectedIndex - 1].Yellow
            };

            _Terminal.Schemes.Add(scheme);
            FillTerminalSchemes(_Terminal, TerSchemes);
            TerSchemes.SelectedIndex = TerSchemes.Items.Count - 1;
        }

        private void Button18_Click(object sender, EventArgs e)
        {
            if (TerProfiles.SelectedIndex == 0)
            {
                MsgBox(Program.Localization.Strings.Aspects.Terminals.ProfileNotCloneable, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            WinTerminal.Types.Profile P = new()
            {
                Name = $"{_Terminal.Profiles.List[TerProfiles.SelectedIndex - 1].Name} {Program.Localization.Strings.General.Clone} #{TerProfiles.Items.Count}",
                BackgroundImage = _Terminal.Profiles.List[TerProfiles.SelectedIndex - 1].BackgroundImage,
                BackgroundImageOpacity = _Terminal.Profiles.List[TerProfiles.SelectedIndex - 1].BackgroundImageOpacity,
                ColorScheme = _Terminal.Profiles.List[TerProfiles.SelectedIndex - 1].ColorScheme,
                Commandline = _Terminal.Profiles.List[TerProfiles.SelectedIndex - 1].Commandline,
                CursorHeight = _Terminal.Profiles.List[TerProfiles.SelectedIndex - 1].CursorHeight,
                CursorShape = _Terminal.Profiles.List[TerProfiles.SelectedIndex - 1].CursorShape,
                Font = _Terminal.Profiles.List[TerProfiles.SelectedIndex - 1].Font,
                Icon = _Terminal.Profiles.List[TerProfiles.SelectedIndex - 1].Icon,
                Opacity = _Terminal.Profiles.List[TerProfiles.SelectedIndex - 1].Opacity,
                TabColor = _Terminal.Profiles.List[TerProfiles.SelectedIndex - 1].TabColor,
                TabTitle = _Terminal.Profiles.List[TerProfiles.SelectedIndex - 1].TabTitle,
                UseAcrylic = _Terminal.Profiles.List[TerProfiles.SelectedIndex - 1].UseAcrylic
            };

            _Terminal.Profiles.List.Add(P);
            FillTerminalProfiles(_Terminal, TerProfiles);
            TerProfiles.SelectedIndex = TerProfiles.Items.Count - 1;
        }

        private void Button19_Click(object sender, EventArgs e)
        {
            if (TerThemes.SelectedIndex < 4)
            {
                MsgBox(Program.Localization.Strings.Aspects.Terminals.ThemeNotCloneable, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            WinTerminal.Types.Theme Th = new()
            {
                Name = $"{_Terminal.Themes[TerThemes.SelectedIndex - 4].Name} {Program.Localization.Strings.General.Clone} #{TerThemes.Items.Count}",
            };

            Th.Window.ApplicationTheme = _Terminal.Themes[TerThemes.SelectedIndex - 4].Window.ApplicationTheme;
            Th.Tab.Background = _Terminal.Themes[TerThemes.SelectedIndex - 4].Tab.Background;
            Th.Tab.UnfocusedBackground = _Terminal.Themes[TerThemes.SelectedIndex - 4].Tab.UnfocusedBackground;
            Th.TabRow.Background = _Terminal.Themes[TerThemes.SelectedIndex - 4].TabRow.Background;
            Th.TabRow.UnfocusedBackground = _Terminal.Themes[TerThemes.SelectedIndex - 4].TabRow.UnfocusedBackground;

            _Terminal.Themes.Add(Th);
            FillTerminalThemes(_Terminal, TerThemes);
            TerThemes.SelectedIndex = TerThemes.Items.Count - 1;
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            UI.WP.ComboBox temp = Forms.WindowsTerminalCopycat.ComboBox1;
            temp.Items.Clear();
            CCat = null;

            foreach (object x in TerProfiles.Items) temp.Items.Add(x);

            if (Forms.WindowsTerminalCopycat.ShowDialog() == DialogResult.OK)
            {
                for (int x = 0, loopTo = TerProfiles.Items.Count - 1; x <= loopTo; x++)
                {
                    if ((TerProfiles.Items[x].ToString().ToLower() ?? string.Empty) == (CCat.ToLower() ?? string.Empty))
                    {
                        WinTerminal.Types.Profile CCatFrom = x == 0 ? _Terminal.Profiles.Defaults : _Terminal.Profiles.List[x - 1];
                        WinTerminal.Types.Profile temp1 = TerProfiles.SelectedIndex == 0 ? _Terminal.Profiles.Defaults : _Terminal.Profiles.List[TerProfiles.SelectedIndex - 1];

                        temp1.BackgroundImage = CCatFrom.BackgroundImage;
                        temp1.BackgroundImageOpacity = CCatFrom.BackgroundImageOpacity;
                        temp1.ColorScheme = CCatFrom.ColorScheme;
                        temp1.CursorHeight = CCatFrom.CursorHeight;
                        temp1.CursorShape = CCatFrom.CursorShape;
                        temp1.Font.Face = CCatFrom.Font.Face;
                        temp1.Font.Weight = CCatFrom.Font.Weight;
                        temp1.Font.Size = CCatFrom.Font.Size;
                        temp1.Icon = CCatFrom.Icon;
                        temp1.Opacity = CCatFrom.Opacity;
                        temp1.TabColor = CCatFrom.TabColor;
                        temp1.TabTitle = CCatFrom.TabTitle;
                        temp1.UseAcrylic = CCatFrom.UseAcrylic;

                        if (CCatFrom.ColorScheme is not null && TerSchemes.Items.Contains(CCatFrom.ColorScheme)) TerSchemes.SelectedItem = CCatFrom.ColorScheme;
                        else TerSchemes.SelectedIndex = 0;

                        TerBackImage.Text = CCatFrom.BackgroundImage;
                        TerImageOpacity.Value = (int)(CCatFrom.BackgroundImageOpacity * 100f);

                        TerCursorStyle.SelectedIndex = (int)CCatFrom.CursorShape;
                        TerCursorHeightBar.Value = CCatFrom.CursorHeight;

                        TerFontName.Text = CCatFrom.Font.Face;
                        GDI32.LogFont fx = new();
                        Font f_cmd = new(CCatFrom.Font.Face, CCatFrom.Font.Size);
                        f_cmd.ToLogFont(fx);
                        fx.lfWeight = (int)CCatFrom.Font.Weight * 100;
                        f_cmd = new(f_cmd.Name, f_cmd.Size, Font.FromLogFont(fx).Style);
                        TerFontName.Font = new(f_cmd.Name, 9f, f_cmd.Style);

                        TerFontSizeBar.Value = (int)CCatFrom.Font.Size;
                        TerFontWeight.SelectedIndex = (int)CCatFrom.Font.Weight;

                        TerAcrylic.Checked = CCatFrom.UseAcrylic;
                        TerOpacityBar.Value = CCatFrom.Opacity;

                        Terminal1.Opacity = CCatFrom.Opacity;
                        Terminal1.OpacityBackImage = (float)CCatFrom.BackgroundImageOpacity * 100f;

                        if (!string.IsNullOrEmpty(CCatFrom.TabTitle))
                        {
                            Terminal1.TabTitle = CCatFrom.TabTitle;
                        }
                        else if (!string.IsNullOrEmpty(CCatFrom.Name))
                        {
                            Terminal1.TabTitle = CCatFrom.Name;
                        }
                        else if (TerProfiles.SelectedIndex == 0)
                        {
                            Terminal1.TabTitle = Program.Localization.Strings.General.Default;
                        }
                        else
                        {
                            Terminal1.TabTitle = Program.Localization.Strings.General.Untitled;
                        }

                        if (File.Exists(CCatFrom.Icon))
                        {
                            Terminal1.TabIcon = BitmapMgr.Load(CCatFrom.Icon);
                        }

                        else
                        {
                            IntPtr intPtr = IntPtr.Zero;
                            Kernel32.Wow64DisableWow64FsRedirection(ref intPtr);
                            string path = string.Empty;
                            if (CCatFrom.Commandline is not null)
                                path = CCatFrom.Commandline.Replace("%SystemRoot%", SysPaths.Windows);
                            Kernel32.Wow64RevertWow64FsRedirection(IntPtr.Zero);

                            if (File.Exists(path))
                            {
                                Terminal1.TabIcon = ((Icon)NativeMethods.Helpers.ExtractSmallIcon(path)).ToBitmap();
                            }
                            else
                            {
                                Terminal1.TabIcon = null;
                                Terminal1.TabIconButItIsString = "";
                            }
                        }

                        break;
                    }
                }

                ApplyPreview(_Terminal);
            }
        }

        private void Button20_Click(object sender, EventArgs e)
        {
            UI.WP.ComboBox temp = Forms.WindowsTerminalCopycat.ComboBox1;
            temp.Items.Clear();
            CCat = null;

            if (temp != null)
            {
                foreach (object x in TerSchemes.Items) { if (x != TerSchemes.SelectedItem && x != TerSchemes.Items[0]) temp.Items.Add(x); }
            }

            if (Forms.WindowsTerminalCopycat.ShowDialog() == DialogResult.OK)
            {
                for (int x = 0, loopTo = TerSchemes.Items.Count - 1; x <= loopTo; x++)
                {
                    if ((TerSchemes.Items[x].ToString().ToLower() ?? string.Empty) == (CCat.ToLower() ?? string.Empty))
                    {
                        WinTerminal.Types.Scheme CCatFrom = _Terminal.Schemes[x - 1];
                        WinTerminal.Types.Scheme temp1 = _Terminal.Schemes[TerSchemes.SelectedIndex - 1];

                        temp1.Background = CCatFrom.Background;
                        temp1.Black = CCatFrom.Black;
                        temp1.Blue = CCatFrom.Blue;
                        temp1.BrightBlack = CCatFrom.BrightBlack;
                        temp1.BrightBlue = CCatFrom.BrightBlue;
                        temp1.BrightCyan = CCatFrom.BrightCyan;
                        temp1.BrightGreen = CCatFrom.BrightGreen;
                        temp1.BrightPurple = CCatFrom.BrightPurple;
                        temp1.BrightRed = CCatFrom.BrightRed;
                        temp1.BrightWhite = CCatFrom.BrightWhite;
                        temp1.BrightYellow = CCatFrom.BrightYellow;
                        temp1.CursorColor = CCatFrom.CursorColor;
                        temp1.Cyan = CCatFrom.Cyan;
                        temp1.Foreground = CCatFrom.Foreground;
                        temp1.Green = CCatFrom.Green;
                        temp1.Purple = CCatFrom.Purple;
                        temp1.Red = CCatFrom.Red;
                        temp1.SelectionBackground = CCatFrom.SelectionBackground;
                        temp1.White = CCatFrom.White;
                        temp1.Yellow = CCatFrom.Yellow;

                        TerBackground.BackColor = temp1.Background;
                        TerForeground.BackColor = temp1.Foreground;
                        TerSelection.BackColor = temp1.SelectionBackground;
                        TerCursor.BackColor = temp1.CursorColor;

                        TerBlack.BackColor = temp1.Black;
                        TerBlue.BackColor = temp1.Blue;
                        TerGreen.BackColor = temp1.Green;
                        TerCyan.BackColor = temp1.Cyan;
                        TerRed.BackColor = temp1.Red;
                        TerPurple.BackColor = temp1.Purple;
                        TerYellow.BackColor = temp1.Yellow;
                        TerWhite.BackColor = temp1.White;

                        TerBlackB.BackColor = temp1.BrightBlack;
                        TerBlueB.BackColor = temp1.BrightBlue;
                        TerGreenB.BackColor = temp1.BrightGreen;
                        TerCyanB.BackColor = temp1.BrightCyan;
                        TerRedB.BackColor = temp1.BrightRed;
                        TerPurpleB.BackColor = temp1.BrightPurple;
                        TerYellowB.BackColor = temp1.BrightYellow;
                        TerWhiteB.BackColor = temp1.BrightWhite;

                        ApplyPreview(_Terminal);

                        break;
                    }
                }
            }
        }

        private void Button21_Click(object sender, EventArgs e)
        {
            if (TerThemes.SelectedIndex < 4)
            {
                MsgBox(Program.Localization.Strings.Aspects.Terminals.ThemeNotCloneable, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            UI.WP.ComboBox temp = Forms.WindowsTerminalCopycat.ComboBox1;
            temp.Items.Clear();
            CCat = null;

            foreach (object x in TerThemes.Items) temp.Items.Add(x);

            if (Forms.WindowsTerminalCopycat.ShowDialog() == DialogResult.OK)
            {
                for (int x = 0, loopTo = TerThemes.Items.Count - 4; x <= loopTo; x++)
                {
                    if ((TerThemes.Items[x].ToString().ToLower() ?? string.Empty) == (CCat.ToLower() ?? string.Empty))
                    {
                        WinTerminal.Types.Theme CCatFrom = _Terminal.Themes[x - 3];
                        WinTerminal.Types.Theme temp1 = _Terminal.Themes[TerThemes.SelectedIndex - 4];

                        temp1.Window.ApplicationTheme = CCatFrom.Window.ApplicationTheme;
                        temp1.Tab.Background = CCatFrom.Tab.Background;
                        temp1.Tab.UnfocusedBackground = CCatFrom.Tab.UnfocusedBackground;
                        temp1.TabRow.Background = CCatFrom.TabRow.Background;
                        temp1.TabRow.UnfocusedBackground = CCatFrom.TabRow.UnfocusedBackground;

                        TerTitlebarActive.BackColor = CCatFrom.TabRow.Background;
                        TerTitlebarInactive.BackColor = CCatFrom.TabRow.UnfocusedBackground;
                        TerTabActive.BackColor = CCatFrom.Tab.Background;
                        TerTabInactive.BackColor = CCatFrom.Tab.UnfocusedBackground;
                        TerMode.Checked = !(CCatFrom.Window.ApplicationTheme.ToLower() == "light");

                        break;
                    }
                }

                ApplyPreview(_Terminal);
            }
        }

        private void Button23_Click(object sender, EventArgs e)
        {
            using (FontDialog dlg = new() { Font = Terminal1.Font, FixedPitchOnly = !Program.Settings.WindowsTerminals.ListAllFonts })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    TerFontName.Text = dlg.Font.Name;
                    GDI32.LogFont fx = new();
                    dlg.Font.ToLogFont(fx);
                    fx.lfWeight = TerFontWeight.SelectedIndex * 100;
                    {
                        Font temp = Font.FromLogFont(fx);
                        Terminal1.Font = new(dlg.Font.Name, dlg.Font.Size, temp.Style);
                    }
                    TerFontName.Font = new(dlg.Font.Name, 9f, Terminal1.Font.Style);
                    TerFontSizeBar.Value = (int)(dlg.Font.Size);

                    if (TerProfiles.SelectedIndex == 0)
                    {
                        _Terminal.Profiles.Defaults.Font.Face = dlg.Font.Name;
                        _Terminal.Profiles.Defaults.Font.Weight = (WinTerminal.Types.FontWeight)TerFontWeight.SelectedIndex;
                        _Terminal.Profiles.Defaults.Font.Size = dlg.Font.Size;
                    }
                    else if (TerProfiles.SelectedIndex > 0 && _Terminal.Profiles.List.Count > 0)
                    {
                        _Terminal.Profiles.List[TerProfiles.SelectedIndex - 1].Font.Face = dlg.Font.Name;
                        _Terminal.Profiles.List[TerProfiles.SelectedIndex - 1].Font.Weight = (WinTerminal.Types.FontWeight)TerFontWeight.SelectedIndex;
                        _Terminal.Profiles.List[TerProfiles.SelectedIndex - 1].Font.Size = dlg.Font.Size;
                    }
                }
            }
        }

        private void TerCursorHeightBar_ValueChanged(object sender, EventArgs e)
        {
            Terminal1.CursorHeight = (int)(sender as TrackBarX).Value;

            if (!IsShown) return;

            WinTerminal.Types.Profile temp = TerProfiles.SelectedIndex == 0 ? _Terminal.Profiles.Defaults : _Terminal.Profiles.List[TerProfiles.SelectedIndex - 1];
            temp.CursorHeight = (int)(sender as TrackBarX).Value;
        }

        private void trackBarX1_ValueChanged(object sender, EventArgs e)
        {
            Terminal1.OpacityBackImage = TerImageOpacity.Value;

            if (!IsShown) return;

            WinTerminal.Types.Profile temp = TerProfiles.SelectedIndex == 0 ? _Terminal.Profiles.Defaults : _Terminal.Profiles.List[TerProfiles.SelectedIndex - 1];
            temp.BackgroundImageOpacity = (float)(TerImageOpacity.Value / 100d);
        }

        private void trackBarX1_ValueChanged_1(object sender, EventArgs e)
        {
            Terminal1.Opacity = TerOpacityBar.Value;

            if (IsShown)
            {
                WinTerminal.Types.Profile temp = TerProfiles.SelectedIndex == 0 ? _Terminal.Profiles.Defaults : _Terminal.Profiles.List[TerProfiles.SelectedIndex - 1];
                temp.Opacity = (int)TerOpacityBar.Value;
            }
        }

        private void trackBarX1_ValueChanged_2(object sender, EventArgs e)
        {
            if (!IsShown) return;

            Terminal1.Font = new(Terminal1.Font.Name, TerFontSizeBar.Value, Terminal1.Font.Style);

            WinTerminal.Types.Profile temp = TerProfiles.SelectedIndex == 0 ? _Terminal.Profiles.Defaults : _Terminal.Profiles.List[TerProfiles.SelectedIndex - 1];
            temp.Font.Size = TerFontSizeBar.Value;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (TerThemes.SelectedIndex > 3 && MsgBox(string.Format(Program.Localization.Strings.Messages.TerminalDeleteTheme, TerThemes.SelectedItem), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int i = TerThemes.SelectedIndex;
                TerThemes.Items.RemoveAt(i);
                TerThemes.SelectedIndex = i > TerThemes.Items.Count - 1 ? TerThemes.Items.Count - 1 : i;
                _Terminal.Themes.RemoveAt(i - 4);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (TerSchemes.SelectedIndex > 0 && MsgBox(string.Format(Program.Localization.Strings.Messages.TerminalDeleteScheme, TerSchemes.SelectedItem), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int i = TerSchemes.SelectedIndex;
                TerSchemes.Items.RemoveAt(i);
                TerSchemes.SelectedIndex = i > TerSchemes.Items.Count - 1 ? TerSchemes.Items.Count - 1 : i;
                _Terminal.Schemes.RemoveAt(i - 1);
            }
        }

        private void TerWhiteB_ContextMenuMadeColorChangeInvoker(object sender, ColorItem.ContextMenuMadeColorChangeEventArgs e)
        {
            Color cx = e.Color;

            WinTerminal.Types.Scheme scheme = new();
            WinTerminal.Types.Theme theme = new();

            if (TerProfiles.SelectedIndex == 0)
            {
                scheme = _Terminal.Schemes
                    .Where(s => s.Name.ToLower() == (_Terminal.Profiles.Defaults.ColorScheme.ToString() ?? string.Empty).ToLower()).FirstOrDefault();
            }
            else if (TerProfiles.SelectedIndex > 0)
            {
                scheme = _Terminal.Schemes
                    .Where(s => s.Name.ToLower() == _Terminal.Profiles.List[TerProfiles.SelectedIndex - 1].ColorScheme.ToString().ToLower()).FirstOrDefault();
            }

            if (TerThemes.SelectedIndex > 3)
            {
                theme = _Terminal.Themes[TerThemes.SelectedIndex - 4] ?? new();
            }

            if (ColorClipboard.Event != ColorClipboard.MenuEvent.None)
            {
                if (((ColorItem)sender).Name.ToString().ToLower().Contains(TerBackground.Name.ToLower()))
                {
                    scheme.Background = cx;
                }

                if (((ColorItem)sender).Name.ToString().ToLower().Contains(TerForeground.Name.ToLower()))
                {
                    scheme.Foreground = cx;
                }

                if (((ColorItem)sender).Name.ToString().ToLower().Contains(TerSelection.Name.ToLower()))
                {
                    scheme.SelectionBackground = cx;
                }

                if (((ColorItem)sender).Name.ToString().ToLower().Contains(TerCursor.Name.ToLower()))
                {
                    scheme.CursorColor = cx;
                }

                if (((ColorItem)sender).Name.ToString().ToLower().Contains(TerTabActive.Name.ToLower()))
                {
                    theme.Tab.Background = cx;
                }

                if (((ColorItem)sender).Name.ToString().ToLower().Contains(TerTabInactive.Name.ToLower()))
                {
                    theme.Tab.UnfocusedBackground = cx;
                }

                if (((ColorItem)sender).Name.ToString().ToLower().Contains(TerTitlebarActive.Name.ToLower()))
                {
                    theme.TabRow.Background = cx;
                }

                if (((ColorItem)sender).Name.ToString().ToLower().Contains(TerTitlebarInactive.Name.ToLower()))
                {
                    theme.TabRow.UnfocusedBackground = cx;
                }

                ApplyPreview(_Terminal);
            }
        }
    }
}