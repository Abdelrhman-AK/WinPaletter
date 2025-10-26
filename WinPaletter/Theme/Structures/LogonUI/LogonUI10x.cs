using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using Serilog.Events;
using System;
using System.IO;
using System.Security.Principal;
using System.Windows.Forms;
using WinPaletter.NativeMethods;

namespace WinPaletter.Theme.Structures
{
    /// <summary>
    /// Lock screen structure for Windows 10/11/12
    /// </summary>
    public class LogonUI10x : ManagerBase<LogonUI10x>
    {
        /// <summary>Controls if this feature is enabled or not</summary>
        public bool Enabled { get; set; } = true;

        /// <summary>If true, it will disable acrylic effect on LogonUI background</summary>
        public bool DisableAcrylicBackgroundOnLogon { get; set; } = false;

        /// <summary>If true, it will disable background on LogonUI</summary>
        public bool DisableLogonBackgroundImage { get; set; } = false;

        /// <summary>If true, it will disable lock screen</summary>
        public bool NoLockScreen { get; set; } = false;

        /// <summary>
        /// Represents the file path of an image.
        /// </summary>
        /// <remarks>This field is initialized to an empty string by default.  It is expected to hold the
        /// path to an image file as a string.</remarks>
        public string ImageFile { get; set; } = string.Empty;

        /// <summary>
        /// Creates a new Windows 10/11 LogonUI data structure with default values
        /// </summary>
        public LogonUI10x() { }

        /// <summary>
        /// Loads Windows 10/11 LogonUI data from registry
        /// </summary>
        /// <param name="default">Default Windows 10/11 LogonUI data structure</param>
        /// <param name="edition">Windows edition (e.g., "Windows10x")</param>
        public void Load(string edition, LogonUI10x @default)
        {
            Program.Log?.Write(LogEventLevel.Information, $"Loading Windows lock screen preferences from registry.");

            Enabled = ReadReg($@"HKEY_CURRENT_USER\Software\WinPaletter\Aspects\LogonUI\{edition}", string.Empty, @default.Enabled);

            DisableAcrylicBackgroundOnLogon = ReadReg(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\System", "DisableAcrylicBackgroundOnLogon", @default.DisableAcrylicBackgroundOnLogon);
            DisableLogonBackgroundImage = ReadReg(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\System", "DisableLogonBackgroundImage", @default.DisableLogonBackgroundImage);
            NoLockScreen = ReadReg(@"HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Windows\Personalization", "NoLockScreen", @default.NoLockScreen);

            ImageFile = ReadReg($@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\PersonalizationCSP", "LockScreenImagePath", @default.ImageFile);
        }

        /// <summary>
        /// Saves Windows 10/11 LogonUI data into registry
        /// </summary>
        /// <param name="treeView">treeView used as a theme log</param>
        /// <param name="edition">Windows edition (e.g., "Windows10x")</param>
        public void Apply(string edition, TreeView treeView = null)
        {
            Program.Log?.Write(LogEventLevel.Information, $"Saving Wiindows lock screen data into registry.");

            SaveToggleState(edition, treeView);

            if (Enabled)
            {
                WriteReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\System", "DisableAcrylicBackgroundOnLogon", DisableAcrylicBackgroundOnLogon ? 1 : 0);
                WriteReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\System", "DisableLogonBackgroundImage", DisableLogonBackgroundImage ? 1 : 0);
                WriteReg(treeView, @"HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Windows\Personalization", "NoLockScreen", NoLockScreen ? 1 : 0);

                if (File.Exists(ImageFile))
                {
                    WriteReg(treeView, $@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\PersonalizationCSP", "LockScreenImagePath", ImageFile, RegistryValueKind.String);
                    WriteReg(treeView, $@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\PersonalizationCSP", "LockScreenImageStatus", 1);
                }
                else
                {
                    DeleteValue(treeView, $@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\PersonalizationCSP", "LockScreenImagePath");
                    DeleteValue(treeView, $@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\PersonalizationCSP", "LockScreenImageStatus");
                }

                using (WindowsImpersonationContext wic = User.Identity.Impersonate())
                {
                    // Broadcast the system message to notify about the setting change
                    Program.Log?.Write(LogEventLevel.Information, "Broadcasting system message to notify about the setting change (User32.UpdatePerUserSystemParameters(1, true)).");
                    User32.UpdatePerUserSystemParameters(1, true);

                    wic.Undo();
                }
            }
        }

        /// <summary>
        /// Saves the toggle state of Windows 10/11 LogonUI
        /// </summary>
        /// <param name="treeView"></param>
        /// <param name="edition"></param>
        public void SaveToggleState(string edition, TreeView treeView = null)
        {
            WriteReg(treeView, @$"HKEY_CURRENT_USER\Software\WinPaletter\Aspects\LogonUI\{edition}", string.Empty, Enabled);
        }
    }
}
