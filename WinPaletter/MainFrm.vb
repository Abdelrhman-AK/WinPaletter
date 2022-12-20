Imports System.ComponentModel
Imports System.Net
Imports System.Reflection
Imports WinPaletter.CP
Imports WinPaletter.XenonCore

Public Class MainFrm
    Private _Shown As Boolean = False
    Public CP, CP_Original, CP_FirstTime, CP_BeforeDragAndDrop As CP
    Public PreviewConfig As WinVer = WinVer.Eleven
    Dim RaiseUpdate As Boolean = False
    Dim ver As String = ""
    Dim StableInt, BetaInt, UpdateChannel As Integer
    Dim ChannelFixer As Integer
    Public ShowWhatsNew As Boolean = False

    Public Shared Sub SetTreeViewTheme(ByVal treeHandle As IntPtr)
        NativeMethods.Uxtheme.SetWindowTheme(treeHandle, "explorer", Nothing)
    End Sub

#Region "CP Subs"
    Sub ApplyLivePreviewFromCP(ByVal [CP] As CP)
        Dim AnimX1 As Integer = 25
        Dim AnimX2 As Integer = 1

        XenonWindow1.Active = True
        XenonWindow2.Active = False

        Select Case PreviewConfig
            Case WinVer.Eleven
#Region "Win11"
                XenonWindow1.AccentColor_Enabled = [CP].Windows11.ApplyAccentonTitlebars
                XenonWindow2.AccentColor_Enabled = [CP].Windows11.ApplyAccentonTitlebars

                XenonWindow1.AccentColor_Active = [CP].Windows11.Titlebar_Active
                XenonWindow2.AccentColor_Active = [CP].Windows11.Titlebar_Active

                XenonWindow1.AccentColor_Inactive = [CP].Windows11.Titlebar_Inactive
                XenonWindow2.AccentColor_Inactive = [CP].Windows11.Titlebar_Inactive

                XenonWindow1.DarkMode = Not [CP].Windows11.AppMode_Light
                XenonWindow2.DarkMode = Not [CP].Windows11.AppMode_Light

                Visual.FadeColor(Label8, "Forecolor", Label8.ForeColor, If([CP].Windows11.AppMode_Light, Color.Black, Color.White), AnimX1, AnimX2)

                W11_lbl5.Text = My.Application.LanguageHelper.CP_11_Settings
                W11_lbl6.Text = My.Application.LanguageHelper.CP_11_SomePressedButtons
                W11_lbl7.Text = My.Application.LanguageHelper.CP_Undefined
                W11_lbl8.Text = My.Application.LanguageHelper.CP_Undefined
                W11_pic5.Image = My.Resources.Mini_SettingsIcons
                W11_pic6.Image = My.Resources.Mini_PressedButton
                W11_pic7.Image = My.Resources.Mini_Undefined
                W11_pic8.Image = My.Resources.Mini_Undefined
                W11_pic9.Image = My.Resources.Mini_Undefined

                Select Case Not [CP].Windows11.WinMode_Light
                    Case True   ''''''''''Dark
                        W11_lbl1.Text = My.Application.LanguageHelper.CP_11_StartMenu_Taskbar_AC
                        W11_lbl2.Text = My.Application.LanguageHelper.CP_11_ACHover_Links
                        W11_lbl3.Text = My.Application.LanguageHelper.CP_11_Lines_Toggles_Buttons
                        W11_lbl4.Text = My.Application.LanguageHelper.CP_Undefined

                        W11_pic1.Image = My.Resources.Mini_StartMenu_Taskbar_AC
                        W11_pic2.Image = My.Resources.Mini_ACHover_Links
                        W11_pic3.Image = My.Resources.Mini_Lines_Toggles_Buttons
                        W11_pic4.Image = My.Resources.Mini_Undefined


                    Case False   ''''''''''Light
                        W11_lbl1.Text = My.Application.LanguageHelper.CP_11_ACHover_Links
                        W11_lbl2.Text = My.Application.LanguageHelper.CP_11_StartMenu_AC
                        W11_lbl3.Text = My.Application.LanguageHelper.CP_11_Taskbar
                        W11_lbl4.Text = My.Application.LanguageHelper.CP_11_Lines_Toggles_Buttons

                        W11_pic1.Image = My.Resources.Mini_ACHover_Links
                        W11_pic2.Image = My.Resources.Mini_StartMenu_Taskbar_AC
                        W11_pic3.Image = My.Resources.Mini_Taskbar
                        W11_pic4.Image = My.Resources.Mini_Lines_Toggles_Buttons
                End Select

                start.DarkMode = Not [CP].Windows11.WinMode_Light
                taskbar.DarkMode = Not [CP].Windows11.WinMode_Light
                ActionCenter.DarkMode = Not [CP].Windows11.WinMode_Light

                taskbar.Transparency = [CP].Windows11.Transparency
                start.Transparency = [CP].Windows11.Transparency
                ActionCenter.Transparency = [CP].Windows11.Transparency

                start.Top = taskbar.Top - start.Height - 9

                Select Case Not [CP].Windows11.WinMode_Light
                    Case True   ''''''''''Dark
                        taskbar.BackColorAlpha = 130
                        start.BackColorAlpha = 130
                        ActionCenter.BackColorAlpha = 130

                        Select Case [CP].Windows11.ApplyAccentonTaskbar
                            Case ApplyAccentonTaskbar_Level.None
                                Visual.FadeColor(taskbar, "BackColor", taskbar.BackColor, Color.FromArgb(55, 55, 55), AnimX1, AnimX2)
                                Visual.FadeColor(start, "BackColor", start.BackColor, Color.FromArgb(40, 40, 40), AnimX1, AnimX2)
                                Visual.FadeColor(ActionCenter, "BackColor", ActionCenter.BackColor, Color.FromArgb(55, 55, 55), AnimX1, AnimX2)

                            Case ApplyAccentonTaskbar_Level.Taskbar_Start_AC
                                Visual.FadeColor(taskbar, "BackColor", taskbar.BackColor, Color.FromArgb(taskbar.BackColor.A, [CP].Windows11.Color_Index5), AnimX1, AnimX2)
                                Visual.FadeColor(start, "BackColor", start.BackColor, Color.FromArgb(start.BackColor.A, [CP].Windows11.Color_Index5), AnimX1, AnimX2)
                                Visual.FadeColor(ActionCenter, "BackColor", ActionCenter.BackColor, Color.FromArgb(ActionCenter.BackColor.A, [CP].Windows11.Color_Index5), AnimX1, AnimX2)

                            Case ApplyAccentonTaskbar_Level.Taskbar
                                Visual.FadeColor(taskbar, "BackColor", taskbar.BackColor, Color.FromArgb(taskbar.BackColor.A, [CP].Windows11.Color_Index5), AnimX1, AnimX2)
                                Visual.FadeColor(start, "BackColor", start.BackColor, Color.FromArgb(40, 40, 40), AnimX1, AnimX2)
                                Visual.FadeColor(ActionCenter, "BackColor", ActionCenter.BackColor, Color.FromArgb(55, 55, 55), AnimX1, AnimX2)

                        End Select


                        Visual.FadeColor(ActionCenter, "ActionCenterButton_Normal", ActionCenter.ActionCenterButton_Normal, [CP].Windows11.Color_Index1, AnimX1, AnimX2)
                        Visual.FadeColor(ActionCenter, "ActionCenterButton_Hover", ActionCenter.ActionCenterButton_Hover, [CP].Windows11.Color_Index0, AnimX1, AnimX2)
                        Visual.FadeColor(ActionCenter, "ActionCenterButton_Pressed", ActionCenter.ActionCenterButton_Pressed, [CP].Windows11.Color_Index2, AnimX1, AnimX2)
                        Visual.FadeColor(start, "SearchBoxAccent", start.SearchBoxAccent, [CP].Windows11.Color_Index1, AnimX1, AnimX2)
                        Visual.FadeColor(taskbar, "AppUnderline", taskbar.AppUnderline, [CP].Windows11.Color_Index1, AnimX1, AnimX2)

                        Visual.FadeColor(setting_icon_preview, "Forecolor", setting_icon_preview.ForeColor, [CP].Windows11.Color_Index3, AnimX1, AnimX2)
                        Visual.FadeColor(lnk_preview, "Forecolor", lnk_preview.ForeColor, [CP].Windows11.Color_Index0, AnimX1, AnimX2)

                    Case False   ''''''''''Light
                        taskbar.BackColorAlpha = 180
                        start.BackColorAlpha = 180
                        ActionCenter.BackColorAlpha = 180

                        Select Case [CP].Windows11.ApplyAccentonTaskbar
                            Case ApplyAccentonTaskbar_Level.None
                                Visual.FadeColor(taskbar, "BackColor", taskbar.BackColor, Color.FromArgb(255, 255, 255), AnimX1, AnimX2)
                                Visual.FadeColor(start, "BackColor", start.BackColor, Color.FromArgb(255, 255, 255), AnimX1, AnimX2)
                                Visual.FadeColor(ActionCenter, "BackColor", ActionCenter.BackColor, Color.FromArgb(255, 255, 255), AnimX1, AnimX2)

                            Case ApplyAccentonTaskbar_Level.Taskbar_Start_AC
                                Visual.FadeColor(taskbar, "BackColor", taskbar.BackColor, Color.FromArgb(taskbar.BackColor.A, [CP].Windows11.Color_Index1), AnimX1, AnimX2)
                                Visual.FadeColor(start, "BackColor", start.BackColor, Color.FromArgb(start.BackColor.A, [CP].Windows11.Color_Index0), AnimX1, AnimX2)
                                Visual.FadeColor(ActionCenter, "BackColor", ActionCenter.BackColor, Color.FromArgb(ActionCenter.BackColor.A, [CP].Windows11.Color_Index0), AnimX1, AnimX2)

                            Case ApplyAccentonTaskbar_Level.Taskbar
                                Visual.FadeColor(taskbar, "BackColor", taskbar.BackColor, Color.FromArgb(taskbar.BackColor.A, [CP].Windows11.Color_Index1), AnimX1, AnimX2)
                                Visual.FadeColor(start, "BackColor", start.BackColor, Color.FromArgb(255, 255, 255), AnimX1, AnimX2)
                                Visual.FadeColor(ActionCenter, "BackColor", ActionCenter.BackColor, Color.FromArgb(255, 255, 255), AnimX1, AnimX2)

                        End Select


                        Visual.FadeColor(ActionCenter, "ActionCenterButton_Normal", ActionCenter.ActionCenterButton_Normal, [CP].Windows11.Color_Index4, AnimX1, AnimX2)
                        Visual.FadeColor(ActionCenter, "ActionCenterButton_Hover", ActionCenter.ActionCenterButton_Hover, [CP].Windows11.Color_Index5, AnimX1, AnimX2)
                        Visual.FadeColor(ActionCenter, "ActionCenterButton_Pressed", ActionCenter.ActionCenterButton_Pressed, [CP].Windows11.Color_Index2, AnimX1, AnimX2)
                        Visual.FadeColor(start, "SearchBoxAccent", start.SearchBoxAccent, [CP].Windows11.Color_Index4, AnimX1, AnimX2)
                        Visual.FadeColor(taskbar, "AppUnderline", taskbar.AppUnderline, [CP].Windows11.Color_Index4, AnimX1, AnimX2)

                        Visual.FadeColor(setting_icon_preview, "Forecolor", setting_icon_preview.ForeColor, [CP].Windows11.Color_Index3, AnimX1, AnimX2)
                        Visual.FadeColor(lnk_preview, "Forecolor", lnk_preview.ForeColor, [CP].Windows11.Color_Index5, AnimX1, AnimX2)
                End Select
#End Region
            Case WinVer.Ten
