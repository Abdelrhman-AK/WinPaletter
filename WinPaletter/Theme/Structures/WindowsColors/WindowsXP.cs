using Microsoft.Win32;
using System;
using System.Windows.Forms;
using WinPaletter.NativeMethods;
using static WinPaletter.CMD;

namespace WinPaletter.Theme.Structures
{
    /// <summary>
    /// Structure responsible for managing Windows WXP appearance
    /// </summary>
    public class WindowsXP : ICloneable
    {
        /// <summary> Controls if Windows WXP themes editing is enabled or not </summary> 
        public bool Enabled = true;

        /// <summary>
        /// WinTheme used for Windows WXP
        /// <code>
        /// LunaBlue
        /// LunaOliveGreen
        /// LunaSilver
        /// Classic
        /// Custom
        /// </code>
        /// </summary>
        public Themes Theme = Themes.LunaBlue;

        /// <summary>Visual styles File used when <see cref="Theme"/> selected as <see cref="Themes.Custom"/></summary>
        public string ThemeFile = $@"{SysPaths.Windows}\resources\Themes\Luna\Luna.msstyles";

        /// <summary>Color scheme of visual styles File 'WindowsXPThemePath' when 'WinTheme' selected as 'Custom'</summary>
        public string ColorScheme = "NormalColor";

        /// <summary>
        /// Creates WindowsXP data structure with default values
        /// </summary>
        public WindowsXP() { }

        /// <summary>Enumeration of Windows WXP stock themes.</summary>
        public enum Themes
        {
            /// <summary>Blue default color scheme of Luna</summary>
            LunaBlue,
            /// <summary>Olive green color scheme of Luna</summary>
            LunaOliveGreen,
            /// <summary>Silver color scheme of Luna</summary>
            LunaSilver,
            /// <summary>Classic theme</summary>
            Classic,
            /// <summary>
            /// Use 'WindowsXPThemePath' to be the current visual styles.
            /// <br><b>- Requires UxTheme patched Windows WXP</b></br>
            /// </summary>
            Custom
        }

        /// <summary>
        /// Loads WindowsXP data from registry
        /// </summary>
        /// <param name="default">Default WindowsXP data structure</param>
        public void Load(WindowsXP @default)
        {
            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Loading Windows XP appearance preferences from registry and UxTheme.GetCurrentVS()");

            Enabled = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Aspects\WindowsColorsThemes\WindowsXP", string.Empty, @default.Enabled));

            if (OS.WXP)
            {
                Tuple<string, string, string> ThemeTuple = UxTheme.GetCurrentVS();
                string vsFile = ThemeTuple.Item1;
                string colorName = ThemeTuple.Item2;

                if ((vsFile.ToString().ToLower() ?? string.Empty) == (SysPaths.Windows.ToLower() + @"\resources\Themes\Luna\Luna.msstyles".ToLower() ?? string.Empty))
                {
                    if (colorName.ToString().ToLower() == "normalcolor")
                    {
                        Theme = Themes.LunaBlue;
                    }
                    else if (colorName.ToString().ToLower() == "homestead")
                    {
                        Theme = Themes.LunaOliveGreen;
                    }
                    else if (colorName.ToString().ToLower() == "metallic")
                    {
                        Theme = Themes.LunaSilver;
                    }
                    else
                    {
                        Theme = Themes.LunaBlue;
                    }

                    ThemeFile = vsFile.ToString();
                    ColorScheme = colorName.ToString();
                }

                else if (System.IO.File.Exists(vsFile.ToString()) && System.IO.Path.GetExtension(vsFile.ToString()) == ".theme" | System.IO.Path.GetExtension(vsFile.ToString()) == ".msstyles")
                {
                    Theme = Themes.Custom;
                    ThemeFile = vsFile.ToString();
                    ColorScheme = colorName.ToString();
                }

                else if (string.IsNullOrEmpty(vsFile.ToString()))
                {
                    Theme = Themes.Classic;
                    ThemeFile = $@"{SysPaths.Windows.ToLower()}\resources\Themes\Luna.theme";
                    ColorScheme = "NormalColor";
                }

                else
                {
                    Theme = Themes.Custom;
                    ThemeFile = string.Empty;
                    ColorScheme = string.Empty;
                }
            }

            else
            {
                Theme = @default.Theme;
                ThemeFile = @default.ThemeFile;
                ColorScheme = @default.ColorScheme;
            }
        }

