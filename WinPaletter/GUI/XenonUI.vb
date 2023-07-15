Imports System.ComponentModel
Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.Drawing.Text
Imports System.Runtime.CompilerServices
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
    Public Sub DrawGlowString(G As Graphics, GlowSize As Integer, Text As String, Font As Font, [ForeColor] As Color, GlowColor As Color, ClientRect As Rectangle, Rect As Rectangle, FormatX As StringFormat)
        Dim w As Integer = Math.Max(8, ClientRect.Width / 5)
        Dim h As Integer = Math.Max(8, ClientRect.Height / 5)
        Dim emSize As Single = G.DpiY * Font.SizeInPoints / 72

        Using b As New Bitmap(w, h)
            Using gp As New GraphicsPath()
                gp.AddString(Text, Font.FontFamily, Font.Style, emSize, Rect, FormatX)

                Using gx As Graphics = Graphics.FromImage(b)
                    Using m = New Matrix(1.0F / 5, 0, 0, 1.0F / 5, -(1.0F / 5), -(1.0F / 5))
                        gx.SmoothingMode = SmoothingMode.AntiAlias
                        gx.Transform = m
                        Using pn = New Pen(GlowColor, GlowSize)
                            gx.DrawPath(pn, gp)
                            gx.FillPath(pn.Brush, gp)
                        End Using
                    End Using
                End Using

                G.InterpolationMode = InterpolationMode.HighQualityBicubic
                G.DrawImage(b, ClientRect, 0, 0, b.Width, b.Height, GraphicsUnit.Pixel)

                G.SmoothingMode = SmoothingMode.AntiAlias
                Using br As New SolidBrush(ForeColor)
                    G.DrawString(Text, Font, br, Rect, FormatX)
                End Using

            End Using
        End Using
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

            Using bm As New Bitmap(CInt(Rect.Width / GlowFade), CInt(Rect.Height / GlowFade))
                Using G2 As Graphics = Graphics.FromImage(bm)
                    Dim Rect2 As New Rectangle(1, 1, bm.Width, bm.Height)

                    Using br As New SolidBrush(GlowColor)
                        G2.FillRectangle(br, Rect2)
                    End Using

                    G.DrawImage(bm, Rect)
                End Using
            End Using
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
            If My.Settings.Appearance.ManagedByTheme AndAlso My.Settings.Appearance.CustomColors Then
                Return My.Settings.Appearance.RoundedCorners
            Else
                If System.ComponentModel.LicenseManager.UsageMode = System.ComponentModel.LicenseUsageMode.Designtime Then
                    Return False
                Else
                    If My.W11 Then
                        Return True
                    ElseIf My.W10 Or My.W8 Then
                        Return False
                    ElseIf My.W7 OrElse My.WXP OrElse My.WVista Then
                        Return Not My.StartedWithClassicTheme
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
            If BackgroundBlurred IsNot Nothing Then G.DrawRoundImage(BackgroundBlurred, Rect, Radius, True)

            Using br As New SolidBrush(Color.FromArgb(alpha * 255, Color.Black)) : G.FillRoundedRect(br, Rect, Radius, True) : End Using
            Using br As New SolidBrush(Color.FromArgb(alpha * (ColorBalance * 255), Color1)) : G.FillRoundedRect(br, Rect, Radius, True) : End Using

            Dim C1 As Color = Color.FromArgb(ColorBalance * 255, Color1)
            Dim C2 As Color = Color.FromArgb(GlowBalance * 255, Color2)

            Using br As New SolidBrush(Color.FromArgb(alpha * (GlowBalance * 100), Color2)) : G.FillRoundedRect(br, Rect, Radius, True) : End Using
            Using br As New SolidBrush(Color.FromArgb(alpha * (GlowBalance * 150), C1.Blend(C2, 100))) : G.FillRoundedRect(br, Rect, Radius, True) : End Using
        Else
            If BackgroundBlurred IsNot Nothing Then G.DrawImage(BackgroundBlurred, Rect)

            Using br As New SolidBrush(Color.FromArgb(alpha * 255, Color.Black)) : G.FillRectangle(br, Rect) : End Using
            Using br As New SolidBrush(Color.FromArgb(alpha * (ColorBalance * 255), Color1)) : G.FillRectangle(br, Rect) : End Using

            Dim C1 As Color = Color.FromArgb(ColorBalance * 255, Color1)
            Dim C2 As Color = Color.FromArgb(GlowBalance * 255, Color2)

            Using br As New SolidBrush(Color.FromArgb(alpha * (GlowBalance * 100), Color2)) : G.FillRectangle(br, Rect) : End Using
            Using br As New SolidBrush(Color.FromArgb(alpha * (GlowBalance * 150), C1.Blend(C2, 100))) : G.FillRectangle(br, Rect) : End Using
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
    Public Sub DrawRoundImage(ByVal [Graphics] As Graphics, ByVal [Image] As Image, ByVal [Rectangle] As Rectangle, Optional ByVal [Radius] As Integer = -1, Optional ByVal ForcedRoundCorner As Boolean = False)
        Try
            If [Radius] = -1 Then [Radius] = 5

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

            Using [Pen] As New Pen([PenX].Color)
                Using [Pen2] As New Pen([PenX].Color)
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

                    Using [PenG] As New Pen(G)

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

                            If Dark Then
                                [Pen].Color = [PenX].Color.CB(0.05)
                            Else
                                [Pen].Color = [PenX].Color.CB(-0.02)
                            End If

                            [Graphics].DrawRectangle([Pen], [Rectangle])

                        End If

                        SidePen.Dispose()
                    End Using
                End Using
            End Using
        Catch
        End Try

    End Sub
#End Region
End Module

#Region "Xenon UI"
Public Class XenonTreeView
    Inherits TreeView

    Protected Overrides ReadOnly Property CreateParams As CreateParams
        Get
            Dim parms As CreateParams = MyBase.CreateParams
            parms.Style = parms.Style Or &H80
            Return parms
        End Get
    End Property
End Class
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
Public Class TablessControl : Inherits TabControl
    Public Sub New()
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
        SetStyle(ControlStyles.UserPaint Or ControlStyles.ResizeRedraw Or ControlStyles.AllPaintingInWmPaint Or ControlStyles.Opaque, True)
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

        G.TextRenderingHint = If(DesignMode, TextRenderingHint.ClearTypeGridFit, TextRenderingHint.SystemDefault)

        DoubleBuffered = True

        Dim SelectColor As Color
        Dim TextColor As Color
        Dim ParentColor As Color = GetParentColor
        Dim RTL As Boolean = (RightToLeft = 1)
        Dim img As Image = Nothing

        G.Clear(ParentColor)
        Dim Dark As Boolean = GetDarkMode()

        For i = 0 To TabCount - 1
            Dim TabRect As Rectangle = GetTabRect(i)
            Dim SideTapeH As Integer = TabRect.Height * 0.5
            Dim SideTapeW As Integer = 3
            Dim SideTape As Rectangle

            If Alignment = TabAlignment.Right Or Alignment = TabAlignment.Left Then
                SideTape = New Rectangle(TabRect.X + 1, TabRect.Y + (TabRect.Height - SideTapeH) / 2, SideTapeW, SideTapeH)
            ElseIf Alignment = TabAlignment.Top Then
                SideTape = New Rectangle(TabRect.X + TabRect.Width * 0.125, TabRect.Y + TabRect.Height - SideTapeW - 1, TabRect.Width * 0.75, SideTapeW)
            Else
                SideTape = New Rectangle(TabRect.X, TabRect.Y, TabRect.Width, SideTapeW)
            End If

            Try
                If Me.ImageList IsNot Nothing Then
                    Dim ls As ImageList = ImageList
                    img = ls.Images.Item(i)
                    SelectColor = img.AverageColor
                    SelectColor = SelectColor.Light(0.5)
                Else
                    SelectColor = If(Dark, LineColor, LineColor.LightLight)
                End If
            Catch
                SelectColor = If(Dark, LineColor, LineColor.LightLight)
            End Try

            If i = SelectedIndex Then
                Using br As New SolidBrush(ParentColor.CB(If(Dark, 0.08, -0.08))) : G.FillRoundedRect(br, TabRect) : End Using
                G.FillRoundedRect(Noise, TabRect)
                Using br As New SolidBrush(SelectColor) : G.FillRoundedRect(br, SideTape, 2) : End Using
            End If

            TextColor = If(Dark, Color.White, Color.Black)

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
                    Using br As New SolidBrush(TextColor) : G.DrawString(TabPages(i).Text, Font, br, tr, StringAligner(ContentAlignment.MiddleLeft)) : End Using
                Else
                    Dim b As New Bitmap(TabRect.Width, TabRect.Height)
                    Dim gx As Graphics = Graphics.FromImage(b)
                    gx.SmoothingMode = G.SmoothingMode
                    gx.TextRenderingHint = G.TextRenderingHint
                    Using br As New SolidBrush(TextColor) : gx.DrawString(TabPages(i).Text, Font, br, New Rectangle(0, 0, b.Width - imgRect.Right - 10, b.Height - 1), StringAligner(ContentAlignment.MiddleLeft, RTL)) : End Using
                    gx.Flush()
                    b.RotateFlip(RotateFlipType.Rotate180FlipY)
                    G.DrawImage(b, TabRect)
                    gx.Dispose()
                    b.Dispose()
                End If
            Else

                If Not RTL Then
                    If (Alignment = TabAlignment.Right Or Alignment = TabAlignment.Left) Then
                        Using br As New SolidBrush(TextColor) : G.DrawString(TabPages(i).Text, Font, br, New Rectangle(TabRect.X + SideTape.Right + 2, TabRect.Y + 1, TabRect.Width - SideTape.Right - 2, TabRect.Height), StringAligner(ContentAlignment.MiddleLeft)) : End Using
                    Else
                        Using br As New SolidBrush(TextColor) : G.DrawString(TabPages(i).Text, Font, br, TabRect, StringAligner(ContentAlignment.MiddleCenter)) : End Using
                    End If
                Else
                    Dim b As New Bitmap(TabRect.Width, TabRect.Height)
                    Dim gx As Graphics = Graphics.FromImage(b)
                    gx.SmoothingMode = G.SmoothingMode
                    gx.TextRenderingHint = G.TextRenderingHint
                    Using br As New SolidBrush(TextColor) : gx.DrawString(TabPages(i).Text, Font, br, New Rectangle(0, 0, b.Width - 1, b.Height - 1), StringAligner(ContentAlignment.MiddleCenter, RTL)) : End Using
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
        DoubleBuffered = True
        Size = New Size(40, 20)
        Text = ""
    End Sub

    Public Property DarkLight_Toggler As Boolean = False

    ReadOnly DarkLight_TogglerSize As Integer = 13

    Private _checked As Boolean

    Private _Shown As Boolean = False

    <Browsable(True)>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    <EditorBrowsable(EditorBrowsableState.Always)>
    <Editor(GetType(System.ComponentModel.Design.MultilineStringEditor), GetType(System.Drawing.Design.UITypeEditor))>
    <Bindable(True)>
    Public Overrides Property Text As String = ""

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

        Invalidate()
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

            Using br As New SolidBrush(Color.FromArgb(255 * val, Style.Colors.Border_Checked_Hover)) : e.Graphics.FillRoundedRect(br, MainRect, 9, True) : End Using

            Using P As New Pen(Color.FromArgb(255 * val, Style.Colors.Border_Checked)) : e.Graphics.DrawRoundedRect(P, MainRect, 9, True) : End Using

            Using P As New Pen(Color.FromArgb(255 * (1 - val), BorderColor)) : e.Graphics.DrawRoundedRect(P, MainRect, 9, True) : End Using

            If Checked Then
                Using br As New SolidBrush(If(GetDarkMode(), Color.Black, Color.White)) : G.FillEllipse(br, CheckC) : End Using
            Else
                Using br As New SolidBrush(BorderColor) : G.FillEllipse(br, CheckC) : End Using
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

            Using P As New Pen(lgborderChecked) : e.Graphics.DrawRoundedRect(P, MainRect, 9, True) : End Using
            Using P As New Pen(lgborderNonChecked) : e.Graphics.DrawRoundedRect(P, MainRect, 9, True) : End Using
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

    <Browsable(True)>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    <EditorBrowsable(EditorBrowsableState.Always)>
    <Editor(GetType(System.ComponentModel.Design.MultilineStringEditor), GetType(System.Drawing.Design.UITypeEditor))>
    <Bindable(True)>
    Public Overrides Property Text As String
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
            G.TextRenderingHint = If(DesignMode, TextRenderingHint.ClearTypeGridFit, TextRenderingHint.SystemDefault)
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
            Dim NonHoverCircle_Color As Color = Style.Colors.Back_Hover
            Dim BackCircle_Color As Color = Style.Colors.Back
            Dim ParentColor As Color = GetParentColor
#End Region
            '#################################################################################

            G.Clear(ParentColor)
            Using br As New SolidBrush(BackCircle_Color) : G.FillEllipse(br, OuterCircle) : End Using

            If Checked Then
                Using br As New SolidBrush(HoverCircle_Color) : G.FillEllipse(br, OuterCircle) : End Using
                Using br As New SolidBrush(CheckCircle_Color) : G.FillEllipse(br, CheckCircle) : End Using
                Using P As New Pen(HoverCheckedCircle_Color) : G.DrawEllipse(P, OuterCircle) : End Using
            Else
                Using br As New SolidBrush(HoverCircle_Color) : G.FillEllipse(br, OuterCircle) : End Using
                Using br As New SolidBrush(CheckCircle_Color) : G.FillEllipse(br, CheckCircle) : End Using
                Using P As New Pen(Color.FromArgb(255 - alpha, NonHoverCircle_Color)) : G.DrawEllipse(P, InnerCircle) : End Using
                Using P As New Pen(Color.FromArgb(alpha, clr)) : G.DrawEllipse(P, OuterCircle) : End Using
            End If

#Region "Strings"
            If Checked Then
                Using br As New SolidBrush(Color.FromArgb(255 - alpha, ForeColor)) : G.DrawString(Text, Font, br, TextRect, format) : End Using
                Using br As New SolidBrush(Color.FromArgb(alpha, CheckCircle_Color)) : G.DrawString(Text, Font, br, TextRect, format) : End Using
            Else
                Using br As New SolidBrush(ForeColor) : G.DrawString(Text, Font, br, TextRect, format) : End Using
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

    <Browsable(True)>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    <EditorBrowsable(EditorBrowsableState.Always)>
    <Editor(GetType(System.ComponentModel.Design.MultilineStringEditor), GetType(System.Drawing.Design.UITypeEditor))>
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
            G.TextRenderingHint = If(DesignMode, TextRenderingHint.ClearTypeGridFit, TextRenderingHint.SystemDefault)
            DoubleBuffered = True

            Dim MainRect As New Rectangle(0, 0, Width - 1, Height - 1)
            Dim MainRectInner As New Rectangle(1, 1, Width - 3, Height - 3)
            Dim CenterRect As New Rectangle

            If Image IsNot Nothing Then CenterRect = New Rectangle(MainRect.X + (MainRect.Width - Image.Width) / 2,
                                        MainRect.Y + (MainRect.Height - Image.Height) / 2,
                                        Image.Width, Image.Height)

            Dim bkC As Color = If(_Checked, Style.Colors.Back_Checked, Style.Colors.Back)
            Dim bkCC As Color = Color.FromArgb(alpha, Style.Colors.Back_Checked)

            Using br As New SolidBrush(bkC) : G.FillRoundedRect(br, MainRectInner) : End Using
            Using br As New SolidBrush(bkCC) : G.FillRoundedRect(br, MainRect) : End Using

            Dim lC As Color = Color.FromArgb(255 - alpha, If(_Checked, Style.Colors.Border_Checked, Style.Colors.Border))
            Dim lCC As Color = Color.FromArgb(alpha, Style.Colors.Border_Checked_Hover)

            Using P As New Pen(lC) : G.DrawRoundedRect_LikeW11(P, MainRectInner) : End Using
            Using P As New Pen(lCC) : G.DrawRoundedRect_LikeW11(P, MainRect) : End Using

            If Image IsNot Nothing Then G.DrawImage(Image, CenterRect)

            If ShowText Then
                Using br As New SolidBrush(ForeColor) : G.DrawString(Text, Font, br, MainRectInner, StringAligner(ContentAlignment.MiddleCenter)) : End Using
            End If
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

    <Browsable(True)>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    <EditorBrowsable(EditorBrowsableState.Always)>
    <Editor(GetType(System.ComponentModel.Design.MultilineStringEditor), GetType(System.Drawing.Design.UITypeEditor))>
    <Bindable(True)>
    Public Overrides Property Text As String
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
            G.TextRenderingHint = If(DesignMode, TextRenderingHint.ClearTypeGridFit, TextRenderingHint.SystemDefault)
            DoubleBuffered = True

            '################################################################################# Customizer
            Dim format As New StringFormat()

            Dim SZ1 As SizeF = G.MeasureString(Text, Font)
            Dim PT1 As New PointF(Height - 1, (CLng((Height - SZ1.Height)) \ 2) + 1)

            Dim OuterCheckRect As New Rectangle(3, 4, Height - 8, Height - 8)
            Dim InnerCheckRect As New Rectangle(4, 5, Height - 10, Height - 10)
            Dim TextRect As New Rectangle(Height - 1, (CLng((Height - SZ1.Height)) \ 2) + 1, Width - InnerCheckRect.Width, Height - 1)

#Region "Colors System"
            Dim HoverRect_Color As Color
            Dim HoverCheckedRect_Color As Color
            Dim CheckRect_Color As Color
            Dim NonHoverRect_Color As Color
            Dim BackRect_Color As Color
            Dim ParentColor As Color = GetParentColor

            If Enabled Then
                HoverRect_Color = Color.FromArgb(alpha2, Style.Colors.Back_Checked)
                HoverCheckedRect_Color = Color.FromArgb(alpha, Style.Colors.Border_Checked_Hover)
                CheckRect_Color = Color.FromArgb(alpha2, Style.Colors.Core)
                NonHoverRect_Color = Style.Colors.Border
                BackRect_Color = Style.Colors.Back
            Else
                HoverRect_Color = Color.FromArgb(alpha2, Style.Disabled_Colors.Back_Checked)
                HoverCheckedRect_Color = Color.FromArgb(alpha, Style.Disabled_Colors.Border_Checked_Hover)
                CheckRect_Color = Color.FromArgb(alpha2, Style.Disabled_Colors.Core)
                NonHoverRect_Color = Style.Disabled_Colors.Border
                BackRect_Color = Style.Disabled_Colors.Back
            End If

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

            Using CheckSignPen As New Pen(CheckRect_Color, 1.8F)
#End Region
                '#################################################################################

                G.Clear(ParentColor)

                Using br As New SolidBrush(Style.Colors.Back) : G.FillRoundedRect(br, InnerCheckRect, Radius) : End Using

                If _Checked Then
                    Using br As New SolidBrush(HoverRect_Color) : G.FillRoundedRect(br, InnerCheckRect, Radius) : End Using
                    Using br As New SolidBrush(Color.FromArgb(alpha, HoverRect_Color)) : G.FillRoundedRect(br, OuterCheckRect, Radius) : End Using

                    Using P As New Pen(Color.FromArgb(255 - alpha, HoverCheckedRect_Color)) : G.DrawRoundedRect(P, InnerCheckRect, Radius) : End Using
                    Using P As New Pen(Color.FromArgb(alpha, HoverCheckedRect_Color)) : G.DrawRoundedRect(P, OuterCheckRect, Radius) : End Using

                    G.DrawLine(CheckSignPen, x1_Left, y1_Left, x2_Left, y2_Left)
                    G.DrawLine(CheckSignPen, x1_Right, y1_Right, x2_Right, y2_Right)
                Else
                    Using br As New SolidBrush(HoverRect_Color) : G.FillRoundedRect(br, OuterCheckRect, Radius) : End Using
                    Using P As New Pen(HoverCheckedRect_Color) : G.DrawRoundedRect(P, OuterCheckRect, Radius) : End Using

                    G.DrawLine(CheckSignPen, x1_Left, y1_Left, x2_Left, y2_Left)
                    G.DrawLine(CheckSignPen, x1_Right, y1_Right, x2_Right, y2_Right)

                    Using P As New Pen(Color.FromArgb(255 - alpha, Style.Colors.Back_Hover)) : G.DrawRoundedRect(P, InnerCheckRect, Radius) : End Using
                End If

                If Checked Then
                    Using br As New SolidBrush(Color.FromArgb(255 - alpha, ForeColor)) : G.DrawString(Text, Font, br, TextRect, format) : End Using
                    Using br As New SolidBrush(Color.FromArgb(alpha, CheckRect_Color)) : G.DrawString(Text, Font, br, TextRect, format) : End Using
                Else
                    Using br As New SolidBrush(ForeColor) : G.DrawString(Text, Font, br, TextRect, format) : End Using
                End If
            End Using
        Catch
        End Try
    End Sub
End Class

<DefaultEvent("Click")>
Public Class XenonGroupBox : Inherits Panel

    Sub New()
        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint Or ControlStyles.ResizeRedraw, True)
        DoubleBuffered = True
        Text = ""
    End Sub

    Private LineColor As Color

    <Browsable(True)>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    <EditorBrowsable(EditorBrowsableState.Always)>
    <Editor(GetType(System.ComponentModel.Design.MultilineStringEditor), GetType(System.Drawing.Design.UITypeEditor))>
    <Bindable(True)>
    Public Overrides Property Text As String = ""

    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        MyBase.OnPaint(e)
        Dim G As Graphics = e.Graphics
        DoubleBuffered = True
        G.SmoothingMode = SmoothingMode.AntiAlias
        Dim Rect As New Rectangle(0, 0, Width - 1, Height - 1)
        Dim ParentColor As Color = GetParentColor

        G.Clear(ParentColor)
        BackColor = ParentColor.CB(If(ParentColor.IsDark, 0.04, -0.05))
        LineColor = ParentColor.CB(If(ParentColor.IsDark, 0.06, -0.07))
        Using br As New SolidBrush(BackColor) : G.FillRoundedRect(br, Rect) : End Using
        Using P As New Pen(LineColor) : G.DrawRoundedRect(P, Rect) : End Using
    End Sub



End Class

<DefaultEvent("Click")>
Public Class XenonAnimatedBox : Inherits Panel

    Sub New()
        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint Or ControlStyles.ResizeRedraw, True)
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
    <Editor(GetType(System.ComponentModel.Design.MultilineStringEditor), GetType(System.Drawing.Design.UITypeEditor))>
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

        If Not DesignMode AndAlso _Focused Then

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

            Using l As New LinearGradientBrush(Rect, C1, C2, _Angle, False)

                LineColor = Color.FromArgb(120, 150, 150, 150)

                If Dock = DockStyle.None Then
                    G.FillRoundedRect(l, Rect)
                    G.FillRoundedRect(Noise, Rect)
                    Using P As New Pen(LineColor) : G.DrawRoundedRect(P, Rect) : End Using
                Else
                    G.FillRectangle(l, Rect)
                    G.FillRectangle(Noise, Rect)
                End If
            End Using

        End If

    End Sub

    Private Sub XenonAnimatedBox_HandleCreated(sender As Object, e As EventArgs) Handles Me.HandleCreated
        If Not DesignMode Then
            Tmr.Enabled = True
            Tmr.Start()

            Try : AddHandler FindForm.Activated, AddressOf Form_GotFocus : Catch : End Try
            Try : AddHandler FindForm.Deactivate, AddressOf Form_LostFocus : Catch : End Try

        Else
            Tmr.Enabled = False
            Tmr.Stop()
        End If
    End Sub

    Private _Focused As Boolean = True

    Sub Form_GotFocus()
        _Focused = True
        Tmr.Enabled = True
        Tmr.Start()
        Invalidate()
    End Sub

    Sub Form_LostFocus()
        _Focused = False
        Tmr.Enabled = False
        Tmr.Stop()
        Invalidate()
    End Sub
