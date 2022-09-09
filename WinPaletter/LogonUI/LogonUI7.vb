﻿Imports System.IO
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports WinPaletter.XenonCore
Public Class LogonUI7
    Private _Shown As Boolean = False
    Dim imageres As String = Environment.GetFolderPath(Environment.SpecialFolder.Windows) & "\system32\imageres.dll"
    Dim b As Bitmap
    Public ID As Integer

    Private Sub LogonUI7_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ID = 0
        ApplyDarkMode(Me)
        _Shown = False
        LoadFromCP(MainFrm.CP)
        ApplyPreview()

        If MainFrm.PreviewConfig = MainFrm.WinVer.Eight Then
            Label16.Text = "Lock Screen Enabled?"
            XenonButton3.Visible = True
            PictureBox11.Image = My.Resources.LogonUI8
            PictureBox4.Image = My.Resources.Native8
        Else
            Label16.Text = "Enabled?"
            XenonButton3.Visible = False
            PictureBox11.Image = My.Resources.LogonUI7
            PictureBox4.Image = My.Resources.Native7
        End If
    End Sub

    Private Sub LogonUI7_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        _Shown = True
    End Sub

    Sub LoadFromCP(CP As CP)

        If MainFrm.PreviewConfig = MainFrm.WinVer.Eight Then
            XenonToggle1.Checked = Not CP.Metro_NoLockScreen

            Select Case CP.Metro_LockScreenType
                Case CP.LogonUI8_Modes.System
                    XenonRadioButton1.Checked = True

                Case CP.LogonUI8_Modes.Wallpaper
                    XenonRadioButton2.Checked = True

                Case CP.LogonUI8_Modes.CustomImage
                    XenonRadioButton4.Checked = True

                Case CP.LogonUI8_Modes.SolidColor
                    XenonRadioButton3.Checked = True
            End Select

            ID = CP.Metro_LockScreenSystemID
        Else
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
        End If

        XenonTextBox1.Text = CP.LogonUI7_ImagePath
        color_pick.BackColor = CP.LogonUI7_Color
        pnl_preview.BackColor = CP.LogonUI7_Color
        XenonCheckBox8.Checked = CP.LogonUI7_Effect_Grayscale
        XenonCheckBox7.Checked = CP.LogonUI7_Effect_Blur
        XenonCheckBox6.Checked = CP.LogonUI7_Effect_Noise

        XenonTrackbar1.Value = CP.LogonUI7_Effect_Blur_Intensity
        XenonTrackbar2.Value = CP.LogonUI7_Effect_Noise_Intensity

        Select Case CP.LogonUI7_Effect_Noise_Mode
            Case CP.LogonUI7_NoiseMode.Acrylic
                XenonComboBox1.SelectedIndex = 0

            Case CP.LogonUI7_NoiseMode.Aero
                XenonComboBox1.SelectedIndex = 1
        End Select

    End Sub

    Sub LoadToCP(CP As CP)

        If MainFrm.PreviewConfig = MainFrm.WinVer.Eight Then
            CP.Metro_NoLockScreen = Not XenonToggle1.Checked

            If XenonRadioButton1.Checked Then CP.Metro_LockScreenType = CP.LogonUI8_Modes.System
            If XenonRadioButton2.Checked Then CP.Metro_LockScreenType = CP.LogonUI8_Modes.Wallpaper
            If XenonRadioButton3.Checked Then CP.Metro_LockScreenType = CP.LogonUI8_Modes.SolidColor
            If XenonRadioButton4.Checked Then CP.Metro_LockScreenType = CP.LogonUI8_Modes.CustomImage

            CP.Metro_LockScreenSystemID = ID
        Else
            CP.LogonUI7_Enabled = XenonToggle1.Checked

            If XenonRadioButton1.Checked Then CP.LogonUI7_Mode = CP.LogonUI7_Modes.Default_
            If XenonRadioButton2.Checked Then CP.LogonUI7_Mode = CP.LogonUI7_Modes.Wallpaper
            If XenonRadioButton3.Checked Then CP.LogonUI7_Mode = CP.LogonUI7_Modes.SolidColor
            If XenonRadioButton4.Checked Then CP.LogonUI7_Mode = CP.LogonUI7_Modes.CustomImage
        End If


        CP.LogonUI7_ImagePath = XenonTextBox1.Text
        CP.LogonUI7_Color = color_pick.BackColor

        CP.LogonUI7_Effect_Grayscale = XenonCheckBox8.Checked
        CP.LogonUI7_Effect_Blur = XenonCheckBox7.Checked
        CP.LogonUI7_Effect_Noise = XenonCheckBox6.Checked

        CP.LogonUI7_Effect_Blur_Intensity = XenonTrackbar1.Value
        CP.LogonUI7_Effect_Noise_Intensity = XenonTrackbar2.Value

        If XenonComboBox1.SelectedIndex = 0 Then CP.LogonUI7_Effect_Noise_Mode = CP.LogonUI7_NoiseMode.Acrylic
        If XenonComboBox1.SelectedIndex = 1 Then CP.LogonUI7_Effect_Noise_Mode = CP.LogonUI7_NoiseMode.Aero
    End Sub

    Function ReturnBK() As Bitmap
        Dim bmpX As Bitmap

        If XenonRadioButton1.Checked Then
            If My.W7 Then
                bmpX = LoadFromDLL(imageres, 5038)
            End If

            If My.W8 Then
                Dim syslock As String
                If Not ID = 1 And Not ID = 3 Then
                    syslock = String.Format(Environment.GetFolderPath(Environment.SpecialFolder.Windows) & "\Web\Screen\img10{0}.jpg", ID)
                Else
                    syslock = String.Format(Environment.GetFolderPath(Environment.SpecialFolder.Windows) & "\Web\Screen\img10{0}.png", ID)
                End If
                bmpX = Image.FromStream(New FileStream(syslock, IO.FileMode.Open, IO.FileAccess.Read))
            End If

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

        If XenonCheckBox7.Checked Then _bmp = BlurBitmap(_bmp, XenonTrackbar1.Value)

        If XenonCheckBox6.Checked Then
            Select Case XenonComboBox1.SelectedIndex
                Case 0
                    _bmp = NoiseBitmap(_bmp, CP.LogonUI7_NoiseMode.Acrylic, XenonTrackbar2.Value / 100)
                Case 1
                    _bmp = NoiseBitmap(_bmp, CP.LogonUI7_NoiseMode.Aero, XenonTrackbar2.Value / 100)
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

    Private Sub XenonTrackbar1_Scroll(sender As Object) Handles XenonTrackbar1.Scroll
        If _Shown And XenonCheckBox7.Checked Then pnl_preview.BackgroundImage = ReturnBK()
    End Sub

    Private Sub XenonNumericUpDown2_Click(sender As Object) Handles XenonTrackbar2.Scroll
        If _Shown And XenonCheckBox6.Checked Then pnl_preview.BackgroundImage = ReturnBK()
    End Sub

    Private Sub XenonComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles XenonComboBox1.SelectedIndexChanged
        If _Shown And XenonCheckBox6.Checked Then pnl_preview.BackgroundImage = ReturnBK()
    End Sub

    Private Sub XenonButton1_Click(sender As Object, e As EventArgs) Handles XenonButton1.Click
        LoadToCP(MainFrm.CP)
        Me.Close()
    End Sub

    Private Sub XenonButton7_Click(sender As Object, e As EventArgs) Handles XenonButton7.Click
        If OpenFileDialog1.ShowDialog = DialogResult.OK Then
            XenonTextBox1.Text = OpenFileDialog1.FileName
        End If
    End Sub

    Private Sub color_pick_Click(sender As Object, e As EventArgs) Handles color_pick.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                pnl_preview.BackgroundImage = ReturnBK()
            End If
            Exit Sub
        End If

        Dim CList As New List(Of Control) From {
          sender, pnl_preview
      }

        If XenonRadioButton3.Checked Then pnl_preview.BackgroundImage = Nothing

        Dim C As Color = ColorPickerDlg.Pick(CList)

        sender.BackColor = Color.FromArgb(255, C)

        pnl_preview.BackgroundImage = ReturnBK()

        CList.Clear()
    End Sub

    Private Sub XenonButton2_Click(sender As Object, e As EventArgs) Handles XenonButton2.Click
        Me.Close()
    End Sub

    Private Sub XenonButton3_Click(sender As Object, e As EventArgs) Handles XenonButton3.Click
        If LogonUI8_Pics.ShowDialog = DialogResult.OK Then
            ApplyPreview()
        End If
    End Sub
End Class