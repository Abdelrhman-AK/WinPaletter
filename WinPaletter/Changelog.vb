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
        RTF = Nothing

        '{\colortbl ;\red255\green255\blue0;\red255\green0\blue0;}

        Dim DefCTCount As Integer
        Dim DarkIndex As String = "cf0"
        Dim LightIndex As String = "cf1"
        Dim TBR As String = "cf"

        For Each l As String In Encoding.ASCII.GetString(e.Result).Split(vbCrLf)
            If l.Contains("{\colortbl") Then

                For Each r As String In l.Split(";")
                    MsgBox(r)
                Next

                DefCTCount = l.Split(";").Count - 2
                l = l.Replace("}", "\red0\green0\blue0;\red255\green255\blue255;}")
                DarkIndex = String.Format("cf{0}", DefCTCount + 1)
                LightIndex = String.Format("cf{0}", DefCTCount + 2)
                RTF &= l
            Else
                RTF &= l
            End If
        Next

        RTF = RTF.Replace("cf2", "cf1")
        RichTextBox1.Rtf = RTF



        Threading.Thread.Sleep(500)
        ProgressBar1.Visible = False
    End Sub

    Private Sub W_DownloadProgressChanged(sender As Object, e As DownloadProgressChangedEventArgs) Handles W.DownloadProgressChanged
        ProgressBar1.Value = e.ProgressPercentage
    End Sub
End Class