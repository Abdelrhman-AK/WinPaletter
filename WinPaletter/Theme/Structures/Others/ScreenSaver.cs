using Microsoft.VisualBasic;
using Microsoft.Win32;
using System;
using System.Windows.Forms;

namespace WinPaletter.Theme.Structures
{
    public struct ScreenSaver : ICloneable
    {
        public bool Enabled;
        public bool IsSecure;
        public int TimeOut;
        public string File;

        public void Load(ScreenSaver _DefScrSaver)
        {
            Enabled = Convert.ToBoolean(Conversion.Val(GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop", "ScreenSaveActive", _DefScrSaver.Enabled.ToInteger())));
            IsSecure = Convert.ToBoolean(Conversion.Val(GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop", "ScreenSaverIsSecure", _DefScrSaver.IsSecure.ToInteger())));
            TimeOut = (int)Math.Round(Conversion.Val(GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop", "ScreenSaveTimeOut", _DefScrSaver.TimeOut)));
            File = GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop", "SCRNSAVE.EXE", _DefScrSaver.File).ToString();
        }

        public void Apply(TreeView TreeView = null)
        {
            EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop", "ScreenSaveActive", Enabled.ToInteger(), RegistryValueKind.String);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop", "ScreenSaverIsSecure", IsSecure.ToInteger(), RegistryValueKind.String);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop", "ScreenSaveTimeOut", TimeOut, RegistryValueKind.String);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop", "SCRNSAVE.EXE", File, RegistryValueKind.String);
        }

        public static bool operator ==(ScreenSaver First, ScreenSaver Second)
        {
            return First.Equals(Second);
        }

        public static bool operator !=(ScreenSaver First, ScreenSaver Second)
        {
            return !First.Equals(Second);
        }
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
