using Microsoft.Win32;
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
using System.Threading.Tasks;
using System.Windows.Forms;
using WinPaletter.NativeMethods;
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

            if (System.IO.File.Exists(path))
            {
                using (MD5 md5 = MD5.Create())
                {
                    byte[] hash = md5.ComputeHash(System.IO.File.ReadAllBytes(path));
                    string result = BitConverter.ToString(hash).Replace("-", string.Empty);
                    MD5_str = result.ToUpper();
                }
            }
            else
            {
                MD5_str = "0";
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
            string fullFilePath = System.IO.Path.Combine(directoryPath, baseFileName);

            if (!System.IO.File.Exists(fullFilePath))
            {
                // The File with the given name does not exist, so it is already unique
                return fullFilePath;
            }

            // If the File exists, generate a unique File name
            int counter = 1;
            string fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(baseFileName);
            string fileExtension = System.IO.Path.GetExtension(baseFileName);

            do
            {
                string uniqueFileName = $"{fileNameWithoutExtension}_{counter}{fileExtension}";
                fullFilePath = System.IO.Path.Combine(directoryPath, uniqueFileName);
                counter++;
            }
            while (System.IO.File.Exists(fullFilePath));

            return fullFilePath;
        }

        /// <summary>
        /// Send a command to Command Prompt as Administrator impersonating the selected user
        /// </summary>
        /// <param name="command"></param>
        /// <param name="Wait"></param>
        public static void SendCommand(string command, bool Wait = true)
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
                    process.Start();
                    process.WaitForExit();
                }

                wic.Undo();
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
            string FileName = System.IO.Path.GetFileNameWithoutExtension(FullPath).ToLower();

            foreach (Process p in Process.GetProcessesByName(FileName))
            {
                if (FullPath.ToLower() == NativeMethods.Kernel32.GetProcessFilename(p).ToLower())
                    processes.Add(p);
            }

            return processes;
        }

        /// <summary>
        /// Load the Theme Manager and set the Window Style for the preview depending on the OS version
        /// </summary>
        private static void LoadThemeManager()
        {
            if (OS.W12)
                WindowStyle = PreviewHelpers.WindowStyle.W12;

            if (OS.W11)
                WindowStyle = PreviewHelpers.WindowStyle.W11;

            else if (OS.W10)
                WindowStyle = PreviewHelpers.WindowStyle.W10;

            else if (OS.W81)
                WindowStyle = PreviewHelpers.WindowStyle.W81;

            else if (OS.W8)
                WindowStyle = PreviewHelpers.WindowStyle.W81;

            else if (OS.W7)
                WindowStyle = PreviewHelpers.WindowStyle.W7;

            else if (OS.WVista)
                WindowStyle = PreviewHelpers.WindowStyle.WVista;

            else if (OS.WXP)
                WindowStyle = PreviewHelpers.WindowStyle.WXP;

            else
                WindowStyle = PreviewHelpers.WindowStyle.W12;

            // Load Manager
            if (!ExternalLink)
            {
                Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Loading Theme Manager from selected user preferences and registry.");

                if (TM is null) TM = new(Theme.Manager.Source.Registry);
                Forms.Home.Text = Application.ProductName;
            }
            else
            {
                Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Loading Theme Manager from File: {ExternalLink_File}");

                TM = new(Theme.Manager.Source.File, ExternalLink_File);
                Forms.Home.File = ExternalLink_File;
                Forms.Home.Text = System.IO.Path.GetFileName(ExternalLink_File);
                ExternalLink = false;
                ExternalLink_File = string.Empty;
            }

            TM_Original = TM.Clone() as Theme.Manager;
            TM_FirstTime = TM.Clone() as Theme.Manager;

            if (Program.Settings.BackupTheme.Enabled && Program.Settings.BackupTheme.AutoBackupOnAppOpen)
            {
                Log?.Write(Serilog.Events.LogEventLevel.Information, $"Creating a backup of the current theme on application open.");

                string filename = Program.GetUniqueFileName($"{Program.Settings.BackupTheme.BackupPath}\\OnAppOpen", $"{TM.Info.ThemeName}_{DateTime.Now.Hour}.{DateTime.Now.Minute}.{DateTime.Now.Second}.wpth");
                TM.Save(Source.File, filename);
            }
        }

        /// <summary>
        /// Delete the old Winpaletter executable files
        /// </summary>
        private static void DeleteUpdateResiduals()
        {
            try
            {
                if (System.IO.File.Exists("oldWinpaletter.trash"))
                {
                    Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Deleting old WinPaletter executable file: oldWinpaletter.trash");
                    System.IO.File.Delete("oldWinpaletter.trash");
                }
                if (System.IO.File.Exists("oldWinpaletter_2.trash"))
                {
                    Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Deleting old WinPaletter executable file: oldWinpaletter_2.trash");
                    System.IO.File.Delete("oldWinpaletter_2.trash");
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
                Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Loading JetBrains Mono font from memory.");

                MemoryFonts.AddMemoryFont(Properties.Resources.JetBrainsMono_Medium);
                Fonts.Console = MemoryFonts.GetFont(0, 7.75f);
                Fonts.ConsoleMedium = MemoryFonts.GetFont(0, 9f);
                Fonts.ConsoleLarge = MemoryFonts.GetFont(0, 10f);
            }
            catch
            {
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
                    Lang.Load(Settings.Language.File);
                }
                catch (Exception ex)
                {
                    Forms.BugReport.ThrowError(ex);
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
                    Log?.Write(Serilog.Events.LogEventLevel.Information, $"The setup has not been completed, WinPaletter will exit.");
                    Program.ForceExit();
                }
            }

            Log?.Write(Serilog.Events.LogEventLevel.Information, $"The setup has not been completed, continuing...");
        }

        /// <summary>
        /// Start monitoiring Windows Wallpaper and Theme changes to adjust the preview accordingly
        /// </summary>
        private static void StartMonitors()
        {
            FirstVisualStyles = UxTheme.GetCurrentVS().Item1 ?? SysPaths.Windows + "\\Resources\\Themes\\aero\\aero.msstyles";

            if (!OS.WXP)
            {
                try { Monitor(); }
                catch (Exception ex)
                {
                    if (MsgBox(Lang.Strings.Messages.MonitorIssue, MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, $"{Lang.Strings.Messages.MonitorIssue2}\r\n{Lang.Strings.ThemeManager.Errors.RestoreCursorsErrorPressOK}") == DialogResult.OK)
                    {
                        Forms.BugReport.ThrowError(ex);
                    }
                }
            }
            else
            {
                SystemEvents.UserPreferenceChanged += OldWinPreferenceChanged;
            }
        }

        /// <summary>
        /// Initialize the Image Lists for the Theme Log and the Language TreeView
        /// </summary>
        private static void InitializeImageLists()
        {
            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Initializing Image Lists for Theme Log and Language TreeView.");

            ImageLists.ThemeLog.Images.Add("info", Assets.Notifications.Info);
            ImageLists.ThemeLog.Images.Add("apply", Assets.Notifications.Applying);
            ImageLists.ThemeLog.Images.Add("error", Assets.Notifications.Error);
            ImageLists.ThemeLog.Images.Add("warning", Assets.Notifications.Warning);
            ImageLists.ThemeLog.Images.Add("time", Assets.Notifications.Time);
            ImageLists.ThemeLog.Images.Add("success", Assets.Notifications.Success);
            ImageLists.ThemeLog.Images.Add("skip", Assets.Notifications.Skip);
            ImageLists.ThemeLog.Images.Add("admin", Assets.Notifications.Administrator);
            ImageLists.ThemeLog.Images.Add("reg_add", Assets.Notifications.Reg_add);
            ImageLists.ThemeLog.Images.Add("reg_delete", Assets.Notifications.Reg_delete);
            ImageLists.ThemeLog.Images.Add("reg_skip", Assets.Notifications.Reg_skip);
            ImageLists.ThemeLog.Images.Add("task_remove", Assets.Notifications.Task_remove);
            ImageLists.ThemeLog.Images.Add("file_rename", Assets.Notifications.File_rename);
            ImageLists.ThemeLog.Images.Add("dll", Assets.Notifications.DLL);
            ImageLists.ThemeLog.Images.Add("pe_patch", Assets.Notifications.PE_patch);
            ImageLists.ThemeLog.Images.Add("pe_backup", Assets.Notifications.PE_backup);
            ImageLists.ThemeLog.Images.Add("pe_restore", Assets.Notifications.PE_restore);

            ImageLists.Language.Images.Add("main", Properties.Resources.LangNode_Main);
            ImageLists.Language.Images.Add("value", Properties.Resources.LangNode_Value);
            ImageLists.Language.Images.Add("json", Properties.Resources.LangNode_JSON);

            Exceptions.ThemeApply.Clear();
            Exceptions.ThemeLoad.Clear();
        }

        /// <summary>
        /// Associate the WinPaletter file types with the application
        /// </summary>
        private static void AssociateFiles()
        {
            if (Settings.FileTypeManagement.AutoAddExt)
            {
                if (!System.IO.Directory.Exists(SysPaths.appData))
                {
                    System.IO.Directory.CreateDirectory(SysPaths.appData);
                    Log?.Write(Serilog.Events.LogEventLevel.Information, $"A new directory has been created: {SysPaths.appData}");
                }

                WriteIfChangedOrNotExists($"{SysPaths.appData}\\fileextension.ico", Properties.Resources.fileextension.ToBytes());
                WriteIfChangedOrNotExists($"{SysPaths.appData}\\settingsfile.ico", Properties.Resources.settingsfile.ToBytes());
                WriteIfChangedOrNotExists($"{SysPaths.appData}\\themerespack.ico", Properties.Resources.ThemesResIcon.ToBytes());

                bool assoc0 = CreateFileAssociation(".wpth", "WinPaletter.ThemeFile", Lang.Strings.Extensions.WinPaletterTheme, $@"{SysPaths.appData}\fileextension.ico", Assembly.GetExecutingAssembly().Location);
                bool assoc1 = CreateFileAssociation(".wpsf", "WinPaletter.SettingsFile", Lang.Strings.Extensions.WinPaletterSettings, $@"{SysPaths.appData}\settingsfile.ico", Assembly.GetExecutingAssembly().Location);
                bool assoc2 = CreateFileAssociation(".wptp", "WinPaletter.ThemeResourcesPack", Lang.Strings.Extensions.WinPaletterResourcesPack, $@"{SysPaths.appData}\themerespack.ico", Assembly.GetExecutingAssembly().Location);

                if (!assoc0 || !assoc1 || !assoc2)
                {
                    // Notify Windows that File associations have changed
                    Shell32.SHChangeNotify(NativeMethods.Shell32.ShellConstants.SHCNE_ASSOCCHANGED, NativeMethods.Shell32.ShellConstants.SHCNF_IDLIST, 0, 0);
                    Log?.Write(Serilog.Events.LogEventLevel.Information, $"File associations have been updated.");
                }
            }
        }

        /// <summary>
        /// Write the given data to the file if the file does not exist or the data is different (To avoid unnecessary writes, disk usage, and SSD wear)
        /// </summary>
        /// <param name="file"></param>
        /// <param name="data"></param>
        private static void WriteIfChangedOrNotExists(string file, byte[] data)
        {
            if (!System.IO.File.Exists(file))
            {
                Log?.Write(Serilog.Events.LogEventLevel.Information, $"A new file has been created: {file}");
                System.IO.File.WriteAllBytes(file, data);
            }
            else if (!System.IO.File.ReadAllBytes(file).Equals_Method2(data))
            {
                Log?.Write(Serilog.Events.LogEventLevel.Information, $"A file has been updated: {file}");
                System.IO.File.WriteAllBytes(file, data);
            }
        }

        /// <summary>
        /// Extract the Luna theme from the resources to the Themes directory to be used for Windows XP preview
        /// </summary>
        private static void ExtractLuna()
        {
            try
            {
                if (!System.IO.Directory.Exists(SysPaths.MSTheme_Dir))
                {
                    System.IO.Directory.CreateDirectory(SysPaths.MSTheme_Dir);
                    Log?.Write(Serilog.Events.LogEventLevel.Information, $"A new directory has been created: {SysPaths.MSTheme_Dir}");
                }

                WriteIfChangedOrNotExists(SysPaths.MSTheme_ZIP, Properties.Resources.luna);

                using (System.IO.FileStream archiveStream = new(SysPaths.MSTheme_ZIP, System.IO.FileMode.Open, System.IO.FileAccess.Read))
                using (ZipArchive zip = new(archiveStream, System.IO.Compression.ZipArchiveMode.Read))
                {
                    foreach (ZipArchiveEntry entry in zip.Entries)
                    {
                        string destinationPath = System.IO.Path.Combine(SysPaths.MSTheme_Dir, entry.FullName);

                        if (entry.FullName.Contains("\\"))
                        {
                            string destDir = destinationPath.Replace($"\\{destinationPath.Split('\\').Last()}", string.Empty);
                            if (!System.IO.Directory.Exists(destDir))
                            {
                                System.IO.Directory.CreateDirectory(destDir);
                                Log?.Write(Serilog.Events.LogEventLevel.Information, $"A new directory has been created: {destDir}");
                            }
                        }

                        using (Stream entryStream = entry.Open())
                        using (MemoryStream ms = new())
                        {
                            entryStream.CopyTo(ms, 4096 * 32);
                            byte[] entryBytes = ms.ToArray();

                            if (System.IO.File.Exists(destinationPath))
                            {
                                byte[] existingBytes = System.IO.File.ReadAllBytes(destinationPath);

                                if (!existingBytes.Equals_Method2(entryBytes))
                                {
                                    System.IO.File.WriteAllBytes(destinationPath, entryBytes);
                                    Log?.Write(Serilog.Events.LogEventLevel.Information, $"A file has been updated: {destinationPath}");
                                }
                            }
                            else
                            {
                                System.IO.File.WriteAllBytes(destinationPath, entryBytes);
                                Log?.Write(Serilog.Events.LogEventLevel.Information, $"A file has been extracted: {destinationPath}");
                            }
                        }
                    }

                    archiveStream.Close();
                }

                System.IO.File.WriteAllText(SysPaths.MSTheme_Luna_theme, $"[VisualStyles]{"\r\n"}Path={$@"{SysPaths.appData}\VisualStyles\Luna\luna.msstyles"}{"\r\n"}ColorStyle=NormalColor{"\r\n"}Size=NormalSize");
            }
            catch (Exception ex)
            {
                Forms.BugReport.ThrowError(ex);
            }
        }

        /// <summary>
        /// Backup the Windows Startup Sound from imageres.dll to the AppData directory
        /// </summary>
        private static void BackupWindowsStartupSound()
        {
            try
            {
                if (!OS.WXP && !System.IO.File.Exists($@"{SysPaths.appData}\WindowsStartup_Backup.wav"))
                {
                    byte[] SoundBytes = PE.GetResource(SysPaths.imageres, "WAVE", OS.WVista ? 5051 : 5080);
                    System.IO.File.WriteAllBytes($@"{SysPaths.appData}\WindowsStartup_Backup.wav", SoundBytes);
                    Program.Log.Information($"Windows startup sound has been backed-up: {SysPaths.appData}\\WindowsStartup_Backup.wav");
                }
            }
            catch (Exception ex)
            {
                Forms.BugReport.ThrowError(ex);
            }
        }

        /// <summary>
        /// Update SysEventsSounds service
        /// </summary>
        public static void UpdateSysEventsSounds()
        {
            if (!Program.UninstallDone)
            {
                if (System.IO.File.Exists(SysPaths.SysEventsSounds) && !Properties.Resources.WinPaletter_SysEventsSounds.Equals_Method2(System.IO.File.ReadAllBytes(SysPaths.SysEventsSounds)))
                {
                    //Update
                    if (Settings.UsersServices.ShowSysEventsSoundsInstaller)
                    {
                        Log?.Write(Serilog.Events.LogEventLevel.Information, $"SysEventsSounds service is not up to date, updating it.");
                        Forms.SysEventsSndsInstaller.Install(false);
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// Check if the new version of application is running for the first time, and show the What'archiveStream New pop-up if it is
        /// </summary>
        public static void CheckWhatsNew()
        {
            if (!Settings.General.WhatsNewRecord.Contains(Version))
            {
                Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"This application version is running for the first time: {Version}");

                // Display the What'archiveStream New pop-up
                ShowWhatsNew = true;

                // Update version information
                List<string> versionHistory = [Version, .. Settings.General.WhatsNewRecord];
                versionHistory = versionHistory.Distinct().ToList();
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
        public static void RefreshDWM(Theme.Manager TM)
        {
            Task.Run(() =>
            {
                using (WindowsImpersonationContext wic = User.Identity.Impersonate())
                {
                    if (DWMAPI.IsCompositionEnabled())
                    {
                        if (OS.W8x)
                        {
                            DWMAPI.DWM_COLORIZATION_PARAMS temp = new()
                            {
                                clrColor = (uint)TM.Windows81.ColorizationColor.ToArgb(),
                                nIntensity = (uint)TM.Windows81.ColorizationColorBalance
                            };
                            DWMAPI.DwmSetColorizationParameters(ref temp, false);
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
                                fOpaque = TM.Windows7.Theme == Theme.Structures.Windows7.Themes.AeroOpaque
                            };
                            DWMAPI.DwmSetColorizationParameters(ref temp, false);
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
            try
            {
                if (User.SID == User.UserSID_OpenedWP && User.SID == User.AdminSID_GrantedUAC)
                {
                    if (treeView is not null) { ThemeLog.AddNode(treeView, $"{DateTime.Now.ToLongTimeString()}: {Program.Lang.Strings.ThemeManager.Actions.KillingExplorer}", "info"); }

                    Stopwatch sw = new();
                    sw.Reset();
                    sw.Start();

                    Program.ExplorerKiller.Start();
                    Program.ExplorerKiller.WaitForExit();
                    Program.Explorer_exe.Start();

                    sw.Stop();

                    if (treeView is not null) { ThemeLog.AddNode(treeView, $"{DateTime.Now.ToLongTimeString()}: {string.Format(Program.Lang.Strings.ThemeManager.Actions.ExplorerRestarted, sw.ElapsedMilliseconds / 1000d)}", "time"); }

                    sw.Reset();
                }
                else
                {
                    if (treeView is not null)
                    {
                        ThemeLog.AddNode(treeView, $"{Program.Lang.Strings.Messages.RestartExplorerIssue0}. {Program.Lang.Strings.Messages.RestartExplorerIssue1}", "warning");
                    }
                    else
                    {
                        MsgBox(Program.Lang.Strings.Messages.RestartExplorerIssue0, MessageBoxButtons.OK, MessageBoxIcon.Warning, Program.Lang.Strings.Messages.RestartExplorerIssue1);
                    }
                }
            }
            catch (Exception ex)
            {
                if (treeView is not null)
                {
                    ThemeLog.AddNode(treeView, $"{DateTime.Now.ToLongTimeString()}: {Program.Lang.Strings.ThemeManager.Errors.ExplorerRestart}", "error");
                    Exceptions.ThemeApply.Add(new Tuple<string, Exception>(Program.Lang.Strings.ThemeManager.Errors.ExplorerRestart, ex));
                }
                else
                {
                    Forms.BugReport.ThrowError(ex);
                }
            }
        }

        /// <summary>
        /// Indicates whether any network connection is available
        /// </summary>
        /// <returns>
        ///    <c>true</c> if a network connection is available; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNetworkAvailable
        {
            get
            {
                return IsNetworkOperational && HasNetworkInterfaces && Wininet.CheckNet();
            }
        }

        private static bool HasNetworkInterfaces
        {
            get
            {
                try
                {
                    var interfaces = System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces();
                    return interfaces.Any(nic => nic.OperationalStatus == System.Net.NetworkInformation.OperationalStatus.Up);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Interface check failed: " + ex.Message);
                    return false;
                }
            }
        }

        /// <summary>
        /// Checks if network stack is available (to avoid dead network socket exception, especially in safe mode without networking).
        /// </summary>
        public static bool IsNetworkOperational
        {
            get
            {
                try
                {
                    var interfaces = NetworkInterface.GetAllNetworkInterfaces();
                    return interfaces.Any(nic =>
                        nic.OperationalStatus == OperationalStatus.Up &&
                        nic.NetworkInterfaceType != NetworkInterfaceType.Loopback &&
                        nic.NetworkInterfaceType != NetworkInterfaceType.Tunnel);
                }
                catch
                {
                    return false;
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
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Timeout = Timeout;
                request.AllowAutoRedirect = false;
                request.Method = "HEAD";

                using (request.GetResponse())
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Get the Windows Screen Scaling Factor
        /// </summary>
        /// <param name="percentage"></param>
        /// <returns></returns>
        public static double GetWindowsScreenScalingFactor(bool percentage = true)
        {
            using (WindowsImpersonationContext wic = User.Identity.Impersonate())
            {
                Graphics GraphicsObject = Graphics.FromHwnd(IntPtr.Zero);
                IntPtr DeviceContextHandle = GraphicsObject.GetHdc();
                int LogicalScreenHeight = GDI32.GetDeviceCaps(DeviceContextHandle, (int)GDI32.DeviceCap.VERTRES);
                int PhysicalScreenHeight = GDI32.GetDeviceCaps(DeviceContextHandle, (int)GDI32.DeviceCap.DESKTOPVERTRES);
                double ScreenScalingFactor = Math.Round(PhysicalScreenHeight / (double)LogicalScreenHeight, 2);

                if (percentage)
                    ScreenScalingFactor *= 100.0d;

                GraphicsObject.ReleaseHdc(DeviceContextHandle);
                GraphicsObject.Dispose();

                wic.Undo();
                return ScreenScalingFactor;
            }
        }

        /// <summary>
        /// Exit WinPaletter by force
        /// </summary>
        public static void ForceExit()
        {
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