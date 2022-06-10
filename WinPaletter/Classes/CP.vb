Imports System.Globalization
Imports System.Security.Principal
Imports Microsoft.Win32
Imports WinPaletter.XenonCore
Imports System.Reflection

Public Class CP
    ReadOnly isElevated As Boolean = New WindowsPrincipal(WindowsIdentity.GetCurrent).IsInRole(WindowsBuiltInRole.Administrator)
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

    Public Property WinMode_Light As Boolean
    Public Property AppMode_Light As Boolean
    Public Property Transparency As Boolean
    Public Property ApplyAccentonTitlebars As Boolean
    Public Property ApplyAccentonTaskbar As Boolean
    Public Property AppVersion As String
    Public Property PaletteName As String
    Public Property PaletteDescription As String
    Public Property PaletteVersion As String
    Public Property Author As String
    Public Property AuthorSocialMediaLink As String

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
                PaletteName = "Current Mode"

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
                    IO.File.WriteAllText("tempreg.reg", result)

                    Dim process As Process = Nothing

                    Dim processStartInfo As New ProcessStartInfo With {
                       .FileName = "regedit",
                       .Verb = "runas",
                       .Arguments = String.Format("/s ""{0}\tempreg.reg""", My.Application.Info.DirectoryPath),
                       .WindowStyle = ProcessWindowStyle.Hidden,
                       .CreateNoWindow = True,
                       .UseShellExecute = True
                    }
                    process = Process.Start(processStartInfo)
                    process.WaitForExit()
                    processStartInfo.FileName = "reg"
                    processStartInfo.Arguments = String.Format("import ""{0}\tempreg.reg""", My.Application.Info.DirectoryPath)
                    process = Process.Start(processStartInfo)
                    process.WaitForExit()
                    Kill("tempreg.reg")
                End If


#End Region

#Region "Win32UI"
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

            Case SavingMode.File

                Dim tx As New List(Of String)
                tx.Clear()
                tx.Add("<WinPaletter - Programmed by Abdelrhman_AK>")
                tx.Add("*Created from App Version= " & AppVersion & vbCrLf)

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
                tx.Add("</Win32UI>" & vbCrLf)
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
            If R IsNot Nothing Then R.Close()
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


    '#### Alternative Method for comparing (less efficient)

    'Public Overrides Function Equals(obj As Object) As Boolean
    'obj = TryCast(obj, CP)
    'Dim LS1, LS2 As New Dictionary(Of String, Object)
    'LS1.Clear() : LS2.Clear()
    'Dim type1 As Type = [GetType]() : Dim properties1 As PropertyInfo() = type1.GetProperties()
    'Dim type2 As Type = obj.[GetType]() : Dim properties2 As PropertyInfo() = type2.GetProperties()
    'For Each [property] As PropertyInfo In properties1
    'LS1.Add([property].Name, [property].GetValue(Me, Nothing))
    'Next
    'For Each [property] As PropertyInfo In properties2
    'LS2.Add([property].Name, [property].GetValue(obj, Nothing))
    'Next
    'Return LS1.SequenceEqual(LS2)
    'End Function

End Class

