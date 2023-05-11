Imports System.IO
Imports Microsoft.Win32
Imports WinPaletter.XenonCore
Imports WinPaletter.PreviewHelpers

Public Class Wallpaper_Editor

    Public WT As New CP.Structures.WallpaperTone
    Dim img, img_filled, img_tile As Bitmap
    Dim img_untouched_forTint, img_tinted, img_tinted_filled, img_tinted_tile As Bitmap

    Dim index As Integer = 0
    Dim ImgLs1 As New List(Of String)
    Dim ImgLs2 As New List(Of String)

    Private Sub Wallpaper_Editor_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ApplyDarkMode(Me)
        XenonButton12.Image = MainFrm.XenonButton20.Image.Resize(16, 16)
        If Not My.Settings.Classic_Color_Picker Then Location = New Point(10, (My.Computer.Screen.Bounds.Height - Height) / 2 - 20)
        ApplyFromCP(MainFrm.CP)
        index = 0
        ApplyPreviewStyle()

        Select Case My.PreviewStyle
            Case WindowStyle.W11
                XenonAlertBox3.Text = String.Format(My.Lang.WallpaperTone_Notice, My.Lang.OS_Win11)
            Case WindowStyle.W10
                XenonAlertBox3.Text = String.Format(My.Lang.WallpaperTone_Notice, My.Lang.OS_Win10)
            Case WindowStyle.W8
                XenonAlertBox3.Text = String.Format(My.Lang.WallpaperTone_Notice, My.Lang.OS_Win8)
            Case WindowStyle.W7
                XenonAlertBox3.Text = String.Format(My.Lang.WallpaperTone_Notice, My.Lang.OS_Win7)
            Case WindowStyle.WVista
                XenonAlertBox3.Text = String.Format(My.Lang.WallpaperTone_Notice, My.Lang.OS_WinVista)
            Case WindowStyle.WXP
                XenonAlertBox3.Text = String.Format(My.Lang.WallpaperTone_Notice, My.Lang.OS_WinXP)
            Case Else
                XenonAlertBox3.Text = String.Format(My.Lang.WallpaperTone_Notice, My.Lang.OS_WinUndefined)
        End Select

        MainFrm.Visible = False
    End Sub

    Sub ApplyFromCP(CP As CP)
        With CP.Wallpaper
            WallpaperEnabled.Checked = .Enabled
            XenonRadioButton1.Checked = .SlideShow_Folder_or_ImagesList
            XenonRadioButton2.Checked = Not .SlideShow_Folder_or_ImagesList

            If WT.Enabled Then
                source_wallpapertone.Checked = True
            Else
                Select Case .WallpaperType

                    Case CP.WallpaperType.Picture
                        source_pic.Checked = True
                    Case CP.WallpaperType.SolidColor
                        source_color.Checked = True
                    Case CP.WallpaperType.SlideShow
                        source_slideshow.Checked = True
                    Case Else
                        source_pic.Checked = True
                End Select
            End If

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

            XenonTextBox3.Text = WT.Image

            HBar.Value = WT.H
            SBar.Value = WT.S
            LBar.Value = WT.L

            XenonTextBox2.Text = .Wallpaper_Slideshow_ImagesRootPath
            ListBox1.Items.Clear()
            ListBox1.Items.AddRange(.Wallpaper_Slideshow_Images)
            XenonTrackbar1.Value = .Wallpaper_Slideshow_Interval
            XenonCheckBox3.Checked = .Wallpaper_Slideshow_Shuffle

            pnl_preview.BackColor = CP.Win32.Background
            color_pick.BackColor = CP.Win32.Background
        End With
    End Sub

    Sub ApplyToCP(CP As CP)
        Cursor = Cursors.AppStarting

        With CP.Wallpaper
            .Enabled = WallpaperEnabled.Checked
            .SlideShow_Folder_or_ImagesList = XenonRadioButton1.Checked

            If source_pic.Checked Then
                .WallpaperType = CP.WallpaperType.Picture
                WT.Enabled = False

            ElseIf source_color.Checked Then
                .WallpaperType = CP.WallpaperType.SolidColor
                WT.Enabled = False

            ElseIf source_slideshow.Checked Then
                .WallpaperType = CP.WallpaperType.SlideShow
                WT.Enabled = False

            ElseIf source_wallpapertone.Checked Then
                .WallpaperType = CP.WallpaperType.Picture
                WT.Enabled = True

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

        CP.Win32.Background = color_pick.BackColor

        Cursor = Cursors.Default
    End Sub

    Sub ApplyWT()
        WT = New CP.Structures.WallpaperTone With {
            .Enabled = source_wallpapertone.Checked,
            .Image = XenonTextBox3.Text,
            .H = HBar.Value,
            .S = SBar.Value,
            .L = LBar.Value
            }

        Select Case My.PreviewStyle
            Case WindowStyle.W11
                MainFrm.CP.WallpaperTone_W11 = WT
            Case WindowStyle.W10
                MainFrm.CP.WallpaperTone_W10 = WT
            Case WindowStyle.W8
                MainFrm.CP.WallpaperTone_W8 = WT
            Case WindowStyle.W7
                MainFrm.CP.WallpaperTone_W7 = WT
            Case WindowStyle.WVista
                MainFrm.CP.WallpaperTone_WVista = WT
            Case WindowStyle.WXP
                MainFrm.CP.WallpaperTone_WXP = WT
            Case Else
                MainFrm.CP.WallpaperTone_W11 = WT

        End Select
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
        Using _Def As CP = CP_Defaults.From(My.PreviewStyle)
            ApplyFromCP(_Def)
        End Using
    End Sub

    Private Sub XenonButton8_Click(sender As Object, e As EventArgs) Handles XenonButton8.Click
        ApplyToCP(MainFrm.CP)
        ApplyWT()
        MainFrm.ApplyStylesToElements(MainFrm.CP)
        Close()
    End Sub

    Private Sub XenonButton10_Click(sender As Object, e As EventArgs) Handles XenonButton10.Click
        Cursor = Cursors.WaitCursor
        Dim CPx As New CP(CP.CP_Type.Registry)
        ApplyToCP(CPx)
        ApplyToCP(MainFrm.CP)
        ApplyWT()

        CPx.Wallpaper.Apply(source_wallpapertone.Checked)
        CPx.Win32.Apply()

        If source_wallpapertone.Checked Then
            WT.Apply()
        End If

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
        If My.PreviewStyle = WindowStyle.WXP Then
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

        If source_slideshow.Checked AndAlso XenonRadioButton2.Checked Then
            Set_SlideshowSource()
            ApplyPreviewStyle()
        End If
    End Sub

    Private Sub XenonButton17_Click(sender As Object, e As EventArgs) Handles XenonButton17.Click
        If ListBox1.SelectedItem IsNot Nothing Then
            Dim items As New ArrayList(ListBox1.SelectedItems)
            For Each item In items
                ListBox1.Items.Remove(item)
            Next
        End If

        If source_slideshow.Checked AndAlso XenonRadioButton2.Checked Then
            Set_SlideshowSource()
            ApplyPreviewStyle()
        End If
    End Sub

    Private Sub XenonTrackbar1_Scroll(sender As Object) Handles XenonTrackbar1.Scroll
        MD.Text = sender.Value
    End Sub

    Private Sub MD_Click(sender As Object, e As EventArgs) Handles MD.Click
        Dim response As String = InputBox(My.Lang.InputValue, sender.text, My.Lang.ItMustBeNumerical)
        sender.Text = Math.Max(Math.Min(Val(response), XenonTrackbar1.Maximum), XenonTrackbar1.Minimum) : XenonTrackbar1.Value = Val(sender.Text)
    End Sub

    Public Sub MoveItem(direction As Integer)
        If ListBox1.SelectedItem Is Nothing OrElse ListBox1.SelectedIndex < 0 Then Return
        Dim newIndex As Integer = ListBox1.SelectedIndex + direction
        If newIndex < 0 OrElse newIndex >= ListBox1.Items.Count Then Return
        Dim selected As Object = ListBox1.SelectedItem
        ListBox1.Items.Remove(selected)
        ListBox1.Items.Insert(newIndex, selected)
        ListBox1.SetSelected(newIndex, True)
    End Sub

    Private Sub XenonButton5_Click(sender As Object, e As EventArgs) Handles XenonButton5.Click
        MoveItem(-1)

        If source_slideshow.Checked AndAlso XenonRadioButton2.Checked Then
            Set_SlideshowSource()
            ApplyPreviewStyle()
        End If
    End Sub

    Private Sub XenonButton6_Click(sender As Object, e As EventArgs) Handles XenonButton6.Click
        MoveItem(+1)

        If source_slideshow.Checked AndAlso XenonRadioButton2.Checked Then
            Set_SlideshowSource()
            ApplyPreviewStyle()
        End If
    End Sub

    Function GetWall(file As String) As Bitmap
        If IO.File.Exists(file) Then
            Try
                Using bmp As New Bitmap(Bitmap_Mgr.Load(file))

                    Dim ScaleW As Single = 1
                    Dim ScaleH As Single = 1

                    If bmp.Width > Screen.PrimaryScreen.Bounds.Size.Width Or bmp.Height > Screen.PrimaryScreen.Bounds.Size.Height Then
                        ScaleW = 1920 / pnl_preview.Size.Width
                        ScaleH = 1080 / pnl_preview.Size.Height
                    End If

                    Return bmp.Resize(bmp.Width / ScaleW, bmp.Height / ScaleH)
                End Using
            Catch
                Return Nothing
            End Try
        Else
            Return Nothing
        End If
    End Function

    Private Sub Source_pic_CheckedChanged(sender As Object) Handles source_pic.CheckedChanged, source_color.CheckedChanged, source_wallpapertone.CheckedChanged
        If sender.Checked Then
            Set_PicSource()
            ApplyHSLPreview()
            ApplyPreviewStyle()
        End If

        Panel1.Visible = False
    End Sub

    Private Sub Source_slideshow_CheckedChanged(sender As Object) Handles source_slideshow.CheckedChanged
        If sender.Checked Then
            Set_SlideshowSource()
            ApplyPreviewStyle()
        End If
        Panel1.Visible = True
    End Sub

    Sub ApplyPreviewStyle()
        Dim temp As Bitmap

        If source_color.Checked Then
            temp = Nothing
        Else

            If Not source_wallpapertone.Checked Then
                If style_fill.Checked Then
                    temp = img_filled

                ElseIf style_tile.Checked Then
                    temp = img_tile

                Else
                    temp = img

                End If
            Else
                If style_fill.Checked Then
                    temp = img_tinted_filled

                ElseIf style_tile.Checked Then
                    temp = img_tinted_tile

                Else
                    temp = img_tinted

                End If
            End If

        End If

        pnl_preview.Image = temp

        If style_fill.Checked Then
            pnl_preview.SizeMode = PictureBoxSizeMode.CenterImage

        ElseIf style_fit.Checked Then
            pnl_preview.SizeMode = PictureBoxSizeMode.Zoom

        ElseIf style_stretch.Checked Then
            pnl_preview.SizeMode = PictureBoxSizeMode.StretchImage

        ElseIf style_center.Checked Then
            pnl_preview.SizeMode = PictureBoxSizeMode.CenterImage

        ElseIf style_tile.Checked Then
            pnl_preview.SizeMode = PictureBoxSizeMode.Normal
        End If
    End Sub

    Private Sub XenonTextBox1_TextChanged(sender As Object, e As EventArgs) Handles XenonTextBox1.TextChanged
        Set_PicSource()
        ApplyPreviewStyle()
    End Sub

    Private Sub XenonTextBox2_TextChanged(sender As Object, e As EventArgs) Handles XenonTextBox2.TextChanged
        If XenonRadioButton1.Checked Then
            Set_SlideshowSource()
            ApplyPreviewStyle()
        End If
    End Sub

    Private Sub XenonTextBox3_TextChanged(sender As Object, e As EventArgs) Handles XenonTextBox3.TextChanged
        Set_PicSource()
        ApplyHSLPreview()
    End Sub

    Private Sub XenonRadioButton1_CheckedChanged(sender As Object) Handles XenonRadioButton1.CheckedChanged, XenonRadioButton2.CheckedChanged
        If sender.Checked Then
            Set_SlideshowSource()
            ApplyPreviewStyle()
        End If
    End Sub

    Private Sub XenonButton13_Click(sender As Object, e As EventArgs) Handles XenonButton13.Click
        If XenonRadioButton1.Checked Then
            If index + 1 <= ImgLs1.Count - 1 Then index += 1 Else index = 0
        Else
            If index + 1 <= ImgLs2.Count - 1 Then index += 1 Else index = 0
        End If

        Set_SlideshowSource()
        ApplyPreviewStyle()
    End Sub

    Private Sub XenonButton14_Click(sender As Object, e As EventArgs) Handles XenonButton14.Click
        If XenonRadioButton1.Checked Then
            If index - 1 > 0 Then index -= 1 Else index = ImgLs2.Count - 1
        Else
            If index - 1 > 0 Then index -= 1 Else index = ImgLs2.Count - 1
        End If
        Set_SlideshowSource()
        ApplyPreviewStyle()
    End Sub

    Sub Set_PicSource()
        Cursor = Cursors.AppStarting

        If source_pic.Checked Then
            If IO.File.Exists(XenonTextBox1.Text) Then
                img = GetWall(XenonTextBox1.Text)
                img_filled = FillScale(img.Clone, pnl_preview.Size)
                img_tile = DirectCast(img.Clone, Bitmap).Tile(pnl_preview.Size)
            Else
                img = Nothing
                img_filled = Nothing
                img_tile = Nothing
            End If

        ElseIf source_wallpapertone.Checked Then
            If IO.File.Exists(XenonTextBox3.Text) Then
                img_untouched_forTint = GetWall(XenonTextBox3.Text)
                ApplyHSLPreview()
            End If

        End If

        Cursor = Cursors.Default
    End Sub

    Private Sub Color_pick_Click(sender As Object, e As EventArgs) Handles color_pick.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            Dim clr As Color = SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                pnl_preview.BackColor = clr
            End If
            Exit Sub
        End If

        Dim CList As New List(Of Control) From {pnl_preview}
        Dim C As Color = ColorPickerDlg.Pick(CList)
        sender.BackColor = Color.FromArgb(255, C)

        CList.Clear()
    End Sub

    Sub Set_SlideshowSource()

        Cursor = Cursors.AppStarting

        If source_slideshow.Checked Then

            If XenonRadioButton1.Checked Then

                If IO.Directory.Exists(XenonTextBox2.Text) Then
                    ImgLs1.Clear()
                    ImgLs1.AddRange(Directory.EnumerateFiles(XenonTextBox2.Text, "*.*", SearchOption.TopDirectoryOnly).Where(Function(s)
                                                                                                                                 Return s.EndsWith(".bmp") _
                                                                                                                                OrElse s.EndsWith(".jpg") _
                                                                                                                                OrElse s.EndsWith(".png") _
                                                                                                                                OrElse s.EndsWith(".gif")
                                                                                                                             End Function))
                    If index > ImgLs1.Count - 1 Then index = 0

                    img = GetWall(ImgLs1(index))
                    img_filled = FillScale(img.Clone, pnl_preview.Size)
                    img_tile = DirectCast(img.Clone, Bitmap).Tile(pnl_preview.Size)

                    Label3.Text = index + 1 & "/" & ImgLs1.Count
                Else
                    img = Nothing
                    img_filled = Nothing
                    img_tile = Nothing
                    Label3.Text = "0/0"

                End If

            Else
                ImgLs2.Clear()

                For Each item As String In ListBox1.Items
                    If IO.File.Exists(item) Then ImgLs2.Add(item)
                Next

                If index > ImgLs2.Count - 1 Then index = 0
                img = GetWall(ImgLs2(index))
                img_filled = FillScale(img.Clone, pnl_preview.Size)
                img_tile = DirectCast(img.Clone, Bitmap).Tile(pnl_preview.Size)

                Label3.Text = index + 1 & "/" & ImgLs2.Count
            End If
        End If

        Cursor = Cursors.Default
    End Sub

    Private Sub Style_fill_CheckedChanged(sender As Object) Handles style_fill.CheckedChanged, style_fit.CheckedChanged, style_stretch.CheckedChanged, style_center.CheckedChanged, style_tile.CheckedChanged
        If sender.Checked Then ApplyPreviewStyle()
    End Sub

    Private Sub Wallpaper_Editor_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        MainFrm.Visible = True
    End Sub

    Sub ApplyHSLPreview()
        If source_wallpapertone.Enabled AndAlso img_untouched_forTint IsNot Nothing Then
            Using ImgF As New ImageProcessor.ImageFactory
                ImgF.Load(img_untouched_forTint)
                ImgF.Hue(HBar.Value, True)
                ImgF.Saturation(SBar.Value - 100)
                ImgF.Brightness(LBar.Value - 100)

                img_tinted = ImgF.Image.Clone
                img_tinted_filled = FillScale(img_tinted.Clone, pnl_preview.Size)
                img_tinted_tile = DirectCast(img_tinted.Clone, Bitmap).Tile(pnl_preview.Size)
            End Using

            ApplyPreviewStyle()
        End If
    End Sub

    Private Sub XenonColorBar1_Scroll(sender As Object) Handles HBar.Scroll
        HB.Text = sender.Value.ToString

        Dim HSL_ As New HSL_Structure
        HSL_ = Color.FromArgb(0, 255, 240).ToHSL()
        HSL_.H = sender.Value
        HSL_.S = 1
        HSL_.L = 0.5

        SBar.AccentColor = HSL_.ToRGB
        SBar.H = HSL_.H

        LBar.AccentColor = HSL_.ToRGB
        LBar.H = HSL_.H

        ApplyHSLPreview()
    End Sub

    Private Sub XenonColorBar2_Scroll_1(sender As Object) Handles SBar.Scroll
        SB.Text = sender.Value.ToString
        ApplyHSLPreview()
    End Sub

    Private Sub XenonColorBar3_Scroll(sender As Object) Handles LBar.Scroll
        LB.Text = sender.Value.ToString
        ApplyHSLPreview()
    End Sub

    Private Sub HB_Click(sender As Object, e As EventArgs) Handles HB.Click
        Dim response As String = InputBox(My.Lang.InputValue, sender.text, My.Lang.ItMustBeNumerical)
        sender.Text = Math.Max(Math.Min(Val(response), HBar.Maximum), HBar.Minimum) : HBar.Value = Val(sender.Text)
        ApplyHSLPreview()
    End Sub

    Private Sub XenonButton20_Click(sender As Object, e As EventArgs) Handles XenonButton20.Click
        If IO.File.Exists(XenonTextBox3.Text) AndAlso SaveFileDialog2.ShowDialog = DialogResult.OK Then
            Using ImgF As New ImageProcessor.ImageFactory
                ImgF.Load(XenonTextBox3.Text)
                ImgF.Hue(HBar.Value, True)
                ImgF.Saturation(SBar.Value - 100)
                ImgF.Brightness(LBar.Value - 100)
                ImgF.Image.Save(SaveFileDialog2.FileName)
            End Using
        End If
    End Sub

    Private Sub XenonButton23_Click(sender As Object, e As EventArgs) Handles XenonButton23.Click
        HBar.Value = 0
    End Sub

    Private Sub XenonButton22_Click(sender As Object, e As EventArgs) Handles XenonButton22.Click
        SBar.Value = 100
    End Sub

    Private Sub XenonButton21_Click(sender As Object, e As EventArgs) Handles XenonButton21.Click
        LBar.Value = 100
    End Sub

    Private Sub XenonButton16_Click(sender As Object, e As EventArgs) Handles XenonButton16.Click
        Dim R1 As RegistryKey = Registry.CurrentUser.OpenSubKey("Control Panel\Desktop", True)
        Dim WallpaperPath As String = R1.GetValue("Wallpaper").ToString()
        If R1 IsNot Nothing Then R1.Close()

        If Not IO.File.Exists(WallpaperPath) Then
            If My.PreviewStyle = WindowStyle.WXP Then
                WallpaperPath = My.PATH_Windows & "\Web\Wallpaper\Bliss.bmp"
            Else
                WallpaperPath = My.PATH_Windows & "\Web\Wallpaper\Windows\img0.jpg"
            End If
        End If

        XenonTextBox3.Text = WallpaperPath
        ApplyHSLPreview()
    End Sub

    Private Sub XenonButton15_Click(sender As Object, e As EventArgs) Handles XenonButton15.Click
        If My.PreviewStyle = WindowStyle.WXP Then
            XenonTextBox3.Text = My.PATH_Windows & "\Web\Wallpaper\Bliss.bmp"
        Else
            XenonTextBox3.Text = My.PATH_Windows & "\Web\Wallpaper\Windows\img0.jpg"
        End If

        If Not IO.File.Exists(XenonTextBox1.Text) Then
            Dim R1 As RegistryKey = Registry.CurrentUser.OpenSubKey("Control Panel\Desktop", True)
            XenonTextBox3.Text = R1.GetValue("Wallpaper").ToString()
            If R1 IsNot Nothing Then R1.Close()
        End If
        ApplyHSLPreview()
    End Sub

    Private Sub XenonButton19_Click(sender As Object, e As EventArgs) Handles XenonButton19.Click
        If OpenImgDlg.ShowDialog = DialogResult.OK Then
            XenonTextBox3.Text = OpenImgDlg.FileName
            ApplyHSLPreview()
        End If
    End Sub

    Private Sub SB_Click(sender As Object, e As EventArgs) Handles SB.Click
        Dim response As String = InputBox(My.Lang.InputValue, sender.text, My.Lang.ItMustBeNumerical)
        sender.Text = Math.Max(Math.Min(Val(response), SBar.Maximum), SBar.Minimum) : SBar.Value = Val(sender.Text)
        ApplyHSLPreview()
    End Sub

    Private Sub LB_Click(sender As Object, e As EventArgs) Handles LB.Click
        Dim response As String = InputBox(My.Lang.InputValue, sender.text, My.Lang.ItMustBeNumerical)
        sender.Text = Math.Max(Math.Min(Val(response), LBar.Maximum), LBar.Minimum) : LBar.Value = Val(sender.Text)
        ApplyHSLPreview()
    End Sub
End Class