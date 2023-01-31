Imports System.ComponentModel
Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.Drawing.Text
Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices
Imports WinPaletter.XenonCore

Module XenonModule
    Sub New()
        TextBitmap = New Bitmap(1, 1)
        TextGraphics = Graphics.FromImage(TextBitmap)
    End Sub

    Private ReadOnly TextBitmap As Bitmap
    Private ReadOnly TextGraphics As Graphics

    ''' <summary>
    ''' Class represents colors for WinPaletter Controls (Styles)
    ''' </summary>
    Public Style As New XenonStyle(DefaultAccent, DefaultBackColorDark)

    <Extension()>
    Public Sub DrawGlowString(ByVal G As Graphics, ByVal GlowSize As Integer, Text As String, ByVal Ctrl As Control, ByVal [ForeColor] As Color, ByVal GlowColor As Color, ByVal Rect As Rectangle, ByVal FormatX As StringFormat)
        Dim bm As Bitmap = New Bitmap(CInt(Ctrl.Width / 5), CInt(Ctrl.Height / 5))
        Dim g2 As Graphics = Graphics.FromImage(bm)
        Dim pth As GraphicsPath = New GraphicsPath()
        pth.AddString(Text, Ctrl.Font.FontFamily, Ctrl.Font.Style, Ctrl.Font.SizeInPoints + 3, Rect, FormatX)
        Dim mx As Matrix = New Matrix(1.0F / 5, 0, 0, 1.0F / 5, -(1.0F / 5), -(1.0F / 5))
        g2.SmoothingMode = SmoothingMode.AntiAlias
        g2.Transform = mx
        Dim p As Pen = New Pen(GlowColor, GlowSize)
        g2.DrawPath(p, pth)
        g2.FillPath(New SolidBrush(GlowColor), pth)
        G.InterpolationMode = InterpolationMode.HighQualityBicubic
        G.DrawImage(bm, Ctrl.ClientRectangle, 0, 0, bm.Width, bm.Height, GraphicsUnit.Pixel)
        'G.FillPath(New SolidBrush([ForeColor]), pth)
        G.DrawString(Text, Ctrl.Font, New SolidBrush([ForeColor]), Rect, FormatX)
        g2.Dispose()
        pth.Dispose()
    End Sub


    Public Function StringAligner(ByVal goTextAlign As ContentAlignment, Optional RTL As Boolean = False) As StringFormat
        Dim oStringFormat As New StringFormat()
        Select Case goTextAlign
            Case ContentAlignment.TopLeft
                oStringFormat.LineAlignment = StringAlignment.Near
                oStringFormat.Alignment = StringAlignment.Near
            Case ContentAlignment.TopCenter
                oStringFormat.LineAlignment = StringAlignment.Near
                oStringFormat.Alignment = StringAlignment.Center
            Case ContentAlignment.TopRight
                oStringFormat.LineAlignment = StringAlignment.Near
                oStringFormat.Alignment = StringAlignment.Far
            Case ContentAlignment.MiddleLeft
                oStringFormat.LineAlignment = StringAlignment.Center
                oStringFormat.Alignment = StringAlignment.Near
            Case ContentAlignment.MiddleCenter
                oStringFormat.LineAlignment = StringAlignment.Center
                oStringFormat.Alignment = StringAlignment.Center
            Case ContentAlignment.MiddleRight
                oStringFormat.LineAlignment = StringAlignment.Center
                oStringFormat.Alignment = StringAlignment.Far
            Case ContentAlignment.BottomLeft
                oStringFormat.LineAlignment = StringAlignment.Far
                oStringFormat.Alignment = StringAlignment.Near
            Case ContentAlignment.BottomCenter
                oStringFormat.LineAlignment = StringAlignment.Far
                oStringFormat.Alignment = StringAlignment.Center
            Case ContentAlignment.BottomRight
                oStringFormat.LineAlignment = StringAlignment.Far
                oStringFormat.Alignment = StringAlignment.Far
        End Select

        If RTL Then oStringFormat.FormatFlags = StringFormatFlags.DirectionRightToLeft

        Return oStringFormat
    End Function

    <Extension()>
    Public Sub DrawGlow(G As Graphics, R As Rectangle, ByVal GlowColor As Color, Optional ByVal GlowSize As Integer = 5, Optional ByVal GlowFade As Integer = 7)
        Try
            If GlowSize <= 0 Then GlowSize = 1
            If GlowFade <= 0 Then GlowFade = 1

            Dim Rect As Rectangle
            With R
                Rect = New Rectangle(.X - GlowSize - 2, .Y - GlowSize - 2, .Width + (GlowSize * 2) + 3, .Height + (GlowSize * 2) + 3)
            End With

            Dim bm As New Bitmap(CInt(Rect.Width / GlowFade), CInt(Rect.Height / GlowFade))
            Dim G2 As Graphics = Graphics.FromImage(bm)
            Dim Rect2 As New Rectangle(1, 1, bm.Width, bm.Height)

            G2.FillRectangle(New SolidBrush(GlowColor), Rect2)

            G.DrawImage(bm, Rect)
            G2.Dispose()
        Catch
        End Try
    End Sub

    <Extension()>
    Friend Function GetParentColor(ByVal ctrl As Control, Optional ByVal Accept_Transparent As Boolean = False) As Color

        If Accept_Transparent Then
            Return ctrl.Parent.BackColor
        Else
            Try

                If ctrl.Parent Is Nothing Then
                    Exit Function
                End If

                If ctrl.Parent.BackColor.A = 255 Then
                    Return Color.FromArgb(255, ctrl.Parent.BackColor)
                Else
                    Try
                        Dim c1 As Color = ctrl.Parent.BackColor
                        Dim c2 As Color
                        If TypeOf ctrl.Parent.Parent IsNot Form Then
                            c2 = ctrl.Parent.Parent.BackColor
                        Else
                            c2 = ctrl.Parent.FindForm.BackColor
                        End If
                        Dim amount As Double = c1.A / 255
                        Dim r As Byte = CByte(((c1.R * amount) + c2.R * (1 - amount)))
                        Dim g As Byte = CByte(((c1.G * amount) + c2.G * (1 - amount)))
                        Dim b As Byte = CByte(((c1.B * amount) + c2.B * (1 - amount)))
                        Return Color.FromArgb(r, g, b)
                    Catch
                        Return ctrl.Parent.BackColor
                    End Try
                End If
            Catch
            End Try
        End If

    End Function
    Friend Function GetRoundedCorners() As Boolean
        Try
            If My.Settings.Appearance_Custom Then
                Return My.Settings.Appearance_Rounded
            Else
                If System.ComponentModel.LicenseManager.UsageMode = System.ComponentModel.LicenseUsageMode.Designtime Then
                    Return False
                Else
                    If My.W11 Or My.W7 Then
                        Return True
                    ElseIf My.W10 Or My.W8 Then
                        Return False
                    Else
                        Return False
                    End If
                End If
            End If
        Catch
            Return False
        End Try
    End Function

    <Extension()>
    Public Sub DrawAeroEffect(G As Graphics, Rect As Rectangle, BackgroundBlurred As Bitmap, Color1 As Color, ColorBalance As Single, Color2 As Color, GlowBalance As Single, alpha As Single,
                       Radius As Integer, RoundedCorners As Boolean)


        If RoundedCorners Then
            G.FillRoundedImg(BackgroundBlurred, Rect, Radius, True)

            G.FillRoundedRect(New SolidBrush(Color.FromArgb(alpha * 255, Color.Black)), Rect, Radius, True)

            G.FillRoundedRect(New SolidBrush(Color.FromArgb(alpha * (ColorBalance * 255), Color1)), Rect, Radius, True)

            Dim C1 As Color = Color.FromArgb(ColorBalance * 255, Color1)
            Dim C2 As Color = Color.FromArgb(GlowBalance * 255, Color2)

            'Dim bk1 As Bitmap = FadeBitmap(ColorTint(BackgroundBlurred, C1), ColorBalance * (1 - alpha))
            'bk1 = FadeBitmap(bk1, 0.5)
            'G.FillRoundedImg( bk1, Rect, Radius, True)

            'Dim bk2 As Bitmap = FadeBitmap(Gray-scale(BackgroundBlurred), alpha * GlowBalance)
            'bk2 = FadeBitmap(bk2, (1 - alpha) * 0.5)
            'G.FillRoundedImg( bk2, Rect, Radius, True)

            G.FillRoundedRect(New SolidBrush(Color.FromArgb(alpha * (GlowBalance * 100), Color2)), Rect, Radius, True)
            G.FillRoundedRect(New SolidBrush(Color.FromArgb(alpha * (GlowBalance * 150), C1.Blend(C2, 100))), Rect, Radius, True)
        Else
            G.DrawImage(BackgroundBlurred, Rect)

            G.FillRectangle(New SolidBrush(Color.FromArgb(alpha * 255, Color.Black)), Rect)

            G.FillRectangle(New SolidBrush(Color.FromArgb(alpha * (ColorBalance * 255), Color1)), Rect)

            Dim C1 As Color = Color.FromArgb(ColorBalance * 255, Color1)
            Dim C2 As Color = Color.FromArgb(GlowBalance * 255, Color2)

            'Dim bk1 As Bitmap = FadeBitmap(ColorTint(BackgroundBlurred, C1), ColorBalance * (1 - alpha))
            'bk1 = FadeBitmap(bk1, 0.5)
            'G.DrawImage(bk1, Rect)

            'Dim bk2 As Bitmap = FadeBitmap(Gray-scale(BackgroundBlurred), alpha * GlowBalance)
            'bk2 = FadeBitmap(bk2, (1 - alpha) * 0.5)
            'G.DrawImage(bk2, Rect)

            G.FillRectangle(New SolidBrush(Color.FromArgb(alpha * (GlowBalance * 100), Color2)), Rect)
            G.FillRectangle(New SolidBrush(Color.FromArgb(alpha * (GlowBalance * 150), C1.Blend(C2, 100))), Rect)
        End If



    End Sub

#Region "Rounded Rectangle System"

    <Extension()>
    Public Sub FillRoundedRect(ByVal [Graphics] As Graphics, ByVal [Brush] As Brush, ByVal [Rectangle] As Rectangle, Optional ByVal [Radius] As Integer = -1, Optional ByVal ForcedRoundCorner As Boolean = False)
        Try
            If [Radius] = -1 Then [Radius] = 5

            If Graphics Is Nothing Then Throw New ArgumentNullException("graphics")
            [Graphics].SmoothingMode = SmoothingMode.AntiAlias

            If (GetRoundedCorners() Or ForcedRoundCorner) And [Radius] > 0 Then
                Using path As GraphicsPath = Rectangle.Round(Radius)
                    Graphics.FillPath(Brush, path)
                End Using
            Else
                Graphics.FillRectangle(Brush, [Rectangle])
            End If
        Catch
        End Try
    End Sub

    <Extension()>
    Public Sub FillRoundedImg(ByVal [Graphics] As Graphics, ByVal [Image] As Image, ByVal [Rectangle] As Rectangle, Optional ByVal [Radius] As Integer = -1, Optional ByVal ForcedRoundCorner As Boolean = False)
        Try
            If [Radius] = -1 Then [Radius] = 5

            If Graphics Is Nothing Then Throw New ArgumentNullException("graphics")

            If (GetRoundedCorners() Or ForcedRoundCorner) And [Radius] > 0 Then
                Using path As GraphicsPath = Rectangle.Round(Radius)
                    Dim reg As New Region(path)
                    [Graphics].Clip = reg
                    [Graphics].SmoothingMode = SmoothingMode.AntiAlias
                    [Graphics].DrawImage([Image], [Rectangle])
                    [Graphics].ResetClip()
                End Using
            Else
                Graphics.DrawImage([Image], [Rectangle])
            End If
        Catch
        End Try
    End Sub

    <Extension()>
    Public Function Round(ByVal r As Rectangle, ByVal radius As Integer) As GraphicsPath
        Try
            Dim path As New GraphicsPath()
            Dim d As Integer = radius * 2

            path.AddLine(r.Left + d, r.Top, r.Right - d, r.Top)
            path.AddArc(Rectangle.FromLTRB(r.Right - d, r.Top, r.Right, r.Top + d), -90, 90)

            path.AddLine(r.Right, r.Top + d, r.Right, r.Bottom - d)
            path.AddArc(Rectangle.FromLTRB(r.Right - d, r.Bottom - d, r.Right, r.Bottom), 0, 90)

            path.AddLine(r.Right - d, r.Bottom, r.Left + d, r.Bottom)
            path.AddArc(Rectangle.FromLTRB(r.Left, r.Bottom - d, r.Left + d, r.Bottom), 90, 90)

            path.AddLine(r.Left, r.Bottom - d, r.Left, r.Top + d)
            path.AddArc(Rectangle.FromLTRB(r.Left, r.Top, r.Left + d, r.Top + d), 180, 90)

            path.CloseFigure()
            Return path
        Catch
            Return Nothing
        End Try
    End Function

    <Extension()>
    Public Sub DrawRoundedRect(ByVal [Graphics] As Graphics, ByVal [Pen] As Pen, ByVal [Rectangle] As Rectangle, Optional ByVal [Radius_willbe_x2] As Integer = -1, Optional ByVal ForcedRoundCorner As Boolean = False)
        Try
            If [Radius_willbe_x2] = -1 Then [Radius_willbe_x2] = 5
            [Radius_willbe_x2] *= 2

            [Graphics].SmoothingMode = SmoothingMode.AntiAlias
            If (GetRoundedCorners() Or ForcedRoundCorner) And [Radius_willbe_x2] > 0 Then
                [Graphics].DrawArc([Pen], [Rectangle].X, [Rectangle].Y, [Radius_willbe_x2], [Radius_willbe_x2], 180, 90)
                [Graphics].DrawLine([Pen], CInt([Rectangle].X + [Radius_willbe_x2] / 2), [Rectangle].Y, CInt([Rectangle].X + [Rectangle].Width - [Radius_willbe_x2] / 2), [Rectangle].Y)
                [Graphics].DrawArc([Pen], [Rectangle].X + [Rectangle].Width - [Radius_willbe_x2], [Rectangle].Y, [Radius_willbe_x2], [Radius_willbe_x2], 270, 90)
                [Graphics].DrawLine([Pen], [Rectangle].X, CInt([Rectangle].Y + [Radius_willbe_x2] / 2), [Rectangle].X, CInt([Rectangle].Y + [Rectangle].Height - [Radius_willbe_x2] / 2))
                [Graphics].DrawLine([Pen], CInt([Rectangle].X + [Rectangle].Width), CInt([Rectangle].Y + [Radius_willbe_x2] / 2), CInt([Rectangle].X + [Rectangle].Width), CInt([Rectangle].Y + [Rectangle].Height - [Radius_willbe_x2] / 2))
                [Graphics].DrawLine([Pen], CInt([Rectangle].X + [Radius_willbe_x2] / 2), CInt([Rectangle].Y + [Rectangle].Height), CInt([Rectangle].X + [Rectangle].Width - [Radius_willbe_x2] / 2), CInt([Rectangle].Y + [Rectangle].Height))
                [Graphics].DrawArc([Pen], [Rectangle].X, [Rectangle].Y + [Rectangle].Height - [Radius_willbe_x2], [Radius_willbe_x2], [Radius_willbe_x2], 90, 90)
                [Graphics].DrawArc([Pen], [Rectangle].X + [Rectangle].Width - [Radius_willbe_x2], [Rectangle].Y + [Rectangle].Height - [Radius_willbe_x2], [Radius_willbe_x2], [Radius_willbe_x2], 0, 90)
            Else
                [Graphics].DrawRectangle([Pen], [Rectangle])
            End If
        Catch
        End Try
    End Sub

    <Extension()>
    Public Sub DrawRoundedRect_LikeW11(ByVal [Graphics] As Graphics, ByVal [PenX] As Pen, ByVal [Rectangle] As Rectangle, Optional ByVal [Radius] As Integer = -1, Optional ByVal ForcedRoundCorner As Boolean = False)
        Try
            Dim Dark As Boolean = GetDarkMode()

            If [Radius] = -1 Then [Radius] = 5
            [Radius] *= 2
            [Graphics].SmoothingMode = SmoothingMode.AntiAlias

            Dim [Pen] As New Pen([PenX].Color)
            Dim [Pen2] As New Pen([PenX].Color)
            Dim SidePen As New Pen([PenX].Color)

            If Dark Then
                [Pen].Color = [PenX].Color.CB(0.1)
                [Pen2].Color = [PenX].Color
            Else
                [Pen].Color = [PenX].Color.CB(-0.02)
                [Pen2].Color = [PenX].Color.CB(-0.05)
            End If

            Dim G As LinearGradientBrush
            Dim CColor As Color = [Pen2].Color.CB(If(Dark, 0.03, -0.05))

            If Dark Then
                G = New LinearGradientBrush([Rectangle], CColor, [Pen].Color, 180)
                Dim cblend As New ColorBlend(3) With {
                    .Colors = New Color(2) {CColor, [Pen].Color, CColor},
                    .Positions = New Single(2) {0F, 0.5F, 1.0F}
                }
                G.InterpolationColors = cblend

            Else
                G = New LinearGradientBrush([Rectangle], [Pen].Color, CColor, 180)
                Dim cblend As New ColorBlend(3) With {
                    .Colors = New Color(2) {[Pen].Color, CColor, [Pen].Color},
                    .Positions = New Single(2) {0F, 0.5F, 1.0F}
                }
                G.InterpolationColors = cblend

            End If

            Dim [PenG] As New Pen(G)

            If (GetRoundedCorners() Or ForcedRoundCorner) And [Radius] > 0 Then

                If Dark Then
                    [Graphics].DrawLine([Pen2], CInt([Rectangle].X + [Radius] / 2), CInt([Rectangle].Y + [Rectangle].Height), CInt([Rectangle].X + [Rectangle].Width - [Radius] / 2), CInt([Rectangle].Y + [Rectangle].Height))
                    [Graphics].DrawArc([Pen2], [Rectangle].X, [Rectangle].Y + [Rectangle].Height - [Radius], [Radius], [Radius], 90, 90)
                    [Graphics].DrawArc([Pen2], [Rectangle].X + [Rectangle].Width - [Radius], [Rectangle].Y + [Rectangle].Height - [Radius], [Radius], [Radius], 0, 90)

                    SidePen = [Pen2]

                    [Graphics].DrawLine(SidePen, [Rectangle].X, CInt([Rectangle].Y + [Radius] / 2), [Rectangle].X, CInt([Rectangle].Y + [Rectangle].Height - [Radius] / 2.5))
                    [Graphics].DrawLine(SidePen, [Rectangle].X + [Rectangle].Width, CInt([Rectangle].Y + [Radius] / 2), CInt([Rectangle].X + [Rectangle].Width), CInt([Rectangle].Y + [Rectangle].Height - [Radius] / 2.5))

                    [Graphics].DrawArc([PenG], [Rectangle].X, [Rectangle].Y, [Radius], [Radius], 180, 90)
                    [Graphics].DrawArc([PenG], [Rectangle].X + [Rectangle].Width - [Radius], [Rectangle].Y, [Radius], [Radius], 270, 90)
                    [Graphics].DrawLine([PenG], CInt([Rectangle].X + [Radius] / 2), [Rectangle].Y, CInt([Rectangle].X + [Rectangle].Width - [Radius] / 2), [Rectangle].Y)

                Else
                    [Graphics].DrawLine([PenG], CInt([Rectangle].X + [Radius] / 2), CInt([Rectangle].Y + [Rectangle].Height), CInt([Rectangle].X + [Rectangle].Width - [Radius] / 2), CInt([Rectangle].Y + [Rectangle].Height))
                    [Graphics].DrawArc([PenG], [Rectangle].X, [Rectangle].Y + [Rectangle].Height - [Radius], [Radius], [Radius], 90, 90)
                    [Graphics].DrawArc([PenG], [Rectangle].X + [Rectangle].Width - [Radius], [Rectangle].Y + [Rectangle].Height - [Radius], [Radius], [Radius], 0, 90)

                    SidePen = [Pen]

                    [Graphics].DrawLine(SidePen, [Rectangle].X, CInt([Rectangle].Y + [Radius] / 2), [Rectangle].X, CInt([Rectangle].Y + [Rectangle].Height - [Radius] / 2.5))
                    [Graphics].DrawLine(SidePen, [Rectangle].X + [Rectangle].Width, CInt([Rectangle].Y + [Radius] / 2), CInt([Rectangle].X + [Rectangle].Width), CInt([Rectangle].Y + [Rectangle].Height - [Radius] / 2.5))

                    [Graphics].DrawArc([Pen], [Rectangle].X, [Rectangle].Y, [Radius], [Radius], 180, 90)
                    [Graphics].DrawArc([Pen], [Rectangle].X + [Rectangle].Width - [Radius], [Rectangle].Y, [Radius], [Radius], 270, 90)
                    [Graphics].DrawLine([Pen], CInt([Rectangle].X + [Radius] / 2), [Rectangle].Y, CInt([Rectangle].X + [Rectangle].Width - [Radius] / 2), [Rectangle].Y)
                End If

            Else
                [Graphics].DrawRectangle([Pen], [Rectangle])
            End If
        Catch
        End Try

    End Sub
#End Region
End Module

#Region "Xenon UI"
Public Class XenonLinkLabel
    Inherits Windows.Forms.LinkLabel

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
Public Class TablessControl
    Inherits TabControl

    Public Sub New()
        SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
        SetStyle(ControlStyles.ResizeRedraw, True)
        Me.DoubleBuffered = True
    End Sub

    Protected Overrides Sub WndProc(ByRef m As Message)
        If m.Msg = &H1328 AndAlso Not DesignMode Then
            m.Result = CType(1, IntPtr)
        Else
            MyBase.WndProc(m)
        End If
    End Sub
End Class
Public Class XenonTabControl : Inherits TabControl
    Public Property LineColor As Color = Color.FromArgb(0, 81, 210)

    Sub New()
        SetStyle(ControlStyles.UserPaint Or ControlStyles.ResizeRedraw Or ControlStyles.AllPaintingInWmPaint Or ControlStyles.OptimizedDoubleBuffer Or ControlStyles.Opaque, True)
        ItemSize = New Size(40, 150)
        DrawMode = TabDrawMode.OwnerDrawFixed
        SizeMode = TabSizeMode.Fixed
        Font = New Font("Segoe UI", 9)
    End Sub

    Protected Overrides Sub CreateHandle()
        MyBase.CreateHandle()

        Dim X1 As Integer = ItemSize.Width
        Dim X2 As Integer = ItemSize.Height

        If Alignment = TabAlignment.Top Or Alignment = TabAlignment.Bottom Then
            If X1 >= X2 Then
                ItemSize = New Size(X1, X2)
            Else
                ItemSize = New Size(X2, X1)
            End If
        Else
            If X2 >= X1 Then
                ItemSize = New Size(X1, X2)
            Else
                ItemSize = New Size(X2, X1)
            End If
        End If
    End Sub

    ReadOnly Noise As New TextureBrush(My.Resources.GaussianBlur.Fade(0.4))
    Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
        Dim G As Graphics = e.Graphics
        G.SmoothingMode = SmoothingMode.AntiAlias
        G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit
        DoubleBuffered = True

        Dim SelectColor As Color
        Dim TextColor As Color
        Dim ParentColor As Color = GetParentColor
        Dim RTL As Boolean = (RightToLeft = 1)
        Dim img As Image = Nothing

        G.Clear(ParentColor)

        For i = 0 To TabCount - 1
            Dim TabRect As Rectangle = GetTabRect(i)

            Try
                If Me.ImageList IsNot Nothing Then
                    Dim ls As ImageList = ImageList
                    img = ls.Images.Item(i)
                    SelectColor = img.AverageColor
                Else
                    SelectColor = LineColor
                End If
            Catch
                SelectColor = LineColor
            End Try

            If Not GetDarkMode() Then SelectColor = SelectColor.LightLight

            If i = SelectedIndex Then
                G.FillRoundedRect(New SolidBrush(SelectColor), TabRect)
                G.FillRoundedRect(Noise, TabRect)
                TextColor = If(SelectColor.IsDark, Color.White, Color.Black)
            Else
                TextColor = If(ParentColor.IsDark, Color.White, Color.Black)
            End If

            Try
                If Not DesignMode Then TabPages.Item(i).BackColor = ParentColor
            Catch
            End Try

            Dim imgRect As Rectangle

            If img IsNot Nothing Then
                imgRect = New Rectangle(TabRect.X + 10, TabRect.Y + (TabRect.Height - img.Height) / 2, img.Width, img.Height)
                If RTL Then img.RotateFlip(RotateFlipType.Rotate180FlipY)
                G.DrawImage(img, imgRect)
            End If

            If img IsNot Nothing And (Alignment = TabAlignment.Right Or Alignment = TabAlignment.Left) Then
                If Not RTL Then
                    Dim tr As New Rectangle(imgRect.Right + 10, TabRect.Y, TabRect.Width - imgRect.Width - 10, TabRect.Height)
                    G.DrawString(TabPages(i).Text, Font, New SolidBrush(TextColor), tr, StringAligner(ContentAlignment.MiddleLeft))
                Else
                    Dim b As New Bitmap(TabRect.Width, TabRect.Height)
                    Dim gx As Graphics = Graphics.FromImage(b)
                    gx.SmoothingMode = G.SmoothingMode
                    gx.TextRenderingHint = G.TextRenderingHint
                    gx.DrawString(TabPages(i).Text, Font, New SolidBrush(TextColor), New Rectangle(0, 0, b.Width - imgRect.Right - 10, b.Height - 1), StringAligner(ContentAlignment.MiddleLeft, RTL))
                    gx.Flush()
                    b.RotateFlip(RotateFlipType.Rotate180FlipY)
                    G.DrawImage(b, TabRect)
                    gx.Dispose()
                    b.Dispose()
                End If
            Else

                If Not RTL Then
                    G.DrawString(TabPages(i).Text, Font, New SolidBrush(TextColor), TabRect, StringAligner(ContentAlignment.MiddleCenter))
                Else
                    Dim b As New Bitmap(TabRect.Width, TabRect.Height)
                    Dim gx As Graphics = Graphics.FromImage(b)
                    gx.SmoothingMode = G.SmoothingMode
                    gx.TextRenderingHint = G.TextRenderingHint
                    gx.DrawString(TabPages(i).Text, Font, New SolidBrush(TextColor), New Rectangle(0, 0, b.Width - 1, b.Height - 1), StringAligner(ContentAlignment.MiddleCenter, RTL))
                    gx.Flush()
                    b.RotateFlip(RotateFlipType.Rotate180FlipY)
                    G.DrawImage(b, TabRect)
                    gx.Dispose()
                    b.Dispose()
                End If
            End If

        Next
    End Sub
End Class

<DefaultEvent("CheckedChanged")>
Public Class XenonToggle
    Inherits UserControl
    Dim CheckC As New Rectangle(4, 4, 11, 11)
    Dim MouseState As Integer = 0
    Dim WasMoving As Boolean = False

    Sub New()
        SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
        DoubleBuffered = True
        Size = New Size(40, 20)
        Text = ""
    End Sub

    Public Property DarkLight_Toggler As Boolean = False

    ReadOnly DarkLight_TogglerSize As Integer = 13

    Private _checked As Boolean

    Private _Shown As Boolean = False

    Public Property Checked As Boolean
        Get
            Return _checked
        End Get
        Set(ByVal value As Boolean)
            If Not _checked.Equals(value) Then
                _checked = value
                Me.OnCheckedChanged()
            End If
        End Set
    End Property

    Protected Overridable Sub OnCheckedChanged()
        RaiseEvent CheckedChanged(Me, EventArgs.Empty)

        If Not DesignMode And _Shown And AnimateOnClick Then
            If Checked Then

                Dim s As Integer = (Width - 17) * 0.5
                For i As Integer = CheckC.Left To Width - 17 Step +5
                    CheckC.X = i + s
                    Threading.Thread.Sleep(1)
                    Refresh()
                    If i + s >= Width - 17 Then Exit For
                    s -= 1
                    If s < 0 Then s = 0
                Next
                CheckC.X = Width - 17

            Else

                Dim s As Integer = 10
                For i As Integer = CheckC.Left To 4 Step -5
                    CheckC.X = i - s
                    Threading.Thread.Sleep(1)
                    Refresh()
                    If i - s <= 4 Then Exit For
                    s -= 1
                    If s < 0 Then s = 0
                Next
                CheckC.X = 4

            End If

        Else
            If Checked Then
                CheckC.X = Width - 17
            Else
                CheckC.X = 4
            End If
        End If

        If DarkLight_Toggler Then
            CheckC.Width = DarkLight_TogglerSize
            CheckC.Height = DarkLight_TogglerSize
        End If

        'Invalidate()
    End Sub
    Public Event CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)

    Dim CheckedC As Rectangle

    Private AnimateOnClick As Boolean = False
    Protected Overrides Sub OnMouseClick(e As MouseEventArgs)
        AnimateOnClick = True
        Me.Checked = Not Me.Checked
        AnimateOnClick = False
        Me.Invalidate()
        MyBase.OnMouseClick(e)
    End Sub

    Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
        Me.OnPaintBackground(e)
        Dim G As Graphics = e.Graphics
        G.SmoothingMode = SmoothingMode.AntiAlias
        DoubleBuffered = True

        If Parent Is Nothing Then Exit Sub

        BackColor = Style.Colors.Back

        G.Clear(GetParentColor)

        '################################################################################# Customizer
        Dim MainRect As New Rectangle(0, 0, Width - 1, Height - 1)
        Dim BorderColor As Color

        If GetDarkMode() Then BorderColor = BackColor.LightLight Else BorderColor = BackColor.Dark(0.5)

        Dim CheckColor As Color
        If MouseState = 0 Then CheckColor = Style.Colors.BaseColor Else CheckColor = BackColor.CB(If(GetDarkMode(), 0.3, -0.5))

        '#################################################################################
        Dim min As Integer = 4
        Dim max As Integer = Width - 17
        Dim val As Decimal = (CheckC.X) / max

        If val < 0 Then val = 0
        If val > 1 Then val = 1
        If CheckC.X <= min Then val = 0
        If CheckC.X >= max Then val = 1

        Dim lgbChecked, lgbNonChecked, lgborderChecked, lgborderNonChecked As LinearGradientBrush

        lgbChecked = New LinearGradientBrush(MainRect, Color.FromArgb(255 * val, Style.Colors.Border_Checked_Hover), Color.FromArgb(255 * val, Style.Colors.Back_Checked), LinearGradientMode.ForwardDiagonal)
        lgborderChecked = New LinearGradientBrush(MainRect, Color.FromArgb(255 * val, Style.Colors.Border_Checked), Color.FromArgb(255 * val, Style.Colors.Back_Checked), LinearGradientMode.BackwardDiagonal)
        lgbNonChecked = New LinearGradientBrush(MainRect, Color.FromArgb(255 * (1 - val), Style.Colors.Back_Checked), Color.FromArgb(255 * (1 - val), Style.Colors.Border_Checked_Hover), LinearGradientMode.BackwardDiagonal)
        lgborderNonChecked = New LinearGradientBrush(MainRect, Color.FromArgb(255 * (1 - val), Style.Colors.Border_Checked), Color.FromArgb(255 * (1 - val), Style.Colors.Back_Checked), LinearGradientMode.ForwardDiagonal)

        If Not DarkLight_Toggler Then

            e.Graphics.FillRoundedRect(New SolidBrush(Color.FromArgb(255 * val, Style.Colors.Border_Checked_Hover)), MainRect, 9, True)

            e.Graphics.DrawRoundedRect(New Pen(Color.FromArgb(255 * val, Style.Colors.Border_Checked)), MainRect, 9, True)

            e.Graphics.DrawRoundedRect(New Pen(Color.FromArgb(255 * (1 - val), BorderColor)), MainRect, 9, True)

            If Checked Then
                G.FillEllipse(New SolidBrush(If(GetDarkMode(), Color.Black, Color.White)), CheckC)
            Else
                G.FillEllipse(New SolidBrush(BorderColor), CheckC)
            End If

        Else

            e.Graphics.FillRoundedRect(lgbChecked, MainRect, 9, True)
            e.Graphics.FillRoundedRect(lgbNonChecked, MainRect, 9, True)

            If Checked Then
                G.DrawImage(If(BorderColor.IsDark, My.Resources.darkmode_dark, My.Resources.darkmode_light).Fade(val), CheckC)
                G.DrawImage(If(BorderColor.IsDark, My.Resources.lightmode_dark, My.Resources.lightmode_light).Fade(1 - val), CheckC)
            Else
                G.DrawImage(If(Style.Colors.BaseColor.IsDark, My.Resources.darkmode_dark, My.Resources.darkmode_light).Fade(val), CheckC)
                G.DrawImage(If(Style.Colors.BaseColor.IsDark, My.Resources.lightmode_dark, My.Resources.lightmode_light).Fade(1 - val), CheckC)
            End If

            e.Graphics.DrawRoundedRect(New Pen(lgborderChecked), MainRect, 9, True)
            e.Graphics.DrawRoundedRect(New Pen(lgborderNonChecked), MainRect, 9, True)
        End If
    End Sub

    Private Sub XenonToggle_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        Me.Height = 20
        If Width < 40 Then Width = 40

        If DarkLight_Toggler Then
            CheckC.Width = DarkLight_TogglerSize
            CheckC.Height = DarkLight_TogglerSize
        End If

        Refresh()
    End Sub

    Private Sub XenonToggle_HandleCreated(sender As Object, e As EventArgs) Handles Me.HandleCreated

        If Checked Then
            CheckC = New Rectangle(Width - 17, 4, 11, 11)
        Else
            CheckC = New Rectangle(4, 4, 11, 11)
        End If

        If DarkLight_Toggler Then
            CheckC.Width = DarkLight_TogglerSize
            CheckC.Height = DarkLight_TogglerSize
        End If

        If Not DesignMode Then
            Try
                AddHandler FindForm.Load, AddressOf Loaded
                AddHandler FindForm.Shown, AddressOf Showed
                AddHandler Parent.BackColorChanged, AddressOf RefreshColorPalette
            Catch
            End Try
        End If
    End Sub

    Sub Loaded()
        _Shown = False
    End Sub

    Sub Showed()
        _Shown = True
        Invalidate()
    End Sub

    Public Sub RefreshColorPalette()
        If _Shown Then
            Invalidate()
        End If
    End Sub

    Private Sub XenonToggle_MouseMove(sender As Object, e As MouseEventArgs) Handles Me.MouseMove
        If e.Button = MouseButtons.Left Then

            Dim i As Integer = e.X - 0.5 * CheckC.Width
            WasMoving = True
            MouseState = 1

            If i <= 4 Then
                CheckC.X = 4
            ElseIf i >= Width - 17 Then
                CheckC.X = Width - 17
            Else
                CheckC.X = i
            End If

            CheckC.Y = 4
            Refresh()
        End If
    End Sub

    Private Sub XenonToggle_MouseUp(sender As Object, e As MouseEventArgs) Handles Me.MouseUp
        MouseState = 0
        CheckC.Width = 11

        If DarkLight_Toggler Then
            CheckC.Width = DarkLight_TogglerSize
            CheckC.Height = DarkLight_TogglerSize
        End If

        If WasMoving Then
            If e.X < Width * 0.5 Then Checked = False
            If e.X >= Width * 0.5 Then Checked = True
            WasMoving = False
        End If
        Refresh()
    End Sub

    Private Sub XenonToggle_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
        MouseState = 1
        CheckC.Width = 13

        Refresh()
    End Sub

End Class

<DefaultEvent("CheckedChanged")>
Public Class XenonRadioButton
    Inherits Control
    Event CheckedChanged(sender As Object)

    Sub New()
        SetStyle(DirectCast(139286, ControlStyles), True)
        SetStyle(ControlStyles.Selectable, False)
        DoubleBuffered = True
        Font = New Font("Segoe UI", 9)
        ForeColor = Color.White

    End Sub

#Region "Properties"
    Private Sub InvalidateParent()
        If Parent Is Nothing Then Return

        For Each C As Control In Parent.Controls
            If Not (C Is Me) AndAlso (TypeOf C Is XenonRadioButton) Then
                DirectCast(C, XenonRadioButton).Checked = False
            End If
        Next
    End Sub

    Public Property Checked() As Boolean
        Get
            Return _Checked
        End Get
        Set(ByVal value As Boolean)
            Try
                _Checked = value

                If _Checked Then
                    InvalidateParent()
                End If

                RaiseEvent CheckedChanged(Me)

                If AnimateOnClick Then
                    Tmr2.Enabled = True
                    Tmr2.Start()
                Else
                    alpha2 = If(Checked, 255, 0)
                End If

                Refresh()

            Catch
            End Try
        End Set
    End Property

    Private _Checked As Boolean
#End Region

#Region "Events"
    Enum MouseState
        None
        Over
        Down
    End Enum

    Public State As MouseState = MouseState.None
    Private AnimateOnClick As Boolean = False

    Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
        Checked = True
        State = MouseState.Down
        Tmr.Enabled = True
        Tmr.Start()
        Invalidate()
        MyBase.OnMouseDown(e)
    End Sub

    Protected Overrides Sub OnMouseClick(e As MouseEventArgs)
        AnimateOnClick = True
        Checked = True
        State = MouseState.Down
        Tmr2.Enabled = True
        Tmr2.Start()
        Invalidate()
        MyBase.OnMouseClick(e)
    End Sub

    Protected Overrides Sub OnMouseUp(e As MouseEventArgs)
        State = MouseState.Over
        Tmr.Enabled = True
        Tmr.Start()
        Invalidate()
    End Sub

    Private Sub XenonRadioButton_MouseEnter(sender As Object, e As EventArgs) Handles Me.MouseEnter
        State = MouseState.Over
        Tmr.Enabled = True
        Tmr.Start()
        Invalidate()
    End Sub

    Private Sub XenonCheckBox_MouseLeave(sender As Object, e As EventArgs) Handles Me.MouseLeave
        State = MouseState.None
        Tmr.Enabled = True
        Tmr.Start()
        Invalidate()
    End Sub

    Private Sub XenonRadioButton_HandleCreated(sender As Object, e As EventArgs) Handles Me.HandleCreated

        Try
            If Not DesignMode Then
                AddHandler FindForm.Shown, AddressOf Showed
                AddHandler Parent.BackColorChanged, AddressOf RefreshColorPalette
                AddHandler Parent.VisibleChanged, AddressOf RefreshColorPalette
                AddHandler Parent.EnabledChanged, AddressOf RefreshColorPalette
                AddHandler VisibleChanged, AddressOf RefreshColorPalette
                AddHandler EnabledChanged, AddressOf RefreshColorPalette
            End If
        Catch
        End Try

        Try
            alpha = 0
            alpha2 = If(Checked, 255, 0)
        Catch
        End Try
    End Sub

    Sub Showed()
        Invalidate()
    End Sub

    Public Sub RefreshColorPalette()
        Invalidate()
    End Sub
#End Region

#Region "Animator"
    Dim alpha, alpha2 As Integer
    ReadOnly Factor As Integer = 25
    Dim WithEvents Tmr, Tmr2 As New Timer With {.Enabled = False, .Interval = 1}

    Private Sub Tmr_Tick(sender As Object, e As EventArgs) Handles Tmr.Tick
        If Not DesignMode Then

            If State = MouseState.Over Then
                If alpha + Factor <= 255 Then
                    alpha += Factor
                ElseIf alpha + Factor > 255 Then
                    alpha = 255
                    Tmr.Enabled = False
                    Tmr.Stop()
                End If

                Threading.Thread.Sleep(1)
                Invalidate()
            End If

            If Not State = MouseState.Over Then
                If alpha - Factor >= 0 Then
                    alpha -= Factor
                ElseIf alpha - Factor < 0 Then
                    alpha = 0
                    Tmr.Enabled = False
                    Tmr.Stop()
                End If

                Threading.Thread.Sleep(1)
                Invalidate()
            End If
        End If
    End Sub

    Private Sub Tmr2_Tick(sender As Object, e As EventArgs) Handles Tmr2.Tick
        If Not DesignMode Then

            If Checked Then
                If alpha2 + Factor <= 255 Then
                    alpha2 += Factor
                ElseIf alpha2 + Factor > 255 Then
                    alpha2 = 255
                    Tmr2.Enabled = False
                    Tmr2.Stop()
                    AnimateOnClick = False
                End If

                Threading.Thread.Sleep(1)
                Invalidate()
            End If

            If Not Checked Then
                If alpha2 - Factor >= 0 Then
                    alpha2 -= Factor
                ElseIf alpha2 - Factor < 0 Then
                    alpha2 = 0
                    Tmr2.Enabled = False
                    Tmr2.Stop()
                    AnimateOnClick = False
                End If

                Threading.Thread.Sleep(1)
                Invalidate()
            End If
        End If
    End Sub
#End Region

    Private SZ1 As SizeF

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        Try
            Dim G As Graphics = e.Graphics
            If Parent Is Nothing Then Exit Sub
            BackColor = Parent.BackColor
            Dim clr As Color = Style.Colors.Core

            G = e.Graphics
            G.SmoothingMode = SmoothingMode.AntiAlias
            G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit
            DoubleBuffered = True

            '################################################################################# Customizer
            SZ1 = G.MeasureString(Text, Font)

            Dim format As New StringFormat()
            Dim OuterCircle As New Rectangle(3, 4, Height - 8, Height - 8)
            Dim InnerCircle As New Rectangle(4, 5, Height - 10, Height - 10)
            Dim CheckCircle As New Rectangle(7, 8, Height - 16, Height - 16)
            Dim TextRect As New Rectangle(Height - 1, (CLng((Height - SZ1.Height)) \ 2) + 1, Width - OuterCircle.Width, Height - 1)
            Dim RTL As Boolean = (RightToLeft = 1)

            If RTL Then
                format = New StringFormat(StringFormatFlags.DirectionRightToLeft)
                OuterCircle.X = Width - OuterCircle.X - OuterCircle.Width
                InnerCircle.X = Width - InnerCircle.X - InnerCircle.Width
                CheckCircle.X = Width - CheckCircle.X - CheckCircle.Width
                TextRect.Width -= OuterCircle.Width + 13
            End If

#Region "Colors System"
            Dim HoverCircle_Color As Color = Color.FromArgb(alpha2, Style.Colors.Back_Checked)
            Dim HoverCheckedCircle_Color As Color = Color.FromArgb(alpha, Style.Colors.Border_Checked_Hover)
            Dim CheckCircle_Color As Color = Color.FromArgb(alpha2, Style.Colors.Core)
            Dim NonHoverCircle_Color As Color = Style.Colors.Border
            Dim BackCircle_Color As Color = Style.Colors.Back
            Dim ParentColor As Color = GetParentColor
#End Region
            '#################################################################################

            G.Clear(ParentColor)
            G.FillEllipse(New SolidBrush(BackCircle_Color), OuterCircle)

            If Checked Then
                G.FillEllipse(New SolidBrush(HoverCircle_Color), OuterCircle)
                G.FillEllipse(New SolidBrush(CheckCircle_Color), CheckCircle)
                G.DrawEllipse(New Pen(HoverCheckedCircle_Color), OuterCircle)
            Else
                G.FillEllipse(New SolidBrush(HoverCircle_Color), OuterCircle)
                G.FillEllipse(New SolidBrush(CheckCircle_Color), CheckCircle)
                G.DrawEllipse(New Pen(Color.FromArgb(255 - alpha, NonHoverCircle_Color)), InnerCircle)
                G.DrawEllipse(New Pen(Color.FromArgb(alpha, clr)), OuterCircle)
            End If

#Region "Strings"
            If Checked Then
                G.DrawString(Text, Font, New SolidBrush(CheckCircle_Color), TextRect, format)
            Else
                G.DrawString(Text, Font, New SolidBrush(ForeColor), TextRect, format)
            End If
#End Region
        Catch

        End Try
    End Sub


End Class

<DefaultEvent("CheckedChanged")>
Public Class XenonRadioImage
    Inherits Control
    Event CheckedChanged(sender As Object)

    Sub New()
        SetStyle(DirectCast(139286, ControlStyles), True)
        SetStyle(ControlStyles.Selectable, False)
        DoubleBuffered = True
        Font = New Font("Segoe UI", 9)
        ForeColor = Color.White
        Text = ""
    End Sub

#Region "Properties"
    Private Sub InvalidateParent()
        If Parent Is Nothing Then Return

        For Each C As Control In Parent.Controls
            If Not (C Is Me) AndAlso (TypeOf C Is XenonRadioImage) Then
                DirectCast(C, XenonRadioImage).Checked = False
            End If
        Next
    End Sub

    Public Property Checked() As Boolean
        Get
            Return _Checked
        End Get
        Set(ByVal value As Boolean)
            Try
                _Checked = value

                If _Checked Then
                    InvalidateParent()
                End If

                RaiseEvent CheckedChanged(Me)

                Invalidate()

            Catch
            End Try
        End Set
    End Property

    Public Property Image As Image
    Private _Checked As Boolean
    Public Property ShowText As Boolean = False

#End Region

#Region "Events"
    Enum MouseState
        None
        Over
        Down
    End Enum

    Public State As MouseState = MouseState.None
    Private AnimateOnClick As Boolean = False

    Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
        AnimateOnClick = True
        Checked = True
        State = MouseState.Down
        Tmr.Enabled = True
        Tmr.Start()
        Invalidate()
        MyBase.OnMouseDown(e)
    End Sub

    Protected Overrides Sub OnMouseUp(e As MouseEventArgs)
        State = MouseState.Over
        Tmr.Enabled = True
        Tmr.Start()
        Invalidate()
    End Sub

    Private Sub XenonRadioButton_MouseEnter(sender As Object, e As EventArgs) Handles Me.MouseEnter
        State = MouseState.Over
        Tmr.Enabled = True
        Tmr.Start()
        Invalidate()
    End Sub

    Private Sub XenonCheckBox_MouseLeave(sender As Object, e As EventArgs) Handles Me.MouseLeave
        State = MouseState.None
        Tmr.Enabled = True
        Tmr.Start()
        Invalidate()
    End Sub

    Private Sub XenonRadioButton_HandleCreated(sender As Object, e As EventArgs) Handles Me.HandleCreated

        Try
            If Not DesignMode Then
                AddHandler FindForm.Shown, AddressOf Showed
                AddHandler Parent.BackColorChanged, AddressOf RefreshColorPalette
                AddHandler Parent.VisibleChanged, AddressOf RefreshColorPalette
                AddHandler Parent.EnabledChanged, AddressOf RefreshColorPalette
                AddHandler VisibleChanged, AddressOf RefreshColorPalette
                AddHandler EnabledChanged, AddressOf RefreshColorPalette
            End If
        Catch
        End Try

        Try
            alpha = 0
        Catch
        End Try
    End Sub

    Sub Showed()
        Invalidate()
    End Sub

    Public Sub RefreshColorPalette()
        Invalidate()
    End Sub
#End Region

#Region "Animator"
    Dim alpha As Integer
    ReadOnly Factor As Integer = 25
    Dim WithEvents Tmr As New Timer With {.Enabled = False, .Interval = 1}

    Private Sub Tmr_Tick(sender As Object, e As EventArgs) Handles Tmr.Tick
        If Not DesignMode Then

            If State = MouseState.Over Then
                If alpha + Factor <= 255 Then
                    alpha += Factor
                ElseIf alpha + Factor > 255 Then
                    alpha = 255
                    Tmr.Enabled = False
                    Tmr.Stop()
                    AnimateOnClick = False
                End If

                Threading.Thread.Sleep(1)
                Invalidate()
            End If

            If Not State = MouseState.Over Then
                If alpha - Factor >= 0 Then
                    alpha -= Factor
                ElseIf alpha - Factor < 0 Then
                    alpha = 0
                    Tmr.Enabled = False
                    Tmr.Stop()
                    AnimateOnClick = False
                End If

                Threading.Thread.Sleep(1)
                Invalidate()
            End If
        End If
    End Sub
#End Region
    Protected Overrides Sub OnPaint(e As System.Windows.Forms.PaintEventArgs)
        Try
            Dim G As Graphics = e.Graphics
            If Parent Is Nothing Then Exit Sub
            G.Clear(GetParentColor)

            G = e.Graphics
            G.SmoothingMode = SmoothingMode.AntiAlias
            G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit
            DoubleBuffered = True

            Dim MainRect As New Rectangle(0, 0, Width - 1, Height - 1)
            Dim MainRectInner As New Rectangle(1, 1, Width - 3, Height - 3)
            Dim CenterRect As New Rectangle

            If Image IsNot Nothing Then CenterRect = New Rectangle(MainRect.X + (MainRect.Width - Image.Width) / 2,
                                        MainRect.Y + (MainRect.Height - Image.Height) / 2,
                                        Image.Width, Image.Height)

            Dim bkC As Color = If(_Checked, Style.Colors.Back_Checked, Style.Colors.Back)
            Dim bkCC As Color = Color.FromArgb(alpha, Style.Colors.Back_Checked)

            G.FillRoundedRect(New SolidBrush(bkC), MainRectInner)
            G.FillRoundedRect(New SolidBrush(bkCC), MainRect)

            Dim lC As Color = Color.FromArgb(255 - alpha, If(_Checked, Style.Colors.Border_Checked, Style.Colors.Border))
            Dim lCC As Color = Color.FromArgb(alpha, Style.Colors.Border_Checked_Hover)

            G.DrawRoundedRect_LikeW11(New Pen(lC), MainRectInner)
            G.DrawRoundedRect_LikeW11(New Pen(lCC), MainRect)

            If Image IsNot Nothing Then G.DrawImage(Image, CenterRect)

            If ShowText Then G.DrawString(Text, Font, New SolidBrush(ForeColor), MainRectInner, StringAligner(ContentAlignment.MiddleCenter))
        Catch

        End Try
    End Sub

End Class

<DefaultEvent("CheckedChanged")>
Public Class XenonCheckBox
    Inherits Control
    Event CheckedChanged(sender As Object)

    Sub New()
        SetStyle(DirectCast(139286, ControlStyles), True)
        SetStyle(ControlStyles.Selectable, False)
        DoubleBuffered = True
        Font = New Font("Segoe UI", 9)
        ForeColor = Color.White
    End Sub

#Region "Properties"
    Public Property Checked() As Boolean
        Get
            Return _Checked
        End Get
        Set(ByVal value As Boolean)
            Try
                _Checked = value
                RaiseEvent CheckedChanged(Me)
                If AnimateOnClick Then
                    Tmr2.Enabled = True
                    Tmr2.Start()
                Else
                    alpha2 = If(Checked, 255, 0)
                End If
                Refresh()
            Catch
            End Try
        End Set
    End Property

    Private _Checked As Boolean

    ReadOnly Radius As Integer = 5

#End Region

#Region "Events"

    Enum MouseState
        None
        Over
        Down
    End Enum

    Public State As MouseState = MouseState.None
    Private AnimateOnClick As Boolean = False

    Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
        State = MouseState.Down
        Tmr.Enabled = True
        Tmr.Start()
        Invalidate()
    End Sub

    Protected Overrides Sub OnMouseClick(e As MouseEventArgs)
        AnimateOnClick = True
        Checked = Not Checked
        State = MouseState.Down
        Tmr2.Enabled = True
        Tmr2.Start()
        Invalidate()
    End Sub

    Protected Overrides Sub OnMouseUp(e As MouseEventArgs)
        State = MouseState.Over
        Tmr.Enabled = True
        Tmr.Start()
        Invalidate()
    End Sub

    Private Sub XenonCheckBox_MouseEnter(sender As Object, e As EventArgs) Handles Me.MouseEnter
        State = MouseState.Over
        Tmr.Enabled = True
        Tmr.Start()
        Invalidate()
    End Sub

    Private Sub XenonCheckBox_MouseLeave(sender As Object, e As EventArgs) Handles Me.MouseLeave
        State = MouseState.None
        Tmr.Enabled = True
        Tmr.Start()
        Invalidate()
    End Sub

    Private Sub XenonCheckbox_HandleCreated(sender As Object, e As EventArgs) Handles Me.HandleCreated
        Try
            alpha = If(DesignMode, 255, 0)
            alpha2 = If(Checked, 255, 0)

            If Not DesignMode Then
                Try
                    AddHandler FindForm.Shown, AddressOf Showed
                    AddHandler Parent.BackColorChanged, AddressOf RefreshColorPalette
                    AddHandler Parent.VisibleChanged, AddressOf RefreshColorPalette
                    AddHandler Parent.EnabledChanged, AddressOf RefreshColorPalette
                Catch
                End Try
            End If
        Catch
        End Try

    End Sub

    Sub Showed()
        Invalidate()
    End Sub

    Public Sub RefreshColorPalette()
        Invalidate()
    End Sub
#End Region

#Region "Animator"
    Dim alpha, alpha2 As Integer
    ReadOnly Factor As Integer = 25
    Private WithEvents Tmr, Tmr2 As New Timer With {.Enabled = False, .Interval = 1}
    Private Sub Tmr_Tick(sender As Object, e As EventArgs) Handles Tmr.Tick
        If Not DesignMode Then

            If State = MouseState.Over Then
                If alpha + Factor <= 255 Then
                    alpha += Factor
                ElseIf alpha + Factor > 255 Then
                    alpha = 255
                    Tmr.Enabled = False
                    Tmr.Stop()
                End If

                Threading.Thread.Sleep(1)
                Refresh()
            End If

            If Not State = MouseState.Over Then
                If alpha - Factor >= 0 Then
                    alpha -= Factor
                ElseIf alpha - Factor < 0 Then
                    alpha = 0
                    Tmr.Enabled = False
                    Tmr.Stop()
                End If

                Threading.Thread.Sleep(1)
                Refresh()
            End If
        End If
    End Sub

    Private Sub Tmr2_Tick(sender As Object, e As EventArgs) Handles Tmr2.Tick
        If Not DesignMode Then

            If Checked Then
                If alpha2 + Factor <= 255 Then
                    alpha2 += Factor
                ElseIf alpha2 + Factor > 255 Then
                    alpha2 = 255
                    Tmr2.Enabled = False
                    Tmr2.Stop()
                    AnimateOnClick = False
                End If

                Threading.Thread.Sleep(1)
                Refresh()
            End If

            If Not Checked Then
                If alpha2 - Factor >= 0 Then
                    alpha2 -= Factor
                ElseIf alpha2 - Factor < 0 Then
                    alpha2 = 0
                    Tmr2.Enabled = False
                    Tmr2.Stop()
                    AnimateOnClick = False
                End If

                Threading.Thread.Sleep(1)
                Refresh()

            End If
        End If
    End Sub
#End Region

    Protected Overrides Sub OnPaint(e As System.Windows.Forms.PaintEventArgs)
        Try
            If Parent Is Nothing Then Exit Sub
            BackColor = Parent.BackColor

            Dim G As Graphics = e.Graphics
            G.SmoothingMode = SmoothingMode.AntiAlias
            G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit
            DoubleBuffered = True

            '################################################################################# Customizer
            Dim format As New StringFormat()

            Dim SZ1 As SizeF = G.MeasureString(Text, Font)
            Dim PT1 As New PointF(Height - 1, (CLng((Height - SZ1.Height)) \ 2) + 1)

            Dim OuterCheckRect As New Rectangle(3, 4, Height - 8, Height - 8)
            Dim InnerCheckRect As New Rectangle(4, 5, Height - 10, Height - 10)
            Dim TextRect As New Rectangle(Height - 1, (CLng((Height - SZ1.Height)) \ 2) + 1, Width - InnerCheckRect.Width, Height - 1)

#Region "Colors System"
            Dim HoverRect_Color As Color = Color.FromArgb(alpha2, Style.Colors.Back_Checked)
            Dim HoverCheckedRect_Color As Color = Color.FromArgb(alpha, Style.Colors.Border_Checked_Hover)
            Dim CheckRect_Color As Color = Color.FromArgb(alpha2, Style.Colors.Core)
            Dim NonHoverRect_Color As Color = Style.Colors.Border
            Dim BackRect_Color As Color = Style.Colors.Back
            Dim ParentColor As Color = GetParentColor
#End Region

            Dim RTL As Boolean = (RightToLeft = 1)

            If RTL Then
                format = New StringFormat(StringFormatFlags.DirectionRightToLeft)
                OuterCheckRect.X = Width - OuterCheckRect.X - OuterCheckRect.Width
                InnerCheckRect.X = Width - InnerCheckRect.X - InnerCheckRect.Width
                TextRect.Width = Width - InnerCheckRect.Width - 10
                TextRect.X = 0
            End If

#Region "Check Sign x,y system"
            Dim x1_Left As Integer = InnerCheckRect.X + 3
            Dim y1_Left As Integer = CInt(0.8 * InnerCheckRect.Height)
            Dim x2_Left As Integer = x1_Left
            Dim y2_Left As Integer = InnerCheckRect.Y + InnerCheckRect.Height - 3

            Dim x1_Right As Integer = x2_Left
            Dim y1_Right As Integer = y2_Left
            Dim x2_Right As Integer = InnerCheckRect.Right - 2
            Dim y2_Right As Integer = y1_Left - 3

            Dim CheckSignPen As New Pen(CheckRect_Color, 1.8F)
#End Region
            '#################################################################################

            G.Clear(ParentColor)

            G.FillRoundedRect(New SolidBrush(Style.Colors.Back), InnerCheckRect, Radius)

            If _Checked Then
                G.FillRoundedRect(New SolidBrush(HoverRect_Color), InnerCheckRect, Radius)
                G.FillRoundedRect(New SolidBrush(Color.FromArgb(alpha, HoverRect_Color)), OuterCheckRect, Radius)

                G.DrawRoundedRect(New Pen(Color.FromArgb(255 - alpha, HoverCheckedRect_Color)), InnerCheckRect, Radius)
                G.DrawRoundedRect(New Pen(Color.FromArgb(alpha, HoverCheckedRect_Color)), OuterCheckRect, Radius)

                G.DrawLine(CheckSignPen, x1_Left, y1_Left, x2_Left, y2_Left)
                G.DrawLine(CheckSignPen, x1_Right, y1_Right, x2_Right, y2_Right)
            Else
                G.FillRoundedRect(New SolidBrush(HoverRect_Color), OuterCheckRect, Radius)
                G.DrawRoundedRect(New Pen(HoverCheckedRect_Color), OuterCheckRect, Radius)
                G.DrawLine(CheckSignPen, x1_Left, y1_Left, x2_Left, y2_Left)
                G.DrawLine(CheckSignPen, x1_Right, y1_Right, x2_Right, y2_Right)
                G.DrawRoundedRect(New Pen(Color.FromArgb(255 - alpha, NonHoverRect_Color)), InnerCheckRect, Radius)
            End If

            If Checked Then
                G.DrawString(Text, Font, New SolidBrush(CheckRect_Color), TextRect, format)
            Else
                G.DrawString(Text, Font, New SolidBrush(ForeColor), TextRect, format)
            End If

        Catch
        End Try
    End Sub
End Class

<DefaultEvent("Click")>
Public Class XenonGroupBox : Inherits Panel

    Sub New()
        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.OptimizedDoubleBuffer Or ControlStyles.UserPaint Or ControlStyles.ResizeRedraw, True)
        DoubleBuffered = True
        Text = ""
    End Sub

    Private LineColor As Color

    <Browsable(True)>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    <EditorBrowsable(EditorBrowsableState.Always)>
    <Bindable(True)>
    Public Overrides Property Text As String = ""

    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        MyBase.OnPaint(e)
        Dim G As Graphics = e.Graphics
        DoubleBuffered = True
        G.SmoothingMode = SmoothingMode.AntiAlias
        Dim Rect As New Rectangle(0, 0, Width - 1, Height - 1)
        G.Clear(GetParentColor)
        BackColor = GetParentColor.CB(If(GetParentColor.IsDark, 0.04, -0.05))
        LineColor = GetParentColor.CB(If(GetParentColor.IsDark, 0.06, -0.07))
        G.FillRoundedRect(New SolidBrush(BackColor), Rect)
        G.DrawRoundedRect(New Pen(LineColor), Rect)
    End Sub



End Class

<DefaultEvent("Click")>
Public Class XenonAnimatedBox : Inherits Panel

    Sub New()
        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.OptimizedDoubleBuffer Or ControlStyles.UserPaint Or ControlStyles.ResizeRedraw, True)
        DoubleBuffered = True
        Text = ""
    End Sub

#Region "Properties"
    Public Property Color1 As Color = Color.DodgerBlue
    Public Property Color2 As Color = Color.Crimson
    Public Property [Color] As Color = Color1
    Property Style As ColorsStyle = ColorsStyle.SwapColors

    <Browsable(True)>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    <EditorBrowsable(EditorBrowsableState.Always)>
    <Bindable(True)>
    Public Overrides Property Text As String = ""

    Enum ColorsStyle
        SwapColors
        MixedColors
    End Enum
#End Region


    Private LineColor As Color
    Private WithEvents Tmr As New Timer With {.Enabled = False, .Interval = 1}
    Private _Angle As Single = 0

    Private Sub Tmr_Tick(sender As Object, e As EventArgs) Handles Tmr.Tick
        If Not DesignMode Then

            If _Angle + 1.5 > 360 Then
                _Angle = 0

                If Style = ColorsStyle.SwapColors Then
                    Dim Cx1, Cx2 As Color

                    If GetDarkMode() Then
                        Cx1 = Color1.Dark(0.15)
                        Cx2 = Color2.Dark(0.15)
                    Else
                        Cx1 = Color1.Light(0.6)
                        Cx2 = Color2.Light(0.6)
                    End If

                    If [Color] = Cx1 Or [Color] = Color1 Then
                        Visual.FadeColor(Me, "Color", [Color], Cx2, 10, 1)
                    Else
                        Visual.FadeColor(Me, "Color", [Color], Cx1, 10, 1)
                    End If

                End If

            Else
                _Angle += 1.5
            End If

            Refresh()
        Else
            Tmr.Enabled = False
            Tmr.Stop()
        End If
    End Sub

    Dim C1, C2 As Color

    ReadOnly Noise As New TextureBrush(My.Resources.GaussianBlur.Fade(0.7))

    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        MyBase.OnPaint(e)
        Dim G As Graphics = e.Graphics
        DoubleBuffered = True

        G.SmoothingMode = SmoothingMode.AntiAlias

        Dim Rect As New Rectangle(0, 0, Width - 1, Height - 1)

        G.Clear(GetParentColor)

        If Not DesignMode Then
            If Style = ColorsStyle.SwapColors Then
                If GetDarkMode() Then
                    C1 = [Color].Dark(0.15)
                Else
                    C1 = [Color].Light(0.6)
                End If

                C2 = GetParentColor
            ElseIf Style = ColorsStyle.MixedColors Then

                If GetDarkMode() Then
                    C1 = Color1.Dark(0.15)
                    C2 = Color2.Dark(0.15)
                Else
                    C1 = Color1.Light(0.6)
                    C2 = Color2.Light(0.6)
                End If

            End If

            Dim l As New LinearGradientBrush(Rect, C1, C2, _Angle, False)

            LineColor = Color.FromArgb(120, 150, 150, 150)

            If Dock = DockStyle.None Then
                G.FillRoundedRect(l, Rect)
                G.FillRoundedRect(Noise, Rect)
                G.DrawRoundedRect(New Pen(LineColor), Rect)
            Else
                G.FillRectangle(l, Rect)
                G.FillRectangle(Noise, Rect)
            End If
        End If

    End Sub

    Private Sub XenonAnimatedBox_HandleCreated(sender As Object, e As EventArgs) Handles Me.HandleCreated
        If Not DesignMode Then
            Tmr.Enabled = True
            Tmr.Start()
        Else
            Tmr.Enabled = False
            Tmr.Stop()
        End If
    End Sub

End Class

<DefaultEvent("Click")>
Public Class XenonCP
    Inherits Panel

    Sub New()
        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.OptimizedDoubleBuffer Or ControlStyles.UserPaint Or ControlStyles.ResizeRedraw, True)
        DoubleBuffered = True
        Text = ""
    End Sub

#Region "Properties"
    Public Property DefaultColor As Color = Color.Black
    Public Property ForceNoNerd As Boolean = False

    Private LineColor As Color

    <Browsable(True)>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    <EditorBrowsable(EditorBrowsableState.Always)>
    <Bindable(True)>
    Public Overrides Property Text As String = ""
#End Region

#Region "Events"

    Enum MouseState
        None
        Over
        Down
    End Enum

    Public State As MouseState = MouseState.None

    Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
        State = MouseState.Down
        Tmr.Enabled = True
        Tmr.Start()
        Invalidate()
    End Sub

    Protected Overrides Sub OnMouseUp(e As MouseEventArgs)
        State = MouseState.Over
        Tmr.Enabled = True
        Tmr.Start()
        Invalidate()
    End Sub

    Private Sub XenonCheckBox_MouseEnter(sender As Object, e As EventArgs) Handles Me.MouseEnter
        State = MouseState.Over
        Tmr.Enabled = True
        Tmr.Start()
        Invalidate()
    End Sub

    Private Sub XenonCheckBox_MouseLeave(sender As Object, e As EventArgs) Handles Me.MouseLeave
        State = MouseState.None
        Tmr.Enabled = True
        Tmr.Start()
        Invalidate()
    End Sub

    Private Sub XenonRadioButton_HandleCreated(sender As Object, e As EventArgs) Handles Me.HandleCreated
        alpha = 0
    End Sub

#End Region

#Region "Animator"
    Dim alpha As Integer
    ReadOnly Factor As Integer = 15
    Dim WithEvents Tmr As New Timer With {.Enabled = False, .Interval = 1}

    Private Sub Tmr_Tick(sender As Object, e As EventArgs) Handles Tmr.Tick
        If Not DesignMode Then

            If State = MouseState.Over Then
                If alpha + Factor <= 255 Then
                    alpha += Factor
                ElseIf alpha + Factor > 255 Then
                    alpha = 255
                    Tmr.Enabled = False
                    Tmr.Stop()
                End If

                Threading.Thread.Sleep(1)
                Invalidate()
            End If

            If Not State = MouseState.Over Then
                If alpha - Factor >= 0 Then
                    alpha -= Factor
                ElseIf alpha - Factor < 0 Then
                    alpha = 0
                    Tmr.Enabled = False
                    Tmr.Stop()
                End If

                Threading.Thread.Sleep(1)
                Invalidate()
            End If
        End If
    End Sub
#End Region

    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        MyBase.OnPaint(e)
        Dim G As Graphics = e.Graphics
        G.SmoothingMode = SmoothingMode.AntiAlias
        DoubleBuffered = True
        Dim Rect As New Rectangle(0, 0, Width - 1, Height - 1)
        Dim RectInner As New Rectangle(1, 1, Width - 3, Height - 3)

        G.Clear(GetParentColor)

        Select Case State
            Case MouseState.None
                LineColor = If(BackColor.IsDark, BackColor.CB(0.05), BackColor.CB(-0.05))

            Case MouseState.Over
                LineColor = If(BackColor.IsDark, BackColor.CB(0.15), BackColor.CB(-0.15))

            Case MouseState.Down
                LineColor = If(BackColor.IsDark, BackColor.CB(0.1), BackColor.CB(-0.1))

        End Select


        LineColor = Color.FromArgb(255, LineColor.R, LineColor.G, LineColor.B)

        Dim R As Integer = 5

        G.FillRoundedRect(New SolidBrush(BackColor), RectInner, R)
        G.FillRoundedRect(New SolidBrush(Color.FromArgb(alpha, BackColor)), Rect, R)

        G.DrawRoundedRect_LikeW11(New Pen(Color.FromArgb(alpha, LineColor)), Rect, R)
        G.DrawRoundedRect_LikeW11(New Pen(Color.FromArgb(255 - alpha, LineColor)), RectInner, R)


        If Not DesignMode Then
            If My.[Settings].Nerd_Stats And Not ForceNoNerd Then
                G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit
                Dim IsDefault As Boolean = (BackColor = DefaultColor)
                Dim FC0 As Color = If(BackColor.IsDark, LineColor.LightLight, LineColor.Dark(0.9))
                Dim FC1 As Color = If(BackColor.IsDark, LineColor.LightLight, LineColor.Dark(0.9))

                FC0 = Color.FromArgb(100, FC0)
                FC1 = Color.FromArgb(alpha, FC1)

                Dim RectX As Rectangle = Rect
                RectX.Y += 1

                Dim CF As ColorFormat = ColorFormat.HEX
                If My.[Settings].Nerd_Stats_Kind = XeSettings.Nerd_Stats_Type.HEX Then CF = ColorFormat.HEX
                If My.[Settings].Nerd_Stats_Kind = XeSettings.Nerd_Stats_Type.RGB Then CF = ColorFormat.RGB
                If My.[Settings].Nerd_Stats_Kind = XeSettings.Nerd_Stats_Type.HSL Then CF = ColorFormat.HSL
                If My.[Settings].Nerd_Stats_Kind = XeSettings.Nerd_Stats_Type.Dec Then CF = ColorFormat.Dec


                Dim S As String = If(IsDefault, "D ", "") & BackColor.ReturnFormat(CF, My.[Settings].Nerd_Stats_HexHash, Not (BackColor.A = 255))
                Dim F As Font

                If IsDefault Then
                    F = My.Application.ConsoleFontDef
                Else
                    F = My.Application.ConsoleFont
                End If

                G.DrawString(S, F, New SolidBrush(FC0), RectX, StringAligner(ContentAlignment.MiddleCenter))
                G.DrawString(S, F, New SolidBrush(FC1), RectX, StringAligner(ContentAlignment.MiddleCenter))

            End If
        End If

    End Sub



End Class
Public Class XenonButton : Inherits Button
    Sub New()
        Font = New Font("Segoe UI", 9)
        ForeColor = Color.White
        If GetDarkMode() Then BackColor = Color.FromArgb(50, 50, 50) Else BackColor = Color.FromArgb(225, 225, 225)
        LineColor = Color.FromArgb(0, 81, 210)
        Image = MyBase.Image
        DoubleBuffered = True

        Try
            If Image IsNot Nothing Then : LineImage = Image.AverageColor
            Else : LineImage = LineColor : End If
        Catch : End Try


    End Sub

#Region "Properties"
    Public Property LineSize As Integer = 1

#Region "Line Color Property"
    Private LineColorValue As Color = Color.FromArgb(0, 81, 210)
    Public Event LineColorChanged As PropertyChangedEventHandler

    Private Sub LineColorNotifyPropertyChanged(ByVal info As String)
        RaiseEvent LineColorChanged(Me, New PropertyChangedEventArgs(info))
    End Sub

    Public Property LineColor() As Color
        Get
            Return LineColorValue
        End Get

        Set(ByVal LineColor As Color)
            If Not (LineColor = LineColorValue) Then
                LineColorValue = LineColor
                LineColorNotifyPropertyChanged("ControlColorChanged")
            End If
        End Set
    End Property
#End Region


    Private _Image As Image

    Public Overloads Property Image() As Image
        Get
            Return _Image
        End Get
        Set(ByVal value As Image)
            _Image = value

            Try
                If Image IsNot Nothing Then
                    LineImage = Image.AverageColor
                    LineColor = LineImage
                Else
                    LineImage = LineColor
                End If
            Catch

            End Try

            Invalidate()
        End Set
    End Property

    Dim LineImage As Color = LineColor
#End Region

#Region "Events"
    Dim BC As Color
    ReadOnly Steps As Integer = 15
    ReadOnly Delay As Integer = 1

    Protected Overrides Sub OnPaintBackground(e As PaintEventArgs)
        MyBase.OnPaintBackground(e)
    End Sub

    Protected Overrides Sub OnBackColorChanged(e As EventArgs)
        Invalidate()
        MyBase.OnBackColorChanged(e)
    End Sub

#Region "OnMouse"
    Protected Overrides Sub OnMouseEnter(ByVal e As EventArgs)
        MyBase.OnMouseEnter(e)
        State = MouseState.Over
        Dim C_Before As Color = BackColor
        Dim C_After As Color

        Select Case GetDarkMode()
            Case True
                C_After = LineColor.Dark(0.15)
            Case False
                C_After = LineColor.Light(0.9).CB(0.4)
        End Select

        If Not DesignMode Then Visual.FadeColor(Me, "BackColor", C_Before, C_After, Steps, Delay)

        If _Shown Then
            Tmr.Enabled = True
            Tmr.Start()
        End If

        Invalidate()
    End Sub

    Protected Overrides Sub OnMouseLeave(ByVal e As EventArgs)
        MyBase.OnMouseLeave(e)
        State = MouseState.None

        Dim C_Before As Color = BackColor

        Dim C_After As Color = GetParentColor.CB(If(GetParentColor.IsDark, 0.04, -0.03))

        If Not DesignMode Then Visual.FadeColor(Me, "BackColor", C_Before, C_After, Steps, Delay)

        If _Shown Then
            Tmr.Enabled = True
            Tmr.Start()
        End If

        Invalidate()
    End Sub

    Protected Overrides Sub OnMouseDown(ByVal e As MouseEventArgs)

        Dim C_Before As Color = BackColor
        Dim C_After As Color

        Select Case GetDarkMode()
            Case True
                C_After = LineColor.Dark(0.3)
            Case False
                C_After = LineColor.Light(0.75)
        End Select

        If Not DesignMode Then Visual.FadeColor(Me, "BackColor", C_Before, C_After, Steps, Delay)
        State = MouseState.Down

        If _Shown Then
            Tmr.Enabled = True
            Tmr.Start()
        End If

        Invalidate()

        MyBase.OnMouseDown(e)
    End Sub

    Protected Overrides Sub OnMouseUp(ByVal e As MouseEventArgs)
        MyBase.OnMouseUp(e)

        Dim C_Before As Color = BackColor
        Dim C_After As Color

        Select Case GetDarkMode()
            Case True
                C_After = LineColor.Dark(0.15)
            Case False
                C_After = LineColor.Light(0.9).CB(0.4)
        End Select

        If Not DesignMode Then Visual.FadeColor(Me, "BackColor", C_Before, C_After, Steps, Delay)

        State = MouseState.Over

        If _Shown Then
            Tmr.Enabled = True
            Tmr.Start()
        End If

        Invalidate()
    End Sub
#End Region

#Region "OnKey"
    Protected Overrides Sub OnKeyDown(ByVal e As KeyEventArgs)
        MyBase.OnKeyDown(e)
        Dim C_Before As Color = BackColor
        Dim C_After As Color

        Select Case GetDarkMode()
            Case True
                C_After = LineColor.Light(0.3)
            Case False
                C_After = LineColor.Light(0.75)
        End Select

        If Not DesignMode Then Visual.FadeColor(Me, "BackColor", C_Before, C_After, Steps, Delay)
        State = MouseState.Down : Invalidate()
    End Sub

    Protected Overrides Sub OnKeyUp(ByVal e As KeyEventArgs)
        MyBase.OnKeyUp(e)
        State = MouseState.None

        Dim C_Before As Color = BackColor
        Dim C_After As Color = GetParentColor.CB(If(GetParentColor.IsDark, 0.04, -0.03))
        If Not DesignMode Then Visual.FadeColor(Me, "BackColor", C_Before, C_After, Steps, Delay)
        Invalidate()
    End Sub
#End Region

    Protected Overrides Sub OnLeave(ByVal e As EventArgs)
        MyBase.OnLeave(e)
        State = MouseState.None

        Dim C_Before As Color = BackColor
        Dim C_After As Color = GetParentColor.CB(If(GetParentColor.IsDark, 0.04, -0.03))
        If Not DesignMode Then Visual.FadeColor(Me, "BackColor", C_Before, C_After, Steps, Delay)
        Invalidate()
    End Sub

    Private Sub XenonButton_LostFocus(sender As Object, e As EventArgs) Handles Me.LostFocus
        State = MouseState.None : Invalidate()
    End Sub

    Enum MouseState
        None
        Over
        Down
    End Enum

    Public State As MouseState = MouseState.None
#End Region

#Region "Animator"
    Dim alpha As Integer
    ReadOnly Factor As Integer = 15
    Dim WithEvents Tmr As New Timer With {.Enabled = False, .Interval = 1}

    Private Sub Tmr_Tick(sender As Object, e As EventArgs) Handles Tmr.Tick
        Try
            If Not DesignMode Then

                If State = MouseState.Over Then
                    If alpha + Factor <= 255 Then
                        alpha += Factor
                    ElseIf alpha + Factor > 255 Then
                        alpha = 255
                        Tmr.Enabled = False
                        Tmr.Stop()
                    End If

                    If _Shown Then
                        Threading.Thread.Sleep(1)
                        Invalidate()
                    End If
                End If

                If Not State = MouseState.Over Then
                    If alpha - Factor >= 0 Then
                        alpha -= Factor
                    ElseIf alpha - Factor < 0 Then
                        alpha = 0
                        Tmr.Enabled = False
                        Tmr.Stop()
                    End If

                    If _Shown Then
                        Threading.Thread.Sleep(1)
                        Invalidate()
                    End If

                End If
            End If
        Catch
        End Try
    End Sub
#End Region

    ReadOnly Noise As New TextureBrush(My.Resources.GaussianBlur.Fade(0.6))

    Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
        Dim G As Graphics = e.Graphics
        G.SmoothingMode = SmoothingMode.AntiAlias
        G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit
        DoubleBuffered = True

        '################################################################################# Customizer
        Dim Rect As New Rectangle(0, 0, Width - 1, Height - 1)
        Dim ParentColor As Color = GetParentColor
        Dim InnerRect As New Rectangle(1, 1, Width - 3, Height - 3)
        '#################################################################################

        G.Clear(ParentColor)
        Dim c1, c1x As Color

        G.FillRoundedRect(New SolidBrush(Color.FromArgb(255 - alpha, BackColor)), InnerRect)
        G.FillRoundedRect(New SolidBrush(Color.FromArgb(alpha, BackColor)), Rect)

        If Not State = MouseState.None Then G.FillRoundedRect(Noise, Rect)

        Dim c As Color

        Select Case State
            Case MouseState.None
                c = BackColor.CB(If(ParentColor.IsDark, 0.02, -0.02))

            Case MouseState.Over
                c = BackColor.CB(If(ParentColor.IsDark, 0.15, -0.05))

            Case MouseState.Down
                c = BackColor.CB(If(ParentColor.IsDark, 0.08, -0.03))

        End Select

        c1 = Color.FromArgb(255 - alpha, c)
        c1x = Color.FromArgb(alpha, c)

        G.DrawRoundedRect_LikeW11(New Pen(c1x), Rect)
        G.DrawRoundedRect_LikeW11(New Pen(c1), InnerRect)

#Region "Text and Image Render"
        Dim ButtonString As New StringFormat() With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center}
        Dim RTL As Boolean = (RightToLeft = 1)
        If RTL Then ButtonString.FormatFlags = StringFormatFlags.DirectionRightToLeft

        Dim imgX, imgY As Integer

        Try
            If Image IsNot Nothing Then imgX = CInt((Width - Image.Width) / 2)
        Catch : End Try

        Try : If Image IsNot Nothing Then imgY = CInt((Height - Image.Height) / 2)
        Catch : End Try

        If Image Is Nothing Then
            Try
                G.DrawString(Text, Font, New SolidBrush(ForeColor), New Rectangle(1, 0, Width, Height), StringAligner(TextAlign, RTL))
            Catch
            End Try
        Else

            Select Case Me.ImageAlign
                Case ContentAlignment.MiddleCenter
                    ButtonString.Alignment = StringAlignment.Center : ButtonString.LineAlignment = StringAlignment.Near
                    Dim alx As Integer = CInt((Height - (Image.Height + 4 + Text.Measure(MyBase.Font).Height)) / 2)

                    Try : If Image IsNot Nothing Then
                            If Text = Nothing Then
                                G.DrawImage(Me.Image, New Rectangle(imgX, imgY, Image.Width, Image.Height))
                            Else
                                G.DrawImage(Me.Image, New Rectangle(imgX, alx, Image.Width, Image.Height))
                            End If
                        End If
                        G.DrawString(Text, Font, New SolidBrush(Me.ForeColor), New Rectangle(0, alx + 9 + Image.Height, Width, Height), ButtonString)
                    Catch : End Try

                Case ContentAlignment.MiddleLeft
                    Dim Rec As New Rectangle(imgY, imgY, Image.Width, Image.Height)
                    Dim Bo As Integer = imgY + Image.Width + imgY - 5
                    Dim RecText As New Rectangle(Bo, imgY, Text.Measure(Font).Width + 15 - imgY, Image.Height)
                    Dim u As Rectangle = Rectangle.Union(Rec, RecText)
                    u.X = (Width - u.Width) / 2
                    Dim innerSpace As Integer = RecText.Left - Rec.Right

                    If Not RTL Then
                        Rec.X = u.Left
                        RecText.X = u.Left + Rec.Width + innerSpace
                    Else
                        Rec.X = u.Right - Rec.Width
                        RecText.X = u.Right - RecText.Width - Rec.Width - innerSpace
                    End If


                    G.DrawImage(Me.Image, Rec)
                    G.DrawString(Text, Font, New SolidBrush(ForeColor), RecText, ButtonString)

                Case ContentAlignment.MiddleRight
                    Dim Rec As New Rectangle(imgY, imgY, Image.Width, Image.Height)
                    Dim Bo As Integer = imgY + Image.Width + imgY - 5
                    Dim RecText As New Rectangle(Bo, imgY, Width - Bo, Image.Height)
                    Dim u As Rectangle = Rectangle.Union(Rec, RecText)
                    Dim innerSpace As Integer = RecText.Left - Rec.Right

                    If Not RTL Then
                        Rec.X = u.Left
                        RecText.X = u.Left + Rec.Width + innerSpace
                    Else
                        Rec.X = u.Right - Rec.Width - 2
                        RecText.X = u.Right - RecText.Width - Rec.Width - innerSpace
                    End If

                    G.DrawImage(Me.Image, Rec)
                    G.DrawString(Text, Font, New SolidBrush(ForeColor), RecText, ButtonString)
            End Select
        End If
#End Region

    End Sub

    Private Sub XenonButton_HandleCreated(sender As Object, e As EventArgs) Handles Me.HandleCreated
        Try
            Try
                BackColor = GetParentColor.CB(If(GetParentColor.IsDark, 0.04, -0.04))
            Catch
            End Try

            If Not DesignMode Then
                Try
                    AddHandler FindForm.Load, AddressOf Loaded
                    AddHandler FindForm.Shown, AddressOf Showed
                    AddHandler Parent.Invalidated, AddressOf Rfrsh
                    AddHandler Parent.BackColorChanged, AddressOf Rfrsh
                Catch
                End Try
            End If

            alpha = 0
        Catch
        End Try
    End Sub

    Private _Shown As Boolean = False

    Sub Loaded()
        _Shown = False
    End Sub

    Sub Showed()
        _Shown = True
        Rfrsh()
    End Sub

    Sub Rfrsh()
        Try
            BC = GetParentColor.CB(If(GetParentColor.IsDark, 0.05, -0.03))
            BackColor = BC
            Invalidate()
        Catch
        End Try
    End Sub

End Class
Public Class XenonSeparator
    Inherits Control
    Private G As Graphics

    Sub New()
        TabStop = False
        DoubleBuffered = True
        Text = ""
    End Sub

#Region "Events"
    Protected Overrides Sub OnResize(e As EventArgs)
        MyBase.OnResize(e)
        Size = New Size(Width, 1)
    End Sub
#End Region

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        G = e.Graphics
        G.SmoothingMode = SmoothingMode.AntiAlias
        DoubleBuffered = True
        MyBase.OnPaint(e)

        Dim clr As Color
        clr = Color.FromArgb(75, 75, 75)

        '################################################################################# Customizer
        Dim IdleLine As Color

        If Parent IsNot Nothing Then
            If GetDarkMode() Then IdleLine = Parent.BackColor.CB(0.1) Else IdleLine = Parent.BackColor.CB(-0.1)
        Else
            If GetDarkMode() Then IdleLine = Color.FromArgb(76, 76, 76) Else IdleLine = Color.FromArgb(210, 210, 210)
        End If
        '################################################################################# Customizer

        Using C As New Pen(IdleLine) : G.DrawLine(C, New Point(0, 0), New Point(Width, 0)) : End Using
    End Sub

End Class
Public Class XenonNumericUpDown
    Inherits Control
    Public Event ValueChanged(sender As Object, e As EventArgs)

    Sub New()
        Enabled = True
        DoubleBuffered = True
    End Sub

#Region "Properties"
    Private _Min As Integer
    Private _Max As Integer = 100
    Private IsEnabled As Boolean
    Public Property UpDownStep As Integer = 1
    Private _Value As Integer

    Public Property Value As Integer
        Get
            Return _Value
        End Get
        Set(value As Integer)
            Select Case value
                Case Is > Max
                    value = Max
                    Invalidate()

                Case Is < Min
                    value = Min
                    Invalidate()
            End Select
            _Value = value
            Invalidate()
            RaiseEvent ValueChanged(Me, EventArgs.Empty)
        End Set
    End Property

    Public Property Max As Integer
        Get
            Return _Max
        End Get
        Set(value As Integer)
            Select Case value
                Case Is < _Value
                    _Value = value
            End Select
            _Max = value
            Invalidate()
        End Set
    End Property

    Public Property Min As Integer
        Get
            Return _Min
        End Get
        Set(value As Integer)
            Select Case value
                Case Is > _Value
                    _Value = value
            End Select
            _Min = value
            Invalidate()
        End Set
    End Property

    Public Shadows Property Enabled As Boolean
        Get
            Return EnabledCalc
        End Get
        Set(value As Boolean)
            IsEnabled = value
            Invalidate()
        End Set
    End Property

    <DisplayName("Enabled")>
    Public Property EnabledCalc As Boolean
        Get
            Return IsEnabled
        End Get
        Set(value As Boolean)
            Enabled = value
            Invalidate()
        End Set
    End Property
#End Region

#Region "Animator"
    Dim alpha As Integer
    ReadOnly Factor As Integer = 20
    Dim WithEvents Tmr As New Timer With {.Enabled = False, .Interval = 1}

    Private Sub Tmr_Tick(sender As Object, e As EventArgs) Handles Tmr.Tick
        If Not DesignMode Then

            If State = MouseState.Over Then
                If alpha + Factor <= 255 Then
                    alpha += Factor
                ElseIf alpha + Factor > 255 Then
                    alpha = 255
                    Tmr.Enabled = False
                    Tmr.Stop()
                End If

                If _Shown Then
                    Threading.Thread.Sleep(1)
                    Invalidate()
                End If
            End If

            If Not State = MouseState.Over Then
                If alpha - Factor >= 0 Then
                    alpha -= Factor
                ElseIf alpha - Factor < 0 Then
                    alpha = 0
                    Tmr.Enabled = False
                    Tmr.Stop()
                End If

                If _Shown Then
                    Threading.Thread.Sleep(1)
                    Invalidate()
                End If
            End If
        End If
    End Sub
#End Region

#Region "Events"
    Protected Overrides Sub OnMouseEnter(ByVal e As EventArgs)
        MyBase.OnMouseEnter(e)
        State = MouseState.Over
        _Shown = True
        Tmr.Enabled = True
        Tmr.Start()
        Invalidate()
    End Sub

    Protected Overrides Sub OnMouseLeave(ByVal e As EventArgs)
        MyBase.OnMouseLeave(e)
        State = MouseState.None
        _Shown = True
        Tmr.Enabled = True
        Tmr.Start()
        Invalidate()
    End Sub
    Protected Overrides Sub OnMouseUp(e As MouseEventArgs)
        State = MouseState.Over
        _Shown = True
        Tmr.Enabled = True
        Tmr.Start()

        MyBase.OnMouseUp(e)

        If Enabled Then
            If SideRect.Contains(e.Location) And e.Y < 10 Then
                Value += UpDownStep
            ElseIf SideRect.Contains(e.Location) And e.Y > 10 Then
                Value -= UpDownStep
            End If
        End If
    End Sub

    Private Sub XenonNumericUpDown_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
        State = MouseState.Down
        _Shown = True

        If Enabled And SideRect.Contains(e.Location) Then
            Tmr.Enabled = True
            Tmr.Start()
        End If
    End Sub

    Protected Overrides Sub OnResize(e As EventArgs)
        Me.Invalidate()
    End Sub

    Enum MouseState
        None
        Over
        Down
    End Enum

    Dim State As MouseState = MouseState.None

    Private Sub XenonNumericUpDown_HandleCreated(sender As Object, e As EventArgs) Handles Me.HandleCreated
        alpha = 0

        If Not DesignMode Then
            Try
                AddHandler FindForm.Load, AddressOf Loaded
                AddHandler FindForm.Shown, AddressOf Showed
                AddHandler Parent.BackColorChanged, AddressOf RefreshColorPalette
            Catch
            End Try
        End If
    End Sub

    Sub Loaded()
        _Shown = False
    End Sub

    Sub Showed()
        _Shown = True

        Invalidate()
    End Sub

    Public Sub RefreshColorPalette()
        If _Shown Then

            Invalidate()
        End If
    End Sub

    Private _Shown As Boolean = False
#End Region

    Dim SideRect As New Rectangle

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        Dim G As Graphics = e.Graphics
        G.SmoothingMode = SmoothingMode.AntiAlias
        G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit
        DoubleBuffered = True
        Dim RTL As Boolean = (RightToLeft = 1)

        '################################################################################# Customizer
        Dim OuterRect As New Rectangle(0, 0, Width - 1, Height - 1)
        Dim InnerRect As New Rectangle(1, 1, Width - 3, Height - 3)
        SideRect = New Rectangle(Width - 16, 0, 15, Height)

        If RTL Then
            OuterRect.X = Width - OuterRect.X - OuterRect.Width
            InnerRect.X = Width - InnerRect.X - InnerRect.Width
            SideRect.X = Width - SideRect.X - SideRect.Width
        End If

        '#################################################################################

        G.Clear(GetParentColor)

        G.FillRoundedRect(New SolidBrush(Color.FromArgb(255 - alpha, Style.Colors.Back)), OuterRect)
        G.FillRoundedRect(New SolidBrush(Color.FromArgb(alpha, Style.Colors.Back_Checked)), OuterRect)
        G.FillRoundedRect(New SolidBrush(Color.FromArgb(alpha, Style.Colors.Border_Checked_Hover)), SideRect)

        G.DrawRoundedRect_LikeW11(New Pen(Color.FromArgb(255 - alpha, Style.Colors.Border)), InnerRect)
        G.DrawRoundedRect_LikeW11(New Pen(Color.FromArgb(alpha, Style.Colors.Border_Checked_Hover)), OuterRect)

        If Focused And State = MouseState.None Then G.DrawRoundedRect(New Pen(Color.FromArgb(255, Style.Colors.Border_Checked_Hover)), InnerRect)

        Using TextColor As New SolidBrush(If(GetDarkMode(), Color.White, Color.Black)), TextFont As New Font("Segoe UI", 9)
            G.DrawString(CStr(Value), TextFont, New SolidBrush(TextColor.Color), New Rectangle(0, 0, Width - 15, Height), StringAligner(ContentAlignment.MiddleCenter))
        End Using

        Using SignColor As New SolidBrush(Style.Colors.Back_Checked), SignFont As New Font("Marlett", 11)
            G.DrawString("t", SignFont, SignColor, New Point(SideRect.Left - 1, 0))
            G.DrawString("u", SignFont, SignColor, New Point(SideRect.Left - 1, Height - 16))
        End Using
    End Sub

End Class
Public Class XenonSeparatorVertical
    Inherits Control
    Private G As Graphics

    Sub New()
        TabStop = False
        DoubleBuffered = True
        Text = ""
    End Sub

#Region "Events"

    Protected Overrides Sub OnResize(e As EventArgs)
        MyBase.OnResize(e)
        Size = New Size(2, Height)
    End Sub

#End Region

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        G = e.Graphics
        G.SmoothingMode = SmoothingMode.AntiAlias
        DoubleBuffered = True
        MyBase.OnPaint(e)

        Dim clr As Color
        clr = Color.FromArgb(75, 75, 75)

        '################################################################################# Customizer
        Dim IdleLine As Color
        If Parent IsNot Nothing Then
            If GetDarkMode() Then IdleLine = Parent.BackColor.CB(0.1) Else IdleLine = Parent.BackColor.CB(-0.1)
        Else
            If GetDarkMode() Then IdleLine = Color.FromArgb(60, 60, 60) Else IdleLine = Color.FromArgb(210, 210, 210)
        End If
        '################################################################################# Customizer

        Using C As New Pen(IdleLine) : G.DrawLine(C, New Point(0, 0), New Point(0, Height)) : End Using

    End Sub

End Class
<DefaultEvent("TextChanged")> Public Class XenonTextBox : Inherits Control
    Sub New()
        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint Or
                 ControlStyles.ResizeRedraw Or ControlStyles.OptimizedDoubleBuffer Or ControlStyles.SupportsTransparentBackColor, True)
        DoubleBuffered = True

        ForeColor = Color.White

        TB = New Windows.Forms.TextBox
        If GetDarkMode() Then BackColor = Color.FromArgb(55, 55, 55) Else BackColor = Color.FromArgb(225, 225, 225)
        If GetDarkMode() Then TB.BackColor = Color.FromArgb(55, 55, 55) Else TB.BackColor = Color.FromArgb(225, 225, 225)
        TB.Font = New Font("Segoe UI", 9)
        TB.Text = Text
        TB.ForeColor = Color.White
        TB.MaxLength = _MaxLength
        TB.Multiline = _Multiline
        TB.ReadOnly = _ReadOnly
        TB.UseSystemPasswordChar = _UseSystemPasswordChar
        TB.BorderStyle = BorderStyle.None
        TB.Location = New Point(1, 0)
        TB.Width = Width - 3
        TB.Cursor = Cursors.IBeam

        TB.ScrollBars = Scrollbars
        TB.WordWrap = WordWrap

        If _Multiline Then
            TB.Height = Height - 8
        Else
            Height = TB.Height + 8
        End If

        AddHandler TB.TextChanged, AddressOf OnBaseTextChanged
        AddHandler TB.KeyDown, AddressOf OnBaseKeyDown
    End Sub

#Region "Variables"

    Private State As MouseState = MouseState.None
    Private WithEvents TB As TextBox

    Enum MouseState As Byte
        None = 0
        Over = 1
        Down = 2
        Block = 3
    End Enum

#End Region

#Region "Properties"

#Region "TextBox Properties"
    Private _TextAlign As HorizontalAlignment = HorizontalAlignment.Left

    <Category("Options")>
    Property TextAlign() As HorizontalAlignment
        Get
            Return _TextAlign
        End Get
        Set(ByVal value As HorizontalAlignment)
            _TextAlign = value
            If TB IsNot Nothing Then
                TB.TextAlign = value
            End If
        End Set
    End Property

    Private _MaxLength As Integer = 32767

    <Category("Options")>
    Property MaxLength() As Integer
        Get
            Return _MaxLength
        End Get
        Set(ByVal value As Integer)
            _MaxLength = value
            If TB IsNot Nothing Then
                TB.MaxLength = value
            End If
        End Set
    End Property

    Private _ReadOnly As Boolean

    <Category("Options")>
    Property [ReadOnly]() As Boolean
        Get
            Return _ReadOnly
        End Get
        Set(ByVal value As Boolean)
            _ReadOnly = value
            If TB IsNot Nothing Then
                TB.ReadOnly = value
            End If
        End Set
    End Property

    Private _UseSystemPasswordChar As Boolean

    <Category("Options")>
    Property UseSystemPasswordChar() As Boolean
        Get
            Return _UseSystemPasswordChar
        End Get
        Set(ByVal value As Boolean)
            _UseSystemPasswordChar = value
            If TB IsNot Nothing Then
                TB.UseSystemPasswordChar = value
            End If
        End Set
    End Property

    Private _Multiline As Boolean

    <Category("Options")>
    Property Multiline() As Boolean
        Get
            Return _Multiline
        End Get
        Set(ByVal value As Boolean)
            _Multiline = value
            If TB IsNot Nothing Then
                TB.Multiline = value

                If value Then
                    TB.Height = Height - 8
                Else
                    Height = TB.Height + 8
                End If

            End If
        End Set
    End Property

    <Category("Options")>
    Overrides Property Text As String
        Get
            Return MyBase.Text
        End Get
        Set(ByVal value As String)
            MyBase.Text = value
            If TB IsNot Nothing Then
                TB.Text = value
            End If
        End Set
    End Property

    <Category("Options")>
    Overrides Property Font As Font
        Get
            Return MyBase.Font
        End Get
        Set(ByVal value As Font)
            MyBase.Font = value
            If TB IsNot Nothing Then
                TB.Font = value
                TB.Location = New Point(3, 4)
                TB.Width = Width - 6

                If Not _Multiline Then
                    Height = TB.Height + 8
                End If
            End If
        End Set
    End Property

    Protected Overrides Sub OnCreateControl()
        MyBase.OnCreateControl()
        If Not Controls.Contains(TB) Then
            Controls.Add(TB)
        End If
    End Sub

    Private Sub OnBaseTextChanged(ByVal s As Object, ByVal e As EventArgs)
        Text = TB.Text
    End Sub

    Private Sub OnBaseKeyDown(ByVal s As Object, ByVal e As KeyEventArgs)
        If e.Control AndAlso e.KeyCode = Keys.A Then
            TB.SelectAll()
            e.SuppressKeyPress = True
        End If
        If e.Control AndAlso e.KeyCode = Keys.C Then
            TB.Copy()
            e.SuppressKeyPress = True
        End If
    End Sub

    Protected Overrides Sub OnResize(ByVal e As EventArgs)
        TB.Location = New Point(4, 4)
        TB.Width = Width - 14

        If _Multiline Then
            TB.Height = Height - 8
        Else
            Height = TB.Height + 8
        End If

        MyBase.OnResize(e)
    End Sub
#End Region

#Region "Other Properties"


    Private _Scrollbars As Windows.Forms.ScrollBars = ScrollBars.None
    Public Property Scrollbars As Windows.Forms.ScrollBars
        Get
            Return _Scrollbars
        End Get
        Set(value As Windows.Forms.ScrollBars)
            _Scrollbars = value
            TB.ScrollBars = value
        End Set
    End Property

    Private _WordWrap As Boolean = True
    Public Property WordWrap As Boolean
        Get
            Return _WordWrap
        End Get
        Set(value As Boolean)
            _WordWrap = value
            TB.WordWrap = value
        End Set
    End Property


    Public Property SelectionStart As Integer
        Get
            Return TB.SelectionStart
        End Get
        Set(value As Integer)
            TB.SelectionStart = CInt(value)
        End Set
    End Property

    Public Property SelectionLength As Integer
        Get
            Return TB.SelectionLength
        End Get
        Set(value As Integer)
            TB.SelectionLength = CInt(value)
        End Set
    End Property

    Public Property SelectedText As String
        Get
            Return TB.SelectedText
        End Get
        Set(value As String)
            TB.SelectedText = CStr(value)
        End Set
    End Property
#End Region

#End Region

#Region "Events"
    Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
        MyBase.OnMouseDown(e)
        State = MouseState.Down
        _Shown = True
        Tmr.Enabled = True
        Tmr.Start()
        Invalidate()
    End Sub

    Protected Overrides Sub OnMouseUp(e As MouseEventArgs)
        MyBase.OnMouseUp(e)
        State = MouseState.Over
        _Shown = True
        Tmr.Enabled = True
        Tmr.Start()
        TB.Focus() : Invalidate()
    End Sub

    Protected Overrides Sub OnMouseEnter(ByVal e As EventArgs)
        MyBase.OnMouseEnter(e)
        State = MouseState.Over
        _Shown = True
        Tmr.Enabled = True
        Tmr.Start()
        Invalidate()
    End Sub

    Protected Overrides Sub OnMouseLeave(ByVal e As EventArgs)
        MyBase.OnMouseLeave(e)
        State = MouseState.None
        _Shown = True
        Tmr.Enabled = True
        Tmr.Start()
        Invalidate()
    End Sub

#End Region

#Region "Animator"
    Dim alpha As Integer
    ReadOnly Factor As Integer = 20
    Dim WithEvents Tmr As New Timer With {.Enabled = False, .Interval = 1}

    Private Sub Tmr_Tick(sender As Object, e As EventArgs) Handles Tmr.Tick
        If Not DesignMode Then

            If State = MouseState.Over Then
                If alpha + Factor <= 255 Then
                    alpha += Factor
                ElseIf alpha + Factor > 255 Then
                    alpha = 255
                    Tmr.Enabled = False
                    Tmr.Stop()
                End If

                If _Shown Then
                    Threading.Thread.Sleep(1)
                    Invalidate()
                End If
            End If

            If Not State = MouseState.Over Then
                If alpha - Factor >= 0 Then
                    alpha -= Factor
                ElseIf alpha - Factor < 0 Then
                    alpha = 0
                    Tmr.Enabled = False
                    Tmr.Stop()
                End If

                If _Shown Then
                    Threading.Thread.Sleep(1)
                    Invalidate()
                End If
            End If
        End If
    End Sub
#End Region

    Private Const EM_SETCUEBANNER As Integer = &H1501
    <DllImport("user32.dll", CharSet:=CharSet.Auto)>
    Private Shared Function SendMessage(ByVal hWnd As IntPtr, ByVal msg As Integer,
        ByVal wParam As Integer, ByVal lParam As String) As Int32
    End Function

    Protected Overrides Sub OnHandleCreated(e As EventArgs)
        Try
            If Not DesignMode Then
                MyBase.OnHandleCreated(e)
                If Not String.IsNullOrEmpty(Hint) Then UpdateHint()
                alpha = 0
                If Not DesignMode Then
                    Try
                        AddHandler FindForm.Load, AddressOf Loaded
                        AddHandler FindForm.Shown, AddressOf Showed
                    Catch
                    End Try
                End If
            End If
        Catch
        End Try
    End Sub

    Private m_Hint As String
    Public Property Hint As String
        Get
            Return m_Hint
        End Get
        Set(ByVal value As String)
            m_Hint = value
            UpdateHint()
        End Set
    End Property

    Private Sub UpdateHint()
        SendMessage(TB.Handle, EM_SETCUEBANNER, 1, Hint)
    End Sub


    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        Dim G As Graphics = e.Graphics
        DoubleBuffered = True
        G.SmoothingMode = SmoothingMode.AntiAlias
        MyBase.OnPaint(e)

        If GetDarkMode() Then
            If ForeColor <> Color.White Then ForeColor = Color.White
        Else
            If ForeColor <> Color.Black Then ForeColor = Color.Black
        End If


        Dim OuterRect As New Rectangle(0, 0, Width - 1, Height - 1)
        Dim InnerRect As New Rectangle(1, 1, Width - 3, Height - 3)

        Dim ParentColor As Color = GetParentColor
        Dim LineNone, LineHovered As Color
        Dim BackNone, BackHovered As Color

        LineNone = If(GetDarkMode(), ParentColor.Light(0.3), ParentColor.Light(0.05))
        LineHovered = Style.Colors.Border_Checked_Hover

        BackNone = If(GetDarkMode(), ParentColor.Light(0.05), ParentColor.Light(0.3))
        BackHovered = Style.Colors.Back_Checked

        Dim FadeInColor As Color = Color.FromArgb(alpha, LineHovered)
        Dim FadeOutColor As Color = Color.FromArgb(255 - alpha, LineNone)

        G.Clear(GetParentColor)

        TB.ForeColor = ForeColor

        If TB.Focused Or Focused Then
            G.FillRoundedRect(New SolidBrush(BackHovered), OuterRect)
            G.DrawRoundedRect_LikeW11(New Pen(LineHovered), OuterRect)
            TB.BackColor = BackHovered
        Else
            G.FillRoundedRect(New SolidBrush(BackNone), InnerRect)
            G.FillRoundedRect(New SolidBrush(Color.FromArgb(alpha, BackNone)), OuterRect)
            G.DrawRoundedRect_LikeW11(New Pen(FadeInColor), OuterRect)
            G.DrawRoundedRect_LikeW11(New Pen(FadeOutColor), InnerRect)
            TB.BackColor = BackNone
        End If


    End Sub

    Private Sub TB_MouseDown(sender As Object, e As MouseEventArgs) Handles TB.MouseDown
        State = MouseState.Down
        _Shown = True
        Tmr.Enabled = True
        Tmr.Start()
        Invalidate()
    End Sub

    Private Sub TB_MouseEnter(sender As Object, e As EventArgs) Handles TB.MouseEnter, TB.MouseUp
        State = MouseState.Over
        _Shown = True
        Tmr.Enabled = True
        Tmr.Start()
        Invalidate()
    End Sub

    Private Sub TB_MouseLeave(sender As Object, e As EventArgs) Handles TB.MouseLeave
        State = MouseState.None
        _Shown = True
        Tmr.Enabled = True
        Tmr.Start()
        Invalidate()
    End Sub

    Private Sub TB_LostFocus(sender As Object, e As EventArgs) Handles TB.LostFocus
        State = MouseState.None
        Invalidate()
    End Sub

    Private Sub XenonTextBox_HandleCreated(sender As Object, e As EventArgs) Handles Me.HandleCreated
        alpha = 0
        If Not DesignMode Then
            Try
                AddHandler FindForm.Load, AddressOf Loaded
                AddHandler FindForm.Shown, AddressOf Showed
            Catch
            End Try
        End If
    End Sub

    Private _Shown As Boolean = False

    Sub Loaded()
        _Shown = False
    End Sub

    Sub Showed()
        _Shown = True
    End Sub

End Class
Public Class XenonComboBox : Inherits ComboBox
    Sub New()
        SetStyle(ControlStyles.AllPaintingInWmPaint, True)
        SetStyle(ControlStyles.ResizeRedraw, True)
        SetStyle(ControlStyles.UserPaint, True)
        SetStyle(ControlStyles.DoubleBuffer, True)
        Size = New Size(190, 27)
        DrawMode = DrawMode.OwnerDrawVariable
        ItemHeight = 20

        If GetDarkMode() Then BackColor = Color.FromArgb(55, 55, 55) Else BackColor = Color.FromArgb(225, 225, 225)
        ForeColor = Color.White
        DropDownStyle = ComboBoxStyle.DropDownList
        Font = New Font("Segoe UI", 9)
        DoubleBuffered = True
    End Sub

#Region "Properties"
    Public Property CustomFont As Boolean = False

#End Region

    ReadOnly Noise As New TextureBrush(My.Resources.GaussianBlur.Fade(0.3))

#Region "Subs"
    Sub ReplaceItem(ByVal sender As System.Object, ByVal e As DrawItemEventArgs) Handles Me.DrawItem
        BackColor = Style.Colors.Back
        e.DrawBackground()

        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias
        e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit

        If BackColor.IsDark Then
            If ForeColor <> Color.White Then ForeColor = Color.White
        Else
            If ForeColor <> Color.Black Then ForeColor = Color.Black
        End If

        e.Graphics.FillRectangle(New SolidBrush(BackColor), New Rectangle(e.Bounds.X - 2, e.Bounds.Y - 2, e.Bounds.Width + 4, e.Bounds.Height + 4))

        If (e.State And DrawItemState.Selected) = DrawItemState.Selected Then
            e.Graphics.FillRectangle(New SolidBrush(Style.Colors.Border_Checked_Hover), e.Bounds)
        End If

        Dim f As Font
        If Not CustomFont Then
            f = e.Font
        Else
            Try
                f = New Font(MyBase.GetItemText(MyBase.Items(e.Index)), e.Font.Size, e.Font.Style)
            Catch
                f = e.Font
            End Try
        End If

        Dim Rect As Rectangle = e.Bounds
        Rect.X += 2
        Rect.Width -= 2

        If e.Index >= 0 Then e.Graphics.DrawString(MyBase.GetItemText(MyBase.Items(e.Index)), f, New SolidBrush(ForeColor), Rect, StringAligner(ContentAlignment.MiddleLeft))
    End Sub

    Protected Sub DrawTriangle(ByVal Clr As Color, ByVal FirstPoint As Point, ByVal SecondPoint As Point, ByVal ThirdPoint As Point, ByVal G As Graphics)
        Dim points As New List(Of Point) From {FirstPoint, SecondPoint, ThirdPoint}
        G.FillPolygon(New SolidBrush(Clr), points.ToArray())
    End Sub
#End Region

#Region "Events"
    Enum MouseState
        None
        Over
        Down
    End Enum

    Protected Overrides Sub OnMouseEnter(ByVal e As EventArgs)
        MyBase.OnMouseEnter(e)
        State = MouseState.Over
        _Shown = True
        Tmr.Enabled = True
        Tmr.Start()
        Invalidate()
    End Sub

    Protected Overrides Sub OnMouseLeave(ByVal e As EventArgs)
        MyBase.OnMouseLeave(e)
        State = MouseState.None
        _Shown = True
        Tmr.Enabled = True
        Tmr.Start()
        Invalidate()
    End Sub

    Private Sub XenonComboBox_MouseWheel(sender As Object, e As MouseEventArgs) Handles Me.MouseWheel
        Try
            If e.Delta < 0 Then
                If SelectedIndex < Items.Count - 1 Then
                    If e.Delta <= -240 Then SelectedIndex += 1
                End If
            Else
                If SelectedIndex > 0 Then
                    If e.Delta >= 240 Then SelectedIndex -= 1
                End If
            End If
        Catch
        End Try
    End Sub

    Private Sub XenonComboBox_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
        State = MouseState.Down
        _Shown = True
        Tmr.Enabled = True
        Tmr.Start()
        Invalidate()
    End Sub

    Private Sub XenonComboBox_Click(sender As Object, e As EventArgs) Handles Me.MouseUp
        State = MouseState.Over
        _Shown = True
        Tmr.Enabled = True
        Tmr.Start()
        Invalidate()
    End Sub

    Private _Shown As Boolean = False

    Private Sub XenonComboBox_HandleCreated(sender As Object, e As EventArgs) Handles Me.HandleCreated
        alpha = 0
        alpha2 = 0
        Try
            If Not DesignMode Then
                If Parent IsNot Nothing Then AddHandler Parent.BackColorChanged, AddressOf Invalidate
                AddHandler BackColorChanged, AddressOf Invalidate
            End If
        Catch
        End Try
    End Sub

    Private Sub XenonComboBox_DropDown(sender As Object, e As EventArgs) Handles Me.DropDown
        If _Shown Then
            Tmr2.Enabled = True
            Tmr2.Start()
        End If
    End Sub

    Private Sub XenonComboBox_DropDownClosed(sender As Object, e As EventArgs) Handles Me.DropDownClosed
        If _Shown Then
            Tmr2.Enabled = True
            Tmr2.Start()
        End If
    End Sub

    Dim State As MouseState = MouseState.None
#End Region

#Region "Animator"
    Dim alpha, alpha2 As Integer
    ReadOnly Factor As Integer = 20
    Dim WithEvents Tmr, Tmr2 As New Timer With {.Enabled = False, .Interval = 1}

    Private Sub Tmr_Tick(sender As Object, e As EventArgs) Handles Tmr.Tick
        If Not DesignMode Then

            If State = MouseState.Over Then
                If alpha + Factor <= 255 Then
                    alpha += Factor
                ElseIf alpha + Factor > 255 Then
                    alpha = 255
                    Tmr.Enabled = False
                    Tmr.Stop()
                End If

                If _Shown Then
                    Threading.Thread.Sleep(1)
                    Invalidate()
                End If
            End If

            If Not State = MouseState.Over Then
                If alpha - Factor >= 0 Then
                    alpha -= Factor
                ElseIf alpha - Factor < 0 Then
                    alpha = 0
                    Tmr.Enabled = False
                    Tmr.Stop()
                End If

                If _Shown Then
                    Threading.Thread.Sleep(1)
                    Invalidate()
                End If
            End If
        End If
    End Sub

    Private Sub Tmr2_Tick(sender As Object, e As EventArgs) Handles Tmr2.Tick
        If Not DesignMode Then

            If DroppedDown Then
                If alpha2 + Factor <= 255 Then
                    alpha2 += Factor
                ElseIf alpha2 + Factor > 255 Then
                    alpha2 = 255
                    Tmr2.Enabled = False
                    Tmr2.Stop()
                End If

                If _Shown Then
                    Threading.Thread.Sleep(1)
                    Invalidate()
                End If
            End If

            If Not DroppedDown Then
                If alpha2 - Factor >= 0 Then
                    alpha2 -= Factor
                ElseIf alpha2 - Factor < 0 Then
                    alpha2 = 0
                    Tmr2.Enabled = False
                    Tmr2.Stop()
                End If

                If _Shown Then
                    Threading.Thread.Sleep(1)
                    Invalidate()
                End If
            End If
        End If
    End Sub
#End Region

    Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
        Dim G As Graphics = e.Graphics
        G.SmoothingMode = SmoothingMode.AntiAlias
        G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit
        DoubleBuffered = True

        If GetDarkMode() Then ForeColor = Color.White Else ForeColor = Color.Black
        Dim OuterRect As New Rectangle(0, 0, Width - 1, Height - 1)
        Dim InnerRect As New Rectangle(1, 1, Width - 3, Height - 3)
        Dim TextRect As New Rectangle(5, 0, Width - 1, Height - 1)

        Dim FadeInColor As Color = Color.FromArgb(alpha, Style.Colors.Border_Checked_Hover)
        Dim FadeOutColor As Color = Color.FromArgb(255 - alpha, Style.Colors.Border)

        G.Clear(GetParentColor)

        G.FillRoundedRect(New SolidBrush(Style.Colors.Back), InnerRect)
        G.FillRoundedRect(New SolidBrush(Color.FromArgb(alpha, Style.Colors.Back_Checked)), OuterRect)
        G.FillRoundedRect(Noise, InnerRect)

        G.DrawRoundedRect_LikeW11(New Pen(FadeInColor), OuterRect)
        G.DrawRoundedRect_LikeW11(New Pen(FadeOutColor), InnerRect)

        G.FillRoundedRect(New SolidBrush(Color.FromArgb(alpha2, Style.Colors.Back_Checked)), OuterRect)
        G.DrawRoundedRect_LikeW11(New Pen(Color.FromArgb(alpha2, Style.Colors.Border_Checked_Hover)), OuterRect)

        Dim ArrowHeight As Integer = 4
        Dim Arrow_Y_1 As Integer = (Height - ArrowHeight) / 2 - 1
        Dim Arrow_Y_2 As Integer = Arrow_Y_1 + ArrowHeight

        If Focused And State = MouseState.None Then
            G.DrawRoundedRect(New Pen(Color.FromArgb(255, FadeInColor)), InnerRect)
            G.DrawLine(New Pen(Color.FromArgb(255, FadeInColor), 2), New Point(Width - 18, Arrow_Y_1), New Point(Width - 14, Arrow_Y_2))
            G.DrawLine(New Pen(Color.FromArgb(255, FadeInColor), 2), New Point(Width - 14, Arrow_Y_2), New Point(Width - 10, Arrow_Y_1))
            G.DrawLine(New Pen(Color.FromArgb(255, FadeInColor)), New Point(Width - 14, Arrow_Y_2 + 1), New Point(Width - 14, Arrow_Y_2))
        Else
            G.DrawLine(New Pen(Color.FromArgb(255 - alpha, ForeColor), 2), New Point(Width - 18, Arrow_Y_1), New Point(Width - 14, Arrow_Y_2))
            G.DrawLine(New Pen(Color.FromArgb(255 - alpha, ForeColor), 2), New Point(Width - 14, Arrow_Y_2), New Point(Width - 10, Arrow_Y_1))
            G.DrawLine(New Pen(Color.FromArgb(255 - alpha, ForeColor)), New Point(Width - 14, Arrow_Y_2 + 1), New Point(Width - 14, Arrow_Y_2))

            If Not DroppedDown Then
                G.DrawLine(New Pen(FadeInColor, 2), New Point(Width - 18, Arrow_Y_1), New Point(Width - 14, Arrow_Y_2))
                G.DrawLine(New Pen(FadeInColor, 2), New Point(Width - 14, Arrow_Y_2), New Point(Width - 10, Arrow_Y_1))
                G.DrawLine(New Pen(FadeInColor), New Point(Width - 14, Arrow_Y_2 + 1), New Point(Width - 14, Arrow_Y_2))
            Else
                G.DrawLine(New Pen(FadeInColor), New Point(Width - 14, Arrow_Y_1), New Point(Width - 14, Arrow_Y_1 + 1))
                G.DrawLine(New Pen(FadeInColor, 2), New Point(Width - 18, Arrow_Y_2), New Point(Width - 14, Arrow_Y_1))
                G.DrawLine(New Pen(FadeInColor, 2), New Point(Width - 14, Arrow_Y_1), New Point(Width - 10, Arrow_Y_2))
            End If

        End If

        Dim f As Font
        If Not CustomFont Then
            f = Font
        Else
            Try
                f = New Font(Text, Font.Size, Font.Style)
            Catch
                f = Font
            End Try
        End If

        G.DrawString(Text, f, New SolidBrush(ForeColor), TextRect, New StringFormat With {.LineAlignment = StringAlignment.Center, .Alignment = StringAlignment.Near})
    End Sub
End Class
Public Class XenonAlertBox
    Inherits ContainerControl

    Private exitLocation As Point
    Private overExit As Boolean
    Private borderColor, innerColor, textColor As Color

    Sub New()
        TabStop = False
        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.OptimizedDoubleBuffer Or ControlStyles.UserPaint Or ControlStyles.ResizeRedraw, True)
        DoubleBuffered = True
        Font = New Font("Segoe UI", 9)
        Size = New Size(200, 40)
        CanClose = Close.No
        CenterText = False
        CustomColor = Color.FromArgb(0, 81, 210)
    End Sub

#Region "Properties"
    Enum Style
        Adaptive
        Simple
        Success
        Notice
        Warning
        Informations
        Indigo
        Custom
    End Enum

    Enum Close
        Yes
        No
    End Enum

    Public Property AlertStyle As Style
        Get
            Return _alertStyle
        End Get
        Set(ByVal value As Style)
            _alertStyle = value
            Invalidate()
        End Set
    End Property

    Public Property CanClose As Close
        Get
            Return _Close
        End Get
        Set(ByVal value As Close)
            _Close = value
            Invalidate()
        End Set
    End Property

    Private _alertStyle As Style
    Private _Close As Close

    Public Property Image As Image
    Public Property CustomColor As Color
    Public Property CenterText As Boolean = False
#End Region

#Region "Events"
    Protected Overrides Sub OnMouseEnter(ByVal e As EventArgs)
        MyBase.OnMouseEnter(e)
    End Sub
    Protected Overrides Sub OnMouseLeave(ByVal e As EventArgs)
        MyBase.OnMouseLeave(e)
    End Sub
    Protected Overrides Sub OnMouseMove(ByVal e As System.Windows.Forms.MouseEventArgs)
        MyBase.OnMouseMove(e)

        If e.X >= Width - 26 AndAlso e.X <= Width - 12 AndAlso e.Y > exitLocation.Y AndAlso e.Y < exitLocation.Y + 12 Then
            If CanClose = Close.Yes And Cursor <> Cursors.Hand Then Cursor = Cursors.Hand
            overExit = True
        Else
            If Cursor <> Cursors.Arrow Then Cursor = Cursors.Arrow
            overExit = False
        End If

        Invalidate()
    End Sub
    Protected Overrides Sub OnMouseDown(ByVal e As System.Windows.Forms.MouseEventArgs)
        MyBase.OnMouseDown(e)
        If overExit And CanClose = Close.Yes Then Me.Visible = False
    End Sub
#End Region

    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        MyBase.OnPaint(e)
        Dim G As Graphics = e.Graphics
        G.SmoothingMode = SmoothingMode.AntiAlias
        G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit
        DoubleBuffered = True
        Dim RTL As Boolean = (RightToLeft = 1)
        Dim DM As Boolean = GetDarkMode()

        Select Case _alertStyle
            Case Style.Simple
                If DM Then
                    borderColor = Color.FromArgb(60, 60, 60)
                    innerColor = Color.FromArgb(50, 50, 50)
                    textColor = Color.FromArgb(150, 150, 150)
                Else
                    borderColor = Color.FromArgb(190, 190, 190)
                    innerColor = Color.FromArgb(150, 150, 150)
                    textColor = Color.FromArgb(250, 250, 250)
                End If

            Case Style.Success
                If DM Then
                    borderColor = Color.FromArgb(60, 98, 79)
                    innerColor = Color.FromArgb(60, 85, 79)
                    textColor = Color.FromArgb(35, 169, 110)
                Else
                    borderColor = Color.FromArgb(160, 198, 179)
                    innerColor = Color.FromArgb(140, 170, 155)
                    textColor = Color.FromArgb(135, 255, 210)
                End If

            Case Style.Notice
                If DM Then
                    borderColor = Color.FromArgb(70, 91, 107)
                    innerColor = Color.FromArgb(70, 91, 94)
                    textColor = Color.FromArgb(97, 185, 186)
                Else
                    borderColor = Color.FromArgb(170, 191, 207)
                    innerColor = Color.FromArgb(130, 155, 155)
                    textColor = Color.FromArgb(180, 255, 255)
                End If

            Case Style.Warning
                If DM Then
                    borderColor = Color.FromArgb(100, 71, 71)
                    innerColor = Color.FromArgb(87, 71, 71)
                    textColor = Color.FromArgb(254, 142, 122)
                Else
                    borderColor = Color.FromArgb(200, 171, 171)
                    innerColor = Color.FromArgb(150, 75, 75)
                    textColor = Color.FromArgb(255, 175, 175)
                End If

            Case Style.Informations
                If DM Then
                    borderColor = Color.FromArgb(133, 133, 71)
                    innerColor = Color.FromArgb(120, 120, 71)
                    textColor = Color.FromArgb(254, 224, 122)
                Else
                    borderColor = Color.FromArgb(233, 233, 171)
                    innerColor = Color.FromArgb(195, 195, 150)
                    textColor = Color.FromArgb(250, 250, 150)
                End If


            Case Style.Indigo
                If DM Then
                    borderColor = Color.FromArgb(65, 0, 170)
                    innerColor = Color.FromArgb(60, 0, 140)
                    textColor = Color.FromArgb(140, 0, 255).CB(0.35)
                Else
                    borderColor = Color.FromArgb(165, 0, 225)
                    innerColor = Color.FromArgb(129, 0, 200)
                    textColor = Color.FromArgb(210, 110, 255)
                End If

            Case Style.Custom

                If DM Then
                    borderColor = CustomColor.CB(0.03)
                    innerColor = CustomColor.CB(0.01)
                    textColor = CustomColor.LightLight
                Else
                    borderColor = CustomColor.CB(0.3)
                    innerColor = CustomColor.CB(0.1)
                    textColor = CustomColor.CB(0.7)
                End If

            Case Style.Adaptive
                If Image IsNot Nothing Then
                    Dim cc As Color = Image.AverageColor

                    If DM Then
                        borderColor = cc.Light(0.01)
                        innerColor = cc.Dark(0.001)
                        textColor = cc.LightLight
                    Else
                        borderColor = cc.Light(1)
                        innerColor = cc.LightLight.CB(0.35)
                        textColor = cc
                    End If

                Else
                    If DM Then
                        borderColor = CustomColor.CB(0.03)
                        innerColor = CustomColor.CB(0.01)
                        textColor = CustomColor.LightLight
                    Else
                        borderColor = CustomColor.CB(0.3)
                        innerColor = CustomColor.CB(0.1)
                        textColor = CustomColor.CB(0.7)
                    End If
                End If

        End Select

        G.Clear(GetParentColor)

        BackColor = innerColor

        G.FillRoundedRect(New SolidBrush(innerColor), New Rectangle(0, 0, Width - 1, Height - 1))
        G.DrawRoundedRect_LikeW11(New Pen(borderColor), New Rectangle(0, 0, Width - 1, Height - 1))

        Dim textY As Integer = CInt((Height - Text.Measure(Font).Height) / 2)
        Dim TextX As Integer
        Dim ExitX As Integer

        If Image IsNot Nothing Then G.DrawImage(Image, New Rectangle(If(Not RTL, 5, Width - 5 - Image.Width), 5, Image.Width, Image.Height))

        If Not CenterText Then
            If Image Is Nothing Then
                G.DrawString(Text, Font, New SolidBrush(textColor), New Point(TextX, textY), If(RTL, New StringFormat(StringFormatFlags.DirectionRightToLeft), New StringFormat()))
            Else
                If Not RTL Then
                    G.DrawString(Text, Font, New SolidBrush(textColor), New Rectangle(10 + Image.Width, 7, Width - (5 + Image.Width), Height - 10), StringAligner(ContentAlignment.TopLeft))
                Else
                    G.DrawString(Text, Font, New SolidBrush(textColor), New Rectangle(0, 7, Width - (10 + Image.Width), Height - 10), StringAligner(ContentAlignment.TopLeft, RTL))
                End If
            End If
        Else
            G.DrawString(Text, Font, New SolidBrush(textColor), New Rectangle(1, 0, Width, Height), StringAligner(ContentAlignment.MiddleCenter, RTL))
        End If

        If CanClose = Close.Yes Then
            Dim exitFont As New Font("Marlett", 6)
            Dim exitY As Integer = CInt(((Me.Height - 1) / 2) - (G.MeasureString("r", exitFont).Height / 2) + 1)
            exitLocation = New Point(ExitX, exitY - 3)
            G.DrawString("r", exitFont, New SolidBrush(textColor), New Point(Width - 23, exitY))
        End If

    End Sub

End Class
Public Class XenonAcrylic : Inherits ContainerControl : Implements INotifyPropertyChanged

    Dim Noise As New TextureBrush(My.Resources.GaussianBlur.Fade(0.15))

    Dim Noise7 As Bitmap = My.Resources.AeroGlass
    Dim Noise7Start As Bitmap = My.Resources.Start7Glass

    Dim adaptedBack As Bitmap
    Dim adaptedBackBlurred As Bitmap
    Private _Transparency As Boolean = True
    Public Property Radius As Integer = 5
    Public Property Basic As Boolean = False
    Public Property Borders As Boolean = True
    Public Property UseItAsStartMenu As Boolean = False
    Public Property UseItAsTaskbar As Boolean = False
    Public Property UseItAsTaskbar_Version As TaskbarVersion = TaskbarVersion.Eleven
    Public Property UseItAsActionCenter As Boolean = False
    Public Property DropShadow As Boolean = True
    Public Property RoundedCorners As Boolean = False

    Private _BackColorAlpha As Byte = 130
    Public Event BackColorAlphaChanged As PropertyChangedEventHandler
    Private Sub NotifyBackColorAlphaChanged(ByVal info As Integer)
        Invalidate()
        RaiseEvent BackColorAlphaChanged(Me, New PropertyChangedEventArgs(info))
    End Sub
    Public Property BackColorAlpha() As Integer
        Get
            Return _BackColorAlpha
        End Get

        Set(ByVal value As Integer)
            If Not (value = _BackColorAlpha) Then
                Me._BackColorAlpha = value
                NotifyBackColorAlphaChanged(_BackColorAlpha)
            End If
        End Set
    End Property


    Private _NoisePower As Single = 0.15
    Public Event NoisePowerChanged As PropertyChangedEventHandler
    Private Sub NotifyNoisePowerChanged(ByVal info As Single)
        Invalidate()
        RaiseEvent NoisePowerChanged(Me, New PropertyChangedEventArgs(info))
    End Sub
    Public Property NoisePower() As Single
        Get
            Return _NoisePower
        End Get

        Set(ByVal value As Single)
            If Not (value = _NoisePower) Then
                Me._NoisePower = value
                NotifyNoisePowerChanged(_NoisePower)
                If UseItAsTaskbar_Version = TaskbarVersion.Seven Then
                    Try : Noise7 = My.Resources.AeroGlass.Fade(NoisePower / 100) : Catch : End Try
                    Try : Noise7Start = My.Resources.Start7Glass.Fade(NoisePower / 100) : Catch : End Try
                End If
            End If
        End Set
    End Property

    Private Sub NotifyBlurPowerChanged(ByVal info As String)
        RaiseEvent BlurPowerChanged(Me, New PropertyChangedEventArgs(info))
    End Sub

    Private _BlurPower As Integer = 8
    Public Event BlurPowerChanged As PropertyChangedEventHandler
    Public Property BlurPower() As Integer
        Get
            Return _BlurPower
        End Get

        Set(ByVal value As Integer)
            If Not (value = _BlurPower) Then
                Me._BlurPower = value
                NotifyBlurPowerChanged("BlurPower")
            End If
        End Set
    End Property

    Public Event TransparencyChanged As PropertyChangedEventHandler _
        Implements INotifyPropertyChanged.PropertyChanged
    Private Sub NotifyTransparencyChanged(ByVal info As String)
        RaiseEvent TransparencyChanged(Me, New PropertyChangedEventArgs(info))
    End Sub
    Public Property Transparency() As Boolean
        Get
            Return _Transparency
        End Get

        Set(ByVal value As Boolean)
            If Not (value = _Transparency) Then
                Me._Transparency = value
                NotifyTransparencyChanged("Transparency")
                ProcessBack()
                Refresh()
            End If
        End Set
    End Property


    Private _DarkMode As Boolean = True
    Public Event DarkModeChanged As PropertyChangedEventHandler
    Private Sub NotifyDarkModeChanged(ByVal info As String)
        RaiseEvent DarkModeChanged(Me, New PropertyChangedEventArgs(info))
    End Sub
    Public Property DarkMode() As Boolean
        Get
            Return _DarkMode
        End Get

        Set(ByVal value As Boolean)
            If Not (value = _DarkMode) Then
                Me._DarkMode = value
                NotifyDarkModeChanged("DarkMode")
                Invalidate()
            End If
        End Set
    End Property


    Private _AppUnderline As Color
    Public Event AppUnderlineChanged As PropertyChangedEventHandler
    Private Sub NotifyAppUnderlineChanged(ByVal info As String)
        RaiseEvent AppUnderlineChanged(Me, New PropertyChangedEventArgs(info))
    End Sub
    Public Property AppUnderline() As Color
        Get
            Return _AppUnderline
        End Get

        Set(ByVal value As Color)
            If Not (value = _AppUnderline) Then
                Me._AppUnderline = value
                NotifyAppUnderlineChanged("AppUnderline")
                Try : Refresh() : Catch : End Try
            End If
        End Set
    End Property


    Private _AppBackground As Color
    Public Event AppBackgroundChanged As PropertyChangedEventHandler
    Private Sub NotifyAppBackgroundChanged(ByVal info As String)
        RaiseEvent AppBackgroundChanged(Me, New PropertyChangedEventArgs(info))
    End Sub
    Public Property AppBackground() As Color
        Get
            Return _AppBackground
        End Get

        Set(ByVal value As Color)
            If Not (value = _AppBackground) Then
                Me._AppBackground = value
                NotifyAppBackgroundChanged("AppBackground")
                Try : Refresh() : Catch : End Try
            End If
        End Set
    End Property


    Private _SearchBoxAccent As Color
    Public Event SearchBoxAccentChanged As PropertyChangedEventHandler
    Private Sub NotifySearchBoxAccentChanged(ByVal info As String)
        RaiseEvent SearchBoxAccentChanged(Me, New PropertyChangedEventArgs(info))
    End Sub
    Public Property SearchBoxAccent() As Color
        Get
            Return _SearchBoxAccent
        End Get

        Set(ByVal value As Color)
            If Not (value = _SearchBoxAccent) Then
                Me._SearchBoxAccent = value
                NotifySearchBoxAccentChanged("SearchBoxAccent")
                Try : Refresh() : Catch : End Try
            End If
        End Set
    End Property


    Private _ActionCenterButton_Normal As Color
    Public Event ActionCenterButton_NormalChanged As PropertyChangedEventHandler
    Private Sub NotifyActionCenterButton_NormalChanged(ByVal info As String)
        RaiseEvent ActionCenterButton_NormalChanged(Me, New PropertyChangedEventArgs(info))
    End Sub
    Public Property ActionCenterButton_Normal() As Color
        Get
            Return _ActionCenterButton_Normal
        End Get

        Set(ByVal value As Color)
            If Not (value = _ActionCenterButton_Normal) Then
                Me._ActionCenterButton_Normal = value
                NotifyActionCenterButton_NormalChanged("ActionCenterButton_Normal")
                Try : Refresh() : Catch : End Try
            End If
        End Set
    End Property


    Private _ActionCenterButton_Hover As Color
    Public Event ActionCenterButton_HoverChanged As PropertyChangedEventHandler
    Private Sub NotifyActionCenterButton_HoverChanged(ByVal info As String)
        RaiseEvent ActionCenterButton_HoverChanged(Me, New PropertyChangedEventArgs(info))
    End Sub
    Public Property ActionCenterButton_Hover() As Color
        Get
            Return _ActionCenterButton_Hover
        End Get

        Set(ByVal value As Color)
            If Not (value = _ActionCenterButton_Hover) Then
                Me._ActionCenterButton_Hover = value
                NotifyActionCenterButton_HoverChanged("ActionCenterButton_Hover")
                Try : Refresh() : Catch : End Try
            End If
        End Set
    End Property


    Private _ActionCenterButton_Pressed As Color
    Public Event ActionCenterButton_PressedChanged As PropertyChangedEventHandler
    Private Sub NotifyActionCenterButton_PressedChanged(ByVal info As String)
        RaiseEvent ActionCenterButton_PressedChanged(Me, New PropertyChangedEventArgs(info))
    End Sub
    Public Property ActionCenterButton_Pressed() As Color
        Get
            Return _ActionCenterButton_Pressed
        End Get

        Set(ByVal value As Color)
            If Not (value = _ActionCenterButton_Pressed) Then
                Me._ActionCenterButton_Pressed = value
                NotifyActionCenterButton_PressedChanged("ActionCenterButton_Pressed")
                Try : Refresh() : Catch : End Try
            End If
        End Set
    End Property


    Private _StartColor As Color
    Public Event StartColorChanged As PropertyChangedEventHandler
    Private Sub NotifyStartColorChanged(ByVal info As String)
        RaiseEvent StartColorChanged(Me, New PropertyChangedEventArgs(info))
    End Sub
    Public Property StartColor() As Color
        Get
            Return _StartColor
        End Get

        Set(ByVal value As Color)
            If Not (value = _StartColor) Then
                Me._StartColor = value
                NotifyStartColorChanged("StartColor")
                Refresh()
            End If
        End Set
    End Property

    Private _LinkColor As Color
    Public Event LinkColorChanged As PropertyChangedEventHandler
    Private Sub NotifyLinkColorChanged(ByVal info As String)
        RaiseEvent LinkColorChanged(Me, New PropertyChangedEventArgs(info))
    End Sub
    Public Property LinkColor() As Color
        Get
            Return _LinkColor
        End Get

        Set(ByVal value As Color)
            If Not (value = _LinkColor) Then
                Me._LinkColor = value
                NotifyLinkColorChanged("LinkColor")
                Refresh()
            End If
        End Set
    End Property

    Private _BackColor2 As Color
    Public Event BackColor2Changed As PropertyChangedEventHandler
    Private Sub NotifyBackColor2Changed(ByVal info As String)
        RaiseEvent BackColor2Changed(Me, New PropertyChangedEventArgs(info))
    End Sub
    Public Property BackColor2() As Color
        Get
            Return _BackColor2
        End Get

        Set(ByVal value As Color)
            If Not (value = _BackColor2) Then
                Me._BackColor2 = value
                NotifyBackColor2Changed("BackColor2")
            End If
        End Set
    End Property

    Private _Win7ColorBal As Integer = 0.15
    Public Event Win7ColorBalChanged As PropertyChangedEventHandler
    Private Sub NotifyWin7ColorBalChanged(ByVal info As Integer)
        Invalidate()
        RaiseEvent Win7ColorBalChanged(Me, New PropertyChangedEventArgs(info))
    End Sub
    Public Property Win7ColorBal() As Integer
        Get
            Return _Win7ColorBal
        End Get

        Set(ByVal value As Integer)
            If Not (value = _Win7ColorBal) Then
                Me._Win7ColorBal = value
                NotifyWin7ColorBalChanged(_Win7ColorBal)
            End If
        End Set
    End Property

    Private _Win7GlowBal As Integer = 0.15
    Public Event Win7GlowBalChanged As PropertyChangedEventHandler
    Private Sub NotifyWin7GlowBalChanged(ByVal info As Integer)
        Invalidate()
        RaiseEvent Win7GlowBalChanged(Me, New PropertyChangedEventArgs(info))
    End Sub
    Public Property Win7GlowBal() As Integer
        Get
            Return _Win7GlowBal
        End Get

        Set(ByVal value As Integer)
            If Not (value = _Win7GlowBal) Then
                Me._Win7GlowBal = value
                NotifyWin7GlowBalChanged(_Win7GlowBal)
            End If
        End Set
    End Property

    Enum TaskbarVersion
        Eleven
        Ten
        Eight
        Seven
    End Enum

    Sub New()
        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.OptimizedDoubleBuffer Or ControlStyles.UserPaint Or ControlStyles.ResizeRedraw, True)
    End Sub

    Enum MouseState
        Normal
        Hover
        Pressed
    End Enum

    Private _State_Btn1, _State_Btn2 As MouseState

    Dim Button1 As Rectangle
    Dim Button2 As Rectangle

    Private Sub XenonAcrylic_MouseMove(sender As Object, e As MouseEventArgs) Handles Me.MouseMove, Me.MouseDown, Me.MouseUp
        If UseItAsActionCenter And UseItAsTaskbar_Version = TaskbarVersion.Eleven Then

            If Button1.Contains(PointToClient(MousePosition)) Then
                If e.Button = MouseButtons.None Then _State_Btn1 = MouseState.Hover Else _State_Btn1 = MouseState.Pressed
                Invalidate()
            Else
                If Not _State_Btn1 = MouseState.Normal Then
                    _State_Btn1 = MouseState.Normal
                    Invalidate()
                End If
            End If

            If Button2.Contains(PointToClient(MousePosition)) Then
                If e.Button = MouseButtons.None Then _State_Btn2 = MouseState.Hover Else _State_Btn2 = MouseState.Pressed
                Invalidate()
            Else
                If Not _State_Btn2 = MouseState.Normal Then
                    _State_Btn2 = MouseState.Normal
                    Invalidate()
                End If
            End If

        End If
    End Sub

    Private Sub XenonAcrylic_MouseLeave(sender As Object, e As EventArgs) Handles Me.MouseLeave
        If UseItAsActionCenter And UseItAsTaskbar_Version = TaskbarVersion.Eleven Then
            _State_Btn1 = MouseState.Normal
            _State_Btn2 = MouseState.Normal
            Invalidate()
        End If
    End Sub

    Public Property Win7AeroOpaque As Boolean = False


    Protected Overrides Sub OnPaint(e As System.Windows.Forms.PaintEventArgs)
        Dim G As Graphics = e.Graphics
        G.SmoothingMode = SmoothingMode.AntiAlias
        DoubleBuffered = True

        Dim Rect As New Rectangle(-1, -1, Width + 2, Height + 2)
        Dim RRect As New Rectangle(0, 0, Width, Height)
        G.Clear(Color.Transparent)

        Try
            If RoundedCorners Or UseItAsTaskbar_Version = TaskbarVersion.Eight Then G.DrawImage(adaptedBack, RRect)
        Catch : End Try

        Try
            If Transparency And Not UseItAsTaskbar_Version = TaskbarVersion.Eight Then
                If RoundedCorners Then
                    G.FillRoundedImg(adaptedBackBlurred, RRect, Radius, True)
                Else
                    G.DrawImage(adaptedBackBlurred, Rect)
                End If
            End If
        Catch : End Try


        If RoundedCorners Then

            If UseItAsTaskbar_Version = TaskbarVersion.Seven Then

                If Not Basic Then
                    Dim RestRect As New Rectangle(0, 15, Width, Height - 15)
                    G.DrawImage(adaptedBack.Clone(New Rectangle(0, 0, Width, 16), Imaging.PixelFormat.Format32bppArgb), New Rectangle(0, 0, Width, 16))

                    If Not Win7AeroOpaque Then
                        Dim bk As Bitmap = adaptedBackBlurred

                        Dim alphaX As Single = 1 - BackColorAlpha / 100  'ColorBlurBalance
                        If alphaX < 0 Then alphaX = 0
                        If alphaX > 1 Then alphaX = 1

                        Dim ColBal As Single = Win7ColorBal / 100   'ColorBalance
                        Dim GlowBal As Single = Win7GlowBal / 100   'AfterGlowBalance
                        Dim Color1 As Color = BackColor
                        Dim Color2 As Color = BackColor2

                        G.DrawAeroEffect(RestRect, bk, Color1, ColBal, Color2, GlowBal, alphaX, 3, True)

                    Else
                        G.FillRoundedRect(New SolidBrush(Color.White), RestRect, 3, True)
                        G.FillRoundedRect(New SolidBrush(Color.FromArgb(255 * BackColorAlpha / 100, BackColor)), RestRect, 3, True)
                    End If

                    G.FillRoundedImg(Noise7Start, New Rectangle(0, 0, Width, Height), 3, True)

                    G.FillRoundedImg(My.Resources.Start7, New Rectangle(0, 0, Width, Height), 3, True)

                Else
                    G.FillRoundedImg(My.Resources.Start7Basic, New Rectangle(0, 0, Width, Height), 2, True)
                End If

            Else

                G.FillRoundedRect(New SolidBrush(Color.FromArgb(120, 70, 70, 70)), RRect, Radius, True)
                G.FillRoundedRect(New SolidBrush(Color.FromArgb(If(Transparency, BackColorAlpha, 255), BackColor)), RRect, Radius, True)

                If Transparency And Not UseItAsTaskbar_Version = TaskbarVersion.Eight Then G.FillRoundedRect(Noise, RRect, Radius, True)

                If UseItAsStartMenu Then
                    Dim SearchRect As New Rectangle(7, 10, 120, 18)
                    Dim SearchRectFixer As New Rectangle(7, 21, 120, 5)
                    Dim SearchRectTop As New Rectangle(7, 10, 120, 16)

                    G.FillRoundedImg(If(DarkMode, My.Resources.Start11_Dark, My.Resources.Start11_Light), New Rectangle(0, 0, Width - 1, Height - 1), Radius, True)

                    G.FillRoundedRect(New SolidBrush(SearchBoxAccent), SearchRect, Radius, True)
                    G.FillRoundedRect(New SolidBrush(If(DarkMode, Color.FromArgb(30, 30, 30), Color.FromArgb(230, 230, 230))), SearchRectTop, Radius, True)
                    G.FillRectangle(New SolidBrush(If(DarkMode, Color.FromArgb(30, 30, 30), Color.FromArgb(230, 230, 230))), SearchRectFixer)
                    G.DrawRoundedRect(New Pen(If(DarkMode, Color.FromArgb(50, 50, 50), Color.FromArgb(200, 200, 200))), SearchRect, Radius, True)
                End If

                If UseItAsActionCenter Then
                    Button1 = New Rectangle(8, 8, 49, 20)
                    Button2 = New Rectangle(62, 8, 49, 20)

                    G.FillRoundedImg(If(DarkMode, My.Resources.AC_11_Dark, My.Resources.AC_11_Light), New Rectangle(0, 0, Width - 1, Height - 1), Radius, True)

                    Dim Cx1, Cx2 As Color

                    Select Case _State_Btn1
                        Case MouseState.Normal
                            Cx1 = ActionCenterButton_Normal
                        Case MouseState.Hover
                            Cx1 = ActionCenterButton_Hover
                        Case MouseState.Pressed
                            Cx1 = ActionCenterButton_Pressed
                    End Select

                    Select Case _State_Btn2
                        Case MouseState.Normal
                            Cx2 = If(DarkMode, Color.FromArgb(190, 70, 70, 70), Color.FromArgb(180, 140, 140, 140))
                        Case MouseState.Hover
                            Cx2 = If(DarkMode, Color.FromArgb(190, 90, 90, 90), Color.FromArgb(210, 230, 230, 230))
                        Case MouseState.Pressed
                            Cx2 = If(DarkMode, Color.FromArgb(190, 75, 75, 75), Color.FromArgb(210, 210, 210, 210))
                    End Select

                    G.FillRoundedRect(New SolidBrush(Cx1), Button1, Radius, True)
                    G.DrawRoundedRect_LikeW11(New Pen(Cx1.Light(0.15)), Button1, Radius)

                    G.FillRoundedRect(New SolidBrush(Cx2), Button2, Radius, True)
                    G.DrawRoundedRect(New Pen(Cx2.CB(If(DarkMode, 0.05, -0.05))), Button2, Radius)
                End If

                If Borders Then G.DrawRoundedRect(New Pen(Color.FromArgb(150, 76, 76, 76)), New Rectangle(0, 0, Width - 1, Height - 1), Radius, True)

            End If

        Else

            If Not UseItAsTaskbar_Version = TaskbarVersion.Eight Then
                G.FillRectangle(New SolidBrush(Color.FromArgb(If(Transparency, BackColorAlpha, 255), BackColor)), Rect)
            Else
                Dim c As Color = Color.FromArgb((Win7ColorBal / 100) * 255, BackColor)
                Dim bc As Color = Color.FromArgb(217, 217, 217)

                If Transparency Then
                    G.FillRectangle(New SolidBrush(Color.FromArgb(BackColorAlpha, bc)), Rect)
                    G.FillRectangle(New SolidBrush(Color.FromArgb(BackColorAlpha * (Win7ColorBal / 100), c)), Rect)
                Else
                    G.FillRectangle(New SolidBrush(Color.FromArgb(255, bc)), Rect)
                    G.FillRectangle(New SolidBrush(Color.FromArgb(255 * (Win7ColorBal / 100), c)), Rect)
                End If
            End If

            If Transparency And Not UseItAsTaskbar_Version = TaskbarVersion.Eight Then G.FillRectangle(Noise, Rect)
            If UseItAsStartMenu Then G.DrawImage(If(DarkMode, My.Resources.Start10_Dark, My.Resources.Start10_Light), New Rectangle(0, 0, Width - 1, Height - 1))

            If UseItAsActionCenter Then
                Dim rect1 As New Rectangle(85, 6, 30, 3)
                Dim rect2 As New Rectangle(5, 190, 30, 3)

                Dim rect3 As New Rectangle(42, 201, 34, 24)

                G.FillRectangle(New SolidBrush(ActionCenterButton_Normal), rect3)
                G.DrawImage(If(DarkMode, My.Resources.AC_10_Dark, My.Resources.AC_10_Light), New Rectangle(0, 0, Width - 1, Height - 1))
                G.FillRectangle(New SolidBrush(LinkColor), rect1)
                G.FillRectangle(New SolidBrush(LinkColor), rect2)
                G.DrawLine(New Pen(Color.FromArgb(150, 100, 100, 100)), New Point(0, 0), New Point(0, Height - 1))

            End If

            If Borders Then G.DrawRectangle(New Pen(Color.FromArgb(150, 76, 76, 76)), Rect)
        End If

        If UseItAsTaskbar Then
            Select Case UseItAsTaskbar_Version
                Case TaskbarVersion.Eleven
                    Dim StartBtnRect As New Rectangle(8, 3, 36, 36)
                    Dim StartImgRect As New Rectangle(8, 3, 37, 37)

                    Dim App2BtnRect As New Rectangle(StartBtnRect.Right + 5, 3, 36, 36)
                    Dim App2ImgRect As New Rectangle(StartBtnRect.Right + 5, 3, 37, 37)
                    Dim App2BtnRectUnderline As New Rectangle(App2BtnRect.X + (App2BtnRect.Width - 8) / 2, App2BtnRect.Y + App2BtnRect.Height - 3, 8, 3)

                    Dim AppBtnRect As New Rectangle(App2BtnRect.Right + 5, 3, 36, 36)
                    Dim AppImgRect As New Rectangle(App2BtnRect.Right + 5, 3, 37, 37)
                    Dim AppBtnRectUnderline As New Rectangle(AppBtnRect.X + (AppBtnRect.Width - 16) / 2, AppBtnRect.Y + AppBtnRect.Height - 3, 16, 3)

                    Dim BackC As Color
                    Dim BorderC As Color

                    If DarkMode Then
                        BackC = Color.FromArgb(45, 130, 130, 130)
                        BorderC = Color.FromArgb(45, 130, 130, 130)
                    Else
                        BackC = Color.FromArgb(225, 255, 255, 255)
                        BorderC = Color.FromArgb(200, 230, 230, 230)
                    End If

                    G.FillRoundedRect(New SolidBrush(BackC), StartBtnRect, 3, True)
                    G.DrawRoundedRect_LikeW11(New Pen(BorderC), StartBtnRect, 3)
                    G.DrawImage(If(DarkMode, My.Resources.StartBtn_11Dark, My.Resources.StartBtn_11Light), StartImgRect)

                    G.FillRoundedRect(New SolidBrush(BackC), AppBtnRect, 3, True)
                    G.DrawRoundedRect_LikeW11(New Pen(BorderC), AppBtnRect, 3)
                    G.DrawImage(My.Resources.ActiveApp_Taskbar, AppImgRect)
                    G.FillRoundedRect(New SolidBrush(_AppUnderline), AppBtnRectUnderline, 2, True)

                    G.DrawImage(My.Resources.InactiveApp_Taskbar, App2ImgRect)
                    G.FillRoundedRect(New SolidBrush(Color.FromArgb(255, BackC)), App2BtnRectUnderline, 2, True)

                    G.DrawLine(New Pen(Color.FromArgb(100, 100, 100, 100)), New Point(0, 0), New Point(Width - 1, 0))

                Case TaskbarVersion.Ten
                    G.SmoothingMode = SmoothingMode.HighSpeed

                    Dim StartBtnRect As New Rectangle(-1, -1, 42, Height + 2)
                    Dim StartBtnImgRect As New Rectangle(StartBtnRect.X + (StartBtnRect.Width - My.Resources.StartBtn_10Dark.Width) / 2, StartBtnRect.Y + (StartBtnRect.Height - My.Resources.StartBtn_10Dark.Height) / 2, My.Resources.StartBtn_10Dark.Width, My.Resources.StartBtn_10Dark.Height)


                    Dim AppBtnRect As New Rectangle(StartBtnRect.Right, -1, 40, Height + 2)
                    Dim AppBtnImgRect As New Rectangle(AppBtnRect.X + (AppBtnRect.Width - My.Resources.ActiveApp_Taskbar.Width) / 2, AppBtnRect.Y + (AppBtnRect.Height - My.Resources.ActiveApp_Taskbar.Height) / 2 - 1, My.Resources.ActiveApp_Taskbar.Width, My.Resources.ActiveApp_Taskbar.Height)
                    Dim AppBtnRectUnderline As New Rectangle(AppBtnRect.X, AppBtnRect.Y + AppBtnRect.Height - 3, AppBtnRect.Width, 2)

                    Dim App2BtnRect As New Rectangle(AppBtnRect.Right, -1, 40, Height + 2)
                    Dim App2BtnImgRect As New Rectangle(App2BtnRect.X + (App2BtnRect.Width - My.Resources.InactiveApp_Taskbar.Width) / 2, App2BtnRect.Y + (App2BtnRect.Height - My.Resources.InactiveApp_Taskbar.Height) / 2, My.Resources.InactiveApp_Taskbar.Width, My.Resources.InactiveApp_Taskbar.Height)
                    Dim App2BtnRectUnderline As New Rectangle(App2BtnRect.X + 14 / 2, App2BtnRect.Y + App2BtnRect.Height - 3, App2BtnRect.Width - 14, 2)


                    Dim StartColor As Color = _StartColor
                    G.FillRectangle(New SolidBrush(StartColor), StartBtnRect)
                    G.DrawImage(If(DarkMode, My.Resources.StartBtn_10Dark, My.Resources.StartBtn_10Light), StartBtnImgRect)

                    Dim AppColor As Color = _AppBackground
                    G.FillRectangle(New SolidBrush(AppColor), AppBtnRect)
                    G.FillRectangle(New SolidBrush(_AppUnderline.Light), AppBtnRectUnderline)
                    G.DrawImage(My.Resources.ActiveApp_Taskbar, AppBtnImgRect)

                    G.FillRectangle(New SolidBrush(_AppUnderline.Light), App2BtnRectUnderline)
                    G.DrawImage(My.Resources.InactiveApp_Taskbar, App2BtnImgRect)

                Case TaskbarVersion.Seven

                    If Basic Then
                        G.DrawImage(My.Resources.BasicTaskbar, Rect)
                    Else

                        If Not Win7AeroOpaque Then
                            Dim bk As Bitmap = adaptedBackBlurred
                            Dim alphaX As Single = 1 - BackColorAlpha / 100  'ColorBlurBalance
                            If alphaX < 0 Then alphaX = 0
                            If alphaX > 1 Then alphaX = 1

                            Dim ColBal As Single = Win7ColorBal / 100        'ColorBalance
                            Dim GlowBal As Single = Win7GlowBal / 100        'AfterGlowBalance
                            Dim Color1 As Color = BackColor
                            Dim Color2 As Color = BackColor2

                            G.DrawAeroEffect(Rect, bk, Color1, ColBal, Color2, GlowBal, alphaX, 0, False)

                        Else
                            G.FillRectangle(New SolidBrush(Color.White), Rect)
                            G.FillRectangle(New SolidBrush(Color.FromArgb(255 * BackColorAlpha / 100, BackColor)), Rect)
                        End If

                        G.DrawImage(My.Resources.Win7TaskbarSides, Rect)

                        G.FillRoundedImg(Noise7.Clone(Bounds, PixelFormat.Format32bppArgb), Rect, Radius, True)
                    End If

                    If Not Basic Then
                        G.DrawLine(New Pen(Color.FromArgb(80, 0, 0, 0)), New Point(0, 0), New Point(Width - 1, 0))
                        G.DrawLine(New Pen(Color.FromArgb(80, 255, 255, 255)), New Point(0, 1), New Point(Width - 1, 1))
                    End If

                    G.DrawImage(My.Resources.AeroPeek, New Rectangle(Width - 10, 0, 10, Height))

                    Dim StartORB As New Bitmap(My.Resources.Win7ORB)

                    Dim StartBtnRect As New Rectangle(3, -3, 39, 39)

                    Dim AppBtnRect As New Rectangle(StartBtnRect.Right + 5, 0, 45, 35)
                    Dim AppBtnImgRect As New Rectangle(AppBtnRect.X + (AppBtnRect.Width - My.Resources.ActiveApp_Taskbar.Width) / 2, AppBtnRect.Y + (AppBtnRect.Height - My.Resources.ActiveApp_Taskbar.Height) / 2, My.Resources.ActiveApp_Taskbar.Width, My.Resources.ActiveApp_Taskbar.Height)

                    Dim App2BtnRect As New Rectangle(AppBtnRect.Right + 1, 0, 45, 35)
                    Dim App2BtnImgRect As New Rectangle(App2BtnRect.X + (App2BtnRect.Width - My.Resources.InactiveApp_Taskbar.Width) / 2, App2BtnRect.Y + (App2BtnRect.Height - My.Resources.InactiveApp_Taskbar.Height) / 2, My.Resources.InactiveApp_Taskbar.Width, My.Resources.InactiveApp_Taskbar.Height)

                    G.DrawImage(StartORB, StartBtnRect)

                    G.DrawRoundedRect(New Pen(Color.FromArgb(150, 0, 0, 0)), New Rectangle(AppBtnRect.X, AppBtnRect.Y, AppBtnRect.Width - 2, AppBtnRect.Height - 2), 2)
                    G.DrawImage(My.Resources.Taskbar_ActiveApp7, AppBtnRect)
                    G.DrawImage(My.Resources.ActiveApp_Taskbar, AppBtnImgRect)

                    G.DrawRoundedRect(New Pen(Color.FromArgb(110, 0, 0, 0)), New Rectangle(App2BtnRect.X, App2BtnRect.Y, App2BtnRect.Width - 2, App2BtnRect.Height - 2), 2)
                    G.DrawImage(My.Resources.Taskbar_InactiveApp7, App2BtnRect)
                    G.DrawImage(My.Resources.InactiveApp_Taskbar, App2BtnImgRect)


                Case TaskbarVersion.Eight
                    Dim c As Color = Color.FromArgb((Win7ColorBal / 100) * 255, BackColor)
                    Dim bc As Color = Color.FromArgb(217, 217, 217)

                    If Transparency Then
                        G.DrawLine(New Pen(Color.FromArgb(80, 0, 0, 0)), New Point(0, 0), New Point(Width - 1, 0))
                    Else
                        G.DrawRectangle(New Pen(Color.FromArgb(89, 89, 89)), New Rectangle(0, 0, Width - 1, Height - 1))
                    End If

                    Dim StartORB As New Bitmap(My.Resources.Win8ORB)
                    Dim StartBtnRect As New Rectangle((35 - 27) / 2 + 2, (35 - 27) / 2 - 1, 27, 27)
                    Dim AppBtnRect As New Rectangle(StartBtnRect.Right + 8, 0, 45, Height - 1)
                    Dim AppBtnRectInner As New Rectangle(AppBtnRect.X + 1, AppBtnRect.Y + 1, AppBtnRect.Width - 2, AppBtnRect.Height - 2)

                    Dim AppBtnImgRect As New Rectangle(AppBtnRect.X + (AppBtnRect.Width - My.Resources.ActiveApp_Taskbar.Width) / 2, AppBtnRect.Y + (AppBtnRect.Height - My.Resources.ActiveApp_Taskbar.Height) / 2, My.Resources.ActiveApp_Taskbar.Width, My.Resources.ActiveApp_Taskbar.Height)
                    Dim App2BtnRect As New Rectangle(AppBtnRect.Right + 2, 0, 45, Height - 1)
                    Dim App2BtnRectInner As New Rectangle(App2BtnRect.X + 1, App2BtnRect.Y + 1, App2BtnRect.Width - 2, App2BtnRect.Height - 2)
                    Dim App2BtnImgRect As New Rectangle(App2BtnRect.X + (App2BtnRect.Width - My.Resources.InactiveApp_Taskbar.Width) / 2, App2BtnRect.Y + (App2BtnRect.Height - My.Resources.InactiveApp_Taskbar.Height) / 2, My.Resources.InactiveApp_Taskbar.Width, My.Resources.InactiveApp_Taskbar.Height)


                    G.DrawImage(StartORB, StartBtnRect)

                    If Transparency Then
                        G.FillRectangle(New SolidBrush(Color.FromArgb(100, Color.White)), AppBtnRect)
                        G.DrawRectangle(New Pen(Color.FromArgb(200, c.CB(-0.5))), AppBtnRect)
                        G.DrawRectangle(New Pen(Color.FromArgb(215, Color.White)), AppBtnRectInner)
                    Else
                        G.FillRectangle(New SolidBrush(Color.FromArgb(255, bc.CB(0.5))), AppBtnRect)
                        G.FillRectangle(New SolidBrush(Color.FromArgb(255 * (Win7ColorBal / 100), c.CB(0.5))), AppBtnRect)
                        G.DrawRectangle(New Pen(Color.FromArgb(100, bc.CB(-0.5))), AppBtnRect)
                        G.DrawRectangle(New Pen(Color.FromArgb(100 * (Win7ColorBal / 100), c.CB(-0.5))), AppBtnRect)
                    End If

                    G.DrawImage(My.Resources.ActiveApp_Taskbar, AppBtnImgRect)

                    If Transparency Then
                        G.FillRectangle(New SolidBrush(Color.FromArgb(50, Color.White)), App2BtnRect)
                        G.DrawRectangle(New Pen(Color.FromArgb(100, c.CB(-0.5))), App2BtnRect)
                        G.DrawRectangle(New Pen(Color.FromArgb(100, Color.White)), App2BtnRectInner)
                    Else
                        G.FillRectangle(New SolidBrush(Color.FromArgb(255, bc.Light(0.1))), App2BtnRect)
                        G.FillRectangle(New SolidBrush(Color.FromArgb(255 * (Win7ColorBal / 100), c.Light(0.1))), App2BtnRect)
                        G.DrawRectangle(New Pen(Color.FromArgb(100, bc.Dark(0.1))), App2BtnRect)
                        G.DrawRectangle(New Pen(Color.FromArgb(100 * (Win7ColorBal / 100), c.Dark(0.1))), App2BtnRect)
                    End If

                    G.DrawImage(My.Resources.InactiveApp_Taskbar, App2BtnImgRect)

            End Select
        End If

    End Sub

    Private Sub XenonTaskbar_HandleCreated(sender As Object, e As EventArgs) Handles Me.HandleCreated
        If Not DesignMode Then
            Try : AddHandler Parent.BackgroundImageChanged, AddressOf ProcessBack : Catch : End Try
            Try : AddHandler BlurPowerChanged, AddressOf BlurBack : Catch : End Try
            Try : AddHandler NoisePowerChanged, AddressOf NoiseBack : Catch : End Try

            Try : AddHandler SizeChanged, AddressOf ProcessBack : Catch : End Try
            Try : AddHandler LocationChanged, AddressOf ProcessBack : Catch : End Try
            Try : AddHandler PaddingChanged, AddressOf ProcessBack : Catch : End Try

            'Try : AddHandler BackColorChanged, AddressOf Refresh : Catch : End Try
            'Try : AddHandler BackColor2Changed, AddressOf Refresh : Catch : End Try
            'Try : AddHandler Win7ColorBalChanged, AddressOf Refresh : Catch : End Try
            'Try : AddHandler Win7GlowBalChanged, AddressOf Refresh : Catch : End Try
            'Try : AddHandler BackColorAlphaChanged, AddressOf ProcessBack : Catch : End Try

            ProcessBack()
        End If
    End Sub

    Sub ProcessBack()
        GetBack()
        BlurBack()
        'NoiseBack()
    End Sub

    Sub GetBack()
        Try
            adaptedBack = My.Wallpaper.Clone(Bounds, My.Wallpaper.PixelFormat)
        Catch : End Try

        Try : If Transparency Then
                If UseItAsTaskbar_Version = TaskbarVersion.Seven Then
                    adaptedBackBlurred = New Bitmap(adaptedBack).Blur(1)
                End If
            End If
        Catch : End Try
    End Sub

    Sub BlurBack()
        Try : If Transparency Then
                If UseItAsTaskbar_Version <> TaskbarVersion.Seven And UseItAsTaskbar_Version <> TaskbarVersion.Eight Then
                    adaptedBackBlurred = New Bitmap(adaptedBack).Blur(BlurPower)
                End If
            End If
        Catch : End Try
    End Sub

    Sub NoiseBack()
        Try
            If Transparency Then
                If UseItAsTaskbar_Version = TaskbarVersion.Eleven Or UseItAsTaskbar_Version = TaskbarVersion.Ten Then
                    Noise = New TextureBrush(My.Resources.GaussianBlur.Fade(NoisePower))
                ElseIf UseItAsTaskbar_Version = TaskbarVersion.Seven Then
                    Try : Noise7 = My.Resources.AeroGlass.Fade(NoisePower / 100) : Catch : End Try
                    Try : Noise7Start = My.Resources.Start7Glass.Fade(NoisePower / 100) : Catch : End Try
                ElseIf UseItAsTaskbar_Version = TaskbarVersion.Eight Then
                    Try : Noise7 = My.Resources.AeroGlass.Fade(NoisePower / 100) : Catch : End Try
                    Try : Noise7Start = My.Resources.Start7Glass.Fade(NoisePower / 100) : Catch : End Try
                End If

            End If
        Catch : End Try
    End Sub
End Class
Public Class XenonFakeIcon : Inherits Panel

    Sub New()
        DoubleBuffered = True
        BackColor = Color.Transparent
        SetStyle(ControlStyles.SupportsTransparentBackColor, True)
        SetStyle(ControlStyles.AllPaintingInWmPaint, True)
        SetStyle(ControlStyles.UserPaint, True)
        SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
    End Sub

    Protected Overrides ReadOnly Property CreateParams As CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ExStyle = cp.ExStyle Or &H20
            Return cp
        End Get
    End Property

    Public Property ColorText As Color = Color.White
    Public Property ColorGlow As Color = Color.FromArgb(50, 0, 0, 0)
    Public Property Icon As Icon
    Public Property Title As String = "New Folder"

    Private _IconSize As Integer = 32
    Public Property IconSize As Integer
        Get
            Return _IconSize
        End Get
        Set(value As Integer)
            _IconSize = value
            Invalidate()
        End Set
    End Property

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        Dim G As Graphics = e.Graphics
        G.SmoothingMode = SmoothingMode.HighQuality
        G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit
        DoubleBuffered = True

        Dim IconRect As New Rectangle(0, 0, Width - 1, Height - 30)

        Dim LabelRect As New Rectangle(0, Height - 35, Width - 1, 30)
        Dim LabelRectShadow As New Rectangle(1, Height - 34, Width - 1, 30)

        If _IconSize < 16 Then _IconSize = 16
        If _IconSize > 256 Then _IconSize = 256

        Dim IconRectX As New Rectangle(IconRect.X + (IconRect.Width - _IconSize) / 2, IconRect.Y + (IconRect.Height - _IconSize) / 2, _IconSize, _IconSize)

        If Icon IsNot Nothing Then
            G.DrawIcon(New Icon(Icon, _IconSize, _IconSize), IconRectX)
        End If

        G.DrawString(Title, Me.Font, Brushes.Black, LabelRectShadow, StringAligner(ContentAlignment.MiddleCenter))
        'G.DrawString(Title, Font, Brushes.White, LabelRect, StringAligner(ContentAlignment.MiddleCenter))

        G.DrawGlowString(1, Title, Me, ColorText, ColorGlow, LabelRect, StringAligner(ContentAlignment.MiddleCenter))

    End Sub

End Class
Public Class XenonWindow : Inherits ContainerControl : Implements INotifyPropertyChanged

    Private _DarkMode As Boolean = True

    Public Event DarkModeChanged As PropertyChangedEventHandler _
        Implements INotifyPropertyChanged.PropertyChanged
    Private Sub NotifyDarkModeChanged(ByVal info As String)
        RaiseEvent DarkModeChanged(Me, New PropertyChangedEventArgs(info))
    End Sub
    Public Property DarkMode() As Boolean
        Get
            Return _DarkMode
        End Get

        Set(ByVal value As Boolean)
            If Not (value = _DarkMode) Then
                Me._DarkMode = value
                NotifyDarkModeChanged("DarkMode")
                Invalidate()
            End If
        End Set
    End Property


    Private _AccentColor_Enabled As Boolean = True
    Public Event AccentColor_EnabledChanged As PropertyChangedEventHandler
    Private Sub NotifyAccentColor_EnabledChanged(ByVal info As String)
        RaiseEvent AccentColor_EnabledChanged(Me, New PropertyChangedEventArgs(info))
    End Sub
    Public Property AccentColor_Enabled() As Boolean
        Get
            Return _AccentColor_Enabled
        End Get

        Set(ByVal value As Boolean)
            If Not (value = AccentColor_Enabled) Then
                Me._AccentColor_Enabled = value
                NotifyAccentColor_EnabledChanged("AccentColor_Enabled")
                Invalidate()
            End If
        End Set
    End Property

    Sub New()
        AdjustPadding()
        Font = New Font("Segoe UI", 9)
        DoubleBuffered = True
    End Sub

    Private _DropShadow As Boolean = True
    Public Event DropShadowChanged As PropertyChangedEventHandler
    Private Sub NotifyDropShadowChanged(ByVal info As String)
        RaiseEvent DropShadowChanged(Me, New PropertyChangedEventArgs(info))
    End Sub
    Public Property DropShadow() As Boolean
        Get
            Return _DropShadow
        End Get

        Set(ByVal value As Boolean)
            If Not (value = DropShadow) Then
                Me._DropShadow = value
                NotifyDropShadowChanged("DropShadow")
            End If
        End Set
    End Property

    Private _Win7Noise As Single = 1
    Public Event Win7NoiseChanged As PropertyChangedEventHandler
    Private Sub NotifyWin7NoiseChanged(ByVal info As Single)
        RaiseEvent Win7NoiseChanged(Me, New PropertyChangedEventArgs(info))
    End Sub
    Public Property Win7Noise() As Single
        Get
            Return _Win7Noise
        End Get

        Set(ByVal value As Single)
            If Not (value = Win7Noise) Then
                Me._Win7Noise = value

                If Preview = Preview_Enum.W7Aero Or Preview = Preview_Enum.W7Opaque Or Preview = Preview_Enum.W7Basic Then
                    Try : Noise7 = My.Resources.AeroGlass.Fade(Win7Noise / 100) : Catch : End Try
                End If

                NotifyWin7NoiseChanged(_Win7Noise)
            End If
        End Set
    End Property


    Private _Metrics_CaptionHeight As Integer = 22
    Public Property Metrics_CaptionHeight As Integer
        Get
            Return _Metrics_CaptionHeight
        End Get

        Set(value As Integer)
            _Metrics_CaptionHeight = value
            AdjustPadding()
            Refresh()
            RaiseEvent MetricsChanged()
        End Set
    End Property

    Private _Metrics_BorderWidth As Integer = 1
    Public Property Metrics_BorderWidth As Integer
        Get
            Return _Metrics_BorderWidth
        End Get

        Set(value As Integer)
            _Metrics_BorderWidth = value
            AdjustPadding()
            Refresh()
            RaiseEvent MetricsChanged()
        End Set
    End Property

    Private _Metrics_PaddedBorderWidth As Integer = 4
    Public Property Metrics_PaddedBorderWidth As Integer
        Get
            Return _Metrics_PaddedBorderWidth
        End Get
        Set(value As Integer)
            _Metrics_PaddedBorderWidth = value
            AdjustPadding()
            Refresh()
            RaiseEvent MetricsChanged()
        End Set
    End Property

    Public Sub SetMetrics(ByVal [XenonWindow] As XenonWindow)
        [XenonWindow].Metrics_BorderWidth = Metrics_BorderWidth
        [XenonWindow].Metrics_CaptionHeight = Metrics_CaptionHeight
        [XenonWindow].Metrics_PaddedBorderWidth = Metrics_PaddedBorderWidth
        [XenonWindow].Refresh()
    End Sub

    Public Event MetricsChanged()
    Sub AdjustPadding()
        Dim i, iTop As Integer

        Dim TitleTextH, TitleTextH_9, TitleTextH_Sum As Integer
        TitleTextH = "ABCabc0123xYz.#".Measure(Font).Height
        TitleTextH_9 = "ABCabc0123xYz.#".Measure(New Font(Font.Name, 9, Font.Style)).Height
        TitleTextH_Sum = Math.Max(0, TitleTextH - TitleTextH_9 - 5)

        If Preview = Preview_Enum.W7Aero Or Preview = Preview_Enum.W7Opaque Or Preview = Preview_Enum.W7Basic Or Preview = Preview_Enum.W8 Or Preview = Preview_Enum.W8Lite Then
            i = FreeMargin + _Metrics_PaddedBorderWidth + _Metrics_BorderWidth
            iTop = i + TitleTextH_Sum + _Metrics_CaptionHeight

            i += 4
            iTop += 3
        Else
            i = FreeMargin
            iTop = i + TitleTextH_Sum + _Metrics_PaddedBorderWidth + _Metrics_BorderWidth + _Metrics_CaptionHeight

            i += 1
            iTop += 4
        End If


        Padding = New Padding(i, iTop, i, i)
    End Sub

    Enum Preview_Enum
        W11
        W10
        W8
        W8Lite
        W7Aero
        W7Opaque
        W7Basic
    End Enum

    Public Property Shadow As Boolean = True
    Public Property Radius As Integer = 5
    Public Property AccentColor_Active As Color = Color.FromArgb(0, 120, 212)
    Public Property AccentColor_Inactive As Color = Color.FromArgb(32, 32, 32)
    Public Property AccentColor2_Active As Color = Color.FromArgb(0, 120, 212)
    Public Property AccentColor2_Inactive As Color = Color.FromArgb(32, 32, 32)
    Public Property Active As Boolean = True
    Public Property Preview As Preview_Enum = XenonWindow.Preview_Enum.W11
    Public Property Win7Alpha As Integer = 100
    Public Property Win7ColorBal As Integer = 100
    Public Property Win7GlowBal As Integer = 100
    Public Property ToolWindow As Boolean = False

    Dim AdaptedBack, AdaptedBackBlurred As Bitmap
    Dim Noise7 As Bitmap = My.Resources.AeroGlass

    ReadOnly FreeMargin As Integer = 8

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        Dim G As Graphics = e.Graphics
        G.SmoothingMode = SmoothingMode.AntiAlias
        G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit
        DoubleBuffered = True

        '### Adjust Limits
        'If _Metrics_CaptionHeight < 17 Then _Metrics_CaptionHeight = 17

        Dim Rect As New Rectangle(FreeMargin, FreeMargin, Width - (FreeMargin * 2 + 1), Height - (FreeMargin * 2 + 1))
        Dim RectBK As New Rectangle(0, 0, Width, Height)
        Dim TitleTextH, TitleTextH_9, TitleTextH_Sum As Integer
        TitleTextH = "ABCabc0123xYz.#".Measure(Font).Height
        TitleTextH_9 = "ABCabc0123xYz.#".Measure(New Font(Font.Name, 9, Font.Style)).Height
        TitleTextH_Sum = Math.Max(0, TitleTextH - TitleTextH_9 - 5)
        Dim TitlebarRect As New Rectangle(Rect.X, Rect.Y, Rect.Width, TitleTextH_Sum + _Metrics_BorderWidth + _Metrics_CaptionHeight + _Metrics_PaddedBorderWidth + 3)
        'If TitlebarRect.Height < 25 Then TitlebarRect.Height = 25
        Dim IconSize As Integer = 14
        If _Metrics_CaptionHeight <= 17 Then IconSize = 12
        Dim IconRect As New Rectangle(Rect.X + 4 + _Metrics_PaddedBorderWidth + _Metrics_BorderWidth, Rect.Y + (TitlebarRect.Height - IconSize) / 2, IconSize, IconSize)
        Dim LabelRect As New Rectangle(IconRect.Right + 4, Rect.Y, TitlebarRect.Width - (IconRect.Right + 4), TitlebarRect.Height)
        If ToolWindow Then LabelRect.X = IconRect.X
        Dim LabelRect8 As New Rectangle(Rect.X, Rect.Y + 2, TitlebarRect.Width - 1, TitlebarRect.Height - 3)
        Dim XRect As New Rectangle(Rect.Right - 20, Rect.Y, 20, TitlebarRect.Height)
        G.Clear(Color.Transparent)


        If Preview = Preview_Enum.W11 Then
#Region "Windows 11"
            Try
                G.DrawImage(AdaptedBack, RectBK)
            Catch
            End Try

            If Shadow And Active And Not DesignMode Then
                G.DrawGlow(Rect, Color.FromArgb(150, 0, 0, 0), 5, 15)
            End If

            If DarkMode Then
                G.FillRoundedRect(New SolidBrush(Color.FromArgb(20, 20, 20)), Rect, Radius, True)
            Else
                G.FillRoundedRect(New SolidBrush(Color.FromArgb(240, 240, 240)), Rect, Radius, True)
            End If

            If AccentColor_Enabled Then
                If Active Then
                    G.DrawRoundedRect(New Pen(Color.FromArgb(200, AccentColor_Active)), Rect, Radius, True)
                Else
                    G.DrawRoundedRect(New Pen(Color.FromArgb(200, AccentColor_Inactive)), Rect, Radius, True)
                End If
            Else
                If DarkMode Then
                    G.DrawRoundedRect(New Pen(Color.FromArgb(200, 100, 100, 100)), Rect, Radius, True)
                Else
                    G.DrawRoundedRect(New Pen(Color.FromArgb(200, 220, 220, 220)), Rect, Radius, True)
                End If
            End If

            If AccentColor_Enabled Then
                If Active Then
                    FillSemiRect(G, New SolidBrush(AccentColor_Active), TitlebarRect, Radius)
                    G.DrawLine(New Pen(AccentColor_Active), New Point(TitlebarRect.X + 1, TitlebarRect.Y + TitlebarRect.Height), New Point(TitlebarRect.X + TitlebarRect.Width - 1, TitlebarRect.Y + TitlebarRect.Height))
                Else
                    FillSemiRect(G, New SolidBrush(AccentColor_Inactive), TitlebarRect, Radius)
                    G.DrawLine(New Pen(AccentColor_Inactive), New Point(TitlebarRect.X + 1, TitlebarRect.Y + TitlebarRect.Height), New Point(TitlebarRect.X + TitlebarRect.Width - 1, TitlebarRect.Y + TitlebarRect.Height))
                End If
            Else
                FillSemiRect(G, Brushes.White, TitlebarRect, Radius)
            End If
#End Region

        ElseIf Preview = Preview_Enum.W10 Then
#Region "Windows 10"
            Try
                G.DrawImage(AdaptedBack, RectBK)
            Catch
            End Try

            If Shadow And Active And Not DesignMode Then
                G.DrawGlow(Rect, Color.FromArgb(150, 0, 0, 0), 5, 15)
            End If

            If DarkMode Then
                G.FillRectangle(New SolidBrush(Color.FromArgb(20, 20, 20)), Rect)
            Else
                G.FillRectangle(New SolidBrush(Color.FromArgb(240, 240, 240)), Rect)
            End If

            If AccentColor_Enabled Then
                If Active Then
                    G.DrawRectangle(New Pen(Color.FromArgb(200, AccentColor_Active)), Rect)
                Else
                    G.DrawRectangle(New Pen(Color.FromArgb(200, AccentColor_Inactive)), Rect)
                End If
            Else
                If DarkMode Then
                    G.DrawRectangle(New Pen(Color.FromArgb(200, 100, 100, 100)), Rect)
                Else
                    G.DrawRectangle(New Pen(Color.FromArgb(200, 220, 220, 220)), Rect)
                End If
            End If

            If AccentColor_Enabled Then
                If Active Then
                    G.FillRectangle(New SolidBrush(AccentColor_Active), TitlebarRect)
                Else
                    G.FillRectangle(New SolidBrush(AccentColor_Inactive), TitlebarRect)
                End If
            Else
                G.FillRectangle(Brushes.White, TitlebarRect)
            End If
#End Region

        ElseIf Preview = Preview_Enum.W8 Or Preview = Preview_Enum.W8Lite Then
#Region "Windows 8.1"
            Dim InnerWindow_1 As New Rectangle
            Dim InnerWindow_2 As New Rectangle
            Dim Sum As Integer = Metrics_BorderWidth + Metrics_PaddedBorderWidth
            If Sum < 2 Then Sum = 2
            TitleTextH = "ABCabc0123xYz.#".Measure(Font).Height
            TitleTextH_9 = "ABCabc0123xYz.#".Measure(New Font(Font.Name, 9, Font.Style)).Height
            TitleTextH_Sum = Math.Max(0, TitleTextH - TitleTextH_9 - 5)
            Dim Sum_Ttl As Integer = Sum + Metrics_CaptionHeight + TitleTextH_Sum

            IconRect.X += 2

            With Rect
                InnerWindow_1 = New Rectangle(.X + Sum + If(Not Preview = Preview_Enum.W8Lite, 2, 3), .Y + Sum_Ttl + 3, .Width - (Sum) * 2 - If(Not Preview = Preview_Enum.W8Lite, 4, 6), .Height - (Sum + Sum_Ttl) - If(Not Preview = Preview_Enum.W8Lite, 5, 5))
                InnerWindow_2 = New Rectangle(InnerWindow_1.X + 1, InnerWindow_1.Y + 1, InnerWindow_1.Width - 2, InnerWindow_1.Height - 2)
            End With

            Dim CloseRectH As Integer = Metrics_CaptionHeight + TitleTextH_Sum - 2 + If(Preview = Preview_Enum.W8Lite, 1, 0)

            Dim CloseRectW As Integer = If(Not ToolWindow, CloseRectH * 3 / 2, CloseRectH) + If(Preview = Preview_Enum.W8Lite, 2, 0)

            Dim CloseRect As New Rectangle

            If Not ToolWindow Then

                If Not Preview = Preview_Enum.W8Lite Then
                    CloseRect = New Rectangle(InnerWindow_1.Right - CloseRectW + 1, Rect.Y + 1, CloseRectW, CloseRectH)
                Else
                    CloseRect = New Rectangle(InnerWindow_1.Right - CloseRectW + 2, Rect.Y, CloseRectW, CloseRectH)
                End If

            Else
                CloseRect = New Rectangle(InnerWindow_1.Right - CloseRectW + 1, Rect.Y + 1, CloseRectW, CloseRectH)
            End If

            Dim InC As Color = If(Not Preview = Preview_Enum.W8Lite, Color.FromArgb(235, 235, 235), Color.FromArgb((Win7ColorBal / 100) * 255, AccentColor_Active.CB(0.8)))

            Dim c As Color = If(Active, Color.FromArgb((Win7ColorBal / 100) * 255, AccentColor_Active), InC)

            Dim bc As Color = Color.FromArgb(217, 217, 217)

            G.DrawImage(AdaptedBack, RectBK)

            G.FillRectangle(New SolidBrush(bc), Rect)
            G.FillRectangle(New SolidBrush(c), Rect)

            G.FillRectangle(New SolidBrush(Color.White), InnerWindow_1)

            Dim CloseBtn As Image

            If Not ToolWindow Then
                If CloseRect.Height >= 27 Then
                    CloseBtn = My.Resources.Win8_Close_3
                ElseIf CloseRect.Height >= 24 Then
                    CloseBtn = My.Resources.Win8_Close_2
                ElseIf CloseRect.Height >= 21 Then
                    CloseBtn = My.Resources.Win8_Close_1
                Else
                    CloseBtn = My.Resources.Win8_Close_0
                End If

            Else
                CloseBtn = My.Resources.Win8_Close_ToolWindow
            End If

            If Preview = Preview_Enum.W8Lite Then CloseBtn = CloseBtn.ReplaceColor(Color.FromArgb(255, 255, 255), Color.Black)

            If Not Preview = Preview_Enum.W8Lite Then
                G.DrawRectangle(New Pen(Color.FromArgb(170, bc.CB(-0.2))), InnerWindow_1)
                G.DrawRectangle(New Pen(Color.FromArgb((Win7ColorBal / 100) * 255, c.CB(-0.2))), InnerWindow_1)

                G.SmoothingMode = SmoothingMode.HighSpeed
                G.FillRectangle(New SolidBrush(If(Active, Color.FromArgb(199, 80, 80), Color.FromArgb(188, 188, 188))), CloseRect)
                G.SmoothingMode = SmoothingMode.AntiAlias

                G.DrawImage(CloseBtn, New Rectangle(CloseRect.X + (CloseRect.Width - CloseBtn.Width) / 2, CloseRect.Y + (CloseRect.Height - CloseBtn.Height) / 2, CloseBtn.Width, CloseBtn.Height))

                G.DrawRectangle(New Pen(Color.FromArgb(200, bc.CB(-0.3))), Rect)
                G.DrawRectangle(New Pen(Color.FromArgb((Win7ColorBal / 100) * 255, c.CB(-0.3))), Rect)

            Else

                G.DrawLine(New Pen(Color.FromArgb((Win7ColorBal / 100) * 255, c).LightLight), New Point(InnerWindow_1.X, InnerWindow_1.Y), New Point(InnerWindow_1.X + InnerWindow_1.Width, InnerWindow_1.Y))

                G.DrawLine(New Pen(Color.FromArgb((Win7ColorBal / 100) * 255, c).LightLight), New Point(InnerWindow_1.X, InnerWindow_1.Y + InnerWindow_1.Height), New Point(InnerWindow_1.X + InnerWindow_1.Width, InnerWindow_1.Y + InnerWindow_1.Height))

                G.FillRectangle(New SolidBrush(If(Active, Color.FromArgb(195, 90, 80), Color.Transparent)), CloseRect)

                G.SmoothingMode = SmoothingMode.HighSpeed
                G.DrawRectangle(New Pen(If(Active, Color.FromArgb(92, 58, 55), Color.FromArgb(93, 96, 102))), CloseRect)
                G.SmoothingMode = SmoothingMode.AntiAlias

                G.DrawImage(CloseBtn, New Rectangle(CloseRect.X + (CloseRect.Width - CloseBtn.Width) / 2, CloseRect.Y + (CloseRect.Height - CloseBtn.Height) / 2, CloseBtn.Width, CloseBtn.Height))

                G.DrawRectangle(New Drawing.Pen(Color.FromArgb(47, 48, 51)), Rect)
            End If
#End Region

        ElseIf Preview = Preview_Enum.W7Aero Or Preview = Preview_Enum.W7Opaque Or Preview = Preview_Enum.W7Basic Then
#Region "Windows 7"
            Dim InnerWindow_1 As New Rectangle
            Dim InnerWindow_2 As New Rectangle
            Dim RectSide1 As New Rectangle
            Dim RectSide2 As New Rectangle
            Dim Sum As Integer = Metrics_BorderWidth + Metrics_PaddedBorderWidth
            If Sum < 2 Then Sum = 2

            TitleTextH = "ABCabc0123xYz.#".Measure(Font).Height
            TitleTextH_9 = "ABCabc0123xYz.#".Measure(New Font(Font.Name, 9, Font.Style)).Height
            TitleTextH_Sum = Math.Max(0, TitleTextH - TitleTextH_9 - 5)
            Dim Sum_Ttl As Integer = Sum + Metrics_CaptionHeight + TitleTextH_Sum

            With Rect
                InnerWindow_1 = New Rectangle(.X + Sum + 1, .Y + Sum_Ttl, .Width - (Sum) * 2 - 2, .Height - (Sum + Sum_Ttl) - 2)
                InnerWindow_2 = New Rectangle(InnerWindow_1.X + 1, InnerWindow_1.Y + 1, InnerWindow_1.Width - 2, InnerWindow_1.Height - 2)

                RectSide1 = New Rectangle(.X + 1, InnerWindow_1.Y, Sum, InnerWindow_1.Height * 0.5)
                RectSide2 = New Rectangle(InnerWindow_1.Right - 1, RectSide1.Y, RectSide1.Width + 1, RectSide1.Height)
            End With


            If Preview <> Preview_Enum.W7Basic Then

#Region "Aero"
                G.DrawImage(AdaptedBack, RectBK)

                If Shadow And Active And Not DesignMode Then
                    G.DrawGlow(Rect, Color.FromArgb(150, 0, 0, 0), 5, 15)
                End If

                Dim Radius As Integer = 5

                If Not Preview = Preview_Enum.W7Opaque Then
                    Dim bk As Bitmap = AdaptedBackBlurred

                    Dim alpha As Single = 1 - Win7Alpha / 100   'ColorBlurBalance
                    Dim ColBal As Single = Win7ColorBal / 100   'ColorBalance
                    Dim GlowBal As Single = Win7GlowBal / 100   'AfterGlowBalance

                    Dim Color1 As Color = If(Active, AccentColor_Active, AccentColor_Inactive)
                    Dim Color2 As Color = If(Active, AccentColor2_Active, AccentColor2_Inactive)

                    G.DrawAeroEffect(Rect, bk, Color1, ColBal, Color2, GlowBal, alpha, Radius, Not ToolWindow)
                Else

                    If Not ToolWindow Then
                        G.FillRoundedRect(New SolidBrush(Color.White), Rect, Radius, True)
                        G.FillRoundedRect(New SolidBrush(Color.FromArgb(255 * Win7Alpha / 100, If(Active, AccentColor_Active, AccentColor_Inactive))), Rect, Radius, True)
                    Else
                        G.FillRectangle(New SolidBrush(Color.White), Rect)
                        G.FillRectangle(New SolidBrush(Color.FromArgb(255 * Win7Alpha / 100, If(Active, AccentColor_Active, AccentColor_Inactive))), Rect)
                    End If

                End If

                If Active Then
                    G.DrawImage(My.Resources.Win7Sides, RectSide1)
                    G.DrawImage(My.Resources.Win7Sides, RectSide2)

                    Dim TitleTopW As Integer = Rect.Width * 0.6
                    Dim TitleTopH As Integer = Rect.Height * 0.6

                    G.DrawImage(My.Resources.Win7_TitleTopL, New Rectangle(Rect.X + If(ToolWindow, 0, 1), Rect.Y + If(ToolWindow, 1, 0), TitleTopW, TitleTopH))
                    G.DrawImage(My.Resources.Win7_TitleTopR, New Rectangle(Rect.X + Rect.Width - TitleTopW, Rect.Y + If(ToolWindow, 1, 0), TitleTopW, TitleTopH))
                End If

                Dim inner As New Rectangle(Rect.X + 1, Rect.Y + 1, Rect.Width - 2, Rect.Height - 2)

                If Not ToolWindow Then
                    G.FillRoundedImg(Noise7.Clone(Bounds, PixelFormat.Format32bppArgb), Rect, Radius, True)
                    G.DrawRoundedRect(New Pen(Color.FromArgb(If(Active, 130, 100), 25, 25, 25)), Rect, Radius, True)
                    G.DrawRoundedRect(New Pen(Color.FromArgb(100, 255, 255, 255)), inner, Radius, True)
                    'DrawRect(G, New Pen(Color.FromArgb(255 - 255 * Win7Alpha / 300, BackColor, 0.2))), Rect, Radius, True)
                    G.DrawRoundedRect(New Pen(Color.FromArgb(255 - 255 * Win7Alpha / 300, BackColor.Light(0.2))), InnerWindow_1, 1, True)
                    G.FillRoundedRect(New SolidBrush(Color.White), InnerWindow_1, 1, True)
                    G.DrawRoundedRect(New Pen(Color.FromArgb(255 - 255 * Win7Alpha / 300, BackColor.Dark(0.2))), InnerWindow_2, 1, True)
                Else
                    G.DrawImage(Noise7.Clone(Bounds, PixelFormat.Format32bppArgb), Rect)
                    G.DrawRectangle(New Pen(Color.FromArgb(If(Active, 130, 100), 25, 25, 25)), Rect)
                    G.DrawRectangle(New Pen(Color.FromArgb(100, 255, 255, 255)), inner)
                    'G.DrawRectangle(New Pen(Color.FromArgb(255 - 255 * Win7Alpha / 300, BackColor, 0.2))), Rect)
                    G.DrawRectangle(New Pen(Color.FromArgb(255 - 255 * Win7Alpha / 300, BackColor.Light(0.2))), InnerWindow_1)
                    G.FillRectangle(New SolidBrush(Color.White), InnerWindow_1)
                    G.DrawRectangle(New Pen(Color.FromArgb(255 - 255 * Win7Alpha / 300, BackColor.Dark(0.2))), InnerWindow_2)
                End If


                If Not ToolWindow Then
                    Dim closeBtn As Image
                    Dim CloseRect As New Rectangle

                    If Active Then
                        closeBtn = My.Resources.Win7_Close_Active
                    Else
                        closeBtn = My.Resources.Win7_Close_inactive
                    End If

                    CloseRect = New Rectangle(Rect.X + Rect.Width - closeBtn.Width - 5, Rect.Y + 1, closeBtn.Width, closeBtn.Height)

                    G.DrawImage(closeBtn, CloseRect)

                Else

                    Dim CloseUpperAccent1 As Color
                    Dim CloseUpperAccent2 As Color
                    Dim CloseLowerAccent1 As Color
                    Dim CloseLowerAccent2 As Color
                    Dim CloseOuterBorder As Color
                    Dim CloseInnerBorder As Color

                    If Active Then
                        CloseUpperAccent1 = Color.FromArgb(233, 169, 156)
                        CloseUpperAccent2 = Color.FromArgb(223, 149, 135)
                        CloseLowerAccent1 = Color.FromArgb(184, 67, 44)
                        CloseLowerAccent2 = Color.FromArgb(210, 127, 110)
                        CloseOuterBorder = Color.FromArgb(67, 20, 34)
                        CloseInnerBorder = Color.FromArgb(100, 255, 255, 255)
                    Else
                        CloseUpperAccent1 = Color.FromArgb(189, 203, 218)
                        CloseLowerAccent2 = Color.FromArgb(205, 219, 234)
                        CloseOuterBorder = Color.FromArgb(131, 142, 168)
                        CloseInnerBorder = Color.FromArgb(209, 219, 229)
                    End If

                    Dim Btn_Height As Integer = Metrics_CaptionHeight + TitleTextH_Sum - 5
                    Dim Btn_Width As Integer = Btn_Height

                    Dim CloseRect As New Rectangle(InnerWindow_1.Right - Btn_Width - 3, Rect.Y + (Sum_Ttl - Btn_Height) / 2, Btn_Width, Btn_Height)

                    If Active Then
                        Dim Factor As Single = 0.5

                        Dim UH As Single = Factor * CloseRect.Height
                        Dim LH As Single = CloseRect.Height - UH
                        Dim Interlapping As Single = (UH / CloseRect.Height) * 10

                        Dim CloseRectUpperHalf As New Rectangle(CloseRect.X, CloseRect.Y, CloseRect.Width, UH + Interlapping)
                        Dim CloseUpperPath As New LinearGradientBrush(CloseRectUpperHalf, CloseUpperAccent1, CloseUpperAccent2, LinearGradientMode.Vertical)

                        Dim CloseRectLowerHalf As New Rectangle(CloseRect.X, CloseRectUpperHalf.Bottom - Interlapping, CloseRect.Width, LH)
                        Dim CloseLowerPath As New LinearGradientBrush(CloseRectLowerHalf, CloseLowerAccent1, CloseLowerAccent2, LinearGradientMode.Vertical)

                        G.FillRoundedRect(CloseUpperPath, CloseRectUpperHalf, 1, True)
                        G.FillRoundedRect(CloseLowerPath, CloseRectLowerHalf, 1, True)
                    Else
                        Dim ClosePath As New LinearGradientBrush(CloseRect, CloseUpperAccent1, CloseLowerAccent2, LinearGradientMode.Vertical)
                        G.FillRectangle(ClosePath, CloseRect)
                    End If


                    Dim CloseBtn As Image

                    If Not ToolWindow Then
                        If CloseRect.Height >= 22 Then
                            CloseBtn = My.Resources.Win7_Basic_Close_2
                        ElseIf CloseRect.Height >= 18 Then
                            CloseBtn = My.Resources.Win7_Basic_Close_1
                        Else
                            CloseBtn = My.Resources.Win7_Basic_Close_0
                        End If
                    Else
                        CloseBtn = My.Resources.Win7_Basic_Close_ToolWindow
                    End If

                    Dim xW As Integer = If(CloseRect.Width Mod 2 = 0, CloseBtn.Width + 1, CloseBtn.Width)
                    Dim xH As Integer = If(CloseRect.Height Mod 2 = 0, CloseBtn.Height + 1, CloseBtn.Height)


                    Dim closerenderrect As New Rectangle(CloseRect.X + (CloseRect.Width - xW) / 2, CloseRect.Y + (CloseRect.Height - xH) / 2, xW, xH)

                    G.DrawImage(CloseBtn, closerenderrect)

                    G.DrawRoundedRect(New Pen(CloseOuterBorder), CloseRect, 1, True)
                    G.DrawRoundedRect(New Pen(CloseInnerBorder), New Rectangle(CloseRect.X + 1, CloseRect.Y + 1, CloseRect.Width - 2, CloseRect.Height - 2), 1, True)

                End If

#End Region

            Else

#Region "Basic"
                G.DrawImage(AdaptedBack, RectBK)

                Sum = Metrics_BorderWidth + Metrics_PaddedBorderWidth
                TitleTextH = "ABCabc0123xYz.#".Measure(Font).Height
                TitleTextH_9 = "ABCabc0123xYz.#".Measure(New Font(Font.Name, 9, Font.Style)).Height
                TitleTextH_Sum = Math.Max(0, TitleTextH - TitleTextH_9 - 5)
                Sum_Ttl = Sum + Metrics_CaptionHeight + TitleTextH_Sum

                With Rect
                    InnerWindow_1 = New Rectangle(.X + Sum + 2, .Y + Sum_Ttl + 3, .Width - (Sum) * 2 - 4, .Height - (Sum + Sum_Ttl) - 5)
                    InnerWindow_2 = New Rectangle(InnerWindow_1.X + 1, InnerWindow_1.Y + 1, InnerWindow_1.Width - 2, InnerWindow_1.Height - 2)
                    RectSide1 = New Rectangle(.X + 1, InnerWindow_1.Y, Sum + 1, InnerWindow_1.Height * 0.5)
                    RectSide2 = New Rectangle(InnerWindow_1.Right - 1, RectSide1.Y, RectSide1.Width + 1, RectSide1.Height)
                End With


                Dim Titlebar_Backcolor1 As Color
                Dim Titlebar_Backcolor2 As Color
                Dim Titlebar_OuterBorder As Color
                Dim Titlebar_InnerBorder As Color
                Dim Titlebar_Turquoise As Color
                Dim OuterBorder As Color

                Dim CloseUpperAccent1 As Color
                Dim CloseUpperAccent2 As Color
                Dim CloseLowerAccent1 As Color
                Dim CloseLowerAccent2 As Color
                Dim CloseOuterBorder As Color
                Dim CloseInnerBorder As Color

                If Active Then
                    Titlebar_Backcolor1 = Color.FromArgb(152, 180, 208)
                    Titlebar_Backcolor2 = Color.FromArgb(186, 210, 234)
                    Titlebar_OuterBorder = Color.FromArgb(52, 52, 52)
                    Titlebar_InnerBorder = Color.FromArgb(255, 255, 255)
                    Titlebar_Turquoise = Color.FromArgb(40, 207, 228)
                    OuterBorder = Color.FromArgb(0, 0, 0)

                    CloseUpperAccent1 = Color.FromArgb(233, 169, 156)
                    CloseUpperAccent2 = Color.FromArgb(223, 149, 135)
                    CloseLowerAccent1 = Color.FromArgb(184, 67, 44)
                    CloseLowerAccent2 = Color.FromArgb(210, 127, 110)
                    CloseOuterBorder = Color.FromArgb(67, 20, 34)
                    CloseInnerBorder = Color.FromArgb(100, 255, 255, 255)

                Else
                    Titlebar_Backcolor1 = Color.FromArgb(191, 205, 219)
                    Titlebar_Backcolor2 = Color.FromArgb(215, 228, 242)
                    Titlebar_OuterBorder = Color.FromArgb(76, 76, 76)
                    Titlebar_InnerBorder = Color.FromArgb(226, 230, 239)
                    Titlebar_Turquoise = Color.FromArgb(226, 230, 239)
                    OuterBorder = Color.FromArgb(76, 76, 76)

                    CloseUpperAccent1 = Color.FromArgb(189, 203, 218)
                    CloseLowerAccent2 = Color.FromArgb(205, 219, 234)
                    CloseOuterBorder = Color.FromArgb(131, 142, 168)
                    CloseInnerBorder = Color.FromArgb(209, 219, 229)
                End If

                Dim UpperPart As New Rectangle(Rect.X, Rect.Y, Rect.Width + 1, Sum_Ttl + 4)

                G.SetClip(UpperPart)

                Dim pth_back As New LinearGradientBrush(UpperPart, Titlebar_Backcolor1, Titlebar_Backcolor2, LinearGradientMode.Vertical)
                Dim pth_line As New LinearGradientBrush(UpperPart, Titlebar_InnerBorder, Titlebar_Turquoise, LinearGradientMode.Vertical)

                '### Render Titlebar
                If Not ToolWindow Then
                    G.FillRoundedRect(pth_back, Rect, Radius, True)
                    G.DrawRoundedRect(New Pen(Titlebar_OuterBorder), Rect, Radius, True)
                    G.DrawRoundedRect(New Pen(Titlebar_InnerBorder), New Rectangle(Rect.X + 1, Rect.Y + 1, Rect.Width - 2, Rect.Height - 2), Radius, True)
                    G.SetClip(New Rectangle(UpperPart.X + UpperPart.Width * 0.75, UpperPart.Y, UpperPart.Width * 0.75, UpperPart.Height))
                    G.DrawRoundedRect(New Pen(pth_line), New Rectangle(Rect.X + 1, Rect.Y + 1, Rect.Width - 2, Rect.Height - 2), Radius, True)
                Else
                    G.FillRectangle(pth_back, Rect)
                    G.DrawRectangle(New Pen(Titlebar_OuterBorder), Rect)
                    G.DrawRectangle(New Pen(Titlebar_InnerBorder), New Rectangle(Rect.X + 1, Rect.Y + 1, Rect.Width - 2, Rect.Height - 2))
                    G.SetClip(New Rectangle(UpperPart.X + UpperPart.Width * 0.75, UpperPart.Y, UpperPart.Width * 0.75, UpperPart.Height))
                    G.DrawRectangle(New Pen(pth_line), New Rectangle(Rect.X + 1, Rect.Y + 1, Rect.Width - 2, Rect.Height - 2))
                End If

                G.ResetClip()
                G.ExcludeClip(UpperPart)

                '### Render Rest of Window
                G.FillRectangle(New SolidBrush(Titlebar_Backcolor2), Rect)
                G.DrawRectangle(New Pen(Titlebar_Turquoise), New Rectangle(Rect.X + 1, Rect.Y + 1, Rect.Width - 2, Rect.Height - 2))
                G.DrawRectangle(New Pen(OuterBorder), Rect)
                G.DrawLine(New Pen(Titlebar_InnerBorder), New Point(Rect.X + 1, Rect.Y), New Point(Rect.X + 1, Rect.Y + Rect.Height - 2))
                If Active Then
                    G.DrawImage(My.Resources.Win7Sides, RectSide1)
                    G.DrawImage(My.Resources.Win7Sides, RectSide2)
                End If
                G.ResetClip()
                G.FillRectangle(Brushes.White, InnerWindow_1)
                G.DrawRectangle(New Pen(Color.FromArgb(186, 210, 234)), InnerWindow_1)
                G.DrawRectangle(New Pen(Color.FromArgb(130, 135, 144)), InnerWindow_2)

                '### Render Close Button
                Dim CloseRect As New Rectangle

                Dim Btn_Height As Integer = Metrics_CaptionHeight + TitleTextH_Sum - 5
                Dim Btn_Width As Integer

                If Not ToolWindow Then
                    Btn_Width = (31 / 17) * Btn_Height
                Else
                    Btn_Width = Btn_Height
                End If

                CloseRect = New Rectangle(InnerWindow_1.Right - Btn_Width - 3, InnerWindow_1.Top - Btn_Height - 3, Btn_Width, Btn_Height)

                If Active Then
                    Dim Factor As Single = 0.45
                    If ToolWindow Then Factor = 0.2

                    Dim UH As Single = Factor * CloseRect.Height
                    Dim LH As Single = CloseRect.Height - UH
                    Dim Interlapping As Single = (UH / CloseRect.Height) * 10

                    Dim CloseRectUpperHalf As New Rectangle(CloseRect.X, CloseRect.Y, CloseRect.Width, UH + Interlapping)
                    Dim CloseUpperPath As New LinearGradientBrush(CloseRectUpperHalf, CloseUpperAccent1, CloseUpperAccent2, LinearGradientMode.Vertical)

                    Dim CloseRectLowerHalf As New Rectangle(CloseRect.X, CloseRectUpperHalf.Bottom - Interlapping, CloseRect.Width, LH)
                    Dim CloseLowerPath As New LinearGradientBrush(CloseRectLowerHalf, CloseLowerAccent1, CloseLowerAccent2, LinearGradientMode.Vertical)

                    G.FillRoundedRect(CloseUpperPath, CloseRectUpperHalf, 1, True)
                    G.FillRoundedRect(CloseLowerPath, CloseRectLowerHalf, 1, True)
                Else
                    Dim ClosePath As New LinearGradientBrush(CloseRect, CloseUpperAccent1, CloseLowerAccent2, LinearGradientMode.Vertical)
                    G.FillRectangle(ClosePath, CloseRect)
                End If


                Dim CloseBtn As Image

                If Not ToolWindow Then
                    If CloseRect.Height >= 22 Then
                        CloseBtn = My.Resources.Win7_Basic_Close_2
                    ElseIf CloseRect.Height >= 18 Then
                        CloseBtn = My.Resources.Win7_Basic_Close_1
                    Else
                        CloseBtn = My.Resources.Win7_Basic_Close_0
                    End If
                Else
                    CloseBtn = My.Resources.Win7_Basic_Close_ToolWindow
                End If


                G.DrawImage(CloseBtn, New Point(CloseRect.X + (CloseRect.Width - CloseBtn.Width) / 2 + 1, CloseRect.Y + (CloseRect.Height - CloseBtn.Height) / 2))

                G.DrawRoundedRect(New Pen(CloseOuterBorder), CloseRect, 1, True)
                G.DrawRoundedRect(New Pen(CloseInnerBorder), New Rectangle(CloseRect.X + 1, CloseRect.Y + 1, CloseRect.Width - 2, CloseRect.Height - 2), 1, True)

                IconRect = New Rectangle(InnerWindow_1.X + 4, CloseRect.Top + (CloseRect.Height - IconSize) / 2, IconSize, IconSize)

                LabelRect = New Rectangle(IconRect.Right + 3, CloseRect.Y, UpperPart.Width - (IconRect.Right + 4), CloseRect.Height)

                If ToolWindow Then LabelRect.X = IconRect.X
#End Region

            End If
#End Region

        End If

        Dim ForeColorX As Color
        If AccentColor_Enabled Then
            If Active Then
                ForeColorX = If(AccentColor_Active.IsDark, Color.White, Color.Black)
            Else
                ForeColorX = If(AccentColor_Inactive.IsDark, Color.FromArgb(115, 115, 115), Color.Black)
            End If
        Else
            If Active Then
                ForeColorX = Color.Black
            Else
                ForeColorX = Color.FromArgb(115, 115, 115)
            End If
        End If

        If Not ToolWindow Then G.DrawImage(If(Active, My.Resources.AppPreview, My.Resources.AppPreviewInActive), IconRect)

        If Preview = Preview_Enum.W11 Or Preview = Preview_Enum.W10 Then
            G.DrawString(Text, Font, New SolidBrush(ForeColorX), LabelRect, StringAligner(ContentAlignment.MiddleLeft))

            If Not ToolWindow Then
                G.DrawString("", New Font("Segoe MDL2 Assets", 7, FontStyle.Regular), New SolidBrush(ForeColorX), XRect, StringAligner(ContentAlignment.MiddleLeft))
            Else
                Dim XXRect As New Rectangle(Rect.X + Rect.Width - 2 - (TitlebarRect.Height - 12), Rect.Y + 6, TitlebarRect.Height - 12, TitlebarRect.Height - 12)
                G.FillRectangle(New SolidBrush(Color.FromArgb(199, 80, 80)), XXRect)
                G.DrawString("r", New Font("Marlett", 6.35, FontStyle.Regular), New SolidBrush(Color.White), New Rectangle(XXRect.X + 1, XXRect.Y + 1, XXRect.Width, XXRect.Height), StringAligner(ContentAlignment.MiddleCenter))
            End If

        ElseIf Preview = Preview_Enum.W8 Then
            G.DrawString(Text, Font, New SolidBrush(Color.Black), LabelRect8, StringAligner(ContentAlignment.MiddleCenter))

        ElseIf Preview = Preview_Enum.W8Lite Then
            If Active Then
                G.DrawString(Text, Font, New SolidBrush(MainFrm.CP.Win32.TitleText), LabelRect8, StringAligner(ContentAlignment.MiddleCenter))
            Else
                G.DrawString(Text, Font, New SolidBrush(MainFrm.CP.Win32.InactiveTitleText), LabelRect8, StringAligner(ContentAlignment.MiddleCenter))
            End If

        ElseIf Preview = Preview_Enum.W7Aero Or Preview = Preview_Enum.W7Opaque Then
            Dim LabelRectModified As Rectangle = LabelRect
            LabelRectModified.X -= 2
            LabelRectModified.Y -= 1
            Dim alpha As Integer = If(Active, 120, 75)
            G.DrawGlowString(1, Text, Me, Color.Black, Color.FromArgb(alpha, Color.White), LabelRectModified, StringAligner(ContentAlignment.MiddleLeft))

        ElseIf Preview = Preview_Enum.W7Basic Then
            G.DrawString(Text, Font, New SolidBrush(If(Active, Color.Black, Color.FromArgb(76, 76, 76))), LabelRect, StringAligner(ContentAlignment.MiddleLeft))

        End If

    End Sub

    Public Sub FillSemiRect(ByVal [Graphics] As Graphics, ByVal [Brush] As Brush, ByVal [Rectangle] As Rectangle, Optional ByVal [Radius] As Integer = -1)
        Try
            If [Radius] = -1 Then [Radius] = 6

            If Graphics Is Nothing Then Throw New ArgumentNullException("graphics")
            [Graphics].SmoothingMode = SmoothingMode.AntiAlias

            Using path As GraphicsPath = RoundedSemiRectangle(Rectangle, Radius)
                Graphics.FillPath(Brush, path)
            End Using

        Catch
        End Try
    End Sub

    Public Function RoundedSemiRectangle(ByVal r As Rectangle, ByVal radius As Integer) As GraphicsPath
        Try
            Dim path As New GraphicsPath()
            Dim d As Integer = radius * 2

            path.AddLine(r.Left + d, r.Top, r.Right - d, r.Top)
            path.AddArc(Rectangle.FromLTRB(r.Right - d, r.Top, r.Right, r.Top + d), -90, 90)

            path.AddLine(r.Right, r.Top, r.Right, r.Bottom)

            path.AddLine(r.Right, r.Bottom, r.Left, r.Bottom)

            path.AddLine(r.Left, r.Bottom - d, r.Left, r.Top + d)
            path.AddArc(Rectangle.FromLTRB(r.Left, r.Top, r.Left + d, r.Top + d), 180, 90)

            path.CloseFigure()
            Return path
        Catch
            Return Nothing
        End Try
    End Function

    Private Sub XenonWindow_HandleCreated(sender As Object, e As EventArgs) Handles Me.HandleCreated

        If Not DesignMode Then
            Try : AddHandler Parent.BackgroundImageChanged, AddressOf ProcessBack : Catch : End Try
            Try : AddHandler FindForm.Load, AddressOf ProcessBack : Catch : End Try
            Try : AddHandler SizeChanged, AddressOf ProcessBack : Catch : End Try
            Try : AddHandler LocationChanged, AddressOf ProcessBack : Catch : End Try
            Try : AddHandler PaddingChanged, AddressOf ProcessBack : Catch : End Try
            Try : AddHandler FontChanged, AddressOf AdjustPadding : Catch : End Try
        End If

        ProcessBack()
    End Sub

    Sub ProcessBack()
        Dim Win7 As Boolean = (Preview = Preview_Enum.W7Aero Or Preview = Preview_Enum.W7Opaque Or Preview = Preview_Enum.W7Basic)
        If Win7 Or (Not Win7 And AdaptedBack Is Nothing) Then
            Try : AdaptedBack = My.Wallpaper.Clone(Bounds, My.Wallpaper.PixelFormat) : Catch : End Try
            Try : AdaptedBackBlurred = New Bitmap(AdaptedBack).Blur(1) : Catch : End Try
            Try : Noise7 = My.Resources.AeroGlass.Fade(Win7Noise / 100) : Catch : End Try
        Else
            Try : AdaptedBack = My.Wallpaper.Clone(Bounds, My.Wallpaper.PixelFormat) : Catch : End Try
        End If
    End Sub
End Class

<DefaultEvent("Scroll")>
Public Class XenonTrackbar
    Inherits Control

    Event Scroll(ByVal sender As Object)

#Region "Properties"
    Private _Minimum As Integer
    Property Minimum() As Integer
        Get
            Return _Minimum
        End Get
        Set(ByVal value As Integer)
            If value < 0 Then
                Throw New Exception("Property value is not valid.")
            End If

            _Minimum = value
            If value > _Value Then _Value = value
            If value > _Maximum Then _Maximum = value

            InvalidateLayout()
        End Set
    End Property

    Private _Maximum As Integer = 100
    Property Maximum() As Integer
        Get
            Return _Maximum
        End Get
        Set(ByVal value As Integer)
            If value < 0 Then
                Throw New Exception("Property value is not valid.")
            End If

            _Maximum = value
            If value < _Value Then _Value = value
            If value < _Minimum Then _Minimum = value

            InvalidateLayout()
        End Set
    End Property

    Private _Value As Integer
    Property Value() As Integer
        Get

            Return _Value
        End Get
        Set(ByVal value As Integer)
            If value = _Value Then Return

            If value > _Maximum Then
                value = _Maximum
            End If

            If value < _Minimum Then
                value = _Minimum
            End If

            _Value = value
            InvalidatePosition()

            RaiseEvent Scroll(Me)
        End Set
    End Property

    Private _SmallChange As Integer = 1
    Public Property SmallChange() As Integer
        Get
            Return _SmallChange
        End Get
        Set(ByVal value As Integer)
            If value < 1 Then
                Throw New Exception("Property value is not valid.")
            End If

            _SmallChange = value
        End Set
    End Property

    Private _LargeChange As Integer = 10
    Public Property LargeChange() As Integer
        Get
            Return _LargeChange
        End Get
        Set(ByVal value As Integer)
            If value < 1 Then
                Throw New Exception("Property value is not valid.")
            End If

            _LargeChange = value
        End Set
    End Property
#End Region

    ReadOnly ButtonSize As Integer = 0
    ReadOnly ThumbSize As Integer = 35 ' 14 minimum
    Private LSA As Rectangle
    Private RSA As Rectangle
    Private Shaft As Rectangle
    Private Thumb As Rectangle
    Private ThumbDown As Boolean
    Private Circle As Rectangle
    Private _Shown As Boolean = False

    Enum MouseState
        None
        Over
        Down
    End Enum

#Region "Animator"
    Dim alpha As Integer
    ReadOnly Factor As Integer = 25
    Dim WithEvents Tmr As New Timer With {.Enabled = False, .Interval = 1}

    Private Sub Tmr_Tick(sender As Object, e As EventArgs) Handles Tmr.Tick
        If Not DesignMode Then

            If State = MouseState.Over And _Shown Then
                If alpha + Factor <= 255 Then
                    alpha += Factor
                ElseIf alpha + Factor > 255 Then
                    alpha = 255
                    Tmr.Enabled = False
                    Tmr.Stop()
                End If

                Threading.Thread.Sleep(1)
                Invalidate()
            End If

            If _Shown And (Not State = MouseState.Over Or State = MouseState.Down) Then
                If alpha - Factor >= 0 Then
                    alpha -= Factor
                ElseIf alpha - Factor < 0 Then
                    alpha = 0
                    Tmr.Enabled = False
                    Tmr.Stop()
                End If

                Threading.Thread.Sleep(1)
                Invalidate()
            End If
        End If
    End Sub
#End Region

    Public State As MouseState = MouseState.None

    Sub New()
        SetStyle(DirectCast(139286, ControlStyles), True)
        SetStyle(ControlStyles.Selectable, False)
        Height = 19
        Text = ""
    End Sub

    Dim I1 As Integer

    Protected Overrides Sub OnPaint(e As System.Windows.Forms.PaintEventArgs)
        Dim G As Graphics = e.Graphics
        G.SmoothingMode = SmoothingMode.HighQuality
        G.TextRenderingHint = TextRenderingHint.AntiAliasGridFit

        G.Clear(BackColor)
        Dim Dark As Boolean = GetDarkMode()
        Dim c_back As Color = If(Dark, Color.FromArgb(60, 60, 60), Color.FromArgb(210, 210, 210))
        Dim c_btn As Color = If(Dark, Color.FromArgb(165, 165, 165), Color.FromArgb(100, 100, 100))

        Dim C As Color = Style.Colors.Core

        Dim middleRect As New Rectangle(0, (Height - (Height * 0.25)) / 2, Width - 1, Height * 0.25)

        G.FillRoundedRect(New SolidBrush(c_back), middleRect)

        Circle = New Rectangle((Value / Maximum) * Shaft.Width, 0, Height - 1, Height - 1)

        With Thumb
            G.FillRoundedRect(New SolidBrush(C), New Rectangle(.X + 1, middleRect.Y, Circle.Left + Circle.Width / 2, middleRect.Height))
        End With

        G.FillRectangle(New SolidBrush(BackColor), New Rectangle(-1, 0, 4, Height))

        G.FillRectangle(New SolidBrush(BackColor), New Rectangle(Width - 4, 0, 4, Height))

        G.FillEllipse(New SolidBrush(Style.Colors.Border), Circle)

        Dim smallC1 As New Rectangle(Circle.X + 5, Circle.Y + 5, Circle.Width - 10, Circle.Height - 10)
        Dim smallC2 As New Rectangle(Circle.X + 4, Circle.Y + 4, Circle.Width - 8, Circle.Height - 8)

        G.FillEllipse(New SolidBrush(C), smallC1)
        G.FillEllipse(New SolidBrush(Color.FromArgb(alpha, C)), smallC2)
    End Sub

    Protected Overrides Sub OnSizeChanged(e As EventArgs)
        Height = 19
        InvalidateLayout()
    End Sub

    Private Sub InvalidateLayout()
        LSA = New Rectangle(0, 0, ButtonSize, Height)
        RSA = New Rectangle(Width - ButtonSize, 0, ButtonSize, Height)
        Shaft = New Rectangle(LSA.Right + 1 + 0.5 * Height, 0, Width - Height - 1, Height)
        Thumb = New Rectangle(0, 1, (Value / Maximum) * Shaft.Width, Height - 3)
        Circle = New Rectangle((Value / Maximum) * Shaft.Width, 0, Height - 1, Height - 1)
        RaiseEvent Scroll(Me)
        InvalidatePosition()
    End Sub

    Private Sub InvalidatePosition()
        Thumb.Width = (Value / Maximum) * Width
        Refresh()
    End Sub

    Protected Overrides Sub OnMouseDown(ByVal e As MouseEventArgs)
        If e.Button = Windows.Forms.MouseButtons.Left Then
            State = MouseState.Down
            Tmr.Enabled = True
            Tmr.Start()

            If Circle.Contains(e.Location) Then
                ThumbDown = True
                Return
            Else
                If e.X < Circle.X Then

                    I1 = _Value - _LargeChange
                Else
                    I1 = _Value + _LargeChange
                End If
            End If

            Value = Math.Min(Math.Max(I1, _Minimum), _Maximum)

            InvalidatePosition()
        End If
    End Sub

    Protected Overrides Sub OnMouseMove(ByVal e As MouseEventArgs)
        If Circle.Contains(e.Location) And Not e.Button = MouseButtons.Left Then
            State = MouseState.Over
        Else
            If e.Button = MouseButtons.Left Then State = MouseState.Down Else State = MouseState.None
        End If


        Invalidate()

        If ThumbDown Then
            'Dim ThumbPosition As Integer = e.X '- LSA.Width - (ThumbSize \ 2)
            'Dim ThumbBounds As Integer = Shaft.Width - ThumbSize
            'I1 = CInt((ThumbPosition / ThumbBounds) * (_Maximum - _Minimum)) + _Minimum
            'Value = Math.Min(Math.Max(I1, _Minimum), _Maximum)

            Value = Math.Min(Math.Max((e.X / Width) * Maximum, _Minimum), _Maximum)
            InvalidatePosition()
        End If

        Tmr.Enabled = True
        Tmr.Start()
    End Sub

    Private Function GetProgress() As Double
        Return (_Value - _Minimum) / (_Maximum - _Minimum)
    End Function

    Private Sub NSVScrollBar_MouseUp(sender As Object, e As MouseEventArgs) Handles Me.MouseUp
        ThumbDown = False
        State = MouseState.None
        Tmr.Enabled = True
        Tmr.Start()
        Invalidate()
    End Sub

    Private Sub XenonScrollBarV_MouseEnter(sender As Object, e As EventArgs) Handles Me.MouseEnter
        If Thumb.Contains(MousePosition) Then
            State = MouseState.Over
            Invalidate()
            Tmr.Enabled = True
            Tmr.Start()
        End If
    End Sub

    Private Sub XenonScrollBarV_MouseLeave(sender As Object, e As EventArgs) Handles Me.MouseLeave
        State = MouseState.None
        Tmr.Enabled = True
        Tmr.Start()
        Invalidate()
    End Sub

    Private Sub XenonScrollBarV_MouseWheel(sender As Object, e As MouseEventArgs) Handles Me.MouseWheel
        If e.Delta < 0 Then
            If Value < Maximum Then
                If e.Delta <= -240 Then Value += LargeChange Else Value += SmallChange
            End If
        Else
            If Value > Minimum Then
                If e.Delta >= 240 Then Value -= LargeChange Else Value -= SmallChange
            End If
        End If
    End Sub

    Private Sub XenonRadioButton_HandleCreated(sender As Object, e As EventArgs) Handles Me.HandleCreated

        Try
            If Not DesignMode Then
                AddHandler FindForm.Load, AddressOf Loaded
                AddHandler FindForm.Shown, AddressOf Showed
                AddHandler Parent.BackColorChanged, AddressOf RefreshColorPalette
                AddHandler Parent.VisibleChanged, AddressOf RefreshColorPalette
                AddHandler Parent.EnabledChanged, AddressOf RefreshColorPalette
                AddHandler VisibleChanged, AddressOf RefreshColorPalette
                AddHandler EnabledChanged, AddressOf RefreshColorPalette
            End If
        Catch
        End Try

        Try
            alpha = 0

        Catch
        End Try
    End Sub

    Sub Loaded()
        _Shown = False
    End Sub

    Sub Showed()
        _Shown = True

        Invalidate()
    End Sub

    Public Sub RefreshColorPalette()

        Invalidate()
    End Sub
End Class

<DefaultEvent("Click")>
Public Class XenonCMD
    Inherits ContainerControl
    Public Property CMD_ColorTable00 As Color
    Public Property CMD_ColorTable01 As Color
    Public Property CMD_ColorTable02 As Color
    Public Property CMD_ColorTable03 As Color
    Public Property CMD_ColorTable04 As Color
    Public Property CMD_ColorTable05 As Color
    Public Property CMD_ColorTable06 As Color
    Public Property CMD_ColorTable07 As Color
    Public Property CMD_ColorTable08 As Color
    Public Property CMD_ColorTable09 As Color
    Public Property CMD_ColorTable10 As Color
    Public Property CMD_ColorTable11 As Color
    Public Property CMD_ColorTable12 As Color
    Public Property CMD_ColorTable13 As Color
    Public Property CMD_ColorTable14 As Color
    Public Property CMD_ColorTable15 As Color
    Public Property CMD_ScreenColorsForeground As Integer = 7
    Public Property CMD_ScreenColorsBackground As Integer = 0
    Public Property CMD_PopupForeground As Integer = 15
    Public Property CMD_PopupBackground As Integer = 5
    Public Property PowerShell As Boolean = False
    Public Property Raster As Boolean = True
    Public Property RasterSize As Raster_Sizes = Raster_Sizes._8x12

    Public Property CustomTerminal As Boolean = False

    ReadOnly S1 As String = "(c) Microsoft Corporation. All rights reserved."
    ReadOnly S2 As String = Environment.GetFolderPath(Environment.SpecialFolder.Windows).Replace("WINDOWS", "Windows") & "\System32" & ">"
    ReadOnly CV As String = "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion"

    Enum Raster_Sizes
        _4x6
        _6x8
        _8x8
        _16x8
        _5x12
        _7x12
        _8x12
        _16x12
        _12x16
        _10x18
    End Enum

    Sub New()
        Text = ""
        DoubleBuffered = True
    End Sub

    Protected Overrides Sub OnPaint(e As System.Windows.Forms.PaintEventArgs)
        Dim G As Graphics = e.Graphics
        G.SmoothingMode = SmoothingMode.AntiAlias
        G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit

        DoubleBuffered = True

        Dim Rect As New Rectangle(0, 0, Width - 1, Height - 1)
        Dim RectCMD As New Rectangle(Rect.X + 1, Rect.Y + 5, Rect.Width - 2, Rect.Height - 10)

        Dim pW0, pH0, pX0, pY0 As Integer
        pW0 = 240 * (Font.Size / 18)
        pH0 = 54 * (Font.Size / 18)
        pX0 = 5 * (Font.Size / 18)
        pY0 = 10 * (Font.Size / 18)

        Dim RectMiddle As New Rectangle(Rect.X + (Rect.Width - pW0) / 2, Rect.Y + (Rect.Height - pH0) / 2, pW0, pH0)
        Dim RectMiddleBorder As New Rectangle(RectMiddle.X + pX0, RectMiddle.Y + pY0, RectMiddle.Width - pX0 * 2, RectMiddle.Height - pY0 * 2)

        Dim FC, BK, PCF, PCB As Color
        Dim S As String

        Dim F As Font = Font

        If Not Raster Then
            If Not PowerShell Then
                F = New Font(Font.Name, If(Font.Size * 0.6 <= 0, 1, CSng(Font.Size * 0.6)), Font.Style)
            Else
                F = New Font(Font.Name, If(Font.Size * 0.6 <= 0, 1, CSng(Font.Size * 0.6)), Font.Style)
            End If
        End If

        Select Case CMD_ScreenColorsForeground
            Case 0
                If CMD_ScreenColorsForeground = 0 And CMD_ScreenColorsBackground = 0 Then
                    FC = CMD_ColorTable07
                Else
                    FC = CMD_ColorTable00
                End If
            Case 1
                FC = CMD_ColorTable01
            Case 2
                FC = CMD_ColorTable02
            Case 3
                FC = CMD_ColorTable03
            Case 4
                FC = CMD_ColorTable04
            Case 5
                FC = CMD_ColorTable05
            Case 6
                FC = CMD_ColorTable06
            Case 7
                FC = CMD_ColorTable07
            Case 8
                FC = CMD_ColorTable08
            Case 9
                FC = CMD_ColorTable09
            Case 10
                FC = CMD_ColorTable10
            Case 11
                FC = CMD_ColorTable11
            Case 12
                FC = CMD_ColorTable12
            Case 13
                FC = CMD_ColorTable13
            Case 14
                FC = CMD_ColorTable14
            Case 15
                FC = CMD_ColorTable15
        End Select

        Select Case CMD_ScreenColorsBackground
            Case 0
                BK = CMD_ColorTable00
            Case 1
                BK = CMD_ColorTable01
            Case 2
                BK = CMD_ColorTable02
            Case 3
                BK = CMD_ColorTable03
            Case 4
                BK = CMD_ColorTable04
            Case 5
                BK = CMD_ColorTable05
            Case 6
                BK = CMD_ColorTable06
            Case 7
                BK = CMD_ColorTable07
            Case 8
                BK = CMD_ColorTable08
            Case 9
                BK = CMD_ColorTable09
            Case 10
                BK = CMD_ColorTable10
            Case 11
                BK = CMD_ColorTable11
            Case 12
                BK = CMD_ColorTable12
            Case 13
                BK = CMD_ColorTable13
            Case 14
                BK = CMD_ColorTable14
            Case 15
                BK = CMD_ColorTable15
        End Select

        Select Case CMD_PopupForeground
            Case 0
                PCF = CMD_ColorTable00
            Case 1
                PCF = CMD_ColorTable01
            Case 2
                PCF = CMD_ColorTable02
            Case 3
                PCF = CMD_ColorTable03
            Case 4
                PCF = CMD_ColorTable04
            Case 5
                PCF = CMD_ColorTable05
            Case 6
                PCF = CMD_ColorTable06
            Case 7
                PCF = CMD_ColorTable07
            Case 8
                PCF = CMD_ColorTable08
            Case 9
                PCF = CMD_ColorTable09
            Case 10
                PCF = CMD_ColorTable10
            Case 11
                PCF = CMD_ColorTable11
            Case 12
                PCF = CMD_ColorTable12
            Case 13
                PCF = CMD_ColorTable13
            Case 14
                PCF = CMD_ColorTable14
            Case 15
                PCF = CMD_ColorTable15
        End Select

        Select Case CMD_PopupBackground
            Case 0
                PCB = CMD_ColorTable00
            Case 1
                PCB = CMD_ColorTable01
            Case 2
                PCB = CMD_ColorTable02
            Case 3
                PCB = CMD_ColorTable03
            Case 4
                PCB = CMD_ColorTable04
            Case 5
                PCB = CMD_ColorTable05
            Case 6
                PCB = CMD_ColorTable06
            Case 7
                PCB = CMD_ColorTable07
            Case 8
                PCB = CMD_ColorTable08
            Case 9
                PCB = CMD_ColorTable09
            Case 10
                PCB = CMD_ColorTable10
            Case 11
                PCB = CMD_ColorTable11
            Case 12
                PCB = CMD_ColorTable12
            Case 13
                PCB = CMD_ColorTable13
            Case 14
                PCB = CMD_ColorTable14
            Case 15
                PCB = CMD_ColorTable15
        End Select

        BackColor = BK
        G.Clear(BK)

        If Not CustomTerminal Then
            If Not PowerShell Then
                Dim sx As String = System.Runtime.InteropServices.RuntimeInformation.OSDescription.Replace("Microsoft Windows ", "")
                sx = sx.Replace("S", "").Trim

                Dim sy As String = "." & Microsoft.Win32.Registry.GetValue(CV, "UBR", 0).ToString
                If sy = ".0" Then sy = ""

                S = String.Format("Microsoft Windows [Version {0}{1}]", sx, sy) & vbCrLf & S1 & vbCrLf & vbCrLf & S2

            Else
                S = "Windows PowerShell" & vbCrLf & S1 & vbCrLf & vbCrLf & "Install the latest PowerShell for new features and improvements! https://aka.ms/PSWindows" & vbCrLf & vbCrLf & "PS " & S2
            End If
        Else
            S = "This is just an preview to your custom terminal." & vbCrLf & vbCrLf & S2
        End If


        If Raster Then
            S &= vbCrLf & vbCrLf & "*Note: Raster Font will look different from the preview."
        End If

        If Not Raster Then
            G.DrawString(S, F, New SolidBrush(FC), RectCMD.Location)

            G.FillRectangle(New SolidBrush(PCB), RectMiddle)
            G.DrawRectangle(New Pen(PCF), RectMiddleBorder)

            G.DrawString("This is a pop-up", F, New SolidBrush(PCF), RectMiddleBorder, StringAligner(ContentAlignment.MiddleCenter))

        Else
            Dim i0, i1 As Bitmap
            Dim pW, pH, pX, pY As Integer

            Select Case RasterSize
                Case Raster_Sizes._4x6
                    If Not PowerShell Then i0 = My.Resources.CMD_4x6 Else i0 = My.Resources.PS_4x6
                    i1 = My.Resources.CMD_4x6_P
                    pW = 120
                    pH = 18
                    pX = 2
                    pY = 3

                Case Raster_Sizes._6x8
                    If Not PowerShell Then i0 = My.Resources.CMD_6x8 Else i0 = My.Resources.PS_6x8
                    i1 = My.Resources.CMD_6x8_P
                    pW = 180
                    pH = 24
                    pX = 3
                    pY = 4

                Case Raster_Sizes._8x8
                    If Not PowerShell Then i0 = My.Resources.CMD_8x8 Else i0 = My.Resources.PS_8x8
                    i1 = My.Resources.CMD_8x8_P
                    pW = 240
                    pH = 24
                    pX = 4
                    pY = 4

                Case Raster_Sizes._16x8
                    If Not PowerShell Then i0 = My.Resources.CMD_16x8 Else i0 = My.Resources.PS_16x8
                    i1 = My.Resources.CMD_16x8_P
                    pW = 480
                    pH = 24
                    pX = 8
                    pY = 4

                Case Raster_Sizes._5x12
                    If Not PowerShell Then i0 = My.Resources.CMD_5x12 Else i0 = My.Resources.PS_5x12
                    i1 = My.Resources.CMD_5x12_P
                    pW = 150
                    pH = 36
                    pX = 3
                    pY = 6

                Case Raster_Sizes._7x12
                    If Not PowerShell Then i0 = My.Resources.CMD_7x12 Else i0 = My.Resources.PS_7x12
                    i1 = My.Resources.CMD_7x12_P
                    pW = 210
                    pH = 36
                    pX = 4
                    pY = 6

                Case Raster_Sizes._8x12
                    If Not PowerShell Then i0 = My.Resources.CMD_8x12 Else i0 = My.Resources.PS_8x12
                    i1 = My.Resources.CMD_8x12_P
                    pW = 240
                    pH = 36
                    pX = 4
                    pY = 6

                Case Raster_Sizes._16x12
                    If Not PowerShell Then i0 = My.Resources.CMD_16x12 Else i0 = My.Resources.PS_16x12
                    i1 = My.Resources.CMD_16x12_P
                    pW = 480
                    pH = 36
                    pX = 8
                    pY = 6

                Case Raster_Sizes._12x16
                    If Not PowerShell Then i0 = My.Resources.CMD_12x16 Else i0 = My.Resources.PS_12x16
                    i1 = My.Resources.CMD_12x16_P
                    pW = 360
                    pH = 48
                    pX = 6
                    pY = 8

                Case Raster_Sizes._10x18
                    If Not PowerShell Then i0 = My.Resources.CMD_10x18 Else i0 = My.Resources.PS_10x18
                    i1 = My.Resources.CMD_10x18_P
                    pW = 300
                    pH = 54
                    pX = 8
                    pY = 9

                Case Else
                    If Not PowerShell Then i0 = My.Resources.CMD_8x12 Else i0 = My.Resources.PS_8x12
                    i1 = My.Resources.CMD_8x12_P
                    pW = 240
                    pH = 36
                    pX = 4
                    pY = 6

            End Select

            G.DrawImage(i0.ReplaceColor(Color.FromArgb(204, 204, 204), FC), New Point(0, 1))

            RectMiddle = New Rectangle(Rect.X + (Rect.Width - pW) / 2, Rect.Y + (Rect.Height - 36) / 2, pW, pH)
            RectMiddleBorder = New Rectangle(RectMiddle.X + pX, RectMiddle.Y + pY, RectMiddle.Width - pX * 2, RectMiddle.Height - pY * 2)

            G.FillRectangle(New SolidBrush(PCB), RectMiddle)
            G.DrawRectangle(New Pen(PCF), RectMiddleBorder)


            G.DrawImage(i1.ReplaceColor(Color.FromArgb(204, 204, 204), PCF), New Point(RectMiddle.X + (RectMiddle.Width - i1.Width) / 2, RectMiddle.Y + (RectMiddle.Height - i1.Height) / 2))

        End If
    End Sub
End Class

<DefaultEvent("Click")>
Public Class XenonTerminal
    Inherits ContainerControl

    Dim Noise As New TextureBrush(My.Resources.GaussianBlur.Fade(0.15))
    Dim adaptedBack As Bitmap
    Dim adaptedBackBlurred As Bitmap

    Private _Opacity As Single = 1
    Public Event OpacityChanged As PropertyChangedEventHandler
    Private Sub NotifyOpacityChanged(ByVal info As Single)
        Invalidate()
        RaiseEvent OpacityChanged(Me, New PropertyChangedEventArgs(info))
    End Sub
    Public Property Opacity() As Single
        Get
            Return _Opacity
        End Get

        Set(ByVal value As Single)
            If Not (value = _Opacity) Then
                Me._Opacity = value
                NotifyOpacityChanged(_Opacity)
            End If
        End Set
    End Property

    Private _OpacityBackImage As Single = 100
    Public Event OpacityBackImageChanged As PropertyChangedEventHandler
    Private Sub NotifyOpacityBackImageChanged(ByVal info As Single)
        Invalidate()
        RaiseEvent OpacityBackImageChanged(Me, New PropertyChangedEventArgs(info))
    End Sub
    Public Property OpacityBackImage() As Single
        Get
            Return _OpacityBackImage
        End Get

        Set(ByVal value As Single)
            If Not (value = _OpacityBackImage) Then
                Me._OpacityBackImage = value
                NotifyOpacityBackImageChanged(_OpacityBackImage)
            End If
        End Set
    End Property

    Private _BackImage As Image
    Public Event BackImageChanged As PropertyChangedEventHandler
    Private Sub NotifyBackImageChanged(ByVal info As Object)
        Invalidate()
        UpdateOpacityBackImageChanged()
        RaiseEvent BackImageChanged(Me, New PropertyChangedEventArgs(info))
    End Sub
    Public Property BackImage() As Image
        Get
            Return _BackImage
        End Get

        Set(ByVal value As Image)
            If Not (value Is _BackImage) Then
                Me._BackImage = value
                NotifyBackImageChanged(_BackImage)
            End If
        End Set
    End Property

    Public Property Color_Titlebar As Color = Color.FromArgb(0, 0, 0, 0)
    Public Property Color_Titlebar_Unfocused As Color = Color.FromArgb(0, 0, 0, 0)
    Public Property Color_TabFocused As Color = Color.FromArgb(0, 0, 0, 0)
    Public Property Color_TabUnFocused As Color = Color.FromArgb(0, 0, 0, 0)
    Public Property Color_Background As Color = Color.Black
    Public Property Color_Foreground As Color = Color.White
    Public Property Color_Selection As Color = Color.Gray
    Public Property Color_Cursor As Color = Color.White
    Public Property CursorType As CursorShape_Enum = CursorShape_Enum.bar
    Public Property CursorHeight As Integer = 25
    Public Property Light As Boolean = False
    Public Property UseAcrylicOnTitlebar As Boolean = False
    Public Property UseAcrylic As Boolean = False
    Public Property TabTitle As String = ""
    Public Property TabIcon As Image
    Public Property TabColor As Color = Color.FromArgb(0, 0, 0, 0)

    Public Property PreviewVersion As Boolean = True
    Public Property TabIconButItIsString As String = ""
    Public Property IsFocused As Boolean = True
    Enum CursorShape_Enum
        bar
        doubleUnderscore
        emptyBox
        filledBox
        underscore
        vintage
    End Enum

    Public Function RR(ByVal r As Rectangle, ByVal radius As Integer) As GraphicsPath
        Try
            Dim path As New GraphicsPath()
            Dim d As Integer = radius * 2
            Dim f0 As Single = 0.5
            Dim f1 As Single = 2 - f0

            Dim R1 As New Rectangle(r.X + f0 * d, r.Y, d, d)
            Dim R2 As New Rectangle(r.X + r.Width - f1 * d, r.Y, d, d)
            Dim R3 As New Rectangle(r.X - f0 * d, r.Y + r.Height - f0 * d, d, f0 * d)
            Dim R4 As New Rectangle(r.X + r.Width - f0 * d, r.Y + r.Height - f0 * d, d, f0 * d)

            path.AddArc(R4, 90, 90)
            path.AddLine(New Point(R4.X, R4.Y), New Point(R2.Right, R2.Bottom))
            path.AddArc(R2, 0, -90)
            path.AddArc(R1, -90, -90)
            path.AddArc(R3, 0, 90)
            path.AddLine(New Point(R3.X + R3.Width, R3.Y + R3.Height), New Point(R4.X, R4.Y + R4.Height))

            path.CloseFigure()

            Return path
        Catch
            Return Nothing
        End Try
    End Function

    Public Function RRNoLine(ByVal r As Rectangle, ByVal radius As Integer) As GraphicsPath
        Try
            Dim path As New GraphicsPath()
            Dim d As Integer = radius * 2
            Dim f0 As Single = 0.5
            Dim f1 As Single = 2 - f0

            Dim R1 As New Rectangle(r.X + f0 * d, r.Y, d, d)
            Dim R2 As New Rectangle(r.X + r.Width - f1 * d, r.Y, d, d)
            Dim R3 As New Rectangle(r.X - f0 * d, r.Y + r.Height - f0 * d, d, f0 * d)
            Dim R4 As New Rectangle(r.X + r.Width - f0 * d, r.Y + r.Height - f0 * d, d, f0 * d)

            path.AddArc(R4, 90, 90)
            path.AddLine(New Point(R4.X, R4.Y), New Point(R2.Right, R2.Bottom))
            path.AddArc(R2, 0, -90)
            path.AddArc(R1, -90, -90)
            path.AddArc(R3, 0, 90)
            path.AddLine(New Point(R3.X + R3.Width, R3.Y + R3.Height), New Point(R4.X, R4.Y + R4.Height))

            path.CloseFigure()

            Return path
        Catch
            Return Nothing
        End Try
    End Function
    Public Sub FillSemiRect(ByVal [Graphics] As Graphics, ByVal [Brush] As Brush, ByVal [Rectangle] As Rectangle, Optional ByVal [Radius] As Integer = -1)
        Try
            If [Radius] = -1 Then [Radius] = 6

            If Graphics Is Nothing Then Throw New ArgumentNullException("graphics")

            Using path As GraphicsPath = RoundedSemiRectangle(Rectangle, Radius)
                Graphics.FillPath(Brush, path)
            End Using

        Catch
        End Try
    End Sub

    Public Sub FillRect(ByVal [Graphics] As Graphics, ByVal [Brush] As Brush, ByVal [Rectangle] As Rectangle, Optional ByVal [Radius] As Integer = -1, Optional ByVal ForcedRoundCorner As Boolean = False)
        Try
            If [Radius] = -1 Then [Radius] = 6

            If Graphics Is Nothing Then Throw New ArgumentNullException("graphics")

            If (GetRoundedCorners() Or ForcedRoundCorner) And [Radius] > 0 Then
                Using path As GraphicsPath = Rectangle.Round(Radius)
                    Graphics.FillPath(Brush, path)
                End Using
            Else
                Graphics.FillRectangle(Brush, [Rectangle])
            End If
        Catch
        End Try
    End Sub
    Public Sub FillImg(ByVal [Graphics] As Graphics, ByVal [Image] As Image, ByVal [Rectangle] As Rectangle, Optional ByVal [Radius] As Integer = -1, Optional ByVal ForcedRoundCorner As Boolean = False)
        Try
            If [Radius] = -1 Then [Radius] = 6

            If Graphics Is Nothing Then Throw New ArgumentNullException("graphics")

            If (GetRoundedCorners() Or ForcedRoundCorner) And [Radius] > 0 Then
                Using path As GraphicsPath = Rectangle.Round(Radius)
                    Dim reg As New Region(path)
                    [Graphics].Clip = reg
                    [Graphics].DrawImage([Image], [Rectangle])
                    [Graphics].ResetClip()
                End Using
            Else
                Graphics.DrawImage([Image], [Rectangle])
            End If
        Catch
        End Try
    End Sub
    Public Sub DrawRect(ByVal [Graphics] As Graphics, ByVal [Pen] As Pen, ByVal [Rectangle] As Rectangle, Optional ByVal [Radius_willbe_x2] As Integer = -1, Optional ByVal ForcedRoundCorner As Boolean = False)
        Try
            If [Radius_willbe_x2] = -1 Then [Radius_willbe_x2] = 6
            [Radius_willbe_x2] *= 2

            If (GetRoundedCorners() Or ForcedRoundCorner) And [Radius_willbe_x2] > 0 Then
                [Graphics].DrawArc([Pen], [Rectangle].X, [Rectangle].Y, [Radius_willbe_x2], [Radius_willbe_x2], 180, 90)
                [Graphics].DrawLine([Pen], CInt([Rectangle].X + [Radius_willbe_x2] / 2), [Rectangle].Y, CInt([Rectangle].X + [Rectangle].Width - [Radius_willbe_x2] / 2), [Rectangle].Y)
                [Graphics].DrawArc([Pen], [Rectangle].X + [Rectangle].Width - [Radius_willbe_x2], [Rectangle].Y, [Radius_willbe_x2], [Radius_willbe_x2], 270, 90)
                [Graphics].DrawLine([Pen], [Rectangle].X, CInt([Rectangle].Y + [Radius_willbe_x2] / 2), [Rectangle].X, CInt([Rectangle].Y + [Rectangle].Height - [Radius_willbe_x2] / 2))
                [Graphics].DrawLine([Pen], CInt([Rectangle].X + [Rectangle].Width), CInt([Rectangle].Y + [Radius_willbe_x2] / 2), CInt([Rectangle].X + [Rectangle].Width), CInt([Rectangle].Y + [Rectangle].Height - [Radius_willbe_x2] / 2))
                [Graphics].DrawLine([Pen], CInt([Rectangle].X + [Radius_willbe_x2] / 2), CInt([Rectangle].Y + [Rectangle].Height), CInt([Rectangle].X + [Rectangle].Width - [Radius_willbe_x2] / 2), CInt([Rectangle].Y + [Rectangle].Height))
                [Graphics].DrawArc([Pen], [Rectangle].X, [Rectangle].Y + [Rectangle].Height - [Radius_willbe_x2], [Radius_willbe_x2], [Radius_willbe_x2], 90, 90)
                [Graphics].DrawArc([Pen], [Rectangle].X + [Rectangle].Width - [Radius_willbe_x2], [Rectangle].Y + [Rectangle].Height - [Radius_willbe_x2], [Radius_willbe_x2], [Radius_willbe_x2], 0, 90)
            Else
                [Graphics].DrawRectangle([Pen], [Rectangle])
            End If
        Catch
        End Try
    End Sub

    Public Function RoundedSemiRectangle(ByVal r As Rectangle, ByVal radius As Integer) As GraphicsPath
        Try
            Dim path As New GraphicsPath()
            Dim d As Integer = radius * 2

            path.AddLine(r.Left + d, r.Top, r.Right - d, r.Top)
            path.AddArc(Rectangle.FromLTRB(r.Right - d, r.Top, r.Right, r.Top + d), -90, 90)

            path.AddLine(r.Right, r.Top, r.Right, r.Bottom)

            path.AddLine(r.Right, r.Bottom, r.Left, r.Bottom)

            path.AddLine(r.Left, r.Bottom - d, r.Left, r.Top + d)
            path.AddArc(Rectangle.FromLTRB(r.Left, r.Top, r.Left + d, r.Top + d), 180, 90)

            path.CloseFigure()
            Return path
        Catch
            Return Nothing
        End Try
    End Function
    Public Sub FillSemiImg(ByVal [Graphics] As Graphics, ByVal [Image] As Image, ByVal [Rectangle] As Rectangle, Optional ByVal [Radius] As Integer = -1, Optional ByVal ForcedRoundCorner As Boolean = False)
        Try
            If [Radius] = -1 Then [Radius] = 6

            If Graphics Is Nothing Then Throw New ArgumentNullException("graphics")

            If (GetRoundedCorners() Or ForcedRoundCorner) And [Radius] > 0 Then
                Using path As GraphicsPath = RoundedSemiRectangle(Rectangle, Radius)
                    Dim reg As New Region(path)
                    [Graphics].Clip = reg
                    [Graphics].DrawImage([Image], [Rectangle])
                    [Graphics].ResetClip()
                End Using
            Else
                Graphics.DrawImage([Image], [Rectangle])
            End If
        Catch
        End Try
    End Sub

    Dim WithEvents Tm As New Timer With {.Enabled = False, .Interval = 500}

    Sub New()
        Text = ""
        DoubleBuffered = True
    End Sub

    Protected Overrides Sub OnPaint(e As System.Windows.Forms.PaintEventArgs)
        Dim G As Graphics = e.Graphics
        G.SmoothingMode = SmoothingMode.AntiAlias
        G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit

        DoubleBuffered = True

        If PreviewVersion Then
            If Not Light Then
                If Color_Titlebar = Color.FromArgb(0, 0, 0, 0) Then Color_Titlebar = Color.FromArgb(46, 46, 46)
                If Color_TabFocused = Color.FromArgb(0, 0, 0, 0) Then Color_TabFocused = Color_Background

                If Color_TabUnFocused = Color.FromArgb(0, 0, 0, 0) Then
                    If Color_TabFocused = Color_Background Then
                        Color_TabUnFocused = Color_Titlebar
                    Else
                        Color_TabUnFocused = Color_TabFocused.Dark
                    End If
                End If

                If Color_Titlebar_Unfocused = Color.FromArgb(0, 0, 0, 0) Then Color_Titlebar_Unfocused = Color.FromArgb(46, 46, 46)
            Else
                If Color_Titlebar = Color.FromArgb(0, 0, 0, 0) Then Color_Titlebar = Color.FromArgb(232, 232, 232)
                If Color_TabFocused = Color.FromArgb(0, 0, 0, 0) Then Color_TabFocused = Color_Background

                If Color_TabUnFocused = Color.FromArgb(0, 0, 0, 0) Then
                    If Color_TabFocused = Color_Background Then
                        Color_TabUnFocused = Color_Titlebar
                    Else
                        Color_TabUnFocused = Color_TabFocused.Light
                    End If
                End If

                If Color_Titlebar_Unfocused = Color.FromArgb(0, 0, 0, 0) Then Color_Titlebar_Unfocused = Color.FromArgb(255, 255, 255)
            End If
        Else
            If Not Light Then
                Color_Titlebar = Color.FromArgb(10, 10, 10)
                Color_Titlebar_Unfocused = Color.FromArgb(10, 10, 10)
                Color_TabFocused = Color.FromArgb(40, 40, 40)
                Color_TabUnFocused = Color_Titlebar
            Else
                Color_Titlebar = Color.FromArgb(218, 218, 218)
                Color_Titlebar_Unfocused = Color.FromArgb(218, 218, 218)
                Color_TabFocused = Color.FromArgb(249, 249, 249)
                Color_TabUnFocused = Color_Titlebar
            End If
        End If


        Dim Rect As New Rectangle(0, 0, Width - 1, Height - 1)
        Dim Rect_Titlebar As New Rectangle(0, 0, Width - 1, 32)
        Dim Rect_Console As New Rectangle(1, Rect_Titlebar.Bottom - 1, Width - 3, Height - Rect_Titlebar.Height)

        Dim s1 As String = "Console Sample"
        Dim s2 As String = "This is a selection"
        Dim s3 As String = Environment.GetFolderPath(Environment.SpecialFolder.Windows).Replace("WINDOWS", "Windows") & "\System32" & ">"

        Dim s1X As SizeF = s1.Measure(Font) + New SizeF(5, 0)
        Dim s2X As SizeF = s2.Measure(Font) + New SizeF(2, 0)
        Dim s3X As SizeF = s3.Measure(Font) + New SizeF(2, 0)
        Dim Rect_ConsoleText0 As New Rectangle(8, Rect_Titlebar.Bottom + 8, s1X.Width, s1X.Height)
        Dim Rect_ConsoleText1 As New Rectangle(8, Rect_ConsoleText0.Bottom + 3, s2X.Width, s2X.Height)
        Dim Rect_ConsoleText2 As New Rectangle(8, Rect_ConsoleText1.Bottom + Rect_ConsoleText1.Height + 3, s3X.Width, s3X.Height)

        Dim Rect_ConsoleCursor As New Rectangle(Rect_ConsoleText2.Right, Rect_ConsoleText2.Y, 50, Rect_ConsoleText2.Height - 1)

        If UseAcrylic Then
            G.FillRoundedImg(adaptedBackBlurred, Rect)
            G.FillRoundedRect(Noise, Rect)
            G.FillRoundedRect(New SolidBrush(Color.FromArgb((_Opacity / 100) * 255, Color_Background)), Rect)
            If BackImage IsNot Nothing Then G.FillRoundedImg(img, Rect)
        Else
            G.FillRoundedImg(adaptedBack, Rect)
            G.FillRoundedRect(New SolidBrush(Color.FromArgb((_Opacity / 100) * 255, Color_Background)), Rect)
            If BackImage IsNot Nothing Then G.FillRoundedImg(img, Rect)
        End If

        If UseAcrylicOnTitlebar And Not DesignMode Then
            If GetRoundedCorners() Then
                FillSemiImg(G, adaptedBackBlurred.Clone(Rect_Titlebar, PixelFormat.Format32bppArgb), Rect_Titlebar)
                FillSemiRect(G, Noise, Rect_Titlebar)
            Else
                G.DrawImage(adaptedBackBlurred.Clone(Rect_Titlebar, PixelFormat.Format32bppArgb), Rect_Titlebar)
                G.FillRectangle(Noise, Rect_Titlebar)
            End If

            If Not Light Then
                If GetRoundedCorners() Then
                    FillSemiRect(G, New SolidBrush(Color.FromArgb(If(IsFocused, 100, 255), 35, 35, 35)), Rect_Titlebar)
                Else
                    G.FillRectangle(New SolidBrush(Color.FromArgb(If(IsFocused, 100, 255), 35, 35, 35)), Rect_Titlebar)
                End If
            Else
                If GetRoundedCorners() Then
                    FillSemiRect(G, New SolidBrush(Color.FromArgb(If(IsFocused, 180, 255), 232, 232, 232)), Rect_Titlebar)
                Else
                    G.FillRectangle(New SolidBrush(Color.FromArgb(If(IsFocused, 180, 255), 232, 232, 232)), Rect_Titlebar)
                End If
            End If

        End If

        If Not UseAcrylicOnTitlebar Then
            If GetRoundedCorners() Then
                FillSemiRect(G, New SolidBrush(If(IsFocused, Color_Titlebar, Color_Titlebar_Unfocused)), Rect_Titlebar)
            Else
                G.FillRectangle(New SolidBrush(If(IsFocused, Color_Titlebar, Color_Titlebar_Unfocused)), Rect_Titlebar)
            End If
        End If

        Dim TabFocusedFinalColor As Color

        If TabColor <> Color.FromArgb(0, 0, 0, 0) Then
            TabFocusedFinalColor = TabColor
        Else
            TabFocusedFinalColor = Color_TabFocused
        End If

        Dim Radius As Integer = 5
        Dim TabHeight As Integer = 22
        Dim Rect_Tab0 As New Rectangle(10, Rect_Titlebar.Bottom - TabHeight, 150, TabHeight)
        Dim Rect_Tab1 As Rectangle = Rect_Tab0
        Rect_Tab1.X = Rect_Tab0.X + Rect_Tab0.Width - Radius

        Dim IconRect0 As New Rectangle(Rect_Tab0.X + 10, Rect_Tab0.Y + 3, 16, 16)
        Dim FC0 As Color = If(TabFocusedFinalColor.IsDark, Color.White, Color.Black)
        Dim RectText_Tab0 As New Rectangle(IconRect0.Right + 1, IconRect0.Y + 1, Rect_Tab0.Width - 35 - IconRect0.Width, IconRect0.Height)
        Dim RectClose_Tab0 As New Rectangle(RectText_Tab0.Right + 2, RectText_Tab0.Y - 1, 15, RectText_Tab0.Height)

        Dim IconRect1 As New Rectangle(Rect_Tab1.X + 10, Rect_Tab1.Y + 3, 16, 16)
        Dim FC1 As Color = If(Color_TabUnFocused.IsDark, Color.White, Color.Black)
        Dim RectText_Tab1 As New Rectangle(IconRect1.Right + 1, IconRect1.Y + 1, Rect_Tab1.Width - 35 - IconRect1.Width, IconRect1.Height)
        Dim RectClose_Tab1 As New Rectangle(RectText_Tab1.Right + 2, RectText_Tab1.Y - 1, 15, RectText_Tab1.Height)

        If IsFocused Then
            G.SmoothingMode = SmoothingMode.Default
            G.FillPath(New SolidBrush(TabFocusedFinalColor), RR(Rect_Tab0, Radius))
            G.SmoothingMode = SmoothingMode.AntiAlias
            G.DrawPath(New Pen(TabFocusedFinalColor), RRNoLine(Rect_Tab0, Radius))
            G.SmoothingMode = SmoothingMode.Default

            If Not UseAcrylicOnTitlebar Then
                G.FillPath(New SolidBrush(Color_TabUnFocused), RR(Rect_Tab1, Radius))
            Else
                If Color_TabUnFocused <> Color_Titlebar Then
                    G.FillPath(New SolidBrush(Color_TabUnFocused), RR(Rect_Tab1, Radius))
                End If
            End If
        End If

        Dim fx As Font

        If My.W11 Then
            fx = New Font("Segoe Fluent Icons", 12)
        Else
            fx = New Font("Segoe MDL2 Assets", 12)
        End If

        If TabIcon IsNot Nothing Then
            G.DrawImage(TabIcon, IconRect0)
        Else
            G.DrawString(TabIconButItIsString, fx, New SolidBrush(FC0), IconRect0, StringAligner(ContentAlignment.TopCenter))
        End If

        G.DrawString(TabIconButItIsString, fx, New SolidBrush(FC1), IconRect1, StringAligner(ContentAlignment.TopCenter))

        TextRenderer.DrawText(G, TabTitle, New Font("Segoe UI", 8, FontStyle.Bold), RectText_Tab0, FC0, Color.Transparent, TextFormatFlags.WordEllipsis)
        TextRenderer.DrawText(G, "Other Terminal", New Font("Segoe UI", 8, FontStyle.Regular), RectText_Tab1, FC1, Color.Transparent, TextFormatFlags.WordEllipsis)


        G.DrawString("", New Font("Segoe MDL2 Assets", 6, FontStyle.Regular), New SolidBrush(FC0), RectClose_Tab0, StringAligner(ContentAlignment.MiddleCenter))
        G.DrawString("", New Font("Segoe MDL2 Assets", 6, FontStyle.Regular), New SolidBrush(FC1), RectClose_Tab1, StringAligner(ContentAlignment.MiddleCenter))


        G.DrawString(s1, Font, New SolidBrush(Color_Foreground), Rect_ConsoleText0, StringAligner(ContentAlignment.TopLeft))

        G.FillRectangle(New SolidBrush(Color.FromArgb(125, Color_Selection)), Rect_ConsoleText1)

        G.DrawString(s2, Font, New SolidBrush(Color.FromArgb(255 - 125, Color_Foreground)), Rect_ConsoleText1, StringAligner(ContentAlignment.TopLeft))

        G.DrawString(s3, Font, New SolidBrush(Color_Foreground), Rect_ConsoleText2, StringAligner(ContentAlignment.TopLeft))

        If tk And IsFocused Then
            G.SmoothingMode = SmoothingMode.HighSpeed

            Select Case CursorType
                Case CursorShape_Enum.bar
                    G.FillRectangle(New SolidBrush(Color_Cursor), New Rectangle(Rect_ConsoleCursor.X, Rect_ConsoleCursor.Y, 1, Rect_ConsoleCursor.Height))

                Case CursorShape_Enum.doubleUnderscore
                    G.FillRectangle(New SolidBrush(Color_Cursor), New Rectangle(Rect_ConsoleCursor.X, Rect_ConsoleCursor.Bottom, Rect_ConsoleCursor.Height * 0.5, 1))
                    G.FillRectangle(New SolidBrush(Color_Cursor), New Rectangle(Rect_ConsoleCursor.X, Rect_ConsoleCursor.Bottom - 3, Rect_ConsoleCursor.Height * 0.5, 1))

                Case CursorShape_Enum.emptyBox
                    G.DrawRectangle(New Pen(Color_Cursor), New Rectangle(Rect_ConsoleCursor.X, Rect_ConsoleCursor.Y, Rect_ConsoleCursor.Height * 0.5, Rect_ConsoleCursor.Height))

                Case CursorShape_Enum.filledBox
                    G.FillRectangle(New SolidBrush(Color_Cursor), New Rectangle(Rect_ConsoleCursor.X, Rect_ConsoleCursor.Y, Rect_ConsoleCursor.Height * 0.5, Rect_ConsoleCursor.Height))

                Case CursorShape_Enum.underscore
                    G.FillRectangle(New SolidBrush(Color_Cursor), New Rectangle(Rect_ConsoleCursor.X, Rect_ConsoleCursor.Bottom - 1, Rect_ConsoleCursor.Height * 0.5, 1))

                Case CursorShape_Enum.vintage
                    G.FillRectangle(New SolidBrush(Color_Cursor), New Rectangle(Rect_ConsoleCursor.X, Rect_ConsoleCursor.Bottom - (CursorHeight / 100) * (Rect_ConsoleCursor.Height), Rect_ConsoleCursor.Height * 0.5, (CursorHeight / 100) * (Rect_ConsoleCursor.Height)))


            End Select

            G.SmoothingMode = SmoothingMode.AntiAlias
        End If

        G.DrawRoundedRect(New Pen(Color.FromArgb(45, 45, 45)), Rect)
    End Sub

    Dim tk As Boolean = False

    Private Sub Tm_Tick(sender As Object, e As EventArgs) Handles tm.Tick
        If IsFocused Then
            If tk Then
                tk = False
            Else
                tk = True
            End If

            Refresh()
        End If
    End Sub

    Private Sub XenonTaskbar_HandleCreated(sender As Object, e As EventArgs) Handles Me.HandleCreated
        If Not DesignMode Then
            tm.Enabled = True
            tm.Start()

            Try : AddHandler SizeChanged, AddressOf ProcessBack : Catch : End Try
            Try : AddHandler OpacityBackImageChanged, AddressOf UpdateOpacityBackImageChanged : Catch : End Try

            ProcessBack()
            UpdateOpacityBackImageChanged()
        Else
            tm.Enabled = False
            tm.Stop()
        End If
    End Sub

    Dim img As Image

    Sub UpdateOpacityBackImageChanged()
        If BackImage IsNot Nothing Then
            img = BackImage.Fade(OpacityBackImage / 100)
            Refresh()
        End If
    End Sub

    Sub ProcessBack()
        GetBack()
        NoiseBack()
    End Sub

    Sub GetBack()
        adaptedBack = My.Wallpaper '.Clone(RectangleToScreen(Bounds), My.Wallpaper.PixelFormat)
        adaptedBackBlurred = New Bitmap(adaptedBack).Blur(13)
    End Sub

    Sub NoiseBack()
        Noise = New TextureBrush(My.Resources.GaussianBlur.Fade(0.5))
    End Sub

End Class
#End Region