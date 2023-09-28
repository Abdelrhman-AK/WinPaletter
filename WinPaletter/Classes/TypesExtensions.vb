Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.IO
Imports System.IO.Compression
Imports System.Reflection
Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices
Imports System.Text
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
    Public Function HEX([Color] As Color, Optional Alpha As Boolean = True, Optional Hash As Boolean = False) As String
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
    Public Function ToWin32Reg([Color] As Color, Optional Alpha As Boolean = False) As String
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
    Public Function CB(color As Color, correctionFactor As Single) As Color
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
                    s = Color.ToWin32Reg(Alpha)

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
    Public Function Blend(color As Color, backColor As Color, amount As Double) As Color
        If amount > 100 Then amount = 100
        If amount < 0 Then amount = 0

        Dim a As Byte = CByte((color.A * (amount / 100) + backColor.A * (amount / 100)) / 2)
        Dim r As Byte = CByte((color.R * (amount / 100) + backColor.R * (amount / 100)) / 2)
        Dim g As Byte = CByte((color.G * (amount / 100) + backColor.G * (amount / 100)) / 2)
        Dim b As Byte = CByte((color.B * (amount / 100) + backColor.B * (amount / 100)) / 2)
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
    Public Function Invert([Color] As Color) As Color
        Return Color.FromArgb([Color].A, 255 - [Color].R, 255 - [Color].G, 255 - [Color].B)
    End Function

    '''<summary>
    '''Get If the color is dark or not
    '''</summary>
    <Extension()>
    Public Function IsDark([Color] As Color) As Boolean
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
    Public Function SizeString(length As Long, Optional ShowSecondUnit As Boolean = False) As String
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

        If ShowSecondUnit Then suffix &= My.Lang.SecondUnit

        Return $"{size} {suffix}"
    End Function

    '''<summary>
    '''Return string in the format of XX.XX YY, where XX.XX is the size of a file, YY is the appropriate size unit
    '''</summary>
    <Extension()>
    Public Function SizeString(length As Short, Optional ShowSecondUnit As Boolean = False) As String
        Return SizeString(CLng(length), ShowSecondUnit)
    End Function

    '''<summary>
    '''Return string in the format of XX.XX YY, where XX.XX is the size of a file, YY is the appropriate size unit
    '''</summary>
    <Extension()>
    Public Function SizeString(length As Single, Optional ShowSecondUnit As Boolean = False) As String
        Return SizeString(CLng(length), ShowSecondUnit)
    End Function

    '''<summary>
    '''Return string in the format of XX.XX YY, where XX.XX is the size of a file, YY is the appropriate size unit
    '''</summary>
    <Extension()>
    Public Function SizeString(length As Double, Optional ShowSecondUnit As Boolean = False) As String
        Return SizeString(CLng(length), ShowSecondUnit)
    End Function

    '''<summary>
    '''Return string in the format of XX.XX YY, where XX.XX is the size of a file, YY is the appropriate size unit
    '''</summary>
    <Extension()>
    Public Function SizeString(length As Decimal, Optional ShowSecondUnit As Boolean = False) As String
        Return SizeString(CLng(length), ShowSecondUnit)
    End Function

    '''<summary>
    '''Return string in the format of XX.XX YY, where XX.XX is the size of a file, YY is the appropriate size unit
    '''</summary>
    <Extension()>
    Public Function SizeString(length As Integer, Optional ShowSecondUnit As Boolean = False) As String
        Return SizeString(CLng(length), ShowSecondUnit)
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
    '''Return Color From Reg String String
    '''</summary>
    <Extension()>
    Public Function FromWin32RegToColor([String] As String) As Color

        Try
            If [String].Contains(" ") Then
                Dim Splitted As String() = [String].Split(" ")

                If Splitted.Count = 3 Then
                    Return Color.FromArgb(255, Val(Splitted(0)), Val(Splitted(1)), Val(Splitted(2)))
                ElseIf Splitted.Count = 4 Then
                    Return Color.FromArgb(Val(Splitted(0)), Val(Splitted(1)), Val(Splitted(2)), Val(Splitted(3)))
                Else
                    Return Color.Empty
                End If
            Else
                Return Color.Empty
            End If
        Catch
            Return Color.Empty
        End Try

    End Function

    '''<summary>
    '''Convert String to List (String should be multi-lines)
    '''</summary>
    <Extension()>
    Public Function CList([String] As String) As List(Of String)
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


    <Extension()>
    Public Function PhrasePath(path As String) As String
        Return Environment.ExpandEnvironmentVariables(path.Replace("%WinDir%", "%windir%\").Replace("%ThemeDir%", "%ThemeDir%\").Replace("\\", "\") _
                                                      .Replace("%ThemeDir%", My.PATH_ProgramFiles & "\Plus!\Themes"))
    End Function


    <Extension()>
    Public Function ToBytes(str As String) As Byte()
        Dim numChars As String() = str.Split(" ")
        Dim result = New Byte(numChars.Length - 1) {}

        For i As Integer = 0 To numChars.Length - 1
            If Not String.IsNullOrWhiteSpace(numChars(i)) Then result(i) = numChars(i) Else result(i) = 0
        Next

        Return result
    End Function


    <Extension()>
    Public Function Compress(uncompressedString As String) As String
        Dim compressedBytes As Byte()

        Using uncompressedStream = New MemoryStream(Encoding.UTF8.GetBytes(uncompressedString))
            Using compressedStream = New MemoryStream()
                Using compressorStream = New DeflateStream(compressedStream, CompressionLevel.Fastest, True)
                    uncompressedStream.CopyTo(compressorStream)
                End Using
                compressedBytes = compressedStream.ToArray()
            End Using
        End Using

        Return Convert.ToBase64String(compressedBytes)
    End Function


    <Extension()>
    Public Function Decompress(compressedString As String) As String
        Dim decompressedBytes As Byte()
        Dim compressedStream = New MemoryStream(Convert.FromBase64String(compressedString))

        Using decompressorStream = New DeflateStream(compressedStream, CompressionMode.Decompress)
            Using decompressedStream = New MemoryStream()
                decompressorStream.CopyTo(decompressedStream)
                decompressedBytes = decompressedStream.ToArray()
            End Using
        End Using

        Return Encoding.UTF8.GetString(decompressedBytes)
    End Function


    <Extension()>
    Public Function Replace(s As String, word As String, by As String, stringComparison As StringComparison, WholeWord As Boolean) As String
        s &= " "
        Dim wordSt As Integer
        Dim sb As New StringBuilder()
        While s.IndexOf(word, stringComparison) > -1
            wordSt = s.IndexOf(word, stringComparison)
            If Not WholeWord OrElse (wordSt = 0 OrElse Not Char.IsLetterOrDigit(Char.Parse(s.Substring(wordSt - 1, 1)))) AndAlso Not Char.IsLetterOrDigit(Char.Parse(s.Substring(wordSt + word.Length, 1))) Then
                sb.Append(s.Substring(0, wordSt) & by)
            Else
                sb.Append(s.Substring(0, wordSt + word.Length))
            End If
            s = s.Substring(wordSt + word.Length)
        End While
        sb.Append(s)
        Return sb.ToString().Substring(0, sb.Length - 1)
    End Function


    <Extension()>
    Public Function Replace(s As String, word As String, by As String, IgnoreCase As Boolean, WholeWord As Boolean) As String
        Dim stringComparison As StringComparison = StringComparison.Ordinal
        If IgnoreCase Then stringComparison = StringComparison.OrdinalIgnoreCase
        Return Replace(s, word, by, stringComparison, WholeWord)
    End Function
End Module

Public Module ListOfStringExtensions

    '''<summary>
    '''Deduplicate list of string
    '''</summary>
    <Extension()>
    Function DeDuplicate([List] As List(Of String)) As List(Of String)
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
    Public Function AverageColor([Bitmap] As Bitmap) As Color
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
    Public Function AverageColor([Image] As Image) As Color
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
    Public Function Blur(ByRef Bitmap As Image, Optional BlurForce As Integer = 2) As Bitmap
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
    Public Function ReplaceColor(inputImage As Bitmap, oldColor As Color, NewColor As Color) As Bitmap
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
    Public Function ReplaceColor(inputImage As Image, oldColor As Color, NewColor As Color) As Image
        Return ReplaceColor(DirectCast(inputImage, Bitmap), oldColor, NewColor)
    End Function

    '''<summary>
    '''Return Bitmap filled in the scale of size you choose
    '''</summary>
    <Extension()>
    Public Function FillScale(Bitmap As Bitmap, Size As Size) As Bitmap
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
    Public Function FillScale(Image As Image, Size As Size) As Image
        Return FillScale(DirectCast(Image, Bitmap), Size)
    End Function

    '''<summary>
    '''Resize Bitmap in the size you choose
    '''</summary>
    <Extension()>
    Public Function Resize(bmSource As Bitmap, TargetWidth As Integer, TargetHeight As Integer) As Bitmap
        If bmSource Is Nothing Then Return Nothing

        Using B As New Bitmap(TargetWidth, TargetHeight, PixelFormat.Format32bppArgb)
            Using G As Graphics = Graphics.FromImage(B)
                With G
                    .CompositingQuality = CompositingQuality.HighQuality
                    .InterpolationMode = InterpolationMode.HighQualityBicubic
                    .PixelOffsetMode = PixelOffsetMode.HighQuality
                    .SmoothingMode = SmoothingMode.AntiAlias
                    .CompositingMode = CompositingMode.SourceOver
                    .DrawImage(bmSource, 0, 0, TargetWidth, TargetHeight)
                End With
            End Using

            B.SetResolution(TargetWidth, TargetHeight)
            Return B.Clone
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
    Public Function Tint(sourceImage As Image, [Color] As Color) As Bitmap
        Return Tint(DirectCast(sourceImage, Bitmap), [Color])
    End Function

    '''<summary>
    '''Return Bitmap Tinted by a color
    '''</summary>
    <Extension()>
    Public Function Tint(sourceBitmap As Bitmap, [Color] As Color) As Bitmap
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
    Public Function Fade(originalBitmap As Bitmap, opacity As Double) As Bitmap
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
    Public Function Fade(originalImage As Image, opacity As Double) As Image
        Return Fade(DirectCast(originalImage, Bitmap), opacity)
    End Function

    '''<summary>
    '''Return Bitmap in Grayscale
    '''</summary>
    <Extension()>
    Public Function Grayscale(original As Image) As Bitmap
        Return Grayscale(DirectCast(original, Bitmap))
    End Function

    '''<summary>
    '''Return Bitmap in Grayscale
    '''</summary>
    <Extension()>
    Public Function Grayscale(original As Bitmap) As Bitmap
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

        Using bmpSource As New Bitmap(bmp)
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


    <Extension()>
    Public Function Tile(bmp As Bitmap, Size As Size) As Bitmap
        Using B As New Bitmap(Size.Width, Size.Height)
            Dim G As Graphics = Graphics.FromImage(B)
            G.SmoothingMode = SmoothingMode.HighSpeed
            Dim tb As New TextureBrush(bmp)
            G.FillRectangle(tb, New Rectangle(0, 0, Size.Width, Size.Height))
            G.Save()
            Return B.Clone
        End Using
    End Function
End Module

Public Module ControlExtensions
    '''<summary>
    '''Return graphical state of a control to a bitmap
    '''</summary>
    <Extension()>
    Public Function ToBitmap([Control] As Control, Optional FixMethod As Boolean = False) As Bitmap
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

    Private Function DrawToBitmap(Control As Control) As Bitmap
        Dim childControls = Control.Controls.Cast(Of Control)().ToArray()
        Array.Reverse(childControls)

        Dim bmp As New Bitmap([Control].Width, [Control].Height)

        For Each childControl In childControls
            childControl.DrawToBitmap(bmp, childControl.Bounds)
        Next

        Return bmp
    End Function

    Public Sub DoubleBufferedControl([Control] As Control, setting As Boolean)
        Dim panType As Type = [Control].[GetType]()
        Dim pi As PropertyInfo = panType.GetProperty("DoubleBuffered", BindingFlags.Instance Or BindingFlags.NonPublic)
        pi.SetValue([Control], setting, Nothing)
    End Sub

    <Extension()>
    Friend Function GetParentColor(ctrl As Control, Optional Accept_Transparent As Boolean = False) As Color

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

    <Extension()>
    Public Sub DoubleBuffer(Parent As Control)
        DoubleBufferedControl(Parent, True)

        For Each ctrl As Control In Parent.Controls
            If ctrl.HasChildren Then
                For Each c As Control In ctrl.Controls
                    DoubleBuffer(c)
                Next
            End If

            DoubleBufferedControl(ctrl, True)
        Next
    End Sub

    <Extension()>
    Public Function GetAllControls(parent As Control) As IEnumerable(Of Control)
        Try
            Dim cs = parent.Controls.OfType(Of Control).OrderBy(Function(c) c.Name)
            Return cs.SelectMany(Function(c) GetAllControls(c)).Concat(cs).OrderBy(Function(c) c.Name)
        Catch ex As Exception
            Dim cs = parent.Controls.OfType(Of Control)
            Return cs.SelectMany(Function(c) GetAllControls(c)).Concat(cs)
        End Try
    End Function

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
    Private Delegate Sub setCtrlTxtInvoker(Ctrl As Control, text As String)

    <Extension()>
    Public Sub SetTag(Ctrl As Control, Tag As Object)
        Try
            If Ctrl.InvokeRequired Then
                Ctrl.Invoke(New setCtrlTagInvoker(AddressOf SetTag), Ctrl, Tag)
            Else
                Ctrl.Tag = Tag
            End If
        Catch

        End Try
    End Sub
    Private Delegate Sub setCtrlTagInvoker(Ctrl As Control, Tag As Object)

    <Extension()>
    Public Sub AddTreeNode(Ctrl As Windows.Forms.TreeView, text As String, imagekey As String)
        Try
            If Ctrl.InvokeRequired Then
                Ctrl.Invoke(New AddTreeNodeInvoker(AddressOf AddTreeNode), Ctrl, text, imagekey)
            Else
                With Ctrl.Nodes.Add(text)
                    .ImageKey = imagekey : .SelectedImageKey = imagekey
                End With
            End If
        Catch

        End Try
    End Sub
    Private Delegate Sub AddTreeNodeInvoker(Ctrl As Windows.Forms.TreeView, text As String, imagekey As String)

    <Extension()>
    Public Sub PerformStepMethod2(ProgressBar As ProgressBar)
        If ProgressBar.InvokeRequired Then
            ProgressBar.Invoke(New PerformStepMethod2Invoker(AddressOf PerformStepMethod2), ProgressBar)
        Else
            ProgressBar.PerformStep()
        End If
    End Sub
    Private Delegate Sub PerformStepMethod2Invoker(ProgressBar As ProgressBar)
