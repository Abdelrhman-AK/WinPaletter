﻿Imports WinPaletter.XenonCore

Public Class Whatsnew
    Private Sub Tutorial_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ApplyDarkMode(Me)
    End Sub

    Private Sub XenonButton1_Click(sender As Object, e As EventArgs) Handles XenonButton1.Click
        XenonButton2.Enabled = True
        If sender.text = My.Application.LanguageHelper.OK Then
            Me.Close()
        Else
            If XenonTabControl1.SelectedIndex + 1 <= XenonTabControl1.TabPages.Count - 1 Then XenonTabControl1.SelectedIndex += 1
            If XenonTabControl1.SelectedIndex = XenonTabControl1.TabPages.Count - 1 Then
                XenonButton1.Text = My.Application.LanguageHelper.OK
            End If
        End If
    End Sub

    Private Sub XenonButton2_Click(sender As Object, e As EventArgs) Handles XenonButton2.Click
        XenonButton1.Text = My.Application.LanguageHelper.Next_
        If XenonTabControl1.SelectedIndex > 0 Then XenonTabControl1.SelectedIndex -= 1
        If XenonTabControl1.SelectedIndex = 0 Then XenonButton2.Enabled = False
    End Sub

    Private Sub XenonButton3_Click(sender As Object, e As EventArgs) Handles XenonButton3.Click
        Changelog.ShowDialog()
    End Sub

    Private Sub XenonButton8_Click(sender As Object, e As EventArgs) Handles XenonButton8.Click
        LanguageContribute.Show()
    End Sub

    Private Sub RetroButton1_Click(sender As Object, e As EventArgs) Handles RetroButton1.Click
        OTVDM.ShowDialog()
    End Sub
End Class