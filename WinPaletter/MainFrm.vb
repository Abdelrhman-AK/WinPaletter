Imports System.ComponentModel
Imports System.Net
Imports System.Reflection
Imports Microsoft.Win32
Imports WinPaletter.XenonCore

Public Class MainFrm
    Private _Shown As Boolean = False
    Public CP, CP_Original, CP_FirstTime As CP
    Dim CP_BeforeDragAndDrop As CP
    Public PreviewConfig As WinVer = WinVer.Eleven
    Private ReadOnly ReorderAfterPreviewConfigChange As Boolean = True
    Dim RaiseUpdate As Boolean = False
    Dim ver As String = ""
    Dim StableInt, BetaInt, UpdateChannel As Integer
    Dim ChannelFixer As Integer

#Region "CP Subs"
    Sub ApplyLivePreviewFromCP(ByVal [CP] As CP)
        XenonWindow1.AccentColor_Enabled = [CP].ApplyAccentonTitlebars
        XenonWindow2.AccentColor_Enabled = [CP].ApplyAccentonTitlebars

        XenonWindow1.AccentColor_Active = [CP].Titlebar_Active
        XenonWindow2.AccentColor_Active = [CP].Titlebar_Active

        XenonWindow1.AccentColor_Inactive = [CP].Titlebar_Inactive
        XenonWindow2.AccentColor_Inactive = [CP].Titlebar_Inactive

        XenonWindow1.DarkMode = Not [CP].AppMode_Light
        XenonWindow2.DarkMode = Not [CP].AppMode_Light

        XenonWindow1.Invalidate()
        XenonWindow2.Invalidate()

        Dim AnimX1 As Integer = 30
        Dim AnimX2 As Integer = 1

        Visual.FadeColor(Label8, "Forecolor", Label8.ForeColor, If([CP].AppMode_Light, Color.Black, Color.White), AnimX1, AnimX2)

        pnl1.Top = Label10.Bottom + 3
        pnl2.Top = pnl1.Bottom + 2
        pnl3.Top = pnl2.Bottom + 2
        pnl4.Top = pnl3.Bottom + 2
        pnl5.Top = pnl4.Bottom + 2
        pnl6.Top = pnl5.Bottom + 2
        pnl7.Top = pnl6.Bottom + 2
        pnl8.Top = pnl7.Bottom + 2

        Select Case PreviewConfig
            Case WinVer.Eleven
#Region "Win11"
                start.DarkMode = Not [CP].WinMode_Light
                taskbar.DarkMode = Not [CP].WinMode_Light
                ActionCenter.DarkMode = Not [CP].WinMode_Light

                taskbar.Transparency = [CP].Transparency
                start.Transparency = [CP].Transparency
                ActionCenter.Transparency = [CP].Transparency

                start.Top = taskbar.Top - start.Height - 9

                pic5.Image = My.Resources.Mini_SettingsIcons
                pic6.Image = My.Resources.Mini_Start11
                pic7.Image = My.Resources.Mini_StartMenuAccent
                pic8.Image = My.Resources.Mini_TaskbarActiveIcon

                lbl5.Text = My.Application.LanguageHelper.X7
                lbl6.Text = My.Application.LanguageHelper.X8
                lbl7.Text = My.Application.LanguageHelper.X9
                lbl8.Text = My.Application.LanguageHelper.X10

                pic5.Image = My.Resources.Mini_SettingsIcons
                pic6.Image = My.Resources.Mini_Start11
                pic7.Image = My.Resources.Mini_StartMenuAccent
                pic8.Image = My.Resources.Mini_Taskbar

                Select Case Not [CP].WinMode_Light
                    Case True   ''''''''''Dark
                        lbl1.Text = My.Application.LanguageHelper.X1
                        lbl2.Text = My.Application.LanguageHelper.X2
                        lbl3.Text = My.Application.LanguageHelper.X3
                        lbl4.Text = My.Application.LanguageHelper.X4

                        pic1.Image = My.Resources.Mini_StartMenu_Taskbar_AC
                        pic2.Image = My.Resources.Mini_ACHover_Links
                        pic3.Image = My.Resources.Mini_Lines_Toggles_Buttons
                        pic4.Image = My.Resources.Mini_TaskbarActiveIcon

                        If ReorderAfterPreviewConfigChange Then
                            pnl1.Top = Label10.Bottom + 3
                            pnl2.Top = pnl1.Bottom + 2
                            pnl3.Top = pnl2.Bottom + 2
                            pnl4.Top = pnl3.Bottom + 2
                        End If

                        taskbar.BackColorAlpha = 130
                        start.BackColorAlpha = 130
                        ActionCenter.BackColorAlpha = 130

                        If [CP].ApplyAccentonTaskbar Then
                            Visual.FadeColor(taskbar, "BackColor", taskbar.BackColor, Color.FromArgb(taskbar.BackColor.A, [CP].StartListFolders_TaskbarFront), AnimX1, AnimX2)
                            Visual.FadeColor(start, "BackColor", start.BackColor, Color.FromArgb(start.BackColor.A, [CP].StartListFolders_TaskbarFront), AnimX1, AnimX2)
                            Visual.FadeColor(ActionCenter, "BackColor", ActionCenter.BackColor, Color.FromArgb(ActionCenter.BackColor.A, [CP].StartListFolders_TaskbarFront), AnimX1, AnimX2)
                        Else
                            Visual.FadeColor(taskbar, "BackColor", taskbar.BackColor, Color.FromArgb(55, 55, 55), AnimX1, AnimX2)
                            Visual.FadeColor(start, "BackColor", start.BackColor, Color.FromArgb(40, 40, 40), AnimX1, AnimX2)
                            Visual.FadeColor(ActionCenter, "BackColor", ActionCenter.BackColor, Color.FromArgb(55, 55, 55), AnimX1, AnimX2)
                        End If

                        Visual.FadeColor(ActionCenter, "ActionCenterButton_Normal", ActionCenter.ActionCenterButton_Normal, [CP].Taskbar_Icon_Underline, AnimX1, AnimX2)
                        Visual.FadeColor(ActionCenter, "ActionCenterButton_Hover", ActionCenter.ActionCenterButton_Hover, [CP].ActionCenter_AppsLinks, AnimX1, AnimX2)
                        Visual.FadeColor(ActionCenter, "ActionCenterButton_Pressed", ActionCenter.ActionCenterButton_Pressed, [CP].StartButton_Hover, AnimX1, AnimX2)
                        Visual.FadeColor(start, "SearchBoxAccent", start.SearchBoxAccent, [CP].Taskbar_Icon_Underline, AnimX1, AnimX2)
                        Visual.FadeColor(taskbar, "AppUnderline", taskbar.AppUnderline, [CP].Taskbar_Icon_Underline, AnimX1, AnimX2)

                        Visual.FadeColor(Label3, "Forecolor", Label3.ForeColor, [CP].SettingsIconsAndLinks, AnimX1, AnimX2)
                        Visual.FadeColor(Label12, "Forecolor", Label12.ForeColor, [CP].ActionCenter_AppsLinks, AnimX1, AnimX2)

                    Case False   ''''''''''Light
                        lbl1.Text = My.Application.LanguageHelper.X2
                        lbl2.Text = My.Application.LanguageHelper.X5
                        lbl3.Text = My.Application.LanguageHelper.X6
                        lbl4.Text = My.Application.LanguageHelper.X3

                        pic1.Image = My.Resources.Mini_ACHover_Links
                        pic2.Image = My.Resources.Mini_StartMenu_Taskbar_AC
                        pic3.Image = My.Resources.Mini_StartMenuAccent
                        pic4.Image = My.Resources.Mini_Lines_Toggles_Buttons

                        If ReorderAfterPreviewConfigChange Then
                            pnl2.Top = Label10.Bottom + 3
                            pnl1.Top = pnl2.Bottom + 2
                            pnl4.Top = pnl1.Bottom + 2
                            pnl3.Top = pnl4.Bottom + 2
                        End If

                        taskbar.BackColorAlpha = 180
                        start.BackColorAlpha = 180
                        ActionCenter.BackColorAlpha = 180

                        If [CP].ApplyAccentonTaskbar Then
                            Visual.FadeColor(taskbar, "BackColor", taskbar.BackColor, Color.FromArgb(taskbar.BackColor.A, [CP].Taskbar_Icon_Underline), AnimX1, AnimX2)
                            Visual.FadeColor(start, "BackColor", start.BackColor, Color.FromArgb(start.BackColor.A, [CP].ActionCenter_AppsLinks), AnimX1, AnimX2)
                            Visual.FadeColor(ActionCenter, "BackColor", ActionCenter.BackColor, Color.FromArgb(ActionCenter.BackColor.A, [CP].ActionCenter_AppsLinks), AnimX1, AnimX2)
                        Else
                            Visual.FadeColor(taskbar, "BackColor", taskbar.BackColor, Color.FromArgb(255, 255, 255), AnimX1, AnimX2)
                            Visual.FadeColor(start, "BackColor", start.BackColor, Color.FromArgb(255, 255, 255), AnimX1, AnimX2)
                            Visual.FadeColor(ActionCenter, "BackColor", ActionCenter.BackColor, Color.FromArgb(255, 255, 255), AnimX1, AnimX2)
                        End If

                        Visual.FadeColor(ActionCenter, "ActionCenterButton_Normal", ActionCenter.ActionCenterButton_Normal, [CP].StartMenuBackground_ActiveTaskbarButton, AnimX1, AnimX2)
                        Visual.FadeColor(ActionCenter, "ActionCenterButton_Hover", ActionCenter.ActionCenterButton_Hover, [CP].StartListFolders_TaskbarFront, AnimX1, AnimX2)
                        Visual.FadeColor(ActionCenter, "ActionCenterButton_Pressed", ActionCenter.ActionCenterButton_Pressed, [CP].StartButton_Hover, AnimX1, AnimX2)
                        Visual.FadeColor(start, "SearchBoxAccent", start.SearchBoxAccent, [CP].StartMenuBackground_ActiveTaskbarButton, AnimX1, AnimX2)
                        Visual.FadeColor(taskbar, "AppUnderline", taskbar.AppUnderline, [CP].StartMenuBackground_ActiveTaskbarButton, AnimX1, AnimX2)

                        Visual.FadeColor(Label3, "Forecolor", Label3.ForeColor, [CP].SettingsIconsAndLinks, AnimX1, AnimX2)
                        Visual.FadeColor(Label12, "Forecolor", Label12.ForeColor, [CP].StartListFolders_TaskbarFront, AnimX1, AnimX2)
                End Select
