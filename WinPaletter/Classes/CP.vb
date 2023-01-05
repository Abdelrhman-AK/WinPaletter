Imports System.Reflection
Imports System.Runtime.InteropServices
Imports Microsoft.Win32
Imports Newtonsoft.Json.Linq
Imports WinPaletter.Metrics
Imports WinPaletter.XenonCore

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
    Enum Mode
        Registry
        File
        Empty
    End Enum

#End Region

#Region "Structures"
    Structure Info_Structure : Implements ICloneable
        Public AppVersion As String
        Public PaletteName As String
        Public PaletteDescription As String
        Public PaletteVersion As String
        Public Author As String
        Public AuthorSocialMediaLink As String

        Shared Operator =(First As Info_Structure, Second As Info_Structure) As Boolean
            Return First.Equals(Second)
        End Operator

        Shared Operator <>(First As Info_Structure, Second As Info_Structure) As Boolean
            Return Not First.Equals(Second)
        End Operator

        Public Function Clone() Implements ICloneable.Clone
            Return MemberwiseClone()
        End Function
    End Structure

    Structure Win32UI_Structure : Implements ICloneable
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

        Shared Operator =(First As Win32UI_Structure, Second As Win32UI_Structure) As Boolean
            Return First.Equals(Second)
        End Operator

        Shared Operator <>(First As Win32UI_Structure, Second As Win32UI_Structure) As Boolean
            Return Not First.Equals(Second)
        End Operator

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

            NativeMethods.User32.SetSysColors(C1.Count, C1.ToArray(), C2.ToArray())

            My.Computer.Registry.SetValue("HKEY_CURRENT_USER\Control Panel\Desktop", "UserPreferencesMask", SetUserPreferenceMask(17, EnableTheming), RegistryValueKind.Binary)
            My.Computer.Registry.SetValue("HKEY_CURRENT_USER\Control Panel\Desktop", "UserPreferencesMask", SetUserPreferenceMask(4, EnableGradient), RegistryValueKind.Binary)

            NativeMethods.User32.SystemParametersInfo(Metrics.SPI.SPI_SETFLATMENU, 0, EnableTheming.ToInteger, 0)
            NativeMethods.User32.SystemParametersInfo(Metrics.SPI.SPI_SETGRADIENTCAPTIONS, 0, EnableGradient.ToInteger, 0)

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

    End Structure

    Structure Windows10x_Structure : Implements ICloneable
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


            EditReg("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "AccentColorMenu", Titlebar_Active.Reverse.ToArgb)
            EditReg("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\DWM", "AccentColor", Titlebar_Active.Reverse.ToArgb)
            EditReg("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\DWM", "AccentColorInactive", Titlebar_Inactive.Reverse.ToArgb)

            EditReg("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "SystemUsesLightTheme", WinMode_Light.ToInteger)
            EditReg("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "AppsUseLightTheme", AppMode_Light.ToInteger)
            EditReg("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "EnableTransparency", Transparency.ToInteger)

        End Sub

        Shared Operator =(First As Windows10x_Structure, Second As Windows10x_Structure) As Boolean
            Return First.Equals(Second)
        End Operator

        Shared Operator <>(First As Windows10x_Structure, Second As Windows10x_Structure) As Boolean
            Return Not First.Equals(Second)
        End Operator

        Public Function Clone() Implements ICloneable.Clone
            Return MemberwiseClone()
        End Function

    End Structure

    Structure Windows7_DWM_Structure : Implements ICloneable
        Public ColorizationColor As Color
        Public ColorizationAfterglow As Color
        Public EnableAeroPeek As Boolean
        Public AlwaysHibernateThumbnails As Boolean
        Public ColorizationColorBalance As Integer
        Public ColorizationAfterglowBalance As Integer
        Public ColorizationBlurBalance As Integer
        Public ColorizationGlassReflectionIntensity As Integer
        Public Theme As AeroTheme

        Shared Operator =(First As Windows7_DWM_Structure, Second As Windows7_DWM_Structure) As Boolean
            Return First.Equals(Second)
        End Operator

        Shared Operator <>(First As Windows7_DWM_Structure, Second As Windows7_DWM_Structure) As Boolean
            Return Not First.Equals(Second)
        End Operator

        Public Sub Apply()
            Dim CWindows As String = Environment.GetFolderPath(Environment.SpecialFolder.Windows)

            Select Case Theme
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
    End Structure

    Structure Windows8_Metro_Structure : Implements ICloneable
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

        Shared Operator =(First As Windows8_Metro_Structure, Second As Windows8_Metro_Structure) As Boolean
            Return First.Equals(Second)
        End Operator

        Shared Operator <>(First As Windows8_Metro_Structure, Second As Windows8_Metro_Structure) As Boolean
            Return Not First.Equals(Second)
        End Operator
        Public Function Clone() Implements ICloneable.Clone
            Return MemberwiseClone()
        End Function

        Public Sub Apply()
            EditReg("HKEY_CURRENT_USER\Control Panel\Desktop", "AutoColorization", 0)

            Try
                Select Case Theme
                    Case AeroTheme.Aero
                        NativeMethods.Uxtheme.EnableTheming(1)
                        NativeMethods.Uxtheme.SetSystemVisualStyle("C:\WINDOWS\resources\Themes\Aero\Aero.msstyles", "NormalColor", "NormalSize", 0)
                    Case AeroTheme.AeroLite
                        NativeMethods.Uxtheme.EnableTheming(1)
                        NativeMethods.Uxtheme.SetSystemVisualStyle("C:\WINDOWS\resources\Themes\Aero\AeroLite.msstyles", "NormalColor", "NormalSize", 0)
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

    End Structure

    Structure WinMetrics_Fonts_Structure : Implements ICloneable
        Public Enabled As Boolean
        Public BorderWidth As Integer
        Public CaptionHeight As Integer
        Public CaptionWidth As Integer
        Public IconSpacing As Integer
        Public IconVerticalSpacing As Integer
        Public MenuHeight As Integer
        Public MenuWidth As Integer
        Public MinAnimate As Boolean
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

        Shared Operator =(First As WinMetrics_Fonts_Structure, Second As WinMetrics_Fonts_Structure) As Boolean
            Return First.Equals(Second)
        End Operator

        Shared Operator <>(First As WinMetrics_Fonts_Structure, Second As WinMetrics_Fonts_Structure) As Boolean
            Return Not First.Equals(Second)
        End Operator

        Public Function Clone() Implements ICloneable.Clone
            Return MemberwiseClone()
        End Function

        Public Sub Apply()
            Dim rMain As RegistryKey = Registry.CurrentUser.CreateSubKey("Software\WinPaletter\Metrics")
            rMain.SetValue("", Enabled, RegistryValueKind.DWord)
            rMain.Close()

            If Enabled Then
                Dim NCM As New NONCLIENTMETRICS With {.cbSize = Marshal.SizeOf(NCM)}
                Dim anim As New ANIMATIONINFO With {.cbSize = Marshal.SizeOf(anim)}
                Dim ICO As New ICONMETRICS With {.cbSize = Marshal.SizeOf(ICO)}

                SystemParametersInfo(SPI.SPI_GETNONCLIENTMETRICS, NCM.cbSize, NCM, SPIF.None)
                SystemParametersInfo(SPI.SPI_GETANIMATION, anim.cbSize, anim, SPIF.None)
                SystemParametersInfo(SPI.SPI_GETICONMETRICS, ICO.cbSize, ICO, SPIF.None)

                Dim lfCaptionFont As New LogFont : CaptionFont.ToLogFont(lfCaptionFont)
                Dim lfIconFont As New LogFont : IconFont.ToLogFont(lfIconFont)
                Dim lfMenuFont As New LogFont : MenuFont.ToLogFont(lfMenuFont)
                Dim lfMessageFont As New LogFont : MessageFont.ToLogFont(lfMessageFont)
                Dim lfSMCaptionFont As New LogFont : SmCaptionFont.ToLogFont(lfSMCaptionFont)
                Dim lfStatusFont As New LogFont : StatusFont.ToLogFont(lfStatusFont)

                EditReg("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "CaptionFont", LogFontHelper.LogFontToByte(lfCaptionFont), RegistryValueKind.Binary)
                EditReg("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "IconFont", LogFontHelper.LogFontToByte(lfIconFont), RegistryValueKind.Binary)
                EditReg("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "MenuFont", LogFontHelper.LogFontToByte(lfMenuFont), RegistryValueKind.Binary)
                EditReg("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "MessageFont", LogFontHelper.LogFontToByte(lfMessageFont), RegistryValueKind.Binary)
                EditReg("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "SmCaptionFont", LogFontHelper.LogFontToByte(lfSMCaptionFont), RegistryValueKind.Binary)
                EditReg("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "StatusFont", LogFontHelper.LogFontToByte(lfStatusFont), RegistryValueKind.Binary)

                EditReg("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "BorderWidth", BorderWidth * -15, RegistryValueKind.String)
                EditReg("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "CaptionHeight", CaptionHeight * -15, RegistryValueKind.String)
                EditReg("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "CaptionWidth", CaptionWidth * -15, RegistryValueKind.String)
                EditReg("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "IconSpacing", IconSpacing * -15, RegistryValueKind.String)
                EditReg("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "IconVerticalSpacing", IconVerticalSpacing * -15, RegistryValueKind.String)
                EditReg("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "MenuHeight", MenuHeight * -15, RegistryValueKind.String)
                EditReg("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "MenuWidth", MenuWidth * -15, RegistryValueKind.String)
                EditReg("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "MinAnimate", MinAnimate.ToInteger, RegistryValueKind.String)
                EditReg("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "PaddedBorderWidth", PaddedBorderWidth * -15, RegistryValueKind.String)
                EditReg("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "ScrollHeight", ScrollHeight * -15, RegistryValueKind.String)
                EditReg("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "ScrollWidth", ScrollWidth * -15, RegistryValueKind.String)
                EditReg("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "SmCaptionHeight", SmCaptionHeight * -15, RegistryValueKind.String)
                EditReg("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "SmCaptionWidth", SmCaptionWidth * -15, RegistryValueKind.String)
                EditReg("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "Shell Icon Size", ShellIconSize, RegistryValueKind.String)
                EditReg("HKEY_CURRENT_USER\Software\Microsoft\Windows\Shell\Bags\1\Desktop", "IconSize", DesktopIconSize, RegistryValueKind.String)

                anim.IMinAnimate = MinAnimate.ToInteger

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

                SystemParametersInfo(SPI.SPI_SETNONCLIENTMETRICS, Marshal.SizeOf(NCM), NCM, SPIF.SPIF_SENDCHANGE)
                SystemParametersInfo(SPI.SPI_SETANIMATION, Marshal.SizeOf(anim), anim, SPIF.SPIF_SENDCHANGE)
                SystemParametersInfo(SPI.SPI_SETICONMETRICS, Marshal.SizeOf(ICO), ICO, SPIF.SPIF_SENDCHANGE)


                NativeMethods.User32.SendMessageTimeout(NativeMethods.User32.HWND_BROADCAST, NativeMethods.User32.WM_SETTINGCHANGE, UIntPtr.Zero, Marshal.StringToHGlobalAnsi("WindowMetrics"), NativeMethods.User32.SendMessageTimeoutFlags.SMTO_ABORTIFHUNG, NativeMethods.User32.MSG_TIMEOUT, NativeMethods.User32.RESULT)

                'Try : SendMessageTimeout(HWND_BROADCAST, WM_DWMCOMPOSITIONCHANGED, UIntPtr.Zero, Marshal.StringToHGlobalAnsi("WindowMetrics"), SendMessageTimeoutFlags.SMTO_ABORTIFHUNG, MSG_TIMEOUT, RESULT) : Catch : End Try
                'Try : SendMessageTimeout(HWND_BROADCAST, WM_THEMECHANGED, UIntPtr.Zero, Marshal.StringToHGlobalAnsi("WindowMetrics"), SendMessageTimeoutFlags.SMTO_ABORTIFHUNG, MSG_TIMEOUT, RESULT) : Catch : End Try
                'Try : SendMessageTimeout(HWND_BROADCAST, WM_SYSCOLORCHANGE, UIntPtr.Zero, Marshal.StringToHGlobalAnsi("WindowMetrics"), SendMessageTimeoutFlags.SMTO_ABORTIFHUNG, MSG_TIMEOUT, RESULT) : Catch : End Try
                'Try : SendMessageTimeout(HWND_BROADCAST, WM_PALETTECHANGED, UIntPtr.Zero, Marshal.StringToHGlobalAnsi("WindowMetrics"), SendMessageTimeoutFlags.SMTO_ABORTIFHUNG, MSG_TIMEOUT, RESULT) : Catch : End Try
                'Try : SendMessageTimeout(HWND_BROADCAST, WM_SETTINGCHANGE, UIntPtr.Zero, Marshal.StringToHGlobalAnsi("WindowMetrics"), SendMessageTimeoutFlags.SMTO_ABORTIFHUNG, MSG_TIMEOUT, RESULT) : Catch : End Try
            End If

        End Sub

    End Structure

    Structure LogonUI10x_Structure : Implements ICloneable
        Public DisableAcrylicBackgroundOnLogon As Boolean
        Public DisableLogonBackgroundImage As Boolean
        Public NoLockScreen As Boolean

        Shared Operator =(First As LogonUI10x_Structure, Second As LogonUI10x_Structure) As Boolean
            Return First.Equals(Second)
        End Operator

        Shared Operator <>(First As LogonUI10x_Structure, Second As LogonUI10x_Structure) As Boolean
            Return Not First.Equals(Second)
        End Operator

        Public Function Clone() Implements ICloneable.Clone
            Return MemberwiseClone()
        End Function

        Public Sub Apply()
            EditReg("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\System", "DisableAcrylicBackgroundOnLogon", DisableAcrylicBackgroundOnLogon.ToInteger)
            EditReg("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\System", "DisableLogonBackgroundImage", DisableLogonBackgroundImage.ToInteger)
            EditReg("HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Windows\Personalization", "NoLockScreen", NoLockScreen.ToInteger)
        End Sub

    End Structure

    Structure LogonUI7_Structure : Implements ICloneable
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

        Shared Operator =(First As LogonUI7_Structure, Second As LogonUI7_Structure) As Boolean
            Return First.Equals(Second)
        End Operator

        Shared Operator <>(First As LogonUI7_Structure, Second As LogonUI7_Structure) As Boolean
            Return Not First.Equals(Second)
        End Operator
        Public Function Clone() Implements ICloneable.Clone
            Return MemberwiseClone()
        End Function
    End Structure

    Structure Console_Structure : Implements ICloneable
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

        Shared Function Load_Console_From_Registry([RegKey] As String, [Defaults] As Console_Structure) As Console_Structure

            Dim [Console] As New Console_Structure

            Dim y_cmd As Object

            Dim RegAddress As String = "HKEY_CURRENT_USER\Console" & If(String.IsNullOrEmpty([RegKey]), "", "\" & [RegKey])

            Try
                y_cmd = My.Computer.Registry.GetValue(RegAddress, "ColorTable00", [Defaults].ColorTable00.Reverse.ToArgb)
                [Console].ColorTable00 = Color.FromArgb(255, Color.FromArgb(y_cmd).Reverse)
            Catch
                [Console].ColorTable00 = [Defaults].ColorTable00
            End Try

            Try
                y_cmd = My.Computer.Registry.GetValue(RegAddress, "ColorTable01", [Defaults].ColorTable01.Reverse.ToArgb)
                [Console].ColorTable01 = Color.FromArgb(255, Color.FromArgb(y_cmd).Reverse)
            Catch
                [Console].ColorTable01 = [Defaults].ColorTable01
            End Try

            Try
                y_cmd = My.Computer.Registry.GetValue(RegAddress, "ColorTable02", [Defaults].ColorTable02.Reverse.ToArgb)
                [Console].ColorTable02 = Color.FromArgb(255, Color.FromArgb(y_cmd).Reverse)
            Catch
                [Console].ColorTable02 = [Defaults].ColorTable02
            End Try

            Try
                y_cmd = My.Computer.Registry.GetValue(RegAddress, "ColorTable03", [Defaults].ColorTable03.Reverse.ToArgb)
                [Console].ColorTable03 = Color.FromArgb(255, Color.FromArgb(y_cmd).Reverse)
            Catch
                [Console].ColorTable03 = [Defaults].ColorTable03
            End Try

            Try
                y_cmd = My.Computer.Registry.GetValue(RegAddress, "ColorTable04", [Defaults].ColorTable04.Reverse.ToArgb)
                [Console].ColorTable04 = Color.FromArgb(255, Color.FromArgb(y_cmd).Reverse)
            Catch
                [Console].ColorTable04 = [Defaults].ColorTable04
            End Try

            Try
                y_cmd = My.Computer.Registry.GetValue(RegAddress, "ColorTable05", [Defaults].ColorTable05.Reverse.ToArgb)
                [Console].ColorTable05 = Color.FromArgb(255, Color.FromArgb(y_cmd).Reverse)
            Catch
                [Console].ColorTable05 = [Defaults].ColorTable05
            End Try

            Try
                y_cmd = My.Computer.Registry.GetValue(RegAddress, "ColorTable06", [Defaults].ColorTable06.Reverse.ToArgb)
                [Console].ColorTable06 = Color.FromArgb(255, Color.FromArgb(y_cmd).Reverse)
            Catch
                [Console].ColorTable06 = [Defaults].ColorTable06
            End Try

            Try
                y_cmd = My.Computer.Registry.GetValue(RegAddress, "ColorTable07", [Defaults].ColorTable07.Reverse.ToArgb)
                [Console].ColorTable07 = Color.FromArgb(255, Color.FromArgb(y_cmd).Reverse)
            Catch
                [Console].ColorTable07 = [Defaults].ColorTable07
            End Try

            Try
                y_cmd = My.Computer.Registry.GetValue(RegAddress, "ColorTable08", [Defaults].ColorTable08.Reverse.ToArgb)
                [Console].ColorTable08 = Color.FromArgb(255, Color.FromArgb(y_cmd).Reverse)
            Catch
                [Console].ColorTable08 = [Defaults].ColorTable08
            End Try

            Try
                y_cmd = My.Computer.Registry.GetValue(RegAddress, "ColorTable09", [Defaults].ColorTable09.Reverse.ToArgb)
                [Console].ColorTable09 = Color.FromArgb(255, Color.FromArgb(y_cmd).Reverse)
            Catch
                [Console].ColorTable09 = [Defaults].ColorTable09
            End Try

            Try
                y_cmd = My.Computer.Registry.GetValue(RegAddress, "ColorTable10", [Defaults].ColorTable10.Reverse.ToArgb)
                [Console].ColorTable10 = Color.FromArgb(255, Color.FromArgb(y_cmd).Reverse)
            Catch
                [Console].ColorTable10 = [Defaults].ColorTable10
            End Try

            Try
                y_cmd = My.Computer.Registry.GetValue(RegAddress, "ColorTable11", [Defaults].ColorTable11.Reverse.ToArgb)
                [Console].ColorTable11 = Color.FromArgb(255, Color.FromArgb(y_cmd).Reverse)
            Catch
                [Console].ColorTable11 = [Defaults].ColorTable11
            End Try

            Try
                y_cmd = My.Computer.Registry.GetValue(RegAddress, "ColorTable12", [Defaults].ColorTable12.Reverse.ToArgb)
                [Console].ColorTable12 = Color.FromArgb(255, Color.FromArgb(y_cmd).Reverse)
            Catch
                [Console].ColorTable12 = [Defaults].ColorTable12
            End Try

            Try
                y_cmd = My.Computer.Registry.GetValue(RegAddress, "ColorTable13", [Defaults].ColorTable13.Reverse.ToArgb)
                [Console].ColorTable13 = Color.FromArgb(255, Color.FromArgb(y_cmd).Reverse)
            Catch
                [Console].ColorTable13 = [Defaults].ColorTable13
            End Try

            Try
                y_cmd = My.Computer.Registry.GetValue(RegAddress, "ColorTable14", [Defaults].ColorTable14.Reverse.ToArgb)
                [Console].ColorTable14 = Color.FromArgb(255, Color.FromArgb(y_cmd).Reverse)
            Catch
                [Console].ColorTable14 = [Defaults].ColorTable14
            End Try

            Try
                y_cmd = My.Computer.Registry.GetValue(RegAddress, "ColorTable15", [Defaults].ColorTable15.Reverse.ToArgb)
                [Console].ColorTable15 = Color.FromArgb(255, Color.FromArgb(y_cmd).Reverse)
            Catch
                [Console].ColorTable15 = [Defaults].ColorTable15
            End Try

            Try
                y_cmd = My.Computer.Registry.GetValue(RegAddress, "PopupColors", Convert.ToInt32([Defaults].PopupBackground.ToString("X") & [Defaults].PopupForeground.ToString("X"), 16))
                Dim d As String = CInt(y_cmd).ToString("X")

                If d.Count = 1 Then d = 0 & d
                [Console].PopupBackground = Convert.ToInt32(d.Chars(0), 16)
                [Console].PopupForeground = Convert.ToInt32(d.Chars(1), 16)
            Catch
                [Console].PopupBackground = [Defaults].PopupBackground
                [Console].PopupForeground = [Defaults].PopupForeground
            End Try

            Try
                y_cmd = My.Computer.Registry.GetValue(RegAddress, "ScreenColors", Convert.ToInt32([Defaults].ScreenColorsBackground.ToString("X") & [Defaults].ScreenColorsForeground.ToString("X"), 16))
                Dim d As String = CInt(y_cmd).ToString("X")

                If d.Count = 1 Then d = 0 & d
                [Console].ScreenColorsBackground = Convert.ToInt32(d.Chars(0), 16)
                [Console].ScreenColorsForeground = Convert.ToInt32(d.Chars(1), 16)
            Catch
                [Console].ScreenColorsBackground = [Defaults].ScreenColorsBackground
                [Console].ScreenColorsForeground = [Defaults].ScreenColorsForeground
            End Try

            Try
                y_cmd = My.Computer.Registry.GetValue(RegAddress, "CursorSize", 25)
                [Console].CursorSize = y_cmd
            Catch
                [Console].CursorSize = 25
            End Try

            Try
                y_cmd = My.Computer.Registry.GetValue(RegAddress, "FaceName", [Defaults].FaceName)

                If IsFontInstalled(y_cmd) Then
                    [Console].FaceName = y_cmd
                Else
                    [Console].FaceName = [Defaults].FaceName
                End If

            Catch
                [Console].FaceName = [Defaults].FaceName
            End Try

            Try
                y_cmd = My.Computer.Registry.GetValue(RegAddress, "FontFamily", If(Not [Defaults].FontRaster, 54, 1))
                [Console].FontRaster = If(y_cmd = 1 Or y_cmd = 0 Or y_cmd = 48, True, False)
                If [Console].FaceName.ToLower = "terminal" Then [Console].FontRaster = True
            Catch
                [Console].FontRaster = [Defaults].FontRaster
            End Try

            Try
                y_cmd = My.Computer.Registry.GetValue(RegAddress, "FontSize", [Defaults].FontSize)
                If y_cmd = 0 And Not [Console].FontRaster Then [Console].FontSize = [Defaults].FontSize Else [Console].FontSize = y_cmd
            Catch
                [Console].FontSize = [Defaults].FontSize
            End Try


            Try
                y_cmd = My.Computer.Registry.GetValue(RegAddress, "FontWeight", 400)
                [Console].FontWeight = y_cmd
            Catch
                [Console].FontWeight = 400
            End Try

            If My.W10_1909 Then
                Try
                    y_cmd = My.Computer.Registry.GetValue(RegAddress, "CursorColor", Color.White.Reverse.ToArgb)
                    [Console].W10_1909_CursorColor = Color.FromArgb(255, Color.FromArgb(y_cmd).Reverse)
                Catch
                    [Console].W10_1909_CursorColor = Color.White
                End Try

                Try
                    y_cmd = My.Computer.Registry.GetValue(RegAddress, "CursorType", 1)
                    [Console].W10_1909_CursorType = y_cmd
                Catch
                    [Console].W10_1909_CursorType = 1
                End Try

                Try
                    y_cmd = My.Computer.Registry.GetValue(RegAddress, "ForceV2", True)
                    [Console].W10_1909_ForceV2 = y_cmd
                Catch
                    [Console].W10_1909_ForceV2 = True
                End Try

                Try
                    y_cmd = My.Computer.Registry.GetValue(RegAddress, "LineSelection", False)
                    [Console].W10_1909_LineSelection = y_cmd
                Catch
                    [Console].W10_1909_LineSelection = False
                End Try


                Try
                    y_cmd = My.Computer.Registry.GetValue(RegAddress, "TerminalScrolling", False)
                    [Console].W10_1909_TerminalScrolling = y_cmd
                Catch
                    [Console].W10_1909_TerminalScrolling = False
                End Try


                Try
                    y_cmd = My.Computer.Registry.GetValue(RegAddress, "WindowAlpha", 255)
                    [Console].W10_1909_WindowAlpha = y_cmd
                Catch
                    [Console].W10_1909_WindowAlpha = 255
                End Try
            End If

            Return [Console]
        End Function
        Shared Sub Save_Console_To_Registry([RegKey] As String, [Console] As Console_Structure)

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
        Shared Sub Write_Console_To_ListOfString(Signature As String, [Console] As Console_Structure, tx As List(Of String))
            tx.Add(String.Format("<{0}>", Signature))
            tx.Add(String.Format("*Terminal_{0}_Enabled= {1}", Signature, [Console].Enabled))
            tx.Add(String.Format("*{0}_ColorTable00= {1}", Signature, [Console].ColorTable00.ToArgb))
            tx.Add(String.Format("*{0}_ColorTable01= {1}", Signature, [Console].ColorTable01.ToArgb))
            tx.Add(String.Format("*{0}_ColorTable02= {1}", Signature, [Console].ColorTable02.ToArgb))
            tx.Add(String.Format("*{0}_ColorTable03= {1}", Signature, [Console].ColorTable03.ToArgb))
            tx.Add(String.Format("*{0}_ColorTable04= {1}", Signature, [Console].ColorTable04.ToArgb))
            tx.Add(String.Format("*{0}_ColorTable05= {1}", Signature, [Console].ColorTable05.ToArgb))
            tx.Add(String.Format("*{0}_ColorTable06= {1}", Signature, [Console].ColorTable06.ToArgb))
            tx.Add(String.Format("*{0}_ColorTable07= {1}", Signature, [Console].ColorTable07.ToArgb))
            tx.Add(String.Format("*{0}_ColorTable08= {1}", Signature, [Console].ColorTable08.ToArgb))
            tx.Add(String.Format("*{0}_ColorTable09= {1}", Signature, [Console].ColorTable09.ToArgb))
            tx.Add(String.Format("*{0}_ColorTable10= {1}", Signature, [Console].ColorTable10.ToArgb))
            tx.Add(String.Format("*{0}_ColorTable11= {1}", Signature, [Console].ColorTable11.ToArgb))
            tx.Add(String.Format("*{0}_ColorTable12= {1}", Signature, [Console].ColorTable12.ToArgb))
            tx.Add(String.Format("*{0}_ColorTable13= {1}", Signature, [Console].ColorTable13.ToArgb))
            tx.Add(String.Format("*{0}_ColorTable14= {1}", Signature, [Console].ColorTable14.ToArgb))
            tx.Add(String.Format("*{0}_ColorTable15= {1}", Signature, [Console].ColorTable15.ToArgb))
            tx.Add(String.Format("*{0}_PopupForeground= {1}", Signature, [Console].PopupForeground))
            tx.Add(String.Format("*{0}_PopupBackground= {1}", Signature, [Console].PopupBackground))
            tx.Add(String.Format("*{0}_ScreenColorsForeground= {1}", Signature, [Console].ScreenColorsForeground))
            tx.Add(String.Format("*{0}_ScreenColorsBackground= {1}", Signature, [Console].ScreenColorsBackground))
            tx.Add(String.Format("*{0}_CursorSize= {1}", Signature, [Console].CursorSize))
            tx.Add(String.Format("*{0}_FaceName= {1}", Signature, [Console].FaceName))
            tx.Add(String.Format("*{0}_FontRaster= {1}", Signature, [Console].FontRaster))
            tx.Add(String.Format("*{0}_FontSize= {1}", Signature, [Console].FontSize))
            tx.Add(String.Format("*{0}_FontWeight= {1}", Signature, [Console].FontWeight))
            tx.Add(String.Format("*{0}_1909_CursorType= {1}", Signature, [Console].W10_1909_CursorType))
            tx.Add(String.Format("*{0}_1909_CursorColor= {1}", Signature, [Console].W10_1909_CursorColor.ToArgb))
            tx.Add(String.Format("*{0}_1909_ForceV2= {1}", Signature, [Console].W10_1909_ForceV2))
            tx.Add(String.Format("*{0}_1909_LineSelection= {1}", Signature, [Console].W10_1909_LineSelection))
            tx.Add(String.Format("*{0}_1909_TerminalScrolling= {1}", Signature, [Console].W10_1909_TerminalScrolling))
            tx.Add(String.Format("*{0}_1909_WindowAlpha= {1}", Signature, [Console].W10_1909_WindowAlpha))
            tx.Add(String.Format("</{0}>", Signature) & vbCrLf)
        End Sub
        Shared Function Load_Console_From_ListOfString(tx As List(Of String)) As Console_Structure
            Dim [Console] As New Console_Structure

            For Each lin As String In tx
                If lin.ToLower.StartsWith("ColorTable00= ".ToLower) Then [Console].ColorTable00 = Color.FromArgb(lin.ToLower.Remove(0, "ColorTable00= ".Count))
                If lin.ToLower.StartsWith("ColorTable01= ".ToLower) Then [Console].ColorTable01 = Color.FromArgb(lin.ToLower.Remove(0, "ColorTable01= ".Count))
                If lin.ToLower.StartsWith("ColorTable02= ".ToLower) Then [Console].ColorTable02 = Color.FromArgb(lin.ToLower.Remove(0, "ColorTable02= ".Count))
                If lin.ToLower.StartsWith("ColorTable03= ".ToLower) Then [Console].ColorTable03 = Color.FromArgb(lin.ToLower.Remove(0, "ColorTable03= ".Count))
                If lin.ToLower.StartsWith("ColorTable04= ".ToLower) Then [Console].ColorTable04 = Color.FromArgb(lin.ToLower.Remove(0, "ColorTable04= ".Count))
                If lin.ToLower.StartsWith("ColorTable05= ".ToLower) Then [Console].ColorTable05 = Color.FromArgb(lin.ToLower.Remove(0, "ColorTable05= ".Count))
                If lin.ToLower.StartsWith("ColorTable06= ".ToLower) Then [Console].ColorTable06 = Color.FromArgb(lin.ToLower.Remove(0, "ColorTable06= ".Count))
                If lin.ToLower.StartsWith("ColorTable07= ".ToLower) Then [Console].ColorTable07 = Color.FromArgb(lin.ToLower.Remove(0, "ColorTable07= ".Count))
                If lin.ToLower.StartsWith("ColorTable08= ".ToLower) Then [Console].ColorTable08 = Color.FromArgb(lin.ToLower.Remove(0, "ColorTable08= ".Count))
                If lin.ToLower.StartsWith("ColorTable09= ".ToLower) Then [Console].ColorTable09 = Color.FromArgb(lin.ToLower.Remove(0, "ColorTable09= ".Count))
                If lin.ToLower.StartsWith("ColorTable10= ".ToLower) Then [Console].ColorTable10 = Color.FromArgb(lin.ToLower.Remove(0, "ColorTable10= ".Count))
                If lin.ToLower.StartsWith("ColorTable11= ".ToLower) Then [Console].ColorTable11 = Color.FromArgb(lin.ToLower.Remove(0, "ColorTable11= ".Count))
                If lin.ToLower.StartsWith("ColorTable12= ".ToLower) Then [Console].ColorTable12 = Color.FromArgb(lin.ToLower.Remove(0, "ColorTable12= ".Count))
                If lin.ToLower.StartsWith("ColorTable13= ".ToLower) Then [Console].ColorTable13 = Color.FromArgb(lin.ToLower.Remove(0, "ColorTable13= ".Count))
                If lin.ToLower.StartsWith("ColorTable14= ".ToLower) Then [Console].ColorTable14 = Color.FromArgb(lin.ToLower.Remove(0, "ColorTable14= ".Count))
                If lin.ToLower.StartsWith("ColorTable15= ".ToLower) Then [Console].ColorTable15 = Color.FromArgb(lin.ToLower.Remove(0, "ColorTable15= ".Count))
                If lin.ToLower.StartsWith("PopupForeground= ".ToLower) Then [Console].PopupForeground = lin.ToLower.Remove(0, "PopupForeground= ".Count)
                If lin.ToLower.StartsWith("PopupBackground= ".ToLower) Then [Console].PopupBackground = lin.ToLower.Remove(0, "PopupBackground= ".Count)
                If lin.ToLower.StartsWith("ScreenColorsForeground= ".ToLower) Then [Console].ScreenColorsForeground = lin.ToLower.Remove(0, "ScreenColorsForeground= ".Count)
                If lin.ToLower.StartsWith("ScreenColorsBackground= ".ToLower) Then [Console].ScreenColorsBackground = lin.ToLower.Remove(0, "ScreenColorsBackground= ".Count)
                If lin.ToLower.StartsWith("CursorSize= ".ToLower) Then [Console].CursorSize = lin.ToLower.Remove(0, "CursorSize= ".Count)
                If lin.ToLower.StartsWith("FaceName= ".ToLower) Then [Console].FaceName = lin.ToLower.Remove(0, "FaceName= ".Count)
                If lin.ToLower.StartsWith("FontRaster= ".ToLower) Then [Console].FontRaster = lin.ToLower.Remove(0, "FontRaster= ".Count)
                If lin.ToLower.StartsWith("FontSize= ".ToLower) Then [Console].FontSize = lin.ToLower.Remove(0, "FontSize= ".Count)
                If lin.ToLower.StartsWith("FontWeight= ".ToLower) Then [Console].FontWeight = lin.ToLower.Remove(0, "FontWeight= ".Count)
                If lin.ToLower.StartsWith("1909_CursorType= ".ToLower) Then [Console].W10_1909_CursorType = lin.ToLower.Remove(0, "1909_CursorType= ".Count)
                If lin.ToLower.StartsWith("1909_CursorColor= ".ToLower) Then [Console].W10_1909_CursorColor = Color.FromArgb(lin.ToLower.Remove(0, "1909_CursorColor= ".Count))
                If lin.ToLower.StartsWith("1909_ForceV2= ".ToLower) Then [Console].W10_1909_ForceV2 = lin.ToLower.Remove(0, "1909_ForceV2= ".Count)
                If lin.ToLower.StartsWith("1909_lin.ToLowereSelection= ".ToLower) Then [Console].W10_1909_LineSelection = lin.ToLower.Remove(0, "1909_lin.ToLowereSelection= ".Count)
                If lin.ToLower.StartsWith("1909_TerminalScrollin.ToLowerg= ".ToLower) Then [Console].W10_1909_TerminalScrolling = lin.ToLower.Remove(0, "1909_TerminalScrollin.ToLowerg= ".Count)
                If lin.ToLower.StartsWith("1909_WindowAlpha= ".ToLower) Then [Console].W10_1909_WindowAlpha = lin.ToLower.Remove(0, "1909_WindowAlpha= ".Count)
            Next

            Return [Console]
        End Function

        Shared Operator =(First As Console_Structure, Second As Console_Structure) As Boolean
            Return First.Equals(Second)
        End Operator

        Shared Operator <>(First As Console_Structure, Second As Console_Structure) As Boolean
            Return Not First.Equals(Second)
        End Operator

        Public Function Clone() Implements ICloneable.Clone
            Return MemberwiseClone()
        End Function

    End Structure

    Structure Cursor_Structure : Implements ICloneable
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

        Shared Function Load_Cursor_From_Registry([subKey] As String) As Cursor_Structure
            Dim [Cursor] As New Cursor_Structure
            Dim rMain As RegistryKey = Registry.CurrentUser.CreateSubKey("Software\WinPaletter\Cursors")

            Dim r As RegistryKey = rMain
            r.CreateSubKey([subKey])
            r = r.OpenSubKey([subKey])

            [Cursor].PrimaryColor1 = Color.FromArgb(r.GetValue("PrimaryColor1", Color.White.ToArgb))
            [Cursor].PrimaryColor2 = Color.FromArgb(r.GetValue("PrimaryColor2", Color.White.ToArgb))
            [Cursor].SecondaryColor1 = Color.FromArgb(r.GetValue("SecondaryColor1", If([subKey].ToLower <> "none", Color.Black, Color.Red).ToArgb))
            [Cursor].SecondaryColor2 = Color.FromArgb(r.GetValue("SecondaryColor2", If([subKey].ToLower <> "none", Color.FromArgb(64, 65, 75), Color.Red).ToArgb))
            [Cursor].LoadingCircleBack1 = Color.FromArgb(r.GetValue("LoadingCircleBack1", Color.FromArgb(42, 151, 243).ToArgb))
            [Cursor].LoadingCircleBack2 = Color.FromArgb(r.GetValue("LoadingCircleBack2", Color.FromArgb(42, 151, 243).ToArgb))
            [Cursor].LoadingCircleHot1 = Color.FromArgb(r.GetValue("LoadingCircleHot1", Color.FromArgb(37, 204, 255).ToArgb))
            [Cursor].LoadingCircleHot2 = Color.FromArgb(r.GetValue("LoadingCircleHot2", Color.FromArgb(37, 204, 255).ToArgb))

            [Cursor].PrimaryColorGradient = r.GetValue("PrimaryColorGradient", False)
            [Cursor].SecondaryColorGradient = r.GetValue("SecondaryColorGradient", True)
            [Cursor].LoadingCircleBackGradient = r.GetValue("LoadingCircleBackGradient", False)
            [Cursor].LoadingCircleHotGradient = r.GetValue("LoadingCircleHotGradient", False)

            [Cursor].PrimaryColorNoise = r.GetValue("PrimaryColorNoise", False)
            [Cursor].SecondaryColorNoise = r.GetValue("SecondaryColorNoise", False)
            [Cursor].LoadingCircleBackNoise = r.GetValue("LoadingCircleBackNoise", False)
            [Cursor].LoadingCircleHotNoise = r.GetValue("LoadingCircleHotNoise", False)

            [Cursor].PrimaryColorGradientMode = ReturnGradientModeFromString(r.GetValue("PrimaryColorGradientMode", "Circle"))
            [Cursor].SecondaryColorGradientMode = ReturnGradientModeFromString(r.GetValue("SecondaryColorGradientMode", "Vertical"))
            [Cursor].LoadingCircleBackGradientMode = ReturnGradientModeFromString(r.GetValue("LoadingCircleBackGradientMode", "Circle"))
            [Cursor].LoadingCircleHotGradientMode = ReturnGradientModeFromString(r.GetValue("LoadingCircleHotGradientMode", "Circle"))

            [Cursor].PrimaryColorNoiseOpacity = r.GetValue("PrimaryColorNoiseOpacity", 25) / 100
            [Cursor].SecondaryColorNoiseOpacity = r.GetValue("SecondaryColorNoiseOpacity", 25) / 100
            [Cursor].LoadingCircleBackNoiseOpacity = r.GetValue("LoadingCircleBackNoiseOpacity", 25) / 100
            [Cursor].LoadingCircleHotNoiseOpacity = r.GetValue("LoadingCircleHotNoiseOpacity", 25) / 100

            Return [Cursor]
        End Function

        Shared Sub Save_Cursors_To_Registry(subKey As String, [Cursor] As Cursor_Structure)

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

        Shared Function Load_Cursor_From_ListOfString(tx As List(Of String)) As Cursor_Structure
            Dim [Cursor] As New Cursor_Structure

            For Each lin As String In tx
                If lin.ToLower.StartsWith("PrimaryColor1= ".ToLower) Then [Cursor].PrimaryColor1 = Color.FromArgb(lin.Remove(0, "PrimaryColor1= ".Count))
                If lin.ToLower.StartsWith("PrimaryColor2= ".ToLower) Then [Cursor].PrimaryColor2 = Color.FromArgb(lin.Remove(0, "PrimaryColor2= ".Count))
                If lin.ToLower.StartsWith("PrimaryColorGradient= ".ToLower) Then [Cursor].PrimaryColorGradient = lin.Remove(0, "PrimaryColorGradient= ".Count)
                If lin.ToLower.StartsWith("PrimaryColorGradientMode= ".ToLower) Then [Cursor].PrimaryColorGradientMode = ReturnGradientModeFromString(lin.Remove(0, "PrimaryColorGradientMode= ".Count))
                If lin.ToLower.StartsWith("PrimaryColorNoise= ".ToLower) Then [Cursor].PrimaryColorNoise = lin.Remove(0, "PrimaryColorNoise= ".Count)
                If lin.ToLower.StartsWith("PrimaryColorNoiseOpacity= ".ToLower) Then [Cursor].PrimaryColorNoiseOpacity = lin.Remove(0, "PrimaryColorNoiseOpacity= ".Count)
                If lin.ToLower.StartsWith("SecondaryColor1= ".ToLower) Then [Cursor].SecondaryColor1 = Color.FromArgb(lin.Remove(0, "SecondaryColor1= ".Count))
                If lin.ToLower.StartsWith("SecondaryColor2= ".ToLower) Then [Cursor].SecondaryColor2 = Color.FromArgb(lin.Remove(0, "SecondaryColor2= ".Count))
                If lin.ToLower.StartsWith("SecondaryColorGradient= ".ToLower) Then [Cursor].SecondaryColorGradient = lin.Remove(0, "SecondaryColorGradient= ".Count)
                If lin.ToLower.StartsWith("SecondaryColorGradientMode= ".ToLower) Then [Cursor].SecondaryColorGradientMode = ReturnGradientModeFromString(lin.Remove(0, "SecondaryColorGradientMode= ".Count))
                If lin.ToLower.StartsWith("SecondaryColorNoise= ".ToLower) Then [Cursor].SecondaryColorNoise = lin.Remove(0, "SecondaryColorNoise= ".Count)
                If lin.ToLower.StartsWith("SecondaryColorNoiseOpacity= ".ToLower) Then [Cursor].SecondaryColorNoiseOpacity = lin.Remove(0, "SecondaryColorNoiseOpacity= ".Count)
                If lin.ToLower.StartsWith("LoadingCircleBack1= ".ToLower) Then [Cursor].LoadingCircleBack1 = Color.FromArgb(lin.Remove(0, "LoadingCircleBack1= ".Count))
                If lin.ToLower.StartsWith("LoadingCircleBack2= ".ToLower) Then [Cursor].LoadingCircleBack2 = Color.FromArgb(lin.Remove(0, "LoadingCircleBack2= ".Count))
                If lin.ToLower.StartsWith("LoadingCircleBackGradient= ".ToLower) Then [Cursor].LoadingCircleBackGradient = lin.Remove(0, "LoadingCircleBackGradient= ".Count)
                If lin.ToLower.StartsWith("LoadingCircleBackGradientMode= ".ToLower) Then [Cursor].LoadingCircleBackGradientMode = ReturnGradientModeFromString(lin.Remove(0, "LoadingCircleBackGradientMode= ".Count))
                If lin.ToLower.StartsWith("LoadingCircleBackNoise= ".ToLower) Then [Cursor].LoadingCircleBackNoise = lin.Remove(0, "LoadingCircleBackNoise= ".Count)
                If lin.ToLower.StartsWith("LoadingCircleBackNoiseOpacity= ".ToLower) Then [Cursor].LoadingCircleBackNoiseOpacity = lin.Remove(0, "LoadingCircleBackNoiseOpacity= ".Count)
                If lin.ToLower.StartsWith("LoadingCircleHot1= ".ToLower) Then [Cursor].LoadingCircleHot1 = Color.FromArgb(lin.Remove(0, "LoadingCircleHot1= ".Count))
                If lin.ToLower.StartsWith("LoadingCircleHot2= ".ToLower) Then [Cursor].LoadingCircleHot2 = Color.FromArgb(lin.Remove(0, "LoadingCircleHot2= ".Count))
                If lin.ToLower.StartsWith("LoadingCircleHotGradient= ".ToLower) Then [Cursor].LoadingCircleHotGradient = lin.Remove(0, "LoadingCircleHotGradient= ".Count)
                If lin.ToLower.StartsWith("LoadingCircleHotGradientMode= ".ToLower) Then [Cursor].LoadingCircleHotGradientMode = ReturnGradientModeFromString(lin.Remove(0, "LoadingCircleHotGradientMode= ".Count))
                If lin.ToLower.StartsWith("LoadingCircleHotNoise= ".ToLower) Then [Cursor].LoadingCircleHotNoise = lin.Remove(0, "LoadingCircleHotNoise= ".Count)
                If lin.ToLower.StartsWith("LoadingCircleHotNoiseOpacity= ".ToLower) Then [Cursor].LoadingCircleHotNoiseOpacity = lin.Remove(0, "LoadingCircleHotNoiseOpacity= ".Count)
            Next

            Return [Cursor]
        End Function

        Shared Sub Write_Cursors_To_ListOfString(Signature As String, [Cursor] As Cursor_Structure, tx As List(Of String))
            tx.Add(String.Format("<{0}>", Signature))
            tx.Add(String.Format("*Cursor_{0}_PrimaryColor1= {1}", Signature, [Cursor].PrimaryColor1.ToArgb))
            tx.Add(String.Format("*Cursor_{0}_PrimaryColor2= {1}", Signature, [Cursor].PrimaryColor2.ToArgb))
            tx.Add(String.Format("*Cursor_{0}_PrimaryColorGradient= {1}", Signature, [Cursor].PrimaryColorGradient))
            tx.Add(String.Format("*Cursor_{0}_PrimaryColorGradientMode= {1}", Signature, ReturnStringFromGradientMode([Cursor].PrimaryColorGradientMode)))
            tx.Add(String.Format("*Cursor_{0}_PrimaryColorNoise= {1}", Signature, [Cursor].PrimaryColorNoise))
            tx.Add(String.Format("*Cursor_{0}_PrimaryColorNoiseOpacity= {1}", Signature, [Cursor].PrimaryColorNoiseOpacity))
            tx.Add(String.Format("*Cursor_{0}_SecondaryColor1= {1}", Signature, [Cursor].SecondaryColor1.ToArgb))
            tx.Add(String.Format("*Cursor_{0}_SecondaryColor2= {1}", Signature, [Cursor].SecondaryColor2.ToArgb))
            tx.Add(String.Format("*Cursor_{0}_SecondaryColorGradient= {1}", Signature, [Cursor].SecondaryColorGradient))
            tx.Add(String.Format("*Cursor_{0}_SecondaryColorGradientMode= {1}", Signature, ReturnStringFromGradientMode([Cursor].SecondaryColorGradientMode)))
            tx.Add(String.Format("*Cursor_{0}_SecondaryColorNoise= {1}", Signature, [Cursor].SecondaryColorNoise))
            tx.Add(String.Format("*Cursor_{0}_SecondaryColorNoiseOpacity= {1}", Signature, [Cursor].SecondaryColorNoiseOpacity))
            tx.Add(String.Format("*Cursor_{0}_LoadingCircleBack1= {1}", Signature, [Cursor].LoadingCircleBack1.ToArgb))
            tx.Add(String.Format("*Cursor_{0}_LoadingCircleBack2= {1}", Signature, [Cursor].LoadingCircleBack2.ToArgb))
            tx.Add(String.Format("*Cursor_{0}_LoadingCircleBackGradient= {1}", Signature, [Cursor].LoadingCircleBackGradient))
            tx.Add(String.Format("*Cursor_{0}_LoadingCircleBackGradientMode= {1}", Signature, ReturnStringFromGradientMode([Cursor].LoadingCircleBackGradientMode)))
            tx.Add(String.Format("*Cursor_{0}_LoadingCircleBackNoise= {1}", Signature, [Cursor].LoadingCircleBackNoise))
            tx.Add(String.Format("*Cursor_{0}_LoadingCircleBackNoiseOpacity= {1}", Signature, [Cursor].LoadingCircleBackNoiseOpacity))
            tx.Add(String.Format("*Cursor_{0}_LoadingCircleHot1= {1}", Signature, [Cursor].LoadingCircleHot1.ToArgb))
            tx.Add(String.Format("*Cursor_{0}_LoadingCircleHot2= {1}", Signature, [Cursor].LoadingCircleHot2.ToArgb))
            tx.Add(String.Format("*Cursor_{0}_LoadingCircleHotGradient= {1}", Signature, [Cursor].LoadingCircleHotGradient))
            tx.Add(String.Format("*Cursor_{0}_LoadingCircleHotGradientMode= {1}", Signature, ReturnStringFromGradientMode([Cursor].LoadingCircleHotGradientMode)))
            tx.Add(String.Format("*Cursor_{0}_LoadingCircleHotNoise= {1}", Signature, [Cursor].LoadingCircleHotNoise))
            tx.Add(String.Format("*Cursor_{0}_LoadingCircleHotNoiseOpacity= {1}", Signature, [Cursor].LoadingCircleHotNoiseOpacity))
            tx.Add(String.Format("</{0}>", Signature) & vbCrLf)
        End Sub

        Shared Operator =(First As Cursor_Structure, Second As Cursor_Structure) As Boolean
            Return First.Equals(Second)
        End Operator

        Shared Operator <>(First As Cursor_Structure, Second As Cursor_Structure) As Boolean
            Return Not First.Equals(Second)
        End Operator

        Public Function Clone() Implements ICloneable.Clone
            Return MemberwiseClone()
        End Function

    End Structure

#End Region

#Region "Properties"
    Public Info As New Info_Structure With {
            .AppVersion = My.Application.Info.Version.ToString,
            .PaletteName = "Current Mode",
            .PaletteDescription = "",
            .PaletteVersion = "1.0.0.0",
            .Author = Environment.UserName,
            .AuthorSocialMediaLink = ""
    }

    Public Windows11 As New Windows10x_Structure With {
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

    Public Windows10 As New Windows10x_Structure With {
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

    Public LogonUI10x As New LogonUI10x_Structure With {
        .DisableAcrylicBackgroundOnLogon = False, .DisableLogonBackgroundImage = False, .NoLockScreen = False}

    Public Windows7 As New Windows7_DWM_Structure With {
            .ColorizationColor = Color.FromArgb(116, 184, 252),
            .ColorizationAfterglow = Color.FromArgb(116, 184, 252),
            .ColorizationColorBalance = 8,
            .ColorizationAfterglowBalance = 43,
            .ColorizationBlurBalance = 49,
            .ColorizationGlassReflectionIntensity = 0,
            .EnableAeroPeek = True,
            .AlwaysHibernateThumbnails = False,
            .Theme = CP.AeroTheme.Aero}

    Public Windows8 As New Windows8_Metro_Structure With {
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

    Public LogonUI7 As New LogonUI7_Structure With {
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

    Public Win32 As New Win32UI_Structure With {
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

    Public WinMetrics_Fonts As New WinMetrics_Fonts_Structure With {
                .Enabled = False,
                .BorderWidth = 1,
                .CaptionHeight = 22,
                .CaptionWidth = 22,
                .IconSpacing = 75,
                .IconVerticalSpacing = 75,
                .MenuHeight = 19,
                .MenuWidth = 19,
                .MinAnimate = True,
                .PaddedBorderWidth = 4,
                .ScrollHeight = 19,
                .ScrollWidth = 19,
                .SmCaptionHeight = 22,
                .SmCaptionWidth = 22,
                .DesktopIconSize = 32,
                .ShellIconSize = 32,
                .CaptionFont = New Font("Segoe UI", 9, FontStyle.Regular),
                .IconFont = New Font("Segoe UI", 9, FontStyle.Regular),
                .MenuFont = New Font("Segoe UI", 9, FontStyle.Regular),
                .MessageFont = New Font("Segoe UI", 9, FontStyle.Regular),
                .SmCaptionFont = New Font("Segoe UI", 9, FontStyle.Regular),
                .StatusFont = New Font("Segoe UI", 9, FontStyle.Regular)}

    Public CommandPrompt As New Console_Structure With {
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

    Public PowerShellx86 As New Console_Structure With {
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

    Public PowerShellx64 As New Console_Structure With {
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

    Public Cursors_Enabled As Boolean = False

    Public Cursor_Arrow As New Cursor_Structure With {
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

    Public Cursor_AppLoading As New Cursor_Structure With {
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

    Public Cursor_Busy As New Cursor_Structure With {
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

    Public Cursor_Help As New Cursor_Structure With {
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

    Public Cursor_Move As New Cursor_Structure With {
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

    Public Cursor_NS As New Cursor_Structure With {
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

    Public Cursor_EW As New Cursor_Structure With {
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

    Public Cursor_NESW As New Cursor_Structure With {
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

    Public Cursor_NWSE As New Cursor_Structure With {
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

    Public Cursor_Up As New Cursor_Structure With {
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

    Public Cursor_Pen As New Cursor_Structure With {
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

    Public Cursor_None As New Cursor_Structure With {
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

    Public Cursor_Link As New Cursor_Structure With {
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

    Public Cursor_Pin As New Cursor_Structure With {
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

    Public Cursor_Person As New Cursor_Structure With {
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

    Public Cursor_IBeam As New Cursor_Structure With {
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

    Public Cursor_Cross As New Cursor_Structure With {
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

        If Value IsNot Nothing Then

            Select Case RegType
                Case RegistryValueKind.String
                    regTemplate = "Windows Registry Editor Version 5.00{3}{3}[{0}]{3}""{1}""=""{2}"""
                    _Value = Value.ToString

                Case RegistryValueKind.DWord
                    regTemplate = "Windows Registry Editor Version 5.00{3}{3}[{0}]{3}""{1}""=dword:{2}"
                    _Value = CInt(Value).To8Digits

                Case RegistryValueKind.Binary
                    regTemplate = "Windows Registry Editor Version 5.00{3}{3}[{0}]{3}""{1}""=hex:{2}"
                    _Value = BitConverter.ToString(Value).Replace("-", ",")

                Case Else
                    regTemplate = "Windows Registry Editor Version 5.00{3}{3}[{0}]{3}""{1}""=""{2}"""
                    _Value = Value.ToString

            End Select

        Else
            regTemplate = "Windows Registry Editor Version 5.00{3}{3}[{0}]{3}"
            _Value = ""
        End If

        Dim regFileContent As String = String.Format(regTemplate, RegistryKeyPath, ValueName, _Value, vbCrLf)

        If Not IO.Directory.Exists(My.Application.appData) Then IO.Directory.CreateDirectory(My.Application.appData)
        Dim tempreg As String = My.Application.appData & "\tempreg.reg"

        Try
            If IO.File.Exists(tempreg) Then Kill(tempreg)
        Catch
        End Try

        IO.File.WriteAllText(tempreg, regFileContent)

        Dim process As Process = Nothing

        Dim processStartInfo As New ProcessStartInfo With {
           .FileName = "reg",
           .Verb = "runas",
           .Arguments = String.Format("import ""{0}""", tempreg),
           .WindowStyle = ProcessWindowStyle.Hidden,
           .CreateNoWindow = True,
           .UseShellExecute = True
        }

        process = Process.Start(processStartInfo)
        process.WaitForExit()

        Try
            If IO.File.Exists(tempreg) Then Kill(tempreg)
        Catch
        End Try

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

            Dim tx As New List(Of String)
            tx = IO.File.ReadAllText(Filename).CList

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

        AllThemes = [String].CList
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

        For Each field In GetType(Windows10x_Structure).GetFields(BindingFlags.Instance Or BindingFlags.NonPublic Or BindingFlags.Public)
            If field.FieldType.Name.ToLower = "color" Then
                CL.Add(field.GetValue(Windows11))
                CL.Add(field.GetValue(Windows10))
            End If
        Next

        For Each field In GetType(LogonUI10x_Structure).GetFields(BindingFlags.Instance Or BindingFlags.NonPublic Or BindingFlags.Public)
            If field.FieldType.Name.ToLower = "color" Then
                CL.Add(field.GetValue(LogonUI10x))
            End If
        Next

        For Each field In GetType(Windows8_Metro_Structure).GetFields(BindingFlags.Instance Or BindingFlags.NonPublic Or BindingFlags.Public)
            If field.FieldType.Name.ToLower = "color" Then
                CL.Add(field.GetValue(Windows8))
            End If
        Next

        For Each field In GetType(Windows7_DWM_Structure).GetFields(BindingFlags.Instance Or BindingFlags.NonPublic Or BindingFlags.Public)
            If field.FieldType.Name.ToLower = "color" Then
                CL.Add(field.GetValue(Windows7))
            End If
        Next

        For Each field In GetType(LogonUI7_Structure).GetFields(BindingFlags.Instance Or BindingFlags.NonPublic Or BindingFlags.Public)
            If field.FieldType.Name.ToLower = "color" Then
                CL.Add(field.GetValue(LogonUI7))
            End If
        Next

        For Each field In GetType(Win32UI_Structure).GetFields(BindingFlags.Instance Or BindingFlags.NonPublic Or BindingFlags.Public)
            If field.FieldType.Name.ToLower = "color" Then
                CL.Add(field.GetValue(Win32))
            End If
        Next

        For Each field In GetType(Console_Structure).GetFields(BindingFlags.Instance Or BindingFlags.NonPublic Or BindingFlags.Public)
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

        For Each field In GetType(Cursor_Structure).GetFields(BindingFlags.Instance Or BindingFlags.NonPublic Or BindingFlags.Public)
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

        If [TreeView].InvokeRequired Then

            Try
                [TreeView].Invoke(CType(Sub()
                                            With [TreeView].Nodes.Add([Text])
                                                .ImageKey = [ImageKey] : .SelectedImageKey = [ImageKey]
                                            End With
                                            [TreeView].SelectedNode = [TreeView].Nodes([TreeView].Nodes.Count - 1)
                                            [TreeView].Invalidate()
                                        End Sub, MethodInvoker))
            Catch
            End Try

        Else

            Try
                'With [TreeView].Nodes.Add([Text])
                '.ImageKey = [ImageKey] : .SelectedImageKey = [ImageKey]
                'End With
                '[TreeView].SelectedNode = [TreeView].Nodes([TreeView].Nodes.Count - 1)
                '[TreeView].Invalidate()
                '[TreeView].Parent.Refresh()

                [TreeView].Invoke(CType(Sub()
                                            With [TreeView].Nodes.Add([Text])
                                                .ImageKey = [ImageKey] : .SelectedImageKey = [ImageKey]
                                            End With
                                            [TreeView].SelectedNode = [TreeView].Nodes([TreeView].Nodes.Count - 1)
                                            [TreeView].Invalidate()
                                        End Sub, MethodInvoker))

            Catch

            End Try

        End If
    End Sub
    Private Sub AddException([Label] As String, [Exception] As Exception)
        My.Saving_Exceptions.Add(New Tuple(Of String, Exception)([Label], [Exception]))
    End Sub
#End Region

#Region "EXPERIMENTAL  -  JSON"
    Function GetMembersToJSON([StructureType] As Type, [Structure] As Object) As JObject
        Dim j As New JObject()
        j.RemoveAll()

        For Each field In [StructureType].GetFields(BindingFlags.Instance Or BindingFlags.NonPublic Or BindingFlags.Public)

            'MsgBox(field.FieldType.FullName)

            j.Add(field.Name, field.GetValue([Structure]).ToString)
        Next

        Return j
    End Function

    Sub Save_Mechanism2()
        Dim JSON_Overall As New JObject()
        JSON_Overall.RemoveAll()

        JSON_Overall.Add("Information", GetMembersToJSON(GetType(Info_Structure), Info))
        JSON_Overall.Add("Windows 10x", GetMembersToJSON(GetType(Windows10x_Structure), Windows11))
        JSON_Overall.Add("LogonUI Windows 10x", GetMembersToJSON(GetType(LogonUI10x_Structure), LogonUI10x))
        JSON_Overall.Add("Windows 8", GetMembersToJSON(GetType(Windows8_Metro_Structure), Windows8))
        JSON_Overall.Add("Windows 7", GetMembersToJSON(GetType(Windows7_DWM_Structure), Windows7))
        JSON_Overall.Add("LogonUI Windows 7", GetMembersToJSON(GetType(LogonUI7_Structure), LogonUI7))
        JSON_Overall.Add("Win32UI", GetMembersToJSON(GetType(Win32UI_Structure), Win32))
        JSON_Overall.Add("Metrics & Fonts", GetMembersToJSON(GetType(WinMetrics_Fonts_Structure), WinMetrics_Fonts))

        'IO.File.WriteAllText("wpth.json", JSON_Overall.ToString)
    End Sub
#End Region

#Region "CP Handling (Loading/Applying)"
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

#Region "Personal Info"
                Info.Author = Environment.UserName
                Info.AppVersion = My.Application.Info.Version.ToString
                Info.PaletteVersion = "1.0"
                Info.PaletteName = My.Lang.CurrentMode
#End Region

#Region "Windows 11/10"
                If Not My.W7 And Not My.W8 Then
                    Dim Colors As New List(Of Color)
                    Colors.Clear()

                    Dim Def As CP = If(My.W11, New CP_Defaults().Default_Windows11, New CP_Defaults().Default_Windows10)

                    Dim x As Byte()
                    Dim y As Object

#Region "Windows 11"
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

                        With Windows11
                            .Color_Index0 = Colors(0)
                            .Color_Index1 = Colors(1)
                            .Color_Index2 = Colors(2)
                            .Color_Index3 = Colors(3)
                            .Color_Index4 = Colors(4)
                            .Color_Index5 = Colors(5)
                            .Color_Index6 = Colors(6)
                            .Color_Index7 = Colors(7)
                        End With

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

                        With Windows11
                            .Color_Index0 = Colors(0)
                            .Color_Index1 = Colors(1)
                            .Color_Index2 = Colors(2)
                            .Color_Index3 = Colors(3)
                            .Color_Index4 = Colors(4)
                            .Color_Index5 = Colors(5)
                            .Color_Index6 = Colors(6)
                            .Color_Index7 = Colors(7)
                        End With

                    End Try

                    Try
                        y = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "StartColorMenu", Def.Windows11.StartMenu_Accent.Reverse.ToArgb)
                        Windows11.StartMenu_Accent = Color.FromArgb(y).Reverse
                    Catch
                        Windows11.StartMenu_Accent = Def.Windows11.StartMenu_Accent
                    End Try

                    Try
                        y = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "AccentColorMenu", Def.Windows11.Titlebar_Active.Reverse.ToArgb)
                        Windows11.Titlebar_Active = Color.FromArgb(y).Reverse
                    Catch
                        Windows11.Titlebar_Active = Def.Windows11.Titlebar_Active
                    End Try

                    Try
                        y = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\DWM", "AccentColor", Def.Windows11.Titlebar_Active.Reverse.ToArgb)
                        Windows11.Titlebar_Active = Color.FromArgb(y).Reverse
                    Catch
                        Windows11.Titlebar_Active = Def.Windows11.Titlebar_Active
                    End Try

                    Try
                        y = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\DWM", "AccentColorInactive", Def.Windows11.Titlebar_Inactive.Reverse.ToArgb)
                        Windows11.Titlebar_Inactive = Color.FromArgb(y).Reverse
                    Catch
                        Windows11.Titlebar_Inactive = Def.Windows11.Titlebar_Inactive
                    End Try

                    Try
                        Windows11.WinMode_Light = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "SystemUsesLightTheme", Def.Windows11.WinMode_Light)
                    Catch
                        Windows11.WinMode_Light = Def.Windows11.WinMode_Light
                    End Try

                    Try
                        Windows11.AppMode_Light = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "AppsUseLightTheme", Def.Windows11.AppMode_Light)
                    Catch
                        Windows11.AppMode_Light = Def.Windows11.AppMode_Light
                    End Try

                    Try
                        Windows11.Transparency = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "EnableTransparency", Def.Windows11.Transparency)
                    Catch
                        Windows11.Transparency = Def.Windows11.Transparency
                    End Try

                    Try
                        Select Case My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "ColorPrevalence", 0)
                            Case 0
                                Windows11.ApplyAccentonTaskbar = ApplyAccentonTaskbar_Level.None
                            Case 1
                                Windows11.ApplyAccentonTaskbar = ApplyAccentonTaskbar_Level.Taskbar_Start_AC
                            Case 2
                                Windows11.ApplyAccentonTaskbar = ApplyAccentonTaskbar_Level.Taskbar
                            Case Else
                                Windows11.ApplyAccentonTaskbar = ApplyAccentonTaskbar_Level.None
                        End Select

                    Catch
                        Windows11.ApplyAccentonTaskbar = Def.Windows11.ApplyAccentonTaskbar
                    End Try

                    Try
                        Windows11.ApplyAccentonTitlebars = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\DWM", "ColorPrevalence", Def.Windows11.ApplyAccentonTitlebars)
                    Catch
                        Windows11.ApplyAccentonTitlebars = Def.Windows11.ApplyAccentonTitlebars
                    End Try
#End Region

#Region "Windows 10"
                    Try
                        x = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "AccentPalette", If(My.W11, New CP_Defaults().Default_Windows10Accents_Bytes, New CP_Defaults().Default_Windows10Accents_Bytes))
                        Colors.Add(Color.FromArgb(255, x(0), x(1), x(2)))
                        Colors.Add(Color.FromArgb(255, x(4), x(5), x(6)))
                        Colors.Add(Color.FromArgb(255, x(8), x(9), x(10)))
                        Colors.Add(Color.FromArgb(255, x(12), x(13), x(14)))
                        Colors.Add(Color.FromArgb(255, x(16), x(17), x(18)))
                        Colors.Add(Color.FromArgb(255, x(20), x(21), x(22)))
                        Colors.Add(Color.FromArgb(255, x(24), x(25), x(26)))
                        Colors.Add(Color.FromArgb(255, x(28), x(29), x(30)))

                        With Windows10
                            .Color_Index0 = Colors(0)
                            .Color_Index1 = Colors(1)
                            .Color_Index2 = Colors(2)
                            .Color_Index3 = Colors(3)
                            .Color_Index4 = Colors(4)
                            .Color_Index5 = Colors(5)
                            .Color_Index6 = Colors(6)
                            .Color_Index7 = Colors(7)
                        End With

                    Catch
                        x = If(My.W11, New CP_Defaults().Default_Windows10Accents_Bytes, New CP_Defaults().Default_Windows10Accents_Bytes)

                        Colors.Add(Color.FromArgb(255, x(0), x(1), x(2)))
                        Colors.Add(Color.FromArgb(255, x(4), x(5), x(6)))
                        Colors.Add(Color.FromArgb(255, x(8), x(9), x(10)))
                        Colors.Add(Color.FromArgb(255, x(12), x(13), x(14)))
                        Colors.Add(Color.FromArgb(255, x(16), x(17), x(18)))
                        Colors.Add(Color.FromArgb(255, x(20), x(21), x(22)))
                        Colors.Add(Color.FromArgb(255, x(24), x(25), x(26)))
                        Colors.Add(Color.FromArgb(255, x(28), x(29), x(30)))

                        With Windows10
                            .Color_Index0 = Colors(0)
                            .Color_Index1 = Colors(1)
                            .Color_Index2 = Colors(2)
                            .Color_Index3 = Colors(3)
                            .Color_Index4 = Colors(4)
                            .Color_Index5 = Colors(5)
                            .Color_Index6 = Colors(6)
                            .Color_Index7 = Colors(7)
                        End With

                    End Try

                    Try
                        y = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "StartColorMenu", Def.Windows10.StartMenu_Accent.Reverse.ToArgb)
                        Windows10.StartMenu_Accent = Color.FromArgb(y).Reverse
                    Catch
                        Windows10.StartMenu_Accent = Def.Windows10.StartMenu_Accent
                    End Try

                    Try
                        y = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "AccentColorMenu", Def.Windows10.Titlebar_Active.Reverse.ToArgb)
                        Windows10.Titlebar_Active = Color.FromArgb(y).Reverse
                    Catch
                        Windows10.Titlebar_Active = Def.Windows10.Titlebar_Active
                    End Try

                    Try
                        y = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\DWM", "AccentColor", Def.Windows10.Titlebar_Active.Reverse.ToArgb)
                        Windows10.Titlebar_Active = Color.FromArgb(y).Reverse
                    Catch
                        Windows10.Titlebar_Active = Def.Windows10.Titlebar_Active
                    End Try

                    Try
                        y = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\DWM", "AccentColorInactive", Def.Windows10.Titlebar_Inactive.Reverse.ToArgb)
                        Windows10.Titlebar_Inactive = Color.FromArgb(y).Reverse
                    Catch
                        Windows10.Titlebar_Inactive = Def.Windows10.Titlebar_Inactive
                    End Try

                    Try
                        Windows10.WinMode_Light = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "SystemUsesLightTheme", Def.Windows10.WinMode_Light)
                    Catch
                        Windows10.WinMode_Light = Def.Windows10.WinMode_Light
                    End Try

                    Try
                        Windows10.AppMode_Light = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "AppsUseLightTheme", Def.Windows10.AppMode_Light)
                    Catch
                        Windows10.AppMode_Light = Def.Windows10.AppMode_Light
                    End Try

                    Try
                        Windows10.Transparency = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "EnableTransparency", Def.Windows10.Transparency)
                    Catch
                        Windows10.Transparency = Def.Windows10.Transparency
                    End Try

                    Try
                        Select Case My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "ColorPrevalence", 0)
                            Case 0
                                Windows10.ApplyAccentonTaskbar = ApplyAccentonTaskbar_Level.None
                            Case 1
                                Windows10.ApplyAccentonTaskbar = ApplyAccentonTaskbar_Level.Taskbar_Start_AC
                            Case 2
                                Windows10.ApplyAccentonTaskbar = ApplyAccentonTaskbar_Level.Taskbar
                            Case Else
                                Windows10.ApplyAccentonTaskbar = ApplyAccentonTaskbar_Level.None
                        End Select

                    Catch
                        Windows10.ApplyAccentonTaskbar = Def.Windows10.ApplyAccentonTaskbar
                    End Try

                    Try
                        Windows10.ApplyAccentonTitlebars = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\DWM", "ColorPrevalence", Def.Windows10.ApplyAccentonTitlebars)
                    Catch
                        Windows10.ApplyAccentonTitlebars = Def.Windows10.ApplyAccentonTitlebars
                    End Try
#End Region

                Else
                    Windows11.Color_Index0 = _Def.Windows11.Color_Index0
                    Windows11.Color_Index1 = _Def.Windows11.Color_Index1
                    Windows11.Color_Index2 = _Def.Windows11.Color_Index2
                    Windows11.Color_Index3 = _Def.Windows11.Color_Index3
                    Windows11.Color_Index4 = _Def.Windows11.Color_Index4
                    Windows11.Color_Index5 = _Def.Windows11.Color_Index5
                    Windows11.Color_Index6 = _Def.Windows11.Color_Index6
                    Windows11.StartMenu_Accent = _Def.Windows11.StartMenu_Accent
                    Windows11.Titlebar_Active = _Def.Windows11.Titlebar_Active
                    Windows11.Titlebar_Inactive = _Def.Windows11.Titlebar_Inactive
                    Windows11.WinMode_Light = _Def.Windows11.WinMode_Light
                    Windows11.AppMode_Light = _Def.Windows11.AppMode_Light
                    Windows11.Transparency = _Def.Windows11.Transparency
                    Windows11.ApplyAccentonTaskbar = _Def.Windows11.ApplyAccentonTaskbar
                    Windows11.ApplyAccentonTitlebars = _Def.Windows11.ApplyAccentonTitlebars

                    Windows10.Color_Index0 = _Def.Windows10.Color_Index0
                    Windows10.Color_Index1 = _Def.Windows10.Color_Index1
                    Windows10.Color_Index2 = _Def.Windows10.Color_Index2
                    Windows10.Color_Index3 = _Def.Windows10.Color_Index3
                    Windows10.Color_Index4 = _Def.Windows10.Color_Index4
                    Windows10.Color_Index5 = _Def.Windows10.Color_Index5
                    Windows10.Color_Index6 = _Def.Windows10.Color_Index6
                    Windows10.StartMenu_Accent = _Def.Windows10.StartMenu_Accent
                    Windows10.Titlebar_Active = _Def.Windows10.Titlebar_Active
                    Windows10.Titlebar_Inactive = _Def.Windows10.Titlebar_Inactive
                    Windows10.WinMode_Light = _Def.Windows10.WinMode_Light
                    Windows10.AppMode_Light = _Def.Windows10.AppMode_Light
                    Windows10.Transparency = _Def.Windows10.Transparency
                    Windows10.ApplyAccentonTaskbar = _Def.Windows10.ApplyAccentonTaskbar
                    Windows10.ApplyAccentonTitlebars = _Def.Windows10.ApplyAccentonTitlebars
                End If
#End Region

#Region "Windows 7"
                If My.W7 Or My.W8 Then
                    Dim Def As CP = If(My.W7, New CP_Defaults().Default_Windows7, New CP_Defaults().Default_Windows8)
                    Dim y As Object

                    Try
                        y = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationColor", Def.Windows7.ColorizationColor.ToArgb)
                        Windows7.ColorizationColor = Color.FromArgb(255, Color.FromArgb(y))
                    Catch
                        Windows7.ColorizationColor = Def.Windows7.ColorizationColor
                    End Try

                    Try
                        y = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationColorBalance", Def.Windows7.ColorizationColorBalance)
                        Windows7.ColorizationColorBalance = y
                    Catch
                        Windows7.ColorizationColorBalance = Def.Windows7.ColorizationColorBalance
                    End Try

                    If Not My.W8 Then
                        Try
                            y = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationAfterglow", Def.Windows7.ColorizationAfterglow.ToArgb)
                            Windows7.ColorizationAfterglow = Color.FromArgb(255, Color.FromArgb(y))
                        Catch
                            Windows7.ColorizationAfterglow = Def.Windows7.ColorizationAfterglow
                        End Try

                        Try
                            y = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationAfterglowBalance", Def.Windows7.ColorizationAfterglowBalance)
                            Windows7.ColorizationAfterglowBalance = y
                        Catch
                            Windows7.ColorizationAfterglowBalance = Def.Windows7.ColorizationAfterglowBalance
                        End Try

                        Try
                            y = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationBlurBalance", Def.Windows7.ColorizationBlurBalance)
                            Windows7.ColorizationBlurBalance = y
                        Catch
                            Windows7.ColorizationBlurBalance = Def.Windows7.ColorizationBlurBalance
                        End Try

                        Try
                            y = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationGlassReflectionIntensity", Def.Windows7.ColorizationGlassReflectionIntensity)
                            Windows7.ColorizationGlassReflectionIntensity = y
                        Catch
                            Windows7.ColorizationGlassReflectionIntensity = Def.Windows7.ColorizationGlassReflectionIntensity
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
                            Windows7.Theme = AeroTheme.Classic
                        ElseIf Com Then
                            If Not Opaque Then Windows7.Theme = AeroTheme.Aero Else Windows7.Theme = AeroTheme.AeroOpaque
                        Else
                            Windows7.Theme = AeroTheme.Basic
                        End If

                    End If

                    Try
                        Windows7.EnableAeroPeek = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "EnableAeroPeek", Def.Windows7.EnableAeroPeek)
                    Catch
                        Windows7.EnableAeroPeek = Def.Windows7.EnableAeroPeek
                    End Try

                    Try
                        Windows7.AlwaysHibernateThumbnails = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "AlwaysHibernateThumbnails", Def.Windows7.AlwaysHibernateThumbnails)
                    Catch
                        Windows7.AlwaysHibernateThumbnails = Def.Windows7.AlwaysHibernateThumbnails
                    End Try

                Else
                    Windows7.ColorizationColor = _Def.Windows7.ColorizationColor
                    Windows7.ColorizationColorBalance = _Def.Windows7.ColorizationColorBalance
                    Windows7.ColorizationAfterglow = _Def.Windows7.ColorizationAfterglow
                    Windows7.ColorizationAfterglowBalance = _Def.Windows7.ColorizationAfterglowBalance
                    Windows7.ColorizationBlurBalance = _Def.Windows7.ColorizationBlurBalance
                    Windows7.ColorizationGlassReflectionIntensity = _Def.Windows7.ColorizationGlassReflectionIntensity
                    Windows7.Theme = _Def.Windows7.Theme
                    Windows7.EnableAeroPeek = _Def.Windows7.EnableAeroPeek
                    Windows7.AlwaysHibernateThumbnails = _Def.Windows7.AlwaysHibernateThumbnails
                End If

#End Region

#Region "Windows 8.1"
                If My.W8 Then
                    Dim Def As CP = New CP_Defaults().Default_Windows8
                    Dim y As Object

                    Dim stringThemeName As System.Text.StringBuilder = New System.Text.StringBuilder(260)
                    NativeMethods.Uxtheme.GetCurrentThemeName(stringThemeName, 260, Nothing, 0, Nothing, 0)

                    If stringThemeName.ToString.Split("\").Last.ToLower = "aerolite.msstyles" Or String.IsNullOrWhiteSpace(stringThemeName.ToString) Then
                        Windows8.Theme = AeroTheme.AeroLite
                    Else
                        Windows8.Theme = AeroTheme.Aero
                    End If

                    Try
                        y = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationColor", Def.Windows8.ColorizationColor.ToArgb)
                        Windows8.ColorizationColor = Color.FromArgb(255, Color.FromArgb(y))
                    Catch
                        Windows8.ColorizationColor = Def.Windows8.ColorizationColor
                    End Try

                    Try
                        y = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationColorBalance", Def.Windows8.ColorizationColorBalance)
                        Windows8.ColorizationColorBalance = y
                    Catch
                        Windows8.ColorizationColorBalance = Def.Windows8.ColorizationColorBalance
                    End Try

                    Try
                        y = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "StartColor", Color.FromArgb(84, 0, 30).ToArgb)
                        Windows8.StartColor = Color.FromArgb(255, Color.FromArgb(y)).Reverse
                    Catch
                        Windows8.StartColor = Color.FromArgb(84, 0, 30)
                    End Try

                    Try
                        y = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "AccentColor", Color.FromArgb(178, 29, 72).ToArgb)
                        Windows8.AccentColor = Color.FromArgb(255, Color.FromArgb(y)).Reverse
                    Catch
                        Windows8.AccentColor = Color.FromArgb(178, 29, 72)
                    End Try

                    Dim S As String

                    Try
                        S = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Personalization", "PersonalColors_Background", "#1e0054")
                        Windows8.PersonalColors_Background = S.FromHEXToColor
                    Catch
                        Windows8.PersonalColors_Background = Def.Windows8.PersonalColors_Background
                    End Try

                    Try
                        S = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Personalization", "PersonalColors_Accent", "#481db2")
                        Windows8.PersonalColors_Accent = S.FromHEXToColor
                    Catch
                        Windows8.PersonalColors_Accent = Def.Windows8.PersonalColors_Accent
                    End Try

                    Try
                        Windows8.Start = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Personalization", "ForceStartBackground", 0)
                    Catch
                        Windows8.Start = 0
                    End Try

                    Try
                        Windows8.LogonUI = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "DefaultColorSet", 0)
                    Catch
                        Windows8.LogonUI = 0
                    End Try

                Else
                    Windows8.Theme = _Def.Windows8.Theme
                    Windows8.StartColor = _Def.Windows8.StartColor
                    Windows8.AccentColor = _Def.Windows8.AccentColor
                    Windows8.PersonalColors_Background = _Def.Windows8.PersonalColors_Background
                    Windows8.PersonalColors_Accent = _Def.Windows8.PersonalColors_Accent
                    Windows8.Start = _Def.Windows8.Start
                    Windows8.LogonUI = _Def.Windows8.LogonUI
                End If
#End Region

#Region "LogonUI"
                If Not My.W7 And Not My.W8 Then
                    Dim Def As CP = If(My.W11, New CP_Defaults().Default_Windows11, New CP_Defaults().Default_Windows10)

                    Try
                        LogonUI10x.DisableAcrylicBackgroundOnLogon = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\System", "DisableAcrylicBackgroundOnLogon", Def.LogonUI10x.DisableAcrylicBackgroundOnLogon)
                    Catch
                        LogonUI10x.DisableAcrylicBackgroundOnLogon = Def.LogonUI10x.DisableAcrylicBackgroundOnLogon
                    End Try

                    Try
                        LogonUI10x.DisableLogonBackgroundImage = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\System", "DisableLogonBackgroundImage", Def.LogonUI10x.DisableLogonBackgroundImage)
                    Catch
                        LogonUI10x.DisableLogonBackgroundImage = Def.LogonUI10x.DisableLogonBackgroundImage
                    End Try

                    Try
                        LogonUI10x.NoLockScreen = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Windows\Personalization", "NoLockScreen", Def.LogonUI10x.NoLockScreen)
                    Catch
                        LogonUI10x.NoLockScreen = Def.LogonUI10x.NoLockScreen
                    End Try
                Else
                    LogonUI10x.DisableAcrylicBackgroundOnLogon = _Def.LogonUI10x.DisableAcrylicBackgroundOnLogon
                    LogonUI10x.DisableLogonBackgroundImage = _Def.LogonUI10x.DisableLogonBackgroundImage
                    LogonUI10x.NoLockScreen = _Def.LogonUI10x.NoLockScreen
                End If

#End Region

#Region "LogonUI 7"
                If My.W7 Then
                    Dim b1 As Boolean = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\Software\Microsoft\Windows\CurrentVersion\Authentication\LogonUI\Background", "OEMBackground", False)
                    Dim b2 As Boolean = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Windows\System", "UseOEMBackground", False)
                    LogonUI7.Enabled = b1 Or b2

                    Dim rLog As RegistryKey = Registry.CurrentUser.CreateSubKey("Software\WinPaletter\LogonUI")

                    Try
                        LogonUI7.Mode = rLog.GetValue("Mode", LogonUI_Modes.Default_)
                    Catch
                        LogonUI7.Mode = LogonUI_Modes.Default_
                    End Try

                    Try
                        LogonUI7.ImagePath = rLog.GetValue("ImagePath", "")
                    Catch
                        LogonUI7.ImagePath = ""
                    End Try

                    Try
                        LogonUI7.Color = Color.FromArgb(rLog.GetValue("Color", Color.Black.ToArgb))
                    Catch
                        LogonUI7.Color = Color.Black
                    End Try

                    Try
                        LogonUI7.Blur = rLog.GetValue("Blur", False)
                    Catch
                        LogonUI7.Blur = False
                    End Try

                    Try
                        LogonUI7.Blur_Intensity = rLog.GetValue("Blur_Intensity", 0)
                    Catch
                        LogonUI7.Blur_Intensity = 0
                    End Try

                    Try
                        LogonUI7.Grayscale = rLog.GetValue("Grayscale", False)
                    Catch
                        LogonUI7.Grayscale = False
                    End Try

                    Try
                        LogonUI7.Noise = rLog.GetValue("Noise", False)
                    Catch
                        LogonUI7.Noise = False
                    End Try

                    Try
                        LogonUI7.Noise_Mode = rLog.GetValue("Noise_Mode", NoiseMode.Acrylic)
                    Catch
                        LogonUI7.Noise_Mode = NoiseMode.Acrylic
                    End Try

                    Try
                        LogonUI7.Noise_Intensity = rLog.GetValue("Noise_Intensity", 0)
                    Catch
                        LogonUI7.Noise_Intensity = 0
                    End Try

                    rLog.Close()
                Else
                    LogonUI7.Enabled = _Def.LogonUI7.Enabled
                    LogonUI7.Mode = _Def.LogonUI7.Mode
                    LogonUI7.ImagePath = _Def.LogonUI7.ImagePath
                    LogonUI7.Color = _Def.LogonUI7.Color
                    LogonUI7.Blur = _Def.LogonUI7.Blur
                    LogonUI7.Blur_Intensity = _Def.LogonUI7.Blur_Intensity
                    LogonUI7.Grayscale = _Def.LogonUI7.Grayscale
                    LogonUI7.Noise = _Def.LogonUI7.Noise
                    LogonUI7.Noise_Mode = _Def.LogonUI7.Noise_Mode
                    LogonUI7.Noise_Intensity = _Def.LogonUI7.Noise_Intensity
                End If
#End Region

#Region "LogonUI 8"
                If My.W8 Then
                    Windows8.NoLockScreen = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\Policies\Microsoft\Windows\Personalization", "NoLockScreen", False)

                    Dim rLog As RegistryKey = Registry.CurrentUser.CreateSubKey("Software\WinPaletter\LogonUI")

                    Try
                        Windows8.LockScreenType = rLog.GetValue("Mode", LogonUI_Modes.Default_)
                    Catch
                        Windows8.LockScreenType = LogonUI_Modes.Default_
                    End Try

                    Try
                        Windows8.LockScreenSystemID = rLog.GetValue("Metro_LockScreenSystemID", 0)
                    Catch
                        Windows8.LockScreenSystemID = 0
                    End Try

                    Try
                        LogonUI7.ImagePath = rLog.GetValue("ImagePath", "")
                    Catch
                        LogonUI7.ImagePath = ""
                    End Try

                    Try
                        LogonUI7.Color = Color.FromArgb(rLog.GetValue("Color", Color.Black.ToArgb))
                    Catch
                        LogonUI7.Color = Color.Black
                    End Try

                    Try
                        LogonUI7.Blur = rLog.GetValue("Blur", False)
                    Catch
                        LogonUI7.Blur = False
                    End Try

                    Try
                        LogonUI7.Blur_Intensity = rLog.GetValue("Blur_Intensity", 0)
                    Catch
                        LogonUI7.Blur_Intensity = 0
                    End Try

                    Try
                        LogonUI7.Grayscale = rLog.GetValue("Grayscale", False)
                    Catch
                        LogonUI7.Grayscale = False
                    End Try

                    Try
                        LogonUI7.Noise = rLog.GetValue("Noise", False)
                    Catch
                        LogonUI7.Noise = False
                    End Try

                    Try
                        LogonUI7.Noise_Mode = rLog.GetValue("Noise_Mode", NoiseMode.Acrylic)
                    Catch
                        LogonUI7.Noise_Mode = NoiseMode.Acrylic
                    End Try

                    Try
                        LogonUI7.Noise_Intensity = rLog.GetValue("Noise_Intensity", 0)
                    Catch
                        LogonUI7.Noise_Intensity = 0
                    End Try

                    rLog.Close()

                ElseIf Not My.W7 Then
                    Windows8.NoLockScreen = _Def.Windows8.NoLockScreen
                    Windows8.LockScreenType = _Def.Windows8.LockScreenType
                    Windows8.LockScreenSystemID = _Def.Windows8.LockScreenSystemID
                    LogonUI7.ImagePath = _Def.LogonUI7.ImagePath
                    LogonUI7.Color = _Def.LogonUI7.Color
                    LogonUI7.Blur = _Def.LogonUI7.Blur
                    LogonUI7.Blur_Intensity = _Def.LogonUI7.Blur_Intensity
                    LogonUI7.Grayscale = _Def.LogonUI7.Grayscale
                    LogonUI7.Noise = _Def.LogonUI7.Noise
                    LogonUI7.Noise_Mode = _Def.LogonUI7.Noise_Mode
                    LogonUI7.Noise_Intensity = _Def.LogonUI7.Noise_Intensity
                End If
#End Region

#Region "Win32UI"
                Try
                    Win32.EnableGradient = GetUserPreferencesMask(17)
                Catch
                    Win32.EnableTheming = True
                End Try

                Try
                    Win32.EnableGradient = GetUserPreferencesMask(4)
                Catch
                    Win32.EnableGradient = True
                End Try

                With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "ActiveTitle", "153 180 209")
                    If .ToString.Split(" ").Count = 3 Then Win32.ActiveTitle = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                End With

                With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "AppWorkspace", "171 171 171")
                    If .ToString.Split(" ").Count = 3 Then Win32.AppWorkspace = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                End With

                With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "Background", "0 0 0")
                    If .ToString.Split(" ").Count = 3 Then Win32.Background = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                End With

                With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "ButtonAlternateFace", "0 0 0")
                    If .ToString.Split(" ").Count = 3 Then Win32.ButtonAlternateFace = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                End With

                With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "ButtonDkShadow", "105 105 105")
                    If .ToString.Split(" ").Count = 3 Then Win32.ButtonDkShadow = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                End With

                With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "ButtonFace", "240 240 240")
                    If .ToString.Split(" ").Count = 3 Then Win32.ButtonFace = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                End With

                With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "ButtonHilight", "255 255 255")
                    If .ToString.Split(" ").Count = 3 Then Win32.ButtonHilight = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                End With

                With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "ButtonLight", "227 227 227")
                    If .ToString.Split(" ").Count = 3 Then Win32.ButtonLight = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                End With

                With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "ButtonShadow", "160 160 160")
                    If .ToString.Split(" ").Count = 3 Then Win32.ButtonShadow = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                End With

                With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "ButtonText", "0 0 0")
                    If .ToString.Split(" ").Count = 3 Then Win32.ButtonText = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                End With

                With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "GradientActiveTitle", "185 209 234")
                    If .ToString.Split(" ").Count = 3 Then Win32.GradientActiveTitle = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                End With

                With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "GradientInactiveTitle", "215 228 242")
                    If .ToString.Split(" ").Count = 3 Then Win32.GradientInactiveTitle = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                End With

                With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "GrayText", "109 109 109")
                    If .ToString.Split(" ").Count = 3 Then Win32.GrayText = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                End With

                With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "HilightText", "255 255 255")
                    If .ToString.Split(" ").Count = 3 Then Win32.HilightText = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                End With

                With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "HotTrackingColor", "0 102 204")
                    If .ToString.Split(" ").Count = 3 Then Win32.HotTrackingColor = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                End With

                With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "ActiveBorder", "244 247 252")
                    If .ToString.Split(" ").Count = 3 Then Win32.ActiveBorder = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                End With

                With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "InactiveBorder", "244 247 252")
                    If .ToString.Split(" ").Count = 3 Then Win32.InactiveBorder = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                End With

                With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "InactiveTitle", "191 205 219")
                    If .ToString.Split(" ").Count = 3 Then Win32.InactiveTitle = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                End With

                With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "InactiveTitleText", "0 0 0")
                    If .ToString.Split(" ").Count = 3 Then Win32.InactiveTitleText = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                End With

                With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "InfoText", "0 0 0")
                    If .ToString.Split(" ").Count = 3 Then Win32.InfoText = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                End With

                With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "InfoWindow", "255 255 225")
                    If .ToString.Split(" ").Count = 3 Then Win32.InfoWindow = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                End With

                With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "Menu", "240 240 240")
                    If .ToString.Split(" ").Count = 3 Then Win32.Menu = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                End With

                With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "MenuBar", "240 240 240")
                    If .ToString.Split(" ").Count = 3 Then Win32.MenuBar = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                End With

                With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "MenuText", "0 0 0")
                    If .ToString.Split(" ").Count = 3 Then Win32.MenuText = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                End With

                With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "Scrollbar", "200 200 200")
                    If .ToString.Split(" ").Count = 3 Then Win32.Scrollbar = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                End With

                With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "TitleText", "0 0 0")
                    If .ToString.Split(" ").Count = 3 Then Win32.TitleText = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                End With

                With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "Window", "255 255 255")
                    If .ToString.Split(" ").Count = 3 Then Win32.Window = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                End With

                With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "WindowFrame", "100 100 100")
                    If .ToString.Split(" ").Count = 3 Then Win32.WindowFrame = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                End With

                With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "WindowText", "0 0 0")
                    If .ToString.Split(" ").Count = 3 Then Win32.WindowText = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                End With

                With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "Hilight", "0 120 215")
                    If .ToString.Split(" ").Count = 3 Then Win32.Hilight = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                End With

                With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "MenuHilight", "0 120 215")
                    If .ToString.Split(" ").Count = 3 Then Win32.MenuHilight = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                End With

                With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "Desktop", "0 0 0")
                    If .ToString.Split(" ").Count = 3 Then Win32.Desktop = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                End With
#End Region

#Region "Metrics & Fonts"
                Dim rMain_M As RegistryKey = Registry.CurrentUser.CreateSubKey("Software\WinPaletter\Metrics")
                WinMetrics_Fonts.Enabled = rMain_M.GetValue("", False)
                rMain_M.Close()

                WinMetrics_Fonts.BorderWidth = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "BorderWidth", -15) / -15
                WinMetrics_Fonts.CaptionHeight = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "CaptionHeight", -330) / -15
                WinMetrics_Fonts.CaptionWidth = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "CaptionWidth", -330) / -15
                WinMetrics_Fonts.IconSpacing = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "IconSpacing", -1125) / -15
                WinMetrics_Fonts.IconVerticalSpacing = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "IconVerticalSpacing", -1125) / -15
                WinMetrics_Fonts.MenuHeight = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "MenuHeight", -285) / -15
                WinMetrics_Fonts.MenuWidth = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "MenuWidth", -285) / -15
                WinMetrics_Fonts.MinAnimate = CInt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "MinAnimate", 1)).ToBoolean
                WinMetrics_Fonts.PaddedBorderWidth = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "PaddedBorderWidth", -60) / -15
                WinMetrics_Fonts.ScrollHeight = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "ScrollHeight", -255) / -15
                WinMetrics_Fonts.ScrollWidth = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "ScrollWidth", -255) / -15
                WinMetrics_Fonts.SmCaptionHeight = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "SmCaptionHeight", -330) / -15
                WinMetrics_Fonts.SmCaptionWidth = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "SmCaptionWidth", -330) / -15
                WinMetrics_Fonts.ShellIconSize = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "Shell Icon Size", 32)
                WinMetrics_Fonts.DesktopIconSize = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\Shell\Bags\1\Desktop", "IconSize", 32)

                WinMetrics_Fonts.CaptionFont = Font.FromLogFont(LogFontHelper.ByteToLogFont(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "CaptionFont", Nothing)))
                WinMetrics_Fonts.IconFont = Font.FromLogFont(LogFontHelper.ByteToLogFont(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "IconFont", Nothing)))
                WinMetrics_Fonts.MenuFont = Font.FromLogFont(LogFontHelper.ByteToLogFont(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "MenuFont", Nothing)))
                WinMetrics_Fonts.MessageFont = Font.FromLogFont(LogFontHelper.ByteToLogFont(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "MessageFont", Nothing)))
                WinMetrics_Fonts.SmCaptionFont = Font.FromLogFont(LogFontHelper.ByteToLogFont(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "SmCaptionFont", Nothing)))
                WinMetrics_Fonts.StatusFont = Font.FromLogFont(LogFontHelper.ByteToLogFont(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "StatusFont", Nothing)))
