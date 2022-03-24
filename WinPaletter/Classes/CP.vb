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

#Region "LogonUI"
    Public LogonUI_Background As Color
    Public LogonUI_PersonalColors_Background As Color
    Public LogonUI_PersonalColors_Accent As Color
    Public LogonUI_DisableAcrylicBackgroundOnLogon As Boolean = False
    Public LogonUI_DisableLogonBackgroundImage As Boolean = False
    Public LogonUI_NoLockScreen As Boolean = False
#End Region

#Region "Win32UI"
    Public Win32UI_ActiveBorder As Color
    Public Win32UI_MenuHilight As Color
    Public Win32UI_HotTrackingColor As Color
    Public Win32UI_Hilight As Color
#End Region

    Public WinMode_Light As Boolean
    Public AppMode_Light As Boolean
    Public Transparency As Boolean
    Public Blur As Boolean
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
                Blur = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "EnabledBlurBehind", True)
                ApplyAccentonTaskbar = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "ColorPrevalence", False)
                ApplyAccentonTitlebars = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\DWM", "ColorPrevalence", False)

#Region "LogonUI"
                With My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon", "Background", "0 0 0")
                    LogonUI_Background = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                End With

                y = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Windows\Personalization", "PersonalColors_Background", Nothing)
                LogonUI_PersonalColors_Background = BizareColorInvertor(Color.FromArgb(y))

                y = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Windows\Personalization", "PersonalColors_Accent", Nothing)
                LogonUI_PersonalColors_Accent = BizareColorInvertor(Color.FromArgb(y))

                LogonUI_DisableAcrylicBackgroundOnLogon = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\System", "DisableAcrylicBackgroundOnLogon", False)
                LogonUI_DisableLogonBackgroundImage = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\System", "DisableLogonBackgroundImage", False)
                LogonUI_NoLockScreen = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Windows\Personalization", "NoLockScreen", False)
#End Region

#Region "Win32UI"
                With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "Hilight", "0 0 0")
                    Win32UI_Hilight = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                End With

                With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "HotTrackingColor", "0 0 0")
                    Win32UI_HotTrackingColor = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                End With

                With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "MenuHilight", "0 0 0")
                    Win32UI_MenuHilight = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                End With

                With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "ActiveBorder", "0 0 0")
                    Win32UI_ActiveBorder = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2))
                End With
#End Region


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
                    If lin.Contains("*Blur= ") Then Blur = lin.Remove(0, "*Blur= ".Count)
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

#Region "LogonUI"
                    If lin.Contains("*LogonUI_Background= ") Then LogonUI_Background = Color.FromArgb(lin.Remove(0, "*LogonUI_Background= ".Count))
                    If lin.Contains("*LogonUI_PersonalColors_Background= ") Then LogonUI_PersonalColors_Background = Color.FromArgb(lin.Remove(0, "*LogonUI_PersonalColors_Background= ".Count))
                    If lin.Contains("*LogonUI_PersonalColors_Accent= ") Then LogonUI_PersonalColors_Accent = Color.FromArgb(lin.Remove(0, "*LogonUI_PersonalColors_Accent= ".Count))
                    If lin.Contains("*LogonUI_DisableAcrylicBackgroundOnLogon= ") Then LogonUI_DisableAcrylicBackgroundOnLogon = lin.Remove(0, "*LogonUI_DisableAcrylicBackgroundOnLogon= ".Count)
                    If lin.Contains("*LogonUI_DisableLogonBackgroundImage= ") Then LogonUI_DisableLogonBackgroundImage = lin.Remove(0, "*LogonUI_DisableLogonBackgroundImage= ".Count)
                    If lin.Contains("*LogonUI_NoLockScreen= ") Then LogonUI_NoLockScreen = lin.Remove(0, "*LogonUI_NoLockScreen= ".Count)
#End Region

