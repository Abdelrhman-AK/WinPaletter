Imports WinPaletter.XenonCore

Public Class PE_Warning

    Dim PE_File As String

    Private Sub BugReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadLanguage
        ApplyDarkMode(Me)
        Dim c As Color = PictureBox1.Image.AverageColor.CB(If(GetDarkMode(), -0.35, 0.35))
        XenonAnimatedBox1.BackColor = c
        XenonCheckBox1.Checked = My.Settings.ThemeApplyingBehavior.Ignore_PE_Modify_Alert

        XenonTreeView1.Font = My.Application.ConsoleFontMedium

        Try : BK.Close() : Catch : End Try
        Try : BK.Show() : Catch : End Try

        For Each lbl In XenonAnimatedBox1.Controls.OfType(Of Label)
            lbl.ForeColor = Color.White
        Next

        My.Computer.Audio.PlaySystemSound(Media.SystemSounds.Exclamation)
    End Sub

    Public Function NotifyAction(SourceFile As String, ResourceType As String, ID As Integer, Optional LangID As UShort = 1033) As DialogResult

        Dim result As DialogResult

        XenonTreeView1.Nodes.Clear()

        PE_File = SourceFile

        XenonTreeView1.Nodes.Add(My.Lang.PE_Systemfile).Nodes.Add(IO.Path.GetFullPath(SourceFile))

        With XenonTreeView1.Nodes.Add(My.Lang.PE_ReplacedResourceProperties)
            .Nodes.Add(My.Lang.PE_ResourceType).Nodes.Add(ResourceType)
            .Nodes.Add(My.Lang.PE_ResourceID).Nodes.Add(ID)
            .Nodes.Add(My.Lang.PE_ResourceLanguageCode).Nodes.Add(LangID)
        End With

        With XenonTreeView1.Nodes.Add(My.Lang.PE_RunSFCinCMD_Node)
            .Nodes.Add("sfc /scanfile=""" & IO.Path.GetFullPath(SourceFile) & """")
            .Nodes.Add(My.Lang.PE_DontForgetToRestart)
        End With

        XenonTreeView1.ExpandAll()

        result = ShowDialog()

        BK.Close()

        Return result
    End Function


    Private Sub XenonButton2_Click(sender As Object, e As EventArgs) Handles XenonButton2.Click
        My.Settings.ThemeApplyingBehavior.Ignore_PE_Modify_Alert = XenonCheckBox1.Checked
        My.Settings.Save(XeSettings.Mode.Registry)

        DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub XenonButton1_Click(sender As Object, e As EventArgs) Handles XenonButton1.Click
        My.Settings.ThemeApplyingBehavior.Ignore_PE_Modify_Alert = XenonCheckBox1.Checked
        My.Settings.Save(XeSettings.Mode.Registry)

        DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub XenonTreeView1_DoubleClick(sender As Object, e As EventArgs) Handles XenonTreeView1.DoubleClick
        Try
            If XenonTreeView1.SelectedNode IsNot Nothing Then Clipboard.SetText(XenonTreeView1.SelectedNode.Text)
        Catch
        End Try
    End Sub

    Private Sub PE_Warning_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        My.Settings.ThemeApplyingBehavior.Ignore_PE_Modify_Alert = XenonCheckBox1.Checked
        My.Settings.Save(XeSettings.Mode.Registry)
    End Sub

    Private Sub XenonButton3_Click(sender As Object, e As EventArgs) Handles XenonButton3.Click
        Process.Start(My.Resources.Link_Wiki & "/Antiviruses-or-browsers-download-issue")
    End Sub

    Private Sub XenonButton4_Click(sender As Object, e As EventArgs) Handles XenonButton4.Click
        Cursor = Cursors.WaitCursor
        Reg_IO.SFC(PE_File)
        MsgBox(String.Format("{0}. {1}.", My.Lang.Done, My.Lang.PE_DontForgetToRestart), MsgBoxStyle.Information)
        Cursor = Cursors.Default
    End Sub
End Class