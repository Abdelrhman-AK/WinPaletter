Imports System.Reflection
Imports WinPaletter_Converter.ProfilesList

Public Class WinTerminal
    Public Property Enabled As Boolean = False
    Public Property Colors As List(Of TColor)
    Public Property Profiles As List(Of ProfilesList)
    Public Property DefaultProf As ProfilesList
    Public Property Themes As List(Of ThemesList)
    Public Property Theme As String = "system"
    Public Property UseAcrylicInTabRow As Boolean = False

    Public Sub New(str As String, Mode As Mode, Optional [Version] As Version = Version.Stable)

        Select Case Mode
            Case Mode.JSONFile


            Case Mode.WinPaletterFile
                Dim Collected As New List(Of String)
                Dim Preview As Boolean = False

                Dim lst As New List(Of String)
                lst.Clear()
                lst = str.CList

                For Each lin As String In lst

                    Select Case [Version]
                        Case Version.Stable
                            If lin.StartsWith("terminal.", 5) And Not lin.StartsWith("terminalpreview.", 5) Then
                                Collected.Add(lin.Remove(0, "terminal.".Count))
                            End If

                        Case Version.Preview
                            If lin.StartsWith("terminalpreview.", 5) And Not lin.StartsWith("terminal.", 5) Then
                                Collected.Add(lin.Remove(0, "terminalpreview.".Count))
                            End If

                    End Select


                Next

                Dim Defs As New List(Of String)
                Dim CollectedColors, EnumColors As New List(Of String)
                Dim CollectedProfiles, EnumProfiles As New List(Of String)
                Dim CollectedThemes, EnumThemes As New List(Of String)

                Defs.Clear()

                CollectedColors.Clear()
                CollectedProfiles.Clear()
                CollectedThemes.Clear()

                EnumColors.Clear()
                EnumProfiles.Clear()
                EnumThemes.Clear()

                DefaultProf = New ProfilesList
                Colors = New List(Of TColor)
                Profiles = New List(Of ProfilesList)
                Themes = New List(Of ThemesList)

                For Each lin As String In Collected
                    If lin.StartsWith("theme= ", 5) Then Theme = lin.Remove(0, "theme= ".Count)
                    If lin.StartsWith("useacrylicintabrow= ", 5) Then UseAcrylicInTabRow = lin.Remove(0, "useAcrylicInTabRow= ".Count)
                    If lin.StartsWith("enabled= ", 5) Then Enabled = lin.Remove(0, "enabled= ".Count)

                    If lin.StartsWith("default.", 5) Then Defs.Add(lin.Remove(0, "default.".Count))
                    If lin.StartsWith("schemes.", 5) Then CollectedColors.Add(lin.Remove(0, "schemes.".Count))
                    If lin.StartsWith("profiles.", 5) Then CollectedProfiles.Add(lin.Remove(0, "profiles.".Count))
                    If lin.StartsWith("themes.", 5) Then CollectedThemes.Add(lin.Remove(0, "themes.".Count))
                Next

                For Each lin As String In Defs
                    Dim prop As String = lin.Split("=")(0).Trim
                    Dim value As String = lin.Split("=")(1).Trim

                    Select Case prop.ToLower
                        Case "name".ToLower
                            DefaultProf.Name = value

                        Case "BackgroundImage".ToLower
                            DefaultProf.BackgroundImage = value

                        Case "ColorScheme".ToLower
                            DefaultProf.ColorScheme = value

                        Case "TabTitle".ToLower
                            DefaultProf.TabTitle = value

                        Case "Icon".ToLower
                            DefaultProf.Icon = value

                        Case "CursorShape".ToLower
                            DefaultProf.CursorShape = value

                        Case "Font".ToLower
                            DefaultProf.Font.Face = value.Split(",")(0)
                            DefaultProf.Font.Size = value.Split(",")(1)
                            DefaultProf.Font.Weight = FontWeight_GetFromString(value.Split(",")(2))

                        Case "TabColor".ToLower
                            DefaultProf.TabColor = Color.FromArgb(value)

                        Case "UseAcrylic".ToLower
                            DefaultProf.UseAcrylic = value

                        Case "CursorHeight".ToLower
                            DefaultProf.CursorHeight = value

                        Case "Opacity".ToLower
                            DefaultProf.Opacity = value

                        Case "BackgroundImageOpacity".ToLower
                            DefaultProf.BackgroundImageOpacity = value
                    End Select
                Next

                For Each x As String In CollectedProfiles
                    EnumProfiles.Add(x.Split(".")(0))
                Next
                EnumProfiles = EnumProfiles.Distinct.ToList
                For Each x As String In EnumProfiles
                    Dim P As New ProfilesList
                    For Each lin As String In CollectedProfiles
                        If lin.Split("=")(0).Split(".")(0).Trim.ToLower = x.ToLower Then

                            Dim prop As String = lin.Split("=")(0).Split(".")(1).Trim
                            Dim value As String = lin.Split("=")(1).Trim

                            Select Case prop.ToLower
                                Case "Name".ToLower
                                    P.Name = value

                                Case "TabTitle".ToLower
                                    P.TabTitle = value

                                Case "Icon".ToLower
                                    P.Icon = value

                                Case "TabColor".ToLower
                                    P.TabColor = Color.FromArgb(value)

                                Case "UseAcrylic".ToLower
                                    P.UseAcrylic = value

                                Case "Opacity".ToLower
                                    P.Opacity = value

                                Case "Font".ToLower
                                    P.Font.Face = value.Split(",")(0)
                                    P.Font.Size = value.Split(",")(1)
                                    P.Font.Weight = FontWeight_GetFromString(value.Split(",")(2))

                                Case "BackgroundImage".ToLower
                                    P.BackgroundImage = value

                                Case "BackgroundImageOpacity".ToLower
                                    P.BackgroundImageOpacity = value

                                Case "ColorScheme".ToLower
                                    P.ColorScheme = value

                                Case "CursorShape".ToLower
                                    P.CursorShape = value

                                Case "CursorHeight".ToLower
                                    P.CursorHeight = value

                            End Select

                        End If
                    Next

                    Profiles.Add(P)
                Next

                For Each x As String In CollectedColors
                    EnumColors.Add(x.Split(".")(0))
                Next
                EnumColors = EnumColors.Distinct.ToList
                For Each x As String In EnumColors
                    Dim TC As New TColor

                    For Each lin As String In CollectedColors
                        If lin.Split("=")(0).Split(".")(0).Trim.ToLower = x.ToLower Then
                            Dim prop As String = lin.Split("=")(0).Split(".")(1).Trim
                            Dim value As String = lin.Split("=")(1).Trim

                            Select Case prop.ToLower
                                Case "Name".ToLower
                                    TC.Name = value

                                Case "Background".ToLower
                                    TC.Background = Color.FromArgb(value)

                                Case "Foreground".ToLower
                                    TC.Foreground = Color.FromArgb(value)

                                Case "SelectionBackground".ToLower
                                    TC.SelectionBackground = Color.FromArgb(value)

                                Case "Black".ToLower
                                    TC.Black = Color.FromArgb(value)

                                Case "Blue".ToLower
                                    TC.Blue = Color.FromArgb(value)

                                Case "BrightBlack".ToLower
                                    TC.BrightBlack = Color.FromArgb(value)

                                Case "BrightBlue".ToLower
                                    TC.BrightBlue = Color.FromArgb(value)

                                Case "BrightCyan".ToLower
                                    TC.BrightCyan = Color.FromArgb(value)

                                Case "BrightGreen".ToLower
                                    TC.BrightGreen = Color.FromArgb(value)

                                Case "BrightPurple".ToLower
                                    TC.BrightPurple = Color.FromArgb(value)

                                Case "BrightRed".ToLower
                                    TC.BrightRed = Color.FromArgb(value)

                                Case "BrightWhite".ToLower
                                    TC.BrightWhite = Color.FromArgb(value)

                                Case "BrightYellow".ToLower
                                    TC.BrightYellow = Color.FromArgb(value)

                                Case "CursorColor".ToLower
                                    TC.CursorColor = Color.FromArgb(value)

                                Case "Cyan".ToLower
                                    TC.Cyan = Color.FromArgb(value)

                                Case "Green".ToLower
                                    TC.Green = Color.FromArgb(value)

                                Case "Purple".ToLower
                                    TC.Purple = Color.FromArgb(value)

                                Case "Red".ToLower
                                    TC.Red = Color.FromArgb(value)

                                Case "White".ToLower
                                    TC.White = Color.FromArgb(value)

                                Case "Yellow".ToLower
                                    TC.Yellow = Color.FromArgb(value)

                            End Select

                        End If
                    Next

                    Colors.Add(TC)
                Next

                For Each x As String In CollectedThemes
                    EnumThemes.Add(x.Split(".")(0))
                Next
                EnumThemes = EnumThemes.Distinct.ToList
                For Each x As String In EnumThemes
                    Dim Th As New ThemesList

                    For Each lin As String In CollectedThemes
                        If lin.Split("=")(0).Split(".")(0).Trim.ToLower = x.ToLower Then
                            Dim prop As String = lin.Split("=")(0).Split(".")(1).Trim
                            Dim value As String = lin.Split("=")(1).Trim

                            Select Case prop.ToLower
                                Case "Name".ToLower
                                    Th.Name = value

                                Case "Titlebar_Active".ToLower
                                    Th.Titlebar_Active = Color.FromArgb(value)

                                Case "Titlebar_Inactive".ToLower
                                    Th.Titlebar_Inactive = Color.FromArgb(value)

                                Case "Tab_Active".ToLower
                                    Th.Tab_Active = Color.FromArgb(value)

                                Case "Tab_Inactive".ToLower
                                    Th.Tab_Inactive = Color.FromArgb(value)

                                Case "applicationTheme_light".ToLower
                                    Th.ApplicationTheme_light = value

                            End Select

                        End If
                    Next

                    Themes.Add(Th)
                Next

                If Colors.Count = 0 Then
                    Colors.Add(New TColor With {
                         .Name = "Campbell",
                         .Background = "FF0C0C0C".FromHEXToColor(True),
                         .Black = "FF0C0C0C".FromHEXToColor(True),
                         .Blue = "FF0037DA".FromHEXToColor(True),
                         .BrightBlack = "FF767676".FromHEXToColor(True),
                         .BrightBlue = "FF3B78FF".FromHEXToColor(True),
                         .BrightCyan = "FF61D6D6".FromHEXToColor(True),
                         .BrightGreen = "FF16C60C".FromHEXToColor(True),
                         .BrightPurple = "FFB4009E".FromHEXToColor(True),
                         .BrightRed = "FFE74856".FromHEXToColor(True),
                         .BrightWhite = "FFF2F2F2".FromHEXToColor(True),
                         .BrightYellow = "FFF9F1A5".FromHEXToColor(True),
                         .CursorColor = "FFFFFFFF".FromHEXToColor(True),
                         .Cyan = "FF3A96DD".FromHEXToColor(True),
                         .Foreground = "FFCCCCCC".FromHEXToColor(True),
                         .Green = "FF13A10E".FromHEXToColor(True),
                         .Purple = "FF881798".FromHEXToColor(True),
                         .Red = "FFC50F1F".FromHEXToColor(True),
                         .SelectionBackground = "FFFFFFFF".FromHEXToColor(True),
                         .White = "FFCCCCCC".FromHEXToColor(True),
                         .Yellow = "FFC19C00".FromHEXToColor(True)
                         })
                End If

            Case Mode.Empty

                Profiles = New List(Of ProfilesList)
                Colors = New List(Of TColor)
                DefaultProf = New ProfilesList
                Themes = New List(Of ThemesList)

                Colors.Add(New TColor With {
                           .Name = "Campbell",
                           .Background = "FF0C0C0C".FromHEXToColor(True),
                           .Black = "FF0C0C0C".FromHEXToColor(True),
                           .Blue = "FF0037DA".FromHEXToColor(True),
                           .BrightBlack = "FF767676".FromHEXToColor(True),
                           .BrightBlue = "FF3B78FF".FromHEXToColor(True),
                           .BrightCyan = "FF61D6D6".FromHEXToColor(True),
                           .BrightGreen = "FF16C60C".FromHEXToColor(True),
                           .BrightPurple = "FFB4009E".FromHEXToColor(True),
                           .BrightRed = "FFE74856".FromHEXToColor(True),
                           .BrightWhite = "FFF2F2F2".FromHEXToColor(True),
                           .BrightYellow = "FFF9F1A5".FromHEXToColor(True),
                           .CursorColor = "FFFFFFFF".FromHEXToColor(True),
                           .Cyan = "FF3A96DD".FromHEXToColor(True),
                           .Foreground = "FFCCCCCC".FromHEXToColor(True),
                           .Green = "FF13A10E".FromHEXToColor(True),
                           .Purple = "FF881798".FromHEXToColor(True),
                           .Red = "FFC50F1F".FromHEXToColor(True),
                           .SelectionBackground = "FFFFFFFF".FromHEXToColor(True),
                           .White = "FFCCCCCC".FromHEXToColor(True),
                           .Yellow = "FFC19C00".FromHEXToColor(True)
                           })

        End Select
    End Sub

    Enum Mode As Integer
        JSONFile
        WinPaletterFile
        Empty
    End Enum

    Enum Version As Integer
        Stable
        Preview
    End Enum

    Public Function Save(File As String, Mode As Mode, Optional [Version] As Version = Version.Stable) As String
        Select Case Mode
            Case Mode.JSONFile

            Case Mode.WinPaletterFile

                Dim First As String = ""

                Select Case [Version]
                    Case Version.Stable
                        First = "Terminal."

                    Case Version.Preview
                        First = "TerminalPreview."

                End Select

                Dim S As New List(Of String)
                S.Clear()

                Try
                    Try : S.Add(String.Format("{0}{1}= {2}", First, "theme", Theme)) : Catch : End Try
                    Try : S.Add(String.Format("{0}{1}= {2}", First, "Enabled", Enabled)) : Catch : End Try
                    Try : S.Add(String.Format("{0}{1}= {2}", First, "useAcrylicInTabRow", UseAcrylicInTabRow)) : Catch : End Try
                Catch
                End Try

                Try
                    Dim type1 As Type = DefaultProf.[GetType]() : Dim properties1 As PropertyInfo() = type1.GetProperties()

                    For Each [property] As PropertyInfo In properties1
                        Try
                            If [property].GetValue(Me.DefaultProf) IsNot Nothing Then
                                S.Add(String.Format("{0}{1}.{2}= {3}", First, "Default", [property].Name, ReturnPerfectValue([property].PropertyType, [property].GetValue(Me.DefaultProf))))
                            End If
                        Catch
                        End Try
                    Next
                Catch
                End Try

                Try

                    For Each c As TColor In Colors
                        Dim type2 As Type = c.[GetType]() : Dim properties2 As PropertyInfo() = type2.GetProperties()

                        For Each [property] As PropertyInfo In properties2
                            Try
                                If [property].GetValue(c) IsNot Nothing Then
                                    S.Add(String.Format("{0}{1}.{2}.{3}= {4}", First, "Schemes", c.Name.Replace(" ", "").Replace(".", ""), [property].Name, ReturnPerfectValue([property].PropertyType, [property].GetValue(c))))
                                End If
                            Catch
                            End Try
                        Next

                    Next
                Catch
                End Try

                Try
                    For Each c As ProfilesList In Profiles
                        Dim type2 As Type = c.[GetType]() : Dim properties2 As PropertyInfo() = type2.GetProperties()
                        Try
                            For Each [property] As PropertyInfo In properties2
                                If [property].GetValue(c) IsNot Nothing And [property].Name <> "commandline" Then
                                    S.Add(String.Format("{0}{1}.{2}.{3}= {4}", First, "Profiles", c.Name.Replace(" ", "").Replace(".", ""), [property].Name, ReturnPerfectValue([property].PropertyType, [property].GetValue(c))))
                                End If
                            Next
                        Catch
                        End Try
                    Next
                Catch
                End Try

                Try
                    For Each c As ThemesList In Themes
                        Dim type2 As Type = c.[GetType]() : Dim properties2 As PropertyInfo() = type2.GetProperties()
                        For Each [property] As PropertyInfo In properties2
                            Try
                                If [property].GetValue(c) IsNot Nothing Then
                                    S.Add(String.Format("{0}{1}.{2}.{3}= {4}", First, "Themes", c.Name.Replace(" ", "").Replace(".", ""), [property].Name, ReturnPerfectValue([property].PropertyType, [property].GetValue(c))))
                                End If
                            Catch
                            End Try
                        Next
                    Next
                Catch
                End Try

                Return S.CString

            Case Else
                Return ""

        End Select
    End Function

    Public Overloads Function ToString(Signature As String, Edition As Version) As String
        Dim tx As New List(Of String)
        tx.Clear()
        tx.Add(String.Format("<{0}>", Signature))
        tx.Add(Save("", Mode.WinPaletterFile, Edition))
        tx.Add(String.Format("</{0}>", Signature))
        Return tx.CString
    End Function

    Public Function ReturnPerfectValue(ByVal Type As Type, Value As Object) As String
        Select Case Type.Name.ToLower
            Case "color"
                Return DirectCast(Value, Color).ToArgb

            Case "padding"
                With DirectCast(Value, Padding)
                    Return .Left & "," & .Top & "," & .Right & "," & .Bottom
                End With

            Case "fontsbase"
                With DirectCast(Value, FontsBase)
                    Return .Face & "," & .Size & "," & FontWeight_ReturnToString(.Weight)
                End With

            Case Else
                Return Value

        End Select
    End Function
