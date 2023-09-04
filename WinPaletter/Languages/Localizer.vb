Imports System.IO
Imports System.Reflection
Imports System.Runtime.CompilerServices
Imports Newtonsoft.Json.Linq
Imports WinPaletter.XenonCore

Public Class Localizer : Implements IDisposable

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region
    Sub New()

    End Sub


    Public JSON As JObject
    Dim J_Information As JObject
    Dim J_GlobalStrings As JObject
    Dim J_Forms As JObject
    Dim Deserialized_FormsJSONTree As New List(Of Tuple(Of String, String, String, String))()

#Region "Language Info"
    Property Name As String = Environment.UserName
    Property TranslationVersion As String = "1.0"
    Property Lang As String = "English"
    Property LangCode As String = "EN-US"
    Property AppVer As String = "1.0.0.0"
    Property RightToLeft As Boolean = False
#End Region

#Region "Deep-In-Code Strings"
    Property OK As String = "OK"
    Property [Next] As String = "Next"
    Property Finish As String = "Finish"
    Property Yes As String = "Yes"
    Property No As String = "No"
    Property Cancel As String = "Cancel"
    Property Close As String = "Close"
    Property Retry As String = "Retry"
    Property ExpandNote As String = "Expand note"
    Property CollapseNote As String = "Collapse note"
    Property Bug_NoReport As String = "There is no previous saved report in ""{0}"""
    Property NewUpdate As String = "A new update is available"
    Property By As String = "By"
    Property Done As String = "Done"
    Property Show As String = "Show"
    Property Hide As String = "Hide"
    Property InputValue As String = "Input value"
    Property InputThemeRepos As String = "Type a URL to a WinPaletter themes database"
    Property InputThemeRepos_Notice As String = "This database must follow the guidelines demonstrated in WinPaletter Store documentation"
    Property ItMustBeNumerical As String = "It must be a numerical value"
    Property BugReport_Title As String = "There is a {0} error. Please try again or report this in GitHub issues"
    Property CurrentMode As String = "Current Mode"
    Property SaveMsg As String = "Do you want to save settings?"
    Property SettingsSaved As String = "Settings are saved"
    Property TBSizeUnit As String = "TB"
    Property GBSizeUnit As String = "GB"
    Property MBSizeUnit As String = "MB"
    Property KBSizeUnit As String = "KB"
    Property ByteSizeUnit As String = "B"
    Property SecondUnit As String = "/s"
    Property Stable As String = "Stable"
    Property Beta As String = "Beta"
    Property Channel As String = "Channel"
    Property AndBelow As String = "and below"
    Property InvalidTheme As String = "Error: Invalid theme file."
    Property ThemeNotExist As String = "Theme file doesn't exist."
    Property SettingsFileNotExist As String = "Settings file doesn't exist"
    Property SettingsFileNotJSON As String = "This settings file is not a valid JSON"
    Property OS_Win11 As String = "Windows 11"
    Property OS_Win10 As String = "Windows 10"
    Property OS_Win8 As String = "Windows 8"
    Property OS_Win81 As String = "Windows 8.1"
    Property OS_Win7 As String = "Windows 7"
    Property OS_WinVista As String = "Windows Vista"
    Property OS_WinXP As String = "Windows XP"
    Property OS_WinUndefined As String = "Windows 11 or higher"
    Property Win11ColorsDescTip As String = "These description labels are dependent on the latest stable Windows 11. If your Windows is outdated, these labels might not be the same as your current system."
    Property Win11ColorsDescTip2 As String = "And if you installed ExplorerPatcher and uninstalled it, WinPaletter will detect ExplorerPatcher is still installed (due do ExplorerPatcher registry remnants) and so the descriptions will be different. You can solve this by going to Settings > ExplorerPatcher and then disable preview synchronization."
    Property AltTab_Unsupported As String = "Windows Switcher isn't supported in {0} as it is a classic switcher that can't be changed by registry. Change the preview to another OS and try again."
    Property VistaLogonNotSupported As String = "Editing Windows Vista LogonUI with registry is not supported. Change the preview to another OS and try again."
    Property MonitorIssue As String = "Error occurred during loading registry monitor (Used in real-time-detection of wallpaper\dark mode change from registry). Resetting your wallpaper may fix the issue."
    Property MonitorIssue2 As String = "Anyway, loading will continue without it."
    Property LogoffQuestion As String = "Are you sure you want to log off?"
    Property LogoffAlert1 As String = "This will close all open files and applications"
    Property LogoffAlert2 As String = "Logoff equals 'Sign out' in Windows 8.1, 10 & 11"
    Property LogoffNotFound As String = "Couldn't find logoff process in ""{0}"" directory. You should logoff your Windows profile manually."
    Property WallpaperTone_Notice As String = "This is for {0}. To change another OS preferences, switch the preview in main form"
    Property KillingExplorer As String = "Killing Explorer (To be restarted)"
    Property ExplorerRestarted As String = "Explorer Restarted. It took about {0} seconds to kill explorer"
    Property ErrorExplorerRestart As String = "Error in restarting explorer. Relaunch it in Task Manager (Open Task Manager > Run new task > Type ""Explorer.exe"" and launch)"
    Property Scaling As String = "Scaling"
    Property LanguageRestart As String = "To apply this language, save settings and restart WinPaletter."
    Property CP_11_StartMenu_Taskbar_AC As String = "Start menu, taskbar && action center"
    Property CP_11_ACHover_Links As String = "Action center hover && links"
    Property CP_11_Taskbar_ACHover_Links As String = "Taskbar color, action center hover && links"
    Property CP_EP_ACButton_TaskbarAppLine As String = "Action center buttons && taskbar app underline"
    Property CP_11_Lines_Toggles_Buttons As String = "Lines, toggles, buttons, taskbar app underline && volume slider"
    Property CP_11_Lines_Toggles_Buttons_Overflow As String = "Lines, toggles, buttons, volume slider && taskbar tray (overflow)"
    Property CP_11_OverflowTray As String = "Taskbar tray overflow (requires 22H2, accent on taskbar enabled)"
    Property CP_11_StartMenu_AC As String = "Start menu && action center colors"
    Property CP_11_UnreadBadge As String = "Unread notifications count badge and other circles"
    Property CP_11_Settings As String = "Settings icons, text selection, focus dots && some pressed buttons"
    Property CP_11_SettingsAndTaskbarAppUnderline As String = "Settings icons, taskbar app underline, some pressed buttons && others"
    Property CP_11_SomePressedButtons As String = "Some pressed buttons"
    Property CP_UWPBackground As String = "UWP dialogs background (Windows 8.1 remnants in {0})"
    Property CP_10_ACLinks As String = "Action center links"
    Property CP_10_ACLinks_StartContextMenu As String = "Action center links and start's context menus"
    Property CP_10_TaskbarAppUnderline As String = "Taskbar app underline"
    Property CP_10_StartMenuIconHover As String = "Start menu icon hover"
    Property CP_10_Settings_Links_SomeBtns As String = "Settings icons, links && some pressed buttons"
    Property CP_10_Settings_Links_Taskbar_SomeBtns As String = "Settings icons, links, taskbar focused app && some pressed buttons"
    Property CP_10_Settings_Links_TaskbarUndeline_SomeBtns As String = "Settings icons, links, taskbar app underline && some pressed buttons"
    Property CP_10_Hamburger As String = "Sliding hamburger menu"
    Property CP_10_StartMenu_AC As String = "Start menu && action center"
    Property CP_EP_StartMenu_OverflowMenus As String = "Start menu && overflow menus"
    Property CP_EP_StartMenu_ActionCenterButtons As String = "Start menu && action center buttons"
    Property CP_EP_ActionCenterBackground As String = "Action center background"
    Property CP_10_StartMenu_AC_TaskbarActiveApp As String = "Start menu, action center && taskbar active app"
    Property CP_10_Taskbar As String = "Taskbar"
    Property CP_10_Taskbar_StartContextMenu As String = "Taskbar and start's context menus"
    Property CP_10_StartContextMenu As String = "Start's context menus"
    Property CP_EP_Taskbar_AppUnderline As String = "Taskbar && app underline"
    Property CP_10_Taskbar_ACLinks_StartContextMenu As String = "Taskbar background color, action center links && start's context menus"
    Property CP_10_TaskbarFocusedApp_StartButtonHover As String = "Taskbar focused app && Start menu button hover"
    Property CP_Undefined As String = "Undefined"
    Property CP_AccentOnTaskbarTib As String = "Applying accent on taskbar only isn't effective only for Windows 10 2015 versions, but it is effective for higher versions."
    Property CP_Time As String = "It took {0} seconds"
    Property CP_Time_They As String = "They took {0} seconds"
    Property CP_Time_Cursors As String = "Total applying Windows cursors took {0} seconds"
    Property CP_ApplyFrom As String = "WinPaletter will apply theme from {0}'s section"
    Property CP_Admin_Msg0 As String = "Writing to registry without administrator rights by deflection"
    Property CP_Admin_Msg1 As String = "This deflection will take time more than if started as administrator"
    Property CP_Applying_Started As String = "Applying started"
    Property CP_SavingInfo As String = "Saving theme info into registry"
    Property CP_ThemeReset As String = "Resetting theme to default Windows to apply new theme correctly"
    Property CP_Applying_Win11 As String = "Applying Windows 11 scheme"
    Property CP_Applying_Win10 As String = "Applying Windows 10 scheme"
    Property CP_Applying_Win81 As String = "Applying Windows 8.1 scheme"
    Property CP_Applying_Win7 As String = "Applying Windows 7 theme"
    Property CP_Applying_WinVista As String = "Applying Windows Vista theme"
    Property CP_Applying_WinXP As String = "Applying Windows XP theme"
    Property CP_Applying_LogonUI11 As String = "Applying Windows 11 LogonUI"
    Property CP_Applying_LogonUI10 As String = "Applying Windows 10 LogonUI"
    Property CP_Applying_LogonUI8 As String = "Applying Windows 8.1 Lock Screen"
    Property CP_Applying_LogonUI7 As String = "Applying Windows 7 LogonUI"
    Property CP_Applying_LogonUIXP As String = "Applying Windows XP LogonUI"
    Property CP_Applying_Win32UI As String = "Applying Classic Colors"
    Property CP_Applying_WinEffects As String = "Applying Windows Effects"
    Property CP_Applying_WallpaperTone As String = "Applying Wallpaper Tone"
    Property CP_Applying_DesktopAllUsers As String = "Applying Wallpaper for all users"
    Property CP_Applying_CMD As String = "Applying Command Prompt"
    Property CP_Applying_Metrics As String = "Applying Windows Metrics and Fonts"
    Property CP_Applying_AltTab As String = "Applying Windows Switcher (Alt+Tab) appearance"
    Property CP_Applying_Wallpaper As String = "Applying Wallpaper"
    Property CP_Applying_AppTheme As String = "Applying WinPaletter application theme"
    Property CP_Applying_TerminalPreview As String = "Applying Windows Terminal Preview"
    Property CP_Applying_ScreenSaver As String = "Applying Screen Saver"
    Property CP_Applying_Sounds As String = "Applying Sounds"
    Property CP_Check_Terminals As String = "Checking if Windows Terminal (Stable & Preview) are installed"
    Property CP_Check_TerminalStable As String = "Checking if Windows Terminal Stable is installed"
    Property CP_Check_TerminalPreview As String = "Checking if Windows Terminal Preview is installed"
    Property CP_Skip_TerminalPreview As String = "Skipping Windows Terminal Preview as it is disabled"
    Property CP_Skip_TerminalStable As String = "Skipping Windows Terminal Stable as it is disabled"
    Property CP_Skip_Terminals As String = "Skipping Windows Terminal Stable & Preview as they are disabled"
    Property CP_Skip_Terminals_NotSupported As String = "Skipping Windows Terminal Stable, Preview. Not supported in this OS"
    Property CP_Skip_TerminalPreview_NotInstalled As String = "Skipping Windows Terminal Preview as it isn't installed"
    Property CP_Skip_TerminalPreview_DeflectionNotFound As String = "Skipping Windows Terminal Preview as deflected JSON doesn't exist"
    Property CP_Skip_TerminalStable_NotInstalled As String = "Skipping Windows Terminal Stable as it isn't installed"
    Property CP_Skip_TerminalStable_DeflectionNotFound As String = "Skipping Windows Stable Preview as deflected JSON doesn't exist"
    Property CP_Skip_CMD As String = "Skipping Command Prompt as it is disabled"
    Property CP_Applying_PS64 As String = "Applying PowerShell x64"
    Property CP_Skip_PS64 As String = "Skipping PowerShell x64 as it is disabled"
    Property CP_Applying_PS32 As String = "Applying PowerShell x86"
    Property CP_Skip_PS32 As String = "Skipping PowerShell x86 as it is disabled"
    Property CP_Skip_Metrics As String = "Skipping Windows Metrics and Fonts as they are disabled"
    Property CP_Skip_AltTab As String = "Skipping Windows Switcher (Alt+Tab) Appearance as it is disabled"
    Property CP_Skip_Wallpaper As String = "Skipping Wallpaper as it is disabled"
    Property CP_Skip_AppTheme As String = "Skipping WinPaletter application theme as it is disabled"
    Property CP_Skip_Sounds As String = "Skipping Sounds as its toggle is disabled"
    Property CP_CMD_Error As String = "Error occurred during applying Command Prompt"
    Property CP_PS32_Error As String = "Error occurred during applying PowerShell x86"
    Property CP_PS64_Error As String = "Error occurred during applying PowerShell x64"
    Property CP_WIN32UI_Error As String = "Error occurred during applying Classic Colors"
    Property CP_WinEffects_Error As String = "Error occurred during applying Windows Effects"
    Property CP_WallpaperTone_Error As String = "Error occurred during applying Wallpaper Tone"
    Property CP_LogonUI11_Error As String = "Error occurred during applying Windows 11 LogonUI"
    Property CP_LogonUI10_Error As String = "Error occurred during applying Windows 10 LogonUI"
    Property CP_LogonUI8_Error As String = "Error occurred during applying Windows 8.1 Lock Screen"
    Property CP_LogonUI7_Error As String = "Error occurred during applying Windows 7 LogonUI"
    Property CP_LogonUIXP_Error As String = "Error occurred during applying Windows XP LogonUI"
    Property CP_W10_Error As String = "Error occurred during applying Windows 10 scheme"
    Property CP_SavingInfo_Error As String = "Error occurred during saving theme info into registry"
    Property CP_ThemeReset_Error As String = "Error occurred during resetting theme to default Windows"
    Property CP_W11_Error As String = "Error occurred during applying Windows 11 scheme"
    Property CP_W7_Error As String = "Error occurred during applying Windows 7 theme"
    Property CP_WVista_Error As String = "Error occurred during applying Windows Vista theme"
    Property CP_W81_Error As String = "Error occurred during applying Windows 8.1 scheme"
    Property CP_WXP_Error As String = "Error occurred during applying Windows XP theme"
    Property CP_Error_Cursors As String = "Error occurred during applying Windows Cursors"
    Property CP_Error_SetDesktop As String = "Error occurred during applying Desktop for all users"
    Property CP_Error_Metrics As String = "Error occurred during applying Windows Metrics and Fonts"
    Property CP_Error_AltTab As String = "Error occurred during applying Windows Switcher (Alt+Tab) appearance"
    Property CP_Error_Wallpaper As String = "Error occurred during applying Wallpaper"
    Property CP_Error_AppTheme As String = "Error occurred during applying WinPaletter application theme"
    Property CP_Error_TerminalPreview As String = "Error occurred during applying Windows Terminal Preview"
    Property CP_Error_TerminalStable As String = "Error occurred during applying Windows Terminal Stable"
    Property CP_Error_ScreenSaver As String = "Error occurred during applying Screen Saver"
    Property CP_Error_Sounds As String = "Error occurred during applying Sounds"
    Property CP_MetricsHighDPIAlert As String = "Please Logoff and Logon after setting Metrics and Fonts with a high DPI"
    Property CP_Restricted_Cursors As String = "Modifying Windows Cursors is restricted from settings"
    Property CP_Applying_TerminalStable As String = "Applying Windows Terminal Stable"
    Property CP_AppliedWithErrors As String = "Applying theme done but with error/s. It took {0} seconds"
    Property CP_Applied As String = "Applying theme done. It took {0} seconds"
    Property CP_AllDone As String = "All operations are done"
    Property CP_ErrorHappened As String = "Error\s happened. Press on ""Show Errors"" for details"
    Property CP_LogWillClose As String = "This log will close after {0} second\s"
    Property CP_RestoreCursorsError As String = "Error occurred during resetting cursors to aero. Anyway, process will continue."
    Property CP_RestoreCursorsErrorPressOK As String = "Pressing OK will show details of exception error."
    Property CP_RestoreCursorsTip As String = "If you want to restore default cursors, go to Control Panel > Mouse > Pointers"
    Property CP_UpdateDLL_AsAdmin_Error0 As String = "You must be running WinPaletter as Administrator to update resources of ""{0}"""
    Property CP_UpdateDLL_AsAdmin_Error1 As String = "This process is required for changing Windows startup sound"
    Property CP_UpdateDLL_Error As String = "Couldn't update {0} resources. Please report this in Github issues with error {1}."
    Property CP_Wallpaper_NonBMP0 As String = "Due to odd reason, Windows XP, Vista & 7 can't set an image that is not a bitmap format directly as a wallpaper."
    Property CP_Wallpaper_NonBMP1 As String = "Do you want to convert the current image to have internal bitmap format? (It will still have the same file extension)"
    Property Lang_HasLeftToRight As String = "It has left to right layout"
    Property Lang_HasRightToLeft As String = "It has right to left layout"
    Property CommandPrompt As String = "Command Prompt"
    Property PowerShellx86 As String = "PowerShell x86"
    Property PowerShellx64 As String = "PowerShell x64"
    Property Open_Testing_CMD As String = "Open Command Prompt for testing"
    Property Open_Testing_PowerShellx86 As String = "Open PowerShell x86 for testing"
    Property Open_Testing_PowerShellx64 As String = "Open PowerShell x64 for testing"
    Property TerminalStable As String = "Windows Terminal Stable"
    Property TerminalPreview As String = "Windows Terminal Preview"
    Property Terminal_TypeSchemeName As String = "Type theme name here:"
    Property [Default] As String = "Default"
    Property Untitled As String = "Untitled"
    Property WhatsNewInVersion As String = "What's new in {0}!"
    Property ThisWillRestartExplorer As String = "This will restart the explorer, don't worry this won't close other applications"
    Property LogoffNotice As String = "This will logoff your Windows account. Please save your open files before logging-off"
    Property TitlebarColorNotice As String = "Windows volume slider, UAC and Windows 10 logonUI follow active titlebar color"
    Property NoDefResExplorer As String = "Restarting Explorer is disabled. If theme is not applied correctly, restart it"
    Property RemoveExtMsg As String = "Are you sure from removing files association (*.wpth, *.wptp, *.wpsf) from registry?"
    Property RemoveExtMsgNote As String = "Note: You can reassociate them by activating its checkbox and restarting WinPaletter"
    Property EmptyName As String = "You can't leave theme name empty. Please type a name to it"
    Property EmptyAuthorName As String = "You can't leave author's name empty. Please type author's name or your name."
    Property EmptyVer As String = "You can't leave theme version empty. Please type a version to it in this style (x.x.x.x), replacing (x) by numbers"
    Property WrongVerFormat As String = "Wrong version format. Please type the version to it in this style (x.x.x.x), replacing (x) by numbers."
    Property Extracting As String = "Extracting palette from image depends on your device's performance, maximum palette colors number, image quality and its resolution ..."
    Property Version As String = "Version"
    Property Checking As String = "Checking ..."
    Property DoAction_Update As String = "Do action"
    Property NoUpdateAvailable As String = "No update is available"
    Property CheckForUpdates As String = "Check for update"
    Property NetworkError As String = "Network error. Check you internet connection."
    Property UpdatesOSNoTLS12 As String = "Updates won't work as TLS 1.2 protocol isn't enabled in {0}. Use GitHub instead"
    Property ServerError As String = "Error: Network issues or GitHub repository is private or deleted. Visit GitHub page for details."
    Property Msgbox_Downloaded As String = "Downloaded successfully"
    Property LngExported As String = "Language exported successfully"
    Property ScalingTip As String = "Scaling option is only a preview, the cursor will be saved with different sizes and the suitable size will be loaded according to your DPI settings."
    Property LngShouldClose As String = "You should close the app if you want to export language."
    Property CMD_Enable As String = "You should enable terminal editing from the toggle above."
    Property CMD_NotAllWeights As String = "Not all weights are available according to your OS and the font itself. Normal and Bold ones are the basic ones."
    Property ExtTer_NotFound As String = "Terminal not found. Select a valid one from the list."
    Property ExtTer_Set As String = "Terminal preferences are set successfully!"
    Property ExtTer_NewSuccess As String = "This key is entered into registry successfully."
    Property ExtTer_NewError As String = "Couldn't write this entry to registry. Please check if this key already exists or check permissions."
    Property ErrorDetails As String = "Error details: "
    Property Terminal_alreadyset As String = "You can't set this name as it is already set to another profile."
    Property TerminalStable_notFound As String = "Windows Terminal Stable isn't installed or ""settings.json"" isn't accessible."
    Property TerminalPreview_notFound As String = "Windows Terminal Preview isn't installed or ""settings.json"" isn't accessible."
    Property PowerShellx86_notFound As String = "Microsoft PowerShell x86 is not installed."
    Property PowerShellx64_notFound As String = "Microsoft PowerShell x64 is not installed."
    Property Terminal_supposed As String = "It is supposed to be located in: "
    Property Terminal_Bypass As String = "You can bypass this restriction in Settings > Terminals (In case you want to design a theme for all versions of Windows and save it as a file for sharing, not applying it)."
    Property Terminal_CantRun As String = "You can't run Windows Terminal in current OS. It is available only in Windows 10 and 11."
    Property Terminal_ErrorFile As String = "Error occurred while reading settings file: "
    Property Terminal_ProfileNotCloneable As String = "Default Profile isn't cloneable, select a different profile."
    Property Terminal_ThemeNotCloneable As String = "Default themes (Dark\Light\System) are not cloneable, select a different theme or create a new theme if you want to clone."
    Property Terminal_Clone As String = "Clone"
    Property Terminal_NewProfile As String = "New profile"
    Property Terminal_NewScheme As String = "New scheme"
    Property Terminal_NewTheme As String = "New theme"
    Property Terminal_External_Empty As String = "Terminal can't be empty. Enter a valid one."
    Property Terminal_External_NotExist As String = "Terminal doesn't exist. Enter a valid one."
    Property Terminal_External_Reversed As String = "This terminal is reserved for system. Try again with another one."
    Property Terminal_External_Exists As String = "This terminal already exists. Try again with another one."
    Property CP_RenderingCustomLogonUI_MayNotRespond As String = "WinPaletter may not respond while rendering custom LogonUI"
    Property CP_RenderingCustomLogonUI_Progress As String = "Rendering custom LogonUI:"
    Property CP_RenderingCustomLogonUI As String = "Rendering custom LogonUI"
    Property CP_SavingCursorsColors As String = "Saving Windows Cursors Colors to registry"
    Property CP_RenderingCursors As String = "Rendering Windows Cursors"
    Property CP_RenderingCursors_Error As String = "Error occurred during rendering Windows Cursors"
    Property CP_CursorsApplying_Error As String = "Error occurred during applying Windows Cursors"
    Property CP_ApplyingCursors As String = "Applying Windows Cursors"
    Property OldMSTheme_Copyrights As String = "Copyright © Microsoft Corp. 1995-{0}"
    Property OldMSTheme_CreatedFromAppVer As String = "Created from application version {0}"
    Property OldMSTheme_ProgrammedBy As String = "This theme was designed by WinPaletter, programmed by {0}"
    Property OldMSTheme_CreatedBy As String = "Created by {0}"
    Property OldMSTheme_ThemeName As String = "Theme name: {0}"
    Property OldMSTheme_ThemeVersion As String = "Theme version: {0}"

    Property Store_RemoveTip As String = "You can't remove an essential themes database repository. Try again with another custom repository."
    Property Store_NoNetwork As String = "No internet connection"
    Property Store_TryOffline As String = "Press 'Yes' if you want to continue in offline Store mode. You can select its folders from Settings > Store."
    Property Store_Ping As String = "Testing access to '{0}'"
    Property Store_PingFailed As String = "Couldn't get response from '{0}'. Skipping this themes database"
    Property Store_Accessing As String = "Accessing themes database from '{0}'"
    Property Store_UpdateTheme As String = "Updating theme '{0}' from '{1}'"
    Property Store_DownloadTheme As String = "Downloading theme '{0}' from '{1}'"
    Property Store_DownloadingPackForTheme As String = "Downloading resources pack for theme '{0}' version {1}"
    Property Store_LoadingTheme As String = "Loading theme '{0}'"
    Property Store_LowAppVersionAlert As String = "This theme is designed by WinPaletter {0} which is higher than {1}. please update WinPaletter to enjoy all new themes aspects and avoid errors."
    Property Store_CleaningFromMemory As String = "Cleaning store items from memory"
    Property Store_NoIncludedData As String = "There is no included data"
    Property Store_WontWork_Protocol As String = "Unfortunately, WinPaletter Store won't work as TLS 1.2 protocol isn't enabled in {0}. Do you want to continue?"
    Property Store_ThemeDesignedFor0 As String = "This theme can be applied to all supported versions of Windows, but it was designed for:"
    Property Store_ThemeDesignedFor1 As String = "This theme can be applied to all supported versions of Windows:"
    Property Store_LogoffRecommended As String = "It is recommended to logoff your Windows and logon to apply all effects of the theme"
    Property Store_Calculating As String = "Calculating ..."
    Property Store_AuthorURLRedirect As String = "This will redirect you to author's social media URL. Do you want to continue?"

    Property Store_Toggle_AppTheme As String = "WinPaletter application theme"
    Property Store_Toggle_LogonUI As String = "LogonUI screen"
    Property Store_Toggle_Cursors As String = "Cursors"
    Property Store_Toggle_CMD As String = "Command Prompt"
    Property Store_Toggle_PS86 As String = "PowerShell x86"
    Property Store_Toggle_PS64 As String = "PowerShell x64"
    Property Store_Toggle_TerminalStable As String = "Windows Terminal Stable"
    Property Store_Toggle_TerminalPreview As String = "Windows Terminal Preview"
    Property Store_Toggle_MetricsFonts As String = "Metrics & Fonts"
    Property Store_Toggle_WindowsEffects As String = "Windows Effects"
    Property Store_Toggle_AltTab As String = "Windows Switcher (Alt+Tab appearance)"
    Property Store_Toggle_Wallpaper As String = "Wallpaper"
    Property Store_Toggle_Sounds As String = "Sounds"
    Property Store_Toggle_ScreenSaver As String = "Screen Saver"

    Property Convert_JSON_To_Old As String = "Theme file is JSON-internally-formatted. When you export this theme, it will be with old formatting system (valid for WinPaletter 1.0.7.6 and less)."
    Property Convert_Old_To_JSON As String = "Theme file is old-formatted. When you export this theme, it will be JSON-internally-formatted (valid for WinPaletter 1.0.7.7 and higher). It supports contents compression that is useful for uploading more amount of themes to WinPaletter Store with less downloading duration, and used resources pack export that is useful for downloading missing used resources (wallpapers, images and sounds) from WinPaletter Store (or by external sharing) and applying them automatically with the theme."
    Property Convert_Error_Phrasing As String = "Error occurred while phrasing theme file"
    Property Convert_Done As String = "Theme file is converted and exported successfully"
    Property Convert_Detect_Old_OnLoading0 As String = "WinPaletter detected that you are using an old theme format"
    Property Convert_Detect_Old_OnLoading1 As String = "Do you want to convert it and load it automatically?"
    Property Convert_Detect_Old_OnLoading2 As String = "Pressing 'No' will load a default Windows preset"