#Region "Win10"
                XenonWindow1.AccentColor_Enabled = [CP].Windows10.ApplyAccentonTitlebars
                XenonWindow2.AccentColor_Enabled = [CP].Windows10.ApplyAccentonTitlebars

                XenonWindow1.AccentColor_Active = [CP].Windows10.Titlebar_Active
                XenonWindow2.AccentColor_Active = [CP].Windows10.Titlebar_Active

                XenonWindow1.AccentColor_Inactive = [CP].Windows10.Titlebar_Inactive
                XenonWindow2.AccentColor_Inactive = [CP].Windows10.Titlebar_Inactive

                XenonWindow1.DarkMode = Not [CP].Windows10.AppMode_Light
                XenonWindow2.DarkMode = Not [CP].Windows10.AppMode_Light

                Visual.FadeColor(Label8, "Forecolor", Label8.ForeColor, If([CP].Windows10.AppMode_Light, Color.Black, Color.White), AnimX1, AnimX2)


                Select Case Not [CP].Windows10.WinMode_Light
                    Case True ''''''''''Dark
                        W10_lbl2.Text = My.Application.LanguageHelper.CP_10_ACLinks
                        W10_lbl3.Text = My.Application.LanguageHelper.CP_10_TaskbarAppUnderline
                        W10_lbl5.Text = My.Application.LanguageHelper.CP_10_Settings_Links_SomeBtns
                        W10_lbl6.Text = My.Application.LanguageHelper.CP_10_StartMenuIconHover
                        W10_lbl7.Text = My.Application.LanguageHelper.CP_Undefined

                        W10_pic2.Image = My.Resources.Mini_ACHover_Links
                        W10_pic3.Image = My.Resources.Mini_TaskbarApp
                        W10_pic5.Image = My.Resources.Mini_SettingsIcons
                        W10_pic6.Image = My.Resources.Native10
                        W10_pic7.Image = My.Resources.Mini_Undefined

                        If [CP].Windows10.Transparency Then
                            W10_lbl1.Text = My.Application.LanguageHelper.CP_10_Hamburger
                            W10_lbl4.Text = My.Application.LanguageHelper.CP_10_StartMenu_AC
                            W10_lbl8.Text = My.Application.LanguageHelper.CP_10_Taskbar

                            W10_pic1.Image = My.Resources.Mini_Hamburger
                            W10_pic4.Image = My.Resources.Mini_StartMenu_Taskbar_AC
                            W10_pic8.Image = My.Resources.Mini_Taskbar

                            If [CP].Windows10.ApplyAccentonTaskbar <> ApplyAccentonTaskbar_Level.None Then
                                W10_lbl5.Text = My.Application.LanguageHelper.CP_10_Settings_Links_Taskbar_SomeBtns
                            End If

                        Else
                            W10_lbl1.Text = My.Application.LanguageHelper.CP_10_Taskbar
                            W10_pic1.Image = My.Resources.Mini_Taskbar
                            W10_pic4.Image = My.Resources.Mini_StartMenu_Taskbar_AC

                            If [CP].Windows10.ApplyAccentonTaskbar <> ApplyAccentonTaskbar_Level.None Then
                                W10_lbl4.Text = My.Application.LanguageHelper.CP_10_StartMenu_AC_TaskbarActiveApp
                            Else
                                W10_lbl4.Text = My.Application.LanguageHelper.CP_10_StartMenu_AC
                            End If

                            W10_lbl8.Text = My.Application.LanguageHelper.CP_Undefined
                            W10_pic8.Image = My.Resources.Mini_Undefined

                        End If

                    Case False ''''''''''Light
                        If [CP].Windows10.Transparency Then
                            W10_lbl1.Text = My.Application.LanguageHelper.CP_10_Hamburger
                            W10_lbl4.Text = My.Application.LanguageHelper.CP_10_StartMenu_AC
                            W10_lbl6.Text = My.Application.LanguageHelper.CP_10_StartMenuIconHover
                            W10_lbl7.Text = My.Application.LanguageHelper.CP_Undefined

                            W10_pic1.Image = My.Resources.Mini_Hamburger
                            W10_pic4.Image = My.Resources.Mini_StartMenu_Taskbar_AC
                            W10_pic5.Image = My.Resources.Mini_SettingsIcons
                            W10_pic6.Image = My.Resources.Native10
                            W10_pic7.Image = My.Resources.Mini_Undefined
                            W10_pic8.Image = My.Resources.Mini_Taskbar

                            If [CP].Windows10.ApplyAccentonTaskbar = ApplyAccentonTaskbar_Level.None Then
                                W10_lbl2.Text = My.Application.LanguageHelper.CP_Undefined
                                W10_lbl3.Text = My.Application.LanguageHelper.CP_Undefined
                                W10_lbl5.Text = My.Application.LanguageHelper.CP_10_Settings_Links_TaskbarUndeline_SomeBtns
                                W10_lbl8.Text = My.Application.LanguageHelper.CP_10_Taskbar_ACLinks

                                W10_pic2.Image = My.Resources.Mini_Undefined
                                W10_pic3.Image = My.Resources.Mini_Undefined

                            ElseIf [CP].Windows10.ApplyAccentonTaskbar = ApplyAccentonTaskbar_Level.Taskbar Then
                                W10_lbl2.Text = My.Application.LanguageHelper.CP_Undefined
                                W10_lbl3.Text = My.Application.LanguageHelper.CP_10_TaskbarAppUnderline
                                W10_lbl5.Text = My.Application.LanguageHelper.CP_10_Settings_Links_SomeBtns
                                W10_lbl8.Text = My.Application.LanguageHelper.CP_10_Taskbar_ACLinks

                                W10_pic2.Image = My.Resources.Mini_Undefined
                                W10_pic3.Image = My.Resources.Mini_TaskbarApp

                            Else
                                W10_lbl2.Text = My.Application.LanguageHelper.CP_10_ACLinks
                                W10_lbl3.Text = My.Application.LanguageHelper.CP_10_TaskbarAppUnderline
                                W10_lbl5.Text = My.Application.LanguageHelper.CP_10_Settings_Links_SomeBtns
                                W10_lbl8.Text = My.Application.LanguageHelper.CP_10_Taskbar

                                W10_pic2.Image = My.Resources.Mini_ACHover_Links
                                W10_pic3.Image = My.Resources.Mini_TaskbarApp

                            End If
                        Else
                            W10_lbl1.Text = My.Application.LanguageHelper.CP_10_Taskbar
                            W10_lbl6.Text = My.Application.LanguageHelper.CP_10_StartMenuIconHover
                            W10_lbl7.Text = My.Application.LanguageHelper.CP_Undefined

                            W10_pic1.Image = My.Resources.Mini_Taskbar
                            W10_pic6.Image = My.Resources.Native10
                            W10_pic7.Image = My.Resources.Mini_Undefined

                            If [CP].Windows10.ApplyAccentonTaskbar = ApplyAccentonTaskbar_Level.None Then
                                W10_lbl2.Text = My.Application.LanguageHelper.CP_Undefined
                                W10_lbl3.Text = My.Application.LanguageHelper.CP_Undefined
                                W10_lbl4.Text = My.Application.LanguageHelper.CP_10_StartMenu_AC
                                W10_lbl5.Text = My.Application.LanguageHelper.CP_10_Settings_Links_TaskbarUndeline_SomeBtns
                                W10_lbl8.Text = My.Application.LanguageHelper.CP_10_ACLinks

                                W10_pic2.Image = My.Resources.Mini_Undefined
                                W10_pic3.Image = My.Resources.Mini_Undefined
                                W10_pic4.Image = My.Resources.Mini_StartMenu_Taskbar_AC
                                W10_pic5.Image = My.Resources.Mini_SettingsIcons
                                W10_pic8.Image = My.Resources.Mini_ACHover_Links

                            ElseIf [CP].Windows10.ApplyAccentonTaskbar = ApplyAccentonTaskbar_Level.Taskbar Then
                                W10_lbl2.Text = My.Application.LanguageHelper.CP_Undefined
                                W10_lbl3.Text = My.Application.LanguageHelper.CP_10_TaskbarAppUnderline
                                W10_lbl4.Text = My.Application.LanguageHelper.CP_10_TaskbarFocusedApp_StartButtonHover
                                W10_lbl5.Text = My.Application.LanguageHelper.CP_10_Settings_Links_SomeBtns
                                W10_lbl8.Text = My.Application.LanguageHelper.CP_10_ACLinks

                                W10_pic2.Image = My.Resources.Mini_Undefined
                                W10_pic3.Image = My.Resources.Mini_TaskbarApp
                                W10_pic4.Image = My.Resources.Mini_TaskbarActiveIcon
                                W10_pic5.Image = My.Resources.Mini_SettingsIcons
                                W10_pic8.Image = My.Resources.Mini_ACHover_Links

                            Else
                                W10_lbl2.Text = My.Application.LanguageHelper.CP_10_ACLinks
                                W10_lbl3.Text = My.Application.LanguageHelper.CP_10_TaskbarAppUnderline
                                W10_lbl4.Text = My.Application.LanguageHelper.CP_10_StartMenu_AC_TaskbarActiveApp
                                W10_lbl5.Text = My.Application.LanguageHelper.CP_10_Settings_Links_SomeBtns
                                W10_lbl8.Text = My.Application.LanguageHelper.CP_Undefined

                                W10_pic2.Image = My.Resources.Mini_ACHover_Links
                                W10_pic3.Image = My.Resources.Mini_TaskbarApp
                                W10_pic4.Image = My.Resources.Mini_StartMenu_Taskbar_AC
                                W10_pic5.Image = My.Resources.Mini_SettingsIcons
                                W10_pic8.Image = My.Resources.Mini_Undefined
                            End If
                        End If
                End Select

                start.DarkMode = Not [CP].Windows10.WinMode_Light
                taskbar.DarkMode = Not [CP].Windows10.WinMode_Light
                ActionCenter.DarkMode = Not [CP].Windows10.WinMode_Light
                taskbar.Transparency = [CP].Windows10.Transparency
                start.Transparency = [CP].Windows10.Transparency
                ActionCenter.Transparency = [CP].Windows10.Transparency
                start.Top = taskbar.Top - start.Height

                If [CP].Windows10.Transparency Then
                    If Not [CP].Windows10.WinMode_Light Then
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

                Select Case Not [CP].Windows10.WinMode_Light
                    Case True

                        If [CP].Windows10.Transparency Then
                            Select Case [CP].Windows10.ApplyAccentonTaskbar
                                Case ApplyAccentonTaskbar_Level.None
                                    Visual.FadeColor(taskbar, "BackColor", taskbar.BackColor, Color.FromArgb(16, 16, 16), AnimX1, AnimX2)
                                    Visual.FadeColor(taskbar, "StartColor", taskbar.StartColor, Color.FromArgb(150, 150, 150, 150), AnimX1, AnimX2)
                                    Visual.FadeColor(start, "BackColor", start.BackColor, Color.FromArgb(31, 31, 31), AnimX1, AnimX2)
                                    Visual.FadeColor(ActionCenter, "BackColor", ActionCenter.BackColor, Color.FromArgb(31, 31, 31), AnimX1, AnimX2)

                                    Visual.FadeColor(taskbar, "AppBackground", taskbar.AppBackground, Color.FromArgb(150, 150, 150, 150), AnimX1, AnimX2)
                                    Visual.FadeColor(ActionCenter, "LinkColor", ActionCenter.LinkColor, [CP].Windows10.Color_Index0, AnimX1, AnimX2)
                                    Visual.FadeColor(taskbar, "AppUnderline", taskbar.AppUnderline, [CP].Windows10.Color_Index1, AnimX1, AnimX2)
                                    Visual.FadeColor(setting_icon_preview, "Forecolor", setting_icon_preview.ForeColor, [CP].Windows10.Color_Index3, AnimX1, AnimX2)
                                    Visual.FadeColor(lnk_preview, "Forecolor", lnk_preview.ForeColor, [CP].Windows10.Color_Index3, AnimX1, AnimX2)
                                    Visual.FadeColor(ActionCenter, "ActionCenterButton_Normal", ActionCenter.ActionCenterButton_Normal, [CP].Windows10.Color_Index3, AnimX1, AnimX2)

                                Case ApplyAccentonTaskbar_Level.Taskbar
                                    Visual.FadeColor(taskbar, "BackColor", taskbar.BackColor, [CP].Windows10.Color_Index6, AnimX1, AnimX2)
                                    Visual.FadeColor(taskbar, "StartColor", taskbar.StartColor, Color.FromArgb(0, 0, 0, 0), AnimX1, AnimX2)
                                    Visual.FadeColor(start, "BackColor", start.BackColor, Color.FromArgb(31, 31, 31), AnimX1, AnimX2)
                                    Visual.FadeColor(ActionCenter, "BackColor", ActionCenter.BackColor, Color.FromArgb(31, 31, 31), AnimX1, AnimX2)

                                    Visual.FadeColor(taskbar, "AppBackground", taskbar.AppBackground, Color.FromArgb(150, [CP].Windows10.Color_Index3), AnimX1, AnimX2)
                                    Visual.FadeColor(ActionCenter, "LinkColor", ActionCenter.LinkColor, [CP].Windows10.Color_Index0, AnimX1, AnimX2)
                                    Visual.FadeColor(taskbar, "AppUnderline", taskbar.AppUnderline, [CP].Windows10.Color_Index1, AnimX1, AnimX2)
                                    Visual.FadeColor(setting_icon_preview, "Forecolor", setting_icon_preview.ForeColor, [CP].Windows10.Color_Index3, AnimX1, AnimX2)
                                    Visual.FadeColor(lnk_preview, "Forecolor", lnk_preview.ForeColor, [CP].Windows10.Color_Index3, AnimX1, AnimX2)
                                    Visual.FadeColor(ActionCenter, "ActionCenterButton_Normal", ActionCenter.ActionCenterButton_Normal, [CP].Windows10.Color_Index3, AnimX1, AnimX2)

                                Case ApplyAccentonTaskbar_Level.Taskbar_Start_AC
                                    Visual.FadeColor(taskbar, "BackColor", taskbar.BackColor, [CP].Windows10.Color_Index6, AnimX1, AnimX2)
                                    Visual.FadeColor(taskbar, "StartColor", taskbar.StartColor, Color.FromArgb(0, 0, 0, 0), AnimX1, AnimX2)
                                    Visual.FadeColor(start, "BackColor", start.BackColor, [CP].Windows10.Color_Index4, AnimX1, AnimX2)
                                    Visual.FadeColor(ActionCenter, "BackColor", ActionCenter.BackColor, [CP].Windows10.Color_Index4, AnimX1, AnimX2)

                                    Visual.FadeColor(taskbar, "AppBackground", taskbar.AppBackground, Color.FromArgb(150, [CP].Windows10.Color_Index3), AnimX1, AnimX2)
                                    Visual.FadeColor(ActionCenter, "LinkColor", ActionCenter.LinkColor, [CP].Windows10.Color_Index0, AnimX1, AnimX2)
                                    Visual.FadeColor(taskbar, "AppUnderline", taskbar.AppUnderline, [CP].Windows10.Color_Index1, AnimX1, AnimX2)
                                    Visual.FadeColor(setting_icon_preview, "Forecolor", setting_icon_preview.ForeColor, [CP].Windows10.Color_Index3, AnimX1, AnimX2)
                                    Visual.FadeColor(lnk_preview, "Forecolor", lnk_preview.ForeColor, [CP].Windows10.Color_Index3, AnimX1, AnimX2)
                                    Visual.FadeColor(ActionCenter, "ActionCenterButton_Normal", ActionCenter.ActionCenterButton_Normal, [CP].Windows10.Color_Index3, AnimX1, AnimX2)

                            End Select

                        Else
                            Select Case [CP].Windows10.ApplyAccentonTaskbar
                                Case ApplyAccentonTaskbar_Level.None
                                    Visual.FadeColor(taskbar, "BackColor", taskbar.BackColor, Color.FromArgb(16, 16, 16), AnimX1, AnimX2)
                                    Visual.FadeColor(taskbar, "StartColor", taskbar.StartColor, Color.FromArgb(31, 31, 31), AnimX1, AnimX2)
                                    Visual.FadeColor(start, "BackColor", start.BackColor, Color.FromArgb(31, 31, 31), AnimX1, AnimX2)
                                    Visual.FadeColor(ActionCenter, "BackColor", ActionCenter.BackColor, Color.FromArgb(31, 31, 31), AnimX1, AnimX2)

                                Case ApplyAccentonTaskbar_Level.Taskbar
                                    Visual.FadeColor(taskbar, "BackColor", taskbar.BackColor, [CP].Windows10.Color_Index5, AnimX1, AnimX2)
                                    Visual.FadeColor(taskbar, "StartColor", taskbar.StartColor, [CP].Windows10.Color_Index4, AnimX1, AnimX2)
                                    Visual.FadeColor(start, "BackColor", start.BackColor, Color.FromArgb(31, 31, 31), AnimX1, AnimX2)
                                    Visual.FadeColor(ActionCenter, "BackColor", ActionCenter.BackColor, Color.FromArgb(31, 31, 31), AnimX1, AnimX2)

                                Case ApplyAccentonTaskbar_Level.Taskbar_Start_AC
                                    Visual.FadeColor(taskbar, "BackColor", taskbar.BackColor, [CP].Windows10.Color_Index5, AnimX1, AnimX2)
                                    Visual.FadeColor(taskbar, "StartColor", taskbar.StartColor, [CP].Windows10.Color_Index4, AnimX1, AnimX2)
                                    Visual.FadeColor(start, "BackColor", start.BackColor, [CP].Windows10.Color_Index4, AnimX1, AnimX2)
                                    Visual.FadeColor(ActionCenter, "BackColor", ActionCenter.BackColor, [CP].Windows10.Color_Index4, AnimX1, AnimX2)
                            End Select

                            If [CP].Windows10.ApplyAccentonTaskbar = ApplyAccentonTaskbar_Level.None Then
                                Visual.FadeColor(taskbar, "AppBackground", taskbar.AppBackground, Color.FromArgb(150, 100, 100, 100), AnimX1, AnimX2)
                            Else
                                Visual.FadeColor(taskbar, "AppBackground", taskbar.AppBackground, [CP].Windows10.Color_Index4, AnimX1, AnimX2)
                            End If

                            Visual.FadeColor(ActionCenter, "LinkColor", ActionCenter.LinkColor, [CP].Windows10.Color_Index0, AnimX1, AnimX2)
                            Visual.FadeColor(taskbar, "AppUnderline", taskbar.AppUnderline, [CP].Windows10.Color_Index1, AnimX1, AnimX2)
                            Visual.FadeColor(setting_icon_preview, "Forecolor", setting_icon_preview.ForeColor, [CP].Windows10.Color_Index3, AnimX1, AnimX2)
                            Visual.FadeColor(lnk_preview, "Forecolor", lnk_preview.ForeColor, [CP].Windows10.Color_Index3, AnimX1, AnimX2)
                            Visual.FadeColor(ActionCenter, "ActionCenterButton_Normal", ActionCenter.ActionCenterButton_Normal, [CP].Windows10.Color_Index3, AnimX1, AnimX2)

                        End If

                    Case False
                        If [CP].Windows10.Transparency Then

                            Select Case [CP].Windows10.ApplyAccentonTaskbar
                                Case ApplyAccentonTaskbar_Level.None
                                    Visual.FadeColor(taskbar, "BackColor", taskbar.BackColor, Color.FromArgb(238, 238, 238), AnimX1, AnimX2)
                                    Visual.FadeColor(taskbar, "StartColor", taskbar.StartColor, Color.Transparent, AnimX1, AnimX2)
                                    Visual.FadeColor(start, "BackColor", start.BackColor, Color.FromArgb(228, 228, 228), AnimX1, AnimX2)
                                    Visual.FadeColor(ActionCenter, "BackColor", ActionCenter.BackColor, Color.FromArgb(228, 228, 228), AnimX1, AnimX2)

                                    Visual.FadeColor(taskbar, "AppBackground", taskbar.AppBackground, Color.FromArgb(150, 238, 238, 238), AnimX1, AnimX2)
                                    Visual.FadeColor(ActionCenter, "LinkColor", ActionCenter.LinkColor, [CP].Windows10.Color_Index6, AnimX1, AnimX2)
                                    Visual.FadeColor(taskbar, "AppUnderline", taskbar.AppUnderline, [CP].Windows10.Color_Index3, AnimX1, AnimX2)
                                    Visual.FadeColor(setting_icon_preview, "Forecolor", setting_icon_preview.ForeColor, [CP].Windows10.Color_Index3, AnimX1, AnimX2)
                                    Visual.FadeColor(lnk_preview, "Forecolor", lnk_preview.ForeColor, [CP].Windows10.Color_Index3, AnimX1, AnimX2)
                                    Visual.FadeColor(ActionCenter, "ActionCenterButton_Normal", ActionCenter.ActionCenterButton_Normal, [CP].Windows10.Color_Index3, AnimX1, AnimX2)

                                Case ApplyAccentonTaskbar_Level.Taskbar
                                    Visual.FadeColor(taskbar, "BackColor", taskbar.BackColor, [CP].Windows10.Color_Index6, AnimX1, AnimX2)
                                    Visual.FadeColor(taskbar, "StartColor", taskbar.StartColor, Color.Transparent, AnimX1, AnimX2)
                                    Visual.FadeColor(start, "BackColor", start.BackColor, Color.FromArgb(228, 228, 228), AnimX1, AnimX2)
                                    Visual.FadeColor(ActionCenter, "BackColor", ActionCenter.BackColor, Color.FromArgb(228, 228, 228), AnimX1, AnimX2)

                                    Visual.FadeColor(taskbar, "AppBackground", taskbar.AppBackground, Color.FromArgb(150, [CP].Windows10.Color_Index3), AnimX1, AnimX2)
                                    Visual.FadeColor(ActionCenter, "LinkColor", ActionCenter.LinkColor, [CP].Windows10.Color_Index6, AnimX1, AnimX2)
                                    Visual.FadeColor(taskbar, "AppUnderline", taskbar.AppUnderline, [CP].Windows10.Color_Index1, AnimX1, AnimX2)
                                    Visual.FadeColor(setting_icon_preview, "Forecolor", setting_icon_preview.ForeColor, [CP].Windows10.Color_Index3, AnimX1, AnimX2)
                                    Visual.FadeColor(lnk_preview, "Forecolor", lnk_preview.ForeColor, [CP].Windows10.Color_Index3, AnimX1, AnimX2)
                                    Visual.FadeColor(ActionCenter, "ActionCenterButton_Normal", ActionCenter.ActionCenterButton_Normal, [CP].Windows10.Color_Index3, AnimX1, AnimX2)

                                Case ApplyAccentonTaskbar_Level.Taskbar_Start_AC
                                    Visual.FadeColor(taskbar, "BackColor", taskbar.BackColor, [CP].Windows10.Color_Index6, AnimX1, AnimX2)
                                    Visual.FadeColor(taskbar, "StartColor", taskbar.StartColor, Color.Transparent, AnimX1, AnimX2)
                                    Visual.FadeColor(start, "BackColor", start.BackColor, [CP].Windows10.Color_Index4, AnimX1, AnimX2)
                                    Visual.FadeColor(ActionCenter, "BackColor", ActionCenter.BackColor, [CP].Windows10.Color_Index4, AnimX1, AnimX2)

                                    Visual.FadeColor(taskbar, "AppBackground", taskbar.AppBackground, Color.FromArgb(150, [CP].Windows10.Color_Index3), AnimX1, AnimX2)
                                    Visual.FadeColor(ActionCenter, "LinkColor", ActionCenter.LinkColor, [CP].Windows10.Color_Index0, AnimX1, AnimX2)
                                    Visual.FadeColor(taskbar, "AppUnderline", taskbar.AppUnderline, [CP].Windows10.Color_Index1, AnimX1, AnimX2)
                                    Visual.FadeColor(setting_icon_preview, "Forecolor", setting_icon_preview.ForeColor, [CP].Windows10.Color_Index3, AnimX1, AnimX2)
                                    Visual.FadeColor(lnk_preview, "Forecolor", lnk_preview.ForeColor, [CP].Windows10.Color_Index3, AnimX1, AnimX2)
                                    Visual.FadeColor(ActionCenter, "ActionCenterButton_Normal", ActionCenter.ActionCenterButton_Normal, [CP].Windows10.Color_Index3, AnimX1, AnimX2)

                            End Select

                        Else

                            Select Case [CP].Windows10.ApplyAccentonTaskbar
                                Case ApplyAccentonTaskbar_Level.None
                                    Visual.FadeColor(taskbar, "BackColor", taskbar.BackColor, Color.FromArgb(238, 238, 238), AnimX1, AnimX2)
                                    Visual.FadeColor(taskbar, "StartColor", taskbar.StartColor, Color.FromArgb(241, 241, 241), AnimX1, AnimX2)
                                    Visual.FadeColor(start, "BackColor", start.BackColor, Color.FromArgb(228, 228, 228), AnimX1, AnimX2)
                                    Visual.FadeColor(ActionCenter, "BackColor", ActionCenter.BackColor, Color.FromArgb(228, 228, 228), AnimX1, AnimX2)

                                    Visual.FadeColor(taskbar, "AppBackground", taskbar.AppBackground, Color.FromArgb(252, 252, 252), AnimX1, AnimX2)
                                    Visual.FadeColor(ActionCenter, "LinkColor", ActionCenter.LinkColor, [CP].Windows10.Color_Index6, AnimX1, AnimX2)
                                    Visual.FadeColor(taskbar, "AppUnderline", taskbar.AppUnderline, [CP].Windows10.Color_Index3, AnimX1, AnimX2)
                                    Visual.FadeColor(setting_icon_preview, "Forecolor", setting_icon_preview.ForeColor, [CP].Windows10.Color_Index3, AnimX1, AnimX2)
                                    Visual.FadeColor(lnk_preview, "Forecolor", lnk_preview.ForeColor, [CP].Windows10.Color_Index3, AnimX1, AnimX2)
                                    Visual.FadeColor(ActionCenter, "ActionCenterButton_Normal", ActionCenter.ActionCenterButton_Normal, [CP].Windows10.Color_Index3, AnimX1, AnimX2)

                                Case ApplyAccentonTaskbar_Level.Taskbar
                                    Visual.FadeColor(taskbar, "BackColor", taskbar.BackColor, [CP].Windows10.Color_Index5, AnimX1, AnimX2)
                                    Visual.FadeColor(taskbar, "StartColor", taskbar.StartColor, [CP].Windows10.Color_Index4, AnimX1, AnimX2)
                                    Visual.FadeColor(start, "BackColor", start.BackColor, Color.FromArgb(228, 228, 228), AnimX1, AnimX2)
                                    Visual.FadeColor(ActionCenter, "BackColor", ActionCenter.BackColor, Color.FromArgb(228, 228, 228), AnimX1, AnimX2)

                                    Visual.FadeColor(taskbar, "AppBackground", taskbar.AppBackground, [CP].Windows10.Color_Index4, AnimX1, AnimX2)
                                    Visual.FadeColor(ActionCenter, "LinkColor", ActionCenter.LinkColor, [CP].Windows10.Color_Index6, AnimX1, AnimX2)
                                    Visual.FadeColor(taskbar, "AppUnderline", taskbar.AppUnderline, [CP].Windows10.Color_Index1, AnimX1, AnimX2)
                                    Visual.FadeColor(setting_icon_preview, "Forecolor", setting_icon_preview.ForeColor, [CP].Windows10.Color_Index3, AnimX1, AnimX2)
                                    Visual.FadeColor(lnk_preview, "Forecolor", lnk_preview.ForeColor, [CP].Windows10.Color_Index3, AnimX1, AnimX2)
                                    Visual.FadeColor(ActionCenter, "ActionCenterButton_Normal", ActionCenter.ActionCenterButton_Normal, [CP].Windows10.Color_Index3, AnimX1, AnimX2)

                                Case ApplyAccentonTaskbar_Level.Taskbar_Start_AC
                                    Visual.FadeColor(taskbar, "BackColor", taskbar.BackColor, [CP].Windows10.Color_Index5, AnimX1, AnimX2)
                                    Visual.FadeColor(taskbar, "StartColor", taskbar.StartColor, [CP].Windows10.Color_Index4, AnimX1, AnimX2)
                                    Visual.FadeColor(start, "BackColor", start.BackColor, [CP].Windows10.Color_Index4, AnimX1, AnimX2)
                                    Visual.FadeColor(ActionCenter, "BackColor", ActionCenter.BackColor, [CP].Windows10.Color_Index4, AnimX1, AnimX2)


                                    Visual.FadeColor(taskbar, "AppBackground", taskbar.AppBackground, [CP].Windows10.Color_Index4, AnimX1, AnimX2)
                                    Visual.FadeColor(ActionCenter, "LinkColor", ActionCenter.LinkColor, [CP].Windows10.Color_Index0, AnimX1, AnimX2)
                                    Visual.FadeColor(taskbar, "AppUnderline", taskbar.AppUnderline, [CP].Windows10.Color_Index1, AnimX1, AnimX2)
                                    Visual.FadeColor(setting_icon_preview, "Forecolor", setting_icon_preview.ForeColor, [CP].Windows10.Color_Index3, AnimX1, AnimX2)
                                    Visual.FadeColor(lnk_preview, "Forecolor", lnk_preview.ForeColor, [CP].Windows10.Color_Index3, AnimX1, AnimX2)
                                    Visual.FadeColor(ActionCenter, "ActionCenterButton_Normal", ActionCenter.ActionCenterButton_Normal, [CP].Windows10.Color_Index3, AnimX1, AnimX2)

                            End Select

                        End If
                End Select

