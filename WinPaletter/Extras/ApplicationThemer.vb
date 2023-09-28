Imports System.ComponentModel

Public Class ApplicationThemer

    Private BackupSettings As WPSettings
    Private _Shown As Boolean = False
    Private CloseAndApply As Boolean = False
    Public FixLanguageDarkModeBug As Boolean = True

    Private Sub ApplicationThemer_Editor_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _Shown = False
        BackupSettings = New WPSettings(WPSettings.Mode.Registry)
        LoadLanguage
        ApplyStyle(Me)
        Button12.Image = MainFrm.Button20.Image.Resize(16, 16)
        ApplyFromCP(My.CP)
        AdjustPreview()
        CloseAndApply = False
    End Sub

    Private Sub ApplicationThemer_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        _Shown = True
    End Sub

    Private Sub ApplicationThemer_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing

        If Not CloseAndApply Then 'Restore previous settings
            With My.Settings.Appearance
                .CustomColors = BackupSettings.Appearance.CustomColors
                .CustomTheme = BackupSettings.Appearance.CustomTheme
                .RoundedCorners = BackupSettings.Appearance.RoundedCorners
                .BackColor = BackupSettings.Appearance.BackColor
                .AccentColor = BackupSettings.Appearance.AccentColor
                .Save()
            End With

            FetchDarkMode()
            ApplyStyle()
        End If

        FixLanguageDarkModeBug = True
    End Sub

    Sub ApplyFromCP(CP As CP)
        With CP.AppTheme
            AppThemeEnabled.Checked = .Enabled
            appearance_dark.Checked = .DarkMode
            RoundedCorners.Checked = .RoundCorners
            BackColorPick.BackColor = .BackColor
            AccentColor.BackColor = .AccentColor
        End With

    End Sub

    Sub ApplyToCP(CP As CP)
        With CP.AppTheme
            .Enabled = AppThemeEnabled.Checked
            .DarkMode = appearance_dark.Checked
            .RoundCorners = RoundedCorners.Checked
            .BackColor = BackColorPick.BackColor
            .AccentColor = AccentColor.BackColor
        End With
    End Sub

    Sub AdjustPreview()
        If FixLanguageDarkModeBug Then Exit Sub

        With My.Settings.Appearance
            .CustomColors = True
            .CustomTheme = appearance_dark.Checked
            .RoundedCorners = RoundedCorners.Checked
            .BackColor = BackColorPick.BackColor
            .AccentColor = AccentColor.BackColor
        End With

        ApplyStyle(Me)

        For Each ctrl As Control In Controls
            ctrl.Invalidate()
        Next

        For Each ctrl As Control In GroupBox12.Controls
            ctrl.Invalidate()
        Next
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        If OpenFileDialog1.ShowDialog = DialogResult.OK Then
            Dim CPx As New CP(CP.CP_Type.File, OpenFileDialog1.FileName)
            ApplyFromCP(CPx)
            AdjustPreview()
            CPx.Dispose()
        End If
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Dim CPx As New CP(CP.CP_Type.Registry)
        ApplyFromCP(CPx)
        AdjustPreview()
        CPx.Dispose()
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        Using _Def As CP = CP_Defaults.From(My.PreviewStyle)
            ApplyFromCP(_Def)
            AdjustPreview()
        End Using
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        ApplyToCP(My.CP)
        CloseAndApply = False
        Close()
    End Sub

    Private Sub Button10_Click_1(sender As Object, e As EventArgs) Handles Button10.Click
        Cursor = Cursors.WaitCursor
        Dim CPx As New CP(CP.CP_Type.Registry)
        ApplyToCP(CPx)
        ApplyToCP(My.CP)
        CPx.AppTheme.Apply()
        CloseAndApply = True
        BackupSettings = New WPSettings(WPSettings.Mode.Registry)
        CPx.Dispose()
        Cursor = Cursors.Default
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        CloseAndApply = False
        Close()
    End Sub

    Private Sub AppThemeEnabled_CheckedChanged(sender As Object, e As EventArgs) Handles AppThemeEnabled.CheckedChanged
        checker_img.Image = If(sender.Checked, My.Resources.checker_enabled, My.Resources.checker_disabled)
        AdjustPreview()
    End Sub

    Private Sub AccentColor_BackColorPick_DragDrop(sender As Object, e As DragEventArgs) Handles AccentColor.DragDrop, BackColorPick.DragDrop
        AdjustPreview()
    End Sub

    Private Sub AccentColor_Click(sender As Object, e As EventArgs) Handles AccentColor.Click

        If TypeOf e Is DragEventArgs Then Exit Sub

        With My.Settings.Appearance
            .CustomColors = BackupSettings.Appearance.CustomColors
            .CustomTheme = BackupSettings.Appearance.CustomTheme
            .RoundedCorners = BackupSettings.Appearance.RoundedCorners
            .BackColor = BackupSettings.Appearance.BackColor
            .AccentColor = BackupSettings.Appearance.AccentColor
        End With

        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            AccentColor.BackColor = SubMenu.ShowMenu(AccentColor)
            AdjustPreview()
            Exit Sub
        End If
        Dim clist As New List(Of Control) From {AccentColor}
        ColorPickerDlg.Pick(clist)
        clist.Clear()

        AdjustPreview()
    End Sub

    Private Sub BackColorPick_Click(sender As Object, e As EventArgs) Handles BackColorPick.Click
        With My.Settings.Appearance
            .CustomColors = BackupSettings.Appearance.CustomColors
            .CustomTheme = BackupSettings.Appearance.CustomTheme
            .RoundedCorners = BackupSettings.Appearance.RoundedCorners
            .BackColor = BackupSettings.Appearance.BackColor
            .AccentColor = BackupSettings.Appearance.AccentColor
        End With

        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            BackColorPick.BackColor = SubMenu.ShowMenu(BackColorPick)
            AdjustPreview()
            Exit Sub
        End If

        Dim clist As New List(Of Control) From {BackColorPick, Me}
        ColorPickerDlg.Pick(clist)
        clist.Clear()

        AdjustPreview()
    End Sub

    Private Sub CheckedChanged(sender As Object) Handles appearance_dark.CheckedChanged, RoundedCorners.CheckedChanged
        AdjustPreview()
    End Sub

    Private Sub Appearance_list_SelectedIndexChanged(sender As Object, e As EventArgs) Handles appearance_list.SelectedIndexChanged
        If _Shown Then
            Select Case appearance_list.SelectedItem.ToString.ToLower
                Case "Default Dark".ToLower
                    appearance_dark.Checked = True
                    RoundedCorners.Checked = (My.W11 Or My.W7)
                    AccentColor.BackColor = My.DefaultAccent
                    BackColorPick.BackColor = My.DefaultBackColorDark

                Case "Default Light".ToLower
                    appearance_dark.Checked = False
                    RoundedCorners.Checked = (My.W11 Or My.W7)
                    AccentColor.BackColor = My.DefaultAccent
                    BackColorPick.BackColor = My.DefaultBackColorLight

                Case "AMOLED".ToLower
                    appearance_dark.Checked = True
                    RoundedCorners.Checked = (My.W11 Or My.W7)
                    AccentColor.BackColor = My.DefaultAccent
                    BackColorPick.BackColor = Color.Black

                Case "Extreme White".ToLower
                    appearance_dark.Checked = False
                    RoundedCorners.Checked = (My.W11 Or My.W7)
                    AccentColor.BackColor = My.DefaultAccent
                    BackColorPick.BackColor = Color.White

                Case "GitHub Dark".ToLower
                    appearance_dark.Checked = True
                    RoundedCorners.Checked = True
                    AccentColor.BackColor = Color.FromArgb(19, 35, 58)
                    BackColorPick.BackColor = Color.FromArgb(13, 17, 23)

                Case "GitHub Light".ToLower
                    appearance_dark.Checked = False
                    RoundedCorners.Checked = True
                    AccentColor.BackColor = Color.FromArgb(31, 111, 235)
                    BackColorPick.BackColor = Color.FromArgb(246, 248, 250)

                Case "Reddit Dark".ToLower
                    appearance_dark.Checked = True
                    RoundedCorners.Checked = True
                    AccentColor.BackColor = Color.FromArgb(255, 70, 0)
                    BackColorPick.BackColor = Color.FromArgb(9, 9, 9)

                Case "Reddit Light".ToLower
                    appearance_dark.Checked = False
                    RoundedCorners.Checked = True
                    AccentColor.BackColor = Color.FromArgb(255, 70, 0)
                    BackColorPick.BackColor = Color.FromArgb(242, 242, 242)

                Case "Discord Dark".ToLower
                    appearance_dark.Checked = True
                    RoundedCorners.Checked = False
                    AccentColor.BackColor = Color.FromArgb(65, 71, 78)
                    BackColorPick.BackColor = Color.FromArgb(32, 34, 38)

                Case "Discord Light".ToLower
                    appearance_dark.Checked = False
                    RoundedCorners.Checked = False
                    AccentColor.BackColor = Color.FromArgb(138, 140, 143)
                    BackColorPick.BackColor = Color.FromArgb(255, 255, 255)

            End Select

            AdjustPreview()
        End If
    End Sub

    Private Sub ApplicationThemer_BackColorChanged(sender As Object, e As EventArgs) Handles Me.BackColorChanged
        For Each ctrl As Control In Controls
            ctrl.Invalidate()
        Next

        For Each ctrl As Control In GroupBox12.Controls
            ctrl.Invalidate()
        Next
    End Sub

    Private Sub Form_HelpButtonClicked(sender As Object, e As CancelEventArgs) Handles Me.HelpButtonClicked
        Process.Start(My.Resources.Link_Wiki & "/Edit-WinPaletter-application-theme")
    End Sub

End Class