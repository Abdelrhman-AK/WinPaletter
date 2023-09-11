Imports System.IO
Imports System.Media
Imports WinPaletter.XenonCore

Public Class Sounds_Editor
    Private snd As String
    Private SP As New SoundPlayer
    Private AltPlayingMethod As Boolean = False
#Region "Main Subs"

    Private Sub XenonButton20_Click(sender As Object, e As EventArgs) Handles XenonButton20.Click 'imageres.dll player
        AltPlayingMethod = False
        snd = DirectCast(sender, XenonButton).Parent.Controls.OfType(Of XenonTextBox).ElementAt(0).Text


        If XenonTextBox2.Text.ToUpper.Trim = "CURRENT" Then
            If Not My.WXP Then

                Dim SoundBytes As Byte() = PE.GetResource(My.PATH_imageres, "WAVE", If(My.WVista, 5051, 5080))
                Try
                    Using ms As New MemoryStream(SoundBytes)
                        SP = New SoundPlayer(ms)
                        SP.Load()
                        SP.Play()
                    End Using
                Catch
                    Dim tmp As String = Path.GetTempFileName()
                    IO.File.WriteAllBytes(tmp, SoundBytes)
                    AltPlayingMethod = True
                    NativeMethods.DLLFunc.PlayAudio(tmp)
                    If IO.File.Exists(tmp) Then Kill(tmp)
                End Try

            End If

        ElseIf XenonTextBox2.Text.ToUpper.Trim = "DEFAULT" Then
            If Not My.WXP Then

                Try
                    Using FS As New IO.FileStream(My.PATH_appData & "\WindowsStartup_Backup.wav", IO.FileMode.Open, IO.FileAccess.Read)
                        SP = New SoundPlayer(FS)
                        SP.Load()
                        SP.Play()
                    End Using
                Catch ex As Exception
                    AltPlayingMethod = True
                    NativeMethods.DLLFunc.PlayAudio(My.PATH_appData & "\WindowsStartup_Backup.wav")
                End Try

            End If

        ElseIf snd = DirectCast(sender, XenonButton).Parent.Controls.OfType(Of XenonTextBox).ElementAt(0).Text Then

            If IO.File.Exists(snd) Then

                If SP IsNot Nothing Then
                    SP.Stop()
                    SP.Dispose()
                End If

                Try
                    Using FS As New IO.FileStream(snd, IO.FileMode.Open, IO.FileAccess.Read)
                        SP = New SoundPlayer(FS)
                        SP.Load()
                        SP.Play()
                    End Using
                Catch ex As Exception
                    AltPlayingMethod = True
                    NativeMethods.DLLFunc.PlayAudio(snd)
                End Try

            Else
                If AltPlayingMethod Then NativeMethods.DLLFunc.StopAudio()

                If SP IsNot Nothing Then
                    SP.Stop()
                    SP.Dispose()
                End If
            End If

        Else

        End If
    End Sub

    Sub PressPlay(sender As Object, e As EventArgs)
        AltPlayingMethod = False
        snd = DirectCast(sender, XenonButton).Parent.Controls.OfType(Of XenonTextBox).ElementAt(0).Text

        If IO.File.Exists(snd) Then

            If SP IsNot Nothing Then
                SP.Stop()
                SP.Dispose()
            End If

            Try
                Using FS As New IO.FileStream(snd, IO.FileMode.Open, IO.FileAccess.Read)
                    SP = New SoundPlayer(FS)
                    SP.Load()
                    SP.Play()
                End Using
            Catch
                AltPlayingMethod = True
                NativeMethods.DLLFunc.PlayAudio(snd)
            End Try

        Else
            If AltPlayingMethod Then NativeMethods.DLLFunc.StopAudio()

            If SP IsNot Nothing Then
                SP.Stop()
                SP.Dispose()
            End If
        End If
    End Sub

    Sub StopPlayer(sender As Object, e As EventArgs)
        If AltPlayingMethod Then NativeMethods.DLLFunc.StopAudio()

        If SP IsNot Nothing Then
            SP.Stop()
            SP.Dispose()
        End If
    End Sub

    Sub BrowseForWAV(sender As Object, e As EventArgs)
        With DirectCast(sender, XenonButton).Parent.Controls.OfType(Of XenonTextBox).ElementAt(0)
            If IO.File.Exists(.Text) Then
                OpenFileDialog2.FileName = New FileInfo(.Text).Name
                OpenFileDialog2.InitialDirectory = New FileInfo(.Text).DirectoryName
            End If
        End With

        If OpenFileDialog2.ShowDialog = DialogResult.OK Then
            snd = OpenFileDialog2.FileName

            DirectCast(sender, XenonButton).Parent.Controls.OfType(Of XenonTextBox).ElementAt(0).Text = snd
            Try
                Using FS As New IO.FileStream(snd, IO.FileMode.Open, IO.FileAccess.Read)
                    SP = New SoundPlayer(FS)
                    SP.Load()
                    SP.Play()
                End Using
            Catch ex As Exception
                AltPlayingMethod = True
                NativeMethods.DLLFunc.PlayAudio(snd)
            End Try
        End If
    End Sub
