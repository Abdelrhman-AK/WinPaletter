Public Class Whatsnew
    Private Sub Tutorial_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label2.Text = String.Format(My.Lang.WhatsNewInVersion, My.AppVersion)
        LoadLanguage
        ApplyStyle(Me)
        TabControl1.SelectedIndex = 0
        Button1.Text = My.Lang.Next
    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Button2.Enabled = True

        If sender.text = My.Lang.OK Then
            Me.Close()
        Else
            If TabControl1.SelectedIndex + 1 <= TabControl1.TabPages.Count - 1 Then TabControl1.SelectedIndex += 1
            If TabControl1.SelectedIndex = TabControl1.TabPages.Count - 1 Then
                Button1.Text = My.Lang.OK
            End If
        End If

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Button1.Text = My.Lang.Next
        If TabControl1.SelectedIndex > 0 Then TabControl1.SelectedIndex -= 1
        If TabControl1.SelectedIndex = 0 Then Button2.Enabled = False
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Process.Start(My.Resources.Link_Changelog)
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Store_Intro.ShowDialog()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Process.Start(My.Resources.Link_Wiki)
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Process.Start(My.Resources.Link_Wiki)
    End Sub
End Class