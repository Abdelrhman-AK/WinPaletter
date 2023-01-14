Imports WinPaletter.XenonCore
Public Class Saving_ex_list
    Private Sub Saving_exceptions_list_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ApplyDarkMode(Me)
        Icon = BugReport.Icon

        MainFrm.SetTreeViewTheme(TreeView1.Handle)
        TreeView1.ImageList = My.Notifications_IL
        TreeView1.Nodes.Clear()

        For Each x In My.Saving_Exceptions
            With TreeView1.Nodes.Add(x.Item1)
                .ImageKey = "error" : .SelectedImageKey = "error"
            End With
        Next

    End Sub

    Private Sub XenonButton2_Click(sender As Object, e As EventArgs) Handles XenonButton2.Click

        If TreeView1.SelectedNode IsNot Nothing Then BugReport.ThrowError(My.Saving_Exceptions.Item(TreeView1.SelectedNode.Index).Item2)

    End Sub

    Private Sub XenonButton1_Click(sender As Object, e As EventArgs) Handles XenonButton1.Click
        Me.Close()
    End Sub
End Class