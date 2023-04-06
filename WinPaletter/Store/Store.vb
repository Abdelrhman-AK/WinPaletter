Imports System.IO
Imports WinPaletter.XenonCore
Imports System.Security.Cryptography
Imports WinPaletter.MainFrm
Imports WinPaletter.CP
Imports WinPaletter.NativeMethods
Public Class Store
    Private FinishedLoadingInitialCPs As Boolean
    Dim CPList As New Dictionary(Of String, CP)
    Dim w As Integer = 528 * 0.6
    Dim h As Integer = 297 * 0.6

    Private elapsedSeconds As Integer = 0
    Private SwitchAfterSecs As Integer = 1

    Private hoveredItem As StoreItem
    Private ModernOrClassic As Integer = 0
    Private SwitchedByMouseWheel As Boolean = False
#Region "Preview Subs"

    Sub ReValidateLivePreview(ByVal Parent As Control)
        Parent.Refresh()

        For Each ctrl As Control In Parent.Controls
            ctrl.Refresh()
            If ctrl.HasChildren Then
                For Each c As Control In ctrl.Controls
                    ReValidateLivePreview(c)
                Next
            End If
        Next
    End Sub

    Sub Adjust_Preview(CP As CP)
        Dim condition0 As Boolean = MainFrm.PreviewConfig = WinVer.W7 AndAlso CP.Windows7.Theme = AeroTheme.Classic
        Dim condition1 As Boolean = MainFrm.PreviewConfig = WinVer.WXP AndAlso CP.WindowsXP.Theme = WinXPTheme.Classic

        tabs_preview.SelectedIndex = If(condition0 Or condition1, 1, ModernOrClassic)

        Panel3.Visible = (MainFrm.PreviewConfig = WinVer.W11 Or MainFrm.PreviewConfig = WinVer.W10)
        lnk_preview.Visible = (MainFrm.PreviewConfig = WinVer.W11 Or MainFrm.PreviewConfig = WinVer.W10)
        start.Visible = (Not MainFrm.PreviewConfig = WinVer.W8)
        ActionCenter.Visible = (MainFrm.PreviewConfig = WinVer.W11 Or MainFrm.PreviewConfig = WinVer.W10)

        RetroButton3.Image = My.Resources.ActiveApp_Taskbar
        RetroButton4.Image = My.Resources.InactiveApp_Taskbar

        Select Case MainFrm.PreviewConfig
            Case WinVer.W11
#Region "Win11"
                If CP.WallpaperTone_W11.Enabled Then pnl_preview.BackgroundImage = MainFrm.GetTintedWallpaper(CP.WallpaperTone_W11)
                XenonWindow1.AccentColor_Enabled = [CP].Windows11.ApplyAccentonTitlebars
                XenonWindow2.AccentColor_Enabled = [CP].Windows11.ApplyAccentonTitlebars

                XenonWindow1.AccentColor_Active = [CP].Windows11.Titlebar_Active
                XenonWindow2.AccentColor_Active = [CP].Windows11.Titlebar_Active

                XenonWindow1.AccentColor_Inactive = [CP].Windows11.Titlebar_Inactive
                XenonWindow2.AccentColor_Inactive = [CP].Windows11.Titlebar_Inactive

                XenonWindow1.DarkMode = Not [CP].Windows11.AppMode_Light
                XenonWindow2.DarkMode = Not [CP].Windows11.AppMode_Light

                XenonWindow1.Shadow = [CP].WindowsEffects.WindowShadow
                XenonWindow2.Shadow = [CP].WindowsEffects.WindowShadow

                Label8.ForeColor = If([CP].Windows11.AppMode_Light, Color.Black, Color.White)

                start.DarkMode = Not [CP].Windows11.WinMode_Light
                taskbar.DarkMode = Not [CP].Windows11.WinMode_Light
                ActionCenter.DarkMode = Not [CP].Windows11.WinMode_Light

                taskbar.Transparency = [CP].Windows11.Transparency
                start.Transparency = [CP].Windows11.Transparency
                ActionCenter.Transparency = [CP].Windows11.Transparency

                Select Case Not [CP].Windows11.WinMode_Light
                    Case True   ''''''''''Dark
                        ActionCenter.BackColorAlpha = 90

                        If ExplorerPatcher.IsAllowed Then
                            If My.EP.UseStart10 Then
                                start.BackColorAlpha = 185
                            Else
                                start.BackColorAlpha = 90
                            End If

                            If My.EP.UseTaskbar10 Then
                                taskbar.BackColorAlpha = 185
                                taskbar.BlurPower = 8
                            Else
                                taskbar.BackColorAlpha = 105
                                taskbar.BlurPower = 8
                            End If
                        Else
                            taskbar.BackColorAlpha = 105
                            taskbar.BlurPower = 8
                            start.BackColorAlpha = 90
                        End If

                        Select Case [CP].Windows11.ApplyAccentonTaskbar
                            Case ApplyAccentonTaskbar_Level.None
                                taskbar.BackColor = Color.FromArgb(28, 28, 28)
                                start.BackColor = Color.FromArgb(28, 28, 28)
                                ActionCenter.BackColor = Color.FromArgb(28, 28, 28)

                            Case ApplyAccentonTaskbar_Level.Taskbar_Start_AC
                                taskbar.BackColor = Color.FromArgb(taskbar.BackColor.A, [CP].Windows11.Color_Index5)

                                If ExplorerPatcher.IsAllowed And My.EP.UseStart10 Then
                                    start.BackColor = Color.FromArgb(start.BackColor.A, [CP].Windows11.Color_Index4)
                                Else
                                    start.BackColor = Color.FromArgb(start.BackColor.A, [CP].Windows11.Color_Index5)
                                End If

                                ActionCenter.BackColor = Color.FromArgb(ActionCenter.BackColor.A, [CP].Windows11.Color_Index5)

                            Case ApplyAccentonTaskbar_Level.Taskbar
                                taskbar.BackColor = Color.FromArgb(taskbar.BackColor.A, [CP].Windows11.Color_Index5)
                                start.BackColor = Color.FromArgb(28, 28, 28)
                                ActionCenter.BackColor = Color.FromArgb(28, 28, 28)

                        End Select

                        ActionCenter.ActionCenterButton_Normal = [CP].Windows11.Color_Index1
                        ActionCenter.ActionCenterButton_Hover = [CP].Windows11.Color_Index0
                        ActionCenter.ActionCenterButton_Pressed = [CP].Windows11.Color_Index2
                        taskbar.AppUnderline = [CP].Windows11.Color_Index1

                        setting_icon_preview.ForeColor = [CP].Windows11.Color_Index3
                        lnk_preview.ForeColor = [CP].Windows11.Color_Index0

                    Case False   ''''''''''Light
                        ActionCenter.BackColorAlpha = 180

                        If ExplorerPatcher.IsAllowed Then
                            If My.EP.UseStart10 Then
                                start.BackColorAlpha = 210
                            Else
                                start.BackColorAlpha = 180
                            End If

                            If My.EP.UseTaskbar10 Then
                                taskbar.BackColorAlpha = 210
                                taskbar.BlurPower = 8
                            Else
                                taskbar.BackColorAlpha = 180
                                taskbar.BlurPower = 8
                            End If
                        Else
                            taskbar.BlurPower = 8
                            taskbar.BackColorAlpha = 180
                            start.BackColorAlpha = 180
                        End If

                        Select Case [CP].Windows11.ApplyAccentonTaskbar
                            Case ApplyAccentonTaskbar_Level.None
                                taskbar.BackColor = Color.FromArgb(255, 255, 255)
                                start.BackColor = Color.FromArgb(255, 255, 255)
                                ActionCenter.BackColor = Color.FromArgb(255, 255, 255)

                            Case ApplyAccentonTaskbar_Level.Taskbar_Start_AC
                                taskbar.BackColor = Color.FromArgb(taskbar.BackColor.A, [CP].Windows11.Color_Index5)

                                If ExplorerPatcher.IsAllowed And My.EP.UseStart10 Then
                                    start.BackColor = Color.FromArgb(start.BackColor.A, [CP].Windows11.Color_Index4)
                                Else
                                    start.BackColor = Color.FromArgb(start.BackColor.A, [CP].Windows11.Color_Index0)
                                End If

                                ActionCenter.BackColor = Color.FromArgb(ActionCenter.BackColor.A, [CP].Windows11.Color_Index0)

                            Case ApplyAccentonTaskbar_Level.Taskbar
                                taskbar.BackColor = Color.FromArgb(taskbar.BackColor.A, [CP].Windows11.Color_Index5)
                                start.BackColor = Color.FromArgb(255, 255, 255)
                                ActionCenter.BackColor = Color.FromArgb(255, 255, 255)

                        End Select

                        ActionCenter.ActionCenterButton_Normal = [CP].Windows11.Color_Index4
                        ActionCenter.ActionCenterButton_Hover = [CP].Windows11.Color_Index5
                        ActionCenter.ActionCenterButton_Pressed = [CP].Windows11.Color_Index2

                        If ExplorerPatcher.IsAllowed And My.EP.UseTaskbar10 Then
                            taskbar.AppUnderline = [CP].Windows11.Color_Index1
                        Else
                            taskbar.AppUnderline = [CP].Windows11.Color_Index3
                        End If

                        setting_icon_preview.ForeColor = [CP].Windows11.Color_Index3
                        lnk_preview.ForeColor = [CP].Windows11.Color_Index5
                End Select

                RetroButton2.Image = My.Resources.Native11.Resize(18, 16)
