Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.IO
Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices
Imports System.Xml

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
    Public Function ToHSL([Color] As Color) As HSL_Structure
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
    '''Return RGB Color From HSL_Structure
    '''</summary>
    <Extension()>
    Public Function ToRGB(hsl As HSL_Structure) As Color
        Dim r As Byte = 0
        Dim g As Byte = 0
        Dim b As Byte = 0

        If hsl.S = 0 Then
            r = CByte(Math.Truncate(hsl.L * 255))
            g = CByte(Math.Truncate(hsl.L * 255))
            b = CByte(Math.Truncate(hsl.L * 255))
        Else
            Dim v1 As Single, v2 As Single
            Dim hue As Single = CSng(hsl.H) / 360

            v2 = If((hsl.L < 0.5), (hsl.L * (1 + hsl.S)), ((hsl.L + hsl.S) - (hsl.L * hsl.S)))
            v1 = 2 * hsl.L - v2

            r = CByte(Math.Truncate(255 * HueToRGB(v1, v2, hue + (1.0F / 3))))
            g = CByte(Math.Truncate(255 * HueToRGB(v1, v2, hue)))
            b = CByte(Math.Truncate(255 * HueToRGB(v1, v2, hue - (1.0F / 3))))
        End If

        Return Color.FromArgb(r, g, b)
    End Function

    Private Function HueToRGB(v1 As Single, v2 As Single, vH As Single) As Single
        If vH < 0 Then
            vH += 1
        End If

        If vH > 1 Then
            vH -= 1
        End If

        If (6 * vH) < 1 Then
            Return (v1 + (v2 - v1) * 6 * vH)
        End If

        If (2 * vH) < 1 Then
            Return v2
        End If

        If (3 * vH) < 2 Then
            Return (v1 + (v2 - v1) * ((2.0F / 3) - vH) * 6)
        End If

        Return v1
    End Function

    '''<summary>
    '''Return HSL String in the format of H S% L% From RGB Color
    '''</summary>
    <Extension()>
    Public Function HSL_Text([Color] As Color) As String
        Return String.Format("{0} {1}% {2}%", [Color].ToHSL.H, Math.Round([Color].ToHSL.S * 100), Math.Round([Color].ToHSL.L * 100))
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
    '''Change Color Brightness
    '''</summary>
    <Extension()>
    Public Function CB(ByVal color As Color, ByVal correctionFactor As Single) As Color
        Dim red As Single = CSng(color.R)
        Dim green As Single = CSng(color.G)
        Dim blue As Single = CSng(color.B)

        If correctionFactor < 0 Then
            correctionFactor = 1 + correctionFactor
            red *= correctionFactor
            green *= correctionFactor
            blue *= correctionFactor
        Else
            red = (255 - red) * correctionFactor + red
            green = (255 - green) * correctionFactor + green
            blue = (255 - blue) * correctionFactor + blue
        End If
        Try
            Return Color.FromArgb(color.A, CInt(red), CInt(green), CInt(blue))
        Catch
        End Try
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
    Enum ColorFormat
        HEX
        RGB
        HSL
        Dec
    End Enum

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

    '''<summary>
    '''Return String in the format of xxxxxxxx, useful for registry handling
    '''</summary>
    <Extension()>
    Public Function To8Digits(int As Integer) As String
        If int.ToString.Count <= 8 Then
            Dim i As Integer = 8 - int.ToString.Count
            Dim s As String = ""
            For i = 1 To i
                s &= "0"
            Next
            s &= int
            Return s
        Else
            Return int.ToString
        End If

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

    '''<summary>
    '''Convert String to List (String should be multi-lines)
    '''</summary>
    <Extension()>
    Public Function CList(ByVal [String] As String) As List(Of String)
        Dim [List] As New List(Of String)
        [List].Clear()
        Using Reader As New StringReader([String])
            While Reader.Peek >= 0
                [List].Add(Reader.ReadLine)
            End While
            Reader.Close()
            Reader.Dispose()
        End Using
        Return [List]
    End Function

    '''<summary>
    '''Measure String by a certain font
    '''</summary>
    <Extension()>
    Public Function Measure(text As String, font As Font) As SizeF

        Try
            Dim TextBitmap As New Bitmap(1, 1)
            Dim TextGraphics As Graphics = Graphics.FromImage(TextBitmap)
            Return TextGraphics.MeasureString(text, font)
        Catch
        End Try

    End Function
End Module

