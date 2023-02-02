Imports WinPaletter.CP
Imports WinPaletter.MainFrm

Public Class DragPreviewer

    Public CP As CP
    Public File As String

    Private Sub DragPreviewer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SuspendLayout()

        MainFrm.MakeItDoubleBuffered(Me)
        MainFrm.MakeItDoubleBuffered(pnl_preview)
        MainFrm.MakeItDoubleBuffered(pnlRetroPreview)

        Opacity = 0
        Visible = False

        pnl_preview.BackgroundImage = My.Wallpaper

        If My.W11 Or My.W10 Then
            FormBorderStyle = FormBorderStyle.None
            BackColor = Color.Black
            TransparencyKey = Color.Black
        Else
            FormBorderStyle = FormBorderStyle.FixedToolWindow
            BackColor = Color.Fuchsia
            TransparencyKey = Color.Fuchsia
        End If

        CP = New CP(CP.Mode.File, File, True)

        Adjust_Preview()
        ApplyLivePreviewFromCP(CP)
        ApplyRetroPreview(CP)
        SetMetics(CP)

        Acrylism.EnableBlur(Me, True)

        ResumeLayout()
    End Sub

    Private Sub DragPreviewer_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Opacity = 1
        Visible = True
        Me.Invalidate()
    End Sub

    Sub ApplyLivePreviewFromCP([CP] As CP)
        XenonWindow1.Active = True
        XenonWindow2.Active = False

        Select Case MainFrm.PreviewConfig
            Case MainFrm.WinVer.W11
#Region "Win11"
                XenonWindow1.AccentColor_Enabled = [CP].Windows11.ApplyAccentonTitlebars
                XenonWindow2.AccentColor_Enabled = [CP].Windows11.ApplyAccentonTitlebars

                XenonWindow1.AccentColor_Active = [CP].Windows11.Titlebar_Active
                XenonWindow2.AccentColor_Active = [CP].Windows11.Titlebar_Active

                XenonWindow1.AccentColor_Inactive = [CP].Windows11.Titlebar_Inactive
                XenonWindow2.AccentColor_Inactive = [CP].Windows11.Titlebar_Inactive

                XenonWindow1.DarkMode = Not [CP].Windows11.AppMode_Light
                XenonWindow2.DarkMode = Not [CP].Windows11.AppMode_Light

                Label8.ForeColor = If([CP].Windows11.AppMode_Light, Color.Black, Color.White)

                start.DarkMode = Not [CP].Windows11.WinMode_Light
                taskbar.DarkMode = Not [CP].Windows11.WinMode_Light
                ActionCenter.DarkMode = Not [CP].Windows11.WinMode_Light

                taskbar.Transparency = [CP].Windows11.Transparency
                start.Transparency = [CP].Windows11.Transparency
                ActionCenter.Transparency = [CP].Windows11.Transparency

                start.Top = taskbar.Top - start.Height - 9

                Select Case Not [CP].Windows11.WinMode_Light
                    Case True   ''''''''''Dark
                        taskbar.BackColorAlpha = 130
                        start.BackColorAlpha = 130
                        ActionCenter.BackColorAlpha = 130

                        Select Case [CP].Windows11.ApplyAccentonTaskbar
                            Case ApplyAccentonTaskbar_Level.None
                                taskbar.BackColor = Color.FromArgb(55, 55, 55)
                                start.BackColor = Color.FromArgb(40, 40, 40)
                                ActionCenter.BackColor = Color.FromArgb(55, 55, 55)

                            Case ApplyAccentonTaskbar_Level.Taskbar_Start_AC
                                taskbar.BackColor = Color.FromArgb(taskbar.BackColor.A, [CP].Windows11.Color_Index5)
                                start.BackColor = Color.FromArgb(start.BackColor.A, [CP].Windows11.Color_Index5)
                                ActionCenter.BackColor = Color.FromArgb(ActionCenter.BackColor.A, [CP].Windows11.Color_Index5)

                            Case ApplyAccentonTaskbar_Level.Taskbar
                                taskbar.BackColor = Color.FromArgb(taskbar.BackColor.A, [CP].Windows11.Color_Index5)
                                start.BackColor = Color.FromArgb(40, 40, 40)
                                ActionCenter.BackColor = Color.FromArgb(55, 55, 55)

                        End Select


                        ActionCenter.ActionCenterButton_Normal = [CP].Windows11.Color_Index1
                        ActionCenter.ActionCenterButton_Hover = [CP].Windows11.Color_Index0
                        ActionCenter.ActionCenterButton_Pressed = [CP].Windows11.Color_Index2
                        start.SearchBoxAccent = [CP].Windows11.Color_Index1
                        taskbar.AppUnderline = [CP].Windows11.Color_Index1

                        setting_icon_preview.ForeColor = [CP].Windows11.Color_Index3
                        lnk_preview.ForeColor = [CP].Windows11.Color_Index0

                    Case False   ''''''''''Light
                        taskbar.BackColorAlpha = 180
                        start.BackColorAlpha = 180
                        ActionCenter.BackColorAlpha = 180

                        Select Case [CP].Windows11.ApplyAccentonTaskbar
                            Case ApplyAccentonTaskbar_Level.None
                                taskbar.BackColor = Color.FromArgb(255, 255, 255)
                                start.BackColor = Color.FromArgb(255, 255, 255)
                                ActionCenter.BackColor = Color.FromArgb(255, 255, 255)

                            Case ApplyAccentonTaskbar_Level.Taskbar_Start_AC
                                taskbar.BackColor = Color.FromArgb(taskbar.BackColor.A, [CP].Windows11.Color_Index1)
                                start.BackColor = Color.FromArgb(start.BackColor.A, [CP].Windows11.Color_Index0)
                                ActionCenter.BackColor = Color.FromArgb(ActionCenter.BackColor.A, [CP].Windows11.Color_Index0)

                            Case ApplyAccentonTaskbar_Level.Taskbar
                                taskbar.BackColor = Color.FromArgb(taskbar.BackColor.A, [CP].Windows11.Color_Index1)
                                start.BackColor = Color.FromArgb(255, 255, 255)
                                ActionCenter.BackColor = Color.FromArgb(255, 255, 255)

                        End Select


                        ActionCenter.ActionCenterButton_Normal = [CP].Windows11.Color_Index4
                        ActionCenter.ActionCenterButton_Hover = [CP].Windows11.Color_Index5
                        ActionCenter.ActionCenterButton_Pressed = [CP].Windows11.Color_Index2
                        start.SearchBoxAccent = [CP].Windows11.Color_Index4
                        taskbar.AppUnderline = [CP].Windows11.Color_Index4

                        setting_icon_preview.ForeColor = [CP].Windows11.Color_Index3
                        lnk_preview.ForeColor = [CP].Windows11.Color_Index5
                End Select

                ReValidateLivePreview(pnl_preview)