#End Region

#Region "Terminals"

                CommandPrompt = Console_Structure.Load_Console_From_Registry("", _Def.CommandPrompt)

                Dim regPath_PS86, AppPath_PS86 As String
                Dim regPath_PS64, AppPath_PS64 As String

                regPath_PS86 = "%SystemRoot%_System32_WindowsPowerShell_v1.0_powershell.exe"
                AppPath_PS86 = Environment.GetEnvironmentVariable("WINDIR") & "\System32\WindowsPowerShell\v1.0"

                regPath_PS64 = "%SystemRoot%_SysWOW64_WindowsPowerShell_v1.0_powershell.exe"
                AppPath_PS64 = Environment.GetEnvironmentVariable("WINDIR") & "\SysWOW64\WindowsPowerShell\v1.0"

                If IO.Directory.Exists(AppPath_PS86) Then
                    Try : Registry.CurrentUser.CreateSubKey("Console\" & regPath_PS86, True).Close() : Catch : End Try
                    PowerShellx86 = Console_Structure.Load_Console_From_Registry(regPath_PS86, _Def.PowerShellx86)
                End If

                If IO.Directory.Exists(AppPath_PS64) Then
                    Try : Registry.CurrentUser.CreateSubKey("Console\" & regPath_PS64, True).Close() : Catch : End Try
                    PowerShellx64 = Console_Structure.Load_Console_From_Registry(regPath_PS64, _Def.PowerShellx64)
                End If

#Region "Locking - Loading it must be after loading preferences first"
                Dim rLogX As RegistryKey = Registry.CurrentUser.CreateSubKey("Software\WinPaletter\Terminals")

                Try
                    CommandPrompt.Enabled = CInt(rLogX.GetValue("Terminal_CMD_Enabled", 0)).ToBoolean
                Catch
                    CommandPrompt.Enabled = False
                End Try

                Try
                    PowerShellx86.Enabled = CInt(rLogX.GetValue("Terminal_PS_32_Enabled", 0)).ToBoolean
                Catch
                    PowerShellx86.Enabled = False
                End Try

                Try
                    PowerShellx64.Enabled = CInt(rLogX.GetValue("Terminal_PS_64_Enabled", 0)).ToBoolean
                Catch
                    PowerShellx64.Enabled = False
                End Try

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
#End Region


#Region "Windows Terminal"
                If My.W10 Or My.W11 Then
                    Dim TerDir As String
                    Dim TerPreDir As String

                    If Not My.[Settings].Terminal_Path_Deflection Then
                        TerDir = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) & "\AppData\Local\Packages\Microsoft.WindowsTerminal_8wekyb3d8bbwe\LocalState\settings.json"
                        TerPreDir = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) & "\AppData\Local\Packages\Microsoft.WindowsTerminalPreview_8wekyb3d8bbwe\LocalState\settings.json"
                    Else
                        If IO.File.Exists(My.[Settings].Terminal_Stable_Path) Then
                            TerDir = My.[Settings].Terminal_Stable_Path
                        Else
                            TerDir = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) & "\AppData\Local\Packages\Microsoft.WindowsTerminal_8wekyb3d8bbwe\LocalState\settings.json"
                        End If

                        If IO.File.Exists(My.[Settings].Terminal_Preview_Path) Then
                            TerPreDir = My.[Settings].Terminal_Preview_Path
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
                Cursors_Enabled = rMain.GetValue("", False)
                rMain.Close()

                Cursor_Arrow = Cursor_Structure.Load_Cursor_From_Registry("Arrow")
                Cursor_Help = Cursor_Structure.Load_Cursor_From_Registry("Help")
                Cursor_AppLoading = Cursor_Structure.Load_Cursor_From_Registry("AppLoading")
                Cursor_Busy = Cursor_Structure.Load_Cursor_From_Registry("Busy")
                Cursor_Move = Cursor_Structure.Load_Cursor_From_Registry("Move")
                Cursor_NS = Cursor_Structure.Load_Cursor_From_Registry("NS")
                Cursor_EW = Cursor_Structure.Load_Cursor_From_Registry("EW")
                Cursor_NESW = Cursor_Structure.Load_Cursor_From_Registry("NESW")
                Cursor_NWSE = Cursor_Structure.Load_Cursor_From_Registry("NWSE")
                Cursor_Up = Cursor_Structure.Load_Cursor_From_Registry("Up")
                Cursor_Pen = Cursor_Structure.Load_Cursor_From_Registry("Pen")
                Cursor_None = Cursor_Structure.Load_Cursor_From_Registry("None")
                Cursor_Link = Cursor_Structure.Load_Cursor_From_Registry("Link")
                Cursor_Pin = Cursor_Structure.Load_Cursor_From_Registry("Pin")
                Cursor_Person = Cursor_Structure.Load_Cursor_From_Registry("Person")
                Cursor_IBeam = Cursor_Structure.Load_Cursor_From_Registry("IBeam")
                Cursor_Cross = Cursor_Structure.Load_Cursor_From_Registry("Cross")