Public Module ListOfStringExtensions

    '''<summary>
    '''Deduplicate list of string
    '''</summary>
    <Extension()>
    Function DeDuplicate(ByVal [List] As List(Of String)) As List(Of String)
        Dim Result As New List(Of String)

        Dim Exist As Boolean = False
        For Each ElementString As String In [List]
            Exist = False
            For Each ElementStringInResult As String In Result
                If ElementString = ElementStringInResult Then
                    Exist = True
                    Exit For
                End If
            Next
            If Not Exist Then
                Result.Add(ElementString)
            End If
        Next

        Return Result
    End Function

    '''<summary>
    '''Return String from List, each item is in a separate line
    '''</summary>
    <Extension()>
    Public Function CString([List] As List(Of String)) As String
        Return String.Join(vbCrLf, [List].ToArray)
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
                br = New TextureBrush(Fade(My.Resources.GaussianBlur, opacity))
                g.FillRectangle(br, New Rectangle(0, 0, bmp.Width, bmp.Height))
            ElseIf NoiseMode = NoiseMode.Aero Then
                g.DrawImage(Fade(My.Resources.AeroGlass, opacity), New Rectangle(0, 0, bmp.Width, bmp.Height))
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
    '''Replace Color in Bitmap (Pixels) by color you choose
    '''</summary>
    <Extension()>
    Public Function ReplaceColor(ByVal inputImage As Bitmap, ByVal oldColor As Color, ByVal NewColor As Color) As Bitmap
        Dim outputImage As Bitmap = New Bitmap(inputImage.Width, inputImage.Height)
        Dim G As Graphics = Graphics.FromImage(outputImage)


        For y As Int32 = 0 To inputImage.Height - 1

            For x As Int32 = 0 To inputImage.Width - 1
                Dim PixelColor As Color = inputImage.GetPixel(x, y)

                If PixelColor = oldColor Then
                    outputImage.SetPixel(x, y, NewColor)
                Else
                    outputImage.SetPixel(x, y, PixelColor)
                End If

            Next
        Next

        G.DrawImage(outputImage, 0, 0)
        G.Dispose()
        Return outputImage
        outputImage.Dispose()

    End Function

    '''<summary>
    '''Replace Color in Image (Pixels) by color you choose
    '''</summary>
    <Extension()>
    Public Function ReplaceColor(ByVal inputImage As Image, ByVal oldColor As Color, ByVal NewColor As Color) As Image
        Return ReplaceColor(DirectCast(inputImage, Bitmap), oldColor, NewColor)
    End Function

    '''<summary>
    '''Return Bitmap filled in the scale of size you choose
    '''</summary>
    <Extension()>
    Public Function FillScale(ByVal Bitmap As Bitmap, Size As Size) As Bitmap
        Try
            Dim sourceWidth As Integer = Bitmap.Width
            Dim sourceHeight As Integer = Bitmap.Height
            Dim sourceX As Integer = 0
            Dim sourceY As Integer = 0
            Dim destX As Integer = 0
            Dim destY As Integer = 0
            Dim nPercent As Single = 0
            Dim nPercentW As Single = 0
            Dim nPercentH As Single = 0
            nPercentW = (CSng(Size.Width) / CSng(sourceWidth))
            nPercentH = (CSng(Size.Height) / CSng(sourceHeight))

            If nPercentH < nPercentW Then
                nPercent = nPercentH
                destX = System.Convert.ToInt16((Size.Width - (sourceWidth * nPercent)) / 2)
            Else
                nPercent = nPercentW
                destY = System.Convert.ToInt16((Size.Height - (sourceHeight * nPercent)) / 2)
            End If

            Dim destWidth As Integer = CInt((sourceWidth * nPercent))
            Dim destHeight As Integer = CInt((sourceHeight * nPercent))
            Dim bmPhoto As Bitmap = New Bitmap(Size.Width, Size.Height, PixelFormat.Format32bppArgb)
            bmPhoto.SetResolution(Bitmap.HorizontalResolution, Bitmap.VerticalResolution)
            Dim grPhoto As Graphics = Graphics.FromImage(bmPhoto)
            grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic
            grPhoto.DrawImage(Bitmap, New Rectangle(0, 0, destWidth, destHeight))
            grPhoto.Dispose()
            Dim bm As Bitmap = bmPhoto.Clone(New Rectangle(0, 0, destWidth, destHeight), PixelFormat.Format32bppArgb)
            Dim f As Single

            If nPercentH < nPercentW Then
                f = Size.Width - bm.Width
                bm = bm.Resize(Size.Width, Size.Height + f)
                bm = bm.Clone(New Rectangle(0, 1 / 3 * f, Size.Width, Size.Height), PixelFormat.Format32bppArgb)
            Else
                f = Size.Height - bm.Height
                bm = bm.Resize(Size.Width, Size.Height + f)
                bm = bm.Clone(New Rectangle(1 / 3 * f, 0, Size.Width, Size.Height), PixelFormat.Format32bppArgb)
            End If


            Return bm
        Catch
            Return Bitmap
        End Try
    End Function

    '''<summary>
    '''Return Image filled in the scale of size you choose
    '''</summary>
    <Extension()>
    Public Function FillScale(ByVal Image As Image, Size As Size) As Image
        Return FillScale(DirectCast(Image, Bitmap), Size)
    End Function

    '''<summary>
    '''Resize Bitmap in the size you choose
    '''</summary>
    <Extension()>
    Public Function Resize(bmSource As Bitmap, TargetWidth As Integer, TargetHeight As Integer) As Bitmap
        If bmSource Is Nothing Then
            Exit Function
        End If

        Dim bmDest As New Bitmap(TargetWidth, TargetHeight, PixelFormat.Format32bppArgb)

        Dim nSourceAspectRatio = bmSource.Width / bmSource.Height
        Dim nDestAspectRatio = bmDest.Width / bmDest.Height

        Dim NewX = 0
        Dim NewY = 0

        Using grDest = Graphics.FromImage(bmDest)
            With grDest
                .CompositingQuality = Drawing.Drawing2D.CompositingQuality.HighQuality
                .InterpolationMode = Drawing.Drawing2D.InterpolationMode.HighQualityBicubic
                .PixelOffsetMode = Drawing.Drawing2D.PixelOffsetMode.HighQuality
                .SmoothingMode = Drawing.Drawing2D.SmoothingMode.AntiAlias
                .CompositingMode = Drawing.Drawing2D.CompositingMode.SourceOver
                .DrawImage(bmSource, 0, 0, TargetWidth, TargetHeight)
            End With
        End Using

        Return bmDest
    End Function

    '''<summary>
    '''Resize Bitmap in the size you choose
    '''</summary>
    <Extension()>
    Public Function Resize(bmSource As Bitmap, TargetSize As Size) As Image
        Return Resize(bmSource, TargetSize.Width, TargetSize.Height)
    End Function

    '''<summary>
    '''Resize Image in the size you choose
    '''</summary>
    <Extension()>
    Public Function Resize(imSource As Image, TargetWidth As Integer, TargetHeight As Integer) As Image
        Return Resize(DirectCast(imSource, Bitmap), TargetWidth, TargetHeight)
    End Function

    '''<summary>
    '''Resize Image in the size you choose
    '''</summary>
    <Extension()>
    Public Function Resize(imSource As Image, TargetSize As Size) As Image
        Return Resize(DirectCast(imSource, Bitmap), TargetSize.Width, TargetSize.Height)
    End Function

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
    '''Fade Bitmap (Change Opacity)
    '''</summary>
    <Extension()>
    Public Function Fade(ByVal originalBitmap As Bitmap, ByVal opacity As Double) As Bitmap
        Const bytesPerPixel As Integer = 4
        If opacity > 1 Then opacity = 1
        If opacity < 0 Then opacity = 0

        'If (originalImage.PixelFormat And PixelFormat.Indexed) = PixelFormat.Indexed Then
        'Return originalImage
        'End If

        Dim bmp As Bitmap = CType(originalBitmap.Clone(), Bitmap)
        Dim pxf As PixelFormat = PixelFormat.Format32bppArgb
        Dim rect As Rectangle = New Rectangle(0, 0, bmp.Width, bmp.Height)
        Dim bmpData As BitmapData = bmp.LockBits(rect, ImageLockMode.ReadWrite, pxf)
        Dim ptr As IntPtr = bmpData.Scan0
        Dim numBytes As Integer = bmp.Width * bmp.Height * bytesPerPixel
        Dim argbValues As Byte() = New Byte(numBytes - 1) {}
        System.Runtime.InteropServices.Marshal.Copy(ptr, argbValues, 0, numBytes)
        Dim counter As Integer = 0

        While counter < argbValues.Length
            'If argbValues(counter + bytesPerPixel - 1) <> 0 Then Exit While
            Dim pos As Integer = 0
            pos += 1
            pos += 1
            pos += 1
            argbValues(counter + pos) = CByte((argbValues(counter + pos) * opacity))
            counter += bytesPerPixel
        End While

        Marshal.Copy(argbValues, 0, ptr, numBytes)
        bmp.UnlockBits(bmpData)
        Return bmp
    End Function

    '''<summary>
    '''Fade Image (Change Opacity)
    '''</summary>
    <Extension()>
    Public Function Fade(ByVal originalImage As Image, ByVal opacity As Double) As Image
        Return Fade(DirectCast(originalImage, Bitmap), opacity)
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

Public Module ControlExtentions
    '''<summary>
    '''Return graphical state of a control to a bitmap
    '''</summary>
    <Extension()>
    Public Function ToBitmap([Control] As Control) As Bitmap
        Dim bm As New Bitmap([Control].Width, [Control].Height)
        [Control].DrawToBitmap(bm, New Rectangle(0, 0, [Control].Width, [Control].Height))
        Return bm
    End Function
End Module