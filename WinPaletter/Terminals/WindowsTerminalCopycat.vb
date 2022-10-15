Imports WinPaletter.XenonCore

Public Class WindowsTerminalCopycat
    Private Sub WindowsTerminalCopycat_Load(sender As Object, e As EventArgs) Handles Me.Load
        ApplyDarkMode(Me)

        Try
            XenonComboBox1.SelectedIndex = 0
        Catch
        End Try

    End Sub

    Private Sub XenonButton1_Click(sender As Object, e As EventArgs) Handles XenonButton1.Click
        If Not String.IsNullOrEmpty(XenonComboBox1.SelectedItem) Then
            WindowsTerminal.CCat = XenonComboBox1.SelectedItem.ToString
            DialogResult = DialogResult.OK
        Else
            WindowsTerminal.CCat = Nothing
            DialogResult = DialogResult.Cancel
        End If

        Close()
    End Sub

    Private Sub XenonButton2_Click(sender As Object, e As EventArgs) Handles XenonButton2.Click
        DialogResult = DialogResult.Cancel
        Close()
    End Sub

End Class