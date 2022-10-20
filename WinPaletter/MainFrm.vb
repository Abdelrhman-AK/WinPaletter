Imports System.ComponentModel
Imports System.Net
Imports System.Reflection
Imports System.Text
Imports WinPaletter.CP
Imports WinPaletter.XenonCore

Public Class MainFrm
    Private _Shown As Boolean = False
    Public CP, CP_Original, CP_FirstTime As CP
    Dim CP_BeforeDragAndDrop As CP
    Public PreviewConfig As WinVer = WinVer.Eleven
    Dim RaiseUpdate As Boolean = False
    Dim ver As String = ""
    Dim StableInt, BetaInt, UpdateChannel As Integer
    Dim ChannelFixer As Integer

#Region "CP Subs"
    Sub ApplyLivePreviewFromCP(ByVal [CP] As CP)
        Dim AnimX1 As Integer = 25
        Dim AnimX2 As Integer = 1

        If Not PreviewConfig = WinVer.Seven And Not PreviewConfig = WinVer.Eight Then
            XenonWindow1.Active = True
            XenonWindow2.Active = False

            XenonWindow1.AccentColor_Enabled = [CP].ApplyAccentonTitlebars
            XenonWindow2.AccentColor_Enabled = [CP].ApplyAccentonTitlebars

            XenonWindow1.AccentColor_Active = [CP].Titlebar_Active
            XenonWindow2.AccentColor_Active = [CP].Titlebar_Active

            XenonWindow1.AccentColor_Inactive = [CP].Titlebar_Inactive
            XenonWindow2.AccentColor_Inactive = [CP].Titlebar_Inactive

            XenonWindow1.DarkMode = Not [CP].AppMode_Light
            XenonWindow2.DarkMode = Not [CP].AppMode_Light

            Visual.FadeColor(Label8, "Forecolor", Label8.ForeColor, If([CP].AppMode_Light, Color.Black, Color.White), AnimX1, AnimX2)

            If PreviewConfig = WinVer.Eleven Then
                lbl5.Text = My.Application.LanguageHelper.CP_11_Settings
                lbl6.Text = My.Application.LanguageHelper.CP_11_SomePressedButtons
                lbl7.Text = My.Application.LanguageHelper.CP_Undefined
                lbl8.Text = My.Application.LanguageHelper.CP_Undefined
                pic5.Image = My.Resources.Mini_SettingsIcons
                pic6.Image = My.Resources.Mini_PressedButton
                pic7.Image = My.Resources.Mini_Undefined
                pic8.Image = My.Resources.Mini_Undefined
                pic9.Image = My.Resources.Mini_Undefined

                Select Case Not [CP].WinMode_Light
                    Case True   ''''''''''Dark
                        lbl1.Text = My.Application.LanguageHelper.CP_11_StartMenu_Taskbar_AC
                        lbl2.Text = My.Application.LanguageHelper.CP_11_ACHover_Links
                        lbl3.Text = My.Application.LanguageHelper.CP_11_Lines_Toggles_Buttons
                        lbl4.Text = My.Application.LanguageHelper.CP_Undefined

                        pic1.Image = My.Resources.Mini_StartMenu_Taskbar_AC
                        pic2.Image = My.Resources.Mini_ACHover_Links
                        pic3.Image = My.Resources.Mini_Lines_Toggles_Buttons
                        pic4.Image = My.Resources.Mini_Undefined


                    Case False   ''''''''''Light
                        lbl1.Text = My.Application.LanguageHelper.CP_11_ACHover_Links
                        lbl2.Text = My.Application.LanguageHelper.CP_11_StartMenu_AC
                        lbl3.Text = My.Application.LanguageHelper.CP_11_Taskbar
                        lbl4.Text = My.Application.LanguageHelper.CP_11_Lines_Toggles_Buttons

                        pic1.Image = My.Resources.Mini_ACHover_Links
                        pic2.Image = My.Resources.Mini_StartMenu_Taskbar_AC
                        pic3.Image = My.Resources.Mini_Taskbar
                        pic4.Image = My.Resources.Mini_Lines_Toggles_Buttons
                End Select

            Else
                Select Case Not [CP].WinMode_Light
                    Case True ''''''''''Dark
                        lbl2.Text = My.Application.LanguageHelper.CP_10_ACLinks
                        lbl3.Text = My.Application.LanguageHelper.CP_10_TaskbarAppUnderline
                        lbl5.Text = My.Application.LanguageHelper.CP_10_Settings_Links_SomeBtns
                        lbl6.Text = My.Application.LanguageHelper.CP_10_StartMenuIconHover
                        lbl7.Text = My.Application.LanguageHelper.CP_Undefined

                        pic2.Image = My.Resources.Mini_ACHover_Links
                        pic3.Image = My.Resources.Mini_TaskbarApp
                        pic5.Image = My.Resources.Mini_SettingsIcons
                        pic6.Image = My.Resources.Native10
                        pic7.Image = My.Resources.Mini_Undefined

                        If [CP].Transparency Then
                            lbl1.Text = My.Application.LanguageHelper.CP_10_Hamburger
                            lbl4.Text = My.Application.LanguageHelper.CP_10_StartMenu_AC
                            lbl8.Text = My.Application.LanguageHelper.CP_10_Taskbar

                            pic1.Image = My.Resources.Mini_Hamburger
                            pic4.Image = My.Resources.Mini_StartMenu_Taskbar_AC
                            pic8.Image = My.Resources.Mini_Taskbar

                            If [CP].ApplyAccentonTaskbar <> ApplyAccentonTaskbar_Level.None Then
                                lbl5.Text = My.Application.LanguageHelper.CP_10_Settings_Links_Taskbar_SomeBtns
                            End If

                        Else
                            lbl1.Text = My.Application.LanguageHelper.CP_10_Taskbar
                            pic1.Image = My.Resources.Mini_Taskbar
                            pic4.Image = My.Resources.Mini_StartMenu_Taskbar_AC

                            If [CP].ApplyAccentonTaskbar <> ApplyAccentonTaskbar_Level.None Then
                                lbl4.Text = My.Application.LanguageHelper.CP_10_StartMenu_AC_TaskbarActiveApp
                            Else
                                lbl4.Text = My.Application.LanguageHelper.CP_10_StartMenu_AC
                            End If

                            lbl8.Text = My.Application.LanguageHelper.CP_Undefined
                            pic8.Image = My.Resources.Mini_Undefined

                        End If

                    Case False ''''''''''Light
                        If [CP].Transparency Then
                            lbl1.Text = My.Application.LanguageHelper.CP_10_Hamburger
                            lbl4.Text = My.Application.LanguageHelper.CP_10_StartMenu_AC
                            lbl6.Text = My.Application.LanguageHelper.CP_10_StartMenuIconHover
                            lbl7.Text = My.Application.LanguageHelper.CP_Undefined

                            pic1.Image = My.Resources.Mini_Hamburger
                            pic4.Image = My.Resources.Mini_StartMenu_Taskbar_AC
                            pic5.Image = My.Resources.Mini_SettingsIcons
                            pic6.Image = My.Resources.Native10
                            pic7.Image = My.Resources.Mini_Undefined
                            pic8.Image = My.Resources.Mini_Taskbar

                            If [CP].ApplyAccentonTaskbar = ApplyAccentonTaskbar_Level.None Then
                                lbl2.Text = My.Application.LanguageHelper.CP_Undefined
                                lbl3.Text = My.Application.LanguageHelper.CP_Undefined
                                lbl5.Text = My.Application.LanguageHelper.CP_10_Settings_Links_TaskbarUndeline_SomeBtns
                                lbl8.Text = My.Application.LanguageHelper.CP_10_Taskbar_ACLinks

                                pic2.Image = My.Resources.Mini_Undefined
                                pic3.Image = My.Resources.Mini_Undefined

                            ElseIf [CP].ApplyAccentonTaskbar = ApplyAccentonTaskbar_Level.Taskbar Then
                                lbl2.Text = My.Application.LanguageHelper.CP_Undefined
                                lbl3.Text = My.Application.LanguageHelper.CP_10_TaskbarAppUnderline
                                lbl5.Text = My.Application.LanguageHelper.CP_10_Settings_Links_SomeBtns
                                lbl8.Text = My.Application.LanguageHelper.CP_10_Taskbar_ACLinks

                                pic2.Image = My.Resources.Mini_Undefined
                                pic3.Image = My.Resources.Mini_TaskbarApp

                            Else
                                lbl2.Text = My.Application.LanguageHelper.CP_10_ACLinks
                                lbl3.Text = My.Application.LanguageHelper.CP_10_TaskbarAppUnderline
                                lbl5.Text = My.Application.LanguageHelper.CP_10_Settings_Links_SomeBtns
                                lbl8.Text = My.Application.LanguageHelper.CP_10_Taskbar

                                pic2.Image = My.Resources.Mini_ACHover_Links
                                pic3.Image = My.Resources.Mini_TaskbarApp

                            End If
                        Else
                            lbl1.Text = My.Application.LanguageHelper.CP_10_Taskbar
                            lbl6.Text = My.Application.LanguageHelper.CP_10_StartMenuIconHover
                            lbl7.Text = My.Application.LanguageHelper.CP_Undefined

                            pic1.Image = My.Resources.Mini_Taskbar
                            pic6.Image = My.Resources.Native10
                            pic7.Image = My.Resources.Mini_Undefined

                            If [CP].ApplyAccentonTaskbar = ApplyAccentonTaskbar_Level.None Then
                                lbl2.Text = My.Application.LanguageHelper.CP_Undefined
                                lbl3.Text = My.Application.LanguageHelper.CP_Undefined
                                lbl4.Text = My.Application.LanguageHelper.CP_10_StartMenu_AC
                                lbl5.Text = My.Application.LanguageHelper.CP_10_Settings_Links_TaskbarUndeline_SomeBtns
                                lbl8.Text = My.Application.LanguageHelper.CP_10_ACLinks

                                pic2.Image = My.Resources.Mini_Undefined
                                pic3.Image = My.Resources.Mini_Undefined
                                pic4.Image = My.Resources.Mini_StartMenu_Taskbar_AC
                                pic5.Image = My.Resources.Mini_SettingsIcons
                                pic8.Image = My.Resources.Mini_ACHover_Links

                            ElseIf [CP].ApplyAccentonTaskbar = ApplyAccentonTaskbar_Level.Taskbar Then
                                lbl2.Text = My.Application.LanguageHelper.CP_Undefined
                                lbl3.Text = My.Application.LanguageHelper.CP_10_TaskbarAppUnderline
                                lbl4.Text = My.Application.LanguageHelper.CP_10_TaskbarFocusedApp_StartButtonHover
                                lbl5.Text = My.Application.LanguageHelper.CP_10_Settings_Links_SomeBtns
                                lbl8.Text = My.Application.LanguageHelper.CP_10_ACLinks

                                pic2.Image = My.Resources.Mini_Undefined
                                pic3.Image = My.Resources.Mini_TaskbarApp
                                pic4.Image = My.Resources.Mini_TaskbarActiveIcon
                                pic5.Image = My.Resources.Mini_SettingsIcons
                                pic8.Image = My.Resources.Mini_ACHover_Links

                            Else
                                lbl2.Text = My.Application.LanguageHelper.CP_10_ACLinks
                                lbl3.Text = My.Application.LanguageHelper.CP_10_TaskbarAppUnderline
                                lbl4.Text = My.Application.LanguageHelper.CP_10_StartMenu_AC_TaskbarActiveApp
                                lbl5.Text = My.Application.LanguageHelper.CP_10_Settings_Links_SomeBtns
                                lbl8.Text = My.Application.LanguageHelper.CP_Undefined

                                pic2.Image = My.Resources.Mini_ACHover_Links
                                pic3.Image = My.Resources.Mini_TaskbarApp
                                pic4.Image = My.Resources.Mini_StartMenu_Taskbar_AC
                                pic5.Image = My.Resources.Mini_SettingsIcons
                                pic8.Image = My.Resources.Mini_Undefined
                            End If
                        End If
                End Select
            End If


        Else
            XenonWindow1.Active = True
            XenonWindow2.Active = False
        End If

        Select Case PreviewConfig
            Case WinVer.Eleven
