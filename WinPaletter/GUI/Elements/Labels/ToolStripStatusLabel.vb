Imports System.ComponentModel

Namespace UI.WP

    <Description("ToolStripStatusLabel fixed to respect dark/light mode")> Public Class ToolStripStatusLabel : Inherits Windows.Forms.ToolStripStatusLabel
        Protected Overrides Sub OnPaint(e As PaintEventArgs)
            e.Graphics.TextRenderingHint = My.RenderingHint

            Using br As New SolidBrush(BackColor) : e.Graphics.FillRectangle(br, New Rectangle(0, 0, Width, Height)) : End Using
            Using br As New SolidBrush(ForeColor) : e.Graphics.DrawString(Text, Font, br, New Rectangle(0, 0, Width, Height), TextAlign.ToStringFormat) : End Using
        End Sub
    End Class

End Namespace