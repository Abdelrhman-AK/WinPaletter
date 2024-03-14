using Microsoft.VisualBasic.CompilerServices;
using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace WinPaletter
{
    public class Settings
    {
        private BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public;

        public class Structures
        {
            #region paths
            public static readonly string REG = @"HKEY_CURRENT_USER\Software\WinPaletter\Settings";
            public static readonly string REG_General = $@"{REG}\General";
            public static readonly string REG_General_MainForm = $@"{REG_General}\MainForm";
            public static readonly string REG_Updates = $@"{REG}\Updates";
            public static readonly string REG_FileTypeManagement = $@"{REG}\FileTypeManagement";
            public static readonly string REG_ThemeApplyingBehavior = $@"{REG}\ThemeApplyingBehavior";
            public static readonly string REG_Appearance = $@"{REG}\Appearance\NewGen";
            public static readonly string REG_Language = $@"{REG}\Language";
            public static readonly string REG_EP = $@"{REG}\ExplorerPatcher";
            public static readonly string REG_ThemeLog = $@"{REG}\ThemeLog";
            public static readonly string REG_WindowsTerminals = $@"{REG}\WindowsTerminals";
            public static readonly string REG_Store = $@"{REG}\Store";
            public static readonly string REG_NerdStats = $@"{REG}\NerdStats";
            public static readonly string REG_UsersServices = $@"{REG}\UsersServices";
            public static readonly string REG_Miscellaneous = $@"{REG}\Miscellaneous";
            public static readonly string REG_Backup = $@"{REG}\Backup";
            public static readonly string REG_AspectsControl = $@"{REG}\AspectsControl";
            #endregion

            public struct General
            {
                public bool LicenseAccepted;
                public object MainFormWidth;
                public object MainFormHeight;
                public object MainFormStatus;
                public string[] WhatsNewRecord;

                public void Load()
                {
                    LicenseAccepted = Conversions.ToBoolean(GetReg(REG_General, "LicenseAccepted", false));
                    WhatsNewRecord = (string[])GetReg(REG_General, "WhatsNewRecord", new[] { string.Empty });
                    MainFormWidth = GetReg(REG_General_MainForm, "MainFormWidth", 1110);
                    MainFormHeight = GetReg(REG_General_MainForm, "MainFormHeight", 725);
                    MainFormStatus = GetReg(REG_General_MainForm, "MainFormStatus", FormWindowState.Normal);
                }

                public void Save()
                {
                    EditReg(REG_General, "LicenseAccepted", LicenseAccepted, RegistryValueKind.DWord);
                    EditReg(REG_General, "WhatsNewRecord", WhatsNewRecord, RegistryValueKind.MultiString);
                    EditReg(REG_General_MainForm, "MainFormWidth", MainFormWidth, RegistryValueKind.DWord);
                    EditReg(REG_General_MainForm, "MainFormHeight", MainFormHeight, RegistryValueKind.DWord);
                    EditReg(REG_General_MainForm, "MainFormStatus", MainFormStatus, RegistryValueKind.DWord);
                }

            }

            public struct Updates
            {
                public bool AutoCheck;
                public Channels Channel;

                public enum Channels
                {
                    Stable,
                    Beta
                }

                public void Load()
                {
                    AutoCheck = Conversions.ToBoolean(GetReg(REG_Updates, "AutoCheck", true));
                    Channel = (Channels)GetReg(REG_Updates, "Channel", Channels.Stable) == Channels.Stable ? Channels.Stable : Channels.Beta;
                }

                public void Save()
                {
                    EditReg(REG_Updates, "AutoCheck", AutoCheck, RegistryValueKind.DWord);
                    EditReg(REG_Updates, "Channel", Channel == Channels.Stable ? 0 : 1);
                }

            }

            public struct FileTypeMgr
            {
                public bool AutoAddExt;
                public bool OpeningPreviewInApp_or_AppliesIt;
                public bool CompressThemeFile;

                public void Load()
                {
                    AutoAddExt = Conversions.ToBoolean(GetReg(REG_FileTypeManagement, "AutoAddExt", true));
                    OpeningPreviewInApp_or_AppliesIt = Conversions.ToBoolean(GetReg(REG_FileTypeManagement, "OpeningPreviewInApp_or_AppliesIt", true));
                    CompressThemeFile = Conversions.ToBoolean(GetReg(REG_FileTypeManagement, "CompressThemeFile", true));
                }

                public void Save()
                {
                    EditReg(REG_FileTypeManagement, "AutoAddExt", AutoAddExt, RegistryValueKind.DWord);
                    EditReg(REG_FileTypeManagement, "OpeningPreviewInApp_or_AppliesIt", OpeningPreviewInApp_or_AppliesIt, RegistryValueKind.DWord);
                    EditReg(REG_FileTypeManagement, "CompressThemeFile", CompressThemeFile, RegistryValueKind.DWord);
                }

            }

            public struct ThemeApplyingBehavior
            {
                public bool AutoRestartExplorer;
                public bool ShowSaveConfirmation;
                public OverwriteOptions ClassicColors_HKU_DEFAULT_Prefs;
                public OverwriteOptions ClassicColors_HKLM_Prefs;
                public bool UPM_HKU_DEFAULT;
                public OverwriteOptions Metrics_HKU_DEFAULT_Prefs;
                public bool ResetCursorsToAero;
                public OverwriteOptions Cursors_HKU_DEFAULT_Prefs;
                public OverwriteOptions CMD_HKU_DEFAULT_Prefs;
                public OverwriteOptions PS86_HKU_DEFAULT_Prefs;
                public OverwriteOptions PS64_HKU_DEFAULT_Prefs;
                public OverwriteOptions Desktop_HKU_DEFAULT;
                public bool CMD_OverrideUserPreferences;
                public bool SFC_on_restoring_StartupSound;
                public bool Ignore_PE_Modify_Alert;
                public bool PE_ModifyByDefault;
                public bool Show_WinEffects_Alert;

                public enum OverwriteOptions
                {
                    DontChange,
                    Overwrite,
                    RestoreDefaults,
                    Erase
                }

                public void Load()
                {
                    AutoRestartExplorer = Conversions.ToBoolean(GetReg(REG_ThemeApplyingBehavior, "AutoRestartExplorer", true));
                    ShowSaveConfirmation = Conversions.ToBoolean(GetReg(REG_ThemeApplyingBehavior, "ShowSaveConfirmation", true));
                    ClassicColors_HKU_DEFAULT_Prefs = (OverwriteOptions)Convert.ToInt32(GetReg(REG_ThemeApplyingBehavior, "ClassicColors_HKU_DEFAULT_Prefs", OverwriteOptions.Overwrite));
                    ClassicColors_HKLM_Prefs = (OverwriteOptions)Convert.ToInt32(GetReg(REG_ThemeApplyingBehavior, "ClassicColors_HKLM_Prefs", OverwriteOptions.Erase));
                    UPM_HKU_DEFAULT = Conversions.ToBoolean(GetReg(REG_ThemeApplyingBehavior, "UPM_HKU_DEFAULT", false));
                    Metrics_HKU_DEFAULT_Prefs = (OverwriteOptions)Convert.ToInt32(GetReg(REG_ThemeApplyingBehavior, "Metrics_HKU_DEFAULT_Prefs", OverwriteOptions.DontChange));
                    Cursors_HKU_DEFAULT_Prefs = (OverwriteOptions)Convert.ToInt32(GetReg(REG_ThemeApplyingBehavior, "Cursors_HKU_DEFAULT_Prefs", OverwriteOptions.DontChange));
                    CMD_HKU_DEFAULT_Prefs = (OverwriteOptions)Convert.ToInt32(GetReg(REG_ThemeApplyingBehavior, "CMD_HKU_DEFAULT_Prefs", OverwriteOptions.DontChange));
                    PS86_HKU_DEFAULT_Prefs = (OverwriteOptions)Convert.ToInt32(GetReg(REG_ThemeApplyingBehavior, "PS86_HKU_DEFAULT_Prefs", OverwriteOptions.DontChange));
                    PS64_HKU_DEFAULT_Prefs = (OverwriteOptions)Convert.ToInt32(GetReg(REG_ThemeApplyingBehavior, "PS64_HKU_DEFAULT_Prefs", OverwriteOptions.DontChange));
                    Desktop_HKU_DEFAULT = (OverwriteOptions)Convert.ToInt32(GetReg(REG_ThemeApplyingBehavior, "Desktop_HKU_DEFAULT", OverwriteOptions.DontChange));
                    CMD_OverrideUserPreferences = Conversions.ToBoolean(GetReg(REG_ThemeApplyingBehavior, "CMD_OverrideUserPreferences", true));
                    ResetCursorsToAero = Conversions.ToBoolean(GetReg(REG_ThemeApplyingBehavior, "ResetCursorsToAero", OS.WXP));
                    SFC_on_restoring_StartupSound = Conversions.ToBoolean(GetReg(REG_ThemeApplyingBehavior, "SFC_on_restoring_StartupSound", false));
                    Ignore_PE_Modify_Alert = Conversions.ToBoolean(GetReg(REG_ThemeApplyingBehavior, "Ignore_PE_Modify_Alert", false));
                    Show_WinEffects_Alert = Conversions.ToBoolean(GetReg(REG_ThemeApplyingBehavior, "Show_WinEffects_Alert", true));
                    PE_ModifyByDefault = Conversions.ToBoolean(GetReg(REG_ThemeApplyingBehavior, "PE_ModifyByDefault", true));
                }

                public void Save()
                {
                    EditReg(REG_ThemeApplyingBehavior, "AutoRestartExplorer", AutoRestartExplorer, RegistryValueKind.DWord);
                    EditReg(REG_ThemeApplyingBehavior, "ShowSaveConfirmation", ShowSaveConfirmation, RegistryValueKind.DWord);
                    EditReg(REG_ThemeApplyingBehavior, "ClassicColors_HKU_DEFAULT_Prefs", ClassicColors_HKU_DEFAULT_Prefs, RegistryValueKind.DWord);
                    EditReg(REG_ThemeApplyingBehavior, "ClassicColors_HKLM_Prefs", ClassicColors_HKLM_Prefs, RegistryValueKind.DWord);
                    EditReg(REG_ThemeApplyingBehavior, "UPM_HKU_DEFAULT", UPM_HKU_DEFAULT, RegistryValueKind.DWord);
                    EditReg(REG_ThemeApplyingBehavior, "Metrics_HKU_DEFAULT_Prefs", Metrics_HKU_DEFAULT_Prefs, RegistryValueKind.DWord);
                    EditReg(REG_ThemeApplyingBehavior, "Cursors_HKU_DEFAULT_Prefs", Cursors_HKU_DEFAULT_Prefs, RegistryValueKind.DWord);
                    EditReg(REG_ThemeApplyingBehavior, "CMD_HKU_DEFAULT_Prefs", CMD_HKU_DEFAULT_Prefs, RegistryValueKind.DWord);
                    EditReg(REG_ThemeApplyingBehavior, "PS86_HKU_DEFAULT_Prefs", PS86_HKU_DEFAULT_Prefs, RegistryValueKind.DWord);
                    EditReg(REG_ThemeApplyingBehavior, "PS64_HKU_DEFAULT_Prefs", PS64_HKU_DEFAULT_Prefs, RegistryValueKind.DWord);
                    EditReg(REG_ThemeApplyingBehavior, "Desktop_HKU_DEFAULT", Desktop_HKU_DEFAULT, RegistryValueKind.DWord);
                    EditReg(REG_ThemeApplyingBehavior, "ResetCursorsToAero", ResetCursorsToAero, RegistryValueKind.DWord);
                    EditReg(REG_ThemeApplyingBehavior, "CMD_OverrideUserPreferences", CMD_OverrideUserPreferences, RegistryValueKind.DWord);
                    EditReg(REG_ThemeApplyingBehavior, "SFC_on_restoring_StartupSound", SFC_on_restoring_StartupSound, RegistryValueKind.DWord);
                    EditReg(REG_ThemeApplyingBehavior, "Ignore_PE_Modify_Alert", Ignore_PE_Modify_Alert, RegistryValueKind.DWord);
                    EditReg(REG_ThemeApplyingBehavior, "Show_WinEffects_Alert", Show_WinEffects_Alert, RegistryValueKind.DWord);
                    EditReg(REG_ThemeApplyingBehavior, "PE_ModifyByDefault", PE_ModifyByDefault, RegistryValueKind.DWord);
                }

            }

            public struct Appearance
            {
                public bool DarkMode;
                public bool AutoDarkMode;
                public bool CustomColors;
                public bool CustomTheme_DarkMode;
                public Color AccentColor;
                public Color SecondaryColor;
                public Color TertiaryColor;
                public Color DisabledColor;
                public Color BackColor;
                public Color DisabledBackColor;
                public bool RoundedCorners;
                public bool ManagedByTheme;
                public bool Animations;

                public void Load()
                {
                    DarkMode = Conversions.ToBoolean(GetReg(REG_Appearance, "DarkMode", true));
                    Animations = Conversions.ToBoolean(GetReg(REG_Appearance, "Animations", true));
                    AutoDarkMode = Conversions.ToBoolean(GetReg(REG_Appearance, "AutoDarkMode", true));
                    CustomColors = Conversions.ToBoolean(GetReg(REG_Appearance, "CustomColors", false));
                    CustomTheme_DarkMode = Conversions.ToBoolean(GetReg(REG_Appearance, "CustomTheme", true));
                    AccentColor = Color.FromArgb(Convert.ToInt32(GetReg(REG_Appearance, "AccentColor", DefaultColors.PrimaryColor_Dark.ToArgb())));
                    BackColor = Color.FromArgb(Convert.ToInt32(GetReg(REG_Appearance, "BackColor", DefaultColors.BackColor_Dark.ToArgb())));
                    SecondaryColor = Color.FromArgb(Convert.ToInt32(GetReg(REG_Appearance, "SecondaryColor", DefaultColors.SecondaryColor_Dark.ToArgb())));
                    TertiaryColor = Color.FromArgb(Convert.ToInt32(GetReg(REG_Appearance, "TertiaryColor", DefaultColors.TertiaryColor_Dark.ToArgb())));
                    DisabledColor = Color.FromArgb(Convert.ToInt32(GetReg(REG_Appearance, "DisabledColor", DefaultColors.DisabledColor_Dark.ToArgb())));
                    DisabledBackColor = Color.FromArgb(Convert.ToInt32(GetReg(REG_Appearance, "DisabledBackColor", DefaultColors.DisabledBackColor_Dark.ToArgb())));
                    RoundedCorners = Conversions.ToBoolean(GetReg(REG_Appearance, "RoundedCorners", true));
                    ManagedByTheme = Conversions.ToBoolean(GetReg(REG_Appearance, "ManagedByTheme", true));
                }

                public void Save()
                {
                    EditReg(REG_Appearance, "DarkMode", DarkMode, RegistryValueKind.DWord);
                    EditReg(REG_Appearance, "Animations", Animations, RegistryValueKind.DWord);
                    EditReg(REG_Appearance, "AutoDarkMode", AutoDarkMode, RegistryValueKind.DWord);
                    EditReg(REG_Appearance, "CustomColors", CustomColors, RegistryValueKind.DWord);
                    EditReg(REG_Appearance, "CustomTheme", CustomTheme_DarkMode, RegistryValueKind.DWord);
                    EditReg(REG_Appearance, "AccentColor", AccentColor.ToArgb(), RegistryValueKind.DWord);
                    EditReg(REG_Appearance, "BackColor", BackColor.ToArgb(), RegistryValueKind.DWord);
                    EditReg(REG_Appearance, "SecondaryColor", SecondaryColor.ToArgb(), RegistryValueKind.DWord);
                    EditReg(REG_Appearance, "TertiaryColor", TertiaryColor.ToArgb(), RegistryValueKind.DWord);
                    EditReg(REG_Appearance, "DisabledColor", DisabledColor.ToArgb(), RegistryValueKind.DWord);
                    EditReg(REG_Appearance, "DisabledBackColor", DisabledBackColor.ToArgb(), RegistryValueKind.DWord);
                    EditReg(REG_Appearance, "RoundedCorners", RoundedCorners, RegistryValueKind.DWord);
                    EditReg(REG_Appearance, "ManagedByTheme", ManagedByTheme, RegistryValueKind.DWord);
                }
            }

            public struct Language
            {
                public bool Enabled;
                public string File;

                public void Load()
                {
                    Enabled = Conversions.ToBoolean(GetReg(REG_Language, string.Empty, false));
                    File = GetReg(REG_Language, "File", string.Empty).ToString();
                }

                public void Save()
                {
                    EditReg(REG_Language, string.Empty, Enabled, RegistryValueKind.DWord);
                    EditReg(REG_Language, "File", File, RegistryValueKind.String);
                }

            }

            public struct EP
            {
                public bool Enabled;
                public bool Enabled_Force;
                public bool UseStart10;
                public bool UseTaskbar10;
                public bool TaskbarButton10;
                public ExplorerPatcher.StartStyles StartStyle;

                public void Load()
                {
                    Enabled = Conversions.ToBoolean(GetReg(REG_EP, string.Empty, true));
                    Enabled_Force = Conversions.ToBoolean(GetReg(REG_EP, "Enabled_Force", false));
                    UseStart10 = Conversions.ToBoolean(GetReg(REG_EP, "UseStart10", false));
                    UseTaskbar10 = Conversions.ToBoolean(GetReg(REG_EP, "UseTaskbar10", false));
                    TaskbarButton10 = Conversions.ToBoolean(GetReg(REG_EP, "TaskbarButton10", false));
                    StartStyle = (ExplorerPatcher.StartStyles)Convert.ToInt32(GetReg(REG_EP, "StartStyle", WinPaletter.ExplorerPatcher.StartStyles.NotRounded));
                }

                public void Save()
                {
                    EditReg(REG_EP, string.Empty, Enabled, RegistryValueKind.DWord);
                    EditReg(REG_EP, "Enabled_Force", Enabled_Force, RegistryValueKind.DWord);
                    EditReg(REG_EP, "UseStart10", UseStart10, RegistryValueKind.DWord);
                    EditReg(REG_EP, "UseTaskbar10", UseTaskbar10, RegistryValueKind.DWord);
                    EditReg(REG_EP, "TaskbarButton10", TaskbarButton10, RegistryValueKind.DWord);
                    EditReg(REG_EP, "StartStyle", StartStyle, RegistryValueKind.DWord);
                }
            }

            public struct ThemeLog
            {
                public VerboseLevels VerboseLevel;
                public bool ShowSkippedItemsOnDetailedVerbose;
                public bool CountDown;
                public int CountDown_Seconds;

                public enum VerboseLevels
                {
                    None,
                    Basic,
                    Detailed
                }

                public bool Enabled()
                {
                    return VerboseLevel != VerboseLevels.None;
                }

                public void Load()
                {
                    VerboseLevel = (VerboseLevels)Convert.ToInt32(GetReg(REG_ThemeLog, "VerboseLevel", VerboseLevels.Basic));
                    ShowSkippedItemsOnDetailedVerbose = Conversions.ToBoolean(GetReg(REG_ThemeLog, "ShowSkippedItemsOnDetailedVerbose", false));
                    CountDown = Conversions.ToBoolean(GetReg(REG_ThemeLog, "CountDown", true));
                    CountDown_Seconds = Convert.ToInt32(GetReg(REG_ThemeLog, "CountDown_Seconds", 20));
                }

                public void Save()
                {
                    EditReg(REG_ThemeLog, "VerboseLevel", VerboseLevel, RegistryValueKind.DWord);
                    EditReg(REG_ThemeLog, "ShowSkippedItemsOnDetailedVerbose", ShowSkippedItemsOnDetailedVerbose, RegistryValueKind.DWord);
                    EditReg(REG_ThemeLog, "CountDown", CountDown, RegistryValueKind.DWord);
                    EditReg(REG_ThemeLog, "CountDown_Seconds", CountDown_Seconds, RegistryValueKind.DWord);
                }

            }

            public struct WindowsTerminal
            {
                public bool Bypass;
                public bool ListAllFonts;
                public bool Path_Deflection;
                public string Terminal_Stable_Path;
                public string Terminal_Preview_Path;

                public void Load()
                {
                    Bypass = Conversions.ToBoolean(GetReg(REG_WindowsTerminals, "Bypass", false));
                    ListAllFonts = Conversions.ToBoolean(GetReg(REG_WindowsTerminals, "ListAllFonts", false));
                    Path_Deflection = Conversions.ToBoolean(GetReg(REG_WindowsTerminals, "Path_Deflection", false));
                    Terminal_Stable_Path = GetReg(REG_WindowsTerminals, "Terminal_Stable_Path", SysPaths.TerminalJSON).ToString();
                    Terminal_Preview_Path = GetReg(REG_WindowsTerminals, "Terminal_Preview_Path", SysPaths.TerminalPreviewJSON).ToString();
                }

                public void Save()
                {
                    EditReg(REG_WindowsTerminals, "Bypass", Bypass, RegistryValueKind.DWord);
                    EditReg(REG_WindowsTerminals, "ListAllFonts", ListAllFonts, RegistryValueKind.DWord);
                    EditReg(REG_WindowsTerminals, "Path_Deflection", Path_Deflection, RegistryValueKind.DWord);
                    EditReg(REG_WindowsTerminals, "Terminal_Stable_Path", Terminal_Stable_Path, RegistryValueKind.String);
                    EditReg(REG_WindowsTerminals, "Terminal_Preview_Path", Terminal_Preview_Path, RegistryValueKind.String);
                }

            }

            public struct Store
            {
                public bool Search_ThemeNames;
                public bool Search_AuthorsNames;
                public bool Search_Descriptions;
                public bool Online_or_Offline;
                public string[] Online_Repositories;
                public string[] Offline_Directories;
                public bool Offline_SubFolders;
                public bool ShowTips;

                public void Load()
                {
                    Online_or_Offline = Conversions.ToBoolean(GetReg(REG_Store, "Online_or_Offline", true));
                    Online_Repositories = (string[])GetReg(REG_Store, "Online_Repositories", new[] { Links.Store_MainDB, Links.Store_2ndDB });
                    Offline_Directories = (string[])GetReg(REG_Store, "Offline_Directories", new[] { string.Empty });
                    Offline_SubFolders = Conversions.ToBoolean(GetReg(REG_Store, "Offline_SubFolders", true));
                    Search_ThemeNames = Conversions.ToBoolean(GetReg(REG_Store, "Search_ThemeNames", true));
                    Search_AuthorsNames = Conversions.ToBoolean(GetReg(REG_Store, "Search_AuthorsNames", true));
                    Search_Descriptions = Conversions.ToBoolean(GetReg(REG_Store, "Search_Descriptions", true));
                    ShowTips = Conversions.ToBoolean(GetReg(REG_Store, "ShowTips", true));

                    if (!Online_Repositories.Contains(Links.Store_MainDB))
                    {
                        Array.Resize(ref Online_Repositories, Online_Repositories.Length + 1);
                        Online_Repositories[Online_Repositories.Length - 1] = Links.Store_MainDB;
                    }

                    if (!Online_Repositories.Contains(Links.Store_2ndDB))
                    {
                        Array.Resize(ref Online_Repositories, Online_Repositories.Length + 1);
                        Online_Repositories[Online_Repositories.Length - 1] = Links.Store_2ndDB;
                    }
                }

                public void Save()
                {
                    if (!Online_Repositories.Contains(Links.Store_MainDB))
                    {
                        Array.Resize(ref Online_Repositories, Online_Repositories.Length + 1);
                        Online_Repositories[Online_Repositories.Length - 1] = Links.Store_MainDB;
                    }

                    if (!Online_Repositories.Contains(Links.Store_2ndDB))
                    {
                        Array.Resize(ref Online_Repositories, Online_Repositories.Length + 1);
                        Online_Repositories[Online_Repositories.Length - 1] = Links.Store_2ndDB;
                    }

                    EditReg(REG_Store, "Search_ThemeNames", Search_ThemeNames, RegistryValueKind.DWord);
                    EditReg(REG_Store, "Search_AuthorsNames", Search_AuthorsNames, RegistryValueKind.DWord);
                    EditReg(REG_Store, "Search_Descriptions", Search_Descriptions, RegistryValueKind.DWord);
                    EditReg(REG_Store, "Online_or_Offline", Online_or_Offline, RegistryValueKind.DWord);
                    EditReg(REG_Store, "Online_Repositories", Online_Repositories, RegistryValueKind.MultiString);
                    EditReg(REG_Store, "Offline_Directories", Offline_Directories, RegistryValueKind.MultiString);
                    EditReg(REG_Store, "Offline_SubFolders", Offline_SubFolders, RegistryValueKind.DWord);
                    EditReg(REG_Store, "ShowTips", ShowTips, RegistryValueKind.DWord);
                }

            }

            public struct NerdStats
            {
                public bool Enabled;
                public Formats Type;
                public bool ShowHexHash;
                public bool UseWindowsMonospacedFont;
                public bool MoreLabelTransparency;
                public bool DotDefaultChangedIndicator;
                public bool DragAndDrop;
                public bool Classic_Color_Picker;

                public enum Formats
                {
                    HEX,
                    RGB,
                    HSL,
                    Dec
                }

                public void Load()
                {
                    Enabled = Conversions.ToBoolean(GetReg(REG_NerdStats, string.Empty, true));
                    ShowHexHash = Conversions.ToBoolean(GetReg(REG_NerdStats, "ShowHexHash", true));
                    Type = (Formats)Convert.ToInt32(GetReg(REG_NerdStats, "Type", Formats.HEX));
                    UseWindowsMonospacedFont = Conversions.ToBoolean(GetReg(REG_NerdStats, "UseWindowsMonospacedFont", false));
                    MoreLabelTransparency = Conversions.ToBoolean(GetReg(REG_NerdStats, "MoreLabelTransparency", false));
                    DotDefaultChangedIndicator = Conversions.ToBoolean(GetReg(REG_NerdStats, "DotDefaultChangedIndicator", true));
                    DragAndDrop = Conversions.ToBoolean(GetReg(REG_NerdStats, "DragAndDrop", true));
                    Classic_Color_Picker = Conversions.ToBoolean(GetReg(REG_NerdStats, "Classic_Color_Picker", false));
                }

                public void Save()
                {
                    EditReg(REG_NerdStats, string.Empty, Enabled, RegistryValueKind.DWord);
                    EditReg(REG_NerdStats, "ShowHexHash", ShowHexHash, RegistryValueKind.DWord);
                    EditReg(REG_NerdStats, "Type", (int)Type);
                    EditReg(REG_NerdStats, "UseWindowsMonospacedFont", UseWindowsMonospacedFont);
                    EditReg(REG_NerdStats, "MoreLabelTransparency", MoreLabelTransparency);
                    EditReg(REG_NerdStats, "DotDefaultChangedIndicator", DotDefaultChangedIndicator);
                    EditReg(REG_NerdStats, "DragAndDrop", DragAndDrop);
                    EditReg(REG_NerdStats, "Classic_Color_Picker", Classic_Color_Picker, RegistryValueKind.DWord);

                }

            }

            public struct UsersServices
            {
                public bool ShowSysEventsSoundsInstaller;

                public void Load()
                {
                    ShowSysEventsSoundsInstaller = Conversions.ToBoolean(GetReg(REG_UsersServices, "ShowSysEventsSoundsInstaller", true));
                }

                public void Save()
                {
                    EditReg(REG_UsersServices, "ShowSysEventsSoundsInstaller", ShowSysEventsSoundsInstaller, RegistryValueKind.DWord);
                }
            }

            public struct Miscellaneous
            {
                public bool Win7LivePreview;

                public void Load()
                {
                    Win7LivePreview = Conversions.ToBoolean(GetReg(REG_Miscellaneous, "Win7LivePreview", true));
                }

                public void Save()
                {
                    EditReg(REG_Miscellaneous, "Win7LivePreview", Win7LivePreview, RegistryValueKind.DWord);
                }

            }

            public struct BackupTheme
            {
                public bool Enabled;
                public bool AutoBackupOnAppOpen;
                public bool AutoBackupOnApply;
                public bool AutoBackupOnApplySingleAspect;
                public bool AutoBackupOnThemeLoad;
                public string BackupPath;

                public void Load()
                {
                    Enabled = Conversions.ToBoolean(GetReg(REG_Backup, "Enabled", true));
                    AutoBackupOnAppOpen = Conversions.ToBoolean(GetReg(REG_Backup, "AutoBackupOnAppOpen", false));
                    AutoBackupOnApply = Conversions.ToBoolean(GetReg(REG_Backup, "AutoBackupOnApply", true));
                    AutoBackupOnApplySingleAspect = Conversions.ToBoolean(GetReg(REG_Backup, "AutoBackupOnApplySingleAspect", true));
                    AutoBackupOnThemeLoad = Conversions.ToBoolean(GetReg(REG_Backup, "AutoBackupOnThemeLoad", false));
                    BackupPath = GetReg(REG_Backup, "BackupPath", $"{SysPaths.appData}\\Backup\\Themes").ToString();
                }

                public void Save()
                {
                    EditReg(REG_Backup, "Enabled", Enabled, RegistryValueKind.DWord);
                    EditReg(REG_Backup, "AutoBackupOnAppOpen", AutoBackupOnAppOpen, RegistryValueKind.DWord);
                    EditReg(REG_Backup, "AutoBackupOnApply", AutoBackupOnApply, RegistryValueKind.DWord);
                    EditReg(REG_Backup, "AutoBackupOnApplySingleAspect", AutoBackupOnApplySingleAspect, RegistryValueKind.DWord);
                    EditReg(REG_Backup, "AutoBackupOnThemeLoad", AutoBackupOnThemeLoad, RegistryValueKind.DWord);
                    EditReg(REG_Backup, "BackupPath", BackupPath, RegistryValueKind.String);
                }
            }

            public struct AspectsControl
            {
                public bool Enabled;
                public bool WinColors;
                public bool WinColors_Advanced;
                public bool LogonUI;
                public bool ClassicColors;
                public bool ClassicColors_Advanced;
                public bool MetricsFonts;
                public bool MetricsFonts_Advanced;
                public bool Cursors;
                public bool Cursors_Advanced;
                public bool Consoles;
                public bool WinTerminals;
                public bool Wallpaper;
                public bool Wallpaper_Advanced;
                public bool Effects;
                public bool Sounds;
                public bool ScreenSaver;
                public bool AltTab;
                public bool Icons;

                public void Load()
                {
                    Enabled = Conversions.ToBoolean(GetReg(REG_AspectsControl, string.Empty, false));
                    WinColors = Conversions.ToBoolean(GetReg(REG_AspectsControl, "WinColors", true));
                    WinColors_Advanced = Conversions.ToBoolean(GetReg(REG_AspectsControl, "WinColors_Advanced", true));
                    LogonUI = Conversions.ToBoolean(GetReg(REG_AspectsControl, "LogonUI", true));
                    ClassicColors = Conversions.ToBoolean(GetReg(REG_AspectsControl, "ClassicColors", true));
                    ClassicColors_Advanced = Conversions.ToBoolean(GetReg(REG_AspectsControl, "ClassicColors_Advanced", true));
                    MetricsFonts = Conversions.ToBoolean(GetReg(REG_AspectsControl, "MetricsFonts", true));
                    MetricsFonts_Advanced = Conversions.ToBoolean(GetReg(REG_AspectsControl, "MetricsFonts_Advanced", true));
                    Cursors = Conversions.ToBoolean(GetReg(REG_AspectsControl, "Cursors", true));
                    Cursors_Advanced = Conversions.ToBoolean(GetReg(REG_AspectsControl, "Cursors_Advanced", true));
                    Consoles = Conversions.ToBoolean(GetReg(REG_AspectsControl, "Consoles", true));
                    WinTerminals = Conversions.ToBoolean(GetReg(REG_AspectsControl, "WinTerminals", true));
                    Wallpaper = Conversions.ToBoolean(GetReg(REG_AspectsControl, "Wallpaper", true));
                    Wallpaper_Advanced = Conversions.ToBoolean(GetReg(REG_AspectsControl, "Wallpaper_Advanced", true));
                    Effects = Conversions.ToBoolean(GetReg(REG_AspectsControl, "Effects", true));
                    Sounds = Conversions.ToBoolean(GetReg(REG_AspectsControl, "Sounds", true));
                    ScreenSaver = Conversions.ToBoolean(GetReg(REG_AspectsControl, "ScreenSaver", true));
                    AltTab = Conversions.ToBoolean(GetReg(REG_AspectsControl, "AltTab", true));
                    Icons = Conversions.ToBoolean(GetReg(REG_AspectsControl, "Icons", true));
                }

                public void Save()
                {
                    EditReg(REG_AspectsControl, string.Empty, Enabled, RegistryValueKind.DWord);
                    EditReg(REG_AspectsControl, "WinColors", WinColors, RegistryValueKind.DWord);
                    EditReg(REG_AspectsControl, "WinColors_Advanced", WinColors_Advanced, RegistryValueKind.DWord);
                    EditReg(REG_AspectsControl, "LogonUI", LogonUI, RegistryValueKind.DWord);
                    EditReg(REG_AspectsControl, "ClassicColors", ClassicColors, RegistryValueKind.DWord);
                    EditReg(REG_AspectsControl, "ClassicColors_Advanced", ClassicColors_Advanced, RegistryValueKind.DWord);
                    EditReg(REG_AspectsControl, "MetricsFonts", MetricsFonts, RegistryValueKind.DWord);
                    EditReg(REG_AspectsControl, "MetricsFonts_Advanced", MetricsFonts_Advanced, RegistryValueKind.DWord);
                    EditReg(REG_AspectsControl, "Cursors", Cursors, RegistryValueKind.DWord);
                    EditReg(REG_AspectsControl, "Cursors_Advanced", Cursors_Advanced, RegistryValueKind.DWord);
                    EditReg(REG_AspectsControl, "Consoles", Consoles, RegistryValueKind.DWord);
                    EditReg(REG_AspectsControl, "WinTerminals", WinTerminals, RegistryValueKind.DWord);
                    EditReg(REG_AspectsControl, "Wallpaper", Wallpaper, RegistryValueKind.DWord);
                    EditReg(REG_AspectsControl, "Wallpaper_Advanced", Wallpaper_Advanced, RegistryValueKind.DWord);
                    EditReg(REG_AspectsControl, "Effects", Effects, RegistryValueKind.DWord);
                    EditReg(REG_AspectsControl, "Sounds", Sounds, RegistryValueKind.DWord);
                    EditReg(REG_AspectsControl, "ScreenSaver", ScreenSaver, RegistryValueKind.DWord);
                    EditReg(REG_AspectsControl, "AltTab", AltTab, RegistryValueKind.DWord);
                    EditReg(REG_AspectsControl, "Icons", Icons, RegistryValueKind.DWord);
                }
            }
        }

        public Structures.General General = new()
        {
            LicenseAccepted = false,
            MainFormWidth = 1110,
            MainFormHeight = 725,
            MainFormStatus = FormWindowState.Normal,
            WhatsNewRecord = new[] { string.Empty }
        };

        public Structures.Updates Updates = new()
        {
            AutoCheck = true,
            Channel = Program.IsBeta ? Structures.Updates.Channels.Beta : Structures.Updates.Channels.Stable
        };

        public Structures.FileTypeMgr FileTypeManagement = new()
        {
            AutoAddExt = true,
            OpeningPreviewInApp_or_AppliesIt = true,
            CompressThemeFile = true
        };

        public Structures.ThemeApplyingBehavior ThemeApplyingBehavior = new()
        {
            AutoRestartExplorer = true,
            ShowSaveConfirmation = true,
            ClassicColors_HKU_DEFAULT_Prefs = Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite,
            ClassicColors_HKLM_Prefs = Structures.ThemeApplyingBehavior.OverwriteOptions.Erase,
            UPM_HKU_DEFAULT = false,
            Metrics_HKU_DEFAULT_Prefs = Structures.ThemeApplyingBehavior.OverwriteOptions.DontChange,
            ResetCursorsToAero = OS.WXP,
            Cursors_HKU_DEFAULT_Prefs = Structures.ThemeApplyingBehavior.OverwriteOptions.DontChange,
            CMD_HKU_DEFAULT_Prefs = Structures.ThemeApplyingBehavior.OverwriteOptions.DontChange,
            PS86_HKU_DEFAULT_Prefs = Structures.ThemeApplyingBehavior.OverwriteOptions.DontChange,
            PS64_HKU_DEFAULT_Prefs = Structures.ThemeApplyingBehavior.OverwriteOptions.DontChange,
            Desktop_HKU_DEFAULT = Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite,
            CMD_OverrideUserPreferences = true,
            SFC_on_restoring_StartupSound = false,
            Ignore_PE_Modify_Alert = false,
            Show_WinEffects_Alert = true,
            PE_ModifyByDefault = true
        };

        public Structures.Appearance Appearance = new()
        {
            DarkMode = true,
            AutoDarkMode = true,
            CustomColors = false,
            CustomTheme_DarkMode = true,
            AccentColor = DefaultColors.PrimaryColor_Dark,
            BackColor = DefaultColors.BackColor_Dark,
            SecondaryColor = DefaultColors.SecondaryColor_Dark,
            TertiaryColor = DefaultColors.TertiaryColor_Dark,
            DisabledColor = DefaultColors.DisabledColor_Dark,
            DisabledBackColor = DefaultColors.DisabledBackColor_Dark,
            RoundedCorners = true,
            ManagedByTheme = true,
            Animations = true
        };

        public Structures.Language Language = new()
        {
            Enabled = false,
            File = null
        };

        public Structures.EP ExplorerPatcher = new()
        {
            Enabled = true,
            Enabled_Force = false,
            UseStart10 = false,
            UseTaskbar10 = false,
            TaskbarButton10 = false,
            StartStyle = WinPaletter.ExplorerPatcher.StartStyles.NotRounded
        };

        public Structures.ThemeLog ThemeLog = new()
        {
            VerboseLevel = Structures.ThemeLog.VerboseLevels.Basic,
            ShowSkippedItemsOnDetailedVerbose = false,
            CountDown = true,
            CountDown_Seconds = 20
        };

        public Structures.WindowsTerminal WindowsTerminals = new()
        {
            Bypass = false,
            ListAllFonts = false,
            Path_Deflection = false,
            Terminal_Stable_Path = SysPaths.TerminalJSON,
            Terminal_Preview_Path = SysPaths.TerminalPreviewJSON
        };

        public Structures.Store Store = new()
        {
            Search_ThemeNames = true,
            Search_AuthorsNames = true,
            Search_Descriptions = true,
            Online_or_Offline = true,
            Online_Repositories = new[] { Links.Store_MainDB, Links.Store_2ndDB },
            Offline_Directories = new[] { string.Empty },
            Offline_SubFolders = true,
            ShowTips = true
        };

        public Structures.NerdStats NerdStats = new()
        {
            Enabled = true,
            Type = Structures.NerdStats.Formats.HEX,
            ShowHexHash = true,
            MoreLabelTransparency = false,
            UseWindowsMonospacedFont = false,
            DotDefaultChangedIndicator = true,
            DragAndDrop = true,
            Classic_Color_Picker = false
        };

        public Structures.UsersServices UsersServices = new()
        {
            ShowSysEventsSoundsInstaller = true,
        };

        public Structures.Miscellaneous Miscellaneous = new()
        {
            Win7LivePreview = true,
        };

        public Structures.BackupTheme BackupTheme = new()
        {
            Enabled = true,
            AutoBackupOnAppOpen = false,
            AutoBackupOnApply = true,
            AutoBackupOnApplySingleAspect = true,
            AutoBackupOnThemeLoad = false,
            BackupPath = $"{SysPaths.appData}\\Backup\\Themes"
        };

        public Structures.AspectsControl AspectsControl = new()
        {
            Enabled = false,
            WinColors = true,
            WinColors_Advanced = true,
            LogonUI = true,
            ClassicColors = true,
            ClassicColors_Advanced = true,
            MetricsFonts = true,
            MetricsFonts_Advanced = true,
            Cursors = true,
            Cursors_Advanced = true,
            Consoles = true,
            WinTerminals = true,
            Wallpaper = true,
            Wallpaper_Advanced = true,
            Effects = true,
            Sounds = true,
            ScreenSaver = true,
            AltTab = true,
            Icons = true,
        };

        public enum Mode
        {
            Registry,
            File,
            Empty
        }

        public Settings(Mode LoadFrom, string File = null)
        {
            switch (LoadFrom)
            {

                case Mode.Registry:
                    {
                        General.Load();
                        Updates.Load();
                        FileTypeManagement.Load();
                        ThemeApplyingBehavior.Load();
                        Appearance.Load();
                        Language.Load();
                        ExplorerPatcher.Load();
                        ThemeLog.Load();
                        WindowsTerminals.Load();
                        Store.Load();
                        NerdStats.Load();
                        UsersServices.Load();
                        Miscellaneous.Load();
                        BackupTheme.Load();
                        AspectsControl.Load();
                        break;
                    }

                case Mode.File:
                    {
                        if (System.IO.File.Exists(File))
                        {
                            string[] txt = System.IO.File.ReadAllLines(File);

                            if (IsValidJson(string.Join("\r\n", txt)))
                            {
                                try
                                {
                                    JObject J = JObject.Parse(string.Join("\r\n", txt));

                                    foreach (FieldInfo field in GetType().GetFields(bindingFlags))
                                    {
                                        Type type = field.FieldType;
                                        JsonSerializerSettings JSet = new();

                                        if (J[field.Name] is not null)
                                        {
                                            field.SetValue(this, J[field.Name].ToObject(type));
                                        }
                                    }

                                    if (!Store.Online_Repositories.Contains(Links.Store_MainDB))
                                    {
                                        Array.Resize(ref Store.Online_Repositories, Store.Online_Repositories.Length + 1);
                                        Store.Online_Repositories[Store.Online_Repositories.Length - 1] = Links.Store_MainDB;
                                    }

                                    if (!Store.Online_Repositories.Contains(Links.Store_2ndDB))
                                    {
                                        Array.Resize(ref Store.Online_Repositories, Store.Online_Repositories.Length + 1);
                                        Store.Online_Repositories[Store.Online_Repositories.Length - 1] = Links.Store_2ndDB;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Forms.BugReport.ThrowError(ex);
                                }
                            }
                            else
                            {
                                Forms.BugReport.ThrowError(new Exception(Program.Lang.SettingsFileNotJSON));
                            }
                        }
                        else
                        {
                            Forms.BugReport.ThrowError(new Exception(Program.Lang.SettingsFileNotExist));
                        }

                        break;
                    }

            }
        }

        public void Save(Mode SaveTo, string File = null)
        {
            switch (SaveTo)
            {
                case Mode.Registry:
                    {

                        General.Save();
                        Updates.Save();
                        FileTypeManagement.Save();
                        ThemeApplyingBehavior.Save();
                        Appearance.Save();
                        Language.Save();
                        ExplorerPatcher.Save();
                        ThemeLog.Save();
                        WindowsTerminals.Save();
                        Store.Save();
                        NerdStats.Save();
                        UsersServices.Save();
                        Miscellaneous.Save();
                        BackupTheme.Save();
                        AspectsControl.Save();
                        break;
                    }

                case Mode.File:
                    {
                        if (!Store.Online_Repositories.Contains(Links.Store_MainDB))
                        {
                            Array.Resize(ref Store.Online_Repositories, Store.Online_Repositories.Length + 1);
                            Store.Online_Repositories[Store.Online_Repositories.Length - 1] = Links.Store_MainDB;
                        }

                        if (!Store.Online_Repositories.Contains(Links.Store_2ndDB))
                        {
                            Array.Resize(ref Store.Online_Repositories, Store.Online_Repositories.Length + 1);
                            Store.Online_Repositories[Store.Online_Repositories.Length - 1] = Links.Store_2ndDB;
                        }

                        System.IO.File.WriteAllText(File, ToString());
                        break;
                    }
            }
        }

        public override string ToString()
        {
            JObject JSON_Overall = new();
            JSON_Overall.RemoveAll();

            foreach (FieldInfo field in GetType().GetFields(bindingFlags))
            {
                if (field.Name.Trim().ToUpper() != "GENERAL")
                {
                    Type type = field.FieldType;

                    if (IsStructure(type))
                    {
                        JSON_Overall.Add(field.Name, DeserializeProps(type, field.GetValue(this)));
                    }
                    else
                    {
                        JSON_Overall.Add(field.Name, JToken.FromObject(field.GetValue(this)));
                    }
                }
            }

            return JSON_Overall.ToString();
        }

        private JObject DeserializeProps(Type StructureType, object Structure)
        {
            JObject j = new();

            j.RemoveAll();

            foreach (FieldInfo field in StructureType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
            {
                JToken result;

                try
                {
                    result = JToken.FromObject(field.GetValue(Structure));
                }
                catch
                {
                    result = default;
                }

                j.Add(field.Name, result);
            }

            return j;
        }

        private static bool IsValidJson(string strInput)
        {
            if (string.IsNullOrWhiteSpace(strInput))
            {
                return false;
            }
            strInput = strInput.Trim();
            if (strInput.StartsWith("{") && strInput.EndsWith("}") || strInput.StartsWith("[") && strInput.EndsWith("]")) // For object
            {
                // For array
                try
                {
                    JToken obj = JToken.Parse(strInput);
                    return true;
                }
                catch (JsonReaderException)
                {
                    // Exception in parsing json
                    return false;
                }
                catch (Exception) // some other exception
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public bool IsStructure(Type type)
        {
            return type.IsValueType && !type.IsPrimitive && type.Namespace is not null && !type.Namespace.StartsWith("System.");
        }

    }
}