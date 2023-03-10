Imports System.ComponentModel
Imports System.IO
Imports System.Net
Imports System.Reflection
Imports System.Text
Imports WinPaletter.CP
Imports WinPaletter.XenonCore
Imports Devcorp.Controls.VisualStyles

Public Class MainFrm
    Private _Shown As Boolean = False
    Public CP, CP_Original, CP_FirstTime, CP_BeforeDrag As CP
    Public PreviewConfig As WinVer = WinVer.W11
    Dim RaiseUpdate As Boolean = False
    Dim ver As String = ""
    Dim StableInt, BetaInt, UpdateChannel As Integer
    Dim ChannelFixer As Integer
    Dim Updates_ls As New List(Of String)

#Region "CP Subs"

    Sub ApplyLivePreviewFromCP(ByVal [CP] As CP)
        Dim AnimX1 As Integer = 15
        Dim AnimX2 As Integer = 1

        XenonWindow1.Active = True
        XenonWindow2.Active = False
        If ExplorerPatcher.IsAllowed Then My.EP = New ExplorerPatcher

        Select Case PreviewConfig
            Case WinVer.W11
#Region "Win11"
                tabs_preview.SelectedIndex = 0
                XenonWindow1.AccentColor_Enabled = [CP].Windows11.ApplyAccentonTitlebars
                XenonWindow2.AccentColor_Enabled = [CP].Windows11.ApplyAccentonTitlebars

                XenonWindow1.AccentColor_Active = [CP].Windows11.Titlebar_Active
                XenonWindow2.AccentColor_Active = [CP].Windows11.Titlebar_Active

                XenonWindow1.AccentColor_Inactive = [CP].Windows11.Titlebar_Inactive
                XenonWindow2.AccentColor_Inactive = [CP].Windows11.Titlebar_Inactive

                XenonWindow1.DarkMode = Not [CP].Windows11.AppMode_Light
                XenonWindow2.DarkMode = Not [CP].Windows11.AppMode_Light

                XenonWindow1.Shadow = [CP].WindowsEffects.WindowShadow
                XenonWindow2.Shadow = [CP].WindowsEffects.WindowShadow

                Visual.FadeColor(Label8, "ForeColor", Label8.ForeColor, If([CP].Windows11.AppMode_Light, Color.Black, Color.White), AnimX1, AnimX2)

                W11_lbl5.Text = My.Lang.CP_11_Settings
                W11_lbl6.Text = My.Lang.CP_11_SomePressedButtons
                W11_lbl7.Text = String.Format(My.Lang.CP_UWPBackground, My.Lang.OS_Win11)
                W11_lbl8.Text = My.Lang.CP_Undefined
                W11_lbl9.Text = My.Lang.CP_Undefined
                W11_pic5.Image = My.Resources.Mini_Settings_Icons
                W11_pic6.Image = My.Resources.Mini_PressedButton
                W11_pic7.Image = My.Resources.Mini_UWPDlg
                W11_pic8.Image = My.Resources.Mini_Undefined
                W11_pic9.Image = My.Resources.Mini_Undefined

                Select Case Not [CP].Windows11.WinMode_Light
                    Case True   ''''''''''Dark
                        W11_lbl1.Text = My.Lang.CP_11_StartMenu_Taskbar_AC
                        W11_lbl2.Text = My.Lang.CP_11_ACHover_Links
                        W11_lbl3.Text = My.Lang.CP_11_Lines_Toggles_Buttons
                        W11_lbl4.Text = My.Lang.CP_11_OverflowTray

                        W11_pic1.Image = My.Resources.Mini_StartMenu_Taskbar_AC
                        W11_pic2.Image = My.Resources.Mini_ACHover_Links
                        W11_pic3.Image = My.Resources.Mini_Lines_Toggles_Buttons
                        W11_pic4.Image = My.Resources.Mini_Overflow
                    Case False   ''''''''''Light
                        W11_lbl1.Text = My.Lang.CP_11_ACHover_Links
                        W11_lbl2.Text = My.Lang.CP_11_StartMenu_AC
                        W11_lbl3.Text = My.Lang.CP_11_Taskbar
                        W11_lbl4.Text = My.Lang.CP_11_Lines_Toggles_Buttons_Overflow
                        W11_pic1.Image = My.Resources.Mini_ACHover_Links
                        W11_pic2.Image = My.Resources.Mini_StartMenu_Taskbar_AC
                        W11_pic3.Image = My.Resources.Mini_Taskbar
                        W11_pic4.Image = My.Resources.Mini_Lines_Toggles_Buttons
                End Select

                If ExplorerPatcher.IsAllowed Then
                    Select Case Not [CP].Windows11.WinMode_Light
                        Case True ''''''''''Dark

                            If My.EP.UseTaskbar10 Then
                                W11_lbl5.Text = My.Lang.CP_10_Settings_Links_SomeBtns

                                If My.EP.UseStart10 Then
                                    W11_lbl1.Text = My.Lang.CP_10_Taskbar
                                    W11_pic1.Image = My.Resources.Mini_Taskbar
                                Else
                                    W11_lbl1.Text = My.Lang.CP_11_StartMenu_Taskbar_AC
                                    W11_pic1.Image = My.Resources.Mini_StartMenu_Taskbar_AC
                                End If

                                W11_lbl3.Text = My.Lang.CP_EP_ACButton_TaskbarAppLine
                                W11_lbl6.Text = My.Lang.CP_10_StartMenuIconHover

                                W11_pic3.Image = My.Resources.Mini_AC
                                W11_pic5.Image = My.Resources.Mini_Settings_Icons
                                W11_pic6.Image = My.Resources.Native11
                            End If

                            If My.EP.UseStart10 Then
                                W11_lbl4.Text = My.Lang.CP_EP_StartMenu_OverflowMenus
                                W11_pic4.Image = My.Resources.Mini_StartMenu
                            End If

                        Case False ''''''''''Light

                            If My.EP.UseTaskbar10 Then
                                W11_lbl3.Text = My.Lang.CP_EP_Taskbar_AppUnderline
                                W11_lbl5.Text = My.Lang.CP_10_Settings_Links_SomeBtns
                                W11_lbl6.Text = My.Lang.CP_10_StartMenuIconHover

                                W11_pic3.Image = My.Resources.Mini_TaskbarApp
                                W11_pic5.Image = My.Resources.Mini_Settings_Icons
                                W11_pic6.Image = My.Resources.Native11
                            End If

                            If My.EP.UseStart10 Then
                                W11_lbl2.Text = My.Lang.CP_EP_ActionCenterBackground
                                W11_lbl4.Text = My.Lang.CP_EP_StartMenu_ActionCenterButtons
                                W11_pic2.Image = My.Resources.Mini_AC
                                W11_pic4.Image = My.Resources.Mini_StartMenu_Taskbar_AC
                            End If

                    End Select
                End If

                start.DarkMode = Not [CP].Windows11.WinMode_Light
                taskbar.DarkMode = Not [CP].Windows11.WinMode_Light
                ActionCenter.DarkMode = Not [CP].Windows11.WinMode_Light

                taskbar.Transparency = [CP].Windows11.Transparency
                start.Transparency = [CP].Windows11.Transparency
                ActionCenter.Transparency = [CP].Windows11.Transparency

                Select Case Not [CP].Windows11.WinMode_Light
                    Case True   ''''''''''Dark
                        ActionCenter.BackColorAlpha = 75

                        If ExplorerPatcher.IsAllowed Then
                            If My.EP.UseStart10 Then
                                start.BackColorAlpha = 185
                            Else
                                start.BackColorAlpha = 75
                            End If

                            If My.EP.UseTaskbar10 Then
                                taskbar.BackColorAlpha = 185
                                taskbar.BlurPower = 12
                            Else
                                taskbar.BackColorAlpha = 75
                                taskbar.BlurPower = 12
                            End If
                        Else
                            taskbar.BackColorAlpha = 75
                            taskbar.BlurPower = 12
                            start.BackColorAlpha = 75
                        End If

                        Select Case [CP].Windows11.ApplyAccentonTaskbar
                            Case ApplyAccentonTaskbar_Level.None
                                Visual.FadeColor(taskbar, "BackColor", taskbar.BackColor, Color.FromArgb(55, 55, 55), AnimX1, AnimX2)
                                Visual.FadeColor(start, "BackColor", start.BackColor, Color.FromArgb(40, 40, 40), AnimX1, AnimX2)
                                Visual.FadeColor(ActionCenter, "BackColor", ActionCenter.BackColor, Color.FromArgb(55, 55, 55), AnimX1, AnimX2)

                            Case ApplyAccentonTaskbar_Level.Taskbar_Start_AC
                                Visual.FadeColor(taskbar, "BackColor", taskbar.BackColor, Color.FromArgb(taskbar.BackColor.A, [CP].Windows11.Color_Index5), AnimX1, AnimX2)

                                If ExplorerPatcher.IsAllowed And My.EP.UseStart10 Then
                                    Visual.FadeColor(start, "BackColor", start.BackColor, Color.FromArgb(start.BackColor.A, [CP].Windows11.Color_Index4), AnimX1, AnimX2)
                                Else
                                    Visual.FadeColor(start, "BackColor", start.BackColor, Color.FromArgb(start.BackColor.A, [CP].Windows11.Color_Index5), AnimX1, AnimX2)
                                End If

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

                        Visual.FadeColor(setting_icon_preview, "ForeColor", setting_icon_preview.ForeColor, [CP].Windows11.Color_Index3, AnimX1, AnimX2)
                        Visual.FadeColor(lnk_preview, "ForeColor", lnk_preview.ForeColor, [CP].Windows11.Color_Index0, AnimX1, AnimX2)

                    Case False   ''''''''''Light
                        ActionCenter.BackColorAlpha = 180

                        If ExplorerPatcher.IsAllowed Then
                            If My.EP.UseStart10 Then
                                start.BackColorAlpha = 210
                            Else
                                start.BackColorAlpha = 180
                            End If

                            If My.EP.UseTaskbar10 Then
                                taskbar.BackColorAlpha = 210
                                taskbar.BlurPower = 12
                            Else
                                taskbar.BackColorAlpha = 180
                                taskbar.BlurPower = 12
                            End If
                        Else
                            taskbar.BlurPower = 12
                            taskbar.BackColorAlpha = 180
                            start.BackColorAlpha = 180
                        End If

                        Select Case [CP].Windows11.ApplyAccentonTaskbar
                            Case ApplyAccentonTaskbar_Level.None
                                Visual.FadeColor(taskbar, "BackColor", taskbar.BackColor, Color.FromArgb(255, 255, 255), AnimX1, AnimX2)
                                Visual.FadeColor(start, "BackColor", start.BackColor, Color.FromArgb(255, 255, 255), AnimX1, AnimX2)
                                Visual.FadeColor(ActionCenter, "BackColor", ActionCenter.BackColor, Color.FromArgb(255, 255, 255), AnimX1, AnimX2)

                            Case ApplyAccentonTaskbar_Level.Taskbar_Start_AC
                                Visual.FadeColor(taskbar, "BackColor", taskbar.BackColor, Color.FromArgb(taskbar.BackColor.A, [CP].Windows11.Color_Index1), AnimX1, AnimX2)

                                If ExplorerPatcher.IsAllowed And My.EP.UseStart10 Then
                                    Visual.FadeColor(start, "BackColor", start.BackColor, Color.FromArgb(start.BackColor.A, [CP].Windows11.Color_Index4), AnimX1, AnimX2)
                                Else
                                    Visual.FadeColor(start, "BackColor", start.BackColor, Color.FromArgb(start.BackColor.A, [CP].Windows11.Color_Index0), AnimX1, AnimX2)
                                End If

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

                        If ExplorerPatcher.IsAllowed And My.EP.UseTaskbar10 Then
                            Visual.FadeColor(taskbar, "AppUnderline", taskbar.AppUnderline, [CP].Windows11.Color_Index1, AnimX1, AnimX2)
                        Else
                            Visual.FadeColor(taskbar, "AppUnderline", taskbar.AppUnderline, [CP].Windows11.Color_Index4, AnimX1, AnimX2)
                        End If

                        Visual.FadeColor(setting_icon_preview, "ForeColor", setting_icon_preview.ForeColor, [CP].Windows11.Color_Index3, AnimX1, AnimX2)
                        Visual.FadeColor(lnk_preview, "ForeColor", lnk_preview.ForeColor, [CP].Windows11.Color_Index5, AnimX1, AnimX2)
                End Select
#End Region
            Case WinVer.W10
