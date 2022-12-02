Imports System.Reflection.Emit
Imports System.Runtime.InteropServices
Imports WinPaletter.CP
Imports WinPaletter.XenonCore

Public Class dragPreviewer

    Public CP As CP
    Public File As String

    Private Sub dragPreviewer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SuspendLayout()

        MainFrm.MakeItDoubleBuffered(Me)
        MainFrm.MakeItDoubleBuffered(pnl_preview)
        MainFrm.MakeItDoubleBuffered(pnlRetroPreview)

        Opacity = 0
        Visible = False

        pnl_preview.BackgroundImage = My.Application.Wallpaper

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

        'pnl_preview.Visible = True : PictureBox1.Image = GetControlImage(pnl_preview) : pnl_preview.Visible = False

        Acrylism.EnableBlur(Me, True)

        ResumeLayout()
    End Sub

    Private Sub dragPreviewer_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Opacity = 1
        Visible = True
        Me.Invalidate()
    End Sub

    Sub ApplyLivePreviewFromCP([CP] As CP)
        If Not MainFrm.PreviewConfig = MainFrm.WinVer.Seven And Not MainFrm.PreviewConfig = MainFrm.WinVer.Eight Then
            XenonWindow1.Active = True
            XenonWindow2.Active = False

            XenonWindow1.AccentColor_Enabled = [CP].Windows10x.ApplyAccentonTitlebars
            XenonWindow2.AccentColor_Enabled = [CP].Windows10x.ApplyAccentonTitlebars

            XenonWindow1.AccentColor_Active = [CP].Windows10x.Titlebar_Active
            XenonWindow2.AccentColor_Active = [CP].Windows10x.Titlebar_Active

            XenonWindow1.AccentColor_Inactive = [CP].Windows10x.Titlebar_Inactive
            XenonWindow2.AccentColor_Inactive = [CP].Windows10x.Titlebar_Inactive

            XenonWindow1.DarkMode = Not [CP].Windows10x.AppMode_Light
            XenonWindow2.DarkMode = Not [CP].Windows10x.AppMode_Light

            Label8.ForeColor = If([CP].Windows10x.AppMode_Light, Color.Black, Color.White)

        Else
            XenonWindow1.Active = True
            XenonWindow2.Active = False
        End If

        Select Case MainFrm.PreviewConfig
            Case MainFrm.WinVer.Eleven
