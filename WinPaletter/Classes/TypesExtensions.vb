Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.IO
Imports System.IO.Compression
Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Public Module ColorsExtensions

    '''<summary>
    '''Reverse Color From RGB To BGR
    '''</summary>
    <Extension()>
    Public Function Reverse([Color] As Color, Optional Alpha As Boolean = False) As Color
        If Not Alpha Then
            Return Color.FromArgb([Color].B, [Color].G, [Color].R)

        Else
            Return Color.FromArgb([Color].A, [Color].B, [Color].G, [Color].R)

        End If

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
        Dim r As Byte
        Dim g As Byte
        Dim b As Byte

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
    '''Get Color as string in the format you choose
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
    '''Return string in the format of XXXXXXXX, useful for registry handling
    '''</summary>
    <Extension()>
    Public Function DWORD(int As Integer) As String
        If int.ToString.Count <= 8 Then
            Dim i As Integer = 8 - int.ToString.Count
            Dim s As String = ""
            For i = 1 To i
                s &= "0"
            Next
            s &= int
            Return s.Replace("-", "")
        Else
            Return int.ToString.Replace("-", "")
        End If

    End Function

    '''<summary>
    '''Return string in the format of XXXXXXXXXXXXXXXX, useful for registry handling
    '''</summary>
    <Extension()>
    Public Function QWORD(int As Integer) As String
        If int.ToString.Count <= 16 Then
            Dim i As Integer = 16 - int.ToString.Count
            Dim s As String = ""
            For i = 1 To i
                s &= "0"
            Next
            s &= int
            Return s.Replace("-", "")
        Else
            Return int.ToString.Replace("-", "")
        End If

    End Function

    '''<summary>
    '''Return string in the format of XX.XX YY, where XX.XX is the size of a file, YY is the appropriate size unit
    '''</summary>
    <Extension()>
    Public Function SizeString(ByVal length As Long) As String
        Dim B As Long = 0, KB As Long = 1024, MB As Long = KB * 1024, GB As Long = MB * 1024, TB As Long = GB * 1024
        Dim size As Double = length
        Dim suffix As String = My.Lang.ByteSizeUnit

        If length >= TB Then
            size = Math.Round(CDbl(length) / TB, 2)
            suffix = My.Lang.TBSizeUnit

        ElseIf length >= GB Then
            size = Math.Round(CDbl(length) / GB, 2)
            suffix = My.Lang.GBSizeUnit

        ElseIf length >= MB Then
            size = Math.Round(CDbl(length) / MB, 2)
            suffix = My.Lang.MBSizeUnit

        ElseIf length >= KB Then
            size = Math.Round(CDbl(length) / KB, 2)
            suffix = My.Lang.KBSizeUnit

        End If

        Return $"{size} {suffix}"
    End Function

    '''<summary>
    '''Return string in the format of XX.XX YY, where XX.XX is the size of a file, YY is the appropriate size unit
    '''</summary>
    <Extension()>
    Public Function SizeString(ByVal length As Short) As String
        Return SizeString(CLng(length))
    End Function

    '''<summary>
    '''Return string in the format of XX.XX YY, where XX.XX is the size of a file, YY is the appropriate size unit
    '''</summary>
    <Extension()>
    Public Function SizeString(ByVal length As Single) As String
        Return SizeString(CLng(length))
    End Function

    '''<summary>
    '''Return string in the format of XX.XX YY, where XX.XX is the size of a file, YY is the appropriate size unit
    '''</summary>
    <Extension()>
    Public Function SizeString(ByVal length As Double) As String
        Return SizeString(CLng(length))
    End Function

    '''<summary>
    '''Return string in the format of XX.XX YY, where XX.XX is the size of a file, YY is the appropriate size unit
    '''</summary>
    <Extension()>
    Public Function SizeString(ByVal length As Decimal) As String
        Return SizeString(CLng(length))
    End Function

    '''<summary>
    '''Return string in the format of XX.XX YY, where XX.XX is the size of a file, YY is the appropriate size unit
    '''</summary>
    <Extension()>
    Public Function SizeString(ByVal length As Integer) As String
        Return SizeString(CLng(length))
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

        Dim Exist As Boolean

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
    Public Function CString([List] As List(Of String), Optional JoinBy As String = vbCrLf) As String
        Return String.Join(JoinBy, [List].ToArray)
    End Function

End Module

Public Module BitmapExtensions
    '''<summary>
    '''Return Most Used Color From Bitmap
    '''</summary>
    <Extension()>
    Public Function AverageColor(ByVal [Bitmap] As Bitmap) As Color
        Try
            Using bmp As Bitmap = [Bitmap].Clone
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
            End Using
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
    Public Function Blur(ByRef image As Bitmap, Optional BlurForce As Integer = 2) As Bitmap
        Using img As New Bitmap(DirectCast(image.Clone, Bitmap))
            Using G As Graphics = Graphics.FromImage(img)
                G.SmoothingMode = SmoothingMode.AntiAlias

                Dim att As New ImageAttributes
                Dim m As New ColorMatrix With {.Matrix33 = 0.4F}
                att.SetColorMatrix(m)

                BlurForce += 1

                For x = -BlurForce To BlurForce Step 0.5
                    G.DrawImage(img, New Rectangle(x, 0, img.Width - 1, img.Height - 1), 0, 0, img.Width - 1, img.Height - 1, GraphicsUnit.Pixel, att)
                Next

                For y = -BlurForce To BlurForce Step 0.5
                    G.DrawImage(img, New Rectangle(0, y, img.Width - 1, img.Height - 1), 0, 0, img.Width - 1, img.Height - 1, GraphicsUnit.Pixel, att)
                Next

                G.Save()
                att.Dispose()
                G.Dispose()
                Return img.Clone
            End Using
        End Using
    End Function

    '''<summary>
    '''Return Blurred Bitmap
    '''</summary>
    <Extension()>
    Public Function Blur(ByRef Bitmap As Image, Optional ByVal BlurForce As Integer = 2) As Bitmap
        Return Blur(DirectCast(Bitmap, Bitmap), BlurForce)
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
        Dim outputImage As New Bitmap(inputImage.Width, inputImage.Height)
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
            Dim bmPhoto As New Bitmap(Size.Width, Size.Height, PixelFormat.Format32bppArgb)
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
            Return Nothing
            Exit Function
        End If

        Using bmDest As New Bitmap(TargetWidth, TargetHeight, PixelFormat.Format32bppArgb)
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
            Return bmDest.Clone
        End Using
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
    '''Return Image Tinted by a color
    '''</summary>
    <Extension()>
    Public Function Tint(ByVal sourceImage As Image, [Color] As Color) As Bitmap
        Return Tint(DirectCast(sourceImage, Bitmap), [Color])
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
        Dim blue As Single
        Dim green As Single
        Dim red As Single
        Dim k As Integer

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

        Dim resultBitmap As New Bitmap(sourceBitmap.Width, sourceBitmap.Height)
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
        Try
            Using bmp As New Bitmap(originalBitmap.Width, originalBitmap.Height)

                Using gfx As Graphics = Graphics.FromImage(bmp)
                    Dim matrix As New ColorMatrix With {.Matrix33 = opacity}
                    Dim attributes As New ImageAttributes()
                    attributes.SetColorMatrix(matrix, ColorMatrixFlag.[Default], ColorAdjustType.Bitmap)
                    gfx.DrawImage(originalBitmap, New Rectangle(0, 0, bmp.Width, bmp.Height), 0, 0, originalBitmap.Width, originalBitmap.Height, GraphicsUnit.Pixel, attributes)
                End Using

                Return bmp.Clone
            End Using
        Catch ex As Exception
            Return Nothing
        End Try
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
    Public Function Grayscale(ByVal original As Image) As Bitmap
        Return Grayscale(DirectCast(original, Bitmap))
    End Function

    '''<summary>
    '''Return Bitmap in Grayscale
    '''</summary>
    <Extension()>
    Public Function Grayscale(ByVal original As Bitmap) As Bitmap
        Dim newBitmap As New Bitmap(original.Width, original.Height)

        Using g As Graphics = Graphics.FromImage(newBitmap)
            Dim colorMatrix As New ColorMatrix(New Single()() {New Single() {0.3F, 0.3F, 0.3F, 0, 0}, New Single() {0.59F, 0.59F, 0.59F, 0, 0}, New Single() {0.11F, 0.11F, 0.11F, 0, 0}, New Single() {0, 0, 0, 1, 0}, New Single() {0, 0, 0, 0, 1}})

            Using attributes As New ImageAttributes()
                attributes.SetColorMatrix(colorMatrix)
                g.DrawImage(original, New Rectangle(0, 0, original.Width, original.Height), 0, 0, original.Width, original.Height, GraphicsUnit.Pixel, attributes)
            End Using
        End Using

        Return newBitmap
    End Function

    '''<summary>
    '''Return Inverted Bitmap
    '''</summary>
    <Extension()>
    Public Function Invert(bmp As Bitmap) As Bitmap
        Dim bmpDest As Bitmap = Nothing

        Using bmpSource As Bitmap = New Bitmap(bmp)
            bmpDest = New Bitmap(bmpSource.Width, bmpSource.Height)

            For x As Integer = 0 To bmpSource.Width - 1
                For y As Integer = 0 To bmpSource.Height - 1
                    Dim clrPixel As Color = bmpSource.GetPixel(x, y)
                    clrPixel = Color.FromArgb(clrPixel.A, 255 - clrPixel.R, 255 -
                       clrPixel.G, 255 - clrPixel.B)
                    bmpDest.SetPixel(x, y, clrPixel)
                Next
            Next
        End Using

        Return bmpDest
    End Function
End Module

Public Module ControlExtensions
    '''<summary>
    '''Return graphical state of a control to a bitmap
    '''</summary>
    <Extension()>
    Public Function ToBitmap([Control] As Control, Optional ByVal FixMethod As Boolean = False) As Bitmap
        If Not FixMethod Then
            Dim bm As New Bitmap([Control].Width, [Control].Height)

            SyncLock [Control]
                [Control].DrawToBitmap(bm, New Rectangle(0, 0, [Control].Width, [Control].Height))
                Return bm.Clone
            End SyncLock
        Else
            Return DrawToBitmap([Control])
        End If

    End Function

    Private Function DrawToBitmap(ByVal Control As Control) As Bitmap
        Dim childControls = Control.Controls.Cast(Of Control)().ToArray()
        Array.Reverse(childControls)

        Dim bmp As New Bitmap([Control].Width, [Control].Height)

        For Each childControl In childControls
            childControl.DrawToBitmap(bmp, childControl.Bounds)
        Next

        Return bmp
    End Function
