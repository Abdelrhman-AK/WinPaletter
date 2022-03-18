Imports System.Globalization
Imports Microsoft.Win32
Imports WinPaletter.XenonCore

Public Class CP
    Public Titlebar_Active As Color
    Public Titlebar_DWM_Active As Color
    Public Titlebar_Inactive As Color
    Public StartMenu_Accent As Color
    Public StartButton_Hover As Color
    Public Taskbar_Background As Color
    Public Taskbar_Icon_Underline As Color
    Public StartMenuBackground_ActiveTaskbarButton As Color
    Public StartListFolders_TaskbarFront As Color
    Public ActionCenter_AppsLinks As Color
    Public SettingsIconsAndLinks As Color

    Public WinMode_Light As Boolean
    Public AppMode_Light As Boolean
    Public Transparency As Boolean
    Public ApplyAccentonTitlebars As Boolean
    Public ApplyAccentonTaskbar As Boolean

    Public AppVersion As String
    Public PaletteName As String
    Public PaletteDescription As String
    Public PaletteVersion As String
    Public Author As String
    Public AuthorSocialMediaLink As String

    Enum Mode
        Registry
        Init
        File
    End Enum

    Sub New([Mode] As Mode, Optional ByVal PaletteFile As String = "")
        Select Case [Mode]
            Case Mode.Registry
                Dim Colors As New List(Of Color)
                Colors.Clear()

                Author = Environment.UserName
                AppVersion = My.Application.Info.Version.ToString
                PaletteVersion = "1.0"
                PaletteName = "Current Mode"

                Dim x As Byte() = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "AccentPalette", Nothing)
                Colors.Add(Color.FromArgb(255, x(0), x(1), x(2)))
                Colors.Add(Color.FromArgb(255, x(4), x(5), x(6)))
                Colors.Add(Color.FromArgb(255, x(8), x(9), x(10)))
                Colors.Add(Color.FromArgb(255, x(12), x(13), x(14)))
                Colors.Add(Color.FromArgb(255, x(16), x(17), x(18)))
                Colors.Add(Color.FromArgb(255, x(20), x(21), x(22)))
                Colors.Add(Color.FromArgb(255, x(24), x(25), x(26)))
                Colors.Add(Color.FromArgb(255, x(28), x(29), x(30)))

                ActionCenter_AppsLinks = Colors(0)
                Taskbar_Icon_Underline = Colors(1)
                StartButton_Hover = Colors(2)
                SettingsIconsAndLinks = Colors(3)
                StartMenuBackground_ActiveTaskbarButton = Colors(4)
                StartListFolders_TaskbarFront = Colors(5)
                Taskbar_Background = Colors(6)

                Dim y As Integer
                y = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "StartColorMenu", Nothing)
                StartMenu_Accent = BizareColorInvertor(Color.FromArgb(y))

                y = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "AccentColorMenu", Nothing)
                Titlebar_Active = BizareColorInvertor(Color.FromArgb(y))

                y = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\DWM", "AccentColor", Nothing)
                Titlebar_DWM_Active = BizareColorInvertor(Color.FromArgb(y))

                y = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\DWM", "AccentColorInactive", Nothing)
                Titlebar_Inactive = BizareColorInvertor(Color.FromArgb(y))

                WinMode_Light = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "SystemUsesLightTheme", True)
                AppMode_Light = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "AppsUseLightTheme", True)
                Transparency = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "EnableTransparency", True)
                ApplyAccentonTaskbar = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "ColorPrevalence", False)
                ApplyAccentonTitlebars = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\DWM", "ColorPrevalence", False)

            Case Mode.File
                Dim txt As New List(Of String)
                txt.Clear()
                CList_FromStr(txt, Protector.Decrypt(IO.File.ReadAllText(PaletteFile)))

                For Each lin As String In txt
                    If lin.Contains("*Created from App Version= ") Then AppVersion = lin.Remove(0, "*Created from App Version= ".Count)
                    If lin.Contains("*Palette Name= ") Then PaletteName = lin.Remove(0, "*Palette Name= ".Count)
                    If lin.Contains("*Palette Description= ") Then PaletteDescription = lin.Remove(0, "*Palette Description= ".Count).Replace("<br>", vbCrLf)
                    If lin.Contains("*Palette File Version= ") Then PaletteVersion = lin.Remove(0, "*Palette File Version= ".Count)
                    If lin.Contains("*Author= ") Then Author = lin.Remove(0, "*Author= ".Count)
                    If lin.Contains("*AuthorSocialMediaLink= ") Then AuthorSocialMediaLink = lin.Remove(0, "*AuthorSocialMediaLink= ".Count)
                    If lin.Contains("*Palette File Version= ") Then PaletteVersion = lin.Remove(0, "*Palette File Version= ".Count)
                    If lin.Contains("*WinMode_Light= ") Then WinMode_Light = lin.Remove(0, "*WinMode_Light= ".Count)
                    If lin.Contains("*AppMode_Light= ") Then AppMode_Light = lin.Remove(0, "*AppMode_Light= ".Count)
                    If lin.Contains("*Transparency= ") Then Transparency = lin.Remove(0, "*Transparency= ".Count)
                    If lin.Contains("*AccentColorOnTitlebarAndBorders= ") Then ApplyAccentonTitlebars = lin.Remove(0, "*AccentColorOnTitlebarAndBorders= ".Count)
                    If lin.Contains("*AccentColorOnStartTaskbarAndActionCenter= ") Then ApplyAccentonTaskbar = lin.Remove(0, "*AccentColorOnStartTaskbarAndActionCenter= ".Count)
                    If lin.Contains("*Titlebar_Active= ") Then
                        Titlebar_Active = Color.FromArgb(lin.Remove(0, "*Titlebar_Active= ".Count))
                        Titlebar_DWM_Active = Titlebar_Active
                    End If
                    If lin.Contains("*Titlebar_Inactive= ") Then Titlebar_Inactive = Color.FromArgb(lin.Remove(0, "*Titlebar_Inactive= ".Count))
                    If lin.Contains("*ActionCenter_AppsLinks= ") Then ActionCenter_AppsLinks = Color.FromArgb(lin.Remove(0, "*ActionCenter_AppsLinks= ".Count))
                    If lin.Contains("*Taskbar_Icon_Underline= ") Then Taskbar_Icon_Underline = Color.FromArgb(lin.Remove(0, "*Taskbar_Icon_Underline= ".Count))
                    If lin.Contains("*StartButton_Hover= ") Then StartButton_Hover = Color.FromArgb(lin.Remove(0, "*StartButton_Hover= ".Count))
                    If lin.Contains("*SettingsIconsAndLinks= ") Then SettingsIconsAndLinks = Color.FromArgb(lin.Remove(0, "*SettingsIconsAndLinks= ".Count))
                    If lin.Contains("*StartMenuBackground_ActiveTaskbarButton= ") Then StartMenuBackground_ActiveTaskbarButton = Color.FromArgb(lin.Remove(0, "*StartMenuBackground_ActiveTaskbarButton= ".Count))
                    If lin.Contains("*StartListFolders_TaskbarFront= ") Then StartListFolders_TaskbarFront = Color.FromArgb(lin.Remove(0, "*StartListFolders_TaskbarFront= ".Count))
                    If lin.Contains("*Taskbar_Background= ") Then Taskbar_Background = Color.FromArgb(lin.Remove(0, "*Taskbar_Background= ".Count))
                    If lin.Contains("*StartMenu_Accent= ") Then StartMenu_Accent = Color.FromArgb(lin.Remove(0, "*StartMenu_Accent= ".Count))
                Next

            Case Mode.Init
                Author = Environment.UserName
                AppVersion = My.Application.Info.Version.ToString
                PaletteVersion = "1.0"
                PaletteName = "Init"

                Titlebar_Active = New Color
                Titlebar_Inactive = New Color
                StartMenu_Accent = New Color
                StartButton_Hover = New Color
                Taskbar_Background = New Color
                Taskbar_Icon_Underline = New Color
                StartMenuBackground_ActiveTaskbarButton = New Color
                StartListFolders_TaskbarFront = New Color
                ActionCenter_AppsLinks = New Color
                WinMode_Light = False
                AppMode_Light = False
                Transparency = True
                ApplyAccentonTitlebars = False
                ApplyAccentonTaskbar = False
        End Select

    End Sub

    Sub Save(ByVal [SaveTo] As SavingMode, Optional ByVal FileLocation As String = "")
        Select Case [SaveTo]
            Case SavingMode.Registery

                Dim Colors As Byte() = {(ActionCenter_AppsLinks).R, (ActionCenter_AppsLinks).G, (ActionCenter_AppsLinks).B, (ActionCenter_AppsLinks).A _
                     , (Taskbar_Icon_Underline).R, (Taskbar_Icon_Underline).G, (Taskbar_Icon_Underline).B, (Taskbar_Icon_Underline).A _
                     , (StartButton_Hover).R, (StartButton_Hover).G, (StartButton_Hover).B, (StartButton_Hover).A _
                     , (SettingsIconsAndLinks).R, (SettingsIconsAndLinks).G, (SettingsIconsAndLinks).B, (SettingsIconsAndLinks).A _
                     , (StartMenuBackground_ActiveTaskbarButton).R, (StartMenuBackground_ActiveTaskbarButton).G, (StartMenuBackground_ActiveTaskbarButton).B, (StartMenuBackground_ActiveTaskbarButton).A _
                     , (StartListFolders_TaskbarFront).R, (StartListFolders_TaskbarFront).G, (StartListFolders_TaskbarFront).B, (StartListFolders_TaskbarFront).A _
                     , (Taskbar_Background).R, (Taskbar_Background).G, (Taskbar_Background).B, (Taskbar_Background).A _
                     , 255, 0, 0, 0}

                EditReg("SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "AccentPalette", Colors, True)

                EditReg("SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "StartColorMenu", BizareColorInvertor(StartMenu_Accent).ToArgb)
                EditReg("SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "AccentColorMenu", BizareColorInvertor(Titlebar_Active).ToArgb)
                EditReg("SOFTWARE\Microsoft\Windows\DWM", "AccentColor", BizareColorInvertor(Titlebar_DWM_Active).ToArgb)
                EditReg("SOFTWARE\Microsoft\Windows\DWM", "AccentColorInactive", BizareColorInvertor(Titlebar_Inactive).ToArgb)

                EditReg("SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "SystemUsesLightTheme", If(WinMode_Light, 1, 0))
                EditReg("SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "AppsUseLightTheme", If(AppMode_Light, 1, 0))
                EditReg("SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "EnableTransparency", If(Transparency, 1, 0))
                EditReg("SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "ColorPrevalence", If(ApplyAccentonTaskbar, 1, 0))
                EditReg("SOFTWARE\Microsoft\Windows\DWM", "ColorPrevalence", If(ApplyAccentonTitlebars, 1, 0))

            Case SavingMode.File

                Dim tx As New List(Of String)
                tx.Clear()
                tx.Add("<WinPaletter - Programmed by Abdelrhman_AK>")
                tx.Add("*Created from App Version= " & AppVersion & vbCrLf)

                tx.Add("<General>")
                tx.Add("*Palette Name= " & PaletteName)
                If String.IsNullOrWhiteSpace(PaletteDescription) Then
                    tx.Add("*Palette Description= ")
                Else
                    tx.Add("*Palette Description= " & PaletteDescription.Replace(vbCrLf, "<br>"))
                End If
                tx.Add("*Palette File Version= " & PaletteVersion)
                tx.Add("*Author= " & Author)
                tx.Add("*AuthorSocialMediaLink= " & AuthorSocialMediaLink)
                tx.Add("</General>" & vbCrLf)

                tx.Add("<Toggles>")
                tx.Add("*WinMode_Light= " & WinMode_Light)
                tx.Add("*AppMode_Light= " & AppMode_Light)
                tx.Add("*Transparency= " & Transparency)
                tx.Add("*AccentColorOnTitlebarAndBorders= " & ApplyAccentonTitlebars)
                tx.Add("*AccentColorOnStartTaskbarAndActionCenter= " & ApplyAccentonTaskbar)
                tx.Add("</Toggles>" & vbCrLf)

                tx.Add("<Colors>")
                tx.Add("*Titlebar_Active= " & Titlebar_Active.ToArgb)
                tx.Add("*Titlebar_Inactive= " & Titlebar_Inactive.ToArgb)
                tx.Add("*ActionCenter_AppsLinks= " & ActionCenter_AppsLinks.ToArgb)
                tx.Add("*Taskbar_Icon_Underline= " & Taskbar_Icon_Underline.ToArgb)
                tx.Add("*StartButton_Hover= " & StartButton_Hover.ToArgb)
                tx.Add("*SettingsIconsAndLinks= " & SettingsIconsAndLinks.ToArgb)
                tx.Add("*StartMenuBackground_ActiveTaskbarButton= " & StartMenuBackground_ActiveTaskbarButton.ToArgb)
                tx.Add("*StartListFolders_TaskbarFront= " & StartListFolders_TaskbarFront.ToArgb)
                tx.Add("*Taskbar_Background= " & Taskbar_Background.ToArgb)
                tx.Add("*StartMenu_Accent= " & StartMenu_Accent.ToArgb)
                tx.Add("*Undefined= " & Color.FromArgb(255, 0, 0, 0).ToArgb)
                tx.Add("</Colors>" & vbCrLf)

                tx.Add(vbCrLf & "</WinPaletter>")

                IO.File.WriteAllText(FileLocation, Protector.Encrypt(CStr_FromList(tx)))
        End Select

    End Sub

    Function HexStringToBinary(ByVal hexString As String) As String
        Dim num As Integer = Integer.Parse(hexString, NumberStyles.HexNumber)
        Return Convert.ToString(num, 2)
    End Function

    Sub EditReg(KeyName As String, ValueName As String, Value As Object, Optional ByVal Binary As Boolean = False)

        If Binary Then
            Registry.CurrentUser.OpenSubKey(KeyName, True).SetValue(ValueName, Value, RegistryValueKind.Binary)
        Else
            Registry.CurrentUser.OpenSubKey(KeyName, True).SetValue(ValueName, Value, RegistryValueKind.DWord)
        End If

    End Sub

    Enum SavingMode
        Registery
        File
    End Enum

    Function RGB2HEX(ByVal [Color] As Color) As List(Of String)
        Dim sb As New List(Of String)
        sb.Clear()

        sb.Add(String.Format("{0:X2}", Color.A, Color.R, Color.G, Color.B))
        sb.Add(String.Format("{1:X2}", Color.A, Color.R, Color.G, Color.B))
        sb.Add(String.Format("{2:X2}", Color.A, Color.R, Color.G, Color.B))
        sb.Add(String.Format("{3:X2}", Color.A, Color.R, Color.G, Color.B))

        Return sb
    End Function

    Function BizareColorInvertor([Color] As Color)
        Return Color.FromArgb([Color].B, [Color].G, [Color].R)
    End Function
End Class
