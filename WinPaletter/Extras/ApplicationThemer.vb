Imports WinPaletter.XenonCore
Imports WinPaletter.PreviewHelpers

Public Class ApplicationThemer

    Private BackupSettings As XeSettings
    Private _Shown As Boolean = False
    Private CloseAndApply As Boolean = False

    Private Sub ApplicationThemer_Editor_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _Shown = False
        BackupSettings = New XeSettings(XeSettings.Mode.Registry)
        ApplyDarkMode(Me)
        XenonButton12.Image = MainFrm.XenonButton20.Image.Resize(16, 16)
        ApplyFromCP(MainFrm.CP)
        AdjustPreview()
        CloseAndApply = False
    End Sub

    Private Sub ApplicationThemer_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        _Shown = True
    End Sub

    Private Sub ApplicationThemer_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing

        If Not CloseAndApply Then 'Restore previous settings
            With My.Settings
                .Appearance_Custom = BackupSettings.Appearance_Custom
                .Appearance_Custom_Dark = BackupSettings.Appearance_Custom_Dark
                .Appearance_Rounded = BackupSettings.Appearance_Rounded
                .Appearance_Back = BackupSettings.Appearance_Back
                .Appearance_Accent = BackupSettings.Appearance_Accent
                .Save(XeSettings.Mode.Registry)
            End With

            ApplyDarkMode()
        End If

    End Sub

    Sub ApplyFromCP(CP As CP)
        With CP.AppTheme
            AppThemeEnabled.Checked = .Enabled
            appearance_dark.Checked = .DarkMode
            appearance_rounded.Checked = .RoundCorners
            appearance_backcolor.BackColor = .BackColor
            appearance_accent.BackColor = .AccentColor
        End With

    End Sub

    Sub ApplyToCP(CP As CP)
        With CP.AppTheme
            .Enabled = AppThemeEnabled.Checked
            .DarkMode = appearance_dark.Checked
            .RoundCorners = appearance_rounded.Checked
            .BackColor = appearance_backcolor.BackColor
            .AccentColor = appearance_accent.BackColor
        End With
    End Sub

    Sub AdjustPreview()
        With My.Settings
            .Appearance_Custom = True
            .Appearance_Custom_Dark = appearance_dark.Checked
            .Appearance_Rounded = appearance_rounded.Checked
            .Appearance_Back = appearance_backcolor.BackColor
            .Appearance_Accent = appearance_accent.BackColor
        End With

        ApplyDarkMode(Me)

        For Each ctrl As Control In Controls
            ctrl.Invalidate()
        Next

        For Each ctrl As Control In XenonGroupBox12.Controls
            ctrl.Invalidate()
        Next
    End Sub

    Private Sub XenonButton11_Click(sender As Object, e As EventArgs) Handles XenonButton11.Click
        If OpenFileDialog1.ShowDialog = DialogResult.OK Then
            Dim CPx As New CP(CP.CP_Type.File, OpenFileDialog1.FileName)
            ApplyFromCP(CPx)
            AdjustPreview()
            CPx.Dispose()
        End If
    End Sub

    Private Sub XenonButton9_Click(sender As Object, e As EventArgs) Handles XenonButton9.Click
        Dim CPx As New CP(CP.CP_Type.Registry)
        ApplyFromCP(CPx)
        AdjustPreview()
        CPx.Dispose()
    End Sub

    Private Sub XenonButton12_Click(sender As Object, e As EventArgs) Handles XenonButton12.Click
        Using _Def As CP = CP_Defaults.From(My.PreviewStyle)
            ApplyFromCP(_Def)
            AdjustPreview()
        End Using
    End Sub

    Private Sub XenonButton8_Click(sender As Object, e As EventArgs) Handles XenonButton8.Click
        ApplyToCP(MainFrm.CP)
        CloseAndApply = False
        Close()
    End Sub

    Private Sub XenonButton10_Click_1(sender As Object, e As EventArgs) Handles XenonButton10.Click
        Cursor = Cursors.WaitCursor
        Dim CPx As New CP(CP.CP_Type.Registry)
        ApplyToCP(CPx)
        ApplyToCP(MainFrm.CP)
        CPx.AppTheme.Apply()
        CloseAndApply = True
        BackupSettings = New XeSettings(XeSettings.Mode.Registry)
        CPx.Dispose()
        Cursor = Cursors.Default
    End Sub

    Private Sub XenonButton7_Click(sender As Object, e As EventArgs) Handles XenonButton7.Click
        CloseAndApply = False
        Close()
    End Sub

    Private Sub AppThemeEnabled_CheckedChanged(sender As Object, e As EventArgs) Handles AppThemeEnabled.CheckedChanged
        checker_img.Image = If(sender.Checked, My.Resources.checker_enabled, My.Resources.checker_disabled)
        AdjustPreview()
    End Sub

    Private Sub appearance_accent_Click(sender As Object, e As EventArgs) Handles appearance_accent.Click

        With My.Settings
            .Appearance_Custom = BackupSettings.Appearance_Custom
            .Appearance_Custom_Dark = BackupSettings.Appearance_Custom_Dark
            .Appearance_Rounded = BackupSettings.Appearance_Rounded
            .Appearance_Back = BackupSettings.Appearance_Back
            .Appearance_Accent = BackupSettings.Appearance_Accent
        End With

        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            appearance_accent.BackColor = SubMenu.ShowMenu(appearance_accent)
            AdjustPreview()
            Exit Sub
        End If
        Dim clist As New List(Of Control) From {appearance_accent}
        ColorPickerDlg.Pick(clist)
        clist.Clear()

        AdjustPreview()
    End Sub

    Private Sub appearance_backcolor_Click(sender As Object, e As EventArgs) Handles appearance_backcolor.Click
        With My.Settings
            .Appearance_Custom = BackupSettings.Appearance_Custom
            .Appearance_Custom_Dark = BackupSettings.Appearance_Custom_Dark
            .Appearance_Rounded = BackupSettings.Appearance_Rounded
            .Appearance_Back = BackupSettings.Appearance_Back
            .Appearance_Accent = BackupSettings.Appearance_Accent
        End With

        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            appearance_backcolor.BackColor = SubMenu.ShowMenu(appearance_backcolor)
            AdjustPreview()
            Exit Sub
        End If

        Dim clist As New List(Of Control) From {appearance_backcolor, Me}
        ColorPickerDlg.Pick(clist)
        clist.Clear()

        AdjustPreview()
    End Sub

    Private Sub CheckedChanged(sender As Object) Handles appearance_dark.CheckedChanged, appearance_rounded.CheckedChanged
        AdjustPreview()
    End Sub

    Private Sub appearance_list_SelectedIndexChanged(sender As Object, e As EventArgs) Handles appearance_list.SelectedIndexChanged
        If _Shown Then
            Select Case appearance_list.SelectedItem.ToString.ToLower
                Case "Default Dark".ToLower
                    appearance_dark.Checked = True
                    appearance_rounded.Checked = (My.W11 Or My.W7)
                    appearance_accent.BackColor = DefaultAccent
                    appearance_backcolor.BackColor = DefaultBackColorDark

                Case "Default Light".ToLower
                    appearance_dark.Checked = False
                    appearance_rounded.Checked = (My.W11 Or My.W7)
                    appearance_accent.BackColor = DefaultAccent
                    appearance_backcolor.BackColor = DefaultBackColorLight

                Case "AMOLED".ToLower
                    appearance_dark.Checked = True
                    appearance_rounded.Checked = (My.W11 Or My.W7)
                    appearance_accent.BackColor = DefaultAccent
                    appearance_backcolor.BackColor = Color.Black

                Case "Extreme White".ToLower
                    appearance_dark.Checked = False
                    appearance_rounded.Checked = (My.W11 Or My.W7)
                    appearance_accent.BackColor = DefaultAccent
                    appearance_backcolor.BackColor = Color.White

                Case "GitHub Dark".ToLower
                    appearance_dark.Checked = True
                    appearance_rounded.Checked = True
                    appearance_accent.BackColor = Color.FromArgb(19, 35, 58)
                    appearance_backcolor.BackColor = Color.FromArgb(13, 17, 23)

                Case "GitHub Light".ToLower
                    appearance_dark.Checked = False
                    appearance_rounded.Checked = True
                    appearance_accent.BackColor = Color.FromArgb(31, 111, 235)
                    appearance_backcolor.BackColor = Color.FromArgb(246, 248, 250)

                Case "Reddit Dark".ToLower
                    appearance_dark.Checked = True
                    appearance_rounded.Checked = True
                    appearance_accent.BackColor = Color.FromArgb(255, 70, 0)
                    appearance_backcolor.BackColor = Color.FromArgb(9, 9, 9)

                Case "Reddit Light".ToLower
                    appearance_dark.Checked = False
                    appearance_rounded.Checked = True
                    appearance_accent.BackColor = Color.FromArgb(255, 70, 0)
                    appearance_backcolor.BackColor = Color.FromArgb(242, 242, 242)

                Case "Discord Dark".ToLower
                    appearance_dark.Checked = True
                    appearance_rounded.Checked = False
                    appearance_accent.BackColor = Color.FromArgb(65, 71, 78)
                    appearance_backcolor.BackColor = Color.FromArgb(32, 34, 38)

                Case "Discord Light".ToLower
                    appearance_dark.Checked = False
                    appearance_rounded.Checked = False
                    appearance_accent.BackColor = Color.FromArgb(138, 140, 143)
                    appearance_backcolor.BackColor = Color.FromArgb(255, 255, 255)

            End Select

            AdjustPreview()
        End If
    End Sub

    Private Sub ApplicationThemer_BackColorChanged(sender As Object, e As EventArgs) Handles Me.BackColorChanged
        For Each ctrl As Control In Controls
            ctrl.Invalidate()
        Next

        For Each ctrl As Control In XenonGroupBox12.Controls
            ctrl.Invalidate()
        Next
    End Sub

End Class