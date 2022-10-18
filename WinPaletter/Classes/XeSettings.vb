Imports Microsoft.Win32
Imports WinPaletter.XenonCore

Public Class XeSettings

#Region "Settings"
    Public Property LicenseAccepted As Boolean = False
    Public Property AutoAddExt As Boolean = True
    Public Property AutoApplyCursors As Boolean = False
    Public Property DragAndDropPreview As Boolean = True
    Public Property OpeningPreviewInApp_or_AppliesIt As Boolean = True
    Public Property AutoRestartExplorer As Boolean = True
    Public Property AutoUpdatesChecking As Boolean = True
    Public Property Win7LivePreview As Boolean = True
    Public Property CustomPreviewConfig_Enabled As Boolean = False
    Public Property CustomPreviewConfig As WinVer = WinVer.Eleven
    Public Property UpdateChannel As UpdateChannels = UpdateChannels.Stable   ' Don't forget to make it beta when you design a beta one
    Public Property Appearance_Dark As Boolean = True
    Public Property Appearance_Auto As Boolean = True
    Public Property WhatsNewRecord As String() = {""}
    Public Property Language As Boolean = False
    Public Property Language_File As String = Nothing
    Public Property Nerd_Stats As Boolean = True
    Public Property Nerd_Stats_Kind As Nerd_Stats_Type = Nerd_Stats_Type.HEX
    Public Property Nerd_Stats_HexHash As Boolean = True
    Public Property Terminal_Bypass As Boolean = False
    Public Property Terminal_OtherFonts As Boolean = False

    Public Property Terminal_Path_Deflection As Boolean = False
    Public Property Terminal_Stable_Path As String = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) & "\AppData\Local\Packages\Microsoft.WindowsTerminal_8wekyb3d8bbwe\LocalState\settings.json"
    Public Property Terminal_Preview_Path As String = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) & "\AppData\Local\Packages\Microsoft.WindowsTerminalPreview_8wekyb3d8bbwe\LocalState\settings.json"
    Public Property CMD_OverrideUserPreferences As Boolean = True

    Public Property MainFormWidth As Integer = 1110
    Public Property MainFormHeight As Integer = 725
    Public Property MainFormStatus As FormWindowState = FormWindowState.Normal
