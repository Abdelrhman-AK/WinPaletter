Imports System.ComponentModel

Namespace UI.WP

    <Description("ToolStripMenuItem fixed To respect dark/light mode")>
    Public Class ToolStripMenuItem : Inherits System.Windows.Forms.ToolStripMenuItem
        Protected Overrides Sub OnPaint(e As PaintEventArgs)
            MyBase.OnPaint(e)
            e.Graphics.TextRenderingHint = My.RenderingHint
            e.Graphics.Clear(BackColor)
            Using br As New SolidBrush(ForeColor) : e.Graphics.DrawString(Text, Font, br, New Rectangle(0, 0, Width, Height), TextAlign.ToStringFormat) : End Using
        End Sub
    End Class

End Namespace