#End Region
#End Region

                _Def.Dispose()

            Case Mode.File
#Region "File"
                Dim txt As New List(Of String)
                txt.Clear()
                txt = IO.File.ReadAllText(PaletteFile).CList

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

                '## Checks if the loaded file is an old WPTH or not
                Dim OldWPTH As Boolean = False
                For Each lin As String In txt
                    If lin.ToLower.StartsWith("*Created from App Version= ".ToLower) Then
                        Info.AppVersion = lin.Remove(0, "*Created from App Version= ".Count)
                        OldWPTH = (Info.AppVersion < "1.0.6.9")
                        Exit For
                    End If
                Next

                For Each lin As String In txt
#Region "Personal Info"
                    If lin.ToLower.StartsWith("*Created from App Version= ".ToLower) Then Info.AppVersion = lin.Remove(0, "*Created from App Version= ".Count)
                    If lin.ToLower.StartsWith("*Palette Name= ".ToLower) Then Info.PaletteName = lin.Remove(0, "*Palette Name= ".Count)
                    If lin.ToLower.StartsWith("*Palette Description= ".ToLower) Then Info.PaletteDescription = lin.Remove(0, "*Palette Description= ".Count).Replace("<br>", vbCrLf)
                    If lin.ToLower.StartsWith("*Palette File Version= ".ToLower) Then Info.PaletteVersion = lin.Remove(0, "*Palette File Version= ".Count)
                    If lin.ToLower.StartsWith("*Author= ".ToLower) Then Info.Author = lin.Remove(0, "*Author= ".Count)
                    If lin.ToLower.StartsWith("*AuthorSocialMediaLink= ".ToLower) Then Info.AuthorSocialMediaLink = lin.Remove(0, "*AuthorSocialMediaLink= ".Count)
                    If lin.ToLower.StartsWith("*Palette File Version= ".ToLower) Then Info.PaletteVersion = lin.Remove(0, "*Palette File Version= ".Count)
