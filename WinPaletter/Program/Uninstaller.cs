using Microsoft.Win32;
using System;
using System.Windows.Forms;

namespace WinPaletter
{
    internal partial class Program
    {
        public static void CreateUninstaller()
        {
            string guidText = Application.ProductName;
            string RegPath = $"HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\{guidText}";

            if (!System.IO.Directory.Exists(SysPaths.appData)) System.IO.Directory.CreateDirectory(SysPaths.appData);

            WriteIfChangedOrNotExists($"{SysPaths.appData}\\uninstall.ico", Properties.Resources.Icon_Uninstall.ToBytes());

            EditReg(RegPath, "DisplayName", "WinPaletter", RegistryValueKind.String);
            EditReg(RegPath, "ApplicationVersion", Version, RegistryValueKind.String);
            EditReg(RegPath, "DisplayVersion", Version, RegistryValueKind.String);
            EditReg(RegPath, "Publisher", Application.CompanyName, RegistryValueKind.String);
            EditReg(RegPath, "DisplayIcon", $"{SysPaths.appData}\\uninstall.ico", RegistryValueKind.String);
            EditReg(RegPath, "URLInfoAbout", Links.RepositoryURL, RegistryValueKind.String);
            EditReg(RegPath, "Contact", Links.RepositoryURL, RegistryValueKind.String);
            EditReg(RegPath, "InstallDate", DateTime.Now.ToString("yyyyMMdd"), RegistryValueKind.String);
            EditReg(RegPath, "Comments", Lang.Uninstall_Comment, RegistryValueKind.String);
            EditReg(RegPath, "UninstallString", $"{AppFile} -u", RegistryValueKind.String);
            EditReg(RegPath, "QuietUninstallString", $"{AppFile} -q", RegistryValueKind.String);
            EditReg(RegPath, "InstallLocation", new System.IO.FileInfo(Application.ExecutablePath).DirectoryName, RegistryValueKind.String);
            EditReg(RegPath, "NoModify", 1, RegistryValueKind.DWord);
            EditReg(RegPath, "NoRepair", 1, RegistryValueKind.DWord);
            EditReg(RegPath, "EstimatedSize", Length / 1024, RegistryValueKind.DWord);
        }

        public static void Uninstall_Quiet()
        {
            DeleteFileAssociation(".wpth", "WinPaletter.ThemeFile");
            DeleteFileAssociation(".wpsf", "WinPaletter.SettingsFile");
            DeleteFileAssociation(".wptp", "WinPaletter.ThemeResourcesPack");

            Registry.CurrentUser.DeleteSubKeyTree(@"Software\WinPaletter", false);

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

            Forms.SysEventsSndsInstaller.Uninstall(true);

            if (System.IO.Directory.Exists(SysPaths.ProgramFilesData))
            {
                try { System.IO.Directory.Delete(SysPaths.ProgramFilesData, true); }
                catch { } // Ignore errors, could be caused by lack of permissions and we need to continue with the uninstallation as silent as possible
            }

            string guidText = Application.ProductName;

            DelKey($"HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\{guidText}");

            Program.UninstallDone = true;

            Environment.ExitCode = 0;

            Program.ForceExit();
        }
    }
}