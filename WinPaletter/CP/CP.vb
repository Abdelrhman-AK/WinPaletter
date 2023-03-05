Imports System.Reflection
Imports System.Runtime.InteropServices
Imports Microsoft.Win32
Imports WinPaletter.Metrics
Imports WinPaletter.XenonCore
Imports WinPaletter.NativeMethods
Imports WinPaletter.NativeMethods.User32
Imports Devcorp.Controls.VisualStyles

Public Class CP : Implements IDisposable : Implements ICloneable

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

    Public Function Clone() Implements ICloneable.Clone
        Return MemberwiseClone()
    End Function

#Region "Enumerations"
    Enum ApplyAccentonTaskbar_Level
        None
        Taskbar_Start_AC
        Taskbar
    End Enum
    Enum AeroTheme
        Aero
        AeroLite
        AeroOpaque
        Basic
        Classic
    End Enum
    Enum LogonUI_Modes
        Default_
        Wallpaper
        CustomImage
        SolidColor
    End Enum
    Enum LogonUIXP_Modes
        Win2000
        [Default]
    End Enum
    Enum CP_Type
        Registry
        File
        Empty
    End Enum
    Enum WinXPTheme
        LunaBlue
        LunaOliveGreen
        LunaSilver
        Classic
        Custom
    End Enum
    Enum MenuAnimType
        Fade
        Scroll
    End Enum