#End Region

#Region "Windows 11"
                    If lin.ToLower.StartsWith("*Win_11_Color_Index0= ".ToLower) Then Windows11.Color_Index0 = Color.FromArgb(lin.Remove(0, "*Win_11_Color_Index0= ".Count))
                    If lin.ToLower.StartsWith("*Win_11_Color_Index1= ".ToLower) Then Windows11.Color_Index1 = Color.FromArgb(lin.Remove(0, "*Win_11_Color_Index1= ".Count))
                    If lin.ToLower.StartsWith("*Win_11_Color_Index2= ".ToLower) Then Windows11.Color_Index2 = Color.FromArgb(lin.Remove(0, "*Win_11_Color_Index2= ".Count))
                    If lin.ToLower.StartsWith("*Win_11_Color_Index3= ".ToLower) Then Windows11.Color_Index3 = Color.FromArgb(lin.Remove(0, "*Win_11_Color_Index3= ".Count))
                    If lin.ToLower.StartsWith("*Win_11_Color_Index4= ".ToLower) Then Windows11.Color_Index4 = Color.FromArgb(lin.Remove(0, "*Win_11_Color_Index4= ".Count))
                    If lin.ToLower.StartsWith("*Win_11_Color_Index5= ".ToLower) Then Windows11.Color_Index5 = Color.FromArgb(lin.Remove(0, "*Win_11_Color_Index5= ".Count))
                    If lin.ToLower.StartsWith("*Win_11_Color_Index6= ".ToLower) Then Windows11.Color_Index6 = Color.FromArgb(lin.Remove(0, "*Win_11_Color_Index6= ".Count))
                    If lin.ToLower.StartsWith("*Win_11_Color_Index7= ".ToLower) Then Windows11.Color_Index7 = Color.FromArgb(lin.Remove(0, "*Win_11_Color_Index7= ".Count))
                    If lin.ToLower.StartsWith("*Win_11_WinMode_Light= ".ToLower) Then Windows11.WinMode_Light = lin.Remove(0, "*Win_11_WinMode_Light= ".Count)
                    If lin.ToLower.StartsWith("*Win_11_AppMode_Light= ".ToLower) Then Windows11.AppMode_Light = lin.Remove(0, "*Win_11_AppMode_Light= ".Count)
                    If lin.ToLower.StartsWith("*Win_11_Transparency= ".ToLower) Then Windows11.Transparency = lin.Remove(0, "*Win_11_Transparency= ".Count)
                    If lin.ToLower.StartsWith("*Win_11_Titlebar_Active= ".ToLower) Then Windows11.Titlebar_Active = Color.FromArgb(lin.Remove(0, "*Win_11_Titlebar_Active= ".Count))
                    If lin.ToLower.StartsWith("*Win_11_Titlebar_Inactive= ".ToLower) Then Windows11.Titlebar_Inactive = Color.FromArgb(lin.Remove(0, "*Win_11_Titlebar_Inactive= ".Count))
                    If lin.ToLower.StartsWith("*Win_11_StartMenu_Accent= ".ToLower) Then Windows11.StartMenu_Accent = Color.FromArgb(lin.Remove(0, "*Win_11_StartMenu_Accent= ".Count))
                    If lin.ToLower.StartsWith("*Win_11_ApplyAccentonTitlebars= ".ToLower) Then Windows11.ApplyAccentonTitlebars = lin.Remove(0, "*Win_11_ApplyAccentonTitlebars= ".Count)

                    If lin.ToLower.StartsWith("*Win_11_AccentOnStartTBAC= ".ToLower) Then
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
                    If lin.ToLower.StartsWith("*Win_10_Color_Index0= ".ToLower) Then Windows10.Color_Index0 = Color.FromArgb(lin.Remove(0, "*Win_10_Color_Index0= ".Count))
                    If lin.ToLower.StartsWith("*Win_10_Color_Index1= ".ToLower) Then Windows10.Color_Index1 = Color.FromArgb(lin.Remove(0, "*Win_10_Color_Index1= ".Count))
                    If lin.ToLower.StartsWith("*Win_10_Color_Index2= ".ToLower) Then Windows10.Color_Index2 = Color.FromArgb(lin.Remove(0, "*Win_10_Color_Index2= ".Count))
                    If lin.ToLower.StartsWith("*Win_10_Color_Index3= ".ToLower) Then Windows10.Color_Index3 = Color.FromArgb(lin.Remove(0, "*Win_10_Color_Index3= ".Count))
                    If lin.ToLower.StartsWith("*Win_10_Color_Index4= ".ToLower) Then Windows10.Color_Index4 = Color.FromArgb(lin.Remove(0, "*Win_10_Color_Index4= ".Count))
                    If lin.ToLower.StartsWith("*Win_10_Color_Index5= ".ToLower) Then Windows10.Color_Index5 = Color.FromArgb(lin.Remove(0, "*Win_10_Color_Index5= ".Count))
                    If lin.ToLower.StartsWith("*Win_10_Color_Index6= ".ToLower) Then Windows10.Color_Index6 = Color.FromArgb(lin.Remove(0, "*Win_10_Color_Index6= ".Count))
                    If lin.ToLower.StartsWith("*Win_10_Color_Index7= ".ToLower) Then Windows10.Color_Index7 = Color.FromArgb(lin.Remove(0, "*Win_10_Color_Index7= ".Count))
                    If lin.ToLower.StartsWith("*Win_10_WinMode_Light= ".ToLower) Then Windows10.WinMode_Light = lin.Remove(0, "*Win_10_WinMode_Light= ".Count)
                    If lin.ToLower.StartsWith("*Win_10_AppMode_Light= ".ToLower) Then Windows10.AppMode_Light = lin.Remove(0, "*Win_10_AppMode_Light= ".Count)
                    If lin.ToLower.StartsWith("*Win_10_Transparency= ".ToLower) Then Windows10.Transparency = lin.Remove(0, "*Win_10_Transparency= ".Count)
                    If lin.ToLower.StartsWith("*Win_10_Titlebar_Active= ".ToLower) Then Windows10.Titlebar_Active = Color.FromArgb(lin.Remove(0, "*Win_10_Titlebar_Active= ".Count))
                    If lin.ToLower.StartsWith("*Win_10_Titlebar_Inactive= ".ToLower) Then Windows10.Titlebar_Inactive = Color.FromArgb(lin.Remove(0, "*Win_10_Titlebar_Inactive= ".Count))
                    If lin.ToLower.StartsWith("*Win_10_StartMenu_Accent= ".ToLower) Then Windows10.StartMenu_Accent = Color.FromArgb(lin.Remove(0, "*Win_10_StartMenu_Accent= ".Count))
                    If lin.ToLower.StartsWith("*Win_10_ApplyAccentonTitlebars= ".ToLower) Then Windows10.ApplyAccentonTitlebars = lin.Remove(0, "*Win_10_ApplyAccentonTitlebars= ".Count)

                    If lin.ToLower.StartsWith("*Win_10_AccentOnStartTBAC= ".ToLower) Then
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
                            If lin.ToLower.StartsWith("*WinMode_Light= ".ToLower) Then
                                Windows11.WinMode_Light = lin.Remove(0, "*WinMode_Light= ".Count)
                                Windows10.WinMode_Light = Windows11.WinMode_Light
                            End If

                            If lin.ToLower.StartsWith("*AppMode_Light= ".ToLower) Then
                                Windows11.AppMode_Light = lin.Remove(0, "*AppMode_Light= ".Count)
                                Windows10.AppMode_Light = Windows11.AppMode_Light
                            End If


                            If lin.ToLower.StartsWith("*Transparency= ".ToLower) Then
                                Windows11.Transparency = lin.Remove(0, "*Transparency= ".Count)
                                Windows10.Transparency = Windows11.Transparency
                            End If

                            If lin.ToLower.StartsWith("*AccentColorOnTitlebarAndBorders= ".ToLower) Then
                                Windows11.ApplyAccentonTitlebars = lin.Remove(0, "*AccentColorOnTitlebarAndBorders= ".Count)
                                Windows10.ApplyAccentonTitlebars = Windows11.ApplyAccentonTitlebars
                            End If

                            If lin.ToLower.StartsWith("*Titlebar_Active= ".ToLower) Then
                                Windows11.Titlebar_Active = Color.FromArgb(lin.Remove(0, "*Titlebar_Active= ".Count))
                                Windows10.Titlebar_Active = Windows11.Titlebar_Active
                            End If

                            If lin.ToLower.StartsWith("*Titlebar_Inactive= ".ToLower) Then
                                Windows11.Titlebar_Inactive = Color.FromArgb(lin.Remove(0, "*Titlebar_Inactive= ".Count))
                                Windows10.Titlebar_Inactive = Windows11.Titlebar_Inactive
                            End If

                            If lin.ToLower.StartsWith("*ActionCenter_AppsLinks= ".ToLower) Then
                                Windows11.Color_Index0 = Color.FromArgb(lin.Remove(0, "*ActionCenter_AppsLinks= ".Count))
                                Windows10.Color_Index0 = Windows11.Color_Index0
                            End If

                            If lin.ToLower.StartsWith("*Taskbar_Icon_Underline= ".ToLower) Then
                                Windows11.Color_Index1 = Color.FromArgb(lin.Remove(0, "*Taskbar_Icon_Underline= ".Count))
                                Windows10.Color_Index1 = Windows11.Color_Index1
                            End If

                            If lin.ToLower.StartsWith("*StartButton_Hover= ".ToLower) Then
                                Windows11.Color_Index2 = Color.FromArgb(lin.Remove(0, "*StartButton_Hover= ".Count))
                                Windows10.Color_Index2 = Windows11.Color_Index2
                            End If

                            If lin.ToLower.StartsWith("*SettingsIconsAndLinks= ".ToLower) Then
                                Windows11.Color_Index3 = Color.FromArgb(lin.Remove(0, "*SettingsIconsAndLinks= ".Count))
                                Windows10.Color_Index3 = Windows11.Color_Index3
                            End If

                            If lin.ToLower.StartsWith("*StartMenuBackground_ActiveTaskbarButton= ".ToLower) Then
                                Windows11.Color_Index4 = Color.FromArgb(lin.Remove(0, "*StartMenuBackground_ActiveTaskbarButton= ".Count))
                                Windows10.Color_Index4 = Windows11.Color_Index4
                            End If

                            If lin.ToLower.StartsWith("*StartListFolders_TaskbarFront= ".ToLower) Then
                                Windows11.Color_Index5 = Color.FromArgb(lin.Remove(0, "*StartListFolders_TaskbarFront= ".Count))
                                Windows10.Color_Index5 = Windows11.Color_Index5
                            End If

                            If lin.ToLower.StartsWith("*Taskbar_Background= ".ToLower) Then
                                Windows11.Color_Index6 = Color.FromArgb(lin.Remove(0, "*Taskbar_Background= ".Count))
                                Windows10.Color_Index6 = Windows11.Color_Index6
                            End If

                            If lin.ToLower.StartsWith("*StartMenu_Accent= ".ToLower) Then
                                Windows11.StartMenu_Accent = Color.FromArgb(lin.Remove(0, "*StartMenu_Accent= ".Count))
                                Windows10.StartMenu_Accent = Windows11.StartMenu_Accent
                            End If

                            If lin.ToLower.StartsWith("*Undefined= ".ToLower) Then
                                Windows11.Color_Index7 = Color.FromArgb(lin.Remove(0, "*Undefined= ".Count))
                                Windows10.Color_Index7 = Windows11.Color_Index7
                            End If

                            If lin.ToLower.StartsWith("*AccentColorOnStartTaskbarAndActionCenter= ".ToLower) Then
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
                            MsgBox(My.Lang.WPTH_OldGen_LoadError, My.MsgboxRt(MsgBoxStyle.Critical))
                        End Try
                    End If

