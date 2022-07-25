Imports System.Drawing.Drawing2D
Imports WinPaletter.XenonCore
Imports System.Drawing.IconLib
Imports System.Drawing.IconLib.ColorProcessing
Imports System.IO
Imports System.Drawing.Imaging

Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ApplyDarkMode(Me)
    End Sub

    Dim Noise As New TextureBrush(My.Resources.GaussianBlur)

    Private Sub XenonButton1_Click(sender As Object, e As EventArgs) Handles XenonButton1.Click
        Dim b As New Bitmap(32, 32, PixelFormat.Format32bppArgb)
        Dim G As Graphics = Graphics.FromImage(b)
        G.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
        Dim DefCur As New CursorsPath
        G.Clear(Color.Transparent)
        Dim Scale As Single = 1

#Region "Default Cursor"
        'G.FillPath(Brushes.White, DefCur.DefaultCursor(New Rectangle(0, 0, 32, 32),Scale))
        'G.DrawPath(Pens.Black, DefCur.DefaultCursor(New Rectangle(0, 0, 32, 32),Scale))
#End Region


#Region "Busy"
        'Dim C1 As Color = Color.FromArgb(42, 151, 243)
        'Dim C2 As Color = Color.FromArgb(37, 204, 255)
        'G.FillPath(New SolidBrush(C1), DefCur.Busy(New Rectangle(0, 0, 22, 22),Scale))
        'G.FillPath(New SolidBrush(C2), DefCur.BusyLoader(New Rectangle(0, 0, 22, 22), 50,Scale))
#End Region

#Region "AppLoading"
        Dim C1 As Color = Color.FromArgb(42, 151, 243)
        Dim C2 As Color = Color.FromArgb(37, 204, 255)

        Dim CurRect As Rectangle = New Rectangle(0, 8, 32, 32)
        Dim LoadRect As Rectangle = New Rectangle(6, 0, 15, 15)

        G.FillPath(Brushes.White, DefCur.DefaultCursor(CurRect, Scale))
        'G.FillPath(Noise, DefCur.DefaultCursor(CurRect, Scale))
        G.DrawPath(Pens.Black, DefCur.DefaultCursor(CurRect, Scale))

        G.FillPath(New SolidBrush(C1), DefCur.AppLoading(LoadRect, Scale))
        G.FillPath(New SolidBrush(C2), DefCur.AppLoaderCircle(LoadRect, 90, Scale))
#End Region


        Dim g2 As Graphics = Panel1.CreateGraphics
        g2.SmoothingMode = G.SmoothingMode
        g2.Clear(Color.White)
        g2.DrawImage(b, New Point(0, 0))

        ConvertToIco(b, "D:\cur.cur", 32)

    End Sub

    Public Sub ConvertToIco(ByVal img As Image, ByVal file As String, ByVal size As Integer)
        Dim icon As Icon

        Using msImg = New MemoryStream()

            Using msIco = New MemoryStream()
                img.Save(msImg, ImageFormat.Png)

                Using bw = New BinaryWriter(msIco)
                    bw.Write(CShort(0))
                    bw.Write(CShort(1))
                    bw.Write(CShort(1))
                    bw.Write(CByte(size))
                    bw.Write(CByte(size))
                    bw.Write(CByte(0))
                    bw.Write(CByte(0))
                    bw.Write(CShort(0))
                    bw.Write(CShort(32))
                    bw.Write(CInt(msImg.Length))
                    bw.Write(22)
                    bw.Write(msImg.ToArray())
                    bw.Flush()
                    bw.Seek(0, SeekOrigin.Begin)
                    icon = New Icon(msIco)
                End Using
            End Using
        End Using

        Using fs = New FileStream(file, FileMode.Create, FileAccess.Write)
            icon.Save(fs)
        End Using
    End Sub

End Class