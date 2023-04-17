Imports System.IO
Imports System.Reflection
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

    Public Function GetFormFromName(Name As String) As Form
        If Name.ToLower = "About".ToLower Then Return About
        If Name.ToLower = "ColorPickerDlg".ToLower Then Return ColorPickerDlg
        If Name.ToLower = "ComplexSave".ToLower Then Return ComplexSave
        If Name.ToLower = "dragPreviewer".ToLower Then Return dragPreviewer
        If Name.ToLower = "EditInfo".ToLower Then Return EditInfo
        If Name.ToLower = "LogonUI".ToLower Then Return LogonUI
        If Name.ToLower = "MainFrm".ToLower Then Return MainFrm
        If Name.ToLower = "WhatsNew".ToLower Then Return Whatsnew
        If Name.ToLower = "Updates".ToLower Then Return Updates
        If Name.ToLower = "Win32UI".ToLower Then Return Win32UI
        If Name.ToLower = "SettingsX".ToLower Then Return SettingsX
        If Name.ToLower = "CursorsStudio".ToLower Then Return CursorsStudio
        If Name.ToLower = "LogonUI7".ToLower Then Return LogonUI7
        If Name.ToLower = "LogonUI8Colors".ToLower Then Return LogonUI8Colors
        If Name.ToLower = "LogonUI8_Pics".ToLower Then Return LogonUI8_Pics
        If Name.ToLower = "Start8Selector".ToLower Then Return Start8Selector
        If Name.ToLower = "CMD".ToLower Then Return CMD
        If Name.ToLower = "ExternalTerminal".ToLower Then Return ExternalTerminal
        If Name.ToLower = "NewExtTerminal".ToLower Then Return NewExtTerminal
        If Name.ToLower = "TerminalInfo".ToLower Then Return TerminalInfo
        If Name.ToLower = "TerminalsDashboard".ToLower Then Return TerminalsDashboard
        If Name.ToLower = "WindowsTerminal".ToLower Then Return WindowsTerminal
        If Name.ToLower = "WindowsTerminalDecide".ToLower Then Return WindowsTerminalDecide
        If Name.ToLower = "WindowsTerminalCopycat".ToLower Then Return WindowsTerminalCopycat
        If Name.ToLower = "LicenseForm".ToLower Then Return LicenseForm
        If Name.ToLower = "BugReport".ToLower Then Return BugReport
        If Name.ToLower = "Metrics_Fonts".ToLower Then Return Metrics_Fonts
        If Name.ToLower = "Lang_Add_Snippet".ToLower Then Return Lang_Add_Snippet
        If Name.ToLower = "Lang_Dashboard".ToLower Then Return Lang_Dashboard
        If Name.ToLower = "Lang_JSON_Update".ToLower Then Return Lang_JSON_Update
        If Name.ToLower = "Lang_JSON_Manage".ToLower Then Return Lang_JSON_Manage
        If Name.ToLower = "WallpaperToner".ToLower Then Return WallpaperToner
        If Name.ToLower = "WinEffecter".ToLower Then Return WinEffecter
        If Name.ToLower = "LogonUIXP".ToLower Then Return LogonUIXP
        If Name.ToLower = "VS2Win32UI".ToLower Then Return VS2Win32UI
        If Name.ToLower = "VS2Metrics".ToLower Then Return VS2Metrics
        If Name.ToLower = "Uninstall".ToLower Then Return Uninstall
        If Name.ToLower = "AltTabEditor".ToLower Then Return AltTabEditor
        If Name.ToLower = "Store".ToLower Then Return Store
        If Name.ToLower = "Store_CPToggles".ToLower Then Return Store_CPToggles
        If Name.ToLower = "Store_SearchFilter".ToLower Then Return Store_SearchFilter
        If Name.ToLower = "ScreenSaver_Editor".ToLower Then Return ScreenSaver_Editor
    End Function

    Public allForms As New List(Of String) From {
                        "About",
                        "ColorPickerDlg",
                        "ComplexSave",
                        "dragPreviewer",
                        "EditInfo",
                        "LogonUI",
                        "MainFrm",
                        "Whatsnew",
                        "Updates",
                        "Win32UI",
                        "SettingsX",
                        "CursorsStudio",
                        "LogonUI7",
                        "LogonUI8Colors",
                        "LogonUI8_Pics",
                        "Start8Selector",
                        "CMD",
                        "ExternalTerminal",
                        "NewExtTerminal",
                        "TerminalInfo",
                        "TerminalsDashboard",
                        "WindowsTerminal",
                        "WindowsTerminalDecide",
                        "WindowsTerminalCopycat",
                        "LicenseForm",
                        "BugReport",
                        "Metrics_Fonts",
                        "Lang_Add_Snippet",
                        "Lang_Dashboard",
                        "Lang_JSON_Update",
                        "Lang_JSON_Manage",
                        "WallpaperToner",
                        "WinEffecter",
                        "LogonUIXP",
                        "VS2Win32UI",
                        "VS2Metrics",
                        "Uninstall",
                        "AltTabEditor",
                        "Store",
                        "Store_CPToggles",
                        "Store_SearchFilter",
                        "ScreenSaver_Editor"
                        }

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
    Property CurrentMode As String = "Current Mode"
    Property SaveMsg As String = "Do you want to save settings?"
    Property SettingsSaved As String = "Settings are saved"
    Property TBSizeUnit As String = "TB"
    Property GBSizeUnit As String = "GB"
    Property MBSizeUnit As String = "MB"
    Property KBSizeUnit As String = "KB"
    Property ByteSizeUnit As String = "B"
    Property Stable As String = "Stable"
    Property Beta As String = "Beta"
    Property Channel As String = "Channel"
    Property AndBelow As String = "and below"
    Property InvalidTheme As String = "Error: Invalid theme file."
    Property ThemeNotExist As String = "Theme file doesn't exist."
    Property OS_Win11 As String = "Windows 11"
    Property OS_Win10 As String = "Windows 10"
    Property OS_Win8 As String = "Windows 8.1"
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
    Property LogoffAlert1 As String = "This will close all open files\applications"
    Property LogoffAlert2 As String = "Logoff equals Sign Out in Windows 8.1/10/11"
    Property WallpaperTone_Notice As String = "This is for {0}. To change another OS preferences, switch the preview in Main Form"
    Property KillingExplorer As String = "Killing Explorer (To be restarted)"
    Property ExplorerRestarted As String = "Explorer Restarted. It took about {0} seconds to kill explorer"
    Property ErrorExplorerRestart As String = "Error in restarting explorer. Relaunch it in Task Manager (Open Task Manager > Run new task > Type ""Explorer.exe"" and launch)"
    Property Scaling As String = "Scaling"
    Property LanguageRestart As String = "To apply this language, save settings and restart WinPaletter."
    Property WPTH_OldGen_LoadError As String = "Couldn't load preferences saved in the theme file made by old version of WinPaletter. Anyway, loading will continue without it."
    Property WPTH_OldGen_SaveError As String = "Couldn't save preferences to be suitable for old version of WinPaletter. Anyway, saving will continue without it."
    Property CP_11_StartMenu_Taskbar_AC As String = "Start Menu, Taskbar && Action Center"
    Property CP_11_ACHover_Links As String = "Action Center hover && links"
    Property CP_11_Taskbar_ACHover_Links As String = "Taskbar color, Action Center hover && links"
    Property CP_EP_ACButton_TaskbarAppLine As String = "Action Center buttons && taskbar app underline"
    Property CP_11_Lines_Toggles_Buttons As String = "Lines, toggles, buttons, taskbar app underline && volume slider"
    Property CP_11_Lines_Toggles_Buttons_Overflow As String = "Lines, toggles, buttons, volume slider && taskbar tray (overflow)"
    Property CP_11_OverflowTray As String = "Taskbar tray overflow (requires 22H2, accent on taskbar enabled)"
    Property CP_11_StartMenu_AC As String = "Start Menu && Action Center colors"
    Property CP_11_UnreadBadge As String = "Unread notifications count badge and other circles"
    Property CP_11_Settings As String = "Settings icons, text selection, focus dots && some pressed buttons"
    Property CP_11_SettingsAndTaskbarAppUnderline As String = "Settings icons, taskbar app underline, some pressed buttons && others"
    Property CP_11_SomePressedButtons As String = "Some pressed buttons"
    Property CP_UWPBackground As String = "UWP dialogs background (Windows 8.1 remnants in {0})"
    Property CP_10_ACLinks As String = "Action Center links"
    Property CP_10_TaskbarAppUnderline As String = "Taskbar app underline"
    Property CP_10_StartMenuIconHover As String = "Start Menu icon hover"
    Property CP_10_Settings_Links_SomeBtns As String = "Settings icons, links && some pressed buttons"
    Property CP_10_Settings_Links_Taskbar_SomeBtns As String = "Settings icons, links, taskbar focused app && some pressed buttons"
    Property CP_10_Settings_Links_TaskbarUndeline_SomeBtns As String = "Settings icons, links, taskbar app underline && some pressed buttons"
    Property CP_10_Hamburger As String = "Sliding hamburger menu"
    Property CP_10_StartMenu_AC As String = "Start Menu && Action Center"
    Property CP_EP_StartMenu_OverflowMenus As String = "Start Menu && overflow menus"
    Property CP_EP_StartMenu_ActionCenterButtons As String = "Start Menu && Action Center buttons"
    Property CP_EP_ActionCenterBackground As String = "Action Center background"
    Property CP_10_StartMenu_AC_TaskbarActiveApp As String = "Start Menu, Action Center && Taskbar active app"
    Property CP_10_Taskbar As String = "Taskbar"
    Property CP_EP_Taskbar_AppUnderline As String = "Taskbar && app underline"
    Property CP_10_Taskbar_ACLinks As String = "Taskbar background color && Action Center links"
    Property CP_10_TaskbarFocusedApp_StartButtonHover As String = "Taskbar focused app && Start Menu button hover"
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
    Property CP_Applying_Win11 As String = "Applying Windows 11 scheme"
    Property CP_Applying_Win10 As String = "Applying Windows 10 scheme"
    Property CP_Applying_Win8 As String = "Applying Windows 8.1 scheme"
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
    Property CP_Applying_TerminalPreview As String = "Applying Windows Terminal Preview"
    Property CP_Applying_ScreenSaver As String = "Applying Screen Saver"
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
    Property CP_Skip_Cursors As String = "Skipping Windows Cursors as it is disabled"
    Property CP_Skip_CMD As String = "Skipping Command Prompt as it is disabled"
    Property CP_Applying_PS64 As String = "Applying PowerShell x64"
    Property CP_Skip_PS64 As String = "Skipping PowerShell x64 as it is disabled"
    Property CP_Applying_PS32 As String = "Applying PowerShell x86"
    Property CP_Skip_PS32 As String = "Skipping PowerShell x86 as it is disabled"
    Property CP_Skip_Metrics As String = "Skipping Windows Metrics and Fonts as they are disabled"
    Property CP_Skip_AltTab As String = "Skipping Windows Switcher (Alt+Tab) Appearance as it is disabled"
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
    Property CP_W11_Error As String = "Error occurred during applying Windows 11 scheme"
    Property CP_W7_Error As String = "Error occurred during applying Windows 7 theme"
    Property CP_WVista_Error As String = "Error occurred during applying Windows Vista theme"
    Property CP_W8_Error As String = "Error occurred during applying Windows 8.1 scheme"
    Property CP_WXP_Error As String = "Error occurred during applying Windows XP theme"
    Property CP_Error_Cursors As String = "Error occurred during applying Windows Cursors"
    Property CP_Error_SetDesktop As String = "Error occurred during applying Desktop for all users"
    Property CP_Error_Metrics As String = "Error occurred during applying Windows Metrics and Fonts"
    Property CP_Error_AltTab As String = "Error occurred during applying Windows Switcher (Alt+Tab) appearance"
    Property CP_Error_TerminalPreview As String = "Error occurred during applying Windows Terminal Preview"
    Property CP_Error_TerminalStable As String = "Error occurred during applying Windows Terminal Stable"
    Property CP_Error_ScreenSaver As String = "Error occurred during applying Screen Saver"
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
    Property [Default] As String = "Default"
    Property Untitled As String = "Untitled"
    Property WhatsNewInVersion As String = "What's new in {0}!"
    Property ThisWillRestartExplorer As String = "This will restart the explorer, don't worry this won't close other applications"
    Property LogoffNotice As String = "This will logoff your Windows account. Please save your open files before logging-off"
    Property TitlebarColorNotice As String = "Windows volume slider, UAC and Windows 10 logonUI follow active titlebar color"
    Property NoDefResExplorer As String = "Restarting Explorer is disabled. If theme is not applied correctly, restart it"
    Property RemoveExtMsg As String = "Are you sure from removing files association (*.wpth, *.wpsf) from registry?"
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
    Property Terminal_SettingsNotExist As String = "Settings file doesn't exist"
    Property Terminal_External_Empty As String = "Terminal can't be empty. Enter a valid one."
    Property Terminal_External_NotExist As String = "Terminal doesn't exist. Enter a valid one."
    Property Terminal_External_Reversed As String = "This terminal is reserved for system. Try again with another one."
    Property Terminal_External_Exists As String = "This terminal already exists. Try again with another one."
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
    Property Store_Ping As String = "Testing access to ""{0}"""
    Property Store_PingFailed As String = "Couldn't get response from ""{0}"". Skipping this themes database"
    Property Store_Accessing As String = "Accessing themes database from ""{0}"""
    Property Store_UpdateTheme As String = "Updating theme ""{0}"" from ""{1}"""
    Property Store_DownloadTheme As String = "Downloading theme ""{0}"" from ""{1}"""
    Property Store_LoadingTheme As String = "Loading theme ""{0}"""
    Property Store_CleaningFromMemory As String = "Cleaning store items from memory"
    Property Store_NoIncludedData As String = "There is no included data"

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
        j_info.Add("AppVer".ToLower, My.Application.Info.Version.ToString)
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
                        j_child.Add(ctrl.Name & ".Text", ctrl.Text)
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

    Public Sub LoadLanguageFromJSON(File As String, Optional [_Form] As Form = Nothing)
        If IO.File.Exists(File) Then

            Dim St As New StreamReader(File)
            Dim JSON_String As String = St.ReadToEnd
            Dim JSonFile As JObject = JObject.Parse(JSON_String)
            St.Close()

            Dim J_Information As New JObject
            Dim J_GlobalStrings As New JObject
            Dim J_Forms As New JObject

            Dim Valid As Boolean = JSonFile.ContainsKey("Information") And JSonFile.ContainsKey("Global Strings") And JSonFile.ContainsKey("Forms Strings")

            If Not Valid Then
                '$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$

                Exit Sub
            End If

            J_Information = JSonFile("Information")
            J_GlobalStrings = JSonFile("Global Strings")
            J_Forms = JSonFile("Forms Strings")

            Dim type1 As Type = [GetType]() : Dim properties1 As PropertyInfo() = type1.GetProperties()

            For Each [property] As PropertyInfo In properties1
                If Not [property].Name.ToLower = "Name".ToLower _
                 And Not [property].Name.ToLower = "TranslationVersion".ToLower _
                 And Not [property].Name.ToLower = "Lang".ToLower _
                 And Not [property].Name.ToLower = "LangCode".ToLower _
                 And Not [property].Name.ToLower = "AppVer".ToLower _
                 And Not [property].Name.ToLower = "RightToLeft".ToLower Then

                    Dim FoundProp As Boolean = J_GlobalStrings.ContainsKey([property].Name.ToLower)
                    If FoundProp Then [property].SetValue(Me, Convert.ChangeType(J_GlobalStrings([property].Name.ToLower), [property].PropertyType))
                Else
                    Dim FoundProp As Boolean = J_Information.ContainsKey([property].Name.ToLower)
                    If FoundProp Then [property].SetValue(Me, Convert.ChangeType(J_Information([property].Name.ToLower), [property].PropertyType))
                End If
            Next

            Dim PopCtrlList As New List(Of Tuple(Of String, String, String, String))()
            PopCtrlList.Clear()

            Dim FormName, ControlName, Prop, Value As String

            FormName = String.Empty
            ControlName = String.Empty
            Prop = String.Empty
            Value = String.Empty

            For Each F In J_Forms
                Try
                    Dim J_Specific_Form As New JObject

                    J_Specific_Form = J_Forms(F.Key)
                    FormName = F.Key
                    ControlName = String.Empty

                    Prop = "Text"

                    If J_Specific_Form.ContainsKey("Text") Or J_Specific_Form.ContainsKey("text") Or J_Specific_Form.ContainsKey("TEXT") Then

                        If J_Specific_Form.ContainsKey("Text") Then Value = J_Specific_Form("Text")
                        If J_Specific_Form.ContainsKey("text") Then Value = J_Specific_Form("text")
                        If J_Specific_Form.ContainsKey("TEXT") Then Value = J_Specific_Form("TEXT")

                        PopCtrlList.Add(New Tuple(Of String, String, String, String)(FormName, ControlName, Prop, Value))

                    End If

                    If J_Specific_Form.ContainsKey("Controls") Or J_Specific_Form.ContainsKey("controls") Or J_Specific_Form.ContainsKey("CONTROLS") Then

                        Dim J_Controls As New JObject

                        If J_Specific_Form.ContainsKey("Controls") Then J_Controls = J_Specific_Form("Controls")
                        If J_Specific_Form.ContainsKey("controls") Then J_Controls = J_Specific_Form("controls")
                        If J_Specific_Form.ContainsKey("CONTROLS") Then J_Controls = J_Specific_Form("CONTROLS")

                        For Each ctrl In J_Controls
                            Try
                                If ctrl.Key.Contains(".") Then
                                    ControlName = ctrl.Key.Split(".")(0)
                                    Prop = ctrl.Key.Split(".")(1)
                                    Value = ctrl.Value
                                    PopCtrlList.Add(New Tuple(Of String, String, String, String)(FormName, ControlName, Prop, Value))
                                Else
                                    ControlName = ctrl.Key
                                    Prop = "Text"
                                    Value = ctrl.Value
                                    PopCtrlList.Add(New Tuple(Of String, String, String, String)(FormName, ControlName, Prop, Value))
                                End If
                            Catch
                            End Try
                        Next
                    End If

                Catch
                End Try
            Next

            PopCtrlList.Add(New Tuple(Of String, String, String, String)(FormName, ControlName, Prop, Value))

            Dim FList As New List(Of Form)
            FList.Clear()

            If [_Form] Is Nothing Then

                For x As Integer = 0 To allForms.Count - 1
                    With GetFormFromName(allForms(x))
                        If .Visible Then
                            FList.Add(GetFormFromName(allForms(x)))
                            .Visible = False
                        End If

                        '.SuspendLayout()
                        Populate(PopCtrlList, GetFormFromName(allForms(x)))
                        .RightToLeftLayout = RightToLeft
                        '.RightToLeft = If(RightToLeft, 1, 0)
                        'RTL(My.Application.GetFormFromName(My.Application.allForms(x)))
                        '.ResumeLayout()
                        '.Refresh()
                    End With

                Next

            Else
                If [_Form].Visible Then
                    [_Form].Visible = False
                    FList.Add([_Form])
                End If
                Populate(PopCtrlList, [_Form])
            End If

            PopCtrlList.Clear()

            For Each F In FList
                F.Visible = True
            Next

            FList.Clear()
        End If
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

                                Try : If member.Item3.ToLower = "text" Then ctrl.SetText(member.Item4.ToString)
                                Catch : End Try

                                Try : If member.Item3.ToLower = "tag" Then SetCtrlTag(member.Item4.ToString, ctrl)
                                Catch : End Try

                                'ctrl.RightToLeft = If(RightToLeft, 1, 0)
                                'ctrl.Refresh()
                            Next

                        End If

                    End If
                End If

            Catch ex As Exception
                Throw ex
            End Try
        Next

    End Sub

    Sub RTL(Parent As Control)

        If RightToLeft Then

            For Each XeTP As XenonTabControl In Parent.Controls.OfType(Of XenonTabControl)
                XeTP.RightToLeft = If(RightToLeft, 1, 0)
                XeTP.RightToLeftLayout = RightToLeft

                For i = 0 To XeTP.TabPages.Count - 1
                    XeTP.TabPages.Item(i).RightToLeft = If(RightToLeft, 1, 0)
                    If XeTP.TabPages.Item(i).HasChildren Then RTL(XeTP.TabPages.Item(i))

                    For Each Cx As Control In XeTP.TabPages.Item(i).Controls
                        Cx.Left = XeTP.TabPages.Item(i).Width - Cx.Left - Cx.Width
                        If Cx.HasChildren Then RTL(Cx)
                    Next
                Next
            Next

            For Each XeTP As Control In Parent.Controls
                If TypeOf XeTP Is XenonGroupBox Or TypeOf XeTP Is Panel Or TypeOf XeTP Is ContainerControl Then
                    XeTP.RightToLeft = If(RightToLeft, 1, 0)
                    For Each Cx As Control In XeTP.Controls
                        Cx.Left = XeTP.Width - Cx.Left - Cx.Width
                        If Cx.HasChildren Then RTL(Cx)
                    Next
                End If
            Next

        End If


    End Sub

    Private Function GetAllControls(parent As Control) As IEnumerable(Of Control)
        Dim cs = parent.Controls.OfType(Of Control)
        Return cs.SelectMany(Function(c) GetAllControls(c)).Concat(cs)
    End Function

End Class
