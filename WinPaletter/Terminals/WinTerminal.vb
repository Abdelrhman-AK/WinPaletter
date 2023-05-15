Imports System.IO
Imports System.Reflection
Imports Newtonsoft.Json.Linq
Imports WinPaletter.ProfilesList
Imports WinPaletter.XenonCore

Public Class WinTerminal : Implements ICloneable

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
                If IO.File.Exists(str) Then
                    Dim St As New StreamReader(str)
                    Dim JSON_String As String = St.ReadToEnd
                    Dim JSonFile As JObject = JObject.Parse(JSON_String)

                    If JSonFile("useAcrylicInTabRow") IsNot Nothing Then UseAcrylicInTabRow = JSonFile("useAcrylicInTabRow")
                    If JSonFile("theme") IsNot Nothing Then Theme = JSonFile("theme")


#Region "Getting Default Profile"
                    DefaultProf = New ProfilesList

                    If JSonFile("profiles")("defaults")("name") IsNot Nothing Then DefaultProf.Name = JSonFile("profiles")("defaults")("name")
                    If JSonFile("profiles")("defaults")("backgroundImage") IsNot Nothing Then DefaultProf.BackgroundImage = JSonFile("profiles")("defaults")("backgroundImage")
                    If JSonFile("profiles")("defaults")("cursorShape") IsNot Nothing Then DefaultProf.CursorShape = CursorShape_GetFromString(JSonFile("profiles")("defaults")("cursorShape"))

                    If JSonFile("profiles")("defaults")("font") IsNot Nothing Then
                        If JSonFile("profiles")("defaults")("font")("weight") IsNot Nothing Then DefaultProf.Font.Weight = FontWeight_GetFromString(JSonFile("profiles")("defaults")("font")("weight"))
                        If JSonFile("profiles")("defaults")("font")("face") IsNot Nothing Then DefaultProf.Font.Face = JSonFile("profiles")("defaults")("font")("face")
                        If JSonFile("profiles")("defaults")("font")("size") IsNot Nothing Then DefaultProf.Font.Size = JSonFile("profiles")("defaults")("font")("size")
                    End If

                    If JSonFile("profiles")("defaults")("colorScheme") IsNot Nothing Then DefaultProf.ColorScheme = JSonFile("profiles")("defaults")("colorScheme")
                    If JSonFile("profiles")("defaults")("tabTitle") IsNot Nothing Then DefaultProf.TabTitle = JSonFile("profiles")("defaults")("tabTitle")
                    If JSonFile("profiles")("defaults")("icon") IsNot Nothing Then DefaultProf.Icon = JSonFile("profiles")("defaults")("icon")

                    If JSonFile("profiles")("defaults")("tabColor") IsNot Nothing Then
                        DefaultProf.TabColor = HEX2RGB(JSonFile("profiles")("defaults")("tabColor"))
                    End If

                    If JSonFile("profiles")("defaults")("useAcrylic") IsNot Nothing Then DefaultProf.UseAcrylic = JSonFile("profiles")("defaults")("useAcrylic")
                    If JSonFile("profiles")("defaults")("cursorHeight") IsNot Nothing Then DefaultProf.CursorHeight = JSonFile("profiles")("defaults")("cursorHeight")
                    If JSonFile("profiles")("defaults")("opacity") IsNot Nothing Then DefaultProf.Opacity = JSonFile("profiles")("defaults")("opacity")
                    If JSonFile("profiles")("defaults")("backgroundImageOpacity") IsNot Nothing Then DefaultProf.BackgroundImageOpacity = JSonFile("profiles")("defaults")("backgroundImageOpacity")
#End Region