#End Region

            Case WinVer.W10
#Region "Win10"
                If CP.WallpaperTone_W10.Enabled Then pnl_preview.BackgroundImage = MainFrm.GetTintedWallpaper(CP.WallpaperTone_W10)
                XenonWindow1.AccentColor_Enabled = [CP].Windows10.ApplyAccentonTitlebars
                XenonWindow2.AccentColor_Enabled = [CP].Windows10.ApplyAccentonTitlebars

                XenonWindow1.AccentColor_Active = [CP].Windows10.Titlebar_Active
                XenonWindow2.AccentColor_Active = [CP].Windows10.Titlebar_Active

                XenonWindow1.AccentColor_Inactive = [CP].Windows10.Titlebar_Inactive
                XenonWindow2.AccentColor_Inactive = [CP].Windows10.Titlebar_Inactive

                XenonWindow1.DarkMode = Not [CP].Windows10.AppMode_Light
                XenonWindow2.DarkMode = Not [CP].Windows10.AppMode_Light

                XenonWindow1.Shadow = [CP].WindowsEffects.WindowShadow
                XenonWindow2.Shadow = [CP].WindowsEffects.WindowShadow

                Label8.ForeColor = If([CP].Windows10.AppMode_Light, Color.Black, Color.White)

                start.DarkMode = Not [CP].Windows10.WinMode_Light
                taskbar.DarkMode = Not [CP].Windows10.WinMode_Light
                ActionCenter.DarkMode = Not [CP].Windows10.WinMode_Light

                taskbar.Transparency = [CP].Windows10.Transparency
                start.Transparency = [CP].Windows10.Transparency AndAlso [CP].Windows10.TB_Blur
                ActionCenter.Transparency = [CP].Windows10.Transparency AndAlso [CP].Windows10.TB_Blur

                If Not [CP].Windows10.TB_Blur Then
                    taskbar.BlurPower = 0
                Else
                    taskbar.BlurPower = If(Not [CP].Windows10.IncreaseTBTransparency, 8, 6)
                End If

                If [CP].Windows10.Transparency Then
                    If Not [CP].Windows10.WinMode_Light Then
                        taskbar.BackColorAlpha = If(Not CP.Windows10.IncreaseTBTransparency, 150, 75)
                        start.BackColorAlpha = 150
                        ActionCenter.BackColorAlpha = 150
                    Else
                        taskbar.BackColorAlpha = If(Not CP.Windows10.IncreaseTBTransparency, 200, 125)
                        start.BackColorAlpha = 200
                        ActionCenter.BackColorAlpha = 200
                    End If
                Else
                    taskbar.BackColorAlpha = 255
                    start.BackColorAlpha = 255
                    ActionCenter.BackColorAlpha = 255
                End If

                Select Case Not [CP].Windows10.WinMode_Light
                    Case True

                        If [CP].Windows10.Transparency Then
                            Select Case [CP].Windows10.ApplyAccentonTaskbar
                                Case ApplyAccentonTaskbar_Level.None
                                    taskbar.BackColor = Color.FromArgb(16, 16, 16)
                                    taskbar.StartColor = Color.FromArgb(150, 150, 150, 150)
                                    start.BackColor = Color.FromArgb(31, 31, 31)
                                    ActionCenter.BackColor = Color.FromArgb(31, 31, 31)

                                    taskbar.AppBackground = Color.FromArgb(150, 150, 150, 150)
                                    ActionCenter.LinkColor = [CP].Windows10.Color_Index0
                                    taskbar.AppUnderline = [CP].Windows10.Color_Index1
                                    setting_icon_preview.ForeColor = [CP].Windows10.Color_Index3
                                    lnk_preview.ForeColor = [CP].Windows10.Color_Index3
                                    ActionCenter.ActionCenterButton_Normal = [CP].Windows10.Color_Index3

                                Case ApplyAccentonTaskbar_Level.Taskbar
                                    taskbar.BackColor = [CP].Windows10.Color_Index6
                                    taskbar.StartColor = Color.FromArgb(0, 0, 0, 0)
                                    start.BackColor = Color.FromArgb(31, 31, 31)
                                    ActionCenter.BackColor = Color.FromArgb(31, 31, 31)

                                    taskbar.AppBackground = Color.FromArgb(150, [CP].Windows10.Color_Index3)
                                    ActionCenter.LinkColor = [CP].Windows10.Color_Index0
                                    taskbar.AppUnderline = [CP].Windows10.Color_Index1
                                    setting_icon_preview.ForeColor = [CP].Windows10.Color_Index3
                                    lnk_preview.ForeColor = [CP].Windows10.Color_Index3
                                    ActionCenter.ActionCenterButton_Normal = [CP].Windows10.Color_Index3

                                Case ApplyAccentonTaskbar_Level.Taskbar_Start_AC
                                    taskbar.BackColor = [CP].Windows10.Color_Index6
                                    taskbar.StartColor = Color.FromArgb(0, 0, 0, 0)
                                    start.BackColor = [CP].Windows10.Color_Index4
                                    ActionCenter.BackColor = [CP].Windows10.Color_Index4

                                    taskbar.AppBackground = Color.FromArgb(150, [CP].Windows10.Color_Index3)
                                    ActionCenter.LinkColor = [CP].Windows10.Color_Index0
                                    taskbar.AppUnderline = [CP].Windows10.Color_Index1
                                    setting_icon_preview.ForeColor = [CP].Windows10.Color_Index3
                                    lnk_preview.ForeColor = [CP].Windows10.Color_Index3
                                    ActionCenter.ActionCenterButton_Normal = [CP].Windows10.Color_Index3

                            End Select

                        Else
                            Select Case [CP].Windows10.ApplyAccentonTaskbar
                                Case ApplyAccentonTaskbar_Level.None
                                    taskbar.BackColor = Color.FromArgb(16, 16, 16)
                                    taskbar.StartColor = Color.FromArgb(31, 31, 31)
                                    start.BackColor = Color.FromArgb(31, 31, 31)
                                    ActionCenter.BackColor = Color.FromArgb(31, 31, 31)

                                Case ApplyAccentonTaskbar_Level.Taskbar
                                    taskbar.BackColor = [CP].Windows10.Color_Index5
                                    taskbar.StartColor = [CP].Windows10.Color_Index4
                                    start.BackColor = Color.FromArgb(31, 31, 31)
                                    ActionCenter.BackColor = Color.FromArgb(31, 31, 31)

                                Case ApplyAccentonTaskbar_Level.Taskbar_Start_AC
                                    taskbar.BackColor = [CP].Windows10.Color_Index5
                                    taskbar.StartColor = [CP].Windows10.Color_Index4
                                    start.BackColor = [CP].Windows10.Color_Index4
                                    ActionCenter.BackColor = [CP].Windows10.Color_Index4
                            End Select

                            If [CP].Windows10.ApplyAccentonTaskbar = ApplyAccentonTaskbar_Level.None Then
                                taskbar.AppBackground = Color.FromArgb(150, 100, 100, 100)
                            Else
                                taskbar.AppBackground = [CP].Windows10.Color_Index4
                            End If

                            ActionCenter.LinkColor = [CP].Windows10.Color_Index0
                            taskbar.AppUnderline = [CP].Windows10.Color_Index1
                            setting_icon_preview.ForeColor = [CP].Windows10.Color_Index3
                            lnk_preview.ForeColor = [CP].Windows10.Color_Index3
                            ActionCenter.ActionCenterButton_Normal = [CP].Windows10.Color_Index3

                        End If

                    Case False
                        If [CP].Windows10.Transparency Then

                            Select Case [CP].Windows10.ApplyAccentonTaskbar
                                Case ApplyAccentonTaskbar_Level.None
                                    taskbar.BackColor = Color.FromArgb(238, 238, 238)
                                    taskbar.StartColor = Color.Transparent
                                    start.BackColor = Color.FromArgb(228, 228, 228)
                                    ActionCenter.BackColor = Color.FromArgb(228, 228, 228)

                                    taskbar.AppBackground = Color.FromArgb(150, 238, 238, 238)
                                    ActionCenter.LinkColor = [CP].Windows10.Color_Index6
                                    taskbar.AppUnderline = [CP].Windows10.Color_Index3
                                    setting_icon_preview.ForeColor = [CP].Windows10.Color_Index3
                                    lnk_preview.ForeColor = [CP].Windows10.Color_Index3
                                    ActionCenter.ActionCenterButton_Normal = [CP].Windows10.Color_Index3

                                Case ApplyAccentonTaskbar_Level.Taskbar
                                    taskbar.BackColor = [CP].Windows10.Color_Index6
                                    taskbar.StartColor = Color.Transparent
                                    start.BackColor = Color.FromArgb(228, 228, 228)
                                    ActionCenter.BackColor = Color.FromArgb(228, 228, 228)

                                    taskbar.AppBackground = Color.FromArgb(150, [CP].Windows10.Color_Index3)
                                    ActionCenter.LinkColor = [CP].Windows10.Color_Index6
                                    taskbar.AppUnderline = [CP].Windows10.Color_Index1
                                    setting_icon_preview.ForeColor = [CP].Windows10.Color_Index3
                                    lnk_preview.ForeColor = [CP].Windows10.Color_Index3
                                    ActionCenter.ActionCenterButton_Normal = [CP].Windows10.Color_Index3

                                Case ApplyAccentonTaskbar_Level.Taskbar_Start_AC
                                    taskbar.BackColor = [CP].Windows10.Color_Index6
                                    taskbar.StartColor = Color.Transparent
                                    start.BackColor = [CP].Windows10.Color_Index4
                                    ActionCenter.BackColor = [CP].Windows10.Color_Index4

                                    taskbar.AppBackground = Color.FromArgb(150, [CP].Windows10.Color_Index3)
                                    ActionCenter.LinkColor = [CP].Windows10.Color_Index0
                                    taskbar.AppUnderline = [CP].Windows10.Color_Index1
                                    setting_icon_preview.ForeColor = [CP].Windows10.Color_Index3
                                    lnk_preview.ForeColor = [CP].Windows10.Color_Index3
                                    ActionCenter.ActionCenterButton_Normal = [CP].Windows10.Color_Index3

                            End Select

                        Else

                            Select Case [CP].Windows10.ApplyAccentonTaskbar
                                Case ApplyAccentonTaskbar_Level.None
                                    taskbar.BackColor = Color.FromArgb(238, 238, 238)
                                    taskbar.StartColor = Color.FromArgb(241, 241, 241)
                                    start.BackColor = Color.FromArgb(228, 228, 228)
                                    ActionCenter.BackColor = Color.FromArgb(228, 228, 228)

                                    taskbar.AppBackground = Color.FromArgb(252, 252, 252)
                                    ActionCenter.LinkColor = [CP].Windows10.Color_Index6
                                    taskbar.AppUnderline = [CP].Windows10.Color_Index3
                                    setting_icon_preview.ForeColor = [CP].Windows10.Color_Index3
                                    lnk_preview.ForeColor = [CP].Windows10.Color_Index3
                                    ActionCenter.ActionCenterButton_Normal = [CP].Windows10.Color_Index3

                                Case ApplyAccentonTaskbar_Level.Taskbar
                                    taskbar.BackColor = [CP].Windows10.Color_Index5
                                    taskbar.StartColor = [CP].Windows10.Color_Index4
                                    start.BackColor = Color.FromArgb(228, 228, 228)
                                    ActionCenter.BackColor = Color.FromArgb(228, 228, 228)

                                    taskbar.AppBackground = [CP].Windows10.Color_Index4
                                    ActionCenter.LinkColor = [CP].Windows10.Color_Index6
                                    taskbar.AppUnderline = [CP].Windows10.Color_Index1
                                    setting_icon_preview.ForeColor = [CP].Windows10.Color_Index3
                                    lnk_preview.ForeColor = [CP].Windows10.Color_Index3
                                    ActionCenter.ActionCenterButton_Normal = [CP].Windows10.Color_Index3

                                Case ApplyAccentonTaskbar_Level.Taskbar_Start_AC
                                    taskbar.BackColor = [CP].Windows10.Color_Index5
                                    taskbar.StartColor = [CP].Windows10.Color_Index4
                                    start.BackColor = [CP].Windows10.Color_Index4
                                    ActionCenter.BackColor = [CP].Windows10.Color_Index4


                                    taskbar.AppBackground = [CP].Windows10.Color_Index4
                                    ActionCenter.LinkColor = [CP].Windows10.Color_Index0
                                    taskbar.AppUnderline = [CP].Windows10.Color_Index1
                                    setting_icon_preview.ForeColor = [CP].Windows10.Color_Index3
                                    lnk_preview.ForeColor = [CP].Windows10.Color_Index3
                                    ActionCenter.ActionCenterButton_Normal = [CP].Windows10.Color_Index3

                            End Select

                        End If
                End Select

                RetroButton2.Image = My.Resources.Native10.Resize(18, 16)
