Imports System.ComponentModel
Imports System.Net
Imports WinPaletter.XenonCore

Public Class Updates
    Dim WithEvents WebCL, UC As New WebClient
    Dim url As String = Nothing
    Dim ver As String
    Dim StableInt, BetaInt, UpdateChannel As Integer
    Dim OldName As String
    Dim UpdateSize As Decimal
    Dim ReleaseDate As Date
    Private _Shown As Boolean = False

    Private Sub XenonButton1_Click(sender As Object, e As EventArgs) Handles XenonButton1.Click
        If url Is Nothing Then
            Me.Cursor = Cursors.AppStarting
            TreeView1.Nodes.Clear()

            My.Application.AnimatorX.HideSync(XenonAlertBox2, True)
            My.Application.AnimatorX.HideSync(XenonButton1, True)
            My.Application.AnimatorX.HideSync(Panel1, True)
            ProgressBar1.Visible = False
            ProgressBar1.Value = 0

            StableInt = 0 : BetaInt = 0 : UpdateChannel = 0

            Try
                If IsNetAvaliable() Then
                    Label5.Text = "Checking ..."
                    Dim ls As New List(Of String)

                    CList_FromStr(ls, WebCL.DownloadString(My.Resources.Link_Updates))

                    For x = 0 To ls.Count - 1
                        If Not String.IsNullOrEmpty(ls(x)) And Not ls(x).IndexOf("#") = 0 Then
                            If ls(x).Split(" ")(0) = "Stable" Then StableInt = x
                            If ls(x).Split(" ")(0) = "Beta" Then BetaInt = x
                        End If
                    Next

                    If My.Application._Settings.UpdateChannel = XeSettings.UpdateChannels.Stable Then UpdateChannel = StableInt
                    If My.Application._Settings.UpdateChannel = XeSettings.UpdateChannels.Beta Then UpdateChannel = BetaInt

                    ver = ls(UpdateChannel).Split(" ")(1)

                    If ver > My.Application.Info.Version.ToString Then
                        url = ls(UpdateChannel).Split(" ")(4)
                        UpdateSize = ls(UpdateChannel).Split(" ")(2)
                        ReleaseDate = Date.FromBinary(ls(UpdateChannel).Split(" ")(3))

                        Label5.Text = ver
                        Label7.Text = UpdateSize & " MB"
                        Label9.Text = ReleaseDate

                        Dim Customchangelog_str As String = Nothing

                        If IsNetAvaliable() Then
                            Try
                                Customchangelog_str = WebCL.DownloadString(New Uri(My.Resources.Link_Changelog))
                            Catch ex As Exception
                                With TreeView1.Nodes.Add("Error reading changelog online")
                                    Dim imgI As Integer = My.Application.ChangeLogImgLst.Images.IndexOfKey("Error")
                                    .ImageIndex = imgI : .SelectedImageIndex = imgI
                                    With .Nodes.Add(ex.Message.Replace(vbCrLf, ", "))
                                        .ImageIndex = imgI : .SelectedImageIndex = imgI
                                    End With
                                End With

                                TreeView1.ExpandAll()
                            End Try
                        Else
                            With TreeView1.Nodes.Add("No Network is avaliable")
                                Dim imgI As Integer = My.Application.ChangeLogImgLst.Images.IndexOfKey("Error")
                                .ImageIndex = imgI : .SelectedImageIndex = imgI
                                With .Nodes.Add("Check your connection and try again")
                                    .ImageIndex = imgI : .SelectedImageIndex = imgI
                                End With
                            End With

                            TreeView1.ExpandAll()
                        End If

                        Changelog.PhraseInfo(TreeView1, ver, Customchangelog_str)
                        My.Application.AnimatorX.Show(Panel1, True)
                        XenonButton1.Text = "Do Action"
                        XenonAlertBox2.Text = String.Format("Update Avaliable ({0})", ver)
                        XenonAlertBox2.AlertStyle = XenonAlertBox.Style.Warning
                    Else
                        Label5.Text = ""
                        Label7.Text = ""
                        Label9.Text = ""
                        url = Nothing
                        XenonButton1.Text = "Check for updates"
                        XenonAlertBox2.Text = String.Format("No Avaliable Updates")
                        XenonAlertBox2.AlertStyle = XenonAlertBox.Style.Success
                    End If
                Else
                    Label5.Text = ""
                    Label7.Text = ""
                    Label9.Text = ""
                    url = Nothing
                    XenonButton1.Text = "Check for updates"
                    XenonAlertBox2.Text = String.Format("Network Error")
                    XenonAlertBox2.AlertStyle = XenonAlertBox.Style.Warning
                End If
            Catch
                Label5.Text = ""
            Label7.Text = ""
            Label9.Text = ""
            url = Nothing
            XenonButton1.Text = "Check for updates"
            XenonAlertBox2.Text = String.Format("Error: Network issues or Github repository is private or deleted. Visit Github page for details.")
            XenonAlertBox2.AlertStyle = XenonAlertBox.Style.Warning
            End Try

            My.Application.AnimatorX.Show(XenonAlertBox2, True)
            My.Application.AnimatorX.ShowSync(XenonButton1, True)

            Me.Cursor = Cursors.Default
        Else
            ProgressBar1.Visible = False
            ProgressBar1.Value = 0

            If XenonRadioButton1.Checked Then
                ProgressBar1.Visible = True
                OldName = Reflection.Assembly.GetExecutingAssembly().Location
                My.Computer.FileSystem.RenameFile(OldName, "oldWinpaletter.trash")
                UC.DownloadFileAsync(New Uri(url), OldName)
            End If

            If XenonRadioButton2.Checked Then
                SaveFileDialog1.FileName = String.Format("WinPaletter ({0})", ver)

                If SaveFileDialog1.ShowDialog() = DialogResult.OK Then
                    ProgressBar1.Visible = True
                    UC.DownloadFileAsync(New Uri(url), SaveFileDialog1.FileName)
                Else
                    ProgressBar1.Visible = False
                End If
            End If

            If XenonRadioButton3.Checked Then
                Process.Start(My.Resources.Link_Releases)
            End If

        End If

    End Sub

    Private Sub Updates_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ApplyDarkMode(Me)
        TreeView1.Nodes.Clear()
        Label3.Text = String.Format("{0} Channel", If(My.Application._Settings.UpdateChannel = XeSettings.UpdateChannels.Stable, "Stable", "Beta"))
        XenonCheckBox1.Checked = My.Application._Settings.AutoUpdatesChecking
        _Shown = False
        XenonAlertBox2.AlertStyle = XenonAlertBox.Style.Warning
        url = Nothing
        XenonButton1.Text = "Check for updates"
        Label2.Text = My.Application.Info.Version.ToString
    End Sub

    Private Sub Label3_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles Label3.LinkClicked
        Dim f As New Updates
        Me.Close()
        SettingsX.ShowDialog()
        f.Show()
    End Sub

    Private Sub Updates_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        _Shown = True
    End Sub

    Private Sub XenonCheckBox1_CheckedChanged(sender As Object) Handles XenonCheckBox1.CheckedChanged
        If _Shown Then
            My.Application._Settings.AutoUpdatesChecking = XenonCheckBox1.Checked
            My.Application._Settings.Save(XeSettings.Mode.Registry)
        End If
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Changelog.Show()
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
        If XenonRadioButton1.Checked Then
            Process.Start(OldName)
            Process.GetCurrentProcess.Kill()
        End If
    End Sub
End Class