Imports System.Drawing.Imaging

Public Class Luna

    'Public Titlebar_Active As Bitmap
    'Public Titlebar_Inactive As Bitmap
    'Public Taskbar As Bitmap
    'Public Border_Left_Active As Bitmap
    'Public Border_Right_Active As Bitmap
    'Public Border_Bottom_Active As Bitmap
    'Public Border_Left_Inactive As Bitmap
    'Public Border_Right_Inactive As Bitmap
    'Public Border_Bottom_Inactive As Bitmap

    Public Start As Bitmap
    Public StartBtn As Bitmap

    Sub New([_ColorStyle] As ColorStyles)
        ColorStyle = [_ColorStyle]

        GetBitmaps()
    End Sub

    Sub GetBitmaps()

        Dim part As Integer

        Select Case ColorStyle
            Case ColorStyles.Blue
                part = 0
                Start = My.Resources.Luna_Start_Blue

            Case ColorStyles.OliveGreen
                part = 1
                Start = My.Resources.Luna_Start_OliveGreen

            Case ColorStyles.Silver
                part = 2
                Start = My.Resources.Luna_Start_Silver

            Case ColorStyles.Empty
                Start = New Bitmap(My.Resources.Luna_Start_Blue.Size.Width, My.Resources.Luna_Start_Blue.Size.Height)

        End Select

        ''Dim ttl As Bitmap = ElementToBitmap(Element.Titlebar, part, True)
        ''Dim i As Integer = ttl.Width / 3
        ''Titlebar_TopLeft_Active = ttl.Clone(New Rectangle(0, 0, i, ttl.Height), ttl.PixelFormat)
        ''Titlebar_TopRight_Active = ttl.Clone(New Rectangle(ttl.Width - i, 0, i, ttl.Height), ttl.PixelFormat)
        ''Titlebar_Shaft_Active = ttl.Clone(New Rectangle(i, 0, i, ttl.Height), ttl.PixelFormat)

        'Taskbar = ElementToBitmap(Element.Taskbar, part, True)
        If Not ColorStyle = ColorStyles.Empty Then StartBtn = ElementToBitmap(Element.StartBtn, part, True) Else StartBtn = New Bitmap(My.Resources.Luna_StartBtn.Size.Width, CInt(My.Resources.Luna_StartBtn.Size.Height / 3))

        'Titlebar_Active = ElementToBitmap(Element.Titlebar, part, True)
        'Titlebar_Inactive = ElementToBitmap(Element.Titlebar, part, False)
        'Border_Left_Active = ElementToBitmap(Element.LeftEdge, part, True)
        'Border_Right_Active = ElementToBitmap(Element.RightEdge, part, True)
        'Border_Bottom_Active = ElementToBitmap(Element.BottomEdge, part, True)
        'Border_Left_Inactive = ElementToBitmap(Element.LeftEdge, part, False)
        'Border_Right_Inactive = ElementToBitmap(Element.RightEdge, part, False)
        'Border_Bottom_Inactive = ElementToBitmap(Element.BottomEdge, part, False)
    End Sub

    Function ElementToBitmap(Element As Element, Part As Integer, Active As Boolean) As Bitmap


        Select Case Element
            'Case Element.Titlebar
            '    Return CropBitmap(My.Resources.Luna_Titlebar, Part, Active)

            'Case Element.LeftEdge
            '    Return CropBitmap(My.Resources.Luna_LeftEdge, Part, Active)

            'Case Element.RightEdge
            '    Return CropBitmap(My.Resources.Luna_RightEdge, Part, Active)

            'Case Element.BottomEdge
            '    Return CropBitmap(My.Resources.Luna_BottomEdge, Part, Active)

            'Case Element.Taskbar
            '    Return CropBitmap(My.Resources.Luna_Taskbar, Part, Active, True)

            Case Element.StartBtn
                Return CropBitmap(My.Resources.Luna_StartBtn, Part, Active, True)

            Case Else
                Return Nothing

        End Select
    End Function

    Function CropBitmap(bm As Bitmap, Part As Integer, Active As Boolean, Optional IsComposedOfThreeParts As Boolean = False) As Bitmap

        Dim OnePartHeight As Integer = bm.Height / 3        'This will calculate height of one part (one part contains Active+Inactive bmp)
        Dim OneElementHeight = OnePartHeight / 2            'This will calculate height of one element in a part (Active or Inactive bmp)

        Dim bm_part As Bitmap = bm.Clone(New Rectangle(0, Part * OnePartHeight, bm.Width, OnePartHeight), PixelFormat.Format32bppArgb)

        If Not IsComposedOfThreeParts Then
            Dim bm_element As Bitmap = bm_part.Clone(New Rectangle(0, If(Active, 0, 1) * OneElementHeight, bm_part.Width, OneElementHeight), PixelFormat.Format32bppArgb)
            Return bm_element
        Else
            Return bm_part
        End If

    End Function

    Property ColorStyle As ColorStyles

    Enum ColorStyles
        Blue
        OliveGreen
        Silver
        Empty
    End Enum

    Enum Element
        Titlebar
        RightEdge
        LeftEdge
        BottomEdge
        Taskbar
        StartBtn
    End Enum

End Class
