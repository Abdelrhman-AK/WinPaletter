using Microsoft.Win32;
using Serilog.Events;
using System;
using System.Drawing;
using System.Security.Principal;
using System.Windows.Forms;
using WinPaletter.NativeMethods;

namespace WinPaletter.Theme.Structures
{
    public class Windows10x
    {
        /// <summary>
        /// Enumeration for levels of accent applying
        /// </summary>
        public enum AccentTaskbarLevels
        {
            None,
            Taskbar_Start_AC,
            Taskbar
        }
    }

    /// <summary>
    /// Base class for Windows 10/11/12 colors and appearance management
    /// </summary>
    public abstract class Windows10xBase<T> : ManagerBase<T> where T : Windows10xBase<T>
    {
        /// <summary> Controls if Windows colors editing is enabled or not </summary> 
        public bool Enabled { get; set; } = true;

        /// <summary>Color index 0 in registry colorPrevelance array 'AccentPalette'</summary>
        public Color Color_Index0 { get; set; }

        /// <summary>Color index 1 in registry colorPrevelance array 'AccentPalette'</summary>
        public Color Color_Index1 { get; set; }

        /// <summary>Color index 2 in registry colorPrevelance array 'AccentPalette'</summary>
        public Color Color_Index2 { get; set; }

        /// <summary>Color index 3 in registry colorPrevelance array 'AccentPalette'</summary>
        public Color Color_Index3 { get; set; }

        /// <summary>Color index 4 in registry colorPrevelance array 'AccentPalette'</summary>
        public Color Color_Index4 { get; set; }

        /// <summary>Color index 5 in registry colorPrevelance array 'AccentPalette'</summary>
        public Color Color_Index5 { get; set; }

        /// <summary>Color index 6 in registry colorPrevelance array 'AccentPalette'</summary>
        public Color Color_Index6 { get; set; }

        /// <summary>Color index 7 in registry colorPrevelance array 'AccentPalette'</summary>
        public Color Color_Index7 { get; set; }

        /// <summary>Light mode for Windows</summary>
        public bool WinMode_Light { get; set; } = true;

        /// <summary>Light mode for applications</summary>
        public bool AppMode_Light { get; set; } = true;

        /// <summary>Transparency effects</summary>
        public bool Transparency { get; set; } = true;

        /// <summary>Active titlebar color</summary>
        public Color Titlebar_Active { get; set; }

        /// <summary>Inactive titlebar color</summary>
        public Color Titlebar_Inactive { get; set; }

        /// <summary>Start menu accent color</summary>
        public Color StartMenu_Accent { get; set; }

        /// <summary>Make accent can be applied on titlebars</summary>
        public bool ApplyAccentOnTitlebars { get; set; } = false;

        /// <summary>Choices to apply accents on taskbar</summary>
        public Windows10x.AccentTaskbarLevels ApplyAccentOnTaskbar { get; set; } = Windows10x.AccentTaskbarLevels.None;

        /// <summary>Visual styles configuration</summary>
        public VisualStyles VisualStyles { get; set; } = new();

        protected string _signature;

        protected Windows10xBase(string signature)
        {
            _signature = signature;
            SetDefaultColors();
        }

        /// <summary>
        /// Creates a copy of the current object
        /// </summary>
        public new T Clone()
        {
            var clone = (T)Activator.CreateInstance(typeof(T));
            CopyCommonPropertiesTo(clone);
            return clone;
        }

        /// <summary>
        /// Converts this instance to another Windows version
        /// </summary>
        /// <typeparam name="TTarget">Target Windows version type</typeparam>
        public TTarget ConvertTo<TTarget>() where TTarget : Windows10xBase<TTarget>, new()
        {
            var target = new TTarget();
            CopyCommonPropertiesTo(target);

            // Handle version-specific conversions
            HandleConversionTo(target);

            return target;
        }

