using System;
using System.Windows.Forms;

namespace WinPaletter.Theme.Structures
{
    public struct LogonUI10x : ICloneable
    {
        public bool DisableAcrylicBackgroundOnLogon;
        public bool DisableLogonBackgroundImage;
        public bool NoLockScreen;

        public static bool operator ==(LogonUI10x First, LogonUI10x Second)
        {
            return First.Equals(Second);
        }

        public static bool operator !=(LogonUI10x First, LogonUI10x Second)
        {
            return !First.Equals(Second);
        }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public void Load(LogonUI10x _DefLogonUI)
        {
            if (Program.W10 | Program.W11)
            {
                var Def = Program.W11 ? new Theme.Default().Windows11() : new Theme.Default().Windows10();

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

        public void Apply(TreeView TreeView = null)
        {
            EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\System", "DisableAcrylicBackgroundOnLogon", DisableAcrylicBackgroundOnLogon.ToInteger());
            EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\System", "DisableLogonBackgroundImage", DisableLogonBackgroundImage.ToInteger());
            EditReg(TreeView, @"HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Windows\Personalization", "NoLockScreen", NoLockScreen.ToInteger());
        }
    }
}
