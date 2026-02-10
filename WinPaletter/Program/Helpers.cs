using Microsoft.Win32;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinPaletter.Assets;
using WinPaletter.NativeMethods;
using WinPaletter.Properties;
using WinPaletter.Theme;
using static WinPaletter.Theme.Manager;

namespace WinPaletter
{
    internal partial class Program
    {
        /// <summary>
        /// GetTextAndImageRectangles the MD5 hash of a file
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string CalculateMD5(string path)
        {
            string MD5_str;

            if (File.Exists(path))
            {
                using (MD5 md5 = MD5.Create())
                {
                    byte[] hash = md5.ComputeHash(File.ReadAllBytes(path));
                    string result = BitConverter.ToString(hash).Replace("-", string.Empty);
                    MD5_str = result.ToUpper();

                    Program.Log?.Write(LogEventLevel.Information, $"MD5 hash of the file `{path}` is: {MD5_str}");
                }
            }
            else
            {
                MD5_str = "0";

                Program.Log?.Write(LogEventLevel.Warning, $"File `{path}` does not exist, MD5 hash cannot be calculated. Returning 0.");
            }

            return MD5_str;
        }

        /// <summary>
        /// Get a unique unused File name in the given directory
        /// </summary>
        /// <param name="directoryPath"></param>
        /// <param name="baseFileName"></param>
        /// <returns></returns>
        public static string GetUniqueFileName(string directoryPath, string baseFileName)
        {
            string fullFilePath = Path.Combine(directoryPath, baseFileName);

            if (!File.Exists(fullFilePath))
            {
                Program.Log?.Write(LogEventLevel.Information, $"File `{fullFilePath}` does not exist, returning it as unique file name.");

                // The File with the given name does not exist, so it is already unique
                return fullFilePath;
            }

            // If the File exists, generate a unique File name
            int counter = 1;
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(baseFileName);
            string fileExtension = Path.GetExtension(baseFileName);

            do
            {
                string uniqueFileName = $"{fileNameWithoutExtension}_{counter}{fileExtension}";
                fullFilePath = Path.Combine(directoryPath, uniqueFileName);
                counter++;
            }
            while (File.Exists(fullFilePath));

            Program.Log?.Write(LogEventLevel.Information, $"File `{fullFilePath}` already exists, returning a unique file name: {fullFilePath}.");

            return fullFilePath;
        }

        /// <summary>
        /// Send a command to Command Prompt as Administrator impersonating the selected user
        /// </summary>
        /// <param name="command"></param>
        /// <param name="Wait"></param>
        /// <param name="nonAdmin"></param>
        public static void SendCommand(string command, bool Wait = true, bool nonAdmin = false)
        {
            if (!nonAdmin || OS.WXP)
            {
                using (WindowsImpersonationContext wic = User.Identity_Admin.Impersonate())
                {
                    using (Process process = new()
                    {
                        StartInfo = new()
                        {
                            FileName = command.Split(' ')[0],
                            Verb = OS.WXP ? string.Empty : "runas",
                            Arguments = command.Split(' ').Count() > 0 ? string.Join(" ", command.Split(' ').Skip(1)) : string.Empty,
                            WindowStyle = ProcessWindowStyle.Hidden,
                            CreateNoWindow = true,
                            UseShellExecute = true
                        }
                    })
                    {
                        Program.Log?.Write(LogEventLevel.Information, $"Executing command in Command Prompt as Administrator: {command}");
                        process.Start();
                        if (Wait) process.WaitForExit();
                        Program.Log?.Write(LogEventLevel.Information, $"Command executed successfully: {command}");
                    }

                    wic.Undo();
                }
            }
            else
            {
                Task.Run(() =>
                {
                    string taskName = "WinPaletter_CommandNonAdminDeflector";
                    string userName = Environment.UserName;

                    // Create future time (safe for Windows 7)
                    // When you create a one-time task with /SC ONCE, the task must have a valid future start time.
                    // If the time(/ ST) is equal to or before the current time, Windows Task Scheduler will not run it.
                    DateTime startTime = DateTime.Now.AddMinutes(1);
                    string formattedTime = startTime.ToString("HH:mm");

                    // Create task
                    string createCmd = $"schtasks /Create /TN \"{taskName}\" /TR \"{command}\" /SC ONCE /ST {formattedTime} {(!OS.WXP ? "/RL LIMITED" : string.Empty)} /F /RU \"{userName}\"";
                    SendCommand(createCmd);

                    // Run the task
                    string runCmd = $"schtasks /Run /TN \"{taskName}\"";
                    SendCommand(runCmd);

                    if (OS.WVista || OS.W7) Thread.Sleep(500);

                    // Delete task
                    string deleteCmd = $"schtasks /Delete /TN \"{taskName}\" /F";
                    SendCommand(deleteCmd);
                });
            }
        }

