﻿Imports System.ComponentModel
Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.Drawing.Text
Imports System.Runtime.InteropServices
Imports WinPaletter.XenonCore

#Region "Helpers"
Module XenonModule
    Sub New()
        TextBitmap = New Bitmap(1, 1)
        TextGraphics = Graphics.FromImage(TextBitmap)
    End Sub

    Private ReadOnly TextBitmap As Bitmap
    Private ReadOnly TextGraphics As Graphics

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

    Friend Function MeasureString(text As String, font As Font) As SizeF
        Try : Return TextGraphics.MeasureString(text, font) : Catch : End Try
    End Function

    Friend Function GetParentColor(ByVal [Control] As Control, Optional ByVal Accept_Transparent As Boolean = False) As Color
        If Accept_Transparent Then
            Return [Control].Parent.BackColor
        Else
            Try
                If Control.Parent Is Nothing Then Exit Function
                If Control.Parent.BackColor.A = 255 Then
                    Return Color.FromArgb(255, [Control].Parent.BackColor)
                Else
                    Try
                        Dim c1 As Color = [Control].Parent.BackColor
                        Dim c2 As Color
                        If TypeOf [Control].Parent.Parent IsNot Form Then
                            c2 = [Control].Parent.Parent.BackColor
                        Else
                            c2 = [Control].Parent.FindForm.BackColor
                        End If
                        Dim amount As Double = c1.A / 255
                        Dim r As Byte = CByte(((c1.R * amount) + c2.R * (1 - amount)))
                        Dim g As Byte = CByte(((c1.G * amount) + c2.G * (1 - amount)))
                        Dim b As Byte = CByte(((c1.B * amount) + c2.B * (1 - amount)))
                        Return Color.FromArgb(r, g, b)
                    Catch
                        Return [Control].Parent.BackColor
                    End Try
                End If
            Catch
            End Try
        End If
    End Function

    Friend Function GetRoundedCorners() As Boolean
        Try
            If System.ComponentModel.LicenseManager.UsageMode = System.ComponentModel.LicenseUsageMode.Designtime Then
                Return False
            Else
                If My.WindowsVersions.W11 Or My.WindowsVersions.W7 Then
                    Return True
                ElseIf My.WindowsVersions.W10 Or My.WindowsVersions.W8 Then
                    Return False
                Else
                    Return False
                End If
            End If
        Catch
            Return False
        End Try
    End Function

    Public Function ColorTint(ByVal sourceBitmap As Bitmap, [Color] As Color) As Bitmap
        Dim sourceData As BitmapData = sourceBitmap.LockBits(New Rectangle(0, 0, sourceBitmap.Width, sourceBitmap.Height), ImageLockMode.[ReadOnly], PixelFormat.Format32bppArgb)
        Dim pixelBuffer As Byte() = New Byte(sourceData.Stride * sourceData.Height - 1) {}
        Marshal.Copy(sourceData.Scan0, pixelBuffer, 0, pixelBuffer.Length)
        sourceBitmap.UnlockBits(sourceData)
        Dim blue As Single = 0
        Dim green As Single = 0
        Dim red As Single = 0
        Dim k As Integer = 0

        While k + 4 < pixelBuffer.Length
            blue = pixelBuffer(k) + (255 - pixelBuffer(k)) * [Color].B
            green = pixelBuffer(k + 1) + (255 - pixelBuffer(k + 1)) * [Color].G
            red = pixelBuffer(k + 2) + (255 - pixelBuffer(k + 2)) * [Color].R

            If blue > 255 Then
                blue = 255
            End If

            If green > 255 Then
                green = 255
            End If

            If red > 255 Then
                red = 255
            End If

            pixelBuffer(k) = CByte(blue)
            pixelBuffer(k + 1) = CByte(green)
            pixelBuffer(k + 2) = CByte(red)
            k += 4
        End While

        Dim resultBitmap As Bitmap = New Bitmap(sourceBitmap.Width, sourceBitmap.Height)
        Dim resultData As BitmapData = resultBitmap.LockBits(New Rectangle(0, 0, resultBitmap.Width, resultBitmap.Height), ImageLockMode.[WriteOnly], PixelFormat.Format32bppArgb)
        Marshal.Copy(pixelBuffer, 0, resultData.Scan0, pixelBuffer.Length)
        resultBitmap.UnlockBits(resultData)
        Return resultBitmap
    End Function
    Public Function BlendColor(ByVal color As Color, ByVal backColor As Color, ByVal amount As Double) As Color
        If amount > 100 Then amount = 100
        If amount < 0 Then amount = 0

        Dim a As Byte = CByte((color.A * amount / 100 + backColor.A * (amount / 100)) / 2)
        Dim r As Byte = CByte((color.R * amount / 100 + backColor.R * (amount / 100)) / 2)
        Dim g As Byte = CByte((color.G * amount / 100 + backColor.G * (amount / 100)) / 2)
        Dim b As Byte = CByte((color.B * amount / 100 + backColor.B * (amount / 100)) / 2)
        Return Color.FromArgb(a, r, g, b)
    End Function
    Public Function Grayscale(ByVal original As Bitmap) As Bitmap
        Dim newBitmap As Bitmap = New Bitmap(original.Width, original.Height)

        Using g As Graphics = Graphics.FromImage(newBitmap)
            Dim colorMatrix As ColorMatrix = New ColorMatrix(New Single()() {New Single() {0.3F, 0.3F, 0.3F, 0, 0}, New Single() {0.59F, 0.59F, 0.59F, 0, 0}, New Single() {0.11F, 0.11F, 0.11F, 0, 0}, New Single() {0, 0, 0, 1, 0}, New Single() {0, 0, 0, 0, 1}})

            Using attributes As ImageAttributes = New ImageAttributes()
                attributes.SetColorMatrix(colorMatrix)
                g.DrawImage(original, New Rectangle(0, 0, original.Width, original.Height), 0, 0, original.Width, original.Height, GraphicsUnit.Pixel, attributes)
            End Using
        End Using

        Return newBitmap
    End Function
    Public Sub DrawAero(G As Graphics, Rect As Rectangle, BackgroundBlurred As Bitmap, Color1 As Color, ColorBalance As Single, Color2 As Color, GlowBalance As Single, alpha As Single,
                       Radius As Integer)


        FillImg(G, BackgroundBlurred, Rect, Radius, True)

        FillRect(G, New SolidBrush(Color.FromArgb(alpha * 255, Color.Black)), Rect, Radius, True)

        FillRect(G, New SolidBrush(Color.FromArgb(alpha * (ColorBalance * 255), Color1)), Rect, Radius, True)

        Dim C1 As Color = Color.FromArgb(ColorBalance * 255, Color1)
        Dim C2 As Color = Color.FromArgb(GlowBalance * 255, Color2)

        'Dim bk1 As Bitmap = FadeBitmap(ColorTint(BackgroundBlurred, C1), ColorBalance * (1 - alpha))
        'bk1 = FadeBitmap(bk1, 0.5)
        'FillImg(G, bk1, Rect, Radius, True)

        'Dim bk2 As Bitmap = FadeBitmap(Gray-scale(BackgroundBlurred), alpha * GlowBalance)
        'bk2 = FadeBitmap(bk2, (1 - alpha) * 0.5)
        'FillImg(G, bk2, Rect, Radius, True)

        FillRect(G, New SolidBrush(Color.FromArgb(alpha * (GlowBalance * 100), Color2)), Rect, Radius, True)
        FillRect(G, New SolidBrush(Color.FromArgb(alpha * (GlowBalance * 150), BlendColor(C1, C2, 100))), Rect, Radius, True)

    End Sub
#Region "Rounded Rectangle System"
    Public Sub FillRect(ByVal [Graphics] As Graphics, ByVal [Brush] As Brush, ByVal [Rectangle] As Rectangle, Optional ByVal [Radius] As Integer = -1, Optional ByVal ForcedRoundCorner As Boolean = False)
        Try
            If [Radius] = -1 Then [Radius] = 6

            If Graphics Is Nothing Then Throw New ArgumentNullException("graphics")
            [Graphics].SmoothingMode = SmoothingMode.AntiAlias

            If (GetRoundedCorners() Or ForcedRoundCorner) And [Radius] > 0 Then
                Using path As GraphicsPath = RoundedRectangle(Rectangle, Radius)
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
                Using path As GraphicsPath = RoundedRectangle(Rectangle, Radius)
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

    Public Function RoundedRectangle(ByVal r As Rectangle, ByVal radius As Integer) As GraphicsPath
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

    Public Sub DrawRect(ByVal [Graphics] As Graphics, ByVal [Pen] As Pen, ByVal [Rectangle] As Rectangle, Optional ByVal [Radius_willbe_x2] As Integer = -1, Optional ByVal ForcedRoundCorner As Boolean = False)
        Try
            If [Radius_willbe_x2] = -1 Then [Radius_willbe_x2] = 6
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

    Public Sub DrawRect_LikeW11(ByVal [Graphics] As Graphics, ByVal BorderColor As Color, ByVal [Rectangle] As Rectangle, Optional ByVal [Radius_willbe_x2] As Integer = -1, Optional ByVal ForcedRoundCorner As Boolean = False)
        Try
            If [Radius_willbe_x2] = -1 Then [Radius_willbe_x2] = 6
            [Radius_willbe_x2] *= 2
            [Graphics].SmoothingMode = SmoothingMode.AntiAlias

            Dim [Pen] As Pen
            Dim [Pen2] As Pen

            If GetDarkMode() Then
                [Pen] = New Pen(BorderColor)
                [Pen2] = New Pen(CCB(BorderColor, -0.3)) '-0.085)) 
            Else
                [Pen] = New Pen(BorderColor)
                [Pen2] = New Pen(CCB(BorderColor, -0.1))
            End If

            Dim G As New LinearGradientBrush([Rectangle], [Pen].Color, [Pen2].Color, LinearGradientMode.Vertical)
            Dim [PenG] As New Pen(G)

            If (GetRoundedCorners() Or ForcedRoundCorner) And [Radius_willbe_x2] > 0 Then
                [Graphics].DrawLine([Pen2], CInt([Rectangle].X + [Radius_willbe_x2] / 2), CInt([Rectangle].Y + [Rectangle].Height), CInt([Rectangle].X + [Rectangle].Width - [Radius_willbe_x2] / 2), CInt([Rectangle].Y + [Rectangle].Height))
                [Graphics].DrawArc([Pen2], [Rectangle].X, [Rectangle].Y + [Rectangle].Height - [Radius_willbe_x2], [Radius_willbe_x2], [Radius_willbe_x2], 90, 90)
                [Graphics].DrawArc([Pen2], [Rectangle].X + [Rectangle].Width - [Radius_willbe_x2], [Rectangle].Y + [Rectangle].Height - [Radius_willbe_x2], [Radius_willbe_x2], [Radius_willbe_x2], 0, 90)

                [Graphics].DrawLine([PenG], [Rectangle].X, CInt([Rectangle].Y + [Radius_willbe_x2] / 2), [Rectangle].X, CInt([Rectangle].Y + [Rectangle].Height - [Radius_willbe_x2] / 2.5))
                [Graphics].DrawLine([PenG], [Rectangle].X + [Rectangle].Width, CInt([Rectangle].Y + [Radius_willbe_x2] / 2), CInt([Rectangle].X + [Rectangle].Width), CInt([Rectangle].Y + [Rectangle].Height - [Radius_willbe_x2] / 2.5))

                [Graphics].DrawArc([Pen], [Rectangle].X, [Rectangle].Y, [Radius_willbe_x2], [Radius_willbe_x2], 180, 90)
                [Graphics].DrawArc([Pen], [Rectangle].X + [Rectangle].Width - [Radius_willbe_x2], [Rectangle].Y, [Radius_willbe_x2], [Radius_willbe_x2], 270, 90)
                [Graphics].DrawLine([Pen], CInt([Rectangle].X + [Radius_willbe_x2] / 2), [Rectangle].Y, CInt([Rectangle].X + [Rectangle].Width - [Radius_willbe_x2] / 2), [Rectangle].Y)
            Else
                [Graphics].DrawRectangle([Pen], [Rectangle])
            End If
        Catch
        End Try
    End Sub