#End Region

            Case WinVer.W8
#Region "Win8.1"
                If CP.WallpaperTone_W8.Enabled Then pnl_preview.BackgroundImage = MainFrm.GetTintedWallpaper(CP.WallpaperTone_W8)
                Select Case [CP].Windows8.Theme
                    Case CP.AeroTheme.Aero
                        XenonWindow1.Preview = XenonWindow.Preview_Enum.W8
                        XenonWindow2.Preview = XenonWindow.Preview_Enum.W8
                        taskbar.Transparency = True
                        taskbar.BackColorAlpha = 100
                    Case CP.AeroTheme.AeroLite
                        XenonWindow1.Preview = XenonWindow.Preview_Enum.W8Lite
                        XenonWindow2.Preview = XenonWindow.Preview_Enum.W8Lite
                        taskbar.Transparency = False
                        taskbar.BackColorAlpha = 255
                End Select

                XenonWindow1.AccentColor_Active = [CP].Windows8.ColorizationColor
                XenonWindow1.Win7ColorBal = [CP].Windows8.ColorizationColorBalance

                XenonWindow2.AccentColor_Active = [CP].Windows8.ColorizationColor
                XenonWindow2.Win7ColorBal = [CP].Windows8.ColorizationColorBalance

                taskbar.BackColor = [CP].Windows8.ColorizationColor
                taskbar.Win7ColorBal = [CP].Windows8.ColorizationColorBalance

                RetroButton2.Image = My.Resources.Native8.Resize(18, 16)
