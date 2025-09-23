using Microsoft.VisualBasic.CompilerServices;
using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Drawing;
using System.IO;
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
        private readonly BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public;

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
            static readonly string REG_AppLog = $"{REG}\\AppLog";
            static readonly string REG_WindowsTerminals = $"{REG}\\WindowsTerminals";
            static readonly string REG_Store = $"{REG}\\Store";
            static readonly string REG_NerdStats = $"{REG}\\NerdStats";
            static readonly string REG_Miscellaneous = $"{REG}\\Miscellaneous";
            static readonly string REG_Backup = $"{REG}\\Backup";
            static readonly string REG_AspectsControl = $"{REG}\\AspectsControl";
            #endregion

            /// <summary>
            /// General settings structure
            /// </summary>
            public class General
            {
                /// <summary>
                /// A flag to determine if the setup has been completed or not.
                /// </summary>
                public bool SetupCompleted = false;

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
                public string[] WhatsNewRecord = [string.Empty];

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
                    SetupCompleted = Conversions.ToBoolean(ReadReg(REG_General, "SetupCompleted", false));
                    WhatsNewRecord = (string[])ReadReg(REG_General, "WhatsNewRecord", new[] { string.Empty });
                    MainFormWidth = ReadReg(REG_General_MainForm, "MainFormWidth", 1110);
                    MainFormHeight = ReadReg(REG_General_MainForm, "MainFormHeight", 725);
                    MainFormStatus = ReadReg(REG_General_MainForm, "MainFormStatus", FormWindowState.Normal);
                    CompactAspects = Conversions.ToBoolean(ReadReg(REG_General, "CompactAspects", false));
                }

                /// <summary>
                /// Save settings to registry
                /// </summary>
                public void Save()
                {
                    WriteReg(REG_General, "SetupCompleted", SetupCompleted, RegistryValueKind.DWord);
                    WriteReg(REG_General, "WhatsNewRecord", WhatsNewRecord, RegistryValueKind.MultiString);
                    WriteReg(REG_General_MainForm, "MainFormWidth", MainFormWidth, RegistryValueKind.DWord);
                    WriteReg(REG_General_MainForm, "MainFormHeight", MainFormHeight, RegistryValueKind.DWord);
                    WriteReg(REG_General_MainForm, "MainFormStatus", MainFormStatus, RegistryValueKind.DWord);
                    WriteReg(REG_General, "CompactAspects", CompactAspects, RegistryValueKind.DWord);
                }
            }

            /// <summary>
            /// Updates structure
            /// </summary>
            public class Updates
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
                    AutoCheck = Conversions.ToBoolean(ReadReg(REG_Updates, "AutoCheck", true));
                    Channel = (Channels)ReadReg(REG_Updates, "Channel", Channels.Stable) == Channels.Stable ? Channels.Stable : Channels.Beta;
                }

                /// <summary>
                /// Save settings to registry
                /// </summary>
                public void Save()
                {
                    WriteReg(REG_Updates, "AutoCheck", AutoCheck, RegistryValueKind.DWord);
                    WriteReg(REG_Updates, "Channel", Channel == Channels.Stable ? 0 : 1);
                }
            }

            /// <summary>
            /// File type management settings structure
            /// </summary>
            public class FileTypeMgr
            {
                /// <summary>
                /// Automatic add File extension to theme files, settings files, and theme resources pack files at WinPaletter startup
                /// </summary>
                public bool AutoAddExt = true;

                /// <summary>
                /// If <c>true</c>, opening a theme preview File from Windows explorer will open it in WinPaletter.
                /// <br></br>
                /// If <c>false</c>, opening a theme preview File from Windows explorer will apply it without loading WinPaletter GUI.
                /// </summary>
                public bool OpeningPreviewInApp_or_AppliesIt = true;

                /// <summary>
                /// Compress theme File json content when saving it
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
                    AutoAddExt = Conversions.ToBoolean(ReadReg(REG_FileTypeManagement, "AutoAddExt", true));
                    OpeningPreviewInApp_or_AppliesIt = Conversions.ToBoolean(ReadReg(REG_FileTypeManagement, "OpeningPreviewInApp_or_AppliesIt", true));
                    CompressThemeFile = Conversions.ToBoolean(ReadReg(REG_FileTypeManagement, "CompressThemeFile", true));
                }

                /// <summary>
                /// Save settings to registry
                /// </summary>
                public void Save()
                {
                    WriteReg(REG_FileTypeManagement, "AutoAddExt", AutoAddExt, RegistryValueKind.DWord);
                    WriteReg(REG_FileTypeManagement, "OpeningPreviewInApp_or_AppliesIt", OpeningPreviewInApp_or_AppliesIt, RegistryValueKind.DWord);
                    WriteReg(REG_FileTypeManagement, "CompressThemeFile", CompressThemeFile, RegistryValueKind.DWord);
                }
            }

            /// <summary>
            /// WinTheme applying behavior settings structure
            /// </summary>
            public class ThemeApplyingBehavior
            {
                /// <summary>
                /// Automatically create a system restore point before applying theme
                /// </summary>
                public bool CreateSystemRestore = true;

                /// <summary>
                /// Automatically restart Windows Explorer after applying a theme
                /// </summary>
                public bool AutoRestartExplorer = true;

                /// <summary>
                /// Hide save confirmation dialog after closing WinPaletter
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
                /// Ignore PE modification alert when applying a theme and the target PE File is a system File
                /// </summary>
                public bool Ignore_PE_Modify_Alert = false;

                /// <summary>
                /// If <c>true</c> and <c>Ignore_PE_Modify_Alert == true</c>, PE modification will be done.
                /// <br></br>
                /// If <c>false</c> and <c>Ignore_PE_Modify_Alert == true</c>, PE modification will not be done.
                /// </summary>
                public bool PE_ModifyByDefault = true;

                /// <summary>
                /// Hide alert when applying Windows Effects and also on changing its toggle state
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
                    CreateSystemRestore = Conversions.ToBoolean(ReadReg(REG_ThemeApplyingBehavior, "CreateSystemRestore", true));
                    AutoRestartExplorer = Conversions.ToBoolean(ReadReg(REG_ThemeApplyingBehavior, "AutoRestartExplorer", true));
                    ShowSaveConfirmation = Conversions.ToBoolean(ReadReg(REG_ThemeApplyingBehavior, "ShowSaveConfirmation", true));
                    ClassicColors_HKU_DEFAULT_Prefs = (OverwriteOptions)Convert.ToInt32(ReadReg(REG_ThemeApplyingBehavior, "ClassicColors_HKU_DEFAULT_Prefs", OverwriteOptions.Overwrite));
                    ClassicColors_HKLM_Prefs = (OverwriteOptions)Convert.ToInt32(ReadReg(REG_ThemeApplyingBehavior, "ClassicColors_HKLM_Prefs", OverwriteOptions.Erase));
                    UPM_HKU_DEFAULT = Conversions.ToBoolean(ReadReg(REG_ThemeApplyingBehavior, "UPM_HKU_DEFAULT", false));
                    Metrics_HKU_DEFAULT_Prefs = (OverwriteOptions)Convert.ToInt32(ReadReg(REG_ThemeApplyingBehavior, "Metrics_HKU_DEFAULT_Prefs", OverwriteOptions.DontChange));
                    Cursors_HKU_DEFAULT_Prefs = (OverwriteOptions)Convert.ToInt32(ReadReg(REG_ThemeApplyingBehavior, "Cursors_HKU_DEFAULT_Prefs", OverwriteOptions.DontChange));
                    CMD_HKU_DEFAULT_Prefs = (OverwriteOptions)Convert.ToInt32(ReadReg(REG_ThemeApplyingBehavior, "CMD_HKU_DEFAULT_Prefs", OverwriteOptions.DontChange));
                    PS86_HKU_DEFAULT_Prefs = (OverwriteOptions)Convert.ToInt32(ReadReg(REG_ThemeApplyingBehavior, "PS86_HKU_DEFAULT_Prefs", OverwriteOptions.DontChange));
                    PS64_HKU_DEFAULT_Prefs = (OverwriteOptions)Convert.ToInt32(ReadReg(REG_ThemeApplyingBehavior, "PS64_HKU_DEFAULT_Prefs", OverwriteOptions.DontChange));
                    Desktop_HKU_DEFAULT = (OverwriteOptions)Convert.ToInt32(ReadReg(REG_ThemeApplyingBehavior, "Desktop_HKU_DEFAULT", OverwriteOptions.DontChange));
                    CMD_OverrideUserPreferences = Conversions.ToBoolean(ReadReg(REG_ThemeApplyingBehavior, "CMD_OverrideUserPreferences", true));
                    ResetCursorsToAero = Conversions.ToBoolean(ReadReg(REG_ThemeApplyingBehavior, "ResetCursorsToAero", OS.WXP));
                    SFC_on_restoring_StartupSound = Conversions.ToBoolean(ReadReg(REG_ThemeApplyingBehavior, "SFC_on_restoring_StartupSound", false));
                    Ignore_PE_Modify_Alert = Conversions.ToBoolean(ReadReg(REG_ThemeApplyingBehavior, "Ignore_PE_Modify_Alert", false));
                    Show_WinEffects_Alert = Conversions.ToBoolean(ReadReg(REG_ThemeApplyingBehavior, "Show_WinEffects_Alert", true));
                    PE_ModifyByDefault = Conversions.ToBoolean(ReadReg(REG_ThemeApplyingBehavior, "PE_ModifyByDefault", true));
                }

                /// <summary>
                /// Save settings to registry
                /// </summary>
                public void Save()
                {
                    WriteReg(REG_ThemeApplyingBehavior, "CreateSystemRestore", CreateSystemRestore, RegistryValueKind.DWord);
                    WriteReg(REG_ThemeApplyingBehavior, "AutoRestartExplorer", AutoRestartExplorer, RegistryValueKind.DWord);
                    WriteReg(REG_ThemeApplyingBehavior, "ShowSaveConfirmation", ShowSaveConfirmation, RegistryValueKind.DWord);
                    WriteReg(REG_ThemeApplyingBehavior, "ClassicColors_HKU_DEFAULT_Prefs", ClassicColors_HKU_DEFAULT_Prefs, RegistryValueKind.DWord);
                    WriteReg(REG_ThemeApplyingBehavior, "ClassicColors_HKLM_Prefs", ClassicColors_HKLM_Prefs, RegistryValueKind.DWord);
                    WriteReg(REG_ThemeApplyingBehavior, "UPM_HKU_DEFAULT", UPM_HKU_DEFAULT, RegistryValueKind.DWord);
                    WriteReg(REG_ThemeApplyingBehavior, "Metrics_HKU_DEFAULT_Prefs", Metrics_HKU_DEFAULT_Prefs, RegistryValueKind.DWord);
                    WriteReg(REG_ThemeApplyingBehavior, "Cursors_HKU_DEFAULT_Prefs", Cursors_HKU_DEFAULT_Prefs, RegistryValueKind.DWord);
                    WriteReg(REG_ThemeApplyingBehavior, "CMD_HKU_DEFAULT_Prefs", CMD_HKU_DEFAULT_Prefs, RegistryValueKind.DWord);
                    WriteReg(REG_ThemeApplyingBehavior, "PS86_HKU_DEFAULT_Prefs", PS86_HKU_DEFAULT_Prefs, RegistryValueKind.DWord);
                    WriteReg(REG_ThemeApplyingBehavior, "PS64_HKU_DEFAULT_Prefs", PS64_HKU_DEFAULT_Prefs, RegistryValueKind.DWord);
                    WriteReg(REG_ThemeApplyingBehavior, "Desktop_HKU_DEFAULT", Desktop_HKU_DEFAULT, RegistryValueKind.DWord);
                    WriteReg(REG_ThemeApplyingBehavior, "ResetCursorsToAero", ResetCursorsToAero, RegistryValueKind.DWord);
                    WriteReg(REG_ThemeApplyingBehavior, "CMD_OverrideUserPreferences", CMD_OverrideUserPreferences, RegistryValueKind.DWord);
                    WriteReg(REG_ThemeApplyingBehavior, "SFC_on_restoring_StartupSound", SFC_on_restoring_StartupSound, RegistryValueKind.DWord);
                    WriteReg(REG_ThemeApplyingBehavior, "Ignore_PE_Modify_Alert", Ignore_PE_Modify_Alert, RegistryValueKind.DWord);
                    WriteReg(REG_ThemeApplyingBehavior, "Show_WinEffects_Alert", Show_WinEffects_Alert, RegistryValueKind.DWord);
                    WriteReg(REG_ThemeApplyingBehavior, "PE_ModifyByDefault", PE_ModifyByDefault, RegistryValueKind.DWord);
                }
            }

            /// <summary>
            /// Appearance settings structure
            /// </summary>
            public class Appearance
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
                /// Make WinPaletter appearance managed by WinPaletter Application WinTheme aspect in main form
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
                    DarkMode = Conversions.ToBoolean(ReadReg(REG_Appearance, "DarkMode", true));
                    Animations = Conversions.ToBoolean(ReadReg(REG_Appearance, "Animations", true));
                    AutoDarkMode = Conversions.ToBoolean(ReadReg(REG_Appearance, "AutoDarkMode", true));
                    CustomColors = Conversions.ToBoolean(ReadReg(REG_Appearance, "CustomColors", false));
                    CustomTheme_DarkMode = Conversions.ToBoolean(ReadReg(REG_Appearance, "CustomTheme", true));
                    AccentColor = Color.FromArgb(Convert.ToInt32(ReadReg(REG_Appearance, "AccentColor", DefaultColors.PrimaryColor_Dark.ToArgb())));
                    BackColor = Color.FromArgb(Convert.ToInt32(ReadReg(REG_Appearance, "BackColor", DefaultColors.BackColor_Dark.ToArgb())));
                    SecondaryColor = Color.FromArgb(Convert.ToInt32(ReadReg(REG_Appearance, "SecondaryColor", DefaultColors.SecondaryColor_Dark.ToArgb())));
                    TertiaryColor = Color.FromArgb(Convert.ToInt32(ReadReg(REG_Appearance, "TertiaryColor", DefaultColors.TertiaryColor_Dark.ToArgb())));
                    DisabledColor = Color.FromArgb(Convert.ToInt32(ReadReg(REG_Appearance, "DisabledColor", DefaultColors.DisabledColor_Dark.ToArgb())));
                    DisabledBackColor = Color.FromArgb(Convert.ToInt32(ReadReg(REG_Appearance, "DisabledBackColor", DefaultColors.DisabledBackColor_Dark.ToArgb())));
                    RoundedCorners = Conversions.ToBoolean(ReadReg(REG_Appearance, "RoundedCorners", true));
                    ManagedByTheme = Conversions.ToBoolean(ReadReg(REG_Appearance, "ManagedByTheme", true));
                }

                /// <summary>
                /// Save settings to registry
                /// </summary>
                public void Save()
                {
                    WriteReg(REG_Appearance, "DarkMode", DarkMode, RegistryValueKind.DWord);
                    WriteReg(REG_Appearance, "Animations", Animations, RegistryValueKind.DWord);
                    WriteReg(REG_Appearance, "AutoDarkMode", AutoDarkMode, RegistryValueKind.DWord);
                    WriteReg(REG_Appearance, "CustomColors", CustomColors, RegistryValueKind.DWord);
                    WriteReg(REG_Appearance, "CustomTheme", CustomTheme_DarkMode, RegistryValueKind.DWord);
                    WriteReg(REG_Appearance, "AccentColor", AccentColor.ToArgb(), RegistryValueKind.DWord);
                    WriteReg(REG_Appearance, "BackColor", BackColor.ToArgb(), RegistryValueKind.DWord);
                    WriteReg(REG_Appearance, "SecondaryColor", SecondaryColor.ToArgb(), RegistryValueKind.DWord);
                    WriteReg(REG_Appearance, "TertiaryColor", TertiaryColor.ToArgb(), RegistryValueKind.DWord);
                    WriteReg(REG_Appearance, "DisabledColor", DisabledColor.ToArgb(), RegistryValueKind.DWord);
                    WriteReg(REG_Appearance, "DisabledBackColor", DisabledBackColor.ToArgb(), RegistryValueKind.DWord);
                    WriteReg(REG_Appearance, "RoundedCorners", RoundedCorners, RegistryValueKind.DWord);
                    WriteReg(REG_Appearance, "ManagedByTheme", ManagedByTheme, RegistryValueKind.DWord);
                }
            }

            /// <summary>
            /// Language settings structure
            /// </summary>
            public class Language
            {
                /// <summary>
                /// Enable using language File from <c>File</c>
                /// </summary>
                public bool Enabled = false;

                /// <summary>
                /// Language JSON File path
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
                    Enabled = Conversions.ToBoolean(ReadReg(REG_Language, string.Empty, false));
                    File = ReadReg(REG_Language, "File", string.Empty).ToString();
                }

                /// <summary>
                /// Save settings to registry
                /// </summary>
                public void Save()
                {
                    WriteReg(REG_Language, string.Empty, Enabled, RegistryValueKind.DWord);
                    WriteReg(REG_Language, "File", File, RegistryValueKind.String);
                }
            }

            /// <summary>
            /// ExplorerPatch settings structure
            /// </summary>
            public class EP
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
                    Enabled = Conversions.ToBoolean(ReadReg(REG_EP, string.Empty, true));
                    Enabled_Force = Conversions.ToBoolean(ReadReg(REG_EP, "Enabled_Force", false));
                    UseStart10 = Conversions.ToBoolean(ReadReg(REG_EP, "UseStart10", false));
                    UseTaskbar10 = Conversions.ToBoolean(ReadReg(REG_EP, "UseTaskbar10", false));
                    TaskbarButton10 = Conversions.ToBoolean(ReadReg(REG_EP, "TaskbarButton10", false));
                    StartStyle = (ExplorerPatcher.StartStyles)Convert.ToInt32(ReadReg(REG_EP, "StartStyle", WinPaletter.ExplorerPatcher.StartStyles.NotRounded));
                }

                /// <summary>
                /// Save settings to registry
                /// </summary>
                public void Save()
                {
                    WriteReg(REG_EP, string.Empty, Enabled, RegistryValueKind.DWord);
                    WriteReg(REG_EP, "Enabled_Force", Enabled_Force, RegistryValueKind.DWord);
                    WriteReg(REG_EP, "UseStart10", UseStart10, RegistryValueKind.DWord);
                    WriteReg(REG_EP, "UseTaskbar10", UseTaskbar10, RegistryValueKind.DWord);
                    WriteReg(REG_EP, "TaskbarButton10", TaskbarButton10, RegistryValueKind.DWord);
                    WriteReg(REG_EP, "StartStyle", StartStyle, RegistryValueKind.DWord);
                }
            }

            /// <summary>
            /// WinTheme verbose log settings structure
            /// </summary>
            public class ThemeLog
            {
                /// <summary>
                /// Level of verbose theme log
                /// </summary>
                public VerboseLevels VerboseLevel = Structures.ThemeLog.VerboseLevels.Basic;

                /// <summary>
                /// Hide skipped items on detailed verbose log (<c>VerboseLevel == Structures.ThemeLog.VerboseLevels.Detailed</c>)
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
                    /// Hide more details (registry values added\deleted\removed\...)
                    /// </summary>
                    Detailed
                }

                /// <summary>
                /// WinTheme verbose log is enabled or not 
                /// </summary>
                /// <returns></returns>
                public bool Enabled => VerboseLevel != VerboseLevels.None;

                /// <summary>
                /// Load settings from registry
                /// </summary>
                public void Load()
                {
                    VerboseLevel = (VerboseLevels)Convert.ToInt32(ReadReg(REG_ThemeLog, "VerboseLevel", VerboseLevels.Basic));
                    ShowSkippedItemsOnDetailedVerbose = Conversions.ToBoolean(ReadReg(REG_ThemeLog, "ShowSkippedItemsOnDetailedVerbose", false));
                    CountDown = Conversions.ToBoolean(ReadReg(REG_ThemeLog, "CountDown", true));
                    CountDown_Seconds = Convert.ToInt32(ReadReg(REG_ThemeLog, "CountDown_Seconds", 20));
                }

                /// <summary>
                /// Save settings to registry
                /// </summary>
                public void Save()
                {
                    WriteReg(REG_ThemeLog, "VerboseLevel", VerboseLevel, RegistryValueKind.DWord);
                    WriteReg(REG_ThemeLog, "ShowSkippedItemsOnDetailedVerbose", ShowSkippedItemsOnDetailedVerbose, RegistryValueKind.DWord);
                    WriteReg(REG_ThemeLog, "CountDown", CountDown, RegistryValueKind.DWord);
                    WriteReg(REG_ThemeLog, "CountDown_Seconds", CountDown_Seconds, RegistryValueKind.DWord);
                }
            }

            public class AppLog
            {
                public AppLog() { }

                /// <summary>
                /// Indicates whether the feature is enabled.
                /// </summary>
                public bool Enabled = true;

                /// <summary>
                /// Indicates whether logging registry operations are enabled.
                /// </summary>
                public bool Reg = true;

                /// <summary>
                /// Indicates whether logging registry reading operations are enabled.
                /// </summary>
                public bool RegRead = true;

                /// <summary>
                /// Indicates whether logging registry write operations are enabled.
                /// </summary>
                public bool RegWrite = true;

                /// <summary>
                /// Indicates whether logging registry deletion operations are enabled.
                /// </summary>
                public bool RegDelete = true;

                public void Load()
                {
                    Enabled = Conversions.ToBoolean(ReadReg(REG_AppLog, string.Empty, true));
                    Reg = Conversions.ToBoolean(ReadReg(REG_AppLog, "Reg", true));
                    RegRead = Conversions.ToBoolean(ReadReg(REG_AppLog, "RegRead", true));
                    RegWrite = Conversions.ToBoolean(ReadReg(REG_AppLog, "RegWrite", true));
                    RegDelete = Conversions.ToBoolean(ReadReg(REG_AppLog, "RegDelete", true));
                }

                public void Save()
                {
                    WriteReg(REG_AppLog, string.Empty, Enabled, RegistryValueKind.DWord);
                    WriteReg(REG_AppLog, "Reg", Reg, RegistryValueKind.DWord);
                    WriteReg(REG_AppLog, "RegRead", RegRead, RegistryValueKind.DWord);
                    WriteReg(REG_AppLog, "RegWrite", RegWrite, RegistryValueKind.DWord);
                    WriteReg(REG_AppLog, "RegDelete", RegDelete, RegistryValueKind.DWord);
                }
            }

            /// <summary>
            /// Windows Terminal settings structure
            /// </summary>
            public class WindowsTerminal
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
                /// Deflect Windows Terminal settings json File into another File (useful if Terminal is not installed or installed in a different path)
                /// </summary>
                public bool Path_Deflection = false;

                /// <summary>
                /// Deflected Windows Terminal settings json File path
                /// </summary>
                public string Terminal_Stable_Path = SysPaths.TerminalJSON;

                /// <summary>
                /// Deflected Windows Terminal Preview settings json File path
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
                    Bypass = Conversions.ToBoolean(ReadReg(REG_WindowsTerminals, "Bypass", false));
                    ListAllFonts = Conversions.ToBoolean(ReadReg(REG_WindowsTerminals, "ListAllFonts", false));
                    Path_Deflection = Conversions.ToBoolean(ReadReg(REG_WindowsTerminals, "Path_Deflection", false));
                    Terminal_Stable_Path = ReadReg(REG_WindowsTerminals, "Terminal_Stable_Path", SysPaths.TerminalJSON).ToString();
                    Terminal_Preview_Path = ReadReg(REG_WindowsTerminals, "Terminal_Preview_Path", SysPaths.TerminalPreviewJSON).ToString();
                }

                /// <summary>
                /// Save settings to registry
                /// </summary>
                public void Save()
                {
                    WriteReg(REG_WindowsTerminals, "Bypass", Bypass, RegistryValueKind.DWord);
                    WriteReg(REG_WindowsTerminals, "ListAllFonts", ListAllFonts, RegistryValueKind.DWord);
                    WriteReg(REG_WindowsTerminals, "Path_Deflection", Path_Deflection, RegistryValueKind.DWord);
                    WriteReg(REG_WindowsTerminals, "Terminal_Stable_Path", Terminal_Stable_Path, RegistryValueKind.String);
                    WriteReg(REG_WindowsTerminals, "Terminal_Preview_Path", Terminal_Preview_Path, RegistryValueKind.String);
                }
            }

            /// <summary>
            /// WinPaletter Store settings structure
            /// </summary>
            public class Store
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
                public string[] Online_Repositories = [Links.Store_MainDB, Links.Store_2ndDB];

                /// <summary>
                /// String array contains directories to WinPaletter themes sources
                /// </summary>
                public string[] Offline_Directories = [string.Empty];

                /// <summary>
                /// Get themes from subdirectories when <c>Online_or_Offline == false</c>
                /// </summary>
                public bool Offline_SubFolders = true;

                /// <summary>
                /// Hide WinPaletter Store tips
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
                    Online_or_Offline = Conversions.ToBoolean(ReadReg(REG_Store, "Online_or_Offline", true));
                    Online_Repositories = (string[])ReadReg(REG_Store, "Online_Repositories", new[] { Links.Store_MainDB, Links.Store_2ndDB });
                    Offline_Directories = (string[])ReadReg(REG_Store, "Offline_Directories", new[] { string.Empty });
                    Offline_SubFolders = Conversions.ToBoolean(ReadReg(REG_Store, "Offline_SubFolders", true));
                    Search_ThemeNames = Conversions.ToBoolean(ReadReg(REG_Store, "Search_ThemeNames", true));
                    Search_AuthorsNames = Conversions.ToBoolean(ReadReg(REG_Store, "Search_AuthorsNames", true));
                    Search_Descriptions = Conversions.ToBoolean(ReadReg(REG_Store, "Search_Descriptions", true));
                    ShowTips = Conversions.ToBoolean(ReadReg(REG_Store, "ShowTips", true));

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

                    WriteReg(REG_Store, "Search_ThemeNames", Search_ThemeNames, RegistryValueKind.DWord);
                    WriteReg(REG_Store, "Search_AuthorsNames", Search_AuthorsNames, RegistryValueKind.DWord);
                    WriteReg(REG_Store, "Search_Descriptions", Search_Descriptions, RegistryValueKind.DWord);
                    WriteReg(REG_Store, "Online_or_Offline", Online_or_Offline, RegistryValueKind.DWord);
                    WriteReg(REG_Store, "Online_Repositories", Online_Repositories, RegistryValueKind.MultiString);
                    WriteReg(REG_Store, "Offline_Directories", Offline_Directories, RegistryValueKind.MultiString);
                    WriteReg(REG_Store, "Offline_SubFolders", Offline_SubFolders, RegistryValueKind.DWord);
                    WriteReg(REG_Store, "ShowTips", ShowTips, RegistryValueKind.DWord);
                }
            }

            /// <summary>
            /// Color info settings structure
            /// </summary>
            public class NerdStats
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
                /// Hide hash (#) before color hex code if <c>true</c> and <c>Type == Structures.NerdStats.Formats.HEX</c>
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
                /// Hide a dot indicator (small circle) if the color is changed from default
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
                    Dec,

                    /// <summary>
                    /// Red, Green, Blue as percentage (CSS style)
                    /// </summary>
                    RGBPercent,

                    /// <summary>
                    /// Alpha, Red, Green, Blue color code
                    /// </summary>
                    ARGB,

                    /// <summary>
                    /// Hue, Saturation, Lightness + Alpha
                    /// </summary>
                    HSLA,

                    /// <summary>
                    /// Hue, Saturation, Value/Brightness
                    /// </summary>
                    HSV,

                    /// <summary>
                    /// Cyan, Magenta, Yellow, Black
                    /// </summary>
                    CMYK,

                    /// <summary>
                    /// Win32 COLORREF (0x00BBGGRR)
                    /// </summary>
                    Win32,

                    /// <summary>
                    /// Known color name (e.g. "SkyBlue")
                    /// </summary>
                    KnownName,

                    /// <summary>
                    /// CSS color string (name if known, otherwise rgb/rgba)
                    /// </summary>
                    CSS
                }

                /// <summary>
                /// Load settings from registry
                /// </summary>
                public void Load()
                {
                    Enabled = Conversions.ToBoolean(ReadReg(REG_NerdStats, string.Empty, true));
                    ShowHexHash = Conversions.ToBoolean(ReadReg(REG_NerdStats, "ShowHexHash", true));
                    Type = (Formats)Convert.ToInt32(ReadReg(REG_NerdStats, "Type", Formats.HEX));
                    UseWindowsMonospacedFont = Conversions.ToBoolean(ReadReg(REG_NerdStats, "UseWindowsMonospacedFont", false));
                    MoreLabelTransparency = Conversions.ToBoolean(ReadReg(REG_NerdStats, "MoreLabelTransparency", false));
                    DotDefaultChangedIndicator = Conversions.ToBoolean(ReadReg(REG_NerdStats, "DotDefaultChangedIndicator", true));
                    DragAndDrop = Conversions.ToBoolean(ReadReg(REG_NerdStats, "DragAndDrop", true));
                    Classic_Color_Picker = Conversions.ToBoolean(ReadReg(REG_NerdStats, "Classic_Color_Picker", false));
                }

                /// <summary>
                /// Save settings to registry
                /// </summary>
                public void Save()
                {
                    WriteReg(REG_NerdStats, string.Empty, Enabled, RegistryValueKind.DWord);
                    WriteReg(REG_NerdStats, "ShowHexHash", ShowHexHash, RegistryValueKind.DWord);
                    WriteReg(REG_NerdStats, "Type", (int)Type);
                    WriteReg(REG_NerdStats, "UseWindowsMonospacedFont", UseWindowsMonospacedFont);
                    WriteReg(REG_NerdStats, "MoreLabelTransparency", MoreLabelTransparency);
                    WriteReg(REG_NerdStats, "DotDefaultChangedIndicator", DotDefaultChangedIndicator);
                    WriteReg(REG_NerdStats, "DragAndDrop", DragAndDrop);
                    WriteReg(REG_NerdStats, "Classic_Color_Picker", Classic_Color_Picker, RegistryValueKind.DWord);

                }
            }

            /// <summary>
            /// Users and services settings structure
            /// </summary>
            public class UsersServices
            {
                /// <summary>
                /// Create new instance of UsersServices settings structure with default values
                /// </summary>
                public UsersServices() { }

                /// <summary>
                /// Load settings from registry
                /// </summary>
                public void Load()
                {

                }

                /// <summary>
                /// Save settings to registry
                /// </summary>
                public void Save()
                {

                }
            }

            /// <summary>
            /// Miscellaneous settings structure
            /// </summary>
            public class Miscellaneous
            {
                /// <summary>
                /// Change Windows 7/8.1 DWM colors in real-time with changing values in WinPaletter
                /// </summary>
                public bool Win7LivePreview = true;

                /// <summary>
                /// Hide welcome dialog on WinPaletter startup
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
                    Win7LivePreview = Conversions.ToBoolean(ReadReg(REG_Miscellaneous, "Win7LivePreview", true));
                    ShowWelcomeDialog = Conversions.ToBoolean(ReadReg(REG_Miscellaneous, "ShowWelcomeDialog", true));
                }

                /// <summary>
                /// Save settings to registry
                /// </summary>
                public void Save()
                {
                    WriteReg(REG_Miscellaneous, "Win7LivePreview", Win7LivePreview, RegistryValueKind.DWord);
                    WriteReg(REG_Miscellaneous, "ShowWelcomeDialog", ShowWelcomeDialog, RegistryValueKind.DWord);
                }
            }

            /// <summary>
            /// WinPaletter themes backup settings structure
            /// </summary>
            public class BackupTheme
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
                /// Automatically backup current theme before opening a new one from open File dialog
                /// </summary>
                public bool AutoBackupOnThemeLoad = false;

                /// <summary>
                /// Automatically backup current theme when an exception error occurs
                /// </summary>
                public bool AutoBackupOnExError = true;

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
                    Enabled = Conversions.ToBoolean(ReadReg(REG_Backup, "Enabled", true));
                    AutoBackupOnAppOpen = Conversions.ToBoolean(ReadReg(REG_Backup, "AutoBackupOnAppOpen", false));
                    AutoBackupOnApply = Conversions.ToBoolean(ReadReg(REG_Backup, "AutoBackupOnApply", true));
                    AutoBackupOnApplySingleAspect = Conversions.ToBoolean(ReadReg(REG_Backup, "AutoBackupOnApplySingleAspect", true));
                    AutoBackupOnThemeLoad = Conversions.ToBoolean(ReadReg(REG_Backup, "AutoBackupOnThemeLoad", false));
                    AutoBackupOnExError = Conversions.ToBoolean(ReadReg(REG_Backup, "AutoBackupOnExError", true));
                    BackupPath = ReadReg(REG_Backup, "BackupPath", $"{SysPaths.appData}\\Backup\\Themes").ToString();
                }

                /// <summary>
                /// Save settings to registry
                /// </summary>
                public void Save()
                {
                    WriteReg(REG_Backup, "Enabled", Enabled, RegistryValueKind.DWord);
                    WriteReg(REG_Backup, "AutoBackupOnAppOpen", AutoBackupOnAppOpen, RegistryValueKind.DWord);
                    WriteReg(REG_Backup, "AutoBackupOnApply", AutoBackupOnApply, RegistryValueKind.DWord);
                    WriteReg(REG_Backup, "AutoBackupOnApplySingleAspect", AutoBackupOnApplySingleAspect, RegistryValueKind.DWord);
                    WriteReg(REG_Backup, "AutoBackupOnThemeLoad", AutoBackupOnThemeLoad, RegistryValueKind.DWord);
                    WriteReg(REG_Backup, "AutoBackupOnExError", AutoBackupOnExError, RegistryValueKind.DWord);
                    WriteReg(REG_Backup, "BackupPath", BackupPath, RegistryValueKind.String);
                }
            }

            /// <summary>
            /// Aspects control settings structure
            /// </summary>
            public class AspectsControl
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
                /// If <c>false</c>, LogonUI won't be applied at all
                /// </summary>
                public bool LogonUI = true;

                /// <summary>
                /// If <c>false</c>, Classic Colors won't be applied at all
                /// </summary>
                public bool ClassicColors = true;

                /// <summary>
                /// If <c>false</c>, Accessibility won't be applied at all
                /// </summary>
                public bool Accessibility = true;

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
                /// If <c>false</c>, Consoles_Cls won't be applied at all
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
                    Enabled = Conversions.ToBoolean(ReadReg(REG_AspectsControl, string.Empty, false));
                    WinColors = Conversions.ToBoolean(ReadReg(REG_AspectsControl, "WinColors", true));
                    LogonUI = Conversions.ToBoolean(ReadReg(REG_AspectsControl, "LogonUI", true));
                    ClassicColors = Conversions.ToBoolean(ReadReg(REG_AspectsControl, "ClassicColors", true));
                    ClassicColors_Advanced = Conversions.ToBoolean(ReadReg(REG_AspectsControl, "ClassicColors_Advanced", true));
                    Accessibility = Conversions.ToBoolean(ReadReg(REG_AspectsControl, "Accessibility", true));
                    MetricsFonts = Conversions.ToBoolean(ReadReg(REG_AspectsControl, "MetricsFonts", true));
                    MetricsFonts_Advanced = Conversions.ToBoolean(ReadReg(REG_AspectsControl, "MetricsFonts_Advanced", true));
                    Cursors = Conversions.ToBoolean(ReadReg(REG_AspectsControl, "Cursors", true));
                    Consoles = Conversions.ToBoolean(ReadReg(REG_AspectsControl, "Consoles", true));
                    WinTerminals = Conversions.ToBoolean(ReadReg(REG_AspectsControl, "WinTerminals", true));
                    Wallpaper = Conversions.ToBoolean(ReadReg(REG_AspectsControl, "Wallpaper", true));
                    Effects = Conversions.ToBoolean(ReadReg(REG_AspectsControl, "Effects", true));
                    Sounds = Conversions.ToBoolean(ReadReg(REG_AspectsControl, "Sounds", true));
                    ScreenSaver = Conversions.ToBoolean(ReadReg(REG_AspectsControl, "ScreenSaver", true));
                    AltTab = Conversions.ToBoolean(ReadReg(REG_AspectsControl, "AltTab", true));
                    Icons = Conversions.ToBoolean(ReadReg(REG_AspectsControl, "Icons", true));
                }

                /// <summary>
                /// Save settings to registry
                /// </summary>
                public void Save()
                {
                    WriteReg(REG_AspectsControl, string.Empty, Enabled, RegistryValueKind.DWord);
                    WriteReg(REG_AspectsControl, "WinColors", WinColors, RegistryValueKind.DWord);
                    WriteReg(REG_AspectsControl, "LogonUI", LogonUI, RegistryValueKind.DWord);
                    WriteReg(REG_AspectsControl, "ClassicColors", ClassicColors, RegistryValueKind.DWord);
                    WriteReg(REG_AspectsControl, "ClassicColors_Advanced", ClassicColors_Advanced, RegistryValueKind.DWord);
                    WriteReg(REG_AspectsControl, "Accessibility", Accessibility, RegistryValueKind.DWord);
                    WriteReg(REG_AspectsControl, "MetricsFonts", MetricsFonts, RegistryValueKind.DWord);
                    WriteReg(REG_AspectsControl, "MetricsFonts_Advanced", MetricsFonts_Advanced, RegistryValueKind.DWord);
                    WriteReg(REG_AspectsControl, "Cursors", Cursors, RegistryValueKind.DWord);
                    WriteReg(REG_AspectsControl, "Consoles", Consoles, RegistryValueKind.DWord);
                    WriteReg(REG_AspectsControl, "WinTerminals", WinTerminals, RegistryValueKind.DWord);
                    WriteReg(REG_AspectsControl, "Wallpaper", Wallpaper, RegistryValueKind.DWord);
                    WriteReg(REG_AspectsControl, "Effects", Effects, RegistryValueKind.DWord);
                    WriteReg(REG_AspectsControl, "Sounds", Sounds, RegistryValueKind.DWord);
                    WriteReg(REG_AspectsControl, "ScreenSaver", ScreenSaver, RegistryValueKind.DWord);
                    WriteReg(REG_AspectsControl, "AltTab", AltTab, RegistryValueKind.DWord);
                    WriteReg(REG_AspectsControl, "Icons", Icons, RegistryValueKind.DWord);
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
        /// Settings related to WinPaletter File types (*.WPTH, *.WPSF) management and registration
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
        /// WinTheme verbose log settings
        /// </summary>
        public Structures.ThemeLog ThemeLog = new();

        /// <summary>
        /// Represents the application log used for recording and managing log entries.
        /// </summary>
        /// <remarks>This field provides access to the application's logging functionality.  Use it to log
        /// messages, warnings, errors, or other information relevant to the application's operation.</remarks>
        public Structures.AppLog AppLog = new();

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
            /// JSON File
            /// </summary>
            File,
            /// <summary>
            /// EmptyError settings
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
                    AppLog.Load();
                    WindowsTerminals.Load();
                    Store.Load();
                    NerdStats.Load();
                    UsersServices.Load();
                    Miscellaneous.Load();
                    BackupTheme.Load();
                    AspectsControl.Load();
                    break;

                case Source.File:
                    if (File.Exists(file))
                    {
                        string[] txt = File.ReadAllLines(file);

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

                                // Add default online repositories if not exist
                                if (!Store.Online_Repositories.Contains(Links.Store_MainDB))
                                {
                                    Array.Resize(ref Store.Online_Repositories, Store.Online_Repositories.Length + 1);
                                    Store.Online_Repositories[Store.Online_Repositories.Length - 1] = Links.Store_MainDB;
                                }

                                // Add second default online repositories if not exist
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
                            Forms.BugReport.ThrowError(new Exception(Program.Lang.Strings.Messages.SettingsFileNotJSON));
                        }
                    }
                    else
                    {
                        Forms.BugReport.ThrowError(new Exception(Program.Lang.Strings.Messages.SettingsFileNotExist));
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
                    AppLog.Save();
                    WindowsTerminals.Save();
                    Store.Save();
                    NerdStats.Save();
                    UsersServices.Save();
                    Miscellaneous.Save();
                    BackupTheme.Save();
                    AspectsControl.Save();
                    break;

                case Source.File:
                    // Add default online repositories if not exist
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

                    File.WriteAllText(file, ToString());
                    break;
            }
        }

        /// <summary>
        /// Returns WinPaletter settings as a JSON string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            JObject JSON_Overall = [];
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
            JObject j = [];

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