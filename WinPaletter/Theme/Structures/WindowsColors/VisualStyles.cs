using Devcorp.Controls.VisualStyles;
using libmsstyle;
using Microsoft.Win32;
using Serilog.Events;
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
    public class VisualStyles : ManagerBase<VisualStyles>
    {
        /// <summary> 
        /// Controls if VisualStyles editing is enabled or not 
        /// </summary> 
        public bool Enabled { get; set; } = true;

        /// <summary>
        /// Visual styles File used when 'WinTheme' selected as 'Custom'
        /// </summary>
        public string ThemeFile { get; set; } = $@"{SysPaths.Windows}\resources\Themes\Aero\Aero.msstyles";

        /// <summary>
        /// Color scheme of visual styles File
        /// </summary>
        public string ColorScheme { get; set; } = "NormalColor";

        /// <summary>
        /// Size scheme of visual styles File
        /// </summary>
        public string SizeScheme { get; set; } = "NormalSize";

        /// <summary>
        /// Override 'Classic Colors' by selected color scheme in this theme when applying
        /// </summary>
        public bool OverrideColors { get; set; } = false;

        /// <summary>
        /// Override 'Metrics and Fonts' by selected size scheme in this theme when applying
        /// </summary>
        public bool OverrideSizes { get; set; } = false;

        /// <summary>
        /// Represents the default visual styles available for user interface themes.
        /// </summary>
        /// <remarks>This enumeration defines a set of predefined visual styles that can be used to
        /// configure the appearance of a user interface. Each value corresponds to a specific theme or style commonly
        /// associated with Windows operating systems.</remarks>
        public enum DefaultVisualStyles
        {
            /// <summary>
            /// Applies a custom visual style, allowing the user to specify their own theme file.
            /// </summary>
            Custom,

            /// <summary>
            /// Applies the Aero visual style, which is the default for Windows Vista and later versions.
            /// </summary>
            Aero,

            /// <summary>
            /// Represents the AeroLite theme, typically used for accessibility in Windows 8 and later versions.
            /// </summary>
            AeroLite,

            /// <summary>
            /// Applies the Aero visual style, but with an opaque appearance.
            /// </summary>
            AeroOpaque,

            /// <summary>
            /// Applies the Basic visual style, which is a simplified theme available in Windows.
            /// </summary>
            Basic,

            /// <summary>
            /// Represents the LunaBlue visual styles, which is the default theme for Windows XP.
            /// </summary>
            LunaBlue,

            /// <summary>
            /// Represents the LunaSilver class, which is a theme for Windows XP.
            /// entity.
            LunaSilver,

            /// <summary>
            /// Represents the LunaOlive class, which is a theme for Windows XP.
            /// </summary>
            LunaOlive,

            /// <summary>
            /// Applies classic style.
            /// </summary>
            Classic,
        }

        /// <summary>
        /// Specifies the type of visual styles to be applied.
        /// </summary>
        public DefaultVisualStyles VisualStylesType = DefaultVisualStyles.Aero;

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
            Program.Log?.Write(LogEventLevel.Information, $"Loading Windows {edition} Visual Styles using UxTheme.GetCurrentVS");

            Enabled = ReadReg($"HKEY_CURRENT_USER\\Software\\WinPaletter\\Aspects\\WindowsColorsThemes\\{edition}\\VisualStyles", string.Empty, @default.Enabled);

            Tuple<string, string, string> ThemeTuple = UxTheme.GetCurrentVS();
            string vsFile = ThemeTuple.Item1;
            string colorName = ThemeTuple.Item2;
            string sizeName = ThemeTuple.Item3;

            ThemeFile = vsFile.ToString();
            ColorScheme = colorName.ToString();
            SizeScheme = sizeName.ToString();

            OverrideColors = ReadReg($"HKEY_CURRENT_USER\\Software\\WinPaletter\\Aspects\\WindowsColorsThemes\\{edition}\\VisualStyles", "OverrideColors", @default.OverrideColors);
            OverrideSizes = ReadReg($"HKEY_CURRENT_USER\\Software\\WinPaletter\\Aspects\\WindowsColorsThemes\\{edition}\\VisualStyles", "OverrideSizes", @default.OverrideSizes);

            if (ThemeFile.ToLower() == SysPaths.MSSTYLES_Luna_Win.ToLower())
            {
                if (colorName.ToString().ToLower() == "normalcolor")
                {
                    VisualStylesType = DefaultVisualStyles.LunaBlue;
                }
                else if (colorName.ToString().ToLower() == "homestead")
                {
                    VisualStylesType = DefaultVisualStyles.LunaOlive;
                }
                else if (colorName.ToString().ToLower() == "metallic")
                {
                    VisualStylesType = DefaultVisualStyles.LunaSilver;
                }
                else
                {
                    VisualStylesType = DefaultVisualStyles.LunaBlue;
                }
            }
            else if (ThemeFile.ToLower() == SysPaths.MSSTYLES_AeroLite_Win.ToLower())
            {
                VisualStylesType = DefaultVisualStyles.AeroLite;
            }
            else if (ThemeFile.ToLower() == SysPaths.MSSTYLES_Aero_Win.ToLower())
            {
                if (DWMAPI.IsCompositionEnabled())
                {
                    VisualStylesType = !ReadReg(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationOpaqueBlend", false) ? DefaultVisualStyles.Aero : DefaultVisualStyles.AeroOpaque;
                }
                else
                {
                    VisualStylesType = DefaultVisualStyles.Basic;
                }
            }
            else if (File.Exists(ThemeFile) && IsCorrectVSPlatform(ThemeFile).Key)
            {
                VisualStylesType = DefaultVisualStyles.Custom;
            }
            else if (string.IsNullOrEmpty(ThemeFile))
            {
                VisualStylesType = DefaultVisualStyles.Classic;
            }
            else
            {
                VisualStylesType = Program.WindowStyle == WindowStyle.WXP ? DefaultVisualStyles.LunaBlue : DefaultVisualStyles.Aero;
            }
        }

        /// <summary>
        /// Saves VisualStyles data into registry
        /// </summary>
        /// <param name="edition">Edition of Windows</param>
        /// <param name="treeView">treeView used as theme log</param>
        public void Apply(string edition, TreeView treeView = null)
        {
            Program.Log?.Write(LogEventLevel.Information, $"Saving Windows {edition} Visual Styles using UxTheme.SetSystemVisualStyle and writing into registry.");

            SaveToggleState(edition, treeView);

            if (Enabled)
            {
                if (VisualStylesType == DefaultVisualStyles.Custom)
                {
                    if (File.Exists(ThemeFile) && Path.GetExtension(ThemeFile) == ".theme" | Path.GetExtension(ThemeFile) == ".msstyles")
                    {
                        if (Path.GetExtension(ThemeFile) == ".theme") ThemeFile = getMsstylesFile(ThemeFile);
                        if (!IsCorrectVSPlatform(ThemeFile).Key) ThemeFile = UxTheme.GetCurrentVS().Item1;

                        UxTheme.EnableTheming(1);
                        UxTheme.SetSystemVisualStyle(ThemeFile, ColorScheme, SizeScheme, 0, treeView);

                        // Don't use ThemeFile as it may be a .theme File
                        // Visual styles File is already set by UxTheme.SetSystemVisualStyle, get it again by using GetCurrentVS method to get correct msstyles File
                        WriteReg(treeView, @"HKEY_USERS\.DEFAULT\Software\Microsoft\Windows\CurrentVersion\ThemeManager", "DllName", UxTheme.GetCurrentVS().Item1, RegistryValueKind.String);

                        WriteReg(treeView, @"HKEY_USERS\.DEFAULT\Software\Microsoft\Windows\CurrentVersion\ThemeManager", "ColorName", ColorScheme, RegistryValueKind.String);
                        WriteReg(treeView, @"HKEY_USERS\.DEFAULT\Software\Microsoft\Windows\CurrentVersion\ThemeManager", "SizeName", SizeScheme, RegistryValueKind.String);
                    }
                }

                else if (VisualStylesType is DefaultVisualStyles.Aero or DefaultVisualStyles.AeroOpaque or DefaultVisualStyles.Basic)
                {
                    UxTheme.EnableTheming(1);
                    UxTheme.SetSystemVisualStyle(SysPaths.MSSTYLES_Aero_Win, "NormalColor", "NormalSize", 0, treeView);

                    var (policy, composition, opaqueBlend) = VisualStylesType switch
                    {
                        DefaultVisualStyles.Aero => (2, 1, 0),
                        DefaultVisualStyles.AeroOpaque => (2, 1, 1),
                        DefaultVisualStyles.Basic => (1, 0, 0),
                        _ => (0, 0, 0) // fallback, shouldn't occur
                    };

                    WriteReg(treeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "CompositionPolicy", policy);
                    WriteReg(treeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "Composition", composition);
                    WriteReg(treeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationOpaqueBlend", opaqueBlend);
                }

                else if (VisualStylesType is DefaultVisualStyles.AeroLite)
                {
                    UxTheme.EnableTheming(1);
                    UxTheme.SetSystemVisualStyle(SysPaths.MSSTYLES_AeroLite_Win, "NormalColor", "NormalSize", 0);

                    DeleteKey(treeView, "HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Themes\\HighContrast\\Pre-High Contrast Scheme");

                    WriteReg(treeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Themes", "CurrentTheme", string.Empty, RegistryValueKind.String);
                    WriteReg(treeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Themes", "LastHighContrastTheme", string.Empty, RegistryValueKind.String);
                }

                else if (VisualStylesType is DefaultVisualStyles.Classic)
                {
                    UxTheme.EnableTheming(0);
                }

                else if (VisualStylesType is DefaultVisualStyles.LunaBlue)
                {
                    UxTheme.EnableTheming(1);
                    UxTheme.SetSystemVisualStyle($@"{SysPaths.Windows}\resources\Themes\Luna\Luna.msstyles", "NormalColor", "NormalSize", 0);
                }

                else if (VisualStylesType is DefaultVisualStyles.LunaOlive)
                {
                    UxTheme.EnableTheming(1);
                    UxTheme.SetSystemVisualStyle($@"{SysPaths.Windows}\resources\Themes\Luna\Luna.msstyles", "HomeStead", "NormalSize", 0);
                }

                else if (VisualStylesType is DefaultVisualStyles.LunaSilver)
                {
                    UxTheme.EnableTheming(1);
                    UxTheme.SetSystemVisualStyle($@"{SysPaths.Windows}\resources\Themes\Luna\Luna.msstyles", "Metallic", "NormalSize", 0);
                }


                WriteReg(treeView, $"HKEY_CURRENT_USER\\Software\\WinPaletter\\Aspects\\WindowsColorsThemes\\{edition}\\VisualStyles", "OverrideColors", OverrideColors);
                WriteReg(treeView, $"HKEY_CURRENT_USER\\Software\\WinPaletter\\Aspects\\WindowsColorsThemes\\{edition}\\VisualStyles", "OverrideSizes", OverrideSizes);
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
                    if (File.Exists(result))
                    {
                        return result;
                    }
                    else
                    {
                        result = Environment.ExpandEnvironmentVariables(result);
                        if (File.Exists(result))
                        {
                            return result;
                        }
                        else
                        {
                            string fileName = Path.GetFileName(result);

                            DirectoryInfo di = new(Path.GetDirectoryName(themeFile));
                            List<string> matchingStyles = Directory.GetFiles(di.FullName, "*.msstyles", SearchOption.AllDirectories)
                                .Where(f => Path.GetFileName(f).Equals(fileName, StringComparison.OrdinalIgnoreCase))
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
        /// Determines whether the specified visual style file matches the expected platform for the current
        /// application.
        /// </summary>
        /// <remarks>This method checks the platform compatibility of the provided visual style file
        /// against the application's expected platform. The returned string is the OS version that the visual style was designed for.</remarks>
        public KeyValuePair<bool, string> IsCorrectVSPlatform(string theme)
        {
            try
            {
                using (VisualStyle vs = new(theme))
                {
                    // Let's assume that W12 is identical to Win11 until official release !
                    if (vs.Platform == Platform.Win11 && Program.WindowStyle != WindowStyle.W11 && Program.WindowStyle != WindowStyle.W12)
                    {
                        return new(false, Program.Lang.Strings.Windows.W11);
                    }
                    else if (vs.Platform == Platform.Win10 && Program.WindowStyle != WindowStyle.W10)
                    {
                        return new(false, Program.Lang.Strings.Windows.W10);
                    }
                    else if (vs.Platform == Platform.Win81 && Program.WindowStyle != WindowStyle.W81)
                    {
                        return new(false, Program.Lang.Strings.Windows.W81);
                    }
                    else if (vs.Platform == Platform.Win8 && Program.WindowStyle != WindowStyle.W8)
                    {
                        return new(false, Program.Lang.Strings.Windows.W8);
                    }
                    else if (vs.Platform == Platform.Win7 && Program.WindowStyle != WindowStyle.W7)
                    {
                        return new(false, Program.Lang.Strings.Windows.W7);
                    }
                    else if (vs.Platform == Platform.Vista && Program.WindowStyle != WindowStyle.WVista)
                    {
                        return new(false, Program.Lang.Strings.Windows.WVista);
                    }
                    else
                    {
                        return new(true, string.Empty);
                    }
                }
            }
            catch // Couldn't load visual styles File by libmsstyles, so we will assume that it is a Windows XP theme
            {
                try
                {
                    if (Path.GetExtension(theme).ToLower() == ".msstyles")
                    {
                        File.WriteAllText($@"{SysPaths.appData}\VisualStyles\Luna\win32uischeme.theme", $"[VisualStyles]{"\r\n"}Path={theme}{"\r\n"}ColorStyle=NormalColor{"\r\n"}Size=NormalSize");
                        theme = $@"{SysPaths.appData}\VisualStyles\Luna\win32uischeme.theme";
                    }

                    if (File.Exists(theme))
                    {
                        using (VisualStyleFile vs = new(theme))
                        {
                            if (Program.WindowStyle != WindowStyle.WXP)
                            {
                                return new(false, Program.Lang.Strings.Windows.WXP);
                            }
                            else
                            {
                                return new(true, string.Empty);
                            }
                        }
                    }
                    else
                    {
                        return new(false, string.Empty);
                    }
                }
                catch // Couldn't load visual styles by any method.
                {
                    return new(false, string.Empty);
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
            WriteReg(treeView, $"HKEY_CURRENT_USER\\Software\\WinPaletter\\Aspects\\WindowsColorsThemes\\{edition}\\VisualStyles", string.Empty, Enabled);
        }
    }
}