#Region "Getting Profiles"
                    Profiles = New List(Of ProfilesList)
                    Profiles.Clear()

                    If JSonFile("profiles")("list") IsNot Nothing Then
                        For Each item In JSonFile("profiles")("list")
                            Dim P As New ProfilesList
                            If item("name") IsNot Nothing Then P.Name = item("name")
                            If item("backgroundImage") IsNot Nothing Then P.BackgroundImage = item("backgroundImage")
                            If item("cursorShape") IsNot Nothing Then P.CursorShape = CursorShape_GetFromString(item("cursorShape"))

                            If item("font") IsNot Nothing Then
                                If item("font")("weight") IsNot Nothing Then P.Font.Weight = FontWeight_GetFromString(item("font")("weight"))
                                If item("font")("face") IsNot Nothing Then P.Font.Face = item("font")("face")
                                If item("font")("size") IsNot Nothing Then P.Font.Size = item("font")("size")
                            End If
                            '
                            If item("commandline") IsNot Nothing Then P.Commandline = item("commandline")
                            If item("colorScheme") IsNot Nothing Then P.ColorScheme = item("colorScheme") Else P.ColorScheme = DefaultProf.ColorScheme
                            If item("tabTitle") IsNot Nothing Then P.TabTitle = item("tabTitle")
                            If item("icon") IsNot Nothing Then P.Icon = item("icon")
                            If item("tabColor") IsNot Nothing Then P.TabColor = HEX2RGB(item("tabColor"))
                            If item("useAcrylic") IsNot Nothing Then P.UseAcrylic = item("useAcrylic")
                            If item("cursorHeight") IsNot Nothing Then P.CursorHeight = item("cursorHeight")
                            If item("opacity") IsNot Nothing Then P.Opacity = item("opacity")
                            If item("backgroundImageOpacity") IsNot Nothing Then P.BackgroundImageOpacity = item("backgroundImageOpacity")

                            Profiles.Add(P)
                        Next
                    End If

#End Region

#Region "Getting All Colors Schemes"
                    Colors = New List(Of TColor)
                    Colors.Clear()

                    If JSonFile("schemes") IsNot Nothing Then
                        For Each item In JSonFile("schemes")
                            Dim TC As New TColor

                            If item("background") IsNot Nothing Then TC.Background = HEX2RGB(item("background"))
                            If item("black") IsNot Nothing Then TC.Black = HEX2RGB(item("black"))
                            If item("blue") IsNot Nothing Then TC.Blue = HEX2RGB(item("blue"))
                            If item("brightBlack") IsNot Nothing Then TC.BrightBlack = HEX2RGB(item("brightBlack"))
                            If item("brightBlue") IsNot Nothing Then TC.BrightBlue = HEX2RGB(item("brightBlue"))
                            If item("brightCyan") IsNot Nothing Then TC.BrightCyan = HEX2RGB(item("brightCyan"))
                            If item("brightGreen") IsNot Nothing Then TC.BrightGreen = HEX2RGB(item("brightGreen"))
                            If item("brightPurple") IsNot Nothing Then TC.BrightPurple = HEX2RGB(item("brightPurple"))
                            If item("brightRed") IsNot Nothing Then TC.BrightRed = HEX2RGB(item("brightRed"))
                            If item("brightWhite") IsNot Nothing Then TC.BrightWhite = HEX2RGB(item("brightWhite"))
                            If item("brightYellow") IsNot Nothing Then TC.BrightYellow = HEX2RGB(item("brightYellow"))
                            If item("cursorColor") IsNot Nothing Then TC.CursorColor = HEX2RGB(item("cursorColor"))
                            If item("cyan") IsNot Nothing Then TC.Cyan = HEX2RGB(item("cyan"))
                            If item("foreground") IsNot Nothing Then TC.Foreground = HEX2RGB(item("foreground"))
                            If item("green") IsNot Nothing Then TC.Green = HEX2RGB(item("green"))
                            If item("name") IsNot Nothing Then TC.Name = item("name")
                            If item("purple") IsNot Nothing Then TC.Purple = HEX2RGB(item("purple"))
                            If item("red") IsNot Nothing Then TC.Red = HEX2RGB(item("red"))
                            If item("selectionBackground") IsNot Nothing Then TC.SelectionBackground = HEX2RGB(item("selectionBackground"))
                            If item("white") IsNot Nothing Then TC.White = HEX2RGB(item("white"))
                            If item("yellow") IsNot Nothing Then TC.Yellow = HEX2RGB(item("yellow"))

                            Colors.Add(TC)
                        Next
                    End If

#End Region

