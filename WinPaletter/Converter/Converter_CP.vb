Imports System.Drawing.Drawing2D
Imports System.Reflection
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Public Class Converter_CP
    Private bindingFlags As BindingFlags = BindingFlags.Instance Or BindingFlags.Public

    Public Class Structures
        Structure Info
            Public AppVersion As String
            Public ThemeName As String
            Public Description As String
            Public ThemeVersion As String
            Public Author As String
            Public AuthorSocialMediaLink As String

            Public Overrides Function ToString() As String
                Dim tx As New List(Of String)
                tx.Clear()
                tx.Add("<General>")
                tx.Add("*Palette Name= " & ThemeName)
                If String.IsNullOrWhiteSpace(Description) Then
                    tx.Add("*Palette Description= ")
                Else
                    tx.Add("*Palette Description= " & Description.Replace(vbCrLf, "<br>"))
                End If
                tx.Add("*Palette File Version= " & ThemeVersion)
                tx.Add("*Author= " & Author)
                tx.Add("*AuthorSocialMediaLink= " & AuthorSocialMediaLink)
                tx.Add("</General>" & vbCrLf)
                Return tx.CString
            End Function
        End Structure

        Structure Windows10x
            Public Color_Index0 As Color
            Public Color_Index1 As Color
            Public Color_Index2 As Color
            Public Color_Index3 As Color
            Public Color_Index4 As Color
            Public Color_Index5 As Color
            Public Color_Index6 As Color
            Public Color_Index7 As Color
            Public WinMode_Light As Boolean
            Public AppMode_Light As Boolean
            Public Transparency As Boolean
            Public Titlebar_Active As Color
            Public Titlebar_Inactive As Color
            Public StartMenu_Accent As Color
            Public ApplyAccentOnTitlebars As Boolean
            Public ApplyAccentOnTaskbar As AccentTaskbarLevels
            Public IncreaseTBTransparency As Boolean
            Public TB_Blur As Boolean

            Enum AccentTaskbarLevels
                None
                Taskbar_Start_AC
                Taskbar
            End Enum

            Public Overloads Function ToString(Signature As String, MiniSignature As String) As String
                Dim tx As New List(Of String)
                tx.Clear()
                tx.Add(String.Format("<{0}>", Signature))
                tx.Add(String.Format("*{0}_Color_Index0= {1}", MiniSignature, Color_Index0.ToArgb))
                tx.Add(String.Format("*{0}_Color_Index1= {1}", MiniSignature, Color_Index1.ToArgb))
                tx.Add(String.Format("*{0}_Color_Index2= {1}", MiniSignature, Color_Index2.ToArgb))
                tx.Add(String.Format("*{0}_Color_Index3= {1}", MiniSignature, Color_Index3.ToArgb))
                tx.Add(String.Format("*{0}_Color_Index4= {1}", MiniSignature, Color_Index4.ToArgb))
                tx.Add(String.Format("*{0}_Color_Index5= {1}", MiniSignature, Color_Index5.ToArgb))
                tx.Add(String.Format("*{0}_Color_Index6= {1}", MiniSignature, Color_Index6.ToArgb))
                tx.Add(String.Format("*{0}_Color_Index7= {1}", MiniSignature, Color_Index7.ToArgb))
                tx.Add(String.Format("*{0}_Titlebar_Active= {1}", MiniSignature, Titlebar_Active.ToArgb))
                tx.Add(String.Format("*{0}_Titlebar_Inactive= {1}", MiniSignature, Titlebar_Inactive.ToArgb))
                tx.Add(String.Format("*{0}_StartMenu_Accent= {1}", MiniSignature, StartMenu_Accent.ToArgb))
                tx.Add(String.Format("*{0}_WinMode_Light= {1}", MiniSignature, WinMode_Light))
                tx.Add(String.Format("*{0}_AppMode_Light= {1}", MiniSignature, AppMode_Light))
                tx.Add(String.Format("*{0}_Transparency= {1}", MiniSignature, Transparency))
                tx.Add(String.Format("*{0}_IncreaseTBTransparency= {1}", MiniSignature, IncreaseTBTransparency))
                tx.Add(String.Format("*{0}_TB_Blur= {1}", MiniSignature, TB_Blur))
                tx.Add(String.Format("*{0}_ApplyAccentonTitlebars= {1}", MiniSignature, ApplyAccentOnTitlebars))
                tx.Add(String.Format("*{0}_AccentOnStartTBAC= {1}", MiniSignature, CInt(ApplyAccentOnTaskbar)))
                tx.Add(String.Format("</{0}>" & vbCrLf, Signature))
                Return tx.CString
            End Function

            Public Sub FromListOfString(Lines As IEnumerable(Of String))
                For Each Line As String In Lines
                    If Line.StartsWith("*Win_11_", 5) Then Line = Line.Remove(0, "*Win_11_".Count)
                    If Line.StartsWith("*Win_10_", 5) Then Line = Line.Remove(0, "*Win_10_".Count)

                    If Line.StartsWith("Color_Index0= ", 5) Then Color_Index0 = Color.FromArgb(Line.Remove(0, "Color_Index0= ".Count))
                    If Line.StartsWith("Color_Index1= ", 5) Then Color_Index1 = Color.FromArgb(Line.Remove(0, "Color_Index1= ".Count))
                    If Line.StartsWith("Color_Index2= ", 5) Then Color_Index2 = Color.FromArgb(Line.Remove(0, "Color_Index2= ".Count))
                    If Line.StartsWith("Color_Index3= ", 5) Then Color_Index3 = Color.FromArgb(Line.Remove(0, "Color_Index3= ".Count))
                    If Line.StartsWith("Color_Index4= ", 5) Then Color_Index4 = Color.FromArgb(Line.Remove(0, "Color_Index4= ".Count))
                    If Line.StartsWith("Color_Index5= ", 5) Then Color_Index5 = Color.FromArgb(Line.Remove(0, "Color_Index5= ".Count))
                    If Line.StartsWith("Color_Index6= ", 5) Then Color_Index6 = Color.FromArgb(Line.Remove(0, "Color_Index6= ".Count))
                    If Line.StartsWith("Color_Index7= ", 5) Then Color_Index7 = Color.FromArgb(Line.Remove(0, "Color_Index7= ".Count))
                    If Line.StartsWith("WinMode_Light= ", 5) Then WinMode_Light = Line.Remove(0, "WinMode_Light= ".Count)
                    If Line.StartsWith("AppMode_Light= ", 5) Then AppMode_Light = Line.Remove(0, "AppMode_Light= ".Count)
                    If Line.StartsWith("Transparency= ", 5) Then Transparency = Line.Remove(0, "Transparency= ".Count)
                    If Line.StartsWith("IncreaseTBTransparency= ", 5) Then IncreaseTBTransparency = Line.Remove(0, "IncreaseTBTransparency= ".Count)
                    If Line.StartsWith("TB_Blur= ", 5) Then TB_Blur = Line.Remove(0, "TB_Blur= ".Count)
                    If Line.StartsWith("Titlebar_Active= ", 5) Then Titlebar_Active = Color.FromArgb(Line.Remove(0, "Titlebar_Active= ".Count))
                    If Line.StartsWith("Titlebar_Inactive= ", 5) Then Titlebar_Inactive = Color.FromArgb(Line.Remove(0, "Titlebar_Inactive= ".Count))
                    If Line.StartsWith("StartMenu_Accent= ", 5) Then StartMenu_Accent = Color.FromArgb(Line.Remove(0, "StartMenu_Accent= ".Count))
                    If Line.StartsWith("ApplyAccentonTitlebars= ", 5) Then ApplyAccentOnTitlebars = Line.Remove(0, "ApplyAccentonTitlebars= ".Count)

                    If Line.StartsWith("AccentOnStartTBAC= ", 5) Then
                        Select Case Line.Remove(0, "AccentOnStartTBAC= ".Count).ToLower
                            Case "false"
                                ApplyAccentOnTaskbar = Converter_CP.Structures.Windows10x.AccentTaskbarLevels.None

                            Case "true"
                                ApplyAccentOnTaskbar = Converter_CP.Structures.Windows10x.AccentTaskbarLevels.Taskbar_Start_AC

                            Case Else
                                Select Case Line.Remove(0, "AccentOnStartTBAC= ".Count)
                                    Case 0
                                        ApplyAccentOnTaskbar = Converter_CP.Structures.Windows10x.AccentTaskbarLevels.None

                                    Case 1
                                        ApplyAccentOnTaskbar = Converter_CP.Structures.Windows10x.AccentTaskbarLevels.Taskbar_Start_AC

                                    Case 2
                                        ApplyAccentOnTaskbar = Converter_CP.Structures.Windows10x.AccentTaskbarLevels.Taskbar

                                    Case Else
                                        ApplyAccentOnTaskbar = Converter_CP.Structures.Windows10x.AccentTaskbarLevels.None
                                End Select
                        End Select
                    End If
                Next
            End Sub
        End Structure

        Structure Windows8
            Public Start As Integer
            Public ColorizationColor As Color
            Public ColorizationColorBalance As Integer
            Public StartColor As Color
            Public AccentColor As Color
            Public Theme As Windows7.Themes
            Public LogonUI As Integer
            Public PersonalColors_Background As Color
            Public PersonalColors_Accent As Color
            Public NoLockScreen As Boolean
            Public LockScreenType As Structures.LogonUI7.Modes
            Public LockScreenSystemID As Integer
            Public Overrides Function ToString() As String
                Dim tx As New List(Of String)
                tx.Clear()
                tx.Add("<Metro>")
                tx.Add("*Metro_ColorizationColor= " & ColorizationColor.ToArgb)
                tx.Add("*Metro_ColorizationColorBalance= " & ColorizationColorBalance)
                tx.Add("*Metro_PersonalColors_Background= " & PersonalColors_Background.ToArgb)
                tx.Add("*Metro_PersonalColors_Accent= " & PersonalColors_Accent.ToArgb)
                tx.Add("*Metro_StartColor= " & StartColor.ToArgb)
                tx.Add("*Metro_AccentColor= " & AccentColor.ToArgb)
                tx.Add("*Metro_Start= " & Start)
                tx.Add("*Metro_Theme= " & CInt(Theme))
                tx.Add("*Metro_LogonUI= " & LogonUI)
                tx.Add("*Metro_NoLockScreen= " & NoLockScreen)
                tx.Add("*Metro_LockScreenType= " & CInt(LockScreenType))
                tx.Add("*Metro_LockScreenSystemID= " & LockScreenSystemID)
                tx.Add("</Metro>" & vbCrLf)

                Return tx.CString
            End Function

            Public Sub FromListOfString(Lines As IEnumerable(Of String))
                For Each line As String In Lines
                    If line.StartsWith("*Metro_ColorizationColor= ", 5) Then ColorizationColor = Color.FromArgb(line.Remove(0, "*Metro_ColorizationColor= ".Count))
                    If line.StartsWith("*Metro_ColorizationColorBalance= ", 5) Then ColorizationColorBalance = line.Remove(0, "*Metro_ColorizationColorBalance= ".Count)
                    If line.StartsWith("*Metro_PersonalColors_Background= ", 5) Then PersonalColors_Background = Color.FromArgb(line.Remove(0, "*Metro_PersonalColors_Background= ".Count))
                    If line.StartsWith("*Metro_PersonalColors_Accent= ", 5) Then PersonalColors_Accent = Color.FromArgb(line.Remove(0, "*Metro_PersonalColors_Accent= ".Count))
                    If line.StartsWith("*Metro_StartColor= ", 5) Then StartColor = Color.FromArgb(line.Remove(0, "*Metro_StartColor= ".Count))
                    If line.StartsWith("*Metro_AccentColor= ", 5) Then AccentColor = Color.FromArgb(line.Remove(0, "*Metro_AccentColor= ".Count))
                    If line.StartsWith("*Metro_Start= ", 5) Then Start = line.Remove(0, "*Metro_Start= ".Count)
                    If line.StartsWith("*Metro_Theme= ", 5) Then Theme = line.Remove(0, "*Metro_Theme= ".Count)
                    If line.StartsWith("*Metro_LogonUI= ", 5) Then LogonUI = line.Remove(0, "*Metro_LogonUI= ".Count)
                    If line.StartsWith("*Metro_NoLockScreen= ", 5) Then NoLockScreen = line.Remove(0, "*Metro_NoLockScreen= ".Count)
                    If line.StartsWith("*Metro_LockScreenType= ", 5) Then LockScreenType = line.Remove(0, "*Metro_LockScreenType= ".Count)
                    If line.StartsWith("*Metro_LockScreenSystemID= ", 5) Then LockScreenSystemID = line.Remove(0, "*Metro_LockScreenSystemID= ".Count)
                Next
            End Sub
        End Structure

        Structure Windows7
            Public ColorizationColor As Color
            Public ColorizationAfterglow As Color
            Public EnableAeroPeek As Boolean
            Public AlwaysHibernateThumbnails As Boolean
            Public ColorizationColorBalance As Integer
            Public ColorizationAfterglowBalance As Integer
            Public ColorizationBlurBalance As Integer
            Public ColorizationGlassReflectionIntensity As Integer
            Public Theme As Themes

            Enum Themes
                Aero
                AeroLite
                AeroOpaque
                Basic
                Classic
            End Enum

            Public Overrides Function ToString() As String
                Dim tx As New List(Of String)
                tx.Clear()
                tx.Add("<Aero>")
                tx.Add("*Aero_ColorizationColor= " & ColorizationColor.ToArgb)
                tx.Add("*Aero_ColorizationAfterglow= " & ColorizationAfterglow.ToArgb)
                tx.Add("*Aero_ColorizationColorBalance= " & ColorizationColorBalance)
                tx.Add("*Aero_ColorizationAfterglowBalance= " & ColorizationAfterglowBalance)
                tx.Add("*Aero_ColorizationBlurBalance= " & ColorizationBlurBalance)
                tx.Add("*Aero_ColorizationGlassReflectionIntensity= " & ColorizationGlassReflectionIntensity)
                tx.Add("*Aero_EnableAeroPeek= " & EnableAeroPeek)
                tx.Add("*Aero_AlwaysHibernateThumbnails= " & AlwaysHibernateThumbnails)
                tx.Add("*Aero_Theme= " & CInt(Theme))
                tx.Add("</Aero>" & vbCrLf)
                Return tx.CString
            End Function

            Public Sub FromListOfString(Lines As IEnumerable(Of String))
                For Each line As String In Lines
                    If line.StartsWith("*Aero_ColorizationColor= ", 5) Then ColorizationColor = Color.FromArgb(line.Remove(0, "*Aero_ColorizationColor= ".Count))
                    If line.StartsWith("*Aero_ColorizationAfterglow= ", 5) Then ColorizationAfterglow = Color.FromArgb(line.Remove(0, "*Aero_ColorizationAfterglow= ".Count))
                    If line.StartsWith("*Aero_ColorizationColorBalance= ", 5) Then ColorizationColorBalance = line.Remove(0, "*Aero_ColorizationColorBalance= ".Count)
                    If line.StartsWith("*Aero_ColorizationAfterglowBalance= ", 5) Then ColorizationAfterglowBalance = line.Remove(0, "*Aero_ColorizationAfterglowBalance= ".Count)
                    If line.StartsWith("*Aero_ColorizationBlurBalance= ", 5) Then ColorizationBlurBalance = line.Remove(0, "*Aero_ColorizationBlurBalance= ".Count)
                    If line.StartsWith("*Aero_ColorizationGlassReflectionIntensity= ", 5) Then ColorizationGlassReflectionIntensity = line.Remove(0, "*Aero_ColorizationGlassReflectionIntensity= ".Count)
                    If line.StartsWith("*Aero_EnableAeroPeek= ", 5) Then EnableAeroPeek = line.Remove(0, "*Aero_EnableAeroPeek= ".Count)
                    If line.StartsWith("*Aero_AlwaysHibernateThumbnails= ", 5) Then AlwaysHibernateThumbnails = line.Remove(0, "*Aero_AlwaysHibernateThumbnails= ".Count)
                    If line.StartsWith("*Aero_Theme= ", 5) Then Theme = line.Remove(0, "*Aero_Theme= ".Count)
                Next
            End Sub

        End Structure

        Structure WindowsVista
            Public ColorizationColor As Color
            Public [Alpha] As Byte
            Public Theme As Windows7.Themes

            Shared Operator =(First As WindowsVista, Second As WindowsVista) As Boolean
                Return First.Equals(Second)
            End Operator

            Shared Operator <>(First As WindowsVista, Second As WindowsVista) As Boolean
                Return Not First.Equals(Second)
            End Operator

            Public Overrides Function ToString() As String
                Dim tx As New List(Of String)
                tx.Clear()
                tx.Add("<Vista>")
                tx.Add("*Vista_ColorizationColor= " & ColorizationColor.ToArgb)
                tx.Add("*Vista_Alpha= " & Alpha)
                tx.Add("*Vista_Theme= " & CInt(Theme))
                tx.Add("</Vista>" & vbCrLf)
                Return tx.CString
            End Function

            Public Sub FromListOfString(Lines As IEnumerable(Of String))
                For Each line As String In Lines
                    If line.StartsWith("*Vista_ColorizationColor= ", 5) Then ColorizationColor = Color.FromArgb(line.Remove(0, "*Vista_ColorizationColor= ".Count))
                    If line.StartsWith("*Vista_Alpha= ", 5) Then Alpha = line.Remove(0, "*Vista_Alpha= ".Count)
                    If line.StartsWith("*Vista_Theme= ", 5) Then Theme = line.Remove(0, "*Vista_Theme= ".Count)
                Next
            End Sub

        End Structure

        Structure WindowsXP
            Public Theme As Themes
            Public ThemeFile As String
            Public ColorScheme As String

            Enum Themes
                LunaBlue
                LunaOliveGreen
                LunaSilver
                Classic
                Custom
            End Enum

            Public Overrides Function ToString() As String
                Dim tx As New List(Of String)
                tx.Clear()
                tx.Add("<WinXP>")
                tx.Add("*WinXP_Theme= " & CInt(Theme))
                tx.Add("*WinXP_ThemeFile= " & ThemeFile)
                tx.Add("*WinXP_ColorScheme= " & ColorScheme)
                tx.Add("</WinXP>" & vbCrLf)
                Return tx.CString
            End Function

            Public Sub FromListOfString(Lines As IEnumerable(Of String))
                For Each line As String In Lines
                    If line.StartsWith("*WinXP_Theme= ", 5) Then Theme = line.Remove(0, "*WinXP_Theme= ".Count)
                    If line.StartsWith("*WinXP_ThemeFile= ", 5) Then ThemeFile = line.Remove(0, "*WinXP_ThemeFile= ".Count)
                    If line.StartsWith("*WinXP_ColorScheme= ", 5) Then ColorScheme = line.Remove(0, "*WinXP_ColorScheme= ".Count)
                Next
            End Sub

        End Structure

        Structure Win32UI
            Public EnableTheming As Boolean
            Public EnableGradient As Boolean
            Public ActiveBorder As Color
            Public ActiveTitle As Color
            Public AppWorkspace As Color
            Public Background As Color
            Public ButtonAlternateFace As Color
            Public ButtonDkShadow As Color
            Public ButtonFace As Color
            Public ButtonHilight As Color
            Public ButtonLight As Color
            Public ButtonShadow As Color
            Public ButtonText As Color
            Public GradientActiveTitle As Color
            Public GradientInactiveTitle As Color
            Public GrayText As Color
            Public HilightText As Color
            Public HotTrackingColor As Color
            Public InactiveBorder As Color
            Public InactiveTitle As Color
            Public InactiveTitleText As Color
            Public InfoText As Color
            Public InfoWindow As Color
            Public Menu As Color
            Public MenuBar As Color
            Public MenuText As Color
            Public Scrollbar As Color
            Public TitleText As Color
            Public Window As Color
            Public WindowFrame As Color
            Public WindowText As Color
            Public Hilight As Color
            Public MenuHilight As Color
            Public Desktop As Color

            Enum Method
                Registry
                File
                VisualStyles
            End Enum

            Public Overrides Function ToString() As String
                Dim tx As New List(Of String)
                tx.Clear()

                tx.Add("<Win32UI>")
                tx.Add("*Win32UI_EnableTheming= " & EnableTheming)
                tx.Add("*Win32UI_EnableGradient= " & EnableGradient)
                tx.Add("*Win32UI_ActiveBorder= " & ActiveBorder.ToArgb)
                tx.Add("*Win32UI_ActiveTitle= " & ActiveTitle.ToArgb)
                tx.Add("*Win32UI_AppWorkspace= " & AppWorkspace.ToArgb)
                tx.Add("*Win32UI_Background= " & Background.ToArgb)
                tx.Add("*Win32UI_ButtonAlternateFace= " & ButtonAlternateFace.ToArgb)
                tx.Add("*Win32UI_ButtonDkShadow= " & ButtonDkShadow.ToArgb)
                tx.Add("*Win32UI_ButtonFace= " & ButtonFace.ToArgb)
                tx.Add("*Win32UI_ButtonHilight= " & ButtonHilight.ToArgb)
                tx.Add("*Win32UI_ButtonLight= " & ButtonLight.ToArgb)
                tx.Add("*Win32UI_ButtonShadow= " & ButtonShadow.ToArgb)
                tx.Add("*Win32UI_ButtonText= " & ButtonText.ToArgb)
                tx.Add("*Win32UI_GradientActiveTitle= " & GradientActiveTitle.ToArgb)
                tx.Add("*Win32UI_GradientInactiveTitle= " & GradientInactiveTitle.ToArgb)
                tx.Add("*Win32UI_GrayText= " & GrayText.ToArgb)
                tx.Add("*Win32UI_HilightText= " & HilightText.ToArgb)
                tx.Add("*Win32UI_HotTrackingColor= " & HotTrackingColor.ToArgb)
                tx.Add("*Win32UI_InactiveBorder= " & InactiveBorder.ToArgb)
                tx.Add("*Win32UI_InactiveTitle= " & InactiveTitle.ToArgb)
                tx.Add("*Win32UI_InactiveTitleText= " & InactiveTitleText.ToArgb)
                tx.Add("*Win32UI_InfoText= " & InfoText.ToArgb)
                tx.Add("*Win32UI_InfoWindow= " & InfoWindow.ToArgb)
                tx.Add("*Win32UI_Menu= " & Menu.ToArgb)
                tx.Add("*Win32UI_MenuBar= " & MenuBar.ToArgb)
                tx.Add("*Win32UI_MenuText= " & MenuText.ToArgb)
                tx.Add("*Win32UI_Scrollbar= " & Scrollbar.ToArgb)
                tx.Add("*Win32UI_TitleText= " & TitleText.ToArgb)
                tx.Add("*Win32UI_Window= " & Window.ToArgb)
                tx.Add("*Win32UI_WindowFrame= " & WindowFrame.ToArgb)
                tx.Add("*Win32UI_WindowText= " & WindowText.ToArgb)
                tx.Add("*Win32UI_Hilight= " & Hilight.ToArgb)
                tx.Add("*Win32UI_MenuHilight= " & MenuHilight.ToArgb)
                tx.Add("*Win32UI_Desktop= " & Desktop.ToArgb)
                tx.Add("</Win32UI>" & vbCrLf)

                Return tx.CString
            End Function

            Public Sub FromListOfString(Lines As IEnumerable(Of String))
                For Each line As String In Lines
                    If line.StartsWith("*Win32UI_EnableTheming= ", 5) Then EnableTheming = line.Remove(0, "*Win32UI_EnableTheming= ".Count)
                    If line.StartsWith("*Win32UI_EnableGradient= ", 5) Then EnableGradient = line.Remove(0, "*Win32UI_EnableGradient= ".Count)
                    If line.StartsWith("*Win32UI_ActiveBorder= ", 5) Then ActiveBorder = Color.FromArgb(line.Remove(0, "*Win32UI_ActiveBorder= ".Count))
                    If line.StartsWith("*Win32UI_ActiveTitle= ", 5) Then ActiveTitle = Color.FromArgb(line.Remove(0, "*Win32UI_ActiveTitle= ".Count))
                    If line.StartsWith("*Win32UI_AppWorkspace= ", 5) Then AppWorkspace = Color.FromArgb(line.Remove(0, "*Win32UI_AppWorkspace= ".Count))
                    If line.StartsWith("*Win32UI_Background= ", 5) Then Background = Color.FromArgb(line.Remove(0, "*Win32UI_Background= ".Count))
                    If line.StartsWith("*Win32UI_ButtonAlternateFace= ", 5) Then ButtonAlternateFace = Color.FromArgb(line.Remove(0, "*Win32UI_ButtonAlternateFace= ".Count))
                    If line.StartsWith("*Win32UI_ButtonDkShadow= ", 5) Then ButtonDkShadow = Color.FromArgb(line.Remove(0, "*Win32UI_ButtonDkShadow= ".Count))
                    If line.StartsWith("*Win32UI_ButtonFace= ", 5) Then ButtonFace = Color.FromArgb(line.Remove(0, "*Win32UI_ButtonFace= ".Count))
                    If line.StartsWith("*Win32UI_ButtonHilight= ", 5) Then ButtonHilight = Color.FromArgb(line.Remove(0, "*Win32UI_ButtonHilight= ".Count))
                    If line.StartsWith("*Win32UI_ButtonLight= ", 5) Then ButtonLight = Color.FromArgb(line.Remove(0, "*Win32UI_ButtonLight= ".Count))
                    If line.StartsWith("*Win32UI_ButtonShadow= ", 5) Then ButtonShadow = Color.FromArgb(line.Remove(0, "*Win32UI_ButtonShadow= ".Count))
                    If line.StartsWith("*Win32UI_ButtonText= ", 5) Then ButtonText = Color.FromArgb(line.Remove(0, "*Win32UI_ButtonText= ".Count))
                    If line.StartsWith("*Win32UI_GradientActiveTitle= ", 5) Then GradientActiveTitle = Color.FromArgb(line.Remove(0, "*Win32UI_GradientActiveTitle= ".Count))
                    If line.StartsWith("*Win32UI_GradientInactiveTitle= ", 5) Then GradientInactiveTitle = Color.FromArgb(line.Remove(0, "*Win32UI_GradientInactiveTitle= ".Count))
                    If line.StartsWith("*Win32UI_GrayText= ", 5) Then GrayText = Color.FromArgb(line.Remove(0, "*Win32UI_GrayText= ".Count))
                    If line.StartsWith("*Win32UI_HilightText= ", 5) Then HilightText = Color.FromArgb(line.Remove(0, "*Win32UI_HilightText= ".Count))
                    If line.StartsWith("*Win32UI_HotTrackingColor= ", 5) Then HotTrackingColor = Color.FromArgb(line.Remove(0, "*Win32UI_HotTrackingColor= ".Count))
                    If line.StartsWith("*Win32UI_InactiveBorder= ", 5) Then InactiveBorder = Color.FromArgb(line.Remove(0, "*Win32UI_InactiveBorder= ".Count))
                    If line.StartsWith("*Win32UI_InactiveTitle= ", 5) Then InactiveTitle = Color.FromArgb(line.Remove(0, "*Win32UI_InactiveTitle= ".Count))
                    If line.StartsWith("*Win32UI_InactiveTitleText= ", 5) Then InactiveTitleText = Color.FromArgb(line.Remove(0, "*Win32UI_InactiveTitleText= ".Count))
                    If line.StartsWith("*Win32UI_InfoText= ", 5) Then InfoText = Color.FromArgb(line.Remove(0, "*Win32UI_InfoText= ".Count))
                    If line.StartsWith("*Win32UI_InfoWindow= ", 5) Then InfoWindow = Color.FromArgb(line.Remove(0, "*Win32UI_InfoWindow= ".Count))
                    If line.StartsWith("*Win32UI_Menu= ", 5) Then Menu = Color.FromArgb(line.Remove(0, "*Win32UI_Menu= ".Count))
                    If line.StartsWith("*Win32UI_MenuBar= ", 5) Then MenuBar = Color.FromArgb(line.Remove(0, "*Win32UI_MenuBar= ".Count))
                    If line.StartsWith("*Win32UI_MenuText= ", 5) Then MenuText = Color.FromArgb(line.Remove(0, "*Win32UI_MenuText= ".Count))
                    If line.StartsWith("*Win32UI_Scrollbar= ", 5) Then Scrollbar = Color.FromArgb(line.Remove(0, "*Win32UI_Scrollbar= ".Count))
                    If line.StartsWith("*Win32UI_TitleText= ", 5) Then TitleText = Color.FromArgb(line.Remove(0, "*Win32UI_TitleText= ".Count))
                    If line.StartsWith("*Win32UI_Window= ", 5) Then Window = Color.FromArgb(line.Remove(0, "*Win32UI_Window= ".Count))
                    If line.StartsWith("*Win32UI_WindowFrame= ", 5) Then WindowFrame = Color.FromArgb(line.Remove(0, "*Win32UI_WindowFrame= ".Count))
                    If line.StartsWith("*Win32UI_WindowText= ", 5) Then WindowText = Color.FromArgb(line.Remove(0, "*Win32UI_WindowText= ".Count))
                    If line.StartsWith("*Win32UI_Hilight= ", 5) Then Hilight = Color.FromArgb(line.Remove(0, "*Win32UI_Hilight= ".Count))
                    If line.StartsWith("*Win32UI_MenuHilight= ", 5) Then MenuHilight = Color.FromArgb(line.Remove(0, "*Win32UI_MenuHilight= ".Count))
                    If line.StartsWith("*Win32UI_Desktop= ", 5) Then Desktop = Color.FromArgb(line.Remove(0, "*Win32UI_Desktop= ".Count))
                Next
            End Sub

        End Structure

        Structure WinEffects
            Public Enabled As Boolean

            Public WindowAnimation As Boolean
            Public WindowShadow As Boolean
            Public WindowUIEffects As Boolean
            Public ShowWinContentDrag As Boolean

            Public MenuAnimation As Boolean
            Public MenuFade As MenuAnimType
            Public MenuSelectionFade As Boolean
            Public MenuShowDelay As UInteger            'Microsoft uses this as DWORD, which its equivalent is UInteger, not Integer

            Public ComboBoxAnimation As Boolean
            Public ListBoxSmoothScrolling As Boolean

            Public TooltipAnimation As Boolean
            Public TooltipFade As MenuAnimType

            Public IconsShadow As Boolean
            Public IconsDesktopTranslSel As Boolean

            Public KeyboardUnderline As Boolean
            Public FocusRectWidth As UInteger           'Microsoft uses this as DWORD, which its equivalent is UInteger, not Integer
            Public FocusRectHeight As UInteger          'Microsoft uses this as DWORD, which its equivalent is UInteger, not Integer
            Public Caret As UInteger
            Public NotificationDuration As Integer
            Public ShakeToMinimize As Boolean
            Public AWT_Enabled As Boolean
            Public AWT_BringActivatedWindowToTop As Boolean
            Public AWT_Delay As Integer
            Public SnapCursorToDefButton As Boolean

            Public Win11ClassicContextMenu As Boolean
            Public SysListView32 As Boolean
            Public ShowSecondsInSystemClock As Boolean
            Public BalloonNotifications As Boolean
            Public PaintDesktopVersion As Boolean

            Public Win11BootDots As Boolean
            Public Win11ExplorerBar As ExplorerBar
            Public DisableNavBar As Boolean

            Enum ExplorerBar
                [Default]
                Ribbon
                Bar
            End Enum

            Enum ColorFilters
                Grayscale
                Inverted
                GrayscaleInverted
                RedGreen_deuteranopia
                RedGreen_protanopia
                BlueYellow
            End Enum

            Enum MenuAnimType
                Fade
                Scroll
            End Enum

            Public Overrides Function ToString() As String
                Dim tx As New List(Of String)
                tx.Clear()
                tx.Add("<WindowsEffects>")
                tx.Add("*WinEffects_Enabled= " & Enabled)
                tx.Add("*WinEffects_WindowAnimation= " & WindowAnimation)
                tx.Add("*WinEffects_WindowShadow= " & WindowShadow)
                tx.Add("*WinEffects_WindowUIEffects= " & WindowUIEffects)
                tx.Add("*WinEffects_MenuAnimation= " & MenuAnimation)
                tx.Add("*WinEffects_MenuFade= " & CInt(MenuFade))
                tx.Add("*WinEffects_MenuShowDelay= " & MenuShowDelay)
                tx.Add("*WinEffects_MenuSelectionFade= " & MenuSelectionFade)
                tx.Add("*WinEffects_ComboBoxAnimation= " & ComboBoxAnimation)
                tx.Add("*WinEffects_ListboxSmoothScrolling= " & ListBoxSmoothScrolling)
                tx.Add("*WinEffects_TooltipAnimation= " & TooltipAnimation)
                tx.Add("*WinEffects_TooltipFade= " & CInt(TooltipFade))
                tx.Add("*WinEffects_IconsShadow= " & IconsShadow)
                tx.Add("*WinEffects_IconsDesktopTranslSel= " & IconsDesktopTranslSel)
                tx.Add("*WinEffects_ShowWinContentDrag= " & ShowWinContentDrag)
                tx.Add("*WinEffects_KeyboardUnderline= " & KeyboardUnderline)
                tx.Add("*WinEffects_FocusRectWidth= " & FocusRectWidth)
                tx.Add("*WinEffects_FocusRectHeight= " & FocusRectHeight)
                tx.Add("*WinEffects_Caret= " & Caret)
                tx.Add("*WinEffects_NotificationDuration= " & NotificationDuration)
                tx.Add("*WinEffects_ShakeToMinimize= " & ShakeToMinimize)
                tx.Add("*WinEffects_AWT_Enabled= " & AWT_Enabled)
                tx.Add("*WinEffects_AWT_BringActivatedWindowToTop= " & AWT_BringActivatedWindowToTop)
                tx.Add("*WinEffects_AWT_Delay= " & AWT_Delay)
                tx.Add("*WinEffects_SnapCursorToDefButton= " & SnapCursorToDefButton)
                tx.Add("*WinEffects_Win11ClassicContextMenu= " & Win11ClassicContextMenu)
                tx.Add("*WinEffects_SysListView32= " & SysListView32)
                tx.Add("*WinEffects_ShowSecondsInSystemClock= " & ShowSecondsInSystemClock)
                tx.Add("*WinEffects_BalloonNotifications= " & BalloonNotifications)
                tx.Add("*WinEffects_PaintDesktopVersion= " & PaintDesktopVersion)
                tx.Add("*WinEffects_Win11BootDots= " & Win11BootDots)
                tx.Add("*WinEffects_Win11ExplorerBar= " & Win11ExplorerBar)
                tx.Add("*WinEffects_DisableNavBar= " & DisableNavBar)
                tx.Add("</WindowsEffects>" & vbCrLf)
                Return tx.CString
            End Function

            Public Sub FromListOfString(Lines As IEnumerable(Of String))
                For Each line As String In Lines
                    If line.StartsWith("*WinEffects_Enabled= ", 5) Then Enabled = line.Remove(0, "*WinEffects_Enabled= ".Count)
                    If line.StartsWith("*WinEffects_WindowAnimation= ", 5) Then WindowAnimation = line.Remove(0, "*WinEffects_WindowAnimation= ".Count)
                    If line.StartsWith("*WinEffects_WindowShadow= ", 5) Then WindowShadow = line.Remove(0, "*WinEffects_WindowShadow= ".Count)
                    If line.StartsWith("*WinEffects_WindowUIEffects= ", 5) Then WindowUIEffects = line.Remove(0, "*WinEffects_WindowUIEffects= ".Count)
                    If line.StartsWith("*WinEffects_MenuAnimation= ", 5) Then MenuAnimation = line.Remove(0, "*WinEffects_MenuAnimation= ".Count)
                    If line.StartsWith("*WinEffects_MenuFade= ", 5) Then MenuFade = line.Remove(0, "*WinEffects_MenuFade= ".Count)
                    If line.StartsWith("*WinEffects_MenuShowDelay= ", 5) Then MenuShowDelay = line.Remove(0, "*WinEffects_MenuShowDelay= ".Count)
                    If line.StartsWith("*WinEffects_MenuSelectionFade= ", 5) Then MenuSelectionFade = line.Remove(0, "*WinEffects_MenuSelectionFade= ".Count)
                    If line.StartsWith("*WinEffects_ComboBoxAnimation= ", 5) Then ComboBoxAnimation = line.Remove(0, "*WinEffects_ComboBoxAnimation= ".Count)
                    If line.StartsWith("*WinEffects_ListboxSmoothScrolling= ", 5) Then ListBoxSmoothScrolling = line.Remove(0, "*WinEffects_ListboxSmoothScrolling= ".Count)
                    If line.StartsWith("*WinEffects_TooltipAnimation= ", 5) Then TooltipAnimation = line.Remove(0, "*WinEffects_TooltipAnimation= ".Count)
                    If line.StartsWith("*WinEffects_TooltipFade= ", 5) Then TooltipFade = line.Remove(0, "*WinEffects_TooltipFade= ".Count)
                    If line.StartsWith("*WinEffects_IconsShadow= ", 5) Then IconsShadow = line.Remove(0, "*WinEffects_IconsShadow= ".Count)
                    If line.StartsWith("*WinEffects_IconsDesktopTranslSel= ", 5) Then IconsDesktopTranslSel = line.Remove(0, "*WinEffects_IconsDesktopTranslSel= ".Count)
                    If line.StartsWith("*WinEffects_ShowWinContentDrag= ", 5) Then ShowWinContentDrag = line.Remove(0, "*WinEffects_ShowWinContentDrag= ".Count)
                    If line.StartsWith("*WinEffects_KeyboardUnderline= ", 5) Then KeyboardUnderline = line.Remove(0, "*WinEffects_KeyboardUnderline= ".Count)
                    If line.StartsWith("*WinEffects_FocusRectWidth= ", 5) Then FocusRectWidth = line.Remove(0, "*WinEffects_FocusRectWidth= ".Count)
                    If line.StartsWith("*WinEffects_FocusRectHeight= ", 5) Then FocusRectHeight = line.Remove(0, "*WinEffects_FocusRectHeight= ".Count)
                    If line.StartsWith("*WinEffects_Caret= ", 5) Then Caret = line.Remove(0, "*WinEffects_Caret= ".Count)
                    If line.StartsWith("*WinEffects_NotificationDuration= ", 5) Then NotificationDuration = line.Remove(0, "*WinEffects_NotificationDuration= ".Count)
                    If line.StartsWith("*WinEffects_ShakeToMinimize= ", 5) Then ShakeToMinimize = line.Remove(0, "*WinEffects_ShakeToMinimize= ".Count)
                    If line.StartsWith("*WinEffects_AWT_Enabled= ", 5) Then AWT_Enabled = line.Remove(0, "*WinEffects_AWT_Enabled= ".Count)
                    If line.StartsWith("*WinEffects_AWT_BringActivatedWindowToTop= ", 5) Then AWT_BringActivatedWindowToTop = line.Remove(0, "*WinEffects_AWT_BringActivatedWindowToTop= ".Count)
                    If line.StartsWith("*WinEffects_AWT_Delay= ", 5) Then AWT_Delay = line.Remove(0, "*WinEffects_AWT_Delay= ".Count)
                    If line.StartsWith("*WinEffects_SnapCursorToDefButton= ", 5) Then SnapCursorToDefButton = line.Remove(0, "*WinEffects_SnapCursorToDefButton= ".Count)
                    If line.StartsWith("*WinEffects_Win11ClassicContextMenu= ", 5) Then Win11ClassicContextMenu = line.Remove(0, "*WinEffects_Win11ClassicContextMenu= ".Count)
                    If line.StartsWith("*WinEffects_SysListView32= ", 5) Then SysListView32 = line.Remove(0, "*WinEffects_SysListView32= ".Count)
                    If line.StartsWith("*WinEffects_ShowSecondsInSystemClock= ", 5) Then ShowSecondsInSystemClock = line.Remove(0, "*WinEffects_ShowSecondsInSystemClock= ".Count)
                    If line.StartsWith("*WinEffects_BalloonNotifications= ", 5) Then BalloonNotifications = line.Remove(0, "*WinEffects_BalloonNotifications= ".Count)
                    If line.StartsWith("*WinEffects_PaintDesktopVersion= ", 5) Then PaintDesktopVersion = line.Remove(0, "*WinEffects_PaintDesktopVersion= ".Count)
                    If line.StartsWith("*WinEffects_Win11BootDots= ", 5) Then Win11BootDots = line.Remove(0, "*WinEffects_Win11BootDots= ".Count)
                    If line.StartsWith("*WinEffects_Win11ExplorerBar= ", 5) Then Win11ExplorerBar = line.Remove(0, "*WinEffects_Win11ExplorerBar= ".Count)
                    If line.StartsWith("*WinEffects_DisableNavBar= ", 5) Then DisableNavBar = line.Remove(0, "*WinEffects_DisableNavBar= ".Count)
                Next
            End Sub

        End Structure

        Structure WallpaperTone
            Public Enabled As Boolean
            Public Image As String
            Public H, S, L As Integer

            Sub FromListOfString(Lines As IEnumerable(Of String)) 'As WallpaperTone
                If Lines.Count > 0 Then
                    'Dim WT As New WallpaperTone With {.Image = My.PATH_Windows & "\Web\Wallpaper\Windows\img0.jpg"}

                    For Each lin As String In Lines
                        If lin.StartsWith("Enabled= ", 5) Then Enabled = lin.Remove(0, "Enabled= ".Count)
                        If lin.StartsWith("Image= ", 5) Then Image = lin.Remove(0, "Image= ".Count)
                        If lin.StartsWith("H= ", 5) Then H = lin.Remove(0, "H= ".Count)
                        If lin.StartsWith("S= ", 5) Then S = lin.Remove(0, "S= ".Count)
                        If lin.StartsWith("L= ", 5) Then L = lin.Remove(0, "L= ".Count)
                    Next

                    'Return WT
                Else
                    'Return New Structures.WallpaperTone With {
                    '        .Enabled = False,
                    '        .Image = My.PATH_Windows & "\Web\Wallpaper\Windows\img0.jpg",
                    '        .H = 0, .S = 100, .L = 100}
                End If
            End Sub

            Overloads Function ToString(Signature As String) As String
                Dim tx As New List(Of String)
                tx.Clear()
                tx.Add(String.Format("<WallpaperTone_{0}>", Signature))
                tx.Add(String.Format("*WallpaperTone_{0}_Enabled= {1}", Signature, Enabled))
                tx.Add(String.Format("*WallpaperTone_{0}_Image= {1}", Signature, Image))
                tx.Add(String.Format("*WallpaperTone_{0}_H= {1}", Signature, H))
                tx.Add(String.Format("*WallpaperTone_{0}_S= {1}", Signature, S))
                tx.Add(String.Format("*WallpaperTone_{0}_L= {1}", Signature, L))
                tx.Add(String.Format("</WallpaperTone_{0}>", Signature) & vbCrLf)
                Return tx.CString
            End Function
        End Structure

        Structure MetricsFonts
            Public Enabled As Boolean
            Public BorderWidth As Integer
            Public CaptionHeight As Integer
            Public CaptionWidth As Integer
            Public IconSpacing As Integer
            Public IconVerticalSpacing As Integer
            Public MenuHeight As Integer
            Public MenuWidth As Integer
            Public PaddedBorderWidth As Integer
            Public ScrollHeight As Integer
            Public ScrollWidth As Integer
            Public SmCaptionHeight As Integer
            Public SmCaptionWidth As Integer
            Public DesktopIconSize As Integer
            Public ShellIconSize As Integer

            Public CaptionFont As Font
            Public IconFont As Font
            Public MenuFont As Font
            Public MessageFont As Font
            Public SmCaptionFont As Font
            Public StatusFont As Font
            Public FontSubstitute_MSShellDlg As String
            Public FontSubstitute_MSShellDlg2 As String
            Public FontSubstitute_SegoeUI As String

            Function AddFontsToThemeFile(PropName As String, Font As Font) As String
                Dim s As New List(Of String) : s.Clear()
                s.Add(String.Format("*Fonts_{0}_{1}= {2}", PropName, "Name", Font.Name))
                s.Add(String.Format("*Fonts_{0}_{1}= {2}", PropName, "Size", Font.SizeInPoints))
                s.Add(String.Format("*Fonts_{0}_{1}= {2}", PropName, "Style", Font.Style))
                Return s.CString
            End Function

            Public Overrides Function ToString() As String
                Dim tx As New List(Of String)
                tx.Clear()
                tx.Add("<Metrics&Fonts>")
                tx.Add("*MetricsFonts_Enabled= " & Enabled)
                tx.Add("*Metrics_BorderWidth= " & BorderWidth)
                tx.Add("*Metrics_CaptionHeight= " & CaptionHeight)
                tx.Add("*Metrics_CaptionWidth= " & CaptionWidth)
                tx.Add("*Metrics_IconSpacing= " & IconSpacing)
                tx.Add("*Metrics_IconVerticalSpacing= " & IconVerticalSpacing)
                tx.Add("*Metrics_MenuHeight= " & MenuHeight)
                tx.Add("*Metrics_MenuWidth= " & MenuWidth)
                tx.Add("*Metrics_PaddedBorderWidth= " & PaddedBorderWidth)
                tx.Add("*Metrics_ScrollHeight= " & ScrollHeight)
                tx.Add("*Metrics_ScrollWidth= " & ScrollWidth)
                tx.Add("*Metrics_SmCaptionHeight= " & SmCaptionHeight)
                tx.Add("*Metrics_SmCaptionWidth= " & SmCaptionWidth)
                tx.Add("*Metrics_DesktopIconSize= " & DesktopIconSize)
                tx.Add("*Metrics_ShellIconSize= " & ShellIconSize)
                tx.Add("*FontSubstitute_MSShellDlg= " & FontSubstitute_MSShellDlg)
                tx.Add("*FontSubstitute_MSShellDlg2= " & FontSubstitute_MSShellDlg2)
                tx.Add("*FontSubstitute_SegoeUI= " & FontSubstitute_SegoeUI)
                tx.Add(AddFontsToThemeFile("Caption", CaptionFont))
                tx.Add(AddFontsToThemeFile("Icon", IconFont))
                tx.Add(AddFontsToThemeFile("Menu", MenuFont))
                tx.Add(AddFontsToThemeFile("Message", MessageFont))
                tx.Add(AddFontsToThemeFile("SmCaption", SmCaptionFont))
                tx.Add(AddFontsToThemeFile("Status", StatusFont))
                tx.Add("</Metrics&Fonts>" & vbCrLf)
                Return tx.CString
            End Function

            Public Sub FromListOfString(Lines As IEnumerable(Of String))
                Dim fonts As New List(Of String)
                fonts.Clear()

                For Each line As String In Lines
                    If line.StartsWith("*MetricsFonts_Enabled= ", 5) Then Enabled = line.Remove(0, "*MetricsFonts_Enabled= ".Count)
                    If line.StartsWith("*Metrics_BorderWidth= ", 5) Then BorderWidth = line.Remove(0, "*Metrics_BorderWidth= ".Count)
                    If line.StartsWith("*Metrics_CaptionHeight= ", 5) Then CaptionHeight = line.Remove(0, "*Metrics_CaptionHeight= ".Count)
                    If line.StartsWith("*Metrics_CaptionWidth= ", 5) Then CaptionWidth = line.Remove(0, "*Metrics_CaptionWidth= ".Count)
                    If line.StartsWith("*Metrics_IconSpacing= ", 5) Then IconSpacing = line.Remove(0, "*Metrics_IconSpacing= ".Count)
                    If line.StartsWith("*Metrics_IconVerticalSpacing= ", 5) Then IconVerticalSpacing = line.Remove(0, "*Metrics_IconVerticalSpacing= ".Count)
                    If line.StartsWith("*Metrics_MenuHeight= ", 5) Then MenuHeight = line.Remove(0, "*Metrics_MenuHeight= ".Count)
                    If line.StartsWith("*Metrics_MenuWidth= ", 5) Then MenuWidth = line.Remove(0, "*Metrics_MenuWidth= ".Count)
                    If line.StartsWith("*Metrics_PaddedBorderWidth= ", 5) Then PaddedBorderWidth = line.Remove(0, "*Metrics_PaddedBorderWidth= ".Count)
                    If line.StartsWith("*Metrics_ScrollHeight= ", 5) Then ScrollHeight = line.Remove(0, "*Metrics_ScrollHeight= ".Count)
                    If line.StartsWith("*Metrics_ScrollWidth= ", 5) Then ScrollWidth = line.Remove(0, "*Metrics_ScrollWidth= ".Count)
                    If line.StartsWith("*Metrics_SmCaptionHeight= ", 5) Then SmCaptionHeight = line.Remove(0, "*Metrics_SmCaptionHeight= ".Count)
                    If line.StartsWith("*Metrics_SmCaptionWidth= ", 5) Then SmCaptionWidth = line.Remove(0, "*Metrics_SmCaptionWidth= ".Count)
                    If line.StartsWith("*Metrics_DesktopIconSize= ", 5) Then DesktopIconSize = line.Remove(0, "*Metrics_DesktopIconSize= ".Count)
                    If line.StartsWith("*Metrics_ShellIconSize= ", 5) Then ShellIconSize = line.Remove(0, "*Metrics_ShellIconSize= ".Count)
                    If line.StartsWith("*Fonts_", 5) Then fonts.Add(line.Remove(0, "*Fonts_".Count))
                    If line.StartsWith("*FontSubstitute_MSShellDlg= ", 5) Then FontSubstitute_MSShellDlg = line.Remove(0, "*FontSubstitute_MSShellDlg= ".Count)
                    If line.StartsWith("*FontSubstitute_MSShellDlg2= ", 5) Then FontSubstitute_MSShellDlg2 = line.Remove(0, "*FontSubstitute_MSShellDlg2= ".Count)
                    If line.StartsWith("*FontSubstitute_SegoeUI= ", 5) Then FontSubstitute_SegoeUI = line.Remove(0, "*FontSubstitute_SegoeUI= ".Count)
                Next

                If fonts.Count > 0 Then
                    For Each x In fonts
                        Dim Value As String = x.Replace(x.Split("=")(0) & "= ", "").Trim
                        Dim FontName As String = x.Split("=")(0).ToString.Split("_")(0)
                        Dim Prop As String = x.Split("=")(0).ToString.Split("_")(1)

                        Select Case FontName.ToLower
                            Case "Caption".ToLower
                                CaptionFont = SetToFont(Prop, Value, CaptionFont)

                            Case "Icon".ToLower
                                IconFont = SetToFont(Prop, Value, IconFont)

                            Case "Menu".ToLower
                                MenuFont = SetToFont(Prop, Value, MenuFont)

                            Case "Message".ToLower
                                MessageFont = SetToFont(Prop, Value, MessageFont)

                            Case "SmCaption".ToLower
                                SmCaptionFont = SetToFont(Prop, Value, SmCaptionFont)

                            Case "Status".ToLower
                                StatusFont = SetToFont(Prop, Value, StatusFont)

                        End Select
                    Next
                End If

            End Sub

            Function SetToFont(PropName As String, PropValue As String, Font As Font) As Font
                Dim F As New Font(Font.Name, Font.Size, Font.Style)

                Select Case PropName.ToLower
                    Case "Name".ToLower
                        If PropValue.ToUpper = "MS SANS SERIF" Then PropValue = "Microsoft Sans Serif"
                        F = New Font(PropValue, Font.Size, Font.Style)

                    Case "Size".ToLower
                        F = New Font(Font.Name, CSng(PropValue), Font.Style)

                    Case "Style".ToLower
                        F = New Font(Font.Name, Font.Size, ReturnFontStyle(PropValue))

                End Select

                Return F
            End Function

            Function ReturnFontStyle([Value] As String) As FontStyle

                If Not [Value].Contains(",") Then

                    Select Case [Value].ToLower
                        Case "Bold".ToLower
                            Return FontStyle.Bold

                        Case "Italic".ToLower
                            Return FontStyle.Italic

                        Case "Regular".ToLower
                            Return FontStyle.Regular

                        Case "Strikeout".ToLower
                            Return FontStyle.Strikeout

                        Case "Underline".ToLower
                            Return FontStyle.Underline

                        Case Else
                            Return FontStyle.Regular

                    End Select

                Else
                    Dim Collection As New FontStyle

                    For Each x In Value.Split(",")
                        Dim val As String = x.Trim

                        Select Case val.ToLower
                            Case "Bold".ToLower
                                Collection += FontStyle.Bold

                            Case "Italic".ToLower
                                Collection += FontStyle.Italic

                            Case "Regular".ToLower
                                Collection += FontStyle.Regular

                            Case "Strikeout".ToLower
                                Collection += FontStyle.Strikeout

                            Case "Underline".ToLower
                                Collection += FontStyle.Underline

                            Case Else
                                Collection += FontStyle.Regular

                        End Select

                    Next

                    Return Collection
                End If

            End Function

        End Structure

        Structure AltTab
            Public Enabled As Boolean
            Public Style As Styles
            Public Win10Opacity As Integer

            Enum Styles
                [Default]
                ClassicNT
                Placeholder
                EP_Win10
            End Enum

            Public Overrides Function ToString() As String
                Dim tx As New List(Of String)
                tx.Clear()
                tx.Add("<AltTab>")
                tx.Add("*AltTab_Enabled= " & Enabled)
                tx.Add("*AltTab_Style= " & CInt(Style))
                tx.Add("*AltTab_Win10Opacity= " & Win10Opacity)
                tx.Add("</AltTab>" & vbCrLf)
                Return tx.CString
            End Function

            Public Sub FromListOfString(Lines As IEnumerable(Of String))
                For Each line As String In Lines
                    If line.StartsWith("*AltTab_Enabled= ", 5) Then Enabled = line.Remove(0, "*AltTab_Enabled= ".Count)
                    If line.StartsWith("*AltTab_Style= ", 5) Then Style = line.Remove(0, "*AltTab_Style= ".Count)
                    If line.StartsWith("*AltTab_Win10Opacity= ", 5) Then Win10Opacity = line.Remove(0, "*AltTab_Win10Opacity= ".Count)
                Next
            End Sub

        End Structure

        Structure LogonUI10x
            Public DisableAcrylicBackgroundOnLogon As Boolean
            Public DisableLogonBackgroundImage As Boolean
            Public NoLockScreen As Boolean

            Public Overrides Function ToString() As String
                Dim tx As New List(Of String)
                tx.Clear()
                tx.Add("<LogonUI_10_11>")
                tx.Add("*LogonUI_DisableAcrylicBackgroundOnLogon= " & DisableAcrylicBackgroundOnLogon)
                tx.Add("*LogonUI_DisableLogonBackgroundImage= " & DisableLogonBackgroundImage)
                tx.Add("*LogonUI_NoLockScreen= " & NoLockScreen)
                tx.Add("</LogonUI_10_11>" & vbCrLf)
                Return tx.CString
            End Function

            Public Sub FromListOfString(Lines As IEnumerable(Of String))
                For Each line As String In Lines
                    If line.StartsWith("*LogonUI_DisableAcrylicBackgroundOnLogon= ", 5) Then DisableAcrylicBackgroundOnLogon = line.Remove(0, "*LogonUI_DisableAcrylicBackgroundOnLogon= ".Count)
                    If line.StartsWith("*LogonUI_DisableLogonBackgroundImage= ", 5) Then DisableLogonBackgroundImage = line.Remove(0, "*LogonUI_DisableLogonBackgroundImage= ".Count)
                    If line.StartsWith("*LogonUI_NoLockScreen= ", 5) Then NoLockScreen = line.Remove(0, "*LogonUI_NoLockScreen= ".Count)
                Next
            End Sub

        End Structure

        Structure LogonUI7
            Public Enabled As Boolean
            Public Mode As Modes
            Public ImagePath As String
            Public Color As Color
            Public Blur As Boolean
            Public Blur_Intensity As Integer
            Public Grayscale As Boolean
            Public Noise As Boolean
            Public Noise_Mode As NoiseMode
            Public Noise_Intensity As Integer

            Enum NoiseMode
                Aero
                Acrylic
            End Enum

            Enum Modes
                Default_
                Wallpaper
                CustomImage
                SolidColor
            End Enum

            Public Overrides Function ToString() As String
                Dim tx As New List(Of String)
                tx.Clear()
                tx.Add("<LogonUI_7_8>")
                tx.Add("*LogonUI7_Enabled= " & Enabled)
                tx.Add("*LogonUI7_Mode= " & CInt(Mode))
                tx.Add("*LogonUI7_ImagePath= " & ImagePath)
                tx.Add("*LogonUI7_Color= " & Color.ToArgb)
                tx.Add("*LogonUI7_Effect_Blur= " & Blur)
                tx.Add("*LogonUI7_Effect_Blur_Intensity= " & Blur_Intensity)
                tx.Add("*LogonUI7_Effect_Grayscale= " & Grayscale)
                tx.Add("*LogonUI7_Effect_Noise= " & Noise)
                tx.Add("*LogonUI7_Effect_Noise_Mode= " & CInt(Noise_Mode))
                tx.Add("*LogonUI7_Effect_Noise_Intensity= " & Noise_Intensity)
                tx.Add("</LogonUI_7_8>" & vbCrLf)
                Return tx.CString
            End Function

            Public Sub FromListOfString(Lines As IEnumerable(Of String))
                For Each line As String In Lines
                    If line.StartsWith("*LogonUI7_Color= ", 5) Then Color = Color.FromArgb(line.Remove(0, "*LogonUI7_Color= ".Count))
                    If line.StartsWith("*LogonUI7_Enabled= ", 5) Then Enabled = line.Remove(0, "*LogonUI7_Enabled= ".Count)
                    If line.StartsWith("*LogonUI7_Mode= ", 5) Then Mode = line.Remove(0, "*LogonUI7_Mode= ".Count)
                    If line.StartsWith("*LogonUI7_ImagePath= ", 5) Then ImagePath = line.Remove(0, "*LogonUI7_ImagePath= ".Count)
                    If line.StartsWith("*LogonUI7_Blur= ", 5) Then Blur = line.Remove(0, "*LogonUI7_Blur= ".Count)
                    If line.StartsWith("*LogonUI7_Blur_Intensity= ", 5) Then Blur_Intensity = line.Remove(0, "*LogonUI7_Blur_Intensity= ".Count)
                    If line.StartsWith("*LogonUI7_Grayscale= ", 5) Then Grayscale = line.Remove(0, "*LogonUI7_Grayscale= ".Count)
                    If line.StartsWith("*LogonUI7_Noise= ", 5) Then Noise = line.Remove(0, "*LogonUI7_Noise= ".Count)
                    If line.StartsWith("*LogonUI7_Noise_Mode= ", 5) Then Noise_Mode = line.Remove(0, "*LogonUI7_Noise_Mode= ".Count)
                    If line.StartsWith("*LogonUI7_Noise_Intensity= ", 5) Then Noise_Intensity = line.Remove(0, "*LogonUI7_Noise_Intensity= ".Count)
                Next
            End Sub

        End Structure

        Structure LogonUIXP
            Public Enabled As Boolean
            Public Mode As Modes
            Public BackColor As Color
            Public ShowMoreOptions As Boolean

            Enum Modes
                Win2000
                [Default]
            End Enum

            Public Overrides Function ToString() As String
                Dim tx As New List(Of String)
                tx.Clear()
                tx.Add("<LogonUI_XP>")
                tx.Add("*LogonUIXP_Enabled= " & Enabled)
                tx.Add("*LogonUIXP_Mode= " & CInt(Mode))
                tx.Add("*LogonUIXP_BackColor= " & BackColor.ToArgb)
                tx.Add("*LogonUIXP_ShowMoreOptions= " & ShowMoreOptions)
                tx.Add("</LogonUI_XP>" & vbCrLf)
                Return tx.CString
            End Function

            Public Sub FromListOfString(Lines As IEnumerable(Of String))
                For Each line As String In Lines
                    If line.StartsWith("*LogonUIXP_Enabled= ", 5) Then Enabled = line.Remove(0, "*LogonUIXP_Enabled= ".Count)
                    If line.StartsWith("*LogonUIXP_Mode= ", 5) Then Mode = If(line.Remove(0, "*LogonUIXP_Mode= ".Count) = 1, Modes.Default, Modes.Win2000)
                    If line.StartsWith("*LogonUIXP_BackColor= ", 5) Then BackColor = Color.FromArgb(line.Remove(0, "*LogonUIXP_BackColor= ".Count))
                    If line.StartsWith("*LogonUIXP_ShowMoreOptions= ", 5) Then ShowMoreOptions = line.Remove(0, "*LogonUIXP_ShowMoreOptions= ".Count)
                Next
            End Sub

        End Structure

        Structure Console
            Public Enabled As Boolean
            Public ColorTable00 As Color
            Public ColorTable01 As Color
            Public ColorTable02 As Color
            Public ColorTable03 As Color
            Public ColorTable04 As Color
            Public ColorTable05 As Color
            Public ColorTable06 As Color
            Public ColorTable07 As Color
            Public ColorTable08 As Color
            Public ColorTable09 As Color
            Public ColorTable10 As Color
            Public ColorTable11 As Color
            Public ColorTable12 As Color
            Public ColorTable13 As Color
            Public ColorTable14 As Color
            Public ColorTable15 As Color
            Public PopupForeground As Integer
            Public PopupBackground As Integer
            Public ScreenColorsForeground As Integer
            Public ScreenColorsBackground As Integer
            Public CursorSize As Integer
            Public FaceName As String
            Public FontRaster As Boolean
            Public FontSize As Integer
            Public FontWeight As Integer
            Public W10_1909_CursorType As Integer
            Public W10_1909_CursorColor As Color
            Public W10_1909_ForceV2 As Boolean
            Public W10_1909_LineSelection As Boolean
            Public W10_1909_TerminalScrolling As Boolean
            Public W10_1909_WindowAlpha As Integer

            Overloads Function ToString(Signature As String) As String
                Dim tx As New List(Of String)
                tx.Clear()
                tx.Add(String.Format("<{0}>", Signature))
                tx.Add(String.Format("*Terminal_{0}_Enabled= {1}", Signature, Enabled))
                tx.Add(String.Format("*{0}_ColorTable00= {1}", Signature, ColorTable00.ToArgb))
                tx.Add(String.Format("*{0}_ColorTable01= {1}", Signature, ColorTable01.ToArgb))
                tx.Add(String.Format("*{0}_ColorTable02= {1}", Signature, ColorTable02.ToArgb))
                tx.Add(String.Format("*{0}_ColorTable03= {1}", Signature, ColorTable03.ToArgb))
                tx.Add(String.Format("*{0}_ColorTable04= {1}", Signature, ColorTable04.ToArgb))
                tx.Add(String.Format("*{0}_ColorTable05= {1}", Signature, ColorTable05.ToArgb))
                tx.Add(String.Format("*{0}_ColorTable06= {1}", Signature, ColorTable06.ToArgb))
                tx.Add(String.Format("*{0}_ColorTable07= {1}", Signature, ColorTable07.ToArgb))
                tx.Add(String.Format("*{0}_ColorTable08= {1}", Signature, ColorTable08.ToArgb))
                tx.Add(String.Format("*{0}_ColorTable09= {1}", Signature, ColorTable09.ToArgb))
                tx.Add(String.Format("*{0}_ColorTable10= {1}", Signature, ColorTable10.ToArgb))
                tx.Add(String.Format("*{0}_ColorTable11= {1}", Signature, ColorTable11.ToArgb))
                tx.Add(String.Format("*{0}_ColorTable12= {1}", Signature, ColorTable12.ToArgb))
                tx.Add(String.Format("*{0}_ColorTable13= {1}", Signature, ColorTable13.ToArgb))
                tx.Add(String.Format("*{0}_ColorTable14= {1}", Signature, ColorTable14.ToArgb))
                tx.Add(String.Format("*{0}_ColorTable15= {1}", Signature, ColorTable15.ToArgb))
                tx.Add(String.Format("*{0}_PopupForeground= {1}", Signature, PopupForeground))
                tx.Add(String.Format("*{0}_PopupBackground= {1}", Signature, PopupBackground))
                tx.Add(String.Format("*{0}_ScreenColorsForeground= {1}", Signature, ScreenColorsForeground))
                tx.Add(String.Format("*{0}_ScreenColorsBackground= {1}", Signature, ScreenColorsBackground))
                tx.Add(String.Format("*{0}_CursorSize= {1}", Signature, CursorSize))
                tx.Add(String.Format("*{0}_FaceName= {1}", Signature, FaceName))
                tx.Add(String.Format("*{0}_FontRaster= {1}", Signature, FontRaster))
                tx.Add(String.Format("*{0}_FontSize= {1}", Signature, FontSize))
                tx.Add(String.Format("*{0}_FontWeight= {1}", Signature, FontWeight))
                tx.Add(String.Format("*{0}_1909_CursorType= {1}", Signature, W10_1909_CursorType))
                tx.Add(String.Format("*{0}_1909_CursorColor= {1}", Signature, W10_1909_CursorColor.ToArgb))
                tx.Add(String.Format("*{0}_1909_ForceV2= {1}", Signature, W10_1909_ForceV2))
                tx.Add(String.Format("*{0}_1909_LineSelection= {1}", Signature, W10_1909_LineSelection))
                tx.Add(String.Format("*{0}_1909_TerminalScrolling= {1}", Signature, W10_1909_TerminalScrolling))
                tx.Add(String.Format("*{0}_1909_WindowAlpha= {1}", Signature, W10_1909_WindowAlpha))
                tx.Add(String.Format("</{0}>", Signature) & vbCrLf)
                Return tx.CString
            End Function

            Sub FromListOfString(Lines As IEnumerable(Of String)) 'As Console
                'Dim [Console] As New Console

                For Each Line As String In Lines
                    If Line.StartsWith("*CMD_", 5) Then Line = Line.Remove(0, "*CMD_".Count)
                    If Line.StartsWith("*PS_32_", 5) Then Line = Line.Remove(0, "*PS_32_".Count)
                    If Line.StartsWith("*PS_64_", 5) Then Line = Line.Remove(0, "*PS_64_".Count)

                    If Line.StartsWith("ColorTable00= ", 5) Then ColorTable00 = Color.FromArgb(Line.ToLower.Remove(0, "ColorTable00= ".Count))
                    If Line.StartsWith("ColorTable01= ", 5) Then ColorTable01 = Color.FromArgb(Line.ToLower.Remove(0, "ColorTable01= ".Count))
                    If Line.StartsWith("ColorTable02= ", 5) Then ColorTable02 = Color.FromArgb(Line.ToLower.Remove(0, "ColorTable02= ".Count))
                    If Line.StartsWith("ColorTable03= ", 5) Then ColorTable03 = Color.FromArgb(Line.ToLower.Remove(0, "ColorTable03= ".Count))
                    If Line.StartsWith("ColorTable04= ", 5) Then ColorTable04 = Color.FromArgb(Line.ToLower.Remove(0, "ColorTable04= ".Count))
                    If Line.StartsWith("ColorTable05= ", 5) Then ColorTable05 = Color.FromArgb(Line.ToLower.Remove(0, "ColorTable05= ".Count))
                    If Line.StartsWith("ColorTable06= ", 5) Then ColorTable06 = Color.FromArgb(Line.ToLower.Remove(0, "ColorTable06= ".Count))
                    If Line.StartsWith("ColorTable07= ", 5) Then ColorTable07 = Color.FromArgb(Line.ToLower.Remove(0, "ColorTable07= ".Count))
                    If Line.StartsWith("ColorTable08= ", 5) Then ColorTable08 = Color.FromArgb(Line.ToLower.Remove(0, "ColorTable08= ".Count))
                    If Line.StartsWith("ColorTable09= ", 5) Then ColorTable09 = Color.FromArgb(Line.ToLower.Remove(0, "ColorTable09= ".Count))
                    If Line.StartsWith("ColorTable10= ", 5) Then ColorTable10 = Color.FromArgb(Line.ToLower.Remove(0, "ColorTable10= ".Count))
                    If Line.StartsWith("ColorTable11= ", 5) Then ColorTable11 = Color.FromArgb(Line.ToLower.Remove(0, "ColorTable11= ".Count))
                    If Line.StartsWith("ColorTable12= ", 5) Then ColorTable12 = Color.FromArgb(Line.ToLower.Remove(0, "ColorTable12= ".Count))
                    If Line.StartsWith("ColorTable13= ", 5) Then ColorTable13 = Color.FromArgb(Line.ToLower.Remove(0, "ColorTable13= ".Count))
                    If Line.StartsWith("ColorTable14= ", 5) Then ColorTable14 = Color.FromArgb(Line.ToLower.Remove(0, "ColorTable14= ".Count))
                    If Line.StartsWith("ColorTable15= ", 5) Then ColorTable15 = Color.FromArgb(Line.ToLower.Remove(0, "ColorTable15= ".Count))
                    If Line.StartsWith("PopupForeground= ", 5) Then PopupForeground = Line.ToLower.Remove(0, "PopupForeground= ".Count)
                    If Line.StartsWith("PopupBackground= ", 5) Then PopupBackground = Line.ToLower.Remove(0, "PopupBackground= ".Count)
                    If Line.StartsWith("ScreenColorsForeground= ", 5) Then ScreenColorsForeground = Line.ToLower.Remove(0, "ScreenColorsForeground= ".Count)
                    If Line.StartsWith("ScreenColorsBackground= ", 5) Then ScreenColorsBackground = Line.ToLower.Remove(0, "ScreenColorsBackground= ".Count)
                    If Line.StartsWith("CursorSize= ", 5) Then CursorSize = Line.ToLower.Remove(0, "CursorSize= ".Count)
                    If Line.StartsWith("FaceName= ", 5) Then FaceName = Line.ToLower.Remove(0, "FaceName= ".Count)
                    If Line.StartsWith("FontRaster= ", 5) Then FontRaster = Line.ToLower.Remove(0, "FontRaster= ".Count)
                    If Line.StartsWith("FontSize= ", 5) Then FontSize = Line.ToLower.Remove(0, "FontSize= ".Count)
                    If Line.StartsWith("FontWeight= ", 5) Then FontWeight = Line.ToLower.Remove(0, "FontWeight= ".Count)
                    If Line.StartsWith("1909_CursorType= ", 5) Then W10_1909_CursorType = Line.ToLower.Remove(0, "1909_CursorType= ".Count)
                    If Line.StartsWith("1909_CursorColor= ", 5) Then W10_1909_CursorColor = Color.FromArgb(Line.ToLower.Remove(0, "1909_CursorColor= ".Count))
                    If Line.StartsWith("1909_ForceV2= ", 5) Then W10_1909_ForceV2 = Line.ToLower.Remove(0, "1909_ForceV2= ".Count)
                    If Line.StartsWith("1909_lin.ToLowereSelection= ", 5) Then W10_1909_LineSelection = Line.ToLower.Remove(0, "1909_lin.ToLowereSelection= ".Count)
                    If Line.StartsWith("1909_TerminalScrollin.ToLowerg= ", 5) Then W10_1909_TerminalScrolling = Line.ToLower.Remove(0, "1909_TerminalScrollin.ToLowerg= ".Count)
                    If Line.StartsWith("1909_WindowAlpha= ", 5) Then W10_1909_WindowAlpha = Line.ToLower.Remove(0, "1909_WindowAlpha= ".Count)
                Next

                'Return [Console]
            End Sub

        End Structure

        Structure Cursor
            Public ArrowStyle As ArrowStyles
            Public CircleStyle As CircleStyles
            Public PrimaryColor1 As Color
            Public PrimaryColor2 As Color
            Public PrimaryColorGradient As Boolean
            Public PrimaryColorGradientMode As GradientMode
            Public PrimaryColorNoise As Boolean
            Public PrimaryColorNoiseOpacity As Single
            Public SecondaryColor1 As Color
            Public SecondaryColor2 As Color
            Public SecondaryColorGradient As Boolean
            Public SecondaryColorGradientMode As GradientMode
            Public SecondaryColorNoise As Boolean
            Public SecondaryColorNoiseOpacity As Single
            Public LoadingCircleBack1 As Color
            Public LoadingCircleBack2 As Color
            Public LoadingCircleBackGradient As Boolean
            Public LoadingCircleBackGradientMode As GradientMode
            Public LoadingCircleBackNoise As Boolean
            Public LoadingCircleBackNoiseOpacity As Single
            Public LoadingCircleHot1 As Color
            Public LoadingCircleHot2 As Color
            Public LoadingCircleHotGradient As Boolean
            Public LoadingCircleHotGradientMode As GradientMode
            Public LoadingCircleHotNoise As Boolean
            Public LoadingCircleHotNoiseOpacity As Single
            Public Shadow_Enabled As Boolean
            Public Shadow_Color As Color
            Public Shadow_Blur As Integer
            Public Shadow_Opacity As Single
            Public Shadow_OffsetX As Integer
            Public Shadow_OffsetY As Integer

            Enum ArrowStyles
                Aero
                Modern
                Classic
            End Enum

            Enum CircleStyles
                Aero
                Dot
                Classic
            End Enum

            Enum GradientMode
                Vertical
                Horizontal
                ForwardDiagonal
                BackwardDiagonal
                Circle
            End Enum

            Function ReturnGradientModeFromString([String] As String) As GradientMode
                If [String].Trim.ToLower = "vertical" Then
                    Return GradientMode.Vertical

                ElseIf [String].Trim.ToLower = "horizontal" Then
                    Return GradientMode.Horizontal

                ElseIf [String].Trim.ToLower = "forward diagonal" Then
                    Return GradientMode.ForwardDiagonal

                ElseIf [String].Trim.ToLower = "backward diagonal" Then
                    Return GradientMode.BackwardDiagonal

                ElseIf [String].Trim.ToLower = "circle" Then
                    Return GradientMode.Circle

                Else
                    Return Nothing

                End If

            End Function

            Function ReturnStringFromGradientMode([GradientMode] As GradientMode) As String
                If [GradientMode] = GradientMode.Horizontal Then
                    Return "Horizontal"

                ElseIf [GradientMode] = GradientMode.Vertical Then
                    Return "Vertical"

                ElseIf [GradientMode] = GradientMode.ForwardDiagonal Then
                    Return "Forward Diagonal"

                ElseIf [GradientMode] = GradientMode.BackwardDiagonal Then
                    Return "Backward Diagonal"

                ElseIf [GradientMode] = GradientMode.Circle Then
                    Return "Circle"

                Else
                    Return Nothing

                End If

            End Function

            Function ReturnGradience([Rectangle] As [Rectangle], [Color1] As Color, [Color2] As Color, [GradientMode] As GradientMode, Optional Angle As Single = 0) As Brush

                If [GradientMode] = GradientMode.Horizontal Then
                    Return New LinearGradientBrush([Rectangle], [Color1], [Color2], LinearGradientMode.Horizontal)

                ElseIf [GradientMode] = GradientMode.Vertical Then
                    Return New LinearGradientBrush([Rectangle], [Color1], [Color2], LinearGradientMode.Vertical)

                ElseIf [GradientMode] = GradientMode.ForwardDiagonal Then
                    Return New LinearGradientBrush([Rectangle], [Color1], [Color2], LinearGradientMode.ForwardDiagonal)

                ElseIf [GradientMode] = GradientMode.BackwardDiagonal Then
                    Return New LinearGradientBrush([Rectangle], [Color1], [Color2], LinearGradientMode.BackwardDiagonal)

                ElseIf [GradientMode] = GradientMode.Circle Then
                    Return New LinearGradientBrush([Rectangle], [Color1], [Color2], Angle, True)

                Else
                    Return New SolidBrush([Color1])

                End If

            End Function

            Sub FromListOfString(Lines As IEnumerable(Of String)) 'As Cursor

                If Lines.Count > 0 Then
                    'Dim [Cursor] As New Cursor
                    For Each line As String In Lines
                        If line.StartsWith("*Cursor_Arrow_", 5) Then line = line.Remove(0, "*Cursor_Arrow_".Count)
                        If line.StartsWith("*Cursor_Help_", 5) Then line = line.Remove(0, "*Cursor_Help_".Count)
                        If line.StartsWith("*Cursor_AppLoading_", 5) Then line = line.Remove(0, "*Cursor_AppLoading_".Count)
                        If line.StartsWith("*Cursor_Busy_", 5) Then line = line.Remove(0, "*Cursor_Busy_".Count)
                        If line.StartsWith("*Cursor_Move_", 5) Then line = line.Remove(0, "*Cursor_Move_".Count)
                        If line.StartsWith("*Cursor_NS_", 5) Then line = line.Remove(0, "*Cursor_NS_".Count)
                        If line.StartsWith("*Cursor_EW_", 5) Then line = line.Remove(0, "*Cursor_EW_".Count)
                        If line.StartsWith("*Cursor_NESW_", 5) Then line = line.Remove(0, "*Cursor_NESW_".Count)
                        If line.StartsWith("*Cursor_NWSE_", 5) Then line = line.Remove(0, "*Cursor_NWSE_".Count)
                        If line.StartsWith("*Cursor_Up_", 5) Then line = line.Remove(0, "*Cursor_Up_".Count)
                        If line.StartsWith("*Cursor_Pen_", 5) Then line = line.Remove(0, "*Cursor_Pen_".Count)
                        If line.StartsWith("*Cursor_None_", 5) Then line = line.Remove(0, "*Cursor_None_".Count)
                        If line.StartsWith("*Cursor_Link_", 5) Then line = line.Remove(0, "*Cursor_Link_".Count)
                        If line.StartsWith("*Cursor_Pin_", 5) Then line = line.Remove(0, "*Cursor_Pin_".Count)
                        If line.StartsWith("*Cursor_Person_", 5) Then line = line.Remove(0, "*Cursor_Person_".Count)
                        If line.StartsWith("*Cursor_IBeam_", 5) Then line = line.Remove(0, "*Cursor_IBeam_".Count)
                        If line.StartsWith("*Cursor_Cross_", 5) Then line = line.Remove(0, "*Cursor_Cross_".Count)

                        If line.StartsWith("ArrowStyle= ", 5) Then ArrowStyle = line.Remove(0, "ArrowStyle= ".Count)
                        If line.StartsWith("CircleStyle= ", 5) Then CircleStyle = line.Remove(0, "CircleStyle= ".Count)
                        If line.StartsWith("PrimaryColor1= ", 5) Then PrimaryColor1 = Color.FromArgb(line.Remove(0, "PrimaryColor1= ".Count))
                        If line.StartsWith("PrimaryColor2= ", 5) Then PrimaryColor2 = Color.FromArgb(line.Remove(0, "PrimaryColor2= ".Count))
                        If line.StartsWith("PrimaryColorGradient= ", 5) Then PrimaryColorGradient = line.Remove(0, "PrimaryColorGradient= ".Count)
                        If line.StartsWith("PrimaryColorGradientMode= ", 5) Then PrimaryColorGradientMode = ReturnGradientModeFromString(line.Remove(0, "PrimaryColorGradientMode= ".Count))
                        If line.StartsWith("PrimaryColorNoise= ", 5) Then PrimaryColorNoise = line.Remove(0, "PrimaryColorNoise= ".Count)
                        If line.StartsWith("PrimaryColorNoiseOpacity= ", 5) Then PrimaryColorNoiseOpacity = line.Remove(0, "PrimaryColorNoiseOpacity= ".Count)
                        If line.StartsWith("SecondaryColor1= ", 5) Then SecondaryColor1 = Color.FromArgb(line.Remove(0, "SecondaryColor1= ".Count))
                        If line.StartsWith("SecondaryColor2= ", 5) Then SecondaryColor2 = Color.FromArgb(line.Remove(0, "SecondaryColor2= ".Count))
                        If line.StartsWith("SecondaryColorGradient= ", 5) Then SecondaryColorGradient = line.Remove(0, "SecondaryColorGradient= ".Count)
                        If line.StartsWith("SecondaryColorGradientMode= ", 5) Then SecondaryColorGradientMode = ReturnGradientModeFromString(line.Remove(0, "SecondaryColorGradientMode= ".Count))
                        If line.StartsWith("SecondaryColorNoise= ", 5) Then SecondaryColorNoise = line.Remove(0, "SecondaryColorNoise= ".Count)
                        If line.StartsWith("SecondaryColorNoiseOpacity= ", 5) Then SecondaryColorNoiseOpacity = line.Remove(0, "SecondaryColorNoiseOpacity= ".Count)
                        If line.StartsWith("LoadingCircleBack1= ", 5) Then LoadingCircleBack1 = Color.FromArgb(line.Remove(0, "LoadingCircleBack1= ".Count))
                        If line.StartsWith("LoadingCircleBack2= ", 5) Then LoadingCircleBack2 = Color.FromArgb(line.Remove(0, "LoadingCircleBack2= ".Count))
                        If line.StartsWith("LoadingCircleBackGradient= ", 5) Then LoadingCircleBackGradient = line.Remove(0, "LoadingCircleBackGradient= ".Count)
                        If line.StartsWith("LoadingCircleBackGradientMode= ", 5) Then LoadingCircleBackGradientMode = ReturnGradientModeFromString(line.Remove(0, "LoadingCircleBackGradientMode= ".Count))
                        If line.StartsWith("LoadingCircleBackNoise= ", 5) Then LoadingCircleBackNoise = line.Remove(0, "LoadingCircleBackNoise= ".Count)
                        If line.StartsWith("LoadingCircleBackNoiseOpacity= ", 5) Then LoadingCircleBackNoiseOpacity = line.Remove(0, "LoadingCircleBackNoiseOpacity= ".Count)
                        If line.StartsWith("LoadingCircleHot1= ", 5) Then LoadingCircleHot1 = Color.FromArgb(line.Remove(0, "LoadingCircleHot1= ".Count))
                        If line.StartsWith("LoadingCircleHot2= ", 5) Then LoadingCircleHot2 = Color.FromArgb(line.Remove(0, "LoadingCircleHot2= ".Count))
                        If line.StartsWith("LoadingCircleHotGradient= ", 5) Then LoadingCircleHotGradient = line.Remove(0, "LoadingCircleHotGradient= ".Count)
                        If line.StartsWith("LoadingCircleHotGradientMode= ", 5) Then LoadingCircleHotGradientMode = ReturnGradientModeFromString(line.Remove(0, "LoadingCircleHotGradientMode= ".Count))
                        If line.StartsWith("LoadingCircleHotNoise= ", 5) Then LoadingCircleHotNoise = line.Remove(0, "LoadingCircleHotNoise= ".Count)
                        If line.StartsWith("LoadingCircleHotNoiseOpacity= ", 5) Then LoadingCircleHotNoiseOpacity = line.Remove(0, "LoadingCircleHotNoiseOpacity= ".Count)
                        If line.StartsWith("Shadow_Enabled= ", 5) Then Shadow_Enabled = line.Remove(0, "Shadow_Enabled= ".Count)
                        If line.StartsWith("Shadow_Color= ", 5) Then Shadow_Color = Color.FromArgb(line.Remove(0, "Shadow_Color= ".Count))
                        If line.StartsWith("Shadow_Blur= ", 5) Then Shadow_Blur = line.Remove(0, "Shadow_Blur= ".Count)
                        If line.StartsWith("Shadow_Opacity= ", 5) Then Shadow_Opacity = line.Remove(0, "Shadow_Opacity= ".Count) / 100
                        If line.StartsWith("Shadow_OffsetX= ", 5) Then Shadow_OffsetX = line.Remove(0, "Shadow_OffsetX= ".Count)
                        If line.StartsWith("Shadow_OffsetY= ", 5) Then Shadow_OffsetY = line.Remove(0, "Shadow_OffsetY= ".Count)
                    Next

                    'Return [Cursor]
                End If
            End Sub

            Overloads Function ToString(Signature As String) As String
                Dim tx As New List(Of String)
                tx.Clear()
                tx.Add(String.Format("<{0}>", Signature))
                tx.Add(String.Format("*Cursor_{0}_ArrowStyle= {1}", Signature, CInt(ArrowStyle)))
                tx.Add(String.Format("*Cursor_{0}_CircleStyle= {1}", Signature, CInt(CircleStyle)))
                tx.Add(String.Format("*Cursor_{0}_PrimaryColor1= {1}", Signature, PrimaryColor1.ToArgb))
                tx.Add(String.Format("*Cursor_{0}_PrimaryColor2= {1}", Signature, PrimaryColor2.ToArgb))
                tx.Add(String.Format("*Cursor_{0}_PrimaryColorGradient= {1}", Signature, PrimaryColorGradient))
                tx.Add(String.Format("*Cursor_{0}_PrimaryColorGradientMode= {1}", Signature, ReturnStringFromGradientMode(PrimaryColorGradientMode)))
                tx.Add(String.Format("*Cursor_{0}_PrimaryColorNoise= {1}", Signature, PrimaryColorNoise))
                tx.Add(String.Format("*Cursor_{0}_PrimaryColorNoiseOpacity= {1}", Signature, PrimaryColorNoiseOpacity))
                tx.Add(String.Format("*Cursor_{0}_SecondaryColor1= {1}", Signature, SecondaryColor1.ToArgb))
                tx.Add(String.Format("*Cursor_{0}_SecondaryColor2= {1}", Signature, SecondaryColor2.ToArgb))
                tx.Add(String.Format("*Cursor_{0}_SecondaryColorGradient= {1}", Signature, SecondaryColorGradient))
                tx.Add(String.Format("*Cursor_{0}_SecondaryColorGradientMode= {1}", Signature, ReturnStringFromGradientMode(SecondaryColorGradientMode)))
                tx.Add(String.Format("*Cursor_{0}_SecondaryColorNoise= {1}", Signature, SecondaryColorNoise))
                tx.Add(String.Format("*Cursor_{0}_SecondaryColorNoiseOpacity= {1}", Signature, SecondaryColorNoiseOpacity))
                tx.Add(String.Format("*Cursor_{0}_LoadingCircleBack1= {1}", Signature, LoadingCircleBack1.ToArgb))
                tx.Add(String.Format("*Cursor_{0}_LoadingCircleBack2= {1}", Signature, LoadingCircleBack2.ToArgb))
                tx.Add(String.Format("*Cursor_{0}_LoadingCircleBackGradient= {1}", Signature, LoadingCircleBackGradient))
                tx.Add(String.Format("*Cursor_{0}_LoadingCircleBackGradientMode= {1}", Signature, ReturnStringFromGradientMode(LoadingCircleBackGradientMode)))
                tx.Add(String.Format("*Cursor_{0}_LoadingCircleBackNoise= {1}", Signature, LoadingCircleBackNoise))
                tx.Add(String.Format("*Cursor_{0}_LoadingCircleBackNoiseOpacity= {1}", Signature, LoadingCircleBackNoiseOpacity))
                tx.Add(String.Format("*Cursor_{0}_LoadingCircleHot1= {1}", Signature, LoadingCircleHot1.ToArgb))
                tx.Add(String.Format("*Cursor_{0}_LoadingCircleHot2= {1}", Signature, LoadingCircleHot2.ToArgb))
                tx.Add(String.Format("*Cursor_{0}_LoadingCircleHotGradient= {1}", Signature, LoadingCircleHotGradient))
                tx.Add(String.Format("*Cursor_{0}_LoadingCircleHotGradientMode= {1}", Signature, ReturnStringFromGradientMode(LoadingCircleHotGradientMode)))
                tx.Add(String.Format("*Cursor_{0}_LoadingCircleHotNoise= {1}", Signature, LoadingCircleHotNoise))
                tx.Add(String.Format("*Cursor_{0}_LoadingCircleHotNoiseOpacity= {1}", Signature, LoadingCircleHotNoiseOpacity))
                tx.Add(String.Format("*Cursor_{0}_Shadow_Enabled= {1}", Signature, Shadow_Enabled))
                tx.Add(String.Format("*Cursor_{0}_Shadow_Color= {1}", Signature, Shadow_Color.ToArgb))
                tx.Add(String.Format("*Cursor_{0}_Shadow_Blur= {1}", Signature, Shadow_Blur))
                tx.Add(String.Format("*Cursor_{0}_Shadow_Opacity= {1}", Signature, Shadow_Opacity * 100))
                tx.Add(String.Format("*Cursor_{0}_Shadow_OffsetX= {1}", Signature, Shadow_OffsetX))
                tx.Add(String.Format("*Cursor_{0}_Shadow_OffsetY= {1}", Signature, Shadow_OffsetY))
                tx.Add(String.Format("</{0}>", Signature) & vbCrLf)
                Return tx.CString
            End Function

        End Structure
    End Class

