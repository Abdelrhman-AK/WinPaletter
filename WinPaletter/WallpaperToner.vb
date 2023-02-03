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
            Case Else
                XenonAlertBox1.Text = String.Format(My.Lang.WallpaperTone_Notice, My.Lang.OS_WinUndefined)
        End Select
    End Sub

    Sub LoadFromWT([WT] As CP.Structures.WallpaperTone)
        TintEnabled.Checked = [WT].Enabled
        XenonTextBox1.Text = [WT].Image

        Dim S As New FileStream([WT].Image, IO.FileMode.Open, IO.FileAccess.Read)
        img = Image.FromStream(S).Resize(pnl_preview.Size)
        S.Close()

        HBar.Value = [WT].H
        SBar.Value = [WT].S
        LBar.Value = [WT].L
    End Sub

    Function ApplyToWT() As CP.Structures.WallpaperTone
        Return New CP.Structures.WallpaperTone With {
            .Enabled = TintEnabled.Checked,
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
        Dim ib As New Ookii.Dialogs.WinForms.InputDialog With {
                .MainInstruction = My.Lang.InputValue,
                .Input = sender.text,
                .Content = My.Lang.ItMustBeNumerical,
                .WindowTitle = "WinPaletter"
               }

        If ib.ShowDialog() = DialogResult.OK Then
            Dim response As String = ib.Input : If String.IsNullOrWhiteSpace(response) Then response = sender.Text
            sender.Text = Math.Max(Math.Min(Val(response), HBar.Maximum), HBar.Minimum) : HBar.Value = Val(sender.Text)
        End If

        ib.Dispose()

    End Sub

    Private Sub SB_Click(sender As Object, e As EventArgs) Handles SB.Click
        Dim ib As New Ookii.Dialogs.WinForms.InputDialog With {
        .MainInstruction = My.Lang.InputValue,
        .Input = sender.text,
        .Content = My.Lang.ItMustBeNumerical,
        .WindowTitle = "WinPaletter"
       }

        If ib.ShowDialog() = DialogResult.OK Then
            Dim response As String = ib.Input : If String.IsNullOrWhiteSpace(response) Then response = sender.Text
            sender.Text = Math.Max(Math.Min(Val(response), SBar.Maximum), SBar.Minimum) : SBar.Value = Val(sender.Text)
        End If

        ib.Dispose()
    End Sub

    Private Sub LB_Click(sender As Object, e As EventArgs) Handles LB.Click
        Dim ib As New Ookii.Dialogs.WinForms.InputDialog With {
        .MainInstruction = My.Lang.InputValue,
        .Input = sender.text,
        .Content = My.Lang.ItMustBeNumerical,
        .WindowTitle = "WinPaletter"
       }

        If ib.ShowDialog() = DialogResult.OK Then
            Dim response As String = ib.Input : If String.IsNullOrWhiteSpace(response) Then response = sender.Text
            sender.Text = Math.Max(Math.Min(Val(response), LBar.Maximum), LBar.Minimum) : LBar.Value = Val(sender.Text)
        End If

        ib.Dispose()
    End Sub

    Private Sub WallpaperTinter_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        _Shown = True
    End Sub

    Private Sub XenonButton3_Click(sender As Object, e As EventArgs) Handles XenonButton3.Click
        XenonTextBox1.Text = Environment.GetFolderPath(Environment.SpecialFolder.Windows) & "\Web\Wallpaper\Windows\img0.jpg"
    End Sub

    Private Sub XenonButton1_Click(sender As Object, e As EventArgs) Handles XenonButton1.Click
        If OpenFileDialog1.ShowDialog = DialogResult.OK Then
            XenonTextBox1.Text = OpenFileDialog1.FileName
        End If
    End Sub

    Private Sub XenonButton2_Click(sender As Object, e As EventArgs) Handles XenonButton2.Click
        Dim R1 As RegistryKey = Registry.CurrentUser.OpenSubKey("Control Panel\Desktop", True)
        Dim WallpaperPath As String = R1.GetValue("Wallpaper").ToString()
        If R1 IsNot Nothing Then R1.Close()
        If Not IO.File.Exists(WallpaperPath) Then WallpaperPath = Environment.GetFolderPath(Environment.SpecialFolder.Windows) & "\Web\Wallpaper\Windows\img0.jpg"
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
End Class