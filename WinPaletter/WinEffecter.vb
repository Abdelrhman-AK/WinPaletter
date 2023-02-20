Imports WinPaletter.XenonCore

Public Class WinEffecter
    Private Sub WinEffecter_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ApplyDarkMode(Me)
        XenonButton12.Image = MainFrm.XenonButton20.Image.Resize(16, 16)
        ApplyFromCP(MainFrm.CP)
    End Sub

    Sub ApplyFromCP(CP As CP)
        With CP.WindowsEffects
            EffectsEnabled.Checked = .Enabled
            XenonCheckBox1.Checked = .WindowAnimation
            XenonCheckBox2.Checked = .WindowShadow
            XenonCheckBox3.Checked = .WindowUIEffects
            XenonCheckBox6.Checked = .MenuAnimation
            If .MenuFade = CP.MenuAnimType.Fade Then XenonComboBox1.SelectedIndex = 0 Else XenonComboBox1.SelectedIndex = 1
            XenonCheckBox5.Checked = .MenuSelectionFade
            XenonCheckBox8.Checked = .ComboboxAnimation
            XenonCheckBox7.Checked = .ListBoxSmoothScrolling
            XenonCheckBox9.Checked = .TooltipAnimation
            If .TooltipFade = CP.MenuAnimType.Fade Then XenonComboBox2.SelectedIndex = 0 Else XenonComboBox2.SelectedIndex = 1
        End With

    End Sub

    Sub ApplyToCP(CP As CP)
        With CP.WindowsEffects
            .Enabled = EffectsEnabled.Checked
            .WindowAnimation = XenonCheckBox1.Checked
            .WindowShadow = XenonCheckBox2.Checked
            .WindowUIEffects = XenonCheckBox3.Checked
            .MenuAnimation = XenonCheckBox6.Checked
            If XenonComboBox1.SelectedIndex = 0 Then .MenuFade = CP.MenuAnimType.Fade Else .MenuFade = CP.MenuAnimType.Scroll
            .MenuSelectionFade = XenonCheckBox5.Checked
            .ComboboxAnimation = XenonCheckBox8.Checked
            .ListBoxSmoothScrolling = XenonCheckBox7.Checked
            .TooltipAnimation = XenonCheckBox9.Checked
            If XenonComboBox2.SelectedIndex = 0 Then .TooltipAnimation = CP.MenuAnimType.Fade Else .TooltipAnimation = CP.MenuAnimType.Scroll
        End With
    End Sub

    Private Sub XenonButton11_Click(sender As Object, e As EventArgs) Handles XenonButton11.Click
        If OpenFileDialog1.ShowDialog = DialogResult.OK Then
            Dim CPx As New CP(CP.Mode.File, OpenFileDialog1.FileName)
            ApplyFromCP(CPx)
            CPx.Dispose()
        End If
    End Sub

    Private Sub XenonButton9_Click(sender As Object, e As EventArgs) Handles XenonButton9.Click
        Dim CPx As New CP(CP.Mode.Registry)
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
        Dim CPx As New CP(CP.Mode.Registry)
        ApplyToCP(CPx)
        CPx.WindowsEffects.Apply()
        CPx.Dispose()
        Cursor = Cursors.Default
    End Sub

    Private Sub XenonButton8_Click(sender As Object, e As EventArgs) Handles XenonButton8.Click
        ApplyToCP(MainFrm.CP)
        Me.Close()
    End Sub
End Class