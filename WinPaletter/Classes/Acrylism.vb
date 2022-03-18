Imports System.Runtime.InteropServices
Public Class Acrylism

#Region "Fluent"

    Friend Structure WindowCompositionAttributeData
        Public Attribute As WindowCompositionAttribute
        Public Data As IntPtr
        Public SizeOfData As Integer
    End Structure

    Friend Enum WindowCompositionAttribute
        WCA_ACCENT_POLICY = 19
    End Enum

    Friend Enum AccentState
        ACCENT_DISABLED = 0
        ACCENT_ENABLE_GRADIENT = 1
        ACCENT_ENABLE_TRANSPARENTGRADIENT = 2
        ACCENT_ENABLE_BLURBEHIND = 3
        ACCENT_ENABLE_TRANSPARANT = 6
        ACCENT_ENABLE_ACRYLICBLURBEHIND = 4
    End Enum

    <StructLayout(LayoutKind.Sequential)>
    Friend Structure AccentPolicy
        Public AccentState As AccentState
        Public AccentFlags As Integer
        Public GradientColor As Integer
        Public AnimationId As Integer
    End Structure

    Friend Declare Function SetWindowCompositionAttribute Lib "user32.dll" (ByVal hwnd As IntPtr, ByRef data As WindowCompositionAttributeData) As Integer
    Private Declare Auto Function FindWindow Lib "user32.dll" (ByVal lpClassName As String, ByVal lpWindowName As String) As IntPtr

#End Region

#Region "Aero"
    <Runtime.InteropServices.StructLayout(Runtime.InteropServices.LayoutKind.Sequential)> Public Structure Side
        Public Left As Integer
        Public Right As Integer
        Public Top As Integer
        Public Buttom As Integer
    End Structure
    <Runtime.InteropServices.DllImport("dwmapi.dll")> Public Shared Function DwmExtendFrameIntoClientArea(ByVal hWnd As IntPtr, ByRef pMarinset As Side) As Integer
    End Function

#End Region

    Public Shared Sub EnableBlur(ByVal Handle As IntPtr, Optional ByVal Border As Boolean = True)

        Dim accent = New AccentPolicy With {.AccentState = AccentState.ACCENT_ENABLE_BLURBEHIND}
        If Border Then accent.AccentFlags = &H20 Or &H40 Or &H80 Or &H100
        Dim accentStructSize = Marshal.SizeOf(accent)
        Dim accentPtr = Marshal.AllocHGlobal(accentStructSize)
        Marshal.StructureToPtr(accent, accentPtr, False)

        Dim Data = New WindowCompositionAttributeData With {
                .Attribute = WindowCompositionAttribute.WCA_ACCENT_POLICY,
                .SizeOfData = accentStructSize,
                .Data = accentPtr
            }

        SetWindowCompositionAttribute(Handle, Data)

        Marshal.FreeHGlobal(accentPtr)

    End Sub

    Public Shared Sub EnableAero(ByVal Handle As IntPtr)
        Try
            Dim side As Side = New Side
            side.Left = -1
            side.Right = -1
            side.Top = -1
            side.Buttom = -1
            Dim result As Integer = DwmExtendFrameIntoClientArea(Handle, side)
        Catch
        End Try
    End Sub
End Class