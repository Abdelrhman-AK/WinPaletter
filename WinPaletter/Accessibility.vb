Imports WinPaletter.XenonCore

Public Class Accessibility
    Private Sub Accessibility_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ApplyDarkMode(Me)
        XenonButton12.Image = MainFrm.XenonButton20.Image.Resize(16, 16)
        ApplyFromCP(MainFrm.CP)
    End Sub

    Private Sub EffectsEnabled_CheckedChanged(sender As Object, e As EventArgs) Handles AccessibilityEnabled.CheckedChanged
        checker_img.Image = If(sender.Checked, My.Resources.checker_enabled, My.Resources.checker_disabled)
    End Sub

    Private Sub Color_pick_Click(sender As Object, e As EventArgs) Handles color_pick.Click, XenonCP1.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            sender.Backcolor = SubMenu.ShowMenu(sender)
            Exit Sub
        End If

        Dim CList As New List(Of Control) From {
          sender
      }


        Dim C As Color = ColorPickerDlg.Pick(CList)

        sender.BackColor = Color.FromArgb(255, C)

        CList.Clear()
    End Sub

    Sub ApplyFromCP(CP As CP)
        With CP.WinAccessibility
            AccessibilityEnabled.Checked = .Enabled
            color_pick.BackColor = .CursorColor
            XenonTextBox1.Text = .CursorSize
            XenonTextBox2.Text = .CursorType
            XenonTextBox3.Text = .TextScaleFactor
            XenonCP1.BackColor = .IndicatorColor
            XenonTextBox4.Text = .IndicatorType
        End With
    End Sub

    Sub ApplyToCP(CP As CP)
        With CP.WinAccessibility
            .Enabled = AccessibilityEnabled.Checked
            .CursorColor = color_pick.BackColor
            .CursorSize = XenonTextBox1.Text
            .CursorType = XenonTextBox2.Text
            .TextScaleFactor = XenonTextBox3.Text
            .IndicatorColor = XenonCP1.BackColor
            .IndicatorType = XenonTextBox4.Text
        End With
    End Sub

    Private Sub XenonButton7_Click(sender As Object, e As EventArgs) Handles XenonButton7.Click
        Me.Close()
    End Sub

    Private Sub XenonButton10_Click(sender As Object, e As EventArgs) Handles XenonButton10.Click
        Cursor = Cursors.WaitCursor
        Dim CPx As New CP(CP.CP_Type.Registry)
        ApplyToCP(CPx)
        CPx.WinAccessibility.Apply()
        CPx.Dispose()
        Cursor = Cursors.Default
    End Sub

    Private Sub XenonButton8_Click(sender As Object, e As EventArgs) Handles XenonButton8.Click
        ApplyToCP(MainFrm.CP)
        Me.Close()
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
End Class