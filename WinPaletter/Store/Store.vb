Imports System.IO
Imports WinPaletter.XenonCore
Imports System.Security.Cryptography
Imports WinPaletter.MainFrm
Imports WinPaletter.CP
Imports WinPaletter.NativeMethods
Imports Devcorp.Controls.VisualStyles
Imports System.Text
Imports System.Net

Public Class Store

#Region "Variables"
    Private FinishedLoadingInitialCPs As Boolean
    Dim CPList As New Dictionary(Of String, CP)
    Dim w As Integer = 528 * 0.6
    Dim h As Integer = 297 * 0.6

    Private apply_elapsedSecs As Integer = 0

    Private hoveredItem As StoreItem
    Public selectedItem As StoreItem

    Private _Shown As Boolean = False
    Private ReadOnly AnimateList As New List(Of CursorControl)
    Dim Angle As Single = 180
    ReadOnly Increment As Single = 5
    Dim Cycles As Integer = 0
    Dim WithEvents WebCL As New WebClient
    Dim WithEvents ThemeDownloader As New WebClient
    Private ApplyOrEditToggle As Boolean = True
#End Region

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
        Dim condition1 As Boolean = MainFrm.PreviewConfig = WinVer.WVista AndAlso CP.WindowsVista.Theme = AeroTheme.Classic
        Dim condition2 As Boolean = MainFrm.PreviewConfig = WinVer.WXP AndAlso CP.WindowsXP.Theme = WinXPTheme.Classic

        tabs_preview.SelectedIndex = If(condition0 Or condition1 Or condition2, 1, 0)

        Panel3.Visible = (MainFrm.PreviewConfig = WinVer.W11 Or MainFrm.PreviewConfig = WinVer.W10)
        lnk_preview.Visible = (MainFrm.PreviewConfig = WinVer.W11 Or MainFrm.PreviewConfig = WinVer.W10)
        start.Visible = (Not MainFrm.PreviewConfig = WinVer.W8)
        ActionCenter.Visible = (MainFrm.PreviewConfig = WinVer.W11 Or MainFrm.PreviewConfig = WinVer.W10)

        RetroButton3.Image = My.Resources.SampleApp_Active
        RetroButton4.Image = My.Resources.SampleApp_Inactive

        Select Case MainFrm.PreviewConfig
            Case WinVer.W11
