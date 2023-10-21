using Microsoft.Win32;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WinPaletter.NativeMethods;

namespace WinPaletter.Theme.Structures
{
    public struct Windows8x : ICloneable
    {
        public int Start;
        public Color ColorizationColor;
        public int ColorizationColorBalance;
        public Color StartColor;
        public Color AccentColor;
        public Windows7.Themes Theme;
        public int LogonUI;
        public Color PersonalColors_Background;
        public Color PersonalColors_Accent;
        public bool NoLockScreen;
        public LogonUI7.Modes LockScreenType;
        public int LockScreenSystemID;

        public static bool operator ==(Windows8x First, Windows8x Second)
        {
            return First.Equals(Second);
        }

        public static bool operator !=(Windows8x First, Windows8x Second)
        {
            return !First.Equals(Second);
        }
        public object Clone()
        {
            return MemberwiseClone();
        }

        public void Load(Windows8x _DefWin)
        {
            if (OS.W8 | OS.W81)
            {
                object y;

                var stringThemeName = new System.Text.StringBuilder(260);
                UxTheme.GetCurrentThemeName(stringThemeName, 260, null, 0, null, 0);

                if (stringThemeName.ToString().Split('\\').Last().ToLower() == "aerolite.msstyles" | string.IsNullOrWhiteSpace(stringThemeName.ToString()))
                {
                    Theme = Windows7.Themes.AeroLite;
                }
                else
                {
                    Theme = Windows7.Themes.Aero;
                }

                y = GetReg(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationColor", _DefWin.ColorizationColor.ToArgb());
                ColorizationColor = Color.FromArgb(255, Color.FromArgb(Convert.ToInt32(y)));

                y = GetReg(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationColorBalance", _DefWin.ColorizationColorBalance);
                ColorizationColorBalance = Convert.ToInt32(y);

                y = GetReg(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "StartColor", Color.FromArgb(84, 0, 30).ToArgb());
                StartColor = Color.FromArgb(255, Color.FromArgb(Convert.ToInt32(y))).Reverse();

                y = GetReg(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "AccentColor", Color.FromArgb(178, 29, 72).ToArgb());
                AccentColor = Color.FromArgb(255, Color.FromArgb(Convert.ToInt32(y))).Reverse();

                string S;

                S = GetReg(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Personalization", "PersonalColors_Background", _DefWin.PersonalColors_Background.HEX(false, true)).ToString();
                PersonalColors_Background = S.FromHEXToColor();

                S = GetReg(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Personalization", "PersonalColors_Accent", _DefWin.PersonalColors_Accent.HEX(false, true)).ToString();
                PersonalColors_Accent = S.FromHEXToColor();

                Start = Convert.ToInt32(GetReg(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Personalization", "ForceStartBackground", 0));
                LogonUI = Convert.ToInt32(GetReg(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "DefaultColorSet", 0));
                LockScreenType = (LogonUI7.Modes)Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI", "Mode", LogonUI7.Modes.Default));
                LockScreenSystemID = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI", "Metro_LockScreenSystemID", 0));
                NoLockScreen = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\Policies\Microsoft\Windows\Personalization", "NoLockScreen", false));
            }
            else
            {
                Theme = _DefWin.Theme;
                StartColor = _DefWin.StartColor;
                AccentColor = _DefWin.AccentColor;
                PersonalColors_Background = _DefWin.PersonalColors_Background;
                PersonalColors_Accent = _DefWin.PersonalColors_Accent;
                Start = _DefWin.Start;
                LogonUI = _DefWin.LogonUI;
                NoLockScreen = _DefWin.NoLockScreen;
                LockScreenType = _DefWin.LockScreenType;
                LockScreenSystemID = _DefWin.LockScreenSystemID;
            }
        }

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
                            if (TreeView is not null)
                                Manager.AddNode(TreeView, string.Format(Program.Lang.Verbose_UxTheme_ET, "UxTheme", "EnableTheming", 1), "dll");

                            UxTheme.SetSystemVisualStyle(PathsExt.Windows + @"\resources\Themes\Aero\Aero.msstyles", "NormalColor", "NormalSize", 0);
                            if (TreeView is not null)
                                Manager.AddNode(TreeView, string.Format(Program.Lang.Verbose_UxTheme_SSVS, "UxTheme", "SetSystemVisualStyle", PathsExt.Windows + @"\resources\Themes\Aero\Aero.msstyles", "NormalColor", "NormalSize", 0), "dll");
                            break;
                        }

                    case Windows7.Themes.AeroLite:
                        {
                            UxTheme.EnableTheming(1);
                            if (TreeView is not null)
                                Manager.AddNode(TreeView, string.Format(Program.Lang.Verbose_UxTheme_ET, "UxTheme", "EnableTheming", 1), "dll");

                            UxTheme.SetSystemVisualStyle(PathsExt.Windows + @"\resources\Themes\Aero\AeroLite.msstyles", "NormalColor", "NormalSize", 0);
                            if (TreeView is not null)
                                Manager.AddNode(TreeView, string.Format(Program.Lang.Verbose_UxTheme_SSVS, "UxTheme", "SetSystemVisualStyle", PathsExt.Windows + @"\resources\Themes\Aero\AeroLite.msstyles", "NormalColor", "NormalSize", 0), "dll");

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

                            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Themes", "CurrentTheme", "", RegistryValueKind.String);
                            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Themes", "LastHighContrastTheme", "", RegistryValueKind.String);
                            break;
                        }

                }
            }
            catch
            {
            }

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
    }
}
