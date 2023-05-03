Public Class Bitmap_Mgr

    Public Shared Function Load(file As String) As Bitmap
        If IO.File.Exists(file) Then
            Using fs As New IO.FileStream(file, IO.FileMode.Open, IO.FileAccess.Read)
                Dim L As Integer = CInt(fs.Length)
                Dim B As Byte() = New Byte(L - 1) {}
                fs.Read(B, 0, L)
                fs.Close()
                Using ms As New IO.MemoryStream(B)
                    Return Image.FromStream(ms)
                End Using
            End Using
        Else
            Return Nothing
        End If
    End Function


    Public Shared Function Load(Stream As IO.Stream, Optional CloseStream As Boolean = True, Optional DisposeStream As Boolean = True) As Bitmap
        If Stream IsNot Nothing Then
            Dim L As Integer = CInt(Stream.Length)
            Dim B As Byte() = New Byte(L - 1) {}
            Stream.Read(B, 0, L)

            If CloseStream Then Stream.Close()
            If DisposeStream Then Stream.Dispose()

            Using ms As New IO.MemoryStream(B)
                Return Image.FromStream(ms)
            End Using
        Else
            Return Nothing
        End If
    End Function

End Class