End Class

Public Class TColor

    Public Property Name As String
    Public Property Background As Color = "FF0C0C0C".FromHEXToColor(True)
    Public Property Foreground As Color = "FFCCCCCC".FromHEXToColor(True)
    Public Property SelectionBackground As Color = "FFFFFFFF".FromHEXToColor(True)
    Public Property Black As Color = "FF0C0C0C".FromHEXToColor(True)
    Public Property Blue As Color = "FF0037DA".FromHEXToColor(True)
    Public Property BrightBlack As Color = "FF767676".FromHEXToColor(True)
    Public Property BrightBlue As Color = "FF3B78FF".FromHEXToColor(True)
    Public Property BrightCyan As Color = "FF61D6D6".FromHEXToColor(True)
    Public Property BrightGreen As Color = "FF16C60C".FromHEXToColor(True)
    Public Property BrightPurple As Color = "FFB4009E".FromHEXToColor(True)
    Public Property BrightRed As Color = "FFE74856".FromHEXToColor(True)
    Public Property BrightWhite As Color = "FFF2F2F2".FromHEXToColor(True)
    Public Property BrightYellow As Color = "FFF9F1A5".FromHEXToColor(True)
    Public Property CursorColor As Color = "FFFFFFFF".FromHEXToColor(True)
    Public Property Cyan As Color = "FF3A96DD".FromHEXToColor(True)
    Public Property Green As Color = "FF13A10E".FromHEXToColor(True)
    Public Property Purple As Color = "FF881798".FromHEXToColor(True)
    Public Property Red As Color = "FFC50F1F".FromHEXToColor(True)
    Public Property White As Color = "FFCCCCCC".FromHEXToColor(True)
    Public Property Yellow As Color = "FFC19C00".FromHEXToColor(True)
