Imports System.ComponentModel
Public Class Lang_Dashboard
    Private Sub Lang_Dashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Icon = Lang_JSON_Manage.Icon
        LoadLanguage
        ApplyStyle(Me)
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Me.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Lang_JSON_Manage.ShowDialog()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Lang_JSON_Update.ShowDialog()
    End Sub

    Private Sub Form_HelpButtonClicked(sender As Object, e As CancelEventArgs) Handles Me.HelpButtonClicked
        Process.Start(My.Resources.Link_Wiki & "/Language-creation")
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Lang_JSON_GUI.ShowDialog()
    End Sub
End Class