#End Region
End Module
Public Class XenonColorPalette

    Public BaseColor As Color = Color.FromArgb(0, 81, 210)
    Public Color_Border As Color
    Public Color_Border_Checked_Hover As Color
    Public Color_Back As Color
    Public Color_Back_Checked As Color
    Public Color_Core As Color
    Public Color_Parent As Color
    Public Color_Parent_Hover As Color

    Public Color_Border_Checked As Color          ''''''''''''''''''''''''''''''''''''''
    Public Color_Back_Hover As Color              ''''''''''''''''''''''''''''''''''''''
    Public Color_Border_Hover As Color            ''''''''''''''''''''''''''''''''''''''

    Dim Dark As Boolean = True

    Function BizareColorInvertor([Color] As Color) As Color
        Return Color.FromArgb([Color].B, [Color].G, [Color].R)
    End Function

    Sub New(Optional ByVal [Control] As Control = Nothing)

        If [Control] Is Nothing Then Exit Sub

        'Try               'Try is a must because designer can't access My.Application._Settings in designer mode
        'If My.Application._Settings.Appearance_AdaptColors Then
        'Dim x As Byte() = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "AccentPalette", Nothing)
        'Dim Cx As Color = Color.FromArgb(255, x(12), x(13), x(14))
        'BaseColor = Cx 'If(GetDarkMode(), ControlPaint.Light(Cx, 0.2), ControlPaint.Light(Cx, 0.5))
        'End If
        'Catch
        'End Try

        Color_Parent = GetParentColor([Control])

        Dark = GetDarkMode()

        If Control.Enabled Then
            If Dark Then
                Color_Back_Checked = ControlPaint.Dark(BaseColor, 0.2)
                Color_Core = ControlPaint.LightLight(BaseColor)
                Color_Border_Checked_Hover = CCB(BaseColor, -0.2)
                Color_Border = CCB(Color_Parent, 0.08)
                Color_Back = CCB(Color_Parent, 0.04)
                Color_Parent_Hover = CCB(Color_Parent, 0.08)
            Else
                Color_Back_Checked = ControlPaint.LightLight(BaseColor)
                Color_Core = ControlPaint.Dark(BaseColor, 0.1)
                Color_Border_Checked_Hover = CCB(BaseColor, 0.2)
                Color_Border = CCB(Color_Parent, -0.3)
                Color_Back = CCB(Color_Parent, -0.07)
                Color_Parent_Hover = CCB(Color_Parent, -0.08)
            End If
        Else
            If Dark Then
                Color_Back_Checked = Color.FromArgb(80, 80, 80)
                Color_Core = Color.FromArgb(90, 90, 90)
                Color_Border_Checked_Hover = Color.FromArgb(80, 80, 80)
                Color_Border = Color.FromArgb(90, 90, 90)
                Color_Back = Color.FromArgb(80, 80, 80)
                Color_Parent_Hover = Color.FromArgb(90, 90, 90)
            Else
                Color_Back_Checked = Color.FromArgb(180, 180, 180)
                Color_Core = Color.FromArgb(190, 190, 190)
                Color_Border_Checked_Hover = Color.FromArgb(180, 180, 180)
                Color_Border = Color.FromArgb(190, 190, 190)
                Color_Back = Color.FromArgb(180, 180, 180)
                Color_Parent_Hover = Color.FromArgb(190, 190, 190)
            End If
        End If

        Control.Invalidate()
    End Sub
End Class
#End Region

#Region "Xenon UI"
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
Public Class XenonTabControl : Inherits Windows.Forms.TabControl
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

    Dim Noise As New TextureBrush(FadeBitmap(My.Resources.GaussianBlur, 0.5))
    Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
        Dim G As Graphics = e.Graphics
        G.SmoothingMode = SmoothingMode.AntiAlias
        G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit
        DoubleBuffered = True

        Dim SelectColor As Color
        Dim TextColor As Color
        Dim ParentColor As Color = GetParentColor(Me)
        Dim RTL As Boolean = If(RightToLeft = 1, True, False)
        Dim img As Image = Nothing

        G.Clear(ParentColor)

        For i = 0 To TabCount - 1
            Dim TabRect As Rectangle = GetTabRect(i)

            Try
                If Me.ImageList IsNot Nothing Then
                    Dim ls As ImageList = ImageList
                    img = ls.Images.Item(i)
                    SelectColor = GetAverageColor(img)
                Else
                    SelectColor = LineColor
                End If
            Catch
                SelectColor = LineColor
            End Try

            If Not GetDarkMode() Then SelectColor = ControlPaint.Light(SelectColor)

            If i = SelectedIndex Then
                FillRect(G, New SolidBrush(SelectColor), TabRect)
                FillRect(G, Noise, TabRect)
                TextColor = If(IsColorDark(SelectColor), Color.White, Color.Black)
            Else
                TextColor = If(IsColorDark(ParentColor), Color.White, Color.Black)
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
    Public ColorPalette As New XenonColorPalette()
    Dim CheckC As New Rectangle(5, 5, 13, 13)
    Dim MouseState As Integer = 0
    Dim WasMoving As Boolean = False

    Sub New()
        SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
        DoubleBuffered = True
        Size = New Size(50, 24)
    End Sub
    Public Property AccentColor As Color = Color.FromArgb(0, 123, 178)

    Public Property DarkLight_Toggler As Boolean = False

    ReadOnly DarkLight_TogglerSize As Integer = 14

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
        If Not DesignMode Then
            If Checked Then
                If Not DesignMode Then
                    Dim s As Integer = (Width - 19) * 0.5
                    For i As Integer = CheckC.Left To Width - 19 Step +5
                        CheckC.X = i + s
                        If _Shown Then
                            Threading.Thread.Sleep(15)
                            Refresh()
                        End If
                        If i + s >= Width - 19 Then Exit For
                        s -= 1
                        If s < 0 Then s = 0
                    Next
                End If
                CheckC.X = Width - 19
            Else
                If Not DesignMode Then
                    Dim s As Integer = 10
                    For i As Integer = CheckC.Left To 5 Step -5
                        CheckC.X = i - s
                        If _Shown Then
                            Threading.Thread.Sleep(15)
                            Refresh()
                        End If
                        If i - s <= 5 Then Exit For
                        s -= 1
                        If s < 0 Then s = 0
                    Next
                End If
                CheckC.X = 5
            End If
        Else
            If Checked Then
                CheckC.X = Width - 19
            Else
                CheckC.X = 5
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

    Protected Overrides Sub OnMouseClick(e As MouseEventArgs)
        Me.Checked = Not Me.Checked
        Me.Invalidate()
        MyBase.OnMouseClick(e)
    End Sub

    Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
        Me.OnPaintBackground(e)
        Dim G As Graphics = e.Graphics
        G.SmoothingMode = SmoothingMode.AntiAlias
        DoubleBuffered = True

        If Parent Is Nothing Then Exit Sub

        BackColor = ColorPalette.Color_Parent

        G.Clear(GetParentColor(Me))

        '################################################################################# Customizer
        Dim MainRect As New Rectangle(2, 2, Width - 5, Height - 5)
        Dim InnerRect As New Rectangle(3, 3, Width - 7, Height - 7)
        Dim BorderColor As Color

        If GetDarkMode() Then BorderColor = ControlPaint.Light(BackColor, 1.6) Else BorderColor = ControlPaint.DarkDark(BackColor)

        Dim CheckColor As Color
        If MouseState = 0 Then CheckColor = ColorPalette.BaseColor Else CheckColor = CCB(BackColor, If(GetDarkMode(), 0.3, -0.5))

        '#################################################################################
        Dim min As Integer = 5
        Dim max As Integer = Width - 24
        Dim val As Decimal = (CheckC.X - 5) / max
        If val < 0 Then val = 0
        If val > 1 Then val = 1

        Dim lgbChecked, lgbNonChecked, lgborderChecked, lgborderNonChecked As LinearGradientBrush

        lgbChecked = New LinearGradientBrush(MainRect, Color.FromArgb(255 * val, ColorPalette.BaseColor), Color.FromArgb(255 * val, 30, 30, 30), LinearGradientMode.ForwardDiagonal)
        lgborderChecked = New LinearGradientBrush(MainRect, Color.FromArgb(255 * val, ColorPalette.BaseColor), Color.FromArgb(255 * val, 30, 30, 30), LinearGradientMode.BackwardDiagonal)
        lgbNonChecked = New LinearGradientBrush(MainRect, Color.FromArgb(255 * (1 - val), 210, 210, 210), Color.FromArgb(255 * (1 - val), ColorPalette.BaseColor), LinearGradientMode.BackwardDiagonal)
        lgborderNonChecked = New LinearGradientBrush(MainRect, Color.FromArgb(255 * (1 - val), 210, 210, 210), Color.FromArgb(255 * (1 - val), ColorPalette.BaseColor), LinearGradientMode.ForwardDiagonal)


        If Not DarkLight_Toggler Then
            FillRect(e.Graphics, New SolidBrush(Color.FromArgb(255 * val, ColorPalette.BaseColor)), MainRect, 9, True)
            DrawRect(e.Graphics, New Pen(Color.FromArgb(255 * val, ColorPalette.BaseColor)), MainRect, 9, True)
            DrawRect(e.Graphics, New Pen(Color.FromArgb(255 * (1 - val), BorderColor)), MainRect, 9, True)

            If Checked Then
                G.FillEllipse(New SolidBrush(If(IsColorDark(ColorPalette.BaseColor), Color.White, Color.Black)), CheckC)
            Else
                G.FillEllipse(New SolidBrush(BorderColor), CheckC)
            End If

        Else
            FillRect(e.Graphics, lgbChecked, MainRect, 9, True)
            FillRect(e.Graphics, lgbNonChecked, MainRect, 9, True)

            If Checked Then
                G.DrawImage(FadeBitmap(If(IsColorDark(BorderColor), My.Resources.darkmode_dark, My.Resources.darkmode_light), val), CheckC)
                G.DrawImage(FadeBitmap(If(IsColorDark(BorderColor), My.Resources.lightmode_dark, My.Resources.lightmode_light), 1 - val), CheckC)
            Else
                G.DrawImage(FadeBitmap(If(IsColorDark(ColorPalette.BaseColor), My.Resources.darkmode_dark, My.Resources.darkmode_light), val), CheckC)
                G.DrawImage(FadeBitmap(If(IsColorDark(ColorPalette.BaseColor), My.Resources.lightmode_dark, My.Resources.lightmode_light), 1 - val), CheckC)
            End If

            DrawRect(e.Graphics, New Pen(lgborderChecked), MainRect, 9, True)
            DrawRect(e.Graphics, New Pen(lgborderNonChecked), MainRect, 9, True)
        End If
    End Sub

    Private Sub XenonToggle_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        Me.Height = 24
        If Width < 40 Then Width = 40

        If DarkLight_Toggler Then
            CheckC.Width = DarkLight_TogglerSize
            CheckC.Height = DarkLight_TogglerSize
        End If

        Refresh()
    End Sub

    Private Sub XenonToggle_HandleCreated(sender As Object, e As EventArgs) Handles Me.HandleCreated

        If Checked Then
            CheckC = New Rectangle(Width - 19, 5, 13, 13)
        Else
            CheckC = New Rectangle(5, 5, 13, 13)
        End If

        If DarkLight_Toggler Then
            CheckC.Width = DarkLight_TogglerSize
            CheckC.Height = DarkLight_TogglerSize
        End If

        ColorPalette = New XenonColorPalette(Me)

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
        ColorPalette = New XenonColorPalette(Me)
        Invalidate()
    End Sub

    Public Sub RefreshColorPalette()
        If _Shown Then
            ColorPalette = New XenonColorPalette(Me)
            Invalidate()
        End If
    End Sub

    Private Sub XenonToggle_MouseMove(sender As Object, e As MouseEventArgs) Handles Me.MouseMove
        If e.Button = MouseButtons.Left Then

            Dim i As Integer = e.X - 0.5 * CheckC.Width
            WasMoving = True
            MouseState = 1

            If i <= 5 Then
                CheckC.X = 5
            ElseIf i >= Width - 19 Then
                CheckC.X = Width - 19
            Else
                CheckC.X = i
            End If

            CheckC.Y = 5
            Refresh()
        End If
    End Sub

    Private Sub XenonToggle_MouseUp(sender As Object, e As MouseEventArgs) Handles Me.MouseUp
        MouseState = 0
        CheckC.Width = 13

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
        CheckC.Width = 16

        Refresh()
    End Sub

End Class

<DefaultEvent("CheckedChanged")>
Public Class XenonRadioButton
    Inherits Control
    Event CheckedChanged(sender As Object)
    Public ColorPalette As New XenonColorPalette()

    Sub New()
        SetStyle(DirectCast(139286, ControlStyles), True)
        SetStyle(ControlStyles.Selectable, False)
        DoubleBuffered = True
        AccentColor = Color.DodgerBlue
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

                If _Shown Then
                    Tmr2.Enabled = True
                    Tmr2.Start()
                    Invalidate()
                End If

            Catch
            End Try
        End Set
    End Property

    Private _Checked As Boolean
    Private _Shown As Boolean = False
