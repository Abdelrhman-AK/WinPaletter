Imports WinPaletter.XenonCore

Public Class SettingsX
    Private Sub XenonButton2_Click(sender As Object, e As EventArgs) Handles XenonButton2.Click
        Me.Close()
    End Sub

    Private Sub SettingsX_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ApplyDarkMode(Me)
        LoadSettings()
    End Sub

    Public _External As Boolean = False
    Public _File As String = Nothing

    Dim Changed As Boolean = False

    Protected Overrides Sub OnFormClosing(ByVal e As FormClosingEventArgs)
        Dim NewSets As New XeSettings(XeSettings.Mode.Empty)

        Changed = False

        With My.Application._Settings
            If .AutoAddExt <> XenonCheckBox1.Checked Then Changed = True
            If .DragAndDropPreview <> XenonCheckBox3.Checked Then Changed = True
            If .OpeningPreviewInApp_or_AppliesIt <> XenonRadioButton1.Checked Then Changed = True
            If .AutoRestartExplorer <> XenonCheckBox2.Checked Then Changed = True
            If .AutoUpdatesChecking <> XenonCheckBox5.Checked Then Changed = True
            If .CustomPreviewConfig_Enabled <> XenonCheckBox4.Checked Then Changed = True
            If .CustomPreviewConfig <> XenonComboBox1.SelectedIndex Then Changed = True
            If .UpdateChannel <> XenonComboBox2.SelectedIndex Then Changed = True
            If .Appearance_Dark <> XenonRadioButton3.Checked Then Changed = True
            If .Appearance_Auto <> XenonCheckBox6.Checked Then Changed = True
        End With

        If e.CloseReason = CloseReason.UserClosing And Changed Then
            Select Case MsgBox("Do you want to save Settings?", MsgBoxStyle.Question + MsgBoxStyle.YesNoCancel)
                Case DialogResult.Cancel
                    e.Cancel = True
                Case DialogResult.Yes
                    SaveSettings()
                    _External = False
                    _File = Nothing
                    e.Cancel = False
                    MyBase.OnFormClosing(e)
                Case DialogResult.No
                    _External = False
                    _File = Nothing
                    e.Cancel = False
                    MyBase.OnFormClosing(e)
            End Select
        ElseIf e.CloseReason = CloseReason.UserClosing And Not Changed Then
            e.Cancel = False
            MyBase.OnFormClosing(e)
        End If
    End Sub

    Sub LoadSettings()
        Dim sets As XeSettings

        If Not _External Then sets = My.Application._Settings Else sets = New XeSettings(XeSettings.Mode.File, _File)

        With sets
            XenonCheckBox1.Checked = .AutoAddExt
            XenonCheckBox3.Checked = .DragAndDropPreview
            XenonRadioButton1.Checked = .OpeningPreviewInApp_or_AppliesIt
            XenonRadioButton2.Checked = Not .OpeningPreviewInApp_or_AppliesIt

            XenonCheckBox2.Checked = .AutoRestartExplorer
            XenonCheckBox5.Checked = .AutoUpdatesChecking
            XenonCheckBox4.Checked = .CustomPreviewConfig_Enabled
            XenonComboBox1.SelectedIndex = If(.CustomPreviewConfig = .WinVer.Eleven, 0, 1)
            XenonComboBox2.SelectedIndex = If(.UpdateChannel = .UpdateChannels.Stable, 0, 1)

            XenonRadioButton3.Checked = .Appearance_Dark
            XenonRadioButton4.Checked = Not .Appearance_Dark
            XenonCheckBox6.Checked = .Appearance_Auto
        End With

        If _External Then OpenFileDialog1.FileName = _File
    End Sub

    Sub SaveSettings()
        With My.Application._Settings
            .AutoAddExt = XenonCheckBox1.Checked
            .DragAndDropPreview = XenonCheckBox3.Checked
            .OpeningPreviewInApp_or_AppliesIt = XenonRadioButton1.Checked
            .AutoRestartExplorer = XenonCheckBox2.Checked
            .AutoUpdatesChecking = XenonCheckBox5.Checked
            .CustomPreviewConfig_Enabled = XenonCheckBox4.Checked
            .CustomPreviewConfig = XenonComboBox1.SelectedIndex
            .UpdateChannel = XenonComboBox2.SelectedIndex
            .Appearance_Dark = XenonRadioButton3.Checked
            .Appearance_Auto = XenonCheckBox6.Checked
            .Save(XeSettings.Mode.Registry)
        End With

        MsgBox("Settings Saved.", MsgBoxStyle.Information)
        Me.Close()

        If My.Application._Settings.CustomPreviewConfig_Enabled Then
            MainFrm.PreviewConfig = My.Application._Settings.CustomPreviewConfig
        Else
            If My.W11 Then MainFrm.PreviewConfig = MainFrm.WinVer.Eleven Else MainFrm.PreviewConfig = MainFrm.WinVer.Ten
        End If

        MainFrm.Adjust_Preview()
        MainFrm.ApplyLivePreviewFromCP(MainFrm.CP)
        ApplyDarkMode()
    End Sub

    Private Sub XenonButton1_Click(sender As Object, e As EventArgs) Handles XenonButton1.Click
        SaveSettings()
    End Sub

    Private Sub XenonButton3_Click(sender As Object, e As EventArgs) Handles XenonButton3.Click

        If SaveFileDialog1.ShowDialog = DialogResult.OK Then
            Dim sets As New XeSettings(XeSettings.Mode.Empty)

            With sets
                .AutoAddExt = XenonCheckBox1.Checked
                .DragAndDropPreview = XenonCheckBox3.Checked
                .OpeningPreviewInApp_or_AppliesIt = XenonRadioButton1.Checked
                .AutoRestartExplorer = XenonCheckBox2.Checked
                .AutoUpdatesChecking = XenonCheckBox5.Checked
                .CustomPreviewConfig_Enabled = XenonCheckBox4.Checked
                .CustomPreviewConfig = XenonComboBox1.SelectedIndex
                .UpdateChannel = XenonComboBox2.SelectedIndex
                .Appearance_Dark = XenonRadioButton3.Checked
                .Appearance_Auto = XenonCheckBox6.Checked

                .Save(XeSettings.Mode.File, SaveFileDialog1.FileName)
            End With

        End If

    End Sub

    Private Sub XenonButton4_Click(sender As Object, e As EventArgs) Handles XenonButton4.Click
        If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
            Dim sets As New XeSettings(XeSettings.Mode.File, OpenFileDialog1.FileName)

            With sets
                XenonCheckBox1.Checked = .AutoAddExt
                XenonCheckBox3.Checked = .DragAndDropPreview
                XenonRadioButton1.Checked = .OpeningPreviewInApp_or_AppliesIt
                XenonRadioButton2.Checked = Not .OpeningPreviewInApp_or_AppliesIt

                XenonCheckBox2.Checked = .AutoRestartExplorer
                XenonCheckBox5.Checked = .AutoUpdatesChecking
                XenonCheckBox4.Checked = .CustomPreviewConfig_Enabled
                XenonComboBox1.SelectedIndex = If(.CustomPreviewConfig = .WinVer.Eleven, 0, 1)
                XenonComboBox2.SelectedIndex = If(.UpdateChannel = .UpdateChannels.Stable, 0, 1)

                XenonRadioButton3.Checked = .Appearance_Dark
                XenonRadioButton4.Checked = Not .Appearance_Dark
                XenonCheckBox6.Checked = .Appearance_Auto
            End With
        End If
    End Sub

    Private Sub Me_DragEnter(ByVal sender As Object, ByVal e As DragEventArgs) Handles Me.DragEnter
        Dim files() As String = e.Data.GetData(DataFormats.FileDrop)

        If My.Computer.FileSystem.GetFileInfo(files(0)).Extension.ToLower = ".wpsf" Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub MainFrm_DragDrop(sender As Object, e As DragEventArgs) Handles Me.DragDrop
        Dim files() As String = e.Data.GetData(DataFormats.FileDrop)

        Dim sets As New XeSettings(XeSettings.Mode.File, files(0))

        With sets
            XenonCheckBox1.Checked = .AutoAddExt
            XenonCheckBox3.Checked = .DragAndDropPreview
            XenonRadioButton1.Checked = .OpeningPreviewInApp_or_AppliesIt
            XenonRadioButton2.Checked = Not .OpeningPreviewInApp_or_AppliesIt

            XenonCheckBox2.Checked = .AutoRestartExplorer
            XenonCheckBox5.Checked = .AutoUpdatesChecking
            XenonCheckBox4.Checked = .CustomPreviewConfig_Enabled
            XenonComboBox1.SelectedIndex = If(.CustomPreviewConfig = .WinVer.Eleven, 0, 1)
            XenonComboBox2.SelectedIndex = If(.UpdateChannel = .UpdateChannels.Stable, 0, 1)

            XenonRadioButton3.Checked = .Appearance_Dark
            XenonRadioButton4.Checked = Not .Appearance_Dark
            XenonCheckBox6.Checked = .Appearance_Auto
        End With

        OpenFileDialog1.FileName = files(0)
    End Sub

    Private Sub XenonButton5_Click(sender As Object, e As EventArgs) Handles XenonButton5.Click
        If MsgBox("Are you sure from removing files association (*.wpth, *.wpsf) from registry?" & vbCrLf & vbCrLf & "Note: You can reassociate them by activating its checkbox and restarting the application.", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            XenonCheckBox1.Checked = False
            My.Application.DeleteFileAssociation(".wpth", "WinPaletter.ThemeFile")
            My.Application.DeleteFileAssociation(".wpsf", "WinPaletter.SettingsFile")
        End If
    End Sub

    Private Sub XenonButton6_Click(sender As Object, e As EventArgs) Handles XenonButton6.Click
        If MsgBox("Are you sure from Uninstalling the program?" & vbCrLf & vbCrLf & "This will delete associated files extensions from registry and the application itself.", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            My.Application.DeleteFileAssociation(".wpth", "WinPaletter.ThemeFile")
            My.Application.DeleteFileAssociation(".wpsf", "WinPaletter.SettingsFile")

            Dim prc As New Process
            prc.StartInfo.FileName = "cmd.exe"
            prc.StartInfo.Arguments = "/C choice /C Y /N /D Y /T 0 & Del " & """" & Application.ExecutablePath & """"
            prc.StartInfo.CreateNoWindow = True
            prc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
            prc.Start()

            Application.[Exit]()
        End If
    End Sub
End Class