#Region "Win11"
                start.DarkMode = Not [CP].WinMode_Light
                taskbar.DarkMode = Not [CP].WinMode_Light
                ActionCenter.DarkMode = Not [CP].WinMode_Light

                taskbar.Transparency = [CP].Transparency
                start.Transparency = [CP].Transparency
                ActionCenter.Transparency = [CP].Transparency

                start.Top = taskbar.Top - start.Height - 9

                Select Case Not [CP].WinMode_Light
                    Case True   ''''''''''Dark
                        taskbar.BackColorAlpha = 130
                        start.BackColorAlpha = 130
                        ActionCenter.BackColorAlpha = 130

                        Select Case [CP].ApplyAccentonTaskbar
                            Case ApplyAccentonTaskbar_Level.None
                                Visual.FadeColor(taskbar, "BackColor", taskbar.BackColor, Color.FromArgb(55, 55, 55), AnimX1, AnimX2)
                                Visual.FadeColor(start, "BackColor", start.BackColor, Color.FromArgb(40, 40, 40), AnimX1, AnimX2)
                                Visual.FadeColor(ActionCenter, "BackColor", ActionCenter.BackColor, Color.FromArgb(55, 55, 55), AnimX1, AnimX2)

                            Case ApplyAccentonTaskbar_Level.Taskbar_Start_AC
                                Visual.FadeColor(taskbar, "BackColor", taskbar.BackColor, Color.FromArgb(taskbar.BackColor.A, [CP].StartListFolders_TaskbarFront), AnimX1, AnimX2)
                                Visual.FadeColor(start, "BackColor", start.BackColor, Color.FromArgb(start.BackColor.A, [CP].StartListFolders_TaskbarFront), AnimX1, AnimX2)
                                Visual.FadeColor(ActionCenter, "BackColor", ActionCenter.BackColor, Color.FromArgb(ActionCenter.BackColor.A, [CP].StartListFolders_TaskbarFront), AnimX1, AnimX2)

                            Case ApplyAccentonTaskbar_Level.Taskbar
                                Visual.FadeColor(taskbar, "BackColor", taskbar.BackColor, Color.FromArgb(taskbar.BackColor.A, [CP].StartListFolders_TaskbarFront), AnimX1, AnimX2)
                                Visual.FadeColor(start, "BackColor", start.BackColor, Color.FromArgb(40, 40, 40), AnimX1, AnimX2)
                                Visual.FadeColor(ActionCenter, "BackColor", ActionCenter.BackColor, Color.FromArgb(55, 55, 55), AnimX1, AnimX2)

                        End Select


                        Visual.FadeColor(ActionCenter, "ActionCenterButton_Normal", ActionCenter.ActionCenterButton_Normal, [CP].Taskbar_Icon_Underline, AnimX1, AnimX2)
                        Visual.FadeColor(ActionCenter, "ActionCenterButton_Hover", ActionCenter.ActionCenterButton_Hover, [CP].ActionCenter_AppsLinks, AnimX1, AnimX2)
                        Visual.FadeColor(ActionCenter, "ActionCenterButton_Pressed", ActionCenter.ActionCenterButton_Pressed, [CP].StartButton_Hover, AnimX1, AnimX2)
                        Visual.FadeColor(start, "SearchBoxAccent", start.SearchBoxAccent, [CP].Taskbar_Icon_Underline, AnimX1, AnimX2)
                        Visual.FadeColor(taskbar, "AppUnderline", taskbar.AppUnderline, [CP].Taskbar_Icon_Underline, AnimX1, AnimX2)

                        Visual.FadeColor(setting_icon_preview, "Forecolor", setting_icon_preview.ForeColor, [CP].SettingsIconsAndLinks, AnimX1, AnimX2)
                        Visual.FadeColor(lnk_preview, "Forecolor", lnk_preview.ForeColor, [CP].ActionCenter_AppsLinks, AnimX1, AnimX2)

                    Case False   ''''''''''Light
                        taskbar.BackColorAlpha = 180
                        start.BackColorAlpha = 180
                        ActionCenter.BackColorAlpha = 180

                        Select Case [CP].ApplyAccentonTaskbar
                            Case ApplyAccentonTaskbar_Level.None
                                Visual.FadeColor(taskbar, "BackColor", taskbar.BackColor, Color.FromArgb(255, 255, 255), AnimX1, AnimX2)
                                Visual.FadeColor(start, "BackColor", start.BackColor, Color.FromArgb(255, 255, 255), AnimX1, AnimX2)
                                Visual.FadeColor(ActionCenter, "BackColor", ActionCenter.BackColor, Color.FromArgb(255, 255, 255), AnimX1, AnimX2)

                            Case ApplyAccentonTaskbar_Level.Taskbar_Start_AC
                                Visual.FadeColor(taskbar, "BackColor", taskbar.BackColor, Color.FromArgb(taskbar.BackColor.A, [CP].Taskbar_Icon_Underline), AnimX1, AnimX2)
                                Visual.FadeColor(start, "BackColor", start.BackColor, Color.FromArgb(start.BackColor.A, [CP].ActionCenter_AppsLinks), AnimX1, AnimX2)
                                Visual.FadeColor(ActionCenter, "BackColor", ActionCenter.BackColor, Color.FromArgb(ActionCenter.BackColor.A, [CP].ActionCenter_AppsLinks), AnimX1, AnimX2)

                            Case ApplyAccentonTaskbar_Level.Taskbar
                                Visual.FadeColor(taskbar, "BackColor", taskbar.BackColor, Color.FromArgb(taskbar.BackColor.A, [CP].Taskbar_Icon_Underline), AnimX1, AnimX2)
                                Visual.FadeColor(start, "BackColor", start.BackColor, Color.FromArgb(255, 255, 255), AnimX1, AnimX2)
                                Visual.FadeColor(ActionCenter, "BackColor", ActionCenter.BackColor, Color.FromArgb(255, 255, 255), AnimX1, AnimX2)

                        End Select


                        Visual.FadeColor(ActionCenter, "ActionCenterButton_Normal", ActionCenter.ActionCenterButton_Normal, [CP].StartMenuBackground_ActiveTaskbarButton, AnimX1, AnimX2)
                        Visual.FadeColor(ActionCenter, "ActionCenterButton_Hover", ActionCenter.ActionCenterButton_Hover, [CP].StartListFolders_TaskbarFront, AnimX1, AnimX2)
                        Visual.FadeColor(ActionCenter, "ActionCenterButton_Pressed", ActionCenter.ActionCenterButton_Pressed, [CP].StartButton_Hover, AnimX1, AnimX2)
                        Visual.FadeColor(start, "SearchBoxAccent", start.SearchBoxAccent, [CP].StartMenuBackground_ActiveTaskbarButton, AnimX1, AnimX2)
                        Visual.FadeColor(taskbar, "AppUnderline", taskbar.AppUnderline, [CP].StartMenuBackground_ActiveTaskbarButton, AnimX1, AnimX2)

                        Visual.FadeColor(setting_icon_preview, "Forecolor", setting_icon_preview.ForeColor, [CP].SettingsIconsAndLinks, AnimX1, AnimX2)
                        Visual.FadeColor(lnk_preview, "Forecolor", lnk_preview.ForeColor, [CP].StartListFolders_TaskbarFront, AnimX1, AnimX2)
                End Select

                ReValidateLivePreview(pnl_preview)
#End Region
            Case WinVer.Ten
#Region "Win10"

                start.DarkMode = Not [CP].WinMode_Light
                taskbar.DarkMode = Not [CP].WinMode_Light
                ActionCenter.DarkMode = Not [CP].WinMode_Light
                taskbar.Transparency = [CP].Transparency
                start.Transparency = [CP].Transparency
                ActionCenter.Transparency = [CP].Transparency
                start.Top = taskbar.Top - start.Height

                If [CP].Transparency Then
                    If Not [CP].WinMode_Light Then
                        taskbar.BackColorAlpha = 150
                        start.BackColorAlpha = 150
                        ActionCenter.BackColorAlpha = 150
                    Else
                        taskbar.BackColorAlpha = 200
                        start.BackColorAlpha = 200
                        ActionCenter.BackColorAlpha = 200
                    End If
                Else
                    taskbar.BackColorAlpha = 255
                    start.BackColorAlpha = 255
                    ActionCenter.BackColorAlpha = 255
                End If

                Select Case Not [CP].WinMode_Light
                    Case True

                        If [CP].Transparency Then
                            Select Case [CP].ApplyAccentonTaskbar
                                Case ApplyAccentonTaskbar_Level.None
                                    Visual.FadeColor(taskbar, "BackColor", taskbar.BackColor, Color.FromArgb(16, 16, 16), AnimX1, AnimX2)
                                    Visual.FadeColor(taskbar, "StartColor", taskbar.StartColor, Color.FromArgb(150, 150, 150, 150), AnimX1, AnimX2)
                                    Visual.FadeColor(start, "BackColor", start.BackColor, Color.FromArgb(31, 31, 31), AnimX1, AnimX2)
                                    Visual.FadeColor(ActionCenter, "BackColor", ActionCenter.BackColor, Color.FromArgb(31, 31, 31), AnimX1, AnimX2)

                                    Visual.FadeColor(taskbar, "AppBackground", taskbar.AppBackground, Color.FromArgb(150, 150, 150, 150), AnimX1, AnimX2)
                                    Visual.FadeColor(ActionCenter, "LinkColor", ActionCenter.LinkColor, [CP].ActionCenter_AppsLinks, AnimX1, AnimX2)
                                    Visual.FadeColor(taskbar, "AppUnderline", taskbar.AppUnderline, [CP].Taskbar_Icon_Underline, AnimX1, AnimX2)
                                    Visual.FadeColor(setting_icon_preview, "Forecolor", setting_icon_preview.ForeColor, [CP].SettingsIconsAndLinks, AnimX1, AnimX2)
                                    Visual.FadeColor(lnk_preview, "Forecolor", lnk_preview.ForeColor, [CP].SettingsIconsAndLinks, AnimX1, AnimX2)
                                    Visual.FadeColor(ActionCenter, "ActionCenterButton_Normal", ActionCenter.ActionCenterButton_Normal, [CP].SettingsIconsAndLinks, AnimX1, AnimX2)

                                Case ApplyAccentonTaskbar_Level.Taskbar
                                    Visual.FadeColor(taskbar, "BackColor", taskbar.BackColor, [CP].Taskbar_Background, AnimX1, AnimX2)
                                    Visual.FadeColor(taskbar, "StartColor", taskbar.StartColor, Color.FromArgb(0, 0, 0, 0), AnimX1, AnimX2)
                                    Visual.FadeColor(start, "BackColor", start.BackColor, Color.FromArgb(31, 31, 31), AnimX1, AnimX2)
                                    Visual.FadeColor(ActionCenter, "BackColor", ActionCenter.BackColor, Color.FromArgb(31, 31, 31), AnimX1, AnimX2)

                                    Visual.FadeColor(taskbar, "AppBackground", taskbar.AppBackground, Color.FromArgb(150, [CP].SettingsIconsAndLinks), AnimX1, AnimX2)
                                    Visual.FadeColor(ActionCenter, "LinkColor", ActionCenter.LinkColor, [CP].ActionCenter_AppsLinks, AnimX1, AnimX2)
                                    Visual.FadeColor(taskbar, "AppUnderline", taskbar.AppUnderline, [CP].Taskbar_Icon_Underline, AnimX1, AnimX2)
                                    Visual.FadeColor(setting_icon_preview, "Forecolor", setting_icon_preview.ForeColor, [CP].SettingsIconsAndLinks, AnimX1, AnimX2)
                                    Visual.FadeColor(lnk_preview, "Forecolor", lnk_preview.ForeColor, [CP].SettingsIconsAndLinks, AnimX1, AnimX2)
                                    Visual.FadeColor(ActionCenter, "ActionCenterButton_Normal", ActionCenter.ActionCenterButton_Normal, [CP].SettingsIconsAndLinks, AnimX1, AnimX2)

                                Case ApplyAccentonTaskbar_Level.Taskbar_Start_AC
                                    Visual.FadeColor(taskbar, "BackColor", taskbar.BackColor, [CP].Taskbar_Background, AnimX1, AnimX2)
                                    Visual.FadeColor(taskbar, "StartColor", taskbar.StartColor, Color.FromArgb(0, 0, 0, 0), AnimX1, AnimX2)
                                    Visual.FadeColor(start, "BackColor", start.BackColor, [CP].StartMenuBackground_ActiveTaskbarButton, AnimX1, AnimX2)
                                    Visual.FadeColor(ActionCenter, "BackColor", ActionCenter.BackColor, [CP].StartMenuBackground_ActiveTaskbarButton, AnimX1, AnimX2)

                                    Visual.FadeColor(taskbar, "AppBackground", taskbar.AppBackground, Color.FromArgb(150, [CP].SettingsIconsAndLinks), AnimX1, AnimX2)
                                    Visual.FadeColor(ActionCenter, "LinkColor", ActionCenter.LinkColor, [CP].ActionCenter_AppsLinks, AnimX1, AnimX2)
                                    Visual.FadeColor(taskbar, "AppUnderline", taskbar.AppUnderline, [CP].Taskbar_Icon_Underline, AnimX1, AnimX2)
                                    Visual.FadeColor(setting_icon_preview, "Forecolor", setting_icon_preview.ForeColor, [CP].SettingsIconsAndLinks, AnimX1, AnimX2)
                                    Visual.FadeColor(lnk_preview, "Forecolor", lnk_preview.ForeColor, [CP].SettingsIconsAndLinks, AnimX1, AnimX2)
                                    Visual.FadeColor(ActionCenter, "ActionCenterButton_Normal", ActionCenter.ActionCenterButton_Normal, [CP].SettingsIconsAndLinks, AnimX1, AnimX2)

                            End Select

                        Else
                            Select Case [CP].ApplyAccentonTaskbar
                                Case ApplyAccentonTaskbar_Level.None
                                    Visual.FadeColor(taskbar, "BackColor", taskbar.BackColor, Color.FromArgb(16, 16, 16), AnimX1, AnimX2)
                                    Visual.FadeColor(taskbar, "StartColor", taskbar.StartColor, Color.FromArgb(31, 31, 31), AnimX1, AnimX2)
                                    Visual.FadeColor(start, "BackColor", start.BackColor, Color.FromArgb(31, 31, 31), AnimX1, AnimX2)
                                    Visual.FadeColor(ActionCenter, "BackColor", ActionCenter.BackColor, Color.FromArgb(31, 31, 31), AnimX1, AnimX2)

                                Case ApplyAccentonTaskbar_Level.Taskbar
                                    Visual.FadeColor(taskbar, "BackColor", taskbar.BackColor, [CP].StartListFolders_TaskbarFront, AnimX1, AnimX2)
                                    Visual.FadeColor(taskbar, "StartColor", taskbar.StartColor, [CP].StartMenuBackground_ActiveTaskbarButton, AnimX1, AnimX2)
                                    Visual.FadeColor(start, "BackColor", start.BackColor, Color.FromArgb(31, 31, 31), AnimX1, AnimX2)
                                    Visual.FadeColor(ActionCenter, "BackColor", ActionCenter.BackColor, Color.FromArgb(31, 31, 31), AnimX1, AnimX2)

                                Case ApplyAccentonTaskbar_Level.Taskbar_Start_AC
                                    Visual.FadeColor(taskbar, "BackColor", taskbar.BackColor, [CP].StartListFolders_TaskbarFront, AnimX1, AnimX2)
                                    Visual.FadeColor(taskbar, "StartColor", taskbar.StartColor, [CP].StartMenuBackground_ActiveTaskbarButton, AnimX1, AnimX2)
                                    Visual.FadeColor(start, "BackColor", start.BackColor, [CP].StartMenuBackground_ActiveTaskbarButton, AnimX1, AnimX2)
                                    Visual.FadeColor(ActionCenter, "BackColor", ActionCenter.BackColor, [CP].StartMenuBackground_ActiveTaskbarButton, AnimX1, AnimX2)
                            End Select

                            If [CP].ApplyAccentonTaskbar = ApplyAccentonTaskbar_Level.None Then
                                Visual.FadeColor(taskbar, "AppBackground", taskbar.AppBackground, Color.FromArgb(150, 100, 100, 100), AnimX1, AnimX2)
                            Else
                                Visual.FadeColor(taskbar, "AppBackground", taskbar.AppBackground, [CP].StartMenuBackground_ActiveTaskbarButton, AnimX1, AnimX2)
                            End If

                            Visual.FadeColor(ActionCenter, "LinkColor", ActionCenter.LinkColor, [CP].ActionCenter_AppsLinks, AnimX1, AnimX2)
                            Visual.FadeColor(taskbar, "AppUnderline", taskbar.AppUnderline, [CP].Taskbar_Icon_Underline, AnimX1, AnimX2)
                            Visual.FadeColor(setting_icon_preview, "Forecolor", setting_icon_preview.ForeColor, [CP].SettingsIconsAndLinks, AnimX1, AnimX2)
                            Visual.FadeColor(lnk_preview, "Forecolor", lnk_preview.ForeColor, [CP].SettingsIconsAndLinks, AnimX1, AnimX2)
                            Visual.FadeColor(ActionCenter, "ActionCenterButton_Normal", ActionCenter.ActionCenterButton_Normal, [CP].SettingsIconsAndLinks, AnimX1, AnimX2)

                        End If

                    Case False
                        If [CP].Transparency Then

                            Select Case [CP].ApplyAccentonTaskbar
                                Case ApplyAccentonTaskbar_Level.None
                                    Visual.FadeColor(taskbar, "BackColor", taskbar.BackColor, Color.FromArgb(238, 238, 238), AnimX1, AnimX2)
                                    Visual.FadeColor(taskbar, "StartColor", taskbar.StartColor, Color.Transparent, AnimX1, AnimX2)
                                    Visual.FadeColor(start, "BackColor", start.BackColor, Color.FromArgb(228, 228, 228), AnimX1, AnimX2)
                                    Visual.FadeColor(ActionCenter, "BackColor", ActionCenter.BackColor, Color.FromArgb(228, 228, 228), AnimX1, AnimX2)

                                    Visual.FadeColor(taskbar, "AppBackground", taskbar.AppBackground, Color.FromArgb(150, 238, 238, 238), AnimX1, AnimX2)
                                    Visual.FadeColor(ActionCenter, "LinkColor", ActionCenter.LinkColor, [CP].Taskbar_Background, AnimX1, AnimX2)
                                    Visual.FadeColor(taskbar, "AppUnderline", taskbar.AppUnderline, [CP].SettingsIconsAndLinks, AnimX1, AnimX2)
                                    Visual.FadeColor(setting_icon_preview, "Forecolor", setting_icon_preview.ForeColor, [CP].SettingsIconsAndLinks, AnimX1, AnimX2)
                                    Visual.FadeColor(lnk_preview, "Forecolor", lnk_preview.ForeColor, [CP].SettingsIconsAndLinks, AnimX1, AnimX2)
                                    Visual.FadeColor(ActionCenter, "ActionCenterButton_Normal", ActionCenter.ActionCenterButton_Normal, [CP].SettingsIconsAndLinks, AnimX1, AnimX2)

                                Case ApplyAccentonTaskbar_Level.Taskbar
                                    Visual.FadeColor(taskbar, "BackColor", taskbar.BackColor, [CP].Taskbar_Background, AnimX1, AnimX2)
                                    Visual.FadeColor(taskbar, "StartColor", taskbar.StartColor, Color.Transparent, AnimX1, AnimX2)
                                    Visual.FadeColor(start, "BackColor", start.BackColor, Color.FromArgb(228, 228, 228), AnimX1, AnimX2)
                                    Visual.FadeColor(ActionCenter, "BackColor", ActionCenter.BackColor, Color.FromArgb(228, 228, 228), AnimX1, AnimX2)

                                    Visual.FadeColor(taskbar, "AppBackground", taskbar.AppBackground, Color.FromArgb(150, [CP].SettingsIconsAndLinks), AnimX1, AnimX2)
                                    Visual.FadeColor(ActionCenter, "LinkColor", ActionCenter.LinkColor, [CP].Taskbar_Background, AnimX1, AnimX2)
                                    Visual.FadeColor(taskbar, "AppUnderline", taskbar.AppUnderline, [CP].Taskbar_Icon_Underline, AnimX1, AnimX2)
                                    Visual.FadeColor(setting_icon_preview, "Forecolor", setting_icon_preview.ForeColor, [CP].SettingsIconsAndLinks, AnimX1, AnimX2)
                                    Visual.FadeColor(lnk_preview, "Forecolor", lnk_preview.ForeColor, [CP].SettingsIconsAndLinks, AnimX1, AnimX2)
                                    Visual.FadeColor(ActionCenter, "ActionCenterButton_Normal", ActionCenter.ActionCenterButton_Normal, [CP].SettingsIconsAndLinks, AnimX1, AnimX2)

                                Case ApplyAccentonTaskbar_Level.Taskbar_Start_AC
                                    Visual.FadeColor(taskbar, "BackColor", taskbar.BackColor, [CP].Taskbar_Background, AnimX1, AnimX2)
                                    Visual.FadeColor(taskbar, "StartColor", taskbar.StartColor, Color.Transparent, AnimX1, AnimX2)
                                    Visual.FadeColor(start, "BackColor", start.BackColor, [CP].StartMenuBackground_ActiveTaskbarButton, AnimX1, AnimX2)
                                    Visual.FadeColor(ActionCenter, "BackColor", ActionCenter.BackColor, [CP].StartMenuBackground_ActiveTaskbarButton, AnimX1, AnimX2)

                                    Visual.FadeColor(taskbar, "AppBackground", taskbar.AppBackground, Color.FromArgb(150, [CP].SettingsIconsAndLinks), AnimX1, AnimX2)
                                    Visual.FadeColor(ActionCenter, "LinkColor", ActionCenter.LinkColor, [CP].ActionCenter_AppsLinks, AnimX1, AnimX2)
                                    Visual.FadeColor(taskbar, "AppUnderline", taskbar.AppUnderline, [CP].Taskbar_Icon_Underline, AnimX1, AnimX2)
                                    Visual.FadeColor(setting_icon_preview, "Forecolor", setting_icon_preview.ForeColor, [CP].SettingsIconsAndLinks, AnimX1, AnimX2)
                                    Visual.FadeColor(lnk_preview, "Forecolor", lnk_preview.ForeColor, [CP].SettingsIconsAndLinks, AnimX1, AnimX2)
                                    Visual.FadeColor(ActionCenter, "ActionCenterButton_Normal", ActionCenter.ActionCenterButton_Normal, [CP].SettingsIconsAndLinks, AnimX1, AnimX2)

                            End Select

                        Else

                            Select Case [CP].ApplyAccentonTaskbar
                                Case ApplyAccentonTaskbar_Level.None
                                    Visual.FadeColor(taskbar, "BackColor", taskbar.BackColor, Color.FromArgb(238, 238, 238), AnimX1, AnimX2)
                                    Visual.FadeColor(taskbar, "StartColor", taskbar.StartColor, Color.FromArgb(241, 241, 241), AnimX1, AnimX2)
                                    Visual.FadeColor(start, "BackColor", start.BackColor, Color.FromArgb(228, 228, 228), AnimX1, AnimX2)
                                    Visual.FadeColor(ActionCenter, "BackColor", ActionCenter.BackColor, Color.FromArgb(228, 228, 228), AnimX1, AnimX2)

                                    Visual.FadeColor(taskbar, "AppBackground", taskbar.AppBackground, Color.FromArgb(252, 252, 252), AnimX1, AnimX2)
                                    Visual.FadeColor(ActionCenter, "LinkColor", ActionCenter.LinkColor, [CP].Taskbar_Background, AnimX1, AnimX2)
                                    Visual.FadeColor(taskbar, "AppUnderline", taskbar.AppUnderline, [CP].SettingsIconsAndLinks, AnimX1, AnimX2)
                                    Visual.FadeColor(setting_icon_preview, "Forecolor", setting_icon_preview.ForeColor, [CP].SettingsIconsAndLinks, AnimX1, AnimX2)
                                    Visual.FadeColor(lnk_preview, "Forecolor", lnk_preview.ForeColor, [CP].SettingsIconsAndLinks, AnimX1, AnimX2)
                                    Visual.FadeColor(ActionCenter, "ActionCenterButton_Normal", ActionCenter.ActionCenterButton_Normal, [CP].SettingsIconsAndLinks, AnimX1, AnimX2)

                                Case ApplyAccentonTaskbar_Level.Taskbar
                                    Visual.FadeColor(taskbar, "BackColor", taskbar.BackColor, [CP].StartListFolders_TaskbarFront, AnimX1, AnimX2)
                                    Visual.FadeColor(taskbar, "StartColor", taskbar.StartColor, [CP].StartMenuBackground_ActiveTaskbarButton, AnimX1, AnimX2)
                                    Visual.FadeColor(start, "BackColor", start.BackColor, Color.FromArgb(228, 228, 228), AnimX1, AnimX2)
                                    Visual.FadeColor(ActionCenter, "BackColor", ActionCenter.BackColor, Color.FromArgb(228, 228, 228), AnimX1, AnimX2)

                                    Visual.FadeColor(taskbar, "AppBackground", taskbar.AppBackground, [CP].StartMenuBackground_ActiveTaskbarButton, AnimX1, AnimX2)
                                    Visual.FadeColor(ActionCenter, "LinkColor", ActionCenter.LinkColor, [CP].Taskbar_Background, AnimX1, AnimX2)
                                    Visual.FadeColor(taskbar, "AppUnderline", taskbar.AppUnderline, [CP].Taskbar_Icon_Underline, AnimX1, AnimX2)
                                    Visual.FadeColor(setting_icon_preview, "Forecolor", setting_icon_preview.ForeColor, [CP].SettingsIconsAndLinks, AnimX1, AnimX2)
                                    Visual.FadeColor(lnk_preview, "Forecolor", lnk_preview.ForeColor, [CP].SettingsIconsAndLinks, AnimX1, AnimX2)
                                    Visual.FadeColor(ActionCenter, "ActionCenterButton_Normal", ActionCenter.ActionCenterButton_Normal, [CP].SettingsIconsAndLinks, AnimX1, AnimX2)

                                Case ApplyAccentonTaskbar_Level.Taskbar_Start_AC
                                    Visual.FadeColor(taskbar, "BackColor", taskbar.BackColor, [CP].StartListFolders_TaskbarFront, AnimX1, AnimX2)
                                    Visual.FadeColor(taskbar, "StartColor", taskbar.StartColor, [CP].StartMenuBackground_ActiveTaskbarButton, AnimX1, AnimX2)
                                    Visual.FadeColor(start, "BackColor", start.BackColor, [CP].StartMenuBackground_ActiveTaskbarButton, AnimX1, AnimX2)
                                    Visual.FadeColor(ActionCenter, "BackColor", ActionCenter.BackColor, [CP].StartMenuBackground_ActiveTaskbarButton, AnimX1, AnimX2)


                                    Visual.FadeColor(taskbar, "AppBackground", taskbar.AppBackground, [CP].StartMenuBackground_ActiveTaskbarButton, AnimX1, AnimX2)
                                    Visual.FadeColor(ActionCenter, "LinkColor", ActionCenter.LinkColor, [CP].ActionCenter_AppsLinks, AnimX1, AnimX2)
                                    Visual.FadeColor(taskbar, "AppUnderline", taskbar.AppUnderline, [CP].Taskbar_Icon_Underline, AnimX1, AnimX2)
                                    Visual.FadeColor(setting_icon_preview, "Forecolor", setting_icon_preview.ForeColor, [CP].SettingsIconsAndLinks, AnimX1, AnimX2)
                                    Visual.FadeColor(lnk_preview, "Forecolor", lnk_preview.ForeColor, [CP].SettingsIconsAndLinks, AnimX1, AnimX2)
                                    Visual.FadeColor(ActionCenter, "ActionCenterButton_Normal", ActionCenter.ActionCenterButton_Normal, [CP].SettingsIconsAndLinks, AnimX1, AnimX2)

                            End Select

                        End If
                End Select



                ReValidateLivePreview(pnl_preview)
