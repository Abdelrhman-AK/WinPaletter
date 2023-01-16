Imports WinPaletter.XenonCore

Public Class XenonStyle

    Structure Colors_Structure
        Public BaseColor As Color
        Public Border As Color
        Public Border_Checked As Color
        Public Border_Checked_Hover As Color

        Public Back As Color
        Public Back_Checked As Color
        Public Core As Color
        Public Back_Hover As Color              ''''''''''''''''''''''''''''''''''''''
        Public Border_Hover As Color            ''''''''''''''''''''''''''''''''''''''
    End Structure

    Public Colors As New Colors_Structure With {
                    .BaseColor = Color.FromArgb(0, 81, 210),
                    .Core = Colors.BaseColor.LightLight,
                    .Back = Color.FromArgb(40, 40, 40),
                    .Back_Hover = Color.FromArgb(55, 55, 55),
                    .Back_Checked = Colors.BaseColor.Dark(0.2),
                    .Border = Color.FromArgb(55, 55, 55),
                    .Border_Hover = Color.FromArgb(65, 65, 65),
                    .Border_Checked = Colors.BaseColor.CB(0.08),
                    .Border_Checked_Hover = Colors.BaseColor.CB(-0.2)
                }

    Public Disabled_Colors As New Colors_Structure

    ReadOnly Dark As Boolean = True

    Sub New()

        'Try  'Try is a must because designer can't access My.[Settings] in designer mode
        'If My.[Settings].Appearance_AdaptColors Then
        'Dim x As Byte() = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "AccentPalette", Nothing)
        'Dim Cx As Color = Color.FromArgb(255, x(12), x(13), x(14))
        'BaseColor = Cx 'If(GetDarkMode(), Cx, 0.2), Cx, 0.5))
        'End If
        'Catch
        'End Try

        Dark = GetDarkMode()

        If Dark Then
            Colors.Core = Colors.BaseColor.LightLight
            Colors.Back = Color.FromArgb(40, 40, 40)
            Colors.Back_Hover = Color.FromArgb(55, 55, 55)
            Colors.Back_Checked = Colors.BaseColor.Dark(0.2)
            Colors.Border = Color.FromArgb(55, 55, 55)
            Colors.Border_Hover = Color.FromArgb(65, 65, 65)
            Colors.Border_Checked = Colors.BaseColor.CB(0.08)
            Colors.Border_Checked_Hover = Colors.BaseColor.CB(-0.2)
        Else
            Colors.Core = Colors.BaseColor.Dark(0.1)
            Colors.Back = Color.FromArgb(225, 225, 225)
            Colors.Back_Hover = Color.FromArgb(200, 200, 200)
            Colors.Back_Checked = Colors.BaseColor.LightLight
            Colors.Border = Color.FromArgb(200, 200, 200)
            Colors.Border_Hover = Color.FromArgb(190, 190, 190)
            Colors.Border_Checked = Colors.BaseColor.CB(0.35)
            Colors.Border_Checked_Hover = Colors.BaseColor.CB(0.3)
        End If

        If Dark Then
            Disabled_Colors.Back_Checked = Color.FromArgb(80, 80, 80)
            Disabled_Colors.Core = Color.FromArgb(90, 90, 90)
            Disabled_Colors.Border_Checked_Hover = Color.FromArgb(80, 80, 80)
            Disabled_Colors.Border = Color.FromArgb(90, 90, 90)
            Disabled_Colors.Back = Color.FromArgb(80, 80, 80)
        Else
            Disabled_Colors.Back_Checked = Color.FromArgb(180, 180, 180)
            Disabled_Colors.Core = Color.FromArgb(190, 190, 190)
            Disabled_Colors.Border_Checked_Hover = Color.FromArgb(180, 180, 180)
            Disabled_Colors.Border = Color.FromArgb(190, 190, 190)
            Disabled_Colors.Back = Color.FromArgb(180, 180, 180)
        End If
    End Sub
End Class