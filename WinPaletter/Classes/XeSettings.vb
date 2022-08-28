Imports Microsoft.Win32
Imports WinPaletter.XenonCore

Public Class XeSettings

#Region "Settings"
    Public Property AutoAddExt As Boolean = True
    Public Property AutoApplyCursors As Boolean = False
    Public Property DragAndDropPreview As Boolean = True
    Public Property OpeningPreviewInApp_or_AppliesIt As Boolean = True
    Public Property AutoRestartExplorer As Boolean = True
    Public Property AutoUpdatesChecking As Boolean = True
    Public Property CustomPreviewConfig_Enabled As Boolean = False
    Public Property CustomPreviewConfig As WinVer = WinVer.Eleven
    Public Property UpdateChannel As UpdateChannels = UpdateChannels.Stable   ' Don't forget to make it beta when you design a beta one
    Public Property Appearance_Dark As Boolean = True
    Public Property Appearance_Auto As Boolean = True
    Public Property WhatsNewRecord As String() = {""}
    Public Property Language As Boolean = False
    Public Property Language_File As String = Nothing

#End Region

    Public Enum WinVer
        Eleven
        Ten
        Eight
        Seven
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

        If Key.GetValue("AutoUpdatesChecking", Nothing) Is Nothing Then Key.SetValue("AutoUpdatesChecking", AutoUpdatesChecking, RegistryValueKind.DWord)
        If Key.GetValue("AutoAddExt", Nothing) Is Nothing Then Key.SetValue("AutoAddExt", AutoAddExt, RegistryValueKind.DWord)
        If Key.GetValue("DragAndDropPreview", Nothing) Is Nothing Then Key.SetValue("DragAndDropPreview", DragAndDropPreview, RegistryValueKind.DWord)
        If Key.GetValue("OpeningPreviewInApp_or_AppliesIt", Nothing) Is Nothing Then Key.SetValue("OpeningPreviewInApp_or_AppliesIt", OpeningPreviewInApp_or_AppliesIt, RegistryValueKind.DWord)
        If Key.GetValue("AutoRestartExplorer", Nothing) Is Nothing Then Key.SetValue("AutoRestartExplorer", AutoRestartExplorer, RegistryValueKind.DWord)
        If Key.GetValue("AutoApplyCursors", Nothing) Is Nothing Then Key.SetValue("AutoApplyCursors", AutoApplyCursors, RegistryValueKind.DWord)
        If Key.GetValue("CustomPreviewConfig_Enabled", Nothing) Is Nothing Then Key.SetValue("CustomPreviewConfig_Enabled", CustomPreviewConfig_Enabled, RegistryValueKind.DWord)

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
        If Key.GetValue("Appearance_Dark", Nothing) Is Nothing Then Key.SetValue("Appearance_Dark", Appearance_Dark, RegistryValueKind.DWord)
        If Key.GetValue("Appearance_Auto", Nothing) Is Nothing Then Key.SetValue("Appearance_Auto", Appearance_Auto, RegistryValueKind.DWord)
        If Key.GetValue("WhatsNewRecord", Nothing) Is Nothing Then Key.SetValue("WhatsNewRecord", WhatsNewRecord, RegistryValueKind.MultiString)
        If Key.GetValue("Language", Nothing) Is Nothing Then Key.SetValue("Language", Language, RegistryValueKind.DWord)
        If Key.GetValue("Language_File", Nothing) Is Nothing Then Key.SetValue("Language_File", "", RegistryValueKind.String)
    End Sub

    Sub New(ByVal LoadFrom As Mode, Optional ByVal File As String = Nothing)
        Select Case LoadFrom
            Case Mode.Registry
                CheckRegIfIntact()
                Dim Key As RegistryKey
                Dim AppReg As String = "Software\WinPaletter\Settings"
                Key = Registry.CurrentUser.CreateSubKey(AppReg)
                AutoAddExt = Key.GetValue("AutoAddExt", Nothing)
                AutoApplyCursors = Key.GetValue("AutoApplyCursors", Nothing)
                DragAndDropPreview = Key.GetValue("DragAndDropPreview", Nothing)
                OpeningPreviewInApp_or_AppliesIt = Key.GetValue("OpeningPreviewInApp_or_AppliesIt", Nothing)
                AutoRestartExplorer = Key.GetValue("AutoRestartExplorer", Nothing)
                AutoUpdatesChecking = Key.GetValue("AutoUpdatesChecking", Nothing)
                CustomPreviewConfig_Enabled = Key.GetValue("CustomPreviewConfig_Enabled", Nothing)

                Select Case Key.GetValue("CustomPreviewConfig", Nothing)
                    Case 0
                        CustomPreviewConfig = WinVer.Eleven
                    Case 1
                        CustomPreviewConfig = WinVer.Ten
                    Case 2
                        CustomPreviewConfig = WinVer.Eight
                    Case 3
                        CustomPreviewConfig = WinVer.Seven
                End Select

                UpdateChannel = If(Key.GetValue("UpdateChannel", Nothing) = UpdateChannels.Stable, UpdateChannels.Stable, UpdateChannels.Beta)
                Appearance_Dark = Key.GetValue("Appearance_Dark", Nothing)
                Appearance_Auto = Key.GetValue("Appearance_Auto", Nothing)
                WhatsNewRecord = Key.GetValue("WhatsNewRecord", Nothing)
                Language = Key.GetValue("Language", False)
                Language_File = Key.GetValue("Language_File", Nothing)

            Case Mode.File
                Dim l As New List(Of String)
                CList_FromStr(l, IO.File.ReadAllText(File))
                For Each x As String In l
                    If x.StartsWith("AutoAddExt= ") Then AutoAddExt = x.Remove(0, "AutoAddExt= ".Count)
                    If x.StartsWith("AutoApplyCursors= ") Then AutoApplyCursors = x.Remove(0, "AutoApplyCursors= ".Count)
                    If x.StartsWith("DragAndDropPreview= ") Then DragAndDropPreview = x.Remove(0, "DragAndDropPreview= ".Count)
                    If x.StartsWith("OpeningPreviewInApp_or_AppliesIt= ") Then OpeningPreviewInApp_or_AppliesIt = x.Remove(0, "OpeningPreviewInApp_or_AppliesIt= ".Count)
                    If x.StartsWith("AutoRestartExplorer= ") Then AutoRestartExplorer = x.Remove(0, "AutoRestartExplorer= ".Count)
                    If x.StartsWith("AutoUpdatesChecking= ") Then AutoUpdatesChecking = x.Remove(0, "AutoUpdatesChecking= ".Count)
                    If x.StartsWith("CustomPreviewConfig_Enabled= ") Then CustomPreviewConfig_Enabled = x.Remove(0, "CustomPreviewConfig_Enabled= ".Count)
                    If x.StartsWith("CustomPreviewConfig= ") Then CustomPreviewConfig = x.Remove(0, "CustomPreviewConfig= ".Count)
                    If x.StartsWith("UpdateChannel= ") Then UpdateChannel = x.Remove(0, "UpdateChannel= ".Count)
                    If x.StartsWith("Appearance_Dark= ") Then Appearance_Dark = x.Remove(0, "Appearance_Dark= ".Count)
                    If x.StartsWith("Appearance_Auto= ") Then Appearance_Auto = x.Remove(0, "Appearance_Auto= ".Count)
                    If x.StartsWith("Language= ") Then Language = x.Remove(0, "Language= ".Count)
                    If x.StartsWith("Language_File= ") Then Language_File = x.Remove(0, "Language_File= ".Count)

                Next
        End Select
    End Sub

    Sub Save(ByVal SaveTo As Mode, Optional ByVal File As String = Nothing)
        Select Case SaveTo
            Case Mode.Registry
                Dim Key As RegistryKey
                Dim AppReg As String = "Software\WinPaletter\Settings"
                Key = Registry.CurrentUser.CreateSubKey(AppReg)
                Key.SetValue("AutoAddExt", AutoAddExt, RegistryValueKind.DWord)
                Key.SetValue("AutoApplyCursors", AutoApplyCursors, RegistryValueKind.DWord)
                Key.SetValue("DragAndDropPreview", DragAndDropPreview, RegistryValueKind.DWord)
                Key.SetValue("OpeningPreviewInApp_or_AppliesIt", OpeningPreviewInApp_or_AppliesIt, RegistryValueKind.DWord)
                Key.SetValue("AutoRestartExplorer", AutoRestartExplorer, RegistryValueKind.DWord)
                Key.SetValue("AutoUpdatesChecking", AutoUpdatesChecking, RegistryValueKind.DWord)
                Key.SetValue("CustomPreviewConfig_Enabled", CustomPreviewConfig_Enabled, RegistryValueKind.DWord)

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
                l.Add(String.Format("CustomPreviewConfig_Enabled= {0}", CustomPreviewConfig_Enabled))

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

                IO.File.WriteAllText(File, CStr_FromList(l))
        End Select
    End Sub
End Class
