Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports WinPaletter.NativeMethods
Imports WinPaletter.XenonCore

Public Class Metrics_Fonts

    Private Sub EditFonts_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        pnl_preview.BackgroundImage = MainFrm.pnl_preview.BackgroundImage
        ApplyDarkMode(Me)
        ApplyFromCP(MainFrm.CP)

        MainFrm.MakeItDoubleBuffered(pnl_preview)
        MainFrm.MakeItDoubleBuffered(XenonFakeIcon1)
        MainFrm.MakeItDoubleBuffered(XenonFakeIcon2)
        MainFrm.MakeItDoubleBuffered(XenonFakeIcon3)

        XenonFakeIcon1.Title = "Recycle Bin"
        XenonFakeIcon2.Title = "New Folder"
        XenonFakeIcon3.Title = "My App"

        XenonFakeIcon1.Icon = Shell32.GetSystemIcon(Shell32.SHSTOCKICONID.RECYCLER, Shell32.SHGSI.ICON)
        XenonFakeIcon2.Icon = Shell32.GetSystemIcon(Shell32.SHSTOCKICONID.FOLDER, Shell32.SHGSI.ICON)
        XenonFakeIcon3.Icon = Shell32.GetSystemIcon(Shell32.SHSTOCKICONID.APPLICATION, Shell32.SHGSI.ICON)
    End Sub


    Sub ApplyFromCP(CP As CP)

        Label1.Font = CP.Fonts_CaptionFont
        XenonWindow1.Font = CP.Fonts_CaptionFont
        RetroWindow1.Font = CP.Fonts_CaptionFont

        Label2.Font = CP.Fonts_IconFont
        XenonFakeIcon1.Font = CP.Fonts_IconFont
        XenonFakeIcon2.Font = CP.Fonts_IconFont
        XenonFakeIcon3.Font = CP.Fonts_IconFont

        Label3.Font = CP.Fonts_MenuFont
        Label14.Font = CP.Fonts_MenuFont
        Label32.Font = CP.Fonts_MenuFont

        Label5.Font = CP.Fonts_SmCaptionFont
        XenonWindow2.Font = CP.Fonts_SmCaptionFont


        Label4.Font = CP.Fonts_MessageFont

        Label6.Font = CP.Fonts_StatusFont


        XenonTrackbar1.Value = CP.Metrics_BorderWidth
        XenonTrackbar2.Value = CP.Metrics_CaptionHeight
        XenonTrackbar3.Value = CP.Metrics_CaptionWidth
        XenonTrackbar6.Value = CP.Metrics_IconSpacing
        XenonTrackbar4.Value = CP.Metrics_IconVerticalSpacing
        XenonTrackbar9.Value = CP.Metrics_MenuHeight
        XenonTrackbar8.Value = CP.Metrics_MenuWidth
        XenonToggle1.Checked = CP.Metrics_MinAnimate
        XenonTrackbar12.Value = CP.Metrics_PaddedBorderWidth
        XenonTrackbar11.Value = CP.Metrics_ScrollHeight
        XenonTrackbar10.Value = CP.Metrics_ScrollWidth
        XenonTrackbar14.Value = CP.Metrics_SmCaptionHeight
        XenonTrackbar13.Value = CP.Metrics_SmCaptionWidth
        XenonTrackbar7.Value = CP.Metrics_DesktopIconSize
        XenonTrackbar5.Value = CP.Metrics_ShellIconSize
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
        CP.Metrics_IconVerticalSpacing = XenonTrackbar4.Value
        CP.Metrics_MenuHeight = XenonTrackbar9.Value
        CP.Metrics_MenuWidth = XenonTrackbar8.Value
        CP.Metrics_MinAnimate = XenonToggle1.Checked
        CP.Metrics_PaddedBorderWidth = XenonTrackbar12.Value
        CP.Metrics_ScrollHeight = XenonTrackbar11.Value
        CP.Metrics_ScrollWidth = XenonTrackbar10.Value
        CP.Metrics_SmCaptionHeight = XenonTrackbar14.Value
        CP.Metrics_SmCaptionWidth = XenonTrackbar13.Value
        CP.Metrics_DesktopIconSize = XenonTrackbar7.Value
        CP.Metrics_ShellIconSize = XenonTrackbar5.Value
    End Sub

    Private Sub XenonButton1_Click(sender As Object, e As EventArgs) Handles XenonButton1.Click
        FontDialog1.Font = Label1.Font
        If FontDialog1.ShowDialog = DialogResult.OK Then
            Label1.Font = FontDialog1.Font
            XenonWindow1.Font = FontDialog1.Font
            RetroWindow1.Font = FontDialog1.Font
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
            Label14.Font = FontDialog1.Font
            Label32.Font = FontDialog1.Font
        End If
    End Sub

    Private Sub XenonButton4_Click(sender As Object, e As EventArgs) Handles XenonButton4.Click
        FontDialog1.Font = Label4.Font
        If FontDialog1.ShowDialog = DialogResult.OK Then Label4.Font = FontDialog1.Font
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




    Private Sub XenonTrackbar2_Scroll(sender As Object) Handles XenonTrackbar2.Scroll
        ttl_h.Text = sender.Value
        XenonWindow1.Metrics_CaptionHeight = sender.Value
        RetroWindow1.Metrics_CaptionHeight = sender.Value

        RetroButton3.Height = sender.Value - 4
        RetroButton4.Height = sender.Value - 4
        RetroButton5.Height = sender.Value - 4

        Dim iFx As Integer
        iFx = Math.Max(RetroButton3.Width, RetroButton3.Height) / 18

        Dim f As New Font("Marlett", 6.8 * iFx)
        RetroButton3.Font = f
        RetroButton4.Font = f
        RetroButton5.Font = f
    End Sub

    Private Sub XenonTrackbar3_Scroll(sender As Object) Handles XenonTrackbar3.Scroll
        ttl_w.Text = sender.Value

        RetroButton3.Width = sender.Value - 2
        RetroButton4.Width = sender.Value - 2
        RetroButton5.Width = sender.Value - 2

        RetroButton3.Left = RetroWindow1.Width - RetroButton3.Width - XenonTrackbar1.Value - XenonTrackbar12.Value - 5
        RetroButton4.Left = RetroButton3.Left - 2 - RetroButton4.Width
        RetroButton5.Left = RetroButton4.Left - RetroButton5.Width

    End Sub

    Private Sub XenonTrackbar6_Scroll(sender As Object) Handles XenonTrackbar6.Scroll
        Label33.Text = sender.Value

        XenonFakeIcon1.Width = sender.Value + 18
        XenonFakeIcon2.Width = sender.Value + 18
        XenonFakeIcon3.Width = sender.Value + 18


        XenonFakeIcon3.Left = XenonFakeIcon1.Right

        XenonFakeIcon3.SendToBack()
        XenonFakeIcon1.BringToFront()
    End Sub

    Private Sub XenonTrackbar4_Scroll(sender As Object) Handles XenonTrackbar4.Scroll
        Label31.Text = sender.Value
        XenonFakeIcon2.Top = XenonFakeIcon1.Bottom + sender.Value - 45
        XenonFakeIcon2.SendToBack()
        XenonFakeIcon1.BringToFront()
    End Sub

    Private Sub XenonTrackbar9_Scroll(sender As Object) Handles XenonTrackbar9.Scroll
        Label30.Text = sender.Value
        Panel1.Height = sender.Value
    End Sub

    Private Sub XenonTrackbar8_Scroll(sender As Object) Handles XenonTrackbar8.Scroll
        Label29.Text = sender.Value
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
        Label26.Text = sender.Value
    End Sub

    Private Sub XenonTrackbar10_Scroll(sender As Object) Handles XenonTrackbar10.Scroll
        Label25.Text = sender.Value
    End Sub

    Private Sub XenonTrackbar14_Scroll(sender As Object) Handles XenonTrackbar14.Scroll
        Label23.Text = sender.Value
        XenonWindow2.Metrics_CaptionHeight = sender.Value
    End Sub

    Private Sub XenonTrackbar13_Scroll(sender As Object) Handles XenonTrackbar13.Scroll
        Label15.Text = sender.Value
    End Sub

    Private Sub XenonTrackbar7_Scroll(sender As Object) Handles XenonTrackbar7.Scroll
        If XenonTrackbar7.Value < XenonTrackbar7.Minimum Or XenonTrackbar7.Value > XenonTrackbar7.Maximum Then Exit Sub

        Label28.Text = sender.Value
        XenonFakeIcon1.IconSize = sender.Value
        XenonFakeIcon2.IconSize = sender.Value
        XenonFakeIcon3.IconSize = sender.Value

        XenonFakeIcon1.Height = sender.Value + 35
        XenonFakeIcon2.Height = sender.Value + 35
        XenonFakeIcon3.Height = sender.Value + 35

        If sender.Value > 64 Then
            XenonFakeIcon1.Width = sender.Value + 28
            XenonFakeIcon2.Width = sender.Value + 28
            XenonFakeIcon3.Width = sender.Value + 28
        Else
            XenonFakeIcon1.Width = XenonTrackbar6.Value + 18
            XenonFakeIcon2.Width = XenonTrackbar6.Value + 18
            XenonFakeIcon3.Width = XenonTrackbar6.Value + 18
        End If

        XenonFakeIcon2.Top = XenonFakeIcon1.Bottom + XenonTrackbar4.Value - 45
        XenonFakeIcon3.Left = XenonFakeIcon1.Right

    End Sub


    Private Sub Label1_FontChanged(sender As Object, e As EventArgs) Handles Label1.FontChanged, Label2.FontChanged, Label3.FontChanged, Label4.FontChanged, Label5.FontChanged, Label6.FontChanged
        DirectCast(sender, Label).Text = DirectCast(sender, Label).Font.FontFamily.Name
    End Sub

    Private Sub XenonTrackbar5_Scroll(sender As Object) Handles XenonTrackbar5.Scroll
        Label13.Text = sender.Value
    End Sub

    Private Sub ttl_h_TextChanged(sender As Object, e As EventArgs) Handles ttl_h.TextChanged
        XenonTrackbar2.Value = Math.Max(Math.Min(Val(sender.Text), XenonTrackbar2.Maximum), XenonTrackbar2.Minimum)
    End Sub

    Private Sub ttl_w_TextChanged(sender As Object, e As EventArgs) Handles ttl_w.TextChanged
        XenonTrackbar3.Value = Math.Max(Math.Min(Val(sender.Text), XenonTrackbar3.Maximum), XenonTrackbar3.Minimum)

    End Sub

    Private Sub ttl_b_TextChanged(sender As Object, e As EventArgs) Handles ttl_b.TextChanged
        XenonTrackbar1.Value = Math.Max(Math.Min(Val(sender.Text), XenonTrackbar1.Maximum), XenonTrackbar1.Minimum)

    End Sub

    Private Sub ttl_p_TextChanged(sender As Object, e As EventArgs) Handles ttl_p.TextChanged
        XenonTrackbar12.Value = Math.Max(Math.Min(Val(sender.Text), XenonTrackbar12.Maximum), XenonTrackbar12.Minimum)
    End Sub
End Class