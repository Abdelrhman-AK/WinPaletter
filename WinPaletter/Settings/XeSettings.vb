Imports Microsoft.Win32
Imports WinPaletter.Reg_IO

Public Class XeSettings

    Public Const REG As String = "HKEY_CURRENT_USER\Software\WinPaletter\Settings"

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
    Public Property CompressThemeFile As Boolean = True
    Public Property AlwaysExportThemePack As Boolean = False
    Public Property Win7LivePreview As Boolean = True
    Public Property UpdateChannel As UpdateChannels = If(My.IsBeta, UpdateChannels.Beta, UpdateChannels.Stable)

    Public Property Appearance_Dark As Boolean = True
    Public Property Appearance_Auto As Boolean = True
    Public Property Appearance_Custom As Boolean = False
    Public Property Appearance_SchemeName As String = "Default Dark"
    Public Property Appearance_Custom_Dark As Boolean = True
    Public Property Appearance_Accent As Color = Color.FromArgb(0, 81, 210)
    Public Property Appearance_Back As Color = Color.FromArgb(25, 25, 25)
    Public Property Appearance_Rounded As Boolean = True
    Public Property Appearance_ManagedByTheme As Boolean = True

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

    Public Property Store_Search_ThemeNames As Boolean = True
    Public Property Store_Search_AuthorsNames As Boolean = True
    Public Property Store_Search_Descriptions As Boolean = True
    Public Property Store_Online_or_Offline As Boolean = True
    Public Property Store_Online_Repositories As String() = {My.Resources.Link_StoreMainDB, My.Resources.Link_StoreReposDB}
    Public Property Store_Offline_Directories As String()
    Public Property Store_Offline_SubFolders As Boolean = True

    Public Property Classic_Color_Picker As Boolean = False
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

    Sub New(ByVal LoadFrom As Mode, Optional ByVal File As String = Nothing)
        Select Case LoadFrom
            Case Mode.Registry

                LicenseAccepted = GetReg(REG, "LicenseAccepted", False)
                WhatsNewRecord = GetReg(REG, "WhatsNewRecord", {""})
                ComplexSaveResult = GetReg(REG, "ComplexSaveResult", "2.1")

                AutoUpdatesChecking = GetReg(REG, "AutoUpdatesChecking", True)
                UpdateChannel = If(GetReg(REG, "UpdateChannel", UpdateChannels.Stable) = UpdateChannels.Stable, UpdateChannels.Stable, UpdateChannels.Beta)

                Language = GetReg(REG, "Language", False)
                Language_File = GetReg(REG, "Language_File", "")

                Appearance_Dark = GetReg(REG, "Appearance_Dark", True)
                Appearance_Auto = GetReg(REG, "Appearance_Auto", True)
                Appearance_Custom = GetReg(REG, "Appearance_Custom", False)
                Appearance_SchemeName = GetReg(REG, "Appearance_SchemeName", "Default Dark")
                Appearance_Custom_Dark = GetReg(REG, "Appearance_Custom_Dark", True)
                Appearance_Accent = Color.FromArgb(GetReg(REG, "Appearance_Accent", Color.FromArgb(0, 81, 210).ToArgb))
                Appearance_Back = Color.FromArgb(GetReg(REG, "Appearance_Back", Color.FromArgb(25, 25, 25).ToArgb))
                Appearance_Rounded = GetReg(REG, "Appearance_Rounded", True)
                Appearance_ManagedByTheme = GetReg(REG, "Appearance_ManagedByTheme", True)

                AutoAddExt = GetReg(REG, "AutoAddExt", True)
                DragAndDropPreview = GetReg(REG, "DragAndDropPreview", True)
                OpeningPreviewInApp_or_AppliesIt = GetReg(REG, "OpeningPreviewInApp_or_AppliesIt", True)
                SaveForLegacyWP = GetReg(REG, "SaveForLegacyWP", False)
                AlwaysExportThemePack = GetReg(REG, "AlwaysExportThemePack", False)
                CompressThemeFile = GetReg(REG, "CompressThemeFile", True)

                AutoRestartExplorer = GetReg(REG, "AutoRestartExplorer", True)
                ShowSaveConfirmation = GetReg(REG, "ShowSaveConfirmation", True)
                ClassicColors_HKU_DEFAULT_Prefs = GetReg(REG, "ClassicColors_HKU_DEFAULT_Prefs", OverwriteOptions.Overwrite)
                ClassicColors_HKLM_Prefs = GetReg(REG, "ClassicColors_HKLM_Prefs", OverwriteOptions.Erase)
                UPM_HKU_DEFAULT = GetReg(REG, "UPM_HKU_DEFAULT", True)
                Metrics_HKU_DEFAULT_Prefs = GetReg(REG, "Metrics_HKU_DEFAULT_Prefs", OverwriteOptions.DontChange)
                Cursors_HKU_DEFAULT_Prefs = GetReg(REG, "Cursors_HKU_DEFAULT_Prefs", OverwriteOptions.DontChange)
                CMD_HKU_DEFAULT_Prefs = GetReg(REG, "CMD_HKU_DEFAULT_Prefs", OverwriteOptions.DontChange)
                PS86_HKU_DEFAULT_Prefs = GetReg(REG, "PS86_HKU_DEFAULT_Prefs", OverwriteOptions.DontChange)
                PS64_HKU_DEFAULT_Prefs = GetReg(REG, "PS64_HKU_DEFAULT_Prefs", OverwriteOptions.DontChange)
                Desktop_HKU_DEFAULT = GetReg(REG, "Desktop_HKU_DEFAULT", OverwriteOptions.DontChange)
                CMD_OverrideUserPreferences = GetReg(REG, "CMD_OverrideUserPreferences", True)
                AutoApplyCursors = GetReg(REG, "AutoApplyCursors", True)
                ResetCursorsToAero = GetReg(REG, "ResetCursorsToAero", My.WXP)
                DelayMetrics = GetReg(REG, "DelayMetrics", False)

                Store_Online_or_Offline = GetReg(REG, "Store_Online_or_Offline", True)
                Store_Online_Repositories = GetReg(REG, "Store_Online_Repositories", {My.Resources.Link_StoreMainDB, My.Resources.Link_StoreReposDB})
                Store_Offline_Directories = GetReg(REG, "Store_Offline_Directories", {""})
                Store_Offline_SubFolders = GetReg(REG, "Store_Offline_SubFolders", True)
                Store_Search_ThemeNames = GetReg(REG, "Store_Search_ThemeNames", True)
                Store_Search_AuthorsNames = GetReg(REG, "Store_Search_AuthorsNames", True)
                Store_Search_Descriptions = GetReg(REG, "Store_Search_Descriptions", True)

                Log_ShowApplying = GetReg(REG, "Log_ShowApplying", True)
                Log_Countdown_Enabled = GetReg(REG, "Log_Countdown_Enabled", True)
                Log_Countdown = GetReg(REG, "Log_Countdown", 15)

                Terminal_Bypass = GetReg(REG, "Terminal_Bypass", False)
                Terminal_OtherFonts = GetReg(REG, "Terminal_OtherFonts", False)
                Terminal_Path_Deflection = GetReg(REG, "Terminal_Path_Deflection", False)
                Terminal_Stable_Path = GetReg(REG, "Terminal_Stable_Path", My.PATH_TerminalJSON)
                Terminal_Preview_Path = GetReg(REG, "Terminal_Preview_Path", My.PATH_TerminalPreviewJSON)

                EP_Enabled = GetReg(REG, "EP_Enabled", True)
                EP_Enabled_Force = GetReg(REG, "EP_Enabled_Force", False)
                EP_UseStart10 = GetReg(REG, "EP_UseStart10", False)
                EP_UseTaskbar10 = GetReg(REG, "EP_UseTaskbar10", False)
                EP_TaskbarButton10 = GetReg(REG, "EP_TaskbarButton10", False)
                EP_StartStyle = GetReg(REG, "EP_StartStyle", ExplorerPatcher.StartStyles.NotRounded)

                Win7LivePreview = GetReg(REG, "Win7LivePreview", True)
                Nerd_Stats = GetReg(REG, "Nerd_Stats", True)
                Nerd_Stats_HexHash = GetReg(REG, "Nerd_Stats_HexHash", True)
                Nerd_Stats_Kind = GetReg(REG, "Nerd_Stats_Kind", Nerd_Stats_Type.HEX)

                MainFormWidth = GetReg(REG, "MainFormWidth", 1110)
                MainFormHeight = GetReg(REG, "MainFormHeight", 725)
                MainFormStatus = GetReg(REG, "MainFormStatus", FormWindowState.Normal)

                Classic_Color_Picker = GetReg(REG, "Classic_Color_Picker", False)

            Case Mode.File
                Dim l As List(Of String) = IO.File.ReadAllText(File).CList
                For Each x As String In l
                    If x.StartsWith("ComplexSaveResult= ", My._ignore) Then ComplexSaveResult = x.Remove(0, "ComplexSaveResult= ".Count)

                    If x.StartsWith("AutoUpdatesChecking= ", My._ignore) Then AutoUpdatesChecking = x.Remove(0, "AutoUpdatesChecking= ".Count)
                    If x.StartsWith("UpdateChannel= ", My._ignore) Then UpdateChannel = x.Remove(0, "UpdateChannel= ".Count)

                    If x.StartsWith("Language= ", My._ignore) Then Language = x.Remove(0, "Language= ".Count)
                    If x.StartsWith("Language_File= ", My._ignore) Then Language_File = x.Remove(0, "Language_File= ".Count)

                    If x.StartsWith("Appearance_Dark= ", My._ignore) Then Appearance_Dark = x.Remove(0, "Appearance_Dark= ".Count)
                    If x.StartsWith("Appearance_Auto= ", My._ignore) Then Appearance_Auto = x.Remove(0, "Appearance_Auto= ".Count)
                    If x.StartsWith("Appearance_Custom= ", My._ignore) Then Appearance_Custom = x.Remove(0, "Appearance_Custom= ".Count)
                    If x.StartsWith("Appearance_SchemeName= ", My._ignore) Then Appearance_SchemeName = x.Remove(0, "Appearance_SchemeName= ".Count)
                    If x.StartsWith("Appearance_Custom_Dark= ", My._ignore) Then Appearance_Custom_Dark = x.Remove(0, "Appearance_Custom_Dark= ".Count)
                    If x.StartsWith("Appearance_Accent= ", My._ignore) Then Appearance_Accent = Color.FromArgb(x.Remove(0, "Appearance_Accent= ".Count))
                    If x.StartsWith("Appearance_Back= ", My._ignore) Then Appearance_Back = Color.FromArgb(x.Remove(0, "Appearance_Back= ".Count))
                    If x.StartsWith("Appearance_Rounded= ", My._ignore) Then Appearance_Rounded = x.Remove(0, "Appearance_Rounded= ".Count)
                    If x.StartsWith("Appearance_ManagedByTheme= ", My._ignore) Then Appearance_ManagedByTheme = x.Remove(0, "Appearance_ManagedByTheme= ".Count)

                    If x.StartsWith("AutoAddExt= ", My._ignore) Then AutoAddExt = x.Remove(0, "AutoAddExt= ".Count)
                    If x.StartsWith("DragAndDropPreview= ", My._ignore) Then DragAndDropPreview = x.Remove(0, "DragAndDropPreview= ".Count)
                    If x.StartsWith("OpeningPreviewInApp_or_AppliesIt= ", My._ignore) Then OpeningPreviewInApp_or_AppliesIt = x.Remove(0, "OpeningPreviewInApp_or_AppliesIt= ".Count)
                    If x.StartsWith("SaveForLegacyWP= ", My._ignore) Then SaveForLegacyWP = x.Remove(0, "SaveForLegacyWP= ".Count)
                    If x.StartsWith("AlwaysExportThemePack= ", My._ignore) Then AlwaysExportThemePack = x.Remove(0, "AlwaysExportThemePack= ".Count)
                    If x.StartsWith("CompressThemeFile= ", My._ignore) Then CompressThemeFile = x.Remove(0, "CompressThemeFile= ".Count)

                    If x.StartsWith("AutoRestartExplorer= ", My._ignore) Then AutoRestartExplorer = x.Remove(0, "AutoRestartExplorer= ".Count)
                    If x.StartsWith("ShowSaveConfirmation= ", My._ignore) Then ShowSaveConfirmation = x.Remove(0, "ShowSaveConfirmation= ".Count)
                    If x.StartsWith("ClassicColors_HKU_DEFAULT_Prefs= ", My._ignore) Then ClassicColors_HKU_DEFAULT_Prefs = x.Remove(0, "ClassicColors_HKU_DEFAULT_Prefs= ".Count)
                    If x.StartsWith("ClassicColors_HKLM_Prefs= ", My._ignore) Then ClassicColors_HKLM_Prefs = x.Remove(0, "ClassicColors_HKLM_Prefs= ".Count)
                    If x.StartsWith("UPM_HKU_DEFAULT= ", My._ignore) Then UPM_HKU_DEFAULT = x.Remove(0, "UPM_HKU_DEFAULT= ".Count)
                    If x.StartsWith("Metrics_HKU_DEFAULT_Prefs= ", My._ignore) Then Metrics_HKU_DEFAULT_Prefs = x.Remove(0, "Metrics_HKU_DEFAULT_Prefs= ".Count)
                    If x.StartsWith("Cursors_HKU_DEFAULT_Prefs= ", My._ignore) Then Cursors_HKU_DEFAULT_Prefs = x.Remove(0, "Cursors_HKU_DEFAULT_Prefs= ".Count)
                    If x.StartsWith("CMD_HKU_DEFAULT_Prefs= ", My._ignore) Then CMD_HKU_DEFAULT_Prefs = x.Remove(0, "CMD_HKU_DEFAULT_Prefs= ".Count)
                    If x.StartsWith("PS86_HKU_DEFAULT_Prefs= ", My._ignore) Then PS86_HKU_DEFAULT_Prefs = x.Remove(0, "PS86_HKU_DEFAULT_Prefs= ".Count)
                    If x.StartsWith("PS64_HKU_DEFAULT_Prefs= ", My._ignore) Then PS64_HKU_DEFAULT_Prefs = x.Remove(0, "PS64_HKU_DEFAULT_Prefs= ".Count)
                    If x.StartsWith("Desktop_HKU_DEFAULT= ", My._ignore) Then Desktop_HKU_DEFAULT = x.Remove(0, "Desktop_HKU_DEFAULT= ".Count)
                    If x.StartsWith("CMD_OverrideUserPreferences= ", My._ignore) Then CMD_OverrideUserPreferences = x.Remove(0, "CMD_OverrideUserPreferences= ".Count)
                    If x.StartsWith("AutoApplyCursors= ", My._ignore) Then AutoApplyCursors = x.Remove(0, "AutoApplyCursors= ".Count)
                    If x.StartsWith("ResetCursorsToAero= ", My._ignore) Then ResetCursorsToAero = x.Remove(0, "ResetCursorsToAero= ".Count)
                    If x.StartsWith("DelayMetrics= ", My._ignore) Then DelayMetrics = x.Remove(0, "DelayMetrics= ".Count)

                    If x.StartsWith("Store_Online_or_Offline= ", My._ignore) Then Store_Online_or_Offline = x.Remove(0, "Store_Online_or_Offline= ".Count)
                    If x.StartsWith("Store_Online_Repositories= ", My._ignore) Then Store_Online_Repositories = x.Remove(0, "Store_Online_Repositories= ".Count).Split("|")
                    If x.StartsWith("Store_Offline_Directories= ", My._ignore) Then Store_Offline_Directories = x.Remove(0, "Store_Offline_Directories= ".Count).Split("|")
                    If x.StartsWith("Store_Offline_SubFolders= ", My._ignore) Then Store_Offline_SubFolders = x.Remove(0, "Store_Online_or_Offline= ".Count)
                    If x.StartsWith("Store_Search_ThemeNames= ", My._ignore) Then Store_Search_ThemeNames = x.Remove(0, "Store_Search_ThemeNames= ".Count)
                    If x.StartsWith("Store_Search_AuthorsNames= ", My._ignore) Then Store_Search_AuthorsNames = x.Remove(0, "Store_Search_AuthorsNames= ".Count)
                    If x.StartsWith("Store_Search_Descriptions= ", My._ignore) Then Store_Search_Descriptions = x.Remove(0, "Store_Search_Descriptions= ".Count)

                    If x.StartsWith("Log_ShowApplying= ", My._ignore) Then Log_ShowApplying = x.Remove(0, "Log_ShowApplying= ".Count)
                    If x.StartsWith("Log_Countdown_Enabled= ", My._ignore) Then Log_Countdown_Enabled = x.Remove(0, "Log_Countdown_Enabled= ".Count)
                    If x.StartsWith("Log_Countdown= ", My._ignore) Then Log_Countdown = x.Remove(0, "Log_Countdown= ".Count)

                    If x.StartsWith("Terminal_Bypass= ", My._ignore) Then Terminal_Bypass = x.Remove(0, "Terminal_Bypass= ".Count)
                    If x.StartsWith("Terminal_OtherFonts= ", My._ignore) Then Terminal_OtherFonts = x.Remove(0, "Terminal_OtherFonts= ".Count)
                    If x.StartsWith("Terminal_Path_Deflection= ", My._ignore) Then Terminal_Path_Deflection = x.Remove(0, "Terminal_Path_Deflection= ".Count)
                    If x.StartsWith("Terminal_Stable_Path= ", My._ignore) Then Terminal_Stable_Path = x.Remove(0, "Terminal_Stable_Path= ".Count)
                    If x.StartsWith("Terminal_Preview_Path= ", My._ignore) Then Terminal_Preview_Path = x.Remove(0, "Terminal_Preview_Path= ".Count)

                    If x.StartsWith("EP_Enabled= ", My._ignore) Then EP_Enabled = x.Remove(0, "EP_Enabled= ".Count)
                    If x.StartsWith("EP_Enabled_Force= ", My._ignore) Then EP_Enabled_Force = x.Remove(0, "EP_Enabled_Force= ".Count)
                    If x.StartsWith("EP_UseStart10= ", My._ignore) Then EP_UseStart10 = x.Remove(0, "EP_UseStart10= ".Count)
                    If x.StartsWith("EP_UseTaskbar10= ", My._ignore) Then EP_UseTaskbar10 = x.Remove(0, "EP_UseTaskbar10= ".Count)
                    If x.StartsWith("EP_TaskbarButton10= ", My._ignore) Then EP_TaskbarButton10 = x.Remove(0, "EP_TaskbarButton10= ".Count)
                    If x.StartsWith("EP_StartStyle= ", My._ignore) Then EP_StartStyle = x.Remove(0, "EP_StartStyle= ".Count)

                    If x.StartsWith("Win7LivePreview= ", My._ignore) Then Win7LivePreview = x.Remove(0, "Win7LivePreview= ".Count)
                    If x.StartsWith("Nerd_Stats= ", My._ignore) Then Nerd_Stats = x.Remove(0, "Nerd_Stats= ".Count)
                    If x.StartsWith("Nerd_Stats_HexHash= ", My._ignore) Then Nerd_Stats_HexHash = x.Remove(0, "Nerd_Stats_HexHash= ".Count)
                    If x.StartsWith("Nerd_Stats_Kind= ", My._ignore) Then Nerd_Stats_Kind = x.Remove(0, "Nerd_Stats_Kind= ".Count)

                    If x.StartsWith("Classic_Color_Picker= ", My._ignore) Then Classic_Color_Picker = x.Remove(0, "Classic_Color_Picker= ".Count)
                Next
        End Select

        If Not Store_Online_Repositories.Contains(My.Resources.Link_StoreMainDB) Then
            Array.Resize(Store_Online_Repositories, Store_Online_Repositories.Length + 1)
            Store_Online_Repositories(Store_Online_Repositories.Length - 1) = My.Resources.Link_StoreMainDB
        End If

        If Not Store_Online_Repositories.Contains(My.Resources.Link_StoreReposDB) Then
            Array.Resize(Store_Online_Repositories, Store_Online_Repositories.Length + 1)
            Store_Online_Repositories(Store_Online_Repositories.Length - 1) = My.Resources.Link_StoreReposDB
        End If

    End Sub

    Sub Save(ByVal SaveTo As Mode, Optional ByVal File As String = Nothing)

        If Not Store_Online_Repositories.Contains(My.Resources.Link_StoreMainDB) Then
            Array.Resize(Store_Online_Repositories, Store_Online_Repositories.Length + 1)
            Store_Online_Repositories(Store_Online_Repositories.Length - 1) = My.Resources.Link_StoreMainDB
        End If

        If Not Store_Online_Repositories.Contains(My.Resources.Link_StoreReposDB) Then
            Array.Resize(Store_Online_Repositories, Store_Online_Repositories.Length + 1)
            Store_Online_Repositories(Store_Online_Repositories.Length - 1) = My.Resources.Link_StoreReposDB
        End If

        Select Case SaveTo
            Case Mode.Registry
                EditReg(REG, "LicenseAccepted", LicenseAccepted, RegistryValueKind.DWord)
                EditReg(REG, "WhatsNewRecord", WhatsNewRecord, RegistryValueKind.MultiString)
                EditReg(REG, "ComplexSaveResult", ComplexSaveResult, RegistryValueKind.String)

                EditReg(REG, "AutoUpdatesChecking", AutoUpdatesChecking, RegistryValueKind.DWord)
                EditReg(REG, "UpdateChannel", If(UpdateChannel = UpdateChannels.Stable, 0, 1))

                EditReg(REG, "Language", Language, RegistryValueKind.DWord)
                EditReg(REG, "Language_File", Language_File, RegistryValueKind.String)

                EditReg(REG, "Appearance_Dark", Appearance_Dark, RegistryValueKind.DWord)
                EditReg(REG, "Appearance_Auto", Appearance_Auto, RegistryValueKind.DWord)
                EditReg(REG, "Appearance_Custom", Appearance_Custom, RegistryValueKind.DWord)
                EditReg(REG, "Appearance_SchemeName", Appearance_SchemeName, RegistryValueKind.String)
                EditReg(REG, "Appearance_Custom_Dark", Appearance_Custom_Dark, RegistryValueKind.DWord)
                EditReg(REG, "Appearance_Accent", Appearance_Accent.ToArgb, RegistryValueKind.DWord)
                EditReg(REG, "Appearance_Back", Appearance_Back.ToArgb, RegistryValueKind.DWord)
                EditReg(REG, "Appearance_Rounded", Appearance_Rounded, RegistryValueKind.DWord)
                EditReg(REG, "Appearance_ManagedByTheme", Appearance_ManagedByTheme, RegistryValueKind.DWord)

                EditReg(REG, "AutoAddExt", AutoAddExt, RegistryValueKind.DWord)
                EditReg(REG, "DragAndDropPreview", DragAndDropPreview, RegistryValueKind.DWord)
                EditReg(REG, "OpeningPreviewInApp_or_AppliesIt", OpeningPreviewInApp_or_AppliesIt, RegistryValueKind.DWord)
                EditReg(REG, "SaveForLegacyWP", SaveForLegacyWP, RegistryValueKind.DWord)
                EditReg(REG, "AlwaysExportThemePack", AlwaysExportThemePack, RegistryValueKind.DWord)
                EditReg(REG, "CompressThemeFile", CompressThemeFile, RegistryValueKind.DWord)

                EditReg(REG, "AutoRestartExplorer", AutoRestartExplorer, RegistryValueKind.DWord)
                EditReg(REG, "ShowSaveConfirmation", ShowSaveConfirmation, RegistryValueKind.DWord)
                EditReg(REG, "ClassicColors_HKU_DEFAULT_Prefs", ClassicColors_HKU_DEFAULT_Prefs, RegistryValueKind.DWord)
                EditReg(REG, "ClassicColors_HKLM_Prefs", ClassicColors_HKLM_Prefs, RegistryValueKind.DWord)
                EditReg(REG, "UPM_HKU_DEFAULT", UPM_HKU_DEFAULT, RegistryValueKind.DWord)
                EditReg(REG, "Metrics_HKU_DEFAULT_Prefs", Metrics_HKU_DEFAULT_Prefs, RegistryValueKind.DWord)
                EditReg(REG, "Cursors_HKU_DEFAULT_Prefs", Cursors_HKU_DEFAULT_Prefs, RegistryValueKind.DWord)
                EditReg(REG, "CMD_HKU_DEFAULT_Prefs", CMD_HKU_DEFAULT_Prefs, RegistryValueKind.DWord)
                EditReg(REG, "PS86_HKU_DEFAULT_Prefs", PS86_HKU_DEFAULT_Prefs, RegistryValueKind.DWord)
                EditReg(REG, "PS64_HKU_DEFAULT_Prefs", PS64_HKU_DEFAULT_Prefs, RegistryValueKind.DWord)
                EditReg(REG, "Desktop_HKU_DEFAULT", Desktop_HKU_DEFAULT, RegistryValueKind.DWord)
                EditReg(REG, "AutoApplyCursors", AutoApplyCursors, RegistryValueKind.DWord)
                EditReg(REG, "ResetCursorsToAero", ResetCursorsToAero, RegistryValueKind.DWord)
                EditReg(REG, "CMD_OverrideUserPreferences", CMD_OverrideUserPreferences, RegistryValueKind.DWord)
                EditReg(REG, "DelayMetrics", DelayMetrics, RegistryValueKind.DWord)

                EditReg(REG, "Store_Search_ThemeNames", Store_Search_ThemeNames, RegistryValueKind.DWord)
                EditReg(REG, "Store_Search_AuthorsNames", Store_Search_AuthorsNames, RegistryValueKind.DWord)
                EditReg(REG, "Store_Search_Descriptions", Store_Search_Descriptions, RegistryValueKind.DWord)
                EditReg(REG, "Store_Online_or_Offline", Store_Online_or_Offline, RegistryValueKind.DWord)
                EditReg(REG, "Store_Online_Repositories", Store_Online_Repositories, RegistryValueKind.MultiString)
                EditReg(REG, "Store_Offline_Directories", Store_Offline_Directories, RegistryValueKind.MultiString)
                EditReg(REG, "Store_Offline_SubFolders", Store_Offline_SubFolders, RegistryValueKind.DWord)

                EditReg(REG, "Log_ShowApplying", Log_ShowApplying, RegistryValueKind.DWord)
                EditReg(REG, "Log_Countdown_Enabled", Log_Countdown_Enabled, RegistryValueKind.DWord)
                EditReg(REG, "Log_Countdown", Log_Countdown, RegistryValueKind.DWord)

                EditReg(REG, "Terminal_Bypass", Terminal_Bypass, RegistryValueKind.DWord)
                EditReg(REG, "Terminal_OtherFonts", Terminal_OtherFonts, RegistryValueKind.DWord)
                EditReg(REG, "Terminal_Path_Deflection", Terminal_Path_Deflection, RegistryValueKind.DWord)
                EditReg(REG, "Terminal_Stable_Path", Terminal_Stable_Path, RegistryValueKind.String)
                EditReg(REG, "Terminal_Preview_Path", Terminal_Preview_Path, RegistryValueKind.String)

                EditReg(REG, "EP_Enabled", EP_Enabled, RegistryValueKind.DWord)
                EditReg(REG, "EP_Enabled_Force", EP_Enabled_Force, RegistryValueKind.DWord)
                EditReg(REG, "EP_UseStart10", EP_UseStart10, RegistryValueKind.DWord)
                EditReg(REG, "EP_UseTaskbar10", EP_UseTaskbar10, RegistryValueKind.DWord)
                EditReg(REG, "EP_TaskbarButton10", EP_TaskbarButton10, RegistryValueKind.DWord)
                EditReg(REG, "EP_StartStyle", EP_StartStyle, RegistryValueKind.DWord)

                EditReg(REG, "Win7LivePreview", Win7LivePreview, RegistryValueKind.DWord)
                EditReg(REG, "Nerd_Stats", Nerd_Stats, RegistryValueKind.DWord)
                EditReg(REG, "Nerd_Stats_HexHash", Nerd_Stats_HexHash, RegistryValueKind.DWord)
                EditReg(REG, "Nerd_Stats_Kind", CInt(Nerd_Stats_Kind))

                EditReg(REG, "MainFormWidth", MainFormWidth, RegistryValueKind.DWord)
                EditReg(REG, "MainFormHeight", MainFormHeight, RegistryValueKind.DWord)
                EditReg(REG, "MainFormStatus", MainFormStatus, RegistryValueKind.DWord)

                EditReg(REG, "Classic_Color_Picker", Classic_Color_Picker, RegistryValueKind.DWord)

            Case Mode.File
                Dim l As New List(Of String)
                l.Clear()
                l.Add("WinPaletter_Settings_Exported")
                l.Add(String.Format("Date: {0}", Now))
                l.Add("")

                l.Add(String.Format("ComplexSaveResult= {0}", ComplexSaveResult))

                l.Add(String.Format("AutoUpdatesChecking= {0}", AutoUpdatesChecking))
                l.Add(String.Format("UpdateChannel= {0}", If(UpdateChannel = UpdateChannels.Stable, 0, 1)))

                l.Add(String.Format("Language= {0}", Language))
                l.Add(String.Format("Language_File= {0}", Language_File))

                l.Add(String.Format("Appearance_Dark= {0}", Appearance_Dark))
                l.Add(String.Format("Appearance_Auto= {0}", Appearance_Auto))
                l.Add(String.Format("Appearance_Custom= {0}", Appearance_Custom))
                l.Add(String.Format("Appearance_SchemeName= {0}", Appearance_SchemeName))
                l.Add(String.Format("Appearance_Custom_Dark= {0}", Appearance_Custom_Dark))
                l.Add(String.Format("Appearance_Accent= {0}", Appearance_Accent.ToArgb))
                l.Add(String.Format("Appearance_Back= {0}", Appearance_Back.ToArgb))
                l.Add(String.Format("Appearance_Rounded= {0}", Appearance_Rounded))
                l.Add(String.Format("Appearance_ManagedByTheme= {0}", Appearance_ManagedByTheme))

                l.Add(String.Format("AutoAddExt= {0}", AutoAddExt))
                l.Add(String.Format("DragAndDropPreview= {0}", DragAndDropPreview))
                l.Add(String.Format("OpeningPreviewInApp_or_AppliesIt= {0}", OpeningPreviewInApp_or_AppliesIt))
                l.Add(String.Format("SaveForLegacyWP= {0}", SaveForLegacyWP))
                l.Add(String.Format("AlwaysExportThemePack= {0}", AlwaysExportThemePack))
                l.Add(String.Format("CompressThemeFile= {0}", CompressThemeFile))

                l.Add(String.Format("AutoRestartExplorer= {0}", AutoRestartExplorer))
                l.Add(String.Format("ShowSaveConfirmation= {0}", ShowSaveConfirmation))
                l.Add(String.Format("ClassicColors_HKU_DEFAULT_Prefs= {0}", ClassicColors_HKU_DEFAULT_Prefs))
                l.Add(String.Format("ClassicColors_HKLM_Prefs= {0}", ClassicColors_HKLM_Prefs))
                l.Add(String.Format("UPM_HKU_DEFAULT= {0}", UPM_HKU_DEFAULT))
                l.Add(String.Format("Metrics_HKU_DEFAULT_Prefs= {0}", Metrics_HKU_DEFAULT_Prefs))
                l.Add(String.Format("Cursors_HKU_DEFAULT_Prefs= {0}", Cursors_HKU_DEFAULT_Prefs))
                l.Add(String.Format("CMD_HKU_DEFAULT_Prefs= {0}", CMD_HKU_DEFAULT_Prefs))
                l.Add(String.Format("PS86_HKU_DEFAULT_Prefs= {0}", PS86_HKU_DEFAULT_Prefs))
                l.Add(String.Format("PS64_HKU_DEFAULT_Prefs= {0}", PS64_HKU_DEFAULT_Prefs))
                l.Add(String.Format("Desktop_HKU_DEFAULT= {0}", Desktop_HKU_DEFAULT))
                l.Add(String.Format("AutoApplyCursors= {0}", AutoApplyCursors))
                l.Add(String.Format("ResetCursorsToAero= {0}", ResetCursorsToAero))
                l.Add(String.Format("CMD_OverrideUserPreferences= {0}", CMD_OverrideUserPreferences))
                l.Add(String.Format("DelayMetrics= {0}", DelayMetrics))

                l.Add(String.Format("Store_Online_or_Offline= {0}", Store_Online_or_Offline))
                l.Add(String.Format("Store_Online_Repositories= {0}", Store_Online_Repositories.ToArray.ToList.CString("|")))
                l.Add(String.Format("Store_Offline_Directories= {0}", Store_Offline_Directories.ToArray.ToList.CString("|")))
                l.Add(String.Format("Store_Offline_SubFolders= {0}", Store_Offline_SubFolders))
                l.Add(String.Format("Store_Search_ThemeNames= {0}", Store_Search_ThemeNames))
                l.Add(String.Format("Store_Search_AuthorsNames= {0}", Store_Search_AuthorsNames))
                l.Add(String.Format("Store_Search_Descriptions= {0}", Store_Search_Descriptions))

                l.Add(String.Format("Log_ShowApplying= {0}", Log_ShowApplying))
                l.Add(String.Format("Log_Countdown_Enabled= {0}", Log_Countdown_Enabled))
                l.Add(String.Format("Log_Countdown= {0}", Log_Countdown))

                l.Add(String.Format("Terminal_Bypass= {0}", Terminal_Bypass))
                l.Add(String.Format("Terminal_OtherFonts= {0}", Terminal_OtherFonts))
                l.Add(String.Format("Terminal_Path_Deflection= {0}", Terminal_Path_Deflection))
                l.Add(String.Format("Terminal_Stable_Path= {0}", Terminal_Stable_Path))
                l.Add(String.Format("Terminal_Preview_Path= {0}", Terminal_Preview_Path))

                l.Add(String.Format("EP_Enabled= {0}", EP_Enabled))
                l.Add(String.Format("EP_Enabled_Force= {0}", EP_Enabled_Force))
                l.Add(String.Format("EP_UseStart10= {0}", EP_UseStart10))
                l.Add(String.Format("EP_UseTaskbar10= {0}", EP_UseTaskbar10))
                l.Add(String.Format("EP_TaskbarButton10= {0}", EP_TaskbarButton10))
                l.Add(String.Format("EP_StartStyle= {0}", EP_StartStyle))

                l.Add(String.Format("Win7LivePreview= {0}", Win7LivePreview))
                l.Add(String.Format("Nerd_Stats= {0}", Nerd_Stats))
                l.Add(String.Format("Nerd_Stats_HexHash= {0}", Nerd_Stats_HexHash))
                l.Add(String.Format("Nerd_Stats_Kind= {0}", CInt(Nerd_Stats_Kind)))

                l.Add(String.Format("Classic_Color_Picker= {0}", Classic_Color_Picker))

                IO.File.WriteAllText(File, l.CString)
        End Select
    End Sub
End Class