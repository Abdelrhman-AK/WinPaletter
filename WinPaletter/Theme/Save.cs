using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Windows.Forms;
using WinPaletter.NativeMethods;
using WinPaletter.Theme.Structures;

namespace WinPaletter.Theme
{
    /// <summary>
    /// This class is responsible for managing WinPaletter theme
    /// </summary>
    public partial class Manager
    {
        /// <summary>
        /// ApplyToTM or apply WinPaletter theme
        /// </summary>
        /// <param name="destination">destination into which WinPaletter will write theme data. It can be registry or File.</param>
        /// <param name="file">If selected destination is File, this will specify WinPaletter theme File</param>
        /// <param name="treeView">Specify treeView to write theme applying log (Registry destination only)</param>
        /// <param name="resetToDefault">Restore Windows theme to default before applying a WinPaletter theme</param>
        /// <param name="silent">Don't show alerts on applying a WinPaletter theme</param>
        public void Save(Source destination, string file = "", TreeView treeView = null, bool resetToDefault = false, bool silent = false)
        {
            switch (destination)
            {
                case Source.Registry:

                    // Impersonate the user to apply the theme into the correct user registry
                    using (WindowsImpersonationContext wic = User.Identity.Impersonate())
                    {
                        Program.Log?.Write(LogEventLevel.Information, $"Applying WinPaletter theme to registry for user `{User.Domain}\\{User.Name}`.");
                        Program.Log?.Write(LogEventLevel.Information, $"Theme name: {Info.ThemeName}");

                        // If theme backup option is enabled, backup it before applying
                        if (Program.Settings.BackupTheme.Enabled && Program.Settings.BackupTheme.AutoBackupOnApply)
                        {
                            string filename = Program.GetUniqueFileName($"{Program.Settings.BackupTheme.BackupPath}\\OnThemeApply", $"{Info.ThemeName}_{DateTime.Now.Hour}.{DateTime.Now.Minute}.{DateTime.Now.Second}.wpth");

                            Program.Log?.Write(LogEventLevel.Information, $"Backing up WinPaletter theme before applying it as `{filename}`.");

                            Save(Source.File, filename);
                        }

                        // Get flags to report progress (verbosity levels)
                        bool ReportProgress = Program.Settings.ThemeLog.VerboseLevel != Settings.Structures.ThemeLog.VerboseLevels.None && treeView is not null;
                        bool ReportProgress_Detailed = ReportProgress && Program.Settings.ThemeLog.VerboseLevel == Settings.Structures.ThemeLog.VerboseLevels.Detailed;

                        // Get treeView to write theme applying log
                        TreeView tv = ReportProgress_Detailed ? treeView : null;

                        // Reset error flag
                        _ErrorHappened = false;

                        // Start stopwatch to measure time
                        Stopwatch sw_all = new(); sw_all.Reset(); sw_all.Start();

                        // Clear exceptions list and treeView log
                        if (ReportProgress)
                        {
                            Exceptions.ThemeApply.Clear();
                            treeView.Visible = false;
                            treeView.Nodes.Clear();
                            treeView.Visible = true;
                        }

                        // Start system restore point creation
                        if (Program.Settings.ThemeApplyingBehavior.CreateSystemRestore)
                        {
                            if (ReportProgress && SystemRestoreHelper.Enabled)
                            {
                                ThemeLog.AddNode(treeView, $"{Program.Localization.Strings.ThemeManager.Actions.RestorePoint0}", "info");
                                ThemeLog.AddNode(treeView, $"{Program.Localization.Strings.ThemeManager.Actions.RestorePoint1}", "info");
                                ThemeLog.AddNode(treeView, $"{Program.Localization.Strings.ThemeManager.Actions.RestorePoint2}", "time");
                            }

                            bool SR_reult = SystemRestoreHelper.CreateRestorePoint(string.Format(Program.Localization.Strings.General.RestorePoint_Theme, Info.ThemeName));

                            if (ReportProgress && SR_reult)
                            {
                                ThemeLog.AddNode(treeView, $"{string.Format(Program.Localization.Strings.ThemeManager.Actions.RestorePoint3, sw_all.ElapsedMilliseconds / 1000d)}", "time");
                            }
                        }

                        if (ReportProgress)
                        {
                            string OS_str;

                            if (OS.W12) { OS_str = Program.Localization.Strings.Windows.W12; }

                            else if (OS.W11) { OS_str = Program.Localization.Strings.Windows.W11; }

                            else if (OS.W10) { OS_str = Program.Localization.Strings.Windows.W10; }

                            else if (OS.W81) { OS_str = Program.Localization.Strings.Windows.W81; }

                            else if (OS.W81) { OS_str = Program.Localization.Strings.Windows.W8; }

                            else if (OS.W7) { OS_str = Program.Localization.Strings.Windows.W7; }

                            else if (OS.WVista) { OS_str = Program.Localization.Strings.Windows.WVista; }

                            else if (OS.WXP) { OS_str = Program.Localization.Strings.Windows.WXP; }

                            else { OS_str = Program.Localization.Strings.Windows.Undefined; }

                            ThemeLog.AddNode(treeView, $"{string.Format(Program.Localization.Strings.ThemeManager.Actions.ApplyOS, OS_str)}", "info");

                            Program.Log?.Write(LogEventLevel.Information, $"WinPaletter will apply the theme as if you are using {OS_str}");

                            ThemeLog.AddNode(treeView, $"{DateTime.Now.ToLongTimeString()}: {Program.Localization.Strings.ThemeManager.Actions.Applying_Started}", "info");

                            if (!Program.Elevated)
                            {
                                ThemeLog.AddNode(treeView, $"{Program.Localization.Strings.ThemeManager.Actions.Admin_Msg0}", "admin");
                                ThemeLog.AddNode(treeView, $"{Program.Localization.Strings.ThemeManager.Actions.Admin_Msg1}", "admin");
                            }
                        }

                        // Reset to default Windows theme if requested
                        if (resetToDefault)
                        {
                            Program.Log?.Write(LogEventLevel.Information, "Resetting Windows theme to default before applying a new WinPaletter theme.");

                            Execute(() =>
                            {
                                using (Manager def = Default.FromCurrentOS)
                                {
                                    def.Wallpaper.Enabled = false;
                                    def.Save(Source.Registry);
                                }

                            },
                            treeView, Program.Localization.Strings.ThemeManager.Actions.ThemeReset, Program.Localization.Strings.ThemeManager.Errors.ThemeReset, Program.Localization.Strings.ThemeManager.Actions.Time, sw_all);
                        }

                        // Save toggles states (toggle states are saved before applying the theme to make WinPaletter apply enabled features and skip disabled features)

                        Program.Log?.Write(LogEventLevel.Information, "Saving toggles states before applying the theme.");

                        ThemeLog.AddNode(treeView, $"{Program.Localization.Strings.ThemeManager.Actions.SavingToggles}", "info");
                        AppTheme.SaveToggleState(tv);
                        Wallpaper.SaveToggleState(tv);
                        Windows12.SaveToggleState(tv);
                        Windows11.SaveToggleState(tv);
                        Windows10.SaveToggleState(tv);
                        Windows81.SaveToggleState(tv);
                        Windows8.SaveToggleState(tv);
                        Windows7.SaveToggleState(tv);
                        WindowsVista.SaveToggleState(tv);
                        WindowsXP.SaveToggleState(tv);
                        LogonUI12.SaveToggleState("12", tv);
                        LogonUI11.SaveToggleState("11", tv);
                        LogonUI10.SaveToggleState("10", tv);
                        Win32.SaveToggleState(tv);
                        Accessibility.SaveToggleState(tv);
                        LogonUI81.SaveToggleState(tv);
                        LogonUI7.SaveToggleState(tv);
                        LogonUIXP.SaveToggleState(tv);
                        MetricsFonts.SaveToggleState(tv);
                        Wallpaper.SaveToggleState(tv);
                        WallpaperTone.SaveToggleState(WallpaperTone_W12, "Win12", tv);
                        WallpaperTone.SaveToggleState(WallpaperTone_W11, "Win11", tv);
                        WallpaperTone.SaveToggleState(WallpaperTone_W10, "Win10", tv);
                        WallpaperTone.SaveToggleState(WallpaperTone_W81, "Win8.1", tv);
                        WallpaperTone.SaveToggleState(WallpaperTone_W8, "Win8", tv);
                        WallpaperTone.SaveToggleState(WallpaperTone_W7, "Win7", tv);
                        WallpaperTone.SaveToggleState(WallpaperTone_WVista, "WinVista", tv);
                        WallpaperTone.SaveToggleState(WallpaperTone_WXP, "WinXP", tv);
                        Cursors.SaveToggleState(tv);
                        WindowsEffects.SaveToggleState(tv);
                        AltTab.SaveToggleState(tv);
                        ScreenSaver.SaveToggleState(tv);
                        Sounds.SaveToggleState(tv);
                        Icons.SaveToggleState(tv);

                        WriteReg(treeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Terminals", "Terminal_CMD_Enabled", CommandPrompt.Enabled);
                        WriteReg(treeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Terminals", "Terminal_PS_32_Enabled", PowerShellx86.Enabled);
                        WriteReg(treeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Terminals", "Terminal_PS_64_Enabled", PowerShellx64.Enabled);
                        WriteReg(treeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Terminals", "Terminal_Stable_Enabled", Terminal.Enabled);
                        WriteReg(treeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Terminals", "Terminal_Preview_Enabled", TerminalPreview.Enabled);

                        // Sometimes, this entry is set to 1 when manipulating preferences by User32.SystemParametersInfo
                        WriteReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Desktop", "AutoColorization", 0);

                        // WinTheme info
                        Execute(() => Info.Apply(tv), treeView, Program.Localization.Strings.ThemeManager.Actions.SavingInfo, Program.Localization.Strings.ThemeManager.Errors.SavingInfo, Program.Localization.Strings.ThemeManager.Actions.Time, sw_all);

                        // WinPaletter application theme
                        Execute(() => AppTheme.Apply(tv), treeView,
                            string.Format(Program.Localization.Strings.ThemeManager.Actions.Applying_Feature, Program.Localization.Strings.Aspects.AppTheme),
                            string.Format(Program.Localization.Strings.ThemeManager.Errors.Error, Program.Localization.Strings.Aspects.AppTheme),
                            Program.Localization.Strings.ThemeManager.Actions.Time,
                            sw_all,
                            !AppTheme.Enabled,
                            string.Format(Program.Localization.Strings.ThemeManager.Skip.Main, Program.Localization.Strings.Aspects.AppTheme));

                        // Wallpaper
                        // Make Wallpaper before the following LogonUI items, to make a logonUI that depends on current wallpaper gets the correct File
                        Execute(() => Wallpaper.Apply(false, tv),
                            treeView,
                            string.Format(Program.Localization.Strings.ThemeManager.Actions.Applying_Feature, Program.Localization.Strings.Aspects.Wallpaper),
                            string.Format(Program.Localization.Strings.ThemeManager.Errors.Error, Program.Localization.Strings.Aspects.Wallpaper),
                            Program.Localization.Strings.ThemeManager.Actions.Time,
                            sw_all,
                            !Wallpaper.Enabled || (Program.Settings.AspectsControl.Enabled && !Program.Settings.AspectsControl.Wallpaper),
                            string.Format(Program.Localization.Strings.ThemeManager.Skip.Main, Program.Localization.Strings.Aspects.Wallpaper));

                        // Apply Windows 12 execlusive features (Colors, lock screen and visual styles)
                        if (OS.W12)
                        {
                            Execute(() => Windows12.Apply(tv), treeView,
                                string.Format(Program.Localization.Strings.ThemeManager.Actions.Theme, Program.Localization.Strings.Windows.W12),
                                string.Format(Program.Localization.Strings.ThemeManager.Errors.Error, string.Format(Program.Localization.Strings.Aspects.WinTheme, Program.Localization.Strings.Windows.W12)),
                                Program.Localization.Strings.ThemeManager.Actions.Time,
                                sw_all,
                                !Windows12.Enabled || (Program.Settings.AspectsControl.Enabled && !Program.Settings.AspectsControl.WinColors),
                                string.Format(Program.Localization.Strings.ThemeManager.Skip.Main, string.Format(Program.Localization.Strings.Aspects.WinTheme, Program.Localization.Strings.Windows.W12)));

                            Execute(() => LogonUI12.Apply("12", tv), treeView,
                                string.Format(Program.Localization.Strings.ThemeManager.Actions.Applying_Feature_ForOS, Program.Localization.Strings.Windows.W12, Program.Localization.Strings.Aspects.LockScreen),
                                string.Format(Program.Localization.Strings.ThemeManager.Errors.Error, Program.Localization.Strings.Aspects.LockScreen),
                                Program.Localization.Strings.ThemeManager.Actions.Time,
                                sw_all,
                                !LogonUI12.Enabled || (Program.Settings.AspectsControl.Enabled && !Program.Settings.AspectsControl.LogonUI),
                                string.Format(Program.Localization.Strings.ThemeManager.Skip.Main, Program.Localization.Strings.Aspects.LockScreen));
                        }

                        // Apply Windows 11 execlusive features (Colors, lock screen and visual styles)
                        if (OS.W11)
                        {
                            Execute(() => Windows11.Apply(tv), treeView,
                                string.Format(Program.Localization.Strings.ThemeManager.Actions.Theme, Program.Localization.Strings.Windows.W11),
                                string.Format(Program.Localization.Strings.ThemeManager.Errors.Error, string.Format(Program.Localization.Strings.Aspects.WinTheme, Program.Localization.Strings.Windows.W11)),
                                Program.Localization.Strings.ThemeManager.Actions.Time,
                                sw_all,
                                !Windows11.Enabled || (Program.Settings.AspectsControl.Enabled && !Program.Settings.AspectsControl.WinColors),
                                string.Format(Program.Localization.Strings.ThemeManager.Skip.Main, string.Format(Program.Localization.Strings.Aspects.WinTheme, Program.Localization.Strings.Windows.W11)));

                            Execute(() => LogonUI11.Apply("11", tv), treeView,
                                string.Format(Program.Localization.Strings.ThemeManager.Actions.Applying_Feature_ForOS, Program.Localization.Strings.Windows.W11, Program.Localization.Strings.Aspects.LockScreen),
                                string.Format(Program.Localization.Strings.ThemeManager.Errors.Error, Program.Localization.Strings.Aspects.LockScreen),
                                Program.Localization.Strings.ThemeManager.Actions.Time,
                                sw_all,
                                !LogonUI11.Enabled || (Program.Settings.AspectsControl.Enabled && !Program.Settings.AspectsControl.LogonUI),
                                string.Format(Program.Localization.Strings.ThemeManager.Skip.Main, Program.Localization.Strings.Aspects.LockScreen));
                        }

                        // Apply Windows 10 execlusive features (Colors, lock screen and visual styles)
                        if (OS.W10)
                        {
                            Execute(() => Windows10.Apply(tv), treeView,
                                string.Format(Program.Localization.Strings.ThemeManager.Actions.Theme, Program.Localization.Strings.Windows.W10),
                                string.Format(Program.Localization.Strings.ThemeManager.Errors.Error, string.Format(Program.Localization.Strings.Aspects.WinTheme, Program.Localization.Strings.Windows.W10)),
                                Program.Localization.Strings.ThemeManager.Actions.Time,
                                sw_all,
                                !Windows10.Enabled || (Program.Settings.AspectsControl.Enabled && !Program.Settings.AspectsControl.WinColors),
                                string.Format(Program.Localization.Strings.ThemeManager.Skip.Main, string.Format(Program.Localization.Strings.Aspects.WinTheme, Program.Localization.Strings.Windows.W10)));

                            Execute(() => LogonUI10.Apply("10", tv), treeView,
                                string.Format(Program.Localization.Strings.ThemeManager.Actions.Applying_Feature_ForOS, Program.Localization.Strings.Windows.W10, Program.Localization.Strings.Aspects.LockScreen),
                                string.Format(Program.Localization.Strings.ThemeManager.Errors.Error, Program.Localization.Strings.Aspects.LockScreen),
                                Program.Localization.Strings.ThemeManager.Actions.Time,
                                sw_all,
                                !LogonUI10.Enabled || (Program.Settings.AspectsControl.Enabled && !Program.Settings.AspectsControl.LogonUI),
                                string.Format(Program.Localization.Strings.ThemeManager.Skip.Main, Program.Localization.Strings.Aspects.LockScreen));
                        }

                        // Apply Windows 8.1 execlusive features (Colors, lock screen and visual styles)
                        if (OS.W81)
                        {
                            Execute(() => Windows81.Apply(this, tv), treeView,
                                string.Format(Program.Localization.Strings.ThemeManager.Actions.Theme, Program.Localization.Strings.Windows.W81),
                                string.Format(Program.Localization.Strings.ThemeManager.Errors.Error, string.Format(Program.Localization.Strings.Aspects.WinTheme, Program.Localization.Strings.Windows.W81)),
                                Program.Localization.Strings.ThemeManager.Actions.Time,
                                sw_all,
                                !Windows81.Enabled || (Program.Settings.AspectsControl.Enabled && !Program.Settings.AspectsControl.WinColors),
                                string.Format(Program.Localization.Strings.ThemeManager.Skip.Main, string.Format(Program.Localization.Strings.Aspects.WinTheme, Program.Localization.Strings.Windows.W81)));

                            Execute(() => LogonUI81.Apply(treeView), treeView,
                                string.Format(Program.Localization.Strings.ThemeManager.Actions.Applying_Feature_ForOS, Program.Localization.Strings.Windows.W81, Program.Localization.Strings.Aspects.LockScreen),
                                string.Format(Program.Localization.Strings.ThemeManager.Errors.Error, Program.Localization.Strings.Aspects.LockScreen),
                                Program.Localization.Strings.ThemeManager.Actions.Time,
                                sw_all,
                                !LogonUI81.Enabled || (Program.Settings.AspectsControl.Enabled && !Program.Settings.AspectsControl.LogonUI),
                                string.Format(Program.Localization.Strings.ThemeManager.Skip.Main, Program.Localization.Strings.Aspects.LockScreen));
                        }

                        // Apply Windows 8 execlusive features (Colors, lock screen and visual styles)
                        if (OS.W8)
                        {
                            Execute(() => Windows8.Apply(this, tv), treeView,
                                string.Format(Program.Localization.Strings.ThemeManager.Actions.Theme, Program.Localization.Strings.Windows.W8),
                                string.Format(Program.Localization.Strings.ThemeManager.Errors.Error, string.Format(Program.Localization.Strings.Aspects.WinTheme, Program.Localization.Strings.Windows.W8)),
                                Program.Localization.Strings.ThemeManager.Actions.Time,
                                sw_all,
                                !Windows8.Enabled || (Program.Settings.AspectsControl.Enabled && !Program.Settings.AspectsControl.WinColors),
                                string.Format(Program.Localization.Strings.ThemeManager.Skip.Main, string.Format(Program.Localization.Strings.Aspects.WinTheme, Program.Localization.Strings.Windows.W8)));

                            //Execute(() => LogonUI8.Apply(treeView), treeView,
                            //    string.Format(Program.Lang.Strings.ThemeManager.Actions.Applying_Feature_ForOS, Program.Lang.Strings.Windows.W8, Program.Lang.Strings.Aspects.LockScreen),
                            //    string.Format(Program.Lang.Strings.ThemeManager.Errors.Error, Program.Lang.Strings.Aspects.LockScreen),
                            //    Program.Lang.Strings.ThemeManager.Actions.Time,
                            //    sw_all,
                            //    !LogonUI8.Enabled || (Program.Settings.AspectsControl.Enabled && !Program.Settings.AspectsControl.LogonUI),
                            //    string.Format(Program.Lang.Strings.ThemeManager.Skip.Main, Program.Lang.Strings.Aspects.LockScreen));
                        }

                        // Apply Windows 7 execlusive features (Colors, themes, LogonUI screen and visual styles)
                        if (OS.W7)
                        {
                            Execute(() => Windows7.Apply(this, tv), treeView,
                                string.Format(Program.Localization.Strings.ThemeManager.Actions.Theme, Program.Localization.Strings.Windows.W7),
                                string.Format(Program.Localization.Strings.ThemeManager.Errors.Error, string.Format(Program.Localization.Strings.Aspects.WinTheme, Program.Localization.Strings.Windows.W7)),
                                Program.Localization.Strings.ThemeManager.Actions.Time,
                                sw_all,
                                !Windows7.Enabled || (Program.Settings.AspectsControl.Enabled && !Program.Settings.AspectsControl.WinColors),
                                string.Format(Program.Localization.Strings.ThemeManager.Skip.Main, string.Format(Program.Localization.Strings.Aspects.WinTheme, Program.Localization.Strings.Windows.W7)));

                            Execute(() => LogonUI7.Apply(treeView), treeView,
                                string.Format(Program.Localization.Strings.ThemeManager.Actions.Applying_Feature_ForOS, Program.Localization.Strings.Windows.W7, Program.Localization.Strings.Aspects.LogonUI),
                                string.Format(Program.Localization.Strings.ThemeManager.Errors.Error, Program.Localization.Strings.Aspects.LogonUI),
                                Program.Localization.Strings.ThemeManager.Actions.Time,
                                sw_all,
                                !LogonUI7.Enabled || (Program.Settings.AspectsControl.Enabled && !Program.Settings.AspectsControl.LogonUI),
                                string.Format(Program.Localization.Strings.ThemeManager.Skip.Main, Program.Localization.Strings.Aspects.LogonUI));
                        }

                        // Apply Windows Vista execlusive features (Colors, themes, and visual styles)
                        if (OS.WVista)
                        {
                            Execute(() => { WindowsVista.Apply(tv); }, treeView, string.Format(Program.Localization.Strings.ThemeManager.Actions.Theme, Program.Localization.Strings.Windows.WVista), string.Format(Program.Localization.Strings.ThemeManager.Errors.Error, string.Format(Program.Localization.Strings.Aspects.WinTheme, Program.Localization.Strings.Windows.WVista)),
                            Program.Localization.Strings.ThemeManager.Actions.Time,
                            sw_all,
                            !WindowsVista.Enabled || (Program.Settings.AspectsControl.Enabled && !Program.Settings.AspectsControl.WinColors),
                            string.Format(Program.Localization.Strings.ThemeManager.Skip.Main, string.Format(Program.Localization.Strings.Aspects.WinTheme, Program.Localization.Strings.Windows.WVista)));
                        }

                        // Apply Windows XP execlusive features (Themes, LogonUI screen and visual styles)
                        if (OS.WXP)
                        {
                            Execute(() => WindowsXP.Apply(tv), treeView,
                                string.Format(Program.Localization.Strings.ThemeManager.Actions.Theme, Program.Localization.Strings.Windows.WXP),
                                string.Format(Program.Localization.Strings.ThemeManager.Errors.Error, string.Format(Program.Localization.Strings.Aspects.WinTheme, Program.Localization.Strings.Windows.WXP)),
                                Program.Localization.Strings.ThemeManager.Actions.Time,
                                sw_all,
                                !WindowsXP.Enabled || (Program.Settings.AspectsControl.Enabled && !Program.Settings.AspectsControl.WinColors),
                                string.Format(Program.Localization.Strings.ThemeManager.Skip.Main, string.Format(Program.Localization.Strings.Aspects.WinTheme, Program.Localization.Strings.Windows.WXP)));

                            Execute(() => LogonUIXP.Apply(tv), treeView,
                                string.Format(Program.Localization.Strings.ThemeManager.Actions.Applying_Feature_ForOS, Program.Localization.Strings.Windows.WXP, Program.Localization.Strings.Aspects.LogonUI),
                                string.Format(Program.Localization.Strings.ThemeManager.Errors.Error, Program.Localization.Strings.Aspects.LogonUI),
                                Program.Localization.Strings.ThemeManager.Actions.Time,
                                sw_all,
                                !LogonUIXP.Enabled || (Program.Settings.AspectsControl.Enabled && !Program.Settings.AspectsControl.LogonUI),
                                string.Format(Program.Localization.Strings.ThemeManager.Skip.Main, Program.Localization.Strings.Aspects.LogonUI));
                        }

                        // Accessibility
                        Execute(() => Accessibility.Apply(tv), treeView,
                            string.Format(Program.Localization.Strings.ThemeManager.Actions.Applying_Feature, Program.Localization.Strings.Aspects.Accessibility),
                            string.Format(Program.Localization.Strings.ThemeManager.Errors.Error, Program.Localization.Strings.Aspects.Accessibility),
                            Program.Localization.Strings.ThemeManager.Actions.Time,
                            sw_all,
                            !Accessibility.Enabled || (Program.Settings.AspectsControl.Enabled && !Program.Settings.AspectsControl.Accessibility),
                            string.Format(Program.Localization.Strings.ThemeManager.Skip.Main, Program.Localization.Strings.Aspects.Accessibility));

                        // Win32UI
                        Execute(() => Win32.Apply(tv), treeView,
                            string.Format(Program.Localization.Strings.ThemeManager.Actions.Applying_Feature, Program.Localization.Strings.Aspects.ClassicColors),
                            string.Format(Program.Localization.Strings.ThemeManager.Errors.Error, Program.Localization.Strings.Aspects.ClassicColors),
                            Program.Localization.Strings.ThemeManager.Actions.Time,
                            sw_all,
                            !Win32.Enabled || (Program.Settings.AspectsControl.Enabled && !Program.Settings.AspectsControl.ClassicColors),
                            string.Format(Program.Localization.Strings.ThemeManager.Skip.Main, Program.Localization.Strings.Aspects.ClassicColors));

                        // WindowsEffects
                        Execute(() => WindowsEffects.Apply(tv, silent), treeView,
                            string.Format(Program.Localization.Strings.ThemeManager.Actions.Applying_Feature, Program.Localization.Strings.Aspects.WinEffects),
                            string.Format(Program.Localization.Strings.ThemeManager.Errors.Error, Program.Localization.Strings.Aspects.WinEffects),
                            Program.Localization.Strings.ThemeManager.Actions.Time,
                            sw_all,
                            !WindowsEffects.Enabled || (Program.Settings.AspectsControl.Enabled && !Program.Settings.AspectsControl.Effects),
                            string.Format(Program.Localization.Strings.ThemeManager.Skip.Main, Program.Localization.Strings.Aspects.WinEffects));

                        // Metrics\Fonts
                        Execute(() => MetricsFonts.Apply(tv), treeView,
                            string.Format(Program.Localization.Strings.ThemeManager.Actions.Applying_Feature, Program.Localization.Strings.Aspects.MetricsFonts),
                            string.Format(Program.Localization.Strings.ThemeManager.Errors.Error, Program.Localization.Strings.Aspects.MetricsFonts),
                            Program.Localization.Strings.ThemeManager.Actions.Time_MultipleAspects,
                            sw_all,
                            !MetricsFonts.Enabled || (Program.Settings.AspectsControl.Enabled && !Program.Settings.AspectsControl.MetricsFonts),
                            string.Format(Program.Localization.Strings.ThemeManager.Skip.Main, Program.Localization.Strings.Aspects.MetricsFonts));

                        // AltTab
                        Execute(() => AltTab.Apply(tv), treeView,
                            string.Format(Program.Localization.Strings.ThemeManager.Actions.Applying_Feature, Program.Localization.Strings.Aspects.AltTab),
                            string.Format(Program.Localization.Strings.ThemeManager.Errors.Error, Program.Localization.Strings.Aspects.AltTab),
                            Program.Localization.Strings.ThemeManager.Actions.Time,
                            sw_all,
                            !AltTab.Enabled || (Program.Settings.AspectsControl.Enabled && !Program.Settings.AspectsControl.AltTab),
                            string.Format(Program.Localization.Strings.ThemeManager.Skip.Main, Program.Localization.Strings.Aspects.AltTab));

                        // WallpaperTone
                        Execute(() =>
                        {
                            WallpaperTone.Save_To_Registry(WallpaperTone_W12, "Win12", tv);
                            WallpaperTone.Save_To_Registry(WallpaperTone_W11, "Win11", tv);
                            WallpaperTone.Save_To_Registry(WallpaperTone_W10, "Win10", tv);
                            WallpaperTone.Save_To_Registry(WallpaperTone_W81, "Win8.1", tv);
                            WallpaperTone.Save_To_Registry(WallpaperTone_W8, "Win8", tv);
                            WallpaperTone.Save_To_Registry(WallpaperTone_W7, "Win7", tv);
                            WallpaperTone.Save_To_Registry(WallpaperTone_WVista, "WinVista", tv);
                            WallpaperTone.Save_To_Registry(WallpaperTone_WXP, "WinXP", tv);

                            if (Wallpaper.Enabled)
                            {
                                if (OS.W12 & WallpaperTone_W12.Enabled)
                                    WallpaperTone_W12.Apply(tv);

                                if (OS.W11 & WallpaperTone_W11.Enabled)
                                    WallpaperTone_W11.Apply(tv);

                                if (OS.W10 & WallpaperTone_W10.Enabled)
                                    WallpaperTone_W10.Apply(tv);

                                if (OS.W81 & WallpaperTone_W81.Enabled)
                                    WallpaperTone_W81.Apply(tv);

                                if (OS.W8 & WallpaperTone_W8.Enabled)
                                    WallpaperTone_W8.Apply(tv);

                                if (OS.W7 & WallpaperTone_W7.Enabled)
                                    WallpaperTone_W7.Apply(tv);

                                if (OS.WVista & WallpaperTone_WVista.Enabled)
                                    WallpaperTone_WVista.Apply(tv);

                                if (OS.WXP & WallpaperTone_WXP.Enabled)
                                    WallpaperTone_WXP.Apply(tv);
                            }

                        }, treeView,
                        string.Format(Program.Localization.Strings.ThemeManager.Actions.Applying_Feature, Program.Localization.Strings.Aspects.WallpaperTone),
                        string.Format(Program.Localization.Strings.ThemeManager.Errors.Error, Program.Localization.Strings.Aspects.WallpaperTone),
                        Program.Localization.Strings.ThemeManager.Actions.Time,
                        sw_all,
                        !Wallpaper.Enabled || (Program.Settings.AspectsControl.Enabled && !Program.Settings.AspectsControl.Wallpaper),
                        string.Format(Program.Localization.Strings.ThemeManager.Skip.Main, Program.Localization.Strings.Aspects.WallpaperTone));

                        #region Consoles

                        Execute(() => Apply_CommandPrompt(tv), treeView,
                            string.Format(Program.Localization.Strings.ThemeManager.Actions.Applying_Feature, Program.Localization.Strings.Aspects.CommandPrompt),
                            string.Format(Program.Localization.Strings.ThemeManager.Errors.Error, Program.Localization.Strings.Aspects.CommandPrompt),
                            Program.Localization.Strings.ThemeManager.Actions.Time,
                            sw_all,
                            !CommandPrompt.Enabled || (Program.Settings.AspectsControl.Enabled && !Program.Settings.AspectsControl.Consoles),
                            string.Format(Program.Localization.Strings.ThemeManager.Skip.Main, Program.Localization.Strings.Aspects.CommandPrompt));

                        Execute(() => Apply_PowerShell86(tv), treeView,
                            string.Format(Program.Localization.Strings.ThemeManager.Actions.Applying_Feature, Program.Localization.Strings.Aspects.PowerShellx86),
                            string.Format(Program.Localization.Strings.ThemeManager.Errors.Error, Program.Localization.Strings.Aspects.PowerShellx86),
                            Program.Localization.Strings.ThemeManager.Actions.Time,
                            sw_all,
                            !PowerShellx86.Enabled || (Program.Settings.AspectsControl.Enabled && !Program.Settings.AspectsControl.Consoles),
                            string.Format(Program.Localization.Strings.ThemeManager.Skip.Main, Program.Localization.Strings.Aspects.PowerShellx86));

                        Execute(() => Apply_PowerShell64(tv), treeView,
                            string.Format(Program.Localization.Strings.ThemeManager.Actions.Applying_Feature, Program.Localization.Strings.Aspects.PowerShellx64),
                            string.Format(Program.Localization.Strings.ThemeManager.Errors.Error, Program.Localization.Strings.Aspects.PowerShellx64),
                            Program.Localization.Strings.ThemeManager.Actions.Time,
                            sw_all,
                            !PowerShellx64.Enabled || (Program.Settings.AspectsControl.Enabled && !Program.Settings.AspectsControl.Consoles),
                            string.Format(Program.Localization.Strings.ThemeManager.Skip.Main, Program.Localization.Strings.Aspects.PowerShellx64));
                        #endregion

                        #region Windows Terminal
                        Stopwatch sw = new();
                        sw.Reset();
                        sw.Start();
                        if (OS.W12 || OS.W11 || OS.W10)
                        {
                            if (ReportProgress)
                            {
                                if (Program.Settings.AspectsControl.Enabled && !!Program.Settings.AspectsControl.WinTerminals)
                                {
                                    ThemeLog.AddNode(treeView, $"{DateTime.Now.ToLongTimeString()}: {Program.Localization.Strings.ThemeManager.Skip.Terminals}", "skip");
                                }

                                else if (Terminal.Enabled & TerminalPreview.Enabled)
                                {
                                    ThemeLog.AddNode(treeView, $"{DateTime.Now.ToLongTimeString()}: {Program.Localization.Strings.ThemeManager.Check.Terminals}", "info");
                                }

                                else if (Terminal.Enabled)
                                {
                                    ThemeLog.AddNode(treeView, $"{DateTime.Now.ToLongTimeString()}: {string.Format(Program.Localization.Strings.ThemeManager.Skip.Main, Program.Localization.Strings.Aspects.TerminalStable)}", "skip");
                                    ThemeLog.AddNode(treeView, $"{DateTime.Now.ToLongTimeString()}: {string.Format(Program.Localization.Strings.ThemeManager.Check.Terminal, Program.Localization.Strings.Aspects.TerminalStable)}", "info");
                                }

                                else if (TerminalPreview.Enabled)
                                {
                                    ThemeLog.AddNode(treeView, $"{DateTime.Now.ToLongTimeString()}: {string.Format(Program.Localization.Strings.ThemeManager.Skip.Main, Program.Localization.Strings.Aspects.TerminalPreview)}", "skip");
                                    ThemeLog.AddNode(treeView, $"{DateTime.Now.ToLongTimeString()}: {string.Format(Program.Localization.Strings.ThemeManager.Check.Terminal, Program.Localization.Strings.Aspects.TerminalPreview)}", "info");
                                }

                                else
                                {
                                    ThemeLog.AddNode(treeView, $"{DateTime.Now.ToLongTimeString()}: {Program.Localization.Strings.ThemeManager.Skip.Terminals}", "skip");
                                }

                            }

                            // Get Terminal JSON file path (either from the installed path or from the redirected path if enabled in WinPaletter settings)
                            string TerDir;
                            string TerPreDir;

                            if (!Program.Settings.WindowsTerminals.Path_Deflection)
                            {
                                TerDir = SysPaths.TerminalJSON;
                                TerPreDir = SysPaths.TerminalPreviewJSON;
                            }
                            else
                            {
                                if (File.Exists(Program.Settings.WindowsTerminals.Terminal_Stable_Path))
                                {
                                    TerDir = Program.Settings.WindowsTerminals.Terminal_Stable_Path;
                                }
                                else
                                {
                                    TerDir = SysPaths.TerminalJSON;
                                }

                                if (File.Exists(Program.Settings.WindowsTerminals.Terminal_Preview_Path))
                                {
                                    TerPreDir = Program.Settings.WindowsTerminals.Terminal_Preview_Path;
                                }
                                else
                                {
                                    TerPreDir = SysPaths.TerminalPreviewJSON;
                                }
                            }

                            if (Terminal.Enabled && !Program.Settings.AspectsControl.Enabled && !(Program.Settings.AspectsControl.Enabled && !!Program.Settings.AspectsControl.WinTerminals))
                            {
                                if (File.Exists(TerDir))
                                {
                                    try
                                    {
                                        ThemeLog.AddNode(treeView, $"{DateTime.Now.ToLongTimeString()}: {string.Format(Program.Localization.Strings.ThemeManager.Actions.Applying_Feature, Program.Localization.Strings.Aspects.TerminalStable)}", "info");
                                        Terminal.Save(TerDir, WinTerminal.Mode.JSONFile);
                                        if (ReportProgress)
                                            ThemeLog.AddNode(treeView, string.Format(Program.Localization.Strings.ThemeManager.Actions.Time, sw.ElapsedMilliseconds / 1000d), "time");
                                    }
                                    catch (Exception ex)
                                    {
                                        sw.Stop();
                                        sw_all.Stop();
                                        _ErrorHappened = true;
                                        if (ReportProgress)
                                        {
                                            ThemeLog.AddNode(treeView, $"{DateTime.Now.ToLongTimeString()}: {string.Format(Program.Localization.Strings.ThemeManager.Errors.Error, Program.Localization.Strings.Aspects.TerminalStable)}", "error");
                                            AddException(string.Format(Program.Localization.Strings.ThemeManager.Errors.Error, Program.Localization.Strings.Aspects.TerminalStable), ex);
                                        }
                                        else
                                        {
                                            Forms.BugReport.Throw(ex);
                                        }

                                        sw.Start();
                                        sw_all.Start();
                                    }
                                }
                                else
                                {
                                    ThemeLog.AddNode(treeView, $"{DateTime.Now.ToLongTimeString()}: {string.Format(Program.Localization.Strings.ThemeManager.Skip.Terminal_JSONNotFound, Program.Localization.Strings.Aspects.TerminalStable)}", "skip");
                                }
                            }

                            if (TerminalPreview.Enabled && !Program.Settings.AspectsControl.Enabled && !(Program.Settings.AspectsControl.Enabled && !!Program.Settings.AspectsControl.WinTerminals))
                            {
                                if (File.Exists(TerPreDir))
                                {

                                    try
                                    {
                                        ThemeLog.AddNode(treeView, $"{DateTime.Now.ToLongTimeString()}: {string.Format(Program.Localization.Strings.ThemeManager.Actions.Applying_Feature, Program.Localization.Strings.Aspects.TerminalPreview)}", "info");
                                        TerminalPreview.Save(TerPreDir, WinTerminal.Mode.JSONFile, WinTerminal.Version.Preview);
                                        if (ReportProgress)
                                            ThemeLog.AddNode(treeView, string.Format(Program.Localization.Strings.ThemeManager.Actions.Time, sw.ElapsedMilliseconds / 1000d), "time");
                                    }
                                    catch (Exception ex)
                                    {
                                        sw.Stop();
                                        sw_all.Stop();
                                        _ErrorHappened = true;
                                        if (ReportProgress)
                                        {
                                            ThemeLog.AddNode(treeView, $"{DateTime.Now.ToLongTimeString()}: {string.Format(Program.Localization.Strings.ThemeManager.Errors.Error, Program.Localization.Strings.Aspects.TerminalPreview)}", "error");
                                            AddException(string.Format(Program.Localization.Strings.ThemeManager.Errors.Error, Program.Localization.Strings.Aspects.TerminalPreview), ex);
                                        }
                                        else
                                        {
                                            Forms.BugReport.Throw(ex);
                                        }

                                        sw.Start();
                                        sw_all.Start();
                                    }
                                }
                                else
                                {
                                    ThemeLog.AddNode(treeView, $"{DateTime.Now.ToLongTimeString()}: {string.Format(Program.Localization.Strings.ThemeManager.Skip.Terminal_JSONNotFound, Program.Localization.Strings.Aspects.TerminalPreview)}", "skip");
                                }
                            }
                        }

                        else
                        {
                            ThemeLog.AddNode(treeView, $"{DateTime.Now.ToLongTimeString()}: {Program.Localization.Strings.ThemeManager.Skip.Terminals_NotSupported}", "skip");
                        }
                        sw.Stop();
                        #endregion

                        // ScreenSaver
                        Execute(() => ScreenSaver.Apply(tv), treeView,
                            string.Format(Program.Localization.Strings.ThemeManager.Actions.Applying_Feature, Program.Localization.Strings.Aspects.ScreenSaver),
                            string.Format(Program.Localization.Strings.ThemeManager.Errors.Error, Program.Localization.Strings.Aspects.ScreenSaver),
                            Program.Localization.Strings.ThemeManager.Actions.Time,
                            sw_all,
                            !ScreenSaver.Enabled || (Program.Settings.AspectsControl.Enabled && !Program.Settings.AspectsControl.ScreenSaver),
                            string.Format(Program.Localization.Strings.ThemeManager.Skip.Main, Program.Localization.Strings.Aspects.ScreenSaver));

                        // Sounds
                        Execute(() => Sounds.Apply(tv), treeView,
                            string.Format(Program.Localization.Strings.ThemeManager.Actions.Applying_Feature, Program.Localization.Strings.Aspects.Sounds),
                            string.Format(Program.Localization.Strings.ThemeManager.Errors.Error, Program.Localization.Strings.Aspects.Sounds),
                            Program.Localization.Strings.ThemeManager.Actions.Time,
                            sw_all,
                            !Sounds.Enabled || (Program.Settings.AspectsControl.Enabled && !Program.Settings.AspectsControl.Sounds),
                            string.Format(Program.Localization.Strings.ThemeManager.Skip.Main, Program.Localization.Strings.Aspects.Sounds));

                        // Cursors
                        Execute(() => Cursors.Apply(tv), treeView,
                            string.Empty,
                            string.Empty,
                            Program.Localization.Strings.ThemeManager.Actions.Time,
                            sw_all,
                            !Cursors.Enabled || (Program.Settings.AspectsControl.Enabled && !Program.Settings.AspectsControl.Cursors),
                            string.Format(Program.Localization.Strings.ThemeManager.Skip.Main, Program.Localization.Strings.Aspects.Cursors));

                        // Icons
                        Execute(() => Icons.Apply(tv), treeView,
                            string.Format(Program.Localization.Strings.ThemeManager.Actions.Applying_Feature, Program.Localization.Strings.Aspects.Icons),
                            string.Format(Program.Localization.Strings.ThemeManager.Errors.Error, Program.Localization.Strings.Aspects.Icons),
                            Program.Localization.Strings.ThemeManager.Actions.Time,
                            sw_all,
                            !Icons.Enabled || (Program.Settings.AspectsControl.Enabled && !Program.Settings.AspectsControl.Icons),
                            string.Format(Program.Localization.Strings.ThemeManager.Skip.Main, Program.Localization.Strings.Aspects.Icons));

                        // Update LogonUI wallpaper in HKEY_USERS\.DEFAULT
                        if (Program.Settings.ThemeApplyingBehavior.Desktop_HKU_DEFAULT == Settings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite)
                        {
                            Execute(() =>
                            {
                                WriteReg(tv, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop", "Wallpaper", ReadReg(@"HKEY_CURRENT_USER\Control Panel\Desktop", "Wallpaper", string.Empty), RegistryValueKind.String);
                                WriteReg(tv, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop", "WallpaperStyle", ReadReg(@"HKEY_CURRENT_USER\Control Panel\Desktop", "WallpaperStyle", "2"), RegistryValueKind.String);
                                WriteReg(tv, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop", "TileWallpaper", ReadReg(@"HKEY_CURRENT_USER\Control Panel\Desktop", "TileWallpaper", "0"), RegistryValueKind.String);
                                WriteReg(tv, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop", "Pattern", ReadReg(@"HKEY_CURRENT_USER\Control Panel\Desktop", "Pattern", string.Empty), RegistryValueKind.String);
                            }, treeView,
                            string.Format(Program.Localization.Strings.ThemeManager.Actions.Applying_Feature_AllUsers, Program.Localization.Strings.Aspects.Wallpaper),
                            string.Format(Program.Localization.Strings.ThemeManager.Errors.Error_AllUsers, Program.Localization.Strings.Aspects.Wallpaper),
                            Program.Localization.Strings.ThemeManager.Actions.Time);
                        }

                        else if (Program.Settings.ThemeApplyingBehavior.Desktop_HKU_DEFAULT == Settings.Structures.ThemeApplyingBehavior.OverwriteOptions.RestoreDefaults)
                        {
                            Execute(() =>
                            {
                                WriteReg(tv, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop", "Wallpaper", string.Empty, RegistryValueKind.String);
                                WriteReg(tv, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop", "WallpaperStyle", "2", RegistryValueKind.String);
                                WriteReg(tv, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop", "TileWallpaper", "0", RegistryValueKind.String);
                                WriteReg(tv, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop", "Pattern", string.Empty, RegistryValueKind.String);
                            }, treeView,
                            string.Format(Program.Localization.Strings.ThemeManager.Actions.Applying_Feature_AllUsers, Program.Localization.Strings.Aspects.Wallpaper),
                            string.Format(Program.Localization.Strings.ThemeManager.Errors.Error_AllUsers, Program.Localization.Strings.Aspects.Wallpaper),
                            Program.Localization.Strings.ThemeManager.Actions.Time);
                        }

                        Program.Log?.Write(LogEventLevel.Information, "Broadcasting system message to notify about the setting change (User32.UpdatePerUserSystemParameters(1, true)).");
                        User32.UpdatePerUserSystemParameters(1, true);

                        // Sometimes, this entry is set to 1 when manipulating preferences by User32.SystemParametersInfo
                        WriteReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Desktop", "AutoColorization", 0);

                        // Update User Preference Mask for HKEY_USERS\.DEFAULT
                        // Always make it the last operation
                        if (Program.Settings.ThemeApplyingBehavior.UPM_HKU_DEFAULT) Win32.Broadcast_UPM_ToDefUsers(tv);
                        Program.RefreshDWM(this);

                        //PostMessage((IntPtr)User32.HWND_BROADCAST, User32.WindowsMessages.WM_SYSCOLORCHANGE, UIntPtr.Zero, IntPtr.Zero);
                        //PostMessage((IntPtr)User32.HWND_BROADCAST, User32.WindowsMessages.WM_PALETTECHANGED, UIntPtr.Zero, IntPtr.Zero);
                        //PostMessage((IntPtr)User32.HWND_BROADCAST, User32.WindowsMessages.WM_DWMCOLORIZATIONCOLORCHANGED, UIntPtr.Zero, IntPtr.Zero);
                        //PostMessage((IntPtr)User32.HWND_BROADCAST, User32.WindowsMessages.WM_DWMCOMPOSITIONCHANGED, UIntPtr.Zero, IntPtr.Zero);
                        //PostMessage((IntPtr)User32.HWND_BROADCAST, User32.WindowsMessages.WM_THEMECHANGED, UIntPtr.Zero, IntPtr.Zero);
                        //PostMessage((IntPtr)User32.HWND_BROADCAST, User32.WindowsMessages.WM_SETTINGCHANGE, UIntPtr.Zero, IntPtr.Zero);
                        //PostMessage((IntPtr)User32.HWND_BROADCAST, User32.WindowsMessages.WM_WININICHANGE, UIntPtr.Zero, IntPtr.Zero);

                        // Add last log node
                        if (ReportProgress)
                        {
                            if (!_ErrorHappened && Exceptions.ThemeApply.Count == 0)
                            {
                                ThemeLog.AddNode(treeView, $"{DateTime.Now.ToLongTimeString()}: {string.Format(Program.Localization.Strings.ThemeManager.Actions.Applied, sw_all.ElapsedMilliseconds / 1000d)}", "success");
                            }
                            else
                            {
                                ThemeLog.AddNode(treeView, $"{DateTime.Now.ToLongTimeString()}: {string.Format(Program.Localization.Strings.ThemeManager.Actions.AppliedWithErrors, sw_all.ElapsedMilliseconds / 1000d)}", "warning");
                            }
                        }

                        sw_all.Reset();
                        sw_all.Stop();
                        wic.Undo();
                    }
                    break;

                case Source.File:
                    if (!Directory.Exists(Path.GetDirectoryName(file))) Directory.CreateDirectory(Path.GetDirectoryName(file));

                    if (Info.ExportResThemePack)
                    {
                        // Always clone theme manager as PackThemeResources will modify the current theme manager (Some paths will be converted into environment variables)
                        using (Manager TMx = Clone())
                        {
                            PackThemeResources(TMx, file, $"{new FileInfo(file).DirectoryName}\\{Path.GetFileNameWithoutExtension(file)}.wptp");
                        }
                    }

                    else { File.WriteAllText(file, ToString()); }

                    break;
            }
        }

        /// <summary>
        /// Serializes the WinPaletter theme file into a JSON string.
        /// </summary>
        /// <param name="ignoreCompression">If true, skips compression.</param>
        /// <returns>JSON string representation of the theme file, possibly compressed.</returns>
        public string ToString(bool ignoreCompression = false)
        {
            // Ensure AppVersion is up to date
            Info.AppVersion = Program.Version;

            JsonSerializerSettings sets = new()
            {
                ContractResolver = new PublicWritableOnlyContractResolver(),
                //DefaultValueHandling = DefaultValueHandling.Ignore, // optional
                //NullValueHandling = NullValueHandling.Ignore         // optional
            };

            string jsonText = JsonConvert.SerializeObject(this, Formatting.Indented, sets);

            if (Program.Settings.FileTypeManagement.CompressThemeFile && !ignoreCompression)
            {
                try
                {
                    return jsonText.Compress();
                }
                catch (Exception ex)
                {
                    Program.Log?.Write(LogEventLevel.Error, $"Compression faile", ex);
                    return jsonText;
                }
            }

            return jsonText;
        }

        private class PublicWritableOnlyContractResolver : DefaultContractResolver
        {
            protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
            {
                // Get all public instance properties
                var props = type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                .Where(p => p.CanWrite && p.GetSetMethod(false) != null) // must have a *public* setter
                                .Select(p => base.CreateProperty(p, memberSerialization))
                                .ToList();

                // Mark all as readable/writable to preserve JSON values
                foreach (var prop in props)
                {
                    prop.Readable = true;
                    prop.Writable = true;
                }

                return props;
            }
        }

        /// <summary>
        /// Create theme resources pack that contains images and sounds files not located inside Windows system directories
        /// </summary>
        /// <param name="TM">WinPaletter theme manager instance</param>
        /// <param name="ThemeFile">WinPaletter theme File</param>
        /// <param name="Pack">WinTheme resources pack File</param>
        public void PackThemeResources(Manager TM, string ThemeFile, string Pack)
        {
            Program.Log?.Write(LogEventLevel.Information, $"A query to make a theme resources pack for `{ThemeFile}` is made. It will be saved as `{Pack}`.");

            string cache = $"%WinPaletterAppData%\\ThemeResPack_Cache\\{string.Concat(TM.Info.ThemeName.Replace(" ", string.Empty).Split(Path.GetInvalidFileNameChars()))}\\";

            Program.Log?.Write(LogEventLevel.Information, $"Cache directory for the theme resources pack is `{cache}`.");

            // Key: zip entry path, Value: source file path.
            Dictionary<string, string> filesList = [];

            // Key: zip entry path, Value: source directory to compress recursively.
            Dictionary<string, string> dirsList = [];

            if (File.Exists(Pack)) File.Delete(Pack);

            string tempDir = $"{SysPaths.appData}\\Temp";
            if (!Directory.Exists(tempDir)) Directory.CreateDirectory(tempDir);
            foreach (string f in Directory.GetFiles(tempDir)) File.Delete(f);

            string windowsWeb = $@"{SysPaths.Windows}\Web";
            string windowsMedia = $@"{SysPaths.Windows}\media";
            string windowsCursors = $@"{SysPaths.Windows}\Cursors";
            string lunaPath = $@"{SysPaths.Windows}\Resources\Themes\Luna";
            string aeroPath = $@"{SysPaths.Windows}\Resources\Themes\Aero";

            // PHASE 1: Collect all property modifications and build filesList / dirsList

            // Register a file path property for inclusion in the pack.
            // excludePrefix: skip if value starts with this prefix.
            void RegisterPath(Func<string> getter, Action<string> setter, string entryName, string excludePrefix = null)
            {
                string val = getter();
                if (string.IsNullOrWhiteSpace(val)) return;
                if (excludePrefix != null && val.StartsWith(excludePrefix, StringComparison.OrdinalIgnoreCase)) return;

                string zipEntry = $"{cache}{entryName}{Path.GetExtension(val)}";

                if (File.Exists(val))
                {
                    Program.Log?.Write(LogEventLevel.Information, $"Modifying entry of `{entryName}` inside the theme resources pack to be `{zipEntry}`. The previous value was `{val}`.");
                    setter(zipEntry);
                }

                if (!filesList.ContainsKey(zipEntry)) filesList.Add(zipEntry, val);

                Program.Log?.Write(LogEventLevel.Information, $"Adding `{entryName}` (`{val}`) to the theme resources pack as `{zipEntry}`.");
            }

            // Extract icon from PE file to a temp .ico if needed, then queue for compression.
            void RegisterIcon(Func<string> getter, Action<string> setter, string iconName)
            {
                string targetProperty = getter();
                if (string.IsNullOrWhiteSpace(targetProperty)) return;
                if (targetProperty.StartsWith(SysPaths.imageres, StringComparison.OrdinalIgnoreCase) || targetProperty.StartsWith($"{SysPaths.System32}\\shell32.dll", StringComparison.OrdinalIgnoreCase)) return;

                string zipEntry = $"{cache}icons\\{iconName}";
                string iconFile = targetProperty.Split(',')[0];
                int iconIndex = targetProperty.Contains(",") ? int.Parse(targetProperty.Split(',')[1]) : 0;

                if (!File.Exists(iconFile)) return;

                Program.Log?.Write(LogEventLevel.Information, $"Adding icon `{targetProperty}` to the theme resources pack as `{zipEntry}`.");

                string sourcePath;
                if (!Path.GetExtension(iconFile).Equals(".ico", StringComparison.OrdinalIgnoreCase))
                {
                    string tempFile = $"{SysPaths.appData}\\Temp\\{iconName}";
                    using (Icon icon = PE.GetIcon(iconFile, iconIndex))
                    using (FileStream fs = new(tempFile, FileMode.OpenOrCreate, FileAccess.ReadWrite)) icon.Save(fs);
                    sourcePath = tempFile;
                }
                else
                {
                    sourcePath = iconFile;
                }

                if (!filesList.ContainsKey(zipEntry)) filesList.Add(zipEntry, sourcePath);

                Program.Log?.Write(LogEventLevel.Information, $"Icon `{iconName}` is extracted/located and added to list of files to be compressed.");

                setter(zipEntry);
            }

            // Queue a Visual Styles directory for compression and update the theme file property.
            void RegisterVisualStyles(bool enabled, Func<string> getThemeFile, Action<string> setThemeFile, string prefix, string excludePath)
            {
                if (!enabled) return;
                string themeFile = getThemeFile();
                if (string.IsNullOrWhiteSpace(themeFile) || !File.Exists(themeFile)) return;
                if (themeFile.StartsWith(excludePath, StringComparison.OrdinalIgnoreCase)) return;

                string zipEntry = $@"{cache}{prefix}_VS\{Path.GetFileName(themeFile)}";
                Program.Log?.Write(LogEventLevel.Information, $"Modifying entry of {prefix} Visual Style file inside the theme resources pack to be `{zipEntry}`. The previous value was `{themeFile}`.");
                setThemeFile(zipEntry);

                string dirName = new FileInfo(themeFile).Directory.FullName;
                if (!dirsList.ContainsKey(prefix)) dirsList.Add(prefix, dirName);

                Program.Log?.Write(LogEventLevel.Information, $"Queuing {prefix} Visual Style directory `{dirName}` for compression.");
            }

            // Visual Styles
            RegisterVisualStyles(TM.Windows12.VisualStyles.Enabled && TM.Windows12.VisualStyles.VisualStylesType == VisualStyles.DefaultVisualStyles.Custom, () => TM.Windows12.VisualStyles.ThemeFile, v => TM.Windows12.VisualStyles.ThemeFile = v, "W12", aeroPath);
            RegisterVisualStyles(TM.Windows11.VisualStyles.Enabled && TM.Windows11.VisualStyles.VisualStylesType == VisualStyles.DefaultVisualStyles.Custom, () => TM.Windows11.VisualStyles.ThemeFile, v => TM.Windows11.VisualStyles.ThemeFile = v, "W11", aeroPath);
            RegisterVisualStyles(TM.Windows10.VisualStyles.Enabled && TM.Windows10.VisualStyles.VisualStylesType == VisualStyles.DefaultVisualStyles.Custom, () => TM.Windows10.VisualStyles.ThemeFile, v => TM.Windows10.VisualStyles.ThemeFile = v, "W10", aeroPath);
            RegisterVisualStyles(TM.Windows81.VisualStyles.Enabled && TM.Windows81.VisualStyles.VisualStylesType == VisualStyles.DefaultVisualStyles.Custom, () => TM.Windows81.VisualStyles.ThemeFile, v => TM.Windows81.VisualStyles.ThemeFile = v, "W81", aeroPath);
            RegisterVisualStyles(TM.Windows8.VisualStyles.Enabled && TM.Windows8.VisualStyles.VisualStylesType == VisualStyles.DefaultVisualStyles.Custom, () => TM.Windows8.VisualStyles.ThemeFile, v => TM.Windows8.VisualStyles.ThemeFile = v, "W8", aeroPath);
            RegisterVisualStyles(TM.Windows7.VisualStyles.Enabled && TM.Windows7.VisualStyles.VisualStylesType == VisualStyles.DefaultVisualStyles.Custom, () => TM.Windows7.VisualStyles.ThemeFile, v => TM.Windows7.VisualStyles.ThemeFile = v, "W7", aeroPath);
            RegisterVisualStyles(TM.WindowsVista.VisualStyles.Enabled && TM.WindowsVista.VisualStyles.VisualStylesType == VisualStyles.DefaultVisualStyles.Custom, () => TM.WindowsVista.VisualStyles.ThemeFile, v => TM.WindowsVista.VisualStyles.ThemeFile = v, "WVista", aeroPath);
            RegisterVisualStyles(TM.WindowsXP.VisualStyles.Enabled && TM.WindowsXP.VisualStyles.VisualStylesType == VisualStyles.DefaultVisualStyles.Custom, () => TM.WindowsXP.VisualStyles.ThemeFile, v => TM.WindowsXP.VisualStyles.ThemeFile = v, "WXP", lunaPath);

            // LogonUI
            if (TM.LogonUI81.Enabled && TM.LogonUI81.Mode == Structures.LogonUI81.Sources.CustomImage) RegisterPath(() => TM.LogonUI81.ImagePath, v => TM.LogonUI81.ImagePath = v, "LogonUI81", windowsWeb);

            if (TM.LogonUI7.Enabled && TM.LogonUI7.Mode == Structures.LogonUI7.Sources.CustomImage) RegisterPath(() => TM.LogonUI7.ImagePath, v => TM.LogonUI7.ImagePath = v, "LogonUI7", windowsWeb);

            if (TM.LogonUIXP.Enabled && !TM.LogonUIXP.LogonUIEXEPath.Equals("logonUI.exe", StringComparison.OrdinalIgnoreCase) && File.Exists(TM.LogonUIXP.LogonUIEXEPath) && !TM.LogonUIXP.LogonUIEXEPath.StartsWith(SysPaths.System32, StringComparison.OrdinalIgnoreCase) && !TM.LogonUIXP.LogonUIEXEPath.StartsWith(SysPaths.SysWOW64, StringComparison.OrdinalIgnoreCase))
            {
                RegisterPath(() => TM.LogonUIXP.LogonUIEXEPath, v => TM.LogonUIXP.LogonUIEXEPath = v, "LogonUIXP_EXE");
            }

            // Windows Terminal
            void RegisterTerminalProfiles(WinTerminal terminal, string prefix)
            {
                RegisterPath(() => terminal.Profiles.Defaults.BackgroundImage, v => terminal.Profiles.Defaults.BackgroundImage = v, $"{prefix}_defprofile_backimg", windowsWeb);

                if (terminal.Profiles.Defaults.Icon?.Length > 1) RegisterPath(() => terminal.Profiles.Defaults.Icon, v => terminal.Profiles.Defaults.Icon = v, $"{prefix}_defprofile_icon", windowsWeb);

                foreach (WinTerminal.Types.Profile profile in terminal.Profiles.List)
                {
                    string safeName = string.Concat(profile.Name.Replace(" ", string.Empty).Split(Path.GetInvalidFileNameChars()));
                    RegisterPath(() => profile.BackgroundImage, v => profile.BackgroundImage = v, $"{prefix}_profile({safeName})_backimg", windowsWeb);

                    if (profile.Icon?.Length > 1) RegisterPath(() => profile.Icon, v => profile.Icon = v, $"{prefix}_profile({safeName})_icon", windowsWeb);
                }
            }

            if (TM.Terminal.Enabled) RegisterTerminalProfiles(TM.Terminal, "winterminal");
            if (TM.TerminalPreview.Enabled) RegisterTerminalProfiles(TM.TerminalPreview, "winterminal_preview");

            // WallpaperTone
            foreach (System.Reflection.PropertyInfo prop in typeof(Manager).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance).Where(p => p.PropertyType == typeof(WallpaperTone)))
            {
                WallpaperTone wallpaperTone = (WallpaperTone)prop.GetValue(TM);
                if (!wallpaperTone.Enabled) continue;

                System.Reflection.PropertyInfo localProp = prop;
                string entryName = $"wt_{localProp.Name.Replace("WallpaperTone_", string.Empty).ToLowerInvariant()}"; // Compatible with older versions

                RegisterPath(
                    () => ((WallpaperTone)localProp.GetValue(TM)).Image,
                    v => { WallpaperTone wt = (WallpaperTone)localProp.GetValue(TM); wt.Image = v; localProp.SetValue(TM, wt); },
                    entryName,
                    windowsWeb
                );
            }

            // ScreenSaver
            if (TM.ScreenSaver.Enabled) RegisterPath(() => TM.ScreenSaver.File, v => TM.ScreenSaver.File = v, "scrsvr");

            // Cursors
            if (TM.Cursors.Enabled)
            {
                foreach (System.Reflection.PropertyInfo prop in typeof(Structures.Cursors).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance).Where(p => p.PropertyType == typeof(Structures.Cursor) && p.CanRead))
                {
                    System.Reflection.PropertyInfo localProp = prop;
                    Structures.Cursor cursor = (Structures.Cursor)localProp.GetValue(TM.Cursors);
                    if (!cursor.UseFromFile || !File.Exists(cursor.File)) continue;

                    RegisterPath(
                        () => ((Structures.Cursor)localProp.GetValue(TM.Cursors)).File,
                        v => { Structures.Cursor c = (Structures.Cursor)localProp.GetValue(TM.Cursors); c.File = v; localProp.SetValue(TM.Cursors, c); },
                        localProp.Name,
                        windowsCursors
                    );
                }
            }

            // Sounds
            if (TM.Sounds.Enabled)
            {
                foreach (System.Reflection.PropertyInfo prop in typeof(Sounds).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance).Where(p => p.PropertyType == typeof(string) && p.CanRead && p.CanWrite))
                {
                    System.Reflection.PropertyInfo localProp = prop;
                    RegisterPath(
                        () => (string)localProp.GetValue(TM.Sounds),
                        v => localProp.SetValue(TM.Sounds, v),
                        localProp.Name,
                        windowsMedia
                    );
                }
            }

            // Icons
            if (TM.Icons.Enabled)
            {
                RegisterIcon(() => TM.Icons.Computer, v => TM.Icons.Computer = v, "computer.ico");
                RegisterIcon(() => TM.Icons.User, v => TM.Icons.User = v, "user.ico");
                RegisterIcon(() => TM.Icons.Network, v => TM.Icons.Network = v, "network.ico");
                RegisterIcon(() => TM.Icons.ControlPanel, v => TM.Icons.ControlPanel = v, "controlpanel.ico");
                RegisterIcon(() => TM.Icons.RecycleBinEmpty, v => TM.Icons.RecycleBinEmpty = v, "recyclebinempty.ico");
                RegisterIcon(() => TM.Icons.RecycleBinFull, v => TM.Icons.RecycleBinFull = v, "recyclebinfull.ico");
                RegisterIcon(() => TM.Icons.SystemDriveIcon, v => TM.Icons.SystemDriveIcon = v, "systemdriveicon.ico");

                void RegisterWrapperIcons(Dictionary<string, string> wrapperDict, string prefix)
                {
                    foreach (string key in wrapperDict.Keys.ToArray())
                        RegisterIcon(() => wrapperDict[key], v => wrapperDict[key] = v, $"{prefix}_{key}.ico");
                }

                RegisterWrapperIcons(TM.Icons.Shell32Wrapper, "shell32");
                RegisterWrapperIcons(TM.Icons.ControlPanelWrapper, "controlpanel");
                RegisterWrapperIcons(TM.Icons.ExplorerWrapper, "explorer");
            }

            // Wallpaper (single image)
            if (TM.Wallpaper.Enabled && TM.Wallpaper.WallpaperType == Wallpaper.WallpaperTypes.Picture) RegisterPath(() => TM.Wallpaper.ImageFile, v => TM.Wallpaper.ImageFile = v, "wallpaper_file", windowsWeb);

            // Wallpaper slideshow
            if (TM.Wallpaper.Enabled && TM.Wallpaper.WallpaperType == Wallpaper.WallpaperTypes.SlideShow)
            {
                if (TM.Wallpaper.SlideShow_Folder_or_ImagesList)
                {
                    string rootPath = TM.Wallpaper.Wallpaper_Slideshow_ImagesRootPath;
                    if (!string.IsNullOrWhiteSpace(rootPath) && !rootPath.StartsWith(windowsWeb, StringComparison.OrdinalIgnoreCase))
                    {
                        TM.Wallpaper.Wallpaper_Slideshow_ImagesRootPath = $"{cache}wallpapers_slideshow";
                        Program.Log?.Write(LogEventLevel.Information, $"Modifying entry of Wallpaper Slideshow root folder inside the theme resources pack to be `{TM.Wallpaper.Wallpaper_Slideshow_ImagesRootPath}`. The previous value was `{rootPath}`.");

                        if (!dirsList.ContainsKey("wallpapers_slideshow")) dirsList.Add("wallpapers_slideshow", rootPath);
                    }
                }
                else
                {
                    string[] arr = [.. TM.Wallpaper.Wallpaper_Slideshow_Images];
                    if (arr.Length > 0 && !arr[0].StartsWith(windowsWeb, StringComparison.OrdinalIgnoreCase))
                    {
                        TM.Wallpaper.Wallpaper_Slideshow_ImagesRootPath = $"{cache}WallpapersList";
                        Program.Log?.Write(LogEventLevel.Information, $"Modifying entry of Wallpaper Slideshow images list inside the theme resources pack to be `{TM.Wallpaper.Wallpaper_Slideshow_ImagesRootPath}`. The previous value was `{string.Join(", ", arr)}`.");

                        TM.Wallpaper.Wallpaper_Slideshow_Images = [];
                        for (int i = 0; i < arr.Length; i++)
                        {
                            string img = arr[i];
                            if (string.IsNullOrWhiteSpace(img) || img.StartsWith(windowsWeb, StringComparison.OrdinalIgnoreCase)) continue;

                            string zipEntry = $@"{cache}WallpapersList\wallpaperlist_{i}_file{Path.GetExtension(img)}";
                            Program.Log?.Write(LogEventLevel.Information, $"Modifying entry of Wallpaper Slideshow image `{img}` inside the theme resources pack to be `{zipEntry}`.");

                            if (!File.Exists(img)) continue;
                            TM.Wallpaper.Wallpaper_Slideshow_Images = [.. TM.Wallpaper.Wallpaper_Slideshow_Images, zipEntry];

                            if (!filesList.ContainsKey(zipEntry)) filesList.Add(zipEntry, img);
                        }
                    }
                }
            }

            // PHASE 2: Write the modified TM to ThemeFile, then compress everything

            File.WriteAllText(ThemeFile, TM.ToString());
            Program.Log?.Write(LogEventLevel.Information, $"Theme Manager file `{ThemeFile}` is written with modified entries suitable for the created theme resources pack.");

            using (ZipArchive archive = ZipFile.Open(Pack, ZipArchiveMode.Create))
            {
                // Compress individual queued files.
                foreach (KeyValuePair<string, string> file in filesList)
                {
                    if (!File.Exists(file.Value)) continue;
                    archive.CreateEntryFromFile(file.Value, Path.GetFileName(file.Key), CompressionLevel.Optimal);
                    Program.Log?.Write(LogEventLevel.Information, $"File `{file.Value}` is compressed inside themes resources pack as `{file.Key}`.");
                }

                // Compress queued Visual Style directories.
                foreach (KeyValuePair<string, string> dir in dirsList)
                {
                    string prefix = dir.Key;
                    string dirName = dir.Value;

                    foreach (string file in Directory.EnumerateFiles(dirName, "*.*", SearchOption.AllDirectories))
                    {
                        if (!File.Exists(file)) continue;
                        string entryName = prefix.Contains("_VS") // Visual Styles use prefix_VS\relative, slideshow uses flat name
                            ? $"{prefix}{file.Replace(dirName, string.Empty)}"
                            : $@"{prefix}\{Path.GetFileName(file)}";
                        archive.CreateEntryFromFile(file, entryName, CompressionLevel.Optimal);
                        Program.Log?.Write(LogEventLevel.Information, $"Compressing `{file}` to the theme resources pack as `{entryName}`.");
                    }
                }
            }

            foreach (string file in Directory.GetFiles($"{SysPaths.appData}\\Temp"))
            {
                Program.Log?.Write(LogEventLevel.Information, $"Deleting temporary file: {file}");
                File.Delete(file);
            }
        }
    }
}