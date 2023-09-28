Imports System.ComponentModel
Imports System.Net
Imports System.Text
Imports Devcorp.Controls.VisualStyles
Imports WinPaletter.Core
Imports WinPaletter.CP
Imports WinPaletter.NativeMethods
Imports WinPaletter.PreviewHelpers

Public Class MainFrm
    Private _Shown As Boolean = False
    Dim RaiseUpdate As Boolean = False
    Dim ver As String = ""
    Dim StableInt, BetaInt, UpdateChannel As Integer
    Dim ChannelFixer As Integer
    Dim Updates_ls As New List(Of String)
    Private LoggingOff As Boolean = False
    Private ReadOnly _Converter As New Converter
    Private elapsedSecs As Integer = 0
    Private OldWidth As Integer

#Region "Preview Subs"
    Sub ApplyColorsToElements([CP] As CP)
        ApplyWinElementsColors([CP], My.PreviewStyle, True, taskbar, start, ActionCenter, setting_icon_preview, Label8, lnk_preview)
        ApplyWindowStyles([CP], My.PreviewStyle, Window1, Window2, W81_start, W81_logonui)
    End Sub
    Sub ApplyStylesToElements([CP] As CP, Optional AnimateThePreview As Boolean = True)
        Dim ItWasVisible As Boolean = tabs_preview.Visible

        If AnimateThePreview And ItWasVisible Then
            If _Shown Then
                If tabs_preview.Visible Then My.Animator.HideSync(tabs_preview)
            Else
                tabs_preview.Visible = False
            End If
        End If

        My.Wallpaper = My.Application.FetchSuitableWallpaper([CP], My.PreviewStyle)
        pnl_preview.BackgroundImage = My.Wallpaper
        pnl_preview_classic.BackgroundImage = My.Wallpaper

        ApplyWinElementsStyle([CP], My.PreviewStyle, taskbar, start, ActionCenter,
                           Window1, Window2, Panel3, lnk_preview,
                           ClassicTaskbar, ButtonR2, ButtonR3, ButtonR4, ClassicWindow1, ClassicWindow2,
                           WXP_VS_ReplaceColors.Checked, WXP_VS_ReplaceMetrics.Checked, WXP_VS_ReplaceFonts.Checked)

        Button23.Visible = (My.PreviewStyle = WindowStyle.W7)

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
        ApplyStyle(Me)

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

        Select Case [CP].Windows81.Theme
            Case CP.Structures.Windows7.Themes.Aero
                W81_theme_aero.Checked = True

            Case CP.Structures.Windows7.Themes.AeroLite
                W81_theme_aerolite.Checked = True
        End Select
        W81_ColorizationColor_pick.BackColor = [CP].Windows81.ColorizationColor
        W81_ColorizationBalance_bar.Value = [CP].Windows81.ColorizationColorBalance
        W81_ColorizationBalance_val.Text = [CP].Windows81.ColorizationColorBalance
        W81_start_pick.BackColor = [CP].Windows81.StartColor
        W81_accent_pick.BackColor = [CP].Windows81.AccentColor
        W81_personalcls_background_pick.BackColor = [CP].Windows81.PersonalColors_Background
        W81_personalcolor_accent_pick.BackColor = [CP].Windows81.PersonalColors_Accent

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

        ApplyMetroStartToButton([CP], W81_start)
        ApplyBackLogonUI([CP], W81_logonui)
    End Sub
    Sub ApplyDefaultCPValues()
        Dim DefCP As CP

        If My.W11 Then
            DefCP = New CP_Defaults().Default_Windows11
        ElseIf My.W10 Then
            DefCP = New CP_Defaults().Default_Windows10
        ElseIf My.W81 Then
            DefCP = New CP_Defaults().Default_Windows81
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

        W81_ColorizationColor_pick.DefaultColor = DefCP.Windows7.ColorizationColor
        W81_start_pick.DefaultColor = DefCP.Windows81.StartColor
        W81_accent_pick.DefaultColor = DefCP.Windows81.AccentColor
        W81_personalcls_background_pick.DefaultColor = DefCP.Windows81.PersonalColors_Background
        W81_personalcolor_accent_pick.DefaultColor = DefCP.Windows81.PersonalColors_Accent

        W7_ColorizationColor_pick.DefaultColor = DefCP.Windows7.ColorizationColor
        W7_ColorizationAfterglow_pick.DefaultColor = DefCP.Windows7.ColorizationAfterglow

        WVista_ColorizationColor_pick.DefaultColor = DefCP.WindowsVista.ColorizationColor

        DefCP.Dispose()
    End Sub
    Public Sub Update_Wallpaper_Preview()
        Cursor = Cursors.AppStarting
        My.Wallpaper = My.Application.FetchSuitableWallpaper(My.CP, My.PreviewStyle)
        pnl_preview.BackgroundImage = My.Wallpaper
        pnl_preview_classic.BackgroundImage = My.Wallpaper
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
        ElseIf My.PreviewStyle = WindowStyle.W81 Then
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

    Sub UpdateHint(sender As Object, e As EventArgs)
        status_lbl.Text = sender.Tag
    End Sub

    Sub EraseHint(sender As Object, e As EventArgs)
        status_lbl.Text = ""
    End Sub

    Sub UpdateHint_Dashboard(Sender As Object, e As EventArgs)
        status_lbl.Text = Sender.Tag
    End Sub

    Sub EraseHint_Dashboard(sender As Object, e As EventArgs)
        status_lbl.Text = ""
    End Sub