        /// <summary>
        /// Copies common properties from this instance to a target instance
        /// </summary>
        protected void CopyCommonPropertiesTo<TTarget>(TTarget target) where TTarget : Windows10xBase<TTarget>
        {
            // Basic properties
            target.Enabled = this.Enabled;
            target.VisualStyles = this.VisualStyles?.Clone(); // Ensure VisualStyles has Clone method

            // Accent colors
            target.Color_Index0 = this.Color_Index0;
            target.Color_Index1 = this.Color_Index1;
            target.Color_Index2 = this.Color_Index2;
            target.Color_Index3 = this.Color_Index3;
            target.Color_Index4 = this.Color_Index4;
            target.Color_Index5 = this.Color_Index5;
            target.Color_Index6 = this.Color_Index6;
            target.Color_Index7 = this.Color_Index7;

            // Theme settings
            target.WinMode_Light = this.WinMode_Light;
            target.AppMode_Light = this.AppMode_Light;
            target.Transparency = this.Transparency;

            // Titlebar colors
            target.Titlebar_Active = this.Titlebar_Active;
            target.Titlebar_Inactive = this.Titlebar_Inactive;

            // Start menu accent
            target.StartMenu_Accent = this.StartMenu_Accent;

            // Accent application settings
            target.ApplyAccentOnTitlebars = this.ApplyAccentOnTitlebars;
            target.ApplyAccentOnTaskbar = this.ApplyAccentOnTaskbar;

            // Copy signature
            target._signature = this._signature;
        }

        /// <summary>
        /// Handles version-specific conversions to the target type
        /// </summary>
        protected virtual void HandleConversionTo<TTarget>(TTarget target) where TTarget : Windows10xBase<TTarget>
        {
            // Base implementation does nothing
            // Override in derived classes if needed
        }

        /// <summary>
        /// Sets the default colors for the specific Windows version
        /// </summary>
        protected abstract void SetDefaultColors();

        /// <summary>
        /// Load version-specific data from registry
        /// </summary>
        protected abstract void LoadSpecific(Windows10xBase<T> @default);

        /// <summary>
        /// Apply version-specific settings to registry
        /// </summary>
        protected abstract void ApplySpecific(TreeView treeView = null);

