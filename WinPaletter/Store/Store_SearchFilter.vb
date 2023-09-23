Public Class Store_SearchFilter

    Private Sub Store_SearchFilter_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadLanguage
        ApplyStyle(Me)
        Icon = Store.Icon

        XenonCheckBox1.Checked = My.Settings.Store.Search_ThemeNames
        XenonCheckBox2.Checked = My.Settings.Store.Search_AuthorsNames
        XenonCheckBox3.Checked = My.Settings.Store.Search_Descriptions

    End Sub

    Private Sub XenonButton1_Click(sender As Object, e As EventArgs) Handles XenonButton1.Click
        My.Settings.Store.Search_ThemeNames = XenonCheckBox1.Checked
        My.Settings.Store.Search_AuthorsNames = XenonCheckBox2.Checked
        My.Settings.Store.Search_Descriptions = XenonCheckBox3.Checked
        My.Settings.Store.Save()
        Close()
    End Sub

    Private Sub XenonButton7_Click(sender As Object, e As EventArgs) Handles XenonButton7.Click
        Close()
    End Sub
End Class