#Region "Properties"
    Public Info As New Structures.Info With {
            .AppVersion = My.Application.Info.Version.ToString,
            .ThemeName = "Current Mode",
            .Description = "",
            .ThemeVersion = "1.0.0.0",
            .Author = Environment.UserName,
            .AuthorSocialMediaLink = ""}

    Public Windows11 As New Structures.Windows10x With {
            .Color_Index0 = Color.FromArgb(153, 235, 255),
            .Color_Index1 = Color.FromArgb(76, 194, 255),
            .Color_Index2 = Color.FromArgb(0, 145, 248),
            .Color_Index3 = Color.FromArgb(0, 120, 212),
            .Color_Index4 = Color.FromArgb(0, 103, 192),
            .Color_Index5 = Color.FromArgb(0, 62, 146),
            .Color_Index6 = Color.FromArgb(0, 26, 104),
            .Color_Index7 = Color.FromArgb(247, 99, 12),
            .Titlebar_Active = Color.FromArgb(0, 120, 212),
            .Titlebar_Inactive = Color.FromArgb(0, 0, 0),
            .StartMenu_Accent = Color.FromArgb(0, 103, 192),
            .WinMode_Light = True,
            .AppMode_Light = True,
            .Transparency = True,
            .ApplyAccentOnTitlebars = False,
            .ApplyAccentOnTaskbar = Converter_CP.Structures.Windows10x.AccentTaskbarLevels.None,
            .IncreaseTBTransparency = False,
            .TB_Blur = True}

    Public Windows10 As New Structures.Windows10x With {
            .Color_Index0 = Color.FromArgb(166, 216, 255),
            .Color_Index1 = Color.FromArgb(118, 185, 237),
            .Color_Index2 = Color.FromArgb(66, 156, 227),
            .Color_Index3 = Color.FromArgb(0, 120, 215),
            .Color_Index4 = Color.FromArgb(0, 90, 158),
            .Color_Index5 = Color.FromArgb(0, 66, 117),
            .Color_Index6 = Color.FromArgb(0, 38, 66),
            .Color_Index7 = Color.FromArgb(247, 99, 12),
            .Titlebar_Active = Color.FromArgb(0, 120, 215),
            .Titlebar_Inactive = Color.FromArgb(0, 0, 0),
            .StartMenu_Accent = Color.FromArgb(0, 90, 158),
            .WinMode_Light = False,
            .AppMode_Light = True,
            .Transparency = True,
            .ApplyAccentOnTitlebars = False,
            .ApplyAccentOnTaskbar = Converter_CP.Structures.Windows10x.AccentTaskbarLevels.None,
            .IncreaseTBTransparency = False,
            .TB_Blur = True}

    Public LogonUI10x As New Structures.LogonUI10x With {
        .DisableAcrylicBackgroundOnLogon = False, .DisableLogonBackgroundImage = False, .NoLockScreen = False}

    Public Windows8 As New Structures.Windows8 With {
                    .ColorizationColor = Color.FromArgb(246, 195, 74),
                    .ColorizationColorBalance = 78,
                    .Start = 0,
                    .StartColor = Color.FromArgb(30, 0, 84),
                    .AccentColor = Color.FromArgb(72, 29, 178),
                    .Theme = Structures.Windows7.Themes.Aero,
                    .LogonUI = 0,
                    .PersonalColors_Background = Color.FromArgb(30, 0, 84),
                    .PersonalColors_Accent = Color.FromArgb(72, 29, 178),
                    .NoLockScreen = False,
                    .LockScreenType = Structures.LogonUI7.Modes.Default_,
                    .LockScreenSystemID = 0}

    Public Windows7 As New Structures.Windows7 With {
            .ColorizationColor = Color.FromArgb(116, 184, 252),
            .ColorizationAfterglow = Color.FromArgb(116, 184, 252),
            .ColorizationColorBalance = 8,
            .ColorizationAfterglowBalance = 43,
            .ColorizationBlurBalance = 49,
            .ColorizationGlassReflectionIntensity = 0,
            .EnableAeroPeek = True,
            .AlwaysHibernateThumbnails = False,
            .Theme = Converter_CP.Structures.Windows7.Themes.Aero}

    Public WindowsVista As New Structures.WindowsVista With {
            .ColorizationColor = Color.FromArgb(64, 158, 254),
            .Theme = Converter_CP.Structures.Windows7.Themes.Aero}

    Public WindowsXP As New Structures.WindowsXP With {
        .Theme = Structures.WindowsXP.Themes.LunaBlue,
        .ColorScheme = "NormalColor",
        .ThemeFile = My.PATH_Windows & "\resources\Themes\Luna\Luna.msstyles"}

    Public LogonUI7 As New Structures.LogonUI7 With {
                    .Enabled = False,
                    .Mode = Structures.LogonUI7.Modes.Default_,
                    .ImagePath = "C:\Windows\Web\Wallpaper\Windows\img0.jpg",
                    .Color = Color.Black,
                    .Blur = False,
                    .Blur_Intensity = 0,
                    .Grayscale = False,
                    .Noise = False,
                    .Noise_Mode = Structures.LogonUI7.NoiseMode.Acrylic,
                    .Noise_Intensity = 0}

    Public LogonUIXP As New Structures.LogonUIXP With {
        .Enabled = True,
        .Mode = Structures.LogonUIXP.Modes.Default,
        .BackColor = Color.Black,
        .ShowMoreOptions = False}

    Public Win32 As New Structures.Win32UI With {
            .EnableTheming = True,
            .EnableGradient = True,
            .ActiveBorder = Color.FromArgb(180, 180, 180),
            .ActiveTitle = Color.FromArgb(153, 180, 209),
            .AppWorkspace = Color.FromArgb(171, 171, 171),
            .Background = Color.FromArgb(0, 0, 0),
            .ButtonAlternateFace = Color.FromArgb(0, 0, 0),
            .ButtonDkShadow = Color.FromArgb(105, 105, 105),
            .ButtonFace = Color.FromArgb(240, 240, 240),
            .ButtonHilight = Color.FromArgb(255, 255, 255),
            .ButtonLight = Color.FromArgb(227, 227, 227),
            .ButtonShadow = Color.FromArgb(160, 160, 160),
            .ButtonText = Color.FromArgb(0, 0, 0),
            .GradientActiveTitle = Color.FromArgb(185, 209, 234),
            .GradientInactiveTitle = Color.FromArgb(215, 228, 242),
            .GrayText = Color.FromArgb(109, 109, 109),
            .HilightText = Color.FromArgb(255, 255, 255),
            .HotTrackingColor = Color.FromArgb(0, 102, 204),
            .InactiveBorder = Color.FromArgb(244, 247, 252),
            .InactiveTitle = Color.FromArgb(191, 205, 219),
            .InactiveTitleText = Color.FromArgb(0, 0, 0),
            .InfoText = Color.FromArgb(0, 0, 0),
            .InfoWindow = Color.FromArgb(255, 255, 225),
            .Menu = Color.FromArgb(240, 240, 240),
            .MenuBar = Color.FromArgb(240, 240, 240),
            .MenuText = Color.FromArgb(0, 0, 0),
            .Scrollbar = Color.FromArgb(200, 200, 200),
            .TitleText = Color.FromArgb(0, 0, 0),
            .Window = Color.FromArgb(255, 255, 255),
            .WindowFrame = Color.FromArgb(100, 100, 100),
            .WindowText = Color.FromArgb(0, 0, 0),
            .Hilight = Color.FromArgb(0, 120, 215),
            .MenuHilight = Color.FromArgb(0, 120, 215),
            .Desktop = Color.FromArgb(0, 0, 0)
            }

    Public WallpaperTone_W11 As New Structures.WallpaperTone With {
        .Enabled = False,
        .Image = My.PATH_Windows & "\Web\Wallpaper\Windows\img0.jpg",
        .H = 0, .S = 100, .L = 100}

    Public WallpaperTone_W10 As New Structures.WallpaperTone With {
        .Enabled = False,
        .Image = My.PATH_Windows & "\Web\Wallpaper\Windows\img0.jpg",
        .H = 0, .S = 100, .L = 100}

    Public WallpaperTone_W8 As New Structures.WallpaperTone With {
        .Enabled = False,
        .Image = My.PATH_Windows & "\Web\Wallpaper\Windows\img0.jpg",
        .H = 0, .S = 100, .L = 100}

    Public WallpaperTone_W7 As New Structures.WallpaperTone With {
        .Enabled = False,
        .Image = My.PATH_Windows & "\Web\Wallpaper\Windows\img0.jpg",
        .H = 0, .S = 100, .L = 100}

    Public WallpaperTone_WVista As New Structures.WallpaperTone With {
        .Enabled = False,
        .Image = My.PATH_Windows & "\Web\Wallpaper\Windows\img0.jpg",
        .H = 0, .S = 100, .L = 100}

    Public WallpaperTone_WXP As New Structures.WallpaperTone With {
        .Enabled = False,
        .Image = My.PATH_Windows & "\Web\Wallpaper\Bliss.bmp",
        .H = 0, .S = 100, .L = 100}

    Public WindowsEffects As New Structures.WinEffects With {
        .Enabled = True,
        .WindowAnimation = True,
        .WindowShadow = True,
        .WindowUIEffects = True,
        .MenuAnimation = True,
        .MenuSelectionFade = True,
        .MenuFade = Structures.WinEffects.MenuAnimType.Fade,
        .MenuShowDelay = 400,
        .ComboBoxAnimation = True,
        .ListBoxSmoothScrolling = True,
        .TooltipAnimation = True,
        .TooltipFade = Structures.WinEffects.MenuAnimType.Fade,
        .IconsShadow = True,
        .IconsDesktopTranslSel = True,
        .ShowWinContentDrag = True,
        .BalloonNotifications = False,
        .PaintDesktopVersion = False,
        .ShowSecondsInSystemClock = False,
        .Win11ClassicContextMenu = False,
        .SysListView32 = False,
        .SnapCursorToDefButton = False,
        .ShakeToMinimize = True,
        .NotificationDuration = 5,
        .FocusRectWidth = 1,
        .FocusRectHeight = 1,
        .KeyboardUnderline = False,
        .Caret = 1,
        .AWT_Enabled = False,
        .AWT_Delay = 0,
        .AWT_BringActivatedWindowToTop = False,
        .Win11BootDots = Not My.W11,
        .Win11ExplorerBar = Structures.WinEffects.ExplorerBar.Default,
        .DisableNavBar = False}

    Public MetricsFonts As New Structures.MetricsFonts With {
                .Enabled = True,
                .BorderWidth = 1,
                .CaptionHeight = 22,
                .CaptionWidth = 22,
                .IconSpacing = 75,
                .IconVerticalSpacing = 75,
                .MenuHeight = 19,
                .MenuWidth = 19,
                .PaddedBorderWidth = 4,
                .ScrollHeight = 19,
                .ScrollWidth = 19,
                .SmCaptionHeight = 22,
                .SmCaptionWidth = 22,
                .DesktopIconSize = 48,
                .ShellIconSize = 32,
                .CaptionFont = New Font("Segoe UI", 9, FontStyle.Regular),
                .IconFont = New Font("Segoe UI", 9, FontStyle.Regular),
                .MenuFont = New Font("Segoe UI", 9, FontStyle.Regular),
                .MessageFont = New Font("Segoe UI", 9, FontStyle.Regular),
                .SmCaptionFont = New Font("Segoe UI", 9, FontStyle.Regular),
                .StatusFont = New Font("Segoe UI", 9, FontStyle.Regular),
                .FontSubstitute_MSShellDlg = "Microsoft Sans Serif", .FontSubstitute_MSShellDlg2 = "Tahoma",
                .FontSubstitute_SegoeUI = ""}

    Public AltTab As New Structures.AltTab With {.Enabled = True, .Style = Structures.AltTab.Styles.Default, .Win10Opacity = 95}

    Public CommandPrompt As New Structures.Console With {
                    .Enabled = False,
                    .ColorTable00 = Color.FromArgb(12, 12, 12),
                    .ColorTable01 = Color.FromArgb(0, 55, 218),
                    .ColorTable02 = Color.FromArgb(19, 161, 14),
                    .ColorTable03 = Color.FromArgb(58, 150, 221),
                    .ColorTable04 = Color.FromArgb(197, 15, 31),
                    .ColorTable05 = Color.FromArgb(136, 23, 152),
                    .ColorTable06 = Color.FromArgb(193, 156, 0),
                    .ColorTable07 = Color.FromArgb(204, 204, 204),
                    .ColorTable08 = Color.FromArgb(118, 118, 118),
                    .ColorTable09 = Color.FromArgb(59, 120, 255),
                    .ColorTable10 = Color.FromArgb(22, 198, 12),
                    .ColorTable11 = Color.FromArgb(97, 214, 214),
                    .ColorTable12 = Color.FromArgb(231, 72, 86),
                    .ColorTable13 = Color.FromArgb(180, 0, 158),
                    .ColorTable14 = Color.FromArgb(249, 241, 165),
                    .ColorTable15 = Color.FromArgb(242, 242, 242),
                    .PopupForeground = 5,
                    .PopupBackground = 15,
                    .ScreenColorsForeground = 7,
                    .ScreenColorsBackground = 0,
                    .CursorSize = 19,
                    .FaceName = "Consolas",
                    .FontRaster = False,
                    .FontSize = 18 * 65536,
                    .FontWeight = 400,
                    .W10_1909_CursorType = 0,
                    .W10_1909_CursorColor = Color.White,
                    .W10_1909_ForceV2 = True,
                    .W10_1909_LineSelection = False,
                    .W10_1909_TerminalScrolling = False,
                    .W10_1909_WindowAlpha = 255}

    Public PowerShellx86 As New Structures.Console With {
                        .Enabled = False,
                        .ColorTable00 = Color.FromArgb(12, 12, 12),
                        .ColorTable01 = Color.FromArgb(0, 55, 218),
                        .ColorTable02 = Color.FromArgb(19, 161, 14),
                        .ColorTable03 = Color.FromArgb(58, 150, 221),
                        .ColorTable04 = Color.FromArgb(197, 15, 31),
                        .ColorTable05 = Color.FromArgb(1, 36, 86),
                        .ColorTable06 = Color.FromArgb(238, 237, 240),
                        .ColorTable07 = Color.FromArgb(204, 204, 204),
                        .ColorTable08 = Color.FromArgb(118, 118, 118),
                        .ColorTable09 = Color.FromArgb(59, 120, 255),
                        .ColorTable10 = Color.FromArgb(22, 198, 12),
                        .ColorTable11 = Color.FromArgb(97, 214, 214),
                        .ColorTable12 = Color.FromArgb(231, 72, 86),
                        .ColorTable13 = Color.FromArgb(180, 0, 158),
                        .ColorTable14 = Color.FromArgb(249, 241, 165),
                        .ColorTable15 = Color.FromArgb(242, 242, 242),
                        .PopupForeground = 15,
                        .PopupBackground = 3,
                        .ScreenColorsForeground = 6,
                        .ScreenColorsBackground = 5,
                        .CursorSize = 19,
                        .FaceName = "Consolas",
                        .FontRaster = False,
                        .FontSize = 16 * 65536,
                        .FontWeight = 400,
                        .W10_1909_CursorType = 0,
                        .W10_1909_CursorColor = Color.White,
                        .W10_1909_ForceV2 = True,
                        .W10_1909_LineSelection = False,
                        .W10_1909_TerminalScrolling = False,
                        .W10_1909_WindowAlpha = 255}

    Public PowerShellx64 As New Structures.Console With {
                        .Enabled = False,
                        .ColorTable00 = Color.FromArgb(12, 12, 12),
                        .ColorTable01 = Color.FromArgb(0, 55, 218),
                        .ColorTable02 = Color.FromArgb(19, 161, 14),
                        .ColorTable03 = Color.FromArgb(58, 150, 221),
                        .ColorTable04 = Color.FromArgb(197, 15, 31),
                        .ColorTable05 = Color.FromArgb(1, 36, 86),
                        .ColorTable06 = Color.FromArgb(238, 237, 240),
                        .ColorTable07 = Color.FromArgb(204, 204, 204),
                        .ColorTable08 = Color.FromArgb(118, 118, 118),
                        .ColorTable09 = Color.FromArgb(59, 120, 255),
                        .ColorTable10 = Color.FromArgb(22, 198, 12),
                        .ColorTable11 = Color.FromArgb(97, 214, 214),
                        .ColorTable12 = Color.FromArgb(231, 72, 86),
                        .ColorTable13 = Color.FromArgb(180, 0, 158),
                        .ColorTable14 = Color.FromArgb(249, 241, 165),
                        .ColorTable15 = Color.FromArgb(242, 242, 242),
                        .PopupForeground = 15,
                        .PopupBackground = 3,
                        .ScreenColorsForeground = 6,
                        .ScreenColorsBackground = 5,
                        .CursorSize = 19,
                        .FaceName = "Consolas",
                        .FontRaster = False,
                        .FontSize = 16 * 65536,
                        .FontWeight = 400,
                        .W10_1909_CursorType = 0,
                        .W10_1909_CursorColor = Color.White,
                        .W10_1909_ForceV2 = True,
                        .W10_1909_LineSelection = False,
                        .W10_1909_TerminalScrolling = False,
                        .W10_1909_WindowAlpha = 255}

    Public Terminal As New WinTerminal_Converter("", WinTerminal_Converter.Mode.Empty)

    Public TerminalPreview As New WinTerminal_Converter("", WinTerminal_Converter.Mode.Empty)