#End Region

#Region "Language loader"
    Public Sub LoadLanguageFromJSON(File As String, Optional [_Form] As Form = Nothing)
        If IO.File.Exists(File) Then

            Using St As New StreamReader(File)
                JSON = JObject.Parse(St.ReadToEnd)
                St.Close()
            End Using

            J_Information = New JObject
            J_GlobalStrings = New JObject
            J_Forms = New JObject

            Dim Valid As Boolean = JSON.ContainsKey("Information") And JSON.ContainsKey("Global Strings") And JSON.ContainsKey("Forms Strings")

            If Not Valid Then
                '$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$

                Exit Sub
            End If

            J_Information = JSON("Information")
            J_GlobalStrings = JSON("Global Strings")
            J_Forms = JSON("Forms Strings")

            LoadInnerStrings(J_Information, J_GlobalStrings)
            DeserializeFormsJSONIntoTreeList(J_Forms)
            LoadFromStrings([_Form])

        End If
    End Sub

    Public Sub LoadInnerStrings(LangInfo As JObject, Strings As JObject)
        Dim type1 As Type = [GetType]() : Dim properties1 As PropertyInfo() = type1.GetProperties()

        For Each [property] As PropertyInfo In properties1
            If Not [property].Name.ToLower = "Name".ToLower _
             AndAlso Not [property].Name.ToLower = "TranslationVersion".ToLower _
             AndAlso Not [property].Name.ToLower = "Lang".ToLower _
             AndAlso Not [property].Name.ToLower = "LangCode".ToLower _
             AndAlso Not [property].Name.ToLower = "AppVer".ToLower _
             AndAlso Not [property].Name.ToLower = "RightToLeft".ToLower Then

                If Strings.ContainsKey([property].Name.ToLower) Then [property].SetValue(Me, Convert.ChangeType(Strings([property].Name.ToLower), [property].PropertyType))
            Else
                If LangInfo.ContainsKey([property].Name.ToLower) Then [property].SetValue(Me, Convert.ChangeType(LangInfo([property].Name.ToLower), [property].PropertyType))
            End If
        Next
    End Sub

    Public Sub DeserializeFormsJSONIntoTreeList(JSON_Forms As JObject)

        'Tuple of four values; form name, control name, property, property value
        'If there is no control and you want to change form property, make control name: String.Empty
        Deserialized_FormsJSONTree.Clear()

        Dim FormName, ControlName, Prop, Value As String
        FormName = String.Empty
        ControlName = String.Empty
        Prop = String.Empty
        Value = String.Empty

        'Loop through all forms nodes in JSON
        For Each F In JSON_Forms
            Try

                'Get one form node
                'There is only one specific property "Text"
                Dim J_Specific_Form As New JObject
                J_Specific_Form = JSON_Forms(F.Key)
                FormName = F.Key
                ControlName = String.Empty
                Prop = "Text"

                If J_Specific_Form.ContainsKey("Text") Or J_Specific_Form.ContainsKey("text") Or J_Specific_Form.ContainsKey("TEXT") Then
                    If J_Specific_Form.ContainsKey("Text") Then Value = J_Specific_Form("Text")
                    If J_Specific_Form.ContainsKey("text") Then Value = J_Specific_Form("text")
                    If J_Specific_Form.ContainsKey("TEXT") Then Value = J_Specific_Form("TEXT")
                    Deserialized_FormsJSONTree.Add(New Tuple(Of String, String, String, String)(FormName, ControlName, Prop, Value))
                End If

                'If this form has a control/controls then get them
                If J_Specific_Form.ContainsKey("Controls") Or J_Specific_Form.ContainsKey("controls") Or J_Specific_Form.ContainsKey("CONTROLS") Then

                    'JSON nodes of all child controls
                    Dim J_Controls As New JObject
                    If J_Specific_Form.ContainsKey("Controls") Then J_Controls = J_Specific_Form("Controls")
                    If J_Specific_Form.ContainsKey("controls") Then J_Controls = J_Specific_Form("controls")
                    If J_Specific_Form.ContainsKey("CONTROLS") Then J_Controls = J_Specific_Form("CONTROLS")

                    'Loop through all child controls JSON nodes
                    For Each ctrl In J_Controls
                        Try
                            'If there is a dot in JSON node value, then there is a specific mentioned property,
                            'if not, then it is a "Text" property only.
                            If ctrl.Key.Contains(".") Then
                                ControlName = ctrl.Key.Split(".")(0)
                                Prop = ctrl.Key.Split(".")(1)
                                Value = ctrl.Value
                                Deserialized_FormsJSONTree.Add(New Tuple(Of String, String, String, String)(FormName, ControlName, Prop, Value))
                            Else
                                ControlName = ctrl.Key
                                Prop = "Text"
                                Value = ctrl.Value
                                Deserialized_FormsJSONTree.Add(New Tuple(Of String, String, String, String)(FormName, ControlName, Prop, Value))
                            End If
                        Catch
                        End Try
                    Next
                End If

            Catch
            End Try
        Next

    End Sub

    Public Sub LoadFromStrings(Optional [_Form] As Form = Nothing)
        If [_Form] IsNot Nothing Then
            Dim WasVisible As Boolean = [_Form].Visible

            If WasVisible Then
                [_Form].Visible = False
            End If

            Populate(Deserialized_FormsJSONTree, [_Form])

            If WasVisible Then [_Form].Visible = True
        End If
    End Sub

    Public Sub FormLoadEventHandler(sender As Object, e As EventArgs)
        If sender IsNot Nothing Then CType(sender, Form).LoadLanguage
    End Sub

    Sub Populate(ByVal PopCtrlList As List(Of Tuple(Of String, String, String, String)), [Form] As Form)
        'Item1 = FormName
        'Item2 = ControlName
        'Item3 = Prop
        'Item4 = Value

        For Each member In PopCtrlList
            Try
                If [Form].Name.ToLower = member.Item1.ToLower Then

                    If member.Item2 = String.Empty Then
                        '# Form
                        Try : If member.Item3.ToLower = "text" Then [Form].SetText(member.Item4)
                        Catch : End Try

                        Try : If member.Item3.ToLower = "tag" Then SetCtrlTag(member.Item4.ToString, [Form])
                        Catch : End Try
                    Else
                        '# Control

                        If Not String.IsNullOrEmpty(member.Item2) Then

                            For Each ctrl As Control In [Form].Controls.Find(member.Item2, True)

                                Try : If member.Item3.ToLower = "text" Then
                                        If member.Item1.ToLower <> Whatsnew.Name.ToLower Then
                                            ctrl.SetText(member.Item4.ToString)
                                        Else
                                            If Not Whatsnew.XenonTabControl1.TabPages.Cast(Of TabPage)().SelectMany(Function(tp) tp.Controls.OfType(Of Control)()).Contains(ctrl) _
                                                And TypeOf ctrl IsNot TabPage Then
                                                ctrl.SetText(member.Item4.ToString)
                                            End If
                                        End If
                                    End If
                                Catch : End Try

                                Try : If member.Item3.ToLower = "tag" Then SetCtrlTag(member.Item4.ToString, ctrl)
                                Catch : End Try

                            Next

                        End If

                    End If
                End If

            Catch ex As Exception
                Throw ex
            End Try
        Next

    End Sub
