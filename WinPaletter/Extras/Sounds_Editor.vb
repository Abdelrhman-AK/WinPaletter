Imports System.CodeDom.Compiler
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

                Dim SoundBytes As Byte() = DLL_ResourcesManager.GetResource(My.PATH_imageres, "WAVE", If(My.WVista, 5051, 5080))
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
                    Using FS As New IO.FileStream(My.Application.appData & "\WindowsStartup_Backup.wav", IO.FileMode.Open, IO.FileAccess.Read)
                        SP = New SoundPlayer(FS)
                        SP.Load()
                        SP.Play()
                    End Using
                Catch ex As Exception
                    AltPlayingMethod = True
                    NativeMethods.DLLFunc.PlayAudio(My.Application.appData & "\WindowsStartup_Backup.wav")
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
            Using FS As New IO.FileStream(snd, IO.FileMode.Open, IO.FileAccess.Read)
                SP = New SoundPlayer(FS)
                SP.Load()
                SP.Play()
            End Using
        End If
    End Sub
#End Region

    Private Sub Sounds_Editor_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ApplyDarkMode(Me)
        XenonButton12.Image = MainFrm.XenonButton20.Image.Resize(16, 16)
        ApplyFromCP(MainFrm.CP)

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
        With CP.Sounds
            SoundsEnabled.Checked = .Enabled
            XenonTextBox1.Text = .Snd_Win_SystemStartup
            XenonTextBox2.Text = .Snd_Imageres_SystemStartup
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
            XenonTextBox70.Text = .Snd_SpeechRec_DisNumbersSound
            XenonTextBox74.Text = .Snd_SpeechRec_PanelSound
            XenonTextBox69.Text = .Snd_SpeechRec_MisrecoSound
            XenonTextBox73.Text = .Snd_SpeechRec_HubOffSound
            XenonTextBox72.Text = .Snd_SpeechRec_HubOnSound
            XenonTextBox71.Text = .Snd_SpeechRec_HubSleepSound
        End With

    End Sub

    Sub ApplyToCP(CP As CP)
        With CP.Sounds
            .Enabled = SoundsEnabled.Checked
            .Snd_Win_SystemStartup = XenonTextBox1.Text
            .Snd_Imageres_SystemStartup = XenonTextBox2.Text
            .Snd_Win_SystemExit = XenonTextBox3.Text
            .Snd_Win_WindowsLogoff = XenonTextBox4.Text
            .Snd_Win_WindowsLogon = XenonTextBox5.Text
            .Snd_Win_WindowsUnlock = XenonTextBox6.Text
            .Snd_Win_ChangeTheme = XenonTextBox64.Text
            .Snd_Win_SystemQuestion = XenonTextBox7.Text
            .Snd_Win_SystemExclamation = XenonTextBox8.Text
            .Snd_Win_SystemAsterisk = XenonTextBox9.Text
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
            .Snd_Win_SystemHand = XenonTextBox66.Text
            .Snd_SpeechRec_DisNumbersSound = XenonTextBox70.Text
            .Snd_SpeechRec_PanelSound = XenonTextBox74.Text
            .Snd_SpeechRec_MisrecoSound = XenonTextBox69.Text
            .Snd_SpeechRec_HubOffSound = XenonTextBox73.Text
            .Snd_SpeechRec_HubOnSound = XenonTextBox72.Text
            .Snd_SpeechRec_HubSleepSound = XenonTextBox71.Text
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
        Dim _Def As CP
        If MainFrm.PreviewConfig = MainFrm.WinVer.W11 Then
            _Def = New CP_Defaults().Default_Windows11
        ElseIf MainFrm.PreviewConfig = MainFrm.WinVer.W10 Then
            _Def = New CP_Defaults().Default_Windows10
        ElseIf MainFrm.PreviewConfig = MainFrm.WinVer.W8 Then
            _Def = New CP_Defaults().Default_Windows8
        ElseIf MainFrm.PreviewConfig = MainFrm.WinVer.W7 Then
            _Def = New CP_Defaults().Default_Windows7
        ElseIf MainFrm.PreviewConfig = MainFrm.WinVer.WVista Then
            _Def = New CP_Defaults().Default_WindowsVista
        ElseIf MainFrm.PreviewConfig = MainFrm.WinVer.WXP Then
            _Def = New CP_Defaults().Default_WindowsXP
        Else
            _Def = New CP_Defaults().Default_Windows11
        End If

        ApplyFromCP(_Def)
        _Def.Dispose()
    End Sub

    Private Sub XenonButton8_Click(sender As Object, e As EventArgs) Handles XenonButton8.Click
        ApplyToCP(MainFrm.CP)
        Close()
    End Sub

    Private Sub XenonButton10_Click(sender As Object, e As EventArgs) Handles XenonButton10.Click
        Cursor = Cursors.WaitCursor
        Dim CPx As New CP(CP.CP_Type.Registry)
        ApplyToCP(CPx)
        ApplyToCP(MainFrm.CP)
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
End Class