#End Region

#Region "Aero"
                    If lin.ToLower.StartsWith("*Aero_ColorizationColor= ".ToLower) Then Windows7.ColorizationColor = Color.FromArgb(lin.Remove(0, "*Aero_ColorizationColor= ".Count))
                    If lin.ToLower.StartsWith("*Aero_ColorizationAfterglow= ".ToLower) Then Windows7.ColorizationAfterglow = Color.FromArgb(lin.Remove(0, "*Aero_ColorizationAfterglow= ".Count))
                    If lin.ToLower.StartsWith("*Aero_ColorizationColorBalance= ".ToLower) Then Windows7.ColorizationColorBalance = lin.Remove(0, "*Aero_ColorizationColorBalance= ".Count)
                    If lin.ToLower.StartsWith("*Aero_ColorizationAfterglowBalance= ".ToLower) Then Windows7.ColorizationAfterglowBalance = lin.Remove(0, "*Aero_ColorizationAfterglowBalance= ".Count)
                    If lin.ToLower.StartsWith("*Aero_ColorizationBlurBalance= ".ToLower) Then Windows7.ColorizationBlurBalance = lin.Remove(0, "*Aero_ColorizationBlurBalance= ".Count)
                    If lin.ToLower.StartsWith("*Aero_ColorizationGlassReflectionIntensity= ".ToLower) Then Windows7.ColorizationGlassReflectionIntensity = lin.Remove(0, "*Aero_ColorizationGlassReflectionIntensity= ".Count)
                    If lin.ToLower.StartsWith("*Aero_EnableAeroPeek= ".ToLower) Then Windows7.EnableAeroPeek = lin.Remove(0, "*Aero_EnableAeroPeek= ".Count)
                    If lin.ToLower.StartsWith("*Aero_AlwaysHibernateThumbnails= ".ToLower) Then Windows7.AlwaysHibernateThumbnails = lin.Remove(0, "*Aero_AlwaysHibernateThumbnails= ".Count)
                    If lin.ToLower.StartsWith("*Aero_Theme= ".ToLower) Then Windows7.Theme = lin.Remove(0, "*Aero_Theme= ".Count)
#End Region

#Region "Metro"
                    If lin.ToLower.StartsWith("*Metro_ColorizationColor= ".ToLower) Then Windows8.ColorizationColor = Color.FromArgb(lin.Remove(0, "*Metro_ColorizationColor= ".Count))
                    If lin.ToLower.StartsWith("*Metro_ColorizationColorBalance= ".ToLower) Then Windows8.ColorizationColorBalance = lin.Remove(0, "*Metro_ColorizationColorBalance= ".Count)
                    If lin.ToLower.StartsWith("*Metro_PersonalColors_Background= ".ToLower) Then Windows8.PersonalColors_Background = Color.FromArgb(lin.Remove(0, "*Metro_PersonalColors_Background= ".Count))
                    If lin.ToLower.StartsWith("*Metro_PersonalColors_Accent= ".ToLower) Then Windows8.PersonalColors_Accent = Color.FromArgb(lin.Remove(0, "*Metro_PersonalColors_Accent= ".Count))
                    If lin.ToLower.StartsWith("*Metro_StartColor= ".ToLower) Then Windows8.StartColor = Color.FromArgb(lin.Remove(0, "*Metro_StartColor= ".Count))
                    If lin.ToLower.StartsWith("*Metro_AccentColor= ".ToLower) Then Windows8.AccentColor = Color.FromArgb(lin.Remove(0, "*Metro_AccentColor= ".Count))
                    If lin.ToLower.StartsWith("*Metro_Start= ".ToLower) Then Windows8.Start = lin.Remove(0, "*Metro_Start= ".Count)
                    If lin.ToLower.StartsWith("*Metro_Theme= ".ToLower) Then Windows8.Theme = lin.Remove(0, "*Metro_Theme= ".Count)
                    If lin.ToLower.StartsWith("*Metro_LogonUI= ".ToLower) Then Windows8.LogonUI = lin.Remove(0, "*Metro_LogonUI= ".Count)
                    If lin.ToLower.StartsWith("*Metro_NoLockScreen= ".ToLower) Then Windows8.NoLockScreen = lin.Remove(0, "*Metro_NoLockScreen= ".Count)
                    If lin.ToLower.StartsWith("*Metro_LockScreenType= ".ToLower) Then Windows8.LockScreenType = lin.Remove(0, "*Metro_LockScreenType= ".Count)
                    If lin.ToLower.StartsWith("*Metro_LockScreenSystemID= ".ToLower) Then Windows8.LockScreenSystemID = lin.Remove(0, "*Metro_LockScreenSystemID= ".Count)
