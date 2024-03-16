using System;
using System.Diagnostics;
using System.IO;
using System.Security.Principal;
using System.Windows.Forms;
using static WinPaletter.NativeMethods.User32;

namespace WinPaletter.Theme
{
    public partial class Manager
    {
        /// <summary>
        /// Apply Command Prompt preferences
        /// </summary>
        /// <param name="treeView">treeView used to show applying log</param>
        public void Apply_CommandPrompt(TreeView treeView = null)
        {
            if (CommandPrompt.Enabled)
            {
                Theme.Structures.Console.Save_Console_To_Registry("HKEY_CURRENT_USER", string.Empty, CommandPrompt, treeView);
                if (Program.Settings.ThemeApplyingBehavior.CMD_OverrideUserPreferences)
                    Theme.Structures.Console.Save_Console_To_Registry("HKEY_CURRENT_USER", "%SystemRoot%_System32_cmd.exe", CommandPrompt, treeView);

                if (Program.Settings.ThemeApplyingBehavior.CMD_HKU_DEFAULT_Prefs == Settings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite)
                {
                    Theme.Structures.Console.Save_Console_To_Registry(@"HKEY_USERS\.DEFAULT", string.Empty, CommandPrompt, treeView);
                    Theme.Structures.Console.Save_Console_To_Registry(@"HKEY_USERS\.DEFAULT", "%SystemRoot%_System32_cmd.exe", CommandPrompt, treeView);
                }
            }
        }

        /// <summary>
        /// Apply PowerShell x86 preferences
        /// </summary>
        /// <param name="treeView">treeView used to show applying log</param>
        public void Apply_PowerShell86(TreeView treeView = null)
        {
            if (PowerShellx86.Enabled & Directory.Exists($@"{Environment.GetEnvironmentVariable("WINDIR")}\System32\WindowsPowerShell\v1.0"))
            {
                Theme.Structures.Console.Save_Console_To_Registry("HKEY_CURRENT_USER", "%SystemRoot%_System32_WindowsPowerShell_v1.0_powershell.exe", PowerShellx86, treeView);
                if (Program.Settings.ThemeApplyingBehavior.PS86_HKU_DEFAULT_Prefs == Settings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite)
                {
                    Theme.Structures.Console.Save_Console_To_Registry(@"HKEY_USERS\.DEFAULT", "%SystemRoot%_System32_WindowsPowerShell_v1.0_powershell.exe", PowerShellx86, treeView);
                }
            }
        }

        /// <summary>
        /// Apply PowerShell x64 preferences
        /// </summary>
        /// <param name="treeView">treeView used to show applying log</param>
        public void Apply_PowerShell64(TreeView treeView = null)
        {
            if (PowerShellx64.Enabled & Directory.Exists($@"{Environment.GetEnvironmentVariable("WINDIR")}\SysWOW64\WindowsPowerShell\v1.0"))
            {
                Theme.Structures.Console.Save_Console_To_Registry("HKEY_CURRENT_USER", "%SystemRoot%_SysWOW64_WindowsPowerShell_v1.0_powershell.exe", PowerShellx64, treeView);
                if (Program.Settings.ThemeApplyingBehavior.PS64_HKU_DEFAULT_Prefs == Settings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite)
                {
                    Theme.Structures.Console.Save_Console_To_Registry(@"HKEY_USERS\.DEFAULT", "%SystemRoot%_SysWOW64_WindowsPowerShell_v1.0_powershell.exe", PowerShellx64, treeView);
                }
            }
        }

