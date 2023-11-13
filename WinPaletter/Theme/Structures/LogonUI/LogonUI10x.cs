using System;
using System.Windows.Forms;

namespace WinPaletter.Theme.Structures
{
    /// <summary>
    /// LogonUI structure for Windows 10/11
    /// </summary>
    public struct LogonUI10x : ICloneable
    {
        /// <summary>If true, it will disable acrylic effect on LogonUI background</summary>
        public bool DisableAcrylicBackgroundOnLogon;

        /// <summary>If true, it will disable background on LogonUI</summary>
        public bool DisableLogonBackgroundImage;

        /// <summary>If true, it will disable lock screen</summary>
        public bool NoLockScreen;

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
        public object Clone()
        {
            return MemberwiseClone();
        }

        /// <summary>
        /// Loads Windows 10/11 LogonUI data from registry
        /// </summary>
        /// <param name="default">Default Windows 10/11 LogonUI data structure</param>
        public void Load(LogonUI10x @default)
        {
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
        /// <param name="TreeView">TreeView used as a theme log</param>
        public void Apply(TreeView TreeView = null)
        {
            EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\System", "DisableAcrylicBackgroundOnLogon", DisableAcrylicBackgroundOnLogon ? 1 : 0);
            EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\System", "DisableLogonBackgroundImage", DisableLogonBackgroundImage ? 1 : 0);
            EditReg(TreeView, @"HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Windows\Personalization", "NoLockScreen", NoLockScreen ? 1 : 0);
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