End Class
Public Class ThemesList
    Public Property Name As String
    Public Property Titlebar_Active As Color = Color.FromArgb(0, 0, 0, 0)
    Public Property Titlebar_Inactive As Color = Color.FromArgb(0, 0, 0, 0)
    Public Property Tab_Active As Color = Color.FromArgb(0, 0, 0, 0)
    Public Property Tab_Inactive As Color = Color.FromArgb(0, 0, 0, 0)
    Public Property ApplicationTheme_light As String = "dark"
End Class
Public Class FontsBase
    Public Property Face As String = If(My.W11, "Cascadia Mono", "Consolas")
    Public Property Weight As FontWeight_Enum = FontWeight_Enum.normal
    Public Property Size As Integer = 12
End Class
Public Class ProfilesList
    Public Property Name As String
    Public Property TabTitle As String = ""
    Public Property Icon As String = ""
    Public Property Commandline As String

    Public Property TabColor As Color = Color.FromArgb(0, 0, 0, 0)
    Public Property UseAcrylic As Boolean = False
    Public Property Opacity As Integer = 100

    Public Property Font As New FontsBase
    Public Property BackgroundImage As String = ""
    Public Property BackgroundImageOpacity As Single = 1

    Public Property ColorScheme As String = "Campbell"
    Public Property CursorShape As CursorShape_Enum = CursorShape_Enum.bar
    Public Property CursorHeight As Integer = 25

