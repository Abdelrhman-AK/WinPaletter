Imports WinPaletter.XenonCore

Public Class EditInfo

    Private Sub EditInfo_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ApplyDarkMode(Me)
        Load_Info(My.CP)
        MainFrm.Visible = False
        XenonTextBox3.Font = My.Application.ConsoleFontMedium
        XenonTextBox6.Font = My.Application.ConsoleFontMedium

    End Sub
    Private Sub EditInfo_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        MainFrm.Visible = True
    End Sub

    Private Sub XenonButton1_Click(sender As Object, e As EventArgs) Handles XenonButton1.Click
        With My.Lang
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


        Save_Info(My.CP)
        MainFrm.themename_lbl.Text = String.Format("{0} ({1})", My.CP.Info.ThemeName, My.CP.Info.ThemeVersion)
        MainFrm.author_lbl.Text = String.Format("{0} {1}", My.Lang.By, My.CP.Info.Author)

        Me.Close()
    End Sub

    Public Sub Load_Info(ByVal [CP] As CP)
        StoreItem1.CP = [CP]
        XenonTextBox1.Text = [CP].Info.ThemeName
        XenonTextBox2.Text = [CP].Info.ThemeVersion
        XenonTextBox3.Text = [CP].Info.Description
        XenonTextBox4.Text = [CP].Info.Author
        XenonTextBox5.Text = [CP].Info.AuthorSocialMediaLink
        XenonTextBox6.Text = [CP].Info.License
        XenonCheckBox7.Checked = [CP].Info.ExportResThemePack

        color1.BackColor = [CP].Info.Color1
        color2.BackColor = [CP].Info.Color2
        XenonTrackbar1.Value = [CP].Info.Pattern

        XenonCheckBox1.Checked = [CP].Info.DesignedFor_Win11
        XenonCheckBox2.Checked = [CP].Info.DesignedFor_Win10
        XenonCheckBox3.Checked = [CP].Info.DesignedFor_Win8
        XenonCheckBox4.Checked = [CP].Info.DesignedFor_Win7
        XenonCheckBox5.Checked = [CP].Info.DesignedFor_WinVista
        XenonCheckBox6.Checked = [CP].Info.DesignedFor_WinXP
    End Sub

    Sub Save_Info(ByVal [CP] As CP)
        [CP].Info.ThemeName = String.Concat(XenonTextBox1.Text.Split(IO.Path.GetInvalidFileNameChars())).Trim
        [CP].Info.ThemeVersion = XenonTextBox2.Text
        [CP].Info.Description = XenonTextBox3.Text
        [CP].Info.Author = XenonTextBox4.Text
        [CP].Info.AuthorSocialMediaLink = XenonTextBox5.Text
        [CP].Info.License = XenonTextBox6.Text
        [CP].Info.ExportResThemePack = XenonCheckBox7.Checked

        [CP].Info.Color1 = color1.BackColor
        [CP].Info.Color2 = color2.BackColor
        [CP].Info.Pattern = XenonTrackbar1.Value

        [CP].Info.DesignedFor_Win11 = XenonCheckBox1.Checked
        [CP].Info.DesignedFor_Win10 = XenonCheckBox2.Checked
        [CP].Info.DesignedFor_Win8 = XenonCheckBox3.Checked
        [CP].Info.DesignedFor_Win7 = XenonCheckBox4.Checked
        [CP].Info.DesignedFor_WinVista = XenonCheckBox5.Checked
        [CP].Info.DesignedFor_WinXP = XenonCheckBox6.Checked
    End Sub

    Private Sub XenonButton2_Click(sender As Object, e As EventArgs) Handles XenonButton2.Click
        Me.Close()
    End Sub

    Private Sub Color1_Click(sender As Object, e As EventArgs) Handles color1.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            sender.BackColor = SubMenu.ShowMenu(sender)
            StoreItem1.CP.Info.Color1 = sender.BackColor
            Exit Sub
        End If

        Dim _conditions As New Conditions With {.BackColor1 = True}
        Dim clist As New List(Of Control) From {color1, StoreItem1}
        Dim c As Color = ColorPickerDlg.Pick(clist, _conditions)

        StoreItem1.CP.Info.Color1 = c
        clist.Clear()
    End Sub

    Private Sub Color2_Click(sender As Object, e As EventArgs) Handles color2.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            sender.BackColor = SubMenu.ShowMenu(sender)
            StoreItem1.CP.Info.Color2 = sender.BackColor
            Exit Sub
        End If

        Dim _conditions As New Conditions With {.BackColor2 = True}
        Dim clist As New List(Of Control) From {color2, StoreItem1}
        Dim c As Color = ColorPickerDlg.Pick(clist, _conditions)

        StoreItem1.CP.Info.Color2 = c
        clist.Clear()
    End Sub

    Private Sub XenonCheckBox1_CheckedChanged(sender As Object) Handles XenonCheckBox1.CheckedChanged
        If Not CheckAllOS() Then sender.Checked = True
        StoreItem1.CP.Info.DesignedFor_Win11 = sender.Checked
        StoreItem1.UpdateBadges()
    End Sub

    Private Sub XenonCheckBox2_CheckedChanged(sender As Object) Handles XenonCheckBox2.CheckedChanged
        If Not CheckAllOS() Then sender.Checked = True
        StoreItem1.CP.Info.DesignedFor_Win10 = sender.Checked
        StoreItem1.UpdateBadges()
    End Sub

    Private Sub XenonCheckBox3_CheckedChanged(sender As Object) Handles XenonCheckBox3.CheckedChanged
        If Not CheckAllOS() Then sender.Checked = True
        StoreItem1.CP.Info.DesignedFor_Win8 = sender.Checked
        StoreItem1.UpdateBadges()
    End Sub

    Private Sub XenonCheckBox4_CheckedChanged(sender As Object) Handles XenonCheckBox4.CheckedChanged
        If Not CheckAllOS() Then sender.Checked = True
        StoreItem1.CP.Info.DesignedFor_Win7 = sender.Checked
        StoreItem1.UpdateBadges()
    End Sub

    Private Sub XenonCheckBox5_CheckedChanged(sender As Object) Handles XenonCheckBox5.CheckedChanged
        If Not CheckAllOS() Then sender.Checked = True
        StoreItem1.CP.Info.DesignedFor_WinVista = sender.Checked
        StoreItem1.UpdateBadges()
    End Sub

    Private Sub XenonCheckBox6_CheckedChanged(sender As Object) Handles XenonCheckBox6.CheckedChanged
        If Not CheckAllOS() Then sender.Checked = True
        StoreItem1.CP.Info.DesignedFor_WinXP = sender.Checked
        StoreItem1.UpdateBadges()
    End Sub

    Function CheckAllOS() As Boolean
        Return XenonCheckBox1.Checked Or XenonCheckBox2.Checked Or XenonCheckBox3.Checked Or XenonCheckBox4.Checked Or XenonCheckBox5.Checked Or XenonCheckBox6.Checked
    End Function

    Private Sub XenonTrackbar1_Scroll(sender As Object) Handles XenonTrackbar1.Scroll
        StoreItem1.UpdatePattern(XenonTrackbar1.Value)
    End Sub

    Private Sub XenonTextBox1_TextChanged(sender As Object, e As EventArgs) Handles XenonTextBox1.TextChanged
        StoreItem1.CP.Info.ThemeName = sender.Text
    End Sub

    Private Sub XenonTextBox4_TextChanged(sender As Object, e As EventArgs) Handles XenonTextBox4.TextChanged
        StoreItem1.CP.Info.Author = sender.Text
    End Sub
End Class