Imports System.Drawing.Imaging
Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices
Imports WinPaletter.XenonCore

Public Module ColorsExtensions

    '''<summary>
    '''Reverse Color From RGB To BGR
    '''</summary>
    <Extension()>
    Public Function Reverse([Color] As Color) As Color
        Return Color.FromArgb([Color].B, [Color].G, [Color].R)
    End Function

    '''<summary>
    '''Return HEX Color As String From RGB
    '''</summary>
    <Extension()>
    Public Function HEX(ByVal [Color] As Color, Optional Alpha As Boolean = True, Optional Hash As Boolean = False) As String
        Dim S As String
        If Alpha Then
            S = String.Format("{0:X2}", Color.A, Color.R, Color.G, Color.B) & String.Format("{1:X2}", Color.A, Color.R, Color.G, Color.B) &
                String.Format("{2:X2}", Color.A, Color.R, Color.G, Color.B) & String.Format("{3:X2}", Color.A, Color.R, Color.G, Color.B)
        Else
            S = String.Format("{0:X2}{1:X2}{2:X2}", Color.R, Color.G, Color.B)
        End If
        If Hash Then S = "#" & S
        Return S
    End Function

    '''<summary>
    '''Return HSL_Structure From RGB Color
    '''</summary>
    <Extension()>
    Public Function HSL([Color] As Color) As HSL_Structure
        Dim _hsl As New HSL_Structure()

        Dim r As Single = ([Color].R / 255.0F)
        Dim g As Single = ([Color].G / 255.0F)
        Dim b As Single = ([Color].B / 255.0F)

        Dim min As Single = Math.Min(Math.Min(r, g), b)
        Dim max As Single = Math.Max(Math.Max(r, g), b)
        Dim delta As Single = max - min

        _hsl.L = (max + min) / 2

        If delta = 0 Then
            _hsl.H = 0
            _hsl.S = 0.0F
        Else
            _hsl.S = If((_hsl.L <= 0.5), (delta / (max + min)), (delta / (2 - max - min)))

            Dim hue As Single

            If r = max Then
                hue = ((g - b) / 6) / delta
            ElseIf g = max Then
                hue = (1.0F / 3) + ((b - r) / 6) / delta
            Else
                hue = (2.0F / 3) + ((r - g) / 6) / delta
            End If

            If hue < 0 Then
                hue += 1
            End If
            If hue > 1 Then
                hue -= 1
            End If

            _hsl.H = CInt(Math.Truncate(hue * 360))
        End If

        Return _hsl
    End Function

    '''<summary>
    '''Return HSL String in the format of H S% L% From RGB Color
    '''</summary>
    <Extension()>
    Public Function HSL_Text([Color] As Color) As String
        Return String.Format("{0} {1}% {2}%", [Color].HSL.H, Math.Round([Color].HSL.S * 100), Math.Round([Color].HSL.L * 100))
    End Function

    '''<summary>
    '''Get String in the format of R G B from a Color(R,G,B) or A R G B from a Color(A,R,G,B)
    '''</summary>
    <Extension()>
    Public Function Win32_RegColor([Color] As Color, Optional Alpha As Boolean = False) As String
        If Not Alpha Then
            Return String.Format("{0} {1} {2}", [Color].R, [Color].G, [Color].B)
        Else
            Return String.Format("{0} {1} {2} {3}", Color.A, Color.R, Color.G, Color.B)
        End If
    End Function

    '''<summary>
    '''Get Color As String in the format you choose
    '''</summary>
    <Extension()>
    Public Function ReturnFormat(Color As Color, Format As ColorFormat, Optional HexHash As Boolean = False, Optional Alpha As Boolean = False) As String
        Dim s As String = "Empty"

        If Color <> Color.FromArgb(0, 0, 0, 0) Then
            Select Case Format
                Case ColorFormat.HEX
                    s = Color.HEX(Alpha, HexHash)

                Case ColorFormat.RGB
                    s = Color.Win32_RegColor(Alpha)

                Case ColorFormat.HSL
                    s = Color.HSL_Text

                Case ColorFormat.Dec
                    s = Color.ToArgb

            End Select
        Else
            s = "Empty"
        End If

        Return s
    End Function

    '''<summary>
    '''Get Bitmap From Color
    '''</summary>
    <Extension()>
    Public Function ToBitmap([Color] As Color, [Size] As Size)
        Dim b As New Bitmap([Size].Width, [Size].Height)
        Dim g As Graphics = Graphics.FromImage(b)
        g.Clear([Color])
        g.Save()
        Return b
        g.Dispose()
        b.Dispose()
    End Function

    '''<summary>
    '''Return Color by blending two colors
    '''</summary>
    <Extension()>
    Public Function Blend(ByVal color As Color, ByVal backColor As Color, ByVal amount As Double) As Color
        If amount > 100 Then amount = 100
        If amount < 0 Then amount = 0

        Dim a As Byte = CByte((color.A * amount / 100 + backColor.A * (amount / 100)) / 2)
        Dim r As Byte = CByte((color.R * amount / 100 + backColor.R * (amount / 100)) / 2)
        Dim g As Byte = CByte((color.G * amount / 100 + backColor.G * (amount / 100)) / 2)
        Dim b As Byte = CByte((color.B * amount / 100 + backColor.B * (amount / 100)) / 2)
        Return Color.FromArgb(a, r, g, b)
    End Function

    '''<summary>
    '''Get Darker Color From a Color
    '''</summary>
    <Extension()>
    Public Function Dark([Color] As Color) As Color
        Return ControlPaint.Dark([Color])
    End Function

    '''<summary>
    '''Get Darker Color From a Color, with a given percentage
    '''</summary>
    <Extension()>
    Public Function Dark([Color] As Color, percentage As Single) As Color
        Return ControlPaint.Dark([Color], percentage)
    End Function

    '''<summary>
    '''Get Darkest Color From a Color
    '''</summary>
    <Extension()>
    Public Function DarkDark([Color] As Color) As Color
        Return ControlPaint.DarkDark([Color])
    End Function

    '''<summary>
    '''Get Lighter Color From a Color
    '''</summary>
    <Extension()>
    Public Function Light([Color] As Color) As Color
        Return ControlPaint.Light([Color])
    End Function

    '''<summary>
    '''Get Lighter Color From a Color, with a given percentage
    '''</summary>
    <Extension()>
    Public Function Light([Color] As Color, percentage As Single) As Color
        Return ControlPaint.Light([Color], percentage)
    End Function

    '''<summary>
    '''Get Lightest Color From a Color
    '''</summary>
    <Extension()>
    Public Function LightLight([Color] As Color) As Color
        Return ControlPaint.LightLight([Color])
    End Function

    '''<summary>
    '''Get Inverted Color From a Color
    '''</summary>
    <Extension()>
    Public Function Invert(ByVal [Color] As Color) As Color
        Return Color.FromArgb([Color].A, 255 - [Color].R, 255 - [Color].G, 255 - [Color].B)
    End Function

    '''<summary>
    '''Get If the color is dark or not
    '''</summary>
    <Extension()>
    Public Function IsDark(ByVal [Color] As Color) As Boolean
        Return Not ([Color].R * 0.2126 + [Color].G * 0.7152 + [Color].B * 0.0722 > 255 / 2)
    End Function

    Structure HSL_Structure
        Private _h As Integer
        Private _s As Single
        Private _l As Single

        Public Sub New(h As Integer, s As Single, l As Single)
            Me._h = h
            Me._s = s
            Me._l = l
        End Sub

        Public Property H() As Integer
            Get
                Return Me._h
            End Get
            Set(value As Integer)
                Me._h = value
            End Set
        End Property

        Public Property S() As Single
            Get
                Return Me._s
            End Get
            Set(value As Single)
                Me._s = value
            End Set
        End Property

        Public Property L() As Single
            Get
                Return Me._l
            End Get
            Set(value As Single)
                Me._l = value
            End Set
        End Property

        Public Overloads Function Equals(hsl As HSL_Structure) As Boolean
            Return (Me.H = hsl.H) AndAlso (Me.S = hsl.S) AndAlso (Me.L = hsl.L)
        End Function
    End Structure

