using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinPaletter.NativeMethods;
using static WinPaletter.Theme.Manager;

namespace WinPaletter
{
    internal partial class Program
    {
        public static string GetUniqueFileName(string directoryPath, string baseFileName)
        {
            string fullFilePath = System.IO.Path.Combine(directoryPath, baseFileName);

            if (!System.IO.File.Exists(fullFilePath))
            {
                // The file with the given name does not exist, so it is already unique
                return fullFilePath;
            }

            // If the file exists, generate a unique file name
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

        public static List<Process> ProgramsRunning(string FullPath)
        {
            List<Process> processes = new();
            string FileName = System.IO.Path.GetFileNameWithoutExtension(FullPath).ToLower();

            foreach (Process p in Process.GetProcessesByName(FileName))
            {
                if (FullPath.ToLower() == NativeMethods.Kernel32.GetProcessFilename(p).ToLower())
                    processes.Add(p);
            }

            return processes;
        }

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
                if (TM is null) TM = new(Theme.Manager.Source.Registry);
            }
            else
            {
                TM = new(Theme.Manager.Source.File, ExternalLink_File);
                Forms.Home.file = ExternalLink_File;
                ExternalLink = false;
                ExternalLink_File = string.Empty;
            }

            TM_Original = TM.Clone() as Theme.Manager;
            TM_FirstTime = TM.Clone() as Theme.Manager;

            if (Program.Settings.BackupTheme.Enabled && Program.Settings.BackupTheme.AutoBackupOnAppOpen)
            {
                string filename = Program.GetUniqueFileName($"{Program.Settings.BackupTheme.BackupPath}\\OnAppOpen", $"{TM.Info.ThemeName}_{DateTime.Now.Hour}.{DateTime.Now.Minute}.{DateTime.Now.Second}.wpth");
                TM.Save(Source.File, filename);
            }
        }

        private static void DeleteUpdateResiduals()
        {
            try
            {
                if (System.IO.File.Exists("oldWinpaletter.trash")) System.IO.File.Delete("oldWinpaletter.trash");
                if (System.IO.File.Exists("oldWinpaletter_2.trash")) System.IO.File.Delete("oldWinpaletter_2.trash");
            }
            catch { }
        }

