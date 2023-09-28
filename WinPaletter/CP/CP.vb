Imports System.IO
Imports System.IO.Compression
Imports System.Reflection
Imports System.Runtime.InteropServices
Imports Devcorp.Controls.VisualStyles
Imports Microsoft.Win32
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports WinPaletter.Core
Imports WinPaletter.CP.Structures
Imports WinPaletter.Metrics
Imports WinPaletter.NativeMethods
Imports WinPaletter.NativeMethods.User32
Imports WinPaletter.PreviewHelpers
Imports WinPaletter.Reg_IO

Public Class CP : Implements IDisposable : Implements ICloneable

    Private _ErrorHappened As Boolean = False
    Private ReadOnly bindingFlags As BindingFlags = BindingFlags.Instance Or BindingFlags.Public
    Private ReadOnly _Converter As New Converter

#Region "IDisposable Support"
    Private disposedValue As Boolean

    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If
        End If
        Me.disposedValue = True
    End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

    Public Function Clone() Implements ICloneable.Clone
        Return MemberwiseClone()
    End Function

    Enum CP_Type
        Registry
        File
        Empty
    End Enum

    Public Class Structures
        Structure Info : Implements ICloneable
            Public AppVersion As String
            Public ThemeName As String
            Public Description As String
            Public ExportResThemePack As Boolean
            Public License As String
            Public ThemeVersion As String
            Public Author As String
            Public AuthorSocialMediaLink As String
            Public Color1 As Color
            Public Color2 As Color
            Public Pattern As Integer
            Public DesignedFor_Win11 As Boolean
            Public DesignedFor_Win10 As Boolean
            Public DesignedFor_Win81 As Boolean
            Public DesignedFor_Win7 As Boolean
            Public DesignedFor_WinVista As Boolean
            Public DesignedFor_WinXP As Boolean

            Public Sub Load()
                ThemeName = GetReg("HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo", "ThemeName", My.Lang.CurrentMode)
                ThemeVersion = GetReg("HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo", "ThemeVersion", "1.0")
                Author = GetReg("HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo", "Author", Environment.UserName)
                AuthorSocialMediaLink = GetReg("HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo", "AuthorSocialMediaLink", "")
                AppVersion = GetReg("HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo", "AppVersion", My.AppVersion)
                Description = GetReg("HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo", "Description", "")
                ExportResThemePack = GetReg("HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo", "ExportResThemePack", False)
                License = GetReg("HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo", "License", "")
                Dim y As Object = GetReg("HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo\Store", "Color1", Color.FromArgb(0, 102, 204).ToArgb)
                Color1 = Color.FromArgb(y)

                y = GetReg("HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo\Store", "Color2", Color.FromArgb(122, 9, 9).ToArgb)
                Color2 = Color.FromArgb(y)

                Pattern = GetReg("HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo\Store", "Pattern", 1)
                DesignedFor_Win11 = GetReg("HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo\Store", "DesignedFor_Win11", True)
                DesignedFor_Win10 = GetReg("HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo\Store", "DesignedFor_Win10", True)
                DesignedFor_Win81 = GetReg("HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo\Store", "DesignedFor_Win8.1", True)
                DesignedFor_Win7 = GetReg("HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo\Store", "DesignedFor_Win7", True)
                DesignedFor_WinVista = GetReg("HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo\Store", "DesignedFor_WinVista", True)
                DesignedFor_WinXP = GetReg("HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo\Store", "DesignedFor_WinXP", True)
            End Sub
            Sub Apply(Optional [TreeView] As TreeView = Nothing)
                EditReg([TreeView], "HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo", "ThemeName", ThemeName, RegistryValueKind.String)
                EditReg([TreeView], "HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo", "ThemeVersion", ThemeVersion, RegistryValueKind.String)
                EditReg([TreeView], "HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo", "Author", Author, RegistryValueKind.String)
                EditReg([TreeView], "HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo", "AuthorSocialMediaLink", AuthorSocialMediaLink, RegistryValueKind.String)
                EditReg([TreeView], "HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo", "AppVersion", My.AppVersion, RegistryValueKind.String)
                EditReg([TreeView], "HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo", "Description", Description, RegistryValueKind.String)
                EditReg([TreeView], "HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo", "ExportResThemePack", ExportResThemePack, RegistryValueKind.DWord)
                EditReg([TreeView], "HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo", "License", License, RegistryValueKind.String)

                EditReg([TreeView], "HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo\Store", "Color1", Color1.ToArgb, RegistryValueKind.DWord)
                EditReg([TreeView], "HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo\Store", "Color2", Color2.ToArgb, RegistryValueKind.DWord)
                EditReg([TreeView], "HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo\Store", "Pattern", Pattern, RegistryValueKind.DWord)
                EditReg([TreeView], "HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo\Store", "DesignedFor_Win11", DesignedFor_Win11.ToInteger, RegistryValueKind.DWord)
                EditReg([TreeView], "HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo\Store", "DesignedFor_Win10", DesignedFor_Win10.ToInteger, RegistryValueKind.DWord)
                EditReg([TreeView], "HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo\Store", "DesignedFor_Win8.1", DesignedFor_Win81.ToInteger, RegistryValueKind.DWord)
                EditReg([TreeView], "HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo\Store", "DesignedFor_Win7", DesignedFor_Win7.ToInteger, RegistryValueKind.DWord)
                EditReg([TreeView], "HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo\Store", "DesignedFor_WinVista", DesignedFor_WinVista.ToInteger, RegistryValueKind.DWord)
                EditReg([TreeView], "HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo\Store", "DesignedFor_WinXP", DesignedFor_WinXP.ToInteger, RegistryValueKind.DWord)
            End Sub

            Shared Operator =(First As Info, Second As Info) As Boolean
                Return First.Equals(Second)
            End Operator

            Shared Operator <>(First As Info, Second As Info) As Boolean
                Return Not First.Equals(Second)
            End Operator
            Public Function Clone() Implements ICloneable.Clone
                Return MemberwiseClone()
            End Function

        End Structure
        Structure AppTheme : Implements ICloneable
            Public Enabled As Boolean
            Public BackColor As Color
            Public AccentColor As Color
            Public DarkMode As Boolean
            Public RoundCorners As Boolean

            Sub Load(_DefAppTheme As AppTheme)
                Enabled = GetReg("HKEY_CURRENT_USER\Software\WinPaletter\Settings", "Appearance_Custom", _DefAppTheme.Enabled)
                BackColor = Color.FromArgb(GetReg("HKEY_CURRENT_USER\Software\WinPaletter\Settings", ".BackColor", _DefAppTheme.BackColor.ToArgb))
                AccentColor = Color.FromArgb(GetReg("HKEY_CURRENT_USER\Software\WinPaletter\Settings", "AccentColor", _DefAppTheme.AccentColor.ToArgb))
                DarkMode = GetReg("HKEY_CURRENT_USER\Software\WinPaletter\Settings", "Custom_Dark", _DefAppTheme.DarkMode)
                RoundCorners = GetReg("HKEY_CURRENT_USER\Software\WinPaletter\Settings", "RoundedCorners", _DefAppTheme.RoundCorners)
            End Sub
            Sub Apply(Optional [TreeView] As TreeView = Nothing)
                EditReg([TreeView], "HKEY_CURRENT_USER\Software\WinPaletter\Settings", "Appearance_Custom", Enabled)
                EditReg([TreeView], "HKEY_CURRENT_USER\Software\WinPaletter\Settings", ".BackColor", BackColor.ToArgb)
                EditReg([TreeView], "HKEY_CURRENT_USER\Software\WinPaletter\Settings", "AccentColor", AccentColor.ToArgb)
                EditReg([TreeView], "HKEY_CURRENT_USER\Software\WinPaletter\Settings", "Custom_Dark", DarkMode)
                EditReg([TreeView], "HKEY_CURRENT_USER\Software\WinPaletter\Settings", "RoundedCorners", RoundCorners)

                With My.Settings.Appearance
                    .CustomColors = Enabled
                    .BackColor = BackColor
                    .AccentColor = AccentColor
                    .CustomTheme = DarkMode
                    .RoundedCorners = RoundCorners
                End With

                ApplyStyle()
            End Sub

            Shared Operator =(First As AppTheme, Second As AppTheme) As Boolean
                Return First.Equals(Second)
            End Operator

            Shared Operator <>(First As AppTheme, Second As AppTheme) As Boolean
                Return Not First.Equals(Second)
            End Operator
            Public Function Clone() As Object Implements ICloneable.Clone
                Return MemberwiseClone()
            End Function
        End Structure
        Structure Windows10x : Implements ICloneable
            Public Color_Index0 As Color
            Public Color_Index1 As Color
            Public Color_Index2 As Color
            Public Color_Index3 As Color
            Public Color_Index4 As Color
            Public Color_Index5 As Color
            Public Color_Index6 As Color
            Public Color_Index7 As Color
            Public WinMode_Light As Boolean
            Public AppMode_Light As Boolean
            Public Transparency As Boolean
            Public Titlebar_Active As Color
            Public Titlebar_Inactive As Color
            Public StartMenu_Accent As Color
            Public ApplyAccentOnTitlebars As Boolean
            Public ApplyAccentOnTaskbar As AccentTaskbarLevels
            Public IncreaseTBTransparency As Boolean
            Public TB_Blur As Boolean

            Enum AccentTaskbarLevels
                None
                Taskbar_Start_AC
                Taskbar
            End Enum

            Sub Load(_DefWin As Windows10x, DefColorsBytes As Byte())
                If My.W10 Or My.W11 Or My.W12 Then
                    Dim Colors As New List(Of Color)
                    Colors.Clear()

                    Dim x As Byte()
                    Dim y As Object

                    x = GetReg("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "AccentPalette", DefColorsBytes)
                    Colors.Add(Color.FromArgb(255, x(0), x(1), x(2)))
                    Colors.Add(Color.FromArgb(255, x(4), x(5), x(6)))
                    Colors.Add(Color.FromArgb(255, x(8), x(9), x(10)))
                    Colors.Add(Color.FromArgb(255, x(12), x(13), x(14)))
                    Colors.Add(Color.FromArgb(255, x(16), x(17), x(18)))
                    Colors.Add(Color.FromArgb(255, x(20), x(21), x(22)))
                    Colors.Add(Color.FromArgb(255, x(24), x(25), x(26)))
                    Colors.Add(Color.FromArgb(255, x(28), x(29), x(30)))
                    Color_Index0 = Colors(0)
                    Color_Index1 = Colors(1)
                    Color_Index2 = Colors(2)
                    Color_Index3 = Colors(3)
                    Color_Index4 = Colors(4)
                    Color_Index5 = Colors(5)
                    Color_Index6 = Colors(6)
                    Color_Index7 = Colors(7)

                    y = GetReg("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "StartColorMenu", _DefWin.StartMenu_Accent.Reverse.ToArgb)
                    StartMenu_Accent = Color.FromArgb(y).Reverse

                    y = GetReg("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "AccentColorMenu", _DefWin.Titlebar_Active.Reverse.ToArgb)
                    Titlebar_Active = Color.FromArgb(y).Reverse

                    y = GetReg("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\DWM", "AccentColor", _DefWin.Titlebar_Active.Reverse.ToArgb)
                    Titlebar_Active = Color.FromArgb(y).Reverse

                    y = GetReg("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\DWM", "AccentColorInactive", _DefWin.Titlebar_Inactive.Reverse.ToArgb)
                    Titlebar_Inactive = Color.FromArgb(y).Reverse

                    WinMode_Light = GetReg("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "SystemUsesLightTheme", _DefWin.WinMode_Light)
                    AppMode_Light = GetReg("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "AppsUseLightTheme", _DefWin.AppMode_Light)
                    Transparency = GetReg("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "EnableTransparency", _DefWin.Transparency)
                    IncreaseTBTransparency = GetReg("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "UseOLEDTaskbarTransparency", _DefWin.IncreaseTBTransparency)

                    Select Case GetReg("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "ColorPrevalence", _DefWin.ApplyAccentOnTaskbar)
                        Case 0
                            ApplyAccentOnTaskbar = CP.Structures.Windows10x.AccentTaskbarLevels.None
                        Case 1
                            ApplyAccentOnTaskbar = CP.Structures.Windows10x.AccentTaskbarLevels.Taskbar_Start_AC
                        Case 2
                            ApplyAccentOnTaskbar = CP.Structures.Windows10x.AccentTaskbarLevels.Taskbar
                        Case Else
                            ApplyAccentOnTaskbar = CP.Structures.Windows10x.AccentTaskbarLevels.None
                    End Select

                    ApplyAccentOnTitlebars = GetReg("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\DWM", "ColorPrevalence", _DefWin.ApplyAccentOnTitlebars)
                    TB_Blur = Not CInt(GetReg("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\DWM", "ForceEffectMode", (Not _DefWin.TB_Blur).ToInteger)).ToBoolean

                Else
                    Color_Index0 = _DefWin.Color_Index0
                    Color_Index1 = _DefWin.Color_Index1
                    Color_Index2 = _DefWin.Color_Index2
                    Color_Index3 = _DefWin.Color_Index3
                    Color_Index4 = _DefWin.Color_Index4
                    Color_Index5 = _DefWin.Color_Index5
                    Color_Index6 = _DefWin.Color_Index6
                    StartMenu_Accent = _DefWin.StartMenu_Accent
                    Titlebar_Active = _DefWin.Titlebar_Active
                    Titlebar_Inactive = _DefWin.Titlebar_Inactive
                    WinMode_Light = _DefWin.WinMode_Light
                    AppMode_Light = _DefWin.AppMode_Light
                    Transparency = _DefWin.Transparency
                    ApplyAccentOnTaskbar = _DefWin.ApplyAccentOnTaskbar
                    ApplyAccentOnTitlebars = _DefWin.ApplyAccentOnTitlebars
                    IncreaseTBTransparency = _DefWin.IncreaseTBTransparency
                End If

            End Sub
            Sub Apply(Optional [TreeView] As TreeView = Nothing)
                EditReg([TreeView], "HKEY_CURRENT_USER\Control Panel\Desktop", "AutoColorization", 0)

                Dim Colors As Byte() = {Color_Index0.R, (Color_Index0).G, (Color_Index0).B, (Color_Index0).A _
                         , (Color_Index1).R, (Color_Index1).G, (Color_Index1).B, (Color_Index1).A _
                         , (Color_Index2).R, (Color_Index2).G, (Color_Index2).B, (Color_Index2).A _
                         , (Color_Index3).R, (Color_Index3).G, (Color_Index3).B, (Color_Index3).A _
                         , (Color_Index4).R, (Color_Index4).G, (Color_Index4).B, (Color_Index4).A _
                         , (Color_Index5).R, (Color_Index5).G, (Color_Index5).B, (Color_Index5).A _
                         , (Color_Index6).R, (Color_Index6).G, (Color_Index6).B, (Color_Index6).A _
                         , (Color_Index7).R, (Color_Index7).G, (Color_Index7).B, (Color_Index7).A}

                Select Case ApplyAccentOnTaskbar
                    Case CP.Structures.Windows10x.AccentTaskbarLevels.None
                        EditReg([TreeView], "HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "ColorPrevalence", 0)

                    Case CP.Structures.Windows10x.AccentTaskbarLevels.Taskbar_Start_AC
                        EditReg([TreeView], "HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "ColorPrevalence", 1)

                    Case CP.Structures.Windows10x.AccentTaskbarLevels.Taskbar
                        EditReg([TreeView], "HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "ColorPrevalence", 2)

                    Case Else
                        EditReg([TreeView], "HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "ColorPrevalence", 0)
                End Select

                EditReg([TreeView], "HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "AccentColorMenu", Titlebar_Active.ToArgb)
                EditReg([TreeView], "HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\DWM", "ColorPrevalence", ApplyAccentOnTitlebars.ToInteger)
                EditReg([TreeView], "HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "AccentPalette", Colors, RegistryValueKind.Binary)
                EditReg([TreeView], "HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "StartColorMenu", StartMenu_Accent.Reverse.ToArgb)
                EditReg([TreeView], "HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\DWM", "AccentColor", Titlebar_Active.ToArgb)


                EditReg([TreeView], "HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "AccentColorMenu", Titlebar_Active.Reverse.ToArgb)
                EditReg([TreeView], "HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\DWM", "AccentColor", Titlebar_Active.Reverse.ToArgb)
                EditReg([TreeView], "HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\DWM", "AccentColorInactive", Titlebar_Inactive.Reverse.ToArgb)

                EditReg([TreeView], "HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "SystemUsesLightTheme", WinMode_Light.ToInteger)
                EditReg([TreeView], "HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "AppsUseLightTheme", AppMode_Light.ToInteger)
                EditReg([TreeView], "HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "EnableTransparency", Transparency.ToInteger)

                If My.W10 Then
                    EditReg([TreeView], "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "UseOLEDTaskbarTransparency", IncreaseTBTransparency.ToInteger)
                    EditReg([TreeView], "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\DWM", "ForceEffectMode", (Not TB_Blur).ToInteger)
                End If

            End Sub

            Shared Operator =(First As Windows10x, Second As Windows10x) As Boolean
                Return First.Equals(Second)
            End Operator

            Shared Operator <>(First As Windows10x, Second As Windows10x) As Boolean
                Return Not First.Equals(Second)
            End Operator
            Public Function Clone() Implements ICloneable.Clone
                Return MemberwiseClone()
            End Function
        End Structure
        Structure Windows8x : Implements ICloneable
            Public Start As Integer
            Public ColorizationColor As Color
            Public ColorizationColorBalance As Integer
            Public StartColor As Color
            Public AccentColor As Color
            Public Theme As Windows7.Themes
            Public LogonUI As Integer
            Public PersonalColors_Background As Color
            Public PersonalColors_Accent As Color
            Public NoLockScreen As Boolean
            Public LockScreenType As Structures.LogonUI7.Modes
            Public LockScreenSystemID As Integer

            Shared Operator =(First As Windows8x, Second As Windows8x) As Boolean
                Return First.Equals(Second)
            End Operator

            Shared Operator <>(First As Windows8x, Second As Windows8x) As Boolean
                Return Not First.Equals(Second)
            End Operator
            Public Function Clone() Implements ICloneable.Clone
                Return MemberwiseClone()
            End Function

            Public Sub Load(_DefWin As Windows8x)
                If My.W8 Or My.W81 Then
                    Dim y As Object

                    Dim stringThemeName As New System.Text.StringBuilder(260)
                    UxTheme.GetCurrentThemeName(stringThemeName, 260, Nothing, 0, Nothing, 0)

                    If stringThemeName.ToString.Split("\").Last.ToLower = "aerolite.msstyles" Or String.IsNullOrWhiteSpace(stringThemeName.ToString) Then
                        Theme = Structures.Windows7.Themes.AeroLite
                    Else
                        Theme = Structures.Windows7.Themes.Aero
                    End If

                    y = GetReg("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationColor", _DefWin.ColorizationColor.ToArgb)
                    ColorizationColor = Color.FromArgb(255, Color.FromArgb(y))

                    y = GetReg("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationColorBalance", _DefWin.ColorizationColorBalance)
                    ColorizationColorBalance = y

                    y = GetReg("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "StartColor", Color.FromArgb(84, 0, 30).ToArgb)
                    StartColor = Color.FromArgb(255, Color.FromArgb(y)).Reverse

                    y = GetReg("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "AccentColor", Color.FromArgb(178, 29, 72).ToArgb)
                    AccentColor = Color.FromArgb(255, Color.FromArgb(y)).Reverse

                    Dim S As String

                    S = GetReg("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Personalization", "PersonalColors_Background", _DefWin.PersonalColors_Background.HEX(False, True))
                    PersonalColors_Background = S.FromHEXToColor

                    S = GetReg("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Personalization", "PersonalColors_Accent", _DefWin.PersonalColors_Accent.HEX(False, True))
                    PersonalColors_Accent = S.FromHEXToColor

                    Start = GetReg("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Personalization", "ForceStartBackground", 0)
                    LogonUI = GetReg("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "DefaultColorSet", 0)
                    LockScreenType = GetReg("HKEY_CURRENT_USER\Software\WinPaletter\LogonUI", "Mode", Structures.LogonUI7.Modes.Default_)
                    LockScreenSystemID = GetReg("HKEY_CURRENT_USER\Software\WinPaletter\LogonUI", "Metro_LockScreenSystemID", 0)
                    NoLockScreen = GetReg("HKEY_CURRENT_USER\Software\Policies\Microsoft\Windows\Personalization", "NoLockScreen", False)
                Else
                    Theme = _DefWin.Theme
                    StartColor = _DefWin.StartColor
                    AccentColor = _DefWin.AccentColor
                    PersonalColors_Background = _DefWin.PersonalColors_Background
                    PersonalColors_Accent = _DefWin.PersonalColors_Accent
                    Start = _DefWin.Start
                    LogonUI = _DefWin.LogonUI
                    NoLockScreen = _DefWin.NoLockScreen
                    LockScreenType = _DefWin.LockScreenType
                    LockScreenSystemID = _DefWin.LockScreenSystemID
                End If
            End Sub

            Public Sub Apply(Optional [TreeView] As TreeView = Nothing)
                EditReg([TreeView], "HKEY_CURRENT_USER\Control Panel\Desktop", "AutoColorization", 0)

                Try
                    Select Case Theme
                        Case Structures.Windows7.Themes.Aero
                            UxTheme.EnableTheming(1)
                            UxTheme.SetSystemVisualStyle(My.PATH_Windows & "\resources\Themes\Aero\Aero.msstyles", "NormalColor", "NormalSize", 0)
                            If [TreeView] IsNot Nothing Then AddNode([TreeView], My.Lang.Verbose_SettingAero, "dll")

                        Case Structures.Windows7.Themes.AeroLite
                            UxTheme.EnableTheming(1)
                            UxTheme.SetSystemVisualStyle(My.PATH_Windows & "\resources\Themes\Aero\AeroLite.msstyles", "NormalColor", "NormalSize", 0)
                            If [TreeView] IsNot Nothing Then AddNode([TreeView], My.Lang.Verbose_SettingAeroLite, "dll")

                            Try
                                My.Computer.Registry.CurrentUser.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Themes\HighContrast", True).DeleteSubKeyTree("Pre-High Contrast Scheme", False)
                                If [TreeView] IsNot Nothing Then AddNode([TreeView], String.Format(My.Lang.Verbose_DeletingHighContrastThemes, "HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Themes\HighContrast"), "reg_del")
                            Catch
                                'Do nothing
                                My.Computer.Registry.CurrentUser.Close()
                            Finally
                                My.Computer.Registry.CurrentUser.Close()
                            End Try

                            EditReg([TreeView], "HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Themes", "CurrentTheme", "", RegistryValueKind.String)
                            EditReg([TreeView], "HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Themes", "LastHighContrastTheme", "", RegistryValueKind.String)

                    End Select
                Catch
                End Try

                EditReg([TreeView], "HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationColor", ColorizationColor.ToArgb)
                EditReg([TreeView], "HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationColorBalance", ColorizationColorBalance)

                EditReg([TreeView], "HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "StartColor", StartColor.Reverse.ToArgb)
                EditReg([TreeView], "HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "DefaultStartColor", StartColor.Reverse.ToArgb)
                EditReg([TreeView], "HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "AccentColor", AccentColor.Reverse.ToArgb)
                EditReg([TreeView], "HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "DefaultColorSet", LogonUI)

                EditReg([TreeView], "HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Personalization", "ForceStartBackground", Start)
                EditReg([TreeView], "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "DefaultColorSet", LogonUI)
                EditReg([TreeView], "HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Personalization", "PersonalColors_Background", "#" & PersonalColors_Background.HEX(False), RegistryValueKind.String)
                EditReg([TreeView], "HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Personalization", "PersonalColors_Accent", "#" & PersonalColors_Accent.HEX(False), RegistryValueKind.String)
            End Sub
        End Structure
        Structure Windows7 : Implements ICloneable
            Public ColorizationColor As Color
            Public ColorizationAfterglow As Color
            Public EnableAeroPeek As Boolean
            Public AlwaysHibernateThumbnails As Boolean
            Public ColorizationColorBalance As Integer
            Public ColorizationAfterglowBalance As Integer
            Public ColorizationBlurBalance As Integer
            Public ColorizationGlassReflectionIntensity As Integer
            Public Theme As Themes

            Shared Operator =(First As Windows7, Second As Windows7) As Boolean
                Return First.Equals(Second)
            End Operator

            Shared Operator <>(First As Windows7, Second As Windows7) As Boolean
                Return Not First.Equals(Second)
            End Operator

            Enum Themes
                Aero
                AeroLite
                AeroOpaque
                Basic
                Classic
            End Enum

            Public Sub Load(_DefWin As Windows7)
                If My.W7 Or My.W8 Or My.W81 Then
                    Dim y As Object

                    y = GetReg("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationColor", _DefWin.ColorizationColor.ToArgb)
                    ColorizationColor = Color.FromArgb(255, Color.FromArgb(y))

                    y = GetReg("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationColorBalance", _DefWin.ColorizationColorBalance)
                    ColorizationColorBalance = y

                    If My.W7 Then
                        y = GetReg("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationAfterglow", _DefWin.ColorizationAfterglow.ToArgb)
                        ColorizationAfterglow = Color.FromArgb(255, Color.FromArgb(y))

                        y = GetReg("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationAfterglowBalance", _DefWin.ColorizationAfterglowBalance)
                        ColorizationAfterglowBalance = y

                        y = GetReg("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationBlurBalance", _DefWin.ColorizationBlurBalance)
                        ColorizationBlurBalance = y

                        y = GetReg("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationGlassReflectionIntensity", _DefWin.ColorizationGlassReflectionIntensity)
                        ColorizationGlassReflectionIntensity = y

                        Dim Com, Opaque As Boolean
                        Dwmapi.DwmIsCompositionEnabled(Com)

                        Opaque = GetReg("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationOpaqueBlend", False)

                        Dim Classic As Boolean = False

                        Try
                            Dim stringThemeName As New System.Text.StringBuilder(260)
                            UxTheme.GetCurrentThemeName(stringThemeName, 260, Nothing, 0, Nothing, 0)
                            Classic = String.IsNullOrWhiteSpace(stringThemeName.ToString) Or Not IO.File.Exists(stringThemeName.ToString)
                        Catch
                            Classic = False
                        End Try

                        If Classic Then
                            Theme = Themes.Classic
                        ElseIf Com Then
                            If Not Opaque Then Theme = Themes.Aero Else Theme = Themes.AeroOpaque
                        Else
                            Theme = Themes.Basic
                        End If

                    End If

                    EnableAeroPeek = GetReg("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "EnableAeroPeek", _DefWin.EnableAeroPeek)

                    AlwaysHibernateThumbnails = GetReg("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "AlwaysHibernateThumbnails", _DefWin.AlwaysHibernateThumbnails)

                Else
                    ColorizationColor = _DefWin.ColorizationColor
                    ColorizationColorBalance = _DefWin.ColorizationColorBalance
                    ColorizationAfterglow = _DefWin.ColorizationAfterglow
                    ColorizationAfterglowBalance = _DefWin.ColorizationAfterglowBalance
                    ColorizationBlurBalance = _DefWin.ColorizationBlurBalance
                    ColorizationGlassReflectionIntensity = _DefWin.ColorizationGlassReflectionIntensity
                    Theme = _DefWin.Theme
                    EnableAeroPeek = _DefWin.EnableAeroPeek
                    AlwaysHibernateThumbnails = _DefWin.AlwaysHibernateThumbnails
                End If
            End Sub

            Public Sub Apply(Optional [TreeView] As TreeView = Nothing)
                Select Case Theme
                    Case Themes.Aero
                        UxTheme.EnableTheming(1)
                        UxTheme.SetSystemVisualStyle(My.PATH_Windows & "\resources\Themes\Aero\Aero.msstyles", "NormalColor", "NormalSize", 0)
                        If [TreeView] IsNot Nothing Then AddNode([TreeView], My.Lang.Verbose_SettingAero, "dll")

                        EditReg([TreeView], "HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "CompositionPolicy", 2)
                        EditReg([TreeView], "HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "Composition", 1)
                        EditReg([TreeView], "HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationOpaqueBlend", 0)

                    Case Themes.AeroOpaque
                        UxTheme.EnableTheming(1)
                        UxTheme.SetSystemVisualStyle(My.PATH_Windows & "\resources\Themes\Aero\Aero.msstyles", "NormalColor", "NormalSize", 0)

                        EditReg([TreeView], "HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "CompositionPolicy", 2)
                        EditReg([TreeView], "HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "Composition", 1)
                        EditReg([TreeView], "HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationOpaqueBlend", 1)
                        If [TreeView] IsNot Nothing Then AddNode([TreeView], My.Lang.Verbose_SettingAeroOpaque, "dll")

                    Case Themes.Basic
                        UxTheme.EnableTheming(1)
                        UxTheme.SetSystemVisualStyle(My.PATH_Windows & "\resources\Themes\Aero\Aero.msstyles", "NormalColor", "NormalSize", 0)

                        EditReg([TreeView], "HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "CompositionPolicy", 1)
                        EditReg([TreeView], "HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "Composition", 0)
                        EditReg([TreeView], "HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationOpaqueBlend", 0)
                        If [TreeView] IsNot Nothing Then AddNode([TreeView], My.Lang.Verbose_SettingBasic, "dll")

                    Case Themes.Classic
                        UxTheme.EnableTheming(0)
                        If [TreeView] IsNot Nothing Then AddNode([TreeView], My.Lang.Verbose_SettingClassic, "dll")

                End Select

                EditReg([TreeView], "HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationAfterglow", ColorizationAfterglow.ToArgb)
                EditReg([TreeView], "HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationAfterglowBalance", ColorizationAfterglowBalance)
                EditReg([TreeView], "HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationBlurBalance", ColorizationBlurBalance)
                EditReg([TreeView], "HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationGlassReflectionIntensity", ColorizationGlassReflectionIntensity)

                EditReg([TreeView], "HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationColor", ColorizationColor.ToArgb)
                EditReg([TreeView], "HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationColorBalance", ColorizationColorBalance)

                EditReg([TreeView], "HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "EnableAeroPeek", EnableAeroPeek.ToInteger)
                EditReg([TreeView], "HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "AlwaysHibernateThumbnails", AlwaysHibernateThumbnails.ToInteger)
                EditReg([TreeView], "HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "EnableWindowColorization", 1)
            End Sub

            Public Function Clone() Implements ICloneable.Clone
                Return MemberwiseClone()
            End Function
        End Structure
        Structure WindowsVista : Implements ICloneable
            Public ColorizationColor As Color
            Public [Alpha] As Byte
            Public Theme As Windows7.Themes

            Shared Operator =(First As WindowsVista, Second As WindowsVista) As Boolean
                Return First.Equals(Second)
            End Operator

            Shared Operator <>(First As WindowsVista, Second As WindowsVista) As Boolean
                Return Not First.Equals(Second)
            End Operator

            Public Sub Load(_DefWin As WindowsVista)
                If My.WVista Then
                    Dim y As Object

                    y = GetReg("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationColor", _DefWin.ColorizationColor.ToArgb)
                    ColorizationColor = Color.FromArgb(255, Color.FromArgb(y))
                    Alpha = Color.FromArgb(y).A

                    Dim Com, Opaque As Boolean
                    Dwmapi.DwmIsCompositionEnabled(Com)

                    Opaque = GetReg("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationOpaqueBlend", False)

                    Dim Classic As Boolean = False

                    Try
                        Dim stringThemeName As New System.Text.StringBuilder(260)
                        UxTheme.GetCurrentThemeName(stringThemeName, 260, Nothing, 0, Nothing, 0)
                        Classic = String.IsNullOrWhiteSpace(stringThemeName.ToString) Or Not IO.File.Exists(stringThemeName.ToString)
                    Catch
                        Classic = False
                    End Try

                    If Classic Then
                        Theme = Structures.Windows7.Themes.Classic
                    ElseIf Com Then
                        If Not Opaque Then Theme = Structures.Windows7.Themes.Aero Else Theme = Structures.Windows7.Themes.AeroOpaque
                    Else
                        Theme = Structures.Windows7.Themes.Basic
                    End If


                Else
                    ColorizationColor = _DefWin.ColorizationColor
                    Alpha = _DefWin.Alpha
                    Theme = _DefWin.Theme
                End If
            End Sub

            Public Sub Apply(Optional [TreeView] As TreeView = Nothing)
                Select Case Theme
                    Case Structures.Windows7.Themes.Aero
                        UxTheme.EnableTheming(1)
                        UxTheme.SetSystemVisualStyle(My.PATH_Windows & "\resources\Themes\Aero\Aero.msstyles", "NormalColor", "NormalSize", 0)

                        EditReg([TreeView], "HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "CompositionPolicy", 2)
                        EditReg([TreeView], "HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "Composition", 1)
                        EditReg([TreeView], "HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationOpaqueBlend", 0)
                        If [TreeView] IsNot Nothing Then AddNode([TreeView], My.Lang.Verbose_SettingAero, "dll")

                    Case Structures.Windows7.Themes.AeroOpaque
                        UxTheme.EnableTheming(1)
                        UxTheme.SetSystemVisualStyle(My.PATH_Windows & "\resources\Themes\Aero\Aero.msstyles", "NormalColor", "NormalSize", 0)

                        EditReg([TreeView], "HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "CompositionPolicy", 2)
                        EditReg([TreeView], "HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "Composition", 1)
                        EditReg([TreeView], "HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationOpaqueBlend", 1)
                        If [TreeView] IsNot Nothing Then AddNode([TreeView], My.Lang.Verbose_SettingAeroOpaque, "dll")

                    Case Structures.Windows7.Themes.Basic
                        UxTheme.EnableTheming(1)
                        UxTheme.SetSystemVisualStyle(My.PATH_Windows & "\resources\Themes\Aero\Aero.msstyles", "NormalColor", "NormalSize", 0)

                        EditReg([TreeView], "HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "CompositionPolicy", 1)
                        EditReg([TreeView], "HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "Composition", 0)
                        EditReg([TreeView], "HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationOpaqueBlend", 0)
                        If [TreeView] IsNot Nothing Then AddNode([TreeView], My.Lang.Verbose_SettingBasic, "dll")

                    Case Structures.Windows7.Themes.Classic
                        UxTheme.EnableTheming(0)
                        If [TreeView] IsNot Nothing Then AddNode([TreeView], My.Lang.Verbose_SettingClassic, "dll")

                End Select

                EditReg([TreeView], "HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationColor", Color.FromArgb([Alpha], ColorizationColor).ToArgb)

            End Sub

            Public Function Clone() Implements ICloneable.Clone
                Return MemberwiseClone()
            End Function
        End Structure
        Structure WindowsXP : Implements ICloneable
            Public Theme As Themes
            Public ThemeFile As String
            Public ColorScheme As String

            Enum Themes
                LunaBlue
                LunaOliveGreen
                LunaSilver
                Classic
                Custom
            End Enum

            Public Sub Load(_DefWin As WindowsXP)
                If My.WXP Then
                    Dim vsFile As New Text.StringBuilder(260)
                    Dim colorName As New Text.StringBuilder(260)
                    Dim sizeName As New Text.StringBuilder(260)

                    UxTheme.GetCurrentThemeName(vsFile, 260, colorName, 260, sizeName, 260)

                    If vsFile.ToString.ToLower = My.PATH_Windows.ToLower & "\resources\Themes\Luna\Luna.msstyles".ToLower Then
                        If colorName.ToString.ToLower = "normalcolor" Then
                            Theme = Themes.LunaBlue
                        ElseIf colorName.ToString.ToLower = "homestead" Then
                            Theme = Themes.LunaOliveGreen
                        ElseIf colorName.ToString.ToLower = "metallic" Then
                            Theme = Themes.LunaSilver
                        Else
                            Theme = Themes.LunaBlue
                        End If

                        ThemeFile = vsFile.ToString
                        ColorScheme = colorName.ToString

                    ElseIf IO.File.Exists(vsFile.ToString) AndAlso (IO.Path.GetExtension(vsFile.ToString) = ".theme" Or IO.Path.GetExtension(vsFile.ToString) = ".msstyles") Then
                        Theme = Themes.Custom
                        ThemeFile = vsFile.ToString
                        ColorScheme = colorName.ToString

                    ElseIf String.IsNullOrEmpty(vsFile.ToString) Then
                        Theme = Themes.Classic
                        ThemeFile = My.PATH_Windows.ToLower & "\resources\Themes\Luna.theme"
                        ColorScheme = "NormalColor"

                    Else
                        Theme = Themes.Custom
                        ThemeFile = ""
                        ColorScheme = ""

                    End If

                Else
                    Theme = _DefWin.Theme
                    ThemeFile = _DefWin.ThemeFile
                    ColorScheme = _DefWin.ColorScheme
                End If
            End Sub

            Sub Apply(Optional [TreeView] As TreeView = Nothing)
                Try
                    Select Case Theme
                        Case Themes.LunaBlue
                            UxTheme.EnableTheming(1)
                            UxTheme.SetSystemVisualStyle(My.PATH_Windows & "\resources\Themes\Luna\Luna.msstyles", "NormalColor", "NormalSize", 0)
                            My.StartedWithClassicTheme = False
                            If [TreeView] IsNot Nothing Then AddNode([TreeView], My.Lang.Verbose_SettingLunaBlue, "dll")

                        Case Themes.LunaOliveGreen
                            UxTheme.EnableTheming(1)
                            UxTheme.SetSystemVisualStyle(My.PATH_Windows & "\resources\Themes\Luna\Luna.msstyles", "HomeStead", "NormalSize", 0)
                            My.StartedWithClassicTheme = False
                            If [TreeView] IsNot Nothing Then AddNode([TreeView], My.Lang.Verbose_SettingLunaGreen, "dll")

                        Case Themes.LunaSilver
                            UxTheme.EnableTheming(1)
                            UxTheme.SetSystemVisualStyle(My.PATH_Windows & "\resources\Themes\Luna\Luna.msstyles", "Metallic", "NormalSize", 0)
                            My.StartedWithClassicTheme = False
                            If [TreeView] IsNot Nothing Then AddNode([TreeView], My.Lang.Verbose_SettingLunaSilver, "dll")

                        Case Themes.Classic
                            UxTheme.EnableTheming(0)
                            My.StartedWithClassicTheme = True
                            If [TreeView] IsNot Nothing Then AddNode([TreeView], My.Lang.Verbose_SettingClassic, "dll")

                        Case Themes.Custom

                            If IO.File.Exists(ThemeFile) AndAlso (IO.Path.GetExtension(ThemeFile) = ".theme" Or IO.Path.GetExtension(ThemeFile) = ".msstyles") Then
                                UxTheme.EnableTheming(1)
                                UxTheme.SetSystemVisualStyle(ThemeFile, ColorScheme, "NormalSize", 0)
                                My.StartedWithClassicTheme = False
                                If [TreeView] IsNot Nothing Then AddNode([TreeView], String.Format(My.Lang.Verbose_SettingVS, IO.Path.GetFileName(Theme)), "dll")
                            End If

                    End Select

                    Dim vsFile As New Text.StringBuilder(260)
                    Dim colorName As New Text.StringBuilder(260)
                    Dim sizeName As New Text.StringBuilder(260)

                    UxTheme.GetCurrentThemeName(vsFile, 260, colorName, 260, sizeName, 260)

                    EditReg([TreeView], "HKEY_USERS\.DEFAULT\Software\Microsoft\Windows\CurrentVersion\ThemeManager", "DllName", vsFile.ToString, RegistryValueKind.String)
                    EditReg([TreeView], "HKEY_USERS\.DEFAULT\Software\Microsoft\Windows\CurrentVersion\ThemeManager", "ColorName", colorName.ToString, RegistryValueKind.String)

                Catch
                End Try
            End Sub

            Shared Operator =(First As WindowsXP, Second As WindowsXP) As Boolean
                Return First.Equals(Second)
            End Operator

            Shared Operator <>(First As WindowsXP, Second As WindowsXP) As Boolean
                Return Not First.Equals(Second)
            End Operator

            Public Function Clone() As Object Implements ICloneable.Clone
                Return MemberwiseClone()
            End Function
        End Structure
        Structure Win32UI : Implements ICloneable
            Public EnableTheming As Boolean
            Public EnableGradient As Boolean
            Public ActiveBorder As Color
            Public ActiveTitle As Color
            Public AppWorkspace As Color
            Public Background As Color
            Public ButtonAlternateFace As Color
            Public ButtonDkShadow As Color
            Public ButtonFace As Color
            Public ButtonHilight As Color
            Public ButtonLight As Color
            Public ButtonShadow As Color
            Public ButtonText As Color
            Public GradientActiveTitle As Color
            Public GradientInactiveTitle As Color
            Public GrayText As Color
            Public HilightText As Color
            Public HotTrackingColor As Color
            Public InactiveBorder As Color
            Public InactiveTitle As Color
            Public InactiveTitleText As Color
            Public InfoText As Color
            Public InfoWindow As Color
            Public Menu As Color
            Public MenuBar As Color
            Public MenuText As Color
            Public Scrollbar As Color
            Public TitleText As Color
            Public Window As Color
            Public WindowFrame As Color
            Public WindowText As Color
            Public Hilight As Color
            Public MenuHilight As Color
            Public Desktop As Color

            Shared Operator =(First As Win32UI, Second As Win32UI) As Boolean
                Return First.Equals(Second)
            End Operator

            Shared Operator <>(First As Win32UI, Second As Win32UI) As Boolean
                Return Not First.Equals(Second)
            End Operator
            Enum Method
                Registry
                File
                VisualStyles
            End Enum
            Public Sub Load(Optional Method As Method = Method.Registry, Optional vs As VisualStyleMetrics = Nothing)
                Select Case Method
                    Case Method.Registry

                        User32.Fixer.SystemParametersInfo(SPI.Effects.GETFLATMENU, 0, EnableTheming, SPIF.None)
                        User32.Fixer.SystemParametersInfo(SPI.Titlebars.GETGRADIENTCAPTIONS, 0, EnableGradient, SPIF.None)

                        With GetReg("HKEY_CURRENT_USER\Control Panel\Colors", "ActiveTitle", "153 180 209")
                            If .ToString.Split(" ").Count = 3 Then ActiveTitle = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                        End With

                        With GetReg("HKEY_CURRENT_USER\Control Panel\Colors", "AppWorkspace", "171 171 171")
                            If .ToString.Split(" ").Count = 3 Then AppWorkspace = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                        End With

                        With GetReg("HKEY_CURRENT_USER\Control Panel\Colors", "Background", "0 0 0")
                            If .ToString.Split(" ").Count = 3 Then Background = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                        End With

                        With GetReg("HKEY_CURRENT_USER\Control Panel\Colors", "ButtonAlternateFace", "0 0 0")
                            If .ToString.Split(" ").Count = 3 Then ButtonAlternateFace = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                        End With

                        With GetReg("HKEY_CURRENT_USER\Control Panel\Colors", "ButtonDkShadow", "105 105 105")
                            If .ToString.Split(" ").Count = 3 Then ButtonDkShadow = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                        End With

                        With GetReg("HKEY_CURRENT_USER\Control Panel\Colors", "ButtonFace", "240 240 240")
                            If .ToString.Split(" ").Count = 3 Then ButtonFace = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                        End With

                        With GetReg("HKEY_CURRENT_USER\Control Panel\Colors", "ButtonHilight", "255 255 255")
                            If .ToString.Split(" ").Count = 3 Then ButtonHilight = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                        End With

                        With GetReg("HKEY_CURRENT_USER\Control Panel\Colors", "ButtonLight", "227 227 227")
                            If .ToString.Split(" ").Count = 3 Then ButtonLight = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                        End With

                        With GetReg("HKEY_CURRENT_USER\Control Panel\Colors", "ButtonShadow", "160 160 160")
                            If .ToString.Split(" ").Count = 3 Then ButtonShadow = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                        End With

                        With GetReg("HKEY_CURRENT_USER\Control Panel\Colors", "ButtonText", "0 0 0")
                            If .ToString.Split(" ").Count = 3 Then ButtonText = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                        End With

                        With GetReg("HKEY_CURRENT_USER\Control Panel\Colors", "GradientActiveTitle", "185 209 234")
                            If .ToString.Split(" ").Count = 3 Then GradientActiveTitle = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                        End With

                        With GetReg("HKEY_CURRENT_USER\Control Panel\Colors", "GradientInactiveTitle", "215 228 242")
                            If .ToString.Split(" ").Count = 3 Then GradientInactiveTitle = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                        End With

                        With GetReg("HKEY_CURRENT_USER\Control Panel\Colors", "GrayText", "109 109 109")
                            If .ToString.Split(" ").Count = 3 Then GrayText = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                        End With

                        With GetReg("HKEY_CURRENT_USER\Control Panel\Colors", "HilightText", "255 255 255")
                            If .ToString.Split(" ").Count = 3 Then HilightText = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                        End With

                        With GetReg("HKEY_CURRENT_USER\Control Panel\Colors", "HotTrackingColor", "0 102 204")
                            If .ToString.Split(" ").Count = 3 Then HotTrackingColor = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                        End With

                        With GetReg("HKEY_CURRENT_USER\Control Panel\Colors", "ActiveBorder", "244 247 252")
                            If .ToString.Split(" ").Count = 3 Then ActiveBorder = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                        End With

                        With GetReg("HKEY_CURRENT_USER\Control Panel\Colors", "InactiveBorder", "244 247 252")
                            If .ToString.Split(" ").Count = 3 Then InactiveBorder = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                        End With

                        With GetReg("HKEY_CURRENT_USER\Control Panel\Colors", "InactiveTitle", "191 205 219")
                            If .ToString.Split(" ").Count = 3 Then InactiveTitle = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                        End With

                        With GetReg("HKEY_CURRENT_USER\Control Panel\Colors", "InactiveTitleText", "0 0 0")
                            If .ToString.Split(" ").Count = 3 Then InactiveTitleText = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                        End With

                        With GetReg("HKEY_CURRENT_USER\Control Panel\Colors", "InfoText", "0 0 0")
                            If .ToString.Split(" ").Count = 3 Then InfoText = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                        End With

                        With GetReg("HKEY_CURRENT_USER\Control Panel\Colors", "InfoWindow", "255 255 225")
                            If .ToString.Split(" ").Count = 3 Then InfoWindow = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                        End With

                        With GetReg("HKEY_CURRENT_USER\Control Panel\Colors", "Menu", "240 240 240")
                            If .ToString.Split(" ").Count = 3 Then Menu = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                        End With

                        With GetReg("HKEY_CURRENT_USER\Control Panel\Colors", "MenuBar", "240 240 240")
                            If .ToString.Split(" ").Count = 3 Then MenuBar = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                        End With

                        With GetReg("HKEY_CURRENT_USER\Control Panel\Colors", "MenuText", "0 0 0")
                            If .ToString.Split(" ").Count = 3 Then MenuText = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                        End With

                        With GetReg("HKEY_CURRENT_USER\Control Panel\Colors", "Scrollbar", "200 200 200")
                            If .ToString.Split(" ").Count = 3 Then Scrollbar = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                        End With

                        With GetReg("HKEY_CURRENT_USER\Control Panel\Colors", "TitleText", "0 0 0")
                            If .ToString.Split(" ").Count = 3 Then TitleText = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                        End With

                        With GetReg("HKEY_CURRENT_USER\Control Panel\Colors", "Window", "255 255 255")
                            If .ToString.Split(" ").Count = 3 Then Window = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                        End With

                        With GetReg("HKEY_CURRENT_USER\Control Panel\Colors", "WindowFrame", "100 100 100")
                            If .ToString.Split(" ").Count = 3 Then WindowFrame = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                        End With

                        With GetReg("HKEY_CURRENT_USER\Control Panel\Colors", "WindowText", "0 0 0")
                            If .ToString.Split(" ").Count = 3 Then WindowText = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                        End With

                        With GetReg("HKEY_CURRENT_USER\Control Panel\Colors", "Hilight", "0 120 215")
                            If .ToString.Split(" ").Count = 3 Then Hilight = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                        End With

                        With GetReg("HKEY_CURRENT_USER\Control Panel\Colors", "MenuHilight", "0 120 215")
                            If .ToString.Split(" ").Count = 3 Then MenuHilight = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                        End With

                        With GetReg("HKEY_CURRENT_USER\Control Panel\Colors", "Desktop", "0 0 0")
                            If .ToString.Split(" ").Count = 3 Then Desktop = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                        End With

                    Case Method.VisualStyles
                        EnableTheming = vs.FlatMenus
                        'ActiveBorder = ActiveBorder
                        ActiveTitle = vs.Colors.ActiveCaption
                        'AppWorkspace = AppWorkspace
                        Background = vs.Colors.Background
                        'ButtonAlternateFace = btnaltface_pick.BackColor
                        ButtonDkShadow = vs.Colors.DkShadow3d
                        ButtonFace = vs.Colors.Btnface
                        ButtonHilight = vs.Colors.BtnHighlight
                        ButtonLight = vs.Colors.Light3d
                        ButtonShadow = vs.Colors.BtnShadow
                        'ButtonText = vs.Colors.MenuText
                        GradientActiveTitle = vs.Colors.GradientActiveCaption
                        GradientInactiveTitle = vs.Colors.GradientInactiveCaption
                        GrayText = vs.Colors.GrayText
                        HilightText = vs.Colors.HighlightText
                        HotTrackingColor = vs.Colors.HotTracking
                        'InactiveBorder = InactiveBorder
                        InactiveTitle = vs.Colors.InactiveCaption
                        InactiveTitleText = vs.Colors.InactiveCaptionText
                        'InfoText = InfoText
                        'InfoWindow = InfoWindow
                        Menu = vs.Colors.Menu
                        MenuBar = vs.Colors.MenuBar
                        MenuText = vs.Colors.MenuText
                        'Scrollbar = Scrollbar
                        TitleText = vs.Colors.CaptionText
                        Window = vs.Colors.Window
                        'WindowFrame = Frame
                        WindowText = vs.Colors.WindowText
                        Hilight = vs.Colors.Highlight
                        MenuHilight = vs.Colors.MenuHilight
                        Desktop = vs.Colors.Background

                End Select
            End Sub

            'Never change their orders
            Enum ColorsNumbers
                Scrollbar
                Background
                ActiveTitle
                InactiveTitle
                Menu
                Window
                WindowFrame
                MenuText
                WindowText
                TitleText
                ActiveBorder
                InactiveBorder
                AppWorkspace
                Hilight
                HilightText
                ButtonFace
                ButtonShadow
                GrayText
                ButtonText
                InactiveTitleText
                ButtonHilight
                ButtonDkShadow
                ButtonLight
                InfoText
                InfoWindow
                ButtonAlternateFace
                HotTrackingColor
                GradientActiveTitle
                GradientInactiveTitle
                MenuHilight
                MenuBar
            End Enum

            Public Sub Apply(Optional [TreeView] As TreeView = Nothing)
                Dim vsFile As New Text.StringBuilder(260)
                Dim colorName As New Text.StringBuilder(260)
                Dim sizeName As New Text.StringBuilder(260)
                UxTheme.GetCurrentThemeName(vsFile, 260, colorName, 260, sizeName, 260)
                Dim isClassic As Boolean = String.IsNullOrEmpty(vsFile.ToString)

                'Hiding forms is added as there is a bug occurs when a classic theme applied on classic Windows mode
                Dim fl As New List(Of Form) : fl.Clear()
                If isClassic Then
                    For Each f As Form In My.Application.OpenForms
                        If f.Visible Then
                            f.SuspendLayout()
                            f.Visible = False
                            fl.Add(f)
                        End If
                    Next
                End If

                Dim C1 As New List(Of Integer)
                Dim C2 As New List(Of UInteger)

                C1.Clear()
                C2.Clear()

                C1.Add(ColorsNumbers.Hilight)
                C2.Add(ColorTranslator.ToWin32(Hilight))

                C1.Add(ColorsNumbers.HilightText)
                C2.Add(ColorTranslator.ToWin32(HilightText))

                C1.Add(ColorsNumbers.TitleText)
                C2.Add(ColorTranslator.ToWin32(TitleText))

                C1.Add(ColorsNumbers.GrayText)
                C2.Add(ColorTranslator.ToWin32(GrayText))

                C1.Add(ColorsNumbers.InactiveBorder)
                C2.Add(ColorTranslator.ToWin32(InactiveBorder))

                C1.Add(ColorsNumbers.InactiveTitle)
                C2.Add(ColorTranslator.ToWin32(InactiveTitle))

                C1.Add(ColorsNumbers.ActiveTitle)
                C2.Add(ColorTranslator.ToWin32(ActiveTitle))

                C1.Add(ColorsNumbers.ActiveBorder)
                C2.Add(ColorTranslator.ToWin32(ActiveBorder))

                C1.Add(ColorsNumbers.AppWorkspace)
                C2.Add(ColorTranslator.ToWin32(AppWorkspace))

                C1.Add(ColorsNumbers.Background)
                C2.Add(ColorTranslator.ToWin32(Background))

                C1.Add(ColorsNumbers.GradientActiveTitle)
                C2.Add(ColorTranslator.ToWin32(GradientActiveTitle))

                C1.Add(ColorsNumbers.GradientInactiveTitle)
                C2.Add(ColorTranslator.ToWin32(GradientInactiveTitle))

                C1.Add(ColorsNumbers.InactiveTitleText)
                C2.Add(ColorTranslator.ToWin32(InactiveTitleText))

                C1.Add(ColorsNumbers.InfoWindow)
                C2.Add(ColorTranslator.ToWin32(InfoWindow))

                C1.Add(ColorsNumbers.InfoText)
                C2.Add(ColorTranslator.ToWin32(InfoText))

                C1.Add(ColorsNumbers.Menu)
                C2.Add(ColorTranslator.ToWin32(Menu))

                C1.Add(ColorsNumbers.MenuText)
                C2.Add(ColorTranslator.ToWin32(MenuText))

                C1.Add(ColorsNumbers.Scrollbar)
                C2.Add(ColorTranslator.ToWin32(Scrollbar))

                C1.Add(ColorsNumbers.Window)
                C2.Add(ColorTranslator.ToWin32(Window))

                C1.Add(ColorsNumbers.WindowFrame)
                C2.Add(ColorTranslator.ToWin32(WindowFrame))

                C1.Add(ColorsNumbers.WindowText)
                C2.Add(ColorTranslator.ToWin32(WindowText))

                C1.Add(ColorsNumbers.HotTrackingColor)
                C2.Add(ColorTranslator.ToWin32(HotTrackingColor))

                C1.Add(ColorsNumbers.MenuHilight)
                C2.Add(ColorTranslator.ToWin32(MenuHilight))

                C1.Add(ColorsNumbers.MenuBar)
                C2.Add(ColorTranslator.ToWin32(MenuBar))

                C1.Add(ColorsNumbers.ButtonFace)
                C2.Add(ColorTranslator.ToWin32(ButtonFace))

                C1.Add(ColorsNumbers.ButtonHilight)
                C2.Add(ColorTranslator.ToWin32(ButtonHilight))

                C1.Add(ColorsNumbers.ButtonShadow)
                C2.Add(ColorTranslator.ToWin32(ButtonShadow))

                C1.Add(ColorsNumbers.ButtonText)
                C2.Add(ColorTranslator.ToWin32(ButtonText))

                C1.Add(ColorsNumbers.ButtonDkShadow)
                C2.Add(ColorTranslator.ToWin32(ButtonDkShadow))

                C1.Add(ColorsNumbers.ButtonAlternateFace)
                C2.Add(ColorTranslator.ToWin32(ButtonAlternateFace))

                C1.Add(ColorsNumbers.ButtonLight)
                C2.Add(ColorTranslator.ToWin32(ButtonLight))

                User32.SetSysColors(C1.Count, C1.ToArray(), C2.ToArray())

                User32.SystemParametersInfo(SPI.Effects.SETFLATMENU, 0, EnableTheming, SPIF.UpdateINIFile)
                User32.SystemParametersInfo(SPI.Titlebars.SETGRADIENTCAPTIONS, 0, EnableGradient, SPIF.UpdateINIFile)

                EditReg([TreeView], "HKEY_CURRENT_USER\Control Panel\Colors", "ActiveBorder", ActiveBorder.ToWin32Reg, RegistryValueKind.String)
                EditReg([TreeView], "HKEY_CURRENT_USER\Control Panel\Colors", "ActiveTitle", ActiveTitle.ToWin32Reg, RegistryValueKind.String)
                EditReg([TreeView], "HKEY_CURRENT_USER\Control Panel\Colors", "AppWorkspace", AppWorkspace.ToWin32Reg, RegistryValueKind.String)
                EditReg([TreeView], "HKEY_CURRENT_USER\Control Panel\Colors", "Background", Background.ToWin32Reg, RegistryValueKind.String)
                EditReg([TreeView], "HKEY_CURRENT_USER\Control Panel\Colors", "ButtonAlternateFace", ButtonAlternateFace.ToWin32Reg, RegistryValueKind.String)
                EditReg([TreeView], "HKEY_CURRENT_USER\Control Panel\Colors", "ButtonDkShadow", ButtonDkShadow.ToWin32Reg, RegistryValueKind.String)
                EditReg([TreeView], "HKEY_CURRENT_USER\Control Panel\Colors", "ButtonFace", ButtonFace.ToWin32Reg, RegistryValueKind.String)
                EditReg([TreeView], "HKEY_CURRENT_USER\Control Panel\Colors", "ButtonHilight", ButtonHilight.ToWin32Reg, RegistryValueKind.String)
                EditReg([TreeView], "HKEY_CURRENT_USER\Control Panel\Colors", "ButtonLight", ButtonLight.ToWin32Reg, RegistryValueKind.String)
                EditReg([TreeView], "HKEY_CURRENT_USER\Control Panel\Colors", "ButtonShadow", ButtonShadow.ToWin32Reg, RegistryValueKind.String)
                EditReg([TreeView], "HKEY_CURRENT_USER\Control Panel\Colors", "ButtonText", ButtonText.ToWin32Reg, RegistryValueKind.String)
                EditReg([TreeView], "HKEY_CURRENT_USER\Control Panel\Colors", "GradientActiveTitle", GradientActiveTitle.ToWin32Reg, RegistryValueKind.String)
                EditReg([TreeView], "HKEY_CURRENT_USER\Control Panel\Colors", "GradientInactiveTitle", GradientInactiveTitle.ToWin32Reg, RegistryValueKind.String)
                EditReg([TreeView], "HKEY_CURRENT_USER\Control Panel\Colors", "GrayText", GrayText.ToWin32Reg, RegistryValueKind.String)
                EditReg([TreeView], "HKEY_CURRENT_USER\Control Panel\Colors", "HilightText", HilightText.ToWin32Reg, RegistryValueKind.String)
                EditReg([TreeView], "HKEY_CURRENT_USER\Control Panel\Colors", "HotTrackingColor", HotTrackingColor.ToWin32Reg, RegistryValueKind.String)
                EditReg([TreeView], "HKEY_CURRENT_USER\Control Panel\Colors", "InactiveBorder", InactiveBorder.ToWin32Reg, RegistryValueKind.String)
                EditReg([TreeView], "HKEY_CURRENT_USER\Control Panel\Colors", "InactiveTitle", InactiveTitle.ToWin32Reg, RegistryValueKind.String)
                EditReg([TreeView], "HKEY_CURRENT_USER\Control Panel\Colors", "InactiveTitleText", InactiveTitleText.ToWin32Reg, RegistryValueKind.String)
                EditReg([TreeView], "HKEY_CURRENT_USER\Control Panel\Colors", "InfoText", InfoText.ToWin32Reg, RegistryValueKind.String)
                EditReg([TreeView], "HKEY_CURRENT_USER\Control Panel\Colors", "InfoWindow", InfoWindow.ToWin32Reg, RegistryValueKind.String)
                EditReg([TreeView], "HKEY_CURRENT_USER\Control Panel\Colors", "Menu", Menu.ToWin32Reg, RegistryValueKind.String)
                EditReg([TreeView], "HKEY_CURRENT_USER\Control Panel\Colors", "MenuBar", MenuBar.ToWin32Reg, RegistryValueKind.String)
                EditReg([TreeView], "HKEY_CURRENT_USER\Control Panel\Colors", "MenuText", MenuText.ToWin32Reg, RegistryValueKind.String)
                EditReg([TreeView], "HKEY_CURRENT_USER\Control Panel\Colors", "Scrollbar", Scrollbar.ToWin32Reg, RegistryValueKind.String)
                EditReg([TreeView], "HKEY_CURRENT_USER\Control Panel\Colors", "TitleText", TitleText.ToWin32Reg, RegistryValueKind.String)
                EditReg([TreeView], "HKEY_CURRENT_USER\Control Panel\Colors", "Window", Window.ToWin32Reg, RegistryValueKind.String)
                EditReg([TreeView], "HKEY_CURRENT_USER\Control Panel\Colors", "WindowFrame", WindowFrame.ToWin32Reg, RegistryValueKind.String)
                EditReg([TreeView], "HKEY_CURRENT_USER\Control Panel\Colors", "WindowText", WindowText.ToWin32Reg, RegistryValueKind.String)
                EditReg([TreeView], "HKEY_CURRENT_USER\Control Panel\Colors", "Hilight", Hilight.ToWin32Reg, RegistryValueKind.String)
                EditReg([TreeView], "HKEY_CURRENT_USER\Control Panel\Colors", "MenuHilight", MenuHilight.ToWin32Reg, RegistryValueKind.String)
                EditReg([TreeView], "HKEY_CURRENT_USER\Control Panel\Colors", "Desktop", Desktop.ToWin32Reg, RegistryValueKind.String)

                EditReg([TreeView], "HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "ActiveBorder", ActiveBorder.ToWin32Reg, RegistryValueKind.String)
                EditReg([TreeView], "HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "ActiveTitle", ActiveTitle.ToWin32Reg, RegistryValueKind.String)
                EditReg([TreeView], "HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "AppWorkspace", AppWorkspace.ToWin32Reg, RegistryValueKind.String)
                EditReg([TreeView], "HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "Background", Background.ToWin32Reg, RegistryValueKind.String)
                EditReg([TreeView], "HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "ButtonAlternateFace", ButtonAlternateFace.ToWin32Reg, RegistryValueKind.String)
                EditReg([TreeView], "HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "ButtonDkShadow", ButtonDkShadow.ToWin32Reg, RegistryValueKind.String)
                EditReg([TreeView], "HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "ButtonFace", ButtonFace.ToWin32Reg, RegistryValueKind.String)
                EditReg([TreeView], "HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "ButtonHilight", ButtonHilight.ToWin32Reg, RegistryValueKind.String)
                EditReg([TreeView], "HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "ButtonLight", ButtonLight.ToWin32Reg, RegistryValueKind.String)
                EditReg([TreeView], "HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "ButtonShadow", ButtonShadow.ToWin32Reg, RegistryValueKind.String)
                EditReg([TreeView], "HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "ButtonText", ButtonText.ToWin32Reg, RegistryValueKind.String)
                EditReg([TreeView], "HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "GradientActiveTitle", GradientActiveTitle.ToWin32Reg, RegistryValueKind.String)
                EditReg([TreeView], "HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "GradientInactiveTitle", GradientInactiveTitle.ToWin32Reg, RegistryValueKind.String)
                EditReg([TreeView], "HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "GrayText", GrayText.ToWin32Reg, RegistryValueKind.String)
                EditReg([TreeView], "HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "HilightText", HilightText.ToWin32Reg, RegistryValueKind.String)
                EditReg([TreeView], "HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "HotTrackingColor", HotTrackingColor.ToWin32Reg, RegistryValueKind.String)
                EditReg([TreeView], "HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "InactiveBorder", InactiveBorder.ToWin32Reg, RegistryValueKind.String)
                EditReg([TreeView], "HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "InactiveTitle", InactiveTitle.ToWin32Reg, RegistryValueKind.String)
                EditReg([TreeView], "HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "InactiveTitleText", InactiveTitleText.ToWin32Reg, RegistryValueKind.String)
                EditReg([TreeView], "HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "InfoText", InfoText.ToWin32Reg, RegistryValueKind.String)
                EditReg([TreeView], "HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "InfoWindow", InfoWindow.ToWin32Reg, RegistryValueKind.String)
                EditReg([TreeView], "HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "Menu", Menu.ToWin32Reg, RegistryValueKind.String)
                EditReg([TreeView], "HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "MenuBar", MenuBar.ToWin32Reg, RegistryValueKind.String)
                EditReg([TreeView], "HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "MenuText", MenuText.ToWin32Reg, RegistryValueKind.String)
                EditReg([TreeView], "HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "Scrollbar", Scrollbar.ToWin32Reg, RegistryValueKind.String)
                EditReg([TreeView], "HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "TitleText", TitleText.ToWin32Reg, RegistryValueKind.String)
                EditReg([TreeView], "HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "Window", Window.ToWin32Reg, RegistryValueKind.String)
                EditReg([TreeView], "HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "WindowFrame", WindowFrame.ToWin32Reg, RegistryValueKind.String)
                EditReg([TreeView], "HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "WindowText", WindowText.ToWin32Reg, RegistryValueKind.String)
                EditReg([TreeView], "HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "Hilight", Hilight.ToWin32Reg, RegistryValueKind.String)
                EditReg([TreeView], "HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "MenuHilight", MenuHilight.ToWin32Reg, RegistryValueKind.String)
                EditReg([TreeView], "HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "Desktop", Desktop.ToWin32Reg, RegistryValueKind.String)

                If isClassic Then
                    If fl.Count > 0 Then
                        Threading.Thread.Sleep(100)
                        For i = 0 To fl.Count - 1
                            fl(i).Visible = True
                            fl(i).ResumeLayout()
                            fl(i).Refresh()
                        Next
                    End If
                End If

                If My.Settings.ThemeApplyingBehavior.ClassicColors_HKU_DEFAULT_Prefs = WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite Then
                    EditReg([TreeView], "HKEY_USERS\.DEFAULT\Control Panel\Colors", "ActiveBorder", ActiveBorder.ToWin32Reg, RegistryValueKind.String)
                    EditReg([TreeView], "HKEY_USERS\.DEFAULT\Control Panel\Colors", "ActiveTitle", ActiveTitle.ToWin32Reg, RegistryValueKind.String)
                    EditReg([TreeView], "HKEY_USERS\.DEFAULT\Control Panel\Colors", "AppWorkspace", AppWorkspace.ToWin32Reg, RegistryValueKind.String)
                    EditReg([TreeView], "HKEY_USERS\.DEFAULT\Control Panel\Colors", "Background", Background.ToWin32Reg, RegistryValueKind.String)
                    EditReg([TreeView], "HKEY_USERS\.DEFAULT\Control Panel\Colors", "ButtonAlternateFace", ButtonAlternateFace.ToWin32Reg, RegistryValueKind.String)
                    EditReg([TreeView], "HKEY_USERS\.DEFAULT\Control Panel\Colors", "ButtonDkShadow", ButtonDkShadow.ToWin32Reg, RegistryValueKind.String)
                    EditReg([TreeView], "HKEY_USERS\.DEFAULT\Control Panel\Colors", "ButtonFace", ButtonFace.ToWin32Reg, RegistryValueKind.String)
                    EditReg([TreeView], "HKEY_USERS\.DEFAULT\Control Panel\Colors", "ButtonHilight", ButtonHilight.ToWin32Reg, RegistryValueKind.String)
                    EditReg([TreeView], "HKEY_USERS\.DEFAULT\Control Panel\Colors", "ButtonLight", ButtonLight.ToWin32Reg, RegistryValueKind.String)
                    EditReg([TreeView], "HKEY_USERS\.DEFAULT\Control Panel\Colors", "ButtonShadow", ButtonShadow.ToWin32Reg, RegistryValueKind.String)
                    EditReg([TreeView], "HKEY_USERS\.DEFAULT\Control Panel\Colors", "ButtonText", ButtonText.ToWin32Reg, RegistryValueKind.String)
                    EditReg([TreeView], "HKEY_USERS\.DEFAULT\Control Panel\Colors", "GradientActiveTitle", GradientActiveTitle.ToWin32Reg, RegistryValueKind.String)
                    EditReg([TreeView], "HKEY_USERS\.DEFAULT\Control Panel\Colors", "GradientInactiveTitle", GradientInactiveTitle.ToWin32Reg, RegistryValueKind.String)
                    EditReg([TreeView], "HKEY_USERS\.DEFAULT\Control Panel\Colors", "GrayText", GrayText.ToWin32Reg, RegistryValueKind.String)
                    EditReg([TreeView], "HKEY_USERS\.DEFAULT\Control Panel\Colors", "HilightText", HilightText.ToWin32Reg, RegistryValueKind.String)
                    EditReg([TreeView], "HKEY_USERS\.DEFAULT\Control Panel\Colors", "HotTrackingColor", HotTrackingColor.ToWin32Reg, RegistryValueKind.String)
                    EditReg([TreeView], "HKEY_USERS\.DEFAULT\Control Panel\Colors", "InactiveBorder", InactiveBorder.ToWin32Reg, RegistryValueKind.String)
                    EditReg([TreeView], "HKEY_USERS\.DEFAULT\Control Panel\Colors", "InactiveTitle", InactiveTitle.ToWin32Reg, RegistryValueKind.String)
                    EditReg([TreeView], "HKEY_USERS\.DEFAULT\Control Panel\Colors", "InactiveTitleText", InactiveTitleText.ToWin32Reg, RegistryValueKind.String)
                    EditReg([TreeView], "HKEY_USERS\.DEFAULT\Control Panel\Colors", "InfoText", InfoText.ToWin32Reg, RegistryValueKind.String)
                    EditReg([TreeView], "HKEY_USERS\.DEFAULT\Control Panel\Colors", "InfoWindow", InfoWindow.ToWin32Reg, RegistryValueKind.String)
                    EditReg([TreeView], "HKEY_USERS\.DEFAULT\Control Panel\Colors", "Menu", Menu.ToWin32Reg, RegistryValueKind.String)
                    EditReg([TreeView], "HKEY_USERS\.DEFAULT\Control Panel\Colors", "MenuBar", MenuBar.ToWin32Reg, RegistryValueKind.String)
                    EditReg([TreeView], "HKEY_USERS\.DEFAULT\Control Panel\Colors", "MenuText", MenuText.ToWin32Reg, RegistryValueKind.String)
                    EditReg([TreeView], "HKEY_USERS\.DEFAULT\Control Panel\Colors", "Scrollbar", Scrollbar.ToWin32Reg, RegistryValueKind.String)
                    EditReg([TreeView], "HKEY_USERS\.DEFAULT\Control Panel\Colors", "TitleText", TitleText.ToWin32Reg, RegistryValueKind.String)
                    EditReg([TreeView], "HKEY_USERS\.DEFAULT\Control Panel\Colors", "Window", Window.ToWin32Reg, RegistryValueKind.String)
                    EditReg([TreeView], "HKEY_USERS\.DEFAULT\Control Panel\Colors", "WindowFrame", WindowFrame.ToWin32Reg, RegistryValueKind.String)
                    EditReg([TreeView], "HKEY_USERS\.DEFAULT\Control Panel\Colors", "WindowText", WindowText.ToWin32Reg, RegistryValueKind.String)
                    EditReg([TreeView], "HKEY_USERS\.DEFAULT\Control Panel\Colors", "Hilight", Hilight.ToWin32Reg, RegistryValueKind.String)
                    EditReg([TreeView], "HKEY_USERS\.DEFAULT\Control Panel\Colors", "MenuHilight", MenuHilight.ToWin32Reg, RegistryValueKind.String)
                    EditReg([TreeView], "HKEY_USERS\.DEFAULT\Control Panel\Colors", "Desktop", Desktop.ToWin32Reg, RegistryValueKind.String)
                End If

                If My.Settings.ThemeApplyingBehavior.ClassicColors_HKLM_Prefs = WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite Then
                    EditReg([TreeView], "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "ActiveTitle", Color.FromArgb(0, ActiveTitle).Reverse(True).ToArgb, RegistryValueKind.String)
                    EditReg([TreeView], "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "ButtonFace", Color.FromArgb(0, ButtonFace).Reverse(True).ToArgb, RegistryValueKind.String)
                    EditReg([TreeView], "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "ButtonText", Color.FromArgb(0, ButtonText).Reverse(True).ToArgb, RegistryValueKind.String)
                    EditReg([TreeView], "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "GrayText", Color.FromArgb(0, GrayText).Reverse(True).ToArgb, RegistryValueKind.String)
                    EditReg([TreeView], "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "Hilight", Color.FromArgb(0, Hilight).Reverse(True).ToArgb, RegistryValueKind.String)
                    EditReg([TreeView], "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "HilightText", Color.FromArgb(0, HilightText).Reverse(True).ToArgb, RegistryValueKind.String)
                    EditReg([TreeView], "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "HotTrackingColor", Color.FromArgb(0, HotTrackingColor).Reverse(True).ToArgb, RegistryValueKind.String)
                    EditReg([TreeView], "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "InactiveTitle", Color.FromArgb(0, InactiveTitle).Reverse(True).ToArgb, RegistryValueKind.String)
                    EditReg([TreeView], "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "InactiveTitleText", Color.FromArgb(0, InactiveTitleText).Reverse(True).ToArgb, RegistryValueKind.String)
                    EditReg([TreeView], "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "MenuHilight", Color.FromArgb(0, MenuHilight).Reverse(True).ToArgb, RegistryValueKind.String)
                    EditReg([TreeView], "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "TitleText", Color.FromArgb(0, TitleText).Reverse(True).ToArgb, RegistryValueKind.String)
                    EditReg([TreeView], "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "Window", Color.FromArgb(0, Window).Reverse(True).ToArgb, RegistryValueKind.String)
                    EditReg([TreeView], "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "WindowText", Color.FromArgb(0, WindowText).Reverse(True).ToArgb, RegistryValueKind.String)

                ElseIf My.Settings.ThemeApplyingBehavior.ClassicColors_HKLM_Prefs = WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.RestoreDefaults Then
                    Dim _DefWin32 As Structures.Win32UI
                    If My.PreviewStyle = WindowStyle.W11 Then
                        _DefWin32 = New CP_Defaults().Default_Windows11.Win32
                    ElseIf My.PreviewStyle = WindowStyle.W10 Then
                        _DefWin32 = New CP_Defaults().Default_Windows10.Win32
                    ElseIf My.PreviewStyle = WindowStyle.W81 Then
                        _DefWin32 = New CP_Defaults().Default_Windows81.Win32
                    ElseIf My.PreviewStyle = WindowStyle.W7 Then
                        _DefWin32 = New CP_Defaults().Default_Windows7.Win32
                    ElseIf My.PreviewStyle = WindowStyle.WVista Then
                        _DefWin32 = New CP_Defaults().Default_WindowsVista.Win32
                    ElseIf My.PreviewStyle = WindowStyle.WXP Then
                        _DefWin32 = New CP_Defaults().Default_WindowsXP.Win32
                    Else
                        _DefWin32 = New CP_Defaults().Default_Windows11.Win32
                    End If

                    EditReg([TreeView], "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "ActiveTitle", Color.FromArgb(0, _DefWin32.ActiveTitle).Reverse(True).ToArgb, RegistryValueKind.String)
                    EditReg([TreeView], "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "ButtonFace", Color.FromArgb(0, _DefWin32.ButtonFace).Reverse(True).ToArgb, RegistryValueKind.String)
                    EditReg([TreeView], "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "ButtonText", Color.FromArgb(0, _DefWin32.ButtonText).Reverse(True).ToArgb, RegistryValueKind.String)
                    EditReg([TreeView], "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "GrayText", Color.FromArgb(0, _DefWin32.GrayText).Reverse(True).ToArgb, RegistryValueKind.String)
                    EditReg([TreeView], "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "Hilight", Color.FromArgb(0, _DefWin32.Hilight).Reverse(True).ToArgb, RegistryValueKind.String)
                    EditReg([TreeView], "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "HilightText", Color.FromArgb(0, _DefWin32.HilightText).Reverse(True).ToArgb, RegistryValueKind.String)
                    EditReg([TreeView], "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "HotTrackingColor", Color.FromArgb(0, _DefWin32.HotTrackingColor).Reverse(True).ToArgb, RegistryValueKind.String)
                    EditReg([TreeView], "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "InactiveTitle", Color.FromArgb(0, _DefWin32.InactiveTitle).Reverse(True).ToArgb, RegistryValueKind.String)
                    EditReg([TreeView], "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "InactiveTitleText", Color.FromArgb(0, _DefWin32.InactiveTitleText).Reverse(True).ToArgb, RegistryValueKind.String)
                    EditReg([TreeView], "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "MenuHilight", Color.FromArgb(0, _DefWin32.MenuHilight).Reverse(True).ToArgb, RegistryValueKind.String)
                    EditReg([TreeView], "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "TitleText", Color.FromArgb(0, _DefWin32.TitleText).Reverse(True).ToArgb, RegistryValueKind.String)
                    EditReg([TreeView], "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "Window", Color.FromArgb(0, _DefWin32.Window).Reverse(True).ToArgb, RegistryValueKind.String)
                    EditReg([TreeView], "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "WindowText", Color.FromArgb(0, _DefWin32.WindowText).Reverse(True).ToArgb, RegistryValueKind.String)

                ElseIf My.Settings.ThemeApplyingBehavior.ClassicColors_HKLM_Prefs = WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Erase Then
                    DelReg_AdministratorDeflector("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors", "Standard")
                End If


            End Sub

            Public Sub Update_UPM_DEFAULT(Optional [TreeView] As TreeView = Nothing)
                If My.Settings.ThemeApplyingBehavior.UPM_HKU_DEFAULT Then
                    Dim source As Byte() = GetReg("HKEY_CURRENT_USER\Control Panel\Desktop", "UserPreferencesMask", Nothing)
                    If source IsNot Nothing Then EditReg([TreeView], "HKEY_USERS\.DEFAULT\Control Panel\Desktop", "UserPreferencesMask", source, RegistryValueKind.Binary)
                End If
            End Sub

            Public Function Clone() Implements ICloneable.Clone
                Return MemberwiseClone()
            End Function
        End Structure
        Structure LogonUI10x : Implements ICloneable
            Public DisableAcrylicBackgroundOnLogon As Boolean
            Public DisableLogonBackgroundImage As Boolean
            Public NoLockScreen As Boolean

            Shared Operator =(First As LogonUI10x, Second As LogonUI10x) As Boolean
                Return First.Equals(Second)
            End Operator

            Shared Operator <>(First As LogonUI10x, Second As LogonUI10x) As Boolean
                Return Not First.Equals(Second)
            End Operator

            Public Function Clone() Implements ICloneable.Clone
                Return MemberwiseClone()
            End Function

            Public Sub Load(_DefLogonUI As LogonUI10x)
                If My.W10 Or My.W11 Then
                    Dim Def As CP = If(My.W11, New CP_Defaults().Default_Windows11, New CP_Defaults().Default_Windows10)

                    DisableAcrylicBackgroundOnLogon = GetReg("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\System", "DisableAcrylicBackgroundOnLogon", _DefLogonUI.DisableAcrylicBackgroundOnLogon)
                    DisableLogonBackgroundImage = GetReg("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\System", "DisableLogonBackgroundImage", _DefLogonUI.DisableLogonBackgroundImage)
                    NoLockScreen = GetReg("HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Windows\Personalization", "NoLockScreen", _DefLogonUI.NoLockScreen)

                Else
                    DisableAcrylicBackgroundOnLogon = _DefLogonUI.DisableAcrylicBackgroundOnLogon
                    DisableLogonBackgroundImage = _DefLogonUI.DisableLogonBackgroundImage
                    NoLockScreen = _DefLogonUI.NoLockScreen
                End If
            End Sub

            Public Sub Apply(Optional [TreeView] As TreeView = Nothing)
                EditReg([TreeView], "HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\System", "DisableAcrylicBackgroundOnLogon", DisableAcrylicBackgroundOnLogon.ToInteger)
                EditReg([TreeView], "HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\System", "DisableLogonBackgroundImage", DisableLogonBackgroundImage.ToInteger)
                EditReg([TreeView], "HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Windows\Personalization", "NoLockScreen", NoLockScreen.ToInteger)
            End Sub
        End Structure
        Structure LogonUI7 : Implements ICloneable
            Public Enabled As Boolean
            Public Mode As Modes
            Public ImagePath As String
            Public Color As Color
            Public Blur As Boolean
            Public Blur_Intensity As Integer
            Public Grayscale As Boolean
            Public Noise As Boolean
            Public Noise_Mode As NoiseMode
            Public Noise_Intensity As Integer

            Shared Operator =(First As LogonUI7, Second As LogonUI7) As Boolean
                Return First.Equals(Second)
            End Operator

            Shared Operator <>(First As LogonUI7, Second As LogonUI7) As Boolean
                Return Not First.Equals(Second)
            End Operator
            Public Function Clone() Implements ICloneable.Clone
                Return MemberwiseClone()
            End Function
            Enum Modes
                Default_
                Wallpaper
                CustomImage
                SolidColor
            End Enum

            Public Sub Load(_DefLogonUI As LogonUI7)
                If My.W7 Or My.W8 Or My.W81 Then

                    ImagePath = GetReg("HKEY_CURRENT_USER\Software\WinPaletter\LogonUI", "ImagePath", "")
                    Color = Color.FromArgb(GetReg("HKEY_CURRENT_USER\Software\WinPaletter\LogonUI", "Color", Color.Black.ToArgb))
                    Blur = GetReg("HKEY_CURRENT_USER\Software\WinPaletter\LogonUI", "Blur", False)
                    Blur_Intensity = GetReg("HKEY_CURRENT_USER\Software\WinPaletter\LogonUI", "Blur_Intensity", 0)
                    Grayscale = GetReg("HKEY_CURRENT_USER\Software\WinPaletter\LogonUI", "Grayscale", False)
                    Noise = GetReg("HKEY_CURRENT_USER\Software\WinPaletter\LogonUI", "Noise", False)
                    Noise_Mode = GetReg("HKEY_CURRENT_USER\Software\WinPaletter\LogonUI", "Noise_Mode", NoiseMode.Acrylic)
                    Noise_Intensity = GetReg("HKEY_CURRENT_USER\Software\WinPaletter\LogonUI", "Noise_Intensity", 0)

                    If My.W7 Then
                        Dim b1 As Boolean = GetReg("HKEY_LOCAL_MACHINE\Software\Microsoft\Windows\CurrentVersion\Authentication\LogonUI\Background", "OEMBackground", False)
                        Dim b2 As Boolean = GetReg("HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Windows\System", "UseOEMBackground", False)
                        Enabled = b1 Or b2
                        Mode = GetReg("HKEY_CURRENT_USER\Software\WinPaletter\LogonUI", "Mode", Modes.Default_)
                    End If

                Else
                    Enabled = _DefLogonUI.Enabled
                    Mode = _DefLogonUI.Mode
                    ImagePath = _DefLogonUI.ImagePath
                    Color = _DefLogonUI.Color
                    Blur = _DefLogonUI.Blur
                    Blur_Intensity = _DefLogonUI.Blur_Intensity
                    Grayscale = _DefLogonUI.Grayscale
                    Noise = _DefLogonUI.Noise
                    Noise_Mode = _DefLogonUI.Noise_Mode
                    Noise_Intensity = _DefLogonUI.Noise_Intensity
                End If
            End Sub
        End Structure
        Structure LogonUIXP : Implements ICloneable
            Public Enabled As Boolean
            Public Mode As Modes
            Public BackColor As Color
            Public ShowMoreOptions As Boolean

            Enum Modes
                Win2000
                [Default]
            End Enum

            Public Sub Load(_DefLogonUI As LogonUIXP)
                If My.WXP Then

                    Enabled = GetReg("HKEY_CURRENT_USER\Software\WinPaletter\LogonUI\WinXP", "", _DefLogonUI.Enabled)

                    Select Case GetReg("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon", "LogonType", _DefLogonUI.Mode)
                        Case 1
                            Mode = Modes.Default
                        Case Else
                            Mode = Modes.Win2000
                    End Select

                    With GetReg("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon", "Background", "0 0 0")
                        If .ToString.Split(" ").Count = 3 Then
                            BackColor = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                        Else
                            BackColor = _DefLogonUI.BackColor
                        End If
                    End With

                    ShowMoreOptions = GetReg("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon", "ShowLogonOptions", _DefLogonUI.ShowMoreOptions)

                Else
                    Mode = _DefLogonUI.Mode
                    BackColor = _DefLogonUI.BackColor
                    ShowMoreOptions = _DefLogonUI.ShowMoreOptions
                End If
            End Sub

            Sub Apply(Optional [TreeView] As TreeView = Nothing)
                EditReg([TreeView], "HKEY_CURRENT_USER\Software\WinPaletter\LogonUI\WinXP", "", Enabled)

                If Enabled And My.WXP Then
                    EditReg([TreeView], "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon", "LogonType", If(Mode = Modes.Default, 1, 0), RegistryValueKind.DWord)
                    EditReg([TreeView], "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon", "Background", BackColor.ToWin32Reg, RegistryValueKind.String)
                    EditReg([TreeView], "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon", "ShowLogonOptions", ShowMoreOptions.ToInteger, RegistryValueKind.DWord)
                End If
            End Sub

            Shared Operator =(First As LogonUIXP, Second As LogonUIXP) As Boolean
                Return First.Equals(Second)
            End Operator

            Shared Operator <>(First As LogonUIXP, Second As LogonUIXP) As Boolean
                Return Not First.Equals(Second)
            End Operator
            Public Function Clone() Implements ICloneable.Clone
                Return MemberwiseClone()
            End Function
        End Structure
        Structure Wallpaper : Implements ICloneable
            Public Enabled As Boolean
            Public SlideShow_Folder_or_ImagesList As Boolean

            Public ImageFile As String
            Public WallpaperStyle As WallpaperStyles
            Public WallpaperType As WallpaperTypes

            Public Wallpaper_Slideshow_ImagesRootPath As String
            Public Wallpaper_Slideshow_Images As String()
            Public Wallpaper_Slideshow_Interval As Integer
            Public Wallpaper_Slideshow_Shuffle As Boolean

            Enum WallpaperStyles As Integer
                Centered = 0
                Tile = 1
                Stretched = 2
                Fit = 6
                Fill = 10
            End Enum
            Enum WallpaperTypes
                Picture
                SolidColor
                SlideShow
            End Enum

            Sub Load(_DefWallpaper As Wallpaper)
                Enabled = GetReg("HKEY_CURRENT_USER\Software\WinPaletter\Wallpaper", "", _DefWallpaper.Enabled)
                SlideShow_Folder_or_ImagesList = GetReg("HKEY_CURRENT_USER\Software\WinPaletter\Wallpaper", "SlideShow_Folder_or_ImagesList", _DefWallpaper.SlideShow_Folder_or_ImagesList)
                Wallpaper_Slideshow_ImagesRootPath = GetReg("HKEY_CURRENT_USER\Software\WinPaletter\Wallpaper", "Wallpaper_Slideshow_ImagesRootPath", _DefWallpaper.Wallpaper_Slideshow_ImagesRootPath)
                Wallpaper_Slideshow_Images = GetReg("HKEY_CURRENT_USER\Software\WinPaletter\Wallpaper", "Wallpaper_Slideshow_Images", _DefWallpaper.Wallpaper_Slideshow_Images)

                ImageFile = GetReg("HKEY_CURRENT_USER\Control Panel\Desktop", "Wallpaper", _DefWallpaper.ImageFile)

                Dim slideshow_img As String = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\Microsoft\Windows\Themes\TranscodedWallpaper"
                Dim spotlight_img As String = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) & "\Packages\Microsoft.Windows.ContentDeliveryManager_cw5n1h2txyewy\LocalState\Assets"

                'Necessary to remember last wallpaper that is not from slideshow and not a spotlight image
                If ImageFile.StartsWith(slideshow_img, My._ignore) OrElse ImageFile.StartsWith(spotlight_img, My._ignore) OrElse Not IO.File.Exists(ImageFile) Then
                    ImageFile = GetReg("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Wallpapers", "CurrentWallpaperPath", _DefWallpaper.ImageFile)
                End If

                If GetReg("HKEY_CURRENT_USER\Control Panel\Desktop", "TileWallpaper", "0") = "1" Then
                    WallpaperStyle = WallpaperStyles.Tile
                Else
                    WallpaperStyle = GetReg("HKEY_CURRENT_USER\Control Panel\Desktop", "WallpaperStyle", _DefWallpaper.WallpaperStyle)
                End If

                WallpaperType = GetReg("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Wallpapers", "BackgroundType", _DefWallpaper.WallpaperType)

                Wallpaper_Slideshow_Interval = GetReg("HKEY_CURRENT_USER\Control Panel\Personalization\Desktop Slideshow", "Interval", _DefWallpaper.Wallpaper_Slideshow_Interval)
                Wallpaper_Slideshow_Shuffle = GetReg("HKEY_CURRENT_USER\Control Panel\Personalization\Desktop Slideshow", "Shuffle", _DefWallpaper.Wallpaper_Slideshow_Shuffle)

            End Sub

            Sub Apply(Optional SkipSettingWallpaper As Boolean = False, Optional [TreeView] As TreeView = Nothing)
                EditReg([TreeView], "HKEY_CURRENT_USER\Software\WinPaletter\Wallpaper", "", Enabled)
                EditReg([TreeView], "HKEY_CURRENT_USER\Software\WinPaletter\Wallpaper", "SlideShow_Folder_or_ImagesList", SlideShow_Folder_or_ImagesList)
                EditReg([TreeView], "HKEY_CURRENT_USER\Software\WinPaletter\Wallpaper", "Wallpaper_Slideshow_ImagesRootPath", Wallpaper_Slideshow_ImagesRootPath, RegistryValueKind.String)
                EditReg([TreeView], "HKEY_CURRENT_USER\Software\WinPaletter\Wallpaper", "Wallpaper_Slideshow_Images", Wallpaper_Slideshow_Images, RegistryValueKind.MultiString)

                If Enabled Then
                    Dim slideshow_ini As String = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\Microsoft\Windows\Themes\slideshow.ini"
                    Dim slideshow_img As String = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\Microsoft\Windows\Themes\TranscodedWallpaper"

                    If IO.File.Exists(slideshow_ini) Then
                        IO.File.SetAttributes(slideshow_ini, IO.FileAttributes.Normal)
                        IO.File.WriteAllText(slideshow_ini, "")
                        IO.File.SetAttributes(slideshow_ini, IO.FileAttributes.Hidden)
                    End If

                    ' Setting WallpaperStyle must be before setting wallpaper itself
                    If WallpaperStyle = WallpaperStyles.Tile Then
                        EditReg([TreeView], "HKEY_CURRENT_USER\Control Panel\Desktop", "TileWallpaper", "1", RegistryValueKind.String)
                    Else
                        EditReg([TreeView], "HKEY_CURRENT_USER\Control Panel\Desktop", "TileWallpaper", "0", RegistryValueKind.String)
                        EditReg([TreeView], "HKEY_CURRENT_USER\Control Panel\Desktop", "WallpaperStyle", CInt(WallpaperStyle), RegistryValueKind.String)
                    End If

                    If Not SkipSettingWallpaper Then
                        If [TreeView] IsNot Nothing Then AddNode([TreeView], My.Lang.Verbose_SettingWallpaper, "dll")

                        If WallpaperType = WallpaperTypes.SolidColor Then
                            SystemParametersInfo(SPI.Desktop.SETDESKWALLPAPER, 0, "", SPIF.UpdateINIFile)
                            EditReg([TreeView], "HKEY_CURRENT_USER\Control Panel\Desktop", "Wallpaper", "", RegistryValueKind.String)

                        ElseIf WallpaperType = WallpaperTypes.Picture Then
                            If (My.WXP Or My.WVista Or My.W7) AndAlso IO.File.Exists(ImageFile) AndAlso Not New FileInfo(ImageFile).FullName.StartsWith(My.PATH_Windows & "\Web", My._ignore) Then
                                Using bmp As New Bitmap(Bitmap_Mgr.Load(ImageFile))
                                    If bmp.RawFormat IsNot Imaging.ImageFormat.Bmp Then
                                        If MsgBox(My.Lang.CP_Wallpaper_NonBMP0, MsgBoxStyle.Question + MsgBoxStyle.YesNo, My.Lang.CP_Wallpaper_NonBMP1) = MsgBoxResult.Yes Then
                                            bmp.Save(ImageFile, Imaging.ImageFormat.Bmp)
                                        End If
                                    End If
                                End Using
                            End If

                            SystemParametersInfo(SPI.Desktop.SETDESKWALLPAPER, 0, ImageFile, SPIF.UpdateINIFile)
                            EditReg([TreeView], "HKEY_CURRENT_USER\Control Panel\Desktop", "Wallpaper", ImageFile, RegistryValueKind.String)

                            'Necessary to make both WinPaletter and Windows remember last wallpaper that is not from slideshow and not a spotlight image
                            EditReg([TreeView], "HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Wallpapers", "CurrentWallpaperPath", ImageFile, RegistryValueKind.String)

                        ElseIf WallpaperType = WallpaperTypes.SlideShow Then
                            SystemParametersInfo(SPI.Desktop.SETDESKWALLPAPER, 0, slideshow_img, SPIF.UpdateINIFile)
                            EditReg([TreeView], "HKEY_CURRENT_USER\Control Panel\Desktop", "Wallpaper", slideshow_img, RegistryValueKind.String)

                        End If
                    End If

                    EditReg([TreeView], "HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Wallpapers", "BackgroundType", WallpaperType)

                    If Not My.WXP AndAlso Not My.WVista Then

                        If Not SkipSettingWallpaper Then
                            Using _ini As New INI(slideshow_ini)

                                If [TreeView] IsNot Nothing Then AddNode([TreeView], String.Format(My.Lang.Verbose_SettingSlideshow, slideshow_ini), "dll")

                                If WallpaperType = WallpaperTypes.SlideShow AndAlso SlideShow_Folder_or_ImagesList AndAlso IO.Directory.Exists(Wallpaper_Slideshow_ImagesRootPath) Then
                                    _ini.IniWriteValue("Slideshow", "ImagesRootPath", Wallpaper_Slideshow_ImagesRootPath)
                                End If

                                _ini.IniWriteValue("Slideshow", "Interval", Wallpaper_Slideshow_Interval)
                                _ini.IniWriteValue("Slideshow", "Shuffle", Wallpaper_Slideshow_Shuffle)

                                If WallpaperType = WallpaperTypes.SlideShow AndAlso Not SlideShow_Folder_or_ImagesList Then
                                    If IO.Directory.Exists(Wallpaper_Slideshow_Images(0)) Then
                                        _ini.IniWriteValue("Slideshow", "ImagesRootPath", New FileInfo(Wallpaper_Slideshow_Images(0)).Directory.FullName)
                                    End If

                                    For i = 0 To Wallpaper_Slideshow_Images.Count - 1
                                        _ini.IniWriteValue("Slideshow", "Item" & i & "Path", Wallpaper_Slideshow_Images(i))
                                    Next
                                End If

                            End Using
                        End If

                        EditReg([TreeView], "HKEY_CURRENT_USER\Control Panel\Personalization\Desktop Slideshow", "Interval", Wallpaper_Slideshow_Interval)
                        EditReg([TreeView], "HKEY_CURRENT_USER\Control Panel\Personalization\Desktop Slideshow", "Shuffle", Wallpaper_Slideshow_Shuffle)
                    End If
                End If
            End Sub

            Shared Operator =(First As Wallpaper, Second As Wallpaper) As Boolean
                Return First.Equals(Second)
            End Operator


            Shared Operator <>(First As Wallpaper, Second As Wallpaper) As Boolean
                Return Not First.Equals(Second)
            End Operator

            Public Function Clone() As Object Implements ICloneable.Clone
                Return MemberwiseClone()
            End Function
        End Structure
        Structure WallpaperTone : Implements ICloneable
            Public Enabled As Boolean
            Public Image As String
            Public H, S, L As Integer

            Public Sub Load(SubKey As String)
                Dim wallpaper As String

                If SubKey.ToLower = "winxp" Then
                    wallpaper = My.PATH_Windows & "\Web\Wallpaper\Bliss.bmp"
                Else
                    wallpaper = My.PATH_Windows & "\Web\Wallpaper\Windows\img0.jpg"
                End If

                If Not IO.File.Exists(wallpaper) Then wallpaper = GetReg("HKEY_CURRENT_USER\Control Panel\Desktop", "Wallpaper", wallpaper)

                Enabled = GetReg("HKEY_CURRENT_USER\Software\WinPaletter\WallpaperTone\" & SubKey, "Enabled", False)
                Image = GetReg("HKEY_CURRENT_USER\Software\WinPaletter\WallpaperTone\" & SubKey, "Image", wallpaper)
                H = GetReg("HKEY_CURRENT_USER\Software\WinPaletter\WallpaperTone\" & SubKey, "H", 0)
                S = GetReg("HKEY_CURRENT_USER\Software\WinPaletter\WallpaperTone\" & SubKey, "S", 100)
                L = GetReg("HKEY_CURRENT_USER\Software\WinPaletter\WallpaperTone\" & SubKey, "L", 100)
            End Sub

            Public Shared Sub Save_To_Registry(WT As WallpaperTone, SubKey As String, Optional [TreeView] As TreeView = Nothing)
                EditReg([TreeView], "HKEY_CURRENT_USER\Software\WinPaletter\WallpaperTone\" & SubKey, "Enabled", WT.Enabled)
                EditReg([TreeView], "HKEY_CURRENT_USER\Software\WinPaletter\WallpaperTone\" & SubKey, "Image", WT.Image, RegistryValueKind.String)
                EditReg([TreeView], "HKEY_CURRENT_USER\Software\WinPaletter\WallpaperTone\" & SubKey, "H", WT.H)
                EditReg([TreeView], "HKEY_CURRENT_USER\Software\WinPaletter\WallpaperTone\" & SubKey, "S", WT.S)
                EditReg([TreeView], "HKEY_CURRENT_USER\Software\WinPaletter\WallpaperTone\" & SubKey, "L", WT.L)
            End Sub

            Public Sub Apply(Optional [TreeView] As TreeView = Nothing)
                If Not IO.File.Exists(Image) Then Throw New IO.IOException("Couldn't Find image")

                Dim path As String
                If Not My.WXP And Not My.WVista Then
                    path = IO.Path.Combine(My.PATH_appData, "TintedWallpaper.bmp")
                Else
                    path = IO.Path.Combine(My.PATH_Windows, "Web\Wallpaper\TintedWallpaper.bmp")
                End If

                If [TreeView] IsNot Nothing Then AddNode([TreeView], String.Format(My.Lang.Verbose_SettingHSLImage, path), "pe_patch")

                Using ImgF As New ImageProcessor.ImageFactory
                    ImgF.Load(Image)
                    ImgF.Hue(H, True)
                    ImgF.Saturation(S - 100)
                    ImgF.Brightness(L - 100)
                    ImgF.Image.Save(path, Imaging.ImageFormat.Bmp)
                End Using

                If [TreeView] IsNot Nothing Then AddNode([TreeView], My.Lang.Verbose_SettingWallpaper, "dll")
                SystemParametersInfo(SPI.Desktop.SETDESKWALLPAPER, 0, path, SPIF.UpdateINIFile)
                EditReg([TreeView], "HKEY_CURRENT_USER\Control Panel\Desktop", "Wallpaper", path, RegistryValueKind.String)
                EditReg([TreeView], "HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Wallpapers", "BackgroundType", CInt(Structures.Wallpaper.WallpaperTypes.Picture))

                MainFrm.Update_Wallpaper_Preview()
            End Sub

            Shared Operator =(First As WallpaperTone, Second As WallpaperTone) As Boolean
                Return First.Equals(Second)
            End Operator

            Shared Operator <>(First As WallpaperTone, Second As WallpaperTone) As Boolean
                Return Not First.Equals(Second)
            End Operator

            Public Function Clone() Implements ICloneable.Clone
                Return MemberwiseClone()
            End Function
        End Structure
        Structure MetricsFonts : Implements ICloneable
            Public Enabled As Boolean
            Public BorderWidth As Integer
            Public CaptionHeight As Integer
            Public CaptionWidth As Integer
            Public IconSpacing As Integer
            Public IconVerticalSpacing As Integer
            Public MenuHeight As Integer
            Public MenuWidth As Integer
            Public PaddedBorderWidth As Integer
            Public ScrollHeight As Integer
            Public ScrollWidth As Integer
            Public SmCaptionHeight As Integer
            Public SmCaptionWidth As Integer
            Public DesktopIconSize As Integer
            Public ShellIconSize As Integer
            Public ShellSmallIconSize As Integer
            Public Fonts_SingleBitPP As Boolean

            Public CaptionFont As Font
            Public IconFont As Font
            Public MenuFont As Font
            Public MessageFont As Font
            Public SmCaptionFont As Font
            Public StatusFont As Font
            Public FontSubstitute_MSShellDlg As String
            Public FontSubstitute_MSShellDlg2 As String
            Public FontSubstitute_SegoeUI As String

            Shared Operator =(First As MetricsFonts, Second As MetricsFonts) As Boolean
                Return First.Equals(Second)
            End Operator

            Shared Operator <>(First As MetricsFonts, Second As MetricsFonts) As Boolean
                Return Not First.Equals(Second)
            End Operator

            Public Function Clone() Implements ICloneable.Clone
                Return MemberwiseClone()
            End Function

            Sub Overwrite_Metrics(vs As VisualStyleMetrics)
                CaptionHeight = vs.Sizes.CaptionBarHeight
                ScrollHeight = vs.Sizes.ScrollbarHeight
                ScrollWidth = vs.Sizes.ScrollbarWidth
                SmCaptionHeight = vs.Sizes.SMCaptionBarHeight
                SmCaptionWidth = vs.Sizes.SMCaptionBarWidth
            End Sub

            Sub Overwrite_Fonts(vs As VisualStyleMetrics)
                CaptionFont = vs.Fonts.CaptionFont
                IconFont = vs.Fonts.IconTitleFont
                MenuFont = vs.Fonts.MenuFont
                SmCaptionFont = vs.Fonts.SmallCaptionFont
                MessageFont = vs.Fonts.MsgBoxFont
                StatusFont = vs.Fonts.StatusFont
            End Sub

            Public Sub Load(_DefMetricsFonts As MetricsFonts)
                Enabled = GetReg("HKEY_CURRENT_USER\Software\WinPaletter\Metrics", "", _DefMetricsFonts.Enabled)

                BorderWidth = GetReg("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "BorderWidth", _DefMetricsFonts.BorderWidth * -15) / -15
                CaptionHeight = GetReg("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "CaptionHeight", _DefMetricsFonts.CaptionHeight * -15) / -15
                CaptionWidth = GetReg("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "CaptionWidth", _DefMetricsFonts.CaptionWidth * -15) / -15
                IconSpacing = GetReg("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "IconSpacing", _DefMetricsFonts.IconSpacing * -15) / -15
                IconVerticalSpacing = GetReg("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "IconVerticalSpacing", _DefMetricsFonts.IconVerticalSpacing * -15) / -15
                MenuHeight = GetReg("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "MenuHeight", _DefMetricsFonts.MenuHeight * -15) / -15
                MenuWidth = GetReg("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "MenuWidth", _DefMetricsFonts.MenuWidth * -15) / -15
                PaddedBorderWidth = GetReg("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "PaddedBorderWidth", _DefMetricsFonts.PaddedBorderWidth * -15) / -15
                ScrollHeight = GetReg("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "ScrollHeight", _DefMetricsFonts.ScrollHeight * -15) / -15
                ScrollWidth = GetReg("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "ScrollWidth", _DefMetricsFonts.ScrollWidth * -15) / -15
                SmCaptionHeight = GetReg("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "SmCaptionHeight", _DefMetricsFonts.SmCaptionHeight * -15) / -15
                SmCaptionWidth = GetReg("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "SmCaptionWidth", _DefMetricsFonts.SmCaptionWidth * -15) / -15

                If My.WXP Then
                    Try
                        ShellIconSize = GetReg("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "Shell Icon Size", _DefMetricsFonts.ShellIconSize)
                    Catch
                        ShellIconSize = _DefMetricsFonts.ShellIconSize
                    End Try

                    Try
                        ShellSmallIconSize = GetReg("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "Shell Small Icon Size", _DefMetricsFonts.ShellSmallIconSize)
                    Catch
                        ShellSmallIconSize = _DefMetricsFonts.ShellSmallIconSize
                    End Try
                End If

                DesktopIconSize = GetReg("HKEY_CURRENT_USER\Software\Microsoft\Windows\Shell\Bags\1\Desktop", "IconSize", _DefMetricsFonts.DesktopIconSize)
                CaptionFont = DirectCast(GetReg("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "CaptionFont", _DefMetricsFonts.CaptionFont.ToByte), Byte()).ToFont
                IconFont = DirectCast(GetReg("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "IconFont", _DefMetricsFonts.IconFont.ToByte), Byte()).ToFont
                MenuFont = DirectCast(GetReg("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "MenuFont", _DefMetricsFonts.MenuFont.ToByte), Byte()).ToFont
                MessageFont = DirectCast(GetReg("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "MessageFont", _DefMetricsFonts.MessageFont.ToByte), Byte()).ToFont
                SmCaptionFont = DirectCast(GetReg("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "SmCaptionFont", _DefMetricsFonts.SmCaptionFont.ToByte), Byte()).ToFont
                StatusFont = DirectCast(GetReg("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "StatusFont", _DefMetricsFonts.StatusFont.ToByte), Byte()).ToFont
                FontSubstitute_MSShellDlg = GetReg("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\FontSubstitutes", "MS Shell Dlg", _DefMetricsFonts.FontSubstitute_MSShellDlg)
                FontSubstitute_MSShellDlg2 = GetReg("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\FontSubstitutes", "MS Shell Dlg 2", _DefMetricsFonts.FontSubstitute_MSShellDlg2)
                FontSubstitute_SegoeUI = GetReg("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\FontSubstitutes", "Segoe UI", _DefMetricsFonts.FontSubstitute_SegoeUI)

                If Core.GetWindowsScreenScalingFactor > 100 Then
                    CaptionFont = AdjustFont(CaptionFont)
                    IconFont = AdjustFont(IconFont)
                    MenuFont = AdjustFont(MenuFont)
                    MessageFont = AdjustFont(MessageFont)
                    SmCaptionFont = AdjustFont(SmCaptionFont)
                    StatusFont = AdjustFont(StatusFont)
                End If

                Dim temp As Boolean
                Fixer.SystemParametersInfo(SPI.Fonts.GETFONTSMOOTHING, Nothing, temp, SPIF.None)
                Fonts_SingleBitPP = Not temp OrElse GetReg("HKEY_CURRENT_USER\Control Panel\Desktop", "FontSmoothingType", If(My.WXP, 1, 2)) <> 2
            End Sub

            Private Function AdjustFont([Font] As Font) As Font
                Dim DPI As Integer = Core.GetWindowsScreenScalingFactor
                If DPI > 0 Then
                    Dim font_size As Single = [Font].Size * (100 / DPI)
                    If font_size > 0 Then
                        Return New Font([Font].Name, font_size, [Font].Style, GraphicsUnit.Pixel)
                    Else
                        Return [Font]
                    End If
                Else
                    Return [Font]
                End If
            End Function

            Public Sub Apply(Optional [TreeView] As TreeView = Nothing)
                EditReg([TreeView], "HKEY_CURRENT_USER\Software\WinPaletter\Metrics", "", Enabled)

                If Enabled Then
                    If Core.GetWindowsScreenScalingFactor > 100 Then
                        CaptionFont = New Font(CaptionFont.Name, CaptionFont.SizeInPoints, CaptionFont.Style)
                        IconFont = New Font(IconFont.Name, IconFont.SizeInPoints, IconFont.Style)
                        MenuFont = New Font(MenuFont.Name, MenuFont.SizeInPoints, MenuFont.Style)
                        MessageFont = New Font(MessageFont.Name, MessageFont.SizeInPoints, MessageFont.Style)
                        SmCaptionFont = New Font(SmCaptionFont.Name, SmCaptionFont.SizeInPoints, SmCaptionFont.Style)
                        StatusFont = New Font(StatusFont.Name, StatusFont.SizeInPoints, StatusFont.Style)
                    End If

                    Dim lfCaptionFont As New LogFont : CaptionFont.ToLogFont(lfCaptionFont)
                    Dim lfIconFont As New LogFont : IconFont.ToLogFont(lfIconFont)
                    Dim lfMenuFont As New LogFont : MenuFont.ToLogFont(lfMenuFont)
                    Dim lfMessageFont As New LogFont : MessageFont.ToLogFont(lfMessageFont)
                    Dim lfSMCaptionFont As New LogFont : SmCaptionFont.ToLogFont(lfSMCaptionFont)
                    Dim lfStatusFont As New LogFont : StatusFont.ToLogFont(lfStatusFont)

                    EditReg([TreeView], "HKEY_CURRENT_USER\Control Panel\Desktop", "FontSmoothing", If(Not Fonts_SingleBitPP, 2, 0))
                    EditReg([TreeView], "HKEY_CURRENT_USER\Control Panel\Desktop", "FontSmoothingType", If(Not Fonts_SingleBitPP, 2, 1))

                    If [TreeView] IsNot Nothing Then AddNode([TreeView], String.Format(My.Lang.Verbose_SettingFontSmoothing, Not Fonts_SingleBitPP), "dll")
                    SystemParametersInfo(SPI.Fonts.SETFONTSMOOTHING, Not Fonts_SingleBitPP, Nothing, SPIF.UpdateINIFile)

                    If Not My.Settings.ThemeApplyingBehavior.DelayMetrics Then
                        Dim NCM As New NONCLIENTMETRICS With {.cbSize = Marshal.SizeOf(NCM)}
                        Dim ICO As New ICONMETRICS With {.cbSize = Marshal.SizeOf(ICO)}

                        SystemParametersInfo(SPI.Icons.GETICONMETRICS, ICO.cbSize, ICO, SPIF.None)

                        With NCM
                            .lfCaptionFont = lfCaptionFont
                            .lfSMCaptionFont = lfSMCaptionFont
                            .lfStatusFont = lfStatusFont
                            .lfMenuFont = lfMenuFont
                            .lfMessageFont = lfMessageFont

                            .iBorderWidth = BorderWidth
                            .iScrollWidth = ScrollWidth
                            .iScrollHeight = ScrollHeight
                            .iCaptionWidth = CaptionWidth
                            .iCaptionHeight = CaptionHeight
                            .iSMCaptionWidth = SmCaptionWidth
                            .iSMCaptionHeight = SmCaptionHeight
                            .iMenuWidth = MenuWidth
                            .iMenuHeight = MenuHeight
                            .iPaddedBorderWidth = PaddedBorderWidth
                        End With

                        With ICO
                            .iHorzSpacing = IconSpacing
                            .iVertSpacing = IconVerticalSpacing
                            .lfFont = lfIconFont
                        End With

                        If [TreeView] IsNot Nothing Then AddNode([TreeView], My.Lang.Verbose_SettingMetricsFonts, "dll")
                        SystemParametersInfo(SPI.Metrics.SETNONCLIENTMETRICS, Marshal.SizeOf(NCM), NCM, SPIF.UpdateINIFile)

                        If [TreeView] IsNot Nothing Then AddNode([TreeView], My.Lang.Verbose_SettingIconsMetrics, "dll")
                        SystemParametersInfo(SPI.Icons.SETICONMETRICS, Marshal.SizeOf(ICO), ICO, SPIF.UpdateINIFile)

                    Else
                        EditReg([TreeView], "HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "CaptionFont", lfCaptionFont.ToByte, RegistryValueKind.Binary)
                        EditReg([TreeView], "HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "IconFont", lfIconFont.ToByte, RegistryValueKind.Binary)
                        EditReg([TreeView], "HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "MenuFont", lfMenuFont.ToByte, RegistryValueKind.Binary)
                        EditReg([TreeView], "HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "MessageFont", lfMessageFont.ToByte, RegistryValueKind.Binary)
                        EditReg([TreeView], "HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "SmCaptionFont", lfSMCaptionFont.ToByte, RegistryValueKind.Binary)
                        EditReg([TreeView], "HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "StatusFont", lfStatusFont.ToByte, RegistryValueKind.Binary)
                        EditReg([TreeView], "HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "BorderWidth", BorderWidth * -15, RegistryValueKind.String)
                        EditReg([TreeView], "HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "CaptionHeight", CaptionHeight * -15, RegistryValueKind.String)
                        EditReg([TreeView], "HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "CaptionWidth", CaptionWidth * -15, RegistryValueKind.String)
                        EditReg([TreeView], "HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "IconSpacing", IconSpacing * -15, RegistryValueKind.String)
                        EditReg([TreeView], "HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "IconVerticalSpacing", IconVerticalSpacing * -15, RegistryValueKind.String)
                        EditReg([TreeView], "HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "MenuHeight", MenuHeight * -15, RegistryValueKind.String)
                        EditReg([TreeView], "HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "MenuWidth", MenuWidth * -15, RegistryValueKind.String)
                        EditReg([TreeView], "HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "PaddedBorderWidth", PaddedBorderWidth * -15, RegistryValueKind.String)
                        EditReg([TreeView], "HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "ScrollHeight", ScrollHeight * -15, RegistryValueKind.String)
                        EditReg([TreeView], "HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "ScrollWidth", ScrollWidth * -15, RegistryValueKind.String)
                        EditReg([TreeView], "HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "SmCaptionHeight", SmCaptionHeight * -15, RegistryValueKind.String)
                        EditReg([TreeView], "HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "SmCaptionWidth", SmCaptionWidth * -15, RegistryValueKind.String)
                    End If

                    If My.WXP Then
                        EditReg([TreeView], "HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "Shell Icon Size", ShellIconSize, RegistryValueKind.String)
                        EditReg([TreeView], "HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "Shell Small Icon Size", ShellSmallIconSize, RegistryValueKind.String)
                    End If

                    EditReg([TreeView], "HKEY_CURRENT_USER\Software\Microsoft\Windows\Shell\Bags\1\Desktop", "IconSize", DesktopIconSize, RegistryValueKind.String)

                    If My.Settings.ThemeApplyingBehavior.Metrics_HKU_DEFAULT_Prefs = WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite Then
                        EditReg([TreeView], "HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics", "CaptionFont", lfCaptionFont.ToByte, RegistryValueKind.Binary)
                        EditReg([TreeView], "HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics", "IconFont", lfIconFont.ToByte, RegistryValueKind.Binary)
                        EditReg([TreeView], "HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics", "MenuFont", lfMenuFont.ToByte, RegistryValueKind.Binary)
                        EditReg([TreeView], "HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics", "MessageFont", lfMessageFont.ToByte, RegistryValueKind.Binary)
                        EditReg([TreeView], "HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics", "SmCaptionFont", lfSMCaptionFont.ToByte, RegistryValueKind.Binary)
                        EditReg([TreeView], "HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics", "StatusFont", lfStatusFont.ToByte, RegistryValueKind.Binary)
                        EditReg([TreeView], "HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics", "BorderWidth", BorderWidth * -15, RegistryValueKind.String)
                        EditReg([TreeView], "HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics", "CaptionHeight", CaptionHeight * -15, RegistryValueKind.String)
                        EditReg([TreeView], "HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics", "CaptionWidth", CaptionWidth * -15, RegistryValueKind.String)
                        EditReg([TreeView], "HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics", "IconSpacing", IconSpacing * -15, RegistryValueKind.String)
                        EditReg([TreeView], "HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics", "IconVerticalSpacing", IconVerticalSpacing * -15, RegistryValueKind.String)
                        EditReg([TreeView], "HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics", "MenuHeight", MenuHeight * -15, RegistryValueKind.String)
                        EditReg([TreeView], "HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics", "MenuWidth", MenuWidth * -15, RegistryValueKind.String)
                        EditReg([TreeView], "HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics", "PaddedBorderWidth", PaddedBorderWidth * -15, RegistryValueKind.String)
                        EditReg([TreeView], "HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics", "ScrollHeight", ScrollHeight * -15, RegistryValueKind.String)
                        EditReg([TreeView], "HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics", "ScrollWidth", ScrollWidth * -15, RegistryValueKind.String)
                        EditReg([TreeView], "HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics", "SmCaptionHeight", SmCaptionHeight * -15, RegistryValueKind.String)
                        EditReg([TreeView], "HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics", "SmCaptionWidth", SmCaptionWidth * -15, RegistryValueKind.String)
                        EditReg([TreeView], "HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics", "Shell Icon Size", ShellIconSize, RegistryValueKind.String)
                        EditReg([TreeView], "HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics", "Shell Small Icon Size", ShellSmallIconSize, RegistryValueKind.String)
                        EditReg([TreeView], "HKEY_USERS\.DEFAULT\Software\Microsoft\Windows\Shell\Bags\1\Desktop", "IconSize", DesktopIconSize, RegistryValueKind.String)
                        EditReg([TreeView], "HKEY_USERS\.DEFAULT\Control Panel\Desktop", "FontSmoothing", If(Not Fonts_SingleBitPP, 2, 0))
                        EditReg([TreeView], "HKEY_USERS\.DEFAULT\Control Panel\Desktop", "FontSmoothingType", If(Not Fonts_SingleBitPP, 2, 1))
                    End If

                    EditReg([TreeView], "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\FontSubstitutes", "MS Shell Dlg", FontSubstitute_MSShellDlg, RegistryValueKind.String)
                    EditReg([TreeView], "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\FontSubstitutes", "MS Shell Dlg 2", FontSubstitute_MSShellDlg2, RegistryValueKind.String)

                    If String.IsNullOrWhiteSpace(FontSubstitute_SegoeUI) Then
                        EditReg([TreeView], "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI (TrueType)", "segoeui.ttf", RegistryValueKind.String)
                        EditReg([TreeView], "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Bold (TrueType)", "segoeuib.ttf", RegistryValueKind.String)
                        EditReg([TreeView], "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Bold Italic (TrueType)", "segoeuiz.ttf", RegistryValueKind.String)
                        EditReg([TreeView], "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Black (TrueType)", "seguibl.ttf", RegistryValueKind.String)
                        EditReg([TreeView], "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Black Italic (TrueType)", "seguibli.ttf", RegistryValueKind.String)
                        EditReg([TreeView], "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Italic (TrueType)", "segoeuii.ttf", RegistryValueKind.String)
                        EditReg([TreeView], "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Light (TrueType)", "segoeuil.ttf", RegistryValueKind.String)
                        EditReg([TreeView], "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Light Italic (TrueType)", "seguili.ttf", RegistryValueKind.String)
                        EditReg([TreeView], "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Semibold (TrueType)", "seguisb.ttf", RegistryValueKind.String)
                        EditReg([TreeView], "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Semibold Italic (TrueType)", "seguisbi.ttf", RegistryValueKind.String)
                        EditReg([TreeView], "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Semilight (TrueType)", "segoeuisl.ttf", RegistryValueKind.String)
                        EditReg([TreeView], "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Semilight Italic (TrueType)", "seguisli.ttf", RegistryValueKind.String)
                    Else
                        EditReg([TreeView], "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI (TrueType)", "", RegistryValueKind.String)
                        EditReg([TreeView], "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Bold (TrueType)", "", RegistryValueKind.String)
                        EditReg([TreeView], "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Bold Italic (TrueType)", "", RegistryValueKind.String)
                        EditReg([TreeView], "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Black (TrueType)", "", RegistryValueKind.String)
                        EditReg([TreeView], "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Black Italic (TrueType)", "", RegistryValueKind.String)
                        EditReg([TreeView], "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Italic (TrueType)", "", RegistryValueKind.String)
                        EditReg([TreeView], "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Light (TrueType)", "", RegistryValueKind.String)
                        EditReg([TreeView], "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Light Italic (TrueType)", "", RegistryValueKind.String)
                        EditReg([TreeView], "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Semibold (TrueType)", "", RegistryValueKind.String)
                        EditReg([TreeView], "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Semibold Italic (TrueType)", "", RegistryValueKind.String)
                        EditReg([TreeView], "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Semilight (TrueType)", "", RegistryValueKind.String)
                        EditReg([TreeView], "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Semilight Italic (TrueType)", "", RegistryValueKind.String)
                    End If
                    EditReg([TreeView], "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\FontSubstitutes", "Segoe UI", FontSubstitute_SegoeUI, RegistryValueKind.String)

                End If

            End Sub
        End Structure
        Structure WinEffects : Implements ICloneable
            Public Enabled As Boolean

            Public WindowAnimation As Boolean
            Public WindowShadow As Boolean
            Public WindowUIEffects As Boolean
            Public ShowWinContentDrag As Boolean

            Public MenuAnimation As Boolean
            Public MenuFade As MenuAnimType
            Public MenuSelectionFade As Boolean
            Public MenuShowDelay As UInteger            'Microsoft uses this as DWORD, which its equivalent is UInteger, not Integer

            Public ComboBoxAnimation As Boolean
            Public ListBoxSmoothScrolling As Boolean

            Public TooltipAnimation As Boolean
            Public TooltipFade As MenuAnimType

            Public IconsShadow As Boolean
            Public IconsDesktopTranslSel As Boolean

            Public KeyboardUnderline As Boolean
            Public FocusRectWidth As UInteger           'Microsoft uses this as DWORD, which its equivalent is UInteger, not Integer
            Public FocusRectHeight As UInteger          'Microsoft uses this as DWORD, which its equivalent is UInteger, not Integer
            Public Caret As UInteger
            Public NotificationDuration As Integer
            Public ShakeToMinimize As Boolean
            Public AWT_Enabled As Boolean
            Public AWT_BringActivatedWindowToTop As Boolean
            Public AWT_Delay As Integer
            Public SnapCursorToDefButton As Boolean

            Public Win11ClassicContextMenu As Boolean
            Public SysListView32 As Boolean
            Public ShowSecondsInSystemClock As Boolean
            Public BalloonNotifications As Boolean
            Public PaintDesktopVersion As Boolean

            Public Win11BootDots As Boolean
            Public Win11ExplorerBar As ExplorerBar
            Public DisableNavBar As Boolean

            Public AutoHideScrollBars As Boolean
            Public FullScreenStartMenu As Boolean
            Public ColorFilter_Enabled As Boolean
            Public ColorFilter As ColorFilters

            Public ClassicVolMixer As Boolean

            Enum ExplorerBar
                [Default]
                Ribbon
                Bar
            End Enum

            Enum ColorFilters
                Grayscale
                Inverted
                GrayscaleInverted
                RedGreen_deuteranopia
                RedGreen_protanopia
                BlueYellow
            End Enum

            Enum MenuAnimType
                Fade
                Scroll
            End Enum

            Sub Load(_DefEffects As WinEffects)
                Enabled = GetReg("HKEY_CURRENT_USER\Software\WinPaletter\WindowsEffects", "", True)

                If Fixer.SystemParametersInfo(SPI.Effects.GETDROPSHADOW, 0, WindowShadow, SPIF.None) = 0 Then WindowShadow = _DefEffects.WindowShadow
                If Fixer.SystemParametersInfo(SPI.Effects.GETUIEFFECTS, 0, WindowUIEffects, SPIF.None) = 0 Then WindowUIEffects = _DefEffects.WindowUIEffects
                If Fixer.SystemParametersInfo(SPI.Effects.GETMENUANIMATION, 0, MenuAnimation, SPIF.None) = 0 Then MenuAnimation = _DefEffects.MenuAnimation
                If Fixer.SystemParametersInfo(SPI.Effects.GETSELECTIONFADE, 0, MenuSelectionFade, SPIF.None) = 0 Then MenuSelectionFade = _DefEffects.MenuSelectionFade
                If Fixer.SystemParametersInfo(SPI.Effects.GETMENUSHOWDELAY, 0, MenuShowDelay, SPIF.None) = 0 Then MenuShowDelay = _DefEffects.MenuShowDelay
                If Fixer.SystemParametersInfo(SPI.Effects.GETCOMBOBOXANIMATION, 0, ComboBoxAnimation, SPIF.None) = 0 Then ComboBoxAnimation = _DefEffects.ComboBoxAnimation
                If Fixer.SystemParametersInfo(SPI.Effects.GETLISTBOXSMOOTHSCROLLING, 0, ListBoxSmoothScrolling, SPIF.None) = 0 Then ListBoxSmoothScrolling = _DefEffects.ListBoxSmoothScrolling
                If Fixer.SystemParametersInfo(SPI.Effects.GETTOOLTIPANIMATION, 0, TooltipAnimation, SPIF.None) = 0 Then TooltipAnimation = _DefEffects.TooltipAnimation
                If Fixer.SystemParametersInfo(SPI.Effects.GETDRAGFULLWINDOWS, 0, ShowWinContentDrag, SPIF.None) = 0 Then ShowWinContentDrag = _DefEffects.ShowWinContentDrag
                If Fixer.SystemParametersInfo(SPI.Effects.GETMENUUNDERLINES, 0, KeyboardUnderline, SPIF.None) = 0 Then KeyboardUnderline = _DefEffects.KeyboardUnderline
                If Fixer.SystemParametersInfo(SPI.FocusRect.GETFOCUSBORDERWIDTH, 0, FocusRectWidth, SPIF.None) = 0 Then FocusRectWidth = _DefEffects.FocusRectWidth
                If Fixer.SystemParametersInfo(SPI.FocusRect.GETFOCUSBORDERHEIGHT, 0, FocusRectHeight, SPIF.None) = 0 Then FocusRectHeight = _DefEffects.FocusRectHeight
                If Fixer.SystemParametersInfo(SPI.Effects.GETCARETWIDTH, 0, Caret, SPIF.None) = 0 Then Caret = _DefEffects.Caret
                If Fixer.SystemParametersInfo(SPI.Effects.GETACTIVEWINDOWTRACKING, 0, AWT_Enabled, SPIF.None) = 0 Then AWT_Enabled = _DefEffects.AWT_Enabled
                If Fixer.SystemParametersInfo(SPI.Effects.GETACTIVEWNDTRKZORDER, 0, AWT_BringActivatedWindowToTop, SPIF.None) = 0 Then AWT_BringActivatedWindowToTop = _DefEffects.AWT_BringActivatedWindowToTop
                If Fixer.SystemParametersInfo(SPI.Effects.GETACTIVEWNDTRKTIMEOUT, 0, AWT_Delay, SPIF.None) = 0 Then AWT_Delay = _DefEffects.AWT_Delay
                If Fixer.SystemParametersInfo(SPI.Cursors.GETSNAPTODEFBUTTON, 0, SnapCursorToDefButton, SPIF.None) = 0 Then SnapCursorToDefButton = _DefEffects.SnapCursorToDefButton

                Dim anim As New ANIMATIONINFO With {.cbSize = Marshal.SizeOf(anim)}
                If SystemParametersInfo(SPI.Effects.GETANIMATION, anim.cbSize, anim, SPIF.None) = 1 Then
                    WindowAnimation = anim.IMinAnimate.ToBoolean
                Else
                    WindowAnimation = _DefEffects.WindowAnimation
                End If

                Dim x As Boolean

                If Fixer.SystemParametersInfo(SPI.Effects.GETMENUFADE, 0, x, SPIF.None) = 1 Then
                    MenuFade = If(x, MenuAnimType.Fade, MenuAnimType.Scroll)
                Else
                    MenuFade = _DefEffects.MenuFade
                End If

                If Fixer.SystemParametersInfo(SPI.Effects.GETTOOLTIPFADE, 0, x, SPIF.None) = 1 Then
                    TooltipFade = If(x, MenuAnimType.Fade, MenuAnimType.Scroll)
                Else
                    TooltipFade = _DefEffects.TooltipFade
                End If

                IconsShadow = GetReg("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "ListviewShadow", _DefEffects.IconsShadow)
                IconsDesktopTranslSel = GetReg("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "ListviewAlphaSelect", _DefEffects.IconsDesktopTranslSel)
                NotificationDuration = GetReg("HKEY_CURRENT_USER\Control Panel\Accessibility", "MessageDuration", _DefEffects.NotificationDuration)
                ShowSecondsInSystemClock = GetReg("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "ShowSecondsInSystemClock", _DefEffects.ShowSecondsInSystemClock)
                BalloonNotifications = GetReg("HKEY_CURRENT_USER\Software\Policies\Microsoft\Windows\Explorer", "EnableLegacyBalloonNotifications", _DefEffects.BalloonNotifications)
                PaintDesktopVersion = GetReg("HKEY_CURRENT_USER\Control Panel\Desktop", "PaintDesktopVersion", _DefEffects.PaintDesktopVersion)
                ClassicVolMixer = Not GetReg("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\MTCUVC", "EnableMtcUvc", Not _DefEffects.ClassicVolMixer)

                Dim temp As Boolean = GetReg("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "DisallowShaking", Not _DefEffects.ShakeToMinimize)
                ShakeToMinimize = Not temp

                Try
                    If My.Computer.Registry.CurrentUser.OpenSubKey("Software\Classes\CLSID").OpenSubKey("{86ca1aa0-34aa-4e8b-a509-50c905bae2a2}\InprocServer32") IsNot Nothing Then
                        Win11ClassicContextMenu = True
                    Else
                        Win11ClassicContextMenu = False
                    End If
                Catch
                    Win11ClassicContextMenu = _DefEffects.Win11ClassicContextMenu
                Finally
                    My.Computer.Registry.CurrentUser.Close()
                End Try

                Try
                    If My.Computer.Registry.CurrentUser.OpenSubKey("Software\Classes\CLSID").OpenSubKey("{1eeb5b5a-06fb-4732-96b3-975c0194eb39}\InprocServer32") IsNot Nothing Then
                        SysListView32 = True
                    Else
                        SysListView32 = False
                    End If
                Catch
                    SysListView32 = _DefEffects.SysListView32
                Finally
                    My.Computer.Registry.CurrentUser.Close()
                End Try

                If GetReg("HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\BootControl", "BootProgressAnimation", Nothing) Is Nothing Then
                    Win11BootDots = Not My.W11

                Else
                    Select Case GetReg("HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\BootControl", "BootProgressAnimation", If(My.W11, 1, 0))
                        Case 0
                            Win11BootDots = True

                        Case 1
                            Win11BootDots = False

                        Case Else
                            Win11BootDots = False

                    End Select
                End If

                Win11ExplorerBar = GetReg("HKEY_CURRENT_USER\Software\WinPaletter\WindowsEffects", "Win11ExplorerBar", _DefEffects.Win11ExplorerBar)

                Try
                    If My.Computer.Registry.CurrentUser.OpenSubKey("Software\Classes\CLSID").OpenSubKey("{056440FD-8568-48e7-A632-72157243B55B}\InprocServer32") IsNot Nothing Then
                        DisableNavBar = True
                    Else
                        DisableNavBar = False
                    End If
                Catch
                    DisableNavBar = _DefEffects.DisableNavBar
                Finally
                    My.Computer.Registry.CurrentUser.Close()
                End Try

                AutoHideScrollBars = GetReg("HKEY_CURRENT_USER\Control Panel\Accessibility", "DynamicScrollbars", _DefEffects.AutoHideScrollBars)
                FullScreenStartMenu = If(GetReg("HKEY_CURRENT_USER\SOFTWARE\Policies\Microsoft\Windows\Explorer", "ForceStartSize", If(_DefEffects.FullScreenStartMenu, 2, 0)) = 2, True, False)
                ColorFilter_Enabled = GetReg("HKEY_CURRENT_USER\Software\Microsoft\ColorFiltering", "Active", _DefEffects.ColorFilter_Enabled)
                ColorFilter = GetReg("HKEY_CURRENT_USER\Software\Microsoft\ColorFiltering", "FilterType", _DefEffects.ColorFilter)
            End Sub

            Sub Apply(Optional [TreeView] As TreeView = Nothing)
                EditReg([TreeView], "HKEY_CURRENT_USER\Software\WinPaletter\WindowsEffects", "", Enabled)

                If Enabled Then
                    Dim anim As New ANIMATIONINFO With {.cbSize = Marshal.SizeOf(anim), .IMinAnimate = WindowAnimation.ToInteger}
                    If [TreeView] IsNot Nothing Then AddNode([TreeView], String.Format(My.Lang.Verbose_SettingAnimation, anim.IMinAnimate.ToBoolean), "dll")
                    SystemParametersInfo(SPI.Effects.SETANIMATION, anim.cbSize, anim, SPIF.UpdateINIFile)

                    If [TreeView] IsNot Nothing Then AddNode([TreeView], String.Format(My.Lang.Verbose_SettingDropShadow, WindowShadow), "dll")
                    SystemParametersInfo(SPI.Effects.SETDROPSHADOW, 0, WindowShadow, SPIF.UpdateINIFile)

                    If [TreeView] IsNot Nothing Then AddNode([TreeView], String.Format(My.Lang.Verbose_SettingUIEffects, WindowUIEffects), "dll")
                    SystemParametersInfo(SPI.Effects.SETUIEFFECTS, 0, WindowUIEffects, SPIF.UpdateINIFile)

                    If [TreeView] IsNot Nothing Then AddNode([TreeView], String.Format(My.Lang.Verbose_SettingShowWinContentDragging, ShowWinContentDrag), "dll")
                    SystemParametersInfo(SPI.Effects.SETDRAGFULLWINDOWS, ShowWinContentDrag, 0, SPIF.UpdateINIFile)        'use uiParam not pvParam

                    If [TreeView] IsNot Nothing Then AddNode([TreeView], String.Format(My.Lang.Verbose_SettingMenuAnimation, MenuAnimation), "dll")
                    SystemParametersInfo(SPI.Effects.SETMENUANIMATION, 0, MenuAnimation, SPIF.UpdateINIFile)

                    If [TreeView] IsNot Nothing Then AddNode([TreeView], String.Format(My.Lang.Verbose_SettingMenuFade, MenuFade), "dll")
                    SystemParametersInfo(SPI.Effects.SETMENUFADE, 0, MenuFade = MenuAnimType.Fade, SPIF.UpdateINIFile)

                    If [TreeView] IsNot Nothing Then AddNode([TreeView], String.Format(My.Lang.Verbose_SettingMenuDelay, MenuShowDelay), "dll")
                    SystemParametersInfo(SPI.Effects.SETMENUSHOWDELAY, MenuShowDelay, 0, SPIF.UpdateINIFile)               'use uiParam not pvParam

                    If [TreeView] IsNot Nothing Then AddNode([TreeView], String.Format(My.Lang.Verbose_SettingMenuSelectionFade, MenuSelectionFade), "dll")
                    SystemParametersInfo(SPI.Effects.SETSELECTIONFADE, 0, MenuSelectionFade, SPIF.UpdateINIFile)

                    If [TreeView] IsNot Nothing Then AddNode([TreeView], String.Format(My.Lang.Verbose_SettingComboBoxAnimation, ComboBoxAnimation), "dll")
                    SystemParametersInfo(SPI.Effects.SETCOMBOBOXANIMATION, 0, ComboBoxAnimation, SPIF.UpdateINIFile)

                    If [TreeView] IsNot Nothing Then AddNode([TreeView], String.Format(My.Lang.Verbose_SettingListBoxSmoothScrolling, ListBoxSmoothScrolling), "dll")
                    SystemParametersInfo(SPI.Effects.SETLISTBOXSMOOTHSCROLLING, 0, ListBoxSmoothScrolling, SPIF.UpdateINIFile)

                    If [TreeView] IsNot Nothing Then AddNode([TreeView], String.Format(My.Lang.Verbose_SettingTooltipAnimation, TooltipAnimation), "dll")
                    SystemParametersInfo(SPI.Effects.SETTOOLTIPANIMATION, 0, TooltipAnimation, SPIF.UpdateINIFile)

                    If [TreeView] IsNot Nothing Then AddNode([TreeView], String.Format(My.Lang.Verbose_SettingTooltipFade, TooltipFade.ToString), "dll")
                    SystemParametersInfo(SPI.Effects.SETTOOLTIPFADE, 0, TooltipFade = MenuAnimType.Fade, SPIF.UpdateINIFile)

                    If [TreeView] IsNot Nothing Then AddNode([TreeView], String.Format(My.Lang.Verbose_SettingMenuUnderline, KeyboardUnderline), "dll")
                    SystemParametersInfo(SPI.Effects.SETMENUUNDERLINES, 0, KeyboardUnderline, SPIF.UpdateINIFile)

                    If [TreeView] IsNot Nothing Then AddNode([TreeView], String.Format(My.Lang.Verbose_SettingFocusRectW, FocusRectWidth), "dll")
                    SystemParametersInfo(SPI.FocusRect.SETFOCUSBORDERWIDTH, 0, FocusRectWidth, SPIF.UpdateINIFile)

                    If [TreeView] IsNot Nothing Then AddNode([TreeView], String.Format(My.Lang.Verbose_SettingFocusRectH, FocusRectHeight), "dll")
                    SystemParametersInfo(SPI.FocusRect.SETFOCUSBORDERHEIGHT, 0, FocusRectHeight, SPIF.UpdateINIFile)

                    If [TreeView] IsNot Nothing Then AddNode([TreeView], String.Format(My.Lang.Verbose_SettingCaretRect, Caret), "dll")
                    SystemParametersInfo(SPI.Effects.SETCARETWIDTH, 0, Caret, SPIF.UpdateINIFile)

                    If [TreeView] IsNot Nothing Then AddNode([TreeView], String.Format(My.Lang.Verbose_SettingAWT, AWT_Enabled), "dll")
                    SystemParametersInfo(SPI.Effects.SETACTIVEWINDOWTRACKING, 0, AWT_Enabled, SPIF.UpdateINIFile)

                    If [TreeView] IsNot Nothing Then AddNode([TreeView], String.Format(My.Lang.Verbose_SettingAWT_ActivatedWindowTop, AWT_BringActivatedWindowToTop), "dll")
                    SystemParametersInfo(SPI.Effects.SETACTIVEWNDTRKZORDER, 0, AWT_BringActivatedWindowToTop, SPIF.UpdateINIFile)

                    If [TreeView] IsNot Nothing Then AddNode([TreeView], String.Format(My.Lang.Verbose_SettingAWT_Delay, AWT_Delay), "dll")
                    SystemParametersInfo(SPI.Effects.SETACTIVEWNDTRKTIMEOUT, 0, AWT_Delay, SPIF.UpdateINIFile)

                    If [TreeView] IsNot Nothing Then AddNode([TreeView], String.Format(My.Lang.Verbose_SettingSnapCursorToDefault, SnapCursorToDefButton), "dll")
                    SystemParametersInfo(SPI.Cursors.SETSNAPTODEFBUTTON, SnapCursorToDefButton, 0, SPIF.UpdateINIFile)     'use uiParam not pvParam

                    EditReg([TreeView], "HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "ListviewShadow", IconsShadow.ToInteger)
                    EditReg([TreeView], "HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "ListviewAlphaSelect", IconsDesktopTranslSel.ToInteger)
                    EditReg([TreeView], "HKEY_CURRENT_USER\Control Panel\Desktop", "MenuShowDelay", MenuShowDelay, RegistryValueKind.String)
                    EditReg([TreeView], "HKEY_CURRENT_USER\Control Panel\Accessibility", "MessageDuration", NotificationDuration)
                    EditReg([TreeView], "HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "DisallowShaking", (Not ShakeToMinimize).ToInteger)
                    EditReg([TreeView], "HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "ShowSecondsInSystemClock", ShowSecondsInSystemClock.ToInteger)
                    EditReg([TreeView], "HKEY_CURRENT_USER\Control Panel\Desktop", "PaintDesktopVersion", PaintDesktopVersion.ToInteger)
                    EditReg([TreeView], "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\MTCUVC", "EnableMtcUvc", Not ClassicVolMixer)

                    EditReg([TreeView], "HKEY_CURRENT_USER\Control Panel\Accessibility", "DynamicScrollbars", AutoHideScrollBars)

                    EditReg([TreeView], "HKEY_CURRENT_USER\Software\Microsoft\ColorFiltering", "Active", ColorFilter_Enabled)
                    EditReg([TreeView], "HKEY_CURRENT_USER\Software\Microsoft\ColorFiltering", "FilterType", CInt(ColorFilter))
                    EditReg([TreeView], "HKEY_CURRENT_USER\Software\Microsoft\Windows NT\CurrentVersion\Accessibility", "Configuration", If(ColorFilter_Enabled, "colorfiltering", ""), RegistryValueKind.String)

                    If My.Settings.ThemeApplyingBehavior.UPM_HKU_DEFAULT Then
                        EditReg([TreeView], "HKEY_USERS\.DEFAULT\Control Panel\Desktop", "PaintDesktopVersion", PaintDesktopVersion.ToInteger)
                        EditReg([TreeView], "HKEY_USERS\.DEFAULT\Control Panel\Desktop", "CaretWidth", Caret)
                        EditReg([TreeView], "HKEY_USERS\.DEFAULT\Control Panel\Desktop", "MenuShowDelay", MenuShowDelay)
                        EditReg([TreeView], "HKEY_USERS\.DEFAULT\Control Panel\Mouse", "SnapToDefaultButton", SnapCursorToDefButton.ToInteger)
                    End If

                    Try
                        If My.Computer.Registry.CurrentUser.OpenSubKey("Software\ExplorerPatcher") IsNot Nothing Then
                            EditReg([TreeView], "HKEY_CURRENT_USER\Software\ExplorerPatcher", "FileExplorerCommandUI", Win11ExplorerBar)
                        End If
                    Catch
                        'Do nothing
                        My.Computer.Registry.CurrentUser.Close()
                    Finally
                        My.Computer.Registry.CurrentUser.Close()
                    End Try

                    Try
                        If My.Computer.Registry.CurrentUser.OpenSubKey("Software\StartIsBack") IsNot Nothing Then
                            EditReg([TreeView], "HKEY_CURRENT_USER\Software\StartIsBack", "FrameStyle", Win11ExplorerBar)
                        End If
                    Catch
                        'Do nothing
                        My.Computer.Registry.CurrentUser.Close()
                    Finally
                        My.Computer.Registry.CurrentUser.Close()
                    End Try

                    EditReg([TreeView], "HKEY_CURRENT_USER\Software\WinPaletter\WindowsEffects", "Win11ExplorerBar", Win11ExplorerBar)

                    If My.W11 Then EditReg([TreeView], "HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\BootControl", "BootProgressAnimation", (Not Win11BootDots).ToInteger)

                    If My.W8 Or My.W81 OrElse My.W10 Then
                        Select Case Win11ExplorerBar
                            Case ExplorerBar.Bar
                                If IO.File.Exists(My.PATH_System32 & "\UIRibbon.dll") Then
                                    If [TreeView] IsNot Nothing Then AddNode([TreeView], My.Lang.Verbose_EnableExplorerBar, "file_rename")

                                    Takeown_File(My.PATH_System32 & "\UIRibbon.dll")
                                    Move_File(My.PATH_System32 & "\UIRibbon.dll", My.PATH_System32 & "\UIRibbon.dll_bak")
                                End If

                                'DelReg_AdministratorDeflector("HKEY_LOCAL_MACHINE\SOFTWARE\Classes\CLSID", "{926749fa-2615-4987-8845-c33e65f2b957}")

                            Case Else
                                If IO.File.Exists(My.PATH_System32 & "\UIRibbon.dll_bak") Then
                                    If [TreeView] IsNot Nothing Then AddNode([TreeView], My.Lang.Verbose_RestoreExplorerBar, "file_rename")

                                    Takeown_File(My.PATH_System32 & "\UIRibbon.dll_bak")
                                    Takeown_File(My.PATH_System32 & "\UIRibbon.dll")
                                    Move_File(My.PATH_System32 & "\UIRibbon.dll_bak", My.PATH_System32 & "\UIRibbon.dll")
                                End If

                                'TakeOwn_Reg(Registry.LocalMachine.OpenSubKey("SOFTWARE\Classes\CLSID"), "{926749fa-2615-4987-8845-c33e65f2b957}")
                                'EditReg_CMD("HKEY_LOCAL_MACHINE\SOFTWARE\Classes\CLSID\{926749fa-2615-4987-8845-c33e65f2b957}", "", "%SystemRoot%\system32\UIRibbon.dll", RegistryValueKind.ExpandString)
                                'EditReg_CMD("HKEY_LOCAL_MACHINE\SOFTWARE\Classes\CLSID\{926749fa-2615-4987-8845-c33e65f2b957}", "ThreadingModel", "Apartment", RegistryValueKind.String)

                        End Select
                    End If

                    'Try ... Catch is used as sometimes access to HKEY_CURRENT_USER\Software\Policies is denied
                    Try
                        EditReg([TreeView], "HKEY_CURRENT_USER\Software\Policies\Microsoft\Windows\Explorer", "EnableLegacyBalloonNotifications", BalloonNotifications.ToInteger)
                    Catch
                        If [TreeView] IsNot Nothing Then AddNode([TreeView], "HKEY_CURRENT_USER\Software\Policies\Microsoft\Windows\Explorer > EnableLegacyBalloonNotifications = " & BalloonNotifications.ToInteger, "reg_add")
                        EditReg_CMD("HKEY_CURRENT_USER\Software\Policies\Microsoft\Windows\Explorer", "EnableLegacyBalloonNotifications", BalloonNotifications.ToInteger)
                    End Try

                    'Try ... Catch is used as sometimes access to HKEY_CURRENT_USER\Software\Policies is denied
                    Try
                        EditReg([TreeView], "HKEY_CURRENT_USER\SOFTWARE\Policies\Microsoft\Windows\Explorer", "ForceStartSize", If(FullScreenStartMenu, 2, 0))
                    Catch
                        If [TreeView] IsNot Nothing Then AddNode([TreeView], "HKEY_CURRENT_USER\Software\Policies\Microsoft\Windows\Explorer > ForceStartSize = " & If(FullScreenStartMenu, 2, 0), "reg_add")
                        EditReg_CMD("HKEY_CURRENT_USER\SOFTWARE\Policies\Microsoft\Windows\Explorer", "ForceStartSize", If(FullScreenStartMenu, 2, 0))
                    End Try

                    If My.W11 Then
                        Try
                            If Win11ClassicContextMenu Then
                                If [TreeView] IsNot Nothing Then AddNode([TreeView], "HKEY_CURRENT_USER\Software\Classes\CLSID\{86ca1aa0-34aa-4e8b-a509-50c905bae2a2} > InprocServer32", "reg_add")
                                My.Computer.Registry.CurrentUser.CreateSubKey("Software\Classes\CLSID\{86ca1aa0-34aa-4e8b-a509-50c905bae2a2}", True).CreateSubKey("InprocServer32", True).SetValue("", "", RegistryValueKind.String)
                            Else
                                If [TreeView] IsNot Nothing Then AddNode([TreeView], String.Format(My.Lang.Verbose_RegDelete, "HKEY_CURRENT_USER\Software\Classes\CLSID\{86ca1aa0-34aa-4e8b-a509-50c905bae2a2}"), "reg_delete")
                                My.Computer.Registry.CurrentUser.OpenSubKey("Software\Classes\CLSID", True).DeleteSubKeyTree("{86ca1aa0-34aa-4e8b-a509-50c905bae2a2}", False)
                            End If
                        Catch
                            'Do nothing
                            My.Computer.Registry.CurrentUser.Close()
                        Finally
                            My.Computer.Registry.CurrentUser.Close()
                        End Try
                    End If

                    If Not My.WXP AndAlso Not My.WVista Then
                        Try
                            If SysListView32 Then
                                If [TreeView] IsNot Nothing Then AddNode([TreeView], "HKEY_CURRENT_USER\Software\Classes\CLSID\{1eeb5b5a-06fb-4732-96b3-975c0194eb39} > InprocServer32", "reg_add")
                                My.Computer.Registry.CurrentUser.CreateSubKey("Software\Classes\CLSID\{1eeb5b5a-06fb-4732-96b3-975c0194eb39}", True).CreateSubKey("InprocServer32", True).SetValue("", "", RegistryValueKind.String)
                            Else
                                If [TreeView] IsNot Nothing Then AddNode([TreeView], String.Format(My.Lang.Verbose_RegDelete, "HKEY_CURRENT_USER\Software\Classes\CLSID\{1eeb5b5a-06fb-4732-96b3-975c0194eb39}"), "reg_delete")
                                My.Computer.Registry.CurrentUser.OpenSubKey("Software\Classes\CLSID", True).DeleteSubKeyTree("{1eeb5b5a-06fb-4732-96b3-975c0194eb39}", False)
                            End If
                        Catch
                            'Do nothing
                            My.Computer.Registry.CurrentUser.Close()
                        Finally
                            My.Computer.Registry.CurrentUser.Close()
                        End Try
                    End If

                    Try
                        If DisableNavBar Then
                            If [TreeView] IsNot Nothing Then AddNode([TreeView], "HKEY_CURRENT_USER\Software\Classes\CLSID\{056440FD-8568-48e7-A632-72157243B55B}, InprocServer32", "reg_add")
                            My.Computer.Registry.CurrentUser.CreateSubKey("Software\Classes\CLSID\{056440FD-8568-48e7-A632-72157243B55B}", True).CreateSubKey("InprocServer32", True).SetValue("", "", RegistryValueKind.String)
                        Else
                            If [TreeView] IsNot Nothing Then AddNode([TreeView], String.Format(My.Lang.Verbose_RegDelete, "HKEY_CURRENT_USER\Software\Classes\CLSID\{056440FD-8568-48e7-A632-72157243B55B}"), "reg_delete")
                            My.Computer.Registry.CurrentUser.OpenSubKey("Software\Classes\CLSID", True).DeleteSubKeyTree("{056440FD-8568-48e7-A632-72157243B55B}", False)
                        End If
                    Catch
                        'Do nothing
                        My.Computer.Registry.CurrentUser.Close()
                    Finally
                        My.Computer.Registry.CurrentUser.Close()
                    End Try

                End If
            End Sub

            Shared Operator =(First As WinEffects, Second As WinEffects) As Boolean
                Return First.Equals(Second)
            End Operator

            Shared Operator <>(First As WinEffects, Second As WinEffects) As Boolean
                Return Not First.Equals(Second)
            End Operator

            Public Function Clone() As Object Implements ICloneable.Clone
                Return MemberwiseClone()
            End Function
        End Structure
        Structure ScreenSaver : Implements ICloneable
            Public Enabled As Boolean
            Public IsSecure As Boolean
            Public TimeOut As Integer
            Public File As String

            Public Sub Load(_DefScrSaver As ScreenSaver)
                Enabled = Val(GetReg("HKEY_CURRENT_USER\Control Panel\Desktop", "ScreenSaveActive", _DefScrSaver.Enabled.ToInteger))
                IsSecure = Val(GetReg("HKEY_CURRENT_USER\Control Panel\Desktop", "ScreenSaverIsSecure", _DefScrSaver.IsSecure.ToInteger))
                TimeOut = Val(GetReg("HKEY_CURRENT_USER\Control Panel\Desktop", "ScreenSaveTimeOut", _DefScrSaver.TimeOut))
                File = GetReg("HKEY_CURRENT_USER\Control Panel\Desktop", "SCRNSAVE.EXE", _DefScrSaver.File)
            End Sub

            Sub Apply(Optional [TreeView] As TreeView = Nothing)
                EditReg([TreeView], "HKEY_CURRENT_USER\Control Panel\Desktop", "ScreenSaveActive", Enabled.ToInteger, RegistryValueKind.String)
                EditReg([TreeView], "HKEY_CURRENT_USER\Control Panel\Desktop", "ScreenSaverIsSecure", IsSecure.ToInteger, RegistryValueKind.String)
                EditReg([TreeView], "HKEY_CURRENT_USER\Control Panel\Desktop", "ScreenSaveTimeOut", TimeOut, RegistryValueKind.String)
                EditReg([TreeView], "HKEY_CURRENT_USER\Control Panel\Desktop", "SCRNSAVE.EXE", File, RegistryValueKind.String)
            End Sub

            Shared Operator =(First As ScreenSaver, Second As ScreenSaver) As Boolean
                Return First.Equals(Second)
            End Operator

            Shared Operator <>(First As ScreenSaver, Second As ScreenSaver) As Boolean
                Return Not First.Equals(Second)
            End Operator
            Public Function Clone() Implements ICloneable.Clone
                Return MemberwiseClone()
            End Function
        End Structure
        Structure Sounds : Implements ICloneable : Implements IDisposable
            Public Enabled As Boolean

            Public Snd_Win_Default As String
            Public Snd_Win_AppGPFault As String
            Public Snd_Win_CCSelect As String
            Public Snd_Win_ChangeTheme As String
            Public Snd_Win_Close As String
            Public Snd_Win_CriticalBatteryAlarm As String
            Public Snd_Win_DeviceConnect As String
            Public Snd_Win_DeviceDisconnect As String
            Public Snd_Win_DeviceFail As String
            Public Snd_Win_FaxBeep As String
            Public Snd_Win_LowBatteryAlarm As String
            Public Snd_Win_MailBeep As String
            Public Snd_Win_Maximize As String
            Public Snd_Win_MenuCommand As String
            Public Snd_Win_MenuPopup As String
            Public Snd_Win_MessageNudge As String
            Public Snd_Win_Minimize As String
            Public Snd_Win_Notification_Default As String
            Public Snd_Win_Notification_IM As String
            Public Snd_Win_Notification_Looping_Alarm As String
            Public Snd_Win_Notification_Looping_Alarm10 As String
            Public Snd_Win_Notification_Looping_Alarm2 As String
            Public Snd_Win_Notification_Looping_Alarm3 As String
            Public Snd_Win_Notification_Looping_Alarm4 As String
            Public Snd_Win_Notification_Looping_Alarm5 As String
            Public Snd_Win_Notification_Looping_Alarm6 As String
            Public Snd_Win_Notification_Looping_Alarm7 As String
            Public Snd_Win_Notification_Looping_Alarm8 As String
            Public Snd_Win_Notification_Looping_Alarm9 As String
            Public Snd_Win_Notification_Looping_Call As String
            Public Snd_Win_Notification_Looping_Call10 As String
            Public Snd_Win_Notification_Looping_Call2 As String
            Public Snd_Win_Notification_Looping_Call3 As String
            Public Snd_Win_Notification_Looping_Call4 As String
            Public Snd_Win_Notification_Looping_Call5 As String
            Public Snd_Win_Notification_Looping_Call6 As String
            Public Snd_Win_Notification_Looping_Call7 As String
            Public Snd_Win_Notification_Looping_Call8 As String
            Public Snd_Win_Notification_Looping_Call9 As String
            Public Snd_Win_Notification_Mail As String
            Public Snd_Win_Notification_Proximity As String
            Public Snd_Win_Notification_Reminder As String
            Public Snd_Win_Notification_SMS As String
            Public Snd_Win_Open As String
            Public Snd_Win_PrintComplete As String
            Public Snd_Win_ProximityConnection As String
            Public Snd_Win_RestoreDown As String
            Public Snd_Win_RestoreUp As String
            Public Snd_Win_ShowBand As String
            Public Snd_Win_SystemAsterisk As String
            Public Snd_Win_SystemExclamation As String
            Public Snd_Win_SystemExit As String
            Public Snd_Win_SystemStart As String
            Public Snd_Imageres_SystemStart As String
            Public Snd_Win_SystemHand As String
            Public Snd_Win_SystemNotification As String
            Public Snd_Win_SystemQuestion As String
            Public Snd_Win_WindowsLogoff As String
            Public Snd_Win_WindowsLogon As String
            Public Snd_Win_WindowsUAC As String
            Public Snd_Win_WindowsUnlock As String
            Public Snd_Explorer_ActivatingDocument As String
            Public Snd_Explorer_BlockedPopup As String
            Public Snd_Explorer_EmptyRecycleBin As String
            Public Snd_Explorer_FeedDiscovered As String
            Public Snd_Explorer_MoveMenuItem As String
            Public Snd_Explorer_Navigating As String
            Public Snd_Explorer_SecurityBand As String
            Public Snd_Explorer_SearchProviderDiscovered As String
            Public Snd_Explorer_FaxError As String
            Public Snd_Explorer_FaxLineRings As String
            Public Snd_Explorer_FaxNew As String
            Public Snd_Explorer_FaxSent As String
            Public Snd_NetMeeting_PersonJoins As String
            Public Snd_NetMeeting_PersonLeaves As String
            Public Snd_NetMeeting_ReceiveCall As String
            Public Snd_NetMeeting_ReceiveRequestToJoin As String
            Public Snd_SpeechRec_DisNumbersSound As String
            Public Snd_SpeechRec_HubOffSound As String
            Public Snd_SpeechRec_HubOnSound As String
            Public Snd_SpeechRec_HubSleepSound As String
            Public Snd_SpeechRec_MisrecoSound As String
            Public Snd_SpeechRec_PanelSound As String

            Public Snd_Win_SystemExit_TaskMgmt As Boolean
            Public Snd_Win_WindowsLogoff_TaskMgmt As Boolean
            Public Snd_Win_WindowsLogon_TaskMgmt As Boolean
            Public Snd_Win_WindowsUnlock_TaskMgmt As Boolean
            Public Snd_ChargerConnected As String

            Public Sub Load(_DefSounds As Sounds)
                Enabled = GetReg("HKEY_CURRENT_USER\Software\WinPaletter\Sounds", "", _DefSounds.Enabled)
                Snd_Imageres_SystemStart = GetReg("HKEY_CURRENT_USER\Software\WinPaletter\Sounds", "Imageres.dll_Startup", _DefSounds.Snd_Imageres_SystemStart)
                Snd_ChargerConnected = GetReg("HKEY_CURRENT_USER\Software\WinPaletter\Sounds", "Snd_ChargerConnected", _DefSounds.Snd_ChargerConnected)

                Snd_Win_SystemExit_TaskMgmt = GetReg("HKEY_CURRENT_USER\Software\WinPaletter\Sounds", "Snd_Win_SystemExit_TaskMgmt", _DefSounds.Snd_Win_SystemExit_TaskMgmt)
                Snd_Win_WindowsLogoff_TaskMgmt = GetReg("HKEY_CURRENT_USER\Software\WinPaletter\Sounds", "Snd_Win_WindowsLogoff_TaskMgmt", _DefSounds.Snd_Win_WindowsLogoff_TaskMgmt)
                Snd_Win_WindowsLogon_TaskMgmt = GetReg("HKEY_CURRENT_USER\Software\WinPaletter\Sounds", "Snd_Win_WindowsLogon_TaskMgmt", _DefSounds.Snd_Win_WindowsLogon_TaskMgmt)
                Snd_Win_WindowsUnlock_TaskMgmt = GetReg("HKEY_CURRENT_USER\Software\WinPaletter\Sounds", "Snd_Win_WindowsUnlock_TaskMgmt", _DefSounds.Snd_Win_WindowsUnlock_TaskMgmt)

                Dim Scope_Win As String = "HKEY_CURRENT_USER\AppEvents\Schemes\Apps\.Default\{0}\.Current"
                Snd_Win_Default = GetReg(String.Format(Scope_Win, ".Default"), "", _DefSounds.Snd_Win_Default)
                Snd_Win_AppGPFault = GetReg(String.Format(Scope_Win, "AppGPFault"), "", _DefSounds.Snd_Win_AppGPFault)
                Snd_Win_CCSelect = GetReg(String.Format(Scope_Win, "CCSelect"), "", _DefSounds.Snd_Win_CCSelect)
                Snd_Win_ChangeTheme = GetReg(String.Format(Scope_Win, "ChangeTheme"), "", _DefSounds.Snd_Win_ChangeTheme)
                Snd_Win_Close = GetReg(String.Format(Scope_Win, "Close"), "", _DefSounds.Snd_Win_Close)
                Snd_Win_CriticalBatteryAlarm = GetReg(String.Format(Scope_Win, "CriticalBatteryAlarm"), "", _DefSounds.Snd_Win_CriticalBatteryAlarm)
                Snd_Win_DeviceConnect = GetReg(String.Format(Scope_Win, "DeviceConnect"), "", _DefSounds.Snd_Win_DeviceConnect)
                Snd_Win_DeviceDisconnect = GetReg(String.Format(Scope_Win, "DeviceDisconnect"), "", _DefSounds.Snd_Win_DeviceDisconnect)
                Snd_Win_DeviceFail = GetReg(String.Format(Scope_Win, "DeviceFail"), "", _DefSounds.Snd_Win_DeviceFail)
                Snd_Win_FaxBeep = GetReg(String.Format(Scope_Win, "FaxBeep"), "", _DefSounds.Snd_Win_FaxBeep)
                Snd_Win_LowBatteryAlarm = GetReg(String.Format(Scope_Win, "LowBatteryAlarm"), "", _DefSounds.Snd_Win_LowBatteryAlarm)
                Snd_Win_MailBeep = GetReg(String.Format(Scope_Win, "MailBeep"), "", _DefSounds.Snd_Win_MailBeep)
                Snd_Win_Maximize = GetReg(String.Format(Scope_Win, "Maximize"), "", _DefSounds.Snd_Win_Maximize)
                Snd_Win_MenuCommand = GetReg(String.Format(Scope_Win, "MenuCommand"), "", _DefSounds.Snd_Win_MenuCommand)
                Snd_Win_MenuPopup = GetReg(String.Format(Scope_Win, "MenuPopup"), "", _DefSounds.Snd_Win_MenuPopup)
                Snd_Win_MessageNudge = GetReg(String.Format(Scope_Win, "MessageNudge"), "", _DefSounds.Snd_Win_MessageNudge)
                Snd_Win_Minimize = GetReg(String.Format(Scope_Win, "Minimize"), "", _DefSounds.Snd_Win_Minimize)
                Snd_Win_Notification_Default = GetReg(String.Format(Scope_Win, "Notification.Default"), "", _DefSounds.Snd_Win_Notification_Default)
                Snd_Win_Notification_IM = GetReg(String.Format(Scope_Win, "Notification.IM"), "", _DefSounds.Snd_Win_Notification_IM)
                Snd_Win_Notification_Looping_Alarm = GetReg(String.Format(Scope_Win, "Notification.Looping.Alarm"), "", _DefSounds.Snd_Win_Notification_Looping_Alarm)
                Snd_Win_Notification_Looping_Alarm2 = GetReg(String.Format(Scope_Win, "Notification.Looping.Alarm2"), "", _DefSounds.Snd_Win_Notification_Looping_Alarm2)
                Snd_Win_Notification_Looping_Alarm3 = GetReg(String.Format(Scope_Win, "Notification.Looping.Alarm3"), "", _DefSounds.Snd_Win_Notification_Looping_Alarm3)
                Snd_Win_Notification_Looping_Alarm4 = GetReg(String.Format(Scope_Win, "Notification.Looping.Alarm4"), "", _DefSounds.Snd_Win_Notification_Looping_Alarm4)
                Snd_Win_Notification_Looping_Alarm5 = GetReg(String.Format(Scope_Win, "Notification.Looping.Alarm5"), "", _DefSounds.Snd_Win_Notification_Looping_Alarm5)
                Snd_Win_Notification_Looping_Alarm6 = GetReg(String.Format(Scope_Win, "Notification.Looping.Alarm6"), "", _DefSounds.Snd_Win_Notification_Looping_Alarm6)
                Snd_Win_Notification_Looping_Alarm7 = GetReg(String.Format(Scope_Win, "Notification.Looping.Alarm7"), "", _DefSounds.Snd_Win_Notification_Looping_Alarm7)
                Snd_Win_Notification_Looping_Alarm8 = GetReg(String.Format(Scope_Win, "Notification.Looping.Alarm8"), "", _DefSounds.Snd_Win_Notification_Looping_Alarm8)
                Snd_Win_Notification_Looping_Alarm9 = GetReg(String.Format(Scope_Win, "Notification.Looping.Alarm9"), "", _DefSounds.Snd_Win_Notification_Looping_Alarm9)
                Snd_Win_Notification_Looping_Alarm10 = GetReg(String.Format(Scope_Win, "Notification.Looping.Alarm10"), "", _DefSounds.Snd_Win_Notification_Looping_Alarm10)
                Snd_Win_Notification_Looping_Call = GetReg(String.Format(Scope_Win, "Notification.Looping.Call"), "", _DefSounds.Snd_Win_Notification_Looping_Call)
                Snd_Win_Notification_Looping_Call2 = GetReg(String.Format(Scope_Win, "Notification.Looping.Call2"), "", _DefSounds.Snd_Win_Notification_Looping_Call2)
                Snd_Win_Notification_Looping_Call3 = GetReg(String.Format(Scope_Win, "Notification.Looping.Call3"), "", _DefSounds.Snd_Win_Notification_Looping_Call3)
                Snd_Win_Notification_Looping_Call4 = GetReg(String.Format(Scope_Win, "Notification.Looping.Call4"), "", _DefSounds.Snd_Win_Notification_Looping_Call4)
                Snd_Win_Notification_Looping_Call5 = GetReg(String.Format(Scope_Win, "Notification.Looping.Call5"), "", _DefSounds.Snd_Win_Notification_Looping_Call5)
                Snd_Win_Notification_Looping_Call6 = GetReg(String.Format(Scope_Win, "Notification.Looping.Call6"), "", _DefSounds.Snd_Win_Notification_Looping_Call6)
                Snd_Win_Notification_Looping_Call7 = GetReg(String.Format(Scope_Win, "Notification.Looping.Call7"), "", _DefSounds.Snd_Win_Notification_Looping_Call7)
                Snd_Win_Notification_Looping_Call8 = GetReg(String.Format(Scope_Win, "Notification.Looping.Call8"), "", _DefSounds.Snd_Win_Notification_Looping_Call8)
                Snd_Win_Notification_Looping_Call9 = GetReg(String.Format(Scope_Win, "Notification.Looping.Call9"), "", _DefSounds.Snd_Win_Notification_Looping_Call9)
                Snd_Win_Notification_Looping_Call10 = GetReg(String.Format(Scope_Win, "Notification.Looping.Call10"), "", _DefSounds.Snd_Win_Notification_Looping_Call10)
                Snd_Win_Notification_Mail = GetReg(String.Format(Scope_Win, "Notification.Mail"), "", _DefSounds.Snd_Win_Notification_Mail)
                Snd_Win_Notification_Proximity = GetReg(String.Format(Scope_Win, "Notification.Proximity"), "", _DefSounds.Snd_Win_Notification_Proximity)
                Snd_Win_Notification_Reminder = GetReg(String.Format(Scope_Win, "Notification.Reminder"), "", _DefSounds.Snd_Win_Notification_Reminder)
                Snd_Win_Notification_SMS = GetReg(String.Format(Scope_Win, "Notification.SMS"), "", _DefSounds.Snd_Win_Notification_SMS)
                Snd_Win_Open = GetReg(String.Format(Scope_Win, "Open"), "", _DefSounds.Snd_Win_Open)
                Snd_Win_PrintComplete = GetReg(String.Format(Scope_Win, "PrintComplete"), "", _DefSounds.Snd_Win_PrintComplete)
                Snd_Win_ProximityConnection = GetReg(String.Format(Scope_Win, "ProximityConnection"), "", _DefSounds.Snd_Win_ProximityConnection)
                Snd_Win_RestoreDown = GetReg(String.Format(Scope_Win, "RestoreDown"), "", _DefSounds.Snd_Win_RestoreDown)
                Snd_Win_RestoreUp = GetReg(String.Format(Scope_Win, "RestoreUp"), "", _DefSounds.Snd_Win_RestoreUp)
                Snd_Win_ShowBand = GetReg(String.Format(Scope_Win, "ShowBand"), "", _DefSounds.Snd_Win_ShowBand)
                Snd_Win_SystemAsterisk = GetReg(String.Format(Scope_Win, "SystemAsterisk"), "", _DefSounds.Snd_Win_SystemAsterisk)
                Snd_Win_SystemExclamation = GetReg(String.Format(Scope_Win, "SystemExclamation"), "", _DefSounds.Snd_Win_SystemExclamation)
                Snd_Win_SystemExit = GetReg(String.Format(Scope_Win, "SystemExit"), "", _DefSounds.Snd_Win_SystemExit)
                Snd_Win_SystemStart = GetReg(String.Format(Scope_Win, "SystemStart"), "", _DefSounds.Snd_Win_SystemStart)
                Snd_Win_SystemHand = GetReg(String.Format(Scope_Win, "SystemHand"), "", _DefSounds.Snd_Win_SystemHand)
                Snd_Win_SystemNotification = GetReg(String.Format(Scope_Win, "SystemNotification"), "", _DefSounds.Snd_Win_SystemNotification)
                Snd_Win_SystemQuestion = GetReg(String.Format(Scope_Win, "SystemQuestion"), "", _DefSounds.Snd_Win_SystemQuestion)
                Snd_Win_WindowsLogoff = GetReg(String.Format(Scope_Win, "WindowsLogoff"), "", _DefSounds.Snd_Win_WindowsLogoff)
                Snd_Win_WindowsLogon = GetReg(String.Format(Scope_Win, "WindowsLogon"), "", _DefSounds.Snd_Win_WindowsLogon)
                Snd_Win_WindowsUAC = GetReg(String.Format(Scope_Win, "WindowsUAC"), "", _DefSounds.Snd_Win_WindowsUAC)
                Snd_Win_WindowsUnlock = GetReg(String.Format(Scope_Win, "WindowsUnlock"), "", _DefSounds.Snd_Win_WindowsUnlock)

                Dim Scope_Explorer As String = "HKEY_CURRENT_USER\AppEvents\Schemes\Apps\Explorer\{0}\.Current"
                Snd_Explorer_ActivatingDocument = GetReg(String.Format(Scope_Explorer, "ActivatingDocument"), "", _DefSounds.Snd_Explorer_ActivatingDocument)
                Snd_Explorer_BlockedPopup = GetReg(String.Format(Scope_Explorer, "BlockedPopup"), "", _DefSounds.Snd_Explorer_BlockedPopup)
                Snd_Explorer_EmptyRecycleBin = GetReg(String.Format(Scope_Explorer, "EmptyRecycleBin"), "", _DefSounds.Snd_Explorer_EmptyRecycleBin)
                Snd_Explorer_FeedDiscovered = GetReg(String.Format(Scope_Explorer, "FeedDiscovered"), "", _DefSounds.Snd_Explorer_FeedDiscovered)
                Snd_Explorer_MoveMenuItem = GetReg(String.Format(Scope_Explorer, "MoveMenuItem"), "", _DefSounds.Snd_Explorer_MoveMenuItem)
                Snd_Explorer_Navigating = GetReg(String.Format(Scope_Explorer, "Navigating"), "", _DefSounds.Snd_Explorer_Navigating)
                Snd_Explorer_SecurityBand = GetReg(String.Format(Scope_Explorer, "SecurityBand"), "", _DefSounds.Snd_Explorer_SecurityBand)
                Snd_Explorer_SearchProviderDiscovered = GetReg(String.Format(Scope_Explorer, "SearchProviderDiscovered"), "", _DefSounds.Snd_Explorer_SearchProviderDiscovered)
                Snd_Explorer_FaxError = GetReg(String.Format(Scope_Explorer, "FaxError"), "", _DefSounds.Snd_Explorer_FaxError)
                Snd_Explorer_FaxLineRings = GetReg(String.Format(Scope_Explorer, "FaxLineRings"), "", _DefSounds.Snd_Explorer_FaxLineRings)
                Snd_Explorer_FaxNew = GetReg(String.Format(Scope_Explorer, "FaxNew"), "", _DefSounds.Snd_Explorer_FaxNew)
                Snd_Explorer_FaxSent = GetReg(String.Format(Scope_Explorer, "FaxSent"), "", _DefSounds.Snd_Explorer_FaxSent)

                Dim Scope_NetMeeting As String = "HKEY_CURRENT_USER\AppEvents\Schemes\Apps\Conf\{0}\.Current"
                Snd_NetMeeting_PersonJoins = GetReg(String.Format(Scope_NetMeeting, "Person Joins"), "", _DefSounds.Snd_NetMeeting_PersonJoins)
                Snd_NetMeeting_PersonLeaves = GetReg(String.Format(Scope_NetMeeting, "Person Leaves"), "", _DefSounds.Snd_NetMeeting_PersonLeaves)
                Snd_NetMeeting_ReceiveCall = GetReg(String.Format(Scope_NetMeeting, "Receive Call"), "", _DefSounds.Snd_NetMeeting_ReceiveCall)
                Snd_NetMeeting_ReceiveRequestToJoin = GetReg(String.Format(Scope_NetMeeting, "Receive Request to Join"), "", _DefSounds.Snd_NetMeeting_ReceiveRequestToJoin)

                Dim Scope_SpeechRec As String = "HKEY_CURRENT_USER\AppEvents\Schemes\Apps\sapisvr\{0}\.current"
                Snd_SpeechRec_DisNumbersSound = GetReg(String.Format(Scope_SpeechRec, "DisNumbersSound"), "", _DefSounds.Snd_SpeechRec_DisNumbersSound)
                Snd_SpeechRec_HubOffSound = GetReg(String.Format(Scope_SpeechRec, "HubOffSound"), "", _DefSounds.Snd_SpeechRec_HubOffSound)
                Snd_SpeechRec_HubOnSound = GetReg(String.Format(Scope_SpeechRec, "HubOnSound"), "", _DefSounds.Snd_SpeechRec_HubOnSound)
                Snd_SpeechRec_HubSleepSound = GetReg(String.Format(Scope_SpeechRec, "HubSleepSound"), "", _DefSounds.Snd_SpeechRec_HubSleepSound)
                Snd_SpeechRec_MisrecoSound = GetReg(String.Format(Scope_SpeechRec, "MisrecoSound"), "", _DefSounds.Snd_SpeechRec_MisrecoSound)
                Snd_SpeechRec_PanelSound = GetReg(String.Format(Scope_SpeechRec, "PanelSound"), "", _DefSounds.Snd_SpeechRec_PanelSound)

            End Sub

            Sub Apply(Optional [TreeView] As TreeView = Nothing)
                EditReg([TreeView], "HKEY_CURRENT_USER\Software\WinPaletter\Sounds", "", Enabled)
                EditReg([TreeView], "HKEY_CURRENT_USER\Software\WinPaletter\Sounds", "Imageres.dll_Startup", Snd_Imageres_SystemStart, RegistryValueKind.String)
                EditReg([TreeView], "HKEY_CURRENT_USER\Software\WinPaletter\Sounds", "Snd_ChargerConnected", Snd_ChargerConnected, RegistryValueKind.String)

                EditReg([TreeView], "HKEY_CURRENT_USER\Software\WinPaletter\Sounds", "Snd_Win_SystemExit_TaskMgmt", Snd_Win_SystemExit_TaskMgmt)
                EditReg([TreeView], "HKEY_CURRENT_USER\Software\WinPaletter\Sounds", "Snd_Win_WindowsLogoff_TaskMgmt", Snd_Win_WindowsLogoff_TaskMgmt)
                EditReg([TreeView], "HKEY_CURRENT_USER\Software\WinPaletter\Sounds", "Snd_Win_WindowsLogon_TaskMgmt", Snd_Win_WindowsLogon_TaskMgmt)
                EditReg([TreeView], "HKEY_CURRENT_USER\Software\WinPaletter\Sounds", "Snd_Win_WindowsUnlock_TaskMgmt", Snd_Win_WindowsUnlock_TaskMgmt)

                If Enabled Then
                    Dim destination_StartupSnd As String() = {"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Policies\System", "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Authentication\LogonUI\BootAnimation"}

                    If String.IsNullOrWhiteSpace(Snd_Imageres_SystemStart) Then
                        If [TreeView] IsNot Nothing Then AddNode([TreeView], destination_StartupSnd(0) & ", DisableStartupSound = 1", "reg_add")
                        EditReg_CMD(destination_StartupSnd(0), "DisableStartupSound", 1)

                        If [TreeView] IsNot Nothing Then AddNode([TreeView], destination_StartupSnd(1) & ", DisableStartupSound = 1", "reg_add")
                        EditReg_CMD(destination_StartupSnd(1), "DisableStartupSound", 1)

                    ElseIf IO.File.Exists(Snd_Imageres_SystemStart) Then
                        If [TreeView] IsNot Nothing Then AddNode([TreeView], destination_StartupSnd(0) & ", DisableStartupSound = 0", "reg_add")
                        EditReg_CMD(destination_StartupSnd(0), "DisableStartupSound", 0)

                        If [TreeView] IsNot Nothing Then AddNode([TreeView], destination_StartupSnd(1) & ", DisableStartupSound = 0", "reg_add")
                        EditReg_CMD(destination_StartupSnd(1), "DisableStartupSound", 0)

                    ElseIf Snd_Imageres_SystemStart.Trim.ToUpper = "DEFAULT" Then
                        If [TreeView] IsNot Nothing Then AddNode([TreeView], destination_StartupSnd(0) & ", DisableStartupSound = 0", "reg_add")
                        EditReg_CMD(destination_StartupSnd(0), "DisableStartupSound", 0)

                        If [TreeView] IsNot Nothing Then AddNode([TreeView], destination_StartupSnd(1) & ", DisableStartupSound = 0", "reg_add")
                        EditReg_CMD(destination_StartupSnd(1), "DisableStartupSound", 0)

                    ElseIf Not Snd_Imageres_SystemStart.Trim.ToUpper = "CURRENT" Then
                        If [TreeView] IsNot Nothing Then AddNode([TreeView], destination_StartupSnd(0) & ", DisableStartupSound = " & (Not My.W11).ToInteger, "reg_add")
                        EditReg_CMD(destination_StartupSnd(0), "DisableStartupSound", (Not My.W11).ToInteger)

                        If [TreeView] IsNot Nothing Then AddNode([TreeView], destination_StartupSnd(1) & ", DisableStartupSound = " & (Not My.W11).ToInteger, "reg_add")
                        EditReg_CMD(destination_StartupSnd(1), "DisableStartupSound", (Not My.W11).ToInteger)

                    Else
                        If [TreeView] IsNot Nothing Then AddNode([TreeView], destination_StartupSnd(0) & ", DisableStartupSound = 1", "reg_add")
                        EditReg_CMD(destination_StartupSnd(0), "DisableStartupSound", 1)

                        If [TreeView] IsNot Nothing Then AddNode([TreeView], destination_StartupSnd(1) & ", DisableStartupSound = 1", "reg_add")
                        EditReg_CMD(destination_StartupSnd(1), "DisableStartupSound", 1)

                    End If

                    If Not My.WXP Then

                        If IO.File.Exists(Snd_Imageres_SystemStart) AndAlso IO.Path.GetExtension(Snd_Imageres_SystemStart).ToUpper = ".WAV" Then

                            Dim CurrentSoundBytes As Byte() = PE.GetResource(My.PATH_imageres, "WAVE", If(My.WVista, 5051, 5080))
                            Dim TargetSoundBytes As Byte() = IO.File.ReadAllBytes(Snd_Imageres_SystemStart)

                            If Not CurrentSoundBytes.Equals(TargetSoundBytes) Then
                                ReplaceResource([TreeView], My.PATH_imageres, "WAVE", If(My.WVista, 5051, 5080), TargetSoundBytes)
                            End If

                        ElseIf Snd_Imageres_SystemStart.Trim.ToUpper = "DEFAULT" Then

                            Dim CurrentSoundBytes As Byte() = PE.GetResource(My.PATH_imageres, "WAVE", If(My.WVista, 5051, 5080))
                            Dim OriginalSoundBytes As Byte() = IO.File.ReadAllBytes(My.PATH_appData & "\WindowsStartup_Backup.wav")

                            If Not CurrentSoundBytes.Equals(OriginalSoundBytes) Then
                                ReplaceResource([TreeView], My.PATH_imageres, "WAVE", If(My.WVista, 5051, 5080), OriginalSoundBytes)
                            End If

                            If My.Settings.ThemeApplyingBehavior.SFC_on_restoring_StartupSound Then SFC(My.PATH_imageres)

                        End If

                    End If

                    If My.W8 Or My.W81 Or My.W10 Or My.W11 Or My.W12 Then
                        If Snd_Win_SystemExit_TaskMgmt AndAlso IO.File.Exists(Snd_Win_SystemExit) AndAlso IO.Path.GetExtension(Snd_Win_SystemExit).ToUpper = ".WAV" Then
                            TaskMgmt(TaskType.Shutdown, Actions.Add, Snd_Win_SystemExit, [TreeView])
                        Else
                            TaskMgmt(TaskType.Shutdown, Actions.Delete, "", [TreeView])
                        End If

                        If Snd_Win_WindowsLogoff_TaskMgmt AndAlso IO.File.Exists(Snd_Win_WindowsLogoff) AndAlso IO.Path.GetExtension(Snd_Win_WindowsLogoff).ToUpper = ".WAV" Then
                            TaskMgmt(TaskType.Logoff, Actions.Add, Snd_Win_WindowsLogoff, [TreeView])
                        Else
                            TaskMgmt(TaskType.Logoff, Actions.Delete, "", [TreeView])
                        End If

                        If Snd_Win_WindowsLogon_TaskMgmt AndAlso IO.File.Exists(Snd_Win_WindowsLogon) AndAlso IO.Path.GetExtension(Snd_Win_WindowsLogon).ToUpper = ".WAV" Then
                            TaskMgmt(TaskType.Logon, Actions.Add, Snd_Win_WindowsLogon, [TreeView])
                        Else
                            TaskMgmt(TaskType.Logon, Actions.Delete, "", [TreeView])
                        End If

                        If Snd_Win_WindowsUnlock_TaskMgmt AndAlso IO.File.Exists(Snd_Win_WindowsUnlock) AndAlso IO.Path.GetExtension(Snd_Win_WindowsUnlock).ToUpper = ".WAV" Then
                            TaskMgmt(TaskType.Unlock, Actions.Add, Snd_Win_WindowsUnlock, [TreeView])
                        Else
                            TaskMgmt(TaskType.Unlock, Actions.Delete, "", [TreeView])
                        End If
                    End If

                    If IO.File.Exists(Snd_ChargerConnected) AndAlso IO.Path.GetExtension(Snd_ChargerConnected).ToUpper = ".WAV" Then
                        TaskMgmt(TaskType.ChargerConnected, Actions.Add, Snd_ChargerConnected, [TreeView])
                    Else
                        TaskMgmt(TaskType.ChargerConnected, Actions.Delete, "", [TreeView])
                    End If

                    Dim Scope_Win As String() = {"HKEY_CURRENT_USER\AppEvents\Schemes\Apps\.Default\{0}\.Current", "HKEY_CURRENT_USER\AppEvents\Schemes\Apps\.Default\{0}\.Modified"}
                    Dim Scope_Explorer As String() = {"HKEY_CURRENT_USER\AppEvents\Schemes\Apps\Explorer\{0}\.Current", "HKEY_CURRENT_USER\AppEvents\Schemes\Apps\Explorer\{0}\.Modified"}
                    Dim Scope_SpeechRec As String() = {"HKEY_CURRENT_USER\AppEvents\Schemes\Apps\sapisvr\{0}\.Current", "HKEY_CURRENT_USER\AppEvents\Schemes\Apps\sapisvr\{0}\.Modified"}
                    Dim Scope_NetMeeting As String() = {"HKEY_CURRENT_USER\AppEvents\Schemes\Apps\Conf\{0}\.Current", "HKEY_CURRENT_USER\AppEvents\Schemes\Apps\Conf\{0}\.Modified"}

                    For Each Scope As String In Scope_Win
                        EditReg([TreeView], String.Format(Scope, ".Default"), "", Snd_Win_Default, RegistryValueKind.String)
                        EditReg([TreeView], String.Format(Scope, "AppGPFault"), "", Snd_Win_AppGPFault, RegistryValueKind.String)
                        EditReg([TreeView], String.Format(Scope, "CCSelect"), "", Snd_Win_CCSelect, RegistryValueKind.String)
                        EditReg([TreeView], String.Format(Scope, "ChangeTheme"), "", Snd_Win_ChangeTheme, RegistryValueKind.String)
                        EditReg([TreeView], String.Format(Scope, "Close"), "", Snd_Win_Close, RegistryValueKind.String)
                        EditReg([TreeView], String.Format(Scope, "CriticalBatteryAlarm"), "", Snd_Win_CriticalBatteryAlarm, RegistryValueKind.String)
                        EditReg([TreeView], String.Format(Scope, "DeviceConnect"), "", Snd_Win_DeviceConnect, RegistryValueKind.String)
                        EditReg([TreeView], String.Format(Scope, "DeviceDisconnect"), "", Snd_Win_DeviceDisconnect, RegistryValueKind.String)
                        EditReg([TreeView], String.Format(Scope, "DeviceFail"), "", Snd_Win_DeviceFail, RegistryValueKind.String)
                        EditReg([TreeView], String.Format(Scope, "FaxBeep"), "", Snd_Win_FaxBeep, RegistryValueKind.String)
                        EditReg([TreeView], String.Format(Scope, "LowBatteryAlarm"), "", Snd_Win_LowBatteryAlarm, RegistryValueKind.String)
                        EditReg([TreeView], String.Format(Scope, "MailBeep"), "", Snd_Win_MailBeep, RegistryValueKind.String)
                        EditReg([TreeView], String.Format(Scope, "Maximize"), "", Snd_Win_Maximize, RegistryValueKind.String)
                        EditReg([TreeView], String.Format(Scope, "MenuCommand"), "", Snd_Win_MenuCommand, RegistryValueKind.String)
                        EditReg([TreeView], String.Format(Scope, "MenuPopup"), "", Snd_Win_MenuPopup, RegistryValueKind.String)
                        EditReg([TreeView], String.Format(Scope, "MessageNudge"), "", Snd_Win_MessageNudge, RegistryValueKind.String)
                        EditReg([TreeView], String.Format(Scope, "Minimize"), "", Snd_Win_Minimize, RegistryValueKind.String)
                        EditReg([TreeView], String.Format(Scope, "Notification.Default"), "", Snd_Win_Notification_Default, RegistryValueKind.String)
                        EditReg([TreeView], String.Format(Scope, "Notification.IM"), "", Snd_Win_Notification_IM, RegistryValueKind.String)
                        EditReg([TreeView], String.Format(Scope, "Notification.Looping.Alarm"), "", Snd_Win_Notification_Looping_Alarm, RegistryValueKind.String)
                        EditReg([TreeView], String.Format(Scope, "Notification.Looping.Alarm2"), "", Snd_Win_Notification_Looping_Alarm2, RegistryValueKind.String)
                        EditReg([TreeView], String.Format(Scope, "Notification.Looping.Alarm3"), "", Snd_Win_Notification_Looping_Alarm3, RegistryValueKind.String)
                        EditReg([TreeView], String.Format(Scope, "Notification.Looping.Alarm4"), "", Snd_Win_Notification_Looping_Alarm4, RegistryValueKind.String)
                        EditReg([TreeView], String.Format(Scope, "Notification.Looping.Alarm5"), "", Snd_Win_Notification_Looping_Alarm5, RegistryValueKind.String)
                        EditReg([TreeView], String.Format(Scope, "Notification.Looping.Alarm6"), "", Snd_Win_Notification_Looping_Alarm6, RegistryValueKind.String)
                        EditReg([TreeView], String.Format(Scope, "Notification.Looping.Alarm7"), "", Snd_Win_Notification_Looping_Alarm7, RegistryValueKind.String)
                        EditReg([TreeView], String.Format(Scope, "Notification.Looping.Alarm8"), "", Snd_Win_Notification_Looping_Alarm8, RegistryValueKind.String)
                        EditReg([TreeView], String.Format(Scope, "Notification.Looping.Alarm9"), "", Snd_Win_Notification_Looping_Alarm9, RegistryValueKind.String)
                        EditReg([TreeView], String.Format(Scope, "Notification.Looping.Alarm10"), "", Snd_Win_Notification_Looping_Alarm10, RegistryValueKind.String)
                        EditReg([TreeView], String.Format(Scope, "Notification.Looping.Call"), "", Snd_Win_Notification_Looping_Call, RegistryValueKind.String)
                        EditReg([TreeView], String.Format(Scope, "Notification.Looping.Call2"), "", Snd_Win_Notification_Looping_Call2, RegistryValueKind.String)
                        EditReg([TreeView], String.Format(Scope, "Notification.Looping.Call3"), "", Snd_Win_Notification_Looping_Call3, RegistryValueKind.String)
                        EditReg([TreeView], String.Format(Scope, "Notification.Looping.Call4"), "", Snd_Win_Notification_Looping_Call4, RegistryValueKind.String)
                        EditReg([TreeView], String.Format(Scope, "Notification.Looping.Call5"), "", Snd_Win_Notification_Looping_Call5, RegistryValueKind.String)
                        EditReg([TreeView], String.Format(Scope, "Notification.Looping.Call6"), "", Snd_Win_Notification_Looping_Call6, RegistryValueKind.String)
                        EditReg([TreeView], String.Format(Scope, "Notification.Looping.Call7"), "", Snd_Win_Notification_Looping_Call7, RegistryValueKind.String)
                        EditReg([TreeView], String.Format(Scope, "Notification.Looping.Call8"), "", Snd_Win_Notification_Looping_Call8, RegistryValueKind.String)
                        EditReg([TreeView], String.Format(Scope, "Notification.Looping.Call9"), "", Snd_Win_Notification_Looping_Call9, RegistryValueKind.String)
                        EditReg([TreeView], String.Format(Scope, "Notification.Looping.Call10"), "", Snd_Win_Notification_Looping_Call10, RegistryValueKind.String)
                        EditReg([TreeView], String.Format(Scope, "Notification.Mail"), "", Snd_Win_Notification_Mail, RegistryValueKind.String)
                        EditReg([TreeView], String.Format(Scope, "Notification.Proximity"), "", Snd_Win_Notification_Proximity, RegistryValueKind.String)
                        EditReg([TreeView], String.Format(Scope, "Notification.Reminder"), "", Snd_Win_Notification_Reminder, RegistryValueKind.String)
                        EditReg([TreeView], String.Format(Scope, "Notification.SMS"), "", Snd_Win_Notification_SMS, RegistryValueKind.String)
                        EditReg([TreeView], String.Format(Scope, "Open"), "", Snd_Win_Open, RegistryValueKind.String)
                        EditReg([TreeView], String.Format(Scope, "PrintComplete"), "", Snd_Win_PrintComplete, RegistryValueKind.String)
                        EditReg([TreeView], String.Format(Scope, "ProximityConnection"), "", Snd_Win_ProximityConnection, RegistryValueKind.String)
                        EditReg([TreeView], String.Format(Scope, "RestoreDown"), "", Snd_Win_RestoreDown, RegistryValueKind.String)
                        EditReg([TreeView], String.Format(Scope, "RestoreUp"), "", Snd_Win_RestoreUp, RegistryValueKind.String)
                        EditReg([TreeView], String.Format(Scope, "ShowBand"), "", Snd_Win_ShowBand, RegistryValueKind.String)
                        EditReg([TreeView], String.Format(Scope, "SystemAsterisk"), "", Snd_Win_SystemAsterisk, RegistryValueKind.String)
                        EditReg([TreeView], String.Format(Scope, "SystemExclamation"), "", Snd_Win_SystemExclamation, RegistryValueKind.String)
                        EditReg([TreeView], String.Format(Scope, "SystemExit"), "", Snd_Win_SystemExit, RegistryValueKind.String)
                        EditReg([TreeView], String.Format(Scope, "SystemStart"), "", Snd_Win_SystemStart, RegistryValueKind.String)
                        EditReg([TreeView], String.Format(Scope, "SystemHand"), "", Snd_Win_SystemHand, RegistryValueKind.String)
                        EditReg([TreeView], String.Format(Scope, "SystemNotification"), "", Snd_Win_SystemNotification, RegistryValueKind.String)
                        EditReg([TreeView], String.Format(Scope, "SystemQuestion"), "", Snd_Win_SystemQuestion, RegistryValueKind.String)
                        EditReg([TreeView], String.Format(Scope, "WindowsLogoff"), "", Snd_Win_WindowsLogoff, RegistryValueKind.String)
                        EditReg([TreeView], String.Format(Scope, "WindowsLogon"), "", Snd_Win_WindowsLogon, RegistryValueKind.String)
                        EditReg([TreeView], String.Format(Scope, "WindowsUAC"), "", Snd_Win_WindowsUAC, RegistryValueKind.String)
                        EditReg([TreeView], String.Format(Scope, "WindowsUnlock"), "", Snd_Win_WindowsUnlock, RegistryValueKind.String)
                    Next

                    For Each Scope As String In Scope_Explorer
                        EditReg([TreeView], String.Format(Scope, "ActivatingDocument"), "", Snd_Explorer_ActivatingDocument, RegistryValueKind.String)
                        EditReg([TreeView], String.Format(Scope, "BlockedPopup"), "", Snd_Explorer_BlockedPopup, RegistryValueKind.String)
                        EditReg([TreeView], String.Format(Scope, "EmptyRecycleBin"), "", Snd_Explorer_EmptyRecycleBin, RegistryValueKind.String)
                        EditReg([TreeView], String.Format(Scope, "FeedDiscovered"), "", Snd_Explorer_FeedDiscovered, RegistryValueKind.String)
                        EditReg([TreeView], String.Format(Scope, "MoveMenuItem"), "", Snd_Explorer_MoveMenuItem, RegistryValueKind.String)
                        EditReg([TreeView], String.Format(Scope, "Navigating"), "", Snd_Explorer_Navigating, RegistryValueKind.String)
                        EditReg([TreeView], String.Format(Scope, "SecurityBand"), "", Snd_Explorer_SecurityBand, RegistryValueKind.String)
                        EditReg([TreeView], String.Format(Scope, "SearchProviderDiscovered"), "", Snd_Explorer_SearchProviderDiscovered, RegistryValueKind.String)
                        EditReg([TreeView], String.Format(Scope, "FaxError"), "", Snd_Explorer_FaxError, RegistryValueKind.String)
                        EditReg([TreeView], String.Format(Scope, "FaxLineRings"), "", Snd_Explorer_FaxLineRings, RegistryValueKind.String)
                        EditReg([TreeView], String.Format(Scope, "FaxNew"), "", Snd_Explorer_FaxNew, RegistryValueKind.String)
                        EditReg([TreeView], String.Format(Scope, "FaxSent"), "", Snd_Explorer_FaxSent, RegistryValueKind.String)
                    Next

                    For Each Scope As String In Scope_NetMeeting
                        EditReg([TreeView], String.Format(Scope, "Person Joins"), "", Snd_NetMeeting_PersonJoins, RegistryValueKind.String)
                        EditReg([TreeView], String.Format(Scope, "Person Leaves"), "", Snd_NetMeeting_PersonLeaves, RegistryValueKind.String)
                        EditReg([TreeView], String.Format(Scope, "Receive Call"), "", Snd_NetMeeting_ReceiveCall, RegistryValueKind.String)
                        EditReg([TreeView], String.Format(Scope, "Receive Request to Join"), "", Snd_NetMeeting_ReceiveRequestToJoin, RegistryValueKind.String)
                    Next

                    For Each Scope As String In Scope_SpeechRec
                        EditReg([TreeView], String.Format(Scope, "DisNumbersSound"), "", Snd_SpeechRec_DisNumbersSound, RegistryValueKind.String)
                        EditReg([TreeView], String.Format(Scope, "HubOffSound"), "", Snd_SpeechRec_HubOffSound, RegistryValueKind.String)
                        EditReg([TreeView], String.Format(Scope, "HubOnSound"), "", Snd_SpeechRec_HubOnSound, RegistryValueKind.String)
                        EditReg([TreeView], String.Format(Scope, "HubSleepSound"), "", Snd_SpeechRec_HubSleepSound, RegistryValueKind.String)
                        EditReg([TreeView], String.Format(Scope, "MisrecoSound"), "", Snd_SpeechRec_MisrecoSound, RegistryValueKind.String)
                        EditReg([TreeView], String.Format(Scope, "PanelSound"), "", Snd_SpeechRec_PanelSound, RegistryValueKind.String)
                    Next

                End If

            End Sub

            Shared Operator =(First As Sounds, Second As Sounds) As Boolean
                Return First.Equals(Second)
            End Operator

            Shared Operator <>(First As Sounds, Second As Sounds) As Boolean
                Return Not First.Equals(Second)
            End Operator
            Public Function Clone() Implements ICloneable.Clone
                Return MemberwiseClone()
            End Function

            Public Sub Dispose() Implements IDisposable.Dispose
                GC.Collect()
                GC.SuppressFinalize(Me)
            End Sub
        End Structure
        Structure AltTab : Implements ICloneable
            Public Enabled As Boolean
            Public Style As Styles
            Public Win10Opacity As Integer

            Enum Styles
                [Default]
                ClassicNT
                Placeholder
                EP_Win10
            End Enum

            Sub Load(_DefAltTab As AltTab)
                Enabled = GetReg("HKEY_CURRENT_USER\Software\WinPaletter\AltTab", "", _DefAltTab.Enabled)
                Style = GetReg("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer", "AltTabSettings", _DefAltTab.Style)
                Win10Opacity = GetReg("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\MultitaskingView\AltTabViewHost", "Grid_backgroundPercent", _DefAltTab.Win10Opacity)
                If Win10Opacity = Nothing Then Win10Opacity = _DefAltTab.Win10Opacity
            End Sub

            Sub Apply(Optional [TreeView] As TreeView = Nothing)
                EditReg([TreeView], "HKEY_CURRENT_USER\Software\WinPaletter\AltTab", "", Enabled)

                If Enabled Then
                    EditReg([TreeView], "HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer", "AltTabSettings", Style)
                    EditReg([TreeView], "HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\MultitaskingView\AltTabViewHost", "Grid_backgroundPercent", Win10Opacity)
                End If
            End Sub

            Shared Operator =(First As AltTab, Second As AltTab) As Boolean
                Return First.Equals(Second)
            End Operator

            Shared Operator <>(First As AltTab, Second As AltTab) As Boolean
                Return Not First.Equals(Second)
            End Operator

            Public Function Clone() As Object Implements ICloneable.Clone
                Return MemberwiseClone()
            End Function
        End Structure
        Structure Console : Implements ICloneable
            Public Enabled As Boolean
            Public ColorTable00 As Color
            Public ColorTable01 As Color
            Public ColorTable02 As Color
            Public ColorTable03 As Color
            Public ColorTable04 As Color
            Public ColorTable05 As Color
            Public ColorTable06 As Color
            Public ColorTable07 As Color
            Public ColorTable08 As Color
            Public ColorTable09 As Color
            Public ColorTable10 As Color
            Public ColorTable11 As Color
            Public ColorTable12 As Color
            Public ColorTable13 As Color
            Public ColorTable14 As Color
            Public ColorTable15 As Color
            Public PopupForeground As Integer
            Public PopupBackground As Integer
            Public ScreenColorsForeground As Integer
            Public ScreenColorsBackground As Integer
            Public CursorSize As Integer
            Public FaceName As String
            Public FontRaster As Boolean
            Public FontSize As Integer
            Public FontWeight As Integer
            Public W10_1909_CursorType As Integer
            Public W10_1909_CursorColor As Color
            Public W10_1909_ForceV2 As Boolean
            Public W10_1909_LineSelection As Boolean
            Public W10_1909_TerminalScrolling As Boolean
            Public W10_1909_WindowAlpha As Integer

            Public Sub Load([RegKey] As String, Signature_Of_Enable As String, [Defaults] As Console)
                Dim temp As Object
                Dim RegAddress As String = "HKEY_CURRENT_USER\Console" & If(String.IsNullOrEmpty([RegKey]), "", "\" & [RegKey])

                temp = GetReg(RegAddress, "ColorTable00", [Defaults].ColorTable00.Reverse.ToArgb)
                ColorTable00 = Color.FromArgb(255, Color.FromArgb(temp).Reverse)

                temp = GetReg(RegAddress, "ColorTable01", [Defaults].ColorTable01.Reverse.ToArgb)
                ColorTable01 = Color.FromArgb(255, Color.FromArgb(temp).Reverse)

                temp = GetReg(RegAddress, "ColorTable02", [Defaults].ColorTable02.Reverse.ToArgb)
                ColorTable02 = Color.FromArgb(255, Color.FromArgb(temp).Reverse)

                temp = GetReg(RegAddress, "ColorTable03", [Defaults].ColorTable03.Reverse.ToArgb)
                ColorTable03 = Color.FromArgb(255, Color.FromArgb(temp).Reverse)

                temp = GetReg(RegAddress, "ColorTable04", [Defaults].ColorTable04.Reverse.ToArgb)
                ColorTable04 = Color.FromArgb(255, Color.FromArgb(temp).Reverse)

                temp = GetReg(RegAddress, "ColorTable05", [Defaults].ColorTable05.Reverse.ToArgb)
                ColorTable05 = Color.FromArgb(255, Color.FromArgb(temp).Reverse)

                temp = GetReg(RegAddress, "ColorTable06", [Defaults].ColorTable06.Reverse.ToArgb)
                ColorTable06 = Color.FromArgb(255, Color.FromArgb(temp).Reverse)

                temp = GetReg(RegAddress, "ColorTable07", [Defaults].ColorTable07.Reverse.ToArgb)
                ColorTable07 = Color.FromArgb(255, Color.FromArgb(temp).Reverse)

                temp = GetReg(RegAddress, "ColorTable08", [Defaults].ColorTable08.Reverse.ToArgb)
                ColorTable08 = Color.FromArgb(255, Color.FromArgb(temp).Reverse)

                temp = GetReg(RegAddress, "ColorTable09", [Defaults].ColorTable09.Reverse.ToArgb)
                ColorTable09 = Color.FromArgb(255, Color.FromArgb(temp).Reverse)

                temp = GetReg(RegAddress, "ColorTable10", [Defaults].ColorTable10.Reverse.ToArgb)
                ColorTable10 = Color.FromArgb(255, Color.FromArgb(temp).Reverse)

                temp = GetReg(RegAddress, "ColorTable11", [Defaults].ColorTable11.Reverse.ToArgb)
                ColorTable11 = Color.FromArgb(255, Color.FromArgb(temp).Reverse)

                temp = GetReg(RegAddress, "ColorTable12", [Defaults].ColorTable12.Reverse.ToArgb)
                ColorTable12 = Color.FromArgb(255, Color.FromArgb(temp).Reverse)

                temp = GetReg(RegAddress, "ColorTable13", [Defaults].ColorTable13.Reverse.ToArgb)
                ColorTable13 = Color.FromArgb(255, Color.FromArgb(temp).Reverse)

                temp = GetReg(RegAddress, "ColorTable14", [Defaults].ColorTable14.Reverse.ToArgb)
                ColorTable14 = Color.FromArgb(255, Color.FromArgb(temp).Reverse)

                temp = GetReg(RegAddress, "ColorTable15", [Defaults].ColorTable15.Reverse.ToArgb)
                ColorTable15 = Color.FromArgb(255, Color.FromArgb(temp).Reverse)

                temp = GetReg(RegAddress, "PopupColors", Convert.ToInt32([Defaults].PopupBackground.ToString("X") & [Defaults].PopupForeground.ToString("X"), 16))
                Dim d As String = CInt(temp).ToString("X")
                If d.Count = 1 Then d = 0 & d
                PopupBackground = Convert.ToInt32(d.Chars(0), 16)
                PopupForeground = Convert.ToInt32(d.Chars(1), 16)

                temp = GetReg(RegAddress, "ScreenColors", Convert.ToInt32([Defaults].ScreenColorsBackground.ToString("X") & [Defaults].ScreenColorsForeground.ToString("X"), 16))
                d = CInt(temp).ToString("X")
                If d.Count = 1 Then d = 0 & d
                ScreenColorsBackground = Convert.ToInt32(d.Chars(0), 16)
                ScreenColorsForeground = Convert.ToInt32(d.Chars(1), 16)

                CursorSize = GetReg(RegAddress, "CursorSize", 25)

                temp = GetReg(RegAddress, "FaceName", [Defaults].FaceName)
                If IsFontInstalled(temp) Then
                    FaceName = temp
                Else
                    FaceName = [Defaults].FaceName
                End If

                temp = GetReg(RegAddress, "FontFamily", If(Not [Defaults].FontRaster, 54, 1))
                FontRaster = If(temp = 1 Or temp = 0 Or temp = 48, True, False)
                If FaceName.ToLower = "terminal" Then FontRaster = True

                temp = GetReg(RegAddress, "FontSize", [Defaults].FontSize)
                If temp = 0 And Not FontRaster Then FontSize = [Defaults].FontSize Else FontSize = temp

                FontWeight = GetReg(RegAddress, "FontWeight", 400)


                If My.W10_1909 Then
                    temp = GetReg(RegAddress, "CursorColor", Color.White.Reverse.ToArgb)
                    W10_1909_CursorColor = Color.FromArgb(255, Color.FromArgb(temp).Reverse)
                    W10_1909_CursorType = GetReg(RegAddress, "CursorType", 1)
                    W10_1909_ForceV2 = GetReg(RegAddress, "ForceV2", True)
                    W10_1909_LineSelection = GetReg(RegAddress, "LineSelection", False)
                    W10_1909_TerminalScrolling = GetReg(RegAddress, "TerminalScrolling", False)
                    W10_1909_WindowAlpha = GetReg(RegAddress, "WindowAlpha", 255)
                End If

                Enabled = CInt(GetReg("HKEY_CURRENT_USER\Software\WinPaletter\Terminals", Signature_Of_Enable, 0)).ToBoolean

            End Sub
            Shared Sub Save_Console_To_Registry(scopeReg As String, [RegKey] As String, [Console] As Console, Optional [TreeView] As TreeView = Nothing)

                Dim RegAddress As String = scopeReg & "\Console" & If(String.IsNullOrEmpty([RegKey]), "", "\" & [RegKey])

                Try
                    If scopeReg.ToUpper = "HKEY_CURRENT_USER" Then
                        If Not String.IsNullOrEmpty([RegKey]) Then Registry.CurrentUser.CreateSubKey("Console\" & [RegKey], True).Close()
                    End If
                Catch
                End Try

                EditReg([TreeView], RegAddress, "EnableColorSelection", 1)
                EditReg([TreeView], RegAddress, "ColorTable00", Color.FromArgb(0, [Console].ColorTable00.Reverse).ToArgb)
                EditReg([TreeView], RegAddress, "ColorTable01", Color.FromArgb(0, [Console].ColorTable01.Reverse).ToArgb)
                EditReg([TreeView], RegAddress, "ColorTable02", Color.FromArgb(0, [Console].ColorTable02.Reverse).ToArgb)
                EditReg([TreeView], RegAddress, "ColorTable03", Color.FromArgb(0, [Console].ColorTable03.Reverse).ToArgb)
                EditReg([TreeView], RegAddress, "ColorTable04", Color.FromArgb(0, [Console].ColorTable04.Reverse).ToArgb)
                EditReg([TreeView], RegAddress, "ColorTable05", Color.FromArgb(0, [Console].ColorTable05.Reverse).ToArgb)
                EditReg([TreeView], RegAddress, "ColorTable06", Color.FromArgb(0, [Console].ColorTable06.Reverse).ToArgb)
                EditReg([TreeView], RegAddress, "ColorTable07", Color.FromArgb(0, [Console].ColorTable07.Reverse).ToArgb)
                EditReg([TreeView], RegAddress, "ColorTable08", Color.FromArgb(0, [Console].ColorTable08.Reverse).ToArgb)
                EditReg([TreeView], RegAddress, "ColorTable09", Color.FromArgb(0, [Console].ColorTable09.Reverse).ToArgb)
                EditReg([TreeView], RegAddress, "ColorTable10", Color.FromArgb(0, [Console].ColorTable10.Reverse).ToArgb)
                EditReg([TreeView], RegAddress, "ColorTable11", Color.FromArgb(0, [Console].ColorTable11.Reverse).ToArgb)
                EditReg([TreeView], RegAddress, "ColorTable12", Color.FromArgb(0, [Console].ColorTable12.Reverse).ToArgb)
                EditReg([TreeView], RegAddress, "ColorTable13", Color.FromArgb(0, [Console].ColorTable13.Reverse).ToArgb)
                EditReg([TreeView], RegAddress, "ColorTable14", Color.FromArgb(0, [Console].ColorTable14.Reverse).ToArgb)
                EditReg([TreeView], RegAddress, "ColorTable15", Color.FromArgb(0, [Console].ColorTable15.Reverse).ToArgb)
                EditReg([TreeView], RegAddress, "PopupColors", Convert.ToInt32([Console].PopupBackground.ToString("X") & [Console].PopupForeground.ToString("X"), 16))
                EditReg([TreeView], RegAddress, "ScreenColors", Convert.ToInt32([Console].ScreenColorsBackground.ToString("X") & [Console].ScreenColorsForeground.ToString("X"), 16))
                EditReg([TreeView], RegAddress, "CursorSize", [Console].CursorSize)

                If [Console].FontRaster Then
                    EditReg([TreeView], RegAddress, "FaceName", "Terminal", RegistryValueKind.String)
                    EditReg([TreeView], RegAddress, "FontFamily", 48)
                Else
                    EditReg([TreeView], RegAddress, "FaceName", [Console].FaceName, RegistryValueKind.String)
                    EditReg([TreeView], RegAddress, "FontFamily", If([Console].FontRaster, 1, 54))
                End If

                EditReg([TreeView], RegAddress, "FontSize", [Console].FontSize)
                EditReg([TreeView], RegAddress, "FontWeight", [Console].FontWeight)

                If My.W10_1909 Then
                    EditReg([TreeView], RegAddress, "CursorColor", Color.FromArgb(0, [Console].W10_1909_CursorColor.Reverse).ToArgb)
                    EditReg([TreeView], RegAddress, "CursorType", [Console].W10_1909_CursorType)
                    EditReg([TreeView], RegAddress, "WindowAlpha", [Console].W10_1909_WindowAlpha)
                    EditReg([TreeView], RegAddress, "ForceV2", [Console].W10_1909_ForceV2.ToInteger)
                    EditReg([TreeView], RegAddress, "LineSelection", [Console].W10_1909_LineSelection.ToInteger)
                    EditReg([TreeView], RegAddress, "TerminalScrolling", [Console].W10_1909_TerminalScrolling.ToInteger)
                End If

                EditReg([TreeView], "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Console\TrueTypeFont", "000", [Console].FaceName, RegistryValueKind.String)

            End Sub

            Shared Operator =(First As Console, Second As Console) As Boolean
                Return First.Equals(Second)
            End Operator

            Shared Operator <>(First As Console, Second As Console) As Boolean
                Return Not First.Equals(Second)
            End Operator

            Public Function Clone() Implements ICloneable.Clone
                Return MemberwiseClone()
            End Function
        End Structure
        Structure Cursor : Implements ICloneable
            Public ArrowStyle As Paths.ArrowStyle
            Public CircleStyle As Paths.CircleStyle
            Public PrimaryColor1 As Color
            Public PrimaryColor2 As Color
            Public PrimaryColorGradient As Boolean
            Public PrimaryColorGradientMode As GradientMode
            Public PrimaryColorNoise As Boolean
            Public PrimaryColorNoiseOpacity As Single
            Public SecondaryColor1 As Color
            Public SecondaryColor2 As Color
            Public SecondaryColorGradient As Boolean
            Public SecondaryColorGradientMode As GradientMode
            Public SecondaryColorNoise As Boolean
            Public SecondaryColorNoiseOpacity As Single
            Public LoadingCircleBack1 As Color
            Public LoadingCircleBack2 As Color
            Public LoadingCircleBackGradient As Boolean
            Public LoadingCircleBackGradientMode As GradientMode
            Public LoadingCircleBackNoise As Boolean
            Public LoadingCircleBackNoiseOpacity As Single
            Public LoadingCircleHot1 As Color
            Public LoadingCircleHot2 As Color
            Public LoadingCircleHotGradient As Boolean
            Public LoadingCircleHotGradientMode As GradientMode
            Public LoadingCircleHotNoise As Boolean
            Public LoadingCircleHotNoiseOpacity As Single
            Public Shadow_Enabled As Boolean
            Public Shadow_Color As Color
            Public Shadow_Blur As Integer
            Public Shadow_Opacity As Single
            Public Shadow_OffsetX As Integer
            Public Shadow_OffsetY As Integer

            Public Sub Load([subKey] As String)
                ArrowStyle = GetReg("HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" & [subKey], "ArrowStyle", Paths.ArrowStyle.Aero)
                CircleStyle = GetReg("HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" & [subKey], "CircleStyle", Paths.CircleStyle.Aero)

                PrimaryColor1 = Color.FromArgb(GetReg("HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" & [subKey], "PrimaryColor1", Color.White.ToArgb))
                PrimaryColor2 = Color.FromArgb(GetReg("HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" & [subKey], "PrimaryColor2", Color.White.ToArgb))
                SecondaryColor1 = Color.FromArgb(GetReg("HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" & [subKey], "SecondaryColor1", If([subKey].ToLower <> "none", Color.Black, Color.Red).ToArgb))
                SecondaryColor2 = Color.FromArgb(GetReg("HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" & [subKey], "SecondaryColor2", If([subKey].ToLower <> "none", Color.FromArgb(64, 65, 75), Color.Red).ToArgb))
                LoadingCircleBack1 = Color.FromArgb(GetReg("HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" & [subKey], "LoadingCircleBack1", Color.FromArgb(42, 151, 243).ToArgb))
                LoadingCircleBack2 = Color.FromArgb(GetReg("HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" & [subKey], "LoadingCircleBack2", Color.FromArgb(42, 151, 243).ToArgb))
                LoadingCircleHot1 = Color.FromArgb(GetReg("HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" & [subKey], "LoadingCircleHot1", Color.FromArgb(37, 204, 255).ToArgb))
                LoadingCircleHot2 = Color.FromArgb(GetReg("HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" & [subKey], "LoadingCircleHot2", Color.FromArgb(37, 204, 255).ToArgb))

                PrimaryColorGradient = GetReg("HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" & [subKey], "PrimaryColorGradient", False)
                SecondaryColorGradient = GetReg("HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" & [subKey], "SecondaryColorGradient", True)
                LoadingCircleBackGradient = GetReg("HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" & [subKey], "LoadingCircleBackGradient", False)
                LoadingCircleHotGradient = GetReg("HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" & [subKey], "LoadingCircleHotGradient", False)

                PrimaryColorNoise = GetReg("HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" & [subKey], "PrimaryColorNoise", False)
                SecondaryColorNoise = GetReg("HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" & [subKey], "SecondaryColorNoise", False)
                LoadingCircleBackNoise = GetReg("HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" & [subKey], "LoadingCircleBackNoise", False)
                LoadingCircleHotNoise = GetReg("HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" & [subKey], "LoadingCircleHotNoise", False)

                PrimaryColorGradientMode = GetReg("HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" & [subKey], "PrimaryColorGradientMode", CInt(GradientMode.Circle))
                SecondaryColorGradientMode = GetReg("HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" & [subKey], "SecondaryColorGradientMode", CInt(GradientMode.Vertical))
                LoadingCircleBackGradientMode = GetReg("HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" & [subKey], "LoadingCircleBackGradientMode", CInt(GradientMode.Circle))
                LoadingCircleHotGradientMode = GetReg("HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" & [subKey], "LoadingCircleHotGradientMode", CInt(GradientMode.Circle))

                PrimaryColorNoiseOpacity = GetReg("HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" & [subKey], "PrimaryColorNoiseOpacity", 25) / 100
                SecondaryColorNoiseOpacity = GetReg("HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" & [subKey], "SecondaryColorNoiseOpacity", 25) / 100
                LoadingCircleBackNoiseOpacity = GetReg("HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" & [subKey], "LoadingCircleBackNoiseOpacity", 25) / 100
                LoadingCircleHotNoiseOpacity = GetReg("HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" & [subKey], "LoadingCircleHotNoiseOpacity", 25) / 100

                Shadow_Enabled = GetReg("HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" & [subKey], "Shadow_Enabled", False)
                Shadow_Color = Color.FromArgb(GetReg("HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" & [subKey], "Shadow_Color", Color.Black.ToArgb))
                Shadow_Blur = GetReg("HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" & [subKey], "Shadow_Blur", 5)
                Shadow_Opacity = GetReg("HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" & [subKey], "Shadow_Opacity", 30) / 100
                Shadow_OffsetX = GetReg("HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" & [subKey], "Shadow_OffsetX", 2)
                Shadow_OffsetY = GetReg("HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" & [subKey], "Shadow_OffsetY", 2)
            End Sub
            Shared Sub Save_Cursors_To_Registry(subKey As String, [Cursor] As Cursor, Optional [TreeView] As TreeView = Nothing)
                EditReg([TreeView], "HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" & subKey, "ArrowStyle", CInt([Cursor].ArrowStyle))
                EditReg([TreeView], "HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" & subKey, "CircleStyle", CInt([Cursor].CircleStyle))
                EditReg([TreeView], "HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" & subKey, "PrimaryColor1", [Cursor].PrimaryColor1.ToArgb)
                EditReg([TreeView], "HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" & subKey, "PrimaryColor2", [Cursor].PrimaryColor2.ToArgb)
                EditReg([TreeView], "HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" & subKey, "PrimaryColorGradient", [Cursor].PrimaryColorGradient.ToInteger)
                EditReg([TreeView], "HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" & subKey, "PrimaryColorGradientMode", CInt([Cursor].PrimaryColorGradientMode))
                EditReg([TreeView], "HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" & subKey, "PrimaryColorNoise", [Cursor].PrimaryColorNoise.ToInteger)
                EditReg([TreeView], "HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" & subKey, "PrimaryColorNoiseOpacity", [Cursor].PrimaryColorNoiseOpacity * 100)
                EditReg([TreeView], "HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" & subKey, "SecondaryColor1", [Cursor].SecondaryColor1.ToArgb)
                EditReg([TreeView], "HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" & subKey, "SecondaryColor2", [Cursor].SecondaryColor2.ToArgb)
                EditReg([TreeView], "HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" & subKey, "SecondaryColorGradient", [Cursor].SecondaryColorGradient.ToInteger)
                EditReg([TreeView], "HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" & subKey, "SecondaryColorGradientMode", CInt([Cursor].SecondaryColorGradientMode))
                EditReg([TreeView], "HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" & subKey, "SecondaryColorNoise", [Cursor].SecondaryColorNoise.ToInteger)
                EditReg([TreeView], "HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" & subKey, "SecondaryColorNoiseOpacity", [Cursor].SecondaryColorNoiseOpacity * 100)
                EditReg([TreeView], "HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" & subKey, "LoadingCircleBack1", [Cursor].LoadingCircleBack1.ToArgb)
                EditReg([TreeView], "HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" & subKey, "LoadingCircleBack2", [Cursor].LoadingCircleBack2.ToArgb)
                EditReg([TreeView], "HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" & subKey, "LoadingCircleBackGradient", [Cursor].LoadingCircleBackGradient.ToInteger)
                EditReg([TreeView], "HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" & subKey, "LoadingCircleBackGradientMode", CInt([Cursor].LoadingCircleBackGradientMode))
                EditReg([TreeView], "HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" & subKey, "LoadingCircleBackNoise", [Cursor].LoadingCircleBackNoise.ToInteger)
                EditReg([TreeView], "HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" & subKey, "LoadingCircleBackNoiseOpacity", [Cursor].LoadingCircleBackNoiseOpacity * 100)
                EditReg([TreeView], "HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" & subKey, "LoadingCircleHot1", [Cursor].LoadingCircleHot1.ToArgb)
                EditReg([TreeView], "HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" & subKey, "LoadingCircleHot2", [Cursor].LoadingCircleHot2.ToArgb)
                EditReg([TreeView], "HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" & subKey, "LoadingCircleHotGradient", [Cursor].LoadingCircleHotGradient.ToInteger)
                EditReg([TreeView], "HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" & subKey, "LoadingCircleHotGradientMode", CInt([Cursor].LoadingCircleHotGradientMode))
                EditReg([TreeView], "HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" & subKey, "LoadingCircleHotNoise", [Cursor].LoadingCircleHotNoise.ToInteger)
                EditReg([TreeView], "HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" & subKey, "LoadingCircleHotNoiseOpacity", [Cursor].LoadingCircleHotNoiseOpacity * 100)
                EditReg([TreeView], "HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" & subKey, "Shadow_Enabled", [Cursor].Shadow_Enabled)
                EditReg([TreeView], "HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" & subKey, "Shadow_Color", [Cursor].Shadow_Color.ToArgb)
                EditReg([TreeView], "HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" & subKey, "Shadow_Blur", [Cursor].Shadow_Blur)
                EditReg([TreeView], "HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" & subKey, "Shadow_Opacity", CInt([Cursor].Shadow_Opacity * 100))
                EditReg([TreeView], "HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" & subKey, "Shadow_OffsetX", [Cursor].Shadow_OffsetX)
                EditReg([TreeView], "HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" & subKey, "Shadow_OffsetY", [Cursor].Shadow_OffsetY)
            End Sub

            Shared Operator =(First As Cursor, Second As Cursor) As Boolean
                Return First.Equals(Second)
            End Operator

            Shared Operator <>(First As Cursor, Second As Cursor) As Boolean
                Return Not First.Equals(Second)
            End Operator
            Public Function Clone() Implements ICloneable.Clone
                Return MemberwiseClone()
            End Function
        End Structure
    End Class

#Region "Properties"
    Public Info As New Structures.Info With {
            .AppVersion = My.AppVersion,
            .ThemeName = My.Lang.CurrentMode,
            .Description = "",
            .ExportResThemePack = False,
            .License = "",
            .ThemeVersion = "1.0.0.0",
            .Author = Environment.UserName,
            .AuthorSocialMediaLink = "",
            .Color1 = Color.FromArgb(0, 102, 204),
            .Color2 = Color.FromArgb(122, 9, 9),
            .DesignedFor_Win11 = True,
            .DesignedFor_Win10 = True,
            .DesignedFor_Win81 = True,
            .DesignedFor_Win7 = True,
            .DesignedFor_WinVista = True,
            .DesignedFor_WinXP = True,
            .Pattern = 1}

    Public AppTheme As New Structures.AppTheme With {
        .Enabled = False,
        .BackColor = Color.FromArgb(25, 25, 25),
        .AccentColor = Color.FromArgb(0, 81, 210),
        .DarkMode = True,
        .RoundCorners = My.WXP Or My.WVista Or My.W7 Or My.W11}

    Public Windows11 As New Structures.Windows10x With {
            .Color_Index0 = Color.FromArgb(153, 235, 255),
            .Color_Index1 = Color.FromArgb(76, 194, 255),
            .Color_Index2 = Color.FromArgb(0, 145, 248),
            .Color_Index3 = Color.FromArgb(0, 120, 212),
            .Color_Index4 = Color.FromArgb(0, 103, 192),
            .Color_Index5 = Color.FromArgb(0, 62, 146),
            .Color_Index6 = Color.FromArgb(0, 26, 104),
            .Color_Index7 = Color.FromArgb(247, 99, 12),
            .Titlebar_Active = Color.FromArgb(0, 120, 212),
            .Titlebar_Inactive = Color.FromArgb(32, 32, 32),
            .StartMenu_Accent = Color.FromArgb(0, 103, 192),
            .WinMode_Light = True,
            .AppMode_Light = True,
            .Transparency = True,
            .ApplyAccentOnTitlebars = False,
            .ApplyAccentOnTaskbar = CP.Structures.Windows10x.AccentTaskbarLevels.None,
            .IncreaseTBTransparency = False,
            .TB_Blur = True}

    Public Windows10 As New Structures.Windows10x With {
            .Color_Index0 = Color.FromArgb(166, 216, 255),
            .Color_Index1 = Color.FromArgb(118, 185, 237),
            .Color_Index2 = Color.FromArgb(66, 156, 227),
            .Color_Index3 = Color.FromArgb(0, 120, 215),
            .Color_Index4 = Color.FromArgb(0, 90, 158),
            .Color_Index5 = Color.FromArgb(0, 66, 117),
            .Color_Index6 = Color.FromArgb(0, 38, 66),
            .Color_Index7 = Color.FromArgb(247, 99, 12),
            .Titlebar_Active = Color.FromArgb(0, 120, 215),
            .Titlebar_Inactive = Color.FromArgb(43, 43, 43),
            .StartMenu_Accent = Color.FromArgb(0, 90, 158),
            .WinMode_Light = False,
            .AppMode_Light = True,
            .Transparency = True,
            .ApplyAccentOnTitlebars = False,
            .ApplyAccentOnTaskbar = CP.Structures.Windows10x.AccentTaskbarLevels.None,
            .IncreaseTBTransparency = False,
            .TB_Blur = True}

    Public Windows81 As New Structures.Windows8x With {
                    .ColorizationColor = Color.FromArgb(246, 195, 74),
                    .ColorizationColorBalance = 78,
                    .Start = 0,
                    .StartColor = Color.FromArgb(30, 0, 84),
                    .AccentColor = Color.FromArgb(72, 29, 178),
                    .Theme = Structures.Windows7.Themes.Aero,
                    .LogonUI = 0,
                    .PersonalColors_Background = Color.FromArgb(30, 0, 84),
                    .PersonalColors_Accent = Color.FromArgb(72, 29, 178),
                    .NoLockScreen = False,
                    .LockScreenType = Structures.LogonUI7.Modes.Default_,
                    .LockScreenSystemID = 0}

    Public Windows7 As New Structures.Windows7 With {
            .ColorizationColor = Color.FromArgb(116, 184, 252),
            .ColorizationAfterglow = Color.FromArgb(116, 184, 252),
            .ColorizationColorBalance = 8,
            .ColorizationAfterglowBalance = 43,
            .ColorizationBlurBalance = 49,
            .ColorizationGlassReflectionIntensity = 0,
            .EnableAeroPeek = True,
            .AlwaysHibernateThumbnails = False,
            .Theme = CP.Structures.Windows7.Themes.Aero}

    Public WindowsVista As New Structures.WindowsVista With {
            .ColorizationColor = Color.FromArgb(64, 158, 254),
            .Theme = CP.Structures.Windows7.Themes.Aero}

    Public WindowsXP As New Structures.WindowsXP With {
        .Theme = Structures.WindowsXP.Themes.LunaBlue,
        .ColorScheme = "NormalColor",
        .ThemeFile = My.PATH_Windows & "\resources\Themes\Luna\Luna.msstyles"}

    Public Win32 As New Structures.Win32UI With {
            .EnableTheming = True,
            .EnableGradient = True,
            .ActiveBorder = Color.FromArgb(180, 180, 180),
            .ActiveTitle = Color.FromArgb(153, 180, 209),
            .AppWorkspace = Color.FromArgb(171, 171, 171),
            .Background = Color.FromArgb(0, 0, 0),
            .ButtonAlternateFace = Color.FromArgb(0, 0, 0),
            .ButtonDkShadow = Color.FromArgb(105, 105, 105),
            .ButtonFace = Color.FromArgb(240, 240, 240),
            .ButtonHilight = Color.FromArgb(255, 255, 255),
            .ButtonLight = Color.FromArgb(227, 227, 227),
            .ButtonShadow = Color.FromArgb(160, 160, 160),
            .ButtonText = Color.FromArgb(0, 0, 0),
            .GradientActiveTitle = Color.FromArgb(185, 209, 234),
            .GradientInactiveTitle = Color.FromArgb(215, 228, 242),
            .GrayText = Color.FromArgb(109, 109, 109),
            .HilightText = Color.FromArgb(255, 255, 255),
            .HotTrackingColor = Color.FromArgb(0, 102, 204),
            .InactiveBorder = Color.FromArgb(244, 247, 252),
            .InactiveTitle = Color.FromArgb(191, 205, 219),
            .InactiveTitleText = Color.FromArgb(0, 0, 0),
            .InfoText = Color.FromArgb(0, 0, 0),
            .InfoWindow = Color.FromArgb(255, 255, 225),
            .Menu = Color.FromArgb(240, 240, 240),
            .MenuBar = Color.FromArgb(240, 240, 240),
            .MenuText = Color.FromArgb(0, 0, 0),
            .Scrollbar = Color.FromArgb(200, 200, 200),
            .TitleText = Color.FromArgb(0, 0, 0),
            .Window = Color.FromArgb(255, 255, 255),
            .WindowFrame = Color.FromArgb(100, 100, 100),
            .WindowText = Color.FromArgb(0, 0, 0),
            .Hilight = Color.FromArgb(0, 120, 215),
            .MenuHilight = Color.FromArgb(0, 120, 215),
            .Desktop = Color.FromArgb(0, 0, 0)
            }

    Public LogonUI10x As New Structures.LogonUI10x With {
        .DisableAcrylicBackgroundOnLogon = False, .DisableLogonBackgroundImage = False, .NoLockScreen = False}

    Public LogonUI7 As New Structures.LogonUI7 With {
                    .Enabled = False,
                    .Mode = Structures.LogonUI7.Modes.Default_,
                    .ImagePath = "C:\Windows\Web\Wallpaper\Windows\img0.jpg",
                    .Color = Color.Black,
                    .Blur = False,
                    .Blur_Intensity = 0,
                    .Grayscale = False,
                    .Noise = False,
                    .Noise_Mode = NoiseMode.Acrylic,
                    .Noise_Intensity = 0}

    Public LogonUIXP As New Structures.LogonUIXP With {
        .Enabled = True,
        .Mode = Structures.LogonUIXP.Modes.Default,
        .BackColor = Color.Black,
        .ShowMoreOptions = False}

    Public Wallpaper As New Structures.Wallpaper With {
        .Enabled = False,
        .ImageFile = My.PATH_Windows & "\Web\Wallpaper\Windows\img0.jpg",
        .WallpaperType = Structures.Wallpaper.WallpaperTypes.Picture,
        .WallpaperStyle = Structures.Wallpaper.WallpaperStyles.Fill,
        .Wallpaper_Slideshow_Images = New String() {},
        .Wallpaper_Slideshow_ImagesRootPath = "",
        .Wallpaper_Slideshow_Interval = 60000,
        .Wallpaper_Slideshow_Shuffle = False,
        .SlideShow_Folder_or_ImagesList = True}

    Public WallpaperTone_W11 As New Structures.WallpaperTone With {
        .Enabled = False,
        .Image = My.PATH_Windows & "\Web\Wallpaper\Windows\img0.jpg",
        .H = 0, .S = 100, .L = 100}

    Public WallpaperTone_W10 As New Structures.WallpaperTone With {
        .Enabled = False,
        .Image = My.PATH_Windows & "\Web\Wallpaper\Windows\img0.jpg",
        .H = 0, .S = 100, .L = 100}

    Public WallpaperTone_W81 As New Structures.WallpaperTone With {
        .Enabled = False,
        .Image = My.PATH_Windows & "\Web\Wallpaper\Windows\img0.jpg",
        .H = 0, .S = 100, .L = 100}

    Public WallpaperTone_W7 As New Structures.WallpaperTone With {
        .Enabled = False,
        .Image = My.PATH_Windows & "\Web\Wallpaper\Windows\img0.jpg",
        .H = 0, .S = 100, .L = 100}

    Public WallpaperTone_WVista As New Structures.WallpaperTone With {
        .Enabled = False,
        .Image = My.PATH_Windows & "\Web\Wallpaper\Windows\img0.jpg",
        .H = 0, .S = 100, .L = 100}

    Public WallpaperTone_WXP As New Structures.WallpaperTone With {
        .Enabled = False,
        .Image = My.PATH_Windows & "\Web\Wallpaper\Bliss.bmp",
        .H = 0, .S = 100, .L = 100}

    Public MetricsFonts As New Structures.MetricsFonts With {
                .Enabled = Core.GetWindowsScreenScalingFactor() = 100,
                .BorderWidth = 1,
                .CaptionHeight = 22,
                .CaptionWidth = 22,
                .IconSpacing = 75,
                .IconVerticalSpacing = 75,
                .MenuHeight = 19,
                .MenuWidth = 19,
                .PaddedBorderWidth = 4,
                .ScrollHeight = 19,
                .ScrollWidth = 19,
                .SmCaptionHeight = 22,
                .SmCaptionWidth = 22,
                .DesktopIconSize = 48,
                .ShellIconSize = 32,
                .ShellSmallIconSize = 16,
                .Fonts_SingleBitPP = False,
                .CaptionFont = New Font("Segoe UI", 9, FontStyle.Regular),
                .IconFont = New Font("Segoe UI", 9, FontStyle.Regular),
                .MenuFont = New Font("Segoe UI", 9, FontStyle.Regular),
                .MessageFont = New Font("Segoe UI", 9, FontStyle.Regular),
                .SmCaptionFont = New Font("Segoe UI", 9, FontStyle.Regular),
                .StatusFont = New Font("Segoe UI", 9, FontStyle.Regular),
                .FontSubstitute_MSShellDlg = "Microsoft Sans Serif", .FontSubstitute_MSShellDlg2 = "Tahoma",
                .FontSubstitute_SegoeUI = ""}

    Public WindowsEffects As New Structures.WinEffects With {
        .Enabled = True,
        .WindowAnimation = True,
        .WindowShadow = True,
        .WindowUIEffects = True,
        .MenuAnimation = True,
        .MenuSelectionFade = True,
        .MenuFade = Structures.WinEffects.MenuAnimType.Fade,
        .MenuShowDelay = 400,
        .ComboBoxAnimation = True,
        .ListBoxSmoothScrolling = True,
        .TooltipAnimation = True,
        .TooltipFade = Structures.WinEffects.MenuAnimType.Fade,
        .IconsShadow = True,
        .IconsDesktopTranslSel = True,
        .ShowWinContentDrag = True,
        .BalloonNotifications = False,
        .PaintDesktopVersion = False,
        .ShowSecondsInSystemClock = False,
        .Win11ClassicContextMenu = False,
        .SysListView32 = False,
        .SnapCursorToDefButton = False,
        .ShakeToMinimize = True,
        .NotificationDuration = 5,
        .FocusRectWidth = 1,
        .FocusRectHeight = 1,
        .KeyboardUnderline = False,
        .Caret = 1,
        .AWT_Enabled = False,
        .AWT_Delay = 0,
        .AWT_BringActivatedWindowToTop = False,
        .Win11BootDots = Not My.W11,
        .Win11ExplorerBar = Structures.WinEffects.ExplorerBar.Default,
        .DisableNavBar = False,
        .AutoHideScrollBars = True,
        .ColorFilter_Enabled = False,
        .ColorFilter = Structures.WinEffects.ColorFilters.Grayscale,
        .ClassicVolMixer = False,
        .FullScreenStartMenu = False}

    Public ScreenSaver As New ScreenSaver With {
        .Enabled = False,
        .File = "",
        .IsSecure = False,
        .TimeOut = 60}

    Public Sounds As New Sounds With {
        .Enabled = True,
        .Snd_Imageres_SystemStart = If(My.W11, "Default", ""),
        .Snd_Win_SystemExit_TaskMgmt = Not My.WXP And Not My.WVista And Not My.W7,
        .Snd_Win_WindowsLogoff_TaskMgmt = Not My.WXP And Not My.WVista And Not My.W7,
        .Snd_Win_WindowsLogon_TaskMgmt = Not My.WXP And Not My.WVista And Not My.W7,
        .Snd_Win_WindowsUnlock_TaskMgmt = Not My.WXP And Not My.WVista And Not My.W7}

    Public AltTab As New Structures.AltTab With {.Enabled = True, .Style = AltTab.Styles.Default, .Win10Opacity = 95}

    Public CommandPrompt As New Structures.Console With {
                    .Enabled = False,
                    .ColorTable00 = Color.FromArgb(12, 12, 12),
                    .ColorTable01 = Color.FromArgb(0, 55, 218),
                    .ColorTable02 = Color.FromArgb(19, 161, 14),
                    .ColorTable03 = Color.FromArgb(58, 150, 221),
                    .ColorTable04 = Color.FromArgb(197, 15, 31),
                    .ColorTable05 = Color.FromArgb(136, 23, 152),
                    .ColorTable06 = Color.FromArgb(193, 156, 0),
                    .ColorTable07 = Color.FromArgb(204, 204, 204),
                    .ColorTable08 = Color.FromArgb(118, 118, 118),
                    .ColorTable09 = Color.FromArgb(59, 120, 255),
                    .ColorTable10 = Color.FromArgb(22, 198, 12),
                    .ColorTable11 = Color.FromArgb(97, 214, 214),
                    .ColorTable12 = Color.FromArgb(231, 72, 86),
                    .ColorTable13 = Color.FromArgb(180, 0, 158),
                    .ColorTable14 = Color.FromArgb(249, 241, 165),
                    .ColorTable15 = Color.FromArgb(242, 242, 242),
                    .PopupForeground = 5,
                    .PopupBackground = 15,
                    .ScreenColorsForeground = 7,
                    .ScreenColorsBackground = 0,
                    .CursorSize = 19,
                    .FaceName = "Consolas",
                    .FontRaster = False,
                    .FontSize = 18 * 65536,
                    .FontWeight = 400,
                    .W10_1909_CursorType = 0,
                    .W10_1909_CursorColor = Color.White,
                    .W10_1909_ForceV2 = True,
                    .W10_1909_LineSelection = False,
                    .W10_1909_TerminalScrolling = False,
                    .W10_1909_WindowAlpha = 255}

    Public PowerShellx86 As New Structures.Console With {
                        .Enabled = False,
                        .ColorTable00 = Color.FromArgb(12, 12, 12),
                        .ColorTable01 = Color.FromArgb(0, 55, 218),
                        .ColorTable02 = Color.FromArgb(19, 161, 14),
                        .ColorTable03 = Color.FromArgb(58, 150, 221),
                        .ColorTable04 = Color.FromArgb(197, 15, 31),
                        .ColorTable05 = Color.FromArgb(1, 36, 86),
                        .ColorTable06 = Color.FromArgb(238, 237, 240),
                        .ColorTable07 = Color.FromArgb(204, 204, 204),
                        .ColorTable08 = Color.FromArgb(118, 118, 118),
                        .ColorTable09 = Color.FromArgb(59, 120, 255),
                        .ColorTable10 = Color.FromArgb(22, 198, 12),
                        .ColorTable11 = Color.FromArgb(97, 214, 214),
                        .ColorTable12 = Color.FromArgb(231, 72, 86),
                        .ColorTable13 = Color.FromArgb(180, 0, 158),
                        .ColorTable14 = Color.FromArgb(249, 241, 165),
                        .ColorTable15 = Color.FromArgb(242, 242, 242),
                        .PopupForeground = 15,
                        .PopupBackground = 3,
                        .ScreenColorsForeground = 6,
                        .ScreenColorsBackground = 5,
                        .CursorSize = 19,
                        .FaceName = "Consolas",
                        .FontRaster = False,
                        .FontSize = 16 * 65536,
                        .FontWeight = 400,
                        .W10_1909_CursorType = 0,
                        .W10_1909_CursorColor = Color.White,
                        .W10_1909_ForceV2 = True,
                        .W10_1909_LineSelection = False,
                        .W10_1909_TerminalScrolling = False,
                        .W10_1909_WindowAlpha = 255}

    Public PowerShellx64 As New Structures.Console With {
                        .Enabled = False,
                        .ColorTable00 = Color.FromArgb(12, 12, 12),
                        .ColorTable01 = Color.FromArgb(0, 55, 218),
                        .ColorTable02 = Color.FromArgb(19, 161, 14),
                        .ColorTable03 = Color.FromArgb(58, 150, 221),
                        .ColorTable04 = Color.FromArgb(197, 15, 31),
                        .ColorTable05 = Color.FromArgb(1, 36, 86),
                        .ColorTable06 = Color.FromArgb(238, 237, 240),
                        .ColorTable07 = Color.FromArgb(204, 204, 204),
                        .ColorTable08 = Color.FromArgb(118, 118, 118),
                        .ColorTable09 = Color.FromArgb(59, 120, 255),
                        .ColorTable10 = Color.FromArgb(22, 198, 12),
                        .ColorTable11 = Color.FromArgb(97, 214, 214),
                        .ColorTable12 = Color.FromArgb(231, 72, 86),
                        .ColorTable13 = Color.FromArgb(180, 0, 158),
                        .ColorTable14 = Color.FromArgb(249, 241, 165),
                        .ColorTable15 = Color.FromArgb(242, 242, 242),
                        .PopupForeground = 15,
                        .PopupBackground = 3,
                        .ScreenColorsForeground = 6,
                        .ScreenColorsBackground = 5,
                        .CursorSize = 19,
                        .FaceName = "Consolas",
                        .FontRaster = False,
                        .FontSize = 16 * 65536,
                        .FontWeight = 400,
                        .W10_1909_CursorType = 0,
                        .W10_1909_CursorColor = Color.White,
                        .W10_1909_ForceV2 = True,
                        .W10_1909_LineSelection = False,
                        .W10_1909_TerminalScrolling = False,
                        .W10_1909_WindowAlpha = 255}

    Public Terminal As New WinTerminal("", WinTerminal.Mode.Empty)

    Public TerminalPreview As New WinTerminal("", WinTerminal.Mode.Empty)

#Region "Cursors"
    Public Cursor_Enabled As Boolean = False

    Public Cursor_Shadow As Boolean = False

    Public Cursor_Sonar As Boolean = False

    Public Cursor_Trails As Integer = 0

    Public Cursor_Arrow As New Structures.Cursor With {
                    .PrimaryColor1 = Color.White,
                    .PrimaryColor2 = Color.White,
                    .PrimaryColorGradient = False,
                    .PrimaryColorGradientMode = GradientMode.Vertical,
                    .PrimaryColorNoise = False,
                    .PrimaryColorNoiseOpacity = 0.25,
                    .SecondaryColor1 = Color.Black,
                    .SecondaryColor2 = Color.FromArgb(64, 65, 75),
                    .SecondaryColorGradient = True,
                    .SecondaryColorGradientMode = GradientMode.Vertical,
                    .SecondaryColorNoise = False,
                    .SecondaryColorNoiseOpacity = 0.25,
                    .LoadingCircleBack1 = Color.FromArgb(42, 151, 243),
                    .LoadingCircleBack2 = Color.FromArgb(42, 151, 243),
                    .LoadingCircleBackGradient = False,
                    .LoadingCircleBackGradientMode = GradientMode.Circle,
                    .LoadingCircleBackNoise = False,
                    .LoadingCircleBackNoiseOpacity = 0.25,
                    .LoadingCircleHot1 = Color.FromArgb(37, 204, 255),
                    .LoadingCircleHot2 = Color.FromArgb(37, 204, 255),
                    .LoadingCircleHotGradient = False,
                    .LoadingCircleHotGradientMode = GradientMode.Circle,
                    .LoadingCircleHotNoise = False,
                    .LoadingCircleHotNoiseOpacity = 0.25,
                    .ArrowStyle = ArrowStyle.Aero,
                    .CircleStyle = CircleStyle.Aero,
                    .Shadow_Enabled = False,
                    .Shadow_Color = Color.Black,
                    .Shadow_Blur = 5,
                    .Shadow_Opacity = 0.3,
                    .Shadow_OffsetX = 2, .Shadow_OffsetY = 2}

    Public Cursor_AppLoading As New Structures.Cursor With {
                    .PrimaryColor1 = Color.White,
                    .PrimaryColor2 = Color.White,
                    .PrimaryColorGradient = False,
                    .PrimaryColorGradientMode = GradientMode.Circle,
                    .PrimaryColorNoise = False,
                    .PrimaryColorNoiseOpacity = 0.25,
                    .SecondaryColor1 = Color.Black,
                    .SecondaryColor2 = Color.FromArgb(64, 65, 75),
                    .SecondaryColorGradient = True,
                    .SecondaryColorGradientMode = GradientMode.Vertical,
                    .SecondaryColorNoise = False,
                    .SecondaryColorNoiseOpacity = 0.25,
                    .LoadingCircleBack1 = Color.FromArgb(42, 151, 243),
                    .LoadingCircleBack2 = Color.FromArgb(42, 151, 243),
                    .LoadingCircleBackGradient = False,
                    .LoadingCircleBackGradientMode = GradientMode.Circle,
                    .LoadingCircleBackNoise = False,
                    .LoadingCircleBackNoiseOpacity = 0.25,
                    .LoadingCircleHot1 = Color.FromArgb(37, 204, 255),
                    .LoadingCircleHot2 = Color.FromArgb(37, 204, 255),
                    .LoadingCircleHotGradient = False,
                    .LoadingCircleHotGradientMode = GradientMode.Circle,
                    .LoadingCircleHotNoise = False,
                    .LoadingCircleHotNoiseOpacity = 0.25,
                    .ArrowStyle = ArrowStyle.Aero,
                    .CircleStyle = CircleStyle.Aero,
                    .Shadow_Enabled = False,
                    .Shadow_Color = Color.Black,
                    .Shadow_Blur = 5,
                    .Shadow_Opacity = 0.3,
                    .Shadow_OffsetX = 2, .Shadow_OffsetY = 2}

    Public Cursor_Busy As New Structures.Cursor With {
                    .PrimaryColor1 = Color.White,
                    .PrimaryColor2 = Color.White,
                    .PrimaryColorGradient = False,
                    .PrimaryColorGradientMode = GradientMode.Circle,
                    .PrimaryColorNoise = False,
                    .PrimaryColorNoiseOpacity = 0.25,
                    .SecondaryColor1 = Color.Black,
                    .SecondaryColor2 = Color.FromArgb(64, 65, 75),
                    .SecondaryColorGradient = True,
                    .SecondaryColorGradientMode = GradientMode.Circle,
                    .SecondaryColorNoise = False,
                    .SecondaryColorNoiseOpacity = 0.25,
                    .LoadingCircleBack1 = Color.FromArgb(42, 151, 243),
                    .LoadingCircleBack2 = Color.FromArgb(42, 151, 243),
                    .LoadingCircleBackGradient = False,
                    .LoadingCircleBackGradientMode = GradientMode.Circle,
                    .LoadingCircleBackNoise = False,
                    .LoadingCircleBackNoiseOpacity = 0.25,
                    .LoadingCircleHot1 = Color.FromArgb(37, 204, 255),
                    .LoadingCircleHot2 = Color.FromArgb(37, 204, 255),
                    .LoadingCircleHotGradient = False,
                    .LoadingCircleHotGradientMode = GradientMode.Circle,
                    .LoadingCircleHotNoise = False,
                    .LoadingCircleHotNoiseOpacity = 0.25,
                    .ArrowStyle = ArrowStyle.Aero,
                    .CircleStyle = CircleStyle.Aero,
                    .Shadow_Enabled = False,
                    .Shadow_Color = Color.Black,
                    .Shadow_Blur = 5,
                    .Shadow_Opacity = 0.3,
                    .Shadow_OffsetX = 2, .Shadow_OffsetY = 2}

    Public Cursor_Help As New Structures.Cursor With {
                    .PrimaryColor1 = Color.White,
                    .PrimaryColor2 = Color.White,
                    .PrimaryColorGradient = False,
                    .PrimaryColorGradientMode = GradientMode.Vertical,
                    .PrimaryColorNoise = False,
                    .PrimaryColorNoiseOpacity = 0.25,
                    .SecondaryColor1 = Color.Black,
                    .SecondaryColor2 = Color.FromArgb(64, 65, 75),
                    .SecondaryColorGradient = True,
                    .SecondaryColorGradientMode = GradientMode.Vertical,
                    .SecondaryColorNoise = False,
                    .SecondaryColorNoiseOpacity = 0.25,
                    .LoadingCircleBack1 = Color.FromArgb(42, 151, 243),
                    .LoadingCircleBack2 = Color.FromArgb(42, 151, 243),
                    .LoadingCircleBackGradient = False,
                    .LoadingCircleBackGradientMode = GradientMode.Circle,
                    .LoadingCircleBackNoise = False,
                    .LoadingCircleBackNoiseOpacity = 0.25,
                    .LoadingCircleHot1 = Color.FromArgb(37, 204, 255),
                    .LoadingCircleHot2 = Color.FromArgb(37, 204, 255),
                    .LoadingCircleHotGradient = False,
                    .LoadingCircleHotGradientMode = GradientMode.Circle,
                    .LoadingCircleHotNoise = False,
                    .LoadingCircleHotNoiseOpacity = 0.25,
                    .ArrowStyle = ArrowStyle.Aero,
                    .CircleStyle = CircleStyle.Aero,
                    .Shadow_Enabled = False,
                    .Shadow_Color = Color.Black,
                    .Shadow_Blur = 5,
                    .Shadow_Opacity = 0.3,
                    .Shadow_OffsetX = 2, .Shadow_OffsetY = 2}

    Public Cursor_Move As New Structures.Cursor With {
                    .PrimaryColor1 = Color.White,
                    .PrimaryColor2 = Color.White,
                    .PrimaryColorGradient = False,
                    .PrimaryColorGradientMode = GradientMode.Vertical,
                    .PrimaryColorNoise = False,
                    .PrimaryColorNoiseOpacity = 0.25,
                    .SecondaryColor1 = Color.Black,
                    .SecondaryColor2 = Color.FromArgb(64, 65, 75),
                    .SecondaryColorGradient = True,
                    .SecondaryColorGradientMode = GradientMode.Vertical,
                    .SecondaryColorNoise = False,
                    .SecondaryColorNoiseOpacity = 0.25,
                    .LoadingCircleBack1 = Color.FromArgb(42, 151, 243),
                    .LoadingCircleBack2 = Color.FromArgb(42, 151, 243),
                    .LoadingCircleBackGradient = False,
                    .LoadingCircleBackGradientMode = GradientMode.Circle,
                    .LoadingCircleBackNoise = False,
                    .LoadingCircleBackNoiseOpacity = 0.25,
                    .LoadingCircleHot1 = Color.FromArgb(37, 204, 255),
                    .LoadingCircleHot2 = Color.FromArgb(37, 204, 255),
                    .LoadingCircleHotGradient = False,
                    .LoadingCircleHotGradientMode = GradientMode.Circle,
                    .LoadingCircleHotNoise = False,
                    .LoadingCircleHotNoiseOpacity = 0.25,
                    .ArrowStyle = ArrowStyle.Aero,
                    .CircleStyle = CircleStyle.Aero,
                    .Shadow_Enabled = False,
                    .Shadow_Color = Color.Black,
                    .Shadow_Blur = 5,
                    .Shadow_Opacity = 0.3,
                    .Shadow_OffsetX = 2, .Shadow_OffsetY = 2}

    Public Cursor_NS As New Structures.Cursor With {
                    .PrimaryColor1 = Color.White,
                    .PrimaryColor2 = Color.White,
                    .PrimaryColorGradient = False,
                    .PrimaryColorGradientMode = GradientMode.Vertical,
                    .PrimaryColorNoise = False,
                    .PrimaryColorNoiseOpacity = 0.25,
                    .SecondaryColor1 = Color.Black,
                    .SecondaryColor2 = Color.FromArgb(64, 65, 75),
                    .SecondaryColorGradient = True,
                    .SecondaryColorGradientMode = GradientMode.Vertical,
                    .SecondaryColorNoise = False,
                    .SecondaryColorNoiseOpacity = 0.25,
                    .LoadingCircleBack1 = Color.FromArgb(42, 151, 243),
                    .LoadingCircleBack2 = Color.FromArgb(42, 151, 243),
                    .LoadingCircleBackGradient = False,
                    .LoadingCircleBackGradientMode = GradientMode.Circle,
                    .LoadingCircleBackNoise = False,
                    .LoadingCircleBackNoiseOpacity = 0.25,
                    .LoadingCircleHot1 = Color.FromArgb(37, 204, 255),
                    .LoadingCircleHot2 = Color.FromArgb(37, 204, 255),
                    .LoadingCircleHotGradient = False,
                    .LoadingCircleHotGradientMode = GradientMode.Circle,
                    .LoadingCircleHotNoise = False,
                    .LoadingCircleHotNoiseOpacity = 0.25,
                    .ArrowStyle = ArrowStyle.Aero,
                    .CircleStyle = CircleStyle.Aero,
                    .Shadow_Enabled = False,
                    .Shadow_Color = Color.Black,
                    .Shadow_Blur = 5,
                    .Shadow_Opacity = 0.3,
                    .Shadow_OffsetX = 2, .Shadow_OffsetY = 2}

    Public Cursor_EW As New Structures.Cursor With {
                    .PrimaryColor1 = Color.White,
                    .PrimaryColor2 = Color.White,
                    .PrimaryColorGradient = False,
                    .PrimaryColorGradientMode = GradientMode.Vertical,
                    .PrimaryColorNoise = False,
                    .PrimaryColorNoiseOpacity = 0.25,
                    .SecondaryColor1 = Color.Black,
                    .SecondaryColor2 = Color.FromArgb(64, 65, 75),
                    .SecondaryColorGradient = True,
                    .SecondaryColorGradientMode = GradientMode.Vertical,
                    .SecondaryColorNoise = False,
                    .SecondaryColorNoiseOpacity = 0.25,
                    .LoadingCircleBack1 = Color.FromArgb(42, 151, 243),
                    .LoadingCircleBack2 = Color.FromArgb(42, 151, 243),
                    .LoadingCircleBackGradient = False,
                    .LoadingCircleBackGradientMode = GradientMode.Circle,
                    .LoadingCircleBackNoise = False,
                    .LoadingCircleBackNoiseOpacity = 0.25,
                    .LoadingCircleHot1 = Color.FromArgb(37, 204, 255),
                    .LoadingCircleHot2 = Color.FromArgb(37, 204, 255),
                    .LoadingCircleHotGradient = False,
                    .LoadingCircleHotGradientMode = GradientMode.Circle,
                    .LoadingCircleHotNoise = False,
                    .LoadingCircleHotNoiseOpacity = 0.25,
                    .ArrowStyle = ArrowStyle.Aero,
                    .CircleStyle = CircleStyle.Aero,
                    .Shadow_Enabled = False,
                    .Shadow_Color = Color.Black,
                    .Shadow_Blur = 5,
                    .Shadow_Opacity = 0.3,
                    .Shadow_OffsetX = 2, .Shadow_OffsetY = 2}

    Public Cursor_NESW As New Structures.Cursor With {
                    .PrimaryColor1 = Color.White,
                    .PrimaryColor2 = Color.White,
                    .PrimaryColorGradient = False,
                    .PrimaryColorGradientMode = GradientMode.Vertical,
                    .PrimaryColorNoise = False,
                    .PrimaryColorNoiseOpacity = 0.25,
                    .SecondaryColor1 = Color.Black,
                    .SecondaryColor2 = Color.FromArgb(64, 65, 75),
                    .SecondaryColorGradient = True,
                    .SecondaryColorGradientMode = GradientMode.Vertical,
                    .SecondaryColorNoise = False,
                    .SecondaryColorNoiseOpacity = 0.25,
                    .LoadingCircleBack1 = Color.FromArgb(42, 151, 243),
                    .LoadingCircleBack2 = Color.FromArgb(42, 151, 243),
                    .LoadingCircleBackGradient = False,
                    .LoadingCircleBackGradientMode = GradientMode.Circle,
                    .LoadingCircleBackNoise = False,
                    .LoadingCircleBackNoiseOpacity = 0.25,
                    .LoadingCircleHot1 = Color.FromArgb(37, 204, 255),
                    .LoadingCircleHot2 = Color.FromArgb(37, 204, 255),
                    .LoadingCircleHotGradient = False,
                    .LoadingCircleHotGradientMode = GradientMode.Circle,
                    .LoadingCircleHotNoise = False,
                    .LoadingCircleHotNoiseOpacity = 0.25,
                    .ArrowStyle = ArrowStyle.Aero,
                    .CircleStyle = CircleStyle.Aero,
                    .Shadow_Enabled = False,
                    .Shadow_Color = Color.Black,
                    .Shadow_Blur = 5,
                    .Shadow_Opacity = 0.3,
                    .Shadow_OffsetX = 2, .Shadow_OffsetY = 2}

    Public Cursor_NWSE As New Structures.Cursor With {
                    .PrimaryColor1 = Color.White,
                    .PrimaryColor2 = Color.White,
                    .PrimaryColorGradient = False,
                    .PrimaryColorGradientMode = GradientMode.Vertical,
                    .PrimaryColorNoise = False,
                    .PrimaryColorNoiseOpacity = 0.25,
                    .SecondaryColor1 = Color.Black,
                    .SecondaryColor2 = Color.FromArgb(64, 65, 75),
                    .SecondaryColorGradient = True,
                    .SecondaryColorGradientMode = GradientMode.Vertical,
                    .SecondaryColorNoise = False,
                    .SecondaryColorNoiseOpacity = 0.25,
                    .LoadingCircleBack1 = Color.FromArgb(42, 151, 243),
                    .LoadingCircleBack2 = Color.FromArgb(42, 151, 243),
                    .LoadingCircleBackGradient = False,
                    .LoadingCircleBackGradientMode = GradientMode.Circle,
                    .LoadingCircleBackNoise = False,
                    .LoadingCircleBackNoiseOpacity = 0.25,
                    .LoadingCircleHot1 = Color.FromArgb(37, 204, 255),
                    .LoadingCircleHot2 = Color.FromArgb(37, 204, 255),
                    .LoadingCircleHotGradient = False,
                    .LoadingCircleHotGradientMode = GradientMode.Circle,
                    .LoadingCircleHotNoise = False,
                    .LoadingCircleHotNoiseOpacity = 0.25,
                    .ArrowStyle = ArrowStyle.Aero,
                    .CircleStyle = CircleStyle.Aero,
                    .Shadow_Enabled = False,
                    .Shadow_Color = Color.Black,
                    .Shadow_Blur = 5,
                    .Shadow_Opacity = 0.3,
                    .Shadow_OffsetX = 2, .Shadow_OffsetY = 2}

    Public Cursor_Up As New Structures.Cursor With {
                    .PrimaryColor1 = Color.White,
                    .PrimaryColor2 = Color.White,
                    .PrimaryColorGradient = False,
                    .PrimaryColorGradientMode = GradientMode.Vertical,
                    .PrimaryColorNoise = False,
                    .PrimaryColorNoiseOpacity = 0.25,
                    .SecondaryColor1 = Color.Black,
                    .SecondaryColor2 = Color.FromArgb(64, 65, 75),
                    .SecondaryColorGradient = True,
                    .SecondaryColorGradientMode = GradientMode.Vertical,
                    .SecondaryColorNoise = False,
                    .SecondaryColorNoiseOpacity = 0.25,
                    .LoadingCircleBack1 = Color.FromArgb(42, 151, 243),
                    .LoadingCircleBack2 = Color.FromArgb(42, 151, 243),
                    .LoadingCircleBackGradient = False,
                    .LoadingCircleBackGradientMode = GradientMode.Circle,
                    .LoadingCircleBackNoise = False,
                    .LoadingCircleBackNoiseOpacity = 0.25,
                    .LoadingCircleHot1 = Color.FromArgb(37, 204, 255),
                    .LoadingCircleHot2 = Color.FromArgb(37, 204, 255),
                    .LoadingCircleHotGradient = False,
                    .LoadingCircleHotGradientMode = GradientMode.Circle,
                    .LoadingCircleHotNoise = False,
                    .LoadingCircleHotNoiseOpacity = 0.25,
                    .ArrowStyle = ArrowStyle.Aero,
                    .CircleStyle = CircleStyle.Aero,
                    .Shadow_Enabled = False,
                    .Shadow_Color = Color.Black,
                    .Shadow_Blur = 5,
                    .Shadow_Opacity = 0.3,
                    .Shadow_OffsetX = 2, .Shadow_OffsetY = 2}

    Public Cursor_Pen As New Structures.Cursor With {
                    .PrimaryColor1 = Color.White,
                    .PrimaryColor2 = Color.White,
                    .PrimaryColorGradient = False,
                    .PrimaryColorGradientMode = GradientMode.Vertical,
                    .PrimaryColorNoise = False,
                    .PrimaryColorNoiseOpacity = 0.25,
                    .SecondaryColor1 = Color.Black,
                    .SecondaryColor2 = Color.FromArgb(64, 65, 75),
                    .SecondaryColorGradient = True,
                    .SecondaryColorGradientMode = GradientMode.Vertical,
                    .SecondaryColorNoise = False,
                    .SecondaryColorNoiseOpacity = 0.25,
                    .LoadingCircleBack1 = Color.FromArgb(42, 151, 243),
                    .LoadingCircleBack2 = Color.FromArgb(42, 151, 243),
                    .LoadingCircleBackGradient = False,
                    .LoadingCircleBackGradientMode = GradientMode.Circle,
                    .LoadingCircleBackNoise = False,
                    .LoadingCircleBackNoiseOpacity = 0.25,
                    .LoadingCircleHot1 = Color.FromArgb(37, 204, 255),
                    .LoadingCircleHot2 = Color.FromArgb(37, 204, 255),
                    .LoadingCircleHotGradient = False,
                    .LoadingCircleHotGradientMode = GradientMode.Circle,
                    .LoadingCircleHotNoise = False,
                    .LoadingCircleHotNoiseOpacity = 0.25,
                    .ArrowStyle = ArrowStyle.Aero,
                    .CircleStyle = CircleStyle.Aero,
                    .Shadow_Enabled = False,
                    .Shadow_Color = Color.Black,
                    .Shadow_Blur = 5,
                    .Shadow_Opacity = 0.3,
                    .Shadow_OffsetX = 2, .Shadow_OffsetY = 2}

    Public Cursor_None As New Structures.Cursor With {
                    .PrimaryColor1 = Color.White,
                    .PrimaryColor2 = Color.White,
                    .PrimaryColorGradient = False,
                    .PrimaryColorGradientMode = GradientMode.Vertical,
                    .PrimaryColorNoise = False,
                    .PrimaryColorNoiseOpacity = 0.25,
                    .SecondaryColor1 = Color.FromArgb(255, 0, 0),
                    .SecondaryColor2 = Color.FromArgb(255, 0, 0),
                    .SecondaryColorGradient = True,
                    .SecondaryColorGradientMode = GradientMode.Vertical,
                    .SecondaryColorNoise = False,
                    .SecondaryColorNoiseOpacity = 0.25,
                    .LoadingCircleBack1 = Color.FromArgb(42, 151, 243),
                    .LoadingCircleBack2 = Color.FromArgb(42, 151, 243),
                    .LoadingCircleBackGradient = False,
                    .LoadingCircleBackGradientMode = GradientMode.Circle,
                    .LoadingCircleBackNoise = False,
                    .LoadingCircleBackNoiseOpacity = 0.25,
                    .LoadingCircleHot1 = Color.FromArgb(37, 204, 255),
                    .LoadingCircleHot2 = Color.FromArgb(37, 204, 255),
                    .LoadingCircleHotGradient = False,
                    .LoadingCircleHotGradientMode = GradientMode.Circle,
                    .LoadingCircleHotNoise = False,
                    .LoadingCircleHotNoiseOpacity = 0.25,
                    .ArrowStyle = ArrowStyle.Aero,
                    .CircleStyle = CircleStyle.Aero,
                    .Shadow_Enabled = False,
                    .Shadow_Color = Color.Black,
                    .Shadow_Blur = 5,
                    .Shadow_Opacity = 0.3,
                    .Shadow_OffsetX = 2, .Shadow_OffsetY = 2}

    Public Cursor_Link As New Structures.Cursor With {
                    .PrimaryColor1 = Color.White,
                    .PrimaryColor2 = Color.White,
                    .PrimaryColorGradient = False,
                    .PrimaryColorGradientMode = GradientMode.Vertical,
                    .PrimaryColorNoise = False,
                    .PrimaryColorNoiseOpacity = 0.25,
                    .SecondaryColor1 = Color.Black,
                    .SecondaryColor2 = Color.FromArgb(64, 65, 75),
                    .SecondaryColorGradient = True,
                    .SecondaryColorGradientMode = GradientMode.Vertical,
                    .SecondaryColorNoise = False,
                    .SecondaryColorNoiseOpacity = 0.25,
                    .LoadingCircleBack1 = Color.FromArgb(42, 151, 243),
                    .LoadingCircleBack2 = Color.FromArgb(42, 151, 243),
                    .LoadingCircleBackGradient = False,
                    .LoadingCircleBackGradientMode = GradientMode.Circle,
                    .LoadingCircleBackNoise = False,
                    .LoadingCircleBackNoiseOpacity = 0.25,
                    .LoadingCircleHot1 = Color.FromArgb(37, 204, 255),
                    .LoadingCircleHot2 = Color.FromArgb(37, 204, 255),
                    .LoadingCircleHotGradient = False,
                    .LoadingCircleHotGradientMode = GradientMode.Circle,
                    .LoadingCircleHotNoise = False,
                    .LoadingCircleHotNoiseOpacity = 0.25,
                    .ArrowStyle = ArrowStyle.Aero,
                    .CircleStyle = CircleStyle.Aero,
                    .Shadow_Enabled = False,
                    .Shadow_Color = Color.Black,
                    .Shadow_Blur = 5,
                    .Shadow_Opacity = 0.3,
                    .Shadow_OffsetX = 2, .Shadow_OffsetY = 2}

    Public Cursor_Pin As New Structures.Cursor With {
                    .PrimaryColor1 = Color.White,
                    .PrimaryColor2 = Color.White,
                    .PrimaryColorGradient = False,
                    .PrimaryColorGradientMode = GradientMode.Vertical,
                    .PrimaryColorNoise = False,
                    .PrimaryColorNoiseOpacity = 0.25,
                    .SecondaryColor1 = Color.Black,
                    .SecondaryColor2 = Color.FromArgb(64, 65, 75),
                    .SecondaryColorGradient = True,
                    .SecondaryColorGradientMode = GradientMode.Vertical,
                    .SecondaryColorNoise = False,
                    .SecondaryColorNoiseOpacity = 0.25,
                    .LoadingCircleBack1 = Color.FromArgb(42, 151, 243),
                    .LoadingCircleBack2 = Color.FromArgb(42, 151, 243),
                    .LoadingCircleBackGradient = False,
                    .LoadingCircleBackGradientMode = GradientMode.Circle,
                    .LoadingCircleBackNoise = False,
                    .LoadingCircleBackNoiseOpacity = 0.25,
                    .LoadingCircleHot1 = Color.FromArgb(37, 204, 255),
                    .LoadingCircleHot2 = Color.FromArgb(37, 204, 255),
                    .LoadingCircleHotGradient = False,
                    .LoadingCircleHotGradientMode = GradientMode.Circle,
                    .LoadingCircleHotNoise = False,
                    .LoadingCircleHotNoiseOpacity = 0.25,
                    .ArrowStyle = ArrowStyle.Aero,
                    .CircleStyle = CircleStyle.Aero,
                    .Shadow_Enabled = False,
                    .Shadow_Color = Color.Black,
                    .Shadow_Blur = 5,
                    .Shadow_Opacity = 0.3,
                    .Shadow_OffsetX = 2, .Shadow_OffsetY = 2}

    Public Cursor_Person As New Structures.Cursor With {
                    .PrimaryColor1 = Color.White,
                    .PrimaryColor2 = Color.White,
                    .PrimaryColorGradient = False,
                    .PrimaryColorGradientMode = GradientMode.Vertical,
                    .PrimaryColorNoise = False,
                    .PrimaryColorNoiseOpacity = 0.25,
                    .SecondaryColor1 = Color.Black,
                    .SecondaryColor2 = Color.FromArgb(64, 65, 75),
                    .SecondaryColorGradient = True,
                    .SecondaryColorGradientMode = GradientMode.Vertical,
                    .SecondaryColorNoise = False,
                    .SecondaryColorNoiseOpacity = 0.25,
                    .LoadingCircleBack1 = Color.FromArgb(42, 151, 243),
                    .LoadingCircleBack2 = Color.FromArgb(42, 151, 243),
                    .LoadingCircleBackGradient = False,
                    .LoadingCircleBackGradientMode = GradientMode.Circle,
                    .LoadingCircleBackNoise = False,
                    .LoadingCircleBackNoiseOpacity = 0.25,
                    .LoadingCircleHot1 = Color.FromArgb(37, 204, 255),
                    .LoadingCircleHot2 = Color.FromArgb(37, 204, 255),
                    .LoadingCircleHotGradient = False,
                    .LoadingCircleHotGradientMode = GradientMode.Circle,
                    .LoadingCircleHotNoise = False,
                    .LoadingCircleHotNoiseOpacity = 0.25,
                    .ArrowStyle = ArrowStyle.Aero,
                    .CircleStyle = CircleStyle.Aero,
                    .Shadow_Enabled = False,
                    .Shadow_Color = Color.Black,
                    .Shadow_Blur = 5,
                    .Shadow_Opacity = 0.3,
                    .Shadow_OffsetX = 2, .Shadow_OffsetY = 2}

    Public Cursor_IBeam As New Structures.Cursor With {
                    .PrimaryColor1 = Color.White,
                    .PrimaryColor2 = Color.White,
                    .PrimaryColorGradient = False,
                    .PrimaryColorGradientMode = GradientMode.Vertical,
                    .PrimaryColorNoise = False,
                    .PrimaryColorNoiseOpacity = 0.25,
                    .SecondaryColor1 = Color.Black,
                    .SecondaryColor2 = Color.FromArgb(64, 65, 75),
                    .SecondaryColorGradient = True,
                    .SecondaryColorGradientMode = GradientMode.Vertical,
                    .SecondaryColorNoise = False,
                    .SecondaryColorNoiseOpacity = 0.25,
                    .LoadingCircleBack1 = Color.FromArgb(42, 151, 243),
                    .LoadingCircleBack2 = Color.FromArgb(42, 151, 243),
                    .LoadingCircleBackGradient = False,
                    .LoadingCircleBackGradientMode = GradientMode.Circle,
                    .LoadingCircleBackNoise = False,
                    .LoadingCircleBackNoiseOpacity = 0.25,
                    .LoadingCircleHot1 = Color.FromArgb(37, 204, 255),
                    .LoadingCircleHot2 = Color.FromArgb(37, 204, 255),
                    .LoadingCircleHotGradient = False,
                    .LoadingCircleHotGradientMode = GradientMode.Circle,
                    .LoadingCircleHotNoise = False,
                    .LoadingCircleHotNoiseOpacity = 0.25,
                    .ArrowStyle = ArrowStyle.Aero,
                    .CircleStyle = CircleStyle.Aero,
                    .Shadow_Enabled = False,
                    .Shadow_Color = Color.Black,
                    .Shadow_Blur = 5,
                    .Shadow_Opacity = 0.3,
                    .Shadow_OffsetX = 2, .Shadow_OffsetY = 2}

    Public Cursor_Cross As New Structures.Cursor With {
                    .PrimaryColor1 = Color.White,
                    .PrimaryColor2 = Color.White,
                    .PrimaryColorGradient = False,
                    .PrimaryColorGradientMode = GradientMode.Vertical,
                    .PrimaryColorNoise = False,
                    .PrimaryColorNoiseOpacity = 0.25,
                    .SecondaryColor1 = Color.Black,
                    .SecondaryColor2 = Color.FromArgb(64, 65, 75),
                    .SecondaryColorGradient = True,
                    .SecondaryColorGradientMode = GradientMode.Vertical,
                    .SecondaryColorNoise = False,
                    .SecondaryColorNoiseOpacity = 0.25,
                    .LoadingCircleBack1 = Color.FromArgb(42, 151, 243),
                    .LoadingCircleBack2 = Color.FromArgb(42, 151, 243),
                    .LoadingCircleBackGradient = False,
                    .LoadingCircleBackGradientMode = GradientMode.Circle,
                    .LoadingCircleBackNoise = False,
                    .LoadingCircleBackNoiseOpacity = 0.25,
                    .LoadingCircleHot1 = Color.FromArgb(37, 204, 255),
                    .LoadingCircleHot2 = Color.FromArgb(37, 204, 255),
                    .LoadingCircleHotGradient = False,
                    .LoadingCircleHotGradientMode = GradientMode.Circle,
                    .LoadingCircleHotNoise = False,
                    .LoadingCircleHotNoiseOpacity = 0.25,
                    .ArrowStyle = ArrowStyle.Aero,
                    .CircleStyle = CircleStyle.Aero,
                    .Shadow_Enabled = False,
                    .Shadow_Color = Color.Black,
                    .Shadow_Blur = 5,
                    .Shadow_Opacity = 0.3,
                    .Shadow_OffsetX = 2, .Shadow_OffsetY = 2}
#End Region

#End Region

#Region "Functions"
    Public Shared Function GetPaletteFromMSTheme(Filename As String) As List(Of Color)
        If IO.File.Exists(Filename) Then

            Dim ls As New List(Of Color)
            ls.Clear()

            Dim tx As List(Of String) = IO.File.ReadAllText(Filename).CList

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
                                If inx Then ls.Add(c.FromWin32RegToColor)
                            End If
                        End If
                    End If
                Catch
                End Try
            Next
            ls = ls.Distinct.ToList
            ls.Sort(New RGBColorComparer())
            Return ls
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

        Dim AllThemes As List(Of String) = [String].CList
        Dim SelectedTheme As String = ""
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

        Dim SelectedThemeList As List(Of String) = SelectedTheme.CList

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

        ls = ls.Distinct.ToList
        ls.Sort(New RGBColorComparer())
        Return ls
    End Function
    Public Function ListColors(Optional DontMergeRepeatedColors As Boolean = False) As List(Of Color)

        Dim CL As New List(Of Color)
        CL.Clear()

        For Each field In GetType(Structures.Windows10x).GetFields(BindingFlags.Instance Or BindingFlags.NonPublic Or BindingFlags.Public)
            If field.FieldType.Name.ToLower = "color" Then
                CL.Add(field.GetValue(Windows11))
                CL.Add(field.GetValue(Windows10))
            End If
        Next

        For Each field In GetType(Structures.LogonUI10x).GetFields(BindingFlags.Instance Or BindingFlags.NonPublic Or BindingFlags.Public)
            If field.FieldType.Name.ToLower = "color" Then
                CL.Add(field.GetValue(LogonUI10x))
            End If
        Next

        For Each field In GetType(Structures.Windows8x).GetFields(BindingFlags.Instance Or BindingFlags.NonPublic Or BindingFlags.Public)
            If field.FieldType.Name.ToLower = "color" Then
                CL.Add(field.GetValue(Windows81))
            End If
        Next

        For Each field In GetType(Structures.Windows7).GetFields(BindingFlags.Instance Or BindingFlags.NonPublic Or BindingFlags.Public)
            If field.FieldType.Name.ToLower = "color" Then
                CL.Add(field.GetValue(Windows7))
            End If
        Next

        For Each field In GetType(Structures.WindowsVista).GetFields(BindingFlags.Instance Or BindingFlags.NonPublic Or BindingFlags.Public)
            If field.FieldType.Name.ToLower = "color" Then
                CL.Add(field.GetValue(WindowsVista))
            End If
        Next

        For Each field In GetType(Structures.WindowsXP).GetFields(BindingFlags.Instance Or BindingFlags.NonPublic Or BindingFlags.Public)
            If field.FieldType.Name.ToLower = "color" Then
                CL.Add(field.GetValue(WindowsXP))
            End If
        Next

        For Each field In GetType(Structures.LogonUI7).GetFields(BindingFlags.Instance Or BindingFlags.NonPublic Or BindingFlags.Public)
            If field.FieldType.Name.ToLower = "color" Then
                CL.Add(field.GetValue(LogonUI7))
            End If
        Next

        For Each field In GetType(Structures.LogonUIXP).GetFields(BindingFlags.Instance Or BindingFlags.NonPublic Or BindingFlags.Public)
            If field.FieldType.Name.ToLower = "color" Then
                CL.Add(field.GetValue(LogonUIXP))
            End If
        Next

        For Each field In GetType(Structures.Win32UI).GetFields(BindingFlags.Instance Or BindingFlags.NonPublic Or BindingFlags.Public)
            If field.FieldType.Name.ToLower = "color" Then
                CL.Add(field.GetValue(Win32))
            End If
        Next

        For Each field In GetType(Structures.WallpaperTone).GetFields(BindingFlags.Instance Or BindingFlags.NonPublic Or BindingFlags.Public)
            If field.FieldType.Name.ToLower = "color" Then
                CL.Add(field.GetValue(WallpaperTone_W11))
                CL.Add(field.GetValue(WallpaperTone_W10))
                CL.Add(field.GetValue(WallpaperTone_W81))
                CL.Add(field.GetValue(WallpaperTone_W7))
                CL.Add(field.GetValue(WallpaperTone_WVista))
                CL.Add(field.GetValue(WallpaperTone_WXP))
            End If
        Next

        For Each field In GetType(Structures.Console).GetFields(BindingFlags.Instance Or BindingFlags.NonPublic Or BindingFlags.Public)
            If field.FieldType.Name.ToLower = "color" Then
                CL.Add(field.GetValue(CommandPrompt))
                CL.Add(field.GetValue(PowerShellx86))
                CL.Add(field.GetValue(PowerShellx64))
            End If
        Next

        For Each c In Terminal.Colors
            CL.Add(c.Background)
            CL.Add(c.Foreground)
            CL.Add(c.SelectionBackground)
            CL.Add(c.Black)
            CL.Add(c.Blue)
            CL.Add(c.BrightBlack)
            CL.Add(c.BrightBlue)
            CL.Add(c.BrightCyan)
            CL.Add(c.BrightGreen)
            CL.Add(c.BrightPurple)
            CL.Add(c.BrightRed)
            CL.Add(c.BrightWhite)
            CL.Add(c.BrightYellow)
            CL.Add(c.CursorColor)
            CL.Add(c.Cyan)
            CL.Add(c.Green)
            CL.Add(c.Purple)
            CL.Add(c.Red)
            CL.Add(c.White)
            CL.Add(c.Yellow)
        Next

        For Each c In TerminalPreview.Colors
            CL.Add(c.Background)
            CL.Add(c.Foreground)
            CL.Add(c.SelectionBackground)
            CL.Add(c.Black)
            CL.Add(c.Blue)
            CL.Add(c.BrightBlack)
            CL.Add(c.BrightBlue)
            CL.Add(c.BrightCyan)
            CL.Add(c.BrightGreen)
            CL.Add(c.BrightPurple)
            CL.Add(c.BrightRed)
            CL.Add(c.BrightWhite)
            CL.Add(c.BrightYellow)
            CL.Add(c.CursorColor)
            CL.Add(c.Cyan)
            CL.Add(c.Green)
            CL.Add(c.Purple)
            CL.Add(c.Red)
            CL.Add(c.White)
            CL.Add(c.Yellow)
        Next

        For Each c In Terminal.Themes
            CL.Add(c.Titlebar_Inactive)
            CL.Add(c.Titlebar_Active)
            CL.Add(c.Tab_Active)
            CL.Add(c.Tab_Inactive)
        Next

        For Each c In TerminalPreview.Themes
            CL.Add(c.Titlebar_Inactive)
            CL.Add(c.Titlebar_Active)
            CL.Add(c.Tab_Active)
            CL.Add(c.Tab_Inactive)
        Next

        For Each c In Terminal.Profiles
            CL.Add(c.TabColor)
        Next

        For Each c In TerminalPreview.Profiles
            CL.Add(c.TabColor)
        Next

        CL.Add(Terminal.DefaultProf.TabColor)
        CL.Add(TerminalPreview.DefaultProf.TabColor)

        For Each field In GetType(Cursor).GetFields(BindingFlags.Instance Or BindingFlags.NonPublic Or BindingFlags.Public)
            If field.FieldType.Name.ToLower = "color" Then
                CL.Add(field.GetValue(Cursor_Arrow))
                CL.Add(field.GetValue(Cursor_Help))
                CL.Add(field.GetValue(Cursor_AppLoading))
                CL.Add(field.GetValue(Cursor_Busy))
                CL.Add(field.GetValue(Cursor_Pen))
                CL.Add(field.GetValue(Cursor_None))
                CL.Add(field.GetValue(Cursor_Move))
                CL.Add(field.GetValue(Cursor_Up))
                CL.Add(field.GetValue(Cursor_NS))
                CL.Add(field.GetValue(Cursor_EW))
                CL.Add(field.GetValue(Cursor_NESW))
                CL.Add(field.GetValue(Cursor_NWSE))
                CL.Add(field.GetValue(Cursor_Link))
                CL.Add(field.GetValue(Cursor_Pin))
                CL.Add(field.GetValue(Cursor_Person))
                CL.Add(field.GetValue(Cursor_IBeam))
                CL.Add(field.GetValue(Cursor_Cross))
            End If
        Next

        If Not DontMergeRepeatedColors Then CL = CL.Distinct.ToList

        CL.Sort(New RGBColorComparer())

        If CL.Contains(Color.FromArgb(0, 0, 0, 0)) Then
            Do Until Not CL.Contains(Color.FromArgb(0, 0, 0, 0))
                CL.Remove(Color.FromArgb(0, 0, 0, 0))
            Loop
        End If

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
    Public Shared Sub AddNode([TreeView] As TreeView, [Text] As String, [ImageKey] As String)
        If [TreeView] IsNot Nothing Then
            If [TreeView].InvokeRequired Then

                Try
                    [TreeView].Invoke(CType(Sub()
                                                With [TreeView].Nodes.Add([Text])
                                                    .ImageKey = [ImageKey] : .SelectedImageKey = [ImageKey]
                                                End With
                                                [TreeView].SelectedNode = [TreeView].Nodes([TreeView].Nodes.Count - 1)
                                                [TreeView].Update()
                                            End Sub, MethodInvoker))
                Catch
                End Try

            Else

                Try
                    [TreeView].Invoke(CType(Sub()
                                                With [TreeView].Nodes.Add([Text])
                                                    .ImageKey = [ImageKey] : .SelectedImageKey = [ImageKey]
                                                End With
                                                [TreeView].SelectedNode = [TreeView].Nodes([TreeView].Nodes.Count - 1)
                                                [TreeView].Update()
                                            End Sub, MethodInvoker))
                Catch
                End Try
            End If

        End If
    End Sub
    Private Sub AddException([Label] As String, [Exception] As Exception)
        My.Saving_Exceptions.Add(New Tuple(Of String, Exception)([Label], [Exception]))
    End Sub
    Sub Execute(ByVal [Sub] As MethodInvoker, Optional [TreeView] As Windows.Forms.TreeView = Nothing, Optional StartStr As String = "", Optional ErrorStr As String = "",
               Optional TimeStr As String = "", Optional overallStopwatch As Stopwatch = Nothing, Optional Skip As Boolean = False, Optional SkipStr As String = "", Optional ExecuteEvenIfSkip As Boolean = False)

        Dim ReportProgress As Boolean = [TreeView] IsNot Nothing
        Dim sw As New Stopwatch
        sw.Reset()
        sw.Stop()
        sw.Start()

        If Not Skip Or ExecuteEvenIfSkip Then
            If Not ExecuteEvenIfSkip Then
                If Not String.IsNullOrWhiteSpace(StartStr) Then AddNode([TreeView], String.Format("{0}: {1}", Now.ToLongTimeString, StartStr), "apply")
            Else
                If Not String.IsNullOrWhiteSpace(ErrorStr) Then AddNode([TreeView], String.Format("{0}: {1}", Now.ToLongTimeString, SkipStr), "skip")
            End If

            Try
                [Sub]()
                If ReportProgress And Not String.IsNullOrWhiteSpace(TimeStr) Then AddNode([TreeView], String.Format(TimeStr, sw.ElapsedMilliseconds / 1000), "time")
            Catch ex As Exception
                sw.Stop() : overallStopwatch.Stop()
                _ErrorHappened = True
                If ReportProgress Then
                    If Not String.IsNullOrWhiteSpace(ErrorStr) Then AddNode([TreeView], String.Format("{0}: {1}", Now.ToLongTimeString, ErrorStr), "error")
                    AddException(ErrorStr, ex)
                Else
                    BugReport.ThrowError(ex)
                End If
                sw.Start() : overallStopwatch.Start()
            End Try
        Else
            If Not String.IsNullOrWhiteSpace(ErrorStr) Then AddNode([TreeView], String.Format("{0}: {1}", Now.ToLongTimeString, SkipStr), "skip")
        End If

        sw.Stop()
    End Sub
#End Region

#Region "CP Handling (Loading/Applying)"
    Sub New(CP_Type As CP_Type, Optional File As String = "", Optional IgnoreExtractionThemePack As Boolean = False)

        Select Case CP_Type
            Case CP_Type.Registry

                Using _Def As CP = CP_Defaults.From(My.PreviewStyle)
                    My.Loading_Exceptions.Clear()

#Region "Registry"
                    Info.Load()
                    Windows11.Load(New CP_Defaults().Default_Windows11.Windows11, New CP_Defaults().Default_Windows11Accents_Bytes)
                    Windows10.Load(New CP_Defaults().Default_Windows10.Windows10, New CP_Defaults().Default_Windows10Accents_Bytes)
                    Windows81.Load(_Def.Windows81)
                    Windows7.Load(_Def.Windows7)
                    WindowsVista.Load(_Def.WindowsVista)
                    WindowsXP.Load(_Def.WindowsXP)
                    WindowsEffects.Load(_Def.WindowsEffects)
                    LogonUI10x.Load(_Def.LogonUI10x)
                    LogonUI7.Load(_Def.LogonUI7)
                    LogonUIXP.Load(_Def.LogonUIXP)
                    Win32.Load()
                    MetricsFonts.Load(_Def.MetricsFonts)
                    AltTab.Load(_Def.AltTab)
                    ScreenSaver.Load(_Def.ScreenSaver)
                    Sounds.Load(_Def.Sounds)
                    AppTheme.Load(_Def.AppTheme)

                    WallpaperTone_W11.Load("Win11")
                    WallpaperTone_W10.Load("Win10")
                    WallpaperTone_W81.Load("Win8.1")
                    WallpaperTone_W7.Load("Win7")
                    WallpaperTone_WVista.Load("WinVista")
                    WallpaperTone_WXP.Load("WinXP")
                    Wallpaper.Load(_Def.Wallpaper)

                    CommandPrompt.Load("", "Terminal_CMD_Enabled", _Def.CommandPrompt)
                    If IO.Directory.Exists(My.PATH_PS86_app) Then
                        Try : Registry.CurrentUser.CreateSubKey("Console\" & My.PATH_PS86_reg, True).Close() : Catch : End Try
                        PowerShellx86.Load(My.PATH_PS86_reg, "Terminal_PS_32_Enabled", _Def.PowerShellx86)
                    Else
                        PowerShellx86 = _Def.PowerShellx86
                    End If
                    If IO.Directory.Exists(My.PATH_PS64_app) Then
                        Try : Registry.CurrentUser.CreateSubKey("Console\" & My.PATH_PS64_reg, True).Close() : Catch : End Try
                        PowerShellx64.Load(My.PATH_PS64_reg, "Terminal_PS_64_Enabled", _Def.PowerShellx64)
                    Else
                        PowerShellx64 = _Def.PowerShellx64
                    End If


#Region "Windows Terminal"
                    Terminal.Enabled = CInt(GetReg("HKEY_CURRENT_USER\Software\WinPaletter\Terminals", "Terminal_Stable_Enabled", 0)).ToBoolean
                    TerminalPreview.Enabled = CInt(GetReg("HKEY_CURRENT_USER\Software\WinPaletter\Terminals", "Terminal_Preview_Enabled", 0)).ToBoolean

                    If My.W10 Or My.W11 Then
                        Dim TerDir As String
                        Dim TerPreDir As String

                        If Not My.Settings.WindowsTerminals.Path_Deflection Then
                            TerDir = My.PATH_TerminalJSON
                            TerPreDir = My.PATH_TerminalPreviewJSON
                        Else
                            If IO.File.Exists(My.Settings.WindowsTerminals.Terminal_Stable_Path) Then
                                TerDir = My.Settings.WindowsTerminals.Terminal_Stable_Path
                            Else
                                TerDir = My.PATH_TerminalJSON
                            End If

                            If IO.File.Exists(My.Settings.WindowsTerminals.Terminal_Preview_Path) Then
                                TerPreDir = My.Settings.WindowsTerminals.Terminal_Preview_Path
                            Else
                                TerPreDir = My.PATH_TerminalPreviewJSON
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

#Region "Cursors"
                    Cursor_Enabled = GetReg("HKEY_CURRENT_USER\Software\WinPaletter\Cursors", "", False)

                    If Fixer.SystemParametersInfo(SPI.Cursors.GETCURSORSHADOW, 0, Cursor_Shadow, SPIF.None) = 0 Then Cursor_Shadow = _Def.Cursor_Shadow
                    If Fixer.SystemParametersInfo(SPI.Cursors.GETMOUSETRAILS, 0, Cursor_Trails, SPIF.None) = 0 Then Cursor_Trails = _Def.Cursor_Trails
                    If Fixer.SystemParametersInfo(SPI.Cursors.GETMOUSESONAR, 0, Cursor_Sonar, SPIF.None) = 0 Then Cursor_Sonar = _Def.Cursor_Sonar

                    Cursor_Arrow.Load("Arrow")
                    Cursor_Help.Load("Help")
                    Cursor_AppLoading.Load("AppLoading")
                    Cursor_Busy.Load("Busy")
                    Cursor_Move.Load("Move")
                    Cursor_NS.Load("NS")
                    Cursor_EW.Load("EW")
                    Cursor_NESW.Load("NESW")
                    Cursor_NWSE.Load("NWSE")
                    Cursor_Up.Load("Up")
                    Cursor_Pen.Load("Pen")
                    Cursor_None.Load("None")
                    Cursor_Link.Load("Link")
                    Cursor_Pin.Load("Pin")
                    Cursor_Person.Load("Person")
                    Cursor_IBeam.Load("IBeam")
                    Cursor_Cross.Load("Cross")
#End Region

                    If My.Loading_Exceptions.Count > 0 Then
                        Saving_ex_list.ex_List = My.Loading_Exceptions
                        Saving_ex_list.ShowDialog()
                    End If
#End Region
                End Using

            Case CP_Type.File

#Region "File"
                Using CPx As CP = CP_Defaults.GetDefault
                    For Each field As FieldInfo In Me.GetType.GetFields(bindingFlags)
                        Dim type As Type = field.FieldType
                        Try
                            field.SetValue(Me, field.GetValue(CPx))
                        Catch
                        End Try
                    Next
                End Using

Start:

                If Not IO.File.Exists(File) Then Exit Sub

                'Rough method to get theme name to create its proper resources pack folder
                For Each line In Decompress(File)
                    If line.Trim.StartsWith("""ThemeName"":") Then
                        Info.ThemeName = line.Split(":")(1).ToString.Replace("""", "").Replace(",", "").Trim
                        Exit For
                    End If
                Next

                Dim txt As New List(Of String) : txt.Clear()
                Dim Pack As String = New IO.FileInfo(File).DirectoryName & "\" & IO.Path.GetFileNameWithoutExtension(File) & ".wptp"
                Dim Pack_IsValid As Boolean = IO.File.Exists(Pack) AndAlso New FileInfo(Pack).Length > 0 AndAlso _Converter.FetchFile(File) = Converter_CP.WP_Format.JSON
                Dim cache As String = My.PATH_ThemeResPackCache & "\" & String.Concat(Info.ThemeName.Replace(" ", "").Split(IO.Path.GetInvalidFileNameChars()))

                'Extract theme resources pack
                Try
                    If Pack_IsValid And Not IgnoreExtractionThemePack Then
                        If Not IO.Directory.Exists(cache) Then IO.Directory.CreateDirectory(cache)

                        Using s As New IO.FileStream(Pack, IO.FileMode.Open, IO.FileAccess.Read)
                            Using archive As New ZipArchive(s, ZipArchiveMode.Read)
                                For Each entry As ZipArchiveEntry In archive.Entries
                                    If entry.FullName.Contains("\") Then
                                        Dim dest As String = Path.Combine(cache, entry.FullName)
                                        Dim dest_dir As String = dest.Replace("\" & dest.Split("\").Last, "")
                                        If Not IO.Directory.Exists(dest_dir) Then IO.Directory.CreateDirectory(dest_dir)
                                    End If
                                    entry.ExtractToFile(Path.Combine(cache, entry.FullName), True)
                                Next
                            End Using

                            s.Close()
                            s.Dispose()
                        End Using

                    End If

                Catch ex As Exception
                    Pack_IsValid = False
                    BugReport.ThrowError(ex)
                End Try

                txt = Decompress(File)

                If IsValidJson(String.Join(vbCrLf, txt)) Then

                    'Replace %WinPaletterAppData% variable with a valid AppData folder path
                    For x = 0 To txt.Count - 1
                        If txt(x).Contains(":") Then
                            Dim arr As String() = txt(x).Split(":")
                            If arr.Count = 2 AndAlso arr(1).Contains("%WinPaletterAppData%") Then
                                txt(x) = arr(0) & ":" & arr(1).Replace("%WinPaletterAppData%", My.PATH_appData.Replace("\", "\\"))
                            End If
                        End If
                    Next

                    Dim J As JObject = JObject.Parse(String.Join(vbCrLf, txt))

                    For Each field As FieldInfo In Me.GetType.GetFields(bindingFlags)
                        Dim type As Type = field.FieldType
                        Dim JSet As New JsonSerializerSettings

                        If J(field.Name) IsNot Nothing Then
                            field.SetValue(Me, J(field.Name).ToObject(type))
                        End If
                    Next
                Else

                    If _Converter.FetchFile(File) = Converter_CP.WP_Format.WPTH Then
                        If MsgBox(My.Lang.Convert_Detect_Old_OnLoading0, MsgBoxStyle.Question + MsgBoxStyle.YesNo, My.Lang.Convert_Detect_Old_OnLoading1, "", "", "", "", My.Lang.Convert_Detect_Old_OnLoading2, Ookii.Dialogs.WinForms.TaskDialogIcon.Information) = MsgBoxResult.Yes Then
                            _Converter.Convert(File, File, My.Settings.FileTypeManagement.CompressThemeFile, False)
                            GoTo Start
                        End If
                    Else
                        MsgBox(My.Lang.Convert_Error_Phrasing, MsgBoxStyle.Critical)
                    End If

                End If
#End Region

        End Select
    End Sub

    Sub Save([SaveTo] As CP_Type, Optional File As String = "", Optional [TreeView] As Windows.Forms.TreeView = Nothing, Optional ResetToDefault As Boolean = False)

        Select Case [SaveTo]
            Case CP_Type.Registry

#Region "Registry"
                Dim ReportProgress As Boolean = My.Settings.ThemeLog.VerboseLevel <> WPSettings.Structures.ThemeLog.VerboseLevels.None AndAlso [TreeView] IsNot Nothing
                Dim ReportProgress_Detailed As Boolean = ReportProgress AndAlso My.Settings.ThemeLog.VerboseLevel = WPSettings.Structures.ThemeLog.VerboseLevels.Detailed

                _ErrorHappened = False

                Dim sw_all As New Stopwatch
                sw_all.Reset()
                sw_all.Start()


                If ReportProgress Then
                    My.Saving_Exceptions.Clear()
                    [TreeView].Visible = False
                    [TreeView].Nodes.Clear()
                    [TreeView].Visible = True
                    Dim OS As String
                    If My.W11 Then
                        OS = My.Lang.OS_Win11
                    ElseIf My.W10 Then
                        OS = My.Lang.OS_Win10
                    ElseIf My.W8 Then
                        OS = My.Lang.OS_Win8
                    ElseIf My.W81 Then
                        OS = My.Lang.OS_Win81
                    ElseIf My.W7 Then
                        OS = My.Lang.OS_Win7
                    ElseIf My.WVista Then
                        OS = My.Lang.OS_WinVista
                    ElseIf My.WXP Then
                        OS = My.Lang.OS_WinXP
                    Else
                        OS = My.Lang.OS_WinUndefined
                    End If

                    AddNode([TreeView], String.Format("{0}", String.Format(My.Lang.CP_ApplyFrom, OS)), "info")

                    AddNode([TreeView], String.Format("{0}: {1}", Now.ToLongTimeString, My.Lang.CP_Applying_Started), "info")

                    If Not My.isElevated Then
                        AddNode([TreeView], String.Format("{0}}", My.Lang.CP_Admin_Msg0), "admin")
                        AddNode([TreeView], String.Format("{0}", My.Lang.CP_Admin_Msg1), "admin")
                    End If

                End If

                'Reset to default Windows theme
                If ResetToDefault Then
                    Execute(Sub()
                                Using def As CP = CP_Defaults.GetDefault
                                    def.LogonUI10x.NoLockScreen = False
                                    def.LogonUI7.Enabled = False
                                    def.Windows81.NoLockScreen = False
                                    def.LogonUIXP.Enabled = True

                                    If Not My.WXP Then ResetCursorsToAero() Else ResetCursorsToNone_XP()

                                    def.CommandPrompt.Enabled = True
                                    def.PowerShellx86.Enabled = True
                                    def.PowerShellx64.Enabled = True

                                    def.MetricsFonts.Enabled = True
                                    def.WindowsEffects.Enabled = True
                                    def.AltTab.Enabled = True
                                    def.ScreenSaver.Enabled = True
                                    def.Sounds.Enabled = True
                                    def.AppTheme.Enabled = True

                                    def.Wallpaper.Enabled = False

                                    def.Save(CP_Type.Registry)
                                End Using

                            End Sub, [TreeView], My.Lang.CP_ThemeReset, My.Lang.CP_ThemeReset_Error, My.Lang.CP_Time, sw_all)
                End If

                'Theme info
                Execute(Sub()
                            Info.Apply(If(ReportProgress_Detailed, [TreeView], Nothing))
                        End Sub, [TreeView], My.Lang.CP_SavingInfo, My.Lang.CP_SavingInfo_Error, My.Lang.CP_Time, sw_all)

                'WinPaletter application theme
                Execute(Sub()
                            AppTheme.Apply(If(ReportProgress_Detailed, [TreeView], Nothing))
                        End Sub, [TreeView], My.Lang.CP_Applying_AppTheme, My.Lang.CP_Error_AppTheme, My.Lang.CP_Time, sw_all, Not AppTheme.Enabled, My.Lang.CP_Skip_AppTheme, True)

                'Wallpaper
                'Make Wallpaper before the following LogonUI items, to make a logonUI that depends on current wallpaper gets the correct file
                Execute(CType(Sub()
                                  Wallpaper.Apply(False, If(ReportProgress_Detailed, [TreeView], Nothing))
                              End Sub, MethodInvoker), [TreeView], My.Lang.CP_Applying_Wallpaper, My.Lang.CP_Error_Wallpaper, My.Lang.CP_Time, sw_all, Not Wallpaper.Enabled, My.Lang.CP_Skip_Wallpaper)

                If My.W11 Then
                    Execute(CType(Sub()
                                      Windows11.Apply(If(ReportProgress_Detailed, [TreeView], Nothing))
                                  End Sub, MethodInvoker), [TreeView], My.Lang.CP_Applying_Win11, My.Lang.CP_W11_Error, My.Lang.CP_Time, sw_all)

                    Execute(CType(Sub()
                                      LogonUI10x.Apply(If(ReportProgress_Detailed, [TreeView], Nothing))
                                  End Sub, MethodInvoker), [TreeView], My.Lang.CP_Applying_LogonUI11, My.Lang.CP_LogonUI11_Error, My.Lang.CP_Time, sw_all)
                End If

                If My.W10 Then
                    Execute(CType(Sub()
                                      Windows10.Apply(If(ReportProgress_Detailed, [TreeView], Nothing))
                                  End Sub, MethodInvoker), [TreeView], My.Lang.CP_Applying_Win10, My.Lang.CP_W10_Error, My.Lang.CP_Time, sw_all)

                    Execute(CType(Sub()
                                      LogonUI10x.Apply(If(ReportProgress_Detailed, [TreeView], Nothing))
                                  End Sub, MethodInvoker), [TreeView], My.Lang.CP_Applying_LogonUI10, My.Lang.CP_LogonUI10_Error, My.Lang.CP_Time, sw_all)
                End If

                If My.W8 Or My.W81 Then
                    Execute(CType(Sub()
                                      Windows81.Apply(If(ReportProgress_Detailed, [TreeView], Nothing))
                                      RefreshDWM(Me)
                                  End Sub, MethodInvoker), [TreeView], My.Lang.CP_Applying_Win81, My.Lang.CP_W81_Error, My.Lang.CP_Time, sw_all)

                    Execute(CType(Sub()

                                      Apply_LogonUI_8([TreeView])
                                  End Sub, MethodInvoker), [TreeView], My.Lang.CP_Applying_LogonUI8, My.Lang.CP_LogonUI8_Error, My.Lang.CP_Time, sw_all)
                End If

                If My.W7 Then
                    Execute(CType(Sub()
                                      Windows7.Apply(If(ReportProgress_Detailed, [TreeView], Nothing))
                                      RefreshDWM(Me)
                                  End Sub, MethodInvoker), [TreeView], My.Lang.CP_Applying_Win7, My.Lang.CP_W7_Error, My.Lang.CP_Time, sw_all)

                    Execute(CType(Sub()
                                      Apply_LogonUI7(LogonUI7, "LogonUI", [TreeView])
                                  End Sub, MethodInvoker), [TreeView], My.Lang.CP_Applying_LogonUI7, My.Lang.CP_LogonUI7_Error, My.Lang.CP_Time, sw_all)
                End If

                If My.WVista Then
                    Execute(CType(Sub()
                                      WindowsVista.Apply(If(ReportProgress_Detailed, [TreeView], Nothing))
                                      RefreshDWM(Me)
                                  End Sub, MethodInvoker), [TreeView], My.Lang.CP_Applying_WinVista, My.Lang.CP_WVista_Error, My.Lang.CP_Time, sw_all)
                End If

                If My.WXP Then
                    Execute(CType(Sub()
                                      WindowsXP.Apply(If(ReportProgress_Detailed, [TreeView], Nothing))
                                  End Sub, MethodInvoker), [TreeView], My.Lang.CP_Applying_WinXP, My.Lang.CP_WXP_Error, My.Lang.CP_Time, sw_all)

                    Execute(CType(Sub()
                                      LogonUIXP.Apply(If(ReportProgress_Detailed, [TreeView], Nothing))
                                  End Sub, MethodInvoker), [TreeView], My.Lang.CP_Applying_LogonUIXP, My.Lang.CP_LogonUIXP_Error, My.Lang.CP_Time, sw_all)
                End If

                'Win32UI
                Execute(CType(Sub()
                                  Win32.Apply(If(ReportProgress_Detailed, [TreeView], Nothing))
                              End Sub, MethodInvoker), [TreeView], My.Lang.CP_Applying_Win32UI, My.Lang.CP_WIN32UI_Error, My.Lang.CP_Time, sw_all)

                'WindowsEffects
                Execute(CType(Sub()
                                  WindowsEffects.Apply(If(ReportProgress_Detailed, [TreeView], Nothing))
                              End Sub, MethodInvoker), [TreeView], My.Lang.CP_Applying_WinEffects, My.Lang.CP_WinEffects_Error, My.Lang.CP_Time, sw_all)

                'Metrics\Fonts
                Execute(CType(Sub()
                                  MetricsFonts.Apply(If(ReportProgress_Detailed, [TreeView], Nothing))
                              End Sub, MethodInvoker), [TreeView], My.Lang.CP_Applying_Metrics, My.Lang.CP_Error_Metrics, My.Lang.CP_Time_They, sw_all, Not MetricsFonts.Enabled, My.Lang.CP_Skip_Metrics)

                'AltTab
                Execute(CType(Sub()
                                  AltTab.Apply(If(ReportProgress_Detailed, [TreeView], Nothing))
                              End Sub, MethodInvoker), [TreeView], My.Lang.CP_Applying_AltTab, My.Lang.CP_Error_AltTab, My.Lang.CP_Time, sw_all, Not AltTab.Enabled, My.Lang.CP_Skip_AltTab, True)

                'WallpaperTone
                Execute(CType(Sub()
                                  Structures.WallpaperTone.Save_To_Registry(WallpaperTone_W11, "Win11", If(ReportProgress_Detailed, [TreeView], Nothing))
                                  Structures.WallpaperTone.Save_To_Registry(WallpaperTone_W10, "Win10", If(ReportProgress_Detailed, [TreeView], Nothing))
                                  Structures.WallpaperTone.Save_To_Registry(WallpaperTone_W81, "Win8.1", If(ReportProgress_Detailed, [TreeView], Nothing))
                                  Structures.WallpaperTone.Save_To_Registry(WallpaperTone_W7, "Win7", If(ReportProgress_Detailed, [TreeView], Nothing))
                                  Structures.WallpaperTone.Save_To_Registry(WallpaperTone_WVista, "WinVista", If(ReportProgress_Detailed, [TreeView], Nothing))
                                  Structures.WallpaperTone.Save_To_Registry(WallpaperTone_WXP, "WinXP", If(ReportProgress_Detailed, [TreeView], Nothing))

                                  If Wallpaper.Enabled Then
                                      If My.W11 And WallpaperTone_W11.Enabled Then WallpaperTone_W11.Apply(If(ReportProgress_Detailed, [TreeView], Nothing))
                                      If My.W10 And WallpaperTone_W10.Enabled Then WallpaperTone_W10.Apply(If(ReportProgress_Detailed, [TreeView], Nothing))
                                      If My.W81 And WallpaperTone_W81.Enabled Then WallpaperTone_W81.Apply(If(ReportProgress_Detailed, [TreeView], Nothing))
                                      If My.W7 And WallpaperTone_W7.Enabled Then WallpaperTone_W7.Apply(If(ReportProgress_Detailed, [TreeView], Nothing))
                                      If My.WVista And WallpaperTone_WVista.Enabled Then WallpaperTone_WVista.Apply(If(ReportProgress_Detailed, [TreeView], Nothing))
                                      If My.WXP And WallpaperTone_WXP.Enabled Then WallpaperTone_WXP.Apply(If(ReportProgress_Detailed, [TreeView], Nothing))
                                  End If

                              End Sub, MethodInvoker), [TreeView], My.Lang.CP_Applying_WallpaperTone, My.Lang.CP_WallpaperTone_Error, My.Lang.CP_Time, sw_all)

#Region "Consoles"
                EditReg("HKEY_CURRENT_USER\Software\WinPaletter\Terminals", "Terminal_CMD_Enabled", CommandPrompt.Enabled)
                EditReg("HKEY_CURRENT_USER\Software\WinPaletter\Terminals", "Terminal_PS_32_Enabled", PowerShellx86.Enabled)
                EditReg("HKEY_CURRENT_USER\Software\WinPaletter\Terminals", "Terminal_PS_64_Enabled", PowerShellx64.Enabled)
                EditReg("HKEY_CURRENT_USER\Software\WinPaletter\Terminals", "Terminal_Stable_Enabled", Terminal.Enabled)
                EditReg("HKEY_CURRENT_USER\Software\WinPaletter\Terminals", "Terminal_Preview_Enabled", TerminalPreview.Enabled)

                Execute(CType(Sub()
                                  Apply_CommandPrompt(If(ReportProgress_Detailed, [TreeView], Nothing))
                              End Sub, MethodInvoker), [TreeView], My.Lang.CP_Applying_CMD, My.Lang.CP_CMD_Error, My.Lang.CP_Time, sw_all, Not CommandPrompt.Enabled, My.Lang.CP_Skip_CMD)

                Execute(CType(Sub()
                                  Apply_PowerShell86(If(ReportProgress_Detailed, [TreeView], Nothing))
                              End Sub, MethodInvoker), [TreeView], My.Lang.CP_Applying_PS32, My.Lang.CP_PS32_Error, My.Lang.CP_Time, sw_all, Not PowerShellx86.Enabled, My.Lang.CP_Skip_PS32)

                Execute(CType(Sub()
                                  Apply_PowerShell64(If(ReportProgress_Detailed, [TreeView], Nothing))
                              End Sub, MethodInvoker), [TreeView], My.Lang.CP_Applying_PS64, My.Lang.CP_PS64_Error, My.Lang.CP_Time, sw_all, Not PowerShellx64.Enabled, My.Lang.CP_Skip_PS64)
#End Region

#Region "Windows Terminal"
                Dim sw As New Stopwatch
                sw.Reset() : sw.Start()
                If My.W10 Or My.W11 Then

                    If ReportProgress Then
                        If Terminal.Enabled And TerminalPreview.Enabled Then
                            AddNode([TreeView], String.Format("{0}: {1}", Now.ToLongTimeString, My.Lang.CP_Check_Terminals), "info")

                        ElseIf Terminal.Enabled Then
                            AddNode([TreeView], String.Format("{0}: {1}", Now.ToLongTimeString, My.Lang.CP_Skip_TerminalPreview), "skip")
                            AddNode([TreeView], String.Format("{0}: {1}", Now.ToLongTimeString, My.Lang.CP_Check_TerminalStable), "info")

                        ElseIf TerminalPreview.Enabled Then
                            AddNode([TreeView], String.Format("{0}: {1}", Now.ToLongTimeString, My.Lang.CP_Skip_TerminalStable), "skip")
                            AddNode([TreeView], String.Format("{0}: {1}", Now.ToLongTimeString, My.Lang.CP_Check_TerminalPreview), "info")

                        Else
                            AddNode([TreeView], String.Format("{0}: {1}", Now.ToLongTimeString, My.Lang.CP_Skip_Terminals), "skip")

                        End If

                    End If

                    Dim TerDir As String
                    Dim TerPreDir As String

                    If Not My.Settings.WindowsTerminals.Path_Deflection Then
                        TerDir = My.PATH_TerminalJSON
                        TerPreDir = My.PATH_TerminalPreviewJSON
                    Else
                        If IO.File.Exists(My.Settings.WindowsTerminals.Terminal_Stable_Path) Then
                            TerDir = My.Settings.WindowsTerminals.Terminal_Stable_Path
                        Else
                            TerDir = My.PATH_TerminalJSON
                        End If

                        If IO.File.Exists(My.Settings.WindowsTerminals.Terminal_Preview_Path) Then
                            TerPreDir = My.Settings.WindowsTerminals.Terminal_Preview_Path
                        Else
                            TerPreDir = My.PATH_TerminalPreviewJSON
                        End If
                    End If

                    If Terminal.Enabled Then
                        If IO.File.Exists(TerDir) Then

                            Try
                                AddNode([TreeView], String.Format("{0}: {1}", Now.ToLongTimeString, My.Lang.CP_Applying_TerminalStable), "info")
                                Terminal.Save(TerDir, WinTerminal.Mode.JSONFile)
                                If ReportProgress Then AddNode([TreeView], String.Format(My.Lang.CP_Time, sw.ElapsedMilliseconds / 1000), "time")
                            Catch ex As Exception
                                sw.Stop() : sw_all.Stop()
                                _ErrorHappened = True
                                If ReportProgress Then
                                    AddNode([TreeView], String.Format("{0}: {1}", Now.ToLongTimeString, My.Lang.CP_Error_TerminalStable), "error")
                                    AddException(My.Lang.CP_Error_TerminalStable, ex)
                                Else
                                    BugReport.ThrowError(ex)
                                End If

                                sw.Start() : sw_all.Start()
                            End Try

                        Else

                            If Not My.Settings.WindowsTerminals.Path_Deflection Then
                                AddNode([TreeView], String.Format("{0}: {1}", Now.ToLongTimeString, My.Lang.CP_Skip_TerminalStable_NotInstalled), "skip")
                            Else
                                AddNode([TreeView], String.Format("{0}: {1}", Now.ToLongTimeString, My.Lang.CP_Skip_TerminalStable_DeflectionNotFound), "skip")
                            End If

                        End If
                    End If

                    If TerminalPreview.Enabled Then
                        If IO.File.Exists(TerPreDir) Then

                            Try
                                AddNode([TreeView], String.Format("{0}: {1}", Now.ToLongTimeString, My.Lang.CP_Applying_TerminalPreview), "info")
                                TerminalPreview.Save(TerPreDir, WinTerminal.Mode.JSONFile, WinTerminal.Version.Preview)
                                If ReportProgress Then AddNode([TreeView], String.Format(My.Lang.CP_Time, sw.ElapsedMilliseconds / 1000), "time")
                            Catch ex As Exception
                                sw.Stop() : sw_all.Stop()
                                _ErrorHappened = True
                                If ReportProgress Then
                                    AddNode([TreeView], String.Format("{0}: {1}", Now.ToLongTimeString, My.Lang.CP_Error_TerminalPreview), "error")
                                    AddException(My.Lang.CP_Error_TerminalPreview, ex)
                                Else
                                    BugReport.ThrowError(ex)
                                End If

                                sw.Start() : sw_all.Start()
                            End Try

                        Else
                            If Not My.Settings.WindowsTerminals.Path_Deflection Then
                                AddNode([TreeView], String.Format("{0}: {1}", Now.ToLongTimeString, My.Lang.CP_Skip_TerminalPreview_NotInstalled), "skip")
                            Else
                                AddNode([TreeView], String.Format("{0}: {1}", Now.ToLongTimeString, My.Lang.CP_Skip_TerminalPreview_DeflectionNotFound), "skip")
                            End If
                        End If
                    End If

                Else
                    AddNode([TreeView], String.Format("{0}: {1}", Now.ToLongTimeString, My.Lang.CP_Skip_Terminals_NotSupported), "skip")
                End If
                sw.Stop()
#End Region

                'ScreenSaver
                Execute(CType(Sub()
                                  ScreenSaver.Apply(If(ReportProgress_Detailed, [TreeView], Nothing))
                              End Sub, MethodInvoker), [TreeView], My.Lang.CP_Applying_ScreenSaver, My.Lang.CP_Error_ScreenSaver, My.Lang.CP_Time, sw_all)

                'Sounds
                Execute(CType(Sub()
                                  Sounds.Apply(If(ReportProgress_Detailed, [TreeView], Nothing))
                              End Sub, MethodInvoker), [TreeView], My.Lang.CP_Applying_Sounds, My.Lang.CP_Error_Sounds, My.Lang.CP_Time, sw_all, Not Sounds.Enabled, My.Lang.CP_Skip_Sounds)

                'Cursors
                Execute(CType(Sub()
                                  Apply_Cursors([TreeView])
                              End Sub, MethodInvoker), [TreeView], "", My.Lang.CP_Error_Cursors, My.Lang.CP_Time_Cursors, sw_all)

                'Update LogonUI wallpaper in HKEY_USERS\.DEFAULT
                If My.Settings.ThemeApplyingBehavior.Desktop_HKU_DEFAULT = WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite Then

                    Execute(CType(Sub()
                                      EditReg(If(ReportProgress_Detailed, [TreeView], Nothing), "HKEY_USERS\.DEFAULT\Control Panel\Desktop", "Wallpaper", GetReg("HKEY_CURRENT_USER\Control Panel\Desktop", "Wallpaper", ""), RegistryValueKind.String)
                                      EditReg(If(ReportProgress_Detailed, [TreeView], Nothing), "HKEY_USERS\.DEFAULT\Control Panel\Desktop", "WallpaperStyle", GetReg("HKEY_CURRENT_USER\Control Panel\Desktop", "WallpaperStyle", "2"), RegistryValueKind.String)
                                      EditReg(If(ReportProgress_Detailed, [TreeView], Nothing), "HKEY_USERS\.DEFAULT\Control Panel\Desktop", "TileWallpaper", GetReg("HKEY_CURRENT_USER\Control Panel\Desktop", "TileWallpaper", "0"), RegistryValueKind.String)
                                      EditReg(If(ReportProgress_Detailed, [TreeView], Nothing), "HKEY_USERS\.DEFAULT\Control Panel\Desktop", "Pattern", GetReg("HKEY_CURRENT_USER\Control Panel\Desktop", "Pattern", ""), RegistryValueKind.String)
                                  End Sub, MethodInvoker), [TreeView], My.Lang.CP_Applying_DesktopAllUsers, My.Lang.CP_Error_SetDesktop, My.Lang.CP_Time)

                ElseIf My.Settings.ThemeApplyingBehavior.Desktop_HKU_DEFAULT = WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.RestoreDefaults Then

                    Execute(CType(Sub()
                                      EditReg(If(ReportProgress_Detailed, [TreeView], Nothing), "HKEY_USERS\.DEFAULT\Control Panel\Desktop", "Wallpaper", "", RegistryValueKind.String)
                                      EditReg(If(ReportProgress_Detailed, [TreeView], Nothing), "HKEY_USERS\.DEFAULT\Control Panel\Desktop", "WallpaperStyle", "2", RegistryValueKind.String)
                                      EditReg(If(ReportProgress_Detailed, [TreeView], Nothing), "HKEY_USERS\.DEFAULT\Control Panel\Desktop", "TileWallpaper", "0", RegistryValueKind.String)
                                      EditReg(If(ReportProgress_Detailed, [TreeView], Nothing), "HKEY_USERS\.DEFAULT\Control Panel\Desktop", "Pattern", "", RegistryValueKind.String)
                                  End Sub, MethodInvoker), [TreeView], My.Lang.CP_Applying_DesktopAllUsers, My.Lang.CP_Error_SetDesktop, My.Lang.CP_Time)

                End If

                'Update User Preference Mask for HKEY_USERS\.DEFAULT
                'Always make it the last operation
                Try
                    Win32.Update_UPM_DEFAULT(If(ReportProgress_Detailed, [TreeView], Nothing))
                Catch
                End Try

                If ReportProgress_Detailed Then AddNode([TreeView], My.Lang.Verbose_BroadcastEffects, "dll")
                User32.SendMessageTimeout(User32.HWND_BROADCAST, User32.WM_SETTINGCHANGE, UIntPtr.Zero, Marshal.StringToHGlobalAnsi("Environment"), User32.SendMessageTimeoutFlags.SMTO_ABORTIFHUNG, User32.MSG_TIMEOUT, User32.RESULT)

                If ReportProgress Then
                    If Not _ErrorHappened Then
                        AddNode([TreeView], String.Format("{0}: {1}", Now.ToLongTimeString, String.Format(My.Lang.CP_Applied, sw_all.ElapsedMilliseconds / 1000)), "success")
                    Else
                        AddNode([TreeView], String.Format("{0}: {1}", Now.ToLongTimeString, String.Format(My.Lang.CP_AppliedWithErrors, sw_all.ElapsedMilliseconds / 1000)), "warning")
                    End If
                End If

                sw_all.Reset()
                sw_all.Stop()
#End Region

            Case CP_Type.File
                If IO.File.Exists(File) Then
                    Try : Kill(File) : Catch : End Try
                End If

                If Info.ExportResThemePack Then
                    PackThemeResources(Clone, File, New IO.FileInfo(File).DirectoryName & "\" & IO.Path.GetFileNameWithoutExtension(File) & ".wptp")
                Else
                    IO.File.WriteAllText(File, ToString)
                End If

        End Select
    End Sub

    Overrides Function ToString() As String
        Dim JSON_Overall As New JObject()
        JSON_Overall.RemoveAll()

        Info.AppVersion = My.AppVersion

        For Each field As FieldInfo In Me.GetType.GetFields(bindingFlags)
            Dim type As Type = field.FieldType

            If IsStructure(type) Then
                JSON_Overall.Add(field.Name, DeserializeProps(type, field.GetValue(Me)))
            Else
                JSON_Overall.Add(field.Name, JToken.FromObject(field.GetValue(Me)))
            End If

        Next

        If My.Settings.FileTypeManagement.CompressThemeFile Then
            Return JSON_Overall.ToString.Compress
        Else
            Return JSON_Overall.ToString
        End If
    End Function

    Public Function IsStructure(ByVal type As Type) As Boolean
        Return type.IsValueType AndAlso Not type.IsPrimitive AndAlso type.Namespace IsNot Nothing AndAlso Not type.Namespace.StartsWith("System.")
    End Function

    Sub PackThemeResources(CP As CP, CP_File As String, Package As String)
        Dim cache As String = "%WinPaletterAppData%\ThemeResPack_Cache\" & String.Concat(CP.Info.ThemeName.Replace(" ", "").Split(IO.Path.GetInvalidFileNameChars())) & "\"
        Dim filesList As New Dictionary(Of String, String) : filesList.Clear()
        Dim x As String
        Dim ZipEntry As String

        If IO.File.Exists(Package) Then IO.File.Delete(Package)
        Using archive As ZipArchive = ZipFile.Open(Package, ZipArchiveMode.Create)
            If (CP.LogonUI7.Enabled AndAlso CP.LogonUI7.Mode = Structures.LogonUI7.Modes.CustomImage) OrElse
                (Not CP.Windows81.NoLockScreen AndAlso CP.Windows81.LockScreenType = Structures.LogonUI7.Modes.CustomImage) Then
                x = CP.LogonUI7.ImagePath
                If Not String.IsNullOrWhiteSpace(x) AndAlso Not x.StartsWith(My.PATH_Windows & "\Web", My._ignore) Then
                    ZipEntry = cache & "LogonUI" & IO.Path.GetExtension(x)
                    If IO.File.Exists(x) Then CP.LogonUI7.ImagePath = ZipEntry
                    filesList.Add(ZipEntry, x)
                End If
            End If

            If CP.Terminal.Enabled Then
                x = CP.Terminal.DefaultProf.BackgroundImage
                If Not String.IsNullOrWhiteSpace(x) AndAlso Not x.StartsWith(My.PATH_Windows & "\Web", My._ignore) Then
                    ZipEntry = cache & "winterminal_defprofile_backimg" & IO.Path.GetExtension(x)
                    If IO.File.Exists(x) Then CP.Terminal.DefaultProf.BackgroundImage = ZipEntry
                    filesList.Add(ZipEntry, x)
                End If

                x = CP.Terminal.DefaultProf.Icon
                If Not String.IsNullOrWhiteSpace(x) AndAlso Not x.Length <= 1 AndAlso Not x.StartsWith(My.PATH_Windows & "\Web", My._ignore) Then
                    ZipEntry = cache & "winterminal_defprofile_icon" & IO.Path.GetExtension(x)
                    If IO.File.Exists(x) Then CP.Terminal.DefaultProf.Icon = ZipEntry
                    filesList.Add(ZipEntry, x)
                End If

                For Each i In CP.Terminal.Profiles
                    x = i.BackgroundImage
                    If Not String.IsNullOrWhiteSpace(x) AndAlso Not x.StartsWith(My.PATH_Windows & "\Web", My._ignore) Then
                        ZipEntry = cache & "winterminal_profile(" & String.Concat(i.Name.Replace(" ", "").Split(IO.Path.GetInvalidFileNameChars())) & ")_backimg" & IO.Path.GetExtension(x)
                        If IO.File.Exists(x) Then i.BackgroundImage = ZipEntry
                        filesList.Add(ZipEntry, x)
                    End If

                    x = i.Icon
                    If Not String.IsNullOrWhiteSpace(x) AndAlso Not x.Length <= 1 AndAlso Not x.StartsWith(My.PATH_Windows & "\Web", My._ignore) Then
                        ZipEntry = cache & "winterminal_profile(" & String.Concat(i.Name.Replace(" ", "").Split(IO.Path.GetInvalidFileNameChars())) & ")_icon" & IO.Path.GetExtension(x)
                        If IO.File.Exists(x) Then i.Icon = ZipEntry
                        filesList.Add(ZipEntry, x)
                    End If
                Next
            End If

            If CP.TerminalPreview.Enabled Then
                x = CP.TerminalPreview.DefaultProf.BackgroundImage
                If Not String.IsNullOrWhiteSpace(x) AndAlso Not x.StartsWith(My.PATH_Windows & "\Web", My._ignore) Then
                    ZipEntry = cache & "winterminal_preview_defprofile_backimg" & IO.Path.GetExtension(x)
                    If IO.File.Exists(x) Then CP.TerminalPreview.DefaultProf.BackgroundImage = ZipEntry
                    filesList.Add(ZipEntry, x)
                End If

                x = CP.TerminalPreview.DefaultProf.Icon
                If Not String.IsNullOrWhiteSpace(x) AndAlso Not x.Length <= 1 AndAlso Not x.StartsWith(My.PATH_Windows & "\Web", My._ignore) Then
                    ZipEntry = cache & "winterminal_preview_defprofile_icon" & IO.Path.GetExtension(x)
                    If IO.File.Exists(x) Then CP.TerminalPreview.DefaultProf.Icon = ZipEntry
                    filesList.Add(ZipEntry, x)
                End If

                For Each i In CP.TerminalPreview.Profiles
                    x = i.BackgroundImage
                    If Not String.IsNullOrWhiteSpace(x) AndAlso Not x.StartsWith(My.PATH_Windows & "\Web", My._ignore) Then
                        ZipEntry = cache & "winterminal_preview_profile(" & String.Concat(i.Name.Replace(" ", "").Split(IO.Path.GetInvalidFileNameChars())) & ")_backimg" & IO.Path.GetExtension(x)
                        If IO.File.Exists(x) Then i.BackgroundImage = ZipEntry
                        filesList.Add(ZipEntry, x)
                    End If

                    x = i.Icon
                    If Not String.IsNullOrWhiteSpace(x) AndAlso Not x.Length <= 1 AndAlso Not x.StartsWith(My.PATH_Windows & "\Web", My._ignore) Then
                        ZipEntry = cache & "winterminal_preview_profile(" & String.Concat(i.Name.Replace(" ", "").Split(IO.Path.GetInvalidFileNameChars())) & ")_icon" & IO.Path.GetExtension(x)
                        If IO.File.Exists(x) Then i.Icon = ZipEntry
                        filesList.Add(ZipEntry, x)
                    End If
                Next
            End If

            If CP.WallpaperTone_W11.Enabled Then
                x = CP.WallpaperTone_W11.Image
                If Not String.IsNullOrWhiteSpace(x) AndAlso Not x.StartsWith(My.PATH_Windows & "\Web", My._ignore) Then
                    ZipEntry = cache & "wt_w11" & IO.Path.GetExtension(x)
                    If IO.File.Exists(x) Then CP.WallpaperTone_W11.Image = ZipEntry
                    filesList.Add(ZipEntry, x)
                End If
            End If

            If CP.WallpaperTone_W10.Enabled Then
                x = CP.WallpaperTone_W10.Image
                If Not String.IsNullOrWhiteSpace(x) AndAlso Not x.StartsWith(My.PATH_Windows & "\Web", My._ignore) Then
                    ZipEntry = cache & "wt_w10" & IO.Path.GetExtension(x)
                    If IO.File.Exists(x) Then CP.WallpaperTone_W10.Image = ZipEntry
                    filesList.Add(ZipEntry, x)
                End If
            End If

            If CP.WallpaperTone_W81.Enabled Then
                x = CP.WallpaperTone_W81.Image
                If Not String.IsNullOrWhiteSpace(x) AndAlso Not x.StartsWith(My.PATH_Windows & "\Web", My._ignore) Then
                    ZipEntry = cache & "wt_w81" & IO.Path.GetExtension(x)
                    If IO.File.Exists(x) Then CP.WallpaperTone_W81.Image = ZipEntry
                    filesList.Add(ZipEntry, x)
                End If
            End If

            If CP.WallpaperTone_W7.Enabled Then
                x = CP.WallpaperTone_W7.Image
                If Not String.IsNullOrWhiteSpace(x) AndAlso Not x.StartsWith(My.PATH_Windows & "\Web", My._ignore) Then
                    ZipEntry = cache & "wt_w7" & IO.Path.GetExtension(x)
                    If IO.File.Exists(x) Then CP.WallpaperTone_W7.Image = ZipEntry
                    filesList.Add(ZipEntry, x)
                End If
            End If

            If CP.WallpaperTone_WVista.Enabled Then
                x = CP.WallpaperTone_WVista.Image
                If Not String.IsNullOrWhiteSpace(x) AndAlso Not x.StartsWith(My.PATH_Windows & "\Web", My._ignore) Then
                    ZipEntry = cache & "wt_wvista" & IO.Path.GetExtension(x)
                    If IO.File.Exists(x) Then CP.WallpaperTone_WVista.Image = ZipEntry
                    filesList.Add(ZipEntry, x)
                End If
            End If

            If CP.WallpaperTone_WXP.Enabled Then
                x = CP.WallpaperTone_WXP.Image
                If Not String.IsNullOrWhiteSpace(x) AndAlso Not x.StartsWith(My.PATH_Windows & "\Web", My._ignore) Then
                    ZipEntry = cache & "wt_wxp" & IO.Path.GetExtension(x)
                    If IO.File.Exists(x) Then CP.WallpaperTone_WXP.Image = ZipEntry
                    filesList.Add(ZipEntry, x)
                End If
            End If

            If CP.ScreenSaver.Enabled Then
                x = CP.ScreenSaver.File
                If Not String.IsNullOrWhiteSpace(x) Then
                    ZipEntry = cache & "scrsvr" & IO.Path.GetExtension(x)
                    If IO.File.Exists(x) Then CP.ScreenSaver.File = ZipEntry
                    filesList.Add(ZipEntry, x)
                End If
            End If

#Region "Sounds"
            If CP.Sounds.Enabled Then
                x = CP.Sounds.Snd_Win_Default
                If Not String.IsNullOrWhiteSpace(x) AndAlso Not x.StartsWith(My.PATH_Windows & "\media", My._ignore) Then
                    ZipEntry = cache & "Snd_Win_Default" & IO.Path.GetExtension(x)
                    If IO.File.Exists(x) Then CP.Sounds.Snd_Win_Default = ZipEntry
                    filesList.Add(ZipEntry, x)
                End If

                x = CP.Sounds.Snd_Win_AppGPFault
                If Not String.IsNullOrWhiteSpace(x) AndAlso Not x.StartsWith(My.PATH_Windows & "\media", My._ignore) Then
                    ZipEntry = cache & "Snd_Win_AppGPFault" & IO.Path.GetExtension(x)
                    If IO.File.Exists(x) Then CP.Sounds.Snd_Win_AppGPFault = ZipEntry
                    filesList.Add(ZipEntry, x)
                End If

                x = CP.Sounds.Snd_Win_CCSelect
                If Not String.IsNullOrWhiteSpace(x) AndAlso Not x.StartsWith(My.PATH_Windows & "\media", My._ignore) Then
                    ZipEntry = cache & "Snd_Win_CCSelect" & IO.Path.GetExtension(x)
                    If IO.File.Exists(x) Then CP.Sounds.Snd_Win_CCSelect = ZipEntry
                    filesList.Add(ZipEntry, x)
                End If

                x = CP.Sounds.Snd_Win_ChangeTheme
                If Not String.IsNullOrWhiteSpace(x) AndAlso Not x.StartsWith(My.PATH_Windows & "\media", My._ignore) Then
                    ZipEntry = cache & "Snd_Win_ChangeTheme" & IO.Path.GetExtension(x)
                    If IO.File.Exists(x) Then CP.Sounds.Snd_Win_ChangeTheme = ZipEntry
                    filesList.Add(ZipEntry, x)
                End If

                x = CP.Sounds.Snd_Win_Close
                If Not String.IsNullOrWhiteSpace(x) AndAlso Not x.StartsWith(My.PATH_Windows & "\media", My._ignore) Then
                    ZipEntry = cache & "Snd_Win_Close" & IO.Path.GetExtension(x)
                    If IO.File.Exists(x) Then CP.Sounds.Snd_Win_Close = ZipEntry
                    filesList.Add(ZipEntry, x)
                End If

                x = CP.Sounds.Snd_Win_CriticalBatteryAlarm
                If Not String.IsNullOrWhiteSpace(x) AndAlso Not x.StartsWith(My.PATH_Windows & "\media", My._ignore) Then
                    ZipEntry = cache & "Snd_Win_CriticalBatteryAlarm" & IO.Path.GetExtension(x)
                    If IO.File.Exists(x) Then CP.Sounds.Snd_Win_CriticalBatteryAlarm = ZipEntry
                    filesList.Add(ZipEntry, x)
                End If

                x = CP.Sounds.Snd_Win_DeviceConnect
                If Not String.IsNullOrWhiteSpace(x) AndAlso Not x.StartsWith(My.PATH_Windows & "\media", My._ignore) Then
                    ZipEntry = cache & "Snd_Win_DeviceConnect" & IO.Path.GetExtension(x)
                    If IO.File.Exists(x) Then CP.Sounds.Snd_Win_DeviceConnect = ZipEntry
                    filesList.Add(ZipEntry, x)
                End If

                x = CP.Sounds.Snd_Win_DeviceDisconnect
                If Not String.IsNullOrWhiteSpace(x) AndAlso Not x.StartsWith(My.PATH_Windows & "\media", My._ignore) Then
                    ZipEntry = cache & "Snd_Win_DeviceDisconnect" & IO.Path.GetExtension(x)
                    If IO.File.Exists(x) Then CP.Sounds.Snd_Win_DeviceDisconnect = ZipEntry
                    filesList.Add(ZipEntry, x)
                End If

                x = CP.Sounds.Snd_Win_DeviceFail
                If Not String.IsNullOrWhiteSpace(x) AndAlso Not x.StartsWith(My.PATH_Windows & "\media", My._ignore) Then
                    ZipEntry = cache & "Snd_Win_DeviceFail" & IO.Path.GetExtension(x)
                    If IO.File.Exists(x) Then CP.Sounds.Snd_Win_DeviceFail = ZipEntry
                    filesList.Add(ZipEntry, x)
                End If

                x = CP.Sounds.Snd_Win_FaxBeep
                If Not String.IsNullOrWhiteSpace(x) AndAlso Not x.StartsWith(My.PATH_Windows & "\media", My._ignore) Then
                    ZipEntry = cache & "Snd_Win_FaxBeep" & IO.Path.GetExtension(x)
                    If IO.File.Exists(x) Then CP.Sounds.Snd_Win_FaxBeep = ZipEntry
                    filesList.Add(ZipEntry, x)
                End If

                x = CP.Sounds.Snd_Win_LowBatteryAlarm
                If Not String.IsNullOrWhiteSpace(x) AndAlso Not x.StartsWith(My.PATH_Windows & "\media", My._ignore) Then
                    ZipEntry = cache & "Snd_Win_LowBatteryAlarm" & IO.Path.GetExtension(x)
                    If IO.File.Exists(x) Then CP.Sounds.Snd_Win_LowBatteryAlarm = ZipEntry
                    filesList.Add(ZipEntry, x)
                End If

                x = CP.Sounds.Snd_Win_MailBeep
                If Not String.IsNullOrWhiteSpace(x) AndAlso Not x.StartsWith(My.PATH_Windows & "\media", My._ignore) Then
                    ZipEntry = cache & "Snd_Win_MailBeep" & IO.Path.GetExtension(x)
                    If IO.File.Exists(x) Then CP.Sounds.Snd_Win_MailBeep = ZipEntry
                    filesList.Add(ZipEntry, x)
                End If

                x = CP.Sounds.Snd_Win_Maximize
                If Not String.IsNullOrWhiteSpace(x) AndAlso Not x.StartsWith(My.PATH_Windows & "\media", My._ignore) Then
                    ZipEntry = cache & "Snd_Win_Maximize" & IO.Path.GetExtension(x)
                    If IO.File.Exists(x) Then CP.Sounds.Snd_Win_Maximize = ZipEntry
                    filesList.Add(ZipEntry, x)
                End If

                x = CP.Sounds.Snd_Win_MenuCommand
                If Not String.IsNullOrWhiteSpace(x) AndAlso Not x.StartsWith(My.PATH_Windows & "\media", My._ignore) Then
                    ZipEntry = cache & "Snd_Win_MenuCommand" & IO.Path.GetExtension(x)
                    If IO.File.Exists(x) Then CP.Sounds.Snd_Win_MenuCommand = ZipEntry
                    filesList.Add(ZipEntry, x)
                End If

                x = CP.Sounds.Snd_Win_MenuPopup
                If Not String.IsNullOrWhiteSpace(x) AndAlso Not x.StartsWith(My.PATH_Windows & "\media", My._ignore) Then
                    ZipEntry = cache & "Snd_Win_MenuPopup" & IO.Path.GetExtension(x)
                    If IO.File.Exists(x) Then CP.Sounds.Snd_Win_MenuPopup = ZipEntry
                    filesList.Add(ZipEntry, x)
                End If

                x = CP.Sounds.Snd_Win_MessageNudge
                If Not String.IsNullOrWhiteSpace(x) AndAlso Not x.StartsWith(My.PATH_Windows & "\media", My._ignore) Then
                    ZipEntry = cache & "Snd_Win_MessageNudge" & IO.Path.GetExtension(x)
                    If IO.File.Exists(x) Then CP.Sounds.Snd_Win_MessageNudge = ZipEntry
                    filesList.Add(ZipEntry, x)
                End If

                x = CP.Sounds.Snd_Win_Minimize
                If Not String.IsNullOrWhiteSpace(x) AndAlso Not x.StartsWith(My.PATH_Windows & "\media", My._ignore) Then
                    ZipEntry = cache & "Snd_Win_Minimize" & IO.Path.GetExtension(x)
                    If IO.File.Exists(x) Then CP.Sounds.Snd_Win_Minimize = ZipEntry
                    filesList.Add(ZipEntry, x)
                End If

                x = CP.Sounds.Snd_Win_Notification_Default
                If Not String.IsNullOrWhiteSpace(x) AndAlso Not x.StartsWith(My.PATH_Windows & "\media", My._ignore) Then
                    ZipEntry = cache & "Snd_Win_Notification_Default" & IO.Path.GetExtension(x)
                    If IO.File.Exists(x) Then CP.Sounds.Snd_Win_Notification_Default = ZipEntry
                    filesList.Add(ZipEntry, x)
                End If

                x = CP.Sounds.Snd_Win_Notification_IM
                If Not String.IsNullOrWhiteSpace(x) AndAlso Not x.StartsWith(My.PATH_Windows & "\media", My._ignore) Then
                    ZipEntry = cache & "Snd_Win_Notification_IM" & IO.Path.GetExtension(x)
                    If IO.File.Exists(x) Then CP.Sounds.Snd_Win_Notification_IM = ZipEntry
                    filesList.Add(ZipEntry, x)
                End If

                x = CP.Sounds.Snd_Win_Notification_Looping_Alarm
                If Not String.IsNullOrWhiteSpace(x) AndAlso Not x.StartsWith(My.PATH_Windows & "\media", My._ignore) Then
                    ZipEntry = cache & "Snd_Win_Notification_Looping_Alarm" & IO.Path.GetExtension(x)
                    If IO.File.Exists(x) Then CP.Sounds.Snd_Win_Notification_Looping_Alarm = ZipEntry
                    filesList.Add(ZipEntry, x)
                End If

                x = CP.Sounds.Snd_Win_Notification_Looping_Alarm10
                If Not String.IsNullOrWhiteSpace(x) AndAlso Not x.StartsWith(My.PATH_Windows & "\media", My._ignore) Then
                    ZipEntry = cache & "Snd_Win_Notification_Looping_Alarm10" & IO.Path.GetExtension(x)
                    If IO.File.Exists(x) Then CP.Sounds.Snd_Win_Notification_Looping_Alarm10 = ZipEntry
                    filesList.Add(ZipEntry, x)
                End If

                x = CP.Sounds.Snd_Win_Notification_Looping_Alarm2
                If Not String.IsNullOrWhiteSpace(x) AndAlso Not x.StartsWith(My.PATH_Windows & "\media", My._ignore) Then
                    ZipEntry = cache & "Snd_Win_Notification_Looping_Alarm2" & IO.Path.GetExtension(x)
                    If IO.File.Exists(x) Then CP.Sounds.Snd_Win_Notification_Looping_Alarm2 = ZipEntry
                    filesList.Add(ZipEntry, x)
                End If

                x = CP.Sounds.Snd_Win_Notification_Looping_Alarm3
                If Not String.IsNullOrWhiteSpace(x) AndAlso Not x.StartsWith(My.PATH_Windows & "\media", My._ignore) Then
                    ZipEntry = cache & "Snd_Win_Notification_Looping_Alarm3" & IO.Path.GetExtension(x)
                    If IO.File.Exists(x) Then CP.Sounds.Snd_Win_Notification_Looping_Alarm3 = ZipEntry
                    filesList.Add(ZipEntry, x)
                End If

                x = CP.Sounds.Snd_Win_Notification_Looping_Alarm4
                If Not String.IsNullOrWhiteSpace(x) AndAlso Not x.StartsWith(My.PATH_Windows & "\media", My._ignore) Then
                    ZipEntry = cache & "Snd_Win_Notification_Looping_Alarm4" & IO.Path.GetExtension(x)
                    If IO.File.Exists(x) Then CP.Sounds.Snd_Win_Notification_Looping_Alarm4 = ZipEntry
                    filesList.Add(ZipEntry, x)
                End If

                x = CP.Sounds.Snd_Win_Notification_Looping_Alarm5
                If Not String.IsNullOrWhiteSpace(x) AndAlso Not x.StartsWith(My.PATH_Windows & "\media", My._ignore) Then
                    ZipEntry = cache & "Snd_Win_Notification_Looping_Alarm5" & IO.Path.GetExtension(x)
                    If IO.File.Exists(x) Then CP.Sounds.Snd_Win_Notification_Looping_Alarm5 = ZipEntry
                    filesList.Add(ZipEntry, x)
                End If

                x = CP.Sounds.Snd_Win_Notification_Looping_Alarm6
                If Not String.IsNullOrWhiteSpace(x) AndAlso Not x.StartsWith(My.PATH_Windows & "\media", My._ignore) Then
                    ZipEntry = cache & "Snd_Win_Notification_Looping_Alarm6" & IO.Path.GetExtension(x)
                    If IO.File.Exists(x) Then CP.Sounds.Snd_Win_Notification_Looping_Alarm6 = ZipEntry
                    filesList.Add(ZipEntry, x)
                End If

                x = CP.Sounds.Snd_Win_Notification_Looping_Alarm7
                If Not String.IsNullOrWhiteSpace(x) AndAlso Not x.StartsWith(My.PATH_Windows & "\media", My._ignore) Then
                    ZipEntry = cache & "Snd_Win_Notification_Looping_Alarm7" & IO.Path.GetExtension(x)
                    If IO.File.Exists(x) Then CP.Sounds.Snd_Win_Notification_Looping_Alarm7 = ZipEntry
                    filesList.Add(ZipEntry, x)
                End If

                x = CP.Sounds.Snd_Win_Notification_Looping_Alarm8
                If Not String.IsNullOrWhiteSpace(x) AndAlso Not x.StartsWith(My.PATH_Windows & "\media", My._ignore) Then
                    ZipEntry = cache & "Snd_Win_Notification_Looping_Alarm8" & IO.Path.GetExtension(x)
                    If IO.File.Exists(x) Then CP.Sounds.Snd_Win_Notification_Looping_Alarm8 = ZipEntry
                    filesList.Add(ZipEntry, x)
                End If

                x = CP.Sounds.Snd_Win_Notification_Looping_Alarm9
                If Not String.IsNullOrWhiteSpace(x) AndAlso Not x.StartsWith(My.PATH_Windows & "\media", My._ignore) Then
                    ZipEntry = cache & "Snd_Win_Notification_Looping_Alarm9" & IO.Path.GetExtension(x)
                    If IO.File.Exists(x) Then CP.Sounds.Snd_Win_Notification_Looping_Alarm9 = ZipEntry
                    filesList.Add(ZipEntry, x)
                End If

                x = CP.Sounds.Snd_Win_Notification_Looping_Call
                If Not String.IsNullOrWhiteSpace(x) AndAlso Not x.StartsWith(My.PATH_Windows & "\media", My._ignore) Then
                    ZipEntry = cache & "Snd_Win_Notification_Looping_Call" & IO.Path.GetExtension(x)
                    If IO.File.Exists(x) Then CP.Sounds.Snd_Win_Notification_Looping_Call = ZipEntry
                    filesList.Add(ZipEntry, x)
                End If

                x = CP.Sounds.Snd_Win_Notification_Looping_Call10
                If Not String.IsNullOrWhiteSpace(x) AndAlso Not x.StartsWith(My.PATH_Windows & "\media", My._ignore) Then
                    ZipEntry = cache & "Snd_Win_Notification_Looping_Call10" & IO.Path.GetExtension(x)
                    If IO.File.Exists(x) Then CP.Sounds.Snd_Win_Notification_Looping_Call10 = ZipEntry
                    filesList.Add(ZipEntry, x)
                End If

                x = CP.Sounds.Snd_Win_Notification_Looping_Call2
                If Not String.IsNullOrWhiteSpace(x) AndAlso Not x.StartsWith(My.PATH_Windows & "\media", My._ignore) Then
                    ZipEntry = cache & "Snd_Win_Notification_Looping_Call2" & IO.Path.GetExtension(x)
                    If IO.File.Exists(x) Then CP.Sounds.Snd_Win_Notification_Looping_Call2 = ZipEntry
                    filesList.Add(ZipEntry, x)
                End If

                x = CP.Sounds.Snd_Win_Notification_Looping_Call3
                If Not String.IsNullOrWhiteSpace(x) AndAlso Not x.StartsWith(My.PATH_Windows & "\media", My._ignore) Then
                    ZipEntry = cache & "Snd_Win_Notification_Looping_Call3" & IO.Path.GetExtension(x)
                    If IO.File.Exists(x) Then CP.Sounds.Snd_Win_Notification_Looping_Call3 = ZipEntry
                    filesList.Add(ZipEntry, x)
                End If

                x = CP.Sounds.Snd_Win_Notification_Looping_Call4
                If Not String.IsNullOrWhiteSpace(x) AndAlso Not x.StartsWith(My.PATH_Windows & "\media", My._ignore) Then
                    ZipEntry = cache & "Snd_Win_Notification_Looping_Call4" & IO.Path.GetExtension(x)
                    If IO.File.Exists(x) Then CP.Sounds.Snd_Win_Notification_Looping_Call4 = ZipEntry
                    filesList.Add(ZipEntry, x)
                End If

                x = CP.Sounds.Snd_Win_Notification_Looping_Call5
                If Not String.IsNullOrWhiteSpace(x) AndAlso Not x.StartsWith(My.PATH_Windows & "\media", My._ignore) Then
                    ZipEntry = cache & "Snd_Win_Notification_Looping_Call5" & IO.Path.GetExtension(x)
                    If IO.File.Exists(x) Then CP.Sounds.Snd_Win_Notification_Looping_Call5 = ZipEntry
                    filesList.Add(ZipEntry, x)
                End If

                x = CP.Sounds.Snd_Win_Notification_Looping_Call6
                If Not String.IsNullOrWhiteSpace(x) AndAlso Not x.StartsWith(My.PATH_Windows & "\media", My._ignore) Then
                    ZipEntry = cache & "Snd_Win_Notification_Looping_Call6" & IO.Path.GetExtension(x)
                    If IO.File.Exists(x) Then CP.Sounds.Snd_Win_Notification_Looping_Call6 = ZipEntry
                    filesList.Add(ZipEntry, x)
                End If

                x = CP.Sounds.Snd_Win_Notification_Looping_Call7
                If Not String.IsNullOrWhiteSpace(x) AndAlso Not x.StartsWith(My.PATH_Windows & "\media", My._ignore) Then
                    ZipEntry = cache & "Snd_Win_Notification_Looping_Call7" & IO.Path.GetExtension(x)
                    If IO.File.Exists(x) Then CP.Sounds.Snd_Win_Notification_Looping_Call7 = ZipEntry
                    filesList.Add(ZipEntry, x)
                End If

                x = CP.Sounds.Snd_Win_Notification_Looping_Call8
                If Not String.IsNullOrWhiteSpace(x) AndAlso Not x.StartsWith(My.PATH_Windows & "\media", My._ignore) Then
                    ZipEntry = cache & "Snd_Win_Notification_Looping_Call8" & IO.Path.GetExtension(x)
                    If IO.File.Exists(x) Then CP.Sounds.Snd_Win_Notification_Looping_Call8 = ZipEntry
                    filesList.Add(ZipEntry, x)
                End If

                x = CP.Sounds.Snd_Win_Notification_Looping_Call9
                If Not String.IsNullOrWhiteSpace(x) AndAlso Not x.StartsWith(My.PATH_Windows & "\media", My._ignore) Then
                    ZipEntry = cache & "Snd_Win_Notification_Looping_Call9" & IO.Path.GetExtension(x)
                    If IO.File.Exists(x) Then CP.Sounds.Snd_Win_Notification_Looping_Call9 = ZipEntry
                    filesList.Add(ZipEntry, x)
                End If

                x = CP.Sounds.Snd_Win_Notification_Mail
                If Not String.IsNullOrWhiteSpace(x) AndAlso Not x.StartsWith(My.PATH_Windows & "\media", My._ignore) Then
                    ZipEntry = cache & "Snd_Win_Notification_Mail" & IO.Path.GetExtension(x)
                    If IO.File.Exists(x) Then CP.Sounds.Snd_Win_Notification_Mail = ZipEntry
                    filesList.Add(ZipEntry, x)
                End If

                x = CP.Sounds.Snd_Win_Notification_Proximity
                If Not String.IsNullOrWhiteSpace(x) AndAlso Not x.StartsWith(My.PATH_Windows & "\media", My._ignore) Then
                    ZipEntry = cache & "Snd_Win_Notification_Proximity" & IO.Path.GetExtension(x)
                    If IO.File.Exists(x) Then CP.Sounds.Snd_Win_Notification_Proximity = ZipEntry
                    filesList.Add(ZipEntry, x)
                End If

                x = CP.Sounds.Snd_Win_Notification_Reminder
                If Not String.IsNullOrWhiteSpace(x) AndAlso Not x.StartsWith(My.PATH_Windows & "\media", My._ignore) Then
                    ZipEntry = cache & "Snd_Win_Notification_Reminder" & IO.Path.GetExtension(x)
                    If IO.File.Exists(x) Then CP.Sounds.Snd_Win_Notification_Reminder = ZipEntry
                    filesList.Add(ZipEntry, x)
                End If

                x = CP.Sounds.Snd_Win_Notification_SMS
                If Not String.IsNullOrWhiteSpace(x) AndAlso Not x.StartsWith(My.PATH_Windows & "\media", My._ignore) Then
                    ZipEntry = cache & "Snd_Win_Notification_SMS" & IO.Path.GetExtension(x)
                    If IO.File.Exists(x) Then CP.Sounds.Snd_Win_Notification_SMS = ZipEntry
                    filesList.Add(ZipEntry, x)
                End If

                x = CP.Sounds.Snd_Win_Open
                If Not String.IsNullOrWhiteSpace(x) AndAlso Not x.StartsWith(My.PATH_Windows & "\media", My._ignore) Then
                    ZipEntry = cache & "Snd_Win_Open" & IO.Path.GetExtension(x)
                    If IO.File.Exists(x) Then CP.Sounds.Snd_Win_Open = ZipEntry
                    filesList.Add(ZipEntry, x)
                End If

                x = CP.Sounds.Snd_Win_PrintComplete
                If Not String.IsNullOrWhiteSpace(x) AndAlso Not x.StartsWith(My.PATH_Windows & "\media", My._ignore) Then
                    ZipEntry = cache & "Snd_Win_PrintComplete" & IO.Path.GetExtension(x)
                    If IO.File.Exists(x) Then CP.Sounds.Snd_Win_PrintComplete = ZipEntry
                    filesList.Add(ZipEntry, x)
                End If

                x = CP.Sounds.Snd_Win_ProximityConnection
                If Not String.IsNullOrWhiteSpace(x) AndAlso Not x.StartsWith(My.PATH_Windows & "\media", My._ignore) Then
                    ZipEntry = cache & "Snd_Win_ProximityConnection" & IO.Path.GetExtension(x)
                    If IO.File.Exists(x) Then CP.Sounds.Snd_Win_ProximityConnection = ZipEntry
                    filesList.Add(ZipEntry, x)
                End If

                x = CP.Sounds.Snd_Win_RestoreDown
                If Not String.IsNullOrWhiteSpace(x) AndAlso Not x.StartsWith(My.PATH_Windows & "\media", My._ignore) Then
                    ZipEntry = cache & "Snd_Win_RestoreDown" & IO.Path.GetExtension(x)
                    If IO.File.Exists(x) Then CP.Sounds.Snd_Win_RestoreDown = ZipEntry
                    filesList.Add(ZipEntry, x)
                End If

                x = CP.Sounds.Snd_Win_RestoreUp
                If Not String.IsNullOrWhiteSpace(x) AndAlso Not x.StartsWith(My.PATH_Windows & "\media", My._ignore) Then
                    ZipEntry = cache & "Snd_Win_RestoreUp" & IO.Path.GetExtension(x)
                    If IO.File.Exists(x) Then CP.Sounds.Snd_Win_RestoreUp = ZipEntry
                    filesList.Add(ZipEntry, x)
                End If

                x = CP.Sounds.Snd_Win_ShowBand
                If Not String.IsNullOrWhiteSpace(x) AndAlso Not x.StartsWith(My.PATH_Windows & "\media", My._ignore) Then
                    ZipEntry = cache & "Snd_Win_ShowBand" & IO.Path.GetExtension(x)
                    If IO.File.Exists(x) Then CP.Sounds.Snd_Win_ShowBand = ZipEntry
                    filesList.Add(ZipEntry, x)
                End If

                x = CP.Sounds.Snd_Win_SystemAsterisk
                If Not String.IsNullOrWhiteSpace(x) AndAlso Not x.StartsWith(My.PATH_Windows & "\media", My._ignore) Then
                    ZipEntry = cache & "Snd_Win_SystemAsterisk" & IO.Path.GetExtension(x)
                    If IO.File.Exists(x) Then CP.Sounds.Snd_Win_SystemAsterisk = ZipEntry
                    filesList.Add(ZipEntry, x)
                End If

                x = CP.Sounds.Snd_Win_SystemExclamation
                If Not String.IsNullOrWhiteSpace(x) AndAlso Not x.StartsWith(My.PATH_Windows & "\media", My._ignore) Then
                    ZipEntry = cache & "Snd_Win_SystemExclamation" & IO.Path.GetExtension(x)
                    If IO.File.Exists(x) Then CP.Sounds.Snd_Win_SystemExclamation = ZipEntry
                    filesList.Add(ZipEntry, x)
                End If

                x = CP.Sounds.Snd_Win_SystemExit
                If Not String.IsNullOrWhiteSpace(x) AndAlso Not x.StartsWith(My.PATH_Windows & "\media", My._ignore) Then
                    ZipEntry = cache & "Snd_Win_SystemExit" & IO.Path.GetExtension(x)
                    If IO.File.Exists(x) Then CP.Sounds.Snd_Win_SystemExit = ZipEntry
                    filesList.Add(ZipEntry, x)
                End If

                x = CP.Sounds.Snd_Win_SystemStart
                If Not String.IsNullOrWhiteSpace(x) AndAlso Not x.StartsWith(My.PATH_Windows & "\media", My._ignore) Then
                    ZipEntry = cache & "Snd_Win_SystemStart" & IO.Path.GetExtension(x)
                    If IO.File.Exists(x) Then CP.Sounds.Snd_Win_SystemStart = ZipEntry
                    filesList.Add(ZipEntry, x)
                End If

                x = CP.Sounds.Snd_Imageres_SystemStart
                If Not String.IsNullOrWhiteSpace(x) Then  'Don't include the condition: Not x.StartsWith(My.PATH_Windows & "\media", My._ignore)
                    ZipEntry = cache & "Snd_Imageres_SystemStart" & IO.Path.GetExtension(x)
                    If IO.File.Exists(x) Then CP.Sounds.Snd_Imageres_SystemStart = ZipEntry
                    filesList.Add(ZipEntry, x)
                End If

                x = CP.Sounds.Snd_Win_SystemHand
                If Not String.IsNullOrWhiteSpace(x) AndAlso Not x.StartsWith(My.PATH_Windows & "\media", My._ignore) Then
                    ZipEntry = cache & "Snd_Win_SystemHand" & IO.Path.GetExtension(x)
                    If IO.File.Exists(x) Then CP.Sounds.Snd_Win_SystemHand = ZipEntry
                    filesList.Add(ZipEntry, x)
                End If

                x = CP.Sounds.Snd_Win_SystemNotification
                If Not String.IsNullOrWhiteSpace(x) AndAlso Not x.StartsWith(My.PATH_Windows & "\media", My._ignore) Then
                    ZipEntry = cache & "Snd_Win_SystemNotification" & IO.Path.GetExtension(x)
                    If IO.File.Exists(x) Then CP.Sounds.Snd_Win_SystemNotification = ZipEntry
                    filesList.Add(ZipEntry, x)
                End If

                x = CP.Sounds.Snd_Win_SystemQuestion
                If Not String.IsNullOrWhiteSpace(x) AndAlso Not x.StartsWith(My.PATH_Windows & "\media", My._ignore) Then
                    ZipEntry = cache & "Snd_Win_SystemQuestion" & IO.Path.GetExtension(x)
                    If IO.File.Exists(x) Then CP.Sounds.Snd_Win_SystemQuestion = ZipEntry
                    filesList.Add(ZipEntry, x)
                End If

                x = CP.Sounds.Snd_Win_WindowsLogoff
                If Not String.IsNullOrWhiteSpace(x) AndAlso Not x.StartsWith(My.PATH_Windows & "\media", My._ignore) Then
                    ZipEntry = cache & "Snd_Win_WindowsLogoff" & IO.Path.GetExtension(x)
                    If IO.File.Exists(x) Then CP.Sounds.Snd_Win_WindowsLogoff = ZipEntry
                    filesList.Add(ZipEntry, x)
                End If

                x = CP.Sounds.Snd_Win_WindowsLogon
                If Not String.IsNullOrWhiteSpace(x) AndAlso Not x.StartsWith(My.PATH_Windows & "\media", My._ignore) Then
                    ZipEntry = cache & "Snd_Win_WindowsLogon" & IO.Path.GetExtension(x)
                    If IO.File.Exists(x) Then CP.Sounds.Snd_Win_WindowsLogon = ZipEntry
                    filesList.Add(ZipEntry, x)
                End If

                x = CP.Sounds.Snd_Win_WindowsUAC
                If Not String.IsNullOrWhiteSpace(x) AndAlso Not x.StartsWith(My.PATH_Windows & "\media", My._ignore) Then
                    ZipEntry = cache & "Snd_Win_WindowsUAC" & IO.Path.GetExtension(x)
                    If IO.File.Exists(x) Then CP.Sounds.Snd_Win_WindowsUAC = ZipEntry
                    filesList.Add(ZipEntry, x)
                End If

                x = CP.Sounds.Snd_Win_WindowsUnlock
                If Not String.IsNullOrWhiteSpace(x) AndAlso Not x.StartsWith(My.PATH_Windows & "\media", My._ignore) Then
                    ZipEntry = cache & "Snd_Win_WindowsUnlock" & IO.Path.GetExtension(x)
                    If IO.File.Exists(x) Then CP.Sounds.Snd_Win_WindowsUnlock = ZipEntry
                    filesList.Add(ZipEntry, x)
                End If

                x = CP.Sounds.Snd_Explorer_ActivatingDocument
                If Not String.IsNullOrWhiteSpace(x) AndAlso Not x.StartsWith(My.PATH_Windows & "\media", My._ignore) Then
                    ZipEntry = cache & "Snd_Explorer_ActivatingDocument" & IO.Path.GetExtension(x)
                    If IO.File.Exists(x) Then CP.Sounds.Snd_Explorer_ActivatingDocument = ZipEntry
                    filesList.Add(ZipEntry, x)
                End If

                x = CP.Sounds.Snd_Explorer_BlockedPopup
                If Not String.IsNullOrWhiteSpace(x) AndAlso Not x.StartsWith(My.PATH_Windows & "\media", My._ignore) Then
                    ZipEntry = cache & "Snd_Explorer_BlockedPopup" & IO.Path.GetExtension(x)
                    If IO.File.Exists(x) Then CP.Sounds.Snd_Explorer_BlockedPopup = ZipEntry
                    filesList.Add(ZipEntry, x)
                End If

                x = CP.Sounds.Snd_Explorer_EmptyRecycleBin
                If Not String.IsNullOrWhiteSpace(x) AndAlso Not x.StartsWith(My.PATH_Windows & "\media", My._ignore) Then
                    ZipEntry = cache & "Snd_Explorer_EmptyRecycleBin" & IO.Path.GetExtension(x)
                    If IO.File.Exists(x) Then CP.Sounds.Snd_Explorer_EmptyRecycleBin = ZipEntry
                    filesList.Add(ZipEntry, x)
                End If

                x = CP.Sounds.Snd_Explorer_FeedDiscovered
                If Not String.IsNullOrWhiteSpace(x) AndAlso Not x.StartsWith(My.PATH_Windows & "\media", My._ignore) Then
                    ZipEntry = cache & "Snd_Explorer_FeedDiscovered" & IO.Path.GetExtension(x)
                    If IO.File.Exists(x) Then CP.Sounds.Snd_Explorer_FeedDiscovered = ZipEntry
                    filesList.Add(ZipEntry, x)
                End If

                x = CP.Sounds.Snd_Explorer_MoveMenuItem
                If Not String.IsNullOrWhiteSpace(x) AndAlso Not x.StartsWith(My.PATH_Windows & "\media", My._ignore) Then
                    ZipEntry = cache & "Snd_Explorer_MoveMenuItem" & IO.Path.GetExtension(x)
                    If IO.File.Exists(x) Then CP.Sounds.Snd_Explorer_MoveMenuItem = ZipEntry
                    filesList.Add(ZipEntry, x)
                End If

                x = CP.Sounds.Snd_Explorer_Navigating
                If Not String.IsNullOrWhiteSpace(x) AndAlso Not x.StartsWith(My.PATH_Windows & "\media", My._ignore) Then
                    ZipEntry = cache & "Snd_Explorer_Navigating" & IO.Path.GetExtension(x)
                    If IO.File.Exists(x) Then CP.Sounds.Snd_Explorer_Navigating = ZipEntry
                    filesList.Add(ZipEntry, x)
                End If

                x = CP.Sounds.Snd_Explorer_SecurityBand
                If Not String.IsNullOrWhiteSpace(x) AndAlso Not x.StartsWith(My.PATH_Windows & "\media", My._ignore) Then
                    ZipEntry = cache & "Snd_Explorer_SecurityBand" & IO.Path.GetExtension(x)
                    If IO.File.Exists(x) Then CP.Sounds.Snd_Explorer_SecurityBand = ZipEntry
                    filesList.Add(ZipEntry, x)
                End If

                x = CP.Sounds.Snd_Explorer_SearchProviderDiscovered
                If Not String.IsNullOrWhiteSpace(x) AndAlso Not x.StartsWith(My.PATH_Windows & "\media", My._ignore) Then
                    ZipEntry = cache & "Snd_Explorer_SearchProviderDiscovered" & IO.Path.GetExtension(x)
                    If IO.File.Exists(x) Then CP.Sounds.Snd_Explorer_SearchProviderDiscovered = ZipEntry
                    filesList.Add(ZipEntry, x)
                End If

                x = CP.Sounds.Snd_Explorer_FaxError
                If Not String.IsNullOrWhiteSpace(x) AndAlso Not x.StartsWith(My.PATH_Windows & "\media", My._ignore) Then
                    ZipEntry = cache & "Snd_Explorer_FaxError" & IO.Path.GetExtension(x)
                    If IO.File.Exists(x) Then CP.Sounds.Snd_Explorer_FaxError = ZipEntry
                    filesList.Add(ZipEntry, x)
                End If

                x = CP.Sounds.Snd_Explorer_FaxLineRings
                If Not String.IsNullOrWhiteSpace(x) AndAlso Not x.StartsWith(My.PATH_Windows & "\media", My._ignore) Then
                    ZipEntry = cache & "Snd_Explorer_FaxLineRings" & IO.Path.GetExtension(x)
                    If IO.File.Exists(x) Then CP.Sounds.Snd_Explorer_FaxLineRings = ZipEntry
                    filesList.Add(ZipEntry, x)
                End If

                x = CP.Sounds.Snd_Explorer_FaxNew
                If Not String.IsNullOrWhiteSpace(x) AndAlso Not x.StartsWith(My.PATH_Windows & "\media", My._ignore) Then
                    ZipEntry = cache & "Snd_Explorer_FaxNew" & IO.Path.GetExtension(x)
                    If IO.File.Exists(x) Then CP.Sounds.Snd_Explorer_FaxNew = ZipEntry
                    filesList.Add(ZipEntry, x)
                End If

                x = CP.Sounds.Snd_Explorer_FaxSent
                If Not String.IsNullOrWhiteSpace(x) AndAlso Not x.StartsWith(My.PATH_Windows & "\media", My._ignore) Then
                    ZipEntry = cache & "Snd_Explorer_FaxSent" & IO.Path.GetExtension(x)
                    If IO.File.Exists(x) Then CP.Sounds.Snd_Explorer_FaxSent = ZipEntry
                    filesList.Add(ZipEntry, x)
                End If

                x = CP.Sounds.Snd_NetMeeting_PersonJoins
                If Not String.IsNullOrWhiteSpace(x) AndAlso Not x.StartsWith(My.PATH_Windows & "\media", My._ignore) Then
                    ZipEntry = cache & "Snd_NetMeeting_PersonJoins" & IO.Path.GetExtension(x)
                    If IO.File.Exists(x) Then CP.Sounds.Snd_NetMeeting_PersonJoins = ZipEntry
                    filesList.Add(ZipEntry, x)
                End If

                x = CP.Sounds.Snd_NetMeeting_PersonLeaves
                If Not String.IsNullOrWhiteSpace(x) AndAlso Not x.StartsWith(My.PATH_Windows & "\media", My._ignore) Then
                    ZipEntry = cache & "Snd_NetMeeting_PersonLeaves" & IO.Path.GetExtension(x)
                    If IO.File.Exists(x) Then CP.Sounds.Snd_NetMeeting_PersonLeaves = ZipEntry
                    filesList.Add(ZipEntry, x)
                End If

                x = CP.Sounds.Snd_NetMeeting_ReceiveCall
                If Not String.IsNullOrWhiteSpace(x) AndAlso Not x.StartsWith(My.PATH_Windows & "\media", My._ignore) Then
                    ZipEntry = cache & "Snd_NetMeeting_ReceiveCall" & IO.Path.GetExtension(x)
                    If IO.File.Exists(x) Then CP.Sounds.Snd_NetMeeting_ReceiveCall = ZipEntry
                    filesList.Add(ZipEntry, x)
                End If

                x = CP.Sounds.Snd_NetMeeting_ReceiveRequestToJoin
                If Not String.IsNullOrWhiteSpace(x) AndAlso Not x.StartsWith(My.PATH_Windows & "\media", My._ignore) Then
                    ZipEntry = cache & "Snd_NetMeeting_ReceiveRequestToJoin" & IO.Path.GetExtension(x)
                    If IO.File.Exists(x) Then CP.Sounds.Snd_NetMeeting_ReceiveRequestToJoin = ZipEntry
                    filesList.Add(ZipEntry, x)
                End If

                x = CP.Sounds.Snd_SpeechRec_DisNumbersSound
                If Not String.IsNullOrWhiteSpace(x) AndAlso Not x.StartsWith(My.PATH_Windows & "\media", My._ignore) Then
                    ZipEntry = cache & "Snd_SpeechRec_DisNumbersSound" & IO.Path.GetExtension(x)
                    If IO.File.Exists(x) Then CP.Sounds.Snd_SpeechRec_DisNumbersSound = ZipEntry
                    filesList.Add(ZipEntry, x)
                End If

                x = CP.Sounds.Snd_SpeechRec_HubOffSound
                If Not String.IsNullOrWhiteSpace(x) AndAlso Not x.StartsWith(My.PATH_Windows & "\media", My._ignore) Then
                    ZipEntry = cache & "Snd_SpeechRec_HubOffSound" & IO.Path.GetExtension(x)
                    If IO.File.Exists(x) Then CP.Sounds.Snd_SpeechRec_HubOffSound = ZipEntry
                    filesList.Add(ZipEntry, x)
                End If

                x = CP.Sounds.Snd_SpeechRec_HubOnSound
                If Not String.IsNullOrWhiteSpace(x) AndAlso Not x.StartsWith(My.PATH_Windows & "\media", My._ignore) Then
                    ZipEntry = cache & "Snd_SpeechRec_HubOnSound" & IO.Path.GetExtension(x)
                    If IO.File.Exists(x) Then CP.Sounds.Snd_SpeechRec_HubOnSound = ZipEntry
                    filesList.Add(ZipEntry, x)
                End If

                x = CP.Sounds.Snd_SpeechRec_HubSleepSound
                If Not String.IsNullOrWhiteSpace(x) AndAlso Not x.StartsWith(My.PATH_Windows & "\media", My._ignore) Then
                    ZipEntry = cache & "Snd_SpeechRec_HubSleepSound" & IO.Path.GetExtension(x)
                    If IO.File.Exists(x) Then CP.Sounds.Snd_SpeechRec_HubSleepSound = ZipEntry
                    filesList.Add(ZipEntry, x)
                End If

                x = CP.Sounds.Snd_SpeechRec_MisrecoSound
                If Not String.IsNullOrWhiteSpace(x) AndAlso Not x.StartsWith(My.PATH_Windows & "\media", My._ignore) Then
                    ZipEntry = cache & "Snd_SpeechRec_MisrecoSound" & IO.Path.GetExtension(x)
                    If IO.File.Exists(x) Then CP.Sounds.Snd_SpeechRec_MisrecoSound = ZipEntry
                    filesList.Add(ZipEntry, x)
                End If

                x = CP.Sounds.Snd_SpeechRec_PanelSound
                If Not String.IsNullOrWhiteSpace(x) AndAlso Not x.StartsWith(My.PATH_Windows & "\media", My._ignore) Then
                    ZipEntry = cache & "Snd_SpeechRec_PanelSound" & IO.Path.GetExtension(x)
                    If IO.File.Exists(x) Then CP.Sounds.Snd_SpeechRec_PanelSound = ZipEntry
                    filesList.Add(ZipEntry, x)
                End If
            End If
#End Region

            If CP.Wallpaper.Enabled AndAlso CP.Wallpaper.WallpaperType = Structures.Wallpaper.WallpaperTypes.Picture Then
                x = CP.Wallpaper.ImageFile
                If Not String.IsNullOrWhiteSpace(x) AndAlso Not x.StartsWith(My.PATH_Windows & "\Web", My._ignore) Then
                    ZipEntry = cache & "wallpaper_file" & IO.Path.GetExtension(x)
                    If IO.File.Exists(x) Then CP.Wallpaper.ImageFile = ZipEntry
                    filesList.Add(ZipEntry, x)
                End If
            End If

            For Each _file In filesList
                If IO.File.Exists(_file.Value) Then archive.CreateEntryFromFile(_file.Value, _file.Key.Split("\").Last, CompressionLevel.Optimal)
            Next

            If CP.WindowsXP.Theme = WindowsXP.Themes.Custom Then
                x = CP.WindowsXP.ThemeFile
                If Not String.IsNullOrWhiteSpace(x) AndAlso IO.File.Exists(x) AndAlso Not x.StartsWith(My.PATH_Windows & "\Resources\Themes\Luna", My._ignore) Then
                    ZipEntry = cache & "WXP_VS\" & IO.Path.GetFileName(x)
                    If IO.File.Exists(x) Then CP.WindowsXP.ThemeFile = ZipEntry
                    Dim DirName As String = New FileInfo(x).Directory.FullName
                    For Each file As String In Directory.EnumerateFiles(DirName, "*.*", SearchOption.AllDirectories)
                        If IO.File.Exists(file) Then archive.CreateEntryFromFile(file, "WXP_VS" & file.Replace(DirName, ""), CompressionLevel.Optimal)
                    Next
                End If
            End If

            If CP.Wallpaper.Enabled AndAlso CP.Wallpaper.WallpaperType = Structures.Wallpaper.WallpaperTypes.SlideShow Then
                If CP.Wallpaper.SlideShow_Folder_or_ImagesList Then
                    x = CP.Wallpaper.Wallpaper_Slideshow_ImagesRootPath
                    If Not String.IsNullOrWhiteSpace(x) AndAlso Not x.StartsWith(My.PATH_Windows & "\Web", My._ignore) Then
                        CP.Wallpaper.Wallpaper_Slideshow_ImagesRootPath = cache & "wallpapers_slideshow"

                        For Each image In Directory.EnumerateFiles(x, "*.*", SearchOption.TopDirectoryOnly).Where(Function(s)
                                                                                                                      Return s.EndsWith(".bmp") _
                                                                                                                                      OrElse s.EndsWith(".jpg") _
                                                                                                                                      OrElse s.EndsWith(".png") _
                                                                                                                                      OrElse s.EndsWith(".gif")
                                                                                                                  End Function)


                            If IO.File.Exists(image) Then archive.CreateEntryFromFile(image, "wallpapers_slideshow\" & New FileInfo(image).Name, CompressionLevel.Optimal)

                        Next

                    End If

                Else
                    Dim arr As String() = CP.Wallpaper.Wallpaper_Slideshow_Images.ToArray
                    If arr.Count > 0 Then
                        If Not arr(0).StartsWith(My.PATH_Windows & "\Web", My._ignore) Then
                            CP.Wallpaper.Wallpaper_Slideshow_ImagesRootPath = cache & "WallpapersList"
                            CP.Wallpaper.Wallpaper_Slideshow_Images = New String() {}
                            For x0 = 0 To arr.Count - 1
                                x = arr(x0)
                                If Not String.IsNullOrWhiteSpace(x) AndAlso Not x.StartsWith(My.PATH_Windows & "\Web", My._ignore) Then
                                    ZipEntry = cache & "WallpapersList\wallpaperlist_" & x0 & "_file" & IO.Path.GetExtension(x)
                                    If IO.File.Exists(x) Then
                                        CP.Wallpaper.Wallpaper_Slideshow_Images = CP.Wallpaper.Wallpaper_Slideshow_Images.Append(ZipEntry).ToArray
                                        archive.CreateEntryFromFile(x, "WallpapersList\wallpaperlist_" & x0 & "_file" & IO.Path.GetExtension(x), CompressionLevel.Optimal)
                                    End If
                                End If
                            Next
                        End If
                    End If
                End If

            End If

            IO.File.WriteAllText(CP_File, CP.ToString)
        End Using

    End Sub

    Public Shared Function Decompress(File As String) As IEnumerable(Of String)
        Dim DecompressedData As IEnumerable(Of String)

        Try
            DecompressedData = IO.File.ReadAllText(File).Decompress.CList
        Catch
            DecompressedData = IO.File.ReadAllText(File).CList
        End Try

        Return DecompressedData
    End Function

    Private Function DeserializeProps([StructureType] As Type, [Structure] As Object) As JObject
        Dim j As New JObject()

        j.RemoveAll()

        For Each field In [StructureType].GetFields(BindingFlags.Instance Or BindingFlags.NonPublic Or BindingFlags.Public)
            Dim result As JToken

            Try
                result = JToken.FromObject(field.GetValue([Structure]))
            Catch
                result = Nothing
            End Try

            j.Add(field.Name, result)
        Next

        Return j
    End Function

    Private Shared Function IsValidJson(ByVal strInput As String) As Boolean
        If String.IsNullOrWhiteSpace(strInput) Then
            Return False
        End If
        strInput = strInput.Trim()
        If strInput.StartsWith("{") AndAlso strInput.EndsWith("}") OrElse strInput.StartsWith("[") AndAlso strInput.EndsWith("]") Then 'For object
            'For array
            Try
                Dim obj = JToken.Parse(strInput)
                Return True
            Catch jex As JsonReaderException
                'Exception in parsing json
                Return False
            Catch ex As Exception 'some other exception
                Return False
            End Try
        Else
            Return False
        End If
    End Function
#End Region

#Region "Applying Subs"
    Public Sub Apply_LogonUI7([LogonElement] As Structures.LogonUI7, Optional RegEntryHint As String = "LogonUI", Optional [TreeView] As TreeView = Nothing)

        Dim ReportProgress As Boolean = My.Settings.ThemeLog.VerboseLevel <> WPSettings.Structures.ThemeLog.VerboseLevels.None AndAlso [TreeView] IsNot Nothing
        Dim ReportProgress_Detailed As Boolean = ReportProgress AndAlso My.Settings.ThemeLog.VerboseLevel = WPSettings.Structures.ThemeLog.VerboseLevels.Detailed

        EditReg("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Authentication\LogonUI\Background", "OEMBackground", [LogonElement].Enabled.ToInteger)
        EditReg("HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Windows\System", "UseOEMBackground", [LogonElement].Enabled.ToInteger)

        EditReg("HKEY_CURRENT_USER\Software\WinPaletter\" & RegEntryHint, "Mode", CInt([LogonElement].Mode))
        EditReg("HKEY_CURRENT_USER\Software\WinPaletter\" & RegEntryHint, "ImagePath", [LogonElement].ImagePath, RegistryValueKind.String)
        EditReg("HKEY_CURRENT_USER\Software\WinPaletter\" & RegEntryHint, "Color", [LogonElement].Color.ToArgb)
        EditReg("HKEY_CURRENT_USER\Software\WinPaletter\" & RegEntryHint, "Blur", [LogonElement].Blur.ToInteger)
        EditReg("HKEY_CURRENT_USER\Software\WinPaletter\" & RegEntryHint, "Blur_Intensity", [LogonElement].Blur_Intensity)
        EditReg("HKEY_CURRENT_USER\Software\WinPaletter\" & RegEntryHint, "Grayscale", [LogonElement].Grayscale.ToInteger)
        EditReg("HKEY_CURRENT_USER\Software\WinPaletter\" & RegEntryHint, "Noise", [LogonElement].Noise.ToInteger)
        EditReg("HKEY_CURRENT_USER\Software\WinPaletter\" & RegEntryHint, "Noise_Mode", CInt([LogonElement].Noise_Mode))
        EditReg("HKEY_CURRENT_USER\Software\WinPaletter\" & RegEntryHint, "Noise_Intensity", [LogonElement].Noise_Intensity)

        If [LogonElement].Enabled Then
            Kernel32.Wow64DisableWow64FsRedirection(IntPtr.Zero)

            Dim DirX As String = My.PATH_System32 & "\oobe\info\backgrounds"

            IO.Directory.CreateDirectory(DirX)

            For Each fileX As String In My.Computer.FileSystem.GetFiles(DirX)
                Try : Kill(fileX) : Catch : End Try
            Next

            Dim bmpList As New List(Of Bitmap)
            bmpList.Clear()

            If ReportProgress_Detailed Then AddNode([TreeView], My.Lang.Verbose_GetInstanceLogonUIImg, "info")

            Select Case [LogonElement].Mode
                Case Structures.LogonUI7.Modes.Default_
                    For i As Integer = 5031 To 5043 Step +1
                        bmpList.Add(PE_Functions.GetPNGFromDLL(My.PATH_imageres, i, "IMAGE", My.Computer.Screen.Bounds.Size.Width, My.Computer.Screen.Bounds.Size.Height))
                    Next

                Case Structures.LogonUI7.Modes.CustomImage
                    If IO.File.Exists([LogonElement].ImagePath) Then
                        bmpList.Add(Bitmap_Mgr.Load([LogonElement].ImagePath).Resize(My.Computer.Screen.Bounds.Size))
                    Else
                        bmpList.Add(Color.Black.ToBitmap(My.Computer.Screen.Bounds.Size))
                    End If

                Case Structures.LogonUI7.Modes.SolidColor
                    bmpList.Add([LogonElement].Color.ToBitmap(My.Computer.Screen.Bounds.Size))

                Case Structures.LogonUI7.Modes.Wallpaper
                    Using b As New Bitmap(My.Application.GetWallpaper)
                        bmpList.Add(b.Resize(My.Computer.Screen.Bounds.Size).Clone)
                    End Using

            End Select

            If ReportProgress Then AddNode([TreeView], String.Format(My.Lang.CP_RenderingCustomLogonUI_MayNotRespond), "info")

            For x = 0 To bmpList.Count - 1
                If ReportProgress Then AddNode([TreeView], String.Format("{3}: " & My.Lang.CP_RenderingCustomLogonUI_Progress & " {2} ({0}/{1})", x + 1, bmpList.Count, bmpList(x).Width & "x" & bmpList(x).Height, Now.ToLongTimeString), "info")

                If [LogonElement].Grayscale Then
                    If ReportProgress_Detailed Then AddNode([TreeView], My.Lang.Verbose_GrayscaleLogonUIImg, "apply")
                    bmpList(x) = bmpList(x).Grayscale
                End If


                If [LogonElement].Blur Then
                    If ReportProgress_Detailed Then AddNode([TreeView], My.Lang.Verbose_BlurringLogonUIImg, "apply")

                    Dim imgF As New ImageProcessor.ImageFactory
                    Using b As New Bitmap(bmpList(x))
                        imgF.Load(b)
                        imgF.GaussianBlur([LogonElement].Blur_Intensity)
                        bmpList(x) = imgF.Image
                    End Using

                End If

                If [LogonElement].Noise Then
                    If ReportProgress_Detailed Then AddNode([TreeView], My.Lang.Verbose_NoiseLogonUIImg, "apply")

                    bmpList(x) = bmpList(x).Noise([LogonElement].Noise_Mode, [LogonElement].Noise_Intensity / 100)
                End If
            Next

            If bmpList.Count = 1 Then
                bmpList(0).Save(DirX & "\backgroundDefault.jpg", Imaging.ImageFormat.Jpeg)
                If ReportProgress_Detailed Then AddNode([TreeView], String.Format(My.Lang.Verbose_LogonUIImgSaved, DirX & "\backgroundDefault.jpg"), "info")
            Else
                For x = 0 To bmpList.Count - 1
                    bmpList(x).Save(DirX & String.Format("\background{0}x{1}.jpg", bmpList(x).Width, bmpList(x).Height), Imaging.ImageFormat.Jpeg)
                    If ReportProgress_Detailed Then AddNode([TreeView], String.Format(My.Lang.Verbose_LogonUIImgNUMSaved, DirX & String.Format("\background{0}x{1}.jpg", bmpList(x).Width, bmpList(x).Height), x + 1), "info")

                Next
            End If

            Kernel32.Wow64RevertWow64FsRedirection(IntPtr.Zero)
        End If
    End Sub

    Public Sub Apply_LogonUI_8(Optional [TreeView] As TreeView = Nothing)

        Dim ReportProgress As Boolean = My.Settings.ThemeLog.VerboseLevel <> WPSettings.Structures.ThemeLog.VerboseLevels.None AndAlso [TreeView] IsNot Nothing
        Dim ReportProgress_Detailed As Boolean = ReportProgress AndAlso My.Settings.ThemeLog.VerboseLevel = WPSettings.Structures.ThemeLog.VerboseLevels.Detailed

        Dim lockimg As String = My.PATH_appData & "\LockScreen.png"

        EditReg("HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Windows\Personalization", "NoLockScreen", Windows81.NoLockScreen.ToInteger)
        EditReg("HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Windows\Personalization", "LockScreenImage", lockimg, RegistryValueKind.String)

        EditReg("HKEY_CURRENT_USER\Software\WinPaletter\LogonUI", "Mode", CInt(Windows81.LockScreenType))
        EditReg("HKEY_CURRENT_USER\Software\WinPaletter\LogonUI", "Metro_LockScreenSystemID", Windows81.LockScreenSystemID)
        EditReg("HKEY_CURRENT_USER\Software\WinPaletter\LogonUI", "ImagePath", LogonUI7.ImagePath, RegistryValueKind.String)
        EditReg("HKEY_CURRENT_USER\Software\WinPaletter\LogonUI", "Color", LogonUI7.Color.ToArgb)
        EditReg("HKEY_CURRENT_USER\Software\WinPaletter\LogonUI", "Blur", LogonUI7.Blur.ToInteger)
        EditReg("HKEY_CURRENT_USER\Software\WinPaletter\LogonUI", "Blur_Intensity", LogonUI7.Blur_Intensity)
        EditReg("HKEY_CURRENT_USER\Software\WinPaletter\LogonUI", "Grayscale", LogonUI7.Grayscale.ToInteger)
        EditReg("HKEY_CURRENT_USER\Software\WinPaletter\LogonUI", "Noise", LogonUI7.Noise.ToInteger)
        EditReg("HKEY_CURRENT_USER\Software\WinPaletter\LogonUI", "Noise_Mode", CInt(LogonUI7.Noise_Mode))
        EditReg("HKEY_CURRENT_USER\Software\WinPaletter\LogonUI", "Noise_Intensity", LogonUI7.Noise_Intensity)

        If Not Windows81.NoLockScreen Then
            Dim bmp As Bitmap

            If ReportProgress_Detailed Then AddNode([TreeView], My.Lang.Verbose_GetInstanceLockScreenImg, "info")

            Select Case Windows81.LockScreenType
                Case Structures.LogonUI7.Modes.Default_
                    Dim syslock As String = ""

                    If IO.File.Exists(String.Format(My.PATH_Windows & "\Web\Screen\img10{0}.png", My.CP.Windows81.LockScreenSystemID)) Then
                        syslock = String.Format(My.PATH_Windows & "\Web\Screen\img10{0}.png", My.CP.Windows81.LockScreenSystemID)

                    ElseIf IO.File.Exists(String.Format(My.PATH_Windows & "\Web\Screen\img10{0}.jpg", My.CP.Windows81.LockScreenSystemID)) Then
                        syslock = String.Format(My.PATH_Windows & "\Web\Screen\img10{0}.jpg", My.CP.Windows81.LockScreenSystemID)

                    End If

                    If IO.File.Exists(syslock) Then
                        bmp = Bitmap_Mgr.Load(syslock)
                    Else
                        bmp = Color.Black.ToBitmap(My.Computer.Screen.Bounds.Size)
                    End If

                Case Structures.LogonUI7.Modes.CustomImage
                    If IO.File.Exists(LogonUI7.ImagePath) Then
                        bmp = Bitmap_Mgr.Load(LogonUI7.ImagePath)
                    Else
                        bmp = Color.Black.ToBitmap(My.Computer.Screen.Bounds.Size)
                    End If

                Case Structures.LogonUI7.Modes.SolidColor
                    bmp = LogonUI7.Color.ToBitmap(My.Computer.Screen.Bounds.Size)

                Case Structures.LogonUI7.Modes.Wallpaper
                    Using b As New Bitmap(My.Application.GetWallpaper)
                        bmp = b.Clone
                    End Using

                Case Else
                    bmp = Color.Black.ToBitmap(My.Computer.Screen.Bounds.Size)

            End Select

            If ReportProgress Then AddNode([TreeView], String.Format(My.Lang.CP_RenderingCustomLogonUI_MayNotRespond), "info")

            If ReportProgress Then AddNode([TreeView], String.Format("{0}:  " & My.Lang.CP_RenderingCustomLogonUI, Now.ToLongTimeString), "info")

            If LogonUI7.Grayscale Then
                If ReportProgress_Detailed Then AddNode([TreeView], My.Lang.Verbose_GrayscaleLockScreenImg, "apply")
                bmp = bmp.Grayscale
            End If

            If LogonUI7.Blur Then
                If ReportProgress_Detailed Then AddNode([TreeView], My.Lang.Verbose_BlurringLockScreenImg, "apply")
                Dim imgF As New ImageProcessor.ImageFactory
                Using b As New Bitmap(bmp)
                    imgF.Load(b)
                    imgF.GaussianBlur(LogonUI7.Blur_Intensity)
                    bmp = imgF.Image
                End Using

            End If

            If LogonUI7.Noise Then
                If ReportProgress_Detailed Then AddNode([TreeView], My.Lang.Verbose_NoiseLockScreenImg, "apply")
                bmp = bmp.Noise(LogonUI7.Noise_Mode, LogonUI7.Noise_Intensity / 100)
            End If

            If IO.File.Exists(lockimg) Then Kill(lockimg)

            If ReportProgress_Detailed Then AddNode([TreeView], String.Format(My.Lang.Verbose_LockScreenImgSaved, lockimg), "info")
            bmp.Save(lockimg)

        End If

    End Sub

    Public Sub Apply_CommandPrompt(Optional [TreeView] As TreeView = Nothing)
        If CommandPrompt.Enabled Then
            Structures.Console.Save_Console_To_Registry("HKEY_CURRENT_USER", "", CommandPrompt, [TreeView])
            If My.Settings.ThemeApplyingBehavior.CMD_OverrideUserPreferences Then Structures.Console.Save_Console_To_Registry("HKEY_CURRENT_USER", "%SystemRoot%_System32_cmd.exe", CommandPrompt, [TreeView])

            If My.Settings.ThemeApplyingBehavior.CMD_HKU_DEFAULT_Prefs = WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite Then
                Structures.Console.Save_Console_To_Registry("HKEY_USERS\.DEFAULT", "", CommandPrompt, [TreeView])
                Structures.Console.Save_Console_To_Registry("HKEY_USERS\.DEFAULT", "%SystemRoot%_System32_cmd.exe", CommandPrompt, [TreeView])
            End If
        End If
    End Sub

    Public Sub Apply_PowerShell86(Optional [TreeView] As TreeView = Nothing)
        If PowerShellx86.Enabled And IO.Directory.Exists(Environment.GetEnvironmentVariable("WINDIR") & "\System32\WindowsPowerShell\v1.0") Then

            Structures.Console.Save_Console_To_Registry("HKEY_CURRENT_USER", "%SystemRoot%_System32_WindowsPowerShell_v1.0_powershell.exe", PowerShellx86, [TreeView])


            If My.Settings.ThemeApplyingBehavior.PS86_HKU_DEFAULT_Prefs = WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite Then
                Structures.Console.Save_Console_To_Registry("HKEY_USERS\.DEFAULT", "%SystemRoot%_System32_WindowsPowerShell_v1.0_powershell.exe", PowerShellx86, [TreeView])
            End If

        End If
    End Sub

    Public Sub Apply_PowerShell64(Optional [TreeView] As TreeView = Nothing)
        If PowerShellx64.Enabled And IO.Directory.Exists(Environment.GetEnvironmentVariable("WINDIR") & "\SysWOW64\WindowsPowerShell\v1.0") Then

            Structures.Console.Save_Console_To_Registry("HKEY_CURRENT_USER", "%SystemRoot%_SysWOW64_WindowsPowerShell_v1.0_powershell.exe", PowerShellx64, [TreeView])

            If My.Settings.ThemeApplyingBehavior.PS64_HKU_DEFAULT_Prefs = WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite Then
                Structures.Console.Save_Console_To_Registry("HKEY_USERS\.DEFAULT", "%SystemRoot%_SysWOW64_WindowsPowerShell_v1.0_powershell.exe", PowerShellx64, [TreeView])
            End If
        End If
    End Sub

    Public Sub Apply_Cursors(Optional [TreeView] As TreeView = Nothing)
        Dim ReportProgress As Boolean = My.Settings.ThemeLog.VerboseLevel <> WPSettings.Structures.ThemeLog.VerboseLevels.None AndAlso [TreeView] IsNot Nothing
        Dim ReportProgress_Detailed As Boolean = ReportProgress AndAlso My.Settings.ThemeLog.VerboseLevel = WPSettings.Structures.ThemeLog.VerboseLevels.Detailed

        EditReg("HKEY_CURRENT_USER\Software\WinPaletter\Cursors", "", Cursor_Enabled)

        Dim sw As New Stopwatch
        If ReportProgress Then AddNode([TreeView], String.Format("{0}: " & My.Lang.CP_SavingCursorsColors, Now.ToLongTimeString), "info")

        sw.Reset()
        sw.Start()

        Structures.Cursor.Save_Cursors_To_Registry("Arrow", Cursor_Arrow, If(ReportProgress_Detailed, [TreeView], Nothing))
        Structures.Cursor.Save_Cursors_To_Registry("Help", Cursor_Help, If(ReportProgress_Detailed, [TreeView], Nothing))
        Structures.Cursor.Save_Cursors_To_Registry("AppLoading", Cursor_AppLoading, If(ReportProgress_Detailed, [TreeView], Nothing))
        Structures.Cursor.Save_Cursors_To_Registry("Busy", Cursor_Busy, If(ReportProgress_Detailed, [TreeView], Nothing))
        Structures.Cursor.Save_Cursors_To_Registry("Move", Cursor_Move, If(ReportProgress_Detailed, [TreeView], Nothing))
        Structures.Cursor.Save_Cursors_To_Registry("NS", Cursor_NS, If(ReportProgress_Detailed, [TreeView], Nothing))
        Structures.Cursor.Save_Cursors_To_Registry("EW", Cursor_EW, If(ReportProgress_Detailed, [TreeView], Nothing))
        Structures.Cursor.Save_Cursors_To_Registry("NESW", Cursor_NESW, If(ReportProgress_Detailed, [TreeView], Nothing))
        Structures.Cursor.Save_Cursors_To_Registry("NWSE", Cursor_NWSE, If(ReportProgress_Detailed, [TreeView], Nothing))
        Structures.Cursor.Save_Cursors_To_Registry("Up", Cursor_Up, If(ReportProgress_Detailed, [TreeView], Nothing))
        Structures.Cursor.Save_Cursors_To_Registry("Pen", Cursor_Pen, If(ReportProgress_Detailed, [TreeView], Nothing))
        Structures.Cursor.Save_Cursors_To_Registry("None", Cursor_None, If(ReportProgress_Detailed, [TreeView], Nothing))
        Structures.Cursor.Save_Cursors_To_Registry("Link", Cursor_Link, If(ReportProgress_Detailed, [TreeView], Nothing))
        Structures.Cursor.Save_Cursors_To_Registry("Pin", Cursor_Pin, If(ReportProgress_Detailed, [TreeView], Nothing))
        Structures.Cursor.Save_Cursors_To_Registry("Person", Cursor_Person, If(ReportProgress_Detailed, [TreeView], Nothing))
        Structures.Cursor.Save_Cursors_To_Registry("IBeam", Cursor_IBeam, If(ReportProgress_Detailed, [TreeView], Nothing))
        Structures.Cursor.Save_Cursors_To_Registry("Cross", Cursor_Cross, If(ReportProgress_Detailed, [TreeView], Nothing))

        If ReportProgress Then AddNode([TreeView], String.Format(My.Lang.CP_Time, sw.ElapsedMilliseconds / 1000), "time")
        sw.Stop()

        If Cursor_Enabled Then
            Execute(CType(Sub()
                              ExportCursors(Me, [TreeView])
                          End Sub, MethodInvoker), [TreeView], My.Lang.CP_RenderingCursors, My.Lang.CP_RenderingCursors_Error, My.Lang.CP_Time)

            If My.Settings.ThemeApplyingBehavior.AutoApplyCursors Then
                Execute(CType(Sub()
                                  If ReportProgress_Detailed Then AddNode([TreeView], String.Format(My.Lang.Verbose_SettingCursorShadow, Cursor_Shadow), "dll")
                                  SystemParametersInfo(SPI.Cursors.SETCURSORSHADOW, 0, Cursor_Shadow, SPIF.UpdateINIFile)

                                  If ReportProgress_Detailed Then AddNode([TreeView], String.Format(My.Lang.Verbose_SettingCursorSonar, Cursor_Sonar), "dll")
                                  SystemParametersInfo(SPI.Cursors.SETMOUSESONAR, 0, Cursor_Sonar, SPIF.UpdateINIFile)

                                  If ReportProgress_Detailed Then AddNode([TreeView], String.Format(My.Lang.Verbose_SettingCursorTrails, Cursor_Trails), "dll")
                                  SystemParametersInfo(SPI.Cursors.SETMOUSETRAILS, Cursor_Trails, 0, SPIF.UpdateINIFile)

                                  ApplyCursorsToReg("HKEY_CURRENT_USER", If(ReportProgress_Detailed, [TreeView], Nothing))

                                  If My.Settings.ThemeApplyingBehavior.Cursors_HKU_DEFAULT_Prefs = WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite Then
                                      EditReg("HKEY_USERS\.DEFAULT\Control Panel\Mouse", "MouseTrails", Cursor_Trails)
                                      ApplyCursorsToReg("HKEY_USERS\.DEFAULT", If(ReportProgress_Detailed, [TreeView], Nothing))
                                  End If

                              End Sub, MethodInvoker), [TreeView], My.Lang.CP_ApplyingCursors, My.Lang.CP_CursorsApplying_Error, My.Lang.CP_Time)
            Else
                If ReportProgress Then AddNode([TreeView], String.Format("{0}: {1}", Now.ToLongTimeString, My.Lang.CP_Restricted_Cursors), "error")
            End If
        Else

            If My.Settings.ThemeApplyingBehavior.ResetCursorsToAero Then
                If Not My.WXP Then
                    ResetCursorsToAero("HKEY_CURRENT_USER", If(ReportProgress_Detailed, [TreeView], Nothing))
                    If My.Settings.ThemeApplyingBehavior.Cursors_HKU_DEFAULT_Prefs = WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite Then ResetCursorsToAero("HKEY_USERS\.DEFAULT")

                Else
                    ResetCursorsToNone_XP("HKEY_CURRENT_USER", If(ReportProgress_Detailed, [TreeView], Nothing))
                    If My.Settings.ThemeApplyingBehavior.Cursors_HKU_DEFAULT_Prefs = WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite Then ResetCursorsToNone_XP("HKEY_USERS\.DEFAULT")

                End If
            End If

        End If

    End Sub
#End Region

#Region "Cursors Render"
    Sub ExportCursors([CP] As CP, Optional [TreeView] As TreeView = Nothing)
        Dim ReportProgress As Boolean = My.Settings.ThemeLog.VerboseLevel <> WPSettings.Structures.ThemeLog.VerboseLevels.None AndAlso [TreeView] IsNot Nothing
        Dim ReportProgress_Detailed As Boolean = ReportProgress AndAlso My.Settings.ThemeLog.VerboseLevel = WPSettings.Structures.ThemeLog.VerboseLevels.Detailed

        Try : RenderCursor(CursorType.Arrow, [CP], If(ReportProgress_Detailed, [TreeView], Nothing)) : Catch : End Try
        Try : RenderCursor(CursorType.Help, [CP], If(ReportProgress_Detailed, [TreeView], Nothing)) : Catch : End Try
        Try : RenderCursor(CursorType.AppLoading, [CP], If(ReportProgress_Detailed, [TreeView], Nothing)) : Catch : End Try
        Try : RenderCursor(CursorType.Busy, [CP], If(ReportProgress_Detailed, [TreeView], Nothing)) : Catch : End Try
        Try : RenderCursor(CursorType.Pen, [CP], If(ReportProgress_Detailed, [TreeView], Nothing)) : Catch : End Try
        Try : RenderCursor(CursorType.None, [CP], If(ReportProgress_Detailed, [TreeView], Nothing)) : Catch : End Try
        Try : RenderCursor(CursorType.Move, [CP], If(ReportProgress_Detailed, [TreeView], Nothing)) : Catch : End Try
        Try : RenderCursor(CursorType.Up, [CP], If(ReportProgress_Detailed, [TreeView], Nothing)) : Catch : End Try
        Try : RenderCursor(CursorType.NS, [CP], If(ReportProgress_Detailed, [TreeView], Nothing)) : Catch : End Try
        Try : RenderCursor(CursorType.EW, [CP], If(ReportProgress_Detailed, [TreeView], Nothing)) : Catch : End Try
        Try : RenderCursor(CursorType.NESW, [CP], If(ReportProgress_Detailed, [TreeView], Nothing)) : Catch : End Try
        Try : RenderCursor(CursorType.NWSE, [CP], If(ReportProgress_Detailed, [TreeView], Nothing)) : Catch : End Try
        Try : RenderCursor(CursorType.Link, [CP], If(ReportProgress_Detailed, [TreeView], Nothing)) : Catch : End Try
        Try : RenderCursor(CursorType.Pin, [CP], If(ReportProgress_Detailed, [TreeView], Nothing)) : Catch : End Try
        Try : RenderCursor(CursorType.Person, [CP], If(ReportProgress_Detailed, [TreeView], Nothing)) : Catch : End Try
        Try : RenderCursor(CursorType.IBeam, [CP], If(ReportProgress_Detailed, [TreeView], Nothing)) : Catch : End Try
        Try : RenderCursor(CursorType.Cross, [CP], If(ReportProgress_Detailed, [TreeView], Nothing)) : Catch : End Try
    End Sub

    Sub RenderCursor([Type] As CursorType, [CP] As CP, Optional [TreeView] As TreeView = Nothing)

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

        If [TreeView] IsNot Nothing Then AddNode([TreeView], String.Format(My.Lang.Verbose_RenderingCursor, CurName), "pe_patch")

        If Not [Type] = CursorType.Busy And Not [Type] = CursorType.AppLoading Then

            If Not IO.Directory.Exists(My.PATH_CursorsWP) Then IO.Directory.CreateDirectory(My.PATH_CursorsWP)
            Dim Path As String = String.Format(My.PATH_CursorsWP & "\{0}.cur", CurName)

            Dim fs As New IO.FileStream(Path, IO.FileMode.Create)
            Dim EO As New EOIcoCurWriter(fs, 7, EOIcoCurWriter.IcoCurType.Cursor)

            For i As Single = 1 To 4 Step 0.5
                Dim bmp As New Bitmap(32 * i, 32 * i, Drawing.Imaging.PixelFormat.Format32bppPArgb)
                Dim HotPoint As New Point(1, 1)

                Select Case [Type]
                    Case CursorType.Arrow
                        Dim CurOptions As New CursorOptions(Cursor_Arrow) With {.Cursor = CursorType.Arrow, .LineThickness = 1, .Scale = i, ._Angle = 0}
                        bmp = Draw(CurOptions)
                        HotPoint = New Point(1, 1)

                    Case CursorType.Help
                        Dim CurOptions As New CursorOptions(Cursor_Help) With {.Cursor = CursorType.Help, .LineThickness = 1, .Scale = i, ._Angle = 0}
                        bmp = Draw(CurOptions)
                        HotPoint = New Point(1, 1)

                    Case CursorType.None
                        Dim CurOptions As New CursorOptions(Cursor_None) With {.Cursor = CursorType.None, .LineThickness = 1, .Scale = i, ._Angle = 0}
                        bmp = Draw(CurOptions)
                        HotPoint = New Point(1 + CInt(8 * i), 1 + CInt(8 * i))

                    Case CursorType.Move
                        Dim CurOptions As New CursorOptions(Cursor_Move) With {.Cursor = CursorType.Move, .LineThickness = 1, .Scale = i, ._Angle = 0}
                        bmp = Draw(CurOptions)
                        HotPoint = New Point(1 + CInt(11 * i), 1 + CInt(11 * i))

                    Case CursorType.Up
                        Dim CurOptions As New CursorOptions(Cursor_Up) With {.Cursor = CursorType.Up, .LineThickness = 1, .Scale = i, ._Angle = 0}
                        bmp = Draw(CurOptions)
                        HotPoint = New Point(1 + CInt(4 * i), 1)

                    Case CursorType.NS
                        Dim CurOptions As New CursorOptions(Cursor_NS) With {.Cursor = CursorType.NS, .LineThickness = 1, .Scale = i, ._Angle = 0}
                        bmp = Draw(CurOptions)
                        HotPoint = New Point(1 + CInt(4 * i), 1 + CInt(11 * i))

                    Case CursorType.EW
                        Dim CurOptions As New CursorOptions(Cursor_EW) With {.Cursor = CursorType.EW, .LineThickness = 1, .Scale = i, ._Angle = 0}
                        bmp = Draw(CurOptions)
                        HotPoint = New Point(1 + 11 * i, 1 + 4 * i)

                    Case CursorType.NESW
                        Dim CurOptions As New CursorOptions(Cursor_NESW) With {.Cursor = CursorType.NESW, .LineThickness = 1, .Scale = i, ._Angle = 0}
                        bmp = Draw(CurOptions)
                        HotPoint = New Point(1 + CInt(8 * i), 1 + CInt(8 * i))

                    Case CursorType.NWSE
                        Dim CurOptions As New CursorOptions(Cursor_NWSE) With {.Cursor = CursorType.NWSE, .LineThickness = 1, .Scale = i, ._Angle = 0}
                        bmp = Draw(CurOptions)
                        HotPoint = New Point(1 + CInt(8 * i), 1 + CInt(8 * i))

                    Case CursorType.Pen
                        Dim CurOptions As New CursorOptions(Cursor_Pen) With {.Cursor = CursorType.Pen, .LineThickness = 1, .Scale = i, ._Angle = 0}
                        bmp = Draw(CurOptions)
                        HotPoint = New Point(1, 1)

                    Case CursorType.Link
                        Dim CurOptions As New CursorOptions(Cursor_Link) With {.Cursor = CursorType.Link, .LineThickness = 1, .Scale = i, ._Angle = 0}
                        bmp = Draw(CurOptions)
                        HotPoint = New Point(1 + CInt(6 * i), If(CurOptions.ArrowStyle <> ArrowStyle.Classic, 1, 2))

                    Case CursorType.Pin
                        Dim CurOptions As New CursorOptions(Cursor_Pin) With {.Cursor = CursorType.Pin, .LineThickness = 1, .Scale = i, ._Angle = 0}
                        bmp = Draw(CurOptions)
                        HotPoint = New Point(1 + CInt(6 * i), If(CurOptions.ArrowStyle <> ArrowStyle.Classic, 1, 2))

                    Case CursorType.Person
                        Dim CurOptions As New CursorOptions(Cursor_Person) With {.Cursor = CursorType.Person, .LineThickness = 1, .Scale = i, ._Angle = 0}
                        bmp = Draw(CurOptions)
                        HotPoint = New Point(1 + CInt(6 * i), If(CurOptions.ArrowStyle <> ArrowStyle.Classic, 1, 2))

                    Case CursorType.IBeam
                        Dim CurOptions As New CursorOptions(Cursor_IBeam) With {.Cursor = CursorType.IBeam, .LineThickness = 1, .Scale = i, ._Angle = 0}
                        bmp = Draw(CurOptions)
                        HotPoint = New Point(1 + CInt(4 * i), 1 + CInt(9 * i))

                    Case CursorType.Cross
                        Dim CurOptions As New CursorOptions(Cursor_Cross) With {.Cursor = CursorType.Cross, .LineThickness = 1, .Scale = i, ._Angle = 0}
                        bmp = Draw(CurOptions)
                        HotPoint = New Point(1 + CInt(9 * i), 1 + CInt(9 * i))

                End Select

                EO.WriteBitmap(bmp, Nothing, HotPoint)

            Next

            fs.Close()

            If [TreeView] IsNot Nothing Then AddNode([TreeView], String.Format(My.Lang.Verbose_CursorRenderedInto, Path), "info")

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
                            Dim CurOptions As New CursorOptions(Cursor_AppLoading) With {.Cursor = CursorType.AppLoading, .LineThickness = 1, .Scale = i, ._Angle = ang}
                            bm = New Bitmap(Draw(CurOptions))
                            HotPoint = New Point(1, 1 + CInt(8 * i))

                        Else
                            Dim CurOptions As New CursorOptions(Cursor_Busy) With {.Cursor = CursorType.Busy, .LineThickness = 1, .Scale = i, ._Angle = ang}
                            bm = New Bitmap(Draw(CurOptions))

                            HotPoint = New Point(If(CurOptions.CircleStyle <> CircleStyle.Classic, 1, 2) + CInt(11 * i), 1 + CInt(11 * i))

                        End If

                        BMPList.Add(bm)
                    Next

                    For ang As Integer = 0 To 180 Step +10
                        Dim bm As Bitmap

                        If [Type] = CursorType.AppLoading Then
                            Dim CurOptions As New CursorOptions(Cursor_AppLoading) With {.Cursor = CursorType.AppLoading, .LineThickness = 1, .Scale = i, ._Angle = ang}
                            bm = New Bitmap(Draw(CurOptions))
                            HotPoint = New Point(1, 1 + CInt(8 * i))

                        Else
                            Dim CurOptions As New CursorOptions(Cursor_Busy) With {.Cursor = CursorType.Busy, .LineThickness = 1, .Scale = i, ._Angle = ang}
                            bm = New Bitmap(Draw(CurOptions))
                            HotPoint = New Point(If(CurOptions.CircleStyle <> CircleStyle.Classic, 1, 2) + CInt(11 * i), 1 + CInt(11 * i))

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

                If Not IO.Directory.Exists(My.PATH_CursorsWP) Then IO.Directory.CreateDirectory(My.PATH_CursorsWP)
                Dim fs As New IO.FileStream(String.Format(My.PATH_CursorsWP & "\{0}_{1}x.ani", CurName, i), IO.FileMode.Create)

                Dim AN As New EOANIWriter(fs, Count, Speed, frameRates, seqNums, Nothing, Nothing, HotPoint)

                For ix = 0 To Count - 1
                    AN.WriteFrame32(BMPList(ix))
                Next

                If [TreeView] IsNot Nothing Then AddNode([TreeView], String.Format(My.Lang.Verbose_CursorRenderedInto, String.Format(My.PATH_CursorsWP & "\{0}_{1}x.ani", CurName, i)), "info")

                fs.Close()
            Next

        End If

    End Sub

    Sub ApplyCursorsToReg(Optional scopeReg As String = "HKEY_CURRENT_USER", Optional [TreeView] As TreeView = Nothing)
        Dim Path As String = My.PATH_CursorsWP

        Dim RegValue As String
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

        EditReg(scopeReg & "\Control Panel\Cursors\Schemes", "WinPaletter", RegValue, RegistryValueKind.String)
        EditReg(scopeReg & "\Control Panel\Cursors", "", "WinPaletter", RegistryValueKind.String)
        EditReg(scopeReg & "\Control Panel\Cursors", "CursorBaseSize", 32, RegistryValueKind.DWord)
        EditReg(scopeReg & "\Control Panel\Cursors", "Scheme Source", 1, RegistryValueKind.DWord)

        Dim x As String = String.Format("{0}\{1}", Path, "AppLoading_1x.ani")
        EditReg(scopeReg & "\Control Panel\Cursors", "AppStarting", x, RegistryValueKind.String)

        If [TreeView] IsNot Nothing Then AddNode([TreeView], String.Format(My.Lang.Verbose_SettingCursor, "APPSTARTING", x), "dll")
        User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_APPSTARTING)

        x = String.Format("{0}\{1}", Path, "Arrow.cur")
        EditReg(scopeReg & "\Control Panel\Cursors", "Arrow", x, RegistryValueKind.String)
        If [TreeView] IsNot Nothing Then AddNode([TreeView], String.Format(My.Lang.Verbose_SettingCursor, "NORMAL", x), "dll")
        User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_NORMAL)

        x = String.Format("{0}\{1}", Path, "Cross.cur")
        EditReg(scopeReg & "\Control Panel\Cursors", "Crosshair", x, RegistryValueKind.String)
        If [TreeView] IsNot Nothing Then AddNode([TreeView], String.Format(My.Lang.Verbose_SettingCursor, "CROSS", x), "dll")
        User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_CROSS)

        x = String.Format("{0}\{1}", Path, "Link.cur")
        EditReg(scopeReg & "\Control Panel\Cursors", "Hand", x, RegistryValueKind.String)
        If [TreeView] IsNot Nothing Then AddNode([TreeView], String.Format(My.Lang.Verbose_SettingCursor, "HAND", x), "dll")
        User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_HAND)

        x = String.Format("{0}\{1}", Path, "Help.cur")
        EditReg(scopeReg & "\Control Panel\Cursors", "Help", x, RegistryValueKind.String)
        If [TreeView] IsNot Nothing Then AddNode([TreeView], String.Format(My.Lang.Verbose_SettingCursor, "HELP", x), "dll")
        User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_HELP)

        x = String.Format("{0}\{1}", Path, "IBeam.cur")
        EditReg(scopeReg & "\Control Panel\Cursors", "IBeam", x, RegistryValueKind.String)
        If [TreeView] IsNot Nothing Then AddNode([TreeView], String.Format(My.Lang.Verbose_SettingCursor, "IBEAM", x), "dll")
        User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_IBEAM)

        x = String.Format("{0}\{1}", Path, "None.cur")
        EditReg(scopeReg & "\Control Panel\Cursors", "No", x, RegistryValueKind.String)
        If [TreeView] IsNot Nothing Then AddNode([TreeView], String.Format(My.Lang.Verbose_SettingCursor, "NO", x), "dll")
        User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_NO)

        x = String.Format("{0}\{1}", Path, "Pen.cur")
        EditReg(scopeReg & "\Control Panel\Cursors", "NWPen", x, RegistryValueKind.String)
        'User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_)

        x = String.Format("{0}\{1}", Path, "Person.cur")
        EditReg(scopeReg & "\Control Panel\Cursors", "Person", x, RegistryValueKind.String)
        'User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_APPSTARTING)

        x = String.Format("{0}\{1}", Path, "Pin.cur")
        EditReg(scopeReg & "\Control Panel\Cursors", "Pin", x, RegistryValueKind.String)
        'User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_APPSTARTING)

        x = String.Format("{0}\{1}", Path, "Move.cur")
        EditReg(scopeReg & "\Control Panel\Cursors", "SizeAll", x, RegistryValueKind.String)
        If [TreeView] IsNot Nothing Then AddNode([TreeView], String.Format(My.Lang.Verbose_SettingCursor, "SIZEALL", x), "dll")
        User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_SIZEALL)

        x = String.Format("{0}\{1}", Path, "NESW.cur")
        EditReg(scopeReg & "\Control Panel\Cursors", "SizeNESW", x, RegistryValueKind.String)
        If [TreeView] IsNot Nothing Then AddNode([TreeView], String.Format(My.Lang.Verbose_SettingCursor, "SIZENESW", x), "dll")
        User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_SIZENESW)

        x = String.Format("{0}\{1}", Path, "NS.cur")
        EditReg(scopeReg & "\Control Panel\Cursors", "SizeNS", x, RegistryValueKind.String)
        If [TreeView] IsNot Nothing Then AddNode([TreeView], String.Format(My.Lang.Verbose_SettingCursor, "SIZENS", x), "dll")
        User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_SIZENS)

        x = String.Format("{0}\{1}", Path, "NWSE.cur")
        EditReg(scopeReg & "\Control Panel\Cursors", "SizeNWSE", x, RegistryValueKind.String)
        If [TreeView] IsNot Nothing Then AddNode([TreeView], String.Format(My.Lang.Verbose_SettingCursor, "SIZENWSE", x), "dll")
        User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_SIZENWSE)

        x = String.Format("{0}\{1}", Path, "EW.cur")
        EditReg(scopeReg & "\Control Panel\Cursors", "SizeWE", x, RegistryValueKind.String)
        If [TreeView] IsNot Nothing Then AddNode([TreeView], String.Format(My.Lang.Verbose_SettingCursor, "SIZEWE", x), "dll")
        User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_SIZEWE)

        x = String.Format("{0}\{1}", Path, "Up.cur")
        EditReg(scopeReg & "\Control Panel\Cursors", "UpArrow", x, RegistryValueKind.String)
        If [TreeView] IsNot Nothing Then AddNode([TreeView], String.Format(My.Lang.Verbose_SettingCursor, "UP", x), "dll")
        User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_UP)

        x = String.Format("{0}\{1}", Path, "Busy_1x.ani")
        EditReg(scopeReg & "\Control Panel\Cursors", "Wait", x, RegistryValueKind.String)
        If [TreeView] IsNot Nothing Then AddNode([TreeView], String.Format(My.Lang.Verbose_SettingCursor, "WAIT", x), "dll")
        User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_WAIT)

        If [TreeView] IsNot Nothing Then AddNode([TreeView], My.Lang.Verbose_RefreshingCursors, "dll")
        SystemParametersInfo(SPI.Cursors.SETCURSORS, 0, 0, SPIF.UpdateINIFile Or SPIF.UpdateINIFile)
    End Sub

    Shared Sub ResetCursorsToAero(Optional scopeReg As String = "HKEY_CURRENT_USER", Optional [TreeView] As TreeView = Nothing)
        Try
            Dim path As String = "%SystemRoot%\Cursors"

            If scopeReg.ToUpper = "HKEY_CURRENT_USER" Then
                If Registry.CurrentUser.OpenSubKey("Control Panel\Cursors\Schemes", False) IsNot Nothing Then
                    If [TreeView] IsNot Nothing Then AddNode([TreeView], String.Format(My.Lang.Verbose_DelCursorWPFromReg, "HKEY_CURRENT_USER\Control Panel\Cursors\Schemes"), "reg_delete")
                    Dim rx As RegistryKey = Registry.CurrentUser.OpenSubKey("Control Panel\Cursors\Schemes", True)
                    rx.DeleteValue("WinPaletter", False)
                    rx.Close()
                End If
            End If

            EditReg(scopeReg & "\Control Panel\Cursors", "", "Windows Default", RegistryValueKind.String)
            EditReg(scopeReg & "\Control Panel\Cursors", "CursorBaseSize", 32, RegistryValueKind.DWord)
            EditReg(scopeReg & "\Control Panel\Cursors", "Scheme Source", 2, RegistryValueKind.DWord)

            Dim x As String = String.Format("{0}\{1}", path, "aero_working.ani")
            EditReg(scopeReg & "\Control Panel\Cursors", "AppStarting", x, RegistryValueKind.String)
            If IO.File.Exists(x) Then
                If [TreeView] IsNot Nothing Then AddNode([TreeView], String.Format(My.Lang.Verbose_SettingCursor, "APPSTARTING", x), "dll")
                User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_APPSTARTING)
            End If

            x = String.Format("{0}\{1}", path, "aero_arrow.cur")
            EditReg(scopeReg & "\Control Panel\Cursors", "Arrow", x, RegistryValueKind.String)
            If IO.File.Exists(x) Then
                If [TreeView] IsNot Nothing Then AddNode([TreeView], String.Format(My.Lang.Verbose_SettingCursor, "NORMAL", x), "dll")
                User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_NORMAL)
            End If

            x = String.Format("")
            EditReg(scopeReg & "\Control Panel\Cursors", "Crosshair", x, RegistryValueKind.String)
            If IO.File.Exists(x) Then
                If [TreeView] IsNot Nothing Then AddNode([TreeView], String.Format(My.Lang.Verbose_SettingCursor, "CROSS", x), "dll")
                User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_CROSS)
            End If

            x = String.Format("{0}\{1}", path, "aero_link.cur")
            EditReg(scopeReg & "\Control Panel\Cursors", "Hand", x, RegistryValueKind.String)
            If IO.File.Exists(x) Then
                If [TreeView] IsNot Nothing Then AddNode([TreeView], String.Format(My.Lang.Verbose_SettingCursor, "HAND", x), "dll")
                User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_HAND)
            End If

            x = String.Format("{0}\{1}", path, "aero_helpsel.cur")
            EditReg(scopeReg & "\Control Panel\Cursors", "Help", x, RegistryValueKind.String)
            If IO.File.Exists(x) Then
                If [TreeView] IsNot Nothing Then AddNode([TreeView], String.Format(My.Lang.Verbose_SettingCursor, "HELP", x), "dll")
                User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_HELP)
            End If

            x = String.Format("")
            EditReg(scopeReg & "\Control Panel\Cursors", "IBeam", x, RegistryValueKind.String)
            If IO.File.Exists(x) Then
                If [TreeView] IsNot Nothing Then AddNode([TreeView], String.Format(My.Lang.Verbose_SettingCursor, "IBEAM", x), "dll")
                User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_IBEAM)
            End If

            x = String.Format("{0}\{1}", path, "aero_unavail.cur")
            EditReg(scopeReg & "\Control Panel\Cursors", "No", x, RegistryValueKind.String)
            If IO.File.Exists(x) Then
                If [TreeView] IsNot Nothing Then AddNode([TreeView], String.Format(My.Lang.Verbose_SettingCursor, "NO", x), "dll")
                User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_NO)
            End If

            x = String.Format("{0}\{1}", path, "aero_pen.cur")
            EditReg(scopeReg & "\Control Panel\Cursors", "NWPen", x, RegistryValueKind.String)
            'If IO.File.Exists(X) then User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_)

            x = String.Format("{0}\{1}", path, "aero_person.cur")
            EditReg(scopeReg & "\Control Panel\Cursors", "Person", x, RegistryValueKind.String)
            'If IO.File.Exists(X) then User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_APPSTARTING)

            x = String.Format("{0}\{1}", path, "aero_pin.cur")
            EditReg(scopeReg & "\Control Panel\Cursors", "Pin", x, RegistryValueKind.String)
            'If IO.File.Exists(X) then User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_APPSTARTING)

            x = String.Format("{0}\{1}", path, "aero_move.cur")
            EditReg(scopeReg & "\Control Panel\Cursors", "SizeAll", x, RegistryValueKind.String)
            If IO.File.Exists(x) Then
                If [TreeView] IsNot Nothing Then AddNode([TreeView], String.Format(My.Lang.Verbose_SettingCursor, "SIZEALL", x), "dll")
                User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_SIZEALL)
            End If

            x = String.Format("{0}\{1}", path, "aero_nesw.cur")
            EditReg(scopeReg & "\Control Panel\Cursors", "SizeNESW", x, RegistryValueKind.String)
            If IO.File.Exists(x) Then
                If [TreeView] IsNot Nothing Then AddNode([TreeView], String.Format(My.Lang.Verbose_SettingCursor, "SIZENESW", x), "dll")
                User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_SIZENESW)
            End If

            x = String.Format("{0}\{1}", path, "aero_ns.cur")
            EditReg(scopeReg & "\Control Panel\Cursors", "SizeNS", x, RegistryValueKind.String)
            If IO.File.Exists(x) Then
                If [TreeView] IsNot Nothing Then AddNode([TreeView], String.Format(My.Lang.Verbose_SettingCursor, "SIZENS", x), "dll")
                User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_SIZENS)
            End If

            x = String.Format("{0}\{1}", path, "aero_nwse.cur")
            EditReg(scopeReg & "\Control Panel\Cursors", "SizeNWSE", x, RegistryValueKind.String)
            If IO.File.Exists(x) Then
                If [TreeView] IsNot Nothing Then AddNode([TreeView], String.Format(My.Lang.Verbose_SettingCursor, "SIZENWSE", x), "dll")
                User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_SIZENWSE)
            End If

            x = String.Format("{0}\{1}", path, "aero_ew.cur")
            EditReg(scopeReg & "\Control Panel\Cursors", "SizeWE", x, RegistryValueKind.String)
            If IO.File.Exists(x) Then
                If [TreeView] IsNot Nothing Then AddNode([TreeView], String.Format(My.Lang.Verbose_SettingCursor, "SIZEWE", x), "dll")
                User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_SIZEWE)
            End If

            x = String.Format("{0}\{1}", path, "aero_up.cur")
            EditReg(scopeReg & "\Control Panel\Cursors", "UpArrow", x, RegistryValueKind.String)
            If IO.File.Exists(x) Then
                If [TreeView] IsNot Nothing Then AddNode([TreeView], String.Format(My.Lang.Verbose_SettingCursor, "UP", x), "dll")
                User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_UP)
            End If

            x = String.Format("{0}\{1}", path, "aero_busy.ani")
            EditReg(scopeReg & "\Control Panel\Cursors", "Wait", x, RegistryValueKind.String)
            If IO.File.Exists(x) Then
                If [TreeView] IsNot Nothing Then AddNode([TreeView], String.Format(My.Lang.Verbose_SettingCursor, "WAIT", x), "dll")
                User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_WAIT)
            End If

            If [TreeView] IsNot Nothing Then AddNode([TreeView], My.Lang.Verbose_RefreshingCursors, "dll")
            SystemParametersInfo(SPI.Cursors.SETCURSORS, 0, 0, SPIF.UpdateINIFile Or SPIF.UpdateINIFile)

        Catch ex As Exception

            If MsgBox(My.Lang.CP_RestoreCursorsError, MsgBoxStyle.Exclamation + MsgBoxStyle.OkCancel, My.Lang.CP_RestoreCursorsErrorPressOK,
                     "", "", "", "", My.Lang.CP_RestoreCursorsTip, Ookii.Dialogs.WinForms.TaskDialogIcon.Information) = MsgBoxResult.Ok Then BugReport.ThrowError(ex)

        End Try

    End Sub

    Shared Sub ResetCursorsToNone_XP(Optional scopeReg As String = "HKEY_CURRENT_USER", Optional [TreeView] As TreeView = Nothing)
        Try
            Dim path As String = "%SystemRoot%\Cursors"

            If scopeReg.ToUpper = "HKEY_CURRENT_USER" Then
                Try
                    If Registry.CurrentUser.OpenSubKey("Control Panel\Cursors\Schemes", False) IsNot Nothing Then
                        If [TreeView] IsNot Nothing Then AddNode([TreeView], String.Format(My.Lang.Verbose_DelCursorWPFromReg, "HKEY_CURRENT_USER\Control Panel\Cursors\Schemes"), "reg_delete")
                        Dim rx As RegistryKey = Registry.CurrentUser.OpenSubKey("Control Panel\Cursors\Schemes", True)
                        rx.DeleteValue("WinPaletter", False)
                        rx.Close()
                    End If
                Finally
                    Registry.CurrentUser.Close()
                End Try
            End If

            EditReg(scopeReg & "\Control Panel\Cursors", "", "Windows Default", RegistryValueKind.String)
            EditReg(scopeReg & "\Control Panel\Cursors", "CursorBaseSize", 32, RegistryValueKind.DWord)
            EditReg(scopeReg & "\Control Panel\Cursors", "Scheme Source", 2, RegistryValueKind.DWord)

            Dim x As String = ""
            EditReg(scopeReg & "\Control Panel\Cursors", "AppStarting", x, RegistryValueKind.String)
            If [TreeView] IsNot Nothing Then AddNode([TreeView], String.Format(My.Lang.Verbose_SettingCursor, "APPSTARTING", x), "dll")
            User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_APPSTARTING)

            EditReg(scopeReg & "\Control Panel\Cursors", "Arrow", x, RegistryValueKind.String)
            If [TreeView] IsNot Nothing Then AddNode([TreeView], String.Format(My.Lang.Verbose_SettingCursor, "NORMAL", x), "dll")
            User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_NORMAL)

            EditReg(scopeReg & "\Control Panel\Cursors", "Crosshair", x, RegistryValueKind.String)
            If [TreeView] IsNot Nothing Then AddNode([TreeView], String.Format(My.Lang.Verbose_SettingCursor, "CROSS", x), "dll")
            User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_CROSS)

            EditReg(scopeReg & "\Control Panel\Cursors", "Hand", x, RegistryValueKind.String)
            If [TreeView] IsNot Nothing Then AddNode([TreeView], String.Format(My.Lang.Verbose_SettingCursor, "HAND", x), "dll")
            User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_HAND)

            EditReg(scopeReg & "\Control Panel\Cursors", "Help", x, RegistryValueKind.String)
            If [TreeView] IsNot Nothing Then AddNode([TreeView], String.Format(My.Lang.Verbose_SettingCursor, "HELP", x), "dll")
            User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_HELP)

            EditReg(scopeReg & "\Control Panel\Cursors", "IBeam", x, RegistryValueKind.String)
            If [TreeView] IsNot Nothing Then AddNode([TreeView], String.Format(My.Lang.Verbose_SettingCursor, "IBEAM", x), "dll")
            User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_IBEAM)

            EditReg(scopeReg & "\Control Panel\Cursors", "No", x, RegistryValueKind.String)
            If [TreeView] IsNot Nothing Then AddNode([TreeView], String.Format(My.Lang.Verbose_SettingCursor, "NO", x), "dll")
            User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_NO)

            EditReg(scopeReg & "\Control Panel\Cursors", "SizeAll", x, RegistryValueKind.String)
            If [TreeView] IsNot Nothing Then AddNode([TreeView], String.Format(My.Lang.Verbose_SettingCursor, "SIZEALL", x), "dll")
            User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_SIZEALL)

            EditReg(scopeReg & "\Control Panel\Cursors", "SizeNESW", x, RegistryValueKind.String)
            If [TreeView] IsNot Nothing Then AddNode([TreeView], String.Format(My.Lang.Verbose_SettingCursor, "SIZENESW", x), "dll")
            User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_SIZENESW)

            EditReg(scopeReg & "\Control Panel\Cursors", "SizeNS", x, RegistryValueKind.String)
            If [TreeView] IsNot Nothing Then AddNode([TreeView], String.Format(My.Lang.Verbose_SettingCursor, "SIZENS", x), "dll")
            User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_SIZENS)

            EditReg(scopeReg & "\Control Panel\Cursors", "SizeNWSE", x, RegistryValueKind.String)
            If [TreeView] IsNot Nothing Then AddNode([TreeView], String.Format(My.Lang.Verbose_SettingCursor, "SIZENWSE", x), "dll")
            User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_SIZENWSE)

            EditReg(scopeReg & "\Control Panel\Cursors", "SizeWE", x, RegistryValueKind.String)
            If [TreeView] IsNot Nothing Then AddNode([TreeView], String.Format(My.Lang.Verbose_SettingCursor, "SIZEWE", x), "dll")
            User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_SIZEWE)

            EditReg(scopeReg & "\Control Panel\Cursors", "UpArrow", x, RegistryValueKind.String)
            If [TreeView] IsNot Nothing Then AddNode([TreeView], String.Format(My.Lang.Verbose_SettingCursor, "UP", x), "dll")
            User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_UP)

            EditReg(scopeReg & "\Control Panel\Cursors", "Wait", x, RegistryValueKind.String)
            If [TreeView] IsNot Nothing Then AddNode([TreeView], String.Format(My.Lang.Verbose_SettingCursor, "WAIT", x), "dll")
            User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_WAIT)

            If [TreeView] IsNot Nothing Then AddNode([TreeView], My.Lang.Verbose_RefreshingCursors, "dll")
            SystemParametersInfo(SPI.Cursors.SETCURSORS, 0, 0, SPIF.UpdateINIFile Or SPIF.UpdateINIFile)

        Catch ex As Exception

            If MsgBox(My.Lang.CP_RestoreCursorsError, MsgBoxStyle.Exclamation + MsgBoxStyle.OkCancel, My.Lang.CP_RestoreCursorsErrorPressOK,
                     "", "", "", "", My.Lang.CP_RestoreCursorsTip, Ookii.Dialogs.WinForms.TaskDialogIcon.Information) = MsgBoxResult.Ok Then BugReport.ThrowError(ex)

        End Try

    End Sub
#End Region

#Region "Comparisons"
    Public Overrides Function Equals(obj As Object) As Boolean
        Dim _Equals As Boolean = True

        If Info <> DirectCast(obj, CP).Info Then _Equals = False
        If Windows11 <> DirectCast(obj, CP).Windows11 Then _Equals = False
        If LogonUI10x <> DirectCast(obj, CP).LogonUI10x Then _Equals = False
        If Windows81 <> DirectCast(obj, CP).Windows81 Then _Equals = False
        If Windows7 <> DirectCast(obj, CP).Windows7 Then _Equals = False
        If WindowsVista <> DirectCast(obj, CP).WindowsVista Then _Equals = False
        If WindowsXP <> DirectCast(obj, CP).WindowsXP Then _Equals = False
        If LogonUI7 <> DirectCast(obj, CP).LogonUI7 Then _Equals = False
        If LogonUIXP <> DirectCast(obj, CP).LogonUIXP Then _Equals = False
        If Win32 <> DirectCast(obj, CP).Win32 Then _Equals = False
        If WindowsEffects <> DirectCast(obj, CP).WindowsEffects Then _Equals = False
        If MetricsFonts <> DirectCast(obj, CP).MetricsFonts Then _Equals = False
        If AltTab <> DirectCast(obj, CP).AltTab Then _Equals = False
        If WallpaperTone_W11 <> DirectCast(obj, CP).WallpaperTone_W11 Then _Equals = False
        If WallpaperTone_W10 <> DirectCast(obj, CP).WallpaperTone_W10 Then _Equals = False
        If WallpaperTone_W81 <> DirectCast(obj, CP).WallpaperTone_W81 Then _Equals = False
        If WallpaperTone_W7 <> DirectCast(obj, CP).WallpaperTone_W7 Then _Equals = False
        If WallpaperTone_WVista <> DirectCast(obj, CP).WallpaperTone_WVista Then _Equals = False
        If WallpaperTone_WXP <> DirectCast(obj, CP).WallpaperTone_WXP Then _Equals = False
        If ScreenSaver <> DirectCast(obj, CP).ScreenSaver Then _Equals = False
        If Sounds <> DirectCast(obj, CP).Sounds Then _Equals = False
        If Wallpaper <> DirectCast(obj, CP).Wallpaper Then _Equals = False
        If AppTheme <> DirectCast(obj, CP).AppTheme Then _Equals = False

        If Cursor_Enabled <> DirectCast(obj, CP).Cursor_Enabled Then _Equals = False
        If Cursor_Arrow <> DirectCast(obj, CP).Cursor_Arrow Then _Equals = False
        If Cursor_Help <> DirectCast(obj, CP).Cursor_Help Then _Equals = False
        If Cursor_AppLoading <> DirectCast(obj, CP).Cursor_AppLoading Then _Equals = False
        If Cursor_Busy <> DirectCast(obj, CP).Cursor_Busy Then _Equals = False
        If Cursor_Move <> DirectCast(obj, CP).Cursor_Move Then _Equals = False
        If Cursor_NS <> DirectCast(obj, CP).Cursor_NS Then _Equals = False
        If Cursor_EW <> DirectCast(obj, CP).Cursor_EW Then _Equals = False
        If Cursor_NESW <> DirectCast(obj, CP).Cursor_NESW Then _Equals = False
        If Cursor_NWSE <> DirectCast(obj, CP).Cursor_NWSE Then _Equals = False
        If Cursor_Up <> DirectCast(obj, CP).Cursor_Up Then _Equals = False
        If Cursor_Pen <> DirectCast(obj, CP).Cursor_Pen Then _Equals = False
        If Cursor_None <> DirectCast(obj, CP).Cursor_None Then _Equals = False
        If Cursor_Link <> DirectCast(obj, CP).Cursor_Link Then _Equals = False
        If Cursor_Pin <> DirectCast(obj, CP).Cursor_Pin Then _Equals = False
        If Cursor_Person <> DirectCast(obj, CP).Cursor_Person Then _Equals = False
        If Cursor_IBeam <> DirectCast(obj, CP).Cursor_IBeam Then _Equals = False
        If Cursor_Cross <> DirectCast(obj, CP).Cursor_Cross Then _Equals = False

        If CommandPrompt <> DirectCast(obj, CP).CommandPrompt Then _Equals = False
        If PowerShellx86 <> DirectCast(obj, CP).PowerShellx86 Then _Equals = False
        If PowerShellx64 <> DirectCast(obj, CP).PowerShellx64 Then _Equals = False
        'If Terminal <> DirectCast(obj, CP).Terminal Then _Equals = False
        'If TerminalPreview <> DirectCast(obj, CP).TerminalPreview Then _Equals = False

        Return _Equals
    End Function

    Public Shared Operator =(First As CP, Second As CP)
        Return First.Equals(Second)
    End Operator

    Public Shared Operator <>(First As CP, Second As CP)
        Return Not First = Second
    End Operator
#End Region

End Class