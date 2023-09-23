﻿Imports System.ComponentModel
Imports System.Net
Imports WinPaletter.XenonCore

Public Class Updates
    Dim WithEvents WebCL, UC As New WebClient
    Public url As String = Nothing
    Public ver As String
    Dim StableInt, BetaInt, UpdateChannel As Integer
    Dim OldName As String
    Public UpdateSize As Decimal
    Public ReleaseDate As Date
    Private _Shown As Boolean = False
    Public ls As New List(Of String)

    Private Sub XenonButton1_Click(sender As Object, e As EventArgs) Handles XenonButton1.Click
        MainFrm.NotifyUpdates.Visible = False

        If url Is Nothing Then
            Me.Cursor = Cursors.AppStarting

            My.Animator.HideSync(XenonAlertBox2, True)
            My.Animator.HideSync(XenonButton1, True)
            My.Animator.HideSync(XenonButton3, True)
            My.Animator.HideSync(Panel1, True)
            ProgressBar1.Visible = False
            ProgressBar1.Value = 0

            StableInt = 0 : BetaInt = 0 : UpdateChannel = 0

            Try
                If IsNetworkAvailable() Then
                    Label17.SetText(My.Lang.Checking)

                    ls = WebCL.DownloadString(My.Resources.Link_Updates).CList

                    For x = 0 To ls.Count - 1
                        If Not String.IsNullOrEmpty(ls(x)) And Not ls(x).IndexOf("#") = 0 Then
                            If ls(x).Split(" ")(0) = "Stable" Then StableInt = x
                            If ls(x).Split(" ")(0) = "Beta" Then BetaInt = x
                        End If
                    Next

                    If My.Settings.Updates.Channel = XeSettings.Structures.Updates.Channels.Stable Then UpdateChannel = StableInt
                    If My.Settings.Updates.Channel = XeSettings.Structures.Updates.Channels.Beta Then UpdateChannel = BetaInt

                    ver = ls(UpdateChannel).Split(" ")(1)

                    If ver > My.AppVersion Then
                        url = ls(UpdateChannel).Split(" ")(4)
                        UpdateSize = ls(UpdateChannel).Split(" ")(2)
                        ReleaseDate = Date.FromBinary(ls(UpdateChannel).Split(" ")(3))

                        Label7.Text = UpdateSize & " " & My.Lang.MBSizeUnit
                        Label9.Text = ReleaseDate.ToLongDateString

                        LinkLabel3.Visible = True

                        My.Animator.Show(Panel1, True)
                        XenonButton1.Text = My.Lang.DoAction_Update
                        XenonAlertBox2.Text = String.Format("{0}. {1} {2}", My.Lang.NewUpdate, My.Lang.Version, ver)
                        XenonAlertBox2.AlertStyle = UI.WP.AlertBox.Style.Indigo
                    Else
                        Label7.Text = ""
                        Label9.Text = ""
                        url = Nothing
                        XenonButton1.Text = My.Lang.CheckForUpdates
                        XenonAlertBox2.Text = String.Format(My.Lang.NoUpdateAvailable)
                        XenonAlertBox2.AlertStyle = UI.WP.AlertBox.Style.Success
                    End If

                    Label17.SetText(Text)
                Else
                    Label7.Text = ""
                    Label9.Text = ""
                    url = Nothing
                    XenonButton1.Text = My.Lang.CheckForUpdates
                    XenonAlertBox2.Text = String.Format(My.Lang.NetworkError)
                    XenonAlertBox2.AlertStyle = UI.WP.AlertBox.Style.Warning
                End If
            Catch ex As Exception
                Label7.Text = ""
                Label9.Text = ""
                url = Nothing
                XenonButton1.Text = My.Lang.CheckForUpdates
                XenonAlertBox2.Text = String.Format(My.Lang.ServerError)
                XenonAlertBox2.AlertStyle = UI.WP.AlertBox.Style.Warning
                BugReport.ThrowError(ex)
            End Try

            My.Animator.Show(XenonAlertBox2, True)
            My.Animator.Show(XenonButton1, True)
            My.Animator.ShowSync(XenonButton3, True)

            Me.Cursor = Cursors.Default
        Else
            ProgressBar1.Visible = False
            ProgressBar1.Value = 0

            If XenonRadioButton1.Checked Then
                XenonButton1.Enabled = False
                Panel1.Enabled = False
                ProgressBar1.Visible = True
                OldName = Reflection.Assembly.GetExecutingAssembly().Location
                Try
                    If IO.File.Exists("oldWinpaletter.trash") Then Kill("oldWinpaletter.trash")
                    If IO.File.Exists("oldWinpaletter_2.trash") Then Kill("oldWinpaletter_2.trash")
                Catch : End Try
                My.Computer.FileSystem.RenameFile(OldName, "oldWinpaletter.trash")
                UC.DownloadFileAsync(New Uri(url), OldName)
            End If

            If XenonRadioButton2.Checked Then
                SaveFileDialog1.FileName = String.Format("WinPaletter ({0})", ver)

                If SaveFileDialog1.ShowDialog() = DialogResult.OK Then
                    Panel1.Enabled = False
                    XenonButton1.Enabled = False
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
        LoadLanguage
        ApplyStyle(Me)
        UC = New WebClient
        LinkLabel3.Visible = False
        Dim F As String = If(My.Lang.RightToLeft, "{1}: {0}", "{0} {1}")
        Label3.Text = String.Format(F, If(My.Settings.Updates.Channel = XeSettings.Structures.Updates.Channels.Stable, My.Lang.Stable, My.Lang.Beta), My.Lang.Channel)
        XenonCheckBox1.Checked = My.Settings.Updates.AutoCheck
        _Shown = False
        XenonAlertBox2.AlertStyle = UI.WP.AlertBox.Style.Warning
        XenonAlertBox2.Visible = False
        Panel1.Visible = False
        Label7.Text = ""
        Label9.Text = ""
        url = Nothing
        XenonButton1.Enabled = True
        Panel1.Enabled = True

        XenonButton1.Text = My.Lang.CheckForUpdates
        Label2.Text = My.AppVersion

        If ls.Count > 0 Then
            StableInt = 0 : BetaInt = 0 : UpdateChannel = 0

            For x = 0 To ls.Count - 1
                If Not String.IsNullOrEmpty(ls(x)) And Not ls(x).IndexOf("#") = 0 Then
                    If ls(x).Split(" ")(0) = "Stable" Then StableInt = x
                    If ls(x).Split(" ")(0) = "Beta" Then BetaInt = x
                End If
            Next

            If My.Settings.Updates.Channel = XeSettings.Structures.Updates.Channels.Stable Then UpdateChannel = StableInt
            If My.Settings.Updates.Channel = XeSettings.Structures.Updates.Channels.Beta Then UpdateChannel = BetaInt

            url = ls(UpdateChannel).Split(" ")(4)
            UpdateSize = ls(UpdateChannel).Split(" ")(2)
            ReleaseDate = Date.FromBinary(ls(UpdateChannel).Split(" ")(3))
            ver = ls(UpdateChannel).Split(" ")(1)

            Label7.Text = UpdateSize & " " & My.Lang.MBSizeUnit
            Label9.Text = ReleaseDate.ToLongDateString & " " & ReleaseDate.ToLongTimeString

            LinkLabel3.Visible = True

            My.Animator.Show(Panel1, True)
            XenonButton1.Text = My.Lang.DoAction_Update
            XenonAlertBox2.Text = String.Format("{0}. {1} {2}", My.Lang.NewUpdate, My.Lang.Version, ver)
            XenonAlertBox2.AlertStyle = UI.WP.AlertBox.Style.Indigo

            My.Animator.Show(XenonAlertBox2, True)
            My.Animator.ShowSync(XenonButton1, True)
        End If

        If My.WXP Then
            XenonAlertBox2.AlertStyle = UI.WP.AlertBox.Style.Warning
            XenonAlertBox2.Visible = True
            XenonAlertBox2.Text = String.Format(My.Lang.UpdatesOSNoTLS12, My.Lang.OS_WinXP)

        ElseIf My.WVista Then
            XenonAlertBox2.AlertStyle = UI.WP.AlertBox.Style.Warning
            XenonAlertBox2.Visible = True
            XenonAlertBox2.Text = String.Format(My.Lang.UpdatesOSNoTLS12, My.Lang.OS_WinVista)
        End If

    End Sub

    Private Sub Label3_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles Label3.LinkClicked
        Dim f As New Updates
        Me.Close()
        SettingsX.ShowDialog()
        f.ShowDialog()
    End Sub

    Private Sub Updates_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        _Shown = True
    End Sub

    Private Sub XenonCheckBox1_CheckedChanged(sender As Object) Handles XenonCheckBox1.CheckedChanged
        If _Shown Then
            My.Settings.Updates.AutoCheck = XenonCheckBox1.Checked
            My.Settings.Updates.Save()
        End If
    End Sub

    Private Sub LinkLabel3_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel3.LinkClicked
        Process.Start(My.Resources.Link_Changelog)
    End Sub

    Private Sub XenonButton3_Click(sender As Object, e As EventArgs) Handles XenonButton3.Click
        Me.Close()
    End Sub

    Private Sub UC_DownloadProgressChanged(sender As Object, e As DownloadProgressChangedEventArgs) Handles UC.DownloadProgressChanged
        ProgressBar1.Value = e.ProgressPercentage
    End Sub

    Private Sub UC_DownloadFileCompleted(sender As Object, e As AsyncCompletedEventArgs) Handles UC.DownloadFileCompleted
        XenonButton1.Enabled = True
        Panel1.Enabled = True
        ProgressBar1.Visible = False
        ProgressBar1.Value = 0
        If XenonRadioButton2.Checked Then MsgBox(My.Lang.Msgbox_Downloaded, MsgBoxStyle.Information)
        If XenonRadioButton1.Checked And Not Disturbed Then
            Process.Start(OldName)
            Using Prc As Process = Process.GetCurrentProcess : Prc.Kill() : End Using
        End If
    End Sub

    Private Sub Updates_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        DisturbActions()
        ls.Clear()
    End Sub

    Private Sub Updates_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        DisturbActions()
        ls.Clear()
    End Sub

    Private Disturbed As Boolean = False

    Sub DisturbActions()
        If UC.IsBusy Then
            Disturbed = True

            UC.CancelAsync()
            UC.Dispose()

            If XenonRadioButton1.Checked Then
                Try
                    If IO.File.Exists("oldWinpaletter_2.trash") Then Kill("oldWinpaletter_2.trash")
                Catch
                End Try
                My.Computer.FileSystem.RenameFile(OldName, "oldWinpaletter_2.trash")
                My.Computer.FileSystem.RenameFile("oldWinpaletter.trash", OldName.Split("\").Last)
                Try
                    If IO.File.Exists("oldWinpaletter_2.trash") Then Kill("oldWinpaletter_2.trash")
                    If IO.File.Exists("oldWinpaletter.trash") Then Kill("oldWinpaletter.trash")
                Catch
                End Try
            End If

        End If

        If XenonRadioButton2.Checked Then
            If IO.File.Exists(SaveFileDialog1.FileName) Then Kill(SaveFileDialog1.FileName)
        End If
    End Sub

End Class