        /// <summary>
        /// List all running processes with the given FullPath
        /// </summary>
        /// <param name="FullPath"></param>
        /// <returns></returns>
        public static List<Process> ProgramsRunning(string FullPath)
        {
            List<Process> processes = [];
            string FileName = Path.GetFileNameWithoutExtension(FullPath).ToLower();

            foreach (Process p in Process.GetProcessesByName(FileName))
            {
                if (FullPath.ToLower() == Kernel32.GetProcessFilename(p).ToLower())
                    processes.Add(p);
            }

            if (processes.Count > 0)
            {
                Program.Log?.Write(LogEventLevel.Information, $"Found {processes.Count} running processes with the FullPath: {FullPath}");
            }
            else
            {
                Program.Log?.Write(LogEventLevel.Information, $"No running processes found with the FullPath: {FullPath}");
            }

            return processes;
        }

        /// <summary>
        /// Load the Theme Manager and set the Window Style for the preview depending on the OS version
        /// </summary>
        private static void LoadThemeManager()
        {
            Program.Log?.Write(LogEventLevel.Information, $"Loading Theme Manager and setting Window Style for the preview.");

            if (OS.W12) WindowStyle = PreviewHelpers.WindowStyle.W12;

            if (OS.W11) WindowStyle = PreviewHelpers.WindowStyle.W11;

            else if (OS.W10) WindowStyle = PreviewHelpers.WindowStyle.W10;

            else if (OS.W81) WindowStyle = PreviewHelpers.WindowStyle.W81;

            else if (OS.W8) WindowStyle = PreviewHelpers.WindowStyle.W8;

            else if (OS.W7) WindowStyle = PreviewHelpers.WindowStyle.W7;

            else if (OS.WVista) WindowStyle = PreviewHelpers.WindowStyle.WVista;

            else if (OS.WXP) WindowStyle = PreviewHelpers.WindowStyle.WXP;

            else WindowStyle = PreviewHelpers.WindowStyle.W12;

            // Load Manager
            if (!ExternalLink)
            {
                Program.Log?.Write(LogEventLevel.Information, $"Loading Theme Manager from selected user preferences and registry.");

                // TM is not null, but TM_Original is so during startup.
                if (TM_Original is null) TM = new(Source.Registry);
                Forms.Home.Text = Application.ProductName;
            }
            else
            {
                Program.Log?.Write(LogEventLevel.Information, $"Loading Theme Manager from File: {ExternalLink_File}");

                TM = new(Source.File, ExternalLink_File);
                Forms.Home.File = ExternalLink_File;
                Forms.Home.Text = Path.GetFileName(ExternalLink_File);
                ExternalLink = false;
                ExternalLink_File = string.Empty;
            }

            TM_Original = TM.Clone();
            TM_FirstTime = TM.Clone();

            if (Program.Settings.BackupTheme.Enabled && Program.Settings.BackupTheme.AutoBackupOnAppOpen)
            {
                Log?.Write(LogEventLevel.Information, $"Creating a backup of the current theme on application open.");

                string filename = Program.GetUniqueFileName($"{Program.Settings.BackupTheme.BackupPath}\\OnAppOpen", $"{TM.Info.ThemeName}_{DateTime.Now.Hour}.{DateTime.Now.Minute}.{DateTime.Now.Second}.wpth");
                TM.Save(Source.File, filename);
            }
        }

