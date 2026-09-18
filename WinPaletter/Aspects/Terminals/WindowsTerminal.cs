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
using static WinPaletter.Theme.Structures.WinTerminal.Types;

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
            InitializeComponent();
            SaveState = Mode;
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
            using (Manager TMx = Program.TM.Clone())
            {
                if (Mode == WinTerminal.Version.Stable)
                {
                    string TerDir = ResolveTerminalPath(WinTerminal.Version.Stable);

                    if (System.IO.File.Exists(TerDir)) { TMx.Terminal = new(TerDir, WinTerminal.Mode.JSONFile); }
                    else { TMx.Terminal = new(string.Empty, WinTerminal.Mode.Empty); }

                    _Terminal = TMx.Terminal;
                }
                else
                {
                    string TerPreDir = ResolveTerminalPath(WinTerminal.Version.Preview);

                    if (System.IO.File.Exists(TerPreDir)) { TMx.TerminalPreview = new(TerPreDir, WinTerminal.Mode.JSONFile, WinTerminal.Version.Preview); }
                    else { TMx.TerminalPreview = new(string.Empty, WinTerminal.Mode.Empty, WinTerminal.Version.Preview); }

                    _Terminal = TMx.TerminalPreview;
                }

                Load_FromTerminal();
            }
        }

        private void LoadFromDefault(object sender, EventArgs e)
        {
            using (Manager TMx = Program.TM.Clone())
            {
                if (Mode == WinTerminal.Version.Stable)
                {
                    TMx.Terminal = Default.FromOS(Program.WindowStyle).Terminal;
                    _Terminal = TMx.Terminal;
                }
                else
                {
                    TMx.TerminalPreview = Default.FromOS(Program.WindowStyle).TerminalPreview;
                    _Terminal = TMx.TerminalPreview;
                }

                Load_FromTerminal();
            }
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

            _Terminal.Enabled = AspectEnabled;
            _Terminal.SaveToggleState();

            if (AspectEnabled)
            {
                if (Program.Settings.BackupTheme.Enabled && Program.Settings.BackupTheme.AutoBackupOnApplySingleAspect)
                {
                    using (Manager TMx = new(Manager.Source.Registry))
                    {
                        string filename = Program.GetUniqueFileName(SysPaths.ThemesBackup_OnAspectApply, $"{TMx.Info.ThemeName}_{DateTime.Now.Hour}.{DateTime.Now.Minute}.{DateTime.Now.Second}.wpth");
                        TMx.Save(Manager.Source.File, filename);
                    }
                }

                if (OS.W12 || OS.W11 || OS.W10)
                {
                    Cursor = System.Windows.Forms.Cursors.WaitCursor;

                    string TerDir = ResolveTerminalPath(WinTerminal.Version.Stable);
                    string TerPreDir = ResolveTerminalPath(WinTerminal.Version.Preview);

                    if (File.Exists(TerDir) && Mode == WinTerminal.Version.Stable)
                    {
                        _Terminal.Save(WinTerminal.Mode.JSONFile);
                    }
                    else if (File.Exists(TerPreDir) && Mode == WinTerminal.Version.Preview)
                    {
                        _Terminal.Save(WinTerminal.Mode.JSONFile, WinTerminal.Version.Preview);
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
                        _TerminalDefault = Program.TM.Terminal.Clone();
                        Text = Program.Localization.Strings.Aspects.TerminalStable;
                        AspectEnabled = Program.TM.Terminal.Enabled;
                        break;
                    }

                case WinTerminal.Version.Preview:
                    {
                        _Terminal = Program.TM.TerminalPreview;
                        _TerminalDefault = Program.TM.TerminalPreview.Clone();
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
            if (_Terminal == null) return;

            AspectEnabled = _Terminal.Enabled;

            FillTerminalSchemes(_Terminal, TerSchemes);
            FillTerminalThemes(_Terminal, TerThemes);
            FillTerminalProfiles(_Terminal, TerProfiles);

            TerProfiles.SelectedIndex = 0;

            Terminal1.PreviewVersion = Mode == WinTerminal.Version.Preview;
            Terminal2.PreviewVersion = Mode == WinTerminal.Version.Preview;

            string terminalTheme = _Terminal.Theme;

            if (terminalTheme != null)
            {
                if (terminalTheme.Equals("dark", StringComparison.OrdinalIgnoreCase))
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
                else if (terminalTheme.Equals("light", StringComparison.OrdinalIgnoreCase))
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
                else if (terminalTheme.Equals("system", StringComparison.OrdinalIgnoreCase))
                {
                    TerThemes.SelectedIndex = 3;
                    TerTitlebarActive.BackColor = default;
                    TerTitlebarInactive.BackColor = default;
                    TerTabActive.BackColor = default;
                    TerTabInactive.BackColor = default;

                    ApplySystemThemeToPreview();
                }
                else if (TerThemes.Items.Contains(terminalTheme))
                {
                    TerThemes.SelectedItem = terminalTheme;
                    TerThemesContainer.Enabled = true;

                    int idx = TerThemes.SelectedIndex - 4;
                    if (idx >= 0 && idx < _Terminal.Themes.Count)
                    {
                        WinTerminal.Types.Theme temp = _Terminal.Themes[idx];
                        TerTitlebarActive.BackColor = temp.TabRow.Background;
                        TerTitlebarInactive.BackColor = temp.TabRow.UnfocusedBackground;
                        TerTabActive.BackColor = temp.Tab.Background;
                        TerTabInactive.BackColor = temp.Tab.UnfocusedBackground;
                        bool isLightTheme = temp.Window.ApplicationTheme.Equals("light", StringComparison.OrdinalIgnoreCase);
                        TerMode.Checked = !isLightTheme;
                        Terminal1.Light = !isLightTheme;
                        Terminal2.Light = !isLightTheme;
                    }
                }
                else
                {
                    TerThemes.SelectedIndex = 0;
                    ApplySystemThemeToPreview();
                }
            }
            else
            {
                TerThemes.SelectedIndex = 0;
                ApplySystemThemeToPreview();
            }

            ApplyPreview(_Terminal);
        }

        private void ApplySystemThemeToPreview()
        {
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
                        TerMode.Checked = !Program.TM.Windows11.AppMode_Light;
                        Terminal1.Light = Program.TM.Windows11.AppMode_Light;
                        Terminal2.Light = Program.TM.Windows11.AppMode_Light;
                        break;
                    }
            }
        }

        public void FillTerminalSchemes(WinTerminal Terminal, UI.WP.ComboBox Combobox)
        {
            Combobox.Items.Clear();
            Combobox.Items.Add($"({Program.Localization.Strings.General.Default})");

            if (Terminal?.Schemes != null && Terminal.Schemes.Count > 0)
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

            if (Terminal?.Themes != null && Terminal.Themes.Count > 0)
            {
                for (int x = 0, loopTo = Terminal.Themes.Count - 1; x <= loopTo; x++) Combobox.Items.Add(Terminal.Themes[x].Name);
            }
        }

        public void FillTerminalProfiles(WinTerminal Terminal, UI.WP.ComboBox Combobox)
        {
            Combobox.Items.Clear();
            Combobox.Items.Add($"{Program.Localization.Strings.General.Defaults}");

            if (Terminal?.Profiles?.List != null && Terminal.Profiles.List.Count > 0)
            {
                for (int x = 0, loopTo = Terminal.Profiles.List.Count - 1; x <= loopTo; x++) Combobox.Items.Add(Terminal.Profiles.List[x].Name);
            }
        }

        /// <summary>
        /// Returns the current profile (or defaults) from the model, or null if the index is out of range.
        /// </summary>
        private WinTerminal.Types.Profile GetCurrentProfile()
        {
            if (_Terminal == null || TerProfiles.SelectedIndex < 0) return null;

            if (TerProfiles.SelectedIndex == 0) return _Terminal.Profiles.Defaults;

            int idx = TerProfiles.SelectedIndex - 1;
            if (_Terminal.Profiles?.List == null || idx >= _Terminal.Profiles.List.Count) return _Terminal.Profiles?.Defaults;

            return _Terminal.Profiles.List[idx];
        }

        /// <summary>
        /// Returns the "defaults" profile for the current terminal, or null if unavailable.
        /// Used as the fallback source whenever a selected profile omits a value.
        /// </summary>
        private WinTerminal.Types.Profile GetDefaultsProfile()
        {
            return _Terminal?.Profiles?.Defaults;
        }

        /// <summary>
        /// Returns an effective profile: the selected profile with any unset field filled in from profiles.defaults.
        /// If the selected profile IS defaults (index 0), it is returned as-is. A non-null instance is always returned (a fresh empty profile if the model has nothing).
        /// </summary>
        private WinTerminal.Types.Profile GetEffectiveProfile()
        {
            WinTerminal.Types.Profile selected = GetCurrentProfile();
            WinTerminal.Types.Profile defaults = GetDefaultsProfile();

            if (selected == null && defaults == null) return new WinTerminal.Types.Profile();
            if (selected == null) return defaults;
            if (selected == defaults) return selected;

            return new WinTerminal.Types.Profile
            {
                Name = selected.Name,
                Guid = selected.Guid,
                TabColor = selected.TabColor == Color.Empty ? defaults?.TabColor ?? Color.Empty : selected.TabColor,
                BackgroundImage = string.IsNullOrEmpty(selected.BackgroundImage) ? defaults?.BackgroundImage ?? string.Empty : selected.BackgroundImage,
                CursorShape = selected.CursorShape,
                // ColorScheme is a reference; null means "inherit from defaults".
                ColorScheme = selected.ColorScheme ?? defaults?.ColorScheme,
                CursorHeight = selected.CursorHeight > 0 ? selected.CursorHeight : defaults?.CursorHeight ?? 25,
                Opacity = selected.Opacity > 0 ? selected.Opacity : defaults?.Opacity ?? 100,
                BackgroundImageOpacity = selected.BackgroundImageOpacity > 0 ? selected.BackgroundImageOpacity : defaults?.BackgroundImageOpacity ?? 100,
                UseAcrylic = selected.UseAcrylic,
                Font = new WinTerminal.Types.FontSettings
                {
                    Face = string.IsNullOrEmpty(selected.Font?.Face) ? defaults?.Font?.Face ?? "Cascadia Mono" : selected.Font.Face,
                    Size = selected.Font != null && selected.Font.Size > 0 ? selected.Font.Size : defaults?.Font?.Size ?? 12f,
                    Weight = selected.Font != null ? selected.Font.Weight : defaults?.Font?.Weight ?? WinTerminal.Types.FontWeight.Normal
                },
                Commandline = string.IsNullOrEmpty(selected.Commandline) ? defaults?.Commandline ?? string.Empty : selected.Commandline,
                Icon = string.IsNullOrEmpty(selected.Icon) ? defaults?.Icon : selected.Icon,
                TabTitle = string.IsNullOrEmpty(selected.TabTitle) ? defaults?.TabTitle : selected.TabTitle
            };
        }

        /// <summary>
        /// Returns the current scheme (or null if none / out of range). The "default" scheme (index 0) resolves via the effective profile's ColorScheme name, falling back to the first available scheme.
        /// </summary>
        private WinTerminal.Types.Scheme GetCurrentScheme()
        {
            if (_Terminal?.Schemes == null || _Terminal.Schemes.Count == 0) return null;

            if (TerSchemes.SelectedIndex <= 0)
            {
                string schemeName = GetEffectiveProfile()?.ColorScheme?.ToString() ?? _Terminal.Profiles?.Defaults?.ColorScheme?.ToString();

                if (string.IsNullOrWhiteSpace(schemeName)) return _Terminal.Schemes.FirstOrDefault();

                return _Terminal.Schemes.FirstOrDefault(s => string.Equals(s.Name, schemeName, StringComparison.OrdinalIgnoreCase)) ?? _Terminal.Schemes.FirstOrDefault();
            }

            int idx = TerSchemes.SelectedIndex - 1;
            if (idx >= _Terminal.Schemes.Count) return null;

            return _Terminal.Schemes[idx];
        }

        /// <summary>
        /// Returns the current theme (or null if index points to a built-in).
        /// </summary>
        private WinTerminal.Types.Theme GetCurrentTheme()
        {
            if (_Terminal?.Themes == null) return null;
            if (TerThemes.SelectedIndex <= 3) return null;

            int idx = TerThemes.SelectedIndex - 4;
            if (idx >= _Terminal.Themes.Count) return null;

            return _Terminal.Themes[idx];
        }

        /// <summary>
        /// Resolves the on-disk path of the Windows Terminal settings JSON, honoring path deflection.
        /// </summary>
        private static string ResolveTerminalPath(WinTerminal.Version version)
        {
            if (!Program.Settings.WindowsTerminals.Path_Deflection)
            {
                return version == WinTerminal.Version.Stable ? SysPaths.TerminalJSON : SysPaths.TerminalPreviewJSON;
            }

            if (version == WinTerminal.Version.Stable)
            {
                return System.IO.File.Exists(Program.Settings.WindowsTerminals.Terminal_Stable_Path)
                    ? Program.Settings.WindowsTerminals.Terminal_Stable_Path
                    : SysPaths.TerminalJSON;
            }

            return System.IO.File.Exists(Program.Settings.WindowsTerminals.Terminal_Preview_Path)
                ? Program.Settings.WindowsTerminals.Terminal_Preview_Path
                : SysPaths.TerminalPreviewJSON;
        }

        private void TerSchemes_SelectedIndexChanged(object sender, EventArgs e)
        {
            TerDeleteScheme.Enabled = TerSchemes.SelectedIndex > 0;
            TerEditScheme.Enabled = TerSchemes.SelectedIndex > 0;

            if (TerSchemes.SelectedIndex <= -1) return;

            SetDefaultsToScheme(TerSchemes.SelectedItem?.ToString());

            WinTerminal.Types.Scheme scheme = GetCurrentScheme() ?? new();
            Profile profile = GetCurrentProfile();
            profile.ColorScheme.Dark = scheme.Name;
            profile.ColorScheme.Light = scheme.Name;

            TerBackground.BackColor = scheme.Background;
            TerForeground.BackColor = scheme.Foreground;
            TerSelection.BackColor = scheme.SelectionBackground;
            TerCursor.BackColor = scheme.CursorColor;

            TerBlack.BackColor = scheme.Black;
            TerBlue.BackColor = scheme.Blue;
            TerGreen.BackColor = scheme.Green;
            TerCyan.BackColor = scheme.Cyan;
            TerRed.BackColor = scheme.Red;
            TerPurple.BackColor = scheme.Purple;
            TerYellow.BackColor = scheme.Yellow;
            TerWhite.BackColor = scheme.White;

            TerBlackB.BackColor = scheme.BrightBlack;
            TerBlueB.BackColor = scheme.BrightBlue;
            TerGreenB.BackColor = scheme.BrightGreen;
            TerCyanB.BackColor = scheme.BrightCyan;
            TerRedB.BackColor = scheme.BrightRed;
            TerPurpleB.BackColor = scheme.BrightPurple;
            TerYellowB.BackColor = scheme.BrightYellow;
            TerWhiteB.BackColor = scheme.BrightWhite;

            if (IsShown) ApplyPreview(_Terminal);
        }

        private void TerProfiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Use the effective profile so any field the profile left unset is filled in from profiles.defaults (same layering Windows Terminal itself performs at runtime).
            WinTerminal.Types.Profile profile = GetEffectiveProfile();

            if (profile == null) return;

            string schemeName = (profile.ColorScheme ?? (object)string.Empty).ToString();

            if (string.IsNullOrWhiteSpace(schemeName))
            {
                TerSchemes.SelectedIndex = 0;
            }
            else if (TerSchemes.Items.Contains(schemeName))
            {
                TerSchemes.SelectedItem = schemeName;
            }
            else
            {
                TerSchemes.SelectedIndex = 0;
            }

            TerBackImage.Text = profile.BackgroundImage ?? string.Empty;
            TerImageOpacity.Value = Math.Min(TerImageOpacity.Maximum, Math.Max(TerImageOpacity.Minimum, (int)(profile.BackgroundImageOpacity * 100f)));

            TerCursorStyle.SelectedIndex = Math.Min(TerCursorStyle.Items.Count - 1, Math.Max(0, (int)profile.CursorShape));
            TerCursorHeightBar.Value = Math.Min(TerCursorHeightBar.Maximum, Math.Max(TerCursorHeightBar.Minimum, profile.CursorHeight));

            TerFontName.Text = profile.Font?.Face ?? string.Empty;
            GDI32.LOGFONT fx = new();

            using (Font f_cmd = new(profile.Font?.Face ?? "Cascadia Mono", profile.Font?.Size ?? 12f))
            {
                f_cmd.ToLogFont(fx);
                fx.lfWeight = (int)(profile.Font?.Weight ?? FontWeight.Normal) * 100;

                using (Font temp = Font.FromLogFont(fx))
                using (Font f_cmd_x = new(f_cmd.Name, f_cmd.Size, temp.Style))
                {
                    TerFontName.Font = new(f_cmd_x.Name, 9f, f_cmd_x.Style);
                }
            }

            TerFontSizeBar.Value = Math.Min(TerFontSizeBar.Maximum, Math.Max(TerFontSizeBar.Minimum, (int)(profile.Font?.Size ?? 12f)));
            TerFontWeight.SelectedIndex = Math.Min(TerFontWeight.Items.Count - 1, Math.Max(0, (int)(profile.Font?.Weight ?? FontWeight.Normal)));

            TerAcrylic.Checked = profile.UseAcrylic;
            TerOpacityBar.Value = Math.Min(TerOpacityBar.Maximum, Math.Max(TerOpacityBar.Minimum, profile.Opacity));

            Terminal1.Opacity = profile.Opacity;
            Terminal1.OpacityBackImage = (float)profile.BackgroundImageOpacity * 100f;
            Terminal1.TabIcon?.Dispose();

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
                Terminal1.TabIconButItIsString = null;
            }
            else
            {
                IntPtr intPtr = IntPtr.Zero;
                Kernel32.Wow64DisableWow64FsRedirection(ref intPtr);
                string path = string.Empty;
                if (profile.Commandline is not null) path = profile.Commandline.Replace("%SystemRoot%", SysPaths.Windows);
                Kernel32.Wow64RevertWow64FsRedirection(intPtr);

                if (File.Exists(path))
                {
                    using (Icon ico = NativeMethods.Helpers.ExtractSmallIcon(path))
                    {
                        Terminal1.TabIcon = ico.ToBitmap();
                    }

                    Terminal1.TabIconButItIsString = null;
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

            WinTerminal.Types.Profile temp = GetCurrentProfile();
            if (temp != null) temp.CursorShape = (WinTerminal.Types.CursorShape)TerCursorStyle.SelectedIndex;
        }

        public void SetDefaultsToScheme(string Scheme)
        {
            switch (Scheme?.ToLower() ?? string.Empty)
            {
                case var @case when @case == ("Campbell".ToLower()):
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

                case var case1 when case1 == ("Campbell Powershell".ToLower()):
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

                case var caseCGA when caseCGA == ("CGA".ToLower()):
                    {
                        TerBackground.DefaultBackColor = Color.FromArgb(0, 0, 0);
                        TerBlack.DefaultBackColor = Color.FromArgb(0, 0, 0);
                        TerBlue.DefaultBackColor = Color.FromArgb(0, 0, 170);
                        TerBlackB.DefaultBackColor = Color.FromArgb(85, 85, 85);
                        TerBlueB.DefaultBackColor = Color.FromArgb(85, 85, 255);
                        TerCyanB.DefaultBackColor = Color.FromArgb(85, 255, 255);
                        TerGreenB.DefaultBackColor = Color.FromArgb(85, 255, 85);
                        TerPurpleB.DefaultBackColor = Color.FromArgb(255, 85, 255);
                        TerRedB.DefaultBackColor = Color.FromArgb(255, 85, 85);
                        TerWhiteB.DefaultBackColor = Color.FromArgb(255, 255, 255);
                        TerYellowB.DefaultBackColor = Color.FromArgb(255, 255, 85);
                        TerCursor.DefaultBackColor = Color.FromArgb(0, 170, 0);
                        TerCyan.DefaultBackColor = Color.FromArgb(0, 170, 170);
                        TerForeground.DefaultBackColor = Color.FromArgb(170, 170, 170);
                        TerGreen.DefaultBackColor = Color.FromArgb(0, 170, 0);
                        TerPurple.DefaultBackColor = Color.FromArgb(170, 0, 170);
                        TerRed.DefaultBackColor = Color.FromArgb(170, 0, 0);
                        TerSelection.DefaultBackColor = Color.FromArgb(255, 255, 255);
                        TerWhite.DefaultBackColor = Color.FromArgb(170, 170, 170);
                        TerYellow.DefaultBackColor = Color.FromArgb(170, 85, 0);
                        break;
                    }

                case var caseDarkPlus when caseDarkPlus == ("Dark+".ToLower()):
                    {
                        TerBackground.DefaultBackColor = Color.FromArgb(30, 30, 30);
                        TerBlack.DefaultBackColor = Color.FromArgb(0, 0, 0);
                        TerBlue.DefaultBackColor = Color.FromArgb(36, 114, 200);
                        TerBlackB.DefaultBackColor = Color.FromArgb(102, 102, 102);
                        TerBlueB.DefaultBackColor = Color.FromArgb(59, 142, 234);
                        TerCyanB.DefaultBackColor = Color.FromArgb(41, 184, 219);
                        TerGreenB.DefaultBackColor = Color.FromArgb(35, 209, 139);
                        TerPurpleB.DefaultBackColor = Color.FromArgb(214, 112, 214);
                        TerRedB.DefaultBackColor = Color.FromArgb(241, 76, 76);
                        TerWhiteB.DefaultBackColor = Color.FromArgb(229, 229, 229);
                        TerYellowB.DefaultBackColor = Color.FromArgb(245, 245, 67);
                        TerCursor.DefaultBackColor = Color.FromArgb(128, 128, 128);
                        TerCyan.DefaultBackColor = Color.FromArgb(17, 168, 205);
                        TerForeground.DefaultBackColor = Color.FromArgb(204, 204, 204);
                        TerGreen.DefaultBackColor = Color.FromArgb(13, 188, 121);
                        TerPurple.DefaultBackColor = Color.FromArgb(188, 63, 188);
                        TerRed.DefaultBackColor = Color.FromArgb(205, 49, 49);
                        TerSelection.DefaultBackColor = Color.FromArgb(255, 255, 255);
                        TerWhite.DefaultBackColor = Color.FromArgb(229, 229, 229);
                        TerYellow.DefaultBackColor = Color.FromArgb(229, 229, 16);
                        break;
                    }

                case var caseDimidium when caseDimidium == ("Dimidium".ToLower()):
                    {
                        TerBackground.DefaultBackColor = Color.FromArgb(20, 20, 20);
                        TerBlack.DefaultBackColor = Color.FromArgb(0, 0, 0);
                        TerBlue.DefaultBackColor = Color.FromArgb(5, 117, 216);
                        TerBlackB.DefaultBackColor = Color.FromArgb(129, 126, 126);
                        TerBlueB.DefaultBackColor = Color.FromArgb(104, 141, 253);
                        TerCyanB.DefaultBackColor = Color.FromArgb(50, 224, 251);
                        TerGreenB.DefaultBackColor = Color.FromArgb(55, 229, 123);
                        TerPurpleB.DefaultBackColor = Color.FromArgb(237, 111, 233);
                        TerRedB.DefaultBackColor = Color.FromArgb(255, 100, 59);
                        TerWhiteB.DefaultBackColor = Color.FromArgb(211, 216, 217);
                        TerYellowB.DefaultBackColor = Color.FromArgb(252, 205, 26);
                        TerCursor.DefaultBackColor = Color.FromArgb(55, 229, 123);
                        TerCyan.DefaultBackColor = Color.FromArgb(29, 182, 187);
                        TerForeground.DefaultBackColor = Color.FromArgb(186, 183, 182);
                        TerGreen.DefaultBackColor = Color.FromArgb(96, 180, 66);
                        TerPurple.DefaultBackColor = Color.FromArgb(175, 94, 210);
                        TerRed.DefaultBackColor = Color.FromArgb(207, 73, 76);
                        TerSelection.DefaultBackColor = Color.FromArgb(255, 255, 255);
                        TerWhite.DefaultBackColor = Color.FromArgb(186, 183, 182);
                        TerYellow.DefaultBackColor = Color.FromArgb(219, 156, 17);
                        break;
                    }

                case var caseIBM5153 when caseIBM5153 == ("IBM 5153".ToLower()):
                    {
                        TerBackground.DefaultBackColor = Color.FromArgb(0, 0, 0);
                        TerBlack.DefaultBackColor = Color.FromArgb(0, 0, 0);
                        TerBlue.DefaultBackColor = Color.FromArgb(0, 0, 170);
                        TerBlackB.DefaultBackColor = Color.FromArgb(85, 85, 85);
                        TerBlueB.DefaultBackColor = Color.FromArgb(85, 85, 255);
                        TerCyanB.DefaultBackColor = Color.FromArgb(85, 255, 255);
                        TerGreenB.DefaultBackColor = Color.FromArgb(85, 255, 85);
                        TerPurpleB.DefaultBackColor = Color.FromArgb(255, 85, 255);
                        TerRedB.DefaultBackColor = Color.FromArgb(255, 85, 85);
                        TerWhiteB.DefaultBackColor = Color.FromArgb(255, 255, 255);
                        TerYellowB.DefaultBackColor = Color.FromArgb(255, 255, 85);
                        TerCursor.DefaultBackColor = Color.FromArgb(0, 170, 0);
                        TerCyan.DefaultBackColor = Color.FromArgb(0, 170, 170);
                        TerForeground.DefaultBackColor = Color.FromArgb(170, 170, 170);
                        TerGreen.DefaultBackColor = Color.FromArgb(0, 170, 0);
                        TerPurple.DefaultBackColor = Color.FromArgb(170, 0, 170);
                        TerRed.DefaultBackColor = Color.FromArgb(170, 0, 0);
                        TerSelection.DefaultBackColor = Color.FromArgb(255, 255, 255);
                        TerWhite.DefaultBackColor = Color.FromArgb(170, 170, 170);
                        TerYellow.DefaultBackColor = Color.FromArgb(196, 126, 0);
                        break;
                    }

                case var case2 when case2 == ("One Half Dark".ToLower()):
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

                case var case3 when case3 == ("One Half Light".ToLower()):
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

                case var caseOttosson when caseOttosson == ("Ottosson".ToLower()):
                    {
                        TerBackground.DefaultBackColor = Color.FromArgb(0, 0, 0);
                        TerBlack.DefaultBackColor = Color.FromArgb(0, 0, 0);
                        TerBlue.DefaultBackColor = Color.FromArgb(32, 77, 190);
                        TerBlackB.DefaultBackColor = Color.FromArgb(128, 128, 128);
                        TerBlueB.DefaultBackColor = Color.FromArgb(47, 106, 255);
                        TerCyanB.DefaultBackColor = Color.FromArgb(0, 225, 240);
                        TerGreenB.DefaultBackColor = Color.FromArgb(88, 234, 81);
                        TerPurpleB.DefaultBackColor = Color.FromArgb(252, 116, 255);
                        TerRedB.DefaultBackColor = Color.FromArgb(255, 62, 48);
                        TerWhiteB.DefaultBackColor = Color.FromArgb(255, 255, 255);
                        TerYellowB.DefaultBackColor = Color.FromArgb(255, 201, 68);
                        TerCursor.DefaultBackColor = Color.FromArgb(255, 255, 255);
                        TerCyan.DefaultBackColor = Color.FromArgb(0, 167, 178);
                        TerForeground.DefaultBackColor = Color.FromArgb(190, 190, 190);
                        TerGreen.DefaultBackColor = Color.FromArgb(63, 174, 58);
                        TerPurple.DefaultBackColor = Color.FromArgb(187, 84, 190);
                        TerRed.DefaultBackColor = Color.FromArgb(190, 44, 33);
                        TerSelection.DefaultBackColor = Color.FromArgb(146, 164, 253);
                        TerWhite.DefaultBackColor = Color.FromArgb(190, 190, 190);
                        TerYellow.DefaultBackColor = Color.FromArgb(190, 154, 74);
                        break;
                    }

                case var case4 when case4 == ("Solarized Dark".ToLower()):
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

                case var case5 when case5 == ("Solarized Light".ToLower()):
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

                case var case6 when case6 == ("Tango Dark".ToLower()):
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

                case var case7 when case7 == ("Tango Light".ToLower()):
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

                case var case8 when case8 == ("Vintage".ToLower()):
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
            if (_Terminal?.Schemes == null) return;

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
            WinTerminal.Types.Scheme scheme = GetCurrentScheme();
            WinTerminal.Types.Theme theme = GetCurrentTheme();

            if (scheme == null) return;

            // Don't mutate the model from a combo-box-driven selection unless the user actually edits. For schemes, GetCurrentScheme() returns
            // a live reference; for themes, GetCurrentTheme() may be null.
            string name = ((ColorItem)sender).Name.ToString().ToLower();

            if (e is DragEventArgs)
            {
                if (name.Contains(TerBackground.Name.ToLower())) scheme.Background = ((ColorItem)sender).BackColor;
                if (name.Contains(TerForeground.Name.ToLower())) scheme.Foreground = ((ColorItem)sender).BackColor;
                if (name.Contains(TerSelection.Name.ToLower())) scheme.SelectionBackground = ((ColorItem)sender).BackColor;
                if (name.Contains(TerCursor.Name.ToLower())) scheme.CursorColor = ((ColorItem)sender).BackColor;

                if (theme != null)
                {
                    if (name.Contains(TerTabActive.Name.ToLower())) theme.Tab.Background = ((ColorItem)sender).BackColor;
                    if (name.Contains(TerTabInactive.Name.ToLower())) theme.Tab.UnfocusedBackground = ((ColorItem)sender).BackColor;
                    if (name.Contains(TerTitlebarActive.Name.ToLower())) theme.TabRow.Background = ((ColorItem)sender).BackColor;
                    if (name.Contains(TerTitlebarInactive.Name.ToLower())) theme.TabRow.UnfocusedBackground = ((ColorItem)sender).BackColor;
                }

                ApplyPreview(_Terminal);
                return;
            }

            ColorItem colorItem = (ColorItem)sender;
            Dictionary<Control, string[]> CList = new()
            {
                { colorItem, new string[] { nameof(colorItem.BackColor) } },
            };

            if (name.Contains(TerBackground.Name.ToLower()))
            {
                CList.Add(Terminal1, [nameof(Terminal1.Color_Background)]);
                CList.Add(Terminal2, [nameof(Terminal2.Color_Background)]);
            }

            if (name.Contains(TerForeground.Name.ToLower()))
            {
                CList.Add(Terminal1, [nameof(Terminal1.Color_Foreground)]);
                CList.Add(Terminal2, [nameof(Terminal2.Color_Foreground)]);
            }

            if (name.Contains(TerSelection.Name.ToLower()))
            {
                CList.Add(Terminal1, [nameof(Terminal1.Color_Selection)]);
                CList.Add(Terminal2, [nameof(Terminal2.Color_Selection)]);
            }

            if (name.Contains(TerCursor.Name.ToLower()))
            {
                CList.Add(Terminal1, [nameof(Terminal1.Color_Cursor)]);
                CList.Add(Terminal2, [nameof(Terminal2.Color_Cursor)]);
            }

            if (name.Contains(TerTabActive.Name.ToLower()))
            {
                CList.Add(Terminal1, [nameof(Terminal1.TabColor)]);
                CList.Add(Terminal2, [nameof(Terminal2.TabColor)]);
            }

            if (name.Contains(TerTabInactive.Name.ToLower()))
            {
                CList.Add(Terminal1, [nameof(Terminal1.Color_TabUnFocused)]);
                CList.Add(Terminal2, [nameof(Terminal2.Color_TabUnFocused)]);
            }

            if (name.Contains(TerTitlebarActive.Name.ToLower()))
                CList.Add(Terminal1, [nameof(Terminal1.Color_Titlebar)]);

            if (name.Contains(TerTitlebarInactive.Name.ToLower()))
                CList.Add(Terminal2, [nameof(Terminal2.Color_Titlebar_Unfocused)]);

            Color C = Forms.ColorPickerDlg.Pick(CList);

            if (name.Contains(TerBackground.Name.ToLower())) scheme.Background = C;
            if (name.Contains(TerForeground.Name.ToLower())) scheme.Foreground = C;
            if (name.Contains(TerSelection.Name.ToLower())) scheme.SelectionBackground = C;
            if (name.Contains(TerCursor.Name.ToLower())) scheme.CursorColor = C;

            if (theme != null)
            {
                if (name.Contains(TerTabActive.Name.ToLower())) theme.Tab.Background = C;
                if (name.Contains(TerTabInactive.Name.ToLower())) theme.Tab.UnfocusedBackground = C;
                if (name.Contains(TerTitlebarActive.Name.ToLower())) theme.TabRow.Background = C;
                if (name.Contains(TerTitlebarInactive.Name.ToLower())) theme.TabRow.UnfocusedBackground = C;
            }

            ApplyPreview(_Terminal);

            colorItem.BackColor = C;
            colorItem.Refresh();
            CList.Clear();
        }

        public void ApplyPreview(WinTerminal Terminal)
        {
            if (Terminal == null) return;

            Terminal1.UseAcrylicOnTitlebar = Terminal.UseAcrylicInTabRow;

            // Use the effective profile so unset fields are inherited from profiles.defaults.
            WinTerminal.Types.Profile currentProfile = GetEffectiveProfile();

            if (TerProfiles.SelectedIndex == 0)
            {
                Terminal1.TabColor = Terminal.Profiles?.Defaults?.TabColor ?? Color.Empty;
            }
            else if (currentProfile != null && currentProfile.TabColor == Color.FromArgb(0, 0, 0, 0))
            {
                Terminal1.TabColor = Terminal.Profiles?.Defaults?.TabColor ?? Color.Empty;
            }
            else if (currentProfile != null)
            {
                Terminal1.TabColor = currentProfile.TabColor;
            }

            WinTerminal.Types.Scheme temp = GetCurrentScheme() ?? new();

            Terminal1.Color_Background = temp.Background;
            Terminal1.Color_Foreground = temp.Foreground;
            Terminal1.Color_Selection = temp.SelectionBackground;
            Terminal1.Color_Cursor = temp.CursorColor;

            Terminal2.Color_Background = Terminal1.Color_Background;
            Terminal2.Color_Foreground = Terminal1.Color_Foreground;
            Terminal2.Color_Selection = Terminal1.Color_Selection;
            Terminal2.Color_Cursor = Terminal1.Color_Cursor;

            if (TerThemesContainer.Enabled)
            {
                WinTerminal.Types.Theme theme = GetCurrentTheme() ?? new();
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
                string selected = TerThemes.SelectedItem.ToString().ToLower();

                if (selected == "dark")
                {
                    Terminal1.Light = false;
                    Terminal2.Light = false;
                }
                else if (selected == "light")
                {
                    Terminal1.Light = true;
                    Terminal2.Light = true;
                }
                else if (selected == "system")
                {
                    switch (Program.WindowStyle)
                    {
                        case WindowStyle.W12:
                            Terminal1.Light = Program.TM.Windows12.AppMode_Light;
                            Terminal2.Light = Program.TM.Windows12.AppMode_Light;
                            break;

                        case WindowStyle.W11:
                            Terminal1.Light = Program.TM.Windows11.AppMode_Light;
                            Terminal2.Light = Program.TM.Windows11.AppMode_Light;
                            break;

                        case WindowStyle.W10:
                            Terminal1.Light = Program.TM.Windows10.AppMode_Light;
                            Terminal2.Light = Program.TM.Windows10.AppMode_Light;
                            break;

                        default:
                            Terminal1.Light = Program.TM.Windows11.AppMode_Light;
                            Terminal2.Light = Program.TM.Windows11.AppMode_Light;
                            break;
                    }
                }
                else
                {
                    Terminal1.Light = !TerMode.Checked;
                    Terminal2.Light = !TerMode.Checked;
                }
            }

            WinTerminal.Types.Profile temp_p = currentProfile ?? Terminal.Profiles?.Defaults;
            if (temp_p?.Font != null)
            {
                GDI32.LOGFONT fx = new();
                Font f_cmd = new(temp_p.Font.Face, temp_p.Font.Size);
                f_cmd.ToLogFont(fx);
                fx.lfWeight = (int)temp_p.Font.Weight * 100;
                using (Font temp_s = Font.FromLogFont(fx))
                {
                    f_cmd = new(f_cmd.Name, f_cmd.Size, temp_s.Style);
                }
                Terminal1.Font = f_cmd;
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (_Terminal?.Themes == null) return;

            string s = InputBox(Program.Localization.Strings.Aspects.Terminals.TypeSchemeName, $"{Program.Localization.Strings.General.NewTheme} #{TerThemes.Items.Count - 4}");
            if (string.IsNullOrWhiteSpace(s)) return;
            _Terminal.Themes.Add(new() { Name = s });
            FillTerminalThemes(_Terminal, TerThemes);
            TerThemes.SelectedIndex = TerThemes.Items.Count - 1;
        }

        private void TerFontWeight_SelectedIndexChanged(object sender, EventArgs e)
        {
            GDI32.LOGFONT fx = new();
            Font f_cmd = new(Terminal1.Font.Name, Terminal1.Font.Size, Terminal1.Font.Style);
            f_cmd.ToLogFont(fx);
            fx.lfWeight = Math.Max(100, TerFontWeight.SelectedIndex * 100);
            {
                using (Font temp = Font.FromLogFont(fx))
                {
                    f_cmd = new(Terminal1.Font.Name, Terminal1.Font.Size, temp.Style);
                }
            }
            Terminal1.Font = f_cmd;

            WinTerminal.Types.Profile temp1 = GetCurrentProfile();
            if (temp1 != null) temp1.Font.Weight = (WinTerminal.Types.FontWeight)TerFontWeight.SelectedIndex;
        }

        private void TerThemes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!IsShown) return;

            TerDeleteTheme.Enabled = TerThemes.SelectedIndex > 3;
            TerEditThemeName.Enabled = TerThemes.SelectedIndex > 3;

            if (TerThemes.SelectedIndex > 3)
            {
                TerThemesContainer.Enabled = true;
                WinTerminal.Types.Theme theme = GetCurrentTheme() ?? new();

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

                if (TerThemes.SelectedIndex == 1) TerMode.Checked = true;
                if (TerThemes.SelectedIndex == 2) TerMode.Checked = false;

                switch (Program.WindowStyle)
                {
                    case WindowStyle.W12:
                        if (TerThemes.SelectedIndex == 3) TerMode.Checked = !Program.TM.Windows12.AppMode_Light;
                        break;

                    case WindowStyle.W11:
                        if (TerThemes.SelectedIndex == 3) TerMode.Checked = !Program.TM.Windows11.AppMode_Light;
                        break;

                    case WindowStyle.W10:
                        if (TerThemes.SelectedIndex == 3) TerMode.Checked = !Program.TM.Windows10.AppMode_Light;
                        break;

                    default:
                        if (TerThemes.SelectedIndex == 3) TerMode.Checked = !Program.TM.Windows11.AppMode_Light;
                        break;
                }
            }

            if (TerThemes.SelectedIndex == 0) _Terminal.Theme = null;
            else if (TerThemes.SelectedIndex == 1) _Terminal.Theme = "dark";
            else if (TerThemes.SelectedIndex == 2) _Terminal.Theme = "light";
            else if (TerThemes.SelectedIndex == 3) _Terminal.Theme = "system";
            else _Terminal.Theme = TerThemes.SelectedItem?.ToString();

            ApplyPreview(_Terminal);
        }

        private void TerEditThemeName_Click(object sender, EventArgs e)
        {
            if (TerThemes.SelectedIndex <= 3) return;

            int i = TerThemes.SelectedIndex;
            int modelIndex = i - 4;

            if (_Terminal?.Themes == null || modelIndex < 0 || modelIndex >= _Terminal.Themes.Count) return;

            string current = TerThemes.SelectedItem?.ToString() ?? string.Empty;
            string s = InputBox(Program.Localization.Strings.Aspects.Terminals.TypeSchemeName, current);

            if (string.IsNullOrEmpty(s) || s == current || TerThemes.Items.Contains(s)) return;

            _Terminal.Themes[modelIndex].Name = s;
            TerThemes.Items.RemoveAt(i);
            TerThemes.Items.Insert(i, s);
            TerThemes.SelectedIndex = i;
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            if (TerSchemes.SelectedIndex <= 0) return;
            if (_Terminal?.Schemes == null) return;

            int i = TerSchemes.SelectedIndex;
            int modelIndex = i - 1;

            if (modelIndex < 0 || modelIndex >= _Terminal.Schemes.Count) return;

            string current = TerSchemes.SelectedItem?.ToString() ?? string.Empty;
            string s = InputBox(Program.Localization.Strings.Aspects.Terminals.TypeSchemeName, current);

            if (string.IsNullOrEmpty(s) || s == current || TerSchemes.Items.Contains(s)) return;

            _Terminal.Schemes[modelIndex].Name = s;
            TerSchemes.Items.RemoveAt(i);
            TerSchemes.Items.Insert(i, s);
            TerSchemes.SelectedIndex = i;
        }

        private void Button14_Click(object sender, EventArgs e)
        {
            WinTerminal.Types.Profile current = GetCurrentProfile();
            if (current == null) return;

            Forms.TerminalInfo.Profile = current;

            if (Forms.TerminalInfo.OpenDialog(TerProfiles.SelectedIndex == 0) == DialogResult.OK)
            {
                current.Name = Forms.TerminalInfo.Profile.Name;
                current.TabTitle = Forms.TerminalInfo.Profile.TabTitle;
                current.Icon = Forms.TerminalInfo.Profile.Icon;
                current.TabColor = Forms.TerminalInfo.Profile.TabColor;

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

            WinTerminal.Types.Profile temp = GetCurrentProfile();
            if (temp != null) temp.UseAcrylic = TerAcrylic.Checked;

            ApplyPreview(_Terminal);
        }

        private void Button13_Click(object sender, EventArgs e)
        {
            if (_Terminal?.Profiles?.List == null) return;

            _Terminal.Profiles.List.Add(new()
            {
                Name = $"{Program.Localization.Strings.General.NewProfile} #{TerProfiles.Items.Count}",
                ColorScheme = _Terminal.Profiles.Defaults?.ColorScheme
            });

            FillTerminalProfiles(_Terminal, TerProfiles);
            TerProfiles.SelectedIndex = TerProfiles.Items.Count - 1;
        }

        private void Button15_Click(object sender, EventArgs e)
        {
            string TerDir = ResolveTerminalPath(WinTerminal.Version.Stable);
            string TerPreDir = ResolveTerminalPath(WinTerminal.Version.Preview);

            switch (Mode)
            {
                case WinTerminal.Version.Stable:
                    {
                        if (File.Exists(TerDir))
                            Program.SendCommand(@$"{SysPaths.Explorer} shell:appsFolder\Microsoft.WindowsTerminal_8wekyb3d8bbwe!App", false);
                        break;
                    }

                case WinTerminal.Version.Preview:
                    {
                        if (File.Exists(TerPreDir))
                            Program.SendCommand(@$"{SysPaths.Explorer} shell:appsFolder\Microsoft.WindowsTerminalPreview_8wekyb3d8bbwe!App", false);
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
            try
            {
                if (TerBackImage.Text == "desktopWallpaper")
                {
                    Terminal1.BackImage = Program.WallpaperMonitor.Get(Program.TM, Program.WindowStyle);
                }
                else if (File.Exists(TerBackImage.Text))
                {
                    Terminal1.BackImage = BitmapMgr.Load(TerBackImage.Text).FillInSize(new(Terminal1.Width - 2, Terminal1.Height - 32));
                }
                else
                {
                    Terminal1.BackImage = null;
                }
            }
            catch
            {
                Terminal1.BackImage = null;
            }

            if (!IsShown) return;

            WinTerminal.Types.Profile temp = GetCurrentProfile();
            if (temp != null) temp.BackgroundImage = TerBackImage.Text;

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
            WinTerminal.Types.Theme theme = GetCurrentTheme();
            if (theme != null)
            {
                theme.Window.ApplicationTheme = !TerMode.Checked ? "light" : "dark";
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
                            if (_TerminalDefault != null) Program.TM.Terminal = _TerminalDefault;
                            break;
                        }

                    case WinTerminal.Version.Preview:
                        {
                            if (_TerminalDefault != null) Program.TM.TerminalPreview = _TerminalDefault;
                            break;
                        }
                }
            }

            if (DialogResult != DialogResult.OK) DialogResult = DialogResult.Cancel;
        }

        private void Button11_Click(object sender, EventArgs e)
        {
            if (!(OS.W12 || OS.W11 || OS.W10)) return;

            string TerDir = ResolveTerminalPath(WinTerminal.Version.Stable);
            string TerPreDir = ResolveTerminalPath(WinTerminal.Version.Preview);

            if (File.Exists(TerDir) & Mode == WinTerminal.Version.Stable)
            {
                Process.Start(TerDir);
            }

            if (File.Exists(TerPreDir) & Mode == WinTerminal.Version.Preview)
            {
                Process.Start(TerPreDir);
            }
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            if (!(OS.W12 || OS.W11 || OS.W10)) return;

            using (SaveFileDialog dlg = new() { Filter = Program.Filters.JSON, Title = Program.Localization.Strings.Extensions.SaveJSON })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    string TerDir = ResolveTerminalPath(WinTerminal.Version.Stable);
                    string TerPreDir = ResolveTerminalPath(WinTerminal.Version.Preview);

                    if (File.Exists(TerDir) & Mode == WinTerminal.Version.Stable)
                    {
                        File.Copy(TerDir, dlg.FileName, true);
                    }

                    if (File.Exists(TerPreDir) & Mode == WinTerminal.Version.Preview)
                    {
                        File.Copy(TerPreDir, dlg.FileName, true);
                    }
                }
            }
        }

        private void Button17_Click(object sender, EventArgs e)
        {
            if (TerSchemes.SelectedIndex <= 0) return;
            if (_Terminal?.Schemes == null) return;

            int idx = TerSchemes.SelectedIndex - 1;
            if (idx < 0 || idx >= _Terminal.Schemes.Count) return;

            WinTerminal.Types.Scheme src = _Terminal.Schemes[idx];

            WinTerminal.Types.Scheme scheme = new()
            {
                Name = $"{TerSchemes.SelectedItem} {Program.Localization.Strings.General.Clone} #{TerSchemes.Items.Count - 1}",
                Background = src.Background,
                Black = src.Black,
                Blue = src.Blue,
                BrightBlack = src.BrightBlack,
                BrightBlue = src.BrightBlue,
                BrightCyan = src.BrightCyan,
                BrightGreen = src.BrightGreen,
                BrightPurple = src.BrightPurple,
                BrightRed = src.BrightRed,
                BrightWhite = src.BrightWhite,
                BrightYellow = src.BrightYellow,
                CursorColor = src.CursorColor,
                Cyan = src.Cyan,
                Foreground = src.Foreground,
                Green = src.Green,
                Purple = src.Purple,
                Red = src.Red,
                SelectionBackground = src.SelectionBackground,
                White = src.White,
                Yellow = src.Yellow
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

            if (_Terminal?.Profiles?.List == null) return;

            int idx = TerProfiles.SelectedIndex - 1;
            if (idx < 0 || idx >= _Terminal.Profiles.List.Count) return;

            WinTerminal.Types.Profile src = _Terminal.Profiles.List[idx];

            WinTerminal.Types.Profile P = new()
            {
                Name = $"{src.Name} {Program.Localization.Strings.General.Clone} #{TerProfiles.Items.Count}",
                BackgroundImage = src.BackgroundImage,
                BackgroundImageOpacity = src.BackgroundImageOpacity,
                ColorScheme = src.ColorScheme,
                Commandline = src.Commandline,
                CursorHeight = src.CursorHeight,
                CursorShape = src.CursorShape,
                Font = src.Font.Clone() as WinTerminal.Types.FontSettings,
                Icon = src.Icon,
                Opacity = src.Opacity,
                TabColor = src.TabColor,
                TabTitle = src.TabTitle,
                UseAcrylic = src.UseAcrylic
            };

            _Terminal.Profiles.List.Add(P);
            FillTerminalProfiles(_Terminal, TerProfiles);
            TerProfiles.SelectedIndex = TerProfiles.Items.Count - 1;
        }

        private void Button19_Click(object sender, EventArgs e)
        {
            if (TerThemes.SelectedIndex <= 3)
            {
                MsgBox(Program.Localization.Strings.Aspects.Terminals.ThemeNotCloneable, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (_Terminal?.Themes == null) return;

            int idx = TerThemes.SelectedIndex - 4;
            if (idx < 0 || idx >= _Terminal.Themes.Count) return;

            WinTerminal.Types.Theme src = _Terminal.Themes[idx];

            WinTerminal.Types.Theme Th = new()
            {
                Name = $"{src.Name} {Program.Localization.Strings.General.Clone} #{TerThemes.Items.Count}",
            };

            Th.Window.ApplicationTheme = src.Window.ApplicationTheme;
            Th.Tab.Background = src.Tab.Background;
            Th.Tab.UnfocusedBackground = src.Tab.UnfocusedBackground;
            Th.TabRow.Background = src.TabRow.Background;
            Th.TabRow.UnfocusedBackground = src.TabRow.UnfocusedBackground;

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
                if (string.IsNullOrWhiteSpace(CCat)) return;

                for (int x = 0, loopTo = TerProfiles.Items.Count - 1; x <= loopTo; x++)
                {
                    if (string.Equals(TerProfiles.Items[x]?.ToString(), CCat, StringComparison.OrdinalIgnoreCase))
                    {
                        WinTerminal.Types.Profile CCatFrom = x == 0 ? _Terminal.Profiles.Defaults : _Terminal.Profiles.List[x - 1];
                        WinTerminal.Types.Profile temp1 = GetCurrentProfile();
                        if (temp1 == null) return;

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
                        TerImageOpacity.Value = Math.Min(TerImageOpacity.Maximum, Math.Max(TerImageOpacity.Minimum, (int)(CCatFrom.BackgroundImageOpacity * 100f)));

                        TerCursorStyle.SelectedIndex = Math.Min(TerCursorStyle.Items.Count - 1, Math.Max(0, (int)CCatFrom.CursorShape));
                        TerCursorHeightBar.Value = Math.Min(TerCursorHeightBar.Maximum, Math.Max(TerCursorHeightBar.Minimum, CCatFrom.CursorHeight));

                        TerFontName.Text = CCatFrom.Font.Face;
                        GDI32.LOGFONT fx = new();
                        Font f_cmd = new(CCatFrom.Font.Face, CCatFrom.Font.Size);
                        f_cmd.ToLogFont(fx);
                        fx.lfWeight = (int)CCatFrom.Font.Weight * 100;
                        using (Font temp_s = Font.FromLogFont(fx))
                        {
                            f_cmd = new(f_cmd.Name, f_cmd.Size, temp_s.Style);
                        }
                        TerFontName.Font = new(f_cmd.Name, 9f, f_cmd.Style);

                        TerFontSizeBar.Value = Math.Min(TerFontSizeBar.Maximum, Math.Max(TerFontSizeBar.Minimum, (int)CCatFrom.Font.Size));
                        TerFontWeight.SelectedIndex = Math.Min(TerFontWeight.Items.Count - 1, Math.Max(0, (int)CCatFrom.Font.Weight));

                        TerAcrylic.Checked = CCatFrom.UseAcrylic;
                        TerOpacityBar.Value = Math.Min(TerOpacityBar.Maximum, Math.Max(TerOpacityBar.Minimum, CCatFrom.Opacity));

                        Terminal1.Opacity = CCatFrom.Opacity;
                        Terminal1.OpacityBackImage = (float)CCatFrom.BackgroundImageOpacity * 100f;
                        Terminal1.TabIcon?.Dispose();

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
                            Terminal1.TabIconButItIsString = null;
                        }
                        else
                        {
                            IntPtr intPtr = IntPtr.Zero;
                            Kernel32.Wow64DisableWow64FsRedirection(ref intPtr);
                            string path = string.Empty;
                            if (CCatFrom.Commandline is not null)
                                path = CCatFrom.Commandline.Replace("%SystemRoot%", SysPaths.Windows);
                            Kernel32.Wow64RevertWow64FsRedirection(intPtr);

                            if (File.Exists(path))
                            {
                                using (Icon ico = NativeMethods.Helpers.ExtractSmallIcon(path))
                                {
                                    Terminal1.TabIcon = ico.ToBitmap();
                                    Terminal1.TabIconButItIsString = null;
                                }
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
            if (TerSchemes.SelectedIndex <= 0) return;
            if (_Terminal?.Schemes == null) return;

            UI.WP.ComboBox temp = Forms.WindowsTerminalCopycat.ComboBox1;
            temp.Items.Clear();
            CCat = null;

            foreach (object x in TerSchemes.Items)
            {
                if (x != TerSchemes.SelectedItem && x != TerSchemes.Items[0]) temp.Items.Add(x);
            }

            if (Forms.WindowsTerminalCopycat.ShowDialog() == DialogResult.OK)
            {
                if (string.IsNullOrWhiteSpace(CCat)) return;

                WinTerminal.Types.Scheme target = GetCurrentScheme();
                if (target == null) return;

                for (int x = 1, loopTo = TerSchemes.Items.Count - 1; x <= loopTo; x++)
                {
                    if (string.Equals(TerSchemes.Items[x]?.ToString(), CCat, StringComparison.OrdinalIgnoreCase))
                    {
                        WinTerminal.Types.Scheme CCatFrom = _Terminal.Schemes[x - 1];

                        target.Background = CCatFrom.Background;
                        target.Black = CCatFrom.Black;
                        target.Blue = CCatFrom.Blue;
                        target.BrightBlack = CCatFrom.BrightBlack;
                        target.BrightBlue = CCatFrom.BrightBlue;
                        target.BrightCyan = CCatFrom.BrightCyan;
                        target.BrightGreen = CCatFrom.BrightGreen;
                        target.BrightPurple = CCatFrom.BrightPurple;
                        target.BrightRed = CCatFrom.BrightRed;
                        target.BrightWhite = CCatFrom.BrightWhite;
                        target.BrightYellow = CCatFrom.BrightYellow;
                        target.CursorColor = CCatFrom.CursorColor;
                        target.Cyan = CCatFrom.Cyan;
                        target.Foreground = CCatFrom.Foreground;
                        target.Green = CCatFrom.Green;
                        target.Purple = CCatFrom.Purple;
                        target.Red = CCatFrom.Red;
                        target.SelectionBackground = CCatFrom.SelectionBackground;
                        target.White = CCatFrom.White;
                        target.Yellow = CCatFrom.Yellow;

                        TerBackground.BackColor = target.Background;
                        TerForeground.BackColor = target.Foreground;
                        TerSelection.BackColor = target.SelectionBackground;
                        TerCursor.BackColor = target.CursorColor;

                        TerBlack.BackColor = target.Black;
                        TerBlue.BackColor = target.Blue;
                        TerGreen.BackColor = target.Green;
                        TerCyan.BackColor = target.Cyan;
                        TerRed.BackColor = target.Red;
                        TerPurple.BackColor = target.Purple;
                        TerYellow.BackColor = target.Yellow;
                        TerWhite.BackColor = target.White;

                        TerBlackB.BackColor = target.BrightBlack;
                        TerBlueB.BackColor = target.BrightBlue;
                        TerGreenB.BackColor = target.BrightGreen;
                        TerCyanB.BackColor = target.BrightCyan;
                        TerRedB.BackColor = target.BrightRed;
                        TerPurpleB.BackColor = target.BrightPurple;
                        TerYellowB.BackColor = target.BrightYellow;
                        TerWhiteB.BackColor = target.BrightWhite;

                        ApplyPreview(_Terminal);

                        break;
                    }
                }
            }
        }

        private void Button21_Click(object sender, EventArgs e)
        {
            if (TerThemes.SelectedIndex <= 3)
            {
                MsgBox(Program.Localization.Strings.Aspects.Terminals.ThemeNotCloneable, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (_Terminal?.Themes == null) return;

            UI.WP.ComboBox temp = Forms.WindowsTerminalCopycat.ComboBox1;
            temp.Items.Clear();
            CCat = null;

            foreach (object x in TerThemes.Items) temp.Items.Add(x);

            if (Forms.WindowsTerminalCopycat.ShowDialog() == DialogResult.OK)
            {
                if (string.IsNullOrWhiteSpace(CCat)) return;

                WinTerminal.Types.Theme target = GetCurrentTheme();
                if (target == null) return;

                // Start at 4: the first 4 combo entries are built-ins.
                for (int x = 4, loopTo = TerThemes.Items.Count - 1; x <= loopTo; x++)
                {
                    if (string.Equals(TerThemes.Items[x]?.ToString(), CCat, StringComparison.OrdinalIgnoreCase))
                    {
                        int modelIndex = x - 4;
                        if (modelIndex < 0 || modelIndex >= _Terminal.Themes.Count) break;

                        WinTerminal.Types.Theme CCatFrom = _Terminal.Themes[modelIndex];

                        target.Window.ApplicationTheme = CCatFrom.Window.ApplicationTheme;
                        target.Tab.Background = CCatFrom.Tab.Background;
                        target.Tab.UnfocusedBackground = CCatFrom.Tab.UnfocusedBackground;
                        target.TabRow.Background = CCatFrom.TabRow.Background;
                        target.TabRow.UnfocusedBackground = CCatFrom.TabRow.UnfocusedBackground;

                        TerTitlebarActive.BackColor = CCatFrom.TabRow.Background;
                        TerTitlebarInactive.BackColor = CCatFrom.TabRow.UnfocusedBackground;
                        TerTabActive.BackColor = CCatFrom.Tab.Background;
                        TerTabInactive.BackColor = CCatFrom.Tab.UnfocusedBackground;
                        TerMode.Checked = !(CCatFrom.Window.ApplicationTheme?.ToLower() == "light");

                        break;
                    }
                }

                ApplyPreview(_Terminal);
            }
        }

        private void Button23_Click(object sender, EventArgs e)
        {
            using (FontDialog dlg = new() { Font = Terminal1.Font, FixedPitchOnly = !Program.Settings.WindowsTerminals.ListAllFonts })
            using (UI.Dark.DarkWin32 dark = new())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    TerFontName.Text = dlg.Font.Name;
                    GDI32.LOGFONT fx = new();
                    dlg.Font.ToLogFont(fx);
                    fx.lfWeight = Math.Max(100, TerFontWeight.SelectedIndex * 100);
                    {
                        using (Font temp = Font.FromLogFont(fx))
                        {
                            Terminal1.Font = new(dlg.Font.Name, dlg.Font.Size, temp.Style);
                        }
                    }
                    TerFontName.Font = new(dlg.Font.Name, 9f, Terminal1.Font.Style);
                    TerFontSizeBar.Value = Math.Min(TerFontSizeBar.Maximum, Math.Max(TerFontSizeBar.Minimum, dlg.Font.Size));

                    WinTerminal.Types.Profile current = GetCurrentProfile();
                    if (current?.Font != null)
                    {
                        current.Font.Face = dlg.Font.Name;
                        current.Font.Weight = (WinTerminal.Types.FontWeight)TerFontWeight.SelectedIndex;
                        current.Font.Size = dlg.Font.Size;
                    }
                }
            }
        }

        private void TerCursorHeightBar_ValueChanged(object sender, EventArgs e)
        {
            Terminal1.CursorHeight = (int)(sender as TrackBarX).Value;

            if (!IsShown) return;

            WinTerminal.Types.Profile temp = GetCurrentProfile();
            if (temp != null) temp.CursorHeight = (int)(sender as TrackBarX).Value;
        }

        private void trackBarX1_ValueChanged(object sender, EventArgs e)
        {
            Terminal1.OpacityBackImage = TerImageOpacity.Value;

            if (!IsShown) return;

            WinTerminal.Types.Profile temp = GetCurrentProfile();
            if (temp != null) temp.BackgroundImageOpacity = (float)(TerImageOpacity.Value / 100d);
        }

        private void trackBarX1_ValueChanged_1(object sender, EventArgs e)
        {
            Terminal1.Opacity = TerOpacityBar.Value;

            if (IsShown)
            {
                WinTerminal.Types.Profile temp = GetCurrentProfile();
                if (temp != null) temp.Opacity = (int)TerOpacityBar.Value;
            }
        }

        private void trackBarX1_ValueChanged_2(object sender, EventArgs e)
        {
            if (!IsShown) return;

            Terminal1.Font = new(Terminal1.Font.Name, TerFontSizeBar.Value, Terminal1.Font.Style);

            WinTerminal.Types.Profile temp = GetCurrentProfile();
            if (temp?.Font != null) temp.Font.Size = TerFontSizeBar.Value;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (TerThemes.SelectedIndex <= 3) return;
            if (_Terminal?.Themes == null) return;

            if (MsgBox(string.Format(Program.Localization.Strings.Messages.TerminalDeleteTheme, TerThemes.SelectedItem), MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            int i = TerThemes.SelectedIndex;
            int modelIndex = i - 4;

            if (modelIndex < 0 || modelIndex >= _Terminal.Themes.Count) return;

            _Terminal.Themes.RemoveAt(modelIndex);
            TerThemes.Items.RemoveAt(i);
            TerThemes.SelectedIndex = i > TerThemes.Items.Count - 1 ? TerThemes.Items.Count - 1 : i;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (TerSchemes.SelectedIndex <= 0) return;
            if (_Terminal?.Schemes == null) return;

            if (MsgBox(string.Format(Program.Localization.Strings.Messages.TerminalDeleteScheme, TerSchemes.SelectedItem), MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            int i = TerSchemes.SelectedIndex;
            int modelIndex = i - 1;

            if (modelIndex < 0 || modelIndex >= _Terminal.Schemes.Count) return;

            _Terminal.Schemes.RemoveAt(modelIndex);
            TerSchemes.Items.RemoveAt(i);
            TerSchemes.SelectedIndex = i > TerSchemes.Items.Count - 1 ? TerSchemes.Items.Count - 1 : i;
        }

        private void TerWhiteB_ContextMenuMadeColorChangeInvoker(object sender, ColorItem.ContextMenuMadeColorChangeEventArgs e)
        {
            Color cx = e.Color;

            WinTerminal.Types.Scheme scheme = GetCurrentScheme();
            WinTerminal.Types.Theme theme = GetCurrentTheme();

            if (scheme == null) return;

            if (ColorClipboard.Event != ColorClipboard.MenuEvent.None)
            {
                string name = ((ColorItem)sender).Name.ToString().ToLower();

                if (name.Contains(TerBackground.Name.ToLower())) scheme.Background = cx;
                if (name.Contains(TerForeground.Name.ToLower())) scheme.Foreground = cx;
                if (name.Contains(TerSelection.Name.ToLower())) scheme.SelectionBackground = cx;
                if (name.Contains(TerCursor.Name.ToLower())) scheme.CursorColor = cx;

                if (theme != null)
                {
                    if (name.Contains(TerTabActive.Name.ToLower())) theme.Tab.Background = cx;
                    if (name.Contains(TerTabInactive.Name.ToLower())) theme.Tab.UnfocusedBackground = cx;
                    if (name.Contains(TerTitlebarActive.Name.ToLower())) theme.TabRow.Background = cx;
                    if (name.Contains(TerTitlebarInactive.Name.ToLower())) theme.TabRow.UnfocusedBackground = cx;
                }

                ApplyPreview(_Terminal);
            }
        }
    }
}