#Region "Win32UI"
                    If lin.Contains("*ActiveBorder= ") Then Win32UI_ActiveBorder = Color.FromArgb(lin.Remove(0, "*ActiveBorder= ".Count))
                    If lin.Contains("*Hilight= ") Then Win32UI_Hilight = Color.FromArgb(lin.Remove(0, "*Hilight= ".Count))
                    If lin.Contains("*HotTrackingColor= ") Then Win32UI_HotTrackingColor = Color.FromArgb(lin.Remove(0, "*HotTrackingColor= ".Count))
                    If lin.Contains("*MenuHilight= ") Then Win32UI_MenuHilight = Color.FromArgb(lin.Remove(0, "*MenuHilight= ".Count))
#End Region

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

                Win32UI_ActiveBorder = New Color
                Win32UI_MenuHilight = New Color
                Win32UI_HotTrackingColor = New Color
                Win32UI_Hilight = New Color

                LogonUI_Background = New Color
                LogonUI_PersonalColors_Background = New Color
                LogonUI_PersonalColors_Accent = New Color
                LogonUI_DisableAcrylicBackgroundOnLogon = False
                LogonUI_DisableLogonBackgroundImage = False
                LogonUI_NoLockScreen = False

                WinMode_Light = False
                AppMode_Light = False
                Transparency = True
                Blur = True
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

                EditReg("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "AccentPalette", Colors, True)

                EditReg("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "StartColorMenu", BizareColorInvertor(StartMenu_Accent).ToArgb)
                EditReg("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "AccentColorMenu", BizareColorInvertor(Titlebar_Active).ToArgb)
                EditReg("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\DWM", "AccentColor", BizareColorInvertor(Titlebar_DWM_Active).ToArgb)
                EditReg("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\DWM", "AccentColorInactive", BizareColorInvertor(Titlebar_Inactive).ToArgb)

                EditReg("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "SystemUsesLightTheme", If(WinMode_Light, 1, 0))
                EditReg("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "AppsUseLightTheme", If(AppMode_Light, 1, 0))
                EditReg("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "EnableTransparency", If(Transparency, 1, 0))
                EditReg("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "EnabledBlurBehind", If(Blur, 1, 0))
                EditReg("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "ColorPrevalence", If(ApplyAccentonTaskbar, 1, 0))
                EditReg("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\DWM", "ColorPrevalence", If(ApplyAccentonTitlebars, 1, 0))

#Region "LogonUI"
                EditReg("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon", "Background", String.Format("{0} {1} {2}", LogonUI_Background.R, LogonUI_Background.G, LogonUI_Background.B), False, True)
                EditReg("HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Windows\Personalization", "PersonalColors_Background", BizareColorInvertor(LogonUI_PersonalColors_Background).ToArgb)
                EditReg("HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Windows\Personalization", "PersonalColors_Accent", BizareColorInvertor(LogonUI_PersonalColors_Accent).ToArgb)
                EditReg("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\System", "DisableAcrylicBackgroundOnLogon", If(LogonUI_DisableAcrylicBackgroundOnLogon, 1, 0))
                EditReg("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\System", "DisableLogonBackgroundImage", If(LogonUI_DisableLogonBackgroundImage, 1, 0))
                EditReg("HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Windows\Personalization", "NoLockScreen", If(LogonUI_NoLockScreen, 1, 0))
#End Region

#Region "Win32UI"
                EditReg("HKEY_CURRENT_USER\Control Panel\Colors", "Hilight", String.Format("{0} {1} {2}", Win32UI_Hilight.R, Win32UI_Hilight.G, Win32UI_Hilight.B), False, True)
                EditReg("HKEY_CURRENT_USER\Control Panel\Colors", "HotTrackingColor", String.Format("{0} {1} {2}", Win32UI_HotTrackingColor.R, Win32UI_HotTrackingColor.G, Win32UI_HotTrackingColor.B), False, True)
                EditReg("HKEY_CURRENT_USER\Control Panel\Colors", "MenuHilight", String.Format("{0} {1} {2}", Win32UI_MenuHilight.R, Win32UI_MenuHilight.G, Win32UI_MenuHilight.B), False, True)
                EditReg("HKEY_CURRENT_USER\Control Panel\Colors", "ActiveBorder", String.Format("{0} {1} {2}", Win32UI_ActiveBorder.R, Win32UI_ActiveBorder.G, Win32UI_ActiveBorder.B), False, True)
#End Region

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
                tx.Add("*Blur= " & Blur)
                tx.Add("*AccentColorOnTitlebarAndBorders= " & ApplyAccentonTitlebars)
                tx.Add("*AccentColorOnStartTaskbarAndActionCenter= " & ApplyAccentonTaskbar)
                tx.Add("</Toggles>" & vbCrLf)

                tx.Add("<UWP>")
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
                tx.Add("</UWP>" & vbCrLf)

#Region "LogonUI"
                tx.Add("<LogonUI>")
                tx.Add("*LogonUI_Background= " & LogonUI_Background.ToArgb)
                tx.Add("*LogonUI_PersonalColors_Background= " & LogonUI_PersonalColors_Background.ToArgb)
                tx.Add("*LogonUI_PersonalColors_Accent= " & LogonUI_PersonalColors_Accent.ToArgb)
                tx.Add("*LogonUI_DisableAcrylicBackgroundOnLogon= " & LogonUI_DisableAcrylicBackgroundOnLogon)
                tx.Add("*LogonUI_DisableLogonBackgroundImage= " & LogonUI_DisableLogonBackgroundImage)
                tx.Add("*LogonUI_NoLockScreen= " & LogonUI_NoLockScreen)
                tx.Add("</LogonUI>" & vbCrLf)
#End Region

#Region "Win32UI"
                tx.Add("<Win32UI>")
                tx.Add("*Hilight= " & Win32UI_Hilight.ToArgb)
                tx.Add("*HotTrackingColor= " & Win32UI_HotTrackingColor.ToArgb)
                tx.Add("*MenuHilight= " & Win32UI_MenuHilight.ToArgb)
                tx.Add("*ActiveBorder= " & Win32UI_ActiveBorder.ToArgb)
                tx.Add("</Win32UI>" & vbCrLf)
#End Region
                tx.Add(vbCrLf & "</WinPaletter>")

                IO.File.WriteAllText(FileLocation, Protector.Encrypt(CStr_FromList(tx)))
        End Select

    End Sub

    Function HexStringToBinary(ByVal hexString As String) As String
        Dim num As Integer = Integer.Parse(hexString, NumberStyles.HexNumber)
        Return Convert.ToString(num, 2)
    End Function

    Sub EditReg(KeyName As String, ValueName As String, Value As Object, Optional ByVal Binary As Boolean = False, Optional ByVal [String] As Boolean = False)

        Dim R As RegistryKey

        If KeyName.Contains("HKEY_CURRENT_USER") Then
            R = Registry.CurrentUser
            KeyName = KeyName.Remove(0, "HKEY_CURRENT_USER\".Count)

        ElseIf KeyName.Contains("HKEY_LOCAL_MACHINE") Then
            R = Registry.LocalMachine
            KeyName = KeyName.Remove(0, "HKEY_LOCAL_MACHINE\".Count)
        End If

        Try
            If Binary Then
                R.OpenSubKey(KeyName, True).SetValue(ValueName, Value, RegistryValueKind.Binary)
            ElseIf [String] Then
                R.OpenSubKey(KeyName, True).SetValue(ValueName, Value, RegistryValueKind.String)
            Else
                R.OpenSubKey(KeyName, True).SetValue(ValueName, Value, RegistryValueKind.DWord)
            End If
        Catch
            'MainForm.status_lbl.Text = "Error in applying values of LogonUI. Restart the application as an Administrator and try again."
        Finally
            If R IsNot Nothing Then R.Close()
        End Try

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

    Function BizareColorInvertor([Color] As Color) As Color
        Return Color.FromArgb([Color].B, [Color].G, [Color].R)
    End Function
End Class
