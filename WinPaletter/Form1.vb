Imports System.Drawing.Drawing2D
Imports WinPaletter.XenonCore
Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ApplyDarkMode(Me)
    End Sub

    Private Sub XenonButton1_Click(sender As Object, e As EventArgs) Handles XenonButton1.Click
        Dim G As Graphics = Panel1.CreateGraphics
        G.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
        Dim DefCur As New CursorsPath
        G.Clear(Color.White)

        'G.FillPath(Brushes.White, DefCur.DefaultCursor(New Rectangle(0, 0, 32, 32)))
        'G.DrawPath(Pens.Black, DefCur.DefaultCursor(New Rectangle(0, 0, 32, 32)))

        G.FillPath(Brushes.RoyalBlue, DefCur.Busy(New Rectangle(15, 15, 22, 22), 2.5))
        G.DrawPath(Pens.Red, DefCur.BusyLoader(New Rectangle(15, 15, 22, 22), 0, 2.5))

        'G.FillPath(Brushes.Red, DefCur.BusyLoader(New Rectangle(15, 15, 32, 32), 0, 2.5))

        'G.FillEllipse(Brushes.RoyalBlue, New Rectangle(15, 15, 28, 28))
        'G.FillEllipse(New SolidBrush(Panel1.BackColor), New Rectangle(20, 20, 18, 18))

        G.Flush()


    End Sub
End Class