Imports Microsoft.Win32
Imports WinPaletter.Core

Public Class Uninstall
    Private Sub Uninstall_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadLanguage
        ApplyStyle(Me)
        Icon = My.Resources.Icon_Uninstall
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click

        If CheckBox1.Checked Then
            My.Application.DeleteFileAssociation(".wpth", "WinPaletter.ThemeFile")
            My.Application.DeleteFileAssociation(".wpsf", "WinPaletter.SettingsFile")
            My.Application.DeleteFileAssociation(".wptp", "WinPaletter.ThemeResourcesPack")
        End If

        If CheckBox3.Checked Then
            Registry.CurrentUser.DeleteSubKeyTree("Software\WinPaletter", False)
        End If

        Try
            If Not My.WXP AndAlso IO.File.Exists(My.PATH_appData & "\WindowsStartup_Backup.wav") Then
                ReplaceResource(My.PATH_imageres, "WAV", If(My.WVista, 5051, 5080), IO.File.ReadAllBytes(My.PATH_appData & "\WindowsStartup_Backup.wav"))
            End If
        Catch
        End Try

        If CheckBox2.Checked Then
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

        If RadioImage1.Checked Then
            '# Nothing

        ElseIf RadioImage2.Checked Then
            If OpenFileDialog1.ShowDialog = DialogResult.OK Then
                Dim cpx As New CP(CP.CP_Type.File, OpenFileDialog1.FileName)
                cpx.Save(CP.CP_Type.Registry)
                If My.Settings.ThemeApplyingBehavior.AutoRestartExplorer Then RestartExplorer()
                cpx.Dispose()
            End If
        ElseIf RadioImage3.Checked Then
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