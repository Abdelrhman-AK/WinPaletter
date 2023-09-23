Imports Microsoft.Win32
Imports WinPaletter.XenonCore

Public Class Uninstall
    Private Sub Uninstall_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadLanguage
        ApplyStyle(Me)
        Icon = My.Resources.Icon_Uninstall
    End Sub

    Private Sub XenonButton2_Click(sender As Object, e As EventArgs) Handles XenonButton2.Click
        Me.Close()
    End Sub

    Private Sub XenonButton6_Click(sender As Object, e As EventArgs) Handles XenonButton6.Click

        If XenonCheckBox1.Checked Then
            My.Application.DeleteFileAssociation(".wpth", "WinPaletter.ThemeFile")
            My.Application.DeleteFileAssociation(".wpsf", "WinPaletter.SettingsFile")
            My.Application.DeleteFileAssociation(".wptp", "WinPaletter.ThemeResourcesPack")
        End If

        If XenonCheckBox3.Checked Then
            Registry.CurrentUser.DeleteSubKeyTree("Software\WinPaletter", False)
        End If

        Try
            If Not My.WXP AndAlso IO.File.Exists(My.PATH_appData & "\WindowsStartup_Backup.wav") Then
                ReplaceResource(My.PATH_imageres, "WAV", If(My.WVista, 5051, 5080), IO.File.ReadAllBytes(My.PATH_appData & "\WindowsStartup_Backup.wav"))
            End If
        Catch
        End Try

        If XenonCheckBox2.Checked Then
            If IO.Directory.Exists(My.PATH_appData) Then
                IO.Directory.Delete(My.PATH_appData, True)
                If Not My.WXP Then
                    CP.ResetCursorsToAero()
                    If My.Settings.ThemeApplyingBehavior.Cursors_HKU_DEFAULT_Prefs = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite Then CP.ResetCursorsToAero("HKEY_USERS\.DEFAULT")

                Else
                    CP.ResetCursorsToNone_XP()
                    If My.Settings.ThemeApplyingBehavior.Cursors_HKU_DEFAULT_Prefs = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite Then CP.ResetCursorsToNone_XP("HKEY_USERS\.DEFAULT")

                End If
            End If
        End If

        If XenonRadioImage1.Checked Then
            '# Nothing

        ElseIf XenonRadioImage2.Checked Then
            If OpenFileDialog1.ShowDialog = DialogResult.OK Then
                Dim cpx As New CP(CP.CP_Type.File, OpenFileDialog1.FileName)
                cpx.Save(CP.CP_Type.Registry)
                If My.Settings.ThemeApplyingBehavior.AutoRestartExplorer Then RestartExplorer()
                cpx.Dispose()
            End If
        ElseIf XenonRadioImage3.Checked Then
            Using _Def As CP = CP_Defaults.From(My.PreviewStyle)
                _Def.Save(CP.CP_Type.Registry)
                If My.Settings.ThemeApplyingBehavior.AutoRestartExplorer Then RestartExplorer()
            End Using
        End If

        Dim guidText As String = My.Application.Info.ProductName
        Dim RegPath As String = "Software\Microsoft\Windows\CurrentVersion\Uninstall"
        Registry.CurrentUser.OpenSubKey(RegPath, True).DeleteSubKeyTree(guidText, False)

        Me.Close()
        Using Prc As Process = Process.GetCurrentProcess : Prc.Kill() : End Using

    End Sub

End Class