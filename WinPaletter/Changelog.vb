Imports System.Net
Imports System.Runtime.InteropServices
Imports System.Text
Imports WinPaletter.XenonCore

Public Class Changelog

    Dim WithEvents W As WebClient
    Dim changelog_str As String

    <DllImport("uxtheme.dll", ExactSpelling:=True, CharSet:=CharSet.Unicode)>
    Private Shared Function SetWindowTheme(ByVal hwnd As IntPtr, ByVal pszSubAppName As String, ByVal pszSubIdList As String) As Integer
    End Function
    Public Shared Sub SetTreeViewTheme(ByVal treeHandle As IntPtr)
        SetWindowTheme(treeHandle, "explorer", Nothing)
    End Sub

    Private Sub Changelog_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _Shown = False
        TreeView1.Nodes.Clear()
        TreeView1.FullRowSelect = True
        TreeView1.ItemHeight = 32
        TreeView1.ShowRootLines = True
        TreeView1.ShowPlusMinus = True
        TreeView1.ImageList = My.Application.ChangeLogImgLst

        XenonTextBox1.Text = My.Application.Info.Version.ToString
        XenonCheckBox1.Checked = (My.Application._Settings.UpdateChannel = XeSettings.UpdateChannels.Beta)
        ApplyDarkMode(Me)
        ProgressBar1.Visible = False
        LoadChangelog()
    End Sub

    Sub LoadChangelog()
        ProgressBar1.Visible = True
        ProgressBar1.Value = 0
        W = New WebClient

        If IsNetAvaliable() Then
            Try
                W.DownloadDataAsync(New Uri(My.Resources.Link_Changelog))
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

    End Sub

    Private Sub W_DownloadDataCompleted(sender As Object, e As DownloadDataCompletedEventArgs) Handles W.DownloadDataCompleted
        ProgressBar1.Visible = False

        If e.Error Is Nothing Then
            changelog_str = Encoding.ASCII.GetString(e.Result)
            PhraseInfo(TreeView1)
        Else

            With TreeView1.Nodes.Add("Error reading changelog online")
                Dim imgI As Integer = My.Application.ChangeLogImgLst.Images.IndexOfKey("Error")
                .ImageIndex = imgI : .SelectedImageIndex = imgI
                With .Nodes.Add(e.Error.Message.Replace(vbCrLf, ", "))
                    .ImageIndex = imgI : .SelectedImageIndex = imgI
                End With
            End With

            TreeView1.ExpandAll()
        End If

    End Sub

    Private Sub W_DownloadProgressChanged(sender As Object, e As DownloadProgressChangedEventArgs) Handles W.DownloadProgressChanged
        ProgressBar1.Value = e.ProgressPercentage
    End Sub

    Public Sub PhraseInfo([TreeView] As TreeView, Optional ByVal SpecificVersion As String = Nothing, Optional ByVal Customchangelog_str As String = Nothing)
        Dim Condition0, Condition1 As Boolean
        Dim ls As New List(Of String)
        Dim Versions As New Dictionary(Of String, String)
        Versions.Clear()
        ls.Clear()
        [TreeView].Nodes.Clear()
        [TreeView].FullRowSelect = True
        [TreeView].ItemHeight = 32
        [TreeView].ShowRootLines = True
        [TreeView].ShowPlusMinus = True
        [TreeView].ImageList = My.Application.ChangeLogImgLst
        SetTreeViewTheme([TreeView].Handle)

        Try
            CList_FromStr(ls, If(Customchangelog_str = Nothing, changelog_str, Customchangelog_str))

            For x = 0 To ls.Count - 1
                Condition0 = (SpecificVersion = Nothing And ls(x).Contains("[") And ls(x).Contains("]") And ls(x).Contains("."))

                If Not Condition0 And Not SpecificVersion = Nothing Then
                    Condition1 = ls(x).Contains(SpecificVersion)
                End If

                If Condition0 Or Condition1 Then

                    Dim i1 As Integer = x

                    If ls(i1 + 1).ToLower = "channel= beta" And Not My.Application._Settings.UpdateChannel = XeSettings.UpdateChannels.Beta And SpecificVersion = Nothing Then
                        If Not XenonCheckBox1.Checked Then GoTo Skip
                    End If

                    Dim i2 As Integer

                    For x2 = i1 + 1 To ls.Count - 1
                        If ls(x2).Contains("[") And ls(x2).Contains("]") And ls(x2).Contains(".") Then
                            i2 = x2 - 1
                            Exit For
                        End If

                        If x2 >= ls.Count - 1 Then
                            i2 = ls.Count - 1
                            Exit For
                        End If
                    Next

                    Versions.Add(ls(i1), String.Format("{0}|{1}", i1, i2))
