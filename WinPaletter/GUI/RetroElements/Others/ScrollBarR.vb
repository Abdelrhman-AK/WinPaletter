Imports System.ComponentModel
Imports System.Drawing.Drawing2D

Namespace UI.Retro

    <Description("Retro ScrollBar with Windows 9x style")> Public Class ScrollBarR : Inherits Windows.Forms.Panel

        Sub New()
            DoubleBuffered = True
            BackColor = Color.FromArgb(192, 192, 192)
            BorderStyle = BorderStyle.None
        End Sub

#Region "Properties"
        Public Property ButtonHilight As Color = Color.White

#End Region

        Protected Overrides Sub OnPaint(e As PaintEventArgs)
            Dim G As Graphics = e.Graphics
            G.SmoothingMode = SmoothingMode.HighSpeed
            G.TextRenderingHint = My.RenderingHint
            DoubleBuffered = True

            '################################################################################# Customizer
            Dim Rect As New Rectangle(0, 0, Width - 1, Height - 1)
            '#################################################################################
            G.Clear(BackColor)
            Dim b As New HatchBrush(HatchStyle.Percent50, ButtonHilight, BackColor)
            G.FillRectangle(b, Rect)
        End Sub

    End Class

End Namespace

