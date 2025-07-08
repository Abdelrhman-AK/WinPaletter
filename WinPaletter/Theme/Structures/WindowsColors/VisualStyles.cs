using Devcorp.Controls.VisualStyles;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using WinPaletter.NativeMethods;
using static WinPaletter.PreviewHelpers;

namespace WinPaletter.Theme.Structures
{
    /// <summary>
    /// Structure responsible for managing VisualStyles, provided that patched themes can be applied
    /// </summary>
    public class VisualStyles : ICloneable
    {
        /// <summary> 
        /// Controls if VisualStyles editing is enabled or not 
        /// </summary> 
        public bool Enabled = false;

        /// <summary>
        /// Visual styles File used when 'WinTheme' selected as 'Custom'
        /// </summary>
        public string ThemeFile = $@"{SysPaths.Windows}\resources\Themes\Aero\Aero.msstyles";

        /// <summary>
        /// Color scheme of visual styles File
        /// </summary>
        public string ColorScheme = "NormalColor";

        /// <summary>
        /// Size scheme of visual styles File
        /// </summary>
        public string SizeScheme = "NormalSize";

        /// <summary>
        /// Override 'Classic Colors' by selected color scheme in this theme when applying
        /// </summary>
        public bool OverrideColors = false;

        /// <summary>
        /// Override 'Metrics and Fonts' by selected size scheme in this theme when applying
        /// </summary>
        public bool OverrideSizes = false;

        /// <summary>
        /// Creates VisualStyles data structure with default values
        /// </summary>
        public VisualStyles() { }

        /// <summary>
        /// Loads VisualStyles data from registry
        /// </summary>
        /// <param name="edition">Edition of Windows</param>
        /// <param name="default">Default VisualStyles data structure</param>
        public void Load(string edition, VisualStyles @default)
        {
            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Loading Windows {edition} Visual Styles using UxTheme.GetCurrentVS");

            Enabled = Convert.ToBoolean(GetReg($"HKEY_CURRENT_USER\\Software\\WinPaletter\\Aspects\\WindowsColorsThemes\\VisualStyles\\{edition}", string.Empty, @default.Enabled));

            Tuple<string, string, string> ThemeTuple = UxTheme.GetCurrentVS();
            string vsFile = ThemeTuple.Item1;
            string colorName = ThemeTuple.Item2;
            string sizeName = ThemeTuple.Item3;

            ThemeFile = vsFile.ToString();
            ColorScheme = colorName.ToString();
            SizeScheme = sizeName.ToString();

            OverrideColors = Convert.ToBoolean(GetReg($"HKEY_CURRENT_USER\\Software\\WinPaletter\\Aspects\\WindowsColorsThemes\\VisualStyles\\{edition}", "OverrideColors", @default.OverrideColors));
            OverrideSizes = Convert.ToBoolean(GetReg($"HKEY_CURRENT_USER\\Software\\WinPaletter\\Aspects\\WindowsColorsThemes\\VisualStyles\\{edition}", "OverrideSizes", @default.OverrideSizes));
        }

        /// <summary>
        /// Saves VisualStyles data into registry
        /// </summary>
        /// <param name="edition">Edition of Windows</param>
        /// <param name="treeView">treeView used as theme log</param>
        public void Apply(string edition, TreeView treeView = null)
        {
            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Saving Windows {edition} Visual Styles using UxTheme.SetSystemVisualStyle and writing into registry.");

            SaveToggleState(edition, treeView);

            if (Enabled)
            {
                if (File.Exists(ThemeFile) && Path.GetExtension(ThemeFile) == ".theme" | Path.GetExtension(ThemeFile) == ".msstyles")
                {
                    if (Path.GetExtension(ThemeFile) == ".theme") ThemeFile = getMsstylesFile(ThemeFile);
                    if (!isCorrectVSPlatform(ThemeFile)) ThemeFile = UxTheme.GetCurrentVS().Item1;

                    UxTheme.EnableTheming(1);
                    UxTheme.SetSystemVisualStyle(ThemeFile, ColorScheme, SizeScheme, 0, treeView);

                    // Don't use ThemeFile as it may be a .theme File
                    // Visual styles File is already set by UxTheme.SetSystemVisualStyle, get it again by using GetCurrentVS method to get correct msstyles File
                    EditReg(treeView, @"HKEY_USERS\.DEFAULT\Software\Microsoft\Windows\CurrentVersion\ThemeManager", "DllName", UxTheme.GetCurrentVS().Item1, RegistryValueKind.String);

                    EditReg(treeView, @"HKEY_USERS\.DEFAULT\Software\Microsoft\Windows\CurrentVersion\ThemeManager", "ColorName", ColorScheme, RegistryValueKind.String);
                    EditReg(treeView, @"HKEY_USERS\.DEFAULT\Software\Microsoft\Windows\CurrentVersion\ThemeManager", "SizeName", SizeScheme, RegistryValueKind.String);
                }

                EditReg(treeView, $"HKEY_CURRENT_USER\\Software\\WinPaletter\\Aspects\\WindowsColorsThemes\\VisualStyles\\{edition}", "OverrideColors", OverrideColors);
                EditReg(treeView, $"HKEY_CURRENT_USER\\Software\\WinPaletter\\Aspects\\WindowsColorsThemes\\VisualStyles\\{edition}", "OverrideSizes", OverrideSizes);
            }
        }