        private static void GetMemoryFonts()
        {
            try
            {
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

        public static void CheckIfLicenseIsAccepted()
        {
            if (!Settings.General.LicenseAccepted)
            {
                if (Forms.LicenseForm.ShowDialog() != DialogResult.OK) Program.ForceExit();
            }
        }

        private static void StartWallpaperMonitor()
        {
            if (!OS.WXP)
            {
                try { Monitor(); }
                catch (Exception ex)
                {
                    if (MsgBox(Lang.MonitorIssue, MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, $"{Lang.MonitorIssue2}\r\n{Lang.TM_RestoreCursorsErrorPressOK}") == DialogResult.OK)
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

        private static void InitializeImageLists()
        {
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

        private static void AssociateFiles()
        {
            try
            {
                if (Settings.FileTypeManagement.AutoAddExt)
                {
                    if (!System.IO.Directory.Exists(PathsExt.appData))
                        System.IO.Directory.CreateDirectory(PathsExt.appData);

                    WriteIfChangedOrNotExists($@"{PathsExt.appData}\fileextension.ico", Properties.Resources.fileextension.ToByteArray());
                    WriteIfChangedOrNotExists($@"{PathsExt.appData}\settingsfile.ico", Properties.Resources.settingsfile.ToByteArray());
                    WriteIfChangedOrNotExists($@"{PathsExt.appData}\themerespack.ico", Properties.Resources.ThemesResIcon.ToByteArray());

                    CreateFileAssociation(".wpth", "WinPaletter.ThemeFile", Lang.WP_Theme_FileType, $@"{PathsExt.appData}\fileextension.ico", Assembly.GetExecutingAssembly().Location);
                    CreateFileAssociation(".wpsf", "WinPaletter.SettingsFile", Lang.WP_Settings_FileType, $@"{PathsExt.appData}\settingsfile.ico", Assembly.GetExecutingAssembly().Location);
                    CreateFileAssociation(".wptp", "WinPaletter.ThemeResourcesPack", Lang.WP_ResourcesPack_FileType, $@"{PathsExt.appData}\themerespack.ico", Assembly.GetExecutingAssembly().Location);
                }
            }
            catch { }
        }

        private static void WriteIfChangedOrNotExists(string file, byte[] data)
        {
            if (!System.IO.File.Exists(file))
            {
                System.IO.File.WriteAllBytes(file, data);
            }
            else if (!System.IO.File.ReadAllBytes(file).Equals_Method2(data))
            {
                System.IO.File.WriteAllBytes(file, data);
            }
        }

        private static void ExtractLuna()
        {
            try
            {
                if (!System.IO.Directory.Exists(PathsExt.MSTheme_Dir))
                {
                    System.IO.Directory.CreateDirectory(PathsExt.MSTheme_Dir);
                }

                WriteIfChangedOrNotExists(PathsExt.MSTheme_ZIP, Properties.Resources.luna);

                using (System.IO.FileStream s = new(PathsExt.MSTheme_ZIP, System.IO.FileMode.Open, System.IO.FileAccess.Read))
                {
                    using (System.IO.Compression.ZipArchive z = new(s, System.IO.Compression.ZipArchiveMode.Read))
                    {
                        foreach (System.IO.Compression.ZipArchiveEntry entry in z.Entries)
                        {
                            if (entry.FullName.Contains(@"\"))
                            {
                                string dest = System.IO.Path.Combine(PathsExt.MSTheme_Dir, entry.FullName);
                                string dest_dir = dest.Replace($@"\{dest.Split('\\').Last()}", string.Empty);

                                if (!System.IO.Directory.Exists(dest_dir))
                                {
                                    System.IO.Directory.CreateDirectory(dest_dir);
                                }
                            }
                            entry.ExtractToFile(System.IO.Path.Combine(PathsExt.MSTheme_Dir, entry.FullName), true);
                        }
                    }
                    s.Close();
                }
                System.IO.File.WriteAllText(PathsExt.MSTheme_Luna_theme, $"[VisualStyles]{"\r\n"}Path={$@"{PathsExt.appData}\VisualStyles\Luna\luna.msstyles"}{"\r\n"}ColorStyle=NormalColor{"\r\n"}Size=NormalSize");
            }
            catch (Exception ex)
            {
                Forms.BugReport.ThrowError(ex);
            }
        }

        private static void BackupWindowsStartupSound()
        {
            try
            {
                if (!OS.WXP && !System.IO.File.Exists($@"{PathsExt.appData}\WindowsStartup_Backup.wav"))
                {
                    byte[] SoundBytes = PE.GetResource(PathsExt.imageres, "WAVE", OS.WVista ? 5051 : 5080);
                    System.IO.File.WriteAllBytes($@"{PathsExt.appData}\WindowsStartup_Backup.wav", SoundBytes);
                }
            }
            catch (Exception ex)
            {
                Forms.BugReport.ThrowError(ex);
            }
        }

        public static void UpdateSysEventsSounds()
        {
            if (!Program.UninstallDone)
            {
                if (System.IO.File.Exists(PathsExt.SysEventsSounds) && !Properties.Resources.WinPaletter_SysEventsSounds.Equals_Method2(System.IO.File.ReadAllBytes(PathsExt.SysEventsSounds)))
                {
                    //Update
                    if (Settings.UsersServices.ShowSysEventsSoundsInstaller)
                    {
                        Forms.SysEventsSndsInstaller.Install(false);
                        return;
                    }
                }
            }
        }

        public static void CheckWhatsNew()
        {
            if (!Settings.General.WhatsNewRecord.Contains(Version))
            {
                // Display the What's New pop-up
                ShowWhatsNew = true;

                // Update version information
                List<string> versionHistory = new() { Version };
                versionHistory.AddRange(Settings.General.WhatsNewRecord);
                versionHistory = versionHistory.Distinct().ToList();
                Settings.General.WhatsNewRecord = versionHistory.ToArray();

                // Save updated settings
                Settings.General.Save();
            }
            else
            {
                ShowWhatsNew = false;
            }
        }

        public static void RefreshDWM(Theme.Manager TM)
        {
            Task.Run(() =>
            {
                try
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

                            else if (OS.WVista)
                            {
                                const int WM_DWMCOLORIZATIONCOLORCHANGED = 0x0320;
                                uint color = (uint)Color.FromArgb(TM.WindowsVista.Alpha, TM.WindowsVista.ColorizationColor).ToArgb();
                                IntPtr handle = IntPtr.Zero;
                                while ((handle = User32.FindWindow(null, null)) != IntPtr.Zero)
                                {
                                    User32.SendMessage(handle, WM_DWMCOLORIZATIONCOLORCHANGED, IntPtr.Zero, (IntPtr)color);
                                }
                            }
                        }
                        wic.Undo();
                    }
                }
                catch { }
            });
        }

        public static void RestartExplorer(TreeView TreeView = null)
        {
            try
            {
                if (User.SID == User.UserSID_OpenedWP && User.SID == User.AdminSID_GrantedUAC)
                {
                    if (TreeView is not null) { Theme.Manager.AddNode(TreeView, $"{DateTime.Now.ToLongTimeString()}: {Program.Lang.KillingExplorer}", "info"); }

                    Stopwatch sw = new();
                    sw.Reset();
                    sw.Start();

                    Program.ExplorerKiller.Start();
                    Program.ExplorerKiller.WaitForExit();
                    Program.Explorer_exe.Start();

                    sw.Stop();

                    if (TreeView is not null) { Theme.Manager.AddNode(TreeView, $"{DateTime.Now.ToLongTimeString()}: {(string.Format(Program.Lang.ExplorerRestarted, sw.ElapsedMilliseconds / 1000d))}", "time"); }

                    sw.Reset();
                }
                else
                {
                    if (TreeView is not null)
                    {
                        Theme.Manager.AddNode(TreeView, $"{Program.Lang.RestartExplorerIssue0}. {Program.Lang.RestartExplorerIssue1}", "warning");
                    }
                    else
                    {
                        MsgBox(Program.Lang.RestartExplorerIssue0, MessageBoxButtons.OK, MessageBoxIcon.Warning, Program.Lang.RestartExplorerIssue1);
                    }
                }
            }
            catch (Exception ex)
            {
                if (TreeView is not null)
                {
                    Theme.Manager.AddNode(TreeView, $"{DateTime.Now.ToLongTimeString()}: {Program.Lang.ErrorExplorerRestart}", "error");
                    Exceptions.ThemeApply.Add(new Tuple<string, Exception>(Program.Lang.ErrorExplorerRestart, ex));
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
            get => Wininet.CheckNet();
        }

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
                Forms.Home.LoggingOff = true;
                Forms.MainForm.Close();
                process.Kill();
                wic.Undo();  // :)
            }
        }
    }
}