#End Region
            Case WinVer.Ten
#Region "Win10"
                start.DarkMode = Not [CP].WinMode_Light
                taskbar.DarkMode = Not [CP].WinMode_Light
                ActionCenter.DarkMode = Not [CP].WinMode_Light

                taskbar.Transparency = [CP].Transparency
                start.Transparency = [CP].Transparency
                ActionCenter.Transparency = [CP].Transparency

                start.Top = taskbar.Top - start.Height

                lbl6.Text = My.Application.LanguageHelper.X11

                If [CP].WinMode_Light And Not [CP].ApplyAccentonTaskbar Then
                    lbl1.Text = My.Application.LanguageHelper.X16
                    lbl2.Text = My.Application.LanguageHelper.X16
                    lbl3.Text = My.Application.LanguageHelper.X16
                    lbl4.Text = My.Application.LanguageHelper.X5
                    lbl5.Text = My.Application.LanguageHelper.X12
                    lbl7.Text = My.Application.LanguageHelper.X16
                    lbl8.Text = My.Application.LanguageHelper.X14

                    pic1.Image = My.Resources.Mini_NotUsed
                    pic2.Image = My.Resources.Mini_NotUsed
                    pic3.Image = My.Resources.Mini_NotUsed
                    pic4.Image = My.Resources.Mini_StartMenu_Taskbar_AC
                    pic5.Image = My.Resources.Mini_SettingsIcons
                    pic6.Image = My.Resources.Mini_Start10
                    pic7.Image = My.Resources.Mini_NotUsed
                    pic8.Image = My.Resources.Mini_ACHover_Links

                    If ReorderAfterPreviewConfigChange Then
                        pnl4.Top = Label10.Bottom + 3
                        pnl5.Top = pnl4.Bottom + 2
                        pnl8.Top = pnl5.Bottom + 2
                        pnl6.Top = pnl8.Bottom + 2
                        pnl1.Top = pnl6.Bottom + 2
                        pnl2.Top = pnl1.Bottom + 2
                        pnl3.Top = pnl2.Bottom + 2
                        pnl7.Top = pnl3.Bottom + 2
                    End If

                Else
                    If [CP].Transparency Then
                        lbl1.Text = My.Application.LanguageHelper.X16
                        lbl2.Text = My.Application.LanguageHelper.X14
                        lbl3.Text = My.Application.LanguageHelper.X15
                        lbl4.Text = My.Application.LanguageHelper.X5
                        lbl5.Text = My.Application.LanguageHelper.X7
                        lbl7.Text = My.Application.LanguageHelper.X16
                        lbl8.Text = My.Application.LanguageHelper.X6

                        pic1.Image = My.Resources.Mini_NotUsed
                        pic2.Image = My.Resources.Mini_ACHover_Links
                        pic3.Image = My.Resources.Mini_TaskbarActiveIcon
                        pic4.Image = My.Resources.Mini_StartMenu_Taskbar_AC
                        pic5.Image = My.Resources.Mini_SettingsIcons
                        pic6.Image = My.Resources.Mini_Start10
                        pic7.Image = My.Resources.Mini_NotUsed
                        pic8.Image = My.Resources.Mini_StartMenuAccent

                        If ReorderAfterPreviewConfigChange Then
                            pnl4.Top = Label10.Bottom + 3
                            pnl8.Top = pnl4.Bottom + 2
                            pnl3.Top = pnl8.Bottom + 2
                            pnl5.Top = pnl3.Bottom + 2
                            pnl2.Top = pnl5.Bottom + 2
                            pnl6.Top = pnl2.Bottom + 2
                            pnl1.Top = pnl6.Bottom + 2
                            pnl7.Top = pnl1.Bottom + 2
                        End If

                    Else
                        lbl1.Text = My.Application.LanguageHelper.X6
                        lbl2.Text = My.Application.LanguageHelper.X14
                        lbl3.Text = My.Application.LanguageHelper.X15
                        lbl4.Text = My.Application.LanguageHelper.X13
                        lbl5.Text = My.Application.LanguageHelper.X7
                        lbl7.Text = My.Application.LanguageHelper.X16
                        lbl8.Text = My.Application.LanguageHelper.X16

                        pic1.Image = My.Resources.Mini_StartMenuAccent
                        pic2.Image = My.Resources.Mini_ACHover_Links
                        pic3.Image = My.Resources.Mini_TaskbarActiveIcon
                        pic4.Image = My.Resources.Mini_StartMenu_Taskbar_AC
                        pic5.Image = My.Resources.Mini_SettingsIcons
                        pic6.Image = My.Resources.Mini_Start10
                        pic7.Image = My.Resources.Mini_NotUsed
                        pic8.Image = My.Resources.Mini_NotUsed

                        If ReorderAfterPreviewConfigChange Then
                            pnl4.Top = Label10.Bottom + 3
                            pnl1.Top = pnl4.Bottom + 2
                            pnl3.Top = pnl1.Bottom + 2
                            pnl5.Top = pnl3.Bottom + 2
                            pnl2.Top = pnl5.Bottom + 2
                            pnl6.Top = pnl2.Bottom + 2
                            pnl7.Top = pnl6.Bottom + 2
                            pnl8.Top = pnl7.Bottom + 2
                        End If
                    End If
                End If

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
                    Visual.FadeColor(taskbar, "BackColor", taskbar.BackColor, Color.FromArgb(237, 237, 237), AnimX1, AnimX2)
                    Visual.FadeColor(start, "BackColor", start.BackColor, Color.FromArgb(227, 227, 227), AnimX1, AnimX2)
                    Visual.FadeColor(ActionCenter, "BackColor", ActionCenter.BackColor, Color.FromArgb(227, 227, 227), AnimX1, AnimX2)
                    Visual.FadeColor(taskbar, "StartColor", taskbar.StartColor, Color.Transparent, AnimX1, AnimX2)
                Else
                    If Not [CP].ApplyAccentonTaskbar Then
                        Visual.FadeColor(taskbar, "BackColor", taskbar.BackColor, Color.FromArgb(23, 23, 23), AnimX1, AnimX2)
                        Visual.FadeColor(start, "BackColor", start.BackColor, Color.FromArgb(36, 36, 36), AnimX1, AnimX2)
                        Visual.FadeColor(ActionCenter, "BackColor", ActionCenter.BackColor, Color.FromArgb(36, 36, 36), AnimX1, AnimX2)
                        Visual.FadeColor(taskbar, "StartColor", taskbar.StartColor, Color.Transparent, AnimX1, AnimX2)

                    Else
                        Visual.FadeColor(start, "BackColor", start.BackColor, [CP].StartMenuBackground_ActiveTaskbarButton, AnimX1, AnimX2)
                        Visual.FadeColor(taskbar, "StartColor", taskbar.StartColor, [CP].StartMenuBackground_ActiveTaskbarButton, AnimX1, AnimX2)
                        Visual.FadeColor(ActionCenter, "BackColor", ActionCenter.BackColor, [CP].StartMenuBackground_ActiveTaskbarButton, AnimX1, AnimX2)

                        If [CP].Transparency Then
                            Visual.FadeColor(taskbar, "BackColor", taskbar.BackColor, [CP].Taskbar_Background, AnimX1, AnimX2)
                        Else
                            Visual.FadeColor(taskbar, "BackColor", taskbar.BackColor, [CP].StartListFolders_TaskbarFront, AnimX1, AnimX2)
                        End If

                    End If
                End If

                If [CP].WinMode_Light And Not [CP].ApplyAccentonTaskbar Then
                    Visual.FadeColor(Label3, "Forecolor", Label3.ForeColor, [CP].SettingsIconsAndLinks, AnimX1, AnimX2)
                    Visual.FadeColor(Label12, "Forecolor", Label12.ForeColor, [CP].Taskbar_Background, AnimX1, AnimX2)
                    Visual.FadeColor(ActionCenter, "LinkColor", ActionCenter.LinkColor, [CP].Taskbar_Background, AnimX1, AnimX2)
                    Visual.FadeColor(taskbar, "AppBackground", taskbar.AppBackground, Color.Transparent, AnimX1, AnimX2)
                Else
                    Visual.FadeColor(Label3, "Forecolor", Label3.ForeColor, [CP].SettingsIconsAndLinks, AnimX1, AnimX2)
                    Visual.FadeColor(Label12, "Forecolor", Label12.ForeColor, [CP].ActionCenter_AppsLinks, AnimX1, AnimX2)
                    Visual.FadeColor(taskbar, "AppUnderline", taskbar.AppUnderline, [CP].Taskbar_Icon_Underline, AnimX1, AnimX2)
                    Visual.FadeColor(ActionCenter, "LinkColor", ActionCenter.LinkColor, [CP].Taskbar_Icon_Underline, AnimX1, AnimX2)

                    If Not [CP].Transparency And Not [CP].ApplyAccentonTaskbar Then
                        Visual.FadeColor(taskbar, "AppBackground", taskbar.AppBackground, Color.Transparent, AnimX1, AnimX2)
                    Else
                        Visual.FadeColor(taskbar, "AppBackground", taskbar.AppBackground, [CP].StartMenuBackground_ActiveTaskbarButton, AnimX1, AnimX2)
                    End If
                End If

                ReValidateLivePreview(pnl_preview)
