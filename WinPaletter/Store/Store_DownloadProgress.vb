Imports System.Net

Public Class Store_DownloadProgress
    Public URL As String
    Public File As String
    Public ThemeName As String
    Public ThemeVersion As String

    Dim SW As New Stopwatch
    Dim WithEvents ThemeDownloader As New WebClient

    Private Sub Store_DownloadProgress_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadLanguage
        ApplyStyle(Me)
        Icon = Store.Icon

        Label1.Text = String.Format(My.Lang.Store_DownloadingPackForTheme, ThemeName, ThemeVersion)
        Label2.Text = ""
        Label3.Text = ""
        Label4.Text = ""
        ProgressBar1.Value = 0

        SW.Reset()
        SW.Start()

        ThemeDownloader = New WebClient
        ThemeDownloader.DownloadFileAsync(New Uri(URL), File)
    End Sub

    Private Sub ThemeDownloader_DownloadProgressChanged(sender As Object, e As DownloadProgressChangedEventArgs) Handles ThemeDownloader.DownloadProgressChanged

        Dim Speed As Long = e.BytesReceived / SW.Elapsed.TotalSeconds
        Label3.SetText(Speed.SizeString(True))

        If e.TotalBytesToReceive <> 0 Then
            ProgressBar1.Style = ProgressBarStyle.Blocks
            ProgressBar1.Value = e.ProgressPercentage
            Label2.SetText(String.Format("{0}/{1}", e.BytesReceived.SizeString, e.TotalBytesToReceive.SizeString))
            Dim time As TimeSpan = TimeSpan.FromSeconds((e.TotalBytesToReceive - e.BytesReceived) / Speed)
            Label4.SetText(time.ToString("mm\:ss"))
        Else
            ProgressBar1.Style = ProgressBarStyle.Marquee
            ProgressBar1.Value = 0
            Label2.SetText(e.BytesReceived.SizeString)
            Label4.SetText("")
        End If


    End Sub

    Private Sub ThemeDownloader_DownloadFileCompleted(sender As Object, e As System.ComponentModel.AsyncCompletedEventArgs) Handles ThemeDownloader.DownloadFileCompleted
        SW.Stop()
        SW.Reset()

        If e.Cancelled Or e.Error IsNot Nothing Then
            DialogResult = DialogResult.Abort
        Else
            DialogResult = DialogResult.OK
        End If

        Close()
    End Sub

    Private Sub Store_DownloadProgress_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        If ThemeDownloader.IsBusy Then ThemeDownloader.CancelAsync()
        ThemeDownloader.Dispose()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If ThemeDownloader.IsBusy Then ThemeDownloader.CancelAsync()
        DialogResult = DialogResult.Abort
        Close()
    End Sub
End Class