#End Region
            Case MainFrm.WinVer.W10
#Region "Win10"
                XenonWindow1.AccentColor_Enabled = [CP].Windows10.ApplyAccentonTitlebars
                XenonWindow2.AccentColor_Enabled = [CP].Windows10.ApplyAccentonTitlebars

                XenonWindow1.AccentColor_Active = [CP].Windows10.Titlebar_Active
                XenonWindow2.AccentColor_Active = [CP].Windows10.Titlebar_Active

                XenonWindow1.AccentColor_Inactive = [CP].Windows10.Titlebar_Inactive
                XenonWindow2.AccentColor_Inactive = [CP].Windows10.Titlebar_Inactive

                XenonWindow1.DarkMode = Not [CP].Windows10.AppMode_Light
                XenonWindow2.DarkMode = Not [CP].Windows10.AppMode_Light

                Label8.ForeColor = If([CP].Windows10.AppMode_Light, Color.Black, Color.White)

                start.DarkMode = Not [CP].Windows10.WinMode_Light
                taskbar.DarkMode = Not [CP].Windows10.WinMode_Light
                ActionCenter.DarkMode = Not [CP].Windows10.WinMode_Light
                taskbar.Transparency = [CP].Windows10.Transparency
                start.Transparency = [CP].Windows10.Transparency
                ActionCenter.Transparency = [CP].Windows10.Transparency
                start.Top = taskbar.Top - start.Height

                If [CP].Windows10.Transparency Then
                    If Not [CP].Windows10.WinMode_Light Then
                        taskbar.BackColorAlpha = 150
                        start.BackColorAlpha = 150
                        ActionCenter.BackColorAlpha = 150
                    Else
                        taskbar.BackColorAlpha = 200
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



                ReValidateLivePreview(pnl_preview)