#End Region

    Sub AutoUpdatesCheck()
        If My.WXP OrElse My.WVista Then Exit Sub

        StableInt = 0 : BetaInt = 0 : UpdateChannel = 0 : ChannelFixer = 0
        If My.Settings.Updates.Channel = WPSettings.Structures.Updates.Channels.Stable Then ChannelFixer = 0
        If My.Settings.Updates.Channel = WPSettings.Structures.Updates.Channels.Beta Then ChannelFixer = 1
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
            Button5.Image = My.Resources.Update_Dot
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

    Protected Overrides Sub OnDragOver(e As DragEventArgs)
        If TypeOf e.Data.GetData(GetType(UI.Controllers.ColorItem).FullName) Is UI.Controllers.ColorItem Then
            Focus()
            BringToFront()
        Else
            Exit Sub
        End If

        MyBase.OnDragOver(e)
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
        Select_W81.Image = My.Resources.Native8
        Select_W7.Image = My.Resources.Native7
        Select_WVista.Image = My.Resources.NativeVista
        Select_WXP.Image = My.Resources.NativeXP
        If Not My.isElevated Then apply_btn.Image = My.Resources.WP_Admin

        If My.PreviewStyle = WindowStyle.W11 Then
            TablessControl1.SelectedIndex = 0
            Button20.Image = My.Resources.add_win11
            Select_W11.Checked = True

        ElseIf My.PreviewStyle = WindowStyle.W10 Then
            TablessControl1.SelectedIndex = 1
            Button20.Image = My.Resources.add_win10
            Select_W10.Checked = True

        ElseIf My.PreviewStyle = WindowStyle.W81 Then
            TablessControl1.SelectedIndex = 2
            Button20.Image = My.Resources.add_win8
            Select_W81.Checked = True

        ElseIf My.PreviewStyle = WindowStyle.W7 Then
            TablessControl1.SelectedIndex = 3
            Button20.Image = My.Resources.add_win7
            Select_W7.Checked = True

        ElseIf My.PreviewStyle = WindowStyle.WVista Then
            TablessControl1.SelectedIndex = 4
            Button20.Image = My.Resources.add_winvista
            Select_WVista.Checked = True

        ElseIf My.PreviewStyle = WindowStyle.WXP Then
            TablessControl1.SelectedIndex = 5
            Button20.Image = My.Resources.add_winxp
            Select_WXP.Checked = True

        Else
            TablessControl1.SelectedIndex = 0
            Button20.Image = My.Resources.add_win11
            Select_W11.Checked = True
        End If

        LoadLanguage
        ApplyStyle(Me)
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

        For Each btn As UI.WP.Button In MainToolbar.Controls.OfType(Of UI.WP.Button)
            AddHandler btn.MouseEnter, AddressOf UpdateHint
            AddHandler btn.Enter, AddressOf UpdateHint
            AddHandler btn.MouseLeave, AddressOf EraseHint
            AddHandler btn.Leave, AddressOf EraseHint
        Next

        For Each btn As UI.WP.Button In GroupBox3.Controls.OfType(Of UI.WP.Button)
            AddHandler btn.MouseEnter, AddressOf UpdateHint_Dashboard
            AddHandler btn.Enter, AddressOf UpdateHint_Dashboard
            AddHandler btn.MouseLeave, AddressOf EraseHint_Dashboard
            AddHandler btn.Leave, AddressOf EraseHint_Dashboard
        Next

        For Each btn As UI.WP.RadioImage In previewContainer.Controls.OfType(Of UI.WP.RadioImage)
            AddHandler btn.MouseEnter, AddressOf UpdateHint
            AddHandler btn.Enter, AddressOf UpdateHint
            AddHandler btn.MouseLeave, AddressOf EraseHint
            AddHandler btn.Leave, AddressOf EraseHint
        Next

        If My.Settings.Updates.AutoCheck Then AutoUpdatesCheck()

        If My.Application.ShowWhatsNew Then Whatsnew.ShowDialog()
    End Sub

    Private Sub MainFrm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        For Each btn As UI.WP.Button In MainToolbar.Controls.OfType(Of UI.WP.Button)
            RemoveHandler btn.MouseEnter, AddressOf UpdateHint
            RemoveHandler btn.Enter, AddressOf UpdateHint
            RemoveHandler btn.MouseLeave, AddressOf EraseHint
            RemoveHandler btn.Leave, AddressOf EraseHint
        Next

        For Each btn As UI.WP.Button In GroupBox3.Controls.OfType(Of UI.WP.Button)
            RemoveHandler btn.MouseEnter, AddressOf UpdateHint_Dashboard
            RemoveHandler btn.Enter, AddressOf UpdateHint_Dashboard
            RemoveHandler btn.MouseLeave, AddressOf EraseHint_Dashboard
            RemoveHandler btn.Leave, AddressOf EraseHint_Dashboard
        Next

        For Each btn As UI.WP.RadioImage In previewContainer.Controls.OfType(Of UI.WP.RadioImage)
            RemoveHandler btn.MouseEnter, AddressOf UpdateHint
            RemoveHandler btn.Enter, AddressOf UpdateHint
            RemoveHandler btn.MouseLeave, AddressOf EraseHint
            RemoveHandler btn.Leave, AddressOf EraseHint
        Next
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

        Dim old As New WPSettings(WPSettings.Mode.Registry)
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
                        If (My.W7 Or My.W8 Or My.W81) And My.Settings.Miscellaneous.Win7LivePreview Then RefreshDWM(My.CP_Original)
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
    Private Sub W11_pick_DragDrop(sender As Object, e As DragEventArgs) Handles W11_ActiveTitlebar_pick.DragDrop, W11_InactiveTitlebar_pick.DragDrop, W11_TaskbarFrontAndFoldersOnStart_pick.DragDrop, W11_Color_Index0.DragDrop, W11_Color_Index1.DragDrop, W11_Color_Index2.DragDrop, W11_Color_Index3.DragDrop, W11_Color_Index4.DragDrop, W11_Color_Index5.DragDrop, W11_Color_Index6.DragDrop, W11_Color_Index7.DragDrop
        If CType(sender, UI.Controllers.ColorItem).AllowDrop AndAlso My.PreviewStyle = WindowStyle.W11 Then
            My.CP.Windows11.Titlebar_Active = W11_ActiveTitlebar_pick.BackColor
            My.CP.Windows11.Titlebar_Inactive = W11_InactiveTitlebar_pick.BackColor
            My.CP.Windows11.StartMenu_Accent = W11_Color_Index5.BackColor
            My.CP.Windows11.Color_Index2 = W11_Color_Index4.BackColor
            My.CP.Windows11.Color_Index6 = W11_Color_Index6.BackColor
            My.CP.Windows11.Color_Index1 = W11_Color_Index1.BackColor
            My.CP.Windows11.Color_Index4 = W11_Color_Index2.BackColor
            My.CP.Windows11.Color_Index5 = W11_TaskbarFrontAndFoldersOnStart_pick.BackColor
            My.CP.Windows11.Color_Index0 = W11_Color_Index0.BackColor
            My.CP.Windows11.Color_Index3 = W11_Color_Index3.BackColor
            My.CP.Windows11.Color_Index7 = W11_Color_Index7.BackColor
            ApplyColorsToElements(My.CP)
        End If
    End Sub

    Private Sub W11_ActiveTitlebar_pick_Click(sender As Object, e As EventArgs) Handles W11_ActiveTitlebar_pick.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                My.CP.Windows11.Titlebar_Active = sender.BackColor
                If My.PreviewStyle = WindowStyle.W11 Then ApplyColorsToElements(My.CP)
            End If
            Exit Sub
        End If

        Dim CList As New List(Of Control) From {sender, Window1}

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

        Dim CList As New List(Of Control) From {sender, Window2}

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

    Private Sub W11_Button8_Click_1(sender As Object, e As EventArgs) Handles W11_Button8.Click
        MsgBox(My.Lang.TitlebarColorNotice, MsgBoxStyle.Information)
    End Sub

    Private Sub Button38_Click(sender As Object, e As EventArgs) Handles Button38.Click
        'Copycat from Windows 11 colors
        tabs_preview.Visible = False
        My.CP.Windows10 = My.CP.Windows11.Clone
        ApplyCPValues(My.CP)
        ApplyColorsToElements(My.CP)
        tabs_preview.Visible = True
    End Sub