#Region "Win10"
                tabs_preview.SelectedIndex = 0
                XenonWindow1.AccentColor_Enabled = [CP].Windows10.ApplyAccentonTitlebars
                XenonWindow2.AccentColor_Enabled = [CP].Windows10.ApplyAccentonTitlebars

                XenonWindow1.AccentColor_Active = [CP].Windows10.Titlebar_Active
                XenonWindow2.AccentColor_Active = [CP].Windows10.Titlebar_Active

                XenonWindow1.AccentColor_Inactive = [CP].Windows10.Titlebar_Inactive
                XenonWindow2.AccentColor_Inactive = [CP].Windows10.Titlebar_Inactive

                XenonWindow1.DarkMode = Not [CP].Windows10.AppMode_Light
                XenonWindow2.DarkMode = Not [CP].Windows10.AppMode_Light

                XenonWindow1.Shadow = [CP].WindowsEffects.WindowShadow
                XenonWindow2.Shadow = [CP].WindowsEffects.WindowShadow

                Visual.FadeColor(Label8, "ForeColor", Label8.ForeColor, If([CP].Windows10.AppMode_Light, Color.Black, Color.White), AnimX1, AnimX2)

                W10_lbl9.Text = My.Lang.CP_Undefined

                Select Case Not [CP].Windows10.WinMode_Light
                    Case True ''''''''''Dark
                        W10_lbl2.Text = My.Lang.CP_10_ACLinks
                        W10_lbl3.Text = My.Lang.CP_10_TaskbarAppUnderline
                        W10_lbl5.Text = My.Lang.CP_10_Settings_Links_SomeBtns
                        W10_lbl6.Text = My.Lang.CP_10_StartMenuIconHover
                        W10_lbl7.Text = String.Format(My.Lang.CP_UWPBackground, My.Lang.OS_Win10)

                        W10_pic2.Image = My.Resources.Mini_ACHover_Links
                        W10_pic3.Image = My.Resources.Mini_TaskbarApp
                        W10_pic5.Image = My.Resources.Mini_Settings_Icons
                        W10_pic6.Image = My.Resources.Native10
                        W10_pic7.Image = My.Resources.Mini_UWPDlg

                        If [CP].Windows10.Transparency Then
                            W10_lbl1.Text = My.Lang.CP_10_Hamburger
                            W10_lbl4.Text = My.Lang.CP_10_StartMenu_AC
                            W10_lbl8.Text = My.Lang.CP_10_Taskbar

                            W10_pic1.Image = My.Resources.Mini_Hamburger
                            W10_pic4.Image = My.Resources.Mini_StartMenu_Taskbar_AC
                            W10_pic8.Image = My.Resources.Mini_Taskbar

                            If [CP].Windows10.ApplyAccentonTaskbar <> ApplyAccentonTaskbar_Level.None Then
                                W10_lbl5.Text = My.Lang.CP_10_Settings_Links_Taskbar_SomeBtns
                            End If

                        Else
                            W10_lbl1.Text = My.Lang.CP_10_Taskbar
                            W10_pic1.Image = My.Resources.Mini_Taskbar
                            W10_pic4.Image = My.Resources.Mini_StartMenu_Taskbar_AC

                            If [CP].Windows10.ApplyAccentonTaskbar <> ApplyAccentonTaskbar_Level.None Then
                                W10_lbl4.Text = My.Lang.CP_10_StartMenu_AC_TaskbarActiveApp
                            Else
                                W10_lbl4.Text = My.Lang.CP_10_StartMenu_AC
                            End If

                            W10_lbl8.Text = My.Lang.CP_Undefined
                            W10_pic8.Image = My.Resources.Mini_Undefined

                        End If

                    Case False ''''''''''Light
                        If [CP].Windows10.Transparency Then
                            W10_lbl1.Text = My.Lang.CP_10_Hamburger
                            W10_lbl4.Text = My.Lang.CP_10_StartMenu_AC
                            W10_lbl6.Text = My.Lang.CP_10_StartMenuIconHover
                            W10_lbl7.Text = String.Format(My.Lang.CP_UWPBackground, My.Lang.OS_Win10)

                            W10_pic1.Image = My.Resources.Mini_Hamburger
                            W10_pic4.Image = My.Resources.Mini_StartMenu_Taskbar_AC
                            W10_pic5.Image = My.Resources.Mini_Settings_Icons
                            W10_pic6.Image = My.Resources.Native10
                            W10_pic7.Image = My.Resources.Mini_UWPDlg
                            W10_pic8.Image = My.Resources.Mini_Taskbar

                            If [CP].Windows10.ApplyAccentonTaskbar = ApplyAccentonTaskbar_Level.None Then
                                W10_lbl2.Text = My.Lang.CP_Undefined
                                W10_lbl3.Text = My.Lang.CP_Undefined
                                W10_lbl5.Text = My.Lang.CP_10_Settings_Links_TaskbarUndeline_SomeBtns
                                W10_lbl8.Text = My.Lang.CP_10_Taskbar_ACLinks

                                W10_pic2.Image = My.Resources.Mini_Undefined
                                W10_pic3.Image = My.Resources.Mini_Undefined

                            ElseIf [CP].Windows10.ApplyAccentonTaskbar = ApplyAccentonTaskbar_Level.Taskbar Then
                                W10_lbl2.Text = My.Lang.CP_Undefined
                                W10_lbl3.Text = My.Lang.CP_10_TaskbarAppUnderline
                                W10_lbl5.Text = My.Lang.CP_10_Settings_Links_SomeBtns
                                W10_lbl8.Text = My.Lang.CP_10_Taskbar_ACLinks

                                W10_pic2.Image = My.Resources.Mini_Undefined
                                W10_pic3.Image = My.Resources.Mini_TaskbarApp

                            Else
                                W10_lbl2.Text = My.Lang.CP_10_ACLinks
                                W10_lbl3.Text = My.Lang.CP_10_TaskbarAppUnderline
                                W10_lbl5.Text = My.Lang.CP_10_Settings_Links_SomeBtns
                                W10_lbl8.Text = My.Lang.CP_10_Taskbar

                                W10_pic2.Image = My.Resources.Mini_ACHover_Links
                                W10_pic3.Image = My.Resources.Mini_TaskbarApp

                            End If
                        Else
                            W10_lbl1.Text = My.Lang.CP_10_Taskbar
                            W10_lbl6.Text = My.Lang.CP_10_StartMenuIconHover
                            W10_lbl7.Text = String.Format(My.Lang.CP_UWPBackground, My.Lang.OS_Win10)

                            W10_pic1.Image = My.Resources.Mini_Taskbar
                            W10_pic6.Image = My.Resources.Native10
                            W10_pic7.Image = My.Resources.Mini_UWPDlg

                            If [CP].Windows10.ApplyAccentonTaskbar = ApplyAccentonTaskbar_Level.None Then
                                W10_lbl2.Text = My.Lang.CP_Undefined
                                W10_lbl3.Text = My.Lang.CP_Undefined
                                W10_lbl4.Text = My.Lang.CP_10_StartMenu_AC
                                W10_lbl5.Text = My.Lang.CP_10_Settings_Links_TaskbarUndeline_SomeBtns
                                W10_lbl8.Text = My.Lang.CP_10_ACLinks

                                W10_pic2.Image = My.Resources.Mini_Undefined
                                W10_pic3.Image = My.Resources.Mini_Undefined
                                W10_pic4.Image = My.Resources.Mini_StartMenu_Taskbar_AC
                                W10_pic5.Image = My.Resources.Mini_Settings_Icons
                                W10_pic8.Image = My.Resources.Mini_ACHover_Links

                            ElseIf [CP].Windows10.ApplyAccentonTaskbar = ApplyAccentonTaskbar_Level.Taskbar Then
                                W10_lbl2.Text = My.Lang.CP_Undefined
                                W10_lbl3.Text = My.Lang.CP_10_TaskbarAppUnderline
                                W10_lbl4.Text = My.Lang.CP_10_TaskbarFocusedApp_StartButtonHover
                                W10_lbl5.Text = My.Lang.CP_10_Settings_Links_SomeBtns
                                W10_lbl8.Text = My.Lang.CP_10_ACLinks

                                W10_pic2.Image = My.Resources.Mini_Undefined
                                W10_pic3.Image = My.Resources.Mini_TaskbarApp
                                W10_pic4.Image = My.Resources.Mini_TaskbarActiveIcon
                                W10_pic5.Image = My.Resources.Mini_Settings_Icons
                                W10_pic8.Image = My.Resources.Mini_ACHover_Links

                            Else
                                W10_lbl2.Text = My.Lang.CP_10_ACLinks
                                W10_lbl3.Text = My.Lang.CP_10_TaskbarAppUnderline
                                W10_lbl4.Text = My.Lang.CP_10_StartMenu_AC_TaskbarActiveApp
                                W10_lbl5.Text = My.Lang.CP_10_Settings_Links_SomeBtns
                                W10_lbl8.Text = My.Lang.CP_Undefined

                                W10_pic2.Image = My.Resources.Mini_ACHover_Links
                                W10_pic3.Image = My.Resources.Mini_TaskbarApp
                                W10_pic4.Image = My.Resources.Mini_StartMenu_Taskbar_AC
                                W10_pic5.Image = My.Resources.Mini_Settings_Icons
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

                taskbar.BlurPower = If(Not CP.Windows10.IncreaseTBTransparency, 12, 6)

                If [CP].Windows10.Transparency Then
                    If Not [CP].Windows10.WinMode_Light Then
                        taskbar.BackColorAlpha = If(Not CP.Windows10.IncreaseTBTransparency, 150, 75)
                        start.BackColorAlpha = 150
                        ActionCenter.BackColorAlpha = 150
                    Else
                        taskbar.BackColorAlpha = If(Not CP.Windows10.IncreaseTBTransparency, 200, 125)
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
                                    Visual.FadeColor(setting_icon_preview, "ForeColor", setting_icon_preview.ForeColor, [CP].Windows10.Color_Index3, AnimX1, AnimX2)
                                    Visual.FadeColor(lnk_preview, "ForeColor", lnk_preview.ForeColor, [CP].Windows10.Color_Index3, AnimX1, AnimX2)
                                    Visual.FadeColor(ActionCenter, "ActionCenterButton_Normal", ActionCenter.ActionCenterButton_Normal, [CP].Windows10.Color_Index3, AnimX1, AnimX2)

                                Case ApplyAccentonTaskbar_Level.Taskbar
                                    Visual.FadeColor(taskbar, "BackColor", taskbar.BackColor, [CP].Windows10.Color_Index6, AnimX1, AnimX2)
                                    Visual.FadeColor(taskbar, "StartColor", taskbar.StartColor, Color.FromArgb(0, 0, 0, 0), AnimX1, AnimX2)
                                    Visual.FadeColor(start, "BackColor", start.BackColor, Color.FromArgb(31, 31, 31), AnimX1, AnimX2)
                                    Visual.FadeColor(ActionCenter, "BackColor", ActionCenter.BackColor, Color.FromArgb(31, 31, 31), AnimX1, AnimX2)

                                    Visual.FadeColor(taskbar, "AppBackground", taskbar.AppBackground, Color.FromArgb(150, [CP].Windows10.Color_Index3), AnimX1, AnimX2)
                                    Visual.FadeColor(ActionCenter, "LinkColor", ActionCenter.LinkColor, [CP].Windows10.Color_Index0, AnimX1, AnimX2)
                                    Visual.FadeColor(taskbar, "AppUnderline", taskbar.AppUnderline, [CP].Windows10.Color_Index1, AnimX1, AnimX2)
                                    Visual.FadeColor(setting_icon_preview, "ForeColor", setting_icon_preview.ForeColor, [CP].Windows10.Color_Index3, AnimX1, AnimX2)
                                    Visual.FadeColor(lnk_preview, "ForeColor", lnk_preview.ForeColor, [CP].Windows10.Color_Index3, AnimX1, AnimX2)
                                    Visual.FadeColor(ActionCenter, "ActionCenterButton_Normal", ActionCenter.ActionCenterButton_Normal, [CP].Windows10.Color_Index3, AnimX1, AnimX2)

                                Case ApplyAccentonTaskbar_Level.Taskbar_Start_AC
                                    Visual.FadeColor(taskbar, "BackColor", taskbar.BackColor, [CP].Windows10.Color_Index6, AnimX1, AnimX2)
                                    Visual.FadeColor(taskbar, "StartColor", taskbar.StartColor, Color.FromArgb(0, 0, 0, 0), AnimX1, AnimX2)
                                    Visual.FadeColor(start, "BackColor", start.BackColor, [CP].Windows10.Color_Index4, AnimX1, AnimX2)
                                    Visual.FadeColor(ActionCenter, "BackColor", ActionCenter.BackColor, [CP].Windows10.Color_Index4, AnimX1, AnimX2)

                                    Visual.FadeColor(taskbar, "AppBackground", taskbar.AppBackground, Color.FromArgb(150, [CP].Windows10.Color_Index3), AnimX1, AnimX2)
                                    Visual.FadeColor(ActionCenter, "LinkColor", ActionCenter.LinkColor, [CP].Windows10.Color_Index0, AnimX1, AnimX2)
                                    Visual.FadeColor(taskbar, "AppUnderline", taskbar.AppUnderline, [CP].Windows10.Color_Index1, AnimX1, AnimX2)
                                    Visual.FadeColor(setting_icon_preview, "ForeColor", setting_icon_preview.ForeColor, [CP].Windows10.Color_Index3, AnimX1, AnimX2)
                                    Visual.FadeColor(lnk_preview, "ForeColor", lnk_preview.ForeColor, [CP].Windows10.Color_Index3, AnimX1, AnimX2)
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
                            Visual.FadeColor(setting_icon_preview, "ForeColor", setting_icon_preview.ForeColor, [CP].Windows10.Color_Index3, AnimX1, AnimX2)
                            Visual.FadeColor(lnk_preview, "ForeColor", lnk_preview.ForeColor, [CP].Windows10.Color_Index3, AnimX1, AnimX2)
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
                                    Visual.FadeColor(setting_icon_preview, "ForeColor", setting_icon_preview.ForeColor, [CP].Windows10.Color_Index3, AnimX1, AnimX2)
                                    Visual.FadeColor(lnk_preview, "ForeColor", lnk_preview.ForeColor, [CP].Windows10.Color_Index3, AnimX1, AnimX2)
                                    Visual.FadeColor(ActionCenter, "ActionCenterButton_Normal", ActionCenter.ActionCenterButton_Normal, [CP].Windows10.Color_Index3, AnimX1, AnimX2)

                                Case ApplyAccentonTaskbar_Level.Taskbar
                                    Visual.FadeColor(taskbar, "BackColor", taskbar.BackColor, [CP].Windows10.Color_Index6, AnimX1, AnimX2)
                                    Visual.FadeColor(taskbar, "StartColor", taskbar.StartColor, Color.Transparent, AnimX1, AnimX2)
                                    Visual.FadeColor(start, "BackColor", start.BackColor, Color.FromArgb(228, 228, 228), AnimX1, AnimX2)
                                    Visual.FadeColor(ActionCenter, "BackColor", ActionCenter.BackColor, Color.FromArgb(228, 228, 228), AnimX1, AnimX2)

                                    Visual.FadeColor(taskbar, "AppBackground", taskbar.AppBackground, Color.FromArgb(150, [CP].Windows10.Color_Index3), AnimX1, AnimX2)
                                    Visual.FadeColor(ActionCenter, "LinkColor", ActionCenter.LinkColor, [CP].Windows10.Color_Index6, AnimX1, AnimX2)
                                    Visual.FadeColor(taskbar, "AppUnderline", taskbar.AppUnderline, [CP].Windows10.Color_Index1, AnimX1, AnimX2)
                                    Visual.FadeColor(setting_icon_preview, "ForeColor", setting_icon_preview.ForeColor, [CP].Windows10.Color_Index3, AnimX1, AnimX2)
                                    Visual.FadeColor(lnk_preview, "ForeColor", lnk_preview.ForeColor, [CP].Windows10.Color_Index3, AnimX1, AnimX2)
                                    Visual.FadeColor(ActionCenter, "ActionCenterButton_Normal", ActionCenter.ActionCenterButton_Normal, [CP].Windows10.Color_Index3, AnimX1, AnimX2)

                                Case ApplyAccentonTaskbar_Level.Taskbar_Start_AC
                                    Visual.FadeColor(taskbar, "BackColor", taskbar.BackColor, [CP].Windows10.Color_Index6, AnimX1, AnimX2)
                                    Visual.FadeColor(taskbar, "StartColor", taskbar.StartColor, Color.Transparent, AnimX1, AnimX2)
                                    Visual.FadeColor(start, "BackColor", start.BackColor, [CP].Windows10.Color_Index4, AnimX1, AnimX2)
                                    Visual.FadeColor(ActionCenter, "BackColor", ActionCenter.BackColor, [CP].Windows10.Color_Index4, AnimX1, AnimX2)

                                    Visual.FadeColor(taskbar, "AppBackground", taskbar.AppBackground, Color.FromArgb(150, [CP].Windows10.Color_Index3), AnimX1, AnimX2)
                                    Visual.FadeColor(ActionCenter, "LinkColor", ActionCenter.LinkColor, [CP].Windows10.Color_Index0, AnimX1, AnimX2)
                                    Visual.FadeColor(taskbar, "AppUnderline", taskbar.AppUnderline, [CP].Windows10.Color_Index1, AnimX1, AnimX2)
                                    Visual.FadeColor(setting_icon_preview, "ForeColor", setting_icon_preview.ForeColor, [CP].Windows10.Color_Index3, AnimX1, AnimX2)
                                    Visual.FadeColor(lnk_preview, "ForeColor", lnk_preview.ForeColor, [CP].Windows10.Color_Index3, AnimX1, AnimX2)
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
                                    Visual.FadeColor(setting_icon_preview, "ForeColor", setting_icon_preview.ForeColor, [CP].Windows10.Color_Index3, AnimX1, AnimX2)
                                    Visual.FadeColor(lnk_preview, "ForeColor", lnk_preview.ForeColor, [CP].Windows10.Color_Index3, AnimX1, AnimX2)
                                    Visual.FadeColor(ActionCenter, "ActionCenterButton_Normal", ActionCenter.ActionCenterButton_Normal, [CP].Windows10.Color_Index3, AnimX1, AnimX2)

                                Case ApplyAccentonTaskbar_Level.Taskbar
                                    Visual.FadeColor(taskbar, "BackColor", taskbar.BackColor, [CP].Windows10.Color_Index5, AnimX1, AnimX2)
                                    Visual.FadeColor(taskbar, "StartColor", taskbar.StartColor, [CP].Windows10.Color_Index4, AnimX1, AnimX2)
                                    Visual.FadeColor(start, "BackColor", start.BackColor, Color.FromArgb(228, 228, 228), AnimX1, AnimX2)
                                    Visual.FadeColor(ActionCenter, "BackColor", ActionCenter.BackColor, Color.FromArgb(228, 228, 228), AnimX1, AnimX2)

                                    Visual.FadeColor(taskbar, "AppBackground", taskbar.AppBackground, [CP].Windows10.Color_Index4, AnimX1, AnimX2)
                                    Visual.FadeColor(ActionCenter, "LinkColor", ActionCenter.LinkColor, [CP].Windows10.Color_Index6, AnimX1, AnimX2)
                                    Visual.FadeColor(taskbar, "AppUnderline", taskbar.AppUnderline, [CP].Windows10.Color_Index1, AnimX1, AnimX2)
                                    Visual.FadeColor(setting_icon_preview, "ForeColor", setting_icon_preview.ForeColor, [CP].Windows10.Color_Index3, AnimX1, AnimX2)
                                    Visual.FadeColor(lnk_preview, "ForeColor", lnk_preview.ForeColor, [CP].Windows10.Color_Index3, AnimX1, AnimX2)
                                    Visual.FadeColor(ActionCenter, "ActionCenterButton_Normal", ActionCenter.ActionCenterButton_Normal, [CP].Windows10.Color_Index3, AnimX1, AnimX2)

                                Case ApplyAccentonTaskbar_Level.Taskbar_Start_AC
                                    Visual.FadeColor(taskbar, "BackColor", taskbar.BackColor, [CP].Windows10.Color_Index5, AnimX1, AnimX2)
                                    Visual.FadeColor(taskbar, "StartColor", taskbar.StartColor, [CP].Windows10.Color_Index4, AnimX1, AnimX2)
                                    Visual.FadeColor(start, "BackColor", start.BackColor, [CP].Windows10.Color_Index4, AnimX1, AnimX2)
                                    Visual.FadeColor(ActionCenter, "BackColor", ActionCenter.BackColor, [CP].Windows10.Color_Index4, AnimX1, AnimX2)


                                    Visual.FadeColor(taskbar, "AppBackground", taskbar.AppBackground, [CP].Windows10.Color_Index4, AnimX1, AnimX2)
                                    Visual.FadeColor(ActionCenter, "LinkColor", ActionCenter.LinkColor, [CP].Windows10.Color_Index0, AnimX1, AnimX2)
                                    Visual.FadeColor(taskbar, "AppUnderline", taskbar.AppUnderline, [CP].Windows10.Color_Index1, AnimX1, AnimX2)
                                    Visual.FadeColor(setting_icon_preview, "ForeColor", setting_icon_preview.ForeColor, [CP].Windows10.Color_Index3, AnimX1, AnimX2)
                                    Visual.FadeColor(lnk_preview, "ForeColor", lnk_preview.ForeColor, [CP].Windows10.Color_Index3, AnimX1, AnimX2)
                                    Visual.FadeColor(ActionCenter, "ActionCenterButton_Normal", ActionCenter.ActionCenterButton_Normal, [CP].Windows10.Color_Index3, AnimX1, AnimX2)

                            End Select

                        End If
                End Select

#End Region
            Case WinVer.W8
#Region "Win8.1"
                tabs_preview.SelectedIndex = 0

                If My.W8 And My.[Settings].Win7LivePreview Then
                    RefreshDWM([CP])
                End If

                ApplyMetroStartToButton([CP])
                ApplyBackLogonUI([CP])

                Select Case [CP].Windows8.Theme
                    Case CP.AeroTheme.Aero
                        XenonWindow1.Preview = XenonWindow.Preview_Enum.W8
                        XenonWindow2.Preview = XenonWindow.Preview_Enum.W8
                        taskbar.Transparency = True
                        taskbar.BackColorAlpha = 100
                    Case CP.AeroTheme.AeroLite
                        XenonWindow1.Preview = XenonWindow.Preview_Enum.W8Lite
                        XenonWindow2.Preview = XenonWindow.Preview_Enum.W8Lite
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
            Case WinVer.W7
#Region "Win7"
                If My.WVista And My.[Settings].Win7LivePreview And _Shown Then
                    RefreshDWM([CP])
                End If

                XenonWindow1.Shadow = [CP].WindowsEffects.WindowShadow
                XenonWindow2.Shadow = [CP].WindowsEffects.WindowShadow

                Select Case [CP].Windows7.Theme
                    Case CP.AeroTheme.Aero
                        tabs_preview.SelectedIndex = 0
                        start.Transparency = True
                        taskbar.Transparency = True
                        With XenonWindow1
                            .Preview = XenonWindow.Preview_Enum.W7Aero
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
                            .Preview = XenonWindow.Preview_Enum.W7Aero
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
                            .BackColorAlpha = [CP].Windows7.ColorizationBlurBalance
                            .Win7ColorBal = [CP].Windows7.ColorizationColorBalance
                            .Win7GlowBal = [CP].Windows7.ColorizationAfterglowBalance
                            .BackColor = [CP].Windows7.ColorizationColor
                            .BackColor2 = [CP].Windows7.ColorizationAfterglow
                            .NoisePower = [CP].Windows7.ColorizationGlassReflectionIntensity
                        End With
                        With taskbar
                            .BackColorAlpha = [CP].Windows7.ColorizationBlurBalance
                            .Win7ColorBal = [CP].Windows7.ColorizationColorBalance
                            .Win7GlowBal = [CP].Windows7.ColorizationAfterglowBalance
                            .BackColor = [CP].Windows7.ColorizationColor
                            .BackColor2 = [CP].Windows7.ColorizationAfterglow
                            .NoisePower = [CP].Windows7.ColorizationGlassReflectionIntensity
                        End With


                    Case CP.AeroTheme.AeroOpaque
                        tabs_preview.SelectedIndex = 0
                        start.Transparency = True
                        taskbar.Transparency = True

                        With XenonWindow1
                            .Preview = XenonWindow.Preview_Enum.W7Opaque
                            .Win7Alpha = [CP].Windows7.ColorizationColorBalance
                            .AccentColor_Active = [CP].Windows7.ColorizationColor
                            .AccentColor_Inactive = [CP].Windows7.ColorizationColor
                            .Win7Noise = [CP].Windows7.ColorizationGlassReflectionIntensity
                        End With
                        With XenonWindow2
                            .Preview = XenonWindow.Preview_Enum.W7Opaque
                            .Win7Alpha = [CP].Windows7.ColorizationColorBalance
                            .AccentColor_Active = [CP].Windows7.ColorizationColor
                            .AccentColor_Inactive = [CP].Windows7.ColorizationColor
                            .Win7Noise = [CP].Windows7.ColorizationGlassReflectionIntensity
                        End With
                        With taskbar
                            .BackColorAlpha = [CP].Windows7.ColorizationColorBalance
                            .BackColor = [CP].Windows7.ColorizationColor
                            .BackColor2 = [CP].Windows7.ColorizationColor
                            .NoisePower = [CP].Windows7.ColorizationGlassReflectionIntensity
                        End With
                        With start
                            .BackColorAlpha = [CP].Windows7.ColorizationColorBalance
                            .BackColor = [CP].Windows7.ColorizationColor
                            .BackColor2 = [CP].Windows7.ColorizationColor
                            .NoisePower = [CP].Windows7.ColorizationGlassReflectionIntensity
                        End With


                    Case CP.AeroTheme.Basic
                        tabs_preview.SelectedIndex = 0
                        taskbar.BackColor = Color.FromArgb(166, 190, 218)
                        taskbar.BackColorAlpha = 100

                        start.BackColor = Color.FromArgb(166, 190, 218)
                        start.BackColorAlpha = 100

                        With XenonWindow1
                            .Preview = XenonWindow.Preview_Enum.W7Basic
                            .Win7Alpha = 100
                            .AccentColor_Active = Color.FromArgb(166, 190, 218)
                            .Win7Noise = 0
                        End With
                        With XenonWindow2
                            .Preview = XenonWindow.Preview_Enum.W7Basic
                            .Win7Alpha = 100
                            .AccentColor_Inactive = Color.FromArgb(166, 190, 218)
                            .Win7Noise = 0
                        End With

                        start.Transparency = False
                        start.NoisePower = 0
                        taskbar.Transparency = False
                        taskbar.NoisePower = 0

                        start.Refresh()
                        taskbar.Refresh()

                    Case CP.AeroTheme.Classic
                        tabs_preview.SelectedIndex = 1

                End Select
#End Region
            Case WinVer.WVista