#Region "Cursors"
    Public Cursor_Enabled As Boolean = False

    Public Cursor_Shadow As Boolean = False

    Public Cursor_Sonar As Boolean = False

    Public Cursor_Trails As Integer = 0

    Public Cursor_Arrow As New Structures.Cursor With {
                    .PrimaryColor1 = Color.White,
                    .PrimaryColor2 = Color.White,
                    .PrimaryColorGradient = False,
                    .PrimaryColorGradientMode = Structures.Cursor.GradientMode.Vertical,
                    .PrimaryColorNoise = False,
                    .PrimaryColorNoiseOpacity = 0.25,
                    .SecondaryColor1 = Color.Black,
                    .SecondaryColor2 = Color.FromArgb(64, 65, 75),
                    .SecondaryColorGradient = True,
                    .SecondaryColorGradientMode = Structures.Cursor.GradientMode.Vertical,
                    .SecondaryColorNoise = False,
                    .SecondaryColorNoiseOpacity = 0.25,
                    .LoadingCircleBack1 = Color.FromArgb(42, 151, 243),
                    .LoadingCircleBack2 = Color.FromArgb(42, 151, 243),
                    .LoadingCircleBackGradient = False,
                    .LoadingCircleBackGradientMode = Structures.Cursor.GradientMode.Circle,
                    .LoadingCircleBackNoise = False,
                    .LoadingCircleBackNoiseOpacity = 0.25,
                    .LoadingCircleHot1 = Color.FromArgb(37, 204, 255),
                    .LoadingCircleHot2 = Color.FromArgb(37, 204, 255),
                    .LoadingCircleHotGradient = False,
                    .LoadingCircleHotGradientMode = Structures.Cursor.GradientMode.Circle,
                    .LoadingCircleHotNoise = False,
                    .LoadingCircleHotNoiseOpacity = 0.25,
                    .ArrowStyle = Structures.Cursor.ArrowStyles.Aero,
                    .CircleStyle = Structures.Cursor.CircleStyles.Aero,
                    .Shadow_Enabled = False,
                    .Shadow_Color = Color.Black,
                    .Shadow_Blur = 5,
                    .Shadow_Opacity = 0.3,
                    .Shadow_OffsetX = 2, .Shadow_OffsetY = 2}

    Public Cursor_AppLoading As New Structures.Cursor With {
                    .PrimaryColor1 = Color.White,
                    .PrimaryColor2 = Color.White,
                    .PrimaryColorGradient = False,
                    .PrimaryColorGradientMode = Structures.Cursor.GradientMode.Circle,
                    .PrimaryColorNoise = False,
                    .PrimaryColorNoiseOpacity = 0.25,
                    .SecondaryColor1 = Color.Black,
                    .SecondaryColor2 = Color.FromArgb(64, 65, 75),
                    .SecondaryColorGradient = True,
                    .SecondaryColorGradientMode = Structures.Cursor.GradientMode.Vertical,
                    .SecondaryColorNoise = False,
                    .SecondaryColorNoiseOpacity = 0.25,
                    .LoadingCircleBack1 = Color.FromArgb(42, 151, 243),
                    .LoadingCircleBack2 = Color.FromArgb(42, 151, 243),
                    .LoadingCircleBackGradient = False,
                    .LoadingCircleBackGradientMode = Structures.Cursor.GradientMode.Circle,
                    .LoadingCircleBackNoise = False,
                    .LoadingCircleBackNoiseOpacity = 0.25,
                    .LoadingCircleHot1 = Color.FromArgb(37, 204, 255),
                    .LoadingCircleHot2 = Color.FromArgb(37, 204, 255),
                    .LoadingCircleHotGradient = False,
                    .LoadingCircleHotGradientMode = Structures.Cursor.GradientMode.Circle,
                    .LoadingCircleHotNoise = False,
                    .LoadingCircleHotNoiseOpacity = 0.25,
                    .ArrowStyle = Structures.Cursor.ArrowStyles.Aero,
                    .CircleStyle = Structures.Cursor.ArrowStyles.Aero,
                    .Shadow_Enabled = False,
                    .Shadow_Color = Color.Black,
                    .Shadow_Blur = 5,
                    .Shadow_Opacity = 0.3,
                    .Shadow_OffsetX = 2, .Shadow_OffsetY = 2}

    Public Cursor_Busy As New Structures.Cursor With {
                    .PrimaryColor1 = Color.White,
                    .PrimaryColor2 = Color.White,
                    .PrimaryColorGradient = False,
                    .PrimaryColorGradientMode = Structures.Cursor.GradientMode.Circle,
                    .PrimaryColorNoise = False,
                    .PrimaryColorNoiseOpacity = 0.25,
                    .SecondaryColor1 = Color.Black,
                    .SecondaryColor2 = Color.FromArgb(64, 65, 75),
                    .SecondaryColorGradient = True,
                    .SecondaryColorGradientMode = Structures.Cursor.GradientMode.Circle,
                    .SecondaryColorNoise = False,
                    .SecondaryColorNoiseOpacity = 0.25,
                    .LoadingCircleBack1 = Color.FromArgb(42, 151, 243),
                    .LoadingCircleBack2 = Color.FromArgb(42, 151, 243),
                    .LoadingCircleBackGradient = False,
                    .LoadingCircleBackGradientMode = Structures.Cursor.GradientMode.Circle,
                    .LoadingCircleBackNoise = False,
                    .LoadingCircleBackNoiseOpacity = 0.25,
                    .LoadingCircleHot1 = Color.FromArgb(37, 204, 255),
                    .LoadingCircleHot2 = Color.FromArgb(37, 204, 255),
                    .LoadingCircleHotGradient = False,
                    .LoadingCircleHotGradientMode = Structures.Cursor.GradientMode.Circle,
                    .LoadingCircleHotNoise = False,
                    .LoadingCircleHotNoiseOpacity = 0.25,
                    .ArrowStyle = Structures.Cursor.ArrowStyles.Aero,
                    .CircleStyle = Structures.Cursor.CircleStyles.Aero,
                    .Shadow_Enabled = False,
                    .Shadow_Color = Color.Black,
                    .Shadow_Blur = 5,
                    .Shadow_Opacity = 0.3,
                    .Shadow_OffsetX = 2, .Shadow_OffsetY = 2}

    Public Cursor_Help As New Structures.Cursor With {
                    .PrimaryColor1 = Color.White,
                    .PrimaryColor2 = Color.White,
                    .PrimaryColorGradient = False,
                    .PrimaryColorGradientMode = Structures.Cursor.GradientMode.Vertical,
                    .PrimaryColorNoise = False,
                    .PrimaryColorNoiseOpacity = 0.25,
                    .SecondaryColor1 = Color.Black,
                    .SecondaryColor2 = Color.FromArgb(64, 65, 75),
                    .SecondaryColorGradient = True,
                    .SecondaryColorGradientMode = Structures.Cursor.GradientMode.Vertical,
                    .SecondaryColorNoise = False,
                    .SecondaryColorNoiseOpacity = 0.25,
                    .LoadingCircleBack1 = Color.FromArgb(42, 151, 243),
                    .LoadingCircleBack2 = Color.FromArgb(42, 151, 243),
                    .LoadingCircleBackGradient = False,
                    .LoadingCircleBackGradientMode = Structures.Cursor.GradientMode.Circle,
                    .LoadingCircleBackNoise = False,
                    .LoadingCircleBackNoiseOpacity = 0.25,
                    .LoadingCircleHot1 = Color.FromArgb(37, 204, 255),
                    .LoadingCircleHot2 = Color.FromArgb(37, 204, 255),
                    .LoadingCircleHotGradient = False,
                    .LoadingCircleHotGradientMode = Structures.Cursor.GradientMode.Circle,
                    .LoadingCircleHotNoise = False,
                    .LoadingCircleHotNoiseOpacity = 0.25,
                    .ArrowStyle = Structures.Cursor.ArrowStyles.Aero,
                    .CircleStyle = Structures.Cursor.CircleStyles.Aero,
                    .Shadow_Enabled = False,
                    .Shadow_Color = Color.Black,
                    .Shadow_Blur = 5,
                    .Shadow_Opacity = 0.3,
                    .Shadow_OffsetX = 2, .Shadow_OffsetY = 2}

    Public Cursor_Move As New Structures.Cursor With {
                    .PrimaryColor1 = Color.White,
                    .PrimaryColor2 = Color.White,
                    .PrimaryColorGradient = False,
                    .PrimaryColorGradientMode = Structures.Cursor.GradientMode.Vertical,
                    .PrimaryColorNoise = False,
                    .PrimaryColorNoiseOpacity = 0.25,
                    .SecondaryColor1 = Color.Black,
                    .SecondaryColor2 = Color.FromArgb(64, 65, 75),
                    .SecondaryColorGradient = True,
                    .SecondaryColorGradientMode = Structures.Cursor.GradientMode.Vertical,
                    .SecondaryColorNoise = False,
                    .SecondaryColorNoiseOpacity = 0.25,
                    .LoadingCircleBack1 = Color.FromArgb(42, 151, 243),
                    .LoadingCircleBack2 = Color.FromArgb(42, 151, 243),
                    .LoadingCircleBackGradient = False,
                    .LoadingCircleBackGradientMode = Structures.Cursor.GradientMode.Circle,
                    .LoadingCircleBackNoise = False,
                    .LoadingCircleBackNoiseOpacity = 0.25,
                    .LoadingCircleHot1 = Color.FromArgb(37, 204, 255),
                    .LoadingCircleHot2 = Color.FromArgb(37, 204, 255),
                    .LoadingCircleHotGradient = False,
                    .LoadingCircleHotGradientMode = Structures.Cursor.GradientMode.Circle,
                    .LoadingCircleHotNoise = False,
                    .LoadingCircleHotNoiseOpacity = 0.25,
                    .ArrowStyle = Structures.Cursor.ArrowStyles.Aero,
                    .CircleStyle = Structures.Cursor.CircleStyles.Aero,
                    .Shadow_Enabled = False,
                    .Shadow_Color = Color.Black,
                    .Shadow_Blur = 5,
                    .Shadow_Opacity = 0.3,
                    .Shadow_OffsetX = 2, .Shadow_OffsetY = 2}

    Public Cursor_NS As New Structures.Cursor With {
                    .PrimaryColor1 = Color.White,
                    .PrimaryColor2 = Color.White,
                    .PrimaryColorGradient = False,
                    .PrimaryColorGradientMode = Structures.Cursor.GradientMode.Vertical,
                    .PrimaryColorNoise = False,
                    .PrimaryColorNoiseOpacity = 0.25,
                    .SecondaryColor1 = Color.Black,
                    .SecondaryColor2 = Color.FromArgb(64, 65, 75),
                    .SecondaryColorGradient = True,
                    .SecondaryColorGradientMode = Structures.Cursor.GradientMode.Vertical,
                    .SecondaryColorNoise = False,
                    .SecondaryColorNoiseOpacity = 0.25,
                    .LoadingCircleBack1 = Color.FromArgb(42, 151, 243),
                    .LoadingCircleBack2 = Color.FromArgb(42, 151, 243),
                    .LoadingCircleBackGradient = False,
                    .LoadingCircleBackGradientMode = Structures.Cursor.GradientMode.Circle,
                    .LoadingCircleBackNoise = False,
                    .LoadingCircleBackNoiseOpacity = 0.25,
                    .LoadingCircleHot1 = Color.FromArgb(37, 204, 255),
                    .LoadingCircleHot2 = Color.FromArgb(37, 204, 255),
                    .LoadingCircleHotGradient = False,
                    .LoadingCircleHotGradientMode = Structures.Cursor.GradientMode.Circle,
                    .LoadingCircleHotNoise = False,
                    .LoadingCircleHotNoiseOpacity = 0.25,
                    .ArrowStyle = Structures.Cursor.ArrowStyles.Aero,
                    .CircleStyle = Structures.Cursor.CircleStyles.Aero,
                    .Shadow_Enabled = False,
                    .Shadow_Color = Color.Black,
                    .Shadow_Blur = 5,
                    .Shadow_Opacity = 0.3,
                    .Shadow_OffsetX = 2, .Shadow_OffsetY = 2}

    Public Cursor_EW As New Structures.Cursor With {
                    .PrimaryColor1 = Color.White,
                    .PrimaryColor2 = Color.White,
                    .PrimaryColorGradient = False,
                    .PrimaryColorGradientMode = Structures.Cursor.GradientMode.Vertical,
                    .PrimaryColorNoise = False,
                    .PrimaryColorNoiseOpacity = 0.25,
                    .SecondaryColor1 = Color.Black,
                    .SecondaryColor2 = Color.FromArgb(64, 65, 75),
                    .SecondaryColorGradient = True,
                    .SecondaryColorGradientMode = Structures.Cursor.GradientMode.Vertical,
                    .SecondaryColorNoise = False,
                    .SecondaryColorNoiseOpacity = 0.25,
                    .LoadingCircleBack1 = Color.FromArgb(42, 151, 243),
                    .LoadingCircleBack2 = Color.FromArgb(42, 151, 243),
                    .LoadingCircleBackGradient = False,
                    .LoadingCircleBackGradientMode = Structures.Cursor.GradientMode.Circle,
                    .LoadingCircleBackNoise = False,
                    .LoadingCircleBackNoiseOpacity = 0.25,
                    .LoadingCircleHot1 = Color.FromArgb(37, 204, 255),
                    .LoadingCircleHot2 = Color.FromArgb(37, 204, 255),
                    .LoadingCircleHotGradient = False,
                    .LoadingCircleHotGradientMode = Structures.Cursor.GradientMode.Circle,
                    .LoadingCircleHotNoise = False,
                    .LoadingCircleHotNoiseOpacity = 0.25,
                    .ArrowStyle = Structures.Cursor.ArrowStyles.Aero,
                    .CircleStyle = Structures.Cursor.CircleStyles.Aero,
                    .Shadow_Enabled = False,
                    .Shadow_Color = Color.Black,
                    .Shadow_Blur = 5,
                    .Shadow_Opacity = 0.3,
                    .Shadow_OffsetX = 2, .Shadow_OffsetY = 2}

    Public Cursor_NESW As New Structures.Cursor With {
                    .PrimaryColor1 = Color.White,
                    .PrimaryColor2 = Color.White,
                    .PrimaryColorGradient = False,
                    .PrimaryColorGradientMode = Structures.Cursor.GradientMode.Vertical,
                    .PrimaryColorNoise = False,
                    .PrimaryColorNoiseOpacity = 0.25,
                    .SecondaryColor1 = Color.Black,
                    .SecondaryColor2 = Color.FromArgb(64, 65, 75),
                    .SecondaryColorGradient = True,
                    .SecondaryColorGradientMode = Structures.Cursor.GradientMode.Vertical,
                    .SecondaryColorNoise = False,
                    .SecondaryColorNoiseOpacity = 0.25,
                    .LoadingCircleBack1 = Color.FromArgb(42, 151, 243),
                    .LoadingCircleBack2 = Color.FromArgb(42, 151, 243),
                    .LoadingCircleBackGradient = False,
                    .LoadingCircleBackGradientMode = Structures.Cursor.GradientMode.Circle,
                    .LoadingCircleBackNoise = False,
                    .LoadingCircleBackNoiseOpacity = 0.25,
                    .LoadingCircleHot1 = Color.FromArgb(37, 204, 255),
                    .LoadingCircleHot2 = Color.FromArgb(37, 204, 255),
                    .LoadingCircleHotGradient = False,
                    .LoadingCircleHotGradientMode = Structures.Cursor.GradientMode.Circle,
                    .LoadingCircleHotNoise = False,
                    .LoadingCircleHotNoiseOpacity = 0.25,
                    .ArrowStyle = Structures.Cursor.ArrowStyles.Aero,
                    .CircleStyle = Structures.Cursor.CircleStyles.Aero,
                    .Shadow_Enabled = False,
                    .Shadow_Color = Color.Black,
                    .Shadow_Blur = 5,
                    .Shadow_Opacity = 0.3,
                    .Shadow_OffsetX = 2, .Shadow_OffsetY = 2}

    Public Cursor_NWSE As New Structures.Cursor With {
                    .PrimaryColor1 = Color.White,
                    .PrimaryColor2 = Color.White,
                    .PrimaryColorGradient = False,
                    .PrimaryColorGradientMode = Structures.Cursor.GradientMode.Vertical,
                    .PrimaryColorNoise = False,
                    .PrimaryColorNoiseOpacity = 0.25,
                    .SecondaryColor1 = Color.Black,
                    .SecondaryColor2 = Color.FromArgb(64, 65, 75),
                    .SecondaryColorGradient = True,
                    .SecondaryColorGradientMode = Structures.Cursor.GradientMode.Vertical,
                    .SecondaryColorNoise = False,
                    .SecondaryColorNoiseOpacity = 0.25,
                    .LoadingCircleBack1 = Color.FromArgb(42, 151, 243),
                    .LoadingCircleBack2 = Color.FromArgb(42, 151, 243),
                    .LoadingCircleBackGradient = False,
                    .LoadingCircleBackGradientMode = Structures.Cursor.GradientMode.Circle,
                    .LoadingCircleBackNoise = False,
                    .LoadingCircleBackNoiseOpacity = 0.25,
                    .LoadingCircleHot1 = Color.FromArgb(37, 204, 255),
                    .LoadingCircleHot2 = Color.FromArgb(37, 204, 255),
                    .LoadingCircleHotGradient = False,
                    .LoadingCircleHotGradientMode = Structures.Cursor.GradientMode.Circle,
                    .LoadingCircleHotNoise = False,
                    .LoadingCircleHotNoiseOpacity = 0.25,
                    .ArrowStyle = Structures.Cursor.ArrowStyles.Aero,
                    .CircleStyle = Structures.Cursor.CircleStyles.Aero,
                    .Shadow_Enabled = False,
                    .Shadow_Color = Color.Black,
                    .Shadow_Blur = 5,
                    .Shadow_Opacity = 0.3,
                    .Shadow_OffsetX = 2, .Shadow_OffsetY = 2}

    Public Cursor_Up As New Structures.Cursor With {
                    .PrimaryColor1 = Color.White,
                    .PrimaryColor2 = Color.White,
                    .PrimaryColorGradient = False,
                    .PrimaryColorGradientMode = Structures.Cursor.GradientMode.Vertical,
                    .PrimaryColorNoise = False,
                    .PrimaryColorNoiseOpacity = 0.25,
                    .SecondaryColor1 = Color.Black,
                    .SecondaryColor2 = Color.FromArgb(64, 65, 75),
                    .SecondaryColorGradient = True,
                    .SecondaryColorGradientMode = Structures.Cursor.GradientMode.Vertical,
                    .SecondaryColorNoise = False,
                    .SecondaryColorNoiseOpacity = 0.25,
                    .LoadingCircleBack1 = Color.FromArgb(42, 151, 243),
                    .LoadingCircleBack2 = Color.FromArgb(42, 151, 243),
                    .LoadingCircleBackGradient = False,
                    .LoadingCircleBackGradientMode = Structures.Cursor.GradientMode.Circle,
                    .LoadingCircleBackNoise = False,
                    .LoadingCircleBackNoiseOpacity = 0.25,
                    .LoadingCircleHot1 = Color.FromArgb(37, 204, 255),
                    .LoadingCircleHot2 = Color.FromArgb(37, 204, 255),
                    .LoadingCircleHotGradient = False,
                    .LoadingCircleHotGradientMode = Structures.Cursor.GradientMode.Circle,
                    .LoadingCircleHotNoise = False,
                    .LoadingCircleHotNoiseOpacity = 0.25,
                    .ArrowStyle = Structures.Cursor.ArrowStyles.Aero,
                    .CircleStyle = Structures.Cursor.CircleStyles.Aero,
                    .Shadow_Enabled = False,
                    .Shadow_Color = Color.Black,
                    .Shadow_Blur = 5,
                    .Shadow_Opacity = 0.3,
                    .Shadow_OffsetX = 2, .Shadow_OffsetY = 2}

    Public Cursor_Pen As New Structures.Cursor With {
                    .PrimaryColor1 = Color.White,
                    .PrimaryColor2 = Color.White,
                    .PrimaryColorGradient = False,
                    .PrimaryColorGradientMode = Structures.Cursor.GradientMode.Vertical,
                    .PrimaryColorNoise = False,
                    .PrimaryColorNoiseOpacity = 0.25,
                    .SecondaryColor1 = Color.Black,
                    .SecondaryColor2 = Color.FromArgb(64, 65, 75),
                    .SecondaryColorGradient = True,
                    .SecondaryColorGradientMode = Structures.Cursor.GradientMode.Vertical,
                    .SecondaryColorNoise = False,
                    .SecondaryColorNoiseOpacity = 0.25,
                    .LoadingCircleBack1 = Color.FromArgb(42, 151, 243),
                    .LoadingCircleBack2 = Color.FromArgb(42, 151, 243),
                    .LoadingCircleBackGradient = False,
                    .LoadingCircleBackGradientMode = Structures.Cursor.GradientMode.Circle,
                    .LoadingCircleBackNoise = False,
                    .LoadingCircleBackNoiseOpacity = 0.25,
                    .LoadingCircleHot1 = Color.FromArgb(37, 204, 255),
                    .LoadingCircleHot2 = Color.FromArgb(37, 204, 255),
                    .LoadingCircleHotGradient = False,
                    .LoadingCircleHotGradientMode = Structures.Cursor.GradientMode.Circle,
                    .LoadingCircleHotNoise = False,
                    .LoadingCircleHotNoiseOpacity = 0.25,
                    .ArrowStyle = Structures.Cursor.ArrowStyles.Aero,
                    .CircleStyle = Structures.Cursor.CircleStyles.Aero,
                    .Shadow_Enabled = False,
                    .Shadow_Color = Color.Black,
                    .Shadow_Blur = 5,
                    .Shadow_Opacity = 0.3,
                    .Shadow_OffsetX = 2, .Shadow_OffsetY = 2}

    Public Cursor_None As New Structures.Cursor With {
                    .PrimaryColor1 = Color.White,
                    .PrimaryColor2 = Color.White,
                    .PrimaryColorGradient = False,
                    .PrimaryColorGradientMode = Structures.Cursor.GradientMode.Vertical,
                    .PrimaryColorNoise = False,
                    .PrimaryColorNoiseOpacity = 0.25,
                    .SecondaryColor1 = Color.FromArgb(255, 0, 0),
                    .SecondaryColor2 = Color.FromArgb(255, 0, 0),
                    .SecondaryColorGradient = True,
                    .SecondaryColorGradientMode = Structures.Cursor.GradientMode.Vertical,
                    .SecondaryColorNoise = False,
                    .SecondaryColorNoiseOpacity = 0.25,
                    .LoadingCircleBack1 = Color.FromArgb(42, 151, 243),
                    .LoadingCircleBack2 = Color.FromArgb(42, 151, 243),
                    .LoadingCircleBackGradient = False,
                    .LoadingCircleBackGradientMode = Structures.Cursor.GradientMode.Circle,
                    .LoadingCircleBackNoise = False,
                    .LoadingCircleBackNoiseOpacity = 0.25,
                    .LoadingCircleHot1 = Color.FromArgb(37, 204, 255),
                    .LoadingCircleHot2 = Color.FromArgb(37, 204, 255),
                    .LoadingCircleHotGradient = False,
                    .LoadingCircleHotGradientMode = Structures.Cursor.GradientMode.Circle,
                    .LoadingCircleHotNoise = False,
                    .LoadingCircleHotNoiseOpacity = 0.25,
                    .ArrowStyle = Structures.Cursor.ArrowStyles.Aero,
                    .CircleStyle = Structures.Cursor.CircleStyles.Aero,
                    .Shadow_Enabled = False,
                    .Shadow_Color = Color.Black,
                    .Shadow_Blur = 5,
                    .Shadow_Opacity = 0.3,
                    .Shadow_OffsetX = 2, .Shadow_OffsetY = 2}

    Public Cursor_Link As New Structures.Cursor With {
                    .PrimaryColor1 = Color.White,
                    .PrimaryColor2 = Color.White,
                    .PrimaryColorGradient = False,
                    .PrimaryColorGradientMode = Structures.Cursor.GradientMode.Vertical,
                    .PrimaryColorNoise = False,
                    .PrimaryColorNoiseOpacity = 0.25,
                    .SecondaryColor1 = Color.Black,
                    .SecondaryColor2 = Color.FromArgb(64, 65, 75),
                    .SecondaryColorGradient = True,
                    .SecondaryColorGradientMode = Structures.Cursor.GradientMode.Vertical,
                    .SecondaryColorNoise = False,
                    .SecondaryColorNoiseOpacity = 0.25,
                    .LoadingCircleBack1 = Color.FromArgb(42, 151, 243),
                    .LoadingCircleBack2 = Color.FromArgb(42, 151, 243),
                    .LoadingCircleBackGradient = False,
                    .LoadingCircleBackGradientMode = Structures.Cursor.GradientMode.Circle,
                    .LoadingCircleBackNoise = False,
                    .LoadingCircleBackNoiseOpacity = 0.25,
                    .LoadingCircleHot1 = Color.FromArgb(37, 204, 255),
                    .LoadingCircleHot2 = Color.FromArgb(37, 204, 255),
                    .LoadingCircleHotGradient = False,
                    .LoadingCircleHotGradientMode = Structures.Cursor.GradientMode.Circle,
                    .LoadingCircleHotNoise = False,
                    .LoadingCircleHotNoiseOpacity = 0.25,
                    .ArrowStyle = Structures.Cursor.ArrowStyles.Aero,
                    .CircleStyle = Structures.Cursor.CircleStyles.Aero,
                    .Shadow_Enabled = False,
                    .Shadow_Color = Color.Black,
                    .Shadow_Blur = 5,
                    .Shadow_Opacity = 0.3,
                    .Shadow_OffsetX = 2, .Shadow_OffsetY = 2}

    Public Cursor_Pin As New Structures.Cursor With {
                    .PrimaryColor1 = Color.White,
                    .PrimaryColor2 = Color.White,
                    .PrimaryColorGradient = False,
                    .PrimaryColorGradientMode = Structures.Cursor.GradientMode.Vertical,
                    .PrimaryColorNoise = False,
                    .PrimaryColorNoiseOpacity = 0.25,
                    .SecondaryColor1 = Color.Black,
                    .SecondaryColor2 = Color.FromArgb(64, 65, 75),
                    .SecondaryColorGradient = True,
                    .SecondaryColorGradientMode = Structures.Cursor.GradientMode.Vertical,
                    .SecondaryColorNoise = False,
                    .SecondaryColorNoiseOpacity = 0.25,
                    .LoadingCircleBack1 = Color.FromArgb(42, 151, 243),
                    .LoadingCircleBack2 = Color.FromArgb(42, 151, 243),
                    .LoadingCircleBackGradient = False,
                    .LoadingCircleBackGradientMode = Structures.Cursor.GradientMode.Circle,
                    .LoadingCircleBackNoise = False,
                    .LoadingCircleBackNoiseOpacity = 0.25,
                    .LoadingCircleHot1 = Color.FromArgb(37, 204, 255),
                    .LoadingCircleHot2 = Color.FromArgb(37, 204, 255),
                    .LoadingCircleHotGradient = False,
                    .LoadingCircleHotGradientMode = Structures.Cursor.GradientMode.Circle,
                    .LoadingCircleHotNoise = False,
                    .LoadingCircleHotNoiseOpacity = 0.25,
                    .ArrowStyle = Structures.Cursor.ArrowStyles.Aero,
                    .CircleStyle = Structures.Cursor.CircleStyles.Aero,
                    .Shadow_Enabled = False,
                    .Shadow_Color = Color.Black,
                    .Shadow_Blur = 5,
                    .Shadow_Opacity = 0.3,
                    .Shadow_OffsetX = 2, .Shadow_OffsetY = 2}

    Public Cursor_Person As New Structures.Cursor With {
                    .PrimaryColor1 = Color.White,
                    .PrimaryColor2 = Color.White,
                    .PrimaryColorGradient = False,
                    .PrimaryColorGradientMode = Structures.Cursor.GradientMode.Vertical,
                    .PrimaryColorNoise = False,
                    .PrimaryColorNoiseOpacity = 0.25,
                    .SecondaryColor1 = Color.Black,
                    .SecondaryColor2 = Color.FromArgb(64, 65, 75),
                    .SecondaryColorGradient = True,
                    .SecondaryColorGradientMode = Structures.Cursor.GradientMode.Vertical,
                    .SecondaryColorNoise = False,
                    .SecondaryColorNoiseOpacity = 0.25,
                    .LoadingCircleBack1 = Color.FromArgb(42, 151, 243),
                    .LoadingCircleBack2 = Color.FromArgb(42, 151, 243),
                    .LoadingCircleBackGradient = False,
                    .LoadingCircleBackGradientMode = Structures.Cursor.GradientMode.Circle,
                    .LoadingCircleBackNoise = False,
                    .LoadingCircleBackNoiseOpacity = 0.25,
                    .LoadingCircleHot1 = Color.FromArgb(37, 204, 255),
                    .LoadingCircleHot2 = Color.FromArgb(37, 204, 255),
                    .LoadingCircleHotGradient = False,
                    .LoadingCircleHotGradientMode = Structures.Cursor.GradientMode.Circle,
                    .LoadingCircleHotNoise = False,
                    .LoadingCircleHotNoiseOpacity = 0.25,
                    .ArrowStyle = Structures.Cursor.ArrowStyles.Aero,
                    .CircleStyle = Structures.Cursor.CircleStyles.Aero,
                    .Shadow_Enabled = False,
                    .Shadow_Color = Color.Black,
                    .Shadow_Blur = 5,
                    .Shadow_Opacity = 0.3,
                    .Shadow_OffsetX = 2, .Shadow_OffsetY = 2}

    Public Cursor_IBeam As New Structures.Cursor With {
                    .PrimaryColor1 = Color.White,
                    .PrimaryColor2 = Color.White,
                    .PrimaryColorGradient = False,
                    .PrimaryColorGradientMode = Structures.Cursor.GradientMode.Vertical,
                    .PrimaryColorNoise = False,
                    .PrimaryColorNoiseOpacity = 0.25,
                    .SecondaryColor1 = Color.Black,
                    .SecondaryColor2 = Color.FromArgb(64, 65, 75),
                    .SecondaryColorGradient = True,
                    .SecondaryColorGradientMode = Structures.Cursor.GradientMode.Vertical,
                    .SecondaryColorNoise = False,
                    .SecondaryColorNoiseOpacity = 0.25,
                    .LoadingCircleBack1 = Color.FromArgb(42, 151, 243),
                    .LoadingCircleBack2 = Color.FromArgb(42, 151, 243),
                    .LoadingCircleBackGradient = False,
                    .LoadingCircleBackGradientMode = Structures.Cursor.GradientMode.Circle,
                    .LoadingCircleBackNoise = False,
                    .LoadingCircleBackNoiseOpacity = 0.25,
                    .LoadingCircleHot1 = Color.FromArgb(37, 204, 255),
                    .LoadingCircleHot2 = Color.FromArgb(37, 204, 255),
                    .LoadingCircleHotGradient = False,
                    .LoadingCircleHotGradientMode = Structures.Cursor.GradientMode.Circle,
                    .LoadingCircleHotNoise = False,
                    .LoadingCircleHotNoiseOpacity = 0.25,
                    .ArrowStyle = Structures.Cursor.ArrowStyles.Aero,
                    .CircleStyle = Structures.Cursor.CircleStyles.Aero,
                    .Shadow_Enabled = False,
                    .Shadow_Color = Color.Black,
                    .Shadow_Blur = 5,
                    .Shadow_Opacity = 0.3,
                    .Shadow_OffsetX = 2, .Shadow_OffsetY = 2}

    Public Cursor_Cross As New Structures.Cursor With {
                    .PrimaryColor1 = Color.White,
                    .PrimaryColor2 = Color.White,
                    .PrimaryColorGradient = False,
                    .PrimaryColorGradientMode = Structures.Cursor.GradientMode.Vertical,
                    .PrimaryColorNoise = False,
                    .PrimaryColorNoiseOpacity = 0.25,
                    .SecondaryColor1 = Color.Black,
                    .SecondaryColor2 = Color.FromArgb(64, 65, 75),
                    .SecondaryColorGradient = True,
                    .SecondaryColorGradientMode = Structures.Cursor.GradientMode.Vertical,
                    .SecondaryColorNoise = False,
                    .SecondaryColorNoiseOpacity = 0.25,
                    .LoadingCircleBack1 = Color.FromArgb(42, 151, 243),
                    .LoadingCircleBack2 = Color.FromArgb(42, 151, 243),
                    .LoadingCircleBackGradient = False,
                    .LoadingCircleBackGradientMode = Structures.Cursor.GradientMode.Circle,
                    .LoadingCircleBackNoise = False,
                    .LoadingCircleBackNoiseOpacity = 0.25,
                    .LoadingCircleHot1 = Color.FromArgb(37, 204, 255),
                    .LoadingCircleHot2 = Color.FromArgb(37, 204, 255),
                    .LoadingCircleHotGradient = False,
                    .LoadingCircleHotGradientMode = Structures.Cursor.GradientMode.Circle,
                    .LoadingCircleHotNoise = False,
                    .LoadingCircleHotNoiseOpacity = 0.25,
                    .ArrowStyle = Structures.Cursor.ArrowStyles.Aero,
                    .CircleStyle = Structures.Cursor.CircleStyles.Aero,
                    .Shadow_Enabled = False,
                    .Shadow_Color = Color.Black,
                    .Shadow_Blur = 5,
                    .Shadow_Opacity = 0.3,
                    .Shadow_OffsetX = 2, .Shadow_OffsetY = 2}
