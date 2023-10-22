﻿using System;
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
        /// <param name="_DefLogonUI">Default Windows 10/11 LogonUI data structure</param>
        public void Load(LogonUI10x _DefLogonUI)
        {
            if (OS.W10 | OS.W11)
            {
                DisableAcrylicBackgroundOnLogon = Convert.ToBoolean(GetReg(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\System", "DisableAcrylicBackgroundOnLogon", _DefLogonUI.DisableAcrylicBackgroundOnLogon));
                DisableLogonBackgroundImage = Convert.ToBoolean(GetReg(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\System", "DisableLogonBackgroundImage", _DefLogonUI.DisableLogonBackgroundImage));
                NoLockScreen = Convert.ToBoolean(GetReg(@"HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Windows\Personalization", "NoLockScreen", _DefLogonUI.NoLockScreen));
            }

            else
            {
                DisableAcrylicBackgroundOnLogon = _DefLogonUI.DisableAcrylicBackgroundOnLogon;
                DisableLogonBackgroundImage = _DefLogonUI.DisableLogonBackgroundImage;
                NoLockScreen = _DefLogonUI.NoLockScreen;
            }
        }

        /// <summary>
        /// Apply Windows 10/11 LogonUI data into registry
        /// </summary>
        /// <param name="TreeView">TreeView used as a theme log</param>
        public void Apply(TreeView TreeView = null)
        {
            EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\System", "DisableAcrylicBackgroundOnLogon", DisableAcrylicBackgroundOnLogon.ToInteger());
            EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\System", "DisableLogonBackgroundImage", DisableLogonBackgroundImage.ToInteger());
            EditReg(TreeView, @"HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Windows\Personalization", "NoLockScreen", NoLockScreen.ToInteger());
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
