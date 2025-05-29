using Microsoft.Win32;
using System;
using System.IO;
using System.IO.Compression;
using System.Runtime.InteropServices;
using System.Security.Principal;
using WinPaletter;

namespace SecureUxTheme
{
    /// <summary>
    ///  SecureUxTheme - A secure boot compatible in-memory UxTheme patcher
    ///  <br><b>Translated from C++ into C# by Abdelrhman-AK</b></br>
    /// <code>
    ///  Copyright (C) 2022  namazso: admin@namazso.eu
    ///  
    ///  This library is free software; you can redistribute it and/or
    ///  modify it under the terms of the GNU Lesser General Public
    ///  License as published by the Free Software Foundation; either
    ///  version 2.1 of the License, or (at your option) any later version.
    ///  
    ///  This library is distributed in the hope that it will be useful,
    ///  but WITHOUT ANY WARRANTY; without even the implied warranty of
    ///  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
    ///  Lesser General Public License for more details.
    ///  
    ///  You should have received a copy of the GNU Lesser General Public
    ///  License along with this library; if not, write to the Free Software
    ///  Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301  USA
    /// </code>
    /// </summary>
    static class Wrapper
    {
        /// SecureUxTheme core class for managing the SecureUxTheme setup
        private class Core
        {
            const int FLG_APPLICATION_VERIFIER = 0x100;
            const string kCurrentColorsPath = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Themes";
            const string kCurrentColorsName = @"DefaultColors";
            const string kCurrentColorsBackup = @"DefaultColors_backup";

            static void MoveKey(RegistryKey sourceKey, string destinationKeyName)
            {
                // Create the destination key
                using (RegistryKey destinationKey = Registry.LocalMachine.CreateSubKey(destinationKeyName))
                {
                    // Copy all values from source to destination
                    foreach (string valueName in sourceKey.GetValueNames())
                    {
                        object value = sourceKey.GetValue(valueName);
                        destinationKey.SetValue(valueName, value);
                    }

                    // Copy all subkeys recursively
                    foreach (string subKeyName in sourceKey.GetSubKeyNames())
                    {
                        using (RegistryKey subKey = sourceKey.OpenSubKey(subKeyName))
                        {
                            MoveKey(subKey, $"{destinationKeyName}\\{subKeyName}");
                        }
                    }
                }

                // After copying, delete the original key
                try
                {
                    Registry.LocalMachine.DeleteSubKeyTree(destinationKeyName);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Error deleting key: {ex.Message}");
                }
            }

            public static void RenameDefaultColors()
            {
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey(kCurrentColorsPath + "\\" + kCurrentColorsName, true))
                {
                    if (key != null)
                    {
                        // Delete backup if any exists for good measure
                        Registry.LocalMachine.DeleteSubKey(kCurrentColorsBackup, false);

                        // Move the key to the backup location
                        MoveKey(key, kCurrentColorsPath + "\\" + kCurrentColorsBackup);

                        // Close the original key
                        key.Close();
                    }
                }

                // Create keys
                using (Registry.LocalMachine.CreateSubKey(kCurrentColorsPath + "\\" + kCurrentColorsName))
                {
                }
                using (Registry.LocalMachine.CreateSubKey(kCurrentColorsPath + "\\" + kCurrentColorsName + "\\HighContrast"))
                {
                }
                using (Registry.LocalMachine.CreateSubKey(kCurrentColorsPath + "\\" + kCurrentColorsName + "\\Standard"))
                {
                }
            }

            public static void RestoreDefaultColors()
            {
                // We ignore failures here because it's not a big deal
                bool currentValid = IsValidDefaultColors(kCurrentColorsName);
                bool backupValid = IsValidDefaultColors(kCurrentColorsBackup);

                if (backupValid && !currentValid)
                {
                    Registry.LocalMachine.DeleteSubKeyTree(kCurrentColorsPath + "\\" + kCurrentColorsName);
                    MoveKey(Registry.LocalMachine.OpenSubKey(kCurrentColorsPath + "\\" + kCurrentColorsBackup), kCurrentColorsPath + "\\" + kCurrentColorsName);
                }
            }

            static bool IsValidDefaultColors(string keyName)
            {
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey($"{kCurrentColorsPath}\\{keyName}"))
                {
                    return key != null;
                }
            }

