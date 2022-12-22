Imports WinPaletter.XenonCore

Public Class WindowsTerminalDecide

    Private Sub WindowsTerminalDecide_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ApplyDarkMode(Me)
        Icon = WindowsTerminal.Icon
        Dim c As Color = PictureBox1.Image.AverageColor
        Dim c1 As Color = c.CB(If(GetDarkMode(), -0.35, 0.35))
        Dim c2 As Color = c.CB(If(GetDarkMode(), -0.75, 0.75))
        Panel1.BackColor = c1
        BackColor = c2
        My.Computer.Audio.PlaySystemSound(Media.SystemSounds.Exclamation)
        XenonRadioImage1.Checked = (WindowsTerminal.SaveState = WinTerminal.Version.Stable)
        XenonRadioImage2.Checked = (WindowsTerminal.SaveState = WinTerminal.Version.Preview)
    End Sub

    Private Sub XenonButton1_Click(sender As Object, e As EventArgs) Handles XenonButton1.Click
        If XenonRadioImage1.Checked Then WindowsTerminal.SaveState = WinTerminal.Version.Stable
        If XenonRadioImage2.Checked Then WindowsTerminal.SaveState = WinTerminal.Version.Preview
        DialogResult = DialogResult.OK
        Close()
    End Sub

    Private Sub XenonButton2_Click(sender As Object, e As EventArgs) Handles XenonButton2.Click
        DialogResult = DialogResult.Cancel
        Close()
    End Sub
End Class