#Region "Win11"
                start.DarkMode = Not [CP].Windows10x.WinMode_Light
                taskbar.DarkMode = Not [CP].Windows10x.WinMode_Light
                ActionCenter.DarkMode = Not [CP].Windows10x.WinMode_Light

                taskbar.Transparency = [CP].Windows10x.Transparency
                start.Transparency = [CP].Windows10x.Transparency
                ActionCenter.Transparency = [CP].Windows10x.Transparency

                start.Top = taskbar.Top - start.Height - 9

                Select Case Not [CP].Windows10x.WinMode_Light
                    Case True   ''''''''''Dark
                        taskbar.BackColorAlpha = 130
                        start.BackColorAlpha = 130
                        ActionCenter.BackColorAlpha = 130

                        Select Case [CP].Windows10x.ApplyAccentonTaskbar
                            Case ApplyAccentonTaskbar_Level.None
                                taskbar.BackColor = Color.FromArgb(55, 55, 55)
                                start.BackColor = Color.FromArgb(40, 40, 40)
                                ActionCenter.BackColor = Color.FromArgb(55, 55, 55)

                            Case ApplyAccentonTaskbar_Level.Taskbar_Start_AC
                                taskbar.BackColor = Color.FromArgb(taskbar.BackColor.A, [CP].Windows10x.Color_Index5)
                                start.BackColor = Color.FromArgb(start.BackColor.A, [CP].Windows10x.Color_Index5)
                                ActionCenter.BackColor = Color.FromArgb(ActionCenter.BackColor.A, [CP].Windows10x.Color_Index5)

                            Case ApplyAccentonTaskbar_Level.Taskbar
                                taskbar.BackColor = Color.FromArgb(taskbar.BackColor.A, [CP].Windows10x.Color_Index5)
                                start.BackColor = Color.FromArgb(40, 40, 40)
                                ActionCenter.BackColor = Color.FromArgb(55, 55, 55)

                        End Select


                        ActionCenter.ActionCenterButton_Normal = [CP].Windows10x.Color_Index1
                        ActionCenter.ActionCenterButton_Hover = [CP].Windows10x.Color_Index0
                        ActionCenter.ActionCenterButton_Pressed = [CP].Windows10x.Color_Index2
                        start.SearchBoxAccent = [CP].Windows10x.Color_Index1
                        taskbar.AppUnderline = [CP].Windows10x.Color_Index1

                        setting_icon_preview.ForeColor = [CP].Windows10x.Color_Index3
                        lnk_preview.ForeColor = [CP].Windows10x.Color_Index0

                    Case False   ''''''''''Light
                        taskbar.BackColorAlpha = 180
                        start.BackColorAlpha = 180
                        ActionCenter.BackColorAlpha = 180

                        Select Case [CP].Windows10x.ApplyAccentonTaskbar
                            Case ApplyAccentonTaskbar_Level.None
                                taskbar.BackColor = Color.FromArgb(255, 255, 255)
                                start.BackColor = Color.FromArgb(255, 255, 255)
                                ActionCenter.BackColor = Color.FromArgb(255, 255, 255)

                            Case ApplyAccentonTaskbar_Level.Taskbar_Start_AC
                                taskbar.BackColor = Color.FromArgb(taskbar.BackColor.A, [CP].Windows10x.Color_Index1)
                                start.BackColor = Color.FromArgb(start.BackColor.A, [CP].Windows10x.Color_Index0)
                                ActionCenter.BackColor = Color.FromArgb(ActionCenter.BackColor.A, [CP].Windows10x.Color_Index0)

                            Case ApplyAccentonTaskbar_Level.Taskbar
                                taskbar.BackColor = Color.FromArgb(taskbar.BackColor.A, [CP].Windows10x.Color_Index1)
                                start.BackColor = Color.FromArgb(255, 255, 255)
                                ActionCenter.BackColor = Color.FromArgb(255, 255, 255)

                        End Select


                        ActionCenter.ActionCenterButton_Normal = [CP].Windows10x.Color_Index4
                        ActionCenter.ActionCenterButton_Hover = [CP].Windows10x.Color_Index5
                        ActionCenter.ActionCenterButton_Pressed = [CP].Windows10x.Color_Index2
                        start.SearchBoxAccent = [CP].Windows10x.Color_Index4
                        taskbar.AppUnderline = [CP].Windows10x.Color_Index4

                        setting_icon_preview.ForeColor = [CP].Windows10x.Color_Index3
                        lnk_preview.ForeColor = [CP].Windows10x.Color_Index5
                End Select

                ReValidateLivePreview(pnl_preview)
#End Region
            Case MainFrm.WinVer.Ten
