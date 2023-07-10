Imports System.IO
Imports Newtonsoft.Json.Linq
Imports WinPaletter.XenonCore

Public Class SettingsX
    Public _External As Boolean = False
    Public _File As String = Nothing
    Dim Changed As Boolean = False


    Sub LoadSettings()
        Dim sets As XeSettings

        If Not _External Then sets = My.Settings Else sets = New XeSettings(XeSettings.Mode.File, _File)
        Read(sets)

        With My.Lang
            Label11.Text = .Name
            Label12.Text = .TranslationVersion
            Label14.Text = .AppVer & " " & My.Lang.AndBelow
            Label19.Text = .Lang
            Label16.Text = .LangCode
            Label22.Text = If(Not .RightToLeft, My.Lang.Lang_HasLeftToRight, My.Lang.Lang_HasRightToLeft)
        End With

        If _External Then OpenFileDialog1.FileName = _File
        XenonTextBox3.Text = My.Settings.Language.File
    End Sub
    Sub Read(Sets As XeSettings)
        With Sets
            XenonCheckBox1.Checked = .FileTypeManagement.AutoAddExt

            XenonRadioButton1.Checked = .FileTypeManagement.OpeningPreviewInApp_or_AppliesIt
            XenonRadioButton2.Checked = Not .FileTypeManagement.OpeningPreviewInApp_or_AppliesIt

            XenonCheckBox2.Checked = .ThemeApplyingBehavior.AutoRestartExplorer
            XenonCheckBox7.Checked = .ThemeApplyingBehavior.AutoApplyCursors
            XenonCheckBox16.Checked = .ThemeApplyingBehavior.ResetCursorsToAero

            XenonCheckBox5.Checked = .Updates.AutoCheck
            XenonCheckBox9.Checked = .Miscellaneous.Win7LivePreview

            XenonComboBox2.SelectedIndex = If(.Updates.Channel = XeSettings.Structures.Updates.Channels.Stable, 0, 1)
            XenonCheckBox17.Checked = .ThemeApplyingBehavior.ShowSaveConfirmation
            XenonCheckBox33.Checked = .FileTypeManagement.CompressThemeFile

            XenonRadioButton3.Checked = .Appearance.DarkMode
            XenonRadioButton4.Checked = Not .Appearance.DarkMode
            XenonCheckBox6.Checked = .Appearance.AutoDarkMode
            XenonCheckBox30.Checked = .Appearance.ManagedByTheme

            XenonCheckBox8.Checked = .Language.Enabled
            XenonTextBox3.Text = .Language.File

            XenonCheckBox10.Checked = .NerdStats.Enabled
            XenonCheckBox11.Checked = .NerdStats.ShowHexHash
            XenonCheckBox12.Checked = .WindowsTerminals.Bypass
            XenonCheckBox13.Checked = .WindowsTerminals.ListAllFonts
            XenonCheckBox14.Checked = .WindowsTerminals.Path_Deflection
            XenonTextBox1.Text = .WindowsTerminals.Terminal_Stable_Path
            XenonTextBox2.Text = .WindowsTerminals.Terminal_Preview_Path
            XenonCheckBox15.Checked = .ThemeApplyingBehavior.CMD_OverrideUserPreferences

            Select Case .NerdStats.Type
                Case XeSettings.Structures.NerdStats.Formats.HEX
                    XenonComboBox3.SelectedIndex = 0
                Case XeSettings.Structures.NerdStats.Formats.RGB
                    XenonComboBox3.SelectedIndex = 1
                Case XeSettings.Structures.NerdStats.Formats.HSL
                    XenonComboBox3.SelectedIndex = 2
                Case XeSettings.Structures.NerdStats.Formats.Dec
                    XenonComboBox3.SelectedIndex = 3
            End Select

            XenonCheckBox19.Checked = .ThemeLog.Enabled
            XenonCheckBox18.Checked = .ThemeLog.CountDown
            XenonNumericUpDown1.Value = .ThemeLog.CountDown_Seconds

            XenonCheckBox20.Checked = .ExplorerPatcher.Enabled
            XenonCheckBox21.Checked = .ExplorerPatcher.Enabled_Force
            EP_Start_10.Checked = .ExplorerPatcher.UseStart10
            EP_Start_11.Checked = Not .ExplorerPatcher.UseStart10
            EP_Start_10_Type.SelectedIndex = .ExplorerPatcher.StartStyle
            EP_Taskbar_10.Checked = .ExplorerPatcher.UseTaskbar10
            EP_Taskbar_11.Checked = Not .ExplorerPatcher.UseTaskbar10
            EP_ORB_10.Checked = .ExplorerPatcher.TaskbarButton10
            EP_ORB_11.Checked = Not .ExplorerPatcher.TaskbarButton10

            XenonCheckBox22.Checked = .ThemeApplyingBehavior.DelayMetrics

            XenonRadioButton5.Checked = .ThemeApplyingBehavior.ClassicColors_HKU_DEFAULT_Prefs = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite
            XenonRadioButton6.Checked = Not (.ThemeApplyingBehavior.ClassicColors_HKU_DEFAULT_Prefs = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite)
            XenonRadioButton8.Checked = .ThemeApplyingBehavior.ClassicColors_HKLM_Prefs = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite
            XenonRadioButton10.Checked = .ThemeApplyingBehavior.ClassicColors_HKLM_Prefs = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.DontChange
            XenonRadioButton9.Checked = .ThemeApplyingBehavior.ClassicColors_HKLM_Prefs = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.RestoreDefaults
            XenonRadioButton7.Checked = .ThemeApplyingBehavior.ClassicColors_HKLM_Prefs = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Erase
            XenonCheckBox25.Checked = .ThemeApplyingBehavior.UPM_HKU_DEFAULT
            XenonRadioButton12.Checked = .ThemeApplyingBehavior.Metrics_HKU_DEFAULT_Prefs = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite
            XenonRadioButton11.Checked = Not (.ThemeApplyingBehavior.Metrics_HKU_DEFAULT_Prefs = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite)
            XenonRadioButton14.Checked = .ThemeApplyingBehavior.Cursors_HKU_DEFAULT_Prefs = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite
            XenonRadioButton13.Checked = Not (.ThemeApplyingBehavior.Cursors_HKU_DEFAULT_Prefs = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite)
            XenonRadioButton16.Checked = .ThemeApplyingBehavior.CMD_HKU_DEFAULT_Prefs = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite
            XenonRadioButton15.Checked = Not (.ThemeApplyingBehavior.CMD_HKU_DEFAULT_Prefs = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite)
            XenonRadioButton18.Checked = .ThemeApplyingBehavior.PS86_HKU_DEFAULT_Prefs = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite
            XenonRadioButton17.Checked = Not (.ThemeApplyingBehavior.PS86_HKU_DEFAULT_Prefs = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite)
            XenonRadioButton20.Checked = .ThemeApplyingBehavior.PS64_HKU_DEFAULT_Prefs = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite
            XenonRadioButton19.Checked = Not (.ThemeApplyingBehavior.PS64_HKU_DEFAULT_Prefs = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite)
            XenonRadioButton22.Checked = .ThemeApplyingBehavior.Desktop_HKU_DEFAULT = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite
            XenonRadioButton23.Checked = .ThemeApplyingBehavior.Desktop_HKU_DEFAULT = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.RestoreDefaults
            XenonRadioButton21.Checked = .ThemeApplyingBehavior.Desktop_HKU_DEFAULT = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.DontChange

            Label38.Text = CalcStoreCache().SizeString
            Label43.Text = CalcThemesResCache().SizeString

            XenonRadioImage1.Checked = .Store.Online_or_Offline
            XenonRadioImage2.Checked = Not .Store.Online_or_Offline
            ListBox1.Items.Clear()
            For Each x In .Store.Online_Repositories
                If Not String.IsNullOrWhiteSpace(x) Then ListBox1.Items.Add(x)
            Next
            ListBox2.Items.Clear()
            For Each x In .Store.Offline_Directories
                If Not String.IsNullOrWhiteSpace(x) Then ListBox2.Items.Add(x)
            Next

            XenonCheckBox28.Checked = .Store.Search_ThemeNames
            XenonCheckBox26.Checked = .Store.Search_Descriptions
            XenonCheckBox27.Checked = .Store.Search_AuthorsNames
            XenonCheckBox29.Checked = .Store.Offline_SubFolders
            XenonCheckBox4.Checked = .Store.ShowTips

            XenonCheckBox32.Checked = .Miscellaneous.Classic_Color_Picker
        End With
    End Sub
    Sub SaveSettings()
        Cursor = Cursors.WaitCursor

        'Ch = Change
        Dim ch_nerd As Boolean = False
        Dim ch_terminal As Boolean = False
        Dim ch_lang As Boolean = False
        Dim ch_appearance As Boolean = False
        Dim ch_EP As Boolean = False

        With My.Settings
            If .Appearance.DarkMode <> XenonRadioButton3.Checked Then ch_appearance = True
            If .Appearance.AutoDarkMode <> XenonCheckBox6.Checked Then ch_appearance = True
            If .Appearance.ManagedByTheme <> XenonCheckBox30.Checked Then ch_appearance = True

            If .NerdStats.Enabled <> XenonCheckBox10.Checked Then ch_nerd = True
            If .NerdStats.Type <> XenonComboBox3.SelectedIndex Then ch_nerd = True
            If .NerdStats.ShowHexHash <> XenonCheckBox11.Checked Then ch_nerd = True

            If .WindowsTerminals.Path_Deflection <> XenonCheckBox14.Checked Then ch_terminal = True
            If .WindowsTerminals.Terminal_Stable_Path <> XenonTextBox1.Text Then ch_terminal = True
            If .WindowsTerminals.Terminal_Preview_Path <> XenonTextBox2.Text Then ch_terminal = True

            If .Language.Enabled <> XenonCheckBox8.Checked Then ch_lang = True
            If .Language.File <> XenonTextBox3.Text Then ch_lang = True

            If .ExplorerPatcher.Enabled <> XenonCheckBox20.Checked Then ch_EP = True
            If .ExplorerPatcher.Enabled_Force <> XenonCheckBox21.Checked Then ch_EP = True
            If .ExplorerPatcher.UseStart10 <> EP_Start_10.Checked Then ch_EP = True
            If .ExplorerPatcher.StartStyle <> EP_Start_10_Type.SelectedIndex Then ch_EP = True
            If .ExplorerPatcher.UseTaskbar10 <> EP_Taskbar_10.Checked Then ch_EP = True
            If .ExplorerPatcher.TaskbarButton10 <> EP_ORB_10.Checked Then ch_EP = True
        End With

        Write(My.Settings, XeSettings.Mode.Registry)

        If ch_appearance Then ApplyDarkMode()

        If ch_nerd Then
            For ix As Integer = Application.OpenForms.Count - 1 To 0 Step -1
                Application.OpenForms(ix).Refresh()
            Next
        End If

        If ch_terminal Then
            If My.W10 Or My.W11 Then
                Dim TerDir As String
                Dim TerPreDir As String

                If Not My.Settings.WindowsTerminals.Path_Deflection Then
                    TerDir = My.PATH_TerminalJSON
                    TerPreDir = My.PATH_TerminalPreviewJSON
                Else
                    If IO.File.Exists(My.Settings.WindowsTerminals.Terminal_Stable_Path) Then
                        TerDir = My.Settings.WindowsTerminals.Terminal_Stable_Path
                    Else
                        TerDir = My.PATH_TerminalJSON
                    End If

                    If IO.File.Exists(My.Settings.WindowsTerminals.Terminal_Preview_Path) Then
                        TerPreDir = My.Settings.WindowsTerminals.Terminal_Preview_Path
                    Else
                        TerPreDir = My.PATH_TerminalPreviewJSON
                    End If
                End If


                If IO.File.Exists(TerDir) Then
                    My.CP.Terminal = New WinTerminal(TerDir, WinTerminal.Mode.JSONFile)
                Else
                    My.CP.Terminal = New WinTerminal("", WinTerminal.Mode.Empty)
                End If

                If IO.File.Exists(TerPreDir) Then
                    My.CP.TerminalPreview = New WinTerminal(TerPreDir, WinTerminal.Mode.JSONFile, WinTerminal.Version.Preview)
                Else
                    My.CP.TerminalPreview = New WinTerminal("", WinTerminal.Mode.Empty, WinTerminal.Version.Preview)
                End If

            Else
                My.CP.Terminal = New WinTerminal("", WinTerminal.Mode.Empty)
                My.CP.TerminalPreview = New WinTerminal("", WinTerminal.Mode.Empty, WinTerminal.Version.Preview)
            End If
        End If

        If ch_lang Then
            If XenonCheckBox8.Checked Then
                My.Lang = New Localizer
                My.Lang.LoadLanguageFromJSON(My.Settings.Language.File)
                For Each f As Form In My.Application.OpenForms
                    f.LoadLanguage
                Next
                MainFrm.UpdateLegends()
            Else
                MsgBox(My.Lang.LanguageRestart, MsgBoxStyle.Information)
            End If
        End If

        If ch_EP Then
            My.EP = New ExplorerPatcher
            MainFrm.ApplyColorsToElements(My.CP)
            MainFrm.ApplyCPValues(My.CP)
            MainFrm.ApplyStylesToElements(My.CP, False)
            PreviewHelpers.ReValidateLivePreview(MainFrm.pnl_preview)
        End If

        Cursor = Cursors.Default

        MsgBox(My.Lang.SettingsSaved, MsgBoxStyle.Information)
    End Sub
    Sub Write(Sets As XeSettings, Mode As XeSettings.Mode, Optional File As String = "")
        With Sets
            .FileTypeManagement.AutoAddExt = XenonCheckBox1.Checked
            .FileTypeManagement.OpeningPreviewInApp_or_AppliesIt = XenonRadioButton1.Checked
            .ThemeApplyingBehavior.AutoRestartExplorer = XenonCheckBox2.Checked
            .ThemeApplyingBehavior.AutoApplyCursors = XenonCheckBox7.Checked
            .ThemeApplyingBehavior.ResetCursorsToAero = XenonCheckBox16.Checked

            .Updates.AutoCheck = XenonCheckBox5.Checked
            .Miscellaneous.Win7LivePreview = XenonCheckBox9.Checked
            .Updates.Channel = XenonComboBox2.SelectedIndex

            .Appearance.DarkMode = XenonRadioButton3.Checked
            .Appearance.AutoDarkMode = XenonCheckBox6.Checked
            .Appearance.ManagedByTheme = XenonCheckBox30.Checked

            .ThemeApplyingBehavior.ShowSaveConfirmation = XenonCheckBox17.Checked
            .FileTypeManagement.CompressThemeFile = XenonCheckBox33.Checked

            .Language.Enabled = XenonCheckBox8.Checked
            .Language.File = XenonTextBox3.Text
            .NerdStats.Enabled = XenonCheckBox10.Checked
            .NerdStats.Type = XenonComboBox3.SelectedIndex
            .NerdStats.ShowHexHash = XenonCheckBox11.Checked
            .WindowsTerminals.Bypass = XenonCheckBox12.Checked
            .WindowsTerminals.ListAllFonts = XenonCheckBox13.Checked
            .WindowsTerminals.Path_Deflection = XenonCheckBox14.Checked
            .WindowsTerminals.Terminal_Stable_Path = XenonTextBox1.Text
            .WindowsTerminals.Terminal_Preview_Path = XenonTextBox2.Text
            .ThemeApplyingBehavior.CMD_OverrideUserPreferences = XenonCheckBox15.Checked

            .ThemeLog.Enabled = XenonCheckBox19.Checked
            .ThemeLog.CountDown = XenonCheckBox18.Checked
            .ThemeLog.CountDown_Seconds = XenonNumericUpDown1.Value

            .ExplorerPatcher.Enabled = XenonCheckBox20.Checked
            .ExplorerPatcher.Enabled_Force = XenonCheckBox21.Checked
            .ExplorerPatcher.UseStart10 = EP_Start_10.Checked
            .ExplorerPatcher.StartStyle = EP_Start_10_Type.SelectedIndex
            .ExplorerPatcher.UseTaskbar10 = EP_Taskbar_10.Checked
            .ExplorerPatcher.TaskbarButton10 = EP_ORB_10.Checked
            .ThemeApplyingBehavior.DelayMetrics = XenonCheckBox22.Checked

            If XenonRadioButton5.Checked Then .ThemeApplyingBehavior.ClassicColors_HKU_DEFAULT_Prefs = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite Else .ThemeApplyingBehavior.ClassicColors_HKU_DEFAULT_Prefs = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.DontChange
            If XenonRadioButton8.Checked Then .ThemeApplyingBehavior.ClassicColors_HKLM_Prefs = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite
            If XenonRadioButton10.Checked Then .ThemeApplyingBehavior.ClassicColors_HKLM_Prefs = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.DontChange
            If XenonRadioButton9.Checked Then .ThemeApplyingBehavior.ClassicColors_HKLM_Prefs = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.RestoreDefaults
            If XenonRadioButton7.Checked Then .ThemeApplyingBehavior.ClassicColors_HKLM_Prefs = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Erase
            .ThemeApplyingBehavior.UPM_HKU_DEFAULT = XenonCheckBox25.Checked
            If XenonRadioButton12.Checked Then .ThemeApplyingBehavior.Metrics_HKU_DEFAULT_Prefs = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite Else .ThemeApplyingBehavior.Metrics_HKU_DEFAULT_Prefs = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.DontChange
            If XenonRadioButton14.Checked Then .ThemeApplyingBehavior.Cursors_HKU_DEFAULT_Prefs = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite Else .ThemeApplyingBehavior.Cursors_HKU_DEFAULT_Prefs = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.DontChange
            If XenonRadioButton16.Checked Then .ThemeApplyingBehavior.CMD_HKU_DEFAULT_Prefs = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite Else .ThemeApplyingBehavior.CMD_HKU_DEFAULT_Prefs = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.DontChange
            If XenonRadioButton18.Checked Then .ThemeApplyingBehavior.PS86_HKU_DEFAULT_Prefs = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite Else .ThemeApplyingBehavior.PS86_HKU_DEFAULT_Prefs = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.DontChange
            If XenonRadioButton20.Checked Then .ThemeApplyingBehavior.PS64_HKU_DEFAULT_Prefs = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite Else .ThemeApplyingBehavior.PS64_HKU_DEFAULT_Prefs = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.DontChange
            If XenonRadioButton22.Checked Then .ThemeApplyingBehavior.Desktop_HKU_DEFAULT = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite
            If XenonRadioButton23.Checked Then .ThemeApplyingBehavior.Desktop_HKU_DEFAULT = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.RestoreDefaults
            If XenonRadioButton21.Checked Then .ThemeApplyingBehavior.Desktop_HKU_DEFAULT = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.DontChange

            .Store.Online_or_Offline = XenonRadioImage1.Checked
            .Store.Online_Repositories = ListBox1.Items.OfType(Of String)().Where(Function(s) Not String.IsNullOrEmpty(s)).ToArray()
            .Store.Offline_Directories = ListBox2.Items.OfType(Of String)().Where(Function(s) Not String.IsNullOrEmpty(s)).ToArray()
            .Store.Search_ThemeNames = XenonCheckBox28.Checked
            .Store.Search_Descriptions = XenonCheckBox26.Checked
            .Store.Search_AuthorsNames = XenonCheckBox27.Checked
            .Store.Offline_SubFolders = XenonCheckBox29.Checked
            .Store.ShowTips = XenonCheckBox4.Checked

            .Miscellaneous.Classic_Color_Picker = XenonCheckBox32.Checked

            .Save(Mode, File)
        End With
    End Sub
    Protected Overrides Sub OnFormClosing(ByVal e As FormClosingEventArgs)
        Dim NewSets As New XeSettings(XeSettings.Mode.Empty)

        Changed = False

        With My.Settings
            If .FileTypeManagement.AutoAddExt <> XenonCheckBox1.Checked Then Changed = True
            If .FileTypeManagement.OpeningPreviewInApp_or_AppliesIt <> XenonRadioButton1.Checked Then Changed = True
            If .ThemeApplyingBehavior.AutoRestartExplorer <> XenonCheckBox2.Checked Then Changed = True
            If .ThemeApplyingBehavior.AutoApplyCursors <> XenonCheckBox7.Checked Then Changed = True
            If .ThemeApplyingBehavior.ResetCursorsToAero <> XenonCheckBox16.Checked Then Changed = True
            If .Updates.AutoCheck <> XenonCheckBox5.Checked Then Changed = True
            If .Updates.Channel <> XenonComboBox2.SelectedIndex Then Changed = True
            If .Miscellaneous.Win7LivePreview <> XenonCheckBox9.Checked Then Changed = True
            If .Miscellaneous.Classic_Color_Picker <> XenonCheckBox32.Checked Then Changed = True
            If .ThemeApplyingBehavior.ShowSaveConfirmation <> XenonCheckBox17.Checked Then Changed = True
            If .FileTypeManagement.CompressThemeFile <> XenonCheckBox33.Checked Then Changed = True

            If .Appearance.DarkMode <> XenonRadioButton3.Checked Then Changed = True
            If .Appearance.AutoDarkMode <> XenonCheckBox6.Checked Then Changed = True
            If .Appearance.ManagedByTheme <> XenonCheckBox30.Checked Then Changed = True

            If .Language.Enabled <> XenonCheckBox8.Checked Then Changed = True
            If .Language.File <> XenonTextBox3.Text Then Changed = True
            If .NerdStats.Enabled <> XenonCheckBox10.Checked Then Changed = True
            If .NerdStats.Type <> XenonComboBox3.SelectedIndex Then Changed = True
            If .NerdStats.ShowHexHash <> XenonCheckBox11.Checked Then Changed = True
            If .WindowsTerminals.Bypass <> XenonCheckBox12.Checked Then Changed = True
            If .WindowsTerminals.ListAllFonts <> XenonCheckBox13.Checked Then Changed = True
            If .WindowsTerminals.Path_Deflection <> XenonCheckBox14.Checked Then Changed = True
            If .WindowsTerminals.Terminal_Stable_Path <> XenonTextBox1.Text Then Changed = True
            If .WindowsTerminals.Terminal_Preview_Path <> XenonTextBox2.Text Then Changed = True
            If .ThemeApplyingBehavior.CMD_OverrideUserPreferences <> XenonCheckBox15.Checked Then Changed = True

            If .ThemeLog.Enabled <> XenonCheckBox19.Checked Then Changed = True
            If .ThemeLog.CountDown <> XenonCheckBox18.Checked Then Changed = True
            If .ThemeLog.CountDown_Seconds <> XenonNumericUpDown1.Value Then Changed = True

            If .ExplorerPatcher.Enabled <> XenonCheckBox20.Checked Then Changed = True
            If .ExplorerPatcher.Enabled_Force <> XenonCheckBox21.Checked Then Changed = True
            If .ExplorerPatcher.UseStart10 <> EP_Start_10.Checked Then Changed = True
            If .ExplorerPatcher.StartStyle <> EP_Start_10_Type.SelectedIndex Then Changed = True
            If .ExplorerPatcher.UseTaskbar10 <> EP_Taskbar_10.Checked Then Changed = True
            If .ExplorerPatcher.TaskbarButton10 <> EP_ORB_10.Checked Then Changed = True

            If .ThemeApplyingBehavior.DelayMetrics <> XenonCheckBox22.Checked Then Changed = True

            If .ThemeApplyingBehavior.ClassicColors_HKU_DEFAULT_Prefs = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite And Not XenonRadioButton5.Checked Then Changed = True
            If .ThemeApplyingBehavior.ClassicColors_HKU_DEFAULT_Prefs = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.DontChange And Not XenonRadioButton6.Checked Then Changed = True

            If .ThemeApplyingBehavior.ClassicColors_HKLM_Prefs = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite And Not XenonRadioButton8.Checked Then Changed = True
            If .ThemeApplyingBehavior.ClassicColors_HKLM_Prefs = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.DontChange And Not XenonRadioButton10.Checked Then Changed = True
            If .ThemeApplyingBehavior.ClassicColors_HKLM_Prefs = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.RestoreDefaults And Not XenonRadioButton9.Checked Then Changed = True
            If .ThemeApplyingBehavior.ClassicColors_HKLM_Prefs = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Erase And Not XenonRadioButton7.Checked Then Changed = True
            If .ThemeApplyingBehavior.UPM_HKU_DEFAULT <> XenonCheckBox25.Checked Then Changed = True
            If .ThemeApplyingBehavior.Metrics_HKU_DEFAULT_Prefs = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite And Not XenonRadioButton12.Checked Then Changed = True
            If .ThemeApplyingBehavior.Metrics_HKU_DEFAULT_Prefs = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.DontChange And Not XenonRadioButton11.Checked Then Changed = True
            If .ThemeApplyingBehavior.Cursors_HKU_DEFAULT_Prefs = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite And Not XenonRadioButton14.Checked Then Changed = True
            If .ThemeApplyingBehavior.Cursors_HKU_DEFAULT_Prefs = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.DontChange And Not XenonRadioButton13.Checked Then Changed = True
            If .ThemeApplyingBehavior.CMD_HKU_DEFAULT_Prefs = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite And Not XenonRadioButton16.Checked Then Changed = True
            If .ThemeApplyingBehavior.CMD_HKU_DEFAULT_Prefs = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.DontChange And Not XenonRadioButton15.Checked Then Changed = True
            If .ThemeApplyingBehavior.PS86_HKU_DEFAULT_Prefs = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite And Not XenonRadioButton18.Checked Then Changed = True
            If .ThemeApplyingBehavior.PS86_HKU_DEFAULT_Prefs = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.DontChange And Not XenonRadioButton17.Checked Then Changed = True
            If .ThemeApplyingBehavior.PS64_HKU_DEFAULT_Prefs = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite And Not XenonRadioButton20.Checked Then Changed = True
            If .ThemeApplyingBehavior.PS64_HKU_DEFAULT_Prefs = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.DontChange And Not XenonRadioButton19.Checked Then Changed = True
            If .ThemeApplyingBehavior.Desktop_HKU_DEFAULT = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite And Not XenonRadioButton22.Checked Then Changed = True
            If .ThemeApplyingBehavior.Desktop_HKU_DEFAULT = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.RestoreDefaults And Not XenonRadioButton23.Checked Then Changed = True
            If .ThemeApplyingBehavior.Desktop_HKU_DEFAULT = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.DontChange And Not XenonRadioButton21.Checked Then Changed = True

            If .Store.Online_or_Offline And Not XenonRadioImage1.Checked Then Changed = True
            If Not .Store.Online_or_Offline And Not XenonRadioImage2.Checked Then Changed = True

            If .Store.Search_ThemeNames <> XenonCheckBox28.Checked Then Changed = True
            If .Store.Search_Descriptions <> XenonCheckBox26.Checked Then Changed = True
            If .Store.Search_AuthorsNames <> XenonCheckBox27.Checked Then Changed = True
            If .Store.Offline_SubFolders <> XenonCheckBox29.Checked Then Changed = True
            If .Store.ShowTips <> XenonCheckBox4.Checked Then Changed = True

        End With

        If e.CloseReason = CloseReason.UserClosing And Changed Then
            Select Case MsgBox(My.Lang.SaveMsg, MsgBoxStyle.Question + MsgBoxStyle.YesNoCancel)
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


    Private Sub XenonButton2_Click(sender As Object, e As EventArgs) Handles XenonButton2.Click
        Me.Close()
    End Sub

    Private Sub SettingsX_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        XenonComboBox2.Items.Clear()
        XenonComboBox2.Items.Add(My.Lang.Stable)
        XenonComboBox2.Items.Add(My.Lang.Beta)
        LoadLanguage
        ApplyDarkMode(Me)
        LoadSettings()

        Dim w As Integer = 19
        EP_Start_11.Image = My.Resources.Native11.Resize(w, w)
        EP_Start_10.Image = My.Resources.Native10.Resize(w, w)
        EP_Taskbar_11.Image = EP_Start_11.Image
        EP_Taskbar_10.Image = EP_Start_10.Image

        If GetDarkMode() Then
            EP_ORB_11.Image = My.Resources.StartBtn_11_EP.Resize(w, w)
            EP_ORB_10.Image = My.Resources.StartBtn_10Dark.Resize(w, w)
        Else
            EP_ORB_11.Image = My.Resources.StartBtn_11_EP.Invert.Resize(w, w)
            EP_ORB_10.Image = My.Resources.StartBtn_10Light.Resize(w, w)
        End If

        If My.WXP Then
            XenonAlertBox17.Visible = True
            XenonAlertBox17.Text = String.Format(My.Lang.UpdatesOSNoTLS12, My.Lang.OS_WinXP)

        ElseIf My.WVista Then
            XenonAlertBox17.Visible = True
            XenonAlertBox17.Text = String.Format(My.Lang.UpdatesOSNoTLS12, My.Lang.OS_WinVista)
        End If

        Label38.Font = My.Application.ConsoleFontMedium
        Label43.Font = My.Application.ConsoleFontMedium

    End Sub

    Function CalcStoreCache() As Integer
        If IO.Directory.Exists(My.PATH_StoreCache) Then
            Return Directory.EnumerateFiles(My.PATH_StoreCache, "*", SearchOption.AllDirectories).Sum(Function(fileInfo) New FileInfo(fileInfo).Length)
        Else
            Return 0
        End If
    End Function

    Function CalcThemesResCache() As Integer
        If IO.Directory.Exists(My.PATH_ThemeResPackCache) Then
            Return Directory.EnumerateFiles(My.PATH_ThemeResPackCache, "*", SearchOption.AllDirectories).Sum(Function(fileInfo) New FileInfo(fileInfo).Length)
        Else
            Return 0
        End If
    End Function

    Private Sub XenonButton12_Click(sender As Object, e As EventArgs) Handles XenonButton12.Click
        SaveSettings()
        Me.Close()
    End Sub

    Private Sub XenonButton1_Click(sender As Object, e As EventArgs) Handles XenonButton1.Click
        SaveSettings()
    End Sub

    Private Sub XenonButton3_Click(sender As Object, e As EventArgs) Handles XenonButton3.Click

        If SaveFileDialog1.ShowDialog = DialogResult.OK Then
            Dim sets As New XeSettings(XeSettings.Mode.Empty)
            Write(sets, XeSettings.Mode.File, SaveFileDialog1.FileName)
        End If

    End Sub

    Private Sub XenonButton4_Click(sender As Object, e As EventArgs) Handles XenonButton4.Click
        If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
            Dim sets As New XeSettings(XeSettings.Mode.File, OpenFileDialog1.FileName)
            Read(sets)
        End If
    End Sub

    Private Sub Me_DragEnter(ByVal sender As Object, ByVal e As DragEventArgs) Handles Me.DragEnter
        Dim files() As String = e.Data.GetData(DataFormats.FileDrop)

        If Path.GetExtension(files(0)).ToLower = ".wpsf" Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub MainFrm_DragDrop(sender As Object, e As DragEventArgs) Handles Me.DragDrop
        Dim files() As String = e.Data.GetData(DataFormats.FileDrop)

        Dim sets As New XeSettings(XeSettings.Mode.File, files(0))
        Read(sets)

        OpenFileDialog1.FileName = files(0)
    End Sub

    Private Sub XenonButton5_Click(sender As Object, e As EventArgs) Handles XenonButton5.Click
        If MsgBox(My.Lang.RemoveExtMsg, MsgBoxStyle.Question + MsgBoxStyle.YesNo, "", My.Lang.CollapseNote, My.Lang.ExpandNote, My.Lang.RemoveExtMsgNote) = MsgBoxResult.Yes Then

            XenonCheckBox1.Checked = False
            My.Application.DeleteFileAssociation(".wpth", "WinPaletter.ThemeFile")
            My.Application.DeleteFileAssociation(".wpsf", "WinPaletter.SettingsFile")
            My.Application.DeleteFileAssociation(".wptp", "WinPaletter.ThemeResourcesPack")
        End If
    End Sub

    Private Sub XenonButton6_Click(sender As Object, e As EventArgs) Handles XenonButton6.Click
        Uninstall.ShowDialog()
    End Sub

    Private Sub XenonButton7_Click(sender As Object, e As EventArgs) Handles XenonButton7.Click

        If OpenFileDialog2.ShowDialog = DialogResult.OK Then
            XenonTextBox3.Text = OpenFileDialog2.FileName

            Try
                Dim _File As New StreamReader(XenonTextBox3.Text)
                Dim J As JObject = JObject.Parse(_File.ReadToEnd)
                _File.Close()

                Label11.Text = J("Information")("name")
                Label12.Text = J("Information")("translationversion")
                Label14.Text = J("Information")("appver").ToString & " " & My.Lang.AndBelow
                Label19.Text = J("Information")("lang")
                Label16.Text = J("Information")("langcode")
                Label22.Text = If(Not CBool(J("Information")("righttoleft")), My.Lang.Lang_HasLeftToRight, My.Lang.Lang_HasRightToLeft)
            Catch
            End Try

        End If

    End Sub

    Private Sub XenonButton8_Click(sender As Object, e As EventArgs) Handles XenonButton8.Click
        Process.Start(My.Resources.Link_Repository & "blob/master/Documentations/LangContribution.md")
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

    Private Sub XenonButton11_Click(sender As Object, e As EventArgs) Handles XenonButton11.Click
        Lang_Dashboard.ShowDialog()
    End Sub

    Private Sub XenonButton14_Click(sender As Object, e As EventArgs) Handles XenonButton14.Click
        Dim inputText As String = ""
        If ListBox1.SelectedItem IsNot Nothing Then inputText = ListBox1.SelectedItem
        Dim response As String = InputBox(My.Lang.InputThemeRepos, inputText, My.Lang.InputThemeRepos_Notice)
        If Not ListBox1.Items.Contains(response) Then ListBox1.Items.Add(response)
    End Sub

    Private Sub XenonButton15_Click(sender As Object, e As EventArgs) Handles XenonButton15.Click
        If ListBox1.SelectedItem IsNot Nothing Then
            Dim i As Integer = ListBox1.SelectedIndex

            If Not ListBox1.SelectedItem.ToString.ToUpper = My.Resources.Link_StoreReposDB.ToUpper And Not ListBox1.SelectedItem.ToString.ToUpper = My.Resources.Link_StoreMainDB.ToUpper Then
                ListBox1.Items.RemoveAt(i)
                If i < ListBox1.Items.Count - 1 Then ListBox1.SelectedIndex = i Else ListBox1.SelectedIndex = ListBox1.Items.Count - 1
            Else
                MsgBox(My.Lang.Store_RemoveTip, MsgBoxStyle.Critical)
            End If

        End If
    End Sub

    Private Sub XenonButton18_Click(sender As Object, e As EventArgs) Handles XenonButton18.Click

        If Not My.WXP Then
            Dim dlg As New Ookii.Dialogs.WinForms.VistaFolderBrowserDialog
            If dlg.ShowDialog = DialogResult.OK Then
                If Not ListBox2.Items.Contains(dlg.SelectedPath) Then ListBox2.Items.Add(dlg.SelectedPath)
            End If
            dlg.Dispose()
        Else
            If FolderBrowserDialog1.ShowDialog = DialogResult.OK Then
                If Not ListBox2.Items.Contains(FolderBrowserDialog1.SelectedPath) Then ListBox2.Items.Add(FolderBrowserDialog1.SelectedPath)
            End If
        End If

    End Sub

    Private Sub XenonButton17_Click(sender As Object, e As EventArgs) Handles XenonButton17.Click
        If ListBox2.SelectedItem IsNot Nothing Then
            Dim i As Integer = ListBox2.SelectedIndex
            ListBox2.Items.RemoveAt(i)
            If i < ListBox2.Items.Count - 1 Then ListBox2.SelectedIndex = i Else ListBox2.SelectedIndex = ListBox2.Items.Count - 1
        End If
    End Sub

    Private Sub XenonButton19_Click(sender As Object, e As EventArgs) Handles XenonButton19.Click
        Try
            Directory.Delete(My.PATH_StoreCache, True)
        Catch
        End Try
        Label38.Text = CalcStoreCache().SizeString
    End Sub

    Private Sub XenonButton20_Click(sender As Object, e As EventArgs) Handles XenonButton20.Click
        Try
            Directory.Delete(My.PATH_ThemeResPackCache, True)
        Catch
        End Try
        Label43.Text = CalcThemesResCache().SizeString
    End Sub
End Class