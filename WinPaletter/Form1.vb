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
        Dim fs As FileStream = New FileStream("D:\cur.cur", FileMode.Create)
        Dim EO As New EOIcoCurWriter(fs, 7, EOIcoCurWriter.IcoCurType.Cursor)

        For i As Single = 1 To 4 Step 0.5
            EO.WriteBitmap(Draw(i), Nothing, New Point(5 * i - 0.5 * i, 10 * i - 0.5 * i)) 'New Point(1, 1)
        Next

        fs.Close()

    End Sub



    Function Draw(ByVal Scale As Single) As Bitmap
        Dim b As New Bitmap(32 * Scale, 32 * Scale, PixelFormat.Format32bppPArgb)
        Dim G As Graphics = Graphics.FromImage(b)
        G.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
        Dim DefCur As New CursorsPath
        G.Clear(Color.Transparent)

#Region "Default Cursor"
        'G.FillPath(Brushes.Red, DefCur.DefaultCursor(New Rectangle(0, 0, b.Width, b.Height), Scale))
        'G.FillPath(Noise, DefCur.DefaultCursor(New Rectangle(0, 0, b.Width, b.Height), Scale))
        'G.DrawPath(Pens.White, DefCur.DefaultCursor(New Rectangle(0, 0, b.Width, b.Height), Scale))
#End Region

#Region "Help Cursor"
        'G.FillPath(Brushes.White, DefCur.DefaultCursor(New Rectangle(0, 0, b.Width, b.Height), Scale))
        'G.DrawPath(Pens.Black, DefCur.DefaultCursor(New Rectangle(0, 0, b.Width, b.Height), Scale))
        'G.FillPath(Brushes.White, DefCur.Help(New Rectangle(11, 6, b.Width, b.Height), Scale))
        'G.DrawPath(Pens.Black, DefCur.Help(New Rectangle(11, 6, b.Width, b.Height), Scale))
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

        'Dim CurRect As Rectangle = New Rectangle(0, 8, b.Width, b.Height)
        'Dim LoadRect As Rectangle = New Rectangle(6, 0, 15, 15)

        'G.FillPath(Brushes.White, DefCur.DefaultCursor(CurRect, Scale))
        'G.FillPath(Noise, DefCur.DefaultCursor(CurRect, Scale))
        'G.DrawPath(Pens.Black, DefCur.DefaultCursor(CurRect, Scale))

        'G.FillPath(New SolidBrush(C1), DefCur.AppLoading(LoadRect, Scale))
        'G.FillPath(New SolidBrush(C2), DefCur.AppLoaderCircle(LoadRect, 90, Scale))
#End Region

#Region "None"
        'G.FillPath(Brushes.White, DefCur.NoneBackground(New Rectangle(0, 0, b.Width, b.Height), Scale))
        'G.FillPath(Brushes.Red, DefCur.None(New Rectangle(0, 0, b.Width, b.Height), Scale))
#End Region

#Region "Move"
        'G.FillPath(Brushes.White, DefCur.Move(New Rectangle(0, 0, b.Width, b.Height), Scale))
        'G.DrawPath(Pens.Red, DefCur.Move(New Rectangle(0, 0, b.Width, b.Height), Scale))
#End Region

#Region "Up"
        'G.FillPath(Brushes.White, DefCur.Up(New Rectangle(0, 0, b.Width, b.Height), Scale))
        'G.DrawPath(Pens.Black, DefCur.Up(New Rectangle(0, 0, b.Width, b.Height), Scale))
#End Region

#Region "NS"
        'G.FillPath(Brushes.White, DefCur.NS(New Rectangle(0, 0, b.Width, b.Height), Scale))
        'G.DrawPath(Pens.Black, DefCur.NS(New Rectangle(0, 0, b.Width, b.Height), Scale))
#End Region

#Region "EW"
        'G.FillPath(Brushes.White, DefCur.EW(New Rectangle(0, 0, b.Width, b.Height), Scale))
        'G.DrawPath(Pens.Black, DefCur.EW(New Rectangle(0, 0, b.Width, b.Height), Scale))
#End Region

#Region "NESW"
        'G.FillPath(Brushes.White, DefCur.NESW(New Rectangle(0, 0, b.Width, b.Height), Scale))
        'G.DrawPath(Pens.Red, DefCur.NESW(New Rectangle(0, 0, b.Width, b.Height), Scale))
#End Region

#Region "NWSE"
        'G.FillPath(Brushes.White, DefCur.NWSE(New Rectangle(0, 0, b.Width, b.Height), Scale))
        'G.DrawPath(Pens.Black, DefCur.NWSE(New Rectangle(0, 0, b.Width, b.Height), Scale))
#End Region

#Region "Pen"
        'G.FillPath(Brushes.White, DefCur.PenBackground(New Rectangle(0, 0, b.Width, b.Height), Scale))
        'G.DrawPath(Pens.Black, DefCur.Pen(New Rectangle(0, 0, b.Width, b.Height), Scale))
#End Region

#Region "Link"
        'G.FillPath(Brushes.White, DefCur.Hand(New Rectangle(0, 0, b.Width, b.Height), Scale))
        'G.DrawPath(Pens.Black, DefCur.Hand(New Rectangle(0, 0, b.Width, b.Height), Scale))
#End Region

#Region "Pin"
        'G.FillPath(Brushes.White, DefCur.Hand(New Rectangle(0, 0, b.Width, b.Height), Scale))
        'G.DrawPath(Pens.Black, DefCur.Hand(New Rectangle(0, 0, b.Width, b.Height), Scale))
        'G.DrawPath(New Pen(Color.Black, 2), DefCur.Pin(New Rectangle(15, 11, b.Width, b.Height), Scale))
        'G.FillPath(Brushes.White, DefCur.Pin(New Rectangle(15, 11, b.Width, b.Height), Scale))
        'G.FillPath(Brushes.Black, DefCur.Pin_CenterPoint(New Rectangle(15, 11, b.Width, b.Height), Scale))
#End Region

#Region "Person"
        'G.FillPath(Brushes.White, DefCur.Hand(New Rectangle(0, 0, b.Width, b.Height), Scale))
        'G.DrawPath(Pens.Black, DefCur.Hand(New Rectangle(0, 0, b.Width, b.Height), Scale))
        'G.DrawPath(New Pen(Color.Black, 2), DefCur.Person(New Rectangle(19, 17, b.Width, b.Height), Scale))
        'G.FillPath(Brushes.White, DefCur.Person(New Rectangle(19, 17, b.Width, b.Height), Scale))
#End Region

#Region "IBeam"
        'G.FillPath(Brushes.Black, DefCur.IBeam(New Rectangle(0, 0, b.Width, b.Height), Scale))
        'G.DrawPath(Pens.White, DefCur.IBeam(New Rectangle(0, 0, b.Width, b.Height), Scale))
#End Region

#Region "Cross"
        'G.FillPath(Brushes.Black, DefCur.IBeam(New Rectangle(0, 0, b.Width, b.Height), Scale))
        'G.DrawPath(Pens.Black, DefCur.Cross(New Rectangle(0, 0, b.Width, b.Height), Scale))
#End Region

        Dim g2 As Graphics = Panel1.CreateGraphics
        g2.SmoothingMode = G.SmoothingMode
        g2.Clear(Color.White)
        g2.DrawImage(b, New Point(0, 0))

        b.Save("D:\x.png")

        Return b
    End Function

    Private Sub Form1_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Draw(1)
    End Sub
End Class