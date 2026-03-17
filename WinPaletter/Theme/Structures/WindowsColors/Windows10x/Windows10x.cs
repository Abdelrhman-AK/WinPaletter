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

            Titlebar_Active = ReadReg(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "AccentColorMenu", @default.Titlebar_Active.Reverse()).Reverse();
            Titlebar_Active = ReadReg(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\DWM", "AccentColor", @default.Titlebar_Active.Reverse()).Reverse();
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
        public void Apply(TreeView treeView = null)
        {
            Program.Log?.Write(LogEventLevel.Information, $"Saving Windows {_signature} colors and appearance preferences into registry.");

            SaveToggleState(treeView);

            if (Enabled && IsTargetOS())
            {
                VisualStyles.Apply(_signature, treeView);

                WriteReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Desktop", "AutoColorization", 0);

                int colorPrevelance = ApplyAccentOnTaskbar switch
                {
                    Windows10x.AccentTaskbarLevels.None => 0,
                    Windows10x.AccentTaskbarLevels.Taskbar_Start_AC => 1,
                    Windows10x.AccentTaskbarLevels.Taskbar => 2,
                    _ => 0
                };
                WriteReg(treeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "ColorPrevalence", colorPrevelance);

                WriteReg(treeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\DWM", "ColorPrevalence", ApplyAccentOnTitlebars ? 1 : 0);

                WriteReg(treeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "AccentPalette", ColorBytes, RegistryValueKind.Binary);
                WriteReg(treeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "StartColorMenu", StartMenu_Accent.Reverse().ToArgb());

                WriteReg(treeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\DWM", "AccentColor", Titlebar_Active.Reverse().ToArgb());
                WriteReg(treeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "AccentColorMenu", Titlebar_Active.Reverse().ToArgb());
                WriteReg(treeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\DWM", "AccentColorInactive", Titlebar_Inactive.Reverse().ToArgb());

                if (!OS.W10)
                {
                    WriteReg(treeView, @$"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\SystemProtectedUserData\{User.SID}\AnyoneRead\Colors", "AccentColor", Titlebar_Active.Reverse().ToArgb());
                    WriteReg(treeView, @$"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\SystemProtectedUserData\{User.SID}\AnyoneRead\Colors", "StartColor", StartMenu_Accent.Reverse().ToArgb());
                }

                WriteReg(treeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "SystemUsesLightTheme", WinMode_Light ? 1 : 0);
                WriteReg(treeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "AppsUseLightTheme", AppMode_Light ? 1 : 0);
                WriteReg(treeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "EnableTransparency", Transparency ? 1 : 0);

                // Apply version-specific settings
                ApplySpecific(treeView);

                // Broadcast changes
                BroadcastChanges();
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