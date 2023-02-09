Public Class BK
    Private Sub BK_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If My.W7 Then FormBorderStyle = FormBorderStyle.Sizable
        Acrylism.DoEffects(Me)
    End Sub
End Class