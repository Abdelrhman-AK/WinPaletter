Imports WinPaletter.XenonCore
Public Class LogonUI8Colors
    Private Sub LogonUI8Colors_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadLanguage
        ApplyDarkMode(Me)

        Icon = Start8Selector.Icon

        color0.Image = Color.FromArgb(34, 34, 34).ToBitmap(New Size(32, 32))
        color1.Image = Color.FromArgb(34, 34, 34).ToBitmap(New Size(32, 32))
        color2.Image = Color.FromArgb(34, 34, 34).ToBitmap(New Size(32, 32))
        color3.Image = Color.FromArgb(34, 34, 34).ToBitmap(New Size(32, 32))
        color4.Image = Color.FromArgb(42, 27, 0).ToBitmap(New Size(32, 32))
        color5.Image = Color.FromArgb(59, 0, 3).ToBitmap(New Size(32, 32))
        color6.Image = Color.FromArgb(65, 0, 49).ToBitmap(New Size(32, 32))
        color7.Image = Color.FromArgb(41, 0, 66).ToBitmap(New Size(32, 32))
        color8.Image = Color.FromArgb(30, 3, 84).ToBitmap(New Size(32, 32))
        color9.Image = Color.FromArgb(0, 31, 66).ToBitmap(New Size(32, 32))
        color10.Image = Color.FromArgb(3, 66, 82).ToBitmap(New Size(32, 32))
        color11.Image = Color.FromArgb(30, 144, 255).ToBitmap(New Size(32, 32))
        color12.Image = Color.FromArgb(4, 63, 0).ToBitmap(New Size(32, 32))
        color13.Image = Color.FromArgb(188, 90, 28).ToBitmap(New Size(32, 32))
        color14.Image = Color.FromArgb(155, 28, 29).ToBitmap(New Size(32, 32))
        color15.Image = Color.FromArgb(152, 28, 90).ToBitmap(New Size(32, 32))
        color16.Image = Color.FromArgb(88, 28, 152).ToBitmap(New Size(32, 32))
        color17.Image = Color.FromArgb(28, 74, 153).ToBitmap(New Size(32, 32))
        color18.Image = Color.FromArgb(69, 143, 221).ToBitmap(New Size(32, 32))
        color19.Image = Color.FromArgb(0, 141, 142).ToBitmap(New Size(32, 32))
        color20.Image = Color.FromArgb(120, 168, 33).ToBitmap(New Size(32, 32))
        color21.Image = Color.FromArgb(191, 142, 16).ToBitmap(New Size(32, 32))
        color22.Image = Color.FromArgb(219, 80, 171).ToBitmap(New Size(32, 32))
        color23.Image = Color.FromArgb(154, 154, 154).ToBitmap(New Size(32, 32))
        color24.Image = Color.FromArgb(88, 88, 88).ToBitmap(New Size(32, 32))

        For Each ri As XenonRadioImage In Controls.OfType(Of XenonRadioImage)
            If My.CP.Windows81.LogonUI = ri.Name.Replace("color", "") Then ri.Checked = True Else ri.Checked = False
        Next


    End Sub

    Private Sub XenonButton2_Click(sender As Object, e As EventArgs) Handles XenonButton2.Click
        Me.Close()
    End Sub

    Private Sub XenonButton1_Click(sender As Object, e As EventArgs) Handles XenonButton1.Click

        For Each ri As XenonRadioImage In Controls.OfType(Of XenonRadioImage)
            If ri.Checked Then
                My.CP.Windows81.LogonUI = ri.Name.Replace("color", "")
                Exit For
            End If
        Next

        Me.Close()
    End Sub
End Class