        /// <summary>
        /// Saves WindowsXP data into registry
        /// </summary>
        /// <param name="treeView">treeView used as theme log</param>
        public void Apply(TreeView treeView = null)
        {
            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Saving Windows XP appearance preferences into registry and by using UxTheme.EnableTheming and UxTheme.SetSystemVisualStyle");

            SaveToggleState(treeView);

            if (Enabled)
            {
                switch (Theme)
                {
                    case Themes.LunaBlue:
                        {
                            UxTheme.EnableTheming(1);
                            UxTheme.SetSystemVisualStyle($@"{SysPaths.Windows}\resources\Themes\Luna\Luna.msstyles", "NormalColor", "NormalSize", 0);
                            break;
                        }

                    case Themes.LunaOliveGreen:
                        {
                            UxTheme.EnableTheming(1);
                            UxTheme.SetSystemVisualStyle($@"{SysPaths.Windows}\resources\Themes\Luna\Luna.msstyles", "HomeStead", "NormalSize", 0);
                            break;
                        }

                    case Themes.LunaSilver:
                        {
                            UxTheme.EnableTheming(1);
                            UxTheme.SetSystemVisualStyle($@"{SysPaths.Windows}\resources\Themes\Luna\Luna.msstyles", "Metallic", "NormalSize", 0);
                            break;
                        }

                    case Themes.Classic:
                        {
                            UxTheme.EnableTheming(0);
                            break;
                        }

                    case Themes.Custom:
                        {
                            if (System.IO.File.Exists(ThemeFile) && System.IO.Path.GetExtension(ThemeFile) == ".theme" | System.IO.Path.GetExtension(ThemeFile) == ".msstyles")
                            {
                                UxTheme.EnableTheming(1);
                                UxTheme.SetSystemVisualStyle(ThemeFile, ColorScheme, "NormalSize", 0);
                            }
                            break;
                        }
                }

                Tuple<string, string, string> ThemeTuple = UxTheme.GetCurrentVS();

                EditReg(treeView, @"HKEY_USERS\.DEFAULT\Software\Microsoft\Windows\CurrentVersion\ThemeManager", "DllName", ThemeTuple.Item1, RegistryValueKind.String);
                EditReg(treeView, @"HKEY_USERS\.DEFAULT\Software\Microsoft\Windows\CurrentVersion\ThemeManager", "ColorName", ThemeTuple.Item2, RegistryValueKind.String);
            }
        }

        /// <summary>
        /// Saves WindowsXP toggle state into registry
        /// </summary>
        /// <param name="treeView"></param>
        public void SaveToggleState(TreeView treeView = null)
        {
            EditReg(treeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Aspects\WindowsColorsThemes\WindowsXP", string.Empty, Enabled);
        }

        /// <summary>Operator to check if two WindowsXP structures are equal</summary>
        public static bool operator ==(WindowsXP First, WindowsXP Second)
        {
            return First.Equals(Second);
        }

        /// <summary>Operator to check if two WindowsXP structures are not equal</summary>
        public static bool operator !=(WindowsXP First, WindowsXP Second)
        {
            return !First.Equals(Second);
        }

        /// <summary>Clones WindowsXP structure</summary>
        public object Clone()
        {
            return MemberwiseClone();
        }

        /// <summary>Checks if two WindowsXP structures are equal or not</summary>
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        /// <summary>Get hash code of WindowsXP structure</summary>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