Skip:
                End If
            Next

            Dim Beta As Boolean = False

            For x = 0 To Versions.Count - 1

                With [TreeView].Nodes.Add(Versions.Keys(x).Replace("[", "").Replace("]", ""))
                    Dim imgI As Integer = My.Application.ChangeLogImgLst.Images.IndexOfKey("Stable")
                    .ImageIndex = imgI : .SelectedImageIndex = imgI
                End With


                With Versions.Values(x).ToString
                    For i As Integer = .Split("|")(0) To .Split("|")(1)
                        If Not String.IsNullOrWhiteSpace(ls(i)) And Not Versions.Keys.Contains(ls(i)) Then

                            If Not ls(i).StartsWith("#") Then

                                Dim Str As String = ""
                                Dim imgI As Integer

                                If ls(i).StartsWith("Channel= ") Then
                                    Str = ls(i).Remove(0, "Channel= ".Count) & " Channel"
                                    Beta = Str.ToLower.Contains("beta")
                                    imgI = My.Application.ChangeLogImgLst.Images.IndexOfKey("Channel")
                                End If

                                If ls(i).StartsWith("BugFix= ") Then
                                    Str = ls(i).Remove(0, "BugFix= ".Count)
                                    imgI = My.Application.ChangeLogImgLst.Images.IndexOfKey("BugFix")
                                End If

                                If ls(i).StartsWith("NewFeature= ") Then
                                    Str = ls(i).Remove(0, "NewFeature= ".Count)
                                    imgI = My.Application.ChangeLogImgLst.Images.IndexOfKey("New")
                                End If

                                If ls(i).StartsWith("Added= ") Then
                                    Str = ls(i).Remove(0, "Added= ".Count)
                                    imgI = My.Application.ChangeLogImgLst.Images.IndexOfKey("Add")
                                End If

                                If ls(i).StartsWith("Removed= ") Then
                                    Str = ls(i).Remove(0, "Removed= ".Count)
                                    imgI = My.Application.ChangeLogImgLst.Images.IndexOfKey("Removed")
                                End If

                                If ls(i).StartsWith("Date= ") Then
                                    Str = "Released on: " & Date.FromBinary(ls(i).Remove(0, "Date= ".Count))
                                    imgI = My.Application.ChangeLogImgLst.Images.IndexOfKey("Date")
                                End If

                                With [TreeView].Nodes.Item(x).Nodes.Add(Str)
                                    .ImageIndex = imgI : .SelectedImageIndex = imgI

                                    If Beta Then
                                        With .Parent
                                            imgI = My.Application.ChangeLogImgLst.Images.IndexOfKey("Beta")
                                            .ImageIndex = imgI : .SelectedImageIndex = imgI
                                        End With
                                    End If

                                End With
                            End If

                        End If
                    Next
                End With
            Next

            [TreeView].ExpandAll()
            [TreeView].SelectedNode = [TreeView].Nodes.Item(0)

        Catch ex As Exception
            Dim whatToadd As String

            If SpecificVersion IsNot Nothing Then
                whatToadd = "Version " & SpecificVersion & " is not released yet, deleted or written in a wrong format."
            Else
                whatToadd = ex.Message.Replace(vbCrLf, ", ")
            End If

            With [TreeView].Nodes.Add("Error phrasing changelog")
                .ImageIndex = My.Application.ChangeLogImgLst.Images.IndexOfKey("Error")
                .SelectedImageIndex = My.Application.ChangeLogImgLst.Images.IndexOfKey("Error")
                With .Nodes.Add(whatToadd)
                    .ImageIndex = My.Application.ChangeLogImgLst.Images.IndexOfKey("Error")
                    .SelectedImageIndex = My.Application.ChangeLogImgLst.Images.IndexOfKey("Error")
                End With
            End With

            [TreeView].ExpandAll()
        End Try

    End Sub

    Private Sub XenonCheckBox1_CheckedChanged(sender As Object) Handles XenonCheckBox1.CheckedChanged
        If _Shown Then PhraseInfo(TreeView1)
    End Sub

    Private Sub Changelog_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        _Shown = True
    End Sub

    Private _Shown As Boolean = False

    Private Sub XenonTextBox1_TextChanged(sender As Object, e As EventArgs) Handles XenonTextBox1.TextChanged
        Try
            If Not _Shown Then Exit Sub

            Dim wrongFormat As Boolean = False

            For Each x As Object In XenonTextBox1.Text.Split(".")
                If Not IsNumeric(x) Then wrongFormat = True
            Next

            If Not wrongFormat Then PhraseInfo(TreeView1, XenonTextBox1.Text) Else PhraseInfo(TreeView1)
        Catch

        End Try
    End Sub

    Public Shared Function RemoveExtraSpaces(strVal As String) As String
        Dim sTempstrVal As String

        sTempstrVal = ""

        For iCount = 1 To Len(strVal)
            sTempstrVal += Mid(strVal, iCount, 1).Trim
        Next

        RemoveExtraSpaces = sTempstrVal

        Return RemoveExtraSpaces

    End Function
End Class