#End Region

    Private Sub Sounds_Editor_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadLanguage
        ApplyDarkMode(Me)
        XenonButton12.Image = MainFrm.XenonButton20.Image.Resize(16, 16)
        ApplyFromCP(My.CP)
        XenonCheckBox35_SFC.Checked = My.Settings.ThemeApplyingBehavior.SFC_on_restoring_StartupSound

        'Remove handler to avoid doubling/tripling events
        For Each page As TabPage In XenonTabControl1.TabPages
            For Each pnl As XenonGroupBox In page.Controls.OfType(Of XenonGroupBox)
                For Each btn As XenonButton In pnl.Controls.OfType(Of XenonButton)
                    Try
                        If btn.Tag = "1" Then RemoveHandler btn.Click, AddressOf PressPlay
                        If btn.Tag = "2" Then RemoveHandler btn.Click, AddressOf StopPlayer
                        If btn.Tag = "3" Then RemoveHandler btn.Click, AddressOf BrowseForWAV
                    Catch
                    End Try
                Next
            Next
        Next

        For Each page As TabPage In XenonTabControl1.TabPages
            For Each pnl As XenonGroupBox In page.Controls.OfType(Of XenonGroupBox)
                For Each btn As XenonButton In pnl.Controls.OfType(Of XenonButton)
                    If btn.Tag = "1" Then AddHandler btn.Click, AddressOf PressPlay
                    If btn.Tag = "2" Then AddHandler btn.Click, AddressOf StopPlayer
                    If btn.Tag = "3" Then AddHandler btn.Click, AddressOf BrowseForWAV
                Next
            Next
        Next
    End Sub

    Private Sub Sounds_Editor_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        For Each page As TabPage In XenonTabControl1.TabPages
            For Each pnl As XenonGroupBox In page.Controls.OfType(Of XenonGroupBox)
                For Each btn As XenonButton In pnl.Controls.OfType(Of XenonButton)
                    If btn.Tag = "1" Then RemoveHandler btn.Click, AddressOf PressPlay
                    If btn.Tag = "2" Then RemoveHandler btn.Click, AddressOf StopPlayer
                    If btn.Tag = "3" Then RemoveHandler btn.Click, AddressOf BrowseForWAV
                Next
            Next
        Next
    End Sub

    Sub ApplyFromCP(CP As CP)
        ApplyFromCP(CP.Sounds)
    End Sub

    Sub ApplyFromCP(Sounds As CP.Structures.Sounds)
        With Sounds
            SoundsEnabled.Checked = .Enabled
            XenonTextBox1.Text = .Snd_Win_SystemStart
            XenonTextBox2.Text = .Snd_Imageres_SystemStart
            XenonTextBox3.Text = .Snd_Win_SystemExit
            XenonTextBox4.Text = .Snd_Win_WindowsLogoff
            XenonTextBox5.Text = .Snd_Win_WindowsLogon
            XenonTextBox6.Text = .Snd_Win_WindowsUnlock
            XenonTextBox64.Text = .Snd_Win_ChangeTheme
            XenonTextBox7.Text = .Snd_Win_SystemQuestion
            XenonTextBox8.Text = .Snd_Win_SystemExclamation
            XenonTextBox9.Text = .Snd_Win_SystemAsterisk
            XenonTextBox10.Text = .Snd_Win_SystemNotification
            XenonTextBox11.Text = .Snd_Win_WindowsUAC
            XenonTextBox16.Text = .Snd_Win_Open
            XenonTextBox15.Text = .Snd_Win_Close
            XenonTextBox14.Text = .Snd_Win_Maximize
            XenonTextBox13.Text = .Snd_Win_Minimize
            XenonTextBox12.Text = .Snd_Win_RestoreDown
            XenonTextBox17.Text = .Snd_Win_RestoreUp
            XenonTextBox53.Text = .Snd_Win_MenuPopup
            XenonTextBox54.Text = .Snd_Win_MenuCommand
            XenonTextBox55.Text = .Snd_Win_Default
            XenonTextBox23.Text = .Snd_Win_Notification_Default
            XenonTextBox22.Text = .Snd_Win_Notification_IM
            XenonTextBox21.Text = .Snd_Win_MessageNudge
            XenonTextBox20.Text = .Snd_Win_Notification_Mail
            XenonTextBox65.Text = .Snd_Win_MailBeep
            XenonTextBox19.Text = .Snd_Win_Notification_Proximity
            XenonTextBox18.Text = .Snd_Win_Notification_Reminder
            XenonTextBox24.Text = .Snd_Win_Notification_SMS
            XenonTextBox31.Text = .Snd_Win_Notification_Looping_Alarm
            XenonTextBox30.Text = .Snd_Win_Notification_Looping_Alarm2
            XenonTextBox29.Text = .Snd_Win_Notification_Looping_Alarm3
            XenonTextBox28.Text = .Snd_Win_Notification_Looping_Alarm4
            XenonTextBox27.Text = .Snd_Win_Notification_Looping_Alarm5
            XenonTextBox26.Text = .Snd_Win_Notification_Looping_Alarm6
            XenonTextBox25.Text = .Snd_Win_Notification_Looping_Alarm7
            XenonTextBox34.Text = .Snd_Win_Notification_Looping_Alarm8
            XenonTextBox33.Text = .Snd_Win_Notification_Looping_Alarm9
            XenonTextBox32.Text = .Snd_Win_Notification_Looping_Alarm10
            XenonTextBox44.Text = .Snd_Win_Notification_Looping_Call
            XenonTextBox43.Text = .Snd_Win_Notification_Looping_Call2
            XenonTextBox42.Text = .Snd_Win_Notification_Looping_Call3
            XenonTextBox41.Text = .Snd_Win_Notification_Looping_Call4
            XenonTextBox40.Text = .Snd_Win_Notification_Looping_Call5
            XenonTextBox39.Text = .Snd_Win_Notification_Looping_Call6
            XenonTextBox38.Text = .Snd_Win_Notification_Looping_Call7
            XenonTextBox37.Text = .Snd_Win_Notification_Looping_Call8
            XenonTextBox36.Text = .Snd_Win_Notification_Looping_Call9
            XenonTextBox35.Text = .Snd_Win_Notification_Looping_Call10
            XenonTextBox45.Text = .Snd_Win_DeviceConnect
            XenonTextBox46.Text = .Snd_Win_DeviceDisconnect
            XenonTextBox47.Text = .Snd_Win_DeviceFail
            XenonTextBox48.Text = .Snd_Win_LowBatteryAlarm
            XenonTextBox49.Text = .Snd_Win_CriticalBatteryAlarm
            XenonTextBox50.Text = .Snd_Win_PrintComplete
            XenonTextBox51.Text = .Snd_Win_FaxBeep
            XenonTextBox52.Text = .Snd_Win_ProximityConnection
            XenonTextBox62.Text = .Snd_Explorer_Navigating
            XenonTextBox61.Text = .Snd_Explorer_EmptyRecycleBin
            XenonTextBox56.Text = .Snd_Explorer_MoveMenuItem
            XenonTextBox60.Text = .Snd_Explorer_ActivatingDocument
            XenonTextBox63.Text = .Snd_Win_ShowBand
            XenonTextBox59.Text = .Snd_Explorer_SecurityBand
            XenonTextBox58.Text = .Snd_Explorer_BlockedPopup
            XenonTextBox57.Text = .Snd_Explorer_FeedDiscovered
            XenonTextBox68.Text = .Snd_Win_AppGPFault
            XenonTextBox67.Text = .Snd_Win_CCSelect
            XenonTextBox66.Text = .Snd_Win_SystemHand
            XenonTextBox75.Text = .Snd_Explorer_SearchProviderDiscovered
            XenonTextBox76.Text = .Snd_Explorer_FaxNew
            XenonTextBox77.Text = .Snd_Explorer_FaxSent
            XenonTextBox79.Text = .Snd_Explorer_FaxLineRings
            XenonTextBox78.Text = .Snd_Explorer_FaxError
            XenonTextBox83.Text = .Snd_NetMeeting_PersonJoins
            XenonTextBox82.Text = .Snd_NetMeeting_PersonLeaves
            XenonTextBox80.Text = .Snd_NetMeeting_ReceiveCall
            XenonTextBox81.Text = .Snd_NetMeeting_ReceiveRequestToJoin
            XenonTextBox70.Text = .Snd_SpeechRec_DisNumbersSound
            XenonTextBox74.Text = .Snd_SpeechRec_PanelSound
            XenonTextBox69.Text = .Snd_SpeechRec_MisrecoSound
            XenonTextBox73.Text = .Snd_SpeechRec_HubOffSound
            XenonTextBox72.Text = .Snd_SpeechRec_HubOnSound
            XenonTextBox71.Text = .Snd_SpeechRec_HubSleepSound

            XenonCheckBox1.Checked = .Snd_Win_SystemExit_TaskMgmt
            XenonCheckBox2.Checked = .Snd_Win_WindowsLogoff_TaskMgmt
            XenonCheckBox3.Checked = .Snd_Win_WindowsLogon_TaskMgmt
            XenonCheckBox4.Checked = .Snd_Win_WindowsUnlock_TaskMgmt

            XenonTextBox84.Text = .Snd_ChargerConnected
        End With

    End Sub

    Sub ApplyToCP(CP As CP)
        With CP.Sounds
            .Enabled = SoundsEnabled.Checked
            .Snd_Win_SystemStart = XenonTextBox1.Text
            .Snd_Imageres_SystemStart = XenonTextBox2.Text
            .Snd_Win_SystemExit = XenonTextBox3.Text
            .Snd_Win_WindowsLogoff = XenonTextBox4.Text
            .Snd_Win_WindowsLogon = XenonTextBox5.Text
            .Snd_Win_WindowsUnlock = XenonTextBox6.Text
            .Snd_Win_ChangeTheme = XenonTextBox64.Text
            .Snd_Win_SystemQuestion = XenonTextBox7.Text
            .Snd_Win_SystemExclamation = XenonTextBox8.Text
            .Snd_Win_SystemAsterisk = XenonTextBox9.Text
            .Snd_Win_SystemHand = XenonTextBox66.Text
            .Snd_Win_SystemNotification = XenonTextBox10.Text
            .Snd_Win_WindowsUAC = XenonTextBox11.Text
            .Snd_Win_Open = XenonTextBox16.Text
            .Snd_Win_Close = XenonTextBox15.Text
            .Snd_Win_Maximize = XenonTextBox14.Text
            .Snd_Win_Minimize = XenonTextBox13.Text
            .Snd_Win_RestoreDown = XenonTextBox12.Text
            .Snd_Win_RestoreUp = XenonTextBox17.Text
            .Snd_Win_MenuPopup = XenonTextBox53.Text
            .Snd_Win_MenuCommand = XenonTextBox54.Text
            .Snd_Win_Default = XenonTextBox55.Text
            .Snd_Win_Notification_Default = XenonTextBox23.Text
            .Snd_Win_Notification_IM = XenonTextBox22.Text
            .Snd_Win_MessageNudge = XenonTextBox21.Text
            .Snd_Win_Notification_Mail = XenonTextBox20.Text
            .Snd_Win_MailBeep = XenonTextBox65.Text
            .Snd_Win_Notification_Proximity = XenonTextBox19.Text
            .Snd_Win_Notification_Reminder = XenonTextBox18.Text
            .Snd_Win_Notification_SMS = XenonTextBox24.Text
            .Snd_Win_Notification_Looping_Alarm = XenonTextBox31.Text
            .Snd_Win_Notification_Looping_Alarm2 = XenonTextBox30.Text
            .Snd_Win_Notification_Looping_Alarm3 = XenonTextBox29.Text
            .Snd_Win_Notification_Looping_Alarm4 = XenonTextBox28.Text
            .Snd_Win_Notification_Looping_Alarm5 = XenonTextBox27.Text
            .Snd_Win_Notification_Looping_Alarm6 = XenonTextBox26.Text
            .Snd_Win_Notification_Looping_Alarm7 = XenonTextBox25.Text
            .Snd_Win_Notification_Looping_Alarm8 = XenonTextBox34.Text
            .Snd_Win_Notification_Looping_Alarm9 = XenonTextBox33.Text
            .Snd_Win_Notification_Looping_Alarm10 = XenonTextBox32.Text
            .Snd_Win_Notification_Looping_Call = XenonTextBox44.Text
            .Snd_Win_Notification_Looping_Call2 = XenonTextBox43.Text
            .Snd_Win_Notification_Looping_Call3 = XenonTextBox42.Text
            .Snd_Win_Notification_Looping_Call4 = XenonTextBox41.Text
            .Snd_Win_Notification_Looping_Call5 = XenonTextBox40.Text
            .Snd_Win_Notification_Looping_Call6 = XenonTextBox39.Text
            .Snd_Win_Notification_Looping_Call7 = XenonTextBox38.Text
            .Snd_Win_Notification_Looping_Call8 = XenonTextBox37.Text
            .Snd_Win_Notification_Looping_Call9 = XenonTextBox36.Text
            .Snd_Win_Notification_Looping_Call10 = XenonTextBox35.Text
            .Snd_Win_DeviceConnect = XenonTextBox45.Text
            .Snd_Win_DeviceDisconnect = XenonTextBox46.Text
            .Snd_Win_DeviceFail = XenonTextBox47.Text
            .Snd_Win_LowBatteryAlarm = XenonTextBox48.Text
            .Snd_Win_CriticalBatteryAlarm = XenonTextBox49.Text
            .Snd_Win_PrintComplete = XenonTextBox50.Text
            .Snd_Win_FaxBeep = XenonTextBox51.Text
            .Snd_Win_ProximityConnection = XenonTextBox52.Text
            .Snd_Explorer_Navigating = XenonTextBox62.Text
            .Snd_Explorer_EmptyRecycleBin = XenonTextBox61.Text
            .Snd_Explorer_MoveMenuItem = XenonTextBox56.Text
            .Snd_Explorer_ActivatingDocument = XenonTextBox60.Text
            .Snd_Win_ShowBand = XenonTextBox63.Text
            .Snd_Explorer_SecurityBand = XenonTextBox59.Text
            .Snd_Explorer_BlockedPopup = XenonTextBox58.Text
            .Snd_Explorer_FeedDiscovered = XenonTextBox57.Text
            .Snd_Win_AppGPFault = XenonTextBox68.Text
            .Snd_Win_CCSelect = XenonTextBox67.Text
            .Snd_Explorer_SearchProviderDiscovered = XenonTextBox75.Text
            .Snd_Explorer_FaxNew = XenonTextBox76.Text
            .Snd_Explorer_FaxSent = XenonTextBox77.Text
            .Snd_Explorer_FaxLineRings = XenonTextBox79.Text
            .Snd_Explorer_FaxError = XenonTextBox78.Text
            .Snd_NetMeeting_PersonJoins = XenonTextBox83.Text
            .Snd_NetMeeting_PersonLeaves = XenonTextBox82.Text
            .Snd_NetMeeting_ReceiveCall = XenonTextBox80.Text
            .Snd_NetMeeting_ReceiveRequestToJoin = XenonTextBox81.Text
            .Snd_SpeechRec_DisNumbersSound = XenonTextBox70.Text
            .Snd_SpeechRec_PanelSound = XenonTextBox74.Text
            .Snd_SpeechRec_MisrecoSound = XenonTextBox69.Text
            .Snd_SpeechRec_HubOffSound = XenonTextBox73.Text
            .Snd_SpeechRec_HubOnSound = XenonTextBox72.Text
            .Snd_SpeechRec_HubSleepSound = XenonTextBox71.Text

            .Snd_Win_SystemExit_TaskMgmt = XenonCheckBox1.Checked
            .Snd_Win_WindowsLogoff_TaskMgmt = XenonCheckBox2.Checked
            .Snd_Win_WindowsLogon_TaskMgmt = XenonCheckBox3.Checked
            .Snd_Win_WindowsUnlock_TaskMgmt = XenonCheckBox4.Checked
            .Snd_ChargerConnected = XenonTextBox84.Text
        End With
    End Sub

    Private Sub ScrSvrEnabled_CheckedChanged(sender As Object, e As EventArgs) Handles SoundsEnabled.CheckedChanged
        checker_img.Image = If(sender.Checked, My.Resources.checker_enabled, My.Resources.checker_disabled)
    End Sub

    Private Sub XenonButton11_Click(sender As Object, e As EventArgs) Handles XenonButton11.Click
        If OpenFileDialog1.ShowDialog = DialogResult.OK Then
            Dim CPx As New CP(CP.CP_Type.File, OpenFileDialog1.FileName)
            ApplyFromCP(CPx)
            CPx.Dispose()
        End If
    End Sub

    Private Sub XenonButton9_Click(sender As Object, e As EventArgs) Handles XenonButton9.Click
        Dim CPx As New CP(CP.CP_Type.Registry)
        ApplyFromCP(CPx)
        CPx.Dispose()
    End Sub

    Private Sub XenonButton12_Click(sender As Object, e As EventArgs) Handles XenonButton12.Click
        Using _Def As CP = CP_Defaults.From(My.PreviewStyle)
            ApplyFromCP(_Def)
        End Using
    End Sub

    Private Sub XenonButton8_Click(sender As Object, e As EventArgs) Handles XenonButton8.Click
        My.Settings.ThemeApplyingBehavior.SFC_on_restoring_StartupSound = XenonCheckBox35_SFC.Checked
        My.Settings.Save(XeSettings.Mode.Registry)

        ApplyToCP(My.CP)
        Close()
    End Sub

    Private Sub XenonButton10_Click(sender As Object, e As EventArgs) Handles XenonButton10.Click
        Cursor = Cursors.WaitCursor

        My.Settings.ThemeApplyingBehavior.SFC_on_restoring_StartupSound = XenonCheckBox35_SFC.Checked
        My.Settings.Save(XeSettings.Mode.Registry)

        Dim CPx As New CP(CP.CP_Type.Registry)
        ApplyToCP(CPx)
        ApplyToCP(My.CP)
        CPx.Sounds.Apply()
        CPx.Dispose()
        Cursor = Cursors.Default
    End Sub

    Private Sub XenonButton7_Click(sender As Object, e As EventArgs) Handles XenonButton7.Click
        Close()
    End Sub

    Private Sub XenonButton22_Click(sender As Object, e As EventArgs) Handles XenonButton22.Click
        XenonTextBox2.Text = "Default"
    End Sub

    Private Sub XenonButton23_Click(sender As Object, e As EventArgs) Handles XenonButton23.Click
        XenonTextBox2.Text = ""
    End Sub

    Private Sub XenonButton24_Click(sender As Object, e As EventArgs) Handles XenonButton24.Click
        XenonTextBox2.Text = "Current"
    End Sub

    Private Sub XenonButton259_Click(sender As Object, e As EventArgs) Handles XenonButton259.Click
        If OpenThemeDialog.ShowDialog = DialogResult.OK Then
            Using _Def As CP = CP_Defaults.From(My.PreviewStyle)
                GetFromClassicThemeFile(OpenThemeDialog.FileName, _Def.Sounds)
            End Using
        End If
    End Sub


    Sub GetFromClassicThemeFile(File As String, _DefaultSounds As CP.Structures.Sounds)
        Using _ini As New INI(File)
            Dim snd As New CP.Structures.Sounds

            With snd
                Dim Scope_Win As String = "AppEvents\Schemes\Apps\.Default\{0}\.Current"
                .Snd_Win_Default = _ini.IniReadValue(String.Format(Scope_Win, ".Default"), "DefaultValue", _DefaultSounds.Snd_Win_Default).PhrasePath
                .Snd_Win_AppGPFault = _ini.IniReadValue(String.Format(Scope_Win, "AppGPFault"), "DefaultValue", _DefaultSounds.Snd_Win_AppGPFault).PhrasePath
                .Snd_Win_CCSelect = _ini.IniReadValue(String.Format(Scope_Win, "CCSelect"), "DefaultValue", _DefaultSounds.Snd_Win_CCSelect).PhrasePath
                .Snd_Win_ChangeTheme = _ini.IniReadValue(String.Format(Scope_Win, "ChangeTheme"), "DefaultValue", _DefaultSounds.Snd_Win_ChangeTheme).PhrasePath
                .Snd_Win_Close = _ini.IniReadValue(String.Format(Scope_Win, "Close"), "DefaultValue", _DefaultSounds.Snd_Win_Close).PhrasePath
                .Snd_Win_CriticalBatteryAlarm = _ini.IniReadValue(String.Format(Scope_Win, "CriticalBatteryAlarm"), "DefaultValue", _DefaultSounds.Snd_Win_CriticalBatteryAlarm).PhrasePath
                .Snd_Win_DeviceConnect = _ini.IniReadValue(String.Format(Scope_Win, "DeviceConnect"), "DefaultValue", _DefaultSounds.Snd_Win_DeviceConnect).PhrasePath
                .Snd_Win_DeviceDisconnect = _ini.IniReadValue(String.Format(Scope_Win, "DeviceDisconnect"), "DefaultValue", _DefaultSounds.Snd_Win_DeviceDisconnect).PhrasePath
                .Snd_Win_DeviceFail = _ini.IniReadValue(String.Format(Scope_Win, "DeviceFail"), "DefaultValue", _DefaultSounds.Snd_Win_DeviceFail).PhrasePath
                .Snd_Win_FaxBeep = _ini.IniReadValue(String.Format(Scope_Win, "FaxBeep"), "DefaultValue", _DefaultSounds.Snd_Win_FaxBeep).PhrasePath
                .Snd_Win_LowBatteryAlarm = _ini.IniReadValue(String.Format(Scope_Win, "LowBatteryAlarm"), "DefaultValue", _DefaultSounds.Snd_Win_LowBatteryAlarm).PhrasePath
                .Snd_Win_MailBeep = _ini.IniReadValue(String.Format(Scope_Win, "MailBeep"), "DefaultValue", _DefaultSounds.Snd_Win_MailBeep).PhrasePath
                .Snd_Win_Maximize = _ini.IniReadValue(String.Format(Scope_Win, "Maximize"), "DefaultValue", _DefaultSounds.Snd_Win_Maximize).PhrasePath
                .Snd_Win_MenuCommand = _ini.IniReadValue(String.Format(Scope_Win, "MenuCommand"), "DefaultValue", _DefaultSounds.Snd_Win_MenuCommand).PhrasePath
                .Snd_Win_MenuPopup = _ini.IniReadValue(String.Format(Scope_Win, "MenuPopup"), "DefaultValue", _DefaultSounds.Snd_Win_MenuPopup).PhrasePath
                .Snd_Win_MessageNudge = _ini.IniReadValue(String.Format(Scope_Win, "MessageNudge"), "DefaultValue", _DefaultSounds.Snd_Win_MessageNudge).PhrasePath
                .Snd_Win_Minimize = _ini.IniReadValue(String.Format(Scope_Win, "Minimize"), "DefaultValue", _DefaultSounds.Snd_Win_Minimize).PhrasePath
                .Snd_Win_Notification_Default = _ini.IniReadValue(String.Format(Scope_Win, "Notification.Default"), "DefaultValue", _DefaultSounds.Snd_Win_Notification_Default).PhrasePath
                .Snd_Win_Notification_IM = _ini.IniReadValue(String.Format(Scope_Win, "Notification.IM"), "DefaultValue", _DefaultSounds.Snd_Win_Notification_IM).PhrasePath
                .Snd_Win_Notification_Looping_Alarm = _ini.IniReadValue(String.Format(Scope_Win, "Notification.Looping.Alarm"), "DefaultValue", _DefaultSounds.Snd_Win_Notification_Looping_Alarm).PhrasePath
                .Snd_Win_Notification_Looping_Alarm2 = _ini.IniReadValue(String.Format(Scope_Win, "Notification.Looping.Alarm2"), "DefaultValue", _DefaultSounds.Snd_Win_Notification_Looping_Alarm2).PhrasePath
                .Snd_Win_Notification_Looping_Alarm3 = _ini.IniReadValue(String.Format(Scope_Win, "Notification.Looping.Alarm3"), "DefaultValue", _DefaultSounds.Snd_Win_Notification_Looping_Alarm3).PhrasePath
                .Snd_Win_Notification_Looping_Alarm4 = _ini.IniReadValue(String.Format(Scope_Win, "Notification.Looping.Alarm4"), "DefaultValue", _DefaultSounds.Snd_Win_Notification_Looping_Alarm4).PhrasePath
                .Snd_Win_Notification_Looping_Alarm5 = _ini.IniReadValue(String.Format(Scope_Win, "Notification.Looping.Alarm5"), "DefaultValue", _DefaultSounds.Snd_Win_Notification_Looping_Alarm5).PhrasePath
                .Snd_Win_Notification_Looping_Alarm6 = _ini.IniReadValue(String.Format(Scope_Win, "Notification.Looping.Alarm6"), "DefaultValue", _DefaultSounds.Snd_Win_Notification_Looping_Alarm6).PhrasePath
                .Snd_Win_Notification_Looping_Alarm7 = _ini.IniReadValue(String.Format(Scope_Win, "Notification.Looping.Alarm7"), "DefaultValue", _DefaultSounds.Snd_Win_Notification_Looping_Alarm7).PhrasePath
                .Snd_Win_Notification_Looping_Alarm8 = _ini.IniReadValue(String.Format(Scope_Win, "Notification.Looping.Alarm8"), "DefaultValue", _DefaultSounds.Snd_Win_Notification_Looping_Alarm8).PhrasePath
                .Snd_Win_Notification_Looping_Alarm9 = _ini.IniReadValue(String.Format(Scope_Win, "Notification.Looping.Alarm9"), "DefaultValue", _DefaultSounds.Snd_Win_Notification_Looping_Alarm9).PhrasePath
                .Snd_Win_Notification_Looping_Alarm10 = _ini.IniReadValue(String.Format(Scope_Win, "Notification.Looping.Alarm10"), "DefaultValue", _DefaultSounds.Snd_Win_Notification_Looping_Alarm10).PhrasePath
                .Snd_Win_Notification_Looping_Call = _ini.IniReadValue(String.Format(Scope_Win, "Notification.Looping.Call"), "DefaultValue", _DefaultSounds.Snd_Win_Notification_Looping_Call).PhrasePath
                .Snd_Win_Notification_Looping_Call2 = _ini.IniReadValue(String.Format(Scope_Win, "Notification.Looping.Call2"), "DefaultValue", _DefaultSounds.Snd_Win_Notification_Looping_Call2).PhrasePath
                .Snd_Win_Notification_Looping_Call3 = _ini.IniReadValue(String.Format(Scope_Win, "Notification.Looping.Call3"), "DefaultValue", _DefaultSounds.Snd_Win_Notification_Looping_Call3).PhrasePath
                .Snd_Win_Notification_Looping_Call4 = _ini.IniReadValue(String.Format(Scope_Win, "Notification.Looping.Call4"), "DefaultValue", _DefaultSounds.Snd_Win_Notification_Looping_Call4).PhrasePath
                .Snd_Win_Notification_Looping_Call5 = _ini.IniReadValue(String.Format(Scope_Win, "Notification.Looping.Call5"), "DefaultValue", _DefaultSounds.Snd_Win_Notification_Looping_Call5).PhrasePath
                .Snd_Win_Notification_Looping_Call6 = _ini.IniReadValue(String.Format(Scope_Win, "Notification.Looping.Call6"), "DefaultValue", _DefaultSounds.Snd_Win_Notification_Looping_Call6).PhrasePath
                .Snd_Win_Notification_Looping_Call7 = _ini.IniReadValue(String.Format(Scope_Win, "Notification.Looping.Call7"), "DefaultValue", _DefaultSounds.Snd_Win_Notification_Looping_Call7).PhrasePath
                .Snd_Win_Notification_Looping_Call8 = _ini.IniReadValue(String.Format(Scope_Win, "Notification.Looping.Call8"), "DefaultValue", _DefaultSounds.Snd_Win_Notification_Looping_Call8).PhrasePath
                .Snd_Win_Notification_Looping_Call9 = _ini.IniReadValue(String.Format(Scope_Win, "Notification.Looping.Call9"), "DefaultValue", _DefaultSounds.Snd_Win_Notification_Looping_Call9).PhrasePath
                .Snd_Win_Notification_Looping_Call10 = _ini.IniReadValue(String.Format(Scope_Win, "Notification.Looping.Call10"), "DefaultValue", _DefaultSounds.Snd_Win_Notification_Looping_Call10).PhrasePath
                .Snd_Win_Notification_Mail = _ini.IniReadValue(String.Format(Scope_Win, "Notification.Mail"), "DefaultValue", _DefaultSounds.Snd_Win_Notification_Mail).PhrasePath
                .Snd_Win_Notification_Proximity = _ini.IniReadValue(String.Format(Scope_Win, "Notification.Proximity"), "DefaultValue", _DefaultSounds.Snd_Win_Notification_Proximity).PhrasePath
                .Snd_Win_Notification_Reminder = _ini.IniReadValue(String.Format(Scope_Win, "Notification.Reminder"), "DefaultValue", _DefaultSounds.Snd_Win_Notification_Reminder).PhrasePath
                .Snd_Win_Notification_SMS = _ini.IniReadValue(String.Format(Scope_Win, "Notification.SMS"), "DefaultValue", _DefaultSounds.Snd_Win_Notification_SMS).PhrasePath
                .Snd_Win_Open = _ini.IniReadValue(String.Format(Scope_Win, "Open"), "DefaultValue", _DefaultSounds.Snd_Win_Open).PhrasePath
                .Snd_Win_PrintComplete = _ini.IniReadValue(String.Format(Scope_Win, "PrintComplete"), "DefaultValue", _DefaultSounds.Snd_Win_PrintComplete).PhrasePath
                .Snd_Win_ProximityConnection = _ini.IniReadValue(String.Format(Scope_Win, "ProximityConnection"), "DefaultValue", _DefaultSounds.Snd_Win_ProximityConnection).PhrasePath
                .Snd_Win_RestoreDown = _ini.IniReadValue(String.Format(Scope_Win, "RestoreDown"), "DefaultValue", _DefaultSounds.Snd_Win_RestoreDown).PhrasePath
                .Snd_Win_RestoreUp = _ini.IniReadValue(String.Format(Scope_Win, "RestoreUp"), "DefaultValue", _DefaultSounds.Snd_Win_RestoreUp).PhrasePath
                .Snd_Win_ShowBand = _ini.IniReadValue(String.Format(Scope_Win, "ShowBand"), "DefaultValue", _DefaultSounds.Snd_Win_ShowBand).PhrasePath
                .Snd_Win_SystemAsterisk = _ini.IniReadValue(String.Format(Scope_Win, "SystemAsterisk"), "DefaultValue", _DefaultSounds.Snd_Win_SystemAsterisk).PhrasePath
                .Snd_Win_SystemExclamation = _ini.IniReadValue(String.Format(Scope_Win, "SystemExclamation"), "DefaultValue", _DefaultSounds.Snd_Win_SystemExclamation).PhrasePath
                .Snd_Win_SystemExit = _ini.IniReadValue(String.Format(Scope_Win, "SystemExit"), "DefaultValue", _DefaultSounds.Snd_Win_SystemExit).PhrasePath
                .Snd_Win_SystemStart = _ini.IniReadValue(String.Format(Scope_Win, "SystemStart"), "DefaultValue", _DefaultSounds.Snd_Win_SystemStart).PhrasePath
                If IO.File.Exists(.Snd_Win_SystemStart) Then .Snd_Imageres_SystemStart = .Snd_Win_SystemStart Else .Snd_Imageres_SystemStart = "Current"
                .Snd_Win_SystemHand = _ini.IniReadValue(String.Format(Scope_Win, "SystemHand"), "DefaultValue", _DefaultSounds.Snd_Win_SystemHand).PhrasePath
                .Snd_Win_SystemNotification = _ini.IniReadValue(String.Format(Scope_Win, "SystemNotification"), "DefaultValue", _DefaultSounds.Snd_Win_SystemNotification).PhrasePath
                .Snd_Win_SystemQuestion = _ini.IniReadValue(String.Format(Scope_Win, "SystemQuestion"), "DefaultValue", _DefaultSounds.Snd_Win_SystemQuestion).PhrasePath
                .Snd_Win_WindowsLogoff = _ini.IniReadValue(String.Format(Scope_Win, "WindowsLogoff"), "DefaultValue", _DefaultSounds.Snd_Win_WindowsLogoff).PhrasePath
                .Snd_Win_WindowsLogon = _ini.IniReadValue(String.Format(Scope_Win, "WindowsLogon"), "DefaultValue", _DefaultSounds.Snd_Win_WindowsLogon).PhrasePath
                .Snd_Win_WindowsUAC = _ini.IniReadValue(String.Format(Scope_Win, "WindowsUAC"), "DefaultValue", _DefaultSounds.Snd_Win_WindowsUAC).PhrasePath
                .Snd_Win_WindowsUnlock = _ini.IniReadValue(String.Format(Scope_Win, "WindowsUnlock"), "DefaultValue", _DefaultSounds.Snd_Win_WindowsUnlock).PhrasePath

                Dim Scope_Explorer As String = "AppEvents\Schemes\Apps\Explorer\{0}\.Current"
                .Snd_Explorer_ActivatingDocument = _ini.IniReadValue(String.Format(Scope_Explorer, "ActivatingDocument"), "DefaultValue", _DefaultSounds.Snd_Explorer_ActivatingDocument).PhrasePath
                .Snd_Explorer_BlockedPopup = _ini.IniReadValue(String.Format(Scope_Explorer, "BlockedPopup"), "DefaultValue", _DefaultSounds.Snd_Explorer_BlockedPopup).PhrasePath
                .Snd_Explorer_EmptyRecycleBin = _ini.IniReadValue(String.Format(Scope_Explorer, "EmptyRecycleBin"), "DefaultValue", _DefaultSounds.Snd_Explorer_EmptyRecycleBin).PhrasePath
                .Snd_Explorer_FeedDiscovered = _ini.IniReadValue(String.Format(Scope_Explorer, "FeedDiscovered"), "DefaultValue", _DefaultSounds.Snd_Explorer_FeedDiscovered).PhrasePath
                .Snd_Explorer_MoveMenuItem = _ini.IniReadValue(String.Format(Scope_Explorer, "MoveMenuItem"), "DefaultValue", _DefaultSounds.Snd_Explorer_MoveMenuItem).PhrasePath
                .Snd_Explorer_Navigating = _ini.IniReadValue(String.Format(Scope_Explorer, "Navigating"), "DefaultValue", _DefaultSounds.Snd_Explorer_Navigating).PhrasePath
                .Snd_Explorer_SecurityBand = _ini.IniReadValue(String.Format(Scope_Explorer, "SecurityBand"), "DefaultValue", _DefaultSounds.Snd_Explorer_SecurityBand).PhrasePath
                .Snd_Explorer_SearchProviderDiscovered = _ini.IniReadValue(String.Format(Scope_Explorer, "SearchProviderDiscovered"), "DefaultValue", _DefaultSounds.Snd_Explorer_SearchProviderDiscovered).PhrasePath
                .Snd_Explorer_FaxError = _ini.IniReadValue(String.Format(Scope_Explorer, "FaxError"), "DefaultValue", _DefaultSounds.Snd_Explorer_FaxError).PhrasePath
                .Snd_Explorer_FaxLineRings = _ini.IniReadValue(String.Format(Scope_Explorer, "FaxLineRings"), "DefaultValue", _DefaultSounds.Snd_Explorer_FaxLineRings).PhrasePath
                .Snd_Explorer_FaxNew = _ini.IniReadValue(String.Format(Scope_Explorer, "FaxNew"), "DefaultValue", _DefaultSounds.Snd_Explorer_FaxNew).PhrasePath
                .Snd_Explorer_FaxSent = _ini.IniReadValue(String.Format(Scope_Explorer, "FaxSent"), "DefaultValue", _DefaultSounds.Snd_Explorer_FaxSent).PhrasePath

                Dim Scope_NetMeeting As String = "AppEvents\Schemes\Apps\Conf\{0}\.Current"
                .Snd_NetMeeting_PersonJoins = _ini.IniReadValue(String.Format(Scope_NetMeeting, "Person Joins"), "DefaultValue", _DefaultSounds.Snd_NetMeeting_PersonJoins).PhrasePath
                .Snd_NetMeeting_PersonLeaves = _ini.IniReadValue(String.Format(Scope_NetMeeting, "Person Leaves"), "DefaultValue", _DefaultSounds.Snd_NetMeeting_PersonLeaves).PhrasePath
                .Snd_NetMeeting_ReceiveCall = _ini.IniReadValue(String.Format(Scope_NetMeeting, "Receive Call"), "DefaultValue", _DefaultSounds.Snd_NetMeeting_ReceiveCall).PhrasePath
                .Snd_NetMeeting_ReceiveRequestToJoin = _ini.IniReadValue(String.Format(Scope_NetMeeting, "Receive Request to Join"), "DefaultValue", _DefaultSounds.Snd_NetMeeting_ReceiveRequestToJoin).PhrasePath

                Dim Scope_SpeechRec As String = "AppEvents\Schemes\Apps\sapisvr\{0}\.current"
                .Snd_SpeechRec_DisNumbersSound = _ini.IniReadValue(String.Format(Scope_SpeechRec, "DisNumbersSound"), "DefaultValue", _DefaultSounds.Snd_SpeechRec_DisNumbersSound).PhrasePath
                .Snd_SpeechRec_HubOffSound = _ini.IniReadValue(String.Format(Scope_SpeechRec, "HubOffSound"), "DefaultValue", _DefaultSounds.Snd_SpeechRec_HubOffSound).PhrasePath
                .Snd_SpeechRec_HubOnSound = _ini.IniReadValue(String.Format(Scope_SpeechRec, "HubOnSound"), "DefaultValue", _DefaultSounds.Snd_SpeechRec_HubOnSound).PhrasePath
                .Snd_SpeechRec_HubSleepSound = _ini.IniReadValue(String.Format(Scope_SpeechRec, "HubSleepSound"), "DefaultValue", _DefaultSounds.Snd_SpeechRec_HubSleepSound).PhrasePath
                .Snd_SpeechRec_MisrecoSound = _ini.IniReadValue(String.Format(Scope_SpeechRec, "MisrecoSound"), "DefaultValue", _DefaultSounds.Snd_SpeechRec_MisrecoSound).PhrasePath
                .Snd_SpeechRec_PanelSound = _ini.IniReadValue(String.Format(Scope_SpeechRec, "PanelSound"), "DefaultValue", _DefaultSounds.Snd_SpeechRec_PanelSound).PhrasePath
            End With

            ApplyFromCP(snd)
            GC.Collect()
            GC.SuppressFinalize(snd)
        End Using

    End Sub

    Private Sub Sounds_Editor_HelpButtonClicked(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Me.HelpButtonClicked
        Process.Start(My.Resources.Link_Wiki & "/Edit-Sounds")
    End Sub
End Class