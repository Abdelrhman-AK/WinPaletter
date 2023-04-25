Imports Microsoft.Win32
Imports WinPaletter.XenonCore
Public Class Wallpaper_Editor
    Private Sub Wallpaper_Editor_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ApplyDarkMode(Me)
        XenonButton12.Image = MainFrm.XenonButton20.Image.Resize(16, 16)
        ApplyFromCP(MainFrm.CP)
    End Sub

    Sub ApplyFromCP(CP As CP)
        With CP.Wallpaper
            WallpaperEnabled.Checked = .Enabled
            XenonRadioButton1.Checked = .SlideShow_Folder_or_ImagesList
            XenonRadioButton2.Checked = Not .SlideShow_Folder_or_ImagesList

            Select Case .WallpaperType
                Case CP.WallpaperType.Picture
                    source_pic.Checked = True
                Case CP.WallpaperType.SolidColor
                    source_color.Checked = True
                Case CP.WallpaperType.SlideShow
                    source_slideshow.Checked = True
                Case CP.WallpaperType.WindowsSpotlight
                    source_spotlight.Checked = True
                Case Else
                    source_pic.Checked = True
            End Select

            XenonTextBox1.Text = .ImageFile
            Select Case .WallpaperStyle
                Case CP.WallpaperStyle.Tile
                    style_tile.Checked = True
                Case CP.WallpaperStyle.Centered
                    style_center.Checked = True
                Case CP.WallpaperStyle.Stretched
                    style_stretch.Checked = True
                Case CP.WallpaperStyle.Fill
                    style_fill.Checked = True
                Case CP.WallpaperStyle.Fit
                    style_fit.Checked = True
                Case Else
                    style_fill.Checked = True
            End Select


            XenonTextBox2.Text = .Wallpaper_Slideshow_ImagesRootPath
            ListBox1.Items.Clear()
            ListBox1.Items.AddRange(.Wallpaper_Slideshow_Images)
            XenonTrackbar1.Value = .Wallpaper_Slideshow_Interval
            XenonCheckBox3.Checked = .Wallpaper_Slideshow_Shuffle

        End With
    End Sub

    Sub ApplyToCP(CP As CP)
        With CP.Wallpaper
            .Enabled = WallpaperEnabled.Checked
            .SlideShow_Folder_or_ImagesList = XenonRadioButton1.Checked

            If source_pic.Checked Then
                .WallpaperType = CP.WallpaperType.Picture
            ElseIf source_color.Checked Then
                .WallpaperType = CP.WallpaperType.SolidColor
            ElseIf source_slideshow.Checked Then
                .WallpaperType = CP.WallpaperType.SlideShow
            ElseIf source_spotlight.Checked Then
                .WallpaperType = CP.WallpaperType.WindowsSpotlight
            Else
                .WallpaperType = CP.WallpaperType.Picture
            End If

            .ImageFile = XenonTextBox1.Text

            If style_tile.Checked Then
                .WallpaperStyle = CP.WallpaperStyle.Tile
            ElseIf style_center.Checked Then
                .WallpaperStyle = CP.WallpaperStyle.Centered
            ElseIf style_stretch.Checked Then
                .WallpaperStyle = CP.WallpaperStyle.Stretched
            ElseIf style_fill.Checked Then
                .WallpaperStyle = CP.WallpaperStyle.Fill
            ElseIf style_fit.Checked Then
                .WallpaperStyle = CP.WallpaperStyle.Fit
            Else
                .WallpaperStyle = CP.WallpaperStyle.Fill
            End If

            .Wallpaper_Slideshow_ImagesRootPath = XenonTextBox2.Text
            .Wallpaper_Slideshow_Images = New String() {}
            .Wallpaper_Slideshow_Images = ListBox1.Items.OfType(Of String)().Where(Function(s) Not String.IsNullOrEmpty(s)).ToArray()

            .Wallpaper_Slideshow_Interval = XenonTrackbar1.Value
            .Wallpaper_Slideshow_Shuffle = XenonCheckBox3.Checked
        End With
    End Sub


    Private Sub XenonButton11_Click(sender As Object, e As EventArgs) Handles XenonButton11.Click
        If OpenFileDialog1.ShowDialog = DialogResult.OK Then
            Dim CPx As New CP(CP.CP_Type.File, OpenFileDialog1.FileName)
            ApplyFromCP(CPx)
            CPx.Dispose()
        End If
    End Sub

    Private Sub XenonButton9_Click(sender As Object, e As EventArgs) Handles XenonButton9.Click
        Dim CPx As New CP(CP.CP_Type.Registry)
        ApplyFromCP(CPx)
        CPx.Dispose()
    End Sub

    Private Sub XenonButton12_Click(sender As Object, e As EventArgs) Handles XenonButton12.Click
        Dim _Def As CP
        If MainFrm.PreviewConfig = MainFrm.WinVer.W11 Then
            _Def = New CP_Defaults().Default_Windows11
        ElseIf MainFrm.PreviewConfig = MainFrm.WinVer.W10 Then
            _Def = New CP_Defaults().Default_Windows10
        ElseIf MainFrm.PreviewConfig = MainFrm.WinVer.W8 Then
            _Def = New CP_Defaults().Default_Windows8
        ElseIf MainFrm.PreviewConfig = MainFrm.WinVer.W7 Then
            _Def = New CP_Defaults().Default_Windows7
        ElseIf MainFrm.PreviewConfig = MainFrm.WinVer.WVista Then
            _Def = New CP_Defaults().Default_WindowsVista
        ElseIf MainFrm.PreviewConfig = MainFrm.WinVer.WXP Then
            _Def = New CP_Defaults().Default_WindowsXP
        Else
            _Def = New CP_Defaults().Default_Windows11
        End If

        ApplyFromCP(_Def)
        _Def.Dispose()
    End Sub

    Private Sub XenonButton8_Click(sender As Object, e As EventArgs) Handles XenonButton8.Click
        ApplyToCP(MainFrm.CP)
        Close()
    End Sub

    Private Sub XenonButton10_Click(sender As Object, e As EventArgs) Handles XenonButton10.Click
        Cursor = Cursors.WaitCursor
        Dim CPx As New CP(CP.CP_Type.Registry)
        ApplyToCP(CPx)
        ApplyToCP(MainFrm.CP)
        CPx.Wallpaper.Apply()
        CPx.Dispose()
        Cursor = Cursors.Default
    End Sub

    Private Sub XenonButton7_Click(sender As Object, e As EventArgs) Handles XenonButton7.Click
        Close()
    End Sub

    Private Sub WallpaperEnabled_CheckedChanged(sender As Object, e As EventArgs) Handles WallpaperEnabled.CheckedChanged
        checker_img.Image = If(sender.Checked, My.Resources.checker_enabled, My.Resources.checker_disabled)
    End Sub

    Private Sub XenonButton1_Click(sender As Object, e As EventArgs) Handles XenonButton1.Click
        If OpenImgDlg.ShowDialog = DialogResult.OK Then
            XenonTextBox1.Text = OpenImgDlg.FileName
        End If
    End Sub

    Private Sub XenonButton3_Click(sender As Object, e As EventArgs) Handles XenonButton3.Click
        If MainFrm.PreviewConfig = MainFrm.WinVer.WXP Then
            XenonTextBox1.Text = My.PATH_Windows & "\Web\Wallpaper\Bliss.bmp"
        Else
            XenonTextBox1.Text = My.PATH_Windows & "\Web\Wallpaper\Windows\img0.jpg"
        End If
    End Sub

    Private Sub XenonButton2_Click(sender As Object, e As EventArgs) Handles XenonButton2.Click
        Dim R1 As RegistryKey = Registry.CurrentUser.OpenSubKey("Control Panel\Desktop", True)
        Dim WallpaperPath As String = R1.GetValue("Wallpaper").ToString()
        If R1 IsNot Nothing Then R1.Close()

        XenonTextBox1.Text = WallpaperPath
    End Sub

    Private Sub XenonButton4_Click(sender As Object, e As EventArgs) Handles XenonButton4.Click
        If Not My.WXP Then
            Dim dlg As New Ookii.Dialogs.WinForms.VistaFolderBrowserDialog
            If dlg.ShowDialog = DialogResult.OK Then XenonTextBox2.Text = dlg.SelectedPath
            dlg.Dispose()
        Else
            If FolderBrowserDialog1.ShowDialog = DialogResult.OK Then XenonTextBox2.Text = FolderBrowserDialog1.SelectedPath
        End If
    End Sub

    Private Sub XenonButton18_Click(sender As Object, e As EventArgs) Handles XenonButton18.Click
        OpenImgDlg.Multiselect = True
        If OpenImgDlg.ShowDialog = DialogResult.OK Then
            For Each x In OpenImgDlg.FileNames
                If Not ListBox1.Items.Contains(x) Then ListBox1.Items.Add(x)
            Next
        End If
        OpenImgDlg.Multiselect = False
    End Sub

    Private Sub XenonButton17_Click(sender As Object, e As EventArgs) Handles XenonButton17.Click
        '' Create a code that delete multiple listbox items
        ''
        '
        '
        ''
        '
        '
        '
        '
        '
    End Sub

    Private Sub XenonTrackbar1_Scroll(sender As Object) Handles XenonTrackbar1.Scroll
        MD.Text = sender.Value
    End Sub

    Private Sub MD_Click(sender As Object, e As EventArgs) Handles MD.Click
        Dim response As String = InputBox(My.Lang.InputValue, sender.text, My.Lang.ItMustBeNumerical)
        sender.Text = Math.Max(Math.Min(Val(response), XenonTrackbar1.Maximum), XenonTrackbar1.Minimum) : XenonTrackbar1.Value = Val(sender.Text)
    End Sub
End Class