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

        /// <summary> Secondary color for controls </summary>
        public Color SecondaryColor;

        /// <summary> Tertiary color for controls </summary>
        public Color TertiaryColor;

        /// <summary> Disabled color for controls </summary>
        public Color DisabledColor;

        /// <summary> Disabled back color for controls </summary>
        public Color DisabledBackColor;

        /// <summary>Make WinPaletter in dark mode</summary>
        public bool DarkMode;

        /// <summary>Make controls have rounded corners</summary>
        public bool RoundCorners;

        /// <summary>Enable animations</summary>
        public bool Animations;

        /// <summary>
        /// Loads AppTheme data from registry
        /// </summary>
        /// <param name="default">Default AppTheme data structure</param>
        public void Load(AppTheme @default)
        {
            Enabled = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Settings\Appearance", "CustomColors", @default.Enabled));
            Animations = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Settings\Appearance", "Animations", @default.Animations));
            BackColor = Color.FromArgb(Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Settings\Appearance", "BackColor", @default.BackColor.ToArgb())));
            AccentColor = Color.FromArgb(Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Settings\Appearance", "AccentColor", @default.AccentColor.ToArgb())));
            SecondaryColor = Color.FromArgb(Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Settings\Appearance", "SecondaryColor", @default.SecondaryColor.ToArgb())));
            TertiaryColor = Color.FromArgb(Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Settings\Appearance", "TertiaryColor", @default.TertiaryColor.ToArgb())));
            DisabledColor = Color.FromArgb(Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Settings\Appearance", "DisabledColor", @default.DisabledColor.ToArgb())));
            DisabledBackColor = Color.FromArgb(Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Settings\Appearance", "DisabledBackColor", @default.DisabledBackColor.ToArgb())));
            DarkMode = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Settings\Appearance", "CustomTheme", @default.DarkMode));
            RoundCorners = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Settings\Appearance", "RoundedCorners", @default.RoundCorners));



        }

        /// <summary>
        /// Saves AppTheme data into registry
        /// </summary>
        /// <param name="TreeView">TreeView used as theme log</param>
        public void Apply(TreeView TreeView = null)
        {
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Settings\Appearance", "CustomColors", Enabled);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Settings\Appearance", "BackColor", BackColor.ToArgb());
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Settings\Appearance", "AccentColor", AccentColor.ToArgb());
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Settings\Appearance", "CustomTheme", DarkMode);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Settings\Appearance", "RoundedCorners", RoundCorners);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Settings\Appearance", "Animations", Animations);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Settings\Appearance", "SecondaryColor", SecondaryColor.ToArgb());
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Settings\Appearance", "TertiaryColor", TertiaryColor.ToArgb());
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Settings\Appearance", "DisabledColor", DisabledColor.ToArgb());
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Settings\Appearance", "DisabledBackColor", DisabledBackColor.ToArgb());

            {
                ref Settings.Structures.Appearance Appearance = ref Program.Settings.Appearance;
                Appearance.CustomColors = Enabled;
                Appearance.BackColor = BackColor;
                Appearance.AccentColor = AccentColor;
                Appearance.CustomTheme_DarkMode = DarkMode;
                Appearance.RoundedCorners = RoundCorners;
                Appearance.Animations = Animations;
                Appearance.SecondaryColor = SecondaryColor;
                Appearance.TertiaryColor = TertiaryColor;
                Appearance.DisabledColor = DisabledColor;
                Appearance.DisabledBackColor = DisabledBackColor;
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
