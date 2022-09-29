Imports System.Reflection.Emit
Imports System.Runtime.InteropServices
Imports WinPaletter.XenonCore

Public Class dragPreviewer

    Public CP As CP
    Public File As String

    Private Sub dragPreviewer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Opacity = 0

        If My.W11 Or My.W10 Then
            FormBorderStyle = FormBorderStyle.None
            BackColor = Color.Fuchsia
            TransparencyKey = Color.Fuchsia
        Else
            FormBorderStyle = FormBorderStyle.FixedToolWindow
            BackColor = Color.Fuchsia
            TransparencyKey = Color.Fuchsia
        End If

        CP = New CP(CP.Mode.File, File, True)
        pnl_preview.BackgroundImage = My.Application.Wallpaper
        Adjust_Preview()
        ApplyLivePreviewFromCP(CP)
        ApplyRetroPreview(CP)
        pnl_preview.Visible = True : PictureBox1.Image = GetControlImage(pnl_preview) : pnl_preview.Visible = False
        Me.Invalidate()

        Acrylism.EnableBlur(Me, True)
    End Sub

    Private Sub dragPreviewer_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Opacity = 1
    End Sub

    Sub ApplyLivePreviewFromCP([CP] As CP)
        If Not MainFrm.PreviewConfig = MainFrm.WinVer.Seven And Not MainFrm.PreviewConfig = MainFrm.WinVer.Eight Then
            XenonWindow1.Active = True
            XenonWindow2.Active = False

            XenonWindow1.AccentColor_Enabled = [CP].ApplyAccentonTitlebars
            XenonWindow2.AccentColor_Enabled = [CP].ApplyAccentonTitlebars

            XenonWindow1.AccentColor_Active = [CP].Titlebar_Active
            XenonWindow2.AccentColor_Active = [CP].Titlebar_Active

            XenonWindow1.AccentColor_Inactive = [CP].Titlebar_Inactive
            XenonWindow2.AccentColor_Inactive = [CP].Titlebar_Inactive

            XenonWindow1.DarkMode = Not [CP].AppMode_Light
            XenonWindow2.DarkMode = Not [CP].AppMode_Light

            lbl_uwp.ForeColor = If([CP].AppMode_Light, Color.Black, Color.White)

        Else
            XenonWindow1.Active = True
            XenonWindow2.Active = False
        End If

        Select Case MainFrm.PreviewConfig
            Case MainFrm.WinVer.Eleven
#Region "Win11"
                start.DarkMode = Not [CP].WinMode_Light
                taskbar.DarkMode = Not [CP].WinMode_Light
                ActionCenter.DarkMode = Not [CP].WinMode_Light

                taskbar.Transparency = [CP].Transparency
                start.Transparency = [CP].Transparency
                ActionCenter.Transparency = [CP].Transparency

                start.Top = taskbar.Top - start.Height - 9

                Select Case Not [CP].WinMode_Light
                    Case True   ''''''''''Dark
                        taskbar.BackColorAlpha = 130
                        start.BackColorAlpha = 130
                        ActionCenter.BackColorAlpha = 130

                        If [CP].ApplyAccentonTaskbar Then
                            taskbar.BackColor = Color.FromArgb(taskbar.BackColor.A, [CP].StartListFolders_TaskbarFront)
                            start.BackColor = Color.FromArgb(start.BackColor.A, [CP].StartListFolders_TaskbarFront)
                            ActionCenter.BackColor = Color.FromArgb(ActionCenter.BackColor.A, [CP].StartListFolders_TaskbarFront)
                        Else
                            taskbar.BackColor = Color.FromArgb(55, 55, 55)
                            start.BackColor = Color.FromArgb(40, 40, 40)
                            ActionCenter.BackColor = Color.FromArgb(55, 55, 55)
                        End If

                        ActionCenter.ActionCenterButton_Normal = [CP].Taskbar_Icon_Underline
                        ActionCenter.ActionCenterButton_Hover = [CP].ActionCenter_AppsLinks
                        ActionCenter.ActionCenterButton_Pressed = [CP].StartButton_Hover
                        start.SearchBoxAccent = [CP].Taskbar_Icon_Underline
                        taskbar.AppUnderline = [CP].Taskbar_Icon_Underline
                        Labelx3x.ForeColor = [CP].SettingsIconsAndLinks
                        lblx_linkx.ForeColor = [CP].ActionCenter_AppsLinks

                    Case False   ''''''''''Light
                        taskbar.BackColorAlpha = 180
                        start.BackColorAlpha = 180
                        ActionCenter.BackColorAlpha = 180

                        If [CP].ApplyAccentonTaskbar Then
                            taskbar.BackColor = Color.FromArgb(taskbar.BackColor.A, [CP].Taskbar_Icon_Underline)
                            start.BackColor = Color.FromArgb(start.BackColor.A, [CP].ActionCenter_AppsLinks)
                            ActionCenter.BackColor = Color.FromArgb(ActionCenter.BackColor.A, [CP].ActionCenter_AppsLinks)
                        Else
                            taskbar.BackColor = Color.FromArgb(255, 255, 255)
                            start.BackColor = Color.FromArgb(255, 255, 255)
                            ActionCenter.BackColor = Color.FromArgb(255, 255, 255)
                        End If

                        ActionCenter.ActionCenterButton_Normal = [CP].StartMenuBackground_ActiveTaskbarButton
                        ActionCenter.ActionCenterButton_Hover = [CP].StartListFolders_TaskbarFront
                        ActionCenter.ActionCenterButton_Pressed = [CP].StartButton_Hover
                        start.SearchBoxAccent = [CP].StartMenuBackground_ActiveTaskbarButton
                        taskbar.AppUnderline = [CP].StartMenuBackground_ActiveTaskbarButton
                        Labelx3x.ForeColor = [CP].SettingsIconsAndLinks
                        lblx_linkx.ForeColor = [CP].StartListFolders_TaskbarFront

                End Select
