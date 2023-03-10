Imports WinPaletter.CP

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
        Dim [CP] As New CP(CP.CP_Type.Empty)

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

        Return [CP]
    End Function

    Public Function Default_Windows10() As CP
        Dim [CP] As New CP(CP.CP_Type.Empty)

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

        Return [CP]
    End Function

    Public Function Default_Windows8() As CP
        Dim [CP] As New CP(CP.CP_Type.Empty)

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

        Return [CP]
    End Function

    Public Function Default_Windows7() As CP
        Dim [CP] As New CP(CP.CP_Type.Empty)

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

        Return [CP]
    End Function

    Public Function Default_WindowsVista() As CP
        Dim [CP] As New CP(CP.CP_Type.Empty)

        With [CP].Info
            .PaletteName = "Windows Vista (Initial)"
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

        Return [CP]
    End Function

    Public Function Default_WindowsXP() As CP
        Dim [CP] As New CP(CP.CP_Type.Empty)

        With [CP].Info
            .PaletteName = "Windows XP (Initial)"
            .PaletteDescription = "Initial; Like first time after Windows Setup"
            .PaletteVersion = "1.0.0.0"
            .Author = "Microsoft"
            .AuthorSocialMediaLink = "www.microsoft.com"
            .AppVersion = My.Application.Info.Version.ToString
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
        End With

        With [CP].Cursor_None
            .ArrowStyle = ArrowStyle.Classic
            .CircleStyle = CircleStyle.Classic
            .PrimaryColorGradient = False
            .SecondaryColorGradient = False
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