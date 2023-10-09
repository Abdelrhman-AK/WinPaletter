using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace WinPaletter
{

    public class Localizer : IDisposable
    {

        #region IDisposable Support
        private bool disposedValue; // To detect redundant calls

        // IDisposable
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
                // TODO: set large fields to null.
            }
            disposedValue = true;
        }

        // TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
        // Protected Overrides Sub Finalize()
        // ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        // Dispose(False)
        // MyBase.Finalize()
        // End Sub

        // This code added by Visual Basic to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        public Localizer()
        {

        }

        public JObject JObj;
        private JObject J_Information;
        private JObject J_GlobalStrings;
        private JObject J_Forms;
        private List<Tuple<string, string, string, string>> Deserialized_FormsJSONTree = new List<Tuple<string, string, string, string>>();

        #region Language Info
        public string Name { get; set; } = Environment.UserName;
        public string TranslationVersion { get; set; } = "1.0";
        public string Lang { get; set; } = "English";
        public string LangCode { get; set; } = "EN-US";
        public string AppVer { get; set; } = "1.0.0.0";
        public bool RightToLeft { get; set; } = false;
        #endregion

        #region Deep-In-Code Strings
        public string OK { get; set; } = "OK";
        public string Next { get; set; } = "Next";
        public string Finish { get; set; } = "Finish";
        public string Yes { get; set; } = "Yes";
        public string No { get; set; } = "No";
        public string Cancel { get; set; } = "Cancel";
        public string Close { get; set; } = "Close";
        public string Retry { get; set; } = "Retry";
        public string ExpandNote { get; set; } = "Expand note";
        public string CollapseNote { get; set; } = "Collapse note";
        public string Bug_NoReport { get; set; } = "There is no previous saved report in '{0}'";
        public string NewUpdate { get; set; } = "A new update is available";
        public string By { get; set; } = "By";
        public string Done { get; set; } = "Done";
        public string Show { get; set; } = "Show";
        public string Hide { get; set; } = "Hide";
        public string InputValue { get; set; } = "Input value";
        public string InputThemeRepos { get; set; } = "Type a URL to a WinPaletter themes database";
        public string InputThemeRepos_Notice { get; set; } = "This database must follow the guidelines demonstrated in WinPaletter Store documentation";
        public string ItMustBeNumerical { get; set; } = "It must be a numerical value";
        public string BugReport_Title { get; set; } = "There is a {0} error. Please try again or report this in GitHub issues";
        public string CurrentMode { get; set; } = "Current Mode";
        public string SaveMsg { get; set; } = "Do you want to save settings?";
        public string SettingsSaved { get; set; } = "Settings are saved";
        public string TBSizeUnit { get; set; } = "TB";
        public string GBSizeUnit { get; set; } = "GB";
        public string MBSizeUnit { get; set; } = "MB";
        public string KBSizeUnit { get; set; } = "KB";
        public string ByteSizeUnit { get; set; } = "B";
        public string SecondUnit { get; set; } = "/s";
        public string Stable { get; set; } = "Stable";
        public string Beta { get; set; } = "Beta";
        public string Channel { get; set; } = "Channel";
        public string AndBelow { get; set; } = "and below";
        public string AppPreview { get; set; } = "App preview";
        public string InactiveApp { get; set; } = "Inactive app";
        public string InvalidTheme { get; set; } = "Error: Invalid theme file.";
        public string ThemeNotExist { get; set; } = "Theme file doesn't exist.";
        public string SettingsFileNotExist { get; set; } = "Settings file doesn't exist";
        public string SettingsFileNotJSON { get; set; } = "This settings file is not a valid JSON";
        public string OS_Win11 { get; set; } = "Windows 11";
        public string OS_Win10 { get; set; } = "Windows 10";
        public string OS_Win8 { get; set; } = "Windows 8";
        public string OS_Win81 { get; set; } = "Windows 8.1";
        public string OS_Win7 { get; set; } = "Windows 7";
        public string OS_WinVista { get; set; } = "Windows Vista";
        public string OS_WinXP { get; set; } = "Windows XP";
        public string OS_WinUndefined { get; set; } = "Windows 11 or higher";
        public string Win11ColorsDescTip { get; set; } = "These description labels are dependent on the latest stable Windows 11. If your Windows is outdated, these labels might not be the same as your current system.";
        public string Win11ColorsDescTip2 { get; set; } = "And if you installed ExplorerPatcher and uninstalled it, WinPaletter will detect ExplorerPatcher is still installed (due do ExplorerPatcher registry remnants) and so the descriptions will be different. You can solve this by going to Settings > ExplorerPatcher and then disable preview synchronization.";
        public string AltTab_Unsupported { get; set; } = "Windows Switcher isn't supported in {0} as it is a classic switcher that can't be changed by registry. Change the preview to another OS and try again.";
        public string VistaLogonNotSupported { get; set; } = "Editing Windows Vista LogonUI with registry is not supported. Change the preview to another OS and try again.";
        public string MonitorIssue { get; set; } = @"Error occurred during loading registry monitor (Used in real-time-detection of wallpaper\dark mode change from registry). Resetting your wallpaper may fix the issue.";
        public string MonitorIssue2 { get; set; } = "Anyway, loading will continue without it.";
        public string LogoffQuestion { get; set; } = "Are you sure you want to log off?";
        public string LogoffAlert1 { get; set; } = "This will close all open files and applications";
        public string LogoffAlert2 { get; set; } = "Logoff equals 'Sign out' in Windows 8.1, 10 & 11";
        public string LogoffNotFound { get; set; } = "Couldn't find logoff process in '{0}' directory. You should logoff your Windows profile manually.";
        public string WallpaperTone_Notice { get; set; } = "This is for {0}. To change another OS preferences, switch the preview in main form";
        public string KillingExplorer { get; set; } = "Killing Explorer (To be restarted)";
        public string ExplorerRestarted { get; set; } = "Explorer Restarted. It took about {0} seconds to kill explorer";
        public string ErrorExplorerRestart { get; set; } = "Error in restarting explorer. Relaunch it in Task Manager (Open Task Manager > Run new task > Type 'Explorer.exe' and launch)";
        public string Scaling { get; set; } = "Scaling";
        public string LanguageRestart { get; set; } = "To apply this language, save settings and restart WinPaletter.";
        public string Uninstall_Comment { get; set; } = "This will help you delete WinPaletter and clean up its used data";
        public string WP_Theme_FileType { get; set; } = "WinPaletter Theme File";
        public string WP_Settings_FileType { get; set; } = "WinPaletter Settings File";
        public string WP_ResourcesPack_FileType { get; set; } = "WinPaletter Theme Resources Pack";

        public string TM_11_StartMenu_Taskbar_AC { get; set; } = "Start menu, taskbar && action center";
        public string TM_11_ACHover_Links { get; set; } = "Action center hover && links";
        public string TM_11_Taskbar_ACHover_Links { get; set; } = "Taskbar color, action center hover && links";
        public string TM_EP_ACButton_TaskbarAppLine { get; set; } = "Action center buttons && taskbar app underline";
        public string TM_11_Lines_Toggles_Buttons { get; set; } = "Lines, toggles, buttons, taskbar app underline && volume slider";
        public string TM_11_Lines_Toggles_Buttons_Overflow { get; set; } = "Lines, toggles, buttons, volume slider && taskbar tray (overflow)";
        public string TM_11_OverflowTray { get; set; } = "Taskbar tray overflow (requires 22H2, accent on taskbar enabled)";
        public string TM_11_StartMenu_AC { get; set; } = "Start menu && action center colors";
        public string TM_11_UnreadBadge { get; set; } = "Unread notifications count badge and other circles";
        public string TM_11_Settings { get; set; } = "Settings icons, text selection, focus dots && some pressed buttons";
        public string TM_11_SettingsAndTaskbarAppUnderline { get; set; } = "Settings icons, taskbar app underline, some pressed buttons && others";
        public string TM_11_SomePressedButtons { get; set; } = "Some pressed buttons";
        public string TM_UWPBackground { get; set; } = "UWP dialogs background (Windows 8.1 remnants in {0})";
        public string TM_10_ACLinks { get; set; } = "Action center links";
        public string TM_10_ACLinks_StartContextMenu { get; set; } = "Action center links and start's context menus";
        public string TM_10_TaskbarAppUnderline { get; set; } = "Taskbar app underline";
        public string TM_10_StartMenuIconHover { get; set; } = "Start menu icon hover";
        public string TM_10_Settings_Links_SomeBtns { get; set; } = "Settings icons, links && some pressed buttons";
        public string TM_10_Settings_Links_Taskbar_SomeBtns { get; set; } = "Settings icons, links, taskbar focused app && some pressed buttons";
        public string TM_10_Settings_Links_TaskbarUndeline_SomeBtns { get; set; } = "Settings icons, links, taskbar app underline && some pressed buttons";
        public string TM_10_Hamburger { get; set; } = "Sliding hamburger menu";
        public string TM_10_StartMenu_AC { get; set; } = "Start menu && action center";
        public string TM_EP_StartMenu_OverflowMenus { get; set; } = "Start menu && overflow menus";
        public string TM_EP_StartMenu_ActionCenterButtons { get; set; } = "Start menu && action center buttons";
        public string TM_EP_ActionCenterBackground { get; set; } = "Action center background";
        public string TM_10_StartMenu_AC_TaskbarActiveApp { get; set; } = "Start menu, action center && taskbar active app";
        public string TM_10_Taskbar { get; set; } = "Taskbar";
        public string TM_10_Taskbar_StartContextMenu { get; set; } = "Taskbar and start's context menus";
        public string TM_10_StartContextMenu { get; set; } = "Start's context menus";
        public string TM_EP_Taskbar_AppUnderline { get; set; } = "Taskbar && app underline";
        public string TM_10_Taskbar_ACLinks_StartContextMenu { get; set; } = "Taskbar background color, action center links && start's context menus";
        public string TM_10_TaskbarFocusedApp_StartButtonHover { get; set; } = "Taskbar focused app && Start menu button hover";
        public string TM_Undefined { get; set; } = "Undefined";
        public string TM_AccentOnTaskbarTib { get; set; } = "Applying accent on taskbar only isn't effective only for Windows 10 2015 versions, but it is effective for higher versions.";
        public string TM_Time { get; set; } = "It took {0} seconds";
        public string TM_Time_They { get; set; } = "They took {0} seconds";
        public string TM_Time_Cursors { get; set; } = "Total applying Windows cursors took {0} seconds";
        public string TM_ApplyFrom { get; set; } = "WinPaletter will apply theme from {0}'s section";
        public string TM_Admin_Msg0 { get; set; } = "Writing to registry without administrator rights by deflection";
        public string TM_Admin_Msg1 { get; set; } = "This deflection will take time more than if started as administrator";
        public string TM_Applying_Started { get; set; } = "Applying started";
        public string TM_SavingInfo { get; set; } = "Saving theme info into registry";
        public string TM_ThemeReset { get; set; } = "Resetting theme to default Windows to apply new theme correctly";
        public string TM_Applying_Win11 { get; set; } = "Applying Windows 11 scheme";
        public string TM_Applying_Win10 { get; set; } = "Applying Windows 10 scheme";
        public string TM_Applying_Win81 { get; set; } = "Applying Windows 8.1 scheme";
        public string TM_Applying_Win7 { get; set; } = "Applying Windows 7 theme";
        public string TM_Applying_WinVista { get; set; } = "Applying Windows Vista theme";
        public string TM_Applying_WinXP { get; set; } = "Applying Windows XP theme";
        public string TM_Applying_LogonUI11 { get; set; } = "Applying Windows 11 LogonUI";
        public string TM_Applying_LogonUI10 { get; set; } = "Applying Windows 10 LogonUI";
        public string TM_Applying_LogonUI8 { get; set; } = "Applying Windows 8.1 Lock Screen";
        public string TM_Applying_LogonUI7 { get; set; } = "Applying Windows 7 LogonUI";
        public string TM_Applying_LogonUIXP { get; set; } = "Applying Windows XP LogonUI";
        public string TM_Applying_Win32UI { get; set; } = "Applying Classic Colors";
        public string TM_Applying_WinEffects { get; set; } = "Applying Windows Effects";
        public string TM_Applying_WallpaperTone { get; set; } = "Applying Wallpaper Tone";
        public string TM_Applying_DesktopAllUsers { get; set; } = "Applying Wallpaper for all users";
        public string TM_Applying_CMD { get; set; } = "Applying Command Prompt";
        public string TM_Applying_Metrics { get; set; } = "Applying Windows Metrics and Fonts";
        public string TM_Applying_AltTab { get; set; } = "Applying Windows Switcher (Alt+Tab) appearance";
        public string TM_Applying_Wallpaper { get; set; } = "Applying Wallpaper";
        public string TM_Applying_AppTheme { get; set; } = "Applying WinPaletter application theme";
        public string TM_Applying_TerminalPreview { get; set; } = "Applying Windows Terminal Preview";
        public string TM_Applying_ScreenSaver { get; set; } = "Applying Screen Saver";
        public string TM_Applying_Sounds { get; set; } = "Applying Sounds";
        public string TM_Check_Terminals { get; set; } = "Checking if Windows Terminal (Stable & Preview) are installed";
        public string TM_Check_TerminalStable { get; set; } = "Checking if Windows Terminal Stable is installed";
        public string TM_Check_TerminalPreview { get; set; } = "Checking if Windows Terminal Preview is installed";
        public string TM_Skip_TerminalPreview { get; set; } = "Skipping Windows Terminal Preview as it is disabled";
        public string TM_Skip_TerminalStable { get; set; } = "Skipping Windows Terminal Stable as it is disabled";
        public string TM_Skip_Terminals { get; set; } = "Skipping Windows Terminal Stable & Preview as they are disabled";
        public string TM_Skip_Terminals_NotSupported { get; set; } = "Skipping Windows Terminal Stable, Preview. Not supported in this OS";
        public string TM_Skip_TerminalPreview_NotInstalled { get; set; } = "Skipping Windows Terminal Preview as it isn't installed";
        public string TM_Skip_TerminalPreview_DeflectionNotFound { get; set; } = "Skipping Windows Terminal Preview as deflected JSON doesn't exist";
        public string TM_Skip_TerminalStable_NotInstalled { get; set; } = "Skipping Windows Terminal Stable as it isn't installed";
        public string TM_Skip_TerminalStable_DeflectionNotFound { get; set; } = "Skipping Windows Stable Preview as deflected JSON doesn't exist";
        public string TM_Skip_CMD { get; set; } = "Skipping Command Prompt as it is disabled";
        public string TM_Applying_PS64 { get; set; } = "Applying PowerShell x64";
        public string TM_Skip_PS64 { get; set; } = "Skipping PowerShell x64 as it is disabled";
        public string TM_Applying_PS32 { get; set; } = "Applying PowerShell x86";
        public string TM_Skip_PS32 { get; set; } = "Skipping PowerShell x86 as it is disabled";
        public string TM_Skip_Metrics { get; set; } = "Skipping Windows Metrics and Fonts as they are disabled";
        public string TM_Skip_AltTab { get; set; } = "Skipping Windows Switcher (Alt+Tab) Appearance as it is disabled";
        public string TM_Skip_Wallpaper { get; set; } = "Skipping Wallpaper as it is disabled";
        public string TM_Skip_AppTheme { get; set; } = "Skipping WinPaletter application theme as it is disabled";
        public string TM_Skip_Sounds { get; set; } = "Skipping Sounds as its toggle is disabled";
        public string TM_CMD_Error { get; set; } = "Error occurred during applying Command Prompt";
        public string TM_PS32_Error { get; set; } = "Error occurred during applying PowerShell x86";
        public string TM_PS64_Error { get; set; } = "Error occurred during applying PowerShell x64";
        public string TM_WIN32UI_Error { get; set; } = "Error occurred during applying Classic Colors";
        public string TM_WinEffects_Error { get; set; } = "Error occurred during applying Windows Effects";
        public string TM_WallpaperTone_Error { get; set; } = "Error occurred during applying Wallpaper Tone";
        public string TM_LogonUI11_Error { get; set; } = "Error occurred during applying Windows 11 LogonUI";
        public string TM_LogonUI10_Error { get; set; } = "Error occurred during applying Windows 10 LogonUI";
        public string TM_LogonUI8_Error { get; set; } = "Error occurred during applying Windows 8.1 Lock Screen";
        public string TM_LogonUI7_Error { get; set; } = "Error occurred during applying Windows 7 LogonUI";
        public string TM_LogonUIXP_Error { get; set; } = "Error occurred during applying Windows XP LogonUI";
        public string TM_W10_Error { get; set; } = "Error occurred during applying Windows 10 scheme";
        public string TM_SavingInfo_Error { get; set; } = "Error occurred during saving theme info into registry";
        public string TM_ThemeReset_Error { get; set; } = "Error occurred during resetting theme to default Windows";
        public string TM_W11_Error { get; set; } = "Error occurred during applying Windows 11 scheme";
        public string TM_W7_Error { get; set; } = "Error occurred during applying Windows 7 theme";
        public string TM_WVista_Error { get; set; } = "Error occurred during applying Windows Vista theme";
        public string TM_W81_Error { get; set; } = "Error occurred during applying Windows 8.1 scheme";
        public string TM_WXP_Error { get; set; } = "Error occurred during applying Windows XP theme";
        public string TM_Error_Cursors { get; set; } = "Error occurred during applying Windows Cursors";
        public string TM_Error_SetDesktop { get; set; } = "Error occurred during applying Desktop for all users";
        public string TM_Error_Metrics { get; set; } = "Error occurred during applying Windows Metrics and Fonts";
        public string TM_Error_AltTab { get; set; } = "Error occurred during applying Windows Switcher (Alt+Tab) appearance";
        public string TM_Error_Wallpaper { get; set; } = "Error occurred during applying Wallpaper";
        public string TM_Error_AppTheme { get; set; } = "Error occurred during applying WinPaletter application theme";
        public string TM_Error_TerminalPreview { get; set; } = "Error occurred during applying Windows Terminal Preview";
        public string TM_Error_TerminalStable { get; set; } = "Error occurred during applying Windows Terminal Stable";
        public string TM_Error_ScreenSaver { get; set; } = "Error occurred during applying Screen Saver";
        public string TM_Error_Sounds { get; set; } = "Error occurred during applying Sounds";
        public string TM_MetricsHighDPIAlert { get; set; } = "Please Logoff and Logon after setting Metrics and Fonts with a high DPI";
        public string TM_Restricted_Cursors { get; set; } = "Modifying Windows Cursors is restricted from settings";
        public string TM_Applying_TerminalStable { get; set; } = "Applying Windows Terminal Stable";
        public string TM_AppliedWithErrors { get; set; } = "Applying theme done but with error/s. It took {0} seconds";
        public string TM_Applied { get; set; } = "Applying theme done. It took {0} seconds";
        public string TM_AllDone { get; set; } = "All operations are done";
        public string TM_ErrorHappened { get; set; } = @"Error\s happened. Press on 'Show Errors' for details";
        public string TM_LogWillClose { get; set; } = @"This log will close after {0} second\s";
        public string TM_RestoreCursorsError { get; set; } = "Error occurred during resetting cursors to aero. Anyway, process will continue.";
        public string TM_RestoreCursorsErrorPressOK { get; set; } = "Pressing OK will show details of exception error.";
        public string TM_RestoreCursorsTip { get; set; } = "If you want to restore default cursors, go to Control Panel > Mouse > Pointers";
        public string TM_UpdateDLL_AsAdmin_Error0 { get; set; } = "You must be running WinPaletter as Administrator to update resources of '{0}'";
        public string TM_UpdateDLL_AsAdmin_Error1 { get; set; } = "This process is required for changing Windows startup sound";
        public string TM_Wallpaper_NonBMP0 { get; set; } = "Due to odd reason, Windows XP, Vista & 7 can't set an image that is not a bitmap format directly as a wallpaper.";
        public string TM_Wallpaper_NonBMP1 { get; set; } = "Do you want to convert the current image to have internal bitmap format? (It will still have the same file extension)";

        public string Verbose_RegAdd { get; set; } = "{0} > {1} = {2}, RegistryValueKind = {3}";
        public string Verbose_RegSkipped { get; set; } = "Skipped: {0}";
        public string Verbose_RegDelete { get; set; } = "Deleting: {0}";
        public string Verbose_CreateTask { get; set; } = "Creating task {0} in Task Scheduler";
        public string Verbose_DeleteTask { get; set; } = "Deleting task {0} from Task Scheduler";
        public string Verbose_PE_GettingAccess { get; set; } = "Trying to get authorized access to change '{0}' access/permissions";
        public string Verbose_PE_GetAccessToChangeResources { get; set; } = "Trying to get authorized access to change '{0}' resources";
        public string Verbose_PE_CreateBackup { get; set; } = "Creating '{0}' backup";
        public string Verbose_PE_GetBackupPermissions { get; set; } = "Trying to get '{0}' permissions backup";
        public string Verbose_PE_PatchingPE { get; set; } = "Patching '{0}' resources";
        public string Verbose_PE_RestoringPermissions { get; set; } = "Restoring '{0}' permissions from backup";
        public string Verbose_DeletingHighContrastThemes { get; set; } = "Deleting high contrast themes in '{0}'";
        public string Verbose_UxTheme_SSVS { get; set; } = "Setting visual styles: {0}.{1}({2}, {3}, {4}, {5})";
        public string Verbose_UxTheme_ET { get; set; } = "Enabling theming: {0}.{1}({2})";
        public string Verbose_User32_SPI { get; set; } = "Calling {0}.{1}({2}, {3}, {4}, {5})";
        public string Verbose_User32_SSC { get; set; } = "Calling {0}.{1}({2}, {3})";
        public string Verbose_User32_SMT { get; set; } = "Broadcasting all effects made to registry: {0}.{1}({2}, {3}, {4}, {5}, {6}, {7}, {8})";
        public string Verbose_SettingSlideshow { get; set; } = "Setting desktop slideshow data by modifying '{0}'";
        public string Verbose_SettingHSLImage { get; set; } = "Modifying HSL filters of selected image '{0}'";
        public string Verbose_EnableExplorerBar { get; set; } = "Enabling explorer bar by renaming UIRibbon.dll into UIRibbon.dll_bak";
        public string Verbose_RestoreExplorerBar { get; set; } = "Restoring explorer ribbon/bar by renaming UIRibbon.dll_bak into UIRibbon.dll";
        public string Verbose_GetInstanceLogonUIImg { get; set; } = "Getting an instance of LogonUI image into memory";
        public string Verbose_GetInstanceLockScreenImg { get; set; } = "Getting an instance of lock screen image into memory";
        public string Verbose_GrayscaleLogonUIImg { get; set; } = "Doing grayscale effect on LogonUI image";
        public string Verbose_GrayscaleLockScreenImg { get; set; } = "Doing grayscale effect on lock screen image";
        public string Verbose_BlurringLogonUIImg { get; set; } = "Blurring LogonUI image";
        public string Verbose_BlurringLockScreenImg { get; set; } = "Blurring lock screen image";
        public string Verbose_NoiseLogonUIImg { get; set; } = "Doing noise effect on LogonUI image";
        public string Verbose_NoiseLockScreenImg { get; set; } = "Doing noise effect on lock screen image";
        public string Verbose_LogonUIImgSaved { get; set; } = "Modified LogonUI image is saved into '{0}'";
        public string Verbose_LogonUIImgNUMSaved { get; set; } = "Modified LogonUI image number {1} is saved into '{0}'";
        public string Verbose_LockScreenImgSaved { get; set; } = "Modified lock screen image is saved into '{0}'";
        public string Verbose_RenderingCursor { get; set; } = "Rendering cursor '{0}'";
        public string Verbose_CursorRenderedInto { get; set; } = "Cursor is rendered into '{0}";
        public string Verbose_DelCursorWPFromReg { get; set; } = "Deleting registry value 'WinPaletter' from '{0}' to start cursors restoration";

        public string Lang_HasLeftToRight { get; set; } = "It has left to right layout";
        public string Lang_HasRightToLeft { get; set; } = "It has right to left layout";
        public string Lang_ChooseAForm { get; set; } = "Choose a form then open it. When you finish translation, close the child form below.";
        public string Lang_LoadingChildrenForms { get; set; } = "Loading GUI of all WinPaletter forms into your memory ({0}%). Be cautious as this will extensively increase WinPaletter memory usage.";

        public string CMDSimulator_Alert0 { get; set; } = "This is just an preview to your custom terminal.";
        public string CMDSimulator_Alert1 { get; set; } = "*Note: Raster Font will look different from the preview.";
        public string CMDSimulator_ThisIsAPopUp { get; set; } = "This is a pop-up";
        public string Terminal_ConsoleSample { get; set; } = "Console sample";
        public string Terminal_ThisIsASelection { get; set; } = "This is a selection";
        public string Terminal_Another { get; set; } = "Another Terminal";
        public string CommandPrompt { get; set; } = "Command Prompt";
        public string PowerShellx86 { get; set; } = "PowerShell x86";
        public string PowerShellx64 { get; set; } = "PowerShell x64";
        public string Open_Testing_CMD { get; set; } = "Open Command Prompt for testing";
        public string Open_Testing_PowerShellx86 { get; set; } = "Open PowerShell x86 for testing";
        public string Open_Testing_PowerShellx64 { get; set; } = "Open PowerShell x64 for testing";
        public string TerminalStable { get; set; } = "Windows Terminal Stable";
        public string TerminalPreview { get; set; } = "Windows Terminal Preview";
        public string Terminal_TypeSchemeName { get; set; } = "Type theme name here:";
        public string Default { get; set; } = "Default";
        public string Untitled { get; set; } = "Untitled";
        public string WhatsNewInVersion { get; set; } = "What's new in {0}!";
        public string ThisWillRestartExplorer { get; set; } = "This will restart the explorer, don't worry this won't close other applications";
        public string LogoffNotice { get; set; } = "This will logoff your Windows account. Please save your open files before logging-off";
        public string TitlebarColorNotice { get; set; } = "Windows volume slider, UAC and Windows 10 logonUI follow active titlebar color";
        public string NoDefResExplorer { get; set; } = "Restarting Explorer is disabled. If theme is not applied correctly, restart it";
        public string RemoveExtMsg { get; set; } = "Are you sure from removing files association (*.wpth, *.wptp, *.wpsf) from registry?";
        public string RemoveExtMsgNote { get; set; } = "Note: You can reassociate them by activating its checkbox and restarting WinPaletter";
        public string EmptyName { get; set; } = "You can't leave theme name empty. Please type a name to it";
        public string EmptyAuthorName { get; set; } = "You can't leave author's name empty. Please type author's name or your name.";
        public string EmptyVer { get; set; } = "You can't leave theme version empty. Please type a version to it in this style (x.x.x.x), replacing (x) by numbers";
        public string Empty { get; set; } = "Empty";
        public string WrongVerFormat { get; set; } = "Wrong version format. Please type the version to it in this style (x.x.x.x), replacing (x) by numbers.";
        public string Extracting { get; set; } = "Extracting palette from image depends on your device's performance, maximum palette colors number, image quality and its resolution ...";
        public string Version { get; set; } = "Version";
        public string Checking { get; set; } = "Checking ...";
        public string DoAction_Update { get; set; } = "Do action";
        public string NoUpdateAvailable { get; set; } = "No update is available";
        public string CheckForUpdates { get; set; } = "Check for update";
        public string NetworkError { get; set; } = "Network error. Check you internet connection.";
        public string UpdatesOSNoTLS12 { get; set; } = "Updates won't work as TLS 1.2 protocol isn't enabled in {0}. Use GitHub instead";
        public string ServerError { get; set; } = "Error: Network issues or GitHub repository is private or deleted. Visit GitHub page for details.";
        public string Msgbox_Downloaded { get; set; } = "Downloaded successfully";
        public string LngExported { get; set; } = "Language exported successfully";
        public string LangSaved { get; set; } = "Language file has been saved successfully";
        public string ScalingTip { get; set; } = "Scaling option is only a preview, the cursor will be saved with different sizes and the suitable size will be loaded according to your DPI settings.";
        public string LngShouldClose { get; set; } = "You should close the app if you want to export language.";
        public string CMD_Enable { get; set; } = "You should enable terminal editing from the toggle above.";
        public string CMD_NotAllWeights { get; set; } = "Not all weights are available according to your OS and the font itself. Normal and Bold ones are the basic ones.";
        public string ExtTer_NotFound { get; set; } = "Terminal not found. Select a valid one from the list.";
        public string ExtTer_Set { get; set; } = "Terminal preferences are set successfully!";
        public string ExtTer_NewSuccess { get; set; } = "This key is entered into registry successfully.";
        public string ExtTer_NewError { get; set; } = "Couldn't write this entry to registry. Please check if this key already exists or check permissions.";
        public string ErrorDetails { get; set; } = "Error details: ";
        public string Terminal_alreadyset { get; set; } = "You can't set this name as it is already set to another profile.";
        public string TerminalStable_notFound { get; set; } = "Windows Terminal Stable isn't installed or 'settings.json' isn't accessible.";
        public string TerminalPreview_notFound { get; set; } = "Windows Terminal Preview isn't installed or 'settings.json' isn't accessible.";
        public string PowerShellx86_notFound { get; set; } = "Microsoft PowerShell x86 is not installed.";
        public string PowerShellx64_notFound { get; set; } = "Microsoft PowerShell x64 is not installed.";
        public string Terminal_supposed { get; set; } = "It is supposed to be located in: ";
        public string Terminal_Bypass { get; set; } = "You can bypass this restriction in Settings > Terminals (In case you want to design a theme for all versions of Windows and save it as a file for sharing, not applying it).";
        public string Terminal_CantRun { get; set; } = "You can't run Windows Terminal in current OS. It is available only in Windows 10 and 11.";
        public string Terminal_ErrorFile { get; set; } = "Error occurred while reading settings file: ";
        public string Terminal_ProfileNotCloneable { get; set; } = "Default Profile isn't cloneable, select a different profile.";
        public string Terminal_ThemeNotCloneable { get; set; } = @"Default themes (Dark\Light\System) are not cloneable, select a different theme or create a new theme if you want to clone.";
        public string Terminal_Clone { get; set; } = "Clone";
        public string Terminal_NewProfile { get; set; } = "New profile";
        public string Terminal_NewScheme { get; set; } = "New scheme";
        public string Terminal_NewTheme { get; set; } = "New theme";
        public string Terminal_External_Empty { get; set; } = "Terminal can't be empty. Enter a valid one.";
        public string Terminal_External_NotExist { get; set; } = "Terminal doesn't exist. Enter a valid one.";
        public string Terminal_External_Reversed { get; set; } = "This terminal is reserved for system. Try again with another one.";
        public string Terminal_External_Exists { get; set; } = "This terminal already exists. Try again with another one.";
        public string TM_RenderingCustomLogonUI_MayNotRespond { get; set; } = "WinPaletter may not respond while rendering custom LogonUI";
        public string TM_RenderingCustomLogonUI_Progress { get; set; } = "Rendering custom LogonUI:";
        public string TM_RenderingCustomLogonUI { get; set; } = "Rendering custom LogonUI";
        public string TM_SavingCursorsColors { get; set; } = "Saving Windows Cursors Colors to registry";
        public string TM_RenderingCursors { get; set; } = "Rendering Windows Cursors";
        public string TM_RenderingCursors_Error { get; set; } = "Error occurred during rendering Windows Cursors";
        public string TM_CursorsApplying_Error { get; set; } = "Error occurred during applying Windows Cursors";
        public string TM_ApplyingCursors { get; set; } = "Applying Windows Cursors";
        public string OldMSTheme_Copyrights { get; set; } = "Copyright © Microsoft Corp. 1995-{0}";
        public string OldMSTheme_CreatedFromAppVer { get; set; } = "Created from application version {0}";
        public string OldMSTheme_ProgrammedBy { get; set; } = "This theme was designed by WinPaletter, programmed by {0}";
        public string OldMSTheme_CreatedBy { get; set; } = "Created by {0}";
        public string OldMSTheme_ThemeName { get; set; } = "Theme name: {0}";
        public string OldMSTheme_ThemeVersion { get; set; } = "Theme version: {0}";

        public string PE_Systemfile { get; set; } = "System PE file";
        public string PE_ReplacedResourceProperties { get; set; } = "Replaced resource properties";
        public string PE_ResourceType { get; set; } = "Type";
        public string PE_ResourceID { get; set; } = "ID";
        public string PE_ResourceLanguageCode { get; set; } = "Language code";
        public string PE_RunSFCinCMD_Node { get; set; } = "Run this in Command Prompt as administrator to restore PE file integrity (health)";
        public string PE_DontForgetToRestart { get; set; } = "Don't forget to restart your Windows after that to complete restoring PE file integrity";

        public string ColorItem_Copy { get; set; } = "Copy color into dropped item";
        public string ColorItem_Copy_Invert { get; set; } = "Copy color into dropped item as inverted";
        public string ColorItem_Copy_Darker { get; set; } = "Copy color into dropped item but darker";
        public string ColorItem_Copy_Lighter { get; set; } = "Copy color into dropped item but lighter";
        public string ColorItem_Copy_Mix { get; set; } = "Mix into dropped item";
        public string ColorItem_Swap { get; set; } = "Swap between two colors";
        public string ColorItem_Swap_Invert { get; set; } = "Swap between two colors as inverted";
        public string ColorItem_Swap_Darker { get; set; } = "Swap between two colors but darker";
        public string ColorItem_Swap_Lighter { get; set; } = "Swap between two colors but lighter";

        public string Store_RemoveTip { get; set; } = "You can't remove an essential themes database repository. Try again with another custom repository.";
        public string Store_NoNetwork { get; set; } = "No internet connection";
        public string Store_TryOffline { get; set; } = "Press 'Yes' if you want to continue in offline Store mode. You can select its folders from Settings > Store.";
        public string Store_Ping { get; set; } = "Testing access to '{0}'";
        public string Store_PingFailed { get; set; } = "Couldn't get response from '{0}'. Skipping this themes database";
        public string Store_Accessing { get; set; } = "Accessing themes database from '{0}'";
        public string Store_UpdateTheme { get; set; } = "Updating theme '{0}' from '{1}'";
        public string Store_DownloadTheme { get; set; } = "Downloading theme '{0}' from '{1}'";
        public string Store_DownloadingPackForTheme { get; set; } = "Downloading resources pack for theme '{0}' version {1}";
        public string Store_LoadingTheme { get; set; } = "Loading theme '{0}'";
        public string Store_LowAppVersionAlert { get; set; } = "This theme is designed by WinPaletter {0} which is higher than {1}. please update WinPaletter to enjoy all new themes aspects and avoid errors.";
        public string Store_CleaningFromMemory { get; set; } = "Cleaning store items from memory";
        public string Store_NoIncludedData { get; set; } = "There is no included data";
        public string Store_WontWork_Protocol { get; set; } = "Unfortunately, WinPaletter Store won't work as TLS 1.2 protocol isn't enabled in {0}. Do you want to continue?";
        public string Store_ThemeDesignedFor0 { get; set; } = "This theme can be applied to all supported versions of Windows, but it was designed for:";
        public string Store_ThemeDesignedFor1 { get; set; } = "This theme can be applied to all supported versions of Windows:";
        public string Store_LogoffRecommended { get; set; } = "It is recommended to logoff your Windows and logon to apply all effects of the theme";
        public string Store_Calculating { get; set; } = "Calculating ...";
        public string Store_AuthorURLRedirect { get; set; } = "This will redirect you to author's social media URL. Do you want to continue?";

        public string Store_Toggle_AppTheme { get; set; } = "WinPaletter application theme";
        public string Store_Toggle_LogonUI { get; set; } = "LogonUI screen";
        public string Store_Toggle_Cursors { get; set; } = "Cursors";
        public string Store_Toggle_CMD { get; set; } = "Command Prompt";
        public string Store_Toggle_PS86 { get; set; } = "PowerShell x86";
        public string Store_Toggle_PS64 { get; set; } = "PowerShell x64";
        public string Store_Toggle_TerminalStable { get; set; } = "Windows Terminal Stable";
        public string Store_Toggle_TerminalPreview { get; set; } = "Windows Terminal Preview";
        public string Store_Toggle_MetricsFonts { get; set; } = "Metrics & Fonts";
        public string Store_Toggle_WindowsEffects { get; set; } = "Windows Effects";
        public string Store_Toggle_AltTab { get; set; } = "Windows Switcher (Alt+Tab appearance)";
        public string Store_Toggle_Wallpaper { get; set; } = "Wallpaper";
        public string Store_Toggle_Sounds { get; set; } = "Sounds";
        public string Store_Toggle_ScreenSaver { get; set; } = "Screen Saver";

        public string Convert_JSON_To_Old { get; set; } = "Theme file is JSON-internally-formatted. When you export this theme, it will be with old formatting system (valid for WinPaletter 1.0.7.6 and less).";
        public string Convert_Old_To_JSON { get; set; } = "Theme file is old-formatted. When you export this theme, it will be JSON-internally-formatted (valid for WinPaletter 1.0.7.7 and higher). It supports contents compression that is useful for uploading more amount of themes to WinPaletter Store with less downloading duration, and used resources pack export that is useful for downloading missing used resources (wallpapers, images and sounds) from WinPaletter Store (or by external sharing) and applying them automatically with the theme.";
        public string Convert_Error_Phrasing { get; set; } = "Error occurred while phrasing theme file";
        public string Convert_Done { get; set; } = "Theme file is converted and exported successfully";
        public string Convert_Detect_Old_OnLoading0 { get; set; } = "WinPaletter detected that you are using an old theme format";
        public string Convert_Detect_Old_OnLoading1 { get; set; } = "Do you want to convert it and load it automatically?";
        public string Convert_Detect_Old_OnLoading2 { get; set; } = "Pressing 'No' will load a default Windows preset";
        #endregion

        #region Language loader
        public void LoadLanguageFromJSON(string File, Form _Form = null)
        {
            if (System.IO.File.Exists(File))
            {

                using (var St = new StreamReader(File))
                {
                    JObj = JObject.Parse(St.ReadToEnd());
                    St.Close();
                }

                J_Information = new JObject();
                J_GlobalStrings = new JObject();
                J_Forms = new JObject();

                bool Valid = JObj.ContainsKey("Information") & JObj.ContainsKey("Global Strings") & JObj.ContainsKey("Forms Strings");

                if (!Valid)
                {
                    // $$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$

                    return;
                }

                J_Information = (JObject)JObj["Information"];
                J_GlobalStrings = (JObject)JObj["Global Strings"];
                J_Forms = (JObject)JObj["Forms Strings"];

                LoadInnerStrings(J_Information, J_GlobalStrings);
                DeserializeFormsJSONIntoTreeList(J_Forms);
                LoadFromStrings(_Form);

            }
        }

        public void LoadInnerStrings(JObject LangInfo, JObject Strings)
        {
            var type1 = GetType();
            PropertyInfo[] properties1 = type1.GetProperties();

            foreach (PropertyInfo property in properties1)
            {
                if (!((property.Name.ToLower() ?? "") == ("Name".ToLower() ?? "")) && !((property.Name.ToLower() ?? "") == ("TranslationVersion".ToLower() ?? "")) && !((property.Name.ToLower() ?? "") == ("Lang".ToLower() ?? "")) && !((property.Name.ToLower() ?? "") == ("LangCode".ToLower() ?? "")) && !((property.Name.ToLower() ?? "") == ("AppVer".ToLower() ?? "")) && !((property.Name.ToLower() ?? "") == ("RightToLeft".ToLower() ?? "")))
                {
                    if (Strings.ContainsKey(property.Name.ToLower()))
                        property.SetValue(this, Convert.ChangeType(Strings[property.Name.ToLower()], property.PropertyType));
                }
                else if (LangInfo.ContainsKey(property.Name.ToLower()))
                    property.SetValue(this, Convert.ChangeType(LangInfo[property.Name.ToLower()], property.PropertyType));
            }
        }

        public void DeserializeFormsJSONIntoTreeList(JObject JSON_Forms)
        {

            // Tuple of four values; form name, control name, property, property value
            // If there is no control and you want to change form property, make control name: String.Empty
            Deserialized_FormsJSONTree.Clear();

            string FormName, ControlName, Prop, Value;
            FormName = string.Empty;
            ControlName = string.Empty;
            Prop = string.Empty;
            Value = string.Empty;

            // Loop through all forms nodes in JObj
            foreach (var F in JSON_Forms)
            {
                try
                {

                    // Get one form node
                    // There is only one specific property "Text"
                    var J_Specific_Form = new JObject();
                    J_Specific_Form = (JObject)JSON_Forms[F.Key];
                    FormName = F.Key.ToString();
                    ControlName = string.Empty;
                    Prop = "Text";

                    if (J_Specific_Form.ContainsKey("Text") | J_Specific_Form.ContainsKey("text") | J_Specific_Form.ContainsKey("TEXT"))
                    {
                        if (J_Specific_Form.ContainsKey("Text"))
                            Value = J_Specific_Form["Text"].ToString();
                        if (J_Specific_Form.ContainsKey("text"))
                            Value = J_Specific_Form["text"].ToString();
                        if (J_Specific_Form.ContainsKey("TEXT"))
                            Value = J_Specific_Form["TEXT"].ToString();
                        Deserialized_FormsJSONTree.Add(new Tuple<string, string, string, string>(FormName, ControlName, Prop, Value));
                    }

                    // If this form has a control/controls then get them
                    if (J_Specific_Form.ContainsKey("Controls") | J_Specific_Form.ContainsKey("controls") | J_Specific_Form.ContainsKey("CONTROLS"))
                    {

                        // JObj nodes of all child controls
                        var J_Controls = new JObject();
                        if (J_Specific_Form.ContainsKey("Controls"))
                            J_Controls = (JObject)J_Specific_Form["Controls"];
                        if (J_Specific_Form.ContainsKey("controls"))
                            J_Controls = (JObject)J_Specific_Form["controls"];
                        if (J_Specific_Form.ContainsKey("CONTROLS"))
                            J_Controls = (JObject)J_Specific_Form["CONTROLS"];

                        // Loop through all child controls JObj nodes
                        foreach (var ctrl in J_Controls)
                        {
                            try
                            {
                                // If there is a dot in JObj node value, then there is a specific mentioned property,
                                // if not, then it is a "Text" property only.
                                if (Conversions.ToBoolean(ctrl.Key.Contains(".")))
                                {
                                    ControlName = ctrl.Key.Split('.')[0];
                                    Prop = ctrl.Key.Split('.')[1];
                                    Value = ctrl.Value.ToString();
                                    Deserialized_FormsJSONTree.Add(new Tuple<string, string, string, string>(FormName, ControlName, Prop, Value));
                                }
                                else
                                {
                                    ControlName = ctrl.Key.ToString();
                                    Prop = "Text";
                                    Value = ctrl.Value.ToString();
                                    Deserialized_FormsJSONTree.Add(new Tuple<string, string, string, string>(FormName, ControlName, Prop, Value));
                                }
                            }
                            catch
                            {
                            }
                        }
                    }
                }
                catch
                {
                }
            }

        }

        public void LoadFromStrings(Form _Form = null)
        {
            if (_Form is not null)
            {
                bool WasVisible = _Form.Visible;

                if (WasVisible)
                {
                    _Form.Visible = false;
                }

                Populate(Deserialized_FormsJSONTree, _Form);

                if (WasVisible)
                    _Form.Visible = true;
            }
        }

        public void Populate(List<Tuple<string, string, string, string>> PopCtrlList, Form Form)
        {
            // Item1 = FormName
            // Item2 = ControlName
            // Item3 = Prop
            // Item4 = Value

            foreach (var member in PopCtrlList)
            {
                try
                {
                    if ((Form.Name.ToLower() ?? "") == (member.Item1.ToLower() ?? ""))
                    {

                        if (string.IsNullOrEmpty(member.Item2))
                        {
                            // # Form
                            try
                            {
                                if (member.Item3.ToLower() == "text")
                                    Form.SetText(member.Item4);
                            }
                            catch
                            {
                            }

                            try
                            {
                                if (member.Item3.ToLower() == "tag")
                                    Form.SetTag(member.Item4.ToString());
                            }
                            catch
                            {
                            }
                        }
                        // # Control
                        else if (!string.IsNullOrEmpty(member.Item2))
                        {

                            foreach (Control ctrl in Form.Controls.Find(member.Item2, true))
                            {

                                try
                                {
                                    if (member.Item3.ToLower() == "text")
                                    {
                                        if ((member.Item1.ToLower() ?? "") != (My.MyProject.Forms.Whatsnew.Name.ToLower() ?? ""))
                                        {
                                            ctrl.SetText(member.Item4.ToString());
                                        }
                                        else if (!My.MyProject.Forms.Whatsnew.TabControl1.TabPages.Cast<TabPage>().SelectMany(tp => tp.Controls.OfType<Control>()).Contains(ctrl) & !(ctrl is TabPage))
                                        {
                                            ctrl.SetText(member.Item4.ToString());
                                        }
                                    }
                                }
                                catch
                                {
                                }

                                try
                                {
                                    if (member.Item3.ToLower() == "tag")
                                        ctrl.SetTag(member.Item4.ToString());
                                }
                                catch
                                {
                                }

                            }


                        }
                    }
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }

        }
        #endregion

        public void ExportJSON(string File, Form[] Forms = null)
        {
            var JSON_Overall = new JObject();
            var newL = new Localizer();

            var j_info = new JObject();
            j_info.RemoveAll();
            j_info.Add("Name".ToLower(), newL.Name);
            j_info.Add("TranslationVersion".ToLower(), newL.TranslationVersion);
            j_info.Add("Lang".ToLower(), newL.Lang);
            j_info.Add("LangCode".ToLower(), newL.LangCode);
            j_info.Add("AppVer".ToLower(), My.Env.AppVersion);
            j_info.Add("RightToLeft".ToLower(), newL.RightToLeft);

            var j_globalstrings = new JObject();

            var type1 = newL.GetType();
            PropertyInfo[] properties1 = type1.GetProperties();

            foreach (PropertyInfo property in properties1)
            {
                if (!string.IsNullOrWhiteSpace(property.GetValue(newL).ToString()) & !((property.Name.ToLower() ?? "") == ("Name".ToLower() ?? "")) & !((property.Name.ToLower() ?? "") == ("TranslationVersion".ToLower() ?? "")) & !((property.Name.ToLower() ?? "") == ("Lang".ToLower() ?? "")) & !((property.Name.ToLower() ?? "") == ("LangCode".ToLower() ?? "")) & !((property.Name.ToLower() ?? "") == ("AppVer".ToLower() ?? "")) & !((property.Name.ToLower() ?? "") == ("RightToLeft".ToLower() ?? "")))
                {
                    j_globalstrings.Add(property.Name.ToLower(), property.GetValue(newL).ToString());
                }
            }

            var j_Forms = new JObject();

            if (Forms is null)
            {
                foreach (var f in Assembly.GetExecutingAssembly().GetTypes().Where(t => typeof(Form).IsAssignableFrom(t)))
                {
                    var ins = new Form();
                    ins = (Form)Activator.CreateInstance(f);

                    if ((ins.Name.ToLower() ?? "") != (My.MyProject.Forms.BK.Name.ToLower() ?? ""))
                    {
                        JObject j_ctrl = new JObject(), j_child = new JObject();
                        j_ctrl.RemoveAll();
                        j_child.RemoveAll();

                        j_ctrl.Add("Text", ins.Text);

                        foreach (var ctrl in ins.GetAllControls())
                        {

                            if (!string.IsNullOrWhiteSpace(ctrl.Text) && !ctrl.Text.All(char.IsDigit) && !(ctrl.Text.Count() == 1) && !((ctrl.Text ?? "") == (ctrl.Name ?? "")))
                            {

                                if ((ins.Name.ToLower() ?? "") != (My.MyProject.Forms.Whatsnew.Name.ToLower() ?? ""))
                                {
                                    j_child.Add(ctrl.Name + ".Text", ctrl.Text);
                                }
                                else
                                {
                                    try
                                    {
                                        if (!ins.Controls.OfType<UI.WP.TabControl>().ElementAt(0).TabPages.Cast<TabPage>().SelectMany(tp => tp.Controls.OfType<Control>()).Contains(ctrl) & !(ctrl is TabPage))
                                        {
                                            j_child.Add(ctrl.Name + ".Text", ctrl.Text);
                                        }
                                    }
                                    catch
                                    {
                                        j_child.Add(ctrl.Name + ".Text", ctrl.Text);
                                    }
                                }

                            }

                            if (ctrl.Tag is not null && !string.IsNullOrWhiteSpace(ctrl.Tag.ToString()) && !ctrl.Tag.ToString().All(char.IsDigit))
                            {
                                j_child.Add(ctrl.Name + ".Tag", ctrl.Tag.ToString());
                            }

                        }

                        if (j_ctrl.Count != 0)
                            j_ctrl.Add("Controls", j_child);

                        j_Forms.Add(ins.Name, j_ctrl);
                    }

                    ins.Dispose();
                }
            }
            else
            {
                bool Overwrite = System.IO.File.Exists(File);

                if (Overwrite)
                {
                    JObject OldSource = (JObject)JToken.Parse(System.IO.File.ReadAllText(File));
                    j_Forms = (JObject)OldSource["Forms Strings"];
                }

                foreach (var f in Forms)
                {
                    if ((f.Name.ToLower() ?? "") != (My.MyProject.Forms.BK.Name.ToLower() ?? ""))
                    {
                        JObject j_ctrl = new JObject(), j_child = new JObject();
                        j_ctrl.RemoveAll();
                        j_child.RemoveAll();

                        j_ctrl.Add("Text", f.Text);

                        foreach (var ctrl in f.GetAllControls())
                        {

                            if (!string.IsNullOrWhiteSpace(ctrl.Text) && !ctrl.Text.All(char.IsDigit) && !(ctrl.Text.Count() == 1) && !((ctrl.Text ?? "") == (ctrl.Name ?? "")))
                            {

                                if ((f.Name.ToLower() ?? "") != (My.MyProject.Forms.Whatsnew.Name.ToLower() ?? ""))
                                {
                                    j_child.Add(ctrl.Name + ".Text", ctrl.Text);
                                }
                                else
                                {
                                    try
                                    {
                                        if (!f.Controls.OfType<UI.WP.TabControl>().ElementAt(0).TabPages.Cast<TabPage>().SelectMany(tp => tp.Controls.OfType<Control>()).Contains(ctrl) & !(ctrl is TabPage))
                                        {
                                            j_child.Add(ctrl.Name + ".Text", ctrl.Text);
                                        }
                                    }
                                    catch
                                    {
                                        j_child.Add(ctrl.Name + ".Text", ctrl.Text);
                                    }
                                }

                            }

                            if (!string.IsNullOrWhiteSpace(ctrl.Tag.ToString()))
                            {
                                j_child.Add(ctrl.Name + ".Tag", ctrl.Tag.ToString());
                            }

                        }

                        if (j_ctrl.Count != 0)
                            j_ctrl.Add("Controls", j_child);

                        if (Overwrite)
                        {
                            j_Forms[f.Name] = j_ctrl;
                        }
                        else
                        {
                            j_Forms.Add(f.Name, j_ctrl);
                        }

                    }
                }
            }

            JSON_Overall.Add("Information", j_info);
            JSON_Overall.Add("Global Strings", j_globalstrings);
            JSON_Overall.Add("Forms Strings", j_Forms);

            System.IO.File.WriteAllText(File, JSON_Overall.ToString());
        }

    }

    public static class FormLangHelper
    {

        public static void LoadLanguage(this Form Form, Localizer Localizer = null)
        {
            if (Localizer is null)
            {
                if (My.Env.Settings.Language.Enabled && File.Exists(My.Env.Settings.Language.File))
                    My.Env.Lang.LoadFromStrings(Form);
            }
            else
            {
                Localizer.LoadFromStrings(Form);
            }
        }

    }
}