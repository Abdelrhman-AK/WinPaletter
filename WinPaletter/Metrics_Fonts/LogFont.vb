Imports System.Runtime.InteropServices
Imports System.Text

<StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Auto)>
Public Class LogFont
    Public lfHeight As Integer
    Public lfWidth As Integer
    Public lfEscapement As Integer
    Public lfOrientation As Integer
    Public lfWeight As Integer
    Public lfItalic As Byte
    Public lfUnderline As Byte
    Public lfStrikeOut As Byte
    Public lfCharSet As Byte
    Public lfOutPrecision As Byte
    Public lfClipPrecision As Byte
    Public lfQuality As Byte
    Public lfPitchAndFamily As Byte

    '<see cref="UnmanagedType.ByValTStr"/> means that the string should be marshalled as an array of TCHAR embedded in the structure.
    'This implies that the font names can be no larger than <see cref="LF_FACESIZE"/> including the terminating '\0'. That works out to 31 characters.
    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=32)>
    Public lfFaceName As String = String.Empty

    Public Sub New(Optional lfFaceName As String = Nothing)
        Me.lfFaceName = lfFaceName
        Me.lfHeight = 0
        Me.lfWidth = 0
        Me.lfEscapement = 0
        Me.lfOrientation = 0
        Me.lfWeight = 0
        Me.lfItalic = 0
        Me.lfUnderline = 0
        Me.lfStrikeOut = 0
        Me.lfCharSet = 0
        Me.lfOutPrecision = 0
        Me.lfClipPrecision = 0
        Me.lfQuality = 0
        Me.lfPitchAndFamily = 0
    End Sub

End Class

<StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Auto)>
Public Structure LogFontStr
    Public lfHeight As Integer
    Public lfWidth As Integer
    Public lfEscapement As Integer
    Public lfOrientation As Integer
    Public lfWeight As Integer
    Public lfItalic As Byte
    Public lfUnderline As Byte
    Public lfStrikeOut As Byte
    Public lfCharSet As Byte
    Public lfOutPrecision As Byte
    Public lfClipPrecision As Byte
    Public lfQuality As Byte
    Public lfPitchAndFamily As Byte

    '<see cref="UnmanagedType.ByValTStr"/> means that the string should be marshalled as an array of TCHAR embedded in the structure.
    'This implies that the font names can be no larger than <see cref="LF_FACESIZE"/> including the terminating '\0'. That works out to 31 characters.
    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=32)>
    Public lfFaceName As String '= String.Empty

End Structure


