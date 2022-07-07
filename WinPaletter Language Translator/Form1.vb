Public Class Form1
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        If OpenFileDialog1.ShowDialog = DialogResult.OK Then
            ListBox1.Items.Clear()

            Dim File As String = OpenFileDialog1.FileName
            For Each X As String In IO.File.ReadAllLines(File)
                Dim S As String = X.Split("=")(1).TrimStart

                If Not String.IsNullOrWhiteSpace(S) And Not IsNumeric(S) And Not S = "" And Not S.StartsWith("Xenon") And Not X.StartsWith("@") And Not X.StartsWith("!") Then
                    ListBox1.Items.Add(X)
                ElseIf X.StartsWith("@") Then
                    ListBox1.Items.Add(X)
                ElseIf X.StartsWith("!") Then
                    Dim XX As String = X.Split("=")(0)
                    If XX = "!Name" Then TextBox3.Text = S
                    If XX = "!TrVer" Then TextBox7.Text = S
                    If XX = "!Lang" Then TextBox4.Text = S
                    If XX = "!LangCode" Then TextBox8.Text = S
                    If XX = "!AppVer" Then TextBox5.Text = S
                    If XX = "!RightToLeft" Then CheckBox1.Checked = S
                End If

            Next


        End If

    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        If Not Modifying Then

            Select Case ListBox1.SelectedItem.ToString.StartsWith("@")
                Case True
                    Label5.Text = "Embedded in code"
                    Label4.Text = "Embedded in code"
                    Dim X As String = ListBox1.SelectedItem
                    Dim X1 As String = X.Split("=")(1)
                    TextBox1.Text = X1.TrimStart
                    TextBox2.Text = TextBox1.Text

                Case False
                    Dim X As String = ListBox1.SelectedItem
                    Dim X1 As String = X.Split("=")(0)

                    Dim FormName As String
                    Dim ControlName As String

                    If X1.Contains("\") Then
                        FormName = X1.Split("\")(0)
                        ControlName = X1.Split("\")(1)
                        Label5.Text = FormName
                        Label4.Text = ControlName
                    Else
                        FormName = X1
                        ControlName = X1
                        Label5.Text = FormName
                        Label4.Text = ControlName
                    End If


                    TextBox1.Text = X.Remove(0, X.Split("=")(0).Count + 2)
                    TextBox2.Text = TextBox1.Text
            End Select
        End If
    End Sub

    Dim Modifying As Boolean = False

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Modifying = True

        TextBox1.Text = TextBox2.Text
        Dim i As Integer = ListBox1.SelectedIndex

        Select Case ListBox1.SelectedItem.ToString.StartsWith("@")
            Case True
                Dim tmp As String = ListBox1.SelectedItem.ToString.Split("=")(0)
                ListBox1.Items.RemoveAt(i)
                ListBox1.Items.Insert(i, tmp & "= " & TextBox1.Text)
            Case False
                ListBox1.Items.RemoveAt(i)

                If Label5.Text = Label4.Text Then
                    ListBox1.Items.Insert(i, Label5.Text & "= " & TextBox1.Text)
                Else
                    ListBox1.Items.Insert(i, Label5.Text & "\" & Label4.Text & "= " & TextBox1.Text)
                End If
        End Select


        ListBox1.SelectedIndex = i
        Modifying = False

        ListBox2.Items.Add(TextBox2.Text)
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If sender.checked Then
            TextBox2.RightToLeft = RightToLeft.Yes
        Else
            TextBox2.RightToLeft = RightToLeft.No
        End If
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Button1.PerformClick()
    End Sub

    Private Sub ListBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox2.DoubleClick
        TextBox2.Text = ListBox2.SelectedItem
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If SaveFileDialog1.ShowDialog = DialogResult.OK Then
            Dim s As String = ""
            Dim max As Integer = ListBox1.Items.Count - 1
            s &= "!Name= " & TextBox3.Text & vbCrLf
            s &= "!TrVer= " & TextBox7.Text & vbCrLf
            s &= "!Lang= " & TextBox4.Text & vbCrLf
            s &= "!LangCode= " & TextBox8.Text & vbCrLf
            s &= "!AppVer= " & TextBox5.Text & vbCrLf
            s &= "!RightToLeft= " & CheckBox1.Checked & vbCrLf

            For i As Integer = 0 To max
                s &= ListBox1.Items.Item(i) & If(i < max, vbCrLf, Nothing)
            Next

            IO.File.WriteAllText(SaveFileDialog1.FileName, s)
        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs)

    End Sub
End Class