#End Region
            Case WinVer.Eight
#Region "Win8.1"
                If My.W8 And My.Application._Settings.Win7LivePreview Then
                    RefreshDWM([CP])
                End If

                ApplyMetroStartToButton([CP])
                ApplyBackLogonUI([CP])

                Select Case [CP].Windows8.Theme
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

                XenonWindow1.AccentColor_Active = [CP].Windows8.ColorizationColor
                XenonWindow1.Win7ColorBal = [CP].Windows8.ColorizationColorBalance

                XenonWindow2.AccentColor_Active = [CP].Windows8.ColorizationColor
                XenonWindow2.Win7ColorBal = [CP].Windows8.ColorizationColorBalance

                taskbar.BackColor = [CP].Windows8.ColorizationColor
                taskbar.Win7ColorBal = [CP].Windows8.ColorizationColorBalance
#End Region

            Case WinVer.Seven
#Region "Win7"
                If My.W7 And My.Application._Settings.Win7LivePreview And _Shown Then
                    RefreshDWM([CP])
                End If

                Select Case [CP].Windows7.Theme
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
                            .Win7Alpha = [CP].Windows7.ColorizationBlurBalance
                            .Win7ColorBal = [CP].Windows7.ColorizationColorBalance
                            .Win7GlowBal = [CP].Windows7.ColorizationAfterglowBalance
                            .AccentColor_Active = [CP].Windows7.ColorizationColor
                            .AccentColor2_Active = [CP].Windows7.ColorizationAfterglow
                            .AccentColor_Inactive = [CP].Windows7.ColorizationColor
                            .AccentColor2_Inactive = [CP].Windows7.ColorizationAfterglow
                            .Win7Noise = [CP].Windows7.ColorizationGlassReflectionIntensity
                        End With
                        With XenonWindow2
                            .Win7 = True
                            .Win7Aero = True
                            .Win7AeroOpaque = False
                            .Win7Basic = False
                            .Win7Alpha = [CP].Windows7.ColorizationBlurBalance
                            .Win7ColorBal = [CP].Windows7.ColorizationColorBalance
                            .Win7GlowBal = [CP].Windows7.ColorizationAfterglowBalance
                            .AccentColor_Active = [CP].Windows7.ColorizationColor
                            .AccentColor2_Active = [CP].Windows7.ColorizationAfterglow
                            .AccentColor_Inactive = [CP].Windows7.ColorizationColor
                            .AccentColor2_Inactive = [CP].Windows7.ColorizationAfterglow
                            .Win7Noise = [CP].Windows7.ColorizationGlassReflectionIntensity
                        End With
                        With start
                            .Win7AeroOpaque = False
                            .BackColorAlpha = [CP].Windows7.ColorizationBlurBalance
                            .Win7ColorBal = [CP].Windows7.ColorizationColorBalance
                            .Win7GlowBal = [CP].Windows7.ColorizationAfterglowBalance
                            .BackColor = [CP].Windows7.ColorizationColor
                            .BackColor2 = [CP].Windows7.ColorizationAfterglow
                            .NoisePower = [CP].Windows7.ColorizationGlassReflectionIntensity
                        End With
                        With taskbar
                            .Win7AeroOpaque = False
                            .BackColorAlpha = [CP].Windows7.ColorizationBlurBalance
                            .Win7ColorBal = [CP].Windows7.ColorizationColorBalance
                            .Win7GlowBal = [CP].Windows7.ColorizationAfterglowBalance
                            .BackColor = [CP].Windows7.ColorizationColor
                            .BackColor2 = [CP].Windows7.ColorizationAfterglow
                            .NoisePower = [CP].Windows7.ColorizationGlassReflectionIntensity
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
                            .Win7Alpha = [CP].Windows7.ColorizationColorBalance
                            .AccentColor_Active = [CP].Windows7.ColorizationColor
                            .AccentColor_Inactive = [CP].Windows7.ColorizationColor
                            .Win7Noise = [CP].Windows7.ColorizationGlassReflectionIntensity
                        End With
                        With XenonWindow2
                            .Win7 = True
                            .Win7Aero = False
                            .Win7AeroOpaque = True
                            .Win7Basic = False
                            .Win7Alpha = [CP].Windows7.ColorizationColorBalance
                            .AccentColor_Active = [CP].Windows7.ColorizationColor
                            .AccentColor_Inactive = [CP].Windows7.ColorizationColor
                            .Win7Noise = [CP].Windows7.ColorizationGlassReflectionIntensity
                        End With
                        With taskbar
                            .Win7AeroOpaque = True
                            .BackColorAlpha = [CP].Windows7.ColorizationColorBalance
                            .BackColor = [CP].Windows7.ColorizationColor
                            .BackColor2 = [CP].Windows7.ColorizationColor
                            .NoisePower = [CP].Windows7.ColorizationGlassReflectionIntensity
                        End With
                        With start
                            .Win7AeroOpaque = True
                            .BackColorAlpha = [CP].Windows7.ColorizationColorBalance
                            .BackColor = [CP].Windows7.ColorizationColor
                            .BackColor2 = [CP].Windows7.ColorizationColor
                            .NoisePower = [CP].Windows7.ColorizationGlassReflectionIntensity
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

