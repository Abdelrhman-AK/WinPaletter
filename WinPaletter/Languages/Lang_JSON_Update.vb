Imports System.ComponentModel
Imports System.IO
Imports Newtonsoft.Json.Linq

Public Class Lang_JSON_Update
    Private Sub Lang_JSON_Update_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Icon = Lang_JSON_Manage.Icon
        LoadLanguage
        ApplyStyle(Me)
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        If OpenJSONDlg.ShowDialog = DialogResult.OK Then
            TextBox1.Text = OpenJSONDlg.FileName
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If OpenJSONDlg.ShowDialog = DialogResult.OK Then
            TextBox2.Text = OpenJSONDlg.FileName
        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        If SaveJSONDlg.ShowDialog = DialogResult.OK Then
            Cursor = Cursors.WaitCursor
            Dim Lang As New Localizer
            Lang.ExportJSON(SaveJSONDlg.FileName)
            Lang.Dispose()
            TextBox2.Text = SaveJSONDlg.FileName
            Cursor = Cursors.Default
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        If SaveJSONDlg.ShowDialog = DialogResult.OK Then
            Cursor = Cursors.WaitCursor

            Dim _output As String = SaveJSONDlg.FileName

            Dim _Old_File As New StreamReader(TextBox1.Text)
            Dim J_Old As JObject = JObject.Parse(_Old_File.ReadToEnd)
            _Old_File.Close()

            Dim _New_File As New StreamReader(TextBox2.Text)
            Dim J_New As JObject = JObject.Parse(_New_File.ReadToEnd)
            _New_File.Close()

            'Add information from the New File
            Dim J_Output As New JObject From {{"Information", J_New("Information")}}

            'Manage Global Strings
            Dim J_GlobalStrings As New JObject
            Dim x_old As JObject = J_Old("Global Strings")
            Dim x_new As JObject = J_New("Global Strings")
            For Each j In x_new.Properties
                If x_old(j.Name) Is Nothing Then J_GlobalStrings.Add(j.Name, j.Value)       'Add Missing Strings From New JSON
            Next

            For Each j In x_old.Properties
                If CheckBox1.Checked Then
                    If x_new.ContainsKey(j.Name) Then J_GlobalStrings.Add(j.Name, j.Value)  'Add with exclusion of Old JSON
                Else
                    J_GlobalStrings.Add(j.Name, j.Value)                                    'Add Rest of items from Old JSON
                End If
            Next

            J_Output.Add("Global Strings", J_GlobalStrings)

            'Manage Forms
            Dim J_Forms As New JObject
            x_old = J_Old("Forms Strings")
            x_new = J_New("Forms Strings")

            For Each j In x_new.Properties

                If x_old(j.Name) Is Nothing Then
                    J_Forms.Add(j.Name, j.Value)                                         'Add Missing Forms From New JSON

                Else
                    Dim c_old As JObject = x_old(j.Name)("Controls")
                    Dim c_new As JObject = x_new(j.Name)("Controls")
                    Dim c As New JObject

                    For Each jj In c_new.Properties
                        If c_old(jj.Name) Is Nothing Then c.Add(jj.Name, jj.Value)       'Add Missing Controls From New JSON
                    Next

                    For Each jj In c_old.Properties
                        c.Add(jj.Name, jj.Value)                                         'Add Rest of controls from Old JSON
                    Next

                    x_new(j.Name)("Controls") = c
                    x_new(j.Name)("Text") = x_old(j.Name)("Text")

                End If

            Next

            'Add Modification to the newly created JSON
            For Each j In x_new.Properties
                If Not J_Forms.ContainsKey(j.Name) Then J_Forms.Add(j.Name, j.Value)
            Next

            J_Output.Add("Forms Strings", J_Forms)

            IO.File.WriteAllText(_output, J_Output.ToString)

            Cursor = Cursors.Default

            MsgBox(My.Lang.Done, MsgBoxStyle.Information)
        End If
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Close()
    End Sub

    Private Sub Form_HelpButtonClicked(sender As Object, e As CancelEventArgs) Handles Me.HelpButtonClicked
        Process.Start(My.Resources.Link_Wiki & "/Language-creation-(old-methods)#3-update-your-language-file-when-a-new-winpaletter-is-released")
    End Sub
End Class