        /// <summary>
        /// Get msstyles File from a .theme File
        /// </summary>
        /// <param name="themeFile"></param>
        /// <returns></returns>
        private string getMsstylesFile(string themeFile)
        {
            using (INI ini = new(themeFile))
            {
                string result = ini.Read("VisualStyles", "Path");

                if (!string.IsNullOrEmpty(result))
                {
                    if (System.IO.File.Exists(result))
                    {
                        return result;
                    }
                    else
                    {
                        result = Environment.ExpandEnvironmentVariables(result);
                        if (System.IO.File.Exists(result))
                        {
                            return result;
                        }
                        else
                        {
                            string fileName = System.IO.Path.GetFileName(result);

                            DirectoryInfo di = new(System.IO.Path.GetDirectoryName(themeFile));
                            List<string> matchingStyles = System.IO.Directory.GetFiles(di.FullName, "*.msstyles", System.IO.SearchOption.AllDirectories)
                                .Where(f => System.IO.Path.GetFileName(f).Equals(fileName, StringComparison.OrdinalIgnoreCase))
                                .ToList();

                            if (matchingStyles.Count > 0)
                            {
                                return matchingStyles.First();
                            }
                            else
                            {
                                return null;
                            }
                        }
                    }
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Check if VisualStyles File is compatible with current Windows version
        /// </summary>
        /// <param name="theme"></param>
        /// <returns></returns>
        private bool isCorrectVSPlatform(string theme)
        {
            try
            {
                using (libmsstyle.VisualStyle vs = new(theme))
                {
                    // Let's assume that W12 is identical to Win11 until official release!
                    if (vs.Platform == libmsstyle.Platform.Win11 && Program.WindowStyle != WindowStyle.W11 && Program.WindowStyle != WindowStyle.W12)
                    {
                        return false;
                    }
                    else if (vs.Platform == libmsstyle.Platform.Win10 && Program.WindowStyle != WindowStyle.W10)
                    {
                        return false;
                    }
                    else if (vs.Platform == libmsstyle.Platform.Win81 && Program.WindowStyle != WindowStyle.W81)
                    {
                        return false;
                    }
                    else if (vs.Platform == libmsstyle.Platform.Win7 && Program.WindowStyle != WindowStyle.W7)
                    {
                        return false;
                    }
                    else if (vs.Platform == libmsstyle.Platform.Vista && Program.WindowStyle != WindowStyle.WVista)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            catch // Couldn't load visual styles File by libmsstyles, so we will assume that it is a Windows XP theme
            {
                try
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
                            return Program.WindowStyle == WindowStyle.WXP;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                catch // Couldn't load visual styles by any method.
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Saves VisualStyles toggle state into registry
        /// </summary>
        /// <param name="edition">Edition of Windows</param>
        /// <param name="treeView"></param>
        public void SaveToggleState(string edition, TreeView treeView = null)
        {
            EditReg(treeView, $"HKEY_CURRENT_USER\\Software\\WinPaletter\\Aspects\\WindowsColorsThemes\\VisualStyles\\{edition}", string.Empty, Enabled);
        }

        /// <summary>Operator to check if two VisualStyles structures are equal</summary>
        public static bool operator ==(VisualStyles First, VisualStyles Second)
        {
            return First.Equals(Second);
        }

        /// <summary>Operator to check if two VisualStyles structures are not equal</summary>
        public static bool operator !=(VisualStyles First, VisualStyles Second)
        {
            return !First.Equals(Second);
        }

        /// <summary>Clones VisualStyles structure</summary>
        public object Clone()
        {
            return MemberwiseClone();
        }

        /// <summary>Checks if two VisualStyles structures are equal or not</summary>
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        /// <summary>Get hash code of VisualStyles structure</summary>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
