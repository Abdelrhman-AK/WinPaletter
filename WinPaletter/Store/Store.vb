Imports System.IO
Imports WinPaletter.XenonCore
Imports System.Security.Cryptography
Imports WinPaletter.MainFrm
Imports WinPaletter.CP
Imports WinPaletter.NativeMethods
Imports Devcorp.Controls.VisualStyles
Imports System.Text
Imports Newtonsoft.Json.Linq

Public Class Store
    Private FinishedLoadingInitialCPs As Boolean
    Dim CPList As New Dictionary(Of String, CP)
    Dim w As Integer = 528 * 0.6
    Dim h As Integer = 297 * 0.6

    Private elapsedSeconds As Integer = 0
    Private apply_elapsedSecs As Integer = 0

    Private SwitchAfterSecs As Integer = 1

    Private hoveredItem As StoreItem
    Public selectedItem As StoreItem
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
        SetToClassicRaisedPanel(ClassicTaskbar, CP)
    End Sub

    Private Sub Menu_Window_SizeChanged(sender As Object, e As EventArgs) Handles Menu_Window.SizeChanged, Menu_Window.LocationChanged
        RetroShadow1.Size = Menu_Window.Size
        RetroShadow1.Location = Menu_Window.Location + New Point(6, 5)

        Dim b As New Bitmap(RetroShadow1.Width, RetroShadow1.Height)
        Dim g As Graphics = Graphics.FromImage(b)
        g.DrawGlow(New Rectangle(5, 5, b.Width - 10 - 1, b.Height - 10 - 1), Color.FromArgb(128, 0, 0, 0))
        g.Save()
        RetroShadow1.Image = b
        g.Dispose()

        RetroShadow1.BringToFront()
        Menu_Window.BringToFront()
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
        [Button].Font = [CP].MetricsFonts.CaptionFont
        [Button].Refresh()
    End Sub

    Sub SetClassicMetrics(CP As CP)
        Try
            If MainFrm.PreviewConfig = WinVer.WXP AndAlso MainFrm.WXP_VS_ReplaceMetrics.Checked And CP.WindowsXP.Theme <> WinXPTheme.Classic Then
                If IO.File.Exists(My.VS) And Not String.IsNullOrEmpty(My.VS) Then
                    Dim vs As New VisualStyleFile(My.VS)
                    CP.MetricsFonts.Overwrite_Metrics(vs.Metrics)
                End If

                If IO.File.Exists(My.VS) And Not String.IsNullOrEmpty(My.VS) Then
                    Dim vs As New VisualStyleFile(My.VS)
                    CP.MetricsFonts.Overwrite_Fonts(vs.Metrics)
                End If
            End If
        Catch
        End Try

        RetroPanel2.Width = CP.MetricsFonts.ScrollWidth
        menucontainer0.Height = CP.MetricsFonts.MenuHeight

        menucontainer0.Height = Math.Max(CP.MetricsFonts.MenuHeight, Metrics_Fonts.GetTitleTextHeight(CP.MetricsFonts.MenuFont))

        RetroLabel1.Font = CP.MetricsFonts.MenuFont
        RetroLabel2.Font = CP.MetricsFonts.MenuFont
        RetroLabel3.Font = CP.MetricsFonts.MenuFont

        RetroLabel9.Font = CP.MetricsFonts.MenuFont
        RetroLabel5.Font = CP.MetricsFonts.MenuFont
        RetroLabel6.Font = CP.MetricsFonts.MenuFont

        menucontainer1.Height = Metrics_Fonts.GetTitleTextHeight(CP.MetricsFonts.MenuFont) + 3
        highlight.Height = menucontainer1.Height + 1
        menucontainer3.Height = menucontainer1.Height + 1
        Menu_Window.Height = menucontainer1.Height + highlight.Height + menucontainer3.Height + Menu_Window.Padding.Top + Menu_Window.Padding.Bottom

        RetroLabel4.Font = CP.MetricsFonts.MessageFont

        RetroLabel1.Width = RetroLabel1.Text.Measure(CP.MetricsFonts.MenuFont).Width + 5
        RetroLabel2.Width = RetroLabel2.Text.Measure(CP.MetricsFonts.MenuFont).Width + 5
        RetroPanel1.Width = RetroLabel3.Text.Measure(CP.MetricsFonts.MenuFont).Width + 5 + RetroPanel1.Padding.Left + RetroPanel1.Padding.Right

        Dim TitleTextH, TitleTextH_9, TitleTextH_Sum As Integer
        TitleTextH = "ABCabc0123xYz.#".Measure(CP.MetricsFonts.CaptionFont).Height
        TitleTextH_9 = "ABCabc0123xYz.#".Measure(New Font(CP.MetricsFonts.CaptionFont.Name, 9, Font.Style)).Height
        TitleTextH_Sum = Math.Max(0, TitleTextH - TitleTextH_9 - 5)

        Dim iP As Integer = 3 + CP.MetricsFonts.PaddedBorderWidth + CP.MetricsFonts.BorderWidth
        Dim iT As Integer = 4 + CP.MetricsFonts.PaddedBorderWidth + CP.MetricsFonts.BorderWidth + CP.MetricsFonts.CaptionHeight + TitleTextH_Sum
        Dim _Padding As New Windows.Forms.Padding(iP, iT, iP, iP)

        For Each RW As RetroWindow In ClassicColorsPreview.Controls.OfType(Of RetroWindow)
            If Not RW.UseItAsMenu Then
                RW.Metrics_BorderWidth = CP.MetricsFonts.BorderWidth
                RW.Metrics_CaptionHeight = CP.MetricsFonts.CaptionHeight
                RW.Metrics_CaptionWidth = CP.MetricsFonts.CaptionWidth
                RW.Metrics_PaddedBorderWidth = CP.MetricsFonts.PaddedBorderWidth
                RW.Font = CP.MetricsFonts.CaptionFont

                RW.Padding = _Padding
            End If
        Next

        RetroWindow3.Height = 85 + CP.MetricsFonts.PaddedBorderWidth + CP.MetricsFonts.BorderWidth + RetroWindow3.GetTitleTextHeight
        RetroWindow2.Height = 120 + CP.MetricsFonts.PaddedBorderWidth + CP.MetricsFonts.BorderWidth + RetroWindow2.GetTitleTextHeight + CP.MetricsFonts.MenuHeight


        Menu_Window.Top = RetroWindow2.Top + menucontainer0.Top + menucontainer0.Height
        Menu_Window.Left = Math.Min(RetroWindow2.Left + menucontainer0.Left + RetroPanel1.Left + +3, RetroWindow2.Right - CP.MetricsFonts.PaddedBorderWidth - CP.MetricsFonts.BorderWidth)

        RetroWindow3.Top = RetroWindow2.Top + RetroTextBox1.Top + RetroTextBox1.Font.Height + 10
        RetroWindow3.Left = RetroWindow2.Left + RetroTextBox1.Left + 15

        RetroLabel13.Top = RetroWindow4.Top + RetroWindow4.Metrics_CaptionHeight + 2
        RetroLabel13.Left = RetroWindow4.Right - RetroWindow4.Metrics_CaptionWidth - 2

        RetroShadow1.Visible = CP.WindowsEffects.WindowShadow
    End Sub

    Sub ApplyRetroPreview([CP] As CP)
        Try
            If MainFrm.PreviewConfig = WinVer.WXP AndAlso MainFrm.WXP_VS_ReplaceColors.Checked And CP.WindowsXP.Theme <> WinXPTheme.Classic Then
                If IO.File.Exists(My.VS) And Not String.IsNullOrEmpty(My.VS) Then
                    Dim vs As New VisualStyleFile(My.VS)
                    CP.Win32.Load(Structures.Win32UI.Method.VisualStyles, vs.Metrics)
                End If
            End If
        Catch
        End Try

        RetroWindow1.ColorGradient = [CP].Win32.EnableGradient
        RetroWindow2.ColorGradient = [CP].Win32.EnableGradient
        RetroWindow3.ColorGradient = [CP].Win32.EnableGradient
        RetroWindow4.ColorGradient = [CP].Win32.EnableGradient

        Dim c As Color
        c = [CP].Win32.ActiveTitle
        RetroWindow2.Color1 = c
        RetroWindow3.Color1 = c
        RetroWindow4.Color1 = c

        c = [CP].Win32.GradientActiveTitle
        RetroWindow2.Color2 = c
        RetroWindow3.Color2 = c
        RetroWindow4.Color2 = c

        c = [CP].Win32.TitleText
        RetroWindow2.ForeColor = c
        RetroWindow3.ForeColor = c
        RetroWindow4.ForeColor = c

        c = [CP].Win32.InactiveTitle
        RetroWindow1.Color1 = c

        c = [CP].Win32.GradientInactiveTitle
        RetroWindow1.Color2 = c

        c = [CP].Win32.InactiveTitleText
        RetroWindow1.ForeColor = c

        c = [CP].Win32.ActiveBorder
        RetroWindow2.ColorBorder = c
        RetroWindow3.ColorBorder = c
        RetroWindow4.ColorBorder = c

        c = [CP].Win32.InactiveBorder
        RetroWindow1.ColorBorder = c

        c = [CP].Win32.WindowFrame
        For Each RW As RetroWindow In ClassicColorsPreview.Controls.OfType(Of RetroWindow)
            For Each RB As RetroButton In RW.Controls.OfType(Of RetroButton)
                RB.WindowFrame = c
            Next
        Next
        For Each RB As RetroButton In RetroPanel2.Controls.OfType(Of RetroButton)
            RB.WindowFrame = c
        Next

        c = [CP].Win32.ButtonFace
        For Each RW As RetroWindow In ClassicColorsPreview.Controls.OfType(Of RetroWindow)
            If RW IsNot Menu Then RW.BackColor = c
            For Each RB As RetroButton In RW.Controls.OfType(Of RetroButton)
                RB.BackColor = c
            Next
        Next
        For Each RB As RetroButton In RetroPanel2.Controls.OfType(Of RetroButton)
            RB.BackColor = c
        Next
        RetroPanel2.BackColor = c
        Menu_Window.ButtonFace = c

        c = [CP].Win32.ButtonDkShadow
        For Each RW As RetroWindow In ClassicColorsPreview.Controls.OfType(Of RetroWindow)
            RW.ButtonDkShadow = c
            For Each RB As RetroButton In RW.Controls.OfType(Of RetroButton)
                RB.ButtonDkShadow = c
            Next
        Next
        For Each RB As RetroButton In RetroPanel2.Controls.OfType(Of RetroButton)
            RB.ButtonDkShadow = c
        Next
        RetroTextBox1.ButtonDkShadow = c
        Menu_Window.ButtonDkShadow = c

        c = [CP].Win32.ButtonHilight
        For Each RW As RetroWindow In ClassicColorsPreview.Controls.OfType(Of RetroWindow)
            RW.ButtonHilight = c
            For Each RB As RetroButton In RW.Controls.OfType(Of RetroButton)
                RB.ButtonHilight = c
            Next
        Next
        For Each RB As RetroButton In RetroPanel2.Controls.OfType(Of RetroButton)
            RB.ButtonHilight = c
        Next
        For Each RB As RetroPanelRaised In ClassicColorsPreview.Controls.OfType(Of RetroPanelRaised)
            RB.ButtonHilight = c
        Next
        RetroTextBox1.ButtonHilight = c
        RetroPanel1.ButtonHilight = c
        RetroPanel2.ButtonHilight = c
        Menu_Window.ButtonHilight = c

        c = [CP].Win32.ButtonLight
        For Each RW As RetroWindow In ClassicColorsPreview.Controls.OfType(Of RetroWindow)
            RW.ButtonLight = c
            For Each RB As RetroButton In RW.Controls.OfType(Of RetroButton)
                RB.ButtonLight = c
            Next
        Next
        For Each RB As RetroButton In RetroPanel2.Controls.OfType(Of RetroButton)
            RB.ButtonLight = c
        Next
        RetroTextBox1.ButtonLight = c
        Menu_Window.ButtonLight = c

        c = [CP].Win32.ButtonShadow
        For Each RW As RetroWindow In ClassicColorsPreview.Controls.OfType(Of RetroWindow)
            RW.ButtonShadow = c
            For Each RB As RetroButton In RW.Controls.OfType(Of RetroButton)
                RB.ButtonShadow = c
            Next
        Next
        For Each RB As RetroButton In RetroPanel2.Controls.OfType(Of RetroButton)
            RB.ButtonShadow = c
        Next
        For Each RB As RetroPanelRaised In ClassicColorsPreview.Controls.OfType(Of RetroPanelRaised)
            RB.ButtonShadow = c
        Next
        RetroTextBox1.ButtonShadow = c
        RetroPanel1.ButtonShadow = c
        RetroTextBox1.Invalidate()
        Menu_Window.ButtonShadow = c

        c = [CP].Win32.ButtonText
        For Each RW As RetroWindow In ClassicColorsPreview.Controls.OfType(Of RetroWindow)
            RW.ButtonText = c
            For Each RB As RetroButton In RW.Controls.OfType(Of RetroButton)
                RB.ForeColor = c
            Next
        Next
        For Each RB As RetroButton In RetroPanel2.Controls.OfType(Of RetroButton)
            RB.ForeColor = c
        Next

        c = [CP].Win32.AppWorkspace
        programcontainer.BackColor = c

        c = [CP].Win32.Background
        ClassicColorsPreview.BackColor = c

        c = [CP].Win32.Menu
        Menu_Window.BackColor = c
        RetroPanel1.BackColor = c
        Menu_Window.Invalidate()

        c = [CP].Win32.MenuBar
        menucontainer0.BackColor = c

        c = [CP].Win32.Hilight
        highlight.BackColor = c

        c = [CP].Win32.MenuHilight
        menuhilight.BackColor = c

        c = [CP].Win32.MenuText
        RetroLabel6.ForeColor = c
        RetroLabel1.ForeColor = c

        c = [CP].Win32.HilightText
        RetroLabel5.ForeColor = c

        c = [CP].Win32.GrayText
        RetroLabel2.ForeColor = c
        RetroLabel9.ForeColor = c

        c = [CP].Win32.Window
        RetroTextBox1.BackColor = c

        c = [CP].Win32.WindowText
        RetroTextBox1.ForeColor = c
        RetroLabel4.ForeColor = c

        c = [CP].Win32.InfoWindow
        RetroLabel13.BackColor = c

        c = [CP].Win32.InfoText
        RetroLabel13.ForeColor = c

        For Each RW As RetroWindow In ClassicColorsPreview.Controls.OfType(Of RetroWindow)
            RW.Invalidate()
            For Each RB As RetroButton In RW.Controls.OfType(Of RetroButton)
                RB.Invalidate()
            Next
        Next

        For Each RB As RetroButton In RetroPanel2.Controls.OfType(Of RetroButton)
            RB.Invalidate()
        Next

        Refresh17BitPreference([CP])

        RetroShadow1.Refresh()
    End Sub

    Sub Refresh17BitPreference([CP] As CP)

        If [CP].Win32.EnableTheming Then
            'Theming Enabled (Menus Has colors and borders)
            Menu_Window.Flat = True
            RetroPanel1.Flat = True
            menuhilight.BackColor = [CP].Win32.MenuHilight  'Filling of selected item
            highlight.BackColor = [CP].Win32.Hilight 'Outer Border of selected item

            RetroPanel1.BackColor = [CP].Win32.MenuHilight
            RetroPanel1.ButtonShadow = [CP].Win32.Hilight

            menucontainer0.BackColor = [CP].Win32.MenuBar
            RetroLabel3.ForeColor = [CP].Win32.HilightText
        Else
            'Theming Disabled (Menus are retro 3d)
            Menu_Window.Flat = False
            RetroPanel1.Flat = False
            menuhilight.BackColor = [CP].Win32.Hilight 'Both will have same color
            highlight.BackColor = [CP].Win32.Hilight 'Both will have same color
            RetroPanel1.BackColor = [CP].Win32.Menu
            RetroPanel1.ButtonShadow = [CP].Win32.ButtonShadow
            menucontainer0.BackColor = [CP].Win32.Menu
            RetroLabel3.ForeColor = [CP].Win32.MenuText

        End If

        Menu_Window.Invalidate()
        RetroPanel1.Invalidate()
        menuhilight.Invalidate()
        highlight.Invalidate()

    End Sub