End Class

<DefaultEvent("Click")>
Public Class XenonCP
    Inherits Panel

    Sub New()
        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint Or ControlStyles.ResizeRedraw, True)
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
    <Editor(GetType(System.ComponentModel.Design.MultilineStringEditor), GetType(System.Drawing.Design.UITypeEditor))>
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

        Using br As New SolidBrush(BackColor) : G.FillRoundedRect(br, RectInner, R) : End Using
        Using br As New SolidBrush(Color.FromArgb(alpha, BackColor)) : G.FillRoundedRect(br, Rect, R) : End Using

        Using P As New Pen(Color.FromArgb(alpha, LineColor)) : G.DrawRoundedRect_LikeW11(P, Rect, R) : End Using
        Using P As New Pen(Color.FromArgb(255 - alpha, LineColor)) : G.DrawRoundedRect_LikeW11(P, RectInner, R) : End Using


        If Not DesignMode Then
            If My.Settings.NerdStats.Enabled And Not ForceNoNerd Then
                G.TextRenderingHint = If(DesignMode, TextRenderingHint.ClearTypeGridFit, TextRenderingHint.SystemDefault)
                Dim IsDefault As Boolean = (BackColor = DefaultColor)
                Dim FC0 As Color = If(BackColor.IsDark, LineColor.LightLight, LineColor.Dark(0.9))
                Dim FC1 As Color = If(BackColor.IsDark, LineColor.LightLight, LineColor.Dark(0.9))

                FC0 = Color.FromArgb(100, FC0)
                FC1 = Color.FromArgb(alpha, FC1)

                Dim RectX As Rectangle = Rect
                RectX.Y += 1

                Dim CF As ColorFormat = ColorFormat.HEX
                If My.Settings.NerdStats.Type = XeSettings.Structures.NerdStats.Formats.HEX Then CF = ColorFormat.HEX
                If My.Settings.NerdStats.Type = XeSettings.Structures.NerdStats.Formats.RGB Then CF = ColorFormat.RGB
                If My.Settings.NerdStats.Type = XeSettings.Structures.NerdStats.Formats.HSL Then CF = ColorFormat.HSL
                If My.Settings.NerdStats.Type = XeSettings.Structures.NerdStats.Formats.Dec Then CF = ColorFormat.Dec


                Dim S As String = If(IsDefault, "D ", "") & BackColor.ReturnFormat(CF, My.Settings.NerdStats.ShowHexHash, Not (BackColor.A = 255))
                Dim F As Font

                If IsDefault Then
                    F = My.Application.ConsoleFontDef
                Else
                    F = My.Application.ConsoleFont
                End If

                Using br As New SolidBrush(FC0) : G.DrawString(S, F, br, RectX, StringAligner(ContentAlignment.MiddleCenter)) : End Using
                Using br As New SolidBrush(FC1) : G.DrawString(S, F, br, RectX, StringAligner(ContentAlignment.MiddleCenter)) : End Using

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

    Protected Overrides ReadOnly Property CreateParams() As CreateParams
        Get
            Dim cpar As CreateParams = MyBase.CreateParams
            If DrawOnGlass And Not DesignMode Then
                cpar.ExStyle = cpar.ExStyle Or &H20
                Return cpar
            Else
                Return cpar
            End If
        End Get
    End Property

    Public Property DrawOnGlass As Boolean = False

    Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
        Dim G As Graphics = e.Graphics
        G.SmoothingMode = SmoothingMode.AntiAlias
        G.TextRenderingHint = TextRenderingHint.SystemDefault
        DoubleBuffered = True

        '################################################################################# Customizer
        Dim Rect As New Rectangle(0, 0, Width - 1, Height - 1)
        Dim InnerRect As New Rectangle(1, 1, Width - 3, Height - 3)
        Dim ParentColor As Color = GetParentColor
        '#################################################################################

        If DrawOnGlass Then
            G.Clear(Color.Transparent)
            Using br As New SolidBrush(Color.FromArgb((255 - alpha) * 0.5, BackColor)) : G.FillRoundedRect(br, InnerRect) : End Using
            Using br As New SolidBrush(Color.FromArgb((alpha) * 0.5, BackColor)) : G.FillRoundedRect(br, Rect) : End Using
        Else
            G.Clear(ParentColor)
            Using br As New SolidBrush(Color.FromArgb(255 - alpha, BackColor)) : G.FillRoundedRect(br, InnerRect) : End Using
            Using br As New SolidBrush(Color.FromArgb(alpha, BackColor)) : G.FillRoundedRect(br, Rect) : End Using
        End If

        If Not State = MouseState.None Then G.FillRoundedRect(Noise, Rect)

        Dim c As Color
        Dim c1, c1x As Color

        Select Case State
            Case MouseState.None
                c = BackColor.CB(If(ParentColor.IsDark, 0.02, -0.02))

            Case MouseState.Over
                c = BackColor.CB(If(ParentColor.IsDark, 0.15, -0.05))

            Case MouseState.Down
                c = BackColor.CB(If(ParentColor.IsDark, 0.08, -0.03))

        End Select

        If DrawOnGlass Then
            c1 = Color.FromArgb((255 - alpha) * 0.5, c)
            c1x = Color.FromArgb((alpha) * 0.5, c)
        Else
            c1 = Color.FromArgb(255 - alpha, c)
            c1x = Color.FromArgb(alpha, c)
        End If

        Using P As New Pen(c1) : G.DrawRoundedRect_LikeW11(P, InnerRect) : End Using
        Using P As New Pen(c1x) : G.DrawRoundedRect_LikeW11(P, Rect) : End Using

#Region "Text and Image Render"
        Dim ButtonString As New StringFormat() With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center}
        Dim RTL As Boolean = (RightToLeft = 1)
        If RTL Then ButtonString.FormatFlags = StringFormatFlags.DirectionRightToLeft

        Dim img As Bitmap = Nothing
        If Image IsNot Nothing Then
            If Enabled Then
                img = CType(Image.Clone, Bitmap)
            Else
                img = CType(Image.Clone, Bitmap).Grayscale
            End If
        End If

        Dim imgX, imgY As Integer

        Try
            If img IsNot Nothing Then imgX = CInt((Width - img.Width) / 2)
        Catch : End Try

        Try : If img IsNot Nothing Then imgY = CInt((Height - img.Height) / 2)
        Catch : End Try

        If img Is Nothing Then
            Try
                Using br As New SolidBrush(ForeColor) : G.DrawString(Text, Font, br, New Rectangle(1, 0, Width, Height), StringAligner(TextAlign, RTL)) : End Using
            Catch
            End Try
        Else

            Select Case ImageAlign
                Case ContentAlignment.MiddleCenter
                    ButtonString.Alignment = StringAlignment.Center : ButtonString.LineAlignment = StringAlignment.Near
                    Dim alx As Integer = CInt((Height - (img.Height + 4 + Text.Measure(MyBase.Font).Height)) / 2)

                    Try : If img IsNot Nothing Then
                            If Text = Nothing Then
                                G.DrawImage(img.Clone, New Rectangle(imgX, imgY, img.Width, img.Height))
                            Else
                                G.DrawImage(img.Clone, New Rectangle(imgX, alx, img.Width, img.Height))
                            End If
                        End If
                        Using br As New SolidBrush(ForeColor) : G.DrawString(Text, Font, br, New Rectangle(0, alx + 9 + img.Height, Width, Height), ButtonString) : End Using
                    Catch : End Try

                Case ContentAlignment.MiddleLeft
                    Dim Rec As New Rectangle(imgY, imgY, img.Width, img.Height)
                    Dim Bo As Integer = imgY + img.Width + imgY - 5
                    Dim RecText As New Rectangle(Bo, imgY, Text.Measure(Font).Width + 15 - imgY, img.Height)
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


                    G.DrawImage(img.Clone, Rec)
                    Using br As New SolidBrush(ForeColor) : G.DrawString(Text, Font, br, RecText, ButtonString) : End Using

                Case ContentAlignment.MiddleRight
                    Dim Rec As New Rectangle(imgY, imgY, img.Width, img.Height)
                    Dim Bo As Integer = imgY + img.Width + imgY - 5
                    Dim RecText As New Rectangle(Bo, imgY, Width - Bo, img.Height)
                    Dim u As Rectangle = Rectangle.Union(Rec, RecText)
                    Dim innerSpace As Integer = RecText.Left - Rec.Right

                    If Not RTL Then
                        Rec.X = u.Left
                        RecText.X = u.Left + Rec.Width + innerSpace
                    Else
                        Rec.X = u.Right - Rec.Width - 2
                        RecText.X = u.Right - RecText.Width - Rec.Width - innerSpace
                    End If

                    G.DrawImage(img.Clone, Rec)
                    Using br As New SolidBrush(ForeColor) : G.DrawString(Text, Font, br, RecText, ButtonString) : End Using
            End Select
        End If

#End Region

    End Sub

    Private Sub XenonButton_HandleCreated(sender As Object, e As EventArgs) Handles Me.HandleCreated
        Try
            If Not DesignMode Then
                Try
                    AddHandler FindForm.Load, AddressOf Loaded
                    AddHandler FindForm.Shown, AddressOf Showed
                    AddHandler FindForm.FormClosed, AddressOf Closed
                    AddHandler Parent.Invalidated, AddressOf ForceRefresh
                    AddHandler Parent.BackColorChanged, AddressOf ForceRefresh
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
        Tmr.Enabled = False
        Tmr.Stop()
    End Sub

    Sub Showed()
        _Shown = True
    End Sub

    Sub Closed()
        _Shown = False
        Tmr.Enabled = False
        Tmr.Stop()
    End Sub

    Sub ForceRefresh()
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

    <Browsable(True)>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    <EditorBrowsable(EditorBrowsableState.Always)>
    <Editor(GetType(System.ComponentModel.Design.MultilineStringEditor), GetType(System.Drawing.Design.UITypeEditor))>
    <Bindable(True)>
    Public Overrides Property Text As String = ""

    Public Property AlternativeLook As Boolean = False

#Region "Events"
    Protected Overrides Sub OnResize(e As EventArgs)
        MyBase.OnResize(e)
        Size = New Size(Width, If(Not AlternativeLook, 1, 2))
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

            If GetDarkMode() Then
                If Not AlternativeLook Then
                    IdleLine = Parent.BackColor.CB(0.1)
                Else
                    IdleLine = Color.DarkRed
                End If
            Else
                If Not AlternativeLook Then
                    IdleLine = Parent.BackColor.CB(-0.1)
                Else
                    IdleLine = Color.DarkRed
                End If
            End If

        Else
            If GetDarkMode() Then IdleLine = Color.FromArgb(76, 76, 76) Else IdleLine = Color.FromArgb(210, 210, 210)
        End If

        '################################################################################# Customizer

        Using C As New Pen(IdleLine, If(Not AlternativeLook, 1, 2))
            G.DrawLine(C, New Point(0, 0), New Point(Width, 0))
            G.DrawLine(C, New Point(0, 1), New Point(Width, 1))
        End Using
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

    <Browsable(True)>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    <EditorBrowsable(EditorBrowsableState.Always)>
    <Editor(GetType(System.ComponentModel.Design.MultilineStringEditor), GetType(System.Drawing.Design.UITypeEditor))>
    <Bindable(True)>
    Public Overrides Property Text As String = ""
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
        G.TextRenderingHint = If(DesignMode, TextRenderingHint.ClearTypeGridFit, TextRenderingHint.SystemDefault)
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

        Using br As New SolidBrush(Color.FromArgb(255 - alpha, Style.Colors.Back)) : G.FillRoundedRect(br, OuterRect) : End Using
        Using br As New SolidBrush(Color.FromArgb(alpha, Style.Colors.Back_Checked)) : G.FillRoundedRect(br, OuterRect) : End Using
        Using br As New SolidBrush(Color.FromArgb(alpha, Style.Colors.Border_Checked_Hover)) : G.FillRoundedRect(br, SideRect) : End Using

        Using P As New Pen(Color.FromArgb(255 - alpha, Style.Colors.Border)) : G.DrawRoundedRect_LikeW11(P, InnerRect) : End Using
        Using P As New Pen(Color.FromArgb(alpha, Style.Colors.Border_Checked_Hover)) : G.DrawRoundedRect_LikeW11(P, OuterRect) : End Using

        If Focused And State = MouseState.None Then
            Using P As New Pen(Color.FromArgb(255, Style.Colors.Border_Checked_Hover)) : G.DrawRoundedRect(P, InnerRect) : End Using
        End If

        Using TextColor As New SolidBrush(If(GetDarkMode(), Color.White, Color.Black)), TextFont As New Font("Segoe UI", 9)
            G.DrawString(CStr(Value), TextFont, TextColor, New Rectangle(0, 0, Width - 15, Height), StringAligner(ContentAlignment.MiddleCenter))
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

    <Browsable(True)>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    <EditorBrowsable(EditorBrowsableState.Always)>
    <Editor(GetType(System.ComponentModel.Design.MultilineStringEditor), GetType(System.Drawing.Design.UITypeEditor))>
    <Bindable(True)>
    Public Overrides Property Text As String = ""
    Public Property AlternativeLook As Boolean = False

#Region "Events"

    Protected Overrides Sub OnResize(e As EventArgs)
        MyBase.OnResize(e)

        Size = New Size(If(Not AlternativeLook, 1, 2), Height)
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

            If GetDarkMode() Then
                If Not AlternativeLook Then
                    IdleLine = Parent.BackColor.CB(0.1)
                Else
                    IdleLine = Color.DarkRed
                End If
            Else
                If Not AlternativeLook Then
                    IdleLine = Parent.BackColor.CB(-0.1)
                Else
                    IdleLine = Color.DarkRed
                End If
            End If

        Else
            If GetDarkMode() Then IdleLine = Color.FromArgb(60, 60, 60) Else IdleLine = Color.FromArgb(210, 210, 210)
        End If
        '################################################################################# Customizer

        Using C As New Pen(IdleLine, If(Not AlternativeLook, 1, 2))
            G.DrawLine(C, New Point(0, 0), New Point(0, Height))
            G.DrawLine(C, New Point(1, 0), New Point(1, Height))
        End Using

    End Sub

End Class
<DefaultEvent("TextChanged")> Public Class XenonTextBox : Inherits Control
    Sub New()
        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint Or
                 ControlStyles.ResizeRedraw Or ControlStyles.SupportsTransparentBackColor, True)
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
        AddHandler TB.KeyPress, AddressOf OnKeyPress

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

    <Browsable(True)>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    <EditorBrowsable(EditorBrowsableState.Always)>
    <Editor(GetType(System.ComponentModel.Design.MultilineStringEditor), GetType(System.Drawing.Design.UITypeEditor))>
    <Bindable(True)>
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

    Public Event KeyboardPress(ByVal s As Object, ByVal e As KeyPressEventArgs)

    Public Overloads Sub OnKeyPress(ByVal s As Object, ByVal e As KeyPressEventArgs)
        RaiseEvent KeyboardPress(s, e)
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

    Protected Overrides Sub OnHandleCreated(e As EventArgs)
        Try
            If Not DesignMode Then
                MyBase.OnHandleCreated(e)
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

    Protected Overrides ReadOnly Property CreateParams() As CreateParams
        Get
            Dim cpar As CreateParams = MyBase.CreateParams
            If DrawOnGlass And Not DesignMode Then
                cpar.ExStyle = cpar.ExStyle Or &H20
                Return cpar
            Else
                Return cpar
            End If
        End Get
    End Property

    Public Property DrawOnGlass As Boolean = False

    Private ActiveTTLColor As Color

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        Dim G As Graphics = e.Graphics
        DoubleBuffered = True
        G.SmoothingMode = SmoothingMode.AntiAlias
        G.TextRenderingHint = TextRenderingHint.SystemDefault

        MyBase.OnPaint(e)

        Try
            ActiveTTLColor = New VisualStyles.VisualStyleRenderer(VisualStyles.VisualStyleElement.Window.Caption.Active).GetColor(VisualStyles.ColorProperty.TextColor).Invert
        Catch
            ActiveTTLColor = SystemColors.ActiveCaptionText
        End Try

        If Not DrawOnGlass Then
            If GetDarkMode() Then
                If ForeColor <> Color.White Then ForeColor = Color.White
            Else
                If ForeColor <> Color.Black Then ForeColor = Color.Black
            End If
        Else
            ForeColor = ActiveTTLColor
        End If

        TB.ForeColor = ForeColor

        Dim OuterRect As New Rectangle(0, 0, Width - 1, Height - 1)
        Dim InnerRect As New Rectangle(1, 1, Width - 3, Height - 3)

        Dim ParentColor As Color = GetParentColor
        Dim LineNone, LineHovered As Color
        Dim BackNone, BackHovered As Color

        If Not DrawOnGlass Then
            LineNone = If(GetDarkMode(), ParentColor.Light(0.3), ParentColor.Light(0.05))
            LineHovered = Style.Colors.Border_Checked_Hover

            BackNone = If(GetDarkMode(), ParentColor.Light(0.05), ParentColor.Light(0.3))
            BackHovered = Style.Colors.Back_Checked
        Else
            LineNone = If(Not ActiveTTLColor.IsDark, ParentColor.Light(0.3), ParentColor.Light(0.05))
            LineHovered = Style.Colors.Border_Checked_Hover

            BackNone = If(Not ActiveTTLColor.IsDark, ParentColor.Light(0.05), ParentColor.Light(0.3))
            BackHovered = Style.Colors.Back_Checked
        End If

        Dim FadeInColor As Color = Color.FromArgb(alpha, LineHovered)
        Dim FadeOutColor As Color = Color.FromArgb(255 - alpha, LineNone)

        If DrawOnGlass Then
            G.Clear(Color.Transparent)
        Else
            G.Clear(ParentColor)
        End If

        If TB.Focused Or Focused Then
            If Not DrawOnGlass Then
                Using br As New SolidBrush(BackHovered) : G.FillRoundedRect(br, OuterRect) : End Using
                Using P As New Pen(LineHovered) : G.DrawRoundedRect_LikeW11(P, OuterRect) : End Using
                TB.BackColor = BackHovered
            Else
                Using P As New Pen(LineHovered) : G.DrawRoundedRect_LikeW11(P, OuterRect) : End Using
                TB.BackColor = ParentColor
            End If

        Else
            If Not DrawOnGlass Then
                Using br As New SolidBrush(BackNone) : G.FillRoundedRect(br, InnerRect) : End Using
                Using br As New SolidBrush(Color.FromArgb(alpha, BackNone)) : G.FillRoundedRect(br, OuterRect) : End Using
                Using P As New Pen(FadeInColor) : G.DrawRoundedRect_LikeW11(P, OuterRect) : End Using
                Using P As New Pen(FadeOutColor) : G.DrawRoundedRect_LikeW11(P, InnerRect) : End Using
                TB.BackColor = BackNone
            Else
                Using P As New Pen(FadeInColor.CB(0.1)) : G.DrawRoundedRect_LikeW11(P, OuterRect) : End Using
                Using P As New Pen(FadeOutColor.CB(0.1)) : G.DrawRoundedRect_LikeW11(P, InnerRect) : End Using
                TB.BackColor = ParentColor
            End If
        End If


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
        e.Graphics.TextRenderingHint = If(DesignMode, TextRenderingHint.ClearTypeGridFit, TextRenderingHint.SystemDefault)

        If BackColor.IsDark Then
            If ForeColor <> Color.White Then ForeColor = Color.White
        Else
            If ForeColor <> Color.Black Then ForeColor = Color.Black
        End If

        Using br As New SolidBrush(BackColor) : e.Graphics.FillRectangle(br, New Rectangle(e.Bounds.X - 2, e.Bounds.Y - 2, e.Bounds.Width + 4, e.Bounds.Height + 4)) : End Using

        If (e.State And DrawItemState.Selected) = DrawItemState.Selected Then
            Using br As New SolidBrush(Style.Colors.Border_Checked_Hover) : e.Graphics.FillRectangle(br, e.Bounds) : End Using
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

        If e.Index >= 0 Then
            Using br As New SolidBrush(ForeColor) : e.Graphics.DrawString(MyBase.GetItemText(MyBase.Items(e.Index)), f, br, Rect, StringAligner(ContentAlignment.MiddleLeft)) : End Using
        End If
    End Sub

    Protected Sub DrawTriangle(ByVal Clr As Color, ByVal FirstPoint As Point, ByVal SecondPoint As Point, ByVal ThirdPoint As Point, ByVal G As Graphics)
        Dim points As New List(Of Point) From {FirstPoint, SecondPoint, ThirdPoint}
        Using br As New SolidBrush(Clr) : G.FillPolygon(br, points.ToArray()) : End Using
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
        G.TextRenderingHint = If(DesignMode, TextRenderingHint.ClearTypeGridFit, TextRenderingHint.SystemDefault)
        DoubleBuffered = True

        Dim OuterRect As New Rectangle(0, 0, Width - 1, Height - 1)
        Dim InnerRect As New Rectangle(1, 1, Width - 3, Height - 3)
        Dim TextRect As New Rectangle(5, 0, Width - 1, Height - 1)

        Dim FadeInColor As Color = Color.FromArgb(alpha, Style.Colors.Border_Checked_Hover)
        Dim FadeOutColor As Color = Color.FromArgb(255 - alpha, Style.Colors.Border)

        G.Clear(GetParentColor)

        Using br As New SolidBrush(Style.Colors.Back) : G.FillRoundedRect(br, InnerRect) : End Using
        Using br As New SolidBrush(Color.FromArgb(alpha, Style.Colors.Back_Checked)) : G.FillRoundedRect(br, OuterRect) : End Using
        G.FillRoundedRect(Noise, InnerRect)

        Using P As New Pen(FadeInColor) : G.DrawRoundedRect_LikeW11(P, OuterRect) : End Using
        Using P As New Pen(FadeOutColor) : G.DrawRoundedRect_LikeW11(P, InnerRect) : End Using

        Using br As New SolidBrush(Color.FromArgb(alpha2, Style.Colors.Back_Checked)) : G.FillRoundedRect(br, OuterRect) : End Using
        Using P As New Pen(Color.FromArgb(alpha2, Style.Colors.Border_Checked_Hover)) : G.DrawRoundedRect_LikeW11(P, OuterRect) : End Using

        Dim ArrowHeight As Integer = 4
        Dim Arrow_Y_1 As Integer = (Height - ArrowHeight) / 2 - 1
        Dim Arrow_Y_2 As Integer = Arrow_Y_1 + ArrowHeight

        If Focused And State = MouseState.None Then
            Using P As New Pen(Color.FromArgb(255, FadeInColor))
                G.DrawRoundedRect(P, InnerRect)
                G.DrawLine(P, New Point(Width - 18, Arrow_Y_1), New Point(Width - 14, Arrow_Y_2))
                G.DrawLine(P, New Point(Width - 14, Arrow_Y_2), New Point(Width - 10, Arrow_Y_1))
                G.DrawLine(P, New Point(Width - 14, Arrow_Y_2 + 1), New Point(Width - 14, Arrow_Y_2))
            End Using
        Else
            Using P As New Pen(Color.FromArgb(255 - alpha, ForeColor), 2)
                G.DrawLine(P, New Point(Width - 18, Arrow_Y_1), New Point(Width - 14, Arrow_Y_2))
                G.DrawLine(P, New Point(Width - 14, Arrow_Y_2), New Point(Width - 10, Arrow_Y_1))
            End Using

            Using P As New Pen(Color.FromArgb(255 - alpha, ForeColor)) : G.DrawLine(P, New Point(Width - 14, Arrow_Y_2 + 1), New Point(Width - 14, Arrow_Y_2)) : End Using

            If Not DroppedDown Then

                Using P As New Pen(FadeInColor, 2)
                    G.DrawLine(P, New Point(Width - 18, Arrow_Y_1), New Point(Width - 14, Arrow_Y_2))
                    G.DrawLine(P, New Point(Width - 14, Arrow_Y_2), New Point(Width - 10, Arrow_Y_1))
                End Using

                Using P As New Pen(FadeInColor) : G.DrawLine(New Pen(FadeInColor), New Point(Width - 14, Arrow_Y_2 + 1), New Point(Width - 14, Arrow_Y_2)) : End Using

            Else
                Using P As New Pen(FadeInColor) : G.DrawLine(P, New Point(Width - 14, Arrow_Y_1), New Point(Width - 14, Arrow_Y_1 + 1)) : End Using

                Using P As New Pen(FadeInColor, 2)
                    G.DrawLine(P, New Point(Width - 18, Arrow_Y_2), New Point(Width - 14, Arrow_Y_1))
                    G.DrawLine(P, New Point(Width - 14, Arrow_Y_1), New Point(Width - 10, Arrow_Y_2))
                End Using
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

        Using br As New SolidBrush(ForeColor) : G.DrawString(Text, f, br, TextRect, New StringFormat With {.LineAlignment = StringAlignment.Center, .Alignment = StringAlignment.Near}) : End Using
    End Sub