#Region "Getting All Themes"
                    Themes = New List(Of ThemesList)
                    Themes.Clear()

                    If JSonFile("themes") IsNot Nothing Then
                        For Each item In JSonFile("themes")
                            Dim Th As New ThemesList
                            If item("name") IsNot Nothing Then Th.Name = item("name")
                            If item("tabRow")("background") IsNot Nothing Then Th.Titlebar_Active = HEX2RGB(item("tabRow")("background"))
                            If item("tabRow")("unfocusedBackground") IsNot Nothing Then Th.Titlebar_Inactive = HEX2RGB(item("tabRow")("unfocusedBackground"))
                            If item("tab")("background") IsNot Nothing Then Th.Tab_Active = HEX2RGB(item("tab")("background"))
                            If item("tab")("unfocusedBackground") IsNot Nothing Then Th.Tab_Inactive = HEX2RGB(item("tab")("unfocusedBackground"))
                            If item("window")("applicationTheme") IsNot Nothing Then Th.ApplicationTheme_light = item("window")("applicationTheme")
                            Themes.Add(Th)
                        Next
                    End If

#End Region
                    St.Close()

                Else
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

                    MsgBox(My.Lang.Terminal_SettingsNotExist, MsgBoxStyle.Critical, str)
                End If

            Case Mode.WinPaletterFile
                Using CPx As New CP(CP.CP_Type.File, str)

                    Select Case [Version]
                        Case Version.Stable
                            Dim bindingFlags As BindingFlags = BindingFlags.Instance Or BindingFlags.NonPublic Or BindingFlags.Public
                            For Each field As FieldInfo In Me.GetType.GetFields(bindingFlags)
                                Dim type As Type = field.FieldType
                                field.SetValue(Me, field.GetValue(CPx.TerminalPreview))
                            Next

                        Case Version.Preview
                            Dim bindingFlags As BindingFlags = BindingFlags.Instance Or BindingFlags.NonPublic Or BindingFlags.Public
                            For Each field As FieldInfo In Me.GetType.GetFields(bindingFlags)
                                Dim type As Type = field.FieldType
                                field.SetValue(Me, field.GetValue(CPx.TerminalPreview))
                            Next

                    End Select
                End Using

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
        [Default]
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
                Dim SettingsFile As String = ""

                Select Case [Version]
                    Case Version.Stable
                        If Not My.[Settings].Terminal_Path_Deflection Then
                            SettingsFile = My.PATH_TerminalJSON
                        Else
                            If IO.File.Exists(My.[Settings].Terminal_Stable_Path) Then
                                SettingsFile = My.[Settings].Terminal_Stable_Path
                            Else
                                SettingsFile = My.PATH_TerminalJSON
                            End If
                        End If

                    Case Version.Preview
                        If Not My.[Settings].Terminal_Path_Deflection Then
                            SettingsFile = My.PATH_TerminalPreviewJSON
                        Else
                            If IO.File.Exists(My.[Settings].Terminal_Stable_Path) Then
                                SettingsFile = My.[Settings].Terminal_Stable_Path
                            Else
                                SettingsFile = My.PATH_TerminalPreviewJSON
                            End If
                        End If

                End Select


                Dim St As New StreamReader(SettingsFile)
                Dim JSON_String As String = St.ReadToEnd
                Dim JSonFile As JObject = JObject.Parse(JSON_String)
                Dim JSonFileUntouched As JObject = JObject.Parse(JSON_String)

#Region "Global Settings"
                JSonFile("useAcrylicInTabRow") = UseAcrylicInTabRow
                JSonFile("theme") = Theme
#End Region