#End Region
            Case MainFrm.WinVer.W8
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

                XenonWindow1.AccentColor_Active = [CP].Windows7.ColorizationColor
                XenonWindow1.Win7ColorBal = [CP].Windows7.ColorizationColorBalance

                XenonWindow2.AccentColor_Active = [CP].Windows7.ColorizationColor
                XenonWindow2.Win7ColorBal = [CP].Windows7.ColorizationColorBalance

                taskbar.BackColor = [CP].Windows7.ColorizationColor
                taskbar.Win7ColorBal = [CP].Windows7.ColorizationColorBalance

                ReValidateLivePreview(pnl_preview)
#End Region
            Case MainFrm.WinVer.W7
#Region "Win7"

                Select Case [CP].Windows7.Theme
                    Case CP.AeroTheme.Aero
                        start.Transparency = True
                        start.Basic = False
                        taskbar.Transparency = True
                        taskbar.Basic = False
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
                            .Win7AeroOpaque = False
                            .BackColorAlpha = [CP].Windows7.ColorizationBlurBalance
                            .Win7ColorBal = [CP].Windows7.ColorizationColorBalance
                            .Win7GlowBal = [CP].Windows7.ColorizationAfterglowBalance
                            .BackColor = [CP].Windows7.ColorizationColor
                            .BackColor2 = [CP].Windows7.ColorizationAfterglow
                            .NoisePower = [CP].Windows7.ColorizationGlassReflectionIntensity
                        End With
                        With taskbar
                            .Win7AeroOpaque = False
                            .BackColorAlpha = [CP].Windows7.ColorizationBlurBalance
                            .Win7ColorBal = [CP].Windows7.ColorizationColorBalance
                            .Win7GlowBal = [CP].Windows7.ColorizationAfterglowBalance
                            .BackColor = [CP].Windows7.ColorizationColor
                            .BackColor2 = [CP].Windows7.ColorizationAfterglow
                            .NoisePower = [CP].Windows7.ColorizationGlassReflectionIntensity
                        End With

                    Case CP.AeroTheme.AeroOpaque
                        start.Transparency = True
                        start.Basic = False
                        taskbar.Transparency = True
                        taskbar.Basic = False

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
                            .Win7AeroOpaque = True
                            .BackColorAlpha = [CP].Windows7.ColorizationColorBalance
                            .BackColor = [CP].Windows7.ColorizationColor
                            .BackColor2 = [CP].Windows7.ColorizationColor
                            .NoisePower = [CP].Windows7.ColorizationGlassReflectionIntensity
                        End With
                        With start
                            .Win7AeroOpaque = True
                            .BackColorAlpha = [CP].Windows7.ColorizationColorBalance
                            .BackColor = [CP].Windows7.ColorizationColor
                            .BackColor2 = [CP].Windows7.ColorizationColor
                            .NoisePower = [CP].Windows7.ColorizationGlassReflectionIntensity
                        End With

                    Case CP.AeroTheme.Basic
#Region "Basic"
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
                        start.NoisePower = 0

                        start.Basic = True
                        taskbar.Basic = True
                        start.Refresh()
                        taskbar.Refresh()

#End Region

                    Case CP.AeroTheme.Classic

                End Select

                ReValidateLivePreview(pnl_preview)