#End Region

            Case WinVer.W7
#Region "Win7"
                If CP.WallpaperTone_W7.Enabled Then pnl_preview.BackgroundImage = MainFrm.GetTintedWallpaper(CP.WallpaperTone_W7)

                XenonWindow1.Shadow = [CP].WindowsEffects.WindowShadow
                XenonWindow2.Shadow = [CP].WindowsEffects.WindowShadow

                Select Case [CP].Windows7.Theme
                    Case CP.AeroTheme.Aero
                        start.Transparency = True
                        taskbar.Transparency = True
                        With XenonWindow1
                            .Preview = XenonWindow.Preview_Enum.W7Aero
                            .Win7Alpha = [CP].Windows7.ColorizationBlurBalance
                            .Win7ColorBal = [CP].Windows7.ColorizationColorBalance
                            .Win7GlowBal = [CP].Windows7.ColorizationAfterglowBalance
                            .AccentColor_Active = [CP].Windows7.ColorizationColor
                            .AccentColor2_Active = [CP].Windows7.ColorizationAfterglow
                            .AccentColor_Inactive = [CP].Windows7.ColorizationColor
                            .AccentColor2_Inactive = [CP].Windows7.ColorizationAfterglow
                            .Win7Noise = [CP].Windows7.ColorizationGlassReflectionIntensity
                        End With
                        With XenonWindow2
                            .Preview = XenonWindow.Preview_Enum.W7Aero
                            .Win7Alpha = [CP].Windows7.ColorizationBlurBalance
                            .Win7ColorBal = [CP].Windows7.ColorizationColorBalance
                            .Win7GlowBal = [CP].Windows7.ColorizationAfterglowBalance
                            .AccentColor_Active = [CP].Windows7.ColorizationColor
                            .AccentColor2_Active = [CP].Windows7.ColorizationAfterglow
                            .AccentColor_Inactive = [CP].Windows7.ColorizationColor
                            .AccentColor2_Inactive = [CP].Windows7.ColorizationAfterglow
                            .Win7Noise = [CP].Windows7.ColorizationGlassReflectionIntensity
                        End With
                        With start
                            .BackColorAlpha = [CP].Windows7.ColorizationBlurBalance
                            .Win7ColorBal = [CP].Windows7.ColorizationColorBalance
                            .Win7GlowBal = [CP].Windows7.ColorizationAfterglowBalance
                            .BackColor = [CP].Windows7.ColorizationColor
                            .BackColor2 = [CP].Windows7.ColorizationAfterglow
                            .NoisePower = [CP].Windows7.ColorizationGlassReflectionIntensity
                        End With
                        With taskbar
                            .BackColorAlpha = [CP].Windows7.ColorizationBlurBalance
                            .Win7ColorBal = [CP].Windows7.ColorizationColorBalance
                            .Win7GlowBal = [CP].Windows7.ColorizationAfterglowBalance
                            .BackColor = [CP].Windows7.ColorizationColor
                            .BackColor2 = [CP].Windows7.ColorizationAfterglow
                            .NoisePower = [CP].Windows7.ColorizationGlassReflectionIntensity
                        End With


                    Case CP.AeroTheme.AeroOpaque
                        start.Transparency = True
                        taskbar.Transparency = True

                        With XenonWindow1
                            .Preview = XenonWindow.Preview_Enum.W7Opaque
                            .Win7Alpha = [CP].Windows7.ColorizationColorBalance
                            .AccentColor_Active = [CP].Windows7.ColorizationColor
                            .AccentColor_Inactive = [CP].Windows7.ColorizationColor
                            .Win7Noise = [CP].Windows7.ColorizationGlassReflectionIntensity
                        End With
                        With XenonWindow2
                            .Preview = XenonWindow.Preview_Enum.W7Opaque
                            .Win7Alpha = [CP].Windows7.ColorizationColorBalance
                            .AccentColor_Active = [CP].Windows7.ColorizationColor
                            .AccentColor_Inactive = [CP].Windows7.ColorizationColor
                            .Win7Noise = [CP].Windows7.ColorizationGlassReflectionIntensity
                        End With
                        With taskbar
                            .BackColorAlpha = [CP].Windows7.ColorizationColorBalance
                            .BackColor = [CP].Windows7.ColorizationColor
                            .BackColor2 = [CP].Windows7.ColorizationColor
                            .NoisePower = [CP].Windows7.ColorizationGlassReflectionIntensity
                        End With
                        With start
                            .BackColorAlpha = [CP].Windows7.ColorizationColorBalance
                            .BackColor = [CP].Windows7.ColorizationColor
                            .BackColor2 = [CP].Windows7.ColorizationColor
                            .NoisePower = [CP].Windows7.ColorizationGlassReflectionIntensity
                        End With


                    Case CP.AeroTheme.Basic
                        taskbar.BackColor = Color.FromArgb(166, 190, 218)
                        taskbar.BackColorAlpha = 100

                        start.BackColor = Color.FromArgb(166, 190, 218)
                        start.BackColorAlpha = 100

                        With XenonWindow1
                            .Preview = XenonWindow.Preview_Enum.W7Basic
                            .Win7Alpha = 100
                            .AccentColor_Active = Color.FromArgb(166, 190, 218)
                            .Win7Noise = 0
                        End With
                        With XenonWindow2
                            .Preview = XenonWindow.Preview_Enum.W7Basic
                            .Win7Alpha = 100
                            .AccentColor_Inactive = Color.FromArgb(166, 190, 218)
                            .Win7Noise = 0
                        End With

                        start.Transparency = False
                        start.NoisePower = 0
                        taskbar.Transparency = False
                        taskbar.NoisePower = 0

                        start.Refresh()
                        taskbar.Refresh()
                End Select

                ClassicTaskbar.Height = 44
                RetroButton2.Image = My.Resources.Native7.Resize(18, 16)
                RetroButton3.ImageAlign = Drawing.ContentAlignment.MiddleCenter
                RetroButton4.ImageAlign = Drawing.ContentAlignment.MiddleCenter
                RetroButton3.Width = 48
                RetroButton4.Width = 48
                RetroButton3.Text = ""
                RetroButton4.Text = ""
                RetroButton4.Left = RetroButton3.Right + 3
                RetroButton3.Font = New Font(CP.MetricsFonts.CaptionFont.Name, 8, RetroButton3.Font.Style)
                RetroButton4.Font = New Font(CP.MetricsFonts.CaptionFont.Name, 8, RetroButton4.Font.Style)
                RetroButton2.Font = New Font(CP.MetricsFonts.CaptionFont.Name, 8.5, RetroButton2.Font.Style)
                RetroButton3.HatchBrush = False
