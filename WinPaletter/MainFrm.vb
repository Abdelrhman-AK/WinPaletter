Imports System.ComponentModel
Imports System.Net
Imports System.Text
Imports Devcorp.Controls.VisualStyles
Imports WinPaletter.CP
Imports WinPaletter.NativeMethods
Imports WinPaletter.PreviewHelpers
Imports WinPaletter.XenonCore

Public Class MainFrm
    Private _Shown As Boolean = False
    Dim RaiseUpdate As Boolean = False
    Dim ver As String = ""
    Dim StableInt, BetaInt, UpdateChannel As Integer
    Dim ChannelFixer As Integer
    Dim Updates_ls As New List(Of String)
    Private LoggingOff As Boolean = False
    Private ReadOnly _Converter As New Converter

#Region "Preview Subs"
    Sub ApplyColorsToElements(ByVal [CP] As CP)
        ApplyWinElementsColors([CP], My.PreviewStyle, True, taskbar, start, ActionCenter, setting_icon_preview, Label8, lnk_preview)
        ApplyWindowStyles([CP], My.PreviewStyle, XenonWindow1, XenonWindow2, W8_start, W8_logonui)
    End Sub
    Sub ApplyStylesToElements(ByVal [CP] As CP, Optional AnimateThePreview As Boolean = True)
        Dim ItWasVisible As Boolean = tabs_preview.Visible

        If AnimateThePreview And ItWasVisible Then
            If _Shown Then
                If tabs_preview.Visible Then My.Animator.HideSync(tabs_preview)
            Else
                tabs_preview.Visible = False
            End If
        End If

        ApplyWinElementsStyle([CP], My.PreviewStyle, taskbar, start, ActionCenter,
                           XenonWindow1, XenonWindow2, Panel3, lnk_preview,
                           ClassicTaskbar, RetroButton2, RetroButton3, RetroButton4, ClassicWindow1, ClassicWindow2,
                           WXP_VS_ReplaceColors.Checked, WXP_VS_ReplaceMetrics.Checked, WXP_VS_ReplaceFonts.Checked)

        XenonButton23.Visible = (My.PreviewStyle = WindowStyle.W7)

        pnl_preview.BackgroundImage = My.Application.FetchSuitableWallpaper([CP], My.PreviewStyle)
        pnl_preview_classic.BackgroundImage = pnl_preview.BackgroundImage

        AdjustPreview_ModernOrClassic([CP], My.PreviewStyle, tabs_preview, WXP_Alert2)

        'ReValidateLivePreview(tabs_preview)

        If AnimateThePreview And ItWasVisible Then
            If _Shown Then
                My.Animator.ShowSync(tabs_preview)
            Else
                tabs_preview.Visible = True
            End If
        End If
    End Sub
    Sub ApplyCPValues([CP] As CP)
        themename_lbl.Text = String.Format("{0} ({1})", [CP].Info.ThemeName, [CP].Info.ThemeVersion)
        author_lbl.Text = String.Format("{0} {1}", My.Lang.By, [CP].Info.Author)

        With My.Settings.Appearance
            .CustomColors = [CP].AppTheme.Enabled
            .BackColor = [CP].AppTheme.BackColor
            .AccentColor = [CP].AppTheme.AccentColor
            .CustomTheme = [CP].AppTheme.DarkMode
            .RoundedCorners = [CP].AppTheme.RoundCorners
        End With
        ApplyDarkMode(Me)

        W11_WinMode_Toggle.Checked = Not [CP].Windows11.WinMode_Light
        W11_AppMode_Toggle.Checked = Not [CP].Windows11.AppMode_Light
        W11_Transparency_Toggle.Checked = [CP].Windows11.Transparency
        W11_ShowAccentOnTitlebarAndBorders_Toggle.Checked = [CP].Windows11.ApplyAccentOnTitlebars
        Select Case [CP].Windows11.ApplyAccentOnTaskbar
            Case CP.Structures.Windows10x.AccentTaskbarLevels.None
                W11_Accent_None.Checked = True

            Case CP.Structures.Windows10x.AccentTaskbarLevels.Taskbar_Start_AC
                W11_Accent_StartTaskbar.Checked = True

            Case CP.Structures.Windows10x.AccentTaskbarLevels.Taskbar
                W11_Accent_Taskbar.Checked = True

        End Select
        W11_ActiveTitlebar_pick.BackColor = [CP].Windows11.Titlebar_Active
        W11_InactiveTitlebar_pick.BackColor = [CP].Windows11.Titlebar_Inactive
        W11_Color_Index5.BackColor = [CP].Windows11.StartMenu_Accent
        W11_Color_Index4.BackColor = [CP].Windows11.Color_Index2
        W11_Color_Index6.BackColor = [CP].Windows11.Color_Index6
        W11_Color_Index1.BackColor = [CP].Windows11.Color_Index1
        W11_Color_Index2.BackColor = [CP].Windows11.Color_Index4
        W11_TaskbarFrontAndFoldersOnStart_pick.BackColor = [CP].Windows11.Color_Index5
        W11_Color_Index0.BackColor = [CP].Windows11.Color_Index0
        W11_Color_Index3.BackColor = [CP].Windows11.Color_Index3
        W11_Color_Index7.BackColor = [CP].Windows11.Color_Index7

        W10_WinMode_Toggle.Checked = Not [CP].Windows10.WinMode_Light
        W10_AppMode_Toggle.Checked = Not [CP].Windows10.AppMode_Light
        W10_Transparency_Toggle.Checked = [CP].Windows10.Transparency
        W10_TBTransparency_Toggle.Checked = [CP].Windows10.IncreaseTBTransparency
        W10_TB_Blur.Checked = [CP].Windows10.TB_Blur
        W10_ShowAccentOnTitlebarAndBorders_Toggle.Checked = [CP].Windows10.ApplyAccentOnTitlebars
        Select Case [CP].Windows10.ApplyAccentOnTaskbar
            Case CP.Structures.Windows10x.AccentTaskbarLevels.None
                W10_Accent_None.Checked = True

            Case CP.Structures.Windows10x.AccentTaskbarLevels.Taskbar_Start_AC
                W10_Accent_StartTaskbar.Checked = True

            Case CP.Structures.Windows10x.AccentTaskbarLevels.Taskbar
                W10_Accent_Taskbar.Checked = True
        End Select
        W10_ActiveTitlebar_pick.BackColor = [CP].Windows10.Titlebar_Active
        W10_InactiveTitlebar_pick.BackColor = [CP].Windows10.Titlebar_Inactive
        W10_Color_Index5.BackColor = [CP].Windows10.StartMenu_Accent
        W10_Color_Index4.BackColor = [CP].Windows10.Color_Index2
        W10_Color_Index6.BackColor = [CP].Windows10.Color_Index6
        W10_Color_Index1.BackColor = [CP].Windows10.Color_Index1
        W10_Color_Index2.BackColor = [CP].Windows10.Color_Index4
        W10_TaskbarFrontAndFoldersOnStart_pick.BackColor = [CP].Windows10.Color_Index5
        W10_Color_Index0.BackColor = [CP].Windows10.Color_Index0
        W10_Color_Index3.BackColor = [CP].Windows10.Color_Index3
        W10_Color_Index7.BackColor = [CP].Windows10.Color_Index7

        Select Case [CP].Windows8.Theme
            Case CP.Structures.Windows7.Themes.Aero
                W8_theme_aero.Checked = True

            Case CP.Structures.Windows7.Themes.AeroLite
                W8_theme_aerolite.Checked = True
        End Select
        W8_ColorizationColor_pick.BackColor = [CP].Windows8.ColorizationColor
        W8_ColorizationBalance_bar.Value = [CP].Windows8.ColorizationColorBalance
        W8_ColorizationBalance_val.Text = [CP].Windows8.ColorizationColorBalance
        W8_start_pick.BackColor = [CP].Windows8.StartColor
        W8_accent_pick.BackColor = [CP].Windows8.AccentColor
        W8_personalcls_background_pick.BackColor = [CP].Windows8.PersonalColors_Background
        W8_personalcolor_accent_pick.BackColor = [CP].Windows8.PersonalColors_Accent

        W7_ColorizationColor_pick.BackColor = [CP].Windows7.ColorizationColor
        W7_ColorizationAfterglow_pick.BackColor = [CP].Windows7.ColorizationAfterglow
        W7_ColorizationColorBalance_bar.Value = [CP].Windows7.ColorizationColorBalance
        W7_ColorizationAfterglowBalance_bar.Value = [CP].Windows7.ColorizationAfterglowBalance
        W7_ColorizationBlurBalance_bar.Value = [CP].Windows7.ColorizationBlurBalance
        W7_ColorizationGlassReflectionIntensity_bar.Value = [CP].Windows7.ColorizationGlassReflectionIntensity
        W7_ColorizationColorBalance_val.Text = [CP].Windows7.ColorizationColorBalance
        W7_ColorizationAfterglowBalance_val.Text = [CP].Windows7.ColorizationAfterglowBalance
        W7_ColorizationBlurBalance_val.Text = [CP].Windows7.ColorizationBlurBalance
        W7_ColorizationGlassReflectionIntensity_val.Text = [CP].Windows7.ColorizationGlassReflectionIntensity
        W7_EnableAeroPeek_toggle.Checked = [CP].Windows7.EnableAeroPeek
        W7_AlwaysHibernateThumbnails_Toggle.Checked = [CP].Windows7.AlwaysHibernateThumbnails
        Select Case [CP].Windows7.Theme
            Case CP.Structures.Windows7.Themes.Aero
                W7_theme_aero.Checked = True

            Case CP.Structures.Windows7.Themes.AeroOpaque
                W7_theme_aeroopaque.Checked = True

            Case CP.Structures.Windows7.Themes.Basic
                W7_theme_basic.Checked = True

            Case CP.Structures.Windows7.Themes.Classic
                W7_theme_classic.Checked = True
        End Select

        WVista_ColorizationColor_pick.BackColor = [CP].WindowsVista.ColorizationColor
        WVista_ColorizationColorBalance_bar.Value = [CP].WindowsVista.Alpha
        WVista_ColorizationColorBalance_val.Text = [CP].WindowsVista.Alpha
        Select Case [CP].WindowsVista.Theme
            Case CP.Structures.Windows7.Themes.Aero
                WVista_theme_aero.Checked = True

            Case CP.Structures.Windows7.Themes.AeroOpaque
                WVista_theme_aeroopaque.Checked = True

            Case CP.Structures.Windows7.Themes.Basic
                WVista_theme_basic.Checked = True

            Case CP.Structures.Windows7.Themes.Classic
                WVista_theme_classic.Checked = True
        End Select

        Select Case [CP].WindowsXP.Theme
            Case CP.Structures.WindowsXP.Themes.LunaBlue
                WXP_Luna_Blue.Checked = True

            Case CP.Structures.WindowsXP.Themes.LunaOliveGreen
                WXP_Luna_OliveGreen.Checked = True

            Case CP.Structures.WindowsXP.Themes.LunaSilver
                WXP_Luna_Silver.Checked = True

            Case CP.Structures.WindowsXP.Themes.Custom
                WXP_CustomTheme.Checked = True

            Case CP.Structures.WindowsXP.Themes.Classic
                WXP_Classic.Checked = True

        End Select
        WXP_VS_textbox.Text = [CP].WindowsXP.ThemeFile
        If WXP_VS_ColorsList.Items.Contains([CP].WindowsXP.ColorScheme) Then WXP_VS_ColorsList.SelectedItem = [CP].WindowsXP.ColorScheme

        ApplyMetroStartToButton([CP], W8_start)
        ApplyBackLogonUI([CP], W8_logonui)
    End Sub
    Sub ApplyDefaultCPValues()
        Dim DefCP As CP

        If My.W11 Then
            DefCP = New CP_Defaults().Default_Windows11
        ElseIf My.W10 Then
            DefCP = New CP_Defaults().Default_Windows10
        ElseIf My.W8 Then
            DefCP = New CP_Defaults().Default_Windows8
        ElseIf My.W7 Then
            DefCP = New CP_Defaults().Default_Windows7
        ElseIf My.WVista Then
            DefCP = New CP_Defaults().Default_WindowsVista
        ElseIf My.WXP Then
            DefCP = New CP_Defaults().Default_WindowsXP
        Else
            DefCP = New CP_Defaults().Default_Windows11
        End If

        W11_ActiveTitlebar_pick.DefaultColor = DefCP.Windows11.Titlebar_Active
        W11_InactiveTitlebar_pick.DefaultColor = DefCP.Windows11.Titlebar_Inactive
        W11_Color_Index5.DefaultColor = DefCP.Windows11.StartMenu_Accent
        W11_Color_Index4.DefaultColor = DefCP.Windows11.Color_Index2
        W11_Color_Index6.DefaultColor = DefCP.Windows11.Color_Index6
        W11_Color_Index1.DefaultColor = DefCP.Windows11.Color_Index1
        W11_Color_Index2.DefaultColor = DefCP.Windows11.Color_Index4
        W11_TaskbarFrontAndFoldersOnStart_pick.DefaultColor = DefCP.Windows11.Color_Index5
        W11_Color_Index0.DefaultColor = DefCP.Windows11.Color_Index0
        W11_Color_Index3.DefaultColor = DefCP.Windows11.Color_Index3
        W11_Color_Index7.DefaultColor = DefCP.Windows11.Color_Index7

        W10_ActiveTitlebar_pick.DefaultColor = DefCP.Windows10.Titlebar_Active
        W10_InactiveTitlebar_pick.DefaultColor = DefCP.Windows10.Titlebar_Inactive
        W10_Color_Index5.DefaultColor = DefCP.Windows10.StartMenu_Accent
        W10_Color_Index4.DefaultColor = DefCP.Windows10.Color_Index2
        W10_Color_Index6.DefaultColor = DefCP.Windows10.Color_Index6
        W10_Color_Index1.DefaultColor = DefCP.Windows10.Color_Index1
        W10_Color_Index2.DefaultColor = DefCP.Windows10.Color_Index4
        W10_TaskbarFrontAndFoldersOnStart_pick.DefaultColor = DefCP.Windows10.Color_Index5
        W10_Color_Index0.DefaultColor = DefCP.Windows10.Color_Index0
        W10_Color_Index3.DefaultColor = DefCP.Windows10.Color_Index3
        W10_Color_Index7.DefaultColor = DefCP.Windows10.Color_Index7

        W8_ColorizationColor_pick.DefaultColor = DefCP.Windows7.ColorizationColor
        W8_start_pick.DefaultColor = DefCP.Windows8.StartColor
        W8_accent_pick.DefaultColor = DefCP.Windows8.AccentColor
        W8_personalcls_background_pick.DefaultColor = DefCP.Windows8.PersonalColors_Background
        W8_personalcolor_accent_pick.DefaultColor = DefCP.Windows8.PersonalColors_Accent

        W7_ColorizationColor_pick.DefaultColor = DefCP.Windows7.ColorizationColor
        W7_ColorizationAfterglow_pick.DefaultColor = DefCP.Windows7.ColorizationAfterglow

        WVista_ColorizationColor_pick.DefaultColor = DefCP.WindowsVista.ColorizationColor

        DefCP.Dispose()
    End Sub
    Public Sub Update_Wallpaper_Preview()
        Cursor = Cursors.AppStarting
        pnl_preview.BackgroundImage = My.Application.FetchSuitableWallpaper(My.CP, My.PreviewStyle)
        pnl_preview_classic.BackgroundImage = pnl_preview.BackgroundImage
        ApplyColorsToElements(My.CP)
        ApplyCPValues(My.CP)
        ApplyStylesToElements(My.CP, False)
        ReValidateLivePreview(pnl_preview)
        ReValidateLivePreview(pnl_preview_classic)
        Cursor = Cursors.Default
    End Sub
    Sub SelectLeftPanelIndex()
        If My.PreviewStyle = WindowStyle.W11 Then
            TablessControl1.SelectedIndex = 0
        ElseIf My.PreviewStyle = WindowStyle.W10 Then
            TablessControl1.SelectedIndex = 1
        ElseIf My.PreviewStyle = WindowStyle.W8 Then
            TablessControl1.SelectedIndex = 2
        ElseIf My.PreviewStyle = WindowStyle.W7 Then
            TablessControl1.SelectedIndex = 3
        ElseIf My.PreviewStyle = WindowStyle.WVista Then
            TablessControl1.SelectedIndex = 4
        ElseIf My.PreviewStyle = WindowStyle.WXP Then
            TablessControl1.SelectedIndex = 5
        Else
            TablessControl1.SelectedIndex = 0
        End If
    End Sub
    Sub UpdateLegends()
        If My.PreviewStyle = WindowStyle.W11 Then
            ApplyWin10xLegends(My.CP, My.PreviewStyle,
                    W11_lbl1, W11_lbl2, W11_lbl3, W11_lbl4, W11_lbl5, W11_lbl6, W11_lbl7, W11_lbl8, W11_lbl9,
                    W11_pic1, W11_pic2, W11_pic3, W11_pic4, W11_pic5, W11_pic6, W11_pic7, W11_pic8, W11_pic9)

        ElseIf My.PreviewStyle = WindowStyle.W10 Then
            ApplyWin10xLegends(My.CP, My.PreviewStyle,
                    W10_lbl1, W10_lbl2, W10_lbl3, W10_lbl4, W10_lbl5, W10_lbl6, W10_lbl7, W10_lbl8, W10_lbl9,
                    W10_pic1, W10_pic2, W10_pic3, W10_pic4, W10_pic5, W10_pic6, W10_pic7, W10_pic8, W10_pic9)

        End If
    End Sub
