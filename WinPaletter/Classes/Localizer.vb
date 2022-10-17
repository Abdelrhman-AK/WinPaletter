
Imports System.Reflection
Imports WinPaletter.XenonCore

Public Class Localizer
    Sub New()

    End Sub

#Region "Language Info"
    Property Name As String = "Abdelrhman-AK"
    Property TrVer As String = "1.0"
    Property Lang As String = "English"
    Property LangCode As String = "EN-US"
    Property AppVer As String = "1.0.0.0"
    Property RightToLeft As Boolean = False
#End Region

#Region "Deep-In-Code Strings"
    Property InvalidTheme As String = "Error: Invalid Theme File."
    Property ThemeNotExist As String = "Theme File doesn't exist."
    Property OK As String = "OK"
    Property Next_ As String = "Next"
    Property Yes As String = "Yes"
    Property No As String = "No"
    Property NewTag As String = "Create New Theme (Palette) File"
    Property OpenTag As String = "Open a Theme File"
    Property SaveTag As String = "Save Theme File"
    Property SaveAsTag As String = "Save Theme File as ..."
    Property EditInfoTag As String = "Edit Information of current Theme File"
    Property NewUpdate As String = "New Update Available"
    Property OpenForActions As String = "Open Updates form for actions."
    Property By As String = "By"
    Property Show As String = "Show"
    Property Hide As String = "Hide"

    Property CP_11_StartMenu_Taskbar_AC As String = "Start Menu, Taskbar && Action Center"
    Property CP_11_ACHover_Links As String = "Action Center Hover && Links"
    Property CP_11_Lines_Toggles_Buttons As String = "Lines, Toggles && Buttons"
    Property CP_11_StartMenu_AC As String = "Start Menu && Action Center Colors"
    Property CP_11_Taskbar As String = "Taskbar Color"
    Property CP_11_Settings As String = "Settings Icons, Text Selection, Focus Dots && Some Pressed Buttons"
    Property CP_11_SomePressedButtons As String = "Some Pressed Buttons"

    Property CP_10_ACLinks As String = "Action Center Links"
    Property CP_10_TaskbarAppUnderline As String = "Taskbar App Underline"
    Property CP_10_StartMenuIconHover As String = "Start Menu Icon Hover"
    Property CP_10_Settings_Links_SomeBtns As String = "Settings Icons, Links && Some Pressed Buttons"
    Property CP_10_Settings_Links_Taskbar_SomeBtns As String = "Settings Icons, Links, Taskbar Focused App && Some Pressed Buttons"
    Property CP_10_Settings_Links_TaskbarUndeline_SomeBtns As String = "Settings Icons, Links, Taskbar App Underline && Some Pressed Buttons"
    Property CP_10_Hamburger As String = "Sliding Hamburger Menu"
    Property CP_10_StartMenu_AC As String = "Start Menu && Action Center"
    Property CP_10_StartMenu_AC_TaskbarActiveApp As String = "Start Menu, Action Center && Taskbar Active App"
    Property CP_10_Taskbar As String = "Taskbar"
    Property CP_10_Taskbar_ACLinks As String = "Taskbar Background Color && Action Center Links"
    Property CP_10_TaskbarFocusedApp_StartButtonHover As String = "Taskbar Focused App && Start Menu Button Hover"
    Property CP_Undefined As String = "Undefined"
    Property CP_ClassicThemeEditable As String = "Classic theme is editable by Win32UI Editor"
    Property CP_AccentOnTaskbarTib As String = "Applying Accent on Taskbar only isn't effective only for Windows 10 2015 Versions, but it is effective for higher versions."
    Property CP_TitlebarToggle As String = "To colorize Titlebar, please activate its toggle."

    Property X22 As String = "This will restart the explorer, don't worry this won't close other applications."
    Property X23 As String = "Windows Volume slider, UAC and Windows 10 LogonUI follow Active Titlebar color"
    Property NoDefResExplorer As String = "Restarting Explorer is diabled from settings. If the palette is not applied correctly, <br> restart explorer."

    Property CurrentMode As String = "Current Mode"
    Property SaveMsg As String = "Do you want to save Settings?"
    Property SettingsSaved As String = "Settings Saved"
    Property RemoveExtMsg As String = "Are you sure from removing files association (*.wpth, *.wpsf) from registry?"
    Property RemoveExtMsgNote As String = "Note: You can reassociate them by activating its checkbox and restarting the application."
    Property UninstallMsgLine1 As String = "Are you sure from Uninstalling the program?"
    Property UninstallMsgLine2 As String = "This will delete associated files extensions from registry."
    Property RestartRecommendation As String = "It's Recommended. Don't worry it won't close your work. If you are obsessed about this, save your work at first."
    Property EmptyName As String = "You can't leave Palette Name Empty. Please type a name to it."
    Property EmptyAuthorName As String = "You can't leave Author's Name Empty. Please type Author's name or your name."
    Property EmptyVer As String = "You can't leave Palette Version Empty. Please type a version to it in this style (x.x.x.x), replacing (x) by numbers"
    Property WrongVerFormat As String = "Wrong Version Fomrat. Please type the version to it in this style (x.x.x.x), replacing (x) by numbers."
    Property Extracting As String = "Extracting palette from image depends on your device's performance, maximum palette colors number, image quality and its resolution ..."
    Property Sorting As String = "Sorting Colors in Palette ..."
    Property ErrorPhrasingChangelog As String = "Error phrasing changelog"
    Property VersionNotReleased As String = "is not released yet, deleted or written in a wrong format."
    Property ReleasedOn As String = "Released on:"
    Property Version As String = "Version"
    Property Label5_Checking As String = "Checking ..."
    Property Error_Online As String = "Error reading changelog online"
    Property NoNetwork As String = "No Network is Available"
    Property CheckConnection As String = "Check your connection and try again"
    Property DoAction_Update As String = "Do Action"
    Property NoUpdateAvailable As String = "No Available Updates"
    Property CheckForUpdates As String = "Check for updates"
    Property NetworkError As String = "Network Error"
    Property ServerError As String = "Error: Network issues or Github repository is private or deleted. Visit Github page for details."
    Property Msgbox_Downloaded As String = "Downloaded Successfully"
    Property MBSizeUnit As String = "MB"
    Property Stable As String = "Stable"
    Property Beta As String = "Beta"
    Property Channel As String = "Channel"
    Property LngExported As String = "Language Exported Successfully"
    Property MenuNativeWin As String = "From Init (Native Windows)"
    Property MenuInit As String = "From Init (Empty Colors)"
    Property MenuAppliedReg As String = "From Current Applied Palette"
    Property ScalingTip As String = "Scaling option is only a preview, the cursor will be saved with different sizes and the situable size will be loaded according to your DPI settings."
    Property Win32UISavingThemeError As String = "Error saving file: "
    Property LngShouldClose As String = "You should close the app if you want to export language."
    Property CMD_Enable As String = "You should enable terminal editing from the toggle above."
    Property CMD_NotAllWeights As String = "Not all weights are avaliable according to your OS and the font itself. Normal and Bold ones are the basic ones."
    Property ExtTer_NotFound As String = "Terminal not found. Select a valid one from the list."
    Property ExtTer_Set As String = "Terminal Preferences are set successfully!"
    Property ExtTer_NewSuccess As String = "This key is entered into registry successfully."
    Property ExtTer_NewError As String = "Couldn't write this entry to registry. Please check if this key already exists or check permissions."
    Property ErrorDetails As String = "Error Details: "
    Property Terminal_alreadyset As String = "You can't set this name as it is already set to another profile."
    Property TerminalStable_notFound As String = "Windows Terminal Stable isn't installed or ""settings.json"" isn't accessible."
    Property TerminalPreview_notFound As String = "Windows Terminal Preview isn't installed or ""settings.json"" isn't accessible."
    Property PowerShellx86_notFound As String = "Microsoft PowerShell x86 is not installed."
    Property PowerShellx64_notFound As String = "Microsoft PowerShell x64 is not installed."
    Property Terminal_supposed As String = "It is supposed to be located in: "
    Property Terminal_Bypass As String = "However, you can bypass this restriction in Settings > Terminals (In case you want to design a theme for all Versions of Windows and save it as a file for sharing, not applying it)."
    Property Terminal_CantRun As String = "You can't run Windows Terminal in current OS. It is available only in Windows 10 and 11."
    Property Terminal_ErrorFile As String = "Error occurred while reading settings file: "
    Property Terminal_ProfileNotCloneable As String = "Default Profile isn't cloneable, select a different profile."
    Property Terminal_ThemeNotCloneable As String = "Default themes (Dark\Light\System) are not cloneable, select a different theme or create a new theme if you want to clone."
    Property Terminal_Clone As String = "Clone"
    Property Terminal_NewProfile As String = "New Profile"
    Property Terminal_NewScheme As String = "New Scheme"
    Property Terminal_NewTheme As String = "New Theme"
    Property Terminal_SettingsNotExist As String = "Settings doesn't exist: "

    Property Terminal_External_Empty As String = "Terminal can't be empty. Enter a valid one."
    Property Terminal_External_NotExist As String = "Terminal doesn't exist. Enter a valid one."
    Property Terminal_External_Reversed As String = "This terminal is reserved for system. Try again with another one."
    Property Terminal_External_Exists As String = "This terminal already exists. Try again with another one."

    Property CP_TerminalError As String = "Error occured while saving Windows Terminal Settings. Anyway, WinPaletter will continue saving without Windows Terminal."
    Property CP_ApplyingTheme As String = "Applying Theme ..."
    Property CP_ApplyingColorsAndTweaks As String = "Applying Colors and Tweaks ..."
    Property CP_ApplyingCustomLogonUI As String = "Applying Custom LogonUI ..."
    Property CP_RenderingCustomLogonUI_Progress As String = "Rendering Custom LogonUI:"
    Property CP_RenderingCustomLogonUI As String = "Rendering Custom LogonUI ..."
    Property CP_ApplyingTerminalColors As String = "Applying Terminals Colors ..."
    Property CP_SavingCursorsColors As String = "Saving Cursors Colors ..."
    Property CP_RenderingCursors As String = "Rendering Cursors ..."
    Property CP_ApplyingCursors As String = "Applying Cursors ..."
    Property CP_ApplyingWin32UI As String = "Applying Win32UI Colors (Classic Windows Elements) ..."
