using Microsoft.Win32;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace WinPaletter.Theme.Structures
{
    /// <summary>
    /// LogonUI structure for Windows WXP
    /// </summary>
    public struct LogonUIXP : ICloneable
    {
        /// <summary>Controls if this feature is enabled or not</summary>
        public bool Enabled = false;

        /// <summary>Windows WXP LogonUI mode</summary>
        public Modes Mode = Modes.Default;

        /// <summary>Windows WXP LogonUI background color if selected 'Source' is 'Win2000'</summary>
        public Color BackColor = Color.Black;

        /// <summary>Controls if 'More options' button is visible if selected 'Source' is 'Win2000'</summary>
        public bool ShowMoreOptions = false;

        /// <summary>
        /// Enumeration for Windows WXP LogonUI modes
        /// </summary>
        public enum Modes
        {
            /// <summary>Classic dialog, like in Windows 2000</summary>
            Win2000,
            /// <summary>Default welcome blue screen</summary>
            Default
        }

        /// <summary>
        /// Creates a new Windows WXP LogonUI structure with default values
        /// </summary>
        public LogonUIXP() { }

        /// <summary>
        /// Loads Windows WXP LogonUI data from registry
        /// </summary>
        /// <param name="default">Default Windows WXP LogonUI data structure</param>
        public void Load(LogonUIXP @default)
        {
            if (OS.WXP)
            {
                Enabled = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI\WinXP", string.Empty, @default.Enabled));

                switch (GetReg(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon", "LogonType", @default.Mode))
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
                    object temp = GetReg(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon", "Background", "0 0 0");
                    if (temp.ToString().Split(' ').Count() == 3)
                    {
                        BackColor = Color.FromArgb(255, Convert.ToInt32(temp.ToString().Split(' ')[0]), Convert.ToInt32(temp.ToString().Split(' ')[1]), Convert.ToInt32(temp.ToString().Split(' ')[2]));
                    }
                    else
                    {
                        BackColor = @default.BackColor;
                    }
                }

                ShowMoreOptions = Convert.ToBoolean(GetReg(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon", "ShowLogonOptions", @default.ShowMoreOptions));
            }

            else
            {
                Mode = @default.Mode;
                BackColor = @default.BackColor;
                ShowMoreOptions = @default.ShowMoreOptions;
            }
        }

        /// <summary>
        /// Saves Windows WXP LogonUI data into registry
        /// </summary>
        /// <param name="treeView">treeView used as theme log</param>
        public void Apply(TreeView treeView = null)
        {
            SaveToggleState(treeView);

            if (Enabled & OS.WXP)
            {
                EditReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon", "LogonType", Mode == Modes.Default ? 1 : 0, RegistryValueKind.DWord);
                EditReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon", "Background", BackColor.ToStringWin32(), RegistryValueKind.String);
                EditReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon", "ShowLogonOptions", ShowMoreOptions ? 1 : 0, RegistryValueKind.DWord);
            }
        }

        /// <summary>
        /// Saves the toggle state of Windows WXP LogonUI
        /// </summary>
        /// <param name="treeView"></param>
        public void SaveToggleState(TreeView treeView = null)
        {
            EditReg(treeView, @"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI\WinXP", string.Empty, Enabled);
        }

        /// <summary>Operator to check if two LogonUIXP structures are equal</summary>
        public static bool operator ==(LogonUIXP First, LogonUIXP Second)
        {
            return First.Equals(Second);
        }

        /// <summary>Operator to check if two LogonUIXP structures are not equal</summary>
        public static bool operator !=(LogonUIXP First, LogonUIXP Second)
        {
            return !First.Equals(Second);
        }

        /// <summary>Clones LogonUIXP structure</summary>
        public readonly object Clone()
        {
            return MemberwiseClone();
        }

        /// <summary>
        /// Checks if two LogonUIXP structures are equal or not
        /// </summary>
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        /// <summary>
        /// Get hash code of LogonUIXP structure
        /// </summary>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
