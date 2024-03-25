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
    /// <summary>
    /// WinPaletter settings class
    /// </summary>
    public class Settings
    {
        private BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public;

        /// <summary>
        /// Settings structures
        /// </summary>
        public class Structures
        {
            #region paths
            static readonly string REG = "HKEY_CURRENT_USER\\Software\\WinPaletter\\Settings";
            static readonly string REG_General = $"{REG}\\General";
            static readonly string REG_General_MainForm = $"{REG_General}\\MainForm";
            static readonly string REG_Updates = $"{REG}\\Updates";
            static readonly string REG_FileTypeManagement = $"{REG}\\FileTypeManagement";
            static readonly string REG_ThemeApplyingBehavior = $"{REG}\\ThemeApplyingBehavior";

            /// <summary>
            /// Registry path to Appearance settings
            /// </summary>
            public static readonly string REG_Appearance = $"{REG}\\Appearance\\NewGen";

            static readonly string REG_Language = $"{REG}\\Language";
            static readonly string REG_EP = $"{REG}\\ExplorerPatcher";
            static readonly string REG_ThemeLog = $"{REG}\\ThemeLog";
            static readonly string REG_WindowsTerminals = $"{REG}\\WindowsTerminals";
            static readonly string REG_Store = $"{REG}\\Store";
            static readonly string REG_NerdStats = $"{REG}\\NerdStats";
            static readonly string REG_UsersServices = $"{REG}\\UsersServices";
            static readonly string REG_Miscellaneous = $"{REG}\\Miscellaneous";
            static readonly string REG_Backup = $"{REG}\\Backup";
            static readonly string REG_AspectsControl = $"{REG}\\AspectsControl";
            #endregion

            /// <summary>
            /// General settings structure
            /// </summary>
            public struct General
            {
                /// <summary>
                /// Is license accepted
                /// </summary>
                public bool LicenseAccepted = false;

                /// <summary>
                /// Width of main form (int)
                /// </summary>
                public object MainFormWidth = 1110;

                /// <summary>
                /// Height of main form (int)
                /// </summary>
                public object MainFormHeight = 725;

                /// <summary>
                /// Status of main form, to be remembered at WinPaletter startup
                /// </summary>
                public object MainFormStatus = FormWindowState.Normal;

                /// <summary>
                /// Display aspects cards in the main form in compact mode
                /// </summary>
                public bool CompactAspects = false;

                /// <summary>
                /// String array of opened WinPaletter versions
                /// </summary>
                public string[] WhatsNewRecord = new[] { string.Empty };

                /// <summary>
                /// Create new instance of General settings structure with default values
                /// </summary>
                public General() { }

                /// <summary>
                /// Load settings from registry
                /// </summary>
                public void Load()
                {
                    // Renamed: LicenseAccepted_0, to avoid conflicts with the old version, as new license is MIT/LGPL Dual License
                    LicenseAccepted = Conversions.ToBoolean(GetReg(REG_General, "LicenseAccepted_0", false));
                    WhatsNewRecord = (string[])GetReg(REG_General, "WhatsNewRecord", new[] { string.Empty });
                    MainFormWidth = GetReg(REG_General_MainForm, "MainFormWidth", 1110);
                    MainFormHeight = GetReg(REG_General_MainForm, "MainFormHeight", 725);
                    MainFormStatus = GetReg(REG_General_MainForm, "MainFormStatus", FormWindowState.Normal);
                    CompactAspects = Conversions.ToBoolean(GetReg(REG_General, "CompactAspects", false));
                }

                /// <summary>
                /// Save settings to registry
                /// </summary>
                public void Save()
                {
                    EditReg(REG_General, "LicenseAccepted_0", LicenseAccepted, RegistryValueKind.DWord);
                    EditReg(REG_General, "WhatsNewRecord", WhatsNewRecord, RegistryValueKind.MultiString);
                    EditReg(REG_General_MainForm, "MainFormWidth", MainFormWidth, RegistryValueKind.DWord);
                    EditReg(REG_General_MainForm, "MainFormHeight", MainFormHeight, RegistryValueKind.DWord);
                    EditReg(REG_General_MainForm, "MainFormStatus", MainFormStatus, RegistryValueKind.DWord);
                    EditReg(REG_General, "CompactAspects", CompactAspects, RegistryValueKind.DWord);
                }
            }

            /// <summary>
            /// Updates structure
            /// </summary>
            public struct Updates
            {
                /// <summary>
                /// Automatic check for updates at WinPaletter startup
                /// </summary>
                public bool AutoCheck = true;

                /// <summary>
                /// Updates channel, either <c>stable</c> or <c>beta</c>
                /// </summary>
                public Channels Channel = Program.IsBeta ? Structures.Updates.Channels.Beta : Structures.Updates.Channels.Stable;

                /// <summary>
                /// Create new instance of Updates settings structure with default values
                /// </summary>
                public Updates() { }

                /// <summary>
                /// Updates channels enumeration
                /// </summary>
                public enum Channels
                {
                    /// <summary>
                    /// Stable updates channel
                    /// </summary>
                    Stable,
                    /// <summary>
                    /// Beta updates channel
                    /// </summary>
                    Beta
                }

                /// <summary>
                /// Load settings from registry
                /// </summary>
                public void Load()
                {
                    AutoCheck = Conversions.ToBoolean(GetReg(REG_Updates, "AutoCheck", true));
                    Channel = (Channels)GetReg(REG_Updates, "Channel", Channels.Stable) == Channels.Stable ? Channels.Stable : Channels.Beta;
                }

                /// <summary>
                /// Save settings to registry
                /// </summary>
                public void Save()
                {
                    EditReg(REG_Updates, "AutoCheck", AutoCheck, RegistryValueKind.DWord);
                    EditReg(REG_Updates, "Channel", Channel == Channels.Stable ? 0 : 1);
                }
            }

            /// <summary>
            /// File type management settings structure
            /// </summary>
            public struct FileTypeMgr
            {
                /// <summary>
                /// Automatic add file extension to theme files, settings files, and theme resources pack files at WinPaletter startup
                /// </summary>
                public bool AutoAddExt = true;

                /// <summary>
                /// If <c>true</c>, opening a theme preview file from Windows explorer will open it in WinPaletter.
                /// <br></br>
                /// If <c>false</c>, opening a theme preview file from Windows explorer will apply it without loading WinPaletter GUI.
                /// </summary>
                public bool OpeningPreviewInApp_or_AppliesIt = true;

                /// <summary>
                /// Compress theme file json content when saving it
                /// </summary>
                public bool CompressThemeFile = true;

                /// <summary>
                /// Create new instance of FileTypeMgr settings structure with default values
                /// </summary>
                public FileTypeMgr() { }

                /// <summary>
                /// Load settings from registry
                /// </summary>
                public void Load()
                {
                    AutoAddExt = Conversions.ToBoolean(GetReg(REG_FileTypeManagement, "AutoAddExt", true));
                    OpeningPreviewInApp_or_AppliesIt = Conversions.ToBoolean(GetReg(REG_FileTypeManagement, "OpeningPreviewInApp_or_AppliesIt", true));
                    CompressThemeFile = Conversions.ToBoolean(GetReg(REG_FileTypeManagement, "CompressThemeFile", true));
                }

                /// <summary>
                /// Save settings to registry
                /// </summary>
                public void Save()
                {
                    EditReg(REG_FileTypeManagement, "AutoAddExt", AutoAddExt, RegistryValueKind.DWord);
                    EditReg(REG_FileTypeManagement, "OpeningPreviewInApp_or_AppliesIt", OpeningPreviewInApp_or_AppliesIt, RegistryValueKind.DWord);
                    EditReg(REG_FileTypeManagement, "CompressThemeFile", CompressThemeFile, RegistryValueKind.DWord);
                }
            }

            /// <summary>
            /// Theme applying behavior settings structure
            /// </summary>
            public struct ThemeApplyingBehavior
            {
                /// <summary>
                /// Automatically restart Windows Explorer after applying a theme
                /// </summary>
                public bool AutoRestartExplorer = true;

                /// <summary>
                /// Show save confirmation dialog after closing WinPaletter
                /// </summary>
                public bool ShowSaveConfirmation = true;

                /// <summary>
                /// <see cref="OverwriteOptions"/> to extend effects of Classic Colors settings to HKU\.DEFAULT registry key
                /// </summary>
                public OverwriteOptions ClassicColors_HKU_DEFAULT_Prefs = Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite;

                /// <summary>
                /// <see cref="OverwriteOptions"/> to extend effects of Classic Colors settings to HKEY_LOCAL_MACHINE registry key
                /// </summary>
                public OverwriteOptions ClassicColors_HKLM_Prefs = Structures.ThemeApplyingBehavior.OverwriteOptions.Erase;

                /// <summary>
                /// Update User Preference Mack (UPM) in HKU\.DEFAULT registry key
                /// </summary>
                public bool UPM_HKU_DEFAULT = false;

                /// <summary>
                /// <see cref="OverwriteOptions"/> to extend effects of Metrics settings to HKU\.DEFAULT registry key
                /// </summary>
                public OverwriteOptions Metrics_HKU_DEFAULT_Prefs = Structures.ThemeApplyingBehavior.OverwriteOptions.DontChange;

                /// <summary>
                /// Reset cursors to Aero cursors after applying a theme if Cursors aspect is not enabled
                /// </summary>
                public bool ResetCursorsToAero = OS.WXP;

                /// <summary>
                /// <see cref="OverwriteOptions"/> to extend effects of Cursors settings to HKU\.DEFAULT registry key
                /// </summary>
                public OverwriteOptions Cursors_HKU_DEFAULT_Prefs = Structures.ThemeApplyingBehavior.OverwriteOptions.DontChange;

                /// <summary>
                /// <see cref="OverwriteOptions"/> to extend effects of Command Prompt settings to HKU\.DEFAULT registry key
                /// </summary>
                public OverwriteOptions CMD_HKU_DEFAULT_Prefs = Structures.ThemeApplyingBehavior.OverwriteOptions.DontChange;

                /// <summary>
                /// <see cref="OverwriteOptions"/> to extend effects of PowerShell x86 settings to HKU\.DEFAULT registry key
                /// </summary>
                public OverwriteOptions PS86_HKU_DEFAULT_Prefs = Structures.ThemeApplyingBehavior.OverwriteOptions.DontChange;

                /// <summary>
                /// <see cref="OverwriteOptions"/> to extend effects of PowerShell x64 settings to HKU\.DEFAULT registry key
                /// </summary>
                public OverwriteOptions PS64_HKU_DEFAULT_Prefs = Structures.ThemeApplyingBehavior.OverwriteOptions.DontChange;

                /// <summary>
                /// <see cref="OverwriteOptions"/> to extend effects of Desktop settings (wallpaper) to HKU\.DEFAULT registry key
                /// </summary>
                public OverwriteOptions Desktop_HKU_DEFAULT = Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite;

                /// <summary>
                /// Override user preferences in Command Prompt settings
                /// </summary>
                public bool CMD_OverrideUserPreferences = true;

                /// <summary>
                /// Restore <c>imageres.dll</c> health (integrity) after restoring default startup theme sound, by launching a <c>sfc</c> scan on it.
                /// </summary>
                public bool SFC_on_restoring_StartupSound = false;

                /// <summary>
                /// Ignore PE modification alert when applying a theme and the target PE file is a system file
                /// </summary>
                public bool Ignore_PE_Modify_Alert = false;

                /// <summary>
                /// If <c>true</c> and <c>Ignore_PE_Modify_Alert == true</c>, PE modification will be done.
                /// <br></br>
                /// If <c>false</c> and <c>Ignore_PE_Modify_Alert == true</c>, PE modification will not be done.
                /// </summary>
                public bool PE_ModifyByDefault = true;

                /// <summary>
                /// Show alert when applying Windows Effects and also on changing its toggle state
                /// </summary>
                public bool Show_WinEffects_Alert = true;

                /// <summary>
                /// Create new instance of ThemeApplyingBehavior settings structure with default values
                /// </summary>
                public ThemeApplyingBehavior() { }

                /// <summary>
                /// Enumeration to define overwrite options
                /// </summary>
                public enum OverwriteOptions
                {
                    /// <summary>
                    /// Don't change registry in the target key
                    /// </summary>
                    DontChange,
                    /// <summary>
                    /// Overwrite registry in the target key
                    /// </summary>
                    Overwrite,
                    /// <summary>
                    /// Restore defaults in the target key
                    /// </summary>
                    RestoreDefaults,
                    /// <summary>
                    /// Delete registry values in the target key
                    /// </summary>
                    Erase
                }

                /// <summary>
                /// Load settings from registry
                /// </summary>
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

                /// <summary>
                /// Save settings to registry
                /// </summary>
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

            /// <summary>
            /// Appearance settings structure
            /// </summary>
            public struct Appearance
            {
                /// <summary>
                /// WinPaletter dark mode
                /// </summary>
                public bool DarkMode = true;

                /// <summary>
                /// Get dark mode from Windows settings, not WinPaletter settings
                /// </summary>
                public bool AutoDarkMode = true;

                /// <summary>
                /// Use custom settings for WinPaletter appearance
                /// </summary>
                public bool CustomColors = false;

                /// <summary>
                /// Use dark mode for custom theme
                /// </summary>
                public bool CustomTheme_DarkMode = true;

                /// <summary>
                /// WinPaletter accent color in custom theme
                /// </summary>
                public Color AccentColor = DefaultColors.PrimaryColor_Dark;

                /// <summary>
                /// WinPaletter back color in custom theme
                /// </summary>
                public Color BackColor = DefaultColors.BackColor_Dark;

                /// <summary>
                /// WinPaletter secondary color (errors colors) in custom theme
                /// </summary>
                public Color SecondaryColor = DefaultColors.SecondaryColor_Dark;

                /// <summary>
                /// WinPaletter tertiary color (tips, info colors) in custom theme
                /// </summary>
                public Color TertiaryColor = DefaultColors.TertiaryColor_Dark;

                /// <summary>
                /// WinPaletter disabled accent color in custom theme
                /// </summary>
                public Color DisabledColor = DefaultColors.DisabledColor_Dark;

                /// <summary>
                /// WinPaletter disabled back color in custom theme
                /// </summary>
                public Color DisabledBackColor = DefaultColors.DisabledBackColor_Dark;

                /// <summary>
                /// WinPaletter has rounded corners in custom theme
                /// </summary>
                public bool RoundedCorners = true;

                /// <summary>
                /// WinPaletter animations in custom theme
                /// </summary>
                public bool Animations = true;

                /// <summary>
                /// Make WinPaletter appearance managed by WinPaletter Application Theme aspect in main form
                /// </summary>
                public bool ManagedByTheme = true;

                /// <summary>
                /// Create new instance of Appearance settings structure with default values
                /// </summary>
                public Appearance() { }

                /// <summary>
                /// Load settings from registry
                /// </summary>
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

                /// <summary>
                /// Save settings to registry
                /// </summary>
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

            /// <summary>
            /// Language settings structure
            /// </summary>
            public struct Language
            {
                /// <summary>
                /// Enable using language file from <c>File</c>
                /// </summary>
                public bool Enabled = false;

                /// <summary>
                /// Language JSON file path
                /// </summary>
                public string File = string.Empty;

                /// <summary>
                /// Create new instance of Language settings structure with default values
                /// </summary>
                public Language() { }

                /// <summary>
                /// Load settings from registry
                /// </summary>
                public void Load()
                {
                    Enabled = Conversions.ToBoolean(GetReg(REG_Language, string.Empty, false));
                    File = GetReg(REG_Language, "File", string.Empty).ToString();
                }

                /// <summary>
                /// Save settings to registry
                /// </summary>
                public void Save()
                {
                    EditReg(REG_Language, string.Empty, Enabled, RegistryValueKind.DWord);
                    EditReg(REG_Language, "File", File, RegistryValueKind.String);
                }
            }

            /// <summary>
            /// ExplorerPatch settings structure
            /// </summary>
            public struct EP
            {
                /// <summary>
                /// Enable ExplorerPatcher synchronization with Windows 11 preview
                /// </summary>
                public bool Enabled = true;

                /// <summary>
                /// Enable ExplorerPatcher synchronization with Windows 11 preview even if ExplorerPatcher is not installed and Windows 11 is not detected
                /// </summary>
                public bool Enabled_Force = false;

                /// <summary>
                /// If <c>Enabled_Force == true</c>, make preview has Windows 10 Start Menu style
                /// </summary>
                public bool UseStart10 = false;

                /// <summary>
                /// If <c>Enabled_Force == true</c>, make preview has Windows 10 Taskbar style
                /// </summary>
                public bool UseTaskbar10 = false;

                /// <summary>
                /// If <c>Enabled_Force == true</c>, make preview has Windows 10 start button style
                /// </summary>
                public bool TaskbarButton10 = false;

                /// <summary>
                /// If <c>Enabled_Force == true</c>, make preview has a custom Windows 10 Start Menu style
                /// </summary>
                public ExplorerPatcher.StartStyles StartStyle = WinPaletter.ExplorerPatcher.StartStyles.NotRounded;

                /// <summary>
                /// Create new instance of ExplorerPatcher settings structure with default values
                /// </summary>
                public EP() { }

                /// <summary>
                /// Load settings from registry
                /// </summary>
                public void Load()
                {
                    Enabled = Conversions.ToBoolean(GetReg(REG_EP, string.Empty, true));
                    Enabled_Force = Conversions.ToBoolean(GetReg(REG_EP, "Enabled_Force", false));
                    UseStart10 = Conversions.ToBoolean(GetReg(REG_EP, "UseStart10", false));
                    UseTaskbar10 = Conversions.ToBoolean(GetReg(REG_EP, "UseTaskbar10", false));
                    TaskbarButton10 = Conversions.ToBoolean(GetReg(REG_EP, "TaskbarButton10", false));
                    StartStyle = (ExplorerPatcher.StartStyles)Convert.ToInt32(GetReg(REG_EP, "StartStyle", WinPaletter.ExplorerPatcher.StartStyles.NotRounded));
                }

                /// <summary>
                /// Save settings to registry
                /// </summary>
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

            /// <summary>
            /// Theme verbose log settings structure
            /// </summary>
            public struct ThemeLog
            {
                /// <summary>
                /// Level of verbose theme log
                /// </summary>
                public VerboseLevels VerboseLevel = Structures.ThemeLog.VerboseLevels.Basic;

                /// <summary>
                /// Show skipped items on detailed verbose log (<c>VerboseLevel == Structures.ThemeLog.VerboseLevels.Detailed</c>)
                /// </summary>
                public bool ShowSkippedItemsOnDetailedVerbose = false;

                /// <summary>
                /// Enable countdown before closing verbose theme log
                /// </summary>
                public bool CountDown = true;

                /// <summary>
                /// Seconds of countdown before closing verbose theme log, when <c>CountDown == true</c>
                /// </summary>
                public int CountDown_Seconds = 20;

                /// <summary>
                /// Create new instance of ThemeLog settings structure with default values
                /// </summary>
                public ThemeLog() { }

                /// <summary>
                /// Verbosity levels enumeration
                /// </summary>
                public enum VerboseLevels
                {
                    /// <summary>
                    /// No log will be previewed
                    /// </summary>
                    None,
                    /// <summary>
                    /// Basic details (theme aspect applies, skipped, errors)
                    /// </summary>
                    Basic,
                    /// <summary>
                    /// Show more details (registry values added\deleted\removed\...)
                    /// </summary>
                    Detailed
                }

                /// <summary>
                /// Theme verbose log is enabled or not 
                /// </summary>
                /// <returns></returns>
                public bool Enabled => VerboseLevel != VerboseLevels.None;

                /// <summary>
                /// Load settings from registry
                /// </summary>
                public void Load()
                {
                    VerboseLevel = (VerboseLevels)Convert.ToInt32(GetReg(REG_ThemeLog, "VerboseLevel", VerboseLevels.Basic));
                    ShowSkippedItemsOnDetailedVerbose = Conversions.ToBoolean(GetReg(REG_ThemeLog, "ShowSkippedItemsOnDetailedVerbose", false));
                    CountDown = Conversions.ToBoolean(GetReg(REG_ThemeLog, "CountDown", true));
                    CountDown_Seconds = Convert.ToInt32(GetReg(REG_ThemeLog, "CountDown_Seconds", 20));
                }

                /// <summary>
                /// Save settings to registry
                /// </summary>
                public void Save()
                {
                    EditReg(REG_ThemeLog, "VerboseLevel", VerboseLevel, RegistryValueKind.DWord);
                    EditReg(REG_ThemeLog, "ShowSkippedItemsOnDetailedVerbose", ShowSkippedItemsOnDetailedVerbose, RegistryValueKind.DWord);
                    EditReg(REG_ThemeLog, "CountDown", CountDown, RegistryValueKind.DWord);
                    EditReg(REG_ThemeLog, "CountDown_Seconds", CountDown_Seconds, RegistryValueKind.DWord);
                }
            }

            /// <summary>
            /// Windows Terminal settings structure
            /// </summary>
            public struct WindowsTerminal
            {
                /// <summary>
                /// Bypass error message that Windows Terminal is not installed and use either <c>Terminal_Stable_Path</c> or <c>Terminal_Preview_Path</c> directly
                /// </summary>
                public bool Bypass = false;

                /// <summary>
                /// List all fonts, including the non-monospaced fonts in all consoles aspects
                /// </summary>
                public bool ListAllFonts = false;

                /// <summary>
                /// Deflect Windows Terminal settings json file into another file (useful if Terminal is not installed or installed in a different path)
                /// </summary>
                public bool Path_Deflection = false;

                /// <summary>
                /// Deflected Windows Terminal settings json file path
                /// </summary>
                public string Terminal_Stable_Path = SysPaths.TerminalJSON;

                /// <summary>
                /// Deflected Windows Terminal Preview settings json file path
                /// </summary>
                public string Terminal_Preview_Path = SysPaths.TerminalPreviewJSON;

                /// <summary>
                /// Create new instance of Windows Terminal settings structure with default values
                /// </summary>
                public WindowsTerminal() { }

                /// <summary>
                /// Load settings from registry
                /// </summary>
                public void Load()
                {
                    Bypass = Conversions.ToBoolean(GetReg(REG_WindowsTerminals, "Bypass", false));
                    ListAllFonts = Conversions.ToBoolean(GetReg(REG_WindowsTerminals, "ListAllFonts", false));
                    Path_Deflection = Conversions.ToBoolean(GetReg(REG_WindowsTerminals, "Path_Deflection", false));
                    Terminal_Stable_Path = GetReg(REG_WindowsTerminals, "Terminal_Stable_Path", SysPaths.TerminalJSON).ToString();
                    Terminal_Preview_Path = GetReg(REG_WindowsTerminals, "Terminal_Preview_Path", SysPaths.TerminalPreviewJSON).ToString();
                }

                /// <summary>
                /// Save settings to registry
                /// </summary>
                public void Save()
                {
                    EditReg(REG_WindowsTerminals, "Bypass", Bypass, RegistryValueKind.DWord);
                    EditReg(REG_WindowsTerminals, "ListAllFonts", ListAllFonts, RegistryValueKind.DWord);
                    EditReg(REG_WindowsTerminals, "Path_Deflection", Path_Deflection, RegistryValueKind.DWord);
                    EditReg(REG_WindowsTerminals, "Terminal_Stable_Path", Terminal_Stable_Path, RegistryValueKind.String);
                    EditReg(REG_WindowsTerminals, "Terminal_Preview_Path", Terminal_Preview_Path, RegistryValueKind.String);
                }
            }

            /// <summary>
            /// WinPaletter Store settings structure
            /// </summary>
            public struct Store
            {
                /// <summary>
                /// Search filter (themes names)
                /// </summary>
                public bool Search_ThemeNames = true;

                /// <summary>
                /// Search filter (authors names)
                /// </summary>
                public bool Search_AuthorsNames = true;

                /// <summary>
                /// Search filter (descriptions)
                /// </summary>
                public bool Search_Descriptions = true;

                /// <summary>
                /// If <c>true</c>, WinPaletter will use online sources from <c>Online_Repositories</c> array
                /// <br></br>
                /// If <c>false</c>, WinPaletter will use offline sources from <c>Offline_Directories</c> array
                /// </summary>
                public bool Online_or_Offline = true;

                /// <summary>
                /// String array contains links to WinPaletter themes sources
                /// </summary>
                public string[] Online_Repositories = { Links.Store_MainDB, Links.Store_2ndDB };

                /// <summary>
                /// String array contains directories to WinPaletter themes sources
                /// </summary>
                public string[] Offline_Directories = { string.Empty };

                /// <summary>
                /// Get themes from subdirectories when <c>Online_or_Offline == false</c>
                /// </summary>
                public bool Offline_SubFolders = true;

                /// <summary>
                /// Show WinPaletter Store tips
                /// </summary>
                public bool ShowTips = true;

                /// <summary>
                /// Create new instance of Store settings structure with default values
                /// </summary>
                public Store() { }

                /// <summary>
                /// Load settings from registry
                /// </summary>
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

                /// <summary>
                /// Save settings to registry
                /// </summary>
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

            /// <summary>
            /// Color info settings structure
            /// </summary>
            public struct NerdStats
            {
                /// <summary>
                /// Enable showing color info in a color picking control
                /// </summary>
                public bool Enabled = true;

                /// <summary>
                /// Color label format that is shown as string in a color picking control
                /// </summary>
                public Formats Type = Structures.NerdStats.Formats.HEX;

                /// <summary>
                /// Show hash (#) before color hex code if <c>true</c> and <c>Type == Structures.NerdStats.Formats.HEX</c>
                /// </summary>
                public bool ShowHexHash = true;

                /// <summary>
                /// Make color info label more transparent
                /// </summary>
                public bool MoreLabelTransparency = false;

                /// <summary>
                /// Use default Windows monospaced font for color info label instead of JetbrainsMono
                /// </summary>
                public bool UseWindowsMonospacedFont = false;

                /// <summary>
                /// Show a dot indicator (small circle) if the color is changed from default
                /// </summary>
                public bool DotDefaultChangedIndicator = true;

                /// <summary>
                /// Enable drag and drop color picking between different color picking controls
                /// </summary>
                public bool DragAndDrop = true;

                /// <summary>
                /// Use classic Windows color picker instead WinPaletter's color picker
                /// </summary>
                public bool Classic_Color_Picker = false;

                /// <summary>
                /// Create new instance of NerdStats settings structure with default values
                /// </summary>
                public NerdStats() { }

                /// <summary>
                /// Color label formats enumeration
                /// </summary>
                public enum Formats
                {
                    /// <summary>
                    /// Hexadecimal color code
                    /// </summary>
                    HEX,
                    /// <summary>
                    /// Red, Green, Blue color code
                    /// </summary>
                    RGB,
                    /// <summary>
                    /// Hue, Saturation, Lightness color code
                    /// </summary>
                    HSL,
                    /// <summary>
                    /// Decimal color code
                    /// </summary>
                    Dec
                }

                /// <summary>
                /// Load settings from registry
                /// </summary>
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

                /// <summary>
                /// Save settings to registry
                /// </summary>
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

            /// <summary>
            /// Users and services settings structure
            /// </summary>
            public struct UsersServices
            {
                /// <summary>
                /// Show WinPaletter System Events Sounds Installer dialog when installing or updating it
                /// </summary>
                public bool ShowSysEventsSoundsInstaller = true;

                /// <summary>
                /// Create new instance of UsersServices settings structure with default values
                /// </summary>
                public UsersServices() { }

                /// <summary>
                /// Load settings from registry
                /// </summary>
                public void Load()
                {
                    ShowSysEventsSoundsInstaller = Conversions.ToBoolean(GetReg(REG_UsersServices, "ShowSysEventsSoundsInstaller", true));
                }

                /// <summary>
                /// Save settings to registry
                /// </summary>
                public void Save()
                {
                    EditReg(REG_UsersServices, "ShowSysEventsSoundsInstaller", ShowSysEventsSoundsInstaller, RegistryValueKind.DWord);
                }
            }

            /// <summary>
            /// Miscellaneous settings structure
            /// </summary>
            public struct Miscellaneous
            {
                /// <summary>
                /// Change Windows 7/8.1 DWM colors in real-time with changing values in WinPaletter
                /// </summary>
                public bool Win7LivePreview = true;

                /// <summary>
                /// Show welcome dialog on WinPaletter startup
                /// </summary>
                public bool ShowWelcomeDialog = true;

                /// <summary>
                /// Create new instance of Miscellaneous settings structure with default values
                /// </summary>
                public Miscellaneous() { }

                /// <summary>
                /// Load settings from registry
                /// </summary>
                public void Load()
                {
                    Win7LivePreview = Conversions.ToBoolean(GetReg(REG_Miscellaneous, "Win7LivePreview", true));
                    ShowWelcomeDialog = Conversions.ToBoolean(GetReg(REG_Miscellaneous, "ShowWelcomeDialog", true));
                }

                /// <summary>
                /// Save settings to registry
                /// </summary>
                public void Save()
                {
                    EditReg(REG_Miscellaneous, "Win7LivePreview", Win7LivePreview, RegistryValueKind.DWord);
                    EditReg(REG_Miscellaneous, "ShowWelcomeDialog", ShowWelcomeDialog, RegistryValueKind.DWord);
                }
            }

            /// <summary>
            /// WinPaletter themes backup settings structure
            /// </summary>
            public struct BackupTheme
            {
                /// <summary>
                /// Enable WinPaletter themes backup
                /// </summary>
                public bool Enabled = true;

                /// <summary>
                /// Automatically backup theme when WinPaletter is opened
                /// </summary>
                public bool AutoBackupOnAppOpen = false;

                /// <summary>
                /// Automatically backup current theme before applying a new one
                /// </summary>
                public bool AutoBackupOnApply = true;

                /// <summary>
                /// Automatically backup current theme before applying a single aspect
                /// </summary>
                public bool AutoBackupOnApplySingleAspect = true;

                /// <summary>
                /// Automatically backup current theme before opening a new one from open file dialog
                /// </summary>
                public bool AutoBackupOnThemeLoad = false;

                /// <summary>
                /// Directory containing WinPaletter themes backups
                /// </summary>
                public string BackupPath = $"{SysPaths.appData}\\Backup\\Themes";

                /// <summary>
                /// Create new instance of BackupTheme settings structure with default values
                /// </summary>
                public BackupTheme() { }

                /// <summary>
                /// Load settings from registry
                /// </summary>
                public void Load()
                {
                    Enabled = Conversions.ToBoolean(GetReg(REG_Backup, "Enabled", true));
                    AutoBackupOnAppOpen = Conversions.ToBoolean(GetReg(REG_Backup, "AutoBackupOnAppOpen", false));
                    AutoBackupOnApply = Conversions.ToBoolean(GetReg(REG_Backup, "AutoBackupOnApply", true));
                    AutoBackupOnApplySingleAspect = Conversions.ToBoolean(GetReg(REG_Backup, "AutoBackupOnApplySingleAspect", true));
                    AutoBackupOnThemeLoad = Conversions.ToBoolean(GetReg(REG_Backup, "AutoBackupOnThemeLoad", false));
                    BackupPath = GetReg(REG_Backup, "BackupPath", $"{SysPaths.appData}\\Backup\\Themes").ToString();
                }

                /// <summary>
                /// Save settings to registry
                /// </summary>
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

            /// <summary>
            /// Aspects control settings structure
            /// </summary>
            public struct AspectsControl
            {
                /// <summary>
                /// Enable strict control over WinPaletter aspects applying (to avoid applying unwanted aspects)
                /// </summary>
                public bool Enabled = false;

                /// <summary>
                /// If <c>false</c>, Windows Colors won't be applied at all
                /// </summary>
                public bool WinColors = true;

                /// <summary>
                /// If <c>false</c>, Windows Visual Styles won't be applied at all
                /// </summary>
                public bool VisualStyles = true;

                /// <summary>
                /// If <c>true</c>, Windows Colors aspect form will be opened in advanced mode
                /// <br></br>
                /// If <c>false</c>, Windows Colors aspect form will be opened in simple mode
                /// </summary>
                public bool WinColors_Advanced = true;

                /// <summary>
                /// If <c>false</c>, LogonUI won't be applied at all
                /// </summary>
                public bool LogonUI = true;

                /// <summary>
                /// If <c>false</c>, Classic Colors won't be applied at all
                /// </summary>
                public bool ClassicColors = true;

                /// <summary>
                /// If <c>true</c>, Classic Colors aspect form will be opened in advanced mode
                /// <br></br>
                /// If <c>false</c>, Classic Colors aspect form will be opened in simple mode
                /// </summary>
                public bool ClassicColors_Advanced = true;

                /// <summary>
                /// If <c>false</c>, Metrics and Fonts won't be applied at all
                /// </summary>
                public bool MetricsFonts = true;

                /// <summary>
                /// If <c>true</c>, Metrics and Fonts aspect form will be opened in advanced mode
                /// <br></br>
                /// If <c>false</c>, Metrics and Fonts aspect form will be opened in simple mode
                /// </summary>
                public bool MetricsFonts_Advanced = true;

                /// <summary>
                /// If <c>false</c>, Cursors won't be applied at all
                /// </summary>
                public bool Cursors = true;

                /// <summary>
                /// If <c>true</c>, Cursors aspect form will be opened in advanced mode
                /// <br></br>
                /// If <c>false</c>, Cursors aspect form will be opened in simple mode
                /// </summary>
                public bool Cursors_Advanced = true;

                /// <summary>
                /// If <c>false</c>, Consoles won't be applied at all
                /// </summary>
                public bool Consoles = true;

                /// <summary>
                /// If <c>false</c>, Windows Terminals won't be applied at all
                /// </summary>
                public bool WinTerminals = true;

                /// <summary>
                /// If <c>false</c>, Wallpaper won't be applied at all
                /// </summary>
                public bool Wallpaper = true;

                /// <summary>
                /// If <c>true</c>, Wallpaper aspect form will be opened in advanced mode
                /// <br></br>
                /// If <c>false</c>, Wallpaper aspect form will be opened in simple mode
                /// </summary>
                public bool Wallpaper_Advanced = true;

                /// <summary>
                /// If <c>false</c>, Windows Effects won't be applied at all
                /// </summary>
                public bool Effects = true;

                /// <summary>
                /// If <c>false</c>, Sounds won't be applied at all
                /// </summary>
                public bool Sounds = true;

                /// <summary>
                /// If <c>false</c>, Screen Saver won't be applied at all
                /// </summary>
                public bool ScreenSaver = true;

                /// <summary>
                /// If <c>false</c>, Windows Switcher (Alt+Tab appearance) won't be applied at all
                /// </summary>
                public bool AltTab = true;

                /// <summary>
                /// If <c>false</c>, Windows Icons won't be applied at all
                /// </summary>
                public bool Icons = true;

                /// <summary>
                /// Create new instance of AspectsControl settings structure with default values
                /// </summary>
                public AspectsControl() { }

                /// <summary>
                /// Load settings from registry
                /// </summary>
                public void Load()
                {
                    Enabled = Conversions.ToBoolean(GetReg(REG_AspectsControl, string.Empty, false));
                    WinColors = Conversions.ToBoolean(GetReg(REG_AspectsControl, "WinColors", true));
                    WinColors_Advanced = Conversions.ToBoolean(GetReg(REG_AspectsControl, "WinColors_Advanced", true));
                    VisualStyles = Conversions.ToBoolean(GetReg(REG_AspectsControl, "VisualStyles", true));
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

                /// <summary>
                /// Save settings to registry
                /// </summary>
                public void Save()
                {
                    EditReg(REG_AspectsControl, string.Empty, Enabled, RegistryValueKind.DWord);
                    EditReg(REG_AspectsControl, "WinColors", WinColors, RegistryValueKind.DWord);
                    EditReg(REG_AspectsControl, "WinColors_Advanced", WinColors_Advanced, RegistryValueKind.DWord);
                    EditReg(REG_AspectsControl, "VisualStyles", VisualStyles, RegistryValueKind.DWord);
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

        /// <summary>
        /// General settings that are not visible through settings form
        /// </summary>
        public Structures.General General = new();

        /// <summary>
        /// Updates settings
        /// </summary>
        public Structures.Updates Updates = new();

        /// <summary>
        /// Settings related to WinPaletter file types (*.WPTH, *.WPSF) management and registration
        /// </summary>
        public Structures.FileTypeMgr FileTypeManagement = new();

        /// <summary>
        /// Settings related to theme application behavior (such as how WinPaletter behaves in different situations, automatic restart of Explorer, etc.).
        /// </summary>
        public Structures.ThemeApplyingBehavior ThemeApplyingBehavior = new();

        /// <summary>
        /// Settings related to WinPaletter appearance (dark mode, accent color, etc.)
        /// </summary>
        public Structures.Appearance Appearance = new();

        /// <summary>
        /// Language settings
        /// </summary>
        public Structures.Language Language = new();

        /// <summary>
        /// Settings related to ExplorerPatcher preview synchronization.
        /// </summary>
        public Structures.EP ExplorerPatcher = new();

        /// <summary>
        /// Theme verbose log settings
        /// </summary>
        public Structures.ThemeLog ThemeLog = new();

        /// <summary>
        /// Settings related to Windows Terminals, such as json files path, etc.
        /// </summary>
        public Structures.WindowsTerminal WindowsTerminals = new();

        /// <summary>
        /// WinPaletter Store settings
        /// </summary>
        public Structures.Store Store = new();

        /// <summary>
        /// Settings related to color items. (Note: "NerdStats" is an old and obsolete name, and is no longer relevant.)
        /// </summary>
        public Structures.NerdStats NerdStats = new();

        /// <summary>
        /// Settings related to users and WinPaletter service/s
        /// </summary>
        public Structures.UsersServices UsersServices = new();

        /// <summary>
        /// Miscellaneous settings
        /// </summary>
        public Structures.Miscellaneous Miscellaneous = new();

        /// <summary>
        /// WinPaletter themes backup settings
        /// </summary>
        public Structures.BackupTheme BackupTheme = new();

        /// <summary>
        /// Aspects control settings (secure locks)
        /// </summary>
        public Structures.AspectsControl AspectsControl = new();

        /// <summary>
        /// Source from/into which WinPalette settings are loaded/saved
        /// </summary>
        public enum Source
        {
            /// <summary>
            /// Windows Registry
            /// </summary>
            Registry,
            /// <summary>
            /// JSON file
            /// </summary>
            File,
            /// <summary>
            /// Empty settings
            /// </summary>
            Empty
        }

        /// <summary>
        /// Create a new instance of WinPaletter settings
        /// </summary>
        /// <param name="source"></param>
        /// <param name="file"></param>
        public Settings(Source source, string file = null)
        {
            switch (source)
            {
                case Source.Registry:
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

                case Source.File:
                    if (System.IO.File.Exists(file))
                    {
                        string[] txt = System.IO.File.ReadAllLines(file);

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

        /// <summary>
        /// Save WinPaletter settings
        /// </summary>
        /// <param name="destination"></param>
        /// <param name="file"></param>
        public void Save(Source destination, string file = null)
        {
            switch (destination)
            {
                case Source.Registry:
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

                case Source.File:
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

                    System.IO.File.WriteAllText(file, ToString());
                    break;
            }
        }

        /// <summary>
        /// Returns WinPaletter settings as a JSON string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            JObject JSON_Overall = new();
            JSON_Overall.RemoveAll();

            foreach (FieldInfo field in GetType().GetFields(bindingFlags))
            {
                if (field.Name.Trim().ToUpper() != "GENERAL")
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
    }
}