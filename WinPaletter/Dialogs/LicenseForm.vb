Public Class LicenseForm
    Private Sub LicenseForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadLanguage
        ApplyStyle(Me)
        TextBox1.Font = My.Application.ConsoleFontLarge
        TextBox1.Text = My.Resources.LICENSE
        My.Computer.Audio.PlaySystemSound(Media.SystemSounds.Exclamation)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        DialogResult = DialogResult.OK
        My.Settings.General.LicenseAccepted = True
        My.Settings.General.Save()
        Me.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        DialogResult = DialogResult.Cancel
        My.Settings.General.LicenseAccepted = False
        My.Settings.General.Save()
        Using Prc As Process = Process.GetCurrentProcess : Prc.Kill() : End Using
    End Sub
End Class