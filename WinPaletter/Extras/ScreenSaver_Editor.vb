Imports System.ComponentModel

Public Class ScreenSaver_Editor

    Private Proc As Process

    Private Sub ScreenSaver_Editor_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadLanguage
        ApplyStyle(Me)
        Button12.Image = MainFrm.Button20.Image.Resize(16, 16)
        pnl_preview.DoubleBuffer
        ApplyFromCP(My.CP)
    End Sub


    Sub ApplyFromCP(CP As CP)
        With CP.ScreenSaver
            ScrSvrEnabled.Checked = .Enabled
            TextBox1.Text = .File
            Trackbar5.Value = .TimeOut
            CheckBox1.Checked = .IsSecure
        End With

    End Sub

    Sub ApplyToCP(CP As CP)
        With CP.ScreenSaver
            .Enabled = ScrSvrEnabled.Checked
            .File = TextBox1.Text
            .TimeOut = Trackbar5.Value
            .IsSecure = CheckBox1.Checked
        End With
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        If OpenFileDialog1.ShowDialog = DialogResult.OK Then
            Dim CPx As New CP(CP.CP_Type.File, OpenFileDialog1.FileName)
            ApplyFromCP(CPx)
            CPx.Dispose()
        End If
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Dim CPx As New CP(CP.CP_Type.Registry)
        ApplyFromCP(CPx)
        CPx.Dispose()
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        Using _Def As CP = CP_Defaults.From(My.PreviewStyle)
            ApplyFromCP(_Def)
        End Using
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        ApplyToCP(My.CP)
        Close()
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        Cursor = Cursors.WaitCursor
        Dim CPx As New CP(CP.CP_Type.Registry)
        ApplyToCP(CPx)
        ApplyToCP(My.CP)
        CPx.ScreenSaver.Apply()
        CPx.Dispose()
        Cursor = Cursors.Default
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Close()
    End Sub

    Private Sub ScrSvrEnabled_CheckedChanged(sender As Object, e As EventArgs) Handles ScrSvrEnabled.CheckedChanged
        checker_img.Image = If(sender.Checked, My.Resources.checker_enabled, My.Resources.checker_disabled)
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        If IO.File.Exists(TextBox1.Text) AndAlso IO.Path.GetExtension(TextBox1.Text).ToUpper = ".SCR" Then
            If Proc IsNot Nothing AndAlso Not Proc.HasExited Then Proc.Kill()
            Proc = Process.GetProcessById(Shell("""" & TextBox1.Text & """" & " /p " & pnl_preview.Handle.ToInt32))
        End If
    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If OpenFileDialog2.ShowDialog = DialogResult.OK Then
            TextBox1.Text = OpenFileDialog2.FileName
        End If
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        If IO.File.Exists(TextBox1.Text) AndAlso IO.Path.GetExtension(TextBox1.Text).ToUpper = ".SCR" Then
            If Proc IsNot Nothing AndAlso Not Proc.HasExited Then Proc.Kill()
            Proc = Process.GetProcessById(Shell("""" & TextBox1.Text & """" & " /p " & pnl_preview.Handle.ToInt32))
        End If
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        If Proc IsNot Nothing AndAlso Not Proc.HasExited Then Proc.Kill()
    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        If IO.File.Exists(TextBox1.Text) AndAlso IO.Path.GetExtension(TextBox1.Text).ToUpper = ".SCR" Then Shell("""" & TextBox1.Text & """" & " /s")
    End Sub

    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
        If Proc IsNot Nothing AndAlso Not Proc.HasExited Then Proc.Kill()
        If IO.File.Exists(TextBox1.Text) AndAlso IO.Path.GetExtension(TextBox1.Text).ToUpper = ".SCR" Then
            Proc = Process.GetProcessById(Shell("""" & TextBox1.Text & """" & " /c", AppWinStyle.NormalFocus))
            Proc.WaitForExit()
            Proc = Process.GetProcessById(Shell("""" & TextBox1.Text & """" & " /p " & pnl_preview.Handle.ToInt32))
        End If
    End Sub

    Private Sub ScreenSaver_Editor_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If Proc IsNot Nothing AndAlso Not Proc.HasExited Then Proc.Kill()
    End Sub

    Private Sub Trackbar5_Scroll(sender As Object) Handles Trackbar5.Scroll
        Button4.Text = sender.Value
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim response As String = InputBox(My.Lang.InputValue, sender.text, My.Lang.ItMustBeNumerical)
        sender.Text = Math.Max(Math.Min(Val(response), Trackbar5.Maximum), Trackbar5.Minimum) : Trackbar5.Value = Val(sender.Text)
    End Sub

    Private Sub Button259_Click(sender As Object, e As EventArgs) Handles Button259.Click

        If OpenThemeDialog.ShowDialog = DialogResult.OK Then
            Using _Def As CP = CP_Defaults.From(My.PreviewStyle)
                GetFromClassicThemeFile(OpenThemeDialog.FileName, _Def.ScreenSaver)
            End Using
        End If
    End Sub

    Sub GetFromClassicThemeFile(File As String, _DefaultScrSvr As CP.Structures.ScreenSaver)
        Using _ini As New INI(File)
            TextBox1.Text = _ini.IniReadValue("boot", "SCRNSAVE.EXE", _DefaultScrSvr.File).PhrasePath
        End Using
    End Sub

    Private Sub Form_HelpButtonClicked(sender As Object, e As CancelEventArgs) Handles Me.HelpButtonClicked
        Process.Start(My.Resources.Link_Wiki & "/Edit-Screen-Saver")
    End Sub
End Class