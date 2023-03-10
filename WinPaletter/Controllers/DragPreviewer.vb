Imports Devcorp.Controls.VisualStyles
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

        If My.W11 Or My.W10 Then
            FormBorderStyle = FormBorderStyle.None
            BackColor = Color.Black
            TransparencyKey = Color.Black
        Else
            FormBorderStyle = FormBorderStyle.FixedToolWindow
            BackColor = Color.Fuchsia
            TransparencyKey = Color.Fuchsia
        End If

        DrawDWMEffect(True, FormStyle.Acrylic)

        CP = New CP(CP_Type.File, File, True)

        Select Case MainFrm.PreviewConfig
            Case WinVer.W11
                If CP.WallpaperTone_W11.Enabled Then pnl_preview.BackgroundImage = MainFrm.GetTintedWallpaper(CP.WallpaperTone_W11) Else pnl_preview.BackgroundImage = My.Wallpaper

            Case WinVer.W10
                If CP.WallpaperTone_W10.Enabled Then pnl_preview.BackgroundImage = MainFrm.GetTintedWallpaper(CP.WallpaperTone_W10) Else pnl_preview.BackgroundImage = My.Wallpaper

            Case WinVer.W8
                If CP.WallpaperTone_W8.Enabled Then pnl_preview.BackgroundImage = MainFrm.GetTintedWallpaper(CP.WallpaperTone_W8) Else pnl_preview.BackgroundImage = My.Wallpaper

            Case WinVer.W7
                If CP.WallpaperTone_W7.Enabled Then pnl_preview.BackgroundImage = MainFrm.GetTintedWallpaper(CP.WallpaperTone_W7) Else pnl_preview.BackgroundImage = My.Wallpaper

            Case WinVer.WVista
                If CP.WallpaperTone_WVista.Enabled Then pnl_preview.BackgroundImage = MainFrm.GetTintedWallpaper(CP.WallpaperTone_WVista) Else pnl_preview.BackgroundImage = My.Wallpaper

            Case WinVer.WXP
                If CP.WallpaperTone_WXP.Enabled Then pnl_preview.BackgroundImage = MainFrm.GetTintedWallpaper(CP.WallpaperTone_WXP) Else pnl_preview.BackgroundImage = My.Wallpaper

        End Select

        pnl_preview_classic.BackgroundImage = pnl_preview.BackgroundImage

        Adjust_Preview()
        ApplyLivePreviewFromCP(CP)
        ApplyRetroPreview(CP)
        SetClassicMetrics(CP)
        AdjustClassicPreview()

        XenonAlertBox11.Text = MainFrm.WXP_Alert2.Text
        XenonAlertBox11.Text = MainFrm.WXP_Alert2.Text
        XenonAlertBox11.Size = XenonAlertBox11.Parent.Size - New Size(40, 40)
        XenonAlertBox11.Location = New Point(20, 20)

        ResumeLayout()
    End Sub

    Private Sub DragPreviewer_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Opacity = 1
        Visible = True
        Me.Invalidate()
    End Sub

    Sub ApplyLivePreviewFromCP(ByVal [CP] As CP)
        Dim AnimX1 As Integer = 25
        Dim AnimX2 As Integer = 1

        XenonWindow1.Active = True
        XenonWindow2.Active = False

        Select Case MainFrm.PreviewConfig
            Case WinVer.W11
                tabs_preview.SelectedIndex = 0
                Select Case Not [CP].Windows11.WinMode_Light
                    Case True   ''''''''''Dark
                        ActionCenter.BackColorAlpha = 75

                        If ExplorerPatcher.IsAllowed Then
                            If My.EP.UseStart10 Then
                                start.BackColorAlpha = 185
                            Else
                                start.BackColorAlpha = 75
                            End If

                            If My.EP.UseTaskbar10 Then
                                taskbar.BackColorAlpha = 185
                            Else
                                taskbar.BackColorAlpha = 75
                            End If
                        Else
                            taskbar.BackColorAlpha = 75
                            start.BackColorAlpha = 75
                        End If

                        Select Case [CP].Windows11.ApplyAccentonTaskbar
                            Case ApplyAccentonTaskbar_Level.None
                                taskbar.BackColor = Color.FromArgb(55, 55, 55)
                                start.BackColor = Color.FromArgb(40, 40, 40)
                                ActionCenter.BackColor = Color.FromArgb(55, 55, 55)

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
                        ActionCenter.BackColorAlpha = 180

                        If ExplorerPatcher.IsAllowed Then
                            If My.EP.UseStart10 Then
                                start.BackColorAlpha = 210
                            Else
                                start.BackColorAlpha = 180
                            End If

                            If My.EP.UseTaskbar10 Then
                                taskbar.BackColorAlpha = 210
                            Else
                                taskbar.BackColorAlpha = 180
                            End If
                        Else
                            taskbar.BackColorAlpha = 180
                            start.BackColorAlpha = 180
                        End If

                        Select Case [CP].Windows11.ApplyAccentonTaskbar
                            Case ApplyAccentonTaskbar_Level.None
                                taskbar.BackColor = Color.FromArgb(255, 255, 255)
                                start.BackColor = Color.FromArgb(255, 255, 255)
                                ActionCenter.BackColor = Color.FromArgb(255, 255, 255)

                            Case ApplyAccentonTaskbar_Level.Taskbar_Start_AC
                                taskbar.BackColor = Color.FromArgb(taskbar.BackColor.A, [CP].Windows11.Color_Index1)

                                If ExplorerPatcher.IsAllowed And My.EP.UseStart10 Then
                                    start.BackColor = Color.FromArgb(start.BackColor.A, [CP].Windows11.Color_Index4)
                                Else
                                    start.BackColor = Color.FromArgb(start.BackColor.A, [CP].Windows11.Color_Index0)
                                End If

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

                        If ExplorerPatcher.IsAllowed And My.EP.UseTaskbar10 Then
                            taskbar.AppUnderline = [CP].Windows11.Color_Index1
                        Else
                            taskbar.AppUnderline = [CP].Windows11.Color_Index4
                        End If

                        setting_icon_preview.ForeColor = [CP].Windows11.Color_Index3
                        lnk_preview.ForeColor = [CP].Windows11.Color_Index5
                End Select

            Case WinVer.W10
#Region "Win10"
                tabs_preview.SelectedIndex = 0
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
                start.Transparency = [CP].Windows10.Transparency
                ActionCenter.Transparency = [CP].Windows10.Transparency

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

#End Region
            Case WinVer.W8
#Region "Win8.1"
                tabs_preview.SelectedIndex = 0

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
#End Region
            Case WinVer.W7
#Region "Win7"

                XenonWindow1.Shadow = [CP].WindowsEffects.WindowShadow
                XenonWindow2.Shadow = [CP].WindowsEffects.WindowShadow

                Select Case [CP].Windows7.Theme
                    Case CP.AeroTheme.Aero
                        tabs_preview.SelectedIndex = 0
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
                        tabs_preview.SelectedIndex = 0
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
                        tabs_preview.SelectedIndex = 0
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

                    Case CP.AeroTheme.Classic
                        tabs_preview.SelectedIndex = 1

                End Select
#End Region
            Case WinVer.WVista
#Region "WinVista"
                XenonWindow1.Shadow = [CP].WindowsEffects.WindowShadow
                XenonWindow2.Shadow = [CP].WindowsEffects.WindowShadow

                Select Case [CP].WindowsVista.Theme
                    Case CP.AeroTheme.Aero
                        tabs_preview.SelectedIndex = 0
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
                        tabs_preview.SelectedIndex = 0
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
                        tabs_preview.SelectedIndex = 0
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

                    Case CP.AeroTheme.Classic
                        tabs_preview.SelectedIndex = 1

                End Select
#End Region
            Case WinVer.WXP
#Region "WinXP"


                start.Refresh()
                taskbar.Refresh()
#End Region
        End Select

        XenonWindow1.Invalidate()
        XenonWindow2.Invalidate()
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
        Panel3.Visible = (MainFrm.PreviewConfig = WinVer.W11 Or MainFrm.PreviewConfig = WinVer.W10)
        lnk_preview.Visible = (MainFrm.PreviewConfig = WinVer.W11 Or MainFrm.PreviewConfig = WinVer.W10)
        start.Visible = (Not MainFrm.PreviewConfig = WinVer.W8)
        ActionCenter.Visible = (MainFrm.PreviewConfig = WinVer.W11 Or MainFrm.PreviewConfig = WinVer.W10)
        Dim condition0 As Boolean = MainFrm.PreviewConfig = WinVer.W7 AndAlso CP.Windows7.Theme = AeroTheme.Classic
        Dim condition1 As Boolean = MainFrm.PreviewConfig = WinVer.WXP AndAlso CP.WindowsXP.Theme = WinXPTheme.Classic
        tabs_preview.SelectedIndex = If(condition0 Or condition1, 1, 0)
        XenonAlertBox11.Visible = MainFrm.PreviewConfig = WinVer.WXP AndAlso My.StartedWithClassicTheme

        MainFrm.ApplyMetrics([CP], XenonWindow1)
        MainFrm.ApplyMetrics([CP], XenonWindow2)

        Select Case MainFrm.PreviewConfig
            Case WinVer.W11
                ActionCenter.Style = XenonWinElement.Styles.ActionCenter11

                If ExplorerPatcher.IsAllowed Then
                    With My.EP
                        If Not .UseStart10 Then
                            start.Style = XenonWinElement.Styles.Start11
                        Else
                            start.Style = XenonWinElement.Styles.Start10
                        End If

                        If Not .UseTaskbar10 Then
                            taskbar.Style = XenonWinElement.Styles.Taskbar11
                        Else
                            taskbar.Style = XenonWinElement.Styles.Taskbar10
                        End If

                    End With
                Else
                    taskbar.Style = XenonWinElement.Styles.Taskbar11
                    start.Style = XenonWinElement.Styles.Start11
                End If

                XenonWindow1.Preview = XenonWindow.Preview_Enum.W11

                If CP.WallpaperTone_W11.Enabled Then pnl_preview.BackgroundImage = MainFrm.GetTintedWallpaper(CP.WallpaperTone_W11) Else pnl_preview.BackgroundImage = My.Wallpaper

            Case WinVer.W10
                ActionCenter.Style = XenonWinElement.Styles.ActionCenter10
                taskbar.Style = XenonWinElement.Styles.Taskbar10
                start.Style = XenonWinElement.Styles.Start10
                XenonWindow1.Preview = XenonWindow.Preview_Enum.W10
                If CP.WallpaperTone_W10.Enabled Then pnl_preview.BackgroundImage = MainFrm.GetTintedWallpaper(CP.WallpaperTone_W10) Else pnl_preview.BackgroundImage = My.Wallpaper

            Case WinVer.W8
                taskbar.Style = If(CP.Windows8.Theme = AeroTheme.Aero, XenonWinElement.Styles.Taskbar8Aero, XenonWinElement.Styles.Taskbar8Lite)
                XenonWindow1.Preview = If(CP.Windows8.Theme = AeroTheme.AeroLite, XenonWindow.Preview_Enum.W8Lite, XenonWindow.Preview_Enum.W8)
                If CP.WallpaperTone_W8.Enabled Then pnl_preview.BackgroundImage = MainFrm.GetTintedWallpaper(CP.WallpaperTone_W8) Else pnl_preview.BackgroundImage = My.Wallpaper

            Case WinVer.W7
                If CP.WallpaperTone_W7.Enabled Then pnl_preview.BackgroundImage = MainFrm.GetTintedWallpaper(CP.WallpaperTone_W7) Else pnl_preview.BackgroundImage = My.Wallpaper

                Select Case CP.Windows7.Theme
                    Case AeroTheme.Aero
                        taskbar.Style = XenonWinElement.Styles.Taskbar7Aero
                        start.Style = XenonWinElement.Styles.Start7Aero
                        XenonWindow1.Preview = XenonWindow.Preview_Enum.W7Aero
                        tabs_preview.SelectedIndex = 0

                    Case AeroTheme.AeroOpaque
                        taskbar.Style = XenonWinElement.Styles.Taskbar7Opaque
                        start.Style = XenonWinElement.Styles.Start7Opaque
                        XenonWindow1.Preview = XenonWindow.Preview_Enum.W7Opaque
                        tabs_preview.SelectedIndex = 0

                    Case AeroTheme.Basic
                        taskbar.Style = XenonWinElement.Styles.Taskbar7Basic
                        start.Style = XenonWinElement.Styles.Start7Basic
                        XenonWindow1.Preview = XenonWindow.Preview_Enum.W7Basic
                        tabs_preview.SelectedIndex = 0

                    Case AeroTheme.Classic
                        tabs_preview.SelectedIndex = 1

                End Select

                XenonWindow1.WinVista = False
                XenonWindow2.WinVista = False

            Case WinVer.WVista

                Select Case CP.WindowsVista.Theme     'Windows Vista uses the same aero of Windows 7
                    Case AeroTheme.Aero
                        taskbar.Style = XenonWinElement.Styles.TaskbarVistaAero
                        start.Style = XenonWinElement.Styles.StartVistaAero
                        XenonWindow1.Preview = XenonWindow.Preview_Enum.W7Aero
                        tabs_preview.SelectedIndex = 0

                    Case AeroTheme.AeroOpaque
                        taskbar.Style = XenonWinElement.Styles.TaskbarVistaOpaque
                        start.Style = XenonWinElement.Styles.StartVistaOpaque
                        XenonWindow1.Preview = XenonWindow.Preview_Enum.W7Opaque
                        tabs_preview.SelectedIndex = 0

                    Case AeroTheme.Basic
                        taskbar.Style = XenonWinElement.Styles.TaskbarVistaBasic
                        start.Style = XenonWinElement.Styles.StartVistaBasic
                        XenonWindow1.Preview = XenonWindow.Preview_Enum.W7Basic
                        tabs_preview.SelectedIndex = 0

                    Case AeroTheme.Classic
                        tabs_preview.SelectedIndex = 1

                End Select

                If CP.WallpaperTone_WVista.Enabled Then pnl_preview.BackgroundImage = MainFrm.GetTintedWallpaper(CP.WallpaperTone_WVista) Else pnl_preview.BackgroundImage = My.Wallpaper

                XenonWindow1.WinVista = True
                XenonWindow2.WinVista = True

            Case WinVer.WXP
                taskbar.Style = XenonWinElement.Styles.TaskbarXP
                start.Style = XenonWinElement.Styles.StartXP
                XenonWindow1.Preview = XenonWindow.Preview_Enum.WXP
                If CP.WallpaperTone_WXP.Enabled Then pnl_preview.BackgroundImage = MainFrm.GetTintedWallpaper(CP.WallpaperTone_WXP) Else pnl_preview.BackgroundImage = My.Wallpaper

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

        End Select

        XenonWindow2.Preview = XenonWindow1.Preview
        pnl_preview_classic.BackgroundImage = pnl_preview.BackgroundImage

        Select Case MainFrm.PreviewConfig
            Case WinVer.W11
                ActionCenter.Dock = Nothing
                ActionCenter.BlurPower = 7
                ActionCenter.NoisePower = 0.2
                ActionCenter.Size = New Size(120, 85)
                ActionCenter.Location = New Point(398, 161)

                If ExplorerPatcher.IsAllowed Then

                    With My.EP
                        If Not .UseTaskbar10 Then
                            taskbar.BlurPower = 12
                            taskbar.Height = 42
                        Else
                            taskbar.BlurPower = 12
                            taskbar.Height = 35
                            taskbar.UseWin11ORB_WithWin10 = Not .TaskbarButton10
                        End If

                        If Not .UseStart10 Then
                            start.BlurPower = 7
                            start.NoisePower = 0.2
                            start.Size = New Size(135, 200)
                            start.Location = New Point(9, taskbar.Bottom - taskbar.Height - start.Height - 9)
                        Else
                            start.BlurPower = 7
                            start.NoisePower = 0.2

                            Select Case .StartStyle
                                Case ExplorerPatcher.StartStyles.NotRounded
                                    start.Size = New Size(182, 201)
                                    start.Left = 0
                                    start.Top = taskbar.Bottom - taskbar.Height - start.Height
                                    start.UseWin11RoundedCorners_WithWin10_Level1 = False
                                    start.UseWin11RoundedCorners_WithWin10_Level2 = False

                                Case ExplorerPatcher.StartStyles.RoundedCornersDockedMenu
                                    start.Size = New Size(182, 201)
                                    start.Left = 0
                                    start.Top = taskbar.Bottom - taskbar.Height - start.Height
                                    start.UseWin11RoundedCorners_WithWin10_Level1 = True
                                    start.UseWin11RoundedCorners_WithWin10_Level2 = False

                                Case ExplorerPatcher.StartStyles.RoundedCornersFloatingMenu
                                    start.Size = New Size(182, 201)
                                    start.Location = New Point(9, taskbar.Bottom - taskbar.Height - start.Height - 9)
                                    start.UseWin11RoundedCorners_WithWin10_Level1 = False
                                    start.UseWin11RoundedCorners_WithWin10_Level2 = True

                            End Select

                        End If
                    End With

                Else
                    taskbar.BlurPower = 12
                    taskbar.Height = 42
                    '########################
                    start.BlurPower = 7
                    start.NoisePower = 0.2
                    start.Size = New Size(135, 200)
                    start.Location = New Point(9, taskbar.Bottom - 42 - start.Height - 9)
                End If

            Case WinVer.W10
                ActionCenter.Dock = DockStyle.Right
                ActionCenter.BlurPower = 7
                ActionCenter.NoisePower = 0.2
                '########################
                taskbar.BlurPower = 12
                '########################
                start.BlurPower = 7
                start.NoisePower = 0.2
                '########################

                taskbar.Height = 35
                taskbar.UseWin11ORB_WithWin10 = False
                start.Size = New Size(182, 201)
                start.Left = 0
                start.Top = taskbar.Bottom - taskbar.Height - start.Height
                start.UseWin11RoundedCorners_WithWin10_Level1 = False
                start.UseWin11RoundedCorners_WithWin10_Level2 = False

            Case WinVer.W8
                Panel3.Visible = False
                lnk_preview.Visible = False
                start.Visible = False
                ActionCenter.Visible = False
                taskbar.BlurPower = 0
                taskbar.Height = 34

                start.BlurPower = 0
                start.Top = taskbar.Top - start.Height
                start.Left = 0

            Case WinVer.W7
                Panel3.Visible = False
                lnk_preview.Visible = False
                ActionCenter.Visible = False

                taskbar.BlurPower = 1
                taskbar.NoisePower = CP.Windows7.ColorizationGlassReflectionIntensity / 100
                taskbar.Height = 34

                start.BlurPower = 1
                start.NoisePower = 0.5
                start.Width = 136
                start.Height = 191
                start.NoisePower = CP.Windows7.ColorizationGlassReflectionIntensity / 100
                start.Left = 0
                start.Top = taskbar.Top - start.Height
                ClassicTaskbar.Height = 44
                RetroButton3.Image = My.Resources.ActiveApp_Taskbar
                RetroButton4.Image = My.Resources.InactiveApp_Taskbar
                RetroButton2.Image = My.Resources.Native7.Resize(18, 16)
                RetroButton3.ImageAlign = Drawing.ContentAlignment.MiddleCenter
                RetroButton4.ImageAlign = Drawing.ContentAlignment.MiddleCenter
                RetroButton3.Width = 48
                RetroButton4.Width = 48
                RetroButton3.Text = ""
                RetroButton4.Text = ""
                RetroButton4.Left = RetroButton3.Right + 3
                RetroButton3.Font = New Font(CP.MetricsFonts.CaptionFont.Name, 9, RetroButton3.Font.Style)
                RetroButton4.Font = New Font(CP.MetricsFonts.CaptionFont.Name, 9, RetroButton4.Font.Style)
                RetroButton2.Font = New Font(CP.MetricsFonts.CaptionFont.Name, 8.5, RetroButton2.Font.Style)
                RetroButton3.HatchBrush = False

            Case WinVer.WVista
                Panel3.Visible = False
                lnk_preview.Visible = False
                ActionCenter.Visible = False
                taskbar.Height = 30

                start.Width = 136
                start.Height = 191
                start.Left = 0
                start.Top = taskbar.Top - start.Height
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
                RetroButton3.Font = New Font(CP.MetricsFonts.CaptionFont.Name, 9, RetroButton3.Font.Style)
                RetroButton4.Font = New Font(CP.MetricsFonts.CaptionFont.Name, 9, RetroButton4.Font.Style)
                RetroButton2.Font = New Font(CP.MetricsFonts.CaptionFont.Name, 8.5, RetroButton2.Font.Style)
                RetroButton3.HatchBrush = True

            Case WinVer.WXP
                taskbar.Height = 30
                start.Width = 150
                start.Height = 190
                start.Left = 0
                start.Top = taskbar.Top - start.Height
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
                RetroButton3.Font = New Font(CP.MetricsFonts.CaptionFont.Name, 9, RetroButton3.Font.Style)
                RetroButton4.Font = New Font(CP.MetricsFonts.CaptionFont.Name, 9, RetroButton4.Font.Style)
                RetroButton2.Font = New Font(CP.MetricsFonts.CaptionFont.Name, 8.5, RetroButton2.Font.Style)
                RetroButton3.HatchBrush = True

        End Select

        If MainFrm.PreviewConfig = WinVer.W10 Or MainFrm.PreviewConfig = WinVer.W11 Then

            If My.W11 And My.EP.UseStart10 Then
                XenonWindow1.Top = start.Top - 35
                XenonWindow1.Left = start.Right + 15
            Else
                XenonWindow1.Top = start.Top - If(MainFrm.PreviewConfig = WinVer.W11, 30, 35)
                XenonWindow1.Left = start.Right + If(MainFrm.PreviewConfig = WinVer.W11, 30, 15)
            End If

            XenonWindow2.Top = XenonWindow1.Bottom + 1
            XenonWindow2.Left = XenonWindow1.Left
        Else
            XenonWindow1.Top = 10
            XenonWindow1.Left = (XenonWindow1.Parent.Width - XenonWindow1.Width) / 2

            XenonWindow2.Top = XenonWindow1.Bottom + 5
            XenonWindow2.Left = XenonWindow1.Left
        End If

        ReValidateLivePreview(tabs_preview)
    End Sub

    Sub AdjustClassicPreview()
        SetToClassicWindow(ClassicWindow1, CP)
        SetToClassicWindow(ClassicWindow2, CP, False)

        SetClassicMetrics(ClassicWindow1, CP)
        SetClassicMetrics(ClassicWindow2, CP)
        SetToClassicButton(RetroButton2, CP)
        SetToClassicButton(RetroButton3, CP)
        SetToClassicButton(RetroButton4, CP)
        SetToClassicRaisedPanel(ClassicTaskbar, CP)

        ClassicWindow2.Font = CP.MetricsFonts.CaptionFont
        ClassicWindow1.Font = CP.MetricsFonts.CaptionFont
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

        RetroShadow1.refresh
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

End Class