#Region "Accent Color Property"
    Private AccentColorValue As Color = Color.DodgerBlue
    Public Event AccentColorChanged As PropertyChangedEventHandler

    Private Sub AccentColorNotifyPropertyChanged(ByVal info As String)
        RaiseEvent AccentColorChanged(Me, New PropertyChangedEventArgs(info))
    End Sub

    Public Property AccentColor() As Color
        Get
            Return AccentColorValue
        End Get

        Set(ByVal AccentColor As Color)
            If Not (AccentColor = AccentColorValue) Then
                AccentColorValue = AccentColor
                AccentColorNotifyPropertyChanged("ControlColorChanged")
            End If
        End Set
    End Property
#End Region
#End Region

#Region "Events"
    Enum MouseState
        None
        Over
        Down
    End Enum

    Public State As MouseState = MouseState.None

    Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
        Checked = True
        State = MouseState.Down
        _Shown = True
        Tmr.Enabled = True
        Tmr.Start()
        Invalidate()
        MyBase.OnMouseDown(e)
    End Sub

    Protected Overrides Sub OnMouseUp(e As MouseEventArgs)
        State = MouseState.Over
        _Shown = True
        Tmr.Enabled = True
        Tmr.Start()
        Invalidate()
    End Sub

    Private Sub XenonRadioButton_MouseEnter(sender As Object, e As EventArgs) Handles Me.MouseEnter
        State = MouseState.Over
        _Shown = True
        Tmr.Enabled = True
        Tmr.Start()
        Invalidate()
    End Sub

    Private Sub XenonCheckBox_MouseLeave(sender As Object, e As EventArgs) Handles Me.MouseLeave
        State = MouseState.None
        _Shown = True
        Tmr.Enabled = True
        Tmr.Start()
        Invalidate()
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
            alpha2 = If(Checked, 255, 0)
            ColorPalette = New XenonColorPalette(Me)
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

            If Checked Then
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

            If Not Checked Then
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

#Region "Drawing Variables"
    Private SZ1 As SizeF
    Private PT1 As PointF
#End Region

    Protected Overrides Sub OnPaint(e As System.Windows.Forms.PaintEventArgs)
        Try
            Dim G As Graphics = e.Graphics
            If Parent Is Nothing Then Exit Sub
            BackColor = Parent.BackColor
            Dim clr As Color = AccentColor

            G = e.Graphics
            G.SmoothingMode = SmoothingMode.AntiAlias
            G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit
            DoubleBuffered = True

            '################################################################################# Customizer
            SZ1 = G.MeasureString(Text, Font)

            Dim format As StringFormat = New StringFormat()
            Dim OuterCircle As New Rectangle(3, 4, Height - 8, Height - 8)
            Dim InnerCircle As New Rectangle(4, 5, Height - 10, Height - 10)
            Dim CheckCircle As New Rectangle(7, 8, Height - 16, Height - 16)
            Dim TextRect As New Rectangle(Height - 1, (CLng((Height - SZ1.Height)) \ 2) + 1, Width - OuterCircle.Width, Height - 1)
            Dim RTL As Boolean = If(RightToLeft = 1, True, False)

            If RTL Then
                format = New StringFormat(StringFormatFlags.DirectionRightToLeft)
                OuterCircle.X = Width - OuterCircle.X - OuterCircle.Width
                InnerCircle.X = Width - InnerCircle.X - InnerCircle.Width
                CheckCircle.X = Width - CheckCircle.X - CheckCircle.Width
                TextRect.Width -= OuterCircle.Width + 13
            End If

#Region "Colors System"
            Dim HoverCircle_Color As Color = Color.FromArgb(alpha2, ColorPalette.Color_Back_Checked)
            Dim HoverCheckedCircle_Color As Color = Color.FromArgb(alpha, ColorPalette.Color_Border_Checked_Hover)
            Dim CheckCircle_Color As Color = Color.FromArgb(alpha2, ColorPalette.Color_Core)
            Dim NonHoverCircle_Color As Color = ColorPalette.Color_Border
            Dim BackCircle_Color As Color = ColorPalette.Color_Back
            Dim ParentColor As Color = ColorPalette.Color_Parent
            Dim Selection_Color As Color = ColorPalette.Color_Parent_Hover
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
    Public ColorPalette As New XenonColorPalette()

    Sub New()
        SetStyle(DirectCast(139286, ControlStyles), True)
        SetStyle(ControlStyles.Selectable, False)
        DoubleBuffered = True
        AccentColor = Color.DodgerBlue
        Font = New Font("Segoe UI", 9)
        ForeColor = Color.White
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

                If _Shown Then
                    Invalidate()
                End If

            Catch
            End Try
        End Set
    End Property

    Public Property Image As Image
    Private _Checked As Boolean
    Private _Shown As Boolean = False
#Region "Accent Color Property"
    Private AccentColorValue As Color = Color.DodgerBlue
    Public Event AccentColorChanged As PropertyChangedEventHandler

    Private Sub AccentColorNotifyPropertyChanged(ByVal info As String)
        RaiseEvent AccentColorChanged(Me, New PropertyChangedEventArgs(info))
    End Sub

    Public Property AccentColor() As Color
        Get
            Return AccentColorValue
        End Get

        Set(ByVal AccentColor As Color)
            If Not (AccentColor = AccentColorValue) Then
                AccentColorValue = AccentColor
                AccentColorNotifyPropertyChanged("ControlColorChanged")
            End If
        End Set
    End Property
#End Region
#End Region

#Region "Events"
    Enum MouseState
        None
        Over
        Down
    End Enum

    Public State As MouseState = MouseState.None

    Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
        Checked = True
        State = MouseState.Down
        _Shown = True
        Tmr.Enabled = True
        Tmr.Start()
        Invalidate()
        MyBase.OnMouseDown(e)
    End Sub

    Protected Overrides Sub OnMouseUp(e As MouseEventArgs)
        State = MouseState.Over
        _Shown = True
        Tmr.Enabled = True
        Tmr.Start()
        Invalidate()
    End Sub

    Private Sub XenonRadioButton_MouseEnter(sender As Object, e As EventArgs) Handles Me.MouseEnter
        State = MouseState.Over
        _Shown = True
        Tmr.Enabled = True
        Tmr.Start()
        Invalidate()
    End Sub

    Private Sub XenonCheckBox_MouseLeave(sender As Object, e As EventArgs) Handles Me.MouseLeave
        State = MouseState.None
        _Shown = True
        Tmr.Enabled = True
        Tmr.Start()
        Invalidate()
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
            ColorPalette = New XenonColorPalette(Me)
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
    Protected Overrides Sub OnPaint(e As System.Windows.Forms.PaintEventArgs)
        Try
            Dim G As Graphics = e.Graphics
            If Parent Is Nothing Then Exit Sub
            G.Clear(GetParentColor(Me))

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

            Dim bkC As Color = If(_Checked, ColorPalette.Color_Back_Checked, ColorPalette.Color_Back)
            Dim bkCC As Color = Color.FromArgb(alpha, ColorPalette.Color_Back_Checked)

            FillRect(G, New SolidBrush(bkC), MainRect)
            FillRect(G, New SolidBrush(bkCC), MainRect)

            Dim lC As Color = Color.FromArgb(255 - alpha, If(_Checked, ColorPalette.Color_Border_Checked_Hover, ColorPalette.Color_Border))
            Dim lCC As Color = Color.FromArgb(alpha, ColorPalette.Color_Border_Checked_Hover)

            DrawRect_LikeW11(G, lC, MainRectInner)
            DrawRect_LikeW11(G, lCC, MainRect)

            If Image IsNot Nothing Then G.DrawImage(Image, CenterRect)

        Catch

        End Try
    End Sub


End Class

<DefaultEvent("CheckedChanged")>
Public Class XenonCheckBox
    Inherits Control
    Event CheckedChanged(sender As Object)

    Public ColorPalette As New XenonColorPalette()

    Sub New()
        SetStyle(DirectCast(139286, ControlStyles), True)
        SetStyle(ControlStyles.Selectable, False)
        DoubleBuffered = True
        AccentColor = Color.DodgerBlue
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
                Tmr2.Enabled = True
                Tmr2.Start()
                Invalidate()
            Catch
            End Try
        End Set
    End Property

    Private _Checked As Boolean

    ReadOnly Radius As Integer = 5
#Region "Accent Color Property"
    Private AccentColorValue As Color = Color.DodgerBlue
    Public Event AccentColorChanged As PropertyChangedEventHandler

    Private Sub AccentColorNotifyPropertyChanged(ByVal info As String)
        RaiseEvent AccentColorChanged(Me, New PropertyChangedEventArgs(info))
    End Sub

    Public Property AccentColor() As Color
        Get
            Return AccentColorValue
        End Get

        Set(ByVal AccentColor As Color)
            If Not (AccentColor = AccentColorValue) Then
                AccentColorValue = AccentColor
                AccentColorNotifyPropertyChanged("ControlColorChanged")
            End If
        End Set
    End Property

    Private Sub XenonCheckBox_AccentColorChanged(sender As Object, e As PropertyChangedEventArgs) Handles Me.AccentColorChanged
        Refresh()
    End Sub

#End Region

#End Region

#Region "Events"

    Enum MouseState
        None
        Over
        Down
    End Enum

    Public State As MouseState = MouseState.None

    Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
        Checked = Not Checked
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

    Private Sub XenonCheckbox_HandleCreated(sender As Object, e As EventArgs) Handles Me.HandleCreated
        Try
            alpha = If(DesignMode, 255, 0)
            alpha2 = If(Checked, 255, 0)
            ColorPalette = New XenonColorPalette(Me)
            If Not DesignMode Then
                Try
                    AddHandler FindForm.Load, AddressOf Loaded
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
#End Region

#Region "Animator"
    Dim alpha, alpha2 As Integer
    ReadOnly Factor As Integer = 25
    Dim WithEvents Tmr, Tmr2 As New Timer With {.Enabled = False, .Interval = 1}
    Private _Shown As Boolean = False
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
                End If

                If _Shown Then
                    Threading.Thread.Sleep(1)
                    Invalidate()
                End If
            End If

            If Not Checked Then
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

    Protected Overrides Sub OnPaint(e As System.Windows.Forms.PaintEventArgs)
        Try
            If Parent Is Nothing Then Exit Sub
            BackColor = Parent.BackColor

            Dim G As Graphics = e.Graphics
            G.SmoothingMode = SmoothingMode.AntiAlias
            G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit
            DoubleBuffered = True

            '################################################################################# Customizer
            Dim format As StringFormat = New StringFormat()

            Dim SZ1 As SizeF = G.MeasureString(Text, Font)
            Dim PT1 As New PointF(Height - 1, (CLng((Height - SZ1.Height)) \ 2) + 1)

            Dim OuterCheckRect As New Rectangle(3, 4, Height - 8, Height - 8)
            Dim InnerCheckRect As New Rectangle(4, 5, Height - 10, Height - 10)
            Dim TextRect As New Rectangle(Height - 1, (CLng((Height - SZ1.Height)) \ 2) + 1, Width - InnerCheckRect.Width, Height - 1)

#Region "Colors System"
            Dim HoverRect_Color As Color = Color.FromArgb(alpha2, ColorPalette.Color_Back_Checked)
            Dim HoverCheckedRect_Color As Color = Color.FromArgb(alpha, ColorPalette.Color_Border_Checked_Hover)
            Dim CheckRect_Color As Color = Color.FromArgb(alpha2, ColorPalette.Color_Core)
            Dim NonHoverRect_Color As Color = ColorPalette.Color_Border
            Dim BackRect_Color As Color = ColorPalette.Color_Back
            Dim ParentColor As Color = ColorPalette.Color_Parent
            Dim Selection_Color As Color = ColorPalette.Color_Parent_Hover
