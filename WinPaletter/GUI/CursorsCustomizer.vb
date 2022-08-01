Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports WinPaletter.XenonCore

Public Module Paths
    Enum CursorType
        Arrow
        Help
        Busy
        AppLoading
        None
        Move
        Up
        NS
        EW
        NESW
        NWSE
        Pen
        Link
        Pin
        Person
        IBeam
        Cross
    End Enum

    Dim Noise As New TextureBrush(FadeBitmap(My.Resources.GaussianBlurOpaque, 0.2))

    Public Function Draw([Cursor] As CursorType,
                         [BackColor1] As Color, [BackColor2] As Color, [BackColorGradient] As Boolean, [BackColorGradientMode] As LinearGradientMode,
                         [LineColor1] As Color, [LineColor2] As Color, [LineColorGradient] As Boolean, [LineColorGradientMode] As LinearGradientMode,
                         [LoadingCircleBack1] As Color, [LoadingCircleBack2] As Color, [LoadingCircleBackGradient] As Boolean, [LoadingCircleBackGradientMode] As LinearGradientMode,
                         [LoadingCircleHot1] As Color, [LoadingCircleHot2] As Color, [LoadingCircleHotGradient] As Boolean, [LoadingCircleHotGradientMode] As LinearGradientMode,
                         [LineThickness] As Single, Optional Scale As Single = 1) As Bitmap


        Dim b As New Bitmap(32 * Scale, 32 * Scale, PixelFormat.Format32bppPArgb)
        Dim G As Graphics = Graphics.FromImage(b)
        G.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
        G.Clear(Color.Transparent)

#Region "Rectangles Helpers"
        Dim _Arrow As New Rectangle(0, 0, b.Width, b.Height)
        Dim _Help As New Rectangle(11, 6, b.Width, b.Height)
        Dim _Busy As New Rectangle(0, 0, 22, 22)
        Dim _Angle As Single = 180
        Dim _CurRect As New Rectangle(0, 8, b.Width, b.Height)
        Dim _LoadRect As New Rectangle(6, 0, 15, 15)
        Dim _Pin As New Rectangle(15, 11, b.Width, b.Height)
        Dim _Person As New Rectangle(19, 17, b.Width, b.Height)
