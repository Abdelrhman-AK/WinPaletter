Imports System.ComponentModel
Imports System.Net
Imports WinPaletter.XenonCore

Public Class Updates
    Dim WithEvents WebCL, UC As New WebClient
    Dim url As String = Nothing
    Dim ver As String
    Dim StableInt, BetaInt, UpdateChannel As Integer
    Private Sub XenonButton1_Click(sender As Object, e As EventArgs) Handles XenonButton1.Click
        If url Is Nothing Then
            Me.Cursor = Cursors.AppStarting

            My.Application.AnimatorX.HideSync(XenonAlertBox2, True)
            My.Application.AnimatorX.HideSync(XenonButton1, True)
            My.Application.AnimatorX.HideSync(Panel1, True)
            ProgressBar1.Visible = False
            ProgressBar1.Value = 0

            StableInt = 0 : BetaInt = 0 : UpdateChannel = 0

            'Try
            If IsNetAvaliable() Then
                Dim ls As New List(Of String)

                CList_FromStr(ls, WebCL.DownloadString("https://github.com/Abdelrhman-AK/WinPaletter/blob/master/updates?raw=true"))

                For x = 0 To ls.Count - 1
                    If ls(x).Split(" ")(0) = "Stable" Then StableInt = x
                    If ls(x).Split(" ")(0) = "Beta" Then BetaInt = x
                Next

                If My.Application._Settings.UpdateChannel = XeSettings.UpdateChannels.Stable Then UpdateChannel = StableInt
                If My.Application._Settings.UpdateChannel = XeSettings.UpdateChannels.Beta Then UpdateChannel = BetaInt

                ver = ls(UpdateChannel).Split(" ")(1)

                If ver > My.Application.Info.Version.ToString Then
                    url = ls(UpdateChannel).Split(" ")(2)
                    My.Application.AnimatorX.Show(Panel1, True)
                    XenonButton1.Text = "Do Action"
                    XenonAlertBox2.Text = String.Format("Update Avaliable ({0})", ver)
                    XenonAlertBox2.AlertStyle = XenonAlertBox.Style.Warning
                Else
                    url = Nothing
                    XenonButton1.Text = "Check for updates"
                    XenonAlertBox2.Text = String.Format("No Avaliable Updates")
                    XenonAlertBox2.AlertStyle = XenonAlertBox.Style.Success
                End If
            Else
                url = Nothing
                XenonButton1.Text = "Check for updates"
                XenonAlertBox2.Text = String.Format("Network Error")
                XenonAlertBox2.AlertStyle = XenonAlertBox.Style.Warning
            End If
            'Catch
            'url = Nothing
            'XenonButton1.Text = "Check for updates"
            'XenonAlertBox2.Text = String.Format("Fatal Error, there maybe Network issues or Github Servers Issues.")
            'XenonAlertBox2.AlertStyle = XenonAlertBox.Style.Warning
            'End Try

            My.Application.AnimatorX.Show(XenonAlertBox2, True)
            My.Application.AnimatorX.ShowSync(XenonButton1, True)

            Me.Cursor = Cursors.Default
        Else
            ProgressBar1.Visible = False
            ProgressBar1.Value = 0

            If XenonRadioButton1.Checked Then
                ProgressBar1.Visible = True
                Dim OldName As String = Reflection.Assembly.GetExecutingAssembly().Location
                My.Computer.FileSystem.RenameFile(OldName, "oldWinpaletterfile.trash")
                UC.DownloadFileAsync(New Uri(url), OldName)
                Process.Start(OldName)
                Process.GetCurrentProcess.Kill()
            End If

            If XenonRadioButton2.Checked Then

                If SaveFileDialog1.ShowDialog() = DialogResult.OK Then
                    ProgressBar1.Visible = True
                    UC.DownloadFileAsync(New Uri(url), SaveFileDialog1.FileName)
                Else
                    ProgressBar1.Visible = False
                End If

            End If

            If XenonRadioButton3.Checked Then
                Process.Start(url)
            End If

        End If

    End Sub

    Private Sub Updates_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ApplyDarkMode(Me)
        Label3.Text = String.Format("{0} Channel", If(My.Application._Settings.UpdateChannel = XeSettings.UpdateChannels.Stable, "Stable", "Beta"))

        XenonAlertBox2.AlertStyle = XenonAlertBox.Style.Warning
        url = Nothing
        XenonButton1.Text = "Check for updates"
        Label2.Text = My.Application.Info.Version.ToString
    End Sub

    Private Sub XenonButton3_Click(sender As Object, e As EventArgs) Handles XenonButton3.Click
        Me.Close()
    End Sub

    Private Sub UC_DownloadProgressChanged(sender As Object, e As DownloadProgressChangedEventArgs) Handles UC.DownloadProgressChanged
        ProgressBar1.Value = e.ProgressPercentage
    End Sub

    Private Sub UC_DownloadFileCompleted(sender As Object, e As AsyncCompletedEventArgs) Handles UC.DownloadFileCompleted
        ProgressBar1.Visible = False
        ProgressBar1.Value = 0
        If XenonRadioButton2.Checked Then MsgBox("Downloaded Successfully", MsgBoxStyle.Information)
    End Sub
End Class