#End Region

            Dim RTL As Boolean = If(RightToLeft = 1, True, False)

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

            FillRect(G, New SolidBrush(BackRect_Color), OuterCheckRect, Radius)

            If _Checked Then
                FillRect(G, New SolidBrush(HoverRect_Color), OuterCheckRect, Radius)
                DrawRect(G, New Pen(Color.FromArgb(255 - alpha, HoverCheckedRect_Color)), InnerCheckRect, Radius)
                DrawRect(G, New Pen(Color.FromArgb(alpha, HoverCheckedRect_Color)), OuterCheckRect, Radius)

                G.DrawLine(CheckSignPen, x1_Left, y1_Left, x2_Left, y2_Left)
                G.DrawLine(CheckSignPen, x1_Right, y1_Right, x2_Right, y2_Right)
            Else
                FillRect(G, New SolidBrush(HoverRect_Color), OuterCheckRect, Radius)
                DrawRect(G, New Pen(HoverCheckedRect_Color), OuterCheckRect, Radius)
                G.DrawLine(CheckSignPen, x1_Left, y1_Left, x2_Left, y2_Left)
                G.DrawLine(CheckSignPen, x1_Right, y1_Right, x2_Right, y2_Right)
                DrawRect(G, New Pen(Color.FromArgb(255 - alpha, NonHoverRect_Color)), InnerCheckRect, Radius)
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
Public Class XenonGroupBox
    Inherits Panel
    Sub New()
        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.OptimizedDoubleBuffer Or ControlStyles.UserPaint Or ControlStyles.ResizeRedraw, True)

        DoubleBuffered = True

        Try
            BackColor = CCB(GetParentColor(Me), If(IsColorDark(GetParentColor(Me)), 0.05, -0.05))
            LineColor = CCB(GetParentColor(Me), If(IsColorDark(GetParentColor(Me)), 0.1, -0.1))
        Catch
        End Try

        LineSize = 1
    End Sub

#Region "Properties"
    Public Property LineSize As Integer = 1
    Public Property LineColor As Color = Color.FromArgb(87, 87, 87)
    Public Property DefaultColor As Color = Color.Black
    Public Property CustomColor As Boolean = False
    Public Property ForceNoNerd As Boolean = False
#End Region

    Private _Shown As Boolean = False

#Region "Events"

    Enum MouseState
        None
        Over
        Down
    End Enum

    Public State As MouseState = MouseState.None

    Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
        If Not CustomColor Then Exit Sub
        State = MouseState.Down
        Tmr.Enabled = True
        Tmr.Start()
        Invalidate()
        Invalidate()
    End Sub

    Protected Overrides Sub OnMouseUp(e As MouseEventArgs)
        If Not CustomColor Then Exit Sub
        State = MouseState.Over
        Tmr.Enabled = True
        Tmr.Start()
        Invalidate()
        Invalidate()
    End Sub

    Private Sub XenonCheckBox_MouseEnter(sender As Object, e As EventArgs) Handles Me.MouseEnter
        If Not CustomColor Then Exit Sub
        State = MouseState.Over
        Tmr.Enabled = True
        Tmr.Start()
        Invalidate()
        Invalidate()
    End Sub

    Private Sub XenonCheckBox_MouseLeave(sender As Object, e As EventArgs) Handles Me.MouseLeave
        If Not CustomColor Then Exit Sub
        State = MouseState.None
        Tmr.Enabled = True
        Tmr.Start()
        Invalidate()
        Invalidate()
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

    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        MyBase.OnPaint(e)
        Dim G As Graphics = e.Graphics
        G.SmoothingMode = SmoothingMode.AntiAlias
        Dim Rect As New Rectangle(0, 0, Width - 1, Height - 1)
        Dim RectInner As New Rectangle(1, 1, Width - 3, Height - 3)

        G.Clear(GetParentColor(Me))

        If Not CustomColor Then
            BackColor = CCB(GetParentColor(Me), If(IsColorDark(GetParentColor(Me)), 0.04, -0.05))
            LineColor = CCB(GetParentColor(Me), If(IsColorDark(GetParentColor(Me)), 0.03, -0.06))

            FillRect(G, New SolidBrush(BackColor), Rect)
            DrawRect(G, New Pen(LineColor), Rect)
        Else
            Select Case State
                Case MouseState.None
                    LineColor = CCB(BackColor, If(IsColorDark(BackColor), 0.1, -0.1))

                Case MouseState.Over
                    LineColor = CCB(BackColor, If(IsColorDark(BackColor), 0.25, -0.25))

                Case MouseState.Down
                    LineColor = CCB(BackColor, If(IsColorDark(BackColor), 0.17, -0.17))
            End Select

            LineColor = Color.FromArgb(255, LineColor.R, LineColor.G, LineColor.B)

            FillRect(G, New SolidBrush(BackColor), RectInner)
            DrawRect_LikeW11(G, LineColor, RectInner)

            FillRect(G, New SolidBrush(Color.FromArgb(alpha, BackColor)), Rect)
            DrawRect_LikeW11(G, Color.FromArgb(alpha, LineColor), Rect)

            If Not DesignMode Then
                If My.Application._Settings.Nerd_Stats And Not ForceNoNerd Then
                    G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit

                    Dim FC0 As Color = If(IsColorDark(BackColor), ControlPaint.LightLight(LineColor), ControlPaint.Dark(LineColor, 0.9))
                    Dim FC1 As Color = If(IsColorDark(BackColor), ControlPaint.LightLight(LineColor), ControlPaint.Dark(LineColor, 0.9))

                    FC0 = Color.FromArgb(100, FC0)
                    FC1 = Color.FromArgb(alpha, FC1)

                    Dim RectX As Rectangle = Rect
                    RectX.Y += 1

                    Dim S As String
                    Select Case My.Application._Settings.Nerd_Stats_Kind
                        Case XeSettings.Nerd_Stats_Type.HEX

                            If BackColor.A = 255 Then
                                S = If(My.Application._Settings.Nerd_Stats_HexHash, "#", "") & RGB2HEX(BackColor, False)
                            Else
                                S = If(My.Application._Settings.Nerd_Stats_HexHash, "#", "") & RGB2HEX(BackColor, True)
                            End If

                        Case XeSettings.Nerd_Stats_Type.RGB

                            If BackColor.A = 255 Then
                                S = String.Format("{0},{1},{2}", BackColor.R, BackColor.G, BackColor.B)
                            Else
                                S = String.Format("{0},{1},{2},{3}", BackColor.A, BackColor.R, BackColor.G, BackColor.B)
                            End If

                    End Select

                    G.DrawString(S, New Font("Lucida Console", 7.5), New SolidBrush(FC0), RectX, StringAligner(ContentAlignment.MiddleCenter))
                    G.DrawString(S, New Font("Lucida Console", 7.5), New SolidBrush(FC1), RectX, StringAligner(ContentAlignment.MiddleCenter))

                End If
            End If
        End If

    End Sub

    Function RGB2HEX(ByVal [Color] As Color, Optional ByVal Alpha As Boolean = True) As String
        Dim S As String
        If Alpha Then
            S = String.Format("{0:X2}", Color.A, Color.R, Color.G, Color.B) & String.Format("{1:X2}", Color.A, Color.R, Color.G, Color.B) &
            String.Format("{2:X2}", Color.A, Color.R, Color.G, Color.B) & String.Format("{3:X2}", Color.A, Color.R, Color.G, Color.B)
        Else
            S = String.Format("{0:X2}{1:X2}{2:X2}", Color.R, Color.G, Color.B)
        End If
        Return S
    End Function

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
            If Image IsNot Nothing Then : LineImage = GetAverageColor(CType(Image, Bitmap))
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
                    LineImage = GetAverageColor(CType(Image, Bitmap))
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
    ReadOnly Delay As Integer = 2

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
                C_After = ControlPaint.Dark(LineColor, 0.25)
            Case False
                C_After = ControlPaint.Light(LineColor, 0.9)
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
        Dim C_After As Color = CCB(GetParentColor(Me), If(IsColorDark(GetParentColor(Me)), 0.04, -0.07))

        If Not DesignMode Then Visual.FadeColor(Me, "BackColor", C_Before, C_After, Steps, Delay)

        If _Shown Then
            Tmr.Enabled = True
            Tmr.Start()
        End If

        Invalidate()
    End Sub

    Protected Overrides Sub OnMouseDown(ByVal e As MouseEventArgs)
        MyBase.OnMouseDown(e)

        Dim C_Before As Color = BackColor
        Dim C_After As Color
        Select Case GetDarkMode()
            Case True
                C_After = ControlPaint.Dark(LineColor, 0.5)
            Case False
                C_After = ControlPaint.Light(LineColor, 0.75)
        End Select
        If Not DesignMode Then Visual.FadeColor(Me, "BackColor", C_Before, C_After, Steps, Delay)
        State = MouseState.Down

        If _Shown Then
            Tmr.Enabled = True
            Tmr.Start()
        End If

        Invalidate()
    End Sub

    Protected Overrides Sub OnMouseUp(ByVal e As MouseEventArgs)
        Try : MyBase.OnMouseUp(e) : Catch : End Try

        Dim C_Before As Color = BackColor
        Dim C_After As Color

        Select Case GetDarkMode()
            Case True
                C_After = ControlPaint.Dark(LineColor, 0.25)
            Case False
                C_After = ControlPaint.Light(LineColor, 0.9)
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
                C_After = ControlPaint.Dark(LineColor, 0.5)
            Case False
                C_After = ControlPaint.Light(LineColor, 0.75)
        End Select

        If Not DesignMode Then Visual.FadeColor(Me, "BackColor", C_Before, C_After, Steps, Delay)
        State = MouseState.Down : Invalidate()
    End Sub

    Protected Overrides Sub OnKeyUp(ByVal e As KeyEventArgs)
        MyBase.OnKeyUp(e)
        State = MouseState.None

        Dim C_Before As Color = BackColor
        Dim C_After As Color = CCB(GetParentColor(Me), If(IsColorDark(GetParentColor(Me)), 0.04, -0.04))
        If Not DesignMode Then Visual.FadeColor(Me, "BackColor", C_Before, C_After, Steps, Delay)
        Invalidate()
    End Sub
#End Region

    Protected Overrides Sub OnLeave(ByVal e As EventArgs)
        MyBase.OnLeave(e)
        State = MouseState.None

        Dim C_Before As Color = BackColor
        Dim C_After As Color = CCB(GetParentColor(Me), If(IsColorDark(GetParentColor(Me)), 0.04, -0.04))
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

    Dim Noise As New TextureBrush(FadeBitmap(My.Resources.GaussianBlur, 0.4))

    Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
        Dim G As Graphics = e.Graphics
        G.SmoothingMode = SmoothingMode.AntiAlias
        G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit
        DoubleBuffered = True

        Try
            If Image IsNot Nothing Then : LineColor = GetAverageColor(CType(Image, Bitmap))
            Else : LineImage = LineColor : End If
        Catch : End Try

        '################################################################################# Customizer
        Dim Rect As New Rectangle(0, 0, Width - 1, Height - 1)
        Dim ParentColor As Color = GetParentColor(Me)
        Dim InnerRect As New Rectangle(1, 1, Width - 3, Height - 3)
        '#################################################################################

        G.Clear(ParentColor)
        Dim c1, c1x, c2 As Color

        FillRect(G, New SolidBrush(Color.FromArgb(255 - alpha, BackColor)), InnerRect)
        FillRect(G, New SolidBrush(Color.FromArgb(alpha, BackColor)), Rect)
        If Not State = MouseState.None Then FillRect(G, Noise, Rect)

        c1 = Color.FromArgb(255 - alpha, CCB(BackColor, If(IsColorDark(ParentColor), 0.04, -0.04)))
        c1x = Color.FromArgb(alpha, CCB(BackColor, If(IsColorDark(ParentColor), 0.04, -0.04)))

        DrawRect_LikeW11(G, c1, InnerRect)
        DrawRect_LikeW11(G, c1x, Rect)

        Select Case State
            Case MouseState.Over
                If GetDarkMode() Then
                    c2 = Color.FromArgb(alpha, ControlPaint.Light(LineColor, 0.005))
                Else
                    c2 = Color.FromArgb(alpha, ControlPaint.Light(LineColor, 0.5))
                End If

                DrawRect_LikeW11(G, c2, Rect)
                ForeColor = If(GetDarkMode(), ControlPaint.Light(c2), ControlPaint.Dark(c2))

            Case MouseState.Down
                If GetDarkMode() Then
                    c2 = Color.FromArgb(alpha, ControlPaint.Light(LineColor, 0.1))
                Else
                    c2 = Color.FromArgb(alpha, ControlPaint.Light(LineColor, 0.3))
                End If

                DrawRect_LikeW11(G, c2, InnerRect)
                ForeColor = If(GetDarkMode(), ControlPaint.Light(c2), ControlPaint.Dark(c2))

            Case MouseState.None

                If GetDarkMode() Then
                    c2 = Color.FromArgb(alpha, ControlPaint.Light(BC, 0.07))
                Else
                    c2 = Color.FromArgb(alpha, ControlPaint.Light(BC, 0.5))
                End If

                If Focused Then c2 = Color.FromArgb(255 - alpha, LineColor)

                DrawRect_LikeW11(G, c2, InnerRect)
                ForeColor = If(GetDarkMode(), Color.White, Color.Black)
        End Select



