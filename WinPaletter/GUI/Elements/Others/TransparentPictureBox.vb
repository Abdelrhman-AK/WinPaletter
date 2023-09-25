Imports System.ComponentModel

Namespace UI.WP

    <Description("PictureBox that supports transparent background")>
    Public Class TransparentPictureBox : Inherits PictureBox
        Protected Overrides Sub OnPaintBackground(e As PaintEventArgs)
            MyBase.OnPaintBackground(e)

            DoubleBuffered = True

            If Parent IsNot Nothing Then
                Dim index As Integer = Parent.Controls.GetChildIndex(Me)

                For i As Integer = Parent.Controls.Count - 1 To index + 1 Step -1
                    Dim c As Control = Parent.Controls(i)
                    If c.Bounds.IntersectsWith(Bounds) AndAlso c.Visible = True Then
                        Dim bmp As New Bitmap(c.Width, c.Height, e.Graphics)
                        c.DrawToBitmap(bmp, c.ClientRectangle)
                        e.Graphics.TranslateTransform(c.Left - Left, c.Top - Top)
                        e.Graphics.DrawImageUnscaled(bmp, Point.Empty)
                        e.Graphics.TranslateTransform(Left - c.Left, Top - c.Top)
                        bmp.Dispose()
                    End If
                Next
            End If
        End Sub
    End Class

End Namespace