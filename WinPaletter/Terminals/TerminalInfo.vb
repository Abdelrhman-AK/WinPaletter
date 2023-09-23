Public Class TerminalInfo
    Public Profile As New ProfilesList

    Private Sub TerminalInfo_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadLanguage
        ApplyStyle(Me)
        Icon = WindowsTerminal.Icon
    End Sub

    Public Function OpenDialog(Optional IsDefault As Boolean = False) As DialogResult
        TerName.Text = Profile.Name
        TerTabTitle.Text = Profile.TabTitle
        TerTabIcon.Text = Profile.Icon
        TerTabColor.BackColor = Profile.TabColor
        TerAcrylic.Checked = My.CP.TerminalPreview.useAcrylicInTabRow

        If IsDefault Then
            TerName.Text = ""
            TerTabTitle.Text = ""
            TerTabIcon.Text = ""
            TerName.Enabled = False
            TerTabTitle.Enabled = False
            TerTabIcon.Enabled = False
        Else
            TerName.Enabled = True
            TerTabTitle.Enabled = True
            TerTabIcon.Enabled = True
        End If

        Return ShowDialog()
    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        If WindowsTerminal.TerProfiles.Items.Contains(TerName.Text) And Not WindowsTerminal.TerProfiles.SelectedItem.ToString.ToLower = TerName.Text.ToLower Then
            MsgBox(My.Lang.Terminal_alreadyset, MsgBoxStyle.Critical)
            Exit Sub
        End If

        Profile.Name = TerName.Text
        Profile.TabTitle = TerTabTitle.Text
        Profile.Icon = TerTabIcon.Text
        Profile.TabColor = TerTabColor.BackColor
        My.CP.TerminalPreview.useAcrylicInTabRow = TerAcrylic.Checked
        DialogResult = DialogResult.OK
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        DialogResult = DialogResult.Cancel
    End Sub

    Private Sub TerminalInfo_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If DialogResult <> DialogResult.OK Then DialogResult = DialogResult.Cancel
    End Sub

    Private Sub TerTabColor_Click(sender As Object, e As EventArgs) Handles TerTabColor.Click, TerTabColor.DragDrop

        If TypeOf e Is DragEventArgs Then
            With If(WindowsTerminal.TerProfiles.SelectedIndex = 0, WindowsTerminal._Terminal.DefaultProf, WindowsTerminal._Terminal.Profiles(WindowsTerminal.TerProfiles.SelectedIndex - 1))
                .TabColor = TerTabColor.BackColor
            End With

            WindowsTerminal.ApplyPreview(WindowsTerminal._Terminal)
            Exit Sub
        End If

        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            Dim cx As Color = SubMenu.ShowMenu(sender, True)

            With If(WindowsTerminal.TerProfiles.SelectedIndex = 0, WindowsTerminal._Terminal.DefaultProf, WindowsTerminal._Terminal.Profiles(WindowsTerminal.TerProfiles.SelectedIndex - 1))
                .TabColor = cx
            End With

            WindowsTerminal.ApplyPreview(WindowsTerminal._Terminal)

            Exit Sub
        End If

        Dim CList As New List(Of Control) From {sender, WindowsTerminal.Terminal1}

        Dim _Conditions As New Conditions With {.Terminal_TabColor = True}

        Dim C As Color = ColorPickerDlg.Pick(CList, _Conditions)

        WindowsTerminal.ApplyPreview(WindowsTerminal._Terminal)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()
    End Sub


End Class