#End Region
            Case WinVer.Eight
#Region "Win8.1"
                If My.W8 And My.Application._Settings.Win7LivePreview Then
                    RefreshDWM([CP])
                End If

                ApplyMetroStartToButton([CP])
                ApplyBackLogonUI([CP])

                Select Case [CP].Metro_Theme
                    Case CP.AeroTheme.Aero
                        XenonWindow1.Win8Lite = False
                        XenonWindow2.Win8Lite = False
                        taskbar.Transparency = True
                        taskbar.BackColorAlpha = 100
                    Case CP.AeroTheme.AeroLite
                        XenonWindow1.Win8Lite = True
                        XenonWindow2.Win8Lite = True
                        taskbar.Transparency = False
                        taskbar.BackColorAlpha = 255
                End Select

                XenonWindow1.AccentColor_Active = [CP].Aero_ColorizationColor
                XenonWindow1.Win7ColorBal = [CP].Aero_ColorizationColorBalance

                XenonWindow2.AccentColor_Active = [CP].Aero_ColorizationColor
                XenonWindow2.Win7ColorBal = [CP].Aero_ColorizationColorBalance

                taskbar.BackColor = [CP].Aero_ColorizationColor
                taskbar.Win7ColorBal = [CP].Aero_ColorizationColorBalance

                ReValidateLivePreview(pnl_preview)
#End Region
            Case WinVer.Seven
#Region "Win7"
                If My.W7 And My.Application._Settings.Win7LivePreview And _Shown Then
                    RefreshDWM([CP])
                End If

                Select Case [CP].Aero_Theme
                    Case CP.AeroTheme.Aero
                        start.Transparency = True
                        start.Basic = False
                        taskbar.Transparency = True
                        taskbar.Basic = False
                        With XenonWindow1
                            .Win7 = True
                            .Win7Aero = True
                            .Win7AeroOpaque = False
                            .Win7Basic = False
                            .Win7Alpha = [CP].Aero_ColorizationBlurBalance
                            .Win7ColorBal = [CP].Aero_ColorizationColorBalance
                            .Win7GlowBal = [CP].Aero_ColorizationAfterglowBalance
                            .AccentColor_Active = [CP].Aero_ColorizationColor
                            .AccentColor2_Active = [CP].Aero_ColorizationAfterglow
                            .AccentColor_Inactive = [CP].Aero_ColorizationColor
                            .AccentColor2_Inactive = [CP].Aero_ColorizationAfterglow
                            .Win7Noise = [CP].Aero_ColorizationGlassReflectionIntensity
                        End With
                        With XenonWindow2
                            .Win7 = True
                            .Win7Aero = True
                            .Win7AeroOpaque = False
                            .Win7Basic = False
                            .Win7Alpha = [CP].Aero_ColorizationBlurBalance
                            .Win7ColorBal = [CP].Aero_ColorizationColorBalance
                            .Win7GlowBal = [CP].Aero_ColorizationAfterglowBalance
                            .AccentColor_Active = [CP].Aero_ColorizationColor
                            .AccentColor2_Active = [CP].Aero_ColorizationAfterglow
                            .AccentColor_Inactive = [CP].Aero_ColorizationColor
                            .AccentColor2_Inactive = [CP].Aero_ColorizationAfterglow
                            .Win7Noise = [CP].Aero_ColorizationGlassReflectionIntensity
                        End With
                        With start
                            .Win7AeroOpaque = False
                            .BackColorAlpha = [CP].Aero_ColorizationBlurBalance
                            .Win7ColorBal = [CP].Aero_ColorizationColorBalance
                            .Win7GlowBal = [CP].Aero_ColorizationAfterglowBalance
                            .BackColor = [CP].Aero_ColorizationColor
                            .BackColor2 = [CP].Aero_ColorizationAfterglow
                            .NoisePower = [CP].Aero_ColorizationGlassReflectionIntensity
                        End With
                        With taskbar
                            .Win7AeroOpaque = False
                            .BackColorAlpha = [CP].Aero_ColorizationBlurBalance
                            .Win7ColorBal = [CP].Aero_ColorizationColorBalance
                            .Win7GlowBal = [CP].Aero_ColorizationAfterglowBalance
                            .BackColor = [CP].Aero_ColorizationColor
                            .BackColor2 = [CP].Aero_ColorizationAfterglow
                            .NoisePower = [CP].Aero_ColorizationGlassReflectionIntensity
                        End With

                    Case CP.AeroTheme.AeroOpaque
                        start.Transparency = True
                        start.Basic = False
                        taskbar.Transparency = True
                        taskbar.Basic = False

                        With XenonWindow1
                            .Win7 = True
                            .Win7Aero = False
                            .Win7AeroOpaque = True
                            .Win7Basic = False
                            .Win7Alpha = [CP].Aero_ColorizationColorBalance
                            .AccentColor_Active = [CP].Aero_ColorizationColor
                            .AccentColor_Inactive = [CP].Aero_ColorizationColor
                            .Win7Noise = [CP].Aero_ColorizationGlassReflectionIntensity
                        End With
                        With XenonWindow2
                            .Win7 = True
                            .Win7Aero = False
                            .Win7AeroOpaque = True
                            .Win7Basic = False
                            .Win7Alpha = [CP].Aero_ColorizationColorBalance
                            .AccentColor_Active = [CP].Aero_ColorizationColor
                            .AccentColor_Inactive = [CP].Aero_ColorizationColor
                            .Win7Noise = [CP].Aero_ColorizationGlassReflectionIntensity
                        End With
                        With taskbar
                            .Win7AeroOpaque = True
                            .BackColorAlpha = [CP].Aero_ColorizationColorBalance
                            .BackColor = [CP].Aero_ColorizationColor
                            .BackColor2 = [CP].Aero_ColorizationColor
                            .NoisePower = [CP].Aero_ColorizationGlassReflectionIntensity
                        End With
                        With start
                            .Win7AeroOpaque = True
                            .BackColorAlpha = [CP].Aero_ColorizationColorBalance
                            .BackColor = [CP].Aero_ColorizationColor
                            .BackColor2 = [CP].Aero_ColorizationColor
                            .NoisePower = [CP].Aero_ColorizationGlassReflectionIntensity
                        End With

                    Case CP.AeroTheme.Basic
