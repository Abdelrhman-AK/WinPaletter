using Microsoft.Win32;
using System;
using System.Windows.Forms;
using WinPaletter.NativeMethods;

namespace WinPaletter.Theme.Structures
{
    /// <summary>
    /// Structure responsible for managing Windows XP appearance
    /// </summary>
    public struct WindowsXP : ICloneable
    {
        /// <summary>
        /// Theme used for Windows XP
        /// <code>
        /// LunaBlue
        /// LunaOliveGreen
        /// LunaSilver
        /// Classic
        /// Custom
        /// </code>
        /// </summary>
        public Themes Theme;

        /// <summary>Visual styles file used when 'Theme' selected as 'Custom'</summary>
        public string ThemeFile;

        /// <summary>Color scheme of visual styles file 'ThemeFile' when 'Theme' selected as 'Custom'</summary>
        public string ColorScheme;

        /// <summary>Enumeration of Windows XP stock themes.</summary>
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
            /// Use 'ThemeFile' to be the current visual styles.
            /// <br><b>- Requires UxTheme patched Windows XP</b></br>
            /// </summary>
            Custom
        }

        /// <summary>
        /// Loads WindowsXP data from registry
        /// </summary>
        /// <param name="default">Default WindowsXP data structure</param>
        public void Load(WindowsXP @default)
        {
            if (OS.WXP)
            {
                var vsFile = new System.Text.StringBuilder(260);
                var colorName = new System.Text.StringBuilder(260);
                var sizeName = new System.Text.StringBuilder(260);

                UxTheme.GetCurrentThemeName(vsFile, 260, colorName, 260, sizeName, 260);

                if ((vsFile.ToString().ToLower() ?? "") == (PathsExt.Windows.ToLower() + @"\resources\Themes\Luna\Luna.msstyles".ToLower() ?? ""))
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
                    ThemeFile = PathsExt.Windows.ToLower() + @"\resources\Themes\Luna.theme";
                    ColorScheme = "NormalColor";
                }

                else
                {
                    Theme = Themes.Custom;
                    ThemeFile = "";
                    ColorScheme = "";

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
        /// <param name="TreeView">TreeView used as theme log</param>
        public void Apply(TreeView TreeView = null)
        {
            try
            {
                switch (Theme)
                {
                    case Themes.LunaBlue:
                        {
                            UxTheme.EnableTheming(1);
                            if (TreeView is not null)
                                Manager.AddNode(TreeView, string.Format(Program.Lang.Verbose_UxTheme_ET, "UxTheme", "EnableTheming", 1), "dll");

                            UxTheme.SetSystemVisualStyle(PathsExt.Windows + @"\resources\Themes\Luna\Luna.msstyles", "NormalColor", "NormalSize", 0);
                            if (TreeView is not null)
                                Manager.AddNode(TreeView, string.Format(Program.Lang.Verbose_UxTheme_SSVS, "UxTheme", "SetSystemVisualStyle", PathsExt.Windows + @"\resources\Themes\Luna\Luna.msstyles", "NormalColor", "NormalSize", 0), "dll");

                            Program.StartedWithClassicTheme = false;
                            break;
                        }

                    case Themes.LunaOliveGreen:
                        {
                            UxTheme.EnableTheming(1);
                            if (TreeView is not null)
                                Manager.AddNode(TreeView, string.Format(Program.Lang.Verbose_UxTheme_ET, "UxTheme", "EnableTheming", 1), "dll");

                            UxTheme.SetSystemVisualStyle(PathsExt.Windows + @"\resources\Themes\Luna\Luna.msstyles", "HomeStead", "NormalSize", 0);
                            if (TreeView is not null)
                                Manager.AddNode(TreeView, string.Format(Program.Lang.Verbose_UxTheme_SSVS, "UxTheme", "SetSystemVisualStyle", PathsExt.Windows + @"\resources\Themes\Luna\Luna.msstyles", "HomeStead", "NormalSize", 0), "dll");
                            Program.StartedWithClassicTheme = false;
                            break;
                        }

                    case Themes.LunaSilver:
                        {
                            UxTheme.EnableTheming(1);
                            if (TreeView is not null)
                                Manager.AddNode(TreeView, string.Format(Program.Lang.Verbose_UxTheme_ET, "UxTheme", "EnableTheming", 1), "dll");

                            UxTheme.SetSystemVisualStyle(PathsExt.Windows + @"\resources\Themes\Luna\Luna.msstyles", "Metallic", "NormalSize", 0);
                            if (TreeView is not null)
                                Manager.AddNode(TreeView, string.Format(Program.Lang.Verbose_UxTheme_SSVS, "UxTheme", "SetSystemVisualStyle", PathsExt.Windows + @"\resources\Themes\Luna\Luna.msstyles", "Metallic", "NormalSize", 0), "dll");
                            Program.StartedWithClassicTheme = false;
                            break;
                        }

                    case Themes.Classic:
                        {
                            UxTheme.EnableTheming(0);
                            if (TreeView is not null)
                                Manager.AddNode(TreeView, string.Format(Program.Lang.Verbose_UxTheme_ET, "UxTheme", "EnableTheming", 0), "dll");
                            Program.StartedWithClassicTheme = true;
                            break;
                        }

                    case Themes.Custom:
                        {

                            if (System.IO.File.Exists(ThemeFile) && System.IO.Path.GetExtension(ThemeFile) == ".theme" | System.IO.Path.GetExtension(ThemeFile) == ".msstyles")
                            {
                                UxTheme.EnableTheming(1);
                                if (TreeView is not null)
                                    Manager.AddNode(TreeView, string.Format(Program.Lang.Verbose_UxTheme_ET, "UxTheme", "EnableTheming", 1), "dll");

                                UxTheme.SetSystemVisualStyle(ThemeFile, ColorScheme, "NormalSize", 0);
                                Program.StartedWithClassicTheme = false;
                                if (TreeView is not null)
                                    Manager.AddNode(TreeView, string.Format(Program.Lang.Verbose_UxTheme_SSVS, "UxTheme", "SetSystemVisualStyle", ThemeFile, ColorScheme, "NormalSize", 0), "dll");
                            }

                            break;
                        }

                }

                var vsFile = new System.Text.StringBuilder(260);
                var colorName = new System.Text.StringBuilder(260);
                var sizeName = new System.Text.StringBuilder(260);

                UxTheme.GetCurrentThemeName(vsFile, 260, colorName, 260, sizeName, 260);

                EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Software\Microsoft\Windows\CurrentVersion\ThemeManager", "DllName", vsFile.ToString(), RegistryValueKind.String);
                EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Software\Microsoft\Windows\CurrentVersion\ThemeManager", "ColorName", colorName.ToString(), RegistryValueKind.String);
            }
            catch
            {
            }
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
