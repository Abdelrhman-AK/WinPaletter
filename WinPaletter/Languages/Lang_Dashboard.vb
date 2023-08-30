Imports System.ComponentModel
Imports WinPaletter.XenonCore
Public Class Lang_Dashboard
    Private Sub Lang_Dashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Icon = Lang_JSON_Manage.Icon
        LoadLanguage
        ApplyDarkMode(Me)
    End Sub

    Private Sub XenonButton7_Click(sender As Object, e As EventArgs) Handles XenonButton7.Click
        Me.Close()
    End Sub

    Private Sub XenonButton1_Click(sender As Object, e As EventArgs) Handles XenonButton1.Click
        Lang_JSON_Manage.ShowDialog()
    End Sub

    Private Sub XenonButton2_Click(sender As Object, e As EventArgs) Handles XenonButton2.Click
        Lang_JSON_Update.ShowDialog()
    End Sub

    Private Sub Form_HelpButtonClicked(sender As Object, e As CancelEventArgs) Handles Me.HelpButtonClicked
        Process.Start(My.Resources.Link_Wiki & "/Language-creation")
    End Sub
End Class