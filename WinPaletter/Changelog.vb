Imports System.Net
Imports System.Text
Imports WinPaletter.XenonCore

Public Class Changelog

    Dim WithEvents W As WebClient
    Dim RTF As String

    Private Sub Changelog_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ApplyDarkMode(Me)
        ProgressBar1.Visible = False
        LoadChangelog()
    End Sub

    Sub LoadChangelog()
        'https://github.com/Abdelrhman-AK/WinPaletter/blob/master/Changelog?raw=true
        ProgressBar1.Visible = True
        ProgressBar1.Value = 0
        W = New WebClient
        W.DownloadDataAsync(New Uri("https://github.com/Abdelrhman-AK/WinPaletter/blob/master/Changelog.rtf?raw=true"))
    End Sub

    Private Sub W_DownloadDataCompleted(sender As Object, e As DownloadDataCompletedEventArgs) Handles W.DownloadDataCompleted
        RTF = Encoding.ASCII.GetString(e.Result)
        RichTextBox1.Text = RTF
        Threading.Thread.Sleep(500)
        ProgressBar1.Visible = False
    End Sub

    Private Sub W_DownloadProgressChanged(sender As Object, e As DownloadProgressChangedEventArgs) Handles W.DownloadProgressChanged
        ProgressBar1.Value = e.ProgressPercentage
    End Sub
End Class