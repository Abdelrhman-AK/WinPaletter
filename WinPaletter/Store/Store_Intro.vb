Imports System.ComponentModel

Public Class Store_Intro
    Private Sub Store_Intro_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CheckBox1.Checked = My.Settings.Store.ShowTips
        LoadLanguage
        ApplyStyle(Me)
        Icon = Store.Icon
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TablessControl1.SelectedIndex + 1 <= TablessControl1.TabPages.Count - 1 Then TablessControl1.SelectedIndex += 1

        If Button1.Text = My.Lang.Finish Then
            Close()
            TablessControl1.SelectedIndex = 0
            Button1.Text = My.Lang.Next
        End If

        If TablessControl1.SelectedIndex = TablessControl1.TabPages.Count - 1 Then
            Button1.Text = My.Lang.Finish
            CheckBox1.Visible = True
        Else
            Button1.Text = My.Lang.Next
            CheckBox1.Visible = False
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If TablessControl1.SelectedIndex - 1 >= 0 Then TablessControl1.SelectedIndex -= 1
        Button1.Text = My.Lang.Next
        CheckBox1.Visible = False
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Process.Start(My.Resources.Link_StoreOnlineSourceCreation)
    End Sub

    Private Sub Store_Intro_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        My.Settings.Store.ShowTips = CheckBox1.Checked
        My.Settings.Store.Save()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Process.Start(My.Resources.Link_StoreUpload)
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Process.Start(My.Resources.Link_StoreSourcesExtension)
    End Sub

    Private Sub Form_HelpButtonClicked(sender As Object, e As CancelEventArgs) Handles Me.HelpButtonClicked
        Process.Start(My.Resources.Link_Wiki & "/WinPaletter-Store-basics")
    End Sub
End Class