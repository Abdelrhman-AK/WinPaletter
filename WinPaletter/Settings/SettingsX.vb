Imports System.IO
Imports Newtonsoft.Json.Linq

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
        TextBox3.Text = My.Settings.Language.File
    End Sub
    Sub Read(Sets As XeSettings)
        With Sets
            CheckBox1.Checked = .FileTypeManagement.AutoAddExt

            RadioButton1.Checked = .FileTypeManagement.OpeningPreviewInApp_or_AppliesIt
            RadioButton2.Checked = Not .FileTypeManagement.OpeningPreviewInApp_or_AppliesIt

            CheckBox2.Checked = .ThemeApplyingBehavior.AutoRestartExplorer
            CheckBox7.Checked = .ThemeApplyingBehavior.AutoApplyCursors
            CheckBox16.Checked = .ThemeApplyingBehavior.ResetCursorsToAero

            CheckBox5.Checked = .Updates.AutoCheck
            CheckBox9.Checked = .Miscellaneous.Win7LivePreview

            ComboBox2.SelectedIndex = If(.Updates.Channel = XeSettings.Structures.Updates.Channels.Stable, 0, 1)
            CheckBox17.Checked = .ThemeApplyingBehavior.ShowSaveConfirmation
            CheckBox33.Checked = .FileTypeManagement.CompressThemeFile

            RadioButton3.Checked = .Appearance.DarkMode
            RadioButton4.Checked = Not .Appearance.DarkMode
            CheckBox6.Checked = .Appearance.AutoDarkMode
            CheckBox30.Checked = .Appearance.ManagedByTheme

            CheckBox8.Checked = .Language.Enabled
            TextBox3.Text = .Language.File

            CheckBox12.Checked = .WindowsTerminals.Bypass
            CheckBox13.Checked = .WindowsTerminals.ListAllFonts
            CheckBox14.Checked = .WindowsTerminals.Path_Deflection
            TextBox1.Text = .WindowsTerminals.Terminal_Stable_Path
            TextBox2.Text = .WindowsTerminals.Terminal_Preview_Path
            CheckBox15.Checked = .ThemeApplyingBehavior.CMD_OverrideUserPreferences

            Select Case .NerdStats.Type
                Case XeSettings.Structures.NerdStats.Formats.HEX
                    ComboBox3.SelectedIndex = 0
                Case XeSettings.Structures.NerdStats.Formats.RGB
                    ComboBox3.SelectedIndex = 1
                Case XeSettings.Structures.NerdStats.Formats.HSL
                    ComboBox3.SelectedIndex = 2
                Case XeSettings.Structures.NerdStats.Formats.Dec
                    ComboBox3.SelectedIndex = 3
            End Select
            CheckBox10.Checked = .NerdStats.Enabled
            CheckBox11.Checked = .NerdStats.ShowHexHash
            CheckBox3.Checked = .NerdStats.MoreLabelTransparency
            CheckBox31.Checked = .NerdStats.UseWindowsMonospacedFont
            CheckBox34.Checked = .NerdStats.DotDefaultChangedIndicator

            CheckBox19.Checked = .ThemeLog.Enabled
            CheckBox18.Checked = .ThemeLog.CountDown
            NumericUpDown1.Value = .ThemeLog.CountDown_Seconds

            CheckBox20.Checked = .ExplorerPatcher.Enabled
            CheckBox21.Checked = .ExplorerPatcher.Enabled_Force
            EP_Start_10.Checked = .ExplorerPatcher.UseStart10
            EP_Start_11.Checked = Not .ExplorerPatcher.UseStart10
            EP_Start_10_Type.SelectedIndex = .ExplorerPatcher.StartStyle
            EP_Taskbar_10.Checked = .ExplorerPatcher.UseTaskbar10
            EP_Taskbar_11.Checked = Not .ExplorerPatcher.UseTaskbar10
            EP_ORB_10.Checked = .ExplorerPatcher.TaskbarButton10
            EP_ORB_11.Checked = Not .ExplorerPatcher.TaskbarButton10

            CheckBox22.Checked = .ThemeApplyingBehavior.DelayMetrics

            RadioButton5.Checked = .ThemeApplyingBehavior.ClassicColors_HKU_DEFAULT_Prefs = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite
            RadioButton6.Checked = Not (.ThemeApplyingBehavior.ClassicColors_HKU_DEFAULT_Prefs = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite)
            RadioButton8.Checked = .ThemeApplyingBehavior.ClassicColors_HKLM_Prefs = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite
            RadioButton10.Checked = .ThemeApplyingBehavior.ClassicColors_HKLM_Prefs = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.DontChange
            RadioButton9.Checked = .ThemeApplyingBehavior.ClassicColors_HKLM_Prefs = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.RestoreDefaults
            RadioButton7.Checked = .ThemeApplyingBehavior.ClassicColors_HKLM_Prefs = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Erase
            CheckBox25.Checked = .ThemeApplyingBehavior.UPM_HKU_DEFAULT
            RadioButton12.Checked = .ThemeApplyingBehavior.Metrics_HKU_DEFAULT_Prefs = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite
            RadioButton11.Checked = Not (.ThemeApplyingBehavior.Metrics_HKU_DEFAULT_Prefs = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite)
            RadioButton14.Checked = .ThemeApplyingBehavior.Cursors_HKU_DEFAULT_Prefs = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite
            RadioButton13.Checked = Not (.ThemeApplyingBehavior.Cursors_HKU_DEFAULT_Prefs = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite)
            RadioButton16.Checked = .ThemeApplyingBehavior.CMD_HKU_DEFAULT_Prefs = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite
            RadioButton15.Checked = Not (.ThemeApplyingBehavior.CMD_HKU_DEFAULT_Prefs = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite)
            RadioButton18.Checked = .ThemeApplyingBehavior.PS86_HKU_DEFAULT_Prefs = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite
            RadioButton17.Checked = Not (.ThemeApplyingBehavior.PS86_HKU_DEFAULT_Prefs = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite)
            RadioButton20.Checked = .ThemeApplyingBehavior.PS64_HKU_DEFAULT_Prefs = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite
            RadioButton19.Checked = Not (.ThemeApplyingBehavior.PS64_HKU_DEFAULT_Prefs = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite)
            RadioButton22.Checked = .ThemeApplyingBehavior.Desktop_HKU_DEFAULT = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite
            RadioButton23.Checked = .ThemeApplyingBehavior.Desktop_HKU_DEFAULT = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.RestoreDefaults
            RadioButton21.Checked = .ThemeApplyingBehavior.Desktop_HKU_DEFAULT = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.DontChange
            CheckBox35_SFC.Checked = .ThemeApplyingBehavior.SFC_on_restoring_StartupSound
            CheckBox36.Checked = .ThemeApplyingBehavior.Ignore_PE_Modify_Alert
            RadioButton25.Checked = .ThemeApplyingBehavior.PE_ModifyByDefault
            RadioButton24.Checked = Not .ThemeApplyingBehavior.PE_ModifyByDefault
            CheckBox35.Checked = .NerdStats.DragAndDrop
            CheckBox37.Checked = .NerdStats.DragAndDropColorsGuide
            CheckBox38.Checked = .NerdStats.DragAndDropRippleEffect

            Label38.Text = CalcStoreCache().SizeString
            Label43.Text = CalcThemesResCache().SizeString

            RadioImage1.Checked = .Store.Online_or_Offline
            RadioImage2.Checked = Not .Store.Online_or_Offline
            ListBox1.Items.Clear()
            For Each x In .Store.Online_Repositories
                If Not String.IsNullOrWhiteSpace(x) Then ListBox1.Items.Add(x)
            Next
            ListBox2.Items.Clear()
            For Each x In .Store.Offline_Directories
                If Not String.IsNullOrWhiteSpace(x) Then ListBox2.Items.Add(x)
            Next

            CheckBox28.Checked = .Store.Search_ThemeNames
            CheckBox26.Checked = .Store.Search_Descriptions
            CheckBox27.Checked = .Store.Search_AuthorsNames
            CheckBox29.Checked = .Store.Offline_SubFolders
            CheckBox4.Checked = .Store.ShowTips

            CheckBox32.Checked = .Miscellaneous.Classic_Color_Picker
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
            If .Appearance.DarkMode <> RadioButton3.Checked Then ch_appearance = True
            If .Appearance.AutoDarkMode <> CheckBox6.Checked Then ch_appearance = True
            If .Appearance.ManagedByTheme <> CheckBox30.Checked Then ch_appearance = True

            If .NerdStats.Enabled <> CheckBox10.Checked Then ch_nerd = True
            If .NerdStats.Type <> ComboBox3.SelectedIndex Then ch_nerd = True
            If .NerdStats.ShowHexHash <> CheckBox11.Checked Then ch_nerd = True
            If .NerdStats.MoreLabelTransparency <> CheckBox3.Checked Then ch_nerd = True
            If .NerdStats.UseWindowsMonospacedFont <> CheckBox31.Checked Then ch_nerd = True
            If .NerdStats.DotDefaultChangedIndicator <> CheckBox34.Checked Then ch_nerd = True

            If .WindowsTerminals.Path_Deflection <> CheckBox14.Checked Then ch_terminal = True
            If .WindowsTerminals.Terminal_Stable_Path <> TextBox1.Text Then ch_terminal = True
            If .WindowsTerminals.Terminal_Preview_Path <> TextBox2.Text Then ch_terminal = True

            If .Language.Enabled <> CheckBox8.Checked Then ch_lang = True
            If .Language.File <> TextBox3.Text Then ch_lang = True

            If .ExplorerPatcher.Enabled <> CheckBox20.Checked Then ch_EP = True
            If .ExplorerPatcher.Enabled_Force <> CheckBox21.Checked Then ch_EP = True
            If .ExplorerPatcher.UseStart10 <> EP_Start_10.Checked Then ch_EP = True
            If .ExplorerPatcher.StartStyle <> EP_Start_10_Type.SelectedIndex Then ch_EP = True
            If .ExplorerPatcher.UseTaskbar10 <> EP_Taskbar_10.Checked Then ch_EP = True
            If .ExplorerPatcher.TaskbarButton10 <> EP_ORB_10.Checked Then ch_EP = True
        End With

        Write(My.Settings, XeSettings.Mode.Registry)

        If ch_appearance Then
            FetchDarkMode()
            ApplyStyle()
        End If

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
            If CheckBox8.Checked Then
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
            .FileTypeManagement.AutoAddExt = CheckBox1.Checked
            .FileTypeManagement.OpeningPreviewInApp_or_AppliesIt = RadioButton1.Checked
            .ThemeApplyingBehavior.AutoRestartExplorer = CheckBox2.Checked
            .ThemeApplyingBehavior.AutoApplyCursors = CheckBox7.Checked
            .ThemeApplyingBehavior.ResetCursorsToAero = CheckBox16.Checked

            .Updates.AutoCheck = CheckBox5.Checked
            .Miscellaneous.Win7LivePreview = CheckBox9.Checked
            .Updates.Channel = ComboBox2.SelectedIndex

            .Appearance.DarkMode = RadioButton3.Checked
            .Appearance.AutoDarkMode = CheckBox6.Checked
            .Appearance.ManagedByTheme = CheckBox30.Checked

            .ThemeApplyingBehavior.ShowSaveConfirmation = CheckBox17.Checked
            .FileTypeManagement.CompressThemeFile = CheckBox33.Checked

            .Language.Enabled = CheckBox8.Checked
            .Language.File = TextBox3.Text
            .NerdStats.Enabled = CheckBox10.Checked
            .NerdStats.Type = ComboBox3.SelectedIndex
            .NerdStats.ShowHexHash = CheckBox11.Checked
            .NerdStats.MoreLabelTransparency = CheckBox3.Checked
            .NerdStats.UseWindowsMonospacedFont = CheckBox31.Checked
            .NerdStats.DotDefaultChangedIndicator = CheckBox34.Checked
            .NerdStats.DragAndDrop = CheckBox35.Checked
            .NerdStats.DragAndDropColorsGuide = CheckBox37.Checked
            .NerdStats.DragAndDropRippleEffect = CheckBox38.Checked

            .WindowsTerminals.Bypass = CheckBox12.Checked
            .WindowsTerminals.ListAllFonts = CheckBox13.Checked
            .WindowsTerminals.Path_Deflection = CheckBox14.Checked
            .WindowsTerminals.Terminal_Stable_Path = TextBox1.Text
            .WindowsTerminals.Terminal_Preview_Path = TextBox2.Text
            .ThemeApplyingBehavior.CMD_OverrideUserPreferences = CheckBox15.Checked

            .ThemeLog.Enabled = CheckBox19.Checked
            .ThemeLog.CountDown = CheckBox18.Checked
            .ThemeLog.CountDown_Seconds = NumericUpDown1.Value

            .ExplorerPatcher.Enabled = CheckBox20.Checked
            .ExplorerPatcher.Enabled_Force = CheckBox21.Checked
            .ExplorerPatcher.UseStart10 = EP_Start_10.Checked
            .ExplorerPatcher.StartStyle = EP_Start_10_Type.SelectedIndex
            .ExplorerPatcher.UseTaskbar10 = EP_Taskbar_10.Checked
            .ExplorerPatcher.TaskbarButton10 = EP_ORB_10.Checked
            .ThemeApplyingBehavior.DelayMetrics = CheckBox22.Checked

            If RadioButton5.Checked Then .ThemeApplyingBehavior.ClassicColors_HKU_DEFAULT_Prefs = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite Else .ThemeApplyingBehavior.ClassicColors_HKU_DEFAULT_Prefs = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.DontChange
            If RadioButton8.Checked Then .ThemeApplyingBehavior.ClassicColors_HKLM_Prefs = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite
            If RadioButton10.Checked Then .ThemeApplyingBehavior.ClassicColors_HKLM_Prefs = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.DontChange
            If RadioButton9.Checked Then .ThemeApplyingBehavior.ClassicColors_HKLM_Prefs = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.RestoreDefaults
            If RadioButton7.Checked Then .ThemeApplyingBehavior.ClassicColors_HKLM_Prefs = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Erase
            .ThemeApplyingBehavior.UPM_HKU_DEFAULT = CheckBox25.Checked
            If RadioButton12.Checked Then .ThemeApplyingBehavior.Metrics_HKU_DEFAULT_Prefs = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite Else .ThemeApplyingBehavior.Metrics_HKU_DEFAULT_Prefs = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.DontChange
            If RadioButton14.Checked Then .ThemeApplyingBehavior.Cursors_HKU_DEFAULT_Prefs = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite Else .ThemeApplyingBehavior.Cursors_HKU_DEFAULT_Prefs = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.DontChange
            If RadioButton16.Checked Then .ThemeApplyingBehavior.CMD_HKU_DEFAULT_Prefs = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite Else .ThemeApplyingBehavior.CMD_HKU_DEFAULT_Prefs = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.DontChange
            If RadioButton18.Checked Then .ThemeApplyingBehavior.PS86_HKU_DEFAULT_Prefs = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite Else .ThemeApplyingBehavior.PS86_HKU_DEFAULT_Prefs = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.DontChange
            If RadioButton20.Checked Then .ThemeApplyingBehavior.PS64_HKU_DEFAULT_Prefs = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite Else .ThemeApplyingBehavior.PS64_HKU_DEFAULT_Prefs = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.DontChange
            If RadioButton22.Checked Then .ThemeApplyingBehavior.Desktop_HKU_DEFAULT = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite
            If RadioButton23.Checked Then .ThemeApplyingBehavior.Desktop_HKU_DEFAULT = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.RestoreDefaults
            If RadioButton21.Checked Then .ThemeApplyingBehavior.Desktop_HKU_DEFAULT = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.DontChange
            .ThemeApplyingBehavior.SFC_on_restoring_StartupSound = CheckBox35_SFC.Checked
            .ThemeApplyingBehavior.Ignore_PE_Modify_Alert = CheckBox36.Checked
            .ThemeApplyingBehavior.PE_ModifyByDefault = RadioButton25.Checked

            .Store.Online_or_Offline = RadioImage1.Checked
            .Store.Online_Repositories = ListBox1.Items.OfType(Of String)().Where(Function(s) Not String.IsNullOrEmpty(s)).ToArray()
            .Store.Offline_Directories = ListBox2.Items.OfType(Of String)().Where(Function(s) Not String.IsNullOrEmpty(s)).ToArray()
            .Store.Search_ThemeNames = CheckBox28.Checked
            .Store.Search_Descriptions = CheckBox26.Checked
            .Store.Search_AuthorsNames = CheckBox27.Checked
            .Store.Offline_SubFolders = CheckBox29.Checked
            .Store.ShowTips = CheckBox4.Checked

            .Miscellaneous.Classic_Color_Picker = CheckBox32.Checked

            .Save(Mode, File)
        End With
    End Sub
    Protected Overrides Sub OnFormClosing(ByVal e As FormClosingEventArgs)
        Dim NewSets As New XeSettings(XeSettings.Mode.Empty)

        Changed = False

        With My.Settings
            If .FileTypeManagement.AutoAddExt <> CheckBox1.Checked Then Changed = True
            If .FileTypeManagement.OpeningPreviewInApp_or_AppliesIt <> RadioButton1.Checked Then Changed = True
            If .ThemeApplyingBehavior.AutoRestartExplorer <> CheckBox2.Checked Then Changed = True
            If .ThemeApplyingBehavior.AutoApplyCursors <> CheckBox7.Checked Then Changed = True
            If .ThemeApplyingBehavior.ResetCursorsToAero <> CheckBox16.Checked Then Changed = True
            If .Updates.AutoCheck <> CheckBox5.Checked Then Changed = True
            If .Updates.Channel <> ComboBox2.SelectedIndex Then Changed = True
            If .Miscellaneous.Win7LivePreview <> CheckBox9.Checked Then Changed = True
            If .Miscellaneous.Classic_Color_Picker <> CheckBox32.Checked Then Changed = True
            If .ThemeApplyingBehavior.ShowSaveConfirmation <> CheckBox17.Checked Then Changed = True
            If .FileTypeManagement.CompressThemeFile <> CheckBox33.Checked Then Changed = True

            If .Appearance.DarkMode <> RadioButton3.Checked Then Changed = True
            If .Appearance.AutoDarkMode <> CheckBox6.Checked Then Changed = True
            If .Appearance.ManagedByTheme <> CheckBox30.Checked Then Changed = True

            If .Language.Enabled <> CheckBox8.Checked Then Changed = True
            If .Language.File <> TextBox3.Text Then Changed = True

            If .NerdStats.Enabled <> CheckBox10.Checked Then Changed = True
            If .NerdStats.Type <> ComboBox3.SelectedIndex Then Changed = True
            If .NerdStats.ShowHexHash <> CheckBox11.Checked Then Changed = True
            If .NerdStats.MoreLabelTransparency <> CheckBox3.Checked Then Changed = True
            If .NerdStats.UseWindowsMonospacedFont <> CheckBox31.Checked Then Changed = True
            If .NerdStats.DotDefaultChangedIndicator <> CheckBox34.Checked Then Changed = True
            If .NerdStats.DragAndDrop <> CheckBox35.Checked Then Changed = True
            If .NerdStats.DragAndDropColorsGuide <> CheckBox37.Checked Then Changed = True
            If .NerdStats.DragAndDropRippleEffect <> CheckBox38.Checked Then Changed = True

            If .WindowsTerminals.Bypass <> CheckBox12.Checked Then Changed = True
            If .WindowsTerminals.ListAllFonts <> CheckBox13.Checked Then Changed = True
            If .WindowsTerminals.Path_Deflection <> CheckBox14.Checked Then Changed = True
            If .WindowsTerminals.Terminal_Stable_Path <> TextBox1.Text Then Changed = True
            If .WindowsTerminals.Terminal_Preview_Path <> TextBox2.Text Then Changed = True
            If .ThemeApplyingBehavior.CMD_OverrideUserPreferences <> CheckBox15.Checked Then Changed = True

            If .ThemeLog.Enabled <> CheckBox19.Checked Then Changed = True
            If .ThemeLog.CountDown <> CheckBox18.Checked Then Changed = True
            If .ThemeLog.CountDown_Seconds <> NumericUpDown1.Value Then Changed = True

            If .ExplorerPatcher.Enabled <> CheckBox20.Checked Then Changed = True
            If .ExplorerPatcher.Enabled_Force <> CheckBox21.Checked Then Changed = True
            If .ExplorerPatcher.UseStart10 <> EP_Start_10.Checked Then Changed = True
            If .ExplorerPatcher.StartStyle <> EP_Start_10_Type.SelectedIndex Then Changed = True
            If .ExplorerPatcher.UseTaskbar10 <> EP_Taskbar_10.Checked Then Changed = True
            If .ExplorerPatcher.TaskbarButton10 <> EP_ORB_10.Checked Then Changed = True

            If .ThemeApplyingBehavior.DelayMetrics <> CheckBox22.Checked Then Changed = True

            If .ThemeApplyingBehavior.ClassicColors_HKU_DEFAULT_Prefs = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite And Not RadioButton5.Checked Then Changed = True
            If .ThemeApplyingBehavior.ClassicColors_HKU_DEFAULT_Prefs = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.DontChange And Not RadioButton6.Checked Then Changed = True

            If .ThemeApplyingBehavior.ClassicColors_HKLM_Prefs = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite And Not RadioButton8.Checked Then Changed = True
            If .ThemeApplyingBehavior.ClassicColors_HKLM_Prefs = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.DontChange And Not RadioButton10.Checked Then Changed = True
            If .ThemeApplyingBehavior.ClassicColors_HKLM_Prefs = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.RestoreDefaults And Not RadioButton9.Checked Then Changed = True
            If .ThemeApplyingBehavior.ClassicColors_HKLM_Prefs = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Erase And Not RadioButton7.Checked Then Changed = True
            If .ThemeApplyingBehavior.UPM_HKU_DEFAULT <> CheckBox25.Checked Then Changed = True
            If .ThemeApplyingBehavior.Metrics_HKU_DEFAULT_Prefs = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite And Not RadioButton12.Checked Then Changed = True
            If .ThemeApplyingBehavior.Metrics_HKU_DEFAULT_Prefs = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.DontChange And Not RadioButton11.Checked Then Changed = True
            If .ThemeApplyingBehavior.Cursors_HKU_DEFAULT_Prefs = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite And Not RadioButton14.Checked Then Changed = True
            If .ThemeApplyingBehavior.Cursors_HKU_DEFAULT_Prefs = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.DontChange And Not RadioButton13.Checked Then Changed = True
            If .ThemeApplyingBehavior.CMD_HKU_DEFAULT_Prefs = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite And Not RadioButton16.Checked Then Changed = True
            If .ThemeApplyingBehavior.CMD_HKU_DEFAULT_Prefs = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.DontChange And Not RadioButton15.Checked Then Changed = True
            If .ThemeApplyingBehavior.PS86_HKU_DEFAULT_Prefs = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite And Not RadioButton18.Checked Then Changed = True
            If .ThemeApplyingBehavior.PS86_HKU_DEFAULT_Prefs = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.DontChange And Not RadioButton17.Checked Then Changed = True
            If .ThemeApplyingBehavior.PS64_HKU_DEFAULT_Prefs = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite And Not RadioButton20.Checked Then Changed = True
            If .ThemeApplyingBehavior.PS64_HKU_DEFAULT_Prefs = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.DontChange And Not RadioButton19.Checked Then Changed = True
            If .ThemeApplyingBehavior.Desktop_HKU_DEFAULT = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite And Not RadioButton22.Checked Then Changed = True
            If .ThemeApplyingBehavior.Desktop_HKU_DEFAULT = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.RestoreDefaults And Not RadioButton23.Checked Then Changed = True
            If .ThemeApplyingBehavior.Desktop_HKU_DEFAULT = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.DontChange And Not RadioButton21.Checked Then Changed = True
            If .ThemeApplyingBehavior.SFC_on_restoring_StartupSound <> CheckBox35_SFC.Checked Then Changed = True
            If .ThemeApplyingBehavior.Ignore_PE_Modify_Alert <> CheckBox36.Checked Then Changed = True
            If .ThemeApplyingBehavior.PE_ModifyByDefault <> RadioButton25.Checked Then Changed = True

            If .Store.Online_or_Offline And Not RadioImage1.Checked Then Changed = True
            If Not .Store.Online_or_Offline And Not RadioImage2.Checked Then Changed = True

            If .Store.Search_ThemeNames <> CheckBox28.Checked Then Changed = True
            If .Store.Search_Descriptions <> CheckBox26.Checked Then Changed = True
            If .Store.Search_AuthorsNames <> CheckBox27.Checked Then Changed = True
            If .Store.Offline_SubFolders <> CheckBox29.Checked Then Changed = True
            If .Store.ShowTips <> CheckBox4.Checked Then Changed = True

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


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub SettingsX_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ComboBox2.Items.Clear()
        ComboBox2.Items.Add(My.Lang.Stable)
        ComboBox2.Items.Add(My.Lang.Beta)
        LoadLanguage
        ApplyStyle(Me)
        LoadSettings()

        Dim w As Integer = 19
        EP_Start_11.Image = My.Resources.Native11.Resize(w, w)
        EP_Start_10.Image = My.Resources.Native10.Resize(w, w)
        EP_Taskbar_11.Image = EP_Start_11.Image
        EP_Taskbar_10.Image = EP_Start_10.Image

        If My.Style.DarkMode Then
            EP_ORB_11.Image = My.Resources.StartBtn_11_EP.Resize(w, w)
            EP_ORB_10.Image = My.Resources.StartBtn_10Dark.Resize(w, w)
        Else
            EP_ORB_11.Image = My.Resources.StartBtn_11_EP.Invert.Resize(w, w)
            EP_ORB_10.Image = My.Resources.StartBtn_10Light.Resize(w, w)
        End If

        If My.WXP Then
            AlertBox17.Visible = True
            AlertBox17.Text = String.Format(My.Lang.UpdatesOSNoTLS12, My.Lang.OS_WinXP)

        ElseIf My.WVista Then
            AlertBox17.Visible = True
            AlertBox17.Text = String.Format(My.Lang.UpdatesOSNoTLS12, My.Lang.OS_WinVista)
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

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        SaveSettings()
        Me.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        SaveSettings()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        If SaveFileDialog1.ShowDialog = DialogResult.OK Then
            Dim sets As New XeSettings(XeSettings.Mode.Empty)
            Write(sets, XeSettings.Mode.File, SaveFileDialog1.FileName)
        End If

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
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

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        If MsgBox(My.Lang.RemoveExtMsg, MsgBoxStyle.Question + MsgBoxStyle.YesNo, "", My.Lang.CollapseNote, My.Lang.ExpandNote, My.Lang.RemoveExtMsgNote) = MsgBoxResult.Yes Then

            CheckBox1.Checked = False
            My.Application.DeleteFileAssociation(".wpth", "WinPaletter.ThemeFile")
            My.Application.DeleteFileAssociation(".wpsf", "WinPaletter.SettingsFile")
            My.Application.DeleteFileAssociation(".wptp", "WinPaletter.ThemeResourcesPack")
        End If
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Uninstall.ShowDialog()
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click

        If OpenFileDialog2.ShowDialog = DialogResult.OK Then
            TextBox3.Text = OpenFileDialog2.FileName

            Try
                Dim _File As New StreamReader(TextBox3.Text)
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

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Process.Start(My.Resources.Link_Repository & "wiki/Language-creation")
    End Sub

    Private Sub Button16_Click(sender As Object, e As EventArgs) Handles Button16.Click
        If OpenJSONDlg.ShowDialog = DialogResult.OK Then
            TextBox1.Text = OpenJSONDlg.FileName
        End If
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        If OpenJSONDlg.ShowDialog = DialogResult.OK Then
            TextBox2.Text = OpenJSONDlg.FileName
        End If
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        Process.Start(My.Resources.Link_Repository & "tree/master/Languages")
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        Lang_Dashboard.ShowDialog()
    End Sub

    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
        Dim inputText As String = ""
        If ListBox1.SelectedItem IsNot Nothing Then inputText = ListBox1.SelectedItem
        Dim response As String = InputBox(My.Lang.InputThemeRepos, inputText, My.Lang.InputThemeRepos_Notice)
        If Not ListBox1.Items.Contains(response) Then ListBox1.Items.Add(response)
    End Sub

    Private Sub Button15_Click(sender As Object, e As EventArgs) Handles Button15.Click
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

    Private Sub Button18_Click(sender As Object, e As EventArgs) Handles Button18.Click

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

    Private Sub Button17_Click(sender As Object, e As EventArgs) Handles Button17.Click
        If ListBox2.SelectedItem IsNot Nothing Then
            Dim i As Integer = ListBox2.SelectedIndex
            ListBox2.Items.RemoveAt(i)
            If i < ListBox2.Items.Count - 1 Then ListBox2.SelectedIndex = i Else ListBox2.SelectedIndex = ListBox2.Items.Count - 1
        End If
    End Sub

    Private Sub Button19_Click(sender As Object, e As EventArgs) Handles Button19.Click
        Try
            Directory.Delete(My.PATH_StoreCache, True)
        Catch
        End Try
        Label38.Text = CalcStoreCache().SizeString
    End Sub

    Private Sub Button20_Click(sender As Object, e As EventArgs) Handles Button20.Click
        Try
            Directory.Delete(My.PATH_ThemeResPackCache, True)
        Catch
        End Try
        Label43.Text = CalcThemesResCache().SizeString
    End Sub
End Class