#End Region




        Select Case [Cursor]
            Case CursorType.Arrow
                Dim BB, BL As Brush
                If [BackColorGradient] Then
                    BB = New LinearGradientBrush(_Arrow, [BackColor1], [BackColor2], [BackColorGradientMode])
                Else
                    BB = New SolidBrush([BackColor1])
                End If
                If [LineColorGradientMode] Then
                    BL = New LinearGradientBrush(_Arrow, [LineColor1], [LineColor2], [LineColorGradientMode])
                Else
                    BL = New SolidBrush([LineColor1])
                End If
                Dim PL As New Pen(BL, [LineThickness])

                G.FillPath(BB, DefaultCursor(_Arrow, Scale))
                G.DrawPath(PL, DefaultCursor(_Arrow, Scale))

            Case CursorType.Help
                Dim BB, BL As Brush
                If [BackColorGradient] Then
                    BB = New LinearGradientBrush(_Arrow, [BackColor1], [BackColor2], [BackColorGradientMode])
                Else
                    BB = New SolidBrush([BackColor1])
                End If
                If [LineColorGradientMode] Then
                    BL = New LinearGradientBrush(_Arrow, [LineColor1], [LineColor2], [LineColorGradientMode])
                Else
                    BL = New SolidBrush([LineColor1])
                End If
                Dim PL As New Pen(BL, [LineThickness])

                Dim BB_H, BL_H As Brush
                If [BackColorGradient] Then
                    BB_H = New LinearGradientBrush(_Help, [BackColor1], [BackColor2], [BackColorGradientMode])
                Else
                    BB_H = New SolidBrush([BackColor1])
                End If
                If [LineColorGradientMode] Then
                    BL_H = New LinearGradientBrush(_Help, [LineColor1], [LineColor2], [LineColorGradientMode])
                Else
                    BL_H = New SolidBrush([LineColor1])
                End If
                Dim PL_H As New Pen(BL_H, [LineThickness])

                G.FillPath(BB, DefaultCursor(_Arrow, Scale))
                G.DrawPath(PL, DefaultCursor(_Arrow, Scale))

                G.FillPath(BB_H, Help(_Help, Scale))
                G.DrawPath(PL_H, Help(_Help, Scale))

            Case CursorType.Busy

                Dim BC, BH As Brush
                If [LoadingCircleBackGradient] Then
                    BC = New LinearGradientBrush(_Busy, [LoadingCircleBack1], [LoadingCircleBack2], [LoadingCircleBackGradientMode])
                Else
                    BC = New SolidBrush([LoadingCircleBack1])
                End If
                If [LoadingCircleHotGradient] Then
                    BH = New LinearGradientBrush(_Busy, [LoadingCircleHot1], [LoadingCircleHot2], [LoadingCircleHotGradientMode])
                Else
                    BH = New SolidBrush([LoadingCircleHot1])
                End If

                G.FillPath(BC, Busy(_Busy, Scale))
                G.FillPath(BH, BusyLoader(_Busy, _Angle, Scale))

            Case CursorType.AppLoading
                Dim BB, BL As Brush
                If [BackColorGradient] Then
                    BB = New LinearGradientBrush(_CurRect, [BackColor1], [BackColor2], [BackColorGradientMode])
                Else
                    BB = New SolidBrush([BackColor1])
                End If
                If [LineColorGradientMode] Then
                    BL = New LinearGradientBrush(_CurRect, [LineColor1], [LineColor2], [LineColorGradientMode])
                Else
                    BL = New SolidBrush([LineColor1])
                End If
                Dim PL As New Pen(BL, [LineThickness])

                Dim BC, BH As Brush
                If [LoadingCircleBackGradient] Then
                    BC = New LinearGradientBrush(_LoadRect, [LoadingCircleBack1], [LoadingCircleBack2], [LoadingCircleBackGradientMode])
                Else
                    BC = New SolidBrush([LoadingCircleBack1])
                End If
                If [LoadingCircleHotGradient] Then
                    BH = New LinearGradientBrush(_LoadRect, [LoadingCircleHot1], [LoadingCircleHot2], [LoadingCircleHotGradientMode])
                Else
                    BH = New SolidBrush([LoadingCircleHot1])
                End If

                G.FillPath(BB, DefaultCursor(_CurRect, Scale))
                G.DrawPath(PL, DefaultCursor(_CurRect, Scale))

                G.FillPath(BC, AppLoading(_LoadRect, Scale))
                G.FillPath(BH, AppLoaderCircle(_LoadRect, _Angle, Scale))

            Case CursorType.None
                Dim BB, BL As Brush
                If [BackColorGradient] Then
                    BB = New LinearGradientBrush(_Arrow, [BackColor1], [BackColor2], [BackColorGradientMode])
                Else
                    BB = New SolidBrush([BackColor1])
                End If
                If [LineColorGradientMode] Then
                    BL = New LinearGradientBrush(_Arrow, [LineColor1], [LineColor2], [LineColorGradientMode])
                Else
                    BL = New SolidBrush([LineColor1])
                End If
                Dim PL As New Pen(BL, [LineThickness])

                G.FillPath(BB, NoneBackground(_Arrow, Scale))
                G.FillPath(BL, None(_Arrow, Scale))

            Case CursorType.Move
                Dim BB, BL As Brush
                If [BackColorGradient] Then
                    BB = New LinearGradientBrush(_Arrow, [BackColor1], [BackColor2], [BackColorGradientMode])
                Else
                    BB = New SolidBrush([BackColor1])
                End If
                If [LineColorGradientMode] Then
                    BL = New LinearGradientBrush(_Arrow, [LineColor1], [LineColor2], [LineColorGradientMode])
                Else
                    BL = New SolidBrush([LineColor1])
                End If
                Dim PL As New Pen(BL, [LineThickness])

                G.FillPath(BB, Move(_Arrow, Scale))
                G.DrawPath(PL, Move(_Arrow, Scale))

            Case CursorType.Up
                Dim BB, BL As Brush
                If [BackColorGradient] Then
                    BB = New LinearGradientBrush(_Arrow, [BackColor1], [BackColor2], [BackColorGradientMode])
                Else
                    BB = New SolidBrush([BackColor1])
                End If
                If [LineColorGradientMode] Then
                    BL = New LinearGradientBrush(_Arrow, [LineColor1], [LineColor2], [LineColorGradientMode])
                Else
                    BL = New SolidBrush([LineColor1])
                End If
                Dim PL As New Pen(BL, [LineThickness])

                G.FillPath(BB, Up(_Arrow, Scale))
                G.DrawPath(PL, Up(_Arrow, Scale))

            Case CursorType.NS
                Dim BB, BL As Brush
                If [BackColorGradient] Then
                    BB = New LinearGradientBrush(_Arrow, [BackColor1], [BackColor2], [BackColorGradientMode])
                Else
                    BB = New SolidBrush([BackColor1])
                End If
                If [LineColorGradientMode] Then
                    BL = New LinearGradientBrush(_Arrow, [LineColor1], [LineColor2], [LineColorGradientMode])
                Else
                    BL = New SolidBrush([LineColor1])
                End If
                Dim PL As New Pen(BL, [LineThickness])

                G.FillPath(BB, NS(_Arrow, Scale))
                G.DrawPath(PL, NS(_Arrow, Scale))

            Case CursorType.EW
                Dim BB, BL As Brush
                If [BackColorGradient] Then
                    BB = New LinearGradientBrush(_Arrow, [BackColor1], [BackColor2], [BackColorGradientMode])
                Else
                    BB = New SolidBrush([BackColor1])
                End If
                If [LineColorGradientMode] Then
                    BL = New LinearGradientBrush(_Arrow, [LineColor1], [LineColor2], [LineColorGradientMode])
                Else
                    BL = New SolidBrush([LineColor1])
                End If
                Dim PL As New Pen(BL, [LineThickness])

                G.FillPath(BB, EW(_Arrow, Scale))
                G.DrawPath(PL, EW(_Arrow, Scale))

            Case CursorType.NESW
                Dim BB, BL As Brush
                If [BackColorGradient] Then
                    BB = New LinearGradientBrush(_Arrow, [BackColor1], [BackColor2], [BackColorGradientMode])
                Else
                    BB = New SolidBrush([BackColor1])
                End If
                If [LineColorGradientMode] Then
                    BL = New LinearGradientBrush(_Arrow, [LineColor1], [LineColor2], [LineColorGradientMode])
                Else
                    BL = New SolidBrush([LineColor1])
                End If
                Dim PL As New Pen(BL, [LineThickness])

                G.FillPath(BB, NESW(_Arrow, Scale))
                G.DrawPath(PL, NESW(_Arrow, Scale))

            Case CursorType.NWSE
                Dim BB, BL As Brush
                If [BackColorGradient] Then
                    BB = New LinearGradientBrush(_Arrow, [BackColor1], [BackColor2], [BackColorGradientMode])
                Else
                    BB = New SolidBrush([BackColor1])
                End If
                If [LineColorGradientMode] Then
                    BL = New LinearGradientBrush(_Arrow, [LineColor1], [LineColor2], [LineColorGradientMode])
                Else
                    BL = New SolidBrush([LineColor1])
                End If
                Dim PL As New Pen(BL, [LineThickness])

                G.FillPath(BB, NWSE(_Arrow, Scale))
                G.DrawPath(PL, NWSE(_Arrow, Scale))

            Case CursorType.Pen
                Dim BB, BL As Brush
                If [BackColorGradient] Then
                    BB = New LinearGradientBrush(_Arrow, [BackColor1], [BackColor2], [BackColorGradientMode])
                Else
                    BB = New SolidBrush([BackColor1])
                End If
                If [LineColorGradientMode] Then
                    BL = New LinearGradientBrush(_Arrow, [LineColor1], [LineColor2], [LineColorGradientMode])
                Else
                    BL = New SolidBrush([LineColor1])
                End If
                Dim PL As New Pen(BL, [LineThickness])

                G.FillPath(BB, PenBackground(_Arrow, Scale))
                G.DrawPath(PL, Pen(_Arrow, Scale))

            Case CursorType.Link
                Dim BB, BL As Brush
                If [BackColorGradient] Then
                    BB = New LinearGradientBrush(_Arrow, [BackColor1], [BackColor2], [BackColorGradientMode])
                Else
                    BB = New SolidBrush([BackColor1])
                End If
                If [LineColorGradientMode] Then
                    BL = New LinearGradientBrush(_Arrow, [LineColor1], [LineColor2], [LineColorGradientMode])
                Else
                    BL = New SolidBrush([LineColor1])
                End If
                Dim PL As New Pen(BL, [LineThickness])

                G.FillPath(BB, Hand(_Arrow, Scale))
                G.DrawPath(PL, Hand(_Arrow, Scale))

            Case CursorType.Pin
                Dim BB, BL As Brush
                If [BackColorGradient] Then
                    BB = New LinearGradientBrush(_Arrow, [BackColor1], [BackColor2], [BackColorGradientMode])
                Else
                    BB = New SolidBrush([BackColor1])
                End If
                If [LineColorGradientMode] Then
                    BL = New LinearGradientBrush(_Arrow, [LineColor1], [LineColor2], [LineColorGradientMode])
                Else
                    BL = New SolidBrush([LineColor1])
                End If
                Dim PL As New Pen(BL, [LineThickness])

                Dim BB_P, BL_P As Brush
                If [BackColorGradient] Then
                    BB_P = New LinearGradientBrush(_Pin, [BackColor1], [BackColor2], [BackColorGradientMode])
                Else
                    BB_P = New SolidBrush([BackColor1])
                End If
                If [LineColorGradientMode] Then
                    BL_P = New LinearGradientBrush(_Pin, [LineColor1], [LineColor2], [LineColorGradientMode])
                Else
                    BL_P = New SolidBrush([LineColor1])
                End If

                G.FillPath(BB, Hand(_Arrow, Scale))
                G.DrawPath(PL, Hand(_Arrow, Scale))
                G.FillPath(BB_P, Pin(_Pin, Scale))
                G.FillPath(BL_P, Pin_CenterPoint(_Pin, Scale))
                G.DrawPath(New Pen(BL_P, 2), Pin(_Pin, Scale))

            Case CursorType.Person
                Dim BB, BL As Brush
                If [BackColorGradient] Then
                    BB = New LinearGradientBrush(_Arrow, [BackColor1], [BackColor2], [BackColorGradientMode])
                Else
                    BB = New SolidBrush([BackColor1])
                End If
                If [LineColorGradientMode] Then
                    BL = New LinearGradientBrush(_Arrow, [LineColor1], [LineColor2], [LineColorGradientMode])
                Else
                    BL = New SolidBrush([LineColor1])
                End If
                Dim PL As New Pen(BL, [LineThickness])

                Dim BB_P, BL_P As Brush
                If [BackColorGradient] Then
                    BB_P = New LinearGradientBrush(_Person, [BackColor1], [BackColor2], [BackColorGradientMode])
                Else
                    BB_P = New SolidBrush([BackColor1])
                End If
                If [LineColorGradientMode] Then
                    BL_P = New LinearGradientBrush(_Person, [LineColor1], [LineColor2], [LineColorGradientMode])
                Else
                    BL_P = New SolidBrush([LineColor1])
                End If

                G.FillPath(BB, Hand(_Arrow, Scale))
                G.DrawPath(PL, Hand(_Arrow, Scale))
                G.FillPath(BB_P, Person(_Person, Scale))
                G.DrawPath(New Pen(BL_P, 2), Person(_Person, Scale))

            Case CursorType.IBeam
                Dim BB, BL As Brush
                If [BackColorGradient] Then
                    BB = New LinearGradientBrush(_Arrow, [BackColor1], [BackColor2], [BackColorGradientMode])
                Else
                    BB = New SolidBrush([BackColor1])
                End If
                If [LineColorGradientMode] Then
                    BL = New LinearGradientBrush(_Arrow, [LineColor1], [LineColor2], [LineColorGradientMode])
                Else
                    BL = New SolidBrush([LineColor1])
                End If
                Dim PL As New Pen(BL, [LineThickness])

                G.FillPath(BB, IBeam(_Arrow, Scale))
                G.DrawPath(PL, IBeam(_Arrow, Scale))

            Case CursorType.Cross
                Dim BB, BL As Brush
                If [BackColorGradient] Then
                    BB = New LinearGradientBrush(_Arrow, [BackColor1], [BackColor2], [BackColorGradientMode])
                Else
                    BB = New SolidBrush([BackColor1])
                End If
                If [LineColorGradientMode] Then
                    BL = New LinearGradientBrush(_Arrow, [LineColor1], [LineColor2], [LineColorGradientMode])
                Else
                    BL = New SolidBrush([LineColor1])
                End If
                Dim PL As New Pen(BL, [LineThickness])

                G.FillPath(BB, Cross(_Arrow, Scale))
                G.DrawPath(PL, Cross(_Arrow, Scale))

        End Select


        G.Flush()
        G.Save()
        Return b
        b.Dispose()
        G.Dispose()
    End Function

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
        Dim arcLength = 70
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
        Dim arcLength = 70
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

    Public Function Help([Rectangle] As Rectangle, Optional Scale As Single = 1) As GraphicsPath
        Dim path As New GraphicsPath
        [Rectangle].Width = 7
        [Rectangle].Height = 11
        [Rectangle].X = 11
        [Rectangle].Y = 6

        path.AddString("?", New FontFamily("Segoe UI Black"), FontStyle.Bold, 15, [Rectangle], StringFormat.GenericDefault)

        Dim m As Matrix = New Matrix()
        m.Scale(Scale, Scale, MatrixOrder.Append)
        m.Translate(1, 1, MatrixOrder.Append)
        path.Transform(m)

        Return path
    End Function

    Public Function None([Rectangle] As Rectangle, Optional Scale As Single = 1) As GraphicsPath
        Dim path As New GraphicsPath
        [Rectangle].Width = 17
        [Rectangle].Height = 17
        [Rectangle].X = 0
        [Rectangle].Y = 0

        Dim R As New Rectangle([Rectangle].X + 2, [Rectangle].Y + 2, [Rectangle].Width - 4, [Rectangle].Height - 4)

        path.AddArc(R, 50, 160)
        path.CloseFigure()

        path.AddArc(R, 230, 160)
        path.CloseFigure()

        path.AddEllipse([Rectangle])

        Dim m As Matrix = New Matrix()
        m.Scale(Scale, Scale, MatrixOrder.Append)
        m.Translate(1, 1, MatrixOrder.Append)
        path.Transform(m)

        Return path
    End Function

    Public Function NoneBackground([Rectangle] As Rectangle, Optional Scale As Single = 1) As GraphicsPath
        Dim path As New GraphicsPath
        [Rectangle].Width = 17
        [Rectangle].Height = 17
        [Rectangle].X = 0
        [Rectangle].Y = 0

        path.AddEllipse([Rectangle])

        Dim m As Matrix = New Matrix()
        m.Scale(Scale, Scale, MatrixOrder.Append)
        m.Translate(1, 1, MatrixOrder.Append)
        path.Transform(m)

        Return path
    End Function

    Public Function Up([Rectangle] As Rectangle, Optional Scale As Single = 1) As GraphicsPath
        Dim path As New GraphicsPath
        [Rectangle].Width = 9
        [Rectangle].Height = 19
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

        Dim D1 As Point = New Point(MUL2.X, MUL2.Y + 1)
        Dim D2 As Point = New Point(D1.X + 1, D1.Y)
        path.AddLine(D1, D2)

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
        [Rectangle].X = 0
        [Rectangle].Y = 0

        Dim UR1 As Point = New Point([Rectangle].X + [Rectangle].Width - 1, [Rectangle].Y)
        Dim UR2 As Point = New Point(UR1.X - 6, UR1.Y)
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
        Dim L2 As Point = New Point(L1.X, L1.Y + 6)
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
        Dim R2 As Point = New Point(R1.X, R1.Y - 6)
        path.AddLine(R1, R2)

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
        [Rectangle].X = 0
        [Rectangle].Y = 0

        Dim flipXMatrix As Matrix = New Matrix(-1, 0, 0, 1, [Rectangle].Width, -1)
        Dim transformMatrix As Matrix = New Matrix()
        transformMatrix.Multiply(flipXMatrix)
        path.Transform(transformMatrix)

        Dim m As Matrix = New Matrix()
        m.Scale(Scale, Scale, MatrixOrder.Append)
        m.Translate(1, 1, MatrixOrder.Append)
        path.Transform(m)

        Return path
    End Function

    Public Function EW([Rectangle] As Rectangle, Optional Scale As Single = 1) As GraphicsPath
        Dim path As New GraphicsPath
        [Rectangle].Width = 23
        [Rectangle].Height = 9
        [Rectangle].X = 0
        [Rectangle].Y = 0

        Dim L1 As Point = New Point([Rectangle].X, [Rectangle].Y + 4)
        Dim L2 As Point = New Point(L1.X + 4, L1.Y - 4)
        path.AddLine(L1, L2)

        Dim LX1 As Point = New Point(L2.X + 1, L2.Y)
        Dim LX2 As Point = New Point(LX1.X, LX1.Y + 2)
        path.AddLine(LX1, LX2)

        Dim U1 As Point = New Point(LX2.X, LX2.Y + 1)
        Dim U2 As Point = New Point(U1.X + 12, U1.Y)
        path.AddLine(U1, U2)

        Dim RX1 As Point = New Point(U2.X, U2.Y - 1)
        Dim RX2 As Point = New Point(RX1.X, RX1.Y - 2)
        path.AddLine(RX1, RX2)

        Dim R1 As Point = New Point(RX2.X + 1, RX2.Y)
        Dim R2 As Point = New Point(R1.X + 4, R1.Y + 4)
        path.AddLine(R1, R2)

        Dim R3 As Point = New Point(R2.X, R2.Y)
        Dim R4 As Point = New Point(R3.X - 4, R3.Y + 4)
        path.AddLine(R3, R4)

        Dim RX3 As Point = New Point(R4.X - 1, R4.Y)
        Dim RX4 As Point = New Point(RX3.X, RX3.Y - 2)
        path.AddLine(RX3, RX4)

        Dim D1 As Point = New Point(RX4.X, RX4.Y - 1)
        Dim D2 As Point = New Point(D1.X - 12, D1.Y)
        path.AddLine(D1, D2)

        Dim LX3 As Point = New Point(D2.X, D2.Y + 1)
        Dim LX4 As Point = New Point(LX3.X, LX3.Y + 2)
        path.AddLine(LX3, LX4)

        Dim L3 As Point = New Point(LX4.X - 1, LX4.Y)
        Dim L4 As Point = New Point(L3.X - 4, L3.Y - 4)
        path.AddLine(L3, L4)

        Dim m As Matrix = New Matrix()
        m.Scale(Scale, Scale, MatrixOrder.Append)
        m.Translate(1, 1, MatrixOrder.Append)
        path.Transform(m)

        Return path
    End Function

    Public Function Pen([Rectangle] As Rectangle, Optional Scale As Single = 1) As GraphicsPath
        Dim path As New GraphicsPath
        [Rectangle].Width = 22
        [Rectangle].Height = 22
        [Rectangle].X = 0
        [Rectangle].Y = 0

        Dim T1 As New Point([Rectangle].X, [Rectangle].Y)
        Dim T2 As Point = T1 + New Point(6, 2)
        path.AddLine(T1, T2)

        Dim R1 As New Point(T2.X, T2.Y)
        Dim R2 As New Point(R1.X + 15, R1.Y + 15)
        path.AddLine(R1, R2)

        Dim B1 As New Point(R2.X, R2.Y + 1)
        Dim B2 As New Point(B1.X - 3, B1.Y + 3)
        path.AddLine(B1, B2)

        Dim L1 As New Point(B2.X - 1, B2.Y)
        Dim L2 As New Point(L1.X - 15, L1.Y - 15)
        path.AddLine(L1, L2)

        Dim LX1 As New Point(L2.X, L2.Y)
        path.AddLine(LX1, T1)

        path.CloseFigure()

        Dim S1 As New Point([Rectangle].X + 14, [Rectangle].Y + 18)
        Dim S2 As New Point(S1.X + 4, S1.Y - 4)
        path.AddLine(S2, S1)

        Dim m As Matrix = New Matrix()
        m.Scale(Scale, Scale, MatrixOrder.Append)
        m.Translate(1, 1, MatrixOrder.Append)
        path.Transform(m)

        Return path
    End Function

    Public Function PenBackground([Rectangle] As Rectangle, Optional Scale As Single = 1) As GraphicsPath
        Dim path As New GraphicsPath
        [Rectangle].Width = 22
        [Rectangle].Height = 22
        [Rectangle].X = 0
        [Rectangle].Y = 0

        Dim T1 As New Point([Rectangle].X, [Rectangle].Y)
        Dim T2 As Point = T1 + New Point(6, 2)
        path.AddLine(T1, T2)

        Dim R1 As New Point(T2.X, T2.Y)
        Dim R2 As New Point(R1.X + 15, R1.Y + 15)
        path.AddLine(R1, R2)

        Dim B1 As New Point(R2.X, R2.Y + 1)
        Dim B2 As New Point(B1.X - 3, B1.Y + 3)
        path.AddLine(B1, B2)

        Dim L1 As New Point(B2.X - 1, B2.Y)
        Dim L2 As New Point(L1.X - 15, L1.Y - 15)
        path.AddLine(L1, L2)

        Dim LX1 As New Point(L2.X, L2.Y)
        path.AddLine(LX1, T1)

        path.CloseFigure()


        Dim m As Matrix = New Matrix()
        m.Scale(Scale, Scale, MatrixOrder.Append)
        m.Translate(1, 1, MatrixOrder.Append)
        path.Transform(m)

        Return path
    End Function

    Public Function Hand([Rectangle] As Rectangle, Optional Scale As Single = 1) As GraphicsPath
        Dim path As New GraphicsPath
        [Rectangle].Width = 18
        [Rectangle].Height = 24
        [Rectangle].X = 0
        [Rectangle].Y = 0

        Dim Index_LB1 As New Point([Rectangle].X + 5, [Rectangle].Y + 14)
        Dim Index_LB2 As New Point(Index_LB1.X, Index_LB1.Y - 12)
        path.AddLine(Index_LB1, Index_LB2)

        Dim Index_RB1 As New Point(Index_LB1.X + 3, Index_LB1.Y - 4)
        Dim Index_RB2 As New Point(Index_RB1.X, Index_RB1.Y - 8)

        path.AddArc(Index_LB2.X, Index_LB2.Y - 2, 3, 2, 180, 180)

        path.AddLine(Index_RB1, Index_RB2)

        path.AddArc(Index_RB2.X, Index_LB2.Y + 3, 3, 2, 180, 180)

        Dim Middle_RB1 As New Point(Index_RB1.X + 3, Index_RB1.Y)
        Dim Middle_RB2 As New Point(Middle_RB1.X, Middle_RB1.Y - 3)
        path.AddLine(Middle_RB1, Middle_RB2)

        path.AddArc(Middle_RB2.X, Index_LB2.Y + 4, 3, 2, 180, 180)

        Dim Ring_RB1 As New Point(Middle_RB1.X + 3, Middle_RB1.Y)
        Dim Ring_RB2 As New Point(Ring_RB1.X, Ring_RB1.Y - 2)
        path.AddLine(Ring_RB1, Ring_RB2)

        path.AddArc(Ring_RB2.X, Index_LB2.Y + 5, 3, 2, 180, 180)

        Dim FreeBorder1 As New Point(Ring_RB1.X + 3, Ring_RB1.Y - 1)
        Dim FreeBorder2 As New Point(FreeBorder1.X, FreeBorder1.Y + 8)
        path.AddLine(FreeBorder1, FreeBorder2)

        Dim LW1 As Point = FreeBorder2 + New Point(0, 1)
        Dim RW1 As Point = New Point(LW1.X - 14, LW1.Y)
        Dim Btm As New Rectangle(RW1.X, RW1.Y - 8, 14, 13)
        path.AddArc(Btm, 0, 180)

        Dim L1 As Point = RW1 - New Point(0, 1)
        Dim L2 As New Point(L1.X - 2, L1.Y - 2)
        Dim Thumb As New Rectangle(L2.X - 1, L2.Y - 3, 2, 3)
        path.AddArc(Thumb, 90, 180)
        'path.AddRectangle(Thumb)

        Dim LastBorder1 As New Point(Thumb.X + Thumb.Width, Thumb.Y)
        Dim LastBorder2 As New Point(LastBorder1.X + 2, LastBorder1.Y + 1)
        path.AddLine(LastBorder1, LastBorder2)

        path.CloseFigure()

        Dim m As Matrix = New Matrix()
        m.Scale(Scale, Scale, MatrixOrder.Append)
        m.Translate(1, 1, MatrixOrder.Append)
        path.Transform(m)

        Return path
    End Function

    Public Function Pin([Rectangle] As Rectangle, Optional Scale As Single = 1) As GraphicsPath
        Dim path As New GraphicsPath
        [Rectangle].Width = 13
        [Rectangle].Height = 20
        [Rectangle].X = 15
        [Rectangle].Y = 11

        Dim U As New Rectangle([Rectangle].X, [Rectangle].Y, 12, 10)
        path.AddArc(U, 180, 180)

        Dim C As New Point([Rectangle].X + 6, [Rectangle].Y + 18)
        Dim p1 As New Point([Rectangle].X + 0, [Rectangle].Y + 6)
        Dim p2 As New Point([Rectangle].X + 12, [Rectangle].Y + 6)
        path.AddLine(p2, C)
        path.AddLine(C, p1)
        path.CloseFigure()

        Dim m As Matrix = New Matrix()
        m.Scale(Scale, Scale, MatrixOrder.Append)
        m.Translate(1, 1, MatrixOrder.Append)
        path.Transform(m)

        Return path
    End Function

    Public Function Person([Rectangle] As Rectangle, Optional Scale As Single = 1) As GraphicsPath
        Dim path As New GraphicsPath
        [Rectangle].Width = 10
        [Rectangle].Height = 13
        [Rectangle].X = 19
        [Rectangle].Y = 17

        Dim Face As New Rectangle([Rectangle].X, [Rectangle].Y, 5, 6)
        path.AddEllipse(Face)

        Dim TrunkUpper As New Rectangle(Face.X - 2, Face.Y + Face.Height, 9, 9)
        path.AddArc(TrunkUpper, 180, 180)

        Dim TrunkLower As New Rectangle(TrunkUpper.X, TrunkUpper.Y + 3, 9, 3)
        path.AddArc(TrunkLower, 0, 180)

        Dim m As Matrix = New Matrix()
        m.Scale(Scale, Scale, MatrixOrder.Append)
        m.Translate(1, 1, MatrixOrder.Append)
        path.Transform(m)

        Return path
    End Function

    Public Function IBeam([Rectangle] As Rectangle, Optional Scale As Single = 1) As GraphicsPath
        Dim path As New GraphicsPath

        [Rectangle].X = 0
        [Rectangle].Y = 0

        Dim L1 As New Point([Rectangle].X, [Rectangle].Y)
        Dim L2 As New Point(L1.X, L1.Y + 2)
        path.AddLine(L1, L2)

        Dim BU1 As New Point(L2.X + 3, L2.Y)
        path.AddLine(L2, BU1)

        Dim LX As New Point(BU1.X, BU1.Y + 13)
        path.AddLine(BU1, LX)

        Dim BU2 As New Point(LX.X - 3, LX.Y)
        path.AddLine(LX, BU2)

        Dim L3 As New Point(BU2.X, BU2.Y + 2)
        path.AddLine(BU2, L3)

        Dim Bl As New Point(L3.X + 3, L3.Y)
        path.AddLine(L3, Bl)

        Dim XB As New Point(Bl.X + 1, Bl.Y - 1)
        path.AddLine(Bl, XB)

        Dim Br As New Point(XB.X + 1, XB.Y + 1)
        path.AddLine(XB, Br)

        Dim RB As New Point(Br.X + 3, Br.Y)
        path.AddLine(Br, RB)

        Dim R1 As New Point(RB.X, RB.Y - 2)
        path.AddLine(RB, R1)

        Dim BU3 As New Point(R1.X - 3, R1.Y)
        path.AddLine(R1, BU3)

        Dim RX As New Point(BU3.X, BU3.Y - 13)
        path.AddLine(BU3, RX)

        Dim TU As New Point(RX.X + 3, RX.Y)
        path.AddLine(RX, TU)

        Dim RR As New Point(TU.X, TU.Y - 2)
        path.AddLine(TU, RR)

        Dim T As New Point(RR.X - 3, RR.Y)
        path.AddLine(RR, T)

        Dim Tx As New Point(T.X - 1, T.Y + 1)
        path.AddLine(T, Tx)

        Dim TXL As New Point(Tx.X - 1, Tx.Y - 1)
        path.AddLine(Tx, TXL)

        Dim TL As New Point(TXL.X - 3, TXL.Y)
        path.AddLine(TXL, TL)


        Dim m As Matrix = New Matrix()
        m.Scale(Scale, Scale, MatrixOrder.Append)
        m.Translate(1, 1, MatrixOrder.Append)
        path.Transform(m)

        Return path
    End Function

    Public Function Cross([Rectangle] As Rectangle, Optional Scale As Single = 1) As GraphicsPath
        Dim path As New GraphicsPath
        [Rectangle].Width = 19
        [Rectangle].Height = 19

        Dim L1 As New Point(9, 0)
        Dim L2 As New Point(L1.X - 1, L1.Y)
        path.AddLine(L1, L2)

        Dim L3 As New Point(L2.X, L2.Y + 8)
        path.AddLine(L2, L3)

        Dim L4 As New Point(L3.X - 8, L3.Y)
        path.AddLine(L3, L4)

        Dim L5 As New Point(L4.X, L4.Y + 2)
        path.AddLine(L4, L5)

        Dim L6 As New Point(L5.X + 8, L5.Y)
        path.AddLine(L5, L6)

        Dim L7 As New Point(L6.X, L6.Y + 8)
        path.AddLine(L6, L7)

        Dim L8 As New Point(L7.X + 1, L7.Y)
        path.AddLine(L7, L8)

        path.AddPath(MirrorRight(path), False)

        Dim m As Matrix = New Matrix()
        m.Scale(Scale, Scale, MatrixOrder.Append)
        m.Translate(1, 1, MatrixOrder.Append)
        path.Transform(m)

        Return path
    End Function

    Public Function Pin_CenterPoint([Rectangle] As Rectangle, Optional Scale As Single = 1) As GraphicsPath
        Dim path As New GraphicsPath
        [Rectangle].Width = 13
        [Rectangle].Height = 20
        [Rectangle].X = 15
        [Rectangle].Y = 11

        Dim o As New Rectangle([Rectangle].X, [Rectangle].Y, 12, 12)
        Dim o1 As New Rectangle([Rectangle].X, [Rectangle].Y, 6, 6)
        o1.X = [Rectangle].X + (o.Width - o1.Width) / 2
        o1.Y = [Rectangle].Y + (o.Height - o1.Height) / 2
        path.AddEllipse(o1)

        Dim m As Matrix = New Matrix()
        m.Scale(Scale, Scale, MatrixOrder.Append)
        m.Translate(1, 1, MatrixOrder.Append)
        path.Transform(m)

        Return path
    End Function

    Private Function MirrorRight(ByVal path As GraphicsPath) As GraphicsPath
        Dim r = path.GetBounds()
        Dim p = CType(path.Clone(), GraphicsPath)
        p.Transform(New Matrix(-1, 0, 0, 1, 2 * (r.Left + r.Width), 0))
        Return p
    End Function

