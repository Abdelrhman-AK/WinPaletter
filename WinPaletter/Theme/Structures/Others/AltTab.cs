using Serilog.Events;
using System;
using System.Windows.Forms;

namespace WinPaletter.Theme.Structures
{
    /// <summary>
    /// Structure responsible for managing Windows switcher (Alt+Tab) appearance
    /// </summary>
    public class AltTab : ManagerBase<AltTab>
    {
        /// <summary>Controls if this feature is enabled or not</summary>
        public bool Enabled { get; set; } = false;

        /// <summary>Controls Windows switcher appearance</summary>
        public Styles Style { get; set; } = AltTab.Styles.Default;

        /// <summary>Controls Windows switcher opacity for Windows 10 (or Windows 11 when ExplorerPatcher is installed with changing Alt+Tab appearance into Windows 10</summary>
        public int Win10Opacity { get; set; } = 95;

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
            Program.Log?.Write(LogEventLevel.Information, $"Loading Windows Alt+Tab switcher settings from registry");

            Enabled = ReadReg(@"HKEY_CURRENT_USER\Software\WinPaletter\AltTab", string.Empty, @default.Enabled);
            Style = ReadReg(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer", "AltTabSettings", @default.Style);
            Win10Opacity = ReadReg(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\MultitaskingView\AltTabViewHost", "Grid_backgroundPercent", @default.Win10Opacity);
            if (Win10Opacity == default)
                Win10Opacity = @default.Win10Opacity;
        }

        /// <summary>
        /// Saves AltTab data into registry
        /// </summary>
        /// <param name="treeView">treeView used as theme log</param>
        public void Apply(TreeView treeView = null)
        {
            Program.Log?.Write(LogEventLevel.Information, $"Saving Windows Alt+Tab switcher settings into registry");

            SaveToggleState(treeView);

            if (Enabled)
            {
                WriteReg(treeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer", "AltTabSettings", Style);
                WriteReg(treeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\MultitaskingView\AltTabViewHost", "Grid_backgroundPercent", Win10Opacity);
            }
        }

        /// <summary>
        /// Saves AltTab toggle state into registry
        /// </summary>
        /// <param name="treeView"></param>
        public void SaveToggleState(TreeView treeView = null)
        {
            WriteReg(treeView, @"HKEY_CURRENT_USER\Software\WinPaletter\AltTab", string.Empty, Enabled);
        }
    }
}
