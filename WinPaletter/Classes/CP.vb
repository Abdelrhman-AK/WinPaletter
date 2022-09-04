﻿Imports System.Drawing.Imaging
Imports System.Globalization
Imports System.IO
Imports System.Reflection
Imports System.Runtime.InteropServices
Imports System.Security.Principal
Imports System.Text
Imports System.Threading
Imports Microsoft.Win32
Imports WinPaletter.XenonCore

Public Class CP

#Region "System DLLs Functions"
    <DllImport("user32.dll")>
    Private Shared Function SetSysColors(ByVal cElements As Integer, ByVal lpaElements As Integer(), ByVal lpaRgbValues As UInteger()) As Boolean
    End Function

    <DllImport("UxTheme.DLL", BestFitMapping:=False, CallingConvention:=CallingConvention.Winapi, CharSet:=CharSet.Unicode, EntryPoint:="#65")>
    Shared Function SetSystemVisualStyle(ByVal pszFilename As String, ByVal pszColor As String, ByVal pszSize As String, ByVal dwReserved As Integer) As Integer
    End Function

    <DllImport("uxtheme", ExactSpelling:=True)>
    Public Shared Function EnableTheming(ByVal fEnable As Integer) As Integer
    End Function

    Declare Unicode Function GetCurrentThemeName Lib "uxtheme" (ByVal stringThemeName As StringBuilder, ByVal lengthThemeName As Integer, ByVal stringColorName As StringBuilder, ByVal lengthColorName As Integer, ByVal stringSizeName As StringBuilder, ByVal lengthSizeName As Integer) As Int32

    <DllImport("user32", CharSet:=CharSet.Auto)>
    Public Shared Function SystemParametersInfo(intAction As Integer, intParam As Integer, strParam As String, intWinIniFlag As Integer) As Integer
    End Function
    Private Declare Function SystemParametersInfo Lib "user32" Alias "SystemParametersInfoA" (uAction As Integer, uParam As Integer, ByVal lpvParam As Integer, fuWinIni As Integer) As Integer
#End Region

#Region "Properties"
    ReadOnly isElevated As Boolean = New WindowsPrincipal(WindowsIdentity.GetCurrent).IsInRole(WindowsBuiltInRole.Administrator)

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
    Public Property Transparency As Boolean = True
    Public Property ApplyAccentonTitlebars As Boolean = False
    Public Property ApplyAccentonTaskbar As Boolean = False
#End Region

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
    Public Property Metro_LockScreenType As LogonUI8_Modes = LogonUI8_Modes.System
    Public Property Metro_LockScreenSystemID As Integer = 0
#End Region

#Region "LogonUI_Win10"
    Public Property LogonUI_Background As Color
    Public Property LogonUI_PersonalColors_Background As Color
    Public Property LogonUI_PersonalColors_Accent As Color
    Public Property LogonUI_DisableAcrylicBackgroundOnLogon As Boolean = False
    Public Property LogonUI_DisableLogonBackgroundImage As Boolean = False
    Public Property LogonUI_NoLockScreen As Boolean = False
#End Region

