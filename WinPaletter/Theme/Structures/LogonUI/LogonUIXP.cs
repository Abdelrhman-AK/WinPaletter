using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using Serilog.Events;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace WinPaletter.Theme.Structures
{
    /// <summary>
    /// LogonUI structure for Windows XP
    /// </summary>
    public class LogonUIXP : ManagerBase<LogonUIXP>
    {
        /// <summary>Controls if this feature is enabled or not</summary>
        public bool Enabled { get; set; } = false;

        /// <summary>Windows XP LogonUI mode</summary>
        public Modes Mode { get; set; } = Modes.Default;

        /// <summary>Windows XP LogonUI background color if selected 'Source' is 'Win2000'</summary>
        public Color BackColor { get; set; } = Color.Black;

        /// <summary>Controls if 'More options' button is visible if selected 'Source' is 'Win2000'</summary>
        public bool ShowMoreOptions { get; set; } = false;

        /// <summary>
        /// Enumeration for Windows XP LogonUI modes
        /// </summary>
        public enum Modes
        {
            /// <summary>Classic dialog, like in Windows 2000</summary>
            Win2000,
            /// <summary>Default welcome blue screen</summary>
            Default
        }

        /// <summary>
        /// Creates a new Windows XP LogonUI structure with default values
        /// </summary>
        public LogonUIXP() { }

        /// <summary>
        /// Loads Windows XP LogonUI data from registry
        /// </summary>
        /// <param name="default">Default Windows XP LogonUI data structure</param>
        public void Load(LogonUIXP @default)
        {
            Program.Log?.Write(LogEventLevel.Information, $"Loading Windows XP LogonUI screen preferences from registry.");

            if (OS.WXP)
            {
                Enabled = ReadReg(@"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI\WinXP", string.Empty, @default.Enabled);

                switch (ReadReg(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon", "LogonType", @default.Mode))
                {
                    case Modes.Default:
                        {
                            Mode = Modes.Default;
                            break;
                        }

                    case Modes.Win2000:
                        {
                            Mode = Modes.Win2000;
                            break;
                        }

                    default:
                        {
                            Mode = Modes.Default;
                            break;
                        }
                }

                {
                    object temp = ReadReg(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon", "Background", "0 0 0");
                    if (temp.ToString().Split(' ').Count() == 3)
                    {
                        BackColor = Color.FromArgb(255, int.Parse(temp.ToString().Split(' ')[0]), int.Parse(temp.ToString().Split(' ')[1]), int.Parse(temp.ToString().Split(' ')[2]));
                    }
                    else
                    {
                        BackColor = @default.BackColor;
                    }
                }

                ShowMoreOptions = ReadReg(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon", "ShowLogonOptions", @default.ShowMoreOptions);
            }

            else
            {
                Mode = @default.Mode;
                BackColor = @default.BackColor;
                ShowMoreOptions = @default.ShowMoreOptions;
            }
        }

        /// <summary>
        /// Saves Windows XP LogonUI data into registry
        /// </summary>
        /// <param name="treeView">treeView used as theme log</param>
        public void Apply(TreeView treeView = null)
        {
            Program.Log?.Write(LogEventLevel.Information, $"Saving Windows XP LogonUI screen preferences into registry.");

            SaveToggleState(treeView);

            if (Enabled & OS.WXP)
            {
                WriteReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon", "LogonType", Mode == Modes.Default ? 1 : 0, RegistryValueKind.DWord);
                WriteReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon", "Background", BackColor.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true), RegistryValueKind.String);
                WriteReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon", "ShowLogonOptions", ShowMoreOptions ? 1 : 0, RegistryValueKind.DWord);
            }
        }

        /// <summary>
        /// Saves the toggle state of Windows XP LogonUI
        /// </summary>
        /// <param name="treeView"></param>
        public void SaveToggleState(TreeView treeView = null)
        {
            WriteReg(treeView, @"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI\WinXP", string.Empty, Enabled);
        }
    }
}
