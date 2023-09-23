Imports System.ComponentModel

Namespace UI.WP

    <Description("TreeView fixed to respect dark/light mode")>
    Public Class TreeView : Inherits Windows.Forms.TreeView
        Protected Overrides ReadOnly Property CreateParams As CreateParams
            Get
                Dim parms As CreateParams = MyBase.CreateParams
                parms.Style = parms.Style Or &H80
                Return parms
            End Get
        End Property
    End Class

End Namespace