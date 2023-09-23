Imports System.ComponentModel
Imports ImageProcessor
Imports WinPaletter.PreviewHelpers

Public Class LogonUI7
    Private _Shown As Boolean = False
    ReadOnly b As Bitmap
    Public ID As Integer

    Private Sub LogonUI7_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ID = 0
        LoadLanguage
        ApplyStyle(Me)
        _Shown = False
        LoadFromCP(My.CP)
        ApplyPreview()
        Icon = LogonUI.Icon

        If My.PreviewStyle = WindowStyle.W81 Then
            Button3.Visible = True
            PictureBox11.Image = My.Resources.LogonUI8
            PictureBox4.Image = My.Resources.Native8
        ElseIf My.PreviewStyle = WindowStyle.W7 Then
            Button3.Visible = False
            PictureBox11.Image = My.Resources.LogonUI7
            PictureBox4.Image = My.Resources.Native7
        End If

        Button12.Image = MainFrm.Button20.Image.Resize(16, 16)
    End Sub

    Protected Overrides Sub OnDragOver(drgevent As DragEventArgs)
        If TypeOf drgevent.Data.GetData("WinPaletter.UI.Controllers.ColorItem") Is UI.Controllers.ColorItem Then
            Focus()
            BringToFront()
        Else
            Exit Sub
        End If

        MyBase.OnDragOver(drgevent)
    End Sub

    Private Sub LogonUI7_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        _Shown = True
    End Sub

    Sub LoadFromCP(CP As CP)

        If My.PreviewStyle = WindowStyle.W81 Then
            Toggle1.Checked = Not CP.Windows81.NoLockScreen

            Select Case CP.Windows81.LockScreenType
                Case CP.Structures.LogonUI7.Modes.Default_
                    RadioButton1.Checked = True

                Case CP.Structures.LogonUI7.Modes.Wallpaper
                    RadioButton2.Checked = True

                Case CP.Structures.LogonUI7.Modes.CustomImage
                    RadioButton4.Checked = True

                Case CP.Structures.LogonUI7.Modes.SolidColor
                    RadioButton3.Checked = True
            End Select

            ID = CP.Windows81.LockScreenSystemID

            TextBox1.Text = CP.LogonUI7.ImagePath
            color_pick.BackColor = CP.LogonUI7.Color
            pnl_preview.BackColor = CP.LogonUI7.Color
            CheckBox8.Checked = CP.LogonUI7.Grayscale
            CheckBox7.Checked = CP.LogonUI7.Blur
            CheckBox6.Checked = CP.LogonUI7.Noise

            Trackbar1.Value = CP.LogonUI7.Blur_Intensity
            Trackbar2.Value = CP.LogonUI7.Noise_Intensity

            Select Case CP.LogonUI7.Noise_Mode
                Case BitmapExtensions.NoiseMode.Acrylic
                    ComboBox1.SelectedIndex = 0

                Case BitmapExtensions.NoiseMode.Aero
                    ComboBox1.SelectedIndex = 1
            End Select

        ElseIf My.PreviewStyle = WindowStyle.W7 Then

            Toggle1.Checked = CP.LogonUI7.Enabled

            Select Case CP.LogonUI7.Mode
                Case CP.Structures.LogonUI7.Modes.Default_
                    RadioButton1.Checked = True

                Case CP.Structures.LogonUI7.Modes.Wallpaper
                    RadioButton2.Checked = True

                Case CP.Structures.LogonUI7.Modes.CustomImage
                    RadioButton4.Checked = True

                Case CP.Structures.LogonUI7.Modes.SolidColor
                    RadioButton3.Checked = True
            End Select

            TextBox1.Text = CP.LogonUI7.ImagePath
            color_pick.BackColor = CP.LogonUI7.Color
            pnl_preview.BackColor = CP.LogonUI7.Color
            CheckBox8.Checked = CP.LogonUI7.Grayscale
            CheckBox7.Checked = CP.LogonUI7.Blur
            CheckBox6.Checked = CP.LogonUI7.Noise

            Trackbar1.Value = CP.LogonUI7.Blur_Intensity
            Trackbar2.Value = CP.LogonUI7.Noise_Intensity

            Select Case CP.LogonUI7.Noise_Mode
                Case BitmapExtensions.NoiseMode.Acrylic
                    ComboBox1.SelectedIndex = 0

                Case BitmapExtensions.NoiseMode.Aero
                    ComboBox1.SelectedIndex = 1
            End Select
        End If



    End Sub

    Sub LoadToCP(CP As CP)

        If My.PreviewStyle = WindowStyle.W81 Then
            CP.Windows81.NoLockScreen = Not Toggle1.Checked

            If RadioButton1.Checked Then CP.Windows81.LockScreenType = CP.Structures.LogonUI7.Modes.Default_
            If RadioButton2.Checked Then CP.Windows81.LockScreenType = CP.Structures.LogonUI7.Modes.Wallpaper
            If RadioButton3.Checked Then CP.Windows81.LockScreenType = CP.Structures.LogonUI7.Modes.SolidColor
            If RadioButton4.Checked Then CP.Windows81.LockScreenType = CP.Structures.LogonUI7.Modes.CustomImage

            CP.Windows81.LockScreenSystemID = ID

            CP.LogonUI7.ImagePath = TextBox1.Text
            CP.LogonUI7.Color = color_pick.BackColor

            CP.LogonUI7.Grayscale = CheckBox8.Checked
            CP.LogonUI7.Blur = CheckBox7.Checked
            CP.LogonUI7.Noise = CheckBox6.Checked

            CP.LogonUI7.Blur_Intensity = Trackbar1.Value
            CP.LogonUI7.Noise_Intensity = Trackbar2.Value

            If ComboBox1.SelectedIndex = 0 Then CP.LogonUI7.Noise_Mode = BitmapExtensions.NoiseMode.Acrylic
            If ComboBox1.SelectedIndex = 1 Then CP.LogonUI7.Noise_Mode = BitmapExtensions.NoiseMode.Aero

        ElseIf My.PreviewStyle = WindowStyle.W7 Then
            CP.LogonUI7.Enabled = Toggle1.Checked

            If RadioButton1.Checked Then CP.LogonUI7.Mode = CP.Structures.LogonUI7.Modes.Default_
            If RadioButton2.Checked Then CP.LogonUI7.Mode = CP.Structures.LogonUI7.Modes.Wallpaper
            If RadioButton3.Checked Then CP.LogonUI7.Mode = CP.Structures.LogonUI7.Modes.SolidColor
            If RadioButton4.Checked Then CP.LogonUI7.Mode = CP.Structures.LogonUI7.Modes.CustomImage

            CP.LogonUI7.ImagePath = TextBox1.Text
            CP.LogonUI7.Color = color_pick.BackColor

            CP.LogonUI7.Grayscale = CheckBox8.Checked
            CP.LogonUI7.Blur = CheckBox7.Checked
            CP.LogonUI7.Noise = CheckBox6.Checked

            CP.LogonUI7.Blur_Intensity = Trackbar1.Value
            CP.LogonUI7.Noise_Intensity = Trackbar2.Value

            If ComboBox1.SelectedIndex = 0 Then CP.LogonUI7.Noise_Mode = BitmapExtensions.NoiseMode.Acrylic
            If ComboBox1.SelectedIndex = 1 Then CP.LogonUI7.Noise_Mode = BitmapExtensions.NoiseMode.Aero
        End If

    End Sub

    Function ReturnBK() As Bitmap
        Dim bmpX As Bitmap = Nothing

        If RadioButton1.Checked Then
            If My.W7 Or My.WVista Then
                bmpX = PE_Functions.GetPNGFromDLL(My.PATH_imageres, 5038)

            ElseIf My.W8 Or My.W81 Then
                Dim SysLock As String
                If Not ID = 1 And Not ID = 3 Then
                    SysLock = String.Format(My.PATH_Windows & "\Web\Screen\img10{0}.jpg", ID)
                Else
                    SysLock = String.Format(My.PATH_Windows & "\Web\Screen\img10{0}.png", ID)
                End If

                bmpX = Bitmap_Mgr.Load(SysLock)
            End If

        ElseIf RadioButton2.Checked Then
            Using b As New Bitmap(My.Application.GetWallpaper)
                bmpX = b.Clone
            End Using

        ElseIf RadioButton3.Checked Then
            bmpX = color_pick.BackColor.ToBitmap(My.Computer.Screen.Bounds.Size)

        ElseIf RadioButton4.Checked And IO.File.Exists(TextBox1.Text) Then
            bmpX = Bitmap_Mgr.Load(TextBox1.Text)

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
            If CheckBox8.Checked Then _bmp = _bmp.Grayscale

            If CheckBox7.Checked Then
                Dim imgF As New ImageFactory
                imgF.Load(_bmp.Clone)
                imgF.GaussianBlur(Trackbar1.Value)
                _bmp = imgF.Image
            End If

            If CheckBox6.Checked Then
                Select Case ComboBox1.SelectedIndex
                    Case 0
                        _bmp = _bmp.Noise(BitmapExtensions.NoiseMode.Acrylic, Trackbar2.Value / 100)
                    Case 1
                        _bmp = _bmp.Noise(BitmapExtensions.NoiseMode.Aero, Trackbar2.Value / 100)
                End Select
            End If

        Catch
        End Try

        Return _bmp
    End Function

    Private Sub RadioButton1_CheckedChanged(sender As Object) Handles RadioButton1.CheckedChanged
        If _Shown And RadioButton1.Checked Then pnl_preview.BackgroundImage = ReturnBK()
    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object) Handles RadioButton2.CheckedChanged
        If _Shown And RadioButton2.Checked Then pnl_preview.BackgroundImage = ReturnBK()
    End Sub

    Private Sub RadioButton4_CheckedChanged(sender As Object) Handles RadioButton4.CheckedChanged
        If _Shown And RadioButton4.Checked Then pnl_preview.BackgroundImage = ReturnBK()
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        If _Shown And RadioButton4.Checked And IO.File.Exists(TextBox1.Text) Then pnl_preview.BackgroundImage = ReturnBK()
    End Sub

    Private Sub RadioButton3_CheckedChanged(sender As Object) Handles RadioButton3.CheckedChanged
        If _Shown And RadioButton3.Checked Then pnl_preview.BackgroundImage = ReturnBK()
    End Sub

    Private Sub CheckBox8_CheckedChanged(sender As Object) Handles CheckBox8.CheckedChanged
        If _Shown Then pnl_preview.BackgroundImage = ReturnBK()
    End Sub

    Private Sub CheckBox7_CheckedChanged(sender As Object) Handles CheckBox7.CheckedChanged
        If _Shown Then pnl_preview.BackgroundImage = ReturnBK()
    End Sub

    Private Sub CheckBox6_CheckedChanged(sender As Object) Handles CheckBox6.CheckedChanged
        If _Shown Then pnl_preview.BackgroundImage = ReturnBK()
    End Sub

    Private Sub Trackbar1_Scroll(sender As Object) Handles Trackbar1.Scroll
        ttl_h.Text = sender.Value.ToString()
        If _Shown And CheckBox7.Checked Then pnl_preview.BackgroundImage = ReturnBK()
    End Sub

    Private Sub NumericUpDown2_Click(sender As Object) Handles Trackbar2.Scroll
        Button4.Text = sender.Value.ToString()
        If _Shown And CheckBox6.Checked Then pnl_preview.BackgroundImage = ReturnBK()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        If _Shown And CheckBox6.Checked Then pnl_preview.BackgroundImage = ReturnBK()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        LoadToCP(My.CP)
        Me.Close()
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        If OpenImgDlg.ShowDialog = DialogResult.OK Then
            TextBox1.Text = OpenImgDlg.FileName
        End If
    End Sub

    Private Sub Color_pick_Click(sender As Object, e As EventArgs) Handles color_pick.Click, color_pick.DragDrop

        If TypeOf e Is DragEventArgs Then
            pnl_preview.BackgroundImage = ReturnBK()
            Exit Sub
        End If

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

        If RadioButton3.Checked Then pnl_preview.BackgroundImage = Nothing

        Dim C As Color = ColorPickerDlg.Pick(CList)

        sender.BackColor = Color.FromArgb(255, C)

        pnl_preview.BackgroundImage = ReturnBK()

        CList.Clear()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If LogonUI8_Pics.ShowDialog = DialogResult.OK Then
            ApplyPreview()
        End If
    End Sub

    Private Sub ttl_h_Click(sender As Object, e As EventArgs) Handles ttl_h.Click
        Dim response As String = InputBox(My.Lang.InputValue, sender.text, My.Lang.ItMustBeNumerical)
        sender.Text = Math.Max(Math.Min(Val(response), Trackbar1.Maximum), Trackbar1.Minimum) : Trackbar1.Value = Val(sender.Text)
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim response As String = InputBox(My.Lang.InputValue, sender.text, My.Lang.ItMustBeNumerical)
        sender.Text = Math.Max(Math.Min(Val(response), Trackbar2.Maximum), Trackbar2.Minimum) : Trackbar2.Value = Val(sender.Text)
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        If OpenFileDialog1.ShowDialog = DialogResult.OK Then
            Dim CPx As New CP(CP.CP_Type.File, OpenFileDialog1.FileName)
            LoadFromCP(CPx)
            CPx.Dispose()
        End If
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Dim CPx As New CP(CP.CP_Type.Registry)
        LoadFromCP(CPx)
        CPx.Dispose()
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
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

    Private Sub Toggle1_CheckedChanged(sender As Object, e As EventArgs) Handles Toggle1.CheckedChanged
        checker_img.Image = If(sender.Checked, My.Resources.checker_enabled, My.Resources.checker_disabled)
    End Sub

    Private Sub Form_HelpButtonClicked(sender As Object, e As CancelEventArgs) Handles Me.HelpButtonClicked
        Process.Start(My.Resources.Link_Wiki & "/Edit-LogonUI-screen#windows-81-and-windows-7")
    End Sub
End Class