#End Region

            Case WinVer.WVista
#Region "WinVista"
                If CP.WallpaperTone_WVista.Enabled Then pnl_preview.BackgroundImage = MainFrm.GetTintedWallpaper(CP.WallpaperTone_WVista)

                XenonWindow1.Shadow = [CP].WindowsEffects.WindowShadow
                XenonWindow2.Shadow = [CP].WindowsEffects.WindowShadow

                Select Case [CP].WindowsVista.Theme
                    Case CP.AeroTheme.Aero
                        start.Transparency = True
                        taskbar.Transparency = True
                        With XenonWindow1
                            .Preview = XenonWindow.Preview_Enum.W7Aero
                            .Win7Alpha = ((255 - [CP].WindowsVista.Alpha) / 255) * 100
                            .Win7ColorBal = ((255 - [CP].WindowsVista.Alpha) / 255) * 100
                            '.Win7GlowBal = [CP].WindowsVista.ColorizationAfterglowBalance
                            .AccentColor_Active = Color.FromArgb([CP].WindowsVista.Alpha, [CP].WindowsVista.ColorizationColor)
                            .AccentColor2_Active = Color.FromArgb([CP].WindowsVista.Alpha, [CP].WindowsVista.ColorizationColor)
                            .AccentColor_Inactive = Color.FromArgb(100, [CP].WindowsVista.ColorizationColor)
                            .AccentColor2_Inactive = Color.FromArgb(100, [CP].WindowsVista.ColorizationColor)
                            .Win7Noise = 100
                        End With
                        With XenonWindow2
                            .Preview = XenonWindow.Preview_Enum.W7Aero
                            .Win7Alpha = ((255 - [CP].WindowsVista.Alpha) / 255) * 100
                            .Win7ColorBal = ((255 - [CP].WindowsVista.Alpha) / 255) * 100
                            '.Win7GlowBal = [CP].WindowsVista.ColorizationAfterglowBalance
                            .AccentColor_Active = [CP].WindowsVista.ColorizationColor
                            .AccentColor2_Active = [CP].WindowsVista.ColorizationColor
                            .AccentColor_Inactive = Color.FromArgb(100, [CP].WindowsVista.ColorizationColor)
                            .AccentColor2_Inactive = Color.FromArgb(100, [CP].WindowsVista.ColorizationColor)
                            .Win7Noise = 100
                        End With
                        With start
                            .BackColorAlpha = ([CP].WindowsVista.Alpha / 255) * 180
                            .Win7ColorBal = ((255 - [CP].WindowsVista.Alpha) / 255) * 100
                            '.Win7GlowBal = [CP].WindowsVista.ColorizationAfterglowBalance
                            .BackColor = [CP].WindowsVista.ColorizationColor
                            .BackColor2 = [CP].WindowsVista.ColorizationColor
                            .NoisePower = 100
                        End With
                        With taskbar
                            .BackColorAlpha = ([CP].WindowsVista.Alpha / 255) * 180
                            .Win7ColorBal = ((255 - [CP].WindowsVista.Alpha) / 255) * 100
                            '.Win7GlowBal = [CP].WindowsVista.ColorizationAfterglowBalance
                            .BackColor = [CP].WindowsVista.ColorizationColor
                            .BackColor2 = [CP].WindowsVista.ColorizationColor
                            .NoisePower = 100
                        End With


                    Case CP.AeroTheme.AeroOpaque
                        start.Transparency = True
                        taskbar.Transparency = True

                        With XenonWindow1
                            .Preview = XenonWindow.Preview_Enum.W7Opaque
                            .Win7Alpha = (([CP].WindowsVista.Alpha) / 255) * 100
                            .AccentColor_Active = [CP].WindowsVista.ColorizationColor
                            .AccentColor_Inactive = [CP].WindowsVista.ColorizationColor
                            .Win7Noise = 100
                        End With
                        With XenonWindow2
                            .Preview = XenonWindow.Preview_Enum.W7Opaque
                            .Win7Alpha = (([CP].WindowsVista.Alpha) / 255) * 100
                            .AccentColor_Active = [CP].WindowsVista.ColorizationColor
                            .AccentColor_Inactive = [CP].WindowsVista.ColorizationColor
                            .Win7Noise = 100
                        End With
                        With taskbar
                            .BackColorAlpha = ([CP].WindowsVista.Alpha / 255) * 200
                            .BackColor = [CP].WindowsVista.ColorizationColor
                            .BackColor2 = [CP].WindowsVista.ColorizationColor
                            .NoisePower = 100
                        End With
                        With start
                            .BackColorAlpha = ([CP].WindowsVista.Alpha / 255) * 200
                            .BackColor = [CP].WindowsVista.ColorizationColor
                            .BackColor2 = [CP].WindowsVista.ColorizationColor
                            .NoisePower = 100
                        End With


                    Case CP.AeroTheme.Basic
                        taskbar.BackColor = Color.FromArgb(166, 190, 218)
                        taskbar.BackColorAlpha = 100

                        start.BackColor = Color.FromArgb(166, 190, 218)
                        start.BackColorAlpha = 100

                        With XenonWindow1
                            .Preview = XenonWindow.Preview_Enum.W7Basic
                            .Win7Alpha = 100
                            .AccentColor_Active = Color.FromArgb(166, 190, 218)
                            .Win7Noise = 0
                        End With
                        With XenonWindow2
                            .Preview = XenonWindow.Preview_Enum.W7Basic
                            .Win7Alpha = 100
                            .AccentColor_Inactive = Color.FromArgb(166, 190, 218)
                            .Win7Noise = 0
                        End With

                        start.Transparency = False
                        start.NoisePower = 0
                        taskbar.Transparency = False
                        taskbar.NoisePower = 0

                        start.Refresh()
                        taskbar.Refresh()
                End Select

                ClassicTaskbar.Height = taskbar.Height
                RetroButton3.Image = My.Resources.ActiveApp_Taskbar.Resize(23, 23)
                RetroButton4.Image = My.Resources.InactiveApp_Taskbar.Resize(23, 23)
                RetroButton2.Image = My.Resources.Native7.Resize(18, 16)
                RetroButton3.ImageAlign = Drawing.ContentAlignment.BottomLeft
                RetroButton4.ImageAlign = Drawing.ContentAlignment.BottomLeft
                RetroButton3.Width = 140
                RetroButton4.Width = 140
                RetroButton3.Text = ClassicWindow1.TitlebarText
                RetroButton4.Text = ClassicWindow2.TitlebarText
                RetroButton4.Left = RetroButton3.Right + 3
                RetroButton3.Font = New Font(CP.MetricsFonts.CaptionFont.Name, 8, RetroButton3.Font.Style)
                RetroButton4.Font = New Font(CP.MetricsFonts.CaptionFont.Name, 8, RetroButton4.Font.Style)
                RetroButton2.Font = New Font(CP.MetricsFonts.CaptionFont.Name, 8.5, RetroButton2.Font.Style)
                RetroButton3.HatchBrush = True

