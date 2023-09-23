Imports System.ComponentModel
Imports System.Drawing.Drawing2D

Namespace UI.Retro

    <Description("Retro 3D Panel Preview with Windows 9x style")> Public Class Preview3D : Inherits Control

        Public Property WindowFrame As Color = Color.Black
        Public Property ButtonShadow As Color = Color.FromArgb(128, 128, 128)
        Public Property ButtonDkShadow As Color = Color.Black
        Public Property ButtonHilight As Color = Color.White
        Public Property ButtonLight As Color = Color.FromArgb(192, 192, 192)

        Public Property LineSize As Integer = 6

        Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
            Dim B As New Bitmap(Width, Height)
            Dim G As Graphics = Graphics.FromImage(B)
            G.SmoothingMode = SmoothingMode.HighSpeed
            G.TextRenderingHint = My.RenderingHint
            DoubleBuffered = True

            '################################################################################# Customizer
            Dim BrushDkShadow As New SolidBrush(ButtonDkShadow)
            Dim BrushShadow As New SolidBrush(ButtonShadow)
            Dim BrushHilight As New SolidBrush(ButtonHilight)
            Dim BrushLight As New SolidBrush(ButtonLight)

            Dim HilightTopRect As New Rectangle(LineSize, LineSize, Width - LineSize * 2, LineSize)
            Dim HilightLeftRect As New Rectangle(LineSize, LineSize, LineSize, Height - LineSize * 2)

            Dim LightTopRect As New Rectangle(LineSize * 2, LineSize * 2, Width - LineSize * 4, LineSize)
            Dim LightLeftRect As New Rectangle(LineSize * 2, LineSize * 2, LineSize, Height - LineSize * 4)

            Dim DkShadowRightRect As New Rectangle(Width - LineSize * 2, LineSize, LineSize, Height - LineSize * 2)
            Dim DkShadowBottomRect As New Rectangle(LineSize, Height - LineSize * 2, Width - LineSize * 2, LineSize)

            Dim ShadowRightRect As New Rectangle(Width - LineSize * 3, LineSize * 2, LineSize, Height - LineSize * 4)
            Dim ShadowBottomRect As New Rectangle(LineSize * 2, Height - LineSize * 3, Width - LineSize * 4, LineSize)

            Dim Filling As New Rectangle(LightLeftRect.Right, LightTopRect.Bottom, ShadowRightRect.Left - LightLeftRect.Right, ShadowBottomRect.Top - LightTopRect.Bottom)

            Dim tw As Integer = Filling.Width / 2
            Dim th As Integer = LineSize * 1.75
            Dim TextRect As New Rectangle(Filling.X + (Filling.Width - tw) / 2, Filling.Y + (Filling.Height - th) / 2, tw, th)

            '#################################################################################

            G.Clear(WindowFrame)

            G.FillRectangle(BrushHilight, HilightTopRect)
            G.FillRectangle(BrushHilight, HilightLeftRect)

            G.FillRectangle(BrushLight, LightTopRect)
            G.FillRectangle(BrushLight, LightLeftRect)

            G.FillRectangle(BrushDkShadow, DkShadowRightRect)
            G.FillRectangle(BrushDkShadow, DkShadowBottomRect)

            G.FillRectangle(BrushShadow, ShadowRightRect)
            G.FillRectangle(BrushShadow, ShadowBottomRect)

            G.FillRectangle(New SolidBrush(BackColor), Filling)

            G.FillRectangle(New SolidBrush(ForeColor), TextRect)

            e.Graphics.DrawImage(B, New Point(0, 0))
            G.Dispose() : B.Dispose()
        End Sub

    End Class
End Namespace