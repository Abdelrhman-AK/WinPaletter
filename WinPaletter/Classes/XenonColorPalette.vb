Imports WinPaletter.XenonCore
Public Class XenonColorPalette

    Public BaseColor As Color = Color.FromArgb(0, 81, 210)
    Public Color_Border As Color
    Public Color_Border_Checked_Hover As Color
    Public Color_Back As Color
    Public Color_Back_Checked As Color
    Public Color_Core As Color
    Public Color_Parent As Color
    Public Color_Parent_Hover As Color

    Public Color_Border_Checked As Color          ''''''''''''''''''''''''''''''''''''''
    Public Color_Back_Hover As Color              ''''''''''''''''''''''''''''''''''''''
    Public Color_Border_Hover As Color            ''''''''''''''''''''''''''''''''''''''

    Dim Dark As Boolean = True

    Sub New(Optional ByVal [Control] As Control = Nothing)

        If [Control] Is Nothing Then Exit Sub

        'BaseColor = GetTitlebarColor([Control], True)

        Color_Parent = GetParentColor([Control])

        Dark = GetDarkMode()

        If Control.Enabled Then
            If Dark Then
                Color_Back_Checked = ControlPaint.Dark(BaseColor, 0.2)
                Color_Core = ControlPaint.LightLight(BaseColor)
                Color_Border_Checked_Hover = CCB(BaseColor, -0.2)
                Color_Border = CCB(Color_Parent, 0.3)
                Color_Back = CCB(Color_Parent, 0.07)
                Color_Parent_Hover = CCB(Color_Parent, 0.08)
            Else
                Color_Back_Checked = ControlPaint.LightLight(BaseColor)
                Color_Core = ControlPaint.Dark(BaseColor, 0.1)
                Color_Border_Checked_Hover = CCB(BaseColor, 0.2)
                Color_Border = CCB(Color_Parent, -0.3)
                Color_Back = CCB(Color_Parent, -0.07)
                Color_Parent_Hover = CCB(Color_Parent, -0.08)
            End If
        Else
            If Dark Then
                Color_Back_Checked = Color.FromArgb(80, 80, 80)
                Color_Core = Color.FromArgb(90, 90, 90)
                Color_Border_Checked_Hover = Color.FromArgb(80, 80, 80)
                Color_Border = Color.FromArgb(90, 90, 90)
                Color_Back = Color.FromArgb(80, 80, 80)
                Color_Parent_Hover = Color.FromArgb(90, 90, 90)
            Else
                Color_Back_Checked = Color.FromArgb(180, 180, 180)
                Color_Core = Color.FromArgb(190, 190, 190)
                Color_Border_Checked_Hover = Color.FromArgb(180, 180, 180)
                Color_Border = Color.FromArgb(190, 190, 190)
                Color_Back = Color.FromArgb(180, 180, 180)
                Color_Parent_Hover = Color.FromArgb(190, 190, 190)
            End If
        End If

        Control.Invalidate()
    End Sub
End Class