#Region "Text and Image Render"
        Dim ButtonString As New StringFormat() With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center}
        Dim RTL As Boolean = If(RightToLeft = 1, True, False)
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
                    Dim alx As Integer = CInt((Height - (Image.Height + 4 + MeasureString(Text, MyBase.Font).Height)) / 2)

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
                    Dim RecText As New Rectangle(Bo, imgY, MeasureString(Text, Font).Width + 15 - imgY, Image.Height)
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
                BackColor = CCB(GetParentColor(Me), If(IsColorDark(GetParentColor(Me)), 0.04, -0.04))
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
    End Sub

    Sub Rfrsh()
        If _Shown Then
            BC = CCB(GetParentColor(Me), If(IsColorDark(GetParentColor(Me)), 0.04, -0.04))
            BackColor = BC
            Invalidate()
        End If
    End Sub

End Class
Public Class XenonSeparator
    Inherits Control
    Private G As Graphics

    Sub New()
        TabStop = False
        DoubleBuffered = True
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
            If GetDarkMode() Then IdleLine = CCB(Parent.BackColor, 0.1) Else IdleLine = CCB(Parent.BackColor, -0.1)
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

    Public ColorPalette As New XenonColorPalette()

    Sub New()
        Enabled = True
        DoubleBuffered = True
        LineColor = Color.DodgerBlue
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
#Region "Line Color Property"
    Private LineColorValue As Color = Color.DodgerBlue
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
        ColorPalette = New XenonColorPalette(Me)
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
        ColorPalette = New XenonColorPalette(Me)
        Invalidate()
    End Sub

    Public Sub RefreshColorPalette()
        If _Shown Then
            ColorPalette = New XenonColorPalette(Me)
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
        Dim RTL As Boolean = If(RightToLeft = 1, True, False)

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

        G.Clear(ColorPalette.Color_Parent)

        FillRect(G, New SolidBrush(Color.FromArgb(255 - alpha, ColorPalette.Color_Back)), OuterRect)
        FillRect(G, New SolidBrush(Color.FromArgb(alpha, ColorPalette.Color_Back_Checked)), OuterRect)
        FillRect(G, New SolidBrush(Color.FromArgb(alpha, ColorPalette.Color_Border_Checked_Hover)), SideRect)

        DrawRect_LikeW11(G, Color.FromArgb(255 - alpha, ColorPalette.Color_Border), InnerRect)
        DrawRect_LikeW11(G, Color.FromArgb(alpha, ColorPalette.Color_Border_Checked_Hover), OuterRect)

        If Focused And State = MouseState.None Then DrawRect(G, New Pen(Color.FromArgb(255, ColorPalette.Color_Border_Checked_Hover)), InnerRect)

        Using TextColor As New SolidBrush(If(GetDarkMode(), Color.White, Color.Black)), TextFont As New Font("Segoe UI", 9)
            G.DrawString(CStr(Value), TextFont, New SolidBrush(TextColor.Color), New Rectangle(0, 0, Width - 15, Height), StringAligner(ContentAlignment.MiddleCenter))
        End Using

        Using SignColor As New SolidBrush(ColorPalette.Color_Back_Checked), SignFont As New Font("Marlett", 11)
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
            If GetDarkMode() Then IdleLine = CCB(Parent.BackColor, 0.1) Else IdleLine = CCB(Parent.BackColor, -0.1)
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
        LineColor = Color.DodgerBlue
        If _Multiline Then
            TB.Height = Height - 8
        Else
            Height = TB.Height + 8
        End If

        AddHandler TB.TextChanged, AddressOf OnBaseTextChanged
        AddHandler TB.KeyDown, AddressOf OnBaseKeyDown
    End Sub

    Public ColorPalette As New XenonColorPalette()

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
#Region "Line Color Property"
    Private LineColorValue As Color = Color.DodgerBlue
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
                ColorPalette = New XenonColorPalette(Me)
                If Not DesignMode Then
                    Try
                        AddHandler FindForm.Load, AddressOf Loaded
                        AddHandler FindForm.Shown, AddressOf Showed
                        AddHandler Parent.BackColorChanged, AddressOf RefreshColorPalette
                        AddHandler EnabledChanged, AddressOf RefreshColorPalette
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


        Dim FadeInColor As Color = Color.FromArgb(alpha, ColorPalette.Color_Border_Checked_Hover)
        Dim FadeOutColor As Color = Color.FromArgb(255 - alpha, ColorPalette.Color_Parent_Hover)

        G.Clear(ColorPalette.Color_Parent)

        TB.ForeColor = ForeColor

        If TB.Focused Or Focused Then
            FillRect(G, New SolidBrush(ColorPalette.Color_Back_Checked), OuterRect)
            DrawRect_LikeW11(G, ColorPalette.Color_Border_Checked_Hover, OuterRect)
            TB.BackColor = ColorPalette.Color_Back_Checked
        Else
            FillRect(G, New SolidBrush(ColorPalette.Color_Back), InnerRect)
            FillRect(G, New SolidBrush(Color.FromArgb(alpha, ColorPalette.Color_Back)), OuterRect)

            DrawRect_LikeW11(G, FadeInColor, OuterRect)
            DrawRect_LikeW11(G, FadeOutColor, InnerRect)
            TB.BackColor = ColorPalette.Color_Back
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
        ColorPalette = New XenonColorPalette(Me)
        If Not DesignMode Then
            Try
                AddHandler FindForm.Load, AddressOf Loaded
                AddHandler FindForm.Shown, AddressOf Showed
                AddHandler Parent.BackColorChanged, AddressOf RefreshColorPalette
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
        ColorPalette = New XenonColorPalette(Me)
        Invalidate()
    End Sub

    Public Sub RefreshColorPalette()
        If _Shown Then
            ColorPalette = New XenonColorPalette(Me)
            Invalidate()
        End If
    End Sub
End Class
Public Class XenonComboBox : Inherits ComboBox
    Sub New()
        SetStyle(ControlStyles.AllPaintingInWmPaint, True)
        SetStyle(ControlStyles.ResizeRedraw, True)
        SetStyle(ControlStyles.UserPaint, True)
        SetStyle(ControlStyles.DoubleBuffer, True)
        SetStyle(ControlStyles.SupportsTransparentBackColor, True)
        Size = New Size(190, 27)
        DrawMode = Windows.Forms.DrawMode.OwnerDrawFixed
        If GetDarkMode() Then BackColor = Color.FromArgb(55, 55, 55) Else BackColor = Color.FromArgb(225, 225, 225)
        ForeColor = Color.White
        LineColor = Color.DodgerBlue
        DropDownStyle = ComboBoxStyle.DropDownList
        Font = New Font("Segoe UI", 9)
        DoubleBuffered = True
    End Sub

    Public ColorPalette As New XenonColorPalette()

#Region "Properties"

#Region "Line Color Property"
    Private LineColorValue As Color = Color.DodgerBlue
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
#End Region

    Dim Noise As New TextureBrush(FadeBitmap(My.Resources.GaussianBlur, 0.3))


