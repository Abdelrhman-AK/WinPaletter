Public Class Converter

    Public Shared Format As Converter_CP.WP_Format
    Dim Converter_CP As Converter_CP

    Function FetchFile(File As String) As Converter_CP.WP_Format
        If IO.File.Exists(File) Then
            Converter_CP = New Converter_CP(File)
            Return Format
        Else
            Return Converter_CP.WP_Format.Error
        End If
    End Function

    Sub Convert(File As String, SaveAs As String, Compress As Boolean, OldWPTH1069 As Boolean)
        If IO.File.Exists(File) Then
            Converter_CP = New Converter_CP(File)

            Select Case Format
                Case Converter_CP.WP_Format.JSON
                    Converter_CP.Save(Converter_CP.WP_Format.WPTH, SaveAs, Compress, OldWPTH1069)

                Case Converter_CP.WP_Format.WPTH
                    Converter_CP.Save(Converter_CP.WP_Format.JSON, SaveAs, Compress, OldWPTH1069)

            End Select

        End If
    End Sub
End Class