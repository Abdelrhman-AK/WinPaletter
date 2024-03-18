using System;
using System.Drawing;
using System.Windows.Forms;
using WinPaletter.NativeMethods;

namespace WinPaletter.Theme.Structures
{
    /// <summary>
    /// Structure responsible for managing Windows 7 appearance
    /// </summary>
    public struct Windows7 : ICloneable
    {
        /// <summary> Controls if Windows 7 colors editing is enabled or not </summary> 
        public bool Enabled = true;

        /// <summary>Main Windows color</summary>
        public Color ColorizationColor = Color.FromArgb(116, 184, 252);

        /// <summary>Glow or blur color</summary>
        public Color ColorizationAfterglow = Color.FromArgb(116, 184, 252);

        /// <summary>Control amount of main Windows color</summary>
        public int ColorizationColorBalance = 8;

        /// <summary>Control amount of glow color</summary>
        public int ColorizationAfterglowBalance = 43;

        /// <summary>Control amount of blur power for aero glass</summary>
        public int ColorizationBlurBalance = 49;

        /// <summary>Control amount of aero glass reflection</summary>
        public int ColorizationGlassReflectionIntensity = 0;

        /// <summary>
        /// Theme used for Windows 7
        /// <code>
        /// Aero
        /// AeroOpaque
        /// Basic
        /// Classic
        /// </code>
        /// </summary>
        public Themes Theme = Themes.Aero;

        /// <summary>
        /// Creates Windows7 data structure with default values
        /// </summary>
        public Windows7() { }

        /// <summary>
        /// Enumeration of Windows stock themes.
        /// <br><b>It can be used in Windows8x and Windows Vista structures.</b></br>
        /// </summary>
        public enum Themes
        {
            /// <summary>Default Windows theme with transparency</summary>
            Aero,
            /// <summary>Accessibility theme for Windows 8/8.1/10/11</summary>
            AeroLite,
            /// <summary>Default Windows 7 theme but opaque.</summary>
            AeroOpaque,
            /// <summary>Windows basic theme.</summary>
            Basic,
            /// <summary>Classic theme. Applicable to Windows 7 only.</summary>
            Classic
        }

