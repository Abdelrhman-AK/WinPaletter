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
        public bool Enabled = false;

        /// <summary>Controls Windows switcher appearance</summary>
        public Styles Style = AltTab.Styles.Default;

        /// <summary>Controls Windows switcher opacity for Windows 10 (or Windows 11 when ExplorerPatcher is installed with changing Alt+Tab appearance into Windows 10</summary>
        public int Win10Opacity = 95;

        /// <summary>
        /// Creates new AltTab structure with default values
        /// </summary>
        public AltTab() { }

        /// <summary>
        /// Enumeration for Windows switcher appearance styles
        /// </summary>
        public enum Styles
        {
            ///
            Default,
            /// <summary>Like what is used in Windows WXP and Vista</summary>
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
            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Loading Windows Alt+Tab switcher settings from registry");

            Enabled = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\AltTab", string.Empty, @default.Enabled));
            Style = (Styles)Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer", "AltTabSettings", @default.Style));
            Win10Opacity = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\MultitaskingView\AltTabViewHost", "Grid_backgroundPercent", @default.Win10Opacity));
            if (Win10Opacity == default)
                Win10Opacity = @default.Win10Opacity;
        }

        /// <summary>
        /// Saves AltTab data into registry
        /// </summary>
        /// <param name="treeView">treeView used as theme log</param>
        public void Apply(TreeView treeView = null)
        {
            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Saving Windows Alt+Tab switcher settings into registry");

            SaveToggleState(treeView);

            if (Enabled)
            {
                EditReg(treeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer", "AltTabSettings", Style);
                EditReg(treeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\MultitaskingView\AltTabViewHost", "Grid_backgroundPercent", Win10Opacity);
            }
        }

        /// <summary>
        /// Saves AltTab toggle state into registry
        /// </summary>
        /// <param name="treeView"></param>
        public void SaveToggleState(TreeView treeView = null)
        {
            EditReg(treeView, @"HKEY_CURRENT_USER\Software\WinPaletter\AltTab", string.Empty, Enabled);
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
