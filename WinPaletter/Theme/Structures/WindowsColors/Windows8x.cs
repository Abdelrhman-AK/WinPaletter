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
        /// <summary>Start screen background ID. It can be any number from 1 to 20.</summary>
        public int Start;

        /// <summary>Main Windows color</summary>
        public Color ColorizationColor;

        /// <summary>Control amount of main Windows color</summary>
        public int ColorizationColorBalance;

        /// <summary>Start screen background color</summary>
        public Color StartColor;

        /// <summary>Accent color for start screen and UWP apps</summary>
        public Color AccentColor;

        /// <summary>
        /// Theme used for Windows 8.1
        /// <code>
        /// Aero
        /// AeroLite
        /// Basic
        /// Classic
        /// </code>
        /// </summary>
        public Windows7.Themes Theme;

        /// <summary>LogonUI background color ID for Windows 8 only. It can be any number from 0 to 24.</summary>
        public int LogonUI;

        /// <summary>Start screen background color (secondary)</summary>
        public Color PersonalColors_Background;

        /// <summary>Accent color for start screen and UWP apps (secondary)</summary>
        public Color PersonalColors_Accent;

        /// <summary>Disable lock screen</summary>
        public bool NoLockScreen;

        /// <summary>
        /// Lock screen background type/source
        /// <br><b>- It is separate from LogonUI7 structure</b></br>
        /// <code>
        /// Default
        /// Wallpaper
        /// CustomImage
        /// SolidColor
        /// </code>
        /// </summary>
        public LogonUI7.Sources LockScreenType;

        /// <summary>Lock screen stock background image ID</summary>
        public int LockScreenSystemID;

        /// <summary>
        /// Loads Windows8x data from registry
        /// </summary>
        /// <param name="default">Default Windows8x data structure</param>
        public void Load(Windows8x @default)
        {
            if (OS.W8 | OS.W81)
            {
                object y;

                var stringThemeName = UxTheme.GetCurrentVS().Item1;

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
                LogonUI = Convert.ToInt32(GetReg(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "DefaultColorSet", 0));
                LockScreenType = (LogonUI7.Sources)Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI", "Mode", LogonUI7.Sources.Default));
                LockScreenSystemID = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI", "Metro_LockScreenSystemID", 0));
                NoLockScreen = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\Policies\Microsoft\Windows\Personalization", "NoLockScreen", false));
            }
            else
            {
                Theme = @default.Theme;
                StartColor = @default.StartColor;
                AccentColor = @default.AccentColor;
                PersonalColors_Background = @default.PersonalColors_Background;
                PersonalColors_Accent = @default.PersonalColors_Accent;
                Start = @default.Start;
                LogonUI = @default.LogonUI;
                NoLockScreen = @default.NoLockScreen;
                LockScreenType = @default.LockScreenType;
                LockScreenSystemID = @default.LockScreenSystemID;
            }
        }

        /// <summary>
        /// Saves Windows8x data into registry
        /// </summary>
        /// <param name="TreeView">TreeView used as theme log</param>
        public void Apply(TreeView TreeView = null)
        {
            EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop", "AutoColorization", 0);

            try
            {
                switch (Theme)
                {
                    case Windows7.Themes.Aero:
                        {
                            UxTheme.EnableTheming(1);
                            UxTheme.SetSystemVisualStyle(PathsExt.Windows + @"\resources\Themes\Aero\Aero.msstyles", "NormalColor", "NormalSize", 0);
                            break;
                        }

                    case Windows7.Themes.AeroLite:
                        {
                            UxTheme.EnableTheming(1);
                            UxTheme.SetSystemVisualStyle(PathsExt.Windows + @"\resources\Themes\Aero\AeroLite.msstyles", "NormalColor", "NormalSize", 0);
                            try
                            {
                                Program.Computer.Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Themes\HighContrast", true).DeleteSubKeyTree("Pre-High Contrast Scheme", false);
                                if (TreeView is not null)
                                    Manager.AddNode(TreeView, string.Format(Program.Lang.Verbose_DeletingHighContrastThemes, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Themes\HighContrast"), "reg_del");
                            }
                            catch
                            {
                                // Do nothing
                                Program.Computer.Registry.CurrentUser.Close();
                            }
                            finally
                            {
                                Program.Computer.Registry.CurrentUser.Close();
                            }

                            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Themes", "CurrentTheme", string.Empty, RegistryValueKind.String);
                            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Themes", "LastHighContrastTheme", string.Empty, RegistryValueKind.String);
                            break;
                        }

                }
            }
            catch { }

            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationColor", ColorizationColor.ToArgb());
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationColorBalance", ColorizationColorBalance);

            EditReg(TreeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "StartColor", StartColor.Reverse().ToArgb());
            EditReg(TreeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "DefaultStartColor", StartColor.Reverse().ToArgb());
            EditReg(TreeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "AccentColor", AccentColor.Reverse().ToArgb());
            EditReg(TreeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "DefaultColorSet", LogonUI);

            EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Personalization", "ForceStartBackground", Start);
            EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "DefaultColorSet", LogonUI);
            EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Personalization", "PersonalColors_Background", "#" + PersonalColors_Background.HEX(false), RegistryValueKind.String);
            EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Personalization", "PersonalColors_Accent", "#" + PersonalColors_Accent.HEX(false), RegistryValueKind.String);
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
        public object Clone()
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