        /// <summary>
        /// Configure the security protocol to support TLS 1.1 and TLS 1.2 for better compatibility with modern web services, while maintaining fallback options for older protocols. This method attempts to enable TLS 1.1 and TLS 1.2 by adding their corresponding values to the SecurityProtocolType enumeration. It also includes fallbacks for TLS 1.0 and SSL 3.0 to ensure compatibility with older systems, while prioritizing the use of more secure protocols when available.
        /// </summary>
        private static void ConfigureSecurityProtocol()
        {
            ServicePointManager.Expect100Continue = false;

            SecurityProtocolType protocols = 0;

            try { protocols |= (SecurityProtocolType)768; } catch { }   // TLS 1.1
            try { protocols |= (SecurityProtocolType)3072; } catch { }  // TLS 1.2
            try { protocols |= SecurityProtocolType.Tls; } catch { }    // TLS 1.0
            try { protocols |= SecurityProtocolType.Ssl3; } catch { }   // SSL 3.0 (legacy fallback)

            if (protocols != 0)
                ServicePointManager.SecurityProtocol = protocols;
        }

        /// <summary>
        /// Delete the old WinPaletter executable files
        /// </summary>
        private static void DeleteUpdateResiduals()
        {
            try
            {
                if (File.Exists("oldWinpaletter.trash"))
                {
                    Program.Log?.Write(LogEventLevel.Information, $"Deleting old WinPaletter executable file: oldWinpaletter.trash");
                    File.Delete("oldWinpaletter.trash");
                }
                if (File.Exists("oldWinpaletter_2.trash"))
                {
                    Program.Log?.Write(LogEventLevel.Information, $"Deleting old WinPaletter executable file: oldWinpaletter_2.trash");
                    File.Delete("oldWinpaletter_2.trash");
                }

            }
            catch { } // Ignore deleting old executable files if they are in use
        }

        /// <summary>
        /// Get Jetbrains Mono font from memory
        /// </summary>
        private static void GetMemoryFonts()
        {
            try
            {
                Program.Log?.Write(LogEventLevel.Information, $"Loading JetBrains Mono font from memory.");

                using (MemoryStream ms = new(Resources.JetBrainsMono))
                using (ZipArchive zip = new(ms))
                using (MemoryStream _as = new())
                {
                    zip.Entries[0].Open().CopyTo(_as);
                    _as.Seek(0L, SeekOrigin.Begin);
                    MemoryFonts.AddMemoryFont(_as.ToArray());
                }

                Fonts.Console = MemoryFonts.GetFont(0, 7.75f);
                Fonts.ConsoleMedium = MemoryFonts.GetFont(0, 9f);
                Fonts.ConsoleLarge = MemoryFonts.GetFont(0, 10f);
            }
            catch
            {
                Program.Log?.Write(LogEventLevel.Warning, $"Failed to load JetBrains Mono font from memory, falling back to Lucida Console.");

                Fonts.Console = new("Lucida Console", 7.5f);
                Fonts.ConsoleMedium = new("Lucida Console", 9f);
                Fonts.ConsoleLarge = new("Lucida Console", 10f);
            }
        }

        /// <summary>
        /// Load the language file
        /// </summary>
        public static void LoadLanguage()
        {
            if (Settings.Language.Enabled)
            {
                try
                {
                    Program.Log?.Write(LogEventLevel.Information, $"Loading language file: {Settings.Language.File}");
                    Localization.Load(Settings.Language.File);
                }
                catch (Exception ex)
                {
                    Program.Log?.Write(LogEventLevel.Error, $"Failed to load language file: {Settings.Language.File}. Using default language instead.", ex);
                    Forms.BugReport.Throw(ex);
                }
            }
        }

        /// <summary>
        /// Check if the setup has been completed. If not, show the setup form and exit if the user does not continue with the setup.
        /// </summary>
        public static void CheckIfSetupIsComplete()
        {
            if (!Settings.General.SetupCompleted)
            {
                if (Forms.Setup.ShowDialog() != DialogResult.OK)
                {
                    Log?.Write(LogEventLevel.Information, $"The setup has not been completed, WinPaletter will exit.");
                    Program.ForceExit();
                }
            }

            Log?.Write(LogEventLevel.Information, $"The setup has not been completed, continuing...");
        }

