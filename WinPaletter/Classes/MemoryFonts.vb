Imports System.Drawing.Text
Imports System.Runtime.InteropServices

Public Module MemoryFonts
    Private Property Pfc As PrivateFontCollection

    Sub New()
        If Pfc Is Nothing Then Pfc = New PrivateFontCollection()
    End Sub

    Sub AddMemoryFont(ByVal fontResource As Byte())
        Dim p As IntPtr
        Dim c As UInteger = 0
        p = Marshal.AllocCoTaskMem(fontResource.Length)
        Marshal.Copy(fontResource, 0, p, fontResource.Length)
        NativeMethods.GDI32.AddFontMemResourceEx(p, CUInt(fontResource.Length), IntPtr.Zero, c)
        Pfc.AddMemoryFont(p, fontResource.Length)
        Marshal.FreeCoTaskMem(p)
    End Sub

    Function GetFont(ByVal fontIndex As Integer, ByVal Optional fontSize As Single = 20, ByVal Optional fontStyle As FontStyle = FontStyle.Regular) As Font
        Return New Font(Pfc.Families(fontIndex), fontSize, fontStyle)
    End Function

End Module