#Region "Schemes"
                If JSonFile("schemes") IsNot Nothing Then CType(JSonFile("schemes"), JArray).Clear() Else JSonFile("schemes") = New JArray()
                For x = 0 To Colors.Count - 1
                    Dim JS As New JObject
                    JS("background") = RGB2HEX(Colors(x).Background)
                    JS("black") = RGB2HEX(Colors(x).Black)
                    JS("blue") = RGB2HEX(Colors(x).Blue)
                    JS("brightBlack") = RGB2HEX(Colors(x).BrightBlack)
                    JS("brightBlue") = RGB2HEX(Colors(x).BrightBlue)
                    JS("brightCyan") = RGB2HEX(Colors(x).BrightCyan)
                    JS("brightGreen") = RGB2HEX(Colors(x).BrightGreen)
                    JS("brightPurple") = RGB2HEX(Colors(x).BrightPurple)
                    JS("brightRed") = RGB2HEX(Colors(x).BrightRed)
                    JS("brightWhite") = RGB2HEX(Colors(x).BrightWhite)
                    JS("brightYellow") = RGB2HEX(Colors(x).BrightYellow)
                    JS("cursorColor") = RGB2HEX(Colors(x).CursorColor)
                    JS("cyan") = RGB2HEX(Colors(x).Cyan)
                    JS("green") = RGB2HEX(Colors(x).Green)
                    JS("name") = Colors(x).Name
                    JS("purple") = RGB2HEX(Colors(x).Purple)
                    JS("red") = RGB2HEX(Colors(x).Red)
                    JS("selectionBackground") = RGB2HEX(Colors(x).SelectionBackground)
                    JS("white") = RGB2HEX(Colors(x).White)
                    JS("yellow") = RGB2HEX(Colors(x).Yellow)
                    JS("foreground") = RGB2HEX(Colors(x).Foreground)

                    '# Check for properties reminants from the old JSON to be added to the new one
                    For Each item In JSonFileUntouched("schemes")
                        If item("name").ToString.ToLower = JS("name").ToString.ToLower Then
                            For Each itemX In item
                                Dim Contains As Boolean = JS.ContainsKey(itemX.ToString.Split(":")(0).Trim.Replace("""", ""))
                                If Not Contains Then JS.Add(itemX)
                            Next
                            Exit For
                        End If
                    Next

                    CType(JSonFile("schemes"), JArray).Add(JS)
                Next

                '# Check for reminants from the old JSON to be added to the new one
                For Each x In CType(JSonFileUntouched("schemes"), JArray)
                    Dim name1 As String = x("name")
                    Dim Found As Boolean = False

                    For Each y In CType(JSonFile("schemes"), JArray)
                        Dim name2 As String = y("name")

                        If name1 = name2 Then
                            Found = True
                            Exit For
                        End If

                    Next

                    If Not Found Then
                        CType(JSonFile("schemes"), JArray).Add(x)
                    End If

                    Found = False
                Next
#End Region

#Region "Defaults"
                JSonFile("profiles")("defaults")("backgroundImage") = DefaultProf.BackgroundImage
                If Not String.IsNullOrEmpty(DefaultProf.ColorScheme) Then JSonFile("profiles")("defaults")("colorScheme") = DefaultProf.ColorScheme
                If Not String.IsNullOrEmpty(DefaultProf.TabTitle) Then JSonFile("profiles")("defaults")("tabTitle") = DefaultProf.TabTitle
                If Not String.IsNullOrEmpty(DefaultProf.CursorShape) Then JSonFile("profiles")("defaults")("cursorShape") = CursorShape_ReturnToString(DefaultProf.CursorShape)
                If Not String.IsNullOrEmpty(DefaultProf.Icon) Then JSonFile("profiles")("defaults")("icon") = DefaultProf.Icon
                If Not DefaultProf.CursorHeight = 0 Then JSonFile("profiles")("defaults")("cursorHeight") = DefaultProf.CursorHeight
                If Not DefaultProf.Opacity = 0 Then JSonFile("profiles")("defaults")("opacity") = DefaultProf.Opacity

                If DefaultProf.TabColor <> Color.FromArgb(0, 0, 0, 0) Then
                    JSonFile("profiles")("defaults")("tabColor") = RGB2HEX(DefaultProf.TabColor)
                Else
                    If DirectCast(JSonFile("profiles")("defaults"), JObject).ContainsKey("tabColor") Then JSonFile("profiles")("defaults")("tabColor") = Nothing
                End If


                JSonFile("profiles")("defaults")("useAcrylic") = DefaultProf.UseAcrylic
                JSonFile("profiles")("defaults")("backgroundImageOpacity") = DefaultProf.BackgroundImageOpacity


                If Not DirectCast(JSonFile("profiles")("defaults"), JObject).ContainsKey("font") Then
                    'JFont.Add("font")
                    Dim JFont As New JObject
                    If DefaultProf.Font.Weight <> Nothing Then JFont("weight") = FontWeight_ReturnToString(DefaultProf.Font.Weight)
                    If Not String.IsNullOrEmpty(DefaultProf.Font.Face) Then JFont("face") = DefaultProf.Font.Face
                    If Not DefaultProf.Font.Size = 0 Then JFont("size") = DefaultProf.Font.Size
                    JSonFile("profiles")("defaults")("font") = JFont

                Else
                    If DefaultProf.Font.Weight <> Nothing Then JSonFile("profiles")("defaults")("font")("weight") = FontWeight_ReturnToString(DefaultProf.Font.Weight)
                    If Not String.IsNullOrEmpty(DefaultProf.Font.Face) Then JSonFile("profiles")("defaults")("font")("face") = DefaultProf.Font.Face
                    If Not DefaultProf.Font.Size = 0 Then JSonFile("profiles")("defaults")("font")("size") = DefaultProf.Font.Size
                End If
#End Region

#Region "Profiles"
                If JSonFile("profiles")("list") IsNot Nothing Then CType(JSonFile("profiles")("list"), JArray).Clear() Else JSonFile("profiles")("list") = New JArray()

                For x = 0 To Profiles.Count - 1
                    Dim JS As New JObject
                    JS("name") = Profiles(x).Name

                    JS("backgroundImage") = Profiles(x).BackgroundImage
                    JS("cursorShape") = CursorShape_ReturnToString(Profiles(x).CursorShape)
                    If Not String.IsNullOrEmpty(Profiles(x).ColorScheme) Then JS("colorScheme") = Profiles(x).ColorScheme
                    If Not String.IsNullOrEmpty(Profiles(x).TabTitle) Then JS("tabTitle") = Profiles(x).TabTitle
                    If Not String.IsNullOrEmpty(Profiles(x).Icon) Then JS("icon") = Profiles(x).Icon
                    If Not Profiles(x).CursorHeight = 0 Then JS("cursorHeight") = Profiles(x).CursorHeight
                    If Not Profiles(x).Opacity = 0 Then JS("opacity") = Profiles(x).Opacity
                    If Not Profiles(x).BackgroundImageOpacity = 0 Then JS("backgroundImageOpacity") = Profiles(x).BackgroundImageOpacity

                    If Profiles(x).TabColor <> Color.FromArgb(0, 0, 0, 0) Then JS("tabColor") = RGB2HEX(Profiles(x).TabColor)

                    JS("useAcrylic") = Profiles(x).UseAcrylic

                    Dim JS_Font As New JObject
                    JS_Font("weight") = FontWeight_ReturnToString(Profiles(x).Font.Weight)
                    If Profiles(x).Font.Face IsNot Nothing Then JS_Font("face") = Profiles(x).Font.Face
                    If Profiles(x).Font.Size <> 0 Then JS_Font("size") = Profiles(x).Font.Size
                    JS("font") = JS_Font

                    '# Check for properties reminants from the old JSON to be added to the new one
                    For Each item In JSonFileUntouched("profiles")("list")
                        If item("name").ToString.ToLower = JS("name").ToString.ToLower Then
                            For Each itemX In item
                                If itemX.ToString.Split(":")(0).Trim.Replace("""", "") <> "tabColor" Then
                                    Dim Contains As Boolean = JS.ContainsKey(itemX.ToString.Split(":")(0).Trim.Replace("""", ""))
                                    If Not Contains Then JS.Add(itemX)
                                End If
                            Next
                            Exit For
                        End If
                    Next

                    CType(JSonFile("profiles")("list"), JArray).Add(JS)
                Next

                '# Check for reminants from the old JSON to be added to the new one
                For Each x In CType(JSonFileUntouched("profiles")("list"), JArray)
                    Dim name1 As String = x("name")
                    Dim Found As Boolean = False

                    For Each y In CType(JSonFile("profiles")("list"), JArray)
                        Dim name2 As String = y("name")

                        If name1 = name2 Then
                            Found = True
                            Exit For
                        End If

                    Next

                    If Not Found Then
                        CType(JSonFile("profiles")("list"), JArray).Add(x)
                    End If

                    Found = False
                Next
#End Region

#Region "Themes"

                If Themes.Count <> 0 Then
                    If JSonFile("themes") IsNot Nothing Then CType(JSonFile("themes"), JArray).Clear() Else JSonFile("themes") = New JArray()

                    For x = 0 To Themes.Count - 1
                        Dim JS As New JObject

                        If Themes(x).Name <> Nothing Then JS("name") = Themes(x).Name

                        Dim JS_Tabs As New JObject
                        If Themes(x).Tab_Active <> Color.FromArgb(0, 0, 0, 0) Then JS_Tabs("background") = RGB2HEX(Themes(x).Tab_Active)
                        If Themes(x).Tab_Inactive <> Color.FromArgb(0, 0, 0, 0) Then JS_Tabs("unfocusedBackground") = RGB2HEX(Themes(x).Tab_Inactive)
                        JS("tab") = JS_Tabs

                        Dim JS_TabRow As New JObject
                        If Themes(x).Titlebar_Active <> Color.FromArgb(0, 0, 0, 0) Then JS_TabRow("background") = RGB2HEX(Themes(x).Titlebar_Active)
                        If Themes(x).Titlebar_Inactive <> Color.FromArgb(0, 0, 0, 0) Then JS_TabRow("unfocusedBackground") = RGB2HEX(Themes(x).Titlebar_Inactive)
                        JS("tabRow") = JS_TabRow

                        Dim JS_Window As New JObject
                        If Themes(x).ApplicationTheme_light <> Nothing Then JS_Window("applicationTheme") = Themes(x).ApplicationTheme_light
                        JS("window") = JS_Window

                        CType(JSonFile("themes"), JArray).Add(JS)
                    Next
                End If
#End Region

                St.Close()
                TakeOwnership(File)
                IO.File.WriteAllText(File, JSonFile.ToString)

                Return JSonFile.ToString

            Case Else
                Return ""

        End Select
    End Function

    Public Shared Sub TakeOwnership(ByVal filepath As String)
        Dim proc = New Process()
        proc.StartInfo.FileName = "takeown.exe"
        proc.StartInfo.Arguments = "/R /F """ & filepath & """"
        proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
        proc.Start()
        proc.WaitForExit()

        proc.StartInfo.FileName = "icacls.exe"
        proc.StartInfo.Arguments = """" & filepath & """ /grant *{GROUP_USERS_SID}:F /T"
        proc.Start()
        proc.WaitForExit()
    End Sub

    Public Shared Function HEX2RGB([String] As String) As Color
        Try
            If [String] IsNot Nothing Then
                If [String].Replace("#", "").Count / 2 = 3 Then
                    Return Color.FromArgb(255, Color.FromArgb(Convert.ToInt32([String].Replace("#", ""), 16)))
                Else
                    Dim a As Integer = Convert.ToInt32([String].Substring([String].Count - 2, 2), 16)
                    Return Color.FromArgb(a, Color.FromArgb(Convert.ToInt32([String].Remove([String].Count - 2, 2).Replace("#", ""), 16)))
                End If
            Else
                Return Color.FromArgb(0, 0, 0, 0)
            End If
        Catch
            Return Color.FromArgb(0, 0, 0, 0)
        End Try
    End Function

    Public Function RGB2HEX([Color] As Color) As String
        Return String.Format("#{0:X2}{1:X2}{2:X2}", [Color].R, [Color].G, [Color].B)
    End Function

    Shared Operator =(First As WinTerminal, Second As WinTerminal) As Boolean
        Dim _equal As Boolean = True
        'MsgBox(Enumerable.SequenceEqual(First.Colors, Second.Colors))
        If Not First.DefaultProf = Second.DefaultProf Then _equal = False
        If Not First.Theme = Second.Theme Then _equal = False
        If Not First.UseAcrylicInTabRow = Second.UseAcrylicInTabRow Then _equal = False
        If Not First.Enabled = Second.Enabled Then _equal = False
        Return _equal
    End Operator

    Shared Operator <>(First As WinTerminal, Second As WinTerminal) As Boolean
        Return Not First = Second
    End Operator

    Public Function Clone() Implements ICloneable.Clone
        Return MemberwiseClone()
    End Function
End Class

Public Class TColor : Implements IComparable : Implements ICloneable

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

    Public Function CompareTo(obj As Object) As Integer Implements IComparable.CompareTo
        If Me = DirectCast(obj, TColor) Then Return 1 Else Return 0
    End Function

    Shared Operator =(First As TColor, Second As TColor) As Boolean
        Return First.Equals(Second)
    End Operator

    Shared Operator <>(First As TColor, Second As TColor) As Boolean
        Return Not First.Equals(Second)
    End Operator

    Public Function Clone() Implements ICloneable.Clone
        Return MemberwiseClone()
    End Function
End Class
Public Class ThemesList : Implements IComparable : Implements ICloneable
    Public Property Name As String
    Public Property Titlebar_Active As Color = Color.FromArgb(0, 0, 0, 0)
    Public Property Titlebar_Inactive As Color = Color.FromArgb(0, 0, 0, 0)
    Public Property Tab_Active As Color = Color.FromArgb(0, 0, 0, 0)
    Public Property Tab_Inactive As Color = Color.FromArgb(0, 0, 0, 0)
    Public Property ApplicationTheme_light As String = "dark"

    Public Function CompareTo(obj As Object) As Integer Implements IComparable.CompareTo
        If Me.Equals(DirectCast(obj, ThemesList)) Then Return 1 Else Return 0
    End Function

    Shared Operator =(First As ThemesList, Second As ThemesList) As Boolean
        Return First.Equals(Second)
    End Operator

    Shared Operator <>(First As ThemesList, Second As ThemesList) As Boolean
        Return Not First.Equals(Second)
    End Operator

    Public Function Clone() Implements ICloneable.Clone
        Return MemberwiseClone()
    End Function
End Class
Public Class FontsBase : Implements ICloneable
    Public Property Face As String = If(My.W11, "Cascadia Mono", "Consolas")
    Public Property Weight As FontWeight_Enum = FontWeight_Enum.normal
    Public Property Size As Integer = 12

    Shared Operator =(First As FontsBase, Second As FontsBase) As Boolean
        Return First.Equals(Second)
    End Operator

    Shared Operator <>(First As FontsBase, Second As FontsBase) As Boolean
        Return Not First.Equals(Second)
    End Operator

    Public Function Clone() Implements ICloneable.Clone
        Return MemberwiseClone()
    End Function
End Class
Public Class ProfilesList : Implements IComparable : Implements ICloneable
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

    Public Function CompareTo(obj As Object) As Integer Implements IComparable.CompareTo
        If Me.Equals(DirectCast(obj, ProfilesList)) Then Return 1 Else Return 0
    End Function

    Shared Operator =(First As ProfilesList, Second As ProfilesList) As Boolean
        Return First.Equals(Second)
    End Operator

    Shared Operator <>(First As ProfilesList, Second As ProfilesList) As Boolean
        Return Not First.Equals(Second)
    End Operator

    Public Function Clone() Implements ICloneable.Clone
        Return MemberwiseClone()
    End Function

#Region "Helpers"
    Enum BackgroundImageAlignment_Enum
        bottom
        bottomLeft
        bottomRight
        center
        left
        right
        top
        topLeft
        topRight
    End Enum
    Enum CursorShape_Enum
        bar
        doubleUnderscore
        emptyBox
        filledBox
        underscore
        vintage
    End Enum
    Public Shared Function CursorShape_ReturnToString(int As CursorShape_Enum) As String
        Select Case int
            Case CursorShape_Enum.bar
                Return "bar"

            Case CursorShape_Enum.doubleUnderscore
                Return "doubleUnderscore"

            Case CursorShape_Enum.emptyBox
                Return "emptyBox"

            Case CursorShape_Enum.filledBox
                Return "filledBox"

            Case CursorShape_Enum.underscore
                Return "underscore"

            Case CursorShape_Enum.vintage
                Return "vintage"

            Case Else
                Return "bar"

        End Select
    End Function
    Public Shared Function CursorShape_GetFromString(str As String) As CursorShape_Enum
        Select Case str.ToLower
            Case "bar".ToLower
                Return CursorShape_Enum.bar

            Case "doubleunderscore".ToLower
                Return CursorShape_Enum.doubleUnderscore

            Case "emptybox".ToLower
                Return CursorShape_Enum.emptyBox

            Case "filledbox".ToLower
                Return CursorShape_Enum.filledBox

            Case "underscore".ToLower
                Return CursorShape_Enum.underscore

            Case "vintage".ToLower
                Return CursorShape_Enum.vintage

            Case Else
                Return CursorShape_Enum.bar

        End Select
    End Function
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