#End Region

#Region "LogonUI"
                    If lin.ToLower.StartsWith("*LogonUI_DisableAcrylicBackgroundOnLogon= ".ToLower) Then LogonUI10x.DisableAcrylicBackgroundOnLogon = lin.Remove(0, "*LogonUI_DisableAcrylicBackgroundOnLogon= ".Count)
                    If lin.ToLower.StartsWith("*LogonUI_DisableLogonBackgroundImage= ".ToLower) Then LogonUI10x.DisableLogonBackgroundImage = lin.Remove(0, "*LogonUI_DisableLogonBackgroundImage= ".Count)
                    If lin.ToLower.StartsWith("*LogonUI_NoLockScreen= ".ToLower) Then LogonUI10x.NoLockScreen = lin.Remove(0, "*LogonUI_NoLockScreen= ".Count)
#End Region

#Region "LogonUI_7_8"
                    If lin.ToLower.StartsWith("*LogonUI7_Color= ".ToLower) Then LogonUI7.Color = Color.FromArgb(lin.Remove(0, "*LogonUI7_Color= ".Count))
                    If lin.ToLower.StartsWith("*LogonUI7_Enabled= ".ToLower) Then LogonUI7.Enabled = lin.Remove(0, "*LogonUI7_Enabled= ".Count)
                    If lin.ToLower.StartsWith("*LogonUI7_Mode= ".ToLower) Then LogonUI7.Mode = lin.Remove(0, "*LogonUI7_Mode= ".Count)
                    If lin.ToLower.StartsWith("*LogonUI7_ImagePath= ".ToLower) Then LogonUI7.ImagePath = lin.Remove(0, "*LogonUI7_ImagePath= ".Count)
                    If lin.ToLower.StartsWith("*LogonUI7_Blur= ".ToLower) Then LogonUI7.Blur = lin.Remove(0, "*LogonUI7_Blur= ".Count)
                    If lin.ToLower.StartsWith("*LogonUI7_Blur_Intensity= ".ToLower) Then LogonUI7.Blur_Intensity = lin.Remove(0, "*LogonUI7_Blur_Intensity= ".Count)
                    If lin.ToLower.StartsWith("*LogonUI7_Grayscale= ".ToLower) Then LogonUI7.Grayscale = lin.Remove(0, "*LogonUI7_Grayscale= ".Count)
                    If lin.ToLower.StartsWith("*LogonUI7_Noise= ".ToLower) Then LogonUI7.Noise = lin.Remove(0, "*LogonUI7_Noise= ".Count)
                    If lin.ToLower.StartsWith("*LogonUI7_Noise_Mode= ".ToLower) Then LogonUI7.Noise_Mode = lin.Remove(0, "*LogonUI7_Noise_Mode= ".Count)
                    If lin.ToLower.StartsWith("*LogonUI7_Noise_Intensity= ".ToLower) Then LogonUI7.Noise_Intensity = lin.Remove(0, "*LogonUI7_Noise_Intensity= ".Count)
#End Region

#Region "Win32UI"
                    If lin.ToLower.StartsWith("*Win32UI_EnableTheming= ".ToLower) Then Win32.EnableTheming = lin.Remove(0, "*Win32UI_EnableTheming= ".Count)
                    If lin.ToLower.StartsWith("*Win32UI_EnableGradient= ".ToLower) Then Win32.EnableGradient = lin.Remove(0, "*Win32UI_EnableGradient= ".Count)
                    If lin.ToLower.StartsWith("*Win32UI_ActiveBorder= ".ToLower) Then Win32.ActiveBorder = Color.FromArgb(lin.Remove(0, "*Win32UI_ActiveBorder= ".Count))
                    If lin.ToLower.StartsWith("*Win32UI_ActiveTitle= ".ToLower) Then Win32.ActiveTitle = Color.FromArgb(lin.Remove(0, "*Win32UI_ActiveTitle= ".Count))
                    If lin.ToLower.StartsWith("*Win32UI_AppWorkspace= ".ToLower) Then Win32.AppWorkspace = Color.FromArgb(lin.Remove(0, "*Win32UI_AppWorkspace= ".Count))
                    If lin.ToLower.StartsWith("*Win32UI_Background= ".ToLower) Then Win32.Background = Color.FromArgb(lin.Remove(0, "*Win32UI_Background= ".Count))
                    If lin.ToLower.StartsWith("*Win32UI_ButtonAlternateFace= ".ToLower) Then Win32.ButtonAlternateFace = Color.FromArgb(lin.Remove(0, "*Win32UI_ButtonAlternateFace= ".Count))
                    If lin.ToLower.StartsWith("*Win32UI_ButtonDkShadow= ".ToLower) Then Win32.ButtonDkShadow = Color.FromArgb(lin.Remove(0, "*Win32UI_ButtonDkShadow= ".Count))
                    If lin.ToLower.StartsWith("*Win32UI_ButtonFace= ".ToLower) Then Win32.ButtonFace = Color.FromArgb(lin.Remove(0, "*Win32UI_ButtonFace= ".Count))
                    If lin.ToLower.StartsWith("*Win32UI_ButtonHilight= ".ToLower) Then Win32.ButtonHilight = Color.FromArgb(lin.Remove(0, "*Win32UI_ButtonHilight= ".Count))
                    If lin.ToLower.StartsWith("*Win32UI_ButtonLight= ".ToLower) Then Win32.ButtonLight = Color.FromArgb(lin.Remove(0, "*Win32UI_ButtonLight= ".Count))
                    If lin.ToLower.StartsWith("*Win32UI_ButtonShadow= ".ToLower) Then Win32.ButtonShadow = Color.FromArgb(lin.Remove(0, "*Win32UI_ButtonShadow= ".Count))
                    If lin.ToLower.StartsWith("*Win32UI_ButtonText= ".ToLower) Then Win32.ButtonText = Color.FromArgb(lin.Remove(0, "*Win32UI_ButtonText= ".Count))
                    If lin.ToLower.StartsWith("*Win32UI_GradientActiveTitle= ".ToLower) Then Win32.GradientActiveTitle = Color.FromArgb(lin.Remove(0, "*Win32UI_GradientActiveTitle= ".Count))
                    If lin.ToLower.StartsWith("*Win32UI_GradientInactiveTitle= ".ToLower) Then Win32.GradientInactiveTitle = Color.FromArgb(lin.Remove(0, "*Win32UI_GradientInactiveTitle= ".Count))
                    If lin.ToLower.StartsWith("*Win32UI_GrayText= ".ToLower) Then Win32.GrayText = Color.FromArgb(lin.Remove(0, "*Win32UI_GrayText= ".Count))
                    If lin.ToLower.StartsWith("*Win32UI_HilightText= ".ToLower) Then Win32.HilightText = Color.FromArgb(lin.Remove(0, "*Win32UI_HilightText= ".Count))
                    If lin.ToLower.StartsWith("*Win32UI_HotTrackingColor= ".ToLower) Then Win32.HotTrackingColor = Color.FromArgb(lin.Remove(0, "*Win32UI_HotTrackingColor= ".Count))
                    If lin.ToLower.StartsWith("*Win32UI_InactiveBorder= ".ToLower) Then Win32.InactiveBorder = Color.FromArgb(lin.Remove(0, "*Win32UI_InactiveBorder= ".Count))
                    If lin.ToLower.StartsWith("*Win32UI_InactiveTitle= ".ToLower) Then Win32.InactiveTitle = Color.FromArgb(lin.Remove(0, "*Win32UI_InactiveTitle= ".Count))
                    If lin.ToLower.StartsWith("*Win32UI_InactiveTitleText= ".ToLower) Then Win32.InactiveTitleText = Color.FromArgb(lin.Remove(0, "*Win32UI_InactiveTitleText= ".Count))
                    If lin.ToLower.StartsWith("*Win32UI_InfoText= ".ToLower) Then Win32.InfoText = Color.FromArgb(lin.Remove(0, "*Win32UI_InfoText= ".Count))
                    If lin.ToLower.StartsWith("*Win32UI_InfoWindow= ".ToLower) Then Win32.InfoWindow = Color.FromArgb(lin.Remove(0, "*Win32UI_InfoWindow= ".Count))
                    If lin.ToLower.StartsWith("*Win32UI_Menu= ".ToLower) Then Win32.Menu = Color.FromArgb(lin.Remove(0, "*Win32UI_Menu= ".Count))
                    If lin.ToLower.StartsWith("*Win32UI_MenuBar= ".ToLower) Then Win32.MenuBar = Color.FromArgb(lin.Remove(0, "*Win32UI_MenuBar= ".Count))
                    If lin.ToLower.StartsWith("*Win32UI_MenuText= ".ToLower) Then Win32.MenuText = Color.FromArgb(lin.Remove(0, "*Win32UI_MenuText= ".Count))
                    If lin.ToLower.StartsWith("*Win32UI_Scrollbar= ".ToLower) Then Win32.Scrollbar = Color.FromArgb(lin.Remove(0, "*Win32UI_Scrollbar= ".Count))
                    If lin.ToLower.StartsWith("*Win32UI_TitleText= ".ToLower) Then Win32.TitleText = Color.FromArgb(lin.Remove(0, "*Win32UI_TitleText= ".Count))
                    If lin.ToLower.StartsWith("*Win32UI_Window= ".ToLower) Then Win32.Window = Color.FromArgb(lin.Remove(0, "*Win32UI_Window= ".Count))
                    If lin.ToLower.StartsWith("*Win32UI_WindowFrame= ".ToLower) Then Win32.WindowFrame = Color.FromArgb(lin.Remove(0, "*Win32UI_WindowFrame= ".Count))
                    If lin.ToLower.StartsWith("*Win32UI_WindowText= ".ToLower) Then Win32.WindowText = Color.FromArgb(lin.Remove(0, "*Win32UI_WindowText= ".Count))
                    If lin.ToLower.StartsWith("*Win32UI_Hilight= ".ToLower) Then Win32.Hilight = Color.FromArgb(lin.Remove(0, "*Win32UI_Hilight= ".Count))
                    If lin.ToLower.StartsWith("*Win32UI_MenuHilight= ".ToLower) Then Win32.MenuHilight = Color.FromArgb(lin.Remove(0, "*Win32UI_MenuHilight= ".Count))
                    If lin.ToLower.StartsWith("*Win32UI_Desktop= ".ToLower) Then Win32.Desktop = Color.FromArgb(lin.Remove(0, "*Win32UI_Desktop= ".Count))
#End Region

#Region "Terminals"

                    If lin.ToLower.StartsWith("*Terminal_CMD_Enabled= ".ToLower) Then CommandPrompt.Enabled = lin.Remove(0, "*Terminal_CMD_Enabled= ".Count)
                    If lin.ToLower.StartsWith("*Terminal_PS_32_Enabled= ".ToLower) Then PowerShellx86.Enabled = lin.Remove(0, "*Terminal_PS_32_Enabled= ".Count)
                    If lin.ToLower.StartsWith("*Terminal_PS_64_Enabled= ".ToLower) Then PowerShellx64.Enabled = lin.Remove(0, "*Terminal_PS_64_Enabled= ".Count)
                    If lin.ToLower.StartsWith("*Terminal_Stable_Enabled= ".ToLower) Then Terminal.Enabled = lin.Remove(0, "*Terminal_Stable_Enabled= ".Count)
                    If lin.ToLower.StartsWith("*Terminal_Preview_Enabled= ".ToLower) Then TerminalPreview.Enabled = lin.Remove(0, "*Terminal_Preview_Enabled= ".Count)

                    If lin.ToLower.StartsWith("*CMD_".ToLower) Then cmdList.Add(lin.Remove(0, "*CMD_".Count))
                    If lin.ToLower.StartsWith("*PS_32_".ToLower) Then PS86List.Add(lin.Remove(0, "*PS_32_".Count))
                    If lin.ToLower.StartsWith("*PS_64_".ToLower) Then PS64List.Add(lin.Remove(0, "*PS_64_".Count))

                    If Not IgnoreWindowsTerminal Then
                        If My.W10 Or My.W11 Then
                            If lin.ToLower.StartsWith("terminal.".ToLower) Then ls_stable.Add(lin)
                            If lin.ToLower.StartsWith("terminalpreview.".ToLower) Then ls_preview.Add(lin)
                        End If
                    End If
#End Region

#Region "Cursors"
                    If lin.ToLower.StartsWith("*Cursor_Enabled= ".ToLower) Then Cursors_Enabled = lin.Remove(0, "*Cursor_Enabled= ".Count)

                    If lin.ToLower.StartsWith("*Cursor_Arrow_".ToLower) Then CUR_Arrow_List.Add(lin.Remove(0, "*Cursor_Arrow_".Count))
                    If lin.ToLower.StartsWith("*Cursor_Help_".ToLower) Then CUR_Help_List.Add(lin.Remove(0, "*Cursor_Help_".Count))
                    If lin.ToLower.StartsWith("*Cursor_AppLoading_".ToLower) Then CUR_AppLoading_List.Add(lin.Remove(0, "*Cursor_AppLoading_".Count))
                    If lin.ToLower.StartsWith("*Cursor_Busy_".ToLower) Then CUR_Busy_List.Add(lin.Remove(0, "*Cursor_Busy_".Count))
                    If lin.ToLower.StartsWith("*Cursor_Move_".ToLower) Then CUR_Move_List.Add(lin.Remove(0, "*Cursor_Move_".Count))
                    If lin.ToLower.StartsWith("*Cursor_NS_".ToLower) Then CUR_NS_List.Add(lin.Remove(0, "*Cursor_NS_".Count))
                    If lin.ToLower.StartsWith("*Cursor_EW_".ToLower) Then CUR_EW_List.Add(lin.Remove(0, "*Cursor_EW_".Count))
                    If lin.ToLower.StartsWith("*Cursor_NESW_".ToLower) Then CUR_NESW_List.Add(lin.Remove(0, "*Cursor_NESW_".Count))
                    If lin.ToLower.StartsWith("*Cursor_NWSE_".ToLower) Then CUR_NWSE_List.Add(lin.Remove(0, "*Cursor_NWSE_".Count))
                    If lin.ToLower.StartsWith("*Cursor_Up_".ToLower) Then CUR_Up_List.Add(lin.Remove(0, "*Cursor_Up_".Count))
                    If lin.ToLower.StartsWith("*Cursor_Pen_".ToLower) Then CUR_Pen_List.Add(lin.Remove(0, "*Cursor_Pen_".Count))
                    If lin.ToLower.StartsWith("*Cursor_None_".ToLower) Then CUR_None_List.Add(lin.Remove(0, "*Cursor_None_".Count))
                    If lin.ToLower.StartsWith("*Cursor_Link_".ToLower) Then CUR_Link_List.Add(lin.Remove(0, "*Cursor_Link_".Count))
                    If lin.ToLower.StartsWith("*Cursor_Pin_".ToLower) Then CUR_Pin_List.Add(lin.Remove(0, "*Cursor_Pin_".Count))
                    If lin.ToLower.StartsWith("*Cursor_Person_".ToLower) Then CUR_Person_List.Add(lin.Remove(0, "*Cursor_Person_".Count))
                    If lin.ToLower.StartsWith("*Cursor_IBeam_".ToLower) Then CUR_IBeam_List.Add(lin.Remove(0, "*Cursor_IBeam_".Count))
                    If lin.ToLower.StartsWith("*Cursor_Cross_".ToLower) Then CUR_Cross_List.Add(lin.Remove(0, "*Cursor_Cross_".Count))

#End Region

                Next

#Region "Cursors"
                Cursor_Arrow = Cursor_Structure.Load_Cursor_From_ListOfString(CUR_Arrow_List)
                Cursor_Help = Cursor_Structure.Load_Cursor_From_ListOfString(CUR_Help_List)
                Cursor_AppLoading = Cursor_Structure.Load_Cursor_From_ListOfString(CUR_AppLoading_List)
                Cursor_Busy = Cursor_Structure.Load_Cursor_From_ListOfString(CUR_Busy_List)
                Cursor_Move = Cursor_Structure.Load_Cursor_From_ListOfString(CUR_Move_List)
                Cursor_NS = Cursor_Structure.Load_Cursor_From_ListOfString(CUR_NS_List)
                Cursor_EW = Cursor_Structure.Load_Cursor_From_ListOfString(CUR_EW_List)
                Cursor_NESW = Cursor_Structure.Load_Cursor_From_ListOfString(CUR_NESW_List)
                Cursor_NWSE = Cursor_Structure.Load_Cursor_From_ListOfString(CUR_NWSE_List)
                Cursor_Up = Cursor_Structure.Load_Cursor_From_ListOfString(CUR_Up_List)
                Cursor_Pen = Cursor_Structure.Load_Cursor_From_ListOfString(CUR_Pen_List)
                Cursor_None = Cursor_Structure.Load_Cursor_From_ListOfString(CUR_None_List)
                Cursor_Link = Cursor_Structure.Load_Cursor_From_ListOfString(CUR_Link_List)
                Cursor_Pin = Cursor_Structure.Load_Cursor_From_ListOfString(CUR_Pin_List)
                Cursor_Person = Cursor_Structure.Load_Cursor_From_ListOfString(CUR_Person_List)
                Cursor_IBeam = Cursor_Structure.Load_Cursor_From_ListOfString(CUR_IBeam_List)
                Cursor_Cross = Cursor_Structure.Load_Cursor_From_ListOfString(CUR_Cross_List)
#End Region

#Region "Windows Terminal"
                CommandPrompt = Console_Structure.Load_Console_From_ListOfString(cmdList)
                PowerShellx86 = Console_Structure.Load_Console_From_ListOfString(PS86List)
                PowerShellx64 = Console_Structure.Load_Console_From_ListOfString(PS64List)

                If Not IgnoreWindowsTerminal Then
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

    Sub Save(ByVal [SaveTo] As Mode, Optional ByVal FileLocation As String = "", Optional ByVal [TreeView] As TreeView = Nothing)

        Select Case [SaveTo]
            Case Mode.Registry

                Dim ReportProgress As Boolean = ([TreeView] IsNot Nothing)
                Dim _ErrorHappened As Boolean = False

                Dim sw, sw_all As New Stopwatch
                sw_all.Reset()
                sw_all.Start()
                sw.Reset()
                sw.Stop()


                If ReportProgress Then
                    My.Saving_Exceptions.Clear()
                    [TreeView].Nodes.Clear()

                    Dim OS As String
                    If My.W11 Then
                        OS = "Windows 11"
                    ElseIf My.W10 Then
                        OS = "Windows 10"
                    ElseIf My.W8 Then
                        OS = "Windows 8.1"
                    ElseIf My.W7 Then
                        OS = "Windows 7"
                    Else
                        OS = "Windows 11 or Higher"
                    End If

                    AddNode([TreeView], String.Format("{0}: WinPaletter will apply theme from {1}'s section", Now.ToLongTimeString, OS), "info")

                    AddNode([TreeView], String.Format("{0}: Applying Started", Now.ToLongTimeString), "info")

                    If Not My.isElevated Then
                        AddNode([TreeView], String.Format("{0}: Writing to registry without administrator rights by deflection", Now.ToLongTimeString), "admin")
                        AddNode([TreeView], String.Format("{0}: This deflection takes time longer than if you start as administrator", Now.ToLongTimeString), "admin")
                    End If

                End If