#Region "Subs"
    Sub ReplaceItem(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DrawItemEventArgs) Handles Me.DrawItem
        BackColor = ColorPalette.Color_Back
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias
        e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit
        e.DrawBackground()

        If IsColorDark(BackColor) Then
            If ForeColor <> Color.White Then ForeColor = Color.White
        Else
            If ForeColor <> Color.Black Then ForeColor = Color.Black
        End If

        Try
            e.Graphics.FillRectangle(New SolidBrush(BackColor), e.Bounds)

            If (e.State And DrawItemState.Selected) = DrawItemState.Selected Then
                e.Graphics.FillRectangle(New SolidBrush(ColorPalette.Color_Border_Checked_Hover), e.Bounds)
            End If

            e.Graphics.DrawString(MyBase.GetItemText(MyBase.Items(e.Index)), e.Font, New SolidBrush(ForeColor), e.Bounds.X + 2, e.Bounds.Y + 1)
        Catch
        End Try
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
        Try
            alpha = 0
            alpha2 = 0

            Try
                If Not DesignMode Then
                    AddHandler Parent.BackColorChanged, AddressOf Invalidate
                    AddHandler BackColorChanged, AddressOf Invalidate
                End If
            Catch
            End Try

            ColorPalette = New XenonColorPalette(Me)

            If Not DesignMode Then
                Try
                    AddHandler FindForm.Load, AddressOf Loaded
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

        Dim FadeInColor As Color = Color.FromArgb(alpha, ColorPalette.Color_Border_Checked_Hover)
        Dim FadeOutColor As Color = Color.FromArgb(255 - alpha, ColorPalette.Color_Border)

        G.Clear(ColorPalette.Color_Parent)

        FillRect(G, New SolidBrush(ColorPalette.Color_Back), InnerRect)
        FillRect(G, New SolidBrush(Color.FromArgb(alpha, ColorPalette.Color_Back)), OuterRect)
        FillRect(G, Noise, InnerRect)

        DrawRect_LikeW11(G, FadeInColor, OuterRect)
        DrawRect_LikeW11(G, FadeOutColor, InnerRect)

        FillRect(G, New SolidBrush(Color.FromArgb(alpha2, ColorPalette.Color_Back_Checked)), OuterRect)
        DrawRect_LikeW11(G, Color.FromArgb(alpha2, ColorPalette.Color_Border_Checked_Hover), OuterRect)

        If Focused And State = MouseState.None Then
            DrawRect(G, New Pen(Color.FromArgb(255, FadeInColor)), InnerRect)
            G.DrawLine(New Pen(Color.FromArgb(255, FadeInColor), 2), New Point(Width - 18, 10), New Point(Width - 14, 14))
            G.DrawLine(New Pen(Color.FromArgb(255, FadeInColor), 2), New Point(Width - 14, 14), New Point(Width - 10, 10))
            G.DrawLine(New Pen(Color.FromArgb(255, FadeInColor)), New Point(Width - 14, 15), New Point(Width - 14, 14))
        Else
            G.DrawLine(New Pen(Color.FromArgb(255 - alpha, ForeColor), 2), New Point(Width - 18, 10), New Point(Width - 14, 14))
            G.DrawLine(New Pen(Color.FromArgb(255 - alpha, ForeColor), 2), New Point(Width - 14, 14), New Point(Width - 10, 10))
            G.DrawLine(New Pen(Color.FromArgb(255 - alpha, ForeColor)), New Point(Width - 14, 15), New Point(Width - 14, 14))

            G.DrawLine(New Pen(FadeInColor, 2), New Point(Width - 18, 10), New Point(Width - 14, 14))
            G.DrawLine(New Pen(FadeInColor, 2), New Point(Width - 14, 14), New Point(Width - 10, 10))
            G.DrawLine(New Pen(FadeInColor), New Point(Width - 14, 15), New Point(Width - 14, 14))
        End If

        G.DrawString(Text, Font, New SolidBrush(ForeColor), TextRect, New StringFormat With {.LineAlignment = StringAlignment.Center, .Alignment = StringAlignment.Near})
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
        Dim RTL As Boolean = If(RightToLeft = 1, True, False)

        Select Case _alertStyle
            Case Style.Simple
                borderColor = Color.FromArgb(90, 90, 90)
                innerColor = Color.FromArgb(50, 50, 50)
                textColor = Color.FromArgb(150, 150, 150)
            Case Style.Success
                borderColor = Color.FromArgb(60, 98, 79)
                innerColor = Color.FromArgb(60, 85, 79)
                textColor = Color.FromArgb(35, 169, 110)
            Case Style.Notice
                borderColor = Color.FromArgb(70, 91, 107)
                innerColor = Color.FromArgb(70, 91, 94)
                textColor = Color.FromArgb(97, 185, 186)
            Case Style.Warning
                borderColor = Color.FromArgb(100, 71, 71)
                innerColor = Color.FromArgb(87, 71, 71)
                textColor = Color.FromArgb(254, 142, 122)
            Case Style.Informations
                borderColor = Color.FromArgb(133, 133, 71)
                innerColor = Color.FromArgb(120, 120, 71)
                textColor = Color.FromArgb(254, 224, 122)
            Case Style.Indigo
                Dim cc As Color = Color.Indigo
                borderColor = ControlPaint.Light(cc, 0.005)
                innerColor = ControlPaint.Dark(cc, 0.05)
                textColor = ControlPaint.LightLight(cc)
            Case Style.Custom
                borderColor = ControlPaint.Light(CustomColor, 0.005)
                innerColor = ControlPaint.Dark(CustomColor, 0.05)
                textColor = ControlPaint.LightLight(CustomColor)
            Case Style.Adaptive
                If Image IsNot Nothing Then
                    Dim cc As Color = GetAverageColor(CType(Image, Bitmap))
                    borderColor = ControlPaint.Light(cc, 0.005)
                    innerColor = ControlPaint.Dark(cc, 0.05)
                    textColor = ControlPaint.LightLight(cc)
                Else
                    borderColor = ControlPaint.Light(CustomColor, 0.005)
                    innerColor = ControlPaint.Dark(CustomColor, 0.05)
                    textColor = ControlPaint.LightLight(CustomColor)
                End If
        End Select

        G.Clear(GetParentColor(Me))

        BackColor = innerColor

        FillRect(G, New SolidBrush(innerColor), New Rectangle(0, 0, Width - 1, Height - 1))
        DrawRect_LikeW11(G, borderColor, New Rectangle(0, 0, Width - 1, Height - 1))

        Dim textY As Integer = CInt((Height - MeasureString(Text, Font).Height) / 2)
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

    Dim Noise As New TextureBrush(FadeBitmap(My.Resources.GaussianBlur, 0.15))

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
                    Try : Noise7 = FadeBitmap(My.Resources.AeroGlass, NoisePower / 100) : Catch : End Try
                    Try : Noise7Start = FadeBitmap(My.Resources.Start7Glass, NoisePower / 100) : Catch : End Try
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
                Refresh()
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
                Refresh()
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
                Refresh()
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
                Refresh()
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
                Refresh()
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
                Refresh()
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

    Public Sub DrawRR(ByVal [Graphics] As Graphics, ByVal BorderColor As Color, ByVal [Rectangle] As Rectangle, Optional ByVal [Radius_willbe_x2] As Integer = -1)
        Try
            [Radius_willbe_x2] *= 2
            [Graphics].SmoothingMode = SmoothingMode.AntiAlias

            Dim [Pen] As New Pen(BorderColor)
            Dim [Pen2] As New Pen(CCB(BorderColor, 0.2))

            Dim R1 As New Rectangle([Rectangle].X, [Rectangle].Y, 6, 6)
            Dim LG As New LinearGradientBrush(R1, [Pen2].Color, [Pen].Color, LinearGradientMode.Vertical)

            [Graphics].DrawArc(New Pen(LG), [Rectangle].X, [Rectangle].Y, [Radius_willbe_x2], [Radius_willbe_x2], 180, 90)
            [Graphics].DrawLine([Pen2], CInt([Rectangle].X + [Radius_willbe_x2] / 2), [Rectangle].Y, CInt([Rectangle].X + [Rectangle].Width - [Radius_willbe_x2] / 2), [Rectangle].Y)
            [Graphics].DrawArc(New Pen(LG), [Rectangle].X + [Rectangle].Width - [Radius_willbe_x2], [Rectangle].Y, [Radius_willbe_x2], [Radius_willbe_x2], 270, 90)

            [Graphics].DrawLine([Pen], [Rectangle].X, CInt([Rectangle].Y + [Radius_willbe_x2] / 2), [Rectangle].X, CInt([Rectangle].Y + [Rectangle].Height - [Radius_willbe_x2] / 2.5))
            [Graphics].DrawLine([Pen], CInt([Rectangle].X + [Rectangle].Width), CInt([Rectangle].Y + [Radius_willbe_x2] / 2), CInt([Rectangle].X + [Rectangle].Width), CInt([Rectangle].Y + [Rectangle].Height - [Radius_willbe_x2] / 2.5))

            [Graphics].DrawLine([Pen], CInt([Rectangle].X + [Radius_willbe_x2] / 2), CInt([Rectangle].Y + [Rectangle].Height), CInt([Rectangle].X + [Rectangle].Width - [Radius_willbe_x2] / 2), CInt([Rectangle].Y + [Rectangle].Height))
            [Graphics].DrawArc([Pen], [Rectangle].X, [Rectangle].Y + [Rectangle].Height - [Radius_willbe_x2], [Radius_willbe_x2], [Radius_willbe_x2], 90, 90)
            [Graphics].DrawArc([Pen], [Rectangle].X + [Rectangle].Width - [Radius_willbe_x2], [Rectangle].Y + [Rectangle].Height - [Radius_willbe_x2], [Radius_willbe_x2], [Radius_willbe_x2], 0, 90)
        Catch
        End Try
    End Sub

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
                    FillImg(G, adaptedBackBlurred, RRect, Radius, True)
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

                        DrawAero(G, RestRect, bk, Color1, ColBal, Color2, GlowBal, alphaX, 3)

                    Else
                        FillRect(G, New SolidBrush(Color.White), RestRect, 3, True)
                        FillRect(G, New SolidBrush(Color.FromArgb(255 * BackColorAlpha / 100, BackColor)), RestRect, 3, True)
                    End If

                    FillImg(G, Noise7Start, New Rectangle(0, 0, Width, Height), 3, True)

                    FillImg(G, My.Resources.Start7, New Rectangle(0, 0, Width, Height), 3, True)

                Else
                    FillImg(G, My.Resources.Start7Basic, New Rectangle(0, 0, Width, Height), 2, True)
                End If

            Else

                FillRect(G, New SolidBrush(Color.FromArgb(120, 70, 70, 70)), RRect, Radius, True)
                FillRect(G, New SolidBrush(Color.FromArgb(If(Transparency, BackColorAlpha, 255), BackColor)), RRect, Radius, True)

                If Transparency And Not UseItAsTaskbar_Version = TaskbarVersion.Eight Then FillRect(G, Noise, RRect, Radius, True)

                If UseItAsStartMenu Then
                    Dim SearchRect As New Rectangle(7, 10, 120, 18)
                    Dim SearchRectFixer As New Rectangle(7, 21, 120, 5)
                    Dim SearchRectTop As New Rectangle(7, 10, 120, 16)

                    FillImg(G, If(DarkMode, My.Resources.Start11_Dark, My.Resources.Start11_Light), New Rectangle(0, 0, Width - 1, Height - 1), Radius, True)

                    FillRect(G, New SolidBrush(SearchBoxAccent), SearchRect, Radius, True)
                    FillRect(G, New SolidBrush(If(DarkMode, Color.FromArgb(30, 30, 30), Color.FromArgb(230, 230, 230))), SearchRectTop, Radius, True)
                    G.FillRectangle(New SolidBrush(If(DarkMode, Color.FromArgb(30, 30, 30), Color.FromArgb(230, 230, 230))), SearchRectFixer)
                    DrawRect(G, New Pen(If(DarkMode, Color.FromArgb(50, 50, 50), Color.FromArgb(200, 200, 200))), SearchRect, Radius, True)
                End If

                If UseItAsActionCenter Then
                    Button1 = New Rectangle(8, 8, 49, 20)
                    Button2 = New Rectangle(62, 8, 49, 20)

                    FillImg(G, If(DarkMode, My.Resources.AC_11_Dark, My.Resources.AC_11_Light), New Rectangle(0, 0, Width - 1, Height - 1), Radius, True)

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

                    FillRect(G, New SolidBrush(Cx1), Button1, Radius, True)
                    DrawRR(G, Cx1, Button1, Radius)

                    FillRect(G, New SolidBrush(Cx2), Button2, Radius, True)
                    DrawRect(G, New Pen(CCB(Cx2, If(DarkMode, 0.05, -0.05))), Button2, Radius)
                End If

                If Borders Then DrawRect(G, New Pen(Color.FromArgb(150, 76, 76, 76)), New Rectangle(0, 0, Width - 1, Height - 1), Radius, True)

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

                G.FillRectangle(New SolidBrush(LinkColor), rect3)
                G.DrawImage(If(DarkMode, My.Resources.AC_10_Dark, My.Resources.AC_10_Light), New Rectangle(0, 0, Width - 1, Height - 1))
                G.FillRectangle(New SolidBrush(LinkColor), rect1)
                G.FillRectangle(New SolidBrush(LinkColor), rect2)

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
                    Dim AppBtnRectUnderline As New Rectangle(AppBtnRect.X + (AppBtnRect.Width - 17) / 2 - 1, AppBtnRect.Y + AppBtnRect.Height - 3, 20, 3)

                    Dim BackC As Color
                    Dim BorderC As Color

                    If DarkMode Then
                        BackC = Color.FromArgb(100, 80, 80, 80)
                        BorderC = Color.FromArgb(110, 80, 80, 80)
                    Else
                        BackC = Color.FromArgb(100, 230, 230, 230)
                        BorderC = Color.FromArgb(110, 230, 230, 230)
                    End If

                    FillRect(G, New SolidBrush(BackC), StartBtnRect, Radius, True)
                    DrawRR(G, BorderC, StartBtnRect, Radius)
                    G.DrawImage(If(DarkMode, My.Resources.StartBtn_11Dark, My.Resources.StartBtn_11Light), StartImgRect)

                    FillRect(G, New SolidBrush(BackC), AppBtnRect, Radius, True)
                    DrawRR(G, BorderC, AppBtnRect, Radius)
                    G.DrawImage(My.Resources.ActiveApp_Taskbar, AppImgRect)
                    FillRect(G, New SolidBrush(_AppUnderline), AppBtnRectUnderline, 2, True)

                    G.DrawImage(My.Resources.InactiveApp_Taskbar, App2ImgRect)
                    FillRect(G, New SolidBrush(Color.FromArgb(255, BackC)), App2BtnRectUnderline, 2, True)

                Case TaskbarVersion.Ten
                    Dim StartBtnRect As New Rectangle(-1, -1, 42, Height + 2)
                    Dim StartBtnImgRect As New Rectangle(StartBtnRect.X + (StartBtnRect.Width - My.Resources.StartBtn_10Dark.Width) / 2, StartBtnRect.Y + (StartBtnRect.Height - My.Resources.StartBtn_10Dark.Height) / 2, My.Resources.StartBtn_10Dark.Width, My.Resources.StartBtn_10Dark.Height)


                    Dim AppBtnRect As New Rectangle(StartBtnRect.Right, -1, 40, Height + 2)
                    Dim AppBtnImgRect As New Rectangle(AppBtnRect.X + (AppBtnRect.Width - My.Resources.AppPreview.Width) / 2 + 1, AppBtnRect.Y + (AppBtnRect.Height - My.Resources.AppPreview.Height) / 2 - 1, My.Resources.AppPreview.Width, My.Resources.AppPreview.Height)
                    Dim AppBtnRectUnderline As New Rectangle(AppBtnRect.X, AppBtnRect.Y + AppBtnRect.Height - 3, AppBtnRect.Width, 2)

                    Dim App2BtnRect As New Rectangle(AppBtnRect.Right, -1, 40, Height + 2)
                    Dim App2BtnImgRect As New Rectangle(App2BtnRect.X + (App2BtnRect.Width - My.Resources.AppPreviewInActive.Width) / 2 + 1, App2BtnRect.Y + (App2BtnRect.Height - My.Resources.AppPreviewInActive.Height) / 2 - 1, My.Resources.AppPreviewInActive.Width, My.Resources.AppPreviewInActive.Height)
                    Dim App2BtnRectUnderline As New Rectangle(App2BtnRect.X + 14 / 2, App2BtnRect.Y + App2BtnRect.Height - 3, App2BtnRect.Width - 14, 2)


                    Dim StartColor As Color = If(Transparency, Color.FromArgb(100, 100, 100, 100), _StartColor)
                    G.FillRectangle(New SolidBrush(StartColor), StartBtnRect)
                    G.DrawImage(If(DarkMode, My.Resources.StartBtn_10Dark, My.Resources.StartBtn_10Light), StartBtnImgRect)

                    Dim AppColor As Color = If(Transparency, Color.FromArgb(80, 100, 100, 100), _AppBackground)
                    G.FillRectangle(New SolidBrush(AppColor), AppBtnRect)
                    G.FillRectangle(New SolidBrush(ControlPaint.Light(_AppUnderline)), AppBtnRectUnderline)
                    G.DrawImage(My.Resources.AppPreview, AppBtnImgRect)

                    G.FillRectangle(New SolidBrush(ControlPaint.Light(_AppUnderline)), App2BtnRectUnderline)
                    G.DrawImage(My.Resources.AppPreviewInActive, App2BtnImgRect)

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

                            DrawAero(G, Rect, bk, Color1, ColBal, Color2, GlowBal, alphaX, 0)

                        Else
                            G.FillRectangle(New SolidBrush(Color.White), Rect)
                            G.FillRectangle(New SolidBrush(Color.FromArgb(255 * BackColorAlpha / 100, BackColor)), Rect)
                        End If

                        G.DrawImage(My.Resources.Win7TaskbarSides, Rect)

                        FillImg(G, Noise7.Clone(Bounds, PixelFormat.Format32bppArgb), Rect, Radius, True)
                    End If

                    If Not Basic Then
                        G.DrawLine(New Pen(Color.FromArgb(80, 0, 0, 0)), New Point(0, 0), New Point(Width - 1, 0))
                        G.DrawLine(New Pen(Color.FromArgb(80, 255, 255, 255)), New Point(0, 1), New Point(Width - 1, 1))
                    End If

                    G.DrawImage(My.Resources.AeroPeek, New Rectangle(Width - 10, 0, 10, Height))

                    Dim StartORB As New Bitmap(My.Resources.Win7ORB)

                    Dim StartBtnRect As New Rectangle(3, -3, 39, 39)

                    Dim AppBtnRect As New Rectangle(StartBtnRect.Right + 5, 0, 45, 35)
                    Dim AppBtnImgRect As New Rectangle(AppBtnRect.X + (AppBtnRect.Width - My.Resources.AppPreview.Width) / 2, AppBtnRect.Y + (AppBtnRect.Height - My.Resources.AppPreview.Height) / 2 - 1, My.Resources.AppPreview.Width, My.Resources.AppPreview.Height)

                    Dim App2BtnRect As New Rectangle(AppBtnRect.Right + 1, 0, 45, 35)
                    Dim App2BtnImgRect As New Rectangle(App2BtnRect.X + (App2BtnRect.Width - My.Resources.AppPreviewInActive.Width) / 2, App2BtnRect.Y + (App2BtnRect.Height - My.Resources.AppPreviewInActive.Height) / 2 - 1, My.Resources.AppPreviewInActive.Width, My.Resources.AppPreviewInActive.Height)

                    G.DrawImage(StartORB, StartBtnRect)

                    DrawRect(G, New Pen(Color.FromArgb(150, 0, 0, 0)), New Rectangle(AppBtnRect.X, AppBtnRect.Y, AppBtnRect.Width - 2, AppBtnRect.Height - 2), 2)
                    G.DrawImage(My.Resources.Taskbar_ActiveApp7, AppBtnRect)
                    G.DrawImage(My.Resources.AppPreview, AppBtnImgRect)

                    DrawRect(G, New Pen(Color.FromArgb(110, 0, 0, 0)), New Rectangle(App2BtnRect.X, App2BtnRect.Y, App2BtnRect.Width - 2, App2BtnRect.Height - 2), 2)
                    G.DrawImage(My.Resources.Taskbar_InactiveApp7, App2BtnRect)
                    G.DrawImage(My.Resources.AppPreviewInActive, App2BtnImgRect)


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

                    Dim AppBtnImgRect As New Rectangle(AppBtnRect.X + (AppBtnRect.Width - My.Resources.AppPreview.Width) / 2, AppBtnRect.Y + (AppBtnRect.Height - My.Resources.AppPreview.Height) / 2 - 1, My.Resources.AppPreview.Width, My.Resources.AppPreview.Height)
                    Dim App2BtnRect As New Rectangle(AppBtnRect.Right + 2, 0, 45, Height - 1)
                    Dim App2BtnRectInner As New Rectangle(App2BtnRect.X + 1, App2BtnRect.Y + 1, App2BtnRect.Width - 2, App2BtnRect.Height - 2)
                    Dim App2BtnImgRect As New Rectangle(App2BtnRect.X + (App2BtnRect.Width - My.Resources.AppPreviewInActive.Width) / 2, App2BtnRect.Y + (App2BtnRect.Height - My.Resources.AppPreviewInActive.Height) / 2 - 1, My.Resources.AppPreviewInActive.Width, My.Resources.AppPreviewInActive.Height)


                    G.DrawImage(StartORB, StartBtnRect)

                    If Transparency Then
                        G.FillRectangle(New SolidBrush(Color.FromArgb(100, Color.White)), AppBtnRect)
                        G.DrawRectangle(New Pen(Color.FromArgb(200, CCB(c, -0.5))), AppBtnRect)
                        G.DrawRectangle(New Pen(Color.FromArgb(215, Color.White)), AppBtnRectInner)
                    Else
                        G.FillRectangle(New SolidBrush(Color.FromArgb(255, CCB(bc, 0.5))), AppBtnRect)
                        G.FillRectangle(New SolidBrush(Color.FromArgb(255 * (Win7ColorBal / 100), CCB(c, 0.5))), AppBtnRect)
                        G.DrawRectangle(New Pen(Color.FromArgb(100, CCB(bc, -0.5))), AppBtnRect)
                        G.DrawRectangle(New Pen(Color.FromArgb(100 * (Win7ColorBal / 100), CCB(c, -0.5))), AppBtnRect)
                    End If

                    G.DrawImage(My.Resources.AppPreview, AppBtnImgRect)

                    If Transparency Then
                        G.FillRectangle(New SolidBrush(Color.FromArgb(50, Color.White)), App2BtnRect)
                        G.DrawRectangle(New Pen(Color.FromArgb(100, CCB(c, -0.5))), App2BtnRect)
                        G.DrawRectangle(New Pen(Color.FromArgb(100, Color.White)), App2BtnRectInner)
                    Else
                        G.FillRectangle(New SolidBrush(Color.FromArgb(255, ControlPaint.Light(bc, 0.1))), App2BtnRect)
                        G.FillRectangle(New SolidBrush(Color.FromArgb(255 * (Win7ColorBal / 100), ControlPaint.Light(c, 0.1))), App2BtnRect)
                        G.DrawRectangle(New Pen(Color.FromArgb(100, ControlPaint.Dark(bc, 0.1))), App2BtnRect)
                        G.DrawRectangle(New Pen(Color.FromArgb(100 * (Win7ColorBal / 100), ControlPaint.Dark(c, 0.1))), App2BtnRect)
                    End If

                    G.DrawImage(My.Resources.AppPreviewInActive, App2BtnImgRect)

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
            adaptedBack = My.Application.Wallpaper.Clone(Bounds, My.Application.Wallpaper.PixelFormat)
        Catch : End Try

        Try : If Transparency Then
                If UseItAsTaskbar_Version = TaskbarVersion.Seven Then
                    adaptedBackBlurred = BlurBitmap(New Bitmap(adaptedBack), 1)
                End If
            End If
        Catch : End Try
    End Sub

    Sub BlurBack()
        Try : If Transparency Then
                If UseItAsTaskbar_Version <> TaskbarVersion.Seven And UseItAsTaskbar_Version <> TaskbarVersion.Eight Then
                    adaptedBackBlurred = BlurBitmap(New Bitmap(adaptedBack), BlurPower)
                End If
            End If
        Catch : End Try
    End Sub

    Sub NoiseBack()
        Try
            If Transparency Then
                If UseItAsTaskbar_Version = TaskbarVersion.Eleven Or UseItAsTaskbar_Version = TaskbarVersion.Ten Then
                    Noise = New TextureBrush(FadeBitmap(My.Resources.GaussianBlur, NoisePower))
                ElseIf UseItAsTaskbar_Version = TaskbarVersion.Seven Then
                    Try : Noise7 = FadeBitmap(My.Resources.AeroGlass, NoisePower / 100) : Catch : End Try
                    Try : Noise7Start = FadeBitmap(My.Resources.Start7Glass, NoisePower / 100) : Catch : End Try
                ElseIf UseItAsTaskbar_Version = TaskbarVersion.Eight Then
                    Try : Noise7 = FadeBitmap(My.Resources.AeroGlass, NoisePower / 100) : Catch : End Try
                    Try : Noise7Start = FadeBitmap(My.Resources.Start7Glass, NoisePower / 100) : Catch : End Try
                End If

            End If
        Catch : End Try
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

                If Win7 Then
                    Try : Noise7 = FadeBitmap(My.Resources.AeroGlass, Win7Noise / 100) : Catch : End Try
                End If

                NotifyWin7NoiseChanged(_Win7Noise)
            End If
        End Set
    End Property

    Sub New()
        Padding = New Padding(2, 2, 2, 2)
    End Sub

    Public Property Radius As Integer = 5
    Public Property AccentColor_Active As Color = Color.FromArgb(0, 120, 212)
    Public Property AccentColor_Inactive As Color = Color.FromArgb(32, 32, 32)
    Public Property AccentColor2_Active As Color = Color.FromArgb(0, 120, 212)
    Public Property AccentColor2_Inactive As Color = Color.FromArgb(32, 32, 32)
    Public Property Active As Boolean = True
    Public Property RoundedCorners As Boolean = False
    Public Property Win7 As Boolean = False
    Public Property Win8 As Boolean = False
    Public Property Win8Lite As Boolean = False
    Public Property Win7Aero As Boolean = False
    Public Property Win7AeroOpaque As Boolean = False
    Public Property Win7Basic As Boolean = False
    Public Property Win7Alpha As Integer = 100
    Public Property Win7ColorBal As Integer = 100
    Public Property Win7GlowBal As Integer = 100

    Dim AdaptedBack, AdaptedBackBlurred As Bitmap

    Dim Noise7 As Bitmap = My.Resources.AeroGlass

    Protected Overrides Sub OnPaint(e As System.Windows.Forms.PaintEventArgs)
        Dim G As Graphics = e.Graphics
        G.SmoothingMode = SmoothingMode.AntiAlias
        G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit
        DoubleBuffered = True

        Dim Rect As New Rectangle(0, 0, Width - 1, Height - 1)
        Dim RectBK As New Rectangle(0, 0, Width, Height)
        Dim TitlebarRect As New Rectangle(0, 0, Width - 1, 23)
        Dim IconRect As New Rectangle(4, 4, 15, 15)
        Dim LabelRect As New Rectangle(21, 1, TitlebarRect.Width - 21, TitlebarRect.Height)
        Dim LabelRect8 As New Rectangle(0, 2, TitlebarRect.Width - 1, TitlebarRect.Height - 3)
        Dim XRect As New Rectangle(Rect.Right - 17, 1, 17, TitlebarRect.Height)

        Dim RectClip As Rectangle = Bounds
        G.Clear(Color.Transparent)

        If Not Win7 And Not Win8 Then
            If RoundedCorners Then
                Try
                    G.DrawImage(AdaptedBack, RectBK)
                Catch
                End Try

                If DarkMode Then
                    FillRect(G, New SolidBrush(Color.FromArgb(20, 20, 20)), Rect, Radius, True)
                Else
                    FillRect(G, New SolidBrush(Color.FromArgb(240, 240, 240)), Rect, Radius, True)
                End If

                If AccentColor_Enabled Then
                    If Active Then
                        DrawRect(G, New Pen(Color.FromArgb(150, AccentColor_Active)), Rect, Radius, True)
                    Else
                        DrawRect(G, New Pen(Color.FromArgb(150, AccentColor_Inactive)), Rect, Radius, True)
                    End If
                Else
                    If DarkMode Then
                        DrawRect(G, New Pen(Color.FromArgb(100, 90, 90, 90)), Rect, Radius, True)
                    Else
                        DrawRect(G, New Pen(Color.FromArgb(100, 220, 220, 220)), Rect, Radius, True)
                    End If
                End If

                If AccentColor_Enabled Then
                    If Active Then
                        FillSemiRect(G, New SolidBrush(AccentColor_Active), TitlebarRect, Radius)
                    Else
                        FillSemiRect(G, New SolidBrush(AccentColor_Inactive), TitlebarRect, Radius)
                    End If
                Else
                    FillSemiRect(G, Brushes.White, TitlebarRect, Radius)
                End If

            Else
                If DarkMode Then
                    G.FillRectangle(New SolidBrush(Color.FromArgb(20, 20, 20)), Rect)
                Else
                    G.FillRectangle(New SolidBrush(Color.FromArgb(240, 240, 240)), Rect)
                End If

                If AccentColor_Enabled Then
                    If Active Then
                        G.DrawRectangle(New Pen(Color.FromArgb(150, AccentColor_Active)), Rect)
                    Else
                        G.DrawRectangle(New Pen(Color.FromArgb(150, AccentColor_Inactive)), Rect)
                    End If
                Else
                    If DarkMode Then
                        G.DrawRectangle(New Pen(Color.FromArgb(100, 90, 90, 90)), Rect)
                    Else
                        G.DrawRectangle(New Pen(Color.FromArgb(100, 220, 220, 220)), Rect)
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
            End If
        Else
            If Win7 Then
                Dim Rect7 As New Rectangle(0, 0, Width, Height)
                Dim InnerWindow_1 As New Rectangle(6, 22, Width - 12, Height - 22 - 7)
                Dim InnerWindow_2 As New Rectangle(InnerWindow_1.X + 1, InnerWindow_1.Y + 1, InnerWindow_1.Width - 2, InnerWindow_1.Height - 2)
                Dim RectSide1 As New Rectangle(1, InnerWindow_1.Y, 6, InnerWindow_1.Height)
                Dim RectSide2 As New Rectangle(InnerWindow_2.Right - 1, RectSide1.Y, 6, InnerWindow_1.Height)

                If Not Win7Basic Then

                    G.DrawImage(AdaptedBack, Rect7)

                    Dim Radius As Integer = 3

                    If Not Win7AeroOpaque Then
                        Dim bk As Bitmap = AdaptedBackBlurred

                        Dim alpha As Single = 1 - Win7Alpha / 100   'ColorBlurBalance
                        Dim ColBal As Single = Win7ColorBal / 100   'ColorBalance
                        Dim GlowBal As Single = Win7GlowBal / 100   'AfterGlowBalance
                        Dim Color1 As Color = If(Active, AccentColor_Active, AccentColor_Inactive)
                        Dim Color2 As Color = If(Active, AccentColor2_Active, AccentColor2_Inactive)

                        DrawAero(G, Rect7, bk, Color1, ColBal, Color2, GlowBal, alpha, Radius)

                    Else
                        FillRect(G, New SolidBrush(Color.White), Rect7, Radius, True)
                        FillRect(G, New SolidBrush(Color.FromArgb(255 * Win7Alpha / 100, If(Active, AccentColor_Active, AccentColor_Inactive))), Rect7, Radius, True)
                    End If

                    G.DrawImage(My.Resources.Win7Sides, RectSide1)
                    G.DrawImage(My.Resources.Win7Sides, RectSide2)

                    FillImg(G, Noise7.Clone(Bounds, PixelFormat.Format32bppArgb), Rect7, Radius, True)

                    Dim closeBtn As Image = If(Active, My.Resources.Win7_Close_Active, My.Resources.Win7_Close_inactive)
                    G.DrawImage(closeBtn, New Rectangle(Width - closeBtn.Width - 5, 0, closeBtn.Width, closeBtn.Height))

                    DrawRect(G, New Drawing.Pen(Color.FromArgb(255 - 255 * Win7Alpha / 300, ControlPaint.Dark(BackColor, 0.2))), Rect, 3, True)

                    DrawRect(G, New Drawing.Pen(Color.FromArgb(255 - 255 * Win7Alpha / 300, ControlPaint.Light(BackColor, 0.2))), InnerWindow_1, 1, True)
                    FillRect(G, New SolidBrush(Color.White), InnerWindow_1, 1, True)
                    DrawRect(G, New Drawing.Pen(Color.FromArgb(255 - 255 * Win7Alpha / 300, ControlPaint.Dark(BackColor, 0.2))), InnerWindow_2, 1, True)

                Else
                    G.DrawImage(AdaptedBack, Rect7)

                    If Active Then
                        G.DrawImage(My.Resources.Win7BasicActive, New Point(0, 0))
                    Else
                        G.DrawImage(My.Resources.Win7BasicInactive, New Point(0, 0))
                    End If
                End If

            ElseIf Win8 Then
                Dim Rect8 As New Rectangle(0, 0, Width - 1, Height - 1)
                Dim InnerWindow As New Rectangle(5, 22, Width - 10, Height - 22 - 7)
                Dim CloseRect As New Rectangle(Width - 35, 0, 30, 15)
                Dim CloseRectLbl As New Rectangle(Width - 34, 1, 30, 15)

                Dim InC As Color = If(Not Win8Lite, Color.FromArgb(235, 235, 235), Color.FromArgb((Win7ColorBal / 100) * 255, CCB(AccentColor_Active, 0.8)))

                Dim c As Color = If(Active, Color.FromArgb((Win7ColorBal / 100) * 255, AccentColor_Active), InC)

                Dim bc As Color = Color.FromArgb(217, 217, 217)

                G.FillRectangle(New SolidBrush(bc), Rect8)
                G.FillRectangle(New SolidBrush(c), Rect8)

                G.FillRectangle(New SolidBrush(Color.White), InnerWindow)

                If Not Win8Lite Then
                    G.DrawRectangle(New Pen(ControlPaint.Dark(bc, 0.1)), InnerWindow)
                    G.DrawRectangle(New Pen(Color.FromArgb((Win7ColorBal / 100) * 255, ControlPaint.Dark(c, 0.1))), InnerWindow)

                    G.FillRectangle(New SolidBrush(If(Active, Color.FromArgb(199, 80, 80), Color.FromArgb(188, 188, 188))), CloseRect)
                    G.DrawString("r", New Font("Marlett", 6.35, FontStyle.Regular), New SolidBrush(Color.White), CloseRectLbl, StringAligner(ContentAlignment.MiddleCenter))

                    G.DrawRectangle(New Pen(ControlPaint.Dark(bc, 0.2)), Rect8)
                    G.DrawRectangle(New Pen(Color.FromArgb((Win7ColorBal / 100) * 255, ControlPaint.Dark(c, 0.2))), Rect8)
                Else
                    G.DrawRectangle(New Pen(Color.FromArgb(177, 173, 150)), InnerWindow)
                    G.FillRectangle(New SolidBrush(If(Active, Color.FromArgb(195, 90, 80), Color.Transparent)), CloseRect)
                    G.DrawRectangle(New Pen(If(Active, Color.FromArgb(92, 58, 55), Color.FromArgb(93, 96, 102))), CloseRect)
                    G.DrawString("r", New Font("Marlett", 6.35, FontStyle.Regular), New SolidBrush(Color.Black), CloseRectLbl, StringAligner(ContentAlignment.MiddleCenter))
                    G.DrawRectangle(New Drawing.Pen(Color.FromArgb(47, 48, 51)), Rect8)
                End If
            End If
        End If

        Dim ForeColorX As Color

        If AccentColor_Enabled Then
            If Active Then
                ForeColorX = If(IsColorDark(AccentColor_Active), Color.White, Color.Black)
            Else
                ForeColorX = If(IsColorDark(AccentColor_Inactive), Color.FromArgb(115, 115, 115), Color.Black)
            End If
        Else
            If Active Then
                ForeColorX = Color.Black
            Else
                ForeColorX = Color.FromArgb(115, 115, 115)
            End If
        End If


        G.DrawImage(If(Active, My.Resources.AppPreview, My.Resources.AppPreviewInActive), IconRect)

        If Not Win7 And Not Win8 Then
            G.DrawString(Text, New Font("Segoe UI", 8, FontStyle.Regular), New SolidBrush(ForeColorX), LabelRect, StringAligner(ContentAlignment.MiddleLeft))
            G.DrawString("", New Font("Segoe MDL2 Assets", 6, FontStyle.Regular), New SolidBrush(ForeColorX), XRect, StringAligner(ContentAlignment.MiddleLeft))
        Else
            If Win7 Then
                If Not Win7Basic Then
                    Font = New Font("Segoe UI", 8, FontStyle.Regular)
                    Dim LabelRectModified As Rectangle = LabelRect
                    LabelRectModified.X -= 2
                    LabelRectModified.Y -= 2
                    GlowString(G, 1, Me, Color.Black, Color.FromArgb(120, Color.White), LabelRectModified, StringAligner(ContentAlignment.MiddleLeft))
                Else
                    G.DrawString(Text, New Font("Segoe UI", 8, FontStyle.Regular), New SolidBrush(Color.Black), LabelRect, StringAligner(ContentAlignment.MiddleLeft))
                End If
            ElseIf Win8 Then

                If Not Win8Lite Then
                    G.DrawString(Text, New Font("Segoe UI", 10, FontStyle.Regular), New SolidBrush(Color.Black), LabelRect8, StringAligner(ContentAlignment.MiddleCenter))
                Else
                    If Active Then
                        G.DrawString(Text, New Font("Segoe UI", 10, FontStyle.Regular), New SolidBrush(MainFrm.CP.Win32UI_TitleText), LabelRect8, StringAligner(ContentAlignment.MiddleCenter))
                    Else
                        G.DrawString(Text, New Font("Segoe UI", 10, FontStyle.Regular), New SolidBrush(MainFrm.CP.Win32UI_InactiveTitleText), LabelRect8, StringAligner(ContentAlignment.MiddleCenter))
                    End If
                End If

            End If
        End If


    End Sub

    Public Sub GlowString(ByVal G As Graphics, ByVal GlowSize As Integer, ByVal Ctrl As Control, ByVal [ForeColor] As Color, ByVal GlowColor As Color, ByVal Rect As Rectangle, ByVal FormatX As StringFormat)
        Dim bm As Bitmap = New Bitmap(CInt(Ctrl.Width / 5), CInt(Ctrl.Height / 5))
        Dim g2 As Graphics = Graphics.FromImage(bm)
        Dim pth As GraphicsPath = New GraphicsPath()
        pth.AddString(Ctrl.Text, Ctrl.Font.FontFamily, Ctrl.Font.Style, Ctrl.Font.Size + 3, Rect, FormatX)
        Dim mx As Matrix = New Matrix(1.0F / 5, 0, 0, 1.0F / 5, -(1.0F / 5), -(1.0F / 5))
        g2.SmoothingMode = SmoothingMode.AntiAlias
        g2.Transform = mx
        Dim p As Pen = New Pen(GlowColor, GlowSize)
        g2.DrawPath(p, pth)
        g2.FillPath(New SolidBrush(GlowColor), pth)
        G.InterpolationMode = InterpolationMode.HighQualityBicubic
        G.DrawImage(bm, Ctrl.ClientRectangle, 0, 0, bm.Width, bm.Height, GraphicsUnit.Pixel)
        G.FillPath(New SolidBrush([ForeColor]), pth)
        g2.Dispose()
        pth.Dispose()
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
        End If

        ProcessBack()
    End Sub

    Sub ProcessBack()
        If Win7 Or (Not Win7 And AdaptedBack Is Nothing) Then
            Try : AdaptedBack = My.Application.Wallpaper.Clone(Bounds, My.Application.Wallpaper.PixelFormat) : Catch : End Try
            Try : AdaptedBackBlurred = BlurBitmap(New Bitmap(AdaptedBack), 1) : Catch : End Try
            Try : Noise7 = FadeBitmap(My.Resources.AeroGlass, Win7Noise / 100) : Catch : End Try
        Else
            Try : AdaptedBack = My.Application.Wallpaper.Clone(Bounds, My.Application.Wallpaper.PixelFormat) : Catch : End Try
        End If
    End Sub
