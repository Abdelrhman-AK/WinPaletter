Imports System.Text
Imports WinPaletter.XenonCore

Public Class BugReport
    Private Sub BugReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ApplyDarkMode(Me)
        Dim c As Color = GetAverageColor(PictureBox1.Image)
        Dim c1 As Color = CCB(c, If(GetDarkMode(), -0.35, 0.35))
        Dim c2 As Color = CCB(c, If(GetDarkMode(), -0.75, 0.75))

        Panel1.BackColor = c1
        BackColor = c2

        XenonTextBox1.LineColor = c1

        XenonTextBox1.Font = My.Application.ConsoleFontMedium

        Try : bk.Close() : Catch : End Try
        bk.Show()

        My.Computer.Audio.PlaySystemSound(Media.SystemSounds.Asterisk)

    End Sub

    Public Sub ThrowError(ex As Exception)

        Dim CV As String = "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion"
        Dim sy As String = "." & Microsoft.Win32.Registry.GetValue(CV, "UBR", 0).ToString
        If sy = ".0" Then sy = ""

        Dim sx As String = System.Runtime.InteropServices.RuntimeInformation.OSDescription.Replace("Microsoft Windows ", "")
        sx = sx.Replace("S", "").Trim

        Label2.Text = My.Computer.Info.OSFullName & " - " & sx & sy & " - " & If(Environment.Is64BitOperatingSystem, "64-bit", "32-bit")

        Label3.Text = My.Application.Info.Version.ToString

        XenonTextBox1.Text = "• Error Message:" & vbCrLf & "   " & ex.Message & vbCrLf & vbCrLf &
                             "• Stack Trace:" & vbCrLf & ex.StackTrace '.Replace("   at ", "     - ").Trim

        If Not IO.Directory.Exists(My.Application.appData & "\Reports") Then IO.Directory.CreateDirectory(My.Application.appData & "\Reports")

        IO.File.WriteAllText(String.Format(My.Application.appData & "\Reports\{0}.{1}.{2} {3}-{4}-{5}.txt", Now.Hour, Now.Minute, Now.Second, Now.Day, Now.Month, Now.Year), GetDetails)

        ShowDialog()

        bk.Close()

        If DialogResult = DialogResult.Abort Then My.Application.ExitAfterException = True Else My.Application.ExitAfterException = False

    End Sub

    Private Sub XenonButton2_Click(sender As Object, e As EventArgs) Handles XenonButton2.Click
        DialogResult = DialogResult.Abort
        Me.Close()
    End Sub

    Private Sub XenonButton1_Click(sender As Object, e As EventArgs) Handles XenonButton1.Click
        DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub XenonButton5_Click(sender As Object, e As EventArgs) Handles XenonButton5.Click
        Process.Start(My.Resources.Link_Repository & "issues")
        Try : bk.Close() : Catch : End Try
    End Sub

    Private Sub XenonButton3_Click(sender As Object, e As EventArgs) Handles XenonButton3.Click
        Clipboard.SetText(GetDetails)
    End Sub

    Private Sub XenonButton4_Click(sender As Object, e As EventArgs) Handles XenonButton4.Click
        If SaveFileDialog1.ShowDialog = DialogResult.OK Then
            IO.File.WriteAllText(SaveFileDialog1.FileName, GetDetails)
        End If
    End Sub

    Function GetDetails() As String
        Dim SB As New StringBuilder
        SB.Clear()
        SB.AppendLine(String.Format("• Date of report: {0}", Now.ToLongDateString & " - " & Now.ToLongTimeString))
        SB.AppendLine(String.Format("• OS: {0}", Label2.Text))
        SB.AppendLine(String.Format("• WinPaletter Version: {0}", Label3.Text))
        SB.AppendLine("--------")
        SB.AppendLine()

        SB.AppendLine(XenonTextBox1.Text)
        SB.AppendLine()

        Return SB.ToString
    End Function

    Private Sub XenonButton6_Click(sender As Object, e As EventArgs) Handles XenonButton6.Click

        If IO.Directory.Exists(My.Application.appData & "\Reports") Then
            Process.Start(My.Application.appData & "\Reports")
            Try : bk.Close() : Catch : End Try
        Else
            MsgBox(String.Format("There is no previous saved report in ""{0}""", My.Application.appData & "\Reports"), MsgBoxStyle.Critical + My.Application.MsgboxRt)
        End If

    End Sub
End Class