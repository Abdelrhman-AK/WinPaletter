Imports Microsoft.Win32

Public Class NewExtTerminal
    Private Sub XenonButton16_Click(sender As Object, e As EventArgs) Handles XenonButton16.Click
        If OpenFileDialog1.ShowDialog = DialogResult.OK Then
            XenonTextBox1.Text = OpenFileDialog1.FileName
        End If
    End Sub

    Private Sub XenonButton1_Click(sender As Object, e As EventArgs) Handles XenonButton1.Click

        Try
            Registry.CurrentUser.CreateSubKey(String.Format("Console\%SystemDrive%_{0}", XenonTextBox1.Text.Replace("\", "_").Trim(":")(1)), True).Close()

            MsgBox("This key is entered into registry successfully.", MsgBoxStyle.Information + My.Application.MsgboxRt)
            ExternalTerminal.FillTerminals(ExternalTerminal.XenonComboBox1)
        Catch ex As Exception
            MsgBox("Couldn't write this entry to registry. Please check if this key already exists or check permissions." & vbCrLf & vbCrLf & "Error Details: " & ex.Message, MsgBoxStyle.Critical + My.Application.MsgboxRt)
        End Try

    End Sub
End Class