#End Region

    Public Class Structures
        Structure Info : Implements ICloneable
            Public AppVersion As String
            Public PaletteName As String
            Public PaletteDescription As String
            Public PaletteVersion As String
            Public Author As String
            Public AuthorSocialMediaLink As String

            Shared Operator =(First As Info, Second As Info) As Boolean
                Return First.Equals(Second)
            End Operator

            Shared Operator <>(First As Info, Second As Info) As Boolean
                Return Not First.Equals(Second)
            End Operator

            Public Function Clone() Implements ICloneable.Clone
                Return MemberwiseClone()
            End Function

            Public Sub Load()
                Author = Environment.UserName
                AppVersion = My.Application.Info.Version.ToString
                PaletteVersion = "1.0"
                PaletteName = My.Lang.CurrentMode
            End Sub

            Public Overrides Function ToString() As String
                Dim tx As New List(Of String)
                tx.Clear()
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
                Return tx.CString
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
            Public ApplyAccentonTitlebars As Boolean
            Public ApplyAccentonTaskbar As ApplyAccentonTaskbar_Level

            Sub Load(_DefWin As Windows10x, DefColorsBytes As Byte())
                If My.W10 Or My.W11 Then
                    Dim Colors As New List(Of Color)
                    Colors.Clear()

                    Dim x As Byte()
                    Dim y As Object

                    Try
                        x = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "AccentPalette", DefColorsBytes)
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

                    Catch
                        x = DefColorsBytes

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
                    End Try

                    Try
                        y = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "StartColorMenu", _DefWin.StartMenu_Accent.Reverse.ToArgb)
                        StartMenu_Accent = Color.FromArgb(y).Reverse
                    Catch
                        StartMenu_Accent = _DefWin.StartMenu_Accent
                    End Try

                    Try
                        y = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "AccentColorMenu", _DefWin.Titlebar_Active.Reverse.ToArgb)
                        Titlebar_Active = Color.FromArgb(y).Reverse
                    Catch
                        Titlebar_Active = _DefWin.Titlebar_Active
                    End Try

                    Try
                        y = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\DWM", "AccentColor", _DefWin.Titlebar_Active.Reverse.ToArgb)
                        Titlebar_Active = Color.FromArgb(y).Reverse
                    Catch
                        Titlebar_Active = _DefWin.Titlebar_Active
                    End Try

                    Try
                        y = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\DWM", "AccentColorInactive", _DefWin.Titlebar_Inactive.Reverse.ToArgb)
                        Titlebar_Inactive = Color.FromArgb(y).Reverse
                    Catch
                        Titlebar_Inactive = _DefWin.Titlebar_Inactive
                    End Try

                    Try
                        WinMode_Light = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "SystemUsesLightTheme", _DefWin.WinMode_Light)
                    Catch
                        WinMode_Light = _DefWin.WinMode_Light
                    End Try

                    Try
                        AppMode_Light = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "AppsUseLightTheme", _DefWin.AppMode_Light)
                    Catch
                        AppMode_Light = _DefWin.AppMode_Light
                    End Try

                    Try
                        Transparency = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "EnableTransparency", _DefWin.Transparency)
                    Catch
                        Transparency = _DefWin.Transparency
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
                        ApplyAccentonTaskbar = _DefWin.ApplyAccentonTaskbar
                    End Try

                    Try
                        ApplyAccentonTitlebars = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\DWM", "ColorPrevalence", _DefWin.ApplyAccentonTitlebars)
                    Catch
                        ApplyAccentonTitlebars = _DefWin.ApplyAccentonTitlebars
                    End Try

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
                    ApplyAccentonTaskbar = _DefWin.ApplyAccentonTaskbar
                    ApplyAccentonTitlebars = _DefWin.ApplyAccentonTitlebars
                End If

            End Sub

            Sub Apply()
                EditReg("HKEY_CURRENT_USER\Control Panel\Desktop", "AutoColorization", 0)

                Dim Colors As Byte() = {Color_Index0.R, (Color_Index0).G, (Color_Index0).B, (Color_Index0).A _
                         , (Color_Index1).R, (Color_Index1).G, (Color_Index1).B, (Color_Index1).A _
                         , (Color_Index2).R, (Color_Index2).G, (Color_Index2).B, (Color_Index2).A _
                         , (Color_Index3).R, (Color_Index3).G, (Color_Index3).B, (Color_Index3).A _
                         , (Color_Index4).R, (Color_Index4).G, (Color_Index4).B, (Color_Index4).A _
                         , (Color_Index5).R, (Color_Index5).G, (Color_Index5).B, (Color_Index5).A _
                         , (Color_Index6).R, (Color_Index6).G, (Color_Index6).B, (Color_Index6).A _
                         , (Color_Index7).R, (Color_Index7).G, (Color_Index7).B, (Color_Index7).A}

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
                EditReg("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\DWM", "ColorPrevalence", ApplyAccentonTitlebars.ToInteger)
                EditReg("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "AccentPalette", Colors, RegistryValueKind.Binary)
                EditReg("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "StartColorMenu", StartMenu_Accent.Reverse.ToArgb)
                EditReg("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\DWM", "AccentColor", Titlebar_Active.ToArgb)


                EditReg("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "AccentColorMenu", Titlebar_Active.Reverse.ToArgb)
                EditReg("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\DWM", "AccentColor", Titlebar_Active.Reverse.ToArgb)
                EditReg("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\DWM", "AccentColorInactive", Titlebar_Inactive.Reverse.ToArgb)

                EditReg("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "SystemUsesLightTheme", WinMode_Light.ToInteger)
                EditReg("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "AppsUseLightTheme", AppMode_Light.ToInteger)
                EditReg("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "EnableTransparency", Transparency.ToInteger)

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

            Public Overloads Function ToString(Signature As String, MiniSignature As String) As String
                Dim tx As New List(Of String)
                tx.Clear()
                tx.Add(String.Format("<{0}>", Signature))
                tx.Add(String.Format("*{0}_Color_Index0= {1}", MiniSignature, Color_Index0.ToArgb))
                tx.Add(String.Format("*{0}_Color_Index1= {1}", MiniSignature, Color_Index1.ToArgb))
                tx.Add(String.Format("*{0}_Color_Index2= {1}", MiniSignature, Color_Index2.ToArgb))
                tx.Add(String.Format("*{0}_Color_Index3= {1}", MiniSignature, Color_Index3.ToArgb))
                tx.Add(String.Format("*{0}_Color_Index4= {1}", MiniSignature, Color_Index4.ToArgb))
                tx.Add(String.Format("*{0}_Color_Index5= {1}", MiniSignature, Color_Index5.ToArgb))
                tx.Add(String.Format("*{0}_Color_Index6= {1}", MiniSignature, Color_Index6.ToArgb))
                tx.Add(String.Format("*{0}_Color_Index7= {1}", MiniSignature, Color_Index7.ToArgb))
                tx.Add(String.Format("*{0}_Titlebar_Active= {1}", MiniSignature, Titlebar_Active.ToArgb))
                tx.Add(String.Format("*{0}_Titlebar_Inactive= {1}", MiniSignature, Titlebar_Inactive.ToArgb))
                tx.Add(String.Format("*{0}_StartMenu_Accent= {1}", MiniSignature, StartMenu_Accent.ToArgb))
                tx.Add(String.Format("*{0}_WinMode_Light= {1}", MiniSignature, WinMode_Light))
                tx.Add(String.Format("*{0}_AppMode_Light= {1}", MiniSignature, AppMode_Light))
                tx.Add(String.Format("*{0}_Transparency= {1}", MiniSignature, Transparency))
                tx.Add(String.Format("*{0}_ApplyAccentonTitlebars= {1}", MiniSignature, ApplyAccentonTitlebars))
                tx.Add(String.Format("*{0}_AccentOnStartTBAC= {1}", MiniSignature, ApplyAccentonTaskbar))
                tx.Add(String.Format("</{0}>" & vbCrLf, Signature))
                Return tx.CString
            End Function
        End Structure

        Structure Windows8 : Implements ICloneable
            Public Start As Integer
            Public ColorizationColor As Color
            Public ColorizationColorBalance As Integer
            Public StartColor As Color
            Public AccentColor As Color
            Public Theme As AeroTheme
            Public LogonUI As Integer
            Public PersonalColors_Background As Color
            Public PersonalColors_Accent As Color
            Public NoLockScreen As Boolean
            Public LockScreenType As LogonUI_Modes
            Public LockScreenSystemID As Integer

            Shared Operator =(First As Windows8, Second As Windows8) As Boolean
                Return First.Equals(Second)
            End Operator

            Shared Operator <>(First As Windows8, Second As Windows8) As Boolean
                Return Not First.Equals(Second)
            End Operator
            Public Function Clone() Implements ICloneable.Clone
                Return MemberwiseClone()
            End Function

            Public Sub Load(_DefWin As Windows8)
                If My.W8 Then
                    Dim y As Object

                    Dim stringThemeName As New System.Text.StringBuilder(260)
                    Uxtheme.GetCurrentThemeName(stringThemeName, 260, Nothing, 0, Nothing, 0)

                    If stringThemeName.ToString.Split("\").Last.ToLower = "aerolite.msstyles" Or String.IsNullOrWhiteSpace(stringThemeName.ToString) Then
                        Theme = AeroTheme.AeroLite
                    Else
                        Theme = AeroTheme.Aero
                    End If

                    Try
                        y = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationColor", _DefWin.ColorizationColor.ToArgb)
                        ColorizationColor = Color.FromArgb(255, Color.FromArgb(y))
                    Catch
                        ColorizationColor = _DefWin.ColorizationColor
                    End Try

                    Try
                        y = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationColorBalance", _DefWin.ColorizationColorBalance)
                        ColorizationColorBalance = y
                    Catch
                        ColorizationColorBalance = _DefWin.ColorizationColorBalance
                    End Try

                    Try
                        y = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "StartColor", Color.FromArgb(84, 0, 30).ToArgb)
                        StartColor = Color.FromArgb(255, Color.FromArgb(y)).Reverse
                    Catch
                        StartColor = Color.FromArgb(84, 0, 30)
                    End Try

                    Try
                        y = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "AccentColor", Color.FromArgb(178, 29, 72).ToArgb)
                        AccentColor = Color.FromArgb(255, Color.FromArgb(y)).Reverse
                    Catch
                        AccentColor = Color.FromArgb(178, 29, 72)
                    End Try

                    Dim S As String

                    Try
                        S = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Personalization", "PersonalColors_Background", "#1e0054")
                        PersonalColors_Background = S.FromHEXToColor
                    Catch
                        PersonalColors_Background = _DefWin.PersonalColors_Background
                    End Try

                    Try
                        S = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Personalization", "PersonalColors_Accent", "#481db2")
                        PersonalColors_Accent = S.FromHEXToColor
                    Catch
                        PersonalColors_Accent = _DefWin.PersonalColors_Accent
                    End Try

                    Try
                        Start = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Personalization", "ForceStartBackground", 0)
                    Catch
                        Start = 0
                    End Try

                    Try
                        LogonUI = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "DefaultColorSet", 0)
                    Catch
                        LogonUI = 0
                    End Try

                    Dim rLog As RegistryKey = Registry.CurrentUser.CreateSubKey("Software\WinPaletter\LogonUI")

                    Try
                        LockScreenType = rLog.GetValue("Mode", LogonUI_Modes.Default_)
                    Catch
                        LockScreenType = LogonUI_Modes.Default_
                    End Try

                    Try
                        LockScreenSystemID = rLog.GetValue("Metro_LockScreenSystemID", 0)
                    Catch
                        LockScreenSystemID = 0
                    End Try

                    rLog.Close()

                    Try
                        NoLockScreen = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\Policies\Microsoft\Windows\Personalization", "NoLockScreen", False)
                    Catch
                        NoLockScreen = False
                    End Try

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

            Public Sub Apply()
                EditReg("HKEY_CURRENT_USER\Control Panel\Desktop", "AutoColorization", 0)

                Try
                    Select Case Theme
                        Case AeroTheme.Aero
                            Uxtheme.EnableTheming(1)
                            Uxtheme.SetSystemVisualStyle(My.PATH_Windows & "\resources\Themes\Aero\Aero.msstyles", "NormalColor", "NormalSize", 0)
                        Case AeroTheme.AeroLite
                            Uxtheme.EnableTheming(1)
                            Uxtheme.SetSystemVisualStyle(My.PATH_Windows & "\resources\Themes\Aero\AeroLite.msstyles", "NormalColor", "NormalSize", 0)
                    End Select
                Catch
                End Try

                EditReg("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationColor", ColorizationColor.ToArgb)
                EditReg("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationColorBalance", ColorizationColorBalance)

                EditReg("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "StartColor", StartColor.Reverse.ToArgb)
                EditReg("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "DefaultStartColor", StartColor.Reverse.ToArgb)
                EditReg("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "AccentColor", AccentColor.Reverse.ToArgb)
                EditReg("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "DefaultColorSet", LogonUI)

                EditReg("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Personalization", "ForceStartBackground", Start)
                EditReg("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "DefaultColorSet", LogonUI)
                EditReg("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Personalization", "PersonalColors_Background", "#" & PersonalColors_Background.HEX(False), RegistryValueKind.String)
                EditReg("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Personalization", "PersonalColors_Accent", "#" & PersonalColors_Accent.HEX(False), RegistryValueKind.String)
            End Sub

            Public Overrides Function ToString() As String
                Dim tx As New List(Of String)
                tx.Clear()
                tx.Add("<Metro>")
                tx.Add("*Metro_ColorizationColor= " & ColorizationColor.ToArgb)
                tx.Add("*Metro_ColorizationColorBalance= " & ColorizationColorBalance)
                tx.Add("*Metro_PersonalColors_Background= " & PersonalColors_Background.ToArgb)
                tx.Add("*Metro_PersonalColors_Accent= " & PersonalColors_Accent.ToArgb)
                tx.Add("*Metro_StartColor= " & StartColor.ToArgb)
                tx.Add("*Metro_AccentColor= " & AccentColor.ToArgb)
                tx.Add("*Metro_Start= " & Start)
                tx.Add("*Metro_Theme= " & Theme)
                tx.Add("*Metro_LogonUI= " & LogonUI)
                tx.Add("*Metro_NoLockScreen= " & NoLockScreen)
                tx.Add("*Metro_LockScreenType= " & LockScreenType)
                tx.Add("*Metro_LockScreenSystemID= " & LockScreenSystemID)
                tx.Add("</Metro>" & vbCrLf)

                Return tx.CString
            End Function
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
            Public Theme As AeroTheme

            Shared Operator =(First As Windows7, Second As Windows7) As Boolean
                Return First.Equals(Second)
            End Operator

            Shared Operator <>(First As Windows7, Second As Windows7) As Boolean
                Return Not First.Equals(Second)
            End Operator

            Public Sub Load(_DefWin As Windows7)
                If My.W7 Or My.W8 Then
                    Dim y As Object

                    Try
                        y = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationColor", _DefWin.ColorizationColor.ToArgb)
                        ColorizationColor = Color.FromArgb(255, Color.FromArgb(y))
                    Catch
                        ColorizationColor = _DefWin.ColorizationColor
                    End Try

                    Try
                        y = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationColorBalance", _DefWin.ColorizationColorBalance)
                        ColorizationColorBalance = y
                    Catch
                        ColorizationColorBalance = _DefWin.ColorizationColorBalance
                    End Try

                    If Not My.W8 Then
                        Try
                            y = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationAfterglow", _DefWin.ColorizationAfterglow.ToArgb)
                            ColorizationAfterglow = Color.FromArgb(255, Color.FromArgb(y))
                        Catch
                            ColorizationAfterglow = _DefWin.ColorizationAfterglow
                        End Try

                        Try
                            y = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationAfterglowBalance", _DefWin.ColorizationAfterglowBalance)
                            ColorizationAfterglowBalance = y
                        Catch
                            ColorizationAfterglowBalance = _DefWin.ColorizationAfterglowBalance
                        End Try

                        Try
                            y = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationBlurBalance", _DefWin.ColorizationBlurBalance)
                            ColorizationBlurBalance = y
                        Catch
                            ColorizationBlurBalance = _DefWin.ColorizationBlurBalance
                        End Try

                        Try
                            y = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationGlassReflectionIntensity", _DefWin.ColorizationGlassReflectionIntensity)
                            ColorizationGlassReflectionIntensity = y
                        Catch
                            ColorizationGlassReflectionIntensity = _DefWin.ColorizationGlassReflectionIntensity
                        End Try

                        Dim Com, Opaque As Boolean
                        Dwmapi.DwmIsCompositionEnabled(Com)

                        Try
                            Opaque = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationOpaqueBlend", False)
                        Catch
                            Opaque = False
                        End Try

                        Dim Classic As Boolean = False

                        Try
                            Dim stringThemeName As New System.Text.StringBuilder(260)
                            Uxtheme.GetCurrentThemeName(stringThemeName, 260, Nothing, 0, Nothing, 0)
                            Classic = String.IsNullOrWhiteSpace(stringThemeName.ToString) Or Not IO.File.Exists(stringThemeName.ToString)
                        Catch
                            Classic = False
                        End Try

                        If Classic Then
                            Theme = AeroTheme.Classic
                        ElseIf Com Then
                            If Not Opaque Then Theme = AeroTheme.Aero Else Theme = AeroTheme.AeroOpaque
                        Else
                            Theme = AeroTheme.Basic
                        End If

                    End If

                    Try
                        EnableAeroPeek = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "EnableAeroPeek", _DefWin.EnableAeroPeek)
                    Catch
                        EnableAeroPeek = _DefWin.EnableAeroPeek
                    End Try

                    Try
                        AlwaysHibernateThumbnails = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "AlwaysHibernateThumbnails", _DefWin.AlwaysHibernateThumbnails)
                    Catch
                        AlwaysHibernateThumbnails = _DefWin.AlwaysHibernateThumbnails
                    End Try

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

            Public Sub Apply()
                Select Case Theme
                    Case AeroTheme.Aero
                        Uxtheme.EnableTheming(1)
                        Uxtheme.SetSystemVisualStyle(My.PATH_Windows & "\resources\Themes\Aero\Aero.msstyles", "NormalColor", "NormalSize", 0)

                        EditReg("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "CompositionPolicy", 2)
                        EditReg("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "Composition", 1)
                        EditReg("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationOpaqueBlend", 0)

                    Case AeroTheme.AeroOpaque
                        Uxtheme.EnableTheming(1)
                        Uxtheme.SetSystemVisualStyle(My.PATH_Windows & "\resources\Themes\Aero\Aero.msstyles", "NormalColor", "NormalSize", 0)

                        EditReg("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "CompositionPolicy", 2)
                        EditReg("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "Composition", 1)
                        EditReg("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationOpaqueBlend", 1)

                    Case AeroTheme.Basic
                        Uxtheme.EnableTheming(1)
                        Uxtheme.SetSystemVisualStyle(My.PATH_Windows & "\resources\Themes\Aero\Aero.msstyles", "NormalColor", "NormalSize", 0)

                        EditReg("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "CompositionPolicy", 1)
                        EditReg("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "Composition", 0)
                        EditReg("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationOpaqueBlend", 0)

                    Case AeroTheme.Classic
                        Uxtheme.EnableTheming(0)

                End Select

                EditReg("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationAfterglow", ColorizationAfterglow.ToArgb)
                EditReg("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationAfterglowBalance", ColorizationAfterglowBalance)
                EditReg("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationBlurBalance", ColorizationBlurBalance)
                EditReg("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationGlassReflectionIntensity", ColorizationGlassReflectionIntensity)

                EditReg("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationColor", ColorizationColor.ToArgb)
                EditReg("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationColorBalance", ColorizationColorBalance)

                EditReg("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "EnableAeroPeek", EnableAeroPeek.ToInteger)
                EditReg("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "AlwaysHibernateThumbnails", AlwaysHibernateThumbnails.ToInteger)
                EditReg("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "EnableWindowColorization", 1)
            End Sub

            Public Function Clone() Implements ICloneable.Clone
                Return MemberwiseClone()
            End Function

            Public Overrides Function ToString() As String
                Dim tx As New List(Of String)
                tx.Clear()
                tx.Add("<Aero>")
                tx.Add("*Aero_ColorizationColor= " & ColorizationColor.ToArgb)
                tx.Add("*Aero_ColorizationAfterglow= " & ColorizationAfterglow.ToArgb)
                tx.Add("*Aero_ColorizationColorBalance= " & ColorizationColorBalance)
                tx.Add("*Aero_ColorizationAfterglowBalance= " & ColorizationAfterglowBalance)
                tx.Add("*Aero_ColorizationBlurBalance= " & ColorizationBlurBalance)
                tx.Add("*Aero_ColorizationGlassReflectionIntensity= " & ColorizationGlassReflectionIntensity)
                tx.Add("*Aero_EnableAeroPeek= " & EnableAeroPeek)
                tx.Add("*Aero_AlwaysHibernateThumbnails= " & AlwaysHibernateThumbnails)
                tx.Add("*Aero_Theme= " & Theme)
                tx.Add("</Aero>" & vbCrLf)
                Return tx.CString
            End Function

        End Structure

        Structure WindowsVista : Implements ICloneable
            Public ColorizationColor As Color
            Public [Alpha] As Byte
            Public Theme As AeroTheme

            Shared Operator =(First As WindowsVista, Second As WindowsVista) As Boolean
                Return First.Equals(Second)
            End Operator

            Shared Operator <>(First As WindowsVista, Second As WindowsVista) As Boolean
                Return Not First.Equals(Second)
            End Operator

            Public Sub Load(_DefWin As WindowsVista)
                If My.WVista Then
                    Dim y As Object

                    Try
                        y = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationColor", _DefWin.ColorizationColor.ToArgb)
                        ColorizationColor = Color.FromArgb(255, Color.FromArgb(y))
                        Alpha = Color.FromArgb(y).A
                    Catch
                        ColorizationColor = _DefWin.ColorizationColor
                        Alpha = _DefWin.Alpha
                    End Try

                    Dim Com, Opaque As Boolean
                    Dwmapi.DwmIsCompositionEnabled(Com)

                    Try
                        Opaque = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationOpaqueBlend", False)
                    Catch
                        Opaque = False
                    End Try

                    Dim Classic As Boolean = False

                    Try
                        Dim stringThemeName As New System.Text.StringBuilder(260)
                        Uxtheme.GetCurrentThemeName(stringThemeName, 260, Nothing, 0, Nothing, 0)
                        Classic = String.IsNullOrWhiteSpace(stringThemeName.ToString) Or Not IO.File.Exists(stringThemeName.ToString)
                    Catch
                        Classic = False
                    End Try

                    If Classic Then
                        Theme = AeroTheme.Classic
                    ElseIf Com Then
                        If Not Opaque Then Theme = AeroTheme.Aero Else Theme = AeroTheme.AeroOpaque
                    Else
                        Theme = AeroTheme.Basic
                    End If

                Else
                    ColorizationColor = _DefWin.ColorizationColor
                    Alpha = _DefWin.Alpha
                    Theme = _DefWin.Theme
                End If
            End Sub

            Public Sub Apply()
                Select Case Theme
                    Case AeroTheme.Aero
                        Uxtheme.EnableTheming(1)
                        Uxtheme.SetSystemVisualStyle(My.PATH_Windows & "\resources\Themes\Aero\Aero.msstyles", "NormalColor", "NormalSize", 0)

                        EditReg("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "CompositionPolicy", 2)
                        EditReg("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "Composition", 1)
                        EditReg("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationOpaqueBlend", 0)

                    Case AeroTheme.AeroOpaque
                        Uxtheme.EnableTheming(1)
                        Uxtheme.SetSystemVisualStyle(My.PATH_Windows & "\resources\Themes\Aero\Aero.msstyles", "NormalColor", "NormalSize", 0)

                        EditReg("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "CompositionPolicy", 2)
                        EditReg("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "Composition", 1)
                        EditReg("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationOpaqueBlend", 1)

                    Case AeroTheme.Basic
                        Uxtheme.EnableTheming(1)
                        Uxtheme.SetSystemVisualStyle(My.PATH_Windows & "\resources\Themes\Aero\Aero.msstyles", "NormalColor", "NormalSize", 0)

                        EditReg("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "CompositionPolicy", 1)
                        EditReg("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "Composition", 0)
                        EditReg("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationOpaqueBlend", 0)

                    Case AeroTheme.Classic
                        Uxtheme.EnableTheming(0)

                End Select

                EditReg("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationColor", Color.FromArgb([Alpha], ColorizationColor).ToArgb)

            End Sub

            Public Function Clone() Implements ICloneable.Clone
                Return MemberwiseClone()
            End Function

            Public Overrides Function ToString() As String
                Dim tx As New List(Of String)
                tx.Clear()
                tx.Add("<Vista>")
                tx.Add("*Vista_ColorizationColor= " & ColorizationColor.ToArgb)
                tx.Add("*Vista_Alpha= " & Alpha)
                tx.Add("*Vista_Theme= " & Theme)
                tx.Add("</Vista>" & vbCrLf)
                Return tx.CString
            End Function

        End Structure

        Structure WindowsXP : Implements ICloneable

            Public Theme As WinXPTheme
            Public ThemeFile As String
            Public ColorScheme As String

            Public Sub Load(_DefWin As WindowsXP)
                If My.WXP Then
                    Dim vsFile As New Text.StringBuilder(260)
                    Dim colorName As New Text.StringBuilder(260)
                    Dim sizeName As New Text.StringBuilder(260)

                    Uxtheme.GetCurrentThemeName(vsFile, 260, colorName, 260, sizeName, 260)

                    If vsFile.ToString.ToLower = My.PATH_Windows.ToLower & "\resources\Themes\Luna\Luna.msstyles".ToLower Then
                        If colorName.ToString.ToLower = "normalcolor" Then
                            Theme = WinXPTheme.LunaBlue
                        ElseIf colorName.ToString.ToLower = "homestead" Then
                            Theme = WinXPTheme.LunaOliveGreen
                        ElseIf colorName.ToString.ToLower = "metallic" Then
                            Theme = WinXPTheme.LunaSilver
                        Else
                            Theme = WinXPTheme.LunaBlue
                        End If

                        ThemeFile = vsFile.ToString
                        ColorScheme = colorName.ToString

                    ElseIf IO.File.Exists(vsFile.ToString) AndAlso (IO.Path.GetExtension(vsFile.ToString) = ".theme" Or IO.Path.GetExtension(vsFile.ToString) = ".msstyles") Then
                        Theme = WinXPTheme.Custom
                        ThemeFile = vsFile.ToString
                        ColorScheme = colorName.ToString

                    ElseIf String.IsNullOrEmpty(vsFile.ToString) Then
                        Theme = WinXPTheme.Classic
                        ThemeFile = My.PATH_Windows.ToLower & "\resources\Themes\Luna.theme"
                        ColorScheme = "NormalColor"

                    Else
                        Theme = WinXPTheme.Custom
                        ThemeFile = ""
                        ColorScheme = ""

                    End If

                Else
                    Theme = _DefWin.Theme
                    ThemeFile = _DefWin.ThemeFile
                    ColorScheme = _DefWin.ColorScheme
                End If
            End Sub

            Sub Apply()
                Try
                    Select Case Theme
                        Case WinXPTheme.LunaBlue
                            Uxtheme.EnableTheming(1)
                            Uxtheme.SetSystemVisualStyle(My.PATH_Windows & "\resources\Themes\Luna\Luna.msstyles", "NormalColor", "NormalSize", 0)

                        Case WinXPTheme.LunaOliveGreen
                            Uxtheme.EnableTheming(1)
                            Uxtheme.SetSystemVisualStyle(My.PATH_Windows & "\resources\Themes\Luna\Luna.msstyles", "HomeStead", "NormalSize", 0)

                        Case WinXPTheme.LunaSilver
                            Uxtheme.EnableTheming(1)
                            Uxtheme.SetSystemVisualStyle(My.PATH_Windows & "\resources\Themes\Luna\Luna.msstyles", "Metallic", "NormalSize", 0)

                        Case WinXPTheme.Classic
                            Uxtheme.EnableTheming(0)

                        Case WinXPTheme.Custom

                            If IO.File.Exists(ThemeFile) AndAlso (IO.Path.GetExtension(ThemeFile) = ".theme" Or IO.Path.GetExtension(ThemeFile) = ".msstyles") Then
                                Uxtheme.EnableTheming(1)
                                Uxtheme.SetSystemVisualStyle(ThemeFile, ColorScheme, "NormalSize", 0)
                            End If

                    End Select
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

            Public Overrides Function ToString() As String
                Dim tx As New List(Of String)
                tx.Clear()
                tx.Add("<WinXP>")
                tx.Add("*WinXP_Theme= " & Theme)
                tx.Add("*WinXP_ThemeFile= " & ThemeFile)
                tx.Add("*WinXP_ColorScheme= " & ColorScheme)
                tx.Add("</WinXP>" & vbCrLf)
                Return tx.CString
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
                        Try
                            EnableTheming = GetUserPreferencesMask(17)
                        Catch
                            EnableTheming = True
                        End Try

                        Try
                            EnableGradient = GetUserPreferencesMask(4)
                        Catch
                            EnableGradient = True
                        End Try

                        With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "ActiveTitle", "153 180 209")
                            If .ToString.Split(" ").Count = 3 Then ActiveTitle = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                        End With

                        With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "AppWorkspace", "171 171 171")
                            If .ToString.Split(" ").Count = 3 Then AppWorkspace = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                        End With

                        With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "Background", "0 0 0")
                            If .ToString.Split(" ").Count = 3 Then Background = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                        End With

                        With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "ButtonAlternateFace", "0 0 0")
                            If .ToString.Split(" ").Count = 3 Then ButtonAlternateFace = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                        End With

                        With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "ButtonDkShadow", "105 105 105")
                            If .ToString.Split(" ").Count = 3 Then ButtonDkShadow = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                        End With

                        With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "ButtonFace", "240 240 240")
                            If .ToString.Split(" ").Count = 3 Then ButtonFace = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                        End With

                        With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "ButtonHilight", "255 255 255")
                            If .ToString.Split(" ").Count = 3 Then ButtonHilight = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                        End With

                        With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "ButtonLight", "227 227 227")
                            If .ToString.Split(" ").Count = 3 Then ButtonLight = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                        End With

                        With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "ButtonShadow", "160 160 160")
                            If .ToString.Split(" ").Count = 3 Then ButtonShadow = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                        End With

                        With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "ButtonText", "0 0 0")
                            If .ToString.Split(" ").Count = 3 Then ButtonText = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                        End With

                        With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "GradientActiveTitle", "185 209 234")
                            If .ToString.Split(" ").Count = 3 Then GradientActiveTitle = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                        End With

                        With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "GradientInactiveTitle", "215 228 242")
                            If .ToString.Split(" ").Count = 3 Then GradientInactiveTitle = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                        End With

                        With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "GrayText", "109 109 109")
                            If .ToString.Split(" ").Count = 3 Then GrayText = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                        End With

                        With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "HilightText", "255 255 255")
                            If .ToString.Split(" ").Count = 3 Then HilightText = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                        End With

                        With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "HotTrackingColor", "0 102 204")
                            If .ToString.Split(" ").Count = 3 Then HotTrackingColor = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                        End With

                        With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "ActiveBorder", "244 247 252")
                            If .ToString.Split(" ").Count = 3 Then ActiveBorder = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                        End With

                        With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "InactiveBorder", "244 247 252")
                            If .ToString.Split(" ").Count = 3 Then InactiveBorder = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                        End With

                        With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "InactiveTitle", "191 205 219")
                            If .ToString.Split(" ").Count = 3 Then InactiveTitle = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                        End With

                        With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "InactiveTitleText", "0 0 0")
                            If .ToString.Split(" ").Count = 3 Then InactiveTitleText = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                        End With

                        With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "InfoText", "0 0 0")
                            If .ToString.Split(" ").Count = 3 Then InfoText = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                        End With

                        With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "InfoWindow", "255 255 225")
                            If .ToString.Split(" ").Count = 3 Then InfoWindow = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                        End With

                        With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "Menu", "240 240 240")
                            If .ToString.Split(" ").Count = 3 Then Menu = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                        End With

                        With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "MenuBar", "240 240 240")
                            If .ToString.Split(" ").Count = 3 Then MenuBar = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                        End With

                        With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "MenuText", "0 0 0")
                            If .ToString.Split(" ").Count = 3 Then MenuText = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                        End With

                        With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "Scrollbar", "200 200 200")
                            If .ToString.Split(" ").Count = 3 Then Scrollbar = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                        End With

                        With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "TitleText", "0 0 0")
                            If .ToString.Split(" ").Count = 3 Then TitleText = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                        End With

                        With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "Window", "255 255 255")
                            If .ToString.Split(" ").Count = 3 Then Window = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                        End With

                        With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "WindowFrame", "100 100 100")
                            If .ToString.Split(" ").Count = 3 Then WindowFrame = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                        End With

                        With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "WindowText", "0 0 0")
                            If .ToString.Split(" ").Count = 3 Then WindowText = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                        End With

                        With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "Hilight", "0 120 215")
                            If .ToString.Split(" ").Count = 3 Then Hilight = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                        End With

                        With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "MenuHilight", "0 120 215")
                            If .ToString.Split(" ").Count = 3 Then MenuHilight = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                        End With

                        With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "Desktop", "0 0 0")
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

            Public Sub Apply()
                Dim C1 As New List(Of Integer)
                Dim C2 As New List(Of UInteger)

                C1.Clear()
                C2.Clear()

                C1.Add(13)
                C2.Add(ColorTranslator.ToWin32(Hilight))

                C1.Add(14)
                C2.Add(ColorTranslator.ToWin32(HilightText))

                C1.Add(9)
                C2.Add(ColorTranslator.ToWin32(TitleText))

                C1.Add(17)
                C2.Add(ColorTranslator.ToWin32(GrayText))

                C1.Add(11)
                C2.Add(ColorTranslator.ToWin32(InactiveBorder))

                C1.Add(3)
                C2.Add(ColorTranslator.ToWin32(InactiveTitle))

                C1.Add(2)
                C2.Add(ColorTranslator.ToWin32(ActiveTitle))

                C1.Add(10)
                C2.Add(ColorTranslator.ToWin32(ActiveBorder))

                C1.Add(12)
                C2.Add(ColorTranslator.ToWin32(AppWorkspace))

                C1.Add(1)
                C2.Add(ColorTranslator.ToWin32(Background))

                C1.Add(27)
                C2.Add(ColorTranslator.ToWin32(GradientActiveTitle))

                C1.Add(28)
                C2.Add(ColorTranslator.ToWin32(GradientInactiveTitle))

                C1.Add(19)
                C2.Add(ColorTranslator.ToWin32(InactiveTitleText))

                C1.Add(24)
                C2.Add(ColorTranslator.ToWin32(InfoWindow))

                C1.Add(23)
                C2.Add(ColorTranslator.ToWin32(InfoText))

                C1.Add(4)
                C2.Add(ColorTranslator.ToWin32(Menu))

                C1.Add(7)
                C2.Add(ColorTranslator.ToWin32(MenuText))

                C1.Add(0)
                C2.Add(ColorTranslator.ToWin32(Scrollbar))

                C1.Add(5)
                C2.Add(ColorTranslator.ToWin32(Window))

                C1.Add(6)
                C2.Add(ColorTranslator.ToWin32(WindowFrame))

                C1.Add(8)
                C2.Add(ColorTranslator.ToWin32(WindowText))

                C1.Add(26)
                C2.Add(ColorTranslator.ToWin32(HotTrackingColor))

                C1.Add(29)
                C2.Add(ColorTranslator.ToWin32(MenuHilight))

                C1.Add(30)
                C2.Add(ColorTranslator.ToWin32(MenuBar))

                C1.Add(15)
                C2.Add(ColorTranslator.ToWin32(ButtonFace))

                C1.Add(20)
                C2.Add(ColorTranslator.ToWin32(ButtonHilight))

                C1.Add(16)
                C2.Add(ColorTranslator.ToWin32(ButtonShadow))

                C1.Add(18)
                C2.Add(ColorTranslator.ToWin32(ButtonText))

                C1.Add(21)
                C2.Add(ColorTranslator.ToWin32(ButtonDkShadow))

                C1.Add(25)
                C2.Add(ColorTranslator.ToWin32(ButtonAlternateFace))

                C1.Add(22)
                C2.Add(ColorTranslator.ToWin32(ButtonLight))

                User32.SetSysColors(C1.Count, C1.ToArray(), C2.ToArray())

                My.Computer.Registry.SetValue("HKEY_CURRENT_USER\Control Panel\Desktop", "UserPreferencesMask", SetUserPreferenceMask(17, EnableTheming), RegistryValueKind.Binary)
                My.Computer.Registry.SetValue("HKEY_CURRENT_USER\Control Panel\Desktop", "UserPreferencesMask", SetUserPreferenceMask(4, EnableGradient), RegistryValueKind.Binary)

                User32.SystemParametersInfo(SPI.Effects.SETFLATMENU, 0, EnableTheming.ToInteger, 0)
                User32.SystemParametersInfo(SPI.Titlebars.SETGRADIENTCAPTIONS, 0, EnableGradient.ToInteger, 0)

                EditReg("HKEY_CURRENT_USER\Control Panel\Colors", "ActiveBorder", ActiveBorder.Win32_RegColor, RegistryValueKind.String)
                EditReg("HKEY_CURRENT_USER\Control Panel\Colors", "ActiveTitle", ActiveTitle.Win32_RegColor, RegistryValueKind.String)
                EditReg("HKEY_CURRENT_USER\Control Panel\Colors", "AppWorkspace", AppWorkspace.Win32_RegColor, RegistryValueKind.String)
                EditReg("HKEY_CURRENT_USER\Control Panel\Colors", "Background", Background.Win32_RegColor, RegistryValueKind.String)
                EditReg("HKEY_CURRENT_USER\Control Panel\Colors", "ButtonAlternateFace", ButtonAlternateFace.Win32_RegColor, RegistryValueKind.String)
                EditReg("HKEY_CURRENT_USER\Control Panel\Colors", "ButtonDkShadow", ButtonDkShadow.Win32_RegColor, RegistryValueKind.String)
                EditReg("HKEY_CURRENT_USER\Control Panel\Colors", "ButtonFace", ButtonFace.Win32_RegColor, RegistryValueKind.String)
                EditReg("HKEY_CURRENT_USER\Control Panel\Colors", "ButtonHilight", ButtonHilight.Win32_RegColor, RegistryValueKind.String)
                EditReg("HKEY_CURRENT_USER\Control Panel\Colors", "ButtonLight", ButtonLight.Win32_RegColor, RegistryValueKind.String)
                EditReg("HKEY_CURRENT_USER\Control Panel\Colors", "ButtonShadow", ButtonShadow.Win32_RegColor, RegistryValueKind.String)
                EditReg("HKEY_CURRENT_USER\Control Panel\Colors", "ButtonText", ButtonText.Win32_RegColor, RegistryValueKind.String)
                EditReg("HKEY_CURRENT_USER\Control Panel\Colors", "GradientActiveTitle", GradientActiveTitle.Win32_RegColor, RegistryValueKind.String)
                EditReg("HKEY_CURRENT_USER\Control Panel\Colors", "GradientInactiveTitle", GradientInactiveTitle.Win32_RegColor, RegistryValueKind.String)
                EditReg("HKEY_CURRENT_USER\Control Panel\Colors", "GrayText", GrayText.Win32_RegColor, RegistryValueKind.String)
                EditReg("HKEY_CURRENT_USER\Control Panel\Colors", "HilightText", HilightText.Win32_RegColor, RegistryValueKind.String)
                EditReg("HKEY_CURRENT_USER\Control Panel\Colors", "HotTrackingColor", HotTrackingColor.Win32_RegColor, RegistryValueKind.String)
                EditReg("HKEY_CURRENT_USER\Control Panel\Colors", "InactiveBorder", InactiveBorder.Win32_RegColor, RegistryValueKind.String)
                EditReg("HKEY_CURRENT_USER\Control Panel\Colors", "InactiveTitle", InactiveTitle.Win32_RegColor, RegistryValueKind.String)
                EditReg("HKEY_CURRENT_USER\Control Panel\Colors", "InactiveTitleText", InactiveTitleText.Win32_RegColor, RegistryValueKind.String)
                EditReg("HKEY_CURRENT_USER\Control Panel\Colors", "InfoText", InfoText.Win32_RegColor, RegistryValueKind.String)
                EditReg("HKEY_CURRENT_USER\Control Panel\Colors", "InfoWindow", InfoWindow.Win32_RegColor, RegistryValueKind.String)
                EditReg("HKEY_CURRENT_USER\Control Panel\Colors", "Menu", Menu.Win32_RegColor, RegistryValueKind.String)
                EditReg("HKEY_CURRENT_USER\Control Panel\Colors", "MenuBar", MenuBar.Win32_RegColor, RegistryValueKind.String)
                EditReg("HKEY_CURRENT_USER\Control Panel\Colors", "MenuText", MenuText.Win32_RegColor, RegistryValueKind.String)
                EditReg("HKEY_CURRENT_USER\Control Panel\Colors", "Scrollbar", Scrollbar.Win32_RegColor, RegistryValueKind.String)
                EditReg("HKEY_CURRENT_USER\Control Panel\Colors", "TitleText", TitleText.Win32_RegColor, RegistryValueKind.String)
                EditReg("HKEY_CURRENT_USER\Control Panel\Colors", "Window", Window.Win32_RegColor, RegistryValueKind.String)
                EditReg("HKEY_CURRENT_USER\Control Panel\Colors", "WindowFrame", WindowFrame.Win32_RegColor, RegistryValueKind.String)
                EditReg("HKEY_CURRENT_USER\Control Panel\Colors", "WindowText", WindowText.Win32_RegColor, RegistryValueKind.String)
                EditReg("HKEY_CURRENT_USER\Control Panel\Colors", "Hilight", Hilight.Win32_RegColor, RegistryValueKind.String)
                EditReg("HKEY_CURRENT_USER\Control Panel\Colors", "MenuHilight", MenuHilight.Win32_RegColor, RegistryValueKind.String)
                EditReg("HKEY_CURRENT_USER\Control Panel\Colors", "Desktop", Desktop.Win32_RegColor, RegistryValueKind.String)
            End Sub

            Public Function Clone() Implements ICloneable.Clone
                Return MemberwiseClone()
            End Function

            Public Overrides Function ToString() As String
                Dim tx As New List(Of String)
                tx.Clear()

                tx.Add("<Win32UI>")
                tx.Add("*Win32UI_EnableTheming= " & EnableTheming)
                tx.Add("*Win32UI_EnableGradient= " & EnableGradient)
                tx.Add("*Win32UI_ActiveBorder= " & ActiveBorder.ToArgb)
                tx.Add("*Win32UI_ActiveTitle= " & ActiveTitle.ToArgb)
                tx.Add("*Win32UI_AppWorkspace= " & AppWorkspace.ToArgb)
                tx.Add("*Win32UI_Background= " & Background.ToArgb)
                tx.Add("*Win32UI_ButtonAlternateFace= " & ButtonAlternateFace.ToArgb)
                tx.Add("*Win32UI_ButtonDkShadow= " & ButtonDkShadow.ToArgb)
                tx.Add("*Win32UI_ButtonFace= " & ButtonFace.ToArgb)
                tx.Add("*Win32UI_ButtonHilight= " & ButtonHilight.ToArgb)
                tx.Add("*Win32UI_ButtonLight= " & ButtonLight.ToArgb)
                tx.Add("*Win32UI_ButtonShadow= " & ButtonShadow.ToArgb)
                tx.Add("*Win32UI_ButtonText= " & ButtonText.ToArgb)
                tx.Add("*Win32UI_GradientActiveTitle= " & GradientActiveTitle.ToArgb)
                tx.Add("*Win32UI_GradientInactiveTitle= " & GradientInactiveTitle.ToArgb)
                tx.Add("*Win32UI_GrayText= " & GrayText.ToArgb)
                tx.Add("*Win32UI_HilightText= " & HilightText.ToArgb)
                tx.Add("*Win32UI_HotTrackingColor= " & HotTrackingColor.ToArgb)
                tx.Add("*Win32UI_InactiveBorder= " & InactiveBorder.ToArgb)
                tx.Add("*Win32UI_InactiveTitle= " & InactiveTitle.ToArgb)
                tx.Add("*Win32UI_InactiveTitleText= " & InactiveTitleText.ToArgb)
                tx.Add("*Win32UI_InfoText= " & InfoText.ToArgb)
                tx.Add("*Win32UI_InfoWindow= " & InfoWindow.ToArgb)
                tx.Add("*Win32UI_Menu= " & Menu.ToArgb)
                tx.Add("*Win32UI_MenuBar= " & MenuBar.ToArgb)
                tx.Add("*Win32UI_MenuText= " & MenuText.ToArgb)
                tx.Add("*Win32UI_Scrollbar= " & Scrollbar.ToArgb)
                tx.Add("*Win32UI_TitleText= " & TitleText.ToArgb)
                tx.Add("*Win32UI_Window= " & Window.ToArgb)
                tx.Add("*Win32UI_WindowFrame= " & WindowFrame.ToArgb)
                tx.Add("*Win32UI_WindowText= " & WindowText.ToArgb)
                tx.Add("*Win32UI_Hilight= " & Hilight.ToArgb)
                tx.Add("*Win32UI_MenuHilight= " & MenuHilight.ToArgb)
                tx.Add("*Win32UI_Desktop= " & Desktop.ToArgb)
                tx.Add("</Win32UI>" & vbCrLf)

                Return tx.CString
            End Function

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

            Public ComboboxAnimation As Boolean
            Public ListBoxSmoothScrolling As Boolean

            Public TooltipAnimation As Boolean
            Public TooltipFade As MenuAnimType

            Public IconsShadow As Boolean
            Public IconsDesktopTranslSel As Boolean

            Sub Load(_DefEffects As WinEffects)
                Dim rMain_WE As RegistryKey = Registry.CurrentUser.CreateSubKey("Software\WinPaletter\WindowsEffects")
                Enabled = rMain_WE.GetValue("", True)
                rMain_WE.Close()

                Dim i As Boolean
                Dim x As Boolean

                Dim anim As New ANIMATIONINFO With {.cbSize = Marshal.SizeOf(anim)}

                Try
                    If SystemParametersInfo(SPI.Effects.GETANIMATION, anim.cbSize, anim, SPIF.None) = 1 Then
                        WindowAnimation = anim.IMinAnimate.ToBoolean
                    Else
                        WindowAnimation = _DefEffects.WindowAnimation
                    End If
                Catch
                    WindowAnimation = _DefEffects.WindowAnimation
                End Try

                Try
                    If Fixer.SystemParametersInfo(SPI.Effects.GETDROPSHADOW, 0, i, SPIF.None) = 1 Then
                        WindowShadow = i
                    Else
                        WindowShadow = _DefEffects.WindowShadow
                    End If
                Catch
                    WindowShadow = _DefEffects.WindowShadow
                End Try

                Try
                    If Fixer.SystemParametersInfo(SPI.Effects.GETUIEFFECTS, 0, i, SPIF.None) = 1 Then
                        WindowUIEffects = i
                    Else
                        WindowUIEffects = _DefEffects.WindowUIEffects
                    End If
                Catch
                    WindowUIEffects = _DefEffects.WindowUIEffects
                End Try

                Try
                    If Fixer.SystemParametersInfo(SPI.Effects.GETMENUANIMATION, 0, i, SPIF.None) = 1 Then
                        MenuAnimation = i
                    Else
                        MenuAnimation = _DefEffects.MenuAnimation
                    End If
                Catch
                    MenuAnimation = _DefEffects.MenuAnimation
                End Try

                Try
                    If Fixer.SystemParametersInfo(SPI.Effects.GETMENUFADE, 0, x, SPIF.None) = 1 Then
                        MenuFade = If(x, MenuAnimType.Fade, MenuAnimType.Scroll)
                    Else
                        MenuFade = _DefEffects.MenuFade
                    End If
                Catch
                    MenuFade = _DefEffects.MenuFade
                End Try

                Try
                    If Fixer.SystemParametersInfo(SPI.Effects.GETSELECTIONFADE, 0, i, SPIF.None) = 1 Then
                        MenuSelectionFade = i
                    Else
                        MenuSelectionFade = _DefEffects.MenuSelectionFade
                    End If
                Catch
                    MenuSelectionFade = _DefEffects.MenuSelectionFade
                End Try

                Try
                    If Fixer.SystemParametersInfo(SPI.Effects.GETCOMBOBOXANIMATION, 0, i, SPIF.None) = 1 Then
                        ComboboxAnimation = i
                    Else
                        ComboboxAnimation = _DefEffects.ComboboxAnimation
                    End If
                Catch
                    ComboboxAnimation = _DefEffects.ComboboxAnimation
                End Try


                Try
                    If Fixer.SystemParametersInfo(SPI.Effects.GETLISTBOXSMOOTHSCROLLING, 0, i, SPIF.None) = 1 Then
                        ListBoxSmoothScrolling = i
                    Else
                        ListBoxSmoothScrolling = _DefEffects.ListBoxSmoothScrolling
                    End If
                Catch
                    ListBoxSmoothScrolling = _DefEffects.ListBoxSmoothScrolling
                End Try


                Try
                    If Fixer.SystemParametersInfo(SPI.Effects.GETTOOLTIPANIMATION, 0, i, SPIF.None) = 1 Then
                        TooltipAnimation = i
                    Else
                        TooltipAnimation = _DefEffects.TooltipAnimation
                    End If
                Catch
                    TooltipAnimation = _DefEffects.TooltipAnimation
                End Try

                Try
                    If Fixer.SystemParametersInfo(SPI.Effects.GETTOOLTIPFADE, 0, x, SPIF.None) = 1 Then
                        TooltipFade = If(x, MenuAnimType.Fade, MenuAnimType.Scroll)
                    Else
                        TooltipFade = _DefEffects.TooltipFade
                    End If
                Catch
                    TooltipFade = _DefEffects.TooltipFade
                End Try

                Try
                    If Fixer.SystemParametersInfo(SPI.Effects.GETDRAGFULLWINDOWS, 0, i, SPIF.None) = 1 Then
                        ShowWinContentDrag = i
                    Else
                        ShowWinContentDrag = _DefEffects.ShowWinContentDrag
                    End If
                Catch
                    ShowWinContentDrag = _DefEffects.ShowWinContentDrag
                End Try

                Try
                    IconsShadow = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "ListviewShadow", _DefEffects.IconsShadow)
                Catch
                    IconsShadow = _DefEffects.IconsShadow
                End Try

                Try
                    IconsDesktopTranslSel = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "ListviewAlphaSelect", _DefEffects.IconsDesktopTranslSel)
                Catch
                    IconsDesktopTranslSel = _DefEffects.IconsDesktopTranslSel
                End Try

            End Sub

            Sub Apply()
                Dim rMain As RegistryKey = Registry.CurrentUser.CreateSubKey("Software\WinPaletter\WindowsEffects")
                rMain.SetValue("", Enabled, RegistryValueKind.DWord)
                rMain.Close()

                If Enabled Then
                    Dim anim As New ANIMATIONINFO With {.cbSize = Marshal.SizeOf(anim), .IMinAnimate = WindowAnimation.ToInteger}
                    SystemParametersInfo(SPI.Effects.SETANIMATION, anim.cbSize, anim, SPIF.SendChange)
                    SystemParametersInfo(SPI.Effects.SETDROPSHADOW, 0, WindowShadow, SPIF.SendChange)
                    SystemParametersInfo(SPI.Effects.SETUIEFFECTS, 0, WindowUIEffects, SPIF.SendChange)
                    SystemParametersInfo(SPI.Effects.SETDRAGFULLWINDOWS, ShowWinContentDrag, 0, SPIF.SendChange) 'use uiParam not pvParam
                    SystemParametersInfo(SPI.Effects.SETMENUANIMATION, 0, MenuAnimation, SPIF.SendChange)
                    SystemParametersInfo(SPI.Effects.SETMENUFADE, 0, MenuFade = MenuAnimType.Fade, SPIF.SendChange)
                    SystemParametersInfo(SPI.Effects.SETSELECTIONFADE, 0, MenuSelectionFade, SPIF.SendChange)
                    SystemParametersInfo(SPI.Effects.SETCOMBOBOXANIMATION, 0, ComboboxAnimation, SPIF.SendChange)
                    SystemParametersInfo(SPI.Effects.SETLISTBOXSMOOTHSCROLLING, 0, ListBoxSmoothScrolling, SPIF.SendChange)
                    SystemParametersInfo(SPI.Effects.SETTOOLTIPANIMATION, 0, TooltipAnimation, SPIF.SendChange)
                    SystemParametersInfo(SPI.Effects.SETTOOLTIPFADE, 0, TooltipFade = MenuAnimType.Fade, SPIF.SendChange)

                    EditReg("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "ListviewShadow", IconsShadow.ToInteger)
                    EditReg("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "ListviewAlphaSelect", IconsDesktopTranslSel.ToInteger)

                    'ShowWinContentDrag should be re-written in registry with string format as SystemParametersInfo looses its effect after logoff here
                    EditReg("HKEY_CURRENT_USER\Control Panel\Desktop", "DragFullWindows", ShowWinContentDrag.ToInteger, RegistryValueKind.String)

                    User32.SendMessageTimeout(User32.HWND_BROADCAST, User32.WM_SETTINGCHANGE, UIntPtr.Zero, Marshal.StringToHGlobalAnsi("Environment"), User32.SendMessageTimeoutFlags.SMTO_ABORTIFHUNG, User32.MSG_TIMEOUT, User32.RESULT)
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
            Public Overrides Function ToString() As String
                Dim tx As New List(Of String)
                tx.Clear()
                tx.Add("<WindowsEffects>")
                tx.Add("*WinEffects_Enabled= " & Enabled)
                tx.Add("*WinEffects_WindowAnimation= " & WindowAnimation)
                tx.Add("*WinEffects_WindowShadow= " & WindowShadow)
                tx.Add("*WinEffects_WindowUIEffects= " & WindowUIEffects)
                tx.Add("*WinEffects_MenuAnimation= " & MenuAnimation)
                tx.Add("*WinEffects_MenuFade= " & MenuFade)
                tx.Add("*WinEffects_MenuSelectionFade= " & MenuSelectionFade)
                tx.Add("*WinEffects_ComboBoxAnimation= " & ComboboxAnimation)
                tx.Add("*WinEffects_ListboxSmoothScrolling= " & ListBoxSmoothScrolling)
                tx.Add("*WinEffects_TooltipAnimation= " & TooltipAnimation)
                tx.Add("*WinEffects_TooltipFade= " & TooltipFade)
                tx.Add("*WinEffects_IconsShadow= " & IconsShadow)
                tx.Add("*WinEffects_IconsDesktopTranslSel= " & IconsDesktopTranslSel)
                tx.Add("*WinEffects_ShowWinContentDrag= " & ShowWinContentDrag)
                tx.Add("</WindowsEffects>" & vbCrLf)
                Return tx.CString
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
                Dim rMain_M As RegistryKey = Registry.CurrentUser.CreateSubKey("Software\WinPaletter\Metrics")
                Enabled = rMain_M.GetValue("", True)
                rMain_M.Close()

                Try
                    BorderWidth = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "BorderWidth", _DefMetricsFonts.BorderWidth * -15) / -15
                Catch
                    BorderWidth = _DefMetricsFonts.BorderWidth
                End Try

                Try
                    CaptionHeight = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "CaptionHeight", _DefMetricsFonts.CaptionHeight * -15) / -15
                Catch
                    CaptionHeight = _DefMetricsFonts.CaptionHeight
                End Try

                Try
                    CaptionWidth = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "CaptionWidth", _DefMetricsFonts.CaptionWidth * -15) / -15
                Catch
                    CaptionWidth = _DefMetricsFonts.CaptionWidth
                End Try

                Try
                    IconSpacing = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "IconSpacing", _DefMetricsFonts.IconSpacing * -15) / -15
                Catch
                    IconSpacing = _DefMetricsFonts.IconSpacing
                End Try

                Try
                    IconVerticalSpacing = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "IconVerticalSpacing", _DefMetricsFonts.IconVerticalSpacing * -15) / -15
                Catch
                    IconVerticalSpacing = _DefMetricsFonts.IconVerticalSpacing
                End Try

                Try
                    MenuHeight = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "MenuHeight", _DefMetricsFonts.MenuHeight * -15) / -15
                Catch
                    MenuHeight = _DefMetricsFonts.MenuHeight
                End Try

                Try
                    MenuWidth = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "MenuWidth", _DefMetricsFonts.MenuWidth * -15) / -15
                Catch
                    MenuWidth = _DefMetricsFonts.MenuWidth
                End Try

                Try
                    PaddedBorderWidth = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "PaddedBorderWidth", _DefMetricsFonts.PaddedBorderWidth * -15) / -15
                Catch
                    PaddedBorderWidth = _DefMetricsFonts.PaddedBorderWidth
                End Try

                Try
                    ScrollHeight = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "ScrollHeight", _DefMetricsFonts.ScrollHeight * -15) / -15
                Catch
                    ScrollHeight = _DefMetricsFonts.ScrollHeight
                End Try

                Try
                    ScrollWidth = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "ScrollWidth", _DefMetricsFonts.ScrollWidth * -15) / -15
                Catch
                    ScrollWidth = _DefMetricsFonts.ScrollWidth
                End Try

                Try
                    SmCaptionHeight = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "SmCaptionHeight", _DefMetricsFonts.SmCaptionHeight * -15) / -15
                Catch
                    SmCaptionHeight = _DefMetricsFonts.SmCaptionHeight
                End Try

                Try
                    SmCaptionWidth = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "SmCaptionWidth", _DefMetricsFonts.SmCaptionWidth * -15) / -15
                Catch
                    SmCaptionWidth = _DefMetricsFonts.SmCaptionWidth
                End Try

                Try
                    ShellIconSize = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "Shell Icon Size", _DefMetricsFonts.ShellIconSize)
                Catch
                    ShellIconSize = _DefMetricsFonts.ShellIconSize
                End Try

                Try
                    DesktopIconSize = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\Shell\Bags\1\Desktop", "IconSize", _DefMetricsFonts.DesktopIconSize)
                Catch
                    DesktopIconSize = _DefMetricsFonts.DesktopIconSize
                End Try


                Try
                    CaptionFont = DirectCast(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "CaptionFont", _DefMetricsFonts.CaptionFont.ToByte), Byte()).ToFont
                Catch
                    CaptionFont = _DefMetricsFonts.CaptionFont
                End Try

                Try
                    IconFont = DirectCast(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "IconFont", _DefMetricsFonts.IconFont.ToByte), Byte()).ToFont
                Catch
                    IconFont = _DefMetricsFonts.IconFont
                End Try

                Try
                    MenuFont = DirectCast(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "MenuFont", _DefMetricsFonts.MenuFont.ToByte), Byte()).ToFont
                Catch
                    MenuFont = _DefMetricsFonts.MenuFont
                End Try

                Try
                    MessageFont = DirectCast(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "MessageFont", _DefMetricsFonts.MessageFont.ToByte), Byte()).ToFont
                Catch
                    MessageFont = _DefMetricsFonts.MessageFont
                End Try

                Try
                    SmCaptionFont = DirectCast(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "SmCaptionFont", _DefMetricsFonts.SmCaptionFont.ToByte), Byte()).ToFont
                Catch
                    SmCaptionFont = _DefMetricsFonts.SmCaptionFont
                End Try

                Try
                    StatusFont = DirectCast(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "StatusFont", _DefMetricsFonts.StatusFont.ToByte), Byte()).ToFont
                Catch
                    StatusFont = _DefMetricsFonts.StatusFont
                End Try

                Try
                    FontSubstitute_MSShellDlg = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\FontSubstitutes", "MS Shell Dlg", _DefMetricsFonts.FontSubstitute_MSShellDlg)
                Catch
                    FontSubstitute_MSShellDlg = _DefMetricsFonts.FontSubstitute_MSShellDlg
                End Try

                Try
                    FontSubstitute_MSShellDlg2 = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\FontSubstitutes", "MS Shell Dlg 2", _DefMetricsFonts.FontSubstitute_MSShellDlg2)
                Catch
                    FontSubstitute_MSShellDlg2 = _DefMetricsFonts.FontSubstitute_MSShellDlg2
                End Try

                Try
                    FontSubstitute_SegoeUI = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\FontSubstitutes", "Segoe UI", _DefMetricsFonts.FontSubstitute_SegoeUI)
                Catch
                    FontSubstitute_SegoeUI = _DefMetricsFonts.FontSubstitute_SegoeUI
                End Try
            End Sub

            Public Sub Apply()
                Dim rMain As RegistryKey = Registry.CurrentUser.CreateSubKey("Software\WinPaletter\Metrics")
                rMain.SetValue("", Enabled, RegistryValueKind.DWord)
                rMain.Close()

                If Enabled Then
                    Dim NCM As New NONCLIENTMETRICS With {.cbSize = Marshal.SizeOf(NCM)}
                    Dim ICO As New ICONMETRICS With {.cbSize = Marshal.SizeOf(ICO)}
                    SystemParametersInfo(SPI.Metrics.GETNONCLIENTMETRICS, NCM.cbSize, NCM, SPIF.None)
                    SystemParametersInfo(SPI.Icons.GETICONMETRICS, ICO.cbSize, ICO, SPIF.None)

                    If XenonCore.GetWindowsScreenScalingFactor > 100 Then
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

                    EditReg("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "CaptionFont", lfCaptionFont.ToByte, RegistryValueKind.Binary)
                    EditReg("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "IconFont", lfIconFont.ToByte, RegistryValueKind.Binary)
                    EditReg("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "MenuFont", lfMenuFont.ToByte, RegistryValueKind.Binary)
                    EditReg("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "MessageFont", lfMessageFont.ToByte, RegistryValueKind.Binary)
                    EditReg("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "SmCaptionFont", lfSMCaptionFont.ToByte, RegistryValueKind.Binary)
                    EditReg("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "StatusFont", lfStatusFont.ToByte, RegistryValueKind.Binary)

                    EditReg("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\FontSubstitutes", "MS Shell Dlg", FontSubstitute_MSShellDlg, RegistryValueKind.String)
                    EditReg("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\FontSubstitutes", "MS Shell Dlg 2", FontSubstitute_MSShellDlg2, RegistryValueKind.String)

                    If String.IsNullOrWhiteSpace(FontSubstitute_SegoeUI) Then
                        EditReg("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI (TrueType)", "segoeui.ttf", RegistryValueKind.String)
                        EditReg("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Bold (TrueType)", "segoeuib.ttf", RegistryValueKind.String)
                        EditReg("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Bold Italic (TrueType)", "segoeuiz.ttf", RegistryValueKind.String)
                        EditReg("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Italic (TrueType)", "segoeuii.ttf", RegistryValueKind.String)
                        EditReg("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Light (TrueType)", "segoeuil.ttf", RegistryValueKind.String)
                        EditReg("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Light Italic (TrueType)", "seguili.ttf", RegistryValueKind.String)
                        EditReg("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Semibold (TrueType)", "seguisb.ttf", RegistryValueKind.String)
                        EditReg("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Semibold Italic (TrueType)", "seguisbi.ttf", RegistryValueKind.String)
                        EditReg("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Semilight (TrueType)", "segoeuisl.ttf", RegistryValueKind.String)
                        EditReg("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Semilight Italic (TrueType)", "seguisli.ttf", RegistryValueKind.String)
                    Else
                        EditReg("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI (TrueType)", "", RegistryValueKind.String)
                        EditReg("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Bold (TrueType)", "", RegistryValueKind.String)
                        EditReg("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Bold Italic (TrueType)", "", RegistryValueKind.String)
                        EditReg("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Italic (TrueType)", "", RegistryValueKind.String)
                        EditReg("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Light (TrueType)", "", RegistryValueKind.String)
                        EditReg("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Light Italic (TrueType)", "", RegistryValueKind.String)
                        EditReg("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Semibold (TrueType)", "", RegistryValueKind.String)
                        EditReg("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Semibold Italic (TrueType)", "", RegistryValueKind.String)
                        EditReg("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Semilight (TrueType)", "", RegistryValueKind.String)
                        EditReg("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Semilight Italic (TrueType)", "", RegistryValueKind.String)
                    End If
                    EditReg("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\FontSubstitutes", "Segoe UI", FontSubstitute_SegoeUI, RegistryValueKind.String)

                    EditReg("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "BorderWidth", BorderWidth * -15, RegistryValueKind.String)
                    EditReg("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "CaptionHeight", CaptionHeight * -15, RegistryValueKind.String)
                    EditReg("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "CaptionWidth", CaptionWidth * -15, RegistryValueKind.String)
                    EditReg("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "IconSpacing", IconSpacing * -15, RegistryValueKind.String)
                    EditReg("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "IconVerticalSpacing", IconVerticalSpacing * -15, RegistryValueKind.String)
                    EditReg("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "MenuHeight", MenuHeight * -15, RegistryValueKind.String)
                    EditReg("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "MenuWidth", MenuWidth * -15, RegistryValueKind.String)
                    EditReg("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "PaddedBorderWidth", PaddedBorderWidth * -15, RegistryValueKind.String)
                    EditReg("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "ScrollHeight", ScrollHeight * -15, RegistryValueKind.String)
                    EditReg("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "ScrollWidth", ScrollWidth * -15, RegistryValueKind.String)
                    EditReg("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "SmCaptionHeight", SmCaptionHeight * -15, RegistryValueKind.String)
                    EditReg("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "SmCaptionWidth", SmCaptionWidth * -15, RegistryValueKind.String)
                    EditReg("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "Shell Icon Size", ShellIconSize, RegistryValueKind.String)
                    EditReg("HKEY_CURRENT_USER\Software\Microsoft\Windows\Shell\Bags\1\Desktop", "IconSize", DesktopIconSize, RegistryValueKind.String)

                    With NCM
                        .lfCaptionFont = lfCaptionFont        'Requires LogOff
                        .lfSMCaptionFont = lfSMCaptionFont    'Requires LogOff
                        .lfStatusFont = lfStatusFont          'Requires LogOff
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

                    SystemParametersInfo(SPI.Metrics.SETNONCLIENTMETRICS, Marshal.SizeOf(NCM), NCM, SPIF.SendChange)
                    SystemParametersInfo(SPI.Icons.SETICONMETRICS, Marshal.SizeOf(ICO), ICO, SPIF.SendChange)
                    User32.SendMessageTimeout(User32.HWND_BROADCAST, User32.WM_SETTINGCHANGE, UIntPtr.Zero, Marshal.StringToHGlobalAnsi("WindowMetrics"), User32.SendMessageTimeoutFlags.SMTO_ABORTIFHUNG, User32.MSG_TIMEOUT, User32.RESULT)

                    'Try : SendMessageTimeout(HWND_BROADCAST, WM_DWMCOMPOSITIONCHANGED, UIntPtr.Zero, Marshal.StringToHGlobalAnsi("WindowMetrics"), SendMessageTimeoutFlags.SMTO_ABORTIFHUNG, MSG_TIMEOUT, RESULT) : Catch : End Try
                    'Try : SendMessageTimeout(HWND_BROADCAST, WM_THEMECHANGED, UIntPtr.Zero, Marshal.StringToHGlobalAnsi("WindowMetrics"), SendMessageTimeoutFlags.SMTO_ABORTIFHUNG, MSG_TIMEOUT, RESULT) : Catch : End Try
                    'Try : SendMessageTimeout(HWND_BROADCAST, WM_SYSCOLORCHANGE, UIntPtr.Zero, Marshal.StringToHGlobalAnsi("WindowMetrics"), SendMessageTimeoutFlags.SMTO_ABORTIFHUNG, MSG_TIMEOUT, RESULT) : Catch : End Try
                    'Try : SendMessageTimeout(HWND_BROADCAST, WM_PALETTECHANGED, UIntPtr.Zero, Marshal.StringToHGlobalAnsi("WindowMetrics"), SendMessageTimeoutFlags.SMTO_ABORTIFHUNG, MSG_TIMEOUT, RESULT) : Catch : End Try
                    'Try : SendMessageTimeout(HWND_BROADCAST, WM_SETTINGCHANGE, UIntPtr.Zero, Marshal.StringToHGlobalAnsi("WindowMetrics"), SendMessageTimeoutFlags.SMTO_ABORTIFHUNG, MSG_TIMEOUT, RESULT) : Catch : End Try
                End If

            End Sub

            Public Overrides Function ToString() As String
                Dim tx As New List(Of String)
                tx.Clear()
                tx.Add("<Metrics&Fonts>")
                tx.Add("*MetricsFonts_Enabled= " & Enabled)
                tx.Add("*Metrics_BorderWidth= " & BorderWidth)
                tx.Add("*Metrics_CaptionHeight= " & CaptionHeight)
                tx.Add("*Metrics_CaptionWidth= " & CaptionWidth)
                tx.Add("*Metrics_IconSpacing= " & IconSpacing)
                tx.Add("*Metrics_IconVerticalSpacing= " & IconVerticalSpacing)
                tx.Add("*Metrics_MenuHeight= " & MenuHeight)
                tx.Add("*Metrics_MenuWidth= " & MenuWidth)
                tx.Add("*Metrics_PaddedBorderWidth= " & PaddedBorderWidth)
                tx.Add("*Metrics_ScrollHeight= " & ScrollHeight)
                tx.Add("*Metrics_ScrollWidth= " & ScrollWidth)
                tx.Add("*Metrics_SmCaptionHeight= " & SmCaptionHeight)
                tx.Add("*Metrics_SmCaptionWidth= " & SmCaptionWidth)
                tx.Add("*Metrics_DesktopIconSize= " & DesktopIconSize)
                tx.Add("*Metrics_ShellIconSize= " & ShellIconSize)
                tx.Add("*FontSubstitute_MSShellDlg= " & FontSubstitute_MSShellDlg)
                tx.Add("*FontSubstitute_MSShellDlg2= " & FontSubstitute_MSShellDlg2)
                tx.Add("*FontSubstitute_SegoeUI= " & FontSubstitute_SegoeUI)
                tx.Add(AddFontsToThemeFile("Caption", CaptionFont))
                tx.Add(AddFontsToThemeFile("Icon", IconFont))
                tx.Add(AddFontsToThemeFile("Menu", MenuFont))
                tx.Add(AddFontsToThemeFile("Message", MessageFont))
                tx.Add(AddFontsToThemeFile("SmCaption", SmCaptionFont))
                tx.Add(AddFontsToThemeFile("Status", StatusFont))
                tx.Add("</Metrics&Fonts>" & vbCrLf)
                Return tx.CString
            End Function

        End Structure

        Structure WallpaperTone : Implements ICloneable
            Public Enabled As Boolean
            Public Image As String
            Public H, S, L As Integer

            Public Sub Load(SubKey As String)

                Dim rMain As RegistryKey = Registry.CurrentUser.CreateSubKey("Software\WinPaletter\WallpaperTone")

                Dim r As RegistryKey = rMain
                r.CreateSubKey(SubKey)
                r = r.OpenSubKey(SubKey)

                Dim wallpaper As String

                If SubKey.ToLower = "winxp" Then
                    wallpaper = My.PATH_Windows & "\Web\Wallpaper\Bliss.bmp"
                Else
                    wallpaper = My.PATH_Windows & "\Web\Wallpaper\Windows\img0.jpg"
                End If

                If Not IO.File.Exists(wallpaper) Then
                    Dim R1 As RegistryKey = Registry.CurrentUser.OpenSubKey("Control Panel\Desktop", True)
                    wallpaper = R1.GetValue("Wallpaper").ToString()
                    If R1 IsNot Nothing Then R1.Close()
                End If

                Enabled = r.GetValue("Enabled", False)
                Image = r.GetValue("Image", wallpaper)
                H = r.GetValue("H", 0)
                S = r.GetValue("S", 50)
                L = r.GetValue("L", 50)

                r.Close()
            End Sub

            Public Shared Sub Save_To_Registry(WT As WallpaperTone, SubKey As String)
                Dim rMain As RegistryKey = Registry.CurrentUser.CreateSubKey("Software\WinPaletter\WallpaperTone")
                Dim r As RegistryKey

                r = rMain.CreateSubKey(SubKey)
                With r
                    .SetValue("Enabled", WT.Enabled, RegistryValueKind.QWord)
                    .SetValue("Image", WT.Image, RegistryValueKind.String)
                    .SetValue("H", WT.H, RegistryValueKind.QWord)
                    .SetValue("S", WT.S, RegistryValueKind.QWord)
                    .SetValue("L", WT.L, RegistryValueKind.QWord)
                End With

                r.Close()
                rMain.Close()
            End Sub

            Public Sub Apply()
                Dim HSL As New HSLFilter With {
                        .Hue = Me.H,
                        .Saturation = (Me.S - 50) * 2,
                        .Lightness = (Me.L - 50) * 2
                    }

                Dim img As Bitmap
                If Not IO.File.Exists(Image) Then Throw New IO.IOException("Couldn't Find image")
                Dim S As New IO.FileStream(Image, IO.FileMode.Open, IO.FileAccess.Read)
                img = System.Drawing.Image.FromStream(S)
                S.Close()
                S.Dispose()

                Dim path As String
                If Not My.WXP And Not My.WVista Then
                    path = IO.Path.Combine(My.Application.appData, "TintedWallpaper.bmp")
                    HSL.ExecuteFilter(img).Save(path, Imaging.ImageFormat.Bmp)

                Else
                    path = IO.Path.Combine(My.PATH_Windows, "Web\Wallpaper\TintedWallpaper.bmp")
                    HSL.ExecuteFilter(img).Save(path, Imaging.ImageFormat.Bmp)

                End If

                User32.SystemParametersInfo(SPI.Desktop.SETDESKWALLPAPER, 0, path, SPIF.SendChange Or SPIF.UpdateINIFile)
                Registry.CurrentUser.OpenSubKey("Control Panel\Desktop", True).SetValue("Wallpaper", path)

                MainFrm.Update_Wallpaper_Preview()
            End Sub

            Shared Function Load_WallpaperTone_From_ListOfString(tx As List(Of String)) As WallpaperTone
                If tx.Count > 0 Then
                    Dim WT As New WallpaperTone With {.Image = My.PATH_Windows & "\Web\Wallpaper\Windows\img0.jpg"}

                    For Each lin As String In tx
                        If lin.StartsWith("Enabled= ", My._strIgnore) Then WT.Enabled = lin.Remove(0, "Enabled= ".Count)
                        If lin.StartsWith("Image= ", My._strIgnore) Then WT.Image = lin.Remove(0, "Image= ".Count)
                        If lin.StartsWith("H= ", My._strIgnore) Then WT.H = lin.Remove(0, "H= ".Count)
                        If lin.StartsWith("S= ", My._strIgnore) Then WT.S = lin.Remove(0, "S= ".Count)
                        If lin.StartsWith("L= ", My._strIgnore) Then WT.L = lin.Remove(0, "L= ".Count)
                    Next

                    Return WT
                Else
                    Return New Structures.WallpaperTone With {
                            .Enabled = False,
                            .Image = My.PATH_Windows & "\Web\Wallpaper\Windows\img0.jpg",
                            .H = 0, .S = 50, .L = 50}
                End If
            End Function

            Overloads Function ToString(Signature As String) As String
                Dim tx As New List(Of String)
                tx.Clear()
                tx.Add(String.Format("<WallpaperTone_{0}>", Signature))
                tx.Add(String.Format("*WallpaperTone_{0}_Enabled= {1}", Signature, Enabled))
                tx.Add(String.Format("*WallpaperTone_{0}_Image= {1}", Signature, Image))
                tx.Add(String.Format("*WallpaperTone_{0}_H= {1}", Signature, H))
                tx.Add(String.Format("*WallpaperTone_{0}_S= {1}", Signature, S))
                tx.Add(String.Format("*WallpaperTone_{0}_L= {1}", Signature, L))
                tx.Add(String.Format("</WallpaperTone_{0}>", Signature) & vbCrLf)
                Return tx.CString
            End Function

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

                    Try
                        DisableAcrylicBackgroundOnLogon = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\System", "DisableAcrylicBackgroundOnLogon", _DefLogonUI.DisableAcrylicBackgroundOnLogon)
                    Catch
                        DisableAcrylicBackgroundOnLogon = _DefLogonUI.DisableAcrylicBackgroundOnLogon
                    End Try

                    Try
                        DisableLogonBackgroundImage = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\System", "DisableLogonBackgroundImage", _DefLogonUI.DisableLogonBackgroundImage)
                    Catch
                        DisableLogonBackgroundImage = _DefLogonUI.DisableLogonBackgroundImage
                    End Try

                    Try
                        NoLockScreen = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Windows\Personalization", "NoLockScreen", _DefLogonUI.NoLockScreen)
                    Catch
                        NoLockScreen = _DefLogonUI.NoLockScreen
                    End Try
                Else

                    DisableAcrylicBackgroundOnLogon = _DefLogonUI.DisableAcrylicBackgroundOnLogon
                    DisableLogonBackgroundImage = _DefLogonUI.DisableLogonBackgroundImage
                    NoLockScreen = _DefLogonUI.NoLockScreen
                End If
            End Sub

            Public Sub Apply()
                EditReg("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\System", "DisableAcrylicBackgroundOnLogon", DisableAcrylicBackgroundOnLogon.ToInteger)
                EditReg("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\System", "DisableLogonBackgroundImage", DisableLogonBackgroundImage.ToInteger)
                EditReg("HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Windows\Personalization", "NoLockScreen", NoLockScreen.ToInteger)
            End Sub

            Public Overrides Function ToString() As String
                Dim tx As New List(Of String)
                tx.Clear()
                tx.Add("<LogonUI_10_11>")
                tx.Add("*LogonUI_DisableAcrylicBackgroundOnLogon= " & DisableAcrylicBackgroundOnLogon)
                tx.Add("*LogonUI_DisableLogonBackgroundImage= " & DisableLogonBackgroundImage)
                tx.Add("*LogonUI_NoLockScreen= " & NoLockScreen)
                tx.Add("</LogonUI_10_11>" & vbCrLf)
                Return tx.CString
            End Function

        End Structure

        Structure LogonUI7 : Implements ICloneable
            Public Enabled As Boolean
            Public Mode As LogonUI_Modes
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

            Public Sub Load(_DefLogonUI As LogonUI7)
                If My.W7 Or My.W8 Then
                    Dim rLog As RegistryKey = Registry.CurrentUser.CreateSubKey("Software\WinPaletter\LogonUI")

                    Try
                        ImagePath = rLog.GetValue("ImagePath", "")
                    Catch
                        ImagePath = ""
                    End Try

                    Try
                        Color = Color.FromArgb(rLog.GetValue("Color", Color.Black.ToArgb))
                    Catch
                        Color = Color.Black
                    End Try

                    Try
                        Blur = rLog.GetValue("Blur", False)
                    Catch
                        Blur = False
                    End Try

                    Try
                        Blur_Intensity = rLog.GetValue("Blur_Intensity", 0)
                    Catch
                        Blur_Intensity = 0
                    End Try

                    Try
                        Grayscale = rLog.GetValue("Grayscale", False)
                    Catch
                        Grayscale = False
                    End Try

                    Try
                        Noise = rLog.GetValue("Noise", False)
                    Catch
                        Noise = False
                    End Try

                    Try
                        Noise_Mode = rLog.GetValue("Noise_Mode", NoiseMode.Acrylic)
                    Catch
                        Noise_Mode = NoiseMode.Acrylic
                    End Try

                    Try
                        Noise_Intensity = rLog.GetValue("Noise_Intensity", 0)
                    Catch
                        Noise_Intensity = 0
                    End Try

                    If My.W7 Then
                        Dim b1 As Boolean = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\Software\Microsoft\Windows\CurrentVersion\Authentication\LogonUI\Background", "OEMBackground", False)
                        Dim b2 As Boolean = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Windows\System", "UseOEMBackground", False)
                        Enabled = b1 Or b2

                        rLog = Registry.CurrentUser.CreateSubKey("Software\WinPaletter\LogonUI")

                        Try
                            Mode = rLog.GetValue("Mode", LogonUI_Modes.Default_)
                        Catch
                            Mode = LogonUI_Modes.Default_
                        End Try
                    End If

                    rLog.Close()

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

            Public Overrides Function ToString() As String
                Dim tx As New List(Of String)
                tx.Clear()
                tx.Add("<LogonUI_7_8>")
                tx.Add("*LogonUI7_Enabled= " & Enabled)
                tx.Add("*LogonUI7_Mode= " & Mode)
                tx.Add("*LogonUI7_ImagePath= " & ImagePath)
                tx.Add("*LogonUI7_Color= " & Color.ToArgb)
                tx.Add("*LogonUI7_Effect_Blur= " & Blur)
                tx.Add("*LogonUI7_Effect_Blur_Intensity= " & Blur_Intensity)
                tx.Add("*LogonUI7_Effect_Grayscale= " & Grayscale)
                tx.Add("*LogonUI7_Effect_Noise= " & Noise)
                tx.Add("*LogonUI7_Effect_Noise_Mode= " & Noise_Mode)
                tx.Add("*LogonUI7_Effect_Noise_Intensity= " & Noise_Intensity)
                tx.Add("</LogonUI_7_8>" & vbCrLf)
                Return tx.CString
            End Function

        End Structure

        Structure LogonUIXP : Implements ICloneable
            Public Enabled As Boolean
            Public Mode As LogonUIXP_Modes
            Public BackColor As Color
            Public ShowMoreOptions As Boolean

            Public Sub Load(_DefLogonUI As LogonUIXP)
                If My.WXP Then
                    Dim rMain_LXP As RegistryKey = Registry.CurrentUser.CreateSubKey("Software\WinPaletter\LogonUI\WinXP")
                    Enabled = rMain_LXP.GetValue("", _DefLogonUI.Enabled)
                    rMain_LXP.Close()

                    Try
                        Select Case My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon", "LogonType", _DefLogonUI.Mode)
                            Case 1
                                Mode = LogonUIXP_Modes.Default
                            Case Else
                                Mode = LogonUIXP_Modes.Win2000
                        End Select
                    Catch
                        Mode = _DefLogonUI.Mode
                    End Try

                    Try
                        With My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon", "Background", "0 0 0")
                            If .ToString.Split(" ").Count = 3 Then
                                BackColor = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                            Else
                                BackColor = _DefLogonUI.BackColor
                            End If
                        End With
                    Catch
                        BackColor = _DefLogonUI.BackColor
                    End Try

                    Try
                        ShowMoreOptions = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon", "ShowLogonOptions", _DefLogonUI.ShowMoreOptions)
                    Catch
                        ShowMoreOptions = _DefLogonUI.ShowMoreOptions
                    End Try

                Else
                    Mode = _DefLogonUI.Mode
                    BackColor = _DefLogonUI.BackColor
                    ShowMoreOptions = _DefLogonUI.ShowMoreOptions
                End If
            End Sub

            Sub Apply()
                Dim rMain As RegistryKey = Registry.CurrentUser.CreateSubKey("Software\WinPaletter\LogonUI\WinXP")
                rMain.SetValue("", Enabled, RegistryValueKind.DWord)
                rMain.Close()

                If Enabled And My.WXP Then
                    EditReg("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon", "LogonType", If(Mode = LogonUIXP_Modes.Default, 1, 0), RegistryValueKind.DWord)
                    EditReg("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon", "Background", BackColor.Win32_RegColor, RegistryValueKind.String)
                    EditReg("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon", "ShowLogonOptions", ShowMoreOptions.ToInteger, RegistryValueKind.DWord)
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

            Public Overrides Function ToString() As String
                Dim tx As New List(Of String)
                tx.Clear()
                tx.Add("<LogonUI_XP>")
                tx.Add("*LogonUIXP_Enabled= " & Enabled)
                tx.Add("*LogonUIXP_Mode= " & Mode)
                tx.Add("*LogonUIXP_BackColor= " & BackColor.ToArgb)
                tx.Add("*LogonUIXP_ShowMoreOptions= " & ShowMoreOptions)
                tx.Add("</LogonUI_XP>" & vbCrLf)
                Return tx.CString
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
                Dim y_cmd As Object
                Dim RegAddress As String = "HKEY_CURRENT_USER\Console" & If(String.IsNullOrEmpty([RegKey]), "", "\" & [RegKey])

                Try
                    y_cmd = My.Computer.Registry.GetValue(RegAddress, "ColorTable00", [Defaults].ColorTable00.Reverse.ToArgb)
                    ColorTable00 = Color.FromArgb(255, Color.FromArgb(y_cmd).Reverse)
                Catch
                    ColorTable00 = [Defaults].ColorTable00
                End Try

                Try
                    y_cmd = My.Computer.Registry.GetValue(RegAddress, "ColorTable01", [Defaults].ColorTable01.Reverse.ToArgb)
                    ColorTable01 = Color.FromArgb(255, Color.FromArgb(y_cmd).Reverse)
                Catch
                    ColorTable01 = [Defaults].ColorTable01
                End Try

                Try
                    y_cmd = My.Computer.Registry.GetValue(RegAddress, "ColorTable02", [Defaults].ColorTable02.Reverse.ToArgb)
                    ColorTable02 = Color.FromArgb(255, Color.FromArgb(y_cmd).Reverse)
                Catch
                    ColorTable02 = [Defaults].ColorTable02
                End Try

                Try
                    y_cmd = My.Computer.Registry.GetValue(RegAddress, "ColorTable03", [Defaults].ColorTable03.Reverse.ToArgb)
                    ColorTable03 = Color.FromArgb(255, Color.FromArgb(y_cmd).Reverse)
                Catch
                    ColorTable03 = [Defaults].ColorTable03
                End Try

                Try
                    y_cmd = My.Computer.Registry.GetValue(RegAddress, "ColorTable04", [Defaults].ColorTable04.Reverse.ToArgb)
                    ColorTable04 = Color.FromArgb(255, Color.FromArgb(y_cmd).Reverse)
                Catch
                    ColorTable04 = [Defaults].ColorTable04
                End Try

                Try
                    y_cmd = My.Computer.Registry.GetValue(RegAddress, "ColorTable05", [Defaults].ColorTable05.Reverse.ToArgb)
                    ColorTable05 = Color.FromArgb(255, Color.FromArgb(y_cmd).Reverse)
                Catch
                    ColorTable05 = [Defaults].ColorTable05
                End Try

                Try
                    y_cmd = My.Computer.Registry.GetValue(RegAddress, "ColorTable06", [Defaults].ColorTable06.Reverse.ToArgb)
                    ColorTable06 = Color.FromArgb(255, Color.FromArgb(y_cmd).Reverse)
                Catch
                    ColorTable06 = [Defaults].ColorTable06
                End Try

                Try
                    y_cmd = My.Computer.Registry.GetValue(RegAddress, "ColorTable07", [Defaults].ColorTable07.Reverse.ToArgb)
                    ColorTable07 = Color.FromArgb(255, Color.FromArgb(y_cmd).Reverse)
                Catch
                    ColorTable07 = [Defaults].ColorTable07
                End Try

                Try
                    y_cmd = My.Computer.Registry.GetValue(RegAddress, "ColorTable08", [Defaults].ColorTable08.Reverse.ToArgb)
                    ColorTable08 = Color.FromArgb(255, Color.FromArgb(y_cmd).Reverse)
                Catch
                    ColorTable08 = [Defaults].ColorTable08
                End Try

                Try
                    y_cmd = My.Computer.Registry.GetValue(RegAddress, "ColorTable09", [Defaults].ColorTable09.Reverse.ToArgb)
                    ColorTable09 = Color.FromArgb(255, Color.FromArgb(y_cmd).Reverse)
                Catch
                    ColorTable09 = [Defaults].ColorTable09
                End Try

                Try
                    y_cmd = My.Computer.Registry.GetValue(RegAddress, "ColorTable10", [Defaults].ColorTable10.Reverse.ToArgb)
                    ColorTable10 = Color.FromArgb(255, Color.FromArgb(y_cmd).Reverse)
                Catch
                    ColorTable10 = [Defaults].ColorTable10
                End Try

                Try
                    y_cmd = My.Computer.Registry.GetValue(RegAddress, "ColorTable11", [Defaults].ColorTable11.Reverse.ToArgb)
                    ColorTable11 = Color.FromArgb(255, Color.FromArgb(y_cmd).Reverse)
                Catch
                    ColorTable11 = [Defaults].ColorTable11
                End Try

                Try
                    y_cmd = My.Computer.Registry.GetValue(RegAddress, "ColorTable12", [Defaults].ColorTable12.Reverse.ToArgb)
                    ColorTable12 = Color.FromArgb(255, Color.FromArgb(y_cmd).Reverse)
                Catch
                    ColorTable12 = [Defaults].ColorTable12
                End Try

                Try
                    y_cmd = My.Computer.Registry.GetValue(RegAddress, "ColorTable13", [Defaults].ColorTable13.Reverse.ToArgb)
                    ColorTable13 = Color.FromArgb(255, Color.FromArgb(y_cmd).Reverse)
                Catch
                    ColorTable13 = [Defaults].ColorTable13
                End Try

                Try
                    y_cmd = My.Computer.Registry.GetValue(RegAddress, "ColorTable14", [Defaults].ColorTable14.Reverse.ToArgb)
                    ColorTable14 = Color.FromArgb(255, Color.FromArgb(y_cmd).Reverse)
                Catch
                    ColorTable14 = [Defaults].ColorTable14
                End Try

                Try
                    y_cmd = My.Computer.Registry.GetValue(RegAddress, "ColorTable15", [Defaults].ColorTable15.Reverse.ToArgb)
                    ColorTable15 = Color.FromArgb(255, Color.FromArgb(y_cmd).Reverse)
                Catch
                    ColorTable15 = [Defaults].ColorTable15
                End Try

                Try
                    y_cmd = My.Computer.Registry.GetValue(RegAddress, "PopupColors", Convert.ToInt32([Defaults].PopupBackground.ToString("X") & [Defaults].PopupForeground.ToString("X"), 16))
                    Dim d As String = CInt(y_cmd).ToString("X")

                    If d.Count = 1 Then d = 0 & d
                    PopupBackground = Convert.ToInt32(d.Chars(0), 16)
                    PopupForeground = Convert.ToInt32(d.Chars(1), 16)
                Catch
                    PopupBackground = [Defaults].PopupBackground
                    PopupForeground = [Defaults].PopupForeground
                End Try

                Try
                    y_cmd = My.Computer.Registry.GetValue(RegAddress, "ScreenColors", Convert.ToInt32([Defaults].ScreenColorsBackground.ToString("X") & [Defaults].ScreenColorsForeground.ToString("X"), 16))
                    Dim d As String = CInt(y_cmd).ToString("X")

                    If d.Count = 1 Then d = 0 & d
                    ScreenColorsBackground = Convert.ToInt32(d.Chars(0), 16)
                    ScreenColorsForeground = Convert.ToInt32(d.Chars(1), 16)
                Catch
                    ScreenColorsBackground = [Defaults].ScreenColorsBackground
                    ScreenColorsForeground = [Defaults].ScreenColorsForeground
                End Try

                Try
                    y_cmd = My.Computer.Registry.GetValue(RegAddress, "CursorSize", 25)
                    CursorSize = y_cmd
                Catch
                    CursorSize = 25
                End Try

                Try
                    y_cmd = My.Computer.Registry.GetValue(RegAddress, "FaceName", [Defaults].FaceName)

                    If IsFontInstalled(y_cmd) Then
                        FaceName = y_cmd
                    Else
                        FaceName = [Defaults].FaceName
                    End If

                Catch
                    FaceName = [Defaults].FaceName
                End Try

                Try
                    y_cmd = My.Computer.Registry.GetValue(RegAddress, "FontFamily", If(Not [Defaults].FontRaster, 54, 1))
                    FontRaster = If(y_cmd = 1 Or y_cmd = 0 Or y_cmd = 48, True, False)
                    If FaceName.ToLower = "terminal" Then FontRaster = True
                Catch
                    FontRaster = [Defaults].FontRaster
                End Try

                Try
                    y_cmd = My.Computer.Registry.GetValue(RegAddress, "FontSize", [Defaults].FontSize)
                    If y_cmd = 0 And Not FontRaster Then FontSize = [Defaults].FontSize Else FontSize = y_cmd
                Catch
                    FontSize = [Defaults].FontSize
                End Try


                Try
                    y_cmd = My.Computer.Registry.GetValue(RegAddress, "FontWeight", 400)
                    FontWeight = y_cmd
                Catch
                    FontWeight = 400
                End Try

                If My.W10_1909 Then
                    Try
                        y_cmd = My.Computer.Registry.GetValue(RegAddress, "CursorColor", Color.White.Reverse.ToArgb)
                        W10_1909_CursorColor = Color.FromArgb(255, Color.FromArgb(y_cmd).Reverse)
                    Catch
                        W10_1909_CursorColor = Color.White
                    End Try

                    Try
                        y_cmd = My.Computer.Registry.GetValue(RegAddress, "CursorType", 1)
                        W10_1909_CursorType = y_cmd
                    Catch
                        W10_1909_CursorType = 1
                    End Try

                    Try
                        y_cmd = My.Computer.Registry.GetValue(RegAddress, "ForceV2", True)
                        W10_1909_ForceV2 = y_cmd
                    Catch
                        W10_1909_ForceV2 = True
                    End Try

                    Try
                        y_cmd = My.Computer.Registry.GetValue(RegAddress, "LineSelection", False)
                        W10_1909_LineSelection = y_cmd
                    Catch
                        W10_1909_LineSelection = False
                    End Try


                    Try
                        y_cmd = My.Computer.Registry.GetValue(RegAddress, "TerminalScrolling", False)
                        W10_1909_TerminalScrolling = y_cmd
                    Catch
                        W10_1909_TerminalScrolling = False
                    End Try


                    Try
                        y_cmd = My.Computer.Registry.GetValue(RegAddress, "WindowAlpha", 255)
                        W10_1909_WindowAlpha = y_cmd
                    Catch
                        W10_1909_WindowAlpha = 255
                    End Try
                End If

                Dim rLogX As RegistryKey = Registry.CurrentUser.CreateSubKey("Software\WinPaletter\Terminals")

                Try
                    Enabled = CInt(rLogX.GetValue(Signature_Of_Enable, 0)).ToBoolean
                Catch
                    Enabled = False
                End Try

            End Sub
            Shared Sub Save_Console_To_Registry([RegKey] As String, [Console] As Console)



                Dim RegAddress As String = "HKEY_CURRENT_USER\Console" & If(String.IsNullOrEmpty([RegKey]), "", "\" & [RegKey])

                Try
                    If Not String.IsNullOrEmpty([RegKey]) Then Registry.CurrentUser.CreateSubKey("Console\" & [RegKey], True).Close()
                Catch

                End Try

                EditReg(RegAddress, "EnableColorSelection", 1)
                EditReg(RegAddress, "ColorTable00", Color.FromArgb(0, [Console].ColorTable00.Reverse).ToArgb)
                EditReg(RegAddress, "ColorTable01", Color.FromArgb(0, [Console].ColorTable01.Reverse).ToArgb)
                EditReg(RegAddress, "ColorTable02", Color.FromArgb(0, [Console].ColorTable02.Reverse).ToArgb)
                EditReg(RegAddress, "ColorTable03", Color.FromArgb(0, [Console].ColorTable03.Reverse).ToArgb)
                EditReg(RegAddress, "ColorTable04", Color.FromArgb(0, [Console].ColorTable04.Reverse).ToArgb)
                EditReg(RegAddress, "ColorTable05", Color.FromArgb(0, [Console].ColorTable05.Reverse).ToArgb)
                EditReg(RegAddress, "ColorTable06", Color.FromArgb(0, [Console].ColorTable06.Reverse).ToArgb)
                EditReg(RegAddress, "ColorTable07", Color.FromArgb(0, [Console].ColorTable07.Reverse).ToArgb)
                EditReg(RegAddress, "ColorTable08", Color.FromArgb(0, [Console].ColorTable08.Reverse).ToArgb)
                EditReg(RegAddress, "ColorTable09", Color.FromArgb(0, [Console].ColorTable09.Reverse).ToArgb)
                EditReg(RegAddress, "ColorTable10", Color.FromArgb(0, [Console].ColorTable10.Reverse).ToArgb)
                EditReg(RegAddress, "ColorTable11", Color.FromArgb(0, [Console].ColorTable11.Reverse).ToArgb)
                EditReg(RegAddress, "ColorTable12", Color.FromArgb(0, [Console].ColorTable12.Reverse).ToArgb)
                EditReg(RegAddress, "ColorTable13", Color.FromArgb(0, [Console].ColorTable13.Reverse).ToArgb)
                EditReg(RegAddress, "ColorTable14", Color.FromArgb(0, [Console].ColorTable14.Reverse).ToArgb)
                EditReg(RegAddress, "ColorTable15", Color.FromArgb(0, [Console].ColorTable15.Reverse).ToArgb)
                EditReg(RegAddress, "PopupColors", Convert.ToInt32([Console].PopupBackground.ToString("X") & [Console].PopupForeground.ToString("X"), 16))
                EditReg(RegAddress, "ScreenColors", Convert.ToInt32([Console].ScreenColorsBackground.ToString("X") & [Console].ScreenColorsForeground.ToString("X"), 16))
                EditReg(RegAddress, "CursorSize", [Console].CursorSize)

                If [Console].FontRaster Then
                    EditReg(RegAddress, "FaceName", "Terminal", RegistryValueKind.String)
                    EditReg(RegAddress, "FontFamily", 48)
                Else
                    EditReg(RegAddress, "FaceName", [Console].FaceName, RegistryValueKind.String)
                    EditReg(RegAddress, "FontFamily", If([Console].FontRaster, 1, 54))
                End If

                EditReg(RegAddress, "FontSize", [Console].FontSize)
                EditReg(RegAddress, "FontWeight", [Console].FontWeight)

                If My.W10_1909 Then
                    EditReg(RegAddress, "CursorColor", Color.FromArgb(0, [Console].W10_1909_CursorColor.Reverse).ToArgb)
                    EditReg(RegAddress, "CursorType", [Console].W10_1909_CursorType)
                    EditReg(RegAddress, "WindowAlpha", [Console].W10_1909_WindowAlpha)
                    EditReg(RegAddress, "ForceV2", [Console].W10_1909_ForceV2.ToInteger)
                    EditReg(RegAddress, "LineSelection", [Console].W10_1909_LineSelection.ToInteger)
                    EditReg(RegAddress, "TerminalScrolling", [Console].W10_1909_TerminalScrolling.ToInteger)
                End If

                EditReg("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Console\TrueTypeFont", "000", [Console].FaceName, RegistryValueKind.String)

            End Sub
            Overloads Function ToString(Signature As String) As String
                Dim tx As New List(Of String)
                tx.Clear()
                tx.Add(String.Format("<{0}>", Signature))
                tx.Add(String.Format("*Terminal_{0}_Enabled= {1}", Signature, Enabled))
                tx.Add(String.Format("*{0}_ColorTable00= {1}", Signature, ColorTable00.ToArgb))
                tx.Add(String.Format("*{0}_ColorTable01= {1}", Signature, ColorTable01.ToArgb))
                tx.Add(String.Format("*{0}_ColorTable02= {1}", Signature, ColorTable02.ToArgb))
                tx.Add(String.Format("*{0}_ColorTable03= {1}", Signature, ColorTable03.ToArgb))
                tx.Add(String.Format("*{0}_ColorTable04= {1}", Signature, ColorTable04.ToArgb))
                tx.Add(String.Format("*{0}_ColorTable05= {1}", Signature, ColorTable05.ToArgb))
                tx.Add(String.Format("*{0}_ColorTable06= {1}", Signature, ColorTable06.ToArgb))
                tx.Add(String.Format("*{0}_ColorTable07= {1}", Signature, ColorTable07.ToArgb))
                tx.Add(String.Format("*{0}_ColorTable08= {1}", Signature, ColorTable08.ToArgb))
                tx.Add(String.Format("*{0}_ColorTable09= {1}", Signature, ColorTable09.ToArgb))
                tx.Add(String.Format("*{0}_ColorTable10= {1}", Signature, ColorTable10.ToArgb))
                tx.Add(String.Format("*{0}_ColorTable11= {1}", Signature, ColorTable11.ToArgb))
                tx.Add(String.Format("*{0}_ColorTable12= {1}", Signature, ColorTable12.ToArgb))
                tx.Add(String.Format("*{0}_ColorTable13= {1}", Signature, ColorTable13.ToArgb))
                tx.Add(String.Format("*{0}_ColorTable14= {1}", Signature, ColorTable14.ToArgb))
                tx.Add(String.Format("*{0}_ColorTable15= {1}", Signature, ColorTable15.ToArgb))
                tx.Add(String.Format("*{0}_PopupForeground= {1}", Signature, PopupForeground))
                tx.Add(String.Format("*{0}_PopupBackground= {1}", Signature, PopupBackground))
                tx.Add(String.Format("*{0}_ScreenColorsForeground= {1}", Signature, ScreenColorsForeground))
                tx.Add(String.Format("*{0}_ScreenColorsBackground= {1}", Signature, ScreenColorsBackground))
                tx.Add(String.Format("*{0}_CursorSize= {1}", Signature, CursorSize))
                tx.Add(String.Format("*{0}_FaceName= {1}", Signature, FaceName))
                tx.Add(String.Format("*{0}_FontRaster= {1}", Signature, FontRaster))
                tx.Add(String.Format("*{0}_FontSize= {1}", Signature, FontSize))
                tx.Add(String.Format("*{0}_FontWeight= {1}", Signature, FontWeight))
                tx.Add(String.Format("*{0}_1909_CursorType= {1}", Signature, W10_1909_CursorType))
                tx.Add(String.Format("*{0}_1909_CursorColor= {1}", Signature, W10_1909_CursorColor.ToArgb))
                tx.Add(String.Format("*{0}_1909_ForceV2= {1}", Signature, W10_1909_ForceV2))
                tx.Add(String.Format("*{0}_1909_LineSelection= {1}", Signature, W10_1909_LineSelection))
                tx.Add(String.Format("*{0}_1909_TerminalScrolling= {1}", Signature, W10_1909_TerminalScrolling))
                tx.Add(String.Format("*{0}_1909_WindowAlpha= {1}", Signature, W10_1909_WindowAlpha))
                tx.Add(String.Format("</{0}>", Signature) & vbCrLf)
                Return tx.CString
            End Function
            Shared Function Load_Console_From_ListOfString(tx As List(Of String)) As Console
                Dim [Console] As New Console

                For Each lin As String In tx
                    If lin.StartsWith("ColorTable00= ", My._strIgnore) Then [Console].ColorTable00 = Color.FromArgb(lin.ToLower.Remove(0, "ColorTable00= ".Count))
                    If lin.StartsWith("ColorTable01= ", My._strIgnore) Then [Console].ColorTable01 = Color.FromArgb(lin.ToLower.Remove(0, "ColorTable01= ".Count))
                    If lin.StartsWith("ColorTable02= ", My._strIgnore) Then [Console].ColorTable02 = Color.FromArgb(lin.ToLower.Remove(0, "ColorTable02= ".Count))
                    If lin.StartsWith("ColorTable03= ", My._strIgnore) Then [Console].ColorTable03 = Color.FromArgb(lin.ToLower.Remove(0, "ColorTable03= ".Count))
                    If lin.StartsWith("ColorTable04= ", My._strIgnore) Then [Console].ColorTable04 = Color.FromArgb(lin.ToLower.Remove(0, "ColorTable04= ".Count))
                    If lin.StartsWith("ColorTable05= ", My._strIgnore) Then [Console].ColorTable05 = Color.FromArgb(lin.ToLower.Remove(0, "ColorTable05= ".Count))
                    If lin.StartsWith("ColorTable06= ", My._strIgnore) Then [Console].ColorTable06 = Color.FromArgb(lin.ToLower.Remove(0, "ColorTable06= ".Count))
                    If lin.StartsWith("ColorTable07= ", My._strIgnore) Then [Console].ColorTable07 = Color.FromArgb(lin.ToLower.Remove(0, "ColorTable07= ".Count))
                    If lin.StartsWith("ColorTable08= ", My._strIgnore) Then [Console].ColorTable08 = Color.FromArgb(lin.ToLower.Remove(0, "ColorTable08= ".Count))
                    If lin.StartsWith("ColorTable09= ", My._strIgnore) Then [Console].ColorTable09 = Color.FromArgb(lin.ToLower.Remove(0, "ColorTable09= ".Count))
                    If lin.StartsWith("ColorTable10= ", My._strIgnore) Then [Console].ColorTable10 = Color.FromArgb(lin.ToLower.Remove(0, "ColorTable10= ".Count))
                    If lin.StartsWith("ColorTable11= ", My._strIgnore) Then [Console].ColorTable11 = Color.FromArgb(lin.ToLower.Remove(0, "ColorTable11= ".Count))
                    If lin.StartsWith("ColorTable12= ", My._strIgnore) Then [Console].ColorTable12 = Color.FromArgb(lin.ToLower.Remove(0, "ColorTable12= ".Count))
                    If lin.StartsWith("ColorTable13= ", My._strIgnore) Then [Console].ColorTable13 = Color.FromArgb(lin.ToLower.Remove(0, "ColorTable13= ".Count))
                    If lin.StartsWith("ColorTable14= ", My._strIgnore) Then [Console].ColorTable14 = Color.FromArgb(lin.ToLower.Remove(0, "ColorTable14= ".Count))
                    If lin.StartsWith("ColorTable15= ", My._strIgnore) Then [Console].ColorTable15 = Color.FromArgb(lin.ToLower.Remove(0, "ColorTable15= ".Count))
                    If lin.StartsWith("PopupForeground= ", My._strIgnore) Then [Console].PopupForeground = lin.ToLower.Remove(0, "PopupForeground= ".Count)
                    If lin.StartsWith("PopupBackground= ", My._strIgnore) Then [Console].PopupBackground = lin.ToLower.Remove(0, "PopupBackground= ".Count)
                    If lin.StartsWith("ScreenColorsForeground= ", My._strIgnore) Then [Console].ScreenColorsForeground = lin.ToLower.Remove(0, "ScreenColorsForeground= ".Count)
                    If lin.StartsWith("ScreenColorsBackground= ", My._strIgnore) Then [Console].ScreenColorsBackground = lin.ToLower.Remove(0, "ScreenColorsBackground= ".Count)
                    If lin.StartsWith("CursorSize= ", My._strIgnore) Then [Console].CursorSize = lin.ToLower.Remove(0, "CursorSize= ".Count)
                    If lin.StartsWith("FaceName= ", My._strIgnore) Then [Console].FaceName = lin.ToLower.Remove(0, "FaceName= ".Count)
                    If lin.StartsWith("FontRaster= ", My._strIgnore) Then [Console].FontRaster = lin.ToLower.Remove(0, "FontRaster= ".Count)
                    If lin.StartsWith("FontSize= ", My._strIgnore) Then [Console].FontSize = lin.ToLower.Remove(0, "FontSize= ".Count)
                    If lin.StartsWith("FontWeight= ", My._strIgnore) Then [Console].FontWeight = lin.ToLower.Remove(0, "FontWeight= ".Count)
                    If lin.StartsWith("1909_CursorType= ", My._strIgnore) Then [Console].W10_1909_CursorType = lin.ToLower.Remove(0, "1909_CursorType= ".Count)
                    If lin.StartsWith("1909_CursorColor= ", My._strIgnore) Then [Console].W10_1909_CursorColor = Color.FromArgb(lin.ToLower.Remove(0, "1909_CursorColor= ".Count))
                    If lin.StartsWith("1909_ForceV2= ", My._strIgnore) Then [Console].W10_1909_ForceV2 = lin.ToLower.Remove(0, "1909_ForceV2= ".Count)
                    If lin.StartsWith("1909_lin.ToLowereSelection= ", My._strIgnore) Then [Console].W10_1909_LineSelection = lin.ToLower.Remove(0, "1909_lin.ToLowereSelection= ".Count)
                    If lin.StartsWith("1909_TerminalScrollin.ToLowerg= ", My._strIgnore) Then [Console].W10_1909_TerminalScrolling = lin.ToLower.Remove(0, "1909_TerminalScrollin.ToLowerg= ".Count)
                    If lin.StartsWith("1909_WindowAlpha= ", My._strIgnore) Then [Console].W10_1909_WindowAlpha = lin.ToLower.Remove(0, "1909_WindowAlpha= ".Count)
                Next

                Return [Console]
            End Function

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

            Public Sub Load([subKey] As String)
                Dim rMain As RegistryKey = Registry.CurrentUser.CreateSubKey("Software\WinPaletter\Cursors")

                Dim r As RegistryKey = rMain
                r.CreateSubKey([subKey])
                r = r.OpenSubKey([subKey])

                PrimaryColor1 = Color.FromArgb(r.GetValue("PrimaryColor1", Color.White.ToArgb))
                PrimaryColor2 = Color.FromArgb(r.GetValue("PrimaryColor2", Color.White.ToArgb))
                SecondaryColor1 = Color.FromArgb(r.GetValue("SecondaryColor1", If([subKey].ToLower <> "none", Color.Black, Color.Red).ToArgb))
                SecondaryColor2 = Color.FromArgb(r.GetValue("SecondaryColor2", If([subKey].ToLower <> "none", Color.FromArgb(64, 65, 75), Color.Red).ToArgb))
                LoadingCircleBack1 = Color.FromArgb(r.GetValue("LoadingCircleBack1", Color.FromArgb(42, 151, 243).ToArgb))
                LoadingCircleBack2 = Color.FromArgb(r.GetValue("LoadingCircleBack2", Color.FromArgb(42, 151, 243).ToArgb))
                LoadingCircleHot1 = Color.FromArgb(r.GetValue("LoadingCircleHot1", Color.FromArgb(37, 204, 255).ToArgb))
                LoadingCircleHot2 = Color.FromArgb(r.GetValue("LoadingCircleHot2", Color.FromArgb(37, 204, 255).ToArgb))

                PrimaryColorGradient = r.GetValue("PrimaryColorGradient", False)
                SecondaryColorGradient = r.GetValue("SecondaryColorGradient", True)
                LoadingCircleBackGradient = r.GetValue("LoadingCircleBackGradient", False)
                LoadingCircleHotGradient = r.GetValue("LoadingCircleHotGradient", False)

                PrimaryColorNoise = r.GetValue("PrimaryColorNoise", False)
                SecondaryColorNoise = r.GetValue("SecondaryColorNoise", False)
                LoadingCircleBackNoise = r.GetValue("LoadingCircleBackNoise", False)
                LoadingCircleHotNoise = r.GetValue("LoadingCircleHotNoise", False)

                PrimaryColorGradientMode = ReturnGradientModeFromString(r.GetValue("PrimaryColorGradientMode", "Circle"))
                SecondaryColorGradientMode = ReturnGradientModeFromString(r.GetValue("SecondaryColorGradientMode", "Vertical"))
                LoadingCircleBackGradientMode = ReturnGradientModeFromString(r.GetValue("LoadingCircleBackGradientMode", "Circle"))
                LoadingCircleHotGradientMode = ReturnGradientModeFromString(r.GetValue("LoadingCircleHotGradientMode", "Circle"))

                PrimaryColorNoiseOpacity = r.GetValue("PrimaryColorNoiseOpacity", 25) / 100
                SecondaryColorNoiseOpacity = r.GetValue("SecondaryColorNoiseOpacity", 25) / 100
                LoadingCircleBackNoiseOpacity = r.GetValue("LoadingCircleBackNoiseOpacity", 25) / 100
                LoadingCircleHotNoiseOpacity = r.GetValue("LoadingCircleHotNoiseOpacity", 25) / 100

                r.Close()
            End Sub

            Shared Sub Save_Cursors_To_Registry(subKey As String, [Cursor] As Cursor)

                Dim rMain As RegistryKey = Registry.CurrentUser.CreateSubKey("Software\WinPaletter\Cursors")
                Dim r As RegistryKey

                r = rMain.CreateSubKey(subKey)
                With r
                    .SetValue("PrimaryColor1", [Cursor].PrimaryColor1.ToArgb, RegistryValueKind.QWord)
                    .SetValue("PrimaryColor2", [Cursor].PrimaryColor2.ToArgb, RegistryValueKind.QWord)
                    .SetValue("PrimaryColorGradient", [Cursor].PrimaryColorGradient.ToInteger, RegistryValueKind.QWord)
                    .SetValue("PrimaryColorGradientMode", [Cursor].PrimaryColorGradientMode, RegistryValueKind.String)
                    .SetValue("PrimaryColorNoise", [Cursor].PrimaryColorNoise.ToInteger, RegistryValueKind.QWord)
                    .SetValue("PrimaryColorNoiseOpacity", [Cursor].PrimaryColorNoiseOpacity * 100, RegistryValueKind.QWord)
                    .SetValue("SecondaryColor1", [Cursor].SecondaryColor1.ToArgb, RegistryValueKind.QWord)
                    .SetValue("SecondaryColor2", [Cursor].SecondaryColor2.ToArgb, RegistryValueKind.QWord)
                    .SetValue("SecondaryColorGradient", [Cursor].SecondaryColorGradient.ToInteger, RegistryValueKind.QWord)
                    .SetValue("SecondaryColorGradientMode", [Cursor].SecondaryColorGradientMode, RegistryValueKind.String)
                    .SetValue("SecondaryColorNoise", [Cursor].SecondaryColorNoise.ToInteger, RegistryValueKind.QWord)
                    .SetValue("SecondaryColorNoiseOpacity", [Cursor].SecondaryColorNoiseOpacity * 100, RegistryValueKind.QWord)
                    .SetValue("LoadingCircleBack1", [Cursor].LoadingCircleBack1.ToArgb, RegistryValueKind.QWord)
                    .SetValue("LoadingCircleBack2", [Cursor].LoadingCircleBack2.ToArgb, RegistryValueKind.QWord)
                    .SetValue("LoadingCircleBackGradient", [Cursor].LoadingCircleBackGradient.ToInteger, RegistryValueKind.QWord)
                    .SetValue("LoadingCircleBackGradientMode", [Cursor].LoadingCircleBackGradientMode, RegistryValueKind.String)
                    .SetValue("LoadingCircleBackNoise", [Cursor].LoadingCircleBackNoise.ToInteger, RegistryValueKind.QWord)
                    .SetValue("LoadingCircleBackNoiseOpacity", [Cursor].LoadingCircleBackNoiseOpacity * 100, RegistryValueKind.QWord)
                    .SetValue("LoadingCircleHot1", [Cursor].LoadingCircleHot1.ToArgb, RegistryValueKind.QWord)
                    .SetValue("LoadingCircleHot2", [Cursor].LoadingCircleHot2.ToArgb, RegistryValueKind.QWord)
                    .SetValue("LoadingCircleHotGradient", [Cursor].LoadingCircleHotGradient.ToInteger, RegistryValueKind.QWord)
                    .SetValue("LoadingCircleHotGradientMode", [Cursor].LoadingCircleHotGradientMode, RegistryValueKind.String)
                    .SetValue("LoadingCircleHotNoise", [Cursor].LoadingCircleHotNoise.ToInteger, RegistryValueKind.QWord)
                    .SetValue("LoadingCircleHotNoiseOpacity", [Cursor].LoadingCircleHotNoiseOpacity * 100, RegistryValueKind.QWord)
                End With

                r.Close()
                rMain.Close()

            End Sub

            Shared Function Load_Cursor_From_ListOfString(tx As List(Of String)) As Cursor
                If tx.Count > 0 Then
                    Dim [Cursor] As New Cursor

                    For Each lin As String In tx
                        If lin.StartsWith("PrimaryColor1= ", My._strIgnore) Then [Cursor].PrimaryColor1 = Color.FromArgb(lin.Remove(0, "PrimaryColor1= ".Count))
                        If lin.StartsWith("PrimaryColor2= ", My._strIgnore) Then [Cursor].PrimaryColor2 = Color.FromArgb(lin.Remove(0, "PrimaryColor2= ".Count))
                        If lin.StartsWith("PrimaryColorGradient= ", My._strIgnore) Then [Cursor].PrimaryColorGradient = lin.Remove(0, "PrimaryColorGradient= ".Count)
                        If lin.StartsWith("PrimaryColorGradientMode= ", My._strIgnore) Then [Cursor].PrimaryColorGradientMode = ReturnGradientModeFromString(lin.Remove(0, "PrimaryColorGradientMode= ".Count))
                        If lin.StartsWith("PrimaryColorNoise= ", My._strIgnore) Then [Cursor].PrimaryColorNoise = lin.Remove(0, "PrimaryColorNoise= ".Count)
                        If lin.StartsWith("PrimaryColorNoiseOpacity= ", My._strIgnore) Then [Cursor].PrimaryColorNoiseOpacity = lin.Remove(0, "PrimaryColorNoiseOpacity= ".Count)
                        If lin.StartsWith("SecondaryColor1= ", My._strIgnore) Then [Cursor].SecondaryColor1 = Color.FromArgb(lin.Remove(0, "SecondaryColor1= ".Count))
                        If lin.StartsWith("SecondaryColor2= ", My._strIgnore) Then [Cursor].SecondaryColor2 = Color.FromArgb(lin.Remove(0, "SecondaryColor2= ".Count))
                        If lin.StartsWith("SecondaryColorGradient= ", My._strIgnore) Then [Cursor].SecondaryColorGradient = lin.Remove(0, "SecondaryColorGradient= ".Count)
                        If lin.StartsWith("SecondaryColorGradientMode= ", My._strIgnore) Then [Cursor].SecondaryColorGradientMode = ReturnGradientModeFromString(lin.Remove(0, "SecondaryColorGradientMode= ".Count))
                        If lin.StartsWith("SecondaryColorNoise= ", My._strIgnore) Then [Cursor].SecondaryColorNoise = lin.Remove(0, "SecondaryColorNoise= ".Count)
                        If lin.StartsWith("SecondaryColorNoiseOpacity= ", My._strIgnore) Then [Cursor].SecondaryColorNoiseOpacity = lin.Remove(0, "SecondaryColorNoiseOpacity= ".Count)
                        If lin.StartsWith("LoadingCircleBack1= ", My._strIgnore) Then [Cursor].LoadingCircleBack1 = Color.FromArgb(lin.Remove(0, "LoadingCircleBack1= ".Count))
                        If lin.StartsWith("LoadingCircleBack2= ", My._strIgnore) Then [Cursor].LoadingCircleBack2 = Color.FromArgb(lin.Remove(0, "LoadingCircleBack2= ".Count))
                        If lin.StartsWith("LoadingCircleBackGradient= ", My._strIgnore) Then [Cursor].LoadingCircleBackGradient = lin.Remove(0, "LoadingCircleBackGradient= ".Count)
                        If lin.StartsWith("LoadingCircleBackGradientMode= ", My._strIgnore) Then [Cursor].LoadingCircleBackGradientMode = ReturnGradientModeFromString(lin.Remove(0, "LoadingCircleBackGradientMode= ".Count))
                        If lin.StartsWith("LoadingCircleBackNoise= ", My._strIgnore) Then [Cursor].LoadingCircleBackNoise = lin.Remove(0, "LoadingCircleBackNoise= ".Count)
                        If lin.StartsWith("LoadingCircleBackNoiseOpacity= ", My._strIgnore) Then [Cursor].LoadingCircleBackNoiseOpacity = lin.Remove(0, "LoadingCircleBackNoiseOpacity= ".Count)
                        If lin.StartsWith("LoadingCircleHot1= ", My._strIgnore) Then [Cursor].LoadingCircleHot1 = Color.FromArgb(lin.Remove(0, "LoadingCircleHot1= ".Count))
                        If lin.StartsWith("LoadingCircleHot2= ", My._strIgnore) Then [Cursor].LoadingCircleHot2 = Color.FromArgb(lin.Remove(0, "LoadingCircleHot2= ".Count))
                        If lin.StartsWith("LoadingCircleHotGradient= ", My._strIgnore) Then [Cursor].LoadingCircleHotGradient = lin.Remove(0, "LoadingCircleHotGradient= ".Count)
                        If lin.StartsWith("LoadingCircleHotGradientMode= ", My._strIgnore) Then [Cursor].LoadingCircleHotGradientMode = ReturnGradientModeFromString(lin.Remove(0, "LoadingCircleHotGradientMode= ".Count))
                        If lin.StartsWith("LoadingCircleHotNoise= ", My._strIgnore) Then [Cursor].LoadingCircleHotNoise = lin.Remove(0, "LoadingCircleHotNoise= ".Count)
                        If lin.StartsWith("LoadingCircleHotNoiseOpacity= ", My._strIgnore) Then [Cursor].LoadingCircleHotNoiseOpacity = lin.Remove(0, "LoadingCircleHotNoiseOpacity= ".Count)
                    Next

                    Return [Cursor]
                End If
            End Function

            Overloads Function ToString(Signature As String) As String
                Dim tx As New List(Of String)
                tx.Clear()
                tx.Add(String.Format("<{0}>", Signature))
                tx.Add(String.Format("*Cursor_{0}_PrimaryColor1= {1}", Signature, PrimaryColor1.ToArgb))
                tx.Add(String.Format("*Cursor_{0}_PrimaryColor2= {1}", Signature, PrimaryColor2.ToArgb))
                tx.Add(String.Format("*Cursor_{0}_PrimaryColorGradient= {1}", Signature, PrimaryColorGradient))
                tx.Add(String.Format("*Cursor_{0}_PrimaryColorGradientMode= {1}", Signature, ReturnStringFromGradientMode(PrimaryColorGradientMode)))
                tx.Add(String.Format("*Cursor_{0}_PrimaryColorNoise= {1}", Signature, PrimaryColorNoise))
                tx.Add(String.Format("*Cursor_{0}_PrimaryColorNoiseOpacity= {1}", Signature, PrimaryColorNoiseOpacity))
                tx.Add(String.Format("*Cursor_{0}_SecondaryColor1= {1}", Signature, SecondaryColor1.ToArgb))
                tx.Add(String.Format("*Cursor_{0}_SecondaryColor2= {1}", Signature, SecondaryColor2.ToArgb))
                tx.Add(String.Format("*Cursor_{0}_SecondaryColorGradient= {1}", Signature, SecondaryColorGradient))
                tx.Add(String.Format("*Cursor_{0}_SecondaryColorGradientMode= {1}", Signature, ReturnStringFromGradientMode(SecondaryColorGradientMode)))
                tx.Add(String.Format("*Cursor_{0}_SecondaryColorNoise= {1}", Signature, SecondaryColorNoise))
                tx.Add(String.Format("*Cursor_{0}_SecondaryColorNoiseOpacity= {1}", Signature, SecondaryColorNoiseOpacity))
                tx.Add(String.Format("*Cursor_{0}_LoadingCircleBack1= {1}", Signature, LoadingCircleBack1.ToArgb))
                tx.Add(String.Format("*Cursor_{0}_LoadingCircleBack2= {1}", Signature, LoadingCircleBack2.ToArgb))
                tx.Add(String.Format("*Cursor_{0}_LoadingCircleBackGradient= {1}", Signature, LoadingCircleBackGradient))
                tx.Add(String.Format("*Cursor_{0}_LoadingCircleBackGradientMode= {1}", Signature, ReturnStringFromGradientMode(LoadingCircleBackGradientMode)))
                tx.Add(String.Format("*Cursor_{0}_LoadingCircleBackNoise= {1}", Signature, LoadingCircleBackNoise))
                tx.Add(String.Format("*Cursor_{0}_LoadingCircleBackNoiseOpacity= {1}", Signature, LoadingCircleBackNoiseOpacity))
                tx.Add(String.Format("*Cursor_{0}_LoadingCircleHot1= {1}", Signature, LoadingCircleHot1.ToArgb))
                tx.Add(String.Format("*Cursor_{0}_LoadingCircleHot2= {1}", Signature, LoadingCircleHot2.ToArgb))
                tx.Add(String.Format("*Cursor_{0}_LoadingCircleHotGradient= {1}", Signature, LoadingCircleHotGradient))
                tx.Add(String.Format("*Cursor_{0}_LoadingCircleHotGradientMode= {1}", Signature, ReturnStringFromGradientMode(LoadingCircleHotGradientMode)))
                tx.Add(String.Format("*Cursor_{0}_LoadingCircleHotNoise= {1}", Signature, LoadingCircleHotNoise))
                tx.Add(String.Format("*Cursor_{0}_LoadingCircleHotNoiseOpacity= {1}", Signature, LoadingCircleHotNoiseOpacity))
                tx.Add(String.Format("</{0}>", Signature) & vbCrLf)
                Return tx.CString
            End Function

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
            .AppVersion = My.Application.Info.Version.ToString,
            .PaletteName = "Current Mode",
            .PaletteDescription = "",
            .PaletteVersion = "1.0.0.0",
            .Author = Environment.UserName,
            .AuthorSocialMediaLink = ""
    }

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
            .Titlebar_Inactive = Color.FromArgb(0, 0, 0),
            .StartMenu_Accent = Color.FromArgb(0, 103, 192),
            .WinMode_Light = True,
            .AppMode_Light = True,
            .Transparency = True,
            .ApplyAccentonTitlebars = False,
            .ApplyAccentonTaskbar = CP.ApplyAccentonTaskbar_Level.None
            }

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
            .Titlebar_Inactive = Color.FromArgb(0, 0, 0),
            .StartMenu_Accent = Color.FromArgb(0, 90, 158),
            .WinMode_Light = False,
            .AppMode_Light = True,
            .Transparency = True,
            .ApplyAccentonTitlebars = False,
            .ApplyAccentonTaskbar = CP.ApplyAccentonTaskbar_Level.None
            }

    Public LogonUI10x As New Structures.LogonUI10x With {
        .DisableAcrylicBackgroundOnLogon = False, .DisableLogonBackgroundImage = False, .NoLockScreen = False}

    Public Windows8 As New Structures.Windows8 With {
                    .ColorizationColor = Color.FromArgb(246, 195, 74),
                    .ColorizationColorBalance = 78,
                    .Start = 0,
                    .StartColor = Color.FromArgb(30, 0, 84),
                    .AccentColor = Color.FromArgb(72, 29, 178),
                    .Theme = AeroTheme.Aero,
                    .LogonUI = 0,
                    .PersonalColors_Background = Color.FromArgb(30, 0, 84),
                    .PersonalColors_Accent = Color.FromArgb(72, 29, 178),
                    .NoLockScreen = False,
                    .LockScreenType = LogonUI_Modes.Default_,
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
            .Theme = CP.AeroTheme.Aero}

    Public WindowsVista As New Structures.WindowsVista With {
            .ColorizationColor = Color.FromArgb(64, 158, 254),
            .Theme = CP.AeroTheme.Aero}

    Public WindowsXP As New Structures.WindowsXP With {
        .Theme = WinXPTheme.LunaBlue,
        .ColorScheme = "NormalColor",
        .ThemeFile = My.PATH_Windows & "\resources\Themes\Luna\Luna.msstyles"}

    Public LogonUI7 As New Structures.LogonUI7 With {
                    .Enabled = False,
                    .Mode = LogonUI_Modes.Default_,
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
        .Mode = LogonUIXP_Modes.Default,
        .BackColor = Color.Black,
        .ShowMoreOptions = False}

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

    Public WallpaperTone_W11 As New Structures.WallpaperTone With {
        .Enabled = False,
        .Image = My.PATH_Windows & "\Web\Wallpaper\Windows\img0.jpg",
        .H = 0, .S = 50, .L = 50}

    Public WallpaperTone_W10 As New Structures.WallpaperTone With {
        .Enabled = False,
        .Image = My.PATH_Windows & "\Web\Wallpaper\Windows\img0.jpg",
        .H = 0, .S = 50, .L = 50}

    Public WallpaperTone_W8 As New Structures.WallpaperTone With {
        .Enabled = False,
        .Image = My.PATH_Windows & "\Web\Wallpaper\Windows\img0.jpg",
        .H = 0, .S = 50, .L = 50}

    Public WallpaperTone_W7 As New Structures.WallpaperTone With {
        .Enabled = False,
        .Image = My.PATH_Windows & "\Web\Wallpaper\Windows\img0.jpg",
        .H = 0, .S = 50, .L = 50}

    Public WallpaperTone_WVista As New Structures.WallpaperTone With {
        .Enabled = False,
        .Image = My.PATH_Windows & "\Web\Wallpaper\Windows\img0.jpg",
        .H = 0, .S = 50, .L = 50}

    Public WallpaperTone_WXP As New Structures.WallpaperTone With {
        .Enabled = False,
        .Image = My.PATH_Windows & "\Web\Wallpaper\Bliss.bmp",
        .H = 0, .S = 50, .L = 50}

    Public WindowsEffects As New Structures.WinEffects With {
        .Enabled = True,
        .WindowAnimation = True,
        .WindowShadow = True,
        .WindowUIEffects = True,
        .MenuAnimation = True,
        .MenuSelectionFade = True,
        .MenuFade = MenuAnimType.Fade,
        .ComboboxAnimation = True,
        .ListBoxSmoothScrolling = True,
        .TooltipAnimation = True,
        .TooltipFade = MenuAnimType.Fade,
        .IconsShadow = True,
        .IconsDesktopTranslSel = True,
        .ShowWinContentDrag = True}

    Public MetricsFonts As New Structures.MetricsFonts With {
                .Enabled = XenonCore.GetWindowsScreenScalingFactor() = 100,
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
                .CaptionFont = New Font("Segoe UI", 9, FontStyle.Regular),
                .IconFont = New Font("Segoe UI", 9, FontStyle.Regular),
                .MenuFont = New Font("Segoe UI", 9, FontStyle.Regular),
                .MessageFont = New Font("Segoe UI", 9, FontStyle.Regular),
                .SmCaptionFont = New Font("Segoe UI", 9, FontStyle.Regular),
                .StatusFont = New Font("Segoe UI", 9, FontStyle.Regular),
                .FontSubstitute_MSShellDlg = "Microsoft Sans Serif", .FontSubstitute_MSShellDlg2 = "Tahoma",
                .FontSubstitute_SegoeUI = ""}

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
                    .LoadingCircleHotNoiseOpacity = 0.25}

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
                    .LoadingCircleHotNoiseOpacity = 0.25}

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
                    .LoadingCircleHotNoiseOpacity = 0.25}

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
                    .LoadingCircleHotNoiseOpacity = 0.25}

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
                    .LoadingCircleHotNoiseOpacity = 0.25}

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
                    .LoadingCircleHotNoiseOpacity = 0.25}

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
                    .LoadingCircleHotNoiseOpacity = 0.25}

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
                    .LoadingCircleHotNoiseOpacity = 0.25}

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
                    .LoadingCircleHotNoiseOpacity = 0.25}

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
                    .LoadingCircleHotNoiseOpacity = 0.25}

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
                    .LoadingCircleHotNoiseOpacity = 0.25}

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
                    .LoadingCircleHotNoiseOpacity = 0.25}

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
                    .LoadingCircleHotNoiseOpacity = 0.25}

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
                    .LoadingCircleHotNoiseOpacity = 0.25}

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
                    .LoadingCircleHotNoiseOpacity = 0.25}

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
                    .LoadingCircleHotNoiseOpacity = 0.25}

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
                    .LoadingCircleHotNoiseOpacity = 0.25}
#End Region

#End Region

#Region "Functions"
#Region "UserPreferenceMask"
    Shared Function GetUserPreferencesMask(Bit As Integer) As Boolean

        Try
            Dim hexstring As Byte() = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Desktop", "UserPreferencesMask", Nothing)
            Dim binarystring As String = String.Join("", hexstring.Reverse().[Select](Function(xb) Convert.ToString(xb, 2).PadLeft(8, "0"c)))
            Return If(binarystring(binarystring.Count - 1 - Bit) = CChar("1"), True, False)
        Catch
            Return False
        End Try

    End Function

    Shared Function SetUserPreferenceMask(Bit As Integer, Value As Boolean) As Byte()
        Dim hexstring As Byte() = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Desktop", "UserPreferencesMask", Nothing)
        Dim binarystring As String = String.Join("", hexstring.Reverse().[Select](Function(xb) Convert.ToString(xb, 2).PadLeft(8, "0"c)))
        Dim EnableThemeIndex As Integer = binarystring.Count - 1 - Bit
        binarystring = binarystring.Remove(EnableThemeIndex, 1).Insert(EnableThemeIndex, Value.ToInteger)
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

    Shared Sub EditReg(KeyName As String, ValueName As String, Value As Object, Optional RegType As RegistryValueKind = RegistryValueKind.DWord)
        Dim R As RegistryKey = Nothing
        If KeyName.StartsWith("Computer\", My._strIgnore) Then KeyName = KeyName.Remove(0, "Computer\".Count)

        Dim LocalMacine As Boolean = False

        If KeyName.ToUpper.Contains("HKEY_CURRENT_USER".ToUpper) Then
            R = Registry.CurrentUser
            KeyName = KeyName.Remove(0, "HKEY_CURRENT_USER\".Count)

            If Registry.CurrentUser.OpenSubKey(KeyName, True) Is Nothing Then Registry.CurrentUser.CreateSubKey(KeyName, True)

        ElseIf KeyName.ToUpper.Contains("HKEY_LOCAL_MACHINE".ToUpper) Then

            LocalMacine = True
            R = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32)
            KeyName = KeyName.Remove(0, "HKEY_LOCAL_MACHINE\".Count)

            If My.isElevated Then
                If Registry.LocalMachine.OpenSubKey(KeyName, True) Is Nothing Then Registry.LocalMachine.CreateSubKey(KeyName, True)
            End If

        End If

        'Skips setting to registry if the values are the same
        Try
            If R.OpenSubKey(KeyName).GetValue(ValueName).Equals(Value) Then
                Try
                    If R IsNot Nothing Then
                        R.Flush()
                        R.Close()
                    End If
                Catch
                End Try

                Exit Sub
            End If
        Catch
        End Try

        If Not LocalMacine Or (LocalMacine And My.isElevated) Then
            R.OpenSubKey(KeyName, True).SetValue(ValueName, Value, RegType)
        Else
            EditReg_AdministratorDeflector("HKEY_LOCAL_MACHINE\" & KeyName, ValueName, Value, RegType)
        End If

        Try
            If R IsNot Nothing Then
                R.Flush()
                R.Close()
            End If
        Catch
        End Try

    End Sub
    Shared Sub EditReg_AdministratorDeflector(ByVal RegistryKeyPath As String, ByVal ValueName As String, ByVal Value As Object, Optional RegType As RegistryValueKind = RegistryValueKind.DWord)
        Dim regTemplate As String

        Dim _Value As String
        If RegistryKeyPath.ToUpper.StartsWith("HKEY_LOCAL_MACHINE") Then RegistryKeyPath = "HKLM" & RegistryKeyPath.Remove(0, "HKEY_LOCAL_MACHINE".Count)

        '/v = Value Name
        '/t = Registry Value Type (https://learn.microsoft.com/en-us/windows/win32/sysinfo/registry-value-types)
        '/d = Value
        '/f = Disable prompt
        If Value IsNot Nothing Then
            Select Case RegType
                Case RegistryValueKind.String
                    regTemplate = "add ""{0}"" /v ""{1}"" /t REG_SZ /d ""{2}"" /f"
                    _Value = Value.ToString

                Case RegistryValueKind.DWord
                    regTemplate = "add ""{0}"" /v ""{1}"" /t REG_DWORD /d {2} /f"
                    _Value = CInt(Value).To8Digits

                Case RegistryValueKind.Binary
                    regTemplate = "add ""{0}"" /v ""{1}"" /t REG_BINARY /d {2} /f"
                    _Value = BitConverter.ToString(Value).Replace("-", "") 'BitConverter.ToString(Value).Replace("-", ",")

                Case Else
                    regTemplate = "add ""{0}"" /v ""{1}"" /d ""{2}"" /f"
                    _Value = Value.ToString

            End Select

        Else
            regTemplate = "add ""{0}"" /v ""{1}"" /d ""{2}"" /f"
            _Value = ""

        End If

        If _Value.Contains("%") Then _Value = _Value.Replace("%", "^%")

        Dim process As New Process With {.StartInfo = New ProcessStartInfo With {
           .FileName = "reg",
           .Verb = "runas",
           .Arguments = String.Format(regTemplate, RegistryKeyPath, ValueName, _Value),
           .WindowStyle = ProcessWindowStyle.Hidden,
           .CreateNoWindow = True,
           .UseShellExecute = True
        }}

        process.Start()
        process.WaitForExit()
    End Sub
    Shared Function AddByteToArray(ByVal bArray As Byte(), ByVal newByte As Byte) As Byte()
        Dim newArray As Byte() = New Byte(bArray.Length + 1 - 1) {}
        bArray.CopyTo(newArray, 1)
        newArray(0) = newByte
        Return newArray
    End Function
    Shared Function StringToBytesArray(ByVal str As String) As Byte()
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

        Dim AllThemes As List(Of String) = [String].CList
        Dim SelectedTheme As String = ""
        Dim SelectedThemeList As New List(Of String)

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

        SelectedThemeList = SelectedTheme.CList


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
    Public Function ListColors() As List(Of Color)

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

        For Each field In GetType(Structures.Windows8).GetFields(BindingFlags.Instance Or BindingFlags.NonPublic Or BindingFlags.Public)
            If field.FieldType.Name.ToLower = "color" Then
                CL.Add(field.GetValue(Windows8))
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
                CL.Add(field.GetValue(WallpaperTone_W8))
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
    Public Shared Sub AddNode([TreeView] As TreeView, [Text] As String, [ImageKey] As String)
        If [TreeView] IsNot Nothing Then
            If [TreeView].InvokeRequired Then

                Try
                    [TreeView].Invoke(CType(Sub()
                                                With [TreeView].Nodes.Add([Text])
                                                    .ImageKey = [ImageKey] : .SelectedImageKey = [ImageKey]
                                                End With
                                                [TreeView].SelectedNode = [TreeView].Nodes([TreeView].Nodes.Count - 1)
                                                [TreeView].Refresh()
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
                                                [TreeView].Refresh()
                                            End Sub, MethodInvoker))
                Catch
                End Try
            End If
        End If
    End Sub
    Private Sub AddException([Label] As String, [Exception] As Exception)
        My.Saving_Exceptions.Add(New Tuple(Of String, Exception)([Label], [Exception]))
    End Sub
    Shared Function AddFontsToThemeFile(PropName As String, Font As Font) As String
        Dim s As New List(Of String) : s.Clear()
        s.Add(String.Format("*Fonts_{0}_{1}= {2}", PropName, "Name", Font.Name))
        s.Add(String.Format("*Fonts_{0}_{1}= {2}", PropName, "Size", Font.SizeInPoints))
        s.Add(String.Format("*Fonts_{0}_{1}= {2}", PropName, "Style", Font.Style))
        Return s.CString
    End Function
    Function SetToFont(PropName As String, PropValue As String, Font As Font) As Font
        Dim F As New Font(Font.Name, Font.Size, Font.Style)

        Select Case PropName.ToLower
            Case "Name", My._strIgnore
                F = New Font(PropValue, Font.Size, Font.Style)

            Case "Size", My._strIgnore
                F = New Font(Font.Name, CSng(PropValue), Font.Style)

            Case "Style", My._strIgnore
                F = New Font(Font.Name, Font.Size, ReturnFontStyle(PropValue))

        End Select

        Return F
    End Function
    Function ReturnFontStyle([Value] As String) As FontStyle

        If Not [Value].Contains(",") Then

            Select Case [Value].ToLower
                Case "Bold", My._strIgnore
                    Return FontStyle.Bold

                Case "Italic", My._strIgnore
                    Return FontStyle.Italic

                Case "Regular", My._strIgnore
                    Return FontStyle.Regular

                Case "Strikeout", My._strIgnore
                    Return FontStyle.Strikeout

                Case "Underline", My._strIgnore
                    Return FontStyle.Underline

                Case Else
                    Return FontStyle.Regular

            End Select

        Else
            Dim Collection As New FontStyle

            For Each x In Value.Split(",")
                Dim val As String = x.Trim

                Select Case val.ToLower
                    Case "Bold", My._strIgnore
                        Collection += FontStyle.Bold

                    Case "Italic", My._strIgnore
                        Collection += FontStyle.Italic

                    Case "Regular", My._strIgnore
                        Collection += FontStyle.Regular

                    Case "Strikeout", My._strIgnore
                        Collection += FontStyle.Strikeout

                    Case "Underline", My._strIgnore
                        Collection += FontStyle.Underline

                    Case Else
                        Collection += FontStyle.Regular

                End Select

            Next

            Return Collection
        End If

    End Function

    Sub Excute(ByVal [Sub] As MethodInvoker, Optional [TreeView] As TreeView = Nothing, Optional StartStr As String = "", Optional ErrorStr As String = "",
               Optional TimeStr As String = "", Optional overallstopwatch As Stopwatch = Nothing, Optional Skip As Boolean = False, Optional SkipStr As String = "")

        Dim ReportProgress As Boolean = [TreeView] IsNot Nothing
        Dim sw As New Stopwatch
        sw.Reset()
        sw.Stop()
        sw.Start()

        If Not Skip Then
            If Not String.IsNullOrWhiteSpace(StartStr) Then AddNode([TreeView], String.Format("{0}: {1}", Now.ToLongTimeString, StartStr), "info")
            Try
                [Sub]()
                If ReportProgress And Not String.IsNullOrWhiteSpace(TimeStr) Then AddNode([TreeView], String.Format(TimeStr, sw.ElapsedMilliseconds / 1000), "time")
            Catch ex As Exception
                sw.Stop() : overallstopwatch.Stop()
                _ErrorHappened = True
                If ReportProgress Then
                    If Not String.IsNullOrWhiteSpace(ErrorStr) Then AddNode([TreeView], String.Format("{0}: {1}", Now.ToLongTimeString, ErrorStr), "error")
                    AddException(ErrorStr, ex)
                Else
                    BugReport.ThrowError(ex)
                End If
                sw.Start() : overallstopwatch.Start()
            End Try
        Else
            If Not String.IsNullOrWhiteSpace(ErrorStr) Then AddNode([TreeView], String.Format("{0}: {1}", Now.ToLongTimeString, SkipStr), "skip")

        End If
        sw.Stop()
    End Sub

#End Region

#Region "CP Handling (Loading/Applying)"

    Dim _ErrorHappened As Boolean = False
    Dim regPath_PS86 As String = "%SystemRoot%_System32_WindowsPowerShell_v1.0_powershell.exe"
    Dim AppPath_PS86 As String = My.PATH_Windows & "\System32\WindowsPowerShell\v1.0"
    Dim regPath_PS64 As String = "%SystemRoot%_SysWOW64_WindowsPowerShell_v1.0_powershell.exe"
    Dim AppPath_PS64 As String = My.PATH_Windows & "\SysWOW64\WindowsPowerShell\v1.0"

    Sub New(CP_Type As CP_Type, Optional File As String = "", Optional IgnoreWTerminal As Boolean = False)

        Select Case CP_Type
            Case CP_Type.Registry
                Dim _Def As CP
                If MainFrm.PreviewConfig = MainFrm.WinVer.W11 Then
                    _Def = New CP_Defaults().Default_Windows11
                ElseIf MainFrm.PreviewConfig = MainFrm.WinVer.W10 Then
                    _Def = New CP_Defaults().Default_Windows10
                ElseIf MainFrm.PreviewConfig = MainFrm.WinVer.W8 Then
                    _Def = New CP_Defaults().Default_Windows8
                ElseIf MainFrm.PreviewConfig = MainFrm.WinVer.W7 Then
                    _Def = New CP_Defaults().Default_Windows7
                ElseIf MainFrm.PreviewConfig = MainFrm.WinVer.WVista Then
                    _Def = New CP_Defaults().Default_WindowsVista
                ElseIf MainFrm.PreviewConfig = MainFrm.WinVer.WXP Then
                    _Def = New CP_Defaults().Default_WindowsXP
                Else
                    _Def = New CP_Defaults().Default_Windows11
                End If

#Region "Registry"
                Info.Load()
                Windows11.Load(New CP_Defaults().Default_Windows11.Windows11, New CP_Defaults().Default_Windows11Accents_Bytes)
                Windows10.Load(New CP_Defaults().Default_Windows10.Windows10, New CP_Defaults().Default_Windows10Accents_Bytes)
                Windows8.Load(_Def.Windows8)
                Windows7.Load(_Def.Windows7)
                WindowsVista.Load(_Def.WindowsVista)
                WindowsXP.Load(_Def.WindowsXP)
                WindowsEffects.Load(_Def.WindowsEffects)
                LogonUI10x.Load(_Def.LogonUI10x)
                LogonUI7.Load(_Def.LogonUI7)
                LogonUIXP.Load(_Def.LogonUIXP)
                Win32.Load()
                MetricsFonts.Load(_Def.MetricsFonts)

                WallpaperTone_W11.Load("Win11")
                WallpaperTone_W10.Load("Win10")
                WallpaperTone_W8.Load("Win8.1")
                WallpaperTone_W7.Load("Win7")
                WallpaperTone_WVista.Load("WinVista")
                WallpaperTone_WXP.Load("WinXP")

                CommandPrompt.Load("", "Terminal_CMD_Enabled", _Def.CommandPrompt)
                If IO.Directory.Exists(AppPath_PS86) Then
                    Try : Registry.CurrentUser.CreateSubKey("Console\" & regPath_PS86, True).Close() : Catch : End Try
                    PowerShellx86.Load(regPath_PS86, "Terminal_PS_32_Enabled", _Def.PowerShellx86)
                Else
                    PowerShellx86 = _Def.PowerShellx86
                End If
                If IO.Directory.Exists(AppPath_PS64) Then
                    Try : Registry.CurrentUser.CreateSubKey("Console\" & regPath_PS64, True).Close() : Catch : End Try
                    PowerShellx64.Load(regPath_PS64, "Terminal_PS_64_Enabled", _Def.PowerShellx64)
                Else
                    PowerShellx64 = _Def.PowerShellx64
                End If


#Region "Windows Terminal"
                Dim rLogX As RegistryKey = Registry.CurrentUser.CreateSubKey("Software\WinPaletter\Terminals")

                Try
                    Terminal.Enabled = CInt(rLogX.GetValue("Terminal_Stable_Enabled", 0)).ToBoolean
                Catch
                    Terminal.Enabled = False
                End Try

                Try
                    TerminalPreview.Enabled = CInt(rLogX.GetValue("Terminal_Preview_Enabled", 0)).ToBoolean
                Catch
                    TerminalPreview.Enabled = False
                End Try

                rLogX.Close()

                If My.W10 Or My.W11 Then
                    Dim TerDir As String
                    Dim TerPreDir As String

                    If Not My.[Settings].Terminal_Path_Deflection Then
                        TerDir = My.PATH_TerminalJSON
                        TerPreDir = My.PATH_TerminalPreviewJSON
                    Else
                        If IO.File.Exists(My.[Settings].Terminal_Stable_Path) Then
                            TerDir = My.[Settings].Terminal_Stable_Path
                        Else
                            TerDir = My.PATH_TerminalJSON
                        End If

                        If IO.File.Exists(My.[Settings].Terminal_Preview_Path) Then
                            TerPreDir = My.[Settings].Terminal_Preview_Path
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
                Dim rMain As RegistryKey = Registry.CurrentUser.CreateSubKey("Software\WinPaletter\Cursors")
                Cursor_Enabled = rMain.GetValue("", False)
                rMain.Close()
                Dim ii As Boolean

                Try
                    If Fixer.SystemParametersInfo(SPI.Cursors.GETCURSORSHADOW, 0, ii, SPIF.None) = 1 Then
                        Cursor_Shadow = ii
                    Else
                        Cursor_Shadow = _Def.Cursor_Shadow
                    End If
                Catch
                    Cursor_Shadow = _Def.Cursor_Shadow
                End Try

                Try
                    Fixer.SystemParametersInfo(SPI.Cursors.GETMOUSETRAILS, 0, Cursor_Trails, SPIF.None)
                Catch
                    Cursor_Trails = _Def.Cursor_Trails
                End Try

                Try
                    If Fixer.SystemParametersInfo(SPI.Cursors.GETMOUSESONAR, 0, ii, SPIF.None) = 1 Then
                        Cursor_Sonar = ii
                    Else
                        Cursor_Sonar = _Def.Cursor_Sonar
                    End If
                Catch
                    Cursor_Sonar = _Def.Cursor_Sonar
                End Try

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

#End Region

                _Def.Dispose()

            Case CP_Type.File
#Region "File"
                Dim txt As New List(Of String)
                txt.Clear()
                txt = IO.File.ReadAllText(File).CList

                Dim CUR_Arrow_List, CUR_AppLoading_List, CUR_Busy_List, CUR_Help_List, CUR_Move_List, CUR_NS_List, CUR_EW_List, CUR_NESW_List, CUR_NWSE_List,
                    CUR_Up_List, CUR_Pen_List, CUR_None_List, CUR_Link_List, CUR_Pin_List, CUR_Person_List, CUR_IBeam_List, CUR_Cross_List As New List(Of String)
                CUR_Arrow_List.Clear() : CUR_AppLoading_List.Clear() : CUR_Busy_List.Clear() : CUR_Help_List.Clear() : CUR_Move_List.Clear() : CUR_NS_List.Clear() : CUR_EW_List.Clear() : CUR_NESW_List.Clear() : CUR_NWSE_List.Clear()
                CUR_Up_List.Clear() : CUR_Pen_List.Clear() : CUR_None_List.Clear() : CUR_Link_List.Clear() : CUR_Pin_List.Clear() : CUR_Person_List.Clear() : CUR_IBeam_List.Clear() : CUR_Cross_List.Clear()

                Dim cmdList, PS86List, PS64List As New List(Of String)
                cmdList.Clear()
                PS86List.Clear()
                PS64List.Clear()

                Dim ls_stable, ls_preview As New List(Of String)
                ls_stable.Clear()
                ls_preview.Clear()

                Dim fonts As New List(Of String)
                fonts.Clear()

                Dim WT_11, WT_10, WT_8, WT_7, WT_Vista, WT_XP As New List(Of String)
                WT_11.Clear()
                WT_10.Clear()
                WT_8.Clear()
                WT_7.Clear()
                WT_Vista.Clear()
                WT_XP.Clear()

                '## Checks if the loaded file is an old WPTH or not
                Dim OldWPTH As Boolean = False
                For Each lin As String In txt
                    If lin.StartsWith("*Created from App Version= ", My._strIgnore) Then
                        Info.AppVersion = lin.Remove(0, "*Created from App Version= ".Count)
                        OldWPTH = (Info.AppVersion < "1.0.6.9")
                        Exit For
                    End If
                Next

                For Each lin As String In txt
#Region "Personal Info"
                    If lin.StartsWith("*Created from App Version= ", My._strIgnore) Then Info.AppVersion = lin.Remove(0, "*Created from App Version= ".Count)
                    If lin.StartsWith("*Palette Name= ", My._strIgnore) Then Info.PaletteName = lin.Remove(0, "*Palette Name= ".Count)
                    If lin.StartsWith("*Palette Description= ", My._strIgnore) Then Info.PaletteDescription = lin.Remove(0, "*Palette Description= ".Count).Replace("<br>", vbCrLf)
                    If lin.StartsWith("*Palette File Version= ", My._strIgnore) Then Info.PaletteVersion = lin.Remove(0, "*Palette File Version= ".Count)
                    If lin.StartsWith("*Author= ", My._strIgnore) Then Info.Author = lin.Remove(0, "*Author= ".Count)
                    If lin.StartsWith("*AuthorSocialMediaLink= ", My._strIgnore) Then Info.AuthorSocialMediaLink = lin.Remove(0, "*AuthorSocialMediaLink= ".Count)
                    If lin.StartsWith("*Palette File Version= ", My._strIgnore) Then Info.PaletteVersion = lin.Remove(0, "*Palette File Version= ".Count)
#End Region

#Region "Windows 11"
                    If lin.StartsWith("*Win_11_Color_Index0= ", My._strIgnore) Then Windows11.Color_Index0 = Color.FromArgb(lin.Remove(0, "*Win_11_Color_Index0= ".Count))
                    If lin.StartsWith("*Win_11_Color_Index1= ", My._strIgnore) Then Windows11.Color_Index1 = Color.FromArgb(lin.Remove(0, "*Win_11_Color_Index1= ".Count))
                    If lin.StartsWith("*Win_11_Color_Index2= ", My._strIgnore) Then Windows11.Color_Index2 = Color.FromArgb(lin.Remove(0, "*Win_11_Color_Index2= ".Count))
                    If lin.StartsWith("*Win_11_Color_Index3= ", My._strIgnore) Then Windows11.Color_Index3 = Color.FromArgb(lin.Remove(0, "*Win_11_Color_Index3= ".Count))
                    If lin.StartsWith("*Win_11_Color_Index4= ", My._strIgnore) Then Windows11.Color_Index4 = Color.FromArgb(lin.Remove(0, "*Win_11_Color_Index4= ".Count))
                    If lin.StartsWith("*Win_11_Color_Index5= ", My._strIgnore) Then Windows11.Color_Index5 = Color.FromArgb(lin.Remove(0, "*Win_11_Color_Index5= ".Count))
                    If lin.StartsWith("*Win_11_Color_Index6= ", My._strIgnore) Then Windows11.Color_Index6 = Color.FromArgb(lin.Remove(0, "*Win_11_Color_Index6= ".Count))
                    If lin.StartsWith("*Win_11_Color_Index7= ", My._strIgnore) Then Windows11.Color_Index7 = Color.FromArgb(lin.Remove(0, "*Win_11_Color_Index7= ".Count))
                    If lin.StartsWith("*Win_11_WinMode_Light= ", My._strIgnore) Then Windows11.WinMode_Light = lin.Remove(0, "*Win_11_WinMode_Light= ".Count)
                    If lin.StartsWith("*Win_11_AppMode_Light= ", My._strIgnore) Then Windows11.AppMode_Light = lin.Remove(0, "*Win_11_AppMode_Light= ".Count)
                    If lin.StartsWith("*Win_11_Transparency= ", My._strIgnore) Then Windows11.Transparency = lin.Remove(0, "*Win_11_Transparency= ".Count)
                    If lin.StartsWith("*Win_11_Titlebar_Active= ", My._strIgnore) Then Windows11.Titlebar_Active = Color.FromArgb(lin.Remove(0, "*Win_11_Titlebar_Active= ".Count))
                    If lin.StartsWith("*Win_11_Titlebar_Inactive= ", My._strIgnore) Then Windows11.Titlebar_Inactive = Color.FromArgb(lin.Remove(0, "*Win_11_Titlebar_Inactive= ".Count))
                    If lin.StartsWith("*Win_11_StartMenu_Accent= ", My._strIgnore) Then Windows11.StartMenu_Accent = Color.FromArgb(lin.Remove(0, "*Win_11_StartMenu_Accent= ".Count))
                    If lin.StartsWith("*Win_11_ApplyAccentonTitlebars= ", My._strIgnore) Then Windows11.ApplyAccentonTitlebars = lin.Remove(0, "*Win_11_ApplyAccentonTitlebars= ".Count)

                    If lin.StartsWith("*Win_11_AccentOnStartTBAC= ", My._strIgnore) Then
                        Select Case lin.Remove(0, "*Win_11_AccentOnStartTBAC= ".Count).ToLower
                            Case "false"
                                Windows11.ApplyAccentonTaskbar = ApplyAccentonTaskbar_Level.None

                            Case "true"
                                Windows11.ApplyAccentonTaskbar = ApplyAccentonTaskbar_Level.Taskbar_Start_AC

                            Case Else
                                Select Case lin.Remove(0, "*Win_11_AccentOnStartTBAC= ".Count)
                                    Case 0
                                        Windows11.ApplyAccentonTaskbar = ApplyAccentonTaskbar_Level.None

                                    Case 1
                                        Windows11.ApplyAccentonTaskbar = ApplyAccentonTaskbar_Level.Taskbar_Start_AC

                                    Case 2
                                        Windows11.ApplyAccentonTaskbar = ApplyAccentonTaskbar_Level.Taskbar

                                    Case Else
                                        Windows11.ApplyAccentonTaskbar = ApplyAccentonTaskbar_Level.None
                                End Select
                        End Select
                    End If
#End Region

#Region "Windows 10"
                    If lin.StartsWith("*Win_10_Color_Index0= ", My._strIgnore) Then Windows10.Color_Index0 = Color.FromArgb(lin.Remove(0, "*Win_10_Color_Index0= ".Count))
                    If lin.StartsWith("*Win_10_Color_Index1= ", My._strIgnore) Then Windows10.Color_Index1 = Color.FromArgb(lin.Remove(0, "*Win_10_Color_Index1= ".Count))
                    If lin.StartsWith("*Win_10_Color_Index2= ", My._strIgnore) Then Windows10.Color_Index2 = Color.FromArgb(lin.Remove(0, "*Win_10_Color_Index2= ".Count))
                    If lin.StartsWith("*Win_10_Color_Index3= ", My._strIgnore) Then Windows10.Color_Index3 = Color.FromArgb(lin.Remove(0, "*Win_10_Color_Index3= ".Count))
                    If lin.StartsWith("*Win_10_Color_Index4= ", My._strIgnore) Then Windows10.Color_Index4 = Color.FromArgb(lin.Remove(0, "*Win_10_Color_Index4= ".Count))
                    If lin.StartsWith("*Win_10_Color_Index5= ", My._strIgnore) Then Windows10.Color_Index5 = Color.FromArgb(lin.Remove(0, "*Win_10_Color_Index5= ".Count))
                    If lin.StartsWith("*Win_10_Color_Index6= ", My._strIgnore) Then Windows10.Color_Index6 = Color.FromArgb(lin.Remove(0, "*Win_10_Color_Index6= ".Count))
                    If lin.StartsWith("*Win_10_Color_Index7= ", My._strIgnore) Then Windows10.Color_Index7 = Color.FromArgb(lin.Remove(0, "*Win_10_Color_Index7= ".Count))
                    If lin.StartsWith("*Win_10_WinMode_Light= ", My._strIgnore) Then Windows10.WinMode_Light = lin.Remove(0, "*Win_10_WinMode_Light= ".Count)
                    If lin.StartsWith("*Win_10_AppMode_Light= ", My._strIgnore) Then Windows10.AppMode_Light = lin.Remove(0, "*Win_10_AppMode_Light= ".Count)
                    If lin.StartsWith("*Win_10_Transparency= ", My._strIgnore) Then Windows10.Transparency = lin.Remove(0, "*Win_10_Transparency= ".Count)
                    If lin.StartsWith("*Win_10_Titlebar_Active= ", My._strIgnore) Then Windows10.Titlebar_Active = Color.FromArgb(lin.Remove(0, "*Win_10_Titlebar_Active= ".Count))
                    If lin.StartsWith("*Win_10_Titlebar_Inactive= ", My._strIgnore) Then Windows10.Titlebar_Inactive = Color.FromArgb(lin.Remove(0, "*Win_10_Titlebar_Inactive= ".Count))
                    If lin.StartsWith("*Win_10_StartMenu_Accent= ", My._strIgnore) Then Windows10.StartMenu_Accent = Color.FromArgb(lin.Remove(0, "*Win_10_StartMenu_Accent= ".Count))
                    If lin.StartsWith("*Win_10_ApplyAccentonTitlebars= ", My._strIgnore) Then Windows10.ApplyAccentonTitlebars = lin.Remove(0, "*Win_10_ApplyAccentonTitlebars= ".Count)

                    If lin.StartsWith("*Win_10_AccentOnStartTBAC= ", My._strIgnore) Then
                        Select Case lin.Remove(0, "*Win_10_AccentOnStartTBAC= ".Count).ToLower
                            Case "false"
                                Windows10.ApplyAccentonTaskbar = ApplyAccentonTaskbar_Level.None

                            Case "true"
                                Windows10.ApplyAccentonTaskbar = ApplyAccentonTaskbar_Level.Taskbar_Start_AC

                            Case Else
                                Select Case lin.Remove(0, "*Win_10_AccentOnStartTBAC= ".Count)
                                    Case 0
                                        Windows10.ApplyAccentonTaskbar = ApplyAccentonTaskbar_Level.None

                                    Case 1
                                        Windows10.ApplyAccentonTaskbar = ApplyAccentonTaskbar_Level.Taskbar_Start_AC

                                    Case 2
                                        Windows10.ApplyAccentonTaskbar = ApplyAccentonTaskbar_Level.Taskbar

                                    Case Else
                                        Windows10.ApplyAccentonTaskbar = ApplyAccentonTaskbar_Level.None
                                End Select
                        End Select
                    End If
#End Region

#Region "Windows 10x - Legacy WinPaletter - Before Vesion 1.0.6.9"

                    If OldWPTH Then
                        Try
                            If lin.StartsWith("*WinMode_Light= ", My._strIgnore) Then
                                Windows11.WinMode_Light = lin.Remove(0, "*WinMode_Light= ".Count)
                                Windows10.WinMode_Light = Windows11.WinMode_Light
                            End If

                            If lin.StartsWith("*AppMode_Light= ", My._strIgnore) Then
                                Windows11.AppMode_Light = lin.Remove(0, "*AppMode_Light= ".Count)
                                Windows10.AppMode_Light = Windows11.AppMode_Light
                            End If


                            If lin.StartsWith("*Transparency= ", My._strIgnore) Then
                                Windows11.Transparency = lin.Remove(0, "*Transparency= ".Count)
                                Windows10.Transparency = Windows11.Transparency
                            End If

                            If lin.StartsWith("*AccentColorOnTitlebarAndBorders= ", My._strIgnore) Then
                                Windows11.ApplyAccentonTitlebars = lin.Remove(0, "*AccentColorOnTitlebarAndBorders= ".Count)
                                Windows10.ApplyAccentonTitlebars = Windows11.ApplyAccentonTitlebars
                            End If

                            If lin.StartsWith("*Titlebar_Active= ", My._strIgnore) Then
                                Windows11.Titlebar_Active = Color.FromArgb(lin.Remove(0, "*Titlebar_Active= ".Count))
                                Windows10.Titlebar_Active = Windows11.Titlebar_Active
                            End If

                            If lin.StartsWith("*Titlebar_Inactive= ", My._strIgnore) Then
                                Windows11.Titlebar_Inactive = Color.FromArgb(lin.Remove(0, "*Titlebar_Inactive= ".Count))
                                Windows10.Titlebar_Inactive = Windows11.Titlebar_Inactive
                            End If

                            If lin.StartsWith("*ActionCenter_AppsLinks= ", My._strIgnore) Then
                                Windows11.Color_Index0 = Color.FromArgb(lin.Remove(0, "*ActionCenter_AppsLinks= ".Count))
                                Windows10.Color_Index0 = Windows11.Color_Index0
                            End If

                            If lin.StartsWith("*Taskbar_Icon_Underline= ", My._strIgnore) Then
                                Windows11.Color_Index1 = Color.FromArgb(lin.Remove(0, "*Taskbar_Icon_Underline= ".Count))
                                Windows10.Color_Index1 = Windows11.Color_Index1
                            End If

                            If lin.StartsWith("*StartButton_Hover= ", My._strIgnore) Then
                                Windows11.Color_Index2 = Color.FromArgb(lin.Remove(0, "*StartButton_Hover= ".Count))
                                Windows10.Color_Index2 = Windows11.Color_Index2
                            End If

                            If lin.StartsWith("*SettingsIconsAndLinks= ", My._strIgnore) Then
                                Windows11.Color_Index3 = Color.FromArgb(lin.Remove(0, "*SettingsIconsAndLinks= ".Count))
                                Windows10.Color_Index3 = Windows11.Color_Index3
                            End If

                            If lin.StartsWith("*StartMenuBackground_ActiveTaskbarButton= ", My._strIgnore) Then
                                Windows11.Color_Index4 = Color.FromArgb(lin.Remove(0, "*StartMenuBackground_ActiveTaskbarButton= ".Count))
                                Windows10.Color_Index4 = Windows11.Color_Index4
                            End If

                            If lin.StartsWith("*StartListFolders_TaskbarFront= ", My._strIgnore) Then
                                Windows11.Color_Index5 = Color.FromArgb(lin.Remove(0, "*StartListFolders_TaskbarFront= ".Count))
                                Windows10.Color_Index5 = Windows11.Color_Index5
                            End If

                            If lin.StartsWith("*Taskbar_Background= ", My._strIgnore) Then
                                Windows11.Color_Index6 = Color.FromArgb(lin.Remove(0, "*Taskbar_Background= ".Count))
                                Windows10.Color_Index6 = Windows11.Color_Index6
                            End If

                            If lin.StartsWith("*StartMenu_Accent= ", My._strIgnore) Then
                                Windows11.StartMenu_Accent = Color.FromArgb(lin.Remove(0, "*StartMenu_Accent= ".Count))
                                Windows10.StartMenu_Accent = Windows11.StartMenu_Accent
                            End If

                            If lin.StartsWith("*Undefined= ", My._strIgnore) Then
                                Windows11.Color_Index7 = Color.FromArgb(lin.Remove(0, "*Undefined= ".Count))
                                Windows10.Color_Index7 = Windows11.Color_Index7
                            End If

                            If lin.StartsWith("*AccentColorOnStartTaskbarAndActionCenter= ", My._strIgnore) Then
                                Select Case lin.Remove(0, "*AccentColorOnStartTaskbarAndActionCenter= ".Count).ToLower
                                    Case "false"
                                        Windows11.ApplyAccentonTaskbar = ApplyAccentonTaskbar_Level.None

                                    Case "true"
                                        Windows11.ApplyAccentonTaskbar = ApplyAccentonTaskbar_Level.Taskbar_Start_AC

                                    Case Else
                                        Select Case lin.Remove(0, "*AccentColorOnStartTaskbarAndActionCenter= ".Count)
                                            Case 0
                                                Windows11.ApplyAccentonTaskbar = ApplyAccentonTaskbar_Level.None

                                            Case 1
                                                Windows11.ApplyAccentonTaskbar = ApplyAccentonTaskbar_Level.Taskbar_Start_AC

                                            Case 2
                                                Windows11.ApplyAccentonTaskbar = ApplyAccentonTaskbar_Level.Taskbar

                                            Case Else
                                                Windows11.ApplyAccentonTaskbar = ApplyAccentonTaskbar_Level.None
                                        End Select
                                End Select

                                Windows10.ApplyAccentonTaskbar = Windows11.ApplyAccentonTaskbar
                            End If
                        Catch
                            MsgBox(My.Lang.WPTH_OldGen_LoadError, MsgBoxStyle.Critical)
                        End Try
                    End If

#End Region

#Region "Metro"
                    If lin.StartsWith("*Metro_ColorizationColor= ", My._strIgnore) Then Windows8.ColorizationColor = Color.FromArgb(lin.Remove(0, "*Metro_ColorizationColor= ".Count))
                    If lin.StartsWith("*Metro_ColorizationColorBalance= ", My._strIgnore) Then Windows8.ColorizationColorBalance = lin.Remove(0, "*Metro_ColorizationColorBalance= ".Count)
                    If lin.StartsWith("*Metro_PersonalColors_Background= ", My._strIgnore) Then Windows8.PersonalColors_Background = Color.FromArgb(lin.Remove(0, "*Metro_PersonalColors_Background= ".Count))
                    If lin.StartsWith("*Metro_PersonalColors_Accent= ", My._strIgnore) Then Windows8.PersonalColors_Accent = Color.FromArgb(lin.Remove(0, "*Metro_PersonalColors_Accent= ".Count))
                    If lin.StartsWith("*Metro_StartColor= ", My._strIgnore) Then Windows8.StartColor = Color.FromArgb(lin.Remove(0, "*Metro_StartColor= ".Count))
                    If lin.StartsWith("*Metro_AccentColor= ", My._strIgnore) Then Windows8.AccentColor = Color.FromArgb(lin.Remove(0, "*Metro_AccentColor= ".Count))
                    If lin.StartsWith("*Metro_Start= ", My._strIgnore) Then Windows8.Start = lin.Remove(0, "*Metro_Start= ".Count)
                    If lin.StartsWith("*Metro_Theme= ", My._strIgnore) Then Windows8.Theme = lin.Remove(0, "*Metro_Theme= ".Count)
                    If lin.StartsWith("*Metro_LogonUI= ", My._strIgnore) Then Windows8.LogonUI = lin.Remove(0, "*Metro_LogonUI= ".Count)
                    If lin.StartsWith("*Metro_NoLockScreen= ", My._strIgnore) Then Windows8.NoLockScreen = lin.Remove(0, "*Metro_NoLockScreen= ".Count)
                    If lin.StartsWith("*Metro_LockScreenType= ", My._strIgnore) Then Windows8.LockScreenType = lin.Remove(0, "*Metro_LockScreenType= ".Count)
                    If lin.StartsWith("*Metro_LockScreenSystemID= ", My._strIgnore) Then Windows8.LockScreenSystemID = lin.Remove(0, "*Metro_LockScreenSystemID= ".Count)
#End Region

#Region "Aero"
                    If lin.StartsWith("*Aero_ColorizationColor= ", My._strIgnore) Then Windows7.ColorizationColor = Color.FromArgb(lin.Remove(0, "*Aero_ColorizationColor= ".Count))
                    If lin.StartsWith("*Aero_ColorizationAfterglow= ", My._strIgnore) Then Windows7.ColorizationAfterglow = Color.FromArgb(lin.Remove(0, "*Aero_ColorizationAfterglow= ".Count))
                    If lin.StartsWith("*Aero_ColorizationColorBalance= ", My._strIgnore) Then Windows7.ColorizationColorBalance = lin.Remove(0, "*Aero_ColorizationColorBalance= ".Count)
                    If lin.StartsWith("*Aero_ColorizationAfterglowBalance= ", My._strIgnore) Then Windows7.ColorizationAfterglowBalance = lin.Remove(0, "*Aero_ColorizationAfterglowBalance= ".Count)
                    If lin.StartsWith("*Aero_ColorizationBlurBalance= ", My._strIgnore) Then Windows7.ColorizationBlurBalance = lin.Remove(0, "*Aero_ColorizationBlurBalance= ".Count)
                    If lin.StartsWith("*Aero_ColorizationGlassReflectionIntensity= ", My._strIgnore) Then Windows7.ColorizationGlassReflectionIntensity = lin.Remove(0, "*Aero_ColorizationGlassReflectionIntensity= ".Count)
                    If lin.StartsWith("*Aero_EnableAeroPeek= ", My._strIgnore) Then Windows7.EnableAeroPeek = lin.Remove(0, "*Aero_EnableAeroPeek= ".Count)
                    If lin.StartsWith("*Aero_AlwaysHibernateThumbnails= ", My._strIgnore) Then Windows7.AlwaysHibernateThumbnails = lin.Remove(0, "*Aero_AlwaysHibernateThumbnails= ".Count)
                    If lin.StartsWith("*Aero_Theme= ", My._strIgnore) Then Windows7.Theme = lin.Remove(0, "*Aero_Theme= ".Count)
#End Region

#Region "Vista"
                    If lin.StartsWith("*Vista_ColorizationColor= ", My._strIgnore) Then WindowsVista.ColorizationColor = Color.FromArgb(lin.Remove(0, "*Vista_ColorizationColor= ".Count))
                    If lin.StartsWith("*Vista_Alpha= ", My._strIgnore) Then WindowsVista.Alpha = lin.Remove(0, "*Vista_Alpha= ".Count)
                    If lin.StartsWith("*Vista_Theme= ", My._strIgnore) Then WindowsVista.Theme = lin.Remove(0, "*Vista_Theme= ".Count)
#End Region

#Region "Windows XP"
                    If lin.StartsWith("*WinXP_Theme= ", My._strIgnore) Then WindowsXP.Theme = lin.Remove(0, "*WinXP_Theme= ".Count)
                    If lin.StartsWith("*WinXP_ThemeFile= ", My._strIgnore) Then WindowsXP.ThemeFile = lin.Remove(0, "*WinXP_ThemeFile= ".Count)
                    If lin.StartsWith("*WinXP_ColorScheme= ", My._strIgnore) Then WindowsXP.ColorScheme = lin.Remove(0, "*WinXP_ColorScheme= ".Count)
#End Region

#Region "LogonUI"
                    If lin.StartsWith("*LogonUI_DisableAcrylicBackgroundOnLogon= ", My._strIgnore) Then LogonUI10x.DisableAcrylicBackgroundOnLogon = lin.Remove(0, "*LogonUI_DisableAcrylicBackgroundOnLogon= ".Count)
                    If lin.StartsWith("*LogonUI_DisableLogonBackgroundImage= ", My._strIgnore) Then LogonUI10x.DisableLogonBackgroundImage = lin.Remove(0, "*LogonUI_DisableLogonBackgroundImage= ".Count)
                    If lin.StartsWith("*LogonUI_NoLockScreen= ", My._strIgnore) Then LogonUI10x.NoLockScreen = lin.Remove(0, "*LogonUI_NoLockScreen= ".Count)
#End Region

#Region "LogonUI_7_8"
                    If lin.StartsWith("*LogonUI7_Color= ", My._strIgnore) Then LogonUI7.Color = Color.FromArgb(lin.Remove(0, "*LogonUI7_Color= ".Count))
                    If lin.StartsWith("*LogonUI7_Enabled= ", My._strIgnore) Then LogonUI7.Enabled = lin.Remove(0, "*LogonUI7_Enabled= ".Count)
                    If lin.StartsWith("*LogonUI7_Mode= ", My._strIgnore) Then LogonUI7.Mode = lin.Remove(0, "*LogonUI7_Mode= ".Count)
                    If lin.StartsWith("*LogonUI7_ImagePath= ", My._strIgnore) Then LogonUI7.ImagePath = lin.Remove(0, "*LogonUI7_ImagePath= ".Count)
                    If lin.StartsWith("*LogonUI7_Blur= ", My._strIgnore) Then LogonUI7.Blur = lin.Remove(0, "*LogonUI7_Blur= ".Count)
                    If lin.StartsWith("*LogonUI7_Blur_Intensity= ", My._strIgnore) Then LogonUI7.Blur_Intensity = lin.Remove(0, "*LogonUI7_Blur_Intensity= ".Count)
                    If lin.StartsWith("*LogonUI7_Grayscale= ", My._strIgnore) Then LogonUI7.Grayscale = lin.Remove(0, "*LogonUI7_Grayscale= ".Count)
                    If lin.StartsWith("*LogonUI7_Noise= ", My._strIgnore) Then LogonUI7.Noise = lin.Remove(0, "*LogonUI7_Noise= ".Count)
                    If lin.StartsWith("*LogonUI7_Noise_Mode= ", My._strIgnore) Then LogonUI7.Noise_Mode = lin.Remove(0, "*LogonUI7_Noise_Mode= ".Count)
                    If lin.StartsWith("*LogonUI7_Noise_Intensity= ", My._strIgnore) Then LogonUI7.Noise_Intensity = lin.Remove(0, "*LogonUI7_Noise_Intensity= ".Count)
#End Region

#Region "LogonUI XP"
                    If lin.StartsWith("*LogonUIXP_Enabled= ", My._strIgnore) Then LogonUIXP.Enabled = lin.Remove(0, "*LogonUIXP_Enabled= ".Count)
                    If lin.StartsWith("*LogonUIXP_Mode= ", My._strIgnore) Then LogonUIXP.Mode = If(lin.Remove(0, "*LogonUIXP_Mode= ".Count) = 1, LogonUIXP_Modes.Default, LogonUIXP_Modes.Win2000)
                    If lin.StartsWith("*LogonUIXP_BackColor= ", My._strIgnore) Then LogonUIXP.BackColor = Color.FromArgb(lin.Remove(0, "*LogonUIXP_BackColor= ".Count))
                    If lin.StartsWith("*LogonUIXP_ShowMoreOptions= ", My._strIgnore) Then LogonUIXP.ShowMoreOptions = lin.Remove(0, "*LogonUIXP_ShowMoreOptions= ".Count)
#End Region

#Region "Win32UI"
                    If lin.StartsWith("*Win32UI_EnableTheming= ", My._strIgnore) Then Win32.EnableTheming = lin.Remove(0, "*Win32UI_EnableTheming= ".Count)
                    If lin.StartsWith("*Win32UI_EnableGradient= ", My._strIgnore) Then Win32.EnableGradient = lin.Remove(0, "*Win32UI_EnableGradient= ".Count)
                    If lin.StartsWith("*Win32UI_ActiveBorder= ", My._strIgnore) Then Win32.ActiveBorder = Color.FromArgb(lin.Remove(0, "*Win32UI_ActiveBorder= ".Count))
                    If lin.StartsWith("*Win32UI_ActiveTitle= ", My._strIgnore) Then Win32.ActiveTitle = Color.FromArgb(lin.Remove(0, "*Win32UI_ActiveTitle= ".Count))
                    If lin.StartsWith("*Win32UI_AppWorkspace= ", My._strIgnore) Then Win32.AppWorkspace = Color.FromArgb(lin.Remove(0, "*Win32UI_AppWorkspace= ".Count))
                    If lin.StartsWith("*Win32UI_Background= ", My._strIgnore) Then Win32.Background = Color.FromArgb(lin.Remove(0, "*Win32UI_Background= ".Count))
                    If lin.StartsWith("*Win32UI_ButtonAlternateFace= ", My._strIgnore) Then Win32.ButtonAlternateFace = Color.FromArgb(lin.Remove(0, "*Win32UI_ButtonAlternateFace= ".Count))
                    If lin.StartsWith("*Win32UI_ButtonDkShadow= ", My._strIgnore) Then Win32.ButtonDkShadow = Color.FromArgb(lin.Remove(0, "*Win32UI_ButtonDkShadow= ".Count))
                    If lin.StartsWith("*Win32UI_ButtonFace= ", My._strIgnore) Then Win32.ButtonFace = Color.FromArgb(lin.Remove(0, "*Win32UI_ButtonFace= ".Count))
                    If lin.StartsWith("*Win32UI_ButtonHilight= ", My._strIgnore) Then Win32.ButtonHilight = Color.FromArgb(lin.Remove(0, "*Win32UI_ButtonHilight= ".Count))
                    If lin.StartsWith("*Win32UI_ButtonLight= ", My._strIgnore) Then Win32.ButtonLight = Color.FromArgb(lin.Remove(0, "*Win32UI_ButtonLight= ".Count))
                    If lin.StartsWith("*Win32UI_ButtonShadow= ", My._strIgnore) Then Win32.ButtonShadow = Color.FromArgb(lin.Remove(0, "*Win32UI_ButtonShadow= ".Count))
                    If lin.StartsWith("*Win32UI_ButtonText= ", My._strIgnore) Then Win32.ButtonText = Color.FromArgb(lin.Remove(0, "*Win32UI_ButtonText= ".Count))
                    If lin.StartsWith("*Win32UI_GradientActiveTitle= ", My._strIgnore) Then Win32.GradientActiveTitle = Color.FromArgb(lin.Remove(0, "*Win32UI_GradientActiveTitle= ".Count))
                    If lin.StartsWith("*Win32UI_GradientInactiveTitle= ", My._strIgnore) Then Win32.GradientInactiveTitle = Color.FromArgb(lin.Remove(0, "*Win32UI_GradientInactiveTitle= ".Count))
                    If lin.StartsWith("*Win32UI_GrayText= ", My._strIgnore) Then Win32.GrayText = Color.FromArgb(lin.Remove(0, "*Win32UI_GrayText= ".Count))
                    If lin.StartsWith("*Win32UI_HilightText= ", My._strIgnore) Then Win32.HilightText = Color.FromArgb(lin.Remove(0, "*Win32UI_HilightText= ".Count))
                    If lin.StartsWith("*Win32UI_HotTrackingColor= ", My._strIgnore) Then Win32.HotTrackingColor = Color.FromArgb(lin.Remove(0, "*Win32UI_HotTrackingColor= ".Count))
                    If lin.StartsWith("*Win32UI_InactiveBorder= ", My._strIgnore) Then Win32.InactiveBorder = Color.FromArgb(lin.Remove(0, "*Win32UI_InactiveBorder= ".Count))
                    If lin.StartsWith("*Win32UI_InactiveTitle= ", My._strIgnore) Then Win32.InactiveTitle = Color.FromArgb(lin.Remove(0, "*Win32UI_InactiveTitle= ".Count))
                    If lin.StartsWith("*Win32UI_InactiveTitleText= ", My._strIgnore) Then Win32.InactiveTitleText = Color.FromArgb(lin.Remove(0, "*Win32UI_InactiveTitleText= ".Count))
                    If lin.StartsWith("*Win32UI_InfoText= ", My._strIgnore) Then Win32.InfoText = Color.FromArgb(lin.Remove(0, "*Win32UI_InfoText= ".Count))
                    If lin.StartsWith("*Win32UI_InfoWindow= ", My._strIgnore) Then Win32.InfoWindow = Color.FromArgb(lin.Remove(0, "*Win32UI_InfoWindow= ".Count))
                    If lin.StartsWith("*Win32UI_Menu= ", My._strIgnore) Then Win32.Menu = Color.FromArgb(lin.Remove(0, "*Win32UI_Menu= ".Count))
                    If lin.StartsWith("*Win32UI_MenuBar= ", My._strIgnore) Then Win32.MenuBar = Color.FromArgb(lin.Remove(0, "*Win32UI_MenuBar= ".Count))
                    If lin.StartsWith("*Win32UI_MenuText= ", My._strIgnore) Then Win32.MenuText = Color.FromArgb(lin.Remove(0, "*Win32UI_MenuText= ".Count))
                    If lin.StartsWith("*Win32UI_Scrollbar= ", My._strIgnore) Then Win32.Scrollbar = Color.FromArgb(lin.Remove(0, "*Win32UI_Scrollbar= ".Count))
                    If lin.StartsWith("*Win32UI_TitleText= ", My._strIgnore) Then Win32.TitleText = Color.FromArgb(lin.Remove(0, "*Win32UI_TitleText= ".Count))
                    If lin.StartsWith("*Win32UI_Window= ", My._strIgnore) Then Win32.Window = Color.FromArgb(lin.Remove(0, "*Win32UI_Window= ".Count))
                    If lin.StartsWith("*Win32UI_WindowFrame= ", My._strIgnore) Then Win32.WindowFrame = Color.FromArgb(lin.Remove(0, "*Win32UI_WindowFrame= ".Count))
                    If lin.StartsWith("*Win32UI_WindowText= ", My._strIgnore) Then Win32.WindowText = Color.FromArgb(lin.Remove(0, "*Win32UI_WindowText= ".Count))
                    If lin.StartsWith("*Win32UI_Hilight= ", My._strIgnore) Then Win32.Hilight = Color.FromArgb(lin.Remove(0, "*Win32UI_Hilight= ".Count))
                    If lin.StartsWith("*Win32UI_MenuHilight= ", My._strIgnore) Then Win32.MenuHilight = Color.FromArgb(lin.Remove(0, "*Win32UI_MenuHilight= ".Count))
                    If lin.StartsWith("*Win32UI_Desktop= ", My._strIgnore) Then Win32.Desktop = Color.FromArgb(lin.Remove(0, "*Win32UI_Desktop= ".Count))
#End Region

#Region "Windows Effects"
                    If lin.StartsWith("*WinEffects_Enabled= ", My._strIgnore) Then WindowsEffects.Enabled = lin.Remove(0, "*WinEffects_Enabled= ".Count)
                    If lin.StartsWith("*WinEffects_WindowAnimation= ", My._strIgnore) Then WindowsEffects.WindowAnimation = lin.Remove(0, "*WinEffects_WindowAnimation= ".Count)
                    If lin.StartsWith("*WinEffects_WindowShadow= ", My._strIgnore) Then WindowsEffects.WindowShadow = lin.Remove(0, "*WinEffects_WindowShadow= ".Count)
                    If lin.StartsWith("*WinEffects_WindowUIEffects= ", My._strIgnore) Then WindowsEffects.WindowUIEffects = lin.Remove(0, "*WinEffects_WindowUIEffects= ".Count)
                    If lin.StartsWith("*WinEffects_MenuAnimation= ", My._strIgnore) Then WindowsEffects.MenuAnimation = lin.Remove(0, "*WinEffects_MenuAnimation= ".Count)
                    If lin.StartsWith("*WinEffects_MenuFade= ", My._strIgnore) Then WindowsEffects.MenuFade = lin.Remove(0, "*WinEffects_MenuFade= ".Count)
                    If lin.StartsWith("*WinEffects_MenuSelectionFade= ", My._strIgnore) Then WindowsEffects.MenuSelectionFade = lin.Remove(0, "*WinEffects_MenuSelectionFade= ".Count)
                    If lin.StartsWith("*WinEffects_ComboBoxAnimation= ", My._strIgnore) Then WindowsEffects.ComboboxAnimation = lin.Remove(0, "*WinEffects_ComboBoxAnimation= ".Count)
                    If lin.StartsWith("*WinEffects_ListboxSmoothScrolling= ", My._strIgnore) Then WindowsEffects.ListBoxSmoothScrolling = lin.Remove(0, "*WinEffects_ListboxSmoothScrolling= ".Count)
                    If lin.StartsWith("*WinEffects_TooltipAnimation= ", My._strIgnore) Then WindowsEffects.TooltipAnimation = lin.Remove(0, "*WinEffects_TooltipAnimation= ".Count)
                    If lin.StartsWith("*WinEffects_TooltipFade= ", My._strIgnore) Then WindowsEffects.TooltipFade = lin.Remove(0, "*WinEffects_TooltipFade= ".Count)
                    If lin.StartsWith("*WinEffects_IconsShadow= ", My._strIgnore) Then WindowsEffects.IconsShadow = lin.Remove(0, "*WinEffects_IconsShadow= ".Count)
                    If lin.StartsWith("*WinEffects_IconsDesktopTranslSel= ", My._strIgnore) Then WindowsEffects.IconsDesktopTranslSel = lin.Remove(0, "*WinEffects_IconsDesktopTranslSel= ".Count)
                    If lin.StartsWith("*WinEffects_ShowWinContentDrag= ", My._strIgnore) Then WindowsEffects.ShowWinContentDrag = lin.Remove(0, "*WinEffects_ShowWinContentDrag= ".Count)
#End Region

#Region "Metrics & Fonts"
                    If lin.StartsWith("*MetricsFonts_Enabled= ", My._strIgnore) Then MetricsFonts.Enabled = lin.Remove(0, "*MetricsFonts_Enabled= ".Count)
                    If lin.StartsWith("*Metrics_BorderWidth= ", My._strIgnore) Then MetricsFonts.BorderWidth = lin.Remove(0, "*Metrics_BorderWidth= ".Count)
                    If lin.StartsWith("*Metrics_CaptionHeight= ", My._strIgnore) Then MetricsFonts.CaptionHeight = lin.Remove(0, "*Metrics_CaptionHeight= ".Count)
                    If lin.StartsWith("*Metrics_CaptionWidth= ", My._strIgnore) Then MetricsFonts.CaptionWidth = lin.Remove(0, "*Metrics_CaptionWidth= ".Count)
                    If lin.StartsWith("*Metrics_IconSpacing= ", My._strIgnore) Then MetricsFonts.IconSpacing = lin.Remove(0, "*Metrics_IconSpacing= ".Count)
                    If lin.StartsWith("*Metrics_IconVerticalSpacing= ", My._strIgnore) Then MetricsFonts.IconVerticalSpacing = lin.Remove(0, "*Metrics_IconVerticalSpacing= ".Count)
                    If lin.StartsWith("*Metrics_MenuHeight= ", My._strIgnore) Then MetricsFonts.MenuHeight = lin.Remove(0, "*Metrics_MenuHeight= ".Count)
                    If lin.StartsWith("*Metrics_MenuWidth= ", My._strIgnore) Then MetricsFonts.MenuWidth = lin.Remove(0, "*Metrics_MenuWidth= ".Count)
                    If lin.StartsWith("*Metrics_PaddedBorderWidth= ", My._strIgnore) Then MetricsFonts.PaddedBorderWidth = lin.Remove(0, "*Metrics_PaddedBorderWidth= ".Count)
                    If lin.StartsWith("*Metrics_ScrollHeight= ", My._strIgnore) Then MetricsFonts.ScrollHeight = lin.Remove(0, "*Metrics_ScrollHeight= ".Count)
                    If lin.StartsWith("*Metrics_ScrollWidth= ", My._strIgnore) Then MetricsFonts.ScrollWidth = lin.Remove(0, "*Metrics_ScrollWidth= ".Count)
                    If lin.StartsWith("*Metrics_SmCaptionHeight= ", My._strIgnore) Then MetricsFonts.SmCaptionHeight = lin.Remove(0, "*Metrics_SmCaptionHeight= ".Count)
                    If lin.StartsWith("*Metrics_SmCaptionWidth= ", My._strIgnore) Then MetricsFonts.SmCaptionWidth = lin.Remove(0, "*Metrics_SmCaptionWidth= ".Count)
                    If lin.StartsWith("*Metrics_DesktopIconSize= ", My._strIgnore) Then MetricsFonts.DesktopIconSize = lin.Remove(0, "*Metrics_DesktopIconSize= ".Count)
                    If lin.StartsWith("*Metrics_ShellIconSize= ", My._strIgnore) Then MetricsFonts.ShellIconSize = lin.Remove(0, "*Metrics_ShellIconSize= ".Count)
                    If lin.StartsWith("*Fonts_", My._strIgnore) Then fonts.Add(lin.Remove(0, "*Fonts_".Count))

                    If lin.StartsWith("*FontSubstitute_MSShellDlg= ", My._strIgnore) Then MetricsFonts.FontSubstitute_MSShellDlg = lin.Remove(0, "*FontSubstitute_MSShellDlg= ".Count)
                    If lin.StartsWith("*FontSubstitute_MSShellDlg2= ", My._strIgnore) Then MetricsFonts.FontSubstitute_MSShellDlg2 = lin.Remove(0, "*FontSubstitute_MSShellDlg2= ".Count)
                    If lin.StartsWith("*FontSubstitute_SegoeUI= ", My._strIgnore) Then MetricsFonts.FontSubstitute_MSShellDlg2 = lin.Remove(0, "*FontSubstitute_SegoeUI= ".Count)
#End Region

#Region "Terminals"

                    If lin.StartsWith("*Terminal_CMD_Enabled= ", My._strIgnore) Then CommandPrompt.Enabled = lin.Remove(0, "*Terminal_CMD_Enabled= ".Count)
                    If lin.StartsWith("*Terminal_PS_32_Enabled= ", My._strIgnore) Then PowerShellx86.Enabled = lin.Remove(0, "*Terminal_PS_32_Enabled= ".Count)
                    If lin.StartsWith("*Terminal_PS_64_Enabled= ", My._strIgnore) Then PowerShellx64.Enabled = lin.Remove(0, "*Terminal_PS_64_Enabled= ".Count)
                    If lin.StartsWith("*Terminal_Stable_Enabled= ", My._strIgnore) Then Terminal.Enabled = lin.Remove(0, "*Terminal_Stable_Enabled= ".Count)
                    If lin.StartsWith("*Terminal_Preview_Enabled= ", My._strIgnore) Then TerminalPreview.Enabled = lin.Remove(0, "*Terminal_Preview_Enabled= ".Count)

                    If lin.StartsWith("*CMD_", My._strIgnore) Then cmdList.Add(lin.Remove(0, "*CMD_".Count))
                    If lin.StartsWith("*PS_32_", My._strIgnore) Then PS86List.Add(lin.Remove(0, "*PS_32_".Count))
                    If lin.StartsWith("*PS_64_", My._strIgnore) Then PS64List.Add(lin.Remove(0, "*PS_64_".Count))

                    If Not IgnoreWTerminal Then
                        If My.W10 Or My.W11 Then
                            If lin.StartsWith("terminal.", My._strIgnore) Then ls_stable.Add(lin)
                            If lin.StartsWith("terminalpreview.", My._strIgnore) Then ls_preview.Add(lin)
                        End If
                    End If
#End Region

#Region "Wallpaper Tone"
                    If lin.StartsWith("*WallpaperTone_Win11_", My._strIgnore) Then WT_11.Add(lin.Remove(0, "*WallpaperTone_Win11_".Count))
                    If lin.StartsWith("*WallpaperTone_Win10_", My._strIgnore) Then WT_10.Add(lin.Remove(0, "*WallpaperTone_Win10_".Count))
                    If lin.StartsWith("*WallpaperTone_Win8.1_", My._strIgnore) Then WT_8.Add(lin.Remove(0, "*WallpaperTone_Win8.1_".Count))
                    If lin.StartsWith("*WallpaperTone_Win7_", My._strIgnore) Then WT_7.Add(lin.Remove(0, "*WallpaperTone_Win7_".Count))
                    If lin.StartsWith("*WallpaperTone_WinVista_", My._strIgnore) Then WT_Vista.Add(lin.Remove(0, "*WallpaperTone_WinVista_".Count))
                    If lin.StartsWith("*WallpaperTone_WinXP_", My._strIgnore) Then WT_XP.Add(lin.Remove(0, "*WallpaperTone_WinXP_".Count))
#End Region

#Region "Cursors"
                    If lin.StartsWith("*Cursor_Enabled= ", My._strIgnore) Then Cursor_Enabled = lin.Remove(0, "*Cursor_Enabled= ".Count)
                    If lin.StartsWith("*Cursor_Shadow= ", My._strIgnore) Then Cursor_Shadow = lin.Remove(0, "*Cursor_Shadow= ".Count)
                    If lin.StartsWith("*Cursor_Trails= ", My._strIgnore) Then Cursor_Trails = lin.Remove(0, "*Cursor_Trails= ".Count)
                    If lin.StartsWith("*Cursor_Sonar= ", My._strIgnore) Then Cursor_Sonar = lin.Remove(0, "*Cursor_Sonar= ".Count)

                    If lin.StartsWith("*Cursor_Arrow_", My._strIgnore) Then CUR_Arrow_List.Add(lin.Remove(0, "*Cursor_Arrow_".Count))
                    If lin.StartsWith("*Cursor_Help_", My._strIgnore) Then CUR_Help_List.Add(lin.Remove(0, "*Cursor_Help_".Count))
                    If lin.StartsWith("*Cursor_AppLoading_", My._strIgnore) Then CUR_AppLoading_List.Add(lin.Remove(0, "*Cursor_AppLoading_".Count))
                    If lin.StartsWith("*Cursor_Busy_", My._strIgnore) Then CUR_Busy_List.Add(lin.Remove(0, "*Cursor_Busy_".Count))
                    If lin.StartsWith("*Cursor_Move_", My._strIgnore) Then CUR_Move_List.Add(lin.Remove(0, "*Cursor_Move_".Count))
                    If lin.StartsWith("*Cursor_NS_", My._strIgnore) Then CUR_NS_List.Add(lin.Remove(0, "*Cursor_NS_".Count))
                    If lin.StartsWith("*Cursor_EW_", My._strIgnore) Then CUR_EW_List.Add(lin.Remove(0, "*Cursor_EW_".Count))
                    If lin.StartsWith("*Cursor_NESW_", My._strIgnore) Then CUR_NESW_List.Add(lin.Remove(0, "*Cursor_NESW_".Count))
                    If lin.StartsWith("*Cursor_NWSE_", My._strIgnore) Then CUR_NWSE_List.Add(lin.Remove(0, "*Cursor_NWSE_".Count))
                    If lin.StartsWith("*Cursor_Up_", My._strIgnore) Then CUR_Up_List.Add(lin.Remove(0, "*Cursor_Up_".Count))
                    If lin.StartsWith("*Cursor_Pen_", My._strIgnore) Then CUR_Pen_List.Add(lin.Remove(0, "*Cursor_Pen_".Count))
                    If lin.StartsWith("*Cursor_None_", My._strIgnore) Then CUR_None_List.Add(lin.Remove(0, "*Cursor_None_".Count))
                    If lin.StartsWith("*Cursor_Link_", My._strIgnore) Then CUR_Link_List.Add(lin.Remove(0, "*Cursor_Link_".Count))
                    If lin.StartsWith("*Cursor_Pin_", My._strIgnore) Then CUR_Pin_List.Add(lin.Remove(0, "*Cursor_Pin_".Count))
                    If lin.StartsWith("*Cursor_Person_", My._strIgnore) Then CUR_Person_List.Add(lin.Remove(0, "*Cursor_Person_".Count))
                    If lin.StartsWith("*Cursor_IBeam_", My._strIgnore) Then CUR_IBeam_List.Add(lin.Remove(0, "*Cursor_IBeam_".Count))
                    If lin.StartsWith("*Cursor_Cross_", My._strIgnore) Then CUR_Cross_List.Add(lin.Remove(0, "*Cursor_Cross_".Count))

#End Region

                Next

#Region "Fonts"
                If fonts.Count > 0 Then
                    For Each x In fonts
                        Dim Value As String = x.Replace(x.Split("=")(0) & "= ", "").Trim
                        Dim FontName As String = x.Split("=")(0).ToString.Split("_")(0)
                        Dim Prop As String = x.Split("=")(0).ToString.Split("_")(1)

                        Select Case FontName.ToLower
                            Case "Caption", My._strIgnore
                                MetricsFonts.CaptionFont = SetToFont(Prop, Value, MetricsFonts.CaptionFont)

                            Case "Icon", My._strIgnore
                                MetricsFonts.IconFont = SetToFont(Prop, Value, MetricsFonts.IconFont)

                            Case "Menu", My._strIgnore
                                MetricsFonts.MenuFont = SetToFont(Prop, Value, MetricsFonts.MenuFont)

                            Case "Message", My._strIgnore
                                MetricsFonts.MessageFont = SetToFont(Prop, Value, MetricsFonts.MessageFont)

                            Case "SmCaption", My._strIgnore
                                MetricsFonts.SmCaptionFont = SetToFont(Prop, Value, MetricsFonts.SmCaptionFont)

                            Case "Status", My._strIgnore
                                MetricsFonts.StatusFont = SetToFont(Prop, Value, MetricsFonts.StatusFont)

                        End Select
                    Next
                End If

#End Region

#Region "Wallpaper Tone"
                WallpaperTone_W11 = Structures.WallpaperTone.Load_WallpaperTone_From_ListOfString(WT_11)
                WallpaperTone_W10 = Structures.WallpaperTone.Load_WallpaperTone_From_ListOfString(WT_10)
                WallpaperTone_W8 = Structures.WallpaperTone.Load_WallpaperTone_From_ListOfString(WT_8)
                WallpaperTone_W7 = Structures.WallpaperTone.Load_WallpaperTone_From_ListOfString(WT_7)
                WallpaperTone_WVista = Structures.WallpaperTone.Load_WallpaperTone_From_ListOfString(WT_Vista)
                WallpaperTone_WXP = Structures.WallpaperTone.Load_WallpaperTone_From_ListOfString(WT_XP)

#End Region

#Region "Cursors"
                Cursor_Arrow = Structures.Cursor.Load_Cursor_From_ListOfString(CUR_Arrow_List)
                Cursor_Help = Structures.Cursor.Load_Cursor_From_ListOfString(CUR_Help_List)
                Cursor_AppLoading = Structures.Cursor.Load_Cursor_From_ListOfString(CUR_AppLoading_List)
                Cursor_Busy = Structures.Cursor.Load_Cursor_From_ListOfString(CUR_Busy_List)
                Cursor_Move = Structures.Cursor.Load_Cursor_From_ListOfString(CUR_Move_List)
                Cursor_NS = Structures.Cursor.Load_Cursor_From_ListOfString(CUR_NS_List)
                Cursor_EW = Structures.Cursor.Load_Cursor_From_ListOfString(CUR_EW_List)
                Cursor_NESW = Structures.Cursor.Load_Cursor_From_ListOfString(CUR_NESW_List)
                Cursor_NWSE = Structures.Cursor.Load_Cursor_From_ListOfString(CUR_NWSE_List)
                Cursor_Up = Structures.Cursor.Load_Cursor_From_ListOfString(CUR_Up_List)
                Cursor_Pen = Structures.Cursor.Load_Cursor_From_ListOfString(CUR_Pen_List)
                Cursor_None = Structures.Cursor.Load_Cursor_From_ListOfString(CUR_None_List)
                Cursor_Link = Structures.Cursor.Load_Cursor_From_ListOfString(CUR_Link_List)
                Cursor_Pin = Structures.Cursor.Load_Cursor_From_ListOfString(CUR_Pin_List)
                Cursor_Person = Structures.Cursor.Load_Cursor_From_ListOfString(CUR_Person_List)
                Cursor_IBeam = Structures.Cursor.Load_Cursor_From_ListOfString(CUR_IBeam_List)
                Cursor_Cross = Structures.Cursor.Load_Cursor_From_ListOfString(CUR_Cross_List)
#End Region


#Region "Windows Terminal"
                CommandPrompt = Structures.Console.Load_Console_From_ListOfString(cmdList)
                PowerShellx86 = Structures.Console.Load_Console_From_ListOfString(PS86List)
                PowerShellx64 = Structures.Console.Load_Console_From_ListOfString(PS64List)

                If Not IgnoreWTerminal Then
                    Dim str_stable, str_preview As String
                    str_stable = ls_stable.CString
                    str_preview = ls_preview.CString

                    Terminal = New WinTerminal(str_stable, WinTerminal.Mode.WinPaletterFile)
                    TerminalPreview = New WinTerminal(str_preview, WinTerminal.Mode.WinPaletterFile, WinTerminal.Version.Preview)
                End If
#End Region


#End Region

            Case Else

        End Select
    End Sub

    Sub Save(ByVal [SaveTo] As CP_Type, Optional ByVal FileLocation As String = "", Optional ByVal [TreeView] As TreeView = Nothing)

        Select Case [SaveTo]
            Case CP_Type.Registry

#Region "Registry"
                Dim ReportProgress As Boolean = ([TreeView] IsNot Nothing)
                _ErrorHappened = False

                Dim sw_all As New Stopwatch
                sw_all.Reset()
                sw_all.Start()

                If ReportProgress Then
                    My.Saving_Exceptions.Clear()
                    [TreeView].Nodes.Clear()

                    Dim OS As String
                    If My.W11 Then
                        OS = My.Lang.OS_Win11
                    ElseIf My.W10 Then
                        OS = My.Lang.OS_Win10
                    ElseIf My.W8 Then
                        OS = My.Lang.OS_Win8
                    ElseIf My.W7 Then
                        OS = My.Lang.OS_Win7
                    ElseIf My.WVista Then
                        OS = My.Lang.OS_WinVista
                    ElseIf My.WXP Then
                        OS = My.Lang.OS_WinXP
                    Else
                        OS = My.Lang.OS_WinUndefined
                    End If

                    AddNode([TreeView], String.Format("{0}: {1}", Now.ToLongTimeString, String.Format(My.Lang.CP_ApplyFrom, OS)), "info")

                    AddNode([TreeView], String.Format("{0}: {1}", Now.ToLongTimeString, My.Lang.CP_Applying_Started), "info")

                    If Not My.isElevated Then
                        AddNode([TreeView], String.Format("{0}: {1}", Now.ToLongTimeString, My.Lang.CP_Admin_Msg0), "admin")
                        AddNode([TreeView], String.Format("{0}: {1}", Now.ToLongTimeString, My.Lang.CP_Admin_Msg1), "admin")
                    End If

                End If

                If My.W11 Then
                    Excute(CType(Sub()
                                     Windows11.Apply()
                                 End Sub, MethodInvoker), [TreeView], My.Lang.CP_Applying_Win11, My.Lang.CP_W11_Error, My.Lang.CP_Time, sw_all)

                    Excute(CType(Sub()
                                     LogonUI10x.Apply()
                                 End Sub, MethodInvoker), [TreeView], My.Lang.CP_Applying_LogonUI11, My.Lang.CP_LogonUI11_Error, My.Lang.CP_Time, sw_all)
                End If

                If My.W10 Then
                    Excute(CType(Sub()
                                     Windows10.Apply()
                                 End Sub, MethodInvoker), [TreeView], My.Lang.CP_Applying_Win10, My.Lang.CP_W10_Error, My.Lang.CP_Time, sw_all)

                    Excute(CType(Sub()
                                     LogonUI10x.Apply()
                                 End Sub, MethodInvoker), [TreeView], My.Lang.CP_Applying_LogonUI10, My.Lang.CP_LogonUI10_Error, My.Lang.CP_Time, sw_all)
                End If

                If My.W8 Then
                    Excute(CType(Sub()
                                     Windows8.Apply()
                                     RefreshDWM(Me)
                                 End Sub, MethodInvoker), [TreeView], My.Lang.CP_Applying_Win8, My.Lang.CP_W8_Error, My.Lang.CP_Time, sw_all)

                    Excute(CType(Sub()
                                     Apply_LogonUI_8([TreeView])
                                 End Sub, MethodInvoker), [TreeView], My.Lang.CP_Applying_LogonUI8, My.Lang.CP_LogonUI8_Error, My.Lang.CP_Time, sw_all)
                End If

                If My.W7 Then
                    Excute(CType(Sub()
                                     Windows7.Apply()
                                     RefreshDWM(Me)
                                 End Sub, MethodInvoker), [TreeView], My.Lang.CP_Applying_Win7, My.Lang.CP_W7_Error, My.Lang.CP_Time, sw_all)

                    Excute(CType(Sub()
                                     Apply_LogonUI7(LogonUI7, "LogonUI", [TreeView])
                                 End Sub, MethodInvoker), [TreeView], My.Lang.CP_Applying_LogonUI7, My.Lang.CP_LogonUI7_Error, My.Lang.CP_Time, sw_all)
                End If

                If My.WVista Then
                    Excute(CType(Sub()
                                     WindowsVista.Apply()
                                     RefreshDWM(Me)
                                 End Sub, MethodInvoker), [TreeView], My.Lang.CP_Applying_WinVista, My.Lang.CP_WVista_Error, My.Lang.CP_Time, sw_all)
                End If

                If My.WXP Then
                    Excute(CType(Sub()
                                     WindowsXP.Apply()
                                 End Sub, MethodInvoker), [TreeView], My.Lang.CP_Applying_WinXP, My.Lang.CP_WXP_Error, My.Lang.CP_Time, sw_all)

                    Excute(CType(Sub()
                                     LogonUIXP.Apply()
                                 End Sub, MethodInvoker), [TreeView], My.Lang.CP_Applying_LogonUIXP, My.Lang.CP_LogonUIXP_Error, My.Lang.CP_Time, sw_all)
                End If

                'Win32UI
                Excute(CType(Sub()
                                 Win32.Apply()
                             End Sub, MethodInvoker), [TreeView], My.Lang.CP_Applying_Win32UI, My.Lang.CP_WIN32UI_Error, My.Lang.CP_Time, sw_all)

                'WindowsEffects
                Excute(CType(Sub()
                                 WindowsEffects.Apply()
                             End Sub, MethodInvoker), [TreeView], My.Lang.CP_Applying_WinEffects, My.Lang.CP_WinEffects_Error, My.Lang.CP_Time, sw_all)

                'Metrics\Fonts
                Excute(CType(Sub()
                                 MetricsFonts.Apply()
                             End Sub, MethodInvoker), [TreeView], My.Lang.CP_Applying_Metrics, My.Lang.CP_Error_Metrics, My.Lang.CP_Time_They, sw_all, Not MetricsFonts.Enabled, My.Lang.CP_Skip_Metrics)

                'WallpaperTone
                Excute(CType(Sub()
                                 Structures.WallpaperTone.Save_To_Registry(WallpaperTone_W11, "Win11")
                                 Structures.WallpaperTone.Save_To_Registry(WallpaperTone_W10, "Win10")
                                 Structures.WallpaperTone.Save_To_Registry(WallpaperTone_W8, "Win8.1")
                                 Structures.WallpaperTone.Save_To_Registry(WallpaperTone_W7, "Win7")
                                 Structures.WallpaperTone.Save_To_Registry(WallpaperTone_WVista, "WinVista")
                                 Structures.WallpaperTone.Save_To_Registry(WallpaperTone_WXP, "WinXP")
                                 If My.W11 And WallpaperTone_W11.Enabled Then WallpaperTone_W11.Apply()
                                 If My.W10 And WallpaperTone_W10.Enabled Then WallpaperTone_W10.Apply()
                                 If My.W8 And WallpaperTone_W8.Enabled Then WallpaperTone_W8.Apply()
                                 If My.W7 And WallpaperTone_W7.Enabled Then WallpaperTone_W7.Apply()
                                 If My.WVista And WallpaperTone_WVista.Enabled Then WallpaperTone_WVista.Apply()
                                 If My.WXP And WallpaperTone_WXP.Enabled Then WallpaperTone_WXP.Apply()
                             End Sub, MethodInvoker), [TreeView], My.Lang.CP_Applying_WallpaperTone, My.Lang.CP_WallpaperTone_Error, My.Lang.CP_Time, sw_all)

#Region "Consoles"
                Dim rLogX As RegistryKey = Registry.CurrentUser.CreateSubKey("Software\WinPaletter\Terminals")
                rLogX.SetValue("Terminal_CMD_Enabled", CommandPrompt.Enabled.ToInteger)
                rLogX.SetValue("Terminal_PS_32_Enabled", PowerShellx86.Enabled.ToInteger)
                rLogX.SetValue("Terminal_PS_64_Enabled", PowerShellx64.Enabled.ToInteger)
                rLogX.SetValue("Terminal_Stable_Enabled", Terminal.Enabled.ToInteger)
                rLogX.SetValue("Terminal_Preview_Enabled", TerminalPreview.Enabled.ToInteger)

                Excute(CType(Sub()
                                 Apply_CommandPrompt()
                             End Sub, MethodInvoker), [TreeView], My.Lang.CP_Applying_CMD, My.Lang.CP_CMD_Error, My.Lang.CP_Time, sw_all, Not CommandPrompt.Enabled, My.Lang.CP_Skip_CMD)

                Excute(CType(Sub()
                                 Apply_PowerShell86()
                             End Sub, MethodInvoker), [TreeView], My.Lang.CP_Applying_PS32, My.Lang.CP_PS32_Error, My.Lang.CP_Time, sw_all, Not PowerShellx86.Enabled, My.Lang.CP_Skip_PS32)

                Excute(CType(Sub()
                                 Apply_PowerShell64()
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

                    If Not My.[Settings].Terminal_Path_Deflection Then
                        TerDir = My.PATH_TerminalJSON
                        TerPreDir = My.PATH_TerminalPreviewJSON
                    Else
                        If IO.File.Exists(My.[Settings].Terminal_Stable_Path) Then
                            TerDir = My.[Settings].Terminal_Stable_Path
                        Else
                            TerDir = My.PATH_TerminalJSON
                        End If

                        If IO.File.Exists(My.[Settings].Terminal_Preview_Path) Then
                            TerPreDir = My.[Settings].Terminal_Preview_Path
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

                            If Not My.[Settings].Terminal_Path_Deflection Then
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
                            If Not My.[Settings].Terminal_Path_Deflection Then
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

                'Cursors
                Excute(CType(Sub()
                                 Apply_Cursors([TreeView])
                             End Sub, MethodInvoker), [TreeView], "", My.Lang.CP_Error_Cursors, My.Lang.CP_Time_Cursors, sw_all)

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
                If IO.File.Exists(FileLocation) Then
                    Try : Kill(FileLocation) : Catch : End Try
                End If

                IO.File.WriteAllText(FileLocation, ToString)
        End Select
    End Sub

    Overrides Function ToString() As String
        Dim tx As New List(Of String)
        tx.Clear()
        tx.Add("<WinPaletter - Programmed by Abdelrhman-AK>")
        tx.Add("*Created from App Version= " & Info.AppVersion)
        tx.Add("*Last Modified by App Version= " & My.Application.Info.Version.ToString & vbCrLf)

        tx.Add(Info.ToString)

#Region "Windows 10x - Legacy WinPaletter - Before Vesion 1.0.6.9"
        If Info.AppVersion < "1.0.6.9" Or My.[Settings].SaveForLegacyWP Then
            Try
                With If(MainFrm.PreviewConfig = MainFrm.WinVer.W11, Windows11, Windows10)
                    tx.Add("<LegacyWinPaletter_Windows11/10>")
                    tx.Add("*WinMode_Light= " & .WinMode_Light)
                    tx.Add("*AppMode_Light= " & .AppMode_Light)
                    tx.Add("*Transparency= " & .Transparency)
                    tx.Add("*AccentColorOnTitlebarAndBorders= " & .ApplyAccentonTitlebars)
                    tx.Add("*AccentColorOnStartTaskbarAndActionCenter= " & .ApplyAccentonTaskbar)
                    tx.Add("*Titlebar_Active= " & .Titlebar_Active.ToArgb)
                    tx.Add("*Titlebar_Inactive= " & .Titlebar_Inactive.ToArgb)
                    tx.Add("*ActionCenter_AppsLinks= " & .Color_Index0.ToArgb)
                    tx.Add("*Taskbar_Icon_Underline= " & .Color_Index1.ToArgb)
                    tx.Add("*StartButton_Hover= " & .Color_Index2.ToArgb)
                    tx.Add("*SettingsIconsAndLinks= " & .Color_Index3.ToArgb)
                    tx.Add("StartMenuBackground_ActiveTaskbarButton= " & .Color_Index4.ToArgb)
                    tx.Add("*StartListFolders_TaskbarFront= " & .Color_Index5.ToArgb)
                    tx.Add("*Taskbar_Background= " & .Color_Index6.ToArgb)
                    tx.Add("*StartMenu_Accent= " & .StartMenu_Accent.ToArgb)
                    tx.Add("*Undefined= " & .Color_Index7.ToArgb)
                    tx.Add("</LegacyWinPaletter_Windows11/10>" & vbCrLf)
                End With
            Catch
                MsgBox(My.Lang.WPTH_OldGen_SaveError, MsgBoxStyle.Critical)
            End Try
        End If
#End Region

        tx.Add(Windows11.ToString("Windows11", "Win_11"))
        tx.Add(Windows10.ToString("Windows10", "Win_10"))
        tx.Add(LogonUI10x.ToString)

        tx.Add(Windows8.ToString)
        tx.Add(Windows7.ToString)
        tx.Add(LogonUI7.ToString)

        tx.Add(WindowsVista.ToString)
        tx.Add(WindowsXP.ToString)
        tx.Add(LogonUIXP.ToString)

        tx.Add(Win32.ToString)
        tx.Add(WindowsEffects.ToString)
        tx.Add(MetricsFonts.ToString)

        tx.Add(WallpaperTone_W11.ToString("Win11"))
        tx.Add(WallpaperTone_W10.ToString("Win10"))
        tx.Add(WallpaperTone_W8.ToString("Win8.1"))
        tx.Add(WallpaperTone_W7.ToString("Win7"))
        tx.Add(WallpaperTone_WVista.ToString("WinVista"))
        tx.Add(WallpaperTone_WXP.ToString("WinXP"))

        tx.Add("<Terminals>")
        tx.Add(CommandPrompt.ToString("CMD"))
        tx.Add(PowerShellx86.ToString("PS_32"))
        tx.Add(PowerShellx64.ToString("PS_64"))
        Try : If Terminal IsNot Nothing Then tx.Add(Terminal.ToString("WindowsTerminal_Stable", WinTerminal.Version.Stable))
        Catch : End Try
        Try : If TerminalPreview IsNot Nothing Then tx.Add(TerminalPreview.ToString("WindowsTerminal_Preview", WinTerminal.Version.Preview))
        Catch : End Try
        tx.Add("</Terminals>" & vbCrLf)

        tx.Add("<Cursors>")
        tx.Add("*Cursor_Enabled= " & Cursor_Enabled)
        tx.Add("*Cursor_Shadow= " & Cursor_Shadow)
        tx.Add("*Cursor_Sonar= " & Cursor_Sonar)
        tx.Add("*Cursor_Trails= " & Cursor_Trails)
        tx.Add(Cursor_Arrow.ToString("Arrow"))
        tx.Add(Cursor_Help.ToString("Help"))
        tx.Add(Cursor_AppLoading.ToString("AppLoading"))
        tx.Add(Cursor_Busy.ToString("Busy"))
        tx.Add(Cursor_Move.ToString("Move"))
        tx.Add(Cursor_NS.ToString("NS"))
        tx.Add(Cursor_EW.ToString("EW"))
        tx.Add(Cursor_NESW.ToString("NESW"))
        tx.Add(Cursor_NWSE.ToString("NWSE"))
        tx.Add(Cursor_Up.ToString("Up"))
        tx.Add(Cursor_Pen.ToString("Pen"))
        tx.Add(Cursor_None.ToString("None"))
        tx.Add(Cursor_Link.ToString("Link"))
        tx.Add(Cursor_Pin.ToString("Pin"))
        tx.Add(Cursor_Person.ToString("Person"))
        tx.Add(Cursor_IBeam.ToString("IBeam"))
        tx.Add(Cursor_Cross.ToString("Cross"))
        tx.Add("</Cursors>" & vbCrLf)

        tx.Add("</WinPaletter>")

        Return tx.CString
    End Function
#End Region

#Region "Applying Subs"
    Public Sub Apply_LogonUI7([LogonElement] As Structures.LogonUI7, Optional RegEntryHint As String = "LogonUI", Optional ByVal [TreeView] As TreeView = Nothing)
        Dim ReportProgress As Boolean = ([TreeView] IsNot Nothing)

        EditReg("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Authentication\LogonUI\Background", "OEMBackground", [LogonElement].Enabled.ToInteger)
        EditReg("HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Windows\System", "UseOEMBackground", [LogonElement].Enabled.ToInteger)

        Dim rLog As RegistryKey = Registry.CurrentUser.CreateSubKey("Software\WinPaletter\" & RegEntryHint)

        Select Case [LogonElement].Mode
            Case LogonUI_Modes.Default_
                rLog.SetValue("Mode", 0)

            Case LogonUI_Modes.Wallpaper
                rLog.SetValue("Mode", 1)

            Case LogonUI_Modes.CustomImage
                rLog.SetValue("Mode", 2)

            Case LogonUI_Modes.SolidColor
                rLog.SetValue("Mode", 3)
        End Select

        rLog.SetValue("ImagePath", [LogonElement].ImagePath)
        rLog.SetValue("Color", [LogonElement].Color.ToArgb)
        rLog.SetValue("Blur", [LogonElement].Blur.ToInteger)
        rLog.SetValue("Blur_Intensity", [LogonElement].Blur_Intensity)
        rLog.SetValue("Grayscale", [LogonElement].Grayscale.ToInteger)
        rLog.SetValue("Noise", [LogonElement].Noise.ToInteger)

        Select Case [LogonElement].Noise_Mode
            Case NoiseMode.Aero
                rLog.SetValue("Noise_Mode", 0)

            Case NoiseMode.Acrylic
                rLog.SetValue("Noise_Mode", 1)
        End Select

        rLog.SetValue("Noise_Intensity", [LogonElement].Noise_Intensity)
        rLog.Flush()
        rLog.Close()

        If [LogonElement].Enabled Then
            Kernel32.Wow64DisableWow64FsRedirection(IntPtr.Zero)

            Dim DirX As String = My.PATH_System32 & "\oobe\info\backgrounds"

            IO.Directory.CreateDirectory(DirX)

            For Each fileX As String In My.Computer.FileSystem.GetFiles(DirX)
                Try : Kill(fileX) : Catch : End Try
            Next

            Dim bmpList As New List(Of Bitmap)
            bmpList.Clear()

            Select Case [LogonElement].Mode
                Case LogonUI_Modes.Default_
                    For i As Integer = 5031 To 5043 Step +1
                        bmpList.Add(DLLFunc.GetDllRes(My.PATH_imageres, i, "IMAGE", My.Computer.Screen.Bounds.Size.Width, My.Computer.Screen.Bounds.Size.Height))
                    Next

                Case LogonUI_Modes.CustomImage
                    If IO.File.Exists([LogonElement].ImagePath) Then
                        bmpList.Add(New Bitmap(Image.FromStream(New IO.FileStream([LogonElement].ImagePath, IO.FileMode.Open, IO.FileAccess.Read))))
                    Else
                        bmpList.Add(Color.Black.ToBitmap(My.Computer.Screen.Bounds.Size))
                    End If

                Case LogonUI_Modes.SolidColor
                    bmpList.Add([LogonElement].Color.ToBitmap(My.Computer.Screen.Bounds.Size))

                Case LogonUI_Modes.Wallpaper
                    bmpList.Add(My.Application.GetWallpaper)

            End Select

            For x = 0 To bmpList.Count - 1
                If ReportProgress Then AddNode([TreeView], String.Format("{3}: " & My.Lang.CP_RenderingCustomLogonUI_Progress & " {2} ({0}/{1})", x + 1, bmpList.Count, bmpList(x).Width & "x" & bmpList(x).Height, Now.ToLongTimeString), "info")

                If [LogonElement].Grayscale Then bmpList(x) = bmpList(x).Grayscale
                If [LogonElement].Blur Then bmpList(x) = bmpList(x).Blur([LogonElement].Blur_Intensity)
                If [LogonElement].Noise Then bmpList(x) = bmpList(x).Noise([LogonElement].Noise_Mode, [LogonElement].Noise_Intensity / 100)
            Next

            If bmpList.Count = 1 Then
                bmpList(0).Save(DirX & "\backgroundDefault.jpg", Imaging.ImageFormat.Jpeg)
            Else
                For x = 0 To bmpList.Count - 1
                    bmpList(x).Save(DirX & String.Format("\background{0}x{1}.jpg", bmpList(x).Width, bmpList(x).Height), Imaging.ImageFormat.Jpeg)
                Next
            End If

            Kernel32.Wow64RevertWow64FsRedirection(IntPtr.Zero)
        End If
    End Sub

    Public Sub Apply_LogonUI_8(Optional ByVal [TreeView] As TreeView = Nothing)

        Dim ReportProgress As Boolean = ([TreeView] IsNot Nothing)

        Dim lockimg As String = My.Application.appData & "\LockScreen.png"

        EditReg("HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Windows\Personalization", "NoLockScreen", Windows8.NoLockScreen.ToInteger)
        EditReg("HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Windows\Personalization", "LockScreenImage", lockimg, RegistryValueKind.String)

        Dim rLog As RegistryKey = Registry.CurrentUser.CreateSubKey("Software\WinPaletter\LogonUI")

        Select Case Windows8.LockScreenType
            Case LogonUI_Modes.Default_
                rLog.SetValue("Mode", 0)

            Case LogonUI_Modes.Wallpaper
                rLog.SetValue("Mode", 1)

            Case LogonUI_Modes.CustomImage
                rLog.SetValue("Mode", 2)

            Case LogonUI_Modes.SolidColor
                rLog.SetValue("Mode", 3)
        End Select

        rLog.SetValue("Metro_LockScreenSystemID", Windows8.LockScreenSystemID)
        rLog.SetValue("ImagePath", LogonUI7.ImagePath)
        rLog.SetValue("Color", LogonUI7.Color.ToArgb)
        rLog.SetValue("Blur", LogonUI7.Blur.ToInteger)
        rLog.SetValue("Blur_Intensity", LogonUI7.Blur_Intensity)
        rLog.SetValue("Grayscale", LogonUI7.Grayscale.ToInteger)
        rLog.SetValue("Noise", LogonUI7.Noise.ToInteger)

        Select Case LogonUI7.Noise_Mode
            Case NoiseMode.Aero
                rLog.SetValue("Noise_Mode", 0)

            Case NoiseMode.Acrylic
                rLog.SetValue("Noise_Mode", 1)
        End Select

        rLog.SetValue("Noise_Intensity", LogonUI7.Noise_Intensity)
        rLog.Flush()
        rLog.Close()

        If Not Windows8.NoLockScreen Then

            Try : Kill(lockimg) : Catch : End Try
            Dim bmp As Bitmap = Nothing

            Select Case Windows8.LockScreenType

                Case LogonUI_Modes.Default_
                    Dim syslock As String
                    If Not MainFrm.CP.Windows8.LockScreenSystemID = 1 And Not MainFrm.CP.Windows8.LockScreenSystemID = 3 Then
                        syslock = String.Format(My.PATH_Windows & "\Web\Screen\img10{0}.jpg", MainFrm.CP.Windows8.LockScreenSystemID)
                    Else
                        syslock = String.Format(My.PATH_Windows & "\Web\Screen\img10{0}.png", MainFrm.CP.Windows8.LockScreenSystemID)
                    End If

                    If IO.File.Exists(syslock) Then
                        bmp = Image.FromStream(New IO.FileStream(syslock, IO.FileMode.Open, IO.FileAccess.Read))
                    Else
                        bmp = Color.Black.ToBitmap(My.Computer.Screen.Bounds.Size)
                    End If

                Case LogonUI_Modes.CustomImage

                    If IO.File.Exists(LogonUI7.ImagePath) Then
                        bmp = Image.FromStream(New IO.FileStream(LogonUI7.ImagePath, IO.FileMode.Open, IO.FileAccess.Read))
                    Else
                        bmp = Color.Black.ToBitmap(My.Computer.Screen.Bounds.Size)
                    End If

                Case LogonUI_Modes.SolidColor
                    bmp = LogonUI7.Color.ToBitmap(My.Computer.Screen.Bounds.Size)

                Case LogonUI_Modes.Wallpaper
                    bmp = My.Application.GetWallpaper
            End Select

            If ReportProgress Then AddNode([TreeView], String.Format("{0}: " & My.Lang.CP_RenderingCustomLogonUI, Now.ToLongTimeString), "info")

            If LogonUI7.Grayscale Then bmp = bmp.Grayscale
            If LogonUI7.Blur Then bmp = bmp.Blur(LogonUI7.Blur_Intensity)
            If LogonUI7.Noise Then bmp = bmp.Noise(LogonUI7.Noise_Mode, LogonUI7.Noise_Intensity / 100)
            bmp.Save(lockimg, Drawing.Imaging.ImageFormat.Png)
        End If

    End Sub

    Public Sub Apply_CommandPrompt()
        If CommandPrompt.Enabled Then
            Structures.Console.Save_Console_To_Registry("", CommandPrompt)
            If My.[Settings].CMD_OverrideUserPreferences Then Structures.Console.Save_Console_To_Registry("%SystemRoot%_System32_cmd.exe", CommandPrompt)
        End If
    End Sub

    Public Sub Apply_PowerShell86()
        If PowerShellx86.Enabled And IO.Directory.Exists(Environment.GetEnvironmentVariable("WINDIR") & "\System32\WindowsPowerShell\v1.0") Then
            Structures.Console.Save_Console_To_Registry("%SystemRoot%_System32_WindowsPowerShell_v1.0_powershell.exe", PowerShellx86)
        End If
    End Sub

    Public Sub Apply_PowerShell64()
        If PowerShellx64.Enabled And IO.Directory.Exists(Environment.GetEnvironmentVariable("WINDIR") & "\SysWOW64\WindowsPowerShell\v1.0") Then
            Structures.Console.Save_Console_To_Registry("%SystemRoot%_SysWOW64_WindowsPowerShell_v1.0_powershell.exe", PowerShellx64)
        End If
    End Sub

    Public Sub Apply_Cursors(Optional ByVal [TreeView] As TreeView = Nothing)

        Dim ReportProgress As Boolean = ([TreeView] IsNot Nothing)

        Dim rMain As RegistryKey = Registry.CurrentUser.CreateSubKey("Software\WinPaletter\Cursors")
        rMain.SetValue("", Cursor_Enabled, RegistryValueKind.DWord)
        rMain.Close()

        Dim sw As New Stopwatch
        If ReportProgress Then AddNode([TreeView], String.Format("{0}: " & My.Lang.CP_SavingCursorsColors, Now.ToLongTimeString), "info")

        sw.Reset()
        sw.Start()

        Structures.Cursor.Save_Cursors_To_Registry("Arrow", Cursor_Arrow)
        Structures.Cursor.Save_Cursors_To_Registry("Help", Cursor_Help)
        Structures.Cursor.Save_Cursors_To_Registry("AppLoading", Cursor_AppLoading)
        Structures.Cursor.Save_Cursors_To_Registry("Busy", Cursor_Busy)
        Structures.Cursor.Save_Cursors_To_Registry("Move", Cursor_Move)
        Structures.Cursor.Save_Cursors_To_Registry("NS", Cursor_NS)
        Structures.Cursor.Save_Cursors_To_Registry("EW", Cursor_EW)
        Structures.Cursor.Save_Cursors_To_Registry("NESW", Cursor_NESW)
        Structures.Cursor.Save_Cursors_To_Registry("NWSE", Cursor_NWSE)
        Structures.Cursor.Save_Cursors_To_Registry("Up", Cursor_Up)
        Structures.Cursor.Save_Cursors_To_Registry("Pen", Cursor_Pen)
        Structures.Cursor.Save_Cursors_To_Registry("None", Cursor_None)
        Structures.Cursor.Save_Cursors_To_Registry("Link", Cursor_Link)
        Structures.Cursor.Save_Cursors_To_Registry("Pin", Cursor_Pin)
        Structures.Cursor.Save_Cursors_To_Registry("Person", Cursor_Person)
        Structures.Cursor.Save_Cursors_To_Registry("IBeam", Cursor_IBeam)
        Structures.Cursor.Save_Cursors_To_Registry("Cross", Cursor_Cross)

        If ReportProgress Then AddNode([TreeView], String.Format(My.Lang.CP_Time, sw.ElapsedMilliseconds / 1000), "time")
        sw.Stop()

        If Cursor_Enabled Then
            Excute(CType(Sub()
                             ExportCursors(Me)
                         End Sub, MethodInvoker), [TreeView], My.Lang.CP_RenderingCursors, My.Lang.CP_RenderingCursors_Error, My.Lang.CP_Time)

            If My.[Settings].AutoApplyCursors Then
                Excute(CType(Sub()

                                 SystemParametersInfo(SPI.Cursors.SETCURSORSHADOW, 0, Cursor_Shadow, SPIF.SendChange)
                                 SystemParametersInfo(SPI.Cursors.SETMOUSESONAR, 0, Cursor_Sonar, SPIF.SendChange)
                                 SystemParametersInfo(SPI.Cursors.SETMOUSETRAILS, Cursor_Trails, 0, SPIF.SendChange)

                                 ApplyCursorsToReg()
                             End Sub, MethodInvoker), [TreeView], My.Lang.CP_ApplyingCursors, My.Lang.CP_CursorsApplying_Error, My.Lang.CP_Time)
            Else
                If ReportProgress Then AddNode([TreeView], String.Format("{0}: {1}", Now.ToLongTimeString, My.Lang.CP_Restricted_Cursors), "error")
            End If
        Else

            If My.[Settings].ResetCursorsToAero Then
                If Not My.WXP Then
                    ResetCursorsToAero()
                Else
                    ResetCursorsToNone_XP()
                End If
            End If

        End If

    End Sub
#End Region

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
                               .Cursor_Arrow.PrimaryColor1, .Cursor_Arrow.PrimaryColor2, .Cursor_Arrow.PrimaryColorGradient, .Cursor_Arrow.PrimaryColorGradientMode,
                               .Cursor_Arrow.SecondaryColor1, .Cursor_Arrow.SecondaryColor2, .Cursor_Arrow.SecondaryColorGradient, .Cursor_Arrow.SecondaryColorGradientMode,
                               Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                               .Cursor_Arrow.PrimaryColorNoise, .Cursor_Arrow.PrimaryColorNoiseOpacity, .Cursor_Arrow.SecondaryColorNoise, .Cursor_Arrow.SecondaryColorNoiseOpacity,
                               Nothing, Nothing, Nothing, Nothing,
                               1, i, 0)
                        End With
                        HotPoint = New Point(1, 1)
#End Region
                    Case CursorType.Help
#Region "Help"
                        With [CP]
                            bmp = Draw([Type],
                               .Cursor_Help.PrimaryColor1, .Cursor_Help.PrimaryColor2, .Cursor_Help.PrimaryColorGradient, .Cursor_Help.PrimaryColorGradientMode,
                               .Cursor_Help.SecondaryColor1, .Cursor_Help.SecondaryColor2, .Cursor_Help.SecondaryColorGradient, .Cursor_Help.SecondaryColorGradientMode,
                               Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                               .Cursor_Help.PrimaryColorNoise, .Cursor_Help.PrimaryColorNoiseOpacity, .Cursor_Help.SecondaryColorNoise, .Cursor_Help.SecondaryColorNoiseOpacity,
                               Nothing, Nothing, Nothing, Nothing,
                               1, i, 0)
                        End With
                        HotPoint = New Point(1, 1)
#End Region
                    Case CursorType.None
#Region "None"
                        With [CP]
                            bmp = Draw([Type],
                               .Cursor_None.PrimaryColor1, .Cursor_None.PrimaryColor2, .Cursor_None.PrimaryColorGradient, .Cursor_None.PrimaryColorGradientMode,
                               .Cursor_None.SecondaryColor1, .Cursor_None.SecondaryColor2, .Cursor_None.SecondaryColorGradient, .Cursor_None.SecondaryColorGradientMode,
                               Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                               .Cursor_None.PrimaryColorNoise, .Cursor_None.PrimaryColorNoiseOpacity, .Cursor_None.SecondaryColorNoise, .Cursor_None.SecondaryColorNoiseOpacity,
                               Nothing, Nothing, Nothing, Nothing,
                               1, i, 0)
                        End With
                        HotPoint = New Point(1 + CInt(8 * i), 1 + CInt(8 * i))
#End Region
                    Case CursorType.Move
#Region "Move"
                        With [CP]
                            bmp = Draw([Type],
                               .Cursor_Move.PrimaryColor1, .Cursor_Move.PrimaryColor2, .Cursor_Move.PrimaryColorGradient, .Cursor_Move.PrimaryColorGradientMode,
                               .Cursor_Move.SecondaryColor1, .Cursor_Move.SecondaryColor2, .Cursor_Move.SecondaryColorGradient, .Cursor_Move.SecondaryColorGradientMode,
                               Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                               .Cursor_Move.PrimaryColorNoise, .Cursor_Move.PrimaryColorNoiseOpacity, .Cursor_Move.SecondaryColorNoise, .Cursor_Move.SecondaryColorNoiseOpacity,
                               Nothing, Nothing, Nothing, Nothing,
                               1, i, 0)
                        End With
                        HotPoint = New Point(1 + CInt(11 * i), 1 + CInt(11 * i))
#End Region
                    Case CursorType.Up
#Region "Up"
                        With [CP]
                            bmp = Draw([Type],
                               .Cursor_Up.PrimaryColor1, .Cursor_Up.PrimaryColor2, .Cursor_Up.PrimaryColorGradient, .Cursor_Up.PrimaryColorGradientMode,
                               .Cursor_Up.SecondaryColor1, .Cursor_Up.SecondaryColor2, .Cursor_Up.SecondaryColorGradient, .Cursor_Up.SecondaryColorGradientMode,
                               Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                               .Cursor_Up.PrimaryColorNoise, .Cursor_Up.PrimaryColorNoiseOpacity, .Cursor_Up.SecondaryColorNoise, .Cursor_Up.SecondaryColorNoiseOpacity,
                               Nothing, Nothing, Nothing, Nothing,
                               1, i, 0)
                        End With
                        HotPoint = New Point(1 + CInt(4 * i), 1)
#End Region
                    Case CursorType.NS
#Region "NS"
                        With [CP]
                            bmp = Draw([Type],
                               .Cursor_NS.PrimaryColor1, .Cursor_NS.PrimaryColor2, .Cursor_NS.PrimaryColorGradient, .Cursor_NS.PrimaryColorGradientMode,
                               .Cursor_NS.SecondaryColor1, .Cursor_NS.SecondaryColor2, .Cursor_NS.SecondaryColorGradient, .Cursor_NS.SecondaryColorGradientMode,
                               Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                               .Cursor_NS.PrimaryColorNoise, .Cursor_NS.PrimaryColorNoiseOpacity, .Cursor_NS.SecondaryColorNoise, .Cursor_NS.SecondaryColorNoiseOpacity,
                               Nothing, Nothing, Nothing, Nothing,
                               1, i, 0)
                        End With
                        HotPoint = New Point(1 + CInt(4 * i), 1 + CInt(11 * i))
#End Region
                    Case CursorType.EW
#Region "EW"
                        With [CP]
                            bmp = Draw([Type],
                               .Cursor_EW.PrimaryColor1, .Cursor_EW.PrimaryColor2, .Cursor_EW.PrimaryColorGradient, .Cursor_EW.PrimaryColorGradientMode,
                               .Cursor_EW.SecondaryColor1, .Cursor_EW.SecondaryColor2, .Cursor_EW.SecondaryColorGradient, .Cursor_EW.SecondaryColorGradientMode,
                               Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                               .Cursor_EW.PrimaryColorNoise, .Cursor_EW.PrimaryColorNoiseOpacity, .Cursor_EW.SecondaryColorNoise, .Cursor_EW.SecondaryColorNoiseOpacity,
                               Nothing, Nothing, Nothing, Nothing,
                               1, i, 0)
                        End With
                        HotPoint = New Point(1 + 11 * i, 1 + 4 * i)
#End Region
                    Case CursorType.NESW
#Region "NESW"
                        With [CP]
                            bmp = Draw([Type],
                               .Cursor_NESW.PrimaryColor1, .Cursor_NESW.PrimaryColor2, .Cursor_NESW.PrimaryColorGradient, .Cursor_NESW.PrimaryColorGradientMode,
                               .Cursor_NESW.SecondaryColor1, .Cursor_NESW.SecondaryColor2, .Cursor_NESW.SecondaryColorGradient, .Cursor_NESW.SecondaryColorGradientMode,
                               Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                               .Cursor_NESW.PrimaryColorNoise, .Cursor_NESW.PrimaryColorNoiseOpacity, .Cursor_NESW.SecondaryColorNoise, .Cursor_NESW.SecondaryColorNoiseOpacity,
                               Nothing, Nothing, Nothing, Nothing,
                               1, i, 0)
                        End With
                        HotPoint = New Point(1 + CInt(8 * i), 1 + CInt(8 * i))
#End Region
                    Case CursorType.NWSE
#Region "NWSE"
                        With [CP]
                            bmp = Draw([Type],
                               .Cursor_NWSE.PrimaryColor1, .Cursor_NWSE.PrimaryColor2, .Cursor_NWSE.PrimaryColorGradient, .Cursor_NWSE.PrimaryColorGradientMode,
                               .Cursor_NWSE.SecondaryColor1, .Cursor_NWSE.SecondaryColor2, .Cursor_NWSE.SecondaryColorGradient, .Cursor_NWSE.SecondaryColorGradientMode,
                               Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                               .Cursor_NWSE.PrimaryColorNoise, .Cursor_NWSE.PrimaryColorNoiseOpacity, .Cursor_NWSE.SecondaryColorNoise, .Cursor_NWSE.SecondaryColorNoiseOpacity,
                               Nothing, Nothing, Nothing, Nothing,
                               1, i, 0)
                        End With
                        HotPoint = New Point(1 + CInt(8 * i), 1 + CInt(8 * i))
#End Region
                    Case CursorType.Pen
#Region "Pen"
                        With [CP]
                            bmp = Draw([Type],
                               .Cursor_Pen.PrimaryColor1, .Cursor_Pen.PrimaryColor2, .Cursor_Pen.PrimaryColorGradient, .Cursor_Pen.PrimaryColorGradientMode,
                               .Cursor_Pen.SecondaryColor1, .Cursor_Pen.SecondaryColor2, .Cursor_Pen.SecondaryColorGradient, .Cursor_Pen.SecondaryColorGradientMode,
                               Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                               .Cursor_Pen.PrimaryColorNoise, .Cursor_Pen.PrimaryColorNoiseOpacity, .Cursor_Pen.SecondaryColorNoise, .Cursor_Pen.SecondaryColorNoiseOpacity,
                               Nothing, Nothing, Nothing, Nothing,
                               1, i, 0)
                        End With
                        HotPoint = New Point(1, 1)
#End Region
                    Case CursorType.Link
#Region "Link"
                        With [CP]
                            bmp = Draw([Type],
                               .Cursor_Link.PrimaryColor1, .Cursor_Link.PrimaryColor2, .Cursor_Link.PrimaryColorGradient, .Cursor_Link.PrimaryColorGradientMode,
                               .Cursor_Link.SecondaryColor1, .Cursor_Link.SecondaryColor2, .Cursor_Link.SecondaryColorGradient, .Cursor_Link.SecondaryColorGradientMode,
                               Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                               .Cursor_Link.PrimaryColorNoise, .Cursor_Link.PrimaryColorNoiseOpacity, .Cursor_Link.SecondaryColorNoise, .Cursor_Link.SecondaryColorNoiseOpacity,
                               Nothing, Nothing, Nothing, Nothing,
                               1, i, 0)
                        End With
                        HotPoint = New Point(1 + CInt(6 * i), 1)
#End Region
                    Case CursorType.Pin
#Region "Pin"
                        With [CP]
                            bmp = Draw([Type],
                               .Cursor_Pin.PrimaryColor1, .Cursor_Pin.PrimaryColor2, .Cursor_Pin.PrimaryColorGradient, .Cursor_Pin.PrimaryColorGradientMode,
                               .Cursor_Pin.SecondaryColor1, .Cursor_Pin.SecondaryColor2, .Cursor_Pin.SecondaryColorGradient, .Cursor_Pin.SecondaryColorGradientMode,
                               Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                               .Cursor_Pin.PrimaryColorNoise, .Cursor_Pin.PrimaryColorNoiseOpacity, .Cursor_Pin.SecondaryColorNoise, .Cursor_Pin.SecondaryColorNoiseOpacity,
                               Nothing, Nothing, Nothing, Nothing,
                               1, i, 0)
                        End With
                        HotPoint = New Point(1 + CInt(6 * i), 1)
#End Region
                    Case CursorType.Person
#Region "Person"
                        With [CP]
                            bmp = Draw([Type],
                               .Cursor_Person.PrimaryColor1, .Cursor_Person.PrimaryColor2, .Cursor_Person.PrimaryColorGradient, .Cursor_Person.PrimaryColorGradientMode,
                               .Cursor_Person.SecondaryColor1, .Cursor_Person.SecondaryColor2, .Cursor_Person.SecondaryColorGradient, .Cursor_Person.SecondaryColorGradientMode,
                               Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                               .Cursor_Person.PrimaryColorNoise, .Cursor_Person.PrimaryColorNoiseOpacity, .Cursor_Person.SecondaryColorNoise, .Cursor_Person.SecondaryColorNoiseOpacity,
                               Nothing, Nothing, Nothing, Nothing,
                               1, i, 0)
                        End With
                        HotPoint = New Point(1 + CInt(6 * i), 1)
#End Region
                    Case CursorType.IBeam
#Region "IBeam"
                        With [CP]
                            bmp = Draw([Type],
                               .Cursor_IBeam.PrimaryColor1, .Cursor_IBeam.PrimaryColor2, .Cursor_IBeam.PrimaryColorGradient, .Cursor_IBeam.PrimaryColorGradientMode,
                               .Cursor_IBeam.SecondaryColor1, .Cursor_IBeam.SecondaryColor2, .Cursor_IBeam.SecondaryColorGradient, .Cursor_IBeam.SecondaryColorGradientMode,
                               Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                               .Cursor_IBeam.PrimaryColorNoise, .Cursor_IBeam.PrimaryColorNoiseOpacity, .Cursor_IBeam.SecondaryColorNoise, .Cursor_IBeam.SecondaryColorNoiseOpacity,
                               Nothing, Nothing, Nothing, Nothing,
                               1, i, 0)
                        End With
                        HotPoint = New Point(1 + CInt(4 * i), 1 + CInt(9 * i))
#End Region
                    Case CursorType.Cross
#Region "Cross"
                        With [CP]
                            bmp = Draw([Type],
                               .Cursor_Cross.PrimaryColor1, .Cursor_Cross.PrimaryColor2, .Cursor_Cross.PrimaryColorGradient, .Cursor_Cross.PrimaryColorGradientMode,
                               .Cursor_Cross.SecondaryColor1, .Cursor_Cross.SecondaryColor2, .Cursor_Cross.SecondaryColorGradient, .Cursor_Cross.SecondaryColorGradientMode,
                               Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                               .Cursor_Cross.PrimaryColorNoise, .Cursor_Cross.PrimaryColorNoiseOpacity, .Cursor_Cross.SecondaryColorNoise, .Cursor_Cross.SecondaryColorNoiseOpacity,
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
                                            .Cursor_AppLoading.PrimaryColor1, .Cursor_AppLoading.PrimaryColor2, .Cursor_AppLoading.PrimaryColorGradient, .Cursor_AppLoading.PrimaryColorGradientMode,
                                            .Cursor_AppLoading.SecondaryColor1, .Cursor_AppLoading.SecondaryColor2, .Cursor_AppLoading.SecondaryColorGradient, .Cursor_AppLoading.SecondaryColorGradientMode,
                                            .Cursor_AppLoading.LoadingCircleBack1, .Cursor_AppLoading.LoadingCircleBack2, .Cursor_AppLoading.LoadingCircleBackGradient, .Cursor_AppLoading.LoadingCircleBackGradientMode,
                                            .Cursor_AppLoading.LoadingCircleHot1, .Cursor_AppLoading.LoadingCircleHot2, .Cursor_AppLoading.LoadingCircleHotGradient, .Cursor_AppLoading.LoadingCircleHotGradientMode,
                                            .Cursor_AppLoading.PrimaryColorNoise, .Cursor_AppLoading.PrimaryColorNoiseOpacity, .Cursor_AppLoading.SecondaryColorNoise, .Cursor_AppLoading.SecondaryColorNoiseOpacity,
                                            .Cursor_AppLoading.LoadingCircleBackNoise, .Cursor_AppLoading.LoadingCircleBackNoiseOpacity, .Cursor_AppLoading.LoadingCircleHotNoise, .Cursor_AppLoading.LoadingCircleHotNoiseOpacity,
                                             1, i, ang))

                            HotPoint = New Point(1, 1 + CInt(8 * i))
                        Else
                            bm = New Bitmap(Draw([Type],
                                            Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                                            .Cursor_Busy.LoadingCircleBack1, .Cursor_Busy.LoadingCircleBack2, .Cursor_Busy.LoadingCircleBackGradient, .Cursor_Busy.LoadingCircleBackGradientMode,
                                            .Cursor_Busy.LoadingCircleHot1, .Cursor_Busy.LoadingCircleHot2, .Cursor_Busy.LoadingCircleHotGradient, .Cursor_Busy.LoadingCircleHotGradientMode,
                                            Nothing, Nothing, Nothing, Nothing,
                                            .Cursor_Busy.LoadingCircleBackNoise, .Cursor_Busy.LoadingCircleBackNoiseOpacity, .Cursor_Busy.LoadingCircleHotNoise, .Cursor_Busy.LoadingCircleHotNoiseOpacity,
                                            1, i, ang))

                            HotPoint = New Point(1 + CInt(11 * i), 1 + CInt(11 * i))
                        End If

                        BMPList.Add(bm)
                    Next

                    For ang As Integer = 0 To 180 Step +10
                        Dim bm As Bitmap

                        If [Type] = CursorType.AppLoading Then
                            bm = New Bitmap(Draw([Type],
                                            .Cursor_AppLoading.PrimaryColor1, .Cursor_AppLoading.PrimaryColor2, .Cursor_AppLoading.PrimaryColorGradient, .Cursor_AppLoading.PrimaryColorGradientMode,
                                            .Cursor_AppLoading.SecondaryColor1, .Cursor_AppLoading.SecondaryColor2, .Cursor_AppLoading.SecondaryColorGradient, .Cursor_AppLoading.SecondaryColorGradientMode,
                                            .Cursor_AppLoading.LoadingCircleBack1, .Cursor_AppLoading.LoadingCircleBack2, .Cursor_AppLoading.LoadingCircleBackGradient, .Cursor_AppLoading.LoadingCircleBackGradientMode,
                                            .Cursor_AppLoading.LoadingCircleHot1, .Cursor_AppLoading.LoadingCircleHot2, .Cursor_AppLoading.LoadingCircleHotGradient, .Cursor_AppLoading.LoadingCircleHotGradientMode,
                                            .Cursor_AppLoading.PrimaryColorNoise, .Cursor_AppLoading.PrimaryColorNoiseOpacity, .Cursor_AppLoading.SecondaryColorNoise, .Cursor_AppLoading.SecondaryColorNoiseOpacity,
                                            .Cursor_AppLoading.LoadingCircleBackNoise, .Cursor_AppLoading.LoadingCircleBackNoiseOpacity, .Cursor_AppLoading.LoadingCircleHotNoise, .Cursor_AppLoading.LoadingCircleHotNoiseOpacity,
                                             1, i, ang))

                            HotPoint = New Point(1, 1 + CInt(8 * i))
                        Else
                            bm = New Bitmap(Draw([Type],
                                            Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                                            .Cursor_Busy.LoadingCircleBack1, .Cursor_Busy.LoadingCircleBack2, .Cursor_Busy.LoadingCircleBackGradient, .Cursor_Busy.LoadingCircleBackGradientMode,
                                            .Cursor_Busy.LoadingCircleHot1, .Cursor_Busy.LoadingCircleHot2, .Cursor_Busy.LoadingCircleHotGradient, .Cursor_Busy.LoadingCircleHotGradientMode,
                                            Nothing, Nothing, Nothing, Nothing,
                                            .Cursor_Busy.LoadingCircleBackNoise, .Cursor_Busy.LoadingCircleBackNoiseOpacity, .Cursor_Busy.LoadingCircleHotNoise, .Cursor_Busy.LoadingCircleHotNoiseOpacity,
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
        Dim rMain As RegistryKey = Registry.CurrentUser.CreateSubKey("Control Panel\Cursors\Schemes", True)
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

        rMain = Registry.CurrentUser.CreateSubKey("Control Panel\Cursors", True)
        rMain.SetValue("", "WinPaletter")
        rMain.SetValue("CursorBaseSize", 32, RegistryValueKind.DWord)

        Dim x As String = String.Format("{0}\{1}", Path, "AppLoading_1x.ani")
        rMain.SetValue("AppStarting", x)
        User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_APPSTARTING)

        x = String.Format("{0}\{1}", Path, "Arrow.cur")
        rMain.SetValue("Arrow", x)
        User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_NORMAL)

        x = String.Format("{0}\{1}", Path, "Cross.cur")
        rMain.SetValue("Crosshair", x)
        User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_CROSS)

        x = String.Format("{0}\{1}", Path, "Link.cur")
        rMain.SetValue("Hand", x)
        User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_HAND)

        x = String.Format("{0}\{1}", Path, "Help.cur")
        rMain.SetValue("Help", x)
        User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_HELP)

        x = String.Format("{0}\{1}", Path, "IBeam.cur")
        rMain.SetValue("IBeam", x)
        User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_IBEAM)

        x = String.Format("{0}\{1}", Path, "None.cur")
        rMain.SetValue("No", x)
        User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_NO)

        x = String.Format("{0}\{1}", Path, "Pen.cur")
        rMain.SetValue("NWPen", x)
        'User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_)

        x = String.Format("{0}\{1}", Path, "Person.cur")
        rMain.SetValue("Person", x)
        'User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_APPSTARTING)

        x = String.Format("{0}\{1}", Path, "Pin.cur")
        rMain.SetValue("Pin", x)
        'User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_APPSTARTING)

        x = String.Format("{0}\{1}", Path, "Move.cur")
        rMain.SetValue("SizeAll", x)
        User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_SIZEALL)

        x = String.Format("{0}\{1}", Path, "NESW.cur")
        rMain.SetValue("SizeNESW", x)
        User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_SIZENESW)

        x = String.Format("{0}\{1}", Path, "NS.cur")
        rMain.SetValue("SizeNS", x)
        User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_SIZENS)

        x = String.Format("{0}\{1}", Path, "NWSE.cur")
        rMain.SetValue("SizeNWSE", x)
        User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_SIZENWSE)

        x = String.Format("{0}\{1}", Path, "EW.cur")
        rMain.SetValue("SizeWE", x)
        User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_SIZEWE)

        x = String.Format("{0}\{1}", Path, "Up.cur")
        rMain.SetValue("UpArrow", x)
        User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_UP)

        x = String.Format("{0}\{1}", Path, "Busy_1x.ani")
        rMain.SetValue("Wait", x)
        User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_WAIT)

        rMain.SetValue("Scheme Source", 1, RegistryValueKind.DWord)
        rMain.Close()

        SystemParametersInfo(SPI.Cursors.SETCURSORS, 0, 0, SPIF.UpdateINIFile Or SPIF.SendChange)
    End Sub

    Shared Sub ResetCursorsToAero()
        Try
            Dim path As String = "%SystemRoot%\Cursors"

            Dim R As RegistryKey = Registry.CurrentUser.OpenSubKey("Control Panel\Cursors", True)
            R.SetValue("", "Windows Default", RegistryValueKind.String)
            R.SetValue("CursorBaseSize", 32, RegistryValueKind.DWord)
            R.SetValue("Scheme Source", 2, RegistryValueKind.DWord)

            Dim x As String = String.Format("{0}\{1}", path, "aero_working.ani")
            R.SetValue("AppStarting", x)
            User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_APPSTARTING)

            x = String.Format("{0}\{1}", path, "aero_arrow.cur")
            R.SetValue("Arrow", x)
            User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_NORMAL)

            x = String.Format("")
            R.SetValue("Crosshair", x)
            User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_CROSS)

            x = String.Format("{0}\{1}", path, "aero_link.cur")
            R.SetValue("Hand", x)
            User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_HAND)

            x = String.Format("{0}\{1}", path, "aero_helpsel.cur")
            R.SetValue("Help", x)
            User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_HELP)

            x = String.Format("")
            R.SetValue("IBeam", x)
            User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_IBEAM)

            x = String.Format("{0}\{1}", path, "aero_unavail.cur")
            R.SetValue("No", x)
            User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_NO)

            x = String.Format("{0}\{1}", path, "aero_pen.cur")
            R.SetValue("NWPen", x)
            'User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_)

            x = String.Format("{0}\{1}", path, "aero_person.cur")
            R.SetValue("Person", x)
            'User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_APPSTARTING)

            x = String.Format("{0}\{1}", path, "aero_pin.cur")
            R.SetValue("Pin", x)
            'User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_APPSTARTING)

            x = String.Format("{0}\{1}", path, "aero_move.cur")
            R.SetValue("SizeAll", x)
            User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_SIZEALL)

            x = String.Format("{0}\{1}", path, "aero_nesw.cur")
            R.SetValue("SizeNESW", x)
            User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_SIZENESW)

            x = String.Format("{0}\{1}", path, "aero_ns.cur")
            R.SetValue("SizeNS", x)
            User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_SIZENS)

            x = String.Format("{0}\{1}", path, "aero_nwse.cur")
            R.SetValue("SizeNWSE", x)
            User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_SIZENWSE)

            x = String.Format("{0}\{1}", path, "aero_ew.cur")
            R.SetValue("SizeWE", x)
            User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_SIZEWE)

            x = String.Format("{0}\{1}", path, "aero_up.cur")
            R.SetValue("UpArrow", x)
            User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_UP)

            x = String.Format("{0}\{1}", path, "aero_busy.ani")
            R.SetValue("Wait", x)
            User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_WAIT)

            R.Close()


            If Registry.CurrentUser.OpenSubKey("Control Panel\Cursors\Schemes", False) IsNot Nothing Then
                Dim rx As RegistryKey = Registry.CurrentUser.OpenSubKey("Control Panel\Cursors\Schemes", True)
                rx.DeleteValue("WinPaletter", False)
                rx.Close()
            End If


            SystemParametersInfo(SPI.Cursors.SETCURSORS, 0, 0, SPIF.UpdateINIFile Or SPIF.SendChange)

        Catch ex As Exception

            If MsgBox(My.Lang.CP_RestoreCursorsError, MsgBoxStyle.Exclamation + MsgBoxStyle.OkCancel, My.Lang.CP_RestoreCursorsErrorPressOK,
                     "", "", "", "", My.Lang.CP_RestoreCursorsTip, Ookii.Dialogs.WinForms.TaskDialogIcon.Information) = MsgBoxResult.Ok Then BugReport.ThrowError(ex)

        End Try

    End Sub

    Shared Sub ResetCursorsToNone_XP()
        Try
            Dim path As String = "%SystemRoot%\Cursors"

            Dim R As RegistryKey = Registry.CurrentUser.OpenSubKey("Control Panel\Cursors", True)
            R.SetValue("", "Windows Default", RegistryValueKind.String)
            R.SetValue("CursorBaseSize", 32, RegistryValueKind.DWord)
            R.SetValue("Scheme Source", 2, RegistryValueKind.DWord)

            Dim x As String = ""
            R.SetValue("AppStarting", x)
            User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_APPSTARTING)

            R.SetValue("Arrow", x)
            User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_NORMAL)

            x = String.Format("")
            R.SetValue("Crosshair", x)
            User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_CROSS)

            R.SetValue("Hand", x)
            User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_HAND)

            R.SetValue("Help", x)
            User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_HELP)

            R.SetValue("IBeam", x)
            User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_IBEAM)

            R.SetValue("No", x)
            User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_NO)

            R.SetValue("SizeAll", x)
            User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_SIZEALL)

            R.SetValue("SizeNESW", x)
            User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_SIZENESW)

            R.SetValue("SizeNS", x)
            User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_SIZENS)

            R.SetValue("SizeNWSE", x)
            User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_SIZENWSE)

            R.SetValue("SizeWE", x)
            User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_SIZEWE)

            R.SetValue("UpArrow", x)
            User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_UP)

            R.SetValue("Wait", x)
            User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_WAIT)

            R.Close()

            If Registry.CurrentUser.OpenSubKey("Control Panel\Cursors\Schemes", False) IsNot Nothing Then
                Dim rx As RegistryKey = Registry.CurrentUser.OpenSubKey("Control Panel\Cursors\Schemes", True)
                rx.DeleteValue("WinPaletter", False)
                rx.Close()
            End If

            SystemParametersInfo(SPI.Cursors.SETCURSORS, 0, 0, SPIF.UpdateINIFile Or SPIF.SendChange)

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
        If Windows8 <> DirectCast(obj, CP).Windows8 Then _Equals = False
        If Windows7 <> DirectCast(obj, CP).Windows7 Then _Equals = False
        If WindowsVista <> DirectCast(obj, CP).WindowsVista Then _Equals = False
        If WindowsXP <> DirectCast(obj, CP).WindowsXP Then _Equals = False
        If LogonUI7 <> DirectCast(obj, CP).LogonUI7 Then _Equals = False
        If LogonUIXP <> DirectCast(obj, CP).LogonUIXP Then _Equals = False
        If Win32 <> DirectCast(obj, CP).Win32 Then _Equals = False
        If WindowsEffects <> DirectCast(obj, CP).WindowsEffects Then _Equals = False
        If MetricsFonts <> DirectCast(obj, CP).MetricsFonts Then _Equals = False
        If WallpaperTone_W11 <> DirectCast(obj, CP).WallpaperTone_W11 Then _Equals = False
        If WallpaperTone_W10 <> DirectCast(obj, CP).WallpaperTone_W10 Then _Equals = False
        If WallpaperTone_W8 <> DirectCast(obj, CP).WallpaperTone_W8 Then _Equals = False
        If WallpaperTone_W7 <> DirectCast(obj, CP).WallpaperTone_W7 Then _Equals = False
        If WallpaperTone_WVista <> DirectCast(obj, CP).WallpaperTone_WVista Then _Equals = False
        If WallpaperTone_WXP <> DirectCast(obj, CP).WallpaperTone_WXP Then _Equals = False

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