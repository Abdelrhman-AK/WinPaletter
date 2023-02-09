Imports WinPaletter.XenonCore

Public Class Lang_JSON_Manage
    Private Sub LangJSON_Manage_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ApplyDarkMode(Me)

        Label6.Font = My.Application.ConsoleFontMedium
        TreeView1.ImageList = My.Lang_IL

        If My.Settings.Language And IO.File.Exists(My.Settings.Language_File) Then
            TreeView1.FromJSON(My.Settings.Language_File, My.Computer.FileSystem.GetFileInfo(My.Settings.Language_File).Name)
            OpenJSONDlg.FileName = My.Settings.Language_File
            SaveJSONDlg.FileName = My.Settings.Language_File
        End If
    End Sub


    Private Sub TreeView1_BeforeLabelEdit(sender As Object, e As NodeLabelEditEventArgs) Handles TreeView1.BeforeLabelEdit
        If e.Node.Nodes.Count > 0 Then e.CancelEdit = True
    End Sub

    Private Sub TreeView1_AfterLabelEdit(sender As Object, e As NodeLabelEditEventArgs) Handles TreeView1.AfterLabelEdit
        XenonTextBox1.Text = e.Label
    End Sub

    Private Sub TreeView1_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles TreeView1.AfterSelect
        Label6.Text = e.Node.FullPath

        If e.Node.Nodes.Count = 0 Then
            Label4.Text = e.Node.Parent.Text
            XenonTextBox1.Text = e.Node.Text
        ElseIf e.Node.Nodes.Count = 1 Then
            Label4.Text = e.Node.Text
            XenonTextBox1.Text = e.Node.Nodes.Item(0).Text

        Else
            Label4.Text = ""
            XenonTextBox1.Text = ""
        End If

        XenonTextBox3.Text = XenonTextBox1.Text
    End Sub

    Private Sub XenonButton1_Click(sender As Object, e As EventArgs) Handles XenonButton1.Click
        If TreeView1.SelectedNode IsNot Nothing Then
            If TreeView1.SelectedNode.Nodes.Count = 0 Then
                TreeView1.SelectedNode.Text = XenonTextBox1.Text

            ElseIf TreeView1.SelectedNode.Nodes.Count = 1 Then
                TreeView1.SelectedNode.Nodes.Item(0).Text = XenonTextBox1.Text

            End If
        End If
    End Sub

    Private Sub XenonButton8_Click(sender As Object, e As EventArgs) Handles XenonButton8.Click

        If OpenJSONDlg.ShowDialog = DialogResult.OK Then
            TreeView1.FromJSON(OpenJSONDlg.FileName, My.Computer.FileSystem.GetFileInfo(OpenJSONDlg.FileName).Name)
        End If

    End Sub

    Private Sub XenonButton2_Click(sender As Object, e As EventArgs) Handles XenonButton2.Click

        If SaveJSONDlg.ShowDialog = DialogResult.OK Then
            IO.File.WriteAllText(SaveJSONDlg.FileName, TreeView1.Nodes.Item(0).ToJSON())
        End If

    End Sub


    Private Sub XenonButton3_Click(sender As Object, e As EventArgs) Handles XenonButton3.Click
        Dim searchText As String = XenonTextBox2.Text

        If String.IsNullOrEmpty(searchText) Then
            Return
        End If

        If LastSearchText <> searchText Then
            CurrentNodeMatches.Clear()
            LastSearchText = searchText
            LastNodeIndex = 0
            SearchNodes(searchText, TreeView1.Nodes(0))
        End If

        If LastNodeIndex >= CurrentNodeMatches.Count Then LastNodeIndex = 0

        If LastNodeIndex >= 0 AndAlso CurrentNodeMatches.Count > 0 AndAlso LastNodeIndex < CurrentNodeMatches.Count Then
            Dim selectedNode As TreeNode = CurrentNodeMatches(LastNodeIndex)
            LastNodeIndex += 1
            Me.TreeView1.SelectedNode = selectedNode
            Me.TreeView1.SelectedNode.Expand()
            Me.TreeView1.[Select]()
        End If
    End Sub


    ReadOnly CurrentNodeMatches As New List(Of TreeNode)()
    Private LastNodeIndex As Integer = 0
    Private LastSearchText As String


    Private Sub SearchNodes(ByVal SearchText As String, ByVal StartNode As TreeNode)
        While StartNode IsNot Nothing

            If StartNode.Text.ToLower().Contains(SearchText.ToLower()) Then
                CurrentNodeMatches.Add(StartNode)
            End If

            If StartNode.Nodes.Count <> 0 Then
                SearchNodes(SearchText, StartNode.Nodes(0))
            End If

            StartNode = StartNode.NextNode
        End While
    End Sub

    Private Sub XenonButton4_Click(sender As Object, e As EventArgs) Handles XenonButton4.Click
        If SaveJSONDlg.ShowDialog = DialogResult.OK Then
            Cursor = Cursors.WaitCursor
            Dim Lang As New Localizer
            Lang.ExportJSON(SaveJSONDlg.FileName)
            Lang.Dispose()
            TreeView1.FromJSON(SaveJSONDlg.FileName, My.Computer.FileSystem.GetFileInfo(SaveJSONDlg.FileName).Name)
            Cursor = Cursors.Default
        End If
    End Sub

    Private Sub XenonButton5_Click(sender As Object, e As EventArgs) Handles XenonButton5.Click
        If SaveJSONDlg.ShowDialog = DialogResult.OK Then
            Cursor = Cursors.WaitCursor
            Dim Lang As New Localizer
            Lang.ExportJSON(SaveJSONDlg.FileName)
            Lang.Dispose()
            Cursor = Cursors.Default
        End If
    End Sub

    Private Sub XenonButton10_Click(sender As Object, e As EventArgs)
        Lang_Add_Snippet.ShowDialog()
    End Sub

    Private Sub XenonButton6_Click(sender As Object, e As EventArgs) Handles XenonButton6.Click
        FontDialog1.Font = XenonTextBox1.Font

        If FontDialog1.ShowDialog = DialogResult.OK Then
            XenonTextBox1.Font = FontDialog1.Font
            XenonTextBox3.Font = FontDialog1.Font
        End If
    End Sub

    Private Sub XenonButton7_Click(sender As Object, e As EventArgs) Handles XenonButton7.Click
        Me.Close()
    End Sub

    Private Sub XenonButton9_Click(sender As Object, e As EventArgs) Handles XenonButton9.Click

        If Lang_Add_Snippet.ShowDialog = DialogResult.OK Then
            XenonTextBox1.Text = Lang_Add_Snippet._Result
        End If

    End Sub

    Private Sub XenonButton10_Click_1(sender As Object, e As EventArgs) Handles XenonButton10.Click
        TreeView1.Visible = False
        TreeView1.ExpandAll()
        TreeView1.Visible = True
    End Sub

    Private Sub XenonButton11_Click(sender As Object, e As EventArgs) Handles XenonButton11.Click
        TreeView1.Visible = False
        TreeView1.CollapseAll()
        TreeView1.Visible = True
    End Sub
End Class