End Module

Module GraphicsExtensions

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

    <Extension()>
    Public Sub DrawGlow(G As Graphics, R As Rectangle, GlowColor As Color, Optional GlowSize As Integer = 5, Optional GlowFade As Integer = 7)
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

    <Extension()>
    Public Sub FillRoundedRect([Graphics] As Graphics, [Brush] As Brush, [Rectangle] As Rectangle, Optional [Radius] As Integer = -1, Optional ForcedRoundCorner As Boolean = False)
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
    Public Sub DrawRoundImage([Graphics] As Graphics, [Image] As Image, [Rectangle] As Rectangle, Optional [Radius] As Integer = -1, Optional ForcedRoundCorner As Boolean = False)
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
    Public Function Round(r As Rectangle, radius As Integer) As GraphicsPath
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
    Public Sub DrawRoundedRect([Graphics] As Graphics, [Pen] As Pen, [Rectangle] As Rectangle, Optional [Radius_willbe_x2] As Integer = -1, Optional ForcedRoundCorner As Boolean = False)
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
    Public Sub DrawRoundedRect_LikeW11([Graphics] As Graphics, [PenX] As Pen, [Rectangle] As Rectangle, Optional [Radius] As Integer = -1, Optional ForcedRoundCorner As Boolean = False)
        Try
            Dim Dark As Boolean = My.Style.DarkMode

            If [Radius] = -1 Then [Radius] = 5
            [Radius] *= 2
            [Graphics].SmoothingMode = SmoothingMode.AntiAlias

            Using [Pen] As New Pen([PenX].Color, [PenX].Width) With {.DashStyle = [PenX].DashStyle, .DashOffset = [PenX].DashOffset}
                Using [Pen2] As New Pen([PenX].Color, [PenX].Width) With {.DashStyle = [PenX].DashStyle, .DashOffset = [PenX].DashOffset}
                    Dim SidePen As New Pen([PenX].Color, [PenX].Width) With {.DashStyle = [PenX].DashStyle, .DashOffset = [PenX].DashOffset}

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

                    Using [PenG] As New Pen(G, [PenX].Width) With {.DashStyle = [PenX].DashStyle, .DashOffset = [PenX].DashOffset}

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
    Public Sub FromJSON([TreeView] As Windows.Forms.TreeView, JSON_File As String, rootName As String)
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

    Private Sub AddNode(token As JToken, inTreeNode As TreeNode)
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
    Public Function ToByteArray(icon As System.Drawing.Icon) As Byte()
        Using ms As New MemoryStream()
            icon.Save(ms)
            Dim b As Byte() = ms.ToArray()
            ms.Close()
            Return ms.ToArray()
        End Using
    End Function

    <Extension()>
    Public Function ToIcon(bytes As Byte()) As System.Drawing.Icon
        Using ms As New MemoryStream(bytes)
            Return New Icon(ms)
        End Using
    End Function