#Region "Win11"
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
                RetroButton3.Image = My.Resources.SampleApp_Active.Resize(23, 23)
                RetroButton4.Image = My.Resources.SampleApp_Inactive.Resize(23, 23)
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
                RetroButton3.Image = My.Resources.SampleApp_Active.Resize(23, 23)
                RetroButton4.Image = My.Resources.SampleApp_Inactive.Resize(23, 23)
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

        pnl_preview.BackgroundImage = My.Application.FetchSuitableWallpaper(CP, MainFrm.PreviewConfig)
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

    Sub ApplyCMDPreview(XenonCMD As XenonCMD, [Console] As CP.Structures.Console, PS As Boolean)
        XenonCMD.CMD_ColorTable00 = [Console].ColorTable00
        XenonCMD.CMD_ColorTable01 = [Console].ColorTable01
        XenonCMD.CMD_ColorTable02 = [Console].ColorTable02
        XenonCMD.CMD_ColorTable03 = [Console].ColorTable03
        XenonCMD.CMD_ColorTable04 = [Console].ColorTable04
        XenonCMD.CMD_ColorTable05 = [Console].ColorTable05
        XenonCMD.CMD_ColorTable06 = [Console].ColorTable06
        XenonCMD.CMD_ColorTable07 = [Console].ColorTable07
        XenonCMD.CMD_ColorTable08 = [Console].ColorTable08
        XenonCMD.CMD_ColorTable09 = [Console].ColorTable09
        XenonCMD.CMD_ColorTable10 = [Console].ColorTable10
        XenonCMD.CMD_ColorTable11 = [Console].ColorTable11
        XenonCMD.CMD_ColorTable12 = [Console].ColorTable12
        XenonCMD.CMD_ColorTable13 = [Console].ColorTable13
        XenonCMD.CMD_ColorTable14 = [Console].ColorTable14
        XenonCMD.CMD_ColorTable15 = [Console].ColorTable15
        XenonCMD.CMD_PopupForeground = [Console].PopupForeground
        XenonCMD.CMD_PopupBackground = [Console].PopupBackground
        XenonCMD.CMD_ScreenColorsForeground = [Console].ScreenColorsForeground
        XenonCMD.CMD_ScreenColorsBackground = [Console].ScreenColorsBackground

        If Not [Console].FontRaster Then
            With Font.FromLogFont(New LogFont With {.lfFaceName = [Console].FaceName, .lfWeight = [Console].FontWeight})
                XenonCMD.Font = New Font(.FontFamily, CInt([Console].FontSize / 65536), .Style)
            End With
        End If

        XenonCMD.PowerShell = PS
        XenonCMD.Raster = [Console].FontRaster
        Select Case [Console].FontSize
            Case 393220
                XenonCMD.RasterSize = XenonCMD.Raster_Sizes._4x6

            Case 524294
                XenonCMD.RasterSize = XenonCMD.Raster_Sizes._6x8


            Case 524296
                XenonCMD.RasterSize = XenonCMD.Raster_Sizes._8x8

            Case 524304
                XenonCMD.RasterSize = XenonCMD.Raster_Sizes._16x8

            Case 786437
                XenonCMD.RasterSize = XenonCMD.Raster_Sizes._5x12

            Case 786439
                XenonCMD.RasterSize = XenonCMD.Raster_Sizes._7x12

            Case 0
                XenonCMD.RasterSize = XenonCMD.Raster_Sizes._8x12

            Case 786448
                XenonCMD.RasterSize = XenonCMD.Raster_Sizes._16x12

            Case 1048588
                XenonCMD.RasterSize = XenonCMD.Raster_Sizes._12x16

            Case 1179658
                XenonCMD.RasterSize = XenonCMD.Raster_Sizes._10x18

            Case Else
                XenonCMD.RasterSize = XenonCMD.Raster_Sizes._8x12

        End Select

        XenonCMD.Refresh()
    End Sub

    Sub LoadCursorsFromCP([CP] As CP)
        CursorCP_to_Cursor(Arrow, [CP].Cursor_Arrow)
        CursorCP_to_Cursor(Help, [CP].Cursor_Help)
        CursorCP_to_Cursor(AppLoading, [CP].Cursor_AppLoading)
        CursorCP_to_Cursor(Busy, [CP].Cursor_Busy)
        CursorCP_to_Cursor(Move_Cur, [CP].Cursor_Move)
        CursorCP_to_Cursor(NS, [CP].Cursor_NS)
        CursorCP_to_Cursor(EW, [CP].Cursor_EW)
        CursorCP_to_Cursor(NESW, [CP].Cursor_NESW)
        CursorCP_to_Cursor(NWSE, [CP].Cursor_NWSE)
        CursorCP_to_Cursor(Up, [CP].Cursor_Up)
        CursorCP_to_Cursor(Pen, [CP].Cursor_Pen)
        CursorCP_to_Cursor(None, [CP].Cursor_None)
        CursorCP_to_Cursor(Link, [CP].Cursor_Link)
        CursorCP_to_Cursor(Pin, [CP].Cursor_Pin)
        CursorCP_to_Cursor(Person, [CP].Cursor_Person)
        CursorCP_to_Cursor(IBeam, [CP].Cursor_IBeam)
        CursorCP_to_Cursor(Cross, [CP].Cursor_Cross)

        For Each i As CursorControl In Cursors_Container.Controls
            If TypeOf i Is CursorControl Then
                i.Invalidate()
            End If
        Next
    End Sub

    Sub CursorCP_to_Cursor([CursorControl] As CursorControl, [Cursor] As CP.Structures.Cursor)
        [CursorControl].Prop_ArrowStyle = [Cursor].ArrowStyle
        [CursorControl].Prop_CircleStyle = [Cursor].CircleStyle
        [CursorControl].Prop_PrimaryColor1 = [Cursor].PrimaryColor1
        [CursorControl].Prop_PrimaryColor2 = [Cursor].PrimaryColor2
        [CursorControl].Prop_PrimaryColorGradient = [Cursor].PrimaryColorGradient
        [CursorControl].Prop_PrimaryColorGradientMode = [Cursor].PrimaryColorGradientMode
        [CursorControl].Prop_PrimaryNoise = [Cursor].PrimaryColorNoise
        [CursorControl].Prop_PrimaryNoiseOpacity = [Cursor].PrimaryColorNoiseOpacity
        [CursorControl].Prop_SecondaryColor1 = [Cursor].SecondaryColor1
        [CursorControl].Prop_SecondaryColor2 = [Cursor].SecondaryColor2
        [CursorControl].Prop_SecondaryColorGradient = [Cursor].SecondaryColorGradient
        [CursorControl].Prop_SecondaryColorGradientMode = [Cursor].SecondaryColorGradientMode
        [CursorControl].Prop_SecondaryNoise = [Cursor].SecondaryColorNoise
        [CursorControl].Prop_SecondaryNoiseOpacity = [Cursor].SecondaryColorNoiseOpacity
        [CursorControl].Prop_LoadingCircleBack1 = [Cursor].LoadingCircleBack1
        [CursorControl].Prop_LoadingCircleBack2 = [Cursor].LoadingCircleBack2
        [CursorControl].Prop_LoadingCircleBackGradient = [Cursor].LoadingCircleBackGradient
        [CursorControl].Prop_LoadingCircleBackGradientMode = [Cursor].LoadingCircleBackGradientMode
        [CursorControl].Prop_LoadingCircleBackNoise = [Cursor].LoadingCircleBackNoise
        [CursorControl].Prop_LoadingCircleBackNoiseOpacity = [Cursor].LoadingCircleBackNoiseOpacity
        [CursorControl].Prop_LoadingCircleHot1 = [Cursor].LoadingCircleHot1
        [CursorControl].Prop_LoadingCircleHot2 = [Cursor].LoadingCircleHot2
        [CursorControl].Prop_LoadingCircleHotGradient = [Cursor].LoadingCircleHotGradient
        [CursorControl].Prop_LoadingCircleHotGradientMode = [Cursor].LoadingCircleHotGradientMode
        [CursorControl].Prop_LoadingCircleHotNoise = [Cursor].LoadingCircleHotNoise
        [CursorControl].Prop_LoadingCircleHotNoiseOpacity = [Cursor].LoadingCircleHotNoiseOpacity
        [CursorControl].Prop_Shadow_Enabled = [Cursor].Shadow_Enabled
        [CursorControl].Prop_Shadow_Color = [Cursor].Shadow_Color
        [CursorControl].Prop_Shadow_Blur = [Cursor].Shadow_Blur
        [CursorControl].Prop_Shadow_Opacity = [Cursor].Shadow_Opacity
        [CursorControl].Prop_Shadow_OffsetX = [Cursor].Shadow_OffsetX
        [CursorControl].Prop_Shadow_OffsetY = [Cursor].Shadow_OffsetY
    End Sub
#End Region

