Imports System.IO
Imports System.IO.Compression
Imports System.Runtime.CompilerServices
Imports System.Text

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
    Public Function Compress(ByVal uncompressedString As String) As String
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
    Public Function Decompress(ByVal compressedString As String) As String
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