Imports WinPaletter.PreviewHelpers

Public Class CP_Defaults : Implements IDisposable

    Public Default_Windows11Accents_Bytes As Byte() = {Default_Windows11.Windows11.Color_Index0.R, Default_Windows11.Windows11.Color_Index0.G, Default_Windows11.Windows11.Color_Index0.B, 255,
                                                    Default_Windows11.Windows11.Color_Index1.R, Default_Windows11.Windows11.Color_Index1.G, Default_Windows11.Windows11.Color_Index1.B, 255,
                                                    Default_Windows11.Windows11.Color_Index2.R, Default_Windows11.Windows11.Color_Index2.G, Default_Windows11.Windows11.Color_Index2.B, 255,
                                                    Default_Windows11.Windows11.Color_Index3.R, Default_Windows11.Windows11.Color_Index3.G, Default_Windows11.Windows11.Color_Index3.B, 255,
                                                    Default_Windows11.Windows11.Color_Index4.R, Default_Windows11.Windows11.Color_Index4.G, Default_Windows11.Windows11.Color_Index4.B, 255,
                                                    Default_Windows11.Windows11.Color_Index5.R, Default_Windows11.Windows11.Color_Index5.G, Default_Windows11.Windows11.Color_Index5.B, 255,
                                                    Default_Windows11.Windows11.Color_Index6.R, Default_Windows11.Windows11.Color_Index6.G, Default_Windows11.Windows11.Color_Index6.B, 255,
                                                    Default_Windows11.Windows11.Color_Index7.R, Default_Windows11.Windows11.Color_Index7.G, Default_Windows11.Windows11.Color_Index7.B, 255}

    Public Default_Windows10Accents_Bytes As Byte() = {Default_Windows10.Windows11.Color_Index0.R, Default_Windows10.Windows11.Color_Index0.G, Default_Windows10.Windows11.Color_Index0.B, 255,
                                                    Default_Windows10.Windows11.Color_Index1.R, Default_Windows10.Windows11.Color_Index1.G, Default_Windows10.Windows11.Color_Index1.B, 255,
                                                    Default_Windows10.Windows11.Color_Index2.R, Default_Windows10.Windows11.Color_Index2.G, Default_Windows10.Windows11.Color_Index2.B, 255,
                                                    Default_Windows10.Windows11.Color_Index3.R, Default_Windows10.Windows11.Color_Index3.G, Default_Windows10.Windows11.Color_Index3.B, 255,
                                                    Default_Windows10.Windows11.Color_Index4.R, Default_Windows10.Windows11.Color_Index4.G, Default_Windows10.Windows11.Color_Index4.B, 255,
                                                    Default_Windows10.Windows11.Color_Index5.R, Default_Windows10.Windows11.Color_Index5.G, Default_Windows10.Windows11.Color_Index5.B, 255,
                                                    Default_Windows10.Windows11.Color_Index6.R, Default_Windows10.Windows11.Color_Index6.G, Default_Windows10.Windows11.Color_Index6.B, 255,
                                                    Default_Windows10.Windows11.Color_Index7.R, Default_Windows10.Windows11.Color_Index7.G, Default_Windows10.Windows11.Color_Index7.B, 255}

    Public Shared Function From([PreviewStyle] As WindowStyle) As CP
        Dim _Def As CP

        If [PreviewStyle] = WindowStyle.W11 Then
            Using X As New CP_Defaults() : _Def = X.Default_Windows11 : End Using

        ElseIf [PreviewStyle] = WindowStyle.W10 Then
            Using X As New CP_Defaults() : _Def = X.Default_Windows10 : End Using

        ElseIf [PreviewStyle] = WindowStyle.W81 Then
            Using X As New CP_Defaults() : _Def = X.Default_Windows81 : End Using

        ElseIf [PreviewStyle] = WindowStyle.W7 Then
            Using X As New CP_Defaults() : _Def = X.Default_Windows7 : End Using

        ElseIf [PreviewStyle] = WindowStyle.WVista Then
            Using X As New CP_Defaults() : _Def = X.Default_WindowsVista : End Using

        ElseIf [PreviewStyle] = WindowStyle.WXP Then
            Using X As New CP_Defaults() : _Def = X.Default_WindowsXP : End Using

        Else
            Using X As New CP_Defaults() : _Def = X.Default_Windows11 : End Using

        End If

        Return _Def
    End Function

    Public Shared Function GetDefault() As CP
        Dim _Def As CP

        If My.W11 Then
            Using X As New CP_Defaults() : _Def = X.Default_Windows11 : End Using

        ElseIf My.W10 Then
            Using X As New CP_Defaults() : _Def = X.Default_Windows10 : End Using

        ElseIf My.W81 Then
            Using X As New CP_Defaults() : _Def = X.Default_Windows81 : End Using

        ElseIf My.W7 Then
            Using X As New CP_Defaults() : _Def = X.Default_Windows7 : End Using

        ElseIf My.WVista Then
            Using X As New CP_Defaults() : _Def = X.Default_WindowsVista : End Using

        ElseIf My.WXP Then
            Using X As New CP_Defaults() : _Def = X.Default_WindowsXP : End Using

        Else
            Using X As New CP_Defaults() : _Def = X.Default_Windows11 : End Using

        End If

        Return _Def
    End Function

    Public Function Default_Windows11() As CP
        Dim [CP] As New CP(CP.CP_Type.Empty)

        [CP].Windows11.Titlebar_Inactive = Color.FromArgb(32, 32, 32)

        With [CP].Info
            .ThemeName = "Windows 11 (Initial)"
            .Description = "Initial; Like first time after Windows Setup"
            .ThemeVersion = "1.0.0.0"
            .Author = "Microsoft"
            .AuthorSocialMediaLink = "www.microsoft.com"
            .AppVersion = My.AppVersion
        End With

        With [CP].CommandPrompt
            .ColorTable05 = Color.FromArgb(136, 23, 152)
            .ColorTable06 = Color.FromArgb(193, 156, 0)
            .PopupBackground = 15
            .PopupForeground = 5
            .ScreenColorsForeground = 7
            .ScreenColorsBackground = 0
            .FaceName = "Consolas"
            .FontSize = 18 * 65536
            .FontRaster = False
            .W10_1909_ForceV2 = True
        End With

        With [CP].PowerShellx86
            .ColorTable05 = Color.FromArgb(1, 36, 86)
            .ColorTable06 = Color.FromArgb(238, 237, 240)
            .PopupBackground = 15
            .PopupForeground = 3
            .ScreenColorsForeground = 6
            .ScreenColorsBackground = 5
            .FaceName = "Consolas"
            .FontSize = 17 * 65536
            .FontRaster = False
            .W10_1909_ForceV2 = True
        End With

        With [CP].PowerShellx64
            .ColorTable05 = Color.FromArgb(1, 36, 86)
            .ColorTable06 = Color.FromArgb(238, 237, 240)
            .PopupBackground = 15
            .PopupForeground = 3
            .ScreenColorsForeground = 6
            .ScreenColorsBackground = 5
            .FaceName = "Consolas"
            .FontSize = 17 * 65536
            .FontRaster = False
            .W10_1909_ForceV2 = True
        End With

        With [CP].MetricsFonts
            .BorderWidth = 1
            .CaptionHeight = 22
            .CaptionWidth = 22
            .IconSpacing = 75
            .IconVerticalSpacing = 75
            .MenuHeight = 19
            .MenuWidth = 19
            .PaddedBorderWidth = 4
            .ScrollHeight = 17
            .ScrollWidth = 17
            .SmCaptionHeight = 22
            .SmCaptionWidth = 22
            .DesktopIconSize = 48
            .ShellIconSize = 32
        End With

        With [CP].WindowsEffects
            .ShakeToMinimize = False
            .BalloonNotifications = False
            .PaintDesktopVersion = False
            .ShowSecondsInSystemClock = False
            .Win11ClassicContextMenu = False
            .SysListView32 = False
        End With

        [CP].Terminal = New WinTerminal("", WinTerminal.Mode.Empty)
        [CP].TerminalPreview = New WinTerminal("", WinTerminal.Mode.Empty)
        [CP].Cursor_Shadow = False

        With [CP].Sounds
            .Snd_Imageres_SystemStart = "Default"
            .Snd_Win_Default = My.PATH_Windows & "\media\Windows Background.wav"
            .Snd_Win_AppGPFault = ""
            .Snd_Win_CCSelect = ""
            .Snd_Win_ChangeTheme = ""
            .Snd_Win_Close = ""
            .Snd_Win_CriticalBatteryAlarm = My.PATH_Windows & "\media\Windows Foreground.wav"
            .Snd_Win_DeviceConnect = My.PATH_Windows & "\media\Windows Hardware Insert.wav"
            .Snd_Win_DeviceDisconnect = My.PATH_Windows & "\media\Windows Hardware Remove.wav"
            .Snd_Win_DeviceFail = My.PATH_Windows & "\media\Windows Hardware Fail.wav"
            .Snd_Win_FaxBeep = My.PATH_Windows & "\media\Windows Notify Email.wav"
            .Snd_Win_LowBatteryAlarm = My.PATH_Windows & "\media\Windows Background.wav"
            .Snd_Win_MailBeep = My.PATH_Windows & "\media\Windows Notify Email.wav"
            .Snd_Win_Maximize = ""
            .Snd_Win_MenuCommand = ""
            .Snd_Win_MenuPopup = ""
            .Snd_Win_MessageNudge = My.PATH_Windows & "\media\Windows Message Nudge.wav"
            .Snd_Win_Minimize = ""
            .Snd_Win_Notification_Default = My.PATH_Windows & "\media\Windows Notify System Generic.wav"
            .Snd_Win_Notification_IM = My.PATH_Windows & "\media\Windows Notify Messaging.wav"
            .Snd_Win_Notification_Looping_Alarm = My.PATH_Windows & "\media\Alarm01.wav"
            .Snd_Win_Notification_Looping_Alarm2 = My.PATH_Windows & "\media\Alarm02.wav"
            .Snd_Win_Notification_Looping_Alarm3 = My.PATH_Windows & "\media\Alarm03.wav"
            .Snd_Win_Notification_Looping_Alarm4 = My.PATH_Windows & "\media\Alarm04.wav"
            .Snd_Win_Notification_Looping_Alarm5 = My.PATH_Windows & "\media\Alarm05.wav"
            .Snd_Win_Notification_Looping_Alarm6 = My.PATH_Windows & "\media\Alarm06.wav"
            .Snd_Win_Notification_Looping_Alarm7 = My.PATH_Windows & "\media\Alarm07.wav"
            .Snd_Win_Notification_Looping_Alarm8 = My.PATH_Windows & "\media\Alarm08.wav"
            .Snd_Win_Notification_Looping_Alarm9 = My.PATH_Windows & "\media\Alarm09.wav"
            .Snd_Win_Notification_Looping_Alarm10 = My.PATH_Windows & "\media\Alarm10.wav"
            .Snd_Win_Notification_Looping_Call = My.PATH_Windows & "\media\Ring01.wav"
            .Snd_Win_Notification_Looping_Call2 = My.PATH_Windows & "\media\Ring02.wav"
            .Snd_Win_Notification_Looping_Call3 = My.PATH_Windows & "\media\Ring03.wav"
            .Snd_Win_Notification_Looping_Call4 = My.PATH_Windows & "\media\Ring04.wav"
            .Snd_Win_Notification_Looping_Call5 = My.PATH_Windows & "\media\Ring05.wav"
            .Snd_Win_Notification_Looping_Call6 = My.PATH_Windows & "\media\Ring06.wav"
            .Snd_Win_Notification_Looping_Call7 = My.PATH_Windows & "\media\Ring07.wav"
            .Snd_Win_Notification_Looping_Call8 = My.PATH_Windows & "\media\Ring08.wav"
            .Snd_Win_Notification_Looping_Call9 = My.PATH_Windows & "\media\Ring09.wav"
            .Snd_Win_Notification_Looping_Call10 = My.PATH_Windows & "\media\Ring10.wav"
            .Snd_Win_Notification_Mail = My.PATH_Windows & "\media\Windows Notify Email.wav"
            .Snd_Win_Notification_Proximity = My.PATH_Windows & "\media\Windows Proximity Notification.wav"
            .Snd_Win_Notification_Reminder = My.PATH_Windows & "\media\Windows Notify Calendar.wav"
            .Snd_Win_Notification_SMS = My.PATH_Windows & "\media\Windows Notify Messaging.wav"
            .Snd_Win_Open = ""
            .Snd_Win_PrintComplete = ""
            .Snd_Win_ProximityConnection = My.PATH_Windows & "\media\Windows Proximity Connection.wav"
            .Snd_Win_RestoreDown = ""
            .Snd_Win_RestoreUp = ""
            .Snd_Win_ShowBand = ""
            .Snd_Win_SystemAsterisk = My.PATH_Windows & "\media\Windows Background.wav"
            .Snd_Win_SystemExclamation = My.PATH_Windows & "\media\Windows Background.wav"
            .Snd_Win_SystemExit = ""
            .Snd_Win_SystemStart = ""
            .Snd_Win_SystemHand = My.PATH_Windows & "\media\Windows Foreground.wav"
            .Snd_Win_SystemNotification = My.PATH_Windows & "\media\Windows Background.wav"
            .Snd_Win_SystemQuestion = ""
            .Snd_Win_WindowsLogoff = ""
            .Snd_Win_WindowsLogon = ""
            .Snd_Win_WindowsUAC = My.PATH_Windows & "\media\Windows User Account Control.wav"
            .Snd_Win_WindowsUnlock = My.PATH_Windows & "\media\Windows Unlock.wav"
            .Snd_Explorer_ActivatingDocument = ""
            .Snd_Explorer_BlockedPopup = ""
            .Snd_Explorer_EmptyRecycleBin = ""
            .Snd_Explorer_FeedDiscovered = ""
            .Snd_Explorer_MoveMenuItem = ""
            .Snd_Explorer_Navigating = ""
            .Snd_Explorer_SecurityBand = ""
            .Snd_Explorer_SearchProviderDiscovered = ""
            .Snd_Explorer_FaxError = ""
            .Snd_Explorer_FaxLineRings = ""
            .Snd_Explorer_FaxNew = ""
            .Snd_Explorer_FaxSent = ""
            .Snd_NetMeeting_PersonJoins = ""
            .Snd_NetMeeting_PersonLeaves = ""
            .Snd_NetMeeting_ReceiveCall = ""
            .Snd_NetMeeting_ReceiveRequestToJoin = ""
            .Snd_SpeechRec_DisNumbersSound = My.PATH_Windows & "\media\Speech Disambiguation.wav"
            .Snd_SpeechRec_HubOffSound = My.PATH_Windows & "\media\Speech Off.wav"
            .Snd_SpeechRec_HubOnSound = My.PATH_Windows & "\media\Speech On.wav"
            .Snd_SpeechRec_HubSleepSound = My.PATH_Windows & "\media\Speech Sleep.wav"
            .Snd_SpeechRec_MisrecoSound = My.PATH_Windows & "\media\Speech Misrecognition.wav"
            .Snd_SpeechRec_PanelSound = My.PATH_Windows & "\media\Speech Disambiguation.wav"

            .Snd_Win_SystemExit_TaskMgmt = True
            .Snd_Win_WindowsLogoff_TaskMgmt = True
            .Snd_Win_WindowsLogon_TaskMgmt = True
            .Snd_Win_WindowsUnlock_TaskMgmt = True
        End With

        Return [CP]
    End Function

    Public Function Default_Windows10() As CP
        Dim [CP] As New CP(CP.CP_Type.Empty)

        With [CP].Info
            .ThemeName = "Windows 10 (Initial)"
            .Description = "Initial; Like first time after Windows Setup"
            .ThemeVersion = "1.0.0.0"
            .Author = "Microsoft"
            .AuthorSocialMediaLink = "www.microsoft.com"
            .AppVersion = My.AppVersion
        End With

        With [CP].CommandPrompt
            .ColorTable05 = Color.FromArgb(136, 23, 152)
            .ColorTable06 = Color.FromArgb(193, 156, 0)
            .PopupBackground = 15
            .PopupForeground = 5
            .ScreenColorsForeground = 7
            .ScreenColorsBackground = 0
            .FaceName = "Consolas"
            .FontSize = 18 * 65536
            .FontRaster = False
            .W10_1909_ForceV2 = My.W10_1909
        End With

        With [CP].PowerShellx86
            .ColorTable05 = Color.FromArgb(1, 36, 86)
            .ColorTable06 = Color.FromArgb(238, 237, 240)
            .PopupBackground = 15
            .PopupForeground = 3
            .ScreenColorsForeground = 6
            .ScreenColorsBackground = 5
            .FaceName = "Consolas"
            .FontSize = 17 * 65536
            .FontRaster = False
            .W10_1909_ForceV2 = My.W10_1909
        End With

        With [CP].PowerShellx64
            .ColorTable05 = Color.FromArgb(1, 36, 86)
            .ColorTable06 = Color.FromArgb(238, 237, 240)
            .PopupBackground = 15
            .PopupForeground = 3
            .ScreenColorsForeground = 6
            .ScreenColorsBackground = 5
            .FaceName = "Consolas"
            .FontSize = 17 * 65536
            .FontRaster = False
            .W10_1909_ForceV2 = My.W10_1909
        End With

        With [CP].MetricsFonts
            .BorderWidth = 1
            .CaptionHeight = 22
            .CaptionWidth = 22
            .IconSpacing = 75
            .IconVerticalSpacing = 75
            .MenuHeight = 19
            .MenuWidth = 19
            .PaddedBorderWidth = 4
            .ScrollHeight = 17
            .ScrollWidth = 17
            .SmCaptionHeight = 22
            .SmCaptionWidth = 22
            .DesktopIconSize = 48
            .ShellIconSize = 32
        End With

        With [CP].WindowsEffects
            .ShakeToMinimize = True
            .BalloonNotifications = False
            .PaintDesktopVersion = False
            .ShowSecondsInSystemClock = False
            .Win11ClassicContextMenu = False
            .SysListView32 = False
        End With

        [CP].Terminal = New WinTerminal("", WinTerminal.Mode.Empty)
        [CP].TerminalPreview = New WinTerminal("", WinTerminal.Mode.Empty)

        [CP].Cursor_Shadow = False

        With [CP].Sounds
            .Snd_Imageres_SystemStart = ""
            .Snd_Win_Default = My.PATH_Windows & "\media\Windows Background.wav"
            .Snd_Win_AppGPFault = ""
            .Snd_Win_CCSelect = ""
            .Snd_Win_ChangeTheme = ""
            .Snd_Win_Close = ""
            .Snd_Win_CriticalBatteryAlarm = My.PATH_Windows & "\media\Windows Foreground.wav"
            .Snd_Win_DeviceConnect = My.PATH_Windows & "\media\Windows Hardware Insert.wav"
            .Snd_Win_DeviceDisconnect = My.PATH_Windows & "\media\Windows Hardware Remove.wav"
            .Snd_Win_DeviceFail = My.PATH_Windows & "\media\Windows Hardware Fail.wav"
            .Snd_Win_FaxBeep = My.PATH_Windows & "\media\Windows Notify Email.wav"
            .Snd_Win_LowBatteryAlarm = My.PATH_Windows & "\media\Windows Background.wav"
            .Snd_Win_MailBeep = My.PATH_Windows & "\media\Windows Notify Email.wav"
            .Snd_Win_Maximize = ""
            .Snd_Win_MenuCommand = ""
            .Snd_Win_MenuPopup = ""
            .Snd_Win_MessageNudge = My.PATH_Windows & "\media\Windows Message Nudge.wav"
            .Snd_Win_Minimize = ""
            .Snd_Win_Notification_Default = My.PATH_Windows & "\media\Windows Notify System Generic.wav"
            .Snd_Win_Notification_IM = My.PATH_Windows & "\media\Windows Notify Messaging.wav"
            .Snd_Win_Notification_Looping_Alarm = My.PATH_Windows & "\media\Alarm01.wav"
            .Snd_Win_Notification_Looping_Alarm2 = My.PATH_Windows & "\media\Alarm02.wav"
            .Snd_Win_Notification_Looping_Alarm3 = My.PATH_Windows & "\media\Alarm03.wav"
            .Snd_Win_Notification_Looping_Alarm4 = My.PATH_Windows & "\media\Alarm04.wav"
            .Snd_Win_Notification_Looping_Alarm5 = My.PATH_Windows & "\media\Alarm05.wav"
            .Snd_Win_Notification_Looping_Alarm6 = My.PATH_Windows & "\media\Alarm06.wav"
            .Snd_Win_Notification_Looping_Alarm7 = My.PATH_Windows & "\media\Alarm07.wav"
            .Snd_Win_Notification_Looping_Alarm8 = My.PATH_Windows & "\media\Alarm08.wav"
            .Snd_Win_Notification_Looping_Alarm9 = My.PATH_Windows & "\media\Alarm09.wav"
            .Snd_Win_Notification_Looping_Alarm10 = My.PATH_Windows & "\media\Alarm10.wav"
            .Snd_Win_Notification_Looping_Call = My.PATH_Windows & "\media\Ring01.wav"
            .Snd_Win_Notification_Looping_Call2 = My.PATH_Windows & "\media\Ring02.wav"
            .Snd_Win_Notification_Looping_Call3 = My.PATH_Windows & "\media\Ring03.wav"
            .Snd_Win_Notification_Looping_Call4 = My.PATH_Windows & "\media\Ring04.wav"
            .Snd_Win_Notification_Looping_Call5 = My.PATH_Windows & "\media\Ring05.wav"
            .Snd_Win_Notification_Looping_Call6 = My.PATH_Windows & "\media\Ring06.wav"
            .Snd_Win_Notification_Looping_Call7 = My.PATH_Windows & "\media\Ring07.wav"
            .Snd_Win_Notification_Looping_Call8 = My.PATH_Windows & "\media\Ring08.wav"
            .Snd_Win_Notification_Looping_Call9 = My.PATH_Windows & "\media\Ring09.wav"
            .Snd_Win_Notification_Looping_Call10 = My.PATH_Windows & "\media\Ring10.wav"
            .Snd_Win_Notification_Mail = My.PATH_Windows & "\media\Windows Notify Email.wav"
            .Snd_Win_Notification_Proximity = My.PATH_Windows & "\media\Windows Proximity Notification.wav"
            .Snd_Win_Notification_Reminder = My.PATH_Windows & "\media\Windows Notify Calendar.wav"
            .Snd_Win_Notification_SMS = My.PATH_Windows & "\media\Windows Notify Messaging.wav"
            .Snd_Win_Open = ""
            .Snd_Win_PrintComplete = ""
            .Snd_Win_ProximityConnection = My.PATH_Windows & "\media\Windows Proximity Connection.wav"
            .Snd_Win_RestoreDown = ""
            .Snd_Win_RestoreUp = ""
            .Snd_Win_ShowBand = ""
            .Snd_Win_SystemAsterisk = My.PATH_Windows & "\media\Windows Background.wav"
            .Snd_Win_SystemExclamation = My.PATH_Windows & "\media\Windows Background.wav"
            .Snd_Win_SystemExit = ""
            .Snd_Win_SystemStart = ""
            .Snd_Win_SystemHand = My.PATH_Windows & "\media\Windows Foreground.wav"
            .Snd_Win_SystemNotification = My.PATH_Windows & "\media\Windows Background.wav"
            .Snd_Win_SystemQuestion = ""
            .Snd_Win_WindowsLogoff = ""
            .Snd_Win_WindowsLogon = ""
            .Snd_Win_WindowsUAC = My.PATH_Windows & "\media\Windows User Account Control.wav"
            .Snd_Win_WindowsUnlock = My.PATH_Windows & "\media\Windows Unlock.wav"
            .Snd_Explorer_ActivatingDocument = ""
            .Snd_Explorer_BlockedPopup = ""
            .Snd_Explorer_EmptyRecycleBin = ""
            .Snd_Explorer_FeedDiscovered = ""
            .Snd_Explorer_MoveMenuItem = ""
            .Snd_Explorer_Navigating = ""
            .Snd_Explorer_SecurityBand = ""
            .Snd_Explorer_SearchProviderDiscovered = ""
            .Snd_Explorer_FaxError = ""
            .Snd_Explorer_FaxLineRings = ""
            .Snd_Explorer_FaxNew = ""
            .Snd_Explorer_FaxSent = ""
            .Snd_NetMeeting_PersonJoins = ""
            .Snd_NetMeeting_PersonLeaves = ""
            .Snd_NetMeeting_ReceiveCall = ""
            .Snd_NetMeeting_ReceiveRequestToJoin = ""
            .Snd_SpeechRec_DisNumbersSound = My.PATH_Windows & "\media\Speech Disambiguation.wav"
            .Snd_SpeechRec_HubOffSound = My.PATH_Windows & "\media\Speech Off.wav"
            .Snd_SpeechRec_HubOnSound = My.PATH_Windows & "\media\Speech On.wav"
            .Snd_SpeechRec_HubSleepSound = My.PATH_Windows & "\media\Speech Sleep.wav"
            .Snd_SpeechRec_MisrecoSound = My.PATH_Windows & "\media\Speech Misrecognition.wav"
            .Snd_SpeechRec_PanelSound = My.PATH_Windows & "\media\Speech Disambiguation.wav"

            .Snd_Win_SystemExit_TaskMgmt = True
            .Snd_Win_WindowsLogoff_TaskMgmt = True
            .Snd_Win_WindowsLogon_TaskMgmt = True
            .Snd_Win_WindowsUnlock_TaskMgmt = True
        End With

        Return [CP]
    End Function

    Public Function Default_Windows81() As CP
        Dim [CP] As New CP(CP.CP_Type.Empty)

        With [CP].Info
            .ThemeName = "Windows 8.1 (Initial)"
            .Description = "Initial; Like first time after Windows Setup"
            .ThemeVersion = "1.0.0.0"
            .Author = "Microsoft"
            .AuthorSocialMediaLink = "www.microsoft.com"
            .AppVersion = My.AppVersion
        End With

        With [CP].Windows81
            .ColorizationColor = Color.FromArgb(246, 195, 74)
            .ColorizationColorBalance = 78
            .PersonalColors_Background = Color.FromArgb(30, 0, 84)
            .PersonalColors_Accent = Color.FromArgb(72, 29, 178)
            .StartColor = Color.FromArgb(30, 0, 84)
            .AccentColor = Color.FromArgb(72, 29, 178)
            .Start = 0
            .Theme = CP.Structures.Windows7.Themes.Aero
            .LogonUI = 0
            .NoLockScreen = False
            .LockScreenType = CP.Structures.LogonUI7.Modes.Default_
            .LockScreenSystemID = 0
        End With

        With [CP].Windows7
            .ColorizationColor = Color.FromArgb(246, 195, 74)
            .ColorizationAfterglow = Color.FromArgb(0, 0, 0)
            .ColorizationColorBalance = 78
            .ColorizationAfterglowBalance = 31
            .ColorizationBlurBalance = 31
            .ColorizationGlassReflectionIntensity = 0
            .EnableAeroPeek = True
            .AlwaysHibernateThumbnails = False
        End With

        With [CP].WindowsVista
            .ColorizationColor = Color.FromArgb(64, 158, 254)
        End With

        With [CP].CommandPrompt
            .ColorTable05 = Color.FromArgb(136, 23, 152)
            .ColorTable06 = Color.FromArgb(193, 156, 0)
            .PopupBackground = 15
            .PopupForeground = 5
            .ScreenColorsForeground = 7
            .ScreenColorsBackground = 0
            .FaceName = "Consolas"
            .FontSize = 18 * 65536
            .FontRaster = True
            .W10_1909_ForceV2 = False
        End With

        With [CP].PowerShellx86
            .ColorTable05 = Color.FromArgb(1, 36, 86)
            .ColorTable06 = Color.FromArgb(238, 237, 240)
            .PopupBackground = 15
            .PopupForeground = 3
            .ScreenColorsForeground = 6
            .ScreenColorsBackground = 5
            .FaceName = "Consolas"
            .FontSize = 14 * 65536
            .FontRaster = True
            .W10_1909_ForceV2 = False
        End With

        With [CP].PowerShellx64
            .ColorTable05 = Color.FromArgb(1, 36, 86)
            .ColorTable06 = Color.FromArgb(238, 237, 240)
            .PopupBackground = 15
            .PopupForeground = 3
            .ScreenColorsForeground = 6
            .ScreenColorsBackground = 5
            .FaceName = "Consolas"
            .FontSize = 14 * 65536
            .FontRaster = True
            .W10_1909_ForceV2 = False
        End With

        With [CP].MetricsFonts
            .BorderWidth = 1
            .CaptionHeight = 22
            .CaptionWidth = 22
            .IconSpacing = 75
            .IconVerticalSpacing = 75
            .MenuHeight = 19
            .MenuWidth = 19
            .PaddedBorderWidth = 4
            .ScrollHeight = 17
            .ScrollWidth = 17
            .SmCaptionHeight = 22
            .SmCaptionWidth = 22
            .DesktopIconSize = 48
            .ShellIconSize = 32
            .CaptionFont = New Font("Segoe UI", 11.25, FontStyle.Regular)
            .SmCaptionFont = New Font("Segoe UI", 11.25, FontStyle.Regular)
        End With

        With [CP].WindowsEffects
            .ShakeToMinimize = True
            .BalloonNotifications = True
            .PaintDesktopVersion = False
            .ShowSecondsInSystemClock = False
            .Win11ClassicContextMenu = False
            .SysListView32 = False
        End With

        [CP].Terminal = New WinTerminal("", WinTerminal.Mode.Empty)
        [CP].TerminalPreview = New WinTerminal("", WinTerminal.Mode.Empty)
        [CP].Cursor_Shadow = False

        With [CP].Sounds
            .Snd_Imageres_SystemStart = ""
            .Snd_Win_Default = My.PATH_Windows & "\media\Windows Background.wav"
            .Snd_Win_AppGPFault = ""
            .Snd_Win_CCSelect = ""
            .Snd_Win_ChangeTheme = ""
            .Snd_Win_Close = ""
            .Snd_Win_CriticalBatteryAlarm = My.PATH_Windows & "\media\Windows Foreground.wav"
            .Snd_Win_DeviceConnect = My.PATH_Windows & "\media\Windows Hardware Insert.wav"
            .Snd_Win_DeviceDisconnect = My.PATH_Windows & "\media\Windows Hardware Remove.wav"
            .Snd_Win_DeviceFail = My.PATH_Windows & "\media\Windows Hardware Fail.wav"
            .Snd_Win_FaxBeep = My.PATH_Windows & "\media\Windows Notify Email.wav"
            .Snd_Win_LowBatteryAlarm = My.PATH_Windows & "\media\Windows Background.wav"
            .Snd_Win_MailBeep = My.PATH_Windows & "\media\Windows Notify Email.wav"
            .Snd_Win_Maximize = ""
            .Snd_Win_MenuCommand = ""
            .Snd_Win_MenuPopup = ""
            .Snd_Win_MessageNudge = My.PATH_Windows & "\media\Windows Message Nudge.wav"
            .Snd_Win_Minimize = ""
            .Snd_Win_Notification_Default = My.PATH_Windows & "\media\Windows Notify System Generic.wav"
            .Snd_Win_Notification_IM = My.PATH_Windows & "\media\Windows Notify Messaging.wav"
            .Snd_Win_Notification_Looping_Alarm = My.PATH_Windows & "\media\Alarm01.wav"
            .Snd_Win_Notification_Looping_Alarm2 = My.PATH_Windows & "\media\Alarm02.wav"
            .Snd_Win_Notification_Looping_Alarm3 = My.PATH_Windows & "\media\Alarm03.wav"
            .Snd_Win_Notification_Looping_Alarm4 = My.PATH_Windows & "\media\Alarm04.wav"
            .Snd_Win_Notification_Looping_Alarm5 = My.PATH_Windows & "\media\Alarm05.wav"
            .Snd_Win_Notification_Looping_Alarm6 = My.PATH_Windows & "\media\Alarm06.wav"
            .Snd_Win_Notification_Looping_Alarm7 = My.PATH_Windows & "\media\Alarm07.wav"
            .Snd_Win_Notification_Looping_Alarm8 = My.PATH_Windows & "\media\Alarm08.wav"
            .Snd_Win_Notification_Looping_Alarm9 = My.PATH_Windows & "\media\Alarm09.wav"
            .Snd_Win_Notification_Looping_Alarm10 = My.PATH_Windows & "\media\Alarm10.wav"
            .Snd_Win_Notification_Looping_Call = My.PATH_Windows & "\media\Ring01.wav"
            .Snd_Win_Notification_Looping_Call2 = My.PATH_Windows & "\media\Ring02.wav"
            .Snd_Win_Notification_Looping_Call3 = My.PATH_Windows & "\media\Ring03.wav"
            .Snd_Win_Notification_Looping_Call4 = My.PATH_Windows & "\media\Ring04.wav"
            .Snd_Win_Notification_Looping_Call5 = My.PATH_Windows & "\media\Ring05.wav"
            .Snd_Win_Notification_Looping_Call6 = My.PATH_Windows & "\media\Ring06.wav"
            .Snd_Win_Notification_Looping_Call7 = My.PATH_Windows & "\media\Ring07.wav"
            .Snd_Win_Notification_Looping_Call8 = My.PATH_Windows & "\media\Ring08.wav"
            .Snd_Win_Notification_Looping_Call9 = My.PATH_Windows & "\media\Ring09.wav"
            .Snd_Win_Notification_Looping_Call10 = My.PATH_Windows & "\media\Ring10.wav"
            .Snd_Win_Notification_Mail = My.PATH_Windows & "\media\Windows Notify Email.wav"
            .Snd_Win_Notification_Proximity = My.PATH_Windows & "\media\Windows Proximity Notification.wav"
            .Snd_Win_Notification_Reminder = My.PATH_Windows & "\media\Windows Notify Calendar.wav"
            .Snd_Win_Notification_SMS = My.PATH_Windows & "\media\Windows Notify Messaging.wav"
            .Snd_Win_Open = ""
            .Snd_Win_PrintComplete = ""
            .Snd_Win_ProximityConnection = My.PATH_Windows & "\media\Windows Proximity Connection.wav"
            .Snd_Win_RestoreDown = ""
            .Snd_Win_RestoreUp = ""
            .Snd_Win_ShowBand = ""
            .Snd_Win_SystemAsterisk = My.PATH_Windows & "\media\Windows Background.wav"
            .Snd_Win_SystemExclamation = My.PATH_Windows & "\media\Windows Background.wav"
            .Snd_Win_SystemExit = ""
            .Snd_Win_SystemStart = ""
            .Snd_Win_SystemHand = My.PATH_Windows & "\media\Windows Foreground.wav"
            .Snd_Win_SystemNotification = My.PATH_Windows & "\media\Windows Background.wav"
            .Snd_Win_SystemQuestion = ""
            .Snd_Win_WindowsLogoff = ""
            .Snd_Win_WindowsLogon = ""
            .Snd_Win_WindowsUAC = My.PATH_Windows & "\media\Windows User Account Control.wav"
            .Snd_Win_WindowsUnlock = My.PATH_Windows & "\media\Windows Unlock.wav"
            .Snd_Explorer_ActivatingDocument = ""
            .Snd_Explorer_BlockedPopup = My.PATH_Windows & "\media\Windows Pop-up Blocked.wav"
            .Snd_Explorer_EmptyRecycleBin = ""
            .Snd_Explorer_FeedDiscovered = My.PATH_Windows & "\media\Windows Feed Discovered.wav"
            .Snd_Explorer_MoveMenuItem = ""
            .Snd_Explorer_Navigating = ""
            .Snd_Explorer_SecurityBand = My.PATH_Windows & "\media\Windows Information Bar.wav"
            .Snd_Explorer_SearchProviderDiscovered = ""
            .Snd_Explorer_FaxError = ""
            .Snd_Explorer_FaxLineRings = ""
            .Snd_Explorer_FaxNew = ""
            .Snd_Explorer_FaxSent = ""
            .Snd_NetMeeting_PersonJoins = ""
            .Snd_NetMeeting_PersonLeaves = ""
            .Snd_NetMeeting_ReceiveCall = ""
            .Snd_NetMeeting_ReceiveRequestToJoin = ""
            .Snd_SpeechRec_DisNumbersSound = My.PATH_Windows & "\media\Speech Disambiguation.wav"
            .Snd_SpeechRec_HubOffSound = My.PATH_Windows & "\media\Speech Off.wav"
            .Snd_SpeechRec_HubOnSound = My.PATH_Windows & "\media\Speech On.wav"
            .Snd_SpeechRec_HubSleepSound = My.PATH_Windows & "\media\Speech Sleep.wav"
            .Snd_SpeechRec_MisrecoSound = My.PATH_Windows & "\media\Speech Misrecognition.wav"
            .Snd_SpeechRec_PanelSound = My.PATH_Windows & "\media\Speech Disambiguation.wav"

            .Snd_Win_SystemExit_TaskMgmt = True
            .Snd_Win_WindowsLogoff_TaskMgmt = True
            .Snd_Win_WindowsLogon_TaskMgmt = True
            .Snd_Win_WindowsUnlock_TaskMgmt = True
        End With

        Return [CP]
    End Function

    Public Function Default_Windows7() As CP
        Dim [CP] As New CP(CP.CP_Type.Empty)

        With [CP].Info
            .ThemeName = "Windows 7 (Initial)"
            .Description = "Initial; Like first time after Windows Setup"
            .ThemeVersion = "1.0.0.0"
            .Author = "Microsoft"
            .AuthorSocialMediaLink = "www.microsoft.com"
            .AppVersion = My.AppVersion
        End With

        With [CP].Windows81
            .ColorizationColor = Color.FromArgb(246, 195, 74)
            .ColorizationColorBalance = 78
            .PersonalColors_Background = Color.FromArgb(30, 0, 84)
            .PersonalColors_Accent = Color.FromArgb(72, 29, 178)
            .StartColor = Color.FromArgb(30, 0, 84)
            .AccentColor = Color.FromArgb(72, 29, 178)
            .Start = 0
            .Theme = CP.Structures.Windows7.Themes.Aero
            .LogonUI = 0
            .NoLockScreen = False
            .LockScreenType = CP.Structures.LogonUI7.Modes.Default_
            .LockScreenSystemID = 0
        End With

        With [CP].Windows7
            .ColorizationColor = Color.FromArgb(116, 184, 252)
            .ColorizationAfterglow = Color.FromArgb(116, 184, 252)
            .ColorizationColorBalance = 8
            .ColorizationAfterglowBalance = 43
            .ColorizationBlurBalance = 49
            .ColorizationGlassReflectionIntensity = 0
            .EnableAeroPeek = True
            .AlwaysHibernateThumbnails = False
            .Theme = CP.Structures.Windows7.Themes.Aero
        End With

        With [CP].WindowsVista
            .ColorizationColor = Color.FromArgb(64, 158, 254)
        End With

        With [CP].CommandPrompt
            .ColorTable05 = Color.FromArgb(136, 23, 152)
            .ColorTable06 = Color.FromArgb(193, 156, 0)
            .PopupBackground = 15
            .PopupForeground = 5
            .ScreenColorsForeground = 7
            .ScreenColorsBackground = 0
            .FaceName = "Consolas"
            .FontSize = 18 * 65536
            .FontRaster = True
            .W10_1909_ForceV2 = False
        End With

        With [CP].PowerShellx86
            .ColorTable05 = Color.FromArgb(1, 36, 86)
            .ColorTable06 = Color.FromArgb(238, 237, 240)
            .PopupBackground = 15
            .PopupForeground = 3
            .ScreenColorsForeground = 6
            .ScreenColorsBackground = 5
            .FaceName = "Consolas"
            .FontSize = 14 * 65536
            .FontRaster = True
            .W10_1909_ForceV2 = False
        End With

        With [CP].PowerShellx64
            .ColorTable05 = Color.FromArgb(1, 36, 86)
            .ColorTable06 = Color.FromArgb(238, 237, 240)
            .PopupBackground = 15
            .PopupForeground = 3
            .ScreenColorsForeground = 6
            .ScreenColorsBackground = 5
            .FaceName = "Consolas"
            .FontSize = 14 * 65536
            .FontRaster = True
            .W10_1909_ForceV2 = False
        End With

        With [CP].MetricsFonts
            .BorderWidth = 1
            .CaptionHeight = 21
            .CaptionWidth = 21
            .IconSpacing = 75
            .IconVerticalSpacing = 75
            .MenuHeight = 19
            .MenuWidth = 19
            .PaddedBorderWidth = 4
            .ScrollHeight = 17
            .ScrollWidth = 17
            .SmCaptionHeight = 17
            .SmCaptionWidth = 17
            .DesktopIconSize = 48
            .ShellIconSize = 32
        End With

        With [CP].WindowsEffects
            .ShakeToMinimize = True
            .BalloonNotifications = True
            .PaintDesktopVersion = False
            .ShowSecondsInSystemClock = False
            .Win11ClassicContextMenu = False
            .SysListView32 = False
        End With

        [CP].Terminal = New WinTerminal("", WinTerminal.Mode.Empty)
        [CP].TerminalPreview = New WinTerminal("", WinTerminal.Mode.Empty)
        [CP].Cursor_Shadow = True

        With [CP].Sounds
            .Snd_Imageres_SystemStart = "Default"
            .Snd_Win_Default = My.PATH_Windows & "\media\Windows Ding.wav"
            .Snd_Win_AppGPFault = ""
            .Snd_Win_CCSelect = ""
            .Snd_Win_ChangeTheme = My.PATH_Windows & "\media\Windows Logon Sound.wav"
            .Snd_Win_Close = ""
            .Snd_Win_CriticalBatteryAlarm = My.PATH_Windows & "\media\Windows Battery Critical.wav"
            .Snd_Win_DeviceConnect = My.PATH_Windows & "\media\Windows Hardware Insert.wav"
            .Snd_Win_DeviceDisconnect = My.PATH_Windows & "\media\Windows Hardware Remove.wav"
            .Snd_Win_DeviceFail = My.PATH_Windows & "\media\Windows Hardware Fail.wav"
            .Snd_Win_FaxBeep = My.PATH_Windows & "\media\Windows Notify.wav"
            .Snd_Win_LowBatteryAlarm = My.PATH_Windows & "\media\Windows Battery Low.wav"
            .Snd_Win_MailBeep = My.PATH_Windows & "\media\Windows Notify.wav"
            .Snd_Win_Maximize = ""
            .Snd_Win_MenuCommand = ""
            .Snd_Win_MenuPopup = ""
            .Snd_Win_MessageNudge = ""
            .Snd_Win_Minimize = ""
            .Snd_Win_Notification_Default = ""
            .Snd_Win_Notification_IM = ""
            .Snd_Win_Notification_Looping_Alarm = ""
            .Snd_Win_Notification_Looping_Alarm2 = ""
            .Snd_Win_Notification_Looping_Alarm3 = ""
            .Snd_Win_Notification_Looping_Alarm4 = ""
            .Snd_Win_Notification_Looping_Alarm5 = ""
            .Snd_Win_Notification_Looping_Alarm6 = ""
            .Snd_Win_Notification_Looping_Alarm7 = ""
            .Snd_Win_Notification_Looping_Alarm8 = ""
            .Snd_Win_Notification_Looping_Alarm9 = ""
            .Snd_Win_Notification_Looping_Alarm10 = ""
            .Snd_Win_Notification_Looping_Call = ""
            .Snd_Win_Notification_Looping_Call2 = ""
            .Snd_Win_Notification_Looping_Call3 = ""
            .Snd_Win_Notification_Looping_Call4 = ""
            .Snd_Win_Notification_Looping_Call5 = ""
            .Snd_Win_Notification_Looping_Call6 = ""
            .Snd_Win_Notification_Looping_Call7 = ""
            .Snd_Win_Notification_Looping_Call8 = ""
            .Snd_Win_Notification_Looping_Call9 = ""
            .Snd_Win_Notification_Looping_Call10 = ""
            .Snd_Win_Notification_Mail = ""
            .Snd_Win_Notification_Proximity = ""
            .Snd_Win_Notification_Reminder = ""
            .Snd_Win_Notification_SMS = ""
            .Snd_Win_Open = ""
            .Snd_Win_PrintComplete = ""
            .Snd_Win_ProximityConnection = ""
            .Snd_Win_RestoreDown = ""
            .Snd_Win_RestoreUp = ""
            .Snd_Win_ShowBand = ""
            .Snd_Win_SystemAsterisk = My.PATH_Windows & "\media\Windows Error.wav"
            .Snd_Win_SystemExclamation = My.PATH_Windows & "\media\Windows Exclamation.wav"
            .Snd_Win_SystemExit = My.PATH_Windows & "\media\Windows Shutdown.wav"
            .Snd_Win_SystemStart = ""
            .Snd_Win_SystemHand = My.PATH_Windows & "\media\Windows Critical Stop.wav"
            .Snd_Win_SystemNotification = My.PATH_Windows & "\media\Windows Balloon.wav"
            .Snd_Win_SystemQuestion = ""
            .Snd_Win_WindowsLogoff = My.PATH_Windows & "\media\Windows Logoff Sound.wav"
            .Snd_Win_WindowsLogon = My.PATH_Windows & "\media\Windows Logon Sound.wav"
            .Snd_Win_WindowsUAC = My.PATH_Windows & "\media\Windows User Account Control.wav"
            .Snd_Win_WindowsUnlock = ""
            .Snd_Explorer_ActivatingDocument = ""
            .Snd_Explorer_BlockedPopup = My.PATH_Windows & "\media\Windows Pop-up Blocked.wav"
            .Snd_Explorer_EmptyRecycleBin = My.PATH_Windows & "\media\Windows Recycle.wav"
            .Snd_Explorer_FeedDiscovered = My.PATH_Windows & "\media\Windows Feed Discovered.wav"
            .Snd_Explorer_MoveMenuItem = ""
            .Snd_Explorer_Navigating = My.PATH_Windows & "\media\Windows Navigation Start.wav"
            .Snd_Explorer_SecurityBand = My.PATH_Windows & "\media\Windows Information Bar.wav"
            .Snd_Explorer_SearchProviderDiscovered = ""
            .Snd_Explorer_FaxError = My.PATH_Windows & "\media\ding.wav"
            .Snd_Explorer_FaxLineRings = My.PATH_Windows & "\media\Windows Ringin.wav"
            .Snd_Explorer_FaxNew = ""
            .Snd_Explorer_FaxSent = My.PATH_Windows & "\media\tada.wav"
            .Snd_NetMeeting_PersonJoins = ""
            .Snd_NetMeeting_PersonLeaves = ""
            .Snd_NetMeeting_ReceiveCall = ""
            .Snd_NetMeeting_ReceiveRequestToJoin = ""
            .Snd_SpeechRec_DisNumbersSound = My.PATH_Windows & "\media\Speech Disambiguation.wav"
            .Snd_SpeechRec_HubOffSound = My.PATH_Windows & "\media\Speech Off.wav"
            .Snd_SpeechRec_HubOnSound = My.PATH_Windows & "\media\Speech On.wav"
            .Snd_SpeechRec_HubSleepSound = My.PATH_Windows & "\media\Speech Sleep.wav"
            .Snd_SpeechRec_MisrecoSound = My.PATH_Windows & "\media\Speech Misrecognition.wav"
            .Snd_SpeechRec_PanelSound = My.PATH_Windows & "\media\Speech Disambiguation.wav"

            .Snd_Win_SystemExit_TaskMgmt = False
            .Snd_Win_WindowsLogoff_TaskMgmt = False
            .Snd_Win_WindowsLogon_TaskMgmt = False
            .Snd_Win_WindowsUnlock_TaskMgmt = False
        End With

        Return [CP]
    End Function

    Public Function Default_WindowsVista() As CP
        Dim [CP] As New CP(CP.CP_Type.Empty)

        With [CP].Info
            .ThemeName = "Windows Vista (Initial)"
            .Description = "Initial; Like first time after Windows Setup"
            .ThemeVersion = "1.0.0.0"
            .Author = "Microsoft"
            .AuthorSocialMediaLink = "www.microsoft.com"
            .AppVersion = My.AppVersion
        End With

        With [CP].Windows81
            .ColorizationColor = Color.FromArgb(246, 195, 74)
            .ColorizationColorBalance = 78
            .PersonalColors_Background = Color.FromArgb(30, 0, 84)
            .PersonalColors_Accent = Color.FromArgb(72, 29, 178)
            .StartColor = Color.FromArgb(30, 0, 84)
            .AccentColor = Color.FromArgb(72, 29, 178)
            .Start = 0
            .Theme = CP.Structures.Windows7.Themes.Aero
            .LogonUI = 0
            .NoLockScreen = False
            .LockScreenType = CP.Structures.LogonUI7.Modes.Default_
            .LockScreenSystemID = 0
        End With

        With [CP].Windows7
            .ColorizationColor = Color.FromArgb(116, 184, 252)
            .ColorizationAfterglow = Color.FromArgb(116, 184, 252)
            .ColorizationColorBalance = 8
            .ColorizationAfterglowBalance = 43
            .ColorizationBlurBalance = 49
            .ColorizationGlassReflectionIntensity = 0
            .EnableAeroPeek = True
            .AlwaysHibernateThumbnails = False
            .Theme = CP.Structures.Windows7.Themes.Aero
        End With

        With [CP].WindowsVista
            .ColorizationColor = Color.FromArgb(64, 158, 254)
        End With

        With [CP].CommandPrompt
            .ColorTable00 = Color.FromArgb(0, 0, 0)
            .ColorTable01 = Color.FromArgb(0, 0, 128)
            .ColorTable02 = Color.FromArgb(0, 128, 0)
            .ColorTable03 = Color.FromArgb(0, 128, 128)
            .ColorTable04 = Color.FromArgb(128, 0, 0)
            .ColorTable05 = Color.FromArgb(128, 0, 128)
            .ColorTable06 = Color.FromArgb(128, 128, 0)
            .ColorTable07 = Color.FromArgb(192, 192, 192)
            .ColorTable08 = Color.FromArgb(128, 128, 128)
            .ColorTable09 = Color.FromArgb(0, 0, 255)
            .ColorTable10 = Color.FromArgb(0, 255, 0)
            .ColorTable11 = Color.FromArgb(0, 255, 255)
            .ColorTable12 = Color.FromArgb(255, 0, 0)
            .ColorTable13 = Color.FromArgb(255, 0, 255)
            .ColorTable14 = Color.FromArgb(255, 255, 0)
            .ColorTable15 = Color.FromArgb(255, 255, 255)
            .PopupForeground = 5
            .PopupBackground = 15
            .ScreenColorsForeground = 7
            .ScreenColorsBackground = 0
            .FaceName = "Consolas"
            .FontSize = 18 * 65536
            .FontRaster = True
            .W10_1909_ForceV2 = False
        End With

        With [CP].PowerShellx86
            .ColorTable00 = Color.FromArgb(12, 12, 12)
            .ColorTable01 = Color.FromArgb(0, 55, 218)
            .ColorTable02 = Color.FromArgb(19, 161, 14)
            .ColorTable03 = Color.FromArgb(58, 150, 221)
            .ColorTable04 = Color.FromArgb(197, 15, 31)
            .ColorTable05 = Color.FromArgb(1, 36, 86)
            .ColorTable06 = Color.FromArgb(238, 237, 240)
            .ColorTable07 = Color.FromArgb(204, 204, 204)
            .ColorTable08 = Color.FromArgb(118, 118, 118)
            .ColorTable09 = Color.FromArgb(59, 120, 255)
            .ColorTable10 = Color.FromArgb(22, 198, 12)
            .ColorTable11 = Color.FromArgb(97, 214, 214)
            .ColorTable12 = Color.FromArgb(231, 72, 86)
            .ColorTable13 = Color.FromArgb(180, 0, 158)
            .ColorTable14 = Color.FromArgb(249, 241, 165)
            .ColorTable15 = Color.FromArgb(242, 242, 242)
            .PopupForeground = 15
            .PopupBackground = 3
            .ScreenColorsForeground = 6
            .ScreenColorsBackground = 5
            .FaceName = "Consolas"
            .FontSize = 14 * 65536
            .FontRaster = True
            .W10_1909_ForceV2 = False
        End With

        With [CP].PowerShellx64
            .ColorTable00 = Color.FromArgb(12, 12, 12)
            .ColorTable01 = Color.FromArgb(0, 55, 218)
            .ColorTable02 = Color.FromArgb(19, 161, 14)
            .ColorTable03 = Color.FromArgb(58, 150, 221)
            .ColorTable04 = Color.FromArgb(197, 15, 31)
            .ColorTable05 = Color.FromArgb(1, 36, 86)
            .ColorTable06 = Color.FromArgb(238, 237, 240)
            .ColorTable07 = Color.FromArgb(204, 204, 204)
            .ColorTable08 = Color.FromArgb(118, 118, 118)
            .ColorTable09 = Color.FromArgb(59, 120, 255)
            .ColorTable10 = Color.FromArgb(22, 198, 12)
            .ColorTable11 = Color.FromArgb(97, 214, 214)
            .ColorTable12 = Color.FromArgb(231, 72, 86)
            .ColorTable13 = Color.FromArgb(180, 0, 158)
            .ColorTable14 = Color.FromArgb(249, 241, 165)
            .ColorTable15 = Color.FromArgb(242, 242, 242)
            .PopupForeground = 15
            .PopupBackground = 3
            .ScreenColorsForeground = 6
            .ScreenColorsBackground = 5
            .FaceName = "Consolas"
            .FontSize = 14 * 65536
            .FontRaster = True
            .W10_1909_ForceV2 = False
        End With

        With [CP].MetricsFonts
            .BorderWidth = 1
            .CaptionHeight = 19
            .CaptionWidth = 19
            .IconSpacing = 75
            .IconVerticalSpacing = 75
            .MenuHeight = 19
            .MenuWidth = 19
            .PaddedBorderWidth = 4
            .ScrollHeight = 17
            .ScrollWidth = 17
            .SmCaptionHeight = 17
            .SmCaptionWidth = 17
            .DesktopIconSize = 48
            .ShellIconSize = 32
        End With

        With [CP].WindowsEffects
            .ShakeToMinimize = False
            .BalloonNotifications = True
            .PaintDesktopVersion = False
            .ShowSecondsInSystemClock = False
            .Win11ClassicContextMenu = False
            .SysListView32 = True
        End With

        [CP].Terminal = New WinTerminal("", WinTerminal.Mode.Empty)
        [CP].TerminalPreview = New WinTerminal("", WinTerminal.Mode.Empty)
        [CP].Cursor_Shadow = True

        With [CP].Sounds
            .Snd_Imageres_SystemStart = "Default"
            .Snd_Win_Default = My.PATH_Windows & "\media\Windows Ding.wav"
            .Snd_Win_AppGPFault = ""
            .Snd_Win_CCSelect = ""
            .Snd_Win_ChangeTheme = ""
            .Snd_Win_Close = ""
            .Snd_Win_CriticalBatteryAlarm = My.PATH_Windows & "\media\Windows Battery Critical.wav"
            .Snd_Win_DeviceConnect = My.PATH_Windows & "\media\Windows Hardware Insert.wav"
            .Snd_Win_DeviceDisconnect = My.PATH_Windows & "\media\Windows Hardware Remove.wav"
            .Snd_Win_DeviceFail = My.PATH_Windows & "\media\Windows Hardware Fail.wav"
            .Snd_Win_FaxBeep = My.PATH_Windows & "\media\Windows Notify.wav"
            .Snd_Win_LowBatteryAlarm = My.PATH_Windows & "\media\Windows Battery Low.wav"
            .Snd_Win_MailBeep = My.PATH_Windows & "\media\Windows Notify.wav"
            .Snd_Win_Maximize = ""
            .Snd_Win_MenuCommand = ""
            .Snd_Win_MenuPopup = ""
            .Snd_Win_MessageNudge = ""
            .Snd_Win_Minimize = ""
            .Snd_Win_Notification_Default = ""
            .Snd_Win_Notification_IM = ""
            .Snd_Win_Notification_Looping_Alarm = ""
            .Snd_Win_Notification_Looping_Alarm2 = ""
            .Snd_Win_Notification_Looping_Alarm3 = ""
            .Snd_Win_Notification_Looping_Alarm4 = ""
            .Snd_Win_Notification_Looping_Alarm5 = ""
            .Snd_Win_Notification_Looping_Alarm6 = ""
            .Snd_Win_Notification_Looping_Alarm7 = ""
            .Snd_Win_Notification_Looping_Alarm8 = ""
            .Snd_Win_Notification_Looping_Alarm9 = ""
            .Snd_Win_Notification_Looping_Alarm10 = ""
            .Snd_Win_Notification_Looping_Call = ""
            .Snd_Win_Notification_Looping_Call2 = ""
            .Snd_Win_Notification_Looping_Call3 = ""
            .Snd_Win_Notification_Looping_Call4 = ""
            .Snd_Win_Notification_Looping_Call5 = ""
            .Snd_Win_Notification_Looping_Call6 = ""
            .Snd_Win_Notification_Looping_Call7 = ""
            .Snd_Win_Notification_Looping_Call8 = ""
            .Snd_Win_Notification_Looping_Call9 = ""
            .Snd_Win_Notification_Looping_Call10 = ""
            .Snd_Win_Notification_Mail = ""
            .Snd_Win_Notification_Proximity = ""
            .Snd_Win_Notification_Reminder = ""
            .Snd_Win_Notification_SMS = ""
            .Snd_Win_Open = ""
            .Snd_Win_PrintComplete = ""
            .Snd_Win_ProximityConnection = ""
            .Snd_Win_RestoreDown = ""
            .Snd_Win_RestoreUp = ""
            .Snd_Win_ShowBand = ""
            .Snd_Win_SystemAsterisk = My.PATH_Windows & "\media\Windows Error.wav"
            .Snd_Win_SystemExclamation = My.PATH_Windows & "\media\Windows Exclamation.wav"
            .Snd_Win_SystemExit = My.PATH_Windows & "\media\Windows Shutdown.wav"
            .Snd_Win_SystemStart = ""
            .Snd_Win_SystemHand = My.PATH_Windows & "\media\Windows Critical Stop.wav"
            .Snd_Win_SystemNotification = My.PATH_Windows & "\media\Windows Balloon.wav"
            .Snd_Win_SystemQuestion = ""
            .Snd_Win_WindowsLogoff = My.PATH_Windows & "\media\Windows Logoff Sound.wav"
            .Snd_Win_WindowsLogon = My.PATH_Windows & "\media\Windows Logon Sound.wav"
            .Snd_Win_WindowsUAC = My.PATH_Windows & "\media\Windows User Account Control.wav"
            .Snd_Win_WindowsUnlock = ""
            .Snd_Explorer_ActivatingDocument = ""
            .Snd_Explorer_BlockedPopup = My.PATH_Windows & "\media\Windows Pop-up Blocked.wav"
            .Snd_Explorer_EmptyRecycleBin = My.PATH_Windows & "\media\Windows Recycle.wav"
            .Snd_Explorer_FeedDiscovered = My.PATH_Windows & "\media\Windows Feed Discovered.wav"
            .Snd_Explorer_MoveMenuItem = ""
            .Snd_Explorer_Navigating = My.PATH_Windows & "\media\Windows Navigation Start.wav"
            .Snd_Explorer_SecurityBand = My.PATH_Windows & "\media\Windows Information Bar.wav"
            .Snd_Explorer_SearchProviderDiscovered = ""
            .Snd_Explorer_FaxError = My.PATH_Windows & "\media\ding.wav"
            .Snd_Explorer_FaxLineRings = My.PATH_Windows & "\media\Windows Ringin.wav"
            .Snd_Explorer_FaxNew = ""
            .Snd_Explorer_FaxSent = My.PATH_Windows & "\media\tada.wav"
            .Snd_NetMeeting_PersonJoins = ""
            .Snd_NetMeeting_PersonLeaves = ""
            .Snd_NetMeeting_ReceiveCall = ""
            .Snd_NetMeeting_ReceiveRequestToJoin = ""
            .Snd_SpeechRec_DisNumbersSound = My.PATH_Windows & "\media\Speech Disambiguation.wav"
            .Snd_SpeechRec_HubOffSound = My.PATH_Windows & "\media\Speech Off.wav"
            .Snd_SpeechRec_HubOnSound = My.PATH_Windows & "\media\Speech On.wav"
            .Snd_SpeechRec_HubSleepSound = My.PATH_Windows & "\media\Speech Sleep.wav"
            .Snd_SpeechRec_MisrecoSound = My.PATH_Windows & "\media\Speech Misrecognition.wav"
            .Snd_SpeechRec_PanelSound = My.PATH_Windows & "\media\Speech Disambiguation.wav"

            .Snd_Win_SystemExit_TaskMgmt = False
            .Snd_Win_WindowsLogoff_TaskMgmt = False
            .Snd_Win_WindowsLogon_TaskMgmt = False
            .Snd_Win_WindowsUnlock_TaskMgmt = False
        End With

        Return [CP]
    End Function

    Public Function Default_WindowsXP() As CP
        Dim [CP] As New CP(CP.CP_Type.Empty)

        With [CP].Info
            .ThemeName = "Windows XP (Initial)"
            .Description = "Initial; Like first time after Windows Setup"
            .ThemeVersion = "1.0.0.0"
            .Author = "Microsoft"
            .AuthorSocialMediaLink = "www.microsoft.com"
            .AppVersion = My.AppVersion
        End With

        With [CP].Win32
            .ActiveBorder = Color.FromArgb(212, 208, 200)
            .ActiveTitle = Color.FromArgb(0, 84, 227)
            .AppWorkspace = Color.FromArgb(128, 128, 128)
            .Background = Color.FromArgb(0, 78, 152)
            .ButtonAlternateFace = Color.FromArgb(181, 181, 181)
            .ButtonDkShadow = Color.FromArgb(113, 111, 100)
            .ButtonFace = Color.FromArgb(236, 233, 216)
            .ButtonHilight = Color.FromArgb(255, 255, 255)
            .ButtonLight = Color.FromArgb(241, 239, 226)
            .ButtonShadow = Color.FromArgb(172, 168, 153)
            .ButtonText = Color.FromArgb(0, 0, 0)
            .GradientActiveTitle = Color.FromArgb(61, 149, 255)
            .GradientInactiveTitle = Color.FromArgb(157, 185, 235)
            .GrayText = Color.FromArgb(172, 168, 153)
            .HilightText = Color.FromArgb(255, 255, 255)
            .HotTrackingColor = Color.FromArgb(0, 0, 128)
            .InactiveBorder = Color.FromArgb(212, 208, 200)
            .InactiveTitle = Color.FromArgb(212, 208, 200)
            .InactiveTitleText = Color.FromArgb(216, 228, 248)
            .InfoText = Color.FromArgb(0, 0, 0)
            .InfoWindow = Color.FromArgb(255, 255, 225)
            .Menu = Color.FromArgb(255, 255, 255)
            .MenuBar = Color.FromArgb(236, 233, 216)
            .MenuText = Color.FromArgb(0, 0, 0)
            .Scrollbar = Color.FromArgb(212, 208, 200)
            .TitleText = Color.FromArgb(255, 255, 255)
            .Window = Color.FromArgb(255, 255, 255)
            .WindowFrame = Color.FromArgb(0, 0, 0)
            .WindowText = Color.FromArgb(0, 0, 0)
            .Hilight = Color.FromArgb(49, 106, 197)
            .MenuHilight = Color.FromArgb(49, 106, 197)
            .Desktop = Color.FromArgb(0, 0, 0)
        End With

        With [CP].CommandPrompt
            .ColorTable00 = Color.FromArgb(12, 12, 12)
            .ColorTable01 = Color.FromArgb(0, 55, 218)
            .ColorTable02 = Color.FromArgb(19, 161, 14)
            .ColorTable03 = Color.FromArgb(58, 150, 221)
            .ColorTable04 = Color.FromArgb(197, 15, 31)
            .ColorTable05 = Color.FromArgb(136, 23, 152)
            .ColorTable06 = Color.FromArgb(193, 156, 0)
            .ColorTable07 = Color.FromArgb(204, 204, 204)
            .ColorTable08 = Color.FromArgb(118, 118, 118)
            .ColorTable09 = Color.FromArgb(59, 120, 255)
            .ColorTable10 = Color.FromArgb(22, 198, 12)
            .ColorTable11 = Color.FromArgb(97, 214, 214)
            .ColorTable12 = Color.FromArgb(231, 72, 86)
            .ColorTable13 = Color.FromArgb(180, 0, 158)
            .ColorTable14 = Color.FromArgb(249, 241, 165)
            .ColorTable15 = Color.FromArgb(242, 242, 242)
            .PopupBackground = 15
            .PopupForeground = 5
            .ScreenColorsForeground = 7
            .ScreenColorsBackground = 0
            .FaceName = "Consolas"
            .FontSize = 18 * 65536
            .FontRaster = True
            .W10_1909_ForceV2 = False
        End With

        With [CP].PowerShellx86
            .ColorTable00 = Color.FromArgb(12, 12, 12)
            .ColorTable01 = Color.FromArgb(0, 55, 218)
            .ColorTable02 = Color.FromArgb(19, 161, 14)
            .ColorTable03 = Color.FromArgb(58, 150, 221)
            .ColorTable04 = Color.FromArgb(197, 15, 31)
            .ColorTable05 = Color.FromArgb(1, 36, 86)
            .ColorTable06 = Color.FromArgb(238, 237, 240)
            .ColorTable07 = Color.FromArgb(204, 204, 204)
            .ColorTable08 = Color.FromArgb(118, 118, 118)
            .ColorTable09 = Color.FromArgb(59, 120, 255)
            .ColorTable10 = Color.FromArgb(22, 198, 12)
            .ColorTable11 = Color.FromArgb(97, 214, 214)
            .ColorTable12 = Color.FromArgb(231, 72, 86)
            .ColorTable13 = Color.FromArgb(180, 0, 158)
            .ColorTable14 = Color.FromArgb(249, 241, 165)
            .ColorTable15 = Color.FromArgb(242, 242, 242)
            .PopupBackground = 15
            .PopupForeground = 3
            .ScreenColorsForeground = 6
            .ScreenColorsBackground = 5
            .FaceName = "Consolas"
            .FontSize = 14 * 65536
            .FontRaster = True
            .W10_1909_ForceV2 = False
        End With

        With [CP].PowerShellx64
            .ColorTable00 = Color.FromArgb(12, 12, 12)
            .ColorTable01 = Color.FromArgb(0, 55, 218)
            .ColorTable02 = Color.FromArgb(19, 161, 14)
            .ColorTable03 = Color.FromArgb(58, 150, 221)
            .ColorTable04 = Color.FromArgb(197, 15, 31)
            .ColorTable05 = Color.FromArgb(1, 36, 86)
            .ColorTable06 = Color.FromArgb(238, 237, 240)
            .ColorTable07 = Color.FromArgb(204, 204, 204)
            .ColorTable08 = Color.FromArgb(118, 118, 118)
            .ColorTable09 = Color.FromArgb(59, 120, 255)
            .ColorTable10 = Color.FromArgb(22, 198, 12)
            .ColorTable11 = Color.FromArgb(97, 214, 214)
            .ColorTable12 = Color.FromArgb(231, 72, 86)
            .ColorTable13 = Color.FromArgb(180, 0, 158)
            .ColorTable14 = Color.FromArgb(249, 241, 165)
            .ColorTable15 = Color.FromArgb(242, 242, 242)
            .PopupBackground = 15
            .PopupForeground = 3
            .ScreenColorsForeground = 6
            .ScreenColorsBackground = 5
            .FaceName = "Consolas"
            .FontSize = 14 * 65536
            .FontRaster = True
            .W10_1909_ForceV2 = False
        End With

        With [CP].MetricsFonts
            .BorderWidth = 0
            .CaptionHeight = 25
            .CaptionWidth = 18
            .IconSpacing = 75
            .IconVerticalSpacing = 75
            .MenuHeight = 19
            .MenuWidth = 18
            .PaddedBorderWidth = 4
            .ScrollHeight = 17
            .ScrollWidth = 17
            .SmCaptionHeight = 17
            .SmCaptionWidth = 17
            .DesktopIconSize = 48
            .ShellIconSize = 32
            .Fonts_SingleBitPP = True
            .CaptionFont = New Font("Trebuchet MS", 9.75, FontStyle.Bold)
            .SmCaptionFont = New Font("Tahoma", 8.25, FontStyle.Regular)
            .IconFont = New Font("Tahoma", 8.25, FontStyle.Regular)
            .MenuFont = New Font("Tahoma", 8.25, FontStyle.Regular)
            .MessageFont = New Font("Tahoma", 8.25, FontStyle.Regular)
            .StatusFont = New Font("Tahoma", 8.25, FontStyle.Regular)
        End With

        [CP].Cursor_Shadow = True
        With [CP].Cursor_Arrow
            .ArrowStyle = ArrowStyle.Classic
            .CircleStyle = CircleStyle.Classic
            .PrimaryColor1 = Color.White
            .PrimaryColor2 = Color.White
            .SecondaryColor1 = Color.Black
            .SecondaryColor2 = Color.Black
            .PrimaryColorGradient = False
            .SecondaryColorGradient = False
            .LoadingCircleBack1 = Color.White
            .LoadingCircleBack2 = Color.White
            .LoadingCircleHot1 = Color.Black
            .LoadingCircleHot2 = Color.Black
            .LoadingCircleBackGradient = False
            .LoadingCircleHotGradient = False
            .Shadow_Enabled = False
            .Shadow_Color = Color.Black
            .Shadow_Blur = 5
            .Shadow_Opacity = 0.3
            .Shadow_OffsetX = 2
            .Shadow_OffsetY = 2
        End With

        With [CP].Cursor_Help
            .ArrowStyle = ArrowStyle.Classic
            .CircleStyle = CircleStyle.Classic
            .PrimaryColor1 = Color.White
            .PrimaryColor2 = Color.White
            .SecondaryColor1 = Color.Black
            .SecondaryColor2 = Color.Black
            .PrimaryColorGradient = False
            .SecondaryColorGradient = False
            .LoadingCircleBack1 = Color.White
            .LoadingCircleBack2 = Color.White
            .LoadingCircleHot1 = Color.Black
            .LoadingCircleHot2 = Color.Black
            .LoadingCircleBackGradient = False
            .LoadingCircleHotGradient = False
            .Shadow_Enabled = False
            .Shadow_Color = Color.Black
            .Shadow_Blur = 5
            .Shadow_Opacity = 0.3
            .Shadow_OffsetX = 2
            .Shadow_OffsetY = 2
        End With

        With [CP].Cursor_AppLoading
            .ArrowStyle = ArrowStyle.Classic
            .CircleStyle = CircleStyle.Classic
            .PrimaryColor1 = Color.White
            .PrimaryColor2 = Color.White
            .SecondaryColor1 = Color.Black
            .SecondaryColor2 = Color.Black
            .LoadingCircleBack1 = Color.White
            .LoadingCircleBack2 = Color.White
            .LoadingCircleHot1 = Color.Black
            .LoadingCircleHot2 = Color.Black
            .LoadingCircleBackGradient = False
            .LoadingCircleHotGradient = False
            .PrimaryColorGradient = False
            .SecondaryColorGradient = False
            .Shadow_Enabled = False
            .Shadow_Color = Color.Black
            .Shadow_Blur = 5
            .Shadow_Opacity = 0.3
            .Shadow_OffsetX = 2
            .Shadow_OffsetY = 2
        End With

        With [CP].Cursor_Busy
            .ArrowStyle = ArrowStyle.Classic
            .CircleStyle = CircleStyle.Classic
            .PrimaryColor1 = Color.White
            .PrimaryColor2 = Color.White
            .SecondaryColor1 = Color.Black
            .SecondaryColor2 = Color.Black
            .LoadingCircleBack1 = Color.White
            .LoadingCircleBack2 = Color.White
            .LoadingCircleHot1 = Color.Black
            .LoadingCircleHot2 = Color.Black
            .LoadingCircleBackGradient = False
            .LoadingCircleHotGradient = False
            .PrimaryColorGradient = False
            .SecondaryColorGradient = False
            .Shadow_Enabled = False
            .Shadow_Color = Color.Black
            .Shadow_Blur = 5
            .Shadow_Opacity = 0.3
            .Shadow_OffsetX = 2
            .Shadow_OffsetY = 2
        End With

        With [CP].Cursor_Up
            .ArrowStyle = ArrowStyle.Classic
            .CircleStyle = CircleStyle.Classic
            .PrimaryColor1 = Color.White
            .PrimaryColor2 = Color.White
            .SecondaryColor1 = Color.Black
            .SecondaryColor2 = Color.Black
            .PrimaryColorGradient = False
            .SecondaryColorGradient = False
            .LoadingCircleBack1 = Color.White
            .LoadingCircleBack2 = Color.White
            .LoadingCircleHot1 = Color.Black
            .LoadingCircleHot2 = Color.Black
            .LoadingCircleBackGradient = False
            .LoadingCircleHotGradient = False
            .Shadow_Enabled = False
            .Shadow_Color = Color.Black
            .Shadow_Blur = 5
            .Shadow_Opacity = 0.3
            .Shadow_OffsetX = 2
            .Shadow_OffsetY = 2
        End With

        With [CP].Cursor_NS
            .ArrowStyle = ArrowStyle.Classic
            .CircleStyle = CircleStyle.Classic
            .PrimaryColor1 = Color.White
            .PrimaryColor2 = Color.White
            .SecondaryColor1 = Color.Black
            .SecondaryColor2 = Color.Black
            .PrimaryColorGradient = False
            .SecondaryColorGradient = False
            .LoadingCircleBack1 = Color.White
            .LoadingCircleBack2 = Color.White
            .LoadingCircleHot1 = Color.Black
            .LoadingCircleHot2 = Color.Black
            .LoadingCircleBackGradient = False
            .LoadingCircleHotGradient = False
            .Shadow_Enabled = False
            .Shadow_Color = Color.Black
            .Shadow_Blur = 5
            .Shadow_Opacity = 0.3
            .Shadow_OffsetX = 2
            .Shadow_OffsetY = 2
        End With

        With [CP].Cursor_EW
            .ArrowStyle = ArrowStyle.Classic
            .CircleStyle = CircleStyle.Classic
            .PrimaryColor1 = Color.White
            .PrimaryColor2 = Color.White
            .SecondaryColor1 = Color.Black
            .SecondaryColor2 = Color.Black
            .PrimaryColorGradient = False
            .SecondaryColorGradient = False
            .LoadingCircleBack1 = Color.White
            .LoadingCircleBack2 = Color.White
            .LoadingCircleHot1 = Color.Black
            .LoadingCircleHot2 = Color.Black
            .LoadingCircleBackGradient = False
            .LoadingCircleHotGradient = False
            .Shadow_Enabled = False
            .Shadow_Color = Color.Black
            .Shadow_Blur = 5
            .Shadow_Opacity = 0.3
            .Shadow_OffsetX = 2
            .Shadow_OffsetY = 2
        End With

        With [CP].Cursor_NESW
            .ArrowStyle = ArrowStyle.Classic
            .CircleStyle = CircleStyle.Classic
            .PrimaryColor1 = Color.White
            .PrimaryColor2 = Color.White
            .SecondaryColor1 = Color.Black
            .SecondaryColor2 = Color.Black
            .PrimaryColorGradient = False
            .SecondaryColorGradient = False
            .LoadingCircleBack1 = Color.White
            .LoadingCircleBack2 = Color.White
            .LoadingCircleHot1 = Color.Black
            .LoadingCircleHot2 = Color.Black
            .LoadingCircleBackGradient = False
            .LoadingCircleHotGradient = False
            .Shadow_Enabled = False
            .Shadow_Color = Color.Black
            .Shadow_Blur = 5
            .Shadow_Opacity = 0.3
            .Shadow_OffsetX = 2
            .Shadow_OffsetY = 2
        End With

        With [CP].Cursor_NWSE
            .ArrowStyle = ArrowStyle.Classic
            .CircleStyle = CircleStyle.Classic
            .PrimaryColor1 = Color.White
            .PrimaryColor2 = Color.White
            .SecondaryColor1 = Color.Black
            .SecondaryColor2 = Color.Black
            .PrimaryColorGradient = False
            .SecondaryColorGradient = False
            .LoadingCircleBack1 = Color.White
            .LoadingCircleBack2 = Color.White
            .LoadingCircleHot1 = Color.Black
            .LoadingCircleHot2 = Color.Black
            .LoadingCircleBackGradient = False
            .LoadingCircleHotGradient = False
            .Shadow_Enabled = False
            .Shadow_Color = Color.Black
            .Shadow_Blur = 5
            .Shadow_Opacity = 0.3
            .Shadow_OffsetX = 2
            .Shadow_OffsetY = 2
        End With

        With [CP].Cursor_Move
            .ArrowStyle = ArrowStyle.Classic
            .CircleStyle = CircleStyle.Classic
            .PrimaryColor1 = Color.White
            .PrimaryColor2 = Color.White
            .SecondaryColor1 = Color.Black
            .SecondaryColor2 = Color.Black
            .PrimaryColorGradient = False
            .SecondaryColorGradient = False
            .LoadingCircleBack1 = Color.White
            .LoadingCircleBack2 = Color.White
            .LoadingCircleHot1 = Color.Black
            .LoadingCircleHot2 = Color.Black
            .LoadingCircleBackGradient = False
            .LoadingCircleHotGradient = False
            .Shadow_Enabled = False
            .Shadow_Color = Color.Black
            .Shadow_Blur = 5
            .Shadow_Opacity = 0.3
            .Shadow_OffsetX = 2
            .Shadow_OffsetY = 2
        End With

        With [CP].Cursor_None
            .ArrowStyle = ArrowStyle.Classic
            .CircleStyle = CircleStyle.Classic
            .PrimaryColor1 = Color.Transparent
            .PrimaryColor2 = Color.Transparent
            .SecondaryColor1 = Color.Black
            .SecondaryColor2 = Color.Black
            .PrimaryColorGradient = False
            .SecondaryColorGradient = False
            .LoadingCircleBack1 = Color.White
            .LoadingCircleBack2 = Color.White
            .LoadingCircleHot1 = Color.Black
            .LoadingCircleHot2 = Color.Black
            .LoadingCircleBackGradient = False
            .LoadingCircleHotGradient = False
            .Shadow_Enabled = False
            .Shadow_Color = Color.Black
            .Shadow_Blur = 5
            .Shadow_Opacity = 0.3
            .Shadow_OffsetX = 2
            .Shadow_OffsetY = 2
        End With

        With [CP].Cursor_Arrow
            .ArrowStyle = ArrowStyle.Classic
            .CircleStyle = CircleStyle.Classic
            .PrimaryColor1 = Color.White
            .PrimaryColor2 = Color.White
            .SecondaryColor1 = Color.Black
            .SecondaryColor2 = Color.Black
            .PrimaryColorGradient = False
            .SecondaryColorGradient = False
            .LoadingCircleBack1 = Color.White
            .LoadingCircleBack2 = Color.White
            .LoadingCircleHot1 = Color.Black
            .LoadingCircleHot2 = Color.Black
            .LoadingCircleBackGradient = False
            .LoadingCircleHotGradient = False
            .Shadow_Enabled = False
            .Shadow_Color = Color.Black
            .Shadow_Blur = 5
            .Shadow_Opacity = 0.3
            .Shadow_OffsetX = 2
            .Shadow_OffsetY = 2
        End With

        With [CP].Cursor_Pen
            .ArrowStyle = ArrowStyle.Classic
            .CircleStyle = CircleStyle.Classic
            .PrimaryColor1 = Color.White
            .PrimaryColor2 = Color.White
            .SecondaryColor1 = Color.Black
            .SecondaryColor2 = Color.Black
            .PrimaryColorGradient = False
            .SecondaryColorGradient = False
            .LoadingCircleBack1 = Color.White
            .LoadingCircleBack2 = Color.White
            .LoadingCircleHot1 = Color.Black
            .LoadingCircleHot2 = Color.Black
            .LoadingCircleBackGradient = False
            .LoadingCircleHotGradient = False
            .Shadow_Enabled = False
            .Shadow_Color = Color.Black
            .Shadow_Blur = 5
            .Shadow_Opacity = 0.3
            .Shadow_OffsetX = 2
            .Shadow_OffsetY = 2
        End With

        With [CP].Cursor_IBeam
            .ArrowStyle = ArrowStyle.Classic
            .CircleStyle = CircleStyle.Classic
            .PrimaryColor1 = Color.White
            .PrimaryColor2 = Color.White
            .SecondaryColor1 = Color.Black
            .SecondaryColor2 = Color.Black
            .PrimaryColorGradient = False
            .SecondaryColorGradient = False
            .LoadingCircleBack1 = Color.White
            .LoadingCircleBack2 = Color.White
            .LoadingCircleHot1 = Color.Black
            .LoadingCircleHot2 = Color.Black
            .LoadingCircleBackGradient = False
            .LoadingCircleHotGradient = False
            .Shadow_Enabled = False
            .Shadow_Color = Color.Black
            .Shadow_Blur = 5
            .Shadow_Opacity = 0.3
            .Shadow_OffsetX = 2
            .Shadow_OffsetY = 2
        End With

        With [CP].Cursor_Cross
            .ArrowStyle = ArrowStyle.Classic
            .CircleStyle = CircleStyle.Classic
            .PrimaryColor1 = Color.White
            .PrimaryColor2 = Color.White
            .SecondaryColor1 = Color.Black
            .SecondaryColor2 = Color.Black
            .PrimaryColorGradient = False
            .SecondaryColorGradient = False
            .LoadingCircleBack1 = Color.White
            .LoadingCircleBack2 = Color.White
            .LoadingCircleHot1 = Color.Black
            .LoadingCircleHot2 = Color.Black
            .LoadingCircleBackGradient = False
            .LoadingCircleHotGradient = False
            .Shadow_Enabled = False
            .Shadow_Color = Color.Black
            .Shadow_Blur = 5
            .Shadow_Opacity = 0.3
            .Shadow_OffsetX = 2
            .Shadow_OffsetY = 2
        End With

        With [CP].Cursor_Link
            .ArrowStyle = ArrowStyle.Classic
            .CircleStyle = CircleStyle.Classic
            .PrimaryColor1 = Color.White
            .PrimaryColor2 = Color.White
            .SecondaryColor1 = Color.Black
            .SecondaryColor2 = Color.Black
            .PrimaryColorGradient = False
            .SecondaryColorGradient = False
            .LoadingCircleBack1 = Color.White
            .LoadingCircleBack2 = Color.White
            .LoadingCircleHot1 = Color.Black
            .LoadingCircleHot2 = Color.Black
            .LoadingCircleBackGradient = False
            .LoadingCircleHotGradient = False
            .Shadow_Enabled = False
            .Shadow_Color = Color.Black
            .Shadow_Blur = 5
            .Shadow_Opacity = 0.3
            .Shadow_OffsetX = 2
            .Shadow_OffsetY = 2
        End With

        With [CP].Cursor_Pin
            .ArrowStyle = ArrowStyle.Classic
            .CircleStyle = CircleStyle.Classic
            .PrimaryColor1 = Color.White
            .PrimaryColor2 = Color.White
            .SecondaryColor1 = Color.Black
            .SecondaryColor2 = Color.Black
            .PrimaryColorGradient = False
            .SecondaryColorGradient = False
            .LoadingCircleBack1 = Color.White
            .LoadingCircleBack2 = Color.White
            .LoadingCircleHot1 = Color.Black
            .LoadingCircleHot2 = Color.Black
            .LoadingCircleBackGradient = False
            .LoadingCircleHotGradient = False
            .Shadow_Enabled = False
            .Shadow_Color = Color.Black
            .Shadow_Blur = 5
            .Shadow_Opacity = 0.3
            .Shadow_OffsetX = 2
            .Shadow_OffsetY = 2
        End With

        With [CP].Cursor_Person
            .ArrowStyle = ArrowStyle.Classic
            .CircleStyle = CircleStyle.Classic
            .PrimaryColor1 = Color.White
            .PrimaryColor2 = Color.White
            .SecondaryColor1 = Color.Black
            .SecondaryColor2 = Color.Black
            .PrimaryColorGradient = False
            .SecondaryColorGradient = False
            .LoadingCircleBack1 = Color.White
            .LoadingCircleBack2 = Color.White
            .LoadingCircleHot1 = Color.Black
            .LoadingCircleHot2 = Color.Black
            .LoadingCircleBackGradient = False
            .LoadingCircleHotGradient = False
            .Shadow_Enabled = False
            .Shadow_Color = Color.Black
            .Shadow_Blur = 5
            .Shadow_Opacity = 0.3
            .Shadow_OffsetX = 2
            .Shadow_OffsetY = 2
        End With

        With [CP].WindowsEffects
            .ShakeToMinimize = False
            .BalloonNotifications = True
            .PaintDesktopVersion = False
            .ShowSecondsInSystemClock = False
            .Win11ClassicContextMenu = False
            .SysListView32 = True
        End With

        [CP].Terminal = New WinTerminal("", WinTerminal.Mode.Empty)
        [CP].TerminalPreview = New WinTerminal("", WinTerminal.Mode.Empty)
        [CP].Cursor_Shadow = True

        With [CP].ScreenSaver
            .Enabled = True
            .IsSecure = False
            .TimeOut = 60
            .File = My.PATH_System32 & "\logon.scr"
        End With

        With [CP].Sounds
            .Snd_Imageres_SystemStart = ""
            .Snd_Win_Default = My.PATH_Windows & "\media\Windows XP Ding.wav"
            .Snd_Win_AppGPFault = ""
            .Snd_Win_CCSelect = ""
            .Snd_Win_ChangeTheme = ""
            .Snd_Win_Close = ""
            .Snd_Win_CriticalBatteryAlarm = My.PATH_Windows & "\media\Windows XP Battery Critical.wav"
            .Snd_Win_DeviceConnect = My.PATH_Windows & "\media\Windows XP Hardware Insert.wav"
            .Snd_Win_DeviceDisconnect = My.PATH_Windows & "\media\Windows XP Hardware Remove.wav"
            .Snd_Win_DeviceFail = My.PATH_Windows & "\media\Windows XP Hardware Fail.wav"
            .Snd_Win_FaxBeep = ""
            .Snd_Win_LowBatteryAlarm = My.PATH_Windows & "\media\Windows XP Battery Low.wav"
            .Snd_Win_MailBeep = My.PATH_Windows & "\media\Windows XP Notify.wav"
            .Snd_Win_Maximize = ""
            .Snd_Win_MenuCommand = ""
            .Snd_Win_MenuPopup = ""
            .Snd_Win_MessageNudge = ""
            .Snd_Win_Minimize = ""
            .Snd_Win_Notification_Default = ""
            .Snd_Win_Notification_IM = ""
            .Snd_Win_Notification_Looping_Alarm = ""
            .Snd_Win_Notification_Looping_Alarm2 = ""
            .Snd_Win_Notification_Looping_Alarm3 = ""
            .Snd_Win_Notification_Looping_Alarm4 = ""
            .Snd_Win_Notification_Looping_Alarm5 = ""
            .Snd_Win_Notification_Looping_Alarm6 = ""
            .Snd_Win_Notification_Looping_Alarm7 = ""
            .Snd_Win_Notification_Looping_Alarm8 = ""
            .Snd_Win_Notification_Looping_Alarm9 = ""
            .Snd_Win_Notification_Looping_Alarm10 = ""
            .Snd_Win_Notification_Looping_Call = ""
            .Snd_Win_Notification_Looping_Call2 = ""
            .Snd_Win_Notification_Looping_Call3 = ""
            .Snd_Win_Notification_Looping_Call4 = ""
            .Snd_Win_Notification_Looping_Call5 = ""
            .Snd_Win_Notification_Looping_Call6 = ""
            .Snd_Win_Notification_Looping_Call7 = ""
            .Snd_Win_Notification_Looping_Call8 = ""
            .Snd_Win_Notification_Looping_Call9 = ""
            .Snd_Win_Notification_Looping_Call10 = ""
            .Snd_Win_Notification_Mail = ""
            .Snd_Win_Notification_Proximity = ""
            .Snd_Win_Notification_Reminder = ""
            .Snd_Win_Notification_SMS = ""
            .Snd_Win_Open = ""
            .Snd_Win_PrintComplete = ""
            .Snd_Win_ProximityConnection = ""
            .Snd_Win_RestoreDown = ""
            .Snd_Win_RestoreUp = ""
            .Snd_Win_ShowBand = ""
            .Snd_Win_SystemAsterisk = My.PATH_Windows & "\media\Windows XP Error.wav"
            .Snd_Win_SystemExclamation = My.PATH_Windows & "\media\Windows XP Exclamation.wav"
            .Snd_Win_SystemExit = My.PATH_Windows & "\media\Windows XP Shutdown.wav"
            .Snd_Win_SystemStart = My.PATH_Windows & "\media\Windows XP Startup.wav"
            .Snd_Win_SystemHand = My.PATH_Windows & "\media\Windows XP Critical Stop.wav"
            .Snd_Win_SystemNotification = My.PATH_Windows & "\media\Windows XP Balloon.wav"
            .Snd_Win_SystemQuestion = ""
            .Snd_Win_WindowsLogoff = My.PATH_Windows & "\media\Windows XP Logoff Sound.wav"
            .Snd_Win_WindowsLogon = My.PATH_Windows & "\media\Windows XP Logon Sound.wav"
            .Snd_Win_WindowsUAC = ""
            .Snd_Win_WindowsUnlock = ""
            .Snd_Explorer_ActivatingDocument = ""
            .Snd_Explorer_BlockedPopup = My.PATH_Windows & "\media\Windows Pop-up Blocked.wav"
            .Snd_Explorer_EmptyRecycleBin = My.PATH_Windows & "\media\Windows XP Recycle.wav"
            .Snd_Explorer_FeedDiscovered = My.PATH_Windows & "\media\Windows Feed Discovered.wav"
            .Snd_Explorer_MoveMenuItem = ""
            .Snd_Explorer_Navigating = My.PATH_Windows & "\media\Windows Navigation Start.wav"
            .Snd_Explorer_SecurityBand = My.PATH_Windows & "\media\Windows Information Bar.wav"
            .Snd_Explorer_SearchProviderDiscovered = ""
            .Snd_Explorer_FaxError = My.PATH_Windows & "\media\ding.wav"
            .Snd_Explorer_FaxLineRings = My.PATH_Windows & "\media\ringin.wav"
            .Snd_Explorer_FaxNew = My.PATH_Windows & "\media\notify.wav"
            .Snd_Explorer_FaxSent = My.PATH_Windows & "\media\tada.wav"
            .Snd_NetMeeting_PersonJoins = My.PATH_ProgramFiles & "\NetMeeting\Blip.wav"
            .Snd_NetMeeting_PersonLeaves = My.PATH_ProgramFiles & "\NetMeeting\Blip.wav"
            .Snd_NetMeeting_ReceiveCall = My.PATH_Windows & "\media\Windows XP RingIn.wav"
            .Snd_NetMeeting_ReceiveRequestToJoin = My.PATH_Windows & "\media\Windows XP RingIn.wav"
            .Snd_SpeechRec_DisNumbersSound = ""
            .Snd_SpeechRec_HubOffSound = ""
            .Snd_SpeechRec_HubOnSound = ""
            .Snd_SpeechRec_HubSleepSound = ""
            .Snd_SpeechRec_MisrecoSound = ""
            .Snd_SpeechRec_PanelSound = ""

            .Snd_Win_SystemExit_TaskMgmt = False
            .Snd_Win_WindowsLogoff_TaskMgmt = False
            .Snd_Win_WindowsLogon_TaskMgmt = False
            .Snd_Win_WindowsUnlock_TaskMgmt = False
        End With

        With [CP].Wallpaper
            .ImageFile = My.PATH_Windows & "\Web\Wallpaper\Bliss.bmp"
            .WallpaperStyle = CP.Structures.Wallpaper.WallpaperStyles.Stretched
        End With

        Return [CP]
    End Function

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

End Class