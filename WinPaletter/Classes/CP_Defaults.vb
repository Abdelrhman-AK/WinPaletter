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

    Public Function Default_Windows11() As CP
        Dim [CP] As New CP(CP.Mode.Empty)

        With [CP].Info
            .PaletteName = "Windows 11 (Initial)"
            .PaletteDescription = "Initial; Like first time after Windows Setup"
            .PaletteVersion = "1.0.0.0"
            .Author = "Microsoft"
            .AuthorSocialMediaLink = "www.microsoft.com"
            .AppVersion = My.Application.Info.Version.ToString
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
            .MinAnimate = True
            .PaddedBorderWidth = 4
            .ScrollHeight = 17
            .ScrollWidth = 17
            .SmCaptionHeight = 22
            .SmCaptionWidth = 22
            .DesktopIconSize = 48
            .ShellIconSize = 32
        End With

        [CP].Terminal = New WinTerminal("", WinTerminal.Mode.Empty)
        [CP].TerminalPreview = New WinTerminal("", WinTerminal.Mode.Empty)

        Return [CP]
    End Function

    Public Function Default_Windows10() As CP
        Dim [CP] As New CP(CP.Mode.Empty)

        With [CP].Info
            .PaletteName = "Windows 10 (Initial)"
            .PaletteDescription = "Initial; Like first time after Windows Setup"
            .PaletteVersion = "1.0.0.0"
            .Author = "Microsoft"
            .AuthorSocialMediaLink = "www.microsoft.com"
            .AppVersion = My.Application.Info.Version.ToString
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
            .MinAnimate = True
            .PaddedBorderWidth = 4
            .ScrollHeight = 17
            .ScrollWidth = 17
            .SmCaptionHeight = 22
            .SmCaptionWidth = 22
            .DesktopIconSize = 48
            .ShellIconSize = 32
        End With

        [CP].Terminal = New WinTerminal("", WinTerminal.Mode.Empty)
        [CP].TerminalPreview = New WinTerminal("", WinTerminal.Mode.Empty)

        Return [CP]
    End Function

    Public Function Default_Windows8() As CP
        Dim [CP] As New CP(CP.Mode.Empty)

        With [CP].Info
            .PaletteName = "Windows 8.1 (Initial)"
            .PaletteDescription = "Initial; Like first time after Windows Setup"
            .PaletteVersion = "1.0.0.0"
            .Author = "Microsoft"
            .AuthorSocialMediaLink = "www.microsoft.com"
            .AppVersion = My.Application.Info.Version.ToString
        End With

        With [CP].Windows8
            .ColorizationColor = Color.FromArgb(246, 195, 74)
            .ColorizationColorBalance = 78
            .PersonalColors_Background = Color.FromArgb(30, 0, 84)
            .PersonalColors_Accent = Color.FromArgb(72, 29, 178)
            .StartColor = Color.FromArgb(30, 0, 84)
            .AccentColor = Color.FromArgb(72, 29, 178)
            .Start = 0
            .Theme = CP.AeroTheme.Aero
            .LogonUI = 0
            .NoLockScreen = False
            .LockScreenType = CP.LogonUI_Modes.Default_
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
            .MinAnimate = True
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

        [CP].Terminal = New WinTerminal("", WinTerminal.Mode.Empty)
        [CP].TerminalPreview = New WinTerminal("", WinTerminal.Mode.Empty)

        Return [CP]
    End Function

    Public Function Default_Windows7() As CP
        Dim [CP] As New CP(CP.Mode.Empty)

        With [CP].Info
            .PaletteName = "Windows 7 (Initial)"
            .PaletteDescription = "Initial; Like first time after Windows Setup"
            .PaletteVersion = "1.0.0.0"
            .Author = "Microsoft"
            .AuthorSocialMediaLink = "www.microsoft.com"
            .AppVersion = My.Application.Info.Version.ToString
        End With

        With [CP].Windows8
            .ColorizationColor = Color.FromArgb(246, 195, 74)
            .ColorizationColorBalance = 78
            .PersonalColors_Background = Color.FromArgb(30, 0, 84)
            .PersonalColors_Accent = Color.FromArgb(72, 29, 178)
            .StartColor = Color.FromArgb(30, 0, 84)
            .AccentColor = Color.FromArgb(72, 29, 178)
            .Start = 0
            .Theme = CP.AeroTheme.Aero
            .LogonUI = 0
            .NoLockScreen = False
            .LockScreenType = CP.LogonUI_Modes.Default_
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
            .Theme = CP.AeroTheme.Aero
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
            .MinAnimate = True
            .PaddedBorderWidth = 4
            .ScrollHeight = 17
            .ScrollWidth = 17
            .SmCaptionHeight = 17
            .SmCaptionWidth = 17
            .DesktopIconSize = 48
            .ShellIconSize = 32
        End With

        [CP].Terminal = New WinTerminal("", WinTerminal.Mode.Empty)
        [CP].TerminalPreview = New WinTerminal("", WinTerminal.Mode.Empty)

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