#End Region

#Region "Misc"

    Sub UpdateHint(Sender As Object, e As EventArgs)
        status_lbl.Text = Sender.Tag
    End Sub

    Sub EraseHint()
        status_lbl.Text = ""
    End Sub

    Sub UpdateHint_Dashboard(Sender As Object, e As EventArgs)
        status_lbl.Text = Sender.Tag
    End Sub

    Sub EraseHint_Dashboard()
        status_lbl.Text = ""
    End Sub

#End Region

    Sub AutoUpdatesCheck()
        If My.WXP OrElse My.WVista Then Exit Sub

        StableInt = 0 : BetaInt = 0 : UpdateChannel = 0 : ChannelFixer = 0
        If My.Settings.Updates.Channel = XeSettings.Structures.Updates.Channels.Stable Then ChannelFixer = 0
        If My.Settings.Updates.Channel = XeSettings.Structures.Updates.Channels.Beta Then ChannelFixer = 1
        BackgroundWorker1.RunWorkerAsync()
    End Sub

    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        If IsNetworkAvailable() Then
            Try
                Dim WebCL As New WebClient
                RaiseUpdate = False
                ver = ""

                Updates_ls = WebCL.DownloadString(My.Resources.Link_Updates).CList

                For x = 0 To Updates_ls.Count - 1
                    If Not String.IsNullOrEmpty(Updates_ls(x)) And Not Updates_ls(x).IndexOf("#") = 0 Then
                        If Updates_ls(x).Split(" ")(0) = "Stable" Then StableInt = x
                        If Updates_ls(x).Split(" ")(0) = "Beta" Then BetaInt = x
                    End If
                Next

                If ChannelFixer = 0 Then UpdateChannel = StableInt
                If ChannelFixer = 1 Then UpdateChannel = BetaInt

                ver = Updates_ls(UpdateChannel).Split(" ")(1)

                RaiseUpdate = (ver > My.AppVersion)
            Catch
            End Try
        End If
    End Sub

    Private Sub BackgroundWorker1_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        If RaiseUpdate Then
            Updates.ls = Updates_ls
            NotifyUpdates.Visible = True
            XenonButton5.Image = My.Resources.Update_Dot
            NotifyUpdates.ShowBalloonTip(10000, My.Application.Info.Title, String.Format("{0}. {1} {2}", My.Lang.NewUpdate, My.Lang.Version, ver), ToolTipIcon.Info)
        End If
    End Sub

    Private Sub NotifyIcon1_BalloonTipClicked(sender As Object, e As EventArgs) Handles NotifyUpdates.BalloonTipClicked
        NotifyUpdates.Visible = False

        If My.Application.OpenForms(Updates.Name) Is Nothing Then
            Updates.ShowDialog()
        Else
            Updates.Focus()
        End If
    End Sub

    Private Sub MainFrm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _Shown = False
        Visible = False
        LoggingOff = False

        NotifyUpdates.Icon = Icon
        TreeView1.ImageList = My.Notifications_IL

        Me.Size = New Size(My.Settings.General.MainFormWidth, My.Settings.General.MainFormHeight)
        Me.WindowState = My.Settings.General.MainFormStatus

        Select_W11.Image = My.Resources.Native11
        Select_W10.Image = My.Resources.Native10
        Select_W8.Image = My.Resources.Native8
        Select_W7.Image = My.Resources.Native7
        Select_WVista.Image = My.Resources.NativeVista
        Select_WXP.Image = My.Resources.NativeXP
        If Not My.isElevated Then apply_btn.Image = My.Resources.WP_Admin

        If My.PreviewStyle = WindowStyle.W11 Then
            TablessControl1.SelectedIndex = 0
            XenonButton20.Image = My.Resources.add_win11
            Select_W11.Checked = True

        ElseIf My.PreviewStyle = WindowStyle.W10 Then
            TablessControl1.SelectedIndex = 1
            XenonButton20.Image = My.Resources.add_win10
            Select_W10.Checked = True

        ElseIf My.PreviewStyle = WindowStyle.W8 Then
            TablessControl1.SelectedIndex = 2
            XenonButton20.Image = My.Resources.add_win8
            Select_W8.Checked = True

        ElseIf My.PreviewStyle = WindowStyle.W7 Then
            TablessControl1.SelectedIndex = 3
            XenonButton20.Image = My.Resources.add_win7
            Select_W7.Checked = True

        ElseIf My.PreviewStyle = WindowStyle.WVista Then
            TablessControl1.SelectedIndex = 4
            XenonButton20.Image = My.Resources.add_winvista
            Select_WVista.Checked = True

        ElseIf My.PreviewStyle = WindowStyle.WXP Then
            TablessControl1.SelectedIndex = 5
            XenonButton20.Image = My.Resources.add_winxp
            Select_WXP.Checked = True

        Else
            TablessControl1.SelectedIndex = 0
            XenonButton20.Image = My.Resources.add_win11
            Select_W11.Checked = True
        End If

        ApplyDarkMode(Me)
        DoubleBuffer
        UpdateLegends()
        ApplyColorsToElements(My.CP)
        ApplyStylesToElements(My.CP)
        ApplyCPValues(My.CP)
        ApplyDefaultCPValues()

        WXP_Alert2.Size = WXP_Alert2.Parent.Size - New Size(40, 40)
        WXP_Alert2.Location = New Point(20, 20)

        Visible = True

        BetaBadge.Visible = My.IsBeta
        If My.IsBeta Then
            status_lbl.Width = BetaBadge.Left - 5 - status_lbl.Left
        Else
            status_lbl.Width = BetaBadge.Right - status_lbl.Left
        End If
    End Sub

    Private Sub MainFrm_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        _Shown = True

        For Each btn As XenonButton In MainToolbar.Controls.OfType(Of XenonButton)
            AddHandler btn.MouseEnter, AddressOf UpdateHint
            AddHandler btn.Enter, AddressOf UpdateHint
            AddHandler btn.MouseLeave, AddressOf EraseHint
            AddHandler btn.Leave, AddressOf EraseHint
        Next

        For Each btn As XenonButton In XenonGroupBox3.Controls.OfType(Of XenonButton)
            AddHandler btn.MouseEnter, AddressOf UpdateHint_Dashboard
            AddHandler btn.Enter, AddressOf UpdateHint_Dashboard
            AddHandler btn.MouseLeave, AddressOf EraseHint_Dashboard
            AddHandler btn.Leave, AddressOf EraseHint_Dashboard
        Next

        For Each btn As XenonRadioImage In previewContainer.Controls.OfType(Of XenonRadioImage)
            AddHandler btn.MouseEnter, AddressOf UpdateHint
            AddHandler btn.Enter, AddressOf UpdateHint
            AddHandler btn.MouseLeave, AddressOf EraseHint
            AddHandler btn.Leave, AddressOf EraseHint
        Next

        If My.Settings.Updates.AutoCheck Then AutoUpdatesCheck()

        If My.Application.ShowWhatsNew Then Whatsnew.ShowDialog()
    End Sub

    Private Sub MainFrm_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        If Me.WindowState = FormWindowState.Normal Then
            My.Settings.General.MainFormWidth = Me.Size.Width
            My.Settings.General.MainFormHeight = Me.Size.Height
        End If
        If Me.WindowState <> FormWindowState.Minimized Then
            My.Settings.General.MainFormStatus = Me.WindowState
        End If
        My.Settings.General.Save()

        Dim old As New XeSettings(XeSettings.Mode.Registry)
        With My.Settings.Appearance
            .CustomColors = old.Appearance.CustomColors
            .BackColor = old.Appearance.BackColor
            .AccentColor = old.Appearance.AccentColor
            .CustomTheme = old.Appearance.CustomTheme
            .RoundedCorners = old.Appearance.RoundedCorners
            .Save()
        End With
    End Sub

    Protected Overrides Sub OnFormClosing(ByVal e As FormClosingEventArgs)
        If My.CP <> My.CP_Original Then

            If My.Settings.ThemeApplyingBehavior.ShowSaveConfirmation AndAlso Not LoggingOff Then

                Select Case ComplexSave.ShowDialog
                    Case DialogResult.Yes

                        Dim r As String() = My.Settings.General.ComplexSaveResult.Split(".")
                        Dim r1 As String = r(0)
                        Dim r2 As String = r(1)

                        Select Case r1
                            Case 0              '' Save
                                If IO.File.Exists(SaveFileDialog1.FileName) Then
                                    My.CP.Save(CP.CP_Type.File, SaveFileDialog1.FileName)
                                    My.CP_Original = My.CP.Clone
                                Else
                                    If SaveFileDialog1.ShowDialog = DialogResult.OK Then
                                        My.CP.Save(CP.CP_Type.File, SaveFileDialog1.FileName)
                                        My.CP_Original = My.CP.Clone
                                    Else
                                        e.Cancel = True
                                    End If
                                End If
                            Case 1              '' Save As
                                If SaveFileDialog1.ShowDialog = DialogResult.OK Then
                                    My.CP.Save(CP.CP_Type.File, SaveFileDialog1.FileName)
                                    My.CP_Original = My.CP.Clone
                                Else
                                    e.Cancel = True
                                End If
                        End Select

                        Select Case r2
                            Case 1
                                Apply_Theme()

                            Case 2
                                Apply_Theme(My.CP_FirstTime)

                            Case 3
                                Apply_Theme(CP_Defaults.GetDefault)

                        End Select

                    Case DialogResult.No
                        e.Cancel = False
                        If (My.W7 Or My.W8) And My.Settings.Miscellaneous.Win7LivePreview Then RefreshDWM(My.CP_Original)
                        MyBase.OnFormClosing(e)

                    Case DialogResult.Cancel
                        e.Cancel = True
                End Select
            Else
                e.Cancel = False
                MyBase.OnFormClosing(e)
            End If
        Else
            e.Cancel = False
            MyBase.OnFormClosing(e)
        End If
    End Sub


