Imports WinPaletter.XenonCore

Public Class QA
    Private Sub QA_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ApplyDarkMode(Me)
    End Sub

    Private Sub XenonButton1_Click(sender As Object, e As EventArgs) Handles XenonButton1.Click
        SettingsX.ShowDialog()
    End Sub

    Private Sub XenonButton3_Click(sender As Object, e As EventArgs) Handles XenonButton3.Click
        Process.Start("https://github.com/Abdelrhman-AK/WinPaletter/issues")
    End Sub

    Private Sub XenonButton2_Click(sender As Object, e As EventArgs) Handles XenonButton2.Click
        Me.Close()
    End Sub
End Class