End Module

Public Module Others
    <Extension()>
    Public Function ToStringFormat(TextAlign As ContentAlignment, Optional RightToLeft As Boolean = False) As StringFormat
        Dim SF As New StringFormat()
        Select Case TextAlign
            Case ContentAlignment.TopLeft
                SF.LineAlignment = StringAlignment.Near
                SF.Alignment = StringAlignment.Near
            Case ContentAlignment.TopCenter
                SF.LineAlignment = StringAlignment.Near
                SF.Alignment = StringAlignment.Center
            Case ContentAlignment.TopRight
                SF.LineAlignment = StringAlignment.Near
                SF.Alignment = StringAlignment.Far
            Case ContentAlignment.MiddleLeft
                SF.LineAlignment = StringAlignment.Center
                SF.Alignment = StringAlignment.Near
            Case ContentAlignment.MiddleCenter
                SF.LineAlignment = StringAlignment.Center
                SF.Alignment = StringAlignment.Center
            Case ContentAlignment.MiddleRight
                SF.LineAlignment = StringAlignment.Center
                SF.Alignment = StringAlignment.Far
            Case ContentAlignment.BottomLeft
                SF.LineAlignment = StringAlignment.Far
                SF.Alignment = StringAlignment.Near
            Case ContentAlignment.BottomCenter
                SF.LineAlignment = StringAlignment.Far
                SF.Alignment = StringAlignment.Center
            Case ContentAlignment.BottomRight
                SF.LineAlignment = StringAlignment.Far
                SF.Alignment = StringAlignment.Far
            Case Else
                SF.LineAlignment = StringAlignment.Center
                SF.Alignment = StringAlignment.Near

        End Select

        If RightToLeft Then SF.FormatFlags = StringFormatFlags.DirectionRightToLeft

        Return SF
    End Function
End Module