#Region "WinVista"
                If My.WVista And My.[Settings].Win7LivePreview And _Shown Then
                    RefreshDWM([CP])
                End If

                XenonWindow1.Shadow = [CP].WindowsEffects.WindowShadow
                XenonWindow2.Shadow = [CP].WindowsEffects.WindowShadow

                Select Case [CP].WindowsVista.Theme
                    Case CP.AeroTheme.Aero
                        tabs_preview.SelectedIndex = 0
                        start.Transparency = True
                        taskbar.Transparency = True
                        With XenonWindow1
                            .Preview = XenonWindow.Preview_Enum.W7Aero
                            .Win7Alpha = ((255 - [CP].WindowsVista.Alpha) / 255) * 100
                            .Win7ColorBal = ((255 - [CP].WindowsVista.Alpha) / 255) * 100
                            '.Win7GlowBal = [CP].WindowsVista.ColorizationAfterglowBalance
                            .AccentColor_Active = Color.FromArgb([CP].WindowsVista.Alpha, [CP].WindowsVista.ColorizationColor)
                            .AccentColor2_Active = Color.FromArgb([CP].WindowsVista.Alpha, [CP].WindowsVista.ColorizationColor)
                            .AccentColor_Inactive = Color.FromArgb(100, [CP].WindowsVista.ColorizationColor)
                            .AccentColor2_Inactive = Color.FromArgb(100, [CP].WindowsVista.ColorizationColor)
                            .Win7Noise = 100
                        End With
                        With XenonWindow2
                            .Preview = XenonWindow.Preview_Enum.W7Aero
                            .Win7Alpha = ((255 - [CP].WindowsVista.Alpha) / 255) * 100
                            .Win7ColorBal = ((255 - [CP].WindowsVista.Alpha) / 255) * 100
                            '.Win7GlowBal = [CP].WindowsVista.ColorizationAfterglowBalance
                            .AccentColor_Active = [CP].WindowsVista.ColorizationColor
                            .AccentColor2_Active = [CP].WindowsVista.ColorizationColor
                            .AccentColor_Inactive = Color.FromArgb(100, [CP].WindowsVista.ColorizationColor)
                            .AccentColor2_Inactive = Color.FromArgb(100, [CP].WindowsVista.ColorizationColor)
                            .Win7Noise = 100
                        End With
                        With start
                            .BackColorAlpha = ([CP].WindowsVista.Alpha / 255) * 180
                            .Win7ColorBal = ((255 - [CP].WindowsVista.Alpha) / 255) * 100
                            '.Win7GlowBal = [CP].WindowsVista.ColorizationAfterglowBalance
                            .BackColor = [CP].WindowsVista.ColorizationColor
                            .BackColor2 = [CP].WindowsVista.ColorizationColor
                            .NoisePower = 100
                        End With
                        With taskbar
                            .BackColorAlpha = ([CP].WindowsVista.Alpha / 255) * 180
                            .Win7ColorBal = ((255 - [CP].WindowsVista.Alpha) / 255) * 100
                            '.Win7GlowBal = [CP].WindowsVista.ColorizationAfterglowBalance
                            .BackColor = [CP].WindowsVista.ColorizationColor
                            .BackColor2 = [CP].WindowsVista.ColorizationColor
                            .NoisePower = 100
                        End With


                    Case CP.AeroTheme.AeroOpaque
                        tabs_preview.SelectedIndex = 0
                        start.Transparency = True
                        taskbar.Transparency = True

                        With XenonWindow1
                            .Preview = XenonWindow.Preview_Enum.W7Opaque
                            .Win7Alpha = (([CP].WindowsVista.Alpha) / 255) * 100
                            .AccentColor_Active = [CP].WindowsVista.ColorizationColor
                            .AccentColor_Inactive = [CP].WindowsVista.ColorizationColor
                            .Win7Noise = 100
                        End With
                        With XenonWindow2
                            .Preview = XenonWindow.Preview_Enum.W7Opaque
                            .Win7Alpha = (([CP].WindowsVista.Alpha) / 255) * 100
                            .AccentColor_Active = [CP].WindowsVista.ColorizationColor
                            .AccentColor_Inactive = [CP].WindowsVista.ColorizationColor
                            .Win7Noise = 100
                        End With
                        With taskbar
                            .BackColorAlpha = ([CP].WindowsVista.Alpha / 255) * 200
                            .BackColor = [CP].WindowsVista.ColorizationColor
                            .BackColor2 = [CP].WindowsVista.ColorizationColor
                            .NoisePower = 100
                        End With
                        With start
                            .BackColorAlpha = ([CP].WindowsVista.Alpha / 255) * 200
                            .BackColor = [CP].WindowsVista.ColorizationColor
                            .BackColor2 = [CP].WindowsVista.ColorizationColor
                            .NoisePower = 100
                        End With


                    Case CP.AeroTheme.Basic
                        tabs_preview.SelectedIndex = 0
                        taskbar.BackColor = Color.FromArgb(166, 190, 218)
                        taskbar.BackColorAlpha = 100

                        start.BackColor = Color.FromArgb(166, 190, 218)
                        start.BackColorAlpha = 100

                        With XenonWindow1
                            .Preview = XenonWindow.Preview_Enum.W7Basic
                            .Win7Alpha = 100
                            .AccentColor_Active = Color.FromArgb(166, 190, 218)
                            .Win7Noise = 0
                        End With
                        With XenonWindow2
                            .Preview = XenonWindow.Preview_Enum.W7Basic
                            .Win7Alpha = 100
                            .AccentColor_Inactive = Color.FromArgb(166, 190, 218)
                            .Win7Noise = 0
                        End With

                        start.Transparency = False
                        start.NoisePower = 0
                        taskbar.Transparency = False
                        taskbar.NoisePower = 0

                        start.Refresh()
                        taskbar.Refresh()

                    Case CP.AeroTheme.Classic
                        tabs_preview.SelectedIndex = 1

                End Select
#End Region
            Case WinVer.WXP
#Region "WinXP"
                start.Refresh()
                taskbar.Refresh()
