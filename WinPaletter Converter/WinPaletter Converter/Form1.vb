Public Class Form1
    Dim CP As CP
    Public Format As CP.WP_Format

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If OpenFileDialog1.ShowDialog = DialogResult.OK Then
            TextBox1.Text = OpenFileDialog1.FileName
        End If
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        FetchFile()
    End Sub

    Sub FetchFile()
        If IO.File.Exists(TextBox1.Text) Then
            CP = New CP(TextBox1.Text)

            Select Case Format
                Case CP.WP_Format.JSON
                    Label3.Text = "Theme file is JSON-internally-formatted. When you export this theme, it will be with old formatting system (valid for WinPaletter 1.0.7.6 and less)."
                    CheckBox2.Enabled = True
                    CheckBox1.Enabled = False

                Case CP.WP_Format.WPTH
                    Label3.Text = "Theme file is old-formatted. When you export this theme, it will be JSON-internally-formatted (valid for WinPaletter 1.0.7.7 and higher). It supports contents compression that is useful for uploading more amount of themes to WinPaletter Store with less downloading duration."
                    CheckBox2.Enabled = False
                    CheckBox1.Enabled = True

                Case CP.WP_Format.Error
                    Label3.Text = "Error occurred while phrasing theme file"
                    CheckBox2.Enabled = False
                    CheckBox1.Enabled = False

            End Select
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If Not Format = CP.WP_Format.Error Then
            If SaveFileDialog1.ShowDialog = DialogResult.OK Then

                FetchFile()

                Select Case Format
                    Case CP.WP_Format.JSON
                        CP.Save(CP.WP_Format.WPTH, SaveFileDialog1.FileName, CheckBox1.Checked, CheckBox2.Checked)

                    Case CP.WP_Format.WPTH
                        CP.Save(CP.WP_Format.JSON, SaveFileDialog1.FileName, CheckBox1.Checked, CheckBox2.Checked)

                End Select

                MsgBox("Theme file is converted and exported successfully", MsgBoxStyle.Information)

            End If
        End If
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Icon = Icon.ExtractAssociatedIcon(Reflection.Assembly.GetExecutingAssembly().Location)
    End Sub

End Class
