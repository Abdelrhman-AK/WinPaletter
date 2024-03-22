using Microsoft.Win32;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WinPaletter.NativeMethods;

namespace WinPaletter.Theme.Structures
{
    /// <summary>
    /// Structure responsible for managing Windows 8/8.1 appearance
    /// </summary>
    public struct Windows8x : ICloneable
    {
        /// <summary> Controls if Windows 8x colors editing is enabled or not </summary> 
        public bool Enabled = true;

        /// <summary>Start screen background ID. It can be any number from 1 to 20.</summary>
        public int Start;

        /// <summary>Main Windows color</summary>
        public Color ColorizationColor = Color.FromArgb(246, 195, 74);

        /// <summary>Control amount of main Windows color</summary>
        public int ColorizationColorBalance = 78;

        /// <summary>Start screen background color</summary>
        public Color StartColor = Color.FromArgb(30, 0, 84);

        /// <summary>Accent color for start screen and UWP apps</summary>
        public Color AccentColor = Color.FromArgb(72, 29, 178);

        /// <summary>
        /// Theme used for Windows 8.1
        /// <code>
        /// Aero
        /// AeroLite
        /// Basic
        /// Classic
        /// </code>
        /// </summary>
        public Windows7.Themes Theme = Windows7.Themes.Aero;

        /// <summary>Start screen background color (secondary)</summary>
        public Color PersonalColors_Background = Color.FromArgb(30, 0, 84);

        /// <summary>Accent color for start screen and UWP apps (secondary)</summary>
        public Color PersonalColors_Accent = Color.FromArgb(72, 29, 178);

        /// <summary>
        /// Creates new Windows8x data structure
        /// </summary>
        public Windows8x() { }

        /// <summary>
        /// Loads Windows8x data from registry
        /// </summary>
        /// <param name="default">Default Windows8x data structure</param>
        public void Load(string Edition, Windows8x @default)
        {
            Enabled = Convert.ToBoolean(GetReg($@"HKEY_CURRENT_USER\Software\WinPaletter\Aspects\WindowsColorsThemes\Windows10x\{Edition}", string.Empty, @default.Enabled));

            if (OS.W8x)
            {
                object y;

                string stringThemeName = UxTheme.GetCurrentVS().Item1;

                if (stringThemeName.ToString().Split('\\').Last().ToLower() == "aerolite.msstyles" | string.IsNullOrWhiteSpace(stringThemeName.ToString()))
                {
                    Theme = Windows7.Themes.AeroLite;
                }
                else
                {
                    Theme = Windows7.Themes.Aero;
                }

                y = GetReg(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationColor", @default.ColorizationColor.ToArgb());
                ColorizationColor = Color.FromArgb(255, Color.FromArgb(Convert.ToInt32(y)));

                y = GetReg(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationColorBalance", @default.ColorizationColorBalance);
                ColorizationColorBalance = Convert.ToInt32(y);

                y = GetReg(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "StartColor", Color.FromArgb(84, 0, 30).ToArgb());
                StartColor = Color.FromArgb(255, Color.FromArgb(Convert.ToInt32(y))).Reverse();

                y = GetReg(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "AccentColor", Color.FromArgb(178, 29, 72).ToArgb());
                AccentColor = Color.FromArgb(255, Color.FromArgb(Convert.ToInt32(y))).Reverse();

                string S;

                S = GetReg(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Personalization", "PersonalColors_Background", @default.PersonalColors_Background.HEX(false, true)).ToString();
                PersonalColors_Background = S.FromHEXToColor();

                S = GetReg(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Personalization", "PersonalColors_Accent", @default.PersonalColors_Accent.HEX(false, true)).ToString();
                PersonalColors_Accent = S.FromHEXToColor();

                Start = Convert.ToInt32(GetReg(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Personalization", "ForceStartBackground", 0));
            }
            else
            {
                Theme = @default.Theme;
                StartColor = @default.StartColor;
                AccentColor = @default.AccentColor;
                PersonalColors_Background = @default.PersonalColors_Background;
                PersonalColors_Accent = @default.PersonalColors_Accent;
                Start = @default.Start;
            }
        }

        /// <summary>
        /// Saves Windows8x data into registry
        /// </summary>
        /// <param name="treeView">treeView used as theme log</param>
        public void Apply(Theme.Manager TM, string edition, TreeView treeView = null)
        {
            SaveToggleState(edition, treeView);

            if (Enabled)
            {
                EditReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Desktop", "AutoColorization", 0);

                switch (Theme)
                {
                    case Windows7.Themes.Aero:
                        {
                            UxTheme.EnableTheming(1);
                            UxTheme.SetSystemVisualStyle($@"{SysPaths.Windows}\resources\Themes\Aero\Aero.msstyles", "NormalColor", "NormalSize", 0);
                            break;
                        }

                    case Windows7.Themes.AeroLite:
                        {
                            UxTheme.EnableTheming(1);
                            UxTheme.SetSystemVisualStyle($@"{SysPaths.Windows}\resources\Themes\Aero\AeroLite.msstyles", "NormalColor", "NormalSize", 0);

                            DelKey(treeView, "HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Themes\\HighContrast\\Pre-High Contrast Scheme");

                            EditReg(treeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Themes", "CurrentTheme", string.Empty, RegistryValueKind.String);
                            EditReg(treeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Themes", "LastHighContrastTheme", string.Empty, RegistryValueKind.String);
                            break;
                        }
                }

                EditReg(treeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationColor", ColorizationColor.ToArgb());
                EditReg(treeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationColorBalance", ColorizationColorBalance);

                EditReg(treeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "StartColor", StartColor.Reverse().ToArgb());
                EditReg(treeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "DefaultStartColor", StartColor.Reverse().ToArgb());
                EditReg(treeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "AccentColor", AccentColor.Reverse().ToArgb());

                EditReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Personalization", "ForceStartBackground", Start);
                EditReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Personalization", "PersonalColors_Background", $"#{PersonalColors_Background.HEX(false)}", RegistryValueKind.String);
                EditReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Personalization", "PersonalColors_Accent", $"#{PersonalColors_Accent.HEX(false)}", RegistryValueKind.String);

                Program.RefreshDWM(TM);
            }
        }

        /// <summary>
        /// Saves Windows8x toggle state into registry
        /// </summary>
        public void SaveToggleState(string edition, TreeView treeView = null)
        {
            EditReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\Aspects\WindowsColorsThemes\Windows8x\{edition}", string.Empty, Enabled);
        }

        /// <summary>Operator to check if two Windows8x structures are equal</summary>
        public static bool operator ==(Windows8x First, Windows8x Second)
        {
            return First.Equals(Second);
        }

        /// <summary>Operator to check if two Windows8x structures are not equal</summary>
        public static bool operator !=(Windows8x First, Windows8x Second)
        {
            return !First.Equals(Second);
        }

        /// <summary>Clones Windows8x structure</summary>
        public readonly object Clone()
        {
            return MemberwiseClone();
        }

        /// <summary>Checks if two Windows8x structures are equal or not</summary>
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        /// <summary>Get hash code of Windows8x structure</summary>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
