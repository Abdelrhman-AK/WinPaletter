Public Class WindowsTerminalCopycat
    Private Sub WindowsTerminalCopycat_Load(sender As Object, e As EventArgs) Handles Me.Load
        LoadLanguage
        ApplyStyle(Me)
        Icon = WindowsTerminal.Icon

        Try
            ComboBox1.SelectedIndex = 0
        Catch
        End Try

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Not String.IsNullOrEmpty(ComboBox1.SelectedItem) Then
            WindowsTerminal.CCat = ComboBox1.SelectedItem.ToString
            DialogResult = DialogResult.OK
        Else
            WindowsTerminal.CCat = Nothing
            DialogResult = DialogResult.Cancel
        End If

        Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        DialogResult = DialogResult.Cancel
        Close()
    End Sub

End Class