Public Class Lang_ReplaceText
    Private Sub Lang_ReplaceText_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Icon = Lang_JSON_Manage.Icon
        LoadLanguage
        ApplyStyle(Me)

    End Sub

    Public Function Replace(data As DataGridView, Column As Integer, FindWhat As String) As String
        Using dlg As New Lang_ReplaceText
            dlg.TextBox3.Text = FindWhat

            If dlg.ShowDialog() = DialogResult.OK Then

                Dim SearchText As String = dlg.TextBox3.Text
                Dim ReplaceBy As String = dlg.TextBox4.Text

                If String.IsNullOrWhiteSpace(SearchText) Then Return ReplaceBy

                For r = 0 To data.Rows.Count - 1
                    With data.Item(Column, r)
                        .Value = .Value.ToString.Replace(SearchText, ReplaceBy, Not dlg.CheckBox1.Checked, dlg.CheckBox2.Checked)
                    End With
                Next

                Return ReplaceBy

            Else
                Return dlg.TextBox4.Text
            End If
        End Using
    End Function

    Public Function Replace(Form As Form, FindWhat As String) As String
        Using dlg As New Lang_ReplaceText
            dlg.TextBox3.Text = FindWhat

            If dlg.ShowDialog() = DialogResult.OK Then

                Dim SearchText As String = dlg.TextBox3.Text
                Dim ReplaceBy As String = dlg.TextBox4.Text

                If String.IsNullOrWhiteSpace(SearchText) Then Return ReplaceBy

                For Each ctrl In Form.GetAllControls
                    If ctrl.Text IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(ctrl.Text) Then
                        ctrl.Text = ctrl.Text.Replace(SearchText, ReplaceBy, Not dlg.CheckBox1.Checked, dlg.CheckBox2.Checked)

                    ElseIf ctrl.Tag IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(ctrl.Tag.ToString) Then
                        ctrl.Tag = ctrl.Tag.ToString.Replace(SearchText, ReplaceBy, Not dlg.CheckBox1.Checked, dlg.CheckBox2.Checked)
                    End If
                Next

                Return ReplaceBy

            Else
                Return dlg.TextBox4.Text
            End If

        End Using
    End Function



    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        DialogResult = DialogResult.OK
        Close()
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        DialogResult = DialogResult.Cancel
        Close()
    End Sub
End Class