            public static void CopyToSystem32(string dllName = "SecureUxTheme.dll")
            {
                if (IsLoadedInSession()) return;

                bool is64bit = Environment.Is64BitOperatingSystem;

                using (MemoryStream stream = new(WinPaletter.Properties.Resources.SecureUxTheme))
                using (ZipArchive archive = new(stream, ZipArchiveMode.Read))
                {
                    foreach (ZipArchiveEntry entry in archive.Entries)
                    {
                        try
                        {
                            if (is64bit && entry.FullName.Contains("64"))
                            {
                                entry.ExtractToFile(Path.Combine(SysPaths.System32, dllName), true);
                                return;
                            }
                            else if (!is64bit && entry.FullName.Contains("86"))
                            {
                                entry.ExtractToFile(Path.Combine(SysPaths.System32, dllName), true);
                                return;
                            }
                        }
                        catch // File is in use
                        {
                            return;
                        }
                    }
                }
            }

            public static void InstallForExecutable(string executable, string dllName = "SecureUxTheme.dll")
            {
                EditReg($"HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Image File Execution Options\\{executable}", "GlobalFlag", FLG_APPLICATION_VERIFIER);
                EditReg($"HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Image File Execution Options\\{executable}", "VerifierDlls", dllName, RegistryValueKind.String);
            }

            public static void UninstallForExecutable(string executable)
            {
                EditReg($"HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Image File Execution Options\\{executable}", "GlobalFlag", 0);
                DelValue($"HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Image File Execution Options\\{executable}", "GlobalFlag");
                DelValue($"HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Image File Execution Options\\{executable}", "VerifierDlls");
            }

            public static bool IsLoadedInSession()
            {
                using (WindowsImpersonationContext wic = User.Identity.Impersonate())
                {
                    bool result;

                    IntPtr h = WinPaletter.NativeMethods.Kernel32.OpenEventW(0x00100000, false, "SecureUxTheme_Loaded"); // SYNCHRONIZE = 0x00100000
                    if (h == IntPtr.Zero)
                    {
                        result = Marshal.GetLastWin32Error() == 5; // ERROR_ACCESS_DENIED = 5
                    }
                    else
                    {
                        WinPaletter.NativeMethods.Kernel32.CloseHandle(h);
                        result = true;
                    }

                    wic.Undo();

                    return result;
                }
            }

            public static bool IsInstalledForExecutable(string executable, string dllName = "SecureUxTheme.dll")
            {
                string subkey = $@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Image File Execution Options\{executable}";
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey(subkey))
                {
                    if (key != null)
                    {
                        int GlobalFlag = (int)key.GetValue("GlobalFlag", 0);
                        string VerifierDlls = key.GetValue("VerifierDlls", string.Empty).ToString();
                        return (GlobalFlag & FLG_APPLICATION_VERIFIER) != 0 && string.Equals(VerifierDlls, dllName, StringComparison.OrdinalIgnoreCase);
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        /// <summary>
        /// Run SecureUxTheme in your system.
        /// </summary>
        /// <param name="hookExplorer">Hook <c>explorer.exe</c> so that users can use "Personalization" to set a patched theme</param>
        /// <param name="hookSystemSettings">Hook <c>SystemSettings.exe</c> so that users can can use "Themes" in settings to set a patched theme</param>
        /// <param name="hookLogonUI">Hook <c>LogonUI.exe</c> to prevent LogonUI from resetting colors  </param>
        /// <param name="renameDefaultColors">Rename the default colors registry key to prevent Windows from resetting colors</param>
        public static void Install(bool hookExplorer = false, bool hookSystemSettings = false, bool hookLogonUI = false, bool renameDefaultColors = true)
        {
            Core.CopyToSystem32();
            Core.InstallForExecutable("winlogon.exe");
            if (hookExplorer) Core.InstallForExecutable("explorer.exe");
            if (hookLogonUI) Core.InstallForExecutable("LogonUI.exe");
            if (hookSystemSettings) Core.InstallForExecutable("SystemSettings.exe");
            if (renameDefaultColors) Core.RenameDefaultColors();
        }

        /// <summary>
        /// Uninstall SecureUxTheme from your system.
        /// </summary>
        public static void Uninstall()
        {
            Core.UninstallForExecutable("winlogon.exe");
            Core.UninstallForExecutable("explorer.exe");
            Core.UninstallForExecutable("LogonUI.exe");
            Core.UninstallForExecutable("SystemSettings.exe");
            Core.UninstallForExecutable("dwm.exe");
            Core.RestoreDefaultColors();
        }

        public static bool IsSecureUxThemeInstalled => Core.IsInstalledForExecutable("winlogon.exe");
        public static bool IsExplorerHooked => Core.IsInstalledForExecutable("explorer.exe");
        public static bool IsSystemSettingsHooked => Core.IsInstalledForExecutable("SystemSettings.exe");
        public static bool IsLogonUIHooked => Core.IsInstalledForExecutable("LogonUI.exe");
        public static bool IsSecureUxThemeRunning => Core.IsLoadedInSession();
    }
}