#End Region
        End Select

        XenonWindow1.Invalidate()
        XenonWindow2.Invalidate()
    End Sub

    Sub AdjustClassicPreview()
        SetToClassicWindow(ClassicWindow1, CP)
        SetToClassicWindow(ClassicWindow2, CP, False)

        SetClassicMetrics(ClassicWindow1, CP)
        SetClassicMetrics(ClassicWindow2, CP)
        SetToClassicButton(RetroButton2, CP)
        SetToClassicButton(RetroButton3, CP)
        SetToClassicButton(RetroButton4, CP)
        SetToClassicRaisedPanel(ClassicTaskbar, CP)

        ClassicWindow2.Font = CP.MetricsFonts.CaptionFont
        ClassicWindow1.Font = CP.MetricsFonts.CaptionFont
    End Sub

    Sub SetClassicMetrics([Window] As RetroWindow, [CP] As CP)
        [Window].Metrics_BorderWidth = CP.MetricsFonts.BorderWidth
        [Window].Metrics_CaptionHeight = CP.MetricsFonts.CaptionHeight
        [Window].Metrics_CaptionWidth = CP.MetricsFonts.CaptionWidth
        [Window].Metrics_PaddedBorderWidth = CP.MetricsFonts.PaddedBorderWidth
        [Window].Font = CP.MetricsFonts.CaptionFont
        [Window].Refresh()
    End Sub

    Sub ApplyMetrics(ByVal [CP] As CP, XenonWindow As XenonWindow)
        XenonWindow.Font = [CP].MetricsFonts.CaptionFont
        XenonWindow.Metrics_BorderWidth = [CP].MetricsFonts.BorderWidth
        XenonWindow.Metrics_CaptionHeight = [CP].MetricsFonts.CaptionHeight
        XenonWindow.Metrics_PaddedBorderWidth = [CP].MetricsFonts.PaddedBorderWidth
        XenonWindow.Invalidate()
    End Sub

    Sub SetToClassicWindow([Window] As RetroWindow, [CP] As CP, Optional Active As Boolean = True)
        [Window].ButtonDkShadow = [CP].Win32.ButtonDkShadow
        [Window].BackColor = [CP].Win32.ButtonFace
        [Window].ButtonHilight = [CP].Win32.ButtonHilight
        [Window].ButtonLight = [CP].Win32.ButtonLight
        [Window].ButtonShadow = [CP].Win32.ButtonShadow
        [Window].ButtonText = [CP].Win32.ButtonText

        If Active Then
            [Window].ColorBorder = [CP].Win32.ActiveBorder
            [Window].ForeColor = [CP].Win32.TitleText
            [Window].Color1 = [CP].Win32.ActiveTitle
            [Window].Color2 = [CP].Win32.GradientActiveTitle
        Else
            [Window].ColorBorder = [CP].Win32.InactiveBorder
            [Window].ForeColor = [CP].Win32.InactiveTitleText
            [Window].Color1 = [CP].Win32.InactiveTitle
            [Window].Color2 = [CP].Win32.GradientInactiveTitle
        End If

        [Window].ColorGradient = [CP].Win32.EnableGradient
    End Sub

    Sub SetToClassicRaisedPanel([Panel] As RetroPanelRaised, [CP] As CP)
        [Panel].BackColor = [CP].Win32.ButtonFace
        [Panel].ButtonHilight = [CP].Win32.ButtonHilight
        [Panel].ButtonLight = [CP].Win32.ButtonLight
        [Panel].ButtonShadow = [CP].Win32.ButtonShadow
        [Panel].ButtonDkShadow = [CP].Win32.ButtonDkShadow
        [Panel].ForeColor = [CP].Win32.TitleText
    End Sub

    Sub SetToClassicButton([Button] As RetroButton, [CP] As CP)
        [Button].ButtonDkShadow = [CP].Win32.ButtonDkShadow
        [Button].ButtonHilight = [CP].Win32.ButtonHilight
        [Button].ButtonLight = [CP].Win32.ButtonLight
        [Button].ButtonShadow = [CP].Win32.ButtonShadow
        [Button].BackColor = [CP].Win32.ButtonFace
        [Button].ForeColor = [CP].Win32.ButtonText
        [Button].FocusRectWidth = [CP].WindowsEffects.FocusRectWidth
        [Button].FocusRectHeight = [CP].WindowsEffects.FocusRectHeight
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

    Sub DoubleBufferAll(ByVal Parent As Control)
        MakeItDoubleBuffered(Parent)

        For Each ctrl As Control In Parent.Controls
            MakeItDoubleBuffered(ctrl)
            If ctrl.HasChildren Then
                For Each c As Control In ctrl.Controls
                    ReValidateLivePreview(c)
                Next
            End If
        Next
    End Sub

    Public Sub Update_Wallpaper_Preview()
        Cursor = Cursors.AppStarting

        My.Wallpaper = My.Application.GetWallpaper().Resize(528, 297)

        Select Case PreviewConfig
            Case WinVer.W11
                If CP.WallpaperTone_W11.Enabled Then pnl_preview.BackgroundImage = GetTintedWallpaper(CP.WallpaperTone_W11) Else pnl_preview.BackgroundImage = My.Wallpaper

            Case WinVer.W10
                If CP.WallpaperTone_W10.Enabled Then pnl_preview.BackgroundImage = GetTintedWallpaper(CP.WallpaperTone_W10) Else pnl_preview.BackgroundImage = My.Wallpaper

            Case WinVer.W8
                If CP.WallpaperTone_W8.Enabled Then pnl_preview.BackgroundImage = GetTintedWallpaper(CP.WallpaperTone_W8) Else pnl_preview.BackgroundImage = My.Wallpaper

            Case WinVer.W7
                If CP.WallpaperTone_W7.Enabled Then pnl_preview.BackgroundImage = GetTintedWallpaper(CP.WallpaperTone_W7) Else pnl_preview.BackgroundImage = My.Wallpaper

            Case WinVer.WVista
                If CP.WallpaperTone_WVista.Enabled Then pnl_preview.BackgroundImage = GetTintedWallpaper(CP.WallpaperTone_WVista) Else pnl_preview.BackgroundImage = My.Wallpaper

            Case WinVer.WXP
                If CP.WallpaperTone_WXP.Enabled Then pnl_preview.BackgroundImage = GetTintedWallpaper(CP.WallpaperTone_WXP) Else pnl_preview.BackgroundImage = My.Wallpaper
        End Select

        pnl_preview_classic.BackgroundImage = pnl_preview.BackgroundImage

        ApplyLivePreviewFromCP(CP)
        ApplyCPValues(CP)
        Adjust_Preview(False)
        ReValidateLivePreview(pnl_preview)
        ReValidateLivePreview(pnl_preview_classic)

        Cursor = Cursors.Default
    End Sub

    Sub Adjust_Preview(Optional AnimateThePreview As Boolean = True)
        If AnimateThePreview Then
            If _Shown Then
                If tabs_preview.Visible Then My.Animator.HideSync(tabs_preview)
            Else
                tabs_preview.Visible = False
            End If
        End If

        If My.W11 Then My.EP = New ExplorerPatcher

        Panel3.Visible = (PreviewConfig = WinVer.W11 Or PreviewConfig = WinVer.W10)
        lnk_preview.Visible = (PreviewConfig = WinVer.W11 Or PreviewConfig = WinVer.W10)
        start.Visible = (Not PreviewConfig = WinVer.W8)
        ActionCenter.Visible = (PreviewConfig = WinVer.W11 Or PreviewConfig = WinVer.W10)
        XenonButton23.Visible = (PreviewConfig = WinVer.W7)
        Dim condition0 As Boolean = PreviewConfig = WinVer.W7 AndAlso CP.Windows7.Theme = AeroTheme.Classic
        Dim condition1 As Boolean = PreviewConfig = WinVer.WXP AndAlso CP.WindowsXP.Theme = WinXPTheme.Classic
        WXP_Alert2.Visible = PreviewConfig = WinVer.WXP AndAlso My.StartedWithClassicTheme

        tabs_preview.SelectedIndex = If(condition0 Or condition1, 1, 0)

        ApplyMetrics([CP], XenonWindow1)
        ApplyMetrics([CP], XenonWindow2)

        Select Case PreviewConfig
            Case WinVer.W11
                ActionCenter.Style = XenonWinElement.Styles.ActionCenter11

                If ExplorerPatcher.IsAllowed Then
                    With My.EP

                        If Not .UseStart10 Then
                            start.Style = XenonWinElement.Styles.Start11
                        Else
                            start.Style = XenonWinElement.Styles.Start10
                        End If

                        If Not .UseTaskbar10 Then
                            taskbar.Style = XenonWinElement.Styles.Taskbar11
                        Else
                            taskbar.Style = XenonWinElement.Styles.Taskbar10
                        End If

                    End With
                Else
                    taskbar.Style = XenonWinElement.Styles.Taskbar11
                    start.Style = XenonWinElement.Styles.Start11
                End If

                XenonWindow1.Preview = XenonWindow.Preview_Enum.W11

                If CP.WallpaperTone_W11.Enabled Then pnl_preview.BackgroundImage = GetTintedWallpaper(CP.WallpaperTone_W11) Else pnl_preview.BackgroundImage = My.Wallpaper

            Case WinVer.W10
                ActionCenter.Style = XenonWinElement.Styles.ActionCenter10
                taskbar.Style = XenonWinElement.Styles.Taskbar10
                start.Style = XenonWinElement.Styles.Start10
                XenonWindow1.Preview = XenonWindow.Preview_Enum.W10
                If CP.WallpaperTone_W10.Enabled Then pnl_preview.BackgroundImage = GetTintedWallpaper(CP.WallpaperTone_W10) Else pnl_preview.BackgroundImage = My.Wallpaper

            Case WinVer.W8
                taskbar.Style = If(CP.Windows8.Theme = AeroTheme.Aero, XenonWinElement.Styles.Taskbar8Aero, XenonWinElement.Styles.Taskbar8Lite)
                XenonWindow1.Preview = If(CP.Windows8.Theme = AeroTheme.AeroLite, XenonWindow.Preview_Enum.W8Lite, XenonWindow.Preview_Enum.W8)
                If CP.WallpaperTone_W8.Enabled Then pnl_preview.BackgroundImage = GetTintedWallpaper(CP.WallpaperTone_W8) Else pnl_preview.BackgroundImage = My.Wallpaper

            Case WinVer.W7
                If CP.WallpaperTone_W7.Enabled Then pnl_preview.BackgroundImage = GetTintedWallpaper(CP.WallpaperTone_W7) Else pnl_preview.BackgroundImage = My.Wallpaper

                Select Case CP.Windows7.Theme
                    Case AeroTheme.Aero
                        taskbar.Style = XenonWinElement.Styles.Taskbar7Aero
                        start.Style = XenonWinElement.Styles.Start7Aero
                        XenonWindow1.Preview = XenonWindow.Preview_Enum.W7Aero
                        tabs_preview.SelectedIndex = 0

                    Case AeroTheme.AeroOpaque
                        taskbar.Style = XenonWinElement.Styles.Taskbar7Opaque
                        start.Style = XenonWinElement.Styles.Start7Opaque
                        XenonWindow1.Preview = XenonWindow.Preview_Enum.W7Opaque
                        tabs_preview.SelectedIndex = 0

                    Case AeroTheme.Basic
                        taskbar.Style = XenonWinElement.Styles.Taskbar7Basic
                        start.Style = XenonWinElement.Styles.Start7Basic
                        XenonWindow1.Preview = XenonWindow.Preview_Enum.W7Basic
                        tabs_preview.SelectedIndex = 0

                    Case AeroTheme.Classic
                        tabs_preview.SelectedIndex = 1

                End Select

                XenonWindow1.WinVista = False
                XenonWindow2.WinVista = False

            Case WinVer.WVista

                Select Case CP.WindowsVista.Theme     'Windows Vista uses the same aero of Windows 7
                    Case AeroTheme.Aero
                        taskbar.Style = XenonWinElement.Styles.TaskbarVistaAero
                        start.Style = XenonWinElement.Styles.StartVistaAero
                        XenonWindow1.Preview = XenonWindow.Preview_Enum.W7Aero
                        tabs_preview.SelectedIndex = 0

                    Case AeroTheme.AeroOpaque
                        taskbar.Style = XenonWinElement.Styles.TaskbarVistaOpaque
                        start.Style = XenonWinElement.Styles.StartVistaOpaque
                        XenonWindow1.Preview = XenonWindow.Preview_Enum.W7Opaque
                        tabs_preview.SelectedIndex = 0

                    Case AeroTheme.Basic
                        taskbar.Style = XenonWinElement.Styles.TaskbarVistaBasic
                        start.Style = XenonWinElement.Styles.StartVistaBasic
                        XenonWindow1.Preview = XenonWindow.Preview_Enum.W7Basic
                        tabs_preview.SelectedIndex = 0

                    Case AeroTheme.Classic
                        tabs_preview.SelectedIndex = 1

                End Select

                If CP.WallpaperTone_WVista.Enabled Then pnl_preview.BackgroundImage = GetTintedWallpaper(CP.WallpaperTone_WVista) Else pnl_preview.BackgroundImage = My.Wallpaper

                XenonWindow1.WinVista = True
                XenonWindow2.WinVista = True

            Case WinVer.WXP
                taskbar.Style = XenonWinElement.Styles.TaskbarXP
                start.Style = XenonWinElement.Styles.StartXP
                XenonWindow1.Preview = XenonWindow.Preview_Enum.WXP
                If CP.WallpaperTone_WXP.Enabled Then pnl_preview.BackgroundImage = GetTintedWallpaper(CP.WallpaperTone_WXP) Else pnl_preview.BackgroundImage = My.Wallpaper

                Select Case CP.WindowsXP.Theme
                    Case WinXPTheme.LunaBlue
                        My.VS = My.Application.appData & "\VisualStyles\Luna\luna.theme"
                        IO.File.WriteAllText(My.Application.appData & "\VisualStyles\Luna\luna.theme", String.Format("[VisualStyles]{1}Path={0}{1}ColorStyle=NormalColor{1}Size=NormalSize", My.Application.appData & "\VisualStyles\Luna\luna.msstyles", vbCrLf))
                        My.LunaRes = New Luna(Luna.ColorStyles.Blue)
                        My.resVS = New VisualStylesRes(My.VS)

                    Case WinXPTheme.LunaOliveGreen
                        My.VS = My.Application.appData & "\VisualStyles\Luna\luna.theme"
                        IO.File.WriteAllText(My.Application.appData & "\VisualStyles\Luna\luna.theme", String.Format("[VisualStyles]{1}Path={0}{1}ColorStyle=HomeStead{1}Size=NormalSize", My.Application.appData & "\VisualStyles\Luna\luna.msstyles", vbCrLf))
                        My.LunaRes = New Luna(Luna.ColorStyles.OliveGreen)
                        My.resVS = New VisualStylesRes(My.VS)

                    Case WinXPTheme.LunaSilver
                        My.VS = My.Application.appData & "\VisualStyles\Luna\luna.theme"
                        IO.File.WriteAllText(My.Application.appData & "\VisualStyles\Luna\luna.theme", String.Format("[VisualStyles]{1}Path={0}{1}ColorStyle=Metallic{1}Size=NormalSize", My.Application.appData & "\VisualStyles\Luna\luna.msstyles", vbCrLf))
                        My.LunaRes = New Luna(Luna.ColorStyles.Silver)
                        My.resVS = New VisualStylesRes(My.VS)

                    Case WinXPTheme.Custom
                        If File.Exists(CP.WindowsXP.ThemeFile) Then
                            If Path.GetExtension(CP.WindowsXP.ThemeFile) = ".theme" Then
                                My.VS = CP.WindowsXP.ThemeFile
                            ElseIf Path.GetExtension(CP.WindowsXP.ThemeFile) = ".msstyles" Then
                                My.VS = My.Application.appData & "\VisualStyles\Luna\luna.theme"
                                File.WriteAllText(My.Application.appData & "\VisualStyles\Luna\luna.theme", String.Format("[VisualStyles]{1}Path={0}{1}ColorStyle={2}{1}Size=NormalSize", CP.WindowsXP.ThemeFile, vbCrLf, CP.WindowsXP.ColorScheme))
                            End If
                        End If
                        My.LunaRes = New Luna(Luna.ColorStyles.Empty)
                        My.resVS = New VisualStylesRes(My.VS)

                    Case WinXPTheme.Classic
                        My.VS = My.Application.appData & "\VisualStyles\Luna\luna.theme"
                        IO.File.WriteAllText(My.Application.appData & "\VisualStyles\Luna\luna.theme", String.Format("[VisualStyles]{1}Path={0}{1}ColorStyle=NormalColor{1}Size=NormalSize", My.Application.appData & "\VisualStyles\Luna\luna.msstyles", vbCrLf))
                        My.LunaRes = New Luna(Luna.ColorStyles.Empty)
                        My.resVS = New VisualStylesRes(My.VS)

                End Select

                Try
                    If WXP_VS_ReplaceColors.Checked And CP.WindowsXP.Theme <> WinXPTheme.Classic Then
                        If File.Exists(My.VS) And Not String.IsNullOrEmpty(My.VS) Then
                            Dim vs As New VisualStyleFile(My.VS)
                            CP.Win32.Load(Structures.Win32UI.Method.VisualStyles, vs.Metrics)
                        End If
                    End If

                    If WXP_VS_ReplaceMetrics.Checked And CP.WindowsXP.Theme <> WinXPTheme.Classic Then
                        If File.Exists(My.VS) And Not String.IsNullOrEmpty(My.VS) Then
                            Dim vs As New VisualStyleFile(My.VS)
                            CP.MetricsFonts.Overwrite_Metrics(vs.Metrics)
                        End If
                    End If

                    If WXP_VS_ReplaceFonts.Checked And CP.WindowsXP.Theme <> WinXPTheme.Classic Then
                        If File.Exists(My.VS) And Not String.IsNullOrEmpty(My.VS) Then
                            Dim vs As New VisualStyleFile(My.VS)
                            CP.MetricsFonts.Overwrite_Fonts(vs.Metrics)
                        End If
                    End If
                Catch
                End Try

        End Select

        XenonWindow2.Preview = XenonWindow1.Preview
        pnl_preview_classic.BackgroundImage = pnl_preview.BackgroundImage

        Select Case PreviewConfig
            Case WinVer.W11
                ActionCenter.Dock = Nothing
                ActionCenter.BlurPower = 7
                ActionCenter.NoisePower = 0.2
                ActionCenter.Size = New Size(120, 85)
                ActionCenter.Location = New Point(398, 161)

                If ExplorerPatcher.IsAllowed Then

                    With My.EP

                        If Not .UseTaskbar10 Then
                            taskbar.BlurPower = 12
                            taskbar.Height = 42
                        Else
                            taskbar.BlurPower = 12
                            taskbar.Height = 35
                            taskbar.UseWin11ORB_WithWin10 = Not .TaskbarButton10
                        End If

                        If Not .UseStart10 Then
                            start.BlurPower = 7
                            start.NoisePower = 0.2
                            start.Size = New Size(135, 200)
                            start.Location = New Point(9, taskbar.Bottom - taskbar.Height - start.Height - 9)
                        Else
                            start.BlurPower = 7
                            start.NoisePower = 0.2

                            Select Case .StartStyle
                                Case ExplorerPatcher.StartStyles.NotRounded
                                    start.Size = New Size(182, 201)
                                    start.Left = 0
                                    start.Top = taskbar.Bottom - taskbar.Height - start.Height
                                    start.UseWin11RoundedCorners_WithWin10_Level1 = False
                                    start.UseWin11RoundedCorners_WithWin10_Level2 = False

                                Case ExplorerPatcher.StartStyles.RoundedCornersDockedMenu
                                    start.Size = New Size(182, 201)
                                    start.Left = 0
                                    start.Top = taskbar.Bottom - taskbar.Height - start.Height
                                    start.UseWin11RoundedCorners_WithWin10_Level1 = True
                                    start.UseWin11RoundedCorners_WithWin10_Level2 = False

                                Case ExplorerPatcher.StartStyles.RoundedCornersFloatingMenu
                                    start.Size = New Size(182, 201)
                                    start.Location = New Point(9, taskbar.Bottom - taskbar.Height - start.Height - 9)
                                    start.UseWin11RoundedCorners_WithWin10_Level1 = False
                                    start.UseWin11RoundedCorners_WithWin10_Level2 = True

                            End Select

                        End If
                    End With

                Else
                    taskbar.BlurPower = 12
                    taskbar.Height = 42
                    '########################
                    start.BlurPower = 7
                    start.NoisePower = 0.2
                    start.Size = New Size(135, 200)
                    start.Location = New Point(9, taskbar.Bottom - 42 - start.Height - 9)
                End If

            Case WinVer.W10
                ActionCenter.Dock = DockStyle.Right
                ActionCenter.BlurPower = 7
                ActionCenter.NoisePower = 0.2
                '########################
                taskbar.BlurPower = If(Not CP.Windows10.IncreaseTBTransparency, 12, 6)
                '########################
                start.BlurPower = 7
                start.NoisePower = 0.2
                '########################

                taskbar.Height = 35
                taskbar.UseWin11ORB_WithWin10 = False
                start.Size = New Size(182, 201)
                start.Left = 0
                start.Top = taskbar.Bottom - taskbar.Height - start.Height
                start.UseWin11RoundedCorners_WithWin10_Level1 = False
                start.UseWin11RoundedCorners_WithWin10_Level2 = False

            Case WinVer.W8
                Panel3.Visible = False
                lnk_preview.Visible = False
                start.Visible = False
                ActionCenter.Visible = False
                taskbar.BlurPower = 0
                taskbar.Height = 34

                start.BlurPower = 0
                start.Top = taskbar.Top - start.Height
                start.Left = 0

            Case WinVer.W7
                XenonButton23.Visible = True
                Panel3.Visible = False
                lnk_preview.Visible = False
                ActionCenter.Visible = False

                taskbar.BlurPower = 1
                taskbar.NoisePower = CP.Windows7.ColorizationGlassReflectionIntensity / 100
                taskbar.Height = 34

                start.BlurPower = 1
                start.NoisePower = 0.5
                start.Width = 136
                start.Height = 191
                start.NoisePower = CP.Windows7.ColorizationGlassReflectionIntensity / 100
                start.Left = 0
                start.Top = taskbar.Top - start.Height
                ClassicTaskbar.Height = 44
                RetroButton3.Image = My.Resources.ActiveApp_Taskbar
                RetroButton4.Image = My.Resources.InactiveApp_Taskbar
                RetroButton2.Image = My.Resources.Native7.Resize(18, 16)
                RetroButton3.ImageAlign = Drawing.ContentAlignment.MiddleCenter
                RetroButton4.ImageAlign = Drawing.ContentAlignment.MiddleCenter
                RetroButton3.Width = 48
                RetroButton4.Width = 48
                RetroButton3.Text = ""
                RetroButton4.Text = ""
                RetroButton4.Left = RetroButton3.Right + 3
                RetroButton3.Font = New Font(CP.MetricsFonts.CaptionFont.Name, 9, RetroButton3.Font.Style)
                RetroButton4.Font = New Font(CP.MetricsFonts.CaptionFont.Name, 9, RetroButton4.Font.Style)
                RetroButton2.Font = New Font(CP.MetricsFonts.CaptionFont.Name, 8.5, RetroButton2.Font.Style)
                RetroButton3.HatchBrush = False

            Case WinVer.WVista
                XenonButton23.Visible = True
                Panel3.Visible = False
                lnk_preview.Visible = False
                ActionCenter.Visible = False
                taskbar.Height = 30

                start.Width = 136
                start.Height = 191
                start.Left = 0
                start.Top = taskbar.Top - start.Height
                ClassicTaskbar.Height = taskbar.Height
                RetroButton3.Image = My.Resources.ActiveApp_Taskbar.Resize(23, 23)
                RetroButton4.Image = My.Resources.InactiveApp_Taskbar.Resize(23, 23)
                RetroButton2.Image = My.Resources.Native7.Resize(18, 16)
                RetroButton3.ImageAlign = Drawing.ContentAlignment.BottomLeft
                RetroButton4.ImageAlign = Drawing.ContentAlignment.BottomLeft
                RetroButton3.Width = 140
                RetroButton4.Width = 140
                RetroButton3.Text = ClassicWindow1.TitlebarText
                RetroButton4.Text = ClassicWindow2.TitlebarText
                RetroButton4.Left = RetroButton3.Right + 3
                RetroButton3.Font = New Font(CP.MetricsFonts.CaptionFont.Name, 9, RetroButton3.Font.Style)
                RetroButton4.Font = New Font(CP.MetricsFonts.CaptionFont.Name, 9, RetroButton4.Font.Style)
                RetroButton2.Font = New Font(CP.MetricsFonts.CaptionFont.Name, 8.5, RetroButton2.Font.Style)
                RetroButton3.HatchBrush = True

            Case WinVer.WXP
                taskbar.Height = 30
                start.Width = 150
                start.Height = 190
                start.Left = 0
                start.Top = taskbar.Top - start.Height
                ClassicTaskbar.Height = taskbar.Height
                RetroButton3.Image = My.Resources.ActiveApp_Taskbar.Resize(23, 23)
                RetroButton4.Image = My.Resources.InactiveApp_Taskbar.Resize(23, 23)
                RetroButton2.Image = My.Resources.NativeXP.Resize(18, 16)
                RetroButton3.ImageAlign = Drawing.ContentAlignment.BottomLeft
                RetroButton4.ImageAlign = Drawing.ContentAlignment.BottomLeft
                RetroButton3.Width = 140
                RetroButton4.Width = 140
                RetroButton3.Text = ClassicWindow1.TitlebarText
                RetroButton4.Text = ClassicWindow2.TitlebarText
                RetroButton4.Left = RetroButton3.Right + 3
                RetroButton3.Font = New Font(CP.MetricsFonts.CaptionFont.Name, 9, RetroButton3.Font.Style)
                RetroButton4.Font = New Font(CP.MetricsFonts.CaptionFont.Name, 9, RetroButton4.Font.Style)
                RetroButton2.Font = New Font(CP.MetricsFonts.CaptionFont.Name, 8.5, RetroButton2.Font.Style)
                RetroButton3.HatchBrush = True

        End Select

        If PreviewConfig = WinVer.W10 Or PreviewConfig = WinVer.W11 Then

            If My.W11 And My.EP.UseStart10 Then
                XenonWindow1.Top = start.Top - 35
                XenonWindow1.Left = start.Right + 15
            Else
                XenonWindow1.Top = start.Top - If(PreviewConfig = WinVer.W11, 30, 35)
                XenonWindow1.Left = start.Right + If(PreviewConfig = WinVer.W11, 30, 15)
            End If

            XenonWindow2.Top = XenonWindow1.Bottom + 1
            XenonWindow2.Left = XenonWindow1.Left
        Else
            XenonWindow1.Top = 10
            XenonWindow1.Left = (XenonWindow1.Parent.Width - XenonWindow1.Width) / 2

            XenonWindow2.Top = XenonWindow1.Bottom + 5
            XenonWindow2.Left = XenonWindow1.Left
        End If

        ReValidateLivePreview(tabs_preview)

        If AnimateThePreview Then
            If _Shown Then
                My.Animator.ShowSync(tabs_preview)
            Else
                tabs_preview.Visible = True
            End If
        End If
    End Sub

    Sub ApplyCPValues([CP] As CP)
        themename_lbl.Text = String.Format("{0} ({1})", Me.[CP].Info.PaletteName, Me.[CP].Info.PaletteVersion)
        author_lbl.Text = String.Format("{0}: {1}", My.Lang.By, Me.[CP].Info.Author)

        W11_WinMode_Toggle.Checked = Not [CP].Windows11.WinMode_Light
        W11_AppMode_Toggle.Checked = Not [CP].Windows11.AppMode_Light
        W11_Transparency_Toggle.Checked = [CP].Windows11.Transparency
        W11_ShowAccentOnTitlebarAndBorders_Toggle.Checked = [CP].Windows11.ApplyAccentonTitlebars
        Select Case [CP].Windows11.ApplyAccentonTaskbar
            Case ApplyAccentonTaskbar_Level.None
                W11_Accent_None.Checked = True

            Case ApplyAccentonTaskbar_Level.Taskbar_Start_AC
                W11_Accent_StartTaskbar.Checked = True

            Case ApplyAccentonTaskbar_Level.Taskbar
                W11_Accent_Taskbar.Checked = True

        End Select
        W11_ActiveTitlebar_pick.BackColor = [CP].Windows11.Titlebar_Active
        W11_InactiveTitlebar_pick.BackColor = [CP].Windows11.Titlebar_Inactive
        W11_Color_Index5.BackColor = [CP].Windows11.StartMenu_Accent
        W11_Color_Index4.BackColor = [CP].Windows11.Color_Index2
        W11_Color_Index6.BackColor = [CP].Windows11.Color_Index6
        W11_Color_Index1.BackColor = [CP].Windows11.Color_Index1
        W11_Color_Index2.BackColor = [CP].Windows11.Color_Index4
        W11_TaskbarFrontAndFoldersOnStart_pick.BackColor = [CP].Windows11.Color_Index5
        W11_Color_Index0.BackColor = [CP].Windows11.Color_Index0
        W11_Color_Index3.BackColor = [CP].Windows11.Color_Index3
        W11_Color_Index7.BackColor = [CP].Windows11.Color_Index7

        W10_WinMode_Toggle.Checked = Not [CP].Windows10.WinMode_Light
        W10_AppMode_Toggle.Checked = Not [CP].Windows10.AppMode_Light
        W10_Transparency_Toggle.Checked = [CP].Windows10.Transparency
        W10_TBTransparency_Toggle.Checked = [CP].Windows10.IncreaseTBTransparency
        W10_ShowAccentOnTitlebarAndBorders_Toggle.Checked = [CP].Windows10.ApplyAccentonTitlebars
        Select Case [CP].Windows10.ApplyAccentonTaskbar
            Case ApplyAccentonTaskbar_Level.None
                W10_Accent_None.Checked = True

            Case ApplyAccentonTaskbar_Level.Taskbar_Start_AC
                W10_Accent_StartTaskbar.Checked = True

            Case ApplyAccentonTaskbar_Level.Taskbar
                W10_Accent_Taskbar.Checked = True
        End Select
        W10_ActiveTitlebar_pick.BackColor = [CP].Windows10.Titlebar_Active
        W10_InactiveTitlebar_pick.BackColor = [CP].Windows10.Titlebar_Inactive
        W10_Color_Index5.BackColor = [CP].Windows10.StartMenu_Accent
        W10_Color_Index4.BackColor = [CP].Windows10.Color_Index2
        W10_Color_Index6.BackColor = [CP].Windows10.Color_Index6
        W10_Color_Index1.BackColor = [CP].Windows10.Color_Index1
        W10_Color_Index2.BackColor = [CP].Windows10.Color_Index4
        W10_TaskbarFrontAndFoldersOnStart_pick.BackColor = [CP].Windows10.Color_Index5
        W10_Color_Index0.BackColor = [CP].Windows10.Color_Index0
        W10_Color_Index3.BackColor = [CP].Windows10.Color_Index3
        W10_Color_Index7.BackColor = [CP].Windows10.Color_Index7

        Select Case [CP].Windows8.Theme
            Case CP.AeroTheme.Aero
                W8_theme_aero.Checked = True

            Case CP.AeroTheme.AeroLite
                W8_theme_aerolite.Checked = True
        End Select
        W8_ColorizationColor_pick.BackColor = [CP].Windows8.ColorizationColor
        W8_ColorizationBalance_bar.Value = [CP].Windows8.ColorizationColorBalance
        W8_ColorizationBalance_val.Text = [CP].Windows8.ColorizationColorBalance
        W8_start_pick.BackColor = [CP].Windows8.StartColor
        W8_accent_pick.BackColor = [CP].Windows8.AccentColor
        W8_personalcls_background_pick.BackColor = [CP].Windows8.PersonalColors_Background
        W8_personalcolor_accent_pick.BackColor = [CP].Windows8.PersonalColors_Accent

        W7_ColorizationColor_pick.BackColor = [CP].Windows7.ColorizationColor
        W7_ColorizationAfterglow_pick.BackColor = [CP].Windows7.ColorizationAfterglow
        W7_ColorizationColorBalance_bar.Value = [CP].Windows7.ColorizationColorBalance
        W7_ColorizationAfterglowBalance_bar.Value = [CP].Windows7.ColorizationAfterglowBalance
        W7_ColorizationBlurBalance_bar.Value = [CP].Windows7.ColorizationBlurBalance
        W7_ColorizationGlassReflectionIntensity_bar.Value = [CP].Windows7.ColorizationGlassReflectionIntensity
        W7_ColorizationColorBalance_val.Text = [CP].Windows7.ColorizationColorBalance
        W7_ColorizationAfterglowBalance_val.Text = [CP].Windows7.ColorizationAfterglowBalance
        W7_ColorizationBlurBalance_val.Text = [CP].Windows7.ColorizationBlurBalance
        W7_ColorizationGlassReflectionIntensity_val.Text = [CP].Windows7.ColorizationGlassReflectionIntensity
        W7_EnableAeroPeek_toggle.Checked = [CP].Windows7.EnableAeroPeek
        W7_AlwaysHibernateThumbnails_Toggle.Checked = [CP].Windows7.AlwaysHibernateThumbnails
        Select Case [CP].Windows7.Theme
            Case CP.AeroTheme.Aero
                W7_theme_aero.Checked = True

            Case CP.AeroTheme.AeroOpaque
                W7_theme_aeroopaque.Checked = True

            Case CP.AeroTheme.Basic
                W7_theme_basic.Checked = True

            Case CP.AeroTheme.Classic
                W7_theme_classic.Checked = True
        End Select

        WVista_ColorizationColor_pick.BackColor = [CP].WindowsVista.ColorizationColor
        WVista_ColorizationColorBalance_bar.Value = [CP].WindowsVista.Alpha
        WVista_ColorizationColorBalance_val.Text = [CP].WindowsVista.Alpha
        Select Case [CP].WindowsVista.Theme
            Case CP.AeroTheme.Aero
                WVista_theme_aero.Checked = True

            Case CP.AeroTheme.AeroOpaque
                WVista_theme_aeroopaque.Checked = True

            Case CP.AeroTheme.Basic
                WVista_theme_basic.Checked = True

            Case CP.AeroTheme.Classic
                WVista_theme_classic.Checked = True
        End Select

        Select Case [CP].WindowsXP.Theme
            Case CP.WinXPTheme.LunaBlue
                WXP_Luna_Blue.Checked = True

            Case CP.WinXPTheme.LunaOliveGreen
                WXP_Luna_OliveGreen.Checked = True

            Case CP.WinXPTheme.LunaSilver
                WXP_Luna_Silver.Checked = True

            Case CP.WinXPTheme.Custom
                WXP_CustomTheme.Checked = True

            Case CP.WinXPTheme.Classic
                WXP_Classic.Checked = True

        End Select
        WXP_VS_textbox.Text = [CP].WindowsXP.ThemeFile
        If WXP_VS_ColorsList.Items.Contains([CP].WindowsXP.ColorScheme) Then WXP_VS_ColorsList.SelectedItem = [CP].WindowsXP.ColorScheme

        ApplyMetroStartToButton([CP])
        ApplyBackLogonUI([CP])
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
        ElseIf My.WVista Then
            DefCP = New CP_Defaults().Default_WindowsVista
        ElseIf My.WXP Then
            DefCP = New CP_Defaults().Default_WindowsXP
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

        W8_ColorizationColor_pick.DefaultColor = DefCP.Windows7.ColorizationColor
        W8_start_pick.DefaultColor = DefCP.Windows8.StartColor
        W8_accent_pick.DefaultColor = DefCP.Windows8.AccentColor
        W8_personalcls_background_pick.DefaultColor = DefCP.Windows8.PersonalColors_Background
        W8_personalcolor_accent_pick.DefaultColor = DefCP.Windows8.PersonalColors_Accent

        W7_ColorizationColor_pick.DefaultColor = DefCP.Windows7.ColorizationColor
        W7_ColorizationAfterglow_pick.DefaultColor = DefCP.Windows7.ColorizationAfterglow

        WVista_ColorizationColor_pick.DefaultColor = DefCP.WindowsVista.ColorizationColor

        CP.Dispose()
    End Sub

    Sub ApplyMetroStartToButton(ColorPalette As CP)
        Select Case ColorPalette.Windows8.Start
            Case 1
                W8_start.Image = My.WinRes.MetroStart_1.Resize(48, 48)
            Case 2
                W8_start.Image = My.WinRes.MetroStart_2.Resize(48, 48)
            Case 3
                W8_start.Image = My.WinRes.MetroStart_3.Resize(48, 48)
            Case 4
                W8_start.Image = My.WinRes.MetroStart_4.Resize(48, 48)
            Case 5
                W8_start.Image = My.WinRes.MetroStart_5.Resize(48, 48)
            Case 6
                W8_start.Image = My.WinRes.MetroStart_6.Resize(48, 48)
            Case 7
                W8_start.Image = My.WinRes.MetroStart_7.Resize(48, 48)
            Case 8
                W8_start.Image = My.WinRes.MetroStart_8.Resize(48, 48)
            Case 9
                W8_start.Image = My.WinRes.MetroStart_9.Resize(48, 48)
            Case 10
                W8_start.Image = My.WinRes.MetroStart_10.Resize(48, 48)
            Case 11
                W8_start.Image = My.WinRes.MetroStart_11.Resize(48, 48)
            Case 12
                W8_start.Image = My.WinRes.MetroStart_12.Resize(48, 48)
            Case 13
                W8_start.Image = My.WinRes.MetroStart_13.Resize(48, 48)
            Case 14
                W8_start.Image = My.WinRes.MetroStart_14.Resize(48, 48)
            Case 15
                W8_start.Image = My.WinRes.MetroStart_15.Resize(48, 48)
            Case 16
                W8_start.Image = My.WinRes.MetroStart_16.Resize(48, 48)
            Case 17
                W8_start.Image = My.WinRes.MetroStart_17.Resize(48, 48)
            Case 18
                W8_start.Image = My.WinRes.MetroStart_18.Resize(48, 48)
            Case 19
                W8_start.Image = ColorPalette.Windows8.PersonalColors_Background.ToBitmap(New Size(48, 48))
            Case 20
                W8_start.Image = My.Application.GetWallpaper.Resize(48, 48)
            Case Else
                W8_start.Image = My.WinRes.MetroStart_1.Resize(48, 48)
        End Select
    End Sub

    Sub ApplyBackLogonUI(ColorPalette As CP)

        Select Case ColorPalette.Windows8.LogonUI
            Case 0
                W8_logonui.Image = Color.FromArgb(34, 34, 34).ToBitmap(New Size(48, 48))

            Case 1
                W8_logonui.Image = Color.FromArgb(34, 34, 34).ToBitmap(New Size(48, 48))

            Case 2
                W8_logonui.Image = Color.FromArgb(34, 34, 34).ToBitmap(New Size(48, 48))

            Case 3
                W8_logonui.Image = Color.FromArgb(34, 34, 34).ToBitmap(New Size(48, 48))

            Case 4
                W8_logonui.Image = Color.FromArgb(42, 27, 0).ToBitmap(New Size(48, 48))

            Case 5
                W8_logonui.Image = Color.FromArgb(59, 0, 3).ToBitmap(New Size(48, 48))

            Case 6
                W8_logonui.Image = Color.FromArgb(65, 0, 49).ToBitmap(New Size(48, 48))

            Case 7
                W8_logonui.Image = Color.FromArgb(41, 0, 66).ToBitmap(New Size(48, 48))

            Case 8
                W8_logonui.Image = Color.FromArgb(30, 3, 84).ToBitmap(New Size(48, 48))

            Case 9
                W8_logonui.Image = Color.FromArgb(0, 31, 66).ToBitmap(New Size(48, 48))

            Case 10
                W8_logonui.Image = Color.FromArgb(3, 66, 82).ToBitmap(New Size(48, 48))

            Case 11
                W8_logonui.Image = Color.FromArgb(30, 144, 255).ToBitmap(New Size(48, 48))

            Case 12
                W8_logonui.Image = Color.FromArgb(4, 63, 0).ToBitmap(New Size(48, 48))

            Case 13
                W8_logonui.Image = Color.FromArgb(188, 90, 28).ToBitmap(New Size(48, 48))

            Case 14
                W8_logonui.Image = Color.FromArgb(155, 28, 29).ToBitmap(New Size(48, 48))

            Case 15
                W8_logonui.Image = Color.FromArgb(152, 28, 90).ToBitmap(New Size(48, 48))

            Case 16
                W8_logonui.Image = Color.FromArgb(88, 28, 152).ToBitmap(New Size(48, 48))

            Case 17
                W8_logonui.Image = Color.FromArgb(28, 74, 153).ToBitmap(New Size(48, 48))

            Case 18
                W8_logonui.Image = Color.FromArgb(69, 143, 221).ToBitmap(New Size(48, 48))

            Case 19
                W8_logonui.Image = Color.FromArgb(0, 141, 142).ToBitmap(New Size(48, 48))

            Case 20
                W8_logonui.Image = Color.FromArgb(120, 168, 33).ToBitmap(New Size(48, 48))

            Case 21
                W8_logonui.Image = Color.FromArgb(191, 142, 16).ToBitmap(New Size(48, 48))

            Case 22
                W8_logonui.Image = Color.FromArgb(219, 80, 171).ToBitmap(New Size(48, 48))

            Case 23
                W8_logonui.Image = Color.FromArgb(154, 154, 154).ToBitmap(New Size(48, 48))

            Case 24
                W8_logonui.Image = Color.FromArgb(88, 88, 88).ToBitmap(New Size(48, 48))

            Case Else
                W8_logonui.Image = Color.FromArgb(34, 34, 34).ToBitmap(New Size(48, 48))

        End Select


    End Sub

    Function GetTintedWallpaper(WT As CP.Structures.WallpaperTone) As Bitmap
        Dim HSL As New HSLFilter With {
            .Hue = WT.H,
            .Saturation = (WT.S - 50) * 2,
            .Lightness = (WT.L - 50) * 2
        }

        Dim img As Bitmap


        If Not IO.File.Exists([WT].Image) Then
            If My.WXP Then
                [WT].Image = My.PATH_Windows & "\Web\Wallpaper\Bliss.bmp"
            Else
                [WT].Image = My.PATH_Windows & "\Web\Wallpaper\Windows\img0.jpg"
            End If
        End If

        Dim S As New IO.FileStream([WT].Image, IO.FileMode.Open, IO.FileAccess.Read)
        img = Image.FromStream(S).Resize(pnl_preview.Size)
        S.Close()

        Return HSL.ExecuteFilter(img)
    End Function
