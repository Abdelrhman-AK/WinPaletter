﻿Imports System.ComponentModel
Imports WinPaletter.XenonCore

Public Class Converter_Form

    Private ReadOnly _Convert As New Converter

    Private Sub Converter_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadLanguage
        ApplyDarkMode(Me)
        XenonCheckBox1.Checked = My.Settings.FileTypeManagement.CompressThemeFile
        Label3.Font = My.Application.ConsoleFontMedium

        If IO.File.Exists(MainFrm.OpenFileDialog1.FileName) AndAlso Not IO.File.Exists(XenonTextBox1.Text) Then
            XenonTextBox1.Text = MainFrm.OpenFileDialog1.FileName
        End If

    End Sub

    Private Sub XenonButton2_Click(sender As Object, e As EventArgs) Handles XenonButton2.Click
        Close()
    End Sub

    Private Sub XenonTextBox1_TextChanged(sender As Object, e As EventArgs) Handles XenonTextBox1.TextChanged
        Select Case _Convert.FetchFile(XenonTextBox1.Text)
            Case Converter_CP.WP_Format.JSON
                Label3.Text = My.Lang.Convert_JSON_To_Old
                XenonCheckBox2.Enabled = True
                XenonCheckBox1.Enabled = False

            Case Converter_CP.WP_Format.WPTH
                Label3.Text = My.Lang.Convert_Old_To_JSON
                XenonCheckBox2.Enabled = False
                XenonCheckBox1.Enabled = True

            Case Converter_CP.WP_Format.Error
                Label3.Text = My.Lang.Convert_Error_Phrasing
                XenonCheckBox2.Enabled = False
                XenonCheckBox1.Enabled = False
        End Select
    End Sub

    Private Sub XenonButton3_Click(sender As Object, e As EventArgs) Handles XenonButton3.Click
        If Not _Convert.FetchFile(XenonTextBox1.Text) = Converter_CP.WP_Format.Error Then
            If SaveFileDialog1.ShowDialog = DialogResult.OK Then
                _Convert.Convert(XenonTextBox1.Text, SaveFileDialog1.FileName, XenonCheckBox1.Checked, XenonCheckBox2.Checked)
                MsgBox(My.Lang.Convert_Done, MsgBoxStyle.Information)
            End If
        End If
    End Sub

    Private Sub XenonButton1_Click(sender As Object, e As EventArgs) Handles XenonButton1.Click
        If OpenFileDialog1.ShowDialog = DialogResult.OK Then
            XenonTextBox1.Text = OpenFileDialog1.FileName
        End If
    End Sub

    Private Sub Form_HelpButtonClicked(sender As Object, e As CancelEventArgs) Handles Me.HelpButtonClicked
        Process.Start(My.Resources.Link_Wiki & "/Convert-WinPaletter-themes-between-old-and-new-formats")
    End Sub
End Class