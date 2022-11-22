Imports WinPaletter.NativeMethods
Imports WinPaletter.XenonCore

Public Class Metrics_Fonts
    Private Sub EditFonts_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        pnl_preview.BackgroundImage = MainFrm.pnl_preview.BackgroundImage
        ApplyDarkMode(Me)
        ApplyFromCP(MainFrm.CP)

        MainFrm.MakeItDoubleBuffered(Panel1)
        MainFrm.MakeItDoubleBuffered(Panel2)
        MainFrm.MakeItDoubleBuffered(Panel3)

        PictureBox1.Image = Shell32.GetSystemIcon(Shell32.SHSTOCKICONID.RECYCLER, Shell32.SHGSI.ICON).ToBitmap
        PictureBox2.Image = Shell32.GetSystemIcon(Shell32.SHSTOCKICONID.FOLDER, Shell32.SHGSI.ICON).ToBitmap
        PictureBox3.Image = Shell32.GetSystemIcon(Shell32.SHSTOCKICONID.APPLICATION, Shell32.SHGSI.ICON).ToBitmap

    End Sub


    Sub ApplyFromCP(CP As CP)
        Label1.Font = CP.Fonts_CaptionFont
        Label2.Font = CP.Fonts_IconFont
        Label3.Font = CP.Fonts_MenuFont
        Label4.Font = CP.Fonts_MessageFont
        Label5.Font = CP.Fonts_SmCaptionFont
        Label6.Font = CP.Fonts_StatusFont

        XenonTrackbar1.Value = CP.Metrics_BorderWidth
        XenonTrackbar2.Value = CP.Metrics_CaptionHeight
        XenonTrackbar3.Value = CP.Metrics_CaptionWidth
        XenonTrackbar6.Value = CP.Metrics_IconSpacing
        XenonTrackbar5.Value = CP.Metrics_IconTitleWrap
        XenonTrackbar4.Value = CP.Metrics_IconVerticalSpacing
        XenonTrackbar9.Value = CP.Metrics_MenuHeight
        XenonTrackbar8.Value = CP.Metrics_MenuWidth
        XenonToggle1.Checked = CP.Metrics_MinAnimate
        XenonTrackbar12.Value = CP.Metrics_PaddedBorderWidth
        XenonTrackbar11.Value = CP.Metrics_ScrollHeight
        XenonTrackbar10.Value = CP.Metrics_ScrollWidth
        XenonTrackbar15.Value = CP.Metrics_ShellIconSize
        XenonTrackbar14.Value = CP.Metrics_SmCaptionHeight
        XenonTrackbar13.Value = CP.Metrics_SmCaptionWidth

    End Sub

    Sub ApplyToCP(CP As CP)
        CP.Fonts_CaptionFont = Label1.Font
        CP.Fonts_IconFont = Label2.Font
        CP.Fonts_MenuFont = Label3.Font
        CP.Fonts_MessageFont = Label4.Font
        CP.Fonts_SmCaptionFont = Label5.Font
        CP.Fonts_StatusFont = Label6.Font

        CP.Metrics_BorderWidth = XenonTrackbar1.Value
        CP.Metrics_CaptionHeight = XenonTrackbar2.Value
        CP.Metrics_CaptionWidth = XenonTrackbar3.Value
        CP.Metrics_IconSpacing = XenonTrackbar6.Value
        CP.Metrics_IconTitleWrap = XenonTrackbar5.Value
        CP.Metrics_IconVerticalSpacing = XenonTrackbar4.Value
        CP.Metrics_MenuHeight = XenonTrackbar9.Value
        CP.Metrics_MenuWidth = XenonTrackbar8.Value
        CP.Metrics_MinAnimate = XenonToggle1.Checked
        CP.Metrics_PaddedBorderWidth = XenonTrackbar12.Value
        CP.Metrics_ScrollHeight = XenonTrackbar11.Value
        CP.Metrics_ScrollWidth = XenonTrackbar10.Value
        CP.Metrics_ShellIconSize = XenonTrackbar15.Value
        CP.Metrics_SmCaptionHeight = XenonTrackbar14.Value
        CP.Metrics_SmCaptionWidth = XenonTrackbar13.Value
    End Sub

    Private Sub XenonButton1_Click(sender As Object, e As EventArgs) Handles XenonButton1.Click
        FontDialog1.Font = Label1.Font
        If FontDialog1.ShowDialog = DialogResult.OK Then Label1.Font = FontDialog1.Font
    End Sub

    Private Sub XenonButton2_Click(sender As Object, e As EventArgs) Handles XenonButton2.Click
        FontDialog1.Font = Label2.Font
        If FontDialog1.ShowDialog = DialogResult.OK Then Label2.Font = FontDialog1.Font
    End Sub

    Private Sub XenonButton3_Click(sender As Object, e As EventArgs) Handles XenonButton3.Click
        FontDialog1.Font = Label3.Font
        If FontDialog1.ShowDialog = DialogResult.OK Then Label3.Font = FontDialog1.Font
    End Sub

    Private Sub XenonButton4_Click(sender As Object, e As EventArgs) Handles XenonButton4.Click
        FontDialog1.Font = Label4.Font
        If FontDialog1.ShowDialog = DialogResult.OK Then Label4.Font = FontDialog1.Font
    End Sub

    Private Sub XenonButton5_Click(sender As Object, e As EventArgs) Handles XenonButton5.Click
        FontDialog1.Font = Label5.Font
        If FontDialog1.ShowDialog = DialogResult.OK Then Label5.Font = FontDialog1.Font
    End Sub

    Private Sub XenonButton6_Click(sender As Object, e As EventArgs) Handles XenonButton6.Click
        FontDialog1.Font = Label6.Font
        If FontDialog1.ShowDialog = DialogResult.OK Then Label6.Font = FontDialog1.Font
    End Sub

    Private Sub XenonButton8_Click(sender As Object, e As EventArgs) Handles XenonButton8.Click
        ApplyToCP(MainFrm.CP)
        Me.Close()
    End Sub

    Private Sub XenonButton7_Click(sender As Object, e As EventArgs) Handles XenonButton7.Click
        Me.Close()
    End Sub

    Private Sub XenonButton10_Click(sender As Object, e As EventArgs) Handles XenonButton10.Click
        Cursor = Cursors.WaitCursor
        Dim CPx As New CP(CP.Mode.Registry)
        ApplyToCP(CPx)
        CPx.Save(CP.SavingMode.Registry)
        Cursor = Cursors.Default
    End Sub


    Private Sub XenonTrackbar1_Scroll(sender As Object) Handles XenonTrackbar1.Scroll
        Label36.Text = sender.Value
        XenonWindow1.Metrics_BorderWidth = sender.Value
    End Sub

    Private Sub XenonTrackbar2_Scroll(sender As Object) Handles XenonTrackbar2.Scroll
        Label35.Text = sender.Value
        XenonWindow1.Metrics_CaptionHeight = sender.Value
    End Sub

    Private Sub XenonTrackbar3_Scroll(sender As Object) Handles XenonTrackbar3.Scroll
        Label34.Text = sender.Value
    End Sub

    Private Sub XenonTrackbar6_Scroll(sender As Object) Handles XenonTrackbar6.Scroll
        Label33.Text = sender.Value
    End Sub

    Private Sub XenonTrackbar5_Scroll(sender As Object) Handles XenonTrackbar5.Scroll
        Label32.Text = sender.Value
    End Sub

    Private Sub XenonTrackbar4_Scroll(sender As Object) Handles XenonTrackbar4.Scroll
        Label31.Text = sender.Value
        Panel2.Top = Panel1.Bottom + sender.Value - 45
    End Sub

    Private Sub XenonTrackbar9_Scroll(sender As Object) Handles XenonTrackbar9.Scroll
        Label30.Text = sender.Value
    End Sub

    Private Sub XenonTrackbar8_Scroll(sender As Object) Handles XenonTrackbar8.Scroll
        Label29.Text = sender.Value
    End Sub

    Private Sub XenonTrackbar12_Scroll(sender As Object) Handles XenonTrackbar12.Scroll
        Label27.Text = sender.Value
        XenonWindow1.Metrics_PaddedBorderWidth = sender.Value
    End Sub

    Private Sub XenonTrackbar11_Scroll(sender As Object) Handles XenonTrackbar11.Scroll
        Label26.Text = sender.Value
    End Sub

    Private Sub XenonTrackbar10_Scroll(sender As Object) Handles XenonTrackbar10.Scroll
        Label25.Text = sender.Value
    End Sub

    Private Sub XenonTrackbar15_Scroll(sender As Object) Handles XenonTrackbar15.Scroll
        Label24.Text = sender.Value
    End Sub

    Private Sub XenonTrackbar14_Scroll(sender As Object) Handles XenonTrackbar14.Scroll
        Label23.Text = sender.Value
    End Sub

    Private Sub XenonTrackbar13_Scroll(sender As Object) Handles XenonTrackbar13.Scroll
        Label15.Text = sender.Value
    End Sub

    Private Sub XenonToggle1_CheckedChanged(sender As Object, e As EventArgs) Handles XenonToggle1.CheckedChanged

    End Sub
End Class