#Region "Registry"

                If My.W11 Then
                    If ReportProgress Then AddNode([TreeView], String.Format("{0}: Applying Windows 11 Scheme", Now.ToLongTimeString), "info")
                    sw.Reset() : sw.Start()
                    Try
                        Windows11.Apply()
                        If ReportProgress Then AddNode([TreeView], String.Format("It took {0} seconds", sw.ElapsedMilliseconds / 1000), "time")
                    Catch ex As Exception
                        sw.Stop() : sw_all.Stop()
                        _ErrorHappened = True

                        If ReportProgress Then
                            AddNode([TreeView], String.Format("{0}: Error occured while applying Windows 11 Scheme", Now.ToLongTimeString), "error")
                            AddException("Error occured while applying Windows 11 Scheme", ex)
                        Else
                            BugReport.ThrowError(ex)
                        End If

                        sw.Start() : sw_all.Start()
                    End Try
                    sw.Stop()

                    If ReportProgress Then AddNode([TreeView], String.Format("{0}: Applying Windows 11 LogonUI", Now.ToLongTimeString), "info")
                    sw.Reset() : sw.Start()
                    Try
                        LogonUI10x.Apply()
                        If ReportProgress Then AddNode([TreeView], String.Format("It took {0} seconds", sw.ElapsedMilliseconds / 1000), "time")
                    Catch ex As Exception
                        sw.Stop() : sw_all.Stop()
                        _ErrorHappened = True
                        If ReportProgress Then
                            AddNode([TreeView], String.Format("{0}: Error occured while applying Windows 11 LogonUI", Now.ToLongTimeString), "error")
                            AddException("Error occured while applying Windows 11 LogonUI", ex)
                        Else
                            BugReport.ThrowError(ex)
                        End If

                        sw.Start() : sw_all.Start()
                    End Try
                    sw.Stop()
                End If

                If My.W10 Then
                    If ReportProgress Then AddNode([TreeView], String.Format("{0}: Applying Windows 10 Scheme", Now.ToLongTimeString), "info")
                    sw.Reset() : sw.Start()
                    Try
                        Windows10.Apply()
                        If ReportProgress Then AddNode([TreeView], String.Format("It took {0} seconds", sw.ElapsedMilliseconds / 1000), "time")
                    Catch ex As Exception
                        sw.Stop() : sw_all.Stop()
                        _ErrorHappened = True
                        If ReportProgress Then
                            AddNode([TreeView], String.Format("{0}: Error occured while applying Windows 10 Scheme", Now.ToLongTimeString), "error")
                            AddException("Error occured while applying Windows 10 Scheme", ex)
                        Else
                            BugReport.ThrowError(ex)
                        End If

                        sw.Start() : sw_all.Start()
                    End Try
                    sw.Stop()


                    If ReportProgress Then AddNode([TreeView], String.Format("{0}: Applying Windows 10 LogonUI", Now.ToLongTimeString), "info")
                    sw.Reset() : sw.Start()
                    Try
                        LogonUI10x.Apply()
                        If ReportProgress Then AddNode([TreeView], String.Format("It took {0} seconds", sw.ElapsedMilliseconds / 1000), "time")
                    Catch ex As Exception
                        sw.Stop() : sw_all.Stop()
                        _ErrorHappened = True
                        If ReportProgress Then
                            AddNode([TreeView], String.Format("{0}: Error occured while applying Windows 10 LogonUI", Now.ToLongTimeString), "error")
                            AddException("Error occured while applying Windows 10 LogonUI", ex)
                        Else
                            BugReport.ThrowError(ex)
                        End If

                        sw.Start() : sw_all.Start()
                    End Try
                    sw.Stop()
                End If

                If My.W7 Then
                    If ReportProgress Then AddNode([TreeView], String.Format("{0}: Applying Windows 7 Colors", Now.ToLongTimeString), "info")
                    sw.Reset() : sw.Start()
                    RefreshDWM(Me)
                    Try
                        Windows7.Apply()
                        If ReportProgress Then AddNode([TreeView], String.Format("It took {0} seconds", sw.ElapsedMilliseconds / 1000), "time")
                    Catch ex As Exception
                        sw.Stop() : sw_all.Stop()
                        _ErrorHappened = True
                        If ReportProgress Then
                            AddNode([TreeView], String.Format("{0}: Error occured while applying Windows 7 Colors", Now.ToLongTimeString), "error")
                            AddException("Error occured while applying Windows 7 Colors", ex)
                        Else
                            BugReport.ThrowError(ex)
                        End If

                        sw.Start() : sw_all.Start()
                    End Try
                    sw.Stop()


                    If ReportProgress Then AddNode([TreeView], String.Format("{0}: Applying Windows 7 LogonUI", Now.ToLongTimeString), "info")
                    sw.Reset() : sw.Start()
                    Try
                        Apply_LogonUI7([TreeView])
                        If ReportProgress Then AddNode([TreeView], String.Format("It took {0} seconds", sw.ElapsedMilliseconds / 1000), "time")
                    Catch ex As Exception
                        sw.Stop() : sw_all.Stop()
                        _ErrorHappened = True
                        If ReportProgress Then
                            AddNode([TreeView], String.Format("{0}: Error occured while applying Windows 7 LogonUI", Now.ToLongTimeString), "error")
                            AddException("Error occured while applying Windows 7 LogonUI", ex)
                        Else
                            BugReport.ThrowError(ex)
                        End If

                        sw.Start() : sw_all.Start()
                    End Try
                    sw.Stop()
                End If

                If My.W8 Then
                    If ReportProgress Then AddNode([TreeView], String.Format("{0}: Applying Windows 8.1 Colors", Now.ToLongTimeString), "info")
                    sw.Reset() : sw.Start()
                    Try
                        Windows8.Apply()
                        If ReportProgress Then AddNode([TreeView], String.Format("It took {0} seconds", sw.ElapsedMilliseconds / 1000), "time")
                    Catch ex As Exception
                        sw.Stop() : sw_all.Stop()
                        _ErrorHappened = True
                        If ReportProgress Then
                            AddNode([TreeView], String.Format("{0}: Error occured while applying Windows 8.1 Colors", Now.ToLongTimeString), "error")
                            AddException("Error occured while applying Windows 8.1 Colors", ex)
                        Else
                            BugReport.ThrowError(ex)
                        End If

                        sw.Start() : sw_all.Start()
                    End Try
                    RefreshDWM(Me)
                    sw.Stop()

                    If ReportProgress Then AddNode([TreeView], String.Format("{0}: Applying Windows 8.1 Lock Screen", Now.ToLongTimeString), "info")
                    sw.Reset() : sw.Start()
                    Try
                        Apply_LogonUI_8([TreeView])
                        If ReportProgress Then AddNode([TreeView], String.Format("It took {0} seconds", sw.ElapsedMilliseconds / 1000), "time")

                    Catch ex As Exception
                        sw.Stop() : sw_all.Stop()
                        _ErrorHappened = True
                        If ReportProgress Then
                            AddNode([TreeView], String.Format("{0}: Error occured while applying Windows 8.1 Lock Screen", Now.ToLongTimeString), "error")
                            AddException("Error occured while applying Windows 8.1 Lock Screen", ex)
                        Else
                            BugReport.ThrowError(ex)
                        End If

                        sw.Start() : sw_all.Start()
                    End Try
                    sw.Stop()

                End If

                If ReportProgress Then AddNode([TreeView], String.Format("{0}: Applying Win32UI (Classic Windows Elements)", Now.ToLongTimeString), "info")
                sw.Reset() : sw.Start()
                Try
                    Win32.Apply()
                    If ReportProgress Then AddNode([TreeView], String.Format("It took {0} seconds", sw.ElapsedMilliseconds / 1000), "time")
                Catch ex As Exception
                    sw.Stop() : sw_all.Stop()
                    _ErrorHappened = True
                    If ReportProgress Then
                        AddNode([TreeView], String.Format("{0}: Error occured while applying Win32UI", Now.ToLongTimeString), "error")
                        AddException("Error occured while applying Win32UI", ex)
                    Else
                        BugReport.ThrowError(ex)
                    End If

                    sw.Start() : sw_all.Start()
                End Try
                sw.Stop()

                If ReportProgress Then
                    If WinMetrics_Fonts.Enabled Then
                        AddNode([TreeView], String.Format("{0}: Applying Windows Metrics and Fonts", Now.ToLongTimeString), "info")
                    Else
                        AddNode([TreeView], String.Format("{0}: Skipping Windows Metrics and Fonts as they are disabled", Now.ToLongTimeString), "skip")
                    End If
                End If
                sw.Reset() : sw.Start()
                Try
                    WinMetrics_Fonts.Apply()
                    If ReportProgress And WinMetrics_Fonts.Enabled Then AddNode([TreeView], String.Format("They took {0} seconds", sw.ElapsedMilliseconds / 1000), "time")
                Catch ex As Exception
                    sw.Stop() : sw_all.Stop()
                    _ErrorHappened = True
                    If ReportProgress Then
                        AddNode([TreeView], String.Format("{0}: Error occured while applying Windows Metrics and Fonts", Now.ToLongTimeString), "error")
                        AddException("Error occured while applying Windows Metrics and Fonts", ex)
                    Else
                        BugReport.ThrowError(ex)
                    End If

                    sw.Start() : sw_all.Start()
                End Try
                sw.Stop()

                'Windows Terminals/Consoles
                Dim rLogX As RegistryKey = Registry.CurrentUser.CreateSubKey("Software\WinPaletter\Terminals")
                rLogX.SetValue("Terminal_CMD_Enabled", CommandPrompt.Enabled.ToInteger)
                rLogX.SetValue("Terminal_PS_32_Enabled", PowerShellx86.Enabled.ToInteger)
                rLogX.SetValue("Terminal_PS_64_Enabled", PowerShellx64.Enabled.ToInteger)
                rLogX.SetValue("Terminal_Stable_Enabled", Terminal.Enabled.ToInteger)
                rLogX.SetValue("Terminal_Preview_Enabled", TerminalPreview.Enabled.ToInteger)

                If ReportProgress Then
                    If CommandPrompt.Enabled Then
                        AddNode([TreeView], String.Format("{0}: Applying Command Prompt", Now.ToLongTimeString), "info")
                    Else
                        AddNode([TreeView], String.Format("{0}: Skipping Command Prompt as it is disabled", Now.ToLongTimeString), "skip")
                    End If
                End If
                sw.Reset() : sw.Start()
                Try
                    Apply_CommandPrompt()
                    If ReportProgress And CommandPrompt.Enabled Then AddNode([TreeView], String.Format("It took {0} seconds", sw.ElapsedMilliseconds / 1000), "time")
                Catch ex As Exception
                    sw.Stop() : sw_all.Stop()
                    _ErrorHappened = True
                    If ReportProgress Then
                        AddNode([TreeView], String.Format("{0}: Error occured while applying Command Prompt", Now.ToLongTimeString), "error")
                        AddException("Error occured while applying Command Prompt", ex)
                    Else
                        BugReport.ThrowError(ex)
                    End If

                    sw.Start() : sw_all.Start()
                End Try
                sw.Stop()

                If ReportProgress Then
                    If PowerShellx86.Enabled Then
                        AddNode([TreeView], String.Format("{0}: Applying PowerShell x86", Now.ToLongTimeString), "info")
                    Else
                        AddNode([TreeView], String.Format("{0}: Skipping PowerShell x86 as it is disabled", Now.ToLongTimeString), "skip")
                    End If
                End If
                sw.Reset() : sw.Start()
                Try
                    Apply_PowerShell86()
                    If ReportProgress And PowerShellx86.Enabled Then AddNode([TreeView], String.Format("It took {0} seconds", sw.ElapsedMilliseconds / 1000), "time")
                Catch ex As Exception
                    sw.Stop() : sw_all.Stop()
                    _ErrorHappened = True
                    If ReportProgress Then
                        AddNode([TreeView], String.Format("{0}: Error occured while applying PowerShell x86", Now.ToLongTimeString), "error")
                        AddException("Error occured while applying PowerShell x86", ex)
                    Else
                        BugReport.ThrowError(ex)
                    End If

                    sw.Start() : sw_all.Start()
                End Try
                sw.Stop()

                If ReportProgress Then
                    If PowerShellx64.Enabled Then
                        AddNode([TreeView], String.Format("{0}: Applying PowerShell x64", Now.ToLongTimeString), "info")
                    Else
                        AddNode([TreeView], String.Format("{0}: Skipping PowerShell x64 as it is disabled", Now.ToLongTimeString), "skip")
                    End If
                End If
                sw.Reset() : sw.Start()
                Try
                    Apply_PowerShell64()
                    If ReportProgress And PowerShellx64.Enabled Then AddNode([TreeView], String.Format("It took {0} seconds", sw.ElapsedMilliseconds / 1000), "time")
                Catch ex As Exception
                    sw.Stop() : sw_all.Stop()
                    _ErrorHappened = True
                    If ReportProgress Then
                        AddNode([TreeView], String.Format("{0}: Error occured while applying PowerShell x64", Now.ToLongTimeString), "error")
                        AddException("Error occured while applying PowerShell x64", ex)
                    Else
                        BugReport.ThrowError(ex)
                    End If

                    sw.Start() : sw_all.Start()
                End Try
                sw.Stop()

                sw.Reset() : sw.Start()
                If My.W10 Or My.W11 Then

                    If ReportProgress Then
                        If Terminal.Enabled And TerminalPreview.Enabled Then
                            AddNode([TreeView], String.Format("{0}: Checking if Windows Terminal (Stable & Preview) are installed", Now.ToLongTimeString), "info")

                        ElseIf Terminal.Enabled Then
                            AddNode([TreeView], String.Format("{0}: Skipping Windows Terminal Preview as it is disabled", Now.ToLongTimeString), "skip")
                            AddNode([TreeView], String.Format("{0}: Checking if Windows Terminal Stable is installed", Now.ToLongTimeString), "info")

                        ElseIf TerminalPreview.Enabled Then
                            AddNode([TreeView], String.Format("{0}: Skipping Windows Terminal Stable as it is disabled", Now.ToLongTimeString), "skip")
                            AddNode([TreeView], String.Format("{0}: Checking if Windows Terminal Preview is installed", Now.ToLongTimeString), "info")

                        Else
                            AddNode([TreeView], String.Format("{0}: Skipping Windows Terminal (Stable & Preview) as they are disabled", Now.ToLongTimeString), "skip")

                        End If

                    End If

                    Dim TerDir As String
                    Dim TerPreDir As String

                    If Not My.[Settings].Terminal_Path_Deflection Then
                        TerDir = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) & "\AppData\Local\Packages\Microsoft.WindowsTerminal_8wekyb3d8bbwe\LocalState\settings.json"
                        TerPreDir = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) & "\AppData\Local\Packages\Microsoft.WindowsTerminalPreview_8wekyb3d8bbwe\LocalState\settings.json"
                    Else
                        If IO.File.Exists(My.[Settings].Terminal_Stable_Path) Then
                            TerDir = My.[Settings].Terminal_Stable_Path
                        Else
                            TerDir = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) & "\AppData\Local\Packages\Microsoft.WindowsTerminal_8wekyb3d8bbwe\LocalState\settings.json"
                        End If

                        If IO.File.Exists(My.[Settings].Terminal_Preview_Path) Then
                            TerPreDir = My.[Settings].Terminal_Preview_Path
                        Else
                            TerPreDir = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) & "\AppData\Local\Packages\Microsoft.WindowsTerminalPreview_8wekyb3d8bbwe\LocalState\settings.json"
                        End If
                    End If

                    If Terminal.Enabled Then
                        If IO.File.Exists(TerDir) Then

                            Try
                                AddNode([TreeView], String.Format("{0}: Applying Windows Terminal Stable", Now.ToLongTimeString), "info")
                                Terminal.Save(TerDir, WinTerminal.Mode.JSONFile)
                                If ReportProgress Then AddNode([TreeView], String.Format("It took {0} seconds", sw.ElapsedMilliseconds / 1000), "time")
                            Catch ex As Exception
                                sw.Stop() : sw_all.Stop()
                                _ErrorHappened = True
                                If ReportProgress Then
                                    AddNode([TreeView], String.Format("{0}: Error occured while applying Windows Terminal Stable", Now.ToLongTimeString), "error")
                                    AddException("Error occured while applying Windows Terminal Stable", ex)
                                Else
                                    BugReport.ThrowError(ex)
                                End If

                                sw.Start() : sw_all.Start()
                            End Try

                        Else

                            If Not My.[Settings].Terminal_Path_Deflection Then
                                AddNode([TreeView], String.Format("{0}: Skipping Windows Terminal Stable as it isn't installed.", Now.ToLongTimeString), "skip")
                            Else
                                AddNode([TreeView], String.Format("{0}: Skipping Windows Terminal Stable as deflected file isn't found.", Now.ToLongTimeString), "skip")
                            End If

                        End If
                    End If

                    If TerminalPreview.Enabled Then
                        If IO.File.Exists(TerPreDir) Then

                            Try
                                AddNode([TreeView], String.Format("{0}: Applying Windows Terminal Preview", Now.ToLongTimeString), "info")
                                TerminalPreview.Save(TerPreDir, WinTerminal.Mode.JSONFile, WinTerminal.Version.Preview)
                                If ReportProgress Then AddNode([TreeView], String.Format("It took {0} seconds", sw.ElapsedMilliseconds / 1000), "time")
                            Catch ex As Exception
                                sw.Stop() : sw_all.Stop()
                                _ErrorHappened = True
                                If ReportProgress Then
                                    AddNode([TreeView], String.Format("{0}: Error occured while applying Windows Terminal Preview", Now.ToLongTimeString), "error")
                                    AddException("Error occured while applying Windows Terminal Preview", ex)
                                Else
                                    BugReport.ThrowError(ex)
                                End If

                                sw.Start() : sw_all.Start()
                            End Try

                        Else
                            If Not My.[Settings].Terminal_Path_Deflection Then
                                AddNode([TreeView], String.Format("{0}: Skipping Windows Terminal Preview as it isn't installed.", Now.ToLongTimeString), "skip")
                            Else
                                AddNode([TreeView], String.Format("{0}: Skipping Windows Terminal Preview as deflected file isn't found.", Now.ToLongTimeString), "skip")
                            End If
                        End If
                    End If

                Else
                    AddNode([TreeView], String.Format("{0}: Skipping Windows Terminal (Stable\Preview) as they are not supported for current OS.", Now.ToLongTimeString), "skip")
                End If
                sw.Stop()

                If ReportProgress Then
                    If Cursors_Enabled Then
                        '### Leave it empty
                    Else
                        AddNode([TreeView], String.Format("{0}: Skipping Windows Cursors as it is disabled", Now.ToLongTimeString), "skip")
                    End If
                End If
                sw.Reset() : sw.Start()
                Try
                    Apply_Cursors([TreeView])
                    If ReportProgress And Cursors_Enabled Then AddNode([TreeView], String.Format("{0}: Total Applying Windows Cursors took {1} seconds", Now.ToLongTimeString, sw.ElapsedMilliseconds / 1000), "time")
                Catch ex As Exception
                    sw.Stop() : sw_all.Stop()
                    _ErrorHappened = True
                    If ReportProgress Then
                        AddNode([TreeView], String.Format("{0}: Error occured while applying Windows Cursors", Now.ToLongTimeString), "error")
                        AddException("Error occured while applying Windows Cursors", ex)
                    Else
                        BugReport.ThrowError(ex)
                    End If

                    sw.Start() : sw_all.Start()
                End Try
                sw.Stop()

                If ReportProgress Then
                    If Not _ErrorHappened Then
                        AddNode([TreeView], String.Format("{0}: Applying Theme Done. It took {1} seconds", Now.ToLongTimeString, sw_all.ElapsedMilliseconds / 1000), "success")
                    Else
                        AddNode([TreeView], String.Format("{0}: Applying Theme Done but with error/s. It took {1} seconds", Now.ToLongTimeString, sw_all.ElapsedMilliseconds / 1000), "warning")
                    End If
                End If

                sw_all.Reset()
                sw_all.Stop()
                sw.Reset()
                sw.Stop()

#End Region

            Case Mode.File
#Region "File"
                Dim tx As New List(Of String)
                tx.Clear()
                tx.Add("<WinPaletter - Programmed by Abdelrhman_AK>")
                tx.Add("*Created from App Version= " & Info.AppVersion & vbCrLf)
                tx.Add("*Last Modified by App Version= " & My.Application.Info.Version.ToString & vbCrLf)

#Region "General Info"
                tx.Add("<General>")
                tx.Add("*Palette Name= " & Info.PaletteName)

                If String.IsNullOrWhiteSpace(Info.PaletteDescription) Then
                    tx.Add("*Palette Description= ")
                Else
                    tx.Add("*Palette Description= " & Info.PaletteDescription.Replace(vbCrLf, "<br>"))
                End If

                tx.Add("*Palette File Version= " & Info.PaletteVersion)
                tx.Add("*Author= " & Info.Author)
                tx.Add("*AuthorSocialMediaLink= " & Info.AuthorSocialMediaLink)
                tx.Add("</General>" & vbCrLf)
#End Region

#Region "Windows 10x - Legacy WinPaletter - Before Vesion 1.0.6.9"

                If Info.AppVersion < "1.0.6.9" Or My.[Settings].SaveForLegacyWP Then

                    Try
                        With If(MainFrm.PreviewConfig = MainFrm.WinVer.Eleven, Windows11, Windows10)
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
                        MsgBox(My.Lang.WPTH_OldGen_SaveError, My.MsgboxRt(MsgBoxStyle.Critical))
                    End Try

                End If
#End Region

#Region "Windows 11"
                tx.Add("<Windows11>")
                tx.Add("*Win_11_Color_Index0= " & Windows11.Color_Index0.ToArgb)
                tx.Add("*Win_11_Color_Index1= " & Windows11.Color_Index1.ToArgb)
                tx.Add("*Win_11_Color_Index2= " & Windows11.Color_Index2.ToArgb)
                tx.Add("*Win_11_Color_Index3= " & Windows11.Color_Index3.ToArgb)
                tx.Add("*Win_11_Color_Index4= " & Windows11.Color_Index4.ToArgb)
                tx.Add("*Win_11_Color_Index5= " & Windows11.Color_Index5.ToArgb)
                tx.Add("*Win_11_Color_Index6= " & Windows11.Color_Index6.ToArgb)
                tx.Add("*Win_11_Color_Index7= " & Windows11.Color_Index7.ToArgb)
                tx.Add("*Win_11_Titlebar_Active= " & Windows11.Titlebar_Active.ToArgb)
                tx.Add("*Win_11_Titlebar_Inactive= " & Windows11.Titlebar_Inactive.ToArgb)
                tx.Add("*Win_11_StartMenu_Accent= " & Windows11.StartMenu_Accent.ToArgb)
                tx.Add("*Win_11_WinMode_Light= " & Windows11.WinMode_Light)
                tx.Add("*Win_11_AppMode_Light= " & Windows11.AppMode_Light)
                tx.Add("*Win_11_Transparency= " & Windows11.Transparency)
                tx.Add("*Win_11_ApplyAccentonTitlebars= " & Windows11.ApplyAccentonTitlebars)
                tx.Add("*Win_11_AccentOnStartTBAC= " & Windows11.ApplyAccentonTaskbar)
                tx.Add("</Windows11>" & vbCrLf)
#End Region

#Region "Windows 10"
                tx.Add("<Windows10>")
                tx.Add("*Win_10_Color_Index0= " & Windows10.Color_Index0.ToArgb)
                tx.Add("*Win_10_Color_Index1= " & Windows10.Color_Index1.ToArgb)
                tx.Add("*Win_10_Color_Index2= " & Windows10.Color_Index2.ToArgb)
                tx.Add("*Win_10_Color_Index3= " & Windows10.Color_Index3.ToArgb)
                tx.Add("*Win_10_Color_Index4= " & Windows10.Color_Index4.ToArgb)
                tx.Add("*Win_10_Color_Index5= " & Windows10.Color_Index5.ToArgb)
                tx.Add("*Win_10_Color_Index6= " & Windows10.Color_Index6.ToArgb)
                tx.Add("*Win_10_Color_Index7= " & Windows10.Color_Index7.ToArgb)
                tx.Add("*Win_10_Titlebar_Active= " & Windows10.Titlebar_Active.ToArgb)
                tx.Add("*Win_10_Titlebar_Inactive= " & Windows10.Titlebar_Inactive.ToArgb)
                tx.Add("*Win_10_StartMenu_Accent= " & Windows10.StartMenu_Accent.ToArgb)
                tx.Add("*Win_10_WinMode_Light= " & Windows10.WinMode_Light)
                tx.Add("*Win_10_AppMode_Light= " & Windows10.AppMode_Light)
                tx.Add("*Win_10_Transparency= " & Windows10.Transparency)
                tx.Add("*Win_10_ApplyAccentonTitlebars= " & Windows10.ApplyAccentonTitlebars)
                tx.Add("*Win_10_AccentOnStartTBAC= " & Windows10.ApplyAccentonTaskbar)
                tx.Add("</Windows10>" & vbCrLf)
#End Region

#Region "Aero"
                tx.Add("<Aero>")
                tx.Add("*Aero_ColorizationColor= " & Windows7.ColorizationColor.ToArgb)
                tx.Add("*Aero_ColorizationAfterglow= " & Windows7.ColorizationAfterglow.ToArgb)
                tx.Add("*Aero_ColorizationColorBalance= " & Windows7.ColorizationColorBalance)
                tx.Add("*Aero_ColorizationAfterglowBalance= " & Windows7.ColorizationAfterglowBalance)
                tx.Add("*Aero_ColorizationBlurBalance= " & Windows7.ColorizationBlurBalance)
                tx.Add("*Aero_ColorizationGlassReflectionIntensity= " & Windows7.ColorizationGlassReflectionIntensity)
                tx.Add("*Aero_EnableAeroPeek= " & Windows7.EnableAeroPeek)
                tx.Add("*Aero_AlwaysHibernateThumbnails= " & Windows7.AlwaysHibernateThumbnails)
                tx.Add("*Aero_Theme= " & Windows7.Theme)
                tx.Add("</Aero>" & vbCrLf)
#End Region

#Region "Metro"
                tx.Add("<Metro>")
                tx.Add("*Metro_ColorizationColor= " & Windows8.ColorizationColor.ToArgb)
                tx.Add("*Metro_ColorizationColorBalance= " & Windows8.ColorizationColorBalance)
                tx.Add("*Metro_PersonalColors_Background= " & Windows8.PersonalColors_Background.ToArgb)
                tx.Add("*Metro_PersonalColors_Accent= " & Windows8.PersonalColors_Accent.ToArgb)
                tx.Add("*Metro_StartColor= " & Windows8.StartColor.ToArgb)
                tx.Add("*Metro_AccentColor= " & Windows8.AccentColor.ToArgb)
                tx.Add("*Metro_Start= " & Windows8.Start)
                tx.Add("*Metro_Theme= " & Windows8.Theme)
                tx.Add("*Metro_LogonUI= " & Windows8.LogonUI)
                tx.Add("*Metro_NoLockScreen= " & Windows8.NoLockScreen)
                tx.Add("*Metro_LockScreenType= " & Windows8.LockScreenType)
                tx.Add("*Metro_LockScreenSystemID= " & Windows8.LockScreenSystemID)
                tx.Add("</Metro>" & vbCrLf)
#End Region

