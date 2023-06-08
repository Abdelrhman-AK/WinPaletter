Imports System.IO
Imports Devcorp.Controls.VisualStyles
Imports WinPaletter.XenonCore
Public Class VS2Metrics
    Private Sub VS2Win32UI_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ApplyDarkMode(Me)
        Icon = Metrics_Fonts.Icon
    End Sub

    Private Sub XenonButton16_Click(sender As Object, e As EventArgs) Handles XenonButton16.Click
        If OpenFileDialog2.ShowDialog = DialogResult.OK Then
            XenonTextBox1.Text = OpenFileDialog2.FileName
        End If
    End Sub

    Private Sub XenonButton8_Click(sender As Object, e As EventArgs) Handles XenonButton8.Click
        Try
            Dim theme As String = ""

            If Path.GetExtension(XenonTextBox1.Text) = ".theme" Then
                theme = XenonTextBox1.Text

            ElseIf Path.GetExtension(XenonTextBox1.Text) = ".msstyles" Then
                theme = My.PATH_appData & "\VisualStyles\Luna\win32uischeme.theme"
                File.WriteAllText(My.PATH_appData & "\VisualStyles\Luna\win32uischeme.theme", String.Format("[VisualStyles]{1}Path={0}{1}ColorStyle=NormalColor{1}Size=NormalSize", XenonTextBox1.Text, vbCrLf))
            End If

            If File.Exists(XenonTextBox1.Text) AndAlso File.Exists(theme) And Not String.IsNullOrEmpty(theme) Then
                Dim vs As New VisualStyleFile(theme)
                LoadColors(vs.Metrics)
                Win32UI.ApplyRetroPreview()
                Me.Close()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub LoadColors(vs As VisualStyleMetrics)
        If XenonCheckBox1.Checked Then
            Metrics_Fonts.XenonTrackbar2.Value = vs.Sizes.CaptionBarHeight
            Metrics_Fonts.XenonTrackbar11.Value = vs.Sizes.ScrollbarHeight
            Metrics_Fonts.XenonTrackbar10.Value = vs.Sizes.ScrollbarWidth
            Metrics_Fonts.XenonTrackbar14.Value = vs.Sizes.SMCaptionBarHeight
            Metrics_Fonts.XenonTrackbar13.Value = vs.Sizes.SMCaptionBarWidth
        End If

        If XenonCheckBox2.Checked Then
            Metrics_Fonts.Label1.Font = vs.Fonts.CaptionFont
            Metrics_Fonts.XenonWindow1.Font = vs.Fonts.CaptionFont
            Metrics_Fonts.RetroWindow1.Font = vs.Fonts.CaptionFont
            Metrics_Fonts.RetroWindow3.Font = vs.Fonts.CaptionFont
            Metrics_Fonts.RetroWindow5.Font = vs.Fonts.CaptionFont

            Metrics_Fonts.Label2.Font = vs.Fonts.IconTitleFont
            Metrics_Fonts.XenonFakeIcon1.Font = vs.Fonts.IconTitleFont
            Metrics_Fonts.XenonFakeIcon2.Font = vs.Fonts.IconTitleFont
            Metrics_Fonts.XenonFakeIcon3.Font = vs.Fonts.IconTitleFont
            Metrics_Fonts.Label2.Text = vs.Fonts.IconTitleFont.Name

            Metrics_Fonts.Label3.Font = vs.Fonts.MenuFont
            Metrics_Fonts.MenuStrip1.Font = vs.Fonts.MenuFont
            Metrics_Fonts.MenuStrip2.Font = vs.Fonts.MenuFont
            Metrics_Fonts.Label3.Text = vs.Fonts.MenuFont.Name

            Metrics_Fonts.Label5.Font = vs.Fonts.SmallCaptionFont
            Metrics_Fonts.XenonWindow2.Font = vs.Fonts.SmallCaptionFont
            Metrics_Fonts.RetroWindow2.Font = vs.Fonts.SmallCaptionFont
            Metrics_Fonts.Label5.Text = vs.Fonts.SmallCaptionFont.Name

            Metrics_Fonts.Label4.Font = vs.Fonts.MsgBoxFont
            Metrics_Fonts.msgLbl.Font = vs.Fonts.MsgBoxFont
            Metrics_Fonts.Label13.Font = vs.Fonts.MsgBoxFont
            Metrics_Fonts.Label4.Text = vs.Fonts.MsgBoxFont.Name

            Metrics_Fonts.Label6.Font = vs.Fonts.StatusFont
            Metrics_Fonts.statusLbl.Font = vs.Fonts.StatusFont
            Metrics_Fonts.Label14.Font = vs.Fonts.StatusFont
            Metrics_Fonts.Label6.Text = vs.Fonts.StatusFont.Name
            Metrics_Fonts.RetroPanel1.Height = Math.Max(Metrics_Fonts.GetTitleTextHeight(vs.Fonts.StatusFont), 20)
        End If
    End Sub

    Private Sub XenonButton7_Click(sender As Object, e As EventArgs) Handles XenonButton7.Click
        Close()
    End Sub
End Class