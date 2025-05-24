using Microsoft.Win32;
using System;
using System.Windows.Forms;

namespace WinPaletter
{
    internal partial class Program
    {
        /// <summary>
        /// Create the uninstaller registry keys and entry in the control panel programs list
        /// </summary>
        public static void CreateUninstaller()
        {
            string guidText = Application.ProductName;
            string RegPath = $"HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\{guidText}";

            if (!System.IO.Directory.Exists(SysPaths.appData))
            {
                System.IO.Directory.CreateDirectory(SysPaths.appData);
                Program.Log.Information($"A new directory has been created: {SysPaths.appData}");
            }

            // Write uninstaller icon to application data folder if it doesn't exist or changed
            WriteIfChangedOrNotExists($"{SysPaths.appData}\\uninstall.ico", Properties.Resources.Icon_Uninstall.ToBytes());

            EditReg(RegPath, "DisplayName", "WinPaletter", RegistryValueKind.String);
            EditReg(RegPath, "ApplicationVersion", Version, RegistryValueKind.String);
            EditReg(RegPath, "DisplayVersion", Version, RegistryValueKind.String);
            EditReg(RegPath, "Publisher", Application.CompanyName, RegistryValueKind.String);
            EditReg(RegPath, "DisplayIcon", $"{SysPaths.appData}\\uninstall.ico", RegistryValueKind.String);
            EditReg(RegPath, "URLInfoAbout", Links.RepositoryURL, RegistryValueKind.String);
            EditReg(RegPath, "Contact", Links.RepositoryURL, RegistryValueKind.String);
            EditReg(RegPath, "InstallDate", DateTime.Now.ToString("yyyyMMdd"), RegistryValueKind.String);
            EditReg(RegPath, "Comments", Lang.Strings.General.Uninstall_Comment, RegistryValueKind.String);
            EditReg(RegPath, "UninstallString", $"{AppFile} -u", RegistryValueKind.String);
            EditReg(RegPath, "QuietUninstallString", $"{AppFile} -q", RegistryValueKind.String);
            EditReg(RegPath, "InstallLocation", new System.IO.FileInfo(Application.ExecutablePath).DirectoryName, RegistryValueKind.String);
            EditReg(RegPath, "NoModify", 1, RegistryValueKind.DWord);
            EditReg(RegPath, "NoRepair", 1, RegistryValueKind.DWord);
            EditReg(RegPath, "EstimatedSize", Length / 1024, RegistryValueKind.DWord);

            Program.Log.Information($"Uninstaller entry has been updated.");
        }

        // Uninstall the application quietly without user interaction
        public static void Uninstall_Quiet()
        {
            // Delete file associations
            DeleteFileAssociation(".wpth", "WinPaletter.ThemeFile");
            DeleteFileAssociation(".wpsf", "WinPaletter.SettingsFile");
            DeleteFileAssociation(".wptp", "WinPaletter.ThemeResourcesPack");

            // Delete the uninstaller registry keys and entry in the control panel programs list
            Registry.CurrentUser.DeleteSubKeyTree(@"Software\WinPaletter", false);

            // Restore Windows startup sound if it was changed by the application
            try
            {
                if (!OS.WXP && System.IO.File.Exists($"{SysPaths.appData}\\WindowsStartup_Backup.wav"))
                {
                    string file = $"{SysPaths.appData}\\WindowsStartup_Backup.wav";

                    byte[] CurrentSoundBytes = PE.GetResource(SysPaths.imageres, "WAVE", OS.WVista ? 5051 : 5080);
                    byte[] TargetSoundBytes = System.IO.File.ReadAllBytes(file);

                    if (!CurrentSoundBytes.Equals_Method2(TargetSoundBytes))
                    {
                        PE.ReplaceResource(SysPaths.imageres, "WAV", OS.WVista ? 5051 : 5080, TargetSoundBytes);
                    }
                }
            }
            catch { } // Ignore errors, could be caused by lack of permissions and we need to continue with the uninstallation as silent as possible

            // Delete the application data folder and restore the system cursors as WinPaletter cursors are rendered in this folder
            if (System.IO.Directory.Exists(SysPaths.appData))
            {
                if (!OS.WXP)
                {
                    Theme.Structures.Cursors.ResetCursorsToAero();
                    if (Settings.ThemeApplyingBehavior.Cursors_HKU_DEFAULT_Prefs == Settings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite)
                        Theme.Structures.Cursors.ResetCursorsToAero(@"HKEY_USERS\.DEFAULT");
                }

                else
                {
                    Theme.Structures.Cursors.ResetCursorsToNone_XP();
                    if (Settings.ThemeApplyingBehavior.Cursors_HKU_DEFAULT_Prefs == Settings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite)
                        Theme.Structures.Cursors.ResetCursorsToNone_XP(@"HKEY_USERS\.DEFAULT");
                }

                try { System.IO.Directory.Delete(SysPaths.appData, true); }
                catch { } // Ignore errors, could be caused by lack of permissions and we need to continue with the uninstallation as silent as possible

            }

            // Uninstall SysEventsSnds service silently
            Forms.SysEventsSndsInstaller.Uninstall(true);

            // Delete the program data folder
            if (System.IO.Directory.Exists(SysPaths.ProgramFilesData))
            {
                try { System.IO.Directory.Delete(SysPaths.ProgramFilesData, true); }
                catch { } // Ignore errors, could be caused by lack of permissions and we need to continue with the uninstallation as silent as possible
            }

            // Restore system restore frequency
            DelValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\SystemRestore", "SystemRestorePointCreationFrequency");

            string guidText = Application.ProductName;

            DelKey($"HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\{guidText}");

            Program.UninstallDone = true;

            // Exit the application with a success code
            Environment.ExitCode = 0;
            Program.ForceExit();
        }
    }
}