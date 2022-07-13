Imports WinPaletter.XenonCore

Public Class Whatsnew
    Private Sub Tutorial_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ApplyDarkMode(Me)
    End Sub

    Private Sub XenonButton1_Click(sender As Object, e As EventArgs) Handles XenonButton1.Click
        XenonButton2.Enabled = True
        If TablessControl1.SelectedIndex + 1 <= TablessControl1.TabPages.Count - 1 Then TablessControl1.SelectedIndex += 1
        If TablessControl1.SelectedIndex = TablessControl1.TabPages.Count - 1 Then XenonButton1.Enabled = False
    End Sub

    Private Sub XenonButton2_Click(sender As Object, e As EventArgs) Handles XenonButton2.Click
        XenonButton1.Enabled = True
        If TablessControl1.SelectedIndex > 0 Then TablessControl1.SelectedIndex -= 1
        If TablessControl1.SelectedIndex = 0 Then XenonButton2.Enabled = False
    End Sub

    Private Sub XenonButton3_Click(sender As Object, e As EventArgs) Handles XenonButton3.Click
        Changelog.ShowDialog()
    End Sub

    Private Sub XenonButton8_Click(sender As Object, e As EventArgs) Handles XenonButton8.Click
        LanguageContribute.Show()
    End Sub
End Class