        /// <summary>
        /// Apply WinPaletter themed cursors
        /// </summary>
        /// <param name="TM">WinPaletter theme manager</param>
        /// <param name="treeView">treeView used to show applying log</param>
        public void Apply_Cursors(TreeView treeView = null)
        {
            using (WindowsImpersonationContext wic = User.Identity.Impersonate())
            {
                bool ReportProgress = Program.Settings.ThemeLog.VerboseLevel != Settings.Structures.ThemeLog.VerboseLevels.None && treeView is not null;
                bool ReportProgress_Detailed = ReportProgress && Program.Settings.ThemeLog.VerboseLevel == Settings.Structures.ThemeLog.VerboseLevels.Detailed;

                Stopwatch sw = new();
                if (ReportProgress)
                    ThemeLog.AddNode(treeView, $"{DateTime.Now.ToLongTimeString()}: {Program.Lang.TM_SavingCursorsColors}", "info");

                sw.Reset();
                sw.Start();

                Structures.Cursor.Save_Cursors_To_Registry("Arrow", Cursor_Arrow, ReportProgress_Detailed ? treeView : null);
                Structures.Cursor.Save_Cursors_To_Registry("Help", Cursor_Help, ReportProgress_Detailed ? treeView : null);
                Structures.Cursor.Save_Cursors_To_Registry("AppLoading", Cursor_AppLoading, ReportProgress_Detailed ? treeView : null);
                Structures.Cursor.Save_Cursors_To_Registry("Busy", Cursor_Busy, ReportProgress_Detailed ? treeView : null);
                Structures.Cursor.Save_Cursors_To_Registry("Move", Cursor_Move, ReportProgress_Detailed ? treeView : null);
                Structures.Cursor.Save_Cursors_To_Registry("NS", Cursor_NS, ReportProgress_Detailed ? treeView : null);
                Structures.Cursor.Save_Cursors_To_Registry("EW", Cursor_EW, ReportProgress_Detailed ? treeView : null);
                Structures.Cursor.Save_Cursors_To_Registry("NESW", Cursor_NESW, ReportProgress_Detailed ? treeView : null);
                Structures.Cursor.Save_Cursors_To_Registry("NWSE", Cursor_NWSE, ReportProgress_Detailed ? treeView : null);
                Structures.Cursor.Save_Cursors_To_Registry("Up", Cursor_Up, ReportProgress_Detailed ? treeView : null);
                Structures.Cursor.Save_Cursors_To_Registry("Pen", Cursor_Pen, ReportProgress_Detailed ? treeView : null);
                Structures.Cursor.Save_Cursors_To_Registry("None", Cursor_None, ReportProgress_Detailed ? treeView : null);
                Structures.Cursor.Save_Cursors_To_Registry("Link", Cursor_Link, ReportProgress_Detailed ? treeView : null);
                Structures.Cursor.Save_Cursors_To_Registry("Pin", Cursor_Pin, ReportProgress_Detailed ? treeView : null);
                Structures.Cursor.Save_Cursors_To_Registry("Person", Cursor_Person, ReportProgress_Detailed ? treeView : null);
                Structures.Cursor.Save_Cursors_To_Registry("IBeam", Cursor_IBeam, ReportProgress_Detailed ? treeView : null);
                Structures.Cursor.Save_Cursors_To_Registry("Cross", Cursor_Cross, ReportProgress_Detailed ? treeView : null);

                if (ReportProgress)
                    ThemeLog.AddNode(treeView, string.Format(Program.Lang.TM_Time, sw.ElapsedMilliseconds / 1000d), "time");

                sw.Stop();

                if (Cursor_Enabled)
                {
                    if (!Program.Settings.AspectsControl.Enabled || (Program.Settings.AspectsControl.Enabled && Program.Settings.AspectsControl.Cursors))
                    {
                        Execute(new MethodInvoker(() => ExportCursors(this, treeView)), treeView, Program.Lang.TM_RenderingCursors, Program.Lang.TM_RenderingCursors_Error, Program.Lang.TM_Time);

                        Execute(new MethodInvoker(() =>
                        {
                            SystemParametersInfo(ReportProgress_Detailed ? treeView : null, SPI.SPI_SETCURSORSHADOW, 0, Cursor_Shadow, SPIF.SPIF_UPDATEINIFILE);
                            SystemParametersInfo(ReportProgress_Detailed ? treeView : null, SPI.SPI_SETMOUSESONAR, 0, Cursor_Sonar, SPIF.SPIF_UPDATEINIFILE);
                            SystemParametersInfo(ReportProgress_Detailed ? treeView : null, SPI.SPI_SETMOUSETRAILS, Cursor_Trails, 0, SPIF.SPIF_UPDATEINIFILE);

                            ApplyCursorsToReg(this, "HKEY_CURRENT_USER", ReportProgress_Detailed ? treeView : null);

                            if (Program.Settings.ThemeApplyingBehavior.Cursors_HKU_DEFAULT_Prefs == Settings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite)
                            {
                                EditReg(@"HKEY_USERS\.DEFAULT\Control Panel\Mouse", "MouseTrails", Cursor_Trails);
                                ApplyCursorsToReg(this, @"HKEY_USERS\.DEFAULT", ReportProgress_Detailed ? treeView : null);
                            }

                        }), treeView, Program.Lang.TM_ApplyingCursors, Program.Lang.TM_CursorsApplying_Error, Program.Lang.TM_Time);
                    }
                    else if (ReportProgress)
                        ThemeLog.AddNode(treeView, $"{DateTime.Now.ToLongTimeString()}: {Program.Lang.TM_Skip_Cursors}", "skip");
                }

                else if (Program.Settings.ThemeApplyingBehavior.ResetCursorsToAero)
                {
                    if (!OS.WXP)
                    {
                        ResetCursorsToAero("HKEY_CURRENT_USER", ReportProgress_Detailed ? treeView : null);
                        if (Program.Settings.ThemeApplyingBehavior.Cursors_HKU_DEFAULT_Prefs == Settings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite)
                            ResetCursorsToAero(@"HKEY_USERS\.DEFAULT");
                    }

                    else
                    {
                        ResetCursorsToNone_XP("HKEY_CURRENT_USER", ReportProgress_Detailed ? treeView : null);
                        if (Program.Settings.ThemeApplyingBehavior.Cursors_HKU_DEFAULT_Prefs == Settings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite)
                            ResetCursorsToNone_XP(@"HKEY_USERS\.DEFAULT");
                    }
                }
                wic.Undo();
            }
        }
    }
}