#Region "Store form events"
    Private Sub Store_Load(sender As Object, e As EventArgs) Handles Me.Load
        Titlebar_panel.BackColor = Style.Colors.Back

        DLLFunc.RemoveFormTitlebarTextAndIcon(Handle)
        ShowIcon = False
        FinishedLoadingInitialCPs = False
        _Shown = False

        store_container.CheckForIllegalCrossThreadCalls = False         'Prevent exception error of cross-thread

        ApplyDarkMode(Me)

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
        MainFrm.MakeItDoubleBuffered(Titlebar_panel)
        MainFrm.MakeItDoubleBuffered(Titlebar_lbl)
        MainFrm.MakeItDoubleBuffered(Tabs)
        MainFrm.MakeItDoubleBuffered(log)

        MainFrm.MakeItDoubleBuffered(Cursors_Container)
        MainFrm.MakeItDoubleBuffered(Arrow)
        MainFrm.MakeItDoubleBuffered(Help)
        MainFrm.MakeItDoubleBuffered(AppLoading)
        MainFrm.MakeItDoubleBuffered(Busy)
        MainFrm.MakeItDoubleBuffered(Move_Cur)
        MainFrm.MakeItDoubleBuffered(NS)
        MainFrm.MakeItDoubleBuffered(EW)
        MainFrm.MakeItDoubleBuffered(NESW)
        MainFrm.MakeItDoubleBuffered(NWSE)
        MainFrm.MakeItDoubleBuffered(Up)
        MainFrm.MakeItDoubleBuffered(Pen)
        MainFrm.MakeItDoubleBuffered(None)
        MainFrm.MakeItDoubleBuffered(Link)
        MainFrm.MakeItDoubleBuffered(Pin)
        MainFrm.MakeItDoubleBuffered(Person)
        MainFrm.MakeItDoubleBuffered(IBeam)
        MainFrm.MakeItDoubleBuffered(Cross)

        log.ImageList = My.Notifications_IL
        Apply_btn.Image = MainFrm.apply_btn.Image
        RestartExplorer.Image = MainFrm.XenonButton19.Image

        WXP_Alert2.Text = MainFrm.WXP_Alert2.Text
        WXP_Alert2.Size = WXP_Alert2.Parent.Size - New Size(40, 40)
        WXP_Alert2.Location = New Point(20, 20)

        pnl_preview.BackgroundImage = MainFrm.pnl_preview.BackgroundImage
        pnl_preview_classic.BackgroundImage = pnl_preview.BackgroundImage

        MD5_lbl.Font = My.Application.ConsoleFontMedium
        themeSize_lbl.Font = My.Application.ConsoleFontMedium
        theme_ver_lbl.Font = My.Application.ConsoleFontMedium
        desc_txt.Font = My.Application.ConsoleFontLarge

    End Sub

    Private Sub Store_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        ShowIcon = True
        _Shown = True
        RemoveAllStoreItems(store_container)
        FilesFetcher.RunWorkerAsync()
    End Sub

    Private Sub Store_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        'To prevent effect of a store theme on the other forms
        My.Settings = New XeSettings(XeSettings.Mode.Registry)
        ApplyDarkMode(Me)

        Status_pnl.Visible = True
        Status_lbl.SetText(My.Lang.Store_CleaningFromMemory)
        Status_lbl.Refresh()

        FilesFetcher.CancelAsync()
        store_container.Visible = False
        RemoveAllStoreItems(store_container)
        store_container.Visible = True
        Tabs.SelectedIndex = 0
        GC.Collect()
        GC.WaitForPendingFinalizers()
        Status_lbl.SetText("")
    End Sub

#End Region

