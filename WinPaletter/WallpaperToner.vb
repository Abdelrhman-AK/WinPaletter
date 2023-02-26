Imports System.IO
Imports Microsoft.Win32
Imports WinPaletter.XenonCore
Public Class WallpaperToner

    Public WT As New CP.Structures.WallpaperTone
    Dim img As Bitmap
    Private _Shown As Boolean = False

    Private Sub WallpaperTinter_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ApplyDarkMode(Me)
        XenonButton12.Image = MainFrm.XenonButton20.Image.Resize(16, 16)
        LoadFromWT(WT)
        ApplyPreview()
        _Shown = False
        MainFrm.MakeItDoubleBuffered(pnl_preview)

        Select Case MainFrm.PreviewConfig
            Case MainFrm.WinVer.W11
                XenonAlertBox1.Text = String.Format(My.Lang.WallpaperTone_Notice, My.Lang.OS_Win11)
            Case MainFrm.WinVer.W10
                XenonAlertBox1.Text = String.Format(My.Lang.WallpaperTone_Notice, My.Lang.OS_Win10)
            Case MainFrm.WinVer.W8
                XenonAlertBox1.Text = String.Format(My.Lang.WallpaperTone_Notice, My.Lang.OS_Win8)
            Case MainFrm.WinVer.W7
                XenonAlertBox1.Text = String.Format(My.Lang.WallpaperTone_Notice, My.Lang.OS_Win7)
            Case MainFrm.WinVer.WVista
                XenonAlertBox1.Text = String.Format(My.Lang.WallpaperTone_Notice, My.Lang.OS_WinVista)
            Case MainFrm.WinVer.WXP
                XenonAlertBox1.Text = String.Format(My.Lang.WallpaperTone_Notice, My.Lang.OS_WinXP)
            Case Else
                XenonAlertBox1.Text = String.Format(My.Lang.WallpaperTone_Notice, My.Lang.OS_WinUndefined)
        End Select
    End Sub

    Sub LoadFromWT([WT] As CP.Structures.WallpaperTone)
        ToneEnabled.Checked = [WT].Enabled
        XenonTextBox1.Text = [WT].Image

        If Not IO.File.Exists([WT].Image) Then
            If MainFrm.PreviewConfig = MainFrm.WinVer.WXP Then
                [WT].Image = My.PATH_Windows & "\Web\Wallpaper\Bliss.bmp"
            Else
                [WT].Image = My.PATH_Windows & "\Web\Wallpaper\Windows\img0.jpg"
            End If
        End If

        If Not IO.File.Exists([WT].Image) Then
            Dim R1 As RegistryKey = Registry.CurrentUser.OpenSubKey("Control Panel\Desktop", True)
            [WT].Image = R1.GetValue("Wallpaper").ToString()
            If R1 IsNot Nothing Then R1.Close()
        End If

        Dim S As New FileStream([WT].Image, IO.FileMode.Open, IO.FileAccess.Read)
        img = Image.FromStream(S).Resize(pnl_preview.Size)
        S.Close()

        HBar.Value = [WT].H
        SBar.Value = [WT].S
        LBar.Value = [WT].L
    End Sub

    Function ApplyToWT() As CP.Structures.WallpaperTone
        Return New CP.Structures.WallpaperTone With {
        .Enabled = ToneEnabled.Checked,
        .Image = XenonTextBox1.Text,
        .H = HBar.Value,
        .S = SBar.Value,
        .L = LBar.Value
        }
    End Function

    Sub ApplyPreview()
        Dim HSL As New HSLFilter With {
            .Hue = HBar.Value,
            .Saturation = (SBar.Value - 50) * 2,
            .Lightness = (LBar.Value - 50) * 2
        }
        pnl_preview.BackgroundImage = HSL.ExecuteFilter(img)
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

        If _Shown Then ApplyPreview()
    End Sub

    Private Sub XenonColorBar2_Scroll_1(sender As Object) Handles SBar.Scroll
        SB.Text = sender.Value.ToString
        If _Shown Then ApplyPreview()
    End Sub

    Private Sub XenonColorBar3_Scroll(sender As Object) Handles LBar.Scroll
        LB.Text = sender.Value.ToString
        If _Shown Then ApplyPreview()
    End Sub

    Private Sub HB_Click(sender As Object, e As EventArgs) Handles HB.Click
        Dim response As String = InputBox(My.Lang.InputValue, sender.text, My.Lang.ItMustBeNumerical)
        sender.Text = Math.Max(Math.Min(Val(response), HBar.Maximum), HBar.Minimum) : HBar.Value = Val(sender.Text)
    End Sub

    Private Sub SB_Click(sender As Object, e As EventArgs) Handles SB.Click
        Dim response As String = InputBox(My.Lang.InputValue, sender.text, My.Lang.ItMustBeNumerical)
        sender.Text = Math.Max(Math.Min(Val(response), SBar.Maximum), SBar.Minimum) : SBar.Value = Val(sender.Text)
    End Sub

    Private Sub LB_Click(sender As Object, e As EventArgs) Handles LB.Click
        Dim response As String = InputBox(My.Lang.InputValue, sender.text, My.Lang.ItMustBeNumerical)
        sender.Text = Math.Max(Math.Min(Val(response), LBar.Maximum), LBar.Minimum) : LBar.Value = Val(sender.Text)
    End Sub

    Private Sub WallpaperTinter_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        _Shown = True
    End Sub

    Private Sub XenonButton3_Click(sender As Object, e As EventArgs) Handles XenonButton3.Click
        If MainFrm.PreviewConfig = MainFrm.WinVer.WXP Then
            XenonTextBox1.Text = My.PATH_Windows & "\Web\Wallpaper\Bliss.bmp"
        Else
            XenonTextBox1.Text = My.PATH_Windows & "\Web\Wallpaper\Windows\img0.jpg"
        End If

        If Not IO.File.Exists(XenonTextBox1.Text) Then
            Dim R1 As RegistryKey = Registry.CurrentUser.OpenSubKey("Control Panel\Desktop", True)
            XenonTextBox1.Text = R1.GetValue("Wallpaper").ToString()
            If R1 IsNot Nothing Then R1.Close()
        End If
    End Sub

    Private Sub XenonButton1_Click(sender As Object, e As EventArgs) Handles XenonButton1.Click
        If OpenImgDlg.ShowDialog = DialogResult.OK Then
            XenonTextBox1.Text = OpenImgDlg.FileName
        End If
    End Sub

    Private Sub XenonButton2_Click(sender As Object, e As EventArgs) Handles XenonButton2.Click
        Dim R1 As RegistryKey = Registry.CurrentUser.OpenSubKey("Control Panel\Desktop", True)
        Dim WallpaperPath As String = R1.GetValue("Wallpaper").ToString()
        If R1 IsNot Nothing Then R1.Close()

        If Not IO.File.Exists(WallpaperPath) Then
            If MainFrm.PreviewConfig = MainFrm.WinVer.WXP Then
                WallpaperPath = My.PATH_Windows & "\Web\Wallpaper\Bliss.bmp"
            Else
                WallpaperPath = My.PATH_Windows & "\Web\Wallpaper\Windows\img0.jpg"
            End If
        End If

        XenonTextBox1.Text = WallpaperPath

    End Sub

    Private Sub XenonTextBox1_TextChanged(sender As Object, e As EventArgs) Handles XenonTextBox1.TextChanged

        If _Shown AndAlso IO.File.Exists(XenonTextBox1.Text) Then
            Dim S As New FileStream(XenonTextBox1.Text, IO.FileMode.Open, IO.FileAccess.Read)
            img = Image.FromStream(S).Resize(pnl_preview.Size)
            S.Close()
            ApplyPreview()
        End If

    End Sub

    Private Sub XenonButton8_Click(sender As Object, e As EventArgs) Handles XenonButton8.Click

        Select Case MainFrm.PreviewConfig
            Case MainFrm.WinVer.W11
                MainFrm.CP.WallpaperTone_W11 = ApplyToWT()
            Case MainFrm.WinVer.W10
                MainFrm.CP.WallpaperTone_W10 = ApplyToWT()
            Case MainFrm.WinVer.W8
                MainFrm.CP.WallpaperTone_W8 = ApplyToWT()
            Case MainFrm.WinVer.W7
                MainFrm.CP.WallpaperTone_W7 = ApplyToWT()
            Case MainFrm.WinVer.WVista
                MainFrm.CP.WallpaperTone_WVista = ApplyToWT()
            Case MainFrm.WinVer.WXP
                MainFrm.CP.WallpaperTone_WXP = ApplyToWT()
            Case Else
                MainFrm.CP.WallpaperTone_W11 = ApplyToWT()

        End Select

        MainFrm.Adjust_Preview()

        Close()
    End Sub

    Private Sub XenonButton7_Click(sender As Object, e As EventArgs) Handles XenonButton7.Click
        Me.Close()
    End Sub

    Private Sub XenonButton10_Click(sender As Object, e As EventArgs) Handles XenonButton10.Click
        Cursor = Cursors.WaitCursor
        ApplyToWT().Apply()
        Cursor = Cursors.Default
    End Sub

    Private Sub XenonButton11_Click(sender As Object, e As EventArgs) Handles XenonButton11.Click
        If OpenFileDialog1.ShowDialog = DialogResult.OK Then
            Cursor = Cursors.WaitCursor

            Dim CPx As New CP(CP.CP_Type.File, OpenFileDialog1.FileName)

            Select Case MainFrm.PreviewConfig
                Case MainFrm.WinVer.W11
                    LoadFromWT(CPx.WallpaperTone_W11)
                Case MainFrm.WinVer.W10
                    LoadFromWT(CPx.WallpaperTone_W10)
                Case MainFrm.WinVer.W8
                    LoadFromWT(CPx.WallpaperTone_W8)
                Case MainFrm.WinVer.W7
                    LoadFromWT(CPx.WallpaperTone_W7)
                Case MainFrm.WinVer.WVista
                    LoadFromWT(CPx.WallpaperTone_WVista)
                Case MainFrm.WinVer.WXP
                    LoadFromWT(CPx.WallpaperTone_WXP)
                Case Else
                    LoadFromWT(CPx.WallpaperTone_W11)

            End Select

            CPx.Dispose()

            Cursor = Cursors.Default
        End If
    End Sub

    Private Sub XenonButton9_Click(sender As Object, e As EventArgs) Handles XenonButton9.Click
        Cursor = Cursors.WaitCursor

        Dim CPx As New CP(CP.CP_Type.Registry)

        Select Case MainFrm.PreviewConfig
            Case MainFrm.WinVer.W11
                LoadFromWT(CPx.WallpaperTone_W11)
            Case MainFrm.WinVer.W10
                LoadFromWT(CPx.WallpaperTone_W10)
            Case MainFrm.WinVer.W8
                LoadFromWT(CPx.WallpaperTone_W8)
            Case MainFrm.WinVer.W7
                LoadFromWT(CPx.WallpaperTone_W7)
            Case MainFrm.WinVer.WVista
                LoadFromWT(CPx.WallpaperTone_WVista)
            Case MainFrm.WinVer.WXP
                LoadFromWT(CPx.WallpaperTone_WXP)
            Case Else
                LoadFromWT(CPx.WallpaperTone_W11)

        End Select

        CPx.Dispose()

        Cursor = Cursors.Default
    End Sub

    Private Sub XenonButton12_Click(sender As Object, e As EventArgs) Handles XenonButton12.Click
        Cursor = Cursors.WaitCursor

        Dim CPx As CP

        Select Case MainFrm.PreviewConfig
            Case MainFrm.WinVer.W11
                CPx = New CP_Defaults().Default_Windows11
                LoadFromWT(CPx.WallpaperTone_W11)
            Case MainFrm.WinVer.W10
                CPx = New CP_Defaults().Default_Windows10
                LoadFromWT(CPx.WallpaperTone_W10)
            Case MainFrm.WinVer.W8
                CPx = New CP_Defaults().Default_Windows8
                LoadFromWT(CPx.WallpaperTone_W8)
            Case MainFrm.WinVer.W7
                CPx = New CP_Defaults().Default_Windows7
                LoadFromWT(CPx.WallpaperTone_W7)
            Case MainFrm.WinVer.WVista
                CPx = New CP_Defaults().Default_WindowsVista
                LoadFromWT(CPx.WallpaperTone_WVista)
            Case MainFrm.WinVer.WXP
                CPx = New CP_Defaults().Default_WindowsXP
                LoadFromWT(CPx.WallpaperTone_WXP)
            Case Else
                CPx = New CP_Defaults().Default_Windows11
                LoadFromWT(CPx.WallpaperTone_W11)
        End Select

        CPx.Dispose()

        Cursor = Cursors.Default
    End Sub

    Private Sub XenonButton4_Click(sender As Object, e As EventArgs) Handles XenonButton4.Click
        If SaveFileDialog2.ShowDialog = DialogResult.OK Then
            Dim HSL As New HSLFilter With {
                .Hue = HBar.Value,
                .Saturation = (SBar.Value - 50) * 2,
                .Lightness = (LBar.Value - 50) * 2
            }

            Dim img As Bitmap
            If Not IO.File.Exists(XenonTextBox1.Text) Then Throw New IO.IOException("Couldn't Find image")
            Dim S As New IO.FileStream(XenonTextBox1.Text, IO.FileMode.Open, IO.FileAccess.Read)
            img = System.Drawing.Image.FromStream(S)
            S.Close()
            S.Dispose()
            HSL.ExecuteFilter(img).Save(SaveFileDialog2.FileName)
        End If
    End Sub
End Class