#End Region
        End Select
    End Sub

    Sub ReValidateLivePreview(ByVal Parent As Control)
        Parent.Invalidate()

        For Each ctrl As Control In Parent.Controls
            If ctrl.HasChildren Then
                For Each c As Control In ctrl.Controls
                    ReValidateLivePreview(c)
                Next
            End If
        Next
    End Sub

    Sub Adjust_Preview()
        If _Shown Then My.Application.AnimatorX.HideSync(pnl_preview)

        Panel3.Visible = True
        Label12.Visible = True

        Select Case PreviewConfig
            Case WinVer.Eleven
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

                XenonWindow1.RoundedCorners = True
                XenonWindow2.RoundedCorners = True

            Case WinVer.Ten
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

                XenonWindow1.RoundedCorners = False
                XenonWindow2.RoundedCorners = False

            Case WinVer.Eight
                Panel3.Visible = False
                Label12.Visible = False
                ActionCenter.Visible = False
                taskbar.UseItAsTaskbar_Version = XenonAcrylic.TaskbarVersion.Eight
                taskbar.BlurPower = 0
                taskbar.Height = 34

                start.Visible = False
                start.BlurPower = 0
                start.Top = taskbar.Top - start.Height
                start.Left = 0


            Case WinVer.Seven
                Panel3.Visible = False
                Label12.Visible = False
                ActionCenter.Visible = False
                taskbar.UseItAsTaskbar_Version = XenonAcrylic.TaskbarVersion.Seven
                taskbar.BlurPower = 1
                taskbar.NoisePower = 0.2
                taskbar.Height = 34

                start.Visible = True
                start.UseItAsTaskbar_Version = XenonAcrylic.TaskbarVersion.Seven
                start.RoundedCorners = True
                start.BlurPower = 1
                start.Left = 0
                start.Width = 136
                start.Height = 191
                start.Top = taskbar.Top - start.Height
        End Select

        XenonWindow1.Top = start.Top
        XenonWindow1.Left = start.Right + 5

        XenonWindow2.Top = XenonWindow1.Bottom + 5
        XenonWindow2.Left = XenonWindow1.Left

        XenonWindow1.Invalidate()
        XenonWindow2.Invalidate()

        ReValidateLivePreview(pnl_preview)

        If _Shown Then My.Application.AnimatorX.ShowSync(pnl_preview)
    End Sub

    Sub ApplyCPValues(ByVal ColorPalette As CP)
        themename_lbl.Text = String.Format("{0} ({1})", CP.PaletteName, CP.PaletteVersion)
        author_lbl.Text = String.Format("{0}: {1}", My.Application.LanguageHelper.By, CP.Author)

        WinMode_Toggle.Checked = Not ColorPalette.WinMode_Light
        AppMode_Toggle.Checked = Not ColorPalette.AppMode_Light
        Transparency_Toggle.Checked = ColorPalette.Transparency
        ShowAccentOnTitlebarAndBorders_Toggle.Checked = ColorPalette.ApplyAccentonTitlebars
        AccentOnStartAndTaskbar_Toggle.Checked = ColorPalette.ApplyAccentonTaskbar
        ActiveTitlebar_picker.BackColor = ColorPalette.Titlebar_Active
        InactiveTitlebar_picker.BackColor = ColorPalette.Titlebar_Inactive
        StartAccent_picker.BackColor = ColorPalette.StartMenu_Accent
        StartButtonHover_picker.BackColor = ColorPalette.StartButton_Hover
        TaskbarBackground_Picker.BackColor = ColorPalette.Taskbar_Background
        TaskbarIconUnderline_picker.BackColor = ColorPalette.Taskbar_Icon_Underline
        StartBackgroundAndTaskbarButton_picker.BackColor = ColorPalette.StartMenuBackground_ActiveTaskbarButton
        TaskbarFrontAndFoldersOnStart_picker.BackColor = ColorPalette.StartListFolders_TaskbarFront
        ActionCenter_picker.BackColor = ColorPalette.ActionCenter_AppsLinks
        SettingsIconsAndLinks_picker.BackColor = ColorPalette.SettingsIconsAndLinks

        Aero_ColorizationColor_pick.BackColor = ColorPalette.Aero_ColorizationColor
        Aero_ColorizationAfterglow_pick.BackColor = ColorPalette.Aero_ColorizationAfterglow
        Aero_ColorizationColorBalance_txt.Text = ColorPalette.Aero_ColorizationColorBalance
        Aero_ColorizationAfterglowBalance_txt.Text = ColorPalette.Aero_ColorizationAfterglowBalance
        Aero_ColorizationBlurBalance_txt.Text = ColorPalette.Aero_ColorizationBlurBalance
        Aero_ColorizationGlassReflectionIntensity_txt.Text = ColorPalette.Aero_ColorizationGlassReflectionIntensity
        Aero_CompositionPolicy_txt.Text = ColorPalette.Aero_CompositionPolicy
        Aero_ColorizationOpaqueBlend_txt.Text = ColorPalette.Aero_ColorizationOpaqueBlend
        Aero_LastDisqualifiedCompositionSignature_txt.Text = ColorPalette.Aero_LastDisqualifiedCompositionSignature
        Aero_EnableAeroPeek_toggle.Checked = ColorPalette.Aero_EnableAeroPeek
        Aero_Composition_toggle.Checked = ColorPalette.Aero_Composition
        Aero_AlwaysHibernateThumbnails_Toggle.Checked = ColorPalette.Aero_AlwaysHibernateThumbnails
        Aero_EnableWindowColorization_toggle.Checked = ColorPalette.Aero_EnableWindowColorization
    End Sub
#End Region

#Region "Misc"
    Enum WinVer
        Eleven
        Ten
        Eight
        Seven
    End Enum

    Public Sub DoubleBufferedControl(ByVal [Control] As Control, ByVal setting As Boolean)
        Dim panType As Type = [Control].[GetType]()
        Dim pi As PropertyInfo = panType.GetProperty("DoubleBuffered", BindingFlags.Instance Or BindingFlags.NonPublic)
        pi.SetValue([Control], setting, Nothing)
    End Sub

    Sub MakeItDoubleBuffered(ByVal Parent As Control)
        DoubleBufferedControl(Parent, True)

        For Each ctrl As Control In Parent.Controls

            If ctrl.HasChildren Then
                For Each c As Control In ctrl.Controls
                    MakeItDoubleBuffered(c)
                Next
            End If

            DoubleBufferedControl(ctrl, True)
        Next
    End Sub

    Sub UpdateHint(Sender As Object, e As EventArgs)
        status_lbl.Text = TryCast(Sender, XenonButton).Tag
    End Sub

    Sub EraseHint()
        status_lbl.Text = ""
    End Sub
