Public Class Bitmap_Mgr

    Public Shared Function Load(file As String) As Bitmap
        If IO.File.Exists(file) Then
            Try
                Using fs As New IO.FileStream(file, IO.FileMode.Open, IO.FileAccess.Read)
                    Dim L As Integer = CInt(fs.Length)
                    Dim B As Byte() = New Byte(L - 1) {}
                    fs.Read(B, 0, L)
                    fs.Close()
                    Using ms As New IO.MemoryStream(B)
                        Using bmp As New Bitmap(ms)
                            Return bmp.Clone
                        End Using
                    End Using
                End Using
            Catch
                Try
                    Using fs As New IO.FileStream(file, IO.FileMode.Open, IO.FileAccess.Read)
                        Using bmp As New Bitmap(fs)
                            Return bmp.Clone
                        End Using
                    End Using
                Catch
                    Return New Bitmap(file).Clone
                End Try
            End Try
        Else
            Return Nothing
        End If
    End Function

End Class
