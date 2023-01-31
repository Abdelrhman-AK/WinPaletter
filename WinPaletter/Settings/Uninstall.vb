Imports Microsoft.Win32
Imports WinPaletter.XenonCore

Public Class Uninstall
    Private Sub Uninstall_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ApplyDarkMode(Me)
        Icon = My.Resources.Icon_Uninstall
    End Sub

    Private Sub XenonButton2_Click(sender As Object, e As EventArgs) Handles XenonButton2.Click
        Me.Close()
    End Sub

    Private Sub XenonButton6_Click(sender As Object, e As EventArgs) Handles XenonButton6.Click

        If XenonCheckBox1.Checked Then
            My.Application.DeleteFileAssociation(".wpth", "WinPaletter.ThemeFile")
            My.Application.DeleteFileAssociation(".wpsf", "WinPaletter.SettingsFile")
        End If

        If XenonCheckBox3.Checked Then
            Registry.CurrentUser.DeleteSubKeyTree("Software\WinPaletter", False)
        End If

        If XenonCheckBox2.Checked Then
            If IO.Directory.Exists(My.Application.appData) Then
                IO.Directory.Delete(My.Application.appData, True)
                CP.ResetCursorsToAero()
            End If
        End If

        If XenonRadioImage1.Checked Then
            '# Nothing

        ElseIf XenonRadioImage2.Checked Then
            If OpenFileDialog1.ShowDialog = DialogResult.OK Then
                Dim cpx As New CP(CP.Mode.File, OpenFileDialog1.FileName)
                cpx.Save(CP.Mode.Registry)
                If My.[Settings].AutoRestartExplorer Then RestartExplorer()
                cpx.Dispose()
            End If
        ElseIf XenonRadioImage3.Checked Then
            Dim _Def As CP
            If My.W11 = MainFrm.WinVer.W11 Then
                _Def = New CP_Defaults().Default_Windows11
            ElseIf My.W10 = MainFrm.WinVer.W10 Then
                _Def = New CP_Defaults().Default_Windows10
            ElseIf My.W8 = MainFrm.WinVer.W8 Then
                _Def = New CP_Defaults().Default_Windows8
            ElseIf My.W7 = MainFrm.WinVer.W7 Then
                _Def = New CP_Defaults().Default_Windows7
            Else
                _Def = New CP_Defaults().Default_Windows11
            End If

            _Def.Save(CP.Mode.Registry)
            If My.[Settings].AutoRestartExplorer Then RestartExplorer()
            _Def.Dispose()
        End If

        Dim guidText As String = My.Application.Info.ProductName
        Dim RegPath As String = "Software\Microsoft\Windows\CurrentVersion\Uninstall"
        Registry.CurrentUser.OpenSubKey(RegPath, True).DeleteSubKeyTree(guidText, False)

        Me.Close()
        Process.GetCurrentProcess.Kill()

    End Sub

End Class