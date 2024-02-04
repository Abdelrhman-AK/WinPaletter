using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Security.Principal;
using System.Windows.Forms;
using WinPaletter.NativeMethods;
using static WinPaletter.NativeMethods.User32;

namespace WinPaletter.Theme
{
    public partial class Manager
    {
        /// <summary>
        /// Apply Windows 7/8.1 LogonUI screen
        /// </summary>
        /// <param name="LogonElement">Structure that contains Windows 7/8.1 LogonUI data: <b>WinPaletter.Theme.Manager.LogonUI7 or LogonUI81</b></param>
        /// <param name="RegEntryHint">Registry subkey to store data in WinPaletter registry (HKEY_CURRENT_USER\Software\WinPaletter)</param>
        /// <param name="TreeView">TreeView used to show applying log</param>
        public void Apply_LogonUI7(Theme.Structures.LogonUI7 LogonElement, string RegEntryHint = "LogonUI", TreeView TreeView = null)
        {

            bool ReportProgress = Program.Settings.ThemeLog.VerboseLevel != Settings.Structures.ThemeLog.VerboseLevels.None && TreeView is not null;
            bool ReportProgress_Detailed = ReportProgress && Program.Settings.ThemeLog.VerboseLevel == Settings.Structures.ThemeLog.VerboseLevels.Detailed;

            EditReg(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Authentication\LogonUI\Background", "OEMBackground", LogonElement.Enabled ? 1 : 0);
            EditReg(@"HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Windows\System", "UseOEMBackground", LogonElement.Enabled ? 1 : 0);

            EditReg($@"HKEY_CURRENT_USER\Software\WinPaletter\{RegEntryHint}", "Mode", (int)LogonElement.Mode);
            EditReg($@"HKEY_CURRENT_USER\Software\WinPaletter\{RegEntryHint}", "ImagePath", LogonElement.ImagePath, RegistryValueKind.String);
            EditReg($@"HKEY_CURRENT_USER\Software\WinPaletter\{RegEntryHint}", "Color", LogonElement.Color.ToArgb());
            EditReg($@"HKEY_CURRENT_USER\Software\WinPaletter\{RegEntryHint}", "Blur", LogonElement.Blur ? 1 : 0);
            EditReg($@"HKEY_CURRENT_USER\Software\WinPaletter\{RegEntryHint}", "Blur_Intensity", LogonElement.Blur_Intensity);
            EditReg($@"HKEY_CURRENT_USER\Software\WinPaletter\{RegEntryHint}", "Grayscale", LogonElement.Grayscale ? 1 : 0);
            EditReg($@"HKEY_CURRENT_USER\Software\WinPaletter\{RegEntryHint}", "Noise", LogonElement.Noise ? 1 : 0);
            EditReg($@"HKEY_CURRENT_USER\Software\WinPaletter\{RegEntryHint}", "Noise_Mode", (int)LogonElement.Noise_Mode);
            EditReg($@"HKEY_CURRENT_USER\Software\WinPaletter\{RegEntryHint}", "Noise_Intensity", LogonElement.Noise_Intensity);

            if (LogonElement.Enabled)
            {
                IntPtr wow64Value = IntPtr.Zero;
                Kernel32.Wow64DisableWow64FsRedirection(ref wow64Value);

                string DirX = $@"{PathsExt.System32}\oobe\info\backgrounds";

                Directory.CreateDirectory(DirX);

                foreach (string fileX in System.IO.Directory.GetFiles(DirX))
                {
                    try { System.IO.File.Delete(fileX); }
                    catch { }
                }

                List<Bitmap> bmpList = new();
                bmpList.Clear();

                if (ReportProgress_Detailed)
                    AddNode(TreeView, Program.Lang.Verbose_GetInstanceLogonUIImg, "info");

                switch (LogonElement.Mode)
                {
                    case Theme.Structures.LogonUI7.Sources.Default:
                        {
                            for (int i = 5031; i <= 5043; i += +1)
                                bmpList.Add(PE_Functions.GetPNGFromDLL(PathsExt.imageres, i, "IMAGE", Screen.PrimaryScreen.Bounds.Size.Width, Screen.PrimaryScreen.Bounds.Size.Height));
                            break;
                        }

                    case Theme.Structures.LogonUI7.Sources.CustomImage:
                        {
                            if (System.IO.File.Exists(LogonElement.ImagePath))
                            {
                                bmpList.Add(Bitmap_Mgr.Load(LogonElement.ImagePath).Resize(Screen.PrimaryScreen.Bounds.Size));
                            }
                            else
                            {
                                bmpList.Add(Color.Black.ToBitmap(Screen.PrimaryScreen.Bounds.Size));
                            }

                            break;
                        }

                    case Theme.Structures.LogonUI7.Sources.SolidColor:
                        {
                            bmpList.Add(LogonElement.Color.ToBitmap(Screen.PrimaryScreen.Bounds.Size));
                            break;
                        }

                    case Theme.Structures.LogonUI7.Sources.Wallpaper:
                        {
                            using (Bitmap b = new(Program.GetWallpaperFromRegistry()))
                            {
                                bmpList.Add((Bitmap)b.Resize(Screen.PrimaryScreen.Bounds.Size).Clone());
                            }

                            break;
                        }

                }

                if (ReportProgress)
                    AddNode(TreeView, string.Format(Program.Lang.TM_RenderingCustomLogonUI_MayNotRespond), "info");

                for (int x = 0, loopTo = bmpList.Count - 1; x <= loopTo; x++)
                {
                    if (ReportProgress)
                        AddNode(TreeView, $"{DateTime.Now.ToLongTimeString()}: {Program.Lang.TM_RenderingCustomLogonUI_Progress} {bmpList[x].Width}x{bmpList[x].Height} ({x + 1}/{bmpList.Count})", "info");

                    if (LogonElement.Grayscale)
                    {
                        if (ReportProgress_Detailed)
                            AddNode(TreeView, Program.Lang.Verbose_GrayscaleLogonUIImg, "apply");

                        bmpList[x] = bmpList[x].Grayscale();
                    }

                    if (LogonElement.Blur)
                    {
                        if (ReportProgress_Detailed)
                            AddNode(TreeView, Program.Lang.Verbose_BlurringLogonUIImg, "apply");

                        ImageProcessor.ImageFactory imgF = new();

                        using (Bitmap b = new(bmpList[x]))
                        {
                            imgF.Load(b);
                            imgF.GaussianBlur(LogonElement.Blur_Intensity);
                            bmpList[x] = (Bitmap)imgF.Image;
                        }
                    }

                    if (LogonElement.Noise)
                    {
                        if (ReportProgress_Detailed)
                            AddNode(TreeView, Program.Lang.Verbose_NoiseLogonUIImg, "apply");

                        bmpList[x] = bmpList[x].Noise(LogonElement.Noise_Mode, (float)(LogonElement.Noise_Intensity / 100d));
                    }
                }

                if (bmpList.Count == 1)
                {
                    if (Program.Elevated)
                    {
                        bmpList[0].Save($@"{DirX}\backgroundDefault.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                    }
                    else
                    {
                        bmpList[0].Save($@"{PathsExt.appData}\backgroundDefault.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                        Reg_IO.Move_File($@"{PathsExt.appData}\backgroundDefault.jpg", $@"{DirX}\backgroundDefault.jpg");
                    }

                    if (ReportProgress_Detailed)
                        AddNode(TreeView, string.Format(Program.Lang.Verbose_LogonUIImgSaved, $@"{DirX}\backgroundDefault.jpg"), "info");
                }
                else
                {
                    for (int x = 0; x <= bmpList.Count - 1; x++)
                    {
                        if (Program.Elevated)
                        {
                            bmpList[x].Save($"{DirX}{($@"\background{bmpList[x].Width}x{bmpList[x].Height}.jpg")}", System.Drawing.Imaging.ImageFormat.Jpeg);
                        }
                        else
                        {
                            bmpList[x].Save($"{PathsExt.appData}{($@"\background{bmpList[x].Width}x{bmpList[x].Height}.jpg")}", System.Drawing.Imaging.ImageFormat.Jpeg);
                            Reg_IO.Move_File($"{PathsExt.appData}{($@"\background{bmpList[x].Width}x{bmpList[x].Height}.jpg")}", $"{DirX}{($@"\background{bmpList[x].Width}x{bmpList[x].Height}.jpg")}");
                        }

                        if (ReportProgress_Detailed)
                            AddNode(TreeView, string.Format(Program.Lang.Verbose_LogonUIImgNUMSaved, $"{DirX}{($@"\background{bmpList[x].Width}x{bmpList[x].Height}.jpg")}", x + 1), "info");

                    }
                }

                Kernel32.Wow64RevertWow64FsRedirection(IntPtr.Zero);
            }
        }

        /// <summary>
        /// Apply Windows 8 lock screen
        /// </summary>
        /// <param name="TreeView">TreeView used to show applying log</param>
        public void Apply_LogonUI_8(TreeView TreeView = null)
        {

            bool ReportProgress = Program.Settings.ThemeLog.VerboseLevel != Settings.Structures.ThemeLog.VerboseLevels.None && TreeView is not null;
            bool ReportProgress_Detailed = ReportProgress && Program.Settings.ThemeLog.VerboseLevel == Settings.Structures.ThemeLog.VerboseLevels.Detailed;

            string lockimg = $@"{PathsExt.appData}\LockScreen.png";

            EditReg(@"HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Windows\Personalization", "NoLockScreen", Windows81.NoLockScreen ? 1 : 0);
            EditReg(@"HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Windows\Personalization", "LockScreenImage", lockimg, RegistryValueKind.String);

            EditReg(@"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI", "Mode", (int)Windows81.LockScreenType);
            EditReg(@"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI", "Metro_LockScreenSystemID", Windows81.LockScreenSystemID);
            EditReg(@"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI", "ImagePath", LogonUI7.ImagePath, RegistryValueKind.String);
            EditReg(@"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI", "Color", LogonUI7.Color.ToArgb());
            EditReg(@"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI", "Blur", LogonUI7.Blur ? 1 : 0);
            EditReg(@"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI", "Blur_Intensity", LogonUI7.Blur_Intensity);
            EditReg(@"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI", "Grayscale", LogonUI7.Grayscale ? 1 : 0);
            EditReg(@"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI", "Noise", LogonUI7.Noise ? 1 : 0);
            EditReg(@"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI", "Noise_Mode", (int)LogonUI7.Noise_Mode);
            EditReg(@"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI", "Noise_Intensity", LogonUI7.Noise_Intensity);

            if (!Windows81.NoLockScreen)
            {
                Bitmap bmp;

                if (ReportProgress_Detailed)
                    AddNode(TreeView, Program.Lang.Verbose_GetInstanceLockScreenImg, "info");

                switch (Windows81.LockScreenType)
                {
                    case Theme.Structures.LogonUI7.Sources.Default:
                        {
                            string syslock = string.Empty;

                            if (System.IO.File.Exists($@"{PathsExt.Windows}\Web\Screen\img10{this.Windows81.LockScreenSystemID}.png"))
                            {
                                syslock = $@"{PathsExt.Windows}\Web\Screen\img10{this.Windows81.LockScreenSystemID}.png";
                            }

                            else if (System.IO.File.Exists($@"{PathsExt.Windows}\Web\Screen\img10{this.Windows81.LockScreenSystemID}.jpg"))
                            {
                                syslock = $@"{PathsExt.Windows}\Web\Screen\img10{this.Windows81.LockScreenSystemID}.jpg";

                            }

                            if (System.IO.File.Exists(syslock))
                            {
                                bmp = Bitmap_Mgr.Load(syslock);
                            }
                            else
                            {
                                bmp = Color.Black.ToBitmap(Screen.PrimaryScreen.Bounds.Size);
                            }

                            break;
                        }

                    case Theme.Structures.LogonUI7.Sources.CustomImage:
                        {
                            if (System.IO.File.Exists(LogonUI7.ImagePath))
                            {
                                bmp = Bitmap_Mgr.Load(LogonUI7.ImagePath);
                            }
                            else
                            {
                                bmp = Color.Black.ToBitmap(Screen.PrimaryScreen.Bounds.Size);
                            }

                            break;
                        }

                    case Theme.Structures.LogonUI7.Sources.SolidColor:
                        {
                            bmp = LogonUI7.Color.ToBitmap(Screen.PrimaryScreen.Bounds.Size);
                            break;
                        }

                    case Theme.Structures.LogonUI7.Sources.Wallpaper:
                        {
                            using (Bitmap b = new(Program.GetWallpaperFromRegistry()))
                            {
                                bmp = (Bitmap)b.Clone();
                            }

                            break;
                        }

                    default:
                        {
                            bmp = Color.Black.ToBitmap(Screen.PrimaryScreen.Bounds.Size);
                            break;
                        }

                }

                if (ReportProgress)
                    AddNode(TreeView, string.Format(Program.Lang.TM_RenderingCustomLogonUI_MayNotRespond), "info");

                if (ReportProgress)
                    AddNode(TreeView, $"{DateTime.Now.ToLongTimeString()}: {Program.Lang.TM_RenderingCustomLogonUI}", "info");

                if (LogonUI7.Grayscale)
                {
                    if (ReportProgress_Detailed)
                        AddNode(TreeView, Program.Lang.Verbose_GrayscaleLockScreenImg, "apply");
                    bmp = bmp.Grayscale();
                }

                if (LogonUI7.Blur)
                {
                    if (ReportProgress_Detailed)
                        AddNode(TreeView, Program.Lang.Verbose_BlurringLockScreenImg, "apply");
                    ImageProcessor.ImageFactory ImgF = new();
                    using (Bitmap b = new(bmp))
                    {
                        ImgF.Load(b);
                        ImgF.GaussianBlur(LogonUI7.Blur_Intensity);
                        bmp = (Bitmap)ImgF.Image;
                    }

                }

                if (LogonUI7.Noise)
                {
                    if (ReportProgress_Detailed)
                        AddNode(TreeView, Program.Lang.Verbose_NoiseLockScreenImg, "apply");
                    bmp = bmp.Noise(LogonUI7.Noise_Mode, (float)(LogonUI7.Noise_Intensity / 100d));
                }

                if (System.IO.File.Exists(lockimg))
                    System.IO.File.Delete(lockimg);

                if (ReportProgress_Detailed)
                    AddNode(TreeView, string.Format(Program.Lang.Verbose_LockScreenImgSaved, lockimg), "info");

                bmp.Save(lockimg);

            }

        }

        /// <summary>
        /// Apply Command Prompt preferences
        /// </summary>
        /// <param name="TreeView">TreeView used to show applying log</param>
        public void Apply_CommandPrompt(TreeView TreeView = null)
        {
            if (CommandPrompt.Enabled)
            {
                Theme.Structures.Console.Save_Console_To_Registry("HKEY_CURRENT_USER", string.Empty, CommandPrompt, TreeView);
                if (Program.Settings.ThemeApplyingBehavior.CMD_OverrideUserPreferences)
                    Theme.Structures.Console.Save_Console_To_Registry("HKEY_CURRENT_USER", "%SystemRoot%_System32_cmd.exe", CommandPrompt, TreeView);

                if (Program.Settings.ThemeApplyingBehavior.CMD_HKU_DEFAULT_Prefs == Settings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite)
                {
                    Theme.Structures.Console.Save_Console_To_Registry(@"HKEY_USERS\.DEFAULT", string.Empty, CommandPrompt, TreeView);
                    Theme.Structures.Console.Save_Console_To_Registry(@"HKEY_USERS\.DEFAULT", "%SystemRoot%_System32_cmd.exe", CommandPrompt, TreeView);
                }
            }
        }

        /// <summary>
        /// Apply PowerShell x86 preferences
        /// </summary>
        /// <param name="TreeView">TreeView used to show applying log</param>
        public void Apply_PowerShell86(TreeView TreeView = null)
        {
            if (PowerShellx86.Enabled & Directory.Exists($@"{Environment.GetEnvironmentVariable("WINDIR")}\System32\WindowsPowerShell\v1.0"))
            {
                Theme.Structures.Console.Save_Console_To_Registry("HKEY_CURRENT_USER", "%SystemRoot%_System32_WindowsPowerShell_v1.0_powershell.exe", PowerShellx86, TreeView);
                if (Program.Settings.ThemeApplyingBehavior.PS86_HKU_DEFAULT_Prefs == Settings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite)
                {
                    Theme.Structures.Console.Save_Console_To_Registry(@"HKEY_USERS\.DEFAULT", "%SystemRoot%_System32_WindowsPowerShell_v1.0_powershell.exe", PowerShellx86, TreeView);
                }
            }
        }

        /// <summary>
        /// Apply PowerShell x64 preferences
        /// </summary>
        /// <param name="TreeView">TreeView used to show applying log</param>
        public void Apply_PowerShell64(TreeView TreeView = null)
        {
            if (PowerShellx64.Enabled & Directory.Exists($@"{Environment.GetEnvironmentVariable("WINDIR")}\SysWOW64\WindowsPowerShell\v1.0"))
            {
                Theme.Structures.Console.Save_Console_To_Registry("HKEY_CURRENT_USER", "%SystemRoot%_SysWOW64_WindowsPowerShell_v1.0_powershell.exe", PowerShellx64, TreeView);
                if (Program.Settings.ThemeApplyingBehavior.PS64_HKU_DEFAULT_Prefs == Settings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite)
                {
                    Theme.Structures.Console.Save_Console_To_Registry(@"HKEY_USERS\.DEFAULT", "%SystemRoot%_SysWOW64_WindowsPowerShell_v1.0_powershell.exe", PowerShellx64, TreeView);
                }
            }
        }

        /// <summary>
        /// Apply WinPaletter themed cursors
        /// </summary>
        /// <param name="TM">WinPaletter theme manager</param>
        /// <param name="TreeView">TreeView used to show applying log</param>
        public void Apply_Cursors(TreeView TreeView = null)
        {
            using (WindowsImpersonationContext wic = User.Identity.Impersonate())
            {
                bool ReportProgress = Program.Settings.ThemeLog.VerboseLevel != Settings.Structures.ThemeLog.VerboseLevels.None && TreeView is not null;
                bool ReportProgress_Detailed = ReportProgress && Program.Settings.ThemeLog.VerboseLevel == Settings.Structures.ThemeLog.VerboseLevels.Detailed;

                EditReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors", string.Empty, Cursor_Enabled);

                Stopwatch sw = new();
                if (ReportProgress)
                    AddNode(TreeView, $"{DateTime.Now.ToLongTimeString()}: {Program.Lang.TM_SavingCursorsColors}", "info");

                sw.Reset();
                sw.Start();

                Structures.Cursor.Save_Cursors_To_Registry("Arrow", Cursor_Arrow, ReportProgress_Detailed ? TreeView : null);
                Structures.Cursor.Save_Cursors_To_Registry("Help", Cursor_Help, ReportProgress_Detailed ? TreeView : null);
                Structures.Cursor.Save_Cursors_To_Registry("AppLoading", Cursor_AppLoading, ReportProgress_Detailed ? TreeView : null);
                Structures.Cursor.Save_Cursors_To_Registry("Busy", Cursor_Busy, ReportProgress_Detailed ? TreeView : null);
                Structures.Cursor.Save_Cursors_To_Registry("Move", Cursor_Move, ReportProgress_Detailed ? TreeView : null);
                Structures.Cursor.Save_Cursors_To_Registry("NS", Cursor_NS, ReportProgress_Detailed ? TreeView : null);
                Structures.Cursor.Save_Cursors_To_Registry("EW", Cursor_EW, ReportProgress_Detailed ? TreeView : null);
                Structures.Cursor.Save_Cursors_To_Registry("NESW", Cursor_NESW, ReportProgress_Detailed ? TreeView : null);
                Structures.Cursor.Save_Cursors_To_Registry("NWSE", Cursor_NWSE, ReportProgress_Detailed ? TreeView : null);
                Structures.Cursor.Save_Cursors_To_Registry("Up", Cursor_Up, ReportProgress_Detailed ? TreeView : null);
                Structures.Cursor.Save_Cursors_To_Registry("Pen", Cursor_Pen, ReportProgress_Detailed ? TreeView : null);
                Structures.Cursor.Save_Cursors_To_Registry("None", Cursor_None, ReportProgress_Detailed ? TreeView : null);
                Structures.Cursor.Save_Cursors_To_Registry("Link", Cursor_Link, ReportProgress_Detailed ? TreeView : null);
                Structures.Cursor.Save_Cursors_To_Registry("Pin", Cursor_Pin, ReportProgress_Detailed ? TreeView : null);
                Structures.Cursor.Save_Cursors_To_Registry("Person", Cursor_Person, ReportProgress_Detailed ? TreeView : null);
                Structures.Cursor.Save_Cursors_To_Registry("IBeam", Cursor_IBeam, ReportProgress_Detailed ? TreeView : null);
                Structures.Cursor.Save_Cursors_To_Registry("Cross", Cursor_Cross, ReportProgress_Detailed ? TreeView : null);

                if (ReportProgress)
                    AddNode(TreeView, string.Format(Program.Lang.TM_Time, sw.ElapsedMilliseconds / 1000d), "time");

                sw.Stop();

                if (Cursor_Enabled)
                {
                    if (!Program.Settings.AspectsControl.Enabled || (Program.Settings.AspectsControl.Enabled && Program.Settings.AspectsControl.Cursors))
                    {
                        Execute(new MethodInvoker(() => ExportCursors(this, TreeView)), TreeView, Program.Lang.TM_RenderingCursors, Program.Lang.TM_RenderingCursors_Error, Program.Lang.TM_Time);

                        Execute(new MethodInvoker(() =>
                        {
                            SystemParametersInfo(ReportProgress_Detailed ? TreeView : null, SPI.SPI_SETCURSORSHADOW, 0, Cursor_Shadow, SPIF.SPIF_UPDATEINIFILE);
                            SystemParametersInfo(ReportProgress_Detailed ? TreeView : null, SPI.SPI_SETMOUSESONAR, 0, Cursor_Sonar, SPIF.SPIF_UPDATEINIFILE);
                            SystemParametersInfo(ReportProgress_Detailed ? TreeView : null, SPI.SPI_SETMOUSETRAILS, Cursor_Trails, 0, SPIF.SPIF_UPDATEINIFILE);

                            ApplyCursorsToReg(this, "HKEY_CURRENT_USER", ReportProgress_Detailed ? TreeView : null);

                            if (Program.Settings.ThemeApplyingBehavior.Cursors_HKU_DEFAULT_Prefs == Settings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite)
                            {
                                EditReg(@"HKEY_USERS\.DEFAULT\Control Panel\Mouse", "MouseTrails", Cursor_Trails);
                                ApplyCursorsToReg(this, @"HKEY_USERS\.DEFAULT", ReportProgress_Detailed ? TreeView : null);
                            }

                        }), TreeView, Program.Lang.TM_ApplyingCursors, Program.Lang.TM_CursorsApplying_Error, Program.Lang.TM_Time);
                    }
                    else if (ReportProgress)
                        AddNode(TreeView, $"{DateTime.Now.ToLongTimeString()}: {Program.Lang.TM_Skip_Cursors}", "skip");
                }

                else if (Program.Settings.ThemeApplyingBehavior.ResetCursorsToAero)
                {
                    if (!OS.WXP)
                    {
                        ResetCursorsToAero("HKEY_CURRENT_USER", ReportProgress_Detailed ? TreeView : null);
                        if (Program.Settings.ThemeApplyingBehavior.Cursors_HKU_DEFAULT_Prefs == Settings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite)
                            ResetCursorsToAero(@"HKEY_USERS\.DEFAULT");
                    }

                    else
                    {
                        ResetCursorsToNone_XP("HKEY_CURRENT_USER", ReportProgress_Detailed ? TreeView : null);
                        if (Program.Settings.ThemeApplyingBehavior.Cursors_HKU_DEFAULT_Prefs == Settings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite)
                            ResetCursorsToNone_XP(@"HKEY_USERS\.DEFAULT");
                    }
                }
                wic.Undo();
            }
        }
    }
}