#End Region

#End Region

#Region "CP Handling (Loading/Applying)"
    Sub New(File As String)
        Dim txt As String = String.Join(vbCrLf, Decompress(File))
        Dim JSON As Boolean = IsValidJson(txt)
        Dim WPTH As Boolean = txt.StartsWith("<WinPaletter - ", 5)

        If JSON Then
            LoadFromJSON(txt)
            Converter.Format = WP_Format.JSON

        ElseIf WPTH Then
            LoadFromOldWPTHFile(File)
            Converter.Format = WP_Format.WPTH

        Else
            Converter.Format = WP_Format.Error

        End If
    End Sub

    Enum WP_Format
        JSON
        WPTH
        [Error]
    End Enum

    Sub Save(Format As WP_Format, File As String, Compress As Boolean, OldWPTH1069 As Boolean)

        If Format = WP_Format.WPTH Then
            If IO.File.Exists(File) Then
                Try : Kill(File) : Catch : End Try
            End If

            IO.File.WriteAllText(File, Me.ToString(OldWPTH1069))

        ElseIf Format = WP_Format.JSON Then

            If Compress Then
                IO.File.WriteAllText(File, ToJSON.Compress)
            Else
                IO.File.WriteAllText(File, ToJSON)
            End If

        End If

    End Sub

    Sub LoadFromOldWPTHFile(File As String)
        Dim txt As New List(Of String) : txt.Clear()
        txt = Decompress(File)

        '## Checks if the loaded file is an old WPTH or not
        Dim OldWPTH As Boolean = False
        For Each line As String In txt
            If line.StartsWith("*Created from App Version= ", 5) Then
                Info.AppVersion = line.Remove(0, "*Created from App Version= ".Count)
                OldWPTH = (Info.AppVersion < "1.0.6.9")
                Exit For
            End If
        Next

        For Each lin As String In txt
            If lin.StartsWith("*Palette Name= ", 5) Then Info.ThemeName = lin.Remove(0, "*Palette Name= ".Count)
            If lin.StartsWith("*Palette Description= ", 5) Then Info.Description = lin.Remove(0, "*Palette Description= ".Count).Replace("<br>", vbCrLf)
            If lin.StartsWith("*Palette File Version= ", 5) Then Info.ThemeVersion = lin.Remove(0, "*Palette File Version= ".Count)
            If lin.StartsWith("*Author= ", 5) Then Info.Author = lin.Remove(0, "*Author= ".Count)
            If lin.StartsWith("*AuthorSocialMediaLink= ", 5) Then Info.AuthorSocialMediaLink = lin.Remove(0, "*AuthorSocialMediaLink= ".Count)
        Next

        Windows11.FromListOfString(txt.Where(Function(l) l.StartsWith("*Win_11", 5)))
        Windows10.FromListOfString(txt.Where(Function(l) l.StartsWith("*Win_10", 5)))

