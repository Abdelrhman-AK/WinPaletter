Imports System.IO
Imports System.Reflection
Imports System.Runtime.InteropServices
Imports Microsoft.Win32

Namespace NativeMethods
    Public Class Dwmapi

        <DllImport("dwmapi.dll", EntryPoint:="#131", PreserveSig:=False)>
        Public Shared Sub DwmSetColorizationParameters(ByRef parameters As DWM_COLORIZATION_PARAMS, ByVal unknown As Boolean)
        End Sub

        <DllImport("dwmapi.dll")>
        Public Shared Sub DwmEnableComposition(ByVal uCompositionAction As CompositionAction)
        End Sub


        <DllImport("dwmapi.dll")>
        Public Shared Function DwmIsCompositionEnabled(ByRef enabled As Boolean) As Integer
        End Function

        <DllImport("dwmapi.dll")>
        Public Shared Function DwmEnableBlurBehindWindow(ByVal hWnd As IntPtr, ByRef pBlurBehind As DwmBlurbehind) As Integer
        End Function

        Public Structure DwmBlurbehind
            Public DwFlags As Integer
            Public FEnable As Boolean
            Public HRgnBlur As IntPtr
            Public FTransitionOnMaximized As Boolean
        End Structure

        <Runtime.InteropServices.DllImport("dwmapi")> Public Shared Function DwmExtendFrameIntoClientArea(ByVal hWnd As IntPtr, ByRef pMarInset As MARGINS) As Integer
        End Function

        <Runtime.InteropServices.DllImport("dwmapi")> Friend Shared Function DwmSetWindowAttribute(ByVal hwnd As IntPtr, ByVal attr As Integer, ByRef attrValue As Integer, ByVal attrSize As Integer) As Integer
        End Function

        Public Const CS_DROPSHADOW As Integer = &H20000
        Public Const WM_NCPAINT As Integer = &H85
        Public Enum CompositionAction As Integer
            DWM_EC_DISABLECOMPOSITION = 0
            DWM_EC_ENABLECOMPOSITION = 1
        End Enum

        <Runtime.InteropServices.StructLayout(Runtime.InteropServices.LayoutKind.Sequential)> Public Structure MARGINS
            Public leftWidth As Integer
            Public rightWidth As Integer
            Public topHeight As Integer
            Public bottomHeight As Integer
        End Structure

        Public Structure DWM_COLORIZATION_PARAMS
            Public clrColor As Integer
            Public clrAfterGlow As Integer
            Public nIntensity As Integer
            Public clrAfterGlowBalance As Integer
            Public clrBlurBalance As Integer
            Public clrGlassReflectionIntensity As Integer
            Public fOpaque As Boolean
        End Structure

    End Class

    Public Class User32
        <DllImport("user32.dll")>
        Public Shared Function AnimateWindow(ByVal hWnd As IntPtr, ByVal time As Integer, ByVal flags As AnimateWindowFlags) As Boolean
        End Function

        <DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto)>
        Public Shared Function SendMessageTimeout(ByVal hWnd As IntPtr, ByVal Msg As UInteger, ByVal wParam As UIntPtr, ByVal lParam As IntPtr, ByVal fuFlags As SendMessageTimeoutFlags, ByVal uTimeout As UInteger, <Out> ByRef lpdwResult As UIntPtr) As IntPtr
        End Function

        Declare Function SHChangeNotify Lib "Shell32.dll" (ByVal wEventID As Int32, ByVal uFlags As Int32, ByVal dwItem1 As Int32, ByVal dwItem2 As Int32) As Int32

        Friend Declare Function SetWindowCompositionAttribute Lib "user32.dll" (ByVal hwnd As IntPtr, ByRef data As WindowCompositionAttributeData) As Integer

        Public Declare Auto Function FindWindow Lib "user32.dll" (ByVal lpClassName As String, ByVal lpWindowName As String) As IntPtr

        <DllImport("user32.dll")>
        Public Shared Function SetSystemCursor(ByVal hcur As IntPtr, ByVal id As Integer) As Boolean
        End Function

        Declare Function LoadCursorFromFile Lib "user32.dll" Alias "LoadCursorFromFileA" (ByVal lpFileName As String) As IntPtr

        <DllImport("Shell32.dll", EntryPoint:="SHDefExtractIconW")>
        Public Shared Function SHDefExtractIconW(<MarshalAs(UnmanagedType.LPTStr)> ByVal pszIconFile As String, ByVal iIndex As Integer, ByVal uFlags As UInteger, ByRef phiconLarge As IntPtr, ByRef phiconSmall As IntPtr, ByVal nIconSize As UInteger) As Integer
        End Function

        <DllImport("user32.dll", EntryPoint:="DestroyIcon")>
        Public Shared Function DestroyIcon(ByVal hIcon As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
        End Function

        Public Shared Function MAKEICONSIZE(ByVal low As Integer, ByVal high As Integer) As Integer
            Return (high << 16) Or (low And &HFFFF)
        End Function

        Public Shared Function ExtractSmallIcon(Path As String, Optional IconIndex As Integer = 0)
            Dim ico As Icon
            'Make the nIconSize value (See the Msdn documents). The LOWORD is the Large Icon Size. The HIWORD is the Small Icon Size.
            'The largest size for an icon is 256.
            Dim LargeAndSmallSize As UInteger = CUInt(MAKEICONSIZE(256, 16))

            Dim hLrgIcon As IntPtr = IntPtr.Zero
            Dim hSmlIcon As IntPtr = IntPtr.Zero

            Dim result As Integer = SHDefExtractIconW(Path, IconIndex, 0, hLrgIcon, hSmlIcon, LargeAndSmallSize)

            If result = 0 Then
                If ico IsNot Nothing Then ico.Dispose()

                'if the large and/or small icons where created in the unmanaged memory successfuly then create
                'a clone of them in the managed icons and delete the icons in the unmanaged memory.

                If hSmlIcon <> IntPtr.Zero Then
                    ico = CType(Icon.FromHandle(hSmlIcon).Clone, Icon)
                    DestroyIcon(hSmlIcon)
                End If

            End If

            Return ico
        End Function
        Enum OCR_SYSTEM_CURSORS As Integer

            ' Standard arrow And small hourglass
            OCR_APPSTARTING = 32650

            'Standard arrow
            OCR_NORMAL = 32512

            'Crosshair
            OCR_CROSS = 32515

            'Windows 2000/XP: Hand
            OCR_HAND = 32649

            'Arrow And question mark
            OCR_HELP = 32651

            'I-beam
            OCR_IBEAM = 32513

            'Slashed circle
            OCR_NO = 32648

            'Four-pointed arrow pointing north south east And west
            OCR_SIZEALL = 32646

            'Double-pointed arrow pointing northeast And southwest
            OCR_SIZENESW = 32643

            'Double-pointed arrow pointing north And south
            OCR_SIZENS = 32645

            'Double-pointed arrow pointing northwest And southeast
            OCR_SIZENWSE = 32642

            'Double-pointed arrow pointing west And east
            OCR_SIZEWE = 32644

            'Vertical arrow
            OCR_UP = 32516

            'Hourglass
            OCR_WAIT = 32514
        End Enum

        <StructLayout(LayoutKind.Sequential)>
        Friend Structure AccentPolicy
            Public AccentState As AccentState
            Public AccentFlags As Integer
            Public GradientColor As Integer
            Public AnimationId As Integer
        End Structure

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

        Enum SendMessageTimeoutFlags As UInteger
            SMTO_NORMAL = &H0
            SMTO_BLOCK = &H1
            SMTO_ABORTIFHUNG = &H2
            SMTO_NOTIMEOUTIFNOTHUNG = &H8
        End Enum

        Shared WM_DWMCOLORIZATIONCOLORCHANGED As Integer = &H320
        Shared WM_DWMCOMPOSITIONCHANGED As Integer = &H31E
        Shared WM_THEMECHANGED As Integer = &H31A
        Shared WM_SYSCOLORCHANGE As Integer = &H15
        Shared WM_PALETTECHANGED As Integer = &H311
        Shared WM_WININICHANGE As UInteger = &H1A
        Shared WM_SETTINGCHANGE As UInteger = WM_WININICHANGE
        Shared HWND_MESSAGE As Int32 = -&H3
        Shared HWND_BROADCAST As IntPtr = New IntPtr(&HFFFF)
        Shared MSG_TIMEOUT As Integer = 5000
        Shared RESULT As UIntPtr

        <Flags>
        Enum AnimateWindowFlags
            AW_HOR_POSITIVE = &H0
            AW_HOR_NEGATIVE = &H2
            AW_VER_POSITIVE = &H4
            AW_VER_NEGATIVE = &H8
            AW_CENTER = &H10
            AW_HIDE = &H10000
            AW_ACTIVATE = &H20000
            AW_SLIDE = &H40000
            AW_BLEND = &H80000
        End Enum

    End Class

    Public Class Kernel32
        <DllImport("kernel32.dll", SetLastError:=True)>
        Public Shared Function LoadLibraryEx(ByVal lpFileName As String, ByVal hFile As IntPtr, ByVal dwFlags As UInteger) As IntPtr
        End Function
        <DllImport("Kernel32.dll", EntryPoint:="LockResource")>
        Public Shared Function LockResource(ByVal hGlobal As IntPtr) As IntPtr
        End Function
        <DllImport("kernel32.dll")>
        Public Shared Function FindResource(ByVal hModule As IntPtr, ByVal lpID As Integer, ByVal lpType As String) As IntPtr
        End Function
        <DllImport("kernel32.dll", SetLastError:=True)>
        Public Shared Function LoadResource(ByVal hModule As IntPtr, ByVal hResInfo As IntPtr) As IntPtr
        End Function
        <DllImport("kernel32.dll", SetLastError:=True)>
        Public Shared Function SizeofResource(ByVal hModule As IntPtr, ByVal hResInfo As IntPtr) As UInteger
        End Function

        <DllImport("kernel32.dll", SetLastError:=True)>
        Public Shared Function Wow64DisableWow64FsRedirection(ByRef ptr As IntPtr) As Boolean
        End Function
        <DllImport("kernel32.dll", SetLastError:=True)>
        Public Shared Function Wow64RevertWow64FsRedirection(ByVal ptr As IntPtr) As Boolean
        End Function

    End Class

    Public Class GDI32
        <DllImport("gdi32.dll", CharSet:=CharSet.Auto)>
        Public Shared Function GetTextMetrics(ByVal hdc As IntPtr, <Out> ByRef lptm As TEXTMETRICW) As Boolean
        End Function
        <DllImport("gdi32.dll", CharSet:=CharSet.Auto, SetLastError:=True)>
        Public Shared Function DeleteObject(ByVal hObject As IntPtr) As Boolean
        End Function
        <DllImport("gdi32.dll", CharSet:=CharSet.Auto)>
        Public Shared Function SelectObject(ByVal hdc As IntPtr, ByVal hgdiObj As IntPtr) As IntPtr
        End Function

        <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Unicode)>
        Public Structure TEXTMETRICW
            Public tmHeight As Integer
            Public tmAscent As Integer
            Public tmDescent As Integer
            Public tmInternalLeading As Integer
            Public tmExternalLeading As Integer
            Public tmAveCharWidth As Integer
            Public tmMaxCharWidth As Integer
            Public tmWeight As Integer
            Public tmOverhang As Integer
            Public tmDigitizedAspectX As Integer
            Public tmDigitizedAspectY As Integer
            Public tmFirstChar As UShort
            Public tmLastChar As UShort
            Public tmDefaultChar As UShort
            Public tmBreakChar As UShort
            Public tmItalic As Byte
            Public tmUnderlined As Byte
            Public tmStruckOut As Byte
            Public tmPitchAndFamily As Byte
            Public tmCharSet As Byte
        End Structure

        Public Shared Iterator Function GetFixedWidthFonts(ByVal dc As IDeviceContext) As IEnumerable(Of FontFamily)
            Dim hDC As IntPtr = dc.GetHdc()

            For Each oFontFamily As System.Drawing.FontFamily In System.Drawing.FontFamily.Families

                Using oFont As System.Drawing.Font = New System.Drawing.Font(oFontFamily, 10)
                    Dim hFont As IntPtr = IntPtr.Zero
                    Dim hFontDefault As IntPtr = IntPtr.Zero

                    Try
                        Dim oTextMetric As TEXTMETRICW
                        hFont = oFont.ToHfont()
                        hFontDefault = SelectObject(hDC, hFont)

                        If GetTextMetrics(hDC, oTextMetric) Then

                            If (oTextMetric.tmPitchAndFamily And 1) = 0 Then
                                Yield oFontFamily
                            End If
                        End If

                    Finally

                        If hFontDefault <> IntPtr.Zero Then
                            SelectObject(hDC, hFontDefault)
                        End If

                        If hFont <> IntPtr.Zero Then
                            DeleteObject(hFont)
                        End If
                    End Try
                End Using
            Next

            dc.ReleaseHdc()
        End Function
    End Class

End Namespace