End Class
Public Class XenonAlertBox
    Inherits ContainerControl

    Private borderColor, innerColor, textColor As Color

    Sub New()
        TabStop = False
        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint Or ControlStyles.ResizeRedraw, True)
        DoubleBuffered = True
        Font = New Font("Segoe UI", 9)
        Size = New Size(200, 40)
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
        Information
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

    Private _alertStyle As Style

    Public Property Image As Image
    Public Property CustomColor As Color
    Public Property CenterText As Boolean = False

    <Browsable(True)>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    <EditorBrowsable(EditorBrowsableState.Always)>
    <Editor(GetType(System.ComponentModel.Design.MultilineStringEditor), GetType(System.Drawing.Design.UITypeEditor))>
    <Bindable(True)>
    Public Overrides Property Text As String
#End Region

    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        MyBase.OnPaint(e)
        Dim G As Graphics = e.Graphics
        G.SmoothingMode = SmoothingMode.AntiAlias
        G.TextRenderingHint = TextRenderingHint.SystemDefault
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

            Case Style.Information
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

        Using br As New SolidBrush(innerColor) : G.FillRoundedRect(br, New Rectangle(0, 0, Width - 1, Height - 1)) : End Using
        Using P As New Pen(borderColor) : G.DrawRoundedRect_LikeW11(P, New Rectangle(0, 0, Width - 1, Height - 1)) : End Using

        Dim textY As Integer = CInt((Height - Text.Measure(Font).Height) / 2)
        Dim TextX As Integer = 5

        If Image IsNot Nothing Then G.DrawImage(Image, New Rectangle(If(Not RTL, 5, Width - 5 - Image.Width), 5, Image.Width, Image.Height))

        If Not CenterText Then
            If Image Is Nothing Then
                Using br As New SolidBrush(textColor) : G.DrawString(Text, Font, br, New Rectangle(TextX, 0, Width, Height), StringAligner(ContentAlignment.MiddleLeft, RTL)) : End Using
            Else
                If Not RTL Then
                    Using br As New SolidBrush(textColor) : G.DrawString(Text, Font, br, New Rectangle(10 + Image.Width, 7, Width - (5 + Image.Width), Height - 10), StringAligner(ContentAlignment.TopLeft)) : End Using
                Else
                    Using br As New SolidBrush(textColor) : G.DrawString(Text, Font, br, New Rectangle(0, 7, Width - (10 + Image.Width), Height - 10), StringAligner(ContentAlignment.TopLeft, RTL)) : End Using
                End If
            End If
        Else
            Using br As New SolidBrush(textColor) : G.DrawString(Text, Font, br, New Rectangle(1, 0, Width, Height), StringAligner(ContentAlignment.MiddleCenter, RTL)) : End Using
        End If

    End Sub

End Class
Public Class XenonWinElement : Inherits ContainerControl
    Dim Noise As New TextureBrush(My.Resources.GaussianBlur.Fade(0.15))
    Dim Noise7 As Bitmap = My.Resources.AeroGlass
    Dim Noise7Start As Bitmap = My.Resources.Start7Glass
    Dim adaptedBackBlurred As Bitmap

#Region "Properties"
    Enum Styles
        Start11
        Taskbar11
        ActionCenter11
        AltTab11
        Start10
        Taskbar10
        ActionCenter10
        AltTab10
        Start8
        Taskbar8Aero
        Taskbar8Lite
        AltTab8Aero
        AltTab8AeroLite
        Start7Aero
        Taskbar7Aero
        Start7Opaque
        Taskbar7Opaque
        Start7Basic
        Taskbar7Basic
        AltTab7Aero
        AltTab7Opaque
        AltTab7Basic
        StartVistaAero
        TaskbarVistaAero
        StartVistaOpaque
        TaskbarVistaOpaque
        StartVistaBasic
        TaskbarVistaBasic
        StartXP
        TaskbarXP
    End Enum

    Private _Style As Styles = Styles.Start11
    Public Property Style As Styles
        Get
            Return _Style
        End Get
        Set(value As Styles)
            _Style = value
            'ProcessBack()
            If Not SuspendRefresh Then Refresh()
        End Set
    End Property

    Private _BackColorAlpha As Byte = 130
    Public Property BackColorAlpha() As Integer
        Get
            Return _BackColorAlpha
        End Get
        Set(ByVal value As Integer)
            _BackColorAlpha = value
            If Not SuspendRefresh Then Refresh()
        End Set
    End Property

    Private _NoisePower As Single = 0.15
    Public Property NoisePower() As Single
        Get
            Return _NoisePower
        End Get
        Set(ByVal value As Single)
            Me._NoisePower = value

            If Style = Styles.Taskbar7Aero Then
                Try : Noise7 = My.Resources.AeroGlass.Fade(NoisePower / 100) : Catch : End Try
            End If

            If Style = Styles.Start7Aero Then
                Try : Noise7Start = My.Resources.Start7Glass.Fade(NoisePower / 100) : Catch : End Try
            End If

            If Not SuspendRefresh Then NoiseBack()
            If Not SuspendRefresh Then Refresh()
        End Set
    End Property

    Private _BlurPower As Integer = 8
    Public Property BlurPower() As Integer
        Get
            Return _BlurPower
        End Get
        Set(ByVal value As Integer)
            _BlurPower = value
            GetBack()
            If Not SuspendRefresh Then Refresh()
        End Set
    End Property

    Private _Transparency As Boolean = True
    Public Property Transparency() As Boolean
        Get
            Return _Transparency
        End Get
        Set(ByVal value As Boolean)
            _Transparency = value
            'ProcessBack()
            If Not SuspendRefresh Then Refresh()
        End Set
    End Property

    Private _DarkMode As Boolean = True
    Public Property DarkMode() As Boolean
        Get
            Return _DarkMode
        End Get
        Set(ByVal value As Boolean)
            _DarkMode = value
            If Not SuspendRefresh Then Refresh()
        End Set
    End Property

    Private _AppUnderline As Color
    Public Property AppUnderline() As Color
        Get
            Return _AppUnderline
        End Get
        Set(ByVal value As Color)
            _AppUnderline = value
            Try : If Not SuspendRefresh Then Refresh()
            Catch : End Try
        End Set
    End Property

    Private _AppBackground As Color
    Public Property AppBackground() As Color
        Get
            Return _AppBackground
        End Get
        Set(ByVal value As Color)
            _AppBackground = value
            Try : If Not SuspendRefresh Then Refresh()
            Catch : End Try
        End Set
    End Property

    Private _ActionCenterButton_Normal As Color
    Public Property ActionCenterButton_Normal() As Color
        Get
            Return _ActionCenterButton_Normal
        End Get
        Set(ByVal value As Color)
            _ActionCenterButton_Normal = value
            Try : If Not SuspendRefresh Then Refresh()
            Catch : End Try
        End Set
    End Property

    Private _ActionCenterButton_Hover As Color
    Public Property ActionCenterButton_Hover() As Color
        Get
            Return _ActionCenterButton_Hover
        End Get
        Set(ByVal value As Color)
            _ActionCenterButton_Hover = value
            Try : If Not SuspendRefresh Then Refresh()
            Catch : End Try
        End Set
    End Property

    Private _ActionCenterButton_Pressed As Color
    Public Property ActionCenterButton_Pressed() As Color
        Get
            Return _ActionCenterButton_Pressed
        End Get
        Set(ByVal value As Color)
            _ActionCenterButton_Pressed = value
            Try : If Not SuspendRefresh Then Refresh()
            Catch : End Try
        End Set
    End Property

    Private _StartColor As Color
    Public Property StartColor() As Color
        Get
            Return _StartColor
        End Get
        Set(ByVal value As Color)
            _StartColor = value
            If Not SuspendRefresh Then Refresh()
        End Set
    End Property

    Private _LinkColor As Color
    Public Property LinkColor() As Color
        Get
            Return _LinkColor
        End Get
        Set(ByVal value As Color)
            _LinkColor = value
            If Not SuspendRefresh Then Refresh()
        End Set
    End Property

    Private _Background As Color
    Public Property Background() As Color
        Get
            Return _Background
        End Get
        Set(ByVal value As Color)
            _Background = value
            If Not SuspendRefresh Then Refresh()
        End Set
    End Property

    Private _Background2 As Color
    Public Property Background2() As Color
        Get
            Return _Background2
        End Get
        Set(ByVal value As Color)
            _Background2 = value
            If Not SuspendRefresh Then Refresh()
        End Set
    End Property

    Private _Win7ColorBal As Integer = 0.15
    Public Property Win7ColorBal() As Integer
        Get
            Return _Win7ColorBal
        End Get
        Set(ByVal value As Integer)
            _Win7ColorBal = value
            If Not SuspendRefresh Then Refresh()
        End Set
    End Property

    Private _Win7GlowBal As Integer = 0.15
    Public Property Win7GlowBal() As Integer
        Get
            Return _Win7GlowBal
        End Get
        Set(ByVal value As Integer)
            _Win7GlowBal = value
            If Not SuspendRefresh Then Refresh()
        End Set
    End Property

    Public Property UseWin11ORB_WithWin10 As Boolean = False
    Public Property UseWin11RoundedCorners_WithWin10_Level1 As Boolean = False
    Public Property UseWin11RoundedCorners_WithWin10_Level2 As Boolean = False
    Public Property Shadow As Boolean = True
    Public Property SuspendRefresh As Boolean = False

    Public Sub CopycatFrom(element As XenonWinElement)
        Style = element.Style
        _NoisePower = element.NoisePower
        _BlurPower = element.BlurPower
        _Transparency = element.Transparency
        _DarkMode = element.DarkMode
        _AppUnderline = element.AppUnderline
        _AppBackground = element.AppBackground
        _ActionCenterButton_Normal = element.ActionCenterButton_Normal
        _ActionCenterButton_Hover = element.ActionCenterButton_Hover
        _ActionCenterButton_Pressed = element.ActionCenterButton_Pressed
        _StartColor = element.StartColor
        _LinkColor = element.LinkColor
        _BackColorAlpha = element.BackColorAlpha
        BackColor = element.BackColor
        _Background2 = element.Background2
        _Win7ColorBal = element.Win7ColorBal
        _Win7GlowBal = element.Win7GlowBal
        UseWin11ORB_WithWin10 = element.UseWin11ORB_WithWin10
        UseWin11RoundedCorners_WithWin10_Level1 = element.UseWin11RoundedCorners_WithWin10_Level1
        UseWin11RoundedCorners_WithWin10_Level2 = element.UseWin11RoundedCorners_WithWin10_Level2
        Shadow = element.Shadow

        Dock = element.Dock
        Size = element.Size
        Location = element.Location
        Text = element.Text

        Try
            If Not SuspendRefresh Then
                'ProcessBack()
                Refresh()
            End If
        Catch : End Try
    End Sub

#End Region

    Sub New()
        SetStyle(ControlStyles.UserPaint, True)
        SetStyle(ControlStyles.AllPaintingInWmPaint, True)
        SetStyle(ControlStyles.ResizeRedraw, True)
        SetStyle(ControlStyles.SupportsTransparentBackColor, True)
        BackColor = Color.Transparent
    End Sub

    Protected Overrides ReadOnly Property CreateParams As CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ExStyle = cp.ExStyle Or &H20
            Return cp
        End Get
    End Property

    Enum MouseState
        Normal
        Hover
        Pressed
    End Enum

    Private _State_Btn1, _State_Btn2 As MouseState

    Dim Button1 As Rectangle
    Dim Button2 As Rectangle

    Private Sub XenonAcrylic_MouseMove(sender As Object, e As MouseEventArgs) Handles Me.MouseMove, Me.MouseDown, Me.MouseUp
        If Style = Styles.ActionCenter11 Then

            If Button1.Contains(PointToClient(MousePosition)) Then
                If e.Button = MouseButtons.None Then _State_Btn1 = MouseState.Hover Else _State_Btn1 = MouseState.Pressed
                If Not SuspendRefresh Then Refresh()
            Else
                If Not _State_Btn1 = MouseState.Normal Then
                    _State_Btn1 = MouseState.Normal
                    If Not SuspendRefresh Then Refresh()
                End If
            End If

            If Button2.Contains(PointToClient(MousePosition)) Then
                If e.Button = MouseButtons.None Then _State_Btn2 = MouseState.Hover Else _State_Btn2 = MouseState.Pressed
                If Not SuspendRefresh Then Refresh()
            Else
                If Not _State_Btn2 = MouseState.Normal Then
                    _State_Btn2 = MouseState.Normal
                    If Not SuspendRefresh Then Refresh()
                End If
            End If

        End If
    End Sub

    Private Sub XenonAcrylic_MouseLeave(sender As Object, e As EventArgs) Handles Me.MouseLeave
        If Style = Styles.ActionCenter11 Then
            _State_Btn1 = MouseState.Normal
            _State_Btn2 = MouseState.Normal
            If Not SuspendRefresh Then Refresh()
        End If
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        Dim G As Graphics = e.Graphics
        G.SmoothingMode = SmoothingMode.AntiAlias
        G.TextRenderingHint = My.RenderingHint
        DoubleBuffered = True
        Dim Rect As New Rectangle(-1, -1, Width + 2, Height + 2)
        Dim RRect As New Rectangle(0, 0, Width - 1, Height - 1)
        Dim Radius As Integer = 5

        Select Case Style
            Case Styles.Start11
#Region "Start 11"
                If Not DesignMode AndAlso Transparency AndAlso adaptedBackBlurred IsNot Nothing Then G.DrawRoundImage(adaptedBackBlurred, RRect, Radius, True)

                If DarkMode Then
                    Using br As New SolidBrush(Color.FromArgb(85, 28, 28, 28)) : G.FillRoundedRect(br, RRect, Radius, True) : End Using
                Else
                    Using br As New SolidBrush(Color.FromArgb(75, 255, 255, 255)) : G.FillRoundedRect(br, RRect, Radius, True) : End Using
                End If

                Using br As New SolidBrush(Color.FromArgb(If(Transparency, BackColorAlpha, 255), Background)) : G.FillRoundedRect(br, RRect, Radius, True) : End Using
                If Transparency Then G.FillRoundedRect(Noise, RRect, Radius, True)
                Dim SearchRect As New Rectangle(8, 10, Width - (8) * 2, 15)

                G.DrawRoundImage(If(DarkMode, My.Resources.Start11_Dark, My.Resources.Start11_Light), RRect, Radius, True)

                Dim SearchColor, SearchBorderColor As Color
                If DarkMode Then
                    SearchColor = Color.FromArgb(150, 28, 28, 28)
                    SearchBorderColor = Color.FromArgb(150, 65, 65, 65)
                Else
                    SearchColor = Color.FromArgb(175, 255, 255, 255)
                    SearchBorderColor = Color.FromArgb(175, 200, 200, 200)
                End If

                Using br As New SolidBrush(SearchColor) : G.FillRoundedRect(br, SearchRect, 8, True) : End Using
                Using P As New Pen(SearchBorderColor) : G.DrawRoundedRect(P, SearchRect, 8, True) : End Using

                Using P As New Pen(Color.FromArgb(150, 90, 90, 90)) : G.DrawRoundedRect(P, RRect, Radius, True) : End Using
#End Region

            Case Styles.ActionCenter11
#Region "Action Center 11"
                If Not DesignMode AndAlso Transparency AndAlso adaptedBackBlurred IsNot Nothing Then G.DrawRoundImage(adaptedBackBlurred, RRect, Radius, True)

                If DarkMode Then
                    Using br As New SolidBrush(Color.FromArgb(85, 28, 28, 28)) : G.FillRoundedRect(br, RRect, Radius, True) : End Using
                Else
                    Using br As New SolidBrush(Color.FromArgb(75, 255, 255, 255)) : G.FillRoundedRect(br, RRect, Radius, True) : End Using
                End If

                Using br As New SolidBrush(Color.FromArgb(If(Transparency, BackColorAlpha, 255), Background)) : G.FillRoundedRect(br, RRect, Radius, True) : End Using
                If Transparency Then G.FillRoundedRect(Noise, RRect, Radius, True)
                Button1 = New Rectangle(8, 8, 49, 20)
                Button2 = New Rectangle(62, 8, 49, 20)

                G.DrawRoundImage(If(DarkMode, My.Resources.AC_11_Dark, My.Resources.AC_11_Light), RRect, Radius, True)

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

                Using br As New SolidBrush(Cx1) : G.FillRoundedRect(br, Button1, Radius, True) : End Using
                Using P As New Pen(Cx1.Light(0.15)) : G.DrawRoundedRect_LikeW11(P, Button1, Radius, True) : End Using
                Using br As New SolidBrush(Cx2) : G.FillRoundedRect(br, Button2, Radius, True) : End Using
                Using P As New Pen(Cx2.CB(If(DarkMode, 0.05, -0.05))) : G.DrawRoundedRect(P, Button2, Radius, True) : End Using
                Using P As New Pen(Color.FromArgb(150, 90, 90, 90)) : G.DrawRoundedRect(P, RRect, Radius, True) : End Using
#End Region

            Case Styles.Taskbar11
#Region "Taskbar 11"
                If Not DesignMode AndAlso Transparency AndAlso adaptedBackBlurred IsNot Nothing Then G.DrawImage(adaptedBackBlurred, Rect)

                If DarkMode Then
                    Using br As New SolidBrush(Color.FromArgb(110, 28, 28, 28)) : G.FillRectangle(br, Rect) : End Using
                Else
                    Using br As New SolidBrush(Color.FromArgb(90, 255, 255, 255)) : G.FillRectangle(br, Rect) : End Using
                End If

                Using br As New SolidBrush(Color.FromArgb(If(Transparency, BackColorAlpha, 255), Background)) : G.FillRectangle(br, Rect) : End Using
                If Transparency Then G.FillRoundedRect(Noise, RRect, Radius, True)

                Dim StartBtnRect As New Rectangle(8, 3, 36, 36)
                Dim StartImgRect As New Rectangle(8, 3, 37, 37)

                Dim App2BtnRect As New Rectangle(StartBtnRect.Right + 5, 3, 36, 36)
                Dim App2ImgRect As New Rectangle(StartBtnRect.Right + 5, 3, 37, 37)
                Dim App2BtnRectUnderline As New Rectangle(App2BtnRect.X + (App2BtnRect.Width - 8) / 2, App2BtnRect.Y + App2BtnRect.Height - 3, 8, 3)

                Dim AppBtnRect As New Rectangle(App2BtnRect.Right + 5, 3, 36, 36)
                Dim AppImgRect As New Rectangle(App2BtnRect.Right + 5, 3, 37, 37)
                Dim AppBtnRectUnderline As New Rectangle(AppBtnRect.X + (AppBtnRect.Width - 18) / 2, AppBtnRect.Y + AppBtnRect.Height - 3, 18, 3)

                Dim BackC As Color
                Dim BorderC As Color

                If DarkMode Then
                    BackC = Color.FromArgb(45, 130, 130, 130)
                    BorderC = Color.FromArgb(45, 130, 130, 130)
                Else
                    BackC = Color.FromArgb(35, 255, 255, 255)
                    BorderC = Color.FromArgb(35, 255, 255, 255)
                End If

                Using br As New SolidBrush(BackC) : G.FillRoundedRect(br, StartBtnRect, 3, True) : End Using
                Using P As New Pen(BorderC) : G.DrawRoundedRect_LikeW11(P, StartBtnRect, 3) : End Using
                G.DrawImage(If(DarkMode, My.Resources.StartBtn_11Dark, My.Resources.StartBtn_11Light), StartImgRect)

                Using br As New SolidBrush(BackC) : G.FillRoundedRect(br, AppBtnRect, 3, True) : End Using
                Using P As New Pen(BorderC) : G.DrawRoundedRect_LikeW11(P, AppBtnRect, 3) : End Using
                G.DrawImage(My.Resources.SampleApp_Active, AppImgRect)
                Using br As New SolidBrush(_AppUnderline) : G.FillRoundedRect(br, AppBtnRectUnderline, 2, True) : End Using

                G.DrawImage(My.Resources.SampleApp_Inactive, App2ImgRect)
                Using br As New SolidBrush(Color.FromArgb(255, BackC)) : G.FillRoundedRect(br, App2BtnRectUnderline, 2, True) : End Using

                Using P As New Pen(Color.FromArgb(100, 100, 100, 100)) : G.DrawLine(P, New Point(0, 0), New Point(Width - 1, 0)) : End Using
#End Region

            Case Styles.AltTab11