#Region "Windows 10x - Legacy WinPaletter - Before Vesion 1.0.6.9"
        If OldWPTH Then
            For Each line As String In txt
                Try
                    If line.StartsWith("*WinMode_Light= ", 5) Then
                        Windows11.WinMode_Light = line.Remove(0, "*WinMode_Light= ".Count)
                        Windows10.WinMode_Light = Windows11.WinMode_Light
                    End If

                    If line.StartsWith("*AppMode_Light= ", 5) Then
                        Windows11.AppMode_Light = line.Remove(0, "*AppMode_Light= ".Count)
                        Windows10.AppMode_Light = Windows11.AppMode_Light
                    End If


                    If line.StartsWith("*Transparency= ", 5) Then
                        Windows11.Transparency = line.Remove(0, "*Transparency= ".Count)
                        Windows10.Transparency = Windows11.Transparency
                    End If

                    If line.StartsWith("*AccentColorOnTitlebarAndBorders= ", 5) Then
                        Windows11.ApplyAccentOnTitlebars = line.Remove(0, "*AccentColorOnTitlebarAndBorders= ".Count)
                        Windows10.ApplyAccentOnTitlebars = Windows11.ApplyAccentOnTitlebars
                    End If

                    If line.StartsWith("*Titlebar_Active= ", 5) Then
                        Windows11.Titlebar_Active = Color.FromArgb(line.Remove(0, "*Titlebar_Active= ".Count))
                        Windows10.Titlebar_Active = Windows11.Titlebar_Active
                    End If

                    If line.StartsWith("*Titlebar_Inactive= ", 5) Then
                        Windows11.Titlebar_Inactive = Color.FromArgb(line.Remove(0, "*Titlebar_Inactive= ".Count))
                        Windows10.Titlebar_Inactive = Windows11.Titlebar_Inactive
                    End If

                    If line.StartsWith("*ActionCenter_AppsLinks= ", 5) Then
                        Windows11.Color_Index0 = Color.FromArgb(line.Remove(0, "*ActionCenter_AppsLinks= ".Count))
                        Windows10.Color_Index0 = Windows11.Color_Index0
                    End If

                    If line.StartsWith("*Taskbar_Icon_Underline= ", 5) Then
                        Windows11.Color_Index1 = Color.FromArgb(line.Remove(0, "*Taskbar_Icon_Underline= ".Count))
                        Windows10.Color_Index1 = Windows11.Color_Index1
                    End If

                    If line.StartsWith("*StartButton_Hover= ", 5) Then
                        Windows11.Color_Index2 = Color.FromArgb(line.Remove(0, "*StartButton_Hover= ".Count))
                        Windows10.Color_Index2 = Windows11.Color_Index2
                    End If

                    If line.StartsWith("*SettingsIconsAndLinks= ", 5) Then
                        Windows11.Color_Index3 = Color.FromArgb(line.Remove(0, "*SettingsIconsAndLinks= ".Count))
                        Windows10.Color_Index3 = Windows11.Color_Index3
                    End If

                    If line.StartsWith("*StartMenuBackground_ActiveTaskbarButton= ", 5) Then
                        Windows11.Color_Index4 = Color.FromArgb(line.Remove(0, "*StartMenuBackground_ActiveTaskbarButton= ".Count))
                        Windows10.Color_Index4 = Windows11.Color_Index4
                    End If

                    If line.StartsWith("*StartListFolders_TaskbarFront= ", 5) Then
                        Windows11.Color_Index5 = Color.FromArgb(line.Remove(0, "*StartListFolders_TaskbarFront= ".Count))
                        Windows10.Color_Index5 = Windows11.Color_Index5
                    End If

                    If line.StartsWith("*Taskbar_Background= ", 5) Then
                        Windows11.Color_Index6 = Color.FromArgb(line.Remove(0, "*Taskbar_Background= ".Count))
                        Windows10.Color_Index6 = Windows11.Color_Index6
                    End If

                    If line.StartsWith("*StartMenu_Accent= ", 5) Then
                        Windows11.StartMenu_Accent = Color.FromArgb(line.Remove(0, "*StartMenu_Accent= ".Count))
                        Windows10.StartMenu_Accent = Windows11.StartMenu_Accent
                    End If

                    If line.StartsWith("*Undefined= ", 5) Then
                        Windows11.Color_Index7 = Color.FromArgb(line.Remove(0, "*Undefined= ".Count))
                        Windows10.Color_Index7 = Windows11.Color_Index7
                    End If

                    If line.StartsWith("*AccentColorOnStartTaskbarAndActionCenter= ", 5) Then
                        Select Case line.Remove(0, "*AccentColorOnStartTaskbarAndActionCenter= ".Count).ToLower
                            Case "false"
                                Windows11.ApplyAccentOnTaskbar = Structures.Windows10x.AccentTaskbarLevels.None

                            Case "true"
                                Windows11.ApplyAccentOnTaskbar = Structures.Windows10x.AccentTaskbarLevels.Taskbar_Start_AC

                            Case Else
                                Select Case line.Remove(0, "*AccentColorOnStartTaskbarAndActionCenter= ".Count)
                                    Case 0
                                        Windows11.ApplyAccentOnTaskbar = Structures.Windows10x.AccentTaskbarLevels.None

                                    Case 1
                                        Windows11.ApplyAccentOnTaskbar = Structures.Windows10x.AccentTaskbarLevels.Taskbar_Start_AC

                                    Case 2
                                        Windows11.ApplyAccentOnTaskbar = Structures.Windows10x.AccentTaskbarLevels.Taskbar

                                    Case Else
                                        Windows11.ApplyAccentOnTaskbar = Structures.Windows10x.AccentTaskbarLevels.None
                                End Select
                        End Select

                        Windows10.ApplyAccentOnTaskbar = Windows11.ApplyAccentOnTaskbar
                    End If
                Catch
                    MsgBox("Error during loading from old wpth format (<1.0.6.9)", MsgBoxStyle.Critical)
                End Try
            Next
        End If
