Public Class Saving_ex_list

    Public ex_List As List(Of Tuple(Of String, Exception))

    Private Sub Saving_exceptions_list_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadLanguage
        ApplyStyle(Me)
        Icon = BugReport.Icon

        TreeView1.ImageList = My.Notifications_IL
        TreeView1.Nodes.Clear()

        For Each x In ex_List
            With TreeView1.Nodes.Add(x.Item1)
                .ImageKey = "error" : .SelectedImageKey = "error"
            End With
        Next
    End Sub

    Private Sub XenonButton2_Click(sender As Object, e As EventArgs) Handles XenonButton2.Click

        If TreeView1.SelectedNode IsNot Nothing Then BugReport.ThrowError(ex_List.Item(TreeView1.SelectedNode.Index).Item2)

    End Sub

    Private Sub XenonButton1_Click(sender As Object, e As EventArgs) Handles XenonButton1.Click
        Me.Close()
    End Sub
End Class