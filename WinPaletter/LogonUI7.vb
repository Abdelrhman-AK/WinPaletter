﻿Imports System.IO
Imports WinPaletter.XenonCore
Public Class LogonUI7
    Private _Shown As Boolean = False
    Dim imageres As String = Environment.GetFolderPath(Environment.SpecialFolder.Windows) & "\system32\imageres.dll"
    Dim b As Bitmap

    Private Sub LogonUI7_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ApplyDarkMode(Me)
        _Shown = False
        LoadFromCP(MainFrm.CP)
        ApplyPreview()
    End Sub

    Private Sub LogonUI7_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        _Shown = True
    End Sub

    Sub LoadFromCP(CP As CP)
        XenonToggle1.Checked = CP.LogonUI7_Enabled

        Select Case CP.LogonUI7_Mode
            Case CP.LogonUI7_Modes.Default_
                XenonRadioButton1.Checked = True

            Case CP.LogonUI7_Modes.Wallpaper
                XenonRadioButton2.Checked = True

            Case CP.LogonUI7_Modes.CustomImage
                XenonRadioButton4.Checked = True

            Case CP.LogonUI7_Modes.SolidColor
                XenonRadioButton3.Checked = True

        End Select

        XenonTextBox1.Text = CP.LogonUI7_ImagePath
        color_pick.BackColor = CP.LogonUI7_Color

        XenonCheckBox8.Checked = CP.LogonUI7_Effect_Grayscale
        XenonCheckBox7.Checked = CP.LogonUI7_Effect_Blur
        XenonCheckBox6.Checked = CP.LogonUI7_Effect_Noise

        XenonNumericUpDown1.Value = CP.LogonUI7_Effect_Blur_Intensity
        XenonNumericUpDown2.Value = CP.LogonUI7_Effect_Noise_Intensity

        Select Case CP.LogonUI7_Effect_Noise_Mode
            Case CP.LogonUI7_NoiseMode.Acrylic
                XenonComboBox1.SelectedIndex = 0

            Case CP.LogonUI7_NoiseMode.Aero
                XenonComboBox1.SelectedIndex = 1

        End Select

    End Sub

    Function ReturnBK() As Bitmap
        Dim bmpX As Bitmap

        If XenonRadioButton1.Checked Then
            bmpX = LoadFromDLL(imageres, 5038)
        ElseIf XenonRadioButton2.Checked Then
            bmpX = My.Application.GetCurrentWallpaper
        ElseIf XenonRadioButton3.Checked Then
            bmpX = ColorToBitmap(color_pick.BackColor, My.Computer.Screen.Bounds.Size)
        ElseIf XenonRadioButton4.Checked And IO.File.Exists(XenonTextBox1.Text) Then
            bmpX = Image.FromStream(New FileStream(XenonTextBox1.Text, IO.FileMode.Open, IO.FileAccess.Read))
        Else
            bmpX = ColorToBitmap(Color.Black, My.Computer.Screen.Bounds.Size)
        End If

        Return ApplyEffects(ResizeImage(bmpX, pnl_preview.Width, pnl_preview.Height))
    End Function

    Sub ApplyPreview()
        Cursor = Cursors.AppStarting

        pnl_preview.BackgroundImage = ReturnBK()

        Cursor = Cursors.Default
    End Sub

    Function ApplyEffects(bmp As Bitmap)
        Dim _bmp As Bitmap
        _bmp = bmp

        If XenonCheckBox8.Checked Then _bmp = Grayscale(_bmp)

        If XenonCheckBox7.Checked Then _bmp = BlurBitmap(_bmp, XenonNumericUpDown1.Value)

        If XenonCheckBox6.Checked Then
            Select Case XenonComboBox1.SelectedIndex
                Case 0
                    _bmp = NoiseBitmap(_bmp, CP.LogonUI7_NoiseMode.Acrylic, XenonNumericUpDown2.Value / 100)
                Case 1
                    _bmp = NoiseBitmap(_bmp, CP.LogonUI7_NoiseMode.Aero, XenonNumericUpDown2.Value / 100)
            End Select
        End If

        Return _bmp
    End Function

    Private Sub XenonRadioButton1_CheckedChanged(sender As Object) Handles XenonRadioButton1.CheckedChanged
        If _Shown And XenonRadioButton1.Checked Then pnl_preview.BackgroundImage = ReturnBK()
    End Sub

    Private Sub XenonRadioButton2_CheckedChanged(sender As Object) Handles XenonRadioButton2.CheckedChanged
        If _Shown And XenonRadioButton2.Checked Then pnl_preview.BackgroundImage = ReturnBK()
    End Sub

    Private Sub XenonRadioButton4_CheckedChanged(sender As Object) Handles XenonRadioButton4.CheckedChanged
        If _Shown And XenonRadioButton4.Checked Then pnl_preview.BackgroundImage = ReturnBK()
    End Sub

    Private Sub XenonTextBox1_TextChanged(sender As Object, e As EventArgs) Handles XenonTextBox1.TextChanged
        If _Shown And XenonRadioButton4.Checked And IO.File.Exists(XenonTextBox1.Text) Then pnl_preview.BackgroundImage = ReturnBK()
    End Sub

    Private Sub XenonRadioButton3_CheckedChanged(sender As Object) Handles XenonRadioButton3.CheckedChanged
        If _Shown And XenonRadioButton3.Checked Then pnl_preview.BackgroundImage = ReturnBK()
    End Sub

    Private Sub XenonCheckBox8_CheckedChanged(sender As Object) Handles XenonCheckBox8.CheckedChanged
        If _Shown Then pnl_preview.BackgroundImage = ReturnBK()
    End Sub

    Private Sub XenonCheckBox7_CheckedChanged(sender As Object) Handles XenonCheckBox7.CheckedChanged
        If _Shown Then pnl_preview.BackgroundImage = ReturnBK()
    End Sub

    Private Sub XenonCheckBox6_CheckedChanged(sender As Object) Handles XenonCheckBox6.CheckedChanged
        If _Shown Then pnl_preview.BackgroundImage = ReturnBK()
    End Sub

    Private Sub XenonNumericUpDown1_Click(sender As Object, e As EventArgs) Handles XenonNumericUpDown1.Click
        If _Shown And XenonCheckBox7.Checked Then pnl_preview.BackgroundImage = ReturnBK()
    End Sub

    Private Sub XenonNumericUpDown2_Click(sender As Object, e As EventArgs) Handles XenonNumericUpDown2.Click
        If _Shown And XenonCheckBox6.Checked Then pnl_preview.BackgroundImage = ReturnBK()
    End Sub

    Private Sub XenonComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles XenonComboBox1.SelectedIndexChanged
        If _Shown And XenonCheckBox6.Checked Then pnl_preview.BackgroundImage = ReturnBK()
    End Sub
End Class