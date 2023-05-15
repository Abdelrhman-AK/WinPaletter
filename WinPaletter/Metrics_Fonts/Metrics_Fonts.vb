Imports WinPaletter.NativeMethods
Imports WinPaletter.XenonCore
Imports WinPaletter.PreviewHelpers

Public Class Metrics_Fonts

    Private Sub EditFonts_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        MenuStrip1.Renderer = New StripRenderer  'Removes the inferior white line from menu strip
        MenuStrip2.Renderer = New StripRenderer

        pnl_preview1.BackgroundImage = MainFrm.pnl_preview.BackgroundImage
        pnl_preview2.BackgroundImage = MainFrm.pnl_preview.BackgroundImage
        pnl_preview3.BackgroundImage = MainFrm.pnl_preview.BackgroundImage
        pnl_preview4.BackgroundImage = MainFrm.pnl_preview.BackgroundImage

        Classic_Preview1.BackgroundImage = MainFrm.pnl_preview.BackgroundImage
        Classic_Preview3.BackgroundImage = MainFrm.pnl_preview.BackgroundImage
        Classic_Preview4.BackgroundImage = MainFrm.pnl_preview.BackgroundImage

        CopyCatPreview(XenonWindow1, MainFrm.XenonWindow1)
        CopyCatPreview(XenonWindow2, MainFrm.XenonWindow2)
        CopyCatPreview(XenonWindow4, MainFrm.XenonWindow1)
        CopyCatPreview(XenonWindow6, MainFrm.XenonWindow1)

        SetClassicWindowColors(My.CP, RetroWindow1)
        SetClassicWindowColors(My.CP, RetroWindow2, False)
        SetClassicWindowColors(My.CP, RetroWindow3)
        SetClassicWindowColors(My.CP, RetroWindow5)
        SetClassicPanelColors(My.CP, RetroPanel1)
        SetClassicPanelColors(My.CP, RetroPanel2)
        SetClassicButtonColors(My.CP, RetroButton1)
        SetClassicButtonColors(My.CP, RetroButton2)
        SetClassicButtonColors(My.CP, RetroButton3)
        SetClassicButtonColors(My.CP, RetroButton10)
        SetClassicButtonColors(My.CP, RetroButton11)
        SetClassicButtonColors(My.CP, RetroButton12)

        RetroScrollBar2.ButtonHilight = My.CP.Win32.ButtonHilight
        RetroScrollBar2.BackColor = My.CP.Win32.ButtonFace
        RetroScrollBar1.ButtonHilight = My.CP.Win32.ButtonHilight
        RetroScrollBar1.BackColor = My.CP.Win32.ButtonFace

        Label13.ForeColor = My.CP.Win32.ButtonText
        Label14.ForeColor = My.CP.Win32.ButtonText
        Refresh17BitPreference()

        ApplyDarkMode(Me)
        ApplyFromCP(My.CP)

        DoubleBuffer

        XenonFakeIcon1.Title = "Icon 1"
        XenonFakeIcon2.Title = "Icon 2"
        XenonFakeIcon3.Title = "Icon 3"

        Dim condition0 As Boolean = My.PreviewStyle = WindowStyle.W7 AndAlso My.CP.Windows7.Theme = CP.Structures.Windows7.Themes.Classic
        Dim condition1 As Boolean = My.PreviewStyle = WindowStyle.WVista AndAlso My.CP.WindowsVista.Theme = CP.Structures.Windows7.Themes.Classic
        Dim condition2 As Boolean = My.PreviewStyle = WindowStyle.WXP AndAlso My.CP.WindowsXP.Theme = CP.Structures.WindowsXP.Themes.Classic

        If condition0 Or condition2 Then
            tabs_preview_1.SelectedIndex = 1
            tabs_preview_2.SelectedIndex = 1
            tabs_preview_3.SelectedIndex = 1
        Else
            tabs_preview_1.SelectedIndex = 0
            tabs_preview_2.SelectedIndex = 0
            tabs_preview_3.SelectedIndex = 0
        End If

        XenonFakeIcon1.Icon = MainFrm.Icon                  'My.Resources.fileextension 'Shell32.GetSystemIcon(Shell32.SHSTOCKICONID.RECYCLER, Shell32.SHGSI.ICON)
        XenonFakeIcon2.Icon = My.Resources.fileextension    'My.Resources.settingsfile 'Shell32.GetSystemIcon(Shell32.SHSTOCKICONID.FOLDER, Shell32.SHGSI.ICON)
        XenonFakeIcon3.Icon = My.Resources.ThemesResIcon    'My.Resources.icons8_command_line 'Shell32.GetSystemIcon(Shell32.SHSTOCKICONID.APPLICATION, Shell32.SHGSI.ICON)

        If My.WXP Then
            PictureBox35.Image = SystemIcons.Information.ToBitmap
            PictureBox36.Image = SystemIcons.Information.ToBitmap
        Else
            PictureBox35.Image = DLLFunc.GetSystemIcon(Shell32.SHSTOCKICONID.INFO, Shell32.SHGSI.ICON).ToBitmap
            PictureBox36.Image = DLLFunc.GetSystemIcon(Shell32.SHSTOCKICONID.INFO, Shell32.SHGSI.ICON).ToBitmap
        End If

        Dim Win7 As Boolean = XenonWindow6.Preview = XenonWindow.Preview_Enum.W7Aero Or XenonWindow6.Preview = XenonWindow.Preview_Enum.W7Opaque Or XenonWindow6.Preview = XenonWindow.Preview_Enum.W7Basic
        Dim Win8 As Boolean = XenonWindow6.Preview = XenonWindow.Preview_Enum.W8 Or XenonWindow6.Preview = XenonWindow.Preview_Enum.W8Lite
        Dim WinXP As Boolean = XenonWindow6.Preview = XenonWindow.Preview_Enum.WXP

        If Not Win7 And Not Win8 And Not WinXP Then
            msgLbl.ForeColor = If(XenonWindow6.DarkMode, Color.White, Color.Black)
            MenuStrip1.BackColor = If(XenonWindow6.DarkMode, Color.FromArgb(35, 35, 35), Color.FromArgb(255, 255, 255))
            MenuStrip1.ForeColor = If(XenonWindow6.DarkMode, Color.White, Color.Black)
        Else
            msgLbl.ForeColor = Color.Black
            MenuStrip1.BackColor = Color.FromArgb(255, 255, 255)
            MenuStrip1.ForeColor = Color.Black
        End If

        XenonButton12.Image = MainFrm.XenonButton20.Image.Resize(16, 16)
        XenonAlertBox10.Text = My.Lang.CP_MetricsHighDPIAlert

        XenonAlertBox11.Text = MainFrm.WXP_Alert2.Text
        XenonAlertBox11.Visible = MainFrm.WXP_Alert2.Visible
        XenonAlertBox11.Size = XenonAlertBox11.Parent.Size - New Size(40, 40)
        XenonAlertBox11.Location = New Point(20, 20)

        XenonAlertBox12.Text = XenonAlertBox11.Text
        XenonAlertBox12.Visible = XenonAlertBox11.Visible
        XenonAlertBox12.Size = XenonAlertBox11.Size
        XenonAlertBox12.Location = XenonAlertBox11.Location

        XenonAlertBox13.Text = XenonAlertBox11.Text
        XenonAlertBox13.Visible = XenonAlertBox11.Visible
        XenonAlertBox13.Size = XenonAlertBox11.Size
        XenonAlertBox13.Location = XenonAlertBox11.Location

        MainFrm.Visible = False
    End Sub

    Sub Refresh17BitPreference()

        If My.CP.Win32.EnableTheming Then
            MenuStrip2.BackColor = My.CP.Win32.MenuBar
        Else
            MenuStrip2.BackColor = My.CP.Win32.Menu
        End If

        ToolStripMenuItem1.ForeColor = My.CP.Win32.MenuText
        ToolStripMenuItem4.ForeColor = My.CP.Win32.MenuText

    End Sub

    Sub CopyCatPreview([ToXenonWindow] As XenonWindow, [FromXenonWindow] As XenonWindow)
        [ToXenonWindow].Active = [FromXenonWindow].Active
        [ToXenonWindow].AccentColor_Active = [FromXenonWindow].AccentColor_Active
        [ToXenonWindow].AccentColor2_Active = [FromXenonWindow].AccentColor2_Active
        [ToXenonWindow].AccentColor_Inactive = [FromXenonWindow].AccentColor_Inactive
        [ToXenonWindow].AccentColor2_Inactive = [FromXenonWindow].AccentColor2_Inactive
        [ToXenonWindow].AccentColor_Enabled = [FromXenonWindow].AccentColor_Enabled
        [ToXenonWindow].BackColor = [FromXenonWindow].BackColor
        [ToXenonWindow].DarkMode = [FromXenonWindow].DarkMode
        [ToXenonWindow].Font = [FromXenonWindow].Font
        [ToXenonWindow].ForeColor = [FromXenonWindow].ForeColor
        [ToXenonWindow].Preview = [FromXenonWindow].Preview
        [ToXenonWindow].Win7Alpha = [FromXenonWindow].Win7Alpha
        [ToXenonWindow].Win7ColorBal = [FromXenonWindow].Win7ColorBal
        [ToXenonWindow].Win7GlowBal = [FromXenonWindow].Win7GlowBal
        [ToXenonWindow].Win7Noise = [FromXenonWindow].Win7Noise
        [ToXenonWindow].Padding = [FromXenonWindow].Padding
        [ToXenonWindow].Shadow = [FromXenonWindow].Shadow
        [ToXenonWindow].WinVista = [FromXenonWindow].WinVista
    End Sub

    Sub ApplyFromCP(CP As CP)
        MetricsEnabled.Checked = CP.MetricsFonts.Enabled

        Label1.Font = CP.MetricsFonts.CaptionFont
        XenonWindow1.Font = CP.MetricsFonts.CaptionFont
        RetroWindow1.Font = CP.MetricsFonts.CaptionFont
        RetroWindow3.Font = CP.MetricsFonts.CaptionFont
        RetroWindow5.Font = CP.MetricsFonts.CaptionFont

        Label1.Text = CP.MetricsFonts.CaptionFont.Name

        Label2.Font = CP.MetricsFonts.IconFont
        XenonFakeIcon1.Font = CP.MetricsFonts.IconFont
        XenonFakeIcon2.Font = CP.MetricsFonts.IconFont
        XenonFakeIcon3.Font = CP.MetricsFonts.IconFont
        Label2.Text = CP.MetricsFonts.IconFont.Name

        Label3.Font = CP.MetricsFonts.MenuFont
        MenuStrip1.Font = CP.MetricsFonts.MenuFont
        MenuStrip2.Font = CP.MetricsFonts.MenuFont
        Label3.Text = CP.MetricsFonts.MenuFont.Name

        Label5.Font = CP.MetricsFonts.SmCaptionFont
        XenonWindow2.Font = CP.MetricsFonts.SmCaptionFont
        RetroWindow2.Font = CP.MetricsFonts.SmCaptionFont
        Label5.Text = CP.MetricsFonts.SmCaptionFont.Name

        Label4.Font = CP.MetricsFonts.MessageFont
        msgLbl.Font = CP.MetricsFonts.MessageFont
        Label13.Font = CP.MetricsFonts.MessageFont
        Label4.Text = CP.MetricsFonts.MessageFont.Name

        Label6.Font = CP.MetricsFonts.StatusFont
        statusLbl.Font = CP.MetricsFonts.StatusFont
        Label14.Font = CP.MetricsFonts.StatusFont
        Label6.Text = CP.MetricsFonts.StatusFont.Name
        RetroPanel1.Height = Math.Max(GetTitleTextHeight(CP.MetricsFonts.StatusFont), 20)

        XenonTextBox1.Text = CP.MetricsFonts.FontSubstitute_MSShellDlg
        XenonTextBox2.Text = CP.MetricsFonts.FontSubstitute_MSShellDlg2
        XenonTextBox3.Text = CP.MetricsFonts.FontSubstitute_SegoeUI

        XenonTrackbar1.Value = CP.MetricsFonts.BorderWidth
        XenonTrackbar2.Value = CP.MetricsFonts.CaptionHeight
        XenonTrackbar3.Value = CP.MetricsFonts.CaptionWidth
        XenonTrackbar6.Value = CP.MetricsFonts.IconSpacing
        XenonTrackbar4.Value = CP.MetricsFonts.IconVerticalSpacing
        XenonTrackbar9.Value = CP.MetricsFonts.MenuHeight
        XenonTrackbar8.Value = CP.MetricsFonts.MenuWidth
        XenonTrackbar12.Value = CP.MetricsFonts.PaddedBorderWidth
        XenonTrackbar11.Value = CP.MetricsFonts.ScrollHeight
        XenonTrackbar10.Value = CP.MetricsFonts.ScrollWidth
        XenonTrackbar14.Value = CP.MetricsFonts.SmCaptionHeight
        XenonTrackbar13.Value = CP.MetricsFonts.SmCaptionWidth
        XenonTrackbar7.Value = CP.MetricsFonts.DesktopIconSize
        XenonTrackbar5.Value = CP.MetricsFonts.ShellIconSize
        XenonTrackbar15.Value = CP.MetricsFonts.ShellSmallIconSize

        RetroWindow1.Metrics_CaptionWidth = CP.MetricsFonts.CaptionWidth
        RetroWindow3.Metrics_CaptionWidth = CP.MetricsFonts.CaptionWidth
        RetroWindow5.Metrics_CaptionWidth = CP.MetricsFonts.CaptionWidth

        If CP.WindowsEffects.IconsShadow Then
            XenonFakeIcon1.ColorGlow = Color.FromArgb(75, 0, 0, 0)
        Else
            XenonFakeIcon1.ColorGlow = Color.FromArgb(0, 0, 0, 0)
        End If
        XenonFakeIcon2.ColorGlow = XenonFakeIcon1.ColorGlow
        XenonFakeIcon3.ColorGlow = XenonFakeIcon1.ColorGlow

        RetroWindow1.Refresh()
        RetroWindow2.Refresh()
        RetroWindow3.Refresh()
        RetroWindow5.Refresh()

        Dim theme As CtrlTheme, statusBackColor, StatusForeColor As Color

        If My.PreviewStyle = WindowStyle.W11 Then

            If CP.Windows11.AppMode_Light Then
                theme = CtrlTheme.Default
                StatusForeColor = Color.Black
                statusBackColor = Color.White
            Else
                theme = CtrlTheme.DarkExplorer
                StatusForeColor = Color.White
                statusBackColor = Color.FromArgb(28, 28, 28)
            End If

        ElseIf My.PreviewStyle = WindowStyle.W10 Then

            If CP.Windows10.AppMode_Light Then
                theme = CtrlTheme.Default
                StatusForeColor = Color.Black
                statusBackColor = Color.White
            Else
                theme = CtrlTheme.DarkExplorer
                StatusForeColor = Color.White
                statusBackColor = Color.FromArgb(28, 28, 28)
            End If

        Else
            StatusForeColor = Color.Black
            statusBackColor = Color.White
            theme = CtrlTheme.Default
        End If

        SetTheme(MenuStrip1.Handle, theme)
        SetTheme(VScrollBar1.Handle, theme)
        SetTheme(HScrollBar1.Handle, theme)
        SetTheme(StatusStrip1.Handle, theme)

        statusLbl.ForeColor = StatusForeColor
        StatusStrip1.BackColor = statusBackColor
    End Sub

    Sub ApplyToCP(CP As CP)
        CP.MetricsFonts.Enabled = MetricsEnabled.Checked

        CP.MetricsFonts.CaptionFont = Label1.Font
        CP.MetricsFonts.IconFont = Label2.Font
        CP.MetricsFonts.MenuFont = Label3.Font
        CP.MetricsFonts.MessageFont = Label4.Font
        CP.MetricsFonts.SmCaptionFont = Label5.Font
        CP.MetricsFonts.StatusFont = Label6.Font

        CP.MetricsFonts.BorderWidth = XenonTrackbar1.Value
        CP.MetricsFonts.CaptionHeight = XenonTrackbar2.Value
        CP.MetricsFonts.CaptionWidth = XenonTrackbar3.Value
        CP.MetricsFonts.IconSpacing = XenonTrackbar6.Value
        CP.MetricsFonts.IconVerticalSpacing = XenonTrackbar4.Value
        CP.MetricsFonts.MenuHeight = XenonTrackbar9.Value
        CP.MetricsFonts.MenuWidth = XenonTrackbar8.Value
        CP.MetricsFonts.PaddedBorderWidth = XenonTrackbar12.Value
        CP.MetricsFonts.ScrollHeight = XenonTrackbar11.Value
        CP.MetricsFonts.ScrollWidth = XenonTrackbar10.Value
        CP.MetricsFonts.SmCaptionHeight = XenonTrackbar14.Value
        CP.MetricsFonts.SmCaptionWidth = XenonTrackbar13.Value
        CP.MetricsFonts.DesktopIconSize = XenonTrackbar7.Value
        CP.MetricsFonts.ShellIconSize = XenonTrackbar5.Value
        CP.MetricsFonts.ShellSmallIconSize = XenonTrackbar15.Value

        CP.MetricsFonts.FontSubstitute_MSShellDlg = XenonTextBox1.Text
        CP.MetricsFonts.FontSubstitute_MSShellDlg2 = XenonTextBox2.Text
        CP.MetricsFonts.FontSubstitute_SegoeUI = XenonTextBox3.Text
    End Sub

    Private Sub XenonButton1_Click(sender As Object, e As EventArgs) Handles XenonButton1.Click
        FontDialog1.Font = Label1.Font
        If FontDialog1.ShowDialog = DialogResult.OK Then
            Label1.Font = FontDialog1.Font
            XenonWindow1.Font = FontDialog1.Font
            RetroWindow1.Font = FontDialog1.Font
            XenonWindow4.Font = FontDialog1.Font
            RetroWindow3.Font = FontDialog1.Font
            XenonWindow6.Font = FontDialog1.Font
            RetroWindow5.Font = FontDialog1.Font
            Label1.Text = FontDialog1.Font.Name
            XenonWindow1.Refresh()
            RetroWindow1.Refresh()
            XenonWindow4.Refresh()
            RetroWindow3.Refresh()
            XenonWindow6.Refresh()
            RetroWindow5.Refresh()
        End If
    End Sub

    Private Sub XenonButton2_Click(sender As Object, e As EventArgs) Handles XenonButton2.Click
        FontDialog1.Font = Label2.Font
        If FontDialog1.ShowDialog = DialogResult.OK Then
            Label2.Font = FontDialog1.Font
            XenonFakeIcon1.Font = FontDialog1.Font
            XenonFakeIcon2.Font = FontDialog1.Font
            XenonFakeIcon3.Font = FontDialog1.Font
            Label2.Text = FontDialog1.Font.Name

        End If
    End Sub

    Private Sub XenonButton3_Click(sender As Object, e As EventArgs) Handles XenonButton3.Click
        FontDialog1.Font = Label3.Font
        If FontDialog1.ShowDialog = DialogResult.OK Then
            Label3.Font = FontDialog1.Font
            MenuStrip1.Font = FontDialog1.Font
            MenuStrip2.Font = FontDialog1.Font
            Label3.Text = FontDialog1.Font.Name
        End If
    End Sub

    Private Sub XenonButton4_Click(sender As Object, e As EventArgs) Handles XenonButton4.Click
        FontDialog1.Font = Label4.Font
        If FontDialog1.ShowDialog = DialogResult.OK Then
            Label4.Font = FontDialog1.Font
            msgLbl.Font = FontDialog1.Font
            Label13.Font = FontDialog1.Font
            Label4.Text = FontDialog1.Font.Name

        End If
    End Sub

    Private Sub XenonButton5_Click(sender As Object, e As EventArgs) Handles XenonButton5.Click
        FontDialog1.Font = Label5.Font
        If FontDialog1.ShowDialog = DialogResult.OK Then
            Label5.Font = FontDialog1.Font
            XenonWindow2.Font = FontDialog1.Font
            RetroWindow2.Font = FontDialog1.Font
            Label5.Text = FontDialog1.Font.Name
        End If
    End Sub

    Private Sub XenonButton6_Click(sender As Object, e As EventArgs) Handles XenonButton6.Click
        FontDialog1.Font = Label6.Font
        If FontDialog1.ShowDialog = DialogResult.OK Then
            Label6.Font = FontDialog1.Font
            Label14.Font = FontDialog1.Font
            statusLbl.Font = FontDialog1.Font
            Label6.Text = FontDialog1.Font.Name
            RetroPanel1.Height = Math.Max(GetTitleTextHeight(FontDialog1.Font), 20)
        End If
    End Sub

    Private Sub XenonButton8_Click(sender As Object, e As EventArgs) Handles XenonButton8.Click
        ApplyToCP(My.CP)
        Me.Close()
        SetModernWindowMetrics(My.CP, MainFrm.XenonWindow1)
        SetModernWindowMetrics(My.CP, MainFrm.XenonWindow2)
        SetClassicWindowMetrics(My.CP, MainFrm.ClassicWindow1)
        SetClassicWindowMetrics(My.CP, MainFrm.ClassicWindow2)

    End Sub

    Private Sub XenonButton7_Click(sender As Object, e As EventArgs) Handles XenonButton7.Click
        Me.Close()
    End Sub

    Private Sub XenonButton10_Click(sender As Object, e As EventArgs) Handles XenonButton10.Click
        Cursor = Cursors.WaitCursor
        Dim CPx As New CP(CP.CP_Type.Registry)
        ApplyToCP(CPx)
        ApplyToCP(My.CP)
        SetModernWindowMetrics(CPx, MainFrm.XenonWindow1)
        SetModernWindowMetrics(CPx, MainFrm.XenonWindow2)
        SetClassicWindowMetrics(CPx, MainFrm.ClassicWindow1)
        SetClassicWindowMetrics(CPx, MainFrm.ClassicWindow2)

        CPx.MetricsFonts.Apply()
        CPx.Dispose()
        Cursor = Cursors.Default
    End Sub

    Private Sub XenonTrackbar2_Scroll(sender As Object) Handles XenonTrackbar2.Scroll
        ttl_h.Text = sender.Value.ToString

        XenonWindow1.Metrics_CaptionHeight = sender.Value
        RetroWindow1.Metrics_CaptionHeight = sender.Value
        RetroWindow3.Metrics_CaptionHeight = sender.Value
        RetroWindow5.Metrics_CaptionHeight = sender.Value
    End Sub

    Private Sub XenonTrackbar3_Scroll(sender As Object) Handles XenonTrackbar3.Scroll
        ttl_w.Text = sender.Value
        RetroWindow1.Metrics_CaptionWidth = sender.Value
        RetroWindow3.Metrics_CaptionWidth = sender.Value
        RetroWindow5.Metrics_CaptionWidth = sender.Value
    End Sub


    Private Sub XenonTrackbar9_Scroll(sender As Object) Handles XenonTrackbar9.Scroll
        m_h.Text = sender.Value
        MenuStrip1.Height = Math.Max(sender.Value, GetTitleTextHeight(MenuStrip1.Font))
        MenuStrip2.Height = MenuStrip1.Height
        RetroPanel2.Refresh()
    End Sub

    Private Sub XenonTrackbar8_Scroll(sender As Object) Handles XenonTrackbar8.Scroll
        m_w.Text = sender.Value
    End Sub

    Private Sub XenonTrackbar1_Scroll(sender As Object) Handles XenonTrackbar1.Scroll
        ttl_b.Text = sender.Value
        XenonWindow1.Metrics_BorderWidth = sender.Value
        RetroWindow1.Metrics_BorderWidth = sender.Value
        RetroWindow2.Metrics_BorderWidth = sender.Value
        RetroWindow3.Metrics_BorderWidth = sender.Value
        RetroWindow5.Metrics_BorderWidth = sender.Value
    End Sub

    Private Sub XenonTrackbar12_Scroll(sender As Object) Handles XenonTrackbar12.Scroll
        ttl_p.Text = sender.Value
        XenonWindow1.Metrics_PaddedBorderWidth = sender.Value
        RetroWindow1.Metrics_PaddedBorderWidth = sender.Value
        RetroWindow2.Metrics_PaddedBorderWidth = sender.Value
        RetroWindow3.Metrics_PaddedBorderWidth = sender.Value
        RetroWindow5.Metrics_PaddedBorderWidth = sender.Value
    End Sub

    Private Sub XenonTrackbar11_Scroll(sender As Object) Handles XenonTrackbar11.Scroll
        s_h.Text = sender.Value
        HScrollBar1.Height = sender.Value
        RetroScrollBar1.Height = sender.Value
        RetroButton1.Height = sender.Value
        RetroScrollBar1.Refresh()
    End Sub

    Private Sub XenonTrackbar10_Scroll(sender As Object) Handles XenonTrackbar10.Scroll
        s_w.Text = sender.Value
        VScrollBar1.Width = sender.Value
        RetroScrollBar2.Width = sender.Value
        RetroButton12.Width = sender.Value
        RetroScrollBar2.Refresh()
    End Sub

    Private Sub XenonTrackbar14_Scroll(sender As Object) Handles XenonTrackbar14.Scroll
        tw_h.Text = sender.Value
        XenonWindow2.Metrics_CaptionHeight = sender.Value
        RetroWindow2.Metrics_CaptionHeight = sender.Value
    End Sub

    Private Sub XenonTrackbar13_Scroll(sender As Object) Handles XenonTrackbar13.Scroll
        tw_w.Text = sender.Value
        RetroWindow2.Metrics_CaptionWidth = sender.Value
    End Sub

    Private Sub XenonTrackbar7_Scroll(sender As Object) Handles XenonTrackbar7.Scroll
        If XenonTrackbar7.Value < XenonTrackbar7.Minimum Then
            XenonTrackbar7.Value = XenonTrackbar7.Minimum
        End If

        If XenonTrackbar7.Value > XenonTrackbar7.Maximum Then
            XenonTrackbar7.Value = XenonTrackbar7.Maximum
        End If

        i_d_s.Text = sender.Value
        XenonFakeIcon1.IconSize = sender.Value
        XenonFakeIcon2.IconSize = sender.Value
        XenonFakeIcon3.IconSize = sender.Value

        XenonFakeIcon1.Height = sender.Value + 35
        XenonFakeIcon2.Height = sender.Value + 35
        XenonFakeIcon3.Height = sender.Value + 35
        XenonFakeIcon2.Top = XenonFakeIcon1.Bottom + (XenonTrackbar4.Value - 45)  '(((XenonTrackbar4.Value - 5) / 2) + 5)

        XenonFakeIcon1.Width = sender.Value + 15 + XenonTrackbar6.Value / 16 '18
        XenonFakeIcon2.Width = sender.Value + 15 + XenonTrackbar6.Value / 16
        XenonFakeIcon3.Width = sender.Value + 15 + XenonTrackbar6.Value / 16
        XenonFakeIcon3.Left = XenonFakeIcon1.Right + 2

    End Sub

    Private Sub XenonTrackbar6_Scroll(sender As Object) Handles XenonTrackbar6.Scroll
        i_s_h.Text = sender.Value

        XenonFakeIcon1.Width = XenonTrackbar7.Value + 15 + sender.Value / 16 '18
        XenonFakeIcon2.Width = XenonTrackbar7.Value + 15 + sender.Value / 16
        XenonFakeIcon3.Width = XenonTrackbar7.Value + 15 + sender.Value / 16
        XenonFakeIcon3.Left = XenonFakeIcon1.Right + 2

        XenonFakeIcon3.SendToBack()
        XenonFakeIcon1.BringToFront()
    End Sub

    Private Sub XenonTrackbar4_Scroll(sender As Object) Handles XenonTrackbar4.Scroll
        i_s_v.Text = sender.Value

        XenonFakeIcon2.Top = XenonFakeIcon1.Bottom + (XenonTrackbar4.Value - 45)

        XenonFakeIcon2.SendToBack()
        XenonFakeIcon1.BringToFront()
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
        Using _Def As CP = CP_Defaults.From(My.PreviewStyle)
            ApplyFromCP(_Def)
        End Using
    End Sub

    Private Sub XenonButton13_Click(sender As Object, e As EventArgs) Handles ttl_h.Click
        Dim response As String = InputBox(My.Lang.InputValue, sender.text, My.Lang.ItMustBeNumerical)
        sender.Text = Math.Max(Math.Min(Val(response), XenonTrackbar2.Maximum), XenonTrackbar2.Minimum) : XenonTrackbar2.Value = Val(sender.Text)
    End Sub

    Private Sub XenonButton13_Click_1(sender As Object, e As EventArgs) Handles ttl_w.Click
        Dim response As String = InputBox(My.Lang.InputValue, sender.text, My.Lang.ItMustBeNumerical)
        sender.Text = Math.Max(Math.Min(Val(response), XenonTrackbar3.Maximum), XenonTrackbar3.Minimum) : XenonTrackbar3.Value = Val(sender.Text)
    End Sub

    Private Sub XenonButton14_Click(sender As Object, e As EventArgs) Handles ttl_b.Click
        Dim response As String = InputBox(My.Lang.InputValue, sender.text, My.Lang.ItMustBeNumerical)
        sender.Text = Math.Max(Math.Min(Val(response), XenonTrackbar1.Maximum), XenonTrackbar1.Minimum) : XenonTrackbar1.Value = Val(sender.Text)
    End Sub

    Private Sub XenonButton15_Click(sender As Object, e As EventArgs) Handles ttl_p.Click
        Dim response As String = InputBox(My.Lang.InputValue, sender.text, My.Lang.ItMustBeNumerical)
        sender.Text = Math.Max(Math.Min(Val(response), XenonTrackbar12.Maximum), XenonTrackbar12.Minimum) : XenonTrackbar12.Value = Val(sender.Text)
    End Sub

    Private Sub Tw_h_Click(sender As Object, e As EventArgs) Handles tw_h.Click
        Dim response As String = InputBox(My.Lang.InputValue, sender.text, My.Lang.ItMustBeNumerical)
        sender.Text = Math.Max(Math.Min(Val(response), XenonTrackbar14.Maximum), XenonTrackbar14.Minimum) : XenonTrackbar14.Value = Val(sender.Text)
    End Sub

    Private Sub Tw_w_Click(sender As Object, e As EventArgs) Handles tw_w.Click
        Dim response As String = InputBox(My.Lang.InputValue, sender.text, My.Lang.ItMustBeNumerical)
        sender.Text = Math.Max(Math.Min(Val(response), XenonTrackbar13.Maximum), XenonTrackbar13.Minimum) : XenonTrackbar13.Value = Val(sender.Text)
    End Sub

    Private Sub I_s_v_Click(sender As Object, e As EventArgs) Handles i_s_v.Click
        Dim response As String = InputBox(My.Lang.InputValue, sender.text, My.Lang.ItMustBeNumerical)
        sender.Text = Math.Max(Math.Min(Val(response), XenonTrackbar4.Maximum), XenonTrackbar4.Minimum) : XenonTrackbar4.Value = Val(sender.Text)
    End Sub

    Private Sub I_s_h_Click(sender As Object, e As EventArgs) Handles i_s_h.Click
        Dim response As String = InputBox(My.Lang.InputValue, sender.text, My.Lang.ItMustBeNumerical)
        sender.Text = Math.Max(Math.Min(Val(response), XenonTrackbar6.Maximum), XenonTrackbar6.Minimum) : XenonTrackbar6.Value = Val(sender.Text)
    End Sub

    Private Sub I_d_s_Click(sender As Object, e As EventArgs) Handles i_d_s.Click
        Dim response As String = InputBox(My.Lang.InputValue, sender.text, My.Lang.ItMustBeNumerical)
        sender.Text = Math.Max(Math.Min(Val(response), XenonTrackbar7.Maximum), XenonTrackbar7.Minimum) : XenonTrackbar7.Value = Val(sender.Text)
    End Sub

    Private Sub I_s_s_Click(sender As Object, e As EventArgs) Handles i_s_s.Click
        Dim response As String = InputBox(My.Lang.InputValue, sender.text, My.Lang.ItMustBeNumerical)
        sender.Text = Math.Max(Math.Min(Val(response), XenonTrackbar5.Maximum), XenonTrackbar5.Minimum) : XenonTrackbar5.Value = Val(sender.Text)
    End Sub

    Private Sub Mh_Click(sender As Object, e As EventArgs) Handles m_h.Click
        Dim response As String = InputBox(My.Lang.InputValue, sender.text, My.Lang.ItMustBeNumerical)
        sender.Text = Math.Max(Math.Min(Val(response), XenonTrackbar9.Maximum), XenonTrackbar9.Minimum) : XenonTrackbar9.Value = Val(sender.Text)
    End Sub

    Private Sub Mw_Click(sender As Object, e As EventArgs) Handles m_w.Click
        Dim response As String = InputBox(My.Lang.InputValue, sender.text, My.Lang.ItMustBeNumerical)
        sender.Text = Math.Max(Math.Min(Val(response), XenonTrackbar8.Maximum), XenonTrackbar8.Minimum) : XenonTrackbar8.Value = Val(sender.Text)
    End Sub

    Private Sub Sh_Click(sender As Object, e As EventArgs) Handles s_h.Click
        Dim response As String = InputBox(My.Lang.InputValue, sender.text, My.Lang.ItMustBeNumerical)
        sender.Text = Math.Max(Math.Min(Val(response), XenonTrackbar11.Maximum), XenonTrackbar11.Minimum) : XenonTrackbar11.Value = Val(sender.Text)
    End Sub

    Private Sub Sw_Click(sender As Object, e As EventArgs) Handles s_w.Click
        Dim response As String = InputBox(My.Lang.InputValue, sender.text, My.Lang.ItMustBeNumerical)
        sender.Text = Math.Max(Math.Min(Val(response), XenonTrackbar10.Maximum), XenonTrackbar10.Minimum) : XenonTrackbar10.Value = Val(sender.Text)
    End Sub

    Private Sub XenonButton13_Click_2(sender As Object, e As EventArgs) Handles XenonButton13.Click
        If tabs_preview_1.SelectedIndex = 0 Then
            tabs_preview_1.SelectedIndex = 1
            tabs_preview_2.SelectedIndex = 1
            tabs_preview_3.SelectedIndex = 1
        Else
            tabs_preview_1.SelectedIndex = 0
            tabs_preview_2.SelectedIndex = 0
            tabs_preview_3.SelectedIndex = 0
        End If
    End Sub

    Private Sub Metrics_Fonts_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        MainFrm.Visible = True
    End Sub

    Private Sub XenonButton14_Click_1(sender As Object, e As EventArgs) Handles XenonButton14.Click
        Dim F As New Font(XenonTextBox1.Text, 9, FontStyle.Regular)
        FontDialog2.Font = F
        If FontDialog2.ShowDialog = DialogResult.OK Then
            XenonTextBox1.Text = FontDialog2.Font.Name
        End If
    End Sub

    Private Sub XenonButton15_Click_1(sender As Object, e As EventArgs) Handles XenonButton15.Click
        Dim F As New Font(XenonTextBox2.Text, 9, FontStyle.Regular)
        FontDialog2.Font = F
        If FontDialog2.ShowDialog = DialogResult.OK Then
            XenonTextBox2.Text = FontDialog2.Font.Name
        End If
    End Sub

    Private Sub XenonButton17_Click(sender As Object, e As EventArgs) Handles XenonButton17.Click
        Dim F As New Font(XenonTextBox3.Text, 9, FontStyle.Regular)
        FontDialog2.Font = F
        If FontDialog2.ShowDialog = DialogResult.OK Then
            XenonTextBox3.Text = FontDialog2.Font.Name
        End If
    End Sub

    Private Sub XenonButton16_Click_1(sender As Object, e As EventArgs) Handles XenonButton16.Click
        XenonTextBox1.Text = "Microsoft Sans Serif"
    End Sub

    Private Sub XenonButton18_Click(sender As Object, e As EventArgs) Handles XenonButton18.Click
        XenonTextBox2.Text = "Tahoma"
    End Sub

    Private Sub XenonButton19_Click(sender As Object, e As EventArgs) Handles XenonButton19.Click
        XenonTextBox3.Text = ""
    End Sub

    Private Sub XenonTextBox1_TextChanged(sender As Object, e As EventArgs) Handles XenonTextBox1.TextChanged

        If CP.IsFontInstalled(sender.Text.ToString, FontStyle.Regular) Then
            sender.Font = New Font(sender.Text.ToString, 9, FontStyle.Regular)
        Else
            sender.Font = New Font("Microsoft Sans Serif", 9, FontStyle.Regular)
        End If

    End Sub

    Private Sub XenonTextBox2_TextChanged(sender As Object, e As EventArgs) Handles XenonTextBox2.TextChanged

        If CP.IsFontInstalled(sender.Text.ToString, FontStyle.Regular) Then
            sender.Font = New Font(sender.Text.ToString, 9, FontStyle.Regular)
        Else
            sender.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If

    End Sub

    Private Sub XenonTextBox3_TextChanged(sender As Object, e As EventArgs) Handles XenonTextBox3.TextChanged

        If CP.IsFontInstalled(sender.Text.ToString, FontStyle.Regular) Then
            sender.Font = New Font(sender.Text.ToString, 9, FontStyle.Regular)
        Else
            sender.Font = New Font("Segoe UI", 9, FontStyle.Regular)
        End If

    End Sub

    Private Sub XenonButton20_Click(sender As Object, e As EventArgs) Handles XenonButton20.Click
        VS2Metrics.ShowDialog()
    End Sub

    Private Sub MetricsEnabled_CheckedChanged(sender As Object, e As EventArgs) Handles MetricsEnabled.CheckedChanged
        checker_img.Image = If(sender.Checked, My.Resources.checker_enabled, My.Resources.checker_disabled)
    End Sub

    Private Sub XenonTrackbar15_Scroll(sender As Object) Handles XenonTrackbar15.Scroll
        i_s_s_s.Text = sender.Value
    End Sub

    Private Sub I_s_s_s_Click(sender As Object, e As EventArgs) Handles i_s_s_s.Click
        Dim response As String = InputBox(My.Lang.InputValue, sender.text, My.Lang.ItMustBeNumerical)
        sender.Text = Math.Max(Math.Min(Val(response), XenonTrackbar15.Maximum), XenonTrackbar15.Minimum) : XenonTrackbar15.Value = Val(sender.Text)
    End Sub
End Class