#Region "Windows 11"
    Private Sub W11_ActiveTitlebar_pick_Click(sender As Object, e As EventArgs) Handles W11_ActiveTitlebar_pick.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                My.CP.Windows11.Titlebar_Active = sender.BackColor
                If My.PreviewStyle = WindowStyle.W11 Then ApplyColorsToElements(My.CP)
            End If
            Exit Sub
        End If

        Dim CList As New List(Of Control) From {sender, XenonWindow1}

        Dim C As Color = ColorPickerDlg.Pick(CList)
        My.CP.Windows11.Titlebar_Active = Color.FromArgb(255, C)
        If My.PreviewStyle = WindowStyle.W11 Then ApplyColorsToElements(My.CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()

    End Sub

    Private Sub W11_InactiveTitlebar_pick_Click(sender As Object, e As EventArgs) Handles W11_InactiveTitlebar_pick.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                My.CP.Windows11.Titlebar_Inactive = sender.BackColor
                If My.PreviewStyle = WindowStyle.W11 Then ApplyColorsToElements(My.CP)
            End If
            Exit Sub
        End If

        Dim CList As New List(Of Control) From {sender, XenonWindow2}

        Dim _Conditions As New Conditions With {.Window_InactiveTitlebar = True}
        Dim C As Color = ColorPickerDlg.Pick(CList, _Conditions)

        My.CP.Windows11.Titlebar_Inactive = Color.FromArgb(255, C)

        If My.PreviewStyle = WindowStyle.W11 Then ApplyColorsToElements(My.CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()
    End Sub

    Private Sub W11_WinMode_Toggle_CheckedChanged(sender As Object, e As EventArgs) Handles W11_WinMode_Toggle.CheckedChanged
        If _Shown Then
            My.CP.Windows11.WinMode_Light = Not sender.Checked
            If My.PreviewStyle = WindowStyle.W11 Then
                UpdateLegends()
                ApplyColorsToElements(My.CP)
            End If
        End If
    End Sub

    Private Sub W11_AppMode_Toggle_CheckedChanged(sender As Object, e As EventArgs) Handles W11_AppMode_Toggle.CheckedChanged
        If _Shown Then
            My.CP.Windows11.AppMode_Light = Not sender.Checked
            If My.PreviewStyle = WindowStyle.W11 Then ApplyColorsToElements(My.CP)
        End If
    End Sub

    Private Sub W11_Transparency_Toggle_CheckedChanged(sender As Object, e As EventArgs) Handles W11_Transparency_Toggle.CheckedChanged
        If _Shown Then
            My.CP.Windows11.Transparency = sender.Checked
            If My.PreviewStyle = WindowStyle.W11 Then
                UpdateLegends()
                ApplyColorsToElements(My.CP)
            End If
        End If
    End Sub

    Private Sub W11_ShowAccentOnTitlebarAndBorders_Toggle_CheckedChanged(sender As Object, e As EventArgs) Handles W11_ShowAccentOnTitlebarAndBorders_Toggle.CheckedChanged
        If _Shown Then
            My.CP.Windows11.ApplyAccentOnTitlebars = sender.Checked
            If My.PreviewStyle = WindowStyle.W11 Then ApplyColorsToElements(My.CP)
        End If
    End Sub

    Private Sub W11_Accent_None_CheckedChanged(sender As Object) Handles W11_Accent_None.CheckedChanged
        If _Shown And sender.Checked Then
            My.CP.Windows11.ApplyAccentOnTaskbar = CP.Structures.Windows10x.AccentTaskbarLevels.None
            If My.PreviewStyle = WindowStyle.W11 Then
                UpdateLegends()
                ApplyColorsToElements(My.CP)
            End If
        End If
    End Sub

    Private Sub W11_Accent_Taskbar_CheckedChanged(sender As Object) Handles W11_Accent_Taskbar.CheckedChanged
        If _Shown And sender.Checked Then
            My.CP.Windows11.ApplyAccentOnTaskbar = CP.Structures.Windows10x.AccentTaskbarLevels.Taskbar
            If My.PreviewStyle = WindowStyle.W11 Then
                UpdateLegends()
                ApplyColorsToElements(My.CP)
            End If
        End If
    End Sub

    Private Sub W11_Accent_StartTaskbar_CheckedChanged(sender As Object) Handles W11_Accent_StartTaskbar.CheckedChanged
        If _Shown And sender.Checked Then
            My.CP.Windows11.ApplyAccentOnTaskbar = CP.Structures.Windows10x.AccentTaskbarLevels.Taskbar_Start_AC
            If My.PreviewStyle = WindowStyle.W11 Then
                UpdateLegends()
                ApplyColorsToElements(My.CP)
            End If
        End If
    End Sub

    Private Sub W11_Color_Index1_Click(sender As Object, e As EventArgs) Handles W11_Color_Index1.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                My.CP.Windows11.Color_Index1 = sender.BackColor
                If My.PreviewStyle = WindowStyle.W11 Then ApplyColorsToElements(My.CP)
            End If
            Exit Sub
        End If

        Dim CList As New List(Of Control)
        Dim C As Color
        CList.Add(sender)

        If ExplorerPatcher.IsAllowed Then
            CList.Add(taskbar)

            If Not My.CP.Windows11.WinMode_Light Then
                CList.Add(ActionCenter)
                Dim _Conditions As New Conditions With {.AppUnderlineOnly = True, .ActionCenterBtn = True}
                C = ColorPickerDlg.Pick(CList, _Conditions)
            Else
                Dim _Conditions As New Conditions With {.AppUnderlineWithTaskbar = True}
                C = ColorPickerDlg.Pick(CList, _Conditions)
            End If
        Else
            If Not My.CP.Windows11.WinMode_Light Then
                CList.Add(ActionCenter)
                CList.Add(taskbar)

                Dim _Conditions As New Conditions With {.AppUnderlineOnly = True, .ActionCenterBtn = True}
                C = ColorPickerDlg.Pick(CList, _Conditions)
            Else
                C = ColorPickerDlg.Pick(CList)
            End If
        End If

        My.CP.Windows11.Color_Index1 = Color.FromArgb(255, C)
        If My.PreviewStyle = WindowStyle.W11 Then ApplyColorsToElements(My.CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()
    End Sub

    Private Sub W11_TaskbarFrontAndFoldersOnStart_pick_Click(sender As Object, e As EventArgs) Handles W11_TaskbarFrontAndFoldersOnStart_pick.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                My.CP.Windows11.Color_Index5 = sender.BackColor
                If My.PreviewStyle = WindowStyle.W11 Then ApplyColorsToElements(My.CP)
            End If
            Exit Sub
        End If

        Dim CList As New List(Of Control) From {sender}

        If ExplorerPatcher.IsAllowed Then
            If Not My.CP.Windows11.WinMode_Light Then
                CList.Add(ActionCenter)
                CList.Add(taskbar)
                If Not My.EP.UseStart10 Then
                    CList.Add(start)
                End If
            Else
                CList.Add(lnk_preview)
            End If

        Else
            If Not My.CP.Windows11.WinMode_Light Then
                CList.Add(taskbar)
                CList.Add(start)
                CList.Add(ActionCenter)
            Else
                CList.Add(taskbar)
                CList.Add(lnk_preview)
            End If
        End If

        Dim C As Color = ColorPickerDlg.Pick(CList)
        My.CP.Windows11.Color_Index5 = Color.FromArgb(255, C)
        If My.PreviewStyle = WindowStyle.W11 Then ApplyColorsToElements(My.CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()
    End Sub

    Private Sub W11_Color_Index0_Click(sender As Object, e As EventArgs) Handles W11_Color_Index0.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                My.CP.Windows11.Color_Index0 = sender.BackColor
                If My.PreviewStyle = WindowStyle.W11 Then ApplyColorsToElements(My.CP)
            End If
            Exit Sub
        End If

        Dim CList As New List(Of Control)
        Dim C As Color

        CList.Add(sender)

        If My.CP.Windows11.WinMode_Light Then
            CList.Add(start)
            CList.Add(ActionCenter)
            C = ColorPickerDlg.Pick(CList)
        Else
            CList.Add(lnk_preview)
            Dim _Conditions As New Conditions With {.AppUnderlineOnly = True}
            C = ColorPickerDlg.Pick(CList, _Conditions)
        End If

        My.CP.Windows11.Color_Index0 = Color.FromArgb(255, C)
        If My.PreviewStyle = WindowStyle.W11 Then ApplyColorsToElements(My.CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()

    End Sub

    Private Sub W11_Color_Index3_Click(sender As Object, e As EventArgs) Handles W11_Color_Index3.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                My.CP.Windows11.Color_Index3 = sender.BackColor
                If My.PreviewStyle = WindowStyle.W11 Then ApplyColorsToElements(My.CP)
            End If
            Exit Sub
        End If

        Dim CList As New List(Of Control)
        Dim C As Color

        CList.Add(sender)
        CList.Add(taskbar)
        CList.Add(setting_icon_preview)

        Dim _Conditions As New Conditions With {.AppUnderlineOnly = True}
        C = ColorPickerDlg.Pick(CList, _Conditions)

        My.CP.Windows11.Color_Index3 = Color.FromArgb(255, C)
        If My.PreviewStyle = WindowStyle.W11 Then ApplyColorsToElements(My.CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()
    End Sub

    Private Sub W11_Color_Index6_Click(sender As Object, e As EventArgs) Handles W11_Color_Index6.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                My.CP.Windows11.Color_Index6 = sender.BackColor
                If My.PreviewStyle = WindowStyle.W11 Then ApplyColorsToElements(My.CP)
            End If
            Exit Sub
        End If

        Dim CList As New List(Of Control)
        Dim C As Color

        CList.Add(sender)
        Dim _Conditions As New Conditions

        If Not ExplorerPatcher.IsAllowed Then
            CList.Add(taskbar)
            _Conditions = New Conditions With {.AppBackgroundOnly = True}
        End If

        C = ColorPickerDlg.Pick(CList, _Conditions)

        My.CP.Windows11.Color_Index6 = Color.FromArgb(255, C)
        If My.PreviewStyle = WindowStyle.W11 Then ApplyColorsToElements(My.CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()
    End Sub

    Private Sub W11_Color_Index5_Click(sender As Object, e As EventArgs) Handles W11_Color_Index5.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                My.CP.Windows11.StartMenu_Accent = sender.BackColor
                If My.PreviewStyle = WindowStyle.W11 Then ApplyColorsToElements(My.CP)
            End If
            Exit Sub
        End If


        Dim CList As New List(Of Control) From {sender}

        Dim C As Color = ColorPickerDlg.Pick(CList)
        My.CP.Windows11.StartMenu_Accent = Color.FromArgb(255, C)
        If My.PreviewStyle = WindowStyle.W11 Then ApplyColorsToElements(My.CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()

    End Sub

    Private Sub W11_Color_Index4_pick_Click(sender As Object, e As EventArgs) Handles W11_Color_Index4.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                My.CP.Windows11.Color_Index2 = sender.BackColor
                If My.PreviewStyle = WindowStyle.W11 Then ApplyColorsToElements(My.CP)
            End If
            Exit Sub
        End If


        Dim CList As New List(Of Control) From {sender}

        Dim C As Color = ColorPickerDlg.Pick(CList)
        My.CP.Windows11.Color_Index2 = Color.FromArgb(255, C)
        If My.PreviewStyle = WindowStyle.W11 Then ApplyColorsToElements(My.CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()
    End Sub

    Private Sub W11_Color_Index2_Click(sender As Object, e As EventArgs) Handles W11_Color_Index2.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                My.CP.Windows11.Color_Index4 = sender.BackColor
                If My.PreviewStyle = WindowStyle.W11 Then ApplyColorsToElements(My.CP)
            End If
            Exit Sub
        End If


        Dim CList As New List(Of Control) From {sender}

        Dim C As Color

        If ExplorerPatcher.IsAllowed Then
            If My.EP.UseStart10 Then
                CList.Add(start)
                C = ColorPickerDlg.Pick(CList)

            Else
                If My.CP.Windows11.WinMode_Light Then
                    CList.Add(ActionCenter)

                    Dim _Conditions As New Conditions With {.ActionCenterBtn = True}
                    C = ColorPickerDlg.Pick(CList, _Conditions)
                Else
                    CList.Add(start)

                    Dim _Conditions As New Conditions With {.StartColorOnly = True}
                    C = ColorPickerDlg.Pick(CList, _Conditions)
                End If
            End If
        Else

            If My.CP.Windows11.WinMode_Light Then
                CList.Add(ActionCenter)
                CList.Add(taskbar)

                Dim _Conditions As New Conditions With {.AppUnderlineOnly = True, .ActionCenterBtn = True}

                C = ColorPickerDlg.Pick(CList, _Conditions)
            Else
                CList.Add(start)

                Dim _Conditions As New Conditions With {.StartColorOnly = True}
                C = ColorPickerDlg.Pick(CList, _Conditions)
            End If

        End If


        My.CP.Windows11.Color_Index4 = Color.FromArgb(255, C)
        If My.PreviewStyle = WindowStyle.W11 Then ApplyColorsToElements(My.CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()
    End Sub

    Private Sub W11_Color_Index7_Click(sender As Object, e As EventArgs) Handles W11_Color_Index7.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            My.CP.Windows11.Color_Index7 = SubMenu.ShowMenu(sender)
            Exit Sub
        End If

        Dim CList As New List(Of Control) From {sender}
        Dim C As Color = ColorPickerDlg.Pick(CList)

        My.CP.Windows11.Color_Index7 = Color.FromArgb(255, C)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()
    End Sub

    Private Sub W11_XenonButton8_Click_1(sender As Object, e As EventArgs) Handles W11_XenonButton8.Click
        MsgBox(My.Lang.TitlebarColorNotice, MsgBoxStyle.Information)
    End Sub

#End Region

#Region "Windows 10"
    Private Sub W10_ActiveTitlebar_pick_Click(sender As Object, e As EventArgs) Handles W10_ActiveTitlebar_pick.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                My.CP.Windows10.Titlebar_Active = sender.BackColor
                If My.PreviewStyle = WindowStyle.W10 Then ApplyColorsToElements(My.CP)
            End If
            Exit Sub
        End If

        Dim CList As New List(Of Control) From {sender, XenonWindow1}

        Dim C As Color = ColorPickerDlg.Pick(CList)
        My.CP.Windows10.Titlebar_Active = Color.FromArgb(255, C)
        If My.PreviewStyle = WindowStyle.W10 Then ApplyColorsToElements(My.CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()
    End Sub

    Private Sub W10_InactiveTitlebar_pick_Click(sender As Object, e As EventArgs) Handles W10_InactiveTitlebar_pick.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                My.CP.Windows10.Titlebar_Inactive = sender.BackColor
                If My.PreviewStyle = WindowStyle.W10 Then ApplyColorsToElements(My.CP)
            End If
            Exit Sub
        End If

        Dim CList As New List(Of Control) From {sender, XenonWindow2}

        Dim _Conditions As New Conditions With {.Window_InactiveTitlebar = True}
        Dim C As Color = ColorPickerDlg.Pick(CList, _Conditions)

        My.CP.Windows10.Titlebar_Inactive = Color.FromArgb(255, C)

        If My.PreviewStyle = WindowStyle.W10 Then ApplyColorsToElements(My.CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()
    End Sub

    Private Sub W10_WinMode_Toggle_CheckedChanged(sender As Object, e As EventArgs) Handles W10_WinMode_Toggle.CheckedChanged
        If _Shown Then
            My.CP.Windows10.WinMode_Light = Not sender.Checked
            If My.PreviewStyle = WindowStyle.W10 Then
                UpdateLegends()
                ApplyColorsToElements(My.CP)
            End If
        End If
    End Sub

    Private Sub W10_AppMode_Toggle_CheckedChanged(sender As Object, e As EventArgs) Handles W10_AppMode_Toggle.CheckedChanged
        If _Shown Then
            My.CP.Windows10.AppMode_Light = Not sender.Checked
            If My.PreviewStyle = WindowStyle.W10 Then ApplyColorsToElements(My.CP)
        End If
    End Sub

    Private Sub W10_Transparency_Toggle_CheckedChanged(sender As Object, e As EventArgs) Handles W10_Transparency_Toggle.CheckedChanged
        If _Shown Then
            My.CP.Windows10.Transparency = sender.Checked
            If My.PreviewStyle = WindowStyle.W10 Then
                UpdateLegends()
                ApplyColorsToElements(My.CP)
            End If
        End If
    End Sub

    Private Sub W10_ShowAccentOnTitlebarAndBorders_Toggle_CheckedChanged(sender As Object, e As EventArgs) Handles W10_ShowAccentOnTitlebarAndBorders_Toggle.CheckedChanged
        If _Shown Then
            My.CP.Windows10.ApplyAccentOnTitlebars = sender.Checked
            If My.PreviewStyle = WindowStyle.W10 Then ApplyColorsToElements(My.CP)
        End If
    End Sub

    Private Sub W10_Accent_None_CheckedChanged(sender As Object) Handles W10_Accent_None.CheckedChanged
        If _Shown And sender.Checked Then
            My.CP.Windows10.ApplyAccentOnTaskbar = CP.Structures.Windows10x.AccentTaskbarLevels.None
            If My.PreviewStyle = WindowStyle.W10 Then
                UpdateLegends()
                ApplyColorsToElements(My.CP)
            End If
        End If
    End Sub

    Private Sub W10_Accent_Taskbar_CheckedChanged(sender As Object) Handles W10_Accent_Taskbar.CheckedChanged
        If _Shown And sender.Checked Then
            My.CP.Windows10.ApplyAccentOnTaskbar = CP.Structures.Windows10x.AccentTaskbarLevels.Taskbar
            If My.PreviewStyle = WindowStyle.W10 Then
                UpdateLegends()
                ApplyColorsToElements(My.CP)
            End If
        End If
    End Sub

    Private Sub W10_Accent_StartTaskbar_CheckedChanged(sender As Object) Handles W10_Accent_StartTaskbar.CheckedChanged
        If _Shown And sender.Checked Then
            My.CP.Windows10.ApplyAccentOnTaskbar = CP.Structures.Windows10x.AccentTaskbarLevels.Taskbar_Start_AC
            If My.PreviewStyle = WindowStyle.W10 Then
                UpdateLegends()
                ApplyColorsToElements(My.CP)
            End If
        End If
    End Sub

    Private Sub W10_TBTransparency_Toggle_CheckedChanged(sender As Object, e As EventArgs) Handles W10_TBTransparency_Toggle.CheckedChanged
        If _Shown Then
            My.CP.Windows10.IncreaseTBTransparency = sender.Checked
            If My.PreviewStyle = WindowStyle.W10 Then ApplyColorsToElements(My.CP)
        End If
    End Sub

    Private Sub W10_Color_Index1_Click(sender As Object, e As EventArgs) Handles W10_Color_Index1.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                My.CP.Windows10.Color_Index1 = sender.BackColor
                If My.PreviewStyle = WindowStyle.W10 Then ApplyColorsToElements(My.CP)
            End If
            Exit Sub
        End If

        Dim CList As New List(Of Control)
        Dim C As Color
        CList.Add(sender)

        Dim _Conditions As New Conditions

        Select Case Not My.CP.Windows10.WinMode_Light
            Case True
                CList.Add(taskbar)  ''AppUnderline
                _Conditions.AppUnderlineOnly = True
            Case False
                If My.CP.Windows10.ApplyAccentOnTaskbar <> CP.Structures.Windows10x.AccentTaskbarLevels.None Then
                    CList.Add(taskbar)  ''AppUnderline
                    _Conditions.AppUnderlineOnly = True
                End If
        End Select

        C = ColorPickerDlg.Pick(CList, _Conditions)

        My.CP.Windows10.Color_Index1 = Color.FromArgb(255, C)
        If My.PreviewStyle = WindowStyle.W10 Then ApplyColorsToElements(My.CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()
    End Sub

    Private Sub W10_TaskbarFrontAndFoldersOnStart_pick_Click(sender As Object, e As EventArgs) Handles W10_TaskbarFrontAndFoldersOnStart_pick.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                My.CP.Windows10.Color_Index5 = sender.BackColor
                If My.PreviewStyle = WindowStyle.W10 Then ApplyColorsToElements(My.CP)
            End If
            Exit Sub
        End If

        Dim CList As New List(Of Control) From {sender}

        If My.CP.Windows10.Transparency Then
            'CList.Add(start) ''Hamburger
        Else
            CList.Add(taskbar)
        End If

        Dim C As Color = ColorPickerDlg.Pick(CList)
        My.CP.Windows10.Color_Index5 = Color.FromArgb(255, C)
        If My.PreviewStyle = WindowStyle.W10 Then ApplyColorsToElements(My.CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()

    End Sub

    Private Sub W10_Color_Index0_Click(sender As Object, e As EventArgs) Handles W10_Color_Index0.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                My.CP.Windows10.Color_Index0 = sender.BackColor
                If My.PreviewStyle = WindowStyle.W10 Then ApplyColorsToElements(My.CP)
            End If
            Exit Sub
        End If

        Dim CList As New List(Of Control)
        Dim C As Color

        CList.Add(sender)


        Dim _Conditions As New Conditions

        Select Case Not My.CP.Windows10.WinMode_Light
            Case True
                CList.Add(ActionCenter) ''Link
                _Conditions.ActionCenterLink = True

            Case False
                If My.CP.Windows10.ApplyAccentOnTaskbar = CP.Structures.Windows10x.AccentTaskbarLevels.Taskbar_Start_AC Then
                    CList.Add(ActionCenter) ''Link
                    _Conditions.ActionCenterLink = True
                End If
        End Select

        C = ColorPickerDlg.Pick(CList, _Conditions)

        My.CP.Windows10.Color_Index0 = Color.FromArgb(255, C)
        If My.PreviewStyle = WindowStyle.W10 Then ApplyColorsToElements(My.CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()

    End Sub

    Private Sub W10_Color_Index3_Click(sender As Object, e As EventArgs) Handles W10_Color_Index3.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                My.CP.Windows10.Color_Index3 = sender.BackColor
                If My.PreviewStyle = WindowStyle.W10 Then ApplyColorsToElements(My.CP)
            End If
            Exit Sub
        End If

        Dim CList As New List(Of Control)
        Dim C As Color

        CList.Add(sender)

        Dim _Conditions As New Conditions

        Select Case Not My.CP.Windows10.WinMode_Light
            Case True
                If My.CP.Windows10.Transparency Then
                    CList.Add(setting_icon_preview)
                    CList.Add(ActionCenter) : _Conditions.ActionCenterBtn = True
                    CList.Add(lnk_preview)
                    If My.CP.Windows10.ApplyAccentOnTaskbar <> CP.Structures.Windows10x.AccentTaskbarLevels.None Then
                        CList.Add(taskbar)  ''AppBackground
                        _Conditions.AppBackgroundOnly = True

                    End If
                Else
                    CList.Add(setting_icon_preview)
                    CList.Add(ActionCenter) : _Conditions.ActionCenterBtn = True
                    CList.Add(lnk_preview)
                End If
            Case False
                CList.Add(setting_icon_preview)
                CList.Add(ActionCenter) : _Conditions.ActionCenterBtn = True
                CList.Add(lnk_preview)

                If My.CP.Windows10.ApplyAccentOnTaskbar = CP.Structures.Windows10x.AccentTaskbarLevels.None Then
                    CList.Add(taskbar)  ''AppBackground
                    _Conditions.AppBackgroundOnly = True
                    _Conditions.AppUnderlineOnly = True

                End If
        End Select
        C = ColorPickerDlg.Pick(CList, _Conditions)

        My.CP.Windows10.Color_Index3 = Color.FromArgb(255, C)
        If My.PreviewStyle = WindowStyle.W10 Then ApplyColorsToElements(My.CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()
    End Sub

    Private Sub W10_Color_Index6_Click(sender As Object, e As EventArgs) Handles W10_Color_Index6.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                My.CP.Windows10.Color_Index6 = sender.BackColor
                If My.PreviewStyle = WindowStyle.W10 Then ApplyColorsToElements(My.CP)
            End If
            Exit Sub
        End If

        Dim CList As New List(Of Control)
        Dim C As Color

        CList.Add(sender)

        Dim _Conditions As New Conditions

        Select Case Not My.CP.Windows10.WinMode_Light
            Case True

                If My.CP.Windows10.Transparency Then
                    CList.Add(taskbar)
                End If

            Case False

                If My.CP.Windows10.Transparency Then
                    CList.Add(taskbar)

                    If My.CP.Windows10.ApplyAccentOnTaskbar <> CP.Structures.Windows10x.AccentTaskbarLevels.Taskbar_Start_AC Then
                        CList.Add(ActionCenter) ''ActionCenterLinks
                        _Conditions.ActionCenterLink = True
                    End If

                Else
                    If My.CP.Windows10.ApplyAccentOnTaskbar <> CP.Structures.Windows10x.AccentTaskbarLevels.Taskbar_Start_AC Then
                        CList.Add(ActionCenter) ''ActionCenterLinks
                        _Conditions.ActionCenterLink = True

                    End If
                End If
        End Select

        C = ColorPickerDlg.Pick(CList, _Conditions)

        My.CP.Windows10.Color_Index6 = Color.FromArgb(255, C)
        If My.PreviewStyle = WindowStyle.W10 Then ApplyColorsToElements(My.CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()
    End Sub

    Private Sub W10_Color_Index5_Click(sender As Object, e As EventArgs) Handles W10_Color_Index5.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                My.CP.Windows10.StartMenu_Accent = sender.BackColor
                If My.PreviewStyle = WindowStyle.W10 Then ApplyColorsToElements(My.CP)
            End If
            Exit Sub
        End If


        Dim CList As New List(Of Control) From {sender}


        Dim C As Color = ColorPickerDlg.Pick(CList)
        My.CP.Windows10.StartMenu_Accent = Color.FromArgb(255, C)
        If My.PreviewStyle = WindowStyle.W10 Then ApplyColorsToElements(My.CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()

    End Sub

    Private Sub W10_Color_Index4_pick_Click(sender As Object, e As EventArgs) Handles W10_Color_Index4.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                My.CP.Windows10.Color_Index2 = sender.BackColor
                If My.PreviewStyle = WindowStyle.W10 Then ApplyColorsToElements(My.CP)
            End If
            Exit Sub
        End If


        Dim CList As New List(Of Control) From {sender}

        If My.PreviewStyle = WindowStyle.W10 Then
            'CList.Add(taskbar) 'Start Icon Hover
        End If

        Dim C As Color = ColorPickerDlg.Pick(CList)
        My.CP.Windows10.Color_Index2 = Color.FromArgb(255, C)
        If My.PreviewStyle = WindowStyle.W10 Then ApplyColorsToElements(My.CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()

    End Sub

    Private Sub W10_Color_Index2_Click(sender As Object, e As EventArgs) Handles W10_Color_Index2.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                My.CP.Windows10.Color_Index4 = sender.BackColor
                If My.PreviewStyle = WindowStyle.W10 Then ApplyColorsToElements(My.CP)
            End If
            Exit Sub
        End If


        Dim CList As New List(Of Control) From {sender}

        Dim C As Color


        Dim _Conditions As New Conditions

        Select Case Not My.CP.Windows10.WinMode_Light
            Case True

                If My.CP.Windows10.Transparency Then
                    CList.Add(start)
                    CList.Add(ActionCenter)
                Else
                    CList.Add(start)
                    CList.Add(ActionCenter)
                    CList.Add(taskbar)  'AppBackground
                    _Conditions.AppBackgroundOnly = True
                End If

            Case False
                If My.CP.Windows10.Transparency Then
                    CList.Add(start)
                    CList.Add(ActionCenter)
                Else
                    If My.CP.Windows10.ApplyAccentOnTaskbar = CP.Structures.Windows10x.AccentTaskbarLevels.None Then
                        CList.Add(start)
                        CList.Add(ActionCenter)
                    ElseIf My.CP.Windows10.ApplyAccentOnTaskbar = CP.Structures.Windows10x.AccentTaskbarLevels.Taskbar Then
                        CList.Add(start)
                        CList.Add(ActionCenter)
                        CList.Add(taskbar)  'Start Button Hover
                        _Conditions.StartColorOnly = True
                    Else
                        CList.Add(start)
                        CList.Add(ActionCenter)
                        CList.Add(taskbar)  'AppBackground
                        _Conditions.AppBackgroundOnly = True
                    End If
                End If
        End Select

        C = ColorPickerDlg.Pick(CList, _Conditions)

        My.CP.Windows10.Color_Index4 = Color.FromArgb(255, C)
        If My.PreviewStyle = WindowStyle.W10 Then ApplyColorsToElements(My.CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()
    End Sub

    Private Sub W10_Color_Index7_Click(sender As Object, e As EventArgs) Handles W10_Color_Index7.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            My.CP.Windows10.Color_Index7 = SubMenu.ShowMenu(sender)
            Exit Sub
        End If

        Dim CList As New List(Of Control) From {sender}
        Dim C As Color = ColorPickerDlg.Pick(CList)

        My.CP.Windows10.Color_Index7 = Color.FromArgb(255, C)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()
    End Sub

    Private Sub W10_XenonButton8_Click_1(sender As Object, e As EventArgs) Handles W10_XenonButton8.Click
        MsgBox(My.Lang.TitlebarColorNotice, MsgBoxStyle.Information)
    End Sub

    Private Sub W10_XenonButton25_Click(sender As Object, e As EventArgs) Handles W10_XenonButton25.Click
        MsgBox(My.Lang.CP_AccentOnTaskbarTib, MsgBoxStyle.Information)
    End Sub

#End Region

#Region "Windows 8.1"
    Private Sub W8_ColorizationColor_pick_Click(sender As Object, e As EventArgs) Handles W8_ColorizationColor_pick.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                My.CP.Windows8.ColorizationColor = sender.BackColor
                If My.PreviewStyle = WindowStyle.W8 Then ApplyColorsToElements(My.CP)
            End If
            Exit Sub
        End If

        Dim CList As New List(Of Control) From {
           sender,
           start,
           taskbar,
           XenonWindow1,
           XenonWindow2
       }

        Dim _Conditions As New Conditions With {.Window_ActiveTitlebar = True, .Window_InactiveTitlebar = True, .Win7LivePreview_Colorization = True}

        Dim C As Color = ColorPickerDlg.Pick(CList, _Conditions)

        My.CP.Windows8.ColorizationColor = Color.FromArgb(255, C)

        If My.PreviewStyle = WindowStyle.W8 Then ApplyColorsToElements(My.CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()
    End Sub

    Private Sub W8_ColorizationBalance_bar_Scroll(sender As Object) Handles W8_ColorizationBalance_bar.Scroll
        If _Shown Then
            W8_ColorizationBalance_val.Text = sender.Value.ToString()
            My.CP.Windows8.ColorizationColorBalance = W8_ColorizationBalance_bar.Value
            If My.PreviewStyle = WindowStyle.W8 Then ApplyColorsToElements(My.CP)
        End If
    End Sub

    Private Sub W8_start_pick_Click(sender As Object, e As EventArgs) Handles W8_start_pick.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                My.CP.Windows8.StartColor = sender.BackColor
                If My.PreviewStyle = WindowStyle.W8 Then ApplyColorsToElements(My.CP)
            End If
            Exit Sub
        End If

        Dim CList As New List(Of Control) From {sender}

        Dim C As Color = ColorPickerDlg.Pick(CList)

        My.CP.Windows8.StartColor = Color.FromArgb(255, C)

        If My.PreviewStyle = WindowStyle.W8 Then ApplyColorsToElements(My.CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()
    End Sub

    Private Sub W8_accent_pick_Click(sender As Object, e As EventArgs) Handles W8_accent_pick.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                My.CP.Windows8.AccentColor = sender.BackColor
                If My.PreviewStyle = WindowStyle.W8 Then ApplyColorsToElements(My.CP)
            End If
            Exit Sub
        End If

        Dim CList As New List(Of Control) From {sender}

        Dim C As Color = ColorPickerDlg.Pick(CList)

        My.CP.Windows8.AccentColor = Color.FromArgb(255, C)

        If My.PreviewStyle = WindowStyle.W8 Then ApplyColorsToElements(My.CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()
    End Sub

    Private Sub W8_personalcls_background_pick_Click(sender As Object, e As EventArgs) Handles W8_personalcls_background_pick.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                My.CP.Windows8.PersonalColors_Background = sender.BackColor
                If My.PreviewStyle = WindowStyle.W8 Then ApplyColorsToElements(My.CP)
            End If
            Exit Sub
        End If

        Dim CList As New List(Of Control) From {sender}

        Dim C As Color = ColorPickerDlg.Pick(CList)

        My.CP.Windows8.PersonalColors_Background = Color.FromArgb(255, C)

        If My.PreviewStyle = WindowStyle.W8 Then ApplyColorsToElements(My.CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()
    End Sub

    Private Sub W8_personalcolor_accent_pick_Click(sender As Object, e As EventArgs) Handles W8_personalcolor_accent_pick.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                My.CP.Windows8.PersonalColors_Accent = sender.BackColor
                If My.PreviewStyle = WindowStyle.W8 Then ApplyColorsToElements(My.CP)
            End If
            Exit Sub
        End If

        Dim CList As New List(Of Control) From {sender}

        Dim C As Color = ColorPickerDlg.Pick(CList)

        My.CP.Windows8.PersonalColors_Accent = Color.FromArgb(255, C)

        If My.PreviewStyle = WindowStyle.W8 Then ApplyColorsToElements(My.CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()
    End Sub

    Private Sub W8_ColorizationBalance_val_Click(sender As Object, e As EventArgs) Handles W8_ColorizationBalance_val.Click
        Dim response As String = InputBox(My.Lang.InputValue, sender.text, My.Lang.ItMustBeNumerical)
        sender.Text = Math.Max(Math.Min(Val(response), W8_ColorizationBalance_bar.Maximum), W8_ColorizationBalance_bar.Minimum) : W8_ColorizationBalance_bar.Value = Val(sender.Text)
    End Sub

    Private Sub W8_theme_aero_CheckedChanged(sender As Object) Handles W8_theme_aero.CheckedChanged
        If W8_theme_aero.Checked Then
            My.CP.Windows8.Theme = CP.Structures.Windows7.Themes.Aero
            If My.PreviewStyle = WindowStyle.W8 Then ApplyColorsToElements(My.CP)
        End If
    End Sub

    Private Sub W8_theme_aerolite_CheckedChanged(sender As Object) Handles W8_theme_aerolite.CheckedChanged
        If W8_theme_aerolite.Checked Then
            My.CP.Windows8.Theme = CP.Structures.Windows7.Themes.AeroLite
            If My.PreviewStyle = WindowStyle.W8 Then ApplyColorsToElements(My.CP)
        End If
    End Sub

    Private Sub W8_start_Click(sender As Object, e As EventArgs) Handles W8_start.Click
        Start8Selector.ShowDialog()
        If My.PreviewStyle = WindowStyle.W8 Then ApplyColorsToElements(My.CP)
    End Sub

    Private Sub W8_logonui_Click(sender As Object, e As EventArgs) Handles W8_logonui.Click
        LogonUI8Colors.ShowDialog()
        If My.PreviewStyle = WindowStyle.W8 Then ApplyColorsToElements(My.CP)
    End Sub
#End Region

#Region "Windows 7"
    Private Sub W7_ColorizationColor_pick_Click(sender As Object, e As EventArgs) Handles W7_ColorizationColor_pick.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                My.CP.Windows7.ColorizationColor = sender.BackColor
                If My.PreviewStyle = WindowStyle.W7 Then ApplyColorsToElements(My.CP)
            End If
            Exit Sub
        End If

        Dim CList As New List(Of Control) From {
            sender,
            start,
            taskbar,
            XenonWindow1,
            XenonWindow2
        }

        Dim _Conditions As New Conditions With {.Win7 = True, .Color1 = True, .BackColor1 = True, .Win7LivePreview_Colorization = True}

        Dim C As Color = ColorPickerDlg.Pick(CList, _Conditions)

        My.CP.Windows7.ColorizationColor = Color.FromArgb(255, C)

        If My.PreviewStyle = WindowStyle.W7 Then ApplyColorsToElements(My.CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()
    End Sub

    Private Sub W7_ColorizationAfterglow_pick_Click(sender As Object, e As EventArgs) Handles W7_ColorizationAfterglow_pick.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                My.CP.Windows7.ColorizationAfterglow = sender.BackColor
                ApplyColorsToElements(My.CP)
            End If
            Exit Sub
        End If

        Dim CList As New List(Of Control) From {
            sender,
            start,
            taskbar,
            XenonWindow1,
            XenonWindow2
        }

        Dim _Conditions As New Conditions With {.Win7 = True, .Color2 = True, .BackColor2 = True, .Win7LivePreview_AfterGlow = True}

        Dim C As Color = ColorPickerDlg.Pick(CList, _Conditions)

        My.CP.Windows7.ColorizationAfterglow = Color.FromArgb(255, C)

        If My.PreviewStyle = WindowStyle.W7 Then ApplyColorsToElements(My.CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()
    End Sub

    Private Sub W7_EnableAeroPeek_toggle_CheckedChanged(sender As Object, e As EventArgs) Handles W7_EnableAeroPeek_toggle.CheckedChanged
        If _Shown Then My.CP.Windows7.EnableAeroPeek = W7_EnableAeroPeek_toggle.Checked
    End Sub

    Private Sub W7_AlwaysHibernateThumbnails_Toggle_CheckedChanged(sender As Object, e As EventArgs) Handles W7_AlwaysHibernateThumbnails_Toggle.CheckedChanged
        If _Shown Then My.CP.Windows7.AlwaysHibernateThumbnails = W7_AlwaysHibernateThumbnails_Toggle.Checked
    End Sub

    Private Sub W7_ColorizationColorBalance_bar_Scroll(sender As Object) Handles W7_ColorizationColorBalance_bar.Scroll
        If _Shown Then
            W7_ColorizationColorBalance_val.Text = sender.Value.ToString()
            My.CP.Windows7.ColorizationColorBalance = W7_ColorizationColorBalance_bar.Value
            If My.PreviewStyle = WindowStyle.W7 Then ApplyColorsToElements(My.CP)
        End If
    End Sub

    Private Sub W7_ColorizationBlurBalance_bar_Scroll(sender As Object) Handles W7_ColorizationBlurBalance_bar.Scroll
        If _Shown Then
            W7_ColorizationBlurBalance_val.Text = sender.Value.ToString()
            My.CP.Windows7.ColorizationBlurBalance = W7_ColorizationBlurBalance_bar.Value
            If My.PreviewStyle = WindowStyle.W7 Then ApplyColorsToElements(My.CP)
        End If
    End Sub

    Private Sub W7_ColorizationGlassReflectionIntensity_bar_Scroll(sender As Object) Handles W7_ColorizationGlassReflectionIntensity_bar.Scroll
        If _Shown Then
            W7_ColorizationGlassReflectionIntensity_val.Text = sender.Value.ToString()
            My.CP.Windows7.ColorizationGlassReflectionIntensity = W7_ColorizationGlassReflectionIntensity_bar.Value
            If My.PreviewStyle = WindowStyle.W7 Then ApplyColorsToElements(My.CP)
        End If
    End Sub

    Private Sub W7_theme_classic_CheckedChanged(sender As Object) Handles W7_theme_classic.CheckedChanged
        If W7_theme_classic.Checked Then
            My.CP.Windows7.Theme = CP.Structures.Windows7.Themes.Classic
            If My.PreviewStyle = WindowStyle.W7 Then
                ApplyColorsToElements(My.CP)
                ApplyStylesToElements(My.CP, False)
            End If
        End If

    End Sub

    Private Sub W7_theme_basic_CheckedChanged(sender As Object) Handles W7_theme_basic.CheckedChanged
        If W7_theme_basic.Checked Then
            My.CP.Windows7.Theme = CP.Structures.Windows7.Themes.Basic
            If My.PreviewStyle = WindowStyle.W7 Then
                ApplyColorsToElements(My.CP)
                ApplyStylesToElements(My.CP, False)
            End If
        End If
    End Sub

    Private Sub W7_theme_aeroopaque_CheckedChanged(sender As Object) Handles W7_theme_aeroopaque.CheckedChanged
        If W7_theme_aeroopaque.Checked Then
            My.CP.Windows7.Theme = CP.Structures.Windows7.Themes.AeroOpaque
            If My.PreviewStyle = WindowStyle.W7 Then
                ApplyColorsToElements(My.CP)
                ApplyStylesToElements(My.CP, False)
            End If
        End If
    End Sub

    Private Sub W7_theme_Aero_CheckedChanged(sender As Object) Handles W7_theme_aero.CheckedChanged
        If W7_theme_aero.Checked Then
            My.CP.Windows7.Theme = CP.Structures.Windows7.Themes.Aero
            If My.PreviewStyle = WindowStyle.W7 Then
                ApplyColorsToElements(My.CP)
                ApplyStylesToElements(My.CP, False)
            End If
        End If
    End Sub

    Private Sub W7_ColorizationAfterglowBalance_bar_Scroll(sender As Object) Handles W7_ColorizationAfterglowBalance_bar.Scroll
        If _Shown Then
            W7_ColorizationAfterglowBalance_val.Text = sender.Value.ToString()
            My.CP.Windows7.ColorizationAfterglowBalance = W7_ColorizationAfterglowBalance_bar.Value
            If My.PreviewStyle = WindowStyle.W7 Then ApplyColorsToElements(My.CP)
        End If
    End Sub
    Private Sub W7_ColorizationColorBalance_val_Click(sender As Object, e As EventArgs) Handles W7_ColorizationColorBalance_val.Click
        Dim response As String = InputBox(My.Lang.InputValue, sender.text, My.Lang.ItMustBeNumerical)
        sender.Text = Math.Max(Math.Min(Val(response), W7_ColorizationColorBalance_bar.Maximum), W7_ColorizationColorBalance_bar.Minimum) : W7_ColorizationColorBalance_bar.Value = Val(sender.Text)
    End Sub

    Private Sub W7_ColorizationAfterglowBalance_val_Click(sender As Object, e As EventArgs) Handles W7_ColorizationAfterglowBalance_val.Click
        Dim response As String = InputBox(My.Lang.InputValue, sender.text, My.Lang.ItMustBeNumerical)
        sender.Text = Math.Max(Math.Min(Val(response), W7_ColorizationAfterglowBalance_bar.Maximum), W7_ColorizationAfterglowBalance_bar.Minimum) : W7_ColorizationAfterglowBalance_bar.Value = Val(sender.Text)
    End Sub

    Private Sub W7_ColorizationBlurBalance_val_Click(sender As Object, e As EventArgs) Handles W7_ColorizationBlurBalance_val.Click
        Dim response As String = InputBox(My.Lang.InputValue, sender.text, My.Lang.ItMustBeNumerical)
        sender.Text = Math.Max(Math.Min(Val(response), W7_ColorizationBlurBalance_bar.Maximum), W7_ColorizationBlurBalance_bar.Minimum) : W7_ColorizationBlurBalance_bar.Value = Val(sender.Text)
    End Sub

    Private Sub W7_ColorizationGlassReflectionIntensity_val_Click(sender As Object, e As EventArgs) Handles W7_ColorizationGlassReflectionIntensity_val.Click
        Dim response As String = InputBox(My.Lang.InputValue, sender.text, My.Lang.ItMustBeNumerical)
        sender.Text = Math.Max(Math.Min(Val(response), W7_ColorizationGlassReflectionIntensity_bar.Maximum), W7_ColorizationGlassReflectionIntensity_bar.Minimum) : W7_ColorizationGlassReflectionIntensity_bar.Value = Val(sender.Text)
    End Sub

#End Region

#Region "Windows Vista"
    Private Sub WVista_ColorizationColor_pick_Click(sender As Object, e As EventArgs) Handles WVista_ColorizationColor_pick.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                My.CP.WindowsVista.ColorizationColor = sender.BackColor
                If My.PreviewStyle = WindowStyle.WVista Then ApplyColorsToElements(My.CP)
            End If
            Exit Sub
        End If

        Dim CList As New List(Of Control) From {
            sender,
            start,
            taskbar,
            XenonWindow1,
            XenonWindow2
        }

        Dim _Conditions As New Conditions With {.Win7 = True, .Color1 = True, .BackColor1 = True, .Win7LivePreview_Colorization = True}

        Dim C As Color = ColorPickerDlg.Pick(CList, _Conditions)

        My.CP.WindowsVista.ColorizationColor = Color.FromArgb(255, C)

        If My.PreviewStyle = WindowStyle.WVista Then ApplyColorsToElements(My.CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()
    End Sub

    Private Sub WVista_ColorizationColorBalance_bar_Scroll(sender As Object) Handles WVista_ColorizationColorBalance_bar.Scroll
        If _Shown Then
            WVista_ColorizationColorBalance_val.Text = sender.Value.ToString()
            My.CP.WindowsVista.Alpha = WVista_ColorizationColorBalance_bar.Value
            If My.PreviewStyle = WindowStyle.WVista Then ApplyColorsToElements(My.CP)
        End If
    End Sub

    Private Sub WVista_theme_classic_CheckedChanged(sender As Object) Handles WVista_theme_classic.CheckedChanged
        If WVista_theme_classic.Checked Then
            My.CP.WindowsVista.Theme = CP.Structures.Windows7.Themes.Classic
            If My.PreviewStyle = WindowStyle.WVista Then
                ApplyColorsToElements(My.CP)
                ApplyStylesToElements(My.CP, False)
            End If
        End If

    End Sub

    Private Sub WVista_theme_basic_CheckedChanged(sender As Object) Handles WVista_theme_basic.CheckedChanged
        If WVista_theme_basic.Checked Then
            My.CP.WindowsVista.Theme = CP.Structures.Windows7.Themes.Basic
            If My.PreviewStyle = WindowStyle.WVista Then
                ApplyColorsToElements(My.CP)
                ApplyStylesToElements(My.CP, False)
            End If
        End If
    End Sub

    Private Sub WVista_theme_aeroopaque_CheckedChanged(sender As Object) Handles WVista_theme_aeroopaque.CheckedChanged
        If WVista_theme_aeroopaque.Checked Then
            My.CP.WindowsVista.Theme = CP.Structures.Windows7.Themes.AeroOpaque
            If My.PreviewStyle = WindowStyle.WVista Then
                ApplyColorsToElements(My.CP)
                ApplyStylesToElements(My.CP, False)
            End If
        End If
    End Sub

    Private Sub WVista_theme_Vista_CheckedChanged(sender As Object) Handles WVista_theme_aero.CheckedChanged
        If WVista_theme_aero.Checked Then
            My.CP.WindowsVista.Theme = CP.Structures.Windows7.Themes.Aero
            If My.PreviewStyle = WindowStyle.WVista Then
                ApplyColorsToElements(My.CP)
                ApplyStylesToElements(My.CP, False)
            End If
        End If
    End Sub

    Private Sub WVista_ColorizationColorBalance_val_Click(sender As Object, e As EventArgs) Handles WVista_ColorizationColorBalance_val.Click
        Dim response As String = InputBox(My.Lang.InputValue, sender.text, My.Lang.ItMustBeNumerical)
        sender.Text = Math.Max(Math.Min(Val(response), WVista_ColorizationColorBalance_bar.Maximum), WVista_ColorizationColorBalance_bar.Minimum) : WVista_ColorizationColorBalance_bar.Value = Val(sender.Text)
    End Sub

#End Region

#Region "Windows XP"
    Private Sub WXP_Luna_Blue_CheckedChanged(sender As Object) Handles WXP_Luna_Blue.CheckedChanged
        If WXP_Luna_Blue.Checked Then
            My.CP.WindowsXP.Theme = CP.Structures.WindowsXP.Themes.LunaBlue
            If My.PreviewStyle = WindowStyle.WXP Then ApplyStylesToElements(My.CP, False)
        End If
    End Sub

    Private Sub WXP_Luna_OliveGreen_CheckedChanged(sender As Object) Handles WXP_Luna_OliveGreen.CheckedChanged
        If WXP_Luna_OliveGreen.Checked Then
            My.CP.WindowsXP.Theme = CP.Structures.WindowsXP.Themes.LunaOliveGreen
            If My.PreviewStyle = WindowStyle.WXP Then ApplyStylesToElements(My.CP, False)
        End If
    End Sub

    Private Sub WXP_Luna_Silver_CheckedChanged(sender As Object) Handles WXP_Luna_Silver.CheckedChanged
        If WXP_Luna_Silver.Checked Then
            My.CP.WindowsXP.Theme = CP.Structures.WindowsXP.Themes.LunaSilver
            If My.PreviewStyle = WindowStyle.WXP Then ApplyStylesToElements(My.CP, False)
        End If
    End Sub

    Private Sub WXP_CustomTheme_CheckedChanged(sender As Object) Handles WXP_CustomTheme.CheckedChanged
        If WXP_CustomTheme.Checked Then
            My.CP.WindowsXP.Theme = CP.Structures.WindowsXP.Themes.Custom
            If My.PreviewStyle = WindowStyle.WXP Then ApplyStylesToElements(My.CP, False)
        End If
    End Sub

    Private Sub WXP_Classic_CheckedChanged(sender As Object) Handles WXP_Classic.CheckedChanged
        If WXP_Classic.Checked Then
            My.CP.WindowsXP.Theme = CP.Structures.WindowsXP.Themes.Classic
            If My.PreviewStyle = WindowStyle.WXP Then ApplyStylesToElements(My.CP, False)
        End If
    End Sub

    Private Sub XenonTextBox1_TextChanged(sender As Object, e As EventArgs) Handles WXP_VS_textbox.TextChanged
        Dim theme As String = ""

        If IO.Path.GetExtension(WXP_VS_textbox.Text) = ".theme" Then
            theme = WXP_VS_textbox.Text

        ElseIf IO.Path.GetExtension(WXP_VS_textbox.Text) = ".msstyles" Then
            theme = My.PATH_appData & "\VisualStyles\Luna\custom.theme"
            IO.File.WriteAllText(My.PATH_appData & "\VisualStyles\Luna\custom.theme", String.Format("[VisualStyles]{1}Path={0}{1}ColorStyle=NormalColor{1}Size=NormalSize", WXP_VS_textbox.Text, vbCrLf))

        End If

        My.CP.WindowsXP.ThemeFile = WXP_VS_textbox.Text

        If IO.File.Exists(WXP_VS_textbox.Text) AndAlso IO.File.Exists(theme) And Not String.IsNullOrEmpty(theme) Then

            Dim vs As New VisualStyleFile(theme)

            WXP_VS_ColorsList.Items.Clear()

            Try
                For Each x In vs.ColorSchemes
                    WXP_VS_ColorsList.Items.Add(x.Name)
                Next
            Catch

            End Try

            If WXP_VS_ColorsList.Items.Count > 0 Then WXP_VS_ColorsList.SelectedIndex = 0

            If WXP_CustomTheme.Checked And My.PreviewStyle = WindowStyle.WXP Then ApplyStylesToElements(My.CP, False)
        End If
    End Sub

    Private Sub XenonButton30_Click(sender As Object, e As EventArgs) Handles WXP_VS_Browse.Click
        If OpenFileDialog2.ShowDialog = DialogResult.OK Then
            WXP_VS_textbox.Text = OpenFileDialog2.FileName
        End If
    End Sub

    Private Sub XenonComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles WXP_VS_ColorsList.SelectedIndexChanged
        If _Shown AndAlso WXP_CustomTheme.Checked Then
            My.CP.WindowsXP.ColorScheme = WXP_VS_ColorsList.SelectedItem
            If My.PreviewStyle = WindowStyle.WXP Then ApplyStylesToElements(My.CP, False)
        End If
    End Sub
#End Region

    Private Sub XenonButton4_Click(sender As Object, e As EventArgs) Handles apply_btn.Click
        Apply_Theme()
    End Sub

    Sub Apply_Theme(Optional [CP] As CP = Nothing)
        If [CP] Is Nothing Then [CP] = My.CP

        Cursor = Cursors.WaitCursor

        log_lbl.Visible = False
        log_lbl.Text = ""
        XenonButton8.Visible = False
        XenonButton14.Visible = False
        XenonButton22.Visible = False
        XenonButton25.Visible = False

        If My.Settings.ThemeLog.Enabled Then
            TablessControl1.SelectedIndex = TablessControl1.TabCount - 1
            TablessControl1.Refresh()
        End If

        [CP].Save(CP.CP_Type.Registry, "", If(My.Settings.ThemeLog.Enabled, TreeView1, Nothing))

        My.CP_Original = New CP(CP_Type.Registry)

        Cursor = Cursors.Default

        If My.Settings.ThemeApplyingBehavior.AutoRestartExplorer Then
            RestartExplorer(If(My.Settings.ThemeLog.Enabled, TreeView1, Nothing))
        Else
            If My.Settings.ThemeLog.Enabled Then CP.AddNode(TreeView1, My.Lang.NoDefResExplorer, "warning")
        End If

        If My.Settings.ThemeLog.Enabled Then CP.AddNode(TreeView1, String.Format("{0}: {1}", Now.ToLongTimeString, My.Lang.CP_AllDone), "info")

        If [CP].MetricsFonts.Enabled And GetWindowsScreenScalingFactor() > 100 Then CP.AddNode(TreeView1, String.Format("{0}", My.Lang.CP_MetricsHighDPIAlert), "info")

        log_lbl.Visible = True
        XenonButton8.Visible = True
        XenonButton22.Visible = True
        XenonButton25.Visible = True

        If Not My.Saving_Exceptions.Count = 0 Then
            log_lbl.Text = My.Lang.CP_ErrorHappened
            XenonButton14.Visible = True
        Else
            If My.Settings.ThemeLog.CountDown Then
                log_lbl.Text = String.Format(My.Lang.CP_LogWillClose, My.Settings.ThemeLog.CountDown_Seconds)
                elapsedSecs = 1
                Timer1.Enabled = True
                Timer1.Start()
            End If
        End If

    End Sub

    Private elapsedSecs As Integer = 0

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        log_lbl.Text = String.Format(My.Lang.CP_LogWillClose, My.Settings.ThemeLog.CountDown_Seconds - elapsedSecs)

        If elapsedSecs + 1 <= My.Settings.ThemeLog.CountDown_Seconds Then
            elapsedSecs += 1
        Else
            log_lbl.Text = ""
            Timer1.Enabled = False
            Timer1.Stop()
            SelectLeftPanelIndex()
        End If

    End Sub
    Private Sub XenonButton14_Click(sender As Object, e As EventArgs) Handles XenonButton14.Click
        log_lbl.Text = ""
        Timer1.Enabled = False
        Timer1.Stop()
        Saving_ex_list.ex_List = My.Saving_Exceptions
        Saving_ex_list.ShowDialog()
    End Sub

    Private Sub XenonButton8_Click(sender As Object, e As EventArgs) Handles XenonButton8.Click
        log_lbl.Text = ""
        Timer1.Enabled = False
        Timer1.Stop()
        SelectLeftPanelIndex()
    End Sub

    Private Sub XenonButton22_Click(sender As Object, e As EventArgs) Handles XenonButton22.Click
        log_lbl.Text = ""
        Timer1.Enabled = False
        Timer1.Stop()

        If SaveFileDialog3.ShowDialog = DialogResult.OK Then
            Dim sb As New StringBuilder
            sb.Clear()

            For Each N As TreeNode In TreeView1.Nodes
                sb.AppendLine(String.Format("[{0}]{2} {1}{3}", N.ImageKey, N.Text, vbTab, vbCrLf))
            Next

            IO.File.WriteAllText(SaveFileDialog3.FileName, sb.ToString)

        End If
    End Sub

    Private Sub Apply_btn_MouseEnter(sender As Object, e As EventArgs) Handles apply_btn.MouseEnter
        If My.Settings.ThemeApplyingBehavior.AutoRestartExplorer Then
            status_lbl.Text = My.Lang.ThisWillRestartExplorer
            status_lbl.ForeColor = If(GetDarkMode(), Color.Gold, Color.Gold.Dark(0.1))
        End If
    End Sub

    Private Sub Apply_btn_MouseLeave(sender As Object, e As EventArgs) Handles apply_btn.MouseLeave
        If My.Settings.ThemeApplyingBehavior.AutoRestartExplorer Then
            status_lbl.Text = ""
            status_lbl.ForeColor = If(GetDarkMode(), Color.White, Color.Black)
        End If
    End Sub

    Private Sub XenonButton19_MouseEnter(sender As Object, e As EventArgs) Handles XenonButton19.MouseEnter
        status_lbl.Text = My.Lang.ThisWillRestartExplorer
        status_lbl.ForeColor = If(GetDarkMode(), Color.Gold, Color.Gold.Dark(0.1))
    End Sub

    Private Sub XenonButton19_MouseLeave(sender As Object, e As EventArgs) Handles XenonButton19.MouseLeave
        status_lbl.Text = ""
        status_lbl.ForeColor = If(GetDarkMode(), Color.White, Color.Black)
    End Sub

#Region "Drag And Drop"
    Dim wpth_or_wpsf As Boolean = True
    Dim DragAccepted As Boolean
    Dim Dropped As Boolean

    Private Sub Me_DragEnter(ByVal sender As Object, ByVal e As DragEventArgs) Handles Me.DragEnter, previewContainer.DragEnter, tabs_preview.DragEnter, TablessControl1.DragEnter

        Dim files() As String = e.Data.GetData(DataFormats.FileDrop)
        Dropped = False

        If IO.Path.GetExtension(files(0)).ToLower = ".wpth" Then
            wpth_or_wpsf = True

            If _Converter.FetchFile(files(0)) = Converter_CP.WP_Format.JSON Then
                e.Effect = DragDropEffects.Copy

                If My.Settings.FileTypeManagement.DragAndDropPreview Then
                    DragAccepted = True
                    My.CP_BeforeDrag = My.CP.Clone
                    DragPreviewer.Location = New Point(e.X + 15, e.Y + 15)
                    DragPreviewer.File = files(0)
                    DragPreviewer.Show()
                End If
            ElseIf _Converter.FetchFile(files(0)) = Converter_CP.WP_Format.WPTH Then
                DragAccepted = True
                e.Effect = DragDropEffects.Copy

            Else
                DragAccepted = False
                e.Effect = DragDropEffects.None

            End If

        ElseIf IO.Path.GetExtension(files(0)).ToLower = ".wpsf" Then
            wpth_or_wpsf = False
            DragAccepted = True
            e.Effect = DragDropEffects.Copy
        Else
            DragAccepted = False
            e.Effect = DragDropEffects.None
        End If

    End Sub

    Private Sub MainFrm_DragLeave(sender As Object, e As EventArgs) Handles Me.DragLeave
        Dropped = False
        If DragAccepted Then
            If My.Settings.FileTypeManagement.DragAndDropPreview Then DragPreviewer.Close()
            My.CP = My.CP_BeforeDrag.Clone
        End If
    End Sub

    Private Sub MainFrm_DragOver(sender As Object, e As DragEventArgs) Handles Me.DragOver, previewContainer.DragOver, tabs_preview.DragOver, TablessControl1.DragOver
        If DragAccepted And My.Settings.FileTypeManagement.DragAndDropPreview Then DragPreviewer.Location = New Point(e.X + 15, e.Y + 15)
        Dropped = Me.Bounds.Contains(New Point(e.X, e.Y))
    End Sub

    Private Sub MainFrm_DragDrop(sender As Object, e As DragEventArgs) Handles Me.DragDrop, previewContainer.DragDrop, tabs_preview.DragDrop, TablessControl1.DragDrop
        If Dropped And DragAccepted Then
            Dim files() As String = e.Data.GetData(DataFormats.FileDrop)
            If wpth_or_wpsf Then
                If My.Settings.FileTypeManagement.DragAndDropPreview Then DragPreviewer.Close()

                My.CP = New CP(CP.CP_Type.File, files(0))
                tabs_preview.Visible = False
                ApplyStylesToElements(My.CP, False)
                ApplyCPValues(My.CP)
                ApplyColorsToElements(My.CP)
                tabs_preview.Visible = True
            Else
                SettingsX._External = True
                SettingsX._File = files(0)
                SettingsX.ShowDialog()
            End If
        End If
    End Sub
#End Region

    Private Sub XenonButton7_Click(sender As Object, e As EventArgs) Handles XenonButton7.Click
        If Not IO.File.Exists(SaveFileDialog1.FileName) Then
            If SaveFileDialog1.ShowDialog = DialogResult.OK Then
                My.CP.Save(CP.CP_Type.File, SaveFileDialog1.FileNames(0))
            End If
        Else
            My.CP.Save(CP.CP_Type.File, SaveFileDialog1.FileNames(0))
        End If
    End Sub

    Private Sub XenonButton2_Click(sender As Object, e As EventArgs) Handles XenonButton2.Click
        If OpenFileDialog1.ShowDialog = DialogResult.OK Then

            ComplexSave.GetResponse(SaveFileDialog1, Sub() Apply_Theme(), Sub() Apply_Theme(My.CP_FirstTime), Sub() Apply_Theme(CP_Defaults.GetDefault))

            SaveFileDialog1.FileName = OpenFileDialog1.FileName
            My.CP = New CP(CP.CP_Type.File, OpenFileDialog1.FileName)
            My.CP_Original = My.CP.Clone

            ApplyStylesToElements(My.CP, False)
            ApplyCPValues(My.CP)
            ApplyColorsToElements(My.CP)
        End If
    End Sub

    Private Sub XenonButton9_Click(sender As Object, e As EventArgs) Handles XenonButton9.Click
        If SaveFileDialog1.ShowDialog = DialogResult.OK Then
            My.CP.Save(CP.CP_Type.File, SaveFileDialog1.FileNames(0))
        End If
    End Sub

    Private Sub XenonButton3_Click(sender As Object, e As EventArgs) Handles XenonButton3.Click

        ComplexSave.GetResponse(SaveFileDialog1, Sub() Apply_Theme(), Sub() Apply_Theme(My.CP_FirstTime), Sub() Apply_Theme(CP_Defaults.GetDefault))

        My.CP = New CP(CP.CP_Type.Registry)
        My.CP_Original = My.CP.Clone
        SaveFileDialog1.FileName = Nothing
        ApplyStylesToElements(My.CP, False)
        ApplyCPValues(My.CP)
        ApplyColorsToElements(My.CP)
    End Sub

    Private Sub XenonButton10_Click(sender As Object, e As EventArgs) Handles XenonButton10.Click, author_lbl.DoubleClick, themename_lbl.DoubleClick
        EditInfo.ShowDialog()
    End Sub

    Private Sub XenonButton12_Click(sender As Object, e As EventArgs) Handles XenonButton12.Click
        About.ShowDialog()
    End Sub

    Private Sub XenonButton5_Click(sender As Object, e As EventArgs) Handles XenonButton5.Click
        Updates.ShowDialog()
        XenonButton5.Image = My.Resources.Update
    End Sub

    Private Sub XenonButton11_Click(sender As Object, e As EventArgs) Handles XenonButton11.Click
        SettingsX.ShowDialog()
    End Sub

    Private Sub XenonButton4_Click_1(sender As Object, e As EventArgs) Handles XenonButton4.Click
        Win32UI.ShowDialog()
    End Sub

    Private Sub XenonButton6_Click(sender As Object, e As EventArgs) Handles XenonButton6.Click
        Whatsnew.ShowDialog()
    End Sub

    Private Sub XenonButton16_Click(sender As Object, e As EventArgs) Handles XenonButton16.Click
        If My.PreviewStyle = WindowStyle.W11 Or My.PreviewStyle = WindowStyle.W10 Then
            LogonUI.ShowDialog()
        ElseIf My.PreviewStyle = WindowStyle.W8 Or My.PreviewStyle = WindowStyle.W7 Then
            LogonUI7.ShowDialog()
        ElseIf My.PreviewStyle = WindowStyle.WXP Then
            LogonUIXP.ShowDialog()
        ElseIf My.PreviewStyle = WindowStyle.WVista Then
            MsgBox(My.Lang.VistaLogonNotSupported, MsgBoxStyle.Exclamation)
        Else
            LogonUI.ShowDialog()
        End If
    End Sub

    Private Sub XenonButton15_Click(sender As Object, e As EventArgs) Handles XenonButton15.Click
        Update_Wallpaper_Preview()
    End Sub

    Private Sub XenonButton13_Click(sender As Object, e As EventArgs) Handles XenonButton13.Click
        Me.Close()
    End Sub

    Private Sub XenonButton1_Click_1(sender As Object, e As EventArgs) Handles XenonButton1.Click
        If SaveFileDialog2.ShowDialog = DialogResult.OK Then
            tabs_preview.ToBitmap.Save(SaveFileDialog2.FileName)
        End If
    End Sub

    Private Sub XenonButton17_Click(sender As Object, e As EventArgs) Handles XenonButton17.Click
        My.CP = My.CP_Original.Clone
        ApplyStylesToElements(My.CP, False)
        ApplyCPValues(My.CP)
        ApplyColorsToElements(My.CP)
    End Sub

    Private Sub XenonButton18_Click(sender As Object, e As EventArgs) Handles XenonButton18.Click
        My.CP = My.CP_FirstTime.Clone
        ApplyStylesToElements(My.CP, False)
        ApplyCPValues(My.CP)
        ApplyColorsToElements(My.CP)
    End Sub

    Private Sub XenonButton19_Click(sender As Object, e As EventArgs) Handles XenonButton19.Click
        RestartExplorer()
    End Sub

    Private Sub XenonButton20_Click(sender As Object, e As EventArgs) Handles XenonButton20.Click
        ComplexSave.GetResponse(SaveFileDialog1, Sub() Apply_Theme(), Sub() Apply_Theme(My.CP_FirstTime), Sub() Apply_Theme(CP_Defaults.GetDefault))

        My.CP = CP_Defaults.GetDefault.Clone
        SaveFileDialog1.FileName = Nothing
        ApplyCPValues(My.CP)
        ApplyStylesToElements(My.CP, False)
        ApplyColorsToElements(My.CP)
    End Sub

    Private Sub XenonButton21_Click(sender As Object, e As EventArgs) Handles XenonButton21.Click
        CursorsStudio.ShowDialog()
    End Sub

    Private Sub XenonButton23_Click(sender As Object, e As EventArgs) Handles XenonButton23.Click
        If XenonButton23.Text.ToLower = My.Lang.Hide.ToLower Then
            tabs_preview.Visible = False
            XenonButton23.Text = My.Lang.Show
        Else
            tabs_preview.Visible = True
            XenonButton23.Text = My.Lang.Hide
        End If
    End Sub

    Private Sub XenonButton24_Click(sender As Object, e As EventArgs) Handles XenonButton24.Click
        TerminalsDashboard.ShowDialog()
    End Sub

    Private Sub MainFrm_ResizeBegin(sender As Object, e As EventArgs) Handles Me.ResizeBegin
        SuspendLayout()
        'previewContainer.Visible = False
        'TablessControl1.Visible = False
    End Sub

    Private Sub MainFrm_ResizeEnd(sender As Object, e As EventArgs) Handles Me.ResizeEnd
        ResumeLayout()
        'previewContainer.Visible = True
        'TablessControl1.Visible = True
    End Sub

    Private Sub XenonButton27_Click(sender As Object, e As EventArgs) Handles XenonButton27.Click
        Metrics_Fonts.ShowDialog()
    End Sub

    Sub Select_Preview_Version()

        My.Animator.HideSync(TablessControl1, True)
        My.Animator.HideSync(tabs_preview, True)

        UpdateLegends()

        ApplyColorsToElements(My.CP)
        ApplyStylesToElements(My.CP)
        ApplyDefaultCPValues()
        SelectLeftPanelIndex()

        If My.PreviewStyle = WindowStyle.W11 Then
            XenonButton20.Image = My.Resources.add_win11
        ElseIf My.PreviewStyle = WindowStyle.W10 Then
            XenonButton20.Image = My.Resources.add_win10
        ElseIf My.PreviewStyle = WindowStyle.W8 Then
            XenonButton20.Image = My.Resources.add_win8
        ElseIf My.PreviewStyle = WindowStyle.W7 Then
            XenonButton20.Image = My.Resources.add_win7
        ElseIf My.PreviewStyle = WindowStyle.WVista Then
            XenonButton20.Image = My.Resources.add_winvista
        ElseIf My.PreviewStyle = WindowStyle.WXP Then
            XenonButton20.Image = My.Resources.add_winxp
        Else
            XenonButton20.Image = My.Resources.add_win11
        End If


        My.Animator.ShowSync(TablessControl1, True)
        My.Animator.ShowSync(tabs_preview, True)

    End Sub

    Private Sub Select_W11_CheckedChanged(sender As Object) Handles Select_W11.CheckedChanged
        If _Shown And Select_W11.Checked Then
            My.PreviewStyle = WindowStyle.W11
            Select_Preview_Version()
        End If
    End Sub

    Private Sub Select_W10_CheckedChanged(sender As Object) Handles Select_W10.CheckedChanged
        If _Shown And Select_W10.Checked Then
            My.PreviewStyle = WindowStyle.W10
            Select_Preview_Version()
        End If
    End Sub

    Private Sub Select_W8_CheckedChanged(sender As Object) Handles Select_W8.CheckedChanged
        If _Shown And Select_W8.Checked Then
            My.PreviewStyle = WindowStyle.W8
            Select_Preview_Version()
        End If
    End Sub

    Private Sub Select_W7_CheckedChanged(sender As Object) Handles Select_W7.CheckedChanged
        If _Shown And Select_W7.Checked Then
            My.PreviewStyle = WindowStyle.W7
            Select_Preview_Version()
        End If
    End Sub

    Private Sub Select_WVista_CheckedChanged(sender As Object) Handles Select_WVista.CheckedChanged
        If _Shown And Select_WVista.Checked Then
            My.PreviewStyle = WindowStyle.WVista
            Select_Preview_Version()
        End If
    End Sub

    Private Sub XenonButton29_Click(sender As Object, e As EventArgs) Handles XenonButton29.Click
        WinEffecter.ShowDialog()
    End Sub

    Private Sub XenonButton32_Click(sender As Object, e As EventArgs) Handles XenonButton32.Click
        If My.PreviewStyle <> WindowStyle.WXP AndAlso My.PreviewStyle <> WindowStyle.WVista Then
            AltTabEditor.ShowDialog()
        Else
            If My.PreviewStyle = WindowStyle.WXP Then MsgBox(String.Format(My.Lang.AltTab_Unsupported, My.Lang.OS_WinXP), MsgBoxStyle.Exclamation)
            If My.PreviewStyle = WindowStyle.WVista Then MsgBox(String.Format(My.Lang.AltTab_Unsupported, My.Lang.OS_WinVista), MsgBoxStyle.Exclamation)
        End If

    End Sub

    Private Sub W10_TB_Blur_CheckedChanged(sender As Object, e As EventArgs) Handles W10_TB_Blur.CheckedChanged
        If _Shown Then
            My.CP.Windows10.TB_Blur = sender.Checked
            If My.PreviewStyle = WindowStyle.W10 Then ApplyColorsToElements(My.CP)
        End If
    End Sub
    Private Sub Select_WXP_CheckedChanged(sender As Object) Handles Select_WXP.CheckedChanged
        If _Shown And Select_WXP.Checked Then
            My.PreviewStyle = WindowStyle.WXP
            Select_Preview_Version()
        End If
    End Sub

    Private Sub XenonButton25_Click(sender As Object, e As EventArgs) Handles XenonButton25.Click
        log_lbl.Text = ""
        Timer1.Enabled = False
        Timer1.Stop()
    End Sub

    Private Sub XenonButton33_Click(sender As Object, e As EventArgs) Handles XenonButton33.Click
        ScreenSaver_Editor.ShowDialog()
    End Sub

    Private Sub XenonButton34_Click(sender As Object, e As EventArgs) Handles XenonButton34.Click
        Sounds_Editor.ShowDialog()
    End Sub

    Private Sub XenonButton35_Click(sender As Object, e As EventArgs) Handles XenonButton35.Click
        If My.PreviewStyle = WindowStyle.W11 Then
            Wallpaper_Editor.WT = My.CP.WallpaperTone_W11
        ElseIf My.PreviewStyle = WindowStyle.W10 Then
            Wallpaper_Editor.WT = My.CP.WallpaperTone_W10
        ElseIf My.PreviewStyle = WindowStyle.W8 Then
            Wallpaper_Editor.WT = My.CP.WallpaperTone_W8
        ElseIf My.PreviewStyle = WindowStyle.W7 Then
            Wallpaper_Editor.WT = My.CP.WallpaperTone_W7
        ElseIf My.PreviewStyle = WindowStyle.WVista Then
            Wallpaper_Editor.WT = My.CP.WallpaperTone_WVista
        ElseIf My.PreviewStyle = WindowStyle.WXP Then
            Wallpaper_Editor.WT = My.CP.WallpaperTone_WXP
        Else
            Wallpaper_Editor.WT = My.CP.WallpaperTone_W11
        End If

        Wallpaper_Editor.ShowDialog()
    End Sub

    Private Sub XenonButton26_Click(sender As Object, e As EventArgs) Handles XenonButton26.Click
        ApplicationThemer.ShowDialog()
    End Sub

    Private Sub XenonButton36_Click(sender As Object, e As EventArgs) Handles XenonButton36.Click
        Converter_Form.ShowDialog()
    End Sub

    Private Sub XenonButton28_Click(sender As Object, e As EventArgs) Handles XenonButton28.Click

        If MsgBox(My.Lang.LogoffQuestion, MsgBoxStyle.Question + MsgBoxStyle.YesNo, My.Lang.LogoffAlert1, "", "", "", "", My.Lang.LogoffAlert2, Ookii.Dialogs.WinForms.TaskDialogIcon.Information) = MsgBoxResult.Yes Then
            LoggingOff = True
            Kernel32.Wow64DisableWow64FsRedirection(IntPtr.Zero)
            Shell(My.PATH_System32 & "\logoff.exe", AppWinStyle.Hide)
        End If

    End Sub

    Private Sub XenonButton28_MouseEnter(sender As Object, e As EventArgs) Handles XenonButton28.MouseEnter
        status_lbl.Text = My.Lang.LogoffNotice
        status_lbl.ForeColor = If(GetDarkMode(), Color.Gold, Color.Gold.Dark(0.1))
    End Sub

    Private Sub XenonButton28_MouseLeave(sender As Object, e As EventArgs) Handles XenonButton28.MouseLeave
        status_lbl.Text = ""
        status_lbl.ForeColor = If(GetDarkMode(), Color.White, Color.Black)
    End Sub

    Private Sub XenonButton38_Click(sender As Object, e As EventArgs) Handles XenonButton38.Click
        'Copycat from Windows 11 colors
        tabs_preview.Visible = False
        My.CP.Windows10 = My.CP.Windows11.Clone
        ApplyCPValues(My.CP)
        ApplyColorsToElements(My.CP)
        tabs_preview.Visible = True
    End Sub

    Private Sub XenonButton37_Click(sender As Object, e As EventArgs) Handles XenonButton37.Click
        'Copycat from Windows 10 colors
        tabs_preview.Visible = False
        My.CP.Windows11 = My.CP.Windows10.Clone
        ApplyCPValues(My.CP)
        ApplyColorsToElements(My.CP)
        tabs_preview.Visible = True
    End Sub

    Private Sub XenonButton39_Click(sender As Object, e As EventArgs) Handles XenonButton39.Click
        Process.Start(My.Resources.Link_Wiki)
    End Sub

    Private Sub XenonButton30_Click_1(sender As Object, e As EventArgs) Handles XenonButton30.Click
        MsgBox(My.Lang.Win11ColorsDescTip, MsgBoxStyle.Information, My.Lang.Win11ColorsDescTip2)
    End Sub

    Private Sub XenonButton31_Click(sender As Object, e As EventArgs) Handles XenonButton31.Click
        If My.WXP Then
            If MsgBox(String.Format(My.Lang.Store_WontWork_Protocol, My.Lang.OS_WinXP), MsgBoxStyle.Critical + MsgBoxStyle.YesNo) <> MsgBoxResult.Yes Then Exit Sub
        ElseIf My.WVista Then
            If MsgBox(String.Format(My.Lang.Store_WontWork_Protocol, My.Lang.OS_WinVista), MsgBoxStyle.Critical + MsgBoxStyle.YesNo) <> MsgBoxResult.Yes Then Exit Sub
        End If

        If My.Settings.Language.Enabled AndAlso IO.File.Exists(My.Settings.Language.File) Then My.Lang.LoadLanguageFromJSON(My.Settings.Language.File, Store)
        Store.Show()
    End Sub
End Class