#Region "Backgroundworkers to load Store CPs"
    Sub OnlineMode()
        Dnsapi.DnsFlushResolverCache()

        Dim response As New List(Of String) : response.Clear()
        Dim repos_list As New List(Of String) : repos_list.Clear()
        Dim items As New List(Of String) : items.Clear()

        'Check by Ping if repos DB URL is accessible or not
        For Each DB As String In My.Settings.Store_Online_Repositories

            If Not DB.StartsWith("https://", My._ignore) Then DB &= "https://"

            Status_lbl.SetText(String.Format(My.Lang.Store_Ping, DB))

            If Ping(DB) Then
                repos_list.Add(DB)
            Else
                Status_lbl.SetText(String.Format(My.Lang.Store_PingFailed, DB))
            End If

        Next

        'Loop through all valid repos DBs
        For Each DB As String In repos_list

            'Try to generate a folder name dependent on the URL
            Dim reposName As String
            If DB.ToUpper.Contains("GITHUB.COM") Then
                Dim x As String() = DB.Replace("https://", "").Replace("http://", "").Split("/")
                reposName = x(1) & "_" & x(2)
                reposName = String.Join("_", reposName.Split(Path.GetInvalidFileNameChars()))
            Else
                reposName = String.Join("_", DB.Replace("https://", "").Replace("http://", "").Split(Path.GetInvalidFileNameChars()))
            End If

            'Get text of the DB from URL
            Status_lbl.SetText(String.Format(My.Lang.Store_Accessing, DB))
            response.Clear()
            response = WebCL.DownloadString(DB).CList
            items.Clear()

            'Add valid lines (Correct format) in a themes list
            For Each item In response
                Dim valid As Boolean = True
                If item.Contains("|") Then
                    For Each x In item.Split("|")
                        If String.IsNullOrWhiteSpace(x) Then
                            valid = False
                            Exit For
                        End If
                    Next
                Else
                    valid = False
                End If

                If valid Then items.Add(item)

            Next

            BeginInvoke(CType(Sub()
                                  ProgressBar1.Visible = True
                              End Sub, MethodInvoker))

            Dim i As Integer = 0
            Dim allProgress As Integer = items.Count * 2

            'Loop through valid lines from the themes list
            For Each item In items
                Dim URL_ThemeFile As String = item.Split("|")(2)
                Dim MD5_ThemeFile As String = item.Split("|")(0).ToUpper
                Dim URL_PackFile As String = item.Split("|")(3)
                Dim MD5_PackFile As String = item.Split("|")(1).ToUpper

                'Create a folder inside AppData folder
                Dim temp As String = URL_ThemeFile.Replace("?raw=true", "")
                Dim FileName As String = temp.Split("/").Last
                temp = temp.Replace("/" & FileName, "")
                Dim FolderName As String = temp.Split("/").Last
                Dim Dir As String = My.PATH_StoreCache
                If Not String.IsNullOrWhiteSpace(FolderName) Then Dir &= "\" & reposName & "\" & FolderName
                If Not Directory.Exists(Dir) Then Directory.CreateDirectory(Dir)

                Status_lbl.SetText("")

                'Download the theme (*.wpth)
                If File.Exists(Dir & "\" & FileName) Then

                    'If it exists, check MD5, if it is changed, redownload the theme
                    If CalculateMD5(Dir & "\" & FileName) <> MD5_ThemeFile Then
                        File.Delete(Dir & "\" & FileName)
                        Status_lbl.SetText(String.Format(My.Lang.Store_UpdateTheme, FileName, URL_ThemeFile))
                        Try : WebCL.DownloadFile(URL_ThemeFile, Dir & "\" & FileName) : Catch : End Try
                    End If
                Else
                    Status_lbl.SetText(String.Format(My.Lang.Store_DownloadTheme, FileName, URL_ThemeFile))
                    Try : WebCL.DownloadFile(URL_ThemeFile, Dir & "\" & FileName) : Catch : End Try
                End If

                i += 1
                If allProgress > 0 Then FilesFetcher.ReportProgress((i / allProgress) * 100)

                'Convert themes CPs into StoreItems
                If File.Exists(Dir & "\" & FileName) Then
                    Try
                        Status_lbl.SetText(String.Format(My.Lang.Store_LoadingTheme, FileName))

                        Using CP As New CP(CP_Type.File, Dir & "\" & FileName)

                            Dim ctrl As New StoreItem With {
                                           .FileName = Dir & "\" & FileName,
                                           .CP = CP,
                                           .MD5_ThemeFile = MD5_ThemeFile,
                                           .MD5_PackFile = MD5_PackFile,
                                           .DoneByWinPaletter = (DB.ToUpper = My.Resources.Link_StoreMainDB.ToUpper),
                                           .Size = New Size(w, h),
                                           .URL_ThemeFile = URL_ThemeFile,
                                           .URL_PackFile = URL_PackFile}

                            If ctrl.DoneByWinPaletter Then ctrl.CP.Info.Author = My.Application.Info.ProductName

                            AddHandler ctrl.Click, AddressOf StoreItem_Clicked
                            AddHandler ctrl.CPChanged, AddressOf StoreItem_CPChanged
                            AddHandler ctrl.MouseEnter, AddressOf StoreItem_MouseEnter
                            AddHandler ctrl.MouseLeave, AddressOf StoreItem_MouseLeave

                            BeginInvoke(CType(Sub()
                                                  store_container.Controls.Add(ctrl)
                                              End Sub, MethodInvoker))

                        End Using

                    Catch ex As Exception

                    End Try
                End If

                Status_lbl.SetText("")

                i += 1

                If allProgress > 0 Then FilesFetcher.ReportProgress((i / allProgress) * 100)

            Next

            'Finalizing
            BeginInvoke(CType(Sub()
                                  ProgressBar1.Visible = False
                              End Sub, MethodInvoker))

            CPList.Clear()
        Next

        FinishedLoadingInitialCPs = True
    End Sub

    Sub OfflineMode()
        BeginInvoke(CType(Sub()
                              ProgressBar1.Visible = True
                              store_container.Visible = False
                          End Sub, MethodInvoker))

        Dim i As Integer = 0
        Dim allProgress As Integer = 0


        For Each folder In My.Settings.Store_Offline_Directories

            If Directory.Exists(folder) Then
                Status_lbl.SetText("Accessing themes from folder """ & folder & """")
                allProgress += Directory.GetFiles(folder, "*.wpth", If(My.Settings.Store_Offline_SubFolders, SearchOption.AllDirectories, SearchOption.TopDirectoryOnly)).Count
            End If

        Next

        allProgress *= 2

        For Each folder In My.Settings.Store_Offline_Directories

            If Directory.Exists(folder) Then

                For Each file As String In Directory.GetFiles(folder, "*.wpth", If(My.Settings.Store_Offline_SubFolders, SearchOption.AllDirectories, SearchOption.TopDirectoryOnly))

                    Try
                        If Not CPList.ContainsKey(file) Then

                            Status_lbl.SetText("Enumerating themes: """ & file & """")

                            Using CPx As New CP(CP.CP_Type.File, file)
                                CPList.Add(file, CPx)
                            End Using
                        End If
                    Catch ex As Exception
                    End Try

                    i += 1

                    If allProgress > 0 Then FilesFetcher.ReportProgress((i / allProgress) * 100)
                Next
            End If
        Next


        For Each StoreItem In CPList
            Status_lbl.SetText("Loading theme """ & StoreItem.Value.Info.ThemeName & """")

            Dim ctrl As New StoreItem With {
                        .FileName = StoreItem.Key,
                        .CP = StoreItem.Value,
                        .MD5_ThemeFile = CalculateMD5(StoreItem.Key),
                        .DoneByWinPaletter = False,
                        .Size = New Size(w, h),
                        .URL_ThemeFile = New FileInfo(StoreItem.Key).FullName}

            If ctrl.DoneByWinPaletter Then ctrl.CP.Info.Author = My.Application.Info.ProductName

            AddHandler ctrl.Click, AddressOf StoreItem_Clicked
            AddHandler ctrl.CPChanged, AddressOf StoreItem_CPChanged
            AddHandler ctrl.MouseEnter, AddressOf StoreItem_MouseEnter
            AddHandler ctrl.MouseLeave, AddressOf StoreItem_MouseLeave

            BeginInvoke(CType(Sub()
                                  store_container.Controls.Add(ctrl)
                              End Sub, MethodInvoker))

            i += 1

            If allProgress > 0 Then FilesFetcher.ReportProgress((i / allProgress) * 100)
        Next

        BeginInvoke(CType(Sub()
                              ProgressBar1.Visible = False
                              store_container.Visible = True
                          End Sub, MethodInvoker))

        Status_lbl.SetText("")

        CPList.Clear()

        FinishedLoadingInitialCPs = True
    End Sub

    Private Sub FilesFetcher_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles FilesFetcher.DoWork

        If My.Settings.Store_Online_or_Offline Then
            If Not IsNetworkAvailable() Then
                Status_lbl.SetText(My.Lang.Store_NoNetwork)

                If MsgBox(My.Lang.Store_NoNetwork, MsgBoxStyle.Question + MsgBoxStyle.YesNo, My.Lang.Store_TryOffline) = MsgBoxResult.Yes Then
                    OfflineMode()
                    Exit Sub
                End If

            Else
                OnlineMode()
            End If

        Else
            OfflineMode()
        End If

        GC.Collect()
        GC.WaitForPendingFinalizers()

        My.Animator.HideSync(Status_pnl)
    End Sub

    Private Sub FilesFetcher_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles FilesFetcher.ProgressChanged
        Try
            ProgressBar1.Value = Math.Max(Math.Min(e.ProgressPercentage, ProgressBar1.Maximum), ProgressBar1.Minimum)
        Catch
        End Try
    End Sub

    Private Sub FilesFetcher_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles FilesFetcher.RunWorkerCompleted

    End Sub

#End Region

#Region "Store item events"
    Public Sub StoreItem_Clicked(sender As Object, e As MouseEventArgs)

        Select Case e.Button
            Case MouseButtons.Right
                With DirectCast(sender, StoreItem)
                    Adjust_Preview(.CP)
                    AdjustClassicPreview(.CP)
                    Store_Hover.Close()

                    tabs_preview.SelectedIndex = 0
                    Store_Hover.img0 = tabs_preview.ToBitmap
                    tabs_preview.SelectedIndex = 1
                    Store_Hover.img1 = tabs_preview.ToBitmap
                    Store_Hover.ShowDialog()

                End With

            Case Else
                selectedItem = DirectCast(sender, StoreItem)

                With selectedItem
                    My.Animator.HideSync(Tabs)

                    If .CP.AppTheme.Enabled Then
                        My.Settings.Appearance_Custom = .CP.AppTheme.Enabled
                        My.Settings.Appearance_Custom_Dark = .CP.AppTheme.DarkMode
                        My.Settings.Appearance_Rounded = .CP.AppTheme.RoundCorners
                        My.Settings.Appearance_Back = .CP.AppTheme.BackColor
                        My.Settings.Appearance_Accent = .CP.AppTheme.AccentColor
                        ApplyDarkMode(Me)
                    End If

                    Adjust_Preview(.CP)
                    AdjustClassicPreview(.CP)
                    ApplyRetroPreview(.CP)
                    SetClassicMetrics(.CP)
                    ApplyCMDPreview(XenonCMD1, .CP.CommandPrompt, False)
                    ApplyCMDPreview(XenonCMD2, .CP.PowerShellx86, True)
                    ApplyCMDPreview(XenonCMD3, .CP.PowerShellx64, True)
                    LoadCursorsFromCP(.CP)

                    For Each i As CursorControl In Cursors_Container.Controls
                        If TypeOf i Is CursorControl Then
                            If i.Prop_Cursor = CursorType.AppLoading Or i.Prop_Cursor = CursorType.Busy Then AnimateList.Add(i)
                        End If
                    Next

                    Titlebar_lbl.Text = .CP.Info.ThemeName & " - " & My.Lang.By & " " & .CP.Info.Author
                    Titlebar_lbl.Font = New Font(.CP.MetricsFonts.CaptionFont.Name, Titlebar_lbl.Font.Size, Titlebar_lbl.Font.Style)

                    theme_name_lbl.Text = .CP.Info.ThemeName
                    theme_ver_lbl.Text = .CP.Info.ThemeVersion
                    author_lbl.Text = .CP.Info.Author
                    MD5_lbl.Text = .MD5_ThemeFile
                    themeSize_lbl.Text = My.Computer.FileSystem.GetFileInfo(.FileName).Length.SizeString
                    Author_link.Text = If(Not String.IsNullOrWhiteSpace(.CP.Info.AuthorSocialMediaLink), .CP.Info.AuthorSocialMediaLink, My.Lang.Store_NoIncludedData)
                    Download_Link.Text = If(Not String.IsNullOrWhiteSpace(.URL_ThemeFile), .URL_ThemeFile.Replace("?raw=true", ""), My.Lang.Store_NoIncludedData)
                    desc_txt.Text = .CP.Info.Description

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
                    SupportedOS_lbl.Text = os_format

                    Tabs.SelectedIndex = 1

                    My.Animator.ShowSync(Tabs)

                    Visual.FadeColor(Titlebar_panel, "BackColor", Titlebar_panel.BackColor, .CP.StoreInfo.Color2, 10, 15)
                End With
        End Select
    End Sub

    Public Sub StoreItem_MouseEnter(sender As Object, e As EventArgs)
        hoveredItem = DirectCast(sender, StoreItem)
        Visual.FadeColor(Titlebar_panel, "BackColor", Titlebar_panel.BackColor, hoveredItem.CP.StoreInfo.Color1, 10, 15)
    End Sub

    Public Sub StoreItem_MouseLeave(sender As Object, e As EventArgs)

        If Tabs.SelectedIndex = 0 Or Tabs.SelectedIndex = 2 Then Visual.FadeColor(Titlebar_panel, "BackColor", Titlebar_panel.BackColor, Style.Colors.Back, 10, 15)
    End Sub

    Public Sub StoreItem_CPChanged(sender As Object, e As EventArgs)
        If FinishedLoadingInitialCPs Then
            With DirectCast(sender, StoreItem)
                Adjust_Preview(.CP)
                AdjustClassicPreview(.CP)
                .Refresh()
            End With
        End If
    End Sub
#End Region

#Region "Subs\Functions"

#Region "   Store"
    Private Sub ThemeDownloader_DownloadProgressChanged(sender As Object, e As DownloadProgressChangedEventArgs) Handles ThemeDownloader.DownloadProgressChanged
        ProgressBar1.Value = (e.BytesReceived / e.TotalBytesToReceive) * 100
    End Sub

    Private Sub ThemeDownloader_DownloadFileCompleted(sender As Object, e As System.ComponentModel.AsyncCompletedEventArgs) Handles ThemeDownloader.DownloadFileCompleted
        ProgressBar1.Value = 0
        Status_lbl.SetText("")
        ProgressBar1.Visible = False
        Status_pnl.Visible = False

        DoActionsAfterPackDownload()
    End Sub


    Sub Apply_Theme()
        Cursor = Cursors.WaitCursor

        log_lbl.Visible = False
        log_lbl.Text = ""
        ok_btn.Visible = False
        ShowErrors_btn.Visible = False
        ExportDetails_btn.Visible = False
        StopTimer_btn.Visible = False

        If My.[Settings].Log_ShowApplying Then
            Tabs.SelectedIndex = Tabs.TabCount - 1
            Tabs.Refresh()
        End If

        With My.Settings
            .Appearance_Custom = selectedItem.CP.AppTheme.Enabled
            .Appearance_Back = selectedItem.CP.AppTheme.BackColor
            .Appearance_Accent = selectedItem.CP.AppTheme.AccentColor
            .Appearance_Custom_Dark = selectedItem.CP.AppTheme.DarkMode
            .Appearance_Rounded = selectedItem.CP.AppTheme.RoundCorners
        End With
        ApplyDarkMode()

        selectedItem.CP.Save(CP.CP_Type.Registry, "", If(My.[Settings].Log_ShowApplying, log, Nothing))

        MainFrm.CP_Original = New CP(CP_Type.Registry)

        Cursor = Cursors.Default

        If My.[Settings].AutoRestartExplorer Then
            XenonCore.RestartExplorer(If(My.[Settings].Log_ShowApplying, log, Nothing))
        Else
            If My.[Settings].Log_ShowApplying Then CP.AddNode(log, My.Lang.NoDefResExplorer, "warning")
        End If

        If My.[Settings].Log_ShowApplying Then CP.AddNode(log, String.Format("{0}: {1}", Now.ToLongTimeString, My.Lang.CP_AllDone), "info")

        If selectedItem.CP.MetricsFonts.Enabled And GetWindowsScreenScalingFactor() > 100 Then CP.AddNode(log, String.Format("{0}", My.Lang.CP_MetricsHighDPIAlert), "info")

        log_lbl.Visible = True
        ok_btn.Visible = True
        ExportDetails_btn.Visible = True
        StopTimer_btn.Visible = True

        If Not My.Saving_Exceptions.Count = 0 Then
            log_lbl.Text = My.Lang.CP_ErrorHappened
            ShowErrors_btn.Visible = True
        Else
            If My.[Settings].Log_Countdown_Enabled Then
                log_lbl.Text = String.Format(My.Lang.CP_LogWillClose, My.[Settings].Log_Countdown)
                apply_elapsedSecs = 1
                Log_Timer.Enabled = True
                Log_Timer.Start()
            End If
        End If

    End Sub

    Sub DoActionsAfterPackDownload()
        If ApplyOrEditToggle Then
            'Apply button is pressed
            Store_CPToggles.CP = selectedItem.CP
            Store_CPToggles.ShowDialog()
            Apply_Theme()

            MainFrm.CP = selectedItem.CP
            MainFrm.CP_Original = MainFrm.CP.Clone

            MainFrm.Adjust_Preview(False)
            MainFrm.ApplyCPValues(MainFrm.CP)
            MainFrm.ApplyLivePreviewFromCP(MainFrm.CP)
            MainFrm.AdjustClassicPreview()

        Else
            'Edit button is pressed
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
        End If
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

    Sub PerformSearch()
        Dim search_text As String = search_box.Text.TrimStart.TrimEnd.Trim.Replace(" ", "").ToUpper

        If String.IsNullOrWhiteSpace(search_text) Then Exit Sub

        Dim lst As New Dictionary(Of String, StoreItem) : lst.Clear()

        For Each st_itm In store_container.Controls.OfType(Of StoreItem)
            lst.Add(st_itm.FileName, st_itm)
        Next

        RemoveAllStoreItems(search_results)

        Dim found_sum As Integer = 0

        For Each st_item In lst
            If (My.Settings.Store_Search_ThemeNames AndAlso st_item.Value.CP.Info.ThemeName.TrimStart.TrimEnd.Trim.Replace(" ", "").ToUpper.Contains(search_text)) _
                Or (My.Settings.Store_Search_AuthorsNames AndAlso st_item.Value.CP.Info.Author.TrimStart.TrimEnd.Trim.Replace(" ", "").ToUpper.Contains(search_text)) _
                Or (My.Settings.Store_Search_Descriptions AndAlso st_item.Value.CP.Info.Description.TrimStart.TrimEnd.Trim.Replace(" ", "").ToUpper.Contains(search_text)) Then

                found_sum += 1

                Dim ctrl As New StoreItem With {
               .FileName = st_item.Key,
               .CP = st_item.Value.CP,
               .MD5_ThemeFile = CalculateMD5(st_item.Key),
               .DoneByWinPaletter = st_item.Value.DoneByWinPaletter,
               .Size = New Size(w, h),
               .URL_ThemeFile = st_item.Value.URL_ThemeFile}

                If ctrl.DoneByWinPaletter Then ctrl.CP.Info.Author = My.Application.Info.ProductName

                AddHandler ctrl.Click, AddressOf StoreItem_Clicked
                AddHandler ctrl.CPChanged, AddressOf StoreItem_CPChanged
                AddHandler ctrl.MouseEnter, AddressOf StoreItem_MouseEnter
                AddHandler ctrl.MouseLeave, AddressOf StoreItem_MouseLeave

                BeginInvoke(CType(Sub()
                                      search_results.Controls.Add(ctrl)
                                  End Sub, MethodInvoker))

            End If
        Next

        Titlebar_lbl.Text = String.Format("Search results ({0})", found_sum)

        Tabs.SelectedIndex = 2

        lst.Clear()
    End Sub
#End Region

#Region "   Helpers"
    Private Function CalculateMD5(ByVal path As String) As String

        Using md5 As MD5 = MD5.Create()
            Dim txt = IO.File.ReadAllText(path)
            Dim hash = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(txt))
            Dim result = BitConverter.ToString(hash).Replace("-", "")
            Return result.ToUpper
        End Using

    End Function

#End Region

#End Region

#Region "Timers"
    Private Sub Log_Timer_Tick(sender As Object, e As EventArgs) Handles Log_Timer.Tick
        log_lbl.Text = String.Format(My.Lang.CP_LogWillClose, My.[Settings].Log_Countdown - apply_elapsedSecs)

        If apply_elapsedSecs + 1 <= My.[Settings].Log_Countdown Then
            apply_elapsedSecs += 1
        Else
            log_lbl.Text = ""
            Log_Timer.Enabled = False
            Log_Timer.Stop()
            Tabs.SelectedIndex = 1
        End If

    End Sub
    Private Sub Cursor_Timer_Tick(sender As Object, e As EventArgs) Handles Cursor_Timer.Tick
        If Not _Shown Then Exit Sub

        Try
            For Each i As CursorControl In AnimateList
                i.Angle = Angle
                i.Refresh()

                If Angle + Increment >= 360 Then Angle = 0
                Angle += Increment

                If Angle = 180 And Cycles >= 2 Then
                    i.Angle = 180
                    Cursor_Timer.Enabled = False
                    Cursor_Timer.Stop()
                ElseIf Angle = 180 Then
                    Cycles += 1
                End If

            Next
        Catch
        End Try
    End Sub
#End Region

#Region "Buttons Events"
    Private Sub back_btn_Click(sender As Object, e As EventArgs) Handles back_btn.Click

        If ThemeDownloader.IsBusy Then
            ProgressBar1.Value = 0
            Status_lbl.SetText("")
            ProgressBar1.Visible = False
            Status_pnl.Visible = False
            ThemeDownloader.CancelAsync()
        End If

        My.Animator.HideSync(Tabs)

        If selectedItem.CP.AppTheme.Enabled Then
            My.Settings = New XeSettings(XeSettings.Mode.Registry)
            ApplyDarkMode(Me)
        End If

        RemoveAllStoreItems(search_results)

        Titlebar_lbl.Font = New Font("Segoe UI", Titlebar_lbl.Font.Size, Titlebar_lbl.Font.Style)
        Tabs.SelectedIndex = 0
        My.Animator.HideSync(back_btn)

        Titlebar_lbl.Text = Text
        My.Animator.ShowSync(Tabs)

        Visual.FadeColor(Titlebar_panel, "BackColor", Titlebar_panel.BackColor, Style.Colors.Back, 10, 15)
    End Sub

#Region "   Preview switchers"
    Private Sub Switch_M_C_btn_Click(sender As Object, e As EventArgs) Handles Switch_M_C_btn.Click
        If tabs_preview.SelectedIndex = 0 Then tabs_preview.SelectedIndex = 1 Else tabs_preview.SelectedIndex = 0
    End Sub
    Private Sub ShowClassic_btn_Click(sender As Object, e As EventArgs) Handles ShowClassic_btn.Click
        tabs_preview.SelectedIndex = 2
    End Sub
    Private Sub ShowCMD_btn_Click(sender As Object, e As EventArgs) Handles ShowCMD_btn.Click
        tabs_preview.SelectedIndex = 3
    End Sub
    Private Sub ShowPS86_btn_Click(sender As Object, e As EventArgs) Handles ShowPS86_btn.Click
        tabs_preview.SelectedIndex = 4
    End Sub
    Private Sub ShowPS64_btn_Click(sender As Object, e As EventArgs) Handles ShowPS64_btn.Click
        tabs_preview.SelectedIndex = 5
    End Sub
    Private Sub ShowCursors_btn_Click(sender As Object, e As EventArgs) Handles ShowCursors_btn.Click
        tabs_preview.SelectedIndex = 6
    End Sub
#End Region

#Region "   Links"
    Private Sub Author_link_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles Author_link.LinkClicked
        Try
            If (Uri.IsWellFormedUriString(Author_link.Text, UriKind.Absolute)) And Not Author_link.Text.Contains(" ") Then Process.Start(Author_link.Text)
        Catch
        End Try
    End Sub

    Private Sub Download_Link_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles Download_Link.LinkClicked
        Try
            If (Uri.IsWellFormedUriString(Download_Link.Text, UriKind.Absolute)) And Not Download_Link.Text.Contains(" ") Then Process.Start(Download_Link.Text)
        Catch
        End Try
    End Sub
#End Region

#Region "   Applying row"
    Private Sub Apply_Edit_btn_Click(sender As Object, e As EventArgs) Handles Apply_btn.Click, Edit_btn.Click
        ApplyOrEditToggle = sender Is Apply_btn

        Dim temp As String = selectedItem.URL_PackFile.Replace("?raw=true", "")
        Dim FileName As String = temp.Split("/").Last
        temp = temp.Replace("/" & FileName, "")
        Dim FolderName As String = temp.Split("/").Last
        Dim Dir As String
        If File.Exists(selectedItem.FileName) Then
            Dir = New FileInfo(selectedItem.FileName).Directory.FullName
        Else
            Dir = selectedItem.FileName.Replace("\" & selectedItem.FileName.Split("\").Last, "")
        End If
        If Not Directory.Exists(Dir) Then Directory.CreateDirectory(Dir)

        Status_pnl.Visible = True

        If (File.Exists(FileName) AndAlso CalculateMD5(FileName) <> selectedItem.MD5_PackFile) OrElse Not File.Exists(FileName) OrElse selectedItem.MD5_PackFile = "0" Then
            Status_lbl.SetText(String.Format(My.Lang.Store_DownloadThemeRes, FileName, selectedItem.URL_PackFile))

            Try
                ProgressBar1.Visible = True
                ThemeDownloader.DownloadFileAsync(New Uri(selectedItem.URL_PackFile), Dir & "\" & FileName)
            Catch
                ProgressBar1.Value = 0
                Status_pnl.Visible = False
            End Try

        Else
            DoActionsAfterPackDownload()

        End If

    End Sub

    Private Sub RestartExplorer_Click(sender As Object, e As EventArgs) Handles RestartExplorer.Click
        XenonCore.RestartExplorer()
    End Sub

#End Region

#Region "   Log row"
    Private Sub ok_btn_Click(sender As Object, e As EventArgs) Handles ok_btn.Click
        log_lbl.Text = ""
        Log_Timer.Enabled = False
        Log_Timer.Stop()
        Tabs.SelectedIndex = 1
    End Sub

    Private Sub ExportDetails_btn_Click(sender As Object, e As EventArgs) Handles ExportDetails_btn.Click
        log_lbl.Text = ""
        Log_Timer.Enabled = False
        Log_Timer.Stop()

        If MainFrm.SaveFileDialog3.ShowDialog = DialogResult.OK Then
            Dim sb As New StringBuilder
            sb.Clear()

            For Each N As TreeNode In log.Nodes
                sb.AppendLine(String.Format("[{0}]{2} {1}{3}", N.ImageKey, N.Text, vbTab, vbCrLf))
            Next

            IO.File.WriteAllText(MainFrm.SaveFileDialog3.FileName, sb.ToString)

        End If
    End Sub

    Private Sub StopTimer_btn_Click(sender As Object, e As EventArgs) Handles StopTimer_btn.Click
        log_lbl.Text = ""
        Log_Timer.Enabled = False
        Log_Timer.Stop()
    End Sub

    Private Sub ShowErrors_btn_Click(sender As Object, e As EventArgs) Handles ShowErrors_btn.Click
        log_lbl.Text = ""
        Log_Timer.Enabled = False
        Log_Timer.Stop()
        Saving_ex_list.ex_List = My.Saving_Exceptions
        Saving_ex_list.ShowDialog()
    End Sub
#End Region

#Region "   Search"
    Private Sub search_btn_Click(sender As Object, e As EventArgs) Handles search_btn.Click
        PerformSearch()
    End Sub
    Private Sub search_filter_btn_Click(sender As Object, e As EventArgs) Handles search_filter_btn.Click
        Store_SearchFilter.ShowDialog()
    End Sub

#End Region

#Region "   Cursors"
    Private Sub cur_anim_btn_Click(sender As Object, e As EventArgs) Handles cur_anim_btn.Click
        Angle = 180
        Cycles = 0
        Cursor_Timer.Enabled = True
        Cursor_Timer.Start()
    End Sub

    Private Sub cur_tip_btn_Click(sender As Object, e As EventArgs) Handles cur_tip_btn.Click
        MsgBox(My.Lang.ScalingTip, MsgBoxStyle.Information)
    End Sub
#End Region

#End Region

#Region "Major Tab"
    Private Sub Tabs_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Tabs.SelectedIndexChanged
        If Tabs.SelectedIndex <> 0 Then
            My.Animator.ShowSync(back_btn)
        Else
            My.Animator.HideSync(back_btn)
        End If

        search_panel.Visible = Tabs.SelectedIndex = 0 Or Tabs.SelectedIndex = 2
    End Sub

#End Region

#Region "Others"
    Private Sub Titlebar_panel_BackColorChanged(sender As Object, e As EventArgs) Handles Titlebar_panel.BackColorChanged

        Titlebar_lbl.ForeColor = If(Titlebar_panel.BackColor.IsDark, Color.White, Color.Black)
        search_box.ForeColor = If(Titlebar_panel.BackColor.IsDark, Color.White, Color.Black)
        back_btn.Image = If(Titlebar_panel.BackColor.IsDark, My.Resources.Store_BackBtn, My.Resources.Store_BackBtn.Invert)

        For Each ctrl As Control In Titlebar_panel.Controls
            ctrl.Refresh()
        Next

        For Each ctrl As Control In search_panel.Controls
            ctrl.Invalidate()
        Next

        DrawCustomTitlebar(Titlebar_panel.BackColor, Titlebar_panel.BackColor)
    End Sub

    Private Sub CursorsSize_Bar_Scroll(sender As Object) Handles CursorsSize_Bar.Scroll
        If Not _Shown Then Exit Sub

        For Each i As CursorControl In Cursors_Container.Controls
            i.Prop_Scale = sender.value / 100
            i.Width = 32 * i.Prop_Scale + 32
            i.Height = i.Width
            i.Refresh()
        Next

        Label17.Text = String.Format("{0} ({1}x)", My.Lang.Scaling, sender.value / 100)
    End Sub

    Private Sub search_box_KeyPress(sender As Object, e As KeyPressEventArgs) Handles search_box.KeyboardPress
        If e.KeyChar = ChrW(Keys.Enter) Then PerformSearch()
    End Sub

    Dim newPoint As New Point()
    Dim oldPoint As New Point()

    Private Sub CustomTitlebar_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles Titlebar_panel.MouseDown, Titlebar_lbl.MouseDown, Titlebar_img.MouseDown
        oldPoint = MousePosition - Location
    End Sub

    Private Sub CustomTitlebar_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles Titlebar_panel.MouseMove, Titlebar_lbl.MouseMove, Titlebar_img.MouseMove
        If e.Button = MouseButtons.Left Then
            newPoint = MousePosition - oldPoint
            Location = newPoint
        End If
    End Sub


#End Region

End Class