#Region "Alt+Tab 11"
                If Not DesignMode AndAlso Transparency AndAlso adaptedBackBlurred IsNot Nothing Then G.DrawRoundImage(adaptedBackBlurred, RRect, Radius, True)

                If Transparency Then
                    If DarkMode Then
                        Using br As New SolidBrush(Color.FromArgb(100, 175, 175, 175)) : G.FillRoundedRect(br, RRect, Radius, True) : End Using
                    Else
                        Using br As New SolidBrush(Color.FromArgb(120, 185, 185, 185)) : G.FillRoundedRect(br, RRect, Radius, True) : End Using
                    End If

                    G.FillRoundedRect(Noise, RRect, Radius, True)
                Else
                    If DarkMode Then
                        Using br As New SolidBrush(Color.FromArgb(32, 32, 32)) : G.FillRoundedRect(br, RRect, Radius, True) : End Using
                        Using P As New Pen(Color.FromArgb(65, 65, 65)) : G.DrawRoundedRect(P, RRect, Radius, True) : End Using
                    Else
                        Using br As New SolidBrush(Color.FromArgb(243, 243, 243)) : G.FillRoundedRect(br, RRect, Radius, True) : End Using
                        Using P As New Pen(Color.FromArgb(171, 171, 171)) : G.DrawRoundedRect(P, RRect, Radius, True) : End Using
                    End If
                End If

                Dim AppHeight As Single = 0.75 * RRect.Height
                Dim _padding As Integer = (RRect.Height - AppHeight) / 2

                Dim appsNumber As Integer = 3
                Dim AllAppsWidthWithPadding As Single = RRect.Width - 2 * _padding
                Dim AppWidth As Single = (AllAppsWidthWithPadding - (appsNumber - 1) * _padding) / appsNumber

                Dim Rects As New List(Of Rectangle)
                Rects.Clear()

                For x = 0 To appsNumber - 1
                    If x = 0 Then
                        Rects.Add(New Rectangle(RRect.X + _padding, RRect.Y + _padding, AppWidth, AppHeight))
                    Else
                        Rects.Add(New Rectangle(Rects(x - 1).Right + _padding, RRect.Y + _padding, AppWidth, AppHeight))
                    End If
                Next

                For x = 0 To Rects.Count - 1
                    Dim r As Rectangle = Rects(x)

                    Dim back As Color = If(DarkMode, Color.FromArgb(23, 23, 23), Color.FromArgb(233, 234, 234))
                    Dim back2 As Color = If(DarkMode, Color.FromArgb(39, 39, 39), Color.FromArgb(255, 255, 255))

                    If x = 0 Then
                        Dim surround As New Rectangle(r.X - 5, r.Y - 5, r.Width + 10, r.Height + 10)
                        Using P As New Pen(Color.FromArgb(75, 182, 237), 3) : G.DrawRoundedRect(P, surround, Radius * 2 + 5 / 2, True) : End Using
                    End If

                    Using br As New SolidBrush(back) : G.FillRoundedRect(br, r, Radius * 2, True) : End Using
                    G.DrawImage(My.Resources.SampleApp_Active, New Rectangle(r.X + 5, r.Y + 5, 20, 20))

                    Using br As New SolidBrush(Color.FromArgb(150, back2)) : G.FillRectangle(br, New Rectangle(r.X + 5 + 20 + 5, r.Y + 5 + (20 - 4) / 2, 20, 4)) : End Using

                    Using br As New SolidBrush(back2) : G.FillRoundedRect(br, New Rectangle(r.X + 1, r.Y + 5 + 20 + 5, r.Width - 2, r.Height - 5 - 20 - 5), Radius * 2, True) : End Using
                Next
#End Region

            Case Styles.Start10
#Region "Start 10"
                If Not UseWin11RoundedCorners_WithWin10_Level1 And Not UseWin11RoundedCorners_WithWin10_Level2 Then
                    If Not DesignMode AndAlso Transparency AndAlso adaptedBackBlurred IsNot Nothing Then G.DrawImage(adaptedBackBlurred, Rect)
                    If Transparency Then G.FillRectangle(Noise, Rect)
                    Using br As New SolidBrush(Color.FromArgb(If(Transparency, BackColorAlpha, 255), Background)) : G.FillRectangle(br, Rect) : End Using
                    G.DrawImage(If(DarkMode, My.Resources.Start10_Dark, My.Resources.Start10_Light), New Rectangle(0, 0, Width - 1, Height - 1))

                ElseIf UseWin11RoundedCorners_WithWin10_Level1 Then
                    If Not DesignMode AndAlso Transparency AndAlso adaptedBackBlurred IsNot Nothing Then G.DrawImage(adaptedBackBlurred, Rect)
                    If Transparency Then G.FillRectangle(Noise, Rect)
                    Using br As New SolidBrush(Color.FromArgb(If(Transparency, BackColorAlpha, 255), Background)) : G.FillRectangle(br, Rect) : End Using
                    G.DrawImage(If(DarkMode, My.Resources.Start11_EP_Rounded_Dark, My.Resources.Start11_EP_Rounded_Light), New Rectangle(0, 0, Width - 1, Height - 1))

                ElseIf UseWin11RoundedCorners_WithWin10_Level2 Then
                    If Not DesignMode AndAlso Transparency AndAlso adaptedBackBlurred IsNot Nothing Then G.DrawImage(adaptedBackBlurred, Rect)
                    If Transparency Then G.FillRoundedRect(Noise, Rect, Radius, True)
                    Using br As New SolidBrush(Color.FromArgb(If(Transparency, BackColorAlpha, 255), Background)) : G.FillRoundedRect(br, Rect, Radius, True) : End Using
                    G.DrawRoundImage(If(DarkMode, My.Resources.Start11_EP_Rounded_Dark, My.Resources.Start11_EP_Rounded_Light), New Rectangle(0, 0, Width - 1, Height - 1), Radius, True)

                End If

#End Region

            Case Styles.ActionCenter10
#Region "Action Center 10"
                If Not DesignMode AndAlso Transparency AndAlso adaptedBackBlurred IsNot Nothing Then G.DrawImage(adaptedBackBlurred, Rect)

                If Transparency Then G.FillRectangle(Noise, Rect)
                Using br As New SolidBrush(Color.FromArgb(If(Transparency, BackColorAlpha, 255), Background)) : G.FillRectangle(br, Rect) : End Using

                Dim rect1 As New Rectangle(85, 6, 30, 3)
                Dim rect2 As New Rectangle(5, 190, 30, 3)
                Dim rect3 As New Rectangle(42, 201, 34, 24)

                Using br As New SolidBrush(ActionCenterButton_Normal) : G.FillRectangle(br, rect3) : End Using
                G.DrawImage(If(DarkMode, My.Resources.AC_10_Dark, My.Resources.AC_10_Light), New Rectangle(0, 0, Width - 1, Height - 1))
                Using br As New SolidBrush(LinkColor) : G.FillRectangle(br, rect1) : End Using
                Using br As New SolidBrush(LinkColor) : G.FillRectangle(br, rect2) : End Using
                Using P As New Pen(Color.FromArgb(150, 100, 100, 100)) : G.DrawLine(P, New Point(0, 0), New Point(0, Height - 1)) : End Using

                Using P As New Pen(Color.FromArgb(150, 76, 76, 76)) : G.DrawRectangle(P, Rect) : End Using
#End Region

            Case Styles.Taskbar10
#Region "Taskbar 10"
                G.SmoothingMode = SmoothingMode.HighSpeed
                If Not DesignMode AndAlso Transparency AndAlso adaptedBackBlurred IsNot Nothing Then G.DrawImage(adaptedBackBlurred, Rect)
                Using br As New SolidBrush(Color.FromArgb(If(Transparency, BackColorAlpha, 255), Background)) : G.FillRectangle(br, Rect) : End Using

                Dim StartBtnRect As New Rectangle(-1, -1, 42, Height + 2)
                Dim StartBtnImgRect As New Rectangle

                If Not UseWin11ORB_WithWin10 Then
                    StartBtnImgRect = New Rectangle(StartBtnRect.X + (StartBtnRect.Width - My.Resources.StartBtn_10Dark.Width) / 2, StartBtnRect.Y + (StartBtnRect.Height - My.Resources.StartBtn_10Dark.Height) / 2, My.Resources.StartBtn_10Dark.Width, My.Resources.StartBtn_10Dark.Height)
                Else
                    StartBtnImgRect = New Rectangle(StartBtnRect.X + (StartBtnRect.Width - My.Resources.StartBtn_11_EP.Width) / 2, StartBtnRect.Y + (StartBtnRect.Height - My.Resources.StartBtn_11_EP.Height) / 2, My.Resources.StartBtn_11_EP.Width, My.Resources.StartBtn_11_EP.Height)
                End If

                Dim AppBtnRect As New Rectangle(StartBtnRect.Right, -1, 40, Height + 2)
                Dim AppBtnImgRect As New Rectangle(AppBtnRect.X + (AppBtnRect.Width - My.Resources.SampleApp_Active.Width) / 2, AppBtnRect.Y + (AppBtnRect.Height - My.Resources.SampleApp_Active.Height) / 2 - 1, My.Resources.SampleApp_Active.Width, My.Resources.SampleApp_Active.Height)
                Dim AppBtnRectUnderline As New Rectangle(AppBtnRect.X, AppBtnRect.Y + AppBtnRect.Height - 3, AppBtnRect.Width, 2)
                Dim App2BtnRect As New Rectangle(AppBtnRect.Right, -1, 40, Height + 2)
                Dim App2BtnImgRect As New Rectangle(App2BtnRect.X + (App2BtnRect.Width - My.Resources.SampleApp_Inactive.Width) / 2, App2BtnRect.Y + (App2BtnRect.Height - My.Resources.SampleApp_Inactive.Height) / 2, My.Resources.SampleApp_Inactive.Width, My.Resources.SampleApp_Inactive.Height)
                Dim App2BtnRectUnderline As New Rectangle(App2BtnRect.X + 14 / 2, App2BtnRect.Y + App2BtnRect.Height - 3, App2BtnRect.Width - 14, 2)
                Dim StartColor As Color = _StartColor
                Using br As New SolidBrush(StartColor) : G.FillRectangle(br, StartBtnRect) : End Using

                If Not UseWin11ORB_WithWin10 Then
                    G.DrawImage(If(DarkMode, My.Resources.StartBtn_10Dark, My.Resources.StartBtn_10Light), StartBtnImgRect)
                Else
                    G.DrawImage(My.Resources.StartBtn_11_EP, StartBtnImgRect)
                End If

                Dim AppColor As Color = _AppBackground
                Using br As New SolidBrush(AppColor) : G.FillRectangle(br, AppBtnRect) : End Using
                Using br As New SolidBrush(_AppUnderline.Light) : G.FillRectangle(br, AppBtnRectUnderline) : End Using
                G.DrawImage(My.Resources.SampleApp_Active, AppBtnImgRect)

                Using br As New SolidBrush(_AppUnderline.Light) : G.FillRectangle(br, App2BtnRectUnderline) : End Using
                G.DrawImage(My.Resources.SampleApp_Inactive, App2BtnImgRect)
#End Region

            Case Styles.AltTab10
#Region "Alt+Tab 10"
                Dim a As Integer = Math.Max(Math.Min(255, (BackColorAlpha / 100) * 255), 0)

                Using br As New SolidBrush(Color.FromArgb(a, 23, 23, 23)) : G.FillRectangle(br, RRect) : End Using

                Dim AppHeight As Single = 0.75 * RRect.Height
                Dim _padding As Integer = (RRect.Height - AppHeight) / 2

                Dim appsNumber As Integer = 3
                Dim AllAppsWidthWithPadding As Single = RRect.Width - 2 * _padding
                Dim AppWidth As Single = (AllAppsWidthWithPadding - (appsNumber - 1) * _padding) / appsNumber

                Dim Rects As New List(Of Rectangle)
                Rects.Clear()

                For x = 0 To appsNumber - 1
                    If x = 0 Then
                        Rects.Add(New Rectangle(RRect.X + _padding, RRect.Y + _padding, AppWidth, AppHeight))
                    Else
                        Rects.Add(New Rectangle(Rects(x - 1).Right + _padding, RRect.Y + _padding, AppWidth, AppHeight))
                    End If
                Next

                For x = 0 To Rects.Count - 1
                    Dim r As Rectangle = Rects(x)

                    Dim back As Color = If(DarkMode, Color.FromArgb(60, 60, 60), Color.FromArgb(255, 255, 255))

                    If x = 0 Then
                        Dim surround As New Rectangle(r.X - 5, r.Y - 5, r.Width + 10, r.Height + 10)
                        Using P As New Pen(Color.White, 2) : G.DrawRectangle(P, surround) : End Using
                    End If

                    G.DrawImage(My.Resources.SampleApp_Active, New Rectangle(r.X + 5, r.Y + 5, 20, 20))

                    G.FillRectangle(Brushes.White, New Rectangle(r.X + 5 + 20 + 5, r.Y + 5 + (20 - 4) / 2, 20, 4))

                    Using br As New SolidBrush(back) : G.FillRectangle(br, New Rectangle(r.X + 1, r.Y + 5 + 20 + 5, r.Width - 2, r.Height - 5 - 20 - 5)) : End Using
                Next
#End Region

            Case Styles.Taskbar8Aero
#Region "Taskbar 8 Aero"
                Dim c As Color = Color.FromArgb((Win7ColorBal / 100) * 255, Background)
                Dim bc As Color = Color.FromArgb(217, 217, 217)

                Using P As New Pen(Color.FromArgb(80, 0, 0, 0)) : G.DrawLine(P, New Point(0, 0), New Point(Width - 1, 0)) : End Using

                Using br As New SolidBrush(Color.FromArgb(BackColorAlpha, bc)) : G.FillRectangle(br, Rect) : End Using
                Using br As New SolidBrush(Color.FromArgb(BackColorAlpha * (Win7ColorBal / 100), c)) : G.FillRectangle(br, Rect) : End Using

                Dim StartORB As New Bitmap(My.Resources.Win8ORB)
                Dim StartBtnRect As New Rectangle((35 - 27) / 2 + 2, (35 - 27) / 2 - 1, 27, 27)
                Dim AppBtnRect As New Rectangle(StartBtnRect.Right + 8, 0, 45, Height - 1)
                Dim AppBtnRectInner As New Rectangle(AppBtnRect.X + 1, AppBtnRect.Y + 1, AppBtnRect.Width - 2, AppBtnRect.Height - 2)

                Dim AppBtnImgRect As New Rectangle(AppBtnRect.X + (AppBtnRect.Width - My.Resources.SampleApp_Active.Width) / 2, AppBtnRect.Y + (AppBtnRect.Height - My.Resources.SampleApp_Active.Height) / 2, My.Resources.SampleApp_Active.Width, My.Resources.SampleApp_Active.Height)
                Dim App2BtnRect As New Rectangle(AppBtnRect.Right + 2, 0, 45, Height - 1)
                Dim App2BtnRectInner As New Rectangle(App2BtnRect.X + 1, App2BtnRect.Y + 1, App2BtnRect.Width - 2, App2BtnRect.Height - 2)
                Dim App2BtnImgRect As New Rectangle(App2BtnRect.X + (App2BtnRect.Width - My.Resources.SampleApp_Inactive.Width) / 2, App2BtnRect.Y + (App2BtnRect.Height - My.Resources.SampleApp_Inactive.Height) / 2, My.Resources.SampleApp_Inactive.Width, My.Resources.SampleApp_Inactive.Height)

                G.DrawImage(StartORB, StartBtnRect)

                Using br As New SolidBrush(Color.FromArgb(100, Color.White)) : G.FillRectangle(br, AppBtnRect) : End Using
                Using P As New Pen(Color.FromArgb(200, c.CB(-0.5))) : G.DrawRectangle(P, AppBtnRect) : End Using
                Using P As New Pen(Color.FromArgb(215, Color.White)) : G.DrawRectangle(P, AppBtnRectInner) : End Using

                G.DrawImage(My.Resources.SampleApp_Active, AppBtnImgRect)

                Using br As New SolidBrush(Color.FromArgb(50, Color.White)) : G.FillRectangle(br, App2BtnRect) : End Using
                Using P As New Pen(Color.FromArgb(100, c.CB(-0.5))) : G.DrawRectangle(P, App2BtnRect) : End Using
                Using P As New Pen(Color.FromArgb(100, Color.White)) : G.DrawRectangle(P, App2BtnRectInner) : End Using

                G.DrawImage(My.Resources.SampleApp_Inactive, App2BtnImgRect)
#End Region

            Case Styles.Taskbar8Lite
#Region "Taskbar 8 Lite"
                Dim c As Color = Color.FromArgb((Win7ColorBal / 100) * 255, Background)
                Dim bc As Color = Color.FromArgb(217, 217, 217)

                Using P As New Pen(Color.FromArgb(89, 89, 89)) : G.DrawRectangle(P, New Rectangle(0, 0, Width - 1, Height - 1)) : End Using

                Using br As New SolidBrush(Color.FromArgb(255, bc)) : G.FillRectangle(br, Rect) : End Using
                Using br As New SolidBrush(Color.FromArgb(255 * (Win7ColorBal / 100), c)) : G.FillRectangle(br, Rect) : End Using

                Dim StartORB As New Bitmap(My.Resources.Win8ORB)
                Dim StartBtnRect As New Rectangle((35 - 27) / 2 + 2, (35 - 27) / 2 - 1, 27, 27)
                Dim AppBtnRect As New Rectangle(StartBtnRect.Right + 8, 0, 45, Height - 1)
                Dim AppBtnRectInner As New Rectangle(AppBtnRect.X + 1, AppBtnRect.Y + 1, AppBtnRect.Width - 2, AppBtnRect.Height - 2)

                Dim AppBtnImgRect As New Rectangle(AppBtnRect.X + (AppBtnRect.Width - My.Resources.SampleApp_Active.Width) / 2, AppBtnRect.Y + (AppBtnRect.Height - My.Resources.SampleApp_Active.Height) / 2, My.Resources.SampleApp_Active.Width, My.Resources.SampleApp_Active.Height)
                Dim App2BtnRect As New Rectangle(AppBtnRect.Right + 2, 0, 45, Height - 1)
                Dim App2BtnRectInner As New Rectangle(App2BtnRect.X + 1, App2BtnRect.Y + 1, App2BtnRect.Width - 2, App2BtnRect.Height - 2)
                Dim App2BtnImgRect As New Rectangle(App2BtnRect.X + (App2BtnRect.Width - My.Resources.SampleApp_Inactive.Width) / 2, App2BtnRect.Y + (App2BtnRect.Height - My.Resources.SampleApp_Inactive.Height) / 2, My.Resources.SampleApp_Inactive.Width, My.Resources.SampleApp_Inactive.Height)

                G.DrawImage(StartORB, StartBtnRect)

                Using br As New SolidBrush(Color.FromArgb(255, bc.CB(0.5))) : G.FillRectangle(br, AppBtnRect) : End Using
                Using br As New SolidBrush(Color.FromArgb(255 * (Win7ColorBal / 100), c.CB(0.5))) : G.FillRectangle(br, AppBtnRect) : End Using
                Using P As New Pen(Color.FromArgb(100, bc.CB(-0.5))) : G.DrawRectangle(P, AppBtnRect) : End Using
                Using P As New Pen(Color.FromArgb(100 * (Win7ColorBal / 100), c.CB(-0.5))) : G.DrawRectangle(P, AppBtnRect) : End Using

                G.DrawImage(My.Resources.SampleApp_Active, AppBtnImgRect)

                Using br As New SolidBrush(Color.FromArgb(255, bc.Light(0.1))) : G.FillRectangle(br, App2BtnRect) : End Using
                Using br As New SolidBrush(Color.FromArgb(255 * (Win7ColorBal / 100), c.Light(0.1))) : G.FillRectangle(br, App2BtnRect) : End Using
                Using P As New Pen(Color.FromArgb(100, bc.Dark(0.1))) : G.DrawRectangle(P, App2BtnRect) : End Using
                Using P As New Pen(Color.FromArgb(100 * (Win7ColorBal / 100), c.Dark(0.1))) : G.DrawRectangle(P, App2BtnRect) : End Using
                G.DrawImage(My.Resources.SampleApp_Inactive, App2BtnImgRect)
#End Region

            Case Styles.AltTab8Aero
#Region "Alt+Tab 8 Aero"
                Using br As New SolidBrush(Background) : G.FillRectangle(br, RRect) : End Using

                Dim AppHeight As Single = 0.75 * RRect.Height
                Dim _padding As Integer = (RRect.Height - AppHeight) / 2

                Dim appsNumber As Integer = 3
                Dim AllAppsWidthWithPadding As Single = RRect.Width - 2 * _padding
                Dim AppWidth As Single = (AllAppsWidthWithPadding - (appsNumber - 1) * _padding) / appsNumber

                Dim Rects As New List(Of Rectangle)
                Rects.Clear()

                For x = 0 To appsNumber - 1
                    If x = 0 Then
                        Rects.Add(New Rectangle(RRect.X + _padding, RRect.Y + _padding + AppHeight * 2 / 5, AppWidth, AppHeight * 3 / 5))
                    Else
                        Rects.Add(New Rectangle(Rects(x - 1).Right + _padding, RRect.Y + _padding + AppHeight * 2 / 5, AppWidth, AppHeight * 3 / 5))
                    End If
                Next

                For x = 0 To Rects.Count - 1
                    Dim r As Rectangle = Rects(x)

                    If x = 0 Then
                        Dim surround As New Rectangle(r.X - 10, r.Y - 10, r.Width + 20, r.Height + 20)
                        Using P As New Pen(Color.White, 2) : G.DrawRectangle(P, surround) : End Using
                    End If

                    G.FillRectangle(Brushes.White, r)
                    Dim icon_w As Integer = My.Resources.SampleApp_Active.Width
                    Dim icon_rect As New Rectangle(r.X + r.Width - 0.7 * icon_w, r.Y + r.Height - 0.6 * icon_w, icon_w, icon_w)
                    G.DrawImage(My.Resources.SampleApp_Active, icon_rect)
                Next

                Dim TextRect As New Rectangle(RRect.X + _padding, RRect.Y, RRect.Width - 2 * _padding, AppHeight * 2 / 5)
                G.DrawString("______", Font, Brushes.White, TextRect, StringAligner(ContentAlignment.MiddleCenter))
#End Region

            Case Styles.AltTab8AeroLite
#Region "Alt+Tab 8 Opaque"
                Using br As New SolidBrush(Background) : G.FillRectangle(br, RRect) : End Using

                Using P As New Pen(LinkColor, 2) : G.DrawRectangle(P, RRect) : End Using

                Dim AppHeight As Single = 0.75 * RRect.Height
                Dim _padding As Integer = (RRect.Height - AppHeight) / 2

                Dim appsNumber As Integer = 3
                Dim AllAppsWidthWithPadding As Single = RRect.Width - 2 * _padding
                Dim AppWidth As Single = (AllAppsWidthWithPadding - (appsNumber - 1) * _padding) / appsNumber

                Dim Rects As New List(Of Rectangle)
                Rects.Clear()

                For x = 0 To appsNumber - 1
                    If x = 0 Then
                        Rects.Add(New Rectangle(RRect.X + _padding, RRect.Y + _padding + AppHeight * 2 / 5, AppWidth, AppHeight * 3 / 5))
                    Else
                        Rects.Add(New Rectangle(Rects(x - 1).Right + _padding, RRect.Y + _padding + AppHeight * 2 / 5, AppWidth, AppHeight * 3 / 5))
                    End If
                Next

                For x = 0 To Rects.Count - 1
                    Dim r As Rectangle = Rects(x)

                    If x = 0 Then
                        Dim surround As New Rectangle(r.X - 10, r.Y - 10, r.Width + 20, r.Height + 20)
                        Using P As New Pen(Background2, 2) : G.DrawRectangle(P, surround) : End Using
                    End If

                    G.FillRectangle(Brushes.White, r)
                    Dim icon_w As Integer = My.Resources.SampleApp_Active.Width
                    Dim icon_rect As New Rectangle(r.X + r.Width - 0.7 * icon_w, r.Y + r.Height - 0.6 * icon_w, icon_w, icon_w)
                    G.DrawImage(My.Resources.SampleApp_Active, icon_rect)
                Next

                Dim TextRect As New Rectangle(RRect.X + _padding, RRect.Y, RRect.Width - 2 * _padding, AppHeight * 2 / 5)
                Using br As New SolidBrush(ForeColor) : G.DrawString("______", Font, br, TextRect, StringAligner(ContentAlignment.MiddleCenter)) : End Using
