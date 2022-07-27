﻿Imports System.Drawing.Drawing2D

Public Class CursorsPath

    Public Function DefaultCursor([Rectangle] As Rectangle, Optional Scale As Single = 1) As GraphicsPath
        Dim path As New GraphicsPath
        Dim R As New Rectangle([Rectangle].X, [Rectangle].Y, 12, 18)

        '#### Left Border
        Dim LLine1 As New Point(R.X, R.Y)
        Dim LLine2 As New Point(R.X, R.Y + R.Height - 3)
        path.AddLine(LLine1, LLine2)

        '#### Left Down Border
        Dim DLLine1 As Point = LLine2 + New Point(0, 2)
        Dim DLLine2 As New Point(DLLine1.X + 4, DLLine1.Y - 4)
        path.AddLine(DLLine1, DLLine2)

        '#### Left Down Handle Border
        Dim DLHLine1 As Point = DLLine2
        Dim DLHLine2 As New Point(DLHLine1.X + 3, R.Y + R.Height)
        path.AddLine(DLHLine1, DLHLine2)

        '#### Down Handle Border
        Dim DHLine1 As Point = DLHLine2
        Dim DHLine2 As New Point(DHLine1.X + 2, DHLine1.Y - 1)
        If Scale > 1 Then path.AddLine(DHLine1, DHLine2) Else path.AddArc(DHLine1.X, DHLine2.Y, DHLine2.X - DHLine1.X, DHLine1.Y - DHLine2.Y, 0, 90)

        '#### Right Down Handle Border
        Dim DRHLine1 As Point = DHLine2
        Dim DRHLine2 As New Point(DLHLine1.X + 3, DLHLine1.Y - 1)
        path.AddLine(DRHLine1, DRHLine2)

        '#### Right Down Border
        Dim DRLine1 As Point = DRHLine2
        Dim DRLine2 As New Point(R.X + R.Width, DLHLine1.Y - 1)
        path.AddLine(DRLine1, DRLine2)

        '#### Right Border
        Dim RLine1 As Point = DRLine2
        Dim RLine2 As Point = LLine1
        path.AddLine(RLine1, RLine2)

        path.CloseFigure()

        Dim m As Matrix = New Matrix()
        m.Scale(Scale, Scale, MatrixOrder.Append)
        m.Translate(1, 1, MatrixOrder.Append)
        path.Transform(m)

        Return path
    End Function

    Public Function Busy([Rectangle] As Rectangle, Optional Scale As Single = 1) As GraphicsPath
        [Rectangle].Width = 22
        [Rectangle].Height = 22

        Dim path As New GraphicsPath
        path.AddEllipse([Rectangle].X, [Rectangle].Y, 22, 22)
        Dim R As New Rectangle([Rectangle].X + 5, [Rectangle].Y + 5, 12, 12)
        path.AddEllipse(R)
        path.CloseFigure()

        Dim m As Matrix = New Matrix()
        m.Scale(Scale, Scale, MatrixOrder.Append)
        m.Translate(1, 1, MatrixOrder.Append)
        path.Transform(m)

        Return path
    End Function

    Public Function BusyLoader([Rectangle] As Rectangle, Angle As Single, Optional Scale As Single = 1) As GraphicsPath
        [Rectangle].X += 0
        [Rectangle].Y += 0

        [Rectangle].Width = 22
        [Rectangle].Height = 22

        Dim path As New GraphicsPath
        Dim CPoint As New Point([Rectangle].X + [Rectangle].Width / 2, [Rectangle].Y + [Rectangle].Height / 2)
        Dim R As New Rectangle([Rectangle].X + 5, [Rectangle].Y + 5, 12, 12)

        Dim innerR = 15
        Dim thickness = 10
        Dim arcLength = 50
        Dim outerR = innerR + thickness

        Dim outerRect = [Rectangle]
        Dim innerRect = R

        path.AddArc(outerRect, Angle, arcLength)
        path.AddArc(innerRect, Angle + arcLength, -arcLength)

        path.CloseFigure()

        Dim m As Matrix = New Matrix()
        m.Scale(Scale, Scale, MatrixOrder.Append)
        m.Translate(1, 1, MatrixOrder.Append)
        path.Transform(m)

        Return path
    End Function


    Public Function AppLoading([Rectangle] As Rectangle, Optional Scale As Single = 1) As GraphicsPath
        [Rectangle].Width = 16
        [Rectangle].Height = 16

        Dim path As New GraphicsPath
        path.AddEllipse([Rectangle])
        Dim R As New Rectangle([Rectangle].X + 4, [Rectangle].Y + 4, 8, 8)
        path.AddEllipse(R)
        path.CloseFigure()

        Dim m As Matrix = New Matrix()
        m.Scale(Scale, Scale, MatrixOrder.Append)
        m.Translate(1, 1, MatrixOrder.Append)
        path.Transform(m)

        Return path
    End Function

    Public Function AppLoaderCircle([Rectangle] As Rectangle, Angle As Single, Optional Scale As Single = 1) As GraphicsPath
        [Rectangle].Width = 16
        [Rectangle].Height = 16

        Dim path As New GraphicsPath
        Dim CPoint As New Point([Rectangle].X + [Rectangle].Width / 2, [Rectangle].Y + [Rectangle].Height / 2)
        Dim R As New Rectangle([Rectangle].X + 4, [Rectangle].Y + 4, 8, 8)

        Dim innerR = 15
        Dim thickness = 6
        Dim arcLength = 50
        Dim outerR = innerR + thickness

        Dim outerRect = [Rectangle]
        Dim innerRect = R

        path.AddArc(outerRect, Angle, arcLength)
        path.AddArc(innerRect, Angle + arcLength, -arcLength)

        path.CloseFigure()

        Dim m As Matrix = New Matrix()
        m.Scale(Scale, Scale, MatrixOrder.Append)
        m.Translate(1, 1, MatrixOrder.Append)
        path.Transform(m)

        Return path
    End Function



    Function PointOnArc([Rectangle] As Rectangle, Angle As Single) As Point

        Dim P As New Point With {
            .X = ([Rectangle].Left + [Rectangle].Width / 2) + (([Rectangle].Width / 2) * Math.Cos(Angle)),
            .Y = ([Rectangle].Top + [Rectangle].Height / 2) + (([Rectangle].Height / 2) * Math.Sin(Angle))
        }

        Return P
    End Function

End Class
