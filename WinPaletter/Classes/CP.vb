Imports System.Runtime.InteropServices
Imports Microsoft.Win32
Imports WinPaletter.Metrics
Imports WinPaletter.XenonCore

Public Class CP

#Region "Properties"

#Region "General Info"
    Public Property AppVersion As String = My.Application.Info.Version.ToString
    Public Property PaletteName As String = "Current Mode"
    Public Property PaletteDescription As String
    Public Property PaletteVersion As String = "1.0.0.0"
    Public Property Author As String = Environment.UserName
    Public Property AuthorSocialMediaLink As String
#End Region

#Region "ModernWindows"
    Public Property Titlebar_Active As Color
    Public Property Titlebar_Inactive As Color
    Public Property StartMenu_Accent As Color
    Public Property StartButton_Hover As Color
    Public Property Taskbar_Background As Color
    Public Property Taskbar_Icon_Underline As Color
    Public Property StartMenuBackground_ActiveTaskbarButton As Color
    Public Property StartListFolders_TaskbarFront As Color
    Public Property ActionCenter_AppsLinks As Color
    Public Property SettingsIconsAndLinks As Color
    Public Property UWP_Undefined As Color = Color.Black
    Public Property WinMode_Light As Boolean
    Public Property AppMode_Light As Boolean
    Public Property Transparency As Boolean = True
    Public Property ApplyAccentonTitlebars As Boolean = False
    Public Property ApplyAccentonTaskbar As ApplyAccentonTaskbar_Level = 0
#End Region

    Enum ApplyAccentonTaskbar_Level
        None
        Taskbar_Start_AC
        Taskbar
    End Enum

#Region "Aero"
    Public Property Aero_ColorizationColor As Color
    Public Property Aero_ColorizationAfterglow As Color
    Public Property Aero_EnableAeroPeek As Boolean = True
    Public Property Aero_AlwaysHibernateThumbnails As Boolean = False
    Public Property Aero_ColorizationColorBalance As Integer = 8
    Public Property Aero_ColorizationAfterglowBalance As Integer = 31
    Public Property Aero_ColorizationBlurBalance As Integer = 31
    Public Property Aero_ColorizationGlassReflectionIntensity As Integer
    Public Property Aero_Theme As AeroTheme = AeroTheme.Aero

    Public Enum AeroTheme
        Aero
        AeroLite
        AeroOpaque
        Basic
        Classic
    End Enum

#End Region

#Region "Metro"
    Public Property Metro_Start As Integer = 0
    Public Property Metro_StartColor As Color = Color.FromArgb(30, 0, 84)
    Public Property Metro_AccentColor As Color = Color.FromArgb(72, 29, 178)
    Public Property Metro_Theme As AeroTheme = AeroTheme.Aero
    Public Property Metro_LogonUI As Integer = 0
    Public Property Metro_PersonalColors_Background As Color = Color.FromArgb(30, 0, 84)
    Public Property Metro_PersonalColors_Accent As Color = Color.FromArgb(72, 29, 178)
    Public Property Metro_NoLockScreen As Boolean = False
    Public Property Metro_LockScreenType As LogonUI_Modes = LogonUI_Modes.Default_
    Public Property Metro_LockScreenSystemID As Integer = 0
#End Region

#Region "LogonUI_Win10"
    Public Property LogonUI_DisableAcrylicBackgroundOnLogon As Boolean = False
    Public Property LogonUI_DisableLogonBackgroundImage As Boolean = False
    Public Property LogonUI_NoLockScreen As Boolean = False
#End Region

#Region "LogonUI_Win7"
    Public Property LogonUI7_Enabled As Boolean = False
    Public Property LogonUI7_Mode As LogonUI_Modes = LogonUI_Modes.Default_
    Public Property LogonUI7_ImagePath As String = "C:\Windows\Web\Wallpaper\Windows\img0.jpg"
    Public Property LogonUI7_Color As Color = Color.Black
    Public Property LogonUI7_Effect_Blur As Boolean = False
    Public Property LogonUI7_Effect_Blur_Intensity As Integer = 0
    Public Property LogonUI7_Effect_Grayscale As Boolean = False
    Public Property LogonUI7_Effect_Noise As Boolean = False
    Public Property LogonUI7_Effect_Noise_Mode As LogonUI7_NoiseMode = LogonUI7_NoiseMode.Acrylic
    Public Property LogonUI7_Effect_Noise_Intensity As Integer = 0
#End Region

#Region "Win32UI"
    Public Property Win32UI_EnableTheming As Boolean = True
    Public Property Win32UI_EnableGradient As Boolean = True
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

#Region "Terminals"

#Region "Locking"
    Public Property Terminal_CMD_Enabled As Boolean = False
    Public Property Terminal_PS_32_Enabled As Boolean = False
    Public Property Terminal_PS_64_Enabled As Boolean = False
    Public Property Terminal_Stable_Enabled As Boolean = False
    Public Property Terminal_Preview_Enabled As Boolean = False
#End Region

#Region "Command Prompt"
    Public Property CMD_ColorTable00 As Color = Color.FromArgb(12, 12, 12)
    Public Property CMD_ColorTable01 As Color = Color.FromArgb(0, 55, 218)
    Public Property CMD_ColorTable02 As Color = Color.FromArgb(19, 161, 14)
    Public Property CMD_ColorTable03 As Color = Color.FromArgb(58, 150, 221)
    Public Property CMD_ColorTable04 As Color = Color.FromArgb(197, 15, 31)
    Public Property CMD_ColorTable05 As Color = Color.FromArgb(136, 23, 152)
    Public Property CMD_ColorTable06 As Color = Color.FromArgb(193, 156, 0)
    Public Property CMD_ColorTable07 As Color = Color.FromArgb(204, 204, 204)
    Public Property CMD_ColorTable08 As Color = Color.FromArgb(118, 118, 118)
    Public Property CMD_ColorTable09 As Color = Color.FromArgb(59, 120, 255)
    Public Property CMD_ColorTable10 As Color = Color.FromArgb(22, 198, 12)
    Public Property CMD_ColorTable11 As Color = Color.FromArgb(97, 214, 214)
    Public Property CMD_ColorTable12 As Color = Color.FromArgb(231, 72, 86)
    Public Property CMD_ColorTable13 As Color = Color.FromArgb(180, 0, 158)
    Public Property CMD_ColorTable14 As Color = Color.FromArgb(249, 241, 165)
    Public Property CMD_ColorTable15 As Color = Color.FromArgb(242, 242, 242)
    Public Property CMD_PopupForeground As Integer = 15
    Public Property CMD_PopupBackground As Integer = 5
    Public Property CMD_ScreenColorsForeground As Integer = 7
    Public Property CMD_ScreenColorsBackground As Integer = 0
    Public Property CMD_CursorSize As Integer = 19
    Public Property CMD_FaceName As String = "Consolas"
    Public Property CMD_FontRaster As Boolean = False
    Public Property CMD_FontSize As Integer = 18 * 65536
    Public Property CMD_FontWeight As Integer = 400
    Public Property CMD_1909_CursorType As Integer = 0
    Public Property CMD_1909_CursorColor As Color = Color.White
    Public Property CMD_1909_ForceV2 As Boolean = True
    Public Property CMD_1909_LineSelection As Boolean = False
    Public Property CMD_1909_TerminalScrolling As Boolean = False
    Public Property CMD_1909_WindowAlpha As Integer = 255
#End Region

#Region "PowerShell 32-bit"
    Public Property PS_32_ColorTable00 As Color = Color.FromArgb(12, 12, 12)
    Public Property PS_32_ColorTable01 As Color = Color.FromArgb(0, 55, 218)
    Public Property PS_32_ColorTable02 As Color = Color.FromArgb(19, 161, 14)
    Public Property PS_32_ColorTable03 As Color = Color.FromArgb(58, 150, 221)
    Public Property PS_32_ColorTable04 As Color = Color.FromArgb(197, 15, 31)
    Public Property PS_32_ColorTable05 As Color = Color.FromArgb(1, 36, 86)
    Public Property PS_32_ColorTable06 As Color = Color.FromArgb(238, 237, 240)
    Public Property PS_32_ColorTable07 As Color = Color.FromArgb(204, 204, 204)
    Public Property PS_32_ColorTable08 As Color = Color.FromArgb(118, 118, 118)
    Public Property PS_32_ColorTable09 As Color = Color.FromArgb(59, 120, 255)
    Public Property PS_32_ColorTable10 As Color = Color.FromArgb(22, 198, 12)
    Public Property PS_32_ColorTable11 As Color = Color.FromArgb(97, 214, 214)
    Public Property PS_32_ColorTable12 As Color = Color.FromArgb(231, 72, 86)
    Public Property PS_32_ColorTable13 As Color = Color.FromArgb(180, 0, 158)
    Public Property PS_32_ColorTable14 As Color = Color.FromArgb(249, 241, 165)
    Public Property PS_32_ColorTable15 As Color = Color.FromArgb(242, 242, 242)
    Public Property PS_32_PopupForeground As Integer = 15
    Public Property PS_32_PopupBackground As Integer = 3
    Public Property PS_32_ScreenColorsForeground As Integer = 6
    Public Property PS_32_ScreenColorsBackground As Integer = 5
    Public Property PS_32_CursorSize As Integer = 19
    Public Property PS_32_FaceName As String = "Consolas"
    Public Property PS_32_FontRaster As Boolean = False
    Public Property PS_32_FontSize As Integer = 16 * 65536
    Public Property PS_32_FontWeight As Integer = 400
    Public Property PS_32_1909_CursorType As Integer = 0
    Public Property PS_32_1909_CursorColor As Color = Color.White
    Public Property PS_32_1909_ForceV2 As Boolean = True
    Public Property PS_32_1909_LineSelection As Boolean = False
    Public Property PS_32_1909_TerminalScrolling As Boolean = False
    Public Property PS_32_1909_WindowAlpha As Integer = 255
#End Region

#Region "PowerShell 64-bit"
    Public Property PS_64_ColorTable00 As Color = Color.FromArgb(12, 12, 12)
    Public Property PS_64_ColorTable01 As Color = Color.FromArgb(0, 55, 218)
    Public Property PS_64_ColorTable02 As Color = Color.FromArgb(19, 161, 14)
    Public Property PS_64_ColorTable03 As Color = Color.FromArgb(58, 150, 221)
    Public Property PS_64_ColorTable04 As Color = Color.FromArgb(197, 15, 31)
    Public Property PS_64_ColorTable05 As Color = Color.FromArgb(1, 36, 86)
    Public Property PS_64_ColorTable06 As Color = Color.FromArgb(238, 237, 240)
    Public Property PS_64_ColorTable07 As Color = Color.FromArgb(204, 204, 204)
    Public Property PS_64_ColorTable08 As Color = Color.FromArgb(118, 118, 118)
    Public Property PS_64_ColorTable09 As Color = Color.FromArgb(59, 120, 255)
    Public Property PS_64_ColorTable10 As Color = Color.FromArgb(22, 198, 12)
    Public Property PS_64_ColorTable11 As Color = Color.FromArgb(97, 214, 214)
    Public Property PS_64_ColorTable12 As Color = Color.FromArgb(231, 72, 86)
    Public Property PS_64_ColorTable13 As Color = Color.FromArgb(180, 0, 158)
    Public Property PS_64_ColorTable14 As Color = Color.FromArgb(249, 241, 165)
    Public Property PS_64_ColorTable15 As Color = Color.FromArgb(242, 242, 242)
    Public Property PS_64_PopupForeground As Integer = 15
    Public Property PS_64_PopupBackground As Integer = 3
    Public Property PS_64_ScreenColorsForeground As Integer = 6
    Public Property PS_64_ScreenColorsBackground As Integer = 5
    Public Property PS_64_CursorSize As Integer = 19
    Public Property PS_64_FaceName As String = "Consolas"
    Public Property PS_64_FontRaster As Boolean = False
    Public Property PS_64_FontSize As Integer = 16 * 65536
    Public Property PS_64_FontWeight As Integer = 400
    Public Property PS_64_1909_CursorType As Integer = 0
    Public Property PS_64_1909_CursorColor As Color = Color.White
    Public Property PS_64_1909_ForceV2 As Boolean = True
    Public Property PS_64_1909_LineSelection As Boolean = False
    Public Property PS_64_1909_TerminalScrolling As Boolean = False
    Public Property PS_64_1909_WindowAlpha As Integer = 255
#End Region

#Region "Windows Terminal"
    Public Terminal As WinTerminal
    Public TerminalPreview As WinTerminal
    'Public TerminalDeveloper As WinTerminal
#End Region
#End Region

#Region "Metrics"
    Public Property Metrics_BorderWidth As Integer
    Public Property Metrics_CaptionHeight As Integer
    Public Property Metrics_CaptionWidth As Integer
    Public Property Metrics_IconSpacing As Integer
    Public Property Metrics_IconVerticalSpacing As Integer
    Public Property Metrics_MenuHeight As Integer
    Public Property Metrics_MenuWidth As Integer
    Public Property Metrics_MinAnimate As Boolean
    Public Property Metrics_PaddedBorderWidth As Integer
    Public Property Metrics_ScrollHeight As Integer
    Public Property Metrics_ScrollWidth As Integer
    Public Property Metrics_SmCaptionHeight As Integer
    Public Property Metrics_SmCaptionWidth As Integer
    Public Property Metrics_DesktopIconSize As Integer = 32
    Public Property Metrics_ShellIconSize As Integer
#End Region

#Region "Fonts"
    Public Property Fonts_CaptionFont As New Font("Segoe UI", 12, FontStyle.Regular)
    Public Property Fonts_IconFont As New Font("Segoe UI", 12, FontStyle.Regular)
    Public Property Fonts_MenuFont As New Font("Segoe UI", 12, FontStyle.Regular)
    Public Property Fonts_MessageFont As New Font("Segoe UI", 12, FontStyle.Regular)
    Public Property Fonts_SmCaptionFont As New Font("Segoe UI", 12, FontStyle.Regular)
    Public Property Fonts_StatusFont As New Font("Segoe UI", 12, FontStyle.Regular)
#End Region

#Region "Cursors"
    Public Property Cursor_Enabled As Boolean = False

#Region "Arrow"
    Public Property Cursor_Arrow_PrimaryColor1 As Color = Color.White
    Public Property Cursor_Arrow_PrimaryColor2 As Color = Color.White
    Public Property Cursor_Arrow_PrimaryColorGradient As Boolean = False
    Public Property Cursor_Arrow_PrimaryColorGradientMode As GradientMode = GradientMode.Vertical
    Public Property Cursor_Arrow_PrimaryColorNoise As Boolean = False
    Public Property Cursor_Arrow_PrimaryColorNoiseOpacity As Single = 0.25
    Public Property Cursor_Arrow_SecondaryColor1 As Color = Color.FromArgb(64, 65, 75)
    Public Property Cursor_Arrow_SecondaryColor2 As Color = Color.FromArgb(64, 65, 75)
    Public Property Cursor_Arrow_SecondaryColorGradient As Boolean = False
    Public Property Cursor_Arrow_SecondaryColorGradientMode As GradientMode = GradientMode.Vertical
    Public Property Cursor_Arrow_SecondaryColorNoise As Boolean = False
    Public Property Cursor_Arrow_SecondaryColorNoiseOpacity As Single = 0.25
#End Region

#Region "Help"
    Public Property Cursor_Help_PrimaryColor1 As Color = Color.White
    Public Property Cursor_Help_PrimaryColor2 As Color = Color.White
    Public Property Cursor_Help_PrimaryColorGradient As Boolean = False
    Public Property Cursor_Help_PrimaryColorGradientMode As GradientMode = GradientMode.Vertical
    Public Property Cursor_Help_PrimaryColorNoise As Boolean = False
    Public Property Cursor_Help_PrimaryColorNoiseOpacity As Single = 0.25
    Public Property Cursor_Help_SecondaryColor1 As Color = Color.FromArgb(64, 65, 75)
    Public Property Cursor_Help_SecondaryColor2 As Color = Color.FromArgb(64, 65, 75)
    Public Property Cursor_Help_SecondaryColorGradient As Boolean = False
    Public Property Cursor_Help_SecondaryColorGradientMode As GradientMode = GradientMode.Vertical
    Public Property Cursor_Help_SecondaryColorNoise As Boolean = False
    Public Property Cursor_Help_SecondaryColorNoiseOpacity As Single = 0.25
#End Region

#Region "AppLoading"
    Public Property Cursor_AppLoading_PrimaryColor1 As Color = Color.White
    Public Property Cursor_AppLoading_PrimaryColor2 As Color = Color.White
    Public Property Cursor_AppLoading_PrimaryColorGradient As Boolean = False
    Public Property Cursor_AppLoading_PrimaryColorGradientMode As GradientMode = GradientMode.Circle
    Public Property Cursor_AppLoading_PrimaryColorNoise As Boolean = False
    Public Property Cursor_AppLoading_PrimaryColorNoiseOpacity As Single = 0.25
    Public Property Cursor_AppLoading_SecondaryColor1 As Color = Color.FromArgb(64, 65, 75)
    Public Property Cursor_AppLoading_SecondaryColor2 As Color = Color.FromArgb(64, 65, 75)
    Public Property Cursor_AppLoading_SecondaryColorGradient As Boolean = False
    Public Property Cursor_AppLoading_SecondaryColorGradientMode As GradientMode = GradientMode.Circle
    Public Property Cursor_AppLoading_SecondaryColorNoise As Boolean = False
    Public Property Cursor_AppLoading_SecondaryColorNoiseOpacity As Single = 0.25
    Public Property Cursor_AppLoading_LoadingCircleBack1 As Color = Color.FromArgb(42, 151, 243)
    Public Property Cursor_AppLoading_LoadingCircleBack2 As Color = Color.FromArgb(42, 151, 243)
    Public Property Cursor_AppLoading_LoadingCircleBackGradient As Boolean = False
    Public Property Cursor_AppLoading_LoadingCircleBackGradientMode As GradientMode = GradientMode.Circle
    Public Property Cursor_AppLoading_LoadingCircleBackNoise As Boolean = False
    Public Property Cursor_AppLoading_LoadingCircleBackNoiseOpacity As Single = 0.25
    Public Property Cursor_AppLoading_LoadingCircleHot1 As Color = Color.FromArgb(37, 204, 255)
    Public Property Cursor_AppLoading_LoadingCircleHot2 As Color = Color.FromArgb(37, 204, 255)
    Public Property Cursor_AppLoading_LoadingCircleHotGradient As Boolean = False
    Public Property Cursor_AppLoading_LoadingCircleHotGradientMode As GradientMode = GradientMode.Circle
    Public Property Cursor_AppLoading_LoadingCircleHotNoise As Boolean = False
    Public Property Cursor_AppLoading_LoadingCircleHotNoiseOpacity As Single = 0.25
#End Region

#Region "Busy"
    Public Property Cursor_Busy_LoadingCircleBack1 As Color = Color.FromArgb(42, 151, 243)
    Public Property Cursor_Busy_LoadingCircleBack2 As Color = Color.FromArgb(42, 151, 243)
    Public Property Cursor_Busy_LoadingCircleBackGradient As Boolean = False
    Public Property Cursor_Busy_LoadingCircleBackGradientMode As GradientMode = GradientMode.Circle
    Public Property Cursor_Busy_LoadingCircleBackNoise As Boolean = False
    Public Property Cursor_Busy_LoadingCircleBackNoiseOpacity As Single = 0.25
    Public Property Cursor_Busy_LoadingCircleHot1 As Color = Color.FromArgb(37, 204, 255)
    Public Property Cursor_Busy_LoadingCircleHot2 As Color = Color.FromArgb(37, 204, 255)
    Public Property Cursor_Busy_LoadingCircleHotGradient As Boolean = False
    Public Property Cursor_Busy_LoadingCircleHotGradientMode As GradientMode = GradientMode.Circle
    Public Property Cursor_Busy_LoadingCircleHotNoise As Boolean = False
    Public Property Cursor_Busy_LoadingCircleHotNoiseOpacity As Single = 0.25
#End Region

#Region "Move"
    Public Property Cursor_Move_PrimaryColor1 As Color = Color.White
    Public Property Cursor_Move_PrimaryColor2 As Color = Color.White
    Public Property Cursor_Move_PrimaryColorGradient As Boolean = False
    Public Property Cursor_Move_PrimaryColorGradientMode As GradientMode = GradientMode.Vertical
    Public Property Cursor_Move_PrimaryColorNoise As Boolean = False
    Public Property Cursor_Move_PrimaryColorNoiseOpacity As Single = 0.25
    Public Property Cursor_Move_SecondaryColor1 As Color = Color.FromArgb(64, 65, 75)
    Public Property Cursor_Move_SecondaryColor2 As Color = Color.FromArgb(64, 65, 75)
    Public Property Cursor_Move_SecondaryColorGradient As Boolean = False
    Public Property Cursor_Move_SecondaryColorGradientMode As GradientMode = GradientMode.Vertical
    Public Property Cursor_Move_SecondaryColorNoise As Boolean = False
    Public Property Cursor_Move_SecondaryColorNoiseOpacity As Single = 0.25
#End Region

#Region "NS"
    Public Property Cursor_NS_PrimaryColor1 As Color = Color.White
    Public Property Cursor_NS_PrimaryColor2 As Color = Color.White
    Public Property Cursor_NS_PrimaryColorGradient As Boolean = False
    Public Property Cursor_NS_PrimaryColorGradientMode As GradientMode = GradientMode.Vertical
    Public Property Cursor_NS_PrimaryColorNoise As Boolean = False
    Public Property Cursor_NS_PrimaryColorNoiseOpacity As Single = 0.25
    Public Property Cursor_NS_SecondaryColor1 As Color = Color.FromArgb(64, 65, 75)
    Public Property Cursor_NS_SecondaryColor2 As Color = Color.FromArgb(64, 65, 75)
    Public Property Cursor_NS_SecondaryColorGradient As Boolean = False
    Public Property Cursor_NS_SecondaryColorGradientMode As GradientMode = GradientMode.Vertical
    Public Property Cursor_NS_SecondaryColorNoise As Boolean = False
    Public Property Cursor_NS_SecondaryColorNoiseOpacity As Single = 0.25
#End Region

#Region "EW"
    Public Property Cursor_EW_PrimaryColor1 As Color = Color.White
    Public Property Cursor_EW_PrimaryColor2 As Color = Color.White
    Public Property Cursor_EW_PrimaryColorGradient As Boolean = False
    Public Property Cursor_EW_PrimaryColorGradientMode As GradientMode = GradientMode.Vertical
    Public Property Cursor_EW_PrimaryColorNoise As Boolean = False
    Public Property Cursor_EW_PrimaryColorNoiseOpacity As Single = 0.25
    Public Property Cursor_EW_SecondaryColor1 As Color = Color.FromArgb(64, 65, 75)
    Public Property Cursor_EW_SecondaryColor2 As Color = Color.FromArgb(64, 65, 75)
    Public Property Cursor_EW_SecondaryColorGradient As Boolean = False
    Public Property Cursor_EW_SecondaryColorGradientMode As GradientMode = GradientMode.Vertical
    Public Property Cursor_EW_SecondaryColorNoise As Boolean = False
    Public Property Cursor_EW_SecondaryColorNoiseOpacity As Single = 0.25
#End Region

#Region "NESW"
    Public Property Cursor_NESW_PrimaryColor1 As Color = Color.White
    Public Property Cursor_NESW_PrimaryColor2 As Color = Color.White
    Public Property Cursor_NESW_PrimaryColorGradient As Boolean = False
    Public Property Cursor_NESW_PrimaryColorGradientMode As GradientMode = GradientMode.Vertical
    Public Property Cursor_NESW_PrimaryColorNoise As Boolean = False
    Public Property Cursor_NESW_PrimaryColorNoiseOpacity As Single = 0.25
    Public Property Cursor_NESW_SecondaryColor1 As Color = Color.FromArgb(64, 65, 75)
    Public Property Cursor_NESW_SecondaryColor2 As Color = Color.FromArgb(64, 65, 75)
    Public Property Cursor_NESW_SecondaryColorGradient As Boolean = False
    Public Property Cursor_NESW_SecondaryColorGradientMode As GradientMode = GradientMode.Vertical
    Public Property Cursor_NESW_SecondaryColorNoise As Boolean = False
    Public Property Cursor_NESW_SecondaryColorNoiseOpacity As Single = 0.25
#End Region

#Region "NWSE"
    Public Property Cursor_NWSE_PrimaryColor1 As Color = Color.White
    Public Property Cursor_NWSE_PrimaryColor2 As Color = Color.White
    Public Property Cursor_NWSE_PrimaryColorGradient As Boolean = False
    Public Property Cursor_NWSE_PrimaryColorGradientMode As GradientMode = GradientMode.Vertical
    Public Property Cursor_NWSE_PrimaryColorNoise As Boolean = False
    Public Property Cursor_NWSE_PrimaryColorNoiseOpacity As Single = 0.25
    Public Property Cursor_NWSE_SecondaryColor1 As Color = Color.FromArgb(64, 65, 75)
    Public Property Cursor_NWSE_SecondaryColor2 As Color = Color.FromArgb(64, 65, 75)
    Public Property Cursor_NWSE_SecondaryColorGradient As Boolean = False
    Public Property Cursor_NWSE_SecondaryColorGradientMode As GradientMode = GradientMode.Vertical
    Public Property Cursor_NWSE_SecondaryColorNoise As Boolean = False
    Public Property Cursor_NWSE_SecondaryColorNoiseOpacity As Single = 0.25
#End Region

#Region "Up"
    Public Property Cursor_Up_PrimaryColor1 As Color = Color.White
    Public Property Cursor_Up_PrimaryColor2 As Color = Color.White
    Public Property Cursor_Up_PrimaryColorGradient As Boolean = False
    Public Property Cursor_Up_PrimaryColorGradientMode As GradientMode = GradientMode.Vertical
    Public Property Cursor_Up_PrimaryColorNoise As Boolean = False
    Public Property Cursor_Up_PrimaryColorNoiseOpacity As Single = 0.25
    Public Property Cursor_Up_SecondaryColor1 As Color = Color.FromArgb(64, 65, 75)
    Public Property Cursor_Up_SecondaryColor2 As Color = Color.FromArgb(64, 65, 75)
    Public Property Cursor_Up_SecondaryColorGradient As Boolean = False
    Public Property Cursor_Up_SecondaryColorGradientMode As GradientMode = GradientMode.Vertical
    Public Property Cursor_Up_SecondaryColorNoise As Boolean = False
    Public Property Cursor_Up_SecondaryColorNoiseOpacity As Single = 0.25
#End Region

#Region "Pen"
    Public Property Cursor_Pen_PrimaryColor1 As Color = Color.White
    Public Property Cursor_Pen_PrimaryColor2 As Color = Color.White
    Public Property Cursor_Pen_PrimaryColorGradient As Boolean = False
    Public Property Cursor_Pen_PrimaryColorGradientMode As GradientMode = GradientMode.Vertical
    Public Property Cursor_Pen_PrimaryColorNoise As Boolean = False
    Public Property Cursor_Pen_PrimaryColorNoiseOpacity As Single = 0.25
    Public Property Cursor_Pen_SecondaryColor1 As Color = Color.FromArgb(64, 65, 75)
    Public Property Cursor_Pen_SecondaryColor2 As Color = Color.FromArgb(64, 65, 75)
    Public Property Cursor_Pen_SecondaryColorGradient As Boolean = False
    Public Property Cursor_Pen_SecondaryColorGradientMode As GradientMode = GradientMode.Vertical
    Public Property Cursor_Pen_SecondaryColorNoise As Boolean = False
    Public Property Cursor_Pen_SecondaryColorNoiseOpacity As Single = 0.25
#End Region

#Region "None"
    Public Property Cursor_None_PrimaryColor1 As Color = Color.White
    Public Property Cursor_None_PrimaryColor2 As Color = Color.White
    Public Property Cursor_None_PrimaryColorGradient As Boolean = False
    Public Property Cursor_None_PrimaryColorGradientMode As GradientMode = GradientMode.Vertical
    Public Property Cursor_None_PrimaryColorNoise As Boolean = False
    Public Property Cursor_None_PrimaryColorNoiseOpacity As Single = 0.25
    Public Property Cursor_None_SecondaryColor1 As Color = Color.Red
    Public Property Cursor_None_SecondaryColor2 As Color = Color.Red
    Public Property Cursor_None_SecondaryColorGradient As Boolean = False
    Public Property Cursor_None_SecondaryColorGradientMode As GradientMode = GradientMode.Vertical
    Public Property Cursor_None_SecondaryColorNoise As Boolean = False
    Public Property Cursor_None_SecondaryColorNoiseOpacity As Single = 0.25
#End Region

#Region "Link"
    Public Property Cursor_Link_PrimaryColor1 As Color = Color.White
    Public Property Cursor_Link_PrimaryColor2 As Color = Color.White
    Public Property Cursor_Link_PrimaryColorGradient As Boolean = False
    Public Property Cursor_Link_PrimaryColorGradientMode As GradientMode = GradientMode.Vertical
    Public Property Cursor_Link_PrimaryColorNoise As Boolean = False
    Public Property Cursor_Link_PrimaryColorNoiseOpacity As Single = 0.25
    Public Property Cursor_Link_SecondaryColor1 As Color = Color.FromArgb(64, 65, 75)
    Public Property Cursor_Link_SecondaryColor2 As Color = Color.FromArgb(64, 65, 75)
    Public Property Cursor_Link_SecondaryColorGradient As Boolean = False
    Public Property Cursor_Link_SecondaryColorGradientMode As GradientMode = GradientMode.Vertical
    Public Property Cursor_Link_SecondaryColorNoise As Boolean = False
    Public Property Cursor_Link_SecondaryColorNoiseOpacity As Single = 0.25
#End Region

#Region "Pin"
    Public Property Cursor_Pin_PrimaryColor1 As Color = Color.White
    Public Property Cursor_Pin_PrimaryColor2 As Color = Color.White
    Public Property Cursor_Pin_PrimaryColorGradient As Boolean = False
    Public Property Cursor_Pin_PrimaryColorGradientMode As GradientMode = GradientMode.Vertical
    Public Property Cursor_Pin_PrimaryColorNoise As Boolean = False
    Public Property Cursor_Pin_PrimaryColorNoiseOpacity As Single = 0.25
    Public Property Cursor_Pin_SecondaryColor1 As Color = Color.FromArgb(64, 65, 75)
    Public Property Cursor_Pin_SecondaryColor2 As Color = Color.FromArgb(64, 65, 75)
    Public Property Cursor_Pin_SecondaryColorGradient As Boolean = False
    Public Property Cursor_Pin_SecondaryColorGradientMode As GradientMode = GradientMode.Vertical
    Public Property Cursor_Pin_SecondaryColorNoise As Boolean = False
    Public Property Cursor_Pin_SecondaryColorNoiseOpacity As Single = 0.25
#End Region

#Region "Person"
    Public Property Cursor_Person_PrimaryColor1 As Color = Color.White
    Public Property Cursor_Person_PrimaryColor2 As Color = Color.White
    Public Property Cursor_Person_PrimaryColorGradient As Boolean = False
    Public Property Cursor_Person_PrimaryColorGradientMode As GradientMode = GradientMode.Vertical
    Public Property Cursor_Person_PrimaryColorNoise As Boolean = False
    Public Property Cursor_Person_PrimaryColorNoiseOpacity As Single = 0.25
    Public Property Cursor_Person_SecondaryColor1 As Color = Color.FromArgb(64, 65, 75)
    Public Property Cursor_Person_SecondaryColor2 As Color = Color.FromArgb(64, 65, 75)
    Public Property Cursor_Person_SecondaryColorGradient As Boolean = False
    Public Property Cursor_Person_SecondaryColorGradientMode As GradientMode = GradientMode.Vertical
    Public Property Cursor_Person_SecondaryColorNoise As Boolean = False
    Public Property Cursor_Person_SecondaryColorNoiseOpacity As Single = 0.25
#End Region

#Region "IBeam"
    Public Property Cursor_IBeam_PrimaryColor1 As Color = Color.White
    Public Property Cursor_IBeam_PrimaryColor2 As Color = Color.White
    Public Property Cursor_IBeam_PrimaryColorGradient As Boolean = False
    Public Property Cursor_IBeam_PrimaryColorGradientMode As GradientMode = GradientMode.Vertical
    Public Property Cursor_IBeam_PrimaryColorNoise As Boolean = False
    Public Property Cursor_IBeam_PrimaryColorNoiseOpacity As Single = 0.25
    Public Property Cursor_IBeam_SecondaryColor1 As Color = Color.FromArgb(64, 65, 75)
    Public Property Cursor_IBeam_SecondaryColor2 As Color = Color.FromArgb(64, 65, 75)
    Public Property Cursor_IBeam_SecondaryColorGradient As Boolean = False
    Public Property Cursor_IBeam_SecondaryColorGradientMode As GradientMode = GradientMode.Vertical
    Public Property Cursor_IBeam_SecondaryColorNoise As Boolean = False
    Public Property Cursor_IBeam_SecondaryColorNoiseOpacity As Single = 0.25
#End Region

#Region "Cross"
    Public Property Cursor_Cross_PrimaryColor1 As Color = Color.White
    Public Property Cursor_Cross_PrimaryColor2 As Color = Color.White
    Public Property Cursor_Cross_PrimaryColorGradient As Boolean = False
    Public Property Cursor_Cross_PrimaryColorGradientMode As GradientMode = GradientMode.Vertical
    Public Property Cursor_Cross_PrimaryColorNoise As Boolean = False
    Public Property Cursor_Cross_PrimaryColorNoiseOpacity As Single = 0.25
    Public Property Cursor_Cross_SecondaryColor1 As Color = Color.FromArgb(64, 65, 75)
    Public Property Cursor_Cross_SecondaryColor2 As Color = Color.FromArgb(64, 65, 75)
    Public Property Cursor_Cross_SecondaryColorGradient As Boolean = False
    Public Property Cursor_Cross_SecondaryColorGradientMode As GradientMode = GradientMode.Vertical
    Public Property Cursor_Cross_SecondaryColorNoise As Boolean = False
    Public Property Cursor_Cross_SecondaryColorNoiseOpacity As Single = 0.25
#End Region
#End Region

    Enum LogonUI_Modes
        Default_
        Wallpaper
        CustomImage
        SolidColor
    End Enum

    Enum LogonUI7_NoiseMode
        Aero
        Acrylic
    End Enum
    Enum Mode
        Registry
        Init
        File
    End Enum
    Enum SavingMode
        Registry
        File
    End Enum
#End Region

#Region "Functions"
#Region "UserPreferenceMask"
    Function GetUserPreferencesMask(Bit As Integer) As Boolean

        Try
            Dim hexstring As Byte() = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Desktop", "UserPreferencesMask", Nothing)
            Dim binarystring As String = String.Join("", hexstring.Reverse().[Select](Function(xb) Convert.ToString(xb, 2).PadLeft(8, "0"c)))
            Return If(binarystring(binarystring.Count - 1 - Bit) = CChar("1"), True, False)
        Catch
            Return False
        End Try

    End Function

    Function SetUserPreferenceMask(Bit As Integer, Value As Boolean) As Byte()
        Dim hexstring As Byte() = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Desktop", "UserPreferencesMask", Nothing)
        Dim binarystring As String = String.Join("", hexstring.Reverse().[Select](Function(xb) Convert.ToString(xb, 2).PadLeft(8, "0"c)))
        Dim EnableThemeIndex As Integer = binarystring.Count - 1 - Bit
        binarystring = binarystring.Remove(EnableThemeIndex, 1).Insert(EnableThemeIndex, If(Value, 1, 0))
        Dim binaryStr As String = binarystring
        Dim ar As Byte()
        ar = StringToBytesArray(binaryStr)
        If ar.Count < 8 Then
            For i = 0 To 7 - ar.Count
                ar = AddByteToArray(ar, 0)
            Next
        End If
        ar = ar.Reverse.ToArray
        Return ar
    End Function
#End Region
    Function ReturnEightDigitsFromInt(int As Integer) As String
        Dim i As Integer = 8 - int.ToString.Count
        Dim s As String = ""
        For i = 1 To i
            s &= "0"
        Next
        s &= int
        Return s
    End Function
    Shared Sub EditReg(KeyName As String, ValueName As String, Value As Object, Optional ByVal Binary As Boolean = False, Optional ByVal [String] As Boolean = False)
        Dim R As RegistryKey = Nothing

        If KeyName.Contains("HKEY_CURRENT_USER") Then
            R = Registry.CurrentUser
            KeyName = KeyName.Remove(0, "HKEY_CURRENT_USER\".Count)
        ElseIf KeyName.Contains("HKEY_LOCAL_MACHINE") Then
            R = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32)
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
        Catch ex As Exception
            MsgBox(ex.Message & vbCrLf & vbCrLf & ex.StackTrace)

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
    Public Function RGB2HEX_oneline(ByVal [Color] As Color, Optional ByVal Alpha As Boolean = True) As String
        Dim S As String
        If Alpha Then
            S = String.Format("{0:X2}", Color.A, Color.R, Color.G, Color.B) & String.Format("{1:X2}", Color.A, Color.R, Color.G, Color.B) &
            String.Format("{2:X2}", Color.A, Color.R, Color.G, Color.B) & String.Format("{3:X2}", Color.A, Color.R, Color.G, Color.B)
        Else
            S = String.Format("{0:X2}{1:X2}{2:X2}", Color.R, Color.G, Color.B)
        End If
        Return S
    End Function
    Function BizareColorInvertor([Color] As Color) As Color
        Return Color.FromArgb([Color].B, [Color].G, [Color].R)
    End Function
    Public Function AddByteToArray(ByVal bArray As Byte(), ByVal newByte As Byte) As Byte()
        Dim newArray As Byte() = New Byte(bArray.Length + 1 - 1) {}
        bArray.CopyTo(newArray, 1)
        newArray(0) = newByte
        Return newArray
    End Function
    Private Function StringToBytesArray(ByVal str As String) As Byte()
        Dim bitsToPad = 8 - str.Length Mod 8

        If bitsToPad <> 8 Then
            Dim neededLength = bitsToPad + str.Length
            str = str.PadLeft(neededLength, "0"c)
        End If

        Dim size As Integer = str.Length / 8
        Dim arr As Byte() = New Byte(size - 1) {}

        For a As Integer = 0 To size - 1
            arr(a) = Convert.ToByte(str.Substring(a * 8, 8), 2)
        Next

        Return arr
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
    Public Shared Function GetPaletteFromString([String] As String, ThemeName As String) As List(Of Color)

        If String.IsNullOrWhiteSpace([String]) Then
            Return Nothing
            Exit Function
        End If

        If Not [String].Contains("|") Then
            Return Nothing
            Exit Function
        End If

        If String.IsNullOrWhiteSpace(ThemeName) Then
            Return Nothing
            Exit Function
        End If

        Dim ls As New List(Of Color)
        ls.Clear()

        Dim AllThemes As New List(Of String)
        Dim SelectedTheme As String = ""
        Dim SelectedThemeList As New List(Of String)

        CList_FromStr(AllThemes, [String])
        Dim Found As Boolean = False

        For Each th As String In AllThemes
            If th.Split("|")(0).ToLower = ThemeName.ToLower Then
                SelectedTheme = th.Replace("|", vbCrLf)
                Found = True
                Exit For
            End If
        Next

        If Not Found Then
            Return Nothing
            Exit Function
        End If

        CList_FromStr(SelectedThemeList, SelectedTheme)


        For Each x As String In SelectedThemeList
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
    End Function
    Public Shared Sub PopulateThemeToListbox([ComboBox] As ComboBox)
        [ComboBox].Items.Clear()
        Dim ls As New List(Of String)
        CList_FromStr(ls, My.Resources.RetroThemesDB)

        For Each x As String In ls
            [ComboBox].Items.Add(x.Split("|")(0))
        Next

    End Sub
    Public Function ListColors() As List(Of Color)
        Dim type1 As Type = [GetType]() : Dim properties1 As System.Reflection.PropertyInfo() = type1.GetProperties()
        Dim CL As New List(Of Color)
        CL.Clear()

        For Each [property] As System.Reflection.PropertyInfo In properties1
            If [property].PropertyType.Name.ToLower = "color" Then
                CL.Add([property].GetValue(Me, Nothing))
            End If
        Next

        CL = CL.Distinct.ToList

        Return CL
    End Function
    Public Shared Function IsFontInstalled(ByVal fontName As String) As Boolean
        Dim installed As Boolean = IsFontInstalled(fontName, FontStyle.Regular)

        If Not installed Then
            installed = IsFontInstalled(fontName, FontStyle.Bold)
        End If

        If Not installed Then
            installed = IsFontInstalled(fontName, FontStyle.Italic)
        End If

        Return installed
    End Function
    Public Shared Function IsFontInstalled(ByVal fontName As String, ByVal style As FontStyle) As Boolean
        Dim installed As Boolean = False
        Const emSize As Single = 8.0F

        Try

            Using testFont = New Font(fontName, emSize, style)
                installed = (0 = String.Compare(fontName, testFont.Name, StringComparison.InvariantCultureIgnoreCase))
            End Using

        Catch
        End Try

        Return installed
    End Function

    Public f As Form
    Dim th As Threading.Thread
    Public Sub Task_A()
        f = New ApplyingTheme()
        Application.Run(f)
    End Sub
#End Region

    Sub New([Mode] As Mode, Optional ByVal PaletteFile As String = "", Optional IgnoreWindowsTerminal As Boolean = False)
        Select Case [Mode]
            Case Mode.Registry
                Dim _Def As CP
                If MainFrm.PreviewConfig = MainFrm.WinVer.Eleven Then
                    _Def = New CP_Defaults().Default_Windows11
                ElseIf MainFrm.PreviewConfig = MainFrm.WinVer.Ten Then
                    _Def = New CP_Defaults().Default_Windows10
                ElseIf MainFrm.PreviewConfig = MainFrm.WinVer.Eight Then
                    _Def = New CP_Defaults().Default_Windows8
                ElseIf MainFrm.PreviewConfig = MainFrm.WinVer.Seven Then
                    _Def = New CP_Defaults().Default_Windows7
                Else
                    _Def = New CP_Defaults().Default_Windows11
                End If

#Region "Registry"
                Dim Colors As New List(Of Color)
                Colors.Clear()

#Region "Personal Info"
                Author = Environment.UserName
                AppVersion = My.Application.Info.Version.ToString
                PaletteVersion = "1.0"
                PaletteName = My.Application.LanguageHelper.CurrentMode
#End Region

#Region "Modern Windows"
                If Not My.W7 And Not My.W8 Then
                    Dim Def As CP = If(My.W11, New CP_Defaults().Default_Windows11, New CP_Defaults().Default_Windows10)

                    Dim x As Byte()
                    Dim y As Object

                    Try
                        x = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "AccentPalette", If(My.W11, New CP_Defaults().Default_Windows11Accents_Bytes, New CP_Defaults().Default_Windows10Accents_Bytes))
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
                        UWP_Undefined = Colors(7)

                    Catch
                        x = If(My.W11, New CP_Defaults().Default_Windows11Accents_Bytes, New CP_Defaults().Default_Windows10Accents_Bytes)

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
                        UWP_Undefined = Colors(7)

                    End Try

                    Try
                        y = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "StartColorMenu", BizareColorInvertor(Def.StartMenu_Accent).ToArgb)
                        StartMenu_Accent = BizareColorInvertor(Color.FromArgb(y))
                    Catch
                        StartMenu_Accent = Def.StartMenu_Accent
                    End Try

                    Try
                        y = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "AccentColorMenu", BizareColorInvertor(Def.Titlebar_Active).ToArgb)
                        Titlebar_Active = BizareColorInvertor(Color.FromArgb(y))
                    Catch
                        Titlebar_Active = Def.Titlebar_Active
                    End Try

                    Try
                        y = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\DWM", "AccentColor", BizareColorInvertor(Def.Titlebar_Active).ToArgb)
                        Titlebar_Active = BizareColorInvertor(Color.FromArgb(y))
                    Catch
                        Titlebar_Active = Def.Titlebar_Active
                    End Try

                    Try
                        y = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\DWM", "AccentColorInactive", BizareColorInvertor(Def.Titlebar_Inactive).ToArgb)
                        Titlebar_Inactive = BizareColorInvertor(Color.FromArgb(y))
                    Catch
                        Titlebar_Inactive = Def.Titlebar_Inactive
                    End Try

                    Try
                        WinMode_Light = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "SystemUsesLightTheme", Def.WinMode_Light)
                    Catch
                        WinMode_Light = Def.WinMode_Light
                    End Try

                    Try
                        AppMode_Light = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "AppsUseLightTheme", Def.AppMode_Light)
                    Catch
                        AppMode_Light = Def.AppMode_Light
                    End Try

                    Try
                        Transparency = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "EnableTransparency", Def.Transparency)
                    Catch
                        Transparency = Def.Transparency
                    End Try

                    Try
                        Select Case My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "ColorPrevalence", 0)
                            Case 0
                                ApplyAccentonTaskbar = ApplyAccentonTaskbar_Level.None
                            Case 1
                                ApplyAccentonTaskbar = ApplyAccentonTaskbar_Level.Taskbar_Start_AC
                            Case 2
                                ApplyAccentonTaskbar = ApplyAccentonTaskbar_Level.Taskbar
                            Case Else
                                ApplyAccentonTaskbar = ApplyAccentonTaskbar_Level.None
                        End Select

                    Catch
                        ApplyAccentonTaskbar = Def.ApplyAccentonTaskbar
                    End Try

                    Try
                        ApplyAccentonTitlebars = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\DWM", "ColorPrevalence", Def.ApplyAccentonTitlebars)
                    Catch
                        ApplyAccentonTitlebars = Def.ApplyAccentonTitlebars
                    End Try


                Else

                    ActionCenter_AppsLinks = _Def.ActionCenter_AppsLinks
                    Taskbar_Icon_Underline = _Def.Taskbar_Icon_Underline
                    StartButton_Hover = _Def.StartButton_Hover
                    SettingsIconsAndLinks = _Def.SettingsIconsAndLinks
                    StartMenuBackground_ActiveTaskbarButton = _Def.StartMenuBackground_ActiveTaskbarButton
                    StartListFolders_TaskbarFront = _Def.StartListFolders_TaskbarFront
                    Taskbar_Background = _Def.Taskbar_Background
                    StartMenu_Accent = _Def.StartMenu_Accent
                    Titlebar_Active = _Def.Titlebar_Active
                    Titlebar_Inactive = _Def.Titlebar_Inactive
                    WinMode_Light = _Def.WinMode_Light
                    AppMode_Light = _Def.AppMode_Light
                    Transparency = _Def.Transparency
                    ApplyAccentonTaskbar = _Def.ApplyAccentonTaskbar
                    ApplyAccentonTitlebars = _Def.ApplyAccentonTitlebars
                End If

#End Region

#Region "Aero"
                If My.W7 Or My.W8 Then
                    Dim Def As CP = If(My.W7, New CP_Defaults().Default_Windows7, New CP_Defaults().Default_Windows8)
                    Dim y As Object

                    Try
                        y = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationColor", Def.Aero_ColorizationColor.ToArgb)
                        Aero_ColorizationColor = Color.FromArgb(255, Color.FromArgb(y))
                    Catch
                        Aero_ColorizationColor = Def.Aero_ColorizationColor
                    End Try

                    Try
                        y = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationColorBalance", Def.Aero_ColorizationColorBalance)
                        Aero_ColorizationColorBalance = y
                    Catch
                        Aero_ColorizationColorBalance = Def.Aero_ColorizationColorBalance
                    End Try

                    If Not My.W8 Then
                        Try
                            y = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationAfterglow", Def.Aero_ColorizationAfterglow.ToArgb)
                            Aero_ColorizationAfterglow = Color.FromArgb(255, Color.FromArgb(y))
                        Catch
                            Aero_ColorizationAfterglow = Def.Aero_ColorizationAfterglow
                        End Try

                        Try
                            y = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationAfterglowBalance", Def.Aero_ColorizationAfterglowBalance)
                            Aero_ColorizationAfterglowBalance = y
                        Catch
                            Aero_ColorizationAfterglowBalance = Def.Aero_ColorizationAfterglowBalance
                        End Try

                        Try
                            y = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationBlurBalance", Def.Aero_ColorizationBlurBalance)
                            Aero_ColorizationBlurBalance = y
                        Catch
                            Aero_ColorizationBlurBalance = Def.Aero_ColorizationBlurBalance
                        End Try

                        Try
                            y = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationGlassReflectionIntensity", Def.Aero_ColorizationGlassReflectionIntensity)
                            Aero_ColorizationGlassReflectionIntensity = y
                        Catch
                            Aero_ColorizationGlassReflectionIntensity = Def.Aero_ColorizationGlassReflectionIntensity
                        End Try

                        Dim Com, Opaque As Boolean
                        NativeMethods.Dwmapi.DwmIsCompositionEnabled(Com)

                        Try
                            Opaque = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationOpaqueBlend", False)
                        Catch
                            Opaque = False
                        End Try

                        Dim Classic As Boolean = False

                        Try
                            Dim stringThemeName As System.Text.StringBuilder = New System.Text.StringBuilder(260)
                            NativeMethods.Uxtheme.GetCurrentThemeName(stringThemeName, 260, Nothing, 0, Nothing, 0)
                            Classic = String.IsNullOrWhiteSpace(stringThemeName.ToString) Or Not IO.File.Exists(stringThemeName.ToString)
                        Catch
                            Classic = False
                        End Try

                        If Classic Then
                            Aero_Theme = AeroTheme.Classic
                        ElseIf Com Then
                            If Not Opaque Then Aero_Theme = AeroTheme.Aero Else Aero_Theme = AeroTheme.AeroOpaque
                        Else
                            Aero_Theme = AeroTheme.Basic
                        End If

                    End If

                    Try
                        Aero_EnableAeroPeek = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "EnableAeroPeek", Def.Aero_EnableAeroPeek)
                    Catch
                        Aero_EnableAeroPeek = Def.Aero_EnableAeroPeek
                    End Try

                    Try
                        Aero_AlwaysHibernateThumbnails = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "AlwaysHibernateThumbnails", Def.Aero_AlwaysHibernateThumbnails)
                    Catch
                        Aero_AlwaysHibernateThumbnails = Def.Aero_AlwaysHibernateThumbnails
                    End Try

                Else
                    Aero_ColorizationColor = _Def.Aero_ColorizationColor
                    Aero_ColorizationColorBalance = _Def.Aero_ColorizationColorBalance
                    Aero_ColorizationAfterglow = _Def.Aero_ColorizationAfterglow
                    Aero_ColorizationAfterglowBalance = _Def.Aero_ColorizationAfterglowBalance
                    Aero_ColorizationBlurBalance = _Def.Aero_ColorizationBlurBalance
                    Aero_ColorizationGlassReflectionIntensity = _Def.Aero_ColorizationGlassReflectionIntensity
                    Aero_Theme = _Def.Aero_Theme
                    Aero_EnableAeroPeek = _Def.Aero_EnableAeroPeek
                    Aero_AlwaysHibernateThumbnails = _Def.Aero_AlwaysHibernateThumbnails
                End If

#End Region

#Region "Metro"
                If My.W8 Then
                    Dim Def As CP = New CP_Defaults().Default_Windows8

                    Dim stringThemeName As System.Text.StringBuilder = New System.Text.StringBuilder(260)
                    NativeMethods.Uxtheme.GetCurrentThemeName(stringThemeName, 260, Nothing, 0, Nothing, 0)

                    If stringThemeName.ToString.Split("\").Last.ToLower = "aerolite.msstyles" Or String.IsNullOrWhiteSpace(stringThemeName.ToString) Then
                        Metro_Theme = AeroTheme.AeroLite
                    Else
                        Metro_Theme = AeroTheme.Aero
                    End If

                    Registry.CurrentUser.CreateSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent")

                    If My.Application.isElevated Then
                        Registry.LocalMachine.CreateSubKey("SOFTWARE\Policies\Microsoft\Windows\Personalization")
                        Registry.LocalMachine.CreateSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent")
                    Else
                        Dim ls As New List(Of String)
                        ls.Clear()
                        ls.Add("Windows Registry Editor Version 5.00")
                        ls.Add(vbCrLf)
                        ls.Add("[HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Personalization]")
                        ls.Add(vbCrLf)
                        ls.Add("[HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent]")
                        ls.Add(vbCrLf)

                        Dim result As String = CStr_FromList(ls)

                        If Not IO.Directory.Exists(My.Application.appData) Then IO.Directory.CreateDirectory(My.Application.appData)

                        Dim tempreg As String = My.Application.appData & "\tempreg.reg"

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

                    Dim y As Object

                    Try
                        y = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "StartColor", Color.FromArgb(84, 0, 30).ToArgb)
                        Metro_StartColor = Color.FromArgb(255, BizareColorInvertor(Color.FromArgb(y)))
                    Catch
                        Metro_StartColor = Color.FromArgb(84, 0, 30)
                    End Try

                    Try
                        y = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "AccentColor", Color.FromArgb(178, 29, 72).ToArgb)
                        Metro_AccentColor = Color.FromArgb(255, BizareColorInvertor(Color.FromArgb(y)))
                    Catch
                        Metro_AccentColor = Color.FromArgb(178, 29, 72)
                    End Try

                    Dim S As String

                    Try
                        S = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Personalization", "PersonalColors_Background", "#1e0054")
                        Metro_PersonalColors_Background = Color.FromArgb(255, Color.FromArgb(Convert.ToInt32(S.Replace("#", ""), 16)))
                    Catch
                        Metro_PersonalColors_Background = Def.Metro_PersonalColors_Background
                    End Try

                    Try
                        S = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Personalization", "PersonalColors_Accent", "#481db2")
                        Metro_PersonalColors_Accent = Color.FromArgb(255, Color.FromArgb(Convert.ToInt32(S.Replace("#", ""), 16)))
                    Catch
                        Metro_PersonalColors_Accent = Def.Metro_PersonalColors_Accent
                    End Try

                    Try
                        Metro_Start = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Personalization", "ForceStartBackground", 0)
                    Catch
                        Metro_Start = 0
                    End Try

                    Try
                        Metro_LogonUI = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "DefaultColorSet", 0)
                    Catch
                        Metro_LogonUI = 0
                    End Try
                Else
                    Metro_Theme = _Def.Metro_Theme
                    Metro_StartColor = _Def.Metro_StartColor
                    Metro_AccentColor = _Def.Metro_AccentColor
                    Metro_PersonalColors_Background = _Def.Metro_PersonalColors_Background
                    Metro_PersonalColors_Accent = _Def.Metro_PersonalColors_Accent
                    Metro_Start = _Def.Metro_Start
                    Metro_LogonUI = _Def.Metro_LogonUI
                End If
#End Region

#Region "LogonUI"
                If Not My.W7 And Not My.W8 Then
                    Dim Def As CP = If(My.W11, New CP_Defaults().Default_Windows11, New CP_Defaults().Default_Windows10)

                    Try
                        LogonUI_DisableAcrylicBackgroundOnLogon = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\System", "DisableAcrylicBackgroundOnLogon", Def.LogonUI_DisableAcrylicBackgroundOnLogon)
                    Catch
                        LogonUI_DisableAcrylicBackgroundOnLogon = Def.LogonUI_DisableAcrylicBackgroundOnLogon
                    End Try

                    Try
                        LogonUI_DisableLogonBackgroundImage = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\System", "DisableLogonBackgroundImage", Def.LogonUI_DisableLogonBackgroundImage)
                    Catch
                        LogonUI_DisableLogonBackgroundImage = Def.LogonUI_DisableLogonBackgroundImage
                    End Try

                    Try
                        LogonUI_NoLockScreen = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Windows\Personalization", "NoLockScreen", Def.LogonUI_NoLockScreen)
                    Catch
                        LogonUI_NoLockScreen = Def.LogonUI_NoLockScreen
                    End Try
                Else
                    LogonUI_DisableAcrylicBackgroundOnLogon = _Def.LogonUI_DisableAcrylicBackgroundOnLogon
                    LogonUI_DisableLogonBackgroundImage = _Def.LogonUI_DisableLogonBackgroundImage
                    LogonUI_NoLockScreen = _Def.LogonUI_NoLockScreen
                End If

#End Region

#Region "LogonUI 7"
                If My.W7 Then
                    Dim b1 As Boolean = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Authentication\LogonUI\Background", "OEMBackground", False)
                    Dim b2 As Boolean = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Windows\System", "UseOEMBackground", False)
                    LogonUI7_Enabled = b1 And b2

                    Dim rLog As RegistryKey = Registry.CurrentUser.CreateSubKey("Software\WinPaletter\LogonUI")

                    Try
                        LogonUI7_Mode = rLog.GetValue("Mode", LogonUI_Modes.Default_)
                    Catch
                        LogonUI7_Mode = LogonUI_Modes.Default_
                    End Try

                    Try
                        LogonUI7_ImagePath = rLog.GetValue("ImagePath", "")
                    Catch
                        LogonUI7_ImagePath = ""
                    End Try

                    Try
                        LogonUI7_Color = Color.FromArgb(rLog.GetValue("Color", Color.Black.ToArgb))
                    Catch
                        LogonUI7_Color = Color.Black
                    End Try

                    Try
                        LogonUI7_Effect_Blur = rLog.GetValue("Effect_Blur", False)
                    Catch
                        LogonUI7_Effect_Blur = False
                    End Try

                    Try
                        LogonUI7_Effect_Blur_Intensity = rLog.GetValue("Effect_Blur_Intensity", 0)
                    Catch
                        LogonUI7_Effect_Blur_Intensity = 0
                    End Try

                    Try
                        LogonUI7_Effect_Grayscale = rLog.GetValue("Effect_Grayscale", False)
                    Catch
                        LogonUI7_Effect_Grayscale = False
                    End Try

                    Try
                        LogonUI7_Effect_Noise = rLog.GetValue("Effect_Noise", False)
                    Catch
                        LogonUI7_Effect_Noise = False
                    End Try

                    Try
                        LogonUI7_Effect_Noise_Mode = rLog.GetValue("Noise_Mode", LogonUI7_NoiseMode.Acrylic)
                    Catch
                        LogonUI7_Effect_Noise_Mode = LogonUI7_NoiseMode.Acrylic
                    End Try

                    Try
                        LogonUI7_Effect_Noise_Intensity = rLog.GetValue("Effect_Noise_Intensity", 0)
                    Catch
                        LogonUI7_Effect_Noise_Intensity = 0
                    End Try

                    rLog.Close()
                Else
                    LogonUI7_Enabled = _Def.LogonUI7_Enabled
                    LogonUI7_Mode = _Def.LogonUI7_Mode
                    LogonUI7_ImagePath = _Def.LogonUI7_ImagePath
                    LogonUI7_Color = _Def.LogonUI7_Color
                    LogonUI7_Effect_Blur = _Def.LogonUI7_Effect_Blur
                    LogonUI7_Effect_Blur_Intensity = _Def.LogonUI7_Effect_Blur_Intensity
                    LogonUI7_Effect_Grayscale = _Def.LogonUI7_Effect_Grayscale
                    LogonUI7_Effect_Noise = _Def.LogonUI7_Effect_Noise
                    LogonUI7_Effect_Noise_Mode = _Def.LogonUI7_Effect_Noise_Mode
                    LogonUI7_Effect_Noise_Intensity = _Def.LogonUI7_Effect_Noise_Intensity
                End If
#End Region

#Region "LogonUI 8"
                If My.W8 Then
                    Metro_NoLockScreen = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\Policies\Microsoft\Windows\Personalization", "NoLockScreen", False)

                    Dim rLog As RegistryKey = Registry.CurrentUser.CreateSubKey("Software\WinPaletter\LogonUI")

                    Try
                        Metro_LockScreenType = rLog.GetValue("Mode", LogonUI_Modes.Default_)
                    Catch
                        Metro_LockScreenType = LogonUI_Modes.Default_
                    End Try

                    Try
                        Metro_LockScreenSystemID = rLog.GetValue("Metro_LockScreenSystemID", 0)
                    Catch
                        Metro_LockScreenSystemID = 0
                    End Try

                    Try
                        LogonUI7_ImagePath = rLog.GetValue("ImagePath", "")
                    Catch
                        LogonUI7_ImagePath = ""
                    End Try

                    Try
                        LogonUI7_Color = Color.FromArgb(rLog.GetValue("Color", Color.Black.ToArgb))
                    Catch
                        LogonUI7_Color = Color.Black
                    End Try

                    Try
                        LogonUI7_Effect_Blur = rLog.GetValue("Effect_Blur", False)
                    Catch
                        LogonUI7_Effect_Blur = False
                    End Try

                    Try
                        LogonUI7_Effect_Blur_Intensity = rLog.GetValue("Effect_Blur_Intensity", 0)
                    Catch
                        LogonUI7_Effect_Blur_Intensity = 0
                    End Try

                    Try
                        LogonUI7_Effect_Grayscale = rLog.GetValue("Effect_Grayscale", False)
                    Catch
                        LogonUI7_Effect_Grayscale = False
                    End Try

                    Try
                        LogonUI7_Effect_Noise = rLog.GetValue("Effect_Noise", False)
                    Catch
                        LogonUI7_Effect_Noise = False
                    End Try

                    Try
                        LogonUI7_Effect_Noise_Mode = rLog.GetValue("Noise_Mode", LogonUI7_NoiseMode.Acrylic)
                    Catch
                        LogonUI7_Effect_Noise_Mode = LogonUI7_NoiseMode.Acrylic
                    End Try

                    Try
                        LogonUI7_Effect_Noise_Intensity = rLog.GetValue("Effect_Noise_Intensity", 0)
                    Catch
                        LogonUI7_Effect_Noise_Intensity = 0
                    End Try

                    rLog.Close()

                ElseIf Not My.W7 Then
                    Metro_NoLockScreen = _Def.Metro_NoLockScreen
                    Metro_LockScreenType = _Def.Metro_LockScreenType
                    Metro_LockScreenSystemID = _Def.Metro_LockScreenSystemID
                    LogonUI7_ImagePath = _Def.LogonUI7_ImagePath
                    LogonUI7_Color = _Def.LogonUI7_Color
                    LogonUI7_Effect_Blur = _Def.LogonUI7_Effect_Blur
                    LogonUI7_Effect_Blur_Intensity = _Def.LogonUI7_Effect_Blur_Intensity
                    LogonUI7_Effect_Grayscale = _Def.LogonUI7_Effect_Grayscale
                    LogonUI7_Effect_Noise = _Def.LogonUI7_Effect_Noise
                    LogonUI7_Effect_Noise_Mode = _Def.LogonUI7_Effect_Noise_Mode
                    LogonUI7_Effect_Noise_Intensity = _Def.LogonUI7_Effect_Noise_Intensity
                End If
#End Region

#Region "Win32UI"
                Try
                    Win32UI_EnableTheming = GetUserPreferencesMask(17)
                Catch
                    Win32UI_EnableTheming = True
                End Try

                Try
                    Win32UI_EnableGradient = GetUserPreferencesMask(4)
                Catch
                    Win32UI_EnableGradient = True
                End Try

                With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "ActiveTitle", "153 180 209")
                    If .ToString.Split(" ").Count = 3 Then Win32UI_ActiveTitle = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                End With

                With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "AppWorkspace", "171 171 171")
                    If .ToString.Split(" ").Count = 3 Then Win32UI_AppWorkspace = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                End With

                With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "Background", "0 0 0")
                    If .ToString.Split(" ").Count = 3 Then Win32UI_Background = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                End With

                With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "ButtonAlternateFace", "0 0 0")
                    If .ToString.Split(" ").Count = 3 Then Win32UI_ButtonAlternateFace = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                End With

                With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "ButtonDkShadow", "105 105 105")
                    If .ToString.Split(" ").Count = 3 Then Win32UI_ButtonDkShadow = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                End With

                With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "ButtonFace", "240 240 240")
                    If .ToString.Split(" ").Count = 3 Then Win32UI_ButtonFace = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                End With

                With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "ButtonHilight", "255 255 255")
                    If .ToString.Split(" ").Count = 3 Then Win32UI_ButtonHilight = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                End With

                With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "ButtonLight", "227 227 227")
                    If .ToString.Split(" ").Count = 3 Then Win32UI_ButtonLight = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                End With

                With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "ButtonShadow", "160 160 160")
                    If .ToString.Split(" ").Count = 3 Then Win32UI_ButtonShadow = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                End With

                With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "ButtonText", "0 0 0")
                    If .ToString.Split(" ").Count = 3 Then Win32UI_ButtonText = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                End With

                With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "GradientActiveTitle", "185 209 234")
                    If .ToString.Split(" ").Count = 3 Then Win32UI_GradientActiveTitle = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                End With

                With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "GradientInactiveTitle", "215 228 242")
                    If .ToString.Split(" ").Count = 3 Then Win32UI_GradientInactiveTitle = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                End With

                With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "GrayText", "109 109 109")
                    If .ToString.Split(" ").Count = 3 Then Win32UI_GrayText = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                End With

                With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "HilightText", "255 255 255")
                    If .ToString.Split(" ").Count = 3 Then Win32UI_HilightText = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                End With

                With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "HotTrackingColor", "0 102 204")
                    If .ToString.Split(" ").Count = 3 Then Win32UI_HotTrackingColor = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                End With

                With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "ActiveBorder", "244 247 252")
                    If .ToString.Split(" ").Count = 3 Then Win32UI_ActiveBorder = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                End With

                With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "InactiveBorder", "244 247 252")
                    If .ToString.Split(" ").Count = 3 Then Win32UI_InactiveBorder = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                End With

                With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "InactiveTitle", "191 205 219")
                    If .ToString.Split(" ").Count = 3 Then Win32UI_InactiveTitle = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                End With

                With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "InactiveTitleText", "0 0 0")
                    If .ToString.Split(" ").Count = 3 Then Win32UI_InactiveTitleText = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                End With

                With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "InfoText", "0 0 0")
                    If .ToString.Split(" ").Count = 3 Then Win32UI_InfoText = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                End With

                With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "InfoWindow", "255 255 225")
                    If .ToString.Split(" ").Count = 3 Then Win32UI_InfoWindow = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                End With

                With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "Menu", "240 240 240")
                    If .ToString.Split(" ").Count = 3 Then Win32UI_Menu = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                End With

                With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "MenuBar", "240 240 240")
                    If .ToString.Split(" ").Count = 3 Then Win32UI_MenuBar = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                End With

                With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "MenuText", "0 0 0")
                    If .ToString.Split(" ").Count = 3 Then Win32UI_MenuText = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                End With

                With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "Scrollbar", "200 200 200")
                    If .ToString.Split(" ").Count = 3 Then Win32UI_Scrollbar = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                End With

                With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "TitleText", "0 0 0")
                    If .ToString.Split(" ").Count = 3 Then Win32UI_TitleText = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                End With

                With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "Window", "255 255 255")
                    If .ToString.Split(" ").Count = 3 Then Win32UI_Window = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                End With

                With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "WindowFrame", "100 100 100")
                    If .ToString.Split(" ").Count = 3 Then Win32UI_WindowFrame = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                End With

                With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "WindowText", "0 0 0")
                    If .ToString.Split(" ").Count = 3 Then Win32UI_WindowText = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                End With

                With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "Hilight", "0 120 215")
                    If .ToString.Split(" ").Count = 3 Then Win32UI_Hilight = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                End With

                With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "MenuHilight", "0 120 215")
                    If .ToString.Split(" ").Count = 3 Then Win32UI_MenuHilight = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                End With

                With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "Desktop", "0 0 0")
                    If .ToString.Split(" ").Count = 3 Then Win32UI_Desktop = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                End With
#End Region

#Region "Metrics"
                Metrics_BorderWidth = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "BorderWidth", -15) / -15

                Metrics_CaptionHeight = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "CaptionHeight", -330) / -15

                Metrics_CaptionWidth = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "CaptionWidth", -330) / -15

                Metrics_IconSpacing = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "IconSpacing", -1125) / -15

                Metrics_IconVerticalSpacing = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "IconVerticalSpacing", -1125) / -15

                Metrics_MenuHeight = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "MenuHeight", -285) / -15

                Metrics_MenuWidth = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "MenuWidth", -285) / -15

                Metrics_MinAnimate = If(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "MinAnimate", 1) = 1, True, False)

                Metrics_PaddedBorderWidth = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "PaddedBorderWidth", -60) / -15

                Metrics_ScrollHeight = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "ScrollHeight", -255) / -15

                Metrics_ScrollWidth = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "ScrollWidth", -255) / -15

                Metrics_SmCaptionHeight = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "SmCaptionHeight", -330) / -15

                Metrics_SmCaptionWidth = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "SmCaptionWidth", -330) / -15

                Metrics_ShellIconSize = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "Shell Icon Size", 32)

                Metrics_DesktopIconSize = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\Shell\Bags\1\Desktop", "IconSize", 32)
#End Region

#Region "Fonts"

                Fonts_CaptionFont = Font.FromLogFont(LogFontHelper.ByteToLogFont(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "CaptionFont", Nothing)))

                Fonts_IconFont = Font.FromLogFont(LogFontHelper.ByteToLogFont(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "IconFont", Nothing)))

                Fonts_MenuFont = Font.FromLogFont(LogFontHelper.ByteToLogFont(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "MenuFont", Nothing)))

                Fonts_MessageFont = Font.FromLogFont(LogFontHelper.ByteToLogFont(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "MessageFont", Nothing)))

                Fonts_SmCaptionFont = Font.FromLogFont(LogFontHelper.ByteToLogFont(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "SmCaptionFont", Nothing)))

                Fonts_StatusFont = Font.FromLogFont(LogFontHelper.ByteToLogFont(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "StatusFont", Nothing)))
#End Region

#Region "Terminals"
                Dim y_cmd As Object

#Region "Locking"
                Dim rLogX As RegistryKey = Registry.CurrentUser.CreateSubKey("Software\WinPaletter\Terminals")

                Try
                    Terminal_CMD_Enabled = rLogX.GetValue("Terminal_CMD_Enabled", False)
                Catch
                    Terminal_CMD_Enabled = False
                End Try

                Try
                    Terminal_PS_32_Enabled = rLogX.GetValue("Terminal_PS_32_Enabled", False)
                Catch
                    Terminal_PS_32_Enabled = False
                End Try

                Try
                    Terminal_PS_64_Enabled = rLogX.GetValue("Terminal_PS_64_Enabled", False)
                Catch
                    Terminal_PS_64_Enabled = False
                End Try

                Try
                    Terminal_Stable_Enabled = rLogX.GetValue("Terminal_Stable_Enabled", False)
                Catch
                    Terminal_Stable_Enabled = False
                End Try

                Try
                    Terminal_Preview_Enabled = rLogX.GetValue("Terminal_Preview_Enabled", False)
                Catch
                    Terminal_Preview_Enabled = False
                End Try

#End Region

#Region "Command Prompt"
                Try
                    y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console", "ColorTable00", BizareColorInvertor(_Def.CMD_ColorTable00).ToArgb)
                    CMD_ColorTable00 = Color.FromArgb(255, BizareColorInvertor(Color.FromArgb(y_cmd)))
                Catch
                    CMD_ColorTable00 = _Def.CMD_ColorTable01
                End Try

                Try
                    y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console", "ColorTable01", BizareColorInvertor(_Def.CMD_ColorTable01).ToArgb)
                    CMD_ColorTable01 = Color.FromArgb(255, BizareColorInvertor(Color.FromArgb(y_cmd)))
                Catch
                    CMD_ColorTable01 = _Def.CMD_ColorTable02
                End Try

                Try
                    y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console", "ColorTable02", BizareColorInvertor(_Def.CMD_ColorTable02).ToArgb)
                    CMD_ColorTable02 = Color.FromArgb(255, BizareColorInvertor(Color.FromArgb(y_cmd)))
                Catch
                    CMD_ColorTable02 = _Def.CMD_ColorTable03
                End Try

                Try
                    y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console", "ColorTable03", BizareColorInvertor(_Def.CMD_ColorTable03).ToArgb)
                    CMD_ColorTable03 = Color.FromArgb(255, BizareColorInvertor(Color.FromArgb(y_cmd)))
                Catch
                    CMD_ColorTable03 = _Def.CMD_ColorTable04
                End Try

                Try
                    y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console", "ColorTable04", BizareColorInvertor(_Def.CMD_ColorTable04).ToArgb)
                    CMD_ColorTable04 = Color.FromArgb(255, BizareColorInvertor(Color.FromArgb(y_cmd)))
                Catch
                    CMD_ColorTable04 = _Def.CMD_ColorTable05
                End Try

                Try
                    y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console", "ColorTable05", BizareColorInvertor(_Def.CMD_ColorTable05).ToArgb)
                    CMD_ColorTable05 = Color.FromArgb(255, BizareColorInvertor(Color.FromArgb(y_cmd)))
                Catch
                    CMD_ColorTable05 = _Def.CMD_ColorTable06
                End Try

                Try
                    y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console", "ColorTable06", BizareColorInvertor(_Def.CMD_ColorTable06).ToArgb)
                    CMD_ColorTable06 = Color.FromArgb(255, BizareColorInvertor(Color.FromArgb(y_cmd)))
                Catch
                    CMD_ColorTable06 = _Def.CMD_ColorTable07
                End Try

                Try
                    y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console", "ColorTable07", BizareColorInvertor(_Def.CMD_ColorTable07).ToArgb)
                    CMD_ColorTable07 = Color.FromArgb(255, BizareColorInvertor(Color.FromArgb(y_cmd)))
                Catch
                    CMD_ColorTable07 = _Def.CMD_ColorTable08
                End Try

                Try
                    y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console", "ColorTable08", BizareColorInvertor(_Def.CMD_ColorTable08).ToArgb)
                    CMD_ColorTable08 = Color.FromArgb(255, BizareColorInvertor(Color.FromArgb(y_cmd)))
                Catch
                    CMD_ColorTable08 = _Def.CMD_ColorTable09
                End Try

                Try
                    y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console", "ColorTable09", BizareColorInvertor(_Def.CMD_ColorTable09).ToArgb)
                    CMD_ColorTable09 = Color.FromArgb(255, BizareColorInvertor(Color.FromArgb(y_cmd)))
                Catch
                    CMD_ColorTable09 = _Def.CMD_ColorTable10
                End Try

                Try
                    y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console", "ColorTable10", BizareColorInvertor(_Def.CMD_ColorTable10).ToArgb)
                    CMD_ColorTable10 = Color.FromArgb(255, BizareColorInvertor(Color.FromArgb(y_cmd)))
                Catch
                    CMD_ColorTable10 = _Def.CMD_ColorTable00
                End Try

                Try
                    y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console", "ColorTable11", BizareColorInvertor(_Def.CMD_ColorTable11).ToArgb)
                    CMD_ColorTable11 = Color.FromArgb(255, BizareColorInvertor(Color.FromArgb(y_cmd)))
                Catch
                    CMD_ColorTable11 = _Def.CMD_ColorTable11
                End Try

                Try
                    y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console", "ColorTable12", BizareColorInvertor(_Def.CMD_ColorTable12).ToArgb)
                    CMD_ColorTable12 = Color.FromArgb(255, BizareColorInvertor(Color.FromArgb(y_cmd)))
                Catch
                    CMD_ColorTable12 = _Def.CMD_ColorTable12
                End Try

                Try
                    y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console", "ColorTable13", BizareColorInvertor(_Def.CMD_ColorTable13).ToArgb)
                    CMD_ColorTable13 = Color.FromArgb(255, BizareColorInvertor(Color.FromArgb(y_cmd)))
                Catch
                    CMD_ColorTable13 = _Def.CMD_ColorTable13
                End Try

                Try
                    y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console", "ColorTable14", BizareColorInvertor(_Def.CMD_ColorTable14).ToArgb)
                    CMD_ColorTable14 = Color.FromArgb(255, BizareColorInvertor(Color.FromArgb(y_cmd)))
                Catch
                    CMD_ColorTable14 = _Def.CMD_ColorTable14
                End Try

                Try
                    y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console", "ColorTable15", BizareColorInvertor(_Def.CMD_ColorTable15).ToArgb)
                    CMD_ColorTable15 = Color.FromArgb(255, BizareColorInvertor(Color.FromArgb(y_cmd)))
                Catch
                    CMD_ColorTable15 = _Def.CMD_ColorTable15
                End Try

                Try
                    y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console", "PopupColors", Convert.ToInt32(_Def.CMD_PopupBackground.ToString("X") & _Def.CMD_PopupForeground.ToString("X"), 16))
                    Dim d As String = CInt(y_cmd).ToString("X")

                    If d.Count = 1 Then d = 0 & d
                    CMD_PopupBackground = Convert.ToInt32(d.Chars(0), 16)
                    CMD_PopupForeground = Convert.ToInt32(d.Chars(1), 16)
                Catch
                    CMD_PopupBackground = _Def.CMD_PopupBackground
                    CMD_PopupForeground = _Def.CMD_PopupForeground
                End Try

                Try
                    y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console", "ScreenColors", Convert.ToInt32(_Def.CMD_ScreenColorsBackground.ToString("X") & _Def.CMD_ScreenColorsForeground.ToString("X"), 16))
                    Dim d As String = CInt(y_cmd).ToString("X")

                    If d.Count = 1 Then d = 0 & d
                    CMD_ScreenColorsBackground = Convert.ToInt32(d.Chars(0), 16)
                    CMD_ScreenColorsForeground = Convert.ToInt32(d.Chars(1), 16)
                Catch
                    CMD_ScreenColorsBackground = _Def.CMD_ScreenColorsBackground
                    CMD_ScreenColorsForeground = _Def.CMD_ScreenColorsForeground
                End Try

                Try
                    y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console", "CursorSize", 25)
                    CMD_CursorSize = y_cmd
                Catch
                    CMD_CursorSize = 25
                End Try

                Try
                    y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console", "FaceName", _Def.CMD_FaceName)

                    If IsFontInstalled(y_cmd) Then
                        CMD_FaceName = y_cmd
                    Else
                        CMD_FaceName = _Def.CMD_FaceName
                    End If

                Catch
                    CMD_FaceName = _Def.CMD_FaceName
                End Try

                Try
                    y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console", "FontFamily", If(Not _Def.CMD_FontRaster, 54, 1))
                    CMD_FontRaster = If(y_cmd = 1 Or y_cmd = 0 Or y_cmd = 48, True, False)
                    If CMD_FaceName.ToLower = "terminal" Then CMD_FontRaster = True
                Catch
                    CMD_FontRaster = _Def.CMD_FontRaster
                End Try

                Try
                    y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console", "FontSize", _Def.CMD_FontSize)
                    If y_cmd = 0 And Not CMD_FontRaster Then CMD_FontSize = _Def.CMD_FontSize Else CMD_FontSize = y_cmd
                Catch
                    CMD_FontSize = _Def.CMD_FontSize
                End Try


                Try
                    y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console", "FontWeight", 400)
                    CMD_FontWeight = y_cmd
                Catch
                    CMD_FontWeight = 400
                End Try

                If My.W10_1909 Then
                    Try
                        y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console", "CursorColor", BizareColorInvertor(Color.White).ToArgb)
                        CMD_1909_CursorColor = Color.FromArgb(255, BizareColorInvertor(Color.FromArgb(y_cmd)))
                    Catch
                        CMD_1909_CursorColor = Color.White
                    End Try

                    Try
                        y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console", "CursorType", 1)
                        CMD_1909_CursorType = y_cmd
                    Catch
                        CMD_1909_CursorType = 1
                    End Try

                    Try
                        y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console", "ForceV2", True)
                        CMD_1909_ForceV2 = y_cmd
                    Catch
                        CMD_1909_ForceV2 = True
                    End Try

                    Try
                        y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console", "LineSelection", False)
                        CMD_1909_LineSelection = y_cmd
                    Catch
                        CMD_1909_LineSelection = False
                    End Try


                    Try
                        y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console", "TerminalScrolling", False)
                        CMD_1909_TerminalScrolling = y_cmd
                    Catch
                        CMD_1909_TerminalScrolling = False
                    End Try


                    Try
                        y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console", "WindowAlpha", 255)
                        CMD_1909_WindowAlpha = y_cmd
                    Catch
                        CMD_1909_WindowAlpha = 255
                    End Try
                End If
#End Region

#Region "PowerShell x86"
                If IO.Directory.Exists(Environment.GetEnvironmentVariable("WINDIR") & "\System32\WindowsPowerShell\v1.0") Then
                    Try
                        Registry.CurrentUser.CreateSubKey("Console\%SystemRoot%_System32_WindowsPowerShell_v1.0_powershell.exe", True).Close()
                    Catch

                    End Try

                    Try
                        y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\%SystemRoot%_System32_WindowsPowerShell_v1.0_powershell.exe", "ColorTable00", BizareColorInvertor(CMD_ColorTable00).ToArgb)
                        PS_32_ColorTable00 = Color.FromArgb(255, BizareColorInvertor(Color.FromArgb(y_cmd)))
                    Catch
                        PS_32_ColorTable00 = CMD_ColorTable00
                    End Try

                    Try
                        y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\%SystemRoot%_System32_WindowsPowerShell_v1.0_powershell.exe", "ColorTable01", BizareColorInvertor(CMD_ColorTable01).ToArgb)
                        PS_32_ColorTable01 = Color.FromArgb(255, BizareColorInvertor(Color.FromArgb(y_cmd)))
                    Catch
                        PS_32_ColorTable01 = CMD_ColorTable01
                    End Try

                    Try
                        y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\%SystemRoot%_System32_WindowsPowerShell_v1.0_powershell.exe", "ColorTable02", BizareColorInvertor(CMD_ColorTable02).ToArgb)
                        PS_32_ColorTable02 = Color.FromArgb(255, BizareColorInvertor(Color.FromArgb(y_cmd)))
                    Catch
                        PS_32_ColorTable02 = CMD_ColorTable02
                    End Try

                    Try
                        y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\%SystemRoot%_System32_WindowsPowerShell_v1.0_powershell.exe", "ColorTable03", BizareColorInvertor(CMD_ColorTable03).ToArgb)
                        PS_32_ColorTable03 = Color.FromArgb(255, BizareColorInvertor(Color.FromArgb(y_cmd)))
                    Catch
                        PS_32_ColorTable03 = CMD_ColorTable03
                    End Try

                    Try
                        y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\%SystemRoot%_System32_WindowsPowerShell_v1.0_powershell.exe", "ColorTable04", BizareColorInvertor(CMD_ColorTable04).ToArgb)
                        PS_32_ColorTable04 = Color.FromArgb(255, BizareColorInvertor(Color.FromArgb(y_cmd)))
                    Catch
                        PS_32_ColorTable04 = CMD_ColorTable04
                    End Try

                    Try
                        y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\%SystemRoot%_System32_WindowsPowerShell_v1.0_powershell.exe", "ColorTable05", BizareColorInvertor(_Def.PS_32_ColorTable05).ToArgb)
                        PS_32_ColorTable05 = Color.FromArgb(255, BizareColorInvertor(Color.FromArgb(y_cmd)))
                    Catch
                        PS_32_ColorTable05 = _Def.PS_32_ColorTable05
                    End Try

                    Try
                        y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\%SystemRoot%_System32_WindowsPowerShell_v1.0_powershell.exe", "ColorTable06", BizareColorInvertor(_Def.PS_32_ColorTable06).ToArgb)
                        PS_32_ColorTable06 = Color.FromArgb(255, BizareColorInvertor(Color.FromArgb(y_cmd)))
                    Catch
                        PS_32_ColorTable06 = _Def.PS_32_ColorTable06
                    End Try

                    Try
                        y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\%SystemRoot%_System32_WindowsPowerShell_v1.0_powershell.exe", "ColorTable07", BizareColorInvertor(CMD_ColorTable07).ToArgb)
                        PS_32_ColorTable07 = Color.FromArgb(255, BizareColorInvertor(Color.FromArgb(y_cmd)))
                    Catch
                        PS_32_ColorTable07 = CMD_ColorTable07
                    End Try

                    Try
                        y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\%SystemRoot%_System32_WindowsPowerShell_v1.0_powershell.exe", "ColorTable08", BizareColorInvertor(CMD_ColorTable08).ToArgb)
                        PS_32_ColorTable08 = Color.FromArgb(255, BizareColorInvertor(Color.FromArgb(y_cmd)))
                    Catch
                        PS_32_ColorTable08 = CMD_ColorTable08
                    End Try

                    Try
                        y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\%SystemRoot%_System32_WindowsPowerShell_v1.0_powershell.exe", "ColorTable09", BizareColorInvertor(CMD_ColorTable09).ToArgb)
                        PS_32_ColorTable09 = Color.FromArgb(255, BizareColorInvertor(Color.FromArgb(y_cmd)))
                    Catch
                        PS_32_ColorTable09 = CMD_ColorTable09
                    End Try

                    Try
                        y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\%SystemRoot%_System32_WindowsPowerShell_v1.0_powershell.exe", "ColorTable10", BizareColorInvertor(CMD_ColorTable10).ToArgb)
                        PS_32_ColorTable10 = Color.FromArgb(255, BizareColorInvertor(Color.FromArgb(y_cmd)))
                    Catch
                        PS_32_ColorTable10 = CMD_ColorTable10
                    End Try

                    Try
                        y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\%SystemRoot%_System32_WindowsPowerShell_v1.0_powershell.exe", "ColorTable11", BizareColorInvertor(CMD_ColorTable11).ToArgb)
                        PS_32_ColorTable11 = Color.FromArgb(255, BizareColorInvertor(Color.FromArgb(y_cmd)))
                    Catch
                        PS_32_ColorTable11 = CMD_ColorTable11
                    End Try

                    Try
                        y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\%SystemRoot%_System32_WindowsPowerShell_v1.0_powershell.exe", "ColorTable12", BizareColorInvertor(CMD_ColorTable12).ToArgb)
                        PS_32_ColorTable12 = Color.FromArgb(255, BizareColorInvertor(Color.FromArgb(y_cmd)))
                    Catch
                        PS_32_ColorTable12 = CMD_ColorTable12
                    End Try

                    Try
                        y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\%SystemRoot%_System32_WindowsPowerShell_v1.0_powershell.exe", "ColorTable13", BizareColorInvertor(CMD_ColorTable13).ToArgb)
                        PS_32_ColorTable13 = Color.FromArgb(255, BizareColorInvertor(Color.FromArgb(y_cmd)))
                    Catch
                        PS_32_ColorTable13 = CMD_ColorTable13
                    End Try

                    Try
                        y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\%SystemRoot%_System32_WindowsPowerShell_v1.0_powershell.exe", "ColorTable14", BizareColorInvertor(CMD_ColorTable14).ToArgb)
                        PS_32_ColorTable14 = Color.FromArgb(255, BizareColorInvertor(Color.FromArgb(y_cmd)))
                    Catch
                        PS_32_ColorTable14 = CMD_ColorTable14
                    End Try

                    Try
                        y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\%SystemRoot%_System32_WindowsPowerShell_v1.0_powershell.exe", "ColorTable15", BizareColorInvertor(CMD_ColorTable15).ToArgb)
                        PS_32_ColorTable15 = Color.FromArgb(255, BizareColorInvertor(Color.FromArgb(y_cmd)))
                    Catch
                        PS_32_ColorTable15 = CMD_ColorTable15
                    End Try

                    Try
                        y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\%SystemRoot%_System32_WindowsPowerShell_v1.0_powershell.exe", "PopupColors", Convert.ToInt32(_Def.PS_32_PopupBackground.ToString("X") & _Def.PS_32_PopupForeground.ToString("X"), 16))
                        Dim d As String = CInt(y_cmd).ToString("X")

                        If d.Count = 1 Then d = 0 & d
                        PS_32_PopupBackground = Convert.ToInt32(d.Chars(0), 16)
                        PS_32_PopupForeground = Convert.ToInt32(d.Chars(1), 16)
                    Catch
                        PS_32_PopupBackground = _Def.PS_32_PopupBackground
                        PS_32_PopupForeground = _Def.PS_32_PopupForeground
                    End Try

                    Try
                        y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\%SystemRoot%_System32_WindowsPowerShell_v1.0_powershell.exe", "ScreenColors", Convert.ToInt32(_Def.PS_32_ScreenColorsBackground.ToString("X") & _Def.PS_32_ScreenColorsForeground.ToString("X"), 16))
                        Dim d As String = CInt(y_cmd).ToString("X")

                        If d.Count = 1 Then d = 0 & d
                        PS_32_ScreenColorsBackground = Convert.ToInt32(d.Chars(0), 16)
                        PS_32_ScreenColorsForeground = Convert.ToInt32(d.Chars(1), 16)
                    Catch
                        PS_32_ScreenColorsBackground = _Def.PS_32_ScreenColorsBackground
                        PS_32_ScreenColorsForeground = _Def.PS_32_ScreenColorsForeground
                    End Try

                    Try
                        y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\%SystemRoot%_System32_WindowsPowerShell_v1.0_powershell.exe", "CursorSize", 25)
                        PS_32_CursorSize = y_cmd
                    Catch
                        PS_32_CursorSize = 25
                    End Try

                    Try
                        y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\%SystemRoot%_System32_WindowsPowerShell_v1.0_powershell.exe", "FaceName", _Def.PS_32_FaceName)
                        If IsFontInstalled(y_cmd) Then
                            PS_32_FaceName = y_cmd
                        Else
                            PS_32_FaceName = _Def.PS_32_FaceName
                        End If
                    Catch
                        PS_32_FaceName = _Def.PS_32_FaceName
                    End Try

                    Try
                        y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\%SystemRoot%_System32_WindowsPowerShell_v1.0_powershell.exe", "FontFamily", If(Not _Def.PS_32_FontRaster, 54, 1))
                        PS_32_FontRaster = If(y_cmd = 1 Or y_cmd = 0 Or y_cmd = 48, True, False)
                        If PS_32_FaceName.ToLower = "terminal" Then PS_32_FontRaster = True
                    Catch
                        PS_32_FontRaster = _Def.PS_32_FontRaster
                    End Try

                    Try
                        y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\%SystemRoot%_System32_WindowsPowerShell_v1.0_powershell.exe", "FontSize", _Def.PS_32_FontSize)
                        If y_cmd = 0 And Not PS_32_FontRaster Then PS_32_FontSize = _Def.PS_32_FontSize Else PS_32_FontSize = y_cmd
                    Catch
                        PS_32_FontSize = _Def.PS_32_FontSize
                    End Try

                    Try
                        y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\%SystemRoot%_System32_WindowsPowerShell_v1.0_powershell.exe", "FontWeight", 400)
                        PS_32_FontWeight = y_cmd
                    Catch
                        PS_32_FontWeight = 400
                    End Try

                    If My.W10_1909 Then
                        Try
                            y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\%SystemRoot%_System32_WindowsPowerShell_v1.0_powershell.exe", "CursorColor", BizareColorInvertor(Color.White).ToArgb)
                            PS_32_1909_CursorColor = Color.FromArgb(255, BizareColorInvertor(Color.FromArgb(y_cmd)))
                        Catch
                            PS_32_1909_CursorColor = Color.White
                        End Try

                        Try
                            y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\%SystemRoot%_System32_WindowsPowerShell_v1.0_powershell.exe", "CursorType", 1)
                            PS_32_1909_CursorType = y_cmd
                        Catch
                            PS_32_1909_CursorType = 1
                        End Try

                        Try
                            y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\%SystemRoot%_System32_WindowsPowerShell_v1.0_powershell.exe", "ForceV2", True)
                            PS_32_1909_ForceV2 = y_cmd
                        Catch
                            PS_32_1909_ForceV2 = True
                        End Try

                        Try
                            y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\%SystemRoot%_System32_WindowsPowerShell_v1.0_powershell.exe", "LineSelection", False)
                            PS_32_1909_LineSelection = y_cmd
                        Catch
                            PS_32_1909_LineSelection = False
                        End Try


                        Try
                            y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\%SystemRoot%_System32_WindowsPowerShell_v1.0_powershell.exe", "TerminalScrolling", False)
                            PS_32_1909_TerminalScrolling = y_cmd
                        Catch
                            PS_32_1909_TerminalScrolling = False
                        End Try


                        Try
                            y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\%SystemRoot%_System32_WindowsPowerShell_v1.0_powershell.exe", "WindowAlpha", 255)
                            PS_32_1909_WindowAlpha = y_cmd
                        Catch
                            PS_32_1909_WindowAlpha = 255
                        End Try
                    End If
                End If
#End Region

#Region "PowerShell x64"
                If IO.Directory.Exists(Environment.GetEnvironmentVariable("WINDIR") & "\SysWOW64\WindowsPowerShell\v1.0") Then
                    Try
                        Registry.CurrentUser.CreateSubKey("Console\%SystemRoot%_SysWOW64_WindowsPowerShell_v1.0_powershell.exe", True).Close()
                    Catch

                    End Try

                    Try
                        y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\%SystemRoot%_SysWOW64_WindowsPowerShell_v1.0_powershell.exe", "ColorTable00", BizareColorInvertor(CMD_ColorTable00).ToArgb)
                        PS_64_ColorTable00 = Color.FromArgb(255, BizareColorInvertor(Color.FromArgb(y_cmd)))
                    Catch
                        PS_64_ColorTable00 = CMD_ColorTable00
                    End Try

                    Try
                        y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\%SystemRoot%_SysWOW64_WindowsPowerShell_v1.0_powershell.exe", "ColorTable01", BizareColorInvertor(CMD_ColorTable01).ToArgb)
                        PS_64_ColorTable01 = Color.FromArgb(255, BizareColorInvertor(Color.FromArgb(y_cmd)))
                    Catch
                        PS_64_ColorTable01 = CMD_ColorTable01
                    End Try

                    Try
                        y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\%SystemRoot%_SysWOW64_WindowsPowerShell_v1.0_powershell.exe", "ColorTable02", BizareColorInvertor(CMD_ColorTable02).ToArgb)
                        PS_64_ColorTable02 = Color.FromArgb(255, BizareColorInvertor(Color.FromArgb(y_cmd)))
                    Catch
                        PS_64_ColorTable02 = CMD_ColorTable02
                    End Try

                    Try
                        y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\%SystemRoot%_SysWOW64_WindowsPowerShell_v1.0_powershell.exe", "ColorTable03", BizareColorInvertor(CMD_ColorTable03).ToArgb)
                        PS_64_ColorTable03 = Color.FromArgb(255, BizareColorInvertor(Color.FromArgb(y_cmd)))
                    Catch
                        PS_64_ColorTable03 = CMD_ColorTable03
                    End Try

                    Try
                        y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\%SystemRoot%_SysWOW64_WindowsPowerShell_v1.0_powershell.exe", "ColorTable04", BizareColorInvertor(CMD_ColorTable04).ToArgb)
                        PS_64_ColorTable04 = Color.FromArgb(255, BizareColorInvertor(Color.FromArgb(y_cmd)))
                    Catch
                        PS_64_ColorTable04 = CMD_ColorTable04
                    End Try

                    Try
                        y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\%SystemRoot%_SysWOW64_WindowsPowerShell_v1.0_powershell.exe", "ColorTable05", BizareColorInvertor(_Def.PS_64_ColorTable05).ToArgb)
                        PS_64_ColorTable05 = Color.FromArgb(255, BizareColorInvertor(Color.FromArgb(y_cmd)))
                    Catch
                        PS_64_ColorTable05 = _Def.PS_64_ColorTable05
                    End Try

                    Try
                        y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\%SystemRoot%_SysWOW64_WindowsPowerShell_v1.0_powershell.exe", "ColorTable06", BizareColorInvertor(_Def.PS_64_ColorTable06).ToArgb)
                        PS_64_ColorTable06 = Color.FromArgb(255, BizareColorInvertor(Color.FromArgb(y_cmd)))
                    Catch
                        PS_64_ColorTable06 = _Def.PS_64_ColorTable06
                    End Try

                    Try
                        y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\%SystemRoot%_SysWOW64_WindowsPowerShell_v1.0_powershell.exe", "ColorTable07", BizareColorInvertor(CMD_ColorTable07).ToArgb)
                        PS_64_ColorTable07 = Color.FromArgb(255, BizareColorInvertor(Color.FromArgb(y_cmd)))
                    Catch
                        PS_64_ColorTable07 = CMD_ColorTable07
                    End Try

                    Try
                        y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\%SystemRoot%_SysWOW64_WindowsPowerShell_v1.0_powershell.exe", "ColorTable08", BizareColorInvertor(CMD_ColorTable08).ToArgb)
                        PS_64_ColorTable08 = Color.FromArgb(255, BizareColorInvertor(Color.FromArgb(y_cmd)))
                    Catch
                        PS_64_ColorTable08 = CMD_ColorTable08
                    End Try

                    Try
                        y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\%SystemRoot%_SysWOW64_WindowsPowerShell_v1.0_powershell.exe", "ColorTable09", BizareColorInvertor(CMD_ColorTable09).ToArgb)
                        PS_64_ColorTable09 = Color.FromArgb(255, BizareColorInvertor(Color.FromArgb(y_cmd)))
                    Catch
                        PS_64_ColorTable09 = CMD_ColorTable09
                    End Try

                    Try
                        y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\%SystemRoot%_SysWOW64_WindowsPowerShell_v1.0_powershell.exe", "ColorTable10", BizareColorInvertor(CMD_ColorTable10).ToArgb)
                        PS_64_ColorTable10 = Color.FromArgb(255, BizareColorInvertor(Color.FromArgb(y_cmd)))
                    Catch
                        PS_64_ColorTable10 = CMD_ColorTable10
                    End Try

                    Try
                        y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\%SystemRoot%_SysWOW64_WindowsPowerShell_v1.0_powershell.exe", "ColorTable11", BizareColorInvertor(CMD_ColorTable11).ToArgb)
                        PS_64_ColorTable11 = Color.FromArgb(255, BizareColorInvertor(Color.FromArgb(y_cmd)))
                    Catch
                        PS_64_ColorTable11 = CMD_ColorTable11
                    End Try

                    Try
                        y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\%SystemRoot%_SysWOW64_WindowsPowerShell_v1.0_powershell.exe", "ColorTable12", BizareColorInvertor(CMD_ColorTable12).ToArgb)
                        PS_64_ColorTable12 = Color.FromArgb(255, BizareColorInvertor(Color.FromArgb(y_cmd)))
                    Catch
                        PS_64_ColorTable12 = CMD_ColorTable12
                    End Try

                    Try
                        y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\%SystemRoot%_SysWOW64_WindowsPowerShell_v1.0_powershell.exe", "ColorTable13", BizareColorInvertor(CMD_ColorTable13).ToArgb)
                        PS_64_ColorTable13 = Color.FromArgb(255, BizareColorInvertor(Color.FromArgb(y_cmd)))
                    Catch
                        PS_64_ColorTable13 = CMD_ColorTable13
                    End Try

                    Try
                        y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\%SystemRoot%_SysWOW64_WindowsPowerShell_v1.0_powershell.exe", "ColorTable14", BizareColorInvertor(CMD_ColorTable14).ToArgb)
                        PS_64_ColorTable14 = Color.FromArgb(255, BizareColorInvertor(Color.FromArgb(y_cmd)))
                    Catch
                        PS_64_ColorTable14 = CMD_ColorTable14
                    End Try

                    Try
                        y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\%SystemRoot%_SysWOW64_WindowsPowerShell_v1.0_powershell.exe", "ColorTable15", BizareColorInvertor(CMD_ColorTable15).ToArgb)
                        PS_64_ColorTable15 = Color.FromArgb(255, BizareColorInvertor(Color.FromArgb(y_cmd)))
                    Catch
                        PS_64_ColorTable15 = CMD_ColorTable15
                    End Try

                    Try
                        y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\%SystemRoot%_SysWOW64_WindowsPowerShell_v1.0_powershell.exe", "PopupColors", Convert.ToInt32(_Def.PS_64_PopupBackground.ToString("X") & _Def.PS_64_PopupForeground.ToString("X"), 16))
                        Dim d As String = CInt(y_cmd).ToString("X")

                        If d.Count = 1 Then d = 0 & d
                        PS_64_PopupBackground = Convert.ToInt32(d.Chars(0), 16)
                        PS_64_PopupForeground = Convert.ToInt32(d.Chars(1), 16)
                    Catch
                        PS_64_PopupBackground = _Def.PS_64_PopupBackground
                        PS_64_PopupForeground = _Def.PS_64_PopupForeground
                    End Try

                    Try
                        y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\%SystemRoot%_SysWOW64_WindowsPowerShell_v1.0_powershell.exe", "ScreenColors", Convert.ToInt32(_Def.PS_64_ScreenColorsBackground.ToString("X") & _Def.PS_64_ScreenColorsForeground.ToString("X"), 16))
                        Dim d As String = CInt(y_cmd).ToString("X")

                        If d.Count = 1 Then d = 0 & d
                        PS_64_ScreenColorsBackground = Convert.ToInt32(d.Chars(0), 16)
                        PS_64_ScreenColorsForeground = Convert.ToInt32(d.Chars(1), 16)
                    Catch
                        PS_64_ScreenColorsBackground = _Def.PS_64_ScreenColorsBackground
                        PS_64_ScreenColorsForeground = _Def.PS_64_ScreenColorsForeground
                    End Try

                    Try
                        y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\%SystemRoot%_SysWOW64_WindowsPowerShell_v1.0_powershell.exe", "CursorSize", 25)
                        PS_64_CursorSize = y_cmd
                    Catch
                        PS_64_CursorSize = 25
                    End Try

                    Try
                        y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\%SystemRoot%_SysWOW64_WindowsPowerShell_v1.0_powershell.exe", "FaceName", _Def.PS_64_FaceName)

                        If IsFontInstalled(y_cmd) Then
                            PS_64_FaceName = y_cmd
                        Else
                            PS_64_FaceName = _Def.PS_64_FaceName
                        End If

                    Catch
                        PS_64_FaceName = _Def.PS_64_FaceName
                    End Try

                    Try
                        y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\%SystemRoot%_SysWOW64_WindowsPowerShell_v1.0_powershell.exe", "FontFamily", If(Not _Def.PS_64_FontRaster, 54, 1))
                        PS_64_FontRaster = If(y_cmd = 1 Or y_cmd = 0 Or y_cmd = 48, True, False)
                        If PS_64_FaceName.ToLower = "terminal" Then PS_64_FontRaster = True
                    Catch
                        PS_64_FontRaster = _Def.PS_64_FontRaster
                    End Try

                    Try
                        y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\%SystemRoot%_SysWOW64_WindowsPowerShell_v1.0_powershell.exe", "FontSize", CMD_FontSize)
                        If y_cmd = 0 And Not PS_64_FontRaster Then PS_64_FontSize = _Def.PS_64_FontSize Else PS_64_FontSize = y_cmd
                    Catch
                        PS_64_FontSize = CMD_FontSize
                    End Try

                    Try
                        y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\%SystemRoot%_SysWOW64_WindowsPowerShell_v1.0_powershell.exe", "FontWeight", 400)
                        PS_64_FontWeight = y_cmd
                    Catch
                        PS_64_FontWeight = 400
                    End Try

                    If My.W10_1909 Then
                        Try
                            y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\%SystemRoot%_SysWOW64_WindowsPowerShell_v1.0_powershell.exe", "CursorColor", BizareColorInvertor(Color.White).ToArgb)
                            PS_64_1909_CursorColor = Color.FromArgb(255, BizareColorInvertor(Color.FromArgb(y_cmd)))
                        Catch
                            PS_64_1909_CursorColor = Color.White
                        End Try

                        Try
                            y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\%SystemRoot%_SysWOW64_WindowsPowerShell_v1.0_powershell.exe", "CursorType", 1)
                            PS_64_1909_CursorType = y_cmd
                        Catch
                            PS_64_1909_CursorType = 1
                        End Try

                        Try
                            y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\%SystemRoot%_SysWOW64_WindowsPowerShell_v1.0_powershell.exe", "ForceV2", True)
                            PS_64_1909_ForceV2 = y_cmd
                        Catch
                            PS_64_1909_ForceV2 = True
                        End Try

                        Try
                            y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\%SystemRoot%_SysWOW64_WindowsPowerShell_v1.0_powershell.exe", "LineSelection", False)
                            PS_64_1909_LineSelection = y_cmd
                        Catch
                            PS_64_1909_LineSelection = False
                        End Try


                        Try
                            y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\%SystemRoot%_SysWOW64_WindowsPowerShell_v1.0_powershell.exe", "TerminalScrolling", False)
                            PS_64_1909_TerminalScrolling = y_cmd
                        Catch
                            PS_64_1909_TerminalScrolling = False
                        End Try


                        Try
                            y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\%SystemRoot%_SysWOW64_WindowsPowerShell_v1.0_powershell.exe", "WindowAlpha", 255)
                            PS_64_1909_WindowAlpha = y_cmd
                        Catch
                            PS_64_1909_WindowAlpha = 255
                        End Try
                    End If
                End If
#End Region

#Region "Windows Terminal"
                If My.W10 Or My.W11 Then
                    Dim TerDir As String
                    Dim TerPreDir As String

                    If Not My.Application._Settings.Terminal_Path_Deflection Then
                        TerDir = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) & "\AppData\Local\Packages\Microsoft.WindowsTerminal_8wekyb3d8bbwe\LocalState\settings.json"
                        TerPreDir = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) & "\AppData\Local\Packages\Microsoft.WindowsTerminalPreview_8wekyb3d8bbwe\LocalState\settings.json"
                    Else
                        If IO.File.Exists(My.Application._Settings.Terminal_Stable_Path) Then
                            TerDir = My.Application._Settings.Terminal_Stable_Path
                        Else
                            TerDir = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) & "\AppData\Local\Packages\Microsoft.WindowsTerminal_8wekyb3d8bbwe\LocalState\settings.json"
                        End If

                        If IO.File.Exists(My.Application._Settings.Terminal_Preview_Path) Then
                            TerPreDir = My.Application._Settings.Terminal_Preview_Path
                        Else
                            TerPreDir = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) & "\AppData\Local\Packages\Microsoft.WindowsTerminalPreview_8wekyb3d8bbwe\LocalState\settings.json"
                        End If
                    End If


                    If IO.File.Exists(TerDir) Then
                        Terminal = New WinTerminal(TerDir, WinTerminal.Mode.JSONFile)
                    Else
                        Terminal = New WinTerminal("", WinTerminal.Mode.Empty)
                    End If

                    If IO.File.Exists(TerPreDir) Then
                        TerminalPreview = New WinTerminal(TerPreDir, WinTerminal.Mode.JSONFile, WinTerminal.Version.Preview)
                    Else
                        TerminalPreview = New WinTerminal("", WinTerminal.Mode.Empty, WinTerminal.Version.Preview)
                    End If

                Else
                    Terminal = New WinTerminal("", WinTerminal.Mode.Empty)
                    TerminalPreview = New WinTerminal("", WinTerminal.Mode.Empty, WinTerminal.Version.Preview)
                End If
#End Region
#End Region

#Region "Cursors"
                Dim rMain As RegistryKey = Registry.CurrentUser.CreateSubKey("Software\WinPaletter\Cursors")
                Cursor_Enabled = rMain.GetValue("", False)
                Dim r As RegistryKey = rMain

                r = rMain.CreateSubKey("Arrow")
                r = rMain.CreateSubKey("Help")
                r = rMain.CreateSubKey("AppLoading")
                r = rMain.CreateSubKey("Busy")
                r = rMain.CreateSubKey("Move")
                r = rMain.CreateSubKey("NS")
                r = rMain.CreateSubKey("EW")
                r = rMain.CreateSubKey("NESW")
                r = rMain.CreateSubKey("NWSE")
                r = rMain.CreateSubKey("Up")
                r = rMain.CreateSubKey("Pen")
                r = rMain.CreateSubKey("None")
                r = rMain.CreateSubKey("Link")
                r = rMain.CreateSubKey("Pin")
                r = rMain.CreateSubKey("Person")
                r = rMain.CreateSubKey("IBeam")
                r = rMain.CreateSubKey("Cross")

#Region "Arrow"
                r = rMain.OpenSubKey("Arrow")
                Cursor_Arrow_PrimaryColor1 = Color.FromArgb(r.GetValue("PrimaryColor1", Color.White.ToArgb))
                Cursor_Arrow_PrimaryColor2 = Color.FromArgb(r.GetValue("PrimaryColor2", Color.White.ToArgb))
                Cursor_Arrow_SecondaryColor1 = Color.FromArgb(r.GetValue("SecondaryColor1", Color.FromArgb(64, 65, 75).ToArgb))
                Cursor_Arrow_SecondaryColor2 = Color.FromArgb(r.GetValue("SecondaryColor2", Color.FromArgb(64, 65, 75).ToArgb))

                Cursor_Arrow_PrimaryColorGradient = r.GetValue("PrimaryColorGradient", False)
                Cursor_Arrow_SecondaryColorGradient = r.GetValue("SecondaryColorGradient", False)
                Cursor_Arrow_PrimaryColorNoise = r.GetValue("PrimaryColorNoise", False)
                Cursor_Arrow_SecondaryColorNoise = r.GetValue("SecondaryColorNoise", False)

                Cursor_Arrow_PrimaryColorGradientMode = ReturnGradientModeFromString(r.GetValue("PrimaryColorGradientMode", "Vertical"))
                Cursor_Arrow_SecondaryColorGradientMode = ReturnGradientModeFromString(r.GetValue("SecondaryColorGradientMode", "Vertical"))

                Cursor_Arrow_PrimaryColorNoiseOpacity = r.GetValue("PrimaryColorNoiseOpacity", 25) / 100
                Cursor_Arrow_SecondaryColorNoiseOpacity = r.GetValue("SecondaryColorNoiseOpacity", 25) / 100
#End Region

#Region "Help"
                r = rMain.OpenSubKey("Help")
                Cursor_Help_PrimaryColor1 = Color.FromArgb(r.GetValue("PrimaryColor1", Color.White.ToArgb))
                Cursor_Help_PrimaryColor2 = Color.FromArgb(r.GetValue("PrimaryColor2", Color.White.ToArgb))
                Cursor_Help_SecondaryColor1 = Color.FromArgb(r.GetValue("SecondaryColor1", Color.FromArgb(64, 65, 75).ToArgb))
                Cursor_Help_SecondaryColor2 = Color.FromArgb(r.GetValue("SecondaryColor2", Color.FromArgb(64, 65, 75).ToArgb))

                Cursor_Help_PrimaryColorGradient = r.GetValue("PrimaryColorGradient", False)
                Cursor_Help_PrimaryColorNoise = r.GetValue("PrimaryColorNoise", False)
                Cursor_Help_SecondaryColorGradient = r.GetValue("SecondaryColorGradient", False)
                Cursor_Help_SecondaryColorNoise = r.GetValue("SecondaryColorNoise", False)

                Cursor_Help_PrimaryColorGradientMode = ReturnGradientModeFromString(r.GetValue("PrimaryColorGradientMode", "Vertical"))
                Cursor_Help_SecondaryColorGradientMode = ReturnGradientModeFromString(r.GetValue("SecondaryColorGradientMode", "Vertical"))

                Cursor_Help_PrimaryColorNoiseOpacity = r.GetValue("PrimaryColorNoiseOpacity", 25) / 100
                Cursor_Help_SecondaryColorNoiseOpacity = r.GetValue("SecondaryColorNoiseOpacity", 25) / 100
#End Region

#Region "AppLoading"
                r = rMain.OpenSubKey("AppLoading")
                Cursor_AppLoading_PrimaryColor1 = Color.FromArgb(r.GetValue("PrimaryColor1", Color.White.ToArgb))
                Cursor_AppLoading_PrimaryColor2 = Color.FromArgb(r.GetValue("PrimaryColor2", Color.White.ToArgb))
                Cursor_AppLoading_SecondaryColor1 = Color.FromArgb(r.GetValue("SecondaryColor1", Color.FromArgb(64, 65, 75).ToArgb))
                Cursor_AppLoading_SecondaryColor2 = Color.FromArgb(r.GetValue("SecondaryColor2", Color.FromArgb(64, 65, 75).ToArgb))
                Cursor_AppLoading_LoadingCircleBack1 = Color.FromArgb(r.GetValue("LoadingCircleBack1", Color.FromArgb(42, 151, 243).ToArgb))
                Cursor_AppLoading_LoadingCircleBack2 = Color.FromArgb(r.GetValue("LoadingCircleBack2", Color.FromArgb(42, 151, 243).ToArgb))
                Cursor_AppLoading_LoadingCircleHot1 = Color.FromArgb(r.GetValue("LoadingCircleHot1", Color.FromArgb(37, 204, 255).ToArgb))
                Cursor_AppLoading_LoadingCircleHot2 = Color.FromArgb(r.GetValue("LoadingCircleHot2", Color.FromArgb(37, 204, 255).ToArgb))

                Cursor_AppLoading_PrimaryColorGradient = r.GetValue("PrimaryColorGradient", False)
                Cursor_AppLoading_SecondaryColorGradient = r.GetValue("SecondaryColorGradient", False)
                Cursor_AppLoading_LoadingCircleBackGradient = r.GetValue("LoadingCircleBackGradient", False)
                Cursor_AppLoading_LoadingCircleHotGradient = r.GetValue("LoadingCircleHotGradient", False)
                Cursor_AppLoading_PrimaryColorNoise = r.GetValue("PrimaryColorNoise", False)
                Cursor_AppLoading_SecondaryColorNoise = r.GetValue("SecondaryColorNoise", False)
                Cursor_AppLoading_LoadingCircleBackNoise = r.GetValue("LoadingCircleBackNoise", False)
                Cursor_AppLoading_LoadingCircleHotNoise = r.GetValue("LoadingCircleHotNoise", False)

                Cursor_AppLoading_PrimaryColorGradientMode = ReturnGradientModeFromString(r.GetValue("PrimaryColorGradientMode", "Circle"))
                Cursor_AppLoading_SecondaryColorGradientMode = ReturnGradientModeFromString(r.GetValue("SecondaryColorGradientMode", "Circle"))
                Cursor_AppLoading_LoadingCircleBackGradientMode = ReturnGradientModeFromString(r.GetValue("LoadingCircleBackGradientMode", "Circle"))
                Cursor_AppLoading_LoadingCircleHotGradientMode = ReturnGradientModeFromString(r.GetValue("LoadingCircleHotGradientMode", "Circle"))

                Cursor_AppLoading_PrimaryColorNoiseOpacity = r.GetValue("PrimaryColorNoiseOpacity", 25) / 100
                Cursor_AppLoading_SecondaryColorNoiseOpacity = r.GetValue("SecondaryColorNoiseOpacity", 25) / 100
                Cursor_AppLoading_LoadingCircleBackNoiseOpacity = r.GetValue("LoadingCircleBackNoiseOpacity", 25) / 100
                Cursor_AppLoading_LoadingCircleHotNoiseOpacity = r.GetValue("LoadingCircleHotNoiseOpacity", 25) / 100
#End Region

#Region "Busy"
                r = rMain.OpenSubKey("Busy")
                Cursor_Busy_LoadingCircleBack1 = Color.FromArgb(r.GetValue("LoadingCircleBack1", Color.FromArgb(42, 151, 243).ToArgb))
                Cursor_Busy_LoadingCircleBack2 = Color.FromArgb(r.GetValue("LoadingCircleBack2", Color.FromArgb(42, 151, 243).ToArgb))
                Cursor_Busy_LoadingCircleHot1 = Color.FromArgb(r.GetValue("LoadingCircleHot1", Color.FromArgb(37, 204, 255).ToArgb))
                Cursor_Busy_LoadingCircleHot2 = Color.FromArgb(r.GetValue("LoadingCircleHot2", Color.FromArgb(37, 204, 255).ToArgb))

                Cursor_Busy_LoadingCircleBackGradient = r.GetValue("LoadingCircleBackGradient", False)
                Cursor_Busy_LoadingCircleHotGradient = r.GetValue("LoadingCircleHotGradient", False)
                Cursor_Busy_LoadingCircleBackNoise = r.GetValue("LoadingCircleBackNoise", False)
                Cursor_Busy_LoadingCircleHotNoise = r.GetValue("LoadingCircleHotNoise", False)

                Cursor_Busy_LoadingCircleBackGradientMode = ReturnGradientModeFromString(r.GetValue("LoadingCircleBackGradientMode", "Circle"))
                Cursor_Busy_LoadingCircleHotGradientMode = ReturnGradientModeFromString(r.GetValue("LoadingCircleHotGradientMode", "Circle"))

                Cursor_Busy_LoadingCircleBackNoiseOpacity = r.GetValue("LoadingCircleBackNoiseOpacity", 25) / 100
                Cursor_Busy_LoadingCircleHotNoiseOpacity = r.GetValue("LoadingCircleHotNoiseOpacity", 25) / 100
#End Region

#Region "Move"
                r = rMain.OpenSubKey("Move")
                Cursor_Move_PrimaryColor1 = Color.FromArgb(r.GetValue("PrimaryColor1", Color.White.ToArgb))
                Cursor_Move_PrimaryColor2 = Color.FromArgb(r.GetValue("PrimaryColor2", Color.White.ToArgb))
                Cursor_Move_SecondaryColor1 = Color.FromArgb(r.GetValue("SecondaryColor1", Color.FromArgb(64, 65, 75).ToArgb))
                Cursor_Move_SecondaryColor2 = Color.FromArgb(r.GetValue("SecondaryColor2", Color.FromArgb(64, 65, 75).ToArgb))

                Cursor_Move_PrimaryColorGradient = r.GetValue("PrimaryColorGradient", False)
                Cursor_Move_SecondaryColorGradient = r.GetValue("SecondaryColorGradient", False)
                Cursor_Move_PrimaryColorNoise = r.GetValue("PrimaryColorNoise", False)
                Cursor_Move_SecondaryColorNoise = r.GetValue("SecondaryColorNoise", False)

                Cursor_Move_PrimaryColorGradientMode = ReturnGradientModeFromString(r.GetValue("PrimaryColorGradientMode", "Vertical"))
                Cursor_Move_SecondaryColorGradientMode = ReturnGradientModeFromString(r.GetValue("SecondaryColorGradientMode", "Vertical"))

                Cursor_Move_PrimaryColorNoiseOpacity = r.GetValue("PrimaryColorNoiseOpacity", 25) / 100
                Cursor_Move_SecondaryColorNoiseOpacity = r.GetValue("SecondaryColorNoiseOpacity", 25) / 100
#End Region

#Region "NS"
                r = rMain.OpenSubKey("NS")
                Cursor_NS_PrimaryColor1 = Color.FromArgb(r.GetValue("PrimaryColor1", Color.White.ToArgb))
                Cursor_NS_PrimaryColor2 = Color.FromArgb(r.GetValue("PrimaryColor2", Color.White.ToArgb))
                Cursor_NS_SecondaryColor1 = Color.FromArgb(r.GetValue("SecondaryColor1", Color.FromArgb(64, 65, 75).ToArgb))
                Cursor_NS_SecondaryColor2 = Color.FromArgb(r.GetValue("SecondaryColor2", Color.FromArgb(64, 65, 75).ToArgb))

                Cursor_NS_PrimaryColorGradient = r.GetValue("PrimaryColorGradient", False)
                Cursor_NS_SecondaryColorGradient = r.GetValue("SecondaryColorGradient", False)
                Cursor_NS_PrimaryColorNoise = r.GetValue("PrimaryColorNoise", False)
                Cursor_NS_SecondaryColorNoise = r.GetValue("SecondaryColorNoise", False)

                Cursor_NS_PrimaryColorGradientMode = ReturnGradientModeFromString(r.GetValue("PrimaryColorGradientMode", "Vertical"))
                Cursor_NS_SecondaryColorGradientMode = ReturnGradientModeFromString(r.GetValue("SecondaryColorGradientMode", "Vertical"))

                Cursor_NS_PrimaryColorNoiseOpacity = r.GetValue("PrimaryColorNoiseOpacity", 25) / 100
                Cursor_NS_SecondaryColorNoiseOpacity = r.GetValue("SecondaryColorNoiseOpacity", 25) / 100
#End Region

#Region "EW"
                r = rMain.OpenSubKey("EW")
                Cursor_EW_PrimaryColor1 = Color.FromArgb(r.GetValue("PrimaryColor1", Color.White.ToArgb))
                Cursor_EW_PrimaryColor2 = Color.FromArgb(r.GetValue("PrimaryColor2", Color.White.ToArgb))
                Cursor_EW_SecondaryColor1 = Color.FromArgb(r.GetValue("SecondaryColor1", Color.FromArgb(64, 65, 75).ToArgb))
                Cursor_EW_SecondaryColor2 = Color.FromArgb(r.GetValue("SecondaryColor2", Color.FromArgb(64, 65, 75).ToArgb))

                Cursor_EW_PrimaryColorGradient = r.GetValue("PrimaryColorGradient", False)
                Cursor_EW_SecondaryColorGradient = r.GetValue("SecondaryColorGradient", False)
                Cursor_EW_PrimaryColorNoise = r.GetValue("PrimaryColorNoise", False)
                Cursor_EW_SecondaryColorNoise = r.GetValue("SecondaryColorNoise", False)

                Cursor_EW_PrimaryColorGradientMode = ReturnGradientModeFromString(r.GetValue("PrimaryColorGradientMode", "Vertical"))
                Cursor_EW_SecondaryColorGradientMode = ReturnGradientModeFromString(r.GetValue("SecondaryColorGradientMode", "Vertical"))

                Cursor_EW_PrimaryColorNoiseOpacity = r.GetValue("PrimaryColorNoiseOpacity", 25) / 100
                Cursor_EW_SecondaryColorNoiseOpacity = r.GetValue("SecondaryColorNoiseOpacity", 25) / 100
#End Region

#Region "NESW"
                r = rMain.OpenSubKey("NESW")
                Cursor_NESW_PrimaryColor1 = Color.FromArgb(r.GetValue("PrimaryColor1", Color.White.ToArgb))
                Cursor_NESW_PrimaryColor2 = Color.FromArgb(r.GetValue("PrimaryColor2", Color.White.ToArgb))
                Cursor_NESW_SecondaryColor1 = Color.FromArgb(r.GetValue("SecondaryColor1", Color.FromArgb(64, 65, 75).ToArgb))
                Cursor_NESW_SecondaryColor2 = Color.FromArgb(r.GetValue("SecondaryColor2", Color.FromArgb(64, 65, 75).ToArgb))

                Cursor_NESW_PrimaryColorGradient = r.GetValue("PrimaryColorGradient", False)
                Cursor_NESW_SecondaryColorGradient = r.GetValue("SecondaryColorGradient", False)
                Cursor_NESW_PrimaryColorNoise = r.GetValue("PrimaryColorNoise", False)
                Cursor_NESW_SecondaryColorNoise = r.GetValue("SecondaryColorNoise", False)

                Cursor_NESW_PrimaryColorGradientMode = ReturnGradientModeFromString(r.GetValue("PrimaryColorGradientMode", "Vertical"))
                Cursor_NESW_SecondaryColorGradientMode = ReturnGradientModeFromString(r.GetValue("SecondaryColorGradientMode", "Vertical"))

                Cursor_NESW_PrimaryColorNoiseOpacity = r.GetValue("PrimaryColorNoiseOpacity", 25) / 100
                Cursor_NESW_SecondaryColorNoiseOpacity = r.GetValue("SecondaryColorNoiseOpacity", 25) / 100
#End Region

#Region "NWSE"
                r = rMain.OpenSubKey("NWSE")
                Cursor_NWSE_PrimaryColor1 = Color.FromArgb(r.GetValue("PrimaryColor1", Color.White.ToArgb))
                Cursor_NWSE_PrimaryColor2 = Color.FromArgb(r.GetValue("PrimaryColor2", Color.White.ToArgb))
                Cursor_NWSE_SecondaryColor1 = Color.FromArgb(r.GetValue("SecondaryColor1", Color.FromArgb(64, 65, 75).ToArgb))
                Cursor_NWSE_SecondaryColor2 = Color.FromArgb(r.GetValue("SecondaryColor2", Color.FromArgb(64, 65, 75).ToArgb))

                Cursor_NWSE_PrimaryColorGradient = r.GetValue("PrimaryColorGradient", False)
                Cursor_NWSE_SecondaryColorGradient = r.GetValue("SecondaryColorGradient", False)
                Cursor_NWSE_PrimaryColorNoise = r.GetValue("PrimaryColorNoise", False)
                Cursor_NWSE_SecondaryColorNoise = r.GetValue("SecondaryColorNoise", False)

                Cursor_NWSE_PrimaryColorGradientMode = ReturnGradientModeFromString(r.GetValue("PrimaryColorGradientMode", "Vertical"))
                Cursor_NWSE_SecondaryColorGradientMode = ReturnGradientModeFromString(r.GetValue("SecondaryColorGradientMode", "Vertical"))

                Cursor_NWSE_PrimaryColorNoiseOpacity = r.GetValue("PrimaryColorNoiseOpacity", 25) / 100
                Cursor_NWSE_SecondaryColorNoiseOpacity = r.GetValue("SecondaryColorNoiseOpacity", 25) / 100
#End Region

#Region "Up"
                r = rMain.OpenSubKey("Up")
                Cursor_Up_PrimaryColor1 = Color.FromArgb(r.GetValue("PrimaryColor1", Color.White.ToArgb))
                Cursor_Up_PrimaryColor2 = Color.FromArgb(r.GetValue("PrimaryColor2", Color.White.ToArgb))
                Cursor_Up_SecondaryColor1 = Color.FromArgb(r.GetValue("SecondaryColor1", Color.FromArgb(64, 65, 75).ToArgb))
                Cursor_Up_SecondaryColor2 = Color.FromArgb(r.GetValue("SecondaryColor2", Color.FromArgb(64, 65, 75).ToArgb))

                Cursor_Up_PrimaryColorGradient = r.GetValue("PrimaryColorGradient", False)
                Cursor_Up_SecondaryColorGradient = r.GetValue("SecondaryColorGradient", False)
                Cursor_Up_PrimaryColorNoise = r.GetValue("PrimaryColorNoise", False)
                Cursor_Up_SecondaryColorNoise = r.GetValue("SecondaryColorNoise", False)

                Cursor_Up_PrimaryColorGradientMode = ReturnGradientModeFromString(r.GetValue("PrimaryColorGradientMode", "Vertical"))
                Cursor_Up_SecondaryColorGradientMode = ReturnGradientModeFromString(r.GetValue("SecondaryColorGradientMode", "Vertical"))

                Cursor_Up_PrimaryColorNoiseOpacity = r.GetValue("PrimaryColorNoiseOpacity", 25) / 100
                Cursor_Up_SecondaryColorNoiseOpacity = r.GetValue("SecondaryColorNoiseOpacity", 25) / 100
#End Region

#Region "Pen"
                r = rMain.OpenSubKey("Pen")
                Cursor_Pen_PrimaryColor1 = Color.FromArgb(r.GetValue("PrimaryColor1", Color.White.ToArgb))
                Cursor_Pen_PrimaryColor2 = Color.FromArgb(r.GetValue("PrimaryColor2", Color.White.ToArgb))
                Cursor_Pen_SecondaryColor1 = Color.FromArgb(r.GetValue("SecondaryColor1", Color.FromArgb(64, 65, 75).ToArgb))
                Cursor_Pen_SecondaryColor2 = Color.FromArgb(r.GetValue("SecondaryColor2", Color.FromArgb(64, 65, 75).ToArgb))

                Cursor_Pen_PrimaryColorGradient = r.GetValue("PrimaryColorGradient", False)
                Cursor_Pen_SecondaryColorGradient = r.GetValue("SecondaryColorGradient", False)
                Cursor_Pen_PrimaryColorNoise = r.GetValue("PrimaryColorNoise", False)
                Cursor_Pen_SecondaryColorNoise = r.GetValue("SecondaryColorNoise", False)

                Cursor_Pen_PrimaryColorGradientMode = ReturnGradientModeFromString(r.GetValue("PrimaryColorGradientMode", "Vertical"))
                Cursor_Pen_SecondaryColorGradientMode = ReturnGradientModeFromString(r.GetValue("SecondaryColorGradientMode", "Vertical"))

                Cursor_Pen_PrimaryColorNoiseOpacity = r.GetValue("PrimaryColorNoiseOpacity", 25) / 100
                Cursor_Pen_SecondaryColorNoiseOpacity = r.GetValue("SecondaryColorNoiseOpacity", 25) / 100

#End Region

#Region "None"
                r = rMain.OpenSubKey("None")
                Cursor_None_PrimaryColor1 = Color.FromArgb(r.GetValue("PrimaryColor1", Color.White.ToArgb))
                Cursor_None_PrimaryColor2 = Color.FromArgb(r.GetValue("PrimaryColor2", Color.White.ToArgb))
                Cursor_None_SecondaryColor1 = Color.FromArgb(r.GetValue("SecondaryColor1", Color.Red.ToArgb))
                Cursor_None_SecondaryColor2 = Color.FromArgb(r.GetValue("SecondaryColor2", Color.Red.ToArgb))

                Cursor_None_PrimaryColorGradient = r.GetValue("PrimaryColorGradient", False)
                Cursor_None_SecondaryColorGradient = r.GetValue("SecondaryColorGradient", False)
                Cursor_None_PrimaryColorNoise = r.GetValue("PrimaryColorNoise", False)
                Cursor_None_SecondaryColorNoise = r.GetValue("SecondaryColorNoise", False)

                Cursor_None_PrimaryColorGradientMode = ReturnGradientModeFromString(r.GetValue("PrimaryColorGradientMode", "Vertical"))
                Cursor_None_SecondaryColorGradientMode = ReturnGradientModeFromString(r.GetValue("SecondaryColorGradientMode", "Vertical"))

                Cursor_None_PrimaryColorNoiseOpacity = r.GetValue("PrimaryColorNoiseOpacity", 25) / 100
                Cursor_None_SecondaryColorNoiseOpacity = r.GetValue("SecondaryColorNoiseOpacity", 25) / 100
#End Region

#Region "Link"
                r = rMain.OpenSubKey("Link")
                Cursor_Link_PrimaryColor1 = Color.FromArgb(r.GetValue("PrimaryColor1", Color.White.ToArgb))
                Cursor_Link_PrimaryColor2 = Color.FromArgb(r.GetValue("PrimaryColor2", Color.White.ToArgb))
                Cursor_Link_SecondaryColor1 = Color.FromArgb(r.GetValue("SecondaryColor1", Color.FromArgb(64, 65, 75).ToArgb))
                Cursor_Link_SecondaryColor2 = Color.FromArgb(r.GetValue("SecondaryColor2", Color.FromArgb(64, 65, 75).ToArgb))

                Cursor_Link_PrimaryColorGradient = r.GetValue("PrimaryColorGradient", False)
                Cursor_Link_SecondaryColorGradient = r.GetValue("SecondaryColorGradient", False)
                Cursor_Link_PrimaryColorNoise = r.GetValue("PrimaryColorNoise", False)
                Cursor_Link_SecondaryColorNoise = r.GetValue("SecondaryColorNoise", False)

                Cursor_Link_PrimaryColorGradientMode = ReturnGradientModeFromString(r.GetValue("PrimaryColorGradientMode", "Vertical"))
                Cursor_Link_SecondaryColorGradientMode = ReturnGradientModeFromString(r.GetValue("SecondaryColorGradientMode", "Vertical"))

                Cursor_Link_PrimaryColorNoiseOpacity = r.GetValue("PrimaryColorNoiseOpacity", 25) / 100
                Cursor_Link_SecondaryColorNoiseOpacity = r.GetValue("SecondaryColorNoiseOpacity", 25) / 100
#End Region

#Region "Pin"
                r = rMain.OpenSubKey("Pin")
                Cursor_Pin_PrimaryColor1 = Color.FromArgb(r.GetValue("PrimaryColor1", Color.White.ToArgb))
                Cursor_Pin_PrimaryColor2 = Color.FromArgb(r.GetValue("PrimaryColor2", Color.White.ToArgb))
                Cursor_Pin_SecondaryColor1 = Color.FromArgb(r.GetValue("SecondaryColor1", Color.FromArgb(64, 65, 75).ToArgb))
                Cursor_Pin_SecondaryColor2 = Color.FromArgb(r.GetValue("SecondaryColor2", Color.FromArgb(64, 65, 75).ToArgb))

                Cursor_Pin_PrimaryColorGradient = r.GetValue("PrimaryColorGradient", False)
                Cursor_Pin_SecondaryColorGradient = r.GetValue("SecondaryColorGradient", False)
                Cursor_Pin_PrimaryColorNoise = r.GetValue("PrimaryColorNoise", False)
                Cursor_Pin_SecondaryColorNoise = r.GetValue("SecondaryColorNoise", False)

                Cursor_Pin_PrimaryColorGradientMode = ReturnGradientModeFromString(r.GetValue("PrimaryColorGradientMode", "Vertical"))
                Cursor_Pin_SecondaryColorGradientMode = ReturnGradientModeFromString(r.GetValue("SecondaryColorGradientMode", "Vertical"))

                Cursor_Pin_PrimaryColorNoiseOpacity = r.GetValue("PrimaryColorNoiseOpacity", 25) / 100
                Cursor_Pin_SecondaryColorNoiseOpacity = r.GetValue("SecondaryColorNoiseOpacity", 25) / 100
#End Region

#Region "Person"
                r = rMain.OpenSubKey("Person")
                Cursor_Person_PrimaryColor1 = Color.FromArgb(r.GetValue("PrimaryColor1", Color.White.ToArgb))
                Cursor_Person_PrimaryColor2 = Color.FromArgb(r.GetValue("PrimaryColor2", Color.White.ToArgb))
                Cursor_Person_SecondaryColor1 = Color.FromArgb(r.GetValue("SecondaryColor1", Color.FromArgb(64, 65, 75).ToArgb))
                Cursor_Person_SecondaryColor2 = Color.FromArgb(r.GetValue("SecondaryColor2", Color.FromArgb(64, 65, 75).ToArgb))

                Cursor_Person_PrimaryColorGradient = r.GetValue("PrimaryColorGradient", False)
                Cursor_Person_SecondaryColorGradient = r.GetValue("SecondaryColorGradient", False)
                Cursor_Person_PrimaryColorNoise = r.GetValue("PrimaryColorNoise", False)
                Cursor_Person_SecondaryColorNoise = r.GetValue("SecondaryColorNoise", False)

                Cursor_Person_PrimaryColorGradientMode = ReturnGradientModeFromString(r.GetValue("PrimaryColorGradientMode", "Vertical"))
                Cursor_Person_SecondaryColorGradientMode = ReturnGradientModeFromString(r.GetValue("SecondaryColorGradientMode", "Vertical"))

                Cursor_Person_PrimaryColorNoiseOpacity = r.GetValue("PrimaryColorNoiseOpacity", 25) / 100
                Cursor_Person_SecondaryColorNoiseOpacity = r.GetValue("SecondaryColorNoiseOpacity", 25) / 100
#End Region

#Region "IBeam"
                r = rMain.OpenSubKey("IBeam")
                Cursor_IBeam_PrimaryColor1 = Color.FromArgb(r.GetValue("PrimaryColor1", Color.White.ToArgb))
                Cursor_IBeam_PrimaryColor2 = Color.FromArgb(r.GetValue("PrimaryColor2", Color.White.ToArgb))
                Cursor_IBeam_SecondaryColor1 = Color.FromArgb(r.GetValue("SecondaryColor1", Color.FromArgb(64, 65, 75).ToArgb))
                Cursor_IBeam_SecondaryColor2 = Color.FromArgb(r.GetValue("SecondaryColor2", Color.FromArgb(64, 65, 75).ToArgb))

                Cursor_IBeam_PrimaryColorGradient = r.GetValue("PrimaryColorGradient", False)
                Cursor_IBeam_SecondaryColorGradient = r.GetValue("SecondaryColorGradient", False)
                Cursor_IBeam_PrimaryColorNoise = r.GetValue("PrimaryColorNoise", False)
                Cursor_IBeam_SecondaryColorNoise = r.GetValue("SecondaryColorNoise", False)

                Cursor_IBeam_PrimaryColorGradientMode = ReturnGradientModeFromString(r.GetValue("PrimaryColorGradientMode", "Vertical"))
                Cursor_IBeam_SecondaryColorGradientMode = ReturnGradientModeFromString(r.GetValue("SecondaryColorGradientMode", "Vertical"))

                Cursor_IBeam_PrimaryColorNoiseOpacity = r.GetValue("PrimaryColorNoiseOpacity", 25) / 100
                Cursor_IBeam_SecondaryColorNoiseOpacity = r.GetValue("SecondaryColorNoiseOpacity", 25) / 100
#End Region

#Region "Cross"
                r = rMain.OpenSubKey("Cross")
                Cursor_Cross_PrimaryColor1 = Color.FromArgb(r.GetValue("PrimaryColor1", Color.White.ToArgb))
                Cursor_Cross_PrimaryColor2 = Color.FromArgb(r.GetValue("PrimaryColor2", Color.White.ToArgb))
                Cursor_Cross_SecondaryColor1 = Color.FromArgb(r.GetValue("SecondaryColor1", Color.FromArgb(64, 65, 75).ToArgb))
                Cursor_Cross_SecondaryColor2 = Color.FromArgb(r.GetValue("SecondaryColor2", Color.FromArgb(64, 65, 75).ToArgb))

                Cursor_Cross_PrimaryColorGradient = r.GetValue("PrimaryColorGradient", False)
                Cursor_Cross_SecondaryColorGradient = r.GetValue("SecondaryColorGradient", False)
                Cursor_Cross_PrimaryColorNoise = r.GetValue("PrimaryColorNoise", False)
                Cursor_Cross_SecondaryColorNoise = r.GetValue("SecondaryColorNoise", False)

                Cursor_Cross_PrimaryColorGradientMode = ReturnGradientModeFromString(r.GetValue("PrimaryColorGradientMode", "Vertical"))
                Cursor_Cross_SecondaryColorGradientMode = ReturnGradientModeFromString(r.GetValue("SecondaryColorGradientMode", "Vertical"))

                Cursor_Cross_PrimaryColorNoiseOpacity = r.GetValue("PrimaryColorNoiseOpacity", 25) / 100
                Cursor_Cross_SecondaryColorNoiseOpacity = r.GetValue("SecondaryColorNoiseOpacity", 25) / 100
#End Region

                rMain.Close()
                r.Close()
#End Region
#End Region

            Case Mode.File
#Region "File"
                Dim txt As New List(Of String)
                txt.Clear()
                CList_FromStr(txt, IO.File.ReadAllText(PaletteFile))

                Dim ls_stable, ls_preview As New List(Of String)
                ls_stable.Clear()
                ls_preview.Clear()

                For Each lin As String In txt
#Region "Personal Info"
                    If lin.StartsWith("*Created from App Version= ") Then AppVersion = lin.Remove(0, "*Created from App Version= ".Count)
                    If lin.StartsWith("*Palette Name= ") Then PaletteName = lin.Remove(0, "*Palette Name= ".Count)
                    If lin.StartsWith("*Palette Description= ") Then PaletteDescription = lin.Remove(0, "*Palette Description= ".Count).Replace("<br>", vbCrLf)
                    If lin.StartsWith("*Palette File Version= ") Then PaletteVersion = lin.Remove(0, "*Palette File Version= ".Count)
                    If lin.StartsWith("*Author= ") Then Author = lin.Remove(0, "*Author= ".Count)
                    If lin.StartsWith("*AuthorSocialMediaLink= ") Then AuthorSocialMediaLink = lin.Remove(0, "*AuthorSocialMediaLink= ".Count)
                    If lin.StartsWith("*Palette File Version= ") Then PaletteVersion = lin.Remove(0, "*Palette File Version= ".Count)
#End Region

#Region "Modern Windows"

                    If lin.StartsWith("*WinMode_Light= ") Then WinMode_Light = lin.Remove(0, "*WinMode_Light= ".Count)
                    If lin.StartsWith("*AppMode_Light= ") Then AppMode_Light = lin.Remove(0, "*AppMode_Light= ".Count)
                    If lin.StartsWith("*Transparency= ") Then Transparency = lin.Remove(0, "*Transparency= ".Count)
                    If lin.StartsWith("*AccentColorOnTitlebarAndBorders= ") Then ApplyAccentonTitlebars = lin.Remove(0, "*AccentColorOnTitlebarAndBorders= ".Count)
                    If lin.StartsWith("*AccentColorOnStartTaskbarAndActionCenter= ") Then
                        Select Case lin.Remove(0, "*AccentColorOnStartTaskbarAndActionCenter= ".Count).ToLower
                            Case "false"
                                ApplyAccentonTaskbar = ApplyAccentonTaskbar_Level.None

                            Case "true"
                                ApplyAccentonTaskbar = ApplyAccentonTaskbar_Level.Taskbar_Start_AC

                            Case Else
                                Select Case lin.Remove(0, "*AccentColorOnStartTaskbarAndActionCenter= ".Count)
                                    Case 0
                                        ApplyAccentonTaskbar = ApplyAccentonTaskbar_Level.None

                                    Case 1
                                        ApplyAccentonTaskbar = ApplyAccentonTaskbar_Level.Taskbar_Start_AC

                                    Case 2
                                        ApplyAccentonTaskbar = ApplyAccentonTaskbar_Level.Taskbar

                                    Case Else
                                        ApplyAccentonTaskbar = ApplyAccentonTaskbar_Level.None
                                End Select


                        End Select
                    End If
                    If lin.StartsWith("*Titlebar_Active= ") Then Titlebar_Active = Color.FromArgb(lin.Remove(0, "*Titlebar_Active= ".Count))
                    If lin.StartsWith("*Titlebar_Inactive= ") Then Titlebar_Inactive = Color.FromArgb(lin.Remove(0, "*Titlebar_Inactive= ".Count))
                    If lin.StartsWith("*ActionCenter_AppsLinks= ") Then ActionCenter_AppsLinks = Color.FromArgb(lin.Remove(0, "*ActionCenter_AppsLinks= ".Count))
                    If lin.StartsWith("*Taskbar_Icon_Underline= ") Then Taskbar_Icon_Underline = Color.FromArgb(lin.Remove(0, "*Taskbar_Icon_Underline= ".Count))
                    If lin.StartsWith("*StartButton_Hover= ") Then StartButton_Hover = Color.FromArgb(lin.Remove(0, "*StartButton_Hover= ".Count))
                    If lin.StartsWith("*SettingsIconsAndLinks= ") Then SettingsIconsAndLinks = Color.FromArgb(lin.Remove(0, "*SettingsIconsAndLinks= ".Count))
                    If lin.StartsWith("*StartMenuBackground_ActiveTaskbarButton= ") Then StartMenuBackground_ActiveTaskbarButton = Color.FromArgb(lin.Remove(0, "*StartMenuBackground_ActiveTaskbarButton= ".Count))
                    If lin.StartsWith("*StartListFolders_TaskbarFront= ") Then StartListFolders_TaskbarFront = Color.FromArgb(lin.Remove(0, "*StartListFolders_TaskbarFront= ".Count))
                    If lin.StartsWith("*Taskbar_Background= ") Then Taskbar_Background = Color.FromArgb(lin.Remove(0, "*Taskbar_Background= ".Count))
                    If lin.StartsWith("*StartMenu_Accent= ") Then StartMenu_Accent = Color.FromArgb(lin.Remove(0, "*StartMenu_Accent= ".Count))
                    If lin.StartsWith("*Undefined= ") Then UWP_Undefined = Color.FromArgb(lin.Remove(0, "*Undefined= ".Count))

#End Region

#Region "Aero"
                    If lin.StartsWith("*Aero_ColorizationColor= ") Then Aero_ColorizationColor = Color.FromArgb(lin.Remove(0, "*Aero_ColorizationColor= ".Count))
                    If lin.StartsWith("*Aero_ColorizationAfterglow= ") Then Aero_ColorizationAfterglow = Color.FromArgb(lin.Remove(0, "*Aero_ColorizationAfterglow= ".Count))
                    If lin.StartsWith("*Aero_ColorizationColorBalance= ") Then Aero_ColorizationColorBalance = lin.Remove(0, "*Aero_ColorizationColorBalance= ".Count)
                    If lin.StartsWith("*Aero_ColorizationAfterglowBalance= ") Then Aero_ColorizationAfterglowBalance = lin.Remove(0, "*Aero_ColorizationAfterglowBalance= ".Count)
                    If lin.StartsWith("*Aero_ColorizationBlurBalance= ") Then Aero_ColorizationBlurBalance = lin.Remove(0, "*Aero_ColorizationBlurBalance= ".Count)
                    If lin.StartsWith("*Aero_ColorizationGlassReflectionIntensity= ") Then Aero_ColorizationGlassReflectionIntensity = lin.Remove(0, "*Aero_ColorizationGlassReflectionIntensity= ".Count)
                    If lin.StartsWith("*Aero_EnableAeroPeek= ") Then Aero_EnableAeroPeek = lin.Remove(0, "*Aero_EnableAeroPeek= ".Count)
                    If lin.StartsWith("*Aero_AlwaysHibernateThumbnails= ") Then Aero_AlwaysHibernateThumbnails = lin.Remove(0, "*Aero_AlwaysHibernateThumbnails= ".Count)
                    If lin.StartsWith("*Aero_Theme= ") Then Aero_Theme = lin.Remove(0, "*Aero_Theme= ".Count)
#End Region

#Region "Metro"
                    If lin.StartsWith("*Metro_PersonalColors_Background= ") Then Metro_PersonalColors_Background = Color.FromArgb(lin.Remove(0, "*Metro_PersonalColors_Background= ".Count))
                    If lin.StartsWith("*Metro_PersonalColors_Accent= ") Then Metro_PersonalColors_Accent = Color.FromArgb(lin.Remove(0, "*Metro_PersonalColors_Accent= ".Count))
                    If lin.StartsWith("*Metro_StartColor= ") Then Metro_StartColor = Color.FromArgb(lin.Remove(0, "*Metro_StartColor= ".Count))
                    If lin.StartsWith("*Metro_AccentColor= ") Then Metro_AccentColor = Color.FromArgb(lin.Remove(0, "*Metro_AccentColor= ".Count))
                    If lin.StartsWith("*Metro_Start= ") Then Metro_Start = lin.Remove(0, "*Metro_Start= ".Count)
                    If lin.StartsWith("*Metro_Theme= ") Then Metro_Theme = lin.Remove(0, "*Metro_Theme= ".Count)
                    If lin.StartsWith("*Metro_LogonUI= ") Then Metro_LogonUI = lin.Remove(0, "*Metro_LogonUI= ".Count)
                    If lin.StartsWith("*Metro_NoLockScreen= ") Then Metro_NoLockScreen = lin.Remove(0, "*Metro_NoLockScreen= ".Count)
                    If lin.StartsWith("*Metro_LockScreenType= ") Then Metro_LockScreenType = lin.Remove(0, "*Metro_LockScreenType= ".Count)
                    If lin.StartsWith("*Metro_LockScreenSystemID= ") Then Metro_LockScreenSystemID = lin.Remove(0, "*Metro_LockScreenSystemID= ".Count)
#End Region

#Region "LogonUI"
                    If lin.StartsWith("*LogonUI_DisableAcrylicBackgroundOnLogon= ") Then LogonUI_DisableAcrylicBackgroundOnLogon = lin.Remove(0, "*LogonUI_DisableAcrylicBackgroundOnLogon= ".Count)
                    If lin.StartsWith("*LogonUI_DisableLogonBackgroundImage= ") Then LogonUI_DisableLogonBackgroundImage = lin.Remove(0, "*LogonUI_DisableLogonBackgroundImage= ".Count)
                    If lin.StartsWith("*LogonUI_NoLockScreen= ") Then LogonUI_NoLockScreen = lin.Remove(0, "*LogonUI_NoLockScreen= ".Count)
#End Region

#Region "LogonUI_7_8"
                    If lin.StartsWith("*LogonUI7_Color= ") Then LogonUI7_Color = Color.FromArgb(lin.Remove(0, "*LogonUI7_Color= ".Count))
                    If lin.StartsWith("*LogonUI7_Enabled= ") Then LogonUI7_Enabled = lin.Remove(0, "*LogonUI7_Enabled= ".Count)
                    If lin.StartsWith("*LogonUI7_Mode= ") Then LogonUI7_Mode = lin.Remove(0, "*LogonUI7_Mode= ".Count)
                    If lin.StartsWith("*LogonUI7_ImagePath= ") Then LogonUI7_ImagePath = lin.Remove(0, "*LogonUI7_ImagePath= ".Count)
                    If lin.StartsWith("*LogonUI7_Effect_Blur= ") Then LogonUI7_Effect_Blur = lin.Remove(0, "*LogonUI7_Effect_Blur= ".Count)
                    If lin.StartsWith("*LogonUI7_Effect_Blur_Intensity= ") Then LogonUI7_Effect_Blur_Intensity = lin.Remove(0, "*LogonUI7_Effect_Blur_Intensity= ".Count)
                    If lin.StartsWith("*LogonUI7_Effect_Grayscale= ") Then LogonUI7_Effect_Grayscale = lin.Remove(0, "*LogonUI7_Effect_Grayscale= ".Count)
                    If lin.StartsWith("*LogonUI7_Effect_Noise= ") Then LogonUI7_Effect_Noise = lin.Remove(0, "*LogonUI7_Effect_Noise= ".Count)
                    If lin.StartsWith("*LogonUI7_Effect_Noise_Mode= ") Then LogonUI7_Effect_Noise_Mode = lin.Remove(0, "*LogonUI7_Effect_Noise_Mode= ".Count)
                    If lin.StartsWith("*LogonUI7_Effect_Noise_Intensity= ") Then LogonUI7_Effect_Noise_Intensity = lin.Remove(0, "*LogonUI7_Effect_Noise_Intensity= ".Count)
#End Region

#Region "Win32UI"
                    If lin.StartsWith("*Win32UI_EnableTheming= ") Then Win32UI_EnableTheming = lin.Remove(0, "*Win32UI_EnableTheming= ".Count)
                    If lin.StartsWith("*Win32UI_EnableGradient= ") Then Win32UI_EnableGradient = lin.Remove(0, "*Win32UI_EnableGradient= ".Count)
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

#Region "Terminals"
#Region "Locking"
                    If lin.StartsWith("*Terminal_CMD_Enabled= ") Then Terminal_CMD_Enabled = lin.Remove(0, "*Terminal_CMD_Enabled= ".Count)
                    If lin.StartsWith("*Terminal_PS_32_Enabled= ") Then Terminal_PS_32_Enabled = lin.Remove(0, "*Terminal_PS_32_Enabled= ".Count)
                    If lin.StartsWith("*Terminal_PS_64_Enabled= ") Then Terminal_PS_64_Enabled = lin.Remove(0, "*Terminal_PS_64_Enabled= ".Count)
                    If lin.StartsWith("*Terminal_Stable_Enabled= ") Then Terminal_Stable_Enabled = lin.Remove(0, "*Terminal_Stable_Enabled= ".Count)
                    If lin.StartsWith("*Terminal_Preview_Enabled= ") Then Terminal_Preview_Enabled = lin.Remove(0, "*Terminal_Preview_Enabled= ".Count)
#End Region

#Region "Command Prompt"
                    If lin.StartsWith("*CMD_ColorTable00= ") Then CMD_ColorTable00 = Color.FromArgb(lin.Remove(0, "*CMD_ColorTable00= ".Count))
                    If lin.StartsWith("*CMD_ColorTable01= ") Then CMD_ColorTable01 = Color.FromArgb(lin.Remove(0, "*CMD_ColorTable01= ".Count))
                    If lin.StartsWith("*CMD_ColorTable02= ") Then CMD_ColorTable02 = Color.FromArgb(lin.Remove(0, "*CMD_ColorTable02= ".Count))
                    If lin.StartsWith("*CMD_ColorTable03= ") Then CMD_ColorTable03 = Color.FromArgb(lin.Remove(0, "*CMD_ColorTable03= ".Count))
                    If lin.StartsWith("*CMD_ColorTable04= ") Then CMD_ColorTable04 = Color.FromArgb(lin.Remove(0, "*CMD_ColorTable04= ".Count))
                    If lin.StartsWith("*CMD_ColorTable05= ") Then CMD_ColorTable05 = Color.FromArgb(lin.Remove(0, "*CMD_ColorTable05= ".Count))
                    If lin.StartsWith("*CMD_ColorTable06= ") Then CMD_ColorTable06 = Color.FromArgb(lin.Remove(0, "*CMD_ColorTable06= ".Count))
                    If lin.StartsWith("*CMD_ColorTable07= ") Then CMD_ColorTable07 = Color.FromArgb(lin.Remove(0, "*CMD_ColorTable07= ".Count))
                    If lin.StartsWith("*CMD_ColorTable08= ") Then CMD_ColorTable08 = Color.FromArgb(lin.Remove(0, "*CMD_ColorTable08= ".Count))
                    If lin.StartsWith("*CMD_ColorTable09= ") Then CMD_ColorTable09 = Color.FromArgb(lin.Remove(0, "*CMD_ColorTable09= ".Count))
                    If lin.StartsWith("*CMD_ColorTable10= ") Then CMD_ColorTable10 = Color.FromArgb(lin.Remove(0, "*CMD_ColorTable10= ".Count))
                    If lin.StartsWith("*CMD_ColorTable11= ") Then CMD_ColorTable11 = Color.FromArgb(lin.Remove(0, "*CMD_ColorTable11= ".Count))
                    If lin.StartsWith("*CMD_ColorTable12= ") Then CMD_ColorTable12 = Color.FromArgb(lin.Remove(0, "*CMD_ColorTable12= ".Count))
                    If lin.StartsWith("*CMD_ColorTable13= ") Then CMD_ColorTable13 = Color.FromArgb(lin.Remove(0, "*CMD_ColorTable13= ".Count))
                    If lin.StartsWith("*CMD_ColorTable14= ") Then CMD_ColorTable14 = Color.FromArgb(lin.Remove(0, "*CMD_ColorTable14= ".Count))
                    If lin.StartsWith("*CMD_ColorTable15= ") Then CMD_ColorTable15 = Color.FromArgb(lin.Remove(0, "*CMD_ColorTable15= ".Count))
                    If lin.StartsWith("*CMD_PopupForeground= ") Then CMD_PopupForeground = lin.Remove(0, "*CMD_PopupForeground= ".Count)
                    If lin.StartsWith("*CMD_PopupBackground= ") Then CMD_PopupBackground = lin.Remove(0, "*CMD_PopupBackground= ".Count)
                    If lin.StartsWith("*CMD_ScreenColorsForeground= ") Then CMD_ScreenColorsForeground = lin.Remove(0, "*CMD_ScreenColorsForeground= ".Count)
                    If lin.StartsWith("*CMD_ScreenColorsBackground= ") Then CMD_ScreenColorsBackground = lin.Remove(0, "*CMD_ScreenColorsBackground= ".Count)
                    If lin.StartsWith("*CMD_CursorSize= ") Then CMD_CursorSize = lin.Remove(0, "*CMD_CursorSize= ".Count)
                    If lin.StartsWith("*CMD_FaceName= ") Then CMD_FaceName = lin.Remove(0, "*CMD_FaceName= ".Count)
                    If lin.StartsWith("*CMD_FontRaster= ") Then CMD_FontRaster = lin.Remove(0, "*CMD_FontRaster= ".Count)
                    If lin.StartsWith("*CMD_FontSize= ") Then CMD_FontSize = lin.Remove(0, "*CMD_FontSize= ".Count)
                    If lin.StartsWith("*CMD_FontWeight= ") Then CMD_FontWeight = lin.Remove(0, "*CMD_FontWeight= ".Count)
                    If lin.StartsWith("*CMD_1909_CursorType= ") Then CMD_1909_CursorType = lin.Remove(0, "*CMD_1909_CursorType= ".Count)
                    If lin.StartsWith("*CMD_1909_CursorColor= ") Then CMD_1909_CursorColor = Color.FromArgb(lin.Remove(0, "*CMD_1909_CursorColor= ".Count))
                    If lin.StartsWith("*CMD_1909_ForceV2= ") Then CMD_1909_ForceV2 = lin.Remove(0, "*CMD_1909_ForceV2= ".Count)
                    If lin.StartsWith("*CMD_1909_LineSelection= ") Then CMD_1909_LineSelection = lin.Remove(0, "*CMD_1909_LineSelection= ".Count)
                    If lin.StartsWith("*CMD_1909_TerminalScrolling= ") Then CMD_1909_TerminalScrolling = lin.Remove(0, "*CMD_1909_TerminalScrolling= ".Count)
                    If lin.StartsWith("*CMD_1909_WindowAlpha= ") Then CMD_1909_WindowAlpha = lin.Remove(0, "*CMD_1909_WindowAlpha= ".Count)
#End Region

#Region "PowerShell x86"
                    If lin.StartsWith("*PS_32_ColorTable00= ") Then PS_32_ColorTable00 = Color.FromArgb(lin.Remove(0, "*PS_32_ColorTable00= ".Count))
                    If lin.StartsWith("*PS_32_ColorTable01= ") Then PS_32_ColorTable01 = Color.FromArgb(lin.Remove(0, "*PS_32_ColorTable01= ".Count))
                    If lin.StartsWith("*PS_32_ColorTable02= ") Then PS_32_ColorTable02 = Color.FromArgb(lin.Remove(0, "*PS_32_ColorTable02= ".Count))
                    If lin.StartsWith("*PS_32_ColorTable03= ") Then PS_32_ColorTable03 = Color.FromArgb(lin.Remove(0, "*PS_32_ColorTable03= ".Count))
                    If lin.StartsWith("*PS_32_ColorTable04= ") Then PS_32_ColorTable04 = Color.FromArgb(lin.Remove(0, "*PS_32_ColorTable04= ".Count))
                    If lin.StartsWith("*PS_32_ColorTable05= ") Then PS_32_ColorTable05 = Color.FromArgb(lin.Remove(0, "*PS_32_ColorTable05= ".Count))
                    If lin.StartsWith("*PS_32_ColorTable06= ") Then PS_32_ColorTable06 = Color.FromArgb(lin.Remove(0, "*PS_32_ColorTable06= ".Count))
                    If lin.StartsWith("*PS_32_ColorTable07= ") Then PS_32_ColorTable07 = Color.FromArgb(lin.Remove(0, "*PS_32_ColorTable07= ".Count))
                    If lin.StartsWith("*PS_32_ColorTable08= ") Then PS_32_ColorTable08 = Color.FromArgb(lin.Remove(0, "*PS_32_ColorTable08= ".Count))
                    If lin.StartsWith("*PS_32_ColorTable09= ") Then PS_32_ColorTable09 = Color.FromArgb(lin.Remove(0, "*PS_32_ColorTable09= ".Count))
                    If lin.StartsWith("*PS_32_ColorTable10= ") Then PS_32_ColorTable10 = Color.FromArgb(lin.Remove(0, "*PS_32_ColorTable10= ".Count))
                    If lin.StartsWith("*PS_32_ColorTable11= ") Then PS_32_ColorTable11 = Color.FromArgb(lin.Remove(0, "*PS_32_ColorTable11= ".Count))
                    If lin.StartsWith("*PS_32_ColorTable12= ") Then PS_32_ColorTable12 = Color.FromArgb(lin.Remove(0, "*PS_32_ColorTable12= ".Count))
                    If lin.StartsWith("*PS_32_ColorTable13= ") Then PS_32_ColorTable13 = Color.FromArgb(lin.Remove(0, "*PS_32_ColorTable13= ".Count))
                    If lin.StartsWith("*PS_32_ColorTable14= ") Then PS_32_ColorTable14 = Color.FromArgb(lin.Remove(0, "*PS_32_ColorTable14= ".Count))
                    If lin.StartsWith("*PS_32_ColorTable15= ") Then PS_32_ColorTable15 = Color.FromArgb(lin.Remove(0, "*PS_32_ColorTable15= ".Count))
                    If lin.StartsWith("*PS_32_PopupForeground= ") Then PS_32_PopupForeground = lin.Remove(0, "*PS_32_PopupForeground= ".Count)
                    If lin.StartsWith("*PS_32_PopupBackground= ") Then PS_32_PopupBackground = lin.Remove(0, "*PS_32_PopupBackground= ".Count)
                    If lin.StartsWith("*PS_32_ScreenColorsForeground= ") Then PS_32_ScreenColorsForeground = lin.Remove(0, "*PS_32_ScreenColorsForeground= ".Count)
                    If lin.StartsWith("*PS_32_ScreenColorsBackground= ") Then PS_32_ScreenColorsBackground = lin.Remove(0, "*PS_32_ScreenColorsBackground= ".Count)
                    If lin.StartsWith("*PS_32_CursorSize= ") Then PS_32_CursorSize = lin.Remove(0, "*PS_32_CursorSize= ".Count)
                    If lin.StartsWith("*PS_32_FaceName= ") Then PS_32_FaceName = lin.Remove(0, "*PS_32_FaceName= ".Count)
                    If lin.StartsWith("*PS_32_FontRaster= ") Then PS_32_FontRaster = lin.Remove(0, "*PS_32_FontRaster= ".Count)
                    If lin.StartsWith("*PS_32_FontSize= ") Then PS_32_FontSize = lin.Remove(0, "*PS_32_FontSize= ".Count)
                    If lin.StartsWith("*PS_32_FontWeight= ") Then PS_32_FontWeight = lin.Remove(0, "*PS_32_FontWeight= ".Count)
                    If lin.StartsWith("*PS_32_1909_CursorType= ") Then PS_32_1909_CursorType = lin.Remove(0, "*PS_32_1909_CursorType= ".Count)
                    If lin.StartsWith("*PS_32_1909_CursorColor= ") Then PS_32_1909_CursorColor = Color.FromArgb(lin.Remove(0, "*PS_32_1909_CursorColor= ".Count))
                    If lin.StartsWith("*PS_32_1909_ForceV2= ") Then PS_32_1909_ForceV2 = lin.Remove(0, "*PS_32_1909_ForceV2= ".Count)
                    If lin.StartsWith("*PS_32_1909_LineSelection= ") Then PS_32_1909_LineSelection = lin.Remove(0, "*PS_32_1909_LineSelection= ".Count)
                    If lin.StartsWith("*PS_32_1909_TerminalScrolling= ") Then PS_32_1909_TerminalScrolling = lin.Remove(0, "*PS_32_1909_TerminalScrolling= ".Count)
                    If lin.StartsWith("*PS_32_1909_WindowAlpha= ") Then PS_32_1909_WindowAlpha = lin.Remove(0, "*PS_32_1909_WindowAlpha= ".Count)
#End Region

#Region "PowerShell x64"
                    If lin.StartsWith("*PS_64_ColorTable00= ") Then PS_64_ColorTable00 = Color.FromArgb(lin.Remove(0, "*PS_64_ColorTable00= ".Count))
                    If lin.StartsWith("*PS_64_ColorTable01= ") Then PS_64_ColorTable01 = Color.FromArgb(lin.Remove(0, "*PS_64_ColorTable01= ".Count))
                    If lin.StartsWith("*PS_64_ColorTable02= ") Then PS_64_ColorTable02 = Color.FromArgb(lin.Remove(0, "*PS_64_ColorTable02= ".Count))
                    If lin.StartsWith("*PS_64_ColorTable03= ") Then PS_64_ColorTable03 = Color.FromArgb(lin.Remove(0, "*PS_64_ColorTable03= ".Count))
                    If lin.StartsWith("*PS_64_ColorTable04= ") Then PS_64_ColorTable04 = Color.FromArgb(lin.Remove(0, "*PS_64_ColorTable04= ".Count))
                    If lin.StartsWith("*PS_64_ColorTable05= ") Then PS_64_ColorTable05 = Color.FromArgb(lin.Remove(0, "*PS_64_ColorTable05= ".Count))
                    If lin.StartsWith("*PS_64_ColorTable06= ") Then PS_64_ColorTable06 = Color.FromArgb(lin.Remove(0, "*PS_64_ColorTable06= ".Count))
                    If lin.StartsWith("*PS_64_ColorTable07= ") Then PS_64_ColorTable07 = Color.FromArgb(lin.Remove(0, "*PS_64_ColorTable07= ".Count))
                    If lin.StartsWith("*PS_64_ColorTable08= ") Then PS_64_ColorTable08 = Color.FromArgb(lin.Remove(0, "*PS_64_ColorTable08= ".Count))
                    If lin.StartsWith("*PS_64_ColorTable09= ") Then PS_64_ColorTable09 = Color.FromArgb(lin.Remove(0, "*PS_64_ColorTable09= ".Count))
                    If lin.StartsWith("*PS_64_ColorTable10= ") Then PS_64_ColorTable10 = Color.FromArgb(lin.Remove(0, "*PS_64_ColorTable10= ".Count))
                    If lin.StartsWith("*PS_64_ColorTable11= ") Then PS_64_ColorTable11 = Color.FromArgb(lin.Remove(0, "*PS_64_ColorTable11= ".Count))
                    If lin.StartsWith("*PS_64_ColorTable12= ") Then PS_64_ColorTable12 = Color.FromArgb(lin.Remove(0, "*PS_64_ColorTable12= ".Count))
                    If lin.StartsWith("*PS_64_ColorTable13= ") Then PS_64_ColorTable13 = Color.FromArgb(lin.Remove(0, "*PS_64_ColorTable13= ".Count))
                    If lin.StartsWith("*PS_64_ColorTable14= ") Then PS_64_ColorTable14 = Color.FromArgb(lin.Remove(0, "*PS_64_ColorTable14= ".Count))
                    If lin.StartsWith("*PS_64_ColorTable15= ") Then PS_64_ColorTable15 = Color.FromArgb(lin.Remove(0, "*PS_64_ColorTable15= ".Count))
                    If lin.StartsWith("*PS_64_PopupForeground= ") Then PS_64_PopupForeground = lin.Remove(0, "*PS_64_PopupForeground= ".Count)
                    If lin.StartsWith("*PS_64_PopupBackground= ") Then PS_64_PopupBackground = lin.Remove(0, "*PS_64_PopupBackground= ".Count)
                    If lin.StartsWith("*PS_64_ScreenColorsForeground= ") Then PS_64_ScreenColorsForeground = lin.Remove(0, "*PS_64_ScreenColorsForeground= ".Count)
                    If lin.StartsWith("*PS_64_ScreenColorsBackground= ") Then PS_64_ScreenColorsBackground = lin.Remove(0, "*PS_64_ScreenColorsBackground= ".Count)
                    If lin.StartsWith("*PS_64_CursorSize= ") Then PS_64_CursorSize = lin.Remove(0, "*PS_64_CursorSize= ".Count)
                    If lin.StartsWith("*PS_64_FaceName= ") Then PS_64_FaceName = lin.Remove(0, "*PS_64_FaceName= ".Count)
                    If lin.StartsWith("*PS_64_FontRaster= ") Then PS_64_FontRaster = lin.Remove(0, "*PS_64_FontRaster= ".Count)
                    If lin.StartsWith("*PS_64_FontSize= ") Then PS_64_FontSize = lin.Remove(0, "*PS_64_FontSize= ".Count)
                    If lin.StartsWith("*PS_64_FontWeight= ") Then PS_64_FontWeight = lin.Remove(0, "*PS_64_FontWeight= ".Count)
                    If lin.StartsWith("*PS_64_1909_CursorType= ") Then PS_64_1909_CursorType = lin.Remove(0, "*PS_64_1909_CursorType= ".Count)
                    If lin.StartsWith("*PS_64_1909_CursorColor= ") Then PS_64_1909_CursorColor = Color.FromArgb(lin.Remove(0, "*PS_64_1909_CursorColor= ".Count))
                    If lin.StartsWith("*PS_64_1909_ForceV2= ") Then PS_64_1909_ForceV2 = lin.Remove(0, "*PS_64_1909_ForceV2= ".Count)
                    If lin.StartsWith("*PS_64_1909_LineSelection= ") Then PS_64_1909_LineSelection = lin.Remove(0, "*PS_64_1909_LineSelection= ".Count)
                    If lin.StartsWith("*PS_64_1909_TerminalScrolling= ") Then PS_64_1909_TerminalScrolling = lin.Remove(0, "*PS_64_1909_TerminalScrolling= ".Count)
                    If lin.StartsWith("*PS_64_1909_WindowAlpha= ") Then PS_64_1909_WindowAlpha = lin.Remove(0, "*PS_64_1909_WindowAlpha= ".Count)
#End Region

#Region "Windows Terminal"
                    If Not IgnoreWindowsTerminal Then
                        If My.W10 Or My.W11 Then
                            If lin.ToLower.StartsWith("terminal.") Then ls_stable.Add(lin)
                            If lin.ToLower.StartsWith("terminalpreview.") Then ls_preview.Add(lin)
                        End If
                    End If
#End Region
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

#Region "IBeam"
                    If lin.StartsWith("*Cursor_IBeam_PrimaryColor1= ") Then Cursor_IBeam_PrimaryColor1 = Color.FromArgb(lin.Remove(0, "*Cursor_IBeam_PrimaryColor1= ".Count))
                    If lin.StartsWith("*Cursor_IBeam_PrimaryColor2= ") Then Cursor_IBeam_PrimaryColor2 = Color.FromArgb(lin.Remove(0, "*Cursor_IBeam_PrimaryColor2= ".Count))
                    If lin.StartsWith("*Cursor_IBeam_PrimaryColorGradient= ") Then Cursor_IBeam_PrimaryColorGradient = lin.Remove(0, "*Cursor_IBeam_PrimaryColorGradient= ".Count)
                    If lin.StartsWith("*Cursor_IBeam_PrimaryColorGradientMode= ") Then Cursor_IBeam_PrimaryColorGradientMode = lin.Remove(0, "*Cursor_IBeam_PrimaryColorGradientMode= ".Count)
                    If lin.StartsWith("*Cursor_IBeam_PrimaryColorNoise= ") Then Cursor_IBeam_PrimaryColorNoise = lin.Remove(0, "*Cursor_IBeam_PrimaryColorNoise= ".Count)
                    If lin.StartsWith("*Cursor_IBeam_PrimaryColorNoiseOpacity= ") Then Cursor_IBeam_PrimaryColorNoiseOpacity = lin.Remove(0, "*Cursor_IBeam_PrimaryColorNoiseOpacity= ".Count)
                    If lin.StartsWith("*Cursor_IBeam_SecondaryColor1= ") Then Cursor_IBeam_SecondaryColor1 = Color.FromArgb(lin.Remove(0, "*Cursor_IBeam_SecondaryColor1= ".Count))
                    If lin.StartsWith("*Cursor_IBeam_SecondaryColor2= ") Then Cursor_IBeam_SecondaryColor2 = Color.FromArgb(lin.Remove(0, "*Cursor_IBeam_SecondaryColor2= ".Count))
                    If lin.StartsWith("*Cursor_IBeam_SecondaryColorGradient= ") Then Cursor_IBeam_SecondaryColorGradient = lin.Remove(0, "*Cursor_IBeam_SecondaryColorGradient= ".Count)
                    If lin.StartsWith("*Cursor_IBeam_SecondaryColorGradientMode= ") Then Cursor_IBeam_SecondaryColorGradientMode = lin.Remove(0, "*Cursor_IBeam_SecondaryColorGradientMode= ".Count)
                    If lin.StartsWith("*Cursor_IBeam_SecondaryColorNoise= ") Then Cursor_IBeam_SecondaryColorNoise = lin.Remove(0, "*Cursor_IBeam_SecondaryColorNoise= ".Count)
                    If lin.StartsWith("*Cursor_IBeam_SecondaryColorNoiseOpacity= ") Then Cursor_IBeam_SecondaryColorNoiseOpacity = lin.Remove(0, "*Cursor_IBeam_SecondaryColorNoiseOpacity= ".Count)
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

#Region "Windows Terminal"
                If Not IgnoreWindowsTerminal Then
                    If My.W10 Or My.W11 Then

                        Dim str_stable, str_preview As String
                        str_stable = CStr_FromList(ls_stable)
                        str_preview = CStr_FromList(ls_preview)

                        Terminal = New WinTerminal(str_stable, WinTerminal.Mode.WinPaletterFile)
                        TerminalPreview = New WinTerminal(str_preview, WinTerminal.Mode.WinPaletterFile, WinTerminal.Version.Preview)
                    Else
                        Terminal = New WinTerminal("", WinTerminal.Mode.Empty)
                        TerminalPreview = New WinTerminal("", WinTerminal.Mode.Empty, WinTerminal.Version.Preview)
                    End If
                End If
#End Region
#End Region

            Case Mode.Init
#Region "Init"
#Region "Personal Info"
                Author = Environment.UserName
                AppVersion = My.Application.Info.Version.ToString
                PaletteVersion = "1.0"
                PaletteName = "Init"
#End Region

#Region "Modern Windows"
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
                UWP_Undefined = Color.Black
                WinMode_Light = False
                AppMode_Light = False
                Transparency = True
                ApplyAccentonTitlebars = False
                ApplyAccentonTaskbar = ApplyAccentonTaskbar_Level.None
#End Region

#Region "Aero"
                Aero_ColorizationColor = Color.Black
                Aero_ColorizationAfterglow = Color.Black
                Aero_EnableAeroPeek = True
                Aero_AlwaysHibernateThumbnails = False
                Aero_ColorizationColorBalance = 8
                Aero_ColorizationAfterglowBalance = 31
                Aero_ColorizationBlurBalance = 31
                Aero_ColorizationGlassReflectionIntensity = 50
                Aero_Theme = AeroTheme.Aero
#End Region

#Region "Metro"
                Metro_Start = 0
                Metro_StartColor = Color.Black
                Metro_AccentColor = Color.Black
                Metro_Theme = AeroTheme.Aero
                Metro_LogonUI = 0
                Metro_PersonalColors_Background = Color.Black
                Metro_PersonalColors_Accent = Color.Black
                Metro_NoLockScreen = False
                Metro_LockScreenType = LogonUI_Modes.Default_
                Metro_LockScreenSystemID = 0
#End Region

#Region "LogonUI"
                LogonUI_DisableAcrylicBackgroundOnLogon = False
                LogonUI_DisableLogonBackgroundImage = False
                LogonUI_NoLockScreen = False
#End Region

#Region "LogonUI 7"
                LogonUI7_Enabled = False
                LogonUI7_Mode = LogonUI_Modes.Default_
                LogonUI7_ImagePath = "C:\Windows\Web\Wallpaper\Windows\img0.jpg"
                LogonUI7_Color = Color.Black
                LogonUI7_Effect_Blur = False
                LogonUI7_Effect_Blur_Intensity = 0
                LogonUI7_Effect_Grayscale = False
                LogonUI7_Effect_Noise = False
                LogonUI7_Effect_Noise_Mode = LogonUI7_NoiseMode.Acrylic
                LogonUI7_Effect_Noise_Intensity = 0
#End Region

#Region "Win32UI"
                Win32UI_EnableTheming = True
                Win32UI_EnableGradient = True
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
#End Region

#Region "Terminals"

#Region "Locking"
                Terminal_CMD_Enabled = False
                Terminal_PS_32_Enabled = False
                Terminal_PS_64_Enabled = False
                Terminal_Stable_Enabled = False
                Terminal_Preview_Enabled = False
#End Region

#Region "Command Prompt"
                CMD_ColorTable00 = Color.FromArgb(12, 12, 12)
                CMD_ColorTable01 = Color.FromArgb(0, 55, 218)
                CMD_ColorTable02 = Color.FromArgb(19, 161, 14)
                CMD_ColorTable03 = Color.FromArgb(58, 150, 221)
                CMD_ColorTable04 = Color.FromArgb(197, 15, 31)
                CMD_ColorTable05 = Color.FromArgb(136, 23, 152)
                CMD_ColorTable06 = Color.FromArgb(193, 156, 0)
                CMD_ColorTable07 = Color.FromArgb(204, 204, 204)
                CMD_ColorTable08 = Color.FromArgb(118, 118, 118)
                CMD_ColorTable09 = Color.FromArgb(59, 120, 255)
                CMD_ColorTable10 = Color.FromArgb(22, 198, 12)
                CMD_ColorTable11 = Color.FromArgb(97, 214, 214)
                CMD_ColorTable12 = Color.FromArgb(231, 72, 86)
                CMD_ColorTable13 = Color.FromArgb(180, 0, 158)
                CMD_ColorTable14 = Color.FromArgb(249, 241, 165)
                CMD_ColorTable15 = Color.FromArgb(242, 242, 242)
                CMD_PopupForeground = 15
                CMD_PopupBackground = 5
                CMD_ScreenColorsForeground = 7
                CMD_ScreenColorsBackground = 0
                CMD_CursorSize = 19
                CMD_FaceName = "Consolas"
                CMD_FontRaster = False
                CMD_FontSize = 12 * 65536
                CMD_FontWeight = 400
                CMD_1909_CursorType = 0
                CMD_1909_CursorColor = Color.White
                CMD_1909_ForceV2 = True
                CMD_1909_LineSelection = False
                CMD_1909_TerminalScrolling = False
                CMD_1909_WindowAlpha = 100
#End Region

#Region "PowerShell x86"
                PS_32_ColorTable00 = Color.FromArgb(12, 12, 12)
                PS_32_ColorTable01 = Color.FromArgb(0, 55, 218)
                PS_32_ColorTable02 = Color.FromArgb(19, 161, 14)
                PS_32_ColorTable03 = Color.FromArgb(58, 150, 221)
                PS_32_ColorTable04 = Color.FromArgb(197, 15, 31)
                PS_32_ColorTable05 = Color.FromArgb(1, 36, 86)
                PS_32_ColorTable06 = Color.FromArgb(238, 237, 240)
                PS_32_ColorTable07 = Color.FromArgb(204, 204, 204)
                PS_32_ColorTable08 = Color.FromArgb(118, 118, 118)
                PS_32_ColorTable09 = Color.FromArgb(59, 120, 255)
                PS_32_ColorTable10 = Color.FromArgb(22, 198, 12)
                PS_32_ColorTable11 = Color.FromArgb(97, 214, 214)
                PS_32_ColorTable12 = Color.FromArgb(231, 72, 86)
                PS_32_ColorTable13 = Color.FromArgb(180, 0, 158)
                PS_32_ColorTable14 = Color.FromArgb(249, 241, 165)
                PS_32_ColorTable15 = Color.FromArgb(242, 242, 242)
                PS_32_PopupForeground = 15
                PS_32_PopupBackground = 3
                PS_32_ScreenColorsForeground = 6
                PS_32_ScreenColorsBackground = 8
                PS_32_CursorSize = 19
                PS_32_FaceName = "Consolas"
                PS_32_FontRaster = False
                PS_32_FontSize = 12 * 65536
                PS_32_FontWeight = 400
                PS_32_1909_CursorType = 0
                PS_32_1909_CursorColor = Color.White
                PS_32_1909_ForceV2 = True
                PS_32_1909_LineSelection = False
                PS_32_1909_TerminalScrolling = False
                PS_32_1909_WindowAlpha = 100
#End Region

#Region "PowerShell x64"
                PS_64_ColorTable00 = Color.FromArgb(12, 12, 12)
                PS_64_ColorTable01 = Color.FromArgb(0, 55, 218)
                PS_64_ColorTable02 = Color.FromArgb(19, 161, 14)
                PS_64_ColorTable03 = Color.FromArgb(58, 150, 221)
                PS_64_ColorTable04 = Color.FromArgb(197, 15, 31)
                PS_64_ColorTable05 = Color.FromArgb(1, 36, 86)
                PS_64_ColorTable06 = Color.FromArgb(238, 237, 240)
                PS_64_ColorTable07 = Color.FromArgb(204, 204, 204)
                PS_64_ColorTable08 = Color.FromArgb(118, 118, 118)
                PS_64_ColorTable09 = Color.FromArgb(59, 120, 255)
                PS_64_ColorTable10 = Color.FromArgb(22, 198, 12)
                PS_64_ColorTable11 = Color.FromArgb(97, 214, 214)
                PS_64_ColorTable12 = Color.FromArgb(231, 72, 86)
                PS_64_ColorTable13 = Color.FromArgb(180, 0, 158)
                PS_64_ColorTable14 = Color.FromArgb(249, 241, 165)
                PS_64_ColorTable15 = Color.FromArgb(242, 242, 242)
                PS_64_PopupForeground = 15
                PS_64_PopupBackground = 3
                PS_64_ScreenColorsForeground = 6
                PS_64_ScreenColorsBackground = 8
                PS_64_CursorSize = 19
                PS_64_FaceName = "Consolas"
                PS_64_FontRaster = False
                PS_64_FontSize = 12 * 65536
                PS_64_FontWeight = 400
                PS_64_1909_CursorType = 0
                PS_64_1909_CursorColor = Color.White
                PS_64_1909_ForceV2 = True
                PS_64_1909_LineSelection = False
                PS_64_1909_TerminalScrolling = False
                PS_64_1909_WindowAlpha = 100
#End Region

#Region "Windows Terminal"
                Terminal = New WinTerminal("", WinTerminal.Mode.Empty)
                TerminalPreview = New WinTerminal("", WinTerminal.Mode.Empty, WinTerminal.Version.Preview)
#End Region

#End Region

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

#Region "IBeam"
                Cursor_IBeam_PrimaryColor1 = Color.White
                Cursor_IBeam_PrimaryColor2 = Color.White
                Cursor_IBeam_PrimaryColorGradient = False
                Cursor_IBeam_PrimaryColorGradientMode = GradientMode.Vertical
                Cursor_IBeam_PrimaryColorNoise = False
                Cursor_IBeam_PrimaryColorNoiseOpacity = 0.25
                Cursor_IBeam_SecondaryColor1 = Color.FromArgb(64, 65, 75)
                Cursor_IBeam_SecondaryColor2 = Color.FromArgb(64, 65, 75)
                Cursor_IBeam_SecondaryColorGradient = False
                Cursor_IBeam_SecondaryColorGradientMode = GradientMode.Vertical
                Cursor_IBeam_SecondaryColorNoise = False
                Cursor_IBeam_SecondaryColorNoiseOpacity = 0.25
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
#End Region

        End Select
    End Sub

    Sub Save(ByVal [SaveTo] As SavingMode, Optional ByVal FileLocation As String = "", Optional ByVal ShowProgress As Boolean = True)
        Select Case [SaveTo]
            Case SavingMode.Registry

#Region "Registry"

                If ShowProgress Then
                    If My.W7 Or My.W8 Then
                        th = New Threading.Thread(AddressOf Task_A)
                        th.Start()
                    End If
                End If

#Region "Modern Windows"
                If Not My.W7 And Not My.W8 Then

                    EditReg("HKEY_CURRENT_USER\Control Panel\Desktop", "AutoColorization", 0)

                    Dim Colors As Byte() = {(ActionCenter_AppsLinks).R, (ActionCenter_AppsLinks).G, (ActionCenter_AppsLinks).B, (ActionCenter_AppsLinks).A _
                         , (Taskbar_Icon_Underline).R, (Taskbar_Icon_Underline).G, (Taskbar_Icon_Underline).B, (Taskbar_Icon_Underline).A _
                         , (StartButton_Hover).R, (StartButton_Hover).G, (StartButton_Hover).B, (StartButton_Hover).A _
                         , (SettingsIconsAndLinks).R, (SettingsIconsAndLinks).G, (SettingsIconsAndLinks).B, (SettingsIconsAndLinks).A _
                         , (StartMenuBackground_ActiveTaskbarButton).R, (StartMenuBackground_ActiveTaskbarButton).G, (StartMenuBackground_ActiveTaskbarButton).B, (StartMenuBackground_ActiveTaskbarButton).A _
                         , (StartListFolders_TaskbarFront).R, (StartListFolders_TaskbarFront).G, (StartListFolders_TaskbarFront).B, (StartListFolders_TaskbarFront).A _
                         , (Taskbar_Background).R, (Taskbar_Background).G, (Taskbar_Background).B, (Taskbar_Background).A _
                         , (UWP_Undefined).R, (UWP_Undefined).G, (UWP_Undefined).B, (UWP_Undefined).A}

                    Select Case ApplyAccentonTaskbar
                        Case ApplyAccentonTaskbar_Level.None
                            EditReg("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "ColorPrevalence", 0)

                        Case ApplyAccentonTaskbar_Level.Taskbar_Start_AC
                            EditReg("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "ColorPrevalence", 1)

                        Case ApplyAccentonTaskbar_Level.Taskbar
                            EditReg("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "ColorPrevalence", 2)

                        Case Else
                            EditReg("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "ColorPrevalence", 0)
                    End Select

                    EditReg("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "AccentColorMenu", Titlebar_Active.ToArgb)
                    EditReg("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\DWM", "ColorPrevalence", If(ApplyAccentonTitlebars, 1, 0))
                    EditReg("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "AccentPalette", Colors, True)
                    EditReg("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "StartColorMenu", BizareColorInvertor(StartMenu_Accent).ToArgb)


                    EditReg("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "AccentColorMenu", BizareColorInvertor(Titlebar_Active).ToArgb)
                    EditReg("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\DWM", "AccentColor", BizareColorInvertor(Titlebar_Active).ToArgb)
                    EditReg("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\DWM", "AccentColorInactive", BizareColorInvertor(Titlebar_Inactive).ToArgb)

                    EditReg("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "SystemUsesLightTheme", If(WinMode_Light, 1, 0))
                    EditReg("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "AppsUseLightTheme", If(AppMode_Light, 1, 0))
                    EditReg("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "EnableTransparency", If(Transparency, 1, 0))




                End If
#End Region

#Region "Aero"
                If My.W7 Then
                    SetCtrlTxt(My.Application.LanguageHelper.CP_ApplyingTheme, f)

                    Dim CWindows As String = Environment.GetFolderPath(Environment.SpecialFolder.Windows)

                    Select Case Aero_Theme
                        Case AeroTheme.Aero
                            NativeMethods.Uxtheme.EnableTheming(1)
                            NativeMethods.Uxtheme.SetSystemVisualStyle(CWindows & "\resources\Themes\Aero\Aero.msstyles", "NormalColor", "NormalSize", 0)

                            EditReg("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "CompositionPolicy", 2)
                            EditReg("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "Composition", 1)
                            EditReg("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationOpaqueBlend", 0)

                        Case AeroTheme.AeroOpaque
                            NativeMethods.Uxtheme.EnableTheming(1)
                            NativeMethods.Uxtheme.SetSystemVisualStyle(CWindows & "\resources\Themes\Aero\Aero.msstyles", "NormalColor", "NormalSize", 0)

                            EditReg("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "CompositionPolicy", 2)
                            EditReg("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "Composition", 1)
                            EditReg("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationOpaqueBlend", 1)

                        Case AeroTheme.Basic
                            NativeMethods.Uxtheme.EnableTheming(1)
                            NativeMethods.Uxtheme.SetSystemVisualStyle(CWindows & "\resources\Themes\Aero\Aero.msstyles", "NormalColor", "NormalSize", 0)

                            EditReg("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "CompositionPolicy", 1)
                            EditReg("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "Composition", 0)
                            EditReg("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationOpaqueBlend", 0)


                        Case AeroTheme.Classic
                            NativeMethods.Uxtheme.EnableTheming(0)

                    End Select

                    SetCtrlTxt(My.Application.LanguageHelper.CP_ApplyingColorsAndTweaks, f)

                    EditReg("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationAfterglow", Aero_ColorizationAfterglow.ToArgb)
                    EditReg("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationAfterglowBalance", Aero_ColorizationAfterglowBalance)
                    EditReg("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationBlurBalance", Aero_ColorizationBlurBalance)
                    EditReg("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationGlassReflectionIntensity", Aero_ColorizationGlassReflectionIntensity)

                    EditReg("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationColor", Aero_ColorizationColor.ToArgb)
                    EditReg("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationColorBalance", Aero_ColorizationColorBalance)

                    EditReg("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "EnableAeroPeek", If(Aero_EnableAeroPeek, 1, 0))
                    EditReg("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "AlwaysHibernateThumbnails", If(Aero_AlwaysHibernateThumbnails, 1, 0))
                    EditReg("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "EnableWindowColorization", 1)
                End If


#End Region

#Region "Metro"
                If My.W8 Then
                    SetCtrlTxt(My.Application.LanguageHelper.CP_ApplyingTheme, f)

                    EditReg("HKEY_CURRENT_USER\Control Panel\Desktop", "AutoColorization", 0)

                    Try
                        Select Case Metro_Theme
                            Case AeroTheme.Aero
                                NativeMethods.Uxtheme.EnableTheming(1)
                                NativeMethods.Uxtheme.SetSystemVisualStyle("C:\WINDOWS\resources\Themes\Aero\Aero.msstyles", "NormalColor", "NormalSize", 0)
                            Case AeroTheme.AeroLite
                                NativeMethods.Uxtheme.EnableTheming(1)
                                NativeMethods.Uxtheme.SetSystemVisualStyle("C:\WINDOWS\resources\Themes\Aero\AeroLite.msstyles", "NormalColor", "NormalSize", 0)
                        End Select
                    Catch
                    End Try

                    SetCtrlTxt(My.Application.LanguageHelper.CP_ApplyingColorsAndTweaks, f)

                    EditReg("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationColor", Aero_ColorizationColor.ToArgb)
                    EditReg("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationColorBalance", Aero_ColorizationColorBalance)

                    EditReg("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "StartColor", BizareColorInvertor(Metro_StartColor).ToArgb)
                    EditReg("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "DefaultStartColor", BizareColorInvertor(Metro_StartColor).ToArgb)
                    EditReg("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "AccentColor", BizareColorInvertor(Metro_AccentColor).ToArgb)
                    EditReg("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "DefaultColorSet", Metro_LogonUI)


                    If My.Application.isElevated Then
                        EditReg("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Personalization", "ForceStartBackground", Metro_Start)
                        EditReg("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "DefaultColorSet", Metro_LogonUI)
                        EditReg("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Personalization", "PersonalColors_Background", "#" & RGB2HEX_oneline(Metro_PersonalColors_Background, False), False, True)
                        EditReg("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Personalization", "PersonalColors_Accent", "#" & RGB2HEX_oneline(Metro_PersonalColors_Accent, False), False, True)
                    Else
                        Dim ls As New List(Of String)
                        ls.Clear()
                        ls.Add("Windows Registry Editor Version 5.00")
                        ls.Add(vbCrLf)
                        ls.Add("[HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Personalization]")
                        ls.Add(String.Format("""PersonalColors_Background""=""#{0}""", "#" & RGB2HEX_oneline(Metro_PersonalColors_Background, False)))
                        ls.Add(String.Format("""PersonalColors_Accent""=""#{0}""", "#" & RGB2HEX_oneline(Metro_PersonalColors_Accent, False)))
                        ls.Add(String.Format("""ForceStartBackground""=dword:{0}", ReturnEightDigitsFromInt(Metro_Start)))
                        ls.Add(vbCrLf)
                        ls.Add("[HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent]")
                        ls.Add(String.Format("""DefaultColorSet""=dword:{0}", ReturnEightDigitsFromInt(Metro_LogonUI)))

                        Dim result As String = CStr_FromList(ls)

                        If Not IO.Directory.Exists(My.Application.appData) Then IO.Directory.CreateDirectory(My.Application.appData)

                        Dim tempreg As String = My.Application.appData & "\tempreg.reg"

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
                End If
#End Region

#Region "LogonUI"
                If Not My.W7 And Not My.W8 Then

                    If My.Application.isElevated Then
                        EditReg("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\System", "DisableAcrylicBackgroundOnLogon", If(LogonUI_DisableAcrylicBackgroundOnLogon, 1, 0))
                        EditReg("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\System", "DisableLogonBackgroundImage", If(LogonUI_DisableLogonBackgroundImage, 1, 0))
                        EditReg("HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Windows\Personalization", "NoLockScreen", If(LogonUI_NoLockScreen, 1, 0))
                    Else
                        Dim ls As New List(Of String)
                        ls.Clear()
                        ls.Add("Windows Registry Editor Version 5.00")
                        ls.Add(vbCrLf)
                        ls.Add("[HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Windows\Personalization]")
                        ls.Add(String.Format("""NoLockScreen""=dword:0000000{0}", If(LogonUI_NoLockScreen, 1, 0)))
                        ls.Add(vbCrLf)
                        ls.Add("[HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\System]")
                        ls.Add(String.Format("""DisableAcrylicBackgroundOnLogon""=dword:0000000{0}", If(LogonUI_DisableAcrylicBackgroundOnLogon, 1, 0)))
                        ls.Add(String.Format("""DisableLogonBackgroundImage""=dword:0000000{0}", If(LogonUI_DisableLogonBackgroundImage, 1, 0)))

                        Dim result As String = CStr_FromList(ls)

                        If Not IO.Directory.Exists(My.Application.appData) Then IO.Directory.CreateDirectory(My.Application.appData)

                        Dim tempreg As String = My.Application.appData & "\tempreg.reg"

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
                End If


#End Region

#Region "LogonUI 7"
                If My.W7 Then
                    SetCtrlTxt(My.Application.LanguageHelper.CP_ApplyingCustomLogonUI, f)

                    If My.Application.isElevated Then

                        My.Computer.Registry.LocalMachine.CreateSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Authentication\LogonUI\Background")
                        My.Computer.Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Authentication\LogonUI\Background", "OEMBackground", If(LogonUI7_Enabled, 1, 0))

                        My.Computer.Registry.LocalMachine.CreateSubKey("Software\Policies\Microsoft\Windows\System")
                        My.Computer.Registry.SetValue("HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Windows\System", "UseOEMBackground", If(LogonUI7_Enabled, 1, 0))

                    Else
                        Dim ls As New List(Of String)
                        ls.Clear()
                        ls.Add("Windows Registry Editor Version 5.00")
                        ls.Add(vbCrLf)
                        ls.Add("[HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Authentication\LogonUI\Background]")
                        ls.Add(String.Format("""OEMBackground""=dword:0000000{0}", If(LogonUI7_Enabled, 1, 0)))
                        ls.Add(vbCrLf)
                        ls.Add("[HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Windows\System]")
                        ls.Add(String.Format("""UseOEMBackground""=dword:0000000{0}", If(LogonUI7_Enabled, 1, 0)))

                        Dim result As String = CStr_FromList(ls)

                        If Not IO.Directory.Exists(My.Application.appData) Then IO.Directory.CreateDirectory(My.Application.appData)

                        Dim tempreg As String = My.Application.appData & "\tempreg.reg"

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

                    Dim rLog As RegistryKey = Registry.CurrentUser.CreateSubKey("Software\WinPaletter\LogonUI")

                    Select Case LogonUI7_Mode
                        Case LogonUI_Modes.Default_
                            rLog.SetValue("Mode", 0)

                        Case LogonUI_Modes.Wallpaper
                            rLog.SetValue("Mode", 1)

                        Case LogonUI_Modes.CustomImage
                            rLog.SetValue("Mode", 2)

                        Case LogonUI_Modes.SolidColor
                            rLog.SetValue("Mode", 3)
                    End Select

                    rLog.SetValue("ImagePath", LogonUI7_ImagePath)
                    rLog.SetValue("Color", LogonUI7_Color.ToArgb)
                    rLog.SetValue("Effect_Blur", If(LogonUI7_Effect_Blur, 1, 0))
                    rLog.SetValue("Effect_Blur_Intensity", LogonUI7_Effect_Blur_Intensity)
                    rLog.SetValue("Effect_Grayscale", If(LogonUI7_Effect_Grayscale, 1, 0))
                    rLog.SetValue("Effect_Noise", If(LogonUI7_Effect_Noise, 1, 0))

                    Select Case LogonUI7_Effect_Noise_Mode
                        Case LogonUI7_NoiseMode.Aero
                            rLog.SetValue("Noise_Mode", 0)

                        Case LogonUI7_NoiseMode.Acrylic
                            rLog.SetValue("Noise_Mode", 1)
                    End Select

                    rLog.SetValue("Effect_Noise_Intensity", LogonUI7_Effect_Noise_Intensity)
                    rLog.Flush()
                    rLog.Close()

                    If LogonUI7_Enabled Then
                        NativeMethods.Kernel32.Wow64DisableWow64FsRedirection(IntPtr.Zero)

                        Dim DirX As String = Environment.GetFolderPath(Environment.SpecialFolder.Windows) & "\system32\oobe\info\backgrounds"
                        Dim imageres As String = Environment.GetFolderPath(Environment.SpecialFolder.Windows) & "\system32\imageres.dll"

                        IO.Directory.CreateDirectory(DirX)

                        For Each fileX As String In My.Computer.FileSystem.GetFiles(DirX)
                            Try : Kill(fileX) : Catch : End Try
                        Next

                        Dim bmpList As New List(Of Bitmap)
                        bmpList.Clear()

                        Select Case LogonUI7_Mode
                            Case LogonUI_Modes.Default_

                                For i As Integer = 5031 To 5043 Step +1
                                    bmpList.Add(LoadFromDLL(imageres, i, "IMAGE", My.Computer.Screen.Bounds.Size.Width, My.Computer.Screen.Bounds.Size.Height))
                                Next

                            Case LogonUI_Modes.CustomImage

                                If IO.File.Exists(LogonUI7_ImagePath) Then
                                    bmpList.Add(Image.FromStream(New IO.FileStream(LogonUI7_ImagePath, IO.FileMode.Open, IO.FileAccess.Read)))
                                Else
                                    bmpList.Add(ColorToBitmap(Color.Black, My.Computer.Screen.Bounds.Size))
                                End If

                            Case LogonUI_Modes.SolidColor
                                bmpList.Add(ColorToBitmap(LogonUI7_Color, My.Computer.Screen.Bounds.Size))

                            Case LogonUI_Modes.Wallpaper
                                bmpList.Add(My.Application.GetCurrentWallpaper)
                        End Select


                        For x = 0 To bmpList.Count - 1
                            SetCtrlTxt(String.Format(My.Application.LanguageHelper.CP_RenderingCustomLogonUI_Progress & " {2} " & vbCrLf & "({0}/{1}) ...", x + 1, bmpList.Count, bmpList(x).Width & "x" & bmpList(x).Height), f)
                            If LogonUI7_Effect_Grayscale Then bmpList(x) = Grayscale(bmpList(x))
                            If LogonUI7_Effect_Blur Then bmpList(x) = BlurBitmap(bmpList(x), LogonUI7_Effect_Blur_Intensity)
                            If LogonUI7_Effect_Noise Then bmpList(x) = NoiseBitmap(bmpList(x), LogonUI7_Effect_Noise_Mode, LogonUI7_Effect_Noise_Intensity / 100)
                        Next

                        If bmpList.Count = 1 Then
                            bmpList(0).Save(DirX & "\backgroundDefault.jpg", Drawing.Imaging.ImageFormat.Jpeg)
                        Else
                            For x = 0 To bmpList.Count - 1
                                bmpList(x).Save(DirX & String.Format("\background{0}x{1}.jpg", bmpList(x).Width, bmpList(x).Height), Drawing.Imaging.ImageFormat.Jpeg)
                            Next
                        End If
                        NativeMethods.Kernel32.Wow64RevertWow64FsRedirection(IntPtr.Zero)
                    End If
                End If
#End Region

#Region "LogonUI 8"
                If My.W8 Then
                    SetCtrlTxt(My.Application.LanguageHelper.CP_ApplyingCustomLogonUI, f)

                    Dim lockimg As String = My.Application.appData & "\LockScreen.png"

                    If My.Application.isElevated Then
                        My.Computer.Registry.LocalMachine.CreateSubKey("Software\Policies\Microsoft\Windows\Personalization")
                        My.Computer.Registry.SetValue("HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Windows\Personalization", "NoLockScreen", If(Metro_NoLockScreen, 1, 0))
                        My.Computer.Registry.SetValue("HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Windows\Personalization", "LockScreenImage", lockimg)

                    Else
                        Dim ls As New List(Of String)
                        ls.Clear()
                        ls.Add("Windows Registry Editor Version 5.00")
                        ls.Add(vbCrLf)
                        ls.Add("[HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Windows\Personalization]")
                        ls.Add(String.Format("""NoLockScreen""=dword:0000000{0}", If(Metro_NoLockScreen, 1, 0)))
                        ls.Add(String.Format("""LockScreenImage""=""{0}""", lockimg))

                        Dim result As String = CStr_FromList(ls)

                        If Not IO.Directory.Exists(My.Application.appData) Then IO.Directory.CreateDirectory(My.Application.appData)

                        Dim tempreg As String = My.Application.appData & "\tempreg.reg"

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

                    Dim rLog As RegistryKey = Registry.CurrentUser.CreateSubKey("Software\WinPaletter\LogonUI")

                    Select Case Metro_LockScreenType
                        Case LogonUI_Modes.Default_
                            rLog.SetValue("Mode", 0)

                        Case LogonUI_Modes.Wallpaper
                            rLog.SetValue("Mode", 1)

                        Case LogonUI_Modes.CustomImage
                            rLog.SetValue("Mode", 2)

                        Case LogonUI_Modes.SolidColor
                            rLog.SetValue("Mode", 3)
                    End Select

                    rLog.SetValue("Metro_LockScreenSystemID", Metro_LockScreenSystemID)
                    rLog.SetValue("ImagePath", LogonUI7_ImagePath)
                    rLog.SetValue("Color", LogonUI7_Color.ToArgb)
                    rLog.SetValue("Effect_Blur", If(LogonUI7_Effect_Blur, 1, 0))
                    rLog.SetValue("Effect_Blur_Intensity", LogonUI7_Effect_Blur_Intensity)
                    rLog.SetValue("Effect_Grayscale", If(LogonUI7_Effect_Grayscale, 1, 0))
                    rLog.SetValue("Effect_Noise", If(LogonUI7_Effect_Noise, 1, 0))

                    Select Case LogonUI7_Effect_Noise_Mode
                        Case LogonUI7_NoiseMode.Aero
                            rLog.SetValue("Noise_Mode", 0)

                        Case LogonUI7_NoiseMode.Acrylic
                            rLog.SetValue("Noise_Mode", 1)
                    End Select

                    rLog.SetValue("Effect_Noise_Intensity", LogonUI7_Effect_Noise_Intensity)
                    rLog.Flush()
                    rLog.Close()

                    If Not Metro_NoLockScreen Then

                        Try : Kill(lockimg) : Catch : End Try
                        Dim bmp As Bitmap

                        Select Case Metro_LockScreenType

                            Case LogonUI_Modes.Default_
                                Dim syslock As String
                                If Not MainFrm.CP.Metro_LockScreenSystemID = 1 And Not MainFrm.CP.Metro_LockScreenSystemID = 3 Then
                                    syslock = String.Format(Environment.GetFolderPath(Environment.SpecialFolder.Windows) & "\Web\Screen\img10{0}.jpg", MainFrm.CP.Metro_LockScreenSystemID)
                                Else
                                    syslock = String.Format(Environment.GetFolderPath(Environment.SpecialFolder.Windows) & "\Web\Screen\img10{0}.png", MainFrm.CP.Metro_LockScreenSystemID)
                                End If

                                If IO.File.Exists(syslock) Then
                                    bmp = Image.FromStream(New IO.FileStream(syslock, IO.FileMode.Open, IO.FileAccess.Read))
                                Else
                                    bmp = ColorToBitmap(Color.Black, My.Computer.Screen.Bounds.Size)
                                End If

                            Case LogonUI_Modes.CustomImage

                                If IO.File.Exists(LogonUI7_ImagePath) Then
                                    bmp = Image.FromStream(New IO.FileStream(LogonUI7_ImagePath, IO.FileMode.Open, IO.FileAccess.Read))
                                Else
                                    bmp = ColorToBitmap(Color.Black, My.Computer.Screen.Bounds.Size)
                                End If

                            Case LogonUI_Modes.SolidColor
                                bmp = ColorToBitmap(LogonUI7_Color, My.Computer.Screen.Bounds.Size)

                            Case LogonUI_Modes.Wallpaper
                                bmp = My.Application.GetCurrentWallpaper
                        End Select

                        SetCtrlTxt(My.Application.LanguageHelper.CP_RenderingCustomLogonUI, f)

                        If LogonUI7_Effect_Grayscale Then bmp = Grayscale(bmp)
                        If LogonUI7_Effect_Blur Then bmp = BlurBitmap(bmp, LogonUI7_Effect_Blur_Intensity)
                        If LogonUI7_Effect_Noise Then bmp = NoiseBitmap(bmp, LogonUI7_Effect_Noise_Mode, LogonUI7_Effect_Noise_Intensity / 100)
                        bmp.Save(lockimg, Drawing.Imaging.ImageFormat.Png)
                    End If

                End If
#End Region

#Region "Win32UI"
                If My.W7 Or My.W8 Then SetCtrlTxt(My.Application.LanguageHelper.CP_ApplyingWin32UI, f)

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

                NativeMethods.User32.SetSysColors(C1.Count, C1.ToArray(), C2.ToArray())


                My.Computer.Registry.SetValue("HKEY_CURRENT_USER\Control Panel\Desktop", "UserPreferencesMask", SetUserPreferenceMask(17, Win32UI_EnableTheming), RegistryValueKind.Binary)
                My.Computer.Registry.SetValue("HKEY_CURRENT_USER\Control Panel\Desktop", "UserPreferencesMask", SetUserPreferenceMask(4, Win32UI_EnableGradient), RegistryValueKind.Binary)

                NativeMethods.User32.SystemParametersInfo(Metrics.SPI.SPI_SETFLATMENU, 0, If(Win32UI_EnableTheming, 1, 0), 0)
                NativeMethods.User32.SystemParametersInfo(Metrics.SPI.SPI_SETGRADIENTCAPTIONS, 0, If(Win32UI_EnableGradient, 1, 0), 0)

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

                If My.W7 Or My.W8 Then RefreshDWM(Me)

#Region "Metrics With Fonts"


#Region "Reading from WinAPI"
                Dim NCM As New NONCLIENTMETRICS With {.cbSize = Marshal.SizeOf(NCM)}
                Dim anim As New ANIMATIONINFO With {.cbSize = Marshal.SizeOf(anim)}
                Dim ICO As New ICONMETRICS With {.cbSize = Marshal.SizeOf(ICO)}

                SystemParametersInfo(SPI.SPI_GETNONCLIENTMETRICS, NCM.cbSize, NCM, SPIF.None)
                SystemParametersInfo(SPI.SPI_GETANIMATION, anim.cbSize, anim, SPIF.None)
                SystemParametersInfo(SPI.SPI_GETICONMETRICS, ICO.cbSize, ICO, SPIF.None)
#End Region

#Region "Getting LogFont from Font"
                Dim lfCaptionFont As New LogFont : Fonts_CaptionFont.ToLogFont(lfCaptionFont)
                Dim lfIconFont As New LogFont : Fonts_IconFont.ToLogFont(lfIconFont)
                Dim lfMenuFont As New LogFont : Fonts_MenuFont.ToLogFont(lfMenuFont)
                Dim lfMessageFont As New LogFont : Fonts_MessageFont.ToLogFont(lfMessageFont)
                Dim lfSMCaptionFont As New LogFont : Fonts_SmCaptionFont.ToLogFont(lfSMCaptionFont)
                Dim lfStatusFont As New LogFont : Fonts_StatusFont.ToLogFont(lfStatusFont)
#End Region

#Region "Writing to Registry"
                EditReg("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "CaptionFont", LogFontHelper.LogFontToByte(lfCaptionFont), True)
                EditReg("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "IconFont", LogFontHelper.LogFontToByte(lfIconFont), True)
                EditReg("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "MenuFont", LogFontHelper.LogFontToByte(lfMenuFont), True)
                EditReg("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "MessageFont", LogFontHelper.LogFontToByte(lfMessageFont), True)
                EditReg("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "SmCaptionFont", LogFontHelper.LogFontToByte(lfSMCaptionFont), True)
                EditReg("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "StatusFont", LogFontHelper.LogFontToByte(lfStatusFont), True)

                EditReg("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "BorderWidth", Metrics_BorderWidth * -15, False, True)
                EditReg("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "CaptionHeight", Metrics_CaptionHeight * -15, False, True)
                EditReg("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "CaptionWidth", Metrics_CaptionWidth * -15, False, True)
                EditReg("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "IconSpacing", Metrics_IconSpacing * -15, False, True)
                EditReg("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "IconVerticalSpacing", Metrics_IconVerticalSpacing * -15, False, True)
                EditReg("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "MenuHeight", Metrics_MenuHeight * -15, False, True)
                EditReg("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "MenuWidth", Metrics_MenuWidth * -15, False, True)
                EditReg("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "MinAnimate", If(Metrics_MinAnimate, 1, 0), False, True)
                EditReg("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "PaddedBorderWidth", Metrics_PaddedBorderWidth * -15, False, True)
                EditReg("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "ScrollHeight", Metrics_ScrollHeight * -15, False, True)
                EditReg("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "ScrollWidth", Metrics_ScrollWidth * -15, False, True)
                EditReg("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "SmCaptionHeight", Metrics_SmCaptionHeight * -15, False, True)
                EditReg("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "SmCaptionWidth", Metrics_SmCaptionWidth * -15, False, True)
                EditReg("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "Shell Icon Size", Metrics_ShellIconSize, False, True)
                EditReg("HKEY_CURRENT_USER\Software\Microsoft\Windows\Shell\Bags\1\Desktop", "IconSize", Metrics_DesktopIconSize, False, True)
#End Region

#Region "Writing by WinAPI"
                anim.IMinAnimate = If(Metrics_MinAnimate, 1, 0)

                With NCM
                    .lfCaptionFont = lfCaptionFont        'Requires LogOff
                    .lfSMCaptionFont = lfSMCaptionFont    'Requires LogOff
                    .lfStatusFont = lfStatusFont          'Requires LogOff
                    .lfMenuFont = lfMenuFont
                    .lfMessageFont = lfMessageFont

                    .iBorderWidth = Metrics_BorderWidth
                    .iScrollWidth = Metrics_ScrollWidth
                    .iScrollHeight = Metrics_ScrollHeight
                    .iCaptionWidth = Metrics_CaptionWidth
                    .iCaptionHeight = Metrics_CaptionHeight
                    .iSMCaptionWidth = Metrics_SmCaptionWidth
                    .iSMCaptionHeight = Metrics_SmCaptionHeight
                    .iMenuWidth = Metrics_MenuWidth
                    .iMenuHeight = Metrics_MenuHeight
                    .iPaddedBorderWidth = Metrics_PaddedBorderWidth
                End With

                With ICO
                    .iHorzSpacing = Metrics_IconSpacing
                    .iVertSpacing = Metrics_IconVerticalSpacing
                    .lfFont = lfIconFont
                End With

                SystemParametersInfo(SPI.SPI_SETNONCLIENTMETRICS, Marshal.SizeOf(NCM), NCM, SPIF.SPIF_SENDCHANGE)
                SystemParametersInfo(SPI.SPI_SETANIMATION, Marshal.SizeOf(anim), anim, SPIF.SPIF_SENDCHANGE)
                SystemParametersInfo(SPI.SPI_SETICONMETRICS, Marshal.SizeOf(ICO), ICO, SPIF.SPIF_SENDCHANGE)
#End Region

#End Region

#Region "Terminals"
                If My.W7 Or My.W8 Then SetCtrlTxt(My.Application.LanguageHelper.CP_ApplyingTerminalColors, f)

                Dim rLogX As RegistryKey = Registry.CurrentUser.CreateSubKey("Software\WinPaletter\Terminals")

#Region "Locking"
                rLogX.SetValue("Terminal_CMD_Enabled", If(Terminal_CMD_Enabled, 1, 0))
                rLogX.SetValue("Terminal_PS_32_Enabled", If(Terminal_PS_32_Enabled, 1, 0))
                rLogX.SetValue("Terminal_PS_64_Enabled", If(Terminal_PS_64_Enabled, 1, 0))
                rLogX.SetValue("Terminal_Stable_Enabled", If(Terminal_Stable_Enabled, 1, 0))
                rLogX.SetValue("Terminal_Preview_Enabled", If(Terminal_Preview_Enabled, 1, 0))
#End Region

#Region "Command Prompt"
                If Terminal_CMD_Enabled Then
                    EditReg("HKEY_CURRENT_USER\Console", "EnableColorSelection", 1)
                    EditReg("HKEY_CURRENT_USER\Console", "ColorTable00", Color.FromArgb(0, BizareColorInvertor(CMD_ColorTable00)).ToArgb)
                    EditReg("HKEY_CURRENT_USER\Console", "ColorTable01", Color.FromArgb(0, BizareColorInvertor(CMD_ColorTable01)).ToArgb)
                    EditReg("HKEY_CURRENT_USER\Console", "ColorTable02", Color.FromArgb(0, BizareColorInvertor(CMD_ColorTable02)).ToArgb)
                    EditReg("HKEY_CURRENT_USER\Console", "ColorTable03", Color.FromArgb(0, BizareColorInvertor(CMD_ColorTable03)).ToArgb)
                    EditReg("HKEY_CURRENT_USER\Console", "ColorTable04", Color.FromArgb(0, BizareColorInvertor(CMD_ColorTable04)).ToArgb)
                    EditReg("HKEY_CURRENT_USER\Console", "ColorTable05", Color.FromArgb(0, BizareColorInvertor(CMD_ColorTable05)).ToArgb)
                    EditReg("HKEY_CURRENT_USER\Console", "ColorTable06", Color.FromArgb(0, BizareColorInvertor(CMD_ColorTable06)).ToArgb)
                    EditReg("HKEY_CURRENT_USER\Console", "ColorTable07", Color.FromArgb(0, BizareColorInvertor(CMD_ColorTable07)).ToArgb)
                    EditReg("HKEY_CURRENT_USER\Console", "ColorTable08", Color.FromArgb(0, BizareColorInvertor(CMD_ColorTable08)).ToArgb)
                    EditReg("HKEY_CURRENT_USER\Console", "ColorTable09", Color.FromArgb(0, BizareColorInvertor(CMD_ColorTable09)).ToArgb)
                    EditReg("HKEY_CURRENT_USER\Console", "ColorTable10", Color.FromArgb(0, BizareColorInvertor(CMD_ColorTable10)).ToArgb)
                    EditReg("HKEY_CURRENT_USER\Console", "ColorTable11", Color.FromArgb(0, BizareColorInvertor(CMD_ColorTable11)).ToArgb)
                    EditReg("HKEY_CURRENT_USER\Console", "ColorTable12", Color.FromArgb(0, BizareColorInvertor(CMD_ColorTable12)).ToArgb)
                    EditReg("HKEY_CURRENT_USER\Console", "ColorTable13", Color.FromArgb(0, BizareColorInvertor(CMD_ColorTable13)).ToArgb)
                    EditReg("HKEY_CURRENT_USER\Console", "ColorTable14", Color.FromArgb(0, BizareColorInvertor(CMD_ColorTable14)).ToArgb)
                    EditReg("HKEY_CURRENT_USER\Console", "ColorTable15", Color.FromArgb(0, BizareColorInvertor(CMD_ColorTable15)).ToArgb)
                    EditReg("HKEY_CURRENT_USER\Console", "PopupColors", Convert.ToInt32(CMD_PopupBackground.ToString("X") & CMD_PopupForeground.ToString("X"), 16))
                    EditReg("HKEY_CURRENT_USER\Console", "ScreenColors", Convert.ToInt32(CMD_ScreenColorsBackground.ToString("X") & CMD_ScreenColorsForeground.ToString("X"), 16))
                    EditReg("HKEY_CURRENT_USER\Console", "CursorSize", CMD_CursorSize)

                    If CMD_FontRaster Then
                        EditReg("HKEY_CURRENT_USER\Console", "FaceName", "Terminal", False, True)
                        EditReg("HKEY_CURRENT_USER\Console", "FontFamily", 48)
                    Else
                        EditReg("HKEY_CURRENT_USER\Console", "FaceName", CMD_FaceName, False, True)
                        EditReg("HKEY_CURRENT_USER\Console", "FontFamily", If(CMD_FontRaster, 1, 54))
                    End If

                    EditReg("HKEY_CURRENT_USER\Console", "FontSize", CMD_FontSize)
                    EditReg("HKEY_CURRENT_USER\Console", "FontWeight", CMD_FontWeight)

                    If My.W10_1909 Then
                        EditReg("HKEY_CURRENT_USER\Console", "CursorColor", Color.FromArgb(0, BizareColorInvertor(CMD_1909_CursorColor)).ToArgb)
                        EditReg("HKEY_CURRENT_USER\Console", "CursorType", CMD_1909_CursorType)
                        EditReg("HKEY_CURRENT_USER\Console", "WindowAlpha", CMD_1909_WindowAlpha)
                        EditReg("HKEY_CURRENT_USER\Console", "ForceV2", If(CMD_1909_ForceV2, 1, 0))
                        EditReg("HKEY_CURRENT_USER\Console", "LineSelection", If(CMD_1909_LineSelection, 1, 0))
                        EditReg("HKEY_CURRENT_USER\Console", "TerminalScrolling", If(CMD_1909_TerminalScrolling, 1, 0))
                    End If

                    If My.Application._Settings.CMD_OverrideUserPreferences Then
                        Try
                            Registry.CurrentUser.CreateSubKey("Console\%SystemRoot%_System32_cmd.exe")
                        Catch
                        End Try

                        EditReg("HKEY_CURRENT_USER\Console\%SystemRoot%_System32_cmd.exe", "EnableColorSelection", 1)
                        EditReg("HKEY_CURRENT_USER\Console\%SystemRoot%_System32_cmd.exe", "ColorTable00", Color.FromArgb(0, BizareColorInvertor(CMD_ColorTable00)).ToArgb)
                        EditReg("HKEY_CURRENT_USER\Console\%SystemRoot%_System32_cmd.exe", "ColorTable01", Color.FromArgb(0, BizareColorInvertor(CMD_ColorTable01)).ToArgb)
                        EditReg("HKEY_CURRENT_USER\Console\%SystemRoot%_System32_cmd.exe", "ColorTable02", Color.FromArgb(0, BizareColorInvertor(CMD_ColorTable02)).ToArgb)
                        EditReg("HKEY_CURRENT_USER\Console\%SystemRoot%_System32_cmd.exe", "ColorTable03", Color.FromArgb(0, BizareColorInvertor(CMD_ColorTable03)).ToArgb)
                        EditReg("HKEY_CURRENT_USER\Console\%SystemRoot%_System32_cmd.exe", "ColorTable04", Color.FromArgb(0, BizareColorInvertor(CMD_ColorTable04)).ToArgb)
                        EditReg("HKEY_CURRENT_USER\Console\%SystemRoot%_System32_cmd.exe", "ColorTable05", Color.FromArgb(0, BizareColorInvertor(CMD_ColorTable05)).ToArgb)
                        EditReg("HKEY_CURRENT_USER\Console\%SystemRoot%_System32_cmd.exe", "ColorTable06", Color.FromArgb(0, BizareColorInvertor(CMD_ColorTable06)).ToArgb)
                        EditReg("HKEY_CURRENT_USER\Console\%SystemRoot%_System32_cmd.exe", "ColorTable07", Color.FromArgb(0, BizareColorInvertor(CMD_ColorTable07)).ToArgb)
                        EditReg("HKEY_CURRENT_USER\Console\%SystemRoot%_System32_cmd.exe", "ColorTable08", Color.FromArgb(0, BizareColorInvertor(CMD_ColorTable08)).ToArgb)
                        EditReg("HKEY_CURRENT_USER\Console\%SystemRoot%_System32_cmd.exe", "ColorTable09", Color.FromArgb(0, BizareColorInvertor(CMD_ColorTable09)).ToArgb)
                        EditReg("HKEY_CURRENT_USER\Console\%SystemRoot%_System32_cmd.exe", "ColorTable10", Color.FromArgb(0, BizareColorInvertor(CMD_ColorTable10)).ToArgb)
                        EditReg("HKEY_CURRENT_USER\Console\%SystemRoot%_System32_cmd.exe", "ColorTable11", Color.FromArgb(0, BizareColorInvertor(CMD_ColorTable11)).ToArgb)
                        EditReg("HKEY_CURRENT_USER\Console\%SystemRoot%_System32_cmd.exe", "ColorTable12", Color.FromArgb(0, BizareColorInvertor(CMD_ColorTable12)).ToArgb)
                        EditReg("HKEY_CURRENT_USER\Console\%SystemRoot%_System32_cmd.exe", "ColorTable13", Color.FromArgb(0, BizareColorInvertor(CMD_ColorTable13)).ToArgb)
                        EditReg("HKEY_CURRENT_USER\Console\%SystemRoot%_System32_cmd.exe", "ColorTable14", Color.FromArgb(0, BizareColorInvertor(CMD_ColorTable14)).ToArgb)
                        EditReg("HKEY_CURRENT_USER\Console\%SystemRoot%_System32_cmd.exe", "ColorTable15", Color.FromArgb(0, BizareColorInvertor(CMD_ColorTable15)).ToArgb)
                        EditReg("HKEY_CURRENT_USER\Console\%SystemRoot%_System32_cmd.exe", "PopupColors", Convert.ToInt32(CMD_PopupBackground.ToString("X") & CMD_PopupForeground.ToString("X"), 16))
                        EditReg("HKEY_CURRENT_USER\Console\%SystemRoot%_System32_cmd.exe", "ScreenColors", Convert.ToInt32(CMD_ScreenColorsBackground.ToString("X") & CMD_ScreenColorsForeground.ToString("X"), 16))
                        EditReg("HKEY_CURRENT_USER\Console\%SystemRoot%_System32_cmd.exe", "CursorSize", CMD_CursorSize)

                        If CMD_FontRaster Then
                            EditReg("HKEY_CURRENT_USER\Console\%SystemRoot%_System32_cmd.exe", "FaceName", "Terminal", False, True)
                            EditReg("HKEY_CURRENT_USER\Console\%SystemRoot%_System32_cmd.exe", "FontFamily", 48)
                        Else
                            EditReg("HKEY_CURRENT_USER\Console\%SystemRoot%_System32_cmd.exe", "FaceName", CMD_FaceName, False, True)
                            EditReg("HKEY_CURRENT_USER\Console\%SystemRoot%_System32_cmd.exe", "FontFamily", If(CMD_FontRaster, 1, 54))
                        End If

                        EditReg("HKEY_CURRENT_USER\Console\%SystemRoot%_System32_cmd.exe", "FontSize", CMD_FontSize)


                        EditReg("HKEY_CURRENT_USER\Console\%SystemRoot%_System32_cmd.exe", "FontWeight", CMD_FontWeight)

                        If My.W10_1909 Then
                            EditReg("HKEY_CURRENT_USER\Console\%SystemRoot%_System32_cmd.exe", "CursorColor", Color.FromArgb(0, BizareColorInvertor(CMD_1909_CursorColor)).ToArgb)
                            EditReg("HKEY_CURRENT_USER\Console\%SystemRoot%_System32_cmd.exe", "CursorType", CMD_1909_CursorType)
                            EditReg("HKEY_CURRENT_USER\Console\%SystemRoot%_System32_cmd.exe", "WindowAlpha", CMD_1909_WindowAlpha)
                            EditReg("HKEY_CURRENT_USER\Console\%SystemRoot%_System32_cmd.exe", "ForceV2", If(CMD_1909_ForceV2, 1, 0))
                            EditReg("HKEY_CURRENT_USER\Console\%SystemRoot%_System32_cmd.exe", "LineSelection", If(CMD_1909_LineSelection, 1, 0))
                            EditReg("HKEY_CURRENT_USER\Console\%SystemRoot%_System32_cmd.exe", "TerminalScrolling", If(CMD_1909_TerminalScrolling, 1, 0))
                        End If
                    End If


                    If My.Application.isElevated Then
                        EditReg("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Console\TrueTypeFont", "000", CMD_FaceName, False, True)
                    Else
                        Dim ls As New List(Of String)
                        ls.Clear()
                        ls.Add("Windows Registry Editor Version 5.00")
                        ls.Add(vbCrLf)
                        ls.Add("[HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Console\TrueTypeFont]")
                        ls.Add(String.Format("""000""=""{0}""", CMD_FaceName))

                        Dim result As String = CStr_FromList(ls)

                        If Not IO.Directory.Exists(My.Application.appData) Then IO.Directory.CreateDirectory(My.Application.appData)

                        Dim tempreg As String = My.Application.appData & "\tempreg.reg"

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
                End If
#End Region

#Region "PowerShell x86"
                If Terminal_PS_32_Enabled Then
                    If IO.Directory.Exists(Environment.GetEnvironmentVariable("WINDIR") & "\System32\WindowsPowerShell\v1.0") Then
                        Try
                            Registry.CurrentUser.CreateSubKey("Console\%SystemRoot%_System32_WindowsPowerShell_v1.0_powershell.exe", True).Close()
                        Catch

                        End Try

                        EditReg("HKEY_CURRENT_USER\Console\%SystemRoot%_System32_WindowsPowerShell_v1.0_powershell.exe", "EnableColorSelection", 1)
                        EditReg("HKEY_CURRENT_USER\Console\%SystemRoot%_System32_WindowsPowerShell_v1.0_powershell.exe", "ColorTable00", Color.FromArgb(0, BizareColorInvertor(PS_32_ColorTable00)).ToArgb)
                        EditReg("HKEY_CURRENT_USER\Console\%SystemRoot%_System32_WindowsPowerShell_v1.0_powershell.exe", "ColorTable01", Color.FromArgb(0, BizareColorInvertor(PS_32_ColorTable01)).ToArgb)
                        EditReg("HKEY_CURRENT_USER\Console\%SystemRoot%_System32_WindowsPowerShell_v1.0_powershell.exe", "ColorTable02", Color.FromArgb(0, BizareColorInvertor(PS_32_ColorTable02)).ToArgb)
                        EditReg("HKEY_CURRENT_USER\Console\%SystemRoot%_System32_WindowsPowerShell_v1.0_powershell.exe", "ColorTable03", Color.FromArgb(0, BizareColorInvertor(PS_32_ColorTable03)).ToArgb)
                        EditReg("HKEY_CURRENT_USER\Console\%SystemRoot%_System32_WindowsPowerShell_v1.0_powershell.exe", "ColorTable04", Color.FromArgb(0, BizareColorInvertor(PS_32_ColorTable04)).ToArgb)
                        EditReg("HKEY_CURRENT_USER\Console\%SystemRoot%_System32_WindowsPowerShell_v1.0_powershell.exe", "ColorTable05", Color.FromArgb(0, BizareColorInvertor(PS_32_ColorTable05)).ToArgb)
                        EditReg("HKEY_CURRENT_USER\Console\%SystemRoot%_System32_WindowsPowerShell_v1.0_powershell.exe", "ColorTable06", Color.FromArgb(0, BizareColorInvertor(PS_32_ColorTable06)).ToArgb)
                        EditReg("HKEY_CURRENT_USER\Console\%SystemRoot%_System32_WindowsPowerShell_v1.0_powershell.exe", "ColorTable07", Color.FromArgb(0, BizareColorInvertor(PS_32_ColorTable07)).ToArgb)
                        EditReg("HKEY_CURRENT_USER\Console\%SystemRoot%_System32_WindowsPowerShell_v1.0_powershell.exe", "ColorTable08", Color.FromArgb(0, BizareColorInvertor(PS_32_ColorTable08)).ToArgb)
                        EditReg("HKEY_CURRENT_USER\Console\%SystemRoot%_System32_WindowsPowerShell_v1.0_powershell.exe", "ColorTable09", Color.FromArgb(0, BizareColorInvertor(PS_32_ColorTable09)).ToArgb)
                        EditReg("HKEY_CURRENT_USER\Console\%SystemRoot%_System32_WindowsPowerShell_v1.0_powershell.exe", "ColorTable10", Color.FromArgb(0, BizareColorInvertor(PS_32_ColorTable10)).ToArgb)
                        EditReg("HKEY_CURRENT_USER\Console\%SystemRoot%_System32_WindowsPowerShell_v1.0_powershell.exe", "ColorTable11", Color.FromArgb(0, BizareColorInvertor(PS_32_ColorTable11)).ToArgb)
                        EditReg("HKEY_CURRENT_USER\Console\%SystemRoot%_System32_WindowsPowerShell_v1.0_powershell.exe", "ColorTable12", Color.FromArgb(0, BizareColorInvertor(PS_32_ColorTable12)).ToArgb)
                        EditReg("HKEY_CURRENT_USER\Console\%SystemRoot%_System32_WindowsPowerShell_v1.0_powershell.exe", "ColorTable13", Color.FromArgb(0, BizareColorInvertor(PS_32_ColorTable13)).ToArgb)
                        EditReg("HKEY_CURRENT_USER\Console\%SystemRoot%_System32_WindowsPowerShell_v1.0_powershell.exe", "ColorTable14", Color.FromArgb(0, BizareColorInvertor(PS_32_ColorTable14)).ToArgb)
                        EditReg("HKEY_CURRENT_USER\Console\%SystemRoot%_System32_WindowsPowerShell_v1.0_powershell.exe", "ColorTable15", Color.FromArgb(0, BizareColorInvertor(PS_32_ColorTable15)).ToArgb)
                        EditReg("HKEY_CURRENT_USER\Console\%SystemRoot%_System32_WindowsPowerShell_v1.0_powershell.exe", "PopupColors", Convert.ToInt32(PS_32_PopupBackground.ToString("X") & PS_32_PopupForeground.ToString("X"), 16))
                        EditReg("HKEY_CURRENT_USER\Console\%SystemRoot%_System32_WindowsPowerShell_v1.0_powershell.exe", "ScreenColors", Convert.ToInt32(PS_32_ScreenColorsBackground.ToString("X") & PS_32_ScreenColorsForeground.ToString("X"), 16))
                        EditReg("HKEY_CURRENT_USER\Console\%SystemRoot%_System32_WindowsPowerShell_v1.0_powershell.exe", "CursorSize", PS_32_CursorSize)

                        If PS_32_FontRaster Then
                            EditReg("HKEY_CURRENT_USER\Console\%SystemRoot%_System32_WindowsPowerShell_v1.0_powershell.exe", "FaceName", "Terminal", False, True)
                            EditReg("HKEY_CURRENT_USER\Console\%SystemRoot%_System32_WindowsPowerShell_v1.0_powershell.exe", "FontFamily", 48)
                        Else
                            EditReg("HKEY_CURRENT_USER\Console\%SystemRoot%_System32_WindowsPowerShell_v1.0_powershell.exe", "FaceName", PS_32_FaceName, False, True)
                            EditReg("HKEY_CURRENT_USER\Console\%SystemRoot%_System32_WindowsPowerShell_v1.0_powershell.exe", "FontFamily", If(PS_32_FontRaster, 1, 54))
                        End If

                        EditReg("HKEY_CURRENT_USER\Console\%SystemRoot%_System32_WindowsPowerShell_v1.0_powershell.exe", "FontSize", PS_32_FontSize)
                        EditReg("HKEY_CURRENT_USER\Console\%SystemRoot%_System32_WindowsPowerShell_v1.0_powershell.exe", "FontWeight", PS_32_FontWeight)

                        If My.W10_1909 Then
                            EditReg("HKEY_CURRENT_USER\Console\%SystemRoot%_System32_WindowsPowerShell_v1.0_powershell.exe", "CursorColor", Color.FromArgb(0, BizareColorInvertor(PS_32_1909_CursorColor)).ToArgb)
                            EditReg("HKEY_CURRENT_USER\Console\%SystemRoot%_System32_WindowsPowerShell_v1.0_powershell.exe", "CursorType", PS_32_1909_CursorType)
                            EditReg("HKEY_CURRENT_USER\Console\%SystemRoot%_System32_WindowsPowerShell_v1.0_powershell.exe", "WindowAlpha", PS_32_1909_WindowAlpha)
                            EditReg("HKEY_CURRENT_USER\Console\%SystemRoot%_System32_WindowsPowerShell_v1.0_powershell.exe", "ForceV2", If(PS_32_1909_ForceV2, 1, 0))
                            EditReg("HKEY_CURRENT_USER\Console\%SystemRoot%_System32_WindowsPowerShell_v1.0_powershell.exe", "LineSelection", If(PS_32_1909_LineSelection, 1, 0))
                            EditReg("HKEY_CURRENT_USER\Console\%SystemRoot%_System32_WindowsPowerShell_v1.0_powershell.exe", "TerminalScrolling", If(PS_32_1909_TerminalScrolling, 1, 0))
                        End If

                        If My.Application.isElevated Then
                            EditReg("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Console\%SystemRoot%_System32_WindowsPowerShell_v1.0_powershell.exe\TrueTypeFont", "000", PS_32_FaceName, False, True)
                        Else
                            Dim ls As New List(Of String)
                            ls.Clear()
                            ls.Add("Windows Registry Editor Version 5.00")
                            ls.Add(vbCrLf)
                            ls.Add("[HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Console\%SystemRoot%_System32_WindowsPowerShell_v1.0_powershell.exe\TrueTypeFont]")
                            ls.Add(String.Format("""000""=""{0}""", PS_32_FaceName))

                            Dim result As String = CStr_FromList(ls)

                            If Not IO.Directory.Exists(My.Application.appData) Then IO.Directory.CreateDirectory(My.Application.appData)

                            Dim tempreg As String = My.Application.appData & "\tempreg.reg"

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

                    End If
                End If
#End Region

#Region "PowerShell x64"
                If Terminal_PS_64_Enabled Then
                    If IO.Directory.Exists(Environment.GetEnvironmentVariable("WINDIR") & "\SysWOW64\WindowsPowerShell\v1.0") Then

                        Try
                            Registry.CurrentUser.CreateSubKey("Console\%SystemRoot%_SysWOW64_WindowsPowerShell_v1.0_powershell.exe", True).Close()
                        Catch

                        End Try

                        EditReg("HKEY_CURRENT_USER\Console\%SystemRoot%_SysWOW64_WindowsPowerShell_v1.0_powershell.exe", "EnableColorSelection", 1)
                        EditReg("HKEY_CURRENT_USER\Console\%SystemRoot%_SysWOW64_WindowsPowerShell_v1.0_powershell.exe", "ColorTable00", Color.FromArgb(0, BizareColorInvertor(PS_64_ColorTable00)).ToArgb)
                        EditReg("HKEY_CURRENT_USER\Console\%SystemRoot%_SysWOW64_WindowsPowerShell_v1.0_powershell.exe", "ColorTable01", Color.FromArgb(0, BizareColorInvertor(PS_64_ColorTable01)).ToArgb)
                        EditReg("HKEY_CURRENT_USER\Console\%SystemRoot%_SysWOW64_WindowsPowerShell_v1.0_powershell.exe", "ColorTable02", Color.FromArgb(0, BizareColorInvertor(PS_64_ColorTable02)).ToArgb)
                        EditReg("HKEY_CURRENT_USER\Console\%SystemRoot%_SysWOW64_WindowsPowerShell_v1.0_powershell.exe", "ColorTable03", Color.FromArgb(0, BizareColorInvertor(PS_64_ColorTable03)).ToArgb)
                        EditReg("HKEY_CURRENT_USER\Console\%SystemRoot%_SysWOW64_WindowsPowerShell_v1.0_powershell.exe", "ColorTable04", Color.FromArgb(0, BizareColorInvertor(PS_64_ColorTable04)).ToArgb)
                        EditReg("HKEY_CURRENT_USER\Console\%SystemRoot%_SysWOW64_WindowsPowerShell_v1.0_powershell.exe", "ColorTable05", Color.FromArgb(0, BizareColorInvertor(PS_64_ColorTable05)).ToArgb)
                        EditReg("HKEY_CURRENT_USER\Console\%SystemRoot%_SysWOW64_WindowsPowerShell_v1.0_powershell.exe", "ColorTable06", Color.FromArgb(0, BizareColorInvertor(PS_64_ColorTable06)).ToArgb)
                        EditReg("HKEY_CURRENT_USER\Console\%SystemRoot%_SysWOW64_WindowsPowerShell_v1.0_powershell.exe", "ColorTable07", Color.FromArgb(0, BizareColorInvertor(PS_64_ColorTable07)).ToArgb)
                        EditReg("HKEY_CURRENT_USER\Console\%SystemRoot%_SysWOW64_WindowsPowerShell_v1.0_powershell.exe", "ColorTable08", Color.FromArgb(0, BizareColorInvertor(PS_64_ColorTable08)).ToArgb)
                        EditReg("HKEY_CURRENT_USER\Console\%SystemRoot%_SysWOW64_WindowsPowerShell_v1.0_powershell.exe", "ColorTable09", Color.FromArgb(0, BizareColorInvertor(PS_64_ColorTable09)).ToArgb)
                        EditReg("HKEY_CURRENT_USER\Console\%SystemRoot%_SysWOW64_WindowsPowerShell_v1.0_powershell.exe", "ColorTable10", Color.FromArgb(0, BizareColorInvertor(PS_64_ColorTable10)).ToArgb)
                        EditReg("HKEY_CURRENT_USER\Console\%SystemRoot%_SysWOW64_WindowsPowerShell_v1.0_powershell.exe", "ColorTable11", Color.FromArgb(0, BizareColorInvertor(PS_64_ColorTable11)).ToArgb)
                        EditReg("HKEY_CURRENT_USER\Console\%SystemRoot%_SysWOW64_WindowsPowerShell_v1.0_powershell.exe", "ColorTable12", Color.FromArgb(0, BizareColorInvertor(PS_64_ColorTable12)).ToArgb)
                        EditReg("HKEY_CURRENT_USER\Console\%SystemRoot%_SysWOW64_WindowsPowerShell_v1.0_powershell.exe", "ColorTable13", Color.FromArgb(0, BizareColorInvertor(PS_64_ColorTable13)).ToArgb)
                        EditReg("HKEY_CURRENT_USER\Console\%SystemRoot%_SysWOW64_WindowsPowerShell_v1.0_powershell.exe", "ColorTable14", Color.FromArgb(0, BizareColorInvertor(PS_64_ColorTable14)).ToArgb)
                        EditReg("HKEY_CURRENT_USER\Console\%SystemRoot%_SysWOW64_WindowsPowerShell_v1.0_powershell.exe", "ColorTable15", Color.FromArgb(0, BizareColorInvertor(PS_64_ColorTable15)).ToArgb)
                        EditReg("HKEY_CURRENT_USER\Console\%SystemRoot%_SysWOW64_WindowsPowerShell_v1.0_powershell.exe", "PopupColors", Convert.ToInt32(PS_64_PopupBackground.ToString("X") & PS_64_PopupForeground.ToString("X"), 16))
                        EditReg("HKEY_CURRENT_USER\Console\%SystemRoot%_SysWOW64_WindowsPowerShell_v1.0_powershell.exe", "ScreenColors", Convert.ToInt32(PS_64_ScreenColorsBackground.ToString("X") & PS_64_ScreenColorsForeground.ToString("X"), 16))
                        EditReg("HKEY_CURRENT_USER\Console\%SystemRoot%_SysWOW64_WindowsPowerShell_v1.0_powershell.exe", "CursorSize", PS_64_CursorSize)

                        If PS_64_FontRaster Then
                            EditReg("HKEY_CURRENT_USER\Console\%SystemRoot%_SysWOW64_WindowsPowerShell_v1.0_powershell.exe", "FaceName", "Terminal", False, True)
                            EditReg("HKEY_CURRENT_USER\Console\%SystemRoot%_SysWOW64_WindowsPowerShell_v1.0_powershell.exe", "FontFamily", 48)
                        Else
                            EditReg("HKEY_CURRENT_USER\Console\%SystemRoot%_SysWOW64_WindowsPowerShell_v1.0_powershell.exe", "FaceName", PS_64_FaceName, False, True)
                            EditReg("HKEY_CURRENT_USER\Console\%SystemRoot%_SysWOW64_WindowsPowerShell_v1.0_powershell.exe", "FontFamily", If(PS_64_FontRaster, 1, 54))
                        End If


                        EditReg("HKEY_CURRENT_USER\Console\%SystemRoot%_SysWOW64_WindowsPowerShell_v1.0_powershell.exe", "FontSize", PS_64_FontSize)
                        EditReg("HKEY_CURRENT_USER\Console\%SystemRoot%_SysWOW64_WindowsPowerShell_v1.0_powershell.exe", "FontWeight", PS_64_FontWeight)

                        If My.W10_1909 Then
                            EditReg("HKEY_CURRENT_USER\Console\%SystemRoot%_SysWOW64_WindowsPowerShell_v1.0_powershell.exe", "CursorColor", Color.FromArgb(0, BizareColorInvertor(PS_64_1909_CursorColor)).ToArgb)
                            EditReg("HKEY_CURRENT_USER\Console\%SystemRoot%_SysWOW64_WindowsPowerShell_v1.0_powershell.exe", "CursorType", PS_64_1909_CursorType)
                            EditReg("HKEY_CURRENT_USER\Console\%SystemRoot%_SysWOW64_WindowsPowerShell_v1.0_powershell.exe", "WindowAlpha", PS_64_1909_WindowAlpha)
                            EditReg("HKEY_CURRENT_USER\Console\%SystemRoot%_SysWOW64_WindowsPowerShell_v1.0_powershell.exe", "ForceV2", If(PS_64_1909_ForceV2, 1, 0))
                            EditReg("HKEY_CURRENT_USER\Console\%SystemRoot%_SysWOW64_WindowsPowerShell_v1.0_powershell.exe", "LineSelection", If(PS_64_1909_LineSelection, 1, 0))
                            EditReg("HKEY_CURRENT_USER\Console\%SystemRoot%_SysWOW64_WindowsPowerShell_v1.0_powershell.exe", "TerminalScrolling", If(PS_64_1909_TerminalScrolling, 1, 0))
                        End If


                        If My.Application.isElevated Then
                            EditReg("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Console\%SystemRoot%_SysWOW64_WindowsPowerShell_v1.0_powershell.exe\TrueTypeFont", "000", PS_64_FaceName, False, True)
                        Else
                            Dim ls As New List(Of String)
                            ls.Clear()
                            ls.Add("Windows Registry Editor Version 5.00")
                            ls.Add(vbCrLf)
                            ls.Add("[HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Console\%SystemRoot%_SysWOW64_WindowsPowerShell_v1.0_powershell.exe\TrueTypeFont]")
                            ls.Add(String.Format("""000""=""{0}""", PS_64_FaceName))

                            Dim result As String = CStr_FromList(ls)

                            If Not IO.Directory.Exists(My.Application.appData) Then IO.Directory.CreateDirectory(My.Application.appData)

                            Dim tempreg As String = My.Application.appData & "\tempreg.reg"

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

                    End If
                End If
#End Region

#Region "Windows Terminals"
                If My.W10 Or My.W11 Then
                    Try
                        Dim TerDir As String
                        Dim TerPreDir As String

                        If Not My.Application._Settings.Terminal_Path_Deflection Then
                            TerDir = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) & "\AppData\Local\Packages\Microsoft.WindowsTerminal_8wekyb3d8bbwe\LocalState\settings.json"
                            TerPreDir = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) & "\AppData\Local\Packages\Microsoft.WindowsTerminalPreview_8wekyb3d8bbwe\LocalState\settings.json"
                        Else
                            If IO.File.Exists(My.Application._Settings.Terminal_Stable_Path) Then
                                TerDir = My.Application._Settings.Terminal_Stable_Path
                            Else
                                TerDir = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) & "\AppData\Local\Packages\Microsoft.WindowsTerminal_8wekyb3d8bbwe\LocalState\settings.json"
                            End If

                            If IO.File.Exists(My.Application._Settings.Terminal_Preview_Path) Then
                                TerPreDir = My.Application._Settings.Terminal_Preview_Path
                            Else
                                TerPreDir = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) & "\AppData\Local\Packages\Microsoft.WindowsTerminalPreview_8wekyb3d8bbwe\LocalState\settings.json"
                            End If
                        End If

                        If Terminal_Stable_Enabled Then
                            If IO.File.Exists(TerDir) Then
                                Terminal.Save(TerDir, WinTerminal.Mode.JSONFile)
                            End If
                        End If

                        If Terminal_Preview_Enabled Then
                            If IO.File.Exists(TerPreDir) Then
                                TerminalPreview.Save(TerPreDir, WinTerminal.Mode.JSONFile, WinTerminal.Version.Preview)
                            End If
                        End If
                    Catch ex As Exception
                        MsgBox(My.Application.LanguageHelper.CP_TerminalError & vbCrLf & vbCrLf & ex.Message & vbCrLf & vbCrLf & ex.StackTrace, MsgBoxStyle.Critical + My.Application.MsgboxRt)
                    End Try

                End If
#End Region

#End Region

#Region "Cursors"
                If My.W7 Or My.W8 Then SetCtrlTxt(My.Application.LanguageHelper.CP_SavingCursorsColors, f)

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
                    .SetValue("PrimaryColorNoiseOpacity", Cursor_Arrow_PrimaryColorNoiseOpacity * 100, RegistryValueKind.QWord)
                    .SetValue("SecondaryColor1", Cursor_Arrow_SecondaryColor1.ToArgb, RegistryValueKind.QWord)
                    .SetValue("SecondaryColor2", Cursor_Arrow_SecondaryColor2.ToArgb, RegistryValueKind.QWord)
                    .SetValue("SecondaryColorGradient", If(Cursor_Arrow_SecondaryColorGradient, 1, 0), RegistryValueKind.QWord)
                    .SetValue("SecondaryColorGradientMode", Cursor_Arrow_SecondaryColorGradientMode, RegistryValueKind.String)
                    .SetValue("SecondaryColorNoise", If(Cursor_Arrow_SecondaryColorNoise, 1, 0), RegistryValueKind.QWord)
                    .SetValue("SecondaryColorNoiseOpacity", Cursor_Arrow_SecondaryColorNoiseOpacity * 100, RegistryValueKind.QWord)
                End With

                r = rMain.CreateSubKey("Help")
                With r
                    .SetValue("PrimaryColor1", Cursor_Help_PrimaryColor1.ToArgb, RegistryValueKind.QWord)
                    .SetValue("PrimaryColor2", Cursor_Help_PrimaryColor2.ToArgb, RegistryValueKind.QWord)
                    .SetValue("PrimaryColorGradient", If(Cursor_Help_PrimaryColorGradient, 1, 0), RegistryValueKind.QWord)
                    .SetValue("PrimaryColorGradientMode", Cursor_Help_PrimaryColorGradientMode, RegistryValueKind.String)
                    .SetValue("PrimaryColorNoise", If(Cursor_Help_PrimaryColorNoise, 1, 0), RegistryValueKind.QWord)
                    .SetValue("PrimaryColorNoiseOpacity", Cursor_Help_PrimaryColorNoiseOpacity * 100, RegistryValueKind.QWord)
                    .SetValue("SecondaryColor1", Cursor_Help_SecondaryColor1.ToArgb, RegistryValueKind.QWord)
                    .SetValue("SecondaryColor2", Cursor_Help_SecondaryColor2.ToArgb, RegistryValueKind.QWord)
                    .SetValue("SecondaryColorGradient", If(Cursor_Help_SecondaryColorGradient, 1, 0), RegistryValueKind.QWord)
                    .SetValue("SecondaryColorGradientMode", Cursor_Help_SecondaryColorGradientMode, RegistryValueKind.String)
                    .SetValue("SecondaryColorNoise", If(Cursor_Help_SecondaryColorNoise, 1, 0), RegistryValueKind.QWord)
                    .SetValue("SecondaryColorNoiseOpacity", Cursor_Help_SecondaryColorNoiseOpacity * 100, RegistryValueKind.QWord)
                End With

                r = rMain.CreateSubKey("AppLoading")
                With r
                    .SetValue("PrimaryColor1", Cursor_AppLoading_PrimaryColor1.ToArgb, RegistryValueKind.QWord)
                    .SetValue("PrimaryColor2", Cursor_AppLoading_PrimaryColor2.ToArgb, RegistryValueKind.QWord)
                    .SetValue("PrimaryColorGradient", If(Cursor_AppLoading_PrimaryColorGradient, 1, 0), RegistryValueKind.QWord)
                    .SetValue("PrimaryColorGradientMode", Cursor_AppLoading_PrimaryColorGradientMode, RegistryValueKind.String)
                    .SetValue("PrimaryColorNoise", If(Cursor_AppLoading_PrimaryColorNoise, 1, 0), RegistryValueKind.QWord)
                    .SetValue("PrimaryColorNoiseOpacity", Cursor_AppLoading_PrimaryColorNoiseOpacity * 100, RegistryValueKind.QWord)
                    .SetValue("SecondaryColor1", Cursor_AppLoading_SecondaryColor1.ToArgb, RegistryValueKind.QWord)
                    .SetValue("SecondaryColor2", Cursor_AppLoading_SecondaryColor2.ToArgb, RegistryValueKind.QWord)
                    .SetValue("SecondaryColorGradient", If(Cursor_AppLoading_SecondaryColorGradient, 1, 0), RegistryValueKind.QWord)
                    .SetValue("SecondaryColorGradientMode", Cursor_AppLoading_SecondaryColorGradientMode, RegistryValueKind.String)
                    .SetValue("SecondaryColorNoise", If(Cursor_AppLoading_SecondaryColorNoise, 1, 0), RegistryValueKind.QWord)
                    .SetValue("SecondaryColorNoiseOpacity", Cursor_AppLoading_SecondaryColorNoiseOpacity * 100, RegistryValueKind.QWord)
                    .SetValue("LoadingCircleBack1", Cursor_AppLoading_LoadingCircleBack1.ToArgb, RegistryValueKind.QWord)
                    .SetValue("LoadingCircleBack2", Cursor_AppLoading_LoadingCircleBack2.ToArgb, RegistryValueKind.QWord)
                    .SetValue("LoadingCircleBackGradient", If(Cursor_AppLoading_LoadingCircleBackGradient, 1, 0), RegistryValueKind.QWord)
                    .SetValue("LoadingCircleBackGradientMode", Cursor_AppLoading_LoadingCircleBackGradientMode, RegistryValueKind.String)
                    .SetValue("LoadingCircleBackNoise", If(Cursor_AppLoading_LoadingCircleBackNoise, 1, 0), RegistryValueKind.QWord)
                    .SetValue("LoadingCircleBackNoiseOpacity", Cursor_AppLoading_LoadingCircleBackNoiseOpacity * 100, RegistryValueKind.QWord)
                    .SetValue("LoadingCircleHot1", Cursor_AppLoading_LoadingCircleHot1.ToArgb, RegistryValueKind.QWord)
                    .SetValue("LoadingCircleHot2", Cursor_AppLoading_LoadingCircleHot2.ToArgb, RegistryValueKind.QWord)
                    .SetValue("LoadingCircleHotGradient", If(Cursor_AppLoading_LoadingCircleHotGradient, 1, 0), RegistryValueKind.QWord)
                    .SetValue("LoadingCircleHotGradientMode", Cursor_AppLoading_LoadingCircleHotGradientMode, RegistryValueKind.String)
                    .SetValue("LoadingCircleHotNoise", If(Cursor_AppLoading_LoadingCircleHotNoise, 1, 0), RegistryValueKind.QWord)
                    .SetValue("LoadingCircleHotNoiseOpacity", Cursor_AppLoading_LoadingCircleHotNoiseOpacity * 100, RegistryValueKind.QWord)
                End With

                r = rMain.CreateSubKey("Busy")
                With r
                    .SetValue("LoadingCircleBack1", Cursor_Busy_LoadingCircleBack1.ToArgb, RegistryValueKind.QWord)
                    .SetValue("LoadingCircleBack2", Cursor_Busy_LoadingCircleBack2.ToArgb, RegistryValueKind.QWord)
                    .SetValue("LoadingCircleBackGradient", If(Cursor_Busy_LoadingCircleBackGradient, 1, 0), RegistryValueKind.QWord)
                    .SetValue("LoadingCircleBackGradientMode", Cursor_Busy_LoadingCircleBackGradientMode, RegistryValueKind.String)
                    .SetValue("LoadingCircleBackNoise", If(Cursor_Busy_LoadingCircleBackNoise, 1, 0), RegistryValueKind.QWord)
                    .SetValue("LoadingCircleBackNoiseOpacity", Cursor_Busy_LoadingCircleBackNoiseOpacity * 100, RegistryValueKind.QWord)
                    .SetValue("LoadingCircleHot1", Cursor_Busy_LoadingCircleHot1.ToArgb, RegistryValueKind.QWord)
                    .SetValue("LoadingCircleHot2", Cursor_Busy_LoadingCircleHot2.ToArgb, RegistryValueKind.QWord)
                    .SetValue("LoadingCircleHotGradient", If(Cursor_Busy_LoadingCircleHotGradient, 1, 0), RegistryValueKind.QWord)
                    .SetValue("LoadingCircleHotGradientMode", Cursor_Busy_LoadingCircleHotGradientMode, RegistryValueKind.String)
                    .SetValue("LoadingCircleHotNoise", If(Cursor_Busy_LoadingCircleHotNoise, 1, 0), RegistryValueKind.QWord)
                    .SetValue("LoadingCircleHotNoiseOpacity", Cursor_Busy_LoadingCircleHotNoiseOpacity * 100, RegistryValueKind.QWord)
                End With

                r = rMain.CreateSubKey("Move")
                With r
                    .SetValue("PrimaryColor1", Cursor_Move_PrimaryColor1.ToArgb, RegistryValueKind.QWord)
                    .SetValue("PrimaryColor2", Cursor_Move_PrimaryColor2.ToArgb, RegistryValueKind.QWord)
                    .SetValue("PrimaryColorGradient", If(Cursor_Move_PrimaryColorGradient, 1, 0), RegistryValueKind.QWord)
                    .SetValue("PrimaryColorGradientMode", Cursor_Move_PrimaryColorGradientMode, RegistryValueKind.String)
                    .SetValue("PrimaryColorNoise", If(Cursor_Move_PrimaryColorNoise, 1, 0), RegistryValueKind.QWord)
                    .SetValue("PrimaryColorNoiseOpacity", Cursor_Move_PrimaryColorNoiseOpacity * 100, RegistryValueKind.QWord)
                    .SetValue("SecondaryColor1", Cursor_Move_SecondaryColor1.ToArgb, RegistryValueKind.QWord)
                    .SetValue("SecondaryColor2", Cursor_Move_SecondaryColor2.ToArgb, RegistryValueKind.QWord)
                    .SetValue("SecondaryColorGradient", If(Cursor_Move_SecondaryColorGradient, 1, 0), RegistryValueKind.QWord)
                    .SetValue("SecondaryColorGradientMode", Cursor_Move_SecondaryColorGradientMode, RegistryValueKind.String)
                    .SetValue("SecondaryColorNoise", If(Cursor_Move_SecondaryColorNoise, 1, 0), RegistryValueKind.QWord)
                    .SetValue("SecondaryColorNoiseOpacity", Cursor_Move_SecondaryColorNoiseOpacity * 100, RegistryValueKind.QWord)
                End With

                r = rMain.CreateSubKey("NS")
                With r
                    .SetValue("PrimaryColor1", Cursor_NS_PrimaryColor1.ToArgb, RegistryValueKind.QWord)
                    .SetValue("PrimaryColor2", Cursor_NS_PrimaryColor2.ToArgb, RegistryValueKind.QWord)
                    .SetValue("PrimaryColorGradient", If(Cursor_NS_PrimaryColorGradient, 1, 0), RegistryValueKind.QWord)
                    .SetValue("PrimaryColorGradientMode", Cursor_NS_PrimaryColorGradientMode, RegistryValueKind.String)
                    .SetValue("PrimaryColorNoise", If(Cursor_NS_PrimaryColorNoise, 1, 0), RegistryValueKind.QWord)
                    .SetValue("PrimaryColorNoiseOpacity", Cursor_NS_PrimaryColorNoiseOpacity * 100, RegistryValueKind.QWord)
                    .SetValue("SecondaryColor1", Cursor_NS_SecondaryColor1.ToArgb, RegistryValueKind.QWord)
                    .SetValue("SecondaryColor2", Cursor_NS_SecondaryColor2.ToArgb, RegistryValueKind.QWord)
                    .SetValue("SecondaryColorGradient", If(Cursor_NS_SecondaryColorGradient, 1, 0), RegistryValueKind.QWord)
                    .SetValue("SecondaryColorGradientMode", Cursor_NS_SecondaryColorGradientMode, RegistryValueKind.String)
                    .SetValue("SecondaryColorNoise", If(Cursor_NS_SecondaryColorNoise, 1, 0), RegistryValueKind.QWord)
                    .SetValue("SecondaryColorNoiseOpacity", Cursor_NS_SecondaryColorNoiseOpacity * 100, RegistryValueKind.QWord)
                End With

                r = rMain.CreateSubKey("EW")
                With r
                    .SetValue("PrimaryColor1", Cursor_EW_PrimaryColor1.ToArgb, RegistryValueKind.QWord)
                    .SetValue("PrimaryColor2", Cursor_EW_PrimaryColor2.ToArgb, RegistryValueKind.QWord)
                    .SetValue("PrimaryColorGradient", If(Cursor_EW_PrimaryColorGradient, 1, 0), RegistryValueKind.QWord)
                    .SetValue("PrimaryColorGradientMode", Cursor_EW_PrimaryColorGradientMode, RegistryValueKind.String)
                    .SetValue("PrimaryColorNoise", If(Cursor_EW_PrimaryColorNoise, 1, 0), RegistryValueKind.QWord)
                    .SetValue("PrimaryColorNoiseOpacity", Cursor_EW_PrimaryColorNoiseOpacity * 100, RegistryValueKind.QWord)
                    .SetValue("SecondaryColor1", Cursor_EW_SecondaryColor1.ToArgb, RegistryValueKind.QWord)
                    .SetValue("SecondaryColor2", Cursor_EW_SecondaryColor2.ToArgb, RegistryValueKind.QWord)
                    .SetValue("SecondaryColorGradient", If(Cursor_EW_SecondaryColorGradient, 1, 0), RegistryValueKind.QWord)
                    .SetValue("SecondaryColorGradientMode", Cursor_EW_SecondaryColorGradientMode, RegistryValueKind.String)
                    .SetValue("SecondaryColorNoise", If(Cursor_EW_SecondaryColorNoise, 1, 0), RegistryValueKind.QWord)
                    .SetValue("SecondaryColorNoiseOpacity", Cursor_EW_SecondaryColorNoiseOpacity * 100, RegistryValueKind.QWord)
                End With

                r = rMain.CreateSubKey("NESW")
                With r
                    .SetValue("PrimaryColor1", Cursor_NESW_PrimaryColor1.ToArgb, RegistryValueKind.QWord)
                    .SetValue("PrimaryColor2", Cursor_NESW_PrimaryColor2.ToArgb, RegistryValueKind.QWord)
                    .SetValue("PrimaryColorGradient", If(Cursor_NESW_PrimaryColorGradient, 1, 0), RegistryValueKind.QWord)
                    .SetValue("PrimaryColorGradientMode", Cursor_NESW_PrimaryColorGradientMode, RegistryValueKind.String)
                    .SetValue("PrimaryColorNoise", If(Cursor_NESW_PrimaryColorNoise, 1, 0), RegistryValueKind.QWord)
                    .SetValue("PrimaryColorNoiseOpacity", Cursor_NESW_PrimaryColorNoiseOpacity * 100, RegistryValueKind.QWord)
                    .SetValue("SecondaryColor1", Cursor_NESW_SecondaryColor1.ToArgb, RegistryValueKind.QWord)
                    .SetValue("SecondaryColor2", Cursor_NESW_SecondaryColor2.ToArgb, RegistryValueKind.QWord)
                    .SetValue("SecondaryColorGradient", If(Cursor_NESW_SecondaryColorGradient, 1, 0), RegistryValueKind.QWord)
                    .SetValue("SecondaryColorGradientMode", Cursor_NESW_SecondaryColorGradientMode, RegistryValueKind.String)
                    .SetValue("SecondaryColorNoise", If(Cursor_NESW_SecondaryColorNoise, 1, 0), RegistryValueKind.QWord)
                    .SetValue("SecondaryColorNoiseOpacity", Cursor_NESW_SecondaryColorNoiseOpacity * 100, RegistryValueKind.QWord)
                End With

                r = rMain.CreateSubKey("NWSE")
                With r
                    .SetValue("PrimaryColor1", Cursor_NWSE_PrimaryColor1.ToArgb, RegistryValueKind.QWord)
                    .SetValue("PrimaryColor2", Cursor_NWSE_PrimaryColor2.ToArgb, RegistryValueKind.QWord)
                    .SetValue("PrimaryColorGradient", If(Cursor_NWSE_PrimaryColorGradient, 1, 0), RegistryValueKind.QWord)
                    .SetValue("PrimaryColorGradientMode", Cursor_NWSE_PrimaryColorGradientMode, RegistryValueKind.String)
                    .SetValue("PrimaryColorNoise", If(Cursor_NWSE_PrimaryColorNoise, 1, 0), RegistryValueKind.QWord)
                    .SetValue("PrimaryColorNoiseOpacity", Cursor_NWSE_PrimaryColorNoiseOpacity * 100, RegistryValueKind.QWord)
                    .SetValue("SecondaryColor1", Cursor_NWSE_SecondaryColor1.ToArgb, RegistryValueKind.QWord)
                    .SetValue("SecondaryColor2", Cursor_NWSE_SecondaryColor2.ToArgb, RegistryValueKind.QWord)
                    .SetValue("SecondaryColorGradient", If(Cursor_NWSE_SecondaryColorGradient, 1, 0), RegistryValueKind.QWord)
                    .SetValue("SecondaryColorGradientMode", Cursor_NWSE_SecondaryColorGradientMode, RegistryValueKind.String)
                    .SetValue("SecondaryColorNoise", If(Cursor_NWSE_SecondaryColorNoise, 1, 0), RegistryValueKind.QWord)
                    .SetValue("SecondaryColorNoiseOpacity", Cursor_NWSE_SecondaryColorNoiseOpacity * 100, RegistryValueKind.QWord)
                End With

                r = rMain.CreateSubKey("Up")
                With r
                    .SetValue("PrimaryColor1", Cursor_Up_PrimaryColor1.ToArgb, RegistryValueKind.QWord)
                    .SetValue("PrimaryColor2", Cursor_Up_PrimaryColor2.ToArgb, RegistryValueKind.QWord)
                    .SetValue("PrimaryColorGradient", If(Cursor_Up_PrimaryColorGradient, 1, 0), RegistryValueKind.QWord)
                    .SetValue("PrimaryColorGradientMode", Cursor_Up_PrimaryColorGradientMode, RegistryValueKind.String)
                    .SetValue("PrimaryColorNoise", If(Cursor_Up_PrimaryColorNoise, 1, 0), RegistryValueKind.QWord)
                    .SetValue("PrimaryColorNoiseOpacity", Cursor_Up_PrimaryColorNoiseOpacity * 100, RegistryValueKind.QWord)
                    .SetValue("SecondaryColor1", Cursor_Up_SecondaryColor1.ToArgb, RegistryValueKind.QWord)
                    .SetValue("SecondaryColor2", Cursor_Up_SecondaryColor2.ToArgb, RegistryValueKind.QWord)
                    .SetValue("SecondaryColorGradient", If(Cursor_Up_SecondaryColorGradient, 1, 0), RegistryValueKind.QWord)
                    .SetValue("SecondaryColorGradientMode", Cursor_Up_SecondaryColorGradientMode, RegistryValueKind.String)
                    .SetValue("SecondaryColorNoise", If(Cursor_Up_SecondaryColorNoise, 1, 0), RegistryValueKind.QWord)
                    .SetValue("SecondaryColorNoiseOpacity", Cursor_Up_SecondaryColorNoiseOpacity * 100, RegistryValueKind.QWord)
                End With

                r = rMain.CreateSubKey("Pen")
                With r
                    .SetValue("PrimaryColor1", Cursor_Pen_PrimaryColor1.ToArgb, RegistryValueKind.QWord)
                    .SetValue("PrimaryColor2", Cursor_Pen_PrimaryColor2.ToArgb, RegistryValueKind.QWord)
                    .SetValue("PrimaryColorGradient", If(Cursor_Pen_PrimaryColorGradient, 1, 0), RegistryValueKind.QWord)
                    .SetValue("PrimaryColorGradientMode", Cursor_Pen_PrimaryColorGradientMode, RegistryValueKind.String)
                    .SetValue("PrimaryColorNoise", If(Cursor_Pen_PrimaryColorNoise, 1, 0), RegistryValueKind.QWord)
                    .SetValue("PrimaryColorNoiseOpacity", Cursor_Pen_PrimaryColorNoiseOpacity * 100, RegistryValueKind.QWord)
                    .SetValue("SecondaryColor1", Cursor_Pen_SecondaryColor1.ToArgb, RegistryValueKind.QWord)
                    .SetValue("SecondaryColor2", Cursor_Pen_SecondaryColor2.ToArgb, RegistryValueKind.QWord)
                    .SetValue("SecondaryColorGradient", If(Cursor_Pen_SecondaryColorGradient, 1, 0), RegistryValueKind.QWord)
                    .SetValue("SecondaryColorGradientMode", Cursor_Pen_SecondaryColorGradientMode, RegistryValueKind.String)
                    .SetValue("SecondaryColorNoise", If(Cursor_Pen_SecondaryColorNoise, 1, 0), RegistryValueKind.QWord)
                    .SetValue("SecondaryColorNoiseOpacity", Cursor_Pen_SecondaryColorNoiseOpacity * 100, RegistryValueKind.QWord)
                End With

                r = rMain.CreateSubKey("None")
                With r
                    .SetValue("PrimaryColor1", Cursor_None_PrimaryColor1.ToArgb, RegistryValueKind.QWord)
                    .SetValue("PrimaryColor2", Cursor_None_PrimaryColor2.ToArgb, RegistryValueKind.QWord)
                    .SetValue("PrimaryColorGradient", If(Cursor_None_PrimaryColorGradient, 1, 0), RegistryValueKind.QWord)
                    .SetValue("PrimaryColorGradientMode", Cursor_None_PrimaryColorGradientMode, RegistryValueKind.String)
                    .SetValue("PrimaryColorNoise", If(Cursor_None_PrimaryColorNoise, 1, 0), RegistryValueKind.QWord)
                    .SetValue("PrimaryColorNoiseOpacity", Cursor_None_PrimaryColorNoiseOpacity * 100, RegistryValueKind.QWord)
                    .SetValue("SecondaryColor1", Cursor_None_SecondaryColor1.ToArgb, RegistryValueKind.QWord)
                    .SetValue("SecondaryColor2", Cursor_None_SecondaryColor2.ToArgb, RegistryValueKind.QWord)
                    .SetValue("SecondaryColorGradient", If(Cursor_None_SecondaryColorGradient, 1, 0), RegistryValueKind.QWord)
                    .SetValue("SecondaryColorGradientMode", Cursor_None_SecondaryColorGradientMode, RegistryValueKind.String)
                    .SetValue("SecondaryColorNoise", If(Cursor_None_SecondaryColorNoise, 1, 0), RegistryValueKind.QWord)
                    .SetValue("SecondaryColorNoiseOpacity", Cursor_None_SecondaryColorNoiseOpacity * 100, RegistryValueKind.QWord)
                End With

                r = rMain.CreateSubKey("Link")
                With r
                    .SetValue("PrimaryColor1", Cursor_Link_PrimaryColor1.ToArgb, RegistryValueKind.QWord)
                    .SetValue("PrimaryColor2", Cursor_Link_PrimaryColor2.ToArgb, RegistryValueKind.QWord)
                    .SetValue("PrimaryColorGradient", If(Cursor_Link_PrimaryColorGradient, 1, 0), RegistryValueKind.QWord)
                    .SetValue("PrimaryColorGradientMode", Cursor_Link_PrimaryColorGradientMode, RegistryValueKind.String)
                    .SetValue("PrimaryColorNoise", If(Cursor_Link_PrimaryColorNoise, 1, 0), RegistryValueKind.QWord)
                    .SetValue("PrimaryColorNoiseOpacity", Cursor_Link_PrimaryColorNoiseOpacity * 100, RegistryValueKind.QWord)
                    .SetValue("SecondaryColor1", Cursor_Link_SecondaryColor1.ToArgb, RegistryValueKind.QWord)
                    .SetValue("SecondaryColor2", Cursor_Link_SecondaryColor2.ToArgb, RegistryValueKind.QWord)
                    .SetValue("SecondaryColorGradient", If(Cursor_Link_SecondaryColorGradient, 1, 0), RegistryValueKind.QWord)
                    .SetValue("SecondaryColorGradientMode", Cursor_Link_SecondaryColorGradientMode, RegistryValueKind.String)
                    .SetValue("SecondaryColorNoise", If(Cursor_Link_SecondaryColorNoise, 1, 0), RegistryValueKind.QWord)
                    .SetValue("SecondaryColorNoiseOpacity", Cursor_Link_SecondaryColorNoiseOpacity * 100, RegistryValueKind.QWord)
                End With

                r = rMain.CreateSubKey("Pin")
                With r
                    .SetValue("PrimaryColor1", Cursor_Pin_PrimaryColor1.ToArgb, RegistryValueKind.QWord)
                    .SetValue("PrimaryColor2", Cursor_Pin_PrimaryColor2.ToArgb, RegistryValueKind.QWord)
                    .SetValue("PrimaryColorGradient", If(Cursor_Pin_PrimaryColorGradient, 1, 0), RegistryValueKind.QWord)
                    .SetValue("PrimaryColorGradientMode", Cursor_Pin_PrimaryColorGradientMode, RegistryValueKind.String)
                    .SetValue("PrimaryColorNoise", If(Cursor_Pin_PrimaryColorNoise, 1, 0), RegistryValueKind.QWord)
                    .SetValue("PrimaryColorNoiseOpacity", Cursor_Pin_PrimaryColorNoiseOpacity * 100, RegistryValueKind.QWord)
                    .SetValue("SecondaryColor1", Cursor_Pin_SecondaryColor1.ToArgb, RegistryValueKind.QWord)
                    .SetValue("SecondaryColor2", Cursor_Pin_SecondaryColor2.ToArgb, RegistryValueKind.QWord)
                    .SetValue("SecondaryColorGradient", If(Cursor_Pin_SecondaryColorGradient, 1, 0), RegistryValueKind.QWord)
                    .SetValue("SecondaryColorGradientMode", Cursor_Pin_SecondaryColorGradientMode, RegistryValueKind.String)
                    .SetValue("SecondaryColorNoise", If(Cursor_Pin_SecondaryColorNoise, 1, 0), RegistryValueKind.QWord)
                    .SetValue("SecondaryColorNoiseOpacity", Cursor_Pin_SecondaryColorNoiseOpacity * 100, RegistryValueKind.QWord)
                End With

                r = rMain.CreateSubKey("Person")
                With r
                    .SetValue("PrimaryColor1", Cursor_Person_PrimaryColor1.ToArgb, RegistryValueKind.QWord)
                    .SetValue("PrimaryColor2", Cursor_Person_PrimaryColor2.ToArgb, RegistryValueKind.QWord)
                    .SetValue("PrimaryColorGradient", If(Cursor_Person_PrimaryColorGradient, 1, 0), RegistryValueKind.QWord)
                    .SetValue("PrimaryColorGradientMode", Cursor_Person_PrimaryColorGradientMode, RegistryValueKind.String)
                    .SetValue("PrimaryColorNoise", If(Cursor_Person_PrimaryColorNoise, 1, 0), RegistryValueKind.QWord)
                    .SetValue("PrimaryColorNoiseOpacity", Cursor_Person_PrimaryColorNoiseOpacity * 100, RegistryValueKind.QWord)
                    .SetValue("SecondaryColor1", Cursor_Person_SecondaryColor1.ToArgb, RegistryValueKind.QWord)
                    .SetValue("SecondaryColor2", Cursor_Person_SecondaryColor2.ToArgb, RegistryValueKind.QWord)
                    .SetValue("SecondaryColorGradient", If(Cursor_Person_SecondaryColorGradient, 1, 0), RegistryValueKind.QWord)
                    .SetValue("SecondaryColorGradientMode", Cursor_Person_SecondaryColorGradientMode, RegistryValueKind.String)
                    .SetValue("SecondaryColorNoise", If(Cursor_Person_SecondaryColorNoise, 1, 0), RegistryValueKind.QWord)
                    .SetValue("SecondaryColorNoiseOpacity", Cursor_Person_SecondaryColorNoiseOpacity * 100, RegistryValueKind.QWord)
                End With

                r = rMain.CreateSubKey("IBeam")
                With r
                    .SetValue("PrimaryColor1", Cursor_IBeam_PrimaryColor1.ToArgb, RegistryValueKind.QWord)
                    .SetValue("PrimaryColor2", Cursor_IBeam_PrimaryColor2.ToArgb, RegistryValueKind.QWord)
                    .SetValue("PrimaryColorGradient", If(Cursor_IBeam_PrimaryColorGradient, 1, 0), RegistryValueKind.QWord)
                    .SetValue("PrimaryColorGradientMode", Cursor_IBeam_PrimaryColorGradientMode, RegistryValueKind.String)
                    .SetValue("PrimaryColorNoise", If(Cursor_IBeam_PrimaryColorNoise, 1, 0), RegistryValueKind.QWord)
                    .SetValue("PrimaryColorNoiseOpacity", Cursor_IBeam_PrimaryColorNoiseOpacity * 100, RegistryValueKind.QWord)
                    .SetValue("SecondaryColor1", Cursor_IBeam_SecondaryColor1.ToArgb, RegistryValueKind.QWord)
                    .SetValue("SecondaryColor2", Cursor_IBeam_SecondaryColor2.ToArgb, RegistryValueKind.QWord)
                    .SetValue("SecondaryColorGradient", If(Cursor_IBeam_SecondaryColorGradient, 1, 0), RegistryValueKind.QWord)
                    .SetValue("SecondaryColorGradientMode", Cursor_IBeam_SecondaryColorGradientMode, RegistryValueKind.String)
                    .SetValue("SecondaryColorNoise", If(Cursor_IBeam_SecondaryColorNoise, 1, 0), RegistryValueKind.QWord)
                    .SetValue("SecondaryColorNoiseOpacity", Cursor_IBeam_SecondaryColorNoiseOpacity * 100, RegistryValueKind.QWord)
                End With

                r = rMain.CreateSubKey("Cross")
                With r
                    .SetValue("PrimaryColor1", Cursor_Cross_PrimaryColor1.ToArgb, RegistryValueKind.QWord)
                    .SetValue("PrimaryColor2", Cursor_Cross_PrimaryColor2.ToArgb, RegistryValueKind.QWord)
                    .SetValue("PrimaryColorGradient", If(Cursor_Cross_PrimaryColorGradient, 1, 0), RegistryValueKind.QWord)
                    .SetValue("PrimaryColorGradientMode", Cursor_Cross_PrimaryColorGradientMode, RegistryValueKind.String)
                    .SetValue("PrimaryColorNoise", If(Cursor_Cross_PrimaryColorNoise, 1, 0), RegistryValueKind.QWord)
                    .SetValue("PrimaryColorNoiseOpacity", Cursor_Cross_PrimaryColorNoiseOpacity * 100, RegistryValueKind.QWord)
                    .SetValue("SecondaryColor1", Cursor_Cross_SecondaryColor1.ToArgb, RegistryValueKind.QWord)
                    .SetValue("SecondaryColor2", Cursor_Cross_SecondaryColor2.ToArgb, RegistryValueKind.QWord)
                    .SetValue("SecondaryColorGradient", If(Cursor_Cross_SecondaryColorGradient, 1, 0), RegistryValueKind.QWord)
                    .SetValue("SecondaryColorGradientMode", Cursor_Cross_SecondaryColorGradientMode, RegistryValueKind.String)
                    .SetValue("SecondaryColorNoise", If(Cursor_Cross_SecondaryColorNoise, 1, 0), RegistryValueKind.QWord)
                    .SetValue("SecondaryColorNoiseOpacity", Cursor_Cross_SecondaryColorNoiseOpacity * 100, RegistryValueKind.QWord)
                End With

                r.Close()
                rMain.Close()

                If Cursor_Enabled Then
                    If My.W7 Or My.W8 Then SetCtrlTxt(My.Application.LanguageHelper.CP_RenderingCursors, f)
                    ExportCursors(Me)
                    If My.Application._Settings.AutoApplyCursors Then
                        If My.W7 Or My.W8 Then SetCtrlTxt(My.Application.LanguageHelper.CP_ApplyingCursors, f)
                        ApplyCursorsToReg()
                    End If
                End If
#End Region

                If ShowProgress Then
                    If My.W7 Or My.W8 Then f.BeginInvoke(New Action(Sub() f.Close()))
                End If

#End Region

            Case SavingMode.File
#Region "File"
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
                tx.Add("<ModernWindows>")
                tx.Add("*WinMode_Light= " & WinMode_Light)
                tx.Add("*AppMode_Light= " & AppMode_Light)
                tx.Add("*Transparency= " & Transparency)
                tx.Add("*AccentColorOnTitlebarAndBorders= " & ApplyAccentonTitlebars)
                tx.Add("*AccentColorOnStartTaskbarAndActionCenter= " & ApplyAccentonTaskbar)
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
                tx.Add("*Undefined= " & UWP_Undefined.ToArgb)
                tx.Add("</ModernWindows>" & vbCrLf)
#End Region

#Region "Aero"
                tx.Add("<Aero>")
                tx.Add("*Aero_ColorizationColor= " & Aero_ColorizationColor.ToArgb)
                tx.Add("*Aero_ColorizationAfterglow= " & Aero_ColorizationAfterglow.ToArgb)
                tx.Add("*Aero_ColorizationColorBalance= " & Aero_ColorizationColorBalance)
                tx.Add("*Aero_ColorizationAfterglowBalance= " & Aero_ColorizationAfterglowBalance)
                tx.Add("*Aero_ColorizationBlurBalance= " & Aero_ColorizationBlurBalance)
                tx.Add("*Aero_ColorizationGlassReflectionIntensity= " & Aero_ColorizationGlassReflectionIntensity)
                tx.Add("*Aero_EnableAeroPeek= " & Aero_EnableAeroPeek)
                tx.Add("*Aero_AlwaysHibernateThumbnails= " & Aero_AlwaysHibernateThumbnails)
                tx.Add("*Aero_Theme= " & Aero_Theme)
                tx.Add("</Aero>" & vbCrLf)
#End Region

#Region "Metro"
                tx.Add("<Metro>")
                tx.Add("*Metro_PersonalColors_Background= " & Metro_PersonalColors_Background.ToArgb)
                tx.Add("*Metro_PersonalColors_Accent= " & Metro_PersonalColors_Accent.ToArgb)
                tx.Add("*Metro_StartColor= " & Metro_StartColor.ToArgb)
                tx.Add("*Metro_AccentColor= " & Metro_AccentColor.ToArgb)
                tx.Add("*Metro_Start= " & Metro_Start)
                tx.Add("*Metro_Theme= " & Metro_Theme)
                tx.Add("*Metro_LogonUI= " & Metro_LogonUI)
                tx.Add("*Metro_NoLockScreen= " & Metro_NoLockScreen)
                tx.Add("*Metro_LockScreenType= " & Metro_LockScreenType)
                tx.Add("*Metro_LockScreenSystemID= " & Metro_LockScreenSystemID)
                tx.Add("</Metro>" & vbCrLf)
#End Region

#Region "LogonUI"
                tx.Add("<LogonUI_10_11>")
                tx.Add("*LogonUI_DisableAcrylicBackgroundOnLogon= " & LogonUI_DisableAcrylicBackgroundOnLogon)
                tx.Add("*LogonUI_DisableLogonBackgroundImage= " & LogonUI_DisableLogonBackgroundImage)
                tx.Add("*LogonUI_NoLockScreen= " & LogonUI_NoLockScreen)
                tx.Add("</LogonUI_10_11>" & vbCrLf)
#End Region

#Region "LogonUI_7_8"
                tx.Add("<LogonUI_7_8>")
                tx.Add("*LogonUI7_Enabled= " & LogonUI7_Enabled)
                tx.Add("*LogonUI7_Mode= " & LogonUI7_Mode)
                tx.Add("*LogonUI7_ImagePath= " & LogonUI7_ImagePath)
                tx.Add("*LogonUI7_Color= " & LogonUI7_Color.ToArgb)
                tx.Add("*LogonUI7_Effect_Blur= " & LogonUI7_Effect_Blur)
                tx.Add("*LogonUI7_Effect_Blur_Intensity= " & LogonUI7_Effect_Blur_Intensity)
                tx.Add("*LogonUI7_Effect_Grayscale= " & LogonUI7_Effect_Grayscale)
                tx.Add("*LogonUI7_Effect_Noise= " & LogonUI7_Effect_Noise)
                tx.Add("*LogonUI7_Effect_Noise_Mode= " & LogonUI7_Effect_Noise_Mode)
                tx.Add("*LogonUI7_Effect_Noise_Intensity= " & LogonUI7_Effect_Noise_Intensity)
                tx.Add("</LogonUI_7_8>" & vbCrLf)
#End Region

#Region "Win32UI"
                tx.Add("<Win32UI>")
                tx.Add("*Win32UI_EnableTheming= " & Win32UI_EnableTheming)
                tx.Add("*Win32UI_EnableGradient= " & Win32UI_EnableGradient)
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

#Region "Terminals"
                tx.Add(vbCrLf & "<Terminals>")
                tx.Add("*Terminal_CMD_Enabled= " & Terminal_CMD_Enabled)
                tx.Add("*Terminal_PS_32_Enabled= " & Terminal_PS_32_Enabled)
                tx.Add("*Terminal_PS_64_Enabled= " & Terminal_PS_64_Enabled)
                tx.Add("*Terminal_Stable_Enabled= " & Terminal_Stable_Enabled)
                tx.Add("*Terminal_Preview_Enabled= " & Terminal_Preview_Enabled)

                tx.Add(vbCrLf & "<CMD>")
                tx.Add("*CMD_ColorTable00= " & CMD_ColorTable00.ToArgb)
                tx.Add("*CMD_ColorTable01= " & CMD_ColorTable01.ToArgb)
                tx.Add("*CMD_ColorTable02= " & CMD_ColorTable02.ToArgb)
                tx.Add("*CMD_ColorTable03= " & CMD_ColorTable03.ToArgb)
                tx.Add("*CMD_ColorTable04= " & CMD_ColorTable04.ToArgb)
                tx.Add("*CMD_ColorTable05= " & CMD_ColorTable05.ToArgb)
                tx.Add("*CMD_ColorTable06= " & CMD_ColorTable06.ToArgb)
                tx.Add("*CMD_ColorTable07= " & CMD_ColorTable07.ToArgb)
                tx.Add("*CMD_ColorTable08= " & CMD_ColorTable08.ToArgb)
                tx.Add("*CMD_ColorTable09= " & CMD_ColorTable09.ToArgb)
                tx.Add("*CMD_ColorTable10= " & CMD_ColorTable10.ToArgb)
                tx.Add("*CMD_ColorTable11= " & CMD_ColorTable11.ToArgb)
                tx.Add("*CMD_ColorTable12= " & CMD_ColorTable12.ToArgb)
                tx.Add("*CMD_ColorTable13= " & CMD_ColorTable13.ToArgb)
                tx.Add("*CMD_ColorTable14= " & CMD_ColorTable14.ToArgb)
                tx.Add("*CMD_ColorTable15= " & CMD_ColorTable15.ToArgb)
                tx.Add("*CMD_PopupForeground= " & CMD_PopupForeground)
                tx.Add("*CMD_PopupBackground= " & CMD_PopupBackground)
                tx.Add("*CMD_ScreenColorsForeground= " & CMD_ScreenColorsForeground)
                tx.Add("*CMD_ScreenColorsBackground= " & CMD_ScreenColorsBackground)
                tx.Add("*CMD_CursorSize= " & CMD_CursorSize)
                tx.Add("*CMD_FaceName= " & CMD_FaceName)
                tx.Add("*CMD_FontRaster= " & CMD_FontRaster)
                tx.Add("*CMD_FontSize= " & CMD_FontSize)
                tx.Add("*CMD_FontWeight= " & CMD_FontWeight)
                tx.Add("*CMD_1909_CursorType= " & CMD_1909_CursorType)
                tx.Add("*CMD_1909_CursorColor= " & CMD_1909_CursorColor.ToArgb)
                tx.Add("*CMD_1909_ForceV2= " & CMD_1909_ForceV2)
                tx.Add("*CMD_1909_LineSelection= " & CMD_1909_LineSelection)
                tx.Add("*CMD_1909_TerminalScrolling= " & CMD_1909_TerminalScrolling)
                tx.Add("*CMD_1909_WindowAlpha= " & CMD_1909_WindowAlpha)
                tx.Add("</CMD>" & vbCrLf)

                tx.Add("<PowerShellx86>")
                tx.Add("*PS_32_ColorTable00= " & PS_32_ColorTable00.ToArgb)
                tx.Add("*PS_32_ColorTable01= " & PS_32_ColorTable01.ToArgb)
                tx.Add("*PS_32_ColorTable02= " & PS_32_ColorTable02.ToArgb)
                tx.Add("*PS_32_ColorTable03= " & PS_32_ColorTable03.ToArgb)
                tx.Add("*PS_32_ColorTable04= " & PS_32_ColorTable04.ToArgb)
                tx.Add("*PS_32_ColorTable05= " & PS_32_ColorTable05.ToArgb)
                tx.Add("*PS_32_ColorTable06= " & PS_32_ColorTable06.ToArgb)
                tx.Add("*PS_32_ColorTable07= " & PS_32_ColorTable07.ToArgb)
                tx.Add("*PS_32_ColorTable08= " & PS_32_ColorTable08.ToArgb)
                tx.Add("*PS_32_ColorTable09= " & PS_32_ColorTable09.ToArgb)
                tx.Add("*PS_32_ColorTable10= " & PS_32_ColorTable10.ToArgb)
                tx.Add("*PS_32_ColorTable11= " & PS_32_ColorTable11.ToArgb)
                tx.Add("*PS_32_ColorTable12= " & PS_32_ColorTable12.ToArgb)
                tx.Add("*PS_32_ColorTable13= " & PS_32_ColorTable13.ToArgb)
                tx.Add("*PS_32_ColorTable14= " & PS_32_ColorTable14.ToArgb)
                tx.Add("*PS_32_ColorTable15= " & PS_32_ColorTable15.ToArgb)
                tx.Add("*PS_32_PopupForeground= " & PS_32_PopupForeground)
                tx.Add("*PS_32_PopupBackground= " & PS_32_PopupBackground)
                tx.Add("*PS_32_ScreenColorsForeground= " & PS_32_ScreenColorsForeground)
                tx.Add("*PS_32_ScreenColorsBackground= " & PS_32_ScreenColorsBackground)
                tx.Add("*PS_32_CursorSize= " & PS_32_CursorSize)
                tx.Add("*PS_32_FaceName= " & PS_32_FaceName)
                tx.Add("*PS_32_FontRaster= " & PS_32_FontRaster)
                tx.Add("*PS_32_FontSize= " & PS_32_FontSize)
                tx.Add("*PS_32_FontWeight= " & PS_32_FontWeight)
                tx.Add("*PS_32_1909_CursorType= " & PS_32_1909_CursorType)
                tx.Add("*PS_32_1909_CursorColor= " & PS_32_1909_CursorColor.ToArgb)
                tx.Add("*PS_32_1909_ForceV2= " & PS_32_1909_ForceV2)
                tx.Add("*PS_32_1909_LineSelection= " & PS_32_1909_LineSelection)
                tx.Add("*PS_32_1909_TerminalScrolling= " & PS_32_1909_TerminalScrolling)
                tx.Add("*PS_32_1909_WindowAlpha= " & PS_32_1909_WindowAlpha)
                tx.Add("</PowerShellx86>" & vbCrLf)

                tx.Add("<PowerShellx64>")
                tx.Add("*PS_64_ColorTable00= " & PS_64_ColorTable00.ToArgb)
                tx.Add("*PS_64_ColorTable01= " & PS_64_ColorTable01.ToArgb)
                tx.Add("*PS_64_ColorTable02= " & PS_64_ColorTable02.ToArgb)
                tx.Add("*PS_64_ColorTable03= " & PS_64_ColorTable03.ToArgb)
                tx.Add("*PS_64_ColorTable04= " & PS_64_ColorTable04.ToArgb)
                tx.Add("*PS_64_ColorTable05= " & PS_64_ColorTable05.ToArgb)
                tx.Add("*PS_64_ColorTable06= " & PS_64_ColorTable06.ToArgb)
                tx.Add("*PS_64_ColorTable07= " & PS_64_ColorTable07.ToArgb)
                tx.Add("*PS_64_ColorTable08= " & PS_64_ColorTable08.ToArgb)
                tx.Add("*PS_64_ColorTable09= " & PS_64_ColorTable09.ToArgb)
                tx.Add("*PS_64_ColorTable10= " & PS_64_ColorTable10.ToArgb)
                tx.Add("*PS_64_ColorTable11= " & PS_64_ColorTable11.ToArgb)
                tx.Add("*PS_64_ColorTable12= " & PS_64_ColorTable12.ToArgb)
                tx.Add("*PS_64_ColorTable13= " & PS_64_ColorTable13.ToArgb)
                tx.Add("*PS_64_ColorTable14= " & PS_64_ColorTable14.ToArgb)
                tx.Add("*PS_64_ColorTable15= " & PS_64_ColorTable15.ToArgb)
                tx.Add("*PS_64_PopupForeground= " & PS_64_PopupForeground)
                tx.Add("*PS_64_PopupBackground= " & PS_64_PopupBackground)
                tx.Add("*PS_64_ScreenColorsForeground= " & PS_64_ScreenColorsForeground)
                tx.Add("*PS_64_ScreenColorsBackground= " & PS_64_ScreenColorsBackground)
                tx.Add("*PS_64_CursorSize= " & PS_64_CursorSize)
                tx.Add("*PS_64_FaceName= " & PS_64_FaceName)
                tx.Add("*PS_64_FontRaster= " & PS_64_FontRaster)
                tx.Add("*PS_64_FontSize= " & PS_64_FontSize)
                tx.Add("*PS_64_FontWeight= " & PS_64_FontWeight)
                tx.Add("*PS_64_1909_CursorType= " & PS_64_1909_CursorType)
                tx.Add("*PS_64_1909_CursorColor= " & PS_64_1909_CursorColor.ToArgb)
                tx.Add("*PS_64_1909_ForceV2= " & PS_64_1909_ForceV2)
                tx.Add("*PS_64_1909_LineSelection= " & PS_64_1909_LineSelection)
                tx.Add("*PS_64_1909_TerminalScrolling= " & PS_64_1909_TerminalScrolling)
                tx.Add("*PS_64_1909_WindowAlpha= " & PS_64_1909_WindowAlpha)
                tx.Add("</PowerShellx64>" & vbCrLf)

                tx.Add("<WindowsTerminal_Stable>")
                tx.Add(Terminal.Save("", WinTerminal.Mode.WinPaletterFile))
                tx.Add("</WindowsTerminal_Stable>" & vbCrLf)

                tx.Add("<WindowsTerminal_Preview>")
                tx.Add(TerminalPreview.Save("", WinTerminal.Mode.WinPaletterFile, WinTerminal.Version.Preview))
                tx.Add("</WindowsTerminal_Preview>" & vbCrLf)

                tx.Add("</Terminals>")
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

#Region "IBeam"
                tx.Add("*Cursor_IBeam_PrimaryColor1= " & Cursor_IBeam_PrimaryColor1.ToArgb)
                tx.Add("*Cursor_IBeam_PrimaryColor2= " & Cursor_IBeam_PrimaryColor2.ToArgb)
                tx.Add("*Cursor_IBeam_PrimaryColorGradient= " & Cursor_IBeam_PrimaryColorGradient)
                tx.Add("*Cursor_IBeam_PrimaryColorGradientMode= " & Cursor_IBeam_PrimaryColorGradientMode)
                tx.Add("*Cursor_IBeam_PrimaryColorNoise= " & Cursor_IBeam_PrimaryColorNoise)
                tx.Add("*Cursor_IBeam_PrimaryColorNoiseOpacity= " & Cursor_IBeam_PrimaryColorNoiseOpacity)
                tx.Add("*Cursor_IBeam_SecondaryColor1= " & Cursor_IBeam_SecondaryColor1.ToArgb)
                tx.Add("*Cursor_IBeam_SecondaryColor2= " & Cursor_IBeam_SecondaryColor2.ToArgb)
                tx.Add("*Cursor_IBeam_SecondaryColorGradient= " & Cursor_IBeam_SecondaryColorGradient)
                tx.Add("*Cursor_IBeam_SecondaryColorGradientMode= " & Cursor_IBeam_SecondaryColorGradientMode)
                tx.Add("*Cursor_IBeam_SecondaryColorNoise= " & Cursor_IBeam_SecondaryColorNoise)
                tx.Add("*Cursor_IBeam_SecondaryColorNoiseOpacity= " & Cursor_IBeam_SecondaryColorNoiseOpacity)
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
#End Region

        End Select

    End Sub


#Region "Cursors Render"
    Sub ExportCursors([CP] As CP)
        Try : RenderCursor(CursorType.Arrow, [CP]) : Catch : End Try
        Try : RenderCursor(CursorType.Help, [CP]) : Catch : End Try
        Try : RenderCursor(CursorType.AppLoading, [CP]) : Catch : End Try
        Try : RenderCursor(CursorType.Busy, [CP]) : Catch : End Try
        Try : RenderCursor(CursorType.Pen, [CP]) : Catch : End Try
        Try : RenderCursor(CursorType.None, [CP]) : Catch : End Try
        Try : RenderCursor(CursorType.Move, [CP]) : Catch : End Try
        Try : RenderCursor(CursorType.Up, [CP]) : Catch : End Try
        Try : RenderCursor(CursorType.NS, [CP]) : Catch : End Try
        Try : RenderCursor(CursorType.EW, [CP]) : Catch : End Try
        Try : RenderCursor(CursorType.NESW, [CP]) : Catch : End Try
        Try : RenderCursor(CursorType.NWSE, [CP]) : Catch : End Try
        Try : RenderCursor(CursorType.Link, [CP]) : Catch : End Try
        Try : RenderCursor(CursorType.Pin, [CP]) : Catch : End Try
        Try : RenderCursor(CursorType.Person, [CP]) : Catch : End Try
        Try : RenderCursor(CursorType.IBeam, [CP]) : Catch : End Try
        Try : RenderCursor(CursorType.Cross, [CP]) : Catch : End Try
    End Sub

    Sub RenderCursor([Type] As CursorType, [CP] As CP)

        Dim CurName As String = ""

        Select Case [Type]
            Case CursorType.Arrow
                CurName = "Arrow"

            Case CursorType.Help
                CurName = "Help"

            Case CursorType.Busy
                CurName = "Busy"

            Case CursorType.AppLoading
                CurName = "AppLoading"

            Case CursorType.None
                CurName = "None"

            Case CursorType.Move
                CurName = "Move"

            Case CursorType.Up
                CurName = "Up"

            Case CursorType.NS
                CurName = "NS"

            Case CursorType.EW
                CurName = "EW"

            Case CursorType.NESW
                CurName = "NESW"

            Case CursorType.NWSE
                CurName = "NWSE"

            Case CursorType.Pen
                CurName = "Pen"

            Case CursorType.Link
                CurName = "Link"

            Case CursorType.Pin
                CurName = "Pin"

            Case CursorType.Person
                CurName = "Person"

            Case CursorType.IBeam
                CurName = "IBeam"

            Case CursorType.Cross
                CurName = "Cross"

        End Select

        If Not [Type] = CursorType.Busy And Not [Type] = CursorType.AppLoading Then

            If Not IO.Directory.Exists(My.Application.curPath) Then IO.Directory.CreateDirectory(My.Application.curPath)
            Dim Path As String = String.Format(My.Application.curPath & "\{0}.cur", CurName)

            Dim fs As New IO.FileStream(Path, IO.FileMode.Create)
            Dim EO As New EOIcoCurWriter(fs, 7, EOIcoCurWriter.IcoCurType.Cursor)

            For i As Single = 1 To 4 Step 0.5
                Dim bmp As New Bitmap(32 * i, 32 * i, Drawing.Imaging.PixelFormat.Format32bppPArgb)
                Dim HotPoint As New Point(1, 1)

                Select Case [Type]
                    Case CursorType.Arrow
#Region "Arrow"
                        With [CP]
                            bmp = Draw([Type],
                               .Cursor_Arrow_PrimaryColor1, .Cursor_Arrow_PrimaryColor2, .Cursor_Arrow_PrimaryColorGradient, .Cursor_Arrow_PrimaryColorGradientMode,
                               .Cursor_Arrow_SecondaryColor1, .Cursor_Arrow_SecondaryColor2, .Cursor_Arrow_SecondaryColorGradient, .Cursor_Arrow_SecondaryColorGradientMode,
                               Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                               .Cursor_Arrow_PrimaryColorNoise, .Cursor_Arrow_PrimaryColorNoiseOpacity, .Cursor_Arrow_SecondaryColorNoise, .Cursor_Arrow_SecondaryColorNoiseOpacity,
                               Nothing, Nothing, Nothing, Nothing,
                               1, i, 0)
                        End With
                        HotPoint = New Point(1, 1)
#End Region
                    Case CursorType.Help
#Region "Help"
                        With [CP]
                            bmp = Draw([Type],
                               .Cursor_Help_PrimaryColor1, .Cursor_Help_PrimaryColor2, .Cursor_Help_PrimaryColorGradient, .Cursor_Help_PrimaryColorGradientMode,
                               .Cursor_Help_SecondaryColor1, .Cursor_Help_SecondaryColor2, .Cursor_Help_SecondaryColorGradient, .Cursor_Help_SecondaryColorGradientMode,
                               Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                               .Cursor_Help_PrimaryColorNoise, .Cursor_Help_PrimaryColorNoiseOpacity, .Cursor_Help_SecondaryColorNoise, .Cursor_Help_SecondaryColorNoiseOpacity,
                               Nothing, Nothing, Nothing, Nothing,
                               1, i, 0)
                        End With
                        HotPoint = New Point(1, 1)
#End Region
                    Case CursorType.None
#Region "None"
                        With [CP]
                            bmp = Draw([Type],
                               .Cursor_None_PrimaryColor1, .Cursor_None_PrimaryColor2, .Cursor_None_PrimaryColorGradient, .Cursor_None_PrimaryColorGradientMode,
                               .Cursor_None_SecondaryColor1, .Cursor_None_SecondaryColor2, .Cursor_None_SecondaryColorGradient, .Cursor_None_SecondaryColorGradientMode,
                               Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                               .Cursor_None_PrimaryColorNoise, .Cursor_None_PrimaryColorNoiseOpacity, .Cursor_None_SecondaryColorNoise, .Cursor_None_SecondaryColorNoiseOpacity,
                               Nothing, Nothing, Nothing, Nothing,
                               1, i, 0)
                        End With
                        HotPoint = New Point(1 + CInt(8 * i), 1 + CInt(8 * i))
#End Region
                    Case CursorType.Move
#Region "Move"
                        With [CP]
                            bmp = Draw([Type],
                               .Cursor_Move_PrimaryColor1, .Cursor_Move_PrimaryColor2, .Cursor_Move_PrimaryColorGradient, .Cursor_Move_PrimaryColorGradientMode,
                               .Cursor_Move_SecondaryColor1, .Cursor_Move_SecondaryColor2, .Cursor_Move_SecondaryColorGradient, .Cursor_Move_SecondaryColorGradientMode,
                               Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                               .Cursor_Move_PrimaryColorNoise, .Cursor_Move_PrimaryColorNoiseOpacity, .Cursor_Move_SecondaryColorNoise, .Cursor_Move_SecondaryColorNoiseOpacity,
                               Nothing, Nothing, Nothing, Nothing,
                               1, i, 0)
                        End With
                        HotPoint = New Point(1 + CInt(11 * i), 1 + CInt(11 * i))
#End Region
                    Case CursorType.Up
#Region "Up"
                        With [CP]
                            bmp = Draw([Type],
                               .Cursor_Up_PrimaryColor1, .Cursor_Up_PrimaryColor2, .Cursor_Up_PrimaryColorGradient, .Cursor_Up_PrimaryColorGradientMode,
                               .Cursor_Up_SecondaryColor1, .Cursor_Up_SecondaryColor2, .Cursor_Up_SecondaryColorGradient, .Cursor_Up_SecondaryColorGradientMode,
                               Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                               .Cursor_Up_PrimaryColorNoise, .Cursor_Up_PrimaryColorNoiseOpacity, .Cursor_Up_SecondaryColorNoise, .Cursor_Up_SecondaryColorNoiseOpacity,
                               Nothing, Nothing, Nothing, Nothing,
                               1, i, 0)
                        End With
                        HotPoint = New Point(1 + CInt(4 * i), 1)
#End Region
                    Case CursorType.NS
#Region "NS"
                        With [CP]
                            bmp = Draw([Type],
                               .Cursor_NS_PrimaryColor1, .Cursor_NS_PrimaryColor2, .Cursor_NS_PrimaryColorGradient, .Cursor_NS_PrimaryColorGradientMode,
                               .Cursor_NS_SecondaryColor1, .Cursor_NS_SecondaryColor2, .Cursor_NS_SecondaryColorGradient, .Cursor_NS_SecondaryColorGradientMode,
                               Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                               .Cursor_NS_PrimaryColorNoise, .Cursor_NS_PrimaryColorNoiseOpacity, .Cursor_NS_SecondaryColorNoise, .Cursor_NS_SecondaryColorNoiseOpacity,
                               Nothing, Nothing, Nothing, Nothing,
                               1, i, 0)
                        End With
                        HotPoint = New Point(1 + CInt(4 * i), 1 + CInt(11 * i))
#End Region
                    Case CursorType.EW
#Region "EW"
                        With [CP]
                            bmp = Draw([Type],
                               .Cursor_EW_PrimaryColor1, .Cursor_EW_PrimaryColor2, .Cursor_EW_PrimaryColorGradient, .Cursor_EW_PrimaryColorGradientMode,
                               .Cursor_EW_SecondaryColor1, .Cursor_EW_SecondaryColor2, .Cursor_EW_SecondaryColorGradient, .Cursor_EW_SecondaryColorGradientMode,
                               Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                               .Cursor_EW_PrimaryColorNoise, .Cursor_EW_PrimaryColorNoiseOpacity, .Cursor_EW_SecondaryColorNoise, .Cursor_EW_SecondaryColorNoiseOpacity,
                               Nothing, Nothing, Nothing, Nothing,
                               1, i, 0)
                        End With
                        HotPoint = New Point(1 + 11 * i, 1 + 4 * i)
#End Region
                    Case CursorType.NESW
#Region "NESW"
                        With [CP]
                            bmp = Draw([Type],
                               .Cursor_NESW_PrimaryColor1, .Cursor_NESW_PrimaryColor2, .Cursor_NESW_PrimaryColorGradient, .Cursor_NESW_PrimaryColorGradientMode,
                               .Cursor_NESW_SecondaryColor1, .Cursor_NESW_SecondaryColor2, .Cursor_NESW_SecondaryColorGradient, .Cursor_NESW_SecondaryColorGradientMode,
                               Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                               .Cursor_NESW_PrimaryColorNoise, .Cursor_NESW_PrimaryColorNoiseOpacity, .Cursor_NESW_SecondaryColorNoise, .Cursor_NESW_SecondaryColorNoiseOpacity,
                               Nothing, Nothing, Nothing, Nothing,
                               1, i, 0)
                        End With
                        HotPoint = New Point(1 + CInt(8 * i), 1 + CInt(8 * i))
#End Region
                    Case CursorType.NWSE
#Region "NWSE"
                        With [CP]
                            bmp = Draw([Type],
                               .Cursor_NWSE_PrimaryColor1, .Cursor_NWSE_PrimaryColor2, .Cursor_NWSE_PrimaryColorGradient, .Cursor_NWSE_PrimaryColorGradientMode,
                               .Cursor_NWSE_SecondaryColor1, .Cursor_NWSE_SecondaryColor2, .Cursor_NWSE_SecondaryColorGradient, .Cursor_NWSE_SecondaryColorGradientMode,
                               Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                               .Cursor_NWSE_PrimaryColorNoise, .Cursor_NWSE_PrimaryColorNoiseOpacity, .Cursor_NWSE_SecondaryColorNoise, .Cursor_NWSE_SecondaryColorNoiseOpacity,
                               Nothing, Nothing, Nothing, Nothing,
                               1, i, 0)
                        End With
                        HotPoint = New Point(1 + CInt(8 * i), 1 + CInt(8 * i))
#End Region
                    Case CursorType.Pen
#Region "Pen"
                        With [CP]
                            bmp = Draw([Type],
                               .Cursor_Pen_PrimaryColor1, .Cursor_Pen_PrimaryColor2, .Cursor_Pen_PrimaryColorGradient, .Cursor_Pen_PrimaryColorGradientMode,
                               .Cursor_Pen_SecondaryColor1, .Cursor_Pen_SecondaryColor2, .Cursor_Pen_SecondaryColorGradient, .Cursor_Pen_SecondaryColorGradientMode,
                               Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                               .Cursor_Pen_PrimaryColorNoise, .Cursor_Pen_PrimaryColorNoiseOpacity, .Cursor_Pen_SecondaryColorNoise, .Cursor_Pen_SecondaryColorNoiseOpacity,
                               Nothing, Nothing, Nothing, Nothing,
                               1, i, 0)
                        End With
                        HotPoint = New Point(1, 1)
#End Region
                    Case CursorType.Link
#Region "Link"
                        With [CP]
                            bmp = Draw([Type],
                               .Cursor_Link_PrimaryColor1, .Cursor_Link_PrimaryColor2, .Cursor_Link_PrimaryColorGradient, .Cursor_Link_PrimaryColorGradientMode,
                               .Cursor_Link_SecondaryColor1, .Cursor_Link_SecondaryColor2, .Cursor_Link_SecondaryColorGradient, .Cursor_Link_SecondaryColorGradientMode,
                               Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                               .Cursor_Link_PrimaryColorNoise, .Cursor_Link_PrimaryColorNoiseOpacity, .Cursor_Link_SecondaryColorNoise, .Cursor_Link_SecondaryColorNoiseOpacity,
                               Nothing, Nothing, Nothing, Nothing,
                               1, i, 0)
                        End With
                        HotPoint = New Point(1 + CInt(6 * i), 1)
#End Region
                    Case CursorType.Pin
#Region "Pin"
                        With [CP]
                            bmp = Draw([Type],
                               .Cursor_Pin_PrimaryColor1, .Cursor_Pin_PrimaryColor2, .Cursor_Pin_PrimaryColorGradient, .Cursor_Pin_PrimaryColorGradientMode,
                               .Cursor_Pin_SecondaryColor1, .Cursor_Pin_SecondaryColor2, .Cursor_Pin_SecondaryColorGradient, .Cursor_Pin_SecondaryColorGradientMode,
                               Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                               .Cursor_Pin_PrimaryColorNoise, .Cursor_Pin_PrimaryColorNoiseOpacity, .Cursor_Pin_SecondaryColorNoise, .Cursor_Pin_SecondaryColorNoiseOpacity,
                               Nothing, Nothing, Nothing, Nothing,
                               1, i, 0)
                        End With
                        HotPoint = New Point(1 + CInt(6 * i), 1)
#End Region
                    Case CursorType.Person
#Region "Person"
                        With [CP]
                            bmp = Draw([Type],
                               .Cursor_Person_PrimaryColor1, .Cursor_Person_PrimaryColor2, .Cursor_Person_PrimaryColorGradient, .Cursor_Person_PrimaryColorGradientMode,
                               .Cursor_Person_SecondaryColor1, .Cursor_Person_SecondaryColor2, .Cursor_Person_SecondaryColorGradient, .Cursor_Person_SecondaryColorGradientMode,
                               Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                               .Cursor_Person_PrimaryColorNoise, .Cursor_Person_PrimaryColorNoiseOpacity, .Cursor_Person_SecondaryColorNoise, .Cursor_Person_SecondaryColorNoiseOpacity,
                               Nothing, Nothing, Nothing, Nothing,
                               1, i, 0)
                        End With
                        HotPoint = New Point(1 + CInt(6 * i), 1)
#End Region
                    Case CursorType.IBeam
#Region "IBeam"
                        With [CP]
                            bmp = Draw([Type],
                               .Cursor_IBeam_PrimaryColor1, .Cursor_IBeam_PrimaryColor2, .Cursor_IBeam_PrimaryColorGradient, .Cursor_IBeam_PrimaryColorGradientMode,
                               .Cursor_IBeam_SecondaryColor1, .Cursor_IBeam_SecondaryColor2, .Cursor_IBeam_SecondaryColorGradient, .Cursor_IBeam_SecondaryColorGradientMode,
                               Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                               .Cursor_IBeam_PrimaryColorNoise, .Cursor_IBeam_PrimaryColorNoiseOpacity, .Cursor_IBeam_SecondaryColorNoise, .Cursor_IBeam_SecondaryColorNoiseOpacity,
                               Nothing, Nothing, Nothing, Nothing,
                               1, i, 0)
                        End With
                        HotPoint = New Point(1 + CInt(4 * i), 1 + CInt(9 * i))
#End Region
                    Case CursorType.Cross
#Region "Cross"
                        With [CP]
                            bmp = Draw([Type],
                               .Cursor_Cross_PrimaryColor1, .Cursor_Cross_PrimaryColor2, .Cursor_Cross_PrimaryColorGradient, .Cursor_Cross_PrimaryColorGradientMode,
                               .Cursor_Cross_SecondaryColor1, .Cursor_Cross_SecondaryColor2, .Cursor_Cross_SecondaryColorGradient, .Cursor_Cross_SecondaryColorGradientMode,
                               Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                               .Cursor_Cross_PrimaryColorNoise, .Cursor_Cross_PrimaryColorNoiseOpacity, .Cursor_Cross_SecondaryColorNoise, .Cursor_Cross_SecondaryColorNoiseOpacity,
                               Nothing, Nothing, Nothing, Nothing,
                               1, i, 0)
                        End With
                        HotPoint = New Point(1 + CInt(9 * i), 1 + CInt(9 * i))
#End Region
                End Select

                EO.WriteBitmap(bmp, Nothing, HotPoint)

            Next

            fs.Close()

        Else
            Dim HotPoint As New Point(1, 1)

            For i As Single = 1 To 4 Step 1
                Dim BMPList As New List(Of Bitmap)
                BMPList.Clear()

#Region "Add angles bitmaps from 180 deg to 180 deg (Cycle)"
                With [CP]
                    For ang As Integer = 180 To 360 Step +10
                        Dim bm As Bitmap

                        If [Type] = CursorType.AppLoading Then
                            bm = New Bitmap(Draw([Type],
                                            .Cursor_AppLoading_PrimaryColor1, .Cursor_AppLoading_PrimaryColor2, .Cursor_AppLoading_PrimaryColorGradient, .Cursor_AppLoading_PrimaryColorGradientMode,
                                            .Cursor_AppLoading_SecondaryColor1, .Cursor_AppLoading_SecondaryColor2, .Cursor_AppLoading_SecondaryColorGradient, .Cursor_AppLoading_SecondaryColorGradientMode,
                                            .Cursor_AppLoading_LoadingCircleBack1, .Cursor_AppLoading_LoadingCircleBack2, .Cursor_AppLoading_LoadingCircleBackGradient, .Cursor_AppLoading_LoadingCircleBackGradientMode,
                                            .Cursor_AppLoading_LoadingCircleHot1, .Cursor_AppLoading_LoadingCircleHot2, .Cursor_AppLoading_LoadingCircleHotGradient, .Cursor_AppLoading_LoadingCircleHotGradientMode,
                                            .Cursor_AppLoading_PrimaryColorNoise, .Cursor_AppLoading_PrimaryColorNoiseOpacity, .Cursor_AppLoading_SecondaryColorNoise, .Cursor_AppLoading_SecondaryColorNoiseOpacity,
                                            .Cursor_AppLoading_LoadingCircleBackNoise, .Cursor_AppLoading_LoadingCircleBackNoiseOpacity, .Cursor_AppLoading_LoadingCircleHotNoise, .Cursor_AppLoading_LoadingCircleHotNoiseOpacity,
                                             1, i, ang))

                            HotPoint = New Point(1, 1 + CInt(8 * i))
                        Else
                            bm = New Bitmap(Draw([Type],
                                            Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                                            .Cursor_Busy_LoadingCircleBack1, .Cursor_Busy_LoadingCircleBack2, .Cursor_Busy_LoadingCircleBackGradient, .Cursor_Busy_LoadingCircleBackGradientMode,
                                            .Cursor_Busy_LoadingCircleHot1, .Cursor_Busy_LoadingCircleHot2, .Cursor_Busy_LoadingCircleHotGradient, .Cursor_Busy_LoadingCircleHotGradientMode,
                                            Nothing, Nothing, Nothing, Nothing,
                                            .Cursor_Busy_LoadingCircleBackNoise, .Cursor_Busy_LoadingCircleBackNoiseOpacity, .Cursor_Busy_LoadingCircleHotNoise, .Cursor_Busy_LoadingCircleHotNoiseOpacity,
                                            1, i, ang))

                            HotPoint = New Point(1 + CInt(11 * i), 1 + CInt(11 * i))
                        End If

                        BMPList.Add(bm)
                    Next

                    For ang As Integer = 0 To 180 Step +10
                        Dim bm As Bitmap

                        If [Type] = CursorType.AppLoading Then
                            bm = New Bitmap(Draw([Type],
                                            .Cursor_AppLoading_PrimaryColor1, .Cursor_AppLoading_PrimaryColor2, .Cursor_AppLoading_PrimaryColorGradient, .Cursor_AppLoading_PrimaryColorGradientMode,
                                            .Cursor_AppLoading_SecondaryColor1, .Cursor_AppLoading_SecondaryColor2, .Cursor_AppLoading_SecondaryColorGradient, .Cursor_AppLoading_SecondaryColorGradientMode,
                                            .Cursor_AppLoading_LoadingCircleBack1, .Cursor_AppLoading_LoadingCircleBack2, .Cursor_AppLoading_LoadingCircleBackGradient, .Cursor_AppLoading_LoadingCircleBackGradientMode,
                                            .Cursor_AppLoading_LoadingCircleHot1, .Cursor_AppLoading_LoadingCircleHot2, .Cursor_AppLoading_LoadingCircleHotGradient, .Cursor_AppLoading_LoadingCircleHotGradientMode,
                                            .Cursor_AppLoading_PrimaryColorNoise, .Cursor_AppLoading_PrimaryColorNoiseOpacity, .Cursor_AppLoading_SecondaryColorNoise, .Cursor_AppLoading_SecondaryColorNoiseOpacity,
                                            .Cursor_AppLoading_LoadingCircleBackNoise, .Cursor_AppLoading_LoadingCircleBackNoiseOpacity, .Cursor_AppLoading_LoadingCircleHotNoise, .Cursor_AppLoading_LoadingCircleHotNoiseOpacity,
                                             1, i, ang))

                            HotPoint = New Point(1, 1 + CInt(8 * i))
                        Else
                            bm = New Bitmap(Draw([Type],
                                            Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                                            .Cursor_Busy_LoadingCircleBack1, .Cursor_Busy_LoadingCircleBack2, .Cursor_Busy_LoadingCircleBackGradient, .Cursor_Busy_LoadingCircleBackGradientMode,
                                            .Cursor_Busy_LoadingCircleHot1, .Cursor_Busy_LoadingCircleHot2, .Cursor_Busy_LoadingCircleHotGradient, .Cursor_Busy_LoadingCircleHotGradientMode,
                                            Nothing, Nothing, Nothing, Nothing,
                                            .Cursor_Busy_LoadingCircleBackNoise, .Cursor_Busy_LoadingCircleBackNoiseOpacity, .Cursor_Busy_LoadingCircleHotNoise, .Cursor_Busy_LoadingCircleHotNoiseOpacity,
                                            1, i, ang))

                            HotPoint = New Point(1 + CInt(11 * i), 1 + CInt(11 * i))
                        End If

                        BMPList.Add(bm)
                    Next
                End With
#End Region

                Dim Count As Integer = BMPList.Count
                Dim frameRates As UInteger() = New UInteger(Count - 1) {}
                Dim seqNums As UInteger() = New UInteger(Count - 1) {}
                Dim Speed As Integer = 2

                For ixx = 0 To Count - 1
                    frameRates(ixx) = Convert.ToUInt32(Speed)
                    seqNums(ixx) = CUInt(ixx)
                Next

                If Not IO.Directory.Exists(My.Application.curPath) Then IO.Directory.CreateDirectory(My.Application.curPath)
                Dim fs As New IO.FileStream(String.Format(My.Application.curPath & "\{0}_{1}x.ani", CurName, i), IO.FileMode.Create)

                Dim AN As New EOANIWriter(fs, Count, Speed, frameRates, seqNums, Nothing, Nothing, HotPoint)

                For ix = 0 To Count - 1
                    AN.WriteFrame32(BMPList(ix))
                Next

                fs.Close()
            Next

        End If

    End Sub

    Sub ApplyCursorsToReg()
        Dim rMain As RegistryKey = Registry.CurrentUser.CreateSubKey("Control Panel\Cursors\Schemes")
        Dim RegValue As String
        Dim Path As String = My.Application.curPath

        RegValue = String.Format("{0}\{1}", Path, "Arrow.cur")
        RegValue &= String.Format(",{0}\{1}", Path, "Help.cur")
        RegValue &= String.Format(",{0}\{1}", Path, "AppLoading_1x.ani")
        RegValue &= String.Format(",{0}\{1}", Path, "Busy_1x.ani")
        RegValue &= String.Format(",{0}\{1}", Path, "Cross.cur")
        RegValue &= String.Format(",{0}\{1}", Path, "IBeam.cur")
        RegValue &= String.Format(",{0}\{1}", Path, "Pen.cur")
        RegValue &= String.Format(",{0}\{1}", Path, "None.cur")
        RegValue &= String.Format(",{0}\{1}", Path, "NS.cur")
        RegValue &= String.Format(",{0}\{1}", Path, "EW.cur")
        RegValue &= String.Format(",{0}\{1}", Path, "NWSE.cur")
        RegValue &= String.Format(",{0}\{1}", Path, "NESW.cur")
        RegValue &= String.Format(",{0}\{1}", Path, "Move.cur")
        RegValue &= String.Format(",{0}\{1}", Path, "Up.cur")
        RegValue &= String.Format(",{0}\{1}", Path, "Link.cur")
        RegValue &= String.Format(",{0}\{1}", Path, "Pin.cur")
        RegValue &= String.Format(",{0}\{1}", Path, "Person.cur")

        rMain.SetValue("WinPaletter", RegValue, RegistryValueKind.String)

        rMain = Registry.CurrentUser.CreateSubKey("Control Panel\Cursors")
        rMain.SetValue("", "WinPaletter")
        rMain.SetValue("CursorBaseSize", 32, RegistryValueKind.DWord)

        Dim x As String = String.Format("{0}\{1}", Path, "AppLoading_1x.ani")
        rMain.SetValue("AppStarting", x)
        NativeMethods.User32.SetSystemCursor(NativeMethods.User32.LoadCursorFromFile(x), NativeMethods.User32.OCR_SYSTEM_CURSORS.OCR_APPSTARTING)

        x = String.Format("{0}\{1}", Path, "Arrow.cur")
        rMain.SetValue("Arrow", x)
        NativeMethods.User32.SetSystemCursor(NativeMethods.User32.LoadCursorFromFile(x), NativeMethods.User32.OCR_SYSTEM_CURSORS.OCR_NORMAL)

        x = String.Format("{0}\{1}", Path, "Cross.cur")
        rMain.SetValue("Crosshair", x)
        NativeMethods.User32.SetSystemCursor(NativeMethods.User32.LoadCursorFromFile(x), NativeMethods.User32.OCR_SYSTEM_CURSORS.OCR_CROSS)

        x = String.Format("{0}\{1}", Path, "Link.cur")
        rMain.SetValue("Hand", x)
        NativeMethods.User32.SetSystemCursor(NativeMethods.User32.LoadCursorFromFile(x), NativeMethods.User32.OCR_SYSTEM_CURSORS.OCR_HAND)

        x = String.Format("{0}\{1}", Path, "Help.cur")
        rMain.SetValue("Help", x)
        NativeMethods.User32.SetSystemCursor(NativeMethods.User32.LoadCursorFromFile(x), NativeMethods.User32.OCR_SYSTEM_CURSORS.OCR_HELP)

        x = String.Format("{0}\{1}", Path, "IBeam.cur")
        rMain.SetValue("IBeam", x)
        NativeMethods.User32.SetSystemCursor(NativeMethods.User32.LoadCursorFromFile(x), NativeMethods.User32.OCR_SYSTEM_CURSORS.OCR_IBEAM)

        x = String.Format("{0}\{1}", Path, "None.cur")
        rMain.SetValue("No", x)
        NativeMethods.User32.SetSystemCursor(NativeMethods.User32.LoadCursorFromFile(x), NativeMethods.User32.OCR_SYSTEM_CURSORS.OCR_NO)

        x = String.Format("{0}\{1}", Path, "Pen.cur")
        rMain.SetValue("NWPen", x)
        'NativeMethods.User32.SetSystemCursor(NativeMethods.User32.LoadCursorFromFile(x), NativeMethods.User32.OCR_SYSTEM_CURSORS.OCR_)

        x = String.Format("{0}\{1}", Path, "Person.cur")
        rMain.SetValue("Person", x)
        'NativeMethods.User32.SetSystemCursor(NativeMethods.User32.LoadCursorFromFile(x), NativeMethods.User32.OCR_SYSTEM_CURSORS.OCR_APPSTARTING)

        x = String.Format("{0}\{1}", Path, "Pin.cur")
        rMain.SetValue("Pin", x)
        'NativeMethods.User32.SetSystemCursor(NativeMethods.User32.LoadCursorFromFile(x), NativeMethods.User32.OCR_SYSTEM_CURSORS.OCR_APPSTARTING)

        x = String.Format("{0}\{1}", Path, "Move.cur")
        rMain.SetValue("SizeAll", x)
        NativeMethods.User32.SetSystemCursor(NativeMethods.User32.LoadCursorFromFile(x), NativeMethods.User32.OCR_SYSTEM_CURSORS.OCR_SIZEALL)

        x = String.Format("{0}\{1}", Path, "NESW.cur")
        rMain.SetValue("SizeNESW", x)
        NativeMethods.User32.SetSystemCursor(NativeMethods.User32.LoadCursorFromFile(x), NativeMethods.User32.OCR_SYSTEM_CURSORS.OCR_SIZENESW)

        x = String.Format("{0}\{1}", Path, "NS.cur")
        rMain.SetValue("SizeNS", x)
        NativeMethods.User32.SetSystemCursor(NativeMethods.User32.LoadCursorFromFile(x), NativeMethods.User32.OCR_SYSTEM_CURSORS.OCR_SIZENS)

        x = String.Format("{0}\{1}", Path, "NWSE.cur")
        rMain.SetValue("SizeNWSE", x)
        NativeMethods.User32.SetSystemCursor(NativeMethods.User32.LoadCursorFromFile(x), NativeMethods.User32.OCR_SYSTEM_CURSORS.OCR_SIZENWSE)

        x = String.Format("{0}\{1}", Path, "EW.cur")
        rMain.SetValue("SizeWE", x)
        NativeMethods.User32.SetSystemCursor(NativeMethods.User32.LoadCursorFromFile(x), NativeMethods.User32.OCR_SYSTEM_CURSORS.OCR_SIZEWE)

        x = String.Format("{0}\{1}", Path, "Up.cur")
        rMain.SetValue("UpArrow", x)
        NativeMethods.User32.SetSystemCursor(NativeMethods.User32.LoadCursorFromFile(x), NativeMethods.User32.OCR_SYSTEM_CURSORS.OCR_UP)

        x = String.Format("{0}\{1}", Path, "Busy_1x.ani")
        rMain.SetValue("Wait", x)
        NativeMethods.User32.SetSystemCursor(NativeMethods.User32.LoadCursorFromFile(x), NativeMethods.User32.OCR_SYSTEM_CURSORS.OCR_WAIT)

        rMain.SetValue("Scheme Source", 1, RegistryValueKind.DWord)
        rMain.Close()
    End Sub
#End Region

    Public Overrides Function Equals(obj As Object) As Boolean
        Dim _Equals As Boolean = True

        Dim type1 As Type = [GetType]() : Dim properties1 As System.Reflection.PropertyInfo() = type1.GetProperties()
        Dim type2 As Type = obj.[GetType]() : Dim properties2 As System.Reflection.PropertyInfo() = type2.GetProperties()

        For Each [property] As System.Reflection.PropertyInfo In properties1

            If [property].PropertyType.Name.ToLower <> "winterminal" Then
                Try
                    If [property].GetValue(Me, Nothing) <> [property].GetValue(obj, Nothing) Then
                        _Equals = False
                        Exit For
                    End If
                Catch
                End Try
            End If
        Next

        Return _Equals
    End Function
End Class

