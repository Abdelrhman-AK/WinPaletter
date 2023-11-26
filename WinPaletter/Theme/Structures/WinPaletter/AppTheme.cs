using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter.Theme.Structures
{
    /// <summary>
    /// Structure responsible for managing WinPaletter appearance
    /// </summary>
    public struct AppTheme : ICloneable
    {
        /// <summary>Controls if this feature is enabled or not</summary>
        public bool Enabled;

        /// <summary>Back color of forms</summary>
        public Color BackColor;

        /// <summary>Accent color for controls</summary>
        public Color AccentColor;

        /// <summary>Make WinPaletter in dark mode</summary>
        public bool DarkMode;

        /// <summary>Make controls have rounded corners</summary>
        public bool RoundCorners;

        /// <summary>
        /// Loads AppTheme data from registry
        /// </summary>
        /// <param name="default">Default AppTheme data structure</param>
        public void Load(AppTheme @default)
        {
            Enabled = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Settings", "Appearance_Custom", @default.Enabled));
            BackColor = Color.FromArgb(Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Settings", ".BackColor", @default.BackColor.ToArgb())));
            AccentColor = Color.FromArgb(Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Settings", "AccentColor", @default.AccentColor.ToArgb())));
            DarkMode = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Settings", "Custom_Dark", @default.DarkMode));
            RoundCorners = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Settings", "RoundedCorners", @default.RoundCorners));
        }

        /// <summary>
        /// Saves AppTheme data into registry
        /// </summary>
        /// <param name="TreeView">TreeView used as theme log</param>
        public void Apply(TreeView TreeView = null)
        {
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Settings", "Appearance_Custom", Enabled);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Settings", ".BackColor", BackColor.ToArgb());
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Settings", "AccentColor", AccentColor.ToArgb());
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Settings", "Custom_Dark", DarkMode);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Settings", "RoundedCorners", RoundCorners);

            {
                ref Settings.Structures.Appearance Appearance = ref Program.Settings.Appearance;
                Appearance.CustomColors = Enabled;
                Appearance.BackColor = BackColor;
                Appearance.AccentColor = AccentColor;
                Appearance.CustomTheme = DarkMode;
                Appearance.RoundedCorners = RoundCorners;
            }

            ApplyStyle();
        }

        /// <summary>Operator to check if two AppTheme structures are equal</summary>
        public static bool operator ==(AppTheme First, AppTheme Second)
        {
            return First.Equals(Second);
        }

        /// <summary>Operator to check if two AppTheme structures are not equal</summary>
        public static bool operator !=(AppTheme First, AppTheme Second)
        {
            return !First.Equals(Second);
        }

        /// <summary>Clones AppTheme structure</summary>
        public object Clone()
        {
            return MemberwiseClone();
        }

        /// <summary>Checks if two AppTheme structures are equal or not</summary>
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        /// <summary>Get hash code of AppTheme structure</summary>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
