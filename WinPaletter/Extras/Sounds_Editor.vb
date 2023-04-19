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