#Region "Win10"

                start.DarkMode = Not [CP].Windows10x.WinMode_Light
                taskbar.DarkMode = Not [CP].Windows10x.WinMode_Light
                ActionCenter.DarkMode = Not [CP].Windows10x.WinMode_Light
                taskbar.Transparency = [CP].Windows10x.Transparency
                start.Transparency = [CP].Windows10x.Transparency
                ActionCenter.Transparency = [CP].Windows10x.Transparency
                start.Top = taskbar.Top - start.Height

                If [CP].Windows10x.Transparency Then
                    If Not [CP].Windows10x.WinMode_Light Then
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

                Select Case Not [CP].Windows10x.WinMode_Light
                    Case True

                        If [CP].Windows10x.Transparency Then
                            Select Case [CP].Windows10x.ApplyAccentonTaskbar
                                Case ApplyAccentonTaskbar_Level.None
                                    taskbar.BackColor = Color.FromArgb(16, 16, 16)
                                    taskbar.StartColor = Color.FromArgb(150, 150, 150, 150)
                                    start.BackColor = Color.FromArgb(31, 31, 31)
                                    ActionCenter.BackColor = Color.FromArgb(31, 31, 31)

                                    taskbar.AppBackground = Color.FromArgb(150, 150, 150, 150)
                                    ActionCenter.LinkColor = [CP].Windows10x.Color_Index0
                                    taskbar.AppUnderline = [CP].Windows10x.Color_Index1
                                    setting_icon_preview.ForeColor = [CP].Windows10x.Color_Index3
                                    lnk_preview.ForeColor = [CP].Windows10x.Color_Index3
                                    ActionCenter.ActionCenterButton_Normal = [CP].Windows10x.Color_Index3

                                Case ApplyAccentonTaskbar_Level.Taskbar
                                    taskbar.BackColor = [CP].Windows10x.Color_Index6
                                    taskbar.StartColor = Color.FromArgb(0, 0, 0, 0)
                                    start.BackColor = Color.FromArgb(31, 31, 31)
                                    ActionCenter.BackColor = Color.FromArgb(31, 31, 31)

                                    taskbar.AppBackground = Color.FromArgb(150, [CP].Windows10x.Color_Index3)
                                    ActionCenter.LinkColor = [CP].Windows10x.Color_Index0
                                    taskbar.AppUnderline = [CP].Windows10x.Color_Index1
                                    setting_icon_preview.ForeColor = [CP].Windows10x.Color_Index3
                                    lnk_preview.ForeColor = [CP].Windows10x.Color_Index3
                                    ActionCenter.ActionCenterButton_Normal = [CP].Windows10x.Color_Index3

                                Case ApplyAccentonTaskbar_Level.Taskbar_Start_AC
                                    taskbar.BackColor = [CP].Windows10x.Color_Index6
                                    taskbar.StartColor = Color.FromArgb(0, 0, 0, 0)
                                    start.BackColor = [CP].Windows10x.Color_Index4
                                    ActionCenter.BackColor = [CP].Windows10x.Color_Index4

                                    taskbar.AppBackground = Color.FromArgb(150, [CP].Windows10x.Color_Index3)
                                    ActionCenter.LinkColor = [CP].Windows10x.Color_Index0
                                    taskbar.AppUnderline = [CP].Windows10x.Color_Index1
                                    setting_icon_preview.ForeColor = [CP].Windows10x.Color_Index3
                                    lnk_preview.ForeColor = [CP].Windows10x.Color_Index3
                                    ActionCenter.ActionCenterButton_Normal = [CP].Windows10x.Color_Index3

                            End Select

                        Else
                            Select Case [CP].Windows10x.ApplyAccentonTaskbar
                                Case ApplyAccentonTaskbar_Level.None
                                    taskbar.BackColor = Color.FromArgb(16, 16, 16)
                                    taskbar.StartColor = Color.FromArgb(31, 31, 31)
                                    start.BackColor = Color.FromArgb(31, 31, 31)
                                    ActionCenter.BackColor = Color.FromArgb(31, 31, 31)

                                Case ApplyAccentonTaskbar_Level.Taskbar
                                    taskbar.BackColor = [CP].Windows10x.Color_Index5
                                    taskbar.StartColor = [CP].Windows10x.Color_Index4
                                    start.BackColor = Color.FromArgb(31, 31, 31)
                                    ActionCenter.BackColor = Color.FromArgb(31, 31, 31)

                                Case ApplyAccentonTaskbar_Level.Taskbar_Start_AC
                                    taskbar.BackColor = [CP].Windows10x.Color_Index5
                                    taskbar.StartColor = [CP].Windows10x.Color_Index4
                                    start.BackColor = [CP].Windows10x.Color_Index4
                                    ActionCenter.BackColor = [CP].Windows10x.Color_Index4
                            End Select

                            If [CP].Windows10x.ApplyAccentonTaskbar = ApplyAccentonTaskbar_Level.None Then
                                taskbar.AppBackground = Color.FromArgb(150, 100, 100, 100)
                            Else
                                taskbar.AppBackground = [CP].Windows10x.Color_Index4
                            End If

                            ActionCenter.LinkColor = [CP].Windows10x.Color_Index0
                            taskbar.AppUnderline = [CP].Windows10x.Color_Index1
                            setting_icon_preview.ForeColor = [CP].Windows10x.Color_Index3
                            lnk_preview.ForeColor = [CP].Windows10x.Color_Index3
                            ActionCenter.ActionCenterButton_Normal = [CP].Windows10x.Color_Index3

                        End If

                    Case False
                        If [CP].Windows10x.Transparency Then

                            Select Case [CP].Windows10x.ApplyAccentonTaskbar
                                Case ApplyAccentonTaskbar_Level.None
                                    taskbar.BackColor = Color.FromArgb(238, 238, 238)
                                    taskbar.StartColor = Color.Transparent
                                    start.BackColor = Color.FromArgb(228, 228, 228)
                                    ActionCenter.BackColor = Color.FromArgb(228, 228, 228)

                                    taskbar.AppBackground = Color.FromArgb(150, 238, 238, 238)
                                    ActionCenter.LinkColor = [CP].Windows10x.Color_Index6
                                    taskbar.AppUnderline = [CP].Windows10x.Color_Index3
                                    setting_icon_preview.ForeColor = [CP].Windows10x.Color_Index3
                                    lnk_preview.ForeColor = [CP].Windows10x.Color_Index3
                                    ActionCenter.ActionCenterButton_Normal = [CP].Windows10x.Color_Index3

                                Case ApplyAccentonTaskbar_Level.Taskbar
                                    taskbar.BackColor = [CP].Windows10x.Color_Index6
                                    taskbar.StartColor = Color.Transparent
                                    start.BackColor = Color.FromArgb(228, 228, 228)
                                    ActionCenter.BackColor = Color.FromArgb(228, 228, 228)

                                    taskbar.AppBackground = Color.FromArgb(150, [CP].Windows10x.Color_Index3)
                                    ActionCenter.LinkColor = [CP].Windows10x.Color_Index6
                                    taskbar.AppUnderline = [CP].Windows10x.Color_Index1
                                    setting_icon_preview.ForeColor = [CP].Windows10x.Color_Index3
                                    lnk_preview.ForeColor = [CP].Windows10x.Color_Index3
                                    ActionCenter.ActionCenterButton_Normal = [CP].Windows10x.Color_Index3

                                Case ApplyAccentonTaskbar_Level.Taskbar_Start_AC
                                    taskbar.BackColor = [CP].Windows10x.Color_Index6
                                    taskbar.StartColor = Color.Transparent
                                    start.BackColor = [CP].Windows10x.Color_Index4
                                    ActionCenter.BackColor = [CP].Windows10x.Color_Index4

                                    taskbar.AppBackground = Color.FromArgb(150, [CP].Windows10x.Color_Index3)
                                    ActionCenter.LinkColor = [CP].Windows10x.Color_Index0
                                    taskbar.AppUnderline = [CP].Windows10x.Color_Index1
                                    setting_icon_preview.ForeColor = [CP].Windows10x.Color_Index3
                                    lnk_preview.ForeColor = [CP].Windows10x.Color_Index3
                                    ActionCenter.ActionCenterButton_Normal = [CP].Windows10x.Color_Index3

                            End Select

                        Else

                            Select Case [CP].Windows10x.ApplyAccentonTaskbar
                                Case ApplyAccentonTaskbar_Level.None
                                    taskbar.BackColor = Color.FromArgb(238, 238, 238)
                                    taskbar.StartColor = Color.FromArgb(241, 241, 241)
                                    start.BackColor = Color.FromArgb(228, 228, 228)
                                    ActionCenter.BackColor = Color.FromArgb(228, 228, 228)

                                    taskbar.AppBackground = Color.FromArgb(252, 252, 252)
                                    ActionCenter.LinkColor = [CP].Windows10x.Color_Index6
                                    taskbar.AppUnderline = [CP].Windows10x.Color_Index3
                                    setting_icon_preview.ForeColor = [CP].Windows10x.Color_Index3
                                    lnk_preview.ForeColor = [CP].Windows10x.Color_Index3
                                    ActionCenter.ActionCenterButton_Normal = [CP].Windows10x.Color_Index3

                                Case ApplyAccentonTaskbar_Level.Taskbar
                                    taskbar.BackColor = [CP].Windows10x.Color_Index5
                                    taskbar.StartColor = [CP].Windows10x.Color_Index4
                                    start.BackColor = Color.FromArgb(228, 228, 228)
                                    ActionCenter.BackColor = Color.FromArgb(228, 228, 228)

                                    taskbar.AppBackground = [CP].Windows10x.Color_Index4
                                    ActionCenter.LinkColor = [CP].Windows10x.Color_Index6
                                    taskbar.AppUnderline = [CP].Windows10x.Color_Index1
                                    setting_icon_preview.ForeColor = [CP].Windows10x.Color_Index3
                                    lnk_preview.ForeColor = [CP].Windows10x.Color_Index3
                                    ActionCenter.ActionCenterButton_Normal = [CP].Windows10x.Color_Index3

                                Case ApplyAccentonTaskbar_Level.Taskbar_Start_AC
                                    taskbar.BackColor = [CP].Windows10x.Color_Index5
                                    taskbar.StartColor = [CP].Windows10x.Color_Index4
                                    start.BackColor = [CP].Windows10x.Color_Index4
                                    ActionCenter.BackColor = [CP].Windows10x.Color_Index4


                                    taskbar.AppBackground = [CP].Windows10x.Color_Index4
                                    ActionCenter.LinkColor = [CP].Windows10x.Color_Index0
                                    taskbar.AppUnderline = [CP].Windows10x.Color_Index1
                                    setting_icon_preview.ForeColor = [CP].Windows10x.Color_Index3
                                    lnk_preview.ForeColor = [CP].Windows10x.Color_Index3
                                    ActionCenter.ActionCenterButton_Normal = [CP].Windows10x.Color_Index3

                            End Select

                        End If
                End Select



                ReValidateLivePreview(pnl_preview)