End Module


Public Class CursorControl : Inherits ContainerControl
    Sub New()

    End Sub

    Public Property Prop_Cursor As Paths.CursorType = CursorType.Arrow
    Public Property Prop_BackColor1 As Color = Color.White
    Public Property Prop_BackColor2 As Color = Color.White
    Public Property Prop_BackColorGradient As Boolean = False
    Public Property Prop_BackColorGradientMode As LinearGradientMode = LinearGradientMode.Vertical

    Public Property Prop_LineColor1 As Color = Color.FromArgb(64, 65, 75)
    Public Property Prop_LineColor2 As Color = Color.FromArgb(64, 65, 75)
    Public Property Prop_LineColorGradient As Boolean = False
    Public Property Prop_LineColorGradientMode As LinearGradientMode = LinearGradientMode.Vertical
    Public Property Prop_LineThickness As Single = 1


    Public Property Prop_LoadingCircleBack1 As Color = Color.FromArgb(42, 151, 243)
    Public Property Prop_LoadingCircleBack2 As Color = Color.FromArgb(42, 151, 243)
    Public Property Prop_LoadingCircleBackGradient As Boolean = False
    Public Property Prop_LoadingCircleBackGradientMode As LinearGradientMode = LinearGradientMode.Vertical

    Public Property Prop_LoadingCircleHot1 As Color = Color.FromArgb(37, 204, 255)
    Public Property Prop_LoadingCircleHot2 As Color = Color.FromArgb(37, 204, 255)
    Public Property Prop_LoadingCircleHotGradient As Boolean = False
    Public Property Prop_LoadingCircleHotGradientMode As LinearGradientMode = LinearGradientMode.Vertical


    Public Property Prop_Scale As Single = 1

    Dim ColorPalette As New XenonColorPalette(Me)

    Private _Shown As Boolean = False

    Dim _Focused As Boolean = False

    Dim bmp As Bitmap

    Private Sub XenonRadioButton_HandleCreated(sender As Object, e As EventArgs) Handles Me.HandleCreated

        Try
            If Not DesignMode Then
                AddHandler FindForm.Load, AddressOf Loaded
                AddHandler FindForm.Shown, AddressOf Showed
                AddHandler Parent.BackColorChanged, AddressOf RefreshColorPalette
            End If
        Catch
        End Try

    End Sub

    Sub Loaded()
        _Shown = False
    End Sub

    Sub Showed()
        _Shown = True
        ColorPalette = New XenonColorPalette(Me)
        Invalidate()
    End Sub

    Public Sub RefreshColorPalette()
        If _Shown Then
            ColorPalette = New XenonColorPalette(Me)
            Invalidate()
        End If
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)

        bmp = New Bitmap(32 * Prop_Scale, 32 * Prop_Scale, PixelFormat.Format32bppPArgb)

        bmp = Draw(Prop_Cursor,
                   Prop_BackColor1, Prop_BackColor2, Prop_BackColorGradient, Prop_BackColorGradientMode,
                   Prop_LineColor1, Prop_LineColor2, Prop_LineColorGradient, Prop_LineColorGradientMode,
                   Prop_LoadingCircleBack1, Prop_LoadingCircleBack2, Prop_LoadingCircleBackGradient, Prop_LoadingCircleBackGradientMode,
                   Prop_LoadingCircleHot1, Prop_LoadingCircleHot2, Prop_LoadingCircleHotGradient, Prop_LoadingCircleHotGradientMode,
                   Prop_LineThickness, Prop_Scale)

        DoubleBuffered = True

        Dim MainRect As New Rectangle(0, 0, Width - 1, Height - 1)

        Dim CenterRect As New Rectangle(MainRect.X + (MainRect.Width - bmp.Width) / 2,
                                        MainRect.Y + (MainRect.Height - bmp.Height) / 2,
                                         bmp.Width, bmp.Height)

        e.Graphics.Clear(GetParentColor(Me))
        FillRect(e.Graphics, New SolidBrush(If(_Focused, ColorPalette.Color_Back_Checked, ColorPalette.Color_Back)), MainRect)
        DrawRect_LikeW11(e.Graphics, If(_Focused, ColorPalette.Color_Border_Checked_Hover, ColorPalette.Color_Border), MainRect)
        e.Graphics.DrawImage(bmp, CenterRect)

    End Sub

    Private Sub CursorControl_Click(sender As Object, e As EventArgs) Handles Me.Click

        For Each c As CursorControl In Parent.Controls.OfType(Of CursorControl)
            If c Is sender Then
                c._Focused = True
                c.Invalidate()
            Else
                c._Focused = False
                c.Invalidate()
            End If
        Next

    End Sub
End Class