        /// <summary>
        /// Loads Windows7 data from registry
        /// </summary>
        /// <param name="default">Default Windows7 data structure</param>
        public void Load(Windows7 @default)
        {
            Enabled = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Aspects\WindowsColorsThemes\Windows7", string.Empty, @default.Enabled));

            if (OS.W7 | OS.W8x)
            {
                object y;

                y = GetReg(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationColor", @default.ColorizationColor.ToArgb());
                ColorizationColor = Color.FromArgb(255, Color.FromArgb(Convert.ToInt32(y)));

                y = GetReg(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationColorBalance", @default.ColorizationColorBalance);
                ColorizationColorBalance = Convert.ToInt32(y);

                if (OS.W7)
                {
                    y = GetReg(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationAfterglow", @default.ColorizationAfterglow.ToArgb());
                    ColorizationAfterglow = Color.FromArgb(255, Color.FromArgb(Convert.ToInt32(y)));

                    y = GetReg(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationAfterglowBalance", @default.ColorizationAfterglowBalance);
                    ColorizationAfterglowBalance = Convert.ToInt32(y);

                    y = GetReg(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationBlurBalance", @default.ColorizationBlurBalance);
                    ColorizationBlurBalance = Convert.ToInt32(y);

                    y = GetReg(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationGlassReflectionIntensity", @default.ColorizationGlassReflectionIntensity);
                    ColorizationGlassReflectionIntensity = Convert.ToInt32(y);

                    bool Opaque = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationOpaqueBlend", false));

                    bool Classic = false;

                    try
                    {
                        string stringThemeName = UxTheme.GetCurrentVS().Item1;
                        Classic = string.IsNullOrWhiteSpace(stringThemeName.ToString()) | !System.IO.File.Exists(stringThemeName.ToString());
                    }
                    catch // Couldn't get current visual styles, lets assume that it is not classic.
                    {
                        Classic = false;
                    }

                    if (Classic)
                    {
                        Theme = Themes.Classic;
                    }
                    else if (DWMAPI.IsCompositionEnabled())
                    {
                        Theme = !Opaque ? Themes.Aero : Themes.AeroOpaque;
                    }
                    else
                    {
                        Theme = Themes.Basic;
                    }

                }
            }

            else
            {
                ColorizationColor = @default.ColorizationColor;
                ColorizationColorBalance = @default.ColorizationColorBalance;
                ColorizationAfterglow = @default.ColorizationAfterglow;
                ColorizationAfterglowBalance = @default.ColorizationAfterglowBalance;
                ColorizationBlurBalance = @default.ColorizationBlurBalance;
                ColorizationGlassReflectionIntensity = @default.ColorizationGlassReflectionIntensity;
                Theme = @default.Theme;
            }
        }

        /// <summary>
        /// Saves Windows7 data into registry
        /// </summary>
        /// <param name="treeView">treeView used as theme log</param>
        public void Apply(Theme.Manager TM, TreeView treeView = null)
        {
            SaveToggleState(treeView);

            if (Enabled)
            {
                switch (Theme)
                {
                    case Themes.Aero:
                        {
                            UxTheme.EnableTheming(1);
                            UxTheme.SetSystemVisualStyle($@"{SysPaths.Windows}\resources\Themes\Aero\Aero.msstyles", "NormalColor", "NormalSize", 0);

                            EditReg(treeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "CompositionPolicy", 2);
                            EditReg(treeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "Composition", 1);
                            EditReg(treeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationOpaqueBlend", 0);
                            break;
                        }

                    case Themes.AeroOpaque:
                        {
                            UxTheme.EnableTheming(1);
                            UxTheme.SetSystemVisualStyle($@"{SysPaths.Windows}\resources\Themes\Aero\Aero.msstyles", "NormalColor", "NormalSize", 0);

                            EditReg(treeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "CompositionPolicy", 2);
                            EditReg(treeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "Composition", 1);
                            EditReg(treeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationOpaqueBlend", 1);
                            break;
                        }

                    case Themes.Basic:
                        {
                            UxTheme.EnableTheming(1);
                            UxTheme.SetSystemVisualStyle($@"{SysPaths.Windows}\resources\Themes\Aero\Aero.msstyles", "NormalColor", "NormalSize", 0);

                            EditReg(treeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "CompositionPolicy", 1);
                            EditReg(treeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "Composition", 0);
                            EditReg(treeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationOpaqueBlend", 0);
                            break;
                        }

                    case Themes.Classic:
                        {
                            UxTheme.EnableTheming(0);
                            break;
                        }

                }

                EditReg(treeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationAfterglow", ColorizationAfterglow.ToArgb());
                EditReg(treeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationAfterglowBalance", ColorizationAfterglowBalance);
                EditReg(treeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationBlurBalance", ColorizationBlurBalance);
                EditReg(treeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationGlassReflectionIntensity", ColorizationGlassReflectionIntensity);

                EditReg(treeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationColor", ColorizationColor.ToArgb());
                EditReg(treeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationColorBalance", ColorizationColorBalance);
                EditReg(treeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "EnableWindowColorization", 1);

                Program.RefreshDWM(TM);
            }
        }

        /// <summary>
        /// Saves Windows7 toggle state into registry
        /// </summary>
        public void SaveToggleState(TreeView treeView = null)
        {
            EditReg(treeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Aspects\WindowsColorsThemes\Windows7", string.Empty, Enabled);
        }

        /// <summary>Operator to check if two Windows7 structures are equal</summary>
        public static bool operator ==(Windows7 First, Windows7 Second)
        {
            return First.Equals(Second);
        }

        /// <summary>Operator to check if two Windows7 structures are not equal</summary>
        public static bool operator !=(Windows7 First, Windows7 Second)
        {
            return !First.Equals(Second);
        }

        /// <summary>Clones Windows7 structure</summary>
        public readonly object Clone()
        {
            return MemberwiseClone();
        }

        /// <summary>Checks if two Windows7 structures are equal or not</summary>
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        /// <summary>Get hash code of Windows7 structure</summary>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