#End Region

    Private Sub Store_Load(sender As Object, e As EventArgs) Handles Me.Load
        Panel1.BackColor = Style.Colors.Back

        DLLFunc.RemoveFormTitlebarTextAndIcon(Handle)
        ApplyDarkMode(Me)
        Refresh()
        ShowIcon = False

        If Not IsFontInstalled("Segoe MDL2 Assets") Then
            setting_icon_preview.Font = New Font("Arial", 28, FontStyle.Regular)
            setting_icon_preview.Text = "♣"
        End If

        start.CopycatFrom(MainFrm.start)
        taskbar.CopycatFrom(MainFrm.taskbar)
        ActionCenter.CopycatFrom(MainFrm.ActionCenter)

        XenonWindow1.CopycatFrom(MainFrm.XenonWindow1)
        XenonWindow2.CopycatFrom(MainFrm.XenonWindow2)

        MainFrm.MakeItDoubleBuffered(Me)
        MainFrm.MakeItDoubleBuffered(Panel1)
        MainFrm.MakeItDoubleBuffered(TablessControl1)
        MainFrm.MakeItDoubleBuffered(TreeView1)
        MainFrm.MakeItDoubleBuffered(TreeView2)
        TreeView1.ImageList = My.Notifications_IL
        XenonButton4.Image = MainFrm.apply_btn.Image
        XenonButton19.Image = MainFrm.XenonButton19.Image

        WXP_Alert2.Text = MainFrm.WXP_Alert2.Text
        WXP_Alert2.Size = WXP_Alert2.Parent.Size - New Size(40, 40)
        WXP_Alert2.Location = New Point(20, 20)

        pnl_preview.BackgroundImage = My.Wallpaper
        pnl_preview_classic.BackgroundImage = pnl_preview.BackgroundImage

        FinishedLoadingInitialCPs = False

        Label12.Font = My.Application.ConsoleFontMedium
        Label10.Font = My.Application.ConsoleFontMedium
        Label7.Font = My.Application.ConsoleFontMedium
        XenonTextBox1.Font = My.Application.ConsoleFontLarge

        'Prevent exception error of cross-thread
        container.CheckForIllegalCrossThreadCalls = False
    End Sub

    Private Sub Store_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Visible = False
        FilesFetcher.CancelAsync()
        RemoveAllStoreItems(container)
        TablessControl1.SelectedIndex = 0
    End Sub

    Sub RemoveAllStoreItems(Container As FlowLayoutPanel)
        For x = 0 To Container.Controls.Count - 1

            If TypeOf Container.Controls(0) Is StoreItem Then
                RemoveHandler DirectCast(Container.Controls(0), StoreItem).Click, AddressOf StoreItem_Clicked
                RemoveHandler DirectCast(Container.Controls(0), StoreItem).CPChanged, AddressOf StoreItem_CPChanged
                RemoveHandler DirectCast(Container.Controls(0), StoreItem).MouseEnter, AddressOf StoreItem_MouseEnter
                RemoveHandler DirectCast(Container.Controls(0), StoreItem).MouseLeave, AddressOf StoreItem_MouseLeave
            End If

            Container.Controls(0).Dispose()
        Next
        Container.Controls.Clear()
    End Sub

    Private Sub Store_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        ShowIcon = True
        RemoveAllStoreItems(container)
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
                .Size = New Size(w, h)}

            If ctrl.DoneByWinPaletter Then ctrl.CP.Info.Author = My.Application.Info.ProductName

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
                selectedItem = DirectCast(sender, StoreItem)

                With DirectCast(sender, StoreItem)

                    Adjust_Preview(.CP)
                    AdjustClassicPreview(.CP)
                    ApplyRetroPreview(.CP)
                    SetClassicMetrics(.CP)

                    Label1.Text = .CP.Info.PaletteName & " - " & My.Lang.By & " " & .CP.Info.Author
                    Label6.Text = .CP.Info.PaletteName
                    Label7.Text = .CP.Info.PaletteVersion
                    Label9.Text = .CP.Info.Author
                    Label12.Text = .MD5
                    Label10.Text = Math.Round(My.Computer.FileSystem.GetFileInfo(.FileName).Length / 1024, 2) & " " & My.Lang.KBSizeUnit
                    XenonLinkLabel1.Text = If(Not String.IsNullOrWhiteSpace(.CP.Info.AuthorSocialMediaLink), .CP.Info.AuthorSocialMediaLink, "There is no included data")
                    XenonLinkLabel2.Text = If(Not String.IsNullOrWhiteSpace(.URL), .URL, "There is no included data")
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

                    GetResources()
                End With

                Panel1.BackColor = DirectCast(sender, StoreItem).CP.StoreInfo.Color2
                My.Animator.ShowSync(TablessControl1)
        End Select
    End Sub

    Sub GetResources()
        TreeView2.Nodes.Clear()
        Dim filesList As New List(Of String) : filesList.Clear()
        Dim fontsList As New List(Of String) : fontsList.Clear()

        Dim x As String

        x = selectedItem.CP.WindowsXP.ThemeFile
        If Not String.IsNullOrWhiteSpace(x) And Not File.Exists(x) And Not filesList.Contains(x) And My.WXP Then filesList.Add(x)

        x = selectedItem.CP.LogonUI7.ImagePath
        If Not String.IsNullOrWhiteSpace(x) And Not File.Exists(x) And Not filesList.Contains(x) And (My.W7 Or My.W8) Then filesList.Add(x)

        If My.W11 Or My.W10 Then
            x = selectedItem.CP.Terminal.DefaultProf.BackgroundImage
            If Not String.IsNullOrWhiteSpace(x) And Not File.Exists(x) And Not filesList.Contains(x) Then filesList.Add(x)

            x = selectedItem.CP.Terminal.DefaultProf.Icon
            If Not String.IsNullOrWhiteSpace(x) And Not File.Exists(x) And Not filesList.Contains(x) Then filesList.Add(x)

            x = selectedItem.CP.TerminalPreview.DefaultProf.BackgroundImage
            If Not String.IsNullOrWhiteSpace(x) And Not File.Exists(x) And Not filesList.Contains(x) Then filesList.Add(x)

            x = selectedItem.CP.TerminalPreview.DefaultProf.Icon
            If Not String.IsNullOrWhiteSpace(x) And Not File.Exists(x) And Not filesList.Contains(x) Then filesList.Add(x)

            For Each i In selectedItem.CP.Terminal.Profiles
                x = i.BackgroundImage
                If Not String.IsNullOrWhiteSpace(x) And Not File.Exists(x) And Not filesList.Contains(x) Then filesList.Add(x)

                x = i.Icon
                If Not String.IsNullOrWhiteSpace(x) And Not x.Length <= 1 And Not File.Exists(x) And Not filesList.Contains(x) Then filesList.Add(x)
            Next

            For Each i In selectedItem.CP.TerminalPreview.Profiles
                x = i.BackgroundImage
                If Not String.IsNullOrWhiteSpace(x) And Not File.Exists(x) And Not filesList.Contains(x) Then filesList.Add(x)

                x = i.Icon
                If Not String.IsNullOrWhiteSpace(x) And Not x.Length <= 1 And Not File.Exists(x) And Not filesList.Contains(x) Then filesList.Add(x)
            Next
        End If


        x = selectedItem.CP.WallpaperTone_W11.Image
        If Not String.IsNullOrWhiteSpace(x) And Not File.Exists(x) And Not filesList.Contains(x) And My.W11 Then filesList.Add(x)

        x = selectedItem.CP.WallpaperTone_W10.Image
        If Not String.IsNullOrWhiteSpace(x) And Not File.Exists(x) And Not filesList.Contains(x) And My.W10 Then filesList.Add(x)

        x = selectedItem.CP.WallpaperTone_W8.Image
        If Not String.IsNullOrWhiteSpace(x) And Not File.Exists(x) And Not filesList.Contains(x) And My.W8 Then filesList.Add(x)

        x = selectedItem.CP.WallpaperTone_W7.Image
        If Not String.IsNullOrWhiteSpace(x) And Not File.Exists(x) And Not filesList.Contains(x) And My.W7 Then filesList.Add(x)

        x = selectedItem.CP.WallpaperTone_WVista.Image
        If Not String.IsNullOrWhiteSpace(x) And Not File.Exists(x) And Not filesList.Contains(x) And My.WVista Then filesList.Add(x)

        x = selectedItem.CP.WallpaperTone_WXP.Image
        If Not String.IsNullOrWhiteSpace(x) And Not File.Exists(x) And Not filesList.Contains(x) And My.WXP Then filesList.Add(x)



        x = selectedItem.CP.MetricsFonts.CaptionFont.Name
        If Not CP.IsFontInstalled(x) And Not fontsList.Contains(x) Then fontsList.Add(x)

        x = selectedItem.CP.MetricsFonts.StatusFont.Name
        If Not CP.IsFontInstalled(x) And Not fontsList.Contains(x) Then fontsList.Add(x)

        x = selectedItem.CP.MetricsFonts.MessageFont.Name
        If Not CP.IsFontInstalled(x) And Not fontsList.Contains(x) Then fontsList.Add(x)

        x = selectedItem.CP.MetricsFonts.IconFont.Name
        If Not CP.IsFontInstalled(x) And Not fontsList.Contains(x) Then fontsList.Add(x)

        x = selectedItem.CP.MetricsFonts.MenuFont.Name
        If Not CP.IsFontInstalled(x) And Not fontsList.Contains(x) Then fontsList.Add(x)

        x = selectedItem.CP.MetricsFonts.FontSubstitute_MSShellDlg
        If Not CP.IsFontInstalled(x) And Not fontsList.Contains(x) Then fontsList.Add(x)

        x = selectedItem.CP.MetricsFonts.FontSubstitute_MSShellDlg2
        If Not CP.IsFontInstalled(x) And Not fontsList.Contains(x) Then fontsList.Add(x)

        x = selectedItem.CP.MetricsFonts.FontSubstitute_SegoeUI
        If Not CP.IsFontInstalled(x) And Not fontsList.Contains(x) Then fontsList.Add(x)


        With TreeView2.Nodes.Add("Files")
            For Each y As String In filesList
                .Nodes.Add(y)
            Next
        End With

        With TreeView2.Nodes.Add("Fonts")
            For Each y As String In fontsList
                .Nodes.Add(y)
            Next
        End With

        TreeView2.ExpandAll()
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
        If TablessControl1.SelectedIndex = 0 Or TablessControl1.SelectedIndex = 2 Then Visual.FadeColor(Panel1, "BackColor", Panel1.BackColor, Style.Colors.Back, 10, 15)
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

        RemoveAllStoreItems(search_results)

        TablessControl1.SelectedIndex = 0
        Label1.Text = Text
        My.Animator.ShowSync(TablessControl1)

        Visual.FadeColor(Panel1, "BackColor", Panel1.BackColor, Style.Colors.Back, 10, 15)
    End Sub

    Private Sub Panel1_BackColorChanged(sender As Object, e As EventArgs) Handles Panel1.BackColorChanged
        Label1.ForeColor = If(Panel1.BackColor.IsDark, Color.White, Color.Black)
        For Each ctrl As Control In Panel1.Controls
            ctrl.Refresh()
        Next
        DrawCustomTitlebar(Panel1.BackColor, Panel1.BackColor)
    End Sub

    Private Sub TablessControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TablessControl1.SelectedIndexChanged
        XenonButton1.Visible = TablessControl1.SelectedIndex <> 0
    End Sub

    Private Sub XenonButton5_Click(sender As Object, e As EventArgs) Handles XenonButton5.Click
        tabs_preview.SelectedIndex = 2
    End Sub

    Private Sub XenonButton3_Click(sender As Object, e As EventArgs) Handles XenonButton3.Click
        If tabs_preview.SelectedIndex = 0 Then tabs_preview.SelectedIndex = 1 Else tabs_preview.SelectedIndex = 0
    End Sub

    Private Sub XenonLinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles XenonLinkLabel1.LinkClicked
        Try
            If (Uri.IsWellFormedUriString(XenonLinkLabel1.Text, UriKind.Absolute)) And XenonLinkLabel1.Text.Contains(" ") Then Process.Start(XenonLinkLabel1.Text)
        Catch
        End Try
    End Sub

    Private Sub XenonLinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles XenonLinkLabel2.LinkClicked
        Try
            If (Uri.IsWellFormedUriString(XenonLinkLabel2.Text, UriKind.Absolute)) And XenonLinkLabel2.Text.Contains(" ") Then Process.Start(XenonLinkLabel2.Text)
        Catch
        End Try
    End Sub

    Private Sub XenonButton4_Click(sender As Object, e As EventArgs) Handles XenonButton4.Click
        Store_CPToggles.CP = selectedItem.CP
        Store_CPToggles.ShowDialog()
        Apply_Theme()
    End Sub

    Sub Apply_Theme()
        Cursor = Cursors.WaitCursor

        log_lbl.Visible = False
        log_lbl.Text = ""
        XenonButton8.Visible = False
        XenonButton14.Visible = False
        XenonButton22.Visible = False
        XenonButton25.Visible = False

        If My.[Settings].Log_ShowApplying Then
            TablessControl1.SelectedIndex = TablessControl1.TabCount - 1
            TablessControl1.Refresh()
        End If

        selectedItem.CP.Save(CP.CP_Type.Registry, "", If(My.[Settings].Log_ShowApplying, TreeView1, Nothing))

        MainFrm.CP_Original = New CP(CP_Type.Registry)

        Cursor = Cursors.Default

        If My.[Settings].AutoRestartExplorer Then
            RestartExplorer(If(My.[Settings].Log_ShowApplying, TreeView1, Nothing))
        Else
            If My.[Settings].Log_ShowApplying Then CP.AddNode(TreeView1, My.Lang.NoDefResExplorer, "warning")
        End If

        If My.[Settings].Log_ShowApplying Then CP.AddNode(TreeView1, String.Format("{0}: {1}", Now.ToLongTimeString, My.Lang.CP_AllDone), "info")

        If selectedItem.CP.MetricsFonts.Enabled And GetWindowsScreenScalingFactor() > 100 Then CP.AddNode(TreeView1, String.Format("{0}", My.Lang.CP_MetricsHighDPIAlert), "info")

        log_lbl.Visible = True
        XenonButton8.Visible = True
        XenonButton22.Visible = True
        XenonButton25.Visible = True

        If Not My.Saving_Exceptions.Count = 0 Then
            log_lbl.Text = My.Lang.CP_ErrorHappened
            XenonButton14.Visible = True
        Else
            If My.[Settings].Log_Countdown_Enabled Then
                log_lbl.Text = String.Format(My.Lang.CP_LogWillClose, My.[Settings].Log_Countdown)
                apply_elapsedSecs = 1
                Timer2.Enabled = True
                Timer2.Start()
            End If
        End If

    End Sub

    Private Sub XenonButton2_Click(sender As Object, e As EventArgs) Handles XenonButton2.Click
        Store_ConsolesPreview.CP = selectedItem.CP
        Store_ConsolesPreview.ShowDialog()
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        log_lbl.Text = String.Format(My.Lang.CP_LogWillClose, My.[Settings].Log_Countdown - apply_elapsedSecs)

        If apply_elapsedSecs + 1 <= My.[Settings].Log_Countdown Then
            apply_elapsedSecs += 1
        Else
            log_lbl.Text = ""
            Timer2.Enabled = False
            Timer2.Stop()
            TablessControl1.SelectedIndex = 1
        End If

    End Sub

    Private Sub XenonButton8_Click(sender As Object, e As EventArgs) Handles XenonButton8.Click
        log_lbl.Text = ""
        Timer2.Enabled = False
        Timer2.Stop()
        TablessControl1.SelectedIndex = 1
    End Sub

    Private Sub XenonButton22_Click(sender As Object, e As EventArgs) Handles XenonButton22.Click
        log_lbl.Text = ""
        Timer2.Enabled = False
        Timer2.Stop()

        If MainFrm.SaveFileDialog3.ShowDialog = DialogResult.OK Then
            Dim sb As New StringBuilder
            sb.Clear()

            For Each N As TreeNode In TreeView1.Nodes
                sb.AppendLine(String.Format("[{0}]{2} {1}{3}", N.ImageKey, N.Text, vbTab, vbCrLf))
            Next

            IO.File.WriteAllText(MainFrm.SaveFileDialog3.FileName, sb.ToString)

        End If
    End Sub

    Private Sub XenonButton25_Click(sender As Object, e As EventArgs) Handles XenonButton25.Click
        log_lbl.Text = ""
        Timer2.Enabled = False
        Timer2.Stop()
    End Sub

    Private Sub XenonButton14_Click(sender As Object, e As EventArgs) Handles XenonButton14.Click
        log_lbl.Text = ""
        Timer2.Enabled = False
        Timer2.Stop()
        Saving_ex_list.ex_List = My.Saving_Exceptions
        Saving_ex_list.ShowDialog()
    End Sub

    Private Sub XenonButton6_Click(sender As Object, e As EventArgs) Handles XenonButton6.Click
        XenonGroupBox4.Visible = Not XenonGroupBox4.Visible
    End Sub

    Private Sub XenonButton7_Click(sender As Object, e As EventArgs) Handles XenonButton7.Click
        WindowState = FormWindowState.Minimized

        Dim r As String() = My.[Settings].ComplexSaveResult.Split(".")
        Dim r1 As String = r(0)
        Dim r2 As String = r(1)

        Select Case r1
            Case 0              '' Save
                If IO.File.Exists(MainFrm.SaveFileDialog1.FileName) Then
                    MainFrm.CP.Save(CP.CP_Type.File, MainFrm.SaveFileDialog1.FileName)
                    MainFrm.CP_Original = MainFrm.CP.Clone
                Else
                    If MainFrm.SaveFileDialog1.ShowDialog = DialogResult.OK Then
                        MainFrm.CP.Save(CP.CP_Type.File, MainFrm.SaveFileDialog1.FileName)
                        MainFrm.CP_Original = MainFrm.CP.Clone
                    Else
                        Exit Sub
                    End If
                End If
            Case 1              '' Save As
                If MainFrm.SaveFileDialog1.ShowDialog = DialogResult.OK Then
                    MainFrm.CP.Save(CP.CP_Type.File, MainFrm.SaveFileDialog1.FileName)
                    MainFrm.CP_Original = MainFrm.CP.Clone
                Else
                    Exit Sub
                End If
        End Select

        MainFrm.CP = selectedItem.CP
        MainFrm.CP_Original = MainFrm.CP.Clone

        MainFrm.Adjust_Preview(False)
        MainFrm.ApplyCPValues(MainFrm.CP)
        MainFrm.ApplyLivePreviewFromCP(MainFrm.CP)
        MainFrm.AdjustClassicPreview()

    End Sub

    Private Sub XenonButton9_Click(sender As Object, e As EventArgs) Handles XenonButton9.Click
        Store_CursorsPreview.CP = selectedItem.CP
        Store_CursorsPreview.ShowDialog()
    End Sub

    Private Sub XenonButton19_Click(sender As Object, e As EventArgs) Handles XenonButton19.Click
        RestartExplorer()
    End Sub

    Private Sub XenonButton10_Click(sender As Object, e As EventArgs) Handles XenonButton10.Click
        Dim search_text As String = search_box.Text.TrimStart.TrimEnd.Trim.Replace(" ", "").ToUpper

        Dim lst As New Dictionary(Of String, CP) : lst.Clear()

        For Each st_itm In container.Controls.OfType(Of StoreItem)
            lst.Add(st_itm.FileName, st_itm.CP)
        Next

        RemoveAllStoreItems(search_results)

        For Each st_item In lst
            If (My.Settings.Store_Search_ThemeNames AndAlso st_item.Value.Info.PaletteName.TrimStart.TrimEnd.Trim.Replace(" ", "").ToUpper.Contains(search_text)) _
                Or (My.Settings.Store_Search_AuthorsNames AndAlso st_item.Value.Info.Author.TrimStart.TrimEnd.Trim.Replace(" ", "").ToUpper.Contains(search_text)) _
                Or (My.Settings.Store_Search_Descriptions AndAlso st_item.Value.Info.PaletteDescription.TrimStart.TrimEnd.Trim.Replace(" ", "").ToUpper.Contains(search_text)) Then

                Dim ctrl As New StoreItem With {
               .FileName = st_item.Key,
               .CP = st_item.Value,
               .MD5 = CalculateMD5(st_item.Key),
               .DoneByWinPaletter = True,
               .Size = New Size(w, h)}

                If ctrl.DoneByWinPaletter Then ctrl.CP.Info.Author = My.Application.Info.ProductName

                AddHandler ctrl.Click, AddressOf StoreItem_Clicked
                AddHandler ctrl.CPChanged, AddressOf StoreItem_CPChanged
                AddHandler ctrl.MouseEnter, AddressOf StoreItem_MouseEnter
                AddHandler ctrl.MouseLeave, AddressOf StoreItem_MouseLeave
                AddHandler ctrl.MouseWheel, AddressOf StoreItem_MouseWheel

                BeginInvoke(CType(Sub()
                                      search_results.Controls.Add(ctrl)
                                  End Sub, MethodInvoker))

            End If
        Next

        Label1.Text = "Search results"

        TablessControl1.SelectedIndex = 2

    End Sub

    Private Sub XenonButton11_Click(sender As Object, e As EventArgs) Handles XenonButton11.Click
        Store_SearchFilter.ShowDialog()
    End Sub
End Class