#End Region
            Case MainFrm.WinVer.Ten
#Region "Win10"
                start.DarkMode = Not [CP].WinMode_Light
                taskbar.DarkMode = Not [CP].WinMode_Light
                ActionCenter.DarkMode = Not [CP].WinMode_Light

                taskbar.Transparency = [CP].Transparency
                start.Transparency = [CP].Transparency
                ActionCenter.Transparency = [CP].Transparency

                start.Top = taskbar.Top - start.Height

                taskbar.AppUnderline = ControlPaint.Light(Color.FromArgb(255, [CP].Taskbar_Icon_Underline))

                If [CP].Transparency Then
                    taskbar.BackColorAlpha = 150
                    start.BackColorAlpha = 150
                    ActionCenter.BackColorAlpha = 150
                Else
                    taskbar.BackColorAlpha = 255
                    start.BackColorAlpha = 255
                    ActionCenter.BackColorAlpha = 255
                End If

                If [CP].WinMode_Light And Not [CP].ApplyAccentonTaskbar Then
                    taskbar.BackColor = Color.FromArgb(237, 237, 237)
                    start.BackColor = Color.FromArgb(227, 227, 227)
                    ActionCenter.BackColor = Color.FromArgb(227, 227, 227)
                    taskbar.StartColor = Color.Transparent
                Else
                    If Not [CP].ApplyAccentonTaskbar Then
                        taskbar.BackColor = Color.FromArgb(23, 23, 23)
                        start.BackColor = Color.FromArgb(36, 36, 36)
                        ActionCenter.BackColor = Color.FromArgb(36, 36, 36)
                        taskbar.StartColor = Color.Transparent
                    Else
                        start.BackColor = [CP].StartMenuBackground_ActiveTaskbarButton
                        taskbar.StartColor = [CP].StartMenuBackground_ActiveTaskbarButton
                        ActionCenter.BackColor = [CP].StartMenuBackground_ActiveTaskbarButton

                        If [CP].Transparency Then
                            taskbar.BackColor = [CP].Taskbar_Background
                        Else
                            taskbar.BackColor = [CP].StartListFolders_TaskbarFront
                        End If

                    End If
                End If

                If [CP].WinMode_Light And Not [CP].ApplyAccentonTaskbar Then
                    Labelx3x.ForeColor = [CP].SettingsIconsAndLinks
                    lblx_linkx.ForeColor = [CP].Taskbar_Background
                    ActionCenter.LinkColor = [CP].Taskbar_Background
                    taskbar.AppBackground = Color.Transparent
                Else
                    Labelx3x.ForeColor = [CP].SettingsIconsAndLinks
                    lblx_linkx.ForeColor = [CP].ActionCenter_AppsLinks
                    taskbar.AppUnderline = [CP].Taskbar_Icon_Underline
                    ActionCenter.LinkColor = [CP].Taskbar_Icon_Underline

                    If Not [CP].Transparency And Not [CP].ApplyAccentonTaskbar Then
                        taskbar.AppBackground = Color.Transparent
                    Else
                        taskbar.AppBackground = [CP].StartMenuBackground_ActiveTaskbarButton
                    End If
                End If

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

                XenonWindow1.AccentColor_Active = [CP].Aero_ColorizationColor
                XenonWindow1.Win7ColorBal = [CP].Aero_ColorizationColorBalance

                XenonWindow2.AccentColor_Active = [CP].Aero_ColorizationColor
                XenonWindow2.Win7ColorBal = [CP].Aero_ColorizationColorBalance

                taskbar.BackColor = [CP].Aero_ColorizationColor
                taskbar.Win7ColorBal = [CP].Aero_ColorizationColorBalance

                ReValidateLivePreview(pnl_preview)