        /// <summary>
        /// Start monitoiring Windows Wallpaper and Theme changes to adjust the preview accordingly
        /// </summary>
        private static void StartMonitors()
        {
            FirstVisualStyles = UxTheme.GetCurrentVS().Item1 ?? SysPaths.Windows + "\\Resources\\Themes\\aero\\aero.msstyles";

            if (!OS.WXP)
            {
                Monitor();
            }
            else
            {
                // Force SystemEvents window creation to avoid Windows XP crashes when subscribing to UserPreferenceChanged event
                var dummy = typeof(SystemEvents);
                SystemEvents.UserPreferenceChanged += WallpaperMonitor.OnUserPreferenceChanged;
                WallpaperMonitor.FireWallpaperChanged(); // Init wallpaper
            }
        }

        /// <summary>
        /// Initialize the Image Lists for the Theme Log and the Language TreeView
        /// </summary>
        private static void InitializeImageLists()
        {
            Program.Log?.Write(LogEventLevel.Information, $"Initializing Image Lists for Theme Log and Language TreeView.");

            ImageLists.ThemeLog.AddWithAlpha("info", Notifications.Info);
            ImageLists.ThemeLog.AddWithAlpha("apply", Notifications.Applying);
            ImageLists.ThemeLog.AddWithAlpha("error", Notifications.Error);
            ImageLists.ThemeLog.AddWithAlpha("warning", Notifications.Warning);
            ImageLists.ThemeLog.AddWithAlpha("time", Notifications.Time);
            ImageLists.ThemeLog.AddWithAlpha("success", Notifications.Success);
            ImageLists.ThemeLog.AddWithAlpha("skip", Notifications.Skip);
            ImageLists.ThemeLog.AddWithAlpha("admin", Notifications.Administrator);
            ImageLists.ThemeLog.AddWithAlpha("reg_add", Notifications.Reg_add);
            ImageLists.ThemeLog.AddWithAlpha("reg_delete", Notifications.Reg_delete);
            ImageLists.ThemeLog.AddWithAlpha("reg_skip", Notifications.Reg_skip);
            ImageLists.ThemeLog.AddWithAlpha("task_remove", Notifications.Task_remove);
            ImageLists.ThemeLog.AddWithAlpha("file_rename", Notifications.File_rename);
            ImageLists.ThemeLog.AddWithAlpha("dll", Notifications.DLL);
            ImageLists.ThemeLog.AddWithAlpha("pe_patch", Notifications.PE_patch);
            ImageLists.ThemeLog.AddWithAlpha("pe_backup", Notifications.PE_backup);
            ImageLists.ThemeLog.AddWithAlpha("pe_restore", Notifications.PE_restore);

            ImageLists.Language.AddWithAlpha("main", Resources.LangNode_Main);
            ImageLists.Language.AddWithAlpha("value", Resources.LangNode_Value);
            ImageLists.Language.AddWithAlpha("json", Resources.LangNode_JSON);

            Exceptions.ThemeApply.Clear();
            Exceptions.ThemeLoad.Clear();
        }

        /// <summary>
        /// Associate the WinPaletter file types with the application.
        /// </summary>
        private static void AssociateFiles()
        {
            if (!Settings.FileTypeManagement.AutoAddExt) return;

            if (!Directory.Exists(SysPaths.appData))
            {
                Directory.CreateDirectory(SysPaths.appData);
                Log?.Write(LogEventLevel.Information, $"A new directory has been created: {SysPaths.appData}");
            }

            // Prepare resources once
            var resources = new (string fileName, byte[] data)[]
            {
                ($"{SysPaths.appData}\\fileextension.ico", Resources.fileextension.ToBytes()),
                ($"{SysPaths.appData}\\settingsfile.ico", Resources.settingsfile.ToBytes()),
                ($"{SysPaths.appData}\\themerespack.ico", Resources.ThemesResIcon.ToBytes())
            };

            // Write or update icons only if needed
            foreach (var (file, data) in resources)
                WriteIfChangedOrNotExists(file, data);

            string exePath = Assembly.GetExecutingAssembly().Location;
            string exeQuoted = $"\"{exePath}\"";

            // Use spans and caching for faster string ops
            var associations = new (string ext, string cls, string desc, string icon)[]
            {
                (".wpth", "WinPaletter.ThemeFile", Localization.Strings.Extensions.WinPaletterThemeFiles, $"{SysPaths.appData}\\fileextension.ico"),
                (".wpsf", "WinPaletter.SettingsFile", Localization.Strings.Extensions.WinPaletterSettings, $"{SysPaths.appData}\\settingsfile.ico"),
                (".wptp", "WinPaletter.ThemeResourcesPack", Localization.Strings.Extensions.WinPaletterResourcesPack, $"{SysPaths.appData}\\themerespack.ico")
            };

            bool anyChanged = false;

            foreach (var (ext, cls, desc, icon) in associations)
            {
                if (!CreateFileAssociation(ext, cls, desc, icon, exePath)) anyChanged = true;
            }

            if (anyChanged)
            {
                // Notify Windows that file associations changed
                Shell32.SHChangeNotify(Shell32.ShellConstants.SHCNE_ASSOCCHANGED, Shell32.ShellConstants.SHCNF_IDLIST, 0, 0);
                Log?.Write(LogEventLevel.Information, "File associations have been updated.");
            }
        }

