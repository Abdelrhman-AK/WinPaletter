﻿Imports Microsoft.Win32

Public Class NewExtTerminal
    Private Sub XenonButton16_Click(sender As Object, e As EventArgs) Handles XenonButton16.Click
        If OpenFileDialog1.ShowDialog = DialogResult.OK Then
            XenonTextBox1.Text = OpenFileDialog1.FileName
        End If
    End Sub

    Private Sub XenonButton1_Click(sender As Object, e As EventArgs) Handles XenonButton1.Click

        Try
            Registry.CurrentUser.CreateSubKey(String.Format("Console\%SystemDrive%_{0}", XenonTextBox1.Text.Replace("\", "_").Trim(":")(1)), True).Close()

            MsgBox(My.Application.LanguageHelper.ExtTer_NewSuccess, MsgBoxStyle.Information + My.Application.MsgboxRt)
            ExternalTerminal.FillTerminals(ExternalTerminal.XenonComboBox1)
        Catch ex As Exception
            MsgBox(My.Application.LanguageHelper.ExtTer_NewError & vbCrLf & vbCrLf & My.Application.LanguageHelper.ErrorDetails & ex.Message, MsgBoxStyle.Critical + My.Application.MsgboxRt)
        End Try

    End Sub

    Private Sub XenonButton2_Click(sender As Object, e As EventArgs) Handles XenonButton2.Click
        Me.Close()
    End Sub
End Class