        /// <summary>
        /// Loads data from registry
        /// </summary>
        public void Load(Windows10xBase<T> @default)
        {
            Program.Log?.Write(LogEventLevel.Information, $"Loading Windows {_signature} colors and appearance preferences from registry.");

            Enabled = ReadReg($@"HKEY_CURRENT_USER\Software\WinPaletter\Aspects\WindowsColorsThemes\Windows10x\{_signature}", string.Empty, @default.Enabled);

            VisualStyles.Load(_signature, @default.VisualStyles);

            // Load accent palette
            byte[] defColorsBytes = GetDefaultColorBytes(@default);
            byte[] x = ReadReg(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "AccentPalette", defColorsBytes);

            Color_Index0 = Color.FromArgb(255, x[0], x[1], x[2]);
            Color_Index1 = Color.FromArgb(255, x[4], x[5], x[6]);
            Color_Index2 = Color.FromArgb(255, x[8], x[9], x[10]);
            Color_Index3 = Color.FromArgb(255, x[12], x[13], x[14]);
            Color_Index4 = Color.FromArgb(255, x[16], x[17], x[18]);
            Color_Index5 = Color.FromArgb(255, x[20], x[21], x[22]);
            Color_Index6 = Color.FromArgb(255, x[24], x[25], x[26]);
            Color_Index7 = Color.FromArgb(255, x[28], x[29], x[30]);

            // Load common registry values
            StartMenu_Accent = ReadReg(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "StartColorMenu", @default.StartMenu_Accent.Reverse()).Reverse();

            Color dwmAccent = ReadReg(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\DWM", "AccentColor", @default.Titlebar_Active.Reverse()).Reverse();
            Titlebar_Active = dwmAccent != default ? dwmAccent : ReadReg(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "AccentColorMenu", @default.Titlebar_Active.Reverse()).Reverse();

            Titlebar_Inactive = ReadReg(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\DWM", "AccentColorInactive", @default.Titlebar_Inactive.Reverse()).Reverse();

            WinMode_Light = ReadReg(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "SystemUsesLightTheme", @default.WinMode_Light);
            AppMode_Light = ReadReg(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "AppsUseLightTheme", @default.AppMode_Light);
            Transparency = ReadReg(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "EnableTransparency", @default.Transparency);

            ApplyAccentOnTaskbar = (Windows10x.AccentTaskbarLevels)ReadReg(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "ColorPrevalence", (int)@default.ApplyAccentOnTaskbar);
            ApplyAccentOnTitlebars = ReadReg(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\DWM", "ColorPrevalence", @default.ApplyAccentOnTitlebars);

            // Load version-specific settings
            LoadSpecific(@default);
        }

        /// <summary>
        /// Saves data into registry
        /// </summary>
        /// <summary>
        /// Saves data into registry
        /// </summary>
        public void Apply(TreeView treeView = null)
        {
            Program.Log?.Write(LogEventLevel.Information, $"Saving Windows {_signature} colors and appearance preferences into registry.");

            using (WindowsImpersonationContext wic = User.Identity.Impersonate())
            {
                SaveToggleState(treeView);

                if (Enabled && IsTargetOS())
                {
                    VisualStyles.Apply(_signature, treeView);

                    // Disable auto-colorization from wallpaper and clear its history flag
                    WriteReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Desktop", "AutoColorization", 0);
                    WriteReg(treeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Themes\History", "AutoColor", 0);

                    int colorPrevelance = ApplyAccentOnTaskbar switch
                    {
                        Windows10x.AccentTaskbarLevels.None => 0,
                        Windows10x.AccentTaskbarLevels.Taskbar_Start_AC => 1,
                        Windows10x.AccentTaskbarLevels.Taskbar => 2,
                        _ => 0
                    };

                    #region HKEY_CURRENT_USER

                    WriteReg(treeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "ColorPrevalence", colorPrevelance);
                    WriteReg(treeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "SystemUsesLightTheme", WinMode_Light ? 1 : 0);
                    WriteReg(treeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "AppsUseLightTheme", AppMode_Light ? 1 : 0);
                    WriteReg(treeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "EnableTransparency", Transparency ? 1 : 0);

                    WriteReg(treeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\DWM", "ColorPrevalence", ApplyAccentOnTitlebars ? 1 : 0);
                    WriteReg(treeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\DWM", "AccentColor", Titlebar_Active.Reverse().ToArgb());
                    WriteReg(treeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\DWM", "AccentColorInactive", Titlebar_Inactive.Reverse().ToArgb());

                    WriteReg(treeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "AccentPalette", ColorBytes, RegistryValueKind.Binary);
                    WriteReg(treeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "StartColorMenu", StartMenu_Accent.Reverse().ToArgb());
                    WriteReg(treeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "AccentColorMenu", Titlebar_Active.Reverse().ToArgb());

                    #endregion

                    #region HKEY_LOCAL_MACHINE SystemProtectedUserData

                    string spdColorsPath = $@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\SystemProtectedUserData\{User.SID}\AnyoneRead\Colors";
                    string spdThemePath = $@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\SystemProtectedUserData\{User.SID}\AnyoneRead\Theme";

                    WriteReg(treeView, spdColorsPath, "AccentColor", Titlebar_Active.Reverse().ToArgb());
                    WriteReg(treeView, spdColorsPath, "AccentColorInactive", Titlebar_Inactive.Reverse().ToArgb());
                    WriteReg(treeView, spdColorsPath, "StartColor", StartMenu_Accent.Reverse().ToArgb());
                    WriteReg(treeView, spdColorsPath, "ImmersiveColor", Titlebar_Active.Reverse().ToArgb());

                    // Theme\Personalize mirrors under SystemProtectedUserData for UWP and logon screen reads
                    WriteReg(treeView, spdThemePath, "SystemUsesLightTheme", WinMode_Light ? 1 : 0);
                    WriteReg(treeView, spdThemePath, "AppsUseLightTheme", AppMode_Light ? 1 : 0);
                    WriteReg(treeView, spdThemePath, "ColorPrevalence", colorPrevelance);
                    WriteReg(treeView, spdThemePath, "EnableTransparency", Transparency ? 1 : 0);

                    #endregion

                    #region HKEY_USERS\.DEFAULT

                    if (Program.Settings.ThemeApplyingBehavior.WindowsColors_HKU_DEFAULT_Prefs == Settings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite)
                    {
                        // Mirror everything written to HKCU into HKU\.DEFAULT so DWM and the shell
                        // load the correct values before the user hive is mounted on logon.
                        WriteReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop", "AutoColorization", 0);
                        WriteReg(treeView, @"HKEY_USERS\.DEFAULT\Software\Microsoft\Windows\CurrentVersion\Themes\History", "AutoColor", 0);

                        WriteReg(treeView, @"HKEY_USERS\.DEFAULT\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "ColorPrevalence", colorPrevelance);
                        WriteReg(treeView, @"HKEY_USERS\.DEFAULT\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "SystemUsesLightTheme", WinMode_Light ? 1 : 0);
                        WriteReg(treeView, @"HKEY_USERS\.DEFAULT\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "AppsUseLightTheme", AppMode_Light ? 1 : 0);
                        WriteReg(treeView, @"HKEY_USERS\.DEFAULT\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "EnableTransparency", Transparency ? 1 : 0);

                        WriteReg(treeView, @"HKEY_USERS\.DEFAULT\SOFTWARE\Microsoft\Windows\DWM", "ColorPrevalence", ApplyAccentOnTitlebars ? 1 : 0);
                        WriteReg(treeView, @"HKEY_USERS\.DEFAULT\SOFTWARE\Microsoft\Windows\DWM", "AccentColor", Titlebar_Active.Reverse().ToArgb());
                        WriteReg(treeView, @"HKEY_USERS\.DEFAULT\SOFTWARE\Microsoft\Windows\DWM", "AccentColorInactive", Titlebar_Inactive.Reverse().ToArgb());

                        WriteReg(treeView, @"HKEY_USERS\.DEFAULT\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "AccentPalette", ColorBytes, RegistryValueKind.Binary);
                        WriteReg(treeView, @"HKEY_USERS\.DEFAULT\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "StartColorMenu", StartMenu_Accent.Reverse().ToArgb());
                        WriteReg(treeView, @"HKEY_USERS\.DEFAULT\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "AccentColorMenu", Titlebar_Active.Reverse().ToArgb());
                    }

                    #endregion

                    // Apply version-specific settings
                    ApplySpecific(treeView);

                    // Patch the active .theme file so Windows replays our values on every logon instead of resetting them
                    PatchActiveThemeFile(Program.TM.Win32);

                    // Broadcast changes
                    BroadcastChanges();
                }

                wic?.Undo();
            }
        }

        /// <summary>
        /// Saves toggle state into registry
        /// </summary>
        public void SaveToggleState(TreeView treeView = null)
        {
            WriteReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\Aspects\WindowsColorsThemes\Windows10x\{_signature}", string.Empty, Enabled);
        }

        protected virtual bool IsTargetOS()
        {
            return _signature switch
            {
                "10" => OS.W10,
                "11" => OS.W11,
                "12" => OS.W12,
                _ => false
            };
        }

        private byte[] GetDefaultColorBytes(Windows10xBase<T> @default)
        {
            return
            [
                @default.Color_Index0.R, @default.Color_Index0.G, @default.Color_Index0.B, 255,
                @default.Color_Index1.R, @default.Color_Index1.G, @default.Color_Index1.B, 255,
                @default.Color_Index2.R, @default.Color_Index2.G, @default.Color_Index2.B, 255,
                @default.Color_Index3.R, @default.Color_Index3.G, @default.Color_Index3.B, 255,
                @default.Color_Index4.R, @default.Color_Index4.G, @default.Color_Index4.B, 255,
                @default.Color_Index5.R, @default.Color_Index5.G, @default.Color_Index5.B, 255,
                @default.Color_Index6.R, @default.Color_Index6.G, @default.Color_Index6.B, 255,
                @default.Color_Index7.R, @default.Color_Index7.G, @default.Color_Index7.B, 255
            ];
        }

        private byte[] ColorBytes =>
            [
                Color_Index0.R, Color_Index0.G, Color_Index0.B, 255,
                Color_Index1.R, Color_Index1.G, Color_Index1.B, 255,
                Color_Index2.R, Color_Index2.G, Color_Index2.B, 255,
                Color_Index3.R, Color_Index3.G, Color_Index3.B, 255,
                Color_Index4.R, Color_Index4.G, Color_Index4.B, 255,
                Color_Index5.R, Color_Index5.G, Color_Index5.B, 255,
                Color_Index6.R, Color_Index6.G, Color_Index6.B, 255,
                Color_Index7.R, Color_Index7.G, Color_Index7.B, 255
            ];

        /// <summary>
        /// Patches the active Windows .theme file with the colors WinPaletter just wrote to the registry,
        /// so Windows replays the correct values on every logon instead of resetting them.
        /// Only keys officially read by Windows from .theme files are written.
        /// </summary>
        private void PatchActiveThemeFile(Win32UI win32UI)
        {
            try
            {
                string themeFilePath = ReadReg(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Themes", "CurrentTheme", string.Empty);

                if (string.IsNullOrEmpty(themeFilePath) || !System.IO.File.Exists(themeFilePath))
                {
                    Program.Log?.Write(LogEventLevel.Warning, "PatchActiveThemeFile: CurrentTheme registry value missing or file not found. Skipping .theme patch.");
                    return;
                }

                Program.Log?.Write(LogEventLevel.Information, $"PatchActiveThemeFile: Patching theme file at '{themeFilePath}'.");

                string userThemesFolder = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Microsoft", "Windows", "Themes");
                string tempPath = System.IO.Path.Combine(userThemesFolder, System.IO.Path.GetRandomFileName() + ".theme");

                System.IO.Directory.CreateDirectory(userThemesFolder);
                System.IO.File.Copy(themeFilePath, tempPath, overwrite: true);

                Program.Log?.Write(LogEventLevel.Information, $"PatchActiveThemeFile: Working on temp copy at '{tempPath}'.");

                uint colorizationColor = 0xC4000000u | ((uint)Titlebar_Active.R << 16) | ((uint)Titlebar_Active.G << 8) | Titlebar_Active.B;

                using (INI ini = new(tempPath))
                {
                    ini.Write("Control Panel\\Colors", "ActiveTitle", win32UI.ActiveTitle.ToStringWin32());
                    ini.Write("Control Panel\\Colors", "InactiveTitle", win32UI.InactiveTitle.ToStringWin32());
                    ini.Write("Control Panel\\Colors", "Background", win32UI.Background.ToStringWin32());
                    ini.Write("Control Panel\\Colors", "ActiveBorder", win32UI.ActiveBorder.ToStringWin32());
                    ini.Write("Control Panel\\Colors", "InactiveBorder", win32UI.InactiveBorder.ToStringWin32());
                    ini.Write("Control Panel\\Colors", "Menu", win32UI.Menu.ToStringWin32());
                    ini.Write("Control Panel\\Colors", "Window", win32UI.Window.ToStringWin32());
                    ini.Write("Control Panel\\Colors", "WindowFrame", win32UI.WindowFrame.ToStringWin32());
                    ini.Write("Control Panel\\Colors", "MenuText", win32UI.MenuText.ToStringWin32());
                    ini.Write("Control Panel\\Colors", "WindowText", win32UI.WindowText.ToStringWin32());
                    ini.Write("Control Panel\\Colors", "TitleText", win32UI.TitleText.ToStringWin32());
                    ini.Write("Control Panel\\Colors", "Scrollbar", win32UI.Scrollbar.ToStringWin32());
                    ini.Write("Control Panel\\Colors", "ButtonFace", win32UI.ButtonFace.ToStringWin32());
                    ini.Write("Control Panel\\Colors", "ButtonShadow", win32UI.ButtonShadow.ToStringWin32());
                    ini.Write("Control Panel\\Colors", "ButtonHilight", win32UI.ButtonHilight.ToStringWin32());
                    ini.Write("Control Panel\\Colors", "ButtonDkShadow", win32UI.ButtonDkShadow.ToStringWin32());
                    ini.Write("Control Panel\\Colors", "ButtonLight", win32UI.ButtonLight.ToStringWin32());
                    ini.Write("Control Panel\\Colors", "ButtonText", win32UI.ButtonText.ToStringWin32());
                    ini.Write("Control Panel\\Colors", "GrayText", win32UI.GrayText.ToStringWin32());
                    ini.Write("Control Panel\\Colors", "Hilight", win32UI.Hilight.ToStringWin32());
                    ini.Write("Control Panel\\Colors", "HilightText", win32UI.HilightText.ToStringWin32());
                    ini.Write("Control Panel\\Colors", "InactiveTitleText", win32UI.InactiveTitleText.ToStringWin32());
                    ini.Write("Control Panel\\Colors", "InfoText", win32UI.InfoText.ToStringWin32());
                    ini.Write("Control Panel\\Colors", "InfoWindow", win32UI.InfoWindow.ToStringWin32());
                    ini.Write("Control Panel\\Colors", "AppWorkspace", win32UI.AppWorkspace.ToStringWin32());
                    ini.Write("Control Panel\\Colors", "GradientActiveTitle", win32UI.GradientActiveTitle.ToStringWin32());
                    ini.Write("Control Panel\\Colors", "GradientInactiveTitle", win32UI.GradientInactiveTitle.ToStringWin32());
                    ini.Write("Control Panel\\Colors", "HotTrackingColor", win32UI.HotTrackingColor.ToStringWin32());
                    ini.Write("Control Panel\\Colors", "MenuHilight", win32UI.MenuHilight.ToStringWin32());
                    ini.Write("Control Panel\\Colors", "MenuBar", win32UI.MenuBar.ToStringWin32());
                    ini.Write("Control Panel\\Colors", "Desktop", win32UI.Desktop.ToStringWin32());

                    ini.Write("Personalize", "SystemUsesLightTheme", WinMode_Light ? "1" : "0");
                    ini.Write("Personalize", "AppsUseLightTheme", AppMode_Light ? "1" : "0");
                    ini.Write("Personalize", "EnableTransparency", Transparency ? "1" : "0");
                    ini.Write("Personalize", "ColorPrevalence", ((int)ApplyAccentOnTaskbar).ToString());

                    ini.Write("VisualStyles", "ColorizationColor", $"0X{colorizationColor:X8}");
                    ini.Write("VisualStyles", "AutoColorization", "0");
                    ini.Write("VisualStyles", "SystemMode", WinMode_Light ? "Light" : "Dark");
                    ini.Write("VisualStyles", "AppMode", AppMode_Light ? "Light" : "Dark");
                }

                // Take ownership of the original before overwriting — handles system-owned
                // and WindowsApps-owned theme files without needing a separate redirect path.
                TakeOwnership(themeFilePath, AsAdministrator: true);

                Move_File(tempPath, themeFilePath);

                Program.Log?.Write(LogEventLevel.Information, "PatchActiveThemeFile: Theme file patched successfully.");
            }
            catch (Exception ex)
            {
                Program.Log?.Write(LogEventLevel.Error, $"PatchActiveThemeFile: Failed to patch theme file. Exception: {ex.Message}");
            }
        }

        private void BroadcastChanges()
        {
            using (WindowsImpersonationContext wic = User.Identity.Impersonate())
            {
                Program.Log?.Write(LogEventLevel.Information, "Broadcasting system message to notify about the setting change (User32.UpdatePerUserSystemParameters(1, true)).");
                User32.UpdatePerUserSystemParameters(1, true);

                Program.Log?.Write(LogEventLevel.Information, "Broadcasting system message to notify about the setting change (User32.SendMessage).");
                User32.SendMessage(IntPtr.Zero, User32.WindowsMessages.WM_SETTINGCHANGE, IntPtr.Zero, IntPtr.Zero);
                User32.NotifySettingChanged("ImmersiveColorSet");
                User32.NotifySettingChanged("WindowsThemeElement");

                wic.Undo();
            }
        }
    }
}