Imports System.Globalization
Imports System.Reflection
Imports System.Runtime.InteropServices
Imports System.Security.Principal
Imports Microsoft.Win32
Imports WinPaletter.XenonCore

Public Class CP

    <DllImport("user32.dll")>
    Private Shared Function SetSysColors(ByVal cElements As Integer, ByVal lpaElements As Integer(), ByVal lpaRgbValues As UInteger()) As Boolean
    End Function

    ReadOnly isElevated As Boolean = New WindowsPrincipal(WindowsIdentity.GetCurrent).IsInRole(WindowsBuiltInRole.Administrator)
#Region "General Info"
    Public Property AppVersion As String
    Public Property PaletteName As String
    Public Property PaletteDescription As String
    Public Property PaletteVersion As String
    Public Property Author As String
    Public Property AuthorSocialMediaLink As String
#End Region

#Region "ModernWindows"
    Public Property Titlebar_Active As Color
    Public Property Titlebar_DWM_Active As Color
    Public Property Titlebar_Inactive As Color
    Public Property StartMenu_Accent As Color
    Public Property StartButton_Hover As Color
    Public Property Taskbar_Background As Color
    Public Property Taskbar_Icon_Underline As Color
    Public Property StartMenuBackground_ActiveTaskbarButton As Color
    Public Property StartListFolders_TaskbarFront As Color
    Public Property ActionCenter_AppsLinks As Color
    Public Property SettingsIconsAndLinks As Color
    Public Property WinMode_Light As Boolean
    Public Property AppMode_Light As Boolean
    Public Property Transparency As Boolean
    Public Property ApplyAccentonTitlebars As Boolean
    Public Property ApplyAccentonTaskbar As Boolean
#End Region

#Region "LogonUI"
    Public Property LogonUI_Background As Color
    Public Property LogonUI_PersonalColors_Background As Color
    Public Property LogonUI_PersonalColors_Accent As Color
    Public Property LogonUI_DisableAcrylicBackgroundOnLogon As Boolean = False
    Public Property LogonUI_DisableLogonBackgroundImage As Boolean = False
    Public Property LogonUI_NoLockScreen As Boolean = False
#End Region

#Region "Win32UI"
    Public Property Win32UI_ActiveBorder As Color = Color.FromArgb(180, 180, 180)
    Public Property Win32UI_ActiveTitle As Color = Color.FromArgb(153, 180, 209)
    Public Property Win32UI_AppWorkspace As Color = Color.FromArgb(171, 171, 171)
    Public Property Win32UI_Background As Color = Color.FromArgb(0, 0, 0)
    Public Property Win32UI_ButtonAlternateFace As Color = Color.FromArgb(0, 0, 0)
    Public Property Win32UI_ButtonDkShadow As Color = Color.FromArgb(105, 105, 105)
    Public Property Win32UI_ButtonFace As Color = Color.FromArgb(240, 240, 240)
    Public Property Win32UI_ButtonHilight As Color = Color.FromArgb(255, 255, 255)
    Public Property Win32UI_ButtonLight As Color = Color.FromArgb(227, 227, 227)
    Public Property Win32UI_ButtonShadow As Color = Color.FromArgb(160, 160, 160)
    Public Property Win32UI_ButtonText As Color = Color.FromArgb(0, 0, 0)
    Public Property Win32UI_GradientActiveTitle As Color = Color.FromArgb(185, 209, 234)
    Public Property Win32UI_GradientInactiveTitle As Color = Color.FromArgb(215, 228, 242)
    Public Property Win32UI_GrayText As Color = Color.FromArgb(109, 109, 109)
    Public Property Win32UI_HilightText As Color = Color.FromArgb(255, 255, 255)
    Public Property Win32UI_HotTrackingColor As Color = Color.FromArgb(0, 102, 204)
    Public Property Win32UI_InactiveBorder As Color = Color.FromArgb(244, 247, 252)
    Public Property Win32UI_InactiveTitle As Color = Color.FromArgb(191, 205, 219)
    Public Property Win32UI_InactiveTitleText As Color = Color.FromArgb(0, 0, 0)
    Public Property Win32UI_InfoText As Color = Color.FromArgb(0, 0, 0)
    Public Property Win32UI_InfoWindow As Color = Color.FromArgb(255, 255, 225)
    Public Property Win32UI_Menu As Color = Color.FromArgb(240, 240, 240)
    Public Property Win32UI_MenuBar As Color = Color.FromArgb(240, 240, 240)
    Public Property Win32UI_MenuText As Color = Color.FromArgb(0, 0, 0)
    Public Property Win32UI_Scrollbar As Color = Color.FromArgb(200, 200, 200)
    Public Property Win32UI_TitleText As Color = Color.FromArgb(0, 0, 0)
    Public Property Win32UI_Window As Color = Color.FromArgb(255, 255, 255)
    Public Property Win32UI_WindowFrame As Color = Color.FromArgb(100, 100, 100)
    Public Property Win32UI_WindowText As Color = Color.FromArgb(0, 0, 0)
    Public Property Win32UI_Hilight As Color = Color.FromArgb(0, 120, 215)
    Public Property Win32UI_MenuHilight As Color = Color.FromArgb(0, 120, 215)
    Public Property Win32UI_Desktop As Color = Color.FromArgb(0, 0, 0)
#End Region

#Region "Cursors"
    Public Property Cursor_Enabled As Boolean

#Region "Arrow"
    Public Property Cursor_Arrow_PrimaryColor1 As Color
    Public Property Cursor_Arrow_PrimaryColor2 As Color
    Public Property Cursor_Arrow_PrimaryColorGradient As Boolean
    Public Property Cursor_Arrow_PrimaryColorGradientMode As GradientMode
    Public Property Cursor_Arrow_PrimaryColorNoise As Boolean
    Public Property Cursor_Arrow_PrimaryColorNoiseOpacity As Single
    Public Property Cursor_Arrow_SecondaryColor1 As Color
    Public Property Cursor_Arrow_SecondaryColor2 As Color
    Public Property Cursor_Arrow_SecondaryColorGradient As Boolean
    Public Property Cursor_Arrow_SecondaryColorGradientMode As GradientMode
    Public Property Cursor_Arrow_SecondaryColorNoise As Boolean
    Public Property Cursor_Arrow_SecondaryColorNoiseOpacity As Single
#End Region

#Region "Help"
    Public Property Cursor_Help_PrimaryColor1 As Color
    Public Property Cursor_Help_PrimaryColor2 As Color
    Public Property Cursor_Help_PrimaryColorGradient As Boolean
    Public Property Cursor_Help_PrimaryColorGradientMode As GradientMode
    Public Property Cursor_Help_PrimaryColorNoise As Boolean
    Public Property Cursor_Help_PrimaryColorNoiseOpacity As Single
    Public Property Cursor_Help_SecondaryColor1 As Color
    Public Property Cursor_Help_SecondaryColor2 As Color
    Public Property Cursor_Help_SecondaryColorGradient As Boolean
    Public Property Cursor_Help_SecondaryColorGradientMode As GradientMode
    Public Property Cursor_Help_SecondaryColorNoise As Boolean
    Public Property Cursor_Help_SecondaryColorNoiseOpacity As Single
#End Region

#Region "AppLoading"
    Public Property Cursor_AppLoading_PrimaryColor1 As Color
    Public Property Cursor_AppLoading_PrimaryColor2 As Color
    Public Property Cursor_AppLoading_PrimaryColorGradient As Boolean
    Public Property Cursor_AppLoading_PrimaryColorGradientMode As GradientMode
    Public Property Cursor_AppLoading_PrimaryColorNoise As Boolean
    Public Property Cursor_AppLoading_PrimaryColorNoiseOpacity As Single
    Public Property Cursor_AppLoading_SecondaryColor1 As Color
    Public Property Cursor_AppLoading_SecondaryColor2 As Color
    Public Property Cursor_AppLoading_SecondaryColorGradient As Boolean
    Public Property Cursor_AppLoading_SecondaryColorGradientMode As GradientMode
    Public Property Cursor_AppLoading_SecondaryColorNoise As Boolean
    Public Property Cursor_AppLoading_SecondaryColorNoiseOpacity As Single
    Public Property Cursor_AppLoading_LoadingCircleBack1 As Color
    Public Property Cursor_AppLoading_LoadingCircleBack2 As Color
    Public Property Cursor_AppLoading_LoadingCircleBackGradient As Boolean
    Public Property Cursor_AppLoading_LoadingCircleBackGradientMode As GradientMode
    Public Property Cursor_AppLoading_LoadingCircleBackNoise As Boolean
    Public Property Cursor_AppLoading_LoadingCircleBackNoiseOpacity As Single
    Public Property Cursor_AppLoading_LoadingCircleHot1 As Color
    Public Property Cursor_AppLoading_LoadingCircleHot2 As Color
    Public Property Cursor_AppLoading_LoadingCircleHotGradient As Boolean
    Public Property Cursor_AppLoading_LoadingCircleHotGradientMode As GradientMode
    Public Property Cursor_AppLoading_LoadingCircleHotNoise As Boolean
    Public Property Cursor_AppLoading_LoadingCircleHotNoiseOpacity As Single
#End Region

#Region "Busy"
    Public Property Cursor_Busy_LoadingCircleBack1 As Color
    Public Property Cursor_Busy_LoadingCircleBack2 As Color
    Public Property Cursor_Busy_LoadingCircleBackGradient As Boolean
    Public Property Cursor_Busy_LoadingCircleBackGradientMode As GradientMode
    Public Property Cursor_Busy_LoadingCircleBackNoise As Boolean
    Public Property Cursor_Busy_LoadingCircleBackNoiseOpacity As Single
    Public Property Cursor_Busy_LoadingCircleHot1 As Color
    Public Property Cursor_Busy_LoadingCircleHot2 As Color
    Public Property Cursor_Busy_LoadingCircleHotGradient As Boolean
    Public Property Cursor_Busy_LoadingCircleHotGradientMode As GradientMode
    Public Property Cursor_Busy_LoadingCircleHotNoise As Boolean
    Public Property Cursor_Busy_LoadingCircleHotNoiseOpacity As Single
#End Region

#Region "Move"
    Public Property Cursor_Move_PrimaryColor1 As Color
    Public Property Cursor_Move_PrimaryColor2 As Color
    Public Property Cursor_Move_PrimaryColorGradient As Boolean
    Public Property Cursor_Move_PrimaryColorGradientMode As GradientMode
    Public Property Cursor_Move_PrimaryColorNoise As Boolean
    Public Property Cursor_Move_PrimaryColorNoiseOpacity As Single
    Public Property Cursor_Move_SecondaryColor1 As Color
    Public Property Cursor_Move_SecondaryColor2 As Color
    Public Property Cursor_Move_SecondaryColorGradient As Boolean
    Public Property Cursor_Move_SecondaryColorGradientMode As GradientMode
    Public Property Cursor_Move_SecondaryColorNoise As Boolean
    Public Property Cursor_Move_SecondaryColorNoiseOpacity As Single
#End Region

#Region "NS"
    Public Property Cursor_NS_PrimaryColor1 As Color
    Public Property Cursor_NS_PrimaryColor2 As Color
    Public Property Cursor_NS_PrimaryColorGradient As Boolean
    Public Property Cursor_NS_PrimaryColorGradientMode As GradientMode
    Public Property Cursor_NS_PrimaryColorNoise As Boolean
    Public Property Cursor_NS_PrimaryColorNoiseOpacity As Single
    Public Property Cursor_NS_SecondaryColor1 As Color
    Public Property Cursor_NS_SecondaryColor2 As Color
    Public Property Cursor_NS_SecondaryColorGradient As Boolean
    Public Property Cursor_NS_SecondaryColorGradientMode As GradientMode
    Public Property Cursor_NS_SecondaryColorNoise As Boolean
    Public Property Cursor_NS_SecondaryColorNoiseOpacity As Single
#End Region

#Region "EW"
    Public Property Cursor_EW_PrimaryColor1 As Color
    Public Property Cursor_EW_PrimaryColor2 As Color
    Public Property Cursor_EW_PrimaryColorGradient As Boolean
    Public Property Cursor_EW_PrimaryColorGradientMode As GradientMode
    Public Property Cursor_EW_PrimaryColorNoise As Boolean
    Public Property Cursor_EW_PrimaryColorNoiseOpacity As Single
    Public Property Cursor_EW_SecondaryColor1 As Color
    Public Property Cursor_EW_SecondaryColor2 As Color
    Public Property Cursor_EW_SecondaryColorGradient As Boolean
    Public Property Cursor_EW_SecondaryColorGradientMode As GradientMode
    Public Property Cursor_EW_SecondaryColorNoise As Boolean
    Public Property Cursor_EW_SecondaryColorNoiseOpacity As Single
#End Region

#Region "NESW"
    Public Property Cursor_NESW_PrimaryColor1 As Color
    Public Property Cursor_NESW_PrimaryColor2 As Color
    Public Property Cursor_NESW_PrimaryColorGradient As Boolean
    Public Property Cursor_NESW_PrimaryColorGradientMode As GradientMode
    Public Property Cursor_NESW_PrimaryColorNoise As Boolean
    Public Property Cursor_NESW_PrimaryColorNoiseOpacity As Single
    Public Property Cursor_NESW_SecondaryColor1 As Color
    Public Property Cursor_NESW_SecondaryColor2 As Color
    Public Property Cursor_NESW_SecondaryColorGradient As Boolean
    Public Property Cursor_NESW_SecondaryColorGradientMode As GradientMode
    Public Property Cursor_NESW_SecondaryColorNoise As Boolean
    Public Property Cursor_NESW_SecondaryColorNoiseOpacity As Single
#End Region

#Region "NWSE"
    Public Property Cursor_NWSE_PrimaryColor1 As Color
    Public Property Cursor_NWSE_PrimaryColor2 As Color
    Public Property Cursor_NWSE_PrimaryColorGradient As Boolean
    Public Property Cursor_NWSE_PrimaryColorGradientMode As GradientMode
    Public Property Cursor_NWSE_PrimaryColorNoise As Boolean
    Public Property Cursor_NWSE_PrimaryColorNoiseOpacity As Single
    Public Property Cursor_NWSE_SecondaryColor1 As Color
    Public Property Cursor_NWSE_SecondaryColor2 As Color
    Public Property Cursor_NWSE_SecondaryColorGradient As Boolean
    Public Property Cursor_NWSE_SecondaryColorGradientMode As GradientMode
    Public Property Cursor_NWSE_SecondaryColorNoise As Boolean
    Public Property Cursor_NWSE_SecondaryColorNoiseOpacity As Single
#End Region

#Region "Up"
    Public Property Cursor_Up_PrimaryColor1 As Color
    Public Property Cursor_Up_PrimaryColor2 As Color
    Public Property Cursor_Up_PrimaryColorGradient As Boolean
    Public Property Cursor_Up_PrimaryColorGradientMode As GradientMode
    Public Property Cursor_Up_PrimaryColorNoise As Boolean
    Public Property Cursor_Up_PrimaryColorNoiseOpacity As Single
    Public Property Cursor_Up_SecondaryColor1 As Color
    Public Property Cursor_Up_SecondaryColor2 As Color
    Public Property Cursor_Up_SecondaryColorGradient As Boolean
    Public Property Cursor_Up_SecondaryColorGradientMode As GradientMode
    Public Property Cursor_Up_SecondaryColorNoise As Boolean
    Public Property Cursor_Up_SecondaryColorNoiseOpacity As Single
#End Region

#Region "Pen"
    Public Property Cursor_Pen_PrimaryColor1 As Color
    Public Property Cursor_Pen_PrimaryColor2 As Color
    Public Property Cursor_Pen_PrimaryColorGradient As Boolean
    Public Property Cursor_Pen_PrimaryColorGradientMode As GradientMode
    Public Property Cursor_Pen_PrimaryColorNoise As Boolean
    Public Property Cursor_Pen_PrimaryColorNoiseOpacity As Single
    Public Property Cursor_Pen_SecondaryColor1 As Color
    Public Property Cursor_Pen_SecondaryColor2 As Color
    Public Property Cursor_Pen_SecondaryColorGradient As Boolean
    Public Property Cursor_Pen_SecondaryColorGradientMode As GradientMode
    Public Property Cursor_Pen_SecondaryColorNoise As Boolean
    Public Property Cursor_Pen_SecondaryColorNoiseOpacity As Single
#End Region

#Region "None"
    Public Property Cursor_None_PrimaryColor1 As Color
    Public Property Cursor_None_PrimaryColor2 As Color
    Public Property Cursor_None_PrimaryColorGradient As Boolean
    Public Property Cursor_None_PrimaryColorGradientMode As GradientMode
    Public Property Cursor_None_PrimaryColorNoise As Boolean
    Public Property Cursor_None_PrimaryColorNoiseOpacity As Single
    Public Property Cursor_None_SecondaryColor1 As Color = Color.Red
    Public Property Cursor_None_SecondaryColor2 As Color = Color.Red
    Public Property Cursor_None_SecondaryColorGradient As Boolean
    Public Property Cursor_None_SecondaryColorGradientMode As GradientMode
    Public Property Cursor_None_SecondaryColorNoise As Boolean
    Public Property Cursor_None_SecondaryColorNoiseOpacity As Single
#End Region

#Region "Link"
    Public Property Cursor_Link_PrimaryColor1 As Color
    Public Property Cursor_Link_PrimaryColor2 As Color
    Public Property Cursor_Link_PrimaryColorGradient As Boolean
    Public Property Cursor_Link_PrimaryColorGradientMode As GradientMode
    Public Property Cursor_Link_PrimaryColorNoise As Boolean
    Public Property Cursor_Link_PrimaryColorNoiseOpacity As Single
    Public Property Cursor_Link_SecondaryColor1 As Color
    Public Property Cursor_Link_SecondaryColor2 As Color
    Public Property Cursor_Link_SecondaryColorGradient As Boolean
    Public Property Cursor_Link_SecondaryColorGradientMode As GradientMode
    Public Property Cursor_Link_SecondaryColorNoise As Boolean
    Public Property Cursor_Link_SecondaryColorNoiseOpacity As Single
#End Region

#Region "Pin"
    Public Property Cursor_Pin_PrimaryColor1 As Color
    Public Property Cursor_Pin_PrimaryColor2 As Color
    Public Property Cursor_Pin_PrimaryColorGradient As Boolean
    Public Property Cursor_Pin_PrimaryColorGradientMode As GradientMode
    Public Property Cursor_Pin_PrimaryColorNoise As Boolean
    Public Property Cursor_Pin_PrimaryColorNoiseOpacity As Single
    Public Property Cursor_Pin_SecondaryColor1 As Color
    Public Property Cursor_Pin_SecondaryColor2 As Color
    Public Property Cursor_Pin_SecondaryColorGradient As Boolean
    Public Property Cursor_Pin_SecondaryColorGradientMode As GradientMode
    Public Property Cursor_Pin_SecondaryColorNoise As Boolean
    Public Property Cursor_Pin_SecondaryColorNoiseOpacity As Single
#End Region

#Region "Person"
    Public Property Cursor_Person_PrimaryColor1 As Color
    Public Property Cursor_Person_PrimaryColor2 As Color
    Public Property Cursor_Person_PrimaryColorGradient As Boolean
    Public Property Cursor_Person_PrimaryColorGradientMode As GradientMode
    Public Property Cursor_Person_PrimaryColorNoise As Boolean
    Public Property Cursor_Person_PrimaryColorNoiseOpacity As Single
    Public Property Cursor_Person_SecondaryColor1 As Color
    Public Property Cursor_Person_SecondaryColor2 As Color
    Public Property Cursor_Person_SecondaryColorGradient As Boolean
    Public Property Cursor_Person_SecondaryColorGradientMode As GradientMode
    Public Property Cursor_Person_SecondaryColorNoise As Boolean
    Public Property Cursor_Person_SecondaryColorNoiseOpacity As Single
#End Region

#Region "Cross"
    Public Property Cursor_Cross_PrimaryColor1 As Color
    Public Property Cursor_Cross_PrimaryColor2 As Color
    Public Property Cursor_Cross_PrimaryColorGradient As Boolean
    Public Property Cursor_Cross_PrimaryColorGradientMode As GradientMode
    Public Property Cursor_Cross_PrimaryColorNoise As Boolean
    Public Property Cursor_Cross_PrimaryColorNoiseOpacity As Single
    Public Property Cursor_Cross_SecondaryColor1 As Color
    Public Property Cursor_Cross_SecondaryColor2 As Color
    Public Property Cursor_Cross_SecondaryColorGradient As Boolean
    Public Property Cursor_Cross_SecondaryColorGradientMode As GradientMode
    Public Property Cursor_Cross_SecondaryColorNoise As Boolean
    Public Property Cursor_Cross_SecondaryColorNoiseOpacity As Single
#End Region
#End Region

    Enum Mode
        Registry
        Init
        File
    End Enum

    Sub New([Mode] As Mode, Optional ByVal PaletteFile As String = "")
        Select Case [Mode]
            Case Mode.Registry
                Dim Colors As New List(Of Color)
                Colors.Clear()

                Author = Environment.UserName
                AppVersion = My.Application.Info.Version.ToString
                PaletteVersion = "1.0"
                PaletteName = My.Application.LanguageHelper.CurrentMode

                Dim x As Byte() = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "AccentPalette", Nothing)
                Colors.Add(Color.FromArgb(255, x(0), x(1), x(2)))
                Colors.Add(Color.FromArgb(255, x(4), x(5), x(6)))
                Colors.Add(Color.FromArgb(255, x(8), x(9), x(10)))
                Colors.Add(Color.FromArgb(255, x(12), x(13), x(14)))
                Colors.Add(Color.FromArgb(255, x(16), x(17), x(18)))
                Colors.Add(Color.FromArgb(255, x(20), x(21), x(22)))
                Colors.Add(Color.FromArgb(255, x(24), x(25), x(26)))
                Colors.Add(Color.FromArgb(255, x(28), x(29), x(30)))

                ActionCenter_AppsLinks = Colors(0)
                Taskbar_Icon_Underline = Colors(1)
                StartButton_Hover = Colors(2)
                SettingsIconsAndLinks = Colors(3)
                StartMenuBackground_ActiveTaskbarButton = Colors(4)
                StartListFolders_TaskbarFront = Colors(5)
                Taskbar_Background = Colors(6)

                Dim y As Integer
                y = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "StartColorMenu", Nothing)
                StartMenu_Accent = BizareColorInvertor(Color.FromArgb(y))

                y = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "AccentColorMenu", Nothing)
                Titlebar_Active = BizareColorInvertor(Color.FromArgb(y))

                y = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\DWM", "AccentColor", Nothing)
                Titlebar_DWM_Active = BizareColorInvertor(Color.FromArgb(y))

                y = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\DWM", "AccentColorInactive", Nothing)
                Titlebar_Inactive = BizareColorInvertor(Color.FromArgb(y))

                WinMode_Light = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "SystemUsesLightTheme", True)
                AppMode_Light = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "AppsUseLightTheme", True)
                Transparency = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "EnableTransparency", True)
                ApplyAccentonTaskbar = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "ColorPrevalence", False)
                ApplyAccentonTitlebars = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\DWM", "ColorPrevalence", False)

#Region "LogonUI"
                With My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon", "Background", "0 0 0")
                    LogonUI_Background = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                End With

                y = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Windows\Personalization", "PersonalColors_Background", Nothing)
                LogonUI_PersonalColors_Background = BizareColorInvertor(Color.FromArgb(y))

                y = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Windows\Personalization", "PersonalColors_Accent", Nothing)
                LogonUI_PersonalColors_Accent = BizareColorInvertor(Color.FromArgb(y))

                LogonUI_DisableAcrylicBackgroundOnLogon = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\System", "DisableAcrylicBackgroundOnLogon", False)
                LogonUI_DisableLogonBackgroundImage = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\System", "DisableLogonBackgroundImage", False)
                LogonUI_NoLockScreen = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Windows\Personalization", "NoLockScreen", False)
#End Region

#Region "Win32UI"
                With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "ActiveBorder", "180 180 180")
                    Win32UI_ActiveBorder = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                End With

                With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "ActiveTitle", "153 180 209")
                    Win32UI_ActiveTitle = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                End With

                With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "AppWorkspace", "171 171 171")
                    Win32UI_AppWorkspace = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                End With

                With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "Background", "0 0 0")
                    Win32UI_Background = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                End With

                With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "ButtonAlternateFace", "0 0 0")
                    Win32UI_ButtonAlternateFace = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                End With

                With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "ButtonDkShadow", "105 105 105")
                    Win32UI_ButtonDkShadow = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                End With

                With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "ButtonFace", "240 240 240")
                    Win32UI_ButtonFace = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                End With

                With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "ButtonHilight", "255 255 255")
                    Win32UI_ButtonHilight = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                End With

                With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "ButtonLight", "227 227 227")
                    Win32UI_ButtonLight = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                End With

                With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "ButtonShadow", "160 160 160")
                    Win32UI_ButtonShadow = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                End With

                With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "ButtonText", "0 0 0")
                    Win32UI_ButtonText = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                End With

                With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "GradientActiveTitle", "185 209 234")
                    Win32UI_GradientActiveTitle = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                End With

                With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "GradientInactiveTitle", "215 228 242")
                    Win32UI_GradientInactiveTitle = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                End With

                With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "GrayText", "109 109 109")
                    Win32UI_GrayText = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                End With

                With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "HilightText", "255 255 255")
                    Win32UI_HilightText = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                End With

                With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "HotTrackingColor", "0 102 204")
                    Win32UI_HotTrackingColor = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                End With

                With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "InactiveBorder", "244 247 252")
                    Win32UI_InactiveBorder = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                End With

                With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "InactiveTitle", "191 205 219")
                    Win32UI_InactiveTitle = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                End With

                With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "InactiveTitleText", "0 0 0")
                    Win32UI_InactiveTitleText = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                End With

                With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "InfoText", "0 0 0")
                    Win32UI_InfoText = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                End With

                With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "InfoWindow", "255 255 225")
                    Win32UI_InfoWindow = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                End With

                With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "Menu", "240 240 240")
                    Win32UI_Menu = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                End With

                With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "MenuBar", "240 240 240")
                    Win32UI_MenuBar = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                End With

                With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "MenuText", "0 0 0")
                    Win32UI_MenuText = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                End With

                With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "Scrollbar", "200 200 200")
                    Win32UI_Scrollbar = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                End With

                With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "TitleText", "0 0 0")
                    Win32UI_TitleText = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                End With

                With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "Window", "255 255 255")
                    Win32UI_Window = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                End With

                With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "WindowFrame", "100 100 100")
                    Win32UI_WindowFrame = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                End With

                With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "WindowText", "0 0 0")
                    Win32UI_WindowText = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                End With

                With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "Hilight", "0 120 215")
                    Win32UI_Hilight = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                End With

                With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "MenuHilight", "0 120 215")
                    Win32UI_MenuHilight = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                End With

                With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "Desktop", "0 0 0")
                    Win32UI_Desktop = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                End With
