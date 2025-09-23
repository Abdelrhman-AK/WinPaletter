using Microsoft.Win32;
using Serilog.Events;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WinPaletter.NativeMethods;
using static WinPaletter.NativeMethods.User32;

namespace WinPaletter.Theme.Structures
{
    /// <summary>
    /// Structure responsible for managing Windows accessibility settings
    /// </summary>
    public class Accessibility : ICloneable
    {
        /// <summary>Controls if this feature is enabled or not</summary>
        public bool Enabled = false;

        /// <summary>Controls if high contrast mode is enabled or not</summary>
        public bool HighContrast = false;

        /// <summary>Enable accessibility feature: color filter</summary>
        public bool ColorFilter_Enabled = false;

        /// <summary>Color filter type</summary>
        public ColorFilters ColorFilter = ColorFilters.Grayscale;

        /// <summary>
        /// Enumeration for color filters
        /// </summary>
        public enum ColorFilters
        {
            ///
            Grayscale,
            ///
            Inverted,
            ///
            GrayscaleInverted,
            ///
            RedGreen_deuteranopia,
            ///
            RedGreen_protanopia,
            ///
            BlueYellow
        }

        /// <summary>
        /// Creates new Accessibility structure with default values
        /// </summary>
        public Accessibility() { }

        /// <summary>
        /// Loads Accessibility data from registry
        /// </summary>
        /// <param name="default">Default Accessibility data structure</param>
        public void Load(Accessibility @default)
        {
            if (Program.Settings.AppLog.Enabled) Program.Log?.Write(LogEventLevel.Information, $"Loading Windows Accessibility settings from registry and User32.SystemParametersInfo");

            Enabled = ReadReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Accessibility", string.Empty, @default.Enabled);

            // Get high contrast settings using SystemParametersInfo from User32.dll
            HIGHCONTRAST highContrastStr = new() { cbSize = Marshal.SizeOf(typeof(HIGHCONTRAST)) };
            SystemParametersInfo(SPI.SPI_GETHIGHCONTRAST, 0, ref highContrastStr, SPIF.SPIF_NONE);
            HighContrast = highContrastStr.dwFlags == 1u;

            ColorFilter_Enabled = ReadReg(@"HKEY_CURRENT_USER\Software\Microsoft\ColorFiltering", "Active", @default.ColorFilter_Enabled);
            ColorFilter = (ColorFilters)ReadReg(@"HKEY_CURRENT_USER\Software\Microsoft\ColorFiltering", "FilterType", @default.ColorFilter);
        }

        /// <summary>
        /// Saves Accessibility data into registry
        /// </summary>
        /// <param name="treeView">treeView used as theme log</param>
        public void Apply(TreeView treeView = null)
        {
            if (Program.Settings.AppLog.Enabled) Program.Log?.Write(LogEventLevel.Information, $"Saving Windows Accessibility settings into registry and by using User32.SystemParametersInfo");

            // Save Accessibility toggle state
            SaveToggleState(treeView);

            if (Enabled)
            {
                WriteReg(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Themes", "ColorSetFromTheme", 0);

                // Set high contrast settings using SystemParametersInfo from User32.dll
                HIGHCONTRAST highContrast = new()
                {
                    cbSize = Marshal.SizeOf(typeof(HIGHCONTRAST)),
                    dwFlags = HighContrast ? 0x1u : 0x0u,
                    lpszDefaultScheme = IntPtr.Zero,
                };

                User32.SystemParametersInfo(User32.SPI.SPI_SETHIGHCONTRAST, highContrast.cbSize, ref highContrast, User32.SPIF.SPIF_WRITEANDNOTIFY);

                if (HighContrast)
                {
                    // Classic high contrast theme settings correction
                    string content = ToString();
                    string path = $"{SysPaths.appData}\\hc.theme";
                    File.WriteAllText(path, content);
                    WriteReg(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Themes", "LastHighContrastTheme", path, RegistryValueKind.String);

                    DeleteKey(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Themes\HighContrast\Pre-High Contrast Scheme");
                    WriteReg(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Themes", "CurrentTheme", string.Empty, RegistryValueKind.String);
                    WriteReg(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Themes", "ColorSetFromTheme", 0);

                    // Broadcast the system message to notify about the setting change
                    User32.SendMessage(IntPtr.Zero, User32.WindowsMessages.WM_SETTINGCHANGE | User32.WindowsMessages.WM_THEMECHANGED | User32.WindowsMessages.WM_SYSCOLORCHANGE, IntPtr.Zero, IntPtr.Zero);
                }

                WriteReg(treeView, @"HKEY_CURRENT_USER\Software\Microsoft\ColorFiltering", "Active", ColorFilter_Enabled);
                WriteReg(treeView, @"HKEY_CURRENT_USER\Software\Microsoft\ColorFiltering", "FilterType", (int)ColorFilter);
                WriteReg(treeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows NT\CurrentVersion\Accessibility", "Configuration", ColorFilter_Enabled ? "colorfiltering" : string.Empty, RegistryValueKind.String);
            }
        }

        /// <summary>
        /// Saves Accessibility toggle state into registry
        /// </summary>
        /// <param name="treeView"></param>
        public void SaveToggleState(TreeView treeView = null)
        {
            WriteReg(treeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Accessibility", string.Empty, Enabled);
        }

        /// <summary>Operator to check if two Accessibility structures are equal</summary>
        public static bool operator ==(Accessibility First, Accessibility Second)
        {
            return First.Equals(Second);
        }

        /// <summary>Operator to check if two Accessibility structures are not equal</summary>
        public static bool operator !=(Accessibility First, Accessibility Second)
        {
            return !First.Equals(Second);
        }

        /// <summary>Clones Accessibility structure</summary>
        public object Clone()
        {
            return MemberwiseClone();
        }

        /// <summary>Checks if two Accessibility structures are equal or not</summary>
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        /// <summary>Get hash code of Accessibility structure</summary>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
