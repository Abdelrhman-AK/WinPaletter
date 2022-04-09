Imports System.Net
Imports WinPaletter.XenonCore

Public Class Updates
    Dim WebCL As New WebClient
    Dim url As String = Nothing
    Dim ver As String

    Private Sub XenonButton1_Click(sender As Object, e As EventArgs) Handles XenonButton1.Click
        If url Is Nothing Then
            Me.Cursor = Cursors.AppStarting

            My.Application.AnimatorX.Hide(XenonAlertBox2, True)
            My.Application.AnimatorX.HideSync(XenonButton1, True)

            Try
                If IsNetAvaliable() Then
                    Dim ls As New List(Of String)
                    CList_FromStr(ls, WebCL.DownloadString("https://dl.dropboxusercontent.com/s/rzmw42qxjx4gq5n/update.txt?dl=0"))
                    ver = ls(0)

                    If ver > My.Application.Info.Version.ToString Then
                        url = ls(1)
                        XenonButton1.Text = "Download from Github"
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
            Catch
                url = Nothing
                XenonButton1.Text = "Check for updates"
                XenonAlertBox1.Text = String.Format("Fatal Error, there maybe Network issues or Dropbox Servers Issues.")
                XenonAlertBox2.Text = String.Format("Fatal Error")
                XenonAlertBox1.AlertStyle = XenonAlertBox.Style.Warning
                XenonAlertBox2.AlertStyle = XenonAlertBox.Style.Warning
            End Try

            My.Application.AnimatorX.Show(XenonAlertBox2, True)
            My.Application.AnimatorX.ShowSync(XenonButton1, True)

            Me.Cursor = Cursors.Default
        Else
            Process.Start(url)
        End If

    End Sub

    Private Sub Updates_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ApplyDarkMode(Me)

        XenonAlertBox2.AlertStyle = XenonAlertBox.Style.Warning
        url = Nothing
        XenonButton1.Text = "Check for updates"
        Label2.Text = My.Application.Info.Version.ToString
    End Sub

    Private Sub XenonButton3_Click(sender As Object, e As EventArgs) Handles XenonButton3.Click
        Me.Close()
    End Sub
End Class