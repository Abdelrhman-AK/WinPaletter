Imports System.Reflection
Imports Microsoft.Win32
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports WinPaletter.Reg_IO

Public Class XeSettings
    Private bindingFlags As BindingFlags = BindingFlags.Instance Or BindingFlags.Public

    Class Structures

        Private Const REG As String = "HKEY_CURRENT_USER\Software\WinPaletter\Settings"
        Private Const REG_General As String = REG & "\General"
        Private Const REG_General_MainForm As String = REG_General & "\MainForm"
        Private Const REG_Updates As String = REG & "\Updates"
        Private Const REG_FileTypeManagement As String = REG & "\FileTypeManagement"
        Private Const REG_ThemeApplyingBehavior As String = REG & "\ThemeApplyingBehavior"
        Private Const REG_Appearance As String = REG & "\Appearance"
        Private Const REG_Language As String = REG & "\Language"
        Private Const REG_EP As String = REG & "\ExplorerPatcher"
        Private Const REG_ThemeLog As String = REG & "\ThemeLog"
        Private Const REG_WindowsTerminals As String = REG & "\WindowsTerminals"
        Private Const REG_Store As String = REG & "\Store"
        Private Const REG_NerdStats As String = REG & "\NerdStats"
        Private Const REG_Miscellaneous As String = REG & "\Miscellaneous"

        Structure General
            Public LicenseAccepted As Boolean
            Public ComplexSaveResult As String
            Public MainFormWidth
            Public MainFormHeight
            Public MainFormStatus
            Public WhatsNewRecord As String()

            Sub Load()
                LicenseAccepted = GetReg(REG_General, "LicenseAccepted", False)
                ComplexSaveResult = GetReg(REG_General, "ComplexSaveResult", "2.1")
                WhatsNewRecord = GetReg(REG_General, "WhatsNewRecord", {""})
                MainFormWidth = GetReg(REG_General_MainForm, "MainFormWidth", 1110)
                MainFormHeight = GetReg(REG_General_MainForm, "MainFormHeight", 725)
                MainFormStatus = GetReg(REG_General_MainForm, "MainFormStatus", FormWindowState.Normal)
            End Sub

            Sub Save()
                EditReg(REG_General, "LicenseAccepted", LicenseAccepted, RegistryValueKind.DWord)
                EditReg(REG_General, "ComplexSaveResult", ComplexSaveResult, RegistryValueKind.String)
                EditReg(REG_General, "WhatsNewRecord", WhatsNewRecord, RegistryValueKind.MultiString)
                EditReg(REG_General_MainForm, "MainFormWidth", MainFormWidth, RegistryValueKind.DWord)
                EditReg(REG_General_MainForm, "MainFormHeight", MainFormHeight, RegistryValueKind.DWord)
                EditReg(REG_General_MainForm, "MainFormStatus", MainFormStatus, RegistryValueKind.DWord)
            End Sub

        End Structure

        Structure Updates
            Public AutoCheck As Boolean
            Public Channel As Channels

            Public Enum Channels
                Stable
                Beta
            End Enum

            Sub Load()
                AutoCheck = GetReg(REG_Updates, "AutoCheck", True)
                Channel = If(GetReg(REG_Updates, "Channel", Channels.Stable) = Channels.Stable, Channels.Stable, Channels.Beta)
            End Sub

            Sub Save()
                EditReg(REG_Updates, "AutoCheck", AutoCheck, RegistryValueKind.DWord)
                EditReg(REG_Updates, "Channel", If(Channel = Channels.Stable, 0, 1))
            End Sub

        End Structure

        Structure FileTypeMgr
            Public AutoAddExt As Boolean
            Public OpeningPreviewInApp_or_AppliesIt As Boolean
            Public CompressThemeFile As Boolean

            Sub Load()
                AutoAddExt = GetReg(REG_FileTypeManagement, "AutoAddExt", True)
                OpeningPreviewInApp_or_AppliesIt = GetReg(REG_FileTypeManagement, "OpeningPreviewInApp_or_AppliesIt", True)
                CompressThemeFile = GetReg(REG_FileTypeManagement, "CompressThemeFile", True)
            End Sub

            Sub Save()
                EditReg(REG_FileTypeManagement, "AutoAddExt", AutoAddExt, RegistryValueKind.DWord)
                EditReg(REG_FileTypeManagement, "OpeningPreviewInApp_or_AppliesIt", OpeningPreviewInApp_or_AppliesIt, RegistryValueKind.DWord)
                EditReg(REG_FileTypeManagement, "CompressThemeFile", CompressThemeFile, RegistryValueKind.DWord)
            End Sub

        End Structure

        Structure ThemeApplyingBehavior
            Public AutoRestartExplorer As Boolean
            Public ShowSaveConfirmation As Boolean
            Public DelayMetrics As Boolean
            Public ClassicColors_HKU_DEFAULT_Prefs As OverwriteOptions
            Public ClassicColors_HKLM_Prefs As OverwriteOptions
            Public UPM_HKU_DEFAULT As Boolean
            Public Metrics_HKU_DEFAULT_Prefs As OverwriteOptions
            Public AutoApplyCursors As Boolean
            Public ResetCursorsToAero As Boolean
            Public Cursors_HKU_DEFAULT_Prefs As OverwriteOptions
            Public CMD_HKU_DEFAULT_Prefs As OverwriteOptions
            Public PS86_HKU_DEFAULT_Prefs As OverwriteOptions
            Public PS64_HKU_DEFAULT_Prefs As OverwriteOptions
            Public Desktop_HKU_DEFAULT As OverwriteOptions
            Public CMD_OverrideUserPreferences As Boolean

            Public Enum OverwriteOptions
                DontChange
                Overwrite
                RestoreDefaults
                [Erase]
            End Enum

            Sub Load()
                AutoRestartExplorer = GetReg(REG_ThemeApplyingBehavior, "AutoRestartExplorer", True)
                ShowSaveConfirmation = GetReg(REG_ThemeApplyingBehavior, "ShowSaveConfirmation", True)
                ClassicColors_HKU_DEFAULT_Prefs = GetReg(REG_ThemeApplyingBehavior, "ClassicColors_HKU_DEFAULT_Prefs", OverwriteOptions.Overwrite)
                ClassicColors_HKLM_Prefs = GetReg(REG_ThemeApplyingBehavior, "ClassicColors_HKLM_Prefs", OverwriteOptions.Erase)
                UPM_HKU_DEFAULT = GetReg(REG_ThemeApplyingBehavior, "UPM_HKU_DEFAULT", True)
                Metrics_HKU_DEFAULT_Prefs = GetReg(REG_ThemeApplyingBehavior, "Metrics_HKU_DEFAULT_Prefs", OverwriteOptions.DontChange)
                Cursors_HKU_DEFAULT_Prefs = GetReg(REG_ThemeApplyingBehavior, "Cursors_HKU_DEFAULT_Prefs", OverwriteOptions.DontChange)
                CMD_HKU_DEFAULT_Prefs = GetReg(REG_ThemeApplyingBehavior, "CMD_HKU_DEFAULT_Prefs", OverwriteOptions.DontChange)
                PS86_HKU_DEFAULT_Prefs = GetReg(REG_ThemeApplyingBehavior, "PS86_HKU_DEFAULT_Prefs", OverwriteOptions.DontChange)
                PS64_HKU_DEFAULT_Prefs = GetReg(REG_ThemeApplyingBehavior, "PS64_HKU_DEFAULT_Prefs", OverwriteOptions.DontChange)
                Desktop_HKU_DEFAULT = GetReg(REG_ThemeApplyingBehavior, "Desktop_HKU_DEFAULT", OverwriteOptions.DontChange)
                CMD_OverrideUserPreferences = GetReg(REG_ThemeApplyingBehavior, "CMD_OverrideUserPreferences", True)
                AutoApplyCursors = GetReg(REG_ThemeApplyingBehavior, "AutoApplyCursors", True)
                ResetCursorsToAero = GetReg(REG_ThemeApplyingBehavior, "ResetCursorsToAero", My.WXP)
                DelayMetrics = GetReg(REG_ThemeApplyingBehavior, "DelayMetrics", False)

            End Sub

            Sub Save()
                EditReg(REG_ThemeApplyingBehavior, "AutoRestartExplorer", AutoRestartExplorer, RegistryValueKind.DWord)
                EditReg(REG_ThemeApplyingBehavior, "ShowSaveConfirmation", ShowSaveConfirmation, RegistryValueKind.DWord)
                EditReg(REG_ThemeApplyingBehavior, "ClassicColors_HKU_DEFAULT_Prefs", ClassicColors_HKU_DEFAULT_Prefs, RegistryValueKind.DWord)
                EditReg(REG_ThemeApplyingBehavior, "ClassicColors_HKLM_Prefs", ClassicColors_HKLM_Prefs, RegistryValueKind.DWord)
                EditReg(REG_ThemeApplyingBehavior, "UPM_HKU_DEFAULT", UPM_HKU_DEFAULT, RegistryValueKind.DWord)
                EditReg(REG_ThemeApplyingBehavior, "Metrics_HKU_DEFAULT_Prefs", Metrics_HKU_DEFAULT_Prefs, RegistryValueKind.DWord)
                EditReg(REG_ThemeApplyingBehavior, "Cursors_HKU_DEFAULT_Prefs", Cursors_HKU_DEFAULT_Prefs, RegistryValueKind.DWord)
                EditReg(REG_ThemeApplyingBehavior, "CMD_HKU_DEFAULT_Prefs", CMD_HKU_DEFAULT_Prefs, RegistryValueKind.DWord)
                EditReg(REG_ThemeApplyingBehavior, "PS86_HKU_DEFAULT_Prefs", PS86_HKU_DEFAULT_Prefs, RegistryValueKind.DWord)
                EditReg(REG_ThemeApplyingBehavior, "PS64_HKU_DEFAULT_Prefs", PS64_HKU_DEFAULT_Prefs, RegistryValueKind.DWord)
                EditReg(REG_ThemeApplyingBehavior, "Desktop_HKU_DEFAULT", Desktop_HKU_DEFAULT, RegistryValueKind.DWord)
                EditReg(REG_ThemeApplyingBehavior, "AutoApplyCursors", AutoApplyCursors, RegistryValueKind.DWord)
                EditReg(REG_ThemeApplyingBehavior, "ResetCursorsToAero", ResetCursorsToAero, RegistryValueKind.DWord)
                EditReg(REG_ThemeApplyingBehavior, "CMD_OverrideUserPreferences", CMD_OverrideUserPreferences, RegistryValueKind.DWord)
                EditReg(REG_ThemeApplyingBehavior, "DelayMetrics", DelayMetrics, RegistryValueKind.DWord)
            End Sub

        End Structure

        Structure Appearance
            Public DarkMode As Boolean
            Public AutoDarkMode As Boolean
            Public CustomColors As Boolean
            Public CustomTheme As Boolean
            Public AccentColor As Color
            Public BackColor As Color
            Public RoundedCorners As Boolean
            Public ManagedByTheme As Boolean

            Sub Load()
                DarkMode = GetReg(REG_Appearance, "DarkMode", True)
                AutoDarkMode = GetReg(REG_Appearance, "AutoDarkMode", True)
                CustomColors = GetReg(REG_Appearance, "CustomColors", False)
                CustomTheme = GetReg(REG_Appearance, "CustomTheme", True)
                AccentColor = Color.FromArgb(GetReg(REG_Appearance, "AccentColor", Color.FromArgb(0, 81, 210).ToArgb))
                BackColor = Color.FromArgb(GetReg(REG_Appearance, "BackColor", Color.FromArgb(25, 25, 25).ToArgb))
                RoundedCorners = GetReg(REG_Appearance, "RoundedCorners", True)
                ManagedByTheme = GetReg(REG_Appearance, "ManagedByTheme", True)
            End Sub

            Sub Save()
                EditReg(REG_Appearance, "DarkMode", DarkMode, RegistryValueKind.DWord)
                EditReg(REG_Appearance, "AutoDarkMode", AutoDarkMode, RegistryValueKind.DWord)
                EditReg(REG_Appearance, "CustomColors", CustomColors, RegistryValueKind.DWord)
                EditReg(REG_Appearance, "CustomTheme", CustomTheme, RegistryValueKind.DWord)
                EditReg(REG_Appearance, "AccentColor", AccentColor.ToArgb, RegistryValueKind.DWord)
                EditReg(REG_Appearance, "BackColor", BackColor.ToArgb, RegistryValueKind.DWord)
                EditReg(REG_Appearance, "RoundedCorners", RoundedCorners, RegistryValueKind.DWord)
                EditReg(REG_Appearance, "ManagedByTheme", ManagedByTheme, RegistryValueKind.DWord)
            End Sub

        End Structure

        Structure Language
            Public Enabled As Boolean
            Public File As String

            Sub Load()
                Enabled = GetReg(REG_Language, "", False)
                File = GetReg(REG_Language, "File", "")
            End Sub

            Sub Save()
                EditReg(REG_Language, "", Enabled, RegistryValueKind.DWord)
                EditReg(REG_Language, "File", File, RegistryValueKind.String)
            End Sub

        End Structure

        Structure EP
            Public Enabled As Boolean
            Public Enabled_Force As Boolean
            Public UseStart10 As Boolean
            Public UseTaskbar10 As Boolean
            Public TaskbarButton10 As Boolean
            Public StartStyle As ExplorerPatcher.StartStyles

            Sub Load()
                Enabled = GetReg(REG_EP, "", True)
                Enabled_Force = GetReg(REG_EP, "Enabled_Force", False)
                UseStart10 = GetReg(REG_EP, "UseStart10", False)
                UseTaskbar10 = GetReg(REG_EP, "UseTaskbar10", False)
                TaskbarButton10 = GetReg(REG_EP, "TaskbarButton10", False)
                StartStyle = GetReg(REG_EP, "StartStyle", WinPaletter.ExplorerPatcher.StartStyles.NotRounded)
            End Sub

            Sub Save()
                EditReg(REG_EP, "", Enabled, RegistryValueKind.DWord)
                EditReg(REG_EP, "Enabled_Force", Enabled_Force, RegistryValueKind.DWord)
                EditReg(REG_EP, "UseStart10", UseStart10, RegistryValueKind.DWord)
                EditReg(REG_EP, "UseTaskbar10", UseTaskbar10, RegistryValueKind.DWord)
                EditReg(REG_EP, "TaskbarButton10", TaskbarButton10, RegistryValueKind.DWord)
                EditReg(REG_EP, "StartStyle", StartStyle, RegistryValueKind.DWord)
            End Sub
        End Structure

        Structure ThemeLog
            Public Enabled As Boolean
            Public CountDown As Boolean
            Public CountDown_Seconds As Integer

            Sub Load()
                Enabled = GetReg(REG_ThemeLog, "", True)
                CountDown = GetReg(REG_ThemeLog, "CountDown", True)
                CountDown_Seconds = GetReg(REG_ThemeLog, "CountDown_Seconds", 20)
            End Sub

            Sub Save()
                EditReg(REG_ThemeLog, "", Enabled, RegistryValueKind.DWord)
                EditReg(REG_ThemeLog, "CountDown", CountDown, RegistryValueKind.DWord)
                EditReg(REG_ThemeLog, "CountDown_Seconds", CountDown_Seconds, RegistryValueKind.DWord)
            End Sub

        End Structure

        Structure WindowsTerminal
            Public Bypass As Boolean
            Public ListAllFonts As Boolean
            Public Path_Deflection As Boolean
            Public Terminal_Stable_Path As String
            Public Terminal_Preview_Path As String

            Sub Load()
                Bypass = GetReg(REG_WindowsTerminals, "Bypass", False)
                ListAllFonts = GetReg(REG_WindowsTerminals, "ListAllFonts", False)
                Path_Deflection = GetReg(REG_WindowsTerminals, "Path_Deflection", False)
                Terminal_Stable_Path = GetReg(REG_WindowsTerminals, "Terminal_Stable_Path", My.PATH_TerminalJSON)
                Terminal_Preview_Path = GetReg(REG_WindowsTerminals, "Terminal_Preview_Path", My.PATH_TerminalPreviewJSON)
            End Sub

            Sub Save()
                EditReg(REG_WindowsTerminals, "Bypass", Bypass, RegistryValueKind.DWord)
                EditReg(REG_WindowsTerminals, "ListAllFonts", ListAllFonts, RegistryValueKind.DWord)
                EditReg(REG_WindowsTerminals, "Path_Deflection", Path_Deflection, RegistryValueKind.DWord)
                EditReg(REG_WindowsTerminals, "Terminal_Stable_Path", Terminal_Stable_Path, RegistryValueKind.String)
                EditReg(REG_WindowsTerminals, "Terminal_Preview_Path", Terminal_Preview_Path, RegistryValueKind.String)
            End Sub

        End Structure

        Structure Store
            Public Search_ThemeNames As Boolean
            Public Search_AuthorsNames As Boolean
            Public Search_Descriptions As Boolean
            Public Online_or_Offline As Boolean
            Public Online_Repositories As String()
            Public Offline_Directories As String()
            Public Offline_SubFolders As Boolean
            Public ShowTips As Boolean

            Sub Load()
                Online_or_Offline = GetReg(REG_Store, "Online_or_Offline", True)
                Online_Repositories = GetReg(REG_Store, "Online_Repositories", {My.Resources.Link_StoreMainDB, My.Resources.Link_StoreReposDB})
                Offline_Directories = GetReg(REG_Store, "Offline_Directories", {""})
                Offline_SubFolders = GetReg(REG_Store, "Offline_SubFolders", True)
                Search_ThemeNames = GetReg(REG_Store, "Search_ThemeNames", True)
                Search_AuthorsNames = GetReg(REG_Store, "Search_AuthorsNames", True)
                Search_Descriptions = GetReg(REG_Store, "Search_Descriptions", True)
                ShowTips = GetReg(REG_Store, "ShowTips", True)

                If Not Online_Repositories.Contains(My.Resources.Link_StoreMainDB) Then
                    Array.Resize(Online_Repositories, Online_Repositories.Length + 1)
                    Online_Repositories(Online_Repositories.Length - 1) = My.Resources.Link_StoreMainDB
                End If

                If Not Online_Repositories.Contains(My.Resources.Link_StoreReposDB) Then
                    Array.Resize(Online_Repositories, Online_Repositories.Length + 1)
                    Online_Repositories(Online_Repositories.Length - 1) = My.Resources.Link_StoreReposDB
                End If
            End Sub

            Sub Save()
                If Not Online_Repositories.Contains(My.Resources.Link_StoreMainDB) Then
                    Array.Resize(Online_Repositories, Online_Repositories.Length + 1)
                    Online_Repositories(Online_Repositories.Length - 1) = My.Resources.Link_StoreMainDB
                End If

                If Not Online_Repositories.Contains(My.Resources.Link_StoreReposDB) Then
                    Array.Resize(Online_Repositories, Online_Repositories.Length + 1)
                    Online_Repositories(Online_Repositories.Length - 1) = My.Resources.Link_StoreReposDB
                End If

                EditReg(REG_Store, "Search_ThemeNames", Search_ThemeNames, RegistryValueKind.DWord)
                EditReg(REG_Store, "Search_AuthorsNames", Search_AuthorsNames, RegistryValueKind.DWord)
                EditReg(REG_Store, "Search_Descriptions", Search_Descriptions, RegistryValueKind.DWord)
                EditReg(REG_Store, "Online_or_Offline", Online_or_Offline, RegistryValueKind.DWord)
                EditReg(REG_Store, "Online_Repositories", Online_Repositories, RegistryValueKind.MultiString)
                EditReg(REG_Store, "Offline_Directories", Offline_Directories, RegistryValueKind.MultiString)
                EditReg(REG_Store, "Offline_SubFolders", Offline_SubFolders, RegistryValueKind.DWord)
                EditReg(REG_Store, "ShowTips", ShowTips, RegistryValueKind.DWord)
            End Sub

        End Structure

        Structure NerdStats
            Public Enabled As Boolean
            Public Type As Formats
            Public ShowHexHash As Boolean
            Public UseWindowsMonospacedFont As Boolean
            Public MoreLabelTransparency As Boolean
            Public DotDefaultChangedIndicator As Boolean

            Public Enum Formats
                HEX
                RGB
                HSL
                Dec
            End Enum

            Sub Load()
                Enabled = GetReg(REG_NerdStats, "", True)
                ShowHexHash = GetReg(REG_NerdStats, "ShowHexHash", True)
                Type = GetReg(REG_NerdStats, "Type", Formats.HEX)
                UseWindowsMonospacedFont = GetReg(REG_NerdStats, "UseWindowsMonospacedFont", False)
                MoreLabelTransparency = GetReg(REG_NerdStats, "MoreLabelTransparency", False)
                DotDefaultChangedIndicator = GetReg(REG_NerdStats, "DotDefaultChangedIndicator", True)

            End Sub

            Sub Save()
                EditReg(REG_NerdStats, "", Enabled, RegistryValueKind.DWord)
                EditReg(REG_NerdStats, "ShowHexHash", ShowHexHash, RegistryValueKind.DWord)
                EditReg(REG_NerdStats, "Type", CInt(Type))
                EditReg(REG_NerdStats, "UseWindowsMonospacedFont", UseWindowsMonospacedFont)
                EditReg(REG_NerdStats, "MoreLabelTransparency", MoreLabelTransparency)
                EditReg(REG_NerdStats, "DotDefaultChangedIndicator", DotDefaultChangedIndicator)

            End Sub

        End Structure

        Structure Miscellaneous
            Public Win7LivePreview As Boolean
            Public Classic_Color_Picker As Boolean

            Sub Load()
                Win7LivePreview = GetReg(REG_Miscellaneous, "Win7LivePreview", True)
                Classic_Color_Picker = GetReg(REG_Miscellaneous, "Classic_Color_Picker", False)
            End Sub

            Sub Save()
                EditReg(REG_Miscellaneous, "Win7LivePreview", Win7LivePreview, RegistryValueKind.DWord)
                EditReg(REG_Miscellaneous, "Classic_Color_Picker", Classic_Color_Picker, RegistryValueKind.DWord)
            End Sub

        End Structure
    End Class

    Public General As New Structures.General With {
        .LicenseAccepted = False,
        .ComplexSaveResult = "2.1",
        .MainFormWidth = 1110,
        .MainFormHeight = 725,
        .MainFormStatus = FormWindowState.Normal,
        .WhatsNewRecord = {""}
    }

    Public Updates As New Structures.Updates With {
    .AutoCheck = True,
    .Channel = If(My.IsBeta, Structures.Updates.Channels.Beta, Structures.Updates.Channels.Stable)}

    Public FileTypeManagement As New Structures.FileTypeMgr With {
        .AutoAddExt = True,
        .OpeningPreviewInApp_or_AppliesIt = True,
        .CompressThemeFile = True
    }

    Public ThemeApplyingBehavior As New Structures.ThemeApplyingBehavior With {
            .AutoRestartExplorer = True,
            .ShowSaveConfirmation = True,
            .DelayMetrics = False,
            .ClassicColors_HKU_DEFAULT_Prefs = Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite,
            .ClassicColors_HKLM_Prefs = Structures.ThemeApplyingBehavior.OverwriteOptions.Erase,
            .UPM_HKU_DEFAULT = True,
            .Metrics_HKU_DEFAULT_Prefs = Structures.ThemeApplyingBehavior.OverwriteOptions.DontChange,
            .AutoApplyCursors = True,
            .ResetCursorsToAero = My.WXP,
            .Cursors_HKU_DEFAULT_Prefs = Structures.ThemeApplyingBehavior.OverwriteOptions.DontChange,
            .CMD_HKU_DEFAULT_Prefs = Structures.ThemeApplyingBehavior.OverwriteOptions.DontChange,
            .PS86_HKU_DEFAULT_Prefs = Structures.ThemeApplyingBehavior.OverwriteOptions.DontChange,
            .PS64_HKU_DEFAULT_Prefs = Structures.ThemeApplyingBehavior.OverwriteOptions.DontChange,
            .Desktop_HKU_DEFAULT = Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite,
            .CMD_OverrideUserPreferences = True
            }

    Public Appearance As New Structures.Appearance With {
        .DarkMode = True,
        .AutoDarkMode = True,
        .CustomColors = False,
        .CustomTheme = True,
        .AccentColor = Color.FromArgb(0, 81, 210),
        .BackColor = Color.FromArgb(25, 25, 25),
        .RoundedCorners = True,
        .ManagedByTheme = True
    }

    Public Language As New Structures.Language With {
        .Enabled = False,
        .File = Nothing}

    Public ExplorerPatcher As New Structures.EP With {
        .Enabled = True,
        .Enabled_Force = False,
        .UseStart10 = False,
        .UseTaskbar10 = False,
        .TaskbarButton10 = False,
        .StartStyle = WinPaletter.ExplorerPatcher.StartStyles.NotRounded}

    Public ThemeLog As New Structures.ThemeLog With {
        .Enabled = True,
        .CountDown = True,
        .CountDown_Seconds = 20
    }

    Public WindowsTerminals As New Structures.WindowsTerminal With {
        .Bypass = False,
        .ListAllFonts = False,
        .Path_Deflection = False,
        .Terminal_Stable_Path = My.PATH_TerminalJSON,
        .Terminal_Preview_Path = My.PATH_TerminalPreviewJSON
    }

    Public Store As New Structures.Store With {
        .Search_ThemeNames = True,
        .Search_AuthorsNames = True,
        .Search_Descriptions = True,
        .Online_or_Offline = True,
        .Online_Repositories = {My.Resources.Link_StoreMainDB, My.Resources.Link_StoreReposDB},
        .Offline_Directories = {""},
        .Offline_SubFolders = True,
        .ShowTips = True
    }

    Public NerdStats As New Structures.NerdStats With {
        .Enabled = True,
        .Type = Structures.NerdStats.Formats.HEX,
        .ShowHexHash = True,
        .MoreLabelTransparency = False,
        .UseWindowsMonospacedFont = False,
        .DotDefaultChangedIndicator = True
    }

    Public Miscellaneous As New Structures.Miscellaneous With {
        .Win7LivePreview = True,
        .Classic_Color_Picker = False
    }

    Enum Mode
        Registry
        File
        Empty
    End Enum

    Sub New(LoadFrom As Mode, Optional File As String = Nothing)
        Select Case LoadFrom

            Case Mode.Registry
                General.Load()
                Updates.Load()
                FileTypeManagement.Load()
                ThemeApplyingBehavior.Load()
                Appearance.Load()
                Language.Load()
                ExplorerPatcher.Load()
                ThemeLog.Load()
                WindowsTerminals.Load()
                Store.Load()
                NerdStats.Load()
                Miscellaneous.Load()

            Case Mode.File
                If IO.File.Exists(File) Then
                    Dim txt As String() = IO.File.ReadAllLines(File)

                    If IsValidJson(String.Join(vbCrLf, txt)) Then

                        Try
                            Dim J As JObject = JObject.Parse(String.Join(vbCrLf, txt))

                            For Each field As FieldInfo In Me.GetType.GetFields(bindingFlags)
                                Dim type As Type = field.FieldType
                                Dim JSet As New JsonSerializerSettings

                                If J(field.Name) IsNot Nothing Then
                                    field.SetValue(Me, J(field.Name).ToObject(type))
                                End If
                            Next

                            If Not Store.Online_Repositories.Contains(My.Resources.Link_StoreMainDB) Then
                                Array.Resize(Store.Online_Repositories, Store.Online_Repositories.Length + 1)
                                Store.Online_Repositories(Store.Online_Repositories.Length - 1) = My.Resources.Link_StoreMainDB
                            End If

                            If Not Store.Online_Repositories.Contains(My.Resources.Link_StoreReposDB) Then
                                Array.Resize(Store.Online_Repositories, Store.Online_Repositories.Length + 1)
                                Store.Online_Repositories(Store.Online_Repositories.Length - 1) = My.Resources.Link_StoreReposDB
                            End If
                        Catch ex As Exception
                            BugReport.ThrowError(ex)

                        End Try

                    Else
                        BugReport.ThrowError(New Exception(My.Lang.SettingsFileNotJSON))
                    End If
                Else
                    BugReport.ThrowError(New Exception(My.Lang.SettingsFileNotExist))
                End If

        End Select
    End Sub

    Sub Save(SaveTo As Mode, Optional File As String = Nothing)
        Select Case SaveTo
            Case Mode.Registry

                General.Save()
                Updates.Save()
                FileTypeManagement.Save()
                ThemeApplyingBehavior.Save()
                Appearance.Save()
                Language.Save()
                ExplorerPatcher.Save()
                ThemeLog.Save()
                WindowsTerminals.Save()
                Store.Save()
                NerdStats.Save()
                Miscellaneous.Save()

            Case Mode.File
                If Not Store.Online_Repositories.Contains(My.Resources.Link_StoreMainDB) Then
                    Array.Resize(Store.Online_Repositories, Store.Online_Repositories.Length + 1)
                    Store.Online_Repositories(Store.Online_Repositories.Length - 1) = My.Resources.Link_StoreMainDB
                End If

                If Not Store.Online_Repositories.Contains(My.Resources.Link_StoreReposDB) Then
                    Array.Resize(Store.Online_Repositories, Store.Online_Repositories.Length + 1)
                    Store.Online_Repositories(Store.Online_Repositories.Length - 1) = My.Resources.Link_StoreReposDB
                End If

                IO.File.WriteAllText(File, ToString)
        End Select
    End Sub

    Overrides Function ToString() As String
        Dim JSON_Overall As New JObject()
        JSON_Overall.RemoveAll()

        For Each field As FieldInfo In Me.GetType.GetFields(bindingFlags)
            If field.Name.Trim.ToUpper <> "GENERAL" Then
                Dim type As Type = field.FieldType

                If IsStructure(type) Then
                    JSON_Overall.Add(field.Name, DeserializeProps(type, field.GetValue(Me)))
                Else
                    JSON_Overall.Add(field.Name, JToken.FromObject(field.GetValue(Me)))
                End If
            End If
        Next

        Return JSON_Overall.ToString
    End Function

    Private Function DeserializeProps([StructureType] As Type, [Structure] As Object) As JObject
        Dim j As New JObject()

        j.RemoveAll()

        For Each field In [StructureType].GetFields(BindingFlags.Instance Or BindingFlags.NonPublic Or BindingFlags.Public)
            Dim result As JToken

            Try
                result = JToken.FromObject(field.GetValue([Structure]))
            Catch
                result = Nothing
            End Try

            j.Add(field.Name, result)
        Next

        Return j
    End Function

    Private Shared Function IsValidJson(strInput As String) As Boolean
        If String.IsNullOrWhiteSpace(strInput) Then
            Return False
        End If
        strInput = strInput.Trim()
        If strInput.StartsWith("{") AndAlso strInput.EndsWith("}") OrElse strInput.StartsWith("[") AndAlso strInput.EndsWith("]") Then 'For object
            'For array
            Try
                Dim obj = JToken.Parse(strInput)
                Return True
            Catch jex As JsonReaderException
                'Exception in parsing json
                Return False
            Catch ex As Exception 'some other exception
                Return False
            End Try
        Else
            Return False
        End If
    End Function

    Public Function IsStructure(type As Type) As Boolean
        Return type.IsValueType AndAlso Not type.IsPrimitive AndAlso type.Namespace IsNot Nothing AndAlso Not type.Namespace.StartsWith("System.")
    End Function

End Class