#End Region

#Region "Cursors"
                Dim rMain As RegistryKey = Registry.CurrentUser.CreateSubKey("Software\WinPaletter\Cursors")
                Cursor_Enabled = rMain.GetValue("", False)

                Dim r As RegistryKey

                r = rMain.OpenSubKey("Arrow")
#Region "Arrow"
                Cursor_Arrow_PrimaryColor1 = Color.White
                Cursor_Arrow_PrimaryColor2 = Color.White
                Cursor_Arrow_PrimaryColorGradient = False
                Cursor_Arrow_PrimaryColorGradientMode = GradientMode.Vertical
                Cursor_Arrow_PrimaryColorNoise = False
                Cursor_Arrow_PrimaryColorNoiseOpacity = 0.25
                Cursor_Arrow_SecondaryColor1 = Color.FromArgb(64, 65, 75)
                Cursor_Arrow_SecondaryColor2 = Color.FromArgb(64, 65, 75)
                Cursor_Arrow_SecondaryColorGradient = False
                Cursor_Arrow_SecondaryColorGradientMode = GradientMode.Vertical
                Cursor_Arrow_SecondaryColorNoise = False
                Cursor_Arrow_SecondaryColorNoiseOpacity = 0.25
#End Region

#Region "Help"
                Cursor_Help_PrimaryColor1 = Color.White
                Cursor_Help_PrimaryColor2 = Color.White
                Cursor_Help_PrimaryColorGradient = False
                Cursor_Help_PrimaryColorGradientMode = GradientMode.Vertical
                Cursor_Help_PrimaryColorNoise = False
                Cursor_Help_PrimaryColorNoiseOpacity = 0.25
                Cursor_Help_SecondaryColor1 = Color.FromArgb(64, 65, 75)
                Cursor_Help_SecondaryColor2 = Color.FromArgb(64, 65, 75)
                Cursor_Help_SecondaryColorGradient = False
                Cursor_Help_SecondaryColorGradientMode = GradientMode.Vertical
                Cursor_Help_SecondaryColorNoise = False
                Cursor_Help_SecondaryColorNoiseOpacity = 0.25
#End Region

#Region "AppLoading"
                Cursor_AppLoading_PrimaryColor1 = Color.White
                Cursor_AppLoading_PrimaryColor2 = Color.White
                Cursor_AppLoading_PrimaryColorGradient = False
                Cursor_AppLoading_PrimaryColorGradientMode = GradientMode.Circle
                Cursor_AppLoading_PrimaryColorNoise = False
                Cursor_AppLoading_PrimaryColorNoiseOpacity = 0.25
                Cursor_AppLoading_SecondaryColor1 = Color.FromArgb(64, 65, 75)
                Cursor_AppLoading_SecondaryColor2 = Color.FromArgb(64, 65, 75)
                Cursor_AppLoading_SecondaryColorGradient = False
                Cursor_AppLoading_SecondaryColorGradientMode = GradientMode.Circle
                Cursor_AppLoading_SecondaryColorNoise = False
                Cursor_AppLoading_SecondaryColorNoiseOpacity = 0.25
                Cursor_AppLoading_LoadingCircleBack1 = Color.FromArgb(42, 151, 243)
                Cursor_AppLoading_LoadingCircleBack2 = Color.FromArgb(42, 151, 243)
                Cursor_AppLoading_LoadingCircleBackGradient = False
                Cursor_AppLoading_LoadingCircleBackGradientMode = GradientMode.Circle
                Cursor_AppLoading_LoadingCircleBackNoise = False
                Cursor_AppLoading_LoadingCircleBackNoiseOpacity = 0.25
                Cursor_AppLoading_LoadingCircleHot1 = Color.FromArgb(37, 204, 255)
                Cursor_AppLoading_LoadingCircleHot2 = Color.FromArgb(37, 204, 255)
                Cursor_AppLoading_LoadingCircleHotGradient = False
                Cursor_AppLoading_LoadingCircleHotGradientMode = GradientMode.Circle
                Cursor_AppLoading_LoadingCircleHotNoise = False
                Cursor_AppLoading_LoadingCircleHotNoiseOpacity = 0.25
#End Region

#Region "Busy"
                Cursor_Busy_LoadingCircleBack1 = Color.FromArgb(42, 151, 243)
                Cursor_Busy_LoadingCircleBack2 = Color.FromArgb(42, 151, 243)
                Cursor_Busy_LoadingCircleBackGradient = False
                Cursor_Busy_LoadingCircleBackGradientMode = GradientMode.Circle
                Cursor_Busy_LoadingCircleBackNoise = False
                Cursor_Busy_LoadingCircleBackNoiseOpacity = 0.25
                Cursor_Busy_LoadingCircleHot1 = Color.FromArgb(37, 204, 255)
                Cursor_Busy_LoadingCircleHot2 = Color.FromArgb(37, 204, 255)
                Cursor_Busy_LoadingCircleHotGradient = False
                Cursor_Busy_LoadingCircleHotGradientMode = GradientMode.Circle
                Cursor_Busy_LoadingCircleHotNoise = False
                Cursor_Busy_LoadingCircleHotNoiseOpacity = 0.25
#End Region

#Region "Move"
                Cursor_Move_PrimaryColor1 = Color.White
                Cursor_Move_PrimaryColor2 = Color.White
                Cursor_Move_PrimaryColorGradient = False
                Cursor_Move_PrimaryColorGradientMode = GradientMode.Vertical
                Cursor_Move_PrimaryColorNoise = False
                Cursor_Move_PrimaryColorNoiseOpacity = 0.25
                Cursor_Move_SecondaryColor1 = Color.FromArgb(64, 65, 75)
                Cursor_Move_SecondaryColor2 = Color.FromArgb(64, 65, 75)
                Cursor_Move_SecondaryColorGradient = False
                Cursor_Move_SecondaryColorGradientMode = GradientMode.Vertical
                Cursor_Move_SecondaryColorNoise = False
                Cursor_Move_SecondaryColorNoiseOpacity = 0.25
#End Region

#Region "NS"
                Cursor_NS_PrimaryColor1 = Color.White
                Cursor_NS_PrimaryColor2 = Color.White
                Cursor_NS_PrimaryColorGradient = False
                Cursor_NS_PrimaryColorGradientMode = GradientMode.Vertical
                Cursor_NS_PrimaryColorNoise = False
                Cursor_NS_PrimaryColorNoiseOpacity = 0.25
                Cursor_NS_SecondaryColor1 = Color.FromArgb(64, 65, 75)
                Cursor_NS_SecondaryColor2 = Color.FromArgb(64, 65, 75)
                Cursor_NS_SecondaryColorGradient = False
                Cursor_NS_SecondaryColorGradientMode = GradientMode.Vertical
                Cursor_NS_SecondaryColorNoise = False
                Cursor_NS_SecondaryColorNoiseOpacity = 0.25
#End Region

#Region "EW"
                Cursor_EW_PrimaryColor1 = Color.White
                Cursor_EW_PrimaryColor2 = Color.White
                Cursor_EW_PrimaryColorGradient = False
                Cursor_EW_PrimaryColorGradientMode = GradientMode.Vertical
                Cursor_EW_PrimaryColorNoise = False
                Cursor_EW_PrimaryColorNoiseOpacity = 0.25
                Cursor_EW_SecondaryColor1 = Color.FromArgb(64, 65, 75)
                Cursor_EW_SecondaryColor2 = Color.FromArgb(64, 65, 75)
                Cursor_EW_SecondaryColorGradient = False
                Cursor_EW_SecondaryColorGradientMode = GradientMode.Vertical
                Cursor_EW_SecondaryColorNoise = False
                Cursor_EW_SecondaryColorNoiseOpacity = 0.25
#End Region

#Region "NESW"
                Cursor_NESW_PrimaryColor1 = Color.White
                Cursor_NESW_PrimaryColor2 = Color.White
                Cursor_NESW_PrimaryColorGradient = False
                Cursor_NESW_PrimaryColorGradientMode = GradientMode.Vertical
                Cursor_NESW_PrimaryColorNoise = False
                Cursor_NESW_PrimaryColorNoiseOpacity = 0.25
                Cursor_NESW_SecondaryColor1 = Color.FromArgb(64, 65, 75)
                Cursor_NESW_SecondaryColor2 = Color.FromArgb(64, 65, 75)
                Cursor_NESW_SecondaryColorGradient = False
                Cursor_NESW_SecondaryColorGradientMode = GradientMode.Vertical
                Cursor_NESW_SecondaryColorNoise = False
                Cursor_NESW_SecondaryColorNoiseOpacity = 0.25
#End Region

#Region "NWSE"
                Cursor_NWSE_PrimaryColor1 = Color.White
                Cursor_NWSE_PrimaryColor2 = Color.White
                Cursor_NWSE_PrimaryColorGradient = False
                Cursor_NWSE_PrimaryColorGradientMode = GradientMode.Vertical
                Cursor_NWSE_PrimaryColorNoise = False
                Cursor_NWSE_PrimaryColorNoiseOpacity = 0.25
                Cursor_NWSE_SecondaryColor1 = Color.FromArgb(64, 65, 75)
                Cursor_NWSE_SecondaryColor2 = Color.FromArgb(64, 65, 75)
                Cursor_NWSE_SecondaryColorGradient = False
                Cursor_NWSE_SecondaryColorGradientMode = GradientMode.Vertical
                Cursor_NWSE_SecondaryColorNoise = False
                Cursor_NWSE_SecondaryColorNoiseOpacity = 0.25
#End Region

#Region "Up"
                Cursor_Up_PrimaryColor1 = Color.White
                Cursor_Up_PrimaryColor2 = Color.White
                Cursor_Up_PrimaryColorGradient = False
                Cursor_Up_PrimaryColorGradientMode = GradientMode.Vertical
                Cursor_Up_PrimaryColorNoise = False
                Cursor_Up_PrimaryColorNoiseOpacity = 0.25
                Cursor_Up_SecondaryColor1 = Color.FromArgb(64, 65, 75)
                Cursor_Up_SecondaryColor2 = Color.FromArgb(64, 65, 75)
                Cursor_Up_SecondaryColorGradient = False
                Cursor_Up_SecondaryColorGradientMode = GradientMode.Vertical
                Cursor_Up_SecondaryColorNoise = False
                Cursor_Up_SecondaryColorNoiseOpacity = 0.25
#End Region

#Region "Pen"
                Cursor_Pen_PrimaryColor1 = Color.White
                Cursor_Pen_PrimaryColor2 = Color.White
                Cursor_Pen_PrimaryColorGradient = False
                Cursor_Pen_PrimaryColorGradientMode = GradientMode.Vertical
                Cursor_Pen_PrimaryColorNoise = False
                Cursor_Pen_PrimaryColorNoiseOpacity = 0.25
                Cursor_Pen_SecondaryColor1 = Color.FromArgb(64, 65, 75)
                Cursor_Pen_SecondaryColor2 = Color.FromArgb(64, 65, 75)
                Cursor_Pen_SecondaryColorGradient = False
                Cursor_Pen_SecondaryColorGradientMode = GradientMode.Vertical
                Cursor_Pen_SecondaryColorNoise = False
                Cursor_Pen_SecondaryColorNoiseOpacity = 0.25
#End Region

#Region "None"
                Cursor_None_PrimaryColor1 = Color.White
                Cursor_None_PrimaryColor2 = Color.White
                Cursor_None_PrimaryColorGradient = False
                Cursor_None_PrimaryColorGradientMode = GradientMode.Vertical
                Cursor_None_PrimaryColorNoise = False
                Cursor_None_PrimaryColorNoiseOpacity = 0.25
                Cursor_None_SecondaryColor1 = Color.FromArgb(64, 65, 75)
                Cursor_None_SecondaryColor2 = Color.FromArgb(64, 65, 75)
                Cursor_None_SecondaryColorGradient = False
                Cursor_None_SecondaryColorGradientMode = GradientMode.Vertical
                Cursor_None_SecondaryColorNoise = False
                Cursor_None_SecondaryColorNoiseOpacity = 0.25
#End Region

#Region "Link"
                Cursor_Link_PrimaryColor1 = Color.White
                Cursor_Link_PrimaryColor2 = Color.White
                Cursor_Link_PrimaryColorGradient = False
                Cursor_Link_PrimaryColorGradientMode = GradientMode.Vertical
                Cursor_Link_PrimaryColorNoise = False
                Cursor_Link_PrimaryColorNoiseOpacity = 0.25
                Cursor_Link_SecondaryColor1 = Color.FromArgb(64, 65, 75)
                Cursor_Link_SecondaryColor2 = Color.FromArgb(64, 65, 75)
                Cursor_Link_SecondaryColorGradient = False
                Cursor_Link_SecondaryColorGradientMode = GradientMode.Vertical
                Cursor_Link_SecondaryColorNoise = False
                Cursor_Link_SecondaryColorNoiseOpacity = 0.25
#End Region

#Region "Pin"
                Cursor_Pin_PrimaryColor1 = Color.White
                Cursor_Pin_PrimaryColor2 = Color.White
                Cursor_Pin_PrimaryColorGradient = False
                Cursor_Pin_PrimaryColorGradientMode = GradientMode.Vertical
                Cursor_Pin_PrimaryColorNoise = False
                Cursor_Pin_PrimaryColorNoiseOpacity = 0.25
                Cursor_Pin_SecondaryColor1 = Color.FromArgb(64, 65, 75)
                Cursor_Pin_SecondaryColor2 = Color.FromArgb(64, 65, 75)
                Cursor_Pin_SecondaryColorGradient = False
                Cursor_Pin_SecondaryColorGradientMode = GradientMode.Vertical
                Cursor_Pin_SecondaryColorNoise = False
                Cursor_Pin_SecondaryColorNoiseOpacity = 0.25
#End Region

#Region "Person"
                Cursor_Person_PrimaryColor1 = Color.White
                Cursor_Person_PrimaryColor2 = Color.White
                Cursor_Person_PrimaryColorGradient = False
                Cursor_Person_PrimaryColorGradientMode = GradientMode.Vertical
                Cursor_Person_PrimaryColorNoise = False
                Cursor_Person_PrimaryColorNoiseOpacity = 0.25
                Cursor_Person_SecondaryColor1 = Color.FromArgb(64, 65, 75)
                Cursor_Person_SecondaryColor2 = Color.FromArgb(64, 65, 75)
                Cursor_Person_SecondaryColorGradient = False
                Cursor_Person_SecondaryColorGradientMode = GradientMode.Vertical
                Cursor_Person_SecondaryColorNoise = False
                Cursor_Person_SecondaryColorNoiseOpacity = 0.25
#End Region

#Region "Cross"
                Cursor_Cross_PrimaryColor1 = Color.Transparent
                Cursor_Cross_PrimaryColor2 = Color.Transparent
                Cursor_Cross_PrimaryColorGradient = False
                Cursor_Cross_PrimaryColorGradientMode = GradientMode.Vertical
                Cursor_Cross_PrimaryColorNoise = False
                Cursor_Cross_PrimaryColorNoiseOpacity = 0.25
                Cursor_Cross_SecondaryColor1 = Color.FromArgb(64, 65, 75)
                Cursor_Cross_SecondaryColor2 = Color.FromArgb(64, 65, 75)
                Cursor_Cross_SecondaryColorGradient = False
                Cursor_Cross_SecondaryColorGradientMode = GradientMode.Vertical
                Cursor_Cross_SecondaryColorNoise = False
                Cursor_Cross_SecondaryColorNoiseOpacity = 0.25
#End Region
#End Region

            Case Mode.File
                Dim txt As New List(Of String)
                txt.Clear()
                CList_FromStr(txt, IO.File.ReadAllText(PaletteFile))

                For Each lin As String In txt
                    If lin.StartsWith("*Created from App Version= ") Then AppVersion = lin.Remove(0, "*Created from App Version= ".Count)
                    If lin.StartsWith("*Palette Name= ") Then PaletteName = lin.Remove(0, "*Palette Name= ".Count)
                    If lin.StartsWith("*Palette Description= ") Then PaletteDescription = lin.Remove(0, "*Palette Description= ".Count).Replace("<br>", vbCrLf)
                    If lin.StartsWith("*Palette File Version= ") Then PaletteVersion = lin.Remove(0, "*Palette File Version= ".Count)
                    If lin.StartsWith("*Author= ") Then Author = lin.Remove(0, "*Author= ".Count)
                    If lin.StartsWith("*AuthorSocialMediaLink= ") Then AuthorSocialMediaLink = lin.Remove(0, "*AuthorSocialMediaLink= ".Count)
                    If lin.StartsWith("*Palette File Version= ") Then PaletteVersion = lin.Remove(0, "*Palette File Version= ".Count)
                    If lin.StartsWith("*WinMode_Light= ") Then WinMode_Light = lin.Remove(0, "*WinMode_Light= ".Count)
                    If lin.StartsWith("*AppMode_Light= ") Then AppMode_Light = lin.Remove(0, "*AppMode_Light= ".Count)
                    If lin.StartsWith("*Transparency= ") Then Transparency = lin.Remove(0, "*Transparency= ".Count)
                    If lin.StartsWith("*AccentColorOnTitlebarAndBorders= ") Then ApplyAccentonTitlebars = lin.Remove(0, "*AccentColorOnTitlebarAndBorders= ".Count)
                    If lin.StartsWith("*AccentColorOnStartTaskbarAndActionCenter= ") Then ApplyAccentonTaskbar = lin.Remove(0, "*AccentColorOnStartTaskbarAndActionCenter= ".Count)
                    If lin.StartsWith("*Titlebar_Active= ") Then
                        Titlebar_Active = Color.FromArgb(lin.Remove(0, "*Titlebar_Active= ".Count))
                        Titlebar_DWM_Active = Titlebar_Active
                    End If
                    If lin.StartsWith("*Titlebar_Inactive= ") Then Titlebar_Inactive = Color.FromArgb(lin.Remove(0, "*Titlebar_Inactive= ".Count))
                    If lin.StartsWith("*ActionCenter_AppsLinks= ") Then ActionCenter_AppsLinks = Color.FromArgb(lin.Remove(0, "*ActionCenter_AppsLinks= ".Count))
                    If lin.StartsWith("*Taskbar_Icon_Underline= ") Then Taskbar_Icon_Underline = Color.FromArgb(lin.Remove(0, "*Taskbar_Icon_Underline= ".Count))
                    If lin.StartsWith("*StartButton_Hover= ") Then StartButton_Hover = Color.FromArgb(lin.Remove(0, "*StartButton_Hover= ".Count))
                    If lin.StartsWith("*SettingsIconsAndLinks= ") Then SettingsIconsAndLinks = Color.FromArgb(lin.Remove(0, "*SettingsIconsAndLinks= ".Count))
                    If lin.StartsWith("*StartMenuBackground_ActiveTaskbarButton= ") Then StartMenuBackground_ActiveTaskbarButton = Color.FromArgb(lin.Remove(0, "*StartMenuBackground_ActiveTaskbarButton= ".Count))
                    If lin.StartsWith("*StartListFolders_TaskbarFront= ") Then StartListFolders_TaskbarFront = Color.FromArgb(lin.Remove(0, "*StartListFolders_TaskbarFront= ".Count))
                    If lin.StartsWith("*Taskbar_Background= ") Then Taskbar_Background = Color.FromArgb(lin.Remove(0, "*Taskbar_Background= ".Count))
                    If lin.StartsWith("*StartMenu_Accent= ") Then StartMenu_Accent = Color.FromArgb(lin.Remove(0, "*StartMenu_Accent= ".Count))

#Region "LogonUI"
                    If lin.StartsWith("*LogonUI_Background= ") Then LogonUI_Background = Color.FromArgb(lin.Remove(0, "*LogonUI_Background= ".Count))
                    If lin.StartsWith("*LogonUI_PersonalColors_Background= ") Then LogonUI_PersonalColors_Background = Color.FromArgb(lin.Remove(0, "*LogonUI_PersonalColors_Background= ".Count))
                    If lin.StartsWith("*LogonUI_PersonalColors_Accent= ") Then LogonUI_PersonalColors_Accent = Color.FromArgb(lin.Remove(0, "*LogonUI_PersonalColors_Accent= ".Count))
                    If lin.StartsWith("*LogonUI_DisableAcrylicBackgroundOnLogon= ") Then LogonUI_DisableAcrylicBackgroundOnLogon = lin.Remove(0, "*LogonUI_DisableAcrylicBackgroundOnLogon= ".Count)
                    If lin.StartsWith("*LogonUI_DisableLogonBackgroundImage= ") Then LogonUI_DisableLogonBackgroundImage = lin.Remove(0, "*LogonUI_DisableLogonBackgroundImage= ".Count)
                    If lin.StartsWith("*LogonUI_NoLockScreen= ") Then LogonUI_NoLockScreen = lin.Remove(0, "*LogonUI_NoLockScreen= ".Count)
#End Region

#Region "Win32UI"
                    If lin.StartsWith("*Win32UI_ActiveBorder= ") Then Win32UI_ActiveBorder = Color.FromArgb(lin.Remove(0, "*Win32UI_ActiveBorder= ".Count))
                    If lin.StartsWith("*Win32UI_ActiveTitle= ") Then Win32UI_ActiveTitle = Color.FromArgb(lin.Remove(0, "*Win32UI_ActiveTitle= ".Count))
                    If lin.StartsWith("*Win32UI_AppWorkspace= ") Then Win32UI_AppWorkspace = Color.FromArgb(lin.Remove(0, "*Win32UI_AppWorkspace= ".Count))
                    If lin.StartsWith("*Win32UI_Background= ") Then Win32UI_Background = Color.FromArgb(lin.Remove(0, "*Win32UI_Background= ".Count))
                    If lin.StartsWith("*Win32UI_ButtonAlternateFace= ") Then Win32UI_ButtonAlternateFace = Color.FromArgb(lin.Remove(0, "*Win32UI_ButtonAlternateFace= ".Count))
                    If lin.StartsWith("*Win32UI_ButtonDkShadow= ") Then Win32UI_ButtonDkShadow = Color.FromArgb(lin.Remove(0, "*Win32UI_ButtonDkShadow= ".Count))
                    If lin.StartsWith("*Win32UI_ButtonFace= ") Then Win32UI_ButtonFace = Color.FromArgb(lin.Remove(0, "*Win32UI_ButtonFace= ".Count))
                    If lin.StartsWith("*Win32UI_ButtonHilight= ") Then Win32UI_ButtonHilight = Color.FromArgb(lin.Remove(0, "*Win32UI_ButtonHilight= ".Count))
                    If lin.StartsWith("*Win32UI_ButtonLight= ") Then Win32UI_ButtonLight = Color.FromArgb(lin.Remove(0, "*Win32UI_ButtonLight= ".Count))
                    If lin.StartsWith("*Win32UI_ButtonShadow= ") Then Win32UI_ButtonShadow = Color.FromArgb(lin.Remove(0, "*Win32UI_ButtonShadow= ".Count))
                    If lin.StartsWith("*Win32UI_ButtonText= ") Then Win32UI_ButtonText = Color.FromArgb(lin.Remove(0, "*Win32UI_ButtonText= ".Count))
                    If lin.StartsWith("*Win32UI_GradientActiveTitle= ") Then Win32UI_GradientActiveTitle = Color.FromArgb(lin.Remove(0, "*Win32UI_GradientActiveTitle= ".Count))
                    If lin.StartsWith("*Win32UI_GradientInactiveTitle= ") Then Win32UI_GradientInactiveTitle = Color.FromArgb(lin.Remove(0, "*Win32UI_GradientInactiveTitle= ".Count))
                    If lin.StartsWith("*Win32UI_GrayText= ") Then Win32UI_GrayText = Color.FromArgb(lin.Remove(0, "*Win32UI_GrayText= ".Count))
                    If lin.StartsWith("*Win32UI_HilightText= ") Then Win32UI_HilightText = Color.FromArgb(lin.Remove(0, "*Win32UI_HilightText= ".Count))
                    If lin.StartsWith("*Win32UI_HotTrackingColor= ") Then Win32UI_HotTrackingColor = Color.FromArgb(lin.Remove(0, "*Win32UI_HotTrackingColor= ".Count))
                    If lin.StartsWith("*Win32UI_InactiveBorder= ") Then Win32UI_InactiveBorder = Color.FromArgb(lin.Remove(0, "*Win32UI_InactiveBorder= ".Count))
                    If lin.StartsWith("*Win32UI_InactiveTitle= ") Then Win32UI_InactiveTitle = Color.FromArgb(lin.Remove(0, "*Win32UI_InactiveTitle= ".Count))
                    If lin.StartsWith("*Win32UI_InactiveTitleText= ") Then Win32UI_InactiveTitleText = Color.FromArgb(lin.Remove(0, "*Win32UI_InactiveTitleText= ".Count))
                    If lin.StartsWith("*Win32UI_InfoText= ") Then Win32UI_InfoText = Color.FromArgb(lin.Remove(0, "*Win32UI_InfoText= ".Count))
                    If lin.StartsWith("*Win32UI_InfoWindow= ") Then Win32UI_InfoWindow = Color.FromArgb(lin.Remove(0, "*Win32UI_InfoWindow= ".Count))
                    If lin.StartsWith("*Win32UI_Menu= ") Then Win32UI_Menu = Color.FromArgb(lin.Remove(0, "*Win32UI_Menu= ".Count))
                    If lin.StartsWith("*Win32UI_MenuBar= ") Then Win32UI_MenuBar = Color.FromArgb(lin.Remove(0, "*Win32UI_MenuBar= ".Count))
                    If lin.StartsWith("*Win32UI_MenuText= ") Then Win32UI_MenuText = Color.FromArgb(lin.Remove(0, "*Win32UI_MenuText= ".Count))
                    If lin.StartsWith("*Win32UI_Scrollbar= ") Then Win32UI_Scrollbar = Color.FromArgb(lin.Remove(0, "*Win32UI_Scrollbar= ".Count))
                    If lin.StartsWith("*Win32UI_TitleText= ") Then Win32UI_TitleText = Color.FromArgb(lin.Remove(0, "*Win32UI_TitleText= ".Count))
                    If lin.StartsWith("*Win32UI_Window= ") Then Win32UI_Window = Color.FromArgb(lin.Remove(0, "*Win32UI_Window= ".Count))
                    If lin.StartsWith("*Win32UI_WindowFrame= ") Then Win32UI_WindowFrame = Color.FromArgb(lin.Remove(0, "*Win32UI_WindowFrame= ".Count))
                    If lin.StartsWith("*Win32UI_WindowText= ") Then Win32UI_WindowText = Color.FromArgb(lin.Remove(0, "*Win32UI_WindowText= ".Count))
                    If lin.StartsWith("*Win32UI_Hilight= ") Then Win32UI_Hilight = Color.FromArgb(lin.Remove(0, "*Win32UI_Hilight= ".Count))
                    If lin.StartsWith("*Win32UI_MenuHilight= ") Then Win32UI_MenuHilight = Color.FromArgb(lin.Remove(0, "*Win32UI_MenuHilight= ".Count))
                    If lin.StartsWith("*Win32UI_Desktop= ") Then Win32UI_Desktop = Color.FromArgb(lin.Remove(0, "*Win32UI_Desktop= ".Count))
