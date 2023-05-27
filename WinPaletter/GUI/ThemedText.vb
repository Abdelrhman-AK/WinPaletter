' ***************************************************
'  WindowsFormsAero
'  https://github.com/LorenzCK/WindowsFormsAero
'  http://windowsformsaero.codeplex.com
' 
'  Author: Lorenz Cuno Klopfenstein <lck@klopfenstein.net>
' ***************************************************

Imports System.Windows.Forms.VisualStyles
Imports System.Runtime.InteropServices
Imports WinPaletter.NativeMethods.GDI32
Imports WinPaletter.NativeMethods.UxTheme

''' <summary>
''' Renders themed text.
''' </summary>
''' <remarks>
''' Needs major reworking to be exposed as a public class.
''' </remarks>
Public Class ThemedText
    Implements IDisposable

    Private Shared _win32Black As Integer = ColorTranslator.ToWin32(Color.Black)

    Private Shared renderer As VisualStyleRenderer = New VisualStyleRenderer(VisualStyleElement.Window.Caption.Active)

    Public Sub New()

    End Sub

    Private _invalidated As Boolean = True

    Private _text As String = String.Empty

    Public Property Text As String
        Get
            Return _text
        End Get
        Set(ByVal value As String)
            If Not Equals(_text, value) Then _invalidated = True
            _text = value
        End Set
    End Property

    Private _font As Font = SystemFonts.CaptionFont

    Public Property Font As Font
        Get
            Return _font
        End Get
        Set(ByVal value As Font)
            If _font IsNot value Then _invalidated = True
            _font = value
        End Set
    End Property

    Private _padding As Padding = Padding.Empty

    Public Property Padding As Padding
        Get
            Return _padding
        End Get
        Set(ByVal value As Padding)
            If _padding <> value Then _invalidated = True
            _padding = value
        End Set
    End Property

    Private _win32Color As Integer = ColorTranslator.ToWin32(Color.Black)

    Public Property Color As Color
        Get
            Return ColorTranslator.FromWin32(_win32Black)
        End Get
        Set(ByVal value As Color)
            _invalidated = True
            _win32Color = ColorTranslator.ToWin32(value)
        End Set
    End Property

    Private _formatFlags As TextFormatFlags = TextFormatFlags.Default

    Public Property FormatFlags As TextFormatFlags
        Get
            Return _formatFlags
        End Get
        Set(ByVal value As TextFormatFlags)
            If _formatFlags <> value Then _invalidated = True
            _formatFlags = value
        End Set
    End Property

    Private _glowSize As Integer = 10

    Public Property GlowSize As Integer
        Get
            Return _glowSize
        End Get
        Set(ByVal value As Integer)
            If _glowSize <> value Then _invalidated = True
            _glowSize = value
        End Set
    End Property

    Private _glowEnabled As Boolean = True

    Public Property GlowEnabled As Boolean
        Get
            Return _glowEnabled
        End Get
        Set(ByVal value As Boolean)
            If _glowEnabled <> value Then _invalidated = True
            _glowEnabled = value
        End Set
    End Property

#Region "IDisposable Members"

    Protected Overrides Sub Finalize()
        Dispose()
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        If _textHdc <> IntPtr.Zero Then
            DeleteDC(_textHdc)
            _textHdc = IntPtr.Zero
        End If

        GC.SuppressFinalize(Me)
    End Sub

#End Region

    Public Sub Draw(ByVal g As Graphics, ByVal location As Point, ByVal size As Size)
        Draw(g, location.X, location.Y, size.Width, size.Height)
    End Sub

    Public Sub Draw(ByVal g As Graphics, ByVal rect As Rectangle)
        Draw(g, rect.X, rect.Y, rect.Width, rect.Height)
    End Sub

    Public Sub Draw(ByVal g As Graphics, ByVal x As Integer, ByVal y As Integer, ByVal width As Integer, ByVal height As Integer)
        Dim outputHdc = g.GetHdc()

        Dim sourceHdc = PrepareHdc(outputHdc, width, height)

        BitBlt(outputHdc, x, y, width, height, sourceHdc, 0, 0, BitBltOp.SRCCOPY)

        g.ReleaseHdc(outputHdc)
    End Sub

    Private _textHdc As IntPtr = IntPtr.Zero
    Private _dibSectionRef As IntPtr
    Private _lastHdcWidth As Integer = -1
    Private _lastHdcHeight As Integer = -1

    ''' <summary>
    ''' Ensures that a valid source HDC exists and has been rendered to.
    ''' </summary>
    Private Function PrepareHdc(ByVal outputHdc As IntPtr, ByVal width As Integer, ByVal height As Integer) As IntPtr
        If width = _lastHdcWidth AndAlso height = _lastHdcHeight AndAlso Not _invalidated Then Return _textHdc

        _lastHdcWidth = width
        _lastHdcHeight = height

        If _textHdc <> IntPtr.Zero Then
            DeleteObject(_dibSectionRef)
            DeleteDC(_textHdc)
        End If
        _textHdc = CreateCompatibleDC(outputHdc)

        ' Create a DIB-Bitmap on which to draw
        Dim info = New BitmapInfo() With {
                .biSize = Marshal.SizeOf(GetType(BitmapInfo)),
                .biWidth = width,
                .biHeight = -height, ' DIB use top-down ref system, thus we set negative height
                .biPlanes = 1,
                .biBitCount = 32,
                .biCompression = 0
            }
        _dibSectionRef = CreateDIBSection(outputHdc, info, 0, 0, IntPtr.Zero, 0)
        SelectObject(_textHdc, _dibSectionRef)

        ' Create the Font to use
        Dim hFont As IntPtr = Font.ToHfont()
        SelectObject(_textHdc, hFont)

        ' Prepare options
        Dim dttOpts = New DttOpts With {
                .dwSize = Marshal.SizeOf(GetType(DttOpts)),
                .dwFlags = DttOptsFlags.DTT_COMPOSITED Or DttOptsFlags.DTT_TEXTCOLOR,
                .crText = _win32Color
            }
        If _glowEnabled Then
            dttOpts.dwFlags = dttOpts.dwFlags Or DttOptsFlags.DTT_GLOWSIZE
            dttOpts.iGlowSize = _glowSize
        End If

        ' Set full bounds with padding
        Dim paddedBounds As Rect = New Rect(_padding.Left, _padding.Top, width - _padding.Right, height - _padding.Bottom)

        ' Draw
        Dim ret As Integer = DrawThemeTextEx(renderer.Handle, _textHdc, 0, 0, _text, -1, _formatFlags, paddedBounds, dttOpts)
        If ret <> 0 Then Marshal.ThrowExceptionForHR(ret)

        ' Clean up
        DeleteObject(hFont)

        Return _textHdc
    End Function

End Class