#End Region
        End Select

        ApplyMetrics([CP], XenonWindow1)
        ApplyMetrics([CP], XenonWindow2)

        ReValidateLivePreview(pnl_preview)

    End Sub

    Sub ApplyMetrics(ByVal CP As CP, XenonWindow As XenonWindow)
        XenonWindow.Font = CP.WinMetrics_Fonts.CaptionFont
        XenonWindow.Metrics_BorderWidth = CP.WinMetrics_Fonts.BorderWidth
        XenonWindow.Metrics_CaptionHeight = CP.WinMetrics_Fonts.CaptionHeight
        XenonWindow.Metrics_PaddedBorderWidth = CP.WinMetrics_Fonts.PaddedBorderWidth
        XenonWindow.Refresh()
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

                If CP.Windows7.Theme = AeroTheme.Classic Then
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
                taskbar.NoisePower = CP.Windows7.ColorizationGlassReflectionIntensity / 100
                taskbar.Height = 34

                start.UseItAsTaskbar_Version = XenonAcrylic.TaskbarVersion.Seven
                start.RoundedCorners = True
                start.BlurPower = 1
                start.NoisePower = 0.5
                start.Left = 0
                start.Width = 136
                start.Height = 191
                start.NoisePower = CP.Windows7.ColorizationGlassReflectionIntensity / 100
                start.Top = taskbar.Top - start.Height
        End Select

        If PreviewConfig = WinVer.Ten Or PreviewConfig = WinVer.Eleven Then
            XenonWindow1.Top = start.Top - If(PreviewConfig = WinVer.Eleven, 30, 35)
            XenonWindow1.Left = start.Right + If(PreviewConfig = WinVer.Eleven, 30, 15)

            XenonWindow2.Top = XenonWindow1.Bottom + 1
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
        themename_lbl.Text = String.Format("{0} ({1})", CP.Info.PaletteName, CP.Info.PaletteVersion)
        author_lbl.Text = String.Format("{0}: {1}", My.Application.LanguageHelper.By, CP.Info.Author)

        W11_WinMode_Toggle.Checked = Not ColorPalette.Windows11.WinMode_Light
        W11_AppMode_Toggle.Checked = Not ColorPalette.Windows11.AppMode_Light
        W11_Transparency_Toggle.Checked = ColorPalette.Windows11.Transparency
        W11_ShowAccentOnTitlebarAndBorders_Toggle.Checked = ColorPalette.Windows11.ApplyAccentonTitlebars

        Select Case ColorPalette.Windows11.ApplyAccentonTaskbar
            Case ApplyAccentonTaskbar_Level.None
                W11_Accent_None.Checked = True

            Case ApplyAccentonTaskbar_Level.Taskbar_Start_AC
                W11_Accent_StartTaskbar.Checked = True

            Case ApplyAccentonTaskbar_Level.Taskbar
                W11_Accent_Taskbar.Checked = True

        End Select

        W11_ActiveTitlebar_pick.BackColor = ColorPalette.Windows11.Titlebar_Active
        W11_InactiveTitlebar_pick.BackColor = ColorPalette.Windows11.Titlebar_Inactive
        W11_Color_Index5.BackColor = ColorPalette.Windows11.StartMenu_Accent
        W11_Color_Index4.BackColor = ColorPalette.Windows11.Color_Index2
        W11_Color_Index6.BackColor = ColorPalette.Windows11.Color_Index6
        W11_Color_Index1.BackColor = ColorPalette.Windows11.Color_Index1
        W11_Color_Index2.BackColor = ColorPalette.Windows11.Color_Index4
        W11_TaskbarFrontAndFoldersOnStart_pick.BackColor = ColorPalette.Windows11.Color_Index5
        W11_Color_Index0.BackColor = ColorPalette.Windows11.Color_Index0
        W11_Color_Index3.BackColor = ColorPalette.Windows11.Color_Index3
        W11_Color_Index7.BackColor = ColorPalette.Windows11.Color_Index7

        W10_WinMode_Toggle.Checked = Not ColorPalette.Windows10.WinMode_Light
        W10_AppMode_Toggle.Checked = Not ColorPalette.Windows10.AppMode_Light
        W10_Transparency_Toggle.Checked = ColorPalette.Windows10.Transparency
        W10_ShowAccentOnTitlebarAndBorders_Toggle.Checked = ColorPalette.Windows10.ApplyAccentonTitlebars

        Select Case ColorPalette.Windows10.ApplyAccentonTaskbar
            Case ApplyAccentonTaskbar_Level.None
                W10_Accent_None.Checked = True

            Case ApplyAccentonTaskbar_Level.Taskbar_Start_AC
                W10_Accent_StartTaskbar.Checked = True

            Case ApplyAccentonTaskbar_Level.Taskbar
                W10_Accent_Taskbar.Checked = True
        End Select

        W10_ActiveTitlebar_pick.BackColor = ColorPalette.Windows10.Titlebar_Active
        W10_InactiveTitlebar_pick.BackColor = ColorPalette.Windows10.Titlebar_Inactive
        W10_Color_Index5.BackColor = ColorPalette.Windows10.StartMenu_Accent
        W10_Color_Index4.BackColor = ColorPalette.Windows10.Color_Index2
        W10_Color_Index6.BackColor = ColorPalette.Windows10.Color_Index6
        W10_Color_Index1.BackColor = ColorPalette.Windows10.Color_Index1
        W10_Color_Index2.BackColor = ColorPalette.Windows10.Color_Index4
        W10_TaskbarFrontAndFoldersOnStart_pick.BackColor = ColorPalette.Windows10.Color_Index5
        W10_Color_Index0.BackColor = ColorPalette.Windows10.Color_Index0
        W10_Color_Index3.BackColor = ColorPalette.Windows10.Color_Index3
        W10_Color_Index7.BackColor = ColorPalette.Windows10.Color_Index7


        W7_ColorizationColor_pick.BackColor = ColorPalette.Windows7.ColorizationColor
        W7_ColorizationAfterglow_pick.BackColor = ColorPalette.Windows7.ColorizationAfterglow
        W7_ColorizationColorBalance_bar.Value = ColorPalette.Windows7.ColorizationColorBalance
        W7_ColorizationAfterglowBalance_bar.Value = ColorPalette.Windows7.ColorizationAfterglowBalance
        W7_ColorizationBlurBalance_bar.Value = ColorPalette.Windows7.ColorizationBlurBalance
        W7_ColorizationGlassReflectionIntensity_bar.Value = ColorPalette.Windows7.ColorizationGlassReflectionIntensity
        W7_EnableAeroPeek_toggle.Checked = ColorPalette.Windows7.EnableAeroPeek
        W7_AlwaysHibernateThumbnails_Toggle.Checked = ColorPalette.Windows7.AlwaysHibernateThumbnails

        If PreviewConfig = WinVer.Seven Then
            Select Case ColorPalette.Windows7.Theme
                Case CP.AeroTheme.Aero
                    W7_theme_aero.Checked = True

                Case CP.AeroTheme.AeroOpaque
                    W7_theme_aeroopaque.Checked = True

                Case CP.AeroTheme.Basic
                    W7_theme_basic.Checked = True

                Case CP.AeroTheme.Classic
                    W7_theme_classic.Checked = True
            End Select

        ElseIf PreviewConfig = WinVer.Eight Then
            Select Case ColorPalette.Windows8.Theme
                Case CP.AeroTheme.Aero
                    W8_theme_aero.Checked = True

                Case CP.AeroTheme.AeroLite
                    W8_theme_aerolite.Checked = True
            End Select
        End If

        W8_ColorizationColor_pick.BackColor = ColorPalette.Windows7.ColorizationColor
        W8_ColorizationBalance_bar.Value = ColorPalette.Windows7.ColorizationColorBalance

        W8_start_pick.BackColor = ColorPalette.Windows8.StartColor
        W8_accent_pick.BackColor = ColorPalette.Windows8.AccentColor
        W8_personalcls_background_pick.BackColor = ColorPalette.Windows8.PersonalColors_Background
        W8_personalcolor_accent_pick.BackColor = ColorPalette.Windows8.PersonalColors_Accent

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

        W11_ActiveTitlebar_pick.DefaultColor = DefCP.Windows11.Titlebar_Active
        W11_InactiveTitlebar_pick.DefaultColor = DefCP.Windows11.Titlebar_Inactive
        W11_Color_Index5.DefaultColor = DefCP.Windows11.StartMenu_Accent
        W11_Color_Index4.DefaultColor = DefCP.Windows11.Color_Index2
        W11_Color_Index6.DefaultColor = DefCP.Windows11.Color_Index6
        W11_Color_Index1.DefaultColor = DefCP.Windows11.Color_Index1
        W11_Color_Index2.DefaultColor = DefCP.Windows11.Color_Index4
        W11_TaskbarFrontAndFoldersOnStart_pick.DefaultColor = DefCP.Windows11.Color_Index5
        W11_Color_Index0.DefaultColor = DefCP.Windows11.Color_Index0
        W11_Color_Index3.DefaultColor = DefCP.Windows11.Color_Index3
        W11_Color_Index7.DefaultColor = DefCP.Windows11.Color_Index7

        W10_ActiveTitlebar_pick.DefaultColor = DefCP.Windows10.Titlebar_Active
        W10_InactiveTitlebar_pick.DefaultColor = DefCP.Windows10.Titlebar_Inactive
        W10_Color_Index5.DefaultColor = DefCP.Windows10.StartMenu_Accent
        W10_Color_Index4.DefaultColor = DefCP.Windows10.Color_Index2
        W10_Color_Index6.DefaultColor = DefCP.Windows10.Color_Index6
        W10_Color_Index1.DefaultColor = DefCP.Windows10.Color_Index1
        W10_Color_Index2.DefaultColor = DefCP.Windows10.Color_Index4
        W10_TaskbarFrontAndFoldersOnStart_pick.DefaultColor = DefCP.Windows10.Color_Index5
        W10_Color_Index0.DefaultColor = DefCP.Windows10.Color_Index0
        W10_Color_Index3.DefaultColor = DefCP.Windows10.Color_Index3
        W10_Color_Index7.DefaultColor = DefCP.Windows10.Color_Index7

        W7_ColorizationColor_pick.DefaultColor = DefCP.Windows7.ColorizationColor
        W7_ColorizationAfterglow_pick.DefaultColor = DefCP.Windows7.ColorizationAfterglow
        W8_ColorizationColor_pick.DefaultColor = DefCP.Windows7.ColorizationColor
        W8_start_pick.DefaultColor = DefCP.Windows8.StartColor
        W8_accent_pick.DefaultColor = DefCP.Windows8.AccentColor
        W8_personalcls_background_pick.DefaultColor = DefCP.Windows8.PersonalColors_Background
        W8_personalcolor_accent_pick.DefaultColor = DefCP.Windows8.PersonalColors_Accent

        CP.Dispose()
    End Sub

    Sub ApplyMetroStartToButton(ColorPalette As CP)
        Select Case ColorPalette.Windows8.Start
            Case 1
                W8_start.Image = ResizeImage(My.Application.WinRes.MetroStart_1, 48, 48)
            Case 2
                W8_start.Image = ResizeImage(My.Application.WinRes.MetroStart_2, 48, 48)
            Case 3
                W8_start.Image = ResizeImage(My.Application.WinRes.MetroStart_3, 48, 48)
            Case 4
                W8_start.Image = ResizeImage(My.Application.WinRes.MetroStart_4, 48, 48)
            Case 5
                W8_start.Image = ResizeImage(My.Application.WinRes.MetroStart_5, 48, 48)
            Case 6
                W8_start.Image = ResizeImage(My.Application.WinRes.MetroStart_6, 48, 48)
            Case 7
                W8_start.Image = ResizeImage(My.Application.WinRes.MetroStart_7, 48, 48)
            Case 8
                W8_start.Image = ResizeImage(My.Application.WinRes.MetroStart_8, 48, 48)
            Case 9
                W8_start.Image = ResizeImage(My.Application.WinRes.MetroStart_9, 48, 48)
            Case 10
                W8_start.Image = ResizeImage(My.Application.WinRes.MetroStart_10, 48, 48)
            Case 11
                W8_start.Image = ResizeImage(My.Application.WinRes.MetroStart_11, 48, 48)
            Case 12
                W8_start.Image = ResizeImage(My.Application.WinRes.MetroStart_12, 48, 48)
            Case 13
                W8_start.Image = ResizeImage(My.Application.WinRes.MetroStart_13, 48, 48)
            Case 14
                W8_start.Image = ResizeImage(My.Application.WinRes.MetroStart_14, 48, 48)
            Case 15
                W8_start.Image = ResizeImage(My.Application.WinRes.MetroStart_15, 48, 48)
            Case 16
                W8_start.Image = ResizeImage(My.Application.WinRes.MetroStart_16, 48, 48)
            Case 17
                W8_start.Image = ResizeImage(My.Application.WinRes.MetroStart_17, 48, 48)
            Case 18
                W8_start.Image = ResizeImage(My.Application.WinRes.MetroStart_18, 48, 48)
            Case 19
                W8_start.Image = ColorToBitmap(ColorPalette.Windows8.PersonalColors_Background, New Size(48, 48))
            Case 20
                W8_start.Image = ResizeImage(My.Application.GetCurrentWallpaper, 48, 48)
            Case Else
                W8_start.Image = ResizeImage(My.Application.WinRes.MetroStart_1, 48, 48)
        End Select
    End Sub

    Sub ApplyBackLogonUI(ColorPalette As CP)

        For Each ri As XenonRadioImage In LogonUI8Colors.Controls.OfType(Of XenonRadioImage)
            If ColorPalette.Windows8.LogonUI = ri.Name.Replace("color", "") Then
                W8_logonui.Image = ColorToBitmap(ri.AccentColor, New Size(48, 48))
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
        status_lbl.Text = Sender.Tag
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

        TreeView1.ImageList = My.Application.imgLs

        SetTreeViewTheme(TreeView1.Handle)

        Try
            ApplyDarkMode(Me)
            MakeItDoubleBuffered(Me)
            MakeItDoubleBuffered(TreeView1)

            Me.Size = New Size(My.Application._Settings.MainFormWidth, My.Application._Settings.MainFormHeight)
            Me.WindowState = My.Application._Settings.MainFormStatus

            For Each btn As XenonButton In MainToolbar.Controls.OfType(Of XenonButton)
                AddHandler btn.MouseEnter, AddressOf UpdateHint
                AddHandler btn.Enter, AddressOf UpdateHint
                AddHandler btn.MouseLeave, AddressOf EraseHint
                AddHandler btn.Leave, AddressOf EraseHint
            Next

            For Each btn As XenonRadioImage In previewContainer.Controls.OfType(Of XenonRadioImage)
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


            Select_W11.Image = My.Resources.Native11
            Select_W10.Image = My.Resources.Native10
            Select_W8.Image = My.Resources.Native8
            Select_W7.Image = My.Resources.Native7

            If My.W11 Then PreviewConfig = WinVer.Eleven
            If My.W10 Then PreviewConfig = WinVer.Ten
            If My.W8 Then PreviewConfig = WinVer.Eight
            If My.W7 Then PreviewConfig = WinVer.Seven

            If PreviewConfig = WinVer.Eleven Then
                TablessControl1.SelectedIndex = 0
                XenonButton20.Image = My.Resources.add_win11
                Select_W11.Checked = True

            ElseIf PreviewConfig = WinVer.Ten Then
                TablessControl1.SelectedIndex = 1
                XenonButton20.Image = My.Resources.add_win10
                Select_W10.Checked = True

            ElseIf PreviewConfig = WinVer.Eight Then
                TablessControl1.SelectedIndex = 2
                XenonButton20.Image = My.Resources.add_win8
                Select_W8.Checked = True

            ElseIf PreviewConfig = WinVer.Seven Then
                TablessControl1.SelectedIndex = 3
                XenonButton20.Image = My.Resources.add_win7
                Select_W7.Checked = True

            Else
                TablessControl1.SelectedIndex = 0
                XenonButton20.Image = My.Resources.add_win11
                Select_W11.Checked = True

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

        If ShowWhatsNew Then Whatsnew.ShowDialog()
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
            If My.Application._Settings.ShowSaveConfirmation Then
                Select Case ComplexSave.ShowDialog
                    Case DialogResult.Yes

                        Dim r As String() = My.Application._Settings.ComplexSaveResult.Split(".")
                        Dim r1 As String = r(0)
                        Dim r2 As String = r(1)

                        Select Case r1
                            Case 0              '' Save
                                If IO.File.Exists(SaveFileDialog1.FileName) Then
                                    CP.Save(CP.Mode.File, SaveFileDialog1.FileName)
                                    CP_Original = New CP(Mode.File, SaveFileDialog1.FileName)
                                Else
                                    If SaveFileDialog1.ShowDialog = DialogResult.OK Then
                                        CP.Save(CP.Mode.File, SaveFileDialog1.FileName)
                                        CP_Original = New CP(Mode.File, SaveFileDialog1.FileName)
                                    Else
                                        e.Cancel = True
                                    End If
                                End If
                            Case 1              '' Save As
                                If SaveFileDialog1.ShowDialog = DialogResult.OK Then
                                    CP.Save(CP.Mode.File, SaveFileDialog1.FileName)
                                    CP_Original = New CP(Mode.File, SaveFileDialog1.FileName)
                                Else
                                    e.Cancel = True
                                End If
                        End Select

                        Select Case r2
                            Case 1      '' Apply   ' Case 0= Don't Apply
                                log_lbl.Visible = False
                                log_lbl.Text = ""
                                XenonButton14.Visible = False
                                TablessControl1.SelectedIndex = TablessControl1.TabCount - 1
                                CP.Save(CP.Mode.Registry, "", TreeView1)
                                If My.Application._Settings.AutoRestartExplorer Then
                                    RestartExplorer(TreeView1)
                                Else
                                    CP.AddNode(TreeView1, My.Application.LanguageHelper.NoDefResExplorer, "warning")
                                End If
                                CP.AddNode(TreeView1, String.Format("{0}: All operations are done", Now.ToLongTimeString), "info")
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
        Else
            e.Cancel = False
            MyBase.OnFormClosing(e)
        End If
    End Sub


