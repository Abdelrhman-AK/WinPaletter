Imports WinPaletter.XenonCore

Public Class LicenseForm
    Private Sub LicenseForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ApplyDarkMode(Me)

        XenonTextBox1.Font = My.Application.ConsoleFontLarge

        XenonTextBox1.Text = My.Resources.LICENSE

    End Sub

    Private Sub XenonButton1_Click(sender As Object, e As EventArgs) Handles XenonButton1.Click
        DialogResult = DialogResult.OK
        My.Settings.LicenseAccepted = True
        My.Settings.Save(XeSettings.Mode.Registry)
        Me.Close()
    End Sub

    Private Sub XenonButton2_Click(sender As Object, e As EventArgs) Handles XenonButton2.Click
        DialogResult = DialogResult.Cancel
        My.Settings.LicenseAccepted = False
        My.Settings.Save(XeSettings.Mode.Registry)
        Process.GetCurrentProcess.Kill()
    End Sub
End Class