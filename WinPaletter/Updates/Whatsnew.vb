Imports WinPaletter.XenonCore

Public Class Whatsnew
    Private Sub Tutorial_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label2.Text = String.Format(My.Lang.WhatsNewInVersion, My.AppVersion)
        ApplyDarkMode(Me)
        XenonTabControl1.SelectedIndex = 0
        XenonButton1.Text = My.Lang.Next
    End Sub


    Private Sub XenonButton1_Click(sender As Object, e As EventArgs) Handles XenonButton1.Click
        XenonButton2.Enabled = True

        If sender.text = My.Lang.OK Then
            Me.Close()
        Else
            If XenonTabControl1.SelectedIndex + 1 <= XenonTabControl1.TabPages.Count - 1 Then XenonTabControl1.SelectedIndex += 1
            If XenonTabControl1.SelectedIndex = XenonTabControl1.TabPages.Count - 1 Then
                XenonButton1.Text = My.Lang.OK
            End If
        End If

    End Sub

    Private Sub XenonButton2_Click(sender As Object, e As EventArgs) Handles XenonButton2.Click
        XenonButton1.Text = My.Lang.Next
        If XenonTabControl1.SelectedIndex > 0 Then XenonTabControl1.SelectedIndex -= 1
        If XenonTabControl1.SelectedIndex = 0 Then XenonButton2.Enabled = False
    End Sub

    Private Sub XenonButton3_Click(sender As Object, e As EventArgs) Handles XenonButton3.Click
        Process.Start(My.Resources.Link_Changelog)
    End Sub

    Private Sub XenonButton4_Click(sender As Object, e As EventArgs) Handles XenonButton4.Click
        Store_Intro.ShowDialog()
    End Sub

    Private Sub XenonButton5_Click(sender As Object, e As EventArgs) Handles XenonButton5.Click
        Process.Start(My.Resources.Link_StoreGettingStarted)
    End Sub
End Class