#Region "Windows 11"
    Private Sub W11_ActiveTitlebar_pick_Click(sender As Object, e As EventArgs) Handles W11_ActiveTitlebar_pick.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                CP.Windows11.Titlebar_Active = sender.BackColor
                ApplyLivePreviewFromCP(CP)
            End If
            Exit Sub
        End If

        Dim CList As New List(Of Control) From {sender, XenonWindow1}

        Dim C As Color = ColorPickerDlg.Pick(CList)
        CP.Windows11.Titlebar_Active = Color.FromArgb(255, C)
        ApplyLivePreviewFromCP(CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()

        If Not CP.Windows11.ApplyAccentonTitlebars Then
            Notify(My.Application.LanguageHelper.CP_TitlebarToggle, My.Resources.notify_info, 4000)
        End If
    End Sub

    Private Sub W11_InactiveTitlebar_pick_Click(sender As Object, e As EventArgs) Handles W11_InactiveTitlebar_pick.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                CP.Windows11.Titlebar_Inactive = sender.BackColor
                ApplyLivePreviewFromCP(CP)
            End If
            Exit Sub
        End If

        Dim CList As New List(Of Control) From {sender, XenonWindow2}

        Dim _Conditions As New Conditions With {.Window_InactiveTitlebar = True}
        Dim C As Color = ColorPickerDlg.Pick(CList, _Conditions)

        CP.Windows11.Titlebar_Inactive = Color.FromArgb(255, C)

        ApplyLivePreviewFromCP(CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()

        If Not CP.Windows11.ApplyAccentonTitlebars Then
            Notify(My.Application.LanguageHelper.CP_TitlebarToggle, My.Resources.notify_info, 4000)
        End If
    End Sub

    Private Sub W11_WinMode_Toggle_CheckedChanged(sender As Object, e As EventArgs) Handles W11_WinMode_Toggle.CheckedChanged
        If _Shown Then
            CP.Windows11.WinMode_Light = Not sender.Checked
            ApplyLivePreviewFromCP(CP)
        End If
    End Sub

    Private Sub W11_AppMode_Toggle_CheckedChanged(sender As Object, e As EventArgs) Handles W11_AppMode_Toggle.CheckedChanged
        If _Shown Then
            CP.Windows11.AppMode_Light = Not sender.Checked
            ApplyLivePreviewFromCP(CP)
        End If
    End Sub

    Private Sub W11_Transparency_Toggle_CheckedChanged(sender As Object, e As EventArgs) Handles W11_Transparency_Toggle.CheckedChanged
        If _Shown Then
            CP.Windows11.Transparency = sender.Checked
            ApplyLivePreviewFromCP(CP)
        End If
    End Sub

    Private Sub W11_ShowAccentOnTitlebarAndBorders_Toggle_CheckedChanged(sender As Object, e As EventArgs) Handles W11_ShowAccentOnTitlebarAndBorders_Toggle.CheckedChanged
        If _Shown Then
            CP.Windows11.ApplyAccentonTitlebars = sender.Checked
            ApplyLivePreviewFromCP(CP)
        End If
    End Sub

    Private Sub W11_Accent_None_CheckedChanged(sender As Object) Handles W11_Accent_None.CheckedChanged
        If _Shown Then
            CP.Windows11.ApplyAccentonTaskbar = ApplyAccentonTaskbar_Level.None
            ApplyLivePreviewFromCP(CP)
        End If
    End Sub

    Private Sub W11_Accent_Taskbar_CheckedChanged(sender As Object) Handles W11_Accent_Taskbar.CheckedChanged
        If _Shown Then
            CP.Windows11.ApplyAccentonTaskbar = ApplyAccentonTaskbar_Level.Taskbar
            ApplyLivePreviewFromCP(CP)
        End If
    End Sub

    Private Sub W11_Accent_StartTaskbar_CheckedChanged(sender As Object) Handles W11_Accent_StartTaskbar.CheckedChanged
        If _Shown Then
            CP.Windows11.ApplyAccentonTaskbar = ApplyAccentonTaskbar_Level.Taskbar_Start_AC
            ApplyLivePreviewFromCP(CP)
        End If
    End Sub

    Private Sub W11_Color_Index1_Click(sender As Object, e As EventArgs) Handles W11_Color_Index1.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                CP.Windows11.Color_Index1 = sender.BackColor
                ApplyLivePreviewFromCP(CP)
            End If
            Exit Sub
        End If

        Dim CList As New List(Of Control)
        Dim C As Color
        CList.Add(sender)


        CList.Add(taskbar)

        If Not CP.Windows11.WinMode_Light Then
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


        CP.Windows11.Color_Index1 = Color.FromArgb(255, C)
        ApplyLivePreviewFromCP(CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()
    End Sub

    Private Sub W11_TaskbarFrontAndFoldersOnStart_pick_Click(sender As Object, e As EventArgs) Handles W11_TaskbarFrontAndFoldersOnStart_pick.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                CP.Windows11.Color_Index5 = sender.BackColor
                ApplyLivePreviewFromCP(CP)
            End If
            Exit Sub
        End If

        Dim CList As New List(Of Control) From {sender}

        If Not CP.Windows11.WinMode_Light Then
            CList.Add(taskbar)
            CList.Add(start)
            CList.Add(ActionCenter)
        Else
            CList.Add(lnk_preview)
        End If

        Dim C As Color = ColorPickerDlg.Pick(CList)
        CP.Windows11.Color_Index5 = Color.FromArgb(255, C)
        ApplyLivePreviewFromCP(CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()

    End Sub

    Private Sub W11_Color_Index0_Click(sender As Object, e As EventArgs) Handles W11_Color_Index0.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                CP.Windows11.Color_Index0 = sender.BackColor
                ApplyLivePreviewFromCP(CP)
            End If
            Exit Sub
        End If

        Dim CList As New List(Of Control)
        Dim C As Color

        CList.Add(sender)

        If CP.Windows11.WinMode_Light Then
            CList.Add(start)
            CList.Add(ActionCenter)
            C = ColorPickerDlg.Pick(CList)
        Else
            CList.Add(lnk_preview)
            Dim _Conditions As New Conditions With {.AppUnderlineOnly = True}
            C = ColorPickerDlg.Pick(CList, _Conditions)
        End If

        CP.Windows11.Color_Index0 = Color.FromArgb(255, C)
        ApplyLivePreviewFromCP(CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()

    End Sub

    Private Sub W11_Color_Index3_Click(sender As Object, e As EventArgs) Handles W11_Color_Index3.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                CP.Windows11.Color_Index3 = sender.BackColor
                ApplyLivePreviewFromCP(CP)
            End If
            Exit Sub
        End If

        Dim CList As New List(Of Control)
        Dim C As Color

        CList.Add(sender)

        CList.Add(setting_icon_preview)
        C = ColorPickerDlg.Pick(CList)

        CP.Windows11.Color_Index3 = Color.FromArgb(255, C)
        ApplyLivePreviewFromCP(CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()
    End Sub

    Private Sub W11_Color_Index6_Click(sender As Object, e As EventArgs) Handles W11_Color_Index6.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                CP.Windows11.Color_Index6 = sender.BackColor
                ApplyLivePreviewFromCP(CP)
            End If
            Exit Sub
        End If

        Dim CList As New List(Of Control)
        Dim C As Color

        CList.Add(sender)

        CList.Add(taskbar)
        Dim _Conditions As New Conditions With {.AppBackgroundOnly = True}
        C = ColorPickerDlg.Pick(CList, _Conditions)

        CP.Windows11.Color_Index6 = Color.FromArgb(255, C)
        ApplyLivePreviewFromCP(CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()
    End Sub

    Private Sub W11_Color_Index5_Click(sender As Object, e As EventArgs) Handles W11_Color_Index5.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                CP.Windows11.StartMenu_Accent = sender.BackColor
                ApplyLivePreviewFromCP(CP)
            End If
            Exit Sub
        End If


        Dim CList As New List(Of Control) From {sender}

        CList.Add(start)

        Dim C As Color = ColorPickerDlg.Pick(CList)
        CP.Windows11.StartMenu_Accent = Color.FromArgb(255, C)
        ApplyLivePreviewFromCP(CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()

    End Sub

    Private Sub W11_Color_Index4_pick_Click(sender As Object, e As EventArgs) Handles W11_Color_Index4.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                CP.Windows11.Color_Index2 = sender.BackColor
                ApplyLivePreviewFromCP(CP)
            End If
            Exit Sub
        End If


        Dim CList As New List(Of Control) From {sender}

        Dim C As Color = ColorPickerDlg.Pick(CList)
        CP.Windows11.Color_Index2 = Color.FromArgb(255, C)
        ApplyLivePreviewFromCP(CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()
    End Sub

    Private Sub W11_Color_Index2_Click(sender As Object, e As EventArgs) Handles W11_Color_Index2.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                CP.Windows11.Color_Index4 = sender.BackColor
                ApplyLivePreviewFromCP(CP)
            End If
            Exit Sub
        End If


        Dim CList As New List(Of Control) From {sender}

        Dim C As Color


        If Not W11_Transparency_Toggle.Checked Then
            CList.Add(taskbar)
            CList.Add(start)
            CList.Add(ActionCenter)
        End If

        If CP.Windows11.WinMode_Light Then
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

        CP.Windows11.Color_Index4 = Color.FromArgb(255, C)
        ApplyLivePreviewFromCP(CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()
    End Sub

    Private Sub W11_Color_Index7_Click(sender As Object, e As EventArgs) Handles W11_Color_Index7.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            CP.Windows11.Color_Index7 = SubMenu.ShowMenu(sender)
            Exit Sub
        End If

        Dim CList As New List(Of Control) From {sender}
        CList.Add(sender)
        Dim C As Color = ColorPickerDlg.Pick(CList)

        CP.Windows11.Color_Index7 = Color.FromArgb(255, C)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()
    End Sub

    Private Sub W11_XenonButton8_Click_1(sender As Object, e As EventArgs) Handles W11_XenonButton8.Click
        MsgBox(My.Application.LanguageHelper.X23, MsgBoxStyle.Information + My.Application.MsgboxRt)
    End Sub

    Private Sub W11_XenonButton25_Click(sender As Object, e As EventArgs) Handles W11_XenonButton25.Click
        MsgBox(My.Application.LanguageHelper.CP_AccentOnTaskbarTib, MsgBoxStyle.Information + My.Application.MsgboxRt)
    End Sub

#End Region

#Region "Windows 10"
    Private Sub W10_ActiveTitlebar_pick_Click(sender As Object, e As EventArgs) Handles W10_ActiveTitlebar_pick.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                CP.Windows10.Titlebar_Active = sender.BackColor
                ApplyLivePreviewFromCP(CP)
            End If
            Exit Sub
        End If

        Dim CList As New List(Of Control) From {sender, XenonWindow1}

        Dim C As Color = ColorPickerDlg.Pick(CList)
        CP.Windows10.Titlebar_Active = Color.FromArgb(255, C)
        ApplyLivePreviewFromCP(CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()

        If Not CP.Windows10.ApplyAccentonTitlebars Then
            Notify(My.Application.LanguageHelper.CP_TitlebarToggle, My.Resources.notify_info, 4000)
        End If
    End Sub

    Private Sub W10_InactiveTitlebar_pick_Click(sender As Object, e As EventArgs) Handles W10_InactiveTitlebar_pick.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                CP.Windows10.Titlebar_Inactive = sender.BackColor
                ApplyLivePreviewFromCP(CP)
            End If
            Exit Sub
        End If

        Dim CList As New List(Of Control) From {sender, XenonWindow2}

        Dim _Conditions As New Conditions With {.Window_InactiveTitlebar = True}
        Dim C As Color = ColorPickerDlg.Pick(CList, _Conditions)

        CP.Windows10.Titlebar_Inactive = Color.FromArgb(255, C)

        ApplyLivePreviewFromCP(CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()

        If Not CP.Windows10.ApplyAccentonTitlebars Then
            Notify(My.Application.LanguageHelper.CP_TitlebarToggle, My.Resources.notify_info, 4000)
        End If
    End Sub

    Private Sub W10_WinMode_Toggle_CheckedChanged(sender As Object, e As EventArgs) Handles W10_WinMode_Toggle.CheckedChanged
        If _Shown Then
            CP.Windows10.WinMode_Light = Not sender.Checked
            ApplyLivePreviewFromCP(CP)
        End If
    End Sub

    Private Sub W10_AppMode_Toggle_CheckedChanged(sender As Object, e As EventArgs) Handles W10_AppMode_Toggle.CheckedChanged
        If _Shown Then
            CP.Windows10.AppMode_Light = Not sender.Checked
            ApplyLivePreviewFromCP(CP)
        End If
    End Sub

    Private Sub W10_Transparency_Toggle_CheckedChanged(sender As Object, e As EventArgs) Handles W10_Transparency_Toggle.CheckedChanged
        If _Shown Then
            CP.Windows10.Transparency = sender.Checked
            ApplyLivePreviewFromCP(CP)
        End If
    End Sub

    Private Sub W10_ShowAccentOnTitlebarAndBorders_Toggle_CheckedChanged(sender As Object, e As EventArgs) Handles W10_ShowAccentOnTitlebarAndBorders_Toggle.CheckedChanged
        If _Shown Then
            CP.Windows10.ApplyAccentonTitlebars = sender.Checked
            ApplyLivePreviewFromCP(CP)
        End If
    End Sub

    Private Sub W10_Accent_None_CheckedChanged(sender As Object) Handles W10_Accent_None.CheckedChanged
        If _Shown Then
            CP.Windows10.ApplyAccentonTaskbar = ApplyAccentonTaskbar_Level.None
            ApplyLivePreviewFromCP(CP)
        End If
    End Sub

    Private Sub W10_Accent_Taskbar_CheckedChanged(sender As Object) Handles W10_Accent_Taskbar.CheckedChanged
        If _Shown Then
            CP.Windows10.ApplyAccentonTaskbar = ApplyAccentonTaskbar_Level.Taskbar
            ApplyLivePreviewFromCP(CP)
        End If
    End Sub

    Private Sub W10_Accent_StartTaskbar_CheckedChanged(sender As Object) Handles W10_Accent_StartTaskbar.CheckedChanged
        If _Shown Then
            CP.Windows10.ApplyAccentonTaskbar = ApplyAccentonTaskbar_Level.Taskbar_Start_AC
            ApplyLivePreviewFromCP(CP)
        End If
    End Sub

    Private Sub W10_Color_Index1_Click(sender As Object, e As EventArgs) Handles W10_Color_Index1.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                CP.Windows10.Color_Index1 = sender.BackColor
                ApplyLivePreviewFromCP(CP)
            End If
            Exit Sub
        End If

        Dim CList As New List(Of Control)
        Dim C As Color
        CList.Add(sender)

        Dim _Conditions As New Conditions

        Select Case Not CP.Windows10.WinMode_Light
            Case True
                CList.Add(taskbar)  ''AppUnderline
                _Conditions.AppUnderlineOnly = True
            Case False
                If CP.Windows10.ApplyAccentonTaskbar <> ApplyAccentonTaskbar_Level.None Then
                    CList.Add(taskbar)  ''AppUnderline
                    _Conditions.AppUnderlineOnly = True
                End If
        End Select

        C = ColorPickerDlg.Pick(CList, _Conditions)

        CP.Windows10.Color_Index1 = Color.FromArgb(255, C)
        ApplyLivePreviewFromCP(CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()
    End Sub

    Private Sub W10_TaskbarFrontAndFoldersOnStart_pick_Click(sender As Object, e As EventArgs) Handles W10_TaskbarFrontAndFoldersOnStart_pick.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                CP.Windows10.Color_Index5 = sender.BackColor
                ApplyLivePreviewFromCP(CP)
            End If
            Exit Sub
        End If

        Dim CList As New List(Of Control) From {sender}

        If [CP].Windows10.Transparency Then
            'CList.Add(start) ''Hamburger
        Else
            CList.Add(taskbar)
        End If

        Dim C As Color = ColorPickerDlg.Pick(CList)
        CP.Windows10.Color_Index5 = Color.FromArgb(255, C)
        ApplyLivePreviewFromCP(CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()

    End Sub

    Private Sub W10_Color_Index0_Click(sender As Object, e As EventArgs) Handles W10_Color_Index0.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                CP.Windows10.Color_Index0 = sender.BackColor
                ApplyLivePreviewFromCP(CP)
            End If
            Exit Sub
        End If

        Dim CList As New List(Of Control)
        Dim C As Color

        CList.Add(sender)


        Dim _Conditions As New Conditions

        Select Case Not CP.Windows10.WinMode_Light
            Case True
                CList.Add(ActionCenter) ''Link
                _Conditions.ActionCenterLink = True

            Case False
                If [CP].Windows10.ApplyAccentonTaskbar = ApplyAccentonTaskbar_Level.Taskbar_Start_AC Then
                    CList.Add(ActionCenter) ''Link
                    _Conditions.ActionCenterLink = True
                End If
        End Select

        C = ColorPickerDlg.Pick(CList, _Conditions)

        CP.Windows10.Color_Index0 = Color.FromArgb(255, C)
        ApplyLivePreviewFromCP(CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()

    End Sub

    Private Sub W10_Color_Index3_Click(sender As Object, e As EventArgs) Handles W10_Color_Index3.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                CP.Windows10.Color_Index3 = sender.BackColor
                ApplyLivePreviewFromCP(CP)
            End If
            Exit Sub
        End If

        Dim CList As New List(Of Control)
        Dim C As Color

        CList.Add(sender)

        Dim _Conditions As New Conditions

        Select Case Not [CP].Windows10.WinMode_Light
            Case True
                If [CP].Windows10.Transparency Then
                    CList.Add(setting_icon_preview)
                    CList.Add(ActionCenter) : _Conditions.ActionCenterBtn = True
                    CList.Add(lnk_preview)
                    If [CP].Windows10.ApplyAccentonTaskbar <> ApplyAccentonTaskbar_Level.None Then
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

                If [CP].Windows10.ApplyAccentonTaskbar = ApplyAccentonTaskbar_Level.None Then
                    CList.Add(taskbar)  ''AppBackground
                    _Conditions.AppBackgroundOnly = True
                    _Conditions.AppUnderlineOnly = True

                End If
        End Select
        C = ColorPickerDlg.Pick(CList, _Conditions)

        CP.Windows10.Color_Index3 = Color.FromArgb(255, C)
        ApplyLivePreviewFromCP(CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()
    End Sub

    Private Sub W10_Color_Index6_Click(sender As Object, e As EventArgs) Handles W10_Color_Index6.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                CP.Windows10.Color_Index6 = sender.BackColor
                ApplyLivePreviewFromCP(CP)
            End If
            Exit Sub
        End If

        Dim CList As New List(Of Control)
        Dim C As Color

        CList.Add(sender)

        Dim _Conditions As New Conditions

        Select Case Not [CP].Windows10.WinMode_Light
            Case True

                If [CP].Windows10.Transparency Then
                    CList.Add(taskbar)
                End If

            Case False

                If [CP].Windows10.Transparency Then
                    CList.Add(taskbar)

                    If [CP].Windows10.ApplyAccentonTaskbar <> ApplyAccentonTaskbar_Level.Taskbar_Start_AC Then
                        CList.Add(ActionCenter) ''ActionCenterLinks
                        _Conditions.ActionCenterLink = True
                    End If

                Else
                    If [CP].Windows10.ApplyAccentonTaskbar <> ApplyAccentonTaskbar_Level.Taskbar_Start_AC Then
                        CList.Add(ActionCenter) ''ActionCenterLinks
                        _Conditions.ActionCenterLink = True

                    End If
                End If
        End Select

        C = ColorPickerDlg.Pick(CList, _Conditions)

        CP.Windows10.Color_Index6 = Color.FromArgb(255, C)
        ApplyLivePreviewFromCP(CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()
    End Sub

    Private Sub W10_Color_Index5_Click(sender As Object, e As EventArgs) Handles W10_Color_Index5.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                CP.Windows10.StartMenu_Accent = sender.BackColor
                ApplyLivePreviewFromCP(CP)
            End If
            Exit Sub
        End If


        Dim CList As New List(Of Control) From {sender}


        Dim C As Color = ColorPickerDlg.Pick(CList)
        CP.Windows10.StartMenu_Accent = Color.FromArgb(255, C)
        ApplyLivePreviewFromCP(CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()

    End Sub

    Private Sub W10_Color_Index4_pick_Click(sender As Object, e As EventArgs) Handles W10_Color_Index4.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                CP.Windows10.Color_Index2 = sender.BackColor
                ApplyLivePreviewFromCP(CP)
            End If
            Exit Sub
        End If


        Dim CList As New List(Of Control) From {sender}

        If PreviewConfig = WinVer.Ten Then
            'CList.Add(taskbar) 'Start Icon Hover
        End If

        Dim C As Color = ColorPickerDlg.Pick(CList)
        CP.Windows10.Color_Index2 = Color.FromArgb(255, C)
        ApplyLivePreviewFromCP(CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()

    End Sub

    Private Sub W10_Color_Index2_Click(sender As Object, e As EventArgs) Handles W10_Color_Index2.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                CP.Windows10.Color_Index4 = sender.BackColor
                ApplyLivePreviewFromCP(CP)
            End If
            Exit Sub
        End If


        Dim CList As New List(Of Control) From {sender}

        Dim C As Color


        Dim _Conditions As New Conditions

        Select Case Not [CP].Windows10.WinMode_Light
            Case True

                If [CP].Windows10.Transparency Then
                    CList.Add(start)
                    CList.Add(ActionCenter)
                Else
                    CList.Add(start)
                    CList.Add(ActionCenter)
                    CList.Add(taskbar)  'AppBackground
                    _Conditions.AppBackgroundOnly = True
                End If

            Case False
                If [CP].Windows10.Transparency Then
                    CList.Add(start)
                    CList.Add(ActionCenter)
                Else
                    If [CP].Windows10.ApplyAccentonTaskbar = ApplyAccentonTaskbar_Level.None Then
                        CList.Add(start)
                        CList.Add(ActionCenter)
                    ElseIf [CP].Windows10.ApplyAccentonTaskbar = ApplyAccentonTaskbar_Level.Taskbar Then
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

        CP.Windows10.Color_Index4 = Color.FromArgb(255, C)
        ApplyLivePreviewFromCP(CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()
    End Sub

    Private Sub W10_Color_Index7_Click(sender As Object, e As EventArgs) Handles W10_Color_Index7.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            CP.Windows10.Color_Index7 = SubMenu.ShowMenu(sender)
            Exit Sub
        End If

        Dim CList As New List(Of Control) From {sender}
        CList.Add(sender)
        Dim C As Color = ColorPickerDlg.Pick(CList)

        CP.Windows10.Color_Index7 = Color.FromArgb(255, C)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()
    End Sub

    Private Sub W10_XenonButton8_Click_1(sender As Object, e As EventArgs) Handles W10_XenonButton8.Click
        MsgBox(My.Application.LanguageHelper.X23, MsgBoxStyle.Information + My.Application.MsgboxRt)
    End Sub

    Private Sub W10_XenonButton25_Click(sender As Object, e As EventArgs) Handles W10_XenonButton25.Click
        MsgBox(My.Application.LanguageHelper.CP_AccentOnTaskbarTib, MsgBoxStyle.Information + My.Application.MsgboxRt)
    End Sub

#End Region

#Region "Windows 7"
    Private Sub W7_ColorizationColor_pick_Click(sender As Object, e As EventArgs) Handles W7_ColorizationColor_pick.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                CP.Windows7.ColorizationColor = sender.BackColor
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

        CP.Windows7.ColorizationColor = Color.FromArgb(255, C)

        ApplyLivePreviewFromCP(CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()
    End Sub

    Private Sub W7_ColorizationAfterglow_pick_Click(sender As Object, e As EventArgs) Handles W7_ColorizationAfterglow_pick.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                CP.Windows7.ColorizationAfterglow = sender.BackColor
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

        CP.Windows7.ColorizationAfterglow = Color.FromArgb(255, C)

        ApplyLivePreviewFromCP(CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()
    End Sub

    Private Sub W7_EnableAeroPeek_toggle_CheckedChanged(sender As Object, e As EventArgs) Handles W7_EnableAeroPeek_toggle.CheckedChanged
        If _Shown Then CP.Windows7.EnableAeroPeek = W7_EnableAeroPeek_toggle.Checked
    End Sub

    Private Sub W7_AlwaysHibernateThumbnails_Toggle_CheckedChanged(sender As Object, e As EventArgs) Handles W7_AlwaysHibernateThumbnails_Toggle.CheckedChanged
        If _Shown Then CP.Windows7.AlwaysHibernateThumbnails = W7_AlwaysHibernateThumbnails_Toggle.Checked
    End Sub

    Private Sub W7_ColorizationColorBalance_bar_Scroll(sender As Object) Handles W7_ColorizationColorBalance_bar.Scroll
        If _Shown Then
            CP.Windows7.ColorizationColorBalance = W7_ColorizationColorBalance_bar.Value
            ApplyLivePreviewFromCP(CP)
        End If
    End Sub

    Private Sub W7_ColorizationBlurBalance_bar_Scroll(sender As Object) Handles W7_ColorizationBlurBalance_bar.Scroll
        If _Shown Then
            CP.Windows7.ColorizationBlurBalance = W7_ColorizationBlurBalance_bar.Value
            ApplyLivePreviewFromCP(CP)
        End If
    End Sub

    Private Sub W7_ColorizationGlassReflectionIntensity_bar_Scroll(sender As Object) Handles W7_ColorizationGlassReflectionIntensity_bar.Scroll
        If _Shown Then
            CP.Windows7.ColorizationGlassReflectionIntensity = W7_ColorizationGlassReflectionIntensity_bar.Value
            ApplyLivePreviewFromCP(CP)
        End If
    End Sub

    Private Sub W7_theme_classic_CheckedChanged(sender As Object) Handles W7_theme_classic.CheckedChanged
        If W7_theme_classic.Checked Then
            CP.Windows7.Theme = CP.AeroTheme.Classic
            ApplyLivePreviewFromCP(CP)
            Notify(My.Application.LanguageHelper.CP_ClassicThemeEditable, My.Resources.notify_warning, 5000)
        End If

    End Sub

    Private Sub W7_theme_basic_CheckedChanged(sender As Object) Handles W7_theme_basic.CheckedChanged
        If W7_theme_basic.Checked Then
            CP.Windows7.Theme = CP.AeroTheme.Basic
            ApplyLivePreviewFromCP(CP)
        End If
    End Sub

    Private Sub W7_theme_aeroopaque_CheckedChanged(sender As Object) Handles W7_theme_aeroopaque.CheckedChanged
        If W7_theme_aeroopaque.Checked Then
            CP.Windows7.Theme = CP.AeroTheme.AeroOpaque
            ApplyLivePreviewFromCP(CP)
        End If
    End Sub

    Private Sub W7_theme_Aero_CheckedChanged(sender As Object) Handles W7_theme_aero.CheckedChanged
        If W7_theme_aero.Checked Then
            CP.Windows7.Theme = CP.AeroTheme.Aero
            ApplyLivePreviewFromCP(CP)
        End If
    End Sub

    Private Sub W7_ColorizationAfterglowBalance_bar_Scroll(sender As Object) Handles W7_ColorizationAfterglowBalance_bar.Scroll
        If _Shown Then
            CP.Windows7.ColorizationAfterglowBalance = W7_ColorizationAfterglowBalance_bar.Value
            ApplyLivePreviewFromCP(CP)
        End If
    End Sub
#End Region

#Region "Windows 8.1"
    Private Sub W8_ColorizationColor_pick_Click(sender As Object, e As EventArgs) Handles W8_ColorizationColor_pick.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                CP.Windows8.ColorizationColor = sender.BackColor
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

        CP.Windows8.ColorizationColor = Color.FromArgb(255, C)

        ApplyLivePreviewFromCP(CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()
    End Sub

    Private Sub W8_ColorizationBalance_bar_Scroll(sender As Object) Handles W8_ColorizationBalance_bar.Scroll
        If _Shown Then
            CP.Windows8.ColorizationColorBalance = W8_ColorizationBalance_bar.Value
            ApplyLivePreviewFromCP(CP)
        End If
    End Sub

    Private Sub W8_start_pick_Click(sender As Object, e As EventArgs) Handles W8_start_pick.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                CP.Windows8.StartColor = sender.BackColor
                ApplyLivePreviewFromCP(CP)
            End If
            Exit Sub
        End If

        Dim CList As New List(Of Control) From {sender}

        Dim C As Color = ColorPickerDlg.Pick(CList)

        CP.Windows8.StartColor = Color.FromArgb(255, C)

        ApplyLivePreviewFromCP(CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()
    End Sub

    Private Sub W8_accent_pick_Click(sender As Object, e As EventArgs) Handles W8_accent_pick.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                CP.Windows8.AccentColor = sender.BackColor
                ApplyLivePreviewFromCP(CP)
            End If
            Exit Sub
        End If

        Dim CList As New List(Of Control) From {sender}

        Dim C As Color = ColorPickerDlg.Pick(CList)

        CP.Windows8.AccentColor = Color.FromArgb(255, C)

        ApplyLivePreviewFromCP(CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()
    End Sub

    Private Sub W8_personalcls_background_pick_Click(sender As Object, e As EventArgs) Handles W8_personalcls_background_pick.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                CP.Windows8.PersonalColors_Background = sender.BackColor
                ApplyLivePreviewFromCP(CP)
            End If
            Exit Sub
        End If

        Dim CList As New List(Of Control) From {sender}

        Dim C As Color = ColorPickerDlg.Pick(CList)

        CP.Windows8.PersonalColors_Background = Color.FromArgb(255, C)

        ApplyLivePreviewFromCP(CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()
    End Sub

    Private Sub W8_personalcolor_accent_pick_Click(sender As Object, e As EventArgs) Handles W8_personalcolor_accent_pick.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                CP.Windows8.PersonalColors_Accent = sender.BackColor
                ApplyLivePreviewFromCP(CP)
            End If
            Exit Sub
        End If

        Dim CList As New List(Of Control) From {sender}

        Dim C As Color = ColorPickerDlg.Pick(CList)

        CP.Windows8.PersonalColors_Accent = Color.FromArgb(255, C)

        ApplyLivePreviewFromCP(CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()
    End Sub

    Private Sub W8_theme_aero_CheckedChanged(sender As Object) Handles W8_theme_aero.CheckedChanged
        If W8_theme_aero.Checked Then
            CP.Windows8.Theme = CP.AeroTheme.Aero
            ApplyLivePreviewFromCP(CP)
        End If
    End Sub

    Private Sub W8_theme_aerolite_CheckedChanged(sender As Object) Handles W8_theme_aerolite.CheckedChanged
        If W8_theme_aerolite.Checked Then
            CP.Windows8.Theme = CP.AeroTheme.AeroLite
            ApplyLivePreviewFromCP(CP)
        End If
    End Sub

    Private Sub W8_start_Click(sender As Object, e As EventArgs) Handles W8_start.Click
        Start8Selector.ShowDialog()
        ApplyLivePreviewFromCP(CP)
    End Sub

    Private Sub W8_logonui_Click(sender As Object, e As EventArgs) Handles W8_logonui.Click
        LogonUI8Colors.ShowDialog()
        ApplyLivePreviewFromCP(CP)
    End Sub
#End Region

    Private Sub XenonButton4_Click(sender As Object, e As EventArgs) Handles apply_btn.Click
        Cursor = Cursors.WaitCursor

        log_lbl.Visible = False
        log_lbl.Text = ""
        XenonButton14.Visible = False

        TablessControl1.SelectedIndex = TablessControl1.TabCount - 1

        CP.Save(CP.Mode.Registry, "", TreeView1)

        CP_Original = New CP(Mode.Registry)

        Cursor = Cursors.Default

        If My.Application._Settings.AutoRestartExplorer Then
            RestartExplorer(TreeView1)
        Else
            CP.AddNode(TreeView1, My.Application.LanguageHelper.NoDefResExplorer, "warning")
        End If

        CP.AddNode(TreeView1, String.Format("{0}: All operations are done", Now.ToLongTimeString), "info")

        If Not My.Application.Saving_Exceptions.Count = 0 Then
            log_lbl.Visible = True
            log_lbl.Text = "Error\s happened. Press on Show Errors for details"
            XenonButton14.Visible = True
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

#Region "Drag And Drop"
    Dim wpth_or_wpsf As Boolean = True
    Dim DragAccepted As Boolean

    Private Sub Me_DragEnter(ByVal sender As Object, ByVal e As DragEventArgs) Handles Me.DragEnter, previewContainer.DragEnter, pnl_preview.DragEnter,
        PaletteContainer_W11.DragEnter, XenonGroupBox5.DragEnter, XenonGroupBox1.DragEnter, MainToolbar.DragEnter, XenonGroupBox13.DragEnter

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
        PaletteContainer_W11.DragOver, XenonGroupBox5.DragOver, XenonGroupBox1.DragOver, MainToolbar.DragOver, XenonGroupBox13.DragOver
        If DragAccepted And My.Application._Settings.DragAndDropPreview Then dragPreviewer.Location = New Point(e.X + 15, e.Y + 15)
    End Sub

    Private Sub MainFrm_DragDrop(sender As Object, e As DragEventArgs) Handles Me.DragDrop, previewContainer.DragDrop, pnl_preview.DragDrop,
        PaletteContainer_W11.DragDrop, XenonGroupBox5.DragDrop, XenonGroupBox1.DragDrop, MainToolbar.DragDrop, XenonGroupBox13.DragDrop

        If DragAccepted Then
            Dim files() As String = e.Data.GetData(DataFormats.FileDrop)

            If wpth_or_wpsf Then
                If My.Application._Settings.DragAndDropPreview Then dragPreviewer.Close()

                If Not CP.Equals(CP_Original) Then
                    If My.Application._Settings.ShowSaveConfirmation Then
                        Select Case ComplexSave.ShowDialog
                            Case DialogResult.Yes

                                Dim r As String() = My.Application._Settings.ComplexSaveResult.Split(".")
                                Dim r1 As String = r(0)
                                Dim r2 As String = r(1)

                                Select Case r1
                                    Case 0              '' Save
                                        If IO.File.Exists(SaveFileDialog1.FileName) Then
                                            CP.Save(CP.Mode.File, SaveFileDialog1.FileName)
                                            CP_Original = New CP(Mode.File, SaveFileDialog1.FileName)
                                        Else
                                            If SaveFileDialog1.ShowDialog = DialogResult.OK Then
                                                CP.Save(CP.Mode.File, SaveFileDialog1.FileName)
                                                CP_Original = New CP(Mode.File, SaveFileDialog1.FileName)
                                            Else
                                                '''''''' If My.Application._Settings.DragPreview then ReleaseBlur()
                                                Exit Sub
                                            End If
                                        End If
                                    Case 1              '' Save As
                                        If SaveFileDialog1.ShowDialog = DialogResult.OK Then
                                            CP.Save(CP.Mode.File, SaveFileDialog1.FileName)
                                            CP_Original = New CP(Mode.File, SaveFileDialog1.FileName)
                                        Else
                                            '''''''' If My.Application._Settings.DragPreview then ReleaseBlur()
                                            Exit Sub
                                        End If
                                End Select

                                Select Case r2
                                    Case 1      '' Apply   ' Case 0= Don't Apply
                                        log_lbl.Visible = False
                                        log_lbl.Text = ""
                                        XenonButton14.Visible = False
                                        TablessControl1.SelectedIndex = TablessControl1.TabCount - 1
                                        CP.Save(CP.Mode.Registry, "", TreeView1)
                                        If My.Application._Settings.AutoRestartExplorer Then
                                            RestartExplorer(TreeView1)
                                        Else
                                            CP.AddNode(TreeView1, My.Application.LanguageHelper.NoDefResExplorer, "warning")
                                        End If
                                        CP.AddNode(TreeView1, String.Format("{0}: All operations are done", Now.ToLongTimeString), "info")
                                End Select

                            Case DialogResult.No

                            Case DialogResult.Cancel
                                Exit Sub
                        End Select
                    End If
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
#End Region


    Private Sub XenonButton7_Click(sender As Object, e As EventArgs) Handles XenonButton7.Click
        If Not IO.File.Exists(SaveFileDialog1.FileName) Then
            If SaveFileDialog1.ShowDialog = DialogResult.OK Then
                CP.Save(CP.Mode.File, SaveFileDialog1.FileNames(0))
            End If
        Else
            CP.Save(CP.Mode.File, SaveFileDialog1.FileNames(0))
        End If
    End Sub

    Private Sub XenonButton2_Click(sender As Object, e As EventArgs) Handles XenonButton2.Click
        If OpenFileDialog1.ShowDialog = DialogResult.OK Then
            If My.Application._Settings.ShowSaveConfirmation Then
                Select Case ComplexSave.ShowDialog
                    Case DialogResult.Yes

                        Dim r As String() = My.Application._Settings.ComplexSaveResult.Split(".")
                        Dim r1 As String = r(0)
                        Dim r2 As String = r(1)

                        Select Case r1
                            Case 0              '' Save
                                If IO.File.Exists(SaveFileDialog1.FileName) Then
                                    CP.Save(CP.Mode.File, SaveFileDialog1.FileName)
                                    CP_Original = New CP(Mode.File, SaveFileDialog1.FileName)
                                Else
                                    If SaveFileDialog1.ShowDialog = DialogResult.OK Then
                                        CP.Save(CP.Mode.File, SaveFileDialog1.FileName)
                                        CP_Original = New CP(Mode.File, SaveFileDialog1.FileName)
                                    Else
                                        Exit Sub
                                    End If
                                End If
                            Case 1              '' Save As
                                If SaveFileDialog1.ShowDialog = DialogResult.OK Then
                                    CP.Save(CP.Mode.File, SaveFileDialog1.FileName)
                                    CP_Original = New CP(Mode.File, SaveFileDialog1.FileName)
                                Else
                                    Exit Sub
                                End If
                        End Select

                        Select Case r2
                            Case 1      '' Apply   ' Case 0= Don't Apply
                                log_lbl.Visible = False
                                log_lbl.Text = ""
                                XenonButton14.Visible = False
                                TablessControl1.SelectedIndex = TablessControl1.TabCount - 1
                                CP.Save(CP.Mode.Registry, "", TreeView1)
                                If My.Application._Settings.AutoRestartExplorer Then
                                    RestartExplorer(TreeView1)
                                Else
                                    CP.AddNode(TreeView1, My.Application.LanguageHelper.NoDefResExplorer, "warning")
                                End If
                                CP.AddNode(TreeView1, String.Format("{0}: All operations are done", Now.ToLongTimeString), "info")
                        End Select

                    Case DialogResult.No

                    Case DialogResult.Cancel
                        Exit Sub
                End Select
            End If

            SaveFileDialog1.FileName = OpenFileDialog1.FileName
            CP = New CP(CP.Mode.File, OpenFileDialog1.FileName)
            ApplyCPValues(CP)
            ApplyLivePreviewFromCP(CP)
        End If
    End Sub

    Private Sub XenonButton9_Click(sender As Object, e As EventArgs) Handles XenonButton9.Click
        If SaveFileDialog1.ShowDialog = DialogResult.OK Then
            CP.Save(CP.Mode.File, SaveFileDialog1.FileNames(0))
        End If
    End Sub

    Private Sub XenonButton3_Click(sender As Object, e As EventArgs) Handles XenonButton3.Click
        If Not CP.Equals(CP_Original) Then
            If My.Application._Settings.ShowSaveConfirmation Then
                Select Case ComplexSave.ShowDialog
                    Case DialogResult.Yes

                        Dim r As String() = My.Application._Settings.ComplexSaveResult.Split(".")
                        Dim r1 As String = r(0)
                        Dim r2 As String = r(1)

                        Select Case r1
                            Case 0              '' Save
                                If IO.File.Exists(SaveFileDialog1.FileName) Then
                                    CP.Save(CP.Mode.File, SaveFileDialog1.FileName)
                                    CP_Original = New CP(Mode.File, SaveFileDialog1.FileName)
                                Else
                                    If SaveFileDialog1.ShowDialog = DialogResult.OK Then
                                        CP.Save(CP.Mode.File, SaveFileDialog1.FileName)
                                        CP_Original = New CP(Mode.File, SaveFileDialog1.FileName)
                                    Else
                                        Exit Sub
                                    End If
                                End If
                            Case 1              '' Save As
                                If SaveFileDialog1.ShowDialog = DialogResult.OK Then
                                    CP.Save(CP.Mode.File, SaveFileDialog1.FileName)
                                    CP_Original = New CP(Mode.File, SaveFileDialog1.FileName)
                                Else
                                    Exit Sub
                                End If
                        End Select

                        Select Case r2
                            Case 1      '' Apply   ' Case 0= Don't Apply
                                log_lbl.Visible = False
                                log_lbl.Text = ""
                                XenonButton14.Visible = False
                                TablessControl1.SelectedIndex = TablessControl1.TabCount - 1
                                CP.Save(CP.Mode.Registry, "", TreeView1)
                                If My.Application._Settings.AutoRestartExplorer Then
                                    RestartExplorer(TreeView1)
                                Else
                                    CP.AddNode(TreeView1, My.Application.LanguageHelper.NoDefResExplorer, "warning")
                                End If
                                CP.AddNode(TreeView1, String.Format("{0}: All operations are done", Now.ToLongTimeString), "info")
                        End Select

                    Case DialogResult.No

                    Case DialogResult.Cancel
                        Exit Sub
                End Select
            End If
        End If

        CP = New CP(CP.Mode.Registry)
        CP_Original = New CP(CP.Mode.Registry)
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
        Win32UI.ShowDialog()
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

    Private Sub XenonButton17_Click(sender As Object, e As EventArgs) Handles XenonButton17.Click
        If Not CP.Equals(CP_Original) Then
            If My.Application._Settings.ShowSaveConfirmation Then
                Select Case ComplexSave.ShowDialog
                    Case DialogResult.Yes

                        Dim r As String() = My.Application._Settings.ComplexSaveResult.Split(".")
                        Dim r1 As String = r(0)
                        Dim r2 As String = r(1)

                        Select Case r1
                            Case 0              '' Save
                                If IO.File.Exists(SaveFileDialog1.FileName) Then
                                    CP.Save(CP.Mode.File, SaveFileDialog1.FileName)
                                    CP_Original = New CP(Mode.File, SaveFileDialog1.FileName)
                                Else
                                    If SaveFileDialog1.ShowDialog = DialogResult.OK Then
                                        CP.Save(CP.Mode.File, SaveFileDialog1.FileName)
                                        CP_Original = New CP(Mode.File, SaveFileDialog1.FileName)
                                    End If
                                End If
                            Case 1              '' Save As
                                If SaveFileDialog1.ShowDialog = DialogResult.OK Then
                                    CP.Save(CP.Mode.File, SaveFileDialog1.FileName)
                                    CP_Original = New CP(Mode.File, SaveFileDialog1.FileName)

                                End If
                        End Select

                        Select Case r2
                            Case 1      '' Apply   ' Case 0= Don't Apply
                                log_lbl.Visible = False
                                log_lbl.Text = ""
                                XenonButton14.Visible = False
                                TablessControl1.SelectedIndex = TablessControl1.TabCount - 1
                                CP.Save(CP.Mode.Registry, "", TreeView1)
                                If My.Application._Settings.AutoRestartExplorer Then
                                    RestartExplorer(TreeView1)
                                Else
                                    CP.AddNode(TreeView1, My.Application.LanguageHelper.NoDefResExplorer, "warning")
                                End If
                                CP.AddNode(TreeView1, String.Format("{0}: All operations are done", Now.ToLongTimeString), "info")
                        End Select

                    Case DialogResult.No

                    Case DialogResult.Cancel
                        Exit Sub
                End Select
            End If
        End If

        CP = CP_Original
        ApplyCPValues(CP)
        ApplyLivePreviewFromCP(CP)
    End Sub

    Private Sub XenonButton18_Click(sender As Object, e As EventArgs) Handles XenonButton18.Click
        If Not CP.Equals(CP_Original) Then
            If My.Application._Settings.ShowSaveConfirmation Then
                Select Case ComplexSave.ShowDialog
                    Case DialogResult.Yes

                        Dim r As String() = My.Application._Settings.ComplexSaveResult.Split(".")
                        Dim r1 As String = r(0)
                        Dim r2 As String = r(1)

                        Select Case r1
                            Case 0              '' Save
                                If IO.File.Exists(SaveFileDialog1.FileName) Then
                                    CP.Save(CP.Mode.File, SaveFileDialog1.FileName)
                                    CP_Original = New CP(Mode.File, SaveFileDialog1.FileName)
                                Else
                                    If SaveFileDialog1.ShowDialog = DialogResult.OK Then
                                        CP.Save(CP.Mode.File, SaveFileDialog1.FileName)
                                        CP_Original = New CP(Mode.File, SaveFileDialog1.FileName)
                                    End If
                                End If
                            Case 1              '' Save As
                                If SaveFileDialog1.ShowDialog = DialogResult.OK Then
                                    CP.Save(CP.Mode.File, SaveFileDialog1.FileName)
                                    CP_Original = New CP(Mode.File, SaveFileDialog1.FileName)

                                End If
                        End Select

                        Select Case r2
                            Case 1      '' Apply   ' Case 0= Don't Apply
                                log_lbl.Visible = False
                                log_lbl.Text = ""
                                XenonButton14.Visible = False
                                TablessControl1.SelectedIndex = TablessControl1.TabCount - 1
                                CP.Save(CP.Mode.Registry, "", TreeView1)
                                If My.Application._Settings.AutoRestartExplorer Then
                                    RestartExplorer(TreeView1)
                                Else
                                    CP.AddNode(TreeView1, My.Application.LanguageHelper.NoDefResExplorer, "warning")
                                End If
                                CP.AddNode(TreeView1, String.Format("{0}: All operations are done", Now.ToLongTimeString), "info")
                        End Select

                    Case DialogResult.No

                    Case DialogResult.Cancel
                        Exit Sub
                End Select
            End If
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
            If My.Application._Settings.ShowSaveConfirmation Then
                Select Case ComplexSave.ShowDialog
                    Case DialogResult.Yes

                        Dim r As String() = My.Application._Settings.ComplexSaveResult.Split(".")
                        Dim r1 As String = r(0)
                        Dim r2 As String = r(1)

                        Select Case r1
                            Case 0              '' Save
                                If IO.File.Exists(SaveFileDialog1.FileName) Then
                                    CP.Save(CP.Mode.File, SaveFileDialog1.FileName)
                                    CP_Original = New CP(Mode.File, SaveFileDialog1.FileName)
                                Else
                                    If SaveFileDialog1.ShowDialog = DialogResult.OK Then
                                        CP.Save(CP.Mode.File, SaveFileDialog1.FileName)
                                        CP_Original = New CP(Mode.File, SaveFileDialog1.FileName)
                                    Else
                                        Exit Sub
                                    End If
                                End If
                            Case 1              '' Save As
                                If SaveFileDialog1.ShowDialog = DialogResult.OK Then
                                    CP.Save(CP.Mode.File, SaveFileDialog1.FileName)
                                    CP_Original = New CP(Mode.File, SaveFileDialog1.FileName)
                                Else
                                    Exit Sub
                                End If
                        End Select

                        Select Case r2
                            Case 1      '' Apply   ' Case 0= Don't Apply
                                log_lbl.Visible = False
                                log_lbl.Text = ""
                                XenonButton14.Visible = False
                                TablessControl1.SelectedIndex = TablessControl1.TabCount - 1
                                CP.Save(CP.Mode.Registry, "", TreeView1)
                                If My.Application._Settings.AutoRestartExplorer Then
                                    RestartExplorer(TreeView1)
                                Else
                                    CP.AddNode(TreeView1, My.Application.LanguageHelper.NoDefResExplorer, "warning")
                                End If
                                CP.AddNode(TreeView1, String.Format("{0}: All operations are done", Now.ToLongTimeString), "info")
                        End Select

                    Case DialogResult.No

                    Case DialogResult.Cancel
                        Exit Sub
                End Select
            End If
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

    Private Sub XenonButton23_Click(sender As Object, e As EventArgs) Handles XenonButton23.Click
        If XenonButton23.Text.ToLower = My.Application.LanguageHelper.Hide.ToLower Then
            pnl_preview.Visible = False
            XenonButton23.Text = My.Application.LanguageHelper.Show
        Else
            pnl_preview.Visible = True
            XenonButton23.Text = My.Application.LanguageHelper.Hide
        End If
    End Sub

    Private Sub XenonButton24_Click(sender As Object, e As EventArgs) Handles XenonButton24.Click
        TerminalsDashboard.ShowDialog()
    End Sub

    Private Sub MainFrm_ResizeBegin(sender As Object, e As EventArgs) Handles Me.ResizeBegin
        pnl_preview.Visible = False
    End Sub

    Private Sub MainFrm_ResizeEnd(sender As Object, e As EventArgs) Handles Me.ResizeEnd
        pnl_preview.Visible = True
    End Sub

    Private Sub XenonButton27_Click(sender As Object, e As EventArgs) Handles XenonButton27.Click
        Metrics_Fonts.ShowDialog()
    End Sub

    Sub Select_Preview_Version()
        Adjust_Preview()
        ApplyDefaultCPValues()
        ApplyLivePreviewFromCP(CP)

        If PreviewConfig = WinVer.Eleven Then
            XenonButton20.Image = My.Resources.add_win11
        ElseIf PreviewConfig = WinVer.Ten Then
            XenonButton20.Image = My.Resources.add_win10
        ElseIf PreviewConfig = WinVer.Eight Then
            XenonButton20.Image = My.Resources.add_win8
        ElseIf PreviewConfig = WinVer.Seven Then
            XenonButton20.Image = My.Resources.add_win7
        Else
            XenonButton20.Image = My.Resources.add_win11
        End If

        If PreviewConfig = WinVer.Eleven Then
            TablessControl1.SelectedIndex = 0
        ElseIf PreviewConfig = WinVer.Ten Then
            TablessControl1.SelectedIndex = 1
        ElseIf PreviewConfig = WinVer.Eight Then
            TablessControl1.SelectedIndex = 2
        ElseIf PreviewConfig = WinVer.Seven Then
            TablessControl1.SelectedIndex = 3
        Else
            TablessControl1.SelectedIndex = 0
        End If

    End Sub

    Private Sub Select_W11_CheckedChanged(sender As Object) Handles Select_W11.CheckedChanged
        If _Shown And Select_W11.Checked Then
            PreviewConfig = WinVer.Eleven
            Select_Preview_Version()
        End If
    End Sub

    Private Sub Select_W10_CheckedChanged(sender As Object) Handles Select_W10.CheckedChanged
        If _Shown And Select_W10.Checked Then
            PreviewConfig = WinVer.Ten
            Select_Preview_Version()
        End If
    End Sub

    Private Sub Select_W8_CheckedChanged(sender As Object) Handles Select_W8.CheckedChanged
        If _Shown And Select_W8.Checked Then
            PreviewConfig = WinVer.Eight
            Select_Preview_Version()
        End If
    End Sub

    Private Sub Select_W7_CheckedChanged(sender As Object) Handles Select_W7.CheckedChanged
        If _Shown And Select_W7.Checked Then
            PreviewConfig = WinVer.Seven
            Select_Preview_Version()
        End If
    End Sub

    Private Sub XenonButton14_Click(sender As Object, e As EventArgs) Handles XenonButton14.Click
        Saving_ex_list.ShowDialog()
    End Sub

    Private Sub XenonButton8_Click(sender As Object, e As EventArgs) Handles XenonButton8.Click
        If PreviewConfig = WinVer.Eleven Then
            TablessControl1.SelectedIndex = 0
        ElseIf PreviewConfig = WinVer.Ten Then
            TablessControl1.SelectedIndex = 1
        ElseIf PreviewConfig = WinVer.Eight Then
            TablessControl1.SelectedIndex = 2
        ElseIf PreviewConfig = WinVer.Seven Then
            TablessControl1.SelectedIndex = 3
        Else
            TablessControl1.SelectedIndex = 0
        End If
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
