Imports WinPaletter.XenonCore

Public Class WinEffecter
    Private Sub WinEffecter_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ApplyDarkMode(Me)
        XenonButton12.Image = MainFrm.XenonButton20.Image.Resize(16, 16)
        ApplyFromCP(MainFrm.CP)
        MainFrm.SetToClassicButton(RetroButton1, MainFrm.CP)

    End Sub

    Sub ApplyFromCP(CP As CP)
        With CP.WindowsEffects
            EffectsEnabled.Checked = .Enabled
            XenonCheckBox1.Checked = .WindowAnimation
            XenonCheckBox2.Checked = .WindowShadow
            XenonCheckBox3.Checked = .WindowUIEffects
            XenonCheckBox6.Checked = .MenuAnimation
            If .MenuFade = CP.MenuAnimType.Fade Then XenonComboBox1.SelectedIndex = 0 Else XenonComboBox1.SelectedIndex = 1
            XenonCheckBox5.Checked = .MenuSelectionFade
            XenonTrackbar1.Value = .MenuShowDelay
            XenonCheckBox8.Checked = .ComboboxAnimation
            XenonCheckBox7.Checked = .ListBoxSmoothScrolling
            XenonCheckBox9.Checked = .TooltipAnimation
            If .TooltipFade = CP.MenuAnimType.Fade Then XenonComboBox2.SelectedIndex = 0 Else XenonComboBox2.SelectedIndex = 1
            XenonCheckBox4.Checked = .IconsShadow
            XenonCheckBox10.Checked = .IconsDesktopTranslSel
            XenonCheckBox11.Checked = .ShowWinContentDrag
            XenonCheckBox12.Checked = .KeyboardUnderline
            XenonTrackbar5.Value = .NotificationDuration
            XenonTrackbar2.Value = .FocusRectWidth
            XenonTrackbar3.Value = .FocusRectHeight
            XenonTrackbar4.Value = .Caret
            XenonCheckBox13.Checked = .AWT_Enabled
            XenonCheckBox14.Checked = .AWT_BringActivatedWindowToTop
            XenonTrackbar6.Value = .AWT_Delay
            XenonCheckBox15.Checked = .SnapCursorToDefButton
            XenonCheckBox16.Checked = .Win11ClassicContextMenu
            XenonCheckBox17.Checked = .BalloonNotifications
            XenonCheckBox20.Checked = .SysListView32
            XenonCheckBox19.Checked = .ShowSecondsInSystemClock
            XenonCheckBox18.Checked = .PaintDesktopVersion
            XenonCheckBox21.Checked = .ShakeToMinimize
            XenonCheckBox22.Checked = .Win11BootDots

            XenonRadioButton1.Checked = (.Win11ExplorerBar = CP.ExplorerBar.Default)
            XenonRadioButton2.Checked = (.Win11ExplorerBar = CP.ExplorerBar.Ribbon)
            XenonRadioButton3.Checked = (.Win11ExplorerBar = CP.ExplorerBar.Bar)
            XenonCheckBox23.Checked = .DisableNavBar

            Panel2.Width = .Caret
        End With


    End Sub

    Sub ApplyToCP(CP As CP)
        With CP.WindowsEffects
            .Enabled = EffectsEnabled.Checked
            .WindowAnimation = XenonCheckBox1.Checked
            .WindowShadow = XenonCheckBox2.Checked
            .WindowUIEffects = XenonCheckBox3.Checked
            .MenuAnimation = XenonCheckBox6.Checked
            If XenonComboBox1.SelectedIndex = 0 Then .MenuFade = CP.MenuAnimType.Fade Else .MenuFade = CP.MenuAnimType.Scroll
            .MenuSelectionFade = XenonCheckBox5.Checked
            .MenuShowDelay = XenonTrackbar1.Value
            .ComboboxAnimation = XenonCheckBox8.Checked
            .ListBoxSmoothScrolling = XenonCheckBox7.Checked
            .TooltipAnimation = XenonCheckBox9.Checked
            If XenonComboBox2.SelectedIndex = 0 Then .TooltipFade = CP.MenuAnimType.Fade Else .TooltipFade = CP.MenuAnimType.Scroll
            .IconsShadow = XenonCheckBox4.Checked
            .IconsDesktopTranslSel = XenonCheckBox10.Checked
            .ShowWinContentDrag = XenonCheckBox11.Checked
            .KeyboardUnderline = XenonCheckBox12.Checked
            .NotificationDuration = XenonTrackbar5.Value
            .FocusRectWidth = XenonTrackbar2.Value
            .FocusRectHeight = XenonTrackbar3.Value
            .Caret = XenonTrackbar4.Value
            .AWT_Enabled = XenonCheckBox13.Checked
            .AWT_BringActivatedWindowToTop = XenonCheckBox14.Checked
            .AWT_Delay = XenonTrackbar6.Value
            .SnapCursorToDefButton = XenonCheckBox15.Checked
            .Win11ClassicContextMenu = XenonCheckBox16.Checked
            .BalloonNotifications = XenonCheckBox17.Checked
            .SysListView32 = XenonCheckBox20.Checked
            .ShowSecondsInSystemClock = XenonCheckBox19.Checked
            .PaintDesktopVersion = XenonCheckBox18.Checked
            .ShakeToMinimize = XenonCheckBox21.Checked
            .Win11BootDots = XenonCheckBox22.Checked

            If XenonRadioButton1.Checked Then
                .Win11ExplorerBar = CP.ExplorerBar.Default

            ElseIf XenonRadioButton2.Checked Then
                .Win11ExplorerBar = CP.ExplorerBar.Ribbon

            ElseIf XenonRadioButton3.Checked Then
                .Win11ExplorerBar = CP.ExplorerBar.Bar

            End If

            .DisableNavBar = XenonCheckBox23.Checked

        End With
    End Sub

    Private Sub XenonButton11_Click(sender As Object, e As EventArgs) Handles XenonButton11.Click
        If OpenFileDialog1.ShowDialog = DialogResult.OK Then
            Dim CPx As New CP(CP.CP_Type.File, OpenFileDialog1.FileName)
            ApplyFromCP(CPx)
            CPx.Dispose()
        End If
    End Sub

    Private Sub XenonButton9_Click(sender As Object, e As EventArgs) Handles XenonButton9.Click
        Dim CPx As New CP(CP.CP_Type.Registry)
        ApplyFromCP(CPx)
        CPx.Dispose()
    End Sub

    Private Sub XenonButton12_Click(sender As Object, e As EventArgs) Handles XenonButton12.Click
        Dim _Def As CP
        If MainFrm.PreviewConfig = MainFrm.WinVer.W11 Then
            _Def = New CP_Defaults().Default_Windows11
        ElseIf MainFrm.PreviewConfig = MainFrm.WinVer.W10 Then
            _Def = New CP_Defaults().Default_Windows10
        ElseIf MainFrm.PreviewConfig = MainFrm.WinVer.W8 Then
            _Def = New CP_Defaults().Default_Windows8
        ElseIf MainFrm.PreviewConfig = MainFrm.WinVer.W7 Then
            _Def = New CP_Defaults().Default_Windows7
        ElseIf MainFrm.PreviewConfig = MainFrm.WinVer.WVista Then
            _Def = New CP_Defaults().Default_WindowsVista
        ElseIf MainFrm.PreviewConfig = MainFrm.WinVer.WXP Then
            _Def = New CP_Defaults().Default_WindowsXP
        Else
            _Def = New CP_Defaults().Default_Windows11
        End If

        ApplyFromCP(_Def)
        _Def.Dispose()
    End Sub

    Private Sub XenonButton7_Click(sender As Object, e As EventArgs) Handles XenonButton7.Click
        Me.Close()
    End Sub

    Private Sub XenonButton10_Click(sender As Object, e As EventArgs) Handles XenonButton10.Click
        Cursor = Cursors.WaitCursor
        Dim CPx As New CP(CP.CP_Type.Registry)
        ApplyToCP(CPx)
        CPx.WindowsEffects.Apply()
        CPx.Win32.Update_UPM_DEFAULT()
        CPx.Dispose()
        Cursor = Cursors.Default
    End Sub

    Private Sub XenonButton8_Click(sender As Object, e As EventArgs) Handles XenonButton8.Click
        ApplyToCP(MainFrm.CP)
        MainFrm.ApplyLivePreviewFromCP(MainFrm.CP)
        Me.Close()
    End Sub

    Private Sub EffectsEnabled_CheckedChanged(sender As Object, e As EventArgs) Handles EffectsEnabled.CheckedChanged
        checker_img.Image = If(sender.Checked, My.Resources.checker_enabled, My.Resources.checker_disabled)
    End Sub

    Private Sub MD_Click(sender As Object, e As EventArgs) Handles MD.Click
        Dim response As String = InputBox(My.Lang.InputValue, sender.text, My.Lang.ItMustBeNumerical)
        sender.Text = Math.Max(Math.Min(Val(response), XenonTrackbar1.Maximum), XenonTrackbar1.Minimum) : XenonTrackbar1.Value = Val(sender.Text)
    End Sub

    Private Sub XenonTrackbar1_Scroll(sender As Object) Handles XenonTrackbar1.Scroll
        MD.Text = sender.Value
    End Sub

    Private Sub XenonButton4_Click(sender As Object, e As EventArgs) Handles XenonButton4.Click
        Dim response As String = InputBox(My.Lang.InputValue, sender.text, My.Lang.ItMustBeNumerical)
        sender.Text = Math.Max(Math.Min(Val(response), XenonTrackbar5.Maximum), XenonTrackbar5.Minimum) : XenonTrackbar5.Value = Val(sender.Text)
    End Sub

    Private Sub XenonButton1_Click(sender As Object, e As EventArgs) Handles XenonButton1.Click
        Dim response As String = InputBox(My.Lang.InputValue, sender.text, My.Lang.ItMustBeNumerical)
        sender.Text = Math.Max(Math.Min(Val(response), XenonTrackbar2.Maximum), XenonTrackbar2.Minimum) : XenonTrackbar2.Value = Val(sender.Text)
    End Sub

    Private Sub XenonButton2_Click(sender As Object, e As EventArgs) Handles XenonButton2.Click
        Dim response As String = InputBox(My.Lang.InputValue, sender.text, My.Lang.ItMustBeNumerical)
        sender.Text = Math.Max(Math.Min(Val(response), XenonTrackbar3.Maximum), XenonTrackbar3.Minimum) : XenonTrackbar3.Value = Val(sender.Text)
    End Sub

    Private Sub XenonButton3_Click(sender As Object, e As EventArgs) Handles XenonButton3.Click
        Dim response As String = InputBox(My.Lang.InputValue, sender.text, My.Lang.ItMustBeNumerical)
        sender.Text = Math.Max(Math.Min(Val(response), XenonTrackbar4.Maximum), XenonTrackbar4.Minimum) : XenonTrackbar4.Value = Val(sender.Text)
    End Sub

    Private Sub XenonButton5_Click(sender As Object, e As EventArgs) Handles XenonButton5.Click
        Dim response As String = InputBox(My.Lang.InputValue, sender.text, My.Lang.ItMustBeNumerical)
        sender.Text = Math.Max(Math.Min(Val(response), XenonTrackbar6.Maximum), XenonTrackbar6.Minimum) : XenonTrackbar6.Value = Val(sender.Text)
    End Sub

    Private Sub XenonTrackbar5_Scroll(sender As Object) Handles XenonTrackbar5.Scroll
        XenonButton4.Text = sender.Value
    End Sub

    Private Sub XenonTrackbar2_Scroll(sender As Object) Handles XenonTrackbar2.Scroll
        XenonButton1.Text = sender.Value
        RetroButton1.FocusRectWidth = sender.Value : RetroButton1.Refresh()
    End Sub

    Private Sub XenonTrackbar3_Scroll(sender As Object) Handles XenonTrackbar3.Scroll
        XenonButton2.Text = sender.Value
        RetroButton1.FocusRectHeight = sender.Value : RetroButton1.Refresh()
    End Sub

    Private Sub XenonTrackbar4_Scroll(sender As Object) Handles XenonTrackbar4.Scroll
        XenonButton3.Text = sender.Value
        Panel2.Width = sender.Value
    End Sub

    Private Sub XenonTrackbar6_Scroll(sender As Object) Handles XenonTrackbar6.Scroll
        XenonButton5.Text = sender.Value
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Panel2.Visible = Not Panel2.Visible
    End Sub
End Class