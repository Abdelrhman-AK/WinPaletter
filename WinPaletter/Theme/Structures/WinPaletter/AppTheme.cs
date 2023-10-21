using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter.Theme.Structures
{
    public struct AppTheme : ICloneable
    {
        public bool Enabled;
        public Color BackColor;
        public Color AccentColor;
        public bool DarkMode;
        public bool RoundCorners;

        public void Load(AppTheme _DefAppTheme)
        {
            Enabled = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Settings", "Appearance_Custom", _DefAppTheme.Enabled));
            BackColor = Color.FromArgb(Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Settings", ".BackColor", _DefAppTheme.BackColor.ToArgb())));
            AccentColor = Color.FromArgb(Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Settings", "AccentColor", _DefAppTheme.AccentColor.ToArgb())));
            DarkMode = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Settings", "Custom_Dark", _DefAppTheme.DarkMode));
            RoundCorners = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Settings", "RoundedCorners", _DefAppTheme.RoundCorners));
        }
        public void Apply(TreeView TreeView = null)
        {
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Settings", "Appearance_Custom", Enabled);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Settings", ".BackColor", BackColor.ToArgb());
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Settings", "AccentColor", AccentColor.ToArgb());
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Settings", "Custom_Dark", DarkMode);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Settings", "RoundedCorners", RoundCorners);

            {
                ref WPSettings.Structures.Appearance Appearance = ref Program.Settings.Appearance;
                Appearance.CustomColors = Enabled;
                Appearance.BackColor = BackColor;
                Appearance.AccentColor = AccentColor;
                Appearance.CustomTheme = DarkMode;
                Appearance.RoundedCorners = RoundCorners;
            }

            ApplyStyle();
        }

        public static bool operator ==(AppTheme First, AppTheme Second)
        {
            return First.Equals(Second);
        }

        public static bool operator !=(AppTheme First, AppTheme Second)
        {
            return !First.Equals(Second);
        }
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