#Region "Basic"
                        taskbar.BackColor = Color.FromArgb(166, 190, 218)
                        taskbar.BackColorAlpha = 100
                        start.BackColor = Color.FromArgb(166, 190, 218)
                        start.BackColorAlpha = 100

                        With XenonWindow1
                            .Win7 = True
                            .Win7Aero = False
                            .Win7AeroOpaque = False
                            .Win7Basic = True
                            .Win7Alpha = 100
                            .AccentColor_Active = Color.FromArgb(166, 190, 218)
                            .Win7Noise = 0
                        End With

                        With XenonWindow2
                            .Win7 = True
                            .Win7Aero = False
                            .Win7AeroOpaque = False
                            .Win7Basic = True
                            .Win7Alpha = 100
                            .AccentColor_Inactive = Color.FromArgb(166, 190, 218)
                            .Win7Noise = 0
                        End With

                        start.Transparency = False
                        start.Basic = True
                        start.NoisePower = 0
                        taskbar.Transparency = False
                        taskbar.Basic = True
                        start.NoisePower = 0


#End Region

                    Case CP.AeroTheme.Classic

                End Select

                ReValidateLivePreview(pnl_preview)
#End Region
        End Select
    End Sub

    Sub ReValidateLivePreview(ByVal Parent As Control)
        Parent.Refresh()

        For Each ctrl As Control In Parent.Controls
            ctrl.Refresh()
            If ctrl.HasChildren Then
                For Each c As Control In ctrl.Controls
                    ReValidateLivePreview(c)
                Next
            End If
        Next
    End Sub

    Sub Adjust_Preview()
        If _Shown Then My.Application.AnimatorX.HideSync(pnl_preview)
        Panel3.Visible = True
        lnk_preview.Visible = True

        start.Visible = True
        taskbar.Visible = True
        XenonWindow1.Visible = True
        XenonWindow2.Visible = True
        ActionCenter.Visible = True

        XenonWindow1.Win7 = False
        XenonWindow2.Win7 = False
        XenonWindow1.Win8 = False
        XenonWindow2.Win8 = False
        XenonWindow1.Win8Lite = False
        XenonWindow2.Win8Lite = False
        XenonButton23.Visible = False

        Select Case PreviewConfig
            Case WinVer.Eleven
                ActionCenter.Visible = True
                ActionCenter.Size = New Size(120, 85)
                ActionCenter.Location = New Point(398, 161)
                ActionCenter.Dock = Nothing
                ActionCenter.RoundedCorners = True
                ActionCenter.UseItAsTaskbar_Version = XenonAcrylic.TaskbarVersion.Eleven
                ActionCenter.BlurPower = 7
                ActionCenter.NoisePower = 0.2

                taskbar.Height = 42
                taskbar.UseItAsTaskbar_Version = XenonAcrylic.TaskbarVersion.Eleven
                taskbar.BlurPower = 12

                start.Visible = True
                start.Size = New Size(135, 200)
                start.Location = New Point(7, 46)
                start.RoundedCorners = True
                start.UseItAsTaskbar_Version = XenonAcrylic.TaskbarVersion.Eleven
                start.BlurPower = 7
                start.NoisePower = 0.2

                XenonWindow1.RoundedCorners = True
                XenonWindow2.RoundedCorners = True

            Case WinVer.Ten
                ActionCenter.Visible = True
                ActionCenter.Dock = DockStyle.Right
                ActionCenter.RoundedCorners = False
                ActionCenter.UseItAsTaskbar_Version = XenonAcrylic.TaskbarVersion.Ten
                ActionCenter.BlurPower = 7
                ActionCenter.NoisePower = 0.2

                taskbar.Height = 35
                taskbar.UseItAsTaskbar_Version = XenonAcrylic.TaskbarVersion.Ten
                taskbar.BlurPower = 12

                start.Visible = True
                start.Size = New Size(182, 201)
                start.Location = New Point(0, 59)
                start.RoundedCorners = False
                start.UseItAsTaskbar_Version = XenonAcrylic.TaskbarVersion.Ten
                start.BlurPower = 7
                start.NoisePower = 0.2

                XenonWindow1.RoundedCorners = False
                XenonWindow2.RoundedCorners = False

            Case WinVer.Eight
                Panel3.Visible = False
                lnk_preview.Visible = False

                start.Visible = False
                taskbar.Visible = True
                XenonWindow1.Visible = True
                XenonWindow2.Visible = True
                ActionCenter.Visible = False
                XenonWindow1.Active = True
                XenonWindow2.Active = False
                XenonWindow1.Win8 = True
                XenonWindow2.Win8 = True
                taskbar.UseItAsTaskbar_Version = XenonAcrylic.TaskbarVersion.Eight
                taskbar.BlurPower = 0
                taskbar.Height = 34

                start.Visible = False
                start.BlurPower = 0
                start.Top = taskbar.Top - start.Height
                start.Left = 0

            Case WinVer.Seven
                XenonButton23.Visible = True

                If CP.Aero_Theme = AeroTheme.Classic Then
                    start.Visible = False
                    taskbar.Visible = False
                    XenonWindow1.Visible = False
                    XenonWindow2.Visible = False
                    ActionCenter.Visible = False
                Else
                    start.Visible = True
                    taskbar.Visible = True
                    XenonWindow1.Visible = True
                    XenonWindow2.Visible = True
                    ActionCenter.Visible = False
                End If

                Panel3.Visible = False
                lnk_preview.Visible = False
                taskbar.UseItAsTaskbar_Version = XenonAcrylic.TaskbarVersion.Seven
                taskbar.BlurPower = 1
                taskbar.NoisePower = CP.Aero_ColorizationGlassReflectionIntensity / 100
                taskbar.Height = 34

                start.UseItAsTaskbar_Version = XenonAcrylic.TaskbarVersion.Seven
                start.RoundedCorners = True
                start.BlurPower = 1
                start.NoisePower = 0.5
                start.Left = 0
                start.Width = 136
                start.Height = 191
                start.NoisePower = CP.Aero_ColorizationGlassReflectionIntensity / 100
                start.Top = taskbar.Top - start.Height
        End Select

        If PreviewConfig = WinVer.Ten Or PreviewConfig = WinVer.Eleven Then
            XenonWindow1.Top = start.Top
            XenonWindow1.Left = start.Right + 5

            XenonWindow2.Top = XenonWindow1.Bottom + 5
            XenonWindow2.Left = XenonWindow1.Left
        Else
            XenonWindow1.Top = 10
            XenonWindow1.Left = (XenonWindow1.Parent.Width - XenonWindow1.Width) / 2

            XenonWindow2.Top = XenonWindow1.Bottom + 5
            XenonWindow2.Left = XenonWindow1.Left
        End If

        XenonWindow1.Refresh()
        XenonWindow2.Refresh()

        ReValidateLivePreview(pnl_preview)

        If _Shown Then My.Application.AnimatorX.ShowSync(pnl_preview)
    End Sub

    Sub ApplyCPValues(ByVal ColorPalette As CP)
        themename_lbl.Text = String.Format("{0} ({1})", CP.PaletteName, CP.PaletteVersion)
        author_lbl.Text = String.Format("{0}: {1}", My.Application.LanguageHelper.By, CP.Author)

        WinMode_Toggle.Checked = Not ColorPalette.WinMode_Light
        AppMode_Toggle.Checked = Not ColorPalette.AppMode_Light
        Transparency_Toggle.Checked = ColorPalette.Transparency
        ShowAccentOnTitlebarAndBorders_Toggle.Checked = ColorPalette.ApplyAccentonTitlebars

        Select Case ColorPalette.ApplyAccentonTaskbar
            Case ApplyAccentonTaskbar_Level.None
                Accent_None.Checked = True

            Case ApplyAccentonTaskbar_Level.Taskbar_Start_AC
                Accent_StartTaskbar.Checked = True

            Case ApplyAccentonTaskbar_Level.Taskbar
                Accent_Taskbar.Checked = True

        End Select



        ActiveTitlebar_picker.BackColor = ColorPalette.Titlebar_Active
        InactiveTitlebar_picker.BackColor = ColorPalette.Titlebar_Inactive
        StartAccent_picker.BackColor = ColorPalette.StartMenu_Accent
        StartButtonHover_picker.BackColor = ColorPalette.StartButton_Hover
        TaskbarBackground_Picker.BackColor = ColorPalette.Taskbar_Background
        TaskbarIconUnderline_picker.BackColor = ColorPalette.Taskbar_Icon_Underline
        StartBackgroundAndTaskbarButton_picker.BackColor = ColorPalette.StartMenuBackground_ActiveTaskbarButton
        TaskbarFrontAndFoldersOnStart_picker.BackColor = ColorPalette.StartListFolders_TaskbarFront
        ActionCenter_picker.BackColor = ColorPalette.ActionCenter_AppsLinks
        SettingsIconsAndLinks_picker.BackColor = ColorPalette.SettingsIconsAndLinks
        UWP_Undefined_pick.BackColor = ColorPalette.UWP_Undefined

        Aero_ColorizationColor_pick.BackColor = ColorPalette.Aero_ColorizationColor
        Aero_ColorizationAfterglow_pick.BackColor = ColorPalette.Aero_ColorizationAfterglow
        Aero_ColorizationColorBalance_bar.Value = ColorPalette.Aero_ColorizationColorBalance
        Aero_ColorizationAfterglowBalance_bar.Value = ColorPalette.Aero_ColorizationAfterglowBalance
        Aero_ColorizationBlurBalance_bar.Value = ColorPalette.Aero_ColorizationBlurBalance
        Aero_ColorizationGlassReflectionIntensity_bar.Value = ColorPalette.Aero_ColorizationGlassReflectionIntensity
        Aero_EnableAeroPeek_toggle.Checked = ColorPalette.Aero_EnableAeroPeek
        Aero_AlwaysHibernateThumbnails_Toggle.Checked = ColorPalette.Aero_AlwaysHibernateThumbnails

        If PreviewConfig = WinVer.Seven Then
            Select Case ColorPalette.Aero_Theme
                Case CP.AeroTheme.Aero
                    theme_aero.Checked = True

                Case CP.AeroTheme.AeroOpaque
                    theme_aeroopaque.Checked = True

                Case CP.AeroTheme.Basic
                    theme_basic.Checked = True

                Case CP.AeroTheme.Classic
                    theme_classic.Checked = True
            End Select

        ElseIf PreviewConfig = WinVer.Eight Then
            Select Case ColorPalette.Metro_Theme
                Case CP.AeroTheme.Aero
                    XenonRadioImage1.Checked = True

                Case CP.AeroTheme.AeroLite
                    XenonRadioImage2.Checked = True
            End Select
        End If

        ColorizationColor8_pick.BackColor = ColorPalette.Aero_ColorizationColor
        ColorizationBalance8_track.Value = ColorPalette.Aero_ColorizationColorBalance

        start8_pick.BackColor = ColorPalette.Metro_StartColor
        accent8_pick.BackColor = ColorPalette.Metro_AccentColor
        personalcls8_background_pick.BackColor = ColorPalette.Metro_PersonalColors_Background
        personalcolor8accent_pick.BackColor = ColorPalette.Metro_PersonalColors_Accent

        ApplyMetroStartToButton(ColorPalette)
        ApplyBackLogonUI(ColorPalette)
    End Sub

    Sub ApplyDefaultCPValues()
        Dim DefCP As CP

        If My.W11 Then
            DefCP = New CP_Defaults().Default_Windows11
        ElseIf My.W10 Then
            DefCP = New CP_Defaults().Default_Windows10
        ElseIf My.W8 Then
            DefCP = New CP_Defaults().Default_Windows8
        ElseIf My.W7 Then
            DefCP = New CP_Defaults().Default_Windows7
        Else
            DefCP = New CP_Defaults().Default_Windows11
        End If

        ActiveTitlebar_picker.DefaultColor = DefCP.Titlebar_Active
        InactiveTitlebar_picker.DefaultColor = DefCP.Titlebar_Inactive
        StartAccent_picker.DefaultColor = DefCP.StartMenu_Accent
        StartButtonHover_picker.DefaultColor = DefCP.StartButton_Hover
        TaskbarBackground_Picker.DefaultColor = DefCP.Taskbar_Background
        TaskbarIconUnderline_picker.DefaultColor = DefCP.Taskbar_Icon_Underline
        StartBackgroundAndTaskbarButton_picker.DefaultColor = DefCP.StartMenuBackground_ActiveTaskbarButton
        TaskbarFrontAndFoldersOnStart_picker.DefaultColor = DefCP.StartListFolders_TaskbarFront
        ActionCenter_picker.DefaultColor = DefCP.ActionCenter_AppsLinks
        SettingsIconsAndLinks_picker.DefaultColor = DefCP.SettingsIconsAndLinks
        UWP_Undefined_pick.DefaultColor = DefCP.UWP_Undefined

        Aero_ColorizationColor_pick.DefaultColor = DefCP.Aero_ColorizationColor
        Aero_ColorizationAfterglow_pick.DefaultColor = DefCP.Aero_ColorizationAfterglow
        ColorizationColor8_pick.DefaultColor = DefCP.Aero_ColorizationColor
        start8_pick.DefaultColor = DefCP.Metro_StartColor
        accent8_pick.DefaultColor = DefCP.Metro_AccentColor
        personalcls8_background_pick.DefaultColor = DefCP.Metro_PersonalColors_Background
        personalcolor8accent_pick.DefaultColor = DefCP.Metro_PersonalColors_Accent
    End Sub

    Sub ApplyMetroStartToButton(ColorPalette As CP)
        Select Case ColorPalette.Metro_Start
            Case 1
                XenonButton14.Image = ResizeImage(My.Application.WinRes.MetroStart_1, 48, 48)
            Case 2
                XenonButton14.Image = ResizeImage(My.Application.WinRes.MetroStart_2, 48, 48)
            Case 3
                XenonButton14.Image = ResizeImage(My.Application.WinRes.MetroStart_3, 48, 48)
            Case 4
                XenonButton14.Image = ResizeImage(My.Application.WinRes.MetroStart_4, 48, 48)
            Case 5
                XenonButton14.Image = ResizeImage(My.Application.WinRes.MetroStart_5, 48, 48)
            Case 6
                XenonButton14.Image = ResizeImage(My.Application.WinRes.MetroStart_6, 48, 48)
            Case 7
                XenonButton14.Image = ResizeImage(My.Application.WinRes.MetroStart_7, 48, 48)
            Case 8
                XenonButton14.Image = ResizeImage(My.Application.WinRes.MetroStart_8, 48, 48)
            Case 9
                XenonButton14.Image = ResizeImage(My.Application.WinRes.MetroStart_9, 48, 48)
            Case 10
                XenonButton14.Image = ResizeImage(My.Application.WinRes.MetroStart_10, 48, 48)
            Case 11
                XenonButton14.Image = ResizeImage(My.Application.WinRes.MetroStart_11, 48, 48)
            Case 12
                XenonButton14.Image = ResizeImage(My.Application.WinRes.MetroStart_12, 48, 48)
            Case 13
                XenonButton14.Image = ResizeImage(My.Application.WinRes.MetroStart_13, 48, 48)
            Case 14
                XenonButton14.Image = ResizeImage(My.Application.WinRes.MetroStart_14, 48, 48)
            Case 15
                XenonButton14.Image = ResizeImage(My.Application.WinRes.MetroStart_15, 48, 48)
            Case 16
                XenonButton14.Image = ResizeImage(My.Application.WinRes.MetroStart_16, 48, 48)
            Case 17
                XenonButton14.Image = ResizeImage(My.Application.WinRes.MetroStart_17, 48, 48)
            Case 18
                XenonButton14.Image = ResizeImage(My.Application.WinRes.MetroStart_18, 48, 48)
            Case 19
                XenonButton14.Image = ColorToBitmap(ColorPalette.Metro_PersonalColors_Background, New Size(48, 48))
            Case 20
                XenonButton14.Image = ResizeImage(My.Application.GetCurrentWallpaper, 48, 48)
            Case Else
                XenonButton14.Image = ResizeImage(My.Application.WinRes.MetroStart_1, 48, 48)
        End Select
    End Sub

    Sub ApplyBackLogonUI(ColorPalette As CP)

        For Each ri As XenonRadioImage In LogonUI8Colors.Controls.OfType(Of XenonRadioImage)
            If ColorPalette.Metro_LogonUI = ri.Name.Replace("color", "") Then
                XenonButton22.Image = ColorToBitmap(ri.AccentColor, New Size(48, 48))
                Exit For
            End If
        Next

    End Sub
