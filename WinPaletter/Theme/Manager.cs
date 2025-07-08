using ImageProcessor.Processors;
using Microsoft.VisualBasic.FileIO;
using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using WinPaletter.Theme.Structures;

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
        /// <param name="Source">Source from which WinPaletter will get theme data. It can be from registry, File or empty.</param>
        /// <param name="File">If selected source is File, this will specify WinPaletter theme File</param>
        /// <param name="ignoreExtractionThemePack">This will ignore theme resources pack extraction, useful for previewing or getting theme data quickly without data extraction.</param>
        /// <param name="ignoreErrors">This will ignore any errors that may occur during theme loading.</param>
        public Manager(Source Source, string File = "", bool ignoreExtractionThemePack = false, bool ignoreErrors = false)
        {
            switch (Source)
            {
                case Source.Registry:
                    {
                        // Imperonate the current user to get the theme data from the registry
                        using (User.Identity.Impersonate())
                        {
                            // Get the theme data from the registry and use @default to help WinPaletter know the default values
                            using (Manager @default = Theme.Default.Get(Program.WindowStyle))
                            {
                                Program.Log?.Write(Serilog.Events.LogEventLevel.Information, "A request to load all Windows aspects into WinPaletter theme is made.");
                                Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"This request is targeting user: {User.Domain}\\{User.Name}");

                                // Clear the exception list that has theme load exceptions
                                Exceptions.ThemeLoad.Clear();

                                // Start loading the theme data from the registry
                                Info.Load();
                                Windows12.Load("12", @default.Windows12);
                                Windows11.Load("11", @default.Windows11);
                                Windows10.Load("10", @default.Windows10);
                                Windows81.Load(@default.Windows81);
                                Windows8.Load(@default.Windows8);
                                Windows7.Load(@default.Windows7);
                                WindowsVista.Load(@default.WindowsVista);
                                WindowsXP.Load(@default.WindowsXP);
                                VisualStyles_12.Load("12", @default.VisualStyles_12);
                                VisualStyles_11.Load("11", @default.VisualStyles_11);
                                VisualStyles_10.Load("10", @default.VisualStyles_10);
                                VisualStyles_81.Load("8.1", @default.VisualStyles_81);
                                VisualStyles_8.Load("8", @default.VisualStyles_8);
                                VisualStyles_7.Load("7", @default.VisualStyles_7);
                                VisualStyles_Vista.Load("Vista", @default.VisualStyles_Vista);
                                VisualStyles_XP.Load("XP", @default.VisualStyles_Vista);
                                LogonUI12.Load("12", @default.LogonUI12);
                                LogonUI11.Load("11", @default.LogonUI11);
                                LogonUI10.Load("10", @default.LogonUI10);
                                LogonUI81.Load(@default.LogonUI81);
                                //LogonUI8.Load(@default.LogonUI8);
                                LogonUI7.Load(@default.LogonUI7);
                                LogonUIXP.Load(@default.LogonUIXP);
                                Win32.Load();
                                Accessibility.Load(@default.Accessibility);
                                Cursors.Load(@default.Cursors);
                                MetricsFonts.Load(@default.MetricsFonts);
                                WindowsEffects.Load(@default.WindowsEffects);
                                AltTab.Load(@default.AltTab);
                                ScreenSaver.Load(@default.ScreenSaver);
                                Sounds.Load(@default.Sounds);
                                AppTheme.Load(@default.AppTheme);
                                Icons.Load(@default.Icons);
                                WallpaperTone_W12.Load("Win12");
                                WallpaperTone_W11.Load("Win11");
                                WallpaperTone_W10.Load("Win10");
                                WallpaperTone_W81.Load("Win8.1");
                                WallpaperTone_W8.Load("Win8");
                                WallpaperTone_W7.Load("Win7");
                                WallpaperTone_WVista.Load("WinVista");
                                WallpaperTone_WXP.Load("WinXP");
                                Wallpaper.Load(@default.Wallpaper);

                                Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Targeting Console: Command Prompt");
                                CommandPrompt.Load(string.Empty, "Terminal_CMD_Enabled", @default.CommandPrompt);

                                Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Targeting Console: PowerShell x86");
                                if (Directory.Exists(SysPaths.PS86_dir))
                                {
                                    try { Registry.CurrentUser.CreateSubKey($@"Console\{SysPaths.PS86_reg}", true).Close(); }
                                    catch { }
                                    PowerShellx86.Load(SysPaths.PS86_reg, "Terminal_PS_32_Enabled", @default.PowerShellx86);
                                }
                                else { PowerShellx86 = @default.PowerShellx86; }

                                Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Targeting Console: PowerShell x64");
                                if (Directory.Exists(SysPaths.PS64_dir))
                                {
                                    try { Registry.CurrentUser.CreateSubKey($@"Console\{SysPaths.PS64_reg}", true).Close(); }
                                    catch { }
                                    PowerShellx64.Load(SysPaths.PS64_reg, "Terminal_PS_64_Enabled", @default.PowerShellx64);
                                }
                                else { PowerShellx64 = @default.PowerShellx64; }

                                #region Windows Terminal
                                Terminal.Enabled = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Terminals", "Terminal_Stable_Enabled", 0)) == 1;
                                TerminalPreview.Enabled = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Terminals", "Terminal_Preview_Enabled", 0)) == 1;

                                // Only load Terminal and TerminalPreview if the OS is Windows 10 or higher
                                if (OS.W12 || OS.W11 || OS.W10)
                                {
                                    string TerDir;
                                    string TerPreDir;

                                    // Check if the user has enabled the path redirection feature or not
                                    if (!Program.Settings.WindowsTerminals.Path_Deflection)
                                    {
                                        TerDir = SysPaths.TerminalJSON;
                                        TerPreDir = SysPaths.TerminalPreviewJSON;
                                    }
                                    else
                                    {
                                        if (System.IO.File.Exists(Program.Settings.WindowsTerminals.Terminal_Stable_Path))
                                        { TerDir = Program.Settings.WindowsTerminals.Terminal_Stable_Path; }
                                        else { TerDir = SysPaths.TerminalJSON; }

                                        if (System.IO.File.Exists(Program.Settings.WindowsTerminals.Terminal_Preview_Path))
                                        { TerPreDir = Program.Settings.WindowsTerminals.Terminal_Preview_Path; }
                                        else { TerPreDir = SysPaths.TerminalPreviewJSON; }
                                    }

                                    Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Loading Windows Terminal colors and settings from {TerDir}");
                                    if (System.IO.File.Exists(TerDir)) { Terminal = new(TerDir, WinTerminal.Mode.JSONFile); }
                                    else { Terminal = new(string.Empty, WinTerminal.Mode.Empty); }

                                    Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Loading Windows Terminal Preview colors and settings from {TerPreDir}");
                                    if (System.IO.File.Exists(TerPreDir)) { TerminalPreview = new(TerPreDir, WinTerminal.Mode.JSONFile, WinTerminal.Version.Preview); }
                                    else { TerminalPreview = new(string.Empty, WinTerminal.Mode.Empty, WinTerminal.Version.Preview); }
                                }
                                else // If the OS is not Windows 10 or higher, then set Terminal and TerminalPreview to empty (Default values)
                                {
                                    Program.Log?.Write(Serilog.Events.LogEventLevel.Error, $"Couldn't find Windows Terminals, loading default ones");

                                    Terminal = new(string.Empty, WinTerminal.Mode.Empty);
                                    TerminalPreview = new(string.Empty, WinTerminal.Mode.Empty, WinTerminal.Version.Preview);
                                }
                                #endregion

                                // After loading all the theme data, check if the theme loading process has any exceptions (errors) and display them
                                if (!ignoreErrors && Exceptions.ThemeLoad.Count > 0)
                                {
                                    Forms.Saving_ex_list.ex_List = Exceptions.ThemeLoad;
                                    Forms.Saving_ex_list.ApplyMode = false;
                                    Forms.Saving_ex_list.ShowDialog();
                                }
                            }
                        }

                        break;
                    }

                case Source.File:
                    {
                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Loading WinPaletter theme `{File}` by converting JSON string into a valid theme class instance.");

                        // Clear the exception list that has theme load exceptions
                        Exceptions.ThemeLoad.Clear();

                        // Check if the theme File exists
                        if (!System.IO.File.Exists(File)) return;

                        // Decompress the theme File content_list
                        List<string> content_list = Decompress(File) as List<string>;

                        // Check if the theme File content_list is null or empty
                        if (content_list is null || content_list.Count == 0) return;

                        // Use @default to help WinPaletter know the default values
                        using (Manager @default = Default.Get())
                        {
                            try
                            {
                                // Copy values from default theme instance to current instance's fields, to avoid empty values after upgrading/downgrading WinPaletter
                                SetDefaultValues(@default);

                                // Extract theme name from the theme file quickly, to be used in creating theme pack resources cache
                                Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Extracting theme name from the theme file quickly, to be used in creating theme pack resources cache.");
                                SetThemeName(content_list);

                                string content = string.Join("\r\n", content_list);

                                // Extract theme resources pack from the theme File
                                if (!ignoreExtractionThemePack) ExtractThemeResourcesPack(File);

                                if (IsValidJson(content))
                                {
                                    // Replace %WinPaletterAppData% variable with a valid AppData folder path
                                    ReplaceWPAppData(ref content_list);

                                    // reset content as content_list has been modified
                                    content = string.Join("\r\n", content_list);

                                    // Parse the decompressed content_list as JSON
                                    JObject json = JObject.Parse(content);
                                    MergeDefaultsInCurrent(ref json, JObject.Parse(@default.ToString(true)));

                                    // Fixing new Cursors and Windows Terminal structures from older WPTH files
                                    ExtendCursorsComptability(ref json);
                                    ExtendTerminalComptability(ref json);

                                    // Set the value of the current instance's field from theme File JSON data
                                    SetThemeValues(json);
                                }
                                else if (GetEdition(File) == Editions.OldFormat)
                                {
                                    Program.Log?.Write(Serilog.Events.LogEventLevel.Error, $"The used wpth file has the old format (obsolete.)");

                                    // Display a message box for old format themes
                                    MsgBox(Program.Lang.Strings.Converter.Detect_Old_OnLoading0, MessageBoxButtons.OK, MessageBoxIcon.Error, Program.Lang.Strings.Converter.Detect_Old_OnLoadingTip);
                                    return;
                                }
                                else
                                {
                                    Program.Log?.Write(Serilog.Events.LogEventLevel.Error, $"The used wpth file is invalid.");

                                    // Display a message box for invalid JSON
                                    MsgBox(Program.Lang.Strings.Converter.Error_Phrasing, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }

                                // Display exception information if any
                                if (!ignoreErrors && Exceptions.ThemeLoad.Count > 0)
                                {
                                    Program.Log?.Write(Serilog.Events.LogEventLevel.Error, $"Errors happened in loading theme file and a list of errors dialog will appear.");

                                    Forms.Saving_ex_list.ex_List = Exceptions.ThemeLoad;
                                    Forms.Saving_ex_list.ApplyMode = false;
                                    Forms.Saving_ex_list.ShowDialog();
                                }
                            }
                            catch (Exception ex)
                            {
                                // Handle any unexpected exceptions
                                Forms.BugReport.ThrowError(ex);
                            }
                        }

                        break;
                    }
            }
        }

        /// <summary>
        /// Set the value of the current instance's field from default theme instance
        /// </summary>
        /// <param name="default"></param>
        private void SetDefaultValues(Manager @default)
        {
            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Copying values from default theme instance to current instance's fields, to avoid empty values after upgrading/downgrading WinPaletter and use a different wpth version file.");

            // Copy values from default theme instance to current instance's fields, to avoid empty values after upgrading/downgrading WinPaletter
            foreach (FieldInfo field in GetType().GetFields(bindingFlags))
            {
                Type fieldType = field.FieldType;
                try
                {
                    // Set the value of the current instance's field from default theme instance
                    field.SetValue(this, field.GetValue(@default));
                }
                catch (Exception ex)
                {
                    // Handle exceptions and add them to the error list
                    Exceptions.ThemeLoad.Add(new Tuple<string, Exception>(ex.Message, ex));
                }
            }
        }

        /// <summary>
        /// Set theme name quickly, to be used in creating theme pack resources cache
        /// </summary>
        /// <param name="content"></param>
        private void SetThemeName(List<string> content)
        {
            foreach (string line in content)
            {
                if (line.Trim().ToLower().StartsWith("\"themename\":", StringComparison.OrdinalIgnoreCase))
                {
                    // Set the theme name in the Info object
                    Info.ThemeName = line.Split(':')[1].ToString().Replace("\"", string.Empty).Replace(",", string.Empty).Trim();
                    break;
                }
            }
        }

        /// <summary>
        /// Extract theme resources pack from the theme File
        /// </summary>
        /// <param name="ThemeFile"></param>
        private void ExtractThemeResourcesPack(string ThemeFile)
        {
            // Prepare variables for theme resources pack extraction
            string packPath = $"{new FileInfo(ThemeFile).DirectoryName}\\{Path.GetFileNameWithoutExtension(ThemeFile)}.wptp";
            bool packIsValid = System.IO.File.Exists(packPath) && new FileInfo(packPath).Length > 0L && GetEdition(ThemeFile) == Editions.JSON;
            string cache = $"{SysPaths.ThemeResPackCache}\\{string.Concat(Info.ThemeName.Replace(" ", string.Empty).Split(Path.GetInvalidFileNameChars()))}";

            // Extract theme resources pack
            try
            {
                if (packIsValid)
                {
                    Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"The theme resources pack is valid. Extracting it.");

                    if (!Directory.Exists(cache)) Directory.CreateDirectory(cache);

                    using (FileStream stream = new(packPath, FileMode.Open, FileAccess.Read))
                    using (ZipArchive archive = new(stream, ZipArchiveMode.Read))
                    {
                        foreach (ZipArchiveEntry entry in archive.Entries)
                        {
                            // Create directories if necessary and extract entries
                            if (entry.FullName.Contains("\\"))
                            {
                                string dest = Path.Combine(cache, entry.FullName);
                                string destDir = dest.Replace($"\\{dest.Split('\\').Last()}", string.Empty);

                                if (!Directory.Exists(destDir)) Directory.CreateDirectory(destDir);
                            }

                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Extracting `{entry.Name}` as `{Path.Combine(cache, entry.FullName)}`");

                            entry.ExtractToFile(Path.Combine(cache, entry.FullName), true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                packIsValid = false;
                Forms.BugReport.ThrowError(ex);
            }
        }

        /// <summary>
        /// Replace %WinPaletterAppData% variable with a valid AppData folder path inside theme data
        /// </summary>
        /// <param name="list"></param>
        private void ReplaceWPAppData(ref List<string> list)
        {
            // Replace %WinPaletterAppData% variable with a valid AppData folder path
            for (int x = 0; x < list.Count; x++)
            {
                if (list[x].Contains(":"))
                {
                    string[] arr = list[x].Split(':');
                    if (arr.Length == 2 && (arr[1] ?? string.Empty).ToLower().Contains("%WinPaletterAppData%".ToLower()))
                    {

                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Replacing entry `{list[x]}` with `%WinPaletterAppData%` variable with a valid AppData folder path.");

                        list[x] = $"{arr[0]}:{arr[1].Replace("%WinPaletterAppData%", SysPaths.appData.Replace("\\", "\\\\"))}";
                    }
                }
            }
        }

        /// <summary>
        /// Merge default theme data into current theme data to make a new WinPaletter with new features can load a WinPaletter theme made by an old WinPaletter
        /// </summary>
        /// <param name="current"></param>
        /// <param name="defaults"></param>
        private void MergeDefaultsInCurrent(ref JObject current, JObject defaults)
        {
            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Mergeing default theme data into current theme data to make a new WinPaletter with new features can load a WinPaletter theme made by an old WinPaletter");

            foreach (KeyValuePair<string, JToken?> item in defaults)
            {
                if (current[item.Key] is null && defaults[item.Key] is not null) current[item.Key] = defaults[item.Key];

                if (item.Value is not JValue)
                {
                    foreach (KeyValuePair<string, JToken> prop in item.Value as JObject)
                    {
                        if (current[item.Key][prop.Key] is null && defaults[item.Key] is not null && defaults[item.Key][prop.Key] is not null)
                        {
                            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Merging default property `{prop.Key}` of `{item.Key}` into current theme data.");
                            current[item.Key][prop.Key] = defaults[item.Key][prop.Key];
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Convert format of older WPTH cursors format to make current WinPalette version can handle cursors.
        /// </summary>
        /// <param name="json"></param>
        private void ExtendCursorsComptability(ref JObject json)
        {
            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Converting format of older WPTH cursors format to make current WinPalette version can handle cursors.");

            Structures.Cursors cursors = new();
            bool cursorsModificationDone = false;

            if (json["Cursor_Enabled"] is not null)
            {
                cursorsModificationDone = true;
                cursors.Enabled = json["Cursor_Enabled"].Value<bool>();
                json.Remove("Cursor_Enabled");
            }

            if (json["Cursor_Shadow"] is not null)
            {
                cursorsModificationDone = true;
                cursors.Shadow = json["Cursor_Shadow"].Value<bool>();
                json.Remove("Cursor_Shadow");
            }

            if (json["Cursor_Sonar"] is not null)
            {
                cursorsModificationDone = true;
                cursors.Sonar = json["Cursor_Sonar"].Value<bool>();
                json.Remove("Cursor_Sonar");
            }

            if (json["Cursor_Trails"] is not null)
            {
                cursorsModificationDone = true;
                cursors.Trails = json["Cursor_Trails"].Value<int>();
                json.Remove("Cursor_Trails");
            }

            if (json["Cursor_Arrow"] is not null)
            {
                cursorsModificationDone = true;
                cursors.Cursor_Arrow = json["Cursor_Arrow"].ToObject<Structures.Cursor>();
                json.Remove("Cursor_Arrow");
            }

            if (json["Cursor_AppLoading"] is not null)
            {
                cursorsModificationDone = true;
                cursors.Cursor_AppLoading = json["Cursor_AppLoading"].ToObject<Structures.Cursor>();
                json.Remove("Cursor_AppLoading");
            }

            if (json["Cursor_Busy"] is not null)
            {
                cursorsModificationDone = true;
                cursors.Cursor_Busy = json["Cursor_Busy"].ToObject<Structures.Cursor>();
                json.Remove("Cursor_Busy");
            }

            if (json["Cursor_Help"] is not null)
            {
                cursorsModificationDone = true;
                cursors.Cursor_Help = json["Cursor_Help"].ToObject<Structures.Cursor>();
                json.Remove("Cursor_Help");
            }

            if (json["Cursor_Move"] is not null)
            {
                cursorsModificationDone = true;
                cursors.Cursor_Move = json["Cursor_Move"].ToObject<Structures.Cursor>();
                json.Remove("Cursor_Move");
            }

            if (json["Cursor_NS"] is not null)
            {
                cursorsModificationDone = true;
                cursors.Cursor_NS = json["Cursor_NS"].ToObject<Structures.Cursor>();
                json.Remove("Cursor_NS");
            }

            if (json["Cursor_EW"] is not null)
            {
                cursorsModificationDone = true;
                cursors.Cursor_EW = json["Cursor_EW"].ToObject<Structures.Cursor>();
                json.Remove("Cursor_EW");
            }

            if (json["Cursor_NESW"] is not null)
            {
                cursorsModificationDone = true;
                cursors.Cursor_NESW = json["Cursor_NESW"].ToObject<Structures.Cursor>();
                json.Remove("Cursor_NESW");
            }

            if (json["Cursor_NWSE"] is not null)
            {
                cursorsModificationDone = true;
                cursors.Cursor_NWSE = json["Cursor_NWSE"].ToObject<Structures.Cursor>();
                json.Remove("Cursor_NWSE");
            }

            if (json["Cursor_Up"] is not null)
            {
                cursorsModificationDone = true;
                cursors.Cursor_Up = json["Cursor_Up"].ToObject<Structures.Cursor>();
                json.Remove("Cursor_Up");
            }

            if (json["Cursor_Pen"] is not null)
            {
                cursorsModificationDone = true;
                cursors.Cursor_Pen = json["Cursor_Pen"].ToObject<Structures.Cursor>();
                json.Remove("Cursor_Pen");
            }

            if (json["Cursor_None"] is not null)
            {
                cursorsModificationDone = true;
                cursors.Cursor_None = json["Cursor_None"].ToObject<Structures.Cursor>();
                json.Remove("Cursor_None");
            }

            if (json["Cursor_Link"] is not null)
            {
                cursorsModificationDone = true;
                cursors.Cursor_Link = json["Cursor_Link"].ToObject<Structures.Cursor>();
                json.Remove("Cursor_Link");
            }

            if (json["Cursor_Pin"] is not null)
            {
                cursorsModificationDone = true;
                cursors.Cursor_Pin = json["Cursor_Pin"].ToObject<Structures.Cursor>();
                json.Remove("Cursor_Pin");
            }

            if (json["Cursor_Person"] is not null)
            {
                cursorsModificationDone = true;
                cursors.Cursor_Person = json["Cursor_Person"].ToObject<Structures.Cursor>();
                json.Remove("Cursor_Person");
            }

            if (json["Cursor_IBeam"] is not null)
            {
                cursorsModificationDone = true;
                cursors.Cursor_IBeam = json["Cursor_IBeam"].ToObject<Structures.Cursor>();
                json.Remove("Cursor_IBeam");
            }

            if (json["Cursor_Cross"] is not null)
            {
                cursorsModificationDone = true;
                cursors.Cursor_Cross = json["Cursor_Cross"].ToObject<Structures.Cursor>();
                json.Remove("Cursor_Cross");
            }

            if (cursorsModificationDone)
            {
                json["Cursors"] = JObject.FromObject(cursors);
            }
        }

        /// <summary>
        /// Convert format of older WPTH Windows Terminal format to make current WinPalette version can handle Windows Terminals.
        /// </summary>
        /// <param name="json"></param>
        private void ExtendTerminalComptability(ref JObject json)
        {
            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Converting format of older WPTH Windows Terminal format to make current WinPalette version can handle Windows Terminals.");

            string[] editons = ["Terminal", "TerminalPreview"];

            foreach (string edition in editons)
            {
                if (json[edition] is not null && (json[edition]["DefaultProf"] is not null || json[edition]["Colors"] is not null)) // Terminal is using older WPTH format
                {
                    List<WinTerminal.Types.Scheme> Schemes = json[edition]["Colors"].ToObject<List<WinTerminal.Types.Scheme>>();
                    ((JObject)json[edition]).Remove("Colors");
                    json[edition]["schemes"] = JArray.FromObject(Schemes);

                    WinTerminal.Types.Profiles Profiles = new()
                    {
                        Defaults = json[edition]["DefaultProf"].ToObject<WinTerminal.Types.Profile>(),
                        List = json[edition]["Profiles"].ToObject<List<WinTerminal.Types.Profile>>()
                    };

                    ((JObject)json[edition]).Remove("DefaultProf");
                    ((JObject)json[edition]).Remove("Profiles");

                    json[edition]["profiles"] = JObject.FromObject(Profiles);
                }
            }
        }

        /// <summary>
        /// Set the value of the current instance's field from theme File JSON data
        /// </summary>
        /// <param name="json"></param>
        private void SetThemeValues(JObject json)
        {
            // Set values from JSON to the current instance's fields
            foreach (FieldInfo field in GetType().GetFields(bindingFlags))
            {
                try
                {
                    Type fieldType = field.FieldType;
                    if (json[field.Name] is not null)
                    {
                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Setting field `{field.Name}` with value `{json[field.Name]}` of type `{fieldType.Name}` from theme File JSON data.");
                        field.SetValue(this, json[field.Name].ToObject(fieldType));
                    }
                }
                catch (Exception ex)
                {
                    Program.Log?.Write(Serilog.Events.LogEventLevel.Error, $"Error setting field `{field.Name}`: {ex.Message}");
                    // Handle exceptions and add them to the error list
                    Exceptions.ThemeLoad.Add(new Tuple<string, Exception>(ex.Message, ex));
                }
            }
        }
    }
}