#End Region

    Public Sub ExportNativeLang(File As String)
        Dim lx As New List(Of String)
        lx.Clear()

        lx.Add("!Name = Abdelrhman-AK")
        lx.Add("!TrVer = 1.0")
        lx.Add("!Lang = English")
        lx.Add("!LangCode = EN-US")
        lx.Add("!AppVer = " & My.Application.Info.Version.ToString)
        lx.Add("!RightToLeft = False")

        Dim newL As New Localizer

        Dim type1 As Type = newL.[GetType]() : Dim properties1 As PropertyInfo() = type1.GetProperties()

        For Each [property] As PropertyInfo In properties1

            If Not String.IsNullOrWhiteSpace([property].GetValue(newL)) _
                 And Not [property].Name.ToLower = "Name".ToLower _
                 And Not [property].Name.ToLower = "TrVer".ToLower _
                 And Not [property].Name.ToLower = "Lang".ToLower _
                 And Not [property].Name.ToLower = "LangCode".ToLower _
                 And Not [property].Name.ToLower = "AppVer".ToLower _
                 And Not [property].Name.ToLower = "RightToLeft".ToLower Then

                lx.Add(String.Format("@{0} = {1}", [property].Name, [property].GetValue(newL).ToString.Replace(vbCrLf, "<br>")))

            End If

        Next

        Dim LS As New List(Of String)
        LS.Clear()

        For Each f In Assembly.GetExecutingAssembly().GetTypes().Where(Function(t) GetType(Form).IsAssignableFrom(t))
            Dim ins As New Form()
            ins = DirectCast(Activator.CreateInstance(f), Form)

            LS.Add(ins.Name & ".Text = " & ins.Text)
            For Each ctrl In GetAllControls(ins)
                If Not String.IsNullOrWhiteSpace(ctrl.Text) And Not IsNumeric(ctrl.Text) And Not ctrl.Text.Count = 1 And Not ctrl.Text = ctrl.Name Then
                    LS.Add(ins.Name & "." & ctrl.Name & ".Text = " & ctrl.Text.Replace(vbCrLf, "<br>"))
                End If

                If Not String.IsNullOrWhiteSpace(ctrl.Tag) Then
                    LS.Add(ins.Name & "." & ctrl.Name & ".Tag = " & ctrl.Tag.Replace(vbCrLf, "<br>"))
                End If
            Next
            ins.Dispose()
        Next



        IO.File.WriteAllText(File, CStr_FromList(lx) & vbCrLf & CStr_FromList(LS))
    End Sub

    Public Sub LoadLanguageFromFile(File As String, Optional [_Form] As Form = Nothing)
        If IO.File.Exists(File) Then
            Dim Dic As New List(Of Tuple(Of String, String, String, Object))()
            Dic.Clear()
            Dim Definer As New Dictionary(Of String, String)
            Definer.Clear()

            For Each X As String In IO.File.ReadAllLines(File)
                If X.StartsWith("!") Then
                    If X.ToLower.StartsWith("!Name = ".ToLower) Then Name = X.Remove(0, "!Name = ".Count)
                    If X.ToLower.StartsWith("!TrVer = ".ToLower) Then TrVer = X.Remove(0, "!TrVer = ".Count)
                    If X.ToLower.StartsWith("!Lang = ".ToLower) Then Lang = X.Remove(0, "!Lang = ".Count)
                    If X.ToLower.StartsWith("!LangCode = ".ToLower) Then LangCode = X.Remove(0, "!LangCode = ".Count)
                    If X.ToLower.StartsWith("!AppVer = ".ToLower) Then AppVer = X.Remove(0, "!AppVer = ".Count)

                    Try
                        If X.ToLower.StartsWith("!RightToLeft = ".ToLower) Then RightToLeft = X.Remove(0, "!RightToLeft = ".Count)
                    Catch
                        RightToLeft = False
                    End Try

                ElseIf X.StartsWith("@") Then
                    Dim x0, x1 As String
                    x0 = X.Split("=")(0).Replace("@", "").Trim
                    x1 = X.Split("=")(1).Trim

                    Definer.Add(x0, x1)

                Else
                    Dim FormName, ControlName, Prop, Value As String
                    FormName = Nothing
                    ControlName = Nothing
                    Prop = Nothing
                    Value = Nothing

                    Select Case X.Split("=")(0).Trim.Split(".").Count
                        Case 3
                            FormName = X.Split("=")(0).Trim.Split(".")(0)
                            ControlName = X.Split("=")(0).Trim.Split(".")(1)
                            Prop = X.Split("=")(0).Trim.Split(".")(2)
                        Case 2
                            FormName = X.Split("=")(0).Trim.Split(".")(0)
                            ControlName = Nothing
                            Prop = X.Split("=")(0).Trim.Split(".")(1)
                    End Select

                    Value = X.Replace(X.Split("=")(0), "").Trim.Remove(0, 1).Trim.Replace("<br>", vbCrLf)

                    Dic.Add(New Tuple(Of String, String, String, Object)(FormName, ControlName, Prop, Value))
                End If
            Next


            Dim type1 As Type = [GetType]() : Dim properties1 As PropertyInfo() = type1.GetProperties()

            For Each [property] As PropertyInfo In properties1
                Try
                    If Definer.Keys.Contains([property].Name) Then
                        [property].SetValue(Me, Convert.ChangeType(Definer([property].Name).ToString.Replace("<br>", vbCrLf), [property].PropertyType))
                    End If
                Catch
                End Try
            Next

            If [_Form] Is Nothing Then
                For x As Integer = 0 To My.Application.allForms.Count - 1
                    Populate(Dic, My.Application.GetFormFromName(My.Application.allForms(x)))
                Next
            Else
                Populate(Dic, [_Form])
            End If

            My.Application.AdjustFonts()

            Dic.Clear()
            Definer.Clear()
        End If
    End Sub

    Sub LoadInternal()
        MainFrm.ToolStripMenuItem2.Text = MenuInit
        MainFrm.FromCurrentPaletteToolStripMenuItem.Text = MenuAppliedReg

        My.Application.AdjustFonts()
    End Sub

    Sub Populate(ByVal Dic As List(Of Tuple(Of String, String, String, Object)), [Form] As Form)
        [Form].SuspendLayout()

        'Item1 = FormName
        'Item2 = ControlName
        'Item3 = Prop
        'Item4 = Value


        For Each dicX In Dic

            If [Form].Name.ToLower = dicX.Item1.ToLower Then

                If dicX.Item2 = Nothing Then
                    '# Form
                    Try : If dicX.Item3.ToLower = "text" Then [Form].Text = dicX.Item4
                    Catch : End Try

                    Try : If dicX.Item3.ToLower = "tag" Then [Form].Tag = dicX.Item4.ToString.Replace("<br>", vbCrLf)
                    Catch : End Try

                Else
                    '# Control
                    For Each ctrl As Control In [Form].Controls.Find(dicX.Item2, True)

                        Try : If dicX.Item3.ToLower = "text" Then ctrl.Text = dicX.Item4.ToString.Replace("<br>", vbCrLf)
                        Catch : End Try

                        Try : If dicX.Item3.ToLower = "tag" Then ctrl.Tag = dicX.Item4.ToString.Replace("<br>", vbCrLf)
                        Catch : End Try

                        'ctrl.RightToLeft = If(RightToLeft, 1, 0)

                        ctrl.Refresh()
                    Next

                End If


            End If

        Next

        MainFrm.ToolStripMenuItem2.Text = MenuInit
        MainFrm.FromCurrentPaletteToolStripMenuItem.Text = MenuAppliedReg

        [Form].RightToLeftLayout = RightToLeft
        '[Form].RightToLeft = If(RightToLeft, 1, 0)
        'RTL([Form])

        [Form].ResumeLayout()
        [Form].Refresh()
    End Sub
    Sub RTL(Parent As Control)

        If RightToLeft Then

            For Each XeTP As XenonTabControl In Parent.Controls.OfType(Of XenonTabControl)
                XeTP.RightToLeft = If(RightToLeft, 1, 0)
                XeTP.RightToLeftLayout = RightToLeft

                For i = 0 To XeTP.TabPages.Count - 1
                    XeTP.TabPages.Item(i).RightToLeft = If(RightToLeft, 1, 0)
                    If XeTP.TabPages.Item(i).HasChildren Then RTL(XeTP.TabPages.Item(i))

                    For Each Cx As Control In XeTP.TabPages.Item(i).Controls
                        Cx.Left = XeTP.TabPages.Item(i).Width - Cx.Left - Cx.Width
                        If Cx.HasChildren Then RTL(Cx)
                    Next
                Next
            Next

            For Each XeTP As Control In Parent.Controls
                If TypeOf XeTP Is XenonGroupBox Or TypeOf XeTP Is Panel Or TypeOf XeTP Is ContainerControl Then
                    XeTP.RightToLeft = If(RightToLeft, 1, 0)
                    For Each Cx As Control In XeTP.Controls
                        Cx.Left = XeTP.Width - Cx.Left - Cx.Width
                        If Cx.HasChildren Then RTL(Cx)
                    Next
                End If
            Next

        End If


    End Sub

    Private Function GetAllControls(parent As Control) As IEnumerable(Of Control)
        Dim cs = parent.Controls.OfType(Of Control)
        Return cs.SelectMany(Function(c) GetAllControls(c)).Concat(cs)
    End Function

End Class