#End Region

            Case Styles.Start7Aero
#Region "Start 7 Aero"
                Dim RestRect As New Rectangle(0, 14, Width - 5, Height - 10)

                If Not DesignMode AndAlso adaptedBackBlurred IsNot Nothing Then

                    'To dismiss upper part above start menu and make there is no blur bug
                    G.SetClip(RestRect)
                    G.DrawImage(adaptedBackBlurred, Rect)
                    G.ResetClip()

                    Dim alphaX As Single = 1 - BackColorAlpha / 100  'ColorBlurBalance
                    If alphaX < 0 Then alphaX = 0
                    If alphaX > 1 Then alphaX = 1

                    Dim ColBal As Single = Win7ColorBal / 100   'ColorBalance
                    Dim GlowBal As Single = Win7GlowBal / 100   'AfterGlowBalance
                    Dim Color1 As Color = Background
                    Dim Color2 As Color = Background2

                    G.DrawAeroEffect(RestRect, Nothing, Color1, ColBal, Color2, GlowBal, alphaX, 5, True)
                End If

                G.DrawRoundImage(Noise7Start, Rect, 5, True)

                G.DrawRoundImage(My.Resources.Start7, Rect, 5, True)
#End Region

            Case Styles.Start7Opaque
#Region "Start 7 Opaque"
                Dim RestRect As New Rectangle(0, 14, Width - 5, Height - 10)
                Using br As New SolidBrush(Color.White) : G.FillRoundedRect(br, RestRect, 5, True) : End Using
                Using br As New SolidBrush(Color.FromArgb(255 * BackColorAlpha / 100, Background)) : G.FillRoundedRect(br, RestRect, 5, True) : End Using
                G.DrawRoundImage(Noise7Start, Rect, 5, True)
                G.DrawRoundImage(My.Resources.Start7, Rect, 5, True)
#End Region

            Case Styles.Start7Basic
#Region "Start 7 Basic"
                G.DrawImage(My.Resources.Start7Basic, Rect)
#End Region

            Case Styles.Taskbar7Aero
#Region "Taskbar 7 Aero"

                If Not DesignMode AndAlso adaptedBackBlurred IsNot Nothing Then
                    G.DrawRoundImage(adaptedBackBlurred, RRect, Radius, True)

                    Dim alphaX As Single = 1 - BackColorAlpha / 100  'ColorBlurBalance
                    If alphaX < 0 Then alphaX = 0
                    If alphaX > 1 Then alphaX = 1

                    Dim ColBal As Single = Win7ColorBal / 100        'ColorBalance
                    Dim GlowBal As Single = Win7GlowBal / 100        'AfterGlowBalance
                    Dim Color1 As Color = Background
                    Dim Color2 As Color = Background2

                    G.DrawAeroEffect(Rect, adaptedBackBlurred, Color1, ColBal, Color2, GlowBal, alphaX, 0, False)
                End If

                G.DrawImage(My.Resources.Win7TaskbarSides, Rect)

                G.DrawRoundImage(Noise7.Clone(Bounds, PixelFormat.Format32bppArgb), Rect, Radius, True)

                Using P As New Pen(Color.FromArgb(80, 0, 0, 0)) : G.DrawLine(P, New Point(0, 0), New Point(Width - 1, 0)) : End Using
                Using P As New Pen(Color.FromArgb(80, 255, 255, 255)) : G.DrawLine(P, New Point(0, 1), New Point(Width - 1, 1)) : End Using

                G.DrawImage(My.Resources.AeroPeek, New Rectangle(Width - 10, 0, 10, Height))

                Dim StartORB As New Bitmap(My.Resources.Win7ORB)

                Dim StartBtnRect As New Rectangle(3, -3, 39, 39)

                Dim AppBtnRect As New Rectangle(StartBtnRect.Right + 5, 0, 45, 35)
                Dim AppBtnImgRect As New Rectangle(AppBtnRect.X + (AppBtnRect.Width - My.Resources.SampleApp_Active.Width) / 2, AppBtnRect.Y + (AppBtnRect.Height - My.Resources.SampleApp_Active.Height) / 2, My.Resources.SampleApp_Active.Width, My.Resources.SampleApp_Active.Height)

                Dim App2BtnRect As New Rectangle(AppBtnRect.Right + 1, 0, 45, 35)
                Dim App2BtnImgRect As New Rectangle(App2BtnRect.X + (App2BtnRect.Width - My.Resources.SampleApp_Inactive.Width) / 2, App2BtnRect.Y + (App2BtnRect.Height - My.Resources.SampleApp_Inactive.Height) / 2, My.Resources.SampleApp_Inactive.Width, My.Resources.SampleApp_Inactive.Height)

                G.DrawImage(StartORB, StartBtnRect)

                Using P As New Pen(Color.FromArgb(150, 0, 0, 0)) : G.DrawRoundedRect(P, New Rectangle(AppBtnRect.X, AppBtnRect.Y, AppBtnRect.Width - 2, AppBtnRect.Height - 2), 2, True) : End Using
                G.DrawImage(My.Resources.Taskbar_ActiveApp7, AppBtnRect)
                G.DrawImage(My.Resources.SampleApp_Active, AppBtnImgRect)

                Using P As New Pen(Color.FromArgb(110, 0, 0, 0)) : G.DrawRoundedRect(P, New Rectangle(App2BtnRect.X, App2BtnRect.Y, App2BtnRect.Width - 2, App2BtnRect.Height - 2), 2, True) : End Using
                G.DrawImage(My.Resources.Taskbar_InactiveApp7, App2BtnRect)
                G.DrawImage(My.Resources.SampleApp_Inactive, App2BtnImgRect)
#End Region

            Case Styles.Taskbar7Opaque
#Region "Taskbar 7 Opaque"
                Using br As New SolidBrush(Color.White) : G.FillRectangle(br, Rect) : End Using
                Using br As New SolidBrush(Color.FromArgb(255 * BackColorAlpha / 100, Background)) : G.FillRectangle(br, Rect) : End Using
                G.DrawImage(My.Resources.Win7TaskbarSides, Rect)

                G.DrawRoundImage(Noise7.Clone(Bounds, PixelFormat.Format32bppArgb), Rect, Radius, True)

                Using P As New Pen(Color.FromArgb(80, 0, 0, 0)) : G.DrawLine(P, New Point(0, 0), New Point(Width - 1, 0)) : End Using
                Using P As New Pen(Color.FromArgb(80, 255, 255, 255)) : G.DrawLine(P, New Point(0, 1), New Point(Width - 1, 1)) : End Using

                G.DrawImage(My.Resources.AeroPeek, New Rectangle(Width - 10, 0, 10, Height))

                Dim StartORB As New Bitmap(My.Resources.Win7ORB)

                Dim StartBtnRect As New Rectangle(3, -3, 39, 39)

                Dim AppBtnRect As New Rectangle(StartBtnRect.Right + 5, 0, 45, 35)
                Dim AppBtnImgRect As New Rectangle(AppBtnRect.X + (AppBtnRect.Width - My.Resources.SampleApp_Active.Width) / 2, AppBtnRect.Y + (AppBtnRect.Height - My.Resources.SampleApp_Active.Height) / 2, My.Resources.SampleApp_Active.Width, My.Resources.SampleApp_Active.Height)

                Dim App2BtnRect As New Rectangle(AppBtnRect.Right + 1, 0, 45, 35)
                Dim App2BtnImgRect As New Rectangle(App2BtnRect.X + (App2BtnRect.Width - My.Resources.SampleApp_Inactive.Width) / 2, App2BtnRect.Y + (App2BtnRect.Height - My.Resources.SampleApp_Inactive.Height) / 2, My.Resources.SampleApp_Inactive.Width, My.Resources.SampleApp_Inactive.Height)

                G.DrawImage(StartORB, StartBtnRect)

                Using P As New Pen(Color.FromArgb(150, 0, 0, 0)) : G.DrawRoundedRect(P, New Rectangle(AppBtnRect.X, AppBtnRect.Y, AppBtnRect.Width - 2, AppBtnRect.Height - 2), 2, True) : End Using
                G.DrawImage(My.Resources.Taskbar_ActiveApp7, AppBtnRect)
                G.DrawImage(My.Resources.SampleApp_Active, AppBtnImgRect)

                Using P As New Pen(Color.FromArgb(110, 0, 0, 0)) : G.DrawRoundedRect(P, New Rectangle(App2BtnRect.X, App2BtnRect.Y, App2BtnRect.Width - 2, App2BtnRect.Height - 2), 2, True) : End Using
                G.DrawImage(My.Resources.Taskbar_InactiveApp7, App2BtnRect)
                G.DrawImage(My.Resources.SampleApp_Inactive, App2BtnImgRect)
#End Region

            Case Styles.Taskbar7Basic
#Region "Taskbar 7 Basic"
                G.DrawImage(My.Resources.BasicTaskbar, Rect)

                G.DrawImage(My.Resources.AeroPeek, New Rectangle(Width - 10, 0, 10, Height))

                Dim StartORB As New Bitmap(My.Resources.Win7ORB)

                Dim StartBtnRect As New Rectangle(3, -3, 39, 39)

                Dim AppBtnRect As New Rectangle(StartBtnRect.Right + 5, 0, 45, 35)
                Dim AppBtnImgRect As New Rectangle(AppBtnRect.X + (AppBtnRect.Width - My.Resources.SampleApp_Active.Width) / 2, AppBtnRect.Y + (AppBtnRect.Height - My.Resources.SampleApp_Active.Height) / 2, My.Resources.SampleApp_Active.Width, My.Resources.SampleApp_Active.Height)

                Dim App2BtnRect As New Rectangle(AppBtnRect.Right + 1, 0, 45, 35)
                Dim App2BtnImgRect As New Rectangle(App2BtnRect.X + (App2BtnRect.Width - My.Resources.SampleApp_Inactive.Width) / 2, App2BtnRect.Y + (App2BtnRect.Height - My.Resources.SampleApp_Inactive.Height) / 2, My.Resources.SampleApp_Inactive.Width, My.Resources.SampleApp_Inactive.Height)

                G.DrawImage(StartORB, StartBtnRect)

                Using P As New Pen(Color.FromArgb(150, 0, 0, 0)) : G.DrawRoundedRect(P, New Rectangle(AppBtnRect.X, AppBtnRect.Y, AppBtnRect.Width - 2, AppBtnRect.Height - 2), 2, True) : End Using
                G.DrawImage(My.Resources.Taskbar_ActiveApp7, AppBtnRect)
                G.DrawImage(My.Resources.SampleApp_Active, AppBtnImgRect)

                Using P As New Pen(Color.FromArgb(110, 0, 0, 0)) : G.DrawRoundedRect(P, New Rectangle(App2BtnRect.X, App2BtnRect.Y, App2BtnRect.Width - 2, App2BtnRect.Height - 2), 2, True) : End Using
                G.DrawImage(My.Resources.Taskbar_InactiveApp7, App2BtnRect)
                G.DrawImage(My.Resources.SampleApp_Inactive, App2BtnImgRect)
#End Region

            Case Styles.AltTab7Aero
#Region "Alt+Tab 7 Aero"
                If Shadow And Not DesignMode Then G.DrawGlow(RRect, Color.FromArgb(150, 0, 0, 0), 5, 15)

                Dim inner As New Rectangle(RRect.X + 1, RRect.Y + 1, RRect.Width - 2, RRect.Height - 2)
                Dim Color1 As Color = Background
                Dim Color2 As Color = Background2

                If Not DesignMode AndAlso adaptedBackBlurred IsNot Nothing Then
                    Dim alpha As Single = 1 - BackColorAlpha / 100   'ColorBlurBalance
                    Dim ColBal As Single = Win7ColorBal / 100   'ColorBalance
                    Dim GlowBal As Single = Win7GlowBal / 100   'AfterGlowBalance
                    G.DrawAeroEffect(RRect, adaptedBackBlurred, Color1, ColBal, Color2, GlowBal, alpha, Radius, True)
                End If

                G.DrawRoundImage(Noise7.Clone(Bounds, PixelFormat.Format32bppArgb), RRect, Radius, True)
                Using P As New Pen(Color.FromArgb(200, 25, 25, 25)) : G.DrawRoundedRect(P, RRect, Radius, True) : End Using
                Using P As New Pen(Color.FromArgb(70, 200, 200, 200)) : G.DrawRoundedRect(P, inner, Radius, True) : End Using


                Dim AppHeight As Single = 0.75 * RRect.Height
                Dim _padding As Integer = (RRect.Height - AppHeight) / 2

                Dim appsNumber As Integer = 3
                Dim AllAppsWidthWithPadding As Single = RRect.Width - 2 * _padding
                Dim AppWidth As Single = (AllAppsWidthWithPadding - (appsNumber - 1) * _padding) / appsNumber

                Dim Rects As New List(Of Rectangle)
                Rects.Clear()

                For x = 0 To appsNumber - 1
                    If x = 0 Then
                        Rects.Add(New Rectangle(RRect.X + _padding, RRect.Y + _padding + AppHeight * 2 / 5, AppWidth, AppHeight * 3 / 5))
                    Else
                        Rects.Add(New Rectangle(Rects(x - 1).Right + _padding, RRect.Y + _padding + AppHeight * 2 / 5, AppWidth, AppHeight * 3 / 5))
                    End If
                Next

                For x = 0 To Rects.Count - 1
                    Dim r As Rectangle = Rects(x)

                    If x = 0 Then
                        Dim surround As New Rectangle(r.X - 10, r.Y - 10, r.Width + 20, r.Height + 20)
                        Using br As New SolidBrush(Color.FromArgb(75, 200, 200, 200)) : G.FillRoundedRect(br, surround, 1, True) : End Using
                        G.DrawRoundImage(My.Resources.Win7_TitleTopL.Fade(0.35), surround, 2, True)
                        G.DrawRoundImage(My.Resources.Win7_TitleTopR.Fade(0.35), surround, 2, True)

                        Using P As New Pen(Color1) : G.DrawRoundedRect(P, surround, 1, True) : End Using
                        Using P As New Pen(Color.FromArgb(229, 240, 250)) : G.DrawRectangle(P, New Rectangle(surround.X + 1, surround.Y + 1, surround.Width - 2, surround.Height - 2)) : End Using

                    End If

                    G.FillRoundedRect(Brushes.White, r, 2, True)
                    G.DrawRoundedRect(Pens.Black, r, 2, True)

                    Dim icon_w As Integer = My.Resources.SampleApp_Active.Width

                    Dim icon_rect As New Rectangle(r.X + r.Width - 0.7 * icon_w, r.Y + r.Height - 0.6 * icon_w, icon_w, icon_w)

                    G.DrawImage(My.Resources.SampleApp_Active, icon_rect)
                Next

                Dim TextRect As New Rectangle(RRect.X + _padding, RRect.Y, RRect.Width - 2 * _padding, AppHeight * 2 / 5)
                G.DrawGlowString(2, "______", Font, Color.Black, Color.FromArgb(185, 225, 225, 225), RRect, TextRect, StringAligner(ContentAlignment.MiddleCenter))

#End Region

            Case Styles.AltTab7Opaque
#Region "Alt+Tab 7 Opaque"
                If Shadow And Not DesignMode Then
                    G.DrawGlow(RRect, Color.FromArgb(150, 0, 0, 0), 5, 15)
                End If
                Dim inner As New Rectangle(RRect.X + 1, RRect.Y + 1, RRect.Width - 2, RRect.Height - 2)

                Using br As New SolidBrush(Color.White) : G.FillRoundedRect(br, RRect, Radius, True) : End Using
                Using br As New SolidBrush(Color.FromArgb(255 * Win7ColorBal / 100, Background)) : G.FillRoundedRect(br, RRect, Radius, True) : End Using

                G.DrawRoundImage(Noise7.Clone(Bounds, PixelFormat.Format32bppArgb), RRect, Radius, True)
                Using P As New Pen(Color.FromArgb(200, 25, 25, 25)) : G.DrawRoundedRect(P, RRect, Radius, True) : End Using
                Using P As New Pen(Color.FromArgb(70, 200, 200, 200)) : G.DrawRoundedRect(P, inner, Radius, True) : End Using

                Dim AppHeight As Single = 0.75 * RRect.Height
                Dim _padding As Integer = (RRect.Height - AppHeight) / 2

                Dim appsNumber As Integer = 3
                Dim AllAppsWidthWithPadding As Single = RRect.Width - 2 * _padding
                Dim AppWidth As Single = (AllAppsWidthWithPadding - (appsNumber - 1) * _padding) / appsNumber

                Dim Rects As New List(Of Rectangle)
                Rects.Clear()

                For x = 0 To appsNumber - 1
                    If x = 0 Then
                        Rects.Add(New Rectangle(RRect.X + _padding, RRect.Y + _padding + AppHeight * 2 / 5, AppWidth, AppHeight * 3 / 5))
                    Else
                        Rects.Add(New Rectangle(Rects(x - 1).Right + _padding, RRect.Y + _padding + AppHeight * 2 / 5, AppWidth, AppHeight * 3 / 5))
                    End If
                Next

                For x = 0 To Rects.Count - 1
                    Dim r As Rectangle = Rects(x)

                    If x = 0 Then
                        Dim surround As New Rectangle(r.X - 10, r.Y - 10, r.Width + 20, r.Height + 20)
                        Using br As New SolidBrush(Color.FromArgb(75, 200, 200, 200)) : G.FillRoundedRect(br, surround, 1, True) : End Using
                        G.DrawRoundImage(My.Resources.Win7_TitleTopL.Fade(0.35), surround, 2, True)
                        G.DrawRoundImage(My.Resources.Win7_TitleTopR.Fade(0.35), surround, 2, True)

                        Using P As New Pen(Background) : G.DrawRoundedRect(P, surround, 1, True) : End Using
                        Using P As New Pen(Color.FromArgb(229, 240, 250)) : G.DrawRectangle(P, New Rectangle(surround.X + 1, surround.Y + 1, surround.Width - 2, surround.Height - 2)) : End Using

                    End If

                    G.FillRoundedRect(Brushes.White, r, 2, True)
                    G.DrawRoundedRect(Pens.Black, r, 2, True)

                    Dim icon_w As Integer = My.Resources.SampleApp_Active.Width

                    Dim icon_rect As New Rectangle(r.X + r.Width - 0.7 * icon_w, r.Y + r.Height - 0.6 * icon_w, icon_w, icon_w)

                    G.DrawImage(My.Resources.SampleApp_Active, icon_rect)
                Next

                Dim TextRect As New Rectangle(RRect.X + _padding, RRect.Y, RRect.Width - 2 * _padding, AppHeight * 2 / 5)
                G.DrawGlowString(2, "______", Font, Color.Black, Color.FromArgb(185, 225, 225, 225), RRect, TextRect, StringAligner(ContentAlignment.MiddleCenter))

#End Region

            Case Styles.AltTab7Basic
#Region "Alt+Tab 7 Basic"
                Dim Titlebar_Background1 As Color = Color.FromArgb(152, 180, 208)
                Dim Titlebar_BackColor2 As Color = Color.FromArgb(186, 210, 234)
                Dim Titlebar_OuterBorder As Color = Color.FromArgb(52, 52, 52)
                Dim Titlebar_InnerBorder As Color = Color.FromArgb(255, 255, 255)
                Dim Titlebar_Turquoise As Color = Color.FromArgb(40, 207, 228)
                Dim OuterBorder As Color = Color.FromArgb(0, 0, 0)
                Dim UpperPart As New Rectangle(RRect.X, RRect.Y, RRect.Width + 1, 25)
                G.SetClip(UpperPart)
                Dim pth_back As New LinearGradientBrush(UpperPart, Titlebar_Background1, Titlebar_BackColor2, LinearGradientMode.Vertical)
                Dim pth_line As New LinearGradientBrush(UpperPart, Titlebar_InnerBorder, Titlebar_Turquoise, LinearGradientMode.Vertical)
                '### Render Titlebar
                G.FillRectangle(pth_back, RRect)
                Using P As New Pen(Titlebar_OuterBorder) : G.DrawRectangle(P, RRect) : End Using
                Using P As New Pen(Titlebar_InnerBorder) : G.DrawRectangle(P, New Rectangle(RRect.X + 1, RRect.Y + 1, RRect.Width - 2, RRect.Height - 2)) : End Using
                G.SetClip(New Rectangle(UpperPart.X + UpperPart.Width * 0.75, UpperPart.Y, UpperPart.Width * 0.75, UpperPart.Height))
                Using P As New Pen(pth_line) : G.DrawRectangle(P, New Rectangle(RRect.X + 1, RRect.Y + 1, RRect.Width - 2, RRect.Height - 2)) : End Using
                G.ResetClip()
                G.ExcludeClip(UpperPart)
                '### Render Rest of Window
                Using br As New SolidBrush(Titlebar_BackColor2) : G.FillRectangle(br, RRect) : End Using
                Using P As New Pen(Titlebar_Turquoise) : G.DrawRectangle(P, New Rectangle(RRect.X + 1, RRect.Y + 1, RRect.Width - 2, RRect.Height - 2)) : End Using
                Using P As New Pen(OuterBorder) : G.DrawRectangle(P, RRect) : End Using
                G.ResetClip()
                Using P As New Pen(Color.FromArgb(52, 52, 52)) : G.DrawRectangle(P, RRect) : End Using
                Using P As New Pen(Color.FromArgb(255, 225, 225, 225)) : G.DrawRectangle(P, New Rectangle(RRect.X + 1, RRect.Y + 1, RRect.Width - 2, RRect.Height - 2)) : End Using


                Dim AppHeight As Single = My.Resources.Win7AltTabBasicButton.Height
                Dim _padding As Integer = 5

                Dim appsNumber As Integer = 3
                Dim AppWidth As Single = My.Resources.Win7AltTabBasicButton.Width

                Dim _paddingOuter As Integer = (RRect.Width - AppWidth * appsNumber - _padding * (appsNumber - 1)) / 2

                Dim Rects As New List(Of Rectangle)
                Rects.Clear()

                For x = 0 To appsNumber - 1
                    If x = 0 Then
                        Rects.Add(New Rectangle(RRect.X + _paddingOuter, RRect.Y + RRect.Height - 5 - AppHeight, AppWidth, AppHeight))
                    Else
                        Rects.Add(New Rectangle(Rects(x - 1).Right + _padding, RRect.Y + RRect.Height - 5 - AppHeight, AppWidth, AppHeight))
                    End If
                Next

                For x = 0 To Rects.Count - 1
                    Dim r As Rectangle = Rects(x)
                    If x = 0 Then G.DrawImage(My.Resources.Win7AltTabBasicButton, r)

                    Dim imgrect As New Rectangle(r.X + (r.Width - My.Resources.SampleApp_Active.Width) / 2, r.Y + (r.Height - My.Resources.SampleApp_Active.Height) / 2, My.Resources.SampleApp_Active.Width, My.Resources.SampleApp_Active.Height)

                    G.DrawImage(My.Resources.SampleApp_Active, imgrect)
                Next

                Dim TextRect As New Rectangle(RRect.X + _padding, RRect.Y, RRect.Width - 2 * _padding, 30)
                G.DrawString("______", Font, Brushes.Black, TextRect, StringAligner(ContentAlignment.MiddleCenter))
