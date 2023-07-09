Imports WinPaletter.XenonCore
Public Class LogonUI8_Pics
    Private Sub LogonUI8_Pics_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.DialogResult = DialogResult.None
        LoadLanguage
        ApplyDarkMode(Me)
        Icon = LogonUI.Icon

        If LogonUI7.ID = 0 Then img0.Checked = True
        If LogonUI7.ID = 1 Then img1.Checked = True
        If LogonUI7.ID = 2 Then img2.Checked = True
        If LogonUI7.ID = 3 Then img3.Checked = True
        If LogonUI7.ID = 4 Then img4.Checked = True
        If LogonUI7.ID = 5 Then img5.Checked = True

    End Sub

    Private Sub XenonButton2_Click(sender As Object, e As EventArgs) Handles XenonButton2.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub XenonButton1_Click(sender As Object, e As EventArgs) Handles XenonButton1.Click
        Me.DialogResult = DialogResult.OK
        If img0.Checked Then LogonUI7.ID = 0
        If img1.Checked Then LogonUI7.ID = 1
        If img2.Checked Then LogonUI7.ID = 2
        If img3.Checked Then LogonUI7.ID = 3
        If img4.Checked Then LogonUI7.ID = 4
        If img5.Checked Then LogonUI7.ID = 5
        Me.Close()
    End Sub
End Class