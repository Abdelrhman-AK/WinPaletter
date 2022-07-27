Imports WinPaletter.XenonCore
Imports System.IO
Imports System.Drawing.Imaging
Imports AnimCur

Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ApplyDarkMode(Me)
    End Sub

    Dim Noise As New TextureBrush(FadeBitmap(My.Resources.GaussianBlurOpaque, 0.2))

    Private Sub XenonButton1_Click(sender As Object, e As EventArgs) Handles XenonButton1.Click
        Dim b As New Bitmap(32, 32, PixelFormat.Format32bppPArgb)
        Dim G As Graphics = Graphics.FromImage(b)
        G.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
        Dim DefCur As New CursorsPath
        G.Clear(Color.Transparent)
        Dim Scale As Single = 1

#Region "Default Cursor"
        G.FillPath(Brushes.Red, DefCur.DefaultCursor(New Rectangle(0, 0, 32, 32), Scale))
        G.FillPath(Noise, DefCur.DefaultCursor(New Rectangle(0, 0, 32, 32), Scale))
        G.DrawPath(Pens.White, DefCur.DefaultCursor(New Rectangle(0, 0, 32, 32), Scale))
#End Region

#Region "Busy"
        'Dim C1 As Color = Color.FromArgb(42, 151, 243)
        'Dim C2 As Color = Color.FromArgb(37, 204, 255)
        'G.FillPath(New SolidBrush(C1), DefCur.Busy(New Rectangle(0, 0, 22, 22),Scale))
        'G.FillPath(New SolidBrush(C2), DefCur.BusyLoader(New Rectangle(0, 0, 22, 22), 50,Scale))
#End Region

#Region "AppLoading"
        'Dim C1 As Color = Color.FromArgb(42, 151, 243)
        'Dim C2 As Color = Color.FromArgb(37, 204, 255)

        'Dim CurRect As Rectangle = New Rectangle(0, 8, 32, 32)
        'Dim LoadRect As Rectangle = New Rectangle(6, 0, 15, 15)

        'G.FillPath(Brushes.White, DefCur.DefaultCursor(CurRect, Scale))
        'G.FillPath(Noise, DefCur.DefaultCursor(CurRect, Scale))
        'G.DrawPath(Pens.Black, DefCur.DefaultCursor(CurRect, Scale))

        'G.FillPath(New SolidBrush(C1), DefCur.AppLoading(LoadRect, Scale))
        'G.FillPath(New SolidBrush(C2), DefCur.AppLoaderCircle(LoadRect, 90, Scale))
#End Region


        Dim g2 As Graphics = Panel1.CreateGraphics
        g2.SmoothingMode = G.SmoothingMode
        g2.Clear(Color.White)
        g2.DrawImage(b, New Point(0, 0))



        Dim fs As FileStream = New System.IO.FileStream("D:\cur.cur", FileMode.Create)

        Dim EO As New EOIcoCurWriter(fs, 1, EOIcoCurWriter.IcoCurType.Cursor)

        EO.WriteBitmap(b, Nothing, New Point(1, 1))

        fs.Close()
    End Sub

End Class