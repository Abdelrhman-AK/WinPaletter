Public Class Store_SearchFilter

    Private Sub Store_SearchFilter_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadLanguage
        ApplyStyle(Me)
        Icon = Store.Icon

        CheckBox1.Checked = My.Settings.Store.Search_ThemeNames
        CheckBox2.Checked = My.Settings.Store.Search_AuthorsNames
        CheckBox3.Checked = My.Settings.Store.Search_Descriptions

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        My.Settings.Store.Search_ThemeNames = CheckBox1.Checked
        My.Settings.Store.Search_AuthorsNames = CheckBox2.Checked
        My.Settings.Store.Search_Descriptions = CheckBox3.Checked
        My.Settings.Store.Save()
        Close()
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Close()
    End Sub
End Class