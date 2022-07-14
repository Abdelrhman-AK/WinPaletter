Public Class OTVDM
    Private Sub OTVDM_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        PictureBox1.Image = My.Resources.OTVDM_Scr
        PictureBox1.BackgroundImage = XenonCore.BlurBitmap(My.Resources.OTVDM_Scr, 5)
    End Sub
End Class