#End Region

#Region "Cursors"
                    If lin.StartsWith("*Cursor_Enabled= ") Then Cursor_Enabled = lin.Remove(0, "*Cursor_Enabled= ".Count)

#Region "Arrow"
                    If lin.StartsWith("*Cursor_Arrow_PrimaryColor1= ") Then Cursor_Arrow_PrimaryColor1 = Color.FromArgb(lin.Remove(0, "*Cursor_Arrow_PrimaryColor1= ".Count))
                    If lin.StartsWith("*Cursor_Arrow_PrimaryColor2= ") Then Cursor_Arrow_PrimaryColor2 = Color.FromArgb(lin.Remove(0, "*Cursor_Arrow_PrimaryColor2= ".Count))
                    If lin.StartsWith("*Cursor_Arrow_PrimaryColorGradient= ") Then Cursor_Arrow_PrimaryColorGradient = lin.Remove(0, "*Cursor_Arrow_PrimaryColorGradient= ".Count)
                    If lin.StartsWith("*Cursor_Arrow_PrimaryColorGradientMode= ") Then Cursor_Arrow_PrimaryColorGradientMode = lin.Remove(0, "*Cursor_Arrow_PrimaryColorGradientMode= ".Count)
                    If lin.StartsWith("*Cursor_Arrow_PrimaryColorNoise= ") Then Cursor_Arrow_PrimaryColorNoise = lin.Remove(0, "*Cursor_Arrow_PrimaryColorNoise= ".Count)
                    If lin.StartsWith("*Cursor_Arrow_PrimaryColorNoiseOpacity= ") Then Cursor_Arrow_PrimaryColorNoiseOpacity = lin.Remove(0, "*Cursor_Arrow_PrimaryColorNoiseOpacity= ".Count)
                    If lin.StartsWith("*Cursor_Arrow_SecondaryColor1= ") Then Cursor_Arrow_SecondaryColor1 = Color.FromArgb(lin.Remove(0, "*Cursor_Arrow_SecondaryColor1= ".Count))
                    If lin.StartsWith("*Cursor_Arrow_SecondaryColor2= ") Then Cursor_Arrow_SecondaryColor2 = Color.FromArgb(lin.Remove(0, "*Cursor_Arrow_SecondaryColor2= ".Count))
                    If lin.StartsWith("*Cursor_Arrow_SecondaryColorGradient= ") Then Cursor_Arrow_SecondaryColorGradient = lin.Remove(0, "*Cursor_Arrow_SecondaryColorGradient= ".Count)
                    If lin.StartsWith("*Cursor_Arrow_SecondaryColorGradientMode= ") Then Cursor_Arrow_SecondaryColorGradientMode = lin.Remove(0, "*Cursor_Arrow_SecondaryColorGradientMode= ".Count)
                    If lin.StartsWith("*Cursor_Arrow_SecondaryColorNoise= ") Then Cursor_Arrow_SecondaryColorNoise = lin.Remove(0, "*Cursor_Arrow_SecondaryColorNoise= ".Count)
                    If lin.StartsWith("*Cursor_Arrow_SecondaryColorNoiseOpacity= ") Then Cursor_Arrow_SecondaryColorNoiseOpacity = lin.Remove(0, "*Cursor_Arrow_SecondaryColorNoiseOpacity= ".Count)


#End Region

#Region "Help"
                    If lin.StartsWith("*Cursor_Help_PrimaryColor1= ") Then Cursor_Help_PrimaryColor1 = Color.FromArgb(lin.Remove(0, "*Cursor_Help_PrimaryColor1= ".Count))
                    If lin.StartsWith("*Cursor_Help_PrimaryColor2= ") Then Cursor_Help_PrimaryColor2 = Color.FromArgb(lin.Remove(0, "*Cursor_Help_PrimaryColor2= ".Count))
                    If lin.StartsWith("*Cursor_Help_PrimaryColorGradient= ") Then Cursor_Help_PrimaryColorGradient = lin.Remove(0, "*Cursor_Help_PrimaryColorGradient= ".Count)
                    If lin.StartsWith("*Cursor_Help_PrimaryColorGradientMode= ") Then Cursor_Help_PrimaryColorGradientMode = lin.Remove(0, "*Cursor_Help_PrimaryColorGradientMode= ".Count)
                    If lin.StartsWith("*Cursor_Help_PrimaryColorNoise= ") Then Cursor_Help_PrimaryColorNoise = lin.Remove(0, "*Cursor_Help_PrimaryColorNoise= ".Count)
                    If lin.StartsWith("*Cursor_Help_PrimaryColorNoiseOpacity= ") Then Cursor_Help_PrimaryColorNoiseOpacity = lin.Remove(0, "*Cursor_Help_PrimaryColorNoiseOpacity= ".Count)
                    If lin.StartsWith("*Cursor_Help_SecondaryColor1= ") Then Cursor_Help_SecondaryColor1 = Color.FromArgb(lin.Remove(0, "*Cursor_Help_SecondaryColor1= ".Count))
                    If lin.StartsWith("*Cursor_Help_SecondaryColor2= ") Then Cursor_Help_SecondaryColor2 = Color.FromArgb(lin.Remove(0, "*Cursor_Help_SecondaryColor2= ".Count))
                    If lin.StartsWith("*Cursor_Help_SecondaryColorGradient= ") Then Cursor_Help_SecondaryColorGradient = lin.Remove(0, "*Cursor_Help_SecondaryColorGradient= ".Count)
                    If lin.StartsWith("*Cursor_Help_SecondaryColorGradientMode= ") Then Cursor_Help_SecondaryColorGradientMode = lin.Remove(0, "*Cursor_Help_SecondaryColorGradientMode= ".Count)
                    If lin.StartsWith("*Cursor_Help_SecondaryColorNoise= ") Then Cursor_Help_SecondaryColorNoise = lin.Remove(0, "*Cursor_Help_SecondaryColorNoise= ".Count)
                    If lin.StartsWith("*Cursor_Help_SecondaryColorNoiseOpacity= ") Then Cursor_Help_SecondaryColorNoiseOpacity = lin.Remove(0, "*Cursor_Help_SecondaryColorNoiseOpacity= ".Count)

#End Region

#Region "AppLoading"
                    If lin.StartsWith("*Cursor_AppLoading_PrimaryColor1= ") Then Cursor_AppLoading_PrimaryColor1 = Color.FromArgb(lin.Remove(0, "*Cursor_AppLoading_PrimaryColor1= ".Count))
                    If lin.StartsWith("*Cursor_AppLoading_PrimaryColor2= ") Then Cursor_AppLoading_PrimaryColor2 = Color.FromArgb(lin.Remove(0, "*Cursor_AppLoading_PrimaryColor2= ".Count))
                    If lin.StartsWith("*Cursor_AppLoading_PrimaryColorGradient= ") Then Cursor_AppLoading_PrimaryColorGradient = lin.Remove(0, "*Cursor_AppLoading_PrimaryColorGradient= ".Count)
                    If lin.StartsWith("*Cursor_AppLoading_PrimaryColorGradientMode= ") Then Cursor_AppLoading_PrimaryColorGradientMode = lin.Remove(0, "*Cursor_AppLoading_PrimaryColorGradientMode= ".Count)
                    If lin.StartsWith("*Cursor_AppLoading_PrimaryColorNoise= ") Then Cursor_AppLoading_PrimaryColorNoise = lin.Remove(0, "*Cursor_AppLoading_PrimaryColorNoise= ".Count)
                    If lin.StartsWith("*Cursor_AppLoading_PrimaryColorNoiseOpacity= ") Then Cursor_AppLoading_PrimaryColorNoiseOpacity = lin.Remove(0, "*Cursor_AppLoading_PrimaryColorNoiseOpacity= ".Count)
                    If lin.StartsWith("*Cursor_AppLoading_SecondaryColor1= ") Then Cursor_AppLoading_SecondaryColor1 = Color.FromArgb(lin.Remove(0, "*Cursor_AppLoading_SecondaryColor1= ".Count))
                    If lin.StartsWith("*Cursor_AppLoading_SecondaryColor2= ") Then Cursor_AppLoading_SecondaryColor2 = Color.FromArgb(lin.Remove(0, "*Cursor_AppLoading_SecondaryColor2= ".Count))
                    If lin.StartsWith("*Cursor_AppLoading_SecondaryColorGradient= ") Then Cursor_AppLoading_SecondaryColorGradient = lin.Remove(0, "*Cursor_AppLoading_SecondaryColorGradient= ".Count)
                    If lin.StartsWith("*Cursor_AppLoading_SecondaryColorGradientMode= ") Then Cursor_AppLoading_SecondaryColorGradientMode = lin.Remove(0, "*Cursor_AppLoading_SecondaryColorGradientMode= ".Count)
                    If lin.StartsWith("*Cursor_AppLoading_SecondaryColorNoise= ") Then Cursor_AppLoading_SecondaryColorNoise = lin.Remove(0, "*Cursor_AppLoading_SecondaryColorNoise= ".Count)
                    If lin.StartsWith("*Cursor_AppLoading_SecondaryColorNoiseOpacity= ") Then Cursor_AppLoading_SecondaryColorNoiseOpacity = lin.Remove(0, "*Cursor_AppLoading_SecondaryColorNoiseOpacity= ".Count)
                    If lin.StartsWith("*Cursor_AppLoading_LoadingCircleBack1= ") Then Cursor_AppLoading_LoadingCircleBack1 = Color.FromArgb(lin.Remove(0, "*Cursor_AppLoading_LoadingCircleBack1= ".Count))
                    If lin.StartsWith("*Cursor_AppLoading_LoadingCircleBack2= ") Then Cursor_AppLoading_LoadingCircleBack2 = Color.FromArgb(lin.Remove(0, "*Cursor_AppLoading_LoadingCircleBack2= ".Count))
                    If lin.StartsWith("*Cursor_AppLoading_LoadingCircleBackGradient= ") Then Cursor_AppLoading_LoadingCircleBackGradient = lin.Remove(0, "*Cursor_AppLoading_LoadingCircleBackGradient= ".Count)
                    If lin.StartsWith("*Cursor_AppLoading_LoadingCircleBackGradientMode= ") Then Cursor_AppLoading_LoadingCircleBackGradientMode = lin.Remove(0, "*Cursor_AppLoading_LoadingCircleBackGradientMode= ".Count)
                    If lin.StartsWith("*Cursor_AppLoading_LoadingCircleBackNoise= ") Then Cursor_AppLoading_LoadingCircleBackNoise = lin.Remove(0, "*Cursor_AppLoading_LoadingCircleBackNoise= ".Count)
                    If lin.StartsWith("*Cursor_AppLoading_LoadingCircleBackNoiseOpacity= ") Then Cursor_AppLoading_LoadingCircleBackNoiseOpacity = lin.Remove(0, "*Cursor_AppLoading_LoadingCircleBackNoiseOpacity= ".Count)
                    If lin.StartsWith("*Cursor_AppLoading_LoadingCircleHot1= ") Then Cursor_AppLoading_LoadingCircleHot1 = Color.FromArgb(lin.Remove(0, "*Cursor_AppLoading_LoadingCircleHot1= ".Count))
                    If lin.StartsWith("*Cursor_AppLoading_LoadingCircleHot2= ") Then Cursor_AppLoading_LoadingCircleHot2 = Color.FromArgb(lin.Remove(0, "*Cursor_AppLoading_LoadingCircleHot2= ".Count))
                    If lin.StartsWith("*Cursor_AppLoading_LoadingCircleHotGradient= ") Then Cursor_AppLoading_LoadingCircleHotGradient = lin.Remove(0, "*Cursor_AppLoading_LoadingCircleHotGradient= ".Count)
                    If lin.StartsWith("*Cursor_AppLoading_LoadingCircleHotGradientMode= ") Then Cursor_AppLoading_LoadingCircleHotGradientMode = lin.Remove(0, "*Cursor_AppLoading_LoadingCircleHotGradientMode= ".Count)
                    If lin.StartsWith("*Cursor_AppLoading_LoadingCircleHotNoise= ") Then Cursor_AppLoading_LoadingCircleHotNoise = lin.Remove(0, "*Cursor_AppLoading_LoadingCircleHotNoise= ".Count)
                    If lin.StartsWith("*Cursor_AppLoading_LoadingCircleHotNoiseOpacity= ") Then Cursor_AppLoading_LoadingCircleHotNoiseOpacity = lin.Remove(0, "*Cursor_AppLoading_LoadingCircleHotNoiseOpacity= ".Count)

#End Region

#Region "Busy"
                    If lin.StartsWith("*Cursor_Busy_LoadingCircleBack1= ") Then Cursor_Busy_LoadingCircleBack1 = Color.FromArgb(lin.Remove(0, "*Cursor_Busy_LoadingCircleBack1= ".Count))
                    If lin.StartsWith("*Cursor_Busy_LoadingCircleBack2= ") Then Cursor_Busy_LoadingCircleBack2 = Color.FromArgb(lin.Remove(0, "*Cursor_Busy_LoadingCircleBack2= ".Count))
                    If lin.StartsWith("*Cursor_Busy_LoadingCircleBackGradient= ") Then Cursor_Busy_LoadingCircleBackGradient = lin.Remove(0, "*Cursor_Busy_LoadingCircleBackGradient= ".Count)
                    If lin.StartsWith("*Cursor_Busy_LoadingCircleBackGradientMode= ") Then Cursor_Busy_LoadingCircleBackGradientMode = lin.Remove(0, "*Cursor_Busy_LoadingCircleBackGradientMode= ".Count)
                    If lin.StartsWith("*Cursor_Busy_LoadingCircleBackNoise= ") Then Cursor_Busy_LoadingCircleBackNoise = lin.Remove(0, "*Cursor_Busy_LoadingCircleBackNoise= ".Count)
                    If lin.StartsWith("*Cursor_Busy_LoadingCircleBackNoiseOpacity= ") Then Cursor_Busy_LoadingCircleBackNoiseOpacity = lin.Remove(0, "*Cursor_Busy_LoadingCircleBackNoiseOpacity= ".Count)
                    If lin.StartsWith("*Cursor_Busy_LoadingCircleHot1= ") Then Cursor_Busy_LoadingCircleHot1 = Color.FromArgb(lin.Remove(0, "*Cursor_Busy_LoadingCircleHot1= ".Count))
                    If lin.StartsWith("*Cursor_Busy_LoadingCircleHot2= ") Then Cursor_Busy_LoadingCircleHot2 = Color.FromArgb(lin.Remove(0, "*Cursor_Busy_LoadingCircleHot2= ".Count))
                    If lin.StartsWith("*Cursor_Busy_LoadingCircleHotGradient= ") Then Cursor_Busy_LoadingCircleHotGradient = lin.Remove(0, "*Cursor_Busy_LoadingCircleHotGradient= ".Count)
                    If lin.StartsWith("*Cursor_Busy_LoadingCircleHotGradientMode= ") Then Cursor_Busy_LoadingCircleHotGradientMode = lin.Remove(0, "*Cursor_Busy_LoadingCircleHotGradientMode= ".Count)
                    If lin.StartsWith("*Cursor_Busy_LoadingCircleHotNoise= ") Then Cursor_Busy_LoadingCircleHotNoise = lin.Remove(0, "*Cursor_Busy_LoadingCircleHotNoise= ".Count)
                    If lin.StartsWith("*Cursor_Busy_LoadingCircleHotNoiseOpacity= ") Then Cursor_Busy_LoadingCircleHotNoiseOpacity = lin.Remove(0, "*Cursor_Busy_LoadingCircleHotNoiseOpacity= ".Count)

#End Region

#Region "Move"
                    If lin.StartsWith("*Cursor_Move_PrimaryColor1= ") Then Cursor_Move_PrimaryColor1 = Color.FromArgb(lin.Remove(0, "*Cursor_Move_PrimaryColor1= ".Count))
                    If lin.StartsWith("*Cursor_Move_PrimaryColor2= ") Then Cursor_Move_PrimaryColor2 = Color.FromArgb(lin.Remove(0, "*Cursor_Move_PrimaryColor2= ".Count))
                    If lin.StartsWith("*Cursor_Move_PrimaryColorGradient= ") Then Cursor_Move_PrimaryColorGradient = lin.Remove(0, "*Cursor_Move_PrimaryColorGradient= ".Count)
                    If lin.StartsWith("*Cursor_Move_PrimaryColorGradientMode= ") Then Cursor_Move_PrimaryColorGradientMode = lin.Remove(0, "*Cursor_Move_PrimaryColorGradientMode= ".Count)
                    If lin.StartsWith("*Cursor_Move_PrimaryColorNoise= ") Then Cursor_Move_PrimaryColorNoise = lin.Remove(0, "*Cursor_Move_PrimaryColorNoise= ".Count)
                    If lin.StartsWith("*Cursor_Move_PrimaryColorNoiseOpacity= ") Then Cursor_Move_PrimaryColorNoiseOpacity = lin.Remove(0, "*Cursor_Move_PrimaryColorNoiseOpacity= ".Count)
                    If lin.StartsWith("*Cursor_Move_SecondaryColor1= ") Then Cursor_Move_SecondaryColor1 = Color.FromArgb(lin.Remove(0, "*Cursor_Move_SecondaryColor1= ".Count))
                    If lin.StartsWith("*Cursor_Move_SecondaryColor2= ") Then Cursor_Move_SecondaryColor2 = Color.FromArgb(lin.Remove(0, "*Cursor_Move_SecondaryColor2= ".Count))
                    If lin.StartsWith("*Cursor_Move_SecondaryColorGradient= ") Then Cursor_Move_SecondaryColorGradient = lin.Remove(0, "*Cursor_Move_SecondaryColorGradient= ".Count)
                    If lin.StartsWith("*Cursor_Move_SecondaryColorGradientMode= ") Then Cursor_Move_SecondaryColorGradientMode = lin.Remove(0, "*Cursor_Move_SecondaryColorGradientMode= ".Count)
                    If lin.StartsWith("*Cursor_Move_SecondaryColorNoise= ") Then Cursor_Move_SecondaryColorNoise = lin.Remove(0, "*Cursor_Move_SecondaryColorNoise= ".Count)
                    If lin.StartsWith("*Cursor_Move_SecondaryColorNoiseOpacity= ") Then Cursor_Move_SecondaryColorNoiseOpacity = lin.Remove(0, "*Cursor_Move_SecondaryColorNoiseOpacity= ".Count)

#End Region

#Region "NS"
                    If lin.StartsWith("*Cursor_NS_PrimaryColor1= ") Then Cursor_NS_PrimaryColor1 = Color.FromArgb(lin.Remove(0, "*Cursor_NS_PrimaryColor1= ".Count))
                    If lin.StartsWith("*Cursor_NS_PrimaryColor2= ") Then Cursor_NS_PrimaryColor2 = Color.FromArgb(lin.Remove(0, "*Cursor_NS_PrimaryColor2= ".Count))
                    If lin.StartsWith("*Cursor_NS_PrimaryColorGradient= ") Then Cursor_NS_PrimaryColorGradient = lin.Remove(0, "*Cursor_NS_PrimaryColorGradient= ".Count)
                    If lin.StartsWith("*Cursor_NS_PrimaryColorGradientMode= ") Then Cursor_NS_PrimaryColorGradientMode = lin.Remove(0, "*Cursor_NS_PrimaryColorGradientMode= ".Count)
                    If lin.StartsWith("*Cursor_NS_PrimaryColorNoise= ") Then Cursor_NS_PrimaryColorNoise = lin.Remove(0, "*Cursor_NS_PrimaryColorNoise= ".Count)
                    If lin.StartsWith("*Cursor_NS_PrimaryColorNoiseOpacity= ") Then Cursor_NS_PrimaryColorNoiseOpacity = lin.Remove(0, "*Cursor_NS_PrimaryColorNoiseOpacity= ".Count)
                    If lin.StartsWith("*Cursor_NS_SecondaryColor1= ") Then Cursor_NS_SecondaryColor1 = Color.FromArgb(lin.Remove(0, "*Cursor_NS_SecondaryColor1= ".Count))
                    If lin.StartsWith("*Cursor_NS_SecondaryColor2= ") Then Cursor_NS_SecondaryColor2 = Color.FromArgb(lin.Remove(0, "*Cursor_NS_SecondaryColor2= ".Count))
                    If lin.StartsWith("*Cursor_NS_SecondaryColorGradient= ") Then Cursor_NS_SecondaryColorGradient = lin.Remove(0, "*Cursor_NS_SecondaryColorGradient= ".Count)
                    If lin.StartsWith("*Cursor_NS_SecondaryColorGradientMode= ") Then Cursor_NS_SecondaryColorGradientMode = lin.Remove(0, "*Cursor_NS_SecondaryColorGradientMode= ".Count)
                    If lin.StartsWith("*Cursor_NS_SecondaryColorNoise= ") Then Cursor_NS_SecondaryColorNoise = lin.Remove(0, "*Cursor_NS_SecondaryColorNoise= ".Count)
                    If lin.StartsWith("*Cursor_NS_SecondaryColorNoiseOpacity= ") Then Cursor_NS_SecondaryColorNoiseOpacity = lin.Remove(0, "*Cursor_NS_SecondaryColorNoiseOpacity= ".Count)

#End Region

#Region "EW"
                    If lin.StartsWith("*Cursor_EW_PrimaryColor1= ") Then Cursor_EW_PrimaryColor1 = Color.FromArgb(lin.Remove(0, "*Cursor_EW_PrimaryColor1= ".Count))
                    If lin.StartsWith("*Cursor_EW_PrimaryColor2= ") Then Cursor_EW_PrimaryColor2 = Color.FromArgb(lin.Remove(0, "*Cursor_EW_PrimaryColor2= ".Count))
                    If lin.StartsWith("*Cursor_EW_PrimaryColorGradient= ") Then Cursor_EW_PrimaryColorGradient = lin.Remove(0, "*Cursor_EW_PrimaryColorGradient= ".Count)
                    If lin.StartsWith("*Cursor_EW_PrimaryColorGradientMode= ") Then Cursor_EW_PrimaryColorGradientMode = lin.Remove(0, "*Cursor_EW_PrimaryColorGradientMode= ".Count)
                    If lin.StartsWith("*Cursor_EW_PrimaryColorNoise= ") Then Cursor_EW_PrimaryColorNoise = lin.Remove(0, "*Cursor_EW_PrimaryColorNoise= ".Count)
                    If lin.StartsWith("*Cursor_EW_PrimaryColorNoiseOpacity= ") Then Cursor_EW_PrimaryColorNoiseOpacity = lin.Remove(0, "*Cursor_EW_PrimaryColorNoiseOpacity= ".Count)
                    If lin.StartsWith("*Cursor_EW_SecondaryColor1= ") Then Cursor_EW_SecondaryColor1 = Color.FromArgb(lin.Remove(0, "*Cursor_EW_SecondaryColor1= ".Count))
                    If lin.StartsWith("*Cursor_EW_SecondaryColor2= ") Then Cursor_EW_SecondaryColor2 = Color.FromArgb(lin.Remove(0, "*Cursor_EW_SecondaryColor2= ".Count))
                    If lin.StartsWith("*Cursor_EW_SecondaryColorGradient= ") Then Cursor_EW_SecondaryColorGradient = lin.Remove(0, "*Cursor_EW_SecondaryColorGradient= ".Count)
                    If lin.StartsWith("*Cursor_EW_SecondaryColorGradientMode= ") Then Cursor_EW_SecondaryColorGradientMode = lin.Remove(0, "*Cursor_EW_SecondaryColorGradientMode= ".Count)
                    If lin.StartsWith("*Cursor_EW_SecondaryColorNoise= ") Then Cursor_EW_SecondaryColorNoise = lin.Remove(0, "*Cursor_EW_SecondaryColorNoise= ".Count)
                    If lin.StartsWith("*Cursor_EW_SecondaryColorNoiseOpacity= ") Then Cursor_EW_SecondaryColorNoiseOpacity = lin.Remove(0, "*Cursor_EW_SecondaryColorNoiseOpacity= ".Count)

#End Region

