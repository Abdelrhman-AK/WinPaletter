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

namespace WinPaletter
{
    internal partial class Program
    {
        public static void SendCommand(string command, bool Wait = true)
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
                PreviewStyle = PreviewHelpers.WindowStyle.W12;

            if (OS.W11)
                PreviewStyle = PreviewHelpers.WindowStyle.W11;

            else if (OS.W10)
                PreviewStyle = PreviewHelpers.WindowStyle.W10;

            else if (OS.W81)
                PreviewStyle = PreviewHelpers.WindowStyle.W81;

            else if (OS.W8)
                PreviewStyle = PreviewHelpers.WindowStyle.W81;

            else if (OS.W7)
                PreviewStyle = PreviewHelpers.WindowStyle.W7;

            else if (OS.WVista)
                PreviewStyle = PreviewHelpers.WindowStyle.WVista;

            else if (OS.WXP)
                PreviewStyle = PreviewHelpers.WindowStyle.WXP;

            else
                PreviewStyle = PreviewHelpers.WindowStyle.W12;

            // Load Manager
            if (!ExternalLink)
            {
                TM = new(Theme.Manager.Source.Registry);
            }
            else
            {
                TM = new(Theme.Manager.Source.File, ExternalLink_File);
                Forms.MainFrm.OpenFileDialog1.FileName = ExternalLink_File;
                Forms.MainFrm.SaveFileDialog1.FileName = ExternalLink_File;
                ExternalLink = false;
                ExternalLink_File = string.Empty;
            }

            TM_Original = (Theme.Manager)TM.Clone();
            TM_FirstTime = (Theme.Manager)TM.Clone();
        }