        /// <summary>
        /// Writes the specified data to a file if the file does not exist or if the file's contents differ from the
        /// provided data.
        /// </summary>
        /// <remarks>If the file does not exist, it will be created and the data will be written to it. 
        /// If the file exists but its contents differ from the provided data, the file will be overwritten with the new
        /// data. The method performs a byte-by-byte comparison to determine if the file's contents match the provided
        /// data.</remarks>
        /// <param name="file">The path of the file to write to. This must be a valid file path.</param>
        /// <param name="data">The data to write to the file, represented as a byte array.</param>
        private static void WriteIfChangedOrNotExists(string file, byte[] data)
        {
            if (!File.Exists(file))
            {
                Log?.Write(LogEventLevel.Information, $"A new file has been created: {file}");
                File.WriteAllBytes(file, data);
                return;
            }

            using (var fs = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                if (fs.Length != data.Length)
                {
                    fs.Close();
                    Log?.Write(LogEventLevel.Information, $"A file has been updated: {file}");
                    File.WriteAllBytes(file, data);
                    return;
                }

                byte[] buffer = new byte[8192];
                int offset = 0;
                while (offset < data.Length)
                {
                    int read = fs.Read(buffer, 0, Math.Min(buffer.Length, data.Length - offset));
                    for (int i = 0; i < read; i++)
                    {
                        if (buffer[i] != data[offset + i])
                        {
                            Log?.Write(LogEventLevel.Information, $"A file has been updated: {file}");
                            fs.Close();
                            File.WriteAllBytes(file, data);
                            return;
                        }
                    }
                    offset += read;
                }
            }
        }

