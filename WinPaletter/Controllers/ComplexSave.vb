﻿Imports System.Runtime
Imports WinPaletter.XenonCore

Public Class ComplexSave
    Private Sub XenonButton1_Click(sender As Object, e As EventArgs) Handles XenonButton1.Click
        Dim i1 As Integer = 0
        Dim i2 As Integer = 0

        If XenonRadioButton1.Checked Then i1 = 0
        If XenonRadioButton2.Checked Then i1 = 1
        If XenonRadioButton3.Checked Then i1 = 2
        If XenonCheckBox1.Checked Then i2 = 1 Else i2 = 0

        My.Application._Settings.ComplexSaveResult = String.Format("{0}.{1}", i1, i2)
        My.Application._Settings.ShowSaveConfirmation = XenonCheckBox2.Checked
        My.Application._Settings.Save(XeSettings.Mode.Registry)

        Me.DialogResult = DialogResult.Yes
        Me.Close()
    End Sub

    Private Sub ComplexSave_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ApplyDarkMode(Me)

        Dim c As Color = GetAverageColor(PictureBox1.Image)
        Dim c1 As Color = CCB(c, If(GetDarkMode(), -0.35, 0.35))
        Dim c2 As Color = CCB(c, If(GetDarkMode(), -0.75, 0.75))
        Panel1.BackColor = c1
        BackColor = c2

        My.Computer.Audio.PlaySystemSound(Media.SystemSounds.Exclamation)

        Dim r As String() = My.Application._Settings.ComplexSaveResult.Split(".")
        Dim r1 As String = r(0)
        Dim r2 As String = r(1)

        If r1 = 0 Then
            XenonRadioButton1.Checked = True
        ElseIf r1 = 1 Then
            XenonRadioButton2.Checked = True
        ElseIf r1 = 2 Then
            XenonRadioButton3.Checked = True
        Else
            XenonRadioButton3.Checked = True
        End If

        XenonCheckBox1.Checked = (r2 = 1)

        XenonCheckBox2.Checked = My.Application._Settings.ShowSaveConfirmation

        Me.DialogResult = DialogResult.None
    End Sub

    Private Sub XenonButton2_Click(sender As Object, e As EventArgs) Handles XenonButton2.Click
        My.Application._Settings.ShowSaveConfirmation = XenonCheckBox2.Checked
        My.Application._Settings.Save(XeSettings.Mode.Registry)
        Me.DialogResult = DialogResult.No
        Me.Close()
    End Sub

    Private Sub XenonButton3_Click(sender As Object, e As EventArgs) Handles XenonButton3.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub
End Class