Imports WinPaletter.XenonCore

Public Class EditInfo

    Private Sub EditInfo_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ApplyDarkMode(Me)

        'Load_Info(MainFrm.CP)
    End Sub

    Private Sub XenonButton1_Click(sender As Object, e As EventArgs) Handles XenonButton1.Click
        With My.Application.LanguageHelper
            If String.IsNullOrWhiteSpace(XenonTextBox1.Text) Then
                MsgBox(.EmptyName, MsgBoxStyle.Critical)
                Exit Sub
            End If

            If String.IsNullOrWhiteSpace(XenonTextBox2.Text) Then
                MsgBox(.EmptyVer, MsgBoxStyle.Critical)
                Exit Sub
            End If

            If Not IsNumeric(XenonTextBox2.Text.Replace(".", "")) Then
                MsgBox(.WrongVerFormat, MsgBoxStyle.Critical)
                Exit Sub
            End If

            If String.IsNullOrWhiteSpace(XenonTextBox4.Text) Then
                MsgBox(.EmptyAuthorName, MsgBoxStyle.Critical)
                Exit Sub
            End If
        End With


        Save_Info(MainFrm.CP)
        Me.Close()
    End Sub

    Public Sub Load_Info(ByVal [CP] As CP)
        XenonTextBox1.Text = [CP].PaletteName
        XenonTextBox2.Text = [CP].PaletteVersion
        XenonTextBox3.Text = [CP].PaletteDescription
        XenonTextBox4.Text = [CP].Author
        XenonTextBox5.Text = [CP].AuthorSocialMediaLink
        ShowDialog()
    End Sub

    Sub Save_Info(ByVal [CP] As CP)
        [CP].PaletteName = XenonTextBox1.Text
        [CP].PaletteVersion = XenonTextBox2.Text
        [CP].PaletteDescription = XenonTextBox3.Text
        [CP].Author = XenonTextBox4.Text
        [CP].AuthorSocialMediaLink = XenonTextBox5.Text
    End Sub

    Private Sub XenonButton2_Click(sender As Object, e As EventArgs) Handles XenonButton2.Click
        Me.Close()
    End Sub
End Class