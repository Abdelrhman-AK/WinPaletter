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
        path.AddLine(DHLine1, DHLine2)

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

        'path.CloseFigure()

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
        [Rectangle].Width = 22
        [Rectangle].Height = 22

        Dim path As New GraphicsPath
        Dim R As New Rectangle([Rectangle].X + 5, [Rectangle].Y + 5, 12, 12)
        Dim Increment As Single = 90.0F

        Dim Outer_Pre As Point = PointOnArc([Rectangle], Angle)
        Dim Inner_Pre As Point = PointOnArc(R, Angle)

        Dim Outer_Post As Point = PointOnArc([Rectangle], Angle + Increment)
        Dim Inner_Post As Point = PointOnArc(R, Angle + Increment)

        path.AddArc(R, Angle, Increment)

        path.AddLine(Inner_Pre, Outer_Pre)

        path.AddArc([Rectangle], Angle, Increment)

        path.AddLine(Inner_Post, Outer_Post)


        'path.CloseFigure()

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
