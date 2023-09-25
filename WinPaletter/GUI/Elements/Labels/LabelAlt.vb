Imports System.ComponentModel
Imports System.Drawing.Drawing2D

Namespace UI.WP

    <Description("Label can be drawn on glass (Aero/Acrylic/Mica) for WinPaletter UI")> Public Class LabelAlt : Inherits Label

#Region "Variables"

        Private _textHdc As IntPtr = IntPtr.Zero
        Private _dibSectionRef As IntPtr

#End Region

#Region "Properties"

        Public Property DrawOnGlass As Boolean = False

#End Region

#Region "Subs/Functions"

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

#End Region

        Protected Overrides Sub OnPaint(e As PaintEventArgs)
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality
            e.Graphics.TextRenderingHint = My.RenderingHint
            Using br As New SolidBrush(BackColor) : e.Graphics.FillRectangle(br, New Rectangle(0, 0, Width, Height)) : End Using

            Try
                If DesignMode OrElse Not DrawOnGlass Then
                    Using br As New SolidBrush(ForeColor) : e.Graphics.DrawString(Text, Font, br, New Rectangle(0, 0, Width, Height), TextAlign.ToStringFormat) : End Using

                ElseIf Not DesignMode And DrawOnGlass Then
                    Dim outputHdc = e.Graphics.GetHdc()
                    Dim sourceHdc = PrepareHdc(outputHdc, Width, Height)
                    NativeMethods.GDI32.BitBlt(outputHdc, 0, 0, Width, Height, sourceHdc, 0, 0, NativeMethods.GDI32.BitBltOp.SRCCOPY)
                    e.Graphics.ReleaseHdc(outputHdc)

                End If
            Catch
                Using br As New SolidBrush(ForeColor) : e.Graphics.DrawString(Text, Font, br, New Rectangle(0, 0, Width, Height), TextAlign.ToStringFormat) : End Using
            End Try
        End Sub

    End Class

End Namespace