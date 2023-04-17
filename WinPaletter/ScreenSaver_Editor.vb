Imports System.Threading
Imports WinPaletter.XenonCore

Public Class ScreenSaver_Editor

    Private Proc As Process

    Private Sub ScreenSaver_Editor_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ApplyDarkMode(Me)
        XenonButton12.Image = MainFrm.XenonButton20.Image.Resize(16, 16)
        pnl_preview.CheckForIllegalCrossThreadCalls = False
        MainFrm.MakeItDoubleBuffered(pnl_preview)
        ApplyFromCP(MainFrm.CP)
    End Sub


    Sub ApplyFromCP(CP As CP)
        With CP.ScreenSaver
            ScrSvrEnabled.Checked = .Enabled
            XenonTextBox1.Text = .File
            XenonTrackbar5.Value = .TimeOut
            XenonCheckBox1.Checked = .IsSecure
        End With

    End Sub

    Sub ApplyToCP(CP As CP)
        With CP.ScreenSaver
            .Enabled = ScrSvrEnabled.Checked
            .File = XenonTextBox1.Text
            .TimeOut = XenonTrackbar5.Value
            .IsSecure = XenonCheckBox1.Checked
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

    Private Sub XenonButton8_Click(sender As Object, e As EventArgs) Handles XenonButton8.Click
        ApplyToCP(MainFrm.CP)
        Close()
    End Sub

    Private Sub XenonButton10_Click(sender As Object, e As EventArgs) Handles XenonButton10.Click
        Cursor = Cursors.WaitCursor
        Dim CPx As New CP(CP.CP_Type.Registry)
        ApplyToCP(CPx)
        ApplyToCP(MainFrm.CP)
        CPx.ScreenSaver.Apply()
        CPx.Dispose()
        Cursor = Cursors.Default
    End Sub

    Private Sub XenonButton7_Click(sender As Object, e As EventArgs) Handles XenonButton7.Click
        Close()
    End Sub

    Private Sub ScrSvrEnabled_CheckedChanged(sender As Object, e As EventArgs) Handles ScrSvrEnabled.CheckedChanged
        checker_img.Image = If(sender.Checked, My.Resources.checker_enabled, My.Resources.checker_disabled)
    End Sub

    Private Sub XenonButton5_Click(sender As Object, e As EventArgs) Handles XenonButton5.Click
        If IO.File.Exists(XenonTextBox1.Text) AndAlso IO.Path.GetExtension(XenonTextBox1.Text).ToUpper = ".SCR" Then
            Proc = Process.GetProcessById(Shell("""" & XenonTextBox1.Text & """" & " /p " & pnl_preview.Handle.ToInt32))
        End If
    End Sub


    Private Sub XenonButton1_Click(sender As Object, e As EventArgs) Handles XenonButton1.Click
        If OpenFileDialog2.ShowDialog = DialogResult.OK Then
            XenonTextBox1.Text = OpenFileDialog2.FileName
        End If
    End Sub

    Private Sub XenonTextBox1_TextChanged(sender As Object, e As EventArgs) Handles XenonTextBox1.TextChanged
        If IO.File.Exists(XenonTextBox1.Text) AndAlso IO.Path.GetExtension(XenonTextBox1.Text).ToUpper = ".SCR" Then
            If Proc IsNot Nothing AndAlso Not Proc.HasExited Then Proc.Kill()
            Proc = Process.GetProcessById(Shell("""" & XenonTextBox1.Text & """" & " /p " & pnl_preview.Handle.ToInt32))
        End If
    End Sub

    Private Sub XenonButton6_Click(sender As Object, e As EventArgs) Handles XenonButton6.Click
        If Proc IsNot Nothing AndAlso Not Proc.HasExited Then Proc.Kill()
    End Sub

    Private Sub XenonButton13_Click(sender As Object, e As EventArgs) Handles XenonButton13.Click
        If Proc IsNot Nothing AndAlso Not Proc.HasExited Then Proc.Kill()
        Proc = Process.GetProcessById(Shell("""" & XenonTextBox1.Text & """" & " /s"))
    End Sub

    Private Sub XenonButton14_Click(sender As Object, e As EventArgs) Handles XenonButton14.Click
        If Proc IsNot Nothing AndAlso Not Proc.HasExited Then Proc.Kill()
        Proc = Process.GetProcessById(Shell("""" & XenonTextBox1.Text & """" & " /c", AppWinStyle.NormalFocus))
    End Sub

    Private Sub ScreenSaver_Editor_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If Proc IsNot Nothing AndAlso Not Proc.HasExited Then Proc.Kill()
    End Sub

    Private Sub XenonTrackbar5_Scroll(sender As Object) Handles XenonTrackbar5.Scroll
        XenonButton4.Text = sender.Value
    End Sub

    Private Sub XenonButton4_Click(sender As Object, e As EventArgs) Handles XenonButton4.Click
        Dim response As String = InputBox(My.Lang.InputValue, sender.text, My.Lang.ItMustBeNumerical)
        sender.Text = Math.Max(Math.Min(Val(response), XenonTrackbar5.Maximum), XenonTrackbar5.Minimum) : XenonTrackbar5.Value = Val(sender.Text)
    End Sub
End Class