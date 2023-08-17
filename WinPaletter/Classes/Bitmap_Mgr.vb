Public Class Bitmap_Mgr

    Public Shared Function Load(file As String) As Bitmap
        If IO.File.Exists(file) Then
            Try
                Using bmpStream As IO.Stream = IO.File.Open(file, IO.FileMode.Open, IO.FileAccess.Read)
                    Using image As Image = Image.FromStream(bmpStream)
                        Return New Bitmap(image)
                    End Using
                End Using
            Catch
                Try
                    Using image As Image = Image.FromFile(file)
                        Return New Bitmap(image)
                    End Using
                Catch
                    Return Nothing
                End Try
            End Try
        Else
            Return Nothing
        End If
    End Function

End Class