#End Region

#Region "Misc"
    Enum WinVer
        Eleven
        Ten
        Eight
        Seven
    End Enum

    Public Sub DoubleBufferedControl(ByVal [Control] As Control, ByVal setting As Boolean)
        Dim panType As Type = [Control].[GetType]()
        Dim pi As PropertyInfo = panType.GetProperty("DoubleBuffered", BindingFlags.Instance Or BindingFlags.NonPublic)
        pi.SetValue([Control], setting, Nothing)
    End Sub

    Sub MakeItDoubleBuffered(ByVal Parent As Control)
        DoubleBufferedControl(Parent, True)

        For Each ctrl As Control In Parent.Controls

            If ctrl.HasChildren Then
                For Each c As Control In ctrl.Controls
                    MakeItDoubleBuffered(c)
                Next
            End If

            DoubleBufferedControl(ctrl, True)
        Next
    End Sub

    Sub UpdateHint(Sender As Object, e As EventArgs)
        status_lbl.Text = TryCast(Sender, XenonButton).Tag
    End Sub

    Sub EraseHint()
        status_lbl.Text = ""
    End Sub
#End Region

    Sub AutoUpdatesCheck()
        StableInt = 0 : BetaInt = 0 : UpdateChannel = 0 : ChannelFixer = 0
        If My.Application._Settings.UpdateChannel = XeSettings.UpdateChannels.Stable Then ChannelFixer = 0
        If My.Application._Settings.UpdateChannel = XeSettings.UpdateChannels.Beta Then ChannelFixer = 1
        BackgroundWorker1.RunWorkerAsync()
    End Sub

    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        If IsNetAvailable() Then
            Try

                Dim ls As New List(Of String)
                Dim WebCL As New WebClient
                RaiseUpdate = False
                ver = ""

                CList_FromStr(ls, WebCL.DownloadString(My.Resources.Link_Updates))

                For x = 0 To ls.Count - 1
                    If Not String.IsNullOrEmpty(ls(x)) And Not ls(x).IndexOf("#") = 0 Then
                        If ls(x).Split(" ")(0) = "Stable" Then StableInt = x
                        If ls(x).Split(" ")(0) = "Beta" Then BetaInt = x
                    End If
                Next

                If ChannelFixer = 0 Then UpdateChannel = StableInt
                If ChannelFixer = 1 Then UpdateChannel = BetaInt

                ver = ls(UpdateChannel).Split(" ")(1)

                If ver > My.Application.Info.Version.ToString Then
                    RaiseUpdate = True
                End If
            Catch
            End Try
        End If
    End Sub

    Private Sub BackgroundWorker1_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        If RaiseUpdate Then Notify(String.Format("{0} ({2}). {1}.", My.Application.LanguageHelper.NewUpdate, My.Application.LanguageHelper.OpenForActions, ver), My.Resources.notify_update, 10000)
    End Sub

    Private Sub MainFrm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _Shown = False
        Visible = False

        Try
            ApplyDarkMode(Me)
            MakeItDoubleBuffered(Me)

            Me.Size = New Size(My.Application._Settings.MainFormWidth, My.Application._Settings.MainFormHeight)
            Me.WindowState = My.Application._Settings.MainFormStatus

            For Each btn As XenonButton In MainToolbar.Controls.OfType(Of XenonButton)
                AddHandler btn.MouseEnter, AddressOf UpdateHint
                AddHandler btn.Enter, AddressOf UpdateHint
                AddHandler btn.MouseLeave, AddressOf EraseHint
                AddHandler btn.Leave, AddressOf EraseHint
            Next

            If Not My.Application.ExternalLink Then
                CP = New CP(CP.Mode.Registry)
                CP_Original = New CP(CP.Mode.Registry)
                CP_FirstTime = New CP(CP.Mode.Registry)
            Else
                CP = New CP(CP.Mode.File, My.Application.ExternalLink_File)
                CP_Original = New CP(CP.Mode.File, My.Application.ExternalLink_File)
                CP_FirstTime = New CP(CP.Mode.File, My.Application.ExternalLink_File)
                OpenFileDialog1.FileName = My.Application.ExternalLink_File
                SaveFileDialog1.FileName = My.Application.ExternalLink_File
                My.Application.ExternalLink = False
                My.Application.ExternalLink_File = ""
            End If

            If My.Application._Settings.CustomPreviewConfig_Enabled Then
                PreviewConfig = My.Application._Settings.CustomPreviewConfig
            Else
                If My.W11 Then PreviewConfig = WinVer.Eleven
                If My.W10 Then PreviewConfig = WinVer.Ten
                If My.W8 Then PreviewConfig = WinVer.Eight
                If My.W7 Then PreviewConfig = WinVer.Seven
            End If

            If PreviewConfig = WinVer.Eleven Then
                XenonButton20.Image = My.Resources.Native11
            ElseIf PreviewConfig = WinVer.Ten Then
                XenonButton20.Image = My.Resources.Native10
            ElseIf PreviewConfig = WinVer.Eight Then
                XenonButton20.Image = My.Resources.Native8
            ElseIf PreviewConfig = WinVer.Seven Then
                XenonButton20.Image = My.Resources.Native7
            Else
                XenonButton20.Image = My.Resources.Native11
            End If

            If PreviewConfig = WinVer.Eleven Or PreviewConfig = WinVer.Ten Then
                PaletteContainer_W1x.Visible = True
                PaletteContainer_W8.Visible = False
                PaletteContainer_W7.Visible = False
            End If

            If PreviewConfig = WinVer.Seven Then
                PaletteContainer_W1x.Visible = False
                PaletteContainer_W8.Visible = False
                PaletteContainer_W7.Visible = True
            End If

            If PreviewConfig = WinVer.Eight Then
                PaletteContainer_W1x.Visible = False
                PaletteContainer_W8.Visible = True
                PaletteContainer_W7.Visible = False
            End If

            pnl_preview.BackgroundImage = My.Application.Wallpaper
            dragPreviewer.pnl_preview.BackgroundImage = My.Application.Wallpaper

            Adjust_Preview()
            ApplyCPValues(CP)
            ApplyDefaultCPValues()
            ApplyLivePreviewFromCP(CP)

        Catch ex As Exception
            MsgBox(ex.Message & vbCrLf & vbCrLf & ex.StackTrace)
        End Try

        Visible = True
    End Sub

    Private Sub MainFrm_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        _Shown = True

        If My.Application._Settings.AutoUpdatesChecking Then AutoUpdatesCheck()
    End Sub

    Private Sub MainFrm_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed

        If Me.WindowState = FormWindowState.Normal Then
            My.Application._Settings.MainFormWidth = Me.Size.Width
            My.Application._Settings.MainFormHeight = Me.Size.Height
        End If

        If Me.WindowState <> FormWindowState.Minimized Then
            My.Application._Settings.MainFormStatus = Me.WindowState
        End If

        My.Application._Settings.Save(XeSettings.Mode.Registry)
    End Sub

    Protected Overrides Sub OnFormClosing(ByVal e As FormClosingEventArgs)
        Dim Changed As Boolean = Not CP.Equals(CP_Original)

        If Changed Then
            Select Case ComplexSave.ShowDialog
                Case DialogResult.Yes

                    Dim r As String() = My.Application.ComplexSaveResult.Split(".")
                    Dim r1 As String = r(0)
                    Dim r2 As String = r(1)

                    Select Case r1
                        Case 0              '' Save
                            If IO.File.Exists(SaveFileDialog1.FileName) Then
                                CP.Save(CP.SavingMode.File, SaveFileDialog1.FileName)
                                CP_Original = New CP(Mode.File, SaveFileDialog1.FileName)
                            Else
                                If SaveFileDialog1.ShowDialog = DialogResult.OK Then
                                    CP.Save(CP.SavingMode.File, SaveFileDialog1.FileName)
                                    CP_Original = New CP(Mode.File, SaveFileDialog1.FileName)
                                Else
                                    e.Cancel = True
                                End If
                            End If
                        Case 1              '' Save As
                            If SaveFileDialog1.ShowDialog = DialogResult.OK Then
                                CP.Save(CP.SavingMode.File, SaveFileDialog1.FileName)
                                CP_Original = New CP(Mode.File, SaveFileDialog1.FileName)
                            Else
                                e.Cancel = True
                            End If
                    End Select

                    Select Case r2
                        Case 1      '' Apply   ' Case 0= Don't Apply
                            CP.Save(CP.SavingMode.Registry)
                            If My.Application._Settings.AutoRestartExplorer Then RestartExplorer()
                    End Select

                Case DialogResult.No
                    e.Cancel = False
                    If (My.W7 Or My.W8) And My.Application._Settings.Win7LivePreview Then RefreshDWM(CP_Original)
                    MyBase.OnFormClosing(e)

                Case DialogResult.Cancel
                    e.Cancel = True
            End Select

        Else
            e.Cancel = False
            MyBase.OnFormClosing(e)
        End If
    End Sub

    Private Sub XenonGroupBox10_Click(sender As Object, e As EventArgs) Handles ActiveTitlebar_picker.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                CP.Titlebar_Active = sender.BackColor
                ApplyLivePreviewFromCP(CP)
            End If
            Exit Sub
        End If

        Dim CList As New List(Of Control) From {sender, XenonWindow1}

        Dim C As Color = ColorPickerDlg.Pick(CList)
        CP.Titlebar_Active = Color.FromArgb(255, C)
        ApplyLivePreviewFromCP(CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()

        If Not CP.ApplyAccentonTitlebars Then
            Notify(My.Application.LanguageHelper.CP_TitlebarToggle, My.Resources.notify_info, 4000)
        End If
    End Sub

    Private Sub XenonGroupBox21_Click(sender As Object, e As EventArgs) Handles InactiveTitlebar_picker.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                CP.Titlebar_Inactive = sender.BackColor
                ApplyLivePreviewFromCP(CP)
            End If
            Exit Sub
        End If

        Dim CList As New List(Of Control) From {sender, XenonWindow2}

        Dim _Conditions As New Conditions With {.Window_InactiveTitlebar = True}
        Dim C As Color = ColorPickerDlg.Pick(CList, _Conditions)

        CP.Titlebar_Inactive = Color.FromArgb(255, C)
        ApplyLivePreviewFromCP(CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()

        If Not CP.ApplyAccentonTitlebars Then
            Notify(My.Application.LanguageHelper.CP_TitlebarToggle, My.Resources.notify_info, 4000)
        End If
    End Sub

    Private Sub WinMode_Toggle_CheckedChanged(sender As Object, e As EventArgs) Handles WinMode_Toggle.CheckedChanged
        If _Shown Then
            CP.WinMode_Light = Not sender.Checked
            ApplyLivePreviewFromCP(CP)
        End If
    End Sub

    Private Sub AppMode_Toggle_CheckedChanged(sender As Object, e As EventArgs) Handles AppMode_Toggle.CheckedChanged
        If _Shown Then
            CP.AppMode_Light = Not sender.Checked
            ApplyLivePreviewFromCP(CP)
        End If
    End Sub

    Private Sub Transparency_Toggle_CheckedChanged(sender As Object, e As EventArgs) Handles Transparency_Toggle.CheckedChanged
        If _Shown Then
            CP.Transparency = sender.Checked
            ApplyLivePreviewFromCP(CP)
        End If
    End Sub

    Private Sub ShowAccentOnTitlebarAndBorders_Toggle_CheckedChanged(sender As Object, e As EventArgs) Handles ShowAccentOnTitlebarAndBorders_Toggle.CheckedChanged
        If _Shown Then
            CP.ApplyAccentonTitlebars = sender.Checked
            ApplyLivePreviewFromCP(CP)
        End If
    End Sub

    Private Sub Accent_None_CheckedChanged(sender As Object) Handles Accent_None.CheckedChanged
        If _Shown Then
            CP.ApplyAccentonTaskbar = ApplyAccentonTaskbar_Level.None
            ApplyLivePreviewFromCP(CP)
        End If
    End Sub

    Private Sub Accent_Taskbar_CheckedChanged(sender As Object) Handles Accent_Taskbar.CheckedChanged
        If _Shown Then
            CP.ApplyAccentonTaskbar = ApplyAccentonTaskbar_Level.Taskbar
            ApplyLivePreviewFromCP(CP)
        End If
    End Sub

    Private Sub Accent_StartTaskbar_CheckedChanged(sender As Object) Handles Accent_StartTaskbar.CheckedChanged
        If _Shown Then
            CP.ApplyAccentonTaskbar = ApplyAccentonTaskbar_Level.Taskbar_Start_AC
            ApplyLivePreviewFromCP(CP)
        End If
    End Sub

    Private Sub TaskbarIconUnderline_picker_Click(sender As Object, e As EventArgs) Handles TaskbarIconUnderline_picker.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                CP.Taskbar_Icon_Underline = sender.BackColor
                ApplyLivePreviewFromCP(CP)
            End If
            Exit Sub
        End If

        Dim CList As New List(Of Control)
        Dim C As Color
        CList.Add(sender)

        If PreviewConfig = WinVer.Eleven Then
            CList.Add(taskbar)

            If Not CP.WinMode_Light Then
                CList.Add(ActionCenter)
                CList.Add(start)

                Dim _Conditions As New Conditions With {
                    .AppUnderlineOnly = True,
                     .StartSearchOnly = True,
                     .ActionCenterBtn = True
 }

                C = ColorPickerDlg.Pick(CList, _Conditions)
            Else
                C = ColorPickerDlg.Pick(CList)
            End If

        Else
            Dim _Conditions As New Conditions

            Select Case Not [CP].WinMode_Light
                Case True
                    CList.Add(taskbar)  ''AppUnderline
                    _Conditions.AppUnderlineOnly = True
                Case False
                    If [CP].ApplyAccentonTaskbar <> ApplyAccentonTaskbar_Level.None Then
                        CList.Add(taskbar)  ''AppUnderline
                        _Conditions.AppUnderlineOnly = True
                    End If
            End Select

            C = ColorPickerDlg.Pick(CList, _Conditions)

        End If



        CP.Taskbar_Icon_Underline = Color.FromArgb(255, C)
        ApplyLivePreviewFromCP(CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()
    End Sub

    Private Sub TaskbarFrontAndFoldersOnStart_picker_Click(sender As Object, e As EventArgs) Handles TaskbarFrontAndFoldersOnStart_picker.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                CP.StartListFolders_TaskbarFront = sender.BackColor
                ApplyLivePreviewFromCP(CP)
            End If
            Exit Sub
        End If

        Dim CList As New List(Of Control) From {sender}

        If PreviewConfig = WinVer.Eleven Then
            If Not CP.WinMode_Light Then
                CList.Add(taskbar)
                CList.Add(start)
                CList.Add(ActionCenter)
            Else
                CList.Add(lnk_preview)
            End If
        Else
            If [CP].Transparency Then
                'CList.Add(start) ''Hamburger
            Else
                CList.Add(taskbar)
            End If
        End If

        Dim C As Color = ColorPickerDlg.Pick(CList)
        CP.StartListFolders_TaskbarFront = Color.FromArgb(255, C)
        ApplyLivePreviewFromCP(CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()

    End Sub

    Private Sub ActionCenter_picker_Click(sender As Object, e As EventArgs) Handles ActionCenter_picker.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                CP.ActionCenter_AppsLinks = sender.BackColor
                ApplyLivePreviewFromCP(CP)
            End If
            Exit Sub
        End If

        Dim CList As New List(Of Control)
        Dim C As Color

        CList.Add(sender)

        If PreviewConfig = WinVer.Eleven Then
            If CP.WinMode_Light Then
                CList.Add(start)
                CList.Add(ActionCenter)
                C = ColorPickerDlg.Pick(CList)
            Else
                CList.Add(lnk_preview)
                Dim _Conditions As New Conditions With {.AppUnderlineOnly = True}
                C = ColorPickerDlg.Pick(CList, _Conditions)
            End If
        Else
            Dim _Conditions As New Conditions

            Select Case Not CP.WinMode_Light
                Case True
                    CList.Add(ActionCenter) ''Link
                    _Conditions.ActionCenterLink = True

                Case False
                    If [CP].ApplyAccentonTaskbar = ApplyAccentonTaskbar_Level.Taskbar_Start_AC Then
                        CList.Add(ActionCenter) ''Link
                        _Conditions.ActionCenterLink = True
                    End If
            End Select

            C = ColorPickerDlg.Pick(CList, _Conditions)
        End If

        CP.ActionCenter_AppsLinks = Color.FromArgb(255, C)
        ApplyLivePreviewFromCP(CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()

    End Sub

    Private Sub SettingsIconsAndLinks_picker_Click(sender As Object, e As EventArgs) Handles SettingsIconsAndLinks_picker.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                CP.SettingsIconsAndLinks = sender.BackColor
                ApplyLivePreviewFromCP(CP)
            End If
            Exit Sub
        End If

        Dim CList As New List(Of Control)
        Dim C As Color

        CList.Add(sender)

        If PreviewConfig = WinVer.Eleven Then
            CList.Add(setting_icon_preview)
            C = ColorPickerDlg.Pick(CList)
        Else
            Dim _Conditions As New Conditions

            Select Case Not [CP].WinMode_Light
                Case True
                    If [CP].Transparency Then
                        CList.Add(setting_icon_preview)
                        CList.Add(ActionCenter) : _Conditions.ActionCenterBtn = True
                        CList.Add(lnk_preview)
                        If [CP].ApplyAccentonTaskbar <> ApplyAccentonTaskbar_Level.None Then
                            CList.Add(taskbar)  ''AppBackground
                            _Conditions.AppBackgroundOnly = True

                        End If
                    Else
                        CList.Add(setting_icon_preview)
                        CList.Add(ActionCenter) : _Conditions.ActionCenterBtn = True
                        CList.Add(lnk_preview)
                    End If
                Case False
                    CList.Add(setting_icon_preview)
                    CList.Add(ActionCenter) : _Conditions.ActionCenterBtn = True
                    CList.Add(lnk_preview)

                    If [CP].ApplyAccentonTaskbar = ApplyAccentonTaskbar_Level.None Then
                        CList.Add(taskbar)  ''AppBackground
                        _Conditions.AppBackgroundOnly = True
                        _Conditions.AppUnderlineOnly = True

                    End If
            End Select
            C = ColorPickerDlg.Pick(CList, _Conditions)
        End If

        CP.SettingsIconsAndLinks = Color.FromArgb(255, C)
        ApplyLivePreviewFromCP(CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()
    End Sub

    Private Sub XenonButton4_Click(sender As Object, e As EventArgs) Handles apply_btn.Click
        Cursor = Cursors.WaitCursor

        CP.Save(CP.SavingMode.Registry)
        CP_Original = New CP(Mode.Registry)

        Cursor = Cursors.Default

        If My.Application._Settings.AutoRestartExplorer Then
            RestartExplorer()
        Else
            If Not My.W7 Then Notify(My.Application.LanguageHelper.NoDefResExplorer.Replace("<br>", vbCrLf), My.Resources.notify_warning, 3500)
        End If

    End Sub

    Private Sub XenonButton4_MouseEnter(sender As Object, e As EventArgs) Handles apply_btn.MouseEnter
        If My.Application._Settings.AutoRestartExplorer Then
            status_lbl.Text = My.Application.LanguageHelper.X22
            status_lbl.ForeColor = Color.Gold
        End If
    End Sub

    Private Sub XenonButton4_MouseLeave(sender As Object, e As EventArgs) Handles apply_btn.MouseLeave
        If My.Application._Settings.AutoRestartExplorer Then
            status_lbl.Text = ""
            status_lbl.ForeColor = If(GetDarkMode(), Color.White, Color.Black)
        End If
    End Sub

    Private Sub XenonGroupBox12_Click(sender As Object, e As EventArgs) Handles TaskbarBackground_Picker.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                CP.Taskbar_Background = sender.BackColor
                ApplyLivePreviewFromCP(CP)
            End If
            Exit Sub
        End If

        Dim CList As New List(Of Control)
        Dim C As Color

        CList.Add(sender)

        If PreviewConfig = WinVer.Eleven Then
            CList.Add(taskbar)
            Dim _Conditions As New Conditions With {.AppBackgroundOnly = True}
            C = ColorPickerDlg.Pick(CList, _Conditions)
        Else
            Dim _Conditions As New Conditions

            Select Case Not [CP].WinMode_Light
                Case True

                    If [CP].Transparency Then
                        CList.Add(taskbar)
                    End If

                Case False

                    If [CP].Transparency Then
                        CList.Add(taskbar)

                        If [CP].ApplyAccentonTaskbar <> ApplyAccentonTaskbar_Level.Taskbar_Start_AC Then
                            CList.Add(ActionCenter) ''ActionCenterLinks
                            _Conditions.ActionCenterLink = True
                        End If

                    Else
                        If [CP].ApplyAccentonTaskbar <> ApplyAccentonTaskbar_Level.Taskbar_Start_AC Then
                            CList.Add(ActionCenter) ''ActionCenterLinks
                            _Conditions.ActionCenterLink = True

                        End If
                    End If
            End Select

            C = ColorPickerDlg.Pick(CList, _Conditions)
        End If

        CP.Taskbar_Background = Color.FromArgb(255, C)
        ApplyLivePreviewFromCP(CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()
    End Sub

    Private Sub StartAccent_picker_Click(sender As Object, e As EventArgs) Handles StartAccent_picker.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                CP.StartMenu_Accent = sender.BackColor
                ApplyLivePreviewFromCP(CP)
            End If
            Exit Sub
        End If


        Dim CList As New List(Of Control) From {sender}

        If PreviewConfig = WinVer.Eleven Then
            CList.Add(start)
        Else

        End If

        Dim C As Color = ColorPickerDlg.Pick(CList)
        CP.StartMenu_Accent = Color.FromArgb(255, C)
        ApplyLivePreviewFromCP(CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()

    End Sub

    Private Sub StartButtonHover_picker_Click(sender As Object, e As EventArgs) Handles StartButtonHover_picker.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                CP.StartButton_Hover = sender.BackColor
                ApplyLivePreviewFromCP(CP)
            End If
            Exit Sub
        End If


        Dim CList As New List(Of Control) From {sender}

        If PreviewConfig = WinVer.Ten Then
            'CList.Add(taskbar) 'Start Icon Hover
        End If

        Dim C As Color = ColorPickerDlg.Pick(CList)
        CP.StartButton_Hover = Color.FromArgb(255, C)
        ApplyLivePreviewFromCP(CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()

    End Sub

    Private Sub StartBackgroundAndTaskbarButton_picker_Click(sender As Object, e As EventArgs) Handles StartBackgroundAndTaskbarButton_picker.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                CP.StartMenuBackground_ActiveTaskbarButton = sender.BackColor
                ApplyLivePreviewFromCP(CP)
            End If
            Exit Sub
        End If


        Dim CList As New List(Of Control) From {sender}

        Dim C As Color

        If PreviewConfig = WinVer.Eleven Then

            If Not Transparency_Toggle.Checked Then
                CList.Add(taskbar)
                CList.Add(start)
                CList.Add(ActionCenter)
            End If

            If CP.WinMode_Light Then
                CList.Add(start)
                CList.Add(ActionCenter)
                CList.Add(taskbar)

                Dim _Conditions As New Conditions With {
                    .AppUnderlineOnly = True,
                     .StartSearchOnly = True,
                     .ActionCenterBtn = True
 }

                C = ColorPickerDlg.Pick(CList, _Conditions)
            Else
                Dim _Conditions As New Conditions With {.StartColorOnly = True}
                C = ColorPickerDlg.Pick(CList, _Conditions)
            End If

        Else
            Dim _Conditions As New Conditions

            Select Case Not [CP].WinMode_Light
                Case True

                    If [CP].Transparency Then
                        CList.Add(start)
                        CList.Add(ActionCenter)
                    Else
                        CList.Add(start)
                        CList.Add(ActionCenter)
                        CList.Add(taskbar)  'AppBackground
                        _Conditions.AppBackgroundOnly = True
                    End If

                Case False
                    If [CP].Transparency Then
                        CList.Add(start)
                        CList.Add(ActionCenter)
                    Else
                        If [CP].ApplyAccentonTaskbar = ApplyAccentonTaskbar_Level.None Then
                            CList.Add(start)
                            CList.Add(ActionCenter)
                        ElseIf [CP].ApplyAccentonTaskbar = ApplyAccentonTaskbar_Level.Taskbar Then
                            CList.Add(start)
                            CList.Add(ActionCenter)
                            CList.Add(taskbar)  'Start Button Hover
                            _Conditions.StartColorOnly = True
                        Else
                            CList.Add(start)
                            CList.Add(ActionCenter)
                            CList.Add(taskbar)  'AppBackground
                            _Conditions.AppBackgroundOnly = True
                        End If
                    End If
            End Select

            C = ColorPickerDlg.Pick(CList, _Conditions)
        End If

        CP.StartMenuBackground_ActiveTaskbarButton = Color.FromArgb(255, C)
        ApplyLivePreviewFromCP(CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()
    End Sub


    Dim wpth_or_wpsf As Boolean = True
    Dim DragAccepted As Boolean

    Private Sub Me_DragEnter(ByVal sender As Object, ByVal e As DragEventArgs) Handles Me.DragEnter, previewContainer.DragEnter, pnl_preview.DragEnter,
        PaletteContainer_W1x.DragEnter, XenonGroupBox5.DragEnter, XenonGroupBox1.DragEnter, MainToolbar.DragEnter, XenonGroupBox13.DragEnter

        Dim files() As String = e.Data.GetData(DataFormats.FileDrop)

        If My.Computer.FileSystem.GetFileInfo(files(0)).Extension.ToLower = ".wpth" Then
            wpth_or_wpsf = True
            e.Effect = DragDropEffects.Copy

            If My.Application._Settings.DragAndDropPreview Then
                DragAccepted = True
                CP_BeforeDragAndDrop = CP
                dragPreviewer.Location = New Point(e.X + 15, e.Y + 15)
                dragPreviewer.File = files(0)
                dragPreviewer.Show()
            End If
        ElseIf My.Computer.FileSystem.GetFileInfo(files(0)).Extension.ToLower = ".wpsf" Then
            wpth_or_wpsf = False
            DragAccepted = True
            e.Effect = DragDropEffects.Copy
        Else
            DragAccepted = False
            e.Effect = DragDropEffects.None
        End If

    End Sub

    Private Sub MainFrm_DragLeave(sender As Object, e As EventArgs) Handles Me.DragLeave
        If DragAccepted Then
            If My.Application._Settings.DragAndDropPreview Then dragPreviewer.Close()
            CP = CP_BeforeDragAndDrop
            ApplyCPValues(CP_BeforeDragAndDrop)
            ApplyLivePreviewFromCP(CP_BeforeDragAndDrop)
        End If
    End Sub

    Private Sub MainFrm_DragOver(sender As Object, e As DragEventArgs) Handles Me.DragOver, previewContainer.DragOver, pnl_preview.DragOver,
        PaletteContainer_W1x.DragOver, XenonGroupBox5.DragOver, XenonGroupBox1.DragOver, MainToolbar.DragOver, XenonGroupBox13.DragOver
        If DragAccepted And My.Application._Settings.DragAndDropPreview Then dragPreviewer.Location = New Point(e.X + 15, e.Y + 15)
    End Sub

    Private Sub MainFrm_DragDrop(sender As Object, e As DragEventArgs) Handles Me.DragDrop, previewContainer.DragDrop, pnl_preview.DragDrop,
        PaletteContainer_W1x.DragDrop, XenonGroupBox5.DragDrop, XenonGroupBox1.DragDrop, MainToolbar.DragDrop, XenonGroupBox13.DragDrop

        If DragAccepted Then
            Dim files() As String = e.Data.GetData(DataFormats.FileDrop)

            If wpth_or_wpsf Then
                If My.Application._Settings.DragAndDropPreview Then dragPreviewer.Close()

                If Not CP.Equals(CP_Original) Then

                    Select Case ComplexSave.ShowDialog
                        Case DialogResult.Yes

                            Dim r As String() = My.Application.ComplexSaveResult.Split(".")
                            Dim r1 As String = r(0)
                            Dim r2 As String = r(1)

                            Select Case r1
                                Case 0              '' Save
                                    If IO.File.Exists(SaveFileDialog1.FileName) Then
                                        CP.Save(CP.SavingMode.File, SaveFileDialog1.FileName)
                                        CP_Original = New CP(Mode.File, SaveFileDialog1.FileName)
                                    Else
                                        If SaveFileDialog1.ShowDialog = DialogResult.OK Then
                                            CP.Save(CP.SavingMode.File, SaveFileDialog1.FileName)
                                            CP_Original = New CP(Mode.File, SaveFileDialog1.FileName)
                                        Else
                                            '''''''' If My.Application._Settings.DragPreview then ReleaseBlur()
                                            Exit Sub
                                        End If
                                    End If
                                Case 1              '' Save As
                                    If SaveFileDialog1.ShowDialog = DialogResult.OK Then
                                        CP.Save(CP.SavingMode.File, SaveFileDialog1.FileName)
                                        CP_Original = New CP(Mode.File, SaveFileDialog1.FileName)
                                    Else
                                        '''''''' If My.Application._Settings.DragPreview then ReleaseBlur()
                                        Exit Sub
                                    End If
                            End Select

                            Select Case r2
                                Case 1      '' Apply   ' Case 0= Don't Apply
                                    CP.Save(CP.SavingMode.Registry)
                                    If My.Application._Settings.AutoRestartExplorer Then RestartExplorer()
                            End Select

                        Case DialogResult.No

                        Case DialogResult.Cancel
                            Exit Sub
                    End Select

                End If

                CP = New CP(CP.Mode.File, files(0))
                ApplyCPValues(CP)
                ApplyLivePreviewFromCP(CP)
            Else
                SettingsX._External = True
                SettingsX._File = files(0)
                SettingsX.ShowDialog()
            End If
        End If

    End Sub

    Private Sub XenonButton7_Click(sender As Object, e As EventArgs) Handles XenonButton7.Click
        If Not IO.File.Exists(SaveFileDialog1.FileName) Then
            If SaveFileDialog1.ShowDialog = DialogResult.OK Then
                CP.Save(CP.SavingMode.File, SaveFileDialog1.FileNames(0))
            End If
        Else
            CP.Save(CP.SavingMode.File, SaveFileDialog1.FileNames(0))
        End If
    End Sub

    Private Sub XenonButton2_Click(sender As Object, e As EventArgs) Handles XenonButton2.Click
        If OpenFileDialog1.ShowDialog = DialogResult.OK Then

            Select Case ComplexSave.ShowDialog
                Case DialogResult.Yes

                    Dim r As String() = My.Application.ComplexSaveResult.Split(".")
                    Dim r1 As String = r(0)
                    Dim r2 As String = r(1)

                    Select Case r1
                        Case 0              '' Save
                            If IO.File.Exists(SaveFileDialog1.FileName) Then
                                CP.Save(CP.SavingMode.File, SaveFileDialog1.FileName)
                                CP_Original = New CP(Mode.File, SaveFileDialog1.FileName)
                            Else
                                If SaveFileDialog1.ShowDialog = DialogResult.OK Then
                                    CP.Save(CP.SavingMode.File, SaveFileDialog1.FileName)
                                    CP_Original = New CP(Mode.File, SaveFileDialog1.FileName)
                                Else
                                    Exit Sub
                                End If
                            End If
                        Case 1              '' Save As
                            If SaveFileDialog1.ShowDialog = DialogResult.OK Then
                                CP.Save(CP.SavingMode.File, SaveFileDialog1.FileName)
                                CP_Original = New CP(Mode.File, SaveFileDialog1.FileName)
                            Else
                                Exit Sub
                            End If
                    End Select

                    Select Case r2
                        Case 1      '' Apply   ' Case 0= Don't Apply
                            CP.Save(CP.SavingMode.Registry)
                            If My.Application._Settings.AutoRestartExplorer Then RestartExplorer()
                    End Select

                Case DialogResult.No

                Case DialogResult.Cancel
                    Exit Sub
            End Select

            SaveFileDialog1.FileName = OpenFileDialog1.FileName
            CP = New CP(CP.Mode.File, OpenFileDialog1.FileName)
            ApplyCPValues(CP)
            ApplyLivePreviewFromCP(CP)
        End If
    End Sub

    Private Sub XenonButton9_Click(sender As Object, e As EventArgs) Handles XenonButton9.Click
        If SaveFileDialog1.ShowDialog = DialogResult.OK Then
            CP.Save(CP.SavingMode.File, SaveFileDialog1.FileNames(0))
        End If
    End Sub

    Private Sub XenonButton3_Click(sender As Object, e As EventArgs) Handles XenonButton3.Click
        ContextMenuStrip1.Show(sender, New Point(2, sender.height))
    End Sub

    Private Sub FromCurrentPaletteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FromCurrentPaletteToolStripMenuItem.Click

        If Not CP.Equals(CP_Original) Then

            Select Case ComplexSave.ShowDialog
                Case DialogResult.Yes

                    Dim r As String() = My.Application.ComplexSaveResult.Split(".")
                    Dim r1 As String = r(0)
                    Dim r2 As String = r(1)

                    Select Case r1
                        Case 0              '' Save
                            If IO.File.Exists(SaveFileDialog1.FileName) Then
                                CP.Save(CP.SavingMode.File, SaveFileDialog1.FileName)
                                CP_Original = New CP(Mode.File, SaveFileDialog1.FileName)
                            Else
                                If SaveFileDialog1.ShowDialog = DialogResult.OK Then
                                    CP.Save(CP.SavingMode.File, SaveFileDialog1.FileName)
                                    CP_Original = New CP(Mode.File, SaveFileDialog1.FileName)
                                Else
                                    Exit Sub
                                End If
                            End If
                        Case 1              '' Save As
                            If SaveFileDialog1.ShowDialog = DialogResult.OK Then
                                CP.Save(CP.SavingMode.File, SaveFileDialog1.FileName)
                                CP_Original = New CP(Mode.File, SaveFileDialog1.FileName)
                            Else
                                Exit Sub
                            End If
                    End Select

                    Select Case r2
                        Case 1      '' Apply   ' Case 0= Don't Apply
                            CP.Save(CP.SavingMode.Registry)
                            If My.Application._Settings.AutoRestartExplorer Then RestartExplorer()
                    End Select

                Case DialogResult.No

                Case DialogResult.Cancel
                    Exit Sub
            End Select

        End If

        CP = New CP(CP.Mode.Registry)
        CP_Original = New CP(CP.Mode.Registry)
        OpenFileDialog1.FileName = Nothing
        SaveFileDialog1.FileName = Nothing
        ApplyCPValues(CP)
        ApplyLivePreviewFromCP(CP)
    End Sub

    Private Sub ToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem2.Click
        If Not CP.Equals(CP_Original) Then
            Select Case ComplexSave.ShowDialog
                Case DialogResult.Yes

                    Dim r As String() = My.Application.ComplexSaveResult.Split(".")
                    Dim r1 As String = r(0)
                    Dim r2 As String = r(1)

                    Select Case r1
                        Case 0              '' Save
                            If IO.File.Exists(SaveFileDialog1.FileName) Then
                                CP.Save(CP.SavingMode.File, SaveFileDialog1.FileName)
                                CP_Original = New CP(Mode.File, SaveFileDialog1.FileName)
                            Else
                                If SaveFileDialog1.ShowDialog = DialogResult.OK Then
                                    CP.Save(CP.SavingMode.File, SaveFileDialog1.FileName)
                                    CP_Original = New CP(Mode.File, SaveFileDialog1.FileName)
                                Else
                                    Exit Sub
                                End If
                            End If
                        Case 1              '' Save As
                            If SaveFileDialog1.ShowDialog = DialogResult.OK Then
                                CP.Save(CP.SavingMode.File, SaveFileDialog1.FileName)
                                CP_Original = New CP(Mode.File, SaveFileDialog1.FileName)
                            Else
                                Exit Sub
                            End If
                    End Select

                    Select Case r2
                        Case 1      '' Apply   ' Case 0= Don't Apply
                            CP.Save(CP.SavingMode.Registry)
                            If My.Application._Settings.AutoRestartExplorer Then RestartExplorer()
                    End Select

                Case DialogResult.No

                Case DialogResult.Cancel
                    Exit Sub
            End Select
        End If

        CP = New CP(CP.Mode.Init)
        CP_Original = New CP(CP.Mode.Init)
        OpenFileDialog1.FileName = Nothing
        SaveFileDialog1.FileName = Nothing
        ApplyCPValues(CP)
        ApplyLivePreviewFromCP(CP)
    End Sub

    Private Sub XenonButton10_Click(sender As Object, e As EventArgs) Handles XenonButton10.Click, author_lbl.DoubleClick, themename_lbl.DoubleClick
        EditInfo.Load_Info(CP)
        ApplyCPValues(CP)
    End Sub

    Private Sub XenonButton12_Click(sender As Object, e As EventArgs) Handles XenonButton12.Click
        About.ShowDialog()
    End Sub

    Private Sub XenonButton5_Click(sender As Object, e As EventArgs) Handles XenonButton5.Click
        Updates.ShowDialog()
    End Sub

    Private Sub XenonButton11_Click(sender As Object, e As EventArgs) Handles XenonButton11.Click
        SettingsX.ShowDialog()
    End Sub

    Private Sub XenonButton4_Click_1(sender As Object, e As EventArgs) Handles XenonButton4.Click
        Win32UI.Show()
    End Sub

    Private Sub XenonButton6_Click(sender As Object, e As EventArgs) Handles XenonButton6.Click
        Whatsnew.ShowDialog()
    End Sub

    Private Sub XenonButton16_Click(sender As Object, e As EventArgs) Handles XenonButton16.Click
        If PreviewConfig = WinVer.Eleven Or PreviewConfig = WinVer.Ten Then
            LogonUI.ShowDialog()
        Else
            LogonUI7.ShowDialog()
        End If
    End Sub

    Private Sub XenonButton15_Click(sender As Object, e As EventArgs) Handles XenonButton15.Click
        My.Application.Wallpaper = ResizeImage(My.Application.GetCurrentWallpaper(), 528, 297)
        pnl_preview.BackgroundImage = My.Application.Wallpaper
        ApplyLivePreviewFromCP(CP)
        ApplyCPValues(CP)
    End Sub

    Private Sub XenonButton13_Click(sender As Object, e As EventArgs) Handles XenonButton13.Click
        Me.Close()
    End Sub

    Private Sub XenonButton1_Click_1(sender As Object, e As EventArgs) Handles XenonButton1.Click
        If SaveFileDialog2.ShowDialog = DialogResult.OK Then
            GetControlImage(pnl_preview).Save(SaveFileDialog2.FileName)
        End If
    End Sub

    Private Sub XenonButton8_Click_1(sender As Object, e As EventArgs) Handles XenonButton8.Click
        MsgBox(My.Application.LanguageHelper.X23, MsgBoxStyle.Information + My.Application.MsgboxRt)
    End Sub

    Private Sub XenonButton17_Click(sender As Object, e As EventArgs) Handles XenonButton17.Click
        If Not CP.Equals(CP_Original) Then
            Select Case ComplexSave.ShowDialog
                Case DialogResult.Yes

                    Dim r As String() = My.Application.ComplexSaveResult.Split(".")
                    Dim r1 As String = r(0)
                    Dim r2 As String = r(1)

                    Select Case r1
                        Case 0              '' Save
                            If IO.File.Exists(SaveFileDialog1.FileName) Then
                                CP.Save(CP.SavingMode.File, SaveFileDialog1.FileName)
                                CP_Original = New CP(Mode.File, SaveFileDialog1.FileName)
                            Else
                                If SaveFileDialog1.ShowDialog = DialogResult.OK Then
                                    CP.Save(CP.SavingMode.File, SaveFileDialog1.FileName)
                                    CP_Original = New CP(Mode.File, SaveFileDialog1.FileName)
                                End If
                            End If
                        Case 1              '' Save As
                            If SaveFileDialog1.ShowDialog = DialogResult.OK Then
                                CP.Save(CP.SavingMode.File, SaveFileDialog1.FileName)
                                CP_Original = New CP(Mode.File, SaveFileDialog1.FileName)

                            End If
                    End Select

                    Select Case r2
                        Case 1      '' Apply   ' Case 0= Don't Apply
                            CP.Save(CP.SavingMode.Registry)
                            If My.Application._Settings.AutoRestartExplorer Then RestartExplorer()
                    End Select

                Case DialogResult.No

                Case DialogResult.Cancel
                    Exit Sub
            End Select
        End If

        CP = CP_Original
        ApplyCPValues(CP)
        ApplyLivePreviewFromCP(CP)
    End Sub

    Private Sub XenonButton18_Click(sender As Object, e As EventArgs) Handles XenonButton18.Click
        If Not CP.Equals(CP_Original) Then
            Select Case ComplexSave.ShowDialog
                Case DialogResult.Yes

                    Dim r As String() = My.Application.ComplexSaveResult.Split(".")
                    Dim r1 As String = r(0)
                    Dim r2 As String = r(1)

                    Select Case r1
                        Case 0              '' Save
                            If IO.File.Exists(SaveFileDialog1.FileName) Then
                                CP.Save(CP.SavingMode.File, SaveFileDialog1.FileName)
                                CP_Original = New CP(Mode.File, SaveFileDialog1.FileName)
                            Else
                                If SaveFileDialog1.ShowDialog = DialogResult.OK Then
                                    CP.Save(CP.SavingMode.File, SaveFileDialog1.FileName)
                                    CP_Original = New CP(Mode.File, SaveFileDialog1.FileName)
                                End If
                            End If
                        Case 1              '' Save As
                            If SaveFileDialog1.ShowDialog = DialogResult.OK Then
                                CP.Save(CP.SavingMode.File, SaveFileDialog1.FileName)
                                CP_Original = New CP(Mode.File, SaveFileDialog1.FileName)

                            End If
                    End Select

                    Select Case r2
                        Case 1      '' Apply   ' Case 0= Don't Apply
                            CP.Save(CP.SavingMode.Registry)
                            If My.Application._Settings.AutoRestartExplorer Then RestartExplorer()
                    End Select

                Case DialogResult.No

                Case DialogResult.Cancel
                    Exit Sub
            End Select
        End If

        CP = CP_FirstTime
        ApplyCPValues(CP)
        ApplyLivePreviewFromCP(CP)
    End Sub

    Private Sub XenonButton19_Click(sender As Object, e As EventArgs) Handles XenonButton19.Click
        RestartExplorer()
    End Sub

    Private Sub XenonButton20_Click(sender As Object, e As EventArgs) Handles XenonButton20.Click
        If Not CP.Equals(CP_Original) Then
            Select Case ComplexSave.ShowDialog
                Case DialogResult.Yes

                    Dim r As String() = My.Application.ComplexSaveResult.Split(".")
                    Dim r1 As String = r(0)
                    Dim r2 As String = r(1)

                    Select Case r1
                        Case 0              '' Save
                            If IO.File.Exists(SaveFileDialog1.FileName) Then
                                CP.Save(CP.SavingMode.File, SaveFileDialog1.FileName)
                                CP_Original = New CP(Mode.File, SaveFileDialog1.FileName)
                            Else
                                If SaveFileDialog1.ShowDialog = DialogResult.OK Then
                                    CP.Save(CP.SavingMode.File, SaveFileDialog1.FileName)
                                    CP_Original = New CP(Mode.File, SaveFileDialog1.FileName)
                                Else
                                    Exit Sub
                                End If
                            End If
                        Case 1              '' Save As
                            If SaveFileDialog1.ShowDialog = DialogResult.OK Then
                                CP.Save(CP.SavingMode.File, SaveFileDialog1.FileName)
                                CP_Original = New CP(Mode.File, SaveFileDialog1.FileName)
                            Else
                                Exit Sub
                            End If
                    End Select

                    Select Case r2
                        Case 1      '' Apply   ' Case 0= Don't Apply
                            CP.Save(CP.SavingMode.Registry)
                            If My.Application._Settings.AutoRestartExplorer Then RestartExplorer()
                    End Select

                Case DialogResult.No

                Case DialogResult.Cancel
                    Exit Sub
            End Select
        End If

        Dim Def As New CP_Defaults

        If My.W11 Then
            CP = Def.Default_Windows11
        ElseIf My.W10 Then
            CP = Def.Default_Windows10
        ElseIf My.W8 Then
            CP = Def.Default_Windows8
        ElseIf My.W7 Then
            CP = Def.Default_Windows7
        Else
            CP = Def.Default_Windows11
        End If

        OpenFileDialog1.FileName = Nothing
        SaveFileDialog1.FileName = Nothing
        ApplyCPValues(CP)
        ApplyLivePreviewFromCP(CP)
    End Sub

    Private Sub XenonButton21_Click(sender As Object, e As EventArgs) Handles XenonButton21.Click
        CursorsStudio.ShowDialog()
    End Sub

    Private Sub Aero_ColorizationColor_pick_Click(sender As Object, e As EventArgs) Handles Aero_ColorizationColor_pick.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                CP.Aero_ColorizationColor = sender.BackColor
                ApplyLivePreviewFromCP(CP)
            End If
            Exit Sub
        End If

        Dim CList As New List(Of Control) From {
            sender,
            start,
            taskbar,
            XenonWindow1,
            XenonWindow2
        }

        Dim _Conditions As New Conditions With {.Win7 = True, .Color1 = True, .BackColor1 = True, .Win7LivePreview_Colorization = True}

        Dim C As Color = ColorPickerDlg.Pick(CList, _Conditions)

        CP.Aero_ColorizationColor = Color.FromArgb(255, C)

        ApplyLivePreviewFromCP(CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()
    End Sub

    Private Sub Aero_ColorizationAfterglow_pick_Click(sender As Object, e As EventArgs) Handles Aero_ColorizationAfterglow_pick.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                CP.Aero_ColorizationAfterglow = sender.BackColor
                ApplyLivePreviewFromCP(CP)
            End If
            Exit Sub
        End If

        Dim CList As New List(Of Control) From {
            sender,
            start,
            taskbar,
            XenonWindow1,
            XenonWindow2
        }

        Dim _Conditions As New Conditions With {.Win7 = True, .Color2 = True, .BackColor2 = True, .Win7LivePreview_AfterGlow = True}

        Dim C As Color = ColorPickerDlg.Pick(CList, _Conditions)

        CP.Aero_ColorizationAfterglow = Color.FromArgb(255, C)

        ApplyLivePreviewFromCP(CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()
    End Sub

    Private Sub Aero_EnableAeroPeek_toggle_CheckedChanged(sender As Object, e As EventArgs) Handles Aero_EnableAeroPeek_toggle.CheckedChanged
        If _Shown Then CP.Aero_EnableAeroPeek = Aero_EnableAeroPeek_toggle.Checked
    End Sub

    Private Sub Aero_AlwaysHibernateThumbnails_Toggle_CheckedChanged(sender As Object, e As EventArgs) Handles Aero_AlwaysHibernateThumbnails_Toggle.CheckedChanged
        If _Shown Then CP.Aero_AlwaysHibernateThumbnails = Aero_AlwaysHibernateThumbnails_Toggle.Checked
    End Sub

    Private Sub Aero_ColorizationColorBalance_txt_TextChanged(sender As Object) Handles Aero_ColorizationColorBalance_bar.Scroll
        If _Shown Then
            CP.Aero_ColorizationColorBalance = Aero_ColorizationColorBalance_bar.Value
            ApplyLivePreviewFromCP(CP)
        End If
    End Sub

    Private Sub Aero_ColorizationBlurBalance_TextChanged(sender As Object) Handles Aero_ColorizationBlurBalance_bar.Scroll
        If _Shown Then
            CP.Aero_ColorizationBlurBalance = Aero_ColorizationBlurBalance_bar.Value
            ApplyLivePreviewFromCP(CP)
        End If
    End Sub

    Private Sub Aero_ColorizationGlassReflectionIntensity_txt_TextChanged(sender As Object) Handles Aero_ColorizationGlassReflectionIntensity_bar.Scroll
        If _Shown Then
            CP.Aero_ColorizationGlassReflectionIntensity = Aero_ColorizationGlassReflectionIntensity_bar.Value
            ApplyLivePreviewFromCP(CP)
        End If
    End Sub

    Private Sub theme_classic_CheckedChanged(sender As Object) Handles theme_classic.CheckedChanged
        If theme_classic.Checked Then
            CP.Aero_Theme = CP.AeroTheme.Classic
            ApplyLivePreviewFromCP(CP)
            Notify(My.Application.LanguageHelper.CP_ClassicThemeEditable, My.Resources.notify_warning, 5000)
        End If

    End Sub

    Private Sub theme_basic_CheckedChanged(sender As Object) Handles theme_basic.CheckedChanged
        If theme_basic.Checked Then
            CP.Aero_Theme = CP.AeroTheme.Basic
            ApplyLivePreviewFromCP(CP)
        End If
    End Sub

    Private Sub theme_aeroopaque_CheckedChanged(sender As Object) Handles theme_aeroopaque.CheckedChanged
        If theme_aeroopaque.Checked Then
            CP.Aero_Theme = CP.AeroTheme.AeroOpaque
            ApplyLivePreviewFromCP(CP)
        End If
    End Sub

    Private Sub theme_aero_CheckedChanged(sender As Object) Handles theme_aero.CheckedChanged
        If theme_aero.Checked Then
            CP.Aero_Theme = CP.AeroTheme.Aero
            ApplyLivePreviewFromCP(CP)
        End If
    End Sub

    Private Sub Aero_ColorizationAfterglowBalance_txt_TextChanged(sender As Object) Handles Aero_ColorizationAfterglowBalance_bar.Scroll
        If _Shown Then
            CP.Aero_ColorizationAfterglowBalance = Aero_ColorizationAfterglowBalance_bar.Value
            ApplyLivePreviewFromCP(CP)
        End If
    End Sub

    Private Sub ColorizationColor8_pick_Click(sender As Object, e As EventArgs) Handles ColorizationColor8_pick.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                CP.Aero_ColorizationColor = sender.BackColor
                ApplyLivePreviewFromCP(CP)
            End If
            Exit Sub
        End If

        Dim CList As New List(Of Control) From {
           sender,
           start,
           taskbar,
           XenonWindow1,
           XenonWindow2
       }

        Dim _Conditions As New Conditions With {.Window_ActiveTitlebar = True, .Window_InactiveTitlebar = True, .Win7LivePreview_Colorization = True}

        Dim C As Color = ColorPickerDlg.Pick(CList, _Conditions)

        CP.Aero_ColorizationColor = Color.FromArgb(255, C)

        ApplyLivePreviewFromCP(CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()
    End Sub

    Private Sub ColorizationBalance8_track_Scroll(sender As Object) Handles ColorizationBalance8_track.Scroll
        If _Shown Then
            CP.Aero_ColorizationColorBalance = ColorizationBalance8_track.Value
            ApplyLivePreviewFromCP(CP)
        End If
    End Sub

    Private Sub start8_pick_Click(sender As Object, e As EventArgs) Handles start8_pick.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                CP.Metro_StartColor = sender.BackColor
                ApplyLivePreviewFromCP(CP)
            End If
            Exit Sub
        End If

        Dim CList As New List(Of Control) From {sender}

        Dim C As Color = ColorPickerDlg.Pick(CList)

        CP.Metro_StartColor = Color.FromArgb(255, C)

        ApplyLivePreviewFromCP(CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()
    End Sub

    Private Sub accent8_pick_Click(sender As Object, e As EventArgs) Handles accent8_pick.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                CP.Metro_AccentColor = sender.BackColor
                ApplyLivePreviewFromCP(CP)
            End If
            Exit Sub
        End If

        Dim CList As New List(Of Control) From {sender}

        Dim C As Color = ColorPickerDlg.Pick(CList)

        CP.Metro_AccentColor = Color.FromArgb(255, C)

        ApplyLivePreviewFromCP(CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()
    End Sub

    Private Sub personalcls8_background_pick_Click(sender As Object, e As EventArgs) Handles personalcls8_background_pick.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                CP.Metro_PersonalColors_Background = sender.BackColor
                ApplyLivePreviewFromCP(CP)
            End If
            Exit Sub
        End If

        Dim CList As New List(Of Control) From {sender}

        Dim C As Color = ColorPickerDlg.Pick(CList)

        CP.Metro_PersonalColors_Background = Color.FromArgb(255, C)

        ApplyLivePreviewFromCP(CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()
    End Sub

    Private Sub personalcolor8accent_pick_Click(sender As Object, e As EventArgs) Handles personalcolor8accent_pick.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                CP.Metro_PersonalColors_Accent = sender.BackColor
                ApplyLivePreviewFromCP(CP)
            End If
            Exit Sub
        End If

        Dim CList As New List(Of Control) From {sender}

        Dim C As Color = ColorPickerDlg.Pick(CList)

        CP.Metro_PersonalColors_Accent = Color.FromArgb(255, C)

        ApplyLivePreviewFromCP(CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()
    End Sub

    Private Sub XenonRadioImage1_CheckedChanged(sender As Object) Handles XenonRadioImage1.CheckedChanged
        If XenonRadioImage1.Checked Then
            CP.Metro_Theme = CP.AeroTheme.Aero
            ApplyLivePreviewFromCP(CP)
        End If
    End Sub

    Private Sub XenonRadioImage2_CheckedChanged(sender As Object) Handles XenonRadioImage2.CheckedChanged
        If XenonRadioImage2.Checked Then
            CP.Metro_Theme = CP.AeroTheme.AeroLite
            ApplyLivePreviewFromCP(CP)
        End If
    End Sub

    Private Sub XenonButton14_Click(sender As Object, e As EventArgs)
        RefreshDWM(CP)
    End Sub

    Private Sub XenonButton14_Click_1(sender As Object, e As EventArgs) Handles XenonButton14.Click
        Start8Selector.ShowDialog()
        ApplyLivePreviewFromCP(CP)
    End Sub

    Private Sub XenonButton22_Click(sender As Object, e As EventArgs) Handles XenonButton22.Click
        LogonUI8Colors.ShowDialog()
        ApplyLivePreviewFromCP(CP)
    End Sub

    Private Sub XenonButton23_Click(sender As Object, e As EventArgs) Handles XenonButton23.Click
        If XenonButton23.Text.ToLower = My.Application.LanguageHelper.Hide.ToLower Then
            pnl_preview.Visible = False
            XenonButton23.Text = My.Application.LanguageHelper.Show
        Else
            pnl_preview.Visible = True
            XenonButton23.Text = My.Application.LanguageHelper.Hide
        End If
    End Sub

    Private Sub BackgroundWorker2_DoWork(sender As Object, e As DoWorkEventArgs)
        ApplyingTheme.Show()
    End Sub

    Private Sub XenonButton24_Click(sender As Object, e As EventArgs) Handles XenonButton24.Click
        TerminalsDashboard.ShowDialog()
    End Sub

    Private Sub XenonGroupBox16_Click(sender As Object, e As EventArgs) Handles UWP_Undefined_pick.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            CP.UWP_Undefined = SubMenu.ShowMenu(sender)
            Exit Sub
        End If

        Dim CList As New List(Of Control) From {sender}
        CList.Add(sender)
        Dim C As Color = ColorPickerDlg.Pick(CList)

        CP.UWP_Undefined = Color.FromArgb(255, C)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()
    End Sub


    Private Sub MainFrm_ResizeBegin(sender As Object, e As EventArgs) Handles Me.ResizeBegin
        pnl_preview.Visible = False
    End Sub

    Private Sub MainFrm_ResizeEnd(sender As Object, e As EventArgs) Handles Me.ResizeEnd
        pnl_preview.Visible = True
    End Sub

    Private Sub XenonButton25_Click(sender As Object, e As EventArgs) Handles XenonButton25.Click
        MsgBox(My.Application.LanguageHelper.CP_AccentOnTaskbarTib, MsgBoxStyle.Information + My.Application.MsgboxRt)
    End Sub

    Private Sub XenonButton26_Click(sender As Object, e As EventArgs) Handles XenonButton26.Click
        Dim s As New StringBuilder
        s.Clear()
        s.AppendLine("Announcement (Temporary until mid-January 2023)")
        s.AppendLine(vbCrLf)
        s.AppendLine("- I'll pause working on WinPaletter because I will start final college exams in the next weeks for about 3 months and so I'll be so busy.")
        s.AppendLine("- I won't be able to work on new features until mid-January 2023.")
        s.AppendLine("- If you have an issue, post it in GitHub Issues, And when I have time to fix it, I'll fix and post an update.")
        s.AppendLine("- When I'm back, I will work on Windows Metrics, Mini-Store for Themes and other features/improvements. ")
        s.AppendLine(vbCrLf)
        s.AppendLine("- Thanks for your patience And for understanding this ... ")

        MsgBox(s.ToString, MsgBoxStyle.Information + My.Application.MsgboxRt)

    End Sub

#Region "Notifications Base"
    Sub Notify([Text] As String, [Icon] As Image, [Interval] As Integer)
        Dim NB As New XenonAlertBox With {
         .AlertStyle = XenonAlertBox.Style.Adaptive,
         .Text = Text,
         .Image = [Icon],
         .Size = New Size(NotificationsPanel.Width - 5, MeasureString([Text], .Font).Height + 15),
         .Left = 0
         }

        AddHandler NB.Click, AddressOf NB.Hide

        NotificationsList.Add(New Notifier(NB, [Interval]))
        NotificationsPanel.Controls.Add(NB)
        NotificationsPanel.BringToFront()
        NTimer.Enabled = True
        NTimer.Start()
    End Sub
    Private Sub NTimer_Tick(sender As Object, e As EventArgs) Handles NTimer.Tick

        Try
            For Each nx As Notifier In NotificationsList
                If nx.Interval - NTimer.Interval <= 0 Then
                    NotificationsPanel.Controls.Remove(nx.Control)
                    nx.Control.Dispose()
                    RemoveHandler nx.Control.Click, AddressOf nx.Control.Hide
                    NotificationsList.Remove(nx)
                Else
                    nx.Interval -= NTimer.Interval
                End If
            Next
        Catch
        End Try

        If NotificationsList.Count = 0 Then
            NTimer.Enabled = False
            NTimer.Stop()
        End If

    End Sub


    ReadOnly NotificationsList As New List(Of Notifier)
    Dim WithEvents NTimer As New Timer With {.Enabled = False, .Interval = 100}

#End Region

End Class

Public Class Notifier
    Public Sub New(Control As XenonAlertBox, Interval As Integer)
        Me.Control = Control
        Me.Interval = Interval
    End Sub
    Public Property Control As XenonAlertBox
    Public Property Interval As Integer
End Class