#End Region

        Windows8.FromListOfString(txt.Where(Function(l) l.StartsWith("*Metro", 5)))
        Windows7.FromListOfString(txt.Where(Function(l) l.StartsWith("*Aero", 5)))
        WindowsVista.FromListOfString(txt.Where(Function(l) l.StartsWith("*Vista", 5)))
        WindowsXP.FromListOfString(txt.Where(Function(l) l.StartsWith("*WinXP", 5)))
        LogonUI10x.FromListOfString(txt.Where(Function(l) l.StartsWith("*LogonUI_", 5)))
        LogonUI7.FromListOfString(txt.Where(Function(l) l.StartsWith("*LogonUI7_", 5)))
        LogonUIXP.FromListOfString(txt.Where(Function(l) l.StartsWith("*LogonUIXP_", 5)))
        Win32.FromListOfString(txt.Where(Function(l) l.StartsWith("*Win32UI", 5)))
        WindowsEffects.FromListOfString(txt.Where(Function(l) l.StartsWith("*WinEffects", 5)))

        MetricsFonts.FromListOfString(txt.Where(Function(l)
                                                    Return l.StartsWith("*Metrics", 5) _
                                                            OrElse l.StartsWith("*Fonts_", 5) _
                                                            OrElse l.StartsWith("*FontSubstitute_", 5)
                                                End Function))

        AltTab.FromListOfString(txt.Where(Function(l) l.StartsWith("*AltTab", 5)))

        Try : CommandPrompt.Enabled = txt.Where(Function(l) l.StartsWith("*Terminal_CMD_Enabled", 5))(0).Remove(0, "*Terminal_CMD_Enabled= ".Count) : Catch : End Try
        Try : PowerShellx86.Enabled = txt.Where(Function(l) l.StartsWith("*Terminal_PS_32_Enabled", 5))(0).Remove(0, "*Terminal_PS_32_Enabled= ".Count) : Catch : End Try
        Try : PowerShellx64.Enabled = txt.Where(Function(l) l.StartsWith("*Terminal_PS_64_Enabled", 5))(0).Remove(0, "*Terminal_PS_64_Enabled= ".Count) : Catch : End Try

        CommandPrompt.FromListOfString(txt.Where(Function(l) l.StartsWith("*CMD_", 5)))
        PowerShellx86.FromListOfString(txt.Where(Function(l) l.StartsWith("*PS_32_", 5)))
        PowerShellx64.FromListOfString(txt.Where(Function(l) l.StartsWith("*PS_64_", 5)))

        Dim str_stable, str_preview As String
        str_stable = String.Join(vbCrLf, txt.Where(Function(l) l.StartsWith("terminal.", 5)))
        str_preview = String.Join(vbCrLf, txt.Where(Function(l) l.StartsWith("terminalpreview.", 5)))
        Terminal = New WinTerminal_Converter(str_stable, WinTerminal_Converter.Mode.WinPaletterFile, WinTerminal_Converter.Version.Stable)
        TerminalPreview = New WinTerminal_Converter(str_preview, WinTerminal_Converter.Mode.WinPaletterFile, WinTerminal_Converter.Version.Preview)

        WallpaperTone_W11.FromListOfString(txt.Where(Function(l) l.StartsWith("*WallpaperTone_Win11_", 5)))
        WallpaperTone_W10.FromListOfString(txt.Where(Function(l) l.StartsWith("*WallpaperTone_Win10_", 5)))
        WallpaperTone_W8.FromListOfString(txt.Where(Function(l) l.StartsWith("*WallpaperTone_Win8.1_", 5)))
        WallpaperTone_W7.FromListOfString(txt.Where(Function(l) l.StartsWith("*WallpaperTone_Win7_", 5)))
        WallpaperTone_WVista.FromListOfString(txt.Where(Function(l) l.StartsWith("*WallpaperTone_WinVista_", 5)))
        WallpaperTone_WXP.FromListOfString(txt.Where(Function(l) l.StartsWith("*WallpaperTone_WinXP_", 5)))

        Try : Cursor_Enabled = txt.Where(Function(l) l.StartsWith("*Cursor_Enabled", 5))(0).Remove(0, "*Cursor_Enabled= ".Count) : Catch : End Try
        Try : Cursor_Shadow = txt.Where(Function(l) l.StartsWith("*Cursor_Shadow", 5))(0).Remove(0, "*Cursor_Shadow= ".Count) : Catch : End Try
        Try : Cursor_Trails = txt.Where(Function(l) l.StartsWith("*Cursor_Trails", 5))(0).Remove(0, "*Cursor_Trails= ".Count) : Catch : End Try
        Try : Cursor_Sonar = txt.Where(Function(l) l.StartsWith("*Cursor_Sonar", 5))(0).Remove(0, "*Cursor_Sonar= ".Count) : Catch : End Try

        Cursor_Arrow.FromListOfString(txt.Where(Function(l) l.StartsWith("*Cursor_Arrow_", 5)))
        Cursor_Help.FromListOfString(txt.Where(Function(l) l.StartsWith("*Cursor_Help_", 5)))
        Cursor_AppLoading.FromListOfString(txt.Where(Function(l) l.StartsWith("*Cursor_AppLoading_", 5)))
        Cursor_Busy.FromListOfString(txt.Where(Function(l) l.StartsWith("*Cursor_Busy_", 5)))
        Cursor_Move.FromListOfString(txt.Where(Function(l) l.StartsWith("*Cursor_Move_", 5)))
        Cursor_NS.FromListOfString(txt.Where(Function(l) l.StartsWith("*Cursor_NS_", 5)))
        Cursor_EW.FromListOfString(txt.Where(Function(l) l.StartsWith("*Cursor_EW_", 5)))
        Cursor_NESW.FromListOfString(txt.Where(Function(l) l.StartsWith("*Cursor_NESW_", 5)))
        Cursor_NWSE.FromListOfString(txt.Where(Function(l) l.StartsWith("*Cursor_NWSE_", 5)))
        Cursor_Up.FromListOfString(txt.Where(Function(l) l.StartsWith("*Cursor_Up_", 5)))
        Cursor_Pen.FromListOfString(txt.Where(Function(l) l.StartsWith("*Cursor_Pen_", 5)))
        Cursor_None.FromListOfString(txt.Where(Function(l) l.StartsWith("*Cursor_None_", 5)))
        Cursor_Link.FromListOfString(txt.Where(Function(l) l.StartsWith("*Cursor_Link_", 5)))
        Cursor_Pin.FromListOfString(txt.Where(Function(l) l.StartsWith("*Cursor_Pin_", 5)))
        Cursor_Person.FromListOfString(txt.Where(Function(l) l.StartsWith("*Cursor_Person_", 5)))
        Cursor_IBeam.FromListOfString(txt.Where(Function(l) l.StartsWith("*Cursor_IBeam_", 5)))
        Cursor_Cross.FromListOfString(txt.Where(Function(l) l.StartsWith("*Cursor_Cross_", 5)))
    End Sub

    Overloads Function ToString(OldWPTH1069 As Boolean) As String
        Dim tx As New List(Of String)
        tx.Clear()
        tx.Add("<WinPaletter - Programmed by Abdelrhman-AK>")
        tx.Add("*Created from App Version= " & Info.AppVersion)
        tx.Add("*Last Modified by App Version= " & My.Application.Info.Version.ToString & vbCrLf)

        tx.Add(Info.ToString)

