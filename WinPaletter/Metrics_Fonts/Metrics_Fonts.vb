Imports WinPaletter.NativeMethods
Imports WinPaletter.XenonCore

Public Class Metrics_Fonts

    Private Sub EditFonts_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        pnl_preview1.BackgroundImage = MainFrm.pnl_preview.BackgroundImage
        pnl_preview2.BackgroundImage = MainFrm.pnl_preview.BackgroundImage
        pnl_preview3.BackgroundImage = MainFrm.pnl_preview.BackgroundImage
        pnl_preview4.BackgroundImage = MainFrm.pnl_preview.BackgroundImage

        CopyCatPreview(XenonWindow1, MainFrm.XenonWindow1)
        CopyCatPreview(XenonWindow2, MainFrm.XenonWindow1)
        CopyCatPreview(XenonWindow4, MainFrm.XenonWindow1)
        CopyCatPreview(XenonWindow6, MainFrm.XenonWindow1)

        ApplyDarkMode(Me)
        ApplyFromCP(MainFrm.CP)

        MainFrm.MakeItDoubleBuffered(pnl_preview1)
        MainFrm.MakeItDoubleBuffered(pnl_preview2)
        MainFrm.MakeItDoubleBuffered(pnl_preview3)
        MainFrm.MakeItDoubleBuffered(pnl_preview4)

        MainFrm.MakeItDoubleBuffered(XenonFakeIcon1)
        MainFrm.MakeItDoubleBuffered(XenonFakeIcon2)
        MainFrm.MakeItDoubleBuffered(XenonFakeIcon3)

        XenonFakeIcon1.Title = "Icon 1"
        XenonFakeIcon2.Title = "Icon 2"
        XenonFakeIcon3.Title = "Icon 3"

        XenonFakeIcon1.Icon = MainFrm.Icon 'My.Resources.fileextension 'Shell32.GetSystemIcon(Shell32.SHSTOCKICONID.RECYCLER, Shell32.SHGSI.ICON)
        XenonFakeIcon2.Icon = My.Resources.fileextension  'My.Resources.settingsfile 'Shell32.GetSystemIcon(Shell32.SHSTOCKICONID.FOLDER, Shell32.SHGSI.ICON)
        XenonFakeIcon3.Icon = My.Resources.Icon_Uninstall 'My.Resources.icons8_command_line 'Shell32.GetSystemIcon(Shell32.SHSTOCKICONID.APPLICATION, Shell32.SHGSI.ICON)

        PictureBox35.Image = Shell32.GetSystemIcon(Shell32.SHSTOCKICONID.INFO, Shell32.SHGSI.ICON).ToBitmap

        If Not XenonWindow6.Win7 And Not XenonWindow6.Win8 Then
            msgLbl.ForeColor = If(XenonWindow6.DarkMode, Color.White, Color.Black)
        Else
            msgLbl.ForeColor = Color.Black
        End If

        XenonButton12.Image = MainFrm.XenonButton20.Image.Resize(16, 16)

    End Sub

    Sub CopyCatPreview([ToXenonWindow] As XenonWindow, [FromXenonWindow] As XenonWindow)
        [ToXenonWindow].AccentColor_Active = [FromXenonWindow].AccentColor_Active
        [ToXenonWindow].AccentColor2_Active = [FromXenonWindow].AccentColor2_Active
        [ToXenonWindow].AccentColor_Inactive = [FromXenonWindow].AccentColor_Inactive
        [ToXenonWindow].AccentColor2_Inactive = [FromXenonWindow].AccentColor2_Inactive
        [ToXenonWindow].AccentColor_Enabled = [FromXenonWindow].AccentColor_Enabled
        [ToXenonWindow].BackColor = [FromXenonWindow].BackColor
        [ToXenonWindow].DarkMode = [FromXenonWindow].DarkMode
        [ToXenonWindow].Font = [FromXenonWindow].Font
        [ToXenonWindow].ForeColor = [FromXenonWindow].ForeColor
        [ToXenonWindow].RoundedCorners = [FromXenonWindow].RoundedCorners
        [ToXenonWindow].Win7 = [FromXenonWindow].Win7
        [ToXenonWindow].Win7Aero = [FromXenonWindow].Win7Aero
        [ToXenonWindow].Win7AeroOpaque = [FromXenonWindow].Win7AeroOpaque
        [ToXenonWindow].Win7Alpha = [FromXenonWindow].Win7Alpha
        [ToXenonWindow].Win7Basic = [FromXenonWindow].Win7Basic
        [ToXenonWindow].Win7ColorBal = [FromXenonWindow].Win7ColorBal
        [ToXenonWindow].Win7GlowBal = [FromXenonWindow].Win7GlowBal
        [ToXenonWindow].Win7Noise = [FromXenonWindow].Win7Noise
        [ToXenonWindow].Win8 = [FromXenonWindow].Win8
        [ToXenonWindow].Win8Lite = [FromXenonWindow].Win8Lite
        [ToXenonWindow].Padding = [FromXenonWindow].Padding
    End Sub

    Sub ApplyFromCP(CP As CP)
        MetricsEnabled.Checked = CP.WinMetrics_Fonts.Enabled

        Label1.Font = CP.WinMetrics_Fonts.CaptionFont
        XenonWindow1.Font = CP.WinMetrics_Fonts.CaptionFont
        RetroWindow1.Font = CP.WinMetrics_Fonts.CaptionFont

        Label2.Font = CP.WinMetrics_Fonts.IconFont
        XenonFakeIcon1.Font = CP.WinMetrics_Fonts.IconFont
        XenonFakeIcon2.Font = CP.WinMetrics_Fonts.IconFont
        XenonFakeIcon3.Font = CP.WinMetrics_Fonts.IconFont

        Label3.Font = CP.WinMetrics_Fonts.MenuFont
        MenuStrip1.Font = CP.WinMetrics_Fonts.MenuFont

        Label5.Font = CP.WinMetrics_Fonts.SmCaptionFont
        XenonWindow2.Font = CP.WinMetrics_Fonts.SmCaptionFont

        Label4.Font = CP.WinMetrics_Fonts.MessageFont
        msgLbl.Font = CP.WinMetrics_Fonts.MessageFont

        Label6.Font = CP.WinMetrics_Fonts.StatusFont
        statusLbl.Font = CP.WinMetrics_Fonts.StatusFont

        XenonTrackbar1.Value = CP.WinMetrics_Fonts.BorderWidth
        XenonTrackbar2.Value = CP.WinMetrics_Fonts.CaptionHeight
        XenonTrackbar3.Value = CP.WinMetrics_Fonts.CaptionWidth
        XenonTrackbar6.Value = CP.WinMetrics_Fonts.IconSpacing
        XenonTrackbar4.Value = CP.WinMetrics_Fonts.IconVerticalSpacing
        XenonTrackbar9.Value = CP.WinMetrics_Fonts.MenuHeight
        XenonTrackbar8.Value = CP.WinMetrics_Fonts.MenuWidth
        XenonToggle1.Checked = CP.WinMetrics_Fonts.MinAnimate
        XenonTrackbar12.Value = CP.WinMetrics_Fonts.PaddedBorderWidth
        XenonTrackbar11.Value = CP.WinMetrics_Fonts.ScrollHeight
        XenonTrackbar10.Value = CP.WinMetrics_Fonts.ScrollWidth
        XenonTrackbar14.Value = CP.WinMetrics_Fonts.SmCaptionHeight
        XenonTrackbar13.Value = CP.WinMetrics_Fonts.SmCaptionWidth
        XenonTrackbar7.Value = CP.WinMetrics_Fonts.DesktopIconSize
        XenonTrackbar5.Value = CP.WinMetrics_Fonts.ShellIconSize

        RetroWindow1.ButtonDkShadow = MainFrm.CP.Win32.ButtonDkShadow
        RetroWindow1.BackColor = MainFrm.CP.Win32.ButtonFace
        RetroWindow1.ButtonHilight = MainFrm.CP.Win32.ButtonHilight
        RetroWindow1.ButtonLight = MainFrm.CP.Win32.ButtonLight
        RetroWindow1.ButtonShadow = MainFrm.CP.Win32.ButtonShadow
        RetroWindow1.ColorBorder = MainFrm.CP.Win32.ActiveBorder
        RetroWindow1.ForeColor = MainFrm.CP.Win32.TitleText
        RetroWindow1.Color1 = MainFrm.CP.Win32.ActiveTitle
        RetroWindow1.Color2 = MainFrm.CP.Win32.GradientActiveTitle
        RetroWindow1.ColorGradient = MainFrm.CP.Win32.EnableGradient

        RetroButton3.ButtonDkShadow = MainFrm.CP.Win32.ButtonDkShadow
        RetroButton3.ButtonHilight = MainFrm.CP.Win32.ButtonHilight
        RetroButton3.ButtonLight = MainFrm.CP.Win32.ButtonLight
        RetroButton3.ButtonShadow = MainFrm.CP.Win32.ButtonShadow
        RetroButton3.BackColor = MainFrm.CP.Win32.ButtonFace

        RetroButton4.ButtonDkShadow = MainFrm.CP.Win32.ButtonDkShadow
        RetroButton4.ButtonHilight = MainFrm.CP.Win32.ButtonHilight
        RetroButton4.ButtonLight = MainFrm.CP.Win32.ButtonLight
        RetroButton4.ButtonShadow = MainFrm.CP.Win32.ButtonShadow
        RetroButton4.BackColor = MainFrm.CP.Win32.ButtonFace

        RetroButton5.ButtonDkShadow = MainFrm.CP.Win32.ButtonDkShadow
        RetroButton5.ButtonHilight = MainFrm.CP.Win32.ButtonHilight
        RetroButton5.ButtonLight = MainFrm.CP.Win32.ButtonLight
        RetroButton5.ButtonShadow = MainFrm.CP.Win32.ButtonShadow
        RetroButton5.BackColor = MainFrm.CP.Win32.ButtonFace

    End Sub

    Sub ApplyToCP(CP As CP)
        CP.WinMetrics_Fonts.Enabled = MetricsEnabled.Checked

        CP.WinMetrics_Fonts.CaptionFont = Label1.Font
        CP.WinMetrics_Fonts.IconFont = Label2.Font
        CP.WinMetrics_Fonts.MenuFont = Label3.Font
        CP.WinMetrics_Fonts.MessageFont = Label4.Font
        CP.WinMetrics_Fonts.SmCaptionFont = Label5.Font
        CP.WinMetrics_Fonts.StatusFont = Label6.Font

        CP.WinMetrics_Fonts.BorderWidth = XenonTrackbar1.Value
        CP.WinMetrics_Fonts.CaptionHeight = XenonTrackbar2.Value
        CP.WinMetrics_Fonts.CaptionWidth = XenonTrackbar3.Value
        CP.WinMetrics_Fonts.IconSpacing = XenonTrackbar6.Value
        CP.WinMetrics_Fonts.IconVerticalSpacing = XenonTrackbar4.Value
        CP.WinMetrics_Fonts.MenuHeight = XenonTrackbar9.Value
        CP.WinMetrics_Fonts.MenuWidth = XenonTrackbar8.Value
        CP.WinMetrics_Fonts.MinAnimate = XenonToggle1.Checked
        CP.WinMetrics_Fonts.PaddedBorderWidth = XenonTrackbar12.Value
        CP.WinMetrics_Fonts.ScrollHeight = XenonTrackbar11.Value
        CP.WinMetrics_Fonts.ScrollWidth = XenonTrackbar10.Value
        CP.WinMetrics_Fonts.SmCaptionHeight = XenonTrackbar14.Value
        CP.WinMetrics_Fonts.SmCaptionWidth = XenonTrackbar13.Value
        CP.WinMetrics_Fonts.DesktopIconSize = XenonTrackbar7.Value
        CP.WinMetrics_Fonts.ShellIconSize = XenonTrackbar5.Value
    End Sub

    Private Sub XenonButton1_Click(sender As Object, e As EventArgs) Handles XenonButton1.Click
        FontDialog1.Font = Label1.Font
        If FontDialog1.ShowDialog = DialogResult.OK Then
            Label1.Font = FontDialog1.Font
            XenonWindow1.Font = FontDialog1.Font
            RetroWindow1.Font = FontDialog1.Font

            XenonWindow1.Refresh()
            RetroWindow1.Refresh()

            RetroButton3.Height = RetroWindow1.Metrics_CaptionHeight + RetroWindow1.GetTitleTextHeight - 4
            RetroButton4.Height = RetroWindow1.Metrics_CaptionHeight + RetroWindow1.GetTitleTextHeight - 4
            RetroButton5.Height = RetroWindow1.Metrics_CaptionHeight + RetroWindow1.GetTitleTextHeight - 4

            RetroButton3.Top = XenonTrackbar1.Value + XenonTrackbar12.Value + 5
            RetroButton4.Top = RetroButton3.Top
            RetroButton5.Top = RetroButton3.Top

        End If
    End Sub

    Private Sub XenonButton2_Click(sender As Object, e As EventArgs) Handles XenonButton2.Click
        FontDialog1.Font = Label2.Font
        If FontDialog1.ShowDialog = DialogResult.OK Then
            Label2.Font = FontDialog1.Font
            XenonFakeIcon1.Font = FontDialog1.Font
            XenonFakeIcon2.Font = FontDialog1.Font
            XenonFakeIcon3.Font = FontDialog1.Font
        End If
    End Sub

    Private Sub XenonButton3_Click(sender As Object, e As EventArgs) Handles XenonButton3.Click
        FontDialog1.Font = Label3.Font
        If FontDialog1.ShowDialog = DialogResult.OK Then
            Label3.Font = FontDialog1.Font
            MenuStrip1.Font = FontDialog1.Font
        End If
    End Sub


    Private Sub XenonButton4_Click(sender As Object, e As EventArgs) Handles XenonButton4.Click
        FontDialog1.Font = Label4.Font
        If FontDialog1.ShowDialog = DialogResult.OK Then
            Label4.Font = FontDialog1.Font
            msgLbl.Font = FontDialog1.Font
        End If
    End Sub

    Private Sub XenonButton5_Click(sender As Object, e As EventArgs) Handles XenonButton5.Click
        FontDialog1.Font = Label5.Font
        If FontDialog1.ShowDialog = DialogResult.OK Then
            Label5.Font = FontDialog1.Font
            XenonWindow2.Font = FontDialog1.Font
        End If
    End Sub

    Private Sub XenonButton6_Click(sender As Object, e As EventArgs) Handles XenonButton6.Click
        FontDialog1.Font = Label6.Font
        If FontDialog1.ShowDialog = DialogResult.OK Then
            Label6.Font = FontDialog1.Font
            statusLbl.Font = FontDialog1.Font
        End If
    End Sub

    Private Sub XenonButton8_Click(sender As Object, e As EventArgs) Handles XenonButton8.Click
        ApplyToCP(MainFrm.CP)
        Me.Close()
        MainFrm.ApplyMetrics(MainFrm.CP, MainFrm.XenonWindow1)
        MainFrm.ApplyMetrics(MainFrm.CP, MainFrm.XenonWindow2)
    End Sub

    Private Sub XenonButton7_Click(sender As Object, e As EventArgs) Handles XenonButton7.Click
        Me.Close()
    End Sub

    Private Sub XenonButton10_Click(sender As Object, e As EventArgs) Handles XenonButton10.Click
        Cursor = Cursors.WaitCursor
        Dim CPx As New CP(CP.Mode.Registry)
        ApplyToCP(CPx)
        CPx.WinMetrics_Fonts.Apply()
        CPx.Dispose()
        Cursor = Cursors.Default
    End Sub

    Private Sub XenonTrackbar2_Scroll(sender As Object) Handles XenonTrackbar2.Scroll
        ttl_h.Text = sender.Value.ToString

        XenonWindow1.Metrics_CaptionHeight = sender.Value
        RetroWindow1.Metrics_CaptionHeight = sender.Value

        RetroButton3.Height = sender.Value + RetroWindow1.GetTitleTextHeight - 4
        RetroButton4.Height = sender.Value + RetroWindow1.GetTitleTextHeight - 4
        RetroButton5.Height = sender.Value + RetroWindow1.GetTitleTextHeight - 4

        UpdateTitlebarButtonsFontSize()
    End Sub

    Private Sub XenonTrackbar3_Scroll(sender As Object) Handles XenonTrackbar3.Scroll
        ttl_w.Text = sender.Value
        RetroWindow1.Metrics_CaptionWidth = sender.Value

        RetroButton3.Width = RetroWindow1.Metrics_CaptionWidth - 2
        RetroButton4.Width = RetroWindow1.Metrics_CaptionWidth - 2
        RetroButton5.Width = RetroWindow1.Metrics_CaptionWidth - 2

        RetroButton3.Left = RetroWindow1.Width - RetroButton3.Width - XenonTrackbar1.Value - XenonTrackbar12.Value - 5
        RetroButton4.Left = RetroButton3.Left - 2 - RetroButton4.Width
        RetroButton5.Left = RetroButton4.Left - RetroButton5.Width

        UpdateTitlebarButtonsFontSize()
    End Sub

    Sub UpdateTitlebarButtonsFontSize()
        Try
            Dim i0, iFx As Single
            i0 = Math.Abs(Math.Min(XenonTrackbar2.Value, XenonTrackbar3.Value))
            iFx = i0 / Math.Abs(Math.Min(XenonTrackbar2.Minimum, XenonTrackbar3.Minimum))
            Dim f As New Font("Marlett", 6.8 * iFx)
            RetroButton3.Font = f
            RetroButton4.Font = f
            RetroButton5.Font = f
        Catch

        End Try
    End Sub


    Private Sub XenonTrackbar6_Scroll(sender As Object) Handles XenonTrackbar6.Scroll
        i_s_h.Text = sender.Value

        XenonFakeIcon3.Left = (XenonFakeIcon1.Left + (80 / 100) * XenonTrackbar6.Value) * XenonFakeIcon1.Width / (48 + 18)

        XenonFakeIcon3.SendToBack()
        XenonFakeIcon1.BringToFront()
    End Sub

    Private Sub XenonTrackbar4_Scroll(sender As Object) Handles XenonTrackbar4.Scroll
        i_s_v.Text = sender.Value

        XenonFakeIcon2.Top = (XenonFakeIcon1.Top + (100 / 75) * XenonTrackbar4.Value) * XenonFakeIcon1.Height / (48 + 35)

        XenonFakeIcon2.SendToBack()
        XenonFakeIcon1.BringToFront()
    End Sub

    Private Sub XenonTrackbar9_Scroll(sender As Object) Handles XenonTrackbar9.Scroll
        m_h.Text = sender.Value
        MenuStrip1.Height = Math.Max(sender.Value, GetTitleTextHeight(MenuStrip1.Font))
    End Sub

    Private Sub XenonTrackbar8_Scroll(sender As Object) Handles XenonTrackbar8.Scroll
        m_w.Text = sender.Value
    End Sub

    Private Sub XenonTrackbar1_Scroll(sender As Object) Handles XenonTrackbar1.Scroll
        ttl_b.Text = sender.Value
        XenonWindow1.Metrics_BorderWidth = sender.Value
        RetroWindow1.Metrics_BorderWidth = sender.Value

        RetroButton3.Left = RetroWindow1.Width - RetroButton3.Width - XenonTrackbar1.Value - XenonTrackbar12.Value - 5
        RetroButton3.Top = XenonTrackbar1.Value + XenonTrackbar12.Value + 5
        RetroButton4.Top = RetroButton3.Top
        RetroButton5.Top = RetroButton3.Top

        RetroButton4.Left = RetroButton3.Left - 2 - RetroButton4.Width
        RetroButton5.Left = RetroButton4.Left - RetroButton5.Width

    End Sub

    Private Sub XenonTrackbar12_Scroll(sender As Object) Handles XenonTrackbar12.Scroll
        ttl_p.Text = sender.Value
        XenonWindow1.Metrics_PaddedBorderWidth = sender.Value
        RetroWindow1.Metrics_PaddedBorderWidth = sender.Value

        RetroButton3.Left = RetroWindow1.Width - RetroButton3.Width - XenonTrackbar1.Value - XenonTrackbar12.Value - 5
        RetroButton3.Top = XenonTrackbar1.Value + XenonTrackbar12.Value + 5
        RetroButton4.Top = RetroButton3.Top
        RetroButton5.Top = RetroButton3.Top

        RetroButton4.Left = RetroButton3.Left - 2 - RetroButton4.Width
        RetroButton5.Left = RetroButton4.Left - RetroButton5.Width
    End Sub

    Private Sub XenonTrackbar11_Scroll(sender As Object) Handles XenonTrackbar11.Scroll
        s_h.Text = sender.Value
        HScrollBar1.Height = sender.Value
    End Sub

    Private Sub XenonTrackbar10_Scroll(sender As Object) Handles XenonTrackbar10.Scroll
        s_w.Text = sender.Value
        VScrollBar1.Width = sender.Value
    End Sub

    Private Sub XenonTrackbar14_Scroll(sender As Object) Handles XenonTrackbar14.Scroll
        tw_h.Text = sender.Value
        XenonWindow2.Metrics_CaptionHeight = sender.Value
    End Sub

    Private Sub XenonTrackbar13_Scroll(sender As Object) Handles XenonTrackbar13.Scroll
        tw_w.Text = sender.Value
    End Sub

    Private Sub XenonTrackbar7_Scroll(sender As Object) Handles XenonTrackbar7.Scroll
        If XenonTrackbar7.Value < XenonTrackbar7.Minimum Or XenonTrackbar7.Value > XenonTrackbar7.Maximum Then Exit Sub

        i_d_s.Text = sender.Value
        XenonFakeIcon1.IconSize = sender.Value
        XenonFakeIcon2.IconSize = sender.Value
        XenonFakeIcon3.IconSize = sender.Value

        XenonFakeIcon1.Height = sender.Value + 35
        XenonFakeIcon2.Height = sender.Value + 35
        XenonFakeIcon3.Height = sender.Value + 35

        If sender.Value > 64 Then
            XenonFakeIcon1.Width = sender.Value + 18
            XenonFakeIcon2.Width = sender.Value + 18
            XenonFakeIcon3.Width = sender.Value + 18
        Else
            XenonFakeIcon1.Width = XenonTrackbar6.Value + 8
            XenonFakeIcon2.Width = XenonTrackbar6.Value + 8
            XenonFakeIcon3.Width = XenonTrackbar6.Value + 8
        End If

        XenonFakeIcon2.Top = (XenonFakeIcon1.Top + (100 / 75) * XenonTrackbar4.Value) * XenonFakeIcon1.Height / (48 + 35)

        XenonFakeIcon3.Left = (XenonFakeIcon1.Left + (80 / 100) * XenonTrackbar6.Value) * XenonFakeIcon1.Width / (48 + 18)

    End Sub


    Private Sub Label1_FontChanged(sender As Object, e As EventArgs) Handles Label1.FontChanged, Label2.FontChanged, Label3.FontChanged, Label4.FontChanged, Label5.FontChanged, Label6.FontChanged
        DirectCast(sender, Label).Text = DirectCast(sender, Label).Font.FontFamily.Name
    End Sub

    Private Sub XenonTrackbar5_Scroll(sender As Object) Handles XenonTrackbar5.Scroll
        i_s_s.Text = sender.Value
    End Sub

    Private Sub XenonWindow1_MetricsChanged() Handles XenonWindow1.MetricsChanged
        XenonWindow1.SetMetrics(XenonWindow4)
        XenonWindow1.SetMetrics(XenonWindow6)
    End Sub

    Private Sub XenonWindow1_FontChanged(sender As Object, e As EventArgs) Handles XenonWindow1.FontChanged
        XenonWindow4.Font = XenonWindow1.Font
        XenonWindow6.Font = XenonWindow1.Font
    End Sub

    Private Sub MenuStrip1_FontChanged(sender As Object, e As EventArgs) Handles MenuStrip1.FontChanged
        MenuStrip1.Height = Math.Max(XenonTrackbar9.Value, GetTitleTextHeight(MenuStrip1.Font))
    End Sub

    Public Function GetTitleTextHeight([Font] As Font) As Integer
        Dim TitleTextH As Integer ', TitleTextH_9, TitleTextH_Sum As Integer
        TitleTextH = "ABCabc0123xYz.#".Measure([Font]).Height
        'TitleTextH_9 = "ABCabc0123xYz.#".Measure(New Font([Font].Name, 9, [Font].Style)).Height
        'TitleTextH_Sum = Math.Max(0, TitleTextH - TitleTextH_9)

        Return [Font].Height 'TitleTextH 'TitleTextH_Sum
    End Function

    Public Function GetTitleTextWidth([Font] As Font) As Integer
        Dim TitleTextW As Integer ', TitleTextH_9, TitleTextH_Sum As Integer
        TitleTextW = "ABCabc0123xYz.#".Measure([Font]).Width
        'TitleTextH_9 = "ABCabc0123xYz.#".Measure(New Font([Font].Name, 9, [Font].Style)).Height
        'TitleTextH_Sum = Math.Max(0, TitleTextH - TitleTextH_9)

        Return TitleTextW 'TitleTextH_Sum
    End Function

    Private Sub XenonButton11_Click(sender As Object, e As EventArgs) Handles XenonButton11.Click
        If OpenFileDialog1.ShowDialog = DialogResult.OK Then
            Dim CPx As New CP(CP.Mode.File, OpenFileDialog1.FileName)
            ApplyFromCP(CPx)
            CPx.Dispose()
        End If
    End Sub

    Private Sub XenonButton9_Click(sender As Object, e As EventArgs) Handles XenonButton9.Click
        Dim CPx As New CP(CP.Mode.Registry)
        ApplyFromCP(CPx)
        CPx.Dispose()
    End Sub

    Private Sub XenonButton12_Click(sender As Object, e As EventArgs) Handles XenonButton12.Click
        Dim _Def As CP
        If MainFrm.PreviewConfig = MainFrm.WinVer.Eleven Then
            _Def = New CP_Defaults().Default_Windows11
        ElseIf MainFrm.PreviewConfig = MainFrm.WinVer.Ten Then
            _Def = New CP_Defaults().Default_Windows10
        ElseIf MainFrm.PreviewConfig = MainFrm.WinVer.Eight Then
            _Def = New CP_Defaults().Default_Windows8
        ElseIf MainFrm.PreviewConfig = MainFrm.WinVer.Seven Then
            _Def = New CP_Defaults().Default_Windows7
        Else
            _Def = New CP_Defaults().Default_Windows11
        End If

        ApplyFromCP(_Def)
        _Def.Dispose()
    End Sub

    Private Sub XenonButton13_Click(sender As Object, e As EventArgs) Handles ttl_h.Click
        Dim response As String = InputBox("Input value", Text, sender.Text) : If String.IsNullOrWhiteSpace(response) Then response = sender.Text
        sender.Text = Math.Max(Math.Min(Val(response), XenonTrackbar2.Maximum), XenonTrackbar2.Minimum) : XenonTrackbar2.Value = Val(sender.Text)
    End Sub

    Private Sub XenonButton13_Click_1(sender As Object, e As EventArgs) Handles ttl_w.Click
        Dim response As String = InputBox("Input value", Text, sender.Text) : If String.IsNullOrWhiteSpace(response) Then response = sender.Text
        sender.Text = Math.Max(Math.Min(Val(response), XenonTrackbar3.Maximum), XenonTrackbar3.Minimum) : XenonTrackbar3.Value = Val(sender.Text)
    End Sub

    Private Sub XenonButton14_Click(sender As Object, e As EventArgs) Handles ttl_b.Click
        Dim response As String = InputBox("Input value", Text, sender.Text) : If String.IsNullOrWhiteSpace(response) Then response = sender.Text
        sender.Text = Math.Max(Math.Min(Val(response), XenonTrackbar1.Maximum), XenonTrackbar1.Minimum) : XenonTrackbar1.Value = Val(sender.Text)
    End Sub

    Private Sub XenonButton15_Click(sender As Object, e As EventArgs) Handles ttl_p.Click
        Dim response As String = InputBox("Input value", Text, sender.Text) : If String.IsNullOrWhiteSpace(response) Then response = sender.Text
        sender.Text = Math.Max(Math.Min(Val(response), XenonTrackbar12.Maximum), XenonTrackbar12.Minimum) : XenonTrackbar12.Value = Val(sender.Text)
    End Sub

    Private Sub tw_h_Click(sender As Object, e As EventArgs) Handles tw_h.Click
        Dim response As String = InputBox("Input value", Text, sender.Text) : If String.IsNullOrWhiteSpace(response) Then response = sender.Text
        sender.Text = Math.Max(Math.Min(Val(response), XenonTrackbar14.Maximum), XenonTrackbar14.Minimum) : XenonTrackbar14.Value = Val(sender.Text)
    End Sub

    Private Sub tw_w_Click(sender As Object, e As EventArgs) Handles tw_w.Click
        Dim response As String = InputBox("Input value", Text, sender.Text) : If String.IsNullOrWhiteSpace(response) Then response = sender.Text
        sender.Text = Math.Max(Math.Min(Val(response), XenonTrackbar13.Maximum), XenonTrackbar13.Minimum) : XenonTrackbar13.Value = Val(sender.Text)
    End Sub

    Private Sub i_s_v_Click(sender As Object, e As EventArgs) Handles i_s_v.Click
        Dim response As String = InputBox("Input value", Text, sender.Text) : If String.IsNullOrWhiteSpace(response) Then response = sender.Text
        sender.Text = Math.Max(Math.Min(Val(response), XenonTrackbar4.Maximum), XenonTrackbar4.Minimum) : XenonTrackbar4.Value = Val(sender.Text)
    End Sub

    Private Sub i_s_h_Click(sender As Object, e As EventArgs) Handles i_s_h.Click
        Dim response As String = InputBox("Input value", Text, sender.Text) : If String.IsNullOrWhiteSpace(response) Then response = sender.Text
        sender.Text = Math.Max(Math.Min(Val(response), XenonTrackbar6.Maximum), XenonTrackbar6.Minimum) : XenonTrackbar6.Value = Val(sender.Text)
    End Sub

    Private Sub i_d_s_Click(sender As Object, e As EventArgs) Handles i_d_s.Click
        Dim response As String = InputBox("Input value", Text, sender.Text) : If String.IsNullOrWhiteSpace(response) Then response = sender.Text
        sender.Text = Math.Max(Math.Min(Val(response), XenonTrackbar7.Maximum), XenonTrackbar7.Minimum) : XenonTrackbar7.Value = Val(sender.Text)
    End Sub

    Private Sub i_s_s_Click(sender As Object, e As EventArgs) Handles i_s_s.Click
        Dim response As String = InputBox("Input value", Text, sender.Text) : If String.IsNullOrWhiteSpace(response) Then response = sender.Text
        sender.Text = Math.Max(Math.Min(Val(response), XenonTrackbar5.Maximum), XenonTrackbar5.Minimum) : XenonTrackbar5.Value = Val(sender.Text)
    End Sub

    Private Sub m_h_Click(sender As Object, e As EventArgs) Handles m_h.Click
        Dim response As String = InputBox("Input value", Text, sender.Text) : If String.IsNullOrWhiteSpace(response) Then response = sender.Text
        sender.Text = Math.Max(Math.Min(Val(response), XenonTrackbar9.Maximum), XenonTrackbar9.Minimum) : XenonTrackbar9.Value = Val(sender.Text)
    End Sub

    Private Sub m_w_Click(sender As Object, e As EventArgs) Handles m_w.Click
        Dim response As String = InputBox("Input value", Text, sender.Text) : If String.IsNullOrWhiteSpace(response) Then response = sender.Text
        sender.Text = Math.Max(Math.Min(Val(response), XenonTrackbar8.Maximum), XenonTrackbar8.Minimum) : XenonTrackbar8.Value = Val(sender.Text)
    End Sub

    Private Sub s_h_Click(sender As Object, e As EventArgs) Handles s_h.Click
        Dim response As String = InputBox("Input value", Text, sender.Text) : If String.IsNullOrWhiteSpace(response) Then response = sender.Text
        sender.Text = Math.Max(Math.Min(Val(response), XenonTrackbar11.Maximum), XenonTrackbar11.Minimum) : XenonTrackbar11.Value = Val(sender.Text)
    End Sub

    Private Sub s_w_Click(sender As Object, e As EventArgs) Handles s_w.Click
        Dim response As String = InputBox("Input value", Text, sender.Text) : If String.IsNullOrWhiteSpace(response) Then response = sender.Text
        sender.Text = Math.Max(Math.Min(Val(response), XenonTrackbar10.Maximum), XenonTrackbar10.Minimum) : XenonTrackbar10.Value = Val(sender.Text)
    End Sub

    Private Sub XenonButton13_Click_2(sender As Object, e As EventArgs) Handles XenonButton13.Click
        If XenonWindow1.Visible Then
            RetroWindow1.Visible = True
            XenonWindow1.Visible = False
            XenonWindow2.Visible = False
        Else
            RetroWindow1.Visible = False
            XenonWindow1.Visible = True
            XenonWindow2.Visible = True
        End If
    End Sub
End Class