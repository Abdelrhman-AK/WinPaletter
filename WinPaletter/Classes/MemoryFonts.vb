Imports System.Drawing.Text
Imports System.Runtime.InteropServices

Module MemoryFonts
    <DllImport("gdi32.dll")>
    Private Function AddFontMemResourceEx(ByVal pbFont As IntPtr, ByVal cbFont As UInteger, ByVal pdv As IntPtr,
    <[In]> ByRef pcFonts As UInteger) As IntPtr

    End Function
    Private Property Pfc As PrivateFontCollection

    Sub New()
        If Pfc Is Nothing Then
            Pfc = New PrivateFontCollection()
        End If
    End Sub

    Sub AddMemoryFont(ByVal fontResource As Byte())
        Dim p As IntPtr
        Dim c As UInteger = 0
        p = Marshal.AllocCoTaskMem(fontResource.Length)
        Marshal.Copy(fontResource, 0, p, fontResource.Length)
        AddFontMemResourceEx(p, CUInt(fontResource.Length), IntPtr.Zero, c)
        Pfc.AddMemoryFont(p, fontResource.Length)
        Marshal.FreeCoTaskMem(p)
    End Sub

    Function GetFont(ByVal fontIndex As Integer, ByVal Optional fontSize As Single = 20, ByVal Optional fontStyle As FontStyle = FontStyle.Regular) As Font
        Return New Font(Pfc.Families(fontIndex), fontSize, fontStyle)
    End Function
End Module