#End Region

#Region "Misc"
    Enum WinVer
        W11
        W10
        W8
        W7
        WVista
        WXP
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

    Sub UpdateHint_Dashboard(Sender As Object, e As EventArgs)
        Label61.Text = Sender.Tag
    End Sub

    Sub EraseHint_Dashboard()
        Label61.Text = ""
    End Sub

#End Region

    Sub AutoUpdatesCheck()
        StableInt = 0 : BetaInt = 0 : UpdateChannel = 0 : ChannelFixer = 0
        If My.[Settings].UpdateChannel = XeSettings.UpdateChannels.Stable Then ChannelFixer = 0
        If My.[Settings].UpdateChannel = XeSettings.UpdateChannels.Beta Then ChannelFixer = 1
        BackgroundWorker1.RunWorkerAsync()
    End Sub

    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        If IsNetworkAvailable() Then
            Try
                Dim WebCL As New WebClient
                RaiseUpdate = False
                ver = ""

                Updates_ls = WebCL.DownloadString(My.Resources.Link_Updates).CList

                For x = 0 To Updates_ls.Count - 1
                    If Not String.IsNullOrEmpty(Updates_ls(x)) And Not Updates_ls(x).IndexOf("#") = 0 Then
                        If Updates_ls(x).Split(" ")(0) = "Stable" Then StableInt = x
                        If Updates_ls(x).Split(" ")(0) = "Beta" Then BetaInt = x
                    End If
                Next

                If ChannelFixer = 0 Then UpdateChannel = StableInt
                If ChannelFixer = 1 Then UpdateChannel = BetaInt

                ver = Updates_ls(UpdateChannel).Split(" ")(1)

                RaiseUpdate = (ver > My.Application.Info.Version.ToString)
            Catch
            End Try
        End If
    End Sub

    Private Sub BackgroundWorker1_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        If RaiseUpdate Then
            Updates.ls = Updates_ls
            NotifyUpdates.Visible = True
            XenonButton5.Image = My.Resources.Update_Dot
            NotifyUpdates.ShowBalloonTip(10000, My.Application.Info.Title, String.Format("{0}. {1} {2}", My.Lang.NewUpdate, My.Lang.Version, ver), ToolTipIcon.Info)
        End If
    End Sub

    Private Sub NotifyIcon1_BalloonTipClicked(sender As Object, e As EventArgs) Handles NotifyUpdates.BalloonTipClicked
        NotifyUpdates.Visible = False

        If My.Application.OpenForms(Updates.Name) Is Nothing Then
            Updates.ShowDialog()
        Else
            Updates.Focus()
        End If
    End Sub

    Private Sub MainFrm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _Shown = False
        Visible = False
        NotifyUpdates.Icon = Icon
        TreeView1.ImageList = My.Notifications_IL

        Me.Size = New Size(My.[Settings].MainFormWidth, My.[Settings].MainFormHeight)
        Me.WindowState = My.[Settings].MainFormStatus

        If My.W11 Then PreviewConfig = WinVer.W11
        If My.W10 Then PreviewConfig = WinVer.W10
        If My.W8 Then PreviewConfig = WinVer.W8
        If My.W7 Then PreviewConfig = WinVer.W7
        If My.WVista Then PreviewConfig = WinVer.WVista
        If My.WXP Then PreviewConfig = WinVer.WXP

        If Not My.Application.ExternalLink Then
            CP = New CP(CP_Type.Registry)
        Else
            CP = New CP(CP_Type.File, My.Application.ExternalLink_File)
            OpenFileDialog1.FileName = My.Application.ExternalLink_File
            SaveFileDialog1.FileName = My.Application.ExternalLink_File
            My.Application.ExternalLink = False
            My.Application.ExternalLink_File = ""
        End If

        CP_Original = CP.Clone
        CP_FirstTime = CP.Clone

        Select_W11.Image = My.Resources.Native11
        Select_W10.Image = My.Resources.Native10
        Select_W8.Image = My.Resources.Native8
        Select_W7.Image = My.Resources.Native7
        Select_WVista.Image = My.Resources.NativeVista
        Select_WXP.Image = My.Resources.NativeXP

        If PreviewConfig = WinVer.W11 Then
            TablessControl1.SelectedIndex = 0
            XenonButton20.Image = My.Resources.add_win11
            Select_W11.Checked = True

        ElseIf PreviewConfig = WinVer.W10 Then
            TablessControl1.SelectedIndex = 1
            XenonButton20.Image = My.Resources.add_win10
            Select_W10.Checked = True

        ElseIf PreviewConfig = WinVer.W8 Then
            TablessControl1.SelectedIndex = 2
            XenonButton20.Image = My.Resources.add_win8
            Select_W8.Checked = True

        ElseIf PreviewConfig = WinVer.W7 Then
            TablessControl1.SelectedIndex = 3
            XenonButton20.Image = My.Resources.add_win7
            Select_W7.Checked = True

        ElseIf PreviewConfig = WinVer.WVista Then
            TablessControl1.SelectedIndex = 4
            XenonButton20.Image = My.Resources.add_winvista
            Select_WVista.Checked = True

        ElseIf PreviewConfig = WinVer.WXP Then
            TablessControl1.SelectedIndex = 5
            XenonButton20.Image = My.Resources.add_winxp
            Select_WXP.Checked = True

        Else
            TablessControl1.SelectedIndex = 0
            XenonButton20.Image = My.Resources.add_win11
            Select_W11.Checked = True
        End If

        ApplyDarkMode(Me)
        MakeItDoubleBuffered(Me)
        MakeItDoubleBuffered(TreeView1)
        MakeItDoubleBuffered(TablessControl1)
        DoubleBufferAll(tabs_preview)

        Adjust_Preview()
        ApplyCPValues(CP)
        ApplyDefaultCPValues()
        ApplyLivePreviewFromCP(CP)
        AdjustClassicPreview()

        WXP_Alert2.Size = WXP_Alert2.Parent.Size - New Size(40, 40)
        WXP_Alert2.Location = New Point(20, 20)

        Visible = True
    End Sub

    Private Sub MainFrm_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        _Shown = True

        For Each btn As XenonButton In MainToolbar.Controls.OfType(Of XenonButton)
            AddHandler btn.MouseEnter, AddressOf UpdateHint
            AddHandler btn.Enter, AddressOf UpdateHint
            AddHandler btn.MouseLeave, AddressOf EraseHint
            AddHandler btn.Leave, AddressOf EraseHint
        Next

        For Each btn As XenonButton In XenonGroupBox3.Controls.OfType(Of XenonButton)
            AddHandler btn.MouseEnter, AddressOf UpdateHint_Dashboard
            AddHandler btn.Enter, AddressOf UpdateHint_Dashboard
            AddHandler btn.MouseLeave, AddressOf EraseHint_Dashboard
            AddHandler btn.Leave, AddressOf EraseHint_Dashboard
        Next

        For Each btn As XenonRadioImage In previewContainer.Controls.OfType(Of XenonRadioImage)
            AddHandler btn.MouseEnter, AddressOf UpdateHint
            AddHandler btn.Enter, AddressOf UpdateHint
            AddHandler btn.MouseLeave, AddressOf EraseHint
            AddHandler btn.Leave, AddressOf EraseHint
        Next

        If My.[Settings].AutoUpdatesChecking Then AutoUpdatesCheck()

        If My.Application.ShowWhatsNew Then Whatsnew.ShowDialog()
    End Sub

    Private Sub MainFrm_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        If Me.WindowState = FormWindowState.Normal Then
            My.[Settings].MainFormWidth = Me.Size.Width
            My.[Settings].MainFormHeight = Me.Size.Height
        End If

        If Me.WindowState <> FormWindowState.Minimized Then
            My.[Settings].MainFormStatus = Me.WindowState
        End If

        My.[Settings].Save(XeSettings.Mode.Registry)
    End Sub

    Protected Overrides Sub OnFormClosing(ByVal e As FormClosingEventArgs)
        If CP <> CP_Original Then

            If My.[Settings].ShowSaveConfirmation Then

                Select Case ComplexSave.ShowDialog
                    Case DialogResult.Yes

                        Dim r As String() = My.[Settings].ComplexSaveResult.Split(".")
                        Dim r1 As String = r(0)
                        Dim r2 As String = r(1)

                        Select Case r1
                            Case 0              '' Save
                                If IO.File.Exists(SaveFileDialog1.FileName) Then
                                    CP.Save(CP.CP_Type.File, SaveFileDialog1.FileName)
                                    CP_Original = CP.Clone
                                Else
                                    If SaveFileDialog1.ShowDialog = DialogResult.OK Then
                                        CP.Save(CP.CP_Type.File, SaveFileDialog1.FileName)
                                        CP_Original = CP.Clone
                                    Else
                                        e.Cancel = True
                                    End If
                                End If
                            Case 1              '' Save As
                                If SaveFileDialog1.ShowDialog = DialogResult.OK Then
                                    CP.Save(CP.CP_Type.File, SaveFileDialog1.FileName)
                                    CP_Original = CP.Clone
                                Else
                                    e.Cancel = True
                                End If
                        End Select

                        Select Case r2
                            Case 1      '' Apply   ' Case 0= Don't Apply
                                Apply_Theme()
                        End Select

                    Case DialogResult.No
                        e.Cancel = False
                        If (My.W7 Or My.W8) And My.[Settings].Win7LivePreview Then RefreshDWM(CP_Original)
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
                If PreviewConfig = WinVer.W11 Then ApplyLivePreviewFromCP(CP)
            End If
            Exit Sub
        End If

        Dim CList As New List(Of Control) From {sender, XenonWindow1}

        Dim C As Color = ColorPickerDlg.Pick(CList)
        CP.Windows11.Titlebar_Active = Color.FromArgb(255, C)
        If PreviewConfig = WinVer.W11 Then ApplyLivePreviewFromCP(CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()

        'If Not CP.Windows11.ApplyAccentonTitlebars Then
        'Notify(My.Lang.CP_TitlebarToggle, My.Resources.notify_info, 4000)
        'End If
    End Sub

    Private Sub W11_InactiveTitlebar_pick_Click(sender As Object, e As EventArgs) Handles W11_InactiveTitlebar_pick.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                CP.Windows11.Titlebar_Inactive = sender.BackColor
                If PreviewConfig = WinVer.W11 Then ApplyLivePreviewFromCP(CP)
            End If
            Exit Sub
        End If

        Dim CList As New List(Of Control) From {sender, XenonWindow2}

        Dim _Conditions As New Conditions With {.Window_InactiveTitlebar = True}
        Dim C As Color = ColorPickerDlg.Pick(CList, _Conditions)

        CP.Windows11.Titlebar_Inactive = Color.FromArgb(255, C)

        If PreviewConfig = WinVer.W11 Then ApplyLivePreviewFromCP(CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()

        'If Not CP.Windows11.ApplyAccentonTitlebars Then
        'Notify(My.Lang.CP_TitlebarToggle, My.Resources.notify_info, 4000)
        'End If
    End Sub

    Private Sub W11_WinMode_Toggle_CheckedChanged(sender As Object, e As EventArgs) Handles W11_WinMode_Toggle.CheckedChanged
        If _Shown Then
            CP.Windows11.WinMode_Light = Not sender.Checked
            If PreviewConfig = WinVer.W11 Then ApplyLivePreviewFromCP(CP)
        End If
    End Sub

    Private Sub W11_AppMode_Toggle_CheckedChanged(sender As Object, e As EventArgs) Handles W11_AppMode_Toggle.CheckedChanged
        If _Shown Then
            CP.Windows11.AppMode_Light = Not sender.Checked
            If PreviewConfig = WinVer.W11 Then ApplyLivePreviewFromCP(CP)
        End If
    End Sub

    Private Sub W11_Transparency_Toggle_CheckedChanged(sender As Object, e As EventArgs) Handles W11_Transparency_Toggle.CheckedChanged
        If _Shown Then
            CP.Windows11.Transparency = sender.Checked
            If PreviewConfig = WinVer.W11 Then ApplyLivePreviewFromCP(CP)
        End If
    End Sub

    Private Sub W11_ShowAccentOnTitlebarAndBorders_Toggle_CheckedChanged(sender As Object, e As EventArgs) Handles W11_ShowAccentOnTitlebarAndBorders_Toggle.CheckedChanged
        If _Shown Then
            CP.Windows11.ApplyAccentonTitlebars = sender.Checked
            If PreviewConfig = WinVer.W11 Then ApplyLivePreviewFromCP(CP)
        End If
    End Sub

    Private Sub W11_Accent_None_CheckedChanged(sender As Object) Handles W11_Accent_None.CheckedChanged
        If _Shown Then
            CP.Windows11.ApplyAccentonTaskbar = ApplyAccentonTaskbar_Level.None
            If PreviewConfig = WinVer.W11 Then ApplyLivePreviewFromCP(CP)
        End If
    End Sub

    Private Sub W11_Accent_Taskbar_CheckedChanged(sender As Object) Handles W11_Accent_Taskbar.CheckedChanged
        If _Shown Then
            CP.Windows11.ApplyAccentonTaskbar = ApplyAccentonTaskbar_Level.Taskbar
            If PreviewConfig = WinVer.W11 Then ApplyLivePreviewFromCP(CP)
        End If
    End Sub

    Private Sub W11_Accent_StartTaskbar_CheckedChanged(sender As Object) Handles W11_Accent_StartTaskbar.CheckedChanged
        If _Shown Then
            CP.Windows11.ApplyAccentonTaskbar = ApplyAccentonTaskbar_Level.Taskbar_Start_AC
            If PreviewConfig = WinVer.W11 Then ApplyLivePreviewFromCP(CP)
        End If
    End Sub

    Private Sub W11_Color_Index1_Click(sender As Object, e As EventArgs) Handles W11_Color_Index1.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                CP.Windows11.Color_Index1 = sender.BackColor
                If PreviewConfig = WinVer.W11 Then ApplyLivePreviewFromCP(CP)
            End If
            Exit Sub
        End If

        Dim CList As New List(Of Control)
        Dim C As Color
        CList.Add(sender)

        CList.Add(taskbar)

        If ExplorerPatcher.IsAllowed Then
            If Not CP.Windows11.WinMode_Light Then
                CList.Add(ActionCenter)
                Dim _Conditions As New Conditions With {.AppUnderlineOnly = True, .ActionCenterBtn = True}
                C = ColorPickerDlg.Pick(CList, _Conditions)
            Else
                Dim _Conditions As New Conditions With {.AppUnderlineWithTaskbar = True}
                C = ColorPickerDlg.Pick(CList, _Conditions)
            End If
        Else
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
        End If

        CP.Windows11.Color_Index1 = Color.FromArgb(255, C)
        If PreviewConfig = WinVer.W11 Then ApplyLivePreviewFromCP(CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()
    End Sub

    Private Sub W11_TaskbarFrontAndFoldersOnStart_pick_Click(sender As Object, e As EventArgs) Handles W11_TaskbarFrontAndFoldersOnStart_pick.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                CP.Windows11.Color_Index5 = sender.BackColor
                If PreviewConfig = WinVer.W11 Then ApplyLivePreviewFromCP(CP)
            End If
            Exit Sub
        End If

        Dim CList As New List(Of Control) From {sender}

        If ExplorerPatcher.IsAllowed Then
            If Not CP.Windows11.WinMode_Light Then
                CList.Add(ActionCenter)
                CList.Add(taskbar)
                If Not My.EP.UseStart10 Then
                    CList.Add(start)
                End If
            Else
                CList.Add(lnk_preview)
            End If

        Else
            If Not CP.Windows11.WinMode_Light Then
                CList.Add(taskbar)
                CList.Add(start)
                CList.Add(ActionCenter)
            Else
                CList.Add(lnk_preview)
            End If
        End If

        Dim C As Color = ColorPickerDlg.Pick(CList)
        CP.Windows11.Color_Index5 = Color.FromArgb(255, C)
        If PreviewConfig = WinVer.W11 Then ApplyLivePreviewFromCP(CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()
    End Sub

    Private Sub W11_Color_Index0_Click(sender As Object, e As EventArgs) Handles W11_Color_Index0.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                CP.Windows11.Color_Index0 = sender.BackColor
                If PreviewConfig = WinVer.W11 Then ApplyLivePreviewFromCP(CP)
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
        If PreviewConfig = WinVer.W11 Then ApplyLivePreviewFromCP(CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()

    End Sub

    Private Sub W11_Color_Index3_Click(sender As Object, e As EventArgs) Handles W11_Color_Index3.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                CP.Windows11.Color_Index3 = sender.BackColor
                If PreviewConfig = WinVer.W11 Then ApplyLivePreviewFromCP(CP)
            End If
            Exit Sub
        End If

        Dim CList As New List(Of Control)
        Dim C As Color

        CList.Add(sender)


        CList.Add(setting_icon_preview)
        C = ColorPickerDlg.Pick(CList)

        CP.Windows11.Color_Index3 = Color.FromArgb(255, C)
        If PreviewConfig = WinVer.W11 Then ApplyLivePreviewFromCP(CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()
    End Sub

    Private Sub W11_Color_Index6_Click(sender As Object, e As EventArgs) Handles W11_Color_Index6.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                CP.Windows11.Color_Index6 = sender.BackColor
                If PreviewConfig = WinVer.W11 Then ApplyLivePreviewFromCP(CP)
            End If
            Exit Sub
        End If

        Dim CList As New List(Of Control)
        Dim C As Color

        CList.Add(sender)
        Dim _Conditions As New Conditions

        If Not ExplorerPatcher.IsAllowed Then
            CList.Add(taskbar)
            _Conditions = New Conditions With {.AppBackgroundOnly = True}
        End If

        C = ColorPickerDlg.Pick(CList, _Conditions)

        CP.Windows11.Color_Index6 = Color.FromArgb(255, C)
        If PreviewConfig = WinVer.W11 Then ApplyLivePreviewFromCP(CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()
    End Sub

    Private Sub W11_Color_Index5_Click(sender As Object, e As EventArgs) Handles W11_Color_Index5.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                CP.Windows11.StartMenu_Accent = sender.BackColor
                If PreviewConfig = WinVer.W11 Then ApplyLivePreviewFromCP(CP)
            End If
            Exit Sub
        End If


        Dim CList As New List(Of Control) From {sender}

        Dim C As Color = ColorPickerDlg.Pick(CList)
        CP.Windows11.StartMenu_Accent = Color.FromArgb(255, C)
        If PreviewConfig = WinVer.W11 Then ApplyLivePreviewFromCP(CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()

    End Sub

    Private Sub W11_Color_Index4_pick_Click(sender As Object, e As EventArgs) Handles W11_Color_Index4.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                CP.Windows11.Color_Index2 = sender.BackColor
                If PreviewConfig = WinVer.W11 Then ApplyLivePreviewFromCP(CP)
            End If
            Exit Sub
        End If


        Dim CList As New List(Of Control) From {sender}

        Dim C As Color = ColorPickerDlg.Pick(CList)
        CP.Windows11.Color_Index2 = Color.FromArgb(255, C)
        If PreviewConfig = WinVer.W11 Then ApplyLivePreviewFromCP(CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()
    End Sub

    Private Sub W11_Color_Index2_Click(sender As Object, e As EventArgs) Handles W11_Color_Index2.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                CP.Windows11.Color_Index4 = sender.BackColor
                If PreviewConfig = WinVer.W11 Then ApplyLivePreviewFromCP(CP)
            End If
            Exit Sub
        End If


        Dim CList As New List(Of Control) From {sender}

        Dim C As Color

        If ExplorerPatcher.IsAllowed Then
            If My.EP.UseStart10 Then
                CList.Add(start)
                C = ColorPickerDlg.Pick(CList)

            Else
                If Not W11_Transparency_Toggle.Checked Then
                    CList.Add(taskbar)
                    CList.Add(start)
                    CList.Add(ActionCenter)
                End If

                If CP.Windows11.WinMode_Light Then
                    CList.Add(start)
                    CList.Add(ActionCenter)

                    Dim _Conditions As New Conditions With {.StartSearchOnly = True, .ActionCenterBtn = True}
                    C = ColorPickerDlg.Pick(CList, _Conditions)
                Else
                    Dim _Conditions As New Conditions With {.StartColorOnly = True}
                    C = ColorPickerDlg.Pick(CList, _Conditions)
                End If
            End If
        Else
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
        End If


        CP.Windows11.Color_Index4 = Color.FromArgb(255, C)
        If PreviewConfig = WinVer.W11 Then ApplyLivePreviewFromCP(CP)

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
        Dim C As Color = ColorPickerDlg.Pick(CList)

        CP.Windows11.Color_Index7 = Color.FromArgb(255, C)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()
    End Sub

    Private Sub W11_XenonButton8_Click_1(sender As Object, e As EventArgs) Handles W11_XenonButton8.Click
        MsgBox(My.Lang.TitlebarColorNotice, MsgBoxStyle.Information)
    End Sub

#End Region

#Region "Windows 10"
    Private Sub W10_ActiveTitlebar_pick_Click(sender As Object, e As EventArgs) Handles W10_ActiveTitlebar_pick.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                CP.Windows10.Titlebar_Active = sender.BackColor
                If PreviewConfig = WinVer.W10 Then ApplyLivePreviewFromCP(CP)
            End If
            Exit Sub
        End If

        Dim CList As New List(Of Control) From {sender, XenonWindow1}

        Dim C As Color = ColorPickerDlg.Pick(CList)
        CP.Windows10.Titlebar_Active = Color.FromArgb(255, C)
        If PreviewConfig = WinVer.W10 Then ApplyLivePreviewFromCP(CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()

        'If Not CP.Windows10.ApplyAccentonTitlebars Then
        'Notify(My.Lang.CP_TitlebarToggle, My.Resources.notify_info, 4000)
        'End If
    End Sub

    Private Sub W10_InactiveTitlebar_pick_Click(sender As Object, e As EventArgs) Handles W10_InactiveTitlebar_pick.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                CP.Windows10.Titlebar_Inactive = sender.BackColor
                If PreviewConfig = WinVer.W10 Then ApplyLivePreviewFromCP(CP)
            End If
            Exit Sub
        End If

        Dim CList As New List(Of Control) From {sender, XenonWindow2}

        Dim _Conditions As New Conditions With {.Window_InactiveTitlebar = True}
        Dim C As Color = ColorPickerDlg.Pick(CList, _Conditions)

        CP.Windows10.Titlebar_Inactive = Color.FromArgb(255, C)

        If PreviewConfig = WinVer.W10 Then ApplyLivePreviewFromCP(CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()

        'If Not CP.Windows10.ApplyAccentonTitlebars Then
        'Notify(My.Lang.CP_TitlebarToggle, My.Resources.notify_info, 4000)
        'End If
    End Sub

    Private Sub W10_WinMode_Toggle_CheckedChanged(sender As Object, e As EventArgs) Handles W10_WinMode_Toggle.CheckedChanged
        If _Shown Then
            CP.Windows10.WinMode_Light = Not sender.Checked
            If PreviewConfig = WinVer.W10 Then ApplyLivePreviewFromCP(CP)
        End If
    End Sub

    Private Sub W10_AppMode_Toggle_CheckedChanged(sender As Object, e As EventArgs) Handles W10_AppMode_Toggle.CheckedChanged
        If _Shown Then
            CP.Windows10.AppMode_Light = Not sender.Checked
            If PreviewConfig = WinVer.W10 Then ApplyLivePreviewFromCP(CP)
        End If
    End Sub

    Private Sub W10_Transparency_Toggle_CheckedChanged(sender As Object, e As EventArgs) Handles W10_Transparency_Toggle.CheckedChanged
        If _Shown Then
            CP.Windows10.Transparency = sender.Checked
            If PreviewConfig = WinVer.W10 Then ApplyLivePreviewFromCP(CP)
        End If
    End Sub

    Private Sub W10_ShowAccentOnTitlebarAndBorders_Toggle_CheckedChanged(sender As Object, e As EventArgs) Handles W10_ShowAccentOnTitlebarAndBorders_Toggle.CheckedChanged
        If _Shown Then
            CP.Windows10.ApplyAccentonTitlebars = sender.Checked
            If PreviewConfig = WinVer.W10 Then ApplyLivePreviewFromCP(CP)
        End If
    End Sub

    Private Sub W10_Accent_None_CheckedChanged(sender As Object) Handles W10_Accent_None.CheckedChanged
        If _Shown Then
            CP.Windows10.ApplyAccentonTaskbar = ApplyAccentonTaskbar_Level.None
            If PreviewConfig = WinVer.W10 Then ApplyLivePreviewFromCP(CP)
        End If
    End Sub

    Private Sub W10_Accent_Taskbar_CheckedChanged(sender As Object) Handles W10_Accent_Taskbar.CheckedChanged
        If _Shown Then
            CP.Windows10.ApplyAccentonTaskbar = ApplyAccentonTaskbar_Level.Taskbar
            If PreviewConfig = WinVer.W10 Then ApplyLivePreviewFromCP(CP)
        End If
    End Sub

    Private Sub W10_Accent_StartTaskbar_CheckedChanged(sender As Object) Handles W10_Accent_StartTaskbar.CheckedChanged
        If _Shown Then
            CP.Windows10.ApplyAccentonTaskbar = ApplyAccentonTaskbar_Level.Taskbar_Start_AC
            If PreviewConfig = WinVer.W10 Then ApplyLivePreviewFromCP(CP)
        End If
    End Sub

    Private Sub W10_TBTransparency_Toggle_CheckedChanged(sender As Object, e As EventArgs) Handles W10_TBTransparency_Toggle.CheckedChanged
        If _Shown Then
            CP.Windows10.IncreaseTBTransparency = sender.Checked
            If PreviewConfig = WinVer.W10 Then ApplyLivePreviewFromCP(CP)
        End If
    End Sub

    Private Sub W10_Color_Index1_Click(sender As Object, e As EventArgs) Handles W10_Color_Index1.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                CP.Windows10.Color_Index1 = sender.BackColor
                If PreviewConfig = WinVer.W10 Then ApplyLivePreviewFromCP(CP)
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
        If PreviewConfig = WinVer.W10 Then ApplyLivePreviewFromCP(CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()
    End Sub

    Private Sub W10_TaskbarFrontAndFoldersOnStart_pick_Click(sender As Object, e As EventArgs) Handles W10_TaskbarFrontAndFoldersOnStart_pick.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                CP.Windows10.Color_Index5 = sender.BackColor
                If PreviewConfig = WinVer.W10 Then ApplyLivePreviewFromCP(CP)
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
        If PreviewConfig = WinVer.W10 Then ApplyLivePreviewFromCP(CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()

    End Sub

    Private Sub W10_Color_Index0_Click(sender As Object, e As EventArgs) Handles W10_Color_Index0.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                CP.Windows10.Color_Index0 = sender.BackColor
                If PreviewConfig = WinVer.W10 Then ApplyLivePreviewFromCP(CP)
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
        If PreviewConfig = WinVer.W10 Then ApplyLivePreviewFromCP(CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()

    End Sub

    Private Sub W10_Color_Index3_Click(sender As Object, e As EventArgs) Handles W10_Color_Index3.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                CP.Windows10.Color_Index3 = sender.BackColor
                If PreviewConfig = WinVer.W10 Then ApplyLivePreviewFromCP(CP)
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
        If PreviewConfig = WinVer.W10 Then ApplyLivePreviewFromCP(CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()
    End Sub

    Private Sub W10_Color_Index6_Click(sender As Object, e As EventArgs) Handles W10_Color_Index6.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                CP.Windows10.Color_Index6 = sender.BackColor
                If PreviewConfig = WinVer.W10 Then ApplyLivePreviewFromCP(CP)
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
        If PreviewConfig = WinVer.W10 Then ApplyLivePreviewFromCP(CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()
    End Sub

    Private Sub W10_Color_Index5_Click(sender As Object, e As EventArgs) Handles W10_Color_Index5.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                CP.Windows10.StartMenu_Accent = sender.BackColor
                If PreviewConfig = WinVer.W10 Then ApplyLivePreviewFromCP(CP)
            End If
            Exit Sub
        End If


        Dim CList As New List(Of Control) From {sender}


        Dim C As Color = ColorPickerDlg.Pick(CList)
        CP.Windows10.StartMenu_Accent = Color.FromArgb(255, C)
        If PreviewConfig = WinVer.W10 Then ApplyLivePreviewFromCP(CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()

    End Sub

    Private Sub W10_Color_Index4_pick_Click(sender As Object, e As EventArgs) Handles W10_Color_Index4.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                CP.Windows10.Color_Index2 = sender.BackColor
                If PreviewConfig = WinVer.W10 Then ApplyLivePreviewFromCP(CP)
            End If
            Exit Sub
        End If


        Dim CList As New List(Of Control) From {sender}

        If PreviewConfig = WinVer.W10 Then
            'CList.Add(taskbar) 'Start Icon Hover
        End If

        Dim C As Color = ColorPickerDlg.Pick(CList)
        CP.Windows10.Color_Index2 = Color.FromArgb(255, C)
        If PreviewConfig = WinVer.W10 Then ApplyLivePreviewFromCP(CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()

    End Sub

    Private Sub W10_Color_Index2_Click(sender As Object, e As EventArgs) Handles W10_Color_Index2.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                CP.Windows10.Color_Index4 = sender.BackColor
                If PreviewConfig = WinVer.W10 Then ApplyLivePreviewFromCP(CP)
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
        If PreviewConfig = WinVer.W10 Then ApplyLivePreviewFromCP(CP)

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
        Dim C As Color = ColorPickerDlg.Pick(CList)

        CP.Windows10.Color_Index7 = Color.FromArgb(255, C)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()
    End Sub

    Private Sub W10_XenonButton8_Click_1(sender As Object, e As EventArgs) Handles W10_XenonButton8.Click
        MsgBox(My.Lang.TitlebarColorNotice, MsgBoxStyle.Information)
    End Sub

    Private Sub W10_XenonButton25_Click(sender As Object, e As EventArgs) Handles W10_XenonButton25.Click
        MsgBox(My.Lang.CP_AccentOnTaskbarTib, MsgBoxStyle.Information)
    End Sub

#End Region

#Region "Windows 8.1"
    Private Sub W8_ColorizationColor_pick_Click(sender As Object, e As EventArgs) Handles W8_ColorizationColor_pick.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                CP.Windows8.ColorizationColor = sender.BackColor
                If PreviewConfig = WinVer.W8 Then ApplyLivePreviewFromCP(CP)
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

        If PreviewConfig = WinVer.W8 Then ApplyLivePreviewFromCP(CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()
    End Sub

    Private Sub W8_ColorizationBalance_bar_Scroll(sender As Object) Handles W8_ColorizationBalance_bar.Scroll
        If _Shown Then
            W8_ColorizationBalance_val.Text = sender.Value.ToString()
            CP.Windows8.ColorizationColorBalance = W8_ColorizationBalance_bar.Value
            If PreviewConfig = WinVer.W8 Then ApplyLivePreviewFromCP(CP)
        End If
    End Sub

    Private Sub W8_start_pick_Click(sender As Object, e As EventArgs) Handles W8_start_pick.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                CP.Windows8.StartColor = sender.BackColor
                If PreviewConfig = WinVer.W8 Then ApplyLivePreviewFromCP(CP)
            End If
            Exit Sub
        End If

        Dim CList As New List(Of Control) From {sender}

        Dim C As Color = ColorPickerDlg.Pick(CList)

        CP.Windows8.StartColor = Color.FromArgb(255, C)

        If PreviewConfig = WinVer.W8 Then ApplyLivePreviewFromCP(CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()
    End Sub

    Private Sub W8_accent_pick_Click(sender As Object, e As EventArgs) Handles W8_accent_pick.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                CP.Windows8.AccentColor = sender.BackColor
                If PreviewConfig = WinVer.W8 Then ApplyLivePreviewFromCP(CP)
            End If
            Exit Sub
        End If

        Dim CList As New List(Of Control) From {sender}

        Dim C As Color = ColorPickerDlg.Pick(CList)

        CP.Windows8.AccentColor = Color.FromArgb(255, C)

        If PreviewConfig = WinVer.W8 Then ApplyLivePreviewFromCP(CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()
    End Sub

    Private Sub W8_personalcls_background_pick_Click(sender As Object, e As EventArgs) Handles W8_personalcls_background_pick.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                CP.Windows8.PersonalColors_Background = sender.BackColor
                If PreviewConfig = WinVer.W8 Then ApplyLivePreviewFromCP(CP)
            End If
            Exit Sub
        End If

        Dim CList As New List(Of Control) From {sender}

        Dim C As Color = ColorPickerDlg.Pick(CList)

        CP.Windows8.PersonalColors_Background = Color.FromArgb(255, C)

        If PreviewConfig = WinVer.W8 Then ApplyLivePreviewFromCP(CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()
    End Sub

    Private Sub W8_personalcolor_accent_pick_Click(sender As Object, e As EventArgs) Handles W8_personalcolor_accent_pick.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                CP.Windows8.PersonalColors_Accent = sender.BackColor
                If PreviewConfig = WinVer.W8 Then ApplyLivePreviewFromCP(CP)
            End If
            Exit Sub
        End If

        Dim CList As New List(Of Control) From {sender}

        Dim C As Color = ColorPickerDlg.Pick(CList)

        CP.Windows8.PersonalColors_Accent = Color.FromArgb(255, C)

        If PreviewConfig = WinVer.W8 Then ApplyLivePreviewFromCP(CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()
    End Sub

    Private Sub W8_ColorizationBalance_val_Click(sender As Object, e As EventArgs) Handles W8_ColorizationBalance_val.Click
        Dim response As String = InputBox(My.Lang.InputValue, sender.text, My.Lang.ItMustBeNumerical)
        sender.Text = Math.Max(Math.Min(Val(response), W8_ColorizationBalance_bar.Maximum), W8_ColorizationBalance_bar.Minimum) : W8_ColorizationBalance_bar.Value = Val(sender.Text)
    End Sub

    Private Sub W8_theme_aero_CheckedChanged(sender As Object) Handles W8_theme_aero.CheckedChanged
        If W8_theme_aero.Checked Then
            CP.Windows8.Theme = CP.AeroTheme.Aero
            If PreviewConfig = WinVer.W8 Then ApplyLivePreviewFromCP(CP)
        End If
    End Sub

    Private Sub W8_theme_aerolite_CheckedChanged(sender As Object) Handles W8_theme_aerolite.CheckedChanged
        If W8_theme_aerolite.Checked Then
            CP.Windows8.Theme = CP.AeroTheme.AeroLite
            If PreviewConfig = WinVer.W8 Then ApplyLivePreviewFromCP(CP)
        End If
    End Sub

    Private Sub W8_start_Click(sender As Object, e As EventArgs) Handles W8_start.Click
        Start8Selector.ShowDialog()
        If PreviewConfig = WinVer.W8 Then ApplyLivePreviewFromCP(CP)
    End Sub

    Private Sub W8_logonui_Click(sender As Object, e As EventArgs) Handles W8_logonui.Click
        LogonUI8Colors.ShowDialog()
        If PreviewConfig = WinVer.W8 Then ApplyLivePreviewFromCP(CP)
    End Sub
#End Region

#Region "Windows 7"
    Private Sub W7_ColorizationColor_pick_Click(sender As Object, e As EventArgs) Handles W7_ColorizationColor_pick.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                CP.Windows7.ColorizationColor = sender.BackColor
                If PreviewConfig = WinVer.W7 Then ApplyLivePreviewFromCP(CP)
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

        If PreviewConfig = WinVer.W7 Then ApplyLivePreviewFromCP(CP)

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

        If PreviewConfig = WinVer.W7 Then ApplyLivePreviewFromCP(CP)

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
            W7_ColorizationColorBalance_val.Text = sender.Value.ToString()
            CP.Windows7.ColorizationColorBalance = W7_ColorizationColorBalance_bar.Value
            If PreviewConfig = WinVer.W7 Then ApplyLivePreviewFromCP(CP)
        End If
    End Sub

    Private Sub W7_ColorizationBlurBalance_bar_Scroll(sender As Object) Handles W7_ColorizationBlurBalance_bar.Scroll
        If _Shown Then
            W7_ColorizationBlurBalance_val.Text = sender.Value.ToString()
            CP.Windows7.ColorizationBlurBalance = W7_ColorizationBlurBalance_bar.Value
            If PreviewConfig = WinVer.W7 Then ApplyLivePreviewFromCP(CP)
        End If
    End Sub

    Private Sub W7_ColorizationGlassReflectionIntensity_bar_Scroll(sender As Object) Handles W7_ColorizationGlassReflectionIntensity_bar.Scroll
        If _Shown Then
            W7_ColorizationGlassReflectionIntensity_val.Text = sender.Value.ToString()
            CP.Windows7.ColorizationGlassReflectionIntensity = W7_ColorizationGlassReflectionIntensity_bar.Value
            If PreviewConfig = WinVer.W7 Then ApplyLivePreviewFromCP(CP)
        End If
    End Sub

    Private Sub W7_theme_classic_CheckedChanged(sender As Object) Handles W7_theme_classic.CheckedChanged
        If W7_theme_classic.Checked Then
            CP.Windows7.Theme = CP.AeroTheme.Classic
            If PreviewConfig = WinVer.W7 Then
                ApplyLivePreviewFromCP(CP)
                Adjust_Preview(False)
                tabs_preview.Refresh()
            End If
        End If

    End Sub

    Private Sub W7_theme_basic_CheckedChanged(sender As Object) Handles W7_theme_basic.CheckedChanged
        If W7_theme_basic.Checked Then
            CP.Windows7.Theme = CP.AeroTheme.Basic
            If PreviewConfig = WinVer.W7 Then
                ApplyLivePreviewFromCP(CP)
                Adjust_Preview(False)
                tabs_preview.Refresh()
            End If
        End If
    End Sub

    Private Sub W7_theme_aeroopaque_CheckedChanged(sender As Object) Handles W7_theme_aeroopaque.CheckedChanged
        If W7_theme_aeroopaque.Checked Then
            CP.Windows7.Theme = CP.AeroTheme.AeroOpaque
            If PreviewConfig = WinVer.W7 Then
                ApplyLivePreviewFromCP(CP)
                Adjust_Preview(False)
                tabs_preview.Refresh()
            End If
        End If
    End Sub

    Private Sub W7_theme_Aero_CheckedChanged(sender As Object) Handles W7_theme_aero.CheckedChanged
        If W7_theme_aero.Checked Then
            CP.Windows7.Theme = CP.AeroTheme.Aero
            If PreviewConfig = WinVer.W7 Then
                ApplyLivePreviewFromCP(CP)
                Adjust_Preview(False)
                tabs_preview.Refresh()
            End If
        End If
    End Sub

    Private Sub W7_ColorizationAfterglowBalance_bar_Scroll(sender As Object) Handles W7_ColorizationAfterglowBalance_bar.Scroll
        If _Shown Then
            W7_ColorizationAfterglowBalance_val.Text = sender.Value.ToString()
            CP.Windows7.ColorizationAfterglowBalance = W7_ColorizationAfterglowBalance_bar.Value
            If PreviewConfig = WinVer.W7 Then ApplyLivePreviewFromCP(CP)
        End If
    End Sub
    Private Sub W7_ColorizationColorBalance_val_Click(sender As Object, e As EventArgs) Handles W7_ColorizationColorBalance_val.Click
        Dim response As String = InputBox(My.Lang.InputValue, sender.text, My.Lang.ItMustBeNumerical)
        sender.Text = Math.Max(Math.Min(Val(response), W7_ColorizationColorBalance_bar.Maximum), W7_ColorizationColorBalance_bar.Minimum) : W7_ColorizationColorBalance_bar.Value = Val(sender.Text)
    End Sub

    Private Sub W7_ColorizationAfterglowBalance_val_Click(sender As Object, e As EventArgs) Handles W7_ColorizationAfterglowBalance_val.Click
        Dim response As String = InputBox(My.Lang.InputValue, sender.text, My.Lang.ItMustBeNumerical)
        sender.Text = Math.Max(Math.Min(Val(response), W7_ColorizationAfterglowBalance_bar.Maximum), W7_ColorizationAfterglowBalance_bar.Minimum) : W7_ColorizationAfterglowBalance_bar.Value = Val(sender.Text)
    End Sub

    Private Sub W7_ColorizationBlurBalance_val_Click(sender As Object, e As EventArgs) Handles W7_ColorizationBlurBalance_val.Click
        Dim response As String = InputBox(My.Lang.InputValue, sender.text, My.Lang.ItMustBeNumerical)
        sender.Text = Math.Max(Math.Min(Val(response), W7_ColorizationBlurBalance_bar.Maximum), W7_ColorizationBlurBalance_bar.Minimum) : W7_ColorizationBlurBalance_bar.Value = Val(sender.Text)
    End Sub

    Private Sub W7_ColorizationGlassReflectionIntensity_val_Click(sender As Object, e As EventArgs) Handles W7_ColorizationGlassReflectionIntensity_val.Click
        Dim response As String = InputBox(My.Lang.InputValue, sender.text, My.Lang.ItMustBeNumerical)
        sender.Text = Math.Max(Math.Min(Val(response), W7_ColorizationGlassReflectionIntensity_bar.Maximum), W7_ColorizationGlassReflectionIntensity_bar.Minimum) : W7_ColorizationGlassReflectionIntensity_bar.Value = Val(sender.Text)
    End Sub

#End Region

#Region "Windows Vista"
    Private Sub WVista_ColorizationColor_pick_Click(sender As Object, e As EventArgs) Handles WVista_ColorizationColor_pick.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                CP.WindowsVista.ColorizationColor = sender.BackColor
                If PreviewConfig = WinVer.WVista Then ApplyLivePreviewFromCP(CP)
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

        CP.WindowsVista.ColorizationColor = Color.FromArgb(255, C)

        If PreviewConfig = WinVer.WVista Then ApplyLivePreviewFromCP(CP)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()
    End Sub

    Private Sub WVista_ColorizationColorBalance_bar_Scroll(sender As Object) Handles WVista_ColorizationColorBalance_bar.Scroll
        If _Shown Then
            WVista_ColorizationColorBalance_val.Text = sender.Value.ToString()
            CP.WindowsVista.Alpha = WVista_ColorizationColorBalance_bar.Value
            If PreviewConfig = WinVer.WVista Then ApplyLivePreviewFromCP(CP)
        End If
    End Sub

    Private Sub WVista_theme_classic_CheckedChanged(sender As Object) Handles WVista_theme_classic.CheckedChanged
        If WVista_theme_classic.Checked Then
            CP.WindowsVista.Theme = CP.AeroTheme.Classic
            If PreviewConfig = WinVer.WVista Then
                ApplyLivePreviewFromCP(CP)
                Adjust_Preview(False)
            End If
        End If

    End Sub

    Private Sub WVista_theme_basic_CheckedChanged(sender As Object) Handles WVista_theme_basic.CheckedChanged
        If WVista_theme_basic.Checked Then
            CP.WindowsVista.Theme = CP.AeroTheme.Basic
            If PreviewConfig = WinVer.WVista Then
                ApplyLivePreviewFromCP(CP)
                Adjust_Preview(False)
            End If
        End If
    End Sub

    Private Sub WVista_theme_aeroopaque_CheckedChanged(sender As Object) Handles WVista_theme_aeroopaque.CheckedChanged
        If WVista_theme_aeroopaque.Checked Then
            CP.WindowsVista.Theme = CP.AeroTheme.AeroOpaque
            If PreviewConfig = WinVer.WVista Then
                ApplyLivePreviewFromCP(CP)
                Adjust_Preview(False)
            End If
        End If
    End Sub

    Private Sub WVista_theme_Vista_CheckedChanged(sender As Object) Handles WVista_theme_aero.CheckedChanged
        If WVista_theme_aero.Checked Then
            CP.WindowsVista.Theme = CP.AeroTheme.Aero
            If PreviewConfig = WinVer.WVista Then
                ApplyLivePreviewFromCP(CP)
                Adjust_Preview(False)
            End If
        End If
    End Sub

    Private Sub WVista_ColorizationColorBalance_val_Click(sender As Object, e As EventArgs) Handles WVista_ColorizationColorBalance_val.Click
        Dim response As String = InputBox(My.Lang.InputValue, sender.text, My.Lang.ItMustBeNumerical)
        sender.Text = Math.Max(Math.Min(Val(response), WVista_ColorizationColorBalance_bar.Maximum), WVista_ColorizationColorBalance_bar.Minimum) : WVista_ColorizationColorBalance_bar.Value = Val(sender.Text)
    End Sub

#End Region

#Region "Windows XP"
    Private Sub WXP_Luna_Blue_CheckedChanged(sender As Object) Handles WXP_Luna_Blue.CheckedChanged
        If WXP_Luna_Blue.Checked Then
            CP.WindowsXP.Theme = WinXPTheme.LunaBlue
            If PreviewConfig = WinVer.WXP Then Adjust_Preview(False)
        End If
    End Sub

    Private Sub WXP_Luna_OliveGreen_CheckedChanged(sender As Object) Handles WXP_Luna_OliveGreen.CheckedChanged
        If WXP_Luna_OliveGreen.Checked Then
            CP.WindowsXP.Theme = WinXPTheme.LunaOliveGreen
            If PreviewConfig = WinVer.WXP Then Adjust_Preview(False)
        End If
    End Sub

    Private Sub WXP_Luna_Silver_CheckedChanged(sender As Object) Handles WXP_Luna_Silver.CheckedChanged
        If WXP_Luna_Silver.Checked Then
            CP.WindowsXP.Theme = WinXPTheme.LunaSilver
            If PreviewConfig = WinVer.WXP Then Adjust_Preview(False)
        End If
    End Sub

    Private Sub WXP_CustomTheme_CheckedChanged(sender As Object) Handles WXP_CustomTheme.CheckedChanged
        If WXP_CustomTheme.Checked Then
            CP.WindowsXP.Theme = WinXPTheme.Custom
            If PreviewConfig = WinVer.WXP Then Adjust_Preview(False)
        End If
    End Sub

    Private Sub WXP_Classic_CheckedChanged(sender As Object) Handles WXP_Classic.CheckedChanged
        If WXP_Classic.Checked Then
            CP.WindowsXP.Theme = WinXPTheme.Classic
            If PreviewConfig = WinVer.WXP Then Adjust_Preview(False)
        End If
    End Sub

    Private Sub XenonTextBox1_TextChanged(sender As Object, e As EventArgs) Handles WXP_VS_textbox.TextChanged
        Dim theme As String = ""

        If Path.GetExtension(WXP_VS_textbox.Text) = ".theme" Then
            theme = WXP_VS_textbox.Text

        ElseIf Path.GetExtension(WXP_VS_textbox.Text) = ".msstyles" Then
            theme = My.Application.appData & "\VisualStyles\Luna\custom.theme"
            File.WriteAllText(My.Application.appData & "\VisualStyles\Luna\custom.theme", String.Format("[VisualStyles]{1}Path={0}{1}ColorStyle=NormalColor{1}Size=NormalSize", WXP_VS_textbox.Text, vbCrLf))

        End If

        CP.WindowsXP.ThemeFile = WXP_VS_textbox.Text

        If File.Exists(WXP_VS_textbox.Text) AndAlso File.Exists(theme) And Not String.IsNullOrEmpty(theme) Then

            Dim vs As New VisualStyleFile(theme)

            WXP_VS_ColorsList.Items.Clear()

            Try
                For Each x In vs.ColorSchemes
                    WXP_VS_ColorsList.Items.Add(x.Name)
                Next
            Catch

            End Try

            If WXP_VS_ColorsList.Items.Count > 0 Then WXP_VS_ColorsList.SelectedIndex = 0

            If WXP_CustomTheme.Checked And PreviewConfig = WinVer.WXP Then Adjust_Preview(False)
        End If
    End Sub

    Private Sub XenonButton30_Click(sender As Object, e As EventArgs) Handles WXP_VS_Browse.Click
        If OpenFileDialog2.ShowDialog = DialogResult.OK Then
            WXP_VS_textbox.Text = OpenFileDialog2.FileName
        End If
    End Sub

    Private Sub XenonComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles WXP_VS_ColorsList.SelectedIndexChanged
        If _Shown AndAlso WXP_CustomTheme.Checked Then
            CP.WindowsXP.ColorScheme = WXP_VS_ColorsList.SelectedItem
            If PreviewConfig = WinVer.WXP Then Adjust_Preview(False)
        End If
    End Sub
#End Region

    Private Sub XenonButton4_Click(sender As Object, e As EventArgs) Handles apply_btn.Click
        Apply_Theme()
    End Sub

    Sub Apply_Theme()
        Cursor = Cursors.WaitCursor

        log_lbl.Visible = False
        log_lbl.Text = ""
        XenonButton8.Visible = False
        XenonButton14.Visible = False
        XenonButton22.Visible = False
        XenonButton25.Visible = False

        If My.[Settings].Log_ShowApplying Then
            TablessControl1.SelectedIndex = TablessControl1.TabCount - 1
            TablessControl1.Refresh()
        End If

        CP.Save(CP.CP_Type.Registry, "", If(My.[Settings].Log_ShowApplying, TreeView1, Nothing))

        CP_Original = New CP(CP_Type.Registry)

        Cursor = Cursors.Default

        If My.[Settings].AutoRestartExplorer Then
            RestartExplorer(If(My.[Settings].Log_ShowApplying, TreeView1, Nothing))
        Else
            If My.[Settings].Log_ShowApplying Then CP.AddNode(TreeView1, My.Lang.NoDefResExplorer, "warning")
        End If

        If My.[Settings].Log_ShowApplying Then CP.AddNode(TreeView1, String.Format("{0}: {1}", Now.ToLongTimeString, My.Lang.CP_AllDone), "info")

        If CP.MetricsFonts.Enabled And GetWindowsScreenScalingFactor() > 100 Then CP.AddNode(TreeView1, String.Format("{0}", My.Lang.CP_MetricsHighDPIAlert), "info")

        log_lbl.Visible = True
        XenonButton8.Visible = True
        XenonButton22.Visible = True
        XenonButton25.Visible = True

        If Not My.Saving_Exceptions.Count = 0 Then
            log_lbl.Text = My.Lang.CP_ErrorHappened
            XenonButton14.Visible = True
        Else
            If My.[Settings].Log_Countdown_Enabled Then
                log_lbl.Text = String.Format(My.Lang.CP_LogWillClose, My.[Settings].Log_Countdown)
                ellapsedSecs = 1
                Timer1.Enabled = True
                Timer1.Start()
            End If
        End If

    End Sub

    Private ellapsedSecs As Integer = 0

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        log_lbl.Text = String.Format(My.Lang.CP_LogWillClose, My.[Settings].Log_Countdown - ellapsedSecs)

        If ellapsedSecs + 1 <= My.[Settings].Log_Countdown Then
            ellapsedSecs += 1
        Else
            log_lbl.Text = ""
            Timer1.Enabled = False
            Timer1.Stop()
            SelectPreview()
        End If

    End Sub
    Private Sub XenonButton14_Click(sender As Object, e As EventArgs) Handles XenonButton14.Click
        log_lbl.Text = ""
        Timer1.Enabled = False
        Timer1.Stop()
        Saving_ex_list.ShowDialog()
    End Sub

    Private Sub XenonButton8_Click(sender As Object, e As EventArgs) Handles XenonButton8.Click
        log_lbl.Text = ""
        Timer1.Enabled = False
        Timer1.Stop()
        SelectPreview()
    End Sub

    Private Sub XenonButton22_Click(sender As Object, e As EventArgs) Handles XenonButton22.Click
        log_lbl.Text = ""
        Timer1.Enabled = False
        Timer1.Stop()

        If SaveFileDialog3.ShowDialog = DialogResult.OK Then
            Dim sb As New StringBuilder
            sb.Clear()

            For Each N As TreeNode In TreeView1.Nodes
                sb.AppendLine(String.Format("[{0}]{2} {1}{3}", N.ImageKey, N.Text, vbTab, vbCrLf))
            Next

            IO.File.WriteAllText(SaveFileDialog3.FileName, sb.ToString)

        End If
    End Sub

    Sub SelectPreview()
        If PreviewConfig = WinVer.W11 Then
            TablessControl1.SelectedIndex = 0
        ElseIf PreviewConfig = WinVer.W10 Then
            TablessControl1.SelectedIndex = 1
        ElseIf PreviewConfig = WinVer.W8 Then
            TablessControl1.SelectedIndex = 2
        ElseIf PreviewConfig = WinVer.W7 Then
            TablessControl1.SelectedIndex = 3
        ElseIf PreviewConfig = WinVer.WVista Then
            TablessControl1.SelectedIndex = 4
        ElseIf PreviewConfig = WinVer.WXP Then
            TablessControl1.SelectedIndex = 5
        Else
            TablessControl1.SelectedIndex = 0
        End If
    End Sub

    Private Sub apply_btn_MouseEnter(sender As Object, e As EventArgs) Handles apply_btn.MouseEnter
        If My.[Settings].AutoRestartExplorer Then
            status_lbl.Text = My.Lang.ThisWillRestartExplorer
            status_lbl.ForeColor = If(GetDarkMode(), Color.Gold, Color.Gold.Dark(0.1))
        End If
    End Sub

    Private Sub apply_btn_MouseLeave(sender As Object, e As EventArgs) Handles apply_btn.MouseLeave
        If My.[Settings].AutoRestartExplorer Then
            status_lbl.Text = ""
            status_lbl.ForeColor = If(GetDarkMode(), Color.White, Color.Black)
        End If
    End Sub

    Private Sub XenonButton19_MouseEnter(sender As Object, e As EventArgs) Handles XenonButton19.MouseEnter
        status_lbl.Text = My.Lang.ThisWillRestartExplorer
        status_lbl.ForeColor = If(GetDarkMode(), Color.Gold, Color.Gold.Dark(0.1))
    End Sub

    Private Sub XenonButton19_MouseLeave(sender As Object, e As EventArgs) Handles XenonButton19.MouseLeave
        status_lbl.Text = ""
        status_lbl.ForeColor = If(GetDarkMode(), Color.White, Color.Black)
    End Sub

#Region "Drag And Drop"
    Dim wpth_or_wpsf As Boolean = True
    Dim DragAccepted As Boolean
    Dim Dropped As Boolean


    Private Sub Me_DragEnter(ByVal sender As Object, ByVal e As DragEventArgs) Handles Me.DragEnter, previewContainer.DragEnter, tabs_preview.DragEnter, TablessControl1.DragEnter

        Dim files() As String = e.Data.GetData(DataFormats.FileDrop)
        Dropped = False

        If My.Computer.FileSystem.GetFileInfo(files(0)).Extension.ToLower = ".wpth" Then
            wpth_or_wpsf = True
            e.Effect = DragDropEffects.Copy

            If My.[Settings].DragAndDropPreview Then
                DragAccepted = True
                CP_BeforeDrag = CP.Clone
                DragPreviewer.Location = New Point(e.X + 15, e.Y + 15)
                DragPreviewer.File = files(0)
                DragPreviewer.Show()
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
        Dropped = False
        If DragAccepted Then
            If My.[Settings].DragAndDropPreview Then DragPreviewer.Close()
            CP = CP_BeforeDrag.Clone
        End If
    End Sub

    Private Sub MainFrm_DragOver(sender As Object, e As DragEventArgs) Handles Me.DragOver, previewContainer.DragOver, tabs_preview.DragOver, TablessControl1.DragOver
        If DragAccepted And My.[Settings].DragAndDropPreview Then DragPreviewer.Location = New Point(e.X + 15, e.Y + 15)
        Dropped = Me.Bounds.Contains(New Point(e.X, e.Y))
    End Sub

    Private Sub MainFrm_DragDrop(sender As Object, e As DragEventArgs) Handles Me.DragDrop, previewContainer.DragDrop, tabs_preview.DragDrop, TablessControl1.DragDrop
        If Dropped And DragAccepted Then
            Dim files() As String = e.Data.GetData(DataFormats.FileDrop)
            If wpth_or_wpsf Then
                If My.[Settings].DragAndDropPreview Then DragPreviewer.Close()

                If CP <> CP_Original Then
                    If My.[Settings].ShowSaveConfirmation Then
                        Select Case ComplexSave.ShowDialog
                            Case DialogResult.Yes

                                Dim r As String() = My.[Settings].ComplexSaveResult.Split(".")
                                Dim r1 As String = r(0)
                                Dim r2 As String = r(1)

                                Select Case r1
                                    Case 0              '' Save
                                        If IO.File.Exists(SaveFileDialog1.FileName) Then
                                            CP.Save(CP.CP_Type.File, SaveFileDialog1.FileName)
                                            CP_Original = CP.Clone
                                        Else
                                            If SaveFileDialog1.ShowDialog = DialogResult.OK Then
                                                CP.Save(CP.CP_Type.File, SaveFileDialog1.FileName)
                                                CP_Original = CP.Clone
                                            Else
                                                '''''''' If My.[Settings].DragPreview then ReleaseBlur()
                                                Exit Sub
                                            End If
                                        End If
                                    Case 1              '' Save As
                                        If SaveFileDialog1.ShowDialog = DialogResult.OK Then
                                            CP.Save(CP.CP_Type.File, SaveFileDialog1.FileName)
                                            CP_Original = CP.Clone
                                        Else
                                            '''''''' If My.[Settings].DragPreview then ReleaseBlur()
                                            Exit Sub
                                        End If
                                End Select

                                Select Case r2
                                    Case 1      '' Apply   ' Case 0= Don't Apply
                                        Apply_Theme()
                                End Select

                            Case DialogResult.No

                            Case DialogResult.Cancel
                                Exit Sub
                        End Select
                    End If
                End If

                CP = New CP(CP.CP_Type.File, files(0))
                tabs_preview.Visible = False
                Adjust_Preview(False)
                ApplyCPValues(CP)
                ApplyLivePreviewFromCP(CP)
                AdjustClassicPreview()
                tabs_preview.Visible = True
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
                CP.Save(CP.CP_Type.File, SaveFileDialog1.FileNames(0))
            End If
        Else
            CP.Save(CP.CP_Type.File, SaveFileDialog1.FileNames(0))
        End If
    End Sub

    Private Sub XenonButton2_Click(sender As Object, e As EventArgs) Handles XenonButton2.Click
        If OpenFileDialog1.ShowDialog = DialogResult.OK Then


            If CP <> CP_Original And My.[Settings].ShowSaveConfirmation Then
                Select Case ComplexSave.ShowDialog
                    Case DialogResult.Yes

                        Dim r As String() = My.[Settings].ComplexSaveResult.Split(".")
                        Dim r1 As String = r(0)
                        Dim r2 As String = r(1)

                        Select Case r1
                            Case 0              '' Save
                                If IO.File.Exists(SaveFileDialog1.FileName) Then
                                    CP.Save(CP.CP_Type.File, SaveFileDialog1.FileName)
                                    CP_Original = CP.Clone
                                Else
                                    If SaveFileDialog1.ShowDialog = DialogResult.OK Then
                                        CP.Save(CP.CP_Type.File, SaveFileDialog1.FileName)
                                        CP_Original = CP.Clone
                                    Else
                                        Exit Sub
                                    End If
                                End If
                            Case 1              '' Save As
                                If SaveFileDialog1.ShowDialog = DialogResult.OK Then
                                    CP.Save(CP.CP_Type.File, SaveFileDialog1.FileName)
                                    CP_Original = CP.Clone
                                Else
                                    Exit Sub
                                End If
                        End Select

                        Select Case r2
                            Case 1      '' Apply   ' Case 0= Don't Apply
                                Apply_Theme()
                        End Select

                    Case DialogResult.No

                    Case DialogResult.Cancel
                        Exit Sub
                End Select

            End If

            SaveFileDialog1.FileName = OpenFileDialog1.FileName
            CP = New CP(CP.CP_Type.File, OpenFileDialog1.FileName)
            CP_Original = CP.Clone

            ApplyCPValues(CP)
            ApplyLivePreviewFromCP(CP)
        End If
    End Sub

    Private Sub XenonButton9_Click(sender As Object, e As EventArgs) Handles XenonButton9.Click
        If SaveFileDialog1.ShowDialog = DialogResult.OK Then
            CP.Save(CP.CP_Type.File, SaveFileDialog1.FileNames(0))
        End If
    End Sub

    Private Sub XenonButton3_Click(sender As Object, e As EventArgs) Handles XenonButton3.Click
        If CP <> CP_Original Then
            If My.[Settings].ShowSaveConfirmation Then
                Select Case ComplexSave.ShowDialog
                    Case DialogResult.Yes

                        Dim r As String() = My.[Settings].ComplexSaveResult.Split(".")
                        Dim r1 As String = r(0)
                        Dim r2 As String = r(1)

                        Select Case r1
                            Case 0              '' Save
                                If IO.File.Exists(SaveFileDialog1.FileName) Then
                                    CP.Save(CP.CP_Type.File, SaveFileDialog1.FileName)
                                    CP_Original = CP.Clone
                                Else
                                    If SaveFileDialog1.ShowDialog = DialogResult.OK Then
                                        CP.Save(CP.CP_Type.File, SaveFileDialog1.FileName)
                                        CP_Original = CP.Clone
                                    Else
                                        Exit Sub
                                    End If
                                End If
                            Case 1              '' Save As
                                If SaveFileDialog1.ShowDialog = DialogResult.OK Then
                                    CP.Save(CP.CP_Type.File, SaveFileDialog1.FileName)
                                    CP_Original = CP.Clone
                                Else
                                    Exit Sub
                                End If
                        End Select

                        Select Case r2
                            Case 1      '' Apply   ' Case 0= Don't Apply
                                Apply_Theme()

                        End Select

                    Case DialogResult.No

                    Case DialogResult.Cancel
                        Exit Sub
                End Select
            End If
        End If

        CP = New CP(CP.CP_Type.Registry)
        CP_Original = CP.Clone
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
        XenonButton5.Image = My.Resources.Update
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
        If PreviewConfig = WinVer.W11 Or PreviewConfig = WinVer.W10 Then
            LogonUI.ShowDialog()
        ElseIf PreviewConfig = WinVer.W8 Or PreviewConfig = WinVer.W7 Then
            LogonUI7.ShowDialog()
        ElseIf PreviewConfig = WinVer.WXP Then
            LogonUIXP.ShowDialog()
        ElseIf PreviewConfig = WinVer.WVista Then
            MsgBox(My.Lang.VistaLogonNotSupported, MsgBoxStyle.Exclamation)
        Else
            LogonUI.ShowDialog()
        End If
    End Sub

    Private Sub XenonButton15_Click(sender As Object, e As EventArgs) Handles XenonButton15.Click
        Update_Wallpaper_Preview()
    End Sub

    Private Sub XenonButton13_Click(sender As Object, e As EventArgs) Handles XenonButton13.Click
        Me.Close()
    End Sub

    Private Sub XenonButton1_Click_1(sender As Object, e As EventArgs) Handles XenonButton1.Click
        If SaveFileDialog2.ShowDialog = DialogResult.OK Then
            tabs_preview.ToBitmap.Save(SaveFileDialog2.FileName)
        End If
    End Sub

    Private Sub XenonButton17_Click(sender As Object, e As EventArgs) Handles XenonButton17.Click
        CP = CP_Original.Clone
        ApplyCPValues(CP)
        ApplyLivePreviewFromCP(CP)
    End Sub

    Private Sub XenonButton18_Click(sender As Object, e As EventArgs) Handles XenonButton18.Click
        CP = CP_FirstTime.Clone
        ApplyCPValues(CP)
        ApplyLivePreviewFromCP(CP)
    End Sub

    Private Sub XenonButton19_Click(sender As Object, e As EventArgs) Handles XenonButton19.Click
        RestartExplorer()
    End Sub

    Private Sub XenonButton20_Click(sender As Object, e As EventArgs) Handles XenonButton20.Click
        If CP <> CP_Original Then
            If My.[Settings].ShowSaveConfirmation Then
                Select Case ComplexSave.ShowDialog
                    Case DialogResult.Yes

                        Dim r As String() = My.[Settings].ComplexSaveResult.Split(".")
                        Dim r1 As String = r(0)
                        Dim r2 As String = r(1)

                        Select Case r1
                            Case 0              '' Save
                                If IO.File.Exists(SaveFileDialog1.FileName) Then
                                    CP.Save(CP.CP_Type.File, SaveFileDialog1.FileName)
                                    CP_Original = CP.Clone
                                Else
                                    If SaveFileDialog1.ShowDialog = DialogResult.OK Then
                                        CP.Save(CP.CP_Type.File, SaveFileDialog1.FileName)
                                        CP_Original = CP.Clone
                                    Else
                                        Exit Sub
                                    End If
                                End If
                            Case 1              '' Save As
                                If SaveFileDialog1.ShowDialog = DialogResult.OK Then
                                    CP.Save(CP.CP_Type.File, SaveFileDialog1.FileName)
                                    CP_Original = CP.Clone
                                Else
                                    Exit Sub
                                End If
                        End Select

                        Select Case r2
                            Case 1      '' Apply   ' Case 0= Don't Apply
                                Apply_Theme()
                        End Select

                    Case DialogResult.No

                    Case DialogResult.Cancel
                        Exit Sub
                End Select
            End If
        End If

        Dim Def As New CP_Defaults

        If My.W11 Then
            CP = Def.Default_Windows11.Clone
        ElseIf My.W10 Then
            CP = Def.Default_Windows10.Clone
        ElseIf My.W8 Then
            CP = Def.Default_Windows8.Clone
        ElseIf My.W7 Then
            CP = Def.Default_Windows7.Clone
        ElseIf My.WVista Then
            CP = Def.Default_WindowsVista.Clone
        ElseIf My.WXP Then
            CP = Def.Default_WindowsXP.Clone
        Else
            CP = Def.Default_Windows11.Clone
        End If

        Def.Dispose()

        SaveFileDialog1.FileName = Nothing

        ApplyCPValues(CP)
        ApplyLivePreviewFromCP(CP)
    End Sub

    Private Sub XenonButton21_Click(sender As Object, e As EventArgs) Handles XenonButton21.Click
        CursorsStudio.ShowDialog()
    End Sub

    Private Sub XenonButton23_Click(sender As Object, e As EventArgs) Handles XenonButton23.Click
        If XenonButton23.Text.ToLower = My.Lang.Hide.ToLower Then
            tabs_preview.Visible = False
            XenonButton23.Text = My.Lang.Show
        Else
            tabs_preview.Visible = True
            XenonButton23.Text = My.Lang.Hide
        End If
    End Sub

    Private Sub XenonButton24_Click(sender As Object, e As EventArgs) Handles XenonButton24.Click
        TerminalsDashboard.ShowDialog()
    End Sub

    Private Sub MainFrm_ResizeBegin(sender As Object, e As EventArgs) Handles Me.ResizeBegin
        SuspendLayout()
        'previewContainer.Visible = False
        'TablessControl1.Visible = False
    End Sub

    Private Sub MainFrm_ResizeEnd(sender As Object, e As EventArgs) Handles Me.ResizeEnd
        ResumeLayout()
        'previewContainer.Visible = True
        'TablessControl1.Visible = True
    End Sub

    Private Sub XenonButton27_Click(sender As Object, e As EventArgs) Handles XenonButton27.Click
        Metrics_Fonts.ShowDialog()
    End Sub

    Sub Select_Preview_Version()

        My.Animator.HideSync(TablessControl1)

        Adjust_Preview()
        ApplyLivePreviewFromCP(CP)
        ApplyDefaultCPValues()

        If PreviewConfig = WinVer.W11 Then
            XenonButton20.Image = My.Resources.add_win11
        ElseIf PreviewConfig = WinVer.W10 Then
            XenonButton20.Image = My.Resources.add_win10
        ElseIf PreviewConfig = WinVer.W8 Then
            XenonButton20.Image = My.Resources.add_win8
        ElseIf PreviewConfig = WinVer.W7 Then
            XenonButton20.Image = My.Resources.add_win7
        ElseIf PreviewConfig = WinVer.WVista Then
            XenonButton20.Image = My.Resources.add_winvista
        ElseIf PreviewConfig = WinVer.WXP Then
            XenonButton20.Image = My.Resources.add_winxp
        Else
            XenonButton20.Image = My.Resources.add_win11
        End If

        If PreviewConfig = WinVer.W11 Then
            TablessControl1.SelectedIndex = 0
        ElseIf PreviewConfig = WinVer.W10 Then
            TablessControl1.SelectedIndex = 1
        ElseIf PreviewConfig = WinVer.W8 Then
            TablessControl1.SelectedIndex = 2
        ElseIf PreviewConfig = WinVer.W7 Then
            TablessControl1.SelectedIndex = 3
        ElseIf PreviewConfig = WinVer.WVista Then
            TablessControl1.SelectedIndex = 4
        ElseIf PreviewConfig = WinVer.WXP Then
            TablessControl1.SelectedIndex = 5
        Else
            TablessControl1.SelectedIndex = 0
        End If

        My.Animator.Show(TablessControl1)

    End Sub

    Private Sub XenonButton26_Click(sender As Object, e As EventArgs) Handles XenonButton26.Click
        If PreviewConfig = WinVer.W11 Then
            WallpaperToner.WT = CP.WallpaperTone_W11
        ElseIf PreviewConfig = WinVer.W10 Then
            WallpaperToner.WT = CP.WallpaperTone_W10
        ElseIf PreviewConfig = WinVer.W8 Then
            WallpaperToner.WT = CP.WallpaperTone_W8
        ElseIf PreviewConfig = WinVer.W7 Then
            WallpaperToner.WT = CP.WallpaperTone_W7
        ElseIf PreviewConfig = WinVer.WVista Then
            WallpaperToner.WT = CP.WallpaperTone_WVista
        ElseIf PreviewConfig = WinVer.WXP Then
            WallpaperToner.WT = CP.WallpaperTone_WXP
        Else
            WallpaperToner.WT = CP.WallpaperTone_W11
        End If

        WallpaperToner.ShowDialog()
    End Sub


    Private Sub Select_W11_CheckedChanged(sender As Object) Handles Select_W11.CheckedChanged
        If _Shown And Select_W11.Checked Then
            PreviewConfig = WinVer.W11
            Select_Preview_Version()
        End If
    End Sub

    Private Sub Select_W10_CheckedChanged(sender As Object) Handles Select_W10.CheckedChanged
        If _Shown And Select_W10.Checked Then
            PreviewConfig = WinVer.W10
            Select_Preview_Version()
        End If
    End Sub

    Private Sub Select_W8_CheckedChanged(sender As Object) Handles Select_W8.CheckedChanged
        If _Shown And Select_W8.Checked Then
            PreviewConfig = WinVer.W8
            Select_Preview_Version()
        End If
    End Sub

    Private Sub Select_W7_CheckedChanged(sender As Object) Handles Select_W7.CheckedChanged
        If _Shown And Select_W7.Checked Then
            PreviewConfig = WinVer.W7
            Select_Preview_Version()
        End If
    End Sub

    Private Sub Select_WVista_CheckedChanged(sender As Object) Handles Select_WVista.CheckedChanged
        If _Shown And Select_WVista.Checked Then
            PreviewConfig = WinVer.WVista
            Select_Preview_Version()
        End If
    End Sub

    Private Sub XenonButton29_Click(sender As Object, e As EventArgs) Handles XenonButton29.Click
        WinEffecter.ShowDialog()
    End Sub

    Private Sub XenonButton32_Click(sender As Object, e As EventArgs) Handles XenonButton32.Click
        If PreviewConfig <> WinVer.WXP AndAlso PreviewConfig <> WinVer.WVista Then
            AltTabEditor.ShowDialog()
        Else
            If PreviewConfig = WinVer.WXP Then MsgBox(String.Format(My.Lang.AltTab_Unsupported, My.Lang.OS_WinXP), MsgBoxStyle.Exclamation)
            If PreviewConfig = WinVer.WVista Then MsgBox(String.Format(My.Lang.AltTab_Unsupported, My.Lang.OS_WinVista), MsgBoxStyle.Exclamation)
        End If

    End Sub

    Private Sub Select_WXP_CheckedChanged(sender As Object) Handles Select_WXP.CheckedChanged
        If _Shown And Select_WXP.Checked Then
            PreviewConfig = WinVer.WXP
            Select_Preview_Version()
        End If
    End Sub

    Private Sub XenonButton25_Click(sender As Object, e As EventArgs) Handles XenonButton25.Click
        log_lbl.Text = ""
        Timer1.Enabled = False
        Timer1.Stop()
    End Sub

    Private Sub XenonButton28_Click(sender As Object, e As EventArgs) Handles XenonButton28.Click

        If MsgBox(My.Lang.LogoffQuestion, MsgBoxStyle.Question + MsgBoxStyle.YesNo, My.Lang.LogoffAlert1, "", "", "", "", My.Lang.LogoffAlert2, Ookii.Dialogs.WinForms.TaskDialogIcon.Information) = MsgBoxResult.Yes Then
            Shell("logoff", AppWinStyle.Hide)
        End If

    End Sub

    Private Sub XenonButton28_MouseEnter(sender As Object, e As EventArgs) Handles XenonButton28.MouseEnter
        status_lbl.Text = My.Lang.LogoffNotice
        status_lbl.ForeColor = If(GetDarkMode(), Color.Gold, Color.Gold.Dark(0.1))
    End Sub

    Private Sub XenonButton28_MouseLeave(sender As Object, e As EventArgs) Handles XenonButton28.MouseLeave
        status_lbl.Text = ""
        status_lbl.ForeColor = If(GetDarkMode(), Color.White, Color.Black)
    End Sub
End Class


