Imports System.ComponentModel

Public Class EditInfo

    Private Sub EditInfo_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadLanguage
        ApplyStyle(Me)
        Load_Info(My.CP)
        TextBox3.Font = My.Application.ConsoleFontMedium
        TextBox6.Font = My.Application.ConsoleFontMedium

    End Sub

    Protected Overrides Sub OnDragOver(drgevent As DragEventArgs)
        If TypeOf drgevent.Data.GetData("WinPaletter.UI.Controllers.ColorItem") Is UI.Controllers.ColorItem Then
            Focus()
            BringToFront()
        Else
            Exit Sub
        End If

        MyBase.OnDragOver(drgevent)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        With My.Lang
            If String.IsNullOrWhiteSpace(TextBox1.Text) Then
                MsgBox(.EmptyName, MsgBoxStyle.Critical)
                Exit Sub
            End If

            If String.IsNullOrWhiteSpace(TextBox2.Text) Then
                MsgBox(.EmptyVer, MsgBoxStyle.Critical)
                Exit Sub
            End If

            If Not IsNumeric(TextBox2.Text.Replace(".", "")) Then
                MsgBox(.WrongVerFormat, MsgBoxStyle.Critical)
                Exit Sub
            End If

            If String.IsNullOrWhiteSpace(TextBox4.Text) Then
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
        TextBox1.Text = [CP].Info.ThemeName
        TextBox2.Text = [CP].Info.ThemeVersion
        TextBox3.Text = [CP].Info.Description
        TextBox4.Text = [CP].Info.Author
        TextBox5.Text = [CP].Info.AuthorSocialMediaLink
        TextBox6.Text = [CP].Info.License
        CheckBox7.Checked = [CP].Info.ExportResThemePack

        color1.BackColor = [CP].Info.Color1
        color2.BackColor = [CP].Info.Color2
        Trackbar1.Value = [CP].Info.Pattern

        CheckBox1.Checked = [CP].Info.DesignedFor_Win11
        CheckBox2.Checked = [CP].Info.DesignedFor_Win10
        CheckBox3.Checked = [CP].Info.DesignedFor_Win81
        CheckBox4.Checked = [CP].Info.DesignedFor_Win7
        CheckBox5.Checked = [CP].Info.DesignedFor_WinVista
        CheckBox6.Checked = [CP].Info.DesignedFor_WinXP
    End Sub

    Sub Save_Info(ByVal [CP] As CP)
        [CP].Info.ThemeName = String.Concat(TextBox1.Text.Split(IO.Path.GetInvalidFileNameChars())).Trim
        [CP].Info.ThemeVersion = TextBox2.Text
        [CP].Info.Description = TextBox3.Text
        [CP].Info.Author = TextBox4.Text
        [CP].Info.AuthorSocialMediaLink = TextBox5.Text
        [CP].Info.License = TextBox6.Text
        [CP].Info.ExportResThemePack = CheckBox7.Checked

        [CP].Info.Color1 = color1.BackColor
        [CP].Info.Color2 = color2.BackColor
        [CP].Info.Pattern = Trackbar1.Value

        [CP].Info.DesignedFor_Win11 = CheckBox1.Checked
        [CP].Info.DesignedFor_Win10 = CheckBox2.Checked
        [CP].Info.DesignedFor_Win81 = CheckBox3.Checked
        [CP].Info.DesignedFor_Win7 = CheckBox4.Checked
        [CP].Info.DesignedFor_WinVista = CheckBox5.Checked
        [CP].Info.DesignedFor_WinXP = CheckBox6.Checked
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub color1_2_DragDrop(sender As Object, e As DragEventArgs) Handles color1.DragDrop, color2.DragDrop
        StoreItem1.CP.Info.Color1 = color1.BackColor
        StoreItem1.CP.Info.Color2 = color2.BackColor
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
        color1.BackColor = c

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
        color2.BackColor = c

        clist.Clear()
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object) Handles CheckBox1.CheckedChanged
        If Not CheckAllOS() Then sender.Checked = True
        Try
            If StoreItem1.CP IsNot Nothing Then StoreItem1.CP.Info.DesignedFor_Win11 = sender.Checked
            StoreItem1.UpdateBadges()
        Catch
        End Try
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object) Handles CheckBox2.CheckedChanged
        If Not CheckAllOS() Then sender.Checked = True
        Try
            If StoreItem1.CP IsNot Nothing Then StoreItem1.CP.Info.DesignedFor_Win10 = sender.Checked
            StoreItem1.UpdateBadges()
        Catch
        End Try
    End Sub

    Private Sub CheckBox3_CheckedChanged(sender As Object) Handles CheckBox3.CheckedChanged
        If Not CheckAllOS() Then sender.Checked = True
        Try
            If StoreItem1.CP IsNot Nothing Then StoreItem1.CP.Info.DesignedFor_Win81 = sender.Checked
            StoreItem1.UpdateBadges()
        Catch
        End Try
    End Sub

    Private Sub CheckBox4_CheckedChanged(sender As Object) Handles CheckBox4.CheckedChanged
        If Not CheckAllOS() Then sender.Checked = True
        Try
            If StoreItem1.CP IsNot Nothing Then StoreItem1.CP.Info.DesignedFor_Win7 = sender.Checked
            StoreItem1.UpdateBadges()
        Catch
        End Try
    End Sub

    Private Sub CheckBox5_CheckedChanged(sender As Object) Handles CheckBox5.CheckedChanged
        If Not CheckAllOS() Then sender.Checked = True
        Try
            If StoreItem1.CP IsNot Nothing Then StoreItem1.CP.Info.DesignedFor_WinVista = sender.Checked
            StoreItem1.UpdateBadges()
        Catch
        End Try
    End Sub

    Private Sub CheckBox6_CheckedChanged(sender As Object) Handles CheckBox6.CheckedChanged
        If Not CheckAllOS() Then sender.Checked = True
        Try
            If StoreItem1.CP IsNot Nothing Then StoreItem1.CP.Info.DesignedFor_WinXP = sender.Checked
            StoreItem1.UpdateBadges()
        Catch
        End Try
    End Sub

    Function CheckAllOS() As Boolean
        Return CheckBox1.Checked Or CheckBox2.Checked Or CheckBox3.Checked Or CheckBox4.Checked Or CheckBox5.Checked Or CheckBox6.Checked
    End Function

    Private Sub Trackbar1_Scroll(sender As Object) Handles Trackbar1.Scroll
        StoreItem1.UpdatePattern(Trackbar1.Value)
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        StoreItem1.CP.Info.ThemeName = sender.Text
    End Sub

    Private Sub TextBox4_TextChanged(sender As Object, e As EventArgs) Handles TextBox4.TextChanged
        StoreItem1.CP.Info.Author = sender.Text
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        StoreItem1.CP.Info.ThemeVersion = sender.Text
    End Sub

    Private Sub Form_HelpButtonClicked(sender As Object, e As CancelEventArgs) Handles Me.HelpButtonClicked
        Process.Start(My.Resources.Link_Wiki & "/Edit-theme-info")
    End Sub
End Class