#End Region

#Region "Windows 10"
    Private Sub W10_pick_DragDrop(sender As Object, e As DragEventArgs) Handles W10_ActiveTitlebar_pick.DragDrop, W10_InactiveTitlebar_pick.DragDrop, W10_TaskbarFrontAndFoldersOnStart_pick.DragDrop, W10_Color_Index0.DragDrop, W10_Color_Index1.DragDrop, W10_Color_Index2.DragDrop, W10_Color_Index3.DragDrop, W10_Color_Index4.DragDrop, W10_Color_Index5.DragDrop, W10_Color_Index6.DragDrop, W10_Color_Index7.DragDrop
        If CType(sender, UI.Controllers.ColorItem).AllowDrop AndAlso My.PreviewStyle = WindowStyle.W10 Then
            My.CP.Windows10.Titlebar_Active = W10_ActiveTitlebar_pick.BackColor
            My.CP.Windows10.Titlebar_Inactive = W10_InactiveTitlebar_pick.BackColor
            My.CP.Windows10.StartMenu_Accent = W10_Color_Index5.BackColor
            My.CP.Windows10.Color_Index2 = W10_Color_Index4.BackColor
            My.CP.Windows10.Color_Index6 = W10_Color_Index6.BackColor
            My.CP.Windows10.Color_Index1 = W10_Color_Index1.BackColor
            My.CP.Windows10.Color_Index4 = W10_Color_Index2.BackColor
            My.CP.Windows10.Color_Index5 = W10_TaskbarFrontAndFoldersOnStart_pick.BackColor
            My.CP.Windows10.Color_Index0 = W10_Color_Index0.BackColor
            My.CP.Windows10.Color_Index3 = W10_Color_Index3.BackColor
            My.CP.Windows10.Color_Index7 = W10_Color_Index7.BackColor
            ApplyColorsToElements(My.CP)
        End If
    End Sub

    Private Sub W10_ActiveTitlebar_pick_Click(sender As Object, e As EventArgs) Handles W10_ActiveTitlebar_pick.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                My.CP.Windows10.Titlebar_Active = sender.BackColor
                If My.PreviewStyle = WindowStyle.W10 Then ApplyColorsToElements(My.CP)
            End If
            Exit Sub
        End If

        Dim CList As New List(Of Control) From {sender, Window1}

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

        Dim CList As New List(Of Control) From {sender, Window2}

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
            'ColorControls_List.Add(start) ''Hamburger
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
            'ColorControls_List.Add(taskbar) 'Start Icon Hover
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
                        CList.Add(taskbar)  'Start ButtonR Hover
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

    Private Sub W10_Button8_Click_1(sender As Object, e As EventArgs) Handles W10_Button8.Click
        MsgBox(My.Lang.TitlebarColorNotice, MsgBoxStyle.Information)
    End Sub

    Private Sub W10_Button25_Click(sender As Object, e As EventArgs) Handles W10_Button25.Click
        MsgBox(My.Lang.CP_AccentOnTaskbarTib, MsgBoxStyle.Information)
    End Sub

    Private Sub W10_TB_Blur_CheckedChanged(sender As Object, e As EventArgs) Handles W10_TB_Blur.CheckedChanged
        If _Shown Then
            My.CP.Windows10.TB_Blur = sender.Checked
            If My.PreviewStyle = WindowStyle.W10 Then ApplyColorsToElements(My.CP)
        End If
    End Sub

    Private Sub Button37_Click(sender As Object, e As EventArgs) Handles Button37.Click
        'Copycat from Windows 10 colors
        tabs_preview.Visible = False
        My.CP.Windows11 = My.CP.Windows10.Clone
        ApplyCPValues(My.CP)
        ApplyColorsToElements(My.CP)
        tabs_preview.Visible = True
    End Sub