#Region "NESW"
                    If lin.StartsWith("*Cursor_NESW_PrimaryColor1= ") Then Cursor_NESW_PrimaryColor1 = Color.FromArgb(lin.Remove(0, "*Cursor_NESW_PrimaryColor1= ".Count))
                    If lin.StartsWith("*Cursor_NESW_PrimaryColor2= ") Then Cursor_NESW_PrimaryColor2 = Color.FromArgb(lin.Remove(0, "*Cursor_NESW_PrimaryColor2= ".Count))
                    If lin.StartsWith("*Cursor_NESW_PrimaryColorGradient= ") Then Cursor_NESW_PrimaryColorGradient = lin.Remove(0, "*Cursor_NESW_PrimaryColorGradient= ".Count)
                    If lin.StartsWith("*Cursor_NESW_PrimaryColorGradientMode= ") Then Cursor_NESW_PrimaryColorGradientMode = lin.Remove(0, "*Cursor_NESW_PrimaryColorGradientMode= ".Count)
                    If lin.StartsWith("*Cursor_NESW_PrimaryColorNoise= ") Then Cursor_NESW_PrimaryColorNoise = lin.Remove(0, "*Cursor_NESW_PrimaryColorNoise= ".Count)
                    If lin.StartsWith("*Cursor_NESW_PrimaryColorNoiseOpacity= ") Then Cursor_NESW_PrimaryColorNoiseOpacity = lin.Remove(0, "*Cursor_NESW_PrimaryColorNoiseOpacity= ".Count)
                    If lin.StartsWith("*Cursor_NESW_SecondaryColor1= ") Then Cursor_NESW_SecondaryColor1 = Color.FromArgb(lin.Remove(0, "*Cursor_NESW_SecondaryColor1= ".Count))
                    If lin.StartsWith("*Cursor_NESW_SecondaryColor2= ") Then Cursor_NESW_SecondaryColor2 = Color.FromArgb(lin.Remove(0, "*Cursor_NESW_SecondaryColor2= ".Count))
                    If lin.StartsWith("*Cursor_NESW_SecondaryColorGradient= ") Then Cursor_NESW_SecondaryColorGradient = lin.Remove(0, "*Cursor_NESW_SecondaryColorGradient= ".Count)
                    If lin.StartsWith("*Cursor_NESW_SecondaryColorGradientMode= ") Then Cursor_NESW_SecondaryColorGradientMode = lin.Remove(0, "*Cursor_NESW_SecondaryColorGradientMode= ".Count)
                    If lin.StartsWith("*Cursor_NESW_SecondaryColorNoise= ") Then Cursor_NESW_SecondaryColorNoise = lin.Remove(0, "*Cursor_NESW_SecondaryColorNoise= ".Count)
                    If lin.StartsWith("*Cursor_NESW_SecondaryColorNoiseOpacity= ") Then Cursor_NESW_SecondaryColorNoiseOpacity = lin.Remove(0, "*Cursor_NESW_SecondaryColorNoiseOpacity= ".Count)

#End Region

#Region "NWSE"
                    If lin.StartsWith("*Cursor_NWSE_PrimaryColor1= ") Then Cursor_NWSE_PrimaryColor1 = Color.FromArgb(lin.Remove(0, "*Cursor_NWSE_PrimaryColor1= ".Count))
                    If lin.StartsWith("*Cursor_NWSE_PrimaryColor2= ") Then Cursor_NWSE_PrimaryColor2 = Color.FromArgb(lin.Remove(0, "*Cursor_NWSE_PrimaryColor2= ".Count))
                    If lin.StartsWith("*Cursor_NWSE_PrimaryColorGradient= ") Then Cursor_NWSE_PrimaryColorGradient = lin.Remove(0, "*Cursor_NWSE_PrimaryColorGradient= ".Count)
                    If lin.StartsWith("*Cursor_NWSE_PrimaryColorGradientMode= ") Then Cursor_NWSE_PrimaryColorGradientMode = lin.Remove(0, "*Cursor_NWSE_PrimaryColorGradientMode= ".Count)
                    If lin.StartsWith("*Cursor_NWSE_PrimaryColorNoise= ") Then Cursor_NWSE_PrimaryColorNoise = lin.Remove(0, "*Cursor_NWSE_PrimaryColorNoise= ".Count)
                    If lin.StartsWith("*Cursor_NWSE_PrimaryColorNoiseOpacity= ") Then Cursor_NWSE_PrimaryColorNoiseOpacity = lin.Remove(0, "*Cursor_NWSE_PrimaryColorNoiseOpacity= ".Count)
                    If lin.StartsWith("*Cursor_NWSE_SecondaryColor1= ") Then Cursor_NWSE_SecondaryColor1 = Color.FromArgb(lin.Remove(0, "*Cursor_NWSE_SecondaryColor1= ".Count))
                    If lin.StartsWith("*Cursor_NWSE_SecondaryColor2= ") Then Cursor_NWSE_SecondaryColor2 = Color.FromArgb(lin.Remove(0, "*Cursor_NWSE_SecondaryColor2= ".Count))
                    If lin.StartsWith("*Cursor_NWSE_SecondaryColorGradient= ") Then Cursor_NWSE_SecondaryColorGradient = lin.Remove(0, "*Cursor_NWSE_SecondaryColorGradient= ".Count)
                    If lin.StartsWith("*Cursor_NWSE_SecondaryColorGradientMode= ") Then Cursor_NWSE_SecondaryColorGradientMode = lin.Remove(0, "*Cursor_NWSE_SecondaryColorGradientMode= ".Count)
                    If lin.StartsWith("*Cursor_NWSE_SecondaryColorNoise= ") Then Cursor_NWSE_SecondaryColorNoise = lin.Remove(0, "*Cursor_NWSE_SecondaryColorNoise= ".Count)
                    If lin.StartsWith("*Cursor_NWSE_SecondaryColorNoiseOpacity= ") Then Cursor_NWSE_SecondaryColorNoiseOpacity = lin.Remove(0, "*Cursor_NWSE_SecondaryColorNoiseOpacity= ".Count)

#End Region

#Region "Up"
                    If lin.StartsWith("*Cursor_Up_PrimaryColor1= ") Then Cursor_Up_PrimaryColor1 = Color.FromArgb(lin.Remove(0, "*Cursor_Up_PrimaryColor1= ".Count))
                    If lin.StartsWith("*Cursor_Up_PrimaryColor2= ") Then Cursor_Up_PrimaryColor2 = Color.FromArgb(lin.Remove(0, "*Cursor_Up_PrimaryColor2= ".Count))
                    If lin.StartsWith("*Cursor_Up_PrimaryColorGradient= ") Then Cursor_Up_PrimaryColorGradient = lin.Remove(0, "*Cursor_Up_PrimaryColorGradient= ".Count)
                    If lin.StartsWith("*Cursor_Up_PrimaryColorGradientMode= ") Then Cursor_Up_PrimaryColorGradientMode = lin.Remove(0, "*Cursor_Up_PrimaryColorGradientMode= ".Count)
                    If lin.StartsWith("*Cursor_Up_PrimaryColorNoise= ") Then Cursor_Up_PrimaryColorNoise = lin.Remove(0, "*Cursor_Up_PrimaryColorNoise= ".Count)
                    If lin.StartsWith("*Cursor_Up_PrimaryColorNoiseOpacity= ") Then Cursor_Up_PrimaryColorNoiseOpacity = lin.Remove(0, "*Cursor_Up_PrimaryColorNoiseOpacity= ".Count)
                    If lin.StartsWith("*Cursor_Up_SecondaryColor1= ") Then Cursor_Up_SecondaryColor1 = Color.FromArgb(lin.Remove(0, "*Cursor_Up_SecondaryColor1= ".Count))
                    If lin.StartsWith("*Cursor_Up_SecondaryColor2= ") Then Cursor_Up_SecondaryColor2 = Color.FromArgb(lin.Remove(0, "*Cursor_Up_SecondaryColor2= ".Count))
                    If lin.StartsWith("*Cursor_Up_SecondaryColorGradient= ") Then Cursor_Up_SecondaryColorGradient = lin.Remove(0, "*Cursor_Up_SecondaryColorGradient= ".Count)
                    If lin.StartsWith("*Cursor_Up_SecondaryColorGradientMode= ") Then Cursor_Up_SecondaryColorGradientMode = lin.Remove(0, "*Cursor_Up_SecondaryColorGradientMode= ".Count)
                    If lin.StartsWith("*Cursor_Up_SecondaryColorNoise= ") Then Cursor_Up_SecondaryColorNoise = lin.Remove(0, "*Cursor_Up_SecondaryColorNoise= ".Count)
                    If lin.StartsWith("*Cursor_Up_SecondaryColorNoiseOpacity= ") Then Cursor_Up_SecondaryColorNoiseOpacity = lin.Remove(0, "*Cursor_Up_SecondaryColorNoiseOpacity= ".Count)

#End Region

#Region "Pen"
                    If lin.StartsWith("*Cursor_Pen_PrimaryColor1= ") Then Cursor_Pen_PrimaryColor1 = Color.FromArgb(lin.Remove(0, "*Cursor_Pen_PrimaryColor1= ".Count))
                    If lin.StartsWith("*Cursor_Pen_PrimaryColor2= ") Then Cursor_Pen_PrimaryColor2 = Color.FromArgb(lin.Remove(0, "*Cursor_Pen_PrimaryColor2= ".Count))
                    If lin.StartsWith("*Cursor_Pen_PrimaryColorGradient= ") Then Cursor_Pen_PrimaryColorGradient = lin.Remove(0, "*Cursor_Pen_PrimaryColorGradient= ".Count)
                    If lin.StartsWith("*Cursor_Pen_PrimaryColorGradientMode= ") Then Cursor_Pen_PrimaryColorGradientMode = lin.Remove(0, "*Cursor_Pen_PrimaryColorGradientMode= ".Count)
                    If lin.StartsWith("*Cursor_Pen_PrimaryColorNoise= ") Then Cursor_Pen_PrimaryColorNoise = lin.Remove(0, "*Cursor_Pen_PrimaryColorNoise= ".Count)
                    If lin.StartsWith("*Cursor_Pen_PrimaryColorNoiseOpacity= ") Then Cursor_Pen_PrimaryColorNoiseOpacity = lin.Remove(0, "*Cursor_Pen_PrimaryColorNoiseOpacity= ".Count)
                    If lin.StartsWith("*Cursor_Pen_SecondaryColor1= ") Then Cursor_Pen_SecondaryColor1 = Color.FromArgb(lin.Remove(0, "*Cursor_Pen_SecondaryColor1= ".Count))
                    If lin.StartsWith("*Cursor_Pen_SecondaryColor2= ") Then Cursor_Pen_SecondaryColor2 = Color.FromArgb(lin.Remove(0, "*Cursor_Pen_SecondaryColor2= ".Count))
                    If lin.StartsWith("*Cursor_Pen_SecondaryColorGradient= ") Then Cursor_Pen_SecondaryColorGradient = lin.Remove(0, "*Cursor_Pen_SecondaryColorGradient= ".Count)
                    If lin.StartsWith("*Cursor_Pen_SecondaryColorGradientMode= ") Then Cursor_Pen_SecondaryColorGradientMode = lin.Remove(0, "*Cursor_Pen_SecondaryColorGradientMode= ".Count)
                    If lin.StartsWith("*Cursor_Pen_SecondaryColorNoise= ") Then Cursor_Pen_SecondaryColorNoise = lin.Remove(0, "*Cursor_Pen_SecondaryColorNoise= ".Count)
                    If lin.StartsWith("*Cursor_Pen_SecondaryColorNoiseOpacity= ") Then Cursor_Pen_SecondaryColorNoiseOpacity = lin.Remove(0, "*Cursor_Pen_SecondaryColorNoiseOpacity= ".Count)

#End Region

#Region "None"
                    If lin.StartsWith("*Cursor_None_PrimaryColor1= ") Then Cursor_None_PrimaryColor1 = Color.FromArgb(lin.Remove(0, "*Cursor_None_PrimaryColor1= ".Count))
                    If lin.StartsWith("*Cursor_None_PrimaryColor2= ") Then Cursor_None_PrimaryColor2 = Color.FromArgb(lin.Remove(0, "*Cursor_None_PrimaryColor2= ".Count))
                    If lin.StartsWith("*Cursor_None_PrimaryColorGradient= ") Then Cursor_None_PrimaryColorGradient = lin.Remove(0, "*Cursor_None_PrimaryColorGradient= ".Count)
                    If lin.StartsWith("*Cursor_None_PrimaryColorGradientMode= ") Then Cursor_None_PrimaryColorGradientMode = lin.Remove(0, "*Cursor_None_PrimaryColorGradientMode= ".Count)
                    If lin.StartsWith("*Cursor_None_PrimaryColorNoise= ") Then Cursor_None_PrimaryColorNoise = lin.Remove(0, "*Cursor_None_PrimaryColorNoise= ".Count)
                    If lin.StartsWith("*Cursor_None_PrimaryColorNoiseOpacity= ") Then Cursor_None_PrimaryColorNoiseOpacity = lin.Remove(0, "*Cursor_None_PrimaryColorNoiseOpacity= ".Count)
                    If lin.StartsWith("*Cursor_None_SecondaryColor1= ") Then Cursor_None_SecondaryColor1 = Color.FromArgb(lin.Remove(0, "*Cursor_None_SecondaryColor1= ".Count))
                    If lin.StartsWith("*Cursor_None_SecondaryColor2= ") Then Cursor_None_SecondaryColor2 = Color.FromArgb(lin.Remove(0, "*Cursor_None_SecondaryColor2= ".Count))
                    If lin.StartsWith("*Cursor_None_SecondaryColorGradient= ") Then Cursor_None_SecondaryColorGradient = lin.Remove(0, "*Cursor_None_SecondaryColorGradient= ".Count)
                    If lin.StartsWith("*Cursor_None_SecondaryColorGradientMode= ") Then Cursor_None_SecondaryColorGradientMode = lin.Remove(0, "*Cursor_None_SecondaryColorGradientMode= ".Count)
                    If lin.StartsWith("*Cursor_None_SecondaryColorNoise= ") Then Cursor_None_SecondaryColorNoise = lin.Remove(0, "*Cursor_None_SecondaryColorNoise= ".Count)
                    If lin.StartsWith("*Cursor_None_SecondaryColorNoiseOpacity= ") Then Cursor_None_SecondaryColorNoiseOpacity = lin.Remove(0, "*Cursor_None_SecondaryColorNoiseOpacity= ".Count)

#End Region

#Region "Link"
                    If lin.StartsWith("*Cursor_Link_PrimaryColor1= ") Then Cursor_Link_PrimaryColor1 = Color.FromArgb(lin.Remove(0, "*Cursor_Link_PrimaryColor1= ".Count))
                    If lin.StartsWith("*Cursor_Link_PrimaryColor2= ") Then Cursor_Link_PrimaryColor2 = Color.FromArgb(lin.Remove(0, "*Cursor_Link_PrimaryColor2= ".Count))
                    If lin.StartsWith("*Cursor_Link_PrimaryColorGradient= ") Then Cursor_Link_PrimaryColorGradient = lin.Remove(0, "*Cursor_Link_PrimaryColorGradient= ".Count)
                    If lin.StartsWith("*Cursor_Link_PrimaryColorGradientMode= ") Then Cursor_Link_PrimaryColorGradientMode = lin.Remove(0, "*Cursor_Link_PrimaryColorGradientMode= ".Count)
                    If lin.StartsWith("*Cursor_Link_PrimaryColorNoise= ") Then Cursor_Link_PrimaryColorNoise = lin.Remove(0, "*Cursor_Link_PrimaryColorNoise= ".Count)
                    If lin.StartsWith("*Cursor_Link_PrimaryColorNoiseOpacity= ") Then Cursor_Link_PrimaryColorNoiseOpacity = lin.Remove(0, "*Cursor_Link_PrimaryColorNoiseOpacity= ".Count)
                    If lin.StartsWith("*Cursor_Link_SecondaryColor1= ") Then Cursor_Link_SecondaryColor1 = Color.FromArgb(lin.Remove(0, "*Cursor_Link_SecondaryColor1= ".Count))
                    If lin.StartsWith("*Cursor_Link_SecondaryColor2= ") Then Cursor_Link_SecondaryColor2 = Color.FromArgb(lin.Remove(0, "*Cursor_Link_SecondaryColor2= ".Count))
                    If lin.StartsWith("*Cursor_Link_SecondaryColorGradient= ") Then Cursor_Link_SecondaryColorGradient = lin.Remove(0, "*Cursor_Link_SecondaryColorGradient= ".Count)
                    If lin.StartsWith("*Cursor_Link_SecondaryColorGradientMode= ") Then Cursor_Link_SecondaryColorGradientMode = lin.Remove(0, "*Cursor_Link_SecondaryColorGradientMode= ".Count)
                    If lin.StartsWith("*Cursor_Link_SecondaryColorNoise= ") Then Cursor_Link_SecondaryColorNoise = lin.Remove(0, "*Cursor_Link_SecondaryColorNoise= ".Count)
                    If lin.StartsWith("*Cursor_Link_SecondaryColorNoiseOpacity= ") Then Cursor_Link_SecondaryColorNoiseOpacity = lin.Remove(0, "*Cursor_Link_SecondaryColorNoiseOpacity= ".Count)

#End Region

#Region "Pin"
                    If lin.StartsWith("*Cursor_Pin_PrimaryColor1= ") Then Cursor_Pin_PrimaryColor1 = Color.FromArgb(lin.Remove(0, "*Cursor_Pin_PrimaryColor1= ".Count))
                    If lin.StartsWith("*Cursor_Pin_PrimaryColor2= ") Then Cursor_Pin_PrimaryColor2 = Color.FromArgb(lin.Remove(0, "*Cursor_Pin_PrimaryColor2= ".Count))
                    If lin.StartsWith("*Cursor_Pin_PrimaryColorGradient= ") Then Cursor_Pin_PrimaryColorGradient = lin.Remove(0, "*Cursor_Pin_PrimaryColorGradient= ".Count)
                    If lin.StartsWith("*Cursor_Pin_PrimaryColorGradientMode= ") Then Cursor_Pin_PrimaryColorGradientMode = lin.Remove(0, "*Cursor_Pin_PrimaryColorGradientMode= ".Count)
                    If lin.StartsWith("*Cursor_Pin_PrimaryColorNoise= ") Then Cursor_Pin_PrimaryColorNoise = lin.Remove(0, "*Cursor_Pin_PrimaryColorNoise= ".Count)
                    If lin.StartsWith("*Cursor_Pin_PrimaryColorNoiseOpacity= ") Then Cursor_Pin_PrimaryColorNoiseOpacity = lin.Remove(0, "*Cursor_Pin_PrimaryColorNoiseOpacity= ".Count)
                    If lin.StartsWith("*Cursor_Pin_SecondaryColor1= ") Then Cursor_Pin_SecondaryColor1 = Color.FromArgb(lin.Remove(0, "*Cursor_Pin_SecondaryColor1= ".Count))
                    If lin.StartsWith("*Cursor_Pin_SecondaryColor2= ") Then Cursor_Pin_SecondaryColor2 = Color.FromArgb(lin.Remove(0, "*Cursor_Pin_SecondaryColor2= ".Count))
                    If lin.StartsWith("*Cursor_Pin_SecondaryColorGradient= ") Then Cursor_Pin_SecondaryColorGradient = lin.Remove(0, "*Cursor_Pin_SecondaryColorGradient= ".Count)
                    If lin.StartsWith("*Cursor_Pin_SecondaryColorGradientMode= ") Then Cursor_Pin_SecondaryColorGradientMode = lin.Remove(0, "*Cursor_Pin_SecondaryColorGradientMode= ".Count)
                    If lin.StartsWith("*Cursor_Pin_SecondaryColorNoise= ") Then Cursor_Pin_SecondaryColorNoise = lin.Remove(0, "*Cursor_Pin_SecondaryColorNoise= ".Count)
                    If lin.StartsWith("*Cursor_Pin_SecondaryColorNoiseOpacity= ") Then Cursor_Pin_SecondaryColorNoiseOpacity = lin.Remove(0, "*Cursor_Pin_SecondaryColorNoiseOpacity= ".Count)

#End Region

#Region "Person"
                    If lin.StartsWith("*Cursor_Person_PrimaryColor1= ") Then Cursor_Person_PrimaryColor1 = Color.FromArgb(lin.Remove(0, "*Cursor_Person_PrimaryColor1= ".Count))
                    If lin.StartsWith("*Cursor_Person_PrimaryColor2= ") Then Cursor_Person_PrimaryColor2 = Color.FromArgb(lin.Remove(0, "*Cursor_Person_PrimaryColor2= ".Count))
                    If lin.StartsWith("*Cursor_Person_PrimaryColorGradient= ") Then Cursor_Person_PrimaryColorGradient = lin.Remove(0, "*Cursor_Person_PrimaryColorGradient= ".Count)
                    If lin.StartsWith("*Cursor_Person_PrimaryColorGradientMode= ") Then Cursor_Person_PrimaryColorGradientMode = lin.Remove(0, "*Cursor_Person_PrimaryColorGradientMode= ".Count)
                    If lin.StartsWith("*Cursor_Person_PrimaryColorNoise= ") Then Cursor_Person_PrimaryColorNoise = lin.Remove(0, "*Cursor_Person_PrimaryColorNoise= ".Count)
                    If lin.StartsWith("*Cursor_Person_PrimaryColorNoiseOpacity= ") Then Cursor_Person_PrimaryColorNoiseOpacity = lin.Remove(0, "*Cursor_Person_PrimaryColorNoiseOpacity= ".Count)
                    If lin.StartsWith("*Cursor_Person_SecondaryColor1= ") Then Cursor_Person_SecondaryColor1 = Color.FromArgb(lin.Remove(0, "*Cursor_Person_SecondaryColor1= ".Count))
                    If lin.StartsWith("*Cursor_Person_SecondaryColor2= ") Then Cursor_Person_SecondaryColor2 = Color.FromArgb(lin.Remove(0, "*Cursor_Person_SecondaryColor2= ".Count))
                    If lin.StartsWith("*Cursor_Person_SecondaryColorGradient= ") Then Cursor_Person_SecondaryColorGradient = lin.Remove(0, "*Cursor_Person_SecondaryColorGradient= ".Count)
                    If lin.StartsWith("*Cursor_Person_SecondaryColorGradientMode= ") Then Cursor_Person_SecondaryColorGradientMode = lin.Remove(0, "*Cursor_Person_SecondaryColorGradientMode= ".Count)
                    If lin.StartsWith("*Cursor_Person_SecondaryColorNoise= ") Then Cursor_Person_SecondaryColorNoise = lin.Remove(0, "*Cursor_Person_SecondaryColorNoise= ".Count)
                    If lin.StartsWith("*Cursor_Person_SecondaryColorNoiseOpacity= ") Then Cursor_Person_SecondaryColorNoiseOpacity = lin.Remove(0, "*Cursor_Person_SecondaryColorNoiseOpacity= ".Count)

#End Region

#Region "Cross"
                    If lin.StartsWith("*Cursor_Cross_PrimaryColor1= ") Then Cursor_Cross_PrimaryColor1 = Color.FromArgb(lin.Remove(0, "*Cursor_Cross_PrimaryColor1= ".Count))
                    If lin.StartsWith("*Cursor_Cross_PrimaryColor2= ") Then Cursor_Cross_PrimaryColor2 = Color.FromArgb(lin.Remove(0, "*Cursor_Cross_PrimaryColor2= ".Count))
                    If lin.StartsWith("*Cursor_Cross_PrimaryColorGradient= ") Then Cursor_Cross_PrimaryColorGradient = lin.Remove(0, "*Cursor_Cross_PrimaryColorGradient= ".Count)
                    If lin.StartsWith("*Cursor_Cross_PrimaryColorGradientMode= ") Then Cursor_Cross_PrimaryColorGradientMode = lin.Remove(0, "*Cursor_Cross_PrimaryColorGradientMode= ".Count)
                    If lin.StartsWith("*Cursor_Cross_PrimaryColorNoise= ") Then Cursor_Cross_PrimaryColorNoise = lin.Remove(0, "*Cursor_Cross_PrimaryColorNoise= ".Count)
                    If lin.StartsWith("*Cursor_Cross_PrimaryColorNoiseOpacity= ") Then Cursor_Cross_PrimaryColorNoiseOpacity = lin.Remove(0, "*Cursor_Cross_PrimaryColorNoiseOpacity= ".Count)
                    If lin.StartsWith("*Cursor_Cross_SecondaryColor1= ") Then Cursor_Cross_SecondaryColor1 = Color.FromArgb(lin.Remove(0, "*Cursor_Cross_SecondaryColor1= ".Count))
                    If lin.StartsWith("*Cursor_Cross_SecondaryColor2= ") Then Cursor_Cross_SecondaryColor2 = Color.FromArgb(lin.Remove(0, "*Cursor_Cross_SecondaryColor2= ".Count))
                    If lin.StartsWith("*Cursor_Cross_SecondaryColorGradient= ") Then Cursor_Cross_SecondaryColorGradient = lin.Remove(0, "*Cursor_Cross_SecondaryColorGradient= ".Count)
                    If lin.StartsWith("*Cursor_Cross_SecondaryColorGradientMode= ") Then Cursor_Cross_SecondaryColorGradientMode = lin.Remove(0, "*Cursor_Cross_SecondaryColorGradientMode= ".Count)
                    If lin.StartsWith("*Cursor_Cross_SecondaryColorNoise= ") Then Cursor_Cross_SecondaryColorNoise = lin.Remove(0, "*Cursor_Cross_SecondaryColorNoise= ".Count)
                    If lin.StartsWith("*Cursor_Cross_SecondaryColorNoiseOpacity= ") Then Cursor_Cross_SecondaryColorNoiseOpacity = lin.Remove(0, "*Cursor_Cross_SecondaryColorNoiseOpacity= ".Count)

