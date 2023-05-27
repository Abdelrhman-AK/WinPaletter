
Public Class BK
    Private Sub BK_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If My.W7 Or My.WVista Then FormBorderStyle = FormBorderStyle.Sizable
        If Not My.WVista Then
            DrawDWMEffect(Nothing, False, FormDWMEffects.FormStyle.Acrylic)
        Else
            DrawTransparentGray
        End If
    End Sub

End Class