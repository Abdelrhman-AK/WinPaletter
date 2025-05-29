using System;
using System.Windows.Forms;
using static WinPaletter.CMD;

namespace WinPaletter.Theme.Structures
{
    /// <summary>
    /// Lock screen structure for Windows 10/11/12
    /// </summary>
    public struct LogonUI10x : ICloneable
    {
        /// <summary>Controls if this feature is enabled or not</summary>
        public bool Enabled = true;

        /// <summary>If true, it will disable acrylic effect on LogonUI background</summary>
        public bool DisableAcrylicBackgroundOnLogon = false;

        /// <summary>If true, it will disable background on LogonUI</summary>
        public bool DisableLogonBackgroundImage = false;

        /// <summary>If true, it will disable lock screen</summary>
        public bool NoLockScreen = false;

        /// <summary>
        /// Creates a new Windows 10/11 LogonUI data structure with default values
        /// </summary>
        public LogonUI10x() { }

        /// <summary>Operator to check if two LogonUI10x structures are equal</summary>
        public static bool operator ==(LogonUI10x First, LogonUI10x Second)
        {
            return First.Equals(Second);
        }

        /// <summary>Operator to check if two LogonUI10x structures are not equal</summary>
        public static bool operator !=(LogonUI10x First, LogonUI10x Second)
        {
            return !First.Equals(Second);
        }

        /// <summary>Clones LogonUI10x structure</summary>
        public readonly object Clone()
        {
            return MemberwiseClone();
        }

        /// <summary>
        /// Loads Windows 10/11 LogonUI data from registry
        /// </summary>
        /// <param name="default">Default Windows 10/11 LogonUI data structure</param>
        public void Load(LogonUI10x @default)
        {
            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Loading Windows 10/11/12 lock screen preferences from registry.");

            Enabled = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Aspects\LogonUI\Windows10x", string.Empty, @default.Enabled));

            if (OS.W12 || OS.W11 || OS.W10)
            {
                DisableAcrylicBackgroundOnLogon = Convert.ToBoolean(GetReg(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\System", "DisableAcrylicBackgroundOnLogon", @default.DisableAcrylicBackgroundOnLogon));
                DisableLogonBackgroundImage = Convert.ToBoolean(GetReg(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\System", "DisableLogonBackgroundImage", @default.DisableLogonBackgroundImage));
                NoLockScreen = Convert.ToBoolean(GetReg(@"HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Windows\Personalization", "NoLockScreen", @default.NoLockScreen));
            }

            else
            {
                DisableAcrylicBackgroundOnLogon = @default.DisableAcrylicBackgroundOnLogon;
                DisableLogonBackgroundImage = @default.DisableLogonBackgroundImage;
                NoLockScreen = @default.NoLockScreen;
            }
        }

        /// <summary>
        /// Saves Windows 10/11 LogonUI data into registry
        /// </summary>
        /// <param name="treeView">treeView used as a theme log</param>
        public void Apply(TreeView treeView = null)
        {
            SaveToggleState(treeView);

            if (Enabled)
            {
                EditReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\System", "DisableAcrylicBackgroundOnLogon", DisableAcrylicBackgroundOnLogon ? 1 : 0);
                EditReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\System", "DisableLogonBackgroundImage", DisableLogonBackgroundImage ? 1 : 0);
                EditReg(treeView, @"HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Windows\Personalization", "NoLockScreen", NoLockScreen ? 1 : 0);
            }
        }

        /// <summary>
        /// Saves the toggle state of Windows 10/11 LogonUI
        /// </summary>
        /// <param name="treeView"></param>
        public void SaveToggleState(TreeView treeView = null)
        {
            EditReg(treeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Aspects\LogonUI\Windows10x", string.Empty, Enabled);
        }

        /// <summary>
        /// Checks if two LogonUI10x structures are equal or not
        /// </summary>
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        /// <summary>
        /// Get hash code of LogonUI10x structure
        /// </summary>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }
}
