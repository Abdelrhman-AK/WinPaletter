Imports System.Text
Imports WinPaletter.NativeMethods.Kernel32

Public Class INI : Implements IDisposable

    Public path As String
    Private disposedValue As Boolean

    Public Sub New(ByVal INIPath As String)
        path = INIPath
    End Sub

    Public Sub IniWriteValue(ByVal Section As String, ByVal Key As String, ByVal Value As String)
        WritePrivateProfileString(Section, Key, Value, Me.path)
    End Sub

    Public Function IniReadValue(ByVal Section As String, ByVal Key As String, Optional DefaultValue As String = Nothing) As String
        Dim temp As StringBuilder = New StringBuilder(65535)
        Dim i As Integer = GetPrivateProfileString(Section, Key, DefaultValue, temp, temp.Capacity, Me.path)
        Return temp.ToString()
    End Function

    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects)
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override finalizer
            ' TODO: set large fields to null
            disposedValue = True
        End If
    End Sub

    ' ' TODO: override finalizer only if 'Dispose(disposing As Boolean)' has code to free unmanaged resources
    ' Protected Overrides Sub Finalize()
    '     ' Do not change this code. Put cleanup code in 'Dispose(disposing As Boolean)' method
    '     Dispose(disposing:=False)
    '     MyBase.Finalize()
    ' End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code. Put cleanup code in 'Dispose(disposing As Boolean)' method
        Dispose(disposing:=True)
        GC.SuppressFinalize(Me)
    End Sub
End Class
