Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Runtime.InteropServices
Imports System.Drawing.Text
Imports System.Drawing

Module MemoryFonts
    <DllImport("gdi32.dll")>
    Private Function AddFontMemResourceEx(ByVal pbFont As IntPtr, ByVal cbFont As UInteger, ByVal pdv As IntPtr,
    <[In]> ByRef pcFonts As UInteger) As IntPtr

    End Function
    Private Property pfc As PrivateFontCollection

    Sub New()
        If pfc Is Nothing Then
            pfc = New PrivateFontCollection()
        End If
    End Sub

    Sub AddMemoryFont(ByVal fontResource As Byte())
        Dim p As IntPtr
        Dim c As UInteger = 0
        p = Marshal.AllocCoTaskMem(fontResource.Length)
        Marshal.Copy(fontResource, 0, p, fontResource.Length)
        AddFontMemResourceEx(p, CUInt(fontResource.Length), IntPtr.Zero, c)
        pfc.AddMemoryFont(p, fontResource.Length)
        Marshal.FreeCoTaskMem(p)
        p = IntPtr.Zero
    End Sub

    Function GetFont(ByVal fontIndex As Integer, ByVal Optional fontSize As Single = 20, ByVal Optional fontStyle As FontStyle = FontStyle.Regular) As Font
        Return New Font(pfc.Families(fontIndex), fontSize, fontStyle)
    End Function

    Function UnicodeToChar(ByVal hex As String) As String
        Dim code As Integer = Integer.Parse(hex, System.Globalization.NumberStyles.HexNumber)
        Dim unicodeString As String = Char.ConvertFromUtf32(code)
        Return unicodeString
    End Function
End Module