#End Region

            Case Styles.StartVistaAero
#Region "Start Vista Aero"
                Dim RestRect As New Rectangle(0, 14, Width - 6, Height - 14)

                'To dismiss upper part above start menu and make there is no blur bug
                G.SetClip(RestRect)
                If Not DesignMode AndAlso adaptedBackBlurred IsNot Nothing Then G.DrawImage(adaptedBackBlurred, Rect)
                G.ResetClip()

                Using br As New SolidBrush(Color.FromArgb(BackColorAlpha, Background)) : G.FillRoundedRect(br, RestRect, 4, True) : End Using
                G.DrawImage(My.Resources.Vista_StartAero, New Rectangle(0, 0, Width, Height))
#End Region

            Case Styles.StartVistaOpaque
#Region "Start Vista Opaque"
                Dim RestRect As New Rectangle(0, 14, Width - 6, Height - 14)
                G.FillRoundedRect(Brushes.White, RestRect, 4, True)
                Using br As New SolidBrush(Color.FromArgb(BackColorAlpha, Background)) : G.FillRoundedRect(br, RestRect, 4, True) : End Using
                G.DrawImage(My.Resources.Vista_StartAero, New Rectangle(0, 0, Width, Height))
#End Region

            Case Styles.StartVistaBasic
#Region "Start Vista Basic"
                G.DrawImage(My.Resources.Vista_StartBasic, New Rectangle(0, 0, Width, Height))
#End Region

            Case Styles.TaskbarVistaAero
#Region "Taskbar Vista Aero"
                If Not DesignMode AndAlso adaptedBackBlurred IsNot Nothing Then G.DrawImage(adaptedBackBlurred, Rect)
                Using br As New SolidBrush(Color.FromArgb(BackColorAlpha, Background)) : G.FillRectangle(br, Rect) : End Using
                G.FillRectangle(New TextureBrush(My.Resources.Vista_Taskbar), Rect)
                Dim orb As Bitmap = My.Resources.Vista_StartLowerORB
                G.DrawImage(orb, New Rectangle(0, 0, orb.Width, Height))

                Dim apprect1 As New Rectangle(Rect.X + 60, 1, 140, Rect.Height - 4)
                Dim apprect2 As New Rectangle(apprect1.Right + 2, 1, 140, Rect.Height - 4)
                Dim appIcon1 As New Rectangle(apprect1.X + 4, apprect1.Y + (apprect1.Height - 20) / 2, 20, 20)
                Dim appIcon2 As New Rectangle(apprect2.X + 4, apprect2.Y + (apprect2.Height - 20) / 2, 20, 20)
                Dim appLabel1 As New Rectangle(apprect1.X + 25, apprect1.Y, apprect1.Width - 30, apprect1.Height)
                Dim appLabel2 As New Rectangle(apprect2.X + 25, apprect2.Y, apprect2.Width - 30, apprect2.Height)

                G.DrawImage(My.Resources.Vista_ActiveApp, apprect1)
                G.DrawImage(My.Resources.Vista_InactiveApp, apprect2)

                G.DrawImage(My.Resources.SampleApp_Active, appIcon1)
                G.DrawImage(My.Resources.SampleApp_Inactive, appIcon2)

                G.DrawString("App Preview", Font, Brushes.White, appLabel1, StringAligner(ContentAlignment.MiddleLeft))
                G.DrawString("Inactive app", Font, Brushes.White, appLabel2, StringAligner(ContentAlignment.MiddleLeft))
#End Region

            Case Styles.TaskbarVistaOpaque
#Region "Taskbar Vista Opaque"
                Dim orb As Bitmap = My.Resources.Vista_StartLowerORB
                G.FillRectangle(Brushes.White, Rect)
                Using br As New SolidBrush(Color.FromArgb(BackColorAlpha, Background)) : G.FillRectangle(br, Rect) : End Using
                G.FillRectangle(New TextureBrush(My.Resources.Vista_Taskbar), Rect)
                G.DrawImage(orb, New Rectangle(0, 0, orb.Width, Height))

                Dim apprect1 As New Rectangle(Rect.X + 60, 1, 140, Rect.Height - 4)
                Dim apprect2 As New Rectangle(apprect1.Right + 2, 1, 140, Rect.Height - 4)
                Dim appIcon1 As New Rectangle(apprect1.X + 4, apprect1.Y + (apprect1.Height - 20) / 2, 20, 20)
                Dim appIcon2 As New Rectangle(apprect2.X + 4, apprect2.Y + (apprect2.Height - 20) / 2, 20, 20)
                Dim appLabel1 As New Rectangle(apprect1.X + 25, apprect1.Y, apprect1.Width - 30, apprect1.Height)
                Dim appLabel2 As New Rectangle(apprect2.X + 25, apprect2.Y, apprect2.Width - 30, apprect2.Height)

                G.DrawImage(My.Resources.Vista_ActiveApp, apprect1)
                G.DrawImage(My.Resources.Vista_InactiveApp, apprect2)

                G.DrawImage(My.Resources.SampleApp_Active, appIcon1)
                G.DrawImage(My.Resources.SampleApp_Inactive, appIcon2)

                G.DrawString("App Preview", Font, Brushes.White, appLabel1, StringAligner(ContentAlignment.MiddleLeft))
                G.DrawString("Inactive app", Font, Brushes.White, appLabel2, StringAligner(ContentAlignment.MiddleLeft))
#End Region

            Case Styles.TaskbarVistaBasic
#Region "Taskbar Vista Basic"
                Dim orb As Bitmap = My.Resources.Vista_StartLowerORB
                G.FillRectangle(New TextureBrush(My.Resources.Vista_Taskbar), Rect)
                G.DrawImage(orb, New Rectangle(0, 0, orb.Width, Height))

                Dim apprect1 As New Rectangle(Rect.X + 60, 1, 140, Rect.Height - 4)
                Dim apprect2 As New Rectangle(apprect1.Right + 2, 1, 140, Rect.Height - 4)
                Dim appIcon1 As New Rectangle(apprect1.X + 4, apprect1.Y + (apprect1.Height - 20) / 2, 20, 20)
                Dim appIcon2 As New Rectangle(apprect2.X + 4, apprect2.Y + (apprect2.Height - 20) / 2, 20, 20)
                Dim appLabel1 As New Rectangle(apprect1.X + 25, apprect1.Y, apprect1.Width - 30, apprect1.Height)
                Dim appLabel2 As New Rectangle(apprect2.X + 25, apprect2.Y, apprect2.Width - 30, apprect2.Height)

                G.DrawImage(My.Resources.Vista_ActiveApp, apprect1)
                G.DrawImage(My.Resources.Vista_InactiveApp, apprect2)

                G.DrawImage(My.Resources.SampleApp_Active, appIcon1)
                G.DrawImage(My.Resources.SampleApp_Inactive, appIcon2)

                G.DrawString("App Preview", Font, Brushes.White, appLabel1, StringAligner(ContentAlignment.MiddleLeft))
                G.DrawString("Inactive app", Font, Brushes.White, appLabel2, StringAligner(ContentAlignment.MiddleLeft))
#End Region

            Case Styles.StartXP
#Region "Start XP"
                G.DrawImage(My.LunaRes.Start, Rect)
#End Region

            Case Styles.TaskbarXP
#Region "Taskbar XP"
                Try
                    Dim sm As SmoothingMode = G.SmoothingMode
                    G.SmoothingMode = SmoothingMode.HighSpeed

                    My.resVS.Draw(G, Rect, VisualStylesRes.Element.Taskbar, True, False)
                    G.DrawImage(My.LunaRes.StartBtn, New Rectangle(0, 0, My.LunaRes.StartBtn.Width, Rect.Height - 1))

                    G.SmoothingMode = sm
                Catch
                End Try
#End Region

        End Select

    End Sub

    Private Sub XenonWinElement_HandleCreated(sender As Object, e As EventArgs) Handles Me.HandleCreated
        If Not DesignMode Then
            Try : AddHandler Parent.BackgroundImageChanged, AddressOf ProcessBack : Catch : End Try
            ProcessBack()
        End If
    End Sub

    Sub ProcessBack()
        GetBack()
    End Sub

    Sub GetBack()
        Try
            If Style = Styles.Taskbar11 Or Style = Styles.Start11 Or Style = Styles.ActionCenter11 Or Style = Styles.AltTab11 Then

                If Transparency Then
                    Dim b As Bitmap
                    If My.Wallpaper IsNot Nothing Then
                        b = My.Wallpaper.Clone(Bounds, My.Wallpaper.PixelFormat).Blur(BlurPower)
                    End If

                    If DarkMode Then
                        If b IsNot Nothing Then
                            Using ImgF As New ImageProcessor.ImageFactory
                                ImgF.Load(b)
                                ImgF.Saturation(60)
                                adaptedBackBlurred = ImgF.Image.Clone
                            End Using
                        End If

                    Else
                        adaptedBackBlurred = b
                    End If
                Else
                    adaptedBackBlurred = Nothing
                End If

            ElseIf Style = Styles.Taskbar10 Or Style = Styles.Start10 Or Style = Styles.ActionCenter10 Then
                If Transparency AndAlso My.Wallpaper IsNot Nothing Then
                    adaptedBackBlurred = My.Wallpaper.Clone(Bounds, My.Wallpaper.PixelFormat).Blur(BlurPower)
                Else
                    adaptedBackBlurred = Nothing
                End If

            ElseIf Style = Styles.Start7Aero Or Style = Styles.Taskbar7Aero Or Style = Styles.StartVistaAero Or Style = Styles.TaskbarVistaAero Or Style = Styles.AltTab7Aero Then
                If My.Wallpaper IsNot Nothing Then
                    adaptedBackBlurred = My.Wallpaper.Clone(Bounds, My.Wallpaper.PixelFormat).Blur(1)
                Else
                    adaptedBackBlurred = Nothing
                End If

            Else
                adaptedBackBlurred = Nothing

            End If

        Catch
            adaptedBackBlurred = Nothing

        End Try
    End Sub

    Sub NoiseBack()

        If Style = Styles.ActionCenter11 Or Style = Styles.Start11 Or Style = Styles.Taskbar11 Or Style = Styles.AltTab11 Then
            If Transparency Then Noise = New TextureBrush(My.Resources.GaussianBlur.Fade(NoisePower))

        ElseIf Style = Styles.ActionCenter10 Or Style = Styles.Start10 Or Style = Styles.Taskbar10 Then
            If Transparency Then Noise = New TextureBrush(My.Resources.GaussianBlur.Fade(NoisePower))

        ElseIf Style = Styles.Start7Aero Or Style = Styles.Taskbar7Aero Or Style = Styles.AltTab7Aero Or Style = Styles.AltTab7Opaque Then
            Try : Noise7 = My.Resources.AeroGlass.Fade(NoisePower / 100) : Catch : End Try
            Try : Noise7Start = My.Resources.Start7Glass.Fade(NoisePower / 100) : Catch : End Try

        End If

    End Sub

    Private Sub XenonWinElement_BackColorChanged(sender As Object, e As EventArgs) Handles Me.BackColorChanged
        If Not BackColor = Color.Transparent Then BackColor = Color.Transparent
    End Sub
End Class
Public Class XenonWindow : Inherits Panel
    Sub New()
        AdjustPadding()
        Font = New Font("Segoe UI", 9)
        DoubleBuffered = True
        SetStyle(ControlStyles.SupportsTransparentBackColor, True)
        BackColor = Color.Transparent
    End Sub

    Protected Overrides ReadOnly Property CreateParams As CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ExStyle = cp.ExStyle Or &H20
            Return cp
        End Get
    End Property

#Region "Properties"
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
    Public Property WinVista As Boolean = False
    Public Property SuspendRefresh As Boolean = False

    Public Event MetricsChanged()
    Private _DarkMode As Boolean = True
    Public Property DarkMode() As Boolean
        Get
            Return _DarkMode
        End Get
        Set(ByVal value As Boolean)
            _DarkMode = value
            If Not SuspendRefresh Then Refresh()
        End Set
    End Property

    Private _AccentColor_Enabled As Boolean = True
    Public Property AccentColor_Enabled() As Boolean
        Get
            Return _AccentColor_Enabled
        End Get
        Set(ByVal value As Boolean)
            _AccentColor_Enabled = value
            If Not SuspendRefresh Then Refresh()
        End Set
    End Property

    Private _Win7Noise As Single = 1
    Public Property Win7Noise() As Single
        Get
            Return _Win7Noise
        End Get
        Set(ByVal value As Single)
            _Win7Noise = value
            If Preview = Preview_Enum.W7Aero Or Preview = Preview_Enum.W7Opaque Or Preview = Preview_Enum.W7Basic Then
                Try : Noise7 = My.Resources.AeroGlass.Fade(Win7Noise / 100) : Catch : End Try
            End If
            If Not SuspendRefresh Then Refresh()
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
            If Not SuspendRefresh Then Refresh()
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
            If Not SuspendRefresh Then Refresh()
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
            If Not SuspendRefresh Then Refresh()
            RaiseEvent MetricsChanged()
        End Set
    End Property

    <Browsable(True)>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    <EditorBrowsable(EditorBrowsableState.Always)>
    <Editor(GetType(System.ComponentModel.Design.MultilineStringEditor), GetType(System.Drawing.Design.UITypeEditor))>
    <Bindable(True)>
    Public Overrides Property Text As String
#End Region

#Region "Helpers"
    Dim AdaptedBackBlurred As Bitmap
    Dim Noise7 As Bitmap = My.Resources.AeroGlass
    ReadOnly FreeMargin As Integer = 8

    Public Sub CopycatFrom(Window As XenonWindow, Optional IgnoreLocationSizesAndText As Boolean = False)
        Shadow = Window.Shadow
        Radius = Window.Radius
        AccentColor_Active = Window.AccentColor_Active
        AccentColor_Inactive = Window.AccentColor_Inactive
        AccentColor2_Active = Window.AccentColor2_Active
        AccentColor2_Inactive = Window.AccentColor2_Inactive
        Active = Window.Active
        Preview = Window.Preview
        Win7Alpha = Window.Win7Alpha
        Win7ColorBal = Window.Win7ColorBal
        Win7GlowBal = Window.Win7GlowBal
        WinVista = Window.WinVista
        _DarkMode = Window.DarkMode
        _AccentColor_Enabled = Window.AccentColor_Enabled
        _Win7Noise = Window.Win7Noise

        If Not IgnoreLocationSizesAndText Then
            ToolWindow = Window.ToolWindow
            Dock = Window.Dock
            Size = Window.Size
            Location = Window.Location
            Text = Window.Text
        End If

        ProcessBack()
        Refresh()
    End Sub
    Public Sub SetMetrics(ByVal [XenonWindow] As XenonWindow)
        [XenonWindow].Metrics_BorderWidth = Metrics_BorderWidth
        [XenonWindow].Metrics_CaptionHeight = Metrics_CaptionHeight
        [XenonWindow].Metrics_PaddedBorderWidth = Metrics_PaddedBorderWidth
        [XenonWindow].Refresh()
    End Sub
    Sub AdjustPadding()
        Dim i, iTop As Integer

        Dim TitleTextH, TitleTextH_9, TitleTextH_Sum As Integer
        TitleTextH = "ABCabc0123xYz.#".Measure(Font).Height
        TitleTextH_9 = "ABCabc0123xYz.#".Measure(New Font(Font.Name, 9, Font.Style)).Height
        TitleTextH_Sum = Math.Max(0, TitleTextH - TitleTextH_9 - 5)

        If Preview = Preview_Enum.W7Aero Or Preview = Preview_Enum.W7Opaque Or Preview = Preview_Enum.W7Basic Or Preview = Preview_Enum.W8 Or Preview = Preview_Enum.W8Lite Or Preview = Preview_Enum.WXP Then

            i = FreeMargin + If(Not Preview = Preview_Enum.WXP, _Metrics_PaddedBorderWidth, 0) + _Metrics_BorderWidth
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
        WXP
    End Enum
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
#End Region

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        Dim G As Graphics = e.Graphics
        G.SmoothingMode = SmoothingMode.AntiAlias
        G.TextRenderingHint = My.RenderingHint
        DoubleBuffered = True

        If Win7Alpha > 255 Then Win7Alpha = 255
        If Win7Alpha < 0 Then Win7Alpha = 0

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
        Dim XRect As New Rectangle(Rect.Right - 35, Rect.Y, 35, TitlebarRect.Height)

        'G.Clear(Color.Transparent)


        If Preview = Preview_Enum.W11 Then
#Region "Windows 11"
            If Shadow And Active And Not DesignMode Then
                G.DrawGlow(Rect, Color.FromArgb(150, 0, 0, 0), 5, 15)
            End If

            If Not AccentColor_Enabled AndAlso Active Then
                G.SetClip(TitlebarRect)
                G.DrawRoundImage(AdaptedBackBlurred, Rect, Radius, True)
                G.ResetClip()
            End If

            G.ExcludeClip(TitlebarRect)
            If DarkMode Then
                Using br As New SolidBrush(Color.FromArgb(20, 20, 20)) : G.FillRoundedRect(br, Rect, Radius, True) : End Using
            Else
                Using br As New SolidBrush(Color.FromArgb(240, 240, 240)) : G.FillRoundedRect(br, Rect, Radius, True) : End Using
            End If
            G.ResetClip()

            If AccentColor_Enabled Then
                If Active Then
                    Using P As New Pen(Color.FromArgb(200, AccentColor_Active)) : G.DrawRoundedRect(P, Rect, Radius, True) : End Using
                Else
                    Using P As New Pen(Color.FromArgb(200, AccentColor_Inactive)) : G.DrawRoundedRect(P, Rect, Radius, True) : End Using
                End If
            Else
                If DarkMode Then
                    Using P As New Pen(Color.FromArgb(200, 100, 100, 100)) : G.DrawRoundedRect(P, Rect, Radius, True) : End Using
                Else
                    Using P As New Pen(Color.FromArgb(200, 220, 220, 220)) : G.DrawRoundedRect(P, Rect, Radius, True) : End Using
                End If
            End If

            If AccentColor_Enabled Then
                If Active Then
                    Using br As New SolidBrush(AccentColor_Active) : FillSemiRect(G, br, TitlebarRect, Radius) : End Using
                    Using P As New Pen(AccentColor_Active) : G.DrawLine(P, New Point(TitlebarRect.X + 1, TitlebarRect.Y + TitlebarRect.Height), New Point(TitlebarRect.X + TitlebarRect.Width - 1, TitlebarRect.Y + TitlebarRect.Height)) : End Using
                Else
                    Using br As New SolidBrush(AccentColor_Inactive) : FillSemiRect(G, br, TitlebarRect, Radius) : End Using
                    Using P As New Pen(AccentColor_Inactive) : G.DrawLine(P, New Point(TitlebarRect.X + 1, TitlebarRect.Y + TitlebarRect.Height), New Point(TitlebarRect.X + TitlebarRect.Width - 1, TitlebarRect.Y + TitlebarRect.Height)) : End Using
                End If
            Else
                Dim a As Integer = If(Active, If(DarkMode, 180, 245), 255)
                If DarkMode Then
                    Using br As New SolidBrush(Color.FromArgb(a, 32, 32, 32)) : FillSemiRect(G, br, TitlebarRect, Radius) : End Using
                Else
                    Using br As New SolidBrush(Color.FromArgb(a, 245, 245, 245)) : FillSemiRect(G, br, TitlebarRect, Radius) : End Using
                End If
            End If
#End Region

        ElseIf Preview = Preview_Enum.W10 Then
#Region "Windows 10"
            If Shadow And Active And Not DesignMode Then
                G.DrawGlow(Rect, Color.FromArgb(150, 0, 0, 0), 5, 15)
            End If

            If DarkMode Then
                Using br As New SolidBrush(Color.FromArgb(20, 20, 20)) : G.FillRectangle(br, Rect) : End Using
            Else
                Using br As New SolidBrush(Color.FromArgb(240, 240, 240)) : G.FillRectangle(br, Rect) : End Using
            End If

            If AccentColor_Enabled Then
                If Active Then
                    Using P As New Pen(Color.FromArgb(200, AccentColor_Active)) : G.DrawRectangle(P, Rect) : End Using
                Else
                    Using P As New Pen(Color.FromArgb(200, AccentColor_Inactive)) : G.DrawRectangle(P, Rect) : End Using
                End If
            Else
                If DarkMode Then
                    Using P As New Pen(Color.FromArgb(200, 100, 100, 100)) : G.DrawRectangle(P, Rect) : End Using
                Else
                    Using P As New Pen(Color.FromArgb(200, 220, 220, 220)) : G.DrawRectangle(P, Rect) : End Using
                End If
            End If

            If AccentColor_Enabled Then
                If Active Then
                    Using br As New SolidBrush(AccentColor_Active) : G.FillRectangle(br, TitlebarRect) : End Using
                Else
                    Using br As New SolidBrush(AccentColor_Inactive) : G.FillRectangle(br, TitlebarRect) : End Using
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

            Using br As New SolidBrush(bc) : G.FillRectangle(br, Rect) : End Using
            Using br As New SolidBrush(c) : G.FillRectangle(br, Rect) : End Using

            Using br As New SolidBrush(Color.White) : G.FillRectangle(br, InnerWindow_1) : End Using

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
                Using P As New Pen(Color.FromArgb(170, bc.CB(-0.2))) : G.DrawRectangle(P, InnerWindow_1) : End Using
                Using P As New Pen(Color.FromArgb((Win7ColorBal / 100) * 255, c.CB(-0.2))) : G.DrawRectangle(P, InnerWindow_1) : End Using

                G.SmoothingMode = SmoothingMode.HighSpeed
                Using br As New SolidBrush(If(Active, Color.FromArgb(199, 80, 80), Color.FromArgb(188, 188, 188))) : G.FillRectangle(br, CloseRect) : End Using
                G.SmoothingMode = SmoothingMode.AntiAlias

                G.DrawImage(CloseBtn, New Rectangle(CloseRect.X + (CloseRect.Width - CloseBtn.Width) / 2, CloseRect.Y + (CloseRect.Height - CloseBtn.Height) / 2, CloseBtn.Width, CloseBtn.Height))

                Using P As New Pen(Color.FromArgb(200, bc.CB(-0.3))) : G.DrawRectangle(P, Rect) : End Using
                Using P As New Pen(Color.FromArgb((Win7ColorBal / 100) * 255, c.CB(-0.3))) : G.DrawRectangle(P, Rect) : End Using

            Else

                Using P As New Pen(Color.FromArgb((Win7ColorBal / 100) * 255, c).LightLight) : G.DrawLine(P, New Point(InnerWindow_1.X, InnerWindow_1.Y), New Point(InnerWindow_1.X + InnerWindow_1.Width, InnerWindow_1.Y)) : End Using

                Using P As New Pen(Color.FromArgb((Win7ColorBal / 100) * 255, c).LightLight) : G.DrawLine(P, New Point(InnerWindow_1.X, InnerWindow_1.Y + InnerWindow_1.Height), New Point(InnerWindow_1.X + InnerWindow_1.Width, InnerWindow_1.Y + InnerWindow_1.Height)) : End Using

                Using br As New SolidBrush(If(Active, Color.FromArgb(195, 90, 80), Color.Transparent)) : G.FillRectangle(br, CloseRect) : End Using

                G.SmoothingMode = SmoothingMode.HighSpeed
                Using P As New Pen(If(Active, Color.FromArgb(92, 58, 55), Color.FromArgb(93, 96, 102))) : G.DrawRectangle(P, CloseRect) : End Using
                G.SmoothingMode = SmoothingMode.AntiAlias

                G.DrawImage(CloseBtn, New Rectangle(CloseRect.X + (CloseRect.Width - CloseBtn.Width) / 2, CloseRect.Y + (CloseRect.Height - CloseBtn.Height) / 2, CloseBtn.Width, CloseBtn.Height))

                G.DrawRectangle(New Drawing.Pen(Color.FromArgb(47, 48, 51)), Rect)
            End If
