Imports WinPaletter.XenonCore
Public Class TerminalInfo
    Public Profile As New ProfilesList

    Private Sub TerminalInfo_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ApplyDarkMode(Me)

    End Sub

    Public Function OpenDialog(Optional IsDefault As Boolean = False) As DialogResult
        TerPrevName.Text = Profile.Name
        TerPrevTabTitle.Text = Profile.TabTitle
        TerPrevTabIcon.Text = Profile.Icon
        TerPrevTabColor.BackColor = Profile.TabColor
        TerPrevAdjustColors.Checked = Profile.adjustIndistinguishableColors
        TerPrevAcrylic.Checked = MainFrm.CP.TerminalPreview.useAcrylicInTabRow

        If IsDefault Then
            TerPrevName.Text = ""
            TerPrevTabTitle.Text = ""
            TerPrevTabIcon.Text = ""
            TerPrevName.Enabled = False
            TerPrevTabTitle.Enabled = False
            TerPrevTabIcon.Enabled = False
        Else
            TerPrevName.Enabled = True
            TerPrevTabTitle.Enabled = True
            TerPrevTabIcon.Enabled = True
        End If

        Return ShowDialog()
    End Function

    Private Sub XenonButton1_Click(sender As Object, e As EventArgs) Handles XenonButton1.Click

        If WindowsTerminal.TerProfiles.Items.Contains(TerPrevName.Text) And Not WindowsTerminal.TerProfiles.SelectedItem.ToString.ToLower = TerPrevName.Text.ToLower Then
            MsgBox("You can't set this name as it is already set to another profile.", MsgBoxStyle.Critical + My.Application.MsgboxRt)
            Exit Sub
        End If

        Profile.Name = TerPrevName.Text
        Profile.TabTitle = TerPrevTabTitle.Text
        Profile.Icon = TerPrevTabIcon.Text
        Profile.TabColor = TerPrevTabColor.BackColor
        Profile.adjustIndistinguishableColors = TerPrevAdjustColors.Checked
        MainFrm.CP.TerminalPreview.useAcrylicInTabRow = TerPrevAcrylic.Checked
        DialogResult = DialogResult.OK
    End Sub

    Private Sub XenonButton2_Click(sender As Object, e As EventArgs) Handles XenonButton2.Click
        DialogResult = DialogResult.Cancel
    End Sub

    Private Sub TerminalInfo_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If DialogResult <> DialogResult.OK Then DialogResult = DialogResult.Cancel
    End Sub

    Private Sub TerPrevTabColor_Click(sender As Object, e As EventArgs) Handles TerPrevTabColor.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            Exit Sub
        End If

        Dim CList As New List(Of Control) From {sender, WindowsTerminal.XenonTerminal1, WindowsTerminal.XenonTerminal2}

        Dim _Conditions As New Conditions

        Dim C As Color = ColorPickerDlg.Pick(CList, _Conditions)

        'ApplyPreview(MainFrm.CP.TerminalPreview)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()
    End Sub
End Class