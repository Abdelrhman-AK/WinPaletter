Imports Microsoft.Win32
Imports WinPaletter.XenonCore

Public Class SettingsX

    Public _External As Boolean = False
    Public _File As String = Nothing
    Dim Changed As Boolean = False

    Private Sub XenonButton2_Click(sender As Object, e As EventArgs) Handles XenonButton2.Click
        Me.Close()
    End Sub

    Private Sub SettingsX_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        XenonComboBox2.Items.Clear()
        XenonComboBox2.Items.Add(My.Application.LanguageHelper.Stable)
        XenonComboBox2.Items.Add(My.Application.LanguageHelper.Beta)
        ApplyDarkMode(Me)
        LoadSettings()
    End Sub

    Protected Overrides Sub OnFormClosing(ByVal e As FormClosingEventArgs)
        Dim NewSets As New XeSettings(XeSettings.Mode.Empty)

        Changed = False

        With My.Application._Settings
            If .AutoAddExt <> XenonCheckBox1.Checked Then Changed = True
            If .DragAndDropPreview <> XenonCheckBox3.Checked Then Changed = True
            If .OpeningPreviewInApp_or_AppliesIt <> XenonRadioButton1.Checked Then Changed = True
            If .AutoRestartExplorer <> XenonCheckBox2.Checked Then Changed = True
            If .AutoApplyCursors <> XenonCheckBox7.Checked Then Changed = True
            If .AutoUpdatesChecking <> XenonCheckBox5.Checked Then Changed = True
            If .UpdateChannel <> XenonComboBox2.SelectedIndex Then Changed = True
            If .Win7LivePreview <> XenonCheckBox9.Checked Then Changed = True
            If .ShowSaveConfirmation <> XenonCheckBox17.Checked Then Changed = True
            If .Appearance_Dark <> XenonRadioButton3.Checked Then Changed = True
            If .Appearance_Auto <> XenonCheckBox6.Checked Then Changed = True
            If .Language <> XenonCheckBox8.Checked Then Changed = True
            If .Language_File <> XenonTextBox3.Text Then Changed = True
            If .Nerd_Stats <> XenonCheckBox10.Checked Then Changed = True
            If .Nerd_Stats_Kind <> XenonComboBox3.SelectedIndex Then Changed = True
            If .Nerd_Stats_HexHash <> XenonCheckBox11.Checked Then Changed = True
            If .Terminal_Bypass <> XenonCheckBox12.Checked Then Changed = True
            If .Terminal_OtherFonts <> XenonCheckBox13.Checked Then Changed = True
            If .LoadThemeFileAsLegacy <> XenonCheckBox16.Checked Then Changed = True
            If .SaveThemeFileAsLegacy <> XenonCheckBox4.Checked Then Changed = True
            If .Terminal_Path_Deflection <> XenonCheckBox14.Checked Then Changed = True
            If .Terminal_Stable_Path <> XenonTextBox1.Text Then Changed = True
            If .Terminal_Preview_Path <> XenonTextBox2.Text Then Changed = True
            If .CMD_OverrideUserPreferences <> XenonCheckBox15.Checked Then Changed = True
        End With

        If e.CloseReason = CloseReason.UserClosing And Changed Then
            Select Case MsgBox(My.Application.LanguageHelper.SaveMsg, MsgBoxStyle.Question + MsgBoxStyle.YesNoCancel + My.Application.MsgboxRt)
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
            XenonCheckBox7.Checked = .AutoApplyCursors
            XenonCheckBox5.Checked = .AutoUpdatesChecking
            XenonCheckBox9.Checked = .Win7LivePreview

            XenonComboBox2.SelectedIndex = If(.UpdateChannel = .UpdateChannels.Stable, 0, 1)
            XenonCheckBox17.Checked = .ShowSaveConfirmation

            XenonRadioButton3.Checked = .Appearance_Dark
            XenonRadioButton4.Checked = Not .Appearance_Dark
            XenonCheckBox6.Checked = .Appearance_Auto

            XenonCheckBox16.Checked = .LoadThemeFileAsLegacy
            XenonCheckBox4.Checked = .SaveThemeFileAsLegacy

            XenonCheckBox8.Checked = .Language
            XenonTextBox3.Text = .Language_File

            XenonCheckBox10.Checked = .Nerd_Stats
            XenonCheckBox11.Checked = .Nerd_Stats_HexHash
            XenonCheckBox12.Checked = .Terminal_Bypass
            XenonCheckBox13.Checked = .Terminal_OtherFonts
            XenonCheckBox14.Checked = .Terminal_Path_Deflection
            XenonTextBox1.Text = .Terminal_Stable_Path
            XenonTextBox2.Text = .Terminal_Preview_Path
            XenonCheckBox15.Checked = .CMD_OverrideUserPreferences

            Select Case .Nerd_Stats_Kind
                Case XeSettings.Nerd_Stats_Type.HEX
                    XenonComboBox3.SelectedIndex = 0
                Case XeSettings.Nerd_Stats_Type.RGB
                    XenonComboBox3.SelectedIndex = 1
                Case XeSettings.Nerd_Stats_Type.HSL
                    XenonComboBox3.SelectedIndex = 2
                Case XeSettings.Nerd_Stats_Type.Dec
                    XenonComboBox3.SelectedIndex = 3
            End Select

        End With

        With My.Application.LanguageHelper
            Label11.Text = .Name
            Label12.Text = .TrVer
            Label14.Text = .AppVer
            Label19.Text = .Lang
            Label16.Text = .LangCode
            Label21.Text = If(.RightToLeft, .Yes, .No)
        End With

        If _External Then OpenFileDialog1.FileName = _File
        XenonTextBox3.Text = My.Application._Settings.Language_File
    End Sub

    Sub SaveSettings()
        Cursor = Cursors.WaitCursor

        'Ch = Change
        Dim ch_dark As Boolean = False
        Dim ch_nerd As Boolean = False
        Dim ch_terminal As Boolean = False

        With My.Application._Settings
            If .Appearance_Dark <> XenonRadioButton3.Checked Then ch_dark = True
            If .Appearance_Auto <> XenonCheckBox6.Checked Then ch_dark = True

            If .Nerd_Stats <> XenonCheckBox10.Checked Then ch_nerd = True
            If .Nerd_Stats_Kind <> XenonComboBox3.SelectedIndex Then ch_nerd = True
            If .Nerd_Stats_HexHash <> XenonCheckBox11.Checked Then ch_nerd = True

            If .Terminal_Path_Deflection <> XenonCheckBox14.Checked Then ch_terminal = True
            If .Terminal_Stable_Path <> XenonTextBox1.Text Then ch_terminal = True
            If .Terminal_Preview_Path <> XenonTextBox2.Text Then ch_terminal = True

            .AutoAddExt = XenonCheckBox1.Checked
            .DragAndDropPreview = XenonCheckBox3.Checked
            .OpeningPreviewInApp_or_AppliesIt = XenonRadioButton1.Checked
            .AutoRestartExplorer = XenonCheckBox2.Checked
            .AutoApplyCursors = XenonCheckBox7.Checked
            .AutoUpdatesChecking = XenonCheckBox5.Checked
            .Win7LivePreview = XenonCheckBox9.Checked
            .UpdateChannel = XenonComboBox2.SelectedIndex
            .Appearance_Dark = XenonRadioButton3.Checked
            .Appearance_Auto = XenonCheckBox6.Checked
            .ShowSaveConfirmation = XenonCheckBox17.Checked

            .LoadThemeFileAsLegacy = XenonCheckBox16.Checked
            .SaveThemeFileAsLegacy = XenonCheckBox4.Checked

            .Language = XenonCheckBox8.Checked
            .Language_File = XenonTextBox3.Text
            .Nerd_Stats = XenonCheckBox10.Checked
            .Nerd_Stats_Kind = XenonComboBox3.SelectedIndex
            .Nerd_Stats_HexHash = XenonCheckBox11.Checked
            .Terminal_Bypass = XenonCheckBox12.Checked
            .Terminal_OtherFonts = XenonCheckBox13.Checked
            .Terminal_Path_Deflection = XenonCheckBox14.Checked
            .Terminal_Stable_Path = XenonTextBox1.Text
            .Terminal_Preview_Path = XenonTextBox2.Text
            .CMD_OverrideUserPreferences = XenonCheckBox15.Checked

            .Save(XeSettings.Mode.Registry)
        End With

        If ch_dark Then ApplyDarkMode()

        If ch_nerd Then
            For ix As Integer = Application.OpenForms.Count - 1 To 0 Step -1
                Application.OpenForms(ix).Refresh()
            Next
        End If

        If ch_terminal Then
            If My.W10 Or My.W11 Then
                Dim TerDir As String
                Dim TerPreDir As String

                If Not My.Application._Settings.Terminal_Path_Deflection Then
                    TerDir = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) & "\AppData\Local\Packages\Microsoft.WindowsTerminal_8wekyb3d8bbwe\LocalState\settings.json"
                    TerPreDir = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) & "\AppData\Local\Packages\Microsoft.WindowsTerminalPreview_8wekyb3d8bbwe\LocalState\settings.json"
                Else
                    If IO.File.Exists(My.Application._Settings.Terminal_Stable_Path) Then
                        TerDir = My.Application._Settings.Terminal_Stable_Path
                    Else
                        TerDir = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) & "\AppData\Local\Packages\Microsoft.WindowsTerminal_8wekyb3d8bbwe\LocalState\settings.json"
                    End If

                    If IO.File.Exists(My.Application._Settings.Terminal_Preview_Path) Then
                        TerPreDir = My.Application._Settings.Terminal_Preview_Path
                    Else
                        TerPreDir = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) & "\AppData\Local\Packages\Microsoft.WindowsTerminalPreview_8wekyb3d8bbwe\LocalState\settings.json"
                    End If
                End If


                If IO.File.Exists(TerDir) Then
                    MainFrm.CP.Terminal = New WinTerminal(TerDir, WinTerminal.Mode.JSONFile)
                Else
                    MainFrm.CP.Terminal = New WinTerminal("", WinTerminal.Mode.Empty)
                End If

                If IO.File.Exists(TerPreDir) Then
                    MainFrm.CP.TerminalPreview = New WinTerminal(TerPreDir, WinTerminal.Mode.JSONFile, WinTerminal.Version.Preview)
                Else
                    MainFrm.CP.TerminalPreview = New WinTerminal("", WinTerminal.Mode.Empty, WinTerminal.Version.Preview)
                End If

            Else
                MainFrm.CP.Terminal = New WinTerminal("", WinTerminal.Mode.Empty)
                MainFrm.CP.TerminalPreview = New WinTerminal("", WinTerminal.Mode.Empty, WinTerminal.Version.Preview)
            End If
        End If


        Cursor = Cursors.Default

        MsgBox(My.Application.LanguageHelper.SettingsSaved, MsgBoxStyle.Information + My.Application.MsgboxRt)
        Me.Close()
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
                .AutoApplyCursors = XenonCheckBox7.Checked
                .AutoUpdatesChecking = XenonCheckBox5.Checked
                .Win7LivePreview = XenonCheckBox9.Checked
                .UpdateChannel = XenonComboBox2.SelectedIndex
                .Appearance_Dark = XenonRadioButton3.Checked
                .Appearance_Auto = XenonCheckBox6.Checked
                .ShowSaveConfirmation = XenonCheckBox17.Checked

                .LoadThemeFileAsLegacy = XenonCheckBox16.Checked
                .SaveThemeFileAsLegacy = XenonCheckBox4.Checked

                .Language = XenonCheckBox8.Checked
                .Language_File = XenonTextBox3.Text
                .Nerd_Stats = XenonCheckBox10.Checked
                .Nerd_Stats_Kind = XenonComboBox3.SelectedIndex
                .Nerd_Stats_HexHash = XenonCheckBox11.Checked
                .Terminal_Bypass = XenonCheckBox12.Checked
                .Terminal_OtherFonts = XenonCheckBox13.Checked
                .Terminal_Path_Deflection = XenonCheckBox14.Checked
                .Terminal_Stable_Path = XenonTextBox1.Text
                .Terminal_Preview_Path = XenonTextBox2.Text
                .CMD_OverrideUserPreferences = XenonCheckBox15.Checked

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
                XenonCheckBox7.Checked = .AutoApplyCursors
                XenonCheckBox5.Checked = .AutoUpdatesChecking
                XenonCheckBox9.Checked = .Win7LivePreview

                XenonCheckBox16.Checked = .LoadThemeFileAsLegacy
                XenonCheckBox4.Checked = .SaveThemeFileAsLegacy
                XenonCheckBox17.Checked = .ShowSaveConfirmation

                XenonCheckBox12.Checked = .Terminal_Bypass
                XenonCheckBox13.Checked = .Terminal_OtherFonts
                XenonCheckBox14.Checked = .Terminal_Path_Deflection
                XenonTextBox1.Text = .Terminal_Stable_Path
                XenonTextBox2.Text = .Terminal_Preview_Path
                XenonCheckBox15.Checked = .CMD_OverrideUserPreferences

                XenonComboBox2.SelectedIndex = If(.UpdateChannel = .UpdateChannels.Stable, 0, 1)

                XenonCheckBox10.Checked = .Nerd_Stats
                XenonCheckBox11.Checked = .Nerd_Stats_HexHash
                Select Case .Nerd_Stats_Kind
                    Case XeSettings.Nerd_Stats_Type.HEX
                        XenonComboBox3.SelectedIndex = 0
                    Case XeSettings.Nerd_Stats_Type.RGB
                        XenonComboBox3.SelectedIndex = 1
                    Case XeSettings.Nerd_Stats_Type.HSL
                        XenonComboBox3.SelectedIndex = 2
                    Case XeSettings.Nerd_Stats_Type.Dec
                        XenonComboBox3.SelectedIndex = 3
                End Select

                XenonRadioButton3.Checked = .Appearance_Dark
                XenonRadioButton4.Checked = Not .Appearance_Dark
                XenonCheckBox6.Checked = .Appearance_Auto
                XenonCheckBox8.Checked = .Language
                XenonTextBox3.Text = .Language_File
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
            XenonCheckBox7.Checked = .AutoApplyCursors
            XenonCheckBox5.Checked = .AutoUpdatesChecking
            XenonCheckBox9.Checked = .Win7LivePreview

            XenonCheckBox16.Checked = .LoadThemeFileAsLegacy
            XenonCheckBox4.Checked = .SaveThemeFileAsLegacy
            XenonCheckBox17.Checked = .ShowSaveConfirmation

            XenonCheckBox12.Checked = .Terminal_Bypass
            XenonCheckBox13.Checked = .Terminal_OtherFonts
            XenonCheckBox14.Checked = .Terminal_Path_Deflection
            XenonTextBox1.Text = .Terminal_Stable_Path
            XenonTextBox2.Text = .Terminal_Preview_Path
            XenonCheckBox15.Checked = .CMD_OverrideUserPreferences

            XenonComboBox2.SelectedIndex = If(.UpdateChannel = .UpdateChannels.Stable, 0, 1)

            XenonCheckBox10.Checked = .Nerd_Stats
            XenonCheckBox11.Checked = .Nerd_Stats_HexHash
            Select Case .Nerd_Stats_Kind
                Case XeSettings.Nerd_Stats_Type.HEX
                    XenonComboBox3.SelectedIndex = 0
                Case XeSettings.Nerd_Stats_Type.RGB
                    XenonComboBox3.SelectedIndex = 1
                Case XeSettings.Nerd_Stats_Type.HSL
                    XenonComboBox3.SelectedIndex = 2
                Case XeSettings.Nerd_Stats_Type.Dec
                    XenonComboBox3.SelectedIndex = 3
            End Select

            XenonRadioButton3.Checked = .Appearance_Dark
            XenonRadioButton4.Checked = Not .Appearance_Dark
            XenonCheckBox6.Checked = .Appearance_Auto
            XenonCheckBox8.Checked = .Language
            XenonTextBox3.Text = .Language_File
        End With

        OpenFileDialog1.FileName = files(0)
    End Sub

    Private Sub XenonButton5_Click(sender As Object, e As EventArgs) Handles XenonButton5.Click
        If MsgBox(My.Application.LanguageHelper.RemoveExtMsg & vbCrLf & vbCrLf & My.Application.LanguageHelper.RemoveExtMsgNote, MsgBoxStyle.Question + MsgBoxStyle.YesNo _
                  + My.Application.MsgboxRt) = MsgBoxResult.Yes Then

            XenonCheckBox1.Checked = False
            My.Application.DeleteFileAssociation(".wpth", "WinPaletter.ThemeFile")
            My.Application.DeleteFileAssociation(".wpsf", "WinPaletter.SettingsFile")
        End If
    End Sub

    Private Sub XenonButton6_Click(sender As Object, e As EventArgs) Handles XenonButton6.Click
        If MsgBox(My.Application.LanguageHelper.UninstallMsgLine1 & vbCrLf & vbCrLf & My.Application.LanguageHelper.UninstallMsgLine2, MsgBoxStyle.Question + MsgBoxStyle.YesNo _
                  + My.Application.MsgboxRt) = MsgBoxResult.Yes Then

            My.Application.DeleteFileAssociation(".wpth", "WinPaletter.ThemeFile")
            My.Application.DeleteFileAssociation(".wpsf", "WinPaletter.SettingsFile")

            Try : Registry.CurrentUser.DeleteSubKeyTree("Software\WinPaletter", True) : Catch : End Try

            IO.Directory.Delete(My.Application.appData, True)

            Application.[Exit]()
        End If
    End Sub

    Private Sub XenonButton7_Click(sender As Object, e As EventArgs) Handles XenonButton7.Click

        If OpenFileDialog2.ShowDialog = DialogResult.OK Then
            XenonTextBox3.Text = OpenFileDialog2.FileName
            MsgBox(My.Application.LanguageHelper.LanguageRestart, MsgBoxStyle.Information + My.Application.MsgboxRt)
        End If

    End Sub

    Private Sub XenonButton8_Click(sender As Object, e As EventArgs) Handles XenonButton8.Click
        Process.Start(My.Resources.Link_Repository & "blob/master/TranslationContribution.md")
    End Sub

    Private Sub XenonButton16_Click(sender As Object, e As EventArgs) Handles XenonButton16.Click
        If OpenJSONDlg.ShowDialog = DialogResult.OK Then
            XenonTextBox1.Text = OpenJSONDlg.FileName
        End If
    End Sub

    Private Sub XenonButton9_Click(sender As Object, e As EventArgs) Handles XenonButton9.Click
        If OpenJSONDlg.ShowDialog = DialogResult.OK Then
            XenonTextBox2.Text = OpenJSONDlg.FileName
        End If
    End Sub

    Private Sub XenonButton10_Click(sender As Object, e As EventArgs) Handles XenonButton10.Click
        Process.Start(My.Resources.Link_Repository & "tree/master/Languages")
    End Sub
End Class