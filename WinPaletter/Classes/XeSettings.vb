Imports Microsoft.Win32
Imports WinPaletter.XenonCore

Public Class XeSettings

#Region "Settings"
    Public AutoAddExt As Boolean = True
    Public DragAndDropPreview As Boolean = True
    Public OpeningPreviewInApp_or_AppliesIt As Boolean = True
    Public AutoRestartExplorer As Boolean = True
    Public AutoUpdatesChecking As Boolean = True
    Public CustomPreviewConfig_Enabled As Boolean = False
    Public CustomPreviewConfig As WinVer = WinVer.Eleven
#End Region

    Public Enum WinVer
        Eleven
        Ten
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

            Case Mode.File
                Dim l As New List(Of String)
                CList_FromStr(l, IO.File.ReadAllText(File))
                For Each x As String In l
                    If x.Contains("AutoAddExt= ") Then AutoAddExt = x.Remove(0, "AutoAddExt= ".Count)
                    If x.Contains("DragAndDropPreview= ") Then DragAndDropPreview = x.Remove(0, "DragAndDropPreview= ".Count)
                    If x.Contains("OpeningPreviewInApp_or_AppliesIt= ") Then OpeningPreviewInApp_or_AppliesIt = x.Remove(0, "OpeningPreviewInApp_or_AppliesIt= ".Count)
                    If x.Contains("AutoRestartExplorer= ") Then AutoRestartExplorer = x.Remove(0, "AutoRestartExplorer= ".Count)
                    If x.Contains("AutoUpdatesChecking= ") Then AutoUpdatesChecking = x.Remove(0, "AutoUpdatesChecking= ".Count)
                    If x.Contains("CustomPreviewConfig_Enabled= ") Then CustomPreviewConfig_Enabled = x.Remove(0, "CustomPreviewConfig_Enabled= ".Count)
                    If x.Contains("CustomPreviewConfig= ") Then CustomPreviewConfig = x.Remove(0, "CustomPreviewConfig= ".Count)
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
                IO.File.WriteAllText(File, CStr_FromList(l))
        End Select
    End Sub
End Class