#End Region
#End Region

                Next


            Case Mode.Init
                Author = Environment.UserName
                AppVersion = My.Application.Info.Version.ToString
                PaletteVersion = "1.0"
                PaletteName = "Init"

                Titlebar_Active = Color.Black
                Titlebar_Inactive = Color.Black
                StartMenu_Accent = Color.Black
                StartButton_Hover = Color.Black
                Taskbar_Background = Color.Black
                Taskbar_Icon_Underline = Color.Black
                StartMenuBackground_ActiveTaskbarButton = Color.Black
                StartListFolders_TaskbarFront = Color.Black
                SettingsIconsAndLinks = Color.Black
                ActionCenter_AppsLinks = Color.Black

                LogonUI_Background = Color.Black
                LogonUI_PersonalColors_Background = Color.Black
                LogonUI_PersonalColors_Accent = Color.Black
                LogonUI_DisableAcrylicBackgroundOnLogon = False
                LogonUI_DisableLogonBackgroundImage = False
                LogonUI_NoLockScreen = False

                Win32UI_ActiveBorder = Color.FromArgb(180, 180, 180)
                Win32UI_ActiveTitle = Color.FromArgb(153, 180, 209)
                Win32UI_AppWorkspace = Color.FromArgb(171, 171, 171)
                Win32UI_Background = Color.FromArgb(0, 0, 0)
                Win32UI_ButtonAlternateFace = Color.FromArgb(0, 0, 0)
                Win32UI_ButtonDkShadow = Color.FromArgb(105, 105, 105)
                Win32UI_ButtonFace = Color.FromArgb(240, 240, 240)
                Win32UI_ButtonHilight = Color.FromArgb(255, 255, 255)
                Win32UI_ButtonLight = Color.FromArgb(227, 227, 227)
                Win32UI_ButtonShadow = Color.FromArgb(160, 160, 160)
                Win32UI_ButtonText = Color.FromArgb(0, 0, 0)
                Win32UI_GradientActiveTitle = Color.FromArgb(185, 209, 234)
                Win32UI_GradientInactiveTitle = Color.FromArgb(215, 228, 242)
                Win32UI_GrayText = Color.FromArgb(109, 109, 109)
                Win32UI_HilightText = Color.FromArgb(255, 255, 255)
                Win32UI_HotTrackingColor = Color.FromArgb(0, 102, 204)
                Win32UI_InactiveBorder = Color.FromArgb(244, 247, 252)
                Win32UI_InactiveTitle = Color.FromArgb(191, 205, 219)
                Win32UI_InactiveTitleText = Color.FromArgb(0, 0, 0)
                Win32UI_InfoText = Color.FromArgb(0, 0, 0)
                Win32UI_InfoWindow = Color.FromArgb(255, 255, 225)
                Win32UI_Menu = Color.FromArgb(240, 240, 240)
                Win32UI_MenuBar = Color.FromArgb(240, 240, 240)
                Win32UI_MenuText = Color.FromArgb(0, 0, 0)
                Win32UI_Scrollbar = Color.FromArgb(200, 200, 200)
                Win32UI_TitleText = Color.FromArgb(0, 0, 0)
                Win32UI_Window = Color.FromArgb(255, 255, 255)
                Win32UI_WindowFrame = Color.FromArgb(100, 100, 100)
                Win32UI_WindowText = Color.FromArgb(0, 0, 0)
                Win32UI_Hilight = Color.FromArgb(0, 120, 215)
                Win32UI_MenuHilight = Color.FromArgb(0, 120, 215)
                Win32UI_Desktop = Color.FromArgb(0, 0, 0)

#Region "Cursors"
                Cursor_Enabled = False

#Region "Arrow"
                Cursor_Arrow_PrimaryColor1 = Color.White
                Cursor_Arrow_PrimaryColor2 = Color.White
                Cursor_Arrow_PrimaryColorGradient = False
                Cursor_Arrow_PrimaryColorGradientMode = GradientMode.Vertical
                Cursor_Arrow_PrimaryColorNoise = False
                Cursor_Arrow_PrimaryColorNoiseOpacity = 0.25
                Cursor_Arrow_SecondaryColor1 = Color.FromArgb(64, 65, 75)
                Cursor_Arrow_SecondaryColor2 = Color.FromArgb(64, 65, 75)
                Cursor_Arrow_SecondaryColorGradient = False
                Cursor_Arrow_SecondaryColorGradientMode = GradientMode.Vertical
                Cursor_Arrow_SecondaryColorNoise = False
                Cursor_Arrow_SecondaryColorNoiseOpacity = 0.25
#End Region

#Region "Help"
                Cursor_Help_PrimaryColor1 = Color.White
                Cursor_Help_PrimaryColor2 = Color.White
                Cursor_Help_PrimaryColorGradient = False
                Cursor_Help_PrimaryColorGradientMode = GradientMode.Vertical
                Cursor_Help_PrimaryColorNoise = False
                Cursor_Help_PrimaryColorNoiseOpacity = 0.25
                Cursor_Help_SecondaryColor1 = Color.FromArgb(64, 65, 75)
                Cursor_Help_SecondaryColor2 = Color.FromArgb(64, 65, 75)
                Cursor_Help_SecondaryColorGradient = False
                Cursor_Help_SecondaryColorGradientMode = GradientMode.Vertical
                Cursor_Help_SecondaryColorNoise = False
                Cursor_Help_SecondaryColorNoiseOpacity = 0.25
#End Region

#Region "AppLoading"
                Cursor_AppLoading_PrimaryColor1 = Color.White
                Cursor_AppLoading_PrimaryColor2 = Color.White
                Cursor_AppLoading_PrimaryColorGradient = False
                Cursor_AppLoading_PrimaryColorGradientMode = GradientMode.Circle
                Cursor_AppLoading_PrimaryColorNoise = False
                Cursor_AppLoading_PrimaryColorNoiseOpacity = 0.25
                Cursor_AppLoading_SecondaryColor1 = Color.FromArgb(64, 65, 75)
                Cursor_AppLoading_SecondaryColor2 = Color.FromArgb(64, 65, 75)
                Cursor_AppLoading_SecondaryColorGradient = False
                Cursor_AppLoading_SecondaryColorGradientMode = GradientMode.Circle
                Cursor_AppLoading_SecondaryColorNoise = False
                Cursor_AppLoading_SecondaryColorNoiseOpacity = 0.25
                Cursor_AppLoading_LoadingCircleBack1 = Color.FromArgb(42, 151, 243)
                Cursor_AppLoading_LoadingCircleBack2 = Color.FromArgb(42, 151, 243)
                Cursor_AppLoading_LoadingCircleBackGradient = False
                Cursor_AppLoading_LoadingCircleBackGradientMode = GradientMode.Circle
                Cursor_AppLoading_LoadingCircleBackNoise = False
                Cursor_AppLoading_LoadingCircleBackNoiseOpacity = 0.25
                Cursor_AppLoading_LoadingCircleHot1 = Color.FromArgb(37, 204, 255)
                Cursor_AppLoading_LoadingCircleHot2 = Color.FromArgb(37, 204, 255)
                Cursor_AppLoading_LoadingCircleHotGradient = False
                Cursor_AppLoading_LoadingCircleHotGradientMode = GradientMode.Circle
                Cursor_AppLoading_LoadingCircleHotNoise = False
                Cursor_AppLoading_LoadingCircleHotNoiseOpacity = 0.25
#End Region

#Region "Busy"
                Cursor_Busy_LoadingCircleBack1 = Color.FromArgb(42, 151, 243)
                Cursor_Busy_LoadingCircleBack2 = Color.FromArgb(42, 151, 243)
                Cursor_Busy_LoadingCircleBackGradient = False
                Cursor_Busy_LoadingCircleBackGradientMode = GradientMode.Circle
                Cursor_Busy_LoadingCircleBackNoise = False
                Cursor_Busy_LoadingCircleBackNoiseOpacity = 0.25
                Cursor_Busy_LoadingCircleHot1 = Color.FromArgb(37, 204, 255)
                Cursor_Busy_LoadingCircleHot2 = Color.FromArgb(37, 204, 255)
                Cursor_Busy_LoadingCircleHotGradient = False
                Cursor_Busy_LoadingCircleHotGradientMode = GradientMode.Circle
                Cursor_Busy_LoadingCircleHotNoise = False
                Cursor_Busy_LoadingCircleHotNoiseOpacity = 0.25
#End Region

#Region "Move"
                Cursor_Move_PrimaryColor1 = Color.White
                Cursor_Move_PrimaryColor2 = Color.White
                Cursor_Move_PrimaryColorGradient = False
                Cursor_Move_PrimaryColorGradientMode = GradientMode.Vertical
                Cursor_Move_PrimaryColorNoise = False
                Cursor_Move_PrimaryColorNoiseOpacity = 0.25
                Cursor_Move_SecondaryColor1 = Color.FromArgb(64, 65, 75)
                Cursor_Move_SecondaryColor2 = Color.FromArgb(64, 65, 75)
                Cursor_Move_SecondaryColorGradient = False
                Cursor_Move_SecondaryColorGradientMode = GradientMode.Vertical
                Cursor_Move_SecondaryColorNoise = False
                Cursor_Move_SecondaryColorNoiseOpacity = 0.25
#End Region

#Region "NS"
                Cursor_NS_PrimaryColor1 = Color.White
                Cursor_NS_PrimaryColor2 = Color.White
                Cursor_NS_PrimaryColorGradient = False
                Cursor_NS_PrimaryColorGradientMode = GradientMode.Vertical
                Cursor_NS_PrimaryColorNoise = False
                Cursor_NS_PrimaryColorNoiseOpacity = 0.25
                Cursor_NS_SecondaryColor1 = Color.FromArgb(64, 65, 75)
                Cursor_NS_SecondaryColor2 = Color.FromArgb(64, 65, 75)
                Cursor_NS_SecondaryColorGradient = False
                Cursor_NS_SecondaryColorGradientMode = GradientMode.Vertical
                Cursor_NS_SecondaryColorNoise = False
                Cursor_NS_SecondaryColorNoiseOpacity = 0.25
#End Region

#Region "EW"
                Cursor_EW_PrimaryColor1 = Color.White
                Cursor_EW_PrimaryColor2 = Color.White
                Cursor_EW_PrimaryColorGradient = False
                Cursor_EW_PrimaryColorGradientMode = GradientMode.Vertical
                Cursor_EW_PrimaryColorNoise = False
                Cursor_EW_PrimaryColorNoiseOpacity = 0.25
                Cursor_EW_SecondaryColor1 = Color.FromArgb(64, 65, 75)
                Cursor_EW_SecondaryColor2 = Color.FromArgb(64, 65, 75)
                Cursor_EW_SecondaryColorGradient = False
                Cursor_EW_SecondaryColorGradientMode = GradientMode.Vertical
                Cursor_EW_SecondaryColorNoise = False
                Cursor_EW_SecondaryColorNoiseOpacity = 0.25
#End Region

#Region "NESW"
                Cursor_NESW_PrimaryColor1 = Color.White
                Cursor_NESW_PrimaryColor2 = Color.White
                Cursor_NESW_PrimaryColorGradient = False
                Cursor_NESW_PrimaryColorGradientMode = GradientMode.Vertical
                Cursor_NESW_PrimaryColorNoise = False
                Cursor_NESW_PrimaryColorNoiseOpacity = 0.25
                Cursor_NESW_SecondaryColor1 = Color.FromArgb(64, 65, 75)
                Cursor_NESW_SecondaryColor2 = Color.FromArgb(64, 65, 75)
                Cursor_NESW_SecondaryColorGradient = False
                Cursor_NESW_SecondaryColorGradientMode = GradientMode.Vertical
                Cursor_NESW_SecondaryColorNoise = False
                Cursor_NESW_SecondaryColorNoiseOpacity = 0.25
#End Region

#Region "NWSE"
                Cursor_NWSE_PrimaryColor1 = Color.White
                Cursor_NWSE_PrimaryColor2 = Color.White
                Cursor_NWSE_PrimaryColorGradient = False
                Cursor_NWSE_PrimaryColorGradientMode = GradientMode.Vertical
                Cursor_NWSE_PrimaryColorNoise = False
                Cursor_NWSE_PrimaryColorNoiseOpacity = 0.25
                Cursor_NWSE_SecondaryColor1 = Color.FromArgb(64, 65, 75)
                Cursor_NWSE_SecondaryColor2 = Color.FromArgb(64, 65, 75)
                Cursor_NWSE_SecondaryColorGradient = False
                Cursor_NWSE_SecondaryColorGradientMode = GradientMode.Vertical
                Cursor_NWSE_SecondaryColorNoise = False
                Cursor_NWSE_SecondaryColorNoiseOpacity = 0.25
#End Region

#Region "Up"
                Cursor_Up_PrimaryColor1 = Color.White
                Cursor_Up_PrimaryColor2 = Color.White
                Cursor_Up_PrimaryColorGradient = False
                Cursor_Up_PrimaryColorGradientMode = GradientMode.Vertical
                Cursor_Up_PrimaryColorNoise = False
                Cursor_Up_PrimaryColorNoiseOpacity = 0.25
                Cursor_Up_SecondaryColor1 = Color.FromArgb(64, 65, 75)
                Cursor_Up_SecondaryColor2 = Color.FromArgb(64, 65, 75)
                Cursor_Up_SecondaryColorGradient = False
                Cursor_Up_SecondaryColorGradientMode = GradientMode.Vertical
                Cursor_Up_SecondaryColorNoise = False
                Cursor_Up_SecondaryColorNoiseOpacity = 0.25
#End Region

#Region "Pen"
                Cursor_Pen_PrimaryColor1 = Color.White
                Cursor_Pen_PrimaryColor2 = Color.White
                Cursor_Pen_PrimaryColorGradient = False
                Cursor_Pen_PrimaryColorGradientMode = GradientMode.Vertical
                Cursor_Pen_PrimaryColorNoise = False
                Cursor_Pen_PrimaryColorNoiseOpacity = 0.25
                Cursor_Pen_SecondaryColor1 = Color.FromArgb(64, 65, 75)
                Cursor_Pen_SecondaryColor2 = Color.FromArgb(64, 65, 75)
                Cursor_Pen_SecondaryColorGradient = False
                Cursor_Pen_SecondaryColorGradientMode = GradientMode.Vertical
                Cursor_Pen_SecondaryColorNoise = False
                Cursor_Pen_SecondaryColorNoiseOpacity = 0.25
#End Region

#Region "None"
                Cursor_None_PrimaryColor1 = Color.White
                Cursor_None_PrimaryColor2 = Color.White
                Cursor_None_PrimaryColorGradient = False
                Cursor_None_PrimaryColorGradientMode = GradientMode.Vertical
                Cursor_None_PrimaryColorNoise = False
                Cursor_None_PrimaryColorNoiseOpacity = 0.25
                Cursor_None_SecondaryColor1 = Color.Red
                Cursor_None_SecondaryColor2 = Color.Red
                Cursor_None_SecondaryColorGradient = False
                Cursor_None_SecondaryColorGradientMode = GradientMode.Vertical
                Cursor_None_SecondaryColorNoise = False
                Cursor_None_SecondaryColorNoiseOpacity = 0.25
#End Region

#Region "Link"
                Cursor_Link_PrimaryColor1 = Color.White
                Cursor_Link_PrimaryColor2 = Color.White
                Cursor_Link_PrimaryColorGradient = False
                Cursor_Link_PrimaryColorGradientMode = GradientMode.Vertical
                Cursor_Link_PrimaryColorNoise = False
                Cursor_Link_PrimaryColorNoiseOpacity = 0.25
                Cursor_Link_SecondaryColor1 = Color.FromArgb(64, 65, 75)
                Cursor_Link_SecondaryColor2 = Color.FromArgb(64, 65, 75)
                Cursor_Link_SecondaryColorGradient = False
                Cursor_Link_SecondaryColorGradientMode = GradientMode.Vertical
                Cursor_Link_SecondaryColorNoise = False
                Cursor_Link_SecondaryColorNoiseOpacity = 0.25
#End Region

#Region "Pin"
                Cursor_Pin_PrimaryColor1 = Color.White
                Cursor_Pin_PrimaryColor2 = Color.White
                Cursor_Pin_PrimaryColorGradient = False
                Cursor_Pin_PrimaryColorGradientMode = GradientMode.Vertical
                Cursor_Pin_PrimaryColorNoise = False
                Cursor_Pin_PrimaryColorNoiseOpacity = 0.25
                Cursor_Pin_SecondaryColor1 = Color.FromArgb(64, 65, 75)
                Cursor_Pin_SecondaryColor2 = Color.FromArgb(64, 65, 75)
                Cursor_Pin_SecondaryColorGradient = False
                Cursor_Pin_SecondaryColorGradientMode = GradientMode.Vertical
                Cursor_Pin_SecondaryColorNoise = False
                Cursor_Pin_SecondaryColorNoiseOpacity = 0.25
#End Region

#Region "Person"
                Cursor_Person_PrimaryColor1 = Color.White
                Cursor_Person_PrimaryColor2 = Color.White
                Cursor_Person_PrimaryColorGradient = False
                Cursor_Person_PrimaryColorGradientMode = GradientMode.Vertical
                Cursor_Person_PrimaryColorNoise = False
                Cursor_Person_PrimaryColorNoiseOpacity = 0.25
                Cursor_Person_SecondaryColor1 = Color.FromArgb(64, 65, 75)
                Cursor_Person_SecondaryColor2 = Color.FromArgb(64, 65, 75)
                Cursor_Person_SecondaryColorGradient = False
                Cursor_Person_SecondaryColorGradientMode = GradientMode.Vertical
                Cursor_Person_SecondaryColorNoise = False
                Cursor_Person_SecondaryColorNoiseOpacity = 0.25
#End Region

#Region "Cross"
                Cursor_Cross_PrimaryColor1 = Color.Transparent
                Cursor_Cross_PrimaryColor2 = Color.Transparent
                Cursor_Cross_PrimaryColorGradient = False
                Cursor_Cross_PrimaryColorGradientMode = GradientMode.Vertical
                Cursor_Cross_PrimaryColorNoise = False
                Cursor_Cross_PrimaryColorNoiseOpacity = 0.25
                Cursor_Cross_SecondaryColor1 = Color.FromArgb(64, 65, 75)
                Cursor_Cross_SecondaryColor2 = Color.FromArgb(64, 65, 75)
                Cursor_Cross_SecondaryColorGradient = False
                Cursor_Cross_SecondaryColorGradientMode = GradientMode.Vertical
                Cursor_Cross_SecondaryColorNoise = False
                Cursor_Cross_SecondaryColorNoiseOpacity = 0.25
