Imports Microsoft.Win32
Imports WinPaletter.XenonCore

Public Class XeSettings

#Region "Settings"
    Public Property AutoAddExt As Boolean = True
    Public Property DragAndDropPreview As Boolean = True
    Public Property OpeningPreviewInApp_or_AppliesIt As Boolean = True
    Public Property AutoRestartExplorer As Boolean = True
    Public Property AutoUpdatesChecking As Boolean = True
    Public Property CustomPreviewConfig_Enabled As Boolean = False
    Public Property CustomPreviewConfig As WinVer = WinVer.Eleven
    Public Property UpdateChannel As UpdateChannels = UpdateChannels.Stable
    Public Property Appearance_Dark As Boolean = True
    Public Property Appearance_Auto As Boolean = True
    'Public Property Appearance_AdaptColors As Boolean = True

#End Region

    Public Enum WinVer
        Eleven
        Ten
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

        If Key.GetValue("AutoAddExt", Nothing) Is Nothing Then Key.SetValue("AutoAddExt", AutoAddExt, RegistryValueKind.DWord)
        If Key.GetValue("DragAndDropPreview", Nothing) Is Nothing Then Key.SetValue("DragAndDropPreview", DragAndDropPreview, RegistryValueKind.DWord)
        If Key.GetValue("OpeningPreviewInApp_or_AppliesIt", Nothing) Is Nothing Then Key.SetValue("OpeningPreviewInApp_or_AppliesIt", OpeningPreviewInApp_or_AppliesIt, RegistryValueKind.DWord)
        If Key.GetValue("AutoRestartExplorer", Nothing) Is Nothing Then Key.SetValue("AutoRestartExplorer", AutoRestartExplorer, RegistryValueKind.DWord)
        If Key.GetValue("AutoUpdatesChecking", Nothing) Is Nothing Then Key.SetValue("AutoUpdatesChecking", AutoUpdatesChecking, RegistryValueKind.DWord)
        If Key.GetValue("CustomPreviewConfig_Enabled", Nothing) Is Nothing Then Key.SetValue("CustomPreviewConfig_Enabled", CustomPreviewConfig_Enabled, RegistryValueKind.DWord)
        If Key.GetValue("CustomPreviewConfig", Nothing) Is Nothing Then Key.SetValue("CustomPreviewConfig", If(CustomPreviewConfig = WinVer.Eleven, 0, 1))
        If Key.GetValue("UpdateChannel", Nothing) Is Nothing Then Key.SetValue("UpdateChannel", If(UpdateChannel = UpdateChannels.Stable, 0, 1))
        If Key.GetValue("Appearance_Dark", Nothing) Is Nothing Then Key.SetValue("Appearance_Dark", Appearance_Dark, RegistryValueKind.DWord)
        If Key.GetValue("Appearance_Auto", Nothing) Is Nothing Then Key.SetValue("Appearance_Auto", Appearance_Auto, RegistryValueKind.DWord)
        'If Key.GetValue("Appearance_AdaptColors", Nothing) Is Nothing Then Key.SetValue("Appearance_AdaptColors", Appearance_AdaptColors, RegistryValueKind.DWord)

    End Sub

    Sub New(ByVal LoadFrom As Mode, Optional ByVal File As String = Nothing)
        Select Case LoadFrom
            Case Mode.Registry
                CheckRegIfIntact()
                Dim Key As RegistryKey
                Dim AppReg As String = "Software\WinPaletter\Settings"
                Key = Registry.CurrentUser.CreateSubKey(AppReg)
                AutoAddExt = Key.GetValue("AutoAddExt", Nothing)
                DragAndDropPreview = Key.GetValue("DragAndDropPreview", Nothing)
                OpeningPreviewInApp_or_AppliesIt = Key.GetValue("OpeningPreviewInApp_or_AppliesIt", Nothing)
                AutoRestartExplorer = Key.GetValue("AutoRestartExplorer", Nothing)
                AutoUpdatesChecking = Key.GetValue("AutoUpdatesChecking", Nothing)
                CustomPreviewConfig_Enabled = Key.GetValue("CustomPreviewConfig_Enabled", Nothing)
                CustomPreviewConfig = If(Key.GetValue("CustomPreviewConfig", Nothing) = WinVer.Eleven, WinVer.Eleven, WinVer.Ten)
                UpdateChannel = If(Key.GetValue("UpdateChannel", Nothing) = UpdateChannels.Stable, UpdateChannels.Stable, UpdateChannels.Beta)
                Appearance_Dark = Key.GetValue("Appearance_Dark", Nothing)
                Appearance_Auto = Key.GetValue("Appearance_Auto", Nothing)
                'Appearance_AdaptColors = Key.GetValue("Appearance_AdaptColors", Nothing)

            Case Mode.File
                Dim l As New List(Of String)
                CList_FromStr(l, IO.File.ReadAllText(File))
                For Each x As String In l
                    If x.StartsWith("AutoAddExt= ") Then AutoAddExt = x.Remove(0, "AutoAddExt= ".Count)
                    If x.StartsWith("DragAndDropPreview= ") Then DragAndDropPreview = x.Remove(0, "DragAndDropPreview= ".Count)
                    If x.StartsWith("OpeningPreviewInApp_or_AppliesIt= ") Then OpeningPreviewInApp_or_AppliesIt = x.Remove(0, "OpeningPreviewInApp_or_AppliesIt= ".Count)
                    If x.StartsWith("AutoRestartExplorer= ") Then AutoRestartExplorer = x.Remove(0, "AutoRestartExplorer= ".Count)
                    If x.StartsWith("AutoUpdatesChecking= ") Then AutoUpdatesChecking = x.Remove(0, "AutoUpdatesChecking= ".Count)
                    If x.StartsWith("CustomPreviewConfig_Enabled= ") Then CustomPreviewConfig_Enabled = x.Remove(0, "CustomPreviewConfig_Enabled= ".Count)
                    If x.StartsWith("CustomPreviewConfig= ") Then CustomPreviewConfig = x.Remove(0, "CustomPreviewConfig= ".Count)
                    If x.StartsWith("UpdateChannel= ") Then UpdateChannel = x.Remove(0, "UpdateChannel= ".Count)
                    If x.StartsWith("Appearance_Dark= ") Then Appearance_Dark = x.Remove(0, "Appearance_Dark= ".Count)
                    If x.StartsWith("Appearance_Auto= ") Then Appearance_Auto = x.Remove(0, "Appearance_Auto= ".Count)
                    'If x.StartsWith("Appearance_AdaptColors= ") Then Appearance_AdaptColors = x.Remove(0, "Appearance_AdaptColors= ".Count)

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
                Key.SetValue("DragAndDropPreview", DragAndDropPreview, RegistryValueKind.DWord)
                Key.SetValue("OpeningPreviewInApp_or_AppliesIt", OpeningPreviewInApp_or_AppliesIt, RegistryValueKind.DWord)
                Key.SetValue("AutoRestartExplorer", AutoRestartExplorer, RegistryValueKind.DWord)
                Key.SetValue("AutoUpdatesChecking", AutoUpdatesChecking, RegistryValueKind.DWord)
                Key.SetValue("CustomPreviewConfig_Enabled", CustomPreviewConfig_Enabled, RegistryValueKind.DWord)
                Key.SetValue("CustomPreviewConfig", If(CustomPreviewConfig = WinVer.Eleven, 0, 1))
                Key.SetValue("UpdateChannel", If(UpdateChannel = UpdateChannels.Stable, 0, 1))
                Key.SetValue("Appearance_Dark", Appearance_Dark, RegistryValueKind.DWord)
                Key.SetValue("Appearance_Auto", Appearance_Auto, RegistryValueKind.DWord)
                'Key.SetValue("Appearance_AdaptColors", Appearance_AdaptColors, RegistryValueKind.DWord)

            Case Mode.File
                Dim l As New List(Of String)
                l.Clear()
                l.Add("WinPaletter_Settings_Exported")
                l.Add(String.Format("Date: {0}", Now))
                l.Add("")
                l.Add(String.Format("AutoAddExt= {0}", AutoAddExt))
                l.Add(String.Format("DragAndDropPreview= {0}", DragAndDropPreview))
                l.Add(String.Format("OpeningPreviewInApp_or_AppliesIt= {0}", OpeningPreviewInApp_or_AppliesIt))
                l.Add(String.Format("AutoRestartExplorer= {0}", AutoRestartExplorer))
                l.Add(String.Format("AutoUpdatesChecking= {0}", AutoUpdatesChecking))
                l.Add(String.Format("CustomPreviewConfig_Enabled= {0}", CustomPreviewConfig_Enabled))
                l.Add(String.Format("CustomPreviewConfig= {0}", If(CustomPreviewConfig = WinVer.Eleven, 0, 1)))
                l.Add(String.Format("UpdateChannel= {0}", If(UpdateChannel = UpdateChannels.Stable, 0, 1)))
                l.Add(String.Format("Appearance_Dark= {0}", Appearance_Dark))
                l.Add(String.Format("Appearance_Auto= {0}", Appearance_Auto))
                'l.Add(String.Format("Appearance_AdaptColors= {0}", Appearance_AdaptColors))

                IO.File.WriteAllText(File, CStr_FromList(l))
        End Select
    End Sub
End Class