#Region "Windows 10x - Legacy WinPaletter - Before Vesion 1.0.6.9"
        If OldWPTH1069 Then
            Try
                With If(My.W11, Windows11, Windows10)
                    tx.Add("<LegacyWinPaletter_Windows11/10>")
                    tx.Add("*WinMode_Light= " & .WinMode_Light)
                    tx.Add("*AppMode_Light= " & .AppMode_Light)
                    tx.Add("*Transparency= " & .Transparency)
                    tx.Add("*AccentColorOnTitlebarAndBorders= " & .ApplyAccentOnTitlebars)
                    tx.Add("*AccentColorOnStartTaskbarAndActionCenter= " & .ApplyAccentOnTaskbar)
                    tx.Add("*Titlebar_Active= " & .Titlebar_Active.ToArgb)
                    tx.Add("*Titlebar_Inactive= " & .Titlebar_Inactive.ToArgb)
                    tx.Add("*ActionCenter_AppsLinks= " & .Color_Index0.ToArgb)
                    tx.Add("*Taskbar_Icon_Underline= " & .Color_Index1.ToArgb)
                    tx.Add("*StartButton_Hover= " & .Color_Index2.ToArgb)
                    tx.Add("*SettingsIconsAndLinks= " & .Color_Index3.ToArgb)
                    tx.Add("StartMenuBackground_ActiveTaskbarButton= " & .Color_Index4.ToArgb)
                    tx.Add("*StartListFolders_TaskbarFront= " & .Color_Index5.ToArgb)
                    tx.Add("*Taskbar_Background= " & .Color_Index6.ToArgb)
                    tx.Add("*StartMenu_Accent= " & .StartMenu_Accent.ToArgb)
                    tx.Add("*Undefined= " & .Color_Index7.ToArgb)
                    tx.Add("</LegacyWinPaletter_Windows11/10>" & vbCrLf)
                End With
            Catch
            End Try
        End If