#Region "LogonUI"
                tx.Add("<LogonUI_10_11>")
                tx.Add("*LogonUI_DisableAcrylicBackgroundOnLogon= " & LogonUI10x.DisableAcrylicBackgroundOnLogon)
                tx.Add("*LogonUI_DisableLogonBackgroundImage= " & LogonUI10x.DisableLogonBackgroundImage)
                tx.Add("*LogonUI_NoLockScreen= " & LogonUI10x.NoLockScreen)
                tx.Add("</LogonUI_10_11>" & vbCrLf)
#End Region

#Region "LogonUI_7_8"
                tx.Add("<LogonUI_7_8>")
                tx.Add("*LogonUI7_Enabled= " & LogonUI7.Enabled)
                tx.Add("*LogonUI7_Mode= " & LogonUI7.Mode)
                tx.Add("*LogonUI7_ImagePath= " & LogonUI7.ImagePath)
                tx.Add("*LogonUI7_Color= " & LogonUI7.Color.ToArgb)
                tx.Add("*LogonUI7_Effect_Blur= " & LogonUI7.Blur)
                tx.Add("*LogonUI7_Effect_Blur_Intensity= " & LogonUI7.Blur_Intensity)
                tx.Add("*LogonUI7_Effect_Grayscale= " & LogonUI7.Grayscale)
                tx.Add("*LogonUI7_Effect_Noise= " & LogonUI7.Noise)
                tx.Add("*LogonUI7_Effect_Noise_Mode= " & LogonUI7.Noise_Mode)
                tx.Add("*LogonUI7_Effect_Noise_Intensity= " & LogonUI7.Noise_Intensity)
                tx.Add("</LogonUI_7_8>" & vbCrLf)
#End Region

#Region "Win32UI"
                tx.Add("<Win32UI>")
                tx.Add("*Win32UI_EnableTheming= " & Win32.EnableTheming)
                tx.Add("*Win32UI_EnableGradient= " & Win32.EnableGradient)
                tx.Add("*Win32UI_ActiveBorder= " & Win32.ActiveBorder.ToArgb)
                tx.Add("*Win32UI_ActiveTitle= " & Win32.ActiveTitle.ToArgb)
                tx.Add("*Win32UI_AppWorkspace= " & Win32.AppWorkspace.ToArgb)
                tx.Add("*Win32UI_Background= " & Win32.Background.ToArgb)
                tx.Add("*Win32UI_ButtonAlternateFace= " & Win32.ButtonAlternateFace.ToArgb)
                tx.Add("*Win32UI_ButtonDkShadow= " & Win32.ButtonDkShadow.ToArgb)
                tx.Add("*Win32UI_ButtonFace= " & Win32.ButtonFace.ToArgb)
                tx.Add("*Win32UI_ButtonHilight= " & Win32.ButtonHilight.ToArgb)
                tx.Add("*Win32UI_ButtonLight= " & Win32.ButtonLight.ToArgb)
                tx.Add("*Win32UI_ButtonShadow= " & Win32.ButtonShadow.ToArgb)
                tx.Add("*Win32UI_ButtonText= " & Win32.ButtonText.ToArgb)
                tx.Add("*Win32UI_GradientActiveTitle= " & Win32.GradientActiveTitle.ToArgb)
                tx.Add("*Win32UI_GradientInactiveTitle= " & Win32.GradientInactiveTitle.ToArgb)
                tx.Add("*Win32UI_GrayText= " & Win32.GrayText.ToArgb)
                tx.Add("*Win32UI_HilightText= " & Win32.HilightText.ToArgb)
                tx.Add("*Win32UI_HotTrackingColor= " & Win32.HotTrackingColor.ToArgb)
                tx.Add("*Win32UI_InactiveBorder= " & Win32.InactiveBorder.ToArgb)
                tx.Add("*Win32UI_InactiveTitle= " & Win32.InactiveTitle.ToArgb)
                tx.Add("*Win32UI_InactiveTitleText= " & Win32.InactiveTitleText.ToArgb)
                tx.Add("*Win32UI_InfoText= " & Win32.InfoText.ToArgb)
                tx.Add("*Win32UI_InfoWindow= " & Win32.InfoWindow.ToArgb)
                tx.Add("*Win32UI_Menu= " & Win32.Menu.ToArgb)
                tx.Add("*Win32UI_MenuBar= " & Win32.MenuBar.ToArgb)
                tx.Add("*Win32UI_MenuText= " & Win32.MenuText.ToArgb)
                tx.Add("*Win32UI_Scrollbar= " & Win32.Scrollbar.ToArgb)
                tx.Add("*Win32UI_TitleText= " & Win32.TitleText.ToArgb)
                tx.Add("*Win32UI_Window= " & Win32.Window.ToArgb)
                tx.Add("*Win32UI_WindowFrame= " & Win32.WindowFrame.ToArgb)
                tx.Add("*Win32UI_WindowText= " & Win32.WindowText.ToArgb)
                tx.Add("*Win32UI_Hilight= " & Win32.Hilight.ToArgb)
                tx.Add("*Win32UI_MenuHilight= " & Win32.MenuHilight.ToArgb)
                tx.Add("*Win32UI_Desktop= " & Win32.Desktop.ToArgb)
                tx.Add("</Win32UI>")
#End Region

#Region "Terminals"
                tx.Add(vbCrLf & "<Terminals>")

                Console_Structure.Write_Console_To_ListOfString("CMD", CommandPrompt, tx)
                Console_Structure.Write_Console_To_ListOfString("PS_32", PowerShellx86, tx)
                Console_Structure.Write_Console_To_ListOfString("PS_64", PowerShellx64, tx)

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
                tx.Add("*Cursor_Enabled= " & Cursors_Enabled)
                Cursor_Structure.Write_Cursors_To_ListOfString("Arrow", Cursor_Arrow, tx)
                Cursor_Structure.Write_Cursors_To_ListOfString("Help", Cursor_Help, tx)
                Cursor_Structure.Write_Cursors_To_ListOfString("AppLoading", Cursor_AppLoading, tx)
                Cursor_Structure.Write_Cursors_To_ListOfString("Busy", Cursor_Busy, tx)
                Cursor_Structure.Write_Cursors_To_ListOfString("Move", Cursor_Move, tx)
                Cursor_Structure.Write_Cursors_To_ListOfString("NS", Cursor_NS, tx)
                Cursor_Structure.Write_Cursors_To_ListOfString("EW", Cursor_EW, tx)
                Cursor_Structure.Write_Cursors_To_ListOfString("NESW", Cursor_NESW, tx)
                Cursor_Structure.Write_Cursors_To_ListOfString("NWSE", Cursor_NWSE, tx)
                Cursor_Structure.Write_Cursors_To_ListOfString("Up", Cursor_Up, tx)
                Cursor_Structure.Write_Cursors_To_ListOfString("Pen", Cursor_Pen, tx)
                Cursor_Structure.Write_Cursors_To_ListOfString("None", Cursor_None, tx)
                Cursor_Structure.Write_Cursors_To_ListOfString("Link", Cursor_Link, tx)
                Cursor_Structure.Write_Cursors_To_ListOfString("Pin", Cursor_Pin, tx)
                Cursor_Structure.Write_Cursors_To_ListOfString("Person", Cursor_Person, tx)
                Cursor_Structure.Write_Cursors_To_ListOfString("IBeam", Cursor_IBeam, tx)
                Cursor_Structure.Write_Cursors_To_ListOfString("Cross", Cursor_Cross, tx)
                tx.Add("</Cursors>")
#End Region

                tx.Add(vbCrLf & "</WinPaletter>")

                IO.File.WriteAllText(FileLocation, tx.CString)
#End Region

        End Select

    End Sub
#End Region

#Region "Applying Subs"
    Public Sub Apply_LogonUI7(Optional ByVal [TreeView] As TreeView = Nothing)

        Dim ReportProgress As Boolean = ([TreeView] IsNot Nothing)

        EditReg("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Authentication\LogonUI\Background", "OEMBackground", LogonUI7.Enabled.ToInteger)
        EditReg("HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Windows\System", "UseOEMBackground", LogonUI7.Enabled.ToInteger)

        Dim rLog As RegistryKey = Registry.CurrentUser.CreateSubKey("Software\WinPaletter\LogonUI")

        Select Case LogonUI7.Mode
            Case LogonUI_Modes.Default_
                rLog.SetValue("Mode", 0)

            Case LogonUI_Modes.Wallpaper
                rLog.SetValue("Mode", 1)

            Case LogonUI_Modes.CustomImage
                rLog.SetValue("Mode", 2)

            Case LogonUI_Modes.SolidColor
                rLog.SetValue("Mode", 3)
        End Select

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

        If LogonUI7.Enabled Then
            NativeMethods.Kernel32.Wow64DisableWow64FsRedirection(IntPtr.Zero)

            Dim DirX As String = Environment.GetFolderPath(Environment.SpecialFolder.Windows) & "\system32\oobe\info\backgrounds"
            Dim imageres As String = Environment.GetFolderPath(Environment.SpecialFolder.Windows) & "\system32\imageres.dll"

            IO.Directory.CreateDirectory(DirX)

            For Each fileX As String In My.Computer.FileSystem.GetFiles(DirX)
                Try : Kill(fileX) : Catch : End Try
            Next

            Dim bmpList As New List(Of Bitmap)
            bmpList.Clear()

            Select Case LogonUI7.Mode
                Case LogonUI_Modes.Default_

                    For i As Integer = 5031 To 5043 Step +1
                        bmpList.Add(LoadFromDLL(imageres, i, "IMAGE", My.Computer.Screen.Bounds.Size.Width, My.Computer.Screen.Bounds.Size.Height))
                    Next

                Case LogonUI_Modes.CustomImage

                    If IO.File.Exists(LogonUI7.ImagePath) Then
                        bmpList.Add(Image.FromStream(New IO.FileStream(LogonUI7.ImagePath, IO.FileMode.Open, IO.FileAccess.Read)))
                    Else
                        bmpList.Add(Color.Black.ToBitmap(My.Computer.Screen.Bounds.Size))
                    End If

                Case LogonUI_Modes.SolidColor
                    bmpList.Add(LogonUI7.Color.ToBitmap(My.Computer.Screen.Bounds.Size))

                Case LogonUI_Modes.Wallpaper
                    bmpList.Add(My.Application.GetWallpaper)
            End Select


            For x = 0 To bmpList.Count - 1
                If ReportProgress Then AddNode([TreeView], String.Format("{3}: " & My.Lang.CP_RenderingCustomLogonUI_Progress & " {2} ({0}/{1})", x + 1, bmpList.Count, bmpList(x).Width & "x" & bmpList(x).Height, Now.ToLongTimeString), "info")

                If LogonUI7.Grayscale Then bmpList(x) = bmpList(x).Grayscale
                If LogonUI7.Blur Then bmpList(x) = bmpList(x).Blur(LogonUI7.Blur_Intensity)
                If LogonUI7.Noise Then bmpList(x) = bmpList(x).Noise(LogonUI7.Noise_Mode, LogonUI7.Noise_Intensity / 100)
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
            Dim bmp As Bitmap

            Select Case Windows8.LockScreenType

                Case LogonUI_Modes.Default_
                    Dim syslock As String
                    If Not MainFrm.CP.Windows8.LockScreenSystemID = 1 And Not MainFrm.CP.Windows8.LockScreenSystemID = 3 Then
                        syslock = String.Format(Environment.GetFolderPath(Environment.SpecialFolder.Windows) & "\Web\Screen\img10{0}.jpg", MainFrm.CP.Windows8.LockScreenSystemID)
                    Else
                        syslock = String.Format(Environment.GetFolderPath(Environment.SpecialFolder.Windows) & "\Web\Screen\img10{0}.png", MainFrm.CP.Windows8.LockScreenSystemID)
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
            Console_Structure.Save_Console_To_Registry("", CommandPrompt)
            If My.[Settings].CMD_OverrideUserPreferences Then Console_Structure.Save_Console_To_Registry("%SystemRoot%_System32_cmd.exe", CommandPrompt)
        End If
    End Sub

    Public Sub Apply_PowerShell86()
        If PowerShellx86.Enabled And IO.Directory.Exists(Environment.GetEnvironmentVariable("WINDIR") & "\System32\WindowsPowerShell\v1.0") Then
            Console_Structure.Save_Console_To_Registry("%SystemRoot%_System32_WindowsPowerShell_v1.0_powershell.exe", PowerShellx86)
        End If
    End Sub

    Public Sub Apply_PowerShell64()
        If PowerShellx64.Enabled And IO.Directory.Exists(Environment.GetEnvironmentVariable("WINDIR") & "\SysWOW64\WindowsPowerShell\v1.0") Then
            Console_Structure.Save_Console_To_Registry("%SystemRoot%_SysWOW64_WindowsPowerShell_v1.0_powershell.exe", PowerShellx64)
        End If
    End Sub

    Public Sub Apply_Cursors(Optional ByVal [TreeView] As TreeView = Nothing)

        Dim ReportProgress As Boolean = ([TreeView] IsNot Nothing)

        Dim rMain As RegistryKey = Registry.CurrentUser.CreateSubKey("Software\WinPaletter\Cursors")
        rMain.SetValue("", Cursors_Enabled, RegistryValueKind.DWord)
        rMain.Close()

        If Cursors_Enabled Then
            Dim sw As New Stopwatch
            If ReportProgress Then AddNode([TreeView], String.Format("{0}: " & My.Lang.CP_SavingCursorsColors, Now.ToLongTimeString), "info")

            sw.Reset()
            sw.Start()

            Cursor_Structure.Save_Cursors_To_Registry("Arrow", Cursor_Arrow)
            Cursor_Structure.Save_Cursors_To_Registry("Help", Cursor_Help)
            Cursor_Structure.Save_Cursors_To_Registry("AppLoading", Cursor_AppLoading)
            Cursor_Structure.Save_Cursors_To_Registry("Busy", Cursor_Busy)
            Cursor_Structure.Save_Cursors_To_Registry("Move", Cursor_Move)
            Cursor_Structure.Save_Cursors_To_Registry("NS", Cursor_NS)
            Cursor_Structure.Save_Cursors_To_Registry("EW", Cursor_EW)
            Cursor_Structure.Save_Cursors_To_Registry("NESW", Cursor_NESW)
            Cursor_Structure.Save_Cursors_To_Registry("NWSE", Cursor_NWSE)
            Cursor_Structure.Save_Cursors_To_Registry("Up", Cursor_Up)
            Cursor_Structure.Save_Cursors_To_Registry("Pen", Cursor_Pen)
            Cursor_Structure.Save_Cursors_To_Registry("None", Cursor_None)
            Cursor_Structure.Save_Cursors_To_Registry("Link", Cursor_Link)
            Cursor_Structure.Save_Cursors_To_Registry("Pin", Cursor_Pin)
            Cursor_Structure.Save_Cursors_To_Registry("Person", Cursor_Person)
            Cursor_Structure.Save_Cursors_To_Registry("IBeam", Cursor_IBeam)
            Cursor_Structure.Save_Cursors_To_Registry("Cross", Cursor_Cross)

            If ReportProgress Then AddNode([TreeView], String.Format("It took {0} seconds", sw.ElapsedMilliseconds / 1000), "time")
            sw.Stop()

            Try
                sw.Reset()
                sw.Start()
                If ReportProgress Then AddNode([TreeView], String.Format("{0}: " & My.Lang.CP_RenderingCursors, Now.ToLongTimeString), "info")
                ExportCursors(Me)
                sw.Stop()
                If ReportProgress Then AddNode([TreeView], String.Format("It took {0} seconds", sw.ElapsedMilliseconds / 1000), "time")

                If My.[Settings].AutoApplyCursors Then
                    If ReportProgress Then AddNode([TreeView], String.Format("{0}: " & My.Lang.CP_ApplyingCursors, Now.ToLongTimeString), "info")
                    sw.Reset()
                    sw.Start()
                    ApplyCursorsToReg()
                    sw.Stop()
                    If ReportProgress Then AddNode([TreeView], String.Format("It took {0} seconds", sw.ElapsedMilliseconds / 1000), "time")
                Else
                    If ReportProgress Then AddNode([TreeView], String.Format("{0}: Modifying Windows Cursors is restricted from Settings", Now.ToLongTimeString), "error")
                End If

            Catch ex As Exception
                If ReportProgress Then
                    AddNode([TreeView], String.Format("{0}: Error occured while Rendering\Applying Windows Cursors", Now.ToLongTimeString), "error")
                    AddException("Error occured while Rendering\Applying Windows Cursors", ex)
                End If

            End Try
        Else

            If My.[Settings].ResetCursorsToAero Then
                ResetCursorsToAero()
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

        SystemParametersInfo(SPI.SPI_SETCURSORS, 0, 0, SPIF.SPIF_UPDATEINIFILE Or SPIF.SPIF_SENDCHANGE)
    End Sub

    Shared Sub ResetCursorsToAero()
        Dim R As RegistryKey = Registry.CurrentUser.OpenSubKey("Control Panel\Cursors", True)
        Dim path As String = "%SystemRoot%\cursors"

        R.SetValue("", "Windows Default", RegistryValueKind.String)

        R.SetValue("CursorBaseSize", 32, RegistryValueKind.DWord)

        Dim x As String = String.Format("{0}\{1}", path, "aero_working.ani")
        R.SetValue("AppStarting", x)
        NativeMethods.User32.SetSystemCursor(NativeMethods.User32.LoadCursorFromFile(x), NativeMethods.User32.OCR_SYSTEM_CURSORS.OCR_APPSTARTING)

        x = String.Format("{0}\{1}", path, "aero_arrow.cur")
        R.SetValue("Arrow", x)
        NativeMethods.User32.SetSystemCursor(NativeMethods.User32.LoadCursorFromFile(x), NativeMethods.User32.OCR_SYSTEM_CURSORS.OCR_NORMAL)

        x = String.Format("")
        R.SetValue("Crosshair", x)
        NativeMethods.User32.SetSystemCursor(NativeMethods.User32.LoadCursorFromFile(x), NativeMethods.User32.OCR_SYSTEM_CURSORS.OCR_CROSS)

        x = String.Format("{0}\{1}", path, "aero_link.cur")
        R.SetValue("Hand", x)
        NativeMethods.User32.SetSystemCursor(NativeMethods.User32.LoadCursorFromFile(x), NativeMethods.User32.OCR_SYSTEM_CURSORS.OCR_HAND)

        x = String.Format("{0}\{1}", path, "aero_helpsel.cur")
        R.SetValue("Help", x)
        NativeMethods.User32.SetSystemCursor(NativeMethods.User32.LoadCursorFromFile(x), NativeMethods.User32.OCR_SYSTEM_CURSORS.OCR_HELP)

        x = String.Format("")
        R.SetValue("IBeam", x)
        NativeMethods.User32.SetSystemCursor(NativeMethods.User32.LoadCursorFromFile(x), NativeMethods.User32.OCR_SYSTEM_CURSORS.OCR_IBEAM)

        x = String.Format("{0}\{1}", path, "aero_unavail.cur")
        R.SetValue("No", x)
        NativeMethods.User32.SetSystemCursor(NativeMethods.User32.LoadCursorFromFile(x), NativeMethods.User32.OCR_SYSTEM_CURSORS.OCR_NO)

        x = String.Format("{0}\{1}", path, "aero_pen.cur")
        R.SetValue("NWPen", x)
        'NativeMethods.User32.SetSystemCursor(NativeMethods.User32.LoadCursorFromFile(x), NativeMethods.User32.OCR_SYSTEM_CURSORS.OCR_)

        x = String.Format("{0}\{1}", path, "aero_person.cur")
        R.SetValue("Person", x)
        'NativeMethods.User32.SetSystemCursor(NativeMethods.User32.LoadCursorFromFile(x), NativeMethods.User32.OCR_SYSTEM_CURSORS.OCR_APPSTARTING)

        x = String.Format("{0}\{1}", path, "aero_pin.cur")
        R.SetValue("Pin", x)
        'NativeMethods.User32.SetSystemCursor(NativeMethods.User32.LoadCursorFromFile(x), NativeMethods.User32.OCR_SYSTEM_CURSORS.OCR_APPSTARTING)

        x = String.Format("{0}\{1}", path, "aero_move.cur")
        R.SetValue("SizeAll", x)
        NativeMethods.User32.SetSystemCursor(NativeMethods.User32.LoadCursorFromFile(x), NativeMethods.User32.OCR_SYSTEM_CURSORS.OCR_SIZEALL)

        x = String.Format("{0}\{1}", path, "aero_nesw.cur")
        R.SetValue("SizeNESW", x)
        NativeMethods.User32.SetSystemCursor(NativeMethods.User32.LoadCursorFromFile(x), NativeMethods.User32.OCR_SYSTEM_CURSORS.OCR_SIZENESW)

        x = String.Format("{0}\{1}", path, "aero_ns.cur")
        R.SetValue("SizeNS", x)
        NativeMethods.User32.SetSystemCursor(NativeMethods.User32.LoadCursorFromFile(x), NativeMethods.User32.OCR_SYSTEM_CURSORS.OCR_SIZENS)

        x = String.Format("{0}\{1}", path, "aero_nwse.cur")
        R.SetValue("SizeNWSE", x)
        NativeMethods.User32.SetSystemCursor(NativeMethods.User32.LoadCursorFromFile(x), NativeMethods.User32.OCR_SYSTEM_CURSORS.OCR_SIZENWSE)

        x = String.Format("{0}\{1}", path, "aero_ew.cur")
        R.SetValue("SizeWE", x)
        NativeMethods.User32.SetSystemCursor(NativeMethods.User32.LoadCursorFromFile(x), NativeMethods.User32.OCR_SYSTEM_CURSORS.OCR_SIZEWE)

        x = String.Format("{0}\{1}", path, "aero_up.cur")
        R.SetValue("UpArrow", x)
        NativeMethods.User32.SetSystemCursor(NativeMethods.User32.LoadCursorFromFile(x), NativeMethods.User32.OCR_SYSTEM_CURSORS.OCR_UP)

        x = String.Format("{0}\{1}", path, "aero_busy.ani")
        R.SetValue("Wait", x)
        NativeMethods.User32.SetSystemCursor(NativeMethods.User32.LoadCursorFromFile(x), NativeMethods.User32.OCR_SYSTEM_CURSORS.OCR_WAIT)

        R.SetValue("Scheme Source", 2, RegistryValueKind.DWord)

        R.Close()

        Dim rx As RegistryKey = Registry.CurrentUser.OpenSubKey("Control Panel\Cursors\Schemes", True)

        rx.DeleteValue("WinPaletter", False)

        rx.Close()

        SystemParametersInfo(SPI.SPI_SETCURSORS, 0, 0, SPIF.SPIF_UPDATEINIFILE Or SPIF.SPIF_SENDCHANGE)
    End Sub
#End Region

#Region "Comparisons"
    Public Overrides Function Equals(obj As Object) As Boolean
        Dim _Equals As Boolean = True

        If Info <> DirectCast(obj, CP).Info Then _Equals = False
        If Windows11 <> DirectCast(obj, CP).Windows11 Then _Equals = False
        If LogonUI10x <> DirectCast(obj, CP).LogonUI10x Then _Equals = False
        If Windows7 <> DirectCast(obj, CP).Windows7 Then _Equals = False
        If Windows8 <> DirectCast(obj, CP).Windows8 Then _Equals = False
        If LogonUI7 <> DirectCast(obj, CP).LogonUI7 Then _Equals = False
        If Win32 <> DirectCast(obj, CP).Win32 Then _Equals = False
        If WinMetrics_Fonts <> DirectCast(obj, CP).WinMetrics_Fonts Then _Equals = False

        If Cursors_Enabled <> DirectCast(obj, CP).Cursors_Enabled Then _Equals = False
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