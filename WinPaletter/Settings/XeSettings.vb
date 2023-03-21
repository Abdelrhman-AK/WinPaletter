Imports System.Drawing.Drawing2D
Imports System.Windows.Interop
Imports Microsoft.Win32
Public Class XeSettings

#Region "Settings"
    Public Property LicenseAccepted As Boolean = False
    Public Property AutoAddExt As Boolean = True
    Public Property AutoApplyCursors As Boolean = True
    Public Property ResetCursorsToAero As Boolean = My.WXP
    Public Property DragAndDropPreview As Boolean = True
    Public Property OpeningPreviewInApp_or_AppliesIt As Boolean = True
    Public Property AutoRestartExplorer As Boolean = True
    Public Property AutoUpdatesChecking As Boolean = True
    Public Property ComplexSaveResult As String = "2.1"
    Public Property ShowSaveConfirmation As Boolean = True
    Public Property SaveForLegacyWP As Boolean = False
    Public Property Win7LivePreview As Boolean = True
    Public Property UpdateChannel As UpdateChannels = UpdateChannels.Stable   ' Don't forget to make it beta when you design a beta one

    Public Property Appearance_Dark As Boolean = True
    Public Property Appearance_Auto As Boolean = True
    Public Property Appearance_Custom As Boolean = False
    Public Property Appearance_SchemeName As String = "Default Dark"
    Public Property Appearance_Custom_Dark As Boolean = True
    Public Property Appearance_Accent As Color = Color.FromArgb(0, 81, 210)
    Public Property Appearance_Back As Color = Color.FromArgb(25, 25, 25)
    Public Property Appearance_Rounded As Boolean = True
    Public Property WhatsNewRecord As String() = {""}
    Public Property Language As Boolean = False
    Public Property Language_File As String = Nothing
    Public Property Nerd_Stats As Boolean = True
    Public Property Nerd_Stats_Kind As Nerd_Stats_Type = Nerd_Stats_Type.HEX
    Public Property Nerd_Stats_HexHash As Boolean = True
    Public Property Terminal_Bypass As Boolean = False
    Public Property Terminal_OtherFonts As Boolean = False

    Public Property Terminal_Path_Deflection As Boolean = False
    Public Property Terminal_Stable_Path As String = My.PATH_TerminalJSON
    Public Property Terminal_Preview_Path As String = My.PATH_TerminalPreviewJSON
    Public Property CMD_OverrideUserPreferences As Boolean = True

    Public Property MainFormWidth As Integer = 1110
    Public Property MainFormHeight As Integer = 725
    Public Property MainFormStatus As FormWindowState = FormWindowState.Normal

    Public Property Log_ShowApplying As Boolean = True
    Public Property Log_Countdown_Enabled As Boolean = True
    Public Property Log_Countdown As Integer = 20

    Public Property EP_Enabled As Boolean = True
    Public Property EP_Enabled_Force As Boolean = False
    Public Property EP_UseStart10 As Boolean = False
    Public Property EP_UseTaskbar10 As Boolean = False
    Public Property EP_TaskbarButton10 As Boolean = False
    Public Property EP_StartStyle As ExplorerPatcher.StartStyles
    Public Property DelayMetrics As Boolean = False
    Public Property ClassicColors_HKU_DEFAULT_Prefs As OverwriteOptions = OverwriteOptions.Overwrite
    Public Property ClassicColors_HKLM_Prefs As OverwriteOptions = OverwriteOptions.Erase
    Public Property UPM_HKU_DEFAULT As Boolean = True
    Public Property Metrics_HKU_DEFAULT_Prefs As OverwriteOptions = OverwriteOptions.DontChange
    Public Property Cursors_HKU_DEFAULT_Prefs As OverwriteOptions = OverwriteOptions.DontChange
    Public Property CMD_HKU_DEFAULT_Prefs As OverwriteOptions = OverwriteOptions.DontChange
    Public Property PS86_HKU_DEFAULT_Prefs As OverwriteOptions = OverwriteOptions.DontChange
    Public Property PS64_HKU_DEFAULT_Prefs As OverwriteOptions = OverwriteOptions.DontChange
    Public Property Desktop_HKU_DEFAULT As OverwriteOptions = OverwriteOptions.Overwrite
