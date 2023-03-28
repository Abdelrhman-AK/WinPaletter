Imports WinPaletter.XenonCore
Public Class LogonUIXP
    Private Sub LogonUIXP_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ApplyDarkMode(Me)
        Icon = LogonUI.Icon
        XenonButton12.Image = MainFrm.XenonButton20.Image.Resize(16, 16)
        ApplyFromCP(MainFrm.CP)
    End Sub

    Sub ApplyFromCP(CP As CP)
        With CP.LogonUIXP
            XenonToggle1.Checked = .Enabled
            Select Case .Mode
                Case CP.LogonUIXP_Modes.Default
                    XenonRadioImage1.Checked = True
                Case CP.LogonUIXP_Modes.Win2000
                    XenonRadioImage2.Checked = True
                Case Else
                    XenonRadioImage1.Checked = True
            End Select
            color_pick.BackColor = .BackColor
            XenonCheckBox1.Checked = .ShowMoreOptions
        End With
    End Sub

    Sub ApplyToCP(CP As CP)
        With CP.LogonUIXP
            .Enabled = XenonToggle1.Checked
            If XenonRadioImage1.Checked Then .Mode = CP.LogonUIXP_Modes.Default Else .Mode = CP.LogonUIXP_Modes.Win2000
            .BackColor = color_pick.BackColor
            .ShowMoreOptions = XenonCheckBox1.Checked
        End With
    End Sub

    Private Sub XenonButton11_Click(sender As Object, e As EventArgs) Handles XenonButton11.Click
        If OpenFileDialog1.ShowDialog = DialogResult.OK Then
            Dim CPx As New CP(CP.CP_Type.File, OpenFileDialog1.FileName)
            ApplyFromCP(CPx)
            CPx.Dispose()
        End If
    End Sub

    Private Sub XenonButton9_Click(sender As Object, e As EventArgs) Handles XenonButton9.Click
        Dim CPx As New CP(CP.CP_Type.Registry)
        ApplyFromCP(CPx)
        CPx.Dispose()
    End Sub

    Private Sub XenonButton12_Click(sender As Object, e As EventArgs) Handles XenonButton12.Click
        Dim _Def As CP
        If MainFrm.PreviewConfig = MainFrm.WinVer.W11 Then
            _Def = New CP_Defaults().Default_Windows11
        ElseIf MainFrm.PreviewConfig = MainFrm.WinVer.W10 Then
            _Def = New CP_Defaults().Default_Windows10
        ElseIf MainFrm.PreviewConfig = MainFrm.WinVer.W8 Then
            _Def = New CP_Defaults().Default_Windows8
        ElseIf MainFrm.PreviewConfig = MainFrm.WinVer.W7 Then
            _Def = New CP_Defaults().Default_Windows7
        ElseIf MainFrm.PreviewConfig = MainFrm.WinVer.WVista Then
            _Def = New CP_Defaults().Default_WindowsVista
        ElseIf MainFrm.PreviewConfig = MainFrm.WinVer.WXP Then
            _Def = New CP_Defaults().Default_WindowsXP
        Else
            _Def = New CP_Defaults().Default_Windows11
        End If

        ApplyFromCP(_Def)
        _Def.Dispose()
    End Sub

    Private Sub XenonButton7_Click(sender As Object, e As EventArgs) Handles XenonButton7.Click
        Me.Close()
    End Sub

    Private Sub XenonButton10_Click(sender As Object, e As EventArgs) Handles XenonButton10.Click
        Cursor = Cursors.WaitCursor
        Dim CPx As New CP(CP.CP_Type.Registry)
        ApplyToCP(CPx)
        ApplyToCP(MainFrm.CP)
        CPx.LogonUIXP.Apply()
        CPx.Dispose()
        Cursor = Cursors.Default
    End Sub

    Private Sub XenonButton8_Click(sender As Object, e As EventArgs) Handles XenonButton8.Click
        ApplyToCP(MainFrm.CP)
        Me.Close()
    End Sub

    Private Sub color_pick_Click(sender As Object, e As EventArgs) Handles color_pick.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            Exit Sub
        End If

        Dim CList As New List(Of Control) From {sender}
        Dim C As Color = ColorPickerDlg.Pick(CList)
        sender.BackColor = Color.FromArgb(255, C)
        CList.Clear()

    End Sub

    Private Sub XenonToggle1_CheckedChanged(sender As Object, e As EventArgs) Handles XenonToggle1.CheckedChanged
        checker_img.Image = If(sender.Checked, My.Resources.checker_enabled, My.Resources.checker_disabled)
    End Sub
End Class