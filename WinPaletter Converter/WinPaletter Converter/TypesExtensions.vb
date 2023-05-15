Imports System.IO
Imports System.IO.Compression
Imports System.Runtime.CompilerServices
Imports System.Text

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
    '''Return String from List, each item is in a separate line
    '''</summary>
    <Extension()>
    Public Function CString([List] As List(Of String), Optional JoinBy As String = vbCrLf) As String
        Return String.Join(JoinBy, [List].ToArray)
    End Function

End Module