        /// <summary>
        /// Extracts the Luna theme files from a ZIP archive and ensures they are written to the appropriate directory.
        /// If the files already exist and are identical, extraction is skipped.
        /// </summary>
        /// <remarks>This method performs the following operations: <list type="bullet">
        /// <item><description>Ensures the theme directory exists.</description></item> <item><description>Extracts
        /// files from the ZIP archive to the theme directory, skipping files that are unchanged.</description></item>
        /// <item><description>Logs the creation or update of directories and files.</description></item>
        /// <item><description>Writes a Luna.theme configuration file to the theme directory.</description></item>
        /// </list> If an error occurs during the extraction process, the exception is logged and reported.</remarks>
        private static void ExtractLuna()
        {
            try
            {
                // Ensure the theme directory exists
                Directory.CreateDirectory(SysPaths.Theme_Dir_WP);

                // Write ZIP file only if changed
                WriteIfChangedOrNotExists(SysPaths.Theme_ZIP_WP, Resources.luna);

                using (FileStream archiveStream = File.OpenRead(SysPaths.Theme_ZIP_WP))
                using (ZipArchive zip = new(archiveStream, ZipArchiveMode.Read, false))
                {
                    // Use a reusable buffer to reduce allocations
                    byte[] buffer = new byte[4096 * 32];

                    foreach (ZipArchiveEntry entry in zip.Entries)
                    {
                        string destinationPath = Path.Combine(SysPaths.Theme_Dir_WP, entry.FullName);

                        // Ensure directory exists
                        string destDir = Path.GetDirectoryName(destinationPath);
                        if (!string.IsNullOrEmpty(destDir) && !Directory.Exists(destDir))
                        {
                            Directory.CreateDirectory(destDir);
                            Log?.Write(LogEventLevel.Information, $"A new directory has been created: {destDir}");
                        }

                        // Skip extraction if file is same size and contents
                        if (File.Exists(destinationPath))
                        {
                            var existingInfo = new FileInfo(destinationPath);
                            if (existingInfo.Length == entry.Length)
                            {
                                using (var entryStream = entry.Open())
                                using (var fs = File.OpenRead(destinationPath))
                                {
                                    if (fs.Equals(entryStream, buffer)) continue; // identical, skip writing
                                }
                            }
                        }

                        // Write new or changed file
                        using (var entryStream = entry.Open())
                        using (var fs = File.Create(destinationPath))
                        {
                            int bytesRead;
                            while ((bytesRead = entryStream.Read(buffer, 0, buffer.Length)) > 0)
                                fs.Write(buffer, 0, bytesRead);
                        }

                        Log?.Write(LogEventLevel.Information,
                            File.Exists(destinationPath)
                                ? $"A file has been updated: {destinationPath}"
                                : $"A file has been extracted: {destinationPath}");
                    }
                }

                // Write Luna.theme file
                File.WriteAllText(SysPaths.Theme_Luna_WP,
                    "[VisualStyles]\r\n" +
                    $"Path={SysPaths.MSSTYLES_Luna_WP}\r\n" +
                    "ColorStyle=NormalColor\r\n" +
                    "Size=NormalSize");
            }
            catch (Exception ex)
            {
                Forms.BugReport.Throw(ex);
            }
        }

        private static byte[] cachedSound;

        /// <summary>
        /// Creates a backup of the Windows startup sound if it does not already exist.
        /// </summary>
        /// <remarks>This method checks the operating system version and skips execution on Windows XP. 
        /// If a backup file named "WindowsStartup_Backup.wav" does not exist in the application data directory,  the
        /// method retrieves the default Windows startup sound from system resources and writes it to the backup
        /// file.</remarks>
        private static void BackupWindowsStartupSound()
        {
            if (OS.WXP) return;

            string backupPath = Path.Combine(SysPaths.appData, "WindowsStartup_Backup.wav");
            if (File.Exists(backupPath)) return;

            cachedSound ??= PE.GetResource(SysPaths.imageres, "WAVE", OS.WVista ? 5051 : 5080);
            File.WriteAllBytes(backupPath, cachedSound);
        }

        /// <summary>
        /// Update SysEventsSounds service
        /// </summary>
        public static void UpdateSysEventsSounds()
        {
            if (!Program.UninstallDone)
            {
                if (File.Exists(SysPaths.SysEventsSounds) && !Resources.WinPaletter_SysEventsSounds.Equals_Method2(File.ReadAllBytes(SysPaths.SysEventsSounds)))
                {
                    Log?.Write(LogEventLevel.Information, $"SysEventsSounds service is not up to date, updating it.");
                    Forms.ServiceInstaller.Run("WinPaletter.SystemEventsSounds", Program.Localization.Strings.Services.Description_SysEventsSounds, SysPaths.SysEventsSounds, Resources.WinPaletter_SysEventsSounds, ServiceInstaller.RunMethods.Update);
                    return;
                }
            }
        }

        /// <summary>
        /// Check if the new version of application is running for the first time, and show the What's New pop-up if it is
        /// </summary>
        public static void CheckWhatsNew()
        {
            if (!Settings.General.WhatsNewRecord.Contains(Version))
            {
                Program.Log?.Write(LogEventLevel.Information, $"This application version is running for the first time: {Version}");

                // Display the What'archiveStream New pop-up
                ShowWhatsNew = true;

                // Update version information
                List<string> versionHistory = [Version, .. Settings.General.WhatsNewRecord];
                versionHistory = [.. versionHistory.Distinct()];
                Settings.General.WhatsNewRecord = [.. versionHistory];

                // Save updated settings
                Settings.General.Save();
            }
            else
            {
                ShowWhatsNew = false;
            }
        }

