Public Class EditInfo

    Private Sub EditInfo_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Load_Info(MainForm.CP)
    End Sub

    Private Sub XenonButton1_Click(sender As Object, e As EventArgs) Handles XenonButton1.Click

        If String.IsNullOrWhiteSpace(XenonTextBox1.Text) Then
            MsgBox("You can't leave Palette Name Empty. Please type a name to it.", MsgBoxStyle.Critical, "Null Value")
            Exit Sub
        End If

        If String.IsNullOrWhiteSpace(XenonTextBox2.Text) Then
            MsgBox("You can't leave Palette Version Empty. Please type a version to it in this style (x.x.x.x), replacing (x) by numbers.", MsgBoxStyle.Critical, "Null Value")
            Exit Sub
        End If

        If Not IsNumeric(XenonTextBox2.Text.Replace(".", "")) Then
            MsgBox("Wrong Version Fomrat. Please type the version to it in this style (x.x.x.x), replacing (x) by numbers.", MsgBoxStyle.Critical, "Wrong Format")
            Exit Sub
        End If

        If String.IsNullOrWhiteSpace(XenonTextBox4.Text) Then
            MsgBox("You can't leave Author's Name Empty. Please type Author's name or your name.", MsgBoxStyle.Critical, "Null Value")
            Exit Sub
        End If

        Save_Info(MainForm.CP)
        Me.Close()
    End Sub

    Public Sub Load_Info(ByVal CP As CP)
        XenonTextBox1.Text = CP.PaletteName
        XenonTextBox2.Text = CP.PaletteVersion
        XenonTextBox3.Text = CP.PaletteDescription
        XenonTextBox4.Text = CP.Author
        XenonTextBox5.Text = CP.AuthorSocialMediaLink
        ShowDialog()
    End Sub

    Sub Save_Info(ByVal CP As CP)
        CP.PaletteName = XenonTextBox1.Text
        CP.PaletteVersion = XenonTextBox2.Text
        CP.PaletteDescription = XenonTextBox3.Text
        CP.Author = XenonTextBox4.Text
        CP.AuthorSocialMediaLink = XenonTextBox5.Text
    End Sub

    Private Sub XenonButton2_Click(sender As Object, e As EventArgs) Handles XenonButton2.Click
        Me.Close()
    End Sub
End Class