#Region "LogonUI_Win7"
    Public Property LogonUI7_Enabled As Boolean = False
    Public Property LogonUI7_Mode As LogonUI7_Modes = LogonUI7_Modes.Default_
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

    Enum LogonUI7_Modes
        Default_
        Wallpaper
        CustomImage
        SolidColor
    End Enum
    Enum LogonUI8_Modes
        System
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
        Catch 'ex As Exception
            'MsgBox(ex.Message & vbCrLf & vbCrLf & ex.StackTrace)
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
    Function RGB2HEX_oneline(ByVal [Color] As Color, Optional ByVal Alpha As Boolean = True) As String
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
#End Region

    Public f As Form
    Dim th As System.Threading.Thread

    Public Sub Task_A()
        f = New ApplyingTheme()
        Application.Run(f)
    End Sub

    Sub New([Mode] As Mode, Optional ByVal PaletteFile As String = "")
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

                    Dim x As Byte() = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "AccentPalette", If(My.W11, New CP_Defaults().Default_Windows11Accents_Bytes, New CP_Defaults().Default_Windows10Accents_Bytes))

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
                    y = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "StartColorMenu", BizareColorInvertor(Def.StartMenu_Accent).ToArgb)
                    StartMenu_Accent = BizareColorInvertor(Color.FromArgb(y))

                    y = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "AccentColorMenu", BizareColorInvertor(Def.Titlebar_Active).ToArgb)
                    Titlebar_Active = BizareColorInvertor(Color.FromArgb(y))

                    y = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\DWM", "AccentColor", BizareColorInvertor(Def.Titlebar_Active).ToArgb)
                    Titlebar_DWM_Active = BizareColorInvertor(Color.FromArgb(y))

                    y = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\DWM", "AccentColorInactive", BizareColorInvertor(Def.Titlebar_Inactive).ToArgb)
                    Titlebar_Inactive = BizareColorInvertor(Color.FromArgb(y))

                    WinMode_Light = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "SystemUsesLightTheme", Def.WinMode_Light)
                    AppMode_Light = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "AppsUseLightTheme", Def.AppMode_Light)
                    Transparency = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "EnableTransparency", Def.Transparency)
                    ApplyAccentonTaskbar = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "ColorPrevalence", Def.ApplyAccentonTaskbar)
                    ApplyAccentonTitlebars = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\DWM", "ColorPrevalence", Def.ApplyAccentonTitlebars)

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
                    Titlebar_DWM_Active = _Def.Titlebar_DWM_Active
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

                    Dim y As Integer
                    y = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationColor", Def.Aero_ColorizationColor.ToArgb)
                    Aero_ColorizationColor = Color.FromArgb(255, Color.FromArgb(y))

                    y = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationColorBalance", Def.Aero_ColorizationColorBalance)
                    Aero_ColorizationColorBalance = y

                    If Not My.W8 Then
                        y = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationAfterglow", Def.Aero_ColorizationAfterglow.ToArgb)
                        Aero_ColorizationAfterglow = Color.FromArgb(255, Color.FromArgb(y))

                        y = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationAfterglowBalance", Def.Aero_ColorizationAfterglowBalance)
                        Aero_ColorizationAfterglowBalance = y

                        y = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationBlurBalance", Def.Aero_ColorizationBlurBalance)
                        Aero_ColorizationBlurBalance = y

                        y = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationGlassReflectionIntensity", Def.Aero_ColorizationGlassReflectionIntensity)
                        Aero_ColorizationGlassReflectionIntensity = y

                        Dim Com As Boolean
                        NativeMethods.Dwmapi.DwmIsCompositionEnabled(Com)

                        Dim Opaque As Boolean = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationOpaqueBlend", False)

                        Dim Classic As Boolean = False

                        Try
                            Dim stringThemeName As StringBuilder = New StringBuilder(260)
                            GetCurrentThemeName(stringThemeName, 260, Nothing, 0, Nothing, 0)
                            Classic = String.IsNullOrWhiteSpace(stringThemeName.ToString) Or Not File.Exists(stringThemeName.ToString)
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

                    Aero_EnableAeroPeek = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "EnableAeroPeek", Def.Aero_EnableAeroPeek)
                    Aero_AlwaysHibernateThumbnails = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "AlwaysHibernateThumbnails", Def.Aero_AlwaysHibernateThumbnails)

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

                    Dim stringThemeName As StringBuilder = New StringBuilder(260)
                    GetCurrentThemeName(stringThemeName, 260, Nothing, 0, Nothing, 0)

                    If stringThemeName.ToString.Split("\").Last.ToLower = "aerolite.msstyles" Then
                        Metro_Theme = AeroTheme.AeroLite
                    Else
                        Metro_Theme = AeroTheme.Aero
                    End If

                    Registry.CurrentUser.CreateSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent")

                    If isElevated Then
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

                    Dim y As Integer
                    y = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "StartColor", Color.FromArgb(84, 0, 30).ToArgb)
                    Metro_StartColor = Color.FromArgb(255, BizareColorInvertor(Color.FromArgb(y)))

                    y = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "AccentColor", Color.FromArgb(178, 29, 72).ToArgb)
                    Metro_AccentColor = Color.FromArgb(255, BizareColorInvertor(Color.FromArgb(y)))

                    Dim S As String
                    S = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Personalization", "PersonalColors_Background", "#1e0054")
                    Metro_PersonalColors_Background = Color.FromArgb(255, Color.FromArgb(Convert.ToInt32(S.Replace("#", ""), 16)))

                    S = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Personalization", "PersonalColors_Accent", "#481db2")
                    Metro_PersonalColors_Accent = Color.FromArgb(255, Color.FromArgb(Convert.ToInt32(S.Replace("#", ""), 16)))

                    Metro_Start = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Personalization", "ForceStartBackground", 0)

                    Metro_LogonUI = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "DefaultColorSet", 0)

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

                    Dim y As Integer

                    With My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon", "Background", Def.LogonUI_Background.ToArgb)
                        LogonUI_Background = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                    End With

                    y = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Windows\Personalization", "PersonalColors_Background", BizareColorInvertor(Def.LogonUI_PersonalColors_Background).ToArgb)
                    LogonUI_PersonalColors_Background = BizareColorInvertor(Color.FromArgb(y))

                    y = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Windows\Personalization", "PersonalColors_Accent", BizareColorInvertor(Def.LogonUI_PersonalColors_Accent).ToArgb)
                    LogonUI_PersonalColors_Accent = BizareColorInvertor(Color.FromArgb(y))

                    LogonUI_DisableAcrylicBackgroundOnLogon = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\System", "DisableAcrylicBackgroundOnLogon", Def.LogonUI_DisableAcrylicBackgroundOnLogon)
                    LogonUI_DisableLogonBackgroundImage = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\System", "DisableLogonBackgroundImage", Def.LogonUI_DisableLogonBackgroundImage)
                    LogonUI_NoLockScreen = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Windows\Personalization", "NoLockScreen", Def.LogonUI_NoLockScreen)
                Else
                    LogonUI_Background = _Def.LogonUI_Background
                    LogonUI_PersonalColors_Background = _Def.LogonUI_PersonalColors_Background
                    LogonUI_PersonalColors_Accent = _Def.LogonUI_PersonalColors_Accent
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
                    LogonUI7_Mode = rLog.GetValue("Mode", LogonUI7_Modes.Default_)
                    LogonUI7_ImagePath = rLog.GetValue("ImagePath", "")
                    LogonUI7_Color = Color.FromArgb(rLog.GetValue("Color", Color.Black.ToArgb))
                    LogonUI7_Effect_Blur = rLog.GetValue("Effect_Blur", False)
                    LogonUI7_Effect_Blur_Intensity = rLog.GetValue("Effect_Blur_Intensity", 0)
                    LogonUI7_Effect_Grayscale = rLog.GetValue("Effect_Grayscale", False)
                    LogonUI7_Effect_Noise = rLog.GetValue("Effect_Noise", False)
                    LogonUI7_Effect_Noise_Mode = rLog.GetValue("Noise_Mode", LogonUI7_NoiseMode.Acrylic)
                    LogonUI7_Effect_Noise_Intensity = rLog.GetValue("Effect_Noise_Intensity", 0)
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

                    Metro_LockScreenType = rLog.GetValue("Mode", LogonUI8_Modes.System)
                    Metro_LockScreenSystemID = rLog.GetValue("Metro_LockScreenSystemID", 0)

                    LogonUI7_ImagePath = rLog.GetValue("ImagePath", "")
                    LogonUI7_Color = Color.FromArgb(rLog.GetValue("Color", Color.Black.ToArgb))
                    LogonUI7_Effect_Blur = rLog.GetValue("Effect_Blur", False)
                    LogonUI7_Effect_Blur_Intensity = rLog.GetValue("Effect_Blur_Intensity", 0)
                    LogonUI7_Effect_Grayscale = rLog.GetValue("Effect_Grayscale", False)
                    LogonUI7_Effect_Noise = rLog.GetValue("Effect_Noise", False)
                    LogonUI7_Effect_Noise_Mode = rLog.GetValue("Noise_Mode", LogonUI7_NoiseMode.Acrylic)
                    LogonUI7_Effect_Noise_Intensity = rLog.GetValue("Effect_Noise_Intensity", 0)
                    rLog.Close()
                Else
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

                Win32UI_EnableTheming = GetUserPreferencesMask(17)
                Win32UI_EnableGradient = GetUserPreferencesMask(4)

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

                With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "ActiveBorder", "244 247 252")
                    Win32UI_ActiveBorder = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
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
                    If Not My.W7 And Not My.W8 Then

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
                    End If
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
                    If lin.StartsWith("*LogonUI_Background= ") Then LogonUI_Background = Color.FromArgb(lin.Remove(0, "*LogonUI_Background= ".Count))
                    If lin.StartsWith("*LogonUI_PersonalColors_Background= ") Then LogonUI_PersonalColors_Background = Color.FromArgb(lin.Remove(0, "*LogonUI_PersonalColors_Background= ".Count))
                    If lin.StartsWith("*LogonUI_PersonalColors_Accent= ") Then LogonUI_PersonalColors_Accent = Color.FromArgb(lin.Remove(0, "*LogonUI_PersonalColors_Accent= ".Count))
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
                WinMode_Light = False
                AppMode_Light = False
                Transparency = True
                ApplyAccentonTitlebars = False
                ApplyAccentonTaskbar = False
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
                Metro_LockScreenType = LogonUI8_Modes.System
                Metro_LockScreenSystemID = 0