        /// <summary>
        /// Refresh DWM colorization parameters from the given <see cref="Theme.Manager"/> impersonating the selected user
        /// </summary>
        /// <param name="TM"></param>
        public static void RefreshDWM(Manager TM)
        {
            Task.Run(() =>
            {
                using (WindowsImpersonationContext wic = User.Identity.Impersonate())
                {
                    Program.Log?.Write(LogEventLevel.Information, $"Refreshing DWM colorization parameters from the given Theme Manager for user `{User.Domain}\\{User.Name}`.");

                    if (DWMAPI.IsCompositionEnabled())
                    {
                        if (OS.W81)
                        {
                            DWMAPI.DwmGetColorizationParameters(out DWMAPI.DWM_COLORIZATION_PARAMS colorizationParams);

                            colorizationParams.clrColor = (uint)TM.Windows81.ColorizationColor.ToArgb();
                            colorizationParams.nIntensity = (uint)TM.Windows81.ColorizationColorBalance;

                            DWMAPI.DwmSetColorizationParameters(ref colorizationParams, false);
                        }

                        else if (OS.W8)
                        {
                            DWMAPI.DwmGetColorizationParameters(out DWMAPI.DWM_COLORIZATION_PARAMS colorizationParams);

                            colorizationParams.clrColor = (uint)TM.Windows8.ColorizationColor.ToArgb();
                            colorizationParams.nIntensity = (uint)TM.Windows8.ColorizationColorBalance;

                            DWMAPI.DwmSetColorizationParameters(ref colorizationParams, false);
                        }

                        else if (OS.W7)
                        {
                            DWMAPI.DWM_COLORIZATION_PARAMS temp = new()
                            {
                                clrColor = (uint)TM.Windows7.ColorizationColor.ToArgb(),
                                nIntensity = (uint)TM.Windows7.ColorizationColorBalance,

                                clrAfterGlow = (uint)TM.Windows7.ColorizationAfterglow.ToArgb(),
                                clrAfterGlowBalance = (uint)TM.Windows7.ColorizationAfterglowBalance,

                                clrBlurBalance = (uint)TM.Windows7.ColorizationBlurBalance,
                                clrGlassReflectionIntensity = (uint)TM.Windows7.ColorizationGlassReflectionIntensity,
                                fOpaque = TM.Windows7.VisualStyles.VisualStylesType == Theme.Structures.VisualStyles.DefaultVisualStyles.AeroOpaque
                            };

                            DWMAPI.DwmSetColorizationParameters(ref temp, false);
                        }

                        else if (OS.WVista)
                        {
                            DWMAPI.DwmGetColorizationParameters(out DWMAPI.DWM_COLORIZATION_PARAMS colorizationParams);

                            colorizationParams.clrColor = (uint)TM.WindowsVista.ColorizationColor.ToArgb();
                            colorizationParams.nIntensity = (uint)(TM.WindowsVista.ColorizationColor.A / 255f * 100f);

                            colorizationParams.fOpaque = TM.WindowsVista.VisualStyles.VisualStylesType == Theme.Structures.VisualStyles.DefaultVisualStyles.AeroOpaque;

                            DWMAPI.DwmSetColorizationParameters(ref colorizationParams, false);
                        }
                    }
                    wic.Undo();
                }
            });
        }

        /// <summary>
        /// Restart the Windows Explorer process
        /// </summary>
        /// <param name="treeView"></param>
        public static void RestartExplorer(TreeView treeView = null)
        {
            Program.Log?.Write(LogEventLevel.Information, $"Restarting Windows Explorer process for user `{User.Domain}\\{User.Name}` as non-administrator account.");

            Program.ExplorerKiller?.Start();
            Program.ExplorerKiller?.WaitForExit();

            SendCommand(SysPaths.Explorer, false, true);
        }

