Public Class PE_Warning

    Dim PE_File As String

    Private Sub BugReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadLanguage
        ApplyStyle(Me)
        Dim c As Color = PictureBox1.Image.AverageColor.CB(If(My.Style.DarkMode, -0.35, 0.35))
        AnimatedBox1.BackColor = c
        CheckBox1.Checked = My.Settings.ThemeApplyingBehavior.Ignore_PE_Modify_Alert

        TreeView1.Font = My.Application.ConsoleFontMedium

        Try : BK.Close() : Catch : End Try
        Try : BK.Show() : Catch : End Try

        For Each lbl In AnimatedBox1.Controls.OfType(Of Label)
            lbl.ForeColor = Color.White
        Next

        My.Computer.Audio.PlaySystemSound(Media.SystemSounds.Exclamation)
    End Sub

    Public Function NotifyAction(SourceFile As String, ResourceType As String, ID As Integer, Optional LangID As UShort = 1033) As DialogResult

        Dim result As DialogResult

        TreeView1.Nodes.Clear()

        PE_File = SourceFile

        TreeView1.Nodes.Add(My.Lang.PE_Systemfile).Nodes.Add(IO.Path.GetFullPath(SourceFile))

        With TreeView1.Nodes.Add(My.Lang.PE_ReplacedResourceProperties)
            .Nodes.Add(My.Lang.PE_ResourceType).Nodes.Add(ResourceType)
            .Nodes.Add(My.Lang.PE_ResourceID).Nodes.Add(ID)
            .Nodes.Add(My.Lang.PE_ResourceLanguageCode).Nodes.Add(LangID)
        End With

        With TreeView1.Nodes.Add(My.Lang.PE_RunSFCinCMD_Node)
            .Nodes.Add("sfc /scanfile=""" & IO.Path.GetFullPath(SourceFile) & """")
            .Nodes.Add(My.Lang.PE_DontForgetToRestart)
        End With

        TreeView1.ExpandAll()

        result = ShowDialog()

        BK.Close()

        Return result
    End Function


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        My.Settings.ThemeApplyingBehavior.Ignore_PE_Modify_Alert = CheckBox1.Checked
        My.Settings.Save(WPSettings.Mode.Registry)

        DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        My.Settings.ThemeApplyingBehavior.Ignore_PE_Modify_Alert = CheckBox1.Checked
        My.Settings.Save(WPSettings.Mode.Registry)

        DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub TreeView1_DoubleClick(sender As Object, e As EventArgs) Handles TreeView1.DoubleClick
        Try
            If TreeView1.SelectedNode IsNot Nothing Then Clipboard.SetText(TreeView1.SelectedNode.Text)
        Catch
        End Try
    End Sub

    Private Sub PE_Warning_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        My.Settings.ThemeApplyingBehavior.Ignore_PE_Modify_Alert = CheckBox1.Checked
        My.Settings.Save(WPSettings.Mode.Registry)
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Process.Start(My.Resources.Link_Wiki & "/Antiviruses-or-browsers-download-issue")
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Cursor = Cursors.WaitCursor
        Reg_IO.SFC(PE_File)
        MsgBox(String.Format("{0}. {1}.", My.Lang.Done, My.Lang.PE_DontForgetToRestart), MsgBoxStyle.Information)
        Cursor = Cursors.Default
    End Sub
End Class