#End Region

#Region "Windows 8.1"
    Private Sub W81_pick_DragDrop(sender As Object, e As DragEventArgs) Handles W81_ColorizationColor_pick.DragDrop, W81_start_pick.DragDrop, W81_accent_pick.DragDrop, W81_personalcls_background_pick.DragDrop, W81_personalcolor_accent_pick.DragDrop
        If CType(sender, UI.Controllers.ColorItem).AllowDrop AndAlso My.PreviewStyle = WindowStyle.W81 Then
            My.CP.Windows81.ColorizationColor = W81_ColorizationColor_pick.BackColor
            My.CP.Windows81.StartColor = W81_start_pick.BackColor
            My.CP.Windows81.AccentColor = W81_accent_pick.BackColor
            My.CP.Windows81.PersonalColors_Background = W81_personalcls_background_pick.BackColor
            My.CP.Windows81.PersonalColors_Accent = W81_personalcolor_accent_pick.BackColor
            ApplyColorsToElements(My.CP)
        End If
    End Sub

    Private Sub W8_ColorizationColor_pick_Click(sender As Object, e As EventArgs) Handles W81_ColorizationColor_pick.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                My.CP.Windows81.ColorizationColor = sender.BackColor
                If My.PreviewStyle = WindowStyle.W81 Then ApplyColorsToElements(My.CP)
            End If
            Exit Sub
        End If

        Dim CList As New List(Of Control) From {
           sender,
           start,
           taskbar,
           Window1,
           Window2
       }

        Dim _Conditions As New Conditions With {.Window_ActiveTitlebar = True, .Window_InactiveTitlebar = True, .Win7LivePreview_Colorization = True}

        Dim C As Color = ColorPickerDlg.Pick(CList, _Conditions)

        My.CP.Windows81.ColorizationColor = Color.FromArgb(255, C)

        If My.PreviewStyle = WindowStyle.W81 Then ApplyColorsToElements(My.CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()
    End Sub

    Private Sub W8_ColorizationBalance_bar_Scroll(sender As Object) Handles W81_ColorizationBalance_bar.Scroll
        If _Shown Then
            W81_ColorizationBalance_val.Text = sender.Value.ToString()
            My.CP.Windows81.ColorizationColorBalance = W81_ColorizationBalance_bar.Value
            If My.PreviewStyle = WindowStyle.W81 Then ApplyColorsToElements(My.CP)
        End If
    End Sub

    Private Sub W8_start_pick_Click(sender As Object, e As EventArgs) Handles W81_start_pick.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                My.CP.Windows81.StartColor = sender.BackColor
                If My.PreviewStyle = WindowStyle.W81 Then ApplyColorsToElements(My.CP)
            End If
            Exit Sub
        End If

        Dim CList As New List(Of Control) From {sender}

        Dim C As Color = ColorPickerDlg.Pick(CList)

        My.CP.Windows81.StartColor = Color.FromArgb(255, C)

        If My.PreviewStyle = WindowStyle.W81 Then ApplyColorsToElements(My.CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()
    End Sub

    Private Sub W8_accent_pick_Click(sender As Object, e As EventArgs) Handles W81_accent_pick.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                My.CP.Windows81.AccentColor = sender.BackColor
                If My.PreviewStyle = WindowStyle.W81 Then ApplyColorsToElements(My.CP)
            End If
            Exit Sub
        End If

        Dim CList As New List(Of Control) From {sender}

        Dim C As Color = ColorPickerDlg.Pick(CList)

        My.CP.Windows81.AccentColor = Color.FromArgb(255, C)

        If My.PreviewStyle = WindowStyle.W81 Then ApplyColorsToElements(My.CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()
    End Sub

    Private Sub W8_personalcls_background_pick_Click(sender As Object, e As EventArgs) Handles W81_personalcls_background_pick.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                My.CP.Windows81.PersonalColors_Background = sender.BackColor
                If My.PreviewStyle = WindowStyle.W81 Then ApplyColorsToElements(My.CP)
            End If
            Exit Sub
        End If

        Dim CList As New List(Of Control) From {sender}

        Dim C As Color = ColorPickerDlg.Pick(CList)

        My.CP.Windows81.PersonalColors_Background = Color.FromArgb(255, C)

        If My.PreviewStyle = WindowStyle.W81 Then ApplyColorsToElements(My.CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()
    End Sub

    Private Sub W8_personalcolor_accent_pick_Click(sender As Object, e As EventArgs) Handles W81_personalcolor_accent_pick.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                My.CP.Windows81.PersonalColors_Accent = sender.BackColor
                If My.PreviewStyle = WindowStyle.W81 Then ApplyColorsToElements(My.CP)
            End If
            Exit Sub
        End If

        Dim CList As New List(Of Control) From {sender}

        Dim C As Color = ColorPickerDlg.Pick(CList)

        My.CP.Windows81.PersonalColors_Accent = Color.FromArgb(255, C)

        If My.PreviewStyle = WindowStyle.W81 Then ApplyColorsToElements(My.CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()
    End Sub

    Private Sub W8_ColorizationBalance_val_Click(sender As Object, e As EventArgs) Handles W81_ColorizationBalance_val.Click
        Dim response As String = InputBox(My.Lang.InputValue, sender.text, My.Lang.ItMustBeNumerical)
        sender.Text = Math.Max(Math.Min(Val(response), W81_ColorizationBalance_bar.Maximum), W81_ColorizationBalance_bar.Minimum) : W81_ColorizationBalance_bar.Value = Val(sender.Text)
    End Sub

    Private Sub W8_theme_aero_CheckedChanged(sender As Object) Handles W81_theme_aero.CheckedChanged
        If W81_theme_aero.Checked Then
            My.CP.Windows81.Theme = CP.Structures.Windows7.Themes.Aero
            If My.PreviewStyle = WindowStyle.W81 Then ApplyColorsToElements(My.CP)
        End If
    End Sub

    Private Sub W8_theme_aerolite_CheckedChanged(sender As Object) Handles W81_theme_aerolite.CheckedChanged
        If W81_theme_aerolite.Checked Then
            My.CP.Windows81.Theme = CP.Structures.Windows7.Themes.AeroLite
            If My.PreviewStyle = WindowStyle.W81 Then ApplyColorsToElements(My.CP)
        End If
    End Sub

    Private Sub W8_start_Click(sender As Object, e As EventArgs) Handles W81_start.Click
        Start8Selector.ShowDialog()
        If My.PreviewStyle = WindowStyle.W81 Then ApplyColorsToElements(My.CP)
    End Sub

    Private Sub W8_logonui_Click(sender As Object, e As EventArgs) Handles W81_logonui.Click
        LogonUI8Colors.ShowDialog()
        If My.PreviewStyle = WindowStyle.W81 Then ApplyColorsToElements(My.CP)
    End Sub
#End Region

#Region "Windows 7"
    Private Sub W7_pick_DragDrop(sender As Object, e As DragEventArgs) Handles W7_ColorizationColor_pick.DragDrop, W7_ColorizationAfterglow_pick.DragDrop
        If CType(sender, UI.Controllers.ColorItem).AllowDrop AndAlso My.PreviewStyle = WindowStyle.W7 Then
            My.CP.Windows7.ColorizationColor = W7_ColorizationColor_pick.BackColor
            My.CP.Windows7.ColorizationAfterglow = W7_ColorizationAfterglow_pick.BackColor
            ApplyColorsToElements(My.CP)
        End If
    End Sub

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
            Window1,
            Window2
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
            Window1,
            Window2
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
    Private Sub WVista_pick_DragDrop(sender As Object, e As DragEventArgs) Handles WVista_ColorizationColor_pick.DragDrop
        If CType(sender, UI.Controllers.ColorItem).AllowDrop AndAlso My.PreviewStyle = WindowStyle.WVista Then
            My.CP.WindowsVista.ColorizationColor = WVista_ColorizationColor_pick.BackColor
            ApplyColorsToElements(My.CP)
        End If
    End Sub

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
            Window1,
            Window2
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

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles WXP_VS_textbox.TextChanged
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

    Private Sub Button30_Click(sender As Object, e As EventArgs) Handles WXP_VS_Browse.Click
        If OpenFileDialog2.ShowDialog = DialogResult.OK Then
            WXP_VS_textbox.Text = OpenFileDialog2.FileName
        End If
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles WXP_VS_ColorsList.SelectedIndexChanged
        If _Shown AndAlso WXP_CustomTheme.Checked Then
            My.CP.WindowsXP.ColorScheme = WXP_VS_ColorsList.SelectedItem
            If My.PreviewStyle = WindowStyle.WXP Then ApplyStylesToElements(My.CP, False)
        End If
    End Sub
#End Region

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles apply_btn.Click
        Apply_Theme()
    End Sub

    Sub Apply_Theme(Optional [CP] As CP = Nothing)
        If [CP] Is Nothing Then [CP] = My.CP

        Cursor = Cursors.WaitCursor
        OldWidth = TablessControl1.Width

        log_lbl.Visible = False
        log_lbl.Text = ""
        Button8.Visible = False
        Button14.Visible = False
        Button22.Visible = False
        Button25.Visible = False

        If My.Settings.ThemeLog.Enabled Then
            TablessControl1.SelectedIndex = TablessControl1.TabCount - 1
            TablessControl1.Refresh()
            If My.Settings.ThemeLog.VerboseLevel = WPSettings.Structures.ThemeLog.VerboseLevels.Detailed Then
                previewContainer.Visible = False
                TablessControl1.Width = MainToolbar.Width
            End If
        End If

        [CP].Save(CP.CP_Type.Registry, "", If(My.Settings.ThemeLog.Enabled, TreeView1, Nothing))

        If My.PreviewStyle = WindowStyle.WXP Then Update_Wallpaper_Preview()

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
        Button8.Visible = True
        Button22.Visible = True
        Button25.Visible = True

        If Not My.Saving_Exceptions.Count = 0 Then
            log_lbl.Text = My.Lang.CP_ErrorHappened
            Button14.Visible = True
        Else
            If My.Settings.ThemeLog.CountDown AndAlso Not My.Settings.ThemeLog.VerboseLevel = WPSettings.Structures.ThemeLog.VerboseLevels.Detailed Then
                log_lbl.Text = String.Format(My.Lang.CP_LogWillClose, My.Settings.ThemeLog.CountDown_Seconds)
                elapsedSecs = 1
                Timer1.Enabled = True
                Timer1.Start()
            End If
        End If

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        log_lbl.Text = String.Format(My.Lang.CP_LogWillClose, My.Settings.ThemeLog.CountDown_Seconds - elapsedSecs)

        If elapsedSecs + 1 <= My.Settings.ThemeLog.CountDown_Seconds Then
            elapsedSecs += 1
        Else
            log_lbl.Text = ""
            Timer1.Enabled = False
            Timer1.Stop()
            If Not previewContainer.Visible Then previewContainer.Visible = True
            TablessControl1.Width = OldWidth
            SelectLeftPanelIndex()
        End If

    End Sub

    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
        log_lbl.Text = ""
        Timer1.Enabled = False
        Timer1.Stop()
        Saving_ex_list.ex_List = My.Saving_Exceptions
        Saving_ex_list.ShowDialog()
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        log_lbl.Text = ""
        Timer1.Enabled = False
        Timer1.Stop()
        If Not previewContainer.Visible Then previewContainer.Visible = True
        TablessControl1.Width = OldWidth
        SelectLeftPanelIndex()
    End Sub

    Private Sub Button22_Click(sender As Object, e As EventArgs) Handles Button22.Click
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
            status_lbl.ForeColor = If(My.Style.DarkMode, Color.Gold, Color.Gold.Dark(0.1))
        End If
    End Sub

    Private Sub Apply_btn_MouseLeave(sender As Object, e As EventArgs) Handles apply_btn.MouseLeave
        If My.Settings.ThemeApplyingBehavior.AutoRestartExplorer Then
            status_lbl.Text = ""
            status_lbl.ForeColor = If(My.Style.DarkMode, Color.White, Color.Black)
        End If
    End Sub

    Private Sub Button19_MouseEnter(sender As Object, e As EventArgs) Handles Button19.MouseEnter
        status_lbl.Text = My.Lang.ThisWillRestartExplorer
        status_lbl.ForeColor = If(My.Style.DarkMode, Color.Gold, Color.Gold.Dark(0.1))
    End Sub

    Private Sub Button19_MouseLeave(sender As Object, e As EventArgs) Handles Button19.MouseLeave
        status_lbl.Text = ""
        status_lbl.ForeColor = If(My.Style.DarkMode, Color.White, Color.Black)
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        If Not IO.File.Exists(SaveFileDialog1.FileName) Then
            If SaveFileDialog1.ShowDialog = DialogResult.OK Then
                My.CP.Save(CP.CP_Type.File, SaveFileDialog1.FileNames(0))
            End If
        Else
            My.CP.Save(CP.CP_Type.File, SaveFileDialog1.FileNames(0))
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
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

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        If SaveFileDialog1.ShowDialog = DialogResult.OK Then
            My.CP.Save(CP.CP_Type.File, SaveFileDialog1.FileNames(0))
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        ComplexSave.GetResponse(SaveFileDialog1, Sub() Apply_Theme(), Sub() Apply_Theme(My.CP_FirstTime), Sub() Apply_Theme(CP_Defaults.GetDefault))

        My.CP = New CP(CP.CP_Type.Registry)
        My.CP_Original = My.CP.Clone
        SaveFileDialog1.FileName = Nothing
        ApplyStylesToElements(My.CP, False)
        ApplyCPValues(My.CP)
        ApplyColorsToElements(My.CP)
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click, author_lbl.DoubleClick, themename_lbl.DoubleClick
        EditInfo.Show()
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        About.ShowDialog()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Updates.ShowDialog()
        Button5.Image = My.Resources.Update
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        SettingsX.ShowDialog()
    End Sub

    Private Sub Button4_Click_1(sender As Object, e As EventArgs) Handles Button4.Click
        Win32UI.Show()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Whatsnew.ShowDialog()
    End Sub

    Private Sub Button16_Click(sender As Object, e As EventArgs) Handles Button16.Click
        If My.PreviewStyle = WindowStyle.W11 Or My.PreviewStyle = WindowStyle.W10 Then
            LogonUI.ShowDialog()
        ElseIf My.PreviewStyle = WindowStyle.W81 Or My.PreviewStyle = WindowStyle.W7 Then
            LogonUI7.Show()
        ElseIf My.PreviewStyle = WindowStyle.WXP Then
            LogonUIXP.Show()
        ElseIf My.PreviewStyle = WindowStyle.WVista Then
            MsgBox(My.Lang.VistaLogonNotSupported, MsgBoxStyle.Exclamation)
        Else
            LogonUI.ShowDialog()
        End If
    End Sub

    Private Sub Button15_Click(sender As Object, e As EventArgs) Handles Button15.Click
        Update_Wallpaper_Preview()
    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        Me.Close()
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        If SaveFileDialog2.ShowDialog = DialogResult.OK Then
            tabs_preview.ToBitmap.Save(SaveFileDialog2.FileName)
        End If
    End Sub

    Private Sub Button17_Click(sender As Object, e As EventArgs) Handles Button17.Click
        My.CP = My.CP_Original.Clone
        ApplyStylesToElements(My.CP, False)
        ApplyCPValues(My.CP)
        ApplyColorsToElements(My.CP)
    End Sub

    Private Sub Button18_Click(sender As Object, e As EventArgs) Handles Button18.Click
        My.CP = My.CP_FirstTime.Clone
        ApplyStylesToElements(My.CP, False)
        ApplyCPValues(My.CP)
        ApplyColorsToElements(My.CP)
    End Sub

    Private Sub Button19_Click(sender As Object, e As EventArgs) Handles Button19.Click
        RestartExplorer()
    End Sub

    Private Sub Button20_Click(sender As Object, e As EventArgs) Handles Button20.Click
        ComplexSave.GetResponse(SaveFileDialog1, Sub() Apply_Theme(), Sub() Apply_Theme(My.CP_FirstTime), Sub() Apply_Theme(CP_Defaults.GetDefault))

        My.CP = CP_Defaults.GetDefault.Clone
        SaveFileDialog1.FileName = Nothing
        ApplyCPValues(My.CP)
        ApplyStylesToElements(My.CP, False)
        ApplyColorsToElements(My.CP)
    End Sub

    Private Sub Button21_Click(sender As Object, e As EventArgs) Handles Button21.Click
        CursorsStudio.Show()
    End Sub

    Private Sub Button23_Click(sender As Object, e As EventArgs) Handles Button23.Click
        If Button23.Text.ToLower = My.Lang.Hide.ToLower Then
            tabs_preview.Visible = False
            Button23.Text = My.Lang.Show
        Else
            tabs_preview.Visible = True
            Button23.Text = My.Lang.Hide
        End If
    End Sub

    Private Sub Button24_Click(sender As Object, e As EventArgs) Handles Button24.Click
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

    Private Sub Button27_Click(sender As Object, e As EventArgs) Handles Button27.Click
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
            Button20.Image = My.Resources.add_win11
        ElseIf My.PreviewStyle = WindowStyle.W10 Then
            Button20.Image = My.Resources.add_win10
        ElseIf My.PreviewStyle = WindowStyle.W81 Then
            Button20.Image = My.Resources.add_win8
        ElseIf My.PreviewStyle = WindowStyle.W7 Then
            Button20.Image = My.Resources.add_win7
        ElseIf My.PreviewStyle = WindowStyle.WVista Then
            Button20.Image = My.Resources.add_winvista
        ElseIf My.PreviewStyle = WindowStyle.WXP Then
            Button20.Image = My.Resources.add_winxp
        Else
            Button20.Image = My.Resources.add_win11
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

    Private Sub Select_W8_CheckedChanged(sender As Object) Handles Select_W81.CheckedChanged
        If _Shown And Select_W81.Checked Then
            My.PreviewStyle = WindowStyle.W81
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

    Private Sub Select_WXP_CheckedChanged(sender As Object) Handles Select_WXP.CheckedChanged
        If _Shown And Select_WXP.Checked Then
            My.PreviewStyle = WindowStyle.WXP
            Select_Preview_Version()
        End If
    End Sub

    Private Sub Button29_Click(sender As Object, e As EventArgs) Handles Button29.Click
        WinEffecter.ShowDialog()
    End Sub

    Private Sub Button32_Click(sender As Object, e As EventArgs) Handles Button32.Click
        If My.PreviewStyle <> WindowStyle.WXP AndAlso My.PreviewStyle <> WindowStyle.WVista Then
            AltTabEditor.ShowDialog()
        Else
            If My.PreviewStyle = WindowStyle.WXP Then MsgBox(String.Format(My.Lang.AltTab_Unsupported, My.Lang.OS_WinXP), MsgBoxStyle.Exclamation)
            If My.PreviewStyle = WindowStyle.WVista Then MsgBox(String.Format(My.Lang.AltTab_Unsupported, My.Lang.OS_WinVista), MsgBoxStyle.Exclamation)
        End If

    End Sub

    Private Sub Button25_Click(sender As Object, e As EventArgs) Handles Button25.Click
        log_lbl.Text = ""
        Timer1.Enabled = False
        Timer1.Stop()
    End Sub

    Private Sub Button33_Click(sender As Object, e As EventArgs) Handles Button33.Click
        ScreenSaver_Editor.ShowDialog()
    End Sub

    Private Sub Button34_Click(sender As Object, e As EventArgs) Handles Button34.Click
        Sounds_Editor.ShowDialog()
    End Sub

    Private Sub Button35_Click(sender As Object, e As EventArgs) Handles Button35.Click
        If My.PreviewStyle = WindowStyle.W11 Then
            Wallpaper_Editor.WT = My.CP.WallpaperTone_W11
        ElseIf My.PreviewStyle = WindowStyle.W10 Then
            Wallpaper_Editor.WT = My.CP.WallpaperTone_W10
        ElseIf My.PreviewStyle = WindowStyle.W81 Then
            Wallpaper_Editor.WT = My.CP.WallpaperTone_W81
        ElseIf My.PreviewStyle = WindowStyle.W7 Then
            Wallpaper_Editor.WT = My.CP.WallpaperTone_W7
        ElseIf My.PreviewStyle = WindowStyle.WVista Then
            Wallpaper_Editor.WT = My.CP.WallpaperTone_WVista
        ElseIf My.PreviewStyle = WindowStyle.WXP Then
            Wallpaper_Editor.WT = My.CP.WallpaperTone_WXP
        Else
            Wallpaper_Editor.WT = My.CP.WallpaperTone_W11
        End If

        Wallpaper_Editor.Show()
    End Sub

    Private Sub Button26_Click(sender As Object, e As EventArgs) Handles Button26.Click
        ApplicationThemer.FixLanguageDarkModeBug = False
        ApplicationThemer.Show()
    End Sub

    Private Sub Button36_Click(sender As Object, e As EventArgs) Handles Button36.Click
        Converter_Form.ShowDialog()
    End Sub

    Private Sub Button28_Click(sender As Object, e As EventArgs) Handles Button28.Click

        If MsgBox(My.Lang.LogoffQuestion, MsgBoxStyle.Question + MsgBoxStyle.YesNo, My.Lang.LogoffAlert1, "", "", "", "", My.Lang.LogoffAlert2, Ookii.Dialogs.WinForms.TaskDialogIcon.Information) = MsgBoxResult.Yes Then
            LoggingOff = True
            Kernel32.Wow64DisableWow64FsRedirection(IntPtr.Zero)
            If IO.File.Exists(My.PATH_System32 & "\logoff.exe") Then
                Shell(My.PATH_System32 & "\logoff.exe", AppWinStyle.Hide)
            Else
                MsgBox(String.Format(My.Lang.LogoffNotFound, My.PATH_System32), MsgBoxStyle.Exclamation)
            End If
        End If

    End Sub

    Private Sub Button28_MouseEnter(sender As Object, e As EventArgs) Handles Button28.MouseEnter
        status_lbl.Text = My.Lang.LogoffNotice
        status_lbl.ForeColor = If(My.Style.DarkMode, Color.Gold, Color.Gold.Dark(0.1))
    End Sub

    Private Sub Button28_MouseLeave(sender As Object, e As EventArgs) Handles Button28.MouseLeave
        status_lbl.Text = ""
        status_lbl.ForeColor = If(My.Style.DarkMode, Color.White, Color.Black)
    End Sub

    Private Sub Button39_Click(sender As Object, e As EventArgs) Handles Button39.Click
        Process.Start(My.Resources.Link_Wiki)
    End Sub

    Private Sub Button40_Click(sender As Object, e As EventArgs) Handles Button40.Click
        PaletteGenerateDashboard.ShowDialog()
    End Sub

    Private Sub Button30_Click_1(sender As Object, e As EventArgs) Handles Button30.Click
        MsgBox(My.Lang.Win11ColorsDescTip, MsgBoxStyle.Information, My.Lang.Win11ColorsDescTip2)
    End Sub

    Private Sub Button31_Click(sender As Object, e As EventArgs) Handles Button31.Click
        If My.WXP Then
            If MsgBox(String.Format(My.Lang.Store_WontWork_Protocol, My.Lang.OS_WinXP), MsgBoxStyle.Critical + MsgBoxStyle.YesNo) <> MsgBoxResult.Yes Then Exit Sub
        ElseIf My.WVista Then
            If MsgBox(String.Format(My.Lang.Store_WontWork_Protocol, My.Lang.OS_WinVista), MsgBoxStyle.Critical + MsgBoxStyle.YesNo) <> MsgBoxResult.Yes Then Exit Sub
        End If

        Store.Show()
    End Sub
End Class