        public static bool IsNetworkAvailable
        {
            get
            {
                try
                {
                    if (OS.WXP) return Wininet.CheckNet(); // XP fallback

                    NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
                    foreach (NetworkInterface nic in nics)
                    {
                        if (nic.OperationalStatus == OperationalStatus.Up && nic.NetworkInterfaceType != NetworkInterfaceType.Loopback && nic.NetworkInterfaceType != NetworkInterfaceType.Tunnel)
                        {
                            return Wininet.CheckNet();
                        }
                    }

                    return false;
                }
                catch
                {
                    return Wininet.CheckNet(); // fallback
                }
            }
        }

        private static bool HasNetworkInterfaces
        {
            get
            {
                try
                {
                    if (OS.WXP) return true; // Assume XP has at least 1 interface

                    var interfaces = NetworkInterface.GetAllNetworkInterfaces();
                    return interfaces?.Any(nic => nic.OperationalStatus == OperationalStatus.Up) ?? false;
                }
                catch
                {
                    return true; // fallback
                }
            }
        }

        public static bool IsNetworkOperational
        {
            get
            {
                try
                {
                    if (OS.WXP) return true; // XP fallback

                    var interfaces = NetworkInterface.GetAllNetworkInterfaces();
                    return interfaces?.Any(nic =>
                        nic.OperationalStatus == OperationalStatus.Up &&
                        nic.NetworkInterfaceType != NetworkInterfaceType.Loopback &&
                        nic.NetworkInterfaceType != NetworkInterfaceType.Tunnel) ?? false;
                }
                catch
                {
                    return true; // fallback
                }
            }
        }

        /// <summary>
        /// Ping the given URL to check if it is reachable
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static bool Ping(string url)
        {
            try
            {
                Program.Log?.Write(LogEventLevel.Information, $"Pinging URL: {url}");

                Stopwatch sw = new();
                sw.Reset();  sw.Start();

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Timeout = Timeout;
                request.AllowAutoRedirect = false;
                request.Method = "HEAD";

                using (request.GetResponse())
                {
                    Program.Log?.Write(LogEventLevel.Information, $"Ping to URL `{url}` was successful ({sw.ElapsedMilliseconds} ms).");
                    sw.Stop();
                    return true;
                }
            }
            catch
            {
                Program.Log?.Write(LogEventLevel.Warning, $"Ping to URL `{url}` failed.");
                return false;
            }
        }

        /// <summary>
        /// Get the Windows Screen Scaling Factor
        /// </summary>
        /// <param name="percentage"></param>
        /// <returns></returns>
        public static float GetWindowsScreenScalingFactor(bool percentage = true)
        {
            using (WindowsImpersonationContext wic = User.Identity.Impersonate())
            {
                Program.Log?.Write(LogEventLevel.Information, $"Getting Windows Screen Scaling Factor. Returning as percentage: {percentage}");

                Graphics GraphicsObject = Graphics.FromHwnd(IntPtr.Zero);
                IntPtr DeviceContextHandle = GraphicsObject.GetHdc();
                int LogicalScreenHeight = GDI32.GetDeviceCaps(DeviceContextHandle, (int)GDI32.DeviceCap.VERTRES);
                int PhysicalScreenHeight = GDI32.GetDeviceCaps(DeviceContextHandle, (int)GDI32.DeviceCap.DESKTOPVERTRES);
                float ScreenScalingFactor = PhysicalScreenHeight / (float)LogicalScreenHeight;

                if (percentage) ScreenScalingFactor *= 100.0f;

                GraphicsObject.ReleaseHdc(DeviceContextHandle);
                GraphicsObject.Dispose();

                wic.Undo();

                Program.Log?.Write(LogEventLevel.Information, $"Windows Screen Scaling Factor: {ScreenScalingFactor} (as percentage: {percentage})");
                return ScreenScalingFactor;
            }
        }

        /// <summary>
        /// Exit WinPaletter by force
        /// </summary>
        public static void ForceExit()
        {
            Program.Log?.Write(LogEventLevel.Information, $"Exiting WinPaletter by force.");

            using (WindowsImpersonationContext wic = User.Identity_Admin.Impersonate())
            using (Process process = Process.GetCurrentProcess())
            {
                Environment.ExitCode = 0;
                Forms.MainForm.LoggingOff = true;
                Forms.MainForm.Close();
                process.Kill();
                wic.Undo();  // :)
            }
        }
    }
}