#End Region
        End Select
    End Sub

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

    Sub Adjust_Preview()

        tabs_preview.Visible = False
        Panel5.Visible = (MainFrm.PreviewConfig = WinVer.W11 Or MainFrm.PreviewConfig = WinVer.W10)
        lnk_preview.Visible = (MainFrm.PreviewConfig = WinVer.W11 Or MainFrm.PreviewConfig = WinVer.W10)
        start.Visible = (MainFrm.PreviewConfig = WinVer.W11 Or MainFrm.PreviewConfig = WinVer.W10)
        ActionCenter.Visible = (MainFrm.PreviewConfig = WinVer.W11 Or MainFrm.PreviewConfig = WinVer.W10)
        tabs_preview.SelectedIndex = If(MainFrm.PreviewConfig = WinVer.W7 AndAlso CP.Windows7.Theme = AeroTheme.Classic, 1, 0)

        Select Case MainFrm.PreviewConfig
            Case WinVer.W11
                ActionCenter.UseItAsTaskbar_Version = XenonAcrylic.TaskbarVersion.Eleven
                taskbar.UseItAsTaskbar_Version = XenonAcrylic.TaskbarVersion.Eleven
                start.UseItAsTaskbar_Version = XenonAcrylic.TaskbarVersion.Eleven
                XenonWindow1.Preview = XenonWindow.Preview_Enum.W11

            Case WinVer.W10
                ActionCenter.UseItAsTaskbar_Version = XenonAcrylic.TaskbarVersion.Ten
                taskbar.UseItAsTaskbar_Version = XenonAcrylic.TaskbarVersion.Ten
                start.UseItAsTaskbar_Version = XenonAcrylic.TaskbarVersion.Ten
                XenonWindow1.Preview = XenonWindow.Preview_Enum.W10

            Case WinVer.W8
                taskbar.UseItAsTaskbar_Version = XenonAcrylic.TaskbarVersion.Eight
                XenonWindow1.Preview = If(CP.Windows8.Theme = AeroTheme.AeroLite, XenonWindow.Preview_Enum.W8Lite, XenonWindow.Preview_Enum.W8)

            Case WinVer.W7
                taskbar.UseItAsTaskbar_Version = XenonAcrylic.TaskbarVersion.Seven
                start.UseItAsTaskbar_Version = XenonAcrylic.TaskbarVersion.Seven

                Select Case CP.Windows7.Theme
                    Case AeroTheme.Aero
                        XenonWindow1.Preview = XenonWindow.Preview_Enum.W7Aero
                        tabs_preview.SelectedIndex = 0

                    Case AeroTheme.AeroOpaque
                        XenonWindow1.Preview = XenonWindow.Preview_Enum.W7Opaque
                        tabs_preview.SelectedIndex = 0

                    Case AeroTheme.Basic
                        XenonWindow1.Preview = XenonWindow.Preview_Enum.W7Basic
                        tabs_preview.SelectedIndex = 0

                    Case AeroTheme.Classic
                        tabs_preview.SelectedIndex = 1

                End Select

        End Select

        XenonWindow2.Preview = XenonWindow1.Preview

        Select Case MainFrm.PreviewConfig
            Case WinVer.W11
                ActionCenter.Dock = Nothing
                ActionCenter.RoundedCorners = True
                ActionCenter.BlurPower = 7
                ActionCenter.NoisePower = 0.2
                '########################
                taskbar.BlurPower = 12
                '########################
                start.RoundedCorners = True
                start.BlurPower = 7
                start.NoisePower = 0.2
                '########################
                ActionCenter.Size = New Size(120, 85)
                ActionCenter.Location = New Point(398, 161)
                '########################

                taskbar.Height = 42

                start.Size = New Size(135, 200)
                start.Location = New Point(9, taskbar.Bottom - 42 - start.Height - 9)


            Case WinVer.W10
                ActionCenter.Dock = DockStyle.Right
                ActionCenter.RoundedCorners = False
                ActionCenter.BlurPower = 7
                ActionCenter.NoisePower = 0.2
                '########################
                taskbar.BlurPower = 12
                '########################
                start.RoundedCorners = False
                start.BlurPower = 7
                start.NoisePower = 0.2
                '########################

                taskbar.Height = 35

                start.Size = New Size(182, 201)
                start.Left = 0
                start.Top = taskbar.Bottom - taskbar.Height - start.Height

            Case WinVer.W8
                Panel5.Visible = False
                lnk_preview.Visible = False
                start.Visible = False
                ActionCenter.Visible = False
                taskbar.BlurPower = 0
                taskbar.Height = 34

                start.BlurPower = 0
                start.Top = taskbar.Top - start.Height
                start.Left = 0

            Case WinVer.W7
                Panel5.Visible = False
                lnk_preview.Visible = False
                ActionCenter.Visible = False

                taskbar.BlurPower = 1
                taskbar.NoisePower = CP.Windows7.ColorizationGlassReflectionIntensity / 100
                taskbar.Height = 34

                start.RoundedCorners = True
                start.BlurPower = 1
                start.NoisePower = 0.5
                start.Width = 136
                start.Height = 191
                start.NoisePower = CP.Windows7.ColorizationGlassReflectionIntensity / 100
                start.Left = 0
                start.Top = taskbar.Top - start.Height
        End Select

        If MainFrm.PreviewConfig = WinVer.W10 Or MainFrm.PreviewConfig = WinVer.W11 Then
            XenonWindow1.Top = start.Top - If(MainFrm.PreviewConfig = WinVer.W11, 30, 35)
            XenonWindow1.Left = start.Right + If(MainFrm.PreviewConfig = WinVer.W11, 30, 15)

            XenonWindow2.Top = XenonWindow1.Bottom + 1
            XenonWindow2.Left = XenonWindow1.Left
        Else
            XenonWindow1.Top = 10
            XenonWindow1.Left = (XenonWindow1.Parent.Width - XenonWindow1.Width) / 2

            XenonWindow2.Top = XenonWindow1.Bottom + 5
            XenonWindow2.Left = XenonWindow1.Left
        End If

        ReValidateLivePreview(tabs_preview)

        tabs_preview.Visible = True
    End Sub


    Sub SetMetics(CP As CP)
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
        Dim _Padding As New Padding(iP, iT, iP, iP)

        For Each RW As RetroWindow In pnlRetroPreview.Controls.OfType(Of RetroWindow)
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
        Menu_Window.Left = RetroWindow2.Left + menucontainer0.Left + RetroPanel1.Left + +3

        RetroWindow3.Top = RetroWindow2.Top + RetroTextBox1.Top + RetroTextBox1.Font.Height + 10
        RetroWindow3.Left = RetroWindow2.Left + RetroTextBox1.Left + 15

        RetroLabel13.Top = RetroWindow4.Top + RetroWindow4.Metrics_CaptionHeight + 2
        RetroLabel13.Left = RetroWindow4.Right - RetroWindow4.Metrics_CaptionWidth - 2

    End Sub

    Sub ApplyRetroPreview([CP] As CP)
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
        For Each RW As RetroWindow In pnlRetroPreview.Controls.OfType(Of RetroWindow)
            For Each RB As RetroButton In RW.Controls.OfType(Of RetroButton)
                RB.WindowFrame = c
            Next
        Next
        For Each RB As RetroButton In RetroPanel2.Controls.OfType(Of RetroButton)
            RB.WindowFrame = c
        Next

        c = [CP].Win32.ButtonFace
        For Each RW As RetroWindow In pnlRetroPreview.Controls.OfType(Of RetroWindow)
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
        For Each RW As RetroWindow In pnlRetroPreview.Controls.OfType(Of RetroWindow)
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
        For Each RW As RetroWindow In pnlRetroPreview.Controls.OfType(Of RetroWindow)
            RW.ButtonHilight = c
            For Each RB As RetroButton In RW.Controls.OfType(Of RetroButton)
                RB.ButtonHilight = c
            Next
        Next
        For Each RB As RetroButton In RetroPanel2.Controls.OfType(Of RetroButton)
            RB.ButtonHilight = c
        Next
        For Each RB As RetroPanelRaised In pnlRetroPreview.Controls.OfType(Of RetroPanelRaised)
            RB.ButtonHilight = c
        Next
        RetroTextBox1.ButtonHilight = c
        RetroPanel1.ButtonHilight = c
        RetroPanel2.ButtonHilight = c
        Menu_Window.ButtonHilight = c

        c = [CP].Win32.ButtonLight
        For Each RW As RetroWindow In pnlRetroPreview.Controls.OfType(Of RetroWindow)
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
        For Each RW As RetroWindow In pnlRetroPreview.Controls.OfType(Of RetroWindow)
            RW.ButtonShadow = c
            For Each RB As RetroButton In RW.Controls.OfType(Of RetroButton)
                RB.ButtonShadow = c
            Next
        Next
        For Each RB As RetroButton In RetroPanel2.Controls.OfType(Of RetroButton)
            RB.ButtonShadow = c
        Next
        For Each RB As RetroPanelRaised In pnlRetroPreview.Controls.OfType(Of RetroPanelRaised)
            RB.ButtonShadow = c
        Next
        RetroTextBox1.ButtonShadow = c
        RetroPanel1.ButtonShadow = c
        RetroTextBox1.Invalidate()
        Menu_Window.ButtonShadow = c

        c = [CP].Win32.ButtonText
        For Each RW As RetroWindow In pnlRetroPreview.Controls.OfType(Of RetroWindow)
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
        pnlRetroPreview.BackColor = c

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

        For Each RW As RetroWindow In pnlRetroPreview.Controls.OfType(Of RetroWindow)
            RW.Invalidate()
            For Each RB As RetroButton In RW.Controls.OfType(Of RetroButton)
                RB.Invalidate()
            Next
        Next

        For Each RB As RetroButton In RetroPanel2.Controls.OfType(Of RetroButton)
            RB.Invalidate()
        Next

        Refresh17BitPreference([CP])

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


End Class