#End Region
            Case MainFrm.WinVer.Seven
#Region "Win7"

                Select Case [CP].Aero_Theme
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
                            .Win7Alpha = [CP].Aero_ColorizationBlurBalance
                            .Win7ColorBal = [CP].Aero_ColorizationColorBalance
                            .Win7GlowBal = [CP].Aero_ColorizationAfterglowBalance
                            .AccentColor_Active = [CP].Aero_ColorizationColor
                            .AccentColor2_Active = [CP].Aero_ColorizationAfterglow
                            .AccentColor_Inactive = [CP].Aero_ColorizationColor
                            .AccentColor2_Inactive = [CP].Aero_ColorizationAfterglow
                            .Win7Noise = [CP].Aero_ColorizationGlassReflectionIntensity
                        End With
                        With XenonWindow2
                            .Win7 = True
                            .Win7Aero = True
                            .Win7AeroOpaque = False
                            .Win7Basic = False
                            .Win7Alpha = [CP].Aero_ColorizationBlurBalance
                            .Win7ColorBal = [CP].Aero_ColorizationColorBalance
                            .Win7GlowBal = [CP].Aero_ColorizationAfterglowBalance
                            .AccentColor_Active = [CP].Aero_ColorizationColor
                            .AccentColor2_Active = [CP].Aero_ColorizationAfterglow
                            .AccentColor_Inactive = [CP].Aero_ColorizationColor
                            .AccentColor2_Inactive = [CP].Aero_ColorizationAfterglow
                            .Win7Noise = [CP].Aero_ColorizationGlassReflectionIntensity
                        End With
                        With start
                            .Win7AeroOpaque = False
                            .BackColorAlpha = [CP].Aero_ColorizationBlurBalance
                            .Win7ColorBal = [CP].Aero_ColorizationColorBalance
                            .Win7GlowBal = [CP].Aero_ColorizationAfterglowBalance
                            .BackColor = [CP].Aero_ColorizationColor
                            .BackColor2 = [CP].Aero_ColorizationAfterglow
                            .NoisePower = [CP].Aero_ColorizationGlassReflectionIntensity
                        End With
                        With taskbar
                            .Win7AeroOpaque = False
                            .BackColorAlpha = [CP].Aero_ColorizationBlurBalance
                            .Win7ColorBal = [CP].Aero_ColorizationColorBalance
                            .Win7GlowBal = [CP].Aero_ColorizationAfterglowBalance
                            .BackColor = [CP].Aero_ColorizationColor
                            .BackColor2 = [CP].Aero_ColorizationAfterglow
                            .NoisePower = [CP].Aero_ColorizationGlassReflectionIntensity
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
                            .Win7Alpha = [CP].Aero_ColorizationColorBalance
                            .AccentColor_Active = [CP].Aero_ColorizationColor
                            .AccentColor_Inactive = [CP].Aero_ColorizationColor
                            .Win7Noise = [CP].Aero_ColorizationGlassReflectionIntensity
                        End With
                        With XenonWindow2
                            .Win7 = True
                            .Win7Aero = False
                            .Win7AeroOpaque = True
                            .Win7Basic = False
                            .Win7Alpha = [CP].Aero_ColorizationColorBalance
                            .AccentColor_Active = [CP].Aero_ColorizationColor
                            .AccentColor_Inactive = [CP].Aero_ColorizationColor
                            .Win7Noise = [CP].Aero_ColorizationGlassReflectionIntensity
                        End With
                        With taskbar
                            .Win7AeroOpaque = True
                            .BackColorAlpha = [CP].Aero_ColorizationColorBalance
                            .BackColor = [CP].Aero_ColorizationColor
                            .BackColor2 = [CP].Aero_ColorizationColor
                            .NoisePower = [CP].Aero_ColorizationGlassReflectionIntensity
                        End With
                        With start
                            .Win7AeroOpaque = True
                            .BackColorAlpha = [CP].Aero_ColorizationColorBalance
                            .BackColor = [CP].Aero_ColorizationColor
                            .BackColor2 = [CP].Aero_ColorizationColor
                            .NoisePower = [CP].Aero_ColorizationGlassReflectionIntensity
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
        PanelX3X.Visible = True
        lblx_linkx.Visible = True

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
                PanelX3X.Visible = False
                lblx_linkx.Visible = False

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

                If CP.Aero_Theme = CP.AeroTheme.Classic Then
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

                PanelX3X.Visible = False
                lblx_linkx.Visible = False
                taskbar.UseItAsTaskbar_Version = XenonAcrylic.TaskbarVersion.Seven
                taskbar.BlurPower = 1
                taskbar.NoisePower = CP.Aero_ColorizationGlassReflectionIntensity / 100
                taskbar.Height = 34

                start.UseItAsTaskbar_Version = XenonAcrylic.TaskbarVersion.Seven
                start.RoundedCorners = True
                start.BlurPower = 1
                start.NoisePower = 0.5
                start.Left = 0
                start.Width = 136
                start.Height = 191
                start.NoisePower = CP.Aero_ColorizationGlassReflectionIntensity / 100
                start.Top = taskbar.Top - start.Height
        End Select

        If MainFrm.PreviewConfig = MainFrm.WinVer.Ten Or MainFrm.PreviewConfig = MainFrm.WinVer.Eleven Then
            XenonWindow1.Top = start.Top
            XenonWindow1.Left = start.Right + 5

            XenonWindow2.Top = XenonWindow1.Bottom + 5
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

    Sub ApplyRetroPreview([CP] As CP)
        RetroWindow1.ColorGradient = [CP].Win32UI_EnableGradient
        RetroWindow2.ColorGradient = [CP].Win32UI_EnableGradient
        RetroWindow3.ColorGradient = [CP].Win32UI_EnableGradient
        RetroWindow4.ColorGradient = [CP].Win32UI_EnableGradient

        Dim c As Color
        c = [CP].Win32UI_ActiveTitle
        RetroWindow2.Color1 = c
        RetroWindow3.Color1 = c
        RetroWindow4.Color1 = c

        c = [CP].Win32UI_GradientActiveTitle
        RetroWindow2.Color2 = c
        RetroWindow3.Color2 = c
        RetroWindow4.Color2 = c

        c = [CP].Win32UI_TitleText
        RetroWindow2.ForeColor = c
        RetroWindow3.ForeColor = c
        RetroWindow4.ForeColor = c

        c = [CP].Win32UI_InactiveTitle
        RetroWindow1.Color1 = c

        c = [CP].Win32UI_GradientInactiveTitle
        RetroWindow1.Color2 = c

        c = [CP].Win32UI_InactiveTitleText
        RetroWindow1.ForeColor = c

        c = [CP].Win32UI_ActiveBorder
        RetroWindow2.ColorBorder = c
        RetroWindow3.ColorBorder = c
        RetroWindow4.ColorBorder = c

        c = [CP].Win32UI_InactiveBorder
        RetroWindow1.ColorBorder = c

        c = [CP].Win32UI_WindowFrame
        For Each RW As RetroWindow In Panel1.Controls.OfType(Of RetroWindow)
            For Each RB As RetroButton In RW.Controls.OfType(Of RetroButton)
                RB.WindowFrame = c
            Next
        Next
        For Each RB As RetroButton In RetroPanel2.Controls.OfType(Of RetroButton)
            RB.WindowFrame = c
        Next

        c = [CP].Win32UI_ButtonFace
        For Each RW As RetroWindow In Panel1.Controls.OfType(Of RetroWindow)
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

        c = [CP].Win32UI_ButtonDkShadow
        For Each RW As RetroWindow In Panel1.Controls.OfType(Of RetroWindow)
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

        c = [CP].Win32UI_ButtonHilight
        For Each RW As RetroWindow In Panel1.Controls.OfType(Of RetroWindow)
            RW.ButtonHilight = c
            For Each RB As RetroButton In RW.Controls.OfType(Of RetroButton)
                RB.ButtonHilight = c
            Next
        Next
        For Each RB As RetroButton In RetroPanel2.Controls.OfType(Of RetroButton)
            RB.ButtonHilight = c
        Next
        For Each RB As RetroPanelRaised In Panel1.Controls.OfType(Of RetroPanelRaised)
            RB.ButtonHilight = c
        Next
        RetroTextBox1.ButtonHilight = c
        RetroPanel1.ButtonHilight = c
        RetroPanel2.ButtonHilight = c
        Menu.ButtonHilight = c

        c = [CP].Win32UI_ButtonLight
        For Each RW As RetroWindow In Panel1.Controls.OfType(Of RetroWindow)
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

        c = [CP].Win32UI_ButtonShadow
        For Each RW As RetroWindow In Panel1.Controls.OfType(Of RetroWindow)
            RW.ButtonShadow = c
            For Each RB As RetroButton In RW.Controls.OfType(Of RetroButton)
                RB.ButtonShadow = c
            Next
        Next
        For Each RB As RetroButton In RetroPanel2.Controls.OfType(Of RetroButton)
            RB.ButtonShadow = c
        Next
        For Each RB As RetroPanelRaised In Panel1.Controls.OfType(Of RetroPanelRaised)
            RB.ButtonShadow = c
        Next
        RetroTextBox1.ButtonShadow = c
        RetroPanel1.ButtonShadow = c
        RetroTextBox1.Invalidate()
        Menu.ButtonShadow = c

        c = [CP].Win32UI_ButtonText
        For Each RW As RetroWindow In Panel1.Controls.OfType(Of RetroWindow)
            For Each RB As RetroButton In RW.Controls.OfType(Of RetroButton)
                RB.ForeColor = c
            Next
        Next
        For Each RB As RetroButton In RetroPanel2.Controls.OfType(Of RetroButton)
            RB.ForeColor = c
        Next

        c = [CP].Win32UI_AppWorkspace
        Panel2.BackColor = c

        c = [CP].Win32UI_Background
        Panel1.BackColor = c

        c = [CP].Win32UI_Menu
        Menu.BackColor = c
        RetroPanel1.BackColor = c
        Menu.Invalidate()

        c = [CP].Win32UI_MenuBar
        Panel3.BackColor = c

        c = [CP].Win32UI_Hilight
        highlight.BackColor = c

        c = [CP].Win32UI_MenuHilight
        menuhilight.BackColor = c

        c = [CP].Win32UI_MenuText
        RetroLabel6.ForeColor = c
        RetroLabel1.ForeColor = c

        c = [CP].Win32UI_HilightText
        RetroLabel5.ForeColor = c

        c = [CP].Win32UI_GrayText
        RetroLabel2.ForeColor = c
        RetroLabel9.ForeColor = c

        c = [CP].Win32UI_Window
        RetroTextBox1.BackColor = c

        c = [CP].Win32UI_WindowText
        RetroTextBox1.ForeColor = c
        RetroLabel4.ForeColor = c

        c = [CP].Win32UI_InfoWindow
        RetroLabel13.BackColor = c

        c = [CP].Win32UI_InfoText
        RetroLabel13.ForeColor = c

        For Each RW As RetroWindow In Panel1.Controls.OfType(Of RetroWindow)
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

        If [CP].Win32UI_EnableTheming Then
            'Theming Enabled (Menus Has colors and borders)
            Menu.Flat = True
            RetroPanel1.Flat = True
            menuhilight.BackColor = [CP].Win32UI_MenuHilight  'Filling of selected item
            highlight.BackColor = [CP].Win32UI_Hilight 'Outer Border of selected item

            RetroPanel1.BackColor = [CP].Win32UI_MenuHilight
            RetroPanel1.ButtonShadow = [CP].Win32UI_Hilight

            Panel3.BackColor = [CP].Win32UI_MenuBar
            RetroLabel3.ForeColor = [CP].Win32UI_HilightText
        Else
            'Theming Disabled (Menus are retro 3d)
            Menu.Flat = False
            RetroPanel1.Flat = False
            menuhilight.BackColor = [CP].Win32UI_Hilight 'Both will have same color
            highlight.BackColor = [CP].Win32UI_Hilight 'Both will have same color
            RetroPanel1.BackColor = [CP].Win32UI_Menu
            RetroPanel1.ButtonShadow = [CP].Win32UI_ButtonShadow
            Panel3.BackColor = [CP].Win32UI_Menu
            RetroLabel3.ForeColor = [CP].Win32UI_MenuText

        End If

        Menu.Invalidate()
        RetroPanel1.Invalidate()
        menuhilight.Invalidate()
        highlight.Invalidate()

    End Sub


End Class