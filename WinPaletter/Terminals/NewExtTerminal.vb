Imports Microsoft.Win32

Public Class NewExtTerminal
    Private Sub XenonButton16_Click(sender As Object, e As EventArgs) Handles XenonButton16.Click
        If OpenFileDialog1.ShowDialog = DialogResult.OK Then
            XenonTextBox1.Text = OpenFileDialog1.FileName
        End If
    End Sub

    Private Sub XenonButton1_Click(sender As Object, e As EventArgs) Handles XenonButton1.Click

        Try
            If String.IsNullOrWhiteSpace(XenonTextBox1.Text) Then
                MsgBox(My.Lang.Terminal_External_Empty, My.Application.MsgboxRt(MsgBoxStyle.Critical))

            ElseIf Not IO.File.Exists(XenonTextBox1.Text) Then
                MsgBox(My.Lang.Terminal_External_NotExist, My.Application.MsgboxRt(MsgBoxStyle.Critical))

            ElseIf XenonTextBox1.Text.ToLower = "%%Startup".ToLower Or XenonTextBox1.Text.ToLower = "%SystemRoot%_System32_cmd.exe".ToLower _
                Or XenonTextBox1.Text.ToLower = "%SystemRoot%_System32_WindowsPowerShell_v1.0_powershell.exe".ToLower Or
                XenonTextBox1.Text.ToLower = "%SystemRoot%_SysWOW64_WindowsPowerShell_v1.0_powershell.exe".ToLower Then

                MsgBox(My.Lang.Terminal_External_Reversed, My.Application.MsgboxRt(MsgBoxStyle.Critical))

            ElseIf ExternalTerminal.XenonComboBox1.Items.Contains(XenonTextBox1.Text) Then
                MsgBox(My.Lang.Terminal_External_Exists, My.Application.MsgboxRt(MsgBoxStyle.Critical))

            Else
                Registry.CurrentUser.CreateSubKey(String.Format("Console\%SystemDrive%_{0}", XenonTextBox1.Text.Replace("\", "_").Trim(":")(1)), True).Close()

                MsgBox(My.Lang.ExtTer_NewSuccess, My.Application.MsgboxRt(MsgBoxStyle.Information))
                ExternalTerminal.FillTerminals(ExternalTerminal.XenonComboBox1)

            End If

        Catch ex As Exception
            MsgBox(My.Lang.ExtTer_NewError & vbCrLf & vbCrLf & My.Lang.ErrorDetails & ex.Message, My.Application.MsgboxRt(MsgBoxStyle.Critical))
        End Try

    End Sub

    Private Sub XenonButton2_Click(sender As Object, e As EventArgs) Handles XenonButton2.Click
        Me.Close()
    End Sub

    Private Sub NewExtTerminal_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Icon = ExternalTerminal.Icon
    End Sub
End Class