#End Region
#End Region

                WinMode_Light = False
                AppMode_Light = False
                Transparency = True
                ApplyAccentonTitlebars = False
                ApplyAccentonTaskbar = False
        End Select

    End Sub

    Sub Save(ByVal [SaveTo] As SavingMode, Optional ByVal FileLocation As String = "")
        Select Case [SaveTo]
            Case SavingMode.Registry

                EditReg("HKEY_CURRENT_USER\Control Panel\Desktop", "AutoColorization", 0)

                Dim Colors As Byte() = {(ActionCenter_AppsLinks).R, (ActionCenter_AppsLinks).G, (ActionCenter_AppsLinks).B, (ActionCenter_AppsLinks).A _
                     , (Taskbar_Icon_Underline).R, (Taskbar_Icon_Underline).G, (Taskbar_Icon_Underline).B, (Taskbar_Icon_Underline).A _
                     , (StartButton_Hover).R, (StartButton_Hover).G, (StartButton_Hover).B, (StartButton_Hover).A _
                     , (SettingsIconsAndLinks).R, (SettingsIconsAndLinks).G, (SettingsIconsAndLinks).B, (SettingsIconsAndLinks).A _
                     , (StartMenuBackground_ActiveTaskbarButton).R, (StartMenuBackground_ActiveTaskbarButton).G, (StartMenuBackground_ActiveTaskbarButton).B, (StartMenuBackground_ActiveTaskbarButton).A _
                     , (StartListFolders_TaskbarFront).R, (StartListFolders_TaskbarFront).G, (StartListFolders_TaskbarFront).B, (StartListFolders_TaskbarFront).A _
                     , (Taskbar_Background).R, (Taskbar_Background).G, (Taskbar_Background).B, (Taskbar_Background).A _
                     , 255, 0, 0, 0}

                EditReg("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "AccentPalette", Colors, True)
                EditReg("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "StartColorMenu", BizareColorInvertor(StartMenu_Accent).ToArgb)
                EditReg("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "AccentColorMenu", BizareColorInvertor(Titlebar_Active).ToArgb)
                EditReg("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\DWM", "AccentColor", BizareColorInvertor(Titlebar_DWM_Active).ToArgb)
                EditReg("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\DWM", "AccentColorInactive", BizareColorInvertor(Titlebar_Inactive).ToArgb)

                EditReg("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "SystemUsesLightTheme", If(WinMode_Light, 1, 0))
                EditReg("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "AppsUseLightTheme", If(AppMode_Light, 1, 0))
                EditReg("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "EnableTransparency", If(Transparency, 1, 0))
                EditReg("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "ColorPrevalence", If(ApplyAccentonTaskbar, 1, 0))
                EditReg("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\DWM", "ColorPrevalence", If(ApplyAccentonTitlebars, 1, 0))

#Region "LogonUI"
                If isElevated Then
                    EditReg("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon", "Background", String.Format("{0} {1} {2}", LogonUI_Background.R, LogonUI_Background.G, LogonUI_Background.B), False, True)
                    EditReg("HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Windows\Personalization", "PersonalColors_Background", BizareColorInvertor(LogonUI_PersonalColors_Background).ToArgb)
                    EditReg("HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Windows\Personalization", "PersonalColors_Accent", BizareColorInvertor(LogonUI_PersonalColors_Accent).ToArgb)
                    EditReg("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\System", "DisableAcrylicBackgroundOnLogon", If(LogonUI_DisableAcrylicBackgroundOnLogon, 1, 0))
                    EditReg("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\System", "DisableLogonBackgroundImage", If(LogonUI_DisableLogonBackgroundImage, 1, 0))
                    EditReg("HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Windows\Personalization", "NoLockScreen", If(LogonUI_NoLockScreen, 1, 0))

                Else
                    Dim ls As New List(Of String)
                    ls.Clear()
                    ls.Add("Windows Registry Editor Version 5.00")
                    ls.Add(vbCrLf)
                    ls.Add("[HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon]")
                    ls.Add(String.Format("""Background""=""{0} {1} {2}""", LogonUI_Background.R, LogonUI_Background.G, LogonUI_Background.B))
                    ls.Add(vbCrLf)
                    ls.Add("[HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Windows\Personalization]")
                    ls.Add(String.Format("""PersonalColors_Background""=dword:{0}", RGB2HEX_oneline(BizareColorInvertor(LogonUI_PersonalColors_Background))))
                    ls.Add(String.Format("""PersonalColors_Accent""=dword:{0}", RGB2HEX_oneline(BizareColorInvertor(LogonUI_PersonalColors_Accent))))
                    ls.Add(String.Format("""NoLockScreen""=dword:0000000{0}", If(LogonUI_NoLockScreen, 1, 0)))
                    ls.Add(vbCrLf)
                    ls.Add("[HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\System]")
                    ls.Add(String.Format("""DisableAcrylicBackgroundOnLogon""=dword:0000000{0}", If(LogonUI_DisableAcrylicBackgroundOnLogon, 1, 0)))
                    ls.Add(String.Format("""DisableLogonBackgroundImage""=dword:0000000{0}", If(LogonUI_DisableLogonBackgroundImage, 1, 0)))

                    Dim result As String = CStr_FromList(ls)

                    Dim appData As String = System.Windows.Forms.Application.LocalUserAppDataPath
                    If Not IO.Directory.Exists(appData) Then IO.Directory.CreateDirectory(appData)

                    Dim tempreg As String = appData & "\tempreg.reg"

                    IO.File.WriteAllText(tempreg, result)

                    Dim process As Process = Nothing

                    Dim processStartInfo As New ProcessStartInfo With {
                       .FileName = "regedit",
                       .Verb = "runas",
                       .Arguments = String.Format("/s ""{0}""", tempreg),
                       .WindowStyle = ProcessWindowStyle.Hidden,
                       .CreateNoWindow = True,
                       .UseShellExecute = True
                    }
                    process = Process.Start(processStartInfo)
                    process.WaitForExit()
                    processStartInfo.FileName = "reg"
                    processStartInfo.Arguments = String.Format("import ""{0}""", tempreg)
                    process = Process.Start(processStartInfo)
                    process.WaitForExit()
                    Kill(tempreg)
                End If
#End Region

#Region "Win32UI"
                Dim C1 As New List(Of Integer)
                Dim C2 As New List(Of UInteger)

                C1.Clear()
                C2.Clear()

                C1.Add(13)
                C2.Add(ColorTranslator.ToWin32(Win32UI_Hilight))

                C1.Add(14)
                C2.Add(ColorTranslator.ToWin32(Win32UI_HilightText))

                C1.Add(9)
                C2.Add(ColorTranslator.ToWin32(Win32UI_TitleText))

                C1.Add(17)
                C2.Add(ColorTranslator.ToWin32(Win32UI_GrayText))

                C1.Add(11)
                C2.Add(ColorTranslator.ToWin32(Win32UI_InactiveBorder))

                C1.Add(3)
                C2.Add(ColorTranslator.ToWin32(Win32UI_InactiveTitle))

                C1.Add(2)
                C2.Add(ColorTranslator.ToWin32(Win32UI_ActiveTitle))

                C1.Add(10)
                C2.Add(ColorTranslator.ToWin32(Win32UI_ActiveBorder))

                C1.Add(12)
                C2.Add(ColorTranslator.ToWin32(Win32UI_AppWorkspace))

                C1.Add(1)
                C2.Add(ColorTranslator.ToWin32(Win32UI_Background))

                C1.Add(27)
                C2.Add(ColorTranslator.ToWin32(Win32UI_GradientActiveTitle))

                C1.Add(28)
                C2.Add(ColorTranslator.ToWin32(Win32UI_GradientInactiveTitle))

                C1.Add(19)
                C2.Add(ColorTranslator.ToWin32(Win32UI_InactiveTitleText))

                C1.Add(24)
                C2.Add(ColorTranslator.ToWin32(Win32UI_InfoWindow))

                C1.Add(23)
                C2.Add(ColorTranslator.ToWin32(Win32UI_InfoText))

                C1.Add(4)
                C2.Add(ColorTranslator.ToWin32(Win32UI_Menu))

                C1.Add(7)
                C2.Add(ColorTranslator.ToWin32(Win32UI_MenuText))

                C1.Add(0)
                C2.Add(ColorTranslator.ToWin32(Win32UI_Scrollbar))

                C1.Add(5)
                C2.Add(ColorTranslator.ToWin32(Win32UI_Window))

                C1.Add(6)
                C2.Add(ColorTranslator.ToWin32(Win32UI_WindowFrame))

                C1.Add(8)
                C2.Add(ColorTranslator.ToWin32(Win32UI_WindowText))

                C1.Add(26)
                C2.Add(ColorTranslator.ToWin32(Win32UI_HotTrackingColor))

                C1.Add(29)
                C2.Add(ColorTranslator.ToWin32(Win32UI_MenuHilight))

                C1.Add(30)
                C2.Add(ColorTranslator.ToWin32(Win32UI_MenuBar))

                C1.Add(15)
                C2.Add(ColorTranslator.ToWin32(Win32UI_ButtonFace))

                C1.Add(20)
                C2.Add(ColorTranslator.ToWin32(Win32UI_ButtonHilight))

                C1.Add(16)
                C2.Add(ColorTranslator.ToWin32(Win32UI_ButtonShadow))

                C1.Add(18)
                C2.Add(ColorTranslator.ToWin32(Win32UI_ButtonText))

                C1.Add(21)
                C2.Add(ColorTranslator.ToWin32(Win32UI_ButtonDkShadow))

                C1.Add(25)
                C2.Add(ColorTranslator.ToWin32(Win32UI_ButtonAlternateFace))

                C1.Add(22)
                C2.Add(ColorTranslator.ToWin32(Win32UI_ButtonLight))

                SetSysColors(C1.Count, C1.ToArray(), C2.ToArray())

                EditReg("HKEY_CURRENT_USER\Control Panel\Colors", "ActiveBorder", String.Format("{0} {1} {2}", Win32UI_ActiveBorder.R, Win32UI_ActiveBorder.G, Win32UI_ActiveBorder.B), False, True)
                EditReg("HKEY_CURRENT_USER\Control Panel\Colors", "ActiveTitle", String.Format("{0} {1} {2}", Win32UI_ActiveTitle.R, Win32UI_ActiveTitle.G, Win32UI_ActiveTitle.B), False, True)
                EditReg("HKEY_CURRENT_USER\Control Panel\Colors", "AppWorkspace", String.Format("{0} {1} {2}", Win32UI_AppWorkspace.R, Win32UI_AppWorkspace.G, Win32UI_AppWorkspace.B), False, True)
                EditReg("HKEY_CURRENT_USER\Control Panel\Colors", "Background", String.Format("{0} {1} {2}", Win32UI_Background.R, Win32UI_Background.G, Win32UI_Background.B), False, True)
                EditReg("HKEY_CURRENT_USER\Control Panel\Colors", "ButtonAlternateFace", String.Format("{0} {1} {2}", Win32UI_ButtonAlternateFace.R, Win32UI_ButtonAlternateFace.G, Win32UI_ButtonAlternateFace.B), False, True)
                EditReg("HKEY_CURRENT_USER\Control Panel\Colors", "ButtonDkShadow", String.Format("{0} {1} {2}", Win32UI_ButtonDkShadow.R, Win32UI_ButtonDkShadow.G, Win32UI_ButtonDkShadow.B), False, True)
                EditReg("HKEY_CURRENT_USER\Control Panel\Colors", "ButtonFace", String.Format("{0} {1} {2}", Win32UI_ButtonFace.R, Win32UI_ButtonFace.G, Win32UI_ButtonFace.B), False, True)
                EditReg("HKEY_CURRENT_USER\Control Panel\Colors", "ButtonHilight", String.Format("{0} {1} {2}", Win32UI_ButtonHilight.R, Win32UI_ButtonHilight.G, Win32UI_ButtonHilight.B), False, True)
                EditReg("HKEY_CURRENT_USER\Control Panel\Colors", "ButtonLight", String.Format("{0} {1} {2}", Win32UI_ButtonLight.R, Win32UI_ButtonLight.G, Win32UI_ButtonLight.B), False, True)
                EditReg("HKEY_CURRENT_USER\Control Panel\Colors", "ButtonShadow", String.Format("{0} {1} {2}", Win32UI_ButtonShadow.R, Win32UI_ButtonShadow.G, Win32UI_ButtonShadow.B), False, True)
                EditReg("HKEY_CURRENT_USER\Control Panel\Colors", "ButtonText", String.Format("{0} {1} {2}", Win32UI_ButtonText.R, Win32UI_ButtonText.G, Win32UI_ButtonText.B), False, True)
                EditReg("HKEY_CURRENT_USER\Control Panel\Colors", "GradientActiveTitle", String.Format("{0} {1} {2}", Win32UI_GradientActiveTitle.R, Win32UI_GradientActiveTitle.G, Win32UI_GradientActiveTitle.B), False, True)
                EditReg("HKEY_CURRENT_USER\Control Panel\Colors", "GradientInactiveTitle", String.Format("{0} {1} {2}", Win32UI_GradientInactiveTitle.R, Win32UI_GradientInactiveTitle.G, Win32UI_GradientInactiveTitle.B), False, True)
                EditReg("HKEY_CURRENT_USER\Control Panel\Colors", "GrayText", String.Format("{0} {1} {2}", Win32UI_GrayText.R, Win32UI_GrayText.G, Win32UI_GrayText.B), False, True)
                EditReg("HKEY_CURRENT_USER\Control Panel\Colors", "HilightText", String.Format("{0} {1} {2}", Win32UI_HilightText.R, Win32UI_HilightText.G, Win32UI_HilightText.B), False, True)
                EditReg("HKEY_CURRENT_USER\Control Panel\Colors", "HotTrackingColor", String.Format("{0} {1} {2}", Win32UI_HotTrackingColor.R, Win32UI_HotTrackingColor.G, Win32UI_HotTrackingColor.B), False, True)
                EditReg("HKEY_CURRENT_USER\Control Panel\Colors", "InactiveBorder", String.Format("{0} {1} {2}", Win32UI_InactiveBorder.R, Win32UI_InactiveBorder.G, Win32UI_InactiveBorder.B), False, True)
                EditReg("HKEY_CURRENT_USER\Control Panel\Colors", "InactiveTitle", String.Format("{0} {1} {2}", Win32UI_InactiveTitle.R, Win32UI_InactiveTitle.G, Win32UI_InactiveTitle.B), False, True)
                EditReg("HKEY_CURRENT_USER\Control Panel\Colors", "InactiveTitleText", String.Format("{0} {1} {2}", Win32UI_InactiveTitleText.R, Win32UI_InactiveTitleText.G, Win32UI_InactiveTitleText.B), False, True)
                EditReg("HKEY_CURRENT_USER\Control Panel\Colors", "InfoText", String.Format("{0} {1} {2}", Win32UI_InfoText.R, Win32UI_InfoText.G, Win32UI_InfoText.B), False, True)
                EditReg("HKEY_CURRENT_USER\Control Panel\Colors", "InfoWindow", String.Format("{0} {1} {2}", Win32UI_InfoWindow.R, Win32UI_InfoWindow.G, Win32UI_InfoWindow.B), False, True)
                EditReg("HKEY_CURRENT_USER\Control Panel\Colors", "Menu", String.Format("{0} {1} {2}", Win32UI_Menu.R, Win32UI_Menu.G, Win32UI_Menu.B), False, True)
                EditReg("HKEY_CURRENT_USER\Control Panel\Colors", "MenuBar", String.Format("{0} {1} {2}", Win32UI_MenuBar.R, Win32UI_MenuBar.G, Win32UI_MenuBar.B), False, True)
                EditReg("HKEY_CURRENT_USER\Control Panel\Colors", "MenuText", String.Format("{0} {1} {2}", Win32UI_MenuText.R, Win32UI_MenuText.G, Win32UI_MenuText.B), False, True)
                EditReg("HKEY_CURRENT_USER\Control Panel\Colors", "Scrollbar", String.Format("{0} {1} {2}", Win32UI_Scrollbar.R, Win32UI_Scrollbar.G, Win32UI_Scrollbar.B), False, True)
                EditReg("HKEY_CURRENT_USER\Control Panel\Colors", "TitleText", String.Format("{0} {1} {2}", Win32UI_TitleText.R, Win32UI_TitleText.G, Win32UI_TitleText.B), False, True)
                EditReg("HKEY_CURRENT_USER\Control Panel\Colors", "Window", String.Format("{0} {1} {2}", Win32UI_Window.R, Win32UI_Window.G, Win32UI_Window.B), False, True)
                EditReg("HKEY_CURRENT_USER\Control Panel\Colors", "WindowFrame", String.Format("{0} {1} {2}", Win32UI_WindowFrame.R, Win32UI_WindowFrame.G, Win32UI_WindowFrame.B), False, True)
                EditReg("HKEY_CURRENT_USER\Control Panel\Colors", "WindowText", String.Format("{0} {1} {2}", Win32UI_WindowText.R, Win32UI_WindowText.G, Win32UI_WindowText.B), False, True)
                EditReg("HKEY_CURRENT_USER\Control Panel\Colors", "Hilight", String.Format("{0} {1} {2}", Win32UI_Hilight.R, Win32UI_Hilight.G, Win32UI_Hilight.B), False, True)
                EditReg("HKEY_CURRENT_USER\Control Panel\Colors", "MenuHilight", String.Format("{0} {1} {2}", Win32UI_MenuHilight.R, Win32UI_MenuHilight.G, Win32UI_MenuHilight.B), False, True)
                EditReg("HKEY_CURRENT_USER\Control Panel\Colors", "Desktop", String.Format("{0} {1} {2}", Win32UI_Desktop.R, Win32UI_Desktop.G, Win32UI_Desktop.B), False, True)
#End Region

#Region "Cursors"
                Dim rMain As RegistryKey = Registry.CurrentUser.CreateSubKey("Software\WinPaletter\Cursors")
                rMain.SetValue("", Cursor_Enabled, RegistryValueKind.DWord)
                Dim r As RegistryKey

                r = rMain.CreateSubKey("Arrow")
                With r
                    .SetValue("PrimaryColor1", Cursor_Arrow_PrimaryColor1.ToArgb, RegistryValueKind.QWord)
                    .SetValue("PrimaryColor2", Cursor_Arrow_PrimaryColor2.ToArgb, RegistryValueKind.QWord)
                    .SetValue("PrimaryColorGradient", If(Cursor_Arrow_PrimaryColorGradient, 1, 0), RegistryValueKind.QWord)
                    .SetValue("PrimaryColorGradientMode", Cursor_Arrow_PrimaryColorGradientMode, RegistryValueKind.String)
                    .SetValue("PrimaryColorNoise", If(Cursor_Arrow_PrimaryColorNoise, 1, 0), RegistryValueKind.QWord)
                    .SetValue("PrimaryColorNoiseOpacity", Cursor_Arrow_PrimaryColorNoiseOpacity, RegistryValueKind.QWord)
                    .SetValue("SecondaryColor1", Cursor_Arrow_SecondaryColor1.ToArgb, RegistryValueKind.QWord)
                    .SetValue("SecondaryColor2", Cursor_Arrow_SecondaryColor2.ToArgb, RegistryValueKind.QWord)
                    .SetValue("SecondaryColorGradient", If(Cursor_Arrow_SecondaryColorGradient, 1, 0), RegistryValueKind.QWord)
                    .SetValue("SecondaryColorGradientMode", Cursor_Arrow_SecondaryColorGradientMode, RegistryValueKind.String)
                    .SetValue("SecondaryColorNoise", If(Cursor_Arrow_SecondaryColorNoise, 1, 0), RegistryValueKind.QWord)
                    .SetValue("SecondaryColorNoiseOpacity", Cursor_Arrow_SecondaryColorNoiseOpacity, RegistryValueKind.QWord)
                End With

                r = rMain.CreateSubKey("Help")
                With r
                    .SetValue("PrimaryColor1", Cursor_Help_PrimaryColor1.ToArgb, RegistryValueKind.QWord)
                    .SetValue("PrimaryColor2", Cursor_Help_PrimaryColor2.ToArgb, RegistryValueKind.QWord)
                    .SetValue("PrimaryColorGradient", If(Cursor_Help_PrimaryColorGradient, 1, 0), RegistryValueKind.QWord)
                    .SetValue("PrimaryColorGradientMode", Cursor_Help_PrimaryColorGradientMode, RegistryValueKind.String)
                    .SetValue("PrimaryColorNoise", If(Cursor_Help_PrimaryColorNoise, 1, 0), RegistryValueKind.QWord)
                    .SetValue("PrimaryColorNoiseOpacity", Cursor_Help_PrimaryColorNoiseOpacity, RegistryValueKind.QWord)
                    .SetValue("SecondaryColor1", Cursor_Help_SecondaryColor1.ToArgb, RegistryValueKind.QWord)
                    .SetValue("SecondaryColor2", Cursor_Help_SecondaryColor2.ToArgb, RegistryValueKind.QWord)
                    .SetValue("SecondaryColorGradient", If(Cursor_Help_SecondaryColorGradient, 1, 0), RegistryValueKind.QWord)
                    .SetValue("SecondaryColorGradientMode", Cursor_Help_SecondaryColorGradientMode, RegistryValueKind.String)
                    .SetValue("SecondaryColorNoise", If(Cursor_Help_SecondaryColorNoise, 1, 0), RegistryValueKind.QWord)
                    .SetValue("SecondaryColorNoiseOpacity", Cursor_Help_SecondaryColorNoiseOpacity, RegistryValueKind.QWord)
                End With

                r = rMain.CreateSubKey("AppLoading")
                With r
                    .SetValue("PrimaryColor1", Cursor_AppLoading_PrimaryColor1.ToArgb, RegistryValueKind.QWord)
                    .SetValue("PrimaryColor2", Cursor_AppLoading_PrimaryColor2.ToArgb, RegistryValueKind.QWord)
                    .SetValue("PrimaryColorGradient", If(Cursor_AppLoading_PrimaryColorGradient, 1, 0), RegistryValueKind.QWord)
                    .SetValue("PrimaryColorGradientMode", Cursor_AppLoading_PrimaryColorGradientMode, RegistryValueKind.String)
                    .SetValue("PrimaryColorNoise", If(Cursor_AppLoading_PrimaryColorNoise, 1, 0), RegistryValueKind.QWord)
                    .SetValue("PrimaryColorNoiseOpacity", Cursor_AppLoading_PrimaryColorNoiseOpacity, RegistryValueKind.QWord)
                    .SetValue("SecondaryColor1", Cursor_AppLoading_SecondaryColor1.ToArgb, RegistryValueKind.QWord)
                    .SetValue("SecondaryColor2", Cursor_AppLoading_SecondaryColor2.ToArgb, RegistryValueKind.QWord)
                    .SetValue("SecondaryColorGradient", If(Cursor_AppLoading_SecondaryColorGradient, 1, 0), RegistryValueKind.QWord)
                    .SetValue("SecondaryColorGradientMode", Cursor_AppLoading_SecondaryColorGradientMode, RegistryValueKind.String)
                    .SetValue("SecondaryColorNoise", If(Cursor_AppLoading_SecondaryColorNoise, 1, 0), RegistryValueKind.QWord)
                    .SetValue("SecondaryColorNoiseOpacity", Cursor_AppLoading_SecondaryColorNoiseOpacity, RegistryValueKind.QWord)
                    .SetValue("LoadingCircleBack1", Cursor_AppLoading_LoadingCircleBack1.ToArgb, RegistryValueKind.QWord)
                    .SetValue("LoadingCircleBack2", Cursor_AppLoading_LoadingCircleBack2.ToArgb, RegistryValueKind.QWord)
                    .SetValue("LoadingCircleBackGradient", If(Cursor_AppLoading_LoadingCircleBackGradient, 1, 0), RegistryValueKind.QWord)
                    .SetValue("LoadingCircleBackGradientMode", Cursor_AppLoading_LoadingCircleBackGradientMode, RegistryValueKind.String)
                    .SetValue("LoadingCircleBackNoise", If(Cursor_AppLoading_LoadingCircleBackNoise, 1, 0), RegistryValueKind.QWord)
                    .SetValue("LoadingCircleBackNoiseOpacity", Cursor_AppLoading_LoadingCircleBackNoiseOpacity, RegistryValueKind.QWord)
                    .SetValue("LoadingCircleHot1", Cursor_AppLoading_LoadingCircleHot1.ToArgb, RegistryValueKind.QWord)
                    .SetValue("LoadingCircleHot2", Cursor_AppLoading_LoadingCircleHot2.ToArgb, RegistryValueKind.QWord)
                    .SetValue("LoadingCircleHotGradient", If(Cursor_AppLoading_LoadingCircleHotGradient, 1, 0), RegistryValueKind.QWord)
                    .SetValue("LoadingCircleHotGradientMode", Cursor_AppLoading_LoadingCircleHotGradientMode, RegistryValueKind.String)
                    .SetValue("LoadingCircleHotNoise", If(Cursor_AppLoading_LoadingCircleHotNoise, 1, 0), RegistryValueKind.QWord)
                    .SetValue("LoadingCircleHotNoiseOpacity", Cursor_AppLoading_LoadingCircleHotNoiseOpacity, RegistryValueKind.QWord)
                End With

                r = rMain.CreateSubKey("Busy")
                With r
                    .SetValue("LoadingCircleBack1", Cursor_Busy_LoadingCircleBack1.ToArgb, RegistryValueKind.QWord)
                    .SetValue("LoadingCircleBack2", Cursor_Busy_LoadingCircleBack2.ToArgb, RegistryValueKind.QWord)
                    .SetValue("LoadingCircleBackGradient", If(Cursor_Busy_LoadingCircleBackGradient, 1, 0), RegistryValueKind.QWord)
                    .SetValue("LoadingCircleBackGradientMode", Cursor_Busy_LoadingCircleBackGradientMode, RegistryValueKind.String)
                    .SetValue("LoadingCircleBackNoise", If(Cursor_Busy_LoadingCircleBackNoise, 1, 0), RegistryValueKind.QWord)
                    .SetValue("LoadingCircleBackNoiseOpacity", Cursor_Busy_LoadingCircleBackNoiseOpacity, RegistryValueKind.QWord)
                    .SetValue("LoadingCircleHot1", Cursor_Busy_LoadingCircleHot1.ToArgb, RegistryValueKind.QWord)
                    .SetValue("LoadingCircleHot2", Cursor_Busy_LoadingCircleHot2.ToArgb, RegistryValueKind.QWord)
                    .SetValue("LoadingCircleHotGradient", If(Cursor_Busy_LoadingCircleHotGradient, 1, 0), RegistryValueKind.QWord)
                    .SetValue("LoadingCircleHotGradientMode", Cursor_Busy_LoadingCircleHotGradientMode, RegistryValueKind.String)
                    .SetValue("LoadingCircleHotNoise", If(Cursor_Busy_LoadingCircleHotNoise, 1, 0), RegistryValueKind.QWord)
                    .SetValue("LoadingCircleHotNoiseOpacity", Cursor_Busy_LoadingCircleHotNoiseOpacity, RegistryValueKind.QWord)
                End With

                r = rMain.CreateSubKey("Move")
                With r
                    .SetValue("PrimaryColor1", Cursor_Move_PrimaryColor1.ToArgb, RegistryValueKind.QWord)
                    .SetValue("PrimaryColor2", Cursor_Move_PrimaryColor2.ToArgb, RegistryValueKind.QWord)
                    .SetValue("PrimaryColorGradient", If(Cursor_Move_PrimaryColorGradient, 1, 0), RegistryValueKind.QWord)
                    .SetValue("PrimaryColorGradientMode", Cursor_Move_PrimaryColorGradientMode, RegistryValueKind.String)
                    .SetValue("PrimaryColorNoise", If(Cursor_Move_PrimaryColorNoise, 1, 0), RegistryValueKind.QWord)
                    .SetValue("PrimaryColorNoiseOpacity", Cursor_Move_PrimaryColorNoiseOpacity, RegistryValueKind.QWord)
                    .SetValue("SecondaryColor1", Cursor_Move_SecondaryColor1.ToArgb, RegistryValueKind.QWord)
                    .SetValue("SecondaryColor2", Cursor_Move_SecondaryColor2.ToArgb, RegistryValueKind.QWord)
                    .SetValue("SecondaryColorGradient", If(Cursor_Move_SecondaryColorGradient, 1, 0), RegistryValueKind.QWord)
                    .SetValue("SecondaryColorGradientMode", Cursor_Move_SecondaryColorGradientMode, RegistryValueKind.String)
                    .SetValue("SecondaryColorNoise", If(Cursor_Move_SecondaryColorNoise, 1, 0), RegistryValueKind.QWord)
                    .SetValue("SecondaryColorNoiseOpacity", Cursor_Move_SecondaryColorNoiseOpacity, RegistryValueKind.QWord)
                End With

                r = rMain.CreateSubKey("NS")
                With r
                    .SetValue("PrimaryColor1", Cursor_NS_PrimaryColor1.ToArgb, RegistryValueKind.QWord)
                    .SetValue("PrimaryColor2", Cursor_NS_PrimaryColor2.ToArgb, RegistryValueKind.QWord)
                    .SetValue("PrimaryColorGradient", If(Cursor_NS_PrimaryColorGradient, 1, 0), RegistryValueKind.QWord)
                    .SetValue("PrimaryColorGradientMode", Cursor_NS_PrimaryColorGradientMode, RegistryValueKind.String)
                    .SetValue("PrimaryColorNoise", If(Cursor_NS_PrimaryColorNoise, 1, 0), RegistryValueKind.QWord)
                    .SetValue("PrimaryColorNoiseOpacity", Cursor_NS_PrimaryColorNoiseOpacity, RegistryValueKind.QWord)
                    .SetValue("SecondaryColor1", Cursor_NS_SecondaryColor1.ToArgb, RegistryValueKind.QWord)
                    .SetValue("SecondaryColor2", Cursor_NS_SecondaryColor2.ToArgb, RegistryValueKind.QWord)
                    .SetValue("SecondaryColorGradient", If(Cursor_NS_SecondaryColorGradient, 1, 0), RegistryValueKind.QWord)
                    .SetValue("SecondaryColorGradientMode", Cursor_NS_SecondaryColorGradientMode, RegistryValueKind.String)
                    .SetValue("SecondaryColorNoise", If(Cursor_NS_SecondaryColorNoise, 1, 0), RegistryValueKind.QWord)
                    .SetValue("SecondaryColorNoiseOpacity", Cursor_NS_SecondaryColorNoiseOpacity, RegistryValueKind.QWord)
                End With

                r = rMain.CreateSubKey("EW")
                With r
                    .SetValue("PrimaryColor1", Cursor_EW_PrimaryColor1.ToArgb, RegistryValueKind.QWord)
                    .SetValue("PrimaryColor2", Cursor_EW_PrimaryColor2.ToArgb, RegistryValueKind.QWord)
                    .SetValue("PrimaryColorGradient", If(Cursor_EW_PrimaryColorGradient, 1, 0), RegistryValueKind.QWord)
                    .SetValue("PrimaryColorGradientMode", Cursor_EW_PrimaryColorGradientMode, RegistryValueKind.String)
                    .SetValue("PrimaryColorNoise", If(Cursor_EW_PrimaryColorNoise, 1, 0), RegistryValueKind.QWord)
                    .SetValue("PrimaryColorNoiseOpacity", Cursor_EW_PrimaryColorNoiseOpacity, RegistryValueKind.QWord)
                    .SetValue("SecondaryColor1", Cursor_EW_SecondaryColor1.ToArgb, RegistryValueKind.QWord)
                    .SetValue("SecondaryColor2", Cursor_EW_SecondaryColor2.ToArgb, RegistryValueKind.QWord)
                    .SetValue("SecondaryColorGradient", If(Cursor_EW_SecondaryColorGradient, 1, 0), RegistryValueKind.QWord)
                    .SetValue("SecondaryColorGradientMode", Cursor_EW_SecondaryColorGradientMode, RegistryValueKind.String)
                    .SetValue("SecondaryColorNoise", If(Cursor_EW_SecondaryColorNoise, 1, 0), RegistryValueKind.QWord)
                    .SetValue("SecondaryColorNoiseOpacity", Cursor_EW_SecondaryColorNoiseOpacity, RegistryValueKind.QWord)
                End With

                r = rMain.CreateSubKey("NESW")
                With r
                    .SetValue("PrimaryColor1", Cursor_NESW_PrimaryColor1.ToArgb, RegistryValueKind.QWord)
                    .SetValue("PrimaryColor2", Cursor_NESW_PrimaryColor2.ToArgb, RegistryValueKind.QWord)
                    .SetValue("PrimaryColorGradient", If(Cursor_NESW_PrimaryColorGradient, 1, 0), RegistryValueKind.QWord)
                    .SetValue("PrimaryColorGradientMode", Cursor_NESW_PrimaryColorGradientMode, RegistryValueKind.String)
                    .SetValue("PrimaryColorNoise", If(Cursor_NESW_PrimaryColorNoise, 1, 0), RegistryValueKind.QWord)
                    .SetValue("PrimaryColorNoiseOpacity", Cursor_NESW_PrimaryColorNoiseOpacity, RegistryValueKind.QWord)
                    .SetValue("SecondaryColor1", Cursor_NESW_SecondaryColor1.ToArgb, RegistryValueKind.QWord)
                    .SetValue("SecondaryColor2", Cursor_NESW_SecondaryColor2.ToArgb, RegistryValueKind.QWord)
                    .SetValue("SecondaryColorGradient", If(Cursor_NESW_SecondaryColorGradient, 1, 0), RegistryValueKind.QWord)
                    .SetValue("SecondaryColorGradientMode", Cursor_NESW_SecondaryColorGradientMode, RegistryValueKind.String)
                    .SetValue("SecondaryColorNoise", If(Cursor_NESW_SecondaryColorNoise, 1, 0), RegistryValueKind.QWord)
                    .SetValue("SecondaryColorNoiseOpacity", Cursor_NESW_SecondaryColorNoiseOpacity, RegistryValueKind.QWord)
                End With

                r = rMain.CreateSubKey("NWSE")
                With r
                    .SetValue("PrimaryColor1", Cursor_NWSE_PrimaryColor1.ToArgb, RegistryValueKind.QWord)
                    .SetValue("PrimaryColor2", Cursor_NWSE_PrimaryColor2.ToArgb, RegistryValueKind.QWord)
                    .SetValue("PrimaryColorGradient", If(Cursor_NWSE_PrimaryColorGradient, 1, 0), RegistryValueKind.QWord)
                    .SetValue("PrimaryColorGradientMode", Cursor_NWSE_PrimaryColorGradientMode, RegistryValueKind.String)
                    .SetValue("PrimaryColorNoise", If(Cursor_NWSE_PrimaryColorNoise, 1, 0), RegistryValueKind.QWord)
                    .SetValue("PrimaryColorNoiseOpacity", Cursor_NWSE_PrimaryColorNoiseOpacity, RegistryValueKind.QWord)
                    .SetValue("SecondaryColor1", Cursor_NWSE_SecondaryColor1.ToArgb, RegistryValueKind.QWord)
                    .SetValue("SecondaryColor2", Cursor_NWSE_SecondaryColor2.ToArgb, RegistryValueKind.QWord)
                    .SetValue("SecondaryColorGradient", If(Cursor_NWSE_SecondaryColorGradient, 1, 0), RegistryValueKind.QWord)
                    .SetValue("SecondaryColorGradientMode", Cursor_NWSE_SecondaryColorGradientMode, RegistryValueKind.String)
                    .SetValue("SecondaryColorNoise", If(Cursor_NWSE_SecondaryColorNoise, 1, 0), RegistryValueKind.QWord)
                    .SetValue("SecondaryColorNoiseOpacity", Cursor_NWSE_SecondaryColorNoiseOpacity, RegistryValueKind.QWord)
                End With

                r = rMain.CreateSubKey("Up")
                With r
                    .SetValue("PrimaryColor1", Cursor_Up_PrimaryColor1.ToArgb, RegistryValueKind.QWord)
                    .SetValue("PrimaryColor2", Cursor_Up_PrimaryColor2.ToArgb, RegistryValueKind.QWord)
                    .SetValue("PrimaryColorGradient", If(Cursor_Up_PrimaryColorGradient, 1, 0), RegistryValueKind.QWord)
                    .SetValue("PrimaryColorGradientMode", Cursor_Up_PrimaryColorGradientMode, RegistryValueKind.String)
                    .SetValue("PrimaryColorNoise", If(Cursor_Up_PrimaryColorNoise, 1, 0), RegistryValueKind.QWord)
                    .SetValue("PrimaryColorNoiseOpacity", Cursor_Up_PrimaryColorNoiseOpacity, RegistryValueKind.QWord)
                    .SetValue("SecondaryColor1", Cursor_Up_SecondaryColor1.ToArgb, RegistryValueKind.QWord)
                    .SetValue("SecondaryColor2", Cursor_Up_SecondaryColor2.ToArgb, RegistryValueKind.QWord)
                    .SetValue("SecondaryColorGradient", If(Cursor_Up_SecondaryColorGradient, 1, 0), RegistryValueKind.QWord)
                    .SetValue("SecondaryColorGradientMode", Cursor_Up_SecondaryColorGradientMode, RegistryValueKind.String)
                    .SetValue("SecondaryColorNoise", If(Cursor_Up_SecondaryColorNoise, 1, 0), RegistryValueKind.QWord)
                    .SetValue("SecondaryColorNoiseOpacity", Cursor_Up_SecondaryColorNoiseOpacity, RegistryValueKind.QWord)
                End With

                r = rMain.CreateSubKey("Pen")
                With r
                    .SetValue("PrimaryColor1", Cursor_Pen_PrimaryColor1.ToArgb, RegistryValueKind.QWord)
                    .SetValue("PrimaryColor2", Cursor_Pen_PrimaryColor2.ToArgb, RegistryValueKind.QWord)
                    .SetValue("PrimaryColorGradient", If(Cursor_Pen_PrimaryColorGradient, 1, 0), RegistryValueKind.QWord)
                    .SetValue("PrimaryColorGradientMode", Cursor_Pen_PrimaryColorGradientMode, RegistryValueKind.String)
                    .SetValue("PrimaryColorNoise", If(Cursor_Pen_PrimaryColorNoise, 1, 0), RegistryValueKind.QWord)
                    .SetValue("PrimaryColorNoiseOpacity", Cursor_Pen_PrimaryColorNoiseOpacity, RegistryValueKind.QWord)
                    .SetValue("SecondaryColor1", Cursor_Pen_SecondaryColor1.ToArgb, RegistryValueKind.QWord)
                    .SetValue("SecondaryColor2", Cursor_Pen_SecondaryColor2.ToArgb, RegistryValueKind.QWord)
                    .SetValue("SecondaryColorGradient", If(Cursor_Pen_SecondaryColorGradient, 1, 0), RegistryValueKind.QWord)
                    .SetValue("SecondaryColorGradientMode", Cursor_Pen_SecondaryColorGradientMode, RegistryValueKind.String)
                    .SetValue("SecondaryColorNoise", If(Cursor_Pen_SecondaryColorNoise, 1, 0), RegistryValueKind.QWord)
                    .SetValue("SecondaryColorNoiseOpacity", Cursor_Pen_SecondaryColorNoiseOpacity, RegistryValueKind.QWord)
                End With

                r = rMain.CreateSubKey("None")
                With r
                    .SetValue("PrimaryColor1", Cursor_None_PrimaryColor1.ToArgb, RegistryValueKind.QWord)
                    .SetValue("PrimaryColor2", Cursor_None_PrimaryColor2.ToArgb, RegistryValueKind.QWord)
                    .SetValue("PrimaryColorGradient", If(Cursor_None_PrimaryColorGradient, 1, 0), RegistryValueKind.QWord)
                    .SetValue("PrimaryColorGradientMode", Cursor_None_PrimaryColorGradientMode, RegistryValueKind.String)
                    .SetValue("PrimaryColorNoise", If(Cursor_None_PrimaryColorNoise, 1, 0), RegistryValueKind.QWord)
                    .SetValue("PrimaryColorNoiseOpacity", Cursor_None_PrimaryColorNoiseOpacity, RegistryValueKind.QWord)
                    .SetValue("SecondaryColor1", Cursor_None_SecondaryColor1.ToArgb, RegistryValueKind.QWord)
                    .SetValue("SecondaryColor2", Cursor_None_SecondaryColor2.ToArgb, RegistryValueKind.QWord)
                    .SetValue("SecondaryColorGradient", If(Cursor_None_SecondaryColorGradient, 1, 0), RegistryValueKind.QWord)
                    .SetValue("SecondaryColorGradientMode", Cursor_None_SecondaryColorGradientMode, RegistryValueKind.String)
                    .SetValue("SecondaryColorNoise", If(Cursor_None_SecondaryColorNoise, 1, 0), RegistryValueKind.QWord)
                    .SetValue("SecondaryColorNoiseOpacity", Cursor_None_SecondaryColorNoiseOpacity, RegistryValueKind.QWord)
                End With

                r = rMain.CreateSubKey("Link")
                With r
                    .SetValue("PrimaryColor1", Cursor_Link_PrimaryColor1.ToArgb, RegistryValueKind.QWord)
                    .SetValue("PrimaryColor2", Cursor_Link_PrimaryColor2.ToArgb, RegistryValueKind.QWord)
                    .SetValue("PrimaryColorGradient", If(Cursor_Link_PrimaryColorGradient, 1, 0), RegistryValueKind.QWord)
                    .SetValue("PrimaryColorGradientMode", Cursor_Link_PrimaryColorGradientMode, RegistryValueKind.String)
                    .SetValue("PrimaryColorNoise", If(Cursor_Link_PrimaryColorNoise, 1, 0), RegistryValueKind.QWord)
                    .SetValue("PrimaryColorNoiseOpacity", Cursor_Link_PrimaryColorNoiseOpacity, RegistryValueKind.QWord)
                    .SetValue("SecondaryColor1", Cursor_Link_SecondaryColor1.ToArgb, RegistryValueKind.QWord)
                    .SetValue("SecondaryColor2", Cursor_Link_SecondaryColor2.ToArgb, RegistryValueKind.QWord)
                    .SetValue("SecondaryColorGradient", If(Cursor_Link_SecondaryColorGradient, 1, 0), RegistryValueKind.QWord)
                    .SetValue("SecondaryColorGradientMode", Cursor_Link_SecondaryColorGradientMode, RegistryValueKind.String)
                    .SetValue("SecondaryColorNoise", If(Cursor_Link_SecondaryColorNoise, 1, 0), RegistryValueKind.QWord)
                    .SetValue("SecondaryColorNoiseOpacity", Cursor_Link_SecondaryColorNoiseOpacity, RegistryValueKind.QWord)
                End With

                r = rMain.CreateSubKey("Pin")
                With r
                    .SetValue("PrimaryColor1", Cursor_Pin_PrimaryColor1.ToArgb, RegistryValueKind.QWord)
                    .SetValue("PrimaryColor2", Cursor_Pin_PrimaryColor2.ToArgb, RegistryValueKind.QWord)
                    .SetValue("PrimaryColorGradient", If(Cursor_Pin_PrimaryColorGradient, 1, 0), RegistryValueKind.QWord)
                    .SetValue("PrimaryColorGradientMode", Cursor_Pin_PrimaryColorGradientMode, RegistryValueKind.String)
                    .SetValue("PrimaryColorNoise", If(Cursor_Pin_PrimaryColorNoise, 1, 0), RegistryValueKind.QWord)
                    .SetValue("PrimaryColorNoiseOpacity", Cursor_Pin_PrimaryColorNoiseOpacity, RegistryValueKind.QWord)
                    .SetValue("SecondaryColor1", Cursor_Pin_SecondaryColor1.ToArgb, RegistryValueKind.QWord)
                    .SetValue("SecondaryColor2", Cursor_Pin_SecondaryColor2.ToArgb, RegistryValueKind.QWord)
                    .SetValue("SecondaryColorGradient", If(Cursor_Pin_SecondaryColorGradient, 1, 0), RegistryValueKind.QWord)
                    .SetValue("SecondaryColorGradientMode", Cursor_Pin_SecondaryColorGradientMode, RegistryValueKind.String)
                    .SetValue("SecondaryColorNoise", If(Cursor_Pin_SecondaryColorNoise, 1, 0), RegistryValueKind.QWord)
                    .SetValue("SecondaryColorNoiseOpacity", Cursor_Pin_SecondaryColorNoiseOpacity, RegistryValueKind.QWord)
                End With

                r = rMain.CreateSubKey("Person")
                With r
                    .SetValue("PrimaryColor1", Cursor_Person_PrimaryColor1.ToArgb, RegistryValueKind.QWord)
                    .SetValue("PrimaryColor2", Cursor_Person_PrimaryColor2.ToArgb, RegistryValueKind.QWord)
                    .SetValue("PrimaryColorGradient", If(Cursor_Person_PrimaryColorGradient, 1, 0), RegistryValueKind.QWord)
                    .SetValue("PrimaryColorGradientMode", Cursor_Person_PrimaryColorGradientMode, RegistryValueKind.String)
                    .SetValue("PrimaryColorNoise", If(Cursor_Person_PrimaryColorNoise, 1, 0), RegistryValueKind.QWord)
                    .SetValue("PrimaryColorNoiseOpacity", Cursor_Person_PrimaryColorNoiseOpacity, RegistryValueKind.QWord)
                    .SetValue("SecondaryColor1", Cursor_Person_SecondaryColor1.ToArgb, RegistryValueKind.QWord)
                    .SetValue("SecondaryColor2", Cursor_Person_SecondaryColor2.ToArgb, RegistryValueKind.QWord)
                    .SetValue("SecondaryColorGradient", If(Cursor_Person_SecondaryColorGradient, 1, 0), RegistryValueKind.QWord)
                    .SetValue("SecondaryColorGradientMode", Cursor_Person_SecondaryColorGradientMode, RegistryValueKind.String)
                    .SetValue("SecondaryColorNoise", If(Cursor_Person_SecondaryColorNoise, 1, 0), RegistryValueKind.QWord)
                    .SetValue("SecondaryColorNoiseOpacity", Cursor_Person_SecondaryColorNoiseOpacity, RegistryValueKind.QWord)
                End With

                r = rMain.CreateSubKey("Cross")
                With r
                    .SetValue("PrimaryColor1", Cursor_Cross_PrimaryColor1.ToArgb, RegistryValueKind.QWord)
                    .SetValue("PrimaryColor2", Cursor_Cross_PrimaryColor2.ToArgb, RegistryValueKind.QWord)
                    .SetValue("PrimaryColorGradient", If(Cursor_Cross_PrimaryColorGradient, 1, 0), RegistryValueKind.QWord)
                    .SetValue("PrimaryColorGradientMode", Cursor_Cross_PrimaryColorGradientMode, RegistryValueKind.String)
                    .SetValue("PrimaryColorNoise", If(Cursor_Cross_PrimaryColorNoise, 1, 0), RegistryValueKind.QWord)
                    .SetValue("PrimaryColorNoiseOpacity", Cursor_Cross_PrimaryColorNoiseOpacity, RegistryValueKind.QWord)
                    .SetValue("SecondaryColor1", Cursor_Cross_SecondaryColor1.ToArgb, RegistryValueKind.QWord)
                    .SetValue("SecondaryColor2", Cursor_Cross_SecondaryColor2.ToArgb, RegistryValueKind.QWord)
                    .SetValue("SecondaryColorGradient", If(Cursor_Cross_SecondaryColorGradient, 1, 0), RegistryValueKind.QWord)
                    .SetValue("SecondaryColorGradientMode", Cursor_Cross_SecondaryColorGradientMode, RegistryValueKind.String)
                    .SetValue("SecondaryColorNoise", If(Cursor_Cross_SecondaryColorNoise, 1, 0), RegistryValueKind.QWord)
                    .SetValue("SecondaryColorNoiseOpacity", Cursor_Cross_SecondaryColorNoiseOpacity, RegistryValueKind.QWord)
                End With


                r.Close()
                rMain.Close()
#End Region

            Case SavingMode.File

                Dim tx As New List(Of String)
                tx.Clear()
                tx.Add("<WinPaletter - Programmed by Abdelrhman_AK>")
                tx.Add("*Created from App Version= " & AppVersion & vbCrLf)

#Region "General Info"
                tx.Add("<General>")
                tx.Add("*Palette Name= " & PaletteName)
                If String.IsNullOrWhiteSpace(PaletteDescription) Then
                    tx.Add("*Palette Description= ")
                Else
                    tx.Add("*Palette Description= " & PaletteDescription.Replace(vbCrLf, "<br>"))
                End If
                tx.Add("*Palette File Version= " & PaletteVersion)
                tx.Add("*Author= " & Author)
                tx.Add("*AuthorSocialMediaLink= " & AuthorSocialMediaLink)
                tx.Add("</General>" & vbCrLf)
#End Region

#Region "Modern Windows"
                tx.Add("<Toggles>")
                tx.Add("*WinMode_Light= " & WinMode_Light)
                tx.Add("*AppMode_Light= " & AppMode_Light)
                tx.Add("*Transparency= " & Transparency)
                tx.Add("*AccentColorOnTitlebarAndBorders= " & ApplyAccentonTitlebars)
                tx.Add("*AccentColorOnStartTaskbarAndActionCenter= " & ApplyAccentonTaskbar)
                tx.Add("</Toggles>" & vbCrLf)

                tx.Add("<UWP>")
                tx.Add("*Titlebar_Active= " & Titlebar_Active.ToArgb)
                tx.Add("*Titlebar_Inactive= " & Titlebar_Inactive.ToArgb)
                tx.Add("*ActionCenter_AppsLinks= " & ActionCenter_AppsLinks.ToArgb)
                tx.Add("*Taskbar_Icon_Underline= " & Taskbar_Icon_Underline.ToArgb)
                tx.Add("*StartButton_Hover= " & StartButton_Hover.ToArgb)
                tx.Add("*SettingsIconsAndLinks= " & SettingsIconsAndLinks.ToArgb)
                tx.Add("*StartMenuBackground_ActiveTaskbarButton= " & StartMenuBackground_ActiveTaskbarButton.ToArgb)
                tx.Add("*StartListFolders_TaskbarFront= " & StartListFolders_TaskbarFront.ToArgb)
                tx.Add("*Taskbar_Background= " & Taskbar_Background.ToArgb)
                tx.Add("*StartMenu_Accent= " & StartMenu_Accent.ToArgb)
                tx.Add("*Undefined= " & Color.FromArgb(255, 0, 0, 0).ToArgb)
                tx.Add("</UWP>" & vbCrLf)
#End Region

#Region "LogonUI"
                tx.Add("<LogonUI>")
                tx.Add("*LogonUI_Background= " & LogonUI_Background.ToArgb)
                tx.Add("*LogonUI_PersonalColors_Background= " & LogonUI_PersonalColors_Background.ToArgb)
                tx.Add("*LogonUI_PersonalColors_Accent= " & LogonUI_PersonalColors_Accent.ToArgb)
                tx.Add("*LogonUI_DisableAcrylicBackgroundOnLogon= " & LogonUI_DisableAcrylicBackgroundOnLogon)
                tx.Add("*LogonUI_DisableLogonBackgroundImage= " & LogonUI_DisableLogonBackgroundImage)
                tx.Add("*LogonUI_NoLockScreen= " & LogonUI_NoLockScreen)
                tx.Add("</LogonUI>" & vbCrLf)
#End Region

#Region "Win32UI"
                tx.Add("<Win32UI>")
                tx.Add("*Win32UI_ActiveBorder= " & Win32UI_ActiveBorder.ToArgb)
                tx.Add("*Win32UI_ActiveTitle= " & Win32UI_ActiveTitle.ToArgb)
                tx.Add("*Win32UI_AppWorkspace= " & Win32UI_AppWorkspace.ToArgb)
                tx.Add("*Win32UI_Background= " & Win32UI_Background.ToArgb)
                tx.Add("*Win32UI_ButtonAlternateFace= " & Win32UI_ButtonAlternateFace.ToArgb)
                tx.Add("*Win32UI_ButtonDkShadow= " & Win32UI_ButtonDkShadow.ToArgb)
                tx.Add("*Win32UI_ButtonFace= " & Win32UI_ButtonFace.ToArgb)
                tx.Add("*Win32UI_ButtonHilight= " & Win32UI_ButtonHilight.ToArgb)
                tx.Add("*Win32UI_ButtonLight= " & Win32UI_ButtonLight.ToArgb)
                tx.Add("*Win32UI_ButtonShadow= " & Win32UI_ButtonShadow.ToArgb)
                tx.Add("*Win32UI_ButtonText= " & Win32UI_ButtonText.ToArgb)
                tx.Add("*Win32UI_GradientActiveTitle= " & Win32UI_GradientActiveTitle.ToArgb)
                tx.Add("*Win32UI_GradientInactiveTitle= " & Win32UI_GradientInactiveTitle.ToArgb)
                tx.Add("*Win32UI_GrayText= " & Win32UI_GrayText.ToArgb)
                tx.Add("*Win32UI_HilightText= " & Win32UI_HilightText.ToArgb)
                tx.Add("*Win32UI_HotTrackingColor= " & Win32UI_HotTrackingColor.ToArgb)
                tx.Add("*Win32UI_InactiveBorder= " & Win32UI_InactiveBorder.ToArgb)
                tx.Add("*Win32UI_InactiveTitle= " & Win32UI_InactiveTitle.ToArgb)
                tx.Add("*Win32UI_InactiveTitleText= " & Win32UI_InactiveTitleText.ToArgb)
                tx.Add("*Win32UI_InfoText= " & Win32UI_InfoText.ToArgb)
                tx.Add("*Win32UI_InfoWindow= " & Win32UI_InfoWindow.ToArgb)
                tx.Add("*Win32UI_Menu= " & Win32UI_Menu.ToArgb)
                tx.Add("*Win32UI_MenuBar= " & Win32UI_MenuBar.ToArgb)
                tx.Add("*Win32UI_MenuText= " & Win32UI_MenuText.ToArgb)
                tx.Add("*Win32UI_Scrollbar= " & Win32UI_Scrollbar.ToArgb)
                tx.Add("*Win32UI_TitleText= " & Win32UI_TitleText.ToArgb)
                tx.Add("*Win32UI_Window= " & Win32UI_Window.ToArgb)
                tx.Add("*Win32UI_WindowFrame= " & Win32UI_WindowFrame.ToArgb)
                tx.Add("*Win32UI_WindowText= " & Win32UI_WindowText.ToArgb)
                tx.Add("*Win32UI_Hilight= " & Win32UI_Hilight.ToArgb)
                tx.Add("*Win32UI_MenuHilight= " & Win32UI_MenuHilight.ToArgb)
                tx.Add("*Win32UI_Desktop= " & Win32UI_Desktop.ToArgb)
                tx.Add("</Win32UI>")
#End Region

#Region "Cursors"
                tx.Add(vbCrLf & "<Cursors>")
                tx.Add("*Cursor_Enabled= " & Cursor_Enabled)

#Region "Arrow"
                tx.Add("*Cursor_Arrow_PrimaryColor1= " & Cursor_Arrow_PrimaryColor1.ToArgb)
                tx.Add("*Cursor_Arrow_PrimaryColor2= " & Cursor_Arrow_PrimaryColor2.ToArgb)
                tx.Add("*Cursor_Arrow_PrimaryColorGradient= " & Cursor_Arrow_PrimaryColorGradient)
                tx.Add("*Cursor_Arrow_PrimaryColorGradientMode= " & Cursor_Arrow_PrimaryColorGradientMode)
                tx.Add("*Cursor_Arrow_PrimaryColorNoise= " & Cursor_Arrow_PrimaryColorNoise)
                tx.Add("*Cursor_Arrow_PrimaryColorNoiseOpacity= " & Cursor_Arrow_PrimaryColorNoiseOpacity)
                tx.Add("*Cursor_Arrow_SecondaryColor1= " & Cursor_Arrow_SecondaryColor1.ToArgb)
                tx.Add("*Cursor_Arrow_SecondaryColor2= " & Cursor_Arrow_SecondaryColor2.ToArgb)
                tx.Add("*Cursor_Arrow_SecondaryColorGradient= " & Cursor_Arrow_SecondaryColorGradient)
                tx.Add("*Cursor_Arrow_SecondaryColorGradientMode= " & Cursor_Arrow_SecondaryColorGradientMode)
                tx.Add("*Cursor_Arrow_SecondaryColorNoise= " & Cursor_Arrow_SecondaryColorNoise)
                tx.Add("*Cursor_Arrow_SecondaryColorNoiseOpacity= " & Cursor_Arrow_SecondaryColorNoiseOpacity)

#End Region

#Region "Help"
                tx.Add("*Cursor_Help_PrimaryColor1= " & Cursor_Help_PrimaryColor1.ToArgb)
                tx.Add("*Cursor_Help_PrimaryColor2= " & Cursor_Help_PrimaryColor2.ToArgb)
                tx.Add("*Cursor_Help_PrimaryColorGradient= " & Cursor_Help_PrimaryColorGradient)
                tx.Add("*Cursor_Help_PrimaryColorGradientMode= " & Cursor_Help_PrimaryColorGradientMode)
                tx.Add("*Cursor_Help_PrimaryColorNoise= " & Cursor_Help_PrimaryColorNoise)
                tx.Add("*Cursor_Help_PrimaryColorNoiseOpacity= " & Cursor_Help_PrimaryColorNoiseOpacity)
                tx.Add("*Cursor_Help_SecondaryColor1= " & Cursor_Help_SecondaryColor1.ToArgb)
                tx.Add("*Cursor_Help_SecondaryColor2= " & Cursor_Help_SecondaryColor2.ToArgb)
                tx.Add("*Cursor_Help_SecondaryColorGradient= " & Cursor_Help_SecondaryColorGradient)
                tx.Add("*Cursor_Help_SecondaryColorGradientMode= " & Cursor_Help_SecondaryColorGradientMode)
                tx.Add("*Cursor_Help_SecondaryColorNoise= " & Cursor_Help_SecondaryColorNoise)
                tx.Add("*Cursor_Help_SecondaryColorNoiseOpacity= " & Cursor_Help_SecondaryColorNoiseOpacity)
#End Region

#Region "AppLoading"
                tx.Add("*Cursor_AppLoading_PrimaryColor1= " & Cursor_AppLoading_PrimaryColor1.ToArgb)
                tx.Add("*Cursor_AppLoading_PrimaryColor2= " & Cursor_AppLoading_PrimaryColor2.ToArgb)
                tx.Add("*Cursor_AppLoading_PrimaryColorGradient= " & Cursor_AppLoading_PrimaryColorGradient)
                tx.Add("*Cursor_AppLoading_PrimaryColorGradientMode= " & Cursor_AppLoading_PrimaryColorGradientMode)
                tx.Add("*Cursor_AppLoading_PrimaryColorNoise= " & Cursor_AppLoading_PrimaryColorNoise)
                tx.Add("*Cursor_AppLoading_PrimaryColorNoiseOpacity= " & Cursor_AppLoading_PrimaryColorNoiseOpacity)
                tx.Add("*Cursor_AppLoading_SecondaryColor1= " & Cursor_AppLoading_SecondaryColor1.ToArgb)
                tx.Add("*Cursor_AppLoading_SecondaryColor2= " & Cursor_AppLoading_SecondaryColor2.ToArgb)
                tx.Add("*Cursor_AppLoading_SecondaryColorGradient= " & Cursor_AppLoading_SecondaryColorGradient)
                tx.Add("*Cursor_AppLoading_SecondaryColorGradientMode= " & Cursor_AppLoading_SecondaryColorGradientMode)
                tx.Add("*Cursor_AppLoading_SecondaryColorNoise= " & Cursor_AppLoading_SecondaryColorNoise)
                tx.Add("*Cursor_AppLoading_SecondaryColorNoiseOpacity= " & Cursor_AppLoading_SecondaryColorNoiseOpacity)
                tx.Add("*Cursor_AppLoading_LoadingCircleBack1= " & Cursor_AppLoading_LoadingCircleBack1.ToArgb)
                tx.Add("*Cursor_AppLoading_LoadingCircleBack2= " & Cursor_AppLoading_LoadingCircleBack2.ToArgb)
                tx.Add("*Cursor_AppLoading_LoadingCircleBackGradient= " & Cursor_AppLoading_LoadingCircleBackGradient)
                tx.Add("*Cursor_AppLoading_LoadingCircleBackGradientMode= " & Cursor_AppLoading_LoadingCircleBackGradientMode)
                tx.Add("*Cursor_AppLoading_LoadingCircleBackNoise= " & Cursor_AppLoading_LoadingCircleBackNoise)
                tx.Add("*Cursor_AppLoading_LoadingCircleBackNoiseOpacity= " & Cursor_AppLoading_LoadingCircleBackNoiseOpacity)
                tx.Add("*Cursor_AppLoading_LoadingCircleHot1= " & Cursor_AppLoading_LoadingCircleHot1.ToArgb)
                tx.Add("*Cursor_AppLoading_LoadingCircleHot2= " & Cursor_AppLoading_LoadingCircleHot2.ToArgb)
                tx.Add("*Cursor_AppLoading_LoadingCircleHotGradient= " & Cursor_AppLoading_LoadingCircleHotGradient)
                tx.Add("*Cursor_AppLoading_LoadingCircleHotGradientMode= " & Cursor_AppLoading_LoadingCircleHotGradientMode)
                tx.Add("*Cursor_AppLoading_LoadingCircleHotNoise= " & Cursor_AppLoading_LoadingCircleHotNoise)
                tx.Add("*Cursor_AppLoading_LoadingCircleHotNoiseOpacity= " & Cursor_AppLoading_LoadingCircleHotNoiseOpacity)

#End Region

#Region "Busy"
                tx.Add("*Cursor_Busy_LoadingCircleBack1= " & Cursor_Busy_LoadingCircleBack1.ToArgb)
                tx.Add("*Cursor_Busy_LoadingCircleBack2= " & Cursor_Busy_LoadingCircleBack2.ToArgb)
                tx.Add("*Cursor_Busy_LoadingCircleBackGradient= " & Cursor_Busy_LoadingCircleBackGradient)
                tx.Add("*Cursor_Busy_LoadingCircleBackGradientMode= " & Cursor_Busy_LoadingCircleBackGradientMode)
                tx.Add("*Cursor_Busy_LoadingCircleBackNoise= " & Cursor_Busy_LoadingCircleBackNoise)
                tx.Add("*Cursor_Busy_LoadingCircleBackNoiseOpacity= " & Cursor_Busy_LoadingCircleBackNoiseOpacity)
                tx.Add("*Cursor_Busy_LoadingCircleHot1= " & Cursor_Busy_LoadingCircleHot1.ToArgb)
                tx.Add("*Cursor_Busy_LoadingCircleHot2= " & Cursor_Busy_LoadingCircleHot2.ToArgb)
                tx.Add("*Cursor_Busy_LoadingCircleHotGradient= " & Cursor_Busy_LoadingCircleHotGradient)
                tx.Add("*Cursor_Busy_LoadingCircleHotGradientMode= " & Cursor_Busy_LoadingCircleHotGradientMode)
                tx.Add("*Cursor_Busy_LoadingCircleHotNoise= " & Cursor_Busy_LoadingCircleHotNoise)
                tx.Add("*Cursor_Busy_LoadingCircleHotNoiseOpacity= " & Cursor_Busy_LoadingCircleHotNoiseOpacity)

#End Region

#Region "Move"
                tx.Add("*Cursor_Move_PrimaryColor1= " & Cursor_Move_PrimaryColor1.ToArgb)
                tx.Add("*Cursor_Move_PrimaryColor2= " & Cursor_Move_PrimaryColor2.ToArgb)
                tx.Add("*Cursor_Move_PrimaryColorGradient= " & Cursor_Move_PrimaryColorGradient)
                tx.Add("*Cursor_Move_PrimaryColorGradientMode= " & Cursor_Move_PrimaryColorGradientMode)
                tx.Add("*Cursor_Move_PrimaryColorNoise= " & Cursor_Move_PrimaryColorNoise)
                tx.Add("*Cursor_Move_PrimaryColorNoiseOpacity= " & Cursor_Move_PrimaryColorNoiseOpacity)
                tx.Add("*Cursor_Move_SecondaryColor1= " & Cursor_Move_SecondaryColor1.ToArgb)
                tx.Add("*Cursor_Move_SecondaryColor2= " & Cursor_Move_SecondaryColor2.ToArgb)
                tx.Add("*Cursor_Move_SecondaryColorGradient= " & Cursor_Move_SecondaryColorGradient)
                tx.Add("*Cursor_Move_SecondaryColorGradientMode= " & Cursor_Move_SecondaryColorGradientMode)
                tx.Add("*Cursor_Move_SecondaryColorNoise= " & Cursor_Move_SecondaryColorNoise)
                tx.Add("*Cursor_Move_SecondaryColorNoiseOpacity= " & Cursor_Move_SecondaryColorNoiseOpacity)

#End Region

#Region "NS"
                tx.Add("*Cursor_NS_PrimaryColor1= " & Cursor_NS_PrimaryColor1.ToArgb)
                tx.Add("*Cursor_NS_PrimaryColor2= " & Cursor_NS_PrimaryColor2.ToArgb)
                tx.Add("*Cursor_NS_PrimaryColorGradient= " & Cursor_NS_PrimaryColorGradient)
                tx.Add("*Cursor_NS_PrimaryColorGradientMode= " & Cursor_NS_PrimaryColorGradientMode)
                tx.Add("*Cursor_NS_PrimaryColorNoise= " & Cursor_NS_PrimaryColorNoise)
                tx.Add("*Cursor_NS_PrimaryColorNoiseOpacity= " & Cursor_NS_PrimaryColorNoiseOpacity)
                tx.Add("*Cursor_NS_SecondaryColor1= " & Cursor_NS_SecondaryColor1.ToArgb)
                tx.Add("*Cursor_NS_SecondaryColor2= " & Cursor_NS_SecondaryColor2.ToArgb)
                tx.Add("*Cursor_NS_SecondaryColorGradient= " & Cursor_NS_SecondaryColorGradient)
                tx.Add("*Cursor_NS_SecondaryColorGradientMode= " & Cursor_NS_SecondaryColorGradientMode)
                tx.Add("*Cursor_NS_SecondaryColorNoise= " & Cursor_NS_SecondaryColorNoise)
                tx.Add("*Cursor_NS_SecondaryColorNoiseOpacity= " & Cursor_NS_SecondaryColorNoiseOpacity)

#End Region

#Region "EW"
                tx.Add("*Cursor_EW_PrimaryColor1= " & Cursor_EW_PrimaryColor1.ToArgb)
                tx.Add("*Cursor_EW_PrimaryColor2= " & Cursor_EW_PrimaryColor2.ToArgb)
                tx.Add("*Cursor_EW_PrimaryColorGradient= " & Cursor_EW_PrimaryColorGradient)
                tx.Add("*Cursor_EW_PrimaryColorGradientMode= " & Cursor_EW_PrimaryColorGradientMode)
                tx.Add("*Cursor_EW_PrimaryColorNoise= " & Cursor_EW_PrimaryColorNoise)
                tx.Add("*Cursor_EW_PrimaryColorNoiseOpacity= " & Cursor_EW_PrimaryColorNoiseOpacity)
                tx.Add("*Cursor_EW_SecondaryColor1= " & Cursor_EW_SecondaryColor1.ToArgb)
                tx.Add("*Cursor_EW_SecondaryColor2= " & Cursor_EW_SecondaryColor2.ToArgb)
                tx.Add("*Cursor_EW_SecondaryColorGradient= " & Cursor_EW_SecondaryColorGradient)
                tx.Add("*Cursor_EW_SecondaryColorGradientMode= " & Cursor_EW_SecondaryColorGradientMode)
                tx.Add("*Cursor_EW_SecondaryColorNoise= " & Cursor_EW_SecondaryColorNoise)
                tx.Add("*Cursor_EW_SecondaryColorNoiseOpacity= " & Cursor_EW_SecondaryColorNoiseOpacity)

#End Region

#Region "NESW"
                tx.Add("*Cursor_NESW_PrimaryColor1= " & Cursor_NESW_PrimaryColor1.ToArgb)
                tx.Add("*Cursor_NESW_PrimaryColor2= " & Cursor_NESW_PrimaryColor2.ToArgb)
                tx.Add("*Cursor_NESW_PrimaryColorGradient= " & Cursor_NESW_PrimaryColorGradient)
                tx.Add("*Cursor_NESW_PrimaryColorGradientMode= " & Cursor_NESW_PrimaryColorGradientMode)
                tx.Add("*Cursor_NESW_PrimaryColorNoise= " & Cursor_NESW_PrimaryColorNoise)
                tx.Add("*Cursor_NESW_PrimaryColorNoiseOpacity= " & Cursor_NESW_PrimaryColorNoiseOpacity)
                tx.Add("*Cursor_NESW_SecondaryColor1= " & Cursor_NESW_SecondaryColor1.ToArgb)
                tx.Add("*Cursor_NESW_SecondaryColor2= " & Cursor_NESW_SecondaryColor2.ToArgb)
                tx.Add("*Cursor_NESW_SecondaryColorGradient= " & Cursor_NESW_SecondaryColorGradient)
                tx.Add("*Cursor_NESW_SecondaryColorGradientMode= " & Cursor_NESW_SecondaryColorGradientMode)
                tx.Add("*Cursor_NESW_SecondaryColorNoise= " & Cursor_NESW_SecondaryColorNoise)
                tx.Add("*Cursor_NESW_SecondaryColorNoiseOpacity= " & Cursor_NESW_SecondaryColorNoiseOpacity)

#End Region

#Region "NWSE"
                tx.Add("*Cursor_NWSE_PrimaryColor1= " & Cursor_NWSE_PrimaryColor1.ToArgb)
                tx.Add("*Cursor_NWSE_PrimaryColor2= " & Cursor_NWSE_PrimaryColor2.ToArgb)
                tx.Add("*Cursor_NWSE_PrimaryColorGradient= " & Cursor_NWSE_PrimaryColorGradient)
                tx.Add("*Cursor_NWSE_PrimaryColorGradientMode= " & Cursor_NWSE_PrimaryColorGradientMode)
                tx.Add("*Cursor_NWSE_PrimaryColorNoise= " & Cursor_NWSE_PrimaryColorNoise)
                tx.Add("*Cursor_NWSE_PrimaryColorNoiseOpacity= " & Cursor_NWSE_PrimaryColorNoiseOpacity)
                tx.Add("*Cursor_NWSE_SecondaryColor1= " & Cursor_NWSE_SecondaryColor1.ToArgb)
                tx.Add("*Cursor_NWSE_SecondaryColor2= " & Cursor_NWSE_SecondaryColor2.ToArgb)
                tx.Add("*Cursor_NWSE_SecondaryColorGradient= " & Cursor_NWSE_SecondaryColorGradient)
                tx.Add("*Cursor_NWSE_SecondaryColorGradientMode= " & Cursor_NWSE_SecondaryColorGradientMode)
                tx.Add("*Cursor_NWSE_SecondaryColorNoise= " & Cursor_NWSE_SecondaryColorNoise)
                tx.Add("*Cursor_NWSE_SecondaryColorNoiseOpacity= " & Cursor_NWSE_SecondaryColorNoiseOpacity)

#End Region

#Region "Up"
                tx.Add("*Cursor_Up_PrimaryColor1= " & Cursor_Up_PrimaryColor1.ToArgb)
                tx.Add("*Cursor_Up_PrimaryColor2= " & Cursor_Up_PrimaryColor2.ToArgb)
                tx.Add("*Cursor_Up_PrimaryColorGradient= " & Cursor_Up_PrimaryColorGradient)
                tx.Add("*Cursor_Up_PrimaryColorGradientMode= " & Cursor_Up_PrimaryColorGradientMode)
                tx.Add("*Cursor_Up_PrimaryColorNoise= " & Cursor_Up_PrimaryColorNoise)
                tx.Add("*Cursor_Up_PrimaryColorNoiseOpacity= " & Cursor_Up_PrimaryColorNoiseOpacity)
                tx.Add("*Cursor_Up_SecondaryColor1= " & Cursor_Up_SecondaryColor1.ToArgb)
                tx.Add("*Cursor_Up_SecondaryColor2= " & Cursor_Up_SecondaryColor2.ToArgb)
                tx.Add("*Cursor_Up_SecondaryColorGradient= " & Cursor_Up_SecondaryColorGradient)
                tx.Add("*Cursor_Up_SecondaryColorGradientMode= " & Cursor_Up_SecondaryColorGradientMode)
                tx.Add("*Cursor_Up_SecondaryColorNoise= " & Cursor_Up_SecondaryColorNoise)
                tx.Add("*Cursor_Up_SecondaryColorNoiseOpacity= " & Cursor_Up_SecondaryColorNoiseOpacity)

#End Region

#Region "Pen"
                tx.Add("*Cursor_Pen_PrimaryColor1= " & Cursor_Pen_PrimaryColor1.ToArgb)
                tx.Add("*Cursor_Pen_PrimaryColor2= " & Cursor_Pen_PrimaryColor2.ToArgb)
                tx.Add("*Cursor_Pen_PrimaryColorGradient= " & Cursor_Pen_PrimaryColorGradient)
                tx.Add("*Cursor_Pen_PrimaryColorGradientMode= " & Cursor_Pen_PrimaryColorGradientMode)
                tx.Add("*Cursor_Pen_PrimaryColorNoise= " & Cursor_Pen_PrimaryColorNoise)
                tx.Add("*Cursor_Pen_PrimaryColorNoiseOpacity= " & Cursor_Pen_PrimaryColorNoiseOpacity)
                tx.Add("*Cursor_Pen_SecondaryColor1= " & Cursor_Pen_SecondaryColor1.ToArgb)
                tx.Add("*Cursor_Pen_SecondaryColor2= " & Cursor_Pen_SecondaryColor2.ToArgb)
                tx.Add("*Cursor_Pen_SecondaryColorGradient= " & Cursor_Pen_SecondaryColorGradient)
                tx.Add("*Cursor_Pen_SecondaryColorGradientMode= " & Cursor_Pen_SecondaryColorGradientMode)
                tx.Add("*Cursor_Pen_SecondaryColorNoise= " & Cursor_Pen_SecondaryColorNoise)
                tx.Add("*Cursor_Pen_SecondaryColorNoiseOpacity= " & Cursor_Pen_SecondaryColorNoiseOpacity)

#End Region

#Region "None"
                tx.Add("*Cursor_None_PrimaryColor1= " & Cursor_None_PrimaryColor1.ToArgb)
                tx.Add("*Cursor_None_PrimaryColor2= " & Cursor_None_PrimaryColor2.ToArgb)
                tx.Add("*Cursor_None_PrimaryColorGradient= " & Cursor_None_PrimaryColorGradient)
                tx.Add("*Cursor_None_PrimaryColorGradientMode= " & Cursor_None_PrimaryColorGradientMode)
                tx.Add("*Cursor_None_PrimaryColorNoise= " & Cursor_None_PrimaryColorNoise)
                tx.Add("*Cursor_None_PrimaryColorNoiseOpacity= " & Cursor_None_PrimaryColorNoiseOpacity)
                tx.Add("*Cursor_None_SecondaryColor1= " & Cursor_None_SecondaryColor1.ToArgb)
                tx.Add("*Cursor_None_SecondaryColor2= " & Cursor_None_SecondaryColor2.ToArgb)
                tx.Add("*Cursor_None_SecondaryColorGradient= " & Cursor_None_SecondaryColorGradient)
                tx.Add("*Cursor_None_SecondaryColorGradientMode= " & Cursor_None_SecondaryColorGradientMode)
                tx.Add("*Cursor_None_SecondaryColorNoise= " & Cursor_None_SecondaryColorNoise)
                tx.Add("*Cursor_None_SecondaryColorNoiseOpacity= " & Cursor_None_SecondaryColorNoiseOpacity)

#End Region

#Region "Link"
                tx.Add("*Cursor_Link_PrimaryColor1= " & Cursor_Link_PrimaryColor1.ToArgb)
                tx.Add("*Cursor_Link_PrimaryColor2= " & Cursor_Link_PrimaryColor2.ToArgb)
                tx.Add("*Cursor_Link_PrimaryColorGradient= " & Cursor_Link_PrimaryColorGradient)
                tx.Add("*Cursor_Link_PrimaryColorGradientMode= " & Cursor_Link_PrimaryColorGradientMode)
                tx.Add("*Cursor_Link_PrimaryColorNoise= " & Cursor_Link_PrimaryColorNoise)
                tx.Add("*Cursor_Link_PrimaryColorNoiseOpacity= " & Cursor_Link_PrimaryColorNoiseOpacity)
                tx.Add("*Cursor_Link_SecondaryColor1= " & Cursor_Link_SecondaryColor1.ToArgb)
                tx.Add("*Cursor_Link_SecondaryColor2= " & Cursor_Link_SecondaryColor2.ToArgb)
                tx.Add("*Cursor_Link_SecondaryColorGradient= " & Cursor_Link_SecondaryColorGradient)
                tx.Add("*Cursor_Link_SecondaryColorGradientMode= " & Cursor_Link_SecondaryColorGradientMode)
                tx.Add("*Cursor_Link_SecondaryColorNoise= " & Cursor_Link_SecondaryColorNoise)
                tx.Add("*Cursor_Link_SecondaryColorNoiseOpacity= " & Cursor_Link_SecondaryColorNoiseOpacity)

#End Region

#Region "Pin"
                tx.Add("*Cursor_Pin_PrimaryColor1= " & Cursor_Pin_PrimaryColor1.ToArgb)
                tx.Add("*Cursor_Pin_PrimaryColor2= " & Cursor_Pin_PrimaryColor2.ToArgb)
                tx.Add("*Cursor_Pin_PrimaryColorGradient= " & Cursor_Pin_PrimaryColorGradient)
                tx.Add("*Cursor_Pin_PrimaryColorGradientMode= " & Cursor_Pin_PrimaryColorGradientMode)
                tx.Add("*Cursor_Pin_PrimaryColorNoise= " & Cursor_Pin_PrimaryColorNoise)
                tx.Add("*Cursor_Pin_PrimaryColorNoiseOpacity= " & Cursor_Pin_PrimaryColorNoiseOpacity)
                tx.Add("*Cursor_Pin_SecondaryColor1= " & Cursor_Pin_SecondaryColor1.ToArgb)
                tx.Add("*Cursor_Pin_SecondaryColor2= " & Cursor_Pin_SecondaryColor2.ToArgb)
                tx.Add("*Cursor_Pin_SecondaryColorGradient= " & Cursor_Pin_SecondaryColorGradient)
                tx.Add("*Cursor_Pin_SecondaryColorGradientMode= " & Cursor_Pin_SecondaryColorGradientMode)
                tx.Add("*Cursor_Pin_SecondaryColorNoise= " & Cursor_Pin_SecondaryColorNoise)
                tx.Add("*Cursor_Pin_SecondaryColorNoiseOpacity= " & Cursor_Pin_SecondaryColorNoiseOpacity)

#End Region

#Region "Person"
                tx.Add("*Cursor_Person_PrimaryColor1= " & Cursor_Person_PrimaryColor1.ToArgb)
                tx.Add("*Cursor_Person_PrimaryColor2= " & Cursor_Person_PrimaryColor2.ToArgb)
                tx.Add("*Cursor_Person_PrimaryColorGradient= " & Cursor_Person_PrimaryColorGradient)
                tx.Add("*Cursor_Person_PrimaryColorGradientMode= " & Cursor_Person_PrimaryColorGradientMode)
                tx.Add("*Cursor_Person_PrimaryColorNoise= " & Cursor_Person_PrimaryColorNoise)
                tx.Add("*Cursor_Person_PrimaryColorNoiseOpacity= " & Cursor_Person_PrimaryColorNoiseOpacity)
                tx.Add("*Cursor_Person_SecondaryColor1= " & Cursor_Person_SecondaryColor1.ToArgb)
                tx.Add("*Cursor_Person_SecondaryColor2= " & Cursor_Person_SecondaryColor2.ToArgb)
                tx.Add("*Cursor_Person_SecondaryColorGradient= " & Cursor_Person_SecondaryColorGradient)
                tx.Add("*Cursor_Person_SecondaryColorGradientMode= " & Cursor_Person_SecondaryColorGradientMode)
                tx.Add("*Cursor_Person_SecondaryColorNoise= " & Cursor_Person_SecondaryColorNoise)
                tx.Add("*Cursor_Person_SecondaryColorNoiseOpacity= " & Cursor_Person_SecondaryColorNoiseOpacity)

#End Region

#Region "Cross"
                tx.Add("*Cursor_Cross_PrimaryColor1= " & Cursor_Cross_PrimaryColor1.ToArgb)
                tx.Add("*Cursor_Cross_PrimaryColor2= " & Cursor_Cross_PrimaryColor2.ToArgb)
                tx.Add("*Cursor_Cross_PrimaryColorGradient= " & Cursor_Cross_PrimaryColorGradient)
                tx.Add("*Cursor_Cross_PrimaryColorGradientMode= " & Cursor_Cross_PrimaryColorGradientMode)
                tx.Add("*Cursor_Cross_PrimaryColorNoise= " & Cursor_Cross_PrimaryColorNoise)
                tx.Add("*Cursor_Cross_PrimaryColorNoiseOpacity= " & Cursor_Cross_PrimaryColorNoiseOpacity)
                tx.Add("*Cursor_Cross_SecondaryColor1= " & Cursor_Cross_SecondaryColor1.ToArgb)
                tx.Add("*Cursor_Cross_SecondaryColor2= " & Cursor_Cross_SecondaryColor2.ToArgb)
                tx.Add("*Cursor_Cross_SecondaryColorGradient= " & Cursor_Cross_SecondaryColorGradient)
                tx.Add("*Cursor_Cross_SecondaryColorGradientMode= " & Cursor_Cross_SecondaryColorGradientMode)
                tx.Add("*Cursor_Cross_SecondaryColorNoise= " & Cursor_Cross_SecondaryColorNoise)
                tx.Add("*Cursor_Cross_SecondaryColorNoiseOpacity= " & Cursor_Cross_SecondaryColorNoiseOpacity)

#End Region
                tx.Add("</Cursors>")
#End Region

                tx.Add(vbCrLf & "</WinPaletter>")

                IO.File.WriteAllText(FileLocation, CStr_FromList(tx))
        End Select

    End Sub

    Function HexStringToBinary(ByVal hexString As String) As String
        Dim num As Integer = Integer.Parse(hexString, NumberStyles.HexNumber)
        Return Convert.ToString(num, 2)
    End Function

    Sub EditReg(KeyName As String, ValueName As String, Value As Object, Optional ByVal Binary As Boolean = False, Optional ByVal [String] As Boolean = False)
        Dim R As RegistryKey = Nothing

        If KeyName.Contains("HKEY_CURRENT_USER") Then
            R = Registry.CurrentUser
            KeyName = KeyName.Remove(0, "HKEY_CURRENT_USER\".Count)
        ElseIf KeyName.Contains("HKEY_LOCAL_MACHINE") Then
            R = Registry.LocalMachine
            KeyName = KeyName.Remove(0, "HKEY_LOCAL_MACHINE\".Count)
        End If

        Try
            If Binary Then
                R.OpenSubKey(KeyName, True).SetValue(ValueName, Value, RegistryValueKind.Binary)
            ElseIf [String] Then
                R.OpenSubKey(KeyName, True).SetValue(ValueName, Value, RegistryValueKind.String)
            Else
                R.OpenSubKey(KeyName, True).SetValue(ValueName, Value, RegistryValueKind.DWord)
            End If
        Catch
            'MainFrm.status_lbl.Text = "Error in applying values of LogonUI. Restart the application as an Administrator and try again."
        Finally
            If R IsNot Nothing Then
                R.Flush()
                R.Close()
            End If
        End Try

        Try
            R.Flush()
            R.Close()
        Catch
        End Try

    End Sub

    Enum SavingMode
        Registry
        File
    End Enum

    Function RGB2HEX(ByVal [Color] As Color) As List(Of String)
        Dim sb As New List(Of String)
        sb.Clear()

        sb.Add(String.Format("{0:X2}", Color.A, Color.R, Color.G, Color.B))
        sb.Add(String.Format("{1:X2}", Color.A, Color.R, Color.G, Color.B))
        sb.Add(String.Format("{2:X2}", Color.A, Color.R, Color.G, Color.B))
        sb.Add(String.Format("{3:X2}", Color.A, Color.R, Color.G, Color.B))

        Return sb
    End Function

    Function RGB2HEX_oneline(ByVal [Color] As Color) As String
        Return String.Format("{0:X2}", Color.A, Color.R, Color.G, Color.B) & String.Format("{1:X2}", Color.A, Color.R, Color.G, Color.B) &
            String.Format("{2:X2}", Color.A, Color.R, Color.G, Color.B) & String.Format("{3:X2}", Color.A, Color.R, Color.G, Color.B)
    End Function

    Function BizareColorInvertor([Color] As Color) As Color
        Return Color.FromArgb([Color].B, [Color].G, [Color].R)
    End Function

    Public Shared Function GetPaletteFromMSTheme(Filename As String) As List(Of Color)
        If IO.File.Exists(Filename) Then

            Dim ls As New List(Of Color)
            ls.Clear()

            Dim tx As New List(Of String)
            CList_FromStr(tx, IO.File.ReadAllText(Filename))

            For Each x As String In tx
                Try
                    If x.Contains("=") Then
                        If x.Split("=")(1).Contains(" ") Then
                            If x.Split("=")(1).Split(" ").Count = 3 Then
                                Dim c As String = x.Split("=")(1)
                                Dim inx As Boolean = True
                                For Each u In c.Split(" ")
                                    If Not IsNumeric(u) Then inx = False
                                Next
                                If inx Then ls.Add(Color.FromArgb(255, c.Split(" ")(0), c.Split(" ")(1), c.Split(" ")(2)))
                            End If
                        End If
                    End If
                Catch
                End Try
            Next

            Return ls.Distinct.ToList
        Else
            Return Nothing
        End If
    End Function

    Public Overrides Function Equals(obj As Object) As Boolean
        'obj = TryCast(obj, CP)

        Dim _Equals As Boolean = True

        Dim type1 As Type = [GetType]() : Dim properties1 As PropertyInfo() = type1.GetProperties()
        Dim type2 As Type = obj.[GetType]() : Dim properties2 As PropertyInfo() = type2.GetProperties()

        For Each [property] As PropertyInfo In properties1
            If [property].GetValue(Me, Nothing) <> [property].GetValue(obj, Nothing) Then
                _Equals = False
                Exit For
            End If
        Next

        Return _Equals
    End Function

End Class

