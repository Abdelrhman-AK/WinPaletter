Imports System.Globalization
Imports WinPaletter.XenonCore

Public Class Lang_Add_Snippet
    Private Sub Lang_Add_Snippet_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadLanguage
        ApplyDarkMode(Me)

        DialogResult = DialogResult.None

        Dim cultures As CultureInfo() = CultureInfo.GetCultures(CultureTypes.AllCultures)
        XenonComboBox1.Items.Clear()
        XenonComboBox2.Items.Clear()

        For Each culture As CultureInfo In cultures
            If Not XenonComboBox1.Items.Contains(culture.NativeName) And Not String.IsNullOrWhiteSpace(culture.NativeName) Then XenonComboBox1.Items.Add(culture.NativeName)
            If Not XenonComboBox2.Items.Contains(culture.Name) And Not String.IsNullOrWhiteSpace(culture.Name) Then XenonComboBox2.Items.Add(culture.Name)
        Next

        XenonComboBox1.SelectedItem = CultureInfo.CurrentCulture.NativeName
        XenonComboBox2.SelectedItem = CultureInfo.CurrentCulture.Name

    End Sub

    Public _Result As String

    Private Sub XenonButton3_Click(sender As Object, e As EventArgs) Handles XenonButton3.Click
        Try
            Dim ci As CultureInfo = CultureInfo.GetCultureInfo(XenonComboBox2.SelectedItem.ToString)
            _Result = ci.TextInfo.IsRightToLeft
        Catch
            _Result = "False"
        End Try

        DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub XenonButton1_Click(sender As Object, e As EventArgs) Handles XenonButton1.Click
        _Result = XenonComboBox1.SelectedItem
        DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub XenonButton2_Click(sender As Object, e As EventArgs) Handles XenonButton2.Click
        _Result = XenonComboBox2.SelectedItem
        DialogResult = DialogResult.OK
        Me.Close()
    End Sub
End Class