Imports WinPaletter.XenonCore
Public Class LogonUI8Colors
    Private Sub LogonUI8Colors_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ApplyDarkMode(Me)

        For Each ri As XenonRadioImage In Controls.OfType(Of XenonRadioImage)
            ri.Image = ColorToBitmap(ri.AccentColor, New Drawing.Size(32, 32))
            If MainFrm.CP.Windows8.LogonUI = ri.Name.Replace("color", "") Then ri.Checked = True Else ri.Checked = False
        Next


    End Sub

    Private Sub XenonButton2_Click(sender As Object, e As EventArgs) Handles XenonButton2.Click
        Me.Close()
    End Sub

    Private Sub XenonButton1_Click(sender As Object, e As EventArgs) Handles XenonButton1.Click

        For Each ri As XenonRadioImage In Controls.OfType(Of XenonRadioImage)
            If ri.Checked Then
                MainFrm.CP.Windows8.LogonUI = ri.Name.Replace("color", "")
                Exit For
            End If
        Next

        Me.Close()
    End Sub
End Class