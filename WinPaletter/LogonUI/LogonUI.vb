Imports WinPaletter.XenonCore

Public Class LogonUI
    Private Sub LogonUI_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ApplyDarkMode(Me)

        MainFrm.Visible = False
        Location = New Point(10, (My.Computer.Screen.Bounds.Height - Height) / 2 - 20)
        Load_FromCP(MainFrm.CP)

        PictureBox23.Image = My.Resources.Native10
        PictureBox28.Image = My.Resources.Native10

    End Sub

    Sub Load_FromCP(ByVal ColorPalette As CP)
        LogonUI_Background_Picker.BackColor = ColorPalette.LogonUI_Background
        LogonUI_PersonalColorsAccent_Picker.BackColor = ColorPalette.LogonUI_PersonalColors_Accent
        LogonUI_Acrylic_Toggle.Checked = Not ColorPalette.LogonUI_DisableAcrylicBackgroundOnLogon
        LogonUI_Background_Toggle.Checked = Not ColorPalette.LogonUI_DisableLogonBackgroundImage
        LogonUI_Lockscreen_Toggle.Checked = Not ColorPalette.LogonUI_NoLockScreen
    End Sub

    Private Sub LogonUI_Background_Picker_Click(sender As Object, e As EventArgs) Handles LogonUI_Background_Picker.Click,
        LogonUI_PersonalColorsAccent_Picker.Click

        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            Exit Sub
        End If

        Dim CList As New List(Of Control) From {sender}

        Dim C As Color = ColorPickerDlg.Pick(CList)
        CList.Clear()

    End Sub

    Sub Save(ByVal ColorPalette As CP)
        ColorPalette.LogonUI_Background = LogonUI_Background_Picker.BackColor
        ColorPalette.LogonUI_PersonalColors_Accent = LogonUI_PersonalColorsAccent_Picker.BackColor
        ColorPalette.LogonUI_DisableAcrylicBackgroundOnLogon = Not LogonUI_Acrylic_Toggle.Checked
        ColorPalette.LogonUI_DisableLogonBackgroundImage = Not LogonUI_Background_Toggle.Checked
        ColorPalette.LogonUI_NoLockScreen = Not LogonUI_Lockscreen_Toggle.Checked
    End Sub

    Private Sub XenonButton1_Click(sender As Object, e As EventArgs) Handles XenonButton1.Click
        Save(MainFrm.CP)
        Me.Close()
    End Sub

    Private Sub XenonButton2_Click(sender As Object, e As EventArgs) Handles XenonButton2.Click
        Me.Close()
    End Sub

    Private Sub LogonUI_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        MainFrm.Visible = True
    End Sub
End Class