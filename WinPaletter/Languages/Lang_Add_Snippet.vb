Imports System.Globalization

Public Class Lang_Add_Snippet
    Private Sub Lang_Add_Snippet_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadLanguage
        ApplyStyle(Me)

        DialogResult = DialogResult.None

        Dim cultures As CultureInfo() = CultureInfo.GetCultures(CultureTypes.AllCultures)
        ComboBox1.Items.Clear()
        ComboBox2.Items.Clear()

        For Each culture As CultureInfo In cultures
            If Not ComboBox1.Items.Contains(culture.NativeName) And Not String.IsNullOrWhiteSpace(culture.NativeName) Then ComboBox1.Items.Add(culture.NativeName)
            If Not ComboBox2.Items.Contains(culture.Name) And Not String.IsNullOrWhiteSpace(culture.Name) Then ComboBox2.Items.Add(culture.Name)
        Next

        ComboBox1.SelectedItem = CultureInfo.CurrentCulture.NativeName
        ComboBox2.SelectedItem = CultureInfo.CurrentCulture.Name

    End Sub

    Public _Result As String

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Try
            Dim ci As CultureInfo = CultureInfo.GetCultureInfo(ComboBox2.SelectedItem.ToString)
            _Result = ci.TextInfo.IsRightToLeft
        Catch
            _Result = "False"
        End Try

        DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        _Result = ComboBox1.SelectedItem
        DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        _Result = ComboBox2.SelectedItem
        DialogResult = DialogResult.OK
        Me.Close()
    End Sub
End Class