Imports WinPaletter.XenonCore

Public Class LicenseForm
    Private Sub LicenseForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadLanguage
        ApplyDarkMode(Me)
        XenonTextBox1.Font = My.Application.ConsoleFontLarge
        XenonTextBox1.Text = My.Resources.LICENSE
        My.Computer.Audio.PlaySystemSound(Media.SystemSounds.Exclamation)
    End Sub

    Private Sub XenonButton1_Click(sender As Object, e As EventArgs) Handles XenonButton1.Click
        DialogResult = DialogResult.OK
        My.Settings.General.LicenseAccepted = True
        My.Settings.General.Save()
        Me.Close()
    End Sub

    Private Sub XenonButton2_Click(sender As Object, e As EventArgs) Handles XenonButton2.Click
        DialogResult = DialogResult.Cancel
        My.Settings.General.LicenseAccepted = False
        My.Settings.General.Save()
        Process.GetCurrentProcess.Kill()
    End Sub
End Class