Public Class LogFontHelper
    Public Enum FontWeight As Integer
        FW_DONTCARE = 0
        FW_THIN = 100
        FW_EXTRALIGHT = 200
        FW_LIGHT = 300
        FW_NORMAL = 400
        FW_MEDIUM = 500
        FW_SEMIBOLD = 600
        FW_BOLD = 700
        FW_EXTRABOLD = 800
        FW_HEAVY = 900
    End Enum

    Public Enum FontCharSet As Byte
        ANSI_CHARSET = 0
        DEFAULT_CHARSET = 1
        SYMBOL_CHARSET = 2
        SHIFTJIS_CHARSET = 128
        HANGEUL_CHARSET = 129
        HANGUL_CHARSET = 129
        GB2312_CHARSET = 134
        CHINESEBIG5_CHARSET = 136
        OEM_CHARSET = 255
        JOHAB_CHARSET = 130
        HEBREW_CHARSET = 177
        ARABIC_CHARSET = 178
        GREEK_CHARSET = 161
        TURKISH_CHARSET = 162
        VIETNAMESE_CHARSET = 163
        THAI_CHARSET = 222
        EASTEUROPE_CHARSET = 238
        RUSSIAN_CHARSET = 204
        MAC_CHARSET = 77
        BALTIC_CHARSET = 186
    End Enum

    Public Enum FontPrecision As Byte
        OUT_DEFAULT_PRECIS = 0
        OUT_STRING_PRECIS = 1
        OUT_CHARACTER_PRECIS = 2
        OUT_STROKE_PRECIS = 3
        OUT_TT_PRECIS = 4
        OUT_DEVICE_PRECIS = 5
        OUT_RASTER_PRECIS = 6
        OUT_TT_ONLY_PRECIS = 7
        OUT_OUTLINE_PRECIS = 8
        OUT_SCREEN_OUTLINE_PRECIS = 9
        OUT_PS_ONLY_PRECIS = 10
    End Enum

    Public Enum FontClipPrecision As Byte
        CLIP_DEFAULT_PRECIS = 0
        CLIP_CHARACTER_PRECIS = 1
        CLIP_STROKE_PRECIS = 2
        CLIP_MASK = &HF
        CLIP_LH_ANGLES = (1 << 4)
        CLIP_TT_ALWAYS = (2 << 4)
        CLIP_DFA_DISABLE = (4 << 4)
        CLIP_EMBEDDED = (8 << 4)
    End Enum

    Public Enum FontQuality As Byte
        DEFAULT_QUALITY = 0
        DRAFT_QUALITY = 1
        PROOF_QUALITY = 2
        NONANTIALIASED_QUALITY = 3
        ANTIALIASED_QUALITY = 4
        CLEARTYPE_QUALITY = 5
        CLEARTYPE_NATURAL_QUALITY = 6
    End Enum

    Public Enum FontPitchAndFamily As Byte
        DEFAULT_PITCH = 0
        FIXED_PITCH = 1
        VARIABLE_PITCH = 2
        FF_DONTCARE = (0 << 4)
        FF_ROMAN = (1 << 4)
        FF_SWISS = (2 << 4)
        FF_MODERN = (3 << 4)
        FF_SCRIPT = (4 << 4)
        FF_DECORATIVE = (5 << 4)
    End Enum

    Public Shared Function ByteToLogFont(ByVal fontBytes As Byte()) As LogFont
        Dim lOGFONT As LogFont = New LogFont()
        lOGFONT.lfHeight = BitConverter.ToInt32(fontBytes, 0)
        lOGFONT.lfWidth = 0
        lOGFONT.lfEscapement = 0
        lOGFONT.lfOrientation = 0
        lOGFONT.lfWeight = BitConverter.ToInt32(fontBytes, 16)
        lOGFONT.lfItalic = fontBytes(20)
        lOGFONT.lfUnderline = fontBytes(21)
        lOGFONT.lfStrikeOut = fontBytes(22)
        lOGFONT.lfCharSet = fontBytes(23)
        lOGFONT.lfOutPrecision = fontBytes(24)
        lOGFONT.lfClipPrecision = fontBytes(25)
        lOGFONT.lfQuality = fontBytes(26)
        lOGFONT.lfClipPrecision = fontBytes(27)
        Dim array As Byte() = New Byte(63) {}

        For i As Integer = 0 To 64 - 1
            array(i) = fontBytes(i + 28)
        Next

        lOGFONT.lfFaceName = Encoding.Unicode.GetString(array).TrimEnd(Nothing)
        Return lOGFONT
    End Function

    Public Shared Function LogFontToByte(ByVal lOGFONT As LogFont) As Byte()
        Dim b As Byte() = New Byte(91) {}

        For x = 0 To 3 Step +1
            b(x) = BitConverter.GetBytes(lOGFONT.lfHeight)(x)
        Next

        For x = 4 To 15 Step +1
            b(x) = 0
        Next

        For x = 16 To 19 Step +1
            b(x) = BitConverter.GetBytes(lOGFONT.lfWeight)(x - 16)
        Next

        b(20) = lOGFONT.lfItalic
        b(21) = lOGFONT.lfUnderline
        b(22) = lOGFONT.lfStrikeOut
        b(23) = lOGFONT.lfCharSet
        b(24) = lOGFONT.lfOutPrecision
        b(25) = lOGFONT.lfClipPrecision
        b(26) = lOGFONT.lfQuality
        b(27) = lOGFONT.lfClipPrecision


        For Each x As Byte In Encoding.Unicode.GetBytes(lOGFONT.lfFaceName)
            Dim i As Integer
            b(28 + i) = x
            i += 1
        Next

        Return b
    End Function
End Class