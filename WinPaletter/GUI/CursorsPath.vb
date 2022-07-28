Imports System.Drawing.Drawing2D

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

    Public Function Move([Rectangle] As Rectangle, Optional Scale As Single = 1) As GraphicsPath
        Dim path As New GraphicsPath
        [Rectangle].Width = 21
        [Rectangle].Height = 21
        [Rectangle].X = 0
        [Rectangle].Y = 0

        Dim UL1 As Point = New Point([Rectangle].X + 11, [Rectangle].Y)
        Dim UL2 As Point = New Point(UL1.X - 4, [Rectangle].Y + 4)
        path.AddLine(UL1, UL2)

        Dim ULX1 As Point = New Point(UL2.X, UL2.Y + 1)
        Dim ULX2 As Point = New Point(ULX1.X + 3, ULX1.Y)
        path.AddLine(ULX1, ULX2)

        Dim MUL1 As Point = ULX2
        Dim MUL2 As Point = New Point(MUL1.X, ULX2.Y + 4)
        path.AddLine(MUL1, MUL2)

        Dim MULX1 As Point = New Point(MUL2.X, MUL2.Y + 1)
        Dim MULX2 As Point = New Point(MULX1.X - 5, MULX1.Y)
        path.AddLine(MULX1, MULX2)

        Dim LU1 As Point = MULX2
        Dim LU2 As Point = New Point(MULX2.X, MULX2.Y - 3)
        path.AddLine(LU1, LU2)

        Dim LUX1 As Point = New Point(LU2.X - 1, LU2.Y)
        Dim LUX2 As Point = New Point([Rectangle].X, [Rectangle].Y + 11)
        path.AddLine(LUX1, LUX2)

        Dim LDX1 As Point = LUX2
        Dim LDX2 As Point = New Point(LDX1.X + 4, LDX1.Y + 4)
        path.AddLine(LDX1, LDX2)

        Dim LD1 As Point = New Point(LDX2.X + 1, LDX2.Y)
        Dim LD2 As Point = New Point(LD1.X, LD1.Y - 2)
        path.AddLine(LD1, LD2)

        Dim L1 As Point = New Point(LD2.X, LD2.Y - 1)
        Dim L2 As Point = New Point(L1.X + 5, L1.Y)
        path.AddLine(L1, L2)

        Dim DL1 As Point = L2
        Dim DL2 As Point = New Point(L2.X, LD2.Y + 3)
        path.AddLine(DL1, DL2)

        Dim DX1 As Point = New Point(DL2.X, DL2.Y + 1)
        Dim DX2 As Point = New Point(DX1.X - 3, DX1.Y)
        path.AddLine(DX1, DX2)

        Dim DLX1 As Point = New Point(DX2.X, DX2.Y + 1)
        Dim DLX2 As Point = New Point(DLX1.X + 4, DLX1.Y + 4)
        path.AddLine(DLX1, DLX2)

        path.AddPath(MirrorRight(path), False)

        Dim m As Matrix = New Matrix()
        m.Scale(Scale, Scale, MatrixOrder.Append)
        m.Translate(1, 1, MatrixOrder.Append)
        path.Transform(m)

        Return path
    End Function

    Public Function NS([Rectangle] As Rectangle, Optional Scale As Single = 1) As GraphicsPath
        Dim path As New GraphicsPath
        [Rectangle].Width = 9
        [Rectangle].Height = 23
        [Rectangle].X = 0
        [Rectangle].Y = 0


        Dim UL1 As Point = New Point([Rectangle].X + 4, [Rectangle].Y)
        Dim UL2 As Point = New Point([Rectangle].X, [Rectangle].Y + 4)
        path.AddLine(UL1, UL2)

        Dim ULX1 As Point = New Point(UL2.X, UL2.Y + 1)
        Dim ULX2 As Point = New Point(ULX1.X + 3, ULX1.Y)
        path.AddLine(ULX1, ULX2)

        Dim MUL1 As Point = ULX2
        Dim MUL2 As Point = New Point(MUL1.X, MUL1.Y + 12)
        path.AddLine(MUL1, MUL2)

        Dim DL1 As Point = MUL2
        Dim DL2 As Point = New Point(MUL2.X - 3, MUL2.Y)
        path.AddLine(DL1, DL2)

        Dim DX1 As Point = New Point(DL2.X, DL2.Y + 1)
        Dim DX2 As Point = New Point(DX1.X + 4, DX1.Y + 4)
        path.AddLine(DX1, DX2)

        path.AddPath(MirrorRight(path), False)

        Dim m As Matrix = New Matrix()
        m.Scale(Scale, Scale, MatrixOrder.Append)
        m.Translate(1, 1, MatrixOrder.Append)
        path.Transform(m)

        Return path
    End Function

    Public Function NESW([Rectangle] As Rectangle, Optional Scale As Single = 1) As GraphicsPath
        Dim path As New GraphicsPath
        [Rectangle].Width = 17
        [Rectangle].Height = 17
        [Rectangle].X = -4
        [Rectangle].Y = -1

        Dim UR1 As Point = New Point([Rectangle].X + [Rectangle].Width, [Rectangle].Y)
        Dim UR2 As Point = New Point(UR1.X - 4, UR1.Y)
        path.AddLine(UR1, UR2)

        Dim RX1 As Point = New Point(UR2.X, UR2.Y + 1)
        Dim RX2 As Point = New Point(RX1.X + 1, RX1.Y + 1)
        path.AddLine(RX1, RX2)

        Dim LX1 As Point = New Point(RX2.X + 1, RX2.Y + 1)
        Dim LX2 As Point = New Point(LX1.X - 9, LX1.Y + 9)
        path.AddLine(LX1, LX2)

        Dim DX1 As Point = New Point(LX2.X - 1, LX2.Y - 1)
        Dim DX2 As Point = New Point(DX1.X - 1, DX1.Y - 1)
        path.AddLine(DX1, DX2)

        Dim L1 As Point = New Point(DX2.X - 1, DX2.Y)
        Dim L2 As Point = New Point(L1.X, [Rectangle].Y + [Rectangle].Height - 1)
        path.AddLine(L1, L2)

        Dim D1 As Point = New Point(L2.X + 1, L2.Y)
        Dim D2 As Point = New Point(D1.X + 5, D1.Y)
        path.AddLine(D1, D2)

        Dim DL1 As Point = New Point(D2.X, D2.Y - 1)
        Dim DL2 As Point = New Point(DL1.X - 1, DL1.Y - 1)
        path.AddLine(DL1, DL2)

        Dim LX3 As Point = New Point(DL2.X - 1, DL2.Y - 1)
        Dim LX4 As Point = New Point(LX3.X + 9, LX3.Y - 9)
        path.AddLine(LX3, LX4)

        Dim DR1 As Point = New Point(LX4.X + 1, LX4.Y + 1)
        Dim DR2 As Point = New Point(DR1.X + 1, DR1.Y + 1)
        path.AddLine(DR1, DR2)

        Dim R1 As Point = New Point(DR2.X + 1, DR2.Y)
        Dim R2 As Point = New Point(R1.X, [Rectangle].Y)
        path.AddLine(R1, R2)

        path.CloseFigure()


        Dim m As Matrix = New Matrix()
        m.Scale(Scale, Scale, MatrixOrder.Append)
        m.Translate(1, 1, MatrixOrder.Append)
        path.Transform(m)

        Return path
    End Function

    Public Function NWSE([Rectangle] As Rectangle, Optional Scale As Single = 1) As GraphicsPath
        Dim path As GraphicsPath = NESW([Rectangle])
        [Rectangle].Width = 17
        [Rectangle].Height = 17
        [Rectangle].X = -2
        [Rectangle].Y = 0

        Dim flipXMatrix As Matrix = New Matrix(-1, 0, 0, 1, [Rectangle].Width, 0)
        Dim transformMatrix As Matrix = New Matrix()
        transformMatrix.Multiply(flipXMatrix)
        path.Transform(transformMatrix)

        Dim m As Matrix = New Matrix()
        m.Scale(Scale, Scale, MatrixOrder.Append)
        m.Translate(1, 1, MatrixOrder.Append)
        path.Transform(m)

        Return path
    End Function

    Private Function MirrorLeft(ByVal path As GraphicsPath) As GraphicsPath
        Dim r = path.GetBounds()
        Dim p = CType(path.Clone(), GraphicsPath)
        p.Transform(New Matrix(-1, 0, 0, 1, 2 * r.Left, 0))
        Return p
    End Function

    Private Function MirrorRight(ByVal path As GraphicsPath) As GraphicsPath
        Dim r = path.GetBounds()
        Dim p = CType(path.Clone(), GraphicsPath)
        p.Transform(New Matrix(-1, 0, 0, 1, 2 * (r.Left + r.Width), 0))
        Return p
    End Function

    Function PointOnArc([Rectangle] As Rectangle, Angle As Single) As Point

        Dim P As New Point With {
            .X = ([Rectangle].Left + [Rectangle].Width / 2) + (([Rectangle].Width / 2) * Math.Cos(Angle)),
            .Y = ([Rectangle].Top + [Rectangle].Height / 2) + (([Rectangle].Height / 2) * Math.Sin(Angle))
        }

        Return P
    End Function

End Class
