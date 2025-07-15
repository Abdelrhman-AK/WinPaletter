using Microsoft.Win32;
using Newtonsoft.Json.Linq;
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
                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Applying WinPaletter theme to registry for user `{User.Domain}\\{User.Name}`.");
                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Theme name: {Info.ThemeName}");

                        // If theme backup option is enabled, backup it before applying
                        if (Program.Settings.BackupTheme.Enabled && Program.Settings.BackupTheme.AutoBackupOnApply)
                        {
                            string filename = Program.GetUniqueFileName($"{Program.Settings.BackupTheme.BackupPath}\\OnThemeApply", $"{Info.ThemeName}_{DateTime.Now.Hour}.{DateTime.Now.Minute}.{DateTime.Now.Second}.wpth");

                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Backing up WinPaletter theme before applying it as `{filename}`.");

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
                                ThemeLog.AddNode(treeView, $"{Program.Lang.Strings.ThemeManager.Actions.RestorePoint0}", "info");
                                ThemeLog.AddNode(treeView, $"{Program.Lang.Strings.ThemeManager.Actions.RestorePoint1}", "info");
                                ThemeLog.AddNode(treeView, $"{Program.Lang.Strings.ThemeManager.Actions.RestorePoint2}", "time");
                            }

                            bool SR_reult = SystemRestoreHelper.CreateRestorePoint(string.Format(Program.Lang.Strings.General.RestorePoint_Theme, Info.ThemeName));

                            if (ReportProgress && SR_reult)
                            {
                                ThemeLog.AddNode(treeView, $"{string.Format(Program.Lang.Strings.ThemeManager.Actions.RestorePoint3, sw_all.ElapsedMilliseconds / 1000d)}", "time");
                            }
                        }

                        if (ReportProgress)
                        {
                            string OS_str;

                            if (OS.W12) { OS_str = Program.Lang.Strings.Windows.W12; }

                            else if (OS.W11) { OS_str = Program.Lang.Strings.Windows.W11; }

                            else if (OS.W10) { OS_str = Program.Lang.Strings.Windows.W10; }

                            else if (OS.W81) { OS_str = Program.Lang.Strings.Windows.W81; }

                            else if (OS.W81) { OS_str = Program.Lang.Strings.Windows.W8; }

                            else if (OS.W7) { OS_str = Program.Lang.Strings.Windows.W7; }

                            else if (OS.WVista) { OS_str = Program.Lang.Strings.Windows.WVista; }

                            else if (OS.WXP) { OS_str = Program.Lang.Strings.Windows.WXP; }

                            else { OS_str = Program.Lang.Strings.Windows.Undefined; }

                            ThemeLog.AddNode(treeView, $"{string.Format(Program.Lang.Strings.ThemeManager.Actions.ApplyOS, OS_str)}", "info");

                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"WinPaletter will apply the theme as if you are using {OS_str}");

                            ThemeLog.AddNode(treeView, $"{DateTime.Now.ToLongTimeString()}: {Program.Lang.Strings.ThemeManager.Actions.Applying_Started}", "info");

                            if (!Program.Elevated)
                            {
                                ThemeLog.AddNode(treeView, $"{Program.Lang.Strings.ThemeManager.Actions.Admin_Msg0}", "admin");
                                ThemeLog.AddNode(treeView, $"{Program.Lang.Strings.ThemeManager.Actions.Admin_Msg1}", "admin");
                            }
                        }

                        // Reset to default Windows theme if requested
                        if (resetToDefault)
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, "Resetting Windows theme to default before applying a new WinPaletter theme.");

                            Execute(() =>
                            {
                                using (Manager def = Theme.Default.Get())
                                {
                                    def.Wallpaper.Enabled = false;
                                    def.Save(Source.Registry);
                                }

                            },
                            treeView, Program.Lang.Strings.ThemeManager.Actions.ThemeReset, Program.Lang.Strings.ThemeManager.Errors.ThemeReset, Program.Lang.Strings.ThemeManager.Actions.Time, sw_all);
                        }

                        // Save toggles states (toggle states are saved before applying the theme to make WinPaletter apply enabled features and skip disabled features)

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, "Saving toggles states before applying the theme.");

                        ThemeLog.AddNode(treeView, $"{Program.Lang.Strings.ThemeManager.Actions.SavingToggles}", "info");
                        AppTheme.SaveToggleState(tv);
                        Wallpaper.SaveToggleState(tv);
                        Windows12.SaveToggleState("12", tv);
                        Windows11.SaveToggleState("11", tv);
                        Windows10.SaveToggleState("10", tv);
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

                        EditReg(treeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Terminals", "Terminal_CMD_Enabled", CommandPrompt.Enabled);
                        EditReg(treeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Terminals", "Terminal_PS_32_Enabled", PowerShellx86.Enabled);
                        EditReg(treeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Terminals", "Terminal_PS_64_Enabled", PowerShellx64.Enabled);
                        EditReg(treeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Terminals", "Terminal_Stable_Enabled", Terminal.Enabled);
                        EditReg(treeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Terminals", "Terminal_Preview_Enabled", TerminalPreview.Enabled);

                        // WinTheme info
                        Execute(() => Info.Apply(tv), treeView, Program.Lang.Strings.ThemeManager.Actions.SavingInfo, Program.Lang.Strings.ThemeManager.Errors.SavingInfo, Program.Lang.Strings.ThemeManager.Actions.Time, sw_all);

                        // WinPaletter application theme
                        Execute(() => AppTheme.Apply(tv), treeView,
                            string.Format(Program.Lang.Strings.ThemeManager.Actions.Applying_Feature, Program.Lang.Strings.Aspects.AppTheme),
                            string.Format(Program.Lang.Strings.ThemeManager.Errors.Error, Program.Lang.Strings.Aspects.AppTheme),
                            Program.Lang.Strings.ThemeManager.Actions.Time,
                            sw_all,
                            !AppTheme.Enabled,
                            string.Format(Program.Lang.Strings.ThemeManager.Skip.Main, Program.Lang.Strings.Aspects.AppTheme));

                        // Wallpaper
                        // Make Wallpaper before the following LogonUI items, to make a logonUI that depends on current wallpaper gets the correct File
                        Execute(() => Wallpaper.Apply(false, tv),
                            treeView,
                            string.Format(Program.Lang.Strings.ThemeManager.Actions.Applying_Feature, Program.Lang.Strings.Aspects.Wallpaper),
                            string.Format(Program.Lang.Strings.ThemeManager.Errors.Error, Program.Lang.Strings.Aspects.Wallpaper),
                            Program.Lang.Strings.ThemeManager.Actions.Time,
                            sw_all,
                            !Wallpaper.Enabled || (Program.Settings.AspectsControl.Enabled && !Program.Settings.AspectsControl.Wallpaper),
                            string.Format(Program.Lang.Strings.ThemeManager.Skip.Main, Program.Lang.Strings.Aspects.Wallpaper));

                        // Apply Windows 12 execlusive features (Colors, lock screen and visual styles)
                        if (OS.W12)
                        {
                            Execute(() => Windows12.Apply("12", tv), treeView,
                                string.Format(Program.Lang.Strings.ThemeManager.Actions.Theme, Program.Lang.Strings.Windows.W12),
                                string.Format(Program.Lang.Strings.ThemeManager.Errors.Error, string.Format(Program.Lang.Strings.Aspects.WinTheme, Program.Lang.Strings.Windows.W12)),
                                Program.Lang.Strings.ThemeManager.Actions.Time,
                                sw_all,
                                !Windows12.Enabled || (Program.Settings.AspectsControl.Enabled && !Program.Settings.AspectsControl.WinColors),
                                string.Format(Program.Lang.Strings.ThemeManager.Skip.Main, string.Format(Program.Lang.Strings.Aspects.WinTheme, Program.Lang.Strings.Windows.W12)));

                            Execute(() => LogonUI12.Apply("12", tv), treeView,
                                string.Format(Program.Lang.Strings.ThemeManager.Actions.Applying_Feature_ForOS, Program.Lang.Strings.Windows.W12, Program.Lang.Strings.Aspects.LockScreen),
                                string.Format(Program.Lang.Strings.ThemeManager.Errors.Error, Program.Lang.Strings.Aspects.LockScreen),
                                Program.Lang.Strings.ThemeManager.Actions.Time,
                                sw_all,
                                !LogonUI12.Enabled || (Program.Settings.AspectsControl.Enabled && !Program.Settings.AspectsControl.LogonUI),
                                string.Format(Program.Lang.Strings.ThemeManager.Skip.Main, Program.Lang.Strings.Aspects.LockScreen));
                        }

                        // Apply Windows 11 execlusive features (Colors, lock screen and visual styles)
                        if (OS.W11)
                        {
                            Execute(() => Windows11.Apply("11", tv), treeView,
                                string.Format(Program.Lang.Strings.ThemeManager.Actions.Theme, Program.Lang.Strings.Windows.W11),
                                string.Format(Program.Lang.Strings.ThemeManager.Errors.Error, string.Format(Program.Lang.Strings.Aspects.WinTheme, Program.Lang.Strings.Windows.W11)),
                                Program.Lang.Strings.ThemeManager.Actions.Time,
                                sw_all,
                                !Windows11.Enabled || (Program.Settings.AspectsControl.Enabled && !Program.Settings.AspectsControl.WinColors),
                                string.Format(Program.Lang.Strings.ThemeManager.Skip.Main, string.Format(Program.Lang.Strings.Aspects.WinTheme, Program.Lang.Strings.Windows.W11)));

                            Execute(() => LogonUI11.Apply("11", tv), treeView,
                                string.Format(Program.Lang.Strings.ThemeManager.Actions.Applying_Feature_ForOS, Program.Lang.Strings.Windows.W11, Program.Lang.Strings.Aspects.LockScreen),
                                string.Format(Program.Lang.Strings.ThemeManager.Errors.Error, Program.Lang.Strings.Aspects.LockScreen),
                                Program.Lang.Strings.ThemeManager.Actions.Time,
                                sw_all,
                                !LogonUI11.Enabled || (Program.Settings.AspectsControl.Enabled && !Program.Settings.AspectsControl.LogonUI),
                                string.Format(Program.Lang.Strings.ThemeManager.Skip.Main, Program.Lang.Strings.Aspects.LockScreen));
                        }

                        // Apply Windows 10 execlusive features (Colors, lock screen and visual styles)
                        if (OS.W10)
                        {
                            Execute(() => Windows10.Apply("10", tv), treeView,
                                string.Format(Program.Lang.Strings.ThemeManager.Actions.Theme, Program.Lang.Strings.Windows.W10),
                                string.Format(Program.Lang.Strings.ThemeManager.Errors.Error, string.Format(Program.Lang.Strings.Aspects.WinTheme, Program.Lang.Strings.Windows.W10)),
                                Program.Lang.Strings.ThemeManager.Actions.Time,
                                sw_all,
                                !Windows10.Enabled || (Program.Settings.AspectsControl.Enabled && !Program.Settings.AspectsControl.WinColors),
                                string.Format(Program.Lang.Strings.ThemeManager.Skip.Main, string.Format(Program.Lang.Strings.Aspects.WinTheme, Program.Lang.Strings.Windows.W10)));

                            Execute(() => LogonUI10.Apply("10", tv), treeView,
                                string.Format(Program.Lang.Strings.ThemeManager.Actions.Applying_Feature_ForOS, Program.Lang.Strings.Windows.W10, Program.Lang.Strings.Aspects.LockScreen),
                                string.Format(Program.Lang.Strings.ThemeManager.Errors.Error, Program.Lang.Strings.Aspects.LockScreen),
                                Program.Lang.Strings.ThemeManager.Actions.Time,
                                sw_all,
                                !LogonUI10.Enabled || (Program.Settings.AspectsControl.Enabled && !Program.Settings.AspectsControl.LogonUI),
                                string.Format(Program.Lang.Strings.ThemeManager.Skip.Main, Program.Lang.Strings.Aspects.LockScreen));
                        }

                        // Apply Windows 8.1 execlusive features (Colors, lock screen and visual styles)
                        if (OS.W81)
                        {
                            Execute(() => Windows81.Apply(this, tv), treeView,
                                string.Format(Program.Lang.Strings.ThemeManager.Actions.Theme, Program.Lang.Strings.Windows.W81),
                                string.Format(Program.Lang.Strings.ThemeManager.Errors.Error, string.Format(Program.Lang.Strings.Aspects.WinTheme, Program.Lang.Strings.Windows.W81)),
                                Program.Lang.Strings.ThemeManager.Actions.Time,
                                sw_all,
                                !Windows81.Enabled || (Program.Settings.AspectsControl.Enabled && !Program.Settings.AspectsControl.WinColors),
                                string.Format(Program.Lang.Strings.ThemeManager.Skip.Main, string.Format(Program.Lang.Strings.Aspects.WinTheme, Program.Lang.Strings.Windows.W81)));

                            Execute(() => LogonUI81.Apply(treeView), treeView,
                                string.Format(Program.Lang.Strings.ThemeManager.Actions.Applying_Feature_ForOS, Program.Lang.Strings.Windows.W81, Program.Lang.Strings.Aspects.LockScreen),
                                string.Format(Program.Lang.Strings.ThemeManager.Errors.Error, Program.Lang.Strings.Aspects.LockScreen),
                                Program.Lang.Strings.ThemeManager.Actions.Time,
                                sw_all,
                                !LogonUI81.Enabled || (Program.Settings.AspectsControl.Enabled && !Program.Settings.AspectsControl.LogonUI),
                                string.Format(Program.Lang.Strings.ThemeManager.Skip.Main, Program.Lang.Strings.Aspects.LockScreen));
                        }

                        // Apply Windows 8 execlusive features (Colors, lock screen and visual styles)
                        if (OS.W8)
                        {
                            Execute(() => Windows8.Apply(this, tv), treeView,
                                string.Format(Program.Lang.Strings.ThemeManager.Actions.Theme, Program.Lang.Strings.Windows.W8),
                                string.Format(Program.Lang.Strings.ThemeManager.Errors.Error, string.Format(Program.Lang.Strings.Aspects.WinTheme, Program.Lang.Strings.Windows.W8)),
                                Program.Lang.Strings.ThemeManager.Actions.Time,
                                sw_all,
                                !Windows8.Enabled || (Program.Settings.AspectsControl.Enabled && !Program.Settings.AspectsControl.WinColors),
                                string.Format(Program.Lang.Strings.ThemeManager.Skip.Main, string.Format(Program.Lang.Strings.Aspects.WinTheme, Program.Lang.Strings.Windows.W8)));

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
                                string.Format(Program.Lang.Strings.ThemeManager.Actions.Theme, Program.Lang.Strings.Windows.W7),
                                string.Format(Program.Lang.Strings.ThemeManager.Errors.Error, string.Format(Program.Lang.Strings.Aspects.WinTheme, Program.Lang.Strings.Windows.W7)),
                                Program.Lang.Strings.ThemeManager.Actions.Time,
                                sw_all,
                                !Windows7.Enabled || (Program.Settings.AspectsControl.Enabled && !Program.Settings.AspectsControl.WinColors),
                                string.Format(Program.Lang.Strings.ThemeManager.Skip.Main, string.Format(Program.Lang.Strings.Aspects.WinTheme, Program.Lang.Strings.Windows.W7)));

                            Execute(() => LogonUI7.Apply(treeView), treeView,
                                string.Format(Program.Lang.Strings.ThemeManager.Actions.Applying_Feature_ForOS, Program.Lang.Strings.Windows.W7, Program.Lang.Strings.Aspects.LogonUI),
                                string.Format(Program.Lang.Strings.ThemeManager.Errors.Error, Program.Lang.Strings.Aspects.LogonUI),
                                Program.Lang.Strings.ThemeManager.Actions.Time,
                                sw_all,
                                !LogonUI7.Enabled || (Program.Settings.AspectsControl.Enabled && !Program.Settings.AspectsControl.LogonUI),
                                string.Format(Program.Lang.Strings.ThemeManager.Skip.Main, Program.Lang.Strings.Aspects.LogonUI));
                        }

                        // Apply Windows Vista execlusive features (Colors, themes, and visual styles)
                        if (OS.WVista)
                        {
                            Execute(() =>
                            {
                                WindowsVista.Apply(tv);
                                Program.RefreshDWM(this);
                            }, treeView,
                            string.Format(Program.Lang.Strings.ThemeManager.Actions.Theme, Program.Lang.Strings.Windows.WVista),
                                string.Format(Program.Lang.Strings.ThemeManager.Errors.Error, string.Format(Program.Lang.Strings.Aspects.WinTheme, Program.Lang.Strings.Windows.WVista)),
                            Program.Lang.Strings.ThemeManager.Actions.Time,
                            sw_all,
                            !WindowsVista.Enabled || (Program.Settings.AspectsControl.Enabled && !Program.Settings.AspectsControl.WinColors),
                            string.Format(Program.Lang.Strings.ThemeManager.Skip.Main, string.Format(Program.Lang.Strings.Aspects.WinTheme, Program.Lang.Strings.Windows.WVista)));
                        }

                        // Apply Windows WXP execlusive features (Themes, LogonUI screen and visual styles)
                        if (OS.WXP)
                        {
                            Execute(() => WindowsXP.Apply(tv), treeView,
                                string.Format(Program.Lang.Strings.ThemeManager.Actions.Theme, Program.Lang.Strings.Windows.WXP),
                                string.Format(Program.Lang.Strings.ThemeManager.Errors.Error, string.Format(Program.Lang.Strings.Aspects.WinTheme, Program.Lang.Strings.Windows.WXP)),
                                Program.Lang.Strings.ThemeManager.Actions.Time,
                                sw_all,
                                !WindowsXP.Enabled || (Program.Settings.AspectsControl.Enabled && !Program.Settings.AspectsControl.WinColors),
                                string.Format(Program.Lang.Strings.ThemeManager.Skip.Main, string.Format(Program.Lang.Strings.Aspects.WinTheme, Program.Lang.Strings.Windows.WXP)));

                            Execute(() => LogonUIXP.Apply(tv), treeView,
                                string.Format(Program.Lang.Strings.ThemeManager.Actions.Applying_Feature_ForOS, Program.Lang.Strings.Windows.WXP, Program.Lang.Strings.Aspects.LogonUI),
                                string.Format(Program.Lang.Strings.ThemeManager.Errors.Error, Program.Lang.Strings.Aspects.LogonUI),
                                Program.Lang.Strings.ThemeManager.Actions.Time,
                                sw_all,
                                !LogonUIXP.Enabled || (Program.Settings.AspectsControl.Enabled && !Program.Settings.AspectsControl.LogonUI),
                                string.Format(Program.Lang.Strings.ThemeManager.Skip.Main, Program.Lang.Strings.Aspects.LogonUI));
                        }

                        // Accessibility
                        Execute(() => Accessibility.Apply(tv), treeView,
                            string.Format(Program.Lang.Strings.ThemeManager.Actions.Applying_Feature, Program.Lang.Strings.Aspects.Accessibility),
                            string.Format(Program.Lang.Strings.ThemeManager.Errors.Error, Program.Lang.Strings.Aspects.Accessibility),
                            Program.Lang.Strings.ThemeManager.Actions.Time,
                            sw_all,
                            !Accessibility.Enabled || (Program.Settings.AspectsControl.Enabled && !Program.Settings.AspectsControl.Accessibility),
                            string.Format(Program.Lang.Strings.ThemeManager.Skip.Main, Program.Lang.Strings.Aspects.Accessibility));

                        // Win32UI
                        Execute(() => Win32.Apply(tv), treeView,
                            string.Format(Program.Lang.Strings.ThemeManager.Actions.Applying_Feature, Program.Lang.Strings.Aspects.ClassicColors),
                            string.Format(Program.Lang.Strings.ThemeManager.Errors.Error, Program.Lang.Strings.Aspects.ClassicColors),
                            Program.Lang.Strings.ThemeManager.Actions.Time,
                            sw_all,
                            !Win32.Enabled || (Program.Settings.AspectsControl.Enabled && !Program.Settings.AspectsControl.ClassicColors),
                            string.Format(Program.Lang.Strings.ThemeManager.Skip.Main, Program.Lang.Strings.Aspects.ClassicColors));

                        // WindowsEffects
                        Execute(() => WindowsEffects.Apply(tv, silent), treeView,
                            string.Format(Program.Lang.Strings.ThemeManager.Actions.Applying_Feature, Program.Lang.Strings.Aspects.WinEffects),
                            string.Format(Program.Lang.Strings.ThemeManager.Errors.Error, Program.Lang.Strings.Aspects.WinEffects),
                            Program.Lang.Strings.ThemeManager.Actions.Time,
                            sw_all,
                            !WindowsEffects.Enabled || (Program.Settings.AspectsControl.Enabled && !Program.Settings.AspectsControl.Effects),
                            string.Format(Program.Lang.Strings.ThemeManager.Skip.Main, Program.Lang.Strings.Aspects.WinEffects));

                        // Metrics\Fonts
                        Execute(() => MetricsFonts.Apply(tv), treeView,
                            string.Format(Program.Lang.Strings.ThemeManager.Actions.Applying_Feature, Program.Lang.Strings.Aspects.MetricsFonts),
                            string.Format(Program.Lang.Strings.ThemeManager.Errors.Error, Program.Lang.Strings.Aspects.MetricsFonts),
                            Program.Lang.Strings.ThemeManager.Actions.Time_MultipleAspects,
                            sw_all,
                            !MetricsFonts.Enabled || (Program.Settings.AspectsControl.Enabled && !Program.Settings.AspectsControl.MetricsFonts),
                            string.Format(Program.Lang.Strings.ThemeManager.Skip.Main, Program.Lang.Strings.Aspects.MetricsFonts));

                        // AltTab
                        Execute(() => AltTab.Apply(tv), treeView,
                            string.Format(Program.Lang.Strings.ThemeManager.Actions.Applying_Feature, Program.Lang.Strings.Aspects.AltTab),
                            string.Format(Program.Lang.Strings.ThemeManager.Errors.Error, Program.Lang.Strings.Aspects.AltTab),
                            Program.Lang.Strings.ThemeManager.Actions.Time,
                            sw_all,
                            !AltTab.Enabled || (Program.Settings.AspectsControl.Enabled && !Program.Settings.AspectsControl.AltTab),
                            string.Format(Program.Lang.Strings.ThemeManager.Skip.Main, Program.Lang.Strings.Aspects.AltTab));

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
                        string.Format(Program.Lang.Strings.ThemeManager.Actions.Applying_Feature, Program.Lang.Strings.Aspects.WallpaperTone),
                        string.Format(Program.Lang.Strings.ThemeManager.Errors.Error, Program.Lang.Strings.Aspects.WallpaperTone),
                        Program.Lang.Strings.ThemeManager.Actions.Time,
                        sw_all,
                        !Wallpaper.Enabled || (Program.Settings.AspectsControl.Enabled && !Program.Settings.AspectsControl.Wallpaper),
                        string.Format(Program.Lang.Strings.ThemeManager.Skip.Main, Program.Lang.Strings.Aspects.WallpaperTone));

                        #region Consoles

                        Execute(() => Apply_CommandPrompt(tv), treeView,
                            string.Format(Program.Lang.Strings.ThemeManager.Actions.Applying_Feature, Program.Lang.Strings.Aspects.CommandPrompt),
                            string.Format(Program.Lang.Strings.ThemeManager.Errors.Error, Program.Lang.Strings.Aspects.CommandPrompt),
                            Program.Lang.Strings.ThemeManager.Actions.Time,
                            sw_all,
                            !CommandPrompt.Enabled || (Program.Settings.AspectsControl.Enabled && !Program.Settings.AspectsControl.Consoles),
                            string.Format(Program.Lang.Strings.ThemeManager.Skip.Main, Program.Lang.Strings.Aspects.CommandPrompt));

                        Execute(() => Apply_PowerShell86(tv), treeView,
                            string.Format(Program.Lang.Strings.ThemeManager.Actions.Applying_Feature, Program.Lang.Strings.Aspects.PowerShellx86),
                            string.Format(Program.Lang.Strings.ThemeManager.Errors.Error, Program.Lang.Strings.Aspects.PowerShellx86),
                            Program.Lang.Strings.ThemeManager.Actions.Time,
                            sw_all,
                            !PowerShellx86.Enabled || (Program.Settings.AspectsControl.Enabled && !Program.Settings.AspectsControl.Consoles),
                            string.Format(Program.Lang.Strings.ThemeManager.Skip.Main, Program.Lang.Strings.Aspects.PowerShellx86));

                        Execute(() => Apply_PowerShell64(tv), treeView,
                            string.Format(Program.Lang.Strings.ThemeManager.Actions.Applying_Feature, Program.Lang.Strings.Aspects.PowerShellx64),
                            string.Format(Program.Lang.Strings.ThemeManager.Errors.Error, Program.Lang.Strings.Aspects.PowerShellx64),
                            Program.Lang.Strings.ThemeManager.Actions.Time,
                            sw_all,
                            !PowerShellx64.Enabled || (Program.Settings.AspectsControl.Enabled && !Program.Settings.AspectsControl.Consoles),
                            string.Format(Program.Lang.Strings.ThemeManager.Skip.Main, Program.Lang.Strings.Aspects.PowerShellx64));
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
                                    ThemeLog.AddNode(treeView, $"{DateTime.Now.ToLongTimeString()}: {Program.Lang.Strings.ThemeManager.Skip.Terminals}", "skip");
                                }

                                else if (Terminal.Enabled & TerminalPreview.Enabled)
                                {
                                    ThemeLog.AddNode(treeView, $"{DateTime.Now.ToLongTimeString()}: {Program.Lang.Strings.ThemeManager.Check.Terminals}", "info");
                                }

                                else if (Terminal.Enabled)
                                {
                                    ThemeLog.AddNode(treeView, $"{DateTime.Now.ToLongTimeString()}: {string.Format(Program.Lang.Strings.ThemeManager.Skip.Main, Program.Lang.Strings.Aspects.TerminalStable)}", "skip");
                                    ThemeLog.AddNode(treeView, $"{DateTime.Now.ToLongTimeString()}: {string.Format(Program.Lang.Strings.ThemeManager.Check.Terminal, Program.Lang.Strings.Aspects.TerminalStable)}", "info");
                                }

                                else if (TerminalPreview.Enabled)
                                {
                                    ThemeLog.AddNode(treeView, $"{DateTime.Now.ToLongTimeString()}: {string.Format(Program.Lang.Strings.ThemeManager.Skip.Main, Program.Lang.Strings.Aspects.TerminalPreview)}", "skip");
                                    ThemeLog.AddNode(treeView, $"{DateTime.Now.ToLongTimeString()}: {string.Format(Program.Lang.Strings.ThemeManager.Check.Terminal, Program.Lang.Strings.Aspects.TerminalPreview)}", "info");
                                }

                                else
                                {
                                    ThemeLog.AddNode(treeView, $"{DateTime.Now.ToLongTimeString()}: {Program.Lang.Strings.ThemeManager.Skip.Terminals}", "skip");
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
                                if (System.IO.File.Exists(Program.Settings.WindowsTerminals.Terminal_Stable_Path))
                                {
                                    TerDir = Program.Settings.WindowsTerminals.Terminal_Stable_Path;
                                }
                                else
                                {
                                    TerDir = SysPaths.TerminalJSON;
                                }

                                if (System.IO.File.Exists(Program.Settings.WindowsTerminals.Terminal_Preview_Path))
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
                                if (System.IO.File.Exists(TerDir))
                                {
                                    try
                                    {
                                        ThemeLog.AddNode(treeView, $"{DateTime.Now.ToLongTimeString()}: {string.Format(Program.Lang.Strings.ThemeManager.Actions.Applying_Feature, Program.Lang.Strings.Aspects.TerminalStable)}", "info");
                                        Terminal.Save(TerDir, WinTerminal.Mode.JSONFile);
                                        if (ReportProgress)
                                            ThemeLog.AddNode(treeView, string.Format(Program.Lang.Strings.ThemeManager.Actions.Time, sw.ElapsedMilliseconds / 1000d), "time");
                                    }
                                    catch (Exception ex)
                                    {
                                        sw.Stop();
                                        sw_all.Stop();
                                        _ErrorHappened = true;
                                        if (ReportProgress)
                                        {
                                            ThemeLog.AddNode(treeView, $"{DateTime.Now.ToLongTimeString()}: {string.Format(Program.Lang.Strings.ThemeManager.Errors.Error, Program.Lang.Strings.Aspects.TerminalStable)}", "error");
                                            AddException(string.Format(Program.Lang.Strings.ThemeManager.Errors.Error, Program.Lang.Strings.Aspects.TerminalStable), ex);
                                        }
                                        else
                                        {
                                            Forms.BugReport.ThrowError(ex);
                                        }

                                        sw.Start();
                                        sw_all.Start();
                                    }
                                }
                                else
                                {
                                    ThemeLog.AddNode(treeView, $"{DateTime.Now.ToLongTimeString()}: {string.Format(Program.Lang.Strings.ThemeManager.Skip.Terminal_JSONNotFound, Program.Lang.Strings.Aspects.TerminalStable)}", "skip");
                                }
                            }

                            if (TerminalPreview.Enabled && !Program.Settings.AspectsControl.Enabled && !(Program.Settings.AspectsControl.Enabled && !!Program.Settings.AspectsControl.WinTerminals))
                            {
                                if (System.IO.File.Exists(TerPreDir))
                                {

                                    try
                                    {
                                        ThemeLog.AddNode(treeView, $"{DateTime.Now.ToLongTimeString()}: {string.Format(Program.Lang.Strings.ThemeManager.Actions.Applying_Feature, Program.Lang.Strings.Aspects.TerminalPreview)}", "info");
                                        TerminalPreview.Save(TerPreDir, WinTerminal.Mode.JSONFile, WinTerminal.Version.Preview);
                                        if (ReportProgress)
                                            ThemeLog.AddNode(treeView, string.Format(Program.Lang.Strings.ThemeManager.Actions.Time, sw.ElapsedMilliseconds / 1000d), "time");
                                    }
                                    catch (Exception ex)
                                    {
                                        sw.Stop();
                                        sw_all.Stop();
                                        _ErrorHappened = true;
                                        if (ReportProgress)
                                        {
                                            ThemeLog.AddNode(treeView, $"{DateTime.Now.ToLongTimeString()}: {string.Format(Program.Lang.Strings.ThemeManager.Errors.Error, Program.Lang.Strings.Aspects.TerminalPreview)}", "error");
                                            AddException(string.Format(Program.Lang.Strings.ThemeManager.Errors.Error, Program.Lang.Strings.Aspects.TerminalPreview), ex);
                                        }
                                        else
                                        {
                                            Forms.BugReport.ThrowError(ex);
                                        }

                                        sw.Start();
                                        sw_all.Start();
                                    }
                                }
                                else
                                {
                                    ThemeLog.AddNode(treeView, $"{DateTime.Now.ToLongTimeString()}: {string.Format(Program.Lang.Strings.ThemeManager.Skip.Terminal_JSONNotFound, Program.Lang.Strings.Aspects.TerminalPreview)}", "skip");
                                }
                            }
                        }

                        else
                        {
                            ThemeLog.AddNode(treeView, $"{DateTime.Now.ToLongTimeString()}: {Program.Lang.Strings.ThemeManager.Skip.Terminals_NotSupported}", "skip");
                        }
                        sw.Stop();
                        #endregion

                        // ScreenSaver
                        Execute(() => ScreenSaver.Apply(tv), treeView,
                            string.Format(Program.Lang.Strings.ThemeManager.Actions.Applying_Feature, Program.Lang.Strings.Aspects.ScreenSaver),
                            string.Format(Program.Lang.Strings.ThemeManager.Errors.Error, Program.Lang.Strings.Aspects.ScreenSaver),
                            Program.Lang.Strings.ThemeManager.Actions.Time,
                            sw_all,
                            !ScreenSaver.Enabled || (Program.Settings.AspectsControl.Enabled && !Program.Settings.AspectsControl.ScreenSaver),
                            string.Format(Program.Lang.Strings.ThemeManager.Skip.Main, Program.Lang.Strings.Aspects.ScreenSaver));

                        // Sounds
                        Execute(() => Sounds.Apply(tv), treeView,
                            string.Format(Program.Lang.Strings.ThemeManager.Actions.Applying_Feature, Program.Lang.Strings.Aspects.Sounds),
                            string.Format(Program.Lang.Strings.ThemeManager.Errors.Error, Program.Lang.Strings.Aspects.Sounds),
                            Program.Lang.Strings.ThemeManager.Actions.Time,
                            sw_all,
                            !Sounds.Enabled || (Program.Settings.AspectsControl.Enabled && !Program.Settings.AspectsControl.Sounds),
                            string.Format(Program.Lang.Strings.ThemeManager.Skip.Main, Program.Lang.Strings.Aspects.Sounds));

                        // Cursors
                        Execute(() => Cursors.Apply(tv), treeView,
                            string.Empty,
                            string.Empty,
                            Program.Lang.Strings.ThemeManager.Actions.Time,
                            sw_all,
                            !Cursors.Enabled || (Program.Settings.AspectsControl.Enabled && !Program.Settings.AspectsControl.Cursors),
                            string.Format(Program.Lang.Strings.ThemeManager.Skip.Main, Program.Lang.Strings.Aspects.Cursors));

                        // Icons
                        Execute(() => Icons.Apply(tv), treeView,
                            string.Format(Program.Lang.Strings.ThemeManager.Actions.Applying_Feature, Program.Lang.Strings.Aspects.Icons),
                            string.Format(Program.Lang.Strings.ThemeManager.Errors.Error, Program.Lang.Strings.Aspects.Icons),
                            Program.Lang.Strings.ThemeManager.Actions.Time,
                            sw_all,
                            !Icons.Enabled || (Program.Settings.AspectsControl.Enabled && !Program.Settings.AspectsControl.Icons),
                            string.Format(Program.Lang.Strings.ThemeManager.Skip.Main, Program.Lang.Strings.Aspects.Icons));

                        // Update LogonUI wallpaper in HKEY_USERS\.DEFAULT
                        if (Program.Settings.ThemeApplyingBehavior.Desktop_HKU_DEFAULT == Settings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite)
                        {
                            Execute(() =>
                            {
                                EditReg(tv, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop", "Wallpaper", GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop", "Wallpaper", string.Empty), RegistryValueKind.String);
                                EditReg(tv, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop", "WallpaperStyle", GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop", "WallpaperStyle", "2"), RegistryValueKind.String);
                                EditReg(tv, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop", "TileWallpaper", GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop", "TileWallpaper", "0"), RegistryValueKind.String);
                                EditReg(tv, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop", "Pattern", GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop", "Pattern", string.Empty), RegistryValueKind.String);
                            }, treeView,
                            string.Format(Program.Lang.Strings.ThemeManager.Actions.Applying_Feature_AllUsers, Program.Lang.Strings.Aspects.Wallpaper),
                            string.Format(Program.Lang.Strings.ThemeManager.Errors.Error_AllUsers, Program.Lang.Strings.Aspects.Wallpaper),
                            Program.Lang.Strings.ThemeManager.Actions.Time);
                        }

                        else if (Program.Settings.ThemeApplyingBehavior.Desktop_HKU_DEFAULT == Settings.Structures.ThemeApplyingBehavior.OverwriteOptions.RestoreDefaults)
                        {
                            Execute(() =>
                            {
                                EditReg(tv, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop", "Wallpaper", string.Empty, RegistryValueKind.String);
                                EditReg(tv, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop", "WallpaperStyle", "2", RegistryValueKind.String);
                                EditReg(tv, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop", "TileWallpaper", "0", RegistryValueKind.String);
                                EditReg(tv, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop", "Pattern", string.Empty, RegistryValueKind.String);
                            }, treeView,
                            string.Format(Program.Lang.Strings.ThemeManager.Actions.Applying_Feature_AllUsers, Program.Lang.Strings.Aspects.Wallpaper),
                            string.Format(Program.Lang.Strings.ThemeManager.Errors.Error_AllUsers, Program.Lang.Strings.Aspects.Wallpaper),
                            Program.Lang.Strings.ThemeManager.Actions.Time);
                        }

                        // Update User Preference Mask for HKEY_USERS\.DEFAULT
                        // Always make it the last operation
                        if (Program.Settings.ThemeApplyingBehavior.UPM_HKU_DEFAULT) Win32.Broadcast_UPM_ToDefUsers(tv);

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
                                ThemeLog.AddNode(treeView, $"{DateTime.Now.ToLongTimeString()}: {string.Format(Program.Lang.Strings.ThemeManager.Actions.Applied, sw_all.ElapsedMilliseconds / 1000d)}", "success");
                            }
                            else
                            {
                                ThemeLog.AddNode(treeView, $"{DateTime.Now.ToLongTimeString()}: {string.Format(Program.Lang.Strings.ThemeManager.Actions.AppliedWithErrors, sw_all.ElapsedMilliseconds / 1000d)}", "warning");
                            }
                        }

                        sw_all.Reset();
                        sw_all.Stop();
                        wic.Undo();
                    }
                    break;

                case Source.File:
                    if (!System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(file))) System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(file));

                    if (Info.ExportResThemePack)
                    {
                        // Always clone theme manager as PackThemeResources will modify the current theme manager (Some paths will be converted into environment variables)
                        using (Manager TMx = Clone() as Manager)
                        {
                            PackThemeResources(TMx, file, $"{new FileInfo(file).DirectoryName}\\{Path.GetFileNameWithoutExtension(file)}.wptp");
                        }
                    }

                    else { System.IO.File.WriteAllText(file, ToString()); }

                    break;
            }
        }

        /// <summary>
        /// WinPaletter theme File contents
        /// </summary>
        /// <param name="IgnoreCompression"></param>
        /// <returns></returns>
        public string ToString(bool IgnoreCompression = false)
        {
            JObject JSON_Overall = [];
            JSON_Overall.RemoveAll();

            Info.AppVersion = Program.Version;

            foreach (FieldInfo field in GetType().GetFields(bindingFlags))
            {
                Type type = field.FieldType;

                if (type.IsStructure())
                {
                    JSON_Overall.Add(field.Name, DeserializeProps(type, field.GetValue(this)));
                }
                else
                {
                    JSON_Overall.Add(field.Name, JToken.FromObject(field.GetValue(this)));
                }
            }

            if (Program.Settings.FileTypeManagement.CompressThemeFile && !IgnoreCompression)
            {
                return JSON_Overall.ToString().Compress();
            }
            else
            {
                return JSON_Overall.ToString();
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
            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"A query to make a theme resources pack for `{ThemeFile}` is made. It will be saved as `{Pack}`.");

            // Create a cache directory for the theme resources pack
            string cache = $"%WinPaletterAppData%\\ThemeResPack_Cache\\{string.Concat(TM.Info.ThemeName.Replace(" ", string.Empty).Split(Path.GetInvalidFileNameChars()))}\\";

            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Cache directory for the theme resources pack is `{cache}`.");

            Dictionary<string, string> filesList = [];
            filesList.Clear();
            string x;
            string ZipEntry;

            // Delete the previous pack if exists
            if (System.IO.File.Exists(Pack)) System.IO.File.Delete(Pack);
            if (!System.IO.Directory.Exists($"{SysPaths.appData}\\Temp")) System.IO.Directory.CreateDirectory($"{SysPaths.appData}\\Temp");
            foreach (string file_to_delete in System.IO.Directory.GetFiles($"{SysPaths.appData}\\Temp")) System.IO.File.Delete(file_to_delete);

            // Create the pack
            using (ZipArchive archive = ZipFile.Open(Pack, ZipArchiveMode.Create))
            {
                // Add Windows 8.1 logonUI files
                if (TM.LogonUI81.Enabled && TM.LogonUI81.Mode == Theme.Structures.LogonUI81.Sources.CustomImage)
                {
                    x = TM.LogonUI81.ImagePath;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\Web", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = $"{cache}LogonUI81{Path.GetExtension(x)}";
                        if (System.IO.File.Exists(x))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Windows 8.1 LogonUI image inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                            TM.LogonUI81.ImagePath = ZipEntry;
                        }

                        filesList.Add(ZipEntry, x);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Windows 8.1 LogonUI image `{x}` to the theme resources pack as `{ZipEntry}`.");
                    }
                }

                // Add Windows 7 logonUI files
                if (TM.LogonUI7.Enabled && TM.LogonUI7.Mode == Theme.Structures.LogonUI7.Sources.CustomImage)
                {
                    x = TM.LogonUI7.ImagePath;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\Web", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = $"{cache}LogonUI7{Path.GetExtension(x)}";
                        if (System.IO.File.Exists(x))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Windows 7 LogonUI image inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                            TM.LogonUI7.ImagePath = ZipEntry;
                        }

                        filesList.Add(ZipEntry, x);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Windows 7 LogonUI image `{x}` to the theme resources pack as `{ZipEntry}`.");
                    }
                }

                // Add Windows Terminal files
                if (TM.Terminal.Enabled)
                {
                    x = TM.Terminal.Profiles.Defaults.BackgroundImage;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\Web", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = $"{cache}winterminal_defprofile_backimg{Path.GetExtension(x)}";
                        if (System.IO.File.Exists(x))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Windows Terminal default profile background image inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                            TM.Terminal.Profiles.Defaults.BackgroundImage = ZipEntry;
                        }

                        filesList.Add(ZipEntry, x);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Windows Terminal default profile background image `{x}` to the theme resources pack as `{ZipEntry}`.");
                    }

                    x = TM.Terminal.Profiles.Defaults.Icon;
                    if (!string.IsNullOrWhiteSpace(x) && !(x.Length <= 1) && !x.StartsWith($@"{SysPaths.Windows}\Web", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = $"{cache}winterminal_defprofile_icon{Path.GetExtension(x)}";
                        if (System.IO.File.Exists(x))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Windows Terminal default profile icon inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                            TM.Terminal.Profiles.Defaults.Icon = ZipEntry;
                        }

                        filesList.Add(ZipEntry, x);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Windows Terminal default profile icon `{x}` to the theme resources pack as `{ZipEntry}`.");
                    }

                    foreach (WinTerminal.Types.Profile i in TM.Terminal.Profiles.List)
                    {
                        x = i.BackgroundImage;
                        if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\Web", StringComparison.OrdinalIgnoreCase))
                        {
                            ZipEntry = $"{cache}winterminal_profile({string.Concat(i.Name.Replace(" ", string.Empty).Split(Path.GetInvalidFileNameChars()))})_backimg{Path.GetExtension(x)}";
                            if (System.IO.File.Exists(x))
                            {
                                Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Windows Terminal profile `{i.Name}` background image inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                                i.BackgroundImage = ZipEntry;
                            }

                            filesList.Add(ZipEntry, x);

                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Windows Terminal profile `{i.Name}` background image `{x}` to the theme resources pack as `{ZipEntry}`.");
                        }

                        x = i.Icon;
                        if (!string.IsNullOrWhiteSpace(x) && !(x.Length <= 1) && !x.StartsWith($@"{SysPaths.Windows}\Web", StringComparison.OrdinalIgnoreCase))
                        {
                            ZipEntry = $"{cache}winterminal_profile({string.Concat(i.Name.Replace(" ", string.Empty).Split(Path.GetInvalidFileNameChars()))})_icon{Path.GetExtension(x)}";
                            if (System.IO.File.Exists(x))
                            {
                                Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Windows Terminal profile `{i.Name}` icon inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                                i.Icon = ZipEntry;
                            }

                            filesList.Add(ZipEntry, x);

                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Windows Terminal profile `{i.Name}` icon `{x}` to the theme resources pack as `{ZipEntry}`.");
                        }
                    }
                }

                // Add Windows Terminal Preview files
                if (TM.TerminalPreview.Enabled)
                {
                    x = TM.TerminalPreview.Profiles.Defaults.BackgroundImage;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\Web", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = $"{cache}winterminal_preview_defprofile_backimg{Path.GetExtension(x)}";
                        if (System.IO.File.Exists(x))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Windows Terminal Preview default profile background image inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                            TM.TerminalPreview.Profiles.Defaults.BackgroundImage = ZipEntry;
                        }

                        filesList.Add(ZipEntry, x);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Windows Terminal Preview default profile background image `{x}` to the theme resources pack as `{ZipEntry}`.");
                    }

                    x = TM.TerminalPreview.Profiles.Defaults.Icon;
                    if (!string.IsNullOrWhiteSpace(x) && !(x.Length <= 1) && !x.StartsWith($@"{SysPaths.Windows}\Web", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = $"{cache}winterminal_preview_defprofile_icon{Path.GetExtension(x)}";
                        if (System.IO.File.Exists(x))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Windows Terminal Preview default profile icon inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                            TM.TerminalPreview.Profiles.Defaults.Icon = ZipEntry;
                        }

                        filesList.Add(ZipEntry, x);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Windows Terminal Preview default profile icon `{x}` to the theme resources pack as `{ZipEntry}`.");
                    }

                    foreach (WinTerminal.Types.Profile i in TM.TerminalPreview.Profiles.List)
                    {
                        x = i.BackgroundImage;
                        if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\Web", StringComparison.OrdinalIgnoreCase))
                        {
                            ZipEntry = $"{cache}winterminal_preview_profile({string.Concat(i.Name.Replace(" ", string.Empty).Split(Path.GetInvalidFileNameChars()))})_backimg{Path.GetExtension(x)}";
                            if (System.IO.File.Exists(x))
                            {
                                Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Windows Terminal Preview profile `{i.Name}` background image inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                                i.BackgroundImage = ZipEntry;
                            }

                            filesList.Add(ZipEntry, x);

                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Windows Terminal Preview profile `{i.Name}` background image `{x}` to the theme resources pack as `{ZipEntry}`.");
                        }

                        x = i.Icon;
                        if (!string.IsNullOrWhiteSpace(x) && !(x.Length <= 1) && !x.StartsWith($@"{SysPaths.Windows}\Web", StringComparison.OrdinalIgnoreCase))
                        {
                            ZipEntry = $"{cache}winterminal_preview_profile({string.Concat(i.Name.Replace(" ", string.Empty).Split(Path.GetInvalidFileNameChars()))})_icon{Path.GetExtension(x)}";
                            if (System.IO.File.Exists(x))
                            {
                                Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Windows Terminal Preview profile `{i.Name}` icon inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                                i.Icon = ZipEntry;
                            }

                            filesList.Add(ZipEntry, x);

                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Windows Terminal Preview profile `{i.Name}` icon `{x}` to the theme resources pack as `{ZipEntry}`.");
                        }
                    }
                }

                // Add wallpaper image used for Wallpaper Tone feature for Windows 12, 11, 10, 8.1, 7, Vista, and WXP
                if (TM.WallpaperTone_W12.Enabled)
                {
                    x = TM.WallpaperTone_W12.Image;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\Web", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = $"{cache}wt_w12{Path.GetExtension(x)}";
                        if (System.IO.File.Exists(x))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Windows 12 Wallpaper Tone image inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                            TM.WallpaperTone_W12.Image = ZipEntry;
                        }

                        filesList.Add(ZipEntry, x);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Windows 12 Wallpaper Tone image `{x}` to the theme resources pack as `{ZipEntry}`.");
                    }
                }

                if (TM.WallpaperTone_W11.Enabled)
                {
                    x = TM.WallpaperTone_W11.Image;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\Web", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = $"{cache}wt_w11{Path.GetExtension(x)}";
                        if (System.IO.File.Exists(x))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Windows 11 Wallpaper Tone image inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                            TM.WallpaperTone_W11.Image = ZipEntry;
                        }

                        filesList.Add(ZipEntry, x);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Windows 11 Wallpaper Tone image `{x}` to the theme resources pack as `{ZipEntry}`.");
                    }
                }

                if (TM.WallpaperTone_W10.Enabled)
                {
                    x = TM.WallpaperTone_W10.Image;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\Web", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = $"{cache}wt_w10{Path.GetExtension(x)}";
                        if (System.IO.File.Exists(x))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Windows 10 Wallpaper Tone image inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                            TM.WallpaperTone_W10.Image = ZipEntry;
                        }

                        filesList.Add(ZipEntry, x);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Windows 10 Wallpaper Tone image `{x}` to the theme resources pack as `{ZipEntry}`.");
                    }
                }

                if (TM.WallpaperTone_W81.Enabled)
                {
                    x = TM.WallpaperTone_W81.Image;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\Web", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = $"{cache}wt_w81{Path.GetExtension(x)}";
                        if (System.IO.File.Exists(x))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Windows 8.1 Wallpaper Tone image inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                            TM.WallpaperTone_W81.Image = ZipEntry;
                        }

                        filesList.Add(ZipEntry, x);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Windows 8.1 Wallpaper Tone image `{x}` to the theme resources pack as `{ZipEntry}`.");
                    }
                }

                if (TM.WallpaperTone_W7.Enabled)
                {
                    x = TM.WallpaperTone_W7.Image;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\Web", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = $"{cache}wt_w7{Path.GetExtension(x)}";
                        if (System.IO.File.Exists(x))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Windows 7 Wallpaper Tone image inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                            TM.WallpaperTone_W7.Image = ZipEntry;
                        }

                        filesList.Add(ZipEntry, x);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Windows 7 Wallpaper Tone image `{x}` to the theme resources pack as `{ZipEntry}`.");
                    }
                }

                if (TM.WallpaperTone_WVista.Enabled)
                {
                    x = TM.WallpaperTone_WVista.Image;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\Web", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = $"{cache}wt_wvista{Path.GetExtension(x)}";
                        if (System.IO.File.Exists(x))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Windows Vista Wallpaper Tone image inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                            TM.WallpaperTone_WVista.Image = ZipEntry;
                        }

                        filesList.Add(ZipEntry, x);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Windows Vista Wallpaper Tone image `{x}` to the theme resources pack as `{ZipEntry}`.");
                    }
                }

                if (TM.WallpaperTone_WXP.Enabled)
                {
                    x = TM.WallpaperTone_WXP.Image;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\Web", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = $"{cache}wt_wxp{Path.GetExtension(x)}";
                        if (System.IO.File.Exists(x))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Windows XP Wallpaper Tone image inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                            TM.WallpaperTone_WXP.Image = ZipEntry;
                        }

                        filesList.Add(ZipEntry, x);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Windows XP Wallpaper Tone image `{x}` to the theme resources pack as `{ZipEntry}`.");
                    }
                }

                // Add ScreenSaver file
                if (TM.ScreenSaver.Enabled)
                {
                    x = TM.ScreenSaver.File;
                    if (!string.IsNullOrWhiteSpace(x))
                    {
                        ZipEntry = $"{cache}scrsvr{Path.GetExtension(x)}";
                        if (System.IO.File.Exists(x))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of ScreenSaver file inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                            TM.ScreenSaver.File = ZipEntry;
                        }

                        filesList.Add(ZipEntry, x);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding ScreenSaver file `{x}` to the theme resources pack as `{ZipEntry}`.");
                    }
                }

                // Add Cursor files (that are not rendered by WinPaletter)
                if (TM.Cursors.Enabled)
                {
                    if (TM.Cursors.Cursor_Arrow.UseFromFile && System.IO.File.Exists(TM.Cursors.Cursor_Arrow.File))
                    {
                        // Cursor_Arrow
                        x = TM.Cursors.Cursor_Arrow.File;
                        if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\Cursors", StringComparison.OrdinalIgnoreCase))
                        {
                            ZipEntry = $"{cache}Cursor_Arrow{Path.GetExtension(x)}";
                            if (System.IO.File.Exists(x))
                            {
                                Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Cursor_Arrow file inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                                TM.Cursors.Cursor_Arrow.File = ZipEntry;
                            }

                            filesList.Add(ZipEntry, x);

                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Cursor_Arrow file `{x}` to the theme resources pack as `{ZipEntry}`.");
                        }

                        // Cursor_AppLoading
                        x = TM.Cursors.Cursor_AppLoading.File;
                        if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\Cursors", StringComparison.OrdinalIgnoreCase))
                        {
                            ZipEntry = $"{cache}Cursor_AppLoading{Path.GetExtension(x)}";
                            if (System.IO.File.Exists(x))
                            {
                                Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Cursor_AppLoading file inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                                TM.Cursors.Cursor_AppLoading.File = ZipEntry;
                            }

                            filesList.Add(ZipEntry, x);

                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Cursor_AppLoading file `{x}` to the theme resources pack as `{ZipEntry}`.");
                        }

                        // Cursor_Busy
                        x = TM.Cursors.Cursor_Busy.File;
                        if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\Cursors", StringComparison.OrdinalIgnoreCase))
                        {
                            ZipEntry = $"{cache}Cursor_Busy{Path.GetExtension(x)}";
                            if (System.IO.File.Exists(x))
                            {
                                Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Cursor_Busy file inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                                TM.Cursors.Cursor_Busy.File = ZipEntry;
                            }

                            filesList.Add(ZipEntry, x);

                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Cursor_Busy file `{x}` to the theme resources pack as `{ZipEntry}`.");
                        }

                        // Cursor_Help
                        x = TM.Cursors.Cursor_Help.File;
                        if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\Cursors", StringComparison.OrdinalIgnoreCase))
                        {
                            ZipEntry = $"{cache}Cursor_Help{Path.GetExtension(x)}";
                            if (System.IO.File.Exists(x))
                            {
                                Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Cursor_Help file inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                                TM.Cursors.Cursor_Help.File = ZipEntry;
                            }

                            filesList.Add(ZipEntry, x);

                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Cursor_Help file `{x}` to the theme resources pack as `{ZipEntry}`.");
                        }

                        // Cursor_Move
                        x = TM.Cursors.Cursor_Move.File;
                        if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\Cursors", StringComparison.OrdinalIgnoreCase))
                        {
                            ZipEntry = $"{cache}Cursor_Move{Path.GetExtension(x)}";
                            if (System.IO.File.Exists(x))
                            {
                                Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Cursor_Move file inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                                TM.Cursors.Cursor_Move.File = ZipEntry;
                            }

                            filesList.Add(ZipEntry, x);

                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Cursor_Move file `{x}` to the theme resources pack as `{ZipEntry}`.");
                        }

                        // Cursor_NS
                        x = TM.Cursors.Cursor_NS.File;
                        if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\Cursors", StringComparison.OrdinalIgnoreCase))
                        {
                            ZipEntry = $"{cache}Cursor_NS{Path.GetExtension(x)}";
                            if (System.IO.File.Exists(x))
                            {
                                Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Cursor_NS file inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                                TM.Cursors.Cursor_NS.File = ZipEntry;
                            }

                            filesList.Add(ZipEntry, x);

                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Cursor_NS file `{x}` to the theme resources pack as `{ZipEntry}`.");
                        }

                        // Cursor_EW
                        x = TM.Cursors.Cursor_EW.File;
                        if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\Cursors", StringComparison.OrdinalIgnoreCase))
                        {
                            ZipEntry = $"{cache}Cursor_EW{Path.GetExtension(x)}";
                            if (System.IO.File.Exists(x))
                            {
                                Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Cursor_EW file inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                                TM.Cursors.Cursor_EW.File = ZipEntry;
                            }

                            filesList.Add(ZipEntry, x);

                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Cursor_EW file `{x}` to the theme resources pack as `{ZipEntry}`.");
                        }

                        // Cursor_NESW
                        x = TM.Cursors.Cursor_NESW.File;
                        if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\Cursors", StringComparison.OrdinalIgnoreCase))
                        {
                            ZipEntry = $"{cache}Cursor_NESW{Path.GetExtension(x)}";
                            if (System.IO.File.Exists(x))
                            {
                                Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Cursor_NESW file inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                                TM.Cursors.Cursor_NESW.File = ZipEntry;
                            }

                            filesList.Add(ZipEntry, x);

                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Cursor_NESW file `{x}` to the theme resources pack as `{ZipEntry}`.");
                        }

                        // Cursor_NWSE
                        x = TM.Cursors.Cursor_NWSE.File;
                        if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\Cursors", StringComparison.OrdinalIgnoreCase))
                        {
                            ZipEntry = $"{cache}Cursor_NWSE{Path.GetExtension(x)}";
                            if (System.IO.File.Exists(x))
                            {
                                Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Cursor_NWSE file inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                                TM.Cursors.Cursor_NWSE.File = ZipEntry;
                            }

                            filesList.Add(ZipEntry, x);

                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Cursor_NWSE file `{x}` to the theme resources pack as `{ZipEntry}`.");
                        }

                        // Cursor_Up
                        x = TM.Cursors.Cursor_Up.File;
                        if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\Cursors", StringComparison.OrdinalIgnoreCase))
                        {
                            ZipEntry = $"{cache}Cursor_Up{Path.GetExtension(x)}";
                            if (System.IO.File.Exists(x))
                            {
                                Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Cursor_Up file inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                                TM.Cursors.Cursor_Up.File = ZipEntry;
                            }

                            filesList.Add(ZipEntry, x);

                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Cursor_Up file `{x}` to the theme resources pack as `{ZipEntry}`.");
                        }

                        // Cursor_Pen
                        x = TM.Cursors.Cursor_Pen.File;
                        if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\Cursors", StringComparison.OrdinalIgnoreCase))
                        {
                            ZipEntry = $"{cache}Cursor_Pen{Path.GetExtension(x)}";
                            if (System.IO.File.Exists(x))
                            {
                                Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Cursor_Pen file inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                                TM.Cursors.Cursor_Pen.File = ZipEntry;
                            }

                            filesList.Add(ZipEntry, x);

                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Cursor_Pen file `{x}` to the theme resources pack as `{ZipEntry}`.");
                        }

                        // Cursor_None
                        x = TM.Cursors.Cursor_None.File;
                        if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\Cursors", StringComparison.OrdinalIgnoreCase))
                        {
                            ZipEntry = $"{cache}Cursor_None{Path.GetExtension(x)}";
                            if (System.IO.File.Exists(x))
                            {
                                Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Cursor_None file inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                                TM.Cursors.Cursor_None.File = ZipEntry;
                            }

                            filesList.Add(ZipEntry, x);

                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Cursor_None file `{x}` to the theme resources pack as `{ZipEntry}`.");
                        }

                        // Cursor_Link
                        x = TM.Cursors.Cursor_Link.File;
                        if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\Cursors", StringComparison.OrdinalIgnoreCase))
                        {
                            ZipEntry = $"{cache}Cursor_Link{Path.GetExtension(x)}";
                            if (System.IO.File.Exists(x))
                            {
                                Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Cursor_Link file inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                                TM.Cursors.Cursor_Link.File = ZipEntry;
                            }

                            filesList.Add(ZipEntry, x);

                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Cursor_Link file `{x}` to the theme resources pack as `{ZipEntry}`.");
                        }

                        // Cursor_Pin
                        x = TM.Cursors.Cursor_Pin.File;
                        if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\Cursors", StringComparison.OrdinalIgnoreCase))
                        {
                            ZipEntry = $"{cache}Cursor_Pin{Path.GetExtension(x)}";
                            if (System.IO.File.Exists(x))
                            {
                                Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Cursor_Pin file inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                                TM.Cursors.Cursor_Pin.File = ZipEntry;
                            }

                            filesList.Add(ZipEntry, x);

                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Cursor_Pin file `{x}` to the theme resources pack as `{ZipEntry}`.");
                        }

                        // Cursor_Person
                        x = TM.Cursors.Cursor_Person.File;
                        if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\Cursors", StringComparison.OrdinalIgnoreCase))
                        {
                            ZipEntry = $"{cache}Cursor_Person{Path.GetExtension(x)}";
                            if (System.IO.File.Exists(x))
                            {
                                Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Cursor_Person file inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                                TM.Cursors.Cursor_Person.File = ZipEntry;
                            }

                            filesList.Add(ZipEntry, x);

                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Cursor_Person file `{x}` to the theme resources pack as `{ZipEntry}`.");
                        }

                        // Cursor_IBeam
                        x = TM.Cursors.Cursor_IBeam.File;
                        if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\Cursors", StringComparison.OrdinalIgnoreCase))
                        {
                            ZipEntry = $"{cache}Cursor_IBeam{Path.GetExtension(x)}";
                            if (System.IO.File.Exists(x))
                            {
                                Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Cursor_IBeam file inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                                TM.Cursors.Cursor_IBeam.File = ZipEntry;
                            }

                            filesList.Add(ZipEntry, x);

                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Cursor_IBeam file `{x}` to the theme resources pack as `{ZipEntry}`.");
                        }

                        // Cursor_Cross
                        x = TM.Cursors.Cursor_Cross.File;
                        if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\Cursors", StringComparison.OrdinalIgnoreCase))
                        {
                            ZipEntry = $"{cache}Cursor_Cross{Path.GetExtension(x)}";
                            if (System.IO.File.Exists(x))
                            {
                                Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Cursor_Cross file inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                                TM.Cursors.Cursor_Cross.File = ZipEntry;
                            }

                            filesList.Add(ZipEntry, x);

                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Cursor_Cross file `{x}` to the theme resources pack as `{ZipEntry}`.");
                        }
                    }
                }

                #region Sounds
                // Add sounds files
                if (TM.Sounds.Enabled)
                {
                    x = TM.Sounds.Snd_Win_Default;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = $"{cache}Snd_Win_Default{Path.GetExtension(x)}";
                        if (System.IO.File.Exists(x))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Windows Default sound inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                            TM.Sounds.Snd_Win_Default = ZipEntry;
                        }

                        filesList.Add(ZipEntry, x);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Windows Default sound `{x}` to the theme resources pack as `{ZipEntry}`.");
                    }

                    x = TM.Sounds.Snd_Win_AppGPFault;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = $"{cache}Snd_Win_AppGPFault{Path.GetExtension(x)}";
                        if (System.IO.File.Exists(x))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Windows App GP Fault sound inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                            TM.Sounds.Snd_Win_AppGPFault = ZipEntry;
                        }

                        filesList.Add(ZipEntry, x);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Windows App GP Fault sound `{x}` to the theme resources pack as `{ZipEntry}`.");
                    }

                    x = TM.Sounds.Snd_Win_CCSelect;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = $"{cache}Snd_Win_CCSelect{Path.GetExtension(x)}";
                        if (System.IO.File.Exists(x))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Windows CC Select sound inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                            TM.Sounds.Snd_Win_CCSelect = ZipEntry;
                        }

                        filesList.Add(ZipEntry, x);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Windows CC Select sound `{x}` to the theme resources pack as `{ZipEntry}`.");
                    }

                    x = TM.Sounds.Snd_Win_ChangeTheme;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = $"{cache}Snd_Win_ChangeTheme{Path.GetExtension(x)}";
                        if (System.IO.File.Exists(x))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Windows Change Theme sound inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                            TM.Sounds.Snd_Win_ChangeTheme = ZipEntry;
                        }

                        filesList.Add(ZipEntry, x);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Windows Change Theme sound `{x}` to the theme resources pack as `{ZipEntry}`.");
                    }

                    x = TM.Sounds.Snd_Win_Close;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = $"{cache}Snd_Win_Close{Path.GetExtension(x)}";
                        if (System.IO.File.Exists(x))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Windows Close sound inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                            TM.Sounds.Snd_Win_Close = ZipEntry;
                        }

                        filesList.Add(ZipEntry, x);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Windows Close sound `{x}` to the theme resources pack as `{ZipEntry}`.");
                    }

                    x = TM.Sounds.Snd_Win_CriticalBatteryAlarm;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = $"{cache}Snd_Win_CriticalBatteryAlarm{Path.GetExtension(x)}";
                        if (System.IO.File.Exists(x))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Windows Critical Battery Alarm sound inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                            TM.Sounds.Snd_Win_CriticalBatteryAlarm = ZipEntry;
                        }

                        filesList.Add(ZipEntry, x);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Windows Critical Battery Alarm sound `{x}` to the theme resources pack as `{ZipEntry}`.");
                    }

                    x = TM.Sounds.Snd_Win_DeviceConnect;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = $"{cache}Snd_Win_DeviceConnect{Path.GetExtension(x)}";
                        if (System.IO.File.Exists(x))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Windows Device Connect sound inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                            TM.Sounds.Snd_Win_DeviceConnect = ZipEntry;
                        }

                        filesList.Add(ZipEntry, x);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Windows Device Connect sound `{x}` to the theme resources pack as `{ZipEntry}`.");
                    }

                    x = TM.Sounds.Snd_Win_DeviceDisconnect;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = $"{cache}Snd_Win_DeviceDisconnect{Path.GetExtension(x)}";
                        if (System.IO.File.Exists(x))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Windows Device Disconnect sound inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                            TM.Sounds.Snd_Win_DeviceDisconnect = ZipEntry;
                        }

                        filesList.Add(ZipEntry, x);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Windows Device Disconnect sound `{x}` to the theme resources pack as `{ZipEntry}`.");
                    }

                    x = TM.Sounds.Snd_Win_DeviceFail;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = $"{cache}Snd_Win_DeviceFail{Path.GetExtension(x)}";
                        if (System.IO.File.Exists(x))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Windows Device Fail sound inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                            TM.Sounds.Snd_Win_DeviceFail = ZipEntry;
                        }

                        filesList.Add(ZipEntry, x);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Windows Device Fail sound `{x}` to the theme resources pack as `{ZipEntry}`.");
                    }

                    x = TM.Sounds.Snd_Win_FaxBeep;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = $"{cache}Snd_Win_FaxBeep{Path.GetExtension(x)}";
                        if (System.IO.File.Exists(x))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Windows Fax Beep sound inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                            TM.Sounds.Snd_Win_FaxBeep = ZipEntry;
                        }

                        filesList.Add(ZipEntry, x);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Windows Fax Beep sound `{x}` to the theme resources pack as `{ZipEntry}`.");
                    }

                    x = TM.Sounds.Snd_Win_LowBatteryAlarm;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = $"{cache}Snd_Win_LowBatteryAlarm{Path.GetExtension(x)}";
                        if (System.IO.File.Exists(x))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Windows Low Battery Alarm sound inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                            TM.Sounds.Snd_Win_LowBatteryAlarm = ZipEntry;
                        }

                        filesList.Add(ZipEntry, x);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Windows Low Battery Alarm sound `{x}` to the theme resources pack as `{ZipEntry}`.");
                    }

                    x = TM.Sounds.Snd_Win_MailBeep;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = $"{cache}Snd_Win_MailBeep{Path.GetExtension(x)}";
                        if (System.IO.File.Exists(x))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Windows Mail Beep sound inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                            TM.Sounds.Snd_Win_MailBeep = ZipEntry;
                        }

                        filesList.Add(ZipEntry, x);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Windows Mail Beep sound `{x}` to the theme resources pack as `{ZipEntry}`.");
                    }

                    x = TM.Sounds.Snd_Win_Maximize;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = $"{cache}Snd_Win_Maximize{Path.GetExtension(x)}";
                        if (System.IO.File.Exists(x))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Windows Maximize sound inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                            TM.Sounds.Snd_Win_Maximize = ZipEntry;
                        }

                        filesList.Add(ZipEntry, x);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Windows Maximize sound `{x}` to the theme resources pack as `{ZipEntry}`.");
                    }

                    x = TM.Sounds.Snd_Win_MenuCommand;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = $"{cache}Snd_Win_MenuCommand{Path.GetExtension(x)}";
                        if (System.IO.File.Exists(x))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Windows Menu Command sound inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                            TM.Sounds.Snd_Win_MenuCommand = ZipEntry;
                        }

                        filesList.Add(ZipEntry, x);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Windows Menu Command sound `{x}` to the theme resources pack as `{ZipEntry}`.");
                    }

                    x = TM.Sounds.Snd_Win_MenuPopup;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = $"{cache}Snd_Win_MenuPopup{Path.GetExtension(x)}";
                        if (System.IO.File.Exists(x))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Windows Menu Popup sound inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                            TM.Sounds.Snd_Win_MenuPopup = ZipEntry;
                        }

                        filesList.Add(ZipEntry, x);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Windows Menu Popup sound `{x}` to the theme resources pack as `{ZipEntry}`.");
                    }

                    x = TM.Sounds.Snd_Win_MessageNudge;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = $"{cache}Snd_Win_MessageNudge{Path.GetExtension(x)}";
                        if (System.IO.File.Exists(x))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Windows Message Nudge sound inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                            TM.Sounds.Snd_Win_MessageNudge = ZipEntry;
                        }

                        filesList.Add(ZipEntry, x);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Windows Message Nudge sound `{x}` to the theme resources pack as `{ZipEntry}`.");
                    }

                    x = TM.Sounds.Snd_Win_Minimize;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = $"{cache}Snd_Win_Minimize{Path.GetExtension(x)}";
                        if (System.IO.File.Exists(x))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Windows Minimize sound inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                            TM.Sounds.Snd_Win_Minimize = ZipEntry;
                        }

                        filesList.Add(ZipEntry, x);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Windows Minimize sound `{x}` to the theme resources pack as `{ZipEntry}`.");
                    }

                    x = TM.Sounds.Snd_Win_Notification_Default;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = $"{cache}Snd_Win_Notification_Default{Path.GetExtension(x)}";
                        if (System.IO.File.Exists(x))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Windows Notification Default sound inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                            TM.Sounds.Snd_Win_Notification_Default = ZipEntry;
                        }

                        filesList.Add(ZipEntry, x);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Windows Notification Default sound `{x}` to the theme resources pack as `{ZipEntry}`.");
                    }

                    x = TM.Sounds.Snd_Win_Notification_IM;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = $"{cache}Snd_Win_Notification_IM{Path.GetExtension(x)}";
                        if (System.IO.File.Exists(x))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Windows Notification IM sound inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                            TM.Sounds.Snd_Win_Notification_IM = ZipEntry;
                        }

                        filesList.Add(ZipEntry, x);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Windows Notification IM sound `{x}` to the theme resources pack as `{ZipEntry}`.");
                    }

                    x = TM.Sounds.Snd_Win_Notification_Looping_Alarm;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = $"{cache}Snd_Win_Notification_Looping_Alarm{Path.GetExtension(x)}";
                        if (System.IO.File.Exists(x))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Windows Notification Looping Alarm sound inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                            TM.Sounds.Snd_Win_Notification_Looping_Alarm = ZipEntry;
                        }

                        filesList.Add(ZipEntry, x);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Windows Notification Looping Alarm sound `{x}` to the theme resources pack as `{ZipEntry}`.");
                    }

                    x = TM.Sounds.Snd_Win_Notification_Looping_Alarm10;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = $"{cache}Snd_Win_Notification_Looping_Alarm10{Path.GetExtension(x)}";
                        if (System.IO.File.Exists(x))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Windows Notification Looping Alarm 10 sound inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                            TM.Sounds.Snd_Win_Notification_Looping_Alarm10 = ZipEntry;
                        }

                        filesList.Add(ZipEntry, x);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Windows Notification Looping Alarm 10 sound `{x}` to the theme resources pack as `{ZipEntry}`.");
                    }

                    x = TM.Sounds.Snd_Win_Notification_Looping_Alarm2;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = $"{cache}Snd_Win_Notification_Looping_Alarm2{Path.GetExtension(x)}";
                        if (System.IO.File.Exists(x))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Windows Notification Looping Alarm 2 sound inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                            TM.Sounds.Snd_Win_Notification_Looping_Alarm2 = ZipEntry;
                        }

                        filesList.Add(ZipEntry, x);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Windows Notification Looping Alarm 2 sound `{x}` to the theme resources pack as `{ZipEntry}`.");
                    }

                    x = TM.Sounds.Snd_Win_Notification_Looping_Alarm3;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = $"{cache}Snd_Win_Notification_Looping_Alarm3{Path.GetExtension(x)}";
                        if (System.IO.File.Exists(x))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Windows Notification Looping Alarm 3 sound inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                            TM.Sounds.Snd_Win_Notification_Looping_Alarm3 = ZipEntry;
                        }

                        filesList.Add(ZipEntry, x);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Windows Notification Looping Alarm 3 sound `{x}` to the theme resources pack as `{ZipEntry}`.");
                    }

                    x = TM.Sounds.Snd_Win_Notification_Looping_Alarm4;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = $"{cache}Snd_Win_Notification_Looping_Alarm4{Path.GetExtension(x)}";
                        if (System.IO.File.Exists(x))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Windows Notification Looping Alarm 4 sound inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                            TM.Sounds.Snd_Win_Notification_Looping_Alarm4 = ZipEntry;
                        }

                        filesList.Add(ZipEntry, x);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Windows Notification Looping Alarm 4 sound `{x}` to the theme resources pack as `{ZipEntry}`.");
                    }

                    x = TM.Sounds.Snd_Win_Notification_Looping_Alarm5;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = $"{cache}Snd_Win_Notification_Looping_Alarm5{Path.GetExtension(x)}";
                        if (System.IO.File.Exists(x))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Windows Notification Looping Alarm 5 sound inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                            TM.Sounds.Snd_Win_Notification_Looping_Alarm5 = ZipEntry;
                        }

                        filesList.Add(ZipEntry, x);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Windows Notification Looping Alarm 5 sound `{x}` to the theme resources pack as `{ZipEntry}`.");
                    }

                    x = TM.Sounds.Snd_Win_Notification_Looping_Alarm6;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = $"{cache}Snd_Win_Notification_Looping_Alarm6{Path.GetExtension(x)}";
                        if (System.IO.File.Exists(x))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Windows Notification Looping Alarm 6 sound inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                            TM.Sounds.Snd_Win_Notification_Looping_Alarm6 = ZipEntry;
                        }

                        filesList.Add(ZipEntry, x);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Windows Notification Looping Alarm 6 sound `{x}` to the theme resources pack as `{ZipEntry}`.");
                    }

                    x = TM.Sounds.Snd_Win_Notification_Looping_Alarm7;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = $"{cache}Snd_Win_Notification_Looping_Alarm7{Path.GetExtension(x)}";
                        if (System.IO.File.Exists(x))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Windows Notification Looping Alarm 7 sound inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                            TM.Sounds.Snd_Win_Notification_Looping_Alarm7 = ZipEntry;
                        }

                        filesList.Add(ZipEntry, x);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Windows Notification Looping Alarm 7 sound `{x}` to the theme resources pack as `{ZipEntry}`.");
                    }

                    x = TM.Sounds.Snd_Win_Notification_Looping_Alarm8;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = $"{cache}Snd_Win_Notification_Looping_Alarm8{Path.GetExtension(x)}";
                        if (System.IO.File.Exists(x))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Windows Notification Looping Alarm 8 sound inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                            TM.Sounds.Snd_Win_Notification_Looping_Alarm8 = ZipEntry;
                        }

                        filesList.Add(ZipEntry, x);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Windows Notification Looping Alarm 8 sound `{x}` to the theme resources pack as `{ZipEntry}`.");
                    }

                    x = TM.Sounds.Snd_Win_Notification_Looping_Alarm9;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = $"{cache}Snd_Win_Notification_Looping_Alarm9{Path.GetExtension(x)}";
                        if (System.IO.File.Exists(x))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Windows Notification Looping Alarm 9 sound inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                            TM.Sounds.Snd_Win_Notification_Looping_Alarm9 = ZipEntry;
                        }

                        filesList.Add(ZipEntry, x);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Windows Notification Looping Alarm 9 sound `{x}` to the theme resources pack as `{ZipEntry}`.");
                    }

                    x = TM.Sounds.Snd_Win_Notification_Looping_Call;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = $"{cache}Snd_Win_Notification_Looping_Call{Path.GetExtension(x)}";
                        if (System.IO.File.Exists(x))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Windows Notification Looping Call sound inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                            TM.Sounds.Snd_Win_Notification_Looping_Call = ZipEntry;
                        }

                        filesList.Add(ZipEntry, x);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Windows Notification Looping Call sound `{x}` to the theme resources pack as `{ZipEntry}`.");
                    }

                    x = TM.Sounds.Snd_Win_Notification_Looping_Call10;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = $"{cache}Snd_Win_Notification_Looping_Call10{Path.GetExtension(x)}";
                        if (System.IO.File.Exists(x))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Windows Notification Looping Call 10 sound inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                            TM.Sounds.Snd_Win_Notification_Looping_Call10 = ZipEntry;
                        }

                        filesList.Add(ZipEntry, x);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Windows Notification Looping Call 10 sound `{x}` to the theme resources pack as `{ZipEntry}`.");
                    }

                    x = TM.Sounds.Snd_Win_Notification_Looping_Call2;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = $"{cache}Snd_Win_Notification_Looping_Call2{Path.GetExtension(x)}";
                        if (System.IO.File.Exists(x))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Windows Notification Looping Call 2 sound inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                            TM.Sounds.Snd_Win_Notification_Looping_Call2 = ZipEntry;
                        }

                        filesList.Add(ZipEntry, x);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Windows Notification Looping Call 2 sound `{x}` to the theme resources pack as `{ZipEntry}`.");
                    }

                    x = TM.Sounds.Snd_Win_Notification_Looping_Call3;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = $"{cache}Snd_Win_Notification_Looping_Call3{Path.GetExtension(x)}";
                        if (System.IO.File.Exists(x))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Windows Notification Looping Call 3 sound inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                            TM.Sounds.Snd_Win_Notification_Looping_Call3 = ZipEntry;
                        }

                        filesList.Add(ZipEntry, x);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Windows Notification Looping Call 3 sound `{x}` to the theme resources pack as `{ZipEntry}`.");
                    }

                    x = TM.Sounds.Snd_Win_Notification_Looping_Call4;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = $"{cache}Snd_Win_Notification_Looping_Call4{Path.GetExtension(x)}";
                        if (System.IO.File.Exists(x))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Windows Notification Looping Call 4 sound inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                            TM.Sounds.Snd_Win_Notification_Looping_Call4 = ZipEntry;
                        }

                        filesList.Add(ZipEntry, x);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Windows Notification Looping Call 4 sound `{x}` to the theme resources pack as `{ZipEntry}`.");
                    }

                    x = TM.Sounds.Snd_Win_Notification_Looping_Call5;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = $"{cache}Snd_Win_Notification_Looping_Call5{Path.GetExtension(x)}";
                        if (System.IO.File.Exists(x))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Windows Notification Looping Call 5 sound inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                            TM.Sounds.Snd_Win_Notification_Looping_Call5 = ZipEntry;
                        }

                        filesList.Add(ZipEntry, x);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Windows Notification Looping Call 5 sound `{x}` to the theme resources pack as `{ZipEntry}`.");
                    }

                    x = TM.Sounds.Snd_Win_Notification_Looping_Call6;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = $"{cache}Snd_Win_Notification_Looping_Call6{Path.GetExtension(x)}";
                        if (System.IO.File.Exists(x))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Windows Notification Looping Call 6 sound inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                            TM.Sounds.Snd_Win_Notification_Looping_Call6 = ZipEntry;
                        }

                        filesList.Add(ZipEntry, x);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Windows Notification Looping Call 6 sound `{x}` to the theme resources pack as `{ZipEntry}`.");
                    }

                    x = TM.Sounds.Snd_Win_Notification_Looping_Call7;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = $"{cache}Snd_Win_Notification_Looping_Call7{Path.GetExtension(x)}";
                        if (System.IO.File.Exists(x))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Windows Notification Looping Call 7 sound inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                            TM.Sounds.Snd_Win_Notification_Looping_Call7 = ZipEntry;
                        }

                        filesList.Add(ZipEntry, x);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Windows Notification Looping Call 7 sound `{x}` to the theme resources pack as `{ZipEntry}`.");
                    }

                    x = TM.Sounds.Snd_Win_Notification_Looping_Call8;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = $"{cache}Snd_Win_Notification_Looping_Call8{Path.GetExtension(x)}";
                        if (System.IO.File.Exists(x))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Windows Notification Looping Call 8 sound inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                            TM.Sounds.Snd_Win_Notification_Looping_Call8 = ZipEntry;
                        }

                        filesList.Add(ZipEntry, x);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Windows Notification Looping Call 8 sound `{x}` to the theme resources pack as `{ZipEntry}`.");
                    }

                    x = TM.Sounds.Snd_Win_Notification_Looping_Call9;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = $"{cache}Snd_Win_Notification_Looping_Call9{Path.GetExtension(x)}";
                        if (System.IO.File.Exists(x))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Windows Notification Looping Call 9 sound inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                            TM.Sounds.Snd_Win_Notification_Looping_Call9 = ZipEntry;
                        }

                        filesList.Add(ZipEntry, x);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Windows Notification Looping Call 9 sound `{x}` to the theme resources pack as `{ZipEntry}`.");
                    }

                    x = TM.Sounds.Snd_Win_Notification_Mail;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = $"{cache}Snd_Win_Notification_Mail{Path.GetExtension(x)}";
                        if (System.IO.File.Exists(x))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Windows Notification Mail sound inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                            TM.Sounds.Snd_Win_Notification_Mail = ZipEntry;
                        }

                        filesList.Add(ZipEntry, x);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Windows Notification Mail sound `{x}` to the theme resources pack as `{ZipEntry}`.");
                    }

                    x = TM.Sounds.Snd_Win_Notification_Proximity;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = $"{cache}Snd_Win_Notification_Proximity{Path.GetExtension(x)}";
                        if (System.IO.File.Exists(x))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Windows Notification Proximity sound inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                            TM.Sounds.Snd_Win_Notification_Proximity = ZipEntry;
                        }

                        filesList.Add(ZipEntry, x);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Windows Notification Proximity sound `{x}` to the theme resources pack as `{ZipEntry}`.");
                    }

                    x = TM.Sounds.Snd_Win_Notification_Reminder;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = $"{cache}Snd_Win_Notification_Reminder{Path.GetExtension(x)}";
                        if (System.IO.File.Exists(x))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Windows Notification Reminder sound inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                            TM.Sounds.Snd_Win_Notification_Reminder = ZipEntry;
                        }

                        filesList.Add(ZipEntry, x);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Windows Notification Reminder sound `{x}` to the theme resources pack as `{ZipEntry}`.");
                    }

                    x = TM.Sounds.Snd_Win_Notification_SMS;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = $"{cache}Snd_Win_Notification_SMS{Path.GetExtension(x)}";
                        if (System.IO.File.Exists(x))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Windows Notification SMS sound inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                            TM.Sounds.Snd_Win_Notification_SMS = ZipEntry;
                        }

                        filesList.Add(ZipEntry, x);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Windows Notification SMS sound `{x}` to the theme resources pack as `{ZipEntry}`.");
                    }

                    x = TM.Sounds.Snd_Win_Open;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = $"{cache}Snd_Win_Open{Path.GetExtension(x)}";
                        if (System.IO.File.Exists(x))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Windows Open sound inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                            TM.Sounds.Snd_Win_Open = ZipEntry;
                        }

                        filesList.Add(ZipEntry, x);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Windows Open sound `{x}` to the theme resources pack as `{ZipEntry}`.");
                    }

                    x = TM.Sounds.Snd_Win_PrintComplete;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = $"{cache}Snd_Win_PrintComplete{Path.GetExtension(x)}";
                        if (System.IO.File.Exists(x))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Windows Print Complete sound inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                            TM.Sounds.Snd_Win_PrintComplete = ZipEntry;
                        }

                        filesList.Add(ZipEntry, x);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Windows Print Complete sound `{x}` to the theme resources pack as `{ZipEntry}`.");
                    }

                    x = TM.Sounds.Snd_Win_ProximityConnection;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = $"{cache}Snd_Win_ProximityConnection{Path.GetExtension(x)}";
                        if (System.IO.File.Exists(x))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Windows Proximity Connection sound inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                            TM.Sounds.Snd_Win_ProximityConnection = ZipEntry;
                        }

                        filesList.Add(ZipEntry, x);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Windows Proximity Connection sound `{x}` to the theme resources pack as `{ZipEntry}`.");
                    }

                    x = TM.Sounds.Snd_Win_RestoreDown;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = $"{cache}Snd_Win_RestoreDown{Path.GetExtension(x)}";
                        if (System.IO.File.Exists(x))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Windows Restore Down sound inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                            TM.Sounds.Snd_Win_RestoreDown = ZipEntry;
                        }

                        filesList.Add(ZipEntry, x);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Windows Restore Down sound `{x}` to the theme resources pack as `{ZipEntry}`.");
                    }

                    x = TM.Sounds.Snd_Win_RestoreUp;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = $"{cache}Snd_Win_RestoreUp{Path.GetExtension(x)}";
                        if (System.IO.File.Exists(x))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Windows Restore Up sound inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                            TM.Sounds.Snd_Win_RestoreUp = ZipEntry;
                        }

                        filesList.Add(ZipEntry, x);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Windows Restore Up sound `{x}` to the theme resources pack as `{ZipEntry}`.");
                    }

                    x = TM.Sounds.Snd_Win_ShowBand;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = $"{cache}Snd_Win_ShowBand{Path.GetExtension(x)}";
                        if (System.IO.File.Exists(x))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Windows Show Band sound inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                            TM.Sounds.Snd_Win_ShowBand = ZipEntry;
                        }

                        filesList.Add(ZipEntry, x);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Windows Show Band sound `{x}` to the theme resources pack as `{ZipEntry}`.");
                    }

                    x = TM.Sounds.Snd_Win_SystemAsterisk;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = $"{cache}Snd_Win_SystemAsterisk{Path.GetExtension(x)}";
                        if (System.IO.File.Exists(x))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Windows System Asterisk sound inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                            TM.Sounds.Snd_Win_SystemAsterisk = ZipEntry;
                        }

                        filesList.Add(ZipEntry, x);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Windows System Asterisk sound `{x}` to the theme resources pack as `{ZipEntry}`.");
                    }

                    x = TM.Sounds.Snd_Win_SystemExclamation;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = $"{cache}Snd_Win_SystemExclamation{Path.GetExtension(x)}";
                        if (System.IO.File.Exists(x))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Windows System Exclamation sound inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                            TM.Sounds.Snd_Win_SystemExclamation = ZipEntry;
                        }

                        filesList.Add(ZipEntry, x);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Windows System Exclamation sound `{x}` to the theme resources pack as `{ZipEntry}`.");
                    }

                    x = TM.Sounds.Snd_Win_SystemExit;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = $"{cache}Snd_Win_SystemExit{Path.GetExtension(x)}";
                        if (System.IO.File.Exists(x))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Windows System Exit sound inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                            TM.Sounds.Snd_Win_SystemExit = ZipEntry;
                        }

                        filesList.Add(ZipEntry, x);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Windows System Exit sound `{x}` to the theme resources pack as `{ZipEntry}`.");
                    }

                    x = TM.Sounds.Snd_Win_SystemStart;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = $"{cache}Snd_Win_SystemStart{Path.GetExtension(x)}";
                        if (System.IO.File.Exists(x))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Windows System Start sound inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                            TM.Sounds.Snd_Win_SystemStart = ZipEntry;
                        }

                        filesList.Add(ZipEntry, x);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Windows System Start sound `{x}` to the theme resources pack as `{ZipEntry}`.");
                    }

                    x = TM.Sounds.Snd_Imageres_SystemStart;
                    if (!string.IsNullOrWhiteSpace(x))  // Don't include the condition: Not x.StartsWith(My.Directories.Windows & "\media", My.StringComparison.OrdinalIgnoreCase)
                    {
                        ZipEntry = $"{cache}Snd_Imageres_SystemStart{Path.GetExtension(x)}";
                        if (System.IO.File.Exists(x))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Imageres System Start sound inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                            TM.Sounds.Snd_Imageres_SystemStart = ZipEntry;
                        }

                        filesList.Add(ZipEntry, x);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Imageres System Start sound `{x}` to the theme resources pack as `{ZipEntry}`.");
                    }

                    x = TM.Sounds.Snd_Win_SystemHand;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = $"{cache}Snd_Win_SystemHand{Path.GetExtension(x)}";
                        if (System.IO.File.Exists(x))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Windows System Hand sound inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                            TM.Sounds.Snd_Win_SystemHand = ZipEntry;
                        }

                        filesList.Add(ZipEntry, x);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Windows System Hand sound `{x}` to the theme resources pack as `{ZipEntry}`.");
                    }

                    x = TM.Sounds.Snd_Win_SystemNotification;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = $"{cache}Snd_Win_SystemNotification{Path.GetExtension(x)}";
                        if (System.IO.File.Exists(x))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Windows System Notification sound inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                            TM.Sounds.Snd_Win_SystemNotification = ZipEntry;
                        }

                        filesList.Add(ZipEntry, x);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Windows System Notification sound `{x}` to the theme resources pack as `{ZipEntry}`.");
                    }

                    x = TM.Sounds.Snd_Win_SystemQuestion;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = $"{cache}Snd_Win_SystemQuestion{Path.GetExtension(x)}";
                        if (System.IO.File.Exists(x))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Windows System Question sound inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                            TM.Sounds.Snd_Win_SystemQuestion = ZipEntry;
                        }

                        filesList.Add(ZipEntry, x);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Windows System Question sound `{x}` to the theme resources pack as `{ZipEntry}`.");
                    }

                    x = TM.Sounds.Snd_Win_WindowsLogoff;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = $"{cache}Snd_Win_WindowsLogoff{Path.GetExtension(x)}";
                        if (System.IO.File.Exists(x))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Windows Logoff sound inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                            TM.Sounds.Snd_Win_WindowsLogoff = ZipEntry;
                        }

                        filesList.Add(ZipEntry, x);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Windows Logoff sound `{x}` to the theme resources pack as `{ZipEntry}`.");
                    }

                    x = TM.Sounds.Snd_Win_WindowsLogon;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = $"{cache}Snd_Win_WindowsLogon{Path.GetExtension(x)}";
                        if (System.IO.File.Exists(x))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Windows Logon sound inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                            TM.Sounds.Snd_Win_WindowsLogon = ZipEntry;
                        }

                        filesList.Add(ZipEntry, x);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Windows Logon sound `{x}` to the theme resources pack as `{ZipEntry}`.");
                    }

                    x = TM.Sounds.Snd_Win_WindowsUAC;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = $"{cache}Snd_Win_WindowsUAC{Path.GetExtension(x)}";
                        if (System.IO.File.Exists(x))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Windows UAC sound inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                            TM.Sounds.Snd_Win_WindowsUAC = ZipEntry;
                        }

                        filesList.Add(ZipEntry, x);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Windows UAC sound `{x}` to the theme resources pack as `{ZipEntry}`.");
                    }

                    x = TM.Sounds.Snd_Win_WindowsUnlock;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = $"{cache}Snd_Win_WindowsUnlock{Path.GetExtension(x)}";
                        if (System.IO.File.Exists(x))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Windows Unlock sound inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                            TM.Sounds.Snd_Win_WindowsUnlock = ZipEntry;
                        }

                        filesList.Add(ZipEntry, x);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Windows Unlock sound `{x}` to the theme resources pack as `{ZipEntry}`.");
                    }

                    x = TM.Sounds.Snd_Explorer_ActivatingDocument;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = $"{cache}Snd_Explorer_ActivatingDocument{Path.GetExtension(x)}";
                        if (System.IO.File.Exists(x))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Explorer Activating Document sound inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                            TM.Sounds.Snd_Explorer_ActivatingDocument = ZipEntry;
                        }

                        filesList.Add(ZipEntry, x);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Explorer Activating Document sound `{x}` to the theme resources pack as `{ZipEntry}`.");
                    }

                    x = TM.Sounds.Snd_Explorer_BlockedPopup;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = $"{cache}Snd_Explorer_BlockedPopup{Path.GetExtension(x)}";
                        if (System.IO.File.Exists(x))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Explorer Blocked Popup sound inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                            TM.Sounds.Snd_Explorer_BlockedPopup = ZipEntry;
                        }

                        filesList.Add(ZipEntry, x);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Explorer Blocked Popup sound `{x}` to the theme resources pack as `{ZipEntry}`.");
                    }

                    x = TM.Sounds.Snd_Explorer_EmptyRecycleBin;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = $"{cache}Snd_Explorer_EmptyRecycleBin{Path.GetExtension(x)}";
                        if (System.IO.File.Exists(x))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Explorer Empty Recycle Bin sound inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                            TM.Sounds.Snd_Explorer_EmptyRecycleBin = ZipEntry;
                        }

                        filesList.Add(ZipEntry, x);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Explorer Empty Recycle Bin sound `{x}` to the theme resources pack as `{ZipEntry}`.");
                    }

                    x = TM.Sounds.Snd_Explorer_FeedDiscovered;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = $"{cache}Snd_Explorer_FeedDiscovered{Path.GetExtension(x)}";
                        if (System.IO.File.Exists(x))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Explorer Feed Discovered sound inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                            TM.Sounds.Snd_Explorer_FeedDiscovered = ZipEntry;
                        }

                        filesList.Add(ZipEntry, x);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Explorer Feed Discovered sound `{x}` to the theme resources pack as `{ZipEntry}`.");
                    }

                    x = TM.Sounds.Snd_Explorer_MoveMenuItem;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = $"{cache}Snd_Explorer_MoveMenuItem{Path.GetExtension(x)}";
                        if (System.IO.File.Exists(x))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Explorer Move Menu Item sound inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                            TM.Sounds.Snd_Explorer_MoveMenuItem = ZipEntry;
                        }

                        filesList.Add(ZipEntry, x);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Explorer Move Menu Item sound `{x}` to the theme resources pack as `{ZipEntry}`.");
                    }

                    x = TM.Sounds.Snd_Explorer_Navigating;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = $"{cache}Snd_Explorer_Navigating{Path.GetExtension(x)}";
                        if (System.IO.File.Exists(x))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Explorer Navigating sound inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                            TM.Sounds.Snd_Explorer_Navigating = ZipEntry;
                        }

                        filesList.Add(ZipEntry, x);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Explorer Navigating sound `{x}` to the theme resources pack as `{ZipEntry}`.");
                    }

                    x = TM.Sounds.Snd_Explorer_SecurityBand;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = $"{cache}Snd_Explorer_SecurityBand{Path.GetExtension(x)}";
                        if (System.IO.File.Exists(x))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Explorer Security Band sound inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                            TM.Sounds.Snd_Explorer_SecurityBand = ZipEntry;
                        }

                        filesList.Add(ZipEntry, x);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Explorer Security Band sound `{x}` to the theme resources pack as `{ZipEntry}`.");
                    }

                    x = TM.Sounds.Snd_Explorer_SearchProviderDiscovered;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = $"{cache}Snd_Explorer_SearchProviderDiscovered{Path.GetExtension(x)}";
                        if (System.IO.File.Exists(x))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Explorer Search Provider Discovered sound inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                            TM.Sounds.Snd_Explorer_SearchProviderDiscovered = ZipEntry;
                        }

                        filesList.Add(ZipEntry, x);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Explorer Search Provider Discovered sound `{x}` to the theme resources pack as `{ZipEntry}`.");
                    }

                    x = TM.Sounds.Snd_Explorer_FaxError;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = $"{cache}Snd_Explorer_FaxError{Path.GetExtension(x)}";
                        if (System.IO.File.Exists(x))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Explorer Fax Error sound inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                            TM.Sounds.Snd_Explorer_FaxError = ZipEntry;
                        }

                        filesList.Add(ZipEntry, x);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Explorer Fax Error sound `{x}` to the theme resources pack as `{ZipEntry}`.");
                    }

                    x = TM.Sounds.Snd_Explorer_FaxLineRings;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = $"{cache}Snd_Explorer_FaxLineRings{Path.GetExtension(x)}";
                        if (System.IO.File.Exists(x))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Explorer Fax Line Rings sound inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                            TM.Sounds.Snd_Explorer_FaxLineRings = ZipEntry;
                        }

                        filesList.Add(ZipEntry, x);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Explorer Fax Line Rings sound `{x}` to the theme resources pack as `{ZipEntry}`.");
                    }

                    x = TM.Sounds.Snd_Explorer_FaxNew;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = $"{cache}Snd_Explorer_FaxNew{Path.GetExtension(x)}";
                        if (System.IO.File.Exists(x))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Explorer Fax New sound inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                            TM.Sounds.Snd_Explorer_FaxNew = ZipEntry;
                        }

                        filesList.Add(ZipEntry, x);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Explorer Fax New sound `{x}` to the theme resources pack as `{ZipEntry}`.");
                    }

                    x = TM.Sounds.Snd_Explorer_FaxSent;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = $"{cache}Snd_Explorer_FaxSent{Path.GetExtension(x)}";
                        if (System.IO.File.Exists(x))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Explorer Fax Sent sound inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                            TM.Sounds.Snd_Explorer_FaxSent = ZipEntry;
                        }

                        filesList.Add(ZipEntry, x);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Explorer Fax Sent sound `{x}` to the theme resources pack as `{ZipEntry}`.");
                    }

                    x = TM.Sounds.Snd_NetMeeting_PersonJoins;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = $"{cache}Snd_NetMeeting_PersonJoins{Path.GetExtension(x)}";
                        if (System.IO.File.Exists(x))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of NetMeeting Person Joins sound inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                            TM.Sounds.Snd_NetMeeting_PersonJoins = ZipEntry;
                        }

                        filesList.Add(ZipEntry, x);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding NetMeeting Person Joins sound `{x}` to the theme resources pack as `{ZipEntry}`.");
                    }

                    x = TM.Sounds.Snd_NetMeeting_PersonLeaves;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = $"{cache}Snd_NetMeeting_PersonLeaves{Path.GetExtension(x)}";
                        if (System.IO.File.Exists(x))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of NetMeeting Person Leaves sound inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                            TM.Sounds.Snd_NetMeeting_PersonLeaves = ZipEntry;
                        }

                        filesList.Add(ZipEntry, x);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding NetMeeting Person Leaves sound `{x}` to the theme resources pack as `{ZipEntry}`.");
                    }

                    x = TM.Sounds.Snd_NetMeeting_ReceiveCall;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = $"{cache}Snd_NetMeeting_ReceiveCall{Path.GetExtension(x)}";
                        if (System.IO.File.Exists(x))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of NetMeeting Receive Call sound inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                            TM.Sounds.Snd_NetMeeting_ReceiveCall = ZipEntry;
                        }

                        filesList.Add(ZipEntry, x);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding NetMeeting Receive Call sound `{x}` to the theme resources pack as `{ZipEntry}`.");
                    }

                    x = TM.Sounds.Snd_NetMeeting_ReceiveRequestToJoin;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = $"{cache}Snd_NetMeeting_ReceiveRequestToJoin{Path.GetExtension(x)}";
                        if (System.IO.File.Exists(x))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of NetMeeting Receive Request To Join sound inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                            TM.Sounds.Snd_NetMeeting_ReceiveRequestToJoin = ZipEntry;
                        }

                        filesList.Add(ZipEntry, x);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding NetMeeting Receive Request To Join sound `{x}` to the theme resources pack as `{ZipEntry}`.");
                    }

                    x = TM.Sounds.Snd_SpeechRec_DisNumbersSound;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = $"{cache}Snd_SpeechRec_DisNumbersSound{Path.GetExtension(x)}";
                        if (System.IO.File.Exists(x))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Speech Recognition Disable Numbers sound inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                            TM.Sounds.Snd_SpeechRec_DisNumbersSound = ZipEntry;
                        }

                        filesList.Add(ZipEntry, x);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Speech Recognition Disable Numbers sound `{x}` to the theme resources pack as `{ZipEntry}`.");
                    }

                    x = TM.Sounds.Snd_SpeechRec_HubOffSound;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = $"{cache}Snd_SpeechRec_HubOffSound{Path.GetExtension(x)}";
                        if (System.IO.File.Exists(x))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Speech Recognition Hub Off sound inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                            TM.Sounds.Snd_SpeechRec_HubOffSound = ZipEntry;
                        }

                        filesList.Add(ZipEntry, x);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Speech Recognition Hub Off sound `{x}` to the theme resources pack as `{ZipEntry}`.");
                    }

                    x = TM.Sounds.Snd_SpeechRec_HubOnSound;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = $"{cache}Snd_SpeechRec_HubOnSound{Path.GetExtension(x)}";
                        if (System.IO.File.Exists(x))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Speech Recognition Hub On sound inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                            TM.Sounds.Snd_SpeechRec_HubOnSound = ZipEntry;
                        }

                        filesList.Add(ZipEntry, x);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Speech Recognition Hub On sound `{x}` to the theme resources pack as `{ZipEntry}`.");
                    }

                    x = TM.Sounds.Snd_SpeechRec_HubSleepSound;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = $"{cache}Snd_SpeechRec_HubSleepSound{Path.GetExtension(x)}";
                        if (System.IO.File.Exists(x))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Speech Recognition Hub Sleep sound inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                            TM.Sounds.Snd_SpeechRec_HubSleepSound = ZipEntry;
                        }

                        filesList.Add(ZipEntry, x);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Speech Recognition Hub Sleep sound `{x}` to the theme resources pack as `{ZipEntry}`.");
                    }

                    x = TM.Sounds.Snd_SpeechRec_MisrecoSound;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = $"{cache}Snd_SpeechRec_MisrecoSound{Path.GetExtension(x)}";
                        if (System.IO.File.Exists(x))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Speech Recognition Misrecognition sound inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                            TM.Sounds.Snd_SpeechRec_MisrecoSound = ZipEntry;
                        }

                        filesList.Add(ZipEntry, x);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Speech Recognition Misrecognition sound `{x}` to the theme resources pack as `{ZipEntry}`.");
                    }

                    x = TM.Sounds.Snd_SpeechRec_PanelSound;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = $"{cache}Snd_SpeechRec_PanelSound{Path.GetExtension(x)}";
                        if (System.IO.File.Exists(x))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Speech Recognition Panel sound inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                            TM.Sounds.Snd_SpeechRec_PanelSound = ZipEntry;
                        }

                        filesList.Add(ZipEntry, x);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Speech Recognition Panel sound `{x}` to the theme resources pack as `{ZipEntry}`.");
                    }

                    x = TM.Sounds.Snd_ChargerConnected;
                    if (!string.IsNullOrWhiteSpace(x))
                    {
                        ZipEntry = $"{cache}Snd_ChargerConnected{Path.GetExtension(x)}";
                        if (System.IO.File.Exists(x))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Charger Connected sound inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                            TM.Sounds.Snd_ChargerConnected = ZipEntry;
                        }

                        filesList.Add(ZipEntry, x);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Charger Connected sound `{x}` to the theme resources pack as `{ZipEntry}`.");
                    }

                    x = TM.Sounds.Snd_ChargerDisconnected;
                    if (!string.IsNullOrWhiteSpace(x))
                    {
                        ZipEntry = $"{cache}Snd_ChargerDisconnected{Path.GetExtension(x)}";
                        if (System.IO.File.Exists(x))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Charger Disconnected sound inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                            TM.Sounds.Snd_ChargerDisconnected = ZipEntry;
                        }

                        filesList.Add(ZipEntry, x);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Charger Disconnected sound `{x}` to the theme resources pack as `{ZipEntry}`.");
                    }

                    x = TM.Sounds.Snd_Win_WindowsLock;
                    if (!string.IsNullOrWhiteSpace(x))
                    {
                        ZipEntry = $"{cache}Snd_Win_WindowsLock{Path.GetExtension(x)}";
                        if (System.IO.File.Exists(x))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Windows Lock sound inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                            TM.Sounds.Snd_Win_WindowsLock = ZipEntry;
                        }

                        filesList.Add(ZipEntry, x);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Windows Lock sound `{x}` to the theme resources pack as `{ZipEntry}`.");
                    }

                    x = TM.Sounds.Snd_WiFiConnected;
                    if (!string.IsNullOrWhiteSpace(x))
                    {
                        ZipEntry = $"{cache}Snd_WiFiConnected{Path.GetExtension(x)}";
                        if (System.IO.File.Exists(x))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of WiFi Connected sound inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                            TM.Sounds.Snd_WiFiConnected = ZipEntry;
                        }

                        filesList.Add(ZipEntry, x);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding WiFi Connected sound `{x}` to the theme resources pack as `{ZipEntry}`.");
                    }

                    x = TM.Sounds.Snd_WiFiDisconnected;
                    if (!string.IsNullOrWhiteSpace(x))
                    {
                        ZipEntry = $"{cache}Snd_WiFiDisconnected{Path.GetExtension(x)}";
                        if (System.IO.File.Exists(x))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of WiFi Disconnected sound inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                            TM.Sounds.Snd_WiFiDisconnected = ZipEntry;
                        }

                        filesList.Add(ZipEntry, x);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding WiFi Disconnected sound `{x}` to the theme resources pack as `{ZipEntry}`.");
                    }

                    x = TM.Sounds.Snd_WiFiConnectionFailed;
                    if (!string.IsNullOrWhiteSpace(x))
                    {
                        ZipEntry = $"{cache}Snd_WiFiConnectionFailed{Path.GetExtension(x)}";
                        if (System.IO.File.Exists(x))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of WiFi Connection Failed sound inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                            TM.Sounds.Snd_WiFiConnectionFailed = ZipEntry;
                        }

                        filesList.Add(ZipEntry, x);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding WiFi Connection Failed sound `{x}` to the theme resources pack as `{ZipEntry}`.");
                    }
                }
                #endregion

                // Add Icons files
                if (TM.Icons.Enabled)
                {
                    // Add 'Computer' icon
                    if (!string.IsNullOrWhiteSpace(TM.Icons.Computer))
                    {
                        ref string TargetProperty = ref TM.Icons.Computer;

                        bool exit = TargetProperty.StartsWith(SysPaths.imageres, StringComparison.OrdinalIgnoreCase)
                         || TargetProperty.StartsWith($"{SysPaths.System32}\\shell32.dll", StringComparison.OrdinalIgnoreCase);

                        // Don't include icon if is is inside imageres.dll or shell32.dll
                        if (!exit)
                        {
                            string iconName = "computer.ico";

                            ZipEntry = $"{cache}icons\\{iconName}";

                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Computer icon `{TargetProperty}` to the theme resources pack as `{ZipEntry}`.");

                            string tempFile = $"{SysPaths.appData}\\Temp\\{iconName}";

                            string iconFile = TargetProperty.Split(',')[0];
                            int iconIndex = TargetProperty.Contains(",") ? Convert.ToInt32(TargetProperty.Split(',')[1]) : 0;

                            if (File.Exists(iconFile))
                            {
                                if (Path.GetExtension(iconFile).ToLower() != ".ico")
                                {
                                    using (Icon icon = PE.GetIcon(iconFile, iconIndex))
                                    using (FileStream fs = new(tempFile, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                                    {
                                        icon.Save(fs);
                                        fs.Close();
                                    }
                                    filesList.Add(ZipEntry, tempFile);
                                }
                                else
                                {
                                    filesList.Add(ZipEntry, iconFile);
                                }

                                Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Computer icon is extracted from system and added to list of files to be compressed.");

                                TargetProperty = ZipEntry;
                            }
                        }
                    }

                    // Add 'User' icon
                    if (!string.IsNullOrWhiteSpace(TM.Icons.User))
                    {
                        ref string TargetProperty = ref TM.Icons.User;
                        bool exit = TargetProperty.StartsWith(SysPaths.imageres, StringComparison.OrdinalIgnoreCase)
                         || TargetProperty.StartsWith($"{SysPaths.System32}\\shell32.dll", StringComparison.OrdinalIgnoreCase);

                        // Don't include icon if is is inside imageres.dll or shell32.dll
                        if (!exit)
                        {
                            string iconName = "user.ico";

                            ZipEntry = $"{cache}icons\\{iconName}";

                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding User icon `{TargetProperty}` to the theme resources pack as `{ZipEntry}`.");

                            string tempFile = $"{SysPaths.appData}\\Temp\\{iconName}";

                            string iconFile = TargetProperty.Split(',')[0];
                            int iconIndex = TargetProperty.Contains(",") ? Convert.ToInt32(TargetProperty.Split(',')[1]) : 0;

                            if (File.Exists(iconFile))
                            {
                                if (Path.GetExtension(iconFile).ToLower() != ".ico")
                                {
                                    using (Icon icon = PE.GetIcon(iconFile, iconIndex))
                                    using (FileStream fs = new(tempFile, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                                    {
                                        icon.Save(fs);
                                        fs.Close();
                                    }
                                    filesList.Add(ZipEntry, tempFile);
                                }
                                else
                                {
                                    filesList.Add(ZipEntry, iconFile);
                                }

                                Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"User icon is extracted from system and added to list of files to be compressed.");

                                TargetProperty = ZipEntry;
                            }
                        }
                    }

                    // Add 'Network' icon
                    if (!string.IsNullOrWhiteSpace(TM.Icons.Network))
                    {
                        ref string TargetProperty = ref TM.Icons.Network;
                        bool exit = TargetProperty.StartsWith(SysPaths.imageres, StringComparison.OrdinalIgnoreCase)
                         || TargetProperty.StartsWith($"{SysPaths.System32}\\shell32.dll", StringComparison.OrdinalIgnoreCase);

                        // Don't include icon if is is inside imageres.dll or shell32.dll
                        if (!exit)
                        {
                            string iconName = "network.ico";

                            ZipEntry = $"{cache}icons\\{iconName}";

                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Network icon `{TargetProperty}` to the theme resources pack as `{ZipEntry}`.");

                            string tempFile = $"{SysPaths.appData}\\Temp\\{iconName}";

                            string iconFile = TargetProperty.Split(',')[0];
                            int iconIndex = TargetProperty.Contains(",") ? Convert.ToInt32(TargetProperty.Split(',')[1]) : 0;

                            if (File.Exists(iconFile))
                            {
                                if (Path.GetExtension(iconFile).ToLower() != ".ico")
                                {
                                    using (Icon icon = PE.GetIcon(iconFile, iconIndex))
                                    using (FileStream fs = new(tempFile, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                                    {
                                        icon.Save(fs);
                                        fs.Close();
                                    }
                                    filesList.Add(ZipEntry, tempFile);
                                }
                                else
                                {
                                    filesList.Add(ZipEntry, iconFile);
                                }

                                Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Network icon is extracted from system and added to list of files to be compressed.");

                                TargetProperty = ZipEntry;
                            }
                        }
                    }

                    // Add 'Control Panel' icon
                    if (!string.IsNullOrWhiteSpace(TM.Icons.ControlPanel))
                    {
                        ref string TargetProperty = ref TM.Icons.ControlPanel;
                        bool exit = TargetProperty.StartsWith(SysPaths.imageres, StringComparison.OrdinalIgnoreCase)
                                 || TargetProperty.StartsWith($"{SysPaths.System32}\\shell32.dll", StringComparison.OrdinalIgnoreCase);

                        // Don't include icon if is is inside imageres.dll or shell32.dll
                        if (!exit)
                        {
                            string iconName = "controlpanel.ico";

                            ZipEntry = $"{cache}icons\\{iconName}";

                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Control Panel icon `{TargetProperty}` to the theme resources pack as `{ZipEntry}`.");

                            string tempFile = $"{SysPaths.appData}\\Temp\\{iconName}";

                            string iconFile = TargetProperty.Split(',')[0];
                            int iconIndex = TargetProperty.Contains(",") ? Convert.ToInt32(TargetProperty.Split(',')[1]) : 0;

                            if (File.Exists(iconFile))
                            {
                                if (Path.GetExtension(iconFile).ToLower() != ".ico")
                                {
                                    using (Icon icon = PE.GetIcon(iconFile, iconIndex))
                                    using (FileStream fs = new(tempFile, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                                    {
                                        icon.Save(fs);
                                        fs.Close();
                                    }
                                    filesList.Add(ZipEntry, tempFile);
                                }
                                else
                                {
                                    filesList.Add(ZipEntry, iconFile);
                                }

                                Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Control Panel icon is extracted from system and added to list of files to be compressed.");

                                TargetProperty = ZipEntry;
                            }
                        }
                    }

                    // Add 'Recycle Bin (Empty)' icon
                    if (!string.IsNullOrWhiteSpace(TM.Icons.RecycleBinEmpty))
                    {
                        ref string TargetProperty = ref TM.Icons.RecycleBinEmpty;
                        bool exit = TargetProperty.StartsWith(SysPaths.imageres, StringComparison.OrdinalIgnoreCase)
                                 || TargetProperty.StartsWith($"{SysPaths.System32}\\shell32.dll", StringComparison.OrdinalIgnoreCase);

                        // Don't include icon if is is inside imageres.dll or shell32.dll
                        if (!exit)
                        {
                            string iconName = "recyclebinempty.ico";

                            ZipEntry = $"{cache}icons\\{iconName}";

                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Recycle Bin (Empty) icon `{TargetProperty}` to the theme resources pack as `{ZipEntry}`.");

                            string tempFile = $"{SysPaths.appData}\\Temp\\{iconName}";

                            string iconFile = TargetProperty.Split(',')[0];
                            int iconIndex = TargetProperty.Contains(",") ? Convert.ToInt32(TargetProperty.Split(',')[1]) : 0;

                            if (File.Exists(iconFile))
                            {
                                if (Path.GetExtension(iconFile).ToLower() != ".ico")
                                {
                                    using (Icon icon = PE.GetIcon(iconFile, iconIndex))
                                    using (FileStream fs = new(tempFile, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                                    {
                                        icon.Save(fs);
                                        fs.Close();
                                    }
                                    filesList.Add(ZipEntry, tempFile);
                                }
                                else
                                {
                                    filesList.Add(ZipEntry, iconFile);
                                }

                                Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Recycle Bin (Empty) icon is extracted from system and added to list of files to be compressed.");

                                TargetProperty = ZipEntry;
                            }
                        }
                    }

                    // Add 'Recycle Bin (Full)' icon
                    if (!string.IsNullOrWhiteSpace(TM.Icons.RecycleBinFull))
                    {
                        ref string TargetProperty = ref TM.Icons.RecycleBinFull;
                        bool exit = TargetProperty.StartsWith(SysPaths.imageres, StringComparison.OrdinalIgnoreCase)
                                 || TargetProperty.StartsWith($"{SysPaths.System32}\\shell32.dll", StringComparison.OrdinalIgnoreCase);

                        // Don't include icon if is is inside imageres.dll or shell32.dll
                        if (!exit)
                        {
                            string iconName = "recyclebinfull.ico";

                            ZipEntry = $"{cache}icons\\{iconName}";

                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Recycle Bin (Full) icon `{TargetProperty}` to the theme resources pack as `{ZipEntry}`.");

                            string tempFile = $"{SysPaths.appData}\\Temp\\{iconName}";

                            string iconFile = TargetProperty.Split(',')[0];
                            int iconIndex = TargetProperty.Contains(",") ? Convert.ToInt32(TargetProperty.Split(',')[1]) : 0;

                            if (File.Exists(iconFile))
                            {
                                if (Path.GetExtension(iconFile).ToLower() != ".ico")
                                {
                                    using (Icon icon = PE.GetIcon(iconFile, iconIndex))
                                    using (FileStream fs = new(tempFile, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                                    {
                                        icon.Save(fs);
                                        fs.Close();
                                    }
                                    filesList.Add(ZipEntry, tempFile);
                                }
                                else
                                {
                                    filesList.Add(ZipEntry, iconFile);
                                }

                                Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Recycle Bin (Full) icon is extracted from system and added to list of files to be compressed.");

                                TargetProperty = ZipEntry;
                            }
                        }
                    }

                    // Add 'System Drive' icon
                    if (!string.IsNullOrWhiteSpace(TM.Icons.SystemDriveIcon))
                    {
                        ref string TargetProperty = ref TM.Icons.SystemDriveIcon;
                        bool exit = TargetProperty.StartsWith(SysPaths.imageres, StringComparison.OrdinalIgnoreCase)
                                 || TargetProperty.StartsWith($"{SysPaths.System32}\\shell32.dll", StringComparison.OrdinalIgnoreCase);

                        // Don't include icon if is is inside imageres.dll or shell32.dll
                        if (!exit)
                        {
                            string iconName = "systemdriveicon.ico";

                            ZipEntry = $"{cache}icons\\{iconName}";

                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding System Drive icon `{TargetProperty}` to the theme resources pack as `{ZipEntry}`.");

                            string tempFile = $"{SysPaths.appData}\\Temp\\{iconName}";

                            string iconFile = TargetProperty.Split(',')[0];
                            int iconIndex = TargetProperty.Contains(",") ? Convert.ToInt32(TargetProperty.Split(',')[1]) : 0;

                            if (File.Exists(iconFile))
                            {
                                if (Path.GetExtension(iconFile).ToLower() != ".ico")
                                {
                                    using (Icon icon = PE.GetIcon(iconFile, iconIndex))
                                    using (FileStream fs = new(tempFile, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                                    {
                                        icon.Save(fs);
                                        fs.Close();
                                    }
                                    filesList.Add(ZipEntry, tempFile);
                                }
                                else
                                {
                                    filesList.Add(ZipEntry, iconFile);
                                }

                                Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"System Drive icon is extracted from system and added to list of files to be compressed.");

                                TargetProperty = ZipEntry;
                            }
                        }
                    }

                    // Add all icons used as wrappers for shell32.dll.
                    foreach (KeyValuePair<string, string> entry in TM.Icons.Shell32Wrapper.ToArray())
                    {
                        if (!string.IsNullOrWhiteSpace(entry.Value))
                        {
                            string TargetProperty = entry.Value;
                            if (TargetProperty.StartsWith(SysPaths.imageres, StringComparison.OrdinalIgnoreCase)
                             || TargetProperty.StartsWith($"{SysPaths.System32}\\shell32.dll", StringComparison.OrdinalIgnoreCase))
                            {
                                continue;
                            }

                            string shell32Index = entry.Key;
                            string iconName = $"shell32_{shell32Index}.ico";

                            ZipEntry = $"{cache}icons\\{iconName}";

                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Shell32.dll icon `{TargetProperty}` to the theme resources pack as `{ZipEntry}`.");

                            string tempFile = $"{SysPaths.appData}\\Temp\\{iconName}";

                            string iconFile = TargetProperty.Split(',')[0];
                            int iconIndex = TargetProperty.Contains(",") ? Convert.ToInt32(TargetProperty.Split(',')[1]) : 0;

                            if (File.Exists(iconFile))
                            {
                                if (Path.GetExtension(iconFile).ToLower() != ".ico")
                                {
                                    using (Icon icon = PE.GetIcon(iconFile, iconIndex))
                                    using (FileStream fs = new(tempFile, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                                    {
                                        icon.Save(fs);
                                        fs.Close();
                                    }
                                    filesList.Add(ZipEntry, tempFile);
                                }
                                else
                                {
                                    filesList.Add(ZipEntry, iconFile);
                                }

                                Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Shell32.dll icon ID `{shell32Index}` is extracted from system and added to list of files to be compressed.");

                                TM.Icons.Shell32Wrapper[entry.Key] = ZipEntry;
                            }
                        }
                    }

                    // Add all icons used as wrappers for control panel.
                    foreach (KeyValuePair<string, string> entry in TM.Icons.ControlPanelWrapper.ToArray())
                    {
                        if (!string.IsNullOrWhiteSpace(entry.Value))
                        {
                            string TargetProperty = entry.Value;
                            if (TargetProperty.StartsWith(SysPaths.imageres, StringComparison.OrdinalIgnoreCase)
                             || TargetProperty.StartsWith($"{SysPaths.System32}\\shell32.dll", StringComparison.OrdinalIgnoreCase))
                            {
                                continue;
                            }

                            string controlPanelIndex = entry.Key;
                            string iconName = $"controlpanel_{controlPanelIndex}.ico";

                            ZipEntry = $"{cache}icons\\{iconName}";

                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Control Panel icon `{TargetProperty}` to the theme resources pack as `{ZipEntry}`.");

                            string tempFile = $"{SysPaths.appData}\\Temp\\{iconName}";

                            string iconFile = TargetProperty.Split(',')[0];
                            int iconIndex = TargetProperty.Contains(",") ? Convert.ToInt32(TargetProperty.Split(',')[1]) : 0;

                            if (File.Exists(iconFile))
                            {
                                if (Path.GetExtension(iconFile).ToLower() != ".ico")
                                {
                                    using (Icon icon = PE.GetIcon(iconFile, iconIndex))
                                    using (FileStream fs = new(tempFile, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                                    {
                                        icon.Save(fs);
                                        fs.Close();
                                    }
                                    filesList.Add(ZipEntry, tempFile);
                                }
                                else
                                {
                                    filesList.Add(ZipEntry, iconFile);
                                }

                                Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Control Panel icon ID `{controlPanelIndex}` is extracted from system and added to list of files to be compressed.");

                                TM.Icons.ControlPanelWrapper[entry.Key] = ZipEntry;
                            }
                        }
                    }

                    // Add all icons used as wrappers for explorer.
                    foreach (KeyValuePair<string, string> entry in TM.Icons.ExplorerWrapper.ToArray())
                    {
                        if (!string.IsNullOrWhiteSpace(entry.Value))
                        {
                            string TargetProperty = entry.Value;
                            if (TargetProperty.StartsWith(SysPaths.imageres, StringComparison.OrdinalIgnoreCase)
                             || TargetProperty.StartsWith($"{SysPaths.System32}\\shell32.dll", StringComparison.OrdinalIgnoreCase))
                            {
                                continue;
                            }

                            string explorerIndex = entry.Key;
                            string iconName = $"explorer_{explorerIndex}.ico";

                            ZipEntry = $"{cache}icons\\{iconName}";

                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Explorer icon `{TargetProperty}` to the theme resources pack as `{ZipEntry}`.");

                            string tempFile = $"{SysPaths.appData}\\Temp\\{iconName}";

                            string iconFile = TargetProperty.Split(',')[0];
                            int iconIndex = TargetProperty.Contains(",") ? Convert.ToInt32(TargetProperty.Split(',')[1]) : 0;

                            if (File.Exists(iconFile))
                            {
                                if (Path.GetExtension(iconFile).ToLower() != ".ico")
                                {
                                    using (Icon icon = PE.GetIcon(iconFile, iconIndex))
                                    using (FileStream fs = new(tempFile, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                                    {
                                        icon.Save(fs);
                                        fs.Close();
                                    }
                                    filesList.Add(ZipEntry, tempFile);
                                }
                                else
                                {
                                    filesList.Add(ZipEntry, iconFile);
                                }

                                Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Explorer icon ID `{explorerIndex}` is extracted from system and added to list of files to be compressed.");

                                TM.Icons.ExplorerWrapper[entry.Key] = ZipEntry;
                            }
                        }
                    }
                }

                // Add Wallpaper file
                if (TM.Wallpaper.Enabled && TM.Wallpaper.WallpaperType == Wallpaper.WallpaperTypes.Picture)
                {
                    x = TM.Wallpaper.ImageFile;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\Web", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = $"{cache}wallpaper_file{Path.GetExtension(x)}";
                        if (System.IO.File.Exists(x))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Wallpaper file inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                            TM.Wallpaper.ImageFile = ZipEntry;
                        }

                        filesList.Add(ZipEntry, x);

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Adding Wallpaper file `{x}` to the theme resources pack as `{ZipEntry}`.");
                    }
                }

                // Create the archive by adding files from the built list
                foreach (KeyValuePair<string, string> _file in filesList)
                {
                    if (System.IO.File.Exists(_file.Value))
                    {
                        archive.CreateEntryFromFile(_file.Value, _file.Key.Split('\\').Last(), CompressionLevel.Optimal);
                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"File `{_file.Value}` is compressed inside themes resources pack as `{_file.Key}`.");
                    }
                }

                // Add Visual Styles files of Windows WXP
                if (TM.WindowsXP.VisualStyles.VisualStylesType == Structures.VisualStyles.DefaultVisualStyles.Custom)
                {
                    x = TM.WindowsXP.VisualStyles.ThemeFile;
                    if (!string.IsNullOrWhiteSpace(x) && System.IO.File.Exists(x) && !x.StartsWith($@"{SysPaths.Windows}\Resources\Themes\Luna", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = $@"{cache}WXP_VS\{Path.GetFileName(x)}";
                        if (System.IO.File.Exists(x))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Windows XP Visual Style file inside the theme resources pack to be `{ZipEntry}`. The previous value was `{x}`.");
                            TM.WindowsXP.VisualStyles.ThemeFile = ZipEntry;
                        }

                        string DirName = new FileInfo(x).Directory.FullName;
                        foreach (string file in Directory.EnumerateFiles(DirName, "*.*", SearchOption.AllDirectories))
                        {
                            if (System.IO.File.Exists(file))
                            {
                                archive.CreateEntryFromFile(file, $"WXP_VS{file.Replace(DirName, string.Empty)}", CompressionLevel.Optimal);
                                Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Compressing Windows XP Visual Style file `{file}` to the theme resources pack as `WXP_VS{file.Replace(DirName, string.Empty)}`.");
                            }
                        }
                    }
                }

                // Add Visual Styles files of Windows 12
                if (TM.Windows12.VisualStyles.Enabled)
                {
                    ref string targetProperty = ref TM.Windows12.VisualStyles.ThemeFile;
                    if (!string.IsNullOrWhiteSpace(targetProperty) && System.IO.File.Exists(targetProperty) && !targetProperty.StartsWith($@"{SysPaths.Windows}\Resources\Themes\Aero", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = $@"{cache}W12_VS\{Path.GetFileName(targetProperty)}";
                        if (System.IO.File.Exists(targetProperty))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Windows 12 Visual Style file inside the theme resources pack to be `{ZipEntry}`. The previous value was `{targetProperty}`.");
                            targetProperty = ZipEntry;
                        }

                        string DirName = new FileInfo(targetProperty).Directory.FullName;
                        foreach (string file in Directory.EnumerateFiles(DirName, "*.*", SearchOption.AllDirectories))
                        {
                            if (System.IO.File.Exists(file))
                            {
                                archive.CreateEntryFromFile(file, $"W12_VS{file.Replace(DirName, string.Empty)}", CompressionLevel.Optimal);
                                Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Compressing Windows 12 Visual Style file `{file}` to the theme resources pack as `W12_VS{file.Replace(DirName, string.Empty)}`.");
                            }
                        }
                    }
                }

                // Add Visual Styles files of Windows 11
                if (TM.Windows11.VisualStyles.Enabled)
                {
                    ref string targetProperty = ref TM.Windows11.VisualStyles.ThemeFile;
                    if (!string.IsNullOrWhiteSpace(targetProperty) && System.IO.File.Exists(targetProperty) && !targetProperty.StartsWith($@"{SysPaths.Windows}\Resources\Themes\Aero", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = $@"{cache}W11_VS\{Path.GetFileName(targetProperty)}";
                        if (System.IO.File.Exists(targetProperty))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Windows 11 Visual Style file inside the theme resources pack to be `{ZipEntry}`. The previous value was `{targetProperty}`.");
                            targetProperty = ZipEntry;
                        }

                        string DirName = new FileInfo(targetProperty).Directory.FullName;
                        foreach (string file in Directory.EnumerateFiles(DirName, "*.*", SearchOption.AllDirectories))
                        {
                            if (System.IO.File.Exists(file))
                            {
                                archive.CreateEntryFromFile(file, $"W11_VS{file.Replace(DirName, string.Empty)}", CompressionLevel.Optimal);
                                Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Compressing Windows 11 Visual Style file `{file}` to the theme resources pack as `W11_VS{file.Replace(DirName, string.Empty)}`.");
                            }
                        }
                    }
                }

                // Add Visual Styles files of Windows 10
                if (TM.Windows10.VisualStyles.Enabled)
                {
                    ref string targetProperty = ref TM.Windows10.VisualStyles.ThemeFile;
                    if (!string.IsNullOrWhiteSpace(targetProperty) && System.IO.File.Exists(targetProperty) && !targetProperty.StartsWith($@"{SysPaths.Windows}\Resources\Themes\Aero", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = $@"{cache}W10_VS\{Path.GetFileName(targetProperty)}";
                        if (System.IO.File.Exists(targetProperty))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Windows 10 Visual Style file inside the theme resources pack to be `{ZipEntry}`. The previous value was `{targetProperty}`.");
                            targetProperty = ZipEntry;
                        }

                        string DirName = new FileInfo(targetProperty).Directory.FullName;
                        foreach (string file in Directory.EnumerateFiles(DirName, "*.*", SearchOption.AllDirectories))
                        {
                            if (System.IO.File.Exists(file))
                            {
                                archive.CreateEntryFromFile(file, $"W10_VS{file.Replace(DirName, string.Empty)}", CompressionLevel.Optimal);
                                Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Compressing Windows 10 Visual Style file `{file}` to the theme resources pack as `W10_VS{file.Replace(DirName, string.Empty)}`.");
                            }
                        }
                    }
                }

                // Add Visual Styles files of Windows 8.1
                if (TM.Windows81.VisualStyles.Enabled)
                {
                    ref string targetProperty = ref TM.Windows81.VisualStyles.ThemeFile;
                    if (!string.IsNullOrWhiteSpace(targetProperty) && System.IO.File.Exists(targetProperty) && !targetProperty.StartsWith($@"{SysPaths.Windows}\Resources\Themes\Aero", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = $@"{cache}W81_VS\{Path.GetFileName(targetProperty)}";
                        if (System.IO.File.Exists(targetProperty))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Windows 8.1 Visual Style file inside the theme resources pack to be `{ZipEntry}`. The previous value was `{targetProperty}`.");
                            targetProperty = ZipEntry;
                        }

                        string DirName = new FileInfo(targetProperty).Directory.FullName;
                        foreach (string file in Directory.EnumerateFiles(DirName, "*.*", SearchOption.AllDirectories))
                        {
                            if (System.IO.File.Exists(file))
                            {
                                archive.CreateEntryFromFile(file, $"W81_VS{file.Replace(DirName, string.Empty)}", CompressionLevel.Optimal);
                                Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Compressing Windows 8.1 Visual Style file `{file}` to the theme resources pack as `W81_VS{file.Replace(DirName, string.Empty)}`.");
                            }
                        }
                    }
                }

                // Add Visual Styles files of Windows 7
                if (TM.Windows7.VisualStyles.Enabled)
                {
                    ref string targetProperty = ref TM.Windows7.VisualStyles.ThemeFile;
                    if (!string.IsNullOrWhiteSpace(targetProperty) && System.IO.File.Exists(targetProperty) && !targetProperty.StartsWith($@"{SysPaths.Windows}\Resources\Themes\Aero", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = $@"{cache}W7_VS\{Path.GetFileName(targetProperty)}";
                        if (System.IO.File.Exists(targetProperty))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Windows 7 Visual Style file inside the theme resources pack to be `{ZipEntry}`. The previous value was `{targetProperty}`.");
                            targetProperty = ZipEntry;
                        }

                        string DirName = new FileInfo(targetProperty).Directory.FullName;
                        foreach (string file in Directory.EnumerateFiles(DirName, "*.*", SearchOption.AllDirectories))
                        {
                            if (System.IO.File.Exists(file))
                            {
                                archive.CreateEntryFromFile(file, $"W7_VS{file.Replace(DirName, string.Empty)}", CompressionLevel.Optimal);
                                Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Compressing Windows 7 Visual Style file `{file}` to the theme resources pack as `W7_VS{file.Replace(DirName, string.Empty)}`.");
                            }
                        }
                    }
                }

                // Add Visual Styles files of Windows Vista
                if (TM.WindowsVista.VisualStyles.Enabled)
                {
                    ref string targetProperty = ref TM.WindowsVista.VisualStyles.ThemeFile;
                    if (!string.IsNullOrWhiteSpace(targetProperty) && System.IO.File.Exists(targetProperty) && !targetProperty.StartsWith($@"{SysPaths.Windows}\Resources\Themes\Aero", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = $@"{cache}WVista_VS\{Path.GetFileName(targetProperty)}";
                        if (System.IO.File.Exists(targetProperty))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Windows Vista Visual Style file inside the theme resources pack to be `{ZipEntry}`. The previous value was `{targetProperty}`.");
                            targetProperty = ZipEntry;
                        }

                        string DirName = new FileInfo(targetProperty).Directory.FullName;
                        foreach (string file in Directory.EnumerateFiles(DirName, "*.*", SearchOption.AllDirectories))
                        {
                            if (System.IO.File.Exists(file))
                            {
                                archive.CreateEntryFromFile(file, $"WVista_VS{file.Replace(DirName, string.Empty)}", CompressionLevel.Optimal);
                                Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Compressing Windows Vista Visual Style file `{file}` to the theme resources pack as `WVista_VS{file.Replace(DirName, string.Empty)}`.");
                            }
                        }
                    }
                }

                // Add Visual Styles files of Windows WXP
                if (TM.WindowsXP.VisualStyles.Enabled)
                {
                    ref string targetProperty = ref TM.WindowsXP.VisualStyles.ThemeFile;
                    if (!string.IsNullOrWhiteSpace(targetProperty) && System.IO.File.Exists(targetProperty) && !targetProperty.StartsWith($@"{SysPaths.Windows}\Resources\Themes\Aero", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = $@"{cache}WXP_VS\{Path.GetFileName(targetProperty)}";
                        if (System.IO.File.Exists(targetProperty))
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Windows XP Visual Style file inside the theme resources pack to be `{ZipEntry}`. The previous value was `{targetProperty}`.");
                            targetProperty = ZipEntry;
                        }

                        string DirName = new FileInfo(targetProperty).Directory.FullName;
                        foreach (string file in Directory.EnumerateFiles(DirName, "*.*", SearchOption.AllDirectories))
                        {
                            if (System.IO.File.Exists(file))
                            {
                                archive.CreateEntryFromFile(file, $"WXP_VS{file.Replace(DirName, string.Empty)}", CompressionLevel.Optimal);
                                Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Compressing Windows XP Visual Style file `{file}` to the theme resources pack as `WXP_VS{file.Replace(DirName, string.Empty)}`.");
                            }
                        }
                    }
                }

                // Add wallpapers files if the wallpaper type is 'SlideShow'
                if (TM.Wallpaper.Enabled && TM.Wallpaper.WallpaperType == Wallpaper.WallpaperTypes.SlideShow)
                {
                    // Determine if the slideshow is a folder or a list of images
                    if (TM.Wallpaper.SlideShow_Folder_or_ImagesList)
                    {
                        x = TM.Wallpaper.Wallpaper_Slideshow_ImagesRootPath;
                        if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\Web", StringComparison.OrdinalIgnoreCase))
                        {
                            TM.Wallpaper.Wallpaper_Slideshow_ImagesRootPath = $"{cache}wallpapers_slideshow";
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Wallpaper Slideshow root folder inside the theme resources pack to be `{TM.Wallpaper.Wallpaper_Slideshow_ImagesRootPath}`. The previous value was `{x}`.");

                            foreach (string image in Directory.EnumerateFiles(x, "*.*", SearchOption.TopDirectoryOnly).Where(s => s.EndsWith(".bmp") || s.EndsWith(".jpg") || s.EndsWith(".png") || s.EndsWith(".gif")))
                            {
                                if (System.IO.File.Exists(image))
                                {
                                    archive.CreateEntryFromFile(image, $@"wallpapers_slideshow\{new FileInfo(image).Name}", CompressionLevel.Optimal);
                                    Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Compressing Wallpaper Slideshow image `{image}` to the theme resources pack as `wallpapers_slideshow\\{new FileInfo(image).Name}`.");
                                }
                            }
                        }
                    }

                    else
                    {
                        string[] arr = [.. TM.Wallpaper.Wallpaper_Slideshow_Images];
                        if (arr.Count() > 0)
                        {
                            if (!arr[0].StartsWith($@"{SysPaths.Windows}\Web", StringComparison.OrdinalIgnoreCase))
                            {
                                TM.Wallpaper.Wallpaper_Slideshow_ImagesRootPath = $"{cache}WallpapersList";
                                Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Wallpaper Slideshow images list inside the theme resources pack to be `{TM.Wallpaper.Wallpaper_Slideshow_ImagesRootPath}`. The previous value was `{string.Join(", ", arr)}`.");

                                TM.Wallpaper.Wallpaper_Slideshow_Images = [];
                                for (int x0 = 0, loopTo = arr.Count() - 1; x0 <= loopTo; x0++)
                                {
                                    x = arr[x0];
                                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith($@"{SysPaths.Windows}\Web", StringComparison.OrdinalIgnoreCase))
                                    {
                                        ZipEntry = $@"{cache}WallpapersList\wallpaperlist_{x0}_file{Path.GetExtension(x)}";
                                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Modifying entry of Wallpaper Slideshow image `{x}` inside the theme resources pack to be `{ZipEntry}`.");
                                        if (System.IO.File.Exists(x))
                                        {
                                            TM.Wallpaper.Wallpaper_Slideshow_Images = [.. TM.Wallpaper.Wallpaper_Slideshow_Images, ZipEntry];
                                            archive.CreateEntryFromFile(x, $@"WallpapersList\wallpaperlist_{x0}_file{Path.GetExtension(x)}", CompressionLevel.Optimal);
                                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Compressing Wallpaper Slideshow image `{x}` to the theme resources pack as `WallpapersList\\wallpaperlist_{x0}_file{Path.GetExtension(x)}`.");
                                        }
                                    }
                                }
                            }
                        }
                    }

                }

                // Write the modified theme manager that has modified entries with environment variables that are suitable with the created pack
                System.IO.File.WriteAllText(ThemeFile, TM.ToString());

                Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Theme Manager file `{ThemeFile}` is written with modified entries suitable for the created theme resources pack.");
            }

            foreach (string file_to_delete in System.IO.Directory.GetFiles($"{SysPaths.appData}\\Temp"))
            {
                Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Deleting temporary file: {file_to_delete}");
                System.IO.File.Delete(file_to_delete);
            }
        }
    }
}