#End Region

            Case WinVer.WXP
#Region "WinXP"
                If CP.WallpaperTone_WXP.Enabled Then pnl_preview.BackgroundImage = MainFrm.GetTintedWallpaper(CP.WallpaperTone_WXP)

                Select Case CP.WindowsXP.Theme
                    Case WinXPTheme.LunaBlue
                        My.VS = My.Application.appData & "\VisualStyles\Luna\luna.theme"
                        IO.File.WriteAllText(My.Application.appData & "\VisualStyles\Luna\luna.theme", String.Format("[VisualStyles]{1}Path={0}{1}ColorStyle=NormalColor{1}Size=NormalSize", My.Application.appData & "\VisualStyles\Luna\luna.msstyles", vbCrLf))
                        My.LunaRes = New Luna(Luna.ColorStyles.Blue)
                        My.resVS = New VisualStylesRes(My.VS)

                    Case WinXPTheme.LunaOliveGreen
                        My.VS = My.Application.appData & "\VisualStyles\Luna\luna.theme"
                        IO.File.WriteAllText(My.Application.appData & "\VisualStyles\Luna\luna.theme", String.Format("[VisualStyles]{1}Path={0}{1}ColorStyle=HomeStead{1}Size=NormalSize", My.Application.appData & "\VisualStyles\Luna\luna.msstyles", vbCrLf))
                        My.LunaRes = New Luna(Luna.ColorStyles.OliveGreen)
                        My.resVS = New VisualStylesRes(My.VS)

                    Case WinXPTheme.LunaSilver
                        My.VS = My.Application.appData & "\VisualStyles\Luna\luna.theme"
                        IO.File.WriteAllText(My.Application.appData & "\VisualStyles\Luna\luna.theme", String.Format("[VisualStyles]{1}Path={0}{1}ColorStyle=Metallic{1}Size=NormalSize", My.Application.appData & "\VisualStyles\Luna\luna.msstyles", vbCrLf))
                        My.LunaRes = New Luna(Luna.ColorStyles.Silver)
                        My.resVS = New VisualStylesRes(My.VS)

                    Case WinXPTheme.Custom
                        If IO.File.Exists(CP.WindowsXP.ThemeFile) Then
                            If IO.Path.GetExtension(CP.WindowsXP.ThemeFile) = ".theme" Then
                                My.VS = CP.WindowsXP.ThemeFile
                            ElseIf IO.Path.GetExtension(CP.WindowsXP.ThemeFile) = ".msstyles" Then
                                My.VS = My.Application.appData & "\VisualStyles\Luna\luna.theme"
                                IO.File.WriteAllText(My.Application.appData & "\VisualStyles\Luna\luna.theme", String.Format("[VisualStyles]{1}Path={0}{1}ColorStyle={2}{1}Size=NormalSize", CP.WindowsXP.ThemeFile, vbCrLf, CP.WindowsXP.ColorScheme))
                            End If
                        End If
                        My.LunaRes = New Luna(Luna.ColorStyles.Empty)
                        My.resVS = New VisualStylesRes(My.VS)

                    Case WinXPTheme.Classic
                        My.VS = My.Application.appData & "\VisualStyles\Luna\luna.theme"
                        IO.File.WriteAllText(My.Application.appData & "\VisualStyles\Luna\luna.theme", String.Format("[VisualStyles]{1}Path={0}{1}ColorStyle=NormalColor{1}Size=NormalSize", My.Application.appData & "\VisualStyles\Luna\luna.msstyles", vbCrLf))
                        My.LunaRes = New Luna(Luna.ColorStyles.Empty)
                        My.resVS = New VisualStylesRes(My.VS)

                End Select

                ClassicTaskbar.Height = taskbar.Height
                RetroButton3.Image = My.Resources.ActiveApp_Taskbar.Resize(23, 23)
                RetroButton4.Image = My.Resources.InactiveApp_Taskbar.Resize(23, 23)
                RetroButton2.Image = My.Resources.NativeXP.Resize(18, 16)
                RetroButton3.ImageAlign = Drawing.ContentAlignment.BottomLeft
                RetroButton4.ImageAlign = Drawing.ContentAlignment.BottomLeft
                RetroButton3.Width = 140
                RetroButton4.Width = 140
                RetroButton3.Text = ClassicWindow1.TitlebarText
                RetroButton4.Text = ClassicWindow2.TitlebarText
                RetroButton4.Left = RetroButton3.Right + 3
                RetroButton3.Font = New Font(CP.MetricsFonts.CaptionFont.Name, 8, RetroButton3.Font.Style)
                RetroButton4.Font = New Font(CP.MetricsFonts.CaptionFont.Name, 8, RetroButton4.Font.Style)
                RetroButton2.Font = New Font(CP.MetricsFonts.CaptionFont.Name, 8.5, RetroButton2.Font.Style)
                RetroButton3.HatchBrush = True

                start.Refresh()
                taskbar.Refresh()