End Class

<DefaultEvent("Scroll")>
Class XenonTrackbar
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

    Private ButtonSize As Integer = 0
    Private ThumbSize As Integer = 35 ' 14 minimum
    Private LSA As Rectangle
    Private RSA As Rectangle
    Private Shaft As Rectangle
    Private Thumb As Rectangle
    Private ThumbDown As Boolean
    Private Circle As Rectangle
    Dim Colors As XenonColorPalette
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

            If Not State = MouseState.Over Or State = MouseState.Down Then
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

    Public State As MouseState = MouseState.None

    Sub New()
        SetStyle(DirectCast(139286, ControlStyles), True)
        SetStyle(ControlStyles.Selectable, False)
        Height = 19
        Colors = New XenonColorPalette(Me)
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

        Dim C As Color = Colors.Color_Core

        Dim middleRect As New Rectangle(0, (Height - (Height * 0.25)) / 2, Width - 1, Height * 0.25)

        FillRect(G, New SolidBrush(c_back), middleRect)

        Circle = New Rectangle((Value / Maximum) * Shaft.Width, 0, Height - 1, Height - 1)

        With Thumb
            FillRect(G, New SolidBrush(C), New Rectangle(.X + 1, middleRect.Y, Circle.Left + Circle.Width / 2, middleRect.Height))
        End With

        G.FillRectangle(New SolidBrush(BackColor), New Rectangle(-1, 0, 4, Height))

        G.FillRectangle(New SolidBrush(BackColor), New Rectangle(Width - 4, 0, 4, Height))

        G.FillEllipse(New SolidBrush(Colors.Color_Parent_Hover), Circle)

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

            Value = Math.Min((e.X / Width) * Maximum, _Maximum)
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
        If e.Delta > 0 Then
            If e.Delta <= -240 Then Value = Value + LargeChange Else Value = Value + SmallChange
        Else
            If e.Delta >= 240 Then Value = Value - LargeChange Else Value = Value - SmallChange
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
            Colors = New XenonColorPalette(Me)
        Catch
        End Try
    End Sub

    Sub Loaded()
        _Shown = False
    End Sub

    Sub Showed()
        _Shown = True
        Colors = New XenonColorPalette(Me)
        Invalidate()
    End Sub

    Public Sub RefreshColorPalette()
        If _Shown Then
            Colors = New XenonColorPalette(Me)
            Invalidate()
        End If
    End Sub
End Class
#End Region