#End Region

        ElseIf Preview = Preview_Enum.W7Aero Or Preview = Preview_Enum.W7Opaque Or Preview = Preview_Enum.W7Basic Then
#Region "Windows 7\Vista"
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
                    G.ExcludeClip(InnerWindow_1)
                    G.DrawAeroEffect(Rect, bk, Color1, ColBal, Color2, GlowBal, alpha, Radius, Not ToolWindow)
                    G.ResetClip()
                Else

                    If Not ToolWindow Then
                        Using br As New SolidBrush(Color.White) : G.FillRoundedRect(br, Rect, Radius, True) : End Using
                        Using br As New SolidBrush(Color.FromArgb(255 * Win7Alpha / 100, If(Active, AccentColor_Active, AccentColor_Inactive))) : G.FillRoundedRect(br, Rect, Radius, True) : End Using
                    Else
                        Using br As New SolidBrush(Color.White) : G.FillRectangle(br, Rect) : End Using
                        Using br As New SolidBrush(Color.FromArgb(255 * Win7Alpha / 100, If(Active, AccentColor_Active, AccentColor_Inactive))) : G.FillRectangle(br, Rect) : End Using
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
                    G.DrawRoundImage(Noise7.Clone(Bounds, PixelFormat.Format32bppArgb), Rect, Radius, True)
                    Using P As New Pen(Color.FromArgb(If(Active, 130, 100), 25, 25, 25)) : G.DrawRoundedRect(P, Rect, Radius, True) : End Using
                    Using P As New Pen(Color.FromArgb(100, 255, 255, 255)) : G.DrawRoundedRect(P, inner, Radius, True) : End Using
                    'Using P As New Pen(Color.FromArgb(255 - 255 * Win7Alpha / 300, BackColor, 0.2))) : DrawRect(G, P, Rect, Radius, True) : End Using
                    Using P As New Pen(Color.FromArgb(255 - 255 * Win7Alpha / 300, BackColor.Light(0.2))) : G.DrawRoundedRect(P, InnerWindow_1, 1, True) : End Using
                    Using br As New SolidBrush(Color.White) : G.FillRoundedRect(br, InnerWindow_1, 1, True) : End Using
                    Using P As New Pen(Color.FromArgb(255 - 255 * Win7Alpha / 300, BackColor.Dark(0.2))) : G.DrawRoundedRect(P, InnerWindow_2, 1, True) : End Using
                Else
                    G.DrawImage(Noise7.Clone(Bounds, PixelFormat.Format32bppArgb), Rect)
                    Using P As New Pen(Color.FromArgb(If(Active, 130, 100), 25, 25, 25)) : G.DrawRectangle(P, Rect) : End Using
                    Using P As New Pen(Color.FromArgb(100, 255, 255, 255)) : G.DrawRectangle(P, inner) : End Using
                    'Using P As New Pen(Color.FromArgb(255 - 255 * Win7Alpha / 300, BackColor, 0.2))) : G.DrawRectangle(P, Rect) : End Using
                    Using P As New Pen(Color.FromArgb(255 - 255 * Win7Alpha / 300, BackColor.Light(0.2))) : G.DrawRectangle(P, InnerWindow_1) : End Using
                    Using br As New SolidBrush(Color.White) : G.FillRectangle(br, InnerWindow_1) : End Using
                    Using P As New Pen(Color.FromArgb(255 - 255 * Win7Alpha / 300, BackColor.Dark(0.2))) : G.DrawRectangle(P, InnerWindow_2) : End Using
                End If


                If Not ToolWindow Then
                    Dim closeBtn As Image
                    Dim CloseRect As New Rectangle

                    If Active Then
                        If Not WinVista Then
                            closeBtn = My.Resources.Win7_Close_Active
                        Else
                            closeBtn = My.Resources.Vista_Close_Active
                        End If
                    Else
                        If Not WinVista Then
                            closeBtn = My.Resources.Win7_Close_inactive
                        Else
                            closeBtn = My.Resources.Vista_Close_inactive
                        End If
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
                        CloseUpperAccent1 = Color.FromArgb(50, 189, 203, 218)
                        CloseLowerAccent2 = Color.FromArgb(50, 205, 219, 234)
                        CloseOuterBorder = Color.FromArgb(131, 142, 168)
                        CloseInnerBorder = Color.FromArgb(50, 209, 219, 229)
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

                    Using P As New Pen(CloseOuterBorder) : G.DrawRoundedRect(P, CloseRect, 1, True) : End Using
                    Using P As New Pen(CloseInnerBorder) : G.DrawRectangle(P, New Rectangle(CloseRect.X + 1, CloseRect.Y + 1, CloseRect.Width - 2, CloseRect.Height - 2)) : End Using

                End If

#End Region

            Else

#Region "Basic"
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
                    Using P As New Pen(Titlebar_OuterBorder) : G.DrawRoundedRect(P, Rect, Radius, True) : End Using
                    Using P As New Pen(Titlebar_InnerBorder) : G.DrawRoundedRect(P, New Rectangle(Rect.X + 1, Rect.Y + 1, Rect.Width - 2, Rect.Height - 2), Radius, True) : End Using
                    G.SetClip(New Rectangle(UpperPart.X + UpperPart.Width * 0.75, UpperPart.Y, UpperPart.Width * 0.75, UpperPart.Height))
                    Using P As New Pen(pth_line) : G.DrawRoundedRect(P, New Rectangle(Rect.X + 1, Rect.Y + 1, Rect.Width - 2, Rect.Height - 2), Radius, True) : End Using
                Else
                    G.FillRectangle(pth_back, Rect)
                    Using P As New Pen(Titlebar_OuterBorder) : G.DrawRectangle(P, Rect) : End Using
                    Using P As New Pen(Titlebar_InnerBorder) : G.DrawRectangle(P, New Rectangle(Rect.X + 1, Rect.Y + 1, Rect.Width - 2, Rect.Height - 2)) : End Using
                    G.SetClip(New Rectangle(UpperPart.X + UpperPart.Width * 0.75, UpperPart.Y, UpperPart.Width * 0.75, UpperPart.Height))
                    Using P As New Pen(pth_line) : G.DrawRectangle(P, New Rectangle(Rect.X + 1, Rect.Y + 1, Rect.Width - 2, Rect.Height - 2)) : End Using
                End If

                G.ResetClip()
                G.ExcludeClip(UpperPart)

                '### Render Rest of Window
                Using br As New SolidBrush(Titlebar_Backcolor2) : G.FillRectangle(br, Rect) : End Using
                Using P As New Pen(Titlebar_Turquoise) : G.DrawRectangle(P, New Rectangle(Rect.X + 1, Rect.Y + 1, Rect.Width - 2, Rect.Height - 2)) : End Using
                Using P As New Pen(OuterBorder) : G.DrawRectangle(P, Rect) : End Using
                Using P As New Pen(Titlebar_InnerBorder) : G.DrawLine(P, New Point(Rect.X + 1, Rect.Y), New Point(Rect.X + 1, Rect.Y + Rect.Height - 2)) : End Using
                If Active Then
                    G.DrawImage(My.Resources.Win7Sides, RectSide1)
                    G.DrawImage(My.Resources.Win7Sides, RectSide2)
                End If
                G.ResetClip()
                G.FillRectangle(Brushes.White, InnerWindow_1)
                Using P As New Pen(Color.FromArgb(186, 210, 234)) : G.DrawRectangle(P, InnerWindow_1) : End Using
                If Not WinVista Then
                    Using P As New Pen(Color.FromArgb(130, 135, 144)) : G.DrawRectangle(P, InnerWindow_2) : End Using
                End If

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

                Using P As New Pen(CloseOuterBorder) : G.DrawRoundedRect(P, CloseRect, 1, True) : End Using
                Using P As New Pen(CloseInnerBorder) : G.DrawRoundedRect(P, New Rectangle(CloseRect.X + 1, CloseRect.Y + 1, CloseRect.Width - 2, CloseRect.Height - 2), 1, True) : End Using

                IconRect = New Rectangle(InnerWindow_1.X + 4, CloseRect.Top + (CloseRect.Height - IconSize) / 2, IconSize, IconSize)

                LabelRect = New Rectangle(IconRect.Right + 3, CloseRect.Y, UpperPart.Width - (IconRect.Right + 4), CloseRect.Height)

                If ToolWindow Then LabelRect.X = IconRect.X
#End Region

            End If
#End Region

        ElseIf Preview = Preview_Enum.WXP Then
#Region "Windows XP"
            Dim sm As SmoothingMode = G.SmoothingMode
            G.SmoothingMode = SmoothingMode.HighSpeed

            TitlebarRect = New Rectangle(Rect.X, Rect.Y, Rect.Width, TitleTextH_Sum + _Metrics_BorderWidth + _Metrics_CaptionHeight + 5)

            Dim innerRect As New Rectangle(Rect.X, Rect.Y + TitlebarRect.Height - 1, Rect.Width - 2, Rect.Height - TitlebarRect.Height - 1)

            Using br As New SolidBrush(My.resVS.Colors.Btnface) : G.FillRectangle(br, innerRect) : End Using

            My.resVS.Draw(G, TitlebarRect, VisualStylesRes.Element.Titlebar, Active, ToolWindow)

            Dim LE As New Rectangle(Rect.X, Rect.Y + TitlebarRect.Height - 1, Math.Max(4, Metrics_BorderWidth), Rect.Height - TitlebarRect.Height - Math.Max(4, Metrics_BorderWidth) + 2)
            Dim RE As New Rectangle(Rect.X + Rect.Width - Math.Max(4, Metrics_BorderWidth) - 1, Rect.Y + TitlebarRect.Height - 1, Math.Max(4, Metrics_BorderWidth), Rect.Height - TitlebarRect.Height - Metrics_BorderWidth + 2)
            Dim BE As New Rectangle(Rect.X, Rect.Y + Rect.Height - Math.Max(4, Metrics_BorderWidth), Rect.Width - 1, Math.Max(4, Metrics_BorderWidth) + 1)
            Dim CloseBtn_W As Integer = TitleTextH_Sum + _Metrics_CaptionHeight - 4
            Dim CB As New Rectangle(Rect.X + Rect.Width - CloseBtn_W - RE.Width - 2, Rect.Y + TitlebarRect.Height - 4 - CloseBtn_W, CloseBtn_W, CloseBtn_W)

            If Not ToolWindow Then
                LabelRect = New Rectangle(Rect.X + LE.Width + 20, Rect.Y + TitlebarRect.Height - 4 - CloseBtn_W, Rect.Width - CloseBtn_W - LE.Width - RE.Width, CloseBtn_W)
            Else
                LabelRect = New Rectangle(Rect.X + LE.Width + 2, Rect.Y + TitlebarRect.Height - 4 - CloseBtn_W, Rect.Width - CloseBtn_W - LE.Width - RE.Width, CloseBtn_W)
            End If

            IconRect = New Rectangle(Rect.X + LE.Width + 2, Rect.Y + (TitlebarRect.Height - 14) / 2, 14, 14)

            My.resVS.Draw(G, TitlebarRect, VisualStylesRes.Element.Titlebar, Active, ToolWindow)
            My.resVS.Draw(G, LE, VisualStylesRes.Element.LeftEdge, Active, ToolWindow)
            My.resVS.Draw(G, RE, VisualStylesRes.Element.RightEdge, Active, ToolWindow)
            My.resVS.Draw(G, BE, VisualStylesRes.Element.BottomEdge, Active, ToolWindow)
            My.resVS.Draw(G, CB, VisualStylesRes.Element.CloseButton, Active, ToolWindow)

            G.SmoothingMode = sm
#End Region

        End If

        Dim ForeColorX As Color
        Dim closeImg As Bitmap

        If AccentColor_Enabled Then
            If Active Then
                ForeColorX = If(AccentColor_Active.IsDark, Color.White, Color.Black)
                closeImg = If(AccentColor_Active.IsDark, My.Resources.Win10x_Close_Dark, My.Resources.Win10x_Close_Light)
            Else
                ForeColorX = If(AccentColor_Inactive.IsDark, Color.FromArgb(115, 115, 115), Color.Black)
                closeImg = If(AccentColor_Inactive.IsDark, My.Resources.Win10x_Close_Dark, My.Resources.Win10x_Close_Light)
            End If

        Else
            If Active Then
                If Preview = Preview_Enum.W11 Then
                    ForeColorX = If(DarkMode, Color.White, Color.Black)
                    closeImg = If(DarkMode, My.Resources.Win10x_Close_Dark, My.Resources.Win10x_Close_Light)
                Else
                    ForeColorX = Color.Black
                    closeImg = My.Resources.Win10x_Close_Light
                End If
            Else
                ForeColorX = Color.FromArgb(115, 115, 115)
                closeImg = If(DarkMode, My.Resources.Win10x_Close_Dark, My.Resources.Win10x_Close_Light)
            End If
        End If

        If Not ToolWindow Then G.DrawImage(If(Active, My.Resources.SampleApp_Small_Active, My.Resources.SampleApp_Small_Inactive), IconRect)

        If Preview = Preview_Enum.W11 Or Preview = Preview_Enum.W10 Then
            Using br As New SolidBrush(ForeColorX) : G.DrawString(Text, Font, br, LabelRect, StringAligner(ContentAlignment.MiddleLeft)) : End Using

            If Not ToolWindow Then
                Dim r As New Rectangle(XRect.X + (XRect.Width - closeImg.Width) / 2, XRect.Y + (XRect.Height - closeImg.Height) / 2, closeImg.Width, closeImg.Height)
                G.DrawImage(closeImg, r)
            Else
                Dim XXRect As New Rectangle(Rect.X + Rect.Width - 2 - (TitlebarRect.Height - 12), Rect.Y + 6, TitlebarRect.Height - 12, TitlebarRect.Height - 12)

                Using br As New SolidBrush(Color.FromArgb(199, 80, 80)) : G.FillRectangle(br, XXRect) : End Using

                If XXRect.Width >= 12 Then
                    If XXRect.Width Mod 2 = 0 Then
                        XXRect.X += 1
                        XXRect.Y += 1
                    End If
                Else
                    XXRect.X += 1
                End If

                Using br As New SolidBrush(Color.White) : G.DrawString("r", New Font("Marlett", 6.35, FontStyle.Regular), br, New Rectangle(XXRect.X + 1, XXRect.Y + 1, XXRect.Width, XXRect.Height), StringAligner(ContentAlignment.MiddleCenter)) : End Using
            End If

        ElseIf Preview = Preview_Enum.W8 Then
            Using br As New SolidBrush(Color.Black) : G.DrawString(Text, Font, br, LabelRect8, StringAligner(ContentAlignment.MiddleCenter)) : End Using

        ElseIf Preview = Preview_Enum.W8Lite Then
            If Active Then
                Using br As New SolidBrush(My.CP.Win32.TitleText) : G.DrawString(Text, Font, br, LabelRect8, StringAligner(ContentAlignment.MiddleCenter)) : End Using
            Else
                Using br As New SolidBrush(My.CP.Win32.InactiveTitleText) : G.DrawString(Text, Font, br, LabelRect8, StringAligner(ContentAlignment.MiddleCenter)) : End Using
            End If

        ElseIf Preview = Preview_Enum.W7Aero Or Preview = Preview_Enum.W7Opaque Then
            Dim LabelRectModified As Rectangle = LabelRect
            LabelRectModified.X -= 2
            LabelRectModified.Y -= 1
            Dim alpha As Integer = If(Active, 120, 75)
            G.DrawGlowString(1, Text, Font, Color.Black, Color.FromArgb(alpha, Color.White), RectBK, LabelRectModified, StringAligner(ContentAlignment.MiddleLeft))

        ElseIf Preview = Preview_Enum.W7Basic Then
            Using br As New SolidBrush(If(Active, Color.Black, Color.FromArgb(76, 76, 76))) : G.DrawString(Text, Font, br, LabelRect, StringAligner(ContentAlignment.MiddleLeft)) : End Using

        ElseIf Preview = Preview_Enum.WXP Then
            Using br As New SolidBrush(Color.Black) : G.DrawString(Text, Font, br, New Rectangle(LabelRect.X + 1, LabelRect.Y, LabelRect.Width, LabelRect.Height), StringAligner(ContentAlignment.MiddleLeft)) : End Using
            Using br As New SolidBrush(Color.White) : G.DrawString(Text, Font, br, LabelRect, StringAligner(ContentAlignment.MiddleLeft)) : End Using

        End If

    End Sub

    Private Sub XenonWindow_HandleCreated(sender As Object, e As EventArgs) Handles Me.HandleCreated
        If Not DesignMode Then
            Try : AddHandler Parent.BackgroundImageChanged, AddressOf ProcessBack : Catch : End Try
            Try : AddHandler FontChanged, AddressOf AdjustPadding : Catch : End Try
        End If

        ProcessBack()
    End Sub

    Sub ProcessBack()
        Try
            If Preview = Preview_Enum.W11 Then
                If My.Wallpaper IsNot Nothing Then
                    Dim b As Bitmap = New Bitmap(My.Wallpaper.Clone(Bounds, My.Wallpaper.PixelFormat)).Blur(15)
                    If DarkMode Then
                        If b IsNot Nothing Then
                            Using ImgF As New ImageProcessor.ImageFactory
                                ImgF.Load(b)
                                ImgF.Saturation(15)
                                ImgF.Brightness(-10)
                                AdaptedBackBlurred = ImgF.Image.Clone
                            End Using
                        End If

                    Else
                        AdaptedBackBlurred = b
                    End If
                End If

            ElseIf Preview = Preview_Enum.W7Aero Then
                If My.Wallpaper IsNot Nothing Then AdaptedBackBlurred = New Bitmap(My.Wallpaper.Clone(Bounds, My.Wallpaper.PixelFormat)).Blur(1)
                Try : Noise7 = My.Resources.AeroGlass.Fade(Win7Noise / 100) : Catch : End Try

            ElseIf Preview = Preview_Enum.W7Opaque Then
                Try : Noise7 = My.Resources.AeroGlass.Fade(Win7Noise / 100) : Catch : End Try

            End If
        Catch
        End Try

    End Sub