        private static void DeleteUpdateResiduals()
        {
            try
            {
                if (System.IO.File.Exists("oldWinpaletter.trash"))
                    System.IO.File.Delete("oldWinpaletter.trash");
                if (System.IO.File.Exists("oldWinpaletter_2.trash"))
                    System.IO.File.Delete("oldWinpaletter_2.trash");
            }
            catch
            {
            }
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

        public static void CMD_Convert(string arg, bool KillProcessAfterConvert)
        {
            try
            {
                string[] arr = arg.Remove(0, "/convert:".Count()).Split('|');
                string Source = arr[0];
                string Destination = arr[1];
                string Compress = Settings.FileTypeManagement.CompressThemeFile ? "1" : "0";
                string OldWPTH = "0";
                if (arr.Count() == 3)
                    Compress = arr[2];
                if (arr.Count() == 4)
                    OldWPTH = arr[3];

                Converter _Convert = new();

                if (System.IO.File.Exists(Source) && !(_Convert.GetFormat(Source) == Converter_CP.WP_Format.Error))
                {
                    _Convert.Convert(Source, Destination, Compress == "1", OldWPTH == "1");
                }
                else
                {
                    MsgBox(Lang.Convert_Error_Phrasing, MessageBoxButtons.OK, MessageBoxIcon.Error, Source);
                }
            }

            catch (Exception ex)
            {
                Forms.BugReport.ThrowError(ex);
            }

            if (KillProcessAfterConvert)
            {
                using Process Prc = Process.GetCurrentProcess();
                Prc.Kill();
            }
        }

        public static void CMD_Convert_List(string arg, bool KillProcessAfterConvert)
        {
            try
            {
                string source = arg.Remove(0, "/convert-list:".Count());
                Converter _Convert = new();

                if (System.IO.File.Exists(source))
                {
                    foreach (string File in System.IO.File.ReadAllLines(source))
                    {
                        string f;
                        string compress = Settings.FileTypeManagement.CompressThemeFile ? "1" : "0";
                        string OldWPTH = "0";

                        if (!string.IsNullOrWhiteSpace(File))
                        {
                            if (!File.Contains("|"))
                            {
                                f = File.Replace("\"", string.Empty);
                            }
                            else
                            {
                                string[] arr = File.Split('|');
                                f = arr[0].Replace("\"", string.Empty);
                                if (arr.Count() == 2)
                                    compress = arr[1];
                                if (arr.Count() == 3)
                                    compress = arr[2];
                            }

                            System.IO.FileInfo FI = new(f);
                            string Name = System.IO.Path.GetFileNameWithoutExtension(FI.Name);
                            string Dir = FI.FullName.Replace(FI.FullName.Split('\\').Last(), "WinPaletterConversion");
                            string SaveAs = Dir + @"\" + Name + ".wpth";

                            if (!(_Convert.GetFormat(f) == Converter_CP.WP_Format.Error))
                            {
                                if (!System.IO.Directory.Exists(Dir))
                                    System.IO.Directory.CreateDirectory(Dir);
                                _Convert.Convert(f, SaveAs, compress == "1", OldWPTH == "1");
                            }
                            else
                            {
                                MsgBox(Lang.Convert_Error_Phrasing, MessageBoxButtons.OK, MessageBoxIcon.Error, f);
                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                Forms.BugReport.ThrowError(ex);
            }

            if (KillProcessAfterConvert)
            {
                using Process Prc = Process.GetCurrentProcess();
                Prc.Kill();
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

        public static void CheckIfLicenseChecked()
        {
            if (!Settings.General.LicenseAccepted)
            {
                if (Forms.LicenseForm.ShowDialog() != DialogResult.OK)
                {
                    using Process Prc = Process.GetCurrentProcess();
                    Prc.Kill();
                }
            }
        }

        private static void StartWallpaperMonitor()
        {
            if (!OS.WXP)
            {
                try
                {
                    Monitor();
                }
                catch (Exception ex)
                {
                    if (MsgBox(Lang.MonitorIssue, MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, Lang.MonitorIssue2 + "\r\n" + Lang.TM_RestoreCursorsErrorPressOK) == DialogResult.OK)
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
            ImageLists.ThemeLog.Images.Add("info", Properties.Resources.notify_info);
            ImageLists.ThemeLog.Images.Add("apply", Properties.Resources.notify_applying);
            ImageLists.ThemeLog.Images.Add("error", Properties.Resources.notify_error);
            ImageLists.ThemeLog.Images.Add("warning", Properties.Resources.notify_warning);
            ImageLists.ThemeLog.Images.Add("time", Properties.Resources.notify_time);
            ImageLists.ThemeLog.Images.Add("success", Properties.Resources.notify_success);
            ImageLists.ThemeLog.Images.Add("skip", Properties.Resources.notify_skip);
            ImageLists.ThemeLog.Images.Add("admin", Properties.Resources.notify_administrator);
            ImageLists.ThemeLog.Images.Add("reg_add", Properties.Resources.notify_reg_add);
            ImageLists.ThemeLog.Images.Add("reg_delete", Properties.Resources.notify_reg_delete);
            ImageLists.ThemeLog.Images.Add("reg_skip", Properties.Resources.notify_reg_skip);
            ImageLists.ThemeLog.Images.Add("task_add", Properties.Resources.notify_task_add);
            ImageLists.ThemeLog.Images.Add("task_remove", Properties.Resources.notify_task_remove);
            ImageLists.ThemeLog.Images.Add("file_rename", Properties.Resources.notify_file_rename);
            ImageLists.ThemeLog.Images.Add("dll", Properties.Resources.notify_dll);
            ImageLists.ThemeLog.Images.Add("pe_patch", Properties.Resources.notify_pe_patch);
            ImageLists.ThemeLog.Images.Add("pe_backup", Properties.Resources.notify_pe_backup);
            ImageLists.ThemeLog.Images.Add("pe_restore", Properties.Resources.notify_pe_restore);

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

                    WriteIfChangedOrNotExists(PathsExt.appData + @"\fileextension.ico", Properties.Resources.fileextension.ToByteArray());
                    WriteIfChangedOrNotExists(PathsExt.appData + @"\settingsfile.ico", Properties.Resources.settingsfile.ToByteArray());
                    WriteIfChangedOrNotExists(PathsExt.appData + @"\themerespack.ico", Properties.Resources.ThemesResIcon.ToByteArray());

                    CreateFileAssociation(".wpth", "WinPaletter.ThemeFile", Lang.WP_Theme_FileType, PathsExt.appData + @"\fileextension.ico", Assembly.GetExecutingAssembly().Location);
                    CreateFileAssociation(".wpsf", "WinPaletter.SettingsFile", Lang.WP_Settings_FileType, PathsExt.appData + @"\settingsfile.ico", Assembly.GetExecutingAssembly().Location);
                    CreateFileAssociation(".wptp", "WinPaletter.ThemeResourcesPack", Lang.WP_ResourcesPack_FileType, PathsExt.appData + @"\themerespack.ico", Assembly.GetExecutingAssembly().Location);
                }
            }
            catch
            {
            }
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

        private static void DetectIfWPStartedWithClassicTheme()
        {
            StartedWithClassicTheme = string.IsNullOrEmpty(UxTheme.GetCurrentVS().Item1);
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
                                string dest_dir = dest.Replace(@"\" + dest.Split('\\').Last(), string.Empty);

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
                System.IO.File.WriteAllText(PathsExt.MSTheme_Luna_theme, string.Format("[VisualStyles]{1}Path={0}{1}ColorStyle=NormalColor{1}Size=NormalSize", PathsExt.appData + @"\VisualStyles\Luna\luna.msstyles", "\r\n"));
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
                if (!OS.WXP && !System.IO.File.Exists(PathsExt.appData + @"\WindowsStartup_Backup.wav"))
                {
                    byte[] SoundBytes = PE.GetResource(PathsExt.imageres, "WAVE", OS.WVista ? 5051 : 5080);
                    System.IO.File.WriteAllBytes(PathsExt.appData + @"\WindowsStartup_Backup.wav", SoundBytes);
                }
            }
            catch (Exception ex)
            {
                Forms.BugReport.ThrowError(ex);
            }
        }

        public static void InitializeSysEventsSounds(bool ForceUpdate = false)
        {
            bool condition0 = !System.IO.File.Exists(PathsExt.SysEventsSounds);
            bool condition1 = !condition0 && !Properties.Resources.WinPaletter_SysEventsSounds.Equals_Method2(System.IO.File.ReadAllBytes(PathsExt.SysEventsSounds));

            if (ForceUpdate || condition1)
            {
                //Update
                if (Settings.UsersServices.ShowSysEventsSoundsInstaller)
                    Forms.SysEventsSndsInstaller.Install(false);
                else
                    Forms.SysEventsSndsInstaller.Setup();
            }

            if (condition0)
            {
                //Install
                if (Settings.UsersServices.ShowSysEventsSoundsInstaller)
                    Forms.SysEventsSndsInstaller.Install(true);
            }
        }

        public static void CheckWhatsNew()
        {
            if (!Settings.General.WhatsNewRecord.Contains(Version))
            {
                // ### Pop up WhatsNew
                ShowWhatsNew = true;

                List<string> ver = new();
                ver.Clear();
                ver.Add(Version);

                foreach (string X in Settings.General.WhatsNewRecord.ToArray())
                    ver.Add(X);

                ver = ver.DeDuplicate();
                Settings.General.WhatsNewRecord = ver.ToArray();
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
                            if (OS.W8 || OS.W81)
                            {
                                DWMAPI.DWM_COLORIZATION_PARAMS temp = new();
                                temp.clrColor = (uint)TM.Windows81.ColorizationColor.ToArgb();
                                temp.nIntensity = (uint)TM.Windows81.ColorizationColorBalance;
                                DWMAPI.DwmSetColorizationParameters(ref temp, false);
                            }

                            else if (OS.W7)
                            {
                                DWMAPI.DWM_COLORIZATION_PARAMS temp = new();
                                temp.clrColor = (uint)TM.Windows7.ColorizationColor.ToArgb();
                                temp.nIntensity = (uint)TM.Windows7.ColorizationColorBalance;

                                temp.clrAfterGlow = (uint)TM.Windows7.ColorizationAfterglow.ToArgb();
                                temp.clrAfterGlowBalance = (uint)TM.Windows7.ColorizationAfterglowBalance;

                                temp.clrBlurBalance = (uint)TM.Windows7.ColorizationBlurBalance;
                                temp.clrGlassReflectionIntensity = (uint)TM.Windows7.ColorizationGlassReflectionIntensity;
                                temp.fOpaque = TM.Windows7.Theme == Theme.Structures.Windows7.Themes.AeroOpaque;
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
            {
                try
                {
                    if (TreeView is not null)
                        Theme.Manager.AddNode(TreeView, string.Format("{0}: {1}", DateTime.Now.ToLongTimeString(), Program.Lang.KillingExplorer), "info");
                    Stopwatch sw = new();
                    sw.Reset();
                    sw.Start();

                    Program.ExplorerKiller.Start();
                    Program.ExplorerKiller.WaitForExit();
                    Program.Explorer_exe.Start();

                    sw.Stop();
                    if (TreeView is not null)
                        Theme.Manager.AddNode(TreeView, string.Format("{0}: {1}", DateTime.Now.ToLongTimeString(), string.Format(Program.Lang.ExplorerRestarted, sw.ElapsedMilliseconds / 1000d)), "time");
                    sw.Reset();
                }
                catch (Exception ex)
                {
                    if (TreeView is not null)
                    {
                        Theme.Manager.AddNode(TreeView, string.Format("{0}: {1}", DateTime.Now.ToLongTimeString(), Program.Lang.ErrorExplorerRestart), "error");
                        Exceptions.ThemeApply.Add(new Tuple<string, Exception>(Program.Lang.ErrorExplorerRestart, ex));
                    }
                }

            }
        }

        /// <summary>
        /// Indicates whether any network connection is available
        /// </summary>
        /// <returns>
        ///    <c>true</c> if a network connection is available; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNetworkAvailable()
        {
            return Wininet.CheckNet();
        }

        public static bool Ping(string url)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Timeout = 60000;
                request.AllowAutoRedirect = false;
                request.Method = "HEAD";

                using (var response = request.GetResponse())
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
                var GraphicsObject = Graphics.FromHwnd(IntPtr.Zero);
                var DeviceContextHandle = GraphicsObject.GetHdc();
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
    }
}