#End Region
        End Select

        pnl_preview_classic.BackgroundImage = pnl_preview.BackgroundImage

        If MainFrm.PreviewConfig = WinVer.W10 Then taskbar.BlurPower = If(Not CP.Windows10.IncreaseTBTransparency, 12, 6)

        RetroButton3.Font = New Font(CP.MetricsFonts.CaptionFont.Name, 8, RetroButton3.Font.Style)
        RetroButton4.Font = New Font(CP.MetricsFonts.CaptionFont.Name, 8, RetroButton4.Font.Style)
        RetroButton2.Font = New Font(CP.MetricsFonts.CaptionFont.Name, 8.5, RetroButton2.Font.Style)

        MainFrm.ApplyMetrics([CP], XenonWindow1)
        MainFrm.ApplyMetrics([CP], XenonWindow2)

        ReValidateLivePreview(tabs_preview)
    End Sub

    Sub AdjustClassicPreview(CP As CP)
        SetToClassicWindow(ClassicWindow1, CP)
        SetToClassicWindow(ClassicWindow2, CP, False)

        SetClassicMetrics(ClassicWindow1, CP)
        SetClassicMetrics(ClassicWindow2, CP)
        SetToClassicButton(RetroButton2, CP)
        SetToClassicButton(RetroButton3, CP)
        SetToClassicButton(RetroButton4, CP)
        SetToClassicButton(RetroButton1, CP)
        SetToClassicRaisedPanel(ClassicTaskbar, CP)
    End Sub

    Sub SetClassicMetrics([Window] As RetroWindow, [CP] As CP)
        [Window].Metrics_BorderWidth = CP.MetricsFonts.BorderWidth
        [Window].Metrics_CaptionHeight = CP.MetricsFonts.CaptionHeight
        [Window].Metrics_CaptionWidth = CP.MetricsFonts.CaptionWidth
        [Window].Metrics_PaddedBorderWidth = CP.MetricsFonts.PaddedBorderWidth
        [Window].Font = CP.MetricsFonts.CaptionFont
        [Window].Refresh()
    End Sub

    Sub SetToClassicWindow([Window] As RetroWindow, [CP] As CP, Optional Active As Boolean = True)
        [Window].ButtonDkShadow = [CP].Win32.ButtonDkShadow
        [Window].BackColor = [CP].Win32.ButtonFace
        [Window].ButtonHilight = [CP].Win32.ButtonHilight
        [Window].ButtonLight = [CP].Win32.ButtonLight
        [Window].ButtonShadow = [CP].Win32.ButtonShadow
        [Window].ButtonText = [CP].Win32.ButtonText

        If Active Then
            [Window].ColorBorder = [CP].Win32.ActiveBorder
            [Window].ForeColor = [CP].Win32.TitleText
            [Window].Color1 = [CP].Win32.ActiveTitle
            [Window].Color2 = [CP].Win32.GradientActiveTitle
        Else
            [Window].ColorBorder = [CP].Win32.InactiveBorder
            [Window].ForeColor = [CP].Win32.InactiveTitleText
            [Window].Color1 = [CP].Win32.InactiveTitle
            [Window].Color2 = [CP].Win32.GradientInactiveTitle
        End If

        [Window].ColorGradient = [CP].Win32.EnableGradient
    End Sub

    Sub SetToClassicRaisedPanel([Panel] As RetroPanelRaised, [CP] As CP)
        [Panel].BackColor = [CP].Win32.ButtonFace
        [Panel].ButtonHilight = [CP].Win32.ButtonHilight
        [Panel].ButtonShadow = [CP].Win32.ButtonShadow
        [Panel].ForeColor = [CP].Win32.TitleText
    End Sub

    Sub SetToClassicButton([Button] As RetroButton, [CP] As CP)
        [Button].ButtonDkShadow = [CP].Win32.ButtonDkShadow
        [Button].ButtonHilight = [CP].Win32.ButtonHilight
        [Button].ButtonLight = [CP].Win32.ButtonLight
        [Button].ButtonShadow = [CP].Win32.ButtonShadow
        [Button].BackColor = [CP].Win32.ButtonFace
        [Button].ForeColor = [CP].Win32.ButtonText
        [Button].FocusRectWidth = [CP].WindowsEffects.FocusRectWidth
        [Button].FocusRectHeight = [CP].WindowsEffects.FocusRectHeight
        [Button].Refresh()
    End Sub