#End Region
            Case MainFrm.WinVer.Eight
#Region "Win8.1"
                Select Case [CP].Metro_Theme
                    Case CP.AeroTheme.Aero
                        XenonWindow1.Win8Lite = False
                        XenonWindow2.Win8Lite = False
                        taskbar.Transparency = True
                        taskbar.BackColorAlpha = 100
                    Case CP.AeroTheme.AeroLite
                        XenonWindow1.Win8Lite = True
                        XenonWindow2.Win8Lite = True
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
            Case MainFrm.WinVer.Seven
#Region "Win7"

                Select Case [CP].Windows7.Theme
                    Case CP.AeroTheme.Aero
                        start.Transparency = True
                        start.Basic = False
                        taskbar.Transparency = True
                        taskbar.Basic = False
                        With XenonWindow1
                            .Win7 = True
                            .Win7Aero = True
                            .Win7AeroOpaque = False
                            .Win7Basic = False
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
                            .Win7 = True
                            .Win7Aero = True
                            .Win7AeroOpaque = False
                            .Win7Basic = False
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
                            .Win7 = True
                            .Win7Aero = False
                            .Win7AeroOpaque = True
                            .Win7Basic = False
                            .Win7Alpha = [CP].Windows7.ColorizationColorBalance
                            .AccentColor_Active = [CP].Windows7.ColorizationColor
                            .AccentColor_Inactive = [CP].Windows7.ColorizationColor
                            .Win7Noise = [CP].Windows7.ColorizationGlassReflectionIntensity
                        End With
                        With XenonWindow2
                            .Win7 = True
                            .Win7Aero = False
                            .Win7AeroOpaque = True
                            .Win7Basic = False
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
                            .Win7 = True
                            .Win7Aero = False
                            .Win7AeroOpaque = False
                            .Win7Basic = True
                            .Win7Alpha = 100
                            .AccentColor_Active = Color.FromArgb(166, 190, 218)
                            .Win7Noise = 0
                        End With

                        With XenonWindow2
                            .Win7 = True
                            .Win7Aero = False
                            .Win7AeroOpaque = False
                            .Win7Basic = True
                            .Win7Alpha = 100
                            .AccentColor_Inactive = Color.FromArgb(166, 190, 218)
                            .Win7Noise = 0
                        End With

                        start.Transparency = False
                        start.Basic = True
                        start.NoisePower = 0
                        taskbar.Transparency = False
                        taskbar.Basic = True
                        start.NoisePower = 0


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
        Panel5.Visible = True
        lnk_preview.Visible = True

        start.Visible = True
        taskbar.Visible = True
        XenonWindow1.Visible = True
        XenonWindow2.Visible = True
        ActionCenter.Visible = True

        XenonWindow1.Win7 = False
        XenonWindow2.Win7 = False
        XenonWindow1.Win8 = False
        XenonWindow2.Win8 = False
        XenonWindow1.Win8Lite = False
        XenonWindow2.Win8Lite = False

        Select Case MainFrm.PreviewConfig
            Case MainFrm.WinVer.Eleven
                ActionCenter.Visible = True
                ActionCenter.Size = New Size(120, 85)
                ActionCenter.Location = New Point(398, 161)
                ActionCenter.Dock = Nothing
                ActionCenter.RoundedCorners = True

                taskbar.Height = 42
                taskbar.UseItAsTaskbar_Version = XenonAcrylic.TaskbarVersion.Eleven
                taskbar.BlurPower = 12

                start.Visible = True
                start.Size = New Size(135, 200)
                start.Location = New Point(7, 46)
                start.RoundedCorners = True
                start.UseItAsTaskbar_Version = XenonAcrylic.TaskbarVersion.Eleven
                start.BlurPower = 7
                start.NoisePower = 0.2

                XenonWindow1.RoundedCorners = True
                XenonWindow2.RoundedCorners = True

            Case MainFrm.WinVer.Ten
                ActionCenter.Visible = True
                ActionCenter.Dock = DockStyle.Right
                ActionCenter.RoundedCorners = False

                taskbar.Height = 35
                taskbar.UseItAsTaskbar_Version = XenonAcrylic.TaskbarVersion.Ten
                taskbar.BlurPower = 12

                start.Visible = True
                start.Size = New Size(182, 201)
                start.Location = New Point(0, 59)
                start.RoundedCorners = False
                start.UseItAsTaskbar_Version = XenonAcrylic.TaskbarVersion.Ten
                start.BlurPower = 7
                start.NoisePower = 0.2

                XenonWindow1.RoundedCorners = False
                XenonWindow2.RoundedCorners = False

            Case MainFrm.WinVer.Eight
                Panel5.Visible = False
                lnk_preview.Visible = False

                start.Visible = False
                taskbar.Visible = True
                XenonWindow1.Visible = True
                XenonWindow2.Visible = True
                ActionCenter.Visible = False
                XenonWindow1.Active = True
                XenonWindow2.Active = False
                XenonWindow1.Win8 = True
                XenonWindow2.Win8 = True
                taskbar.UseItAsTaskbar_Version = XenonAcrylic.TaskbarVersion.Eight
                taskbar.BlurPower = 0
                taskbar.Height = 34

                start.Visible = False
                start.BlurPower = 0
                start.Top = taskbar.Top - start.Height
                start.Left = 0

            Case MainFrm.WinVer.Seven

                If CP.Windows7.Theme = CP.AeroTheme.Classic Then
                    start.Visible = False
                    taskbar.Visible = False
                    XenonWindow1.Visible = False
                    XenonWindow2.Visible = False
                    ActionCenter.Visible = False
                Else
                    start.Visible = True
                    taskbar.Visible = True
                    XenonWindow1.Visible = True
                    XenonWindow2.Visible = True
                    ActionCenter.Visible = False
                End If

                Panel5.Visible = False
                lnk_preview.Visible = False
                taskbar.UseItAsTaskbar_Version = XenonAcrylic.TaskbarVersion.Seven
                taskbar.BlurPower = 1
                taskbar.NoisePower = CP.Windows7.ColorizationGlassReflectionIntensity / 100
                taskbar.Height = 34

                start.UseItAsTaskbar_Version = XenonAcrylic.TaskbarVersion.Seven
                start.RoundedCorners = True
                start.BlurPower = 1
                start.NoisePower = 0.5
                start.Left = 0
                start.Width = 136
                start.Height = 191
                start.NoisePower = CP.Windows7.ColorizationGlassReflectionIntensity / 100
                start.Top = taskbar.Top - start.Height
        End Select

        If MainFrm.PreviewConfig = MainFrm.WinVer.Ten Or MainFrm.PreviewConfig = MainFrm.WinVer.Eleven Then
            XenonWindow1.Top = start.Top - If(MainFrm.PreviewConfig = MainFrm.WinVer.Eleven, 30, 35)
            XenonWindow1.Left = start.Right + If(MainFrm.PreviewConfig = MainFrm.WinVer.Eleven, 30, 15)

            XenonWindow2.Top = XenonWindow1.Bottom + 1
            XenonWindow2.Left = XenonWindow1.Left
        Else
            XenonWindow1.Top = 10
            XenonWindow1.Left = (XenonWindow1.Parent.Width - XenonWindow1.Width) / 2

            XenonWindow2.Top = XenonWindow1.Bottom + 5
            XenonWindow2.Left = XenonWindow1.Left
        End If

            XenonWindow1.Refresh()
        XenonWindow2.Refresh()

        ReValidateLivePreview(pnl_preview)
    End Sub

    Sub SetMetics(CP As CP)
        RetroPanel2.Width = CP.WinMetrics.ScrollWidth
        menucontainer0.Height = CP.WinMetrics.MenuHeight

        menucontainer0.Height = Math.Max(CP.WinMetrics.MenuHeight, Metrics_Fonts.GetTitleTextHeight(CP.Fonts_MenuFont))

        RetroLabel1.Font = CP.Fonts_MenuFont
        RetroLabel2.Font = CP.Fonts_MenuFont
        RetroLabel3.Font = CP.Fonts_MenuFont

        RetroLabel9.Font = CP.Fonts_MenuFont
        RetroLabel5.Font = CP.Fonts_MenuFont
        RetroLabel6.Font = CP.Fonts_MenuFont

        menucontainer1.Height = Metrics_Fonts.GetTitleTextHeight(CP.Fonts_MenuFont) + 3
        highlight.Height = menucontainer1.Height + 1
        menucontainer3.Height = menucontainer1.Height + 1
        Menu.Height = menucontainer1.Height + highlight.Height + menucontainer3.Height + Menu.Padding.Top + Menu.Padding.Bottom

        RetroLabel4.Font = CP.Fonts_MessageFont

        RetroLabel1.Width = MeasureString(RetroLabel1.Text, CP.Fonts_MenuFont).Width + 5
        RetroLabel2.Width = MeasureString(RetroLabel2.Text, CP.Fonts_MenuFont).Width + 5
        RetroPanel1.Width = MeasureString(RetroLabel3.Text, CP.Fonts_MenuFont).Width + 5 + RetroPanel1.Padding.Left + RetroPanel1.Padding.Right

        Dim TitleTextH, TitleTextH_9, TitleTextH_Sum As Integer
        TitleTextH = MeasureString("ABCabc0123xYz.#", CP.Fonts_CaptionFont).Height
        TitleTextH_9 = MeasureString("ABCabc0123xYz.#", New Font(CP.Fonts_CaptionFont.Name, 9, Font.Style)).Height
        TitleTextH_Sum = Math.Max(0, TitleTextH - TitleTextH_9 - 5)

        Dim iP As Integer = 3 + CP.WinMetrics.PaddedBorderWidth + CP.WinMetrics.BorderWidth
        Dim iT As Integer = 4 + CP.WinMetrics.PaddedBorderWidth + CP.WinMetrics.BorderWidth + CP.WinMetrics.CaptionHeight + TitleTextH_Sum
        Dim _Padding As New Padding(iP, iT, iP, iP)

        For Each RW As RetroWindow In pnlRetroPreview.Controls.OfType(Of RetroWindow)
            If Not RW.UseItAsMenu Then
                RW.Metrics_BorderWidth = CP.WinMetrics.BorderWidth
                RW.Metrics_CaptionHeight = CP.WinMetrics.CaptionHeight
                RW.Metrics_CaptionWidth = CP.WinMetrics.CaptionWidth
                RW.Metrics_PaddedBorderWidth = CP.WinMetrics.PaddedBorderWidth
                RW.Font = CP.Fonts_CaptionFont

                RW.Padding = _Padding
            End If
        Next

        RetroWindow3.Height = 85 + CP.WinMetrics.PaddedBorderWidth + CP.WinMetrics.BorderWidth + RetroWindow3.GetTitleTextHeight
        RetroWindow2.Height = 120 + CP.WinMetrics.PaddedBorderWidth + CP.WinMetrics.BorderWidth + RetroWindow2.GetTitleTextHeight + CP.WinMetrics.MenuHeight

        RetroButton3.Height = CP.WinMetrics.CaptionHeight + RetroWindow2.GetTitleTextHeight - 4
        RetroButton4.Height = CP.WinMetrics.CaptionHeight + RetroWindow2.GetTitleTextHeight - 4
        RetroButton5.Height = CP.WinMetrics.CaptionHeight + RetroWindow2.GetTitleTextHeight - 4
        RetroButton6.Height = CP.WinMetrics.CaptionHeight + RetroWindow1.GetTitleTextHeight - 4
        RetroButton7.Height = CP.WinMetrics.CaptionHeight + RetroWindow1.GetTitleTextHeight - 4
        RetroButton8.Height = CP.WinMetrics.CaptionHeight + RetroWindow1.GetTitleTextHeight - 4
        RetroButton9.Height = CP.WinMetrics.CaptionHeight + RetroWindow4.GetTitleTextHeight - 4

        RetroButton3.Width = CP.WinMetrics.CaptionWidth - 2
        RetroButton4.Width = CP.WinMetrics.CaptionWidth - 2
        RetroButton5.Width = CP.WinMetrics.CaptionWidth - 2
        RetroButton8.Width = CP.WinMetrics.CaptionWidth - 2
        RetroButton7.Width = CP.WinMetrics.CaptionWidth - 2
        RetroButton6.Width = CP.WinMetrics.CaptionWidth - 2
        RetroButton9.Width = CP.WinMetrics.CaptionWidth - 2

        RetroButton3.Top = CP.WinMetrics.PaddedBorderWidth + CP.WinMetrics.BorderWidth + 5
        RetroButton4.Top = RetroButton3.Top
        RetroButton5.Top = RetroButton3.Top

        RetroButton8.Top = CP.WinMetrics.PaddedBorderWidth + CP.WinMetrics.BorderWidth + 5
        RetroButton7.Top = RetroButton8.Top
        RetroButton6.Top = RetroButton8.Top

        RetroButton9.Top = CP.WinMetrics.PaddedBorderWidth + CP.WinMetrics.BorderWidth + 5

        RetroButton3.Left = RetroWindow2.Width - RetroButton3.Width - CP.WinMetrics.PaddedBorderWidth - CP.WinMetrics.BorderWidth - 5
        RetroButton4.Left = RetroButton3.Left - 2 - RetroButton4.Width
        RetroButton5.Left = RetroButton4.Left - RetroButton5.Width

        RetroButton8.Left = RetroWindow1.Width - RetroButton8.Width - CP.WinMetrics.PaddedBorderWidth - CP.WinMetrics.BorderWidth - 5
        RetroButton7.Left = RetroButton8.Left - 2 - RetroButton7.Width
        RetroButton6.Left = RetroButton7.Left - RetroButton6.Width

        RetroButton9.Left = RetroWindow4.Width - RetroButton9.Width - CP.WinMetrics.PaddedBorderWidth - CP.WinMetrics.BorderWidth - 5

        Try
            Dim i0, iFx As Single
            i0 = Math.Abs(Math.Min(CP.WinMetrics.CaptionWidth, CP.WinMetrics.CaptionHeight))
            iFx = i0 / Math.Abs(Math.Min(Metrics_Fonts.XenonTrackbar2.Minimum, Metrics_Fonts.XenonTrackbar3.Minimum))
            Dim f As New Font("Marlett", 6.8 * iFx)
            RetroButton3.Font = f
            RetroButton4.Font = f
            RetroButton5.Font = f
            RetroButton6.Font = f
            RetroButton7.Font = f
            RetroButton8.Font = f
            RetroButton9.Font = f
        Catch

        End Try

        Menu.Top = RetroWindow2.Top + menucontainer0.Top + menucontainer0.Height
        Menu.Left = RetroWindow2.Left + menucontainer0.Left + RetroPanel1.Left + +3

        RetroWindow3.Top = RetroWindow2.Top + RetroTextBox1.Top + RetroTextBox1.Font.Height + 10
        RetroWindow3.Left = RetroWindow2.Left + RetroTextBox1.Left + 15

        RetroLabel13.Top = RetroWindow4.Top + RetroButton9.Bottom + 2
        RetroLabel13.Left = RetroWindow4.Right - RetroButton9.Width - 2

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
        Menu.ButtonFace = c

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
        Menu.ButtonDkShadow = c

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
        Menu.ButtonHilight = c

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
        Menu.ButtonLight = c

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
        Menu.ButtonShadow = c

        c = [CP].Win32.ButtonText
        For Each RW As RetroWindow In pnlRetroPreview.Controls.OfType(Of RetroWindow)
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
        Menu.BackColor = c
        RetroPanel1.BackColor = c
        Menu.Invalidate()

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
            Menu.Flat = True
            RetroPanel1.Flat = True
            menuhilight.BackColor = [CP].Win32.MenuHilight  'Filling of selected item
            highlight.BackColor = [CP].Win32.Hilight 'Outer Border of selected item

            RetroPanel1.BackColor = [CP].Win32.MenuHilight
            RetroPanel1.ButtonShadow = [CP].Win32.Hilight

            menucontainer0.BackColor = [CP].Win32.MenuBar
            RetroLabel3.ForeColor = [CP].Win32.HilightText
        Else
            'Theming Disabled (Menus are retro 3d)
            Menu.Flat = False
            RetroPanel1.Flat = False
            menuhilight.BackColor = [CP].Win32.Hilight 'Both will have same color
            highlight.BackColor = [CP].Win32.Hilight 'Both will have same color
            RetroPanel1.BackColor = [CP].Win32.Menu
            RetroPanel1.ButtonShadow = [CP].Win32.ButtonShadow
            menucontainer0.BackColor = [CP].Win32.Menu
            RetroLabel3.ForeColor = [CP].Win32.MenuText

        End If

        Menu.Invalidate()
        RetroPanel1.Invalidate()
        menuhilight.Invalidate()
        highlight.Invalidate()

    End Sub


End Class