End Module

Public Module BooleanExtensions

    '''<summary>
    '''Return Integer, If True; 1, If False; 0
    '''</summary>
    <Extension()>
    Public Function ToInteger([Boolean] As Boolean) As Integer
        Return If([Boolean], 1, 0)
    End Function

End Module

Public Module IntegerExtensions

    '''<summary>
    '''Return Boolean by comparison to 1 (Default)
    '''</summary>
    <Extension()>
    Public Function ToBoolean([Integer] As Integer, Optional CompareBy As Integer = 1) As Boolean
        Return [Integer] = CompareBy
    End Function

End Module

Public Module StringExtensions

    '''<summary>
    '''Return Color From HEX String
    '''</summary>
    <Extension()>
    Public Function FromHEXToColor([String] As String, Optional Alpha As Boolean = False) As Color

        Try
            If Not Alpha Then
                Return Color.FromArgb(255, Color.FromArgb(Convert.ToInt32([String].Replace("#", ""), 16)))
            Else
                Return Color.FromArgb(Convert.ToInt32([String].Replace("#", ""), 16))
            End If
        Catch
            Return Color.Empty
        End Try

    End Function

End Module

Public Module BitmapExtensions
    '''<summary>
    '''Return Most Used Color From Bitmap
    '''</summary>
    <Extension()>
    Public Function AverageColor(ByVal [Bitmap] As Bitmap) As Color
        Try
            Dim bmp As Bitmap = [Bitmap]
            Dim totalR As Integer = 0
            Dim totalG As Integer = 0
            Dim totalB As Integer = 0

            Try
                If bmp IsNot Nothing Then
                    For x As Integer = 0 To bmp.Width - 1
                        For y As Integer = 0 To bmp.Height - 1
                            Dim pixel As Color = bmp.GetPixel(x, y)
                            totalR += pixel.R
                            totalG += pixel.G
                            totalB += pixel.B
                        Next
                    Next
                End If
            Catch

            End Try

            If bmp IsNot Nothing Then
                Dim totalPixels As Integer = bmp.Height * bmp.Width
                Dim averageR As Integer = totalR \ totalPixels
                Dim averageg As Integer = totalG \ totalPixels
                Dim averageb As Integer = totalB \ totalPixels
                Return Color.FromArgb(averageR, averageg, averageb)
            Else
                Return Color.FromArgb(80, 80, 80)
            End If
        Catch
            Return Color.Empty
        End Try
    End Function

    '''<summary>
    '''Return Most Used Color From Image
    '''</summary>
    <Extension()>
    Public Function AverageColor(ByVal [Image] As Image) As Color
        Return AverageColor(DirectCast([Image], Bitmap))
    End Function

    '''<summary>
    '''Return Blurred Bitmap
    '''</summary>
    <Extension()>
    Public Function Blur(ByRef image As Image, Optional ByVal BlurForce As Integer = 2) As Bitmap
        Dim g As Graphics = Graphics.FromImage(image)

        g.SmoothingMode = Drawing2D.SmoothingMode.HighQuality

        Dim att As New ImageAttributes
        Dim m As New ColorMatrix With {.Matrix33 = 0.4F}
        att.SetColorMatrix(m)

        BlurForce += 1

        For x = -BlurForce To BlurForce Step 0.5
            g.DrawImage(image, New Rectangle(x, 0, image.Width - 1, image.Height - 1), 0, 0, image.Width - 1, image.Height - 1, GraphicsUnit.Pixel, att)
        Next

        For y = -BlurForce To BlurForce Step 0.5
            g.DrawImage(image, New Rectangle(0, y, image.Width - 1, image.Height - 1), 0, 0, image.Width - 1, image.Height - 1, GraphicsUnit.Pixel, att)
        Next

        Return image
        att.Dispose()
        g.Dispose()
    End Function

    '''<summary>
    '''Return Blurred Bitmap
    '''</summary>
    <Extension()>
    Public Function Blur(ByRef Bitmap As Bitmap, Optional ByVal BlurForce As Integer = 2) As Bitmap
        Return Blur(DirectCast(Bitmap, Image), BlurForce)
    End Function

    '''<summary>
    '''Return Noised Bitmap (Noise of Windows 10 Acrylic)
    '''</summary>
    <Extension()>
    Public Function Noise(bmp As Bitmap, NoiseMode As NoiseMode, opacity As Single) As Bitmap
        Try
            Dim g As Graphics = Graphics.FromImage(bmp)

            If NoiseMode = NoiseMode.Acrylic Then
                Dim br As TextureBrush
                br = New TextureBrush(FadeBitmap(My.Resources.GaussianBlur, opacity))
                g.FillRectangle(br, New Rectangle(0, 0, bmp.Width, bmp.Height))
            ElseIf NoiseMode = NoiseMode.Aero Then
                g.DrawImage(FadeBitmap(My.Resources.AeroGlass, opacity), New Rectangle(0, 0, bmp.Width, bmp.Height))
            End If

            g.Save()
            Return bmp
            g.Dispose()
            bmp.Dispose()
        Catch
            Return Nothing
        End Try
    End Function
    Enum NoiseMode
        Aero
        Acrylic
    End Enum

    '''<summary>
    '''Return Bitmap Tinted by a color
    '''</summary>
    <Extension()>
    Public Function Tint(ByVal sourceBitmap As Bitmap, [Color] As Color) As Bitmap
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


    '''<summary>
    '''Return Bitmap in Grayscale
    '''</summary>
    <Extension()>
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

End Module