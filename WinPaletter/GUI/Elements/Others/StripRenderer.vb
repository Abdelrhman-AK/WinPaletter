Imports System.ComponentModel

Namespace UI.WP

    <Description("StripRenderer fixed to respect dark/light mode")>
    Public Class StripRenderer : Inherits ToolStripSystemRenderer
        Public Sub New() : End Sub
        Protected Overrides Sub OnRenderToolStripBorder(e As ToolStripRenderEventArgs) : End Sub
    End Class

End Namespace