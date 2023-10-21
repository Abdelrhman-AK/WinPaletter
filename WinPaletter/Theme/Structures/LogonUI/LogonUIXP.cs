using Microsoft.Win32;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace WinPaletter.Theme.Structures
{
    public struct LogonUIXP : ICloneable
    {
        public bool Enabled;
        public Modes Mode;
        public Color BackColor;
        public bool ShowMoreOptions;

        public enum Modes
        {
            Win2000,
            Default
        }

        public void Load(LogonUIXP _DefLogonUI)
        {
            if (OS.WXP)
            {

                Enabled = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI\WinXP", "", _DefLogonUI.Enabled));

                switch (GetReg(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon", "LogonType", _DefLogonUI.Mode))
                {
                    case 1:
                        {
                            Mode = Modes.Default;
                            break;
                        }

                    default:
                        {
                            Mode = Modes.Win2000;
                            break;
                        }
                }

                {
                    var temp = GetReg(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon", "Background", "0 0 0");
                    if (temp.ToString().Split(' ').Count() == 3)
                    {
                        BackColor = Color.FromArgb(255, Convert.ToInt32(temp.ToString().Split(' ')[0]), Convert.ToInt32(temp.ToString().Split(' ')[1]), Convert.ToInt32(temp.ToString().Split(' ')[2]));
                    }
                    else
                    {
                        BackColor = _DefLogonUI.BackColor;
                    }
                }

                ShowMoreOptions = Convert.ToBoolean(GetReg(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon", "ShowLogonOptions", _DefLogonUI.ShowMoreOptions));
            }

            else
            {
                Mode = _DefLogonUI.Mode;
                BackColor = _DefLogonUI.BackColor;
                ShowMoreOptions = _DefLogonUI.ShowMoreOptions;
            }
        }

        public void Apply(TreeView TreeView = null)
        {
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI\WinXP", "", Enabled);

            if (Enabled & OS.WXP)
            {
                EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon", "LogonType", Mode == Modes.Default ? 1 : 0, RegistryValueKind.DWord);
                EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon", "Background", BackColor.ToWin32Reg(), RegistryValueKind.String);
                EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon", "ShowLogonOptions", ShowMoreOptions.ToInteger(), RegistryValueKind.DWord);
            }
        }

        public static bool operator ==(LogonUIXP First, LogonUIXP Second)
        {
            return First.Equals(Second);
        }

        public static bool operator !=(LogonUIXP First, LogonUIXP Second)
        {
            return !First.Equals(Second);
        }
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