#End Region

        tx.Add(Windows11.ToString("Windows11", "Win_11"))
        tx.Add(Windows10.ToString("Windows10", "Win_10"))
        tx.Add(LogonUI10x.ToString)

        tx.Add(Windows8.ToString)
        tx.Add(Windows7.ToString)
        tx.Add(LogonUI7.ToString)

        tx.Add(WindowsVista.ToString)
        tx.Add(WindowsXP.ToString)
        tx.Add(LogonUIXP.ToString)

        tx.Add(Win32.ToString)
        tx.Add(WindowsEffects.ToString)
        tx.Add(MetricsFonts.ToString)
        tx.Add(AltTab.ToString)

        tx.Add(WallpaperTone_W11.ToString("Win11"))
        tx.Add(WallpaperTone_W10.ToString("Win10"))
        tx.Add(WallpaperTone_W8.ToString("Win8.1"))
        tx.Add(WallpaperTone_W7.ToString("Win7"))
        tx.Add(WallpaperTone_WVista.ToString("WinVista"))
        tx.Add(WallpaperTone_WXP.ToString("WinXP"))

        tx.Add("<Terminals>")
        tx.Add(CommandPrompt.ToString("CMD"))
        tx.Add(PowerShellx86.ToString("PS_32"))
        tx.Add(PowerShellx64.ToString("PS_64"))
        Try : If Terminal IsNot Nothing Then tx.Add(Terminal.ToString("WindowsTerminal_Stable", WinTerminal.Version.Stable))
        Catch : End Try
        Try : If TerminalPreview IsNot Nothing Then tx.Add(TerminalPreview.ToString("WindowsTerminal_Preview", WinTerminal.Version.Preview))
        Catch : End Try
        tx.Add("</Terminals>" & vbCrLf)

        tx.Add("<Cursors>")
        tx.Add("*Cursor_Enabled= " & Cursor_Enabled)
        tx.Add("*Cursor_Shadow= " & Cursor_Shadow)
        tx.Add("*Cursor_Sonar= " & Cursor_Sonar)
        tx.Add("*Cursor_Trails= " & Cursor_Trails)
        tx.Add(Cursor_Arrow.ToString("Arrow"))
        tx.Add(Cursor_Help.ToString("Help"))
        tx.Add(Cursor_AppLoading.ToString("AppLoading"))
        tx.Add(Cursor_Busy.ToString("Busy"))
        tx.Add(Cursor_Move.ToString("Move"))
        tx.Add(Cursor_NS.ToString("NS"))
        tx.Add(Cursor_EW.ToString("EW"))
        tx.Add(Cursor_NESW.ToString("NESW"))
        tx.Add(Cursor_NWSE.ToString("NWSE"))
        tx.Add(Cursor_Up.ToString("Up"))
        tx.Add(Cursor_Pen.ToString("Pen"))
        tx.Add(Cursor_None.ToString("None"))
        tx.Add(Cursor_Link.ToString("Link"))
        tx.Add(Cursor_Pin.ToString("Pin"))
        tx.Add(Cursor_Person.ToString("Person"))
        tx.Add(Cursor_IBeam.ToString("IBeam"))
        tx.Add(Cursor_Cross.ToString("Cross"))
        tx.Add("</Cursors>" & vbCrLf)

        tx.Add("</WinPaletter>")

        Return tx.CString
    End Function

    Function Decompress(File As String) As IEnumerable(Of String)
        Dim DecompressedData As IEnumerable(Of String)

        Try
            DecompressedData = IO.File.ReadAllText(File).Decompress.CList
        Catch
            DecompressedData = IO.File.ReadAllText(File).CList
        End Try

        Return DecompressedData
    End Function

    Private Function DeserializeProps([StructureType] As Type, [Structure] As Object) As JObject
        Dim j As New JObject()

        j.RemoveAll()

        For Each field In [StructureType].GetFields(BindingFlags.Instance Or BindingFlags.NonPublic Or BindingFlags.Public)
            Dim result As JToken

            Try
                result = JToken.FromObject(field.GetValue([Structure]))
            Catch
                result = Nothing
            End Try

            j.Add(field.Name, result)
        Next

        Return j
    End Function

    Public Function ToJSON() As String
        Dim JSON_Overall As New JObject()

        JSON_Overall.RemoveAll()

        For Each field As FieldInfo In Me.GetType.GetFields(bindingFlags)
            Dim type As Type = field.FieldType

            If IsStructure(type) Then
                JSON_Overall.Add(field.Name, DeserializeProps(type, field.GetValue(Me)))

            Else
                JSON_Overall.Add(field.Name, JToken.FromObject(field.GetValue(Me)))

            End If

        Next

        Return JSON_Overall.ToString
    End Function

    Public Function IsStructure(ByVal type As Type) As Boolean
        Return type.IsValueType AndAlso Not type.IsPrimitive AndAlso type.Namespace IsNot Nothing AndAlso Not type.Namespace.StartsWith("System.")
    End Function

    Public Sub LoadFromJSON(JSON_Text As String)
        Dim J As JObject = JObject.Parse(JSON_Text)

        For Each field As FieldInfo In Me.GetType.GetFields(bindingFlags)
            Dim type As Type = field.FieldType

            If J(field.Name) IsNot Nothing Then
                field.SetValue(Me, J(field.Name).ToObject(type))
            End If
        Next
    End Sub

    Private Shared Function IsValidJson(ByVal strInput As String) As Boolean
        If String.IsNullOrWhiteSpace(strInput) Then
            Return False
        End If
        strInput = strInput.Trim()
        If strInput.StartsWith("{") AndAlso strInput.EndsWith("}") OrElse strInput.StartsWith("[") AndAlso strInput.EndsWith("]") Then 'For object
            'For array
            Try
                Dim obj = JToken.Parse(strInput)
                Return True
            Catch jex As JsonReaderException
                'Exception in parsing json
                Return False
            Catch ex As Exception 'some other exception
                Return False
            End Try
        Else
            Return False
        End If
    End Function
#End Region

End Class
