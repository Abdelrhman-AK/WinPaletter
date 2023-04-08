﻿Imports WinPaletter.XenonCore

Public Class EditInfo

    Private Sub EditInfo_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ApplyDarkMode(Me)
        Load_Info(MainFrm.CP)
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


        Save_Info(MainFrm.CP)
        MainFrm.themename_lbl.Text = XenonTextBox1.Text
        MainFrm.author_lbl.Text = XenonTextBox4.Text

        Me.Close()
    End Sub

    Public Sub Load_Info(ByVal [CP] As CP)
        StoreItem1.CP = [CP]
        XenonTextBox1.Text = [CP].Info.PaletteName
        XenonTextBox2.Text = [CP].Info.PaletteVersion
        XenonTextBox3.Text = [CP].Info.PaletteDescription
        XenonTextBox4.Text = [CP].Info.Author
        XenonTextBox5.Text = [CP].Info.AuthorSocialMediaLink

        color1.BackColor = [CP].StoreInfo.Color1
        color2.BackColor = [CP].StoreInfo.Color2
        XenonTrackbar1.Value = [CP].StoreInfo.Pattern

        XenonCheckBox1.Checked = [CP].StoreInfo.DesignedFor_Win11
        XenonCheckBox2.Checked = [CP].StoreInfo.DesignedFor_Win10
        XenonCheckBox3.Checked = [CP].StoreInfo.DesignedFor_Win8
        XenonCheckBox4.Checked = [CP].StoreInfo.DesignedFor_Win7
        XenonCheckBox5.Checked = [CP].StoreInfo.DesignedFor_WinVista
        XenonCheckBox6.Checked = [CP].StoreInfo.DesignedFor_WinXP
    End Sub

    Sub Save_Info(ByVal [CP] As CP)
        [CP].Info.PaletteName = XenonTextBox1.Text
        [CP].Info.PaletteVersion = XenonTextBox2.Text
        [CP].Info.PaletteDescription = XenonTextBox3.Text
        [CP].Info.Author = XenonTextBox4.Text
        [CP].Info.AuthorSocialMediaLink = XenonTextBox5.Text

        [CP].StoreInfo.Color1 = color1.BackColor
        [CP].StoreInfo.Color2 = color2.BackColor
        [CP].StoreInfo.Pattern = XenonTrackbar1.Value

        [CP].StoreInfo.DesignedFor_Win11 = XenonCheckBox1.Checked
        [CP].StoreInfo.DesignedFor_Win10 = XenonCheckBox2.Checked
        [CP].StoreInfo.DesignedFor_Win8 = XenonCheckBox3.Checked
        [CP].StoreInfo.DesignedFor_Win7 = XenonCheckBox4.Checked
        [CP].StoreInfo.DesignedFor_WinVista = XenonCheckBox5.Checked
        [CP].StoreInfo.DesignedFor_WinXP = XenonCheckBox6.Checked
    End Sub

    Private Sub XenonButton2_Click(sender As Object, e As EventArgs) Handles XenonButton2.Click
        Me.Close()
    End Sub

    Private Sub color1_Click(sender As Object, e As EventArgs) Handles color1.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            sender.BackColor = SubMenu.ShowMenu(sender)
            StoreItem1.CP.StoreInfo.Color1 = sender.BackColor
            Exit Sub
        End If

        Dim _conditions As New Conditions With {.BackColor1 = True}
        Dim clist As New List(Of Control) From {color1, StoreItem1}
        Dim c As Color = ColorPickerDlg.Pick(clist, _conditions)

        StoreItem1.CP.StoreInfo.Color1 = c
        clist.Clear()
    End Sub

    Private Sub color2_Click(sender As Object, e As EventArgs) Handles color2.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            sender.BackColor = SubMenu.ShowMenu(sender)
            StoreItem1.CP.StoreInfo.Color2 = sender.BackColor
            Exit Sub
        End If

        Dim _conditions As New Conditions With {.BackColor2 = True}
        Dim clist As New List(Of Control) From {color2, StoreItem1}
        Dim c As Color = ColorPickerDlg.Pick(clist, _conditions)

        StoreItem1.CP.StoreInfo.Color2 = c
        clist.Clear()
    End Sub

    Private Sub XenonCheckBox1_CheckedChanged(sender As Object) Handles XenonCheckBox1.CheckedChanged
        If Not CheckAllOS() Then sender.Checked = True
        StoreItem1.CP.StoreInfo.DesignedFor_Win11 = sender.Checked
        StoreItem1.UpdateBadges()
    End Sub

    Private Sub XenonCheckBox2_CheckedChanged(sender As Object) Handles XenonCheckBox2.CheckedChanged
        If Not CheckAllOS() Then sender.Checked = True
        StoreItem1.CP.StoreInfo.DesignedFor_Win10 = sender.Checked
        StoreItem1.UpdateBadges()
    End Sub

    Private Sub XenonCheckBox3_CheckedChanged(sender As Object) Handles XenonCheckBox3.CheckedChanged
        If Not CheckAllOS() Then sender.Checked = True
        StoreItem1.CP.StoreInfo.DesignedFor_Win8 = sender.Checked
        StoreItem1.UpdateBadges()
    End Sub

    Private Sub XenonCheckBox4_CheckedChanged(sender As Object) Handles XenonCheckBox4.CheckedChanged
        If Not CheckAllOS() Then sender.Checked = True
        StoreItem1.CP.StoreInfo.DesignedFor_Win7 = sender.Checked
        StoreItem1.UpdateBadges()
    End Sub

    Private Sub XenonCheckBox5_CheckedChanged(sender As Object) Handles XenonCheckBox5.CheckedChanged
        If Not CheckAllOS() Then sender.Checked = True
        StoreItem1.CP.StoreInfo.DesignedFor_WinVista = sender.Checked
        StoreItem1.UpdateBadges()
    End Sub

    Private Sub XenonCheckBox6_CheckedChanged(sender As Object) Handles XenonCheckBox6.CheckedChanged
        If Not CheckAllOS() Then sender.Checked = True
        StoreItem1.CP.StoreInfo.DesignedFor_WinXP = sender.Checked
        StoreItem1.UpdateBadges()
    End Sub

    Function CheckAllOS() As Boolean
        Return XenonCheckBox1.Checked Or XenonCheckBox2.Checked Or XenonCheckBox3.Checked Or XenonCheckBox4.Checked Or XenonCheckBox5.Checked Or XenonCheckBox6.Checked
    End Function

    Private Sub XenonTrackbar1_Scroll(sender As Object) Handles XenonTrackbar1.Scroll
        StoreItem1.UpdatePattern(XenonTrackbar1.Value)
    End Sub
End Class