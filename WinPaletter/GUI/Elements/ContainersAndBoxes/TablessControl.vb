Imports System.ComponentModel

Namespace UI.WP

    <Description("TabControl but without tabs for WinPaletter UI")> Public Class TablessControl : Inherits Windows.Forms.TabControl
        Public Sub New()
            SetStyle(ControlStyles.ResizeRedraw, True)
            Me.DoubleBuffered = True
        End Sub

        Protected Overrides Sub WndProc(ByRef m As Message)
            If m.Msg = &H1328 AndAlso Not DesignMode Then
                m.Result = CType(1, IntPtr)
            Else
                MyBase.WndProc(m)
            End If
        End Sub

    End Class

End Namespace