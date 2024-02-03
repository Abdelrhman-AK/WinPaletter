using System;
using System.Windows.Forms;

namespace WinPaletter.Theme.Structures
{
    /// <summary>
    /// Structure responsible for managing Windows switcher (Alt+Tab) appearance
    /// </summary>
    public struct AltTab : ICloneable
    {
        /// <summary>Controls if this feature is enabled or not</summary>
        public bool Enabled;

        /// <summary>Controls Windows switcher appearance</summary>
        public Styles Style;

        /// <summary>Controls Windows switcher opacity for Windows 10 (or Windows 11 when ExplorerPatcher is installed with changing Alt+Tab appearance into Windows 10</summary>
        public int Win10Opacity;

        /// <summary>
        /// Enumeration for Windows switcher appearance styles
        /// </summary>
        public enum Styles
        {
            ///
            Default,
            /// <summary>Like what is used in Windows XP and Vista</summary>
            ClassicNT,
            /// <summary>Placeholder: does nothing</summary>
            Placeholder,
            /// <summary>Windows 10 style for Windows 11 with ExplorerPatcher installed</summary>
            EP_Win10
        }

        /// <summary>
        /// Loads AltTab data from registry
        /// </summary>
        /// <param name="default">Default AltTab data structure</param>
        public void Load(AltTab @default)
        {
            Enabled = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\AltTab", string.Empty, @default.Enabled));
            Style = (Styles)Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer", "AltTabSettings", @default.Style));
            Win10Opacity = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\MultitaskingView\AltTabViewHost", "Grid_backgroundPercent", @default.Win10Opacity));
            if (Win10Opacity == default)
                Win10Opacity = @default.Win10Opacity;
        }

        /// <summary>
        /// Saves AltTab data into registry
        /// </summary>
        /// <param name="TreeView">TreeView used as theme log</param>
        public void Apply(TreeView TreeView = null)
        {
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\AltTab", string.Empty, Enabled);
            if (Enabled)
            {
                EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer", "AltTabSettings", Style);
                EditReg(TreeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\MultitaskingView\AltTabViewHost", "Grid_backgroundPercent", Win10Opacity);
            }
        }

        /// <summary>Operator to check if two AltTab structures are equal</summary>
        public static bool operator ==(AltTab First, AltTab Second)
        {
            return First.Equals(Second);
        }

        /// <summary>Operator to check if two AltTab structures are not equal</summary>
        public static bool operator !=(AltTab First, AltTab Second)
        {
            return !First.Equals(Second);
        }

        /// <summary>Clones AltTab structure</summary>
        public readonly object Clone()
        {
            return MemberwiseClone();
        }

        /// <summary>Checks if two AltTab structures are equal or not</summary>
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        /// <summary>Get hash code of AltTab structure</summary>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