#Region "Helpers"
    Enum CursorShape_Enum
        bar
        doubleUnderscore
        emptyBox
        filledBox
        underscore
        vintage
    End Enum

    Enum FontWeight_Enum   'replace _ by -
        thin
        extra_light
        light
        semi_light
        normal
        medium
        semi_bold
        bold
        extra_bold
        black
        extra_black
    End Enum
    Public Shared Function FontWeight_ReturnToString(int As FontWeight_Enum) As String
        Select Case int
            Case FontWeight_Enum.black
                Return "black"

            Case FontWeight_Enum.bold
                Return "bold"

            Case FontWeight_Enum.extra_black
                Return "extra-black"

            Case FontWeight_Enum.extra_bold
                Return "extra-bold"

            Case FontWeight_Enum.extra_light
                Return "extra-light"

            Case FontWeight_Enum.light
                Return "light"

            Case FontWeight_Enum.medium
                Return "medium"

            Case FontWeight_Enum.normal
                Return "normal"

            Case FontWeight_Enum.semi_bold
                Return "semi-bold"

            Case FontWeight_Enum.semi_light
                Return "semi-light"

            Case FontWeight_Enum.thin
                Return "thin"

            Case Else
                Return "normal"

        End Select
    End Function
    Public Shared Function FontWeight_GetFromString(str As String) As FontWeight_Enum
        Select Case str.ToLower

            Case "thin".ToLower
                Return FontWeight_Enum.thin

            Case "extra-light".ToLower
                Return FontWeight_Enum.extra_light

            Case "light".ToLower
                Return FontWeight_Enum.light

            Case "semi-light".ToLower
                Return FontWeight_Enum.semi_light

            Case "medium".ToLower
                Return FontWeight_Enum.medium

            Case "normal".ToLower
                Return FontWeight_Enum.normal

            Case "semi-bold".ToLower
                Return FontWeight_Enum.semi_bold

            Case "bold".ToLower
                Return FontWeight_Enum.bold

            Case "extra-bold".ToLower
                Return FontWeight_Enum.extra_bold

            Case "black".ToLower
                Return FontWeight_Enum.black

            Case "extra-black".ToLower
                Return FontWeight_Enum.extra_black

            Case Else
                Return FontWeight_Enum.normal

        End Select
    End Function
#End Region
End Class