#End Region

    Public Enum OverwriteOptions
        DontChange
        Overwrite
        RestoreDefaults
        [Erase]
    End Enum

    Public Enum Nerd_Stats_Type
        HEX
        RGB
        HSL
        Dec
    End Enum

    Public Enum UpdateChannels
        Stable
        Beta
    End Enum

    Enum Mode
        Registry
        File
        Empty
    End Enum

    Sub CheckRegIfIntact()
        Dim Key As RegistryKey
        Dim AppReg As String = "Software\WinPaletter\Settings"
        Key = Registry.CurrentUser.CreateSubKey(AppReg)
        If Key.GetValue("LicenseAccepted", Nothing) Is Nothing Then Key.SetValue("LicenseAccepted", False, RegistryValueKind.DWord)

        If Key.GetValue("AutoUpdatesChecking", Nothing) Is Nothing Then Key.SetValue("AutoUpdatesChecking", True, RegistryValueKind.DWord)
        If Key.GetValue("AutoAddExt", Nothing) Is Nothing Then Key.SetValue("AutoAddExt", True, RegistryValueKind.DWord)
        If Key.GetValue("DragAndDropPreview", Nothing) Is Nothing Then Key.SetValue("DragAndDropPreview", True, RegistryValueKind.DWord)
        If Key.GetValue("Win7LivePreview", Nothing) Is Nothing Then Key.SetValue("Win7LivePreview", True, RegistryValueKind.DWord)
        If Key.GetValue("OpeningPreviewInApp_or_AppliesIt", Nothing) Is Nothing Then Key.SetValue("OpeningPreviewInApp_or_AppliesIt", True, RegistryValueKind.DWord)
        If Key.GetValue("AutoRestartExplorer", Nothing) Is Nothing Then Key.SetValue("AutoRestartExplorer", True, RegistryValueKind.DWord)
        If Key.GetValue("AutoApplyCursors", Nothing) Is Nothing Then Key.SetValue("AutoApplyCursors", True, RegistryValueKind.DWord)
        If Key.GetValue("ResetCursorsToAero", Nothing) Is Nothing Then Key.SetValue("ResetCursorsToAero", My.WXP, RegistryValueKind.DWord)
        If Key.GetValue("CustomPreviewConfig_Enabled", Nothing) Is Nothing Then Key.SetValue("CustomPreviewConfig_Enabled", False, RegistryValueKind.DWord)
        If Key.GetValue("ShowLogWhileSaving", Nothing) Is Nothing Then Key.SetValue("ShowLogWhileSaving", False, RegistryValueKind.DWord)
        If Key.GetValue("ComplexSaveResult", Nothing) Is Nothing Then Key.SetValue("ComplexSaveResult", "2.1", RegistryValueKind.String)
        If Key.GetValue("ShowSaveConfirmation", Nothing) Is Nothing Then Key.SetValue("ShowSaveConfirmation", True, RegistryValueKind.DWord)
        If Key.GetValue("SaveForLegacyWP", Nothing) Is Nothing Then Key.SetValue("SaveForLegacyWP", False, RegistryValueKind.DWord)
        If Key.GetValue("MainFormWidth", Nothing) Is Nothing Then Key.SetValue("MainFormWidth", 1110, RegistryValueKind.DWord)
        If Key.GetValue("MainFormHeight", Nothing) Is Nothing Then Key.SetValue("MainFormHeight", 725, RegistryValueKind.DWord)
        If Key.GetValue("MainFormStatus", Nothing) Is Nothing Then Key.SetValue("MainFormStatus", FormWindowState.Normal, RegistryValueKind.DWord)
        If Key.GetValue("UpdateChannel", Nothing) Is Nothing Then Key.SetValue("UpdateChannel", If(UpdateChannel = UpdateChannels.Stable, 0, 1))

        If Key.GetValue("Appearance_Dark", Nothing) Is Nothing Then Key.SetValue("Appearance_Dark", True, RegistryValueKind.DWord)
        If Key.GetValue("Appearance_Auto", Nothing) Is Nothing Then Key.SetValue("Appearance_Auto", True, RegistryValueKind.DWord)
        If Key.GetValue("Appearance_Custom", Nothing) Is Nothing Then Key.SetValue("Appearance_Custom", False, RegistryValueKind.DWord)
        If Key.GetValue("Appearance_SchemeName", Nothing) Is Nothing Then Key.SetValue("Appearance_SchemeName", "Default Dark", RegistryValueKind.String)
        If Key.GetValue("Appearance_Custom_Dark", Nothing) Is Nothing Then Key.SetValue("Appearance_Custom_Dark", True, RegistryValueKind.DWord)
        If Key.GetValue("Appearance_Accent", Nothing) Is Nothing Then Key.SetValue("Appearance_Accent", Color.FromArgb(0, 81, 210).ToArgb, RegistryValueKind.DWord)
        If Key.GetValue("Appearance_Back", Nothing) Is Nothing Then Key.SetValue("Appearance_Back", Color.FromArgb(25, 25, 25).ToArgb, RegistryValueKind.DWord)
        If Key.GetValue("Appearance_Rounded", Nothing) Is Nothing Then Key.SetValue("Appearance_Rounded", True, RegistryValueKind.DWord)

        If Key.GetValue("WhatsNewRecord", Nothing) Is Nothing Then Key.SetValue("WhatsNewRecord", {""}, RegistryValueKind.MultiString)
        If Key.GetValue("Language", Nothing) Is Nothing Then Key.SetValue("Language", False, RegistryValueKind.DWord)
        If Key.GetValue("Language_File", Nothing) Is Nothing Then Key.SetValue("Language_File", "", RegistryValueKind.String)
        If Key.GetValue("Nerd_Stats", Nothing) Is Nothing Then Key.SetValue("Nerd_Stats", True, RegistryValueKind.DWord)
        If Key.GetValue("Nerd_Stats_HexHash", Nothing) Is Nothing Then Key.SetValue("Nerd_Stats_HexHash", True, RegistryValueKind.DWord)

        Select Case Nerd_Stats_Kind
            Case Nerd_Stats_Type.HEX
                If Key.GetValue("Nerd_Stats_Kind", Nothing) Is Nothing Then Key.SetValue("Nerd_Stats_Kind", 0)
            Case Nerd_Stats_Type.RGB
                If Key.GetValue("Nerd_Stats_Kind", Nothing) Is Nothing Then Key.SetValue("Nerd_Stats_Kind", 1)
            Case Nerd_Stats_Type.HSL
                If Key.GetValue("Nerd_Stats_Kind", Nothing) Is Nothing Then Key.SetValue("Nerd_Stats_Kind", 2)
            Case Nerd_Stats_Type.Dec
                If Key.GetValue("Nerd_Stats_Kind", Nothing) Is Nothing Then Key.SetValue("Nerd_Stats_Kind", 3)
        End Select

        If Key.GetValue("Terminal_Bypass", Nothing) Is Nothing Then Key.SetValue("Terminal_Bypass", False, RegistryValueKind.DWord)
        If Key.GetValue("Terminal_OtherFonts", Nothing) Is Nothing Then Key.SetValue("Terminal_OtherFonts", False, RegistryValueKind.DWord)
        If Key.GetValue("Terminal_Path_Deflection", Nothing) Is Nothing Then Key.SetValue("Terminal_Path_Deflection", False, RegistryValueKind.DWord)
        If Key.GetValue("Terminal_Stable_Path", Nothing) Is Nothing Then Key.SetValue("Terminal_Stable_Path", My.PATH_TerminalJSON, RegistryValueKind.String)
        If Key.GetValue("Terminal_Preview_Path", Nothing) Is Nothing Then Key.SetValue("Terminal_Preview_Path", My.PATH_TerminalPreviewJSON, RegistryValueKind.String)
        If Key.GetValue("CMD_OverrideUserPreferences", Nothing) Is Nothing Then Key.SetValue("CMD_OverrideUserPreferences", True, RegistryValueKind.DWord)

        If Key.GetValue("Log_ShowApplying", Nothing) Is Nothing Then Key.SetValue("Log_ShowApplying", True, RegistryValueKind.DWord)
        If Key.GetValue("Log_Countdown_Enabled", Nothing) Is Nothing Then Key.SetValue("Log_Countdown_Enabled", True, RegistryValueKind.DWord)
        If Key.GetValue("Log_Countdown", Nothing) Is Nothing Then Key.SetValue("Log_Countdown", 15, RegistryValueKind.DWord)

        If Key.GetValue("EP_Enabled", Nothing) Is Nothing Then Key.SetValue("EP_Enabled", True, RegistryValueKind.DWord)
        If Key.GetValue("EP_Enabled_Force", Nothing) Is Nothing Then Key.SetValue("EP_Enabled_Force", False, RegistryValueKind.DWord)
        If Key.GetValue("EP_UseStart10", Nothing) Is Nothing Then Key.SetValue("EP_UseStart10", False, RegistryValueKind.DWord)
        If Key.GetValue("EP_UseTaskbar10", Nothing) Is Nothing Then Key.SetValue("EP_UseTaskbar10", False, RegistryValueKind.DWord)
        If Key.GetValue("EP_TaskbarButton10", Nothing) Is Nothing Then Key.SetValue("EP_TaskbarButton10", False, RegistryValueKind.DWord)
        If Key.GetValue("EP_StartStyle", Nothing) Is Nothing Then Key.SetValue("EP_StartStyle", ExplorerPatcher.StartStyles.NotRounded, RegistryValueKind.DWord)

        If Key.GetValue("DelayMetrics", Nothing) Is Nothing Then Key.SetValue("DelayMetrics", False, RegistryValueKind.DWord)
        If Key.GetValue("ClassicColors_HKU_DEFAULT_Prefs", Nothing) Is Nothing Then Key.SetValue("ClassicColors_HKU_DEFAULT_Prefs", OverwriteOptions.Overwrite, RegistryValueKind.DWord)
        If Key.GetValue("ClassicColors_HKLM_Prefs", Nothing) Is Nothing Then Key.SetValue("ClassicColors_HKLM_Prefs", OverwriteOptions.Erase, RegistryValueKind.DWord)
        If Key.GetValue("UPM_HKU_DEFAULT", Nothing) Is Nothing Then Key.SetValue("UPM_HKU_DEFAULT", True, RegistryValueKind.DWord)
        If Key.GetValue("Metrics_HKU_DEFAULT_Prefs", Nothing) Is Nothing Then Key.SetValue("Metrics_HKU_DEFAULT_Prefs", OverwriteOptions.DontChange, RegistryValueKind.DWord)
        If Key.GetValue("Cursors_HKU_DEFAULT_Prefs", Nothing) Is Nothing Then Key.SetValue("Cursors_HKU_DEFAULT_Prefs", OverwriteOptions.DontChange, RegistryValueKind.DWord)
        If Key.GetValue("CMD_HKU_DEFAULT_Prefs", Nothing) Is Nothing Then Key.SetValue("CMD_HKU_DEFAULT_Prefs", OverwriteOptions.DontChange, RegistryValueKind.DWord)
        If Key.GetValue("PS86_HKU_DEFAULT_Prefs", Nothing) Is Nothing Then Key.SetValue("PS86_HKU_DEFAULT_Prefs", OverwriteOptions.DontChange, RegistryValueKind.DWord)
        If Key.GetValue("PS64_HKU_DEFAULT_Prefs", Nothing) Is Nothing Then Key.SetValue("PS64_HKU_DEFAULT_Prefs", OverwriteOptions.DontChange, RegistryValueKind.DWord)
        If Key.GetValue("Desktop_HKU_DEFAULT", Nothing) Is Nothing Then Key.SetValue("Desktop_HKU_DEFAULT", OverwriteOptions.DontChange, RegistryValueKind.DWord)

    End Sub

    Sub New(ByVal LoadFrom As Mode, Optional ByVal File As String = Nothing)
        Select Case LoadFrom
            Case Mode.Registry
                CheckRegIfIntact()

                Dim Key As RegistryKey
                Dim AppReg As String = "Software\WinPaletter\Settings"
                Key = Registry.CurrentUser.CreateSubKey(AppReg)

                LicenseAccepted = Key.GetValue("LicenseAccepted", False)

                AutoAddExt = Key.GetValue("AutoAddExt", True)
                AutoApplyCursors = Key.GetValue("AutoApplyCursors", True)
                ResetCursorsToAero = Key.GetValue("ResetCursorsToAero", My.WXP)

                DragAndDropPreview = Key.GetValue("DragAndDropPreview", True)
                OpeningPreviewInApp_or_AppliesIt = Key.GetValue("OpeningPreviewInApp_or_AppliesIt", True)
                AutoRestartExplorer = Key.GetValue("AutoRestartExplorer", True)
                Win7LivePreview = Key.GetValue("Win7LivePreview", True)
                AutoUpdatesChecking = Key.GetValue("AutoUpdatesChecking", True)

                ComplexSaveResult = Key.GetValue("ComplexSaveResult", "2.1")
                ShowSaveConfirmation = Key.GetValue("ShowSaveConfirmation", True)
                SaveForLegacyWP = Key.GetValue("SaveForLegacyWP", False)

                MainFormWidth = Key.GetValue("MainFormWidth", 1110)
                MainFormHeight = Key.GetValue("MainFormHeight", 725)
                MainFormStatus = Key.GetValue("MainFormStatus", FormWindowState.Normal)

                Terminal_Bypass = Key.GetValue("Terminal_Bypass", False)
                Terminal_OtherFonts = Key.GetValue("Terminal_OtherFonts", False)
                Terminal_Path_Deflection = Key.GetValue("Terminal_Path_Deflection", False)
                Terminal_Stable_Path = Key.GetValue("Terminal_Stable_Path", My.PATH_TerminalJSON)
                Terminal_Preview_Path = Key.GetValue("Terminal_Preview_Path", My.PATH_TerminalPreviewJSON)

                CMD_OverrideUserPreferences = Key.GetValue("CMD_OverrideUserPreferences", True)

                UpdateChannel = If(Key.GetValue("UpdateChannel", UpdateChannels.Stable) = UpdateChannels.Stable, UpdateChannels.Stable, UpdateChannels.Beta)

                Appearance_Dark = Key.GetValue("Appearance_Dark", True)
                Appearance_Auto = Key.GetValue("Appearance_Auto", True)
                Appearance_Custom = Key.GetValue("Appearance_Custom", False)
                Appearance_SchemeName = Key.GetValue("Appearance_SchemeName", "Default Dark")
                Appearance_Custom_Dark = Key.GetValue("Appearance_Custom_Dark", True)
                Appearance_Accent = Color.FromArgb(Key.GetValue("Appearance_Accent", Color.FromArgb(0, 81, 210).ToArgb))
                Appearance_Back = Color.FromArgb(Key.GetValue("Appearance_Back", Color.FromArgb(25, 25, 25).ToArgb))
                Appearance_Rounded = Key.GetValue("Appearance_Rounded", True)

                WhatsNewRecord = Key.GetValue("WhatsNewRecord", {""})
                Language = Key.GetValue("Language", False)
                Language_File = Key.GetValue("Language_File", "")

                Nerd_Stats = Key.GetValue("Nerd_Stats", True)
                Nerd_Stats_HexHash = Key.GetValue("Nerd_Stats_HexHash", True)

                Select Case Key.GetValue("Nerd_Stats_Kind", Nerd_Stats_Type.HEX)
                    Case 0
                        Nerd_Stats_Kind = Nerd_Stats_Type.HEX
                    Case 1
                        Nerd_Stats_Kind = Nerd_Stats_Type.RGB
                    Case 2
                        Nerd_Stats_Kind = Nerd_Stats_Type.HSL
                    Case 3
                        Nerd_Stats_Kind = Nerd_Stats_Type.Dec
                End Select

                Log_ShowApplying = Key.GetValue("Log_ShowApplying", True)
                Log_Countdown_Enabled = Key.GetValue("Log_Countdown_Enabled", True)
                Log_Countdown = Key.GetValue("Log_Countdown", 15)

                EP_Enabled = Key.GetValue("EP_Enabled", True)
                EP_Enabled_Force = Key.GetValue("EP_Enabled_Force", False)
                EP_UseStart10 = Key.GetValue("EP_UseStart10", False)
                EP_UseTaskbar10 = Key.GetValue("EP_UseTaskbar10", False)
                EP_TaskbarButton10 = Key.GetValue("EP_TaskbarButton10", False)
                EP_StartStyle = Key.GetValue("EP_StartStyle", ExplorerPatcher.StartStyles.NotRounded)

                DelayMetrics = Key.GetValue("DelayMetrics", False)

                ClassicColors_HKU_DEFAULT_Prefs = Key.GetValue("ClassicColors_HKU_DEFAULT_Prefs", OverwriteOptions.Overwrite)
                ClassicColors_HKLM_Prefs = Key.GetValue("ClassicColors_HKLM_Prefs", OverwriteOptions.Erase)
                UPM_HKU_DEFAULT = Key.GetValue("UPM_HKU_DEFAULT", True)
                Metrics_HKU_DEFAULT_Prefs = Key.GetValue("Metrics_HKU_DEFAULT_Prefs", OverwriteOptions.DontChange)
                Cursors_HKU_DEFAULT_Prefs = Key.GetValue("Cursors_HKU_DEFAULT_Prefs", OverwriteOptions.DontChange)
                CMD_HKU_DEFAULT_Prefs = Key.GetValue("CMD_HKU_DEFAULT_Prefs", OverwriteOptions.DontChange)
                PS86_HKU_DEFAULT_Prefs = Key.GetValue("PS86_HKU_DEFAULT_Prefs", OverwriteOptions.DontChange)
                PS64_HKU_DEFAULT_Prefs = Key.GetValue("PS64_HKU_DEFAULT_Prefs", OverwriteOptions.DontChange)
                Desktop_HKU_DEFAULT = Key.GetValue("Desktop_HKU_DEFAULT", OverwriteOptions.DontChange)

            Case Mode.File
                Dim l As List(Of String) = IO.File.ReadAllText(File).CList
                For Each x As String In l
                    If x.StartsWith("AutoAddExt= ", My._strIgnore) Then AutoAddExt = x.Remove(0, "AutoAddExt= ".Count)
                    If x.StartsWith("AutoApplyCursors= ", My._strIgnore) Then AutoApplyCursors = x.Remove(0, "AutoApplyCursors= ".Count)
                    If x.StartsWith("ResetCursorsToAero= ", My._strIgnore) Then ResetCursorsToAero = x.Remove(0, "ResetCursorsToAero= ".Count)
                    If x.StartsWith("DragAndDropPreview= ", My._strIgnore) Then DragAndDropPreview = x.Remove(0, "DragAndDropPreview= ".Count)
                    If x.StartsWith("OpeningPreviewInApp_or_AppliesIt= ", My._strIgnore) Then OpeningPreviewInApp_or_AppliesIt = x.Remove(0, "OpeningPreviewInApp_or_AppliesIt= ".Count)
                    If x.StartsWith("AutoRestartExplorer= ", My._strIgnore) Then AutoRestartExplorer = x.Remove(0, "AutoRestartExplorer= ".Count)
                    If x.StartsWith("AutoUpdatesChecking= ", My._strIgnore) Then AutoUpdatesChecking = x.Remove(0, "AutoUpdatesChecking= ".Count)
                    If x.StartsWith("LoadThemeFileAsLegacy= ", My._strIgnore) Then AutoUpdatesChecking = x.Remove(0, "LoadThemeFileAsLegacy= ".Count)
                    If x.StartsWith("SaveThemeFileAsLegacy= ", My._strIgnore) Then AutoUpdatesChecking = x.Remove(0, "SaveThemeFileAsLegacy= ".Count)
                    If x.StartsWith("ComplexSaveResult= ", My._strIgnore) Then ComplexSaveResult = x.Remove(0, "ComplexSaveResult= ".Count)
                    If x.StartsWith("ShowSaveConfirmation= ", My._strIgnore) Then ShowSaveConfirmation = x.Remove(0, "ShowSaveConfirmation= ".Count)
                    If x.StartsWith("SaveForLegacyWP= ", My._strIgnore) Then SaveForLegacyWP = x.Remove(0, "SaveForLegacyWP= ".Count)
                    If x.StartsWith("Win7LivePreview= ", My._strIgnore) Then Win7LivePreview = x.Remove(0, "Win7LivePreview= ".Count)
                    If x.StartsWith("UpdateChannel= ", My._strIgnore) Then UpdateChannel = x.Remove(0, "UpdateChannel= ".Count)

                    If x.StartsWith("Appearance_Dark= ", My._strIgnore) Then Appearance_Dark = x.Remove(0, "Appearance_Dark= ".Count)
                    If x.StartsWith("Appearance_Auto= ", My._strIgnore) Then Appearance_Auto = x.Remove(0, "Appearance_Auto= ".Count)
                    If x.StartsWith("Appearance_Custom= ", My._strIgnore) Then Appearance_Custom = x.Remove(0, "Appearance_Custom= ".Count)
                    If x.StartsWith("Appearance_SchemeName= ", My._strIgnore) Then Appearance_SchemeName = x.Remove(0, "Appearance_SchemeName= ".Count)
                    If x.StartsWith("Appearance_Custom_Dark= ", My._strIgnore) Then Appearance_Custom_Dark = x.Remove(0, "Appearance_Custom_Dark= ".Count)
                    If x.StartsWith("Appearance_Accent= ", My._strIgnore) Then Appearance_Accent = Color.FromArgb(x.Remove(0, "Appearance_Accent= ".Count))
                    If x.StartsWith("Appearance_Back= ", My._strIgnore) Then Appearance_Back = Color.FromArgb(x.Remove(0, "Appearance_Back= ".Count))
                    If x.StartsWith("Appearance_Rounded= ", My._strIgnore) Then Appearance_Rounded = x.Remove(0, "Appearance_Rounded= ".Count)

                    If x.StartsWith("Language= ", My._strIgnore) Then Language = x.Remove(0, "Language= ".Count)
                    If x.StartsWith("Language_File= ", My._strIgnore) Then Language_File = x.Remove(0, "Language_File= ".Count)
                    If x.StartsWith("Nerd_Stats= ", My._strIgnore) Then Nerd_Stats = x.Remove(0, "Nerd_Stats= ".Count)
                    If x.StartsWith("Nerd_Stats_HexHash= ", My._strIgnore) Then Nerd_Stats_HexHash = x.Remove(0, "Nerd_Stats_HexHash= ".Count)
                    If x.StartsWith("Nerd_Stats_Kind= ", My._strIgnore) Then Nerd_Stats_Kind = x.Remove(0, "Nerd_Stats_Kind= ".Count)
                    If x.StartsWith("Terminal_Bypass= ", My._strIgnore) Then Terminal_Bypass = x.Remove(0, "Terminal_Bypass= ".Count)
                    If x.StartsWith("Terminal_OtherFonts= ", My._strIgnore) Then Terminal_OtherFonts = x.Remove(0, "Terminal_OtherFonts= ".Count)
                    If x.StartsWith("Terminal_Path_Deflection= ", My._strIgnore) Then Terminal_Path_Deflection = x.Remove(0, "Terminal_Path_Deflection= ".Count)
                    If x.StartsWith("Terminal_Stable_Path= ", My._strIgnore) Then Terminal_Stable_Path = x.Remove(0, "Terminal_Stable_Path= ".Count)
                    If x.StartsWith("Terminal_Preview_Path= ", My._strIgnore) Then Terminal_Preview_Path = x.Remove(0, "Terminal_Preview_Path= ".Count)
                    If x.StartsWith("CMD_OverrideUserPreferences= ", My._strIgnore) Then CMD_OverrideUserPreferences = x.Remove(0, "CMD_OverrideUserPreferences= ".Count)
                    If x.StartsWith("Log_ShowApplying= ", My._strIgnore) Then Log_ShowApplying = x.Remove(0, "Log_ShowApplying= ".Count)
                    If x.StartsWith("Log_Countdown_Enabled= ", My._strIgnore) Then Log_Countdown_Enabled = x.Remove(0, "Log_Countdown_Enabled= ".Count)
                    If x.StartsWith("Log_Countdown= ", My._strIgnore) Then Log_Countdown = x.Remove(0, "Log_Countdown= ".Count)

                    If x.StartsWith("EP_Enabled= ", My._strIgnore) Then EP_Enabled = x.Remove(0, "EP_Enabled= ".Count)
                    If x.StartsWith("EP_Enabled_Force= ", My._strIgnore) Then EP_Enabled_Force = x.Remove(0, "EP_Enabled_Force= ".Count)
                    If x.StartsWith("EP_UseStart10= ", My._strIgnore) Then EP_UseStart10 = x.Remove(0, "EP_UseStart10= ".Count)
                    If x.StartsWith("EP_UseTaskbar10= ", My._strIgnore) Then EP_UseTaskbar10 = x.Remove(0, "EP_UseTaskbar10= ".Count)
                    If x.StartsWith("EP_TaskbarButton10= ", My._strIgnore) Then EP_TaskbarButton10 = x.Remove(0, "EP_TaskbarButton10= ".Count)
                    If x.StartsWith("EP_StartStyle= ", My._strIgnore) Then EP_StartStyle = x.Remove(0, "EP_StartStyle= ".Count)

                    If x.StartsWith("DelayMetrics= ", My._strIgnore) Then DelayMetrics = x.Remove(0, "DelayMetrics= ".Count)

                    If x.StartsWith("ClassicColors_HKU_DEFAULT_Prefs= ", My._strIgnore) Then ClassicColors_HKU_DEFAULT_Prefs = x.Remove(0, "ClassicColors_HKU_DEFAULT_Prefs= ".Count)
                    If x.StartsWith("ClassicColors_HKLM_Prefs= ", My._strIgnore) Then ClassicColors_HKLM_Prefs = x.Remove(0, "ClassicColors_HKLM_Prefs= ".Count)
                    If x.StartsWith("UPM_HKU_DEFAULT= ", My._strIgnore) Then UPM_HKU_DEFAULT = x.Remove(0, "UPM_HKU_DEFAULT= ".Count)
                    If x.StartsWith("Metrics_HKU_DEFAULT_Prefs= ", My._strIgnore) Then Metrics_HKU_DEFAULT_Prefs = x.Remove(0, "Metrics_HKU_DEFAULT_Prefs= ".Count)
                    If x.StartsWith("Cursors_HKU_DEFAULT_Prefs= ", My._strIgnore) Then Cursors_HKU_DEFAULT_Prefs = x.Remove(0, "Cursors_HKU_DEFAULT_Prefs= ".Count)

                    If x.StartsWith("CMD_HKU_DEFAULT_Prefs= ", My._strIgnore) Then CMD_HKU_DEFAULT_Prefs = x.Remove(0, "CMD_HKU_DEFAULT_Prefs= ".Count)
                    If x.StartsWith("PS86_HKU_DEFAULT_Prefs= ", My._strIgnore) Then PS86_HKU_DEFAULT_Prefs = x.Remove(0, "PS86_HKU_DEFAULT_Prefs= ".Count)
                    If x.StartsWith("PS64_HKU_DEFAULT_Prefs= ", My._strIgnore) Then PS64_HKU_DEFAULT_Prefs = x.Remove(0, "PS64_HKU_DEFAULT_Prefs= ".Count)
                    If x.StartsWith("Desktop_HKU_DEFAULT= ", My._strIgnore) Then Desktop_HKU_DEFAULT = x.Remove(0, "Desktop_HKU_DEFAULT= ".Count)

                Next
        End Select
    End Sub

    Sub Save(ByVal SaveTo As Mode, Optional ByVal File As String = Nothing)
        Select Case SaveTo
            Case Mode.Registry
                Dim Key As RegistryKey
                Dim AppReg As String = "Software\WinPaletter\Settings"
                Key = Registry.CurrentUser.CreateSubKey(AppReg)

                Key.SetValue("LicenseAccepted", LicenseAccepted, RegistryValueKind.DWord)

                Key.SetValue("AutoAddExt", AutoAddExt, RegistryValueKind.DWord)
                Key.SetValue("AutoApplyCursors", AutoApplyCursors, RegistryValueKind.DWord)
                Key.SetValue("ResetCursorsToAero", ResetCursorsToAero, RegistryValueKind.DWord)

                Key.SetValue("DragAndDropPreview", DragAndDropPreview, RegistryValueKind.DWord)
                Key.SetValue("OpeningPreviewInApp_or_AppliesIt", OpeningPreviewInApp_or_AppliesIt, RegistryValueKind.DWord)
                Key.SetValue("AutoRestartExplorer", AutoRestartExplorer, RegistryValueKind.DWord)
                Key.SetValue("AutoUpdatesChecking", AutoUpdatesChecking, RegistryValueKind.DWord)
                Key.SetValue("ComplexSaveResult", ComplexSaveResult, RegistryValueKind.String)
                Key.SetValue("ShowSaveConfirmation", ShowSaveConfirmation, RegistryValueKind.DWord)
                Key.SetValue("Win7LivePreview", Win7LivePreview, RegistryValueKind.DWord)
                Key.SetValue("Terminal_Bypass", Terminal_Bypass, RegistryValueKind.DWord)
                Key.SetValue("Terminal_OtherFonts", Terminal_OtherFonts, RegistryValueKind.DWord)
                Key.SetValue("Terminal_Path_Deflection", Terminal_Path_Deflection, RegistryValueKind.DWord)
                Key.SetValue("Terminal_Stable_Path", Terminal_Stable_Path, RegistryValueKind.String)
                Key.SetValue("Terminal_Preview_Path", Terminal_Preview_Path, RegistryValueKind.String)
                Key.SetValue("CMD_OverrideUserPreferences", CMD_OverrideUserPreferences, RegistryValueKind.DWord)
                Key.SetValue("SaveForLegacyWP", SaveForLegacyWP, RegistryValueKind.DWord)

                Key.SetValue("UpdateChannel", If(UpdateChannel = UpdateChannels.Stable, 0, 1))

                Key.SetValue("Appearance_Dark", Appearance_Dark, RegistryValueKind.DWord)
                Key.SetValue("Appearance_Auto", Appearance_Auto, RegistryValueKind.DWord)
                Key.SetValue("Appearance_Custom", Appearance_Custom, RegistryValueKind.DWord)
                Key.SetValue("Appearance_SchemeName", Appearance_SchemeName, RegistryValueKind.String)
                Key.SetValue("Appearance_Custom_Dark", Appearance_Custom_Dark, RegistryValueKind.DWord)
                Key.SetValue("Appearance_Accent", Appearance_Accent.ToArgb, RegistryValueKind.DWord)
                Key.SetValue("Appearance_Back", Appearance_Back.ToArgb, RegistryValueKind.DWord)
                Key.SetValue("Appearance_Rounded", Appearance_Rounded, RegistryValueKind.DWord)

                Key.SetValue("WhatsNewRecord", WhatsNewRecord, RegistryValueKind.MultiString)
                Key.SetValue("Language", Language, RegistryValueKind.DWord)
                Key.SetValue("Language_File", Language_File, RegistryValueKind.String)
                Key.SetValue("Nerd_Stats", Nerd_Stats, RegistryValueKind.DWord)
                Key.SetValue("Nerd_Stats_HexHash", Nerd_Stats_HexHash, RegistryValueKind.DWord)

                Key.SetValue("MainFormWidth", MainFormWidth, RegistryValueKind.DWord)
                Key.SetValue("MainFormHeight", MainFormHeight, RegistryValueKind.DWord)
                Key.SetValue("MainFormStatus", MainFormStatus, RegistryValueKind.DWord)

                Key.SetValue("Log_ShowApplying", Log_ShowApplying, RegistryValueKind.DWord)
                Key.SetValue("Log_Countdown_Enabled", Log_Countdown_Enabled, RegistryValueKind.DWord)
                Key.SetValue("Log_Countdown", Log_Countdown, RegistryValueKind.DWord)

                Select Case Nerd_Stats_Kind
                    Case Nerd_Stats_Type.HEX
                        Key.SetValue("Nerd_Stats_Kind", 0)
                    Case Nerd_Stats_Type.RGB
                        Key.SetValue("Nerd_Stats_Kind", 1)
                    Case Nerd_Stats_Type.HSL
                        Key.SetValue("Nerd_Stats_Kind", 2)
                    Case Nerd_Stats_Type.Dec
                        Key.SetValue("Nerd_Stats_Kind", 3)
                End Select

                Key.SetValue("EP_Enabled", EP_Enabled, RegistryValueKind.DWord)
                Key.SetValue("EP_Enabled_Force", EP_Enabled_Force, RegistryValueKind.DWord)
                Key.SetValue("EP_UseStart10", EP_UseStart10, RegistryValueKind.DWord)
                Key.SetValue("EP_UseTaskbar10", EP_UseTaskbar10, RegistryValueKind.DWord)
                Key.SetValue("EP_TaskbarButton10", EP_TaskbarButton10, RegistryValueKind.DWord)
                Key.SetValue("EP_StartStyle", EP_StartStyle, RegistryValueKind.DWord)

                Key.SetValue("DelayMetrics", DelayMetrics, RegistryValueKind.DWord)

                Key.SetValue("ClassicColors_HKU_DEFAULT_Prefs", ClassicColors_HKU_DEFAULT_Prefs, RegistryValueKind.DWord)
                Key.SetValue("ClassicColors_HKLM_Prefs", ClassicColors_HKLM_Prefs, RegistryValueKind.DWord)
                Key.SetValue("UPM_HKU_DEFAULT", UPM_HKU_DEFAULT, RegistryValueKind.DWord)
                Key.SetValue("Metrics_HKU_DEFAULT_Prefs", Metrics_HKU_DEFAULT_Prefs, RegistryValueKind.DWord)
                Key.SetValue("Cursors_HKU_DEFAULT_Prefs", Cursors_HKU_DEFAULT_Prefs, RegistryValueKind.DWord)

                Key.SetValue("CMD_HKU_DEFAULT_Prefs", CMD_HKU_DEFAULT_Prefs, RegistryValueKind.DWord)
                Key.SetValue("PS86_HKU_DEFAULT_Prefs", PS86_HKU_DEFAULT_Prefs, RegistryValueKind.DWord)
                Key.SetValue("PS64_HKU_DEFAULT_Prefs", PS64_HKU_DEFAULT_Prefs, RegistryValueKind.DWord)
                Key.SetValue("Desktop_HKU_DEFAULT", Desktop_HKU_DEFAULT, RegistryValueKind.DWord)


            Case Mode.File
                Dim l As New List(Of String)
                l.Clear()
                l.Add("WinPaletter[Settings]_Exported")
                l.Add(String.Format("Date: {0}", Now))
                l.Add("")
                l.Add(String.Format("AutoAddExt= {0}", AutoAddExt))
                l.Add(String.Format("AutoApplyCursors= {0}", AutoApplyCursors))
                l.Add(String.Format("ResetCursorsToAero= {0}", ResetCursorsToAero))

                l.Add(String.Format("DragAndDropPreview= {0}", DragAndDropPreview))
                l.Add(String.Format("OpeningPreviewInApp_or_AppliesIt= {0}", OpeningPreviewInApp_or_AppliesIt))
                l.Add(String.Format("AutoRestartExplorer= {0}", AutoRestartExplorer))
                l.Add(String.Format("AutoUpdatesChecking= {0}", AutoUpdatesChecking))
                l.Add(String.Format("ComplexSaveResult= {0}", ComplexSaveResult))
                l.Add(String.Format("ShowSaveConfirmation= {0}", ShowSaveConfirmation))
                l.Add(String.Format("Win7LivePreview= {0}", Win7LivePreview))
                l.Add(String.Format("Terminal_Bypass= {0}", Terminal_Bypass))
                l.Add(String.Format("Terminal_OtherFonts= {0}", Terminal_OtherFonts))
                l.Add(String.Format("Terminal_Path_Deflection= {0}", Terminal_Path_Deflection))
                l.Add(String.Format("Terminal_Stable_Path= {0}", Terminal_Stable_Path))
                l.Add(String.Format("Terminal_Preview_Path= {0}", Terminal_Preview_Path))
                l.Add(String.Format("CMD_OverrideUserPreferences= {0}", CMD_OverrideUserPreferences))
                l.Add(String.Format("SaveForLegacyWP= {0}", SaveForLegacyWP))

                l.Add(String.Format("UpdateChannel= {0}", If(UpdateChannel = UpdateChannels.Stable, 0, 1)))

                l.Add(String.Format("Appearance_Dark= {0}", Appearance_Dark))
                l.Add(String.Format("Appearance_Auto= {0}", Appearance_Auto))
                l.Add(String.Format("Appearance_Custom= {0}", Appearance_Custom))
                l.Add(String.Format("Appearance_SchemeName= {0}", Appearance_SchemeName))
                l.Add(String.Format("Appearance_Custom_Dark= {0}", Appearance_Custom_Dark))
                l.Add(String.Format("Appearance_Accent= {0}", Appearance_Accent.ToArgb))
                l.Add(String.Format("Appearance_Back= {0}", Appearance_Back.ToArgb))
                l.Add(String.Format("Appearance_Rounded= {0}", Appearance_Rounded))

                l.Add(String.Format("Language= {0}", Language))
                l.Add(String.Format("Language_File= {0}", Language_File))
                l.Add(String.Format("Nerd_Stats= {0}", Nerd_Stats))
                l.Add(String.Format("Nerd_Stats_HexHash= {0}", Nerd_Stats_HexHash))

                l.Add(String.Format("Log_ShowApplying= {0}", Log_ShowApplying))
                l.Add(String.Format("Log_Countdown_Enabled= {0}", Log_Countdown_Enabled))
                l.Add(String.Format("Log_Countdown= {0}", Log_Countdown))


                Select Case Nerd_Stats_Kind
                    Case Nerd_Stats_Type.HEX
                        l.Add(String.Format("Nerd_Stats_Kind= {0}", 0))
                    Case Nerd_Stats_Type.HEX
                        l.Add(String.Format("Nerd_Stats_Kind= {0}", 1))
                    Case Nerd_Stats_Type.HSL
                        l.Add(String.Format("Nerd_Stats_Kind= {0}", 2))
                    Case Nerd_Stats_Type.Dec
                        l.Add(String.Format("Nerd_Stats_Kind= {0}", 3))
                End Select

                l.Add(String.Format("EP_Enabled= {0}", EP_Enabled))
                l.Add(String.Format("EP_Enabled_Force= {0}", EP_Enabled_Force))
                l.Add(String.Format("EP_UseStart10= {0}", EP_UseStart10))
                l.Add(String.Format("EP_UseTaskbar10= {0}", EP_UseTaskbar10))
                l.Add(String.Format("EP_TaskbarButton10= {0}", EP_TaskbarButton10))
                l.Add(String.Format("EP_StartStyle= {0}", EP_StartStyle))

                l.Add(String.Format("DelayMetrics= {0}", DelayMetrics))
                l.Add(String.Format("ClassicColors_HKU_DEFAULT_Prefs= {0}", ClassicColors_HKU_DEFAULT_Prefs))
                l.Add(String.Format("ClassicColors_HKLM_Prefs= {0}", ClassicColors_HKLM_Prefs))
                l.Add(String.Format("UPM_HKU_DEFAULT= {0}", UPM_HKU_DEFAULT))
                l.Add(String.Format("Metrics_HKU_DEFAULT_Prefs= {0}", Metrics_HKU_DEFAULT_Prefs))
                l.Add(String.Format("Cursors_HKU_DEFAULT_Prefs= {0}", Cursors_HKU_DEFAULT_Prefs))

                l.Add(String.Format("CMD_HKU_DEFAULT_Prefs= {0}", CMD_HKU_DEFAULT_Prefs))
                l.Add(String.Format("PS86_HKU_DEFAULT_Prefs= {0}", PS86_HKU_DEFAULT_Prefs))
                l.Add(String.Format("PS64_HKU_DEFAULT_Prefs= {0}", PS64_HKU_DEFAULT_Prefs))
                l.Add(String.Format("Desktop_HKU_DEFAULT= {0}", Desktop_HKU_DEFAULT))

                IO.File.WriteAllText(File, l.CString)
        End Select
    End Sub
End Class