Imports System.IO
Imports Newtonsoft.Json.Linq
Imports WinPaletter.XenonCore

Public Class Lang_JSON_Update
    Private Sub Lang_JSON_Update_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Icon = LangJSON_Manage.Icon
        ApplyDarkMode(Me)
    End Sub

    Private Sub XenonButton8_Click(sender As Object, e As EventArgs) Handles XenonButton8.Click
        If OpenJSONDlg.ShowDialog = DialogResult.OK Then
            XenonTextBox1.Text = OpenJSONDlg.FileName
        End If
    End Sub

    Private Sub XenonButton1_Click(sender As Object, e As EventArgs) Handles XenonButton1.Click
        If OpenJSONDlg.ShowDialog = DialogResult.OK Then
            XenonTextBox2.Text = OpenJSONDlg.FileName
        End If
    End Sub

    Private Sub XenonButton5_Click(sender As Object, e As EventArgs) Handles XenonButton5.Click
        If SaveJSONDlg.ShowDialog = DialogResult.OK Then
            Cursor = Cursors.WaitCursor
            Dim Lang As New Localizer
            Lang.ExportJSON(SaveJSONDlg.FileName)
            Lang.Dispose()
            XenonTextBox2.Text = SaveJSONDlg.FileName
            Cursor = Cursors.Default
        End If
    End Sub

    Private Sub XenonButton3_Click(sender As Object, e As EventArgs) Handles XenonButton3.Click

        If SaveJSONDlg.ShowDialog = DialogResult.OK Then
            Cursor = Cursors.WaitCursor

            Dim _output As String = SaveJSONDlg.FileName

            Dim _Old_File As New StreamReader(XenonTextBox1.Text)
            Dim J_Old As JObject = JObject.Parse(_Old_File.ReadToEnd)
            _Old_File.Close()

            Dim _New_File As New StreamReader(XenonTextBox2.Text)
            Dim J_New As JObject = JObject.Parse(_New_File.ReadToEnd)
            _New_File.Close()

            '# Add the information from the New File
            Dim J_Output As New JObject From {{"Information", J_New("Information")}}

            '# Manage Global Strings
            Dim J_GlobalStrings As New JObject
            Dim x_old As JObject = J_Old("Global Strings")
            Dim x_new As JObject = J_New("Global Strings")
            For Each j In x_new.Properties
                If x_old(j.Name) Is Nothing Then J_GlobalStrings.Add(j.Name, j.Value)       ' Add Missing Strings From New JSON
            Next
            For Each j In x_old.Properties
                J_GlobalStrings.Add(j.Name, j.Value)                                        ' Add Rest of items from Old JSON
            Next
            J_Output.Add("Global Strings", J_GlobalStrings)

            '# Manage Forms
            Dim J_Forms As New JObject
            x_old = J_Old("Forms Strings")
            x_new = J_New("Forms Strings")

            For Each j In x_new.Properties

                If x_old(j.Name) Is Nothing Then
                    J_Forms.Add(j.Name, j.Value)       ' Add Missing Forms From New JSON

                Else

                    Dim c_old As JObject = x_old(j.Name)("Controls")
                    Dim c_new As JObject = x_new(j.Name)("Controls")
                    Dim c As New JObject

                    For Each jj In c_new.Properties
                        If c_old(jj.Name) Is Nothing Then c.Add(jj.Name, jj.Value)       ' Add Missing Controls From New JSON
                    Next
                    For Each jj In c_old.Properties
                        c.Add(jj.Name, jj.Value)                                        ' Add Rest of controls from Old JSON
                    Next

                    x_new(j.Name)("Controls") = c
                End If

            Next

            For Each j In x_old.Properties
                J_Forms.Add(j.Name, j.Value)                                        ' Add Rest of forms from Old JSON
            Next

            J_Output.Add("Forms Strings", J_Forms)


            IO.File.WriteAllText(_output, J_Output.ToString)

            Cursor = Cursors.Default

            MsgBox(My.Lang.Done, MsgBoxStyle.Information)
        End If
    End Sub

    Private Sub XenonButton7_Click(sender As Object, e As EventArgs) Handles XenonButton7.Click
        Close()
    End Sub
End Class