End Class
Public Class XenonIcon : Inherits Panel

    Sub New()
        DoubleBuffered = True
        BackColor = Color.Transparent
        SetStyle(ControlStyles.SupportsTransparentBackColor, True)
        SetStyle(ControlStyles.AllPaintingInWmPaint, True)
        SetStyle(ControlStyles.UserPaint, True)
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
        G.TextRenderingHint = My.RenderingHint
        DoubleBuffered = True

        Dim IconRect As New Rectangle(0, 0, Width - 1, Height - 30)

        Dim LabelRect As New Rectangle(0, Height - 35, Width - 1, 30)
        Dim LabelRectShadow As New Rectangle(1, Height - 34, Width - 1, 30)

        If _IconSize < 16 Then _IconSize = 16
        If _IconSize > 256 Then _IconSize = 256

        Dim IconRectX As New Rectangle(IconRect.X + (IconRect.Width - _IconSize) / 2, IconRect.Y + (IconRect.Height - _IconSize) / 2, _IconSize, _IconSize)

        If Icon IsNot Nothing Then
            Dim ico As New Icon(Icon, _IconSize, _IconSize)
            G.DrawIcon(ico, IconRectX)
            ico.Dispose()
        End If

        If ColorGlow.A > 0 Then G.DrawString(Title, Me.Font, Brushes.Black, LabelRectShadow, StringAligner(ContentAlignment.MiddleCenter))
        'G.DrawString(Title, Font, Brushes.White, LabelRect, StringAligner(ContentAlignment.MiddleCenter))

        G.DrawGlowString(1, Title, Font, ColorText, ColorGlow, New Rectangle(0, 0, Width - 1, Height - 1), LabelRect, StringAligner(ContentAlignment.MiddleCenter))

        'G.DrawRectangle(Pens.Red, New Rectangle(0, 0, Width - 1, Height - 1))
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

    <Browsable(True)>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    <EditorBrowsable(EditorBrowsableState.Always)>
    <Editor(GetType(System.ComponentModel.Design.MultilineStringEditor), GetType(System.Drawing.Design.UITypeEditor))>
    <Bindable(True)>
    Public Overrides Property Text As String = ""
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

        Using br As New SolidBrush(c_back) : G.FillRoundedRect(br, middleRect) : End Using

        Circle = New Rectangle((Value / Maximum) * Shaft.Width, 0, Height - 1, Height - 1)

        With Thumb
            Using br As New SolidBrush(C) : G.FillRoundedRect(br, New Rectangle(.X + 1, middleRect.Y, Circle.Left + Circle.Width / 2, middleRect.Height)) : End Using
        End With

        Using br As New SolidBrush(BackColor) : G.FillRectangle(br, New Rectangle(-1, 0, 4, Height)) : End Using

        Using br As New SolidBrush(BackColor) : G.FillRectangle(br, New Rectangle(Width - 4, 0, 4, Height)) : End Using

        Using br As New SolidBrush(Style.Colors.Border) : G.FillEllipse(br, Circle) : End Using

        Dim smallC1 As New Rectangle(Circle.X + 5, Circle.Y + 5, Circle.Width - 10, Circle.Height - 10)
        Dim smallC2 As New Rectangle(Circle.X + 4, Circle.Y + 4, Circle.Width - 8, Circle.Height - 8)

        Using br As New SolidBrush(C) : G.FillEllipse(br, smallC1) : End Using
        Using br As New SolidBrush(Color.FromArgb(alpha, C)) : G.FillEllipse(br, smallC2) : End Using
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
    ReadOnly S2 As String = My.PATH_System32 & ">"
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
        G.TextRenderingHint = If(DesignMode, TextRenderingHint.ClearTypeGridFit, TextRenderingHint.SystemDefault)

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
                F = New Font(Font.Name, If(Font.SizeInPoints * 0.57 <= 0, 1, CSng(Font.Size * 0.57)), Font.Style)
            Else
                F = New Font(Font.Name, If(Font.SizeInPoints * 0.57 <= 0, 1, CSng(Font.Size * 0.57)), Font.Style)
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
            Using br As New SolidBrush(FC) : G.DrawString(S, F, br, RectCMD.Location) : End Using

            Using br As New SolidBrush(PCB) : G.FillRectangle(br, RectMiddle) : End Using
            Using P As New Pen(PCF) : G.DrawRectangle(P, RectMiddleBorder) : End Using

            Using br As New SolidBrush(PCF) : G.DrawString("This is a pop-up", F, br, RectMiddleBorder, StringAligner(ContentAlignment.MiddleCenter)) : End Using

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

            Using br As New SolidBrush(PCB) : G.FillRectangle(br, RectMiddle) : End Using
            Using P As New Pen(PCF) : G.DrawRectangle(P, RectMiddleBorder) : End Using


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
        G.TextRenderingHint = If(DesignMode, TextRenderingHint.ClearTypeGridFit, TextRenderingHint.SystemDefault)

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
        Dim s3 As String = My.PATH_System32 & ">"

        Dim s1X As SizeF = s1.Measure(Font) + New SizeF(5, 0)
        Dim s2X As SizeF = s2.Measure(Font) + New SizeF(2, 0)
        Dim s3X As SizeF = s3.Measure(Font) + New SizeF(2, 0)
        Dim Rect_ConsoleText0 As New Rectangle(8, Rect_Titlebar.Bottom + 8, s1X.Width, s1X.Height)
        Dim Rect_ConsoleText1 As New Rectangle(8, Rect_ConsoleText0.Bottom + 3, s2X.Width, s2X.Height)
        Dim Rect_ConsoleText2 As New Rectangle(8, Rect_ConsoleText1.Bottom + Rect_ConsoleText1.Height + 3, s3X.Width, s3X.Height)

        Dim Rect_ConsoleCursor As New Rectangle(Rect_ConsoleText2.Right, Rect_ConsoleText2.Y, 50, Rect_ConsoleText2.Height - 1)

        If UseAcrylic Then
            G.DrawRoundImage(adaptedBackBlurred, Rect)
            G.FillRoundedRect(Noise, Rect)
            Using br As New SolidBrush(Color.FromArgb((_Opacity / 100) * 255, Color_Background)) : G.FillRoundedRect(br, Rect) : End Using
            If BackImage IsNot Nothing Then G.DrawRoundImage(img, Rect)
        Else
            G.DrawRoundImage(adaptedBack, Rect)
            Using br As New SolidBrush(Color.FromArgb((_Opacity / 100) * 255, Color_Background)) : G.FillRoundedRect(br, Rect) : End Using
            If BackImage IsNot Nothing Then G.DrawRoundImage(img, Rect)
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
                    Using br As New SolidBrush(Color.FromArgb(If(IsFocused, 100, 255), 35, 35, 35)) : FillSemiRect(G, br, Rect_Titlebar) : End Using
                Else
                    Using br As New SolidBrush(Color.FromArgb(If(IsFocused, 100, 255), 35, 35, 35)) : G.FillRectangle(br, Rect_Titlebar) : End Using
                End If
            Else
                If GetRoundedCorners() Then
                    Using br As New SolidBrush(Color.FromArgb(If(IsFocused, 180, 255), 232, 232, 232)) : FillSemiRect(G, br, Rect_Titlebar) : End Using
                Else
                    Using br As New SolidBrush(Color.FromArgb(If(IsFocused, 180, 255), 232, 232, 232)) : G.FillRectangle(br, Rect_Titlebar) : End Using
                End If
            End If

        End If

        If Not UseAcrylicOnTitlebar Then
            If GetRoundedCorners() Then
                Using br As New SolidBrush(If(IsFocused, Color_Titlebar, Color_Titlebar_Unfocused)) : FillSemiRect(G, br, Rect_Titlebar) : End Using
            Else
                Using br As New SolidBrush(If(IsFocused, Color_Titlebar, Color_Titlebar_Unfocused)) : G.FillRectangle(br, Rect_Titlebar) : End Using
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
            Using br As New SolidBrush(TabFocusedFinalColor) : G.FillPath(br, RR(Rect_Tab0, Radius)) : End Using
            G.SmoothingMode = SmoothingMode.AntiAlias
            Using P As New Pen(TabFocusedFinalColor) : G.DrawPath(P, RRNoLine(Rect_Tab0, Radius)) : End Using
            G.SmoothingMode = SmoothingMode.Default

            If Not UseAcrylicOnTitlebar Then
                Using br As New SolidBrush(Color_TabUnFocused) : G.FillPath(br, RR(Rect_Tab1, Radius)) : End Using
            Else
                If Color_TabUnFocused <> Color_Titlebar Then
                    Using br As New SolidBrush(Color_TabUnFocused) : G.FillPath(br, RR(Rect_Tab1, Radius)) : End Using
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
            Using br As New SolidBrush(FC0) : G.DrawString(TabIconButItIsString, fx, br, IconRect0, StringAligner(ContentAlignment.TopCenter)) : End Using
        End If

        Using br As New SolidBrush(FC1) : G.DrawString(TabIconButItIsString, fx, br, IconRect1, StringAligner(ContentAlignment.TopCenter)) : End Using

        TextRenderer.DrawText(G, TabTitle, New Font("Segoe UI", 8, FontStyle.Bold), RectText_Tab0, FC0, Color.Transparent, TextFormatFlags.WordEllipsis)
        TextRenderer.DrawText(G, "Other Terminal", New Font("Segoe UI", 8, FontStyle.Regular), RectText_Tab1, FC1, Color.Transparent, TextFormatFlags.WordEllipsis)


        Using br As New SolidBrush(FC0) : G.DrawString("", New Font("Segoe MDL2 Assets", 6, FontStyle.Regular), br, RectClose_Tab0, StringAligner(ContentAlignment.MiddleCenter)) : End Using
        Using br As New SolidBrush(FC1) : G.DrawString("", New Font("Segoe MDL2 Assets", 6, FontStyle.Regular), br, RectClose_Tab1, StringAligner(ContentAlignment.MiddleCenter)) : End Using

        Using br As New SolidBrush(Color_Foreground) : G.DrawString(s1, Font, br, Rect_ConsoleText0, StringAligner(ContentAlignment.TopLeft)) : End Using

        Using br As New SolidBrush(Color.FromArgb(125, Color_Selection)) : G.FillRectangle(br, Rect_ConsoleText1) : End Using

        Using br As New SolidBrush(Color.FromArgb(255 - 125, Color_Foreground)) : G.DrawString(s2, Font, br, Rect_ConsoleText1, StringAligner(ContentAlignment.TopLeft)) : End Using

        Using br As New SolidBrush(Color_Foreground) : G.DrawString(s3, Font, br, Rect_ConsoleText2, StringAligner(ContentAlignment.TopLeft)) : End Using

        If tk And IsFocused Then
            G.SmoothingMode = SmoothingMode.HighSpeed

            Using br As New SolidBrush(Color_Cursor)

                Select Case CursorType
                    Case CursorShape_Enum.bar
                        G.FillRectangle(br, New Rectangle(Rect_ConsoleCursor.X, Rect_ConsoleCursor.Y, 1, Rect_ConsoleCursor.Height))

                    Case CursorShape_Enum.doubleUnderscore
                        G.FillRectangle(br, New Rectangle(Rect_ConsoleCursor.X, Rect_ConsoleCursor.Bottom, Rect_ConsoleCursor.Height * 0.5, 1))
                        G.FillRectangle(br, New Rectangle(Rect_ConsoleCursor.X, Rect_ConsoleCursor.Bottom - 3, Rect_ConsoleCursor.Height * 0.5, 1))

                    Case CursorShape_Enum.emptyBox
                        Using p As New Pen(Color_Cursor) : G.DrawRectangle(p, New Rectangle(Rect_ConsoleCursor.X, Rect_ConsoleCursor.Y, Rect_ConsoleCursor.Height * 0.5, Rect_ConsoleCursor.Height)) : End Using

                    Case CursorShape_Enum.filledBox
                        G.FillRectangle(br, New Rectangle(Rect_ConsoleCursor.X, Rect_ConsoleCursor.Y, Rect_ConsoleCursor.Height * 0.5, Rect_ConsoleCursor.Height))

                    Case CursorShape_Enum.underscore
                        G.FillRectangle(br, New Rectangle(Rect_ConsoleCursor.X, Rect_ConsoleCursor.Bottom - 1, Rect_ConsoleCursor.Height * 0.5, 1))

                    Case CursorShape_Enum.vintage
                        G.FillRectangle(br, New Rectangle(Rect_ConsoleCursor.X, Rect_ConsoleCursor.Bottom - (CursorHeight / 100) * (Rect_ConsoleCursor.Height), Rect_ConsoleCursor.Height * 0.5, (CursorHeight / 100) * (Rect_ConsoleCursor.Height)))

                    Case Else
                        G.FillRectangle(br, New Rectangle(Rect_ConsoleCursor.X, Rect_ConsoleCursor.Y, 1, Rect_ConsoleCursor.Height))

                End Select
            End Using

            G.SmoothingMode = SmoothingMode.AntiAlias
        End If

        Using P As New Pen(Color.FromArgb(45, 45, 45)) : G.DrawRoundedRect(P, Rect) : End Using
    End Sub

    Dim tk As Boolean = False

    Private Sub Tm_Tick(sender As Object, e As EventArgs) Handles Tm.Tick
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
            Tm.Enabled = True
            Tm.Start()

            Try : AddHandler SizeChanged, AddressOf ProcessBack : Catch : End Try
            Try : AddHandler OpacityBackImageChanged, AddressOf UpdateOpacityBackImageChanged : Catch : End Try

            ProcessBack()
            'UpdateOpacityBackImageChanged()
        Else
            Tm.Enabled = False
            Tm.Stop()
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
        adaptedBack = My.Wallpaper
        adaptedBackBlurred = New Bitmap(adaptedBack).Blur(13)
    End Sub

    Sub NoiseBack()
        Noise = New TextureBrush(My.Resources.GaussianBlur.Fade(0.5))
    End Sub

End Class

<DefaultEvent("Scroll")>
Public Class XenonColorBar
    Inherits Control

    Event Scroll(ByVal sender As Object)

#Region "Properties"
    Private _Minimum As Integer
    Property Minimum() As Integer
        Get
            Return _Minimum
        End Get
        Set(ByVal value As Integer)
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

    <Browsable(True)>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    <EditorBrowsable(EditorBrowsableState.Always)>
    <Editor(GetType(System.ComponentModel.Design.MultilineStringEditor), GetType(System.Drawing.Design.UITypeEditor))>
    <Bindable(True)>
    Public Overrides Property Text As String = ""
#End Region

    Enum ModesList
        Hue
        Saturation
        Light
    End Enum

    Public Property Mode As ModesList = ModesList.Hue
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

    Private _AccentColor As Color = Style.Colors.Core
    Public Property AccentColor As Color
        Get
            Return _AccentColor
        End Get
        Set(value As Color)
            _AccentColor = value
            Refresh()
        End Set
    End Property


    Private _H As Integer = 0
    Public Property H As Integer
        Get
            Return _H
        End Get
        Set(value As Integer)
            _H = value
            Refresh()
        End Set
    End Property


    Private _S As Single = 1
    Public Property S As Single
        Get
            Return _S
        End Get
        Set(value As Single)
            _S = value
            Refresh()
        End Set
    End Property


    Private _L As Integer = 0.5
    Public Property L As Single
        Get
            Return _L
        End Get
        Set(value As Single)
            _L = value
            Refresh()
        End Set
    End Property

    Private ReadOnly Color1 As Color = Color.FromArgb(255, 23, 0)
    Private ReadOnly Color2 As Color = Color.FromArgb(253, 253, 0)
    Private ReadOnly Color3 As Color = Color.FromArgb(58, 255, 0)
    Private ReadOnly Color4 As Color = Color.FromArgb(0, 255, 240)
    Private ReadOnly Color5 As Color = Color.FromArgb(0, 17, 255)
    Private ReadOnly Color6 As Color = Color.FromArgb(255, 0, 212)
    Private ReadOnly Color7 As Color = Color.FromArgb(255, 0, 23)

    ReadOnly cb_H As New ColorBlend() With {.Positions = {0, 1 / 6.0F, 2 / 6.0F, 3 / 6.0F, 4 / 6.0F, 5 / 6.0F, 1},
                    .Colors = {Color1, Color2, Color3, Color4, Color5, Color6, Color7}}

    Dim cb_L As New ColorBlend() With {.Positions = {0, 1 / 2.0F, 1}, .Colors = {Color.Black, _AccentColor, Color.White}}

    Protected Overrides Sub OnPaint(e As System.Windows.Forms.PaintEventArgs)
        Dim G As Graphics = e.Graphics
        G.SmoothingMode = SmoothingMode.HighQuality
        G.TextRenderingHint = TextRenderingHint.AntiAliasGridFit

        G.Clear(BackColor)
        Dim Dark As Boolean = GetDarkMode()
        Dim c_back As Color = If(Dark, Color.FromArgb(60, 60, 60), Color.FromArgb(210, 210, 210))
        Dim c_btn As Color = If(Dark, Color.FromArgb(165, 165, 165), Color.FromArgb(100, 100, 100))
        Dim C As Color

        Dim middleRect As New Rectangle(0, (Height - (Height * 0.25)) / 2, Width - 1, Height * 0.25)

        Dim back As LinearGradientBrush

        Select Case Mode
            Case ModesList.Hue
                back = New LinearGradientBrush(middleRect, Color.Black, Color.Black, 0, False) With {.InterpolationColors = cb_H}
                Dim HSL_ As HSL_Structure = Color.FromArgb(0, 255, 240).ToHSL()
                HSL_.H = (Value / Maximum) * 359
                C = HSL_.ToRGB

            Case ModesList.Saturation
                Dim HSL_x1 As HSL_Structure = _AccentColor.ToHSL()
                Dim HSL_x2 As HSL_Structure = _AccentColor.ToHSL()
                HSL_x1.S = 0
                HSL_x2.S = 1
                back = New LinearGradientBrush(middleRect, HSL_x1.ToRGB, HSL_x2.ToRGB, LinearGradientMode.Horizontal)
                Dim HSL_ As HSL_Structure = _AccentColor.ToHSL()
                HSL_.S = Value / Maximum
                C = HSL_.ToRGB

            Case ModesList.Light
                cb_L = New ColorBlend() With {.Positions = {0, 1 / 2.0F, 1}, .Colors = {Color.Black, _AccentColor, Color.White}}
                back = New LinearGradientBrush(middleRect, Color.Black, Color.Black, 0, False) With {.InterpolationColors = cb_L}
                Dim HSL_ As HSL_Structure = _AccentColor.ToHSL()
                HSL_.L = Value / Maximum
                C = HSL_.ToRGB

            Case Else
                C = If(Dark, Color.FromArgb(60, 60, 60), Color.FromArgb(210, 210, 210))
                back = New LinearGradientBrush(middleRect, C, C, GradientMode.Horizontal)

        End Select

        G.FillRoundedRect(back, middleRect)

        Circle = New Rectangle((Value / Maximum) * Shaft.Width, 0, Height - 1, Height - 1)

        Using br As New SolidBrush(BackColor) : G.FillRectangle(br, New Rectangle(-1, 0, 4, Height)) : End Using

        Using br As New SolidBrush(BackColor) : G.FillRectangle(br, New Rectangle(Width - 4, 0, 4, Height)) : End Using

        Using br As New SolidBrush(Style.Colors.Border) : G.FillEllipse(br, Circle) : End Using

        Dim smallC1 As New Rectangle(Circle.X + 5, Circle.Y + 5, Circle.Width - 10, Circle.Height - 10)
        Dim smallC2 As New Rectangle(Circle.X + 4, Circle.Y + 4, Circle.Width - 8, Circle.Height - 8)

        Using br As New SolidBrush(C) : G.FillEllipse(br, smallC1) : End Using
        Using br As New SolidBrush(Color.FromArgb(alpha, C)) : G.FillEllipse(br, smallC2) : End Using
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
            Value = Math.Min(Math.Max((e.X / Width) * Maximum, _Minimum), _Maximum)
            InvalidatePosition()
        End If

        Tmr.Enabled = True
        Tmr.Start()
    End Sub

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
Public Class StripRenderer
    Inherits ToolStripSystemRenderer

    Public Sub New()
    End Sub

    Protected Overrides Sub OnRenderToolStripBorder(ByVal e As ToolStripRenderEventArgs)
    End Sub
End Class
Public Class XenonLabel : Inherits Label
    Private _textHdc As IntPtr = IntPtr.Zero
    Private _dibSectionRef As IntPtr

    Public Property DrawOnGlass As Boolean = False

    Protected Function ReturnFormatFlags(Optional Text As String = "") As TextFormatFlags
        Dim format As TextFormatFlags = TextFormatFlags.Default

        If TextAlign = ContentAlignment.BottomCenter Then
            format = format Or TextFormatFlags.HorizontalCenter
            format = format Or TextFormatFlags.Bottom

        ElseIf TextAlign = ContentAlignment.BottomRight Then
            format = format Or TextFormatFlags.Right
            format = format Or TextFormatFlags.Bottom

        ElseIf TextAlign = ContentAlignment.BottomLeft Then
            format = format Or TextFormatFlags.Left
            format = format Or TextFormatFlags.Bottom

        ElseIf TextAlign = ContentAlignment.MiddleCenter Then
            format = format Or TextFormatFlags.HorizontalCenter
            format = format Or TextFormatFlags.VerticalCenter

        ElseIf TextAlign = ContentAlignment.MiddleRight Then
            format = format Or TextFormatFlags.Right
            format = format Or TextFormatFlags.VerticalCenter

        ElseIf TextAlign = ContentAlignment.MiddleLeft Then
            format = format Or TextFormatFlags.Left
            format = format Or TextFormatFlags.VerticalCenter

        ElseIf TextAlign = ContentAlignment.TopCenter Then
            format = format Or TextFormatFlags.HorizontalCenter
            format = format Or TextFormatFlags.Top

        ElseIf TextAlign = ContentAlignment.TopRight Then
            format = format Or TextFormatFlags.Right
            format = format Or TextFormatFlags.Top

        ElseIf TextAlign = ContentAlignment.TopLeft Then
            format = format Or TextFormatFlags.Left
            format = format Or TextFormatFlags.Top
        End If

        If Not Text.Contains(vbCrLf) Then format = format Or TextFormatFlags.SingleLine
        If AutoEllipsis Then format = format Or TextFormatFlags.EndEllipsis
        If RightToLeft = RightToLeft.Yes Then format = format Or TextFormatFlags.RightToLeft

        Return format
    End Function

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        e.Graphics.SmoothingMode = SmoothingMode.HighQuality
        e.Graphics.TextRenderingHint = My.RenderingHint
        Using br As New SolidBrush(BackColor) : e.Graphics.FillRectangle(br, New Rectangle(0, 0, Width, Height)) : End Using

        Try
            If DesignMode OrElse Not DrawOnGlass Then
                Using br As New SolidBrush(ForeColor) : e.Graphics.DrawString(Text, Font, br, New Rectangle(0, 0, Width, Height), StringAligner(TextAlign)) : End Using
            Else
                Dim outputHdc = e.Graphics.GetHdc()
                Dim sourceHdc = PrepareHdc(outputHdc, Width, Height)
                NativeMethods.GDI32.BitBlt(outputHdc, 1, 1, Width, Height, sourceHdc, 0, 0, NativeMethods.GDI32.BitBltOp.SRCCOPY)
                e.Graphics.ReleaseHdc(outputHdc)
            End If
        Catch
            Using br As New SolidBrush(ForeColor) : e.Graphics.DrawString(Text, Font, br, New Rectangle(0, 0, Width, Height), StringAligner(TextAlign)) : End Using
        End Try

    End Sub

    Private Function PrepareHdc(outputHdc As IntPtr, width As Integer, height As Integer) As IntPtr
        If _textHdc <> IntPtr.Zero Then
            NativeMethods.GDI32.DeleteObject(_dibSectionRef)
            NativeMethods.GDI32.DeleteDC(_textHdc)
        End If
        _textHdc = NativeMethods.GDI32.CreateCompatibleDC(outputHdc)

        Dim bmp_info As New NativeMethods.GDI32.BitmapInfo() With {
                .biSize = Runtime.InteropServices.Marshal.SizeOf(GetType(NativeMethods.GDI32.BitmapInfo)),
                .biWidth = width,
                .biHeight = -height, 'DIB use top-down ref system, so use negative height
                .biPlanes = 1,
                .biBitCount = 32,
                .biCompression = 0
            }
        _dibSectionRef = NativeMethods.GDI32.CreateDIBSection(outputHdc, bmp_info, 0, 0, IntPtr.Zero, 0)
        NativeMethods.GDI32.SelectObject(_textHdc, _dibSectionRef)

        Dim hFont As IntPtr = Font.ToHfont()
        NativeMethods.GDI32.SelectObject(_textHdc, hFont)

        Dim Options As New NativeMethods.UxTheme.DttOpts With {
                .dwSize = Runtime.InteropServices.Marshal.SizeOf(GetType(NativeMethods.UxTheme.DttOpts)),
                .dwFlags = NativeMethods.UxTheme.DttOptsFlags.DTT_COMPOSITED Or NativeMethods.UxTheme.DttOptsFlags.DTT_TEXTCOLOR,
                .crText = ForeColor.ToArgb}

        'Glow
        Options.dwFlags = Options.dwFlags Or NativeMethods.UxTheme.DttOptsFlags.DTT_GLOWSIZE
        Options.iGlowSize = 10

        'Draw
        Try
            Dim Rectangle As New NativeMethods.UxTheme.Rect(Padding.Left, Padding.Top, width - Padding.Right, height - Padding.Bottom)  'Set full bounds with padding
            Dim ActiveTitlebarRenderer As New VisualStyles.VisualStyleRenderer(VisualStyles.VisualStyleElement.Window.Caption.Active)
            NativeMethods.UxTheme.DrawThemeTextEx(ActiveTitlebarRenderer.Handle, _textHdc, 0, 0, Text, -1, ReturnFormatFlags, Rectangle, Options)
        Catch
        End Try

        NativeMethods.GDI32.DeleteObject(hFont)

        Return _textHdc
    End Function
End Class
Public Class XenonToolStripStatusLabel : Inherits ToolStripStatusLabel
    Protected Overrides Sub OnPaint(e As System.Windows.Forms.PaintEventArgs)
        e.Graphics.TextRenderingHint = My.RenderingHint

        Using br As New SolidBrush(BackColor) : e.Graphics.FillRectangle(br, New Rectangle(0, 0, Width, Height)) : End Using
        Using br As New SolidBrush(ForeColor) : e.Graphics.DrawString(Text, Font, br, New Rectangle(0, 0, Width, Height), StringAligner(TextAlign)) : End Using
    End Sub
End Class
Class XenonToolStripMenuItem : Inherits System.Windows.Forms.ToolStripMenuItem
    Protected Overrides Sub OnPaint(e As System.Windows.Forms.PaintEventArgs)
        MyBase.OnPaint(e)
        e.Graphics.TextRenderingHint = My.RenderingHint
        e.Graphics.Clear(BackColor)
        Using br As New SolidBrush(ForeColor) : e.Graphics.DrawString(Text, Font, br, New Rectangle(0, 0, Width, Height), StringAligner(TextAlign)) : End Using
    End Sub
End Class
#End Region