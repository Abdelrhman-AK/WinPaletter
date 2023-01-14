Imports WinPaletter.XenonCore
Public Class Lang_Dashboard
    Private Sub Lang_Dashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Icon = LangJSON_Manage.Icon
        ApplyDarkMode(Me)
    End Sub

    Private Sub XenonButton7_Click(sender As Object, e As EventArgs) Handles XenonButton7.Click
        Me.Close()
    End Sub

    Private Sub XenonButton1_Click(sender As Object, e As EventArgs) Handles XenonButton1.Click
        Me.Close()
        LangJSON_Manage.ShowDialog()
    End Sub

    Private Sub XenonButton2_Click(sender As Object, e As EventArgs) Handles XenonButton2.Click
        Me.Close()
        Lang_JSON_Update.ShowDialog()
    End Sub
End Class