#End Region

    Public Enum WinVer
        Eleven
        Ten
        Eight
        Seven
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
        If Key.GetValue("AutoApplyCursors", Nothing) Is Nothing Then Key.SetValue("AutoApplyCursors", False, RegistryValueKind.DWord)
        If Key.GetValue("CustomPreviewConfig_Enabled", Nothing) Is Nothing Then Key.SetValue("CustomPreviewConfig_Enabled", False, RegistryValueKind.DWord)
        If Key.GetValue("ShowLogWhileSaving", Nothing) Is Nothing Then Key.SetValue("ShowLogWhileSaving", False, RegistryValueKind.DWord)

        If Key.GetValue("MainFormWidth", Nothing) Is Nothing Then Key.SetValue("MainFormWidth", 1110, RegistryValueKind.DWord)
        If Key.GetValue("MainFormHeight", Nothing) Is Nothing Then Key.SetValue("MainFormHeight", 725, RegistryValueKind.DWord)
        If Key.GetValue("MainFormStatus", Nothing) Is Nothing Then Key.SetValue("MainFormStatus", FormWindowState.Normal, RegistryValueKind.DWord)

        Select Case CustomPreviewConfig
            Case WinVer.Eleven
                If Key.GetValue("CustomPreviewConfig", Nothing) Is Nothing Then Key.SetValue("CustomPreviewConfig", 0)
            Case WinVer.Ten
                If Key.GetValue("CustomPreviewConfig", Nothing) Is Nothing Then Key.SetValue("CustomPreviewConfig", 1)
            Case WinVer.Eight
                If Key.GetValue("CustomPreviewConfig", Nothing) Is Nothing Then Key.SetValue("CustomPreviewConfig", 2)
            Case WinVer.Seven
                If Key.GetValue("CustomPreviewConfig", Nothing) Is Nothing Then Key.SetValue("CustomPreviewConfig", 3)
        End Select

        If Key.GetValue("UpdateChannel", Nothing) Is Nothing Then Key.SetValue("UpdateChannel", If(UpdateChannel = UpdateChannels.Stable, 0, 1))
        If Key.GetValue("Appearance_Dark", Nothing) Is Nothing Then Key.SetValue("Appearance_Dark", True, RegistryValueKind.DWord)
        If Key.GetValue("Appearance_Auto", Nothing) Is Nothing Then Key.SetValue("Appearance_Auto", True, RegistryValueKind.DWord)
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
        If Key.GetValue("Terminal_Stable_Path", Nothing) Is Nothing Then Key.SetValue("Terminal_Stable_Path", Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) & "\AppData\Local\Packages\Microsoft.WindowsTerminal_8wekyb3d8bbwe\LocalState\settings.json", RegistryValueKind.String)
        If Key.GetValue("Terminal_Preview_Path", Nothing) Is Nothing Then Key.SetValue("Terminal_Preview_Path", Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) & "\AppData\Local\Packages\Microsoft.WindowsTerminalPreview_8wekyb3d8bbwe\LocalState\settings.json", RegistryValueKind.String)
        If Key.GetValue("CMD_OverrideUserPreferences", Nothing) Is Nothing Then Key.SetValue("CMD_OverrideUserPreferences", True, RegistryValueKind.DWord)

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
                AutoApplyCursors = Key.GetValue("AutoApplyCursors", False)
                DragAndDropPreview = Key.GetValue("DragAndDropPreview", True)
                OpeningPreviewInApp_or_AppliesIt = Key.GetValue("OpeningPreviewInApp_or_AppliesIt", True)
                AutoRestartExplorer = Key.GetValue("AutoRestartExplorer", True)
                Win7LivePreview = Key.GetValue("Win7LivePreview", True)
                AutoUpdatesChecking = Key.GetValue("AutoUpdatesChecking", True)
                CustomPreviewConfig_Enabled = Key.GetValue("CustomPreviewConfig_Enabled", False)

                MainFormWidth = Key.GetValue("MainFormWidth", 1110)
                MainFormHeight = Key.GetValue("MainFormHeight", 725)
                MainFormStatus = Key.GetValue("MainFormStatus", FormWindowState.Normal)

                Terminal_Bypass = Key.GetValue("Terminal_Bypass", False)
                Terminal_OtherFonts = Key.GetValue("Terminal_OtherFonts", False)
                Terminal_Path_Deflection = Key.GetValue("Terminal_Path_Deflection", False)
                Terminal_Stable_Path = Key.GetValue("Terminal_Stable_Path", Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) & "\AppData\Local\Packages\Microsoft.WindowsTerminal_8wekyb3d8bbwe\LocalState\settings.json")
                Terminal_Preview_Path = Key.GetValue("Terminal_Preview_Path", Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) & "\AppData\Local\Packages\Microsoft.WindowsTerminalPreview_8wekyb3d8bbwe\LocalState\settings.json")
                Select Case Key.GetValue("CustomPreviewConfig", 0)
                    Case 0
                        CustomPreviewConfig = WinVer.Eleven
                    Case 1
                        CustomPreviewConfig = WinVer.Ten
                    Case 2
                        CustomPreviewConfig = WinVer.Eight
                    Case 3
                        CustomPreviewConfig = WinVer.Seven
                End Select
                CMD_OverrideUserPreferences = Key.GetValue("CMD_OverrideUserPreferences", True)

                UpdateChannel = If(Key.GetValue("UpdateChannel", UpdateChannels.Stable) = UpdateChannels.Stable, UpdateChannels.Stable, UpdateChannels.Beta)
                Appearance_Dark = Key.GetValue("Appearance_Dark", True)
                Appearance_Auto = Key.GetValue("Appearance_Auto", True)
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

            Case Mode.File
                Dim l As New List(Of String)
                CList_FromStr(l, IO.File.ReadAllText(File))
                For Each x As String In l
                    If x.ToLower.StartsWith("AutoAddExt= ".ToLower) Then AutoAddExt = x.Remove(0, "AutoAddExt= ".Count)
                    If x.ToLower.StartsWith("AutoApplyCursors= ".ToLower) Then AutoApplyCursors = x.Remove(0, "AutoApplyCursors= ".Count)
                    If x.ToLower.StartsWith("DragAndDropPreview= ".ToLower) Then DragAndDropPreview = x.Remove(0, "DragAndDropPreview= ".Count)
                    If x.ToLower.StartsWith("OpeningPreviewInApp_or_AppliesIt= ".ToLower) Then OpeningPreviewInApp_or_AppliesIt = x.Remove(0, "OpeningPreviewInApp_or_AppliesIt= ".Count)
                    If x.ToLower.StartsWith("AutoRestartExplorer= ".ToLower) Then AutoRestartExplorer = x.Remove(0, "AutoRestartExplorer= ".Count)
                    If x.ToLower.StartsWith("AutoUpdatesChecking= ".ToLower) Then AutoUpdatesChecking = x.Remove(0, "AutoUpdatesChecking= ".Count)
                    If x.ToLower.StartsWith("Win7LivePreview= ".ToLower) Then Win7LivePreview = x.Remove(0, "Win7LivePreview= ".Count)
                    If x.ToLower.StartsWith("CustomPreviewConfig_Enabled= ".ToLower) Then CustomPreviewConfig_Enabled = x.Remove(0, "CustomPreviewConfig_Enabled= ".Count)
                    If x.ToLower.StartsWith("CustomPreviewConfig= ".ToLower) Then CustomPreviewConfig = x.Remove(0, "CustomPreviewConfig= ".Count)
                    If x.ToLower.StartsWith("UpdateChannel= ".ToLower) Then UpdateChannel = x.Remove(0, "UpdateChannel= ".Count)
                    If x.ToLower.StartsWith("Appearance_Dark= ".ToLower) Then Appearance_Dark = x.Remove(0, "Appearance_Dark= ".Count)
                    If x.ToLower.StartsWith("Appearance_Auto= ".ToLower) Then Appearance_Auto = x.Remove(0, "Appearance_Auto= ".Count)
                    If x.ToLower.StartsWith("Language= ".ToLower) Then Language = x.Remove(0, "Language= ".Count)
                    If x.ToLower.StartsWith("Language_File= ".ToLower) Then Language_File = x.Remove(0, "Language_File= ".Count)
                    If x.ToLower.StartsWith("Nerd_Stats= ".ToLower) Then Nerd_Stats = x.Remove(0, "Nerd_Stats= ".Count)
                    If x.ToLower.StartsWith("Nerd_Stats_HexHash= ".ToLower) Then Nerd_Stats_HexHash = x.Remove(0, "Nerd_Stats_HexHash= ".Count)
                    If x.ToLower.StartsWith("Nerd_Stats_Kind= ".ToLower) Then Nerd_Stats_Kind = x.Remove(0, "Nerd_Stats_Kind= ".Count)
                    If x.ToLower.StartsWith("Terminal_Bypass= ".ToLower) Then Terminal_Bypass = x.Remove(0, "Terminal_Bypass= ".Count)
                    If x.ToLower.StartsWith("Terminal_OtherFonts= ".ToLower) Then Terminal_OtherFonts = x.Remove(0, "Terminal_OtherFonts= ".Count)
                    If x.ToLower.StartsWith("Terminal_Path_Deflection= ".ToLower) Then Terminal_Path_Deflection = x.Remove(0, "Terminal_Path_Deflection= ".Count)
                    If x.ToLower.StartsWith("Terminal_Stable_Path= ".ToLower) Then Terminal_Stable_Path = x.Remove(0, "Terminal_Stable_Path= ".Count)
                    If x.ToLower.StartsWith("Terminal_Preview_Path= ".ToLower) Then Terminal_Preview_Path = x.Remove(0, "Terminal_Preview_Path= ".Count)
                    If x.ToLower.StartsWith("CMD_OverrideUserPreferences= ".ToLower) Then CMD_OverrideUserPreferences = x.Remove(0, "CMD_OverrideUserPreferences= ".Count)

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
                Key.SetValue("DragAndDropPreview", DragAndDropPreview, RegistryValueKind.DWord)
                Key.SetValue("OpeningPreviewInApp_or_AppliesIt", OpeningPreviewInApp_or_AppliesIt, RegistryValueKind.DWord)
                Key.SetValue("AutoRestartExplorer", AutoRestartExplorer, RegistryValueKind.DWord)
                Key.SetValue("AutoUpdatesChecking", AutoUpdatesChecking, RegistryValueKind.DWord)
                Key.SetValue("Win7LivePreview", Win7LivePreview, RegistryValueKind.DWord)
                Key.SetValue("CustomPreviewConfig_Enabled", CustomPreviewConfig_Enabled, RegistryValueKind.DWord)
                Key.SetValue("Terminal_Bypass", Terminal_Bypass, RegistryValueKind.DWord)
                Key.SetValue("Terminal_OtherFonts", Terminal_OtherFonts, RegistryValueKind.DWord)
                Key.SetValue("Terminal_Path_Deflection", Terminal_Path_Deflection, RegistryValueKind.DWord)
                Key.SetValue("Terminal_Stable_Path", Terminal_Stable_Path, RegistryValueKind.String)
                Key.SetValue("Terminal_Preview_Path", Terminal_Preview_Path, RegistryValueKind.String)
                Key.SetValue("CMD_OverrideUserPreferences", CMD_OverrideUserPreferences, RegistryValueKind.DWord)

                Select Case CustomPreviewConfig
                    Case WinVer.Eleven
                        Key.SetValue("CustomPreviewConfig", 0)
                    Case WinVer.Ten
                        Key.SetValue("CustomPreviewConfig", 1)
                    Case WinVer.Eight
                        Key.SetValue("CustomPreviewConfig", 2)
                    Case WinVer.Seven
                        Key.SetValue("CustomPreviewConfig", 3)
                End Select

                Key.SetValue("UpdateChannel", If(UpdateChannel = UpdateChannels.Stable, 0, 1))
                Key.SetValue("Appearance_Dark", Appearance_Dark, RegistryValueKind.DWord)
                Key.SetValue("Appearance_Auto", Appearance_Auto, RegistryValueKind.DWord)
                Key.SetValue("WhatsNewRecord", WhatsNewRecord, RegistryValueKind.MultiString)
                Key.SetValue("Language", Language, RegistryValueKind.DWord)
                Key.SetValue("Language_File", Language_File, RegistryValueKind.String)
                Key.SetValue("Nerd_Stats", Nerd_Stats, RegistryValueKind.DWord)
                Key.SetValue("Nerd_Stats_HexHash", Nerd_Stats_HexHash, RegistryValueKind.DWord)

                Key.SetValue("MainFormWidth", MainFormWidth, RegistryValueKind.DWord)
                Key.SetValue("MainFormHeight", MainFormHeight, RegistryValueKind.DWord)
                Key.SetValue("MainFormStatus", MainFormStatus, RegistryValueKind.DWord)

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

            Case Mode.File
                Dim l As New List(Of String)
                l.Clear()
                l.Add("WinPaletter_Settings_Exported")
                l.Add(String.Format("Date: {0}", Now))
                l.Add("")
                l.Add(String.Format("AutoAddExt= {0}", AutoAddExt))
                l.Add(String.Format("AutoApplyCursors= {0}", AutoApplyCursors))
                l.Add(String.Format("DragAndDropPreview= {0}", DragAndDropPreview))
                l.Add(String.Format("OpeningPreviewInApp_or_AppliesIt= {0}", OpeningPreviewInApp_or_AppliesIt))
                l.Add(String.Format("AutoRestartExplorer= {0}", AutoRestartExplorer))
                l.Add(String.Format("AutoUpdatesChecking= {0}", AutoUpdatesChecking))
                l.Add(String.Format("Win7LivePreview= {0}", Win7LivePreview))
                l.Add(String.Format("CustomPreviewConfig_Enabled= {0}", CustomPreviewConfig_Enabled))
                l.Add(String.Format("Terminal_Bypass= {0}", Terminal_Bypass))
                l.Add(String.Format("Terminal_OtherFonts= {0}", Terminal_OtherFonts))
                l.Add(String.Format("Terminal_Path_Deflection= {0}", Terminal_Path_Deflection))
                l.Add(String.Format("Terminal_Stable_Path= {0}", Terminal_Stable_Path))
                l.Add(String.Format("Terminal_Preview_Path= {0}", Terminal_Preview_Path))
                l.Add(String.Format("CMD_OverrideUserPreferences= {0}", CMD_OverrideUserPreferences))

                Select Case CustomPreviewConfig
                    Case WinVer.Eleven
                        l.Add(String.Format("CustomPreviewConfig= {0}", 0))
                    Case WinVer.Ten
                        l.Add(String.Format("CustomPreviewConfig= {0}", 1))
                    Case WinVer.Eight
                        l.Add(String.Format("CustomPreviewConfig= {0}", 2))
                    Case WinVer.Seven
                        l.Add(String.Format("CustomPreviewConfig= {0}", 3))
                End Select

                l.Add(String.Format("UpdateChannel= {0}", If(UpdateChannel = UpdateChannels.Stable, 0, 1)))
                l.Add(String.Format("Appearance_Dark= {0}", Appearance_Dark))
                l.Add(String.Format("Appearance_Auto= {0}", Appearance_Auto))
                l.Add(String.Format("Language= {0}", Language))
                l.Add(String.Format("Language_File= {0}", Language_File))
                l.Add(String.Format("Nerd_Stats= {0}", Nerd_Stats))
                l.Add(String.Format("Nerd_Stats_HexHash= {0}", Nerd_Stats_HexHash))


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


                IO.File.WriteAllText(File, CStr_FromList(l))
        End Select
    End Sub
End Class
