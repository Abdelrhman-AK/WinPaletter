using Microsoft.VisualBasic;
using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Windows.Forms;
using WinPaletter.NativeMethods;
using WinPaletter.Theme.Structures;
using static WinPaletter.NativeMethods.User32;

namespace WinPaletter.Theme
{
    /// <summary>
    /// This class is responsible for managing WinPaletter theme
    /// </summary>
    public partial class Manager
    {
        /// <summary>
        /// Create new instance of WinPaletter theme
        /// </summary>
        /// <param name="Source">Source from which WinPaletter will get theme data. It can be from registry, file or empty.</param>
        /// <param name="File">If selected source is file, this will specify WinPaletter theme file</param>
        /// <param name="IgnoreExtractionThemePack">This will ignore theme resources pack extraction, useful for previewing or getting theme data quickly without data extraction.</param>
        public Manager(Source Source, string File = "", bool IgnoreExtractionThemePack = false)
        {
            switch (Source)
            {
                case Source.Registry:
                    {
                        using (WindowsImpersonationContext wic = User.Identity.Impersonate())
                        {
                            using (Manager @default = Theme.Default.Get(Program.PreviewStyle))
                            {
                                Exceptions.ThemeLoad.Clear();
                                Info.Load();
                                Windows12.Load(@default.Windows12);
                                Windows11.Load(@default.Windows11);
                                Windows10.Load(@default.Windows10);
                                Windows81.Load(@default.Windows81);
                                Windows7.Load(@default.Windows7);
                                WindowsVista.Load(@default.WindowsVista);
                                WindowsXP.Load(@default.WindowsXP);
                                WindowsEffects.Load(@default.WindowsEffects);
                                LogonUI10x.Load(@default.LogonUI10x);
                                LogonUI7.Load(@default.LogonUI7);
                                LogonUIXP.Load(@default.LogonUIXP);
                                Win32.Load();
                                MetricsFonts.Load(@default.MetricsFonts);
                                AltTab.Load(@default.AltTab);
                                ScreenSaver.Load(@default.ScreenSaver);
                                Sounds.Load(@default.Sounds);
                                AppTheme.Load(@default.AppTheme);

                                WallpaperTone_W12.Load("Win12");
                                WallpaperTone_W11.Load("Win11");
                                WallpaperTone_W10.Load("Win10");
                                WallpaperTone_W81.Load("Win8.1");
                                WallpaperTone_W7.Load("Win7");
                                WallpaperTone_WVista.Load("WinVista");
                                WallpaperTone_WXP.Load("WinXP");
                                Wallpaper.Load(@default.Wallpaper);

                                CommandPrompt.Load(string.Empty, "Terminal_CMD_Enabled", @default.CommandPrompt);
                                if (Directory.Exists(PathsExt.PS86_app))
                                {
                                    try { Registry.CurrentUser.CreateSubKey(@"Console\" + PathsExt.PS86_reg, true).Close(); }
                                    catch { PowerShellx86.Load(PathsExt.PS86_reg, "Terminal_PS_32_Enabled", @default.PowerShellx86); }
                                }
                                else { PowerShellx86 = @default.PowerShellx86; }

                                if (Directory.Exists(PathsExt.PS64_app))
                                {
                                    try { Registry.CurrentUser.CreateSubKey(@"Console\" + PathsExt.PS64_reg, true).Close(); }
                                    catch { PowerShellx64.Load(PathsExt.PS64_reg, "Terminal_PS_64_Enabled", @default.PowerShellx64); }
                                }
                                else { PowerShellx64 = @default.PowerShellx64; }

                                #region Windows Terminal
                                Terminal.Enabled = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Terminals", "Terminal_Stable_Enabled", 0)) == 1;
                                TerminalPreview.Enabled = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Terminals", "Terminal_Preview_Enabled", 0)) == 1;

                                if (OS.W12 || OS.W11 || OS.W10)
                                {
                                    string TerDir;
                                    string TerPreDir;

                                    if (!Program.Settings.WindowsTerminals.Path_Deflection)
                                    {
                                        TerDir = PathsExt.TerminalJSON;
                                        TerPreDir = PathsExt.TerminalPreviewJSON;
                                    }
                                    else
                                    {
                                        if (System.IO.File.Exists(Program.Settings.WindowsTerminals.Terminal_Stable_Path))
                                        { TerDir = Program.Settings.WindowsTerminals.Terminal_Stable_Path; }
                                        else { TerDir = PathsExt.TerminalJSON; }

                                        if (System.IO.File.Exists(Program.Settings.WindowsTerminals.Terminal_Preview_Path))
                                        { TerPreDir = Program.Settings.WindowsTerminals.Terminal_Preview_Path; }
                                        else { TerPreDir = PathsExt.TerminalPreviewJSON; }
                                    }

                                    if (System.IO.File.Exists(TerDir)) { Terminal = new WinTerminal(TerDir, WinTerminal.Mode.JSONFile); }
                                    else { Terminal = new WinTerminal(string.Empty, WinTerminal.Mode.Empty); }

                                    if (System.IO.File.Exists(TerPreDir)) { TerminalPreview = new WinTerminal(TerPreDir, WinTerminal.Mode.JSONFile, WinTerminal.Version.Preview); }
                                    else { TerminalPreview = new WinTerminal(string.Empty, WinTerminal.Mode.Empty, WinTerminal.Version.Preview); }
                                }
                                else
                                {
                                    Terminal = new WinTerminal(string.Empty, WinTerminal.Mode.Empty);
                                    TerminalPreview = new WinTerminal(string.Empty, WinTerminal.Mode.Empty, WinTerminal.Version.Preview);
                                }
                                #endregion

                                #region Cursors
                                Cursor_Enabled = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors", string.Empty, false));

                                if (!SystemParametersInfo(SPI.SPI_GETCURSORSHADOW, 0, ref Cursor_Shadow, SPIF.SPIF_NONE))
                                    Cursor_Shadow = @default.Cursor_Shadow;

                                if (!SystemParametersInfo(SPI.SPI_GETMOUSETRAILS, 0, ref Cursor_Trails, SPIF.SPIF_NONE))
                                    Cursor_Trails = @default.Cursor_Trails;

                                if (!SystemParametersInfo(SPI.SPI_GETMOUSESONAR, 0, ref Cursor_Sonar, SPIF.SPIF_NONE))
                                    Cursor_Sonar = @default.Cursor_Sonar;

                                Cursor_Arrow.Load("Arrow");
                                Cursor_Help.Load("Help");
                                Cursor_AppLoading.Load("AppLoading");
                                Cursor_Busy.Load("Busy");
                                Cursor_Move.Load("Move");
                                Cursor_NS.Load("NS");
                                Cursor_EW.Load("EW");
                                Cursor_NESW.Load("NESW");
                                Cursor_NWSE.Load("NWSE");
                                Cursor_Up.Load("Up");
                                Cursor_Pen.Load("Pen");
                                Cursor_None.Load("None");
                                Cursor_Link.Load("Link");
                                Cursor_Pin.Load("Pin");
                                Cursor_Person.Load("Person");
                                Cursor_IBeam.Load("IBeam");
                                Cursor_Cross.Load("Cross");
                                #endregion

                                if (Exceptions.ThemeLoad.Count > 0)
                                {
                                    Forms.Saving_ex_list.ex_List = Exceptions.ThemeLoad;
                                    Forms.Saving_ex_list.ShowDialog();
                                }
                            }
                        }


                        break;
                    }

                case Source.File:
                    {
                        using (var TMx = Default.Get())
                        {
                            foreach (FieldInfo field in GetType().GetFields(bindingFlags))
                            {
                                Type type = field.FieldType;
                                try { field.SetValue(this, field.GetValue(TMx)); }
                                catch { };
                            }

                        Start:
                            ;

                            if (!System.IO.File.Exists(File)) return;

                            // Rough method to get theme name to create its proper resources pack folder
                            foreach (string line in Decompress(File))
                            {
                                if (line.Trim().StartsWith("\"ThemeName\":", StringComparison.OrdinalIgnoreCase))
                                {
                                    Info.ThemeName = line.Split(':')[1].ToString().Replace("\"", string.Empty).Replace(",", string.Empty).Trim();
                                    break;
                                }
                            }

                            var txt = new List<string>();
                            txt.Clear();
                            string Pack = new FileInfo(File).DirectoryName + @"\" + Path.GetFileNameWithoutExtension(File) + ".wptp";
                            bool Pack_IsValid = System.IO.File.Exists(Pack) && new FileInfo(Pack).Length > 0L && converter.GetFormat(File) == Converter_CP.WP_Format.JSON;
                            string cache = PathsExt.ThemeResPackCache + @"\" + string.Concat(Info.ThemeName.Replace(" ", string.Empty).Split(Path.GetInvalidFileNameChars()));

                            // Extract theme resources pack
                            try
                            {
                                if (Pack_IsValid & !IgnoreExtractionThemePack)
                                {
                                    if (!Directory.Exists(cache))
                                        Directory.CreateDirectory(cache);

                                    using (var s = new FileStream(Pack, FileMode.Open, FileAccess.Read))
                                    {
                                        using (var archive = new ZipArchive(s, ZipArchiveMode.Read))
                                        {
                                            foreach (ZipArchiveEntry entry in archive.Entries)
                                            {
                                                if (entry.FullName.Contains(@"\"))
                                                {
                                                    string dest = Path.Combine(cache, entry.FullName);
                                                    string dest_dir = dest.Replace(@"\" + dest.Split('\\').Last(), string.Empty);
                                                    if (!Directory.Exists(dest_dir))
                                                        Directory.CreateDirectory(dest_dir);
                                                }
                                                entry.ExtractToFile(Path.Combine(cache, entry.FullName), true);
                                            }
                                        }
                                        s.Close();
                                        s.Dispose();
                                    }
                                }
                            }

                            catch (Exception ex)
                            {
                                Pack_IsValid = false;
                                Forms.BugReport.ThrowError(ex);
                            }

                            txt = (List<string>)Decompress(File);

                            if (IsValidJson(string.Join("\r\n", txt)))
                            {
                                // Replace %WinPaletterAppData% variable with a valid AppData folder path
                                for (int x = 0, loopTo = txt.Count - 1; x <= loopTo; x++)
                                {
                                    if (txt[x].Contains(":"))
                                    {
                                        string[] arr = txt[x].Split(':');
                                        if (arr.Count() == 2 && arr[1].Contains("%WinPaletterAppData%"))
                                        {
                                            txt[x] = arr[0] + ":" + arr[1].Replace("%WinPaletterAppData%", PathsExt.appData.Replace(@"\", @"\\"));
                                        }
                                    }
                                }

                                JObject J = JObject.Parse(string.Join("\r\n", txt));

                                // This will get the new added features to fix bug (null values on opening a theme file)
                                try
                                {
                                    JObject J_Original = JObject.Parse(TMx.ToString(true));
                                    foreach (var item in J_Original)
                                    {
                                        if (J[item.Key] is null && J_Original[item.Key] is not null)
                                            J[item.Key] = J_Original[item.Key];
                                        if (item.Value is not JValue)
                                        {
                                            foreach (KeyValuePair<string, JToken> prop in (JObject)item.Value)
                                            {
                                                try
                                                {
                                                    if (J[item.Key][prop.Key] is null && J_Original[item.Key] is not null && J_Original[item.Key][prop.Key] is not null)
                                                    {
                                                        J[item.Key][prop.Key] = J_Original[item.Key][prop.Key];
                                                    }
                                                }
                                                catch { }
                                            }
                                        }
                                    }
                                }
                                catch { }

                                foreach (FieldInfo field in GetType().GetFields(bindingFlags))
                                {
                                    var type = field.FieldType;
                                    var JSet = new JsonSerializerSettings();

                                    if (J[field.Name] is not null)
                                        field.SetValue(this, J[field.Name].ToObject(type));
                                }
                            }

                            else if (converter.GetFormat(File) == Converter_CP.WP_Format.WPTH)
                            {
                                if (MsgBox(Program.Lang.Convert_Detect_Old_OnLoading0, MessageBoxButtons.YesNo, MessageBoxIcon.Question, Program.Lang.Convert_Detect_Old_OnLoading1, string.Empty, string.Empty, string.Empty, string.Empty, Program.Lang.Convert_Detect_Old_OnLoading2, Ookii.Dialogs.WinForms.TaskDialogIcon.Information) == DialogResult.Yes)
                                {
                                    converter.Convert(File, File, Program.Settings.FileTypeManagement.CompressThemeFile, false);
                                    goto Start;
                                }
                            }

                            else { MsgBox(Program.Lang.Convert_Error_Phrasing, MessageBoxButtons.OK, MessageBoxIcon.Error); }
                        }
                        break;
                    }
            }
        }

        /// <summary>
        /// Save or apply WinPaletter theme
        /// </summary>
        /// <param name="Destination">Destination into which WinPaletter will write theme data. It can be registry or file.</param>
        /// <param name="File">If selected destination is file, this will specify WinPaletter theme file</param>
        /// <param name="TreeView">Specify TreeView to write theme applying log (Registry destination only)</param>
        /// <param name="ResetToDefault">Restore Windows theme to default before applying WinPaletter theme</param>
        public void Save(Source Destination, string File = "", TreeView TreeView = null, bool ResetToDefault = false)
        {
            switch (Destination)
            {
                case Source.Registry:
                    {
                        using (WindowsImpersonationContext wic = User.Identity.Impersonate())
                        {
                            bool ReportProgress = Program.Settings.ThemeLog.VerboseLevel != WPSettings.Structures.ThemeLog.VerboseLevels.None && TreeView is not null;
                            bool ReportProgress_Detailed = ReportProgress && Program.Settings.ThemeLog.VerboseLevel == WPSettings.Structures.ThemeLog.VerboseLevels.Detailed;

                            _ErrorHappened = false;

                            var sw_all = new Stopwatch();
                            sw_all.Reset();
                            sw_all.Start();

                            if (ReportProgress)
                            {
                                Exceptions.ThemeApply.Clear();
                                TreeView.Visible = false;
                                TreeView.Nodes.Clear();
                                TreeView.Visible = true;
                                string OS_str;

                                if (OS.W12) { OS_str = Program.Lang.OS_Win12; }

                                else if (OS.W11) { OS_str = Program.Lang.OS_Win11; }

                                else if (OS.W10) { OS_str = Program.Lang.OS_Win10; }

                                else if (OS.W8) { OS_str = Program.Lang.OS_Win8; }

                                else if (OS.W81) { OS_str = Program.Lang.OS_Win81; }

                                else if (OS.W7) { OS_str = Program.Lang.OS_Win7; }

                                else if (OS.WVista) { OS_str = Program.Lang.OS_WinVista; }

                                else if (OS.WXP) { OS_str = Program.Lang.OS_WinXP; }

                                else { OS_str = Program.Lang.OS_WinUndefined; }

                                AddNode(TreeView, string.Format("{0}", string.Format(Program.Lang.TM_ApplyFrom, OS_str)), "info");

                                AddNode(TreeView, string.Format("{0}: {1}", DateTime.Now.ToLongTimeString(), Program.Lang.TM_Applying_Started), "info");

                                if (!Program.Elevated)
                                {
                                    AddNode(TreeView, string.Format("{0}", Program.Lang.TM_Admin_Msg0), "admin");
                                    AddNode(TreeView, string.Format("{0}", Program.Lang.TM_Admin_Msg1), "admin");
                                }

                            }

                            // Reset to default Windows theme
                            if (ResetToDefault)
                            {
                                Execute(() =>
                                {
                                    using (var def = Theme.Default.Get())
                                    {
                                        def.LogonUI10x.NoLockScreen = false;
                                        def.LogonUI7.Enabled = false;
                                        def.Windows81.NoLockScreen = false;
                                        def.LogonUIXP.Enabled = true;
                                        if (!OS.WXP) ResetCursorsToAero(); else ResetCursorsToNone_XP();
                                        def.CommandPrompt.Enabled = true;
                                        def.PowerShellx86.Enabled = true;
                                        def.PowerShellx64.Enabled = true;
                                        def.MetricsFonts.Enabled = true;
                                        def.WindowsEffects.Enabled = true;
                                        def.AltTab.Enabled = true;
                                        def.ScreenSaver.Enabled = true;
                                        def.Sounds.Enabled = true;
                                        def.AppTheme.Enabled = true;
                                        def.Wallpaper.Enabled = false;
                                        def.Save(Source.Registry);
                                    }
                                }, TreeView, Program.Lang.TM_ThemeReset, Program.Lang.TM_ThemeReset_Error, Program.Lang.TM_Time, sw_all);
                            }

                            // Theme info
                            Execute(() => Info.Apply(ReportProgress_Detailed ? TreeView : null), TreeView, Program.Lang.TM_SavingInfo, Program.Lang.TM_SavingInfo_Error, Program.Lang.TM_Time, sw_all);

                            // WinPaletter application theme
                            Execute(() => AppTheme.Apply(ReportProgress_Detailed ? TreeView : null), TreeView, Program.Lang.TM_Applying_AppTheme, Program.Lang.TM_Error_AppTheme, Program.Lang.TM_Time, sw_all, !AppTheme.Enabled, Program.Lang.TM_Skip_AppTheme, true);

                            // Wallpaper
                            // Make Wallpaper before the following LogonUI items, to make a logonUI that depends on current wallpaper gets the correct file
                            this.Execute(new MethodInvoker(() => Wallpaper.Apply(false, ReportProgress_Detailed ? TreeView : null)), TreeView, Program.Lang.TM_Applying_Wallpaper, Program.Lang.TM_Error_Wallpaper, Program.Lang.TM_Time, sw_all, !Wallpaper.Enabled, Program.Lang.TM_Skip_Wallpaper);

                            if (OS.W12)
                            {
                                this.Execute(new MethodInvoker(() => Windows12.Apply(ReportProgress_Detailed ? TreeView : null)), TreeView, Program.Lang.TM_Applying_Win12, Program.Lang.TM_W11_Error, Program.Lang.TM_Time, sw_all);

                                this.Execute(new MethodInvoker(() => LogonUI10x.Apply(ReportProgress_Detailed ? TreeView : null)), TreeView, Program.Lang.TM_Applying_LogonUI12, Program.Lang.TM_LogonUI11_Error, Program.Lang.TM_Time, sw_all);
                            }

                            if (OS.W11)
                            {
                                this.Execute(new MethodInvoker(() => Windows11.Apply(ReportProgress_Detailed ? TreeView : null)), TreeView, Program.Lang.TM_Applying_Win11, Program.Lang.TM_W11_Error, Program.Lang.TM_Time, sw_all);

                                this.Execute(new MethodInvoker(() => LogonUI10x.Apply(ReportProgress_Detailed ? TreeView : null)), TreeView, Program.Lang.TM_Applying_LogonUI11, Program.Lang.TM_LogonUI11_Error, Program.Lang.TM_Time, sw_all);
                            }

                            if (OS.W10)
                            {
                                this.Execute(new MethodInvoker(() => Windows10.Apply(ReportProgress_Detailed ? TreeView : null)), TreeView, Program.Lang.TM_Applying_Win10, Program.Lang.TM_W10_Error, Program.Lang.TM_Time, sw_all);

                                this.Execute(new MethodInvoker(() => LogonUI10x.Apply(ReportProgress_Detailed ? TreeView : null)), TreeView, Program.Lang.TM_Applying_LogonUI10, Program.Lang.TM_LogonUI10_Error, Program.Lang.TM_Time, sw_all);
                            }

                            if (OS.W8 || OS.W81)
                            {
                                this.Execute(new MethodInvoker(() =>
                                {
                                    Windows81.Apply(ReportProgress_Detailed ? TreeView : null);
                                    Program.RefreshDWM(this);
                                }), TreeView, Program.Lang.TM_Applying_Win81, Program.Lang.TM_W81_Error, Program.Lang.TM_Time, sw_all);

                                this.Execute(new MethodInvoker(() => Apply_LogonUI_8(TreeView)), TreeView, Program.Lang.TM_Applying_LogonUI8, Program.Lang.TM_LogonUI8_Error, Program.Lang.TM_Time, sw_all);
                            }

                            if (OS.W7)
                            {
                                this.Execute(new MethodInvoker(() =>
                                {
                                    Windows7.Apply(ReportProgress_Detailed ? TreeView : null);
                                    Program.RefreshDWM(this);
                                }), TreeView, Program.Lang.TM_Applying_Win7, Program.Lang.TM_W7_Error, Program.Lang.TM_Time, sw_all);

                                this.Execute(new MethodInvoker(() => Apply_LogonUI7(LogonUI7, "LogonUI", TreeView)), TreeView, Program.Lang.TM_Applying_LogonUI7, Program.Lang.TM_LogonUI7_Error, Program.Lang.TM_Time, sw_all);
                            }

                            if (OS.WVista)
                            {
                                this.Execute(new MethodInvoker(() =>
                                {
                                    WindowsVista.Apply(ReportProgress_Detailed ? TreeView : null);
                                    Program.RefreshDWM(this);
                                }), TreeView, Program.Lang.TM_Applying_WinVista, Program.Lang.TM_WVista_Error, Program.Lang.TM_Time, sw_all);
                            }

                            if (OS.WXP)
                            {
                                this.Execute(new MethodInvoker(() => WindowsXP.Apply(ReportProgress_Detailed ? TreeView : null)), TreeView, Program.Lang.TM_Applying_WinXP, Program.Lang.TM_WXP_Error, Program.Lang.TM_Time, sw_all);

                                this.Execute(new MethodInvoker(() => LogonUIXP.Apply(ReportProgress_Detailed ? TreeView : null)), TreeView, Program.Lang.TM_Applying_LogonUIXP, Program.Lang.TM_LogonUIXP_Error, Program.Lang.TM_Time, sw_all);
                            }

                            // Win32UI
                            this.Execute(new MethodInvoker(() => Win32.Apply(ReportProgress_Detailed ? TreeView : null)), TreeView, Program.Lang.TM_Applying_Win32UI, Program.Lang.TM_WIN32UI_Error, Program.Lang.TM_Time, sw_all);

                            // WindowsEffects
                            this.Execute(new MethodInvoker(() => WindowsEffects.Apply(ReportProgress_Detailed ? TreeView : null)), TreeView, Program.Lang.TM_Applying_WinEffects, Program.Lang.TM_WinEffects_Error, Program.Lang.TM_Time, sw_all);

                            // Metrics\Fonts
                            this.Execute(new MethodInvoker(() => MetricsFonts.Apply(ReportProgress_Detailed ? TreeView : null)), TreeView, Program.Lang.TM_Applying_Metrics, Program.Lang.TM_Error_Metrics, Program.Lang.TM_Time_They, sw_all, !MetricsFonts.Enabled, Program.Lang.TM_Skip_Metrics);

                            // AltTab
                            this.Execute(new MethodInvoker(() => AltTab.Apply(ReportProgress_Detailed ? TreeView : null)), TreeView, Program.Lang.TM_Applying_AltTab, Program.Lang.TM_Error_AltTab, Program.Lang.TM_Time, sw_all, !AltTab.Enabled, Program.Lang.TM_Skip_AltTab, true);

                            // WallpaperTone
                            this.Execute(new MethodInvoker(() =>
                            {
                                WallpaperTone.Save_To_Registry(WallpaperTone_W12, "Win12", ReportProgress_Detailed ? TreeView : null);
                                WallpaperTone.Save_To_Registry(WallpaperTone_W11, "Win11", ReportProgress_Detailed ? TreeView : null);
                                WallpaperTone.Save_To_Registry(WallpaperTone_W10, "Win10", ReportProgress_Detailed ? TreeView : null);
                                WallpaperTone.Save_To_Registry(WallpaperTone_W81, "Win8.1", ReportProgress_Detailed ? TreeView : null);
                                WallpaperTone.Save_To_Registry(WallpaperTone_W7, "Win7", ReportProgress_Detailed ? TreeView : null);
                                WallpaperTone.Save_To_Registry(WallpaperTone_WVista, "WinVista", ReportProgress_Detailed ? TreeView : null);
                                WallpaperTone.Save_To_Registry(WallpaperTone_WXP, "WinXP", ReportProgress_Detailed ? TreeView : null);

                                if (Wallpaper.Enabled)
                                {
                                    if (OS.W12 & WallpaperTone_W12.Enabled)
                                        WallpaperTone_W12.Apply(ReportProgress_Detailed ? TreeView : null);

                                    if (OS.W11 & WallpaperTone_W11.Enabled)
                                        WallpaperTone_W11.Apply(ReportProgress_Detailed ? TreeView : null);

                                    if (OS.W10 & WallpaperTone_W10.Enabled)
                                        WallpaperTone_W10.Apply(ReportProgress_Detailed ? TreeView : null);

                                    if (OS.W81 & WallpaperTone_W81.Enabled)
                                        WallpaperTone_W81.Apply(ReportProgress_Detailed ? TreeView : null);

                                    if (OS.W7 & WallpaperTone_W7.Enabled)
                                        WallpaperTone_W7.Apply(ReportProgress_Detailed ? TreeView : null);

                                    if (OS.WVista & WallpaperTone_WVista.Enabled)
                                        WallpaperTone_WVista.Apply(ReportProgress_Detailed ? TreeView : null);

                                    if (OS.WXP & WallpaperTone_WXP.Enabled)
                                        WallpaperTone_WXP.Apply(ReportProgress_Detailed ? TreeView : null);
                                }

                            }), TreeView, Program.Lang.TM_Applying_WallpaperTone, Program.Lang.TM_WallpaperTone_Error, Program.Lang.TM_Time, sw_all);

                            #region Consoles
                            EditReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Terminals", "Terminal_CMD_Enabled", CommandPrompt.Enabled);
                            EditReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Terminals", "Terminal_PS_32_Enabled", PowerShellx86.Enabled);
                            EditReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Terminals", "Terminal_PS_64_Enabled", PowerShellx64.Enabled);
                            EditReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Terminals", "Terminal_Stable_Enabled", Terminal.Enabled);
                            EditReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Terminals", "Terminal_Preview_Enabled", TerminalPreview.Enabled);

                            this.Execute(new MethodInvoker(() => Apply_CommandPrompt(ReportProgress_Detailed ? TreeView : null)), TreeView, Program.Lang.TM_Applying_CMD, Program.Lang.TM_CMD_Error, Program.Lang.TM_Time, sw_all, !CommandPrompt.Enabled, Program.Lang.TM_Skip_CMD);

                            this.Execute(new MethodInvoker(() => Apply_PowerShell86(ReportProgress_Detailed ? TreeView : null)), TreeView, Program.Lang.TM_Applying_PS32, Program.Lang.TM_PS32_Error, Program.Lang.TM_Time, sw_all, !PowerShellx86.Enabled, Program.Lang.TM_Skip_PS32);

                            this.Execute(new MethodInvoker(() => Apply_PowerShell64(ReportProgress_Detailed ? TreeView : null)), TreeView, Program.Lang.TM_Applying_PS64, Program.Lang.TM_PS64_Error, Program.Lang.TM_Time, sw_all, !PowerShellx64.Enabled, Program.Lang.TM_Skip_PS64);
                            #endregion

                            #region Windows Terminal
                            var sw = new Stopwatch();
                            sw.Reset();
                            sw.Start();
                            if (OS.W12 || OS.W11 || OS.W10)
                            {
                                if (ReportProgress)
                                {
                                    if (Terminal.Enabled & TerminalPreview.Enabled)
                                    {
                                        AddNode(TreeView, string.Format("{0}: {1}", DateTime.Now.ToLongTimeString(), Program.Lang.TM_Check_Terminals), "info");
                                    }

                                    else if (Terminal.Enabled)
                                    {
                                        AddNode(TreeView, string.Format("{0}: {1}", DateTime.Now.ToLongTimeString(), Program.Lang.TM_Skip_TerminalPreview), "skip");
                                        AddNode(TreeView, string.Format("{0}: {1}", DateTime.Now.ToLongTimeString(), Program.Lang.TM_Check_TerminalStable), "info");
                                    }

                                    else if (TerminalPreview.Enabled)
                                    {
                                        AddNode(TreeView, string.Format("{0}: {1}", DateTime.Now.ToLongTimeString(), Program.Lang.TM_Skip_TerminalStable), "skip");
                                        AddNode(TreeView, string.Format("{0}: {1}", DateTime.Now.ToLongTimeString(), Program.Lang.TM_Check_TerminalPreview), "info");
                                    }

                                    else
                                    {
                                        AddNode(TreeView, string.Format("{0}: {1}", DateTime.Now.ToLongTimeString(), Program.Lang.TM_Skip_Terminals), "skip");

                                    }

                                }

                                string TerDir;
                                string TerPreDir;

                                if (!Program.Settings.WindowsTerminals.Path_Deflection)
                                {
                                    TerDir = PathsExt.TerminalJSON;
                                    TerPreDir = PathsExt.TerminalPreviewJSON;
                                }
                                else
                                {
                                    if (System.IO.File.Exists(Program.Settings.WindowsTerminals.Terminal_Stable_Path))
                                    {
                                        TerDir = Program.Settings.WindowsTerminals.Terminal_Stable_Path;
                                    }
                                    else
                                    {
                                        TerDir = PathsExt.TerminalJSON;
                                    }

                                    if (System.IO.File.Exists(Program.Settings.WindowsTerminals.Terminal_Preview_Path))
                                    {
                                        TerPreDir = Program.Settings.WindowsTerminals.Terminal_Preview_Path;
                                    }
                                    else
                                    {
                                        TerPreDir = PathsExt.TerminalPreviewJSON;
                                    }
                                }

                                if (Terminal.Enabled)
                                {
                                    if (System.IO.File.Exists(TerDir))
                                    {

                                        try
                                        {
                                            AddNode(TreeView, string.Format("{0}: {1}", DateTime.Now.ToLongTimeString(), Program.Lang.TM_Applying_TerminalStable), "info");
                                            Terminal.Save(TerDir, WinTerminal.Mode.JSONFile);
                                            if (ReportProgress)
                                                AddNode(TreeView, string.Format(Program.Lang.TM_Time, sw.ElapsedMilliseconds / 1000d), "time");
                                        }
                                        catch (Exception ex)
                                        {
                                            sw.Stop();
                                            sw_all.Stop();
                                            _ErrorHappened = true;
                                            if (ReportProgress)
                                            {
                                                AddNode(TreeView, string.Format("{0}: {1}", DateTime.Now.ToLongTimeString(), Program.Lang.TM_Error_TerminalStable), "error");
                                                AddException(Program.Lang.TM_Error_TerminalStable, ex);
                                            }
                                            else
                                            {
                                                Forms.BugReport.ThrowError(ex);
                                            }

                                            sw.Start();
                                            sw_all.Start();
                                        }
                                    }


                                    else if (!Program.Settings.WindowsTerminals.Path_Deflection)
                                    {
                                        AddNode(TreeView, string.Format("{0}: {1}", DateTime.Now.ToLongTimeString(), Program.Lang.TM_Skip_TerminalStable_NotInstalled), "skip");
                                    }
                                    else
                                    {
                                        AddNode(TreeView, string.Format("{0}: {1}", DateTime.Now.ToLongTimeString(), Program.Lang.TM_Skip_TerminalStable_DeflectionNotFound), "skip");

                                    }
                                }

                                if (TerminalPreview.Enabled)
                                {
                                    if (System.IO.File.Exists(TerPreDir))
                                    {

                                        try
                                        {
                                            AddNode(TreeView, string.Format("{0}: {1}", DateTime.Now.ToLongTimeString(), Program.Lang.TM_Applying_TerminalPreview), "info");
                                            TerminalPreview.Save(TerPreDir, WinTerminal.Mode.JSONFile, WinTerminal.Version.Preview);
                                            if (ReportProgress)
                                                AddNode(TreeView, string.Format(Program.Lang.TM_Time, sw.ElapsedMilliseconds / 1000d), "time");
                                        }
                                        catch (Exception ex)
                                        {
                                            sw.Stop();
                                            sw_all.Stop();
                                            _ErrorHappened = true;
                                            if (ReportProgress)
                                            {
                                                AddNode(TreeView, string.Format("{0}: {1}", DateTime.Now.ToLongTimeString(), Program.Lang.TM_Error_TerminalPreview), "error");
                                                AddException(Program.Lang.TM_Error_TerminalPreview, ex);
                                            }
                                            else
                                            {
                                                Forms.BugReport.ThrowError(ex);
                                            }

                                            sw.Start();
                                            sw_all.Start();
                                        }
                                    }

                                    else if (!Program.Settings.WindowsTerminals.Path_Deflection)
                                    {
                                        AddNode(TreeView, string.Format("{0}: {1}", DateTime.Now.ToLongTimeString(), Program.Lang.TM_Skip_TerminalPreview_NotInstalled), "skip");
                                    }
                                    else
                                    {
                                        AddNode(TreeView, string.Format("{0}: {1}", DateTime.Now.ToLongTimeString(), Program.Lang.TM_Skip_TerminalPreview_DeflectionNotFound), "skip");
                                    }
                                }
                            }

                            else
                            {
                                AddNode(TreeView, string.Format("{0}: {1}", DateTime.Now.ToLongTimeString(), Program.Lang.TM_Skip_Terminals_NotSupported), "skip");
                            }
                            sw.Stop();
                            #endregion

                            // ScreenSaver
                            this.Execute(new MethodInvoker(() => ScreenSaver.Apply(ReportProgress_Detailed ? TreeView : null)), TreeView, Program.Lang.TM_Applying_ScreenSaver, Program.Lang.TM_Error_ScreenSaver, Program.Lang.TM_Time, sw_all);

                            // Sounds
                            this.Execute(new MethodInvoker(() => Sounds.Apply(ReportProgress_Detailed ? TreeView : null)), TreeView, Program.Lang.TM_Applying_Sounds, Program.Lang.TM_Error_Sounds, Program.Lang.TM_Time, sw_all, !Sounds.Enabled, Program.Lang.TM_Skip_Sounds);

                            // Cursors
                            this.Execute(new MethodInvoker(() => Apply_Cursors(TreeView)), TreeView, string.Empty, Program.Lang.TM_Error_Cursors, Program.Lang.TM_Time_Cursors, sw_all);

                            // Update LogonUI wallpaper in HKEY_USERS\.DEFAULT
                            if (Program.Settings.ThemeApplyingBehavior.Desktop_HKU_DEFAULT == WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite)
                            {
                                this.Execute(new MethodInvoker(() =>
                                {
                                    EditReg(ReportProgress_Detailed ? TreeView : null, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop", "Wallpaper", GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop", "Wallpaper", string.Empty), RegistryValueKind.String);
                                    EditReg(ReportProgress_Detailed ? TreeView : null, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop", "WallpaperStyle", GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop", "WallpaperStyle", "2"), RegistryValueKind.String);
                                    EditReg(ReportProgress_Detailed ? TreeView : null, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop", "TileWallpaper", GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop", "TileWallpaper", "0"), RegistryValueKind.String);
                                    EditReg(ReportProgress_Detailed ? TreeView : null, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop", "Pattern", GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop", "Pattern", string.Empty), RegistryValueKind.String);
                                }), TreeView, Program.Lang.TM_Applying_DesktopAllUsers, Program.Lang.TM_Error_SetDesktop, Program.Lang.TM_Time);
                            }

                            else if (Program.Settings.ThemeApplyingBehavior.Desktop_HKU_DEFAULT == WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.RestoreDefaults)
                            {

                                this.Execute(new MethodInvoker(() =>
                                {
                                    EditReg(ReportProgress_Detailed ? TreeView : null, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop", "Wallpaper", string.Empty, RegistryValueKind.String);
                                    EditReg(ReportProgress_Detailed ? TreeView : null, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop", "WallpaperStyle", "2", RegistryValueKind.String);
                                    EditReg(ReportProgress_Detailed ? TreeView : null, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop", "TileWallpaper", "0", RegistryValueKind.String);
                                    EditReg(ReportProgress_Detailed ? TreeView : null, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop", "Pattern", string.Empty, RegistryValueKind.String);
                                }), TreeView, Program.Lang.TM_Applying_DesktopAllUsers, Program.Lang.TM_Error_SetDesktop, Program.Lang.TM_Time);

                            }

                            // Update User Preference Mask for HKEY_USERS\.DEFAULT
                            // Always make it the last operation
                            try
                            {
                                Win32.Broadcast_UPM_ToDefUsers(ReportProgress_Detailed ? TreeView : null);
                            }
                            catch
                            {
                            }

                            PostMessage((IntPtr)User32.HWND_BROADCAST, User32.WindowsMessages.WM_SYSCOLORCHANGE, UIntPtr.Zero, IntPtr.Zero);
                            PostMessage((IntPtr)User32.HWND_BROADCAST, User32.WindowsMessages.WM_PALETTECHANGED, UIntPtr.Zero, IntPtr.Zero);
                            PostMessage((IntPtr)User32.HWND_BROADCAST, User32.WindowsMessages.WM_DWMCOLORIZATIONCOLORCHANGED, UIntPtr.Zero, IntPtr.Zero);
                            PostMessage((IntPtr)User32.HWND_BROADCAST, User32.WindowsMessages.WM_DWMCOMPOSITIONCHANGED, UIntPtr.Zero, IntPtr.Zero);
                            PostMessage((IntPtr)User32.HWND_BROADCAST, User32.WindowsMessages.WM_THEMECHANGED, UIntPtr.Zero, IntPtr.Zero);
                            PostMessage((IntPtr)User32.HWND_BROADCAST, User32.WindowsMessages.WM_SETTINGCHANGE, UIntPtr.Zero, IntPtr.Zero);
                            PostMessage((IntPtr)User32.HWND_BROADCAST, User32.WindowsMessages.WM_WININICHANGE, UIntPtr.Zero, IntPtr.Zero);

                            if (ReportProgress)
                            {
                                if (!_ErrorHappened)
                                {
                                    AddNode(TreeView, string.Format("{0}: {1}", DateTime.Now.ToLongTimeString(), string.Format(Program.Lang.TM_Applied, sw_all.ElapsedMilliseconds / 1000d)), "success");
                                }
                                else
                                {
                                    AddNode(TreeView, string.Format("{0}: {1}", DateTime.Now.ToLongTimeString(), string.Format(Program.Lang.TM_AppliedWithErrors, sw_all.ElapsedMilliseconds / 1000d)), "warning");
                                }
                            }

                            sw_all.Reset();
                            sw_all.Stop();
                            wic.Undo();
                        }
                        break;
                    }

                case Source.File:
                    {
                        if (System.IO.File.Exists(File))
                        {
                            try
                            {
                                FileSystem.Kill(File);
                            }
                            catch
                            {
                            }
                        }

                        if (Info.ExportResThemePack)
                        {
                            PackThemeResources((Manager)Clone(), File, new FileInfo(File).DirectoryName + @"\" + Path.GetFileNameWithoutExtension(File) + ".wptp");
                        }
                        else
                        {
                            System.IO.File.WriteAllText(File, ToString());
                        }

                        break;
                    }

            }
        }

        /// <summary>
        /// WinPaletter theme file contents
        /// </summary>
        /// <param name="IgnoreCompression"></param>
        /// <returns></returns>
        public string ToString(bool IgnoreCompression = false)
        {
            var JSON_Overall = new JObject();
            JSON_Overall.RemoveAll();

            Info.AppVersion = Program.Version;

            foreach (FieldInfo field in GetType().GetFields(bindingFlags))
            {
                var type = field.FieldType;

                if (IsStructure(type))
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
        /// <param name="ThemeFile">WinPaletter theme file</param>
        /// <param name="Pack">Theme resources pack file</param>
        public void PackThemeResources(Manager TM, string ThemeFile, string Pack)
        {
            string cache = @"%WinPaletterAppData%\ThemeResPack_Cache\" + string.Concat(TM.Info.ThemeName.Replace(" ", string.Empty).Split(Path.GetInvalidFileNameChars())) + @"\";
            var filesList = new Dictionary<string, string>();
            filesList.Clear();
            string x;
            string ZipEntry;

            if (System.IO.File.Exists(Pack))
                System.IO.File.Delete(Pack);
            using (var archive = ZipFile.Open(Pack, ZipArchiveMode.Create))
            {
                if (TM.LogonUI7.Enabled && TM.LogonUI7.Mode == Theme.Structures.LogonUI7.Sources.CustomImage || !TM.Windows81.NoLockScreen && TM.Windows81.LockScreenType == Theme.Structures.LogonUI7.Sources.CustomImage)
                {
                    x = TM.LogonUI7.ImagePath;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\Web", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = cache + "LogonUI" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.LogonUI7.ImagePath = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }
                }

                if (TM.Terminal.Enabled)
                {
                    x = TM.Terminal.DefaultProf.BackgroundImage;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\Web", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = cache + "winterminal_defprofile_backimg" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Terminal.DefaultProf.BackgroundImage = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Terminal.DefaultProf.Icon;
                    if (!string.IsNullOrWhiteSpace(x) && !(x.Length <= 1) && !x.StartsWith(PathsExt.Windows + @"\Web", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = cache + "winterminal_defprofile_icon" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Terminal.DefaultProf.Icon = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    foreach (var i in TM.Terminal.Profiles)
                    {
                        x = i.BackgroundImage;
                        if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\Web", StringComparison.OrdinalIgnoreCase))
                        {
                            ZipEntry = cache + "winterminal_profile(" + string.Concat(i.Name.Replace(" ", string.Empty).Split(Path.GetInvalidFileNameChars())) + ")_backimg" + Path.GetExtension(x);
                            if (System.IO.File.Exists(x))
                                i.BackgroundImage = ZipEntry;
                            filesList.Add(ZipEntry, x);
                        }

                        x = i.Icon;
                        if (!string.IsNullOrWhiteSpace(x) && !(x.Length <= 1) && !x.StartsWith(PathsExt.Windows + @"\Web", StringComparison.OrdinalIgnoreCase))
                        {
                            ZipEntry = cache + "winterminal_profile(" + string.Concat(i.Name.Replace(" ", string.Empty).Split(Path.GetInvalidFileNameChars())) + ")_icon" + Path.GetExtension(x);
                            if (System.IO.File.Exists(x))
                                i.Icon = ZipEntry;
                            filesList.Add(ZipEntry, x);
                        }
                    }
                }

                if (TM.TerminalPreview.Enabled)
                {
                    x = TM.TerminalPreview.DefaultProf.BackgroundImage;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\Web", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = cache + "winterminal_preview_defprofile_backimg" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.TerminalPreview.DefaultProf.BackgroundImage = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.TerminalPreview.DefaultProf.Icon;
                    if (!string.IsNullOrWhiteSpace(x) && !(x.Length <= 1) && !x.StartsWith(PathsExt.Windows + @"\Web", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = cache + "winterminal_preview_defprofile_icon" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.TerminalPreview.DefaultProf.Icon = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    foreach (var i in TM.TerminalPreview.Profiles)
                    {
                        x = i.BackgroundImage;
                        if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\Web", StringComparison.OrdinalIgnoreCase))
                        {
                            ZipEntry = cache + "winterminal_preview_profile(" + string.Concat(i.Name.Replace(" ", string.Empty).Split(Path.GetInvalidFileNameChars())) + ")_backimg" + Path.GetExtension(x);
                            if (System.IO.File.Exists(x))
                                i.BackgroundImage = ZipEntry;
                            filesList.Add(ZipEntry, x);
                        }

                        x = i.Icon;
                        if (!string.IsNullOrWhiteSpace(x) && !(x.Length <= 1) && !x.StartsWith(PathsExt.Windows + @"\Web", StringComparison.OrdinalIgnoreCase))
                        {
                            ZipEntry = cache + "winterminal_preview_profile(" + string.Concat(i.Name.Replace(" ", string.Empty).Split(Path.GetInvalidFileNameChars())) + ")_icon" + Path.GetExtension(x);
                            if (System.IO.File.Exists(x))
                                i.Icon = ZipEntry;
                            filesList.Add(ZipEntry, x);
                        }
                    }
                }

                if (TM.WallpaperTone_W11.Enabled)
                {
                    x = TM.WallpaperTone_W11.Image;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\Web", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = cache + "wt_w11" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.WallpaperTone_W11.Image = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }
                }

                if (TM.WallpaperTone_W10.Enabled)
                {
                    x = TM.WallpaperTone_W10.Image;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\Web", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = cache + "wt_w10" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.WallpaperTone_W10.Image = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }
                }

                if (TM.WallpaperTone_W81.Enabled)
                {
                    x = TM.WallpaperTone_W81.Image;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\Web", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = cache + "wt_w81" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.WallpaperTone_W81.Image = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }
                }

                if (TM.WallpaperTone_W7.Enabled)
                {
                    x = TM.WallpaperTone_W7.Image;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\Web", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = cache + "wt_w7" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.WallpaperTone_W7.Image = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }
                }

                if (TM.WallpaperTone_WVista.Enabled)
                {
                    x = TM.WallpaperTone_WVista.Image;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\Web", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = cache + "wt_wvista" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.WallpaperTone_WVista.Image = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }
                }

                if (TM.WallpaperTone_WXP.Enabled)
                {
                    x = TM.WallpaperTone_WXP.Image;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\Web", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = cache + "wt_wxp" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.WallpaperTone_WXP.Image = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }
                }

                if (TM.ScreenSaver.Enabled)
                {
                    x = TM.ScreenSaver.File;
                    if (!string.IsNullOrWhiteSpace(x))
                    {
                        ZipEntry = cache + "scrsvr" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.ScreenSaver.File = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }
                }

                if (TM.Cursor_Enabled)
                {
                    if (TM.Cursor_Arrow.UseFromFile && System.IO.File.Exists(TM.Cursor_Arrow.File))
                    {
                        // Cursor_Arrow
                        x = TM.Cursor_Arrow.File;
                        if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\Cursors", StringComparison.OrdinalIgnoreCase))
                        {
                            ZipEntry = cache + "Cursor_Arrow" + Path.GetExtension(x);
                            if (System.IO.File.Exists(x))
                                TM.Cursor_Arrow.File = ZipEntry;
                            filesList.Add(ZipEntry, x);
                        }

                        // Cursor_AppLoading
                        x = TM.Cursor_AppLoading.File;
                        if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\Cursors", StringComparison.OrdinalIgnoreCase))
                        {
                            ZipEntry = cache + "Cursor_AppLoading" + Path.GetExtension(x);
                            if (System.IO.File.Exists(x))
                                TM.Cursor_AppLoading.File = ZipEntry;
                            filesList.Add(ZipEntry, x);
                        }

                        // Cursor_Busy
                        x = TM.Cursor_Busy.File;
                        if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\Cursors", StringComparison.OrdinalIgnoreCase))
                        {
                            ZipEntry = cache + "Cursor_Busy" + Path.GetExtension(x);
                            if (System.IO.File.Exists(x))
                                TM.Cursor_Busy.File = ZipEntry;
                            filesList.Add(ZipEntry, x);
                        }

                        // Cursor_Help
                        x = TM.Cursor_Help.File;
                        if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\Cursors", StringComparison.OrdinalIgnoreCase))
                        {
                            ZipEntry = cache + "Cursor_Help" + Path.GetExtension(x);
                            if (System.IO.File.Exists(x))
                                TM.Cursor_Help.File = ZipEntry;
                            filesList.Add(ZipEntry, x);
                        }

                        // Cursor_Move
                        x = TM.Cursor_Move.File;
                        if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\Cursors", StringComparison.OrdinalIgnoreCase))
                        {
                            ZipEntry = cache + "Cursor_Move" + Path.GetExtension(x);
                            if (System.IO.File.Exists(x))
                                TM.Cursor_Move.File = ZipEntry;
                            filesList.Add(ZipEntry, x);
                        }

                        // Cursor_NS
                        x = TM.Cursor_NS.File;
                        if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\Cursors", StringComparison.OrdinalIgnoreCase))
                        {
                            ZipEntry = cache + "Cursor_NS" + Path.GetExtension(x);
                            if (System.IO.File.Exists(x))
                                TM.Cursor_NS.File = ZipEntry;
                            filesList.Add(ZipEntry, x);
                        }

                        // Cursor_EW
                        x = TM.Cursor_EW.File;
                        if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\Cursors", StringComparison.OrdinalIgnoreCase))
                        {
                            ZipEntry = cache + "Cursor_EW" + Path.GetExtension(x);
                            if (System.IO.File.Exists(x))
                                TM.Cursor_EW.File = ZipEntry;
                            filesList.Add(ZipEntry, x);
                        }

                        // Cursor_NESW
                        x = TM.Cursor_NESW.File;
                        if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\Cursors", StringComparison.OrdinalIgnoreCase))
                        {
                            ZipEntry = cache + "Cursor_NESW" + Path.GetExtension(x);
                            if (System.IO.File.Exists(x))
                                TM.Cursor_NESW.File = ZipEntry;
                            filesList.Add(ZipEntry, x);
                        }

                        // Cursor_NWSE
                        x = TM.Cursor_NWSE.File;
                        if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\Cursors", StringComparison.OrdinalIgnoreCase))
                        {
                            ZipEntry = cache + "Cursor_NWSE" + Path.GetExtension(x);
                            if (System.IO.File.Exists(x))
                                TM.Cursor_NWSE.File = ZipEntry;
                            filesList.Add(ZipEntry, x);
                        }

                        // Cursor_Up
                        x = TM.Cursor_Up.File;
                        if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\Cursors", StringComparison.OrdinalIgnoreCase))
                        {
                            ZipEntry = cache + "Cursor_Up" + Path.GetExtension(x);
                            if (System.IO.File.Exists(x))
                                TM.Cursor_Up.File = ZipEntry;
                            filesList.Add(ZipEntry, x);
                        }

                        // Cursor_Pen
                        x = TM.Cursor_Pen.File;
                        if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\Cursors", StringComparison.OrdinalIgnoreCase))
                        {
                            ZipEntry = cache + "Cursor_Pen" + Path.GetExtension(x);
                            if (System.IO.File.Exists(x))
                                TM.Cursor_Pen.File = ZipEntry;
                            filesList.Add(ZipEntry, x);
                        }

                        // Cursor_None
                        x = TM.Cursor_None.File;
                        if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\Cursors", StringComparison.OrdinalIgnoreCase))
                        {
                            ZipEntry = cache + "Cursor_None" + Path.GetExtension(x);
                            if (System.IO.File.Exists(x))
                                TM.Cursor_None.File = ZipEntry;
                            filesList.Add(ZipEntry, x);
                        }

                        // Cursor_Link
                        x = TM.Cursor_Link.File;
                        if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\Cursors", StringComparison.OrdinalIgnoreCase))
                        {
                            ZipEntry = cache + "Cursor_Link" + Path.GetExtension(x);
                            if (System.IO.File.Exists(x))
                                TM.Cursor_Link.File = ZipEntry;
                            filesList.Add(ZipEntry, x);
                        }

                        // Cursor_Pin
                        x = TM.Cursor_Pin.File;
                        if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\Cursors", StringComparison.OrdinalIgnoreCase))
                        {
                            ZipEntry = cache + "Cursor_Pin" + Path.GetExtension(x);
                            if (System.IO.File.Exists(x))
                                TM.Cursor_Pin.File = ZipEntry;
                            filesList.Add(ZipEntry, x);
                        }

                        // Cursor_Person
                        x = TM.Cursor_Person.File;
                        if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\Cursors", StringComparison.OrdinalIgnoreCase))
                        {
                            ZipEntry = cache + "Cursor_Person" + Path.GetExtension(x);
                            if (System.IO.File.Exists(x))
                                TM.Cursor_Person.File = ZipEntry;
                            filesList.Add(ZipEntry, x);
                        }

                        // Cursor_IBeam
                        x = TM.Cursor_IBeam.File;
                        if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\Cursors", StringComparison.OrdinalIgnoreCase))
                        {
                            ZipEntry = cache + "Cursor_IBeam" + Path.GetExtension(x);
                            if (System.IO.File.Exists(x))
                                TM.Cursor_IBeam.File = ZipEntry;
                            filesList.Add(ZipEntry, x);
                        }

                        // Cursor_Cross
                        x = TM.Cursor_Cross.File;
                        if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\Cursors", StringComparison.OrdinalIgnoreCase))
                        {
                            ZipEntry = cache + "Cursor_Cross" + Path.GetExtension(x);
                            if (System.IO.File.Exists(x))
                                TM.Cursor_Cross.File = ZipEntry;
                            filesList.Add(ZipEntry, x);
                        }
                    }
                }

                #region Sounds
                if (TM.Sounds.Enabled)
                {
                    x = TM.Sounds.Snd_Win_Default;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = cache + "Snd_Win_Default" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_Default = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_AppGPFault;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = cache + "Snd_Win_AppGPFault" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_AppGPFault = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_CCSelect;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = cache + "Snd_Win_CCSelect" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_CCSelect = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_ChangeTheme;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = cache + "Snd_Win_ChangeTheme" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_ChangeTheme = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_Close;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = cache + "Snd_Win_Close" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_Close = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_CriticalBatteryAlarm;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = cache + "Snd_Win_CriticalBatteryAlarm" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_CriticalBatteryAlarm = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_DeviceConnect;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = cache + "Snd_Win_DeviceConnect" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_DeviceConnect = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_DeviceDisconnect;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = cache + "Snd_Win_DeviceDisconnect" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_DeviceDisconnect = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_DeviceFail;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = cache + "Snd_Win_DeviceFail" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_DeviceFail = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_FaxBeep;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = cache + "Snd_Win_FaxBeep" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_FaxBeep = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_LowBatteryAlarm;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = cache + "Snd_Win_LowBatteryAlarm" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_LowBatteryAlarm = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_MailBeep;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = cache + "Snd_Win_MailBeep" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_MailBeep = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_Maximize;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = cache + "Snd_Win_Maximize" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_Maximize = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_MenuCommand;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = cache + "Snd_Win_MenuCommand" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_MenuCommand = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_MenuPopup;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = cache + "Snd_Win_MenuPopup" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_MenuPopup = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_MessageNudge;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = cache + "Snd_Win_MessageNudge" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_MessageNudge = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_Minimize;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = cache + "Snd_Win_Minimize" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_Minimize = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_Notification_Default;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = cache + "Snd_Win_Notification_Default" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_Notification_Default = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_Notification_IM;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = cache + "Snd_Win_Notification_IM" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_Notification_IM = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_Notification_Looping_Alarm;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = cache + "Snd_Win_Notification_Looping_Alarm" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_Notification_Looping_Alarm = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_Notification_Looping_Alarm10;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = cache + "Snd_Win_Notification_Looping_Alarm10" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_Notification_Looping_Alarm10 = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_Notification_Looping_Alarm2;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = cache + "Snd_Win_Notification_Looping_Alarm2" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_Notification_Looping_Alarm2 = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_Notification_Looping_Alarm3;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = cache + "Snd_Win_Notification_Looping_Alarm3" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_Notification_Looping_Alarm3 = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_Notification_Looping_Alarm4;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = cache + "Snd_Win_Notification_Looping_Alarm4" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_Notification_Looping_Alarm4 = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_Notification_Looping_Alarm5;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = cache + "Snd_Win_Notification_Looping_Alarm5" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_Notification_Looping_Alarm5 = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_Notification_Looping_Alarm6;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = cache + "Snd_Win_Notification_Looping_Alarm6" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_Notification_Looping_Alarm6 = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_Notification_Looping_Alarm7;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = cache + "Snd_Win_Notification_Looping_Alarm7" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_Notification_Looping_Alarm7 = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_Notification_Looping_Alarm8;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = cache + "Snd_Win_Notification_Looping_Alarm8" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_Notification_Looping_Alarm8 = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_Notification_Looping_Alarm9;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = cache + "Snd_Win_Notification_Looping_Alarm9" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_Notification_Looping_Alarm9 = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_Notification_Looping_Call;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = cache + "Snd_Win_Notification_Looping_Call" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_Notification_Looping_Call = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_Notification_Looping_Call10;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = cache + "Snd_Win_Notification_Looping_Call10" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_Notification_Looping_Call10 = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_Notification_Looping_Call2;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = cache + "Snd_Win_Notification_Looping_Call2" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_Notification_Looping_Call2 = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_Notification_Looping_Call3;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = cache + "Snd_Win_Notification_Looping_Call3" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_Notification_Looping_Call3 = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_Notification_Looping_Call4;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = cache + "Snd_Win_Notification_Looping_Call4" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_Notification_Looping_Call4 = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_Notification_Looping_Call5;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = cache + "Snd_Win_Notification_Looping_Call5" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_Notification_Looping_Call5 = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_Notification_Looping_Call6;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = cache + "Snd_Win_Notification_Looping_Call6" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_Notification_Looping_Call6 = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_Notification_Looping_Call7;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = cache + "Snd_Win_Notification_Looping_Call7" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_Notification_Looping_Call7 = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_Notification_Looping_Call8;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = cache + "Snd_Win_Notification_Looping_Call8" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_Notification_Looping_Call8 = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_Notification_Looping_Call9;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = cache + "Snd_Win_Notification_Looping_Call9" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_Notification_Looping_Call9 = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_Notification_Mail;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = cache + "Snd_Win_Notification_Mail" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_Notification_Mail = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_Notification_Proximity;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = cache + "Snd_Win_Notification_Proximity" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_Notification_Proximity = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_Notification_Reminder;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = cache + "Snd_Win_Notification_Reminder" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_Notification_Reminder = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_Notification_SMS;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = cache + "Snd_Win_Notification_SMS" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_Notification_SMS = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_Open;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = cache + "Snd_Win_Open" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_Open = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_PrintComplete;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = cache + "Snd_Win_PrintComplete" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_PrintComplete = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_ProximityConnection;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = cache + "Snd_Win_ProximityConnection" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_ProximityConnection = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_RestoreDown;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = cache + "Snd_Win_RestoreDown" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_RestoreDown = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_RestoreUp;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = cache + "Snd_Win_RestoreUp" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_RestoreUp = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_ShowBand;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = cache + "Snd_Win_ShowBand" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_ShowBand = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_SystemAsterisk;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = cache + "Snd_Win_SystemAsterisk" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_SystemAsterisk = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_SystemExclamation;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = cache + "Snd_Win_SystemExclamation" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_SystemExclamation = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_SystemExit;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = cache + "Snd_Win_SystemExit" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_SystemExit = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_SystemStart;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = cache + "Snd_Win_SystemStart" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_SystemStart = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Imageres_SystemStart;
                    if (!string.IsNullOrWhiteSpace(x))  // Don't include the condition: Not x.StartsWith(My.Directories.Windows & "\media", My.StringComparison.OrdinalIgnoreCase)
                    {
                        ZipEntry = cache + "Snd_Imageres_SystemStart" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Imageres_SystemStart = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_SystemHand;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = cache + "Snd_Win_SystemHand" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_SystemHand = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_SystemNotification;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = cache + "Snd_Win_SystemNotification" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_SystemNotification = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_SystemQuestion;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = cache + "Snd_Win_SystemQuestion" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_SystemQuestion = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_WindowsLogoff;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = cache + "Snd_Win_WindowsLogoff" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_WindowsLogoff = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_WindowsLogon;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = cache + "Snd_Win_WindowsLogon" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_WindowsLogon = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_WindowsUAC;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = cache + "Snd_Win_WindowsUAC" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_WindowsUAC = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_WindowsUnlock;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = cache + "Snd_Win_WindowsUnlock" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_WindowsUnlock = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Explorer_ActivatingDocument;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = cache + "Snd_Explorer_ActivatingDocument" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Explorer_ActivatingDocument = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Explorer_BlockedPopup;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = cache + "Snd_Explorer_BlockedPopup" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Explorer_BlockedPopup = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Explorer_EmptyRecycleBin;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = cache + "Snd_Explorer_EmptyRecycleBin" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Explorer_EmptyRecycleBin = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Explorer_FeedDiscovered;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = cache + "Snd_Explorer_FeedDiscovered" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Explorer_FeedDiscovered = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Explorer_MoveMenuItem;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = cache + "Snd_Explorer_MoveMenuItem" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Explorer_MoveMenuItem = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Explorer_Navigating;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = cache + "Snd_Explorer_Navigating" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Explorer_Navigating = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Explorer_SecurityBand;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = cache + "Snd_Explorer_SecurityBand" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Explorer_SecurityBand = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Explorer_SearchProviderDiscovered;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = cache + "Snd_Explorer_SearchProviderDiscovered" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Explorer_SearchProviderDiscovered = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Explorer_FaxError;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = cache + "Snd_Explorer_FaxError" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Explorer_FaxError = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Explorer_FaxLineRings;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = cache + "Snd_Explorer_FaxLineRings" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Explorer_FaxLineRings = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Explorer_FaxNew;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = cache + "Snd_Explorer_FaxNew" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Explorer_FaxNew = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Explorer_FaxSent;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = cache + "Snd_Explorer_FaxSent" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Explorer_FaxSent = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_NetMeeting_PersonJoins;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = cache + "Snd_NetMeeting_PersonJoins" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_NetMeeting_PersonJoins = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_NetMeeting_PersonLeaves;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = cache + "Snd_NetMeeting_PersonLeaves" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_NetMeeting_PersonLeaves = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_NetMeeting_ReceiveCall;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = cache + "Snd_NetMeeting_ReceiveCall" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_NetMeeting_ReceiveCall = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_NetMeeting_ReceiveRequestToJoin;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = cache + "Snd_NetMeeting_ReceiveRequestToJoin" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_NetMeeting_ReceiveRequestToJoin = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_SpeechRec_DisNumbersSound;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = cache + "Snd_SpeechRec_DisNumbersSound" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_SpeechRec_DisNumbersSound = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_SpeechRec_HubOffSound;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = cache + "Snd_SpeechRec_HubOffSound" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_SpeechRec_HubOffSound = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_SpeechRec_HubOnSound;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = cache + "Snd_SpeechRec_HubOnSound" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_SpeechRec_HubOnSound = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_SpeechRec_HubSleepSound;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = cache + "Snd_SpeechRec_HubSleepSound" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_SpeechRec_HubSleepSound = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_SpeechRec_MisrecoSound;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = cache + "Snd_SpeechRec_MisrecoSound" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_SpeechRec_MisrecoSound = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_SpeechRec_PanelSound;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\media", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = cache + "Snd_SpeechRec_PanelSound" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_SpeechRec_PanelSound = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }
                }
                #endregion

                if (TM.Wallpaper.Enabled && TM.Wallpaper.WallpaperType == Wallpaper.WallpaperTypes.Picture)
                {
                    x = TM.Wallpaper.ImageFile;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\Web", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = cache + "wallpaper_file" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Wallpaper.ImageFile = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }
                }

                foreach (var _file in filesList)
                {
                    if (System.IO.File.Exists(_file.Value))
                        archive.CreateEntryFromFile(_file.Value, _file.Key.Split('\\').Last(), CompressionLevel.Optimal);
                }

                if (TM.WindowsXP.Theme == WindowsXP.Themes.Custom)
                {
                    x = TM.WindowsXP.ThemeFile;
                    if (!string.IsNullOrWhiteSpace(x) && System.IO.File.Exists(x) && !x.StartsWith(PathsExt.Windows + @"\Resources\Themes\Luna", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipEntry = cache + @"WXP_VS\" + Path.GetFileName(x);
                        if (System.IO.File.Exists(x))
                            TM.WindowsXP.ThemeFile = ZipEntry;
                        string DirName = new FileInfo(x).Directory.FullName;
                        foreach (string file in Directory.EnumerateFiles(DirName, "*.*", SearchOption.AllDirectories))
                        {
                            if (System.IO.File.Exists(file))
                                archive.CreateEntryFromFile(file, "WXP_VS" + file.Replace(DirName, string.Empty), CompressionLevel.Optimal);
                        }
                    }
                }

                if (TM.Wallpaper.Enabled && TM.Wallpaper.WallpaperType == Wallpaper.WallpaperTypes.SlideShow)
                {
                    if (TM.Wallpaper.SlideShow_Folder_or_ImagesList)
                    {
                        x = TM.Wallpaper.Wallpaper_Slideshow_ImagesRootPath;
                        if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\Web", StringComparison.OrdinalIgnoreCase))
                        {
                            TM.Wallpaper.Wallpaper_Slideshow_ImagesRootPath = cache + "wallpapers_slideshow";

                            foreach (var image in Directory.EnumerateFiles(x, "*.*", SearchOption.TopDirectoryOnly).Where(s => s.EndsWith(".bmp") || s.EndsWith(".jpg") || s.EndsWith(".png") || s.EndsWith(".gif")))


                            {


                                if (System.IO.File.Exists(image))
                                    archive.CreateEntryFromFile(image, @"wallpapers_slideshow\" + new FileInfo(image).Name, CompressionLevel.Optimal);

                            }

                        }
                    }

                    else
                    {
                        string[] arr = TM.Wallpaper.Wallpaper_Slideshow_Images.ToArray();
                        if (arr.Count() > 0)
                        {
                            if (!arr[0].StartsWith(PathsExt.Windows + @"\Web", StringComparison.OrdinalIgnoreCase))
                            {
                                TM.Wallpaper.Wallpaper_Slideshow_ImagesRootPath = cache + "WallpapersList";
                                TM.Wallpaper.Wallpaper_Slideshow_Images = new string[] { };
                                for (int x0 = 0, loopTo = arr.Count() - 1; x0 <= loopTo; x0++)
                                {
                                    x = arr[x0];
                                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(PathsExt.Windows + @"\Web", StringComparison.OrdinalIgnoreCase))
                                    {
                                        ZipEntry = cache + @"WallpapersList\wallpaperlist_" + x0 + "_file" + Path.GetExtension(x);
                                        if (System.IO.File.Exists(x))
                                        {
                                            TM.Wallpaper.Wallpaper_Slideshow_Images = TM.Wallpaper.Wallpaper_Slideshow_Images.Append(ZipEntry).ToArray();
                                            archive.CreateEntryFromFile(x, @"WallpapersList\wallpaperlist_" + x0 + "_file" + Path.GetExtension(x), CompressionLevel.Optimal);
                                        }
                                    }
                                }
                            }
                        }
                    }

                }

                System.IO.File.WriteAllText(ThemeFile, TM.ToString());
            }

        }
    }
}