#End Region

    Public Sub ExportJSON(File As String)
        Dim JSON_Overall As New JObject()
        Dim newL As New Localizer

        Dim j_info As New JObject()
        j_info.RemoveAll()
        j_info.Add("Name".ToLower, newL.Name)
        j_info.Add("TranslationVersion".ToLower, newL.TranslationVersion)
        j_info.Add("Lang".ToLower, newL.Lang)
        j_info.Add("LangCode".ToLower, newL.LangCode)
        j_info.Add("AppVer".ToLower, My.AppVersion)
        j_info.Add("RightToLeft".ToLower, newL.RightToLeft)

        Dim j_globalstrings As New JObject()

        Dim type1 As Type = newL.[GetType]() : Dim properties1 As PropertyInfo() = type1.GetProperties()

        For Each [property] As PropertyInfo In properties1

            If Not String.IsNullOrWhiteSpace([property].GetValue(newL)) _
                 And Not [property].Name.ToLower = "Name".ToLower _
                 And Not [property].Name.ToLower = "TranslationVersion".ToLower _
                 And Not [property].Name.ToLower = "Lang".ToLower _
                 And Not [property].Name.ToLower = "LangCode".ToLower _
                 And Not [property].Name.ToLower = "AppVer".ToLower _
                 And Not [property].Name.ToLower = "RightToLeft".ToLower Then

                j_globalstrings.Add([property].Name.ToLower, [property].GetValue(newL).ToString)
            End If

        Next

        Dim j_Forms As New JObject()

        For Each f In Assembly.GetExecutingAssembly().GetTypes().Where(Function(t) GetType(Form).IsAssignableFrom(t))
            'If allForms.Contains(f.Name) Then
            Dim ins As New Form
            ins = DirectCast(Activator.CreateInstance(f), Form)

            If ins.Name.ToLower <> BK.Name.ToLower Then
                Dim j_ctrl, j_child As New JObject()
                j_ctrl.RemoveAll()
                j_child.RemoveAll()

                j_ctrl.Add("Text", ins.Text)

                For Each ctrl In GetAllControls(ins)

                    If Not String.IsNullOrWhiteSpace(ctrl.Text) AndAlso Not IsNumeric(ctrl.Text) AndAlso Not ctrl.Text.Count = 1 AndAlso Not ctrl.Text = ctrl.Name Then

                        If ins.Name.ToLower <> Whatsnew.Name.ToLower Then
                            j_child.Add(ctrl.Name & ".Text", ctrl.Text)
                        Else
                            Try
                                If Not ins.Controls.OfType(Of XenonTabControl).ElementAt(0).TabPages().Cast(Of TabPage).SelectMany(Function(tp) tp.Controls.OfType(Of Control)()).Contains(ctrl) _
                                                And TypeOf ctrl IsNot TabPage Then
                                    j_child.Add(ctrl.Name & ".Text", ctrl.Text)
                                End If
                            Catch
                                j_child.Add(ctrl.Name & ".Text", ctrl.Text)
                            End Try
                        End If

                    End If

                    If Not String.IsNullOrWhiteSpace(ctrl.Tag) Then
                        j_child.Add(ctrl.Name & ".Tag", ctrl.Tag.ToString)
                    End If

                Next

                If j_ctrl.Count <> 0 Then j_ctrl.Add("Controls", j_child)

                j_Forms.Add(ins.Name, j_ctrl)
            End If

            ins.Dispose()
            'End If
        Next

        JSON_Overall.Add("Information", j_info)
        JSON_Overall.Add("Global Strings", j_globalstrings)
        JSON_Overall.Add("Forms Strings", j_Forms)

        IO.File.WriteAllText(File, JSON_Overall.ToString())
    End Sub

    Private Function GetAllControls(parent As Control) As IEnumerable(Of Control)
        Dim cs = parent.Controls.OfType(Of Control)
        Return cs.SelectMany(Function(c) GetAllControls(c)).Concat(cs)
    End Function

End Class

Public Module FormLangHelper

    <Extension()>
    Public Sub LoadLanguage(Form As Form)
        If My.Settings.Language.Enabled AndAlso IO.File.Exists(My.Settings.Language.File) Then My.Lang.LoadFromStrings(Form)
    End Sub

End Module