End Module

Public Module ComboBoxExtensions

    '''<summary>
    '''Add classic themes names to a ComboBox
    '''</summary>
    <Extension()>
    Public Sub PopulateThemes([ComboBox] As ComboBox)
        [ComboBox].Items.Clear()
        [ComboBox].Items.AddRange(My.Resources.RetroThemesDB.CList.[Select](Function(f) f.Split("|")(0)).ToArray())
    End Sub
End Module

Public Module TreeViewExtensions

    '''<summary>
    '''Serialize from a JSON File to TreeView Nodes
    '''</summary>
    <Extension()>
    Public Sub FromJSON([TreeView] As TreeView, ByVal JSON_File As String, ByVal rootName As String)
        Dim reader = New StreamReader(JSON_File)
        Dim jsonReader = New JsonTextReader(reader)
        Dim root = JToken.Load(jsonReader)
        reader.Close()

        [TreeView].BeginUpdate()
        Try
            [TreeView].Nodes.Clear()

            With [TreeView].Nodes.Add(rootName)
                .ImageKey = "json"
                .SelectedImageKey = "json"
                .Tag = root
            End With

            AddNode(root, [TreeView].Nodes.Item(0))

            [TreeView].CollapseAll()
        Finally
            [TreeView].EndUpdate()
        End Try
    End Sub

    Private Sub AddNode(ByVal token As JToken, ByVal inTreeNode As TreeNode)
        If token Is Nothing Then Return

        If TypeOf token Is JValue Then

            With inTreeNode.Nodes.Add(token.ToString())
                .ImageKey = "value"
                .SelectedImageKey = "value"
                .Tag = token
            End With

        ElseIf TypeOf token Is JObject Then
            Dim obj = CType(token, JObject)

            For Each [property] In obj.Properties()
                Dim childNode = inTreeNode.Nodes(inTreeNode.Nodes.Add(New TreeNode([property].Name)))
                childNode.Tag = [property]
                AddNode([property].Value, childNode)
            Next

        ElseIf TypeOf token Is JArray Then
            Dim array = CType(token, JArray)

            For i As Integer = 0 To array.Count - 1
                Dim childNode = inTreeNode.Nodes(inTreeNode.Nodes.Add(New TreeNode(i.ToString())))
                childNode.Tag = array(i)
                AddNode(array(i), childNode)
            Next
        End If
    End Sub


    '''<summary>
    '''Serialize a node to JSON Formatted String
    '''</summary>
    <Extension()>
    Public Function ToJSON([TreeNode] As TreeNode) As String
        Dim J_All As New JObject
        J_All.RemoveAll()

        For Each N As TreeNode In [TreeNode].Nodes

            Dim J As New JObject
            J.RemoveAll()
            LoopThroughNodes(N, N, J)

            J_All.Add(N.Text, J)
        Next

        Return J_All.ToString
    End Function

    Private Sub LoopThroughNodes([Node] As TreeNode, [MainNode] As TreeNode, [JSON] As JObject)
        For Each N As TreeNode In [Node].Nodes
            If N.Nodes.Count = 1 Then
                JSON.Add(N.Text, N.Nodes.Item(0).Text)
            ElseIf N.Nodes.Count > 1 Then
                JSON.Add(N.Text, New JObject())
                Dim Jx As JObject = JSON(N.Text)
                LoopThroughNodes(N, [MainNode], Jx)
            End If
        Next
    End Sub

End Module

Public Module Icons

    <Extension()>
    Public Function ToByteArray(ByVal icon As System.Drawing.Icon) As Byte()
        Dim ms As New MemoryStream()
        icon.Save(ms)
        Dim b As Byte() = ms.ToArray()
        ms.Close()
        ms.Dispose()
        Return ms.ToArray()
    End Function


    <Extension()>
    Public Function ToIcon(ByVal bytes As Byte()) As System.Drawing.Icon
        Dim ms As New MemoryStream(bytes)
        Dim ico As New Icon(ms)
        ms.Close()
        ms.Dispose()
        Return ico
        ico.Dispose()
    End Function

End Module

Public Module Zips
    <Extension()>
    Public Sub ExtractToDirectory(ByVal archive As ZipArchive, ByVal destinationDirectoryName As String, ByVal overwrite As Boolean)
        If Not overwrite Then
            archive.ExtractToDirectory(destinationDirectoryName)
            Return
        End If

        For Each file As ZipArchiveEntry In archive.Entries
            Dim completeFileName As String = Path.Combine(destinationDirectoryName, file.FullName)

            If file.Name = "" Then
                Directory.CreateDirectory(Path.GetDirectoryName(completeFileName))
                Continue For
            End If

            Dim dirToCreate = destinationDirectoryName

            For i = 0 To file.FullName.Split("/"c).Length - 1 - 1
                Dim s = file.FullName.Split("/"c)(i)
                dirToCreate = Path.Combine(dirToCreate, s)
                If Not Directory.Exists(dirToCreate) Then Directory.CreateDirectory(dirToCreate)
            Next

            file.ExtractToFile(completeFileName, True)
        Next
    End Sub
End Module

Public Module Others
    <Extension()>
    Public Sub SetText(Ctrl As Control, text As String)
        Try
            If Ctrl.InvokeRequired Then
                Ctrl.Invoke(New setCtrlTxtInvoker(AddressOf SetText), Ctrl, text)
            Else
                Ctrl.Text = text
                Ctrl.Refresh()
            End If
        Catch

        End Try
    End Sub
    Private Delegate Sub setCtrlTxtInvoker(Ctrl As Control, ByVal text As String)
End Module