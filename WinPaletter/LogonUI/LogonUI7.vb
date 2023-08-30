Imports System.ComponentModel
Imports ImageProcessor
Imports WinPaletter.PreviewHelpers
Imports WinPaletter.XenonCore

Public Class LogonUI7
    Private _Shown As Boolean = False
    ReadOnly b As Bitmap
    Public ID As Integer

    Private Sub LogonUI7_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ID = 0
        LoadLanguage
        ApplyDarkMode(Me)
        _Shown = False
        LoadFromCP(My.CP)
        ApplyPreview()
        Icon = LogonUI.Icon

        If My.PreviewStyle = WindowStyle.W81 Then
            XenonButton3.Visible = True
            PictureBox11.Image = My.Resources.LogonUI8
            PictureBox4.Image = My.Resources.Native8
        ElseIf My.PreviewStyle = WindowStyle.W7 Then
            XenonButton3.Visible = False
            PictureBox11.Image = My.Resources.LogonUI7
            PictureBox4.Image = My.Resources.Native7
        End If

        XenonButton12.Image = MainFrm.XenonButton20.Image.Resize(16, 16)
    End Sub

    Private Sub LogonUI7_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        _Shown = True
    End Sub

    Sub LoadFromCP(CP As CP)

        If My.PreviewStyle = WindowStyle.W81 Then
            XenonToggle1.Checked = Not CP.Windows81.NoLockScreen

            Select Case CP.Windows81.LockScreenType
                Case CP.Structures.LogonUI7.Modes.Default_
                    XenonRadioButton1.Checked = True

                Case CP.Structures.LogonUI7.Modes.Wallpaper
                    XenonRadioButton2.Checked = True

                Case CP.Structures.LogonUI7.Modes.CustomImage
                    XenonRadioButton4.Checked = True

                Case CP.Structures.LogonUI7.Modes.SolidColor
                    XenonRadioButton3.Checked = True
            End Select

            ID = CP.Windows81.LockScreenSystemID

            XenonTextBox1.Text = CP.LogonUI7.ImagePath
            color_pick.BackColor = CP.LogonUI7.Color
            pnl_preview.BackColor = CP.LogonUI7.Color
            XenonCheckBox8.Checked = CP.LogonUI7.Grayscale
            XenonCheckBox7.Checked = CP.LogonUI7.Blur
            XenonCheckBox6.Checked = CP.LogonUI7.Noise

            XenonTrackbar1.Value = CP.LogonUI7.Blur_Intensity
            XenonTrackbar2.Value = CP.LogonUI7.Noise_Intensity

            Select Case CP.LogonUI7.Noise_Mode
                Case BitmapExtensions.NoiseMode.Acrylic
                    XenonComboBox1.SelectedIndex = 0

                Case BitmapExtensions.NoiseMode.Aero
                    XenonComboBox1.SelectedIndex = 1
            End Select

        ElseIf My.PreviewStyle = WindowStyle.W7 Then

            XenonToggle1.Checked = CP.LogonUI7.Enabled

            Select Case CP.LogonUI7.Mode
                Case CP.Structures.LogonUI7.Modes.Default_
                    XenonRadioButton1.Checked = True

                Case CP.Structures.LogonUI7.Modes.Wallpaper
                    XenonRadioButton2.Checked = True

                Case CP.Structures.LogonUI7.Modes.CustomImage
                    XenonRadioButton4.Checked = True

                Case CP.Structures.LogonUI7.Modes.SolidColor
                    XenonRadioButton3.Checked = True
            End Select

            XenonTextBox1.Text = CP.LogonUI7.ImagePath
            color_pick.BackColor = CP.LogonUI7.Color
            pnl_preview.BackColor = CP.LogonUI7.Color
            XenonCheckBox8.Checked = CP.LogonUI7.Grayscale
            XenonCheckBox7.Checked = CP.LogonUI7.Blur
            XenonCheckBox6.Checked = CP.LogonUI7.Noise

            XenonTrackbar1.Value = CP.LogonUI7.Blur_Intensity
            XenonTrackbar2.Value = CP.LogonUI7.Noise_Intensity

            Select Case CP.LogonUI7.Noise_Mode
                Case BitmapExtensions.NoiseMode.Acrylic
                    XenonComboBox1.SelectedIndex = 0

                Case BitmapExtensions.NoiseMode.Aero
                    XenonComboBox1.SelectedIndex = 1
            End Select
        End If



    End Sub

    Sub LoadToCP(CP As CP)

        If My.PreviewStyle = WindowStyle.W81 Then
            CP.Windows81.NoLockScreen = Not XenonToggle1.Checked

            If XenonRadioButton1.Checked Then CP.Windows81.LockScreenType = CP.Structures.LogonUI7.Modes.Default_
            If XenonRadioButton2.Checked Then CP.Windows81.LockScreenType = CP.Structures.LogonUI7.Modes.Wallpaper
            If XenonRadioButton3.Checked Then CP.Windows81.LockScreenType = CP.Structures.LogonUI7.Modes.SolidColor
            If XenonRadioButton4.Checked Then CP.Windows81.LockScreenType = CP.Structures.LogonUI7.Modes.CustomImage

            CP.Windows81.LockScreenSystemID = ID

            CP.LogonUI7.ImagePath = XenonTextBox1.Text
            CP.LogonUI7.Color = color_pick.BackColor

            CP.LogonUI7.Grayscale = XenonCheckBox8.Checked
            CP.LogonUI7.Blur = XenonCheckBox7.Checked
            CP.LogonUI7.Noise = XenonCheckBox6.Checked

            CP.LogonUI7.Blur_Intensity = XenonTrackbar1.Value
            CP.LogonUI7.Noise_Intensity = XenonTrackbar2.Value

            If XenonComboBox1.SelectedIndex = 0 Then CP.LogonUI7.Noise_Mode = BitmapExtensions.NoiseMode.Acrylic
            If XenonComboBox1.SelectedIndex = 1 Then CP.LogonUI7.Noise_Mode = BitmapExtensions.NoiseMode.Aero

        ElseIf My.PreviewStyle = WindowStyle.W7 Then
            CP.LogonUI7.Enabled = XenonToggle1.Checked

            If XenonRadioButton1.Checked Then CP.LogonUI7.Mode = CP.Structures.LogonUI7.Modes.Default_
            If XenonRadioButton2.Checked Then CP.LogonUI7.Mode = CP.Structures.LogonUI7.Modes.Wallpaper
            If XenonRadioButton3.Checked Then CP.LogonUI7.Mode = CP.Structures.LogonUI7.Modes.SolidColor
            If XenonRadioButton4.Checked Then CP.LogonUI7.Mode = CP.Structures.LogonUI7.Modes.CustomImage

            CP.LogonUI7.ImagePath = XenonTextBox1.Text
            CP.LogonUI7.Color = color_pick.BackColor

            CP.LogonUI7.Grayscale = XenonCheckBox8.Checked
            CP.LogonUI7.Blur = XenonCheckBox7.Checked
            CP.LogonUI7.Noise = XenonCheckBox6.Checked

            CP.LogonUI7.Blur_Intensity = XenonTrackbar1.Value
            CP.LogonUI7.Noise_Intensity = XenonTrackbar2.Value

            If XenonComboBox1.SelectedIndex = 0 Then CP.LogonUI7.Noise_Mode = BitmapExtensions.NoiseMode.Acrylic
            If XenonComboBox1.SelectedIndex = 1 Then CP.LogonUI7.Noise_Mode = BitmapExtensions.NoiseMode.Aero
        End If

    End Sub

    Function ReturnBK() As Bitmap
        Dim bmpX As Bitmap = Nothing

        If XenonRadioButton1.Checked Then
            If My.W7 Or My.WVista Then
                bmpX = Resources_Functions.GetImageFromDLL(My.PATH_imageres, 5038)

            ElseIf My.W8 Or My.W81 Then
                Dim SysLock As String
                If Not ID = 1 And Not ID = 3 Then
                    SysLock = String.Format(My.PATH_Windows & "\Web\Screen\img10{0}.jpg", ID)
                Else
                    SysLock = String.Format(My.PATH_Windows & "\Web\Screen\img10{0}.png", ID)
                End If

                bmpX = Bitmap_Mgr.Load(SysLock)
            End If

        ElseIf XenonRadioButton2.Checked Then
            Using b As New Bitmap(My.Application.GetWallpaper)
                bmpX = b.Clone
            End Using

        ElseIf XenonRadioButton3.Checked Then
            bmpX = color_pick.BackColor.ToBitmap(My.Computer.Screen.Bounds.Size)

        ElseIf XenonRadioButton4.Checked And IO.File.Exists(XenonTextBox1.Text) Then
            bmpX = Bitmap_Mgr.Load(XenonTextBox1.Text)

        Else
            bmpX = Color.Black.ToBitmap(My.Computer.Screen.Bounds.Size)

        End If

        If bmpX IsNot Nothing Then
            Return ApplyEffects(bmpX.Resize(pnl_preview.Size))
        Else
            Return Nothing
        End If

    End Function

    Sub ApplyPreview()
        Cursor = Cursors.AppStarting
        pnl_preview.BackgroundImage = ReturnBK()
        Cursor = Cursors.Default
    End Sub

    Function ApplyEffects(bmp As Bitmap)
        Dim _bmp As Bitmap
        _bmp = bmp

        Try
            If XenonCheckBox8.Checked Then _bmp = _bmp.Grayscale

            If XenonCheckBox7.Checked Then
                Dim imgF As New ImageFactory
                imgF.Load(_bmp.Clone)
                imgF.GaussianBlur(XenonTrackbar1.Value)
                _bmp = imgF.Image
            End If

            If XenonCheckBox6.Checked Then
                Select Case XenonComboBox1.SelectedIndex
                    Case 0
                        _bmp = _bmp.Noise(BitmapExtensions.NoiseMode.Acrylic, XenonTrackbar2.Value / 100)
                    Case 1
                        _bmp = _bmp.Noise(BitmapExtensions.NoiseMode.Aero, XenonTrackbar2.Value / 100)
                End Select
            End If

        Catch
        End Try

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
        ttl_h.Text = sender.Value.ToString()
        If _Shown And XenonCheckBox7.Checked Then pnl_preview.BackgroundImage = ReturnBK()
    End Sub

    Private Sub XenonNumericUpDown2_Click(sender As Object) Handles XenonTrackbar2.Scroll
        XenonButton4.Text = sender.Value.ToString()
        If _Shown And XenonCheckBox6.Checked Then pnl_preview.BackgroundImage = ReturnBK()
    End Sub

    Private Sub XenonComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles XenonComboBox1.SelectedIndexChanged
        If _Shown And XenonCheckBox6.Checked Then pnl_preview.BackgroundImage = ReturnBK()
    End Sub

    Private Sub XenonButton1_Click(sender As Object, e As EventArgs) Handles XenonButton1.Click
        LoadToCP(My.CP)
        Me.Close()
    End Sub

    Private Sub XenonButton7_Click(sender As Object, e As EventArgs) Handles XenonButton7.Click
        If OpenImgDlg.ShowDialog = DialogResult.OK Then
            XenonTextBox1.Text = OpenImgDlg.FileName
        End If
    End Sub

    Private Sub Color_pick_Click(sender As Object, e As EventArgs) Handles color_pick.Click
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

    Private Sub ttl_h_Click(sender As Object, e As EventArgs) Handles ttl_h.Click
        Dim response As String = InputBox(My.Lang.InputValue, sender.text, My.Lang.ItMustBeNumerical)
        sender.Text = Math.Max(Math.Min(Val(response), XenonTrackbar1.Maximum), XenonTrackbar1.Minimum) : XenonTrackbar1.Value = Val(sender.Text)
    End Sub

    Private Sub XenonButton4_Click(sender As Object, e As EventArgs) Handles XenonButton4.Click
        Dim response As String = InputBox(My.Lang.InputValue, sender.text, My.Lang.ItMustBeNumerical)
        sender.Text = Math.Max(Math.Min(Val(response), XenonTrackbar2.Maximum), XenonTrackbar2.Minimum) : XenonTrackbar2.Value = Val(sender.Text)
    End Sub

    Private Sub XenonButton11_Click(sender As Object, e As EventArgs) Handles XenonButton11.Click
        If OpenFileDialog1.ShowDialog = DialogResult.OK Then
            Dim CPx As New CP(CP.CP_Type.File, OpenFileDialog1.FileName)
            LoadFromCP(CPx)
            CPx.Dispose()
        End If
    End Sub

    Private Sub XenonButton9_Click(sender As Object, e As EventArgs) Handles XenonButton9.Click
        Dim CPx As New CP(CP.CP_Type.Registry)
        LoadFromCP(CPx)
        CPx.Dispose()
    End Sub

    Private Sub XenonButton12_Click(sender As Object, e As EventArgs) Handles XenonButton12.Click
        Dim CPx As CP
        Select Case My.PreviewStyle
            Case WindowStyle.W11
                CPx = New CP_Defaults().Default_Windows11
            Case WindowStyle.W10
                CPx = New CP_Defaults().Default_Windows10
            Case WindowStyle.W81
                CPx = New CP_Defaults().Default_Windows81
            Case WindowStyle.W7
                CPx = New CP_Defaults().Default_Windows7
            Case WindowStyle.WVista
                CPx = New CP_Defaults().Default_WindowsVista
            Case WindowStyle.WXP
                CPx = New CP_Defaults().Default_WindowsXP
            Case Else
                CPx = New CP_Defaults().Default_Windows11
        End Select
        LoadFromCP(CPx)
        CPx.Dispose()
    End Sub

    Private Sub XenonToggle1_CheckedChanged(sender As Object, e As EventArgs) Handles XenonToggle1.CheckedChanged
        checker_img.Image = If(sender.Checked, My.Resources.checker_enabled, My.Resources.checker_disabled)
    End Sub

    Private Sub Form_HelpButtonClicked(sender As Object, e As CancelEventArgs) Handles Me.HelpButtonClicked
        Process.Start(My.Resources.Link_Wiki & "/Edit-LogonUI-screen#windows-81-and-windows-7")
    End Sub
End Class