#End Region

    Private Sub Store_Load(sender As Object, e As EventArgs) Handles Me.Load
        DLLFunc.RemoveFormTitlebarTextAndIcon(Handle)
        ApplyDarkMode(Me)
        Refresh()
        ShowIcon = False

        Panel1.BackColor = Style.Colors.Back

        If Not IsFontInstalled("Segoe MDL2 Assets") Then
            setting_icon_preview.Font = New Font("Arial", 28, FontStyle.Regular)
            setting_icon_preview.Text = "♣"
        End If

        start.CopycatFrom(MainFrm.start)
        taskbar.CopycatFrom(MainFrm.taskbar)
        ActionCenter.CopycatFrom(MainFrm.ActionCenter)

        XenonWindow1.CopycatFrom(MainFrm.XenonWindow1)
        XenonWindow2.CopycatFrom(MainFrm.XenonWindow2)

        pnl_preview.BackgroundImage = My.Wallpaper
        pnl_preview_classic.BackgroundImage = pnl_preview.BackgroundImage

        FinishedLoadingInitialCPs = False

        'Prevent exception error of cross-thread
        container.CheckForIllegalCrossThreadCalls = False
    End Sub

    Private Sub Store_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Visible = False
        FilesFetcher.CancelAsync()
        RemoveAllStoreItems()
        TablessControl1.SelectedIndex = 0
    End Sub

    Sub RemoveAllStoreItems()
        For x = 0 To container.Controls.Count - 1

            If TypeOf container.Controls(0) Is StoreItem Then
                RemoveHandler DirectCast(container.Controls(0), StoreItem).Click, AddressOf StoreItem_Clicked
                RemoveHandler DirectCast(container.Controls(0), StoreItem).CPChanged, AddressOf StoreItem_CPChanged
                RemoveHandler DirectCast(container.Controls(0), StoreItem).MouseEnter, AddressOf StoreItem_MouseEnter
                RemoveHandler DirectCast(container.Controls(0), StoreItem).MouseLeave, AddressOf StoreItem_MouseLeave
            End If

            container.Controls(0).Dispose()
        Next
        container.Controls.Clear()
    End Sub

    Private Sub Store_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        ShowIcon = True
        RemoveAllStoreItems()
        FilesFetcher.RunWorkerAsync()
    End Sub

    Private Sub FilesFetcher_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles FilesFetcher.DoWork

        For Each file As String In Directory.GetFiles(My.Themes_Storage)
            If Path.GetExtension(file) = ".wpth" Then
                Try
                    Using CPx As New CP(CP.CP_Type.File, file)
                        CPList.Add(file, CPx)
                    End Using
                Catch ex As Exception

                End Try
            End If
        Next

        BeginInvoke(CType(Sub()
                              container.Visible = False
                          End Sub, MethodInvoker))

        For Each StoreItem In CPList
            Dim ctrl As New StoreItem With {
                .FileName = StoreItem.Key,
                .CP = StoreItem.Value,
                .MD5 = CalculateMD5(StoreItem.Key),
                .DoneByWinPaletter = True,
                .Size = New Size(w, h),
                .BackgroundImageLayout = ImageLayout.Stretch}

            AddHandler ctrl.Click, AddressOf StoreItem_Clicked
            AddHandler ctrl.CPChanged, AddressOf StoreItem_CPChanged
            AddHandler ctrl.MouseEnter, AddressOf StoreItem_MouseEnter
            AddHandler ctrl.MouseLeave, AddressOf StoreItem_MouseLeave
            AddHandler ctrl.MouseWheel, AddressOf StoreItem_MouseWheel

            BeginInvoke(CType(Sub()
                                  container.Controls.Add(ctrl)
                              End Sub, MethodInvoker))
        Next

        BeginInvoke(CType(Sub()
                              container.Visible = True
                          End Sub, MethodInvoker))

        CPList.Clear()

        FinishedLoadingInitialCPs = True

    End Sub

    Private Sub FilesFetcher_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles FilesFetcher.RunWorkerCompleted

    End Sub

    Private Function CalculateMD5(ByVal path As String) As String

        Using md5 As MD5 = MD5.Create()
            Dim txt = IO.File.ReadAllText(path)
            Dim hash = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(txt))
            Dim result = BitConverter.ToString(hash).Replace("-", "")
            Return result
        End Using

    End Function

    Public Sub StoreItem_CPChanged(sender As Object, e As EventArgs)
        If FinishedLoadingInitialCPs Then
            With DirectCast(sender, StoreItem)
                Adjust_Preview(.CP)
                AdjustClassicPreview(.CP)
                .BackgroundImage = tabs_preview.ToBitmap
                .Refresh()
            End With
        End If
    End Sub

    Public Sub StoreItem_Clicked(sender As Object, e As MouseEventArgs)

        Select Case e.Button
            Case MouseButtons.Right
                SwitchedByMouseWheel = False
                SwitchStoreItemPreview(DirectCast(sender, StoreItem))

            Case Else
                My.Animator.HideSync(TablessControl1)
                TablessControl1.SelectedIndex = 1
                With DirectCast(sender, StoreItem)
                    Visual.FadeColor(Panel1, "BackColor", Panel1.BackColor, .CP.StoreInfo.Color2, 10, 15)

                    Adjust_Preview(.CP)
                    AdjustClassicPreview(.CP)
                    preview.BackgroundImage = tabs_preview.ToBitmap

                    Label1.Text = .CP.Info.PaletteName
                    Label6.Text = .CP.Info.PaletteName
                    Label7.Text = .CP.Info.PaletteVersion
                    Label9.Text = .CP.Info.Author
                    XenonLinkLabel1.Text = .CP.Info.AuthorSocialMediaLink
                    Label12.Text = .MD5
                    Label10.Text = Math.Round(My.Computer.FileSystem.GetFileInfo(.FileName).Length / 1024, 2) & " " & My.Lang.KBSizeUnit
                    XenonTextBox1.Text = .CP.Info.PaletteDescription

                    Dim os_list As New List(Of String)
                    os_list.Clear()

                    If .CP.StoreInfo.DesignedFor_Win11 Then os_list.Add(My.Lang.OS_Win11)
                    If .CP.StoreInfo.DesignedFor_Win10 Then os_list.Add(My.Lang.OS_Win10)
                    If .CP.StoreInfo.DesignedFor_Win8 Then os_list.Add(My.Lang.OS_Win8)
                    If .CP.StoreInfo.DesignedFor_Win7 Then os_list.Add(My.Lang.OS_Win7)
                    If .CP.StoreInfo.DesignedFor_WinVista Then os_list.Add(My.Lang.OS_WinVista)
                    If .CP.StoreInfo.DesignedFor_WinXP Then os_list.Add(My.Lang.OS_WinXP)

                    Dim os_format As String = ""

                    If os_list.Count = 1 Then
                        os_format = os_list(0)

                    ElseIf os_list.Count = 2 Then
                        os_format = os_list(0) & " && " & os_list(1)

                    ElseIf os_list.Count > 2 Then

                        For i = 0 To os_list.Count - 3
                            os_format &= os_list(i) & ", "
                        Next

                        os_format &= os_list(os_list.Count - 2) & " && " & os_list(os_list.Count - 1)
                    End If

                    Label22.Text = os_format

                    'XenonLinkLabel2 = .Link

                End With



                My.Animator.ShowSync(TablessControl1)
        End Select
    End Sub

    Public Sub StoreItem_MouseEnter(sender As Object, e As EventArgs)
        SwitchedByMouseWheel = False
        hoveredItem = DirectCast(sender, StoreItem)
        Visual.FadeColor(Panel1, "BackColor", Panel1.BackColor, hoveredItem.CP.StoreInfo.Color1, 10, 15)
        Timer1.Enabled = True
        Timer1.Start()
    End Sub

    Public Sub StoreItem_MouseLeave(sender As Object, e As EventArgs)
        elapsedSeconds = 0
        Timer1.Enabled = False
        Timer1.Stop()
        SwitchedByMouseWheel = False
        If hoveredItem.BackgroundImage IsNot Nothing Then SwitchStoreItemPreview(hoveredItem)
        If TablessControl1.SelectedIndex = 0 Then Visual.FadeColor(Panel1, "BackColor", Panel1.BackColor, Style.Colors.Back, 10, 15)
    End Sub

    Public Sub StoreItem_MouseWheel(sender As Object, e As MouseEventArgs)
        If ModernOrClassic = 0 Then ModernOrClassic = 1 Else ModernOrClassic = 0
        SwitchedByMouseWheel = True
        SwitchStoreItemPreview(hoveredItem)
    End Sub

    Sub SwitchStoreItemPreview(St_itm As StoreItem)
        If St_itm.BackgroundImage Is Nothing Or SwitchedByMouseWheel Then
            Adjust_Preview(St_itm.CP)
            AdjustClassicPreview(St_itm.CP)
            St_itm.BackgroundImage = tabs_preview.ToBitmap

        ElseIf Not SwitchedByMouseWheel Then
            St_itm.BackgroundImage = Nothing

        End If

        St_itm.Refresh()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        If elapsedSeconds < SwitchAfterSecs - 1 Then
            elapsedSeconds += 1
        Else
            elapsedSeconds = 0
            SwitchedByMouseWheel = False
            If hoveredItem.BackgroundImage Is Nothing Then SwitchStoreItemPreview(hoveredItem)
            Timer1.Enabled = False
            Timer1.Stop()
        End If
    End Sub

    Private Sub XenonButton1_Click(sender As Object, e As EventArgs) Handles XenonButton1.Click

        My.Animator.HideSync(TablessControl1)

        TablessControl1.SelectedIndex = 0
        Label1.Text = Text
        My.Animator.ShowSync(TablessControl1)

        Visual.FadeColor(Panel1, "BackColor", Panel1.BackColor, Style.Colors.Back, 10, 15)
    End Sub

    Private Sub Panel1_BackColorChanged(sender As Object, e As EventArgs) Handles Panel1.BackColorChanged
        DrawCustomTitlebar(Panel1.BackColor, Panel1.BackColor)
    End Sub

    Private Sub TablessControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TablessControl1.SelectedIndexChanged
        XenonButton1.Visible = TablessControl1.SelectedIndex <> 0
    End Sub

    Private Sub RetroButton1_Click(sender As Object, e As EventArgs) Handles RetroButton1.Click
        tabs_preview.SelectedIndex = 1
        preview.BackgroundImage = tabs_preview.ToBitmap
    End Sub

    Private Sub XenonButton3_Click(sender As Object, e As EventArgs) Handles XenonButton3.Click
        tabs_preview.SelectedIndex = 0
        preview.BackgroundImage = tabs_preview.ToBitmap
    End Sub

    Private Sub XenonLinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles XenonLinkLabel1.LinkClicked
        Try
            If (Uri.IsWellFormedUriString(XenonLinkLabel1.Text, UriKind.Absolute)) Then Process.Start(XenonLinkLabel1.Text)
        Catch
        End Try
    End Sub

    Private Sub XenonLinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles XenonLinkLabel2.LinkClicked
        Try
            If (Uri.IsWellFormedUriString(XenonLinkLabel2.Text, UriKind.Absolute)) Then Process.Start(XenonLinkLabel2.Text)
        Catch
        End Try
    End Sub
End Class