#End Region

#Region "LogonUI"
                LogonUI_Background = Color.Black
                LogonUI_PersonalColors_Background = Color.Black
                LogonUI_PersonalColors_Accent = Color.Black
                LogonUI_DisableAcrylicBackgroundOnLogon = False
                LogonUI_DisableLogonBackgroundImage = False
                LogonUI_NoLockScreen = False
#End Region

#Region "LogonUI 7"
                LogonUI7_Enabled = False
                LogonUI7_Mode = LogonUI7_Modes.Default_
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


    Sub Save(ByVal [SaveTo] As SavingMode, Optional ByVal FileLocation As String = "")
        Select Case [SaveTo]
            Case SavingMode.Registry
#Region "Registry"

                If My.W7 Or My.W8 Then
                    th = New Thread(AddressOf Task_A)
                    th.Start()
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
                End If
#End Region

#Region "Aero"
                If My.W7 Then
                    SetCtrlTxt("Applying Theme ...", f)

                    Dim CWindows As String = Environment.GetFolderPath(Environment.SpecialFolder.Windows)

                    Select Case Aero_Theme
                        Case AeroTheme.Aero
                            EnableTheming(1)
                            SetSystemVisualStyle(CWindows & "\resources\Themes\Aero\Aero.msstyles", "NormalColor", "NormalSize", 0)

                            EditReg("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "CompositionPolicy", 2)
                            EditReg("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "Composition", 1)
                            EditReg("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationOpaqueBlend", 0)

                        Case AeroTheme.AeroOpaque
                            EnableTheming(1)
                            SetSystemVisualStyle(CWindows & "\resources\Themes\Aero\Aero.msstyles", "NormalColor", "NormalSize", 0)

                            EditReg("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "CompositionPolicy", 2)
                            EditReg("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "Composition", 1)
                            EditReg("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationOpaqueBlend", 1)

                        Case AeroTheme.Basic
                            EnableTheming(1)
                            SetSystemVisualStyle(CWindows & "\resources\Themes\Aero\Aero.msstyles", "NormalColor", "NormalSize", 0)

                            EditReg("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "CompositionPolicy", 1)
                            EditReg("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "Composition", 0)
                            EditReg("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationOpaqueBlend", 0)


                        Case AeroTheme.Classic
                            EnableTheming(0)

                    End Select

                    SetCtrlTxt("Applying Colors and Tweaks ...", f)

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
                    SetCtrlTxt("Applying Theme ...", f)

                    EditReg("HKEY_CURRENT_USER\Control Panel\Desktop", "AutoColorization", 0)

                    Try
                        Select Case Metro_Theme
                            Case AeroTheme.Aero
                                EnableTheming(1)
                                SetSystemVisualStyle("C:\WINDOWS\resources\Themes\Aero\Aero.msstyles", "NormalColor", "NormalSize", 0)
                            Case AeroTheme.AeroLite
                                EnableTheming(1)
                                SetSystemVisualStyle("C:\WINDOWS\resources\Themes\Aero\AeroLite.msstyles", "NormalColor", "NormalSize", 0)
                        End Select
                    Catch
                    End Try

                    SetCtrlTxt("Applying Colors and Tweaks ...", f)

                    EditReg("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationColor", Aero_ColorizationColor.ToArgb)
                    EditReg("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationColorBalance", Aero_ColorizationColorBalance)

                    EditReg("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "StartColor", BizareColorInvertor(Metro_StartColor).ToArgb)
                    EditReg("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "DefaultStartColor", BizareColorInvertor(Metro_StartColor).ToArgb)
                    EditReg("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "AccentColor", BizareColorInvertor(Metro_AccentColor).ToArgb)
                    EditReg("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "DefaultColorSet", Metro_LogonUI)


                    If isElevated Then
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
                        ls.Add(String.Format("""PersonalColors_Background""=#{0}", "#" & RGB2HEX_oneline(Metro_PersonalColors_Background, False)))
                        ls.Add(String.Format("""PersonalColors_Accent""=#{0}", "#" & RGB2HEX_oneline(Metro_PersonalColors_Accent, False)))
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
                    SetCtrlTxt("Applying Custom LogonUI ...", f)

                    If isElevated Then

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
                        Case LogonUI7_Modes.Default_
                            rLog.SetValue("Mode", 0)

                        Case LogonUI7_Modes.Wallpaper
                            rLog.SetValue("Mode", 1)

                        Case LogonUI7_Modes.CustomImage
                            rLog.SetValue("Mode", 2)

                        Case LogonUI7_Modes.SolidColor
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
                        Dim Dir As String = Environment.GetFolderPath(Environment.SpecialFolder.Windows) & "\system32\oobe\info\backgrounds"
                        Dim imageres As String = Environment.GetFolderPath(Environment.SpecialFolder.Windows) & "\system32\imageres.dll"

                        If Not IO.Directory.Exists(Dir) Then IO.Directory.CreateDirectory(Dir)

                        For Each fileX As String In My.Computer.FileSystem.GetFiles(Dir)
                            Try : Kill(fileX) : Catch : End Try
                        Next

                        Dim bmpList As New List(Of Bitmap)
                        bmpList.Clear()

                        Select Case LogonUI7_Mode
                            Case LogonUI7_Modes.Default_

                                For i As Integer = 5031 To 5043 Step +1
                                    bmpList.Add(LoadFromDLL(imageres, i, "IMAGE", My.Computer.Screen.Bounds.Size.Width, My.Computer.Screen.Bounds.Size.Height))
                                Next

                            Case LogonUI7_Modes.CustomImage

                                If IO.File.Exists(LogonUI7_ImagePath) Then
                                    bmpList.Add(Image.FromStream(New FileStream(LogonUI7_ImagePath, IO.FileMode.Open, IO.FileAccess.Read)))
                                Else
                                    bmpList.Add(ColorToBitmap(Color.Black, My.Computer.Screen.Bounds.Size))
                                End If

                            Case LogonUI7_Modes.SolidColor
                                bmpList.Add(ColorToBitmap(LogonUI7_Color, My.Computer.Screen.Bounds.Size))

                            Case LogonUI7_Modes.Wallpaper
                                bmpList.Add(My.Application.GetCurrentWallpaper)
                        End Select


                        For x = 0 To bmpList.Count - 1
                            SetCtrlTxt(String.Format("Rendering Custom LogonUI: {2} " & vbCrLf & "({0}/{1}) ...", x + 1, bmpList.Count, bmpList(x).Width & "x" & bmpList(x).Height), f)
                            If LogonUI7_Effect_Grayscale Then bmpList(x) = Grayscale(bmpList(x))
                            If LogonUI7_Effect_Blur Then bmpList(x) = BlurBitmap(bmpList(x), LogonUI7_Effect_Blur_Intensity)
                            If LogonUI7_Effect_Noise Then bmpList(x) = NoiseBitmap(bmpList(x), LogonUI7_Effect_Noise_Mode, LogonUI7_Effect_Noise_Intensity / 100)
                        Next

                        If bmpList.Count = 1 Then
                            bmpList(0).Save(Dir & "\backgroundDefault.jpg", ImageFormat.Jpeg)
                        Else
                            For x = 0 To bmpList.Count - 1
                                bmpList(x).Save(Dir & String.Format("\background{0}x{1}.jpg", bmpList(x).Width, bmpList(x).Height), ImageFormat.Jpeg)
                            Next
                        End If

                    End If

                End If
#End Region

#Region "LogonUI 8"
                If My.W8 Then
                    SetCtrlTxt("Applying Custom LogonUI ...", f)

                    Dim lockimg As String = My.Application.appData & "\LockScreen.png"

                    If isElevated Then
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
                        Case LogonUI8_Modes.System
                            rLog.SetValue("Mode", 0)

                        Case LogonUI8_Modes.Wallpaper
                            rLog.SetValue("Mode", 1)

                        Case LogonUI8_Modes.CustomImage
                            rLog.SetValue("Mode", 2)

                        Case LogonUI8_Modes.SolidColor
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

                            Case LogonUI8_Modes.System
                                Dim syslock As String
                                If Not MainFrm.CP.Metro_LockScreenSystemID = 1 And Not MainFrm.CP.Metro_LockScreenSystemID = 3 Then
                                    syslock = String.Format(Environment.GetFolderPath(Environment.SpecialFolder.Windows) & "\Web\Screen\img10{0}.jpg", MainFrm.CP.Metro_LockScreenSystemID)
                                Else
                                    syslock = String.Format(Environment.GetFolderPath(Environment.SpecialFolder.Windows) & "\Web\Screen\img10{0}.png", MainFrm.CP.Metro_LockScreenSystemID)
                                End If

                                If File.Exists(syslock) Then
                                    bmp = Image.FromStream(New FileStream(syslock, IO.FileMode.Open, IO.FileAccess.Read))
                                Else
                                    bmp = ColorToBitmap(Color.Black, My.Computer.Screen.Bounds.Size)
                                End If

                            Case LogonUI7_Modes.CustomImage

                                If IO.File.Exists(LogonUI7_ImagePath) Then
                                    bmp = Image.FromStream(New FileStream(LogonUI7_ImagePath, IO.FileMode.Open, IO.FileAccess.Read))
                                Else
                                    bmp = ColorToBitmap(Color.Black, My.Computer.Screen.Bounds.Size)
                                End If

                            Case LogonUI7_Modes.SolidColor
                                bmp = ColorToBitmap(LogonUI7_Color, My.Computer.Screen.Bounds.Size)

                            Case LogonUI7_Modes.Wallpaper
                                bmp = My.Application.GetCurrentWallpaper
                        End Select

                        SetCtrlTxt("Rendering Custom LogonUI ...", f)

                        If LogonUI7_Effect_Grayscale Then bmp = Grayscale(bmp)
                        If LogonUI7_Effect_Blur Then bmp = BlurBitmap(bmp, LogonUI7_Effect_Blur_Intensity)
                        If LogonUI7_Effect_Noise Then bmp = NoiseBitmap(bmp, LogonUI7_Effect_Noise_Mode, LogonUI7_Effect_Noise_Intensity / 100)
                        bmp.Save(lockimg, ImageFormat.Png)
                    End If

                End If
#End Region

#Region "Win32UI"
                If My.W7 Or My.W8 Then SetCtrlTxt("Applying Win32UI Colors (Classic Windows Elements) ...", f)

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


                My.Computer.Registry.SetValue("HKEY_CURRENT_USER\Control Panel\Desktop", "UserPreferencesMask", SetUserPreferenceMask(17, Win32UI_EnableTheming), RegistryValueKind.Binary)
                My.Computer.Registry.SetValue("HKEY_CURRENT_USER\Control Panel\Desktop", "UserPreferencesMask", SetUserPreferenceMask(4, Win32UI_EnableGradient), RegistryValueKind.Binary)

                SystemParametersInfo(&H1023, 0, If(Win32UI_EnableTheming, 1, 0), 0)
                SystemParametersInfo(&H1009, 0, If(Win32UI_EnableGradient, 1, 0), 0)

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

#Region "Cursors"
                If My.W7 Or My.W8 Then SetCtrlTxt("Saving Cursors Colors ...", f)

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
                    If My.W7 Or My.W8 Then SetCtrlTxt("Rendering Cursors ...", f)
                    ExportCursors(MainFrm.CP)
                    If My.Application._Settings.AutoApplyCursors Then
                        If My.W7 Or My.W8 Then SetCtrlTxt("Applying Cursors ...", f)
                        ApplyCursorsToReg()
                    End If
                End If
#End Region

                If My.W7 Or My.W8 Then f.BeginInvoke(New Action(Sub() f.Close()))
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
                tx.Add("*Undefined= " & Color.FromArgb(255, 0, 0, 0).ToArgb)
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
                tx.Add("*LogonUI_Background= " & LogonUI_Background.ToArgb)
                tx.Add("*LogonUI_PersonalColors_Background= " & LogonUI_PersonalColors_Background.ToArgb)
                tx.Add("*LogonUI_PersonalColors_Accent= " & LogonUI_PersonalColors_Accent.ToArgb)
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

            Dim fs As New FileStream(Path, FileMode.Create)
            Dim EO As New EOIcoCurWriter(fs, 7, EOIcoCurWriter.IcoCurType.Cursor)

            For i As Single = 1 To 4 Step 0.5
                Dim bmp As New Bitmap(32 * i, 32 * i, PixelFormat.Format32bppPArgb)
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
                Dim fs As New FileStream(String.Format(My.Application.curPath & "\{0}_{1}x.ani", CurName, i), FileMode.Create)

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
        SetSystemCursor(LoadCursorFromFile(x), OCR_SYSTEM_CURSORS.OCR_APPSTARTING)

        x = String.Format("{0}\{1}", Path, "Arrow.cur")
        rMain.SetValue("Arrow", x)
        SetSystemCursor(LoadCursorFromFile(x), OCR_SYSTEM_CURSORS.OCR_NORMAL)

        x = String.Format("{0}\{1}", Path, "Cross.cur")
        rMain.SetValue("Crosshair", x)
        SetSystemCursor(LoadCursorFromFile(x), OCR_SYSTEM_CURSORS.OCR_CROSS)

        x = String.Format("{0}\{1}", Path, "Link.cur")
        rMain.SetValue("Hand", x)
        SetSystemCursor(LoadCursorFromFile(x), OCR_SYSTEM_CURSORS.OCR_HAND)

        x = String.Format("{0}\{1}", Path, "Help.cur")
        rMain.SetValue("Help", x)
        SetSystemCursor(LoadCursorFromFile(x), OCR_SYSTEM_CURSORS.OCR_HELP)

        x = String.Format("{0}\{1}", Path, "IBeam.cur")
        rMain.SetValue("IBeam", x)
        SetSystemCursor(LoadCursorFromFile(x), OCR_SYSTEM_CURSORS.OCR_IBEAM)

        x = String.Format("{0}\{1}", Path, "None.cur")
        rMain.SetValue("No", x)
        SetSystemCursor(LoadCursorFromFile(x), OCR_SYSTEM_CURSORS.OCR_NO)

        x = String.Format("{0}\{1}", Path, "Pen.cur")
        rMain.SetValue("NWPen", x)
        'SetSystemCursor(LoadCursorFromFile(x), OCR_SYSTEM_CURSORS.OCR_)

        x = String.Format("{0}\{1}", Path, "Person.cur")
        rMain.SetValue("Person", x)
        'SetSystemCursor(LoadCursorFromFile(x), OCR_SYSTEM_CURSORS.OCR_APPSTARTING)

        x = String.Format("{0}\{1}", Path, "Pin.cur")
        rMain.SetValue("Pin", x)
        'SetSystemCursor(LoadCursorFromFile(x), OCR_SYSTEM_CURSORS.OCR_APPSTARTING)

        x = String.Format("{0}\{1}", Path, "Move.cur")
        rMain.SetValue("SizeAll", x)
        SetSystemCursor(LoadCursorFromFile(x), OCR_SYSTEM_CURSORS.OCR_SIZEALL)

        x = String.Format("{0}\{1}", Path, "NESW.cur")
        rMain.SetValue("SizeNESW", x)
        SetSystemCursor(LoadCursorFromFile(x), OCR_SYSTEM_CURSORS.OCR_SIZENESW)

        x = String.Format("{0}\{1}", Path, "NS.cur")
        rMain.SetValue("SizeNS", x)
        SetSystemCursor(LoadCursorFromFile(x), OCR_SYSTEM_CURSORS.OCR_SIZENS)

        x = String.Format("{0}\{1}", Path, "NWSE.cur")
        rMain.SetValue("SizeNWSE", x)
        SetSystemCursor(LoadCursorFromFile(x), OCR_SYSTEM_CURSORS.OCR_SIZENWSE)

        x = String.Format("{0}\{1}", Path, "EW.cur")
        rMain.SetValue("SizeWE", x)
        SetSystemCursor(LoadCursorFromFile(x), OCR_SYSTEM_CURSORS.OCR_SIZEWE)

        x = String.Format("{0}\{1}", Path, "Up.cur")
        rMain.SetValue("UpArrow", x)
        SetSystemCursor(LoadCursorFromFile(x), OCR_SYSTEM_CURSORS.OCR_UP)

        x = String.Format("{0}\{1}", Path, "Busy_1x.ani")
        rMain.SetValue("Wait", x)
        SetSystemCursor(LoadCursorFromFile(x), OCR_SYSTEM_CURSORS.OCR_WAIT)

        rMain.SetValue("Scheme Source", 1, RegistryValueKind.DWord)
        rMain.Close()
    End Sub

    <DllImport("user32.dll")>
    Private Shared Function SetSystemCursor(ByVal hcur As IntPtr, ByVal id As Integer) As Boolean
    End Function

    Declare Function LoadCursorFromFile Lib "user32.dll" Alias "LoadCursorFromFileA" (ByVal lpFileName As String) As IntPtr

    Enum OCR_SYSTEM_CURSORS As Integer

        ' Standard arrow And small hourglass
        OCR_APPSTARTING = 32650

        'Standard arrow
        OCR_NORMAL = 32512

        'Crosshair
        OCR_CROSS = 32515

        'Windows 2000/XP: Hand
        OCR_HAND = 32649

        'Arrow And question mark
        OCR_HELP = 32651

        'I-beam
        OCR_IBEAM = 32513

        'Slashed circle
        OCR_NO = 32648

        'Four-pointed arrow pointing north south east And west
        OCR_SIZEALL = 32646

        'Double-pointed arrow pointing northeast And southwest
        OCR_SIZENESW = 32643

        'Double-pointed arrow pointing north And south
        OCR_SIZENS = 32645

        'Double-pointed arrow pointing northwest And southeast
        OCR_SIZENWSE = 32642

        'Double-pointed arrow pointing west And east
        OCR_SIZEWE = 32644

        'Vertical arrow
        OCR_UP = 32516

        'Hourglass
        OCR_WAIT = 32514
    End Enum
#End Region

    Public Overrides Function Equals(obj As Object) As Boolean
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

Public Class CP_Defaults

    Public Default_Windows11 As New CP(CP.Mode.Init) With {
        .AppVersion = My.Application.Info.Version.ToString,
        .PaletteName = "Windows 11 (Initial)",
        .PaletteDescription = "Initial; Like first time after Windows Setup",
        .PaletteVersion = "1.0.0.0",
        .Author = "Microsoft",
        .AuthorSocialMediaLink = "www.microsoft.com",
        .WinMode_Light = True, .AppMode_Light = True, .Transparency = True, .ApplyAccentonTitlebars = False, .ApplyAccentonTaskbar = False,
        .Titlebar_Active = Color.FromArgb(0, 120, 212), .Titlebar_DWM_Active = Color.FromArgb(0, 120, 212), .Titlebar_Inactive = Color.FromArgb(35, 35, 35),
        .ActionCenter_AppsLinks = Color.FromArgb(153, 235, 255), .Taskbar_Icon_Underline = Color.FromArgb(76, 194, 255), .StartButton_Hover = Color.FromArgb(0, 145, 248),
        .SettingsIconsAndLinks = Color.FromArgb(0, 120, 212), .StartMenuBackground_ActiveTaskbarButton = Color.FromArgb(0, 103, 192), .StartListFolders_TaskbarFront = Color.FromArgb(0, 62, 146),
        .Taskbar_Background = Color.FromArgb(0, 26, 104), .StartMenu_Accent = Color.FromArgb(0, 103, 192),
        .LogonUI_Background = Color.FromArgb(0, 0, 0), .LogonUI_PersonalColors_Background = Color.FromArgb(0, 0, 0), .LogonUI_PersonalColors_Accent = Color.FromArgb(0, 0, 0),
        .LogonUI_DisableAcrylicBackgroundOnLogon = False, .LogonUI_DisableLogonBackgroundImage = False, .LogonUI_NoLockScreen = False
       }

    Public Default_Windows10 As New CP(CP.Mode.Init) With {
        .AppVersion = My.Application.Info.Version.ToString,
        .PaletteName = "Windows 10 (Initial)",
        .PaletteDescription = "Initial; Like first time after Windows Setup",
        .PaletteVersion = "1.0.0.0",
        .Author = "Microsoft",
        .AuthorSocialMediaLink = "www.microsoft.com",
        .WinMode_Light = False, .AppMode_Light = True, .Transparency = True, .ApplyAccentonTitlebars = False, .ApplyAccentonTaskbar = False,
        .Titlebar_Active = Color.FromArgb(0, 120, 215), .Titlebar_DWM_Active = Color.FromArgb(0, 120, 215), .Titlebar_Inactive = Color.FromArgb(35, 35, 35),
        .ActionCenter_AppsLinks = Color.FromArgb(166, 216, 255), .Taskbar_Icon_Underline = Color.FromArgb(118, 185, 237), .StartButton_Hover = Color.FromArgb(66, 156, 227),
        .SettingsIconsAndLinks = Color.FromArgb(0, 120, 215), .StartMenuBackground_ActiveTaskbarButton = Color.FromArgb(0, 90, 158), .StartListFolders_TaskbarFront = Color.FromArgb(0, 66, 117),
        .Taskbar_Background = Color.FromArgb(0, 38, 66), .StartMenu_Accent = Color.FromArgb(0, 90, 158),
        .LogonUI_Background = Color.FromArgb(0, 0, 0), .LogonUI_PersonalColors_Background = Color.FromArgb(0, 0, 0), .LogonUI_PersonalColors_Accent = Color.FromArgb(0, 0, 0),
        .LogonUI_DisableAcrylicBackgroundOnLogon = False, .LogonUI_DisableLogonBackgroundImage = False, .LogonUI_NoLockScreen = False
       }

    Public Default_Windows8 As New CP(CP.Mode.Init) With {
        .AppVersion = My.Application.Info.Version.ToString,
        .PaletteName = "Windows 8.1 (Initial)",
        .PaletteDescription = "Initial; Like first time after Windows Setup",
        .PaletteVersion = "1.0.0.0",
        .Author = "Microsoft",
        .AuthorSocialMediaLink = "www.microsoft.com",
        .Aero_ColorizationColor = Color.FromArgb(246, 195, 74),
        .Aero_ColorizationAfterglow = Color.FromArgb(0, 0, 0),
        .Aero_ColorizationColorBalance = 78,
        .Aero_ColorizationAfterglowBalance = 31,
        .Aero_ColorizationBlurBalance = 31,
        .Aero_ColorizationGlassReflectionIntensity = 0,
        .Aero_EnableAeroPeek = True,
        .Aero_AlwaysHibernateThumbnails = False,
        .Metro_PersonalColors_Background = Color.FromArgb(30, 0, 84),
        .Metro_PersonalColors_Accent = Color.FromArgb(72, 29, 178),
        .Metro_StartColor = Color.FromArgb(30, 0, 84),
        .Metro_AccentColor = Color.FromArgb(72, 29, 178),
        .Metro_Start = 0,
        .Metro_Theme = CP.AeroTheme.Aero,
        .Metro_LogonUI = 0,
        .Metro_NoLockScreen = False,
        .Metro_LockScreenType = CP.LogonUI8_Modes.System,
        .Metro_LockScreenSystemID = 0,
        .LogonUI7_Enabled = False,
        .LogonUI7_ImagePath = "",
        .LogonUI7_Color = Color.FromArgb(0, 0, 0),
        .LogonUI7_Effect_Blur = False,
        .LogonUI7_Effect_Blur_Intensity = 0,
        .LogonUI7_Effect_Grayscale = False,
        .LogonUI7_Effect_Noise = False,
        .LogonUI7_Effect_Noise_Mode = CP.LogonUI7_NoiseMode.Acrylic,
        .LogonUI7_Effect_Noise_Intensity = 0
       }

    Public Default_Windows7 As New CP(CP.Mode.Init) With {
        .AppVersion = My.Application.Info.Version.ToString,
        .PaletteName = "Windows 7 (Initial)",
        .PaletteDescription = "Initial; Like first time after Windows Setup",
        .PaletteVersion = "1.0.0.0",
        .Author = "Microsoft",
        .AuthorSocialMediaLink = "www.microsoft.com",
        .Aero_ColorizationColor = Color.FromArgb(116, 184, 252),
        .Aero_ColorizationAfterglow = Color.FromArgb(116, 184, 252),
        .Aero_ColorizationColorBalance = 8,
        .Aero_ColorizationAfterglowBalance = 43,
        .Aero_ColorizationBlurBalance = 49,
        .Aero_ColorizationGlassReflectionIntensity = 0,
        .Aero_EnableAeroPeek = True,
        .Aero_AlwaysHibernateThumbnails = False,
        .Aero_Theme = CP.AeroTheme.Aero,
        .LogonUI7_Enabled = False,
        .LogonUI7_Mode = CP.LogonUI7_Modes.Default_,
        .LogonUI7_ImagePath = "",
        .LogonUI7_Color = Color.FromArgb(0, 0, 0),
        .LogonUI7_Effect_Blur = False,
        .LogonUI7_Effect_Blur_Intensity = 0,
        .LogonUI7_Effect_Grayscale = False,
        .LogonUI7_Effect_Noise = False,
        .LogonUI7_Effect_Noise_Mode = CP.LogonUI7_NoiseMode.Acrylic,
        .LogonUI7_Effect_Noise_Intensity = 0
       }

    Public Default_Windows11Accents_Bytes As Byte() = {Default_Windows11.ActionCenter_AppsLinks.R, Default_Windows11.ActionCenter_AppsLinks.G, Default_Windows11.ActionCenter_AppsLinks.B, 255,
                                                    Default_Windows11.Taskbar_Icon_Underline.R, Default_Windows11.Taskbar_Icon_Underline.G, Default_Windows11.Taskbar_Icon_Underline.B, 255,
                                                    Default_Windows11.StartButton_Hover.R, Default_Windows11.StartButton_Hover.G, Default_Windows11.StartButton_Hover.B, 255,
                                                    Default_Windows11.SettingsIconsAndLinks.R, Default_Windows11.SettingsIconsAndLinks.G, Default_Windows11.SettingsIconsAndLinks.B, 255,
                                                    Default_Windows11.StartMenuBackground_ActiveTaskbarButton.R, Default_Windows11.StartMenuBackground_ActiveTaskbarButton.G, Default_Windows11.StartMenuBackground_ActiveTaskbarButton.B, 255,
                                                    Default_Windows11.StartListFolders_TaskbarFront.R, Default_Windows11.StartListFolders_TaskbarFront.G, Default_Windows11.StartListFolders_TaskbarFront.B, 255,
                                                    Default_Windows11.Taskbar_Background.R, Default_Windows11.Taskbar_Background.G, Default_Windows11.Taskbar_Background.B, 255,
                                                    0, 0, 0, 255}

    Public Default_Windows10Accents_Bytes As Byte() = {Default_Windows10.ActionCenter_AppsLinks.R, Default_Windows10.ActionCenter_AppsLinks.G, Default_Windows10.ActionCenter_AppsLinks.B, 255,
                                                    Default_Windows10.Taskbar_Icon_Underline.R, Default_Windows10.Taskbar_Icon_Underline.G, Default_Windows10.Taskbar_Icon_Underline.B, 255,
                                                    Default_Windows10.StartButton_Hover.R, Default_Windows10.StartButton_Hover.G, Default_Windows10.StartButton_Hover.B, 255,
                                                    Default_Windows10.SettingsIconsAndLinks.R, Default_Windows10.SettingsIconsAndLinks.G, Default_Windows10.SettingsIconsAndLinks.B, 255,
                                                    Default_Windows10.StartMenuBackground_ActiveTaskbarButton.R, Default_Windows10.StartMenuBackground_ActiveTaskbarButton.G, Default_Windows10.StartMenuBackground_ActiveTaskbarButton.B, 255,
                                                    Default_Windows10.StartListFolders_TaskbarFront.R, Default_Windows10.StartListFolders_TaskbarFront.G, Default_Windows10.StartListFolders_TaskbarFront.B, 255,
                                                    Default_Windows10.Taskbar_Background.R, Default_Windows10.Taskbar_Background.G, Default_Windows10.Taskbar_Background.B, 255,
                                                    0, 0, 0, 255}

End Class