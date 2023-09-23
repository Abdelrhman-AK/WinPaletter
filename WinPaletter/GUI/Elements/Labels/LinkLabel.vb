Imports System.ComponentModel

Namespace UI.WP

    <Description("LinkLabel but with a proper hand cursor")>
    Public Class LinkLabel : Inherits Windows.Forms.LinkLabel

        Const WM_SETCURSOR As Integer = 32, IDC_HAND As Integer = 32649

        Protected Overrides Sub WndProc(ByRef m As Message)
            If m.Msg = WM_SETCURSOR Then
                Dim cursor As Integer = NativeMethods.User32.LoadCursor(0, IDC_HAND)
                NativeMethods.User32.SetCursor(cursor)
                m.Result = IntPtr.Zero
                Return
            End If

            MyBase.WndProc(m)
        End Sub

    End Class

End Namespace