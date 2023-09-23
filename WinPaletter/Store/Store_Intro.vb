﻿Imports System.ComponentModel

Public Class Store_Intro
    Private Sub Store_Intro_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        XenonCheckBox1.Checked = My.Settings.Store.ShowTips
        LoadLanguage
        ApplyStyle(Me)
        Icon = Store.Icon
    End Sub

    Private Sub XenonButton1_Click(sender As Object, e As EventArgs) Handles XenonButton1.Click
        If TablessControl1.SelectedIndex + 1 <= TablessControl1.TabPages.Count - 1 Then TablessControl1.SelectedIndex += 1

        If XenonButton1.Text = My.Lang.Finish Then
            Close()
            TablessControl1.SelectedIndex = 0
            XenonButton1.Text = My.Lang.Next
        End If

        If TablessControl1.SelectedIndex = TablessControl1.TabPages.Count - 1 Then
            XenonButton1.Text = My.Lang.Finish
            XenonCheckBox1.Visible = True
        Else
            XenonButton1.Text = My.Lang.Next
            XenonCheckBox1.Visible = False
        End If
    End Sub

    Private Sub XenonButton2_Click(sender As Object, e As EventArgs) Handles XenonButton2.Click
        If TablessControl1.SelectedIndex - 1 >= 0 Then TablessControl1.SelectedIndex -= 1
        XenonButton1.Text = My.Lang.Next
        XenonCheckBox1.Visible = False
    End Sub

    Private Sub XenonButton3_Click(sender As Object, e As EventArgs) Handles XenonButton3.Click
        Process.Start(My.Resources.Link_StoreOnlineSourceCreation)
    End Sub

    Private Sub Store_Intro_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        My.Settings.Store.ShowTips = XenonCheckBox1.Checked
        My.Settings.Store.Save()
    End Sub

    Private Sub XenonButton4_Click(sender As Object, e As EventArgs) Handles XenonButton4.Click
        Process.Start(My.Resources.Link_StoreUpload)
    End Sub

    Private Sub XenonButton5_Click(sender As Object, e As EventArgs) Handles XenonButton5.Click
        Process.Start(My.Resources.Link_StoreSourcesExtension)
    End Sub

    Private Sub Form_HelpButtonClicked(sender As Object, e As CancelEventArgs) Handles Me.HelpButtonClicked
        Process.Start(My.Resources.Link_Wiki & "/WinPaletter-Store-basics")
    End Sub
End Class