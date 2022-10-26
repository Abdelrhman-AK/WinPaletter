Imports WinPaletter.XenonCore

Public Class EditFonts
    Private Sub EditFonts_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ApplyDarkMode(Me)
        ApplyFromCP(MainFrm.CP)
    End Sub

    Sub ApplyFromCP(CP As CP)
        Label1.Font = CP.Fonts_CaptionFont
        Label2.Font = CP.Fonts_IconFont
        Label3.Font = CP.Fonts_MenuFont
        Label4.Font = CP.Fonts_MessageFont
        Label5.Font = CP.Fonts_SmCaptionFont
        Label6.Font = CP.Fonts_StatusFont
    End Sub

    Sub ApplyToCP(CP As CP)
        CP.Fonts_CaptionFont = Label1.Font
        CP.Fonts_IconFont = Label2.Font
        CP.Fonts_MenuFont = Label3.Font
        CP.Fonts_MessageFont = Label4.Font
        CP.Fonts_SmCaptionFont = Label5.Font
        CP.Fonts_StatusFont = Label6.Font
    End Sub

    Private Sub XenonButton1_Click(sender As Object, e As EventArgs) Handles XenonButton1.Click
        FontDialog1.Font = Label1.Font
        If FontDialog1.ShowDialog = DialogResult.OK Then Label1.Font = FontDialog1.Font
    End Sub

    Private Sub XenonButton2_Click(sender As Object, e As EventArgs) Handles XenonButton2.Click
        FontDialog1.Font = Label2.Font
        If FontDialog1.ShowDialog = DialogResult.OK Then Label2.Font = FontDialog1.Font
    End Sub

    Private Sub XenonButton3_Click(sender As Object, e As EventArgs) Handles XenonButton3.Click
        FontDialog1.Font = Label3.Font
        If FontDialog1.ShowDialog = DialogResult.OK Then Label3.Font = FontDialog1.Font
    End Sub

    Private Sub XenonButton4_Click(sender As Object, e As EventArgs) Handles XenonButton4.Click
        FontDialog1.Font = Label4.Font
        If FontDialog1.ShowDialog = DialogResult.OK Then Label4.Font = FontDialog1.Font
    End Sub

    Private Sub XenonButton5_Click(sender As Object, e As EventArgs) Handles XenonButton5.Click
        FontDialog1.Font = Label5.Font
        If FontDialog1.ShowDialog = DialogResult.OK Then Label5.Font = FontDialog1.Font
    End Sub

    Private Sub XenonButton6_Click(sender As Object, e As EventArgs) Handles XenonButton6.Click
        FontDialog1.Font = Label6.Font
        If FontDialog1.ShowDialog = DialogResult.OK Then Label6.Font = FontDialog1.Font
    End Sub

    Private Sub XenonButton8_Click(sender As Object, e As EventArgs) Handles XenonButton8.Click
        ApplyToCP(MainFrm.CP)
        Me.Close()
    End Sub

    Private Sub XenonButton7_Click(sender As Object, e As EventArgs) Handles XenonButton7.Click
        Me.Close()
    End Sub

    Private Sub XenonButton10_Click(sender As Object, e As EventArgs) Handles XenonButton10.Click
        Cursor = Cursors.WaitCursor
        Dim CPx As New CP(CP.Mode.Registry)
        ApplyToCP(CPx)
        CPx.Save(CP.SavingMode.Registry)
        Cursor = Cursors.Default
    End Sub
End Class