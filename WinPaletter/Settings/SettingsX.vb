Imports System.IO
Imports Newtonsoft.Json.Linq
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
        XenonComboBox2.Items.Add(My.Lang.Stable)
        XenonComboBox2.Items.Add(My.Lang.Beta)
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

    End Sub

    Protected Overrides Sub OnFormClosing(ByVal e As FormClosingEventArgs)
        Dim NewSets As New XeSettings(XeSettings.Mode.Empty)

        Changed = False

        With My.[Settings]
            If .AutoAddExt <> XenonCheckBox1.Checked Then Changed = True
            If .DragAndDropPreview <> XenonCheckBox3.Checked Then Changed = True
            If .OpeningPreviewInApp_or_AppliesIt <> XenonRadioButton1.Checked Then Changed = True
            If .AutoRestartExplorer <> XenonCheckBox2.Checked Then Changed = True
            If .AutoApplyCursors <> XenonCheckBox7.Checked Then Changed = True
            If .ResetCursorsToAero <> XenonCheckBox16.Checked Then Changed = True
            If .AutoUpdatesChecking <> XenonCheckBox5.Checked Then Changed = True
            If .UpdateChannel <> XenonComboBox2.SelectedIndex Then Changed = True
            If .SaveForLegacyWP <> XenonCheckBox4.Checked Then Changed = True
            If .Win7LivePreview <> XenonCheckBox9.Checked Then Changed = True
            If .ShowSaveConfirmation <> XenonCheckBox17.Checked Then Changed = True

            If .Appearance_Dark <> XenonRadioButton3.Checked Then Changed = True
            If .Appearance_Auto <> XenonCheckBox6.Checked Then Changed = True
            If .Appearance_Custom <> appearance_enabled.Checked Then Changed = True
            If .Appearance_Custom_Dark <> appearance_dark.Checked Then Changed = True
            If .Appearance_Rounded <> appearance_rounded.Checked Then Changed = True
            If .Appearance_Accent <> appearance_accent.BackColor Then Changed = True
            If .Appearance_Back <> appearance_backcolor.BackColor Then Changed = True

            If .Language <> XenonCheckBox8.Checked Then Changed = True
            If .Language_File <> XenonTextBox3.Text Then Changed = True
            If .Nerd_Stats <> XenonCheckBox10.Checked Then Changed = True
            If .Nerd_Stats_Kind <> XenonComboBox3.SelectedIndex Then Changed = True
            If .Nerd_Stats_HexHash <> XenonCheckBox11.Checked Then Changed = True
            If .Terminal_Bypass <> XenonCheckBox12.Checked Then Changed = True
            If .Terminal_OtherFonts <> XenonCheckBox13.Checked Then Changed = True
            If .Terminal_Path_Deflection <> XenonCheckBox14.Checked Then Changed = True
            If .Terminal_Stable_Path <> XenonTextBox1.Text Then Changed = True
            If .Terminal_Preview_Path <> XenonTextBox2.Text Then Changed = True
            If .CMD_OverrideUserPreferences <> XenonCheckBox15.Checked Then Changed = True

            If .Log_ShowApplying <> XenonCheckBox19.Checked Then Changed = True
            If .Log_Countdown_Enabled <> XenonCheckBox18.Checked Then Changed = True
            If .Log_Countdown <> XenonNumericUpDown1.Value Then Changed = True

            If .EP_Enabled <> XenonCheckBox20.Checked Then Changed = True
            If .EP_Enabled_Force <> XenonCheckBox21.Checked Then Changed = True
            If .EP_UseStart10 <> EP_Start_10.Checked Then Changed = True
            If .EP_StartStyle <> EP_Start_10_Type.SelectedIndex Then Changed = True
            If .EP_UseTaskbar10 <> EP_Taskbar_10.Checked Then Changed = True
            If .EP_TaskbarButton10 <> EP_ORB_10.Checked Then Changed = True

            If .DelayMetrics <> XenonCheckBox22.Checked Then Changed = True

            If .ClassicColors_HKU_DEFAULT_Prefs = XeSettings.OverwriteOptions.Overwrite And Not XenonRadioButton5.Checked Then Changed = True
            If .ClassicColors_HKU_DEFAULT_Prefs = XeSettings.OverwriteOptions.DontChange And Not XenonRadioButton6.Checked Then Changed = True

            If .ClassicColors_HKLM_Prefs = XeSettings.OverwriteOptions.Overwrite And Not XenonRadioButton8.Checked Then Changed = True
            If .ClassicColors_HKLM_Prefs = XeSettings.OverwriteOptions.DontChange And Not XenonRadioButton10.Checked Then Changed = True
            If .ClassicColors_HKLM_Prefs = XeSettings.OverwriteOptions.RestoreDefaults And Not XenonRadioButton9.Checked Then Changed = True
            If .ClassicColors_HKLM_Prefs = XeSettings.OverwriteOptions.Erase And Not XenonRadioButton7.Checked Then Changed = True
            If .ClassicColors_HKU_DEFAULT_UPM <> XenonCheckBox25.Checked Then Changed = True
            If .Metrics_HKU_DEFAULT_Prefs = XeSettings.OverwriteOptions.Overwrite And Not XenonRadioButton12.Checked Then Changed = True
            If .Metrics_HKU_DEFAULT_Prefs = XeSettings.OverwriteOptions.DontChange And Not XenonRadioButton11.Checked Then Changed = True
            If .Cursors_HKU_DEFAULT_Prefs = XeSettings.OverwriteOptions.Overwrite And Not XenonRadioButton14.Checked Then Changed = True
            If .Cursors_HKU_DEFAULT_Prefs = XeSettings.OverwriteOptions.DontChange And Not XenonRadioButton13.Checked Then Changed = True
            If .CMD_HKU_DEFAULT_Prefs = XeSettings.OverwriteOptions.Overwrite And Not XenonRadioButton16.Checked Then Changed = True
            If .CMD_HKU_DEFAULT_Prefs = XeSettings.OverwriteOptions.DontChange And Not XenonRadioButton15.Checked Then Changed = True
            If .PS86_HKU_DEFAULT_Prefs = XeSettings.OverwriteOptions.Overwrite And Not XenonRadioButton18.Checked Then Changed = True
            If .PS86_HKU_DEFAULT_Prefs = XeSettings.OverwriteOptions.DontChange And Not XenonRadioButton17.Checked Then Changed = True
            If .PS64_HKU_DEFAULT_Prefs = XeSettings.OverwriteOptions.Overwrite And Not XenonRadioButton20.Checked Then Changed = True
            If .PS64_HKU_DEFAULT_Prefs = XeSettings.OverwriteOptions.DontChange And Not XenonRadioButton19.Checked Then Changed = True
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

    Sub LoadSettings()
        Dim sets As XeSettings

        If Not _External Then sets = My.[Settings] Else sets = New XeSettings(XeSettings.Mode.File, _File)

        With sets
            XenonCheckBox1.Checked = .AutoAddExt

            XenonCheckBox3.Checked = .DragAndDropPreview
            XenonRadioButton1.Checked = .OpeningPreviewInApp_or_AppliesIt
            XenonRadioButton2.Checked = Not .OpeningPreviewInApp_or_AppliesIt

            XenonCheckBox2.Checked = .AutoRestartExplorer
            XenonCheckBox7.Checked = .AutoApplyCursors
            XenonCheckBox16.Checked = .ResetCursorsToAero

            XenonCheckBox5.Checked = .AutoUpdatesChecking
            XenonCheckBox9.Checked = .Win7LivePreview

            XenonComboBox2.SelectedIndex = If(.UpdateChannel = .UpdateChannels.Stable, 0, 1)
            XenonCheckBox17.Checked = .ShowSaveConfirmation
            XenonCheckBox4.Checked = .SaveForLegacyWP

            XenonRadioButton3.Checked = .Appearance_Dark
            XenonRadioButton4.Checked = Not .Appearance_Dark
            XenonCheckBox6.Checked = .Appearance_Auto

            appearance_enabled.Checked = .Appearance_Custom
            If appearance_list.Items.Contains(.Appearance_SchemeName) Then appearance_list.SelectedItem = .Appearance_SchemeName
            appearance_dark.Checked = .Appearance_Custom_Dark
            appearance_accent.BackColor = .Appearance_Accent
            appearance_backcolor.BackColor = .Appearance_Back
            appearance_rounded.Checked = .Appearance_Rounded

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

            XenonCheckBox19.Checked = .Log_ShowApplying
            XenonCheckBox18.Checked = .Log_Countdown_Enabled
            XenonNumericUpDown1.Value = .Log_Countdown

            XenonCheckBox20.Checked = .EP_Enabled
            XenonCheckBox21.Checked = .EP_Enabled_Force
            EP_Start_10.Checked = .EP_UseStart10
            EP_Start_11.Checked = Not .EP_UseStart10
            EP_Start_10_Type.SelectedIndex = .EP_StartStyle
            EP_Taskbar_10.Checked = .EP_UseTaskbar10
            EP_Taskbar_11.Checked = Not .EP_UseTaskbar10
            EP_ORB_10.Checked = .EP_TaskbarButton10
            EP_ORB_11.Checked = Not .EP_TaskbarButton10

            XenonCheckBox22.Checked = .DelayMetrics

            XenonRadioButton5.Checked = .ClassicColors_HKU_DEFAULT_Prefs = XeSettings.OverwriteOptions.Overwrite
            XenonRadioButton6.Checked = Not (.ClassicColors_HKU_DEFAULT_Prefs = XeSettings.OverwriteOptions.Overwrite)
            XenonRadioButton8.Checked = .ClassicColors_HKLM_Prefs = XeSettings.OverwriteOptions.Overwrite
            XenonRadioButton10.Checked = .ClassicColors_HKLM_Prefs = XeSettings.OverwriteOptions.DontChange
            XenonRadioButton9.Checked = .ClassicColors_HKLM_Prefs = XeSettings.OverwriteOptions.RestoreDefaults
            XenonRadioButton7.Checked = .ClassicColors_HKLM_Prefs = XeSettings.OverwriteOptions.Erase
            XenonCheckBox25.Checked = .ClassicColors_HKU_DEFAULT_UPM
            XenonRadioButton12.Checked = .Metrics_HKU_DEFAULT_Prefs = XeSettings.OverwriteOptions.Overwrite
            XenonRadioButton11.Checked = Not (.Metrics_HKU_DEFAULT_Prefs = XeSettings.OverwriteOptions.Overwrite)
            XenonRadioButton14.Checked = .Cursors_HKU_DEFAULT_Prefs = XeSettings.OverwriteOptions.Overwrite
            XenonRadioButton13.Checked = Not (.Cursors_HKU_DEFAULT_Prefs = XeSettings.OverwriteOptions.Overwrite)
            XenonRadioButton16.Checked = .CMD_HKU_DEFAULT_Prefs = XeSettings.OverwriteOptions.Overwrite
            XenonRadioButton15.Checked = Not (.CMD_HKU_DEFAULT_Prefs = XeSettings.OverwriteOptions.Overwrite)
            XenonRadioButton18.Checked = .PS86_HKU_DEFAULT_Prefs = XeSettings.OverwriteOptions.Overwrite
            XenonRadioButton17.Checked = Not (.PS86_HKU_DEFAULT_Prefs = XeSettings.OverwriteOptions.Overwrite)
            XenonRadioButton20.Checked = .PS64_HKU_DEFAULT_Prefs = XeSettings.OverwriteOptions.Overwrite
            XenonRadioButton19.Checked = Not (.PS64_HKU_DEFAULT_Prefs = XeSettings.OverwriteOptions.Overwrite)
        End With

        With My.Lang
            Label11.Text = .Name
            Label12.Text = .TranslationVersion
            Label14.Text = .AppVer & " " & My.Lang.AndBelow
            Label19.Text = .Lang
            Label16.Text = .LangCode
            Label22.Text = If(Not .RightToLeft, My.Lang.Lang_HasLeftToRight, My.Lang.Lang_HasRightToLeft)
        End With

        If _External Then OpenFileDialog1.FileName = _File
        XenonTextBox3.Text = My.[Settings].Language_File
    End Sub

    Sub SaveSettings()
        Cursor = Cursors.WaitCursor

        'Ch = Change
        Dim ch_nerd As Boolean = False
        Dim ch_terminal As Boolean = False
        Dim ch_lang As Boolean = False
        Dim ch_appearance As Boolean = False
        Dim ch_EP As Boolean = False

        With My.[Settings]
            If .Appearance_Dark <> XenonRadioButton3.Checked Then ch_appearance = True
            If .Appearance_Auto <> XenonCheckBox6.Checked Then ch_appearance = True
            If .Appearance_Custom <> appearance_enabled.Checked Then ch_appearance = True
            If .Appearance_Custom_Dark <> appearance_dark.Checked Then ch_appearance = True
            If .Appearance_Rounded <> appearance_rounded.Checked Then ch_appearance = True
            If .Appearance_Accent <> appearance_accent.BackColor Then ch_appearance = True
            If .Appearance_Back <> appearance_backcolor.BackColor Then ch_appearance = True

            If .Nerd_Stats <> XenonCheckBox10.Checked Then ch_nerd = True
            If .Nerd_Stats_Kind <> XenonComboBox3.SelectedIndex Then ch_nerd = True
            If .Nerd_Stats_HexHash <> XenonCheckBox11.Checked Then ch_nerd = True

            If .Terminal_Path_Deflection <> XenonCheckBox14.Checked Then ch_terminal = True
            If .Terminal_Stable_Path <> XenonTextBox1.Text Then ch_terminal = True
            If .Terminal_Preview_Path <> XenonTextBox2.Text Then ch_terminal = True

            If .Language <> XenonCheckBox8.Checked Then ch_lang = True
            If .Language_File <> XenonTextBox3.Text Then ch_lang = True

            If .EP_Enabled <> XenonCheckBox20.Checked Then ch_EP = True
            If .EP_Enabled_Force <> XenonCheckBox21.Checked Then ch_EP = True
            If .EP_UseStart10 <> EP_Start_10.Checked Then ch_EP = True
            If .EP_StartStyle <> EP_Start_10_Type.SelectedIndex Then ch_EP = True
            If .EP_UseTaskbar10 <> EP_Taskbar_10.Checked Then ch_EP = True
            If .EP_TaskbarButton10 <> EP_ORB_10.Checked Then ch_EP = True

            .AutoAddExt = XenonCheckBox1.Checked
            .DragAndDropPreview = XenonCheckBox3.Checked
            .OpeningPreviewInApp_or_AppliesIt = XenonRadioButton1.Checked
            .AutoRestartExplorer = XenonCheckBox2.Checked
            .AutoApplyCursors = XenonCheckBox7.Checked
            .ResetCursorsToAero = XenonCheckBox16.Checked

            .AutoUpdatesChecking = XenonCheckBox5.Checked
            .Win7LivePreview = XenonCheckBox9.Checked
            .UpdateChannel = XenonComboBox2.SelectedIndex

            .Appearance_Dark = XenonRadioButton3.Checked
            .Appearance_Auto = XenonCheckBox6.Checked
            .Appearance_Custom = appearance_enabled.Checked
            .Appearance_SchemeName = appearance_list.SelectedItem
            .Appearance_Custom_Dark = appearance_dark.Checked
            .Appearance_Accent = appearance_accent.BackColor
            .Appearance_Back = appearance_backcolor.BackColor
            .Appearance_Rounded = appearance_rounded.Checked

            .ShowSaveConfirmation = XenonCheckBox17.Checked
            .SaveForLegacyWP = XenonCheckBox4.Checked

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

            .Log_ShowApplying = XenonCheckBox19.Checked
            .Log_Countdown_Enabled = XenonCheckBox18.Checked
            .Log_Countdown = XenonNumericUpDown1.Value

            .EP_Enabled = XenonCheckBox20.Checked
            .EP_Enabled_Force = XenonCheckBox21.Checked
            .EP_UseStart10 = EP_Start_10.Checked
            .EP_StartStyle = EP_Start_10_Type.SelectedIndex
            .EP_UseTaskbar10 = EP_Taskbar_10.Checked
            .EP_TaskbarButton10 = EP_ORB_10.Checked

            .DelayMetrics = XenonCheckBox22.Checked

            If XenonRadioButton5.Checked Then .ClassicColors_HKU_DEFAULT_Prefs = XeSettings.OverwriteOptions.Overwrite Else .ClassicColors_HKU_DEFAULT_Prefs = XeSettings.OverwriteOptions.DontChange
            If XenonRadioButton8.Checked Then .ClassicColors_HKLM_Prefs = XeSettings.OverwriteOptions.Overwrite
            If XenonRadioButton10.Checked Then .ClassicColors_HKLM_Prefs = XeSettings.OverwriteOptions.DontChange
            If XenonRadioButton9.Checked Then .ClassicColors_HKLM_Prefs = XeSettings.OverwriteOptions.RestoreDefaults
            If XenonRadioButton7.Checked Then .ClassicColors_HKLM_Prefs = XeSettings.OverwriteOptions.Erase
            .ClassicColors_HKU_DEFAULT_UPM = XenonCheckBox25.Checked
            If XenonRadioButton12.Checked Then .Metrics_HKU_DEFAULT_Prefs = XeSettings.OverwriteOptions.Overwrite Else .Metrics_HKU_DEFAULT_Prefs = XeSettings.OverwriteOptions.DontChange
            If XenonRadioButton14.Checked Then .Cursors_HKU_DEFAULT_Prefs = XeSettings.OverwriteOptions.Overwrite Else .Cursors_HKU_DEFAULT_Prefs = XeSettings.OverwriteOptions.DontChange
            If XenonRadioButton16.Checked Then .CMD_HKU_DEFAULT_Prefs = XeSettings.OverwriteOptions.Overwrite Else .CMD_HKU_DEFAULT_Prefs = XeSettings.OverwriteOptions.DontChange
            If XenonRadioButton18.Checked Then .PS86_HKU_DEFAULT_Prefs = XeSettings.OverwriteOptions.Overwrite Else .PS86_HKU_DEFAULT_Prefs = XeSettings.OverwriteOptions.DontChange
            If XenonRadioButton20.Checked Then .PS64_HKU_DEFAULT_Prefs = XeSettings.OverwriteOptions.Overwrite Else .PS64_HKU_DEFAULT_Prefs = XeSettings.OverwriteOptions.DontChange

            .Save(XeSettings.Mode.Registry)
        End With

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

                If Not My.[Settings].Terminal_Path_Deflection Then
                    TerDir = My.PATH_TerminalJSON
                    TerPreDir = My.PATH_TerminalPreviewJSON
                Else
                    If IO.File.Exists(My.[Settings].Terminal_Stable_Path) Then
                        TerDir = My.[Settings].Terminal_Stable_Path
                    Else
                        TerDir = My.PATH_TerminalJSON
                    End If

                    If IO.File.Exists(My.[Settings].Terminal_Preview_Path) Then
                        TerPreDir = My.[Settings].Terminal_Preview_Path
                    Else
                        TerPreDir = My.PATH_TerminalPreviewJSON
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

        If ch_lang Then
            If XenonCheckBox8.Checked Then
                My.Lang = New Localizer
                My.Lang.LoadLanguageFromJSON(My.Settings.Language_File)
            Else
                MsgBox(My.Lang.LanguageRestart, MsgBoxStyle.Information)
            End If
        End If

        If ch_EP Then
            My.EP = New ExplorerPatcher
            MainFrm.ApplyLivePreviewFromCP(MainFrm.CP)
            MainFrm.ApplyCPValues(MainFrm.CP)
            MainFrm.Adjust_Preview(False)
            MainFrm.ReValidateLivePreview(MainFrm.pnl_preview)
        End If

        Cursor = Cursors.Default

        MsgBox(My.Lang.SettingsSaved, MsgBoxStyle.Information)
    End Sub

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

            With sets
                .AutoAddExt = XenonCheckBox1.Checked
                .DragAndDropPreview = XenonCheckBox3.Checked
                .OpeningPreviewInApp_or_AppliesIt = XenonRadioButton1.Checked
                .AutoRestartExplorer = XenonCheckBox2.Checked
                .AutoApplyCursors = XenonCheckBox7.Checked
                .ResetCursorsToAero = XenonCheckBox16.Checked

                .AutoUpdatesChecking = XenonCheckBox5.Checked
                .Win7LivePreview = XenonCheckBox9.Checked
                .UpdateChannel = XenonComboBox2.SelectedIndex

                .Appearance_Dark = XenonRadioButton3.Checked
                .Appearance_Auto = XenonCheckBox6.Checked
                .Appearance_Custom = appearance_enabled.Checked
                .Appearance_SchemeName = appearance_list.SelectedItem
                .Appearance_Custom_Dark = appearance_dark.Checked
                .Appearance_Accent = appearance_accent.BackColor
                .Appearance_Back = appearance_backcolor.BackColor
                .Appearance_Rounded = appearance_rounded.Checked

                .ShowSaveConfirmation = XenonCheckBox17.Checked
                .SaveForLegacyWP = XenonCheckBox4.Checked

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

                .Log_ShowApplying = XenonCheckBox19.Checked
                .Log_Countdown_Enabled = XenonCheckBox18.Checked
                .Log_Countdown = XenonNumericUpDown1.Value

                .EP_Enabled = XenonCheckBox20.Checked
                .EP_Enabled_Force = XenonCheckBox21.Checked
                .EP_UseStart10 = EP_Start_10.Checked
                .EP_StartStyle = EP_Start_10_Type.SelectedIndex
                .EP_UseTaskbar10 = EP_Taskbar_10.Checked
                .EP_TaskbarButton10 = EP_ORB_10.Checked
                .DelayMetrics = XenonCheckBox22.Checked

                If XenonRadioButton5.Checked Then .ClassicColors_HKU_DEFAULT_Prefs = XeSettings.OverwriteOptions.Overwrite Else .ClassicColors_HKU_DEFAULT_Prefs = XeSettings.OverwriteOptions.DontChange
                If XenonRadioButton8.Checked Then .ClassicColors_HKLM_Prefs = XeSettings.OverwriteOptions.Overwrite
                If XenonRadioButton10.Checked Then .ClassicColors_HKLM_Prefs = XeSettings.OverwriteOptions.DontChange
                If XenonRadioButton9.Checked Then .ClassicColors_HKLM_Prefs = XeSettings.OverwriteOptions.RestoreDefaults
                If XenonRadioButton7.Checked Then .ClassicColors_HKLM_Prefs = XeSettings.OverwriteOptions.Erase
                .ClassicColors_HKU_DEFAULT_UPM = XenonCheckBox25.Checked
                If XenonRadioButton12.Checked Then .Metrics_HKU_DEFAULT_Prefs = XeSettings.OverwriteOptions.Overwrite Else .Metrics_HKU_DEFAULT_Prefs = XeSettings.OverwriteOptions.DontChange
                If XenonRadioButton14.Checked Then .Cursors_HKU_DEFAULT_Prefs = XeSettings.OverwriteOptions.Overwrite Else .Cursors_HKU_DEFAULT_Prefs = XeSettings.OverwriteOptions.DontChange
                If XenonRadioButton16.Checked Then .CMD_HKU_DEFAULT_Prefs = XeSettings.OverwriteOptions.Overwrite Else .CMD_HKU_DEFAULT_Prefs = XeSettings.OverwriteOptions.DontChange
                If XenonRadioButton18.Checked Then .PS86_HKU_DEFAULT_Prefs = XeSettings.OverwriteOptions.Overwrite Else .PS86_HKU_DEFAULT_Prefs = XeSettings.OverwriteOptions.DontChange
                If XenonRadioButton20.Checked Then .PS64_HKU_DEFAULT_Prefs = XeSettings.OverwriteOptions.Overwrite Else .PS64_HKU_DEFAULT_Prefs = XeSettings.OverwriteOptions.DontChange

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
                XenonCheckBox16.Checked = .ResetCursorsToAero

                XenonCheckBox5.Checked = .AutoUpdatesChecking
                XenonCheckBox9.Checked = .Win7LivePreview
                XenonCheckBox17.Checked = .ShowSaveConfirmation
                XenonCheckBox4.Checked = .SaveForLegacyWP

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
                appearance_enabled.Checked = .Appearance_Custom
                If appearance_list.Items.Contains(.Appearance_SchemeName) Then appearance_list.SelectedItem = .Appearance_SchemeName
                appearance_dark.Checked = .Appearance_Custom_Dark
                appearance_accent.BackColor = .Appearance_Accent
                appearance_backcolor.BackColor = .Appearance_Back
                appearance_rounded.Checked = .Appearance_Rounded

                XenonCheckBox6.Checked = .Appearance_Auto
                XenonCheckBox8.Checked = .Language
                XenonTextBox3.Text = .Language_File

                XenonCheckBox19.Checked = .Log_ShowApplying
                XenonCheckBox18.Checked = .Log_Countdown_Enabled
                XenonNumericUpDown1.Value = .Log_Countdown

                XenonCheckBox20.Checked = .EP_Enabled
                XenonCheckBox21.Checked = .EP_Enabled_Force
                EP_Start_10.Checked = .EP_UseStart10
                EP_Start_11.Checked = Not .EP_UseStart10
                EP_Start_10_Type.SelectedIndex = .EP_StartStyle
                EP_Taskbar_10.Checked = .EP_UseTaskbar10
                EP_Taskbar_11.Checked = Not .EP_UseTaskbar10
                EP_ORB_10.Checked = .EP_TaskbarButton10
                EP_ORB_11.Checked = Not .EP_TaskbarButton10

                XenonCheckBox22.Checked = .DelayMetrics

                XenonRadioButton5.Checked = .ClassicColors_HKU_DEFAULT_Prefs = XeSettings.OverwriteOptions.Overwrite
                XenonRadioButton6.Checked = Not (.ClassicColors_HKU_DEFAULT_Prefs = XeSettings.OverwriteOptions.Overwrite)
                XenonRadioButton8.Checked = .ClassicColors_HKLM_Prefs = XeSettings.OverwriteOptions.Overwrite
                XenonRadioButton10.Checked = .ClassicColors_HKLM_Prefs = XeSettings.OverwriteOptions.DontChange
                XenonRadioButton9.Checked = .ClassicColors_HKLM_Prefs = XeSettings.OverwriteOptions.RestoreDefaults
                XenonRadioButton7.Checked = .ClassicColors_HKLM_Prefs = XeSettings.OverwriteOptions.Erase
                XenonCheckBox25.Checked = .ClassicColors_HKU_DEFAULT_UPM
                XenonRadioButton12.Checked = .Metrics_HKU_DEFAULT_Prefs = XeSettings.OverwriteOptions.Overwrite
                XenonRadioButton11.Checked = Not (.Metrics_HKU_DEFAULT_Prefs = XeSettings.OverwriteOptions.Overwrite)
                XenonRadioButton14.Checked = .Cursors_HKU_DEFAULT_Prefs = XeSettings.OverwriteOptions.Overwrite
                XenonRadioButton13.Checked = Not (.Cursors_HKU_DEFAULT_Prefs = XeSettings.OverwriteOptions.Overwrite)
                XenonRadioButton16.Checked = .CMD_HKU_DEFAULT_Prefs = XeSettings.OverwriteOptions.Overwrite
                XenonRadioButton15.Checked = Not (.CMD_HKU_DEFAULT_Prefs = XeSettings.OverwriteOptions.Overwrite)
                XenonRadioButton18.Checked = .PS86_HKU_DEFAULT_Prefs = XeSettings.OverwriteOptions.Overwrite
                XenonRadioButton17.Checked = Not (.PS86_HKU_DEFAULT_Prefs = XeSettings.OverwriteOptions.Overwrite)
                XenonRadioButton20.Checked = .PS64_HKU_DEFAULT_Prefs = XeSettings.OverwriteOptions.Overwrite
                XenonRadioButton19.Checked = Not (.PS64_HKU_DEFAULT_Prefs = XeSettings.OverwriteOptions.Overwrite)
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
            XenonCheckBox16.Checked = .ResetCursorsToAero

            XenonCheckBox5.Checked = .AutoUpdatesChecking
            XenonCheckBox9.Checked = .Win7LivePreview
            XenonCheckBox17.Checked = .ShowSaveConfirmation
            XenonCheckBox4.Checked = .SaveForLegacyWP

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
            appearance_enabled.Checked = .Appearance_Custom
            If appearance_list.Items.Contains(.Appearance_SchemeName) Then appearance_list.SelectedItem = .Appearance_SchemeName
            appearance_dark.Checked = .Appearance_Custom_Dark
            appearance_accent.BackColor = .Appearance_Accent
            appearance_backcolor.BackColor = .Appearance_Back
            appearance_rounded.Checked = .Appearance_Rounded

            XenonCheckBox8.Checked = .Language
            XenonTextBox3.Text = .Language_File

            XenonCheckBox19.Checked = .Log_ShowApplying
            XenonCheckBox18.Checked = .Log_Countdown_Enabled
            XenonNumericUpDown1.Value = .Log_Countdown

            XenonCheckBox20.Checked = .EP_Enabled
            XenonCheckBox21.Checked = .EP_Enabled_Force
            EP_Start_10.Checked = .EP_UseStart10
            EP_Start_11.Checked = Not .EP_UseStart10
            EP_Start_10_Type.SelectedIndex = .EP_StartStyle
            EP_Taskbar_10.Checked = .EP_UseTaskbar10
            EP_Taskbar_11.Checked = Not .EP_UseTaskbar10
            EP_ORB_10.Checked = .EP_TaskbarButton10
            EP_ORB_11.Checked = Not .EP_TaskbarButton10

            XenonCheckBox22.Checked = .DelayMetrics

            XenonRadioButton5.Checked = .ClassicColors_HKU_DEFAULT_Prefs = XeSettings.OverwriteOptions.Overwrite
            XenonRadioButton6.Checked = Not (.ClassicColors_HKU_DEFAULT_Prefs = XeSettings.OverwriteOptions.Overwrite)
            XenonRadioButton8.Checked = .ClassicColors_HKLM_Prefs = XeSettings.OverwriteOptions.Overwrite
            XenonRadioButton10.Checked = .ClassicColors_HKLM_Prefs = XeSettings.OverwriteOptions.DontChange
            XenonRadioButton9.Checked = .ClassicColors_HKLM_Prefs = XeSettings.OverwriteOptions.RestoreDefaults
            XenonRadioButton7.Checked = .ClassicColors_HKLM_Prefs = XeSettings.OverwriteOptions.Erase
            XenonCheckBox25.Checked = .ClassicColors_HKU_DEFAULT_UPM
            XenonRadioButton12.Checked = .Metrics_HKU_DEFAULT_Prefs = XeSettings.OverwriteOptions.Overwrite
            XenonRadioButton11.Checked = Not (.Metrics_HKU_DEFAULT_Prefs = XeSettings.OverwriteOptions.Overwrite)
            XenonRadioButton14.Checked = .Cursors_HKU_DEFAULT_Prefs = XeSettings.OverwriteOptions.Overwrite
            XenonRadioButton13.Checked = Not (.Cursors_HKU_DEFAULT_Prefs = XeSettings.OverwriteOptions.Overwrite)
            XenonRadioButton16.Checked = .CMD_HKU_DEFAULT_Prefs = XeSettings.OverwriteOptions.Overwrite
            XenonRadioButton15.Checked = Not (.CMD_HKU_DEFAULT_Prefs = XeSettings.OverwriteOptions.Overwrite)
            XenonRadioButton18.Checked = .PS86_HKU_DEFAULT_Prefs = XeSettings.OverwriteOptions.Overwrite
            XenonRadioButton17.Checked = Not (.PS86_HKU_DEFAULT_Prefs = XeSettings.OverwriteOptions.Overwrite)
            XenonRadioButton20.Checked = .PS64_HKU_DEFAULT_Prefs = XeSettings.OverwriteOptions.Overwrite
            XenonRadioButton19.Checked = Not (.PS64_HKU_DEFAULT_Prefs = XeSettings.OverwriteOptions.Overwrite)
        End With

        OpenFileDialog1.FileName = files(0)
    End Sub

    Private Sub XenonButton5_Click(sender As Object, e As EventArgs) Handles XenonButton5.Click
        If MsgBox(My.Lang.RemoveExtMsg, MsgBoxStyle.Question + MsgBoxStyle.YesNo, "", My.Lang.CollapseNote, My.Lang.ExpandNote, My.Lang.RemoveExtMsgNote) = MsgBoxResult.Yes Then

            XenonCheckBox1.Checked = False
            My.Application.DeleteFileAssociation(".wpth", "WinPaletter.ThemeFile")
            My.Application.DeleteFileAssociation(".wpsf", "WinPaletter.SettingsFile")
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

    Private Sub Appearance_accent_Click(sender As Object, e As EventArgs) Handles appearance_accent.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            appearance_accent.BackColor = SubMenu.ShowMenu(appearance_accent)
            Exit Sub
        End If

        ColorPickerDlg.Pick(New List(Of Control) From {appearance_accent})
    End Sub

    Private Sub appearance_backcolor_Click(sender As Object, e As EventArgs) Handles appearance_backcolor.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            appearance_backcolor.BackColor = SubMenu.ShowMenu(appearance_backcolor)
            Exit Sub
        End If

        ColorPickerDlg.Pick(New List(Of Control) From {appearance_backcolor})
    End Sub

    Private Sub XenonButton13_Click(sender As Object, e As EventArgs) Handles XenonButton13.Click
        Select Case appearance_list.SelectedItem.ToString.ToLower
            Case "Default Dark".ToLower
                appearance_dark.Checked = True
                appearance_rounded.Checked = (My.W11 Or My.W7)
                appearance_accent.BackColor = DefaultAccent
                appearance_backcolor.BackColor = DefaultBackColorDark

            Case "Default Light".ToLower
                appearance_dark.Checked = False
                appearance_rounded.Checked = (My.W11 Or My.W7)
                appearance_accent.BackColor = DefaultAccent
                appearance_backcolor.BackColor = DefaultBackColorLight

            Case "AMOLED".ToLower
                appearance_dark.Checked = True
                appearance_rounded.Checked = (My.W11 Or My.W7)
                appearance_accent.BackColor = DefaultAccent
                appearance_backcolor.BackColor = Color.Black

            Case "Extreme White".ToLower
                appearance_dark.Checked = False
                appearance_rounded.Checked = (My.W11 Or My.W7)
                appearance_accent.BackColor = DefaultAccent
                appearance_backcolor.BackColor = Color.White

            Case "GitHub Dark".ToLower
                appearance_dark.Checked = True
                appearance_rounded.Checked = True
                appearance_accent.BackColor = Color.FromArgb(19, 35, 58)
                appearance_backcolor.BackColor = Color.FromArgb(13, 17, 23)

            Case "GitHub Light".ToLower
                appearance_dark.Checked = False
                appearance_rounded.Checked = True
                appearance_accent.BackColor = Color.FromArgb(31, 111, 235)
                appearance_backcolor.BackColor = Color.FromArgb(246, 248, 250)

            Case "Reddit Dark".ToLower
                appearance_dark.Checked = True
                appearance_rounded.Checked = True
                appearance_accent.BackColor = Color.FromArgb(255, 70, 0)
                appearance_backcolor.BackColor = Color.FromArgb(9, 9, 9)

            Case "Reddit Light".ToLower
                appearance_dark.Checked = False
                appearance_rounded.Checked = True
                appearance_accent.BackColor = Color.FromArgb(255, 70, 0)
                appearance_backcolor.BackColor = Color.FromArgb(242, 242, 242)

            Case "Discord Dark".ToLower
                appearance_dark.Checked = True
                appearance_rounded.Checked = False
                appearance_accent.BackColor = Color.FromArgb(65, 71, 78)
                appearance_backcolor.BackColor = Color.FromArgb(32, 34, 38)

            Case "Discord Light".ToLower
                appearance_dark.Checked = False
                appearance_rounded.Checked = False
                appearance_accent.BackColor = Color.FromArgb(138, 140, 143)
                appearance_backcolor.BackColor = Color.FromArgb(255, 255, 255)

        End Select
    End Sub
End Class