#End Region

    Sub AutoUpdatesCheck()
        StableInt = 0 : BetaInt = 0 : UpdateChannel = 0 : ChannelFixer = 0
        If My.Application._Settings.UpdateChannel = XeSettings.UpdateChannels.Stable Then ChannelFixer = 0
        If My.Application._Settings.UpdateChannel = XeSettings.UpdateChannels.Beta Then ChannelFixer = 1
        BackgroundWorker1.RunWorkerAsync()
    End Sub

    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        If IsNetAvaliable() Then
            Try

                Dim ls As New List(Of String)
                Dim WebCL As New WebClient
                RaiseUpdate = False
                ver = ""

                CList_FromStr(ls, WebCL.DownloadString(My.Resources.Link_Updates))

                For x = 0 To ls.Count - 1
                    If Not String.IsNullOrEmpty(ls(x)) And Not ls(x).IndexOf("#") = 0 Then
                        If ls(x).Split(" ")(0) = "Stable" Then StableInt = x
                        If ls(x).Split(" ")(0) = "Beta" Then BetaInt = x
                    End If
                Next

                If ChannelFixer = 0 Then UpdateChannel = StableInt
                If ChannelFixer = 1 Then UpdateChannel = BetaInt

                ver = ls(UpdateChannel).Split(" ")(1)

                If ver > My.Application.Info.Version.ToString Then
                    RaiseUpdate = True
                End If
            Catch
            End Try
        End If
    End Sub

    Private Sub BackgroundWorker1_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        If RaiseUpdate Then Notify(String.Format("{0} ({2}). {1}.", My.Application.LanguageHelper.NewUpdate, My.Application.LanguageHelper.OpenForActions, ver), My.Resources.notify_update, 10000)
    End Sub

    Private Sub MainFrm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _Shown = False

        If My.W10 Or My.W11 Then
            PaletteContainer_W1x.Visible = True
            PaletteContainer_W7x.Visible = False
        End If

        If My.W7 Or My.W8 Then
            PaletteContainer_W1x.Visible = False
            PaletteContainer_W7x.Visible = True
        End If

        ApplyDarkMode(Me)
        MakeItDoubleBuffered(Me)
        My.Application.AdjustFonts()
        XenonButton20.Image = If(My.W11, My.Resources.Native11, My.Resources.Native10)

        For Each btn As XenonButton In XenonGroupBox2.Controls.OfType(Of XenonButton)
            AddHandler btn.MouseEnter, AddressOf UpdateHint
            AddHandler btn.Enter, AddressOf UpdateHint
            AddHandler btn.MouseLeave, AddressOf EraseHint
            AddHandler btn.Leave, AddressOf EraseHint
        Next

        If Not My.Application.ExternalLink Then
            CP = New CP(CP.Mode.Registry)
            CP_Original = New CP(CP.Mode.Registry)
            CP_FirstTime = New CP(CP.Mode.Registry)
        Else
            CP = New CP(CP.Mode.File, My.Application.ExternalLink_File)
            CP_Original = New CP(CP.Mode.File, My.Application.ExternalLink_File)
            CP_FirstTime = New CP(CP.Mode.File, My.Application.ExternalLink_File)
            OpenFileDialog1.FileName = My.Application.ExternalLink_File
            SaveFileDialog1.FileName = My.Application.ExternalLink_File
            My.Application.ExternalLink = False
            My.Application.ExternalLink_File = ""
        End If

        If My.Application._Settings.CustomPreviewConfig_Enabled Then
            PreviewConfig = My.Application._Settings.CustomPreviewConfig
        Else
            If My.W11 Then PreviewConfig = WinVer.Eleven
            If My.W10 Then PreviewConfig = WinVer.Ten
            If My.W8 Then PreviewConfig = WinVer.Eight
            If My.W7 Then PreviewConfig = WinVer.Seven
        End If

        PreviewConfig = WinVer.Seven

        pnl_preview.BackgroundImage = My.Application.Wallpaper
        dragPreviewer.pnl_preview.BackgroundImage = My.Application.Wallpaper

        Adjust_Preview()
        ApplyCPValues(CP)
        ApplyLivePreviewFromCP(CP)
    End Sub

    Private Sub MainFrm_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        _Shown = True

        If My.Application._Settings.AutoUpdatesChecking Then AutoUpdatesCheck()
    End Sub

    Protected Overrides Sub OnFormClosing(ByVal e As FormClosingEventArgs)
        Dim Changed As Boolean = Not CP.Equals(CP_Original)

        If e.CloseReason = CloseReason.UserClosing And Changed Then

            Select Case ComplexSave.ShowDialog
                Case DialogResult.Yes

                    Dim r As String() = My.Application.ComplexSaveResult.Split(".")
                    Dim r1 As String = r(0)
                    Dim r2 As String = r(1)

                    Select Case r1
                        Case 0              '' Save
                            If IO.File.Exists(SaveFileDialog1.FileName) Then
                                CP.Save(CP.SavingMode.File, SaveFileDialog1.FileName)
                                CP_Original = CP
                            Else
                                If SaveFileDialog1.ShowDialog = DialogResult.OK Then
                                    CP.Save(CP.SavingMode.File, SaveFileDialog1.FileName)
                                    CP_Original = CP
                                Else
                                    e.Cancel = True
                                End If
                            End If
                        Case 1              '' Save As
                            If SaveFileDialog1.ShowDialog = DialogResult.OK Then
                                CP.Save(CP.SavingMode.File, SaveFileDialog1.FileName)
                                CP_Original = CP
                            Else
                                e.Cancel = True
                            End If
                    End Select

                    Select Case r2
                        Case 1      '' Apply   ' Case 0= Don't Apply
                            CP.Save(CP.SavingMode.Registry)
                            RestartExplorer()
                    End Select

                Case DialogResult.No
                    e.Cancel = False
                    MyBase.OnFormClosing(e)

                Case DialogResult.Cancel
                    e.Cancel = True
            End Select

        ElseIf e.CloseReason = CloseReason.UserClosing And Not Changed Then
            e.Cancel = False
            MyBase.OnFormClosing(e)
        End If
    End Sub

    Private Sub XenonGroupBox10_Click(sender As Object, e As EventArgs) Handles ActiveTitlebar_picker.Click
        Dim CList As New List(Of Control) From {sender, XenonWindow1}

        Dim C As Color = ColorPickerDlg.Pick(CList)
        CP.Titlebar_Active = Color.FromArgb(255, C)
        ApplyLivePreviewFromCP(CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()

        If Not CP.ApplyAccentonTitlebars Then
            Notify(My.Application.LanguageHelper.X17, My.Resources.notify_info, 5000)
        End If
    End Sub

    Private Sub XenonGroupBox21_Click(sender As Object, e As EventArgs) Handles InactiveTitlebar_picker.Click

        Dim CList As New List(Of Control) From {sender, XenonWindow2}

        Dim _Conditions As New Conditions With {.Window_InactiveTitlebar = True}
        Dim C As Color = ColorPickerDlg.Pick(CList, _Conditions)

        CP.Titlebar_Inactive = Color.FromArgb(255, C)
        ApplyLivePreviewFromCP(CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()

        If Not CP.ApplyAccentonTitlebars Then
            Notify(My.Application.LanguageHelper.X18, My.Resources.notify_info, 5000)
        End If
    End Sub

    Private Sub WinMode_Toggle_CheckedChanged(sender As Object, e As EventArgs) Handles WinMode_Toggle.CheckedChanged
        CP.WinMode_Light = Not sender.Checked
        ApplyLivePreviewFromCP(CP)
    End Sub

    Private Sub AppMode_Toggle_CheckedChanged(sender As Object, e As EventArgs) Handles AppMode_Toggle.CheckedChanged
        CP.AppMode_Light = Not sender.Checked
        ApplyLivePreviewFromCP(CP)
    End Sub

    Private Sub Transparency_Toggle_CheckedChanged(sender As Object, e As EventArgs) Handles Transparency_Toggle.CheckedChanged
        CP.Transparency = sender.Checked
        ApplyLivePreviewFromCP(CP)
    End Sub

    Private Sub ShowAccentOnTitlebarAndBorders_Toggle_CheckedChanged(sender As Object, e As EventArgs) Handles ShowAccentOnTitlebarAndBorders_Toggle.CheckedChanged
        CP.ApplyAccentonTitlebars = sender.Checked
        ApplyLivePreviewFromCP(CP)
    End Sub

    Private Sub AccentOnStartAndTaskbar_Toggle_CheckedChanged(sender As Object, e As EventArgs) Handles AccentOnStartAndTaskbar_Toggle.CheckedChanged
        CP.ApplyAccentonTaskbar = sender.Checked
        ApplyLivePreviewFromCP(CP)
    End Sub

    Private Sub TaskbarIconUnderline_picker_Click(sender As Object, e As EventArgs) Handles TaskbarIconUnderline_picker.Click
        Dim CList As New List(Of Control)
        Dim C As Color
        CList.Add(sender)

        If PreviewConfig = WinVer.Eleven Then
            CList.Add(taskbar)

            If Not CP.WinMode_Light Then
                CList.Add(ActionCenter)
                CList.Add(start)
                CList.Add(ShowAccentOnTitlebarAndBorders_Toggle)
                CList.Add(WinMode_Toggle)
                CList.Add(AppMode_Toggle)
                CList.Add(Transparency_Toggle)
                CList.Add(AccentOnStartAndTaskbar_Toggle)

                Dim _Conditions As New Conditions With {
                    .AppUnderlineOnly = True,
                     .StartSearchOnly = True,
                     .ActionCenterBtn = True
 }

                C = ColorPickerDlg.Pick(CList, _Conditions)
            Else
                C = ColorPickerDlg.Pick(CList)
            End If
        Else
            If CP.WinMode_Light And Not CP.ApplyAccentonTaskbar Then
            Else
                CList.Add(taskbar)

                Dim _Conditions As New Conditions With {.AppUnderlineOnly = True}
                C = ColorPickerDlg.Pick(CList, _Conditions)
            End If
        End If

        CP.Taskbar_Icon_Underline = Color.FromArgb(255, C)
        ApplyLivePreviewFromCP(CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()


        If PreviewConfig = WinVer.Ten Then
            If CP.WinMode_Light And Not CP.ApplyAccentonTaskbar Then
                Notify(My.Application.LanguageHelper.X19, My.Resources.notify_info, 5000)
            End If
        End If
    End Sub

    Private Sub TaskbarFrontAndFoldersOnStart_picker_Click(sender As Object, e As EventArgs) Handles TaskbarFrontAndFoldersOnStart_picker.Click
        Dim CList As New List(Of Control) From {sender}

        If PreviewConfig = WinVer.Eleven Then
            If Not CP.WinMode_Light Then
                CList.Add(taskbar)
                CList.Add(start)
                CList.Add(ActionCenter)
            Else
                CList.Add(Label12)
            End If
        Else
            If Not CP.Transparency Then
                If Not CP.WinMode_Light Then
                    CList.Add(taskbar)
                Else
                    If CP.ApplyAccentonTaskbar Then CList.Add(taskbar)
                End If
            End If
        End If

        Dim C As Color = ColorPickerDlg.Pick(CList)
        CP.StartListFolders_TaskbarFront = Color.FromArgb(255, C)
        ApplyLivePreviewFromCP(CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()

        If PreviewConfig = WinVer.Eleven Then
            If Not CP.WinMode_Light And Not CP.ApplyAccentonTaskbar Then
                Notify(My.Application.LanguageHelper.X20, My.Resources.notify_info, 5000)
            End If
        Else
            If Not CP.Transparency And Not CP.WinMode_Light And Not CP.ApplyAccentonTaskbar Then
                Notify(My.Application.LanguageHelper.X19, My.Resources.notify_info, 5000)
            End If
        End If

    End Sub

    Private Sub ActionCenter_picker_Click(sender As Object, e As EventArgs) Handles ActionCenter_picker.Click
        Dim CList As New List(Of Control)
        Dim C As Color

        CList.Add(sender)

        If PreviewConfig = WinVer.Eleven Then
            If CP.WinMode_Light Then
                CList.Add(start)
                CList.Add(ActionCenter)
                C = ColorPickerDlg.Pick(CList)
            Else
                CList.Add(Label12)
                Dim _Conditions As New Conditions With {.AppUnderlineOnly = True}
                C = ColorPickerDlg.Pick(CList, _Conditions)
            End If
        Else
            If CP.WinMode_Light And Not CP.ApplyAccentonTaskbar Then

            Else
                CList.Add(ActionCenter)
                CList.Add(Label12)
                Dim _Conditions As New Conditions With {.ActionCenterLink = True}
                C = ColorPickerDlg.Pick(CList, _Conditions)
            End If
        End If

        CP.ActionCenter_AppsLinks = Color.FromArgb(255, C)
        ApplyLivePreviewFromCP(CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()

        If PreviewConfig = WinVer.Ten Then
            If CP.WinMode_Light And Not CP.ApplyAccentonTaskbar Then
                Notify(My.Application.LanguageHelper.X21, My.Resources.notify_info, 5000)
            End If
        End If
    End Sub

    Private Sub SettingsIconsAndLinks_picker_Click(sender As Object, e As EventArgs) Handles SettingsIconsAndLinks_picker.Click
        Dim CList As New List(Of Control)
        Dim C As Color

        CList.Add(sender)

        If PreviewConfig = WinVer.Eleven Then
            CList.Add(Label3)
            C = ColorPickerDlg.Pick(CList)
        Else
            If CP.WinMode_Light And Not CP.ApplyAccentonTaskbar Then
                CList.Add(Label3)
                CList.Add(taskbar)
                Dim _Conditions As New Conditions With {.AppUnderlineOnly = True}
                C = ColorPickerDlg.Pick(CList, _Conditions)
            Else
                CList.Add(Label3)
                C = ColorPickerDlg.Pick(CList)
            End If
        End If

        CP.SettingsIconsAndLinks = Color.FromArgb(255, C)
        ApplyLivePreviewFromCP(CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()
    End Sub

    Private Sub XenonButton4_Click(sender As Object, e As EventArgs) Handles apply_btn.Click
        CP_Original = CP
        CP.Save(CP.SavingMode.Registry)

        RefreshRegisrty()

        If My.W7 Or My.W8 Then
            Visible = False
            My.Application.AeroKiller.Start()
            My.Application.AeroKiller.WaitForExit()
            My.Application.AeroStarter.Start()
            Visible = True
        End If

        If My.Application._Settings.AutoRestartExplorer Then RestartExplorer() Else Notify(My.Application.LanguageHelper.NoDefResExplorer.Replace("<br>", vbCrLf), My.Resources.notify_warning, 7500)
    End Sub

    Private Sub XenonButton4_MouseEnter(sender As Object, e As EventArgs) Handles apply_btn.MouseEnter
        If My.Application._Settings.AutoRestartExplorer Then
            status_lbl.Text = My.Application.LanguageHelper.X22
            status_lbl.ForeColor = Color.Gold
        End If
    End Sub

    Private Sub XenonButton4_MouseLeave(sender As Object, e As EventArgs) Handles apply_btn.MouseLeave
        If My.Application._Settings.AutoRestartExplorer Then
            status_lbl.Text = ""
            status_lbl.ForeColor = If(GetDarkMode(), Color.White, Color.Black)
        End If
    End Sub

    Private Sub XenonGroupBox12_Click(sender As Object, e As EventArgs) Handles TaskbarBackground_Picker.Click
        Dim CList As New List(Of Control)
        Dim C As Color

        CList.Add(sender)

        If PreviewConfig = WinVer.Eleven Then
            CList.Add(taskbar)
            Dim _Conditions As New Conditions With {.AppBackgroundOnly = True}
            C = ColorPickerDlg.Pick(CList, _Conditions)
        Else

            If CP.WinMode_Light Then
                If CP.Transparency Then
                    If CP.ApplyAccentonTaskbar Then CList.Add(taskbar) Else CList.Add(Label12)
                Else
                    CList.Add(Label12)
                End If
            Else
                If CP.Transparency Then CList.Add(taskbar)
            End If

            C = ColorPickerDlg.Pick(CList)
        End If

        CP.Taskbar_Background = Color.FromArgb(255, C)
        ApplyLivePreviewFromCP(CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()


        If PreviewConfig = WinVer.Ten Then
            If CP.WinMode_Light And Not CP.ApplyAccentonTaskbar Then

            Else
                If CP.Transparency Then
                    If Not CP.ApplyAccentonTaskbar Then
                        Notify(My.Application.LanguageHelper.X19, My.Resources.notify_info, 5000)
                    End If
                Else

                End If
            End If
        End If
    End Sub

    Private Sub StartAccent_picker_Click(sender As Object, e As EventArgs) Handles StartAccent_picker.Click

        Dim CList As New List(Of Control) From {sender}

        If PreviewConfig = WinVer.Eleven Then
            CList.Add(start)
        Else

        End If

        Dim C As Color = ColorPickerDlg.Pick(CList)
        CP.StartMenu_Accent = Color.FromArgb(255, C)
        ApplyLivePreviewFromCP(CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()

    End Sub

    Private Sub StartButtonHover_picker_Click(sender As Object, e As EventArgs) Handles StartButtonHover_picker.Click

        Dim CList As New List(Of Control) From {sender}

        Dim C As Color = ColorPickerDlg.Pick(CList)
        CP.StartButton_Hover = Color.FromArgb(255, C)
        ApplyLivePreviewFromCP(CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()

    End Sub

    Private Sub StartBackgroundAndTaskbarButton_picker_Click(sender As Object, e As EventArgs) Handles StartBackgroundAndTaskbarButton_picker.Click

        Dim CList As New List(Of Control) From {sender}

        Dim C As Color

        If PreviewConfig = WinVer.Eleven Then

            If Not Transparency_Toggle.Checked Then
                CList.Add(taskbar)
                CList.Add(start)
                CList.Add(ActionCenter)
            End If

            If CP.WinMode_Light Then
                CList.Add(start)
                CList.Add(ShowAccentOnTitlebarAndBorders_Toggle)
                CList.Add(WinMode_Toggle)
                CList.Add(AppMode_Toggle)
                CList.Add(Transparency_Toggle)
                CList.Add(AccentOnStartAndTaskbar_Toggle)
                CList.Add(ActionCenter)
                CList.Add(taskbar)

                Dim _Conditions As New Conditions With {
                    .AppUnderlineOnly = True,
                     .StartSearchOnly = True,
                     .ActionCenterBtn = True
 }

                C = ColorPickerDlg.Pick(CList, _Conditions)
            Else
                Dim _Conditions As New Conditions With {.StartColorOnly = True}
                C = ColorPickerDlg.Pick(CList, _Conditions)
            End If

        Else
            If CP.Transparency Then
                CList.Add(start)
                CList.Add(ActionCenter)
                C = ColorPickerDlg.Pick(CList)
            Else
                CList.Add(start)
                CList.Add(ActionCenter)
                CList.Add(taskbar)
                Dim _Conditions As New Conditions With {.AppBackgroundOnly = True}
                C = ColorPickerDlg.Pick(CList, _Conditions)
            End If
        End If

        CP.StartMenuBackground_ActiveTaskbarButton = Color.FromArgb(255, C)
        ApplyLivePreviewFromCP(CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()


        If PreviewConfig = WinVer.Ten Then
            If Not CP.ApplyAccentonTaskbar Then
                Notify(My.Application.LanguageHelper.X21, My.Resources.notify_info, 5000)
            End If
        End If
    End Sub

    Private Sub XenonButton8_Click(sender As Object, e As EventArgs)
        PreviewConfig = WinVer.Ten
        Adjust_Preview()
    End Sub

    Private Sub XenonButton1_Click(sender As Object, e As EventArgs)
        PreviewConfig = WinVer.Eleven
        Adjust_Preview()
    End Sub

    Dim wpth_or_wpsf As Boolean = True
    Dim DragAccepted As Boolean

    Private Sub Me_DragEnter(ByVal sender As Object, ByVal e As DragEventArgs) Handles Me.DragEnter, XenonGroupBox8.DragEnter, pnl_preview.DragEnter,
        PaletteContainer_W1x.DragEnter, XenonGroupBox5.DragEnter, XenonGroupBox1.DragEnter, XenonGroupBox2.DragEnter, XenonGroupBox13.DragEnter

        Dim files() As String = e.Data.GetData(DataFormats.FileDrop)

        If My.Computer.FileSystem.GetFileInfo(files(0)).Extension.ToLower = ".wpth" Then
            wpth_or_wpsf = True
            e.Effect = DragDropEffects.Copy

            If My.Application._Settings.DragAndDropPreview Then
                DragAccepted = True
                CP_BeforeDragAndDrop = CP
                dragPreviewer.Location = New Point(e.X + 15, e.Y + 15)
                dragPreviewer.File = files(0)
                dragPreviewer.Show()
            End If
        ElseIf My.Computer.FileSystem.GetFileInfo(files(0)).Extension.ToLower = ".wpsf" Then
            wpth_or_wpsf = False
            DragAccepted = True
            e.Effect = DragDropEffects.Copy
        Else
            DragAccepted = False
            e.Effect = DragDropEffects.None
        End If

    End Sub

    Private Sub MainFrm_DragLeave(sender As Object, e As EventArgs) Handles Me.DragLeave
        If DragAccepted Then
            If My.Application._Settings.DragAndDropPreview Then dragPreviewer.Close()
            CP = CP_BeforeDragAndDrop
            ApplyCPValues(CP_BeforeDragAndDrop)
            ApplyLivePreviewFromCP(CP_BeforeDragAndDrop)
        End If
    End Sub

    Private Sub MainFrm_DragOver(sender As Object, e As DragEventArgs) Handles Me.DragOver, XenonGroupBox8.DragOver, pnl_preview.DragOver,
        PaletteContainer_W1x.DragOver, XenonGroupBox5.DragOver, XenonGroupBox1.DragOver, XenonGroupBox2.DragOver, XenonGroupBox13.DragOver
        If DragAccepted And My.Application._Settings.DragAndDropPreview Then dragPreviewer.Location = New Point(e.X + 15, e.Y + 15)
    End Sub

    Private Sub MainFrm_DragDrop(sender As Object, e As DragEventArgs) Handles Me.DragDrop, XenonGroupBox8.DragDrop, pnl_preview.DragDrop,
        PaletteContainer_W1x.DragDrop, XenonGroupBox5.DragDrop, XenonGroupBox1.DragDrop, XenonGroupBox2.DragDrop, XenonGroupBox13.DragDrop

        If DragAccepted Then
            Dim files() As String = e.Data.GetData(DataFormats.FileDrop)

            If wpth_or_wpsf Then
                If My.Application._Settings.DragAndDropPreview Then dragPreviewer.Close()

                If Not CP.Equals(CP_Original) Then

                    Select Case ComplexSave.ShowDialog
                        Case DialogResult.Yes

                            Dim r As String() = My.Application.ComplexSaveResult.Split(".")
                            Dim r1 As String = r(0)
                            Dim r2 As String = r(1)

                            Select Case r1
                                Case 0              '' Save
                                    If IO.File.Exists(SaveFileDialog1.FileName) Then
                                        CP.Save(CP.SavingMode.File, SaveFileDialog1.FileName)
                                        CP_Original = CP
                                    Else
                                        If SaveFileDialog1.ShowDialog = DialogResult.OK Then
                                            CP.Save(CP.SavingMode.File, SaveFileDialog1.FileName)
                                            CP_Original = CP
                                        Else
                                            '''''''' If My.Application._Settings.DragPreview then ReleaseBlur()
                                            Exit Sub
                                        End If
                                    End If
                                Case 1              '' Save As
                                    If SaveFileDialog1.ShowDialog = DialogResult.OK Then
                                        CP.Save(CP.SavingMode.File, SaveFileDialog1.FileName)
                                        CP_Original = CP
                                    Else
                                        '''''''' If My.Application._Settings.DragPreview then ReleaseBlur()
                                        Exit Sub
                                    End If
                            End Select

                            Select Case r2
                                Case 1      '' Apply   ' Case 0= Don't Apply
                                    CP.Save(CP.SavingMode.Registry)
                                    RestartExplorer()
                            End Select

                        Case DialogResult.No

                        Case DialogResult.Cancel
                            Exit Sub
                    End Select

                End If

                CP = New CP(CP.Mode.File, files(0))
                ApplyCPValues(CP)
                ApplyLivePreviewFromCP(CP)
            Else
                SettingsX._External = True
                SettingsX._File = files(0)
                SettingsX.ShowDialog()
            End If
        End If

    End Sub

    Private Sub XenonButton7_Click(sender As Object, e As EventArgs) Handles XenonButton7.Click
        If Not IO.File.Exists(SaveFileDialog1.FileName) Then
            If SaveFileDialog1.ShowDialog = DialogResult.OK Then
                CP.Save(CP.SavingMode.File, SaveFileDialog1.FileNames(0))
            End If
        Else
            CP.Save(CP.SavingMode.File, SaveFileDialog1.FileNames(0))
        End If
    End Sub

    Private Sub XenonButton2_Click(sender As Object, e As EventArgs) Handles XenonButton2.Click
        If OpenFileDialog1.ShowDialog = DialogResult.OK Then

            Select Case ComplexSave.ShowDialog
                Case DialogResult.Yes

                    Dim r As String() = My.Application.ComplexSaveResult.Split(".")
                    Dim r1 As String = r(0)
                    Dim r2 As String = r(1)

                    Select Case r1
                        Case 0              '' Save
                            If IO.File.Exists(SaveFileDialog1.FileName) Then
                                CP.Save(CP.SavingMode.File, SaveFileDialog1.FileName)
                                CP_Original = CP
                            Else
                                If SaveFileDialog1.ShowDialog = DialogResult.OK Then
                                    CP.Save(CP.SavingMode.File, SaveFileDialog1.FileName)
                                    CP_Original = CP
                                Else
                                    Exit Sub
                                End If
                            End If
                        Case 1              '' Save As
                            If SaveFileDialog1.ShowDialog = DialogResult.OK Then
                                CP.Save(CP.SavingMode.File, SaveFileDialog1.FileName)
                                CP_Original = CP
                            Else
                                Exit Sub
                            End If
                    End Select

                    Select Case r2
                        Case 1      '' Apply   ' Case 0= Don't Apply
                            CP.Save(CP.SavingMode.Registry)
                            RestartExplorer()
                    End Select

                Case DialogResult.No

                Case DialogResult.Cancel
                    Exit Sub
            End Select

            SaveFileDialog1.FileName = OpenFileDialog1.FileName
            CP = New CP(CP.Mode.File, OpenFileDialog1.FileName)
            ApplyCPValues(CP)
            ApplyLivePreviewFromCP(CP)
        End If
    End Sub

    Private Sub XenonButton9_Click(sender As Object, e As EventArgs) Handles XenonButton9.Click
        If SaveFileDialog1.ShowDialog = DialogResult.OK Then
            CP.Save(CP.SavingMode.File, SaveFileDialog1.FileNames(0))
        End If
    End Sub

    Private Sub XenonButton3_Click(sender As Object, e As EventArgs) Handles XenonButton3.Click
        ContextMenuStrip1.Show(sender, New Point(2, sender.height))
    End Sub

    Private Sub FromCurrentPaletteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FromCurrentPaletteToolStripMenuItem.Click

        If Not CP.Equals(CP_Original) Then

            Select Case ComplexSave.ShowDialog
                Case DialogResult.Yes

                    Dim r As String() = My.Application.ComplexSaveResult.Split(".")
                    Dim r1 As String = r(0)
                    Dim r2 As String = r(1)

                    Select Case r1
                        Case 0              '' Save
                            If IO.File.Exists(SaveFileDialog1.FileName) Then
                                CP.Save(CP.SavingMode.File, SaveFileDialog1.FileName)
                                CP_Original = CP
                            Else
                                If SaveFileDialog1.ShowDialog = DialogResult.OK Then
                                    CP.Save(CP.SavingMode.File, SaveFileDialog1.FileName)
                                    CP_Original = CP
                                Else
                                    Exit Sub
                                End If
                            End If
                        Case 1              '' Save As
                            If SaveFileDialog1.ShowDialog = DialogResult.OK Then
                                CP.Save(CP.SavingMode.File, SaveFileDialog1.FileName)
                                CP_Original = CP
                            Else
                                Exit Sub
                            End If
                    End Select

                    Select Case r2
                        Case 1      '' Apply   ' Case 0= Don't Apply
                            CP.Save(CP.SavingMode.Registry)
                            RestartExplorer()
                    End Select

                Case DialogResult.No

                Case DialogResult.Cancel
                    Exit Sub
            End Select

        End If

        CP = New CP(CP.Mode.Registry)
        CP_Original = CP
        OpenFileDialog1.FileName = Nothing
        SaveFileDialog1.FileName = Nothing
        ApplyCPValues(CP)
        ApplyLivePreviewFromCP(CP)
    End Sub

    Private Sub ToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem2.Click
        If Not CP.Equals(CP_Original) Then
            Select Case ComplexSave.ShowDialog
                Case DialogResult.Yes

                    Dim r As String() = My.Application.ComplexSaveResult.Split(".")
                    Dim r1 As String = r(0)
                    Dim r2 As String = r(1)

                    Select Case r1
                        Case 0              '' Save
                            If IO.File.Exists(SaveFileDialog1.FileName) Then
                                CP.Save(CP.SavingMode.File, SaveFileDialog1.FileName)
                                CP_Original = CP
                            Else
                                If SaveFileDialog1.ShowDialog = DialogResult.OK Then
                                    CP.Save(CP.SavingMode.File, SaveFileDialog1.FileName)
                                    CP_Original = CP
                                Else
                                    Exit Sub
                                End If
                            End If
                        Case 1              '' Save As
                            If SaveFileDialog1.ShowDialog = DialogResult.OK Then
                                CP.Save(CP.SavingMode.File, SaveFileDialog1.FileName)
                                CP_Original = CP
                            Else
                                Exit Sub
                            End If
                    End Select

                    Select Case r2
                        Case 1      '' Apply   ' Case 0= Don't Apply
                            CP.Save(CP.SavingMode.Registry)
                            RestartExplorer()
                    End Select

                Case DialogResult.No

                Case DialogResult.Cancel
                    Exit Sub
            End Select
        End If

        CP = New CP(CP.Mode.Init)
        CP_Original = CP
        OpenFileDialog1.FileName = Nothing
        SaveFileDialog1.FileName = Nothing
        ApplyCPValues(CP)
        ApplyLivePreviewFromCP(CP)
    End Sub

    Private Sub XenonButton10_Click(sender As Object, e As EventArgs) Handles XenonButton10.Click, author_lbl.DoubleClick, themename_lbl.DoubleClick
        EditInfo.Load_Info(CP)
        ApplyCPValues(CP)
    End Sub

    Private Sub XenonButton12_Click(sender As Object, e As EventArgs) Handles XenonButton12.Click
        About.ShowDialog()
    End Sub

    Private Sub XenonButton5_Click(sender As Object, e As EventArgs) Handles XenonButton5.Click
        Updates.ShowDialog()
    End Sub

    Private Sub XenonButton11_Click(sender As Object, e As EventArgs) Handles XenonButton11.Click
        SettingsX.ShowDialog()
    End Sub

    Private Sub XenonButton4_Click_1(sender As Object, e As EventArgs) Handles XenonButton4.Click
        Win32UI.Show()
    End Sub

    Private Sub XenonButton6_Click(sender As Object, e As EventArgs) Handles XenonButton6.Click
        Whatsnew.ShowDialog()
    End Sub

    Private Sub XenonButton16_Click(sender As Object, e As EventArgs) Handles XenonButton16.Click
        LogonUI.ShowDialog()
    End Sub

    Private Sub XenonButton15_Click(sender As Object, e As EventArgs) Handles XenonButton15.Click
        My.Application.Wallpaper = ResizeImage(My.Application.GetCurrentWallpaper(), 528, 297)
        pnl_preview.BackgroundImage = My.Application.Wallpaper
        ApplyLivePreviewFromCP(CP)
        ApplyCPValues(CP)
    End Sub

    Private Sub XenonButton13_Click(sender As Object, e As EventArgs) Handles XenonButton13.Click
        Me.Close()
    End Sub

    Private Sub XenonButton1_Click_1(sender As Object, e As EventArgs) Handles XenonButton1.Click
        If SaveFileDialog2.ShowDialog = DialogResult.OK Then
            GetControlImage(pnl_preview).Save(SaveFileDialog2.FileName)
        End If
    End Sub

    Private Sub XenonButton8_Click_1(sender As Object, e As EventArgs) Handles XenonButton8.Click
        MsgBox(My.Application.LanguageHelper.X23, MsgBoxStyle.Information + My.Application.MsgboxRt)
    End Sub

    Private Sub XenonButton17_Click(sender As Object, e As EventArgs) Handles XenonButton17.Click
        If Not CP.Equals(CP_Original) Then
            Select Case ComplexSave.ShowDialog
                Case DialogResult.Yes

                    Dim r As String() = My.Application.ComplexSaveResult.Split(".")
                    Dim r1 As String = r(0)
                    Dim r2 As String = r(1)

                    Select Case r1
                        Case 0              '' Save
                            If IO.File.Exists(SaveFileDialog1.FileName) Then
                                CP.Save(CP.SavingMode.File, SaveFileDialog1.FileName)
                                CP_Original = CP
                            Else
                                If SaveFileDialog1.ShowDialog = DialogResult.OK Then
                                    CP.Save(CP.SavingMode.File, SaveFileDialog1.FileName)
                                    CP_Original = CP
                                End If
                            End If
                        Case 1              '' Save As
                            If SaveFileDialog1.ShowDialog = DialogResult.OK Then
                                CP.Save(CP.SavingMode.File, SaveFileDialog1.FileName)
                                CP_Original = CP

                            End If
                    End Select

                    Select Case r2
                        Case 1      '' Apply   ' Case 0= Don't Apply
                            CP.Save(CP.SavingMode.Registry)
                            RestartExplorer()
                    End Select

                Case DialogResult.No

                Case DialogResult.Cancel
                    Exit Sub
            End Select
        End If

        CP = CP_Original
        ApplyCPValues(CP)
        ApplyLivePreviewFromCP(CP)
    End Sub

    Private Sub XenonButton18_Click(sender As Object, e As EventArgs) Handles XenonButton18.Click
        If Not CP.Equals(CP_Original) Then
            Select Case ComplexSave.ShowDialog
                Case DialogResult.Yes

                    Dim r As String() = My.Application.ComplexSaveResult.Split(".")
                    Dim r1 As String = r(0)
                    Dim r2 As String = r(1)

                    Select Case r1
                        Case 0              '' Save
                            If IO.File.Exists(SaveFileDialog1.FileName) Then
                                CP.Save(CP.SavingMode.File, SaveFileDialog1.FileName)
                                CP_Original = CP
                            Else
                                If SaveFileDialog1.ShowDialog = DialogResult.OK Then
                                    CP.Save(CP.SavingMode.File, SaveFileDialog1.FileName)
                                    CP_Original = CP
                                End If
                            End If
                        Case 1              '' Save As
                            If SaveFileDialog1.ShowDialog = DialogResult.OK Then
                                CP.Save(CP.SavingMode.File, SaveFileDialog1.FileName)
                                CP_Original = CP

                            End If
                    End Select

                    Select Case r2
                        Case 1      '' Apply   ' Case 0= Don't Apply
                            CP.Save(CP.SavingMode.Registry)
                            RestartExplorer()
                    End Select

                Case DialogResult.No

                Case DialogResult.Cancel
                    Exit Sub
            End Select
        End If

        CP = CP_FirstTime
        ApplyCPValues(CP)
        ApplyLivePreviewFromCP(CP)
    End Sub

    Private Sub XenonButton19_Click(sender As Object, e As EventArgs) Handles XenonButton19.Click
        RestartExplorer()
    End Sub

    Private Sub XenonButton20_Click(sender As Object, e As EventArgs) Handles XenonButton20.Click
        If Not CP.Equals(CP_Original) Then
            Select Case ComplexSave.ShowDialog
                Case DialogResult.Yes

                    Dim r As String() = My.Application.ComplexSaveResult.Split(".")
                    Dim r1 As String = r(0)
                    Dim r2 As String = r(1)

                    Select Case r1
                        Case 0              '' Save
                            If IO.File.Exists(SaveFileDialog1.FileName) Then
                                CP.Save(CP.SavingMode.File, SaveFileDialog1.FileName)
                                CP_Original = CP
                            Else
                                If SaveFileDialog1.ShowDialog = DialogResult.OK Then
                                    CP.Save(CP.SavingMode.File, SaveFileDialog1.FileName)
                                    CP_Original = CP
                                Else
                                    Exit Sub
                                End If
                            End If
                        Case 1              '' Save As
                            If SaveFileDialog1.ShowDialog = DialogResult.OK Then
                                CP.Save(CP.SavingMode.File, SaveFileDialog1.FileName)
                                CP_Original = CP
                            Else
                                Exit Sub
                            End If
                    End Select

                    Select Case r2
                        Case 1      '' Apply   ' Case 0= Don't Apply
                            CP.Save(CP.SavingMode.Registry)
                            RestartExplorer()
                    End Select

                Case DialogResult.No

                Case DialogResult.Cancel
                    Exit Sub
            End Select
        End If



        If My.W11 Then IO.File.WriteAllText("temp.wpth", My.Resources.W11_Init) Else IO.File.WriteAllText("temp.wpth", My.Resources.W10_Init)
        CP = New CP(CP.Mode.File, "temp.wpth")
        Kill("temp.wpth")
        CP_Original = CP
        OpenFileDialog1.FileName = Nothing
        SaveFileDialog1.FileName = Nothing
        ApplyCPValues(CP)
        ApplyLivePreviewFromCP(CP)
    End Sub

    Private Sub XenonButton21_Click(sender As Object, e As EventArgs) Handles XenonButton21.Click
        CursorsStudio.ShowDialog()
    End Sub

    Private Sub Aero_ColorizationColor_pick_Click(sender As Object, e As EventArgs) Handles Aero_ColorizationColor_pick.Click
        Dim CList As New List(Of Control) From {sender}

        Dim C As Color = ColorPickerDlg.Pick(CList)

        CP.Aero_ColorizationColor = Color.FromArgb(255, C)
        ApplyLivePreviewFromCP(CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()
    End Sub

    Private Sub Aero_ColorizationAfterglow_pick_Click(sender As Object, e As EventArgs) Handles Aero_ColorizationAfterglow_pick.Click
        Dim CList As New List(Of Control) From {sender}

        Dim C As Color = ColorPickerDlg.Pick(CList)

        CP.Aero_ColorizationAfterglow = Color.FromArgb(255, C)
        ApplyLivePreviewFromCP(CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()
    End Sub

    Private Sub Aero_Composition_toggle_CheckedChanged(sender As Object, e As EventArgs) Handles Aero_Composition_toggle.CheckedChanged
        If _Shown Then CP.Aero_Composition = Aero_Composition_toggle.Checked
    End Sub

    Private Sub Aero_EnableAeroPeek_toggle_CheckedChanged(sender As Object, e As EventArgs) Handles Aero_EnableAeroPeek_toggle.CheckedChanged
        If _Shown Then CP.Aero_EnableAeroPeek = Aero_EnableAeroPeek_toggle.Checked
    End Sub

    Private Sub Aero_AlwaysHibernateThumbnails_Toggle_CheckedChanged(sender As Object, e As EventArgs) Handles Aero_AlwaysHibernateThumbnails_Toggle.CheckedChanged
        If _Shown Then CP.Aero_AlwaysHibernateThumbnails = Aero_AlwaysHibernateThumbnails_Toggle.Checked
    End Sub

    Private Sub Aero_EnableWindowColorization_toggle_CheckedChanged(sender As Object, e As EventArgs) Handles Aero_EnableWindowColorization_toggle.CheckedChanged
        If _Shown Then CP.Aero_EnableWindowColorization = Aero_EnableWindowColorization_toggle.Checked
    End Sub

    Private Sub Aero_ColorizationOpaqueBlend_txt_TextChanged(sender As Object, e As EventArgs) Handles Aero_ColorizationOpaqueBlend_txt.TextChanged
        If _Shown Then CP.Aero_ColorizationOpaqueBlend = Val(Aero_ColorizationOpaqueBlend_txt.Text)
    End Sub

    Private Sub Aero_CompositionPolicy_txt_TextChanged(sender As Object, e As EventArgs) Handles Aero_CompositionPolicy_txt.TextChanged
        If _Shown Then CP.Aero_CompositionPolicy = Val(Aero_CompositionPolicy_txt.Text)
    End Sub

    Private Sub Aero_ColorizationColorBalance_txt_TextChanged(sender As Object, e As EventArgs) Handles Aero_ColorizationColorBalance_txt.TextChanged
        If _Shown Then CP.Aero_ColorizationColorBalance = Val(Aero_ColorizationColorBalance_txt.Text)
    End Sub

    Private Sub Aero_ColorizationAfterglowBalance_txt_TextChanged(sender As Object, e As EventArgs) Handles Aero_ColorizationAfterglowBalance_txt.TextChanged
        If _Shown Then CP.Aero_ColorizationAfterglowBalance = Val(Aero_ColorizationAfterglowBalance_txt.Text)
    End Sub

    Private Sub Aero_ColorizationBlurBalance_TextChanged(sender As Object, e As EventArgs) Handles Aero_ColorizationBlurBalance_txt.TextChanged
        If _Shown Then CP.Aero_ColorizationBlurBalance = Val(Aero_ColorizationBlurBalance_txt.Text)
    End Sub

    Private Sub Aero_ColorizationGlassReflectionIntensity_txt_TextChanged(sender As Object, e As EventArgs) Handles Aero_ColorizationGlassReflectionIntensity_txt.TextChanged
        If _Shown Then CP.Aero_ColorizationGlassReflectionIntensity = Val(Aero_ColorizationGlassReflectionIntensity_txt.Text)
    End Sub

    Private Sub Aero_LastDisqualifiedCompositionSignature_txt_TextChanged(sender As Object, e As EventArgs) Handles Aero_LastDisqualifiedCompositionSignature_txt.TextChanged
        If _Shown Then CP.Aero_LastDisqualifiedCompositionSignature = Val(Aero_LastDisqualifiedCompositionSignature_txt.Text)
    End Sub

#Region "Notifications Base"
    Sub Notify([Text] As String, [Icon] As Image, [Interval] As Integer)
        Dim NB As New XenonAlertBox With {
         .AlertStyle = XenonAlertBox.Style.Adaptive,
         .Text = Text,
         .Image = [Icon],
         .Size = New Size(NotificationsPanel.Width - 5, MeasureString([Text], .Font).Height + 15),
         .Left = 0
         }

        AddHandler NB.Click, AddressOf NB.Hide

        NotificationsList.Add(New Notifier(NB, [Interval]))
        NotificationsPanel.Controls.Add(NB)
        NotificationsPanel.BringToFront()
        NTimer.Enabled = True
        NTimer.Start()
    End Sub
    Private Sub NTimer_Tick(sender As Object, e As EventArgs) Handles NTimer.Tick

        Try
            For Each nx As Notifier In NotificationsList
                If nx.Interval - NTimer.Interval <= 0 Then
                    NotificationsPanel.Controls.Remove(nx.Control)
                    nx.Control.Dispose()
                    RemoveHandler nx.Control.Click, AddressOf nx.Control.Hide
                    NotificationsList.Remove(nx)
                Else
                    nx.Interval -= NTimer.Interval
                End If
            Next
        Catch
        End Try

        If NotificationsList.Count = 0 Then
            NTimer.Enabled = False
            NTimer.Stop()
        End If

    End Sub

    ReadOnly NotificationsList As New List(Of Notifier)
    Dim WithEvents NTimer As New Timer With {.Enabled = False, .Interval = 100}

#End Region

End Class

Public Class Notifier
    Public Sub New(Control As XenonAlertBox, Interval As Integer)
        Me.Control = Control
        Me.Interval = Interval
    End Sub
    Public Property Control As XenonAlertBox
    Public Property Interval As Integer
End Class
