Imports System.Drawing.Text
Imports WinPaletter.CP
Imports WinPaletter.XenonCore

Public Class PreviewHelpers
    Private Shared ReadOnly Steps As Integer = 15
    Private Shared ReadOnly Delay As Integer = 1

    Enum WindowStyle
        W11
        W10
        W81
        W7
        WVista
        WXP
    End Enum

    Public Shared Sub ApplyWin10xLegends([CP] As CP, [Style] As WindowStyle,
                                   lbl1 As Label, lbl2 As Label, lbl3 As Label, lbl4 As Label, lbl5 As Label, lbl6 As Label, lbl7 As Label, lbl8 As Label, lbl9 As Label,
                                   pic1 As PictureBox, pic2 As PictureBox, pic3 As PictureBox, pic4 As PictureBox, pic5 As PictureBox, pic6 As PictureBox, pic7 As PictureBox, pic8 As PictureBox, pic9 As PictureBox)

        If ExplorerPatcher.IsAllowed Then My.EP = New ExplorerPatcher

        Select Case [Style]
            Case WindowStyle.W11
#Region "Win11"
                lbl6.Text = My.Lang.CP_11_SomePressedButtons
                lbl7.Text = String.Format(My.Lang.CP_UWPBackground, My.Lang.OS_Win11)
                lbl8.Text = My.Lang.CP_Undefined
                lbl9.Text = My.Lang.CP_Undefined
                pic5.Image = My.Resources.Mini_Settings_Icons
                pic6.Image = My.Resources.Mini_PressedButton
                pic7.Image = My.Resources.Mini_UWPDlg
                pic8.Image = My.Resources.Mini_Undefined
                pic9.Image = My.Resources.Mini_Undefined

                Select Case Not [CP].Windows11.WinMode_Light
                    Case True   ''''''''''DarkMode
                        lbl1.Text = My.Lang.CP_11_StartMenu_Taskbar_AC
                        lbl2.Text = My.Lang.CP_11_ACHover_Links
                        lbl3.Text = My.Lang.CP_11_Lines_Toggles_Buttons
                        lbl4.Text = My.Lang.CP_11_OverflowTray
                        lbl5.Text = My.Lang.CP_11_Settings

                        pic1.Image = My.Resources.Mini_StartMenu_Taskbar_AC
                        pic2.Image = My.Resources.Mini_ACHover_Links
                        pic3.Image = My.Resources.Mini_Lines_Toggles_Buttons
                        pic4.Image = My.Resources.Mini_Overflow
                    Case False   ''''''''''Light
                        lbl1.Text = My.Lang.CP_11_Taskbar_ACHover_Links
                        lbl2.Text = My.Lang.CP_11_StartMenu_AC
                        lbl3.Text = My.Lang.CP_11_UnreadBadge
                        lbl4.Text = My.Lang.CP_11_Lines_Toggles_Buttons_Overflow
                        lbl5.Text = My.Lang.CP_11_SettingsAndTaskbarAppUnderline

                        pic1.Image = My.Resources.Mini_Taskbar
                        pic2.Image = My.Resources.Mini_StartMenu_Taskbar_AC
                        pic3.Image = My.Resources.Mini_Badge
                        pic4.Image = My.Resources.Mini_Lines_Toggles_Buttons
                End Select

                If ExplorerPatcher.IsAllowed Then
                    Select Case Not [CP].Windows11.WinMode_Light
                        Case True ''''''''''DarkMode

                            If My.EP.UseTaskbar10 Then
                                lbl5.Text = My.Lang.CP_10_Settings_Links_SomeBtns

                                If My.EP.UseStart10 Then
                                    lbl1.Text = My.Lang.CP_10_Taskbar
                                    pic1.Image = My.Resources.Mini_Taskbar
                                Else
                                    lbl1.Text = My.Lang.CP_11_StartMenu_Taskbar_AC
                                    pic1.Image = My.Resources.Mini_StartMenu_Taskbar_AC
                                End If

                                lbl3.Text = My.Lang.CP_EP_ACButton_TaskbarAppLine
                                lbl6.Text = My.Lang.CP_10_StartMenuIconHover

                                pic3.Image = My.Resources.Mini_AC
                                pic5.Image = My.Resources.Mini_Settings_Icons
                                pic6.Image = My.Resources.Native11
                            End If

                            If My.EP.UseStart10 Then
                                lbl4.Text = My.Lang.CP_EP_StartMenu_OverflowMenus
                                pic4.Image = My.Resources.Mini_StartMenu
                            End If

                        Case False ''''''''''Light

                            If My.EP.UseTaskbar10 Then
                                lbl3.Text = My.Lang.CP_EP_Taskbar_AppUnderline
                                lbl5.Text = My.Lang.CP_10_Settings_Links_SomeBtns
                                lbl6.Text = My.Lang.CP_10_StartMenuIconHover

                                pic3.Image = My.Resources.Mini_TaskbarApp
                                pic5.Image = My.Resources.Mini_Settings_Icons
                                pic6.Image = My.Resources.Native11
                            End If

                            If My.EP.UseStart10 Then
                                lbl2.Text = My.Lang.CP_EP_ActionCenterBackground
                                lbl4.Text = My.Lang.CP_EP_StartMenu_ActionCenterButtons
                                pic2.Image = My.Resources.Mini_AC
                                pic4.Image = My.Resources.Mini_StartMenu_Taskbar_AC
                            End If

                    End Select
                End If

#End Region
            Case WindowStyle.W10
#Region "Win10"
                lbl9.Text = My.Lang.CP_Undefined

                Select Case Not [CP].Windows10.WinMode_Light
                    Case True ''''''''''DarkMode
                        lbl2.Text = My.Lang.CP_10_ACLinks
                        lbl3.Text = My.Lang.CP_10_TaskbarAppUnderline
                        lbl5.Text = My.Lang.CP_10_Settings_Links_SomeBtns
                        lbl6.Text = My.Lang.CP_10_StartMenuIconHover
                        lbl7.Text = String.Format(My.Lang.CP_UWPBackground, My.Lang.OS_Win10)

                        pic2.Image = My.Resources.Mini_ACHover_Links
                        pic3.Image = My.Resources.Mini_TaskbarApp
                        pic5.Image = My.Resources.Mini_Settings_Icons
                        pic6.Image = My.Resources.Native10
                        pic7.Image = My.Resources.Mini_UWPDlg

                        If [CP].Windows10.Transparency Then
                            lbl1.Text = My.Lang.CP_10_Hamburger
                            lbl4.Text = My.Lang.CP_10_StartMenu_AC
                            lbl8.Text = My.Lang.CP_10_Taskbar_StartContextMenu

                            pic1.Image = My.Resources.Mini_Hamburger
                            pic4.Image = My.Resources.Mini_StartMenu_Taskbar_AC
                            pic8.Image = My.Resources.Mini_Taskbar

                            If [CP].Windows10.ApplyAccentOnTaskbar <> CP.Structures.Windows10x.AccentTaskbarLevels.None Then
                                lbl5.Text = My.Lang.CP_10_Settings_Links_Taskbar_SomeBtns
                            End If

                        Else
                            lbl1.Text = My.Lang.CP_10_Taskbar
                            pic1.Image = My.Resources.Mini_Taskbar
                            pic4.Image = My.Resources.Mini_StartMenu_Taskbar_AC

                            If [CP].Windows10.ApplyAccentOnTaskbar <> CP.Structures.Windows10x.AccentTaskbarLevels.None Then
                                lbl4.Text = My.Lang.CP_10_StartMenu_AC_TaskbarActiveApp
                            Else
                                lbl4.Text = My.Lang.CP_10_StartMenu_AC
                            End If

                            lbl8.Text = My.Lang.CP_10_StartContextMenu
                            pic8.Image = My.Resources.Mini_StartContextMenu

                        End If

                    Case False ''''''''''Light
                        If [CP].Windows10.Transparency Then
                            lbl1.Text = My.Lang.CP_10_Hamburger
                            lbl4.Text = My.Lang.CP_10_StartMenu_AC
                            lbl6.Text = My.Lang.CP_10_StartMenuIconHover
                            lbl7.Text = String.Format(My.Lang.CP_UWPBackground, My.Lang.OS_Win10)

                            pic1.Image = My.Resources.Mini_Hamburger
                            pic4.Image = My.Resources.Mini_StartMenu_Taskbar_AC
                            pic5.Image = My.Resources.Mini_Settings_Icons
                            pic6.Image = My.Resources.Native10
                            pic7.Image = My.Resources.Mini_UWPDlg
                            pic8.Image = My.Resources.Mini_Taskbar

                            If [CP].Windows10.ApplyAccentOnTaskbar = CP.Structures.Windows10x.AccentTaskbarLevels.None Then
                                lbl2.Text = My.Lang.CP_Undefined
                                lbl3.Text = My.Lang.CP_Undefined
                                lbl5.Text = My.Lang.CP_10_Settings_Links_TaskbarUndeline_SomeBtns
                                lbl8.Text = My.Lang.CP_10_Taskbar_ACLinks_StartContextMenu

                                pic2.Image = My.Resources.Mini_Undefined
                                pic3.Image = My.Resources.Mini_Undefined

                            ElseIf [CP].Windows10.ApplyAccentOnTaskbar = CP.Structures.Windows10x.AccentTaskbarLevels.Taskbar Then
                                lbl2.Text = My.Lang.CP_Undefined
                                lbl3.Text = My.Lang.CP_10_TaskbarAppUnderline
                                lbl5.Text = My.Lang.CP_10_Settings_Links_SomeBtns
                                lbl8.Text = My.Lang.CP_10_Taskbar_ACLinks_StartContextMenu

                                pic2.Image = My.Resources.Mini_Undefined
                                pic3.Image = My.Resources.Mini_TaskbarApp

                            Else
                                lbl2.Text = My.Lang.CP_10_ACLinks
                                lbl3.Text = My.Lang.CP_10_TaskbarAppUnderline
                                lbl5.Text = My.Lang.CP_10_Settings_Links_SomeBtns
                                lbl8.Text = My.Lang.CP_10_Taskbar_StartContextMenu

                                pic2.Image = My.Resources.Mini_ACHover_Links
                                pic3.Image = My.Resources.Mini_TaskbarApp

                            End If
                        Else
                            lbl1.Text = My.Lang.CP_10_Taskbar
                            lbl6.Text = My.Lang.CP_10_StartMenuIconHover
                            lbl7.Text = String.Format(My.Lang.CP_UWPBackground, My.Lang.OS_Win10)

                            pic1.Image = My.Resources.Mini_Taskbar
                            pic6.Image = My.Resources.Native10
                            pic7.Image = My.Resources.Mini_UWPDlg

                            If [CP].Windows10.ApplyAccentOnTaskbar = CP.Structures.Windows10x.AccentTaskbarLevels.None Then
                                lbl2.Text = My.Lang.CP_Undefined
                                lbl3.Text = My.Lang.CP_Undefined
                                lbl4.Text = My.Lang.CP_10_StartMenu_AC
                                lbl5.Text = My.Lang.CP_10_Settings_Links_TaskbarUndeline_SomeBtns
                                lbl8.Text = My.Lang.CP_10_ACLinks_StartContextMenu

                                pic2.Image = My.Resources.Mini_Undefined
                                pic3.Image = My.Resources.Mini_Undefined
                                pic4.Image = My.Resources.Mini_StartMenu_Taskbar_AC
                                pic5.Image = My.Resources.Mini_Settings_Icons
                                pic8.Image = My.Resources.Mini_ACHover_Links

                            ElseIf [CP].Windows10.ApplyAccentOnTaskbar = CP.Structures.Windows10x.AccentTaskbarLevels.Taskbar Then
                                lbl2.Text = My.Lang.CP_Undefined
                                lbl3.Text = My.Lang.CP_10_TaskbarAppUnderline
                                lbl4.Text = My.Lang.CP_10_TaskbarFocusedApp_StartButtonHover
                                lbl5.Text = My.Lang.CP_10_Settings_Links_SomeBtns
                                lbl8.Text = My.Lang.CP_10_ACLinks_StartContextMenu

                                pic2.Image = My.Resources.Mini_Undefined
                                pic3.Image = My.Resources.Mini_TaskbarApp
                                pic4.Image = My.Resources.Mini_TaskbarActiveIcon
                                pic5.Image = My.Resources.Mini_Settings_Icons
                                pic8.Image = My.Resources.Mini_ACHover_Links

                            Else
                                lbl2.Text = My.Lang.CP_10_ACLinks
                                lbl3.Text = My.Lang.CP_10_TaskbarAppUnderline
                                lbl4.Text = My.Lang.CP_10_StartMenu_AC_TaskbarActiveApp
                                lbl5.Text = My.Lang.CP_10_Settings_Links_SomeBtns
                                lbl8.Text = My.Lang.CP_10_StartContextMenu

                                pic2.Image = My.Resources.Mini_ACHover_Links
                                pic3.Image = My.Resources.Mini_TaskbarApp
                                pic4.Image = My.Resources.Mini_StartMenu_Taskbar_AC
                                pic5.Image = My.Resources.Mini_Settings_Icons
                                pic8.Image = My.Resources.Mini_StartContextMenu
                            End If
                        End If
                End Select
#End Region
        End Select

    End Sub

    Public Shared Sub ApplyWinElementsColors([CP] As CP, [Style] As WindowStyle, AnimateColorChange As Boolean, Taskbar As UI.Simulation.WinElement, Start As UI.Simulation.WinElement, ActionCenter As UI.Simulation.WinElement,
                                      setting_icon_preview As UI.WP.LabelAlt, settings_label As UI.WP.LabelAlt, Link_preview As UI.WP.LabelAlt)

        If ExplorerPatcher.IsAllowed Then My.EP = New ExplorerPatcher

        My.RenderingHint = If(CP.MetricsFonts.Fonts_SingleBitPP, TextRenderingHint.SingleBitPerPixelGridFit, TextRenderingHint.ClearTypeGridFit)

        Taskbar.SuspendRefresh = True
        Start.SuspendRefresh = True
        ActionCenter.SuspendRefresh = True

        Select Case [Style]
            Case WindowStyle.W11
#Region "Win11"
                Dim TB_Alpha, S_Alpha, AC_Alpha As Byte
                Dim TB_Blur As Byte
                Dim TB_Color, S_Color, AC_Color As Color
                Dim TB_UL_Color As Color
                Dim Settings_Label_Color, Link_preview_Color As Color
                Dim AC_Normal, AC_Hover, AC_Pressed As Color

                Start.DarkMode = Not [CP].Windows11.WinMode_Light
                Taskbar.DarkMode = Not [CP].Windows11.WinMode_Light
                ActionCenter.DarkMode = Not [CP].Windows11.WinMode_Light
                Taskbar.Transparency = [CP].Windows11.Transparency
                Start.Transparency = [CP].Windows11.Transparency
                ActionCenter.Transparency = [CP].Windows11.Transparency

                Select Case Not [CP].Windows11.WinMode_Light
                    Case True   ''''''''''DarkMode
                        AC_Alpha = 90

                        If ExplorerPatcher.IsAllowed Then
                            If My.EP.UseStart10 Then
                                S_Alpha = 185
                            Else
                                S_Alpha = 90
                            End If

                            If My.EP.UseTaskbar10 Then
                                TB_Alpha = 185
                                TB_Blur = 8
                            Else
                                TB_Alpha = 105
                                TB_Blur = 8
                            End If
                        Else
                            TB_Alpha = 105
                            TB_Blur = 8
                            S_Alpha = 90
                        End If

                        Select Case [CP].Windows11.ApplyAccentOnTaskbar
                            Case CP.Structures.Windows10x.AccentTaskbarLevels.None
                                TB_Color = Color.FromArgb(28, 28, 28)
                                S_Color = Color.FromArgb(28, 28, 28)
                                AC_Color = Color.FromArgb(28, 28, 28)

                            Case CP.Structures.Windows10x.AccentTaskbarLevels.Taskbar
                                TB_Color = Color.FromArgb(Taskbar.Background.A, [CP].Windows11.Color_Index5)
                                S_Color = Color.FromArgb(28, 28, 28)
                                AC_Color = Color.FromArgb(28, 28, 28)

                            Case CP.Structures.Windows10x.AccentTaskbarLevels.Taskbar_Start_AC
                                TB_Color = Color.FromArgb(Taskbar.Background.A, [CP].Windows11.Color_Index5)

                                If ExplorerPatcher.IsAllowed And My.EP.UseStart10 Then
                                    S_Color = Color.FromArgb(Start.Background.A, [CP].Windows11.Color_Index4)
                                Else
                                    S_Color = Color.FromArgb(Start.Background.A, [CP].Windows11.Color_Index5)
                                End If

                                AC_Color = Color.FromArgb(ActionCenter.Background.A, [CP].Windows11.Color_Index5)

                        End Select

                        AC_Normal = [CP].Windows11.Color_Index1
                        AC_Hover = [CP].Windows11.Color_Index0
                        AC_Pressed = [CP].Windows11.Color_Index2
                        TB_UL_Color = [CP].Windows11.Color_Index1
                        Settings_Label_Color = [CP].Windows11.Color_Index3
                        Link_preview_Color = [CP].Windows11.Color_Index0

                    Case False   ''''''''''Light
                        AC_Alpha = 180

                        If ExplorerPatcher.IsAllowed Then
                            If My.EP.UseStart10 Then
                                S_Alpha = 210
                            Else
                                S_Alpha = 180
                            End If

                            If My.EP.UseTaskbar10 Then
                                TB_Alpha = 210
                                TB_Blur = 8
                            Else
                                TB_Alpha = 180
                                TB_Blur = 8
                            End If
                        Else
                            TB_Blur = 8
                            TB_Alpha = 180
                            S_Alpha = 180
                        End If

                        Select Case [CP].Windows11.ApplyAccentOnTaskbar
                            Case CP.Structures.Windows10x.AccentTaskbarLevels.None
                                TB_Color = Color.FromArgb(255, 255, 255)
                                S_Color = Color.FromArgb(255, 255, 255)
                                AC_Color = Color.FromArgb(255, 255, 255)

                            Case CP.Structures.Windows10x.AccentTaskbarLevels.Taskbar
                                TB_Color = Color.FromArgb(Taskbar.Background.A, [CP].Windows11.Color_Index5)
                                S_Color = Color.FromArgb(255, 255, 255)
                                AC_Color = Color.FromArgb(255, 255, 255)

                            Case CP.Structures.Windows10x.AccentTaskbarLevels.Taskbar_Start_AC
                                TB_Color = Color.FromArgb(Taskbar.Background.A, [CP].Windows11.Color_Index5)

                                If ExplorerPatcher.IsAllowed And My.EP.UseStart10 Then
                                    S_Color = Color.FromArgb(Start.Background.A, [CP].Windows11.Color_Index4)
                                Else
                                    S_Color = Color.FromArgb(Start.Background.A, [CP].Windows11.Color_Index0)
                                End If

                                AC_Color = Color.FromArgb(ActionCenter.Background.A, [CP].Windows11.Color_Index0)

                        End Select

                        AC_Normal = [CP].Windows11.Color_Index4
                        AC_Hover = [CP].Windows11.Color_Index5
                        AC_Pressed = [CP].Windows11.Color_Index2

                        If ExplorerPatcher.IsAllowed And My.EP.UseTaskbar10 Then
                            TB_UL_Color = [CP].Windows11.Color_Index1
                        Else
                            TB_UL_Color = [CP].Windows11.Color_Index3
                        End If

                        Settings_Label_Color = [CP].Windows11.Color_Index3
                        Link_preview_Color = [CP].Windows11.Color_Index5
                End Select

                ActionCenter.BackColorAlpha = AC_Alpha
                Start.BackColorAlpha = S_Alpha
                Taskbar.BackColorAlpha = TB_Alpha
                Taskbar.BlurPower = TB_Blur

                If AnimateColorChange Then
                    Visual.FadeColor(Taskbar, "Background", Taskbar.Background, TB_Color, Steps, Delay)
                    Visual.FadeColor(Start, "Background", Start.Background, S_Color, Steps, Delay)
                    Visual.FadeColor(ActionCenter, "Background", ActionCenter.Background, AC_Color, Steps, Delay)
                    Visual.FadeColor(ActionCenter, "ActionCenterButton_Normal", ActionCenter.ActionCenterButton_Normal, AC_Normal, Steps, Delay)
                    Visual.FadeColor(ActionCenter, "ActionCenterButton_Hover", ActionCenter.ActionCenterButton_Hover, AC_Hover, Steps, Delay)
                    Visual.FadeColor(ActionCenter, "ActionCenterButton_Pressed", ActionCenter.ActionCenterButton_Pressed, AC_Pressed, Steps, Delay)
                    Visual.FadeColor(Taskbar, "AppUnderline", Taskbar.AppUnderline, TB_UL_Color, Steps, Delay)
                    Visual.FadeColor(setting_icon_preview, "ForeColor", setting_icon_preview.ForeColor, Settings_Label_Color, Steps, Delay)
                    Visual.FadeColor(Link_preview, "ForeColor", Link_preview.ForeColor, Link_preview_Color, Steps, Delay)
                    Visual.FadeColor(settings_label, "ForeColor", settings_label.ForeColor, If([CP].Windows11.AppMode_Light, Color.Black, Color.White), Steps, Delay)
                Else
                    Taskbar.Background = TB_Color
                    Start.Background = S_Color
                    ActionCenter.Background = AC_Color
                    ActionCenter.ActionCenterButton_Normal = AC_Normal
                    ActionCenter.ActionCenterButton_Hover = AC_Hover
                    ActionCenter.ActionCenterButton_Pressed = AC_Pressed
                    Taskbar.AppUnderline = TB_UL_Color
                    setting_icon_preview.ForeColor = Settings_Label_Color
                    Link_preview.ForeColor = Link_preview_Color
                    settings_label.ForeColor = If([CP].Windows11.AppMode_Light, Color.Black, Color.White)
                End If
#End Region
            Case WindowStyle.W10
#Region "Win10"
                Dim TB_Alpha, S_Alpha, AC_Alpha As Byte
                Dim TB_Blur As Byte
                Dim TB_Color, S_Color, AC_Color As Color
                Dim TB_StartBtnColor As Color

                Dim TB_UL_Color As Color
                Dim TB_AppBack_Color As Color
                Dim AC_LinkColor As Color

                Dim Settings_Label_Color, Link_preview_Color As Color
                Dim AC_Normal As Color

                Start.DarkMode = Not [CP].Windows10.WinMode_Light
                Taskbar.DarkMode = Not [CP].Windows10.WinMode_Light
                ActionCenter.DarkMode = Not [CP].Windows10.WinMode_Light
                Taskbar.Transparency = [CP].Windows10.Transparency
                Start.Transparency = [CP].Windows10.Transparency AndAlso [CP].Windows10.TB_Blur
                ActionCenter.Transparency = [CP].Windows10.Transparency AndAlso [CP].Windows10.TB_Blur

                If Not [CP].Windows10.TB_Blur Then
                    TB_Blur = 0
                Else
                    TB_Blur = If(Not [CP].Windows10.IncreaseTBTransparency, 8, 6)
                End If

                If [CP].Windows10.Transparency Then
                    If Not [CP].Windows10.WinMode_Light Then
                        TB_Alpha = If(Not CP.Windows10.IncreaseTBTransparency, 150, 75)
                        S_Alpha = 150
                        AC_Alpha = 150
                    Else
                        TB_Alpha = If(Not CP.Windows10.IncreaseTBTransparency, 200, 125)
                        S_Alpha = 200
                        AC_Alpha = 200
                    End If
                Else
                    TB_Alpha = 255
                    S_Alpha = 255
                    AC_Alpha = 255
                End If

                Select Case Not [CP].Windows10.WinMode_Light
                    Case True

                        If [CP].Windows10.Transparency Then
                            Select Case [CP].Windows10.ApplyAccentOnTaskbar
                                Case CP.Structures.Windows10x.AccentTaskbarLevels.None
                                    TB_Color = Color.FromArgb(16, 16, 16)
                                    TB_StartBtnColor = Color.FromArgb(150, 150, 150, 150)
                                    S_Color = Color.FromArgb(31, 31, 31)
                                    AC_Color = Color.FromArgb(31, 31, 31)

                                    TB_AppBack_Color = Color.FromArgb(150, 150, 150, 150)
                                    AC_LinkColor = [CP].Windows10.Color_Index0
                                    TB_UL_Color = [CP].Windows10.Color_Index1
                                    Settings_Label_Color = [CP].Windows10.Color_Index3
                                    Link_preview_Color = [CP].Windows10.Color_Index3
                                    AC_Normal = [CP].Windows10.Color_Index3

                                Case CP.Structures.Windows10x.AccentTaskbarLevels.Taskbar
                                    TB_Color = [CP].Windows10.Color_Index6
                                    TB_StartBtnColor = Color.FromArgb(0, 0, 0, 0)
                                    S_Color = Color.FromArgb(31, 31, 31)
                                    AC_Color = Color.FromArgb(31, 31, 31)

                                    TB_AppBack_Color = Color.FromArgb(150, [CP].Windows10.Color_Index3)
                                    AC_LinkColor = [CP].Windows10.Color_Index0
                                    TB_UL_Color = [CP].Windows10.Color_Index1
                                    Settings_Label_Color = [CP].Windows10.Color_Index3
                                    Link_preview_Color = [CP].Windows10.Color_Index3
                                    AC_Normal = [CP].Windows10.Color_Index3

                                Case CP.Structures.Windows10x.AccentTaskbarLevels.Taskbar_Start_AC
                                    TB_Color = [CP].Windows10.Color_Index6
                                    TB_StartBtnColor = Color.FromArgb(0, 0, 0, 0)
                                    S_Color = [CP].Windows10.Color_Index4
                                    AC_Color = [CP].Windows10.Color_Index4

                                    TB_AppBack_Color = Color.FromArgb(150, [CP].Windows10.Color_Index3)
                                    AC_LinkColor = [CP].Windows10.Color_Index0
                                    TB_UL_Color = [CP].Windows10.Color_Index1
                                    Settings_Label_Color = [CP].Windows10.Color_Index3
                                    Link_preview_Color = [CP].Windows10.Color_Index3
                                    AC_Normal = [CP].Windows10.Color_Index3

                            End Select

                        Else
                            Select Case [CP].Windows10.ApplyAccentOnTaskbar
                                Case CP.Structures.Windows10x.AccentTaskbarLevels.None
                                    TB_Color = Color.FromArgb(16, 16, 16)
                                    TB_StartBtnColor = Color.FromArgb(31, 31, 31)
                                    S_Color = Color.FromArgb(31, 31, 31)
                                    AC_Color = Color.FromArgb(31, 31, 31)

                                Case CP.Structures.Windows10x.AccentTaskbarLevels.Taskbar
                                    TB_Color = [CP].Windows10.Color_Index5
                                    TB_StartBtnColor = [CP].Windows10.Color_Index4
                                    S_Color = Color.FromArgb(31, 31, 31)
                                    AC_Color = Color.FromArgb(31, 31, 31)

                                Case CP.Structures.Windows10x.AccentTaskbarLevels.Taskbar_Start_AC
                                    TB_Color = [CP].Windows10.Color_Index5
                                    TB_StartBtnColor = [CP].Windows10.Color_Index4
                                    S_Color = [CP].Windows10.Color_Index4
                                    AC_Color = [CP].Windows10.Color_Index4
                            End Select

                            If [CP].Windows10.ApplyAccentOnTaskbar = CP.Structures.Windows10x.AccentTaskbarLevels.None Then
                                TB_AppBack_Color = Color.FromArgb(150, 100, 100, 100)
                            Else
                                TB_AppBack_Color = [CP].Windows10.Color_Index4
                            End If

                            AC_LinkColor = [CP].Windows10.Color_Index0
                            TB_UL_Color = [CP].Windows10.Color_Index1
                            Settings_Label_Color = [CP].Windows10.Color_Index3
                            Link_preview_Color = [CP].Windows10.Color_Index3
                            AC_Normal = [CP].Windows10.Color_Index3

                        End If

                    Case False
                        If [CP].Windows10.Transparency Then

                            Select Case [CP].Windows10.ApplyAccentOnTaskbar
                                Case CP.Structures.Windows10x.AccentTaskbarLevels.None
                                    TB_Color = Color.FromArgb(238, 238, 238)
                                    TB_StartBtnColor = Color.Transparent
                                    S_Color = Color.FromArgb(228, 228, 228)
                                    AC_Color = Color.FromArgb(228, 228, 228)

                                    TB_AppBack_Color = Color.FromArgb(150, 238, 238, 238)
                                    AC_LinkColor = [CP].Windows10.Color_Index6
                                    TB_UL_Color = [CP].Windows10.Color_Index3
                                    Settings_Label_Color = [CP].Windows10.Color_Index3
                                    Link_preview_Color = [CP].Windows10.Color_Index3
                                    AC_Normal = [CP].Windows10.Color_Index3

                                Case CP.Structures.Windows10x.AccentTaskbarLevels.Taskbar
                                    TB_Color = [CP].Windows10.Color_Index6
                                    TB_StartBtnColor = Color.Transparent
                                    S_Color = Color.FromArgb(228, 228, 228)
                                    AC_Color = Color.FromArgb(228, 228, 228)

                                    TB_AppBack_Color = Color.FromArgb(150, [CP].Windows10.Color_Index3)
                                    AC_LinkColor = [CP].Windows10.Color_Index6
                                    TB_UL_Color = [CP].Windows10.Color_Index1
                                    Settings_Label_Color = [CP].Windows10.Color_Index3
                                    Link_preview_Color = [CP].Windows10.Color_Index3
                                    AC_Normal = [CP].Windows10.Color_Index3

                                Case CP.Structures.Windows10x.AccentTaskbarLevels.Taskbar_Start_AC
                                    TB_Color = [CP].Windows10.Color_Index6
                                    TB_StartBtnColor = Color.Transparent
                                    S_Color = [CP].Windows10.Color_Index4
                                    AC_Color = [CP].Windows10.Color_Index4

                                    TB_AppBack_Color = Color.FromArgb(150, [CP].Windows10.Color_Index3)
                                    AC_LinkColor = [CP].Windows10.Color_Index0
                                    TB_UL_Color = [CP].Windows10.Color_Index1
                                    Settings_Label_Color = [CP].Windows10.Color_Index3
                                    Link_preview_Color = [CP].Windows10.Color_Index3
                                    AC_Normal = [CP].Windows10.Color_Index3

                            End Select

                        Else

                            Select Case [CP].Windows10.ApplyAccentOnTaskbar
                                Case CP.Structures.Windows10x.AccentTaskbarLevels.None
                                    TB_Color = Color.FromArgb(238, 238, 238)
                                    TB_StartBtnColor = Color.FromArgb(241, 241, 241)
                                    S_Color = Color.FromArgb(228, 228, 228)
                                    AC_Color = Color.FromArgb(228, 228, 228)

                                    TB_AppBack_Color = Color.FromArgb(252, 252, 252)
                                    AC_LinkColor = [CP].Windows10.Color_Index6
                                    TB_UL_Color = [CP].Windows10.Color_Index3
                                    Settings_Label_Color = [CP].Windows10.Color_Index3
                                    Link_preview_Color = [CP].Windows10.Color_Index3
                                    AC_Normal = [CP].Windows10.Color_Index3

                                Case CP.Structures.Windows10x.AccentTaskbarLevels.Taskbar
                                    TB_Color = [CP].Windows10.Color_Index5
                                    TB_StartBtnColor = [CP].Windows10.Color_Index4
                                    S_Color = Color.FromArgb(228, 228, 228)
                                    AC_Color = Color.FromArgb(228, 228, 228)

                                    TB_AppBack_Color = [CP].Windows10.Color_Index4
                                    AC_LinkColor = [CP].Windows10.Color_Index6
                                    TB_UL_Color = [CP].Windows10.Color_Index1
                                    Settings_Label_Color = [CP].Windows10.Color_Index3
                                    Link_preview_Color = [CP].Windows10.Color_Index3
                                    AC_Normal = [CP].Windows10.Color_Index3

                                Case CP.Structures.Windows10x.AccentTaskbarLevels.Taskbar_Start_AC
                                    TB_Color = [CP].Windows10.Color_Index5
                                    TB_StartBtnColor = [CP].Windows10.Color_Index4
                                    S_Color = [CP].Windows10.Color_Index4
                                    AC_Color = [CP].Windows10.Color_Index4

                                    TB_AppBack_Color = [CP].Windows10.Color_Index4
                                    AC_LinkColor = [CP].Windows10.Color_Index0
                                    TB_UL_Color = [CP].Windows10.Color_Index1
                                    Settings_Label_Color = [CP].Windows10.Color_Index3
                                    Link_preview_Color = [CP].Windows10.Color_Index3
                                    AC_Normal = [CP].Windows10.Color_Index3
                            End Select

                        End If
                End Select

                ActionCenter.BackColorAlpha = AC_Alpha
                Start.BackColorAlpha = S_Alpha
                Taskbar.BackColorAlpha = TB_Alpha
                Taskbar.BlurPower = TB_Blur

                If AnimateColorChange Then
                    Visual.FadeColor(Taskbar, "Background", Taskbar.Background, TB_Color, Steps, Delay)
                    Visual.FadeColor(Taskbar, "StartColor", Taskbar.StartColor, TB_StartBtnColor, Steps, Delay)
                    Visual.FadeColor(Start, "Background", Start.Background, S_Color, Steps, Delay)
                    Visual.FadeColor(ActionCenter, "Background", ActionCenter.Background, AC_Color, Steps, Delay)
                    Visual.FadeColor(Taskbar, "AppBackground", Taskbar.AppBackground, TB_AppBack_Color, Steps, Delay)
                    Visual.FadeColor(ActionCenter, "LinkColor", ActionCenter.LinkColor, AC_LinkColor, Steps, Delay)
                    Visual.FadeColor(Taskbar, "AppUnderline", Taskbar.AppUnderline, TB_UL_Color, Steps, Delay)
                    Visual.FadeColor(setting_icon_preview, "ForeColor", setting_icon_preview.ForeColor, Settings_Label_Color, Steps, Delay)
                    Visual.FadeColor(Link_preview, "ForeColor", Link_preview.ForeColor, Link_preview_Color, Steps, Delay)
                    Visual.FadeColor(ActionCenter, "ActionCenterButton_Normal", ActionCenter.ActionCenterButton_Normal, AC_Normal, Steps, Delay)
                Else
                    Taskbar.Background = TB_Color
                    Taskbar.StartColor = TB_StartBtnColor
                    Start.Background = S_Color
                    ActionCenter.Background = AC_Color
                    Taskbar.AppBackground = TB_AppBack_Color
                    ActionCenter.LinkColor = AC_LinkColor
                    Taskbar.AppUnderline = TB_UL_Color
                    setting_icon_preview.ForeColor = Settings_Label_Color
                    Link_preview.ForeColor = Link_preview_Color
                    ActionCenter.ActionCenterButton_Normal = AC_Normal
                End If

#End Region
            Case WindowStyle.W81
#Region "Win8.1"
                Select Case [CP].Windows81.Theme
                    Case CP.Structures.Windows7.Themes.Aero
                        Taskbar.Transparency = True
                        Taskbar.BackColorAlpha = 100
                    Case CP.Structures.Windows7.Themes.AeroLite
                        Taskbar.Transparency = False
                        Taskbar.BackColorAlpha = 255
                End Select

                If AnimateColorChange Then
                    Visual.FadeColor(Taskbar, "Background", Taskbar.Background, [CP].Windows81.ColorizationColor, Steps, Delay)
                Else
                    Taskbar.Background = [CP].Windows81.ColorizationColor
                End If

                Taskbar.Win7ColorBal = [CP].Windows81.ColorizationColorBalance
#End Region
            Case WindowStyle.W7
#Region "Win7"
                Start.Transparency = Not [CP].Windows7.Theme = Structures.Windows7.Themes.Basic And Not [CP].Windows7.Theme = Structures.Windows7.Themes.Classic
                Taskbar.Transparency = Not [CP].Windows7.Theme = Structures.Windows7.Themes.Basic And Not [CP].Windows7.Theme = Structures.Windows7.Themes.Classic

                Select Case [CP].Windows7.Theme
                    Case CP.Structures.Windows7.Themes.Aero
                        With Start
                            .BackColorAlpha = [CP].Windows7.ColorizationBlurBalance
                            .Win7ColorBal = [CP].Windows7.ColorizationColorBalance
                            .Win7GlowBal = [CP].Windows7.ColorizationAfterglowBalance
                            .Background = [CP].Windows7.ColorizationColor
                            .Background2 = [CP].Windows7.ColorizationAfterglow
                            .NoisePower = [CP].Windows7.ColorizationGlassReflectionIntensity
                        End With
                        With Taskbar
                            .BackColorAlpha = [CP].Windows7.ColorizationBlurBalance
                            .Win7ColorBal = [CP].Windows7.ColorizationColorBalance
                            .Win7GlowBal = [CP].Windows7.ColorizationAfterglowBalance
                            .Background = [CP].Windows7.ColorizationColor
                            .Background2 = [CP].Windows7.ColorizationAfterglow
                            .NoisePower = [CP].Windows7.ColorizationGlassReflectionIntensity
                        End With

                    Case CP.Structures.Windows7.Themes.AeroOpaque
                        With Taskbar
                            .BackColorAlpha = [CP].Windows7.ColorizationColorBalance
                            .Background = [CP].Windows7.ColorizationColor
                            .Background2 = [CP].Windows7.ColorizationColor
                            .NoisePower = [CP].Windows7.ColorizationGlassReflectionIntensity
                        End With
                        With Start
                            .BackColorAlpha = [CP].Windows7.ColorizationColorBalance
                            .Background = [CP].Windows7.ColorizationColor
                            .Background2 = [CP].Windows7.ColorizationColor
                            .NoisePower = [CP].Windows7.ColorizationGlassReflectionIntensity
                        End With

                    Case CP.Structures.Windows7.Themes.Basic
                        Taskbar.Background = Color.FromArgb(166, 190, 218)
                        Taskbar.BackColorAlpha = 100
                        Start.Background = Color.FromArgb(166, 190, 218)
                        Start.BackColorAlpha = 100
                        Start.NoisePower = 0
                        Taskbar.NoisePower = 0
                End Select
#End Region
            Case WindowStyle.WVista
#Region "WinVista"
                Start.Transparency = Not [CP].WindowsVista.Theme = Structures.Windows7.Themes.Basic And Not [CP].WindowsVista.Theme = Structures.Windows7.Themes.Classic
                Taskbar.Transparency = Not [CP].WindowsVista.Theme = Structures.Windows7.Themes.Basic And Not [CP].WindowsVista.Theme = Structures.Windows7.Themes.Classic

                Select Case [CP].WindowsVista.Theme
                    Case CP.Structures.Windows7.Themes.Aero
                        With Start
                            .BackColorAlpha = ([CP].WindowsVista.Alpha / 255) * 180
                            .Win7ColorBal = ((255 - [CP].WindowsVista.Alpha) / 255) * 100
                            '.Win7GlowBal = [CP].WindowsVista.ColorizationAfterglowBalance
                            .Background = [CP].WindowsVista.ColorizationColor
                            .Background2 = [CP].WindowsVista.ColorizationColor
                            .NoisePower = 100
                        End With
                        With Taskbar
                            .BackColorAlpha = ([CP].WindowsVista.Alpha / 255) * 180
                            .Win7ColorBal = ((255 - [CP].WindowsVista.Alpha) / 255) * 100
                            '.Win7GlowBal = [CP].WindowsVista.ColorizationAfterglowBalance
                            .Background = [CP].WindowsVista.ColorizationColor
                            .Background2 = [CP].WindowsVista.ColorizationColor
                            .NoisePower = 100
                        End With


                    Case CP.Structures.Windows7.Themes.AeroOpaque
                        With Taskbar
                            .BackColorAlpha = ([CP].WindowsVista.Alpha / 255) * 200
                            .Background = [CP].WindowsVista.ColorizationColor
                            .Background2 = [CP].WindowsVista.ColorizationColor
                            .NoisePower = 100
                        End With
                        With Start
                            .BackColorAlpha = ([CP].WindowsVista.Alpha / 255) * 200
                            .Background = [CP].WindowsVista.ColorizationColor
                            .Background2 = [CP].WindowsVista.ColorizationColor
                            .NoisePower = 100
                        End With

                    Case CP.Structures.Windows7.Themes.Basic
                        Taskbar.Background = Color.FromArgb(166, 190, 218)
                        Taskbar.BackColorAlpha = 100
                        Start.Background = Color.FromArgb(166, 190, 218)
                        Start.BackColorAlpha = 100
                        Start.NoisePower = 0
                        Taskbar.NoisePower = 0

                End Select
#End Region
        End Select

        Taskbar.SuspendRefresh = False
        Start.SuspendRefresh = False
        ActionCenter.SuspendRefresh = False

        Taskbar.Invalidate()
        Start.Invalidate()
        ActionCenter.Invalidate()

        If Not IsFontInstalled("Segoe MDL2 Assets") Then
            setting_icon_preview.Font = New Font("Arial", 28, FontStyle.Regular)
            setting_icon_preview.Text = "♣"
        End If

    End Sub
    Public Shared Sub ApplyWinElementsStyle([CP] As CP, [Style] As WindowStyle, Taskbar As UI.Simulation.WinElement, Start As UI.Simulation.WinElement, ActionCenter As UI.Simulation.WinElement,
                                         XenonWindow1 As UI.Simulation.Window, XenonWindow2 As UI.Simulation.Window,
                                         Settings_Container As Panel, Link_preview As Label,
                                         ClassicTaskbar As UI.Retro.PanelRaisedR, ClassicStartButton As UI.Retro.ButtonR, ClassicAppButton1 As UI.Retro.ButtonR, ClassicAppButton2 As UI.Retro.ButtonR,
                                         ClassicWindow1 As UI.Retro.WindowR, ClassicWindow2 As UI.Retro.WindowR,
                                         WXP_VS_ReplaceColors As Boolean, WXP_VS_ReplaceMetrics As Boolean, WXP_VS_ReplaceFonts As Boolean)

        My.RenderingHint = If(CP.MetricsFonts.Fonts_SingleBitPP, TextRenderingHint.SingleBitPerPixelGridFit, TextRenderingHint.ClearTypeGridFit)

        Taskbar.SuspendRefresh = True
        Start.SuspendRefresh = True
        ActionCenter.SuspendRefresh = True
        XenonWindow1.SuspendRefresh = True
        XenonWindow2.SuspendRefresh = True

        Dim AC_Style As UI.Simulation.WinElement.Styles = ActionCenter.Style
        Dim Start_Style As UI.Simulation.WinElement.Styles = Start.Style
        Dim Taskbar_Style As UI.Simulation.WinElement.Styles = Taskbar.Style
        Dim XenonWindow_Style As UI.Simulation.Window.Preview_Enum = XenonWindow1.Preview

        Settings_Container.Visible = ([Style] = WindowStyle.W11 Or [Style] = WindowStyle.W10)
        Link_preview.Visible = ([Style] = WindowStyle.W11 Or [Style] = WindowStyle.W10)
        Start.Visible = (Not [Style] = WindowStyle.W81) And Not ([Style] = WindowStyle.W10 And [CP].WindowsEffects.FullScreenStartMenu)
        ActionCenter.Visible = ([Style] = WindowStyle.W11 Or [Style] = WindowStyle.W10)

        Select Case [Style]
            Case WindowStyle.W11
                XenonWindow_Style = UI.Simulation.Window.Preview_Enum.W11

                AC_Style = UI.Simulation.WinElement.Styles.ActionCenter11

                If ExplorerPatcher.IsAllowed Then
                    With My.EP
                        If Not .UseStart10 Then
                            Start_Style = UI.Simulation.WinElement.Styles.Start11
                        Else
                            Start_Style = UI.Simulation.WinElement.Styles.Start10
                        End If

                        If Not .UseTaskbar10 Then
                            Taskbar_Style = UI.Simulation.WinElement.Styles.Taskbar11
                        Else
                            Taskbar_Style = UI.Simulation.WinElement.Styles.Taskbar10
                        End If
                    End With
                Else
                    Taskbar_Style = UI.Simulation.WinElement.Styles.Taskbar11
                    Start_Style = UI.Simulation.WinElement.Styles.Start11
                End If

            Case WindowStyle.W10
                XenonWindow_Style = UI.Simulation.Window.Preview_Enum.W10
                AC_Style = UI.Simulation.WinElement.Styles.ActionCenter10
                Taskbar_Style = UI.Simulation.WinElement.Styles.Taskbar10
                Start_Style = UI.Simulation.WinElement.Styles.Start10

            Case WindowStyle.W81
                XenonWindow_Style = If(CP.Windows81.Theme = Structures.Windows7.Themes.AeroLite, UI.Simulation.Window.Preview_Enum.W8Lite, UI.Simulation.Window.Preview_Enum.W8)
                Taskbar_Style = If(CP.Windows81.Theme = Structures.Windows7.Themes.Aero, UI.Simulation.WinElement.Styles.Taskbar8Aero, UI.Simulation.WinElement.Styles.Taskbar8Lite)

            Case WindowStyle.W7
                Select Case CP.Windows7.Theme
                    Case Structures.Windows7.Themes.Aero
                        XenonWindow_Style = UI.Simulation.Window.Preview_Enum.W7Aero
                        Taskbar_Style = UI.Simulation.WinElement.Styles.Taskbar7Aero
                        Start_Style = UI.Simulation.WinElement.Styles.Start7Aero

                    Case Structures.Windows7.Themes.AeroOpaque
                        XenonWindow_Style = UI.Simulation.Window.Preview_Enum.W7Opaque
                        Taskbar_Style = UI.Simulation.WinElement.Styles.Taskbar7Opaque
                        Start_Style = UI.Simulation.WinElement.Styles.Start7Opaque

                    Case Structures.Windows7.Themes.Basic
                        XenonWindow_Style = UI.Simulation.Window.Preview_Enum.W7Basic
                        Taskbar_Style = UI.Simulation.WinElement.Styles.Taskbar7Basic
                        Start_Style = UI.Simulation.WinElement.Styles.Start7Basic

                End Select

            Case WindowStyle.WVista
                Select Case CP.WindowsVista.Theme     'Windows Vista uses the same aero of Windows 7
                    Case Structures.Windows7.Themes.Aero
                        XenonWindow_Style = UI.Simulation.Window.Preview_Enum.W7Aero
                        Taskbar_Style = UI.Simulation.WinElement.Styles.TaskbarVistaAero
                        Start_Style = UI.Simulation.WinElement.Styles.StartVistaAero

                    Case Structures.Windows7.Themes.AeroOpaque
                        XenonWindow_Style = UI.Simulation.Window.Preview_Enum.W7Opaque
                        Taskbar_Style = UI.Simulation.WinElement.Styles.TaskbarVistaOpaque
                        Start_Style = UI.Simulation.WinElement.Styles.StartVistaOpaque

                    Case Structures.Windows7.Themes.Basic
                        XenonWindow_Style = UI.Simulation.Window.Preview_Enum.W7Basic
                        Taskbar_Style = UI.Simulation.WinElement.Styles.TaskbarVistaBasic
                        Start_Style = UI.Simulation.WinElement.Styles.StartVistaBasic

                End Select

            Case WindowStyle.WXP
                XenonWindow_Style = UI.Simulation.Window.Preview_Enum.WXP
                Taskbar_Style = UI.Simulation.WinElement.Styles.TaskbarXP
                Start_Style = UI.Simulation.WinElement.Styles.StartXP

                Select Case CP.WindowsXP.Theme
                    Case CP.Structures.WindowsXP.Themes.LunaBlue
                        My.VS = My.PATH_appData & "\VisualStyles\Luna\luna.theme"
                        IO.File.WriteAllText(My.PATH_appData & "\VisualStyles\Luna\luna.theme", String.Format("[VisualStyles]{1}Path={0}{1}ColorStyle=NormalColor{1}Size=NormalSize", My.PATH_appData & "\VisualStyles\Luna\luna.msstyles", vbCrLf))
                        My.resVS = New VisualStylesRes(My.VS)

                    Case CP.Structures.WindowsXP.Themes.LunaOliveGreen
                        My.VS = My.PATH_appData & "\VisualStyles\Luna\luna.theme"
                        IO.File.WriteAllText(My.PATH_appData & "\VisualStyles\Luna\luna.theme", String.Format("[VisualStyles]{1}Path={0}{1}ColorStyle=HomeStead{1}Size=NormalSize", My.PATH_appData & "\VisualStyles\Luna\luna.msstyles", vbCrLf))
                        My.resVS = New VisualStylesRes(My.VS)

                    Case CP.Structures.WindowsXP.Themes.LunaSilver
                        My.VS = My.PATH_appData & "\VisualStyles\Luna\luna.theme"
                        IO.File.WriteAllText(My.PATH_appData & "\VisualStyles\Luna\luna.theme", String.Format("[VisualStyles]{1}Path={0}{1}ColorStyle=Metallic{1}Size=NormalSize", My.PATH_appData & "\VisualStyles\Luna\luna.msstyles", vbCrLf))
                        My.resVS = New VisualStylesRes(My.VS)

                    Case CP.Structures.WindowsXP.Themes.Custom
                        If IO.File.Exists(CP.WindowsXP.ThemeFile) Then
                            If IO.Path.GetExtension(CP.WindowsXP.ThemeFile) = ".theme" Then
                                My.VS = CP.WindowsXP.ThemeFile
                            ElseIf IO.Path.GetExtension(CP.WindowsXP.ThemeFile) = ".msstyles" Then
                                My.VS = My.PATH_appData & "\VisualStyles\Luna\luna.theme"
                                IO.File.WriteAllText(My.PATH_appData & "\VisualStyles\Luna\luna.theme", String.Format("[VisualStyles]{1}Path={0}{1}ColorStyle={2}{1}Size=NormalSize", CP.WindowsXP.ThemeFile, vbCrLf, CP.WindowsXP.ColorScheme))
                            End If
                        End If
                        My.resVS = New VisualStylesRes(My.VS)

                    Case CP.Structures.WindowsXP.Themes.Classic
                        My.VS = My.PATH_appData & "\VisualStyles\Luna\luna.theme"
                        IO.File.WriteAllText(My.PATH_appData & "\VisualStyles\Luna\luna.theme", String.Format("[VisualStyles]{1}Path={0}{1}ColorStyle=NormalColor{1}Size=NormalSize", My.PATH_appData & "\VisualStyles\Luna\luna.msstyles", vbCrLf))
                        My.resVS = New VisualStylesRes(My.VS)

                End Select

                If WXP_VS_ReplaceColors And CP.WindowsXP.Theme <> CP.Structures.WindowsXP.Themes.Classic Then
                    If IO.File.Exists(My.VS) And Not String.IsNullOrEmpty(My.VS) Then
                        Dim vs As New Devcorp.Controls.VisualStyles.VisualStyleFile(My.VS)
                        CP.Win32.Load(Structures.Win32UI.Method.VisualStyles, vs.Metrics)
                    End If
                End If

                If WXP_VS_ReplaceMetrics And CP.WindowsXP.Theme <> CP.Structures.WindowsXP.Themes.Classic Then
                    If IO.File.Exists(My.VS) And Not String.IsNullOrEmpty(My.VS) Then
                        Dim vs As New Devcorp.Controls.VisualStyles.VisualStyleFile(My.VS)
                        CP.MetricsFonts.Overwrite_Metrics(vs.Metrics)
                    End If
                End If

                If WXP_VS_ReplaceFonts And CP.WindowsXP.Theme <> CP.Structures.WindowsXP.Themes.Classic Then
                    If IO.File.Exists(My.VS) And Not String.IsNullOrEmpty(My.VS) Then
                        Dim vs As New Devcorp.Controls.VisualStyles.VisualStyleFile(My.VS)
                        CP.MetricsFonts.Overwrite_Fonts(vs.Metrics)
                    End If
                End If

        End Select
        XenonWindow1.Preview = XenonWindow_Style
        Start.Style = Start_Style
        Taskbar.Style = Taskbar_Style
        ActionCenter.Style = AC_Style
        XenonWindow1.WinVista = ([Style] = WindowStyle.WVista)
        XenonWindow2.WinVista = ([Style] = WindowStyle.WVista)
        XenonWindow2.Preview = XenonWindow1.Preview

        SetModernWindowMetrics([CP], XenonWindow1)
        SetModernWindowMetrics([CP], XenonWindow2)
        SetClassicWindowMetrics([CP], ClassicWindow1)
        SetClassicWindowMetrics([CP], ClassicWindow2)
        SetClassicWindowColors([CP], ClassicWindow1)
        SetClassicWindowColors([CP], ClassicWindow2, False)
        SetClassicButtonColors([CP], ClassicStartButton)
        SetClassicButtonColors([CP], ClassicAppButton1)
        SetClassicButtonColors([CP], ClassicAppButton2)
        SetClassicRaisedPanelColors([CP], ClassicTaskbar)

        If [Style] <> WindowStyle.WVista And [Style] <> WindowStyle.WXP Then
            ClassicTaskbar.Height = 44
            ClassicAppButton1.Image = My.Resources.SampleApp_Active
            ClassicAppButton2.Image = My.Resources.SampleApp_Inactive
            ClassicStartButton.Image = My.Resources.Native7.Resize(18, 16)
            ClassicAppButton1.ImageAlign = ContentAlignment.MiddleCenter
            ClassicAppButton2.ImageAlign = Drawing.ContentAlignment.MiddleCenter
            ClassicAppButton1.Width = 48
            ClassicAppButton2.Width = 48
            ClassicAppButton1.Text = ""
            ClassicAppButton2.Text = ""
            ClassicAppButton2.Left = ClassicAppButton1.Right + 3
            ClassicAppButton1.Font = New Font(CP.MetricsFonts.CaptionFont.Name, 8, ClassicAppButton1.Font.Style)
            ClassicAppButton2.Font = New Font(CP.MetricsFonts.CaptionFont.Name, 8, ClassicAppButton2.Font.Style)
            ClassicStartButton.Font = New Font(CP.MetricsFonts.CaptionFont.Name, 8.5, ClassicStartButton.Font.Style)
            ClassicAppButton1.HatchBrush = False
        End If

        Select Case [Style]
            Case WindowStyle.W11
                If My.W11 Then My.EP = New ExplorerPatcher

                If ExplorerPatcher.IsAllowed Then

                    With My.EP

                        If Not .UseTaskbar10 Then
                            Taskbar.BlurPower = 8
                            Taskbar.Height = 42
                            Taskbar.NoisePower = 0.3
                        Else
                            Taskbar.BlurPower = 8
                            Taskbar.Height = 35
                            Taskbar.UseWin11ORB_WithWin10 = Not .TaskbarButton10
                            Taskbar.NoisePower = 0
                        End If

                        If Not .UseStart10 Then
                            Start.BlurPower = 6
                            Start.NoisePower = 0.3
                            Start.Size = New Size(135, 200)
                            Start.Location = New Point(10, Taskbar.Bottom - Taskbar.Height - Start.Height - 10)
                        Else
                            Start.BlurPower = 7
                            Start.NoisePower = 0.3

                            Select Case .StartStyle
                                Case ExplorerPatcher.StartStyles.NotRounded
                                    Start.Size = New Size(182, 201)
                                    Start.Left = 0
                                    Start.Top = Taskbar.Bottom - Taskbar.Height - Start.Height
                                    Start.UseWin11RoundedCorners_WithWin10_Level1 = False
                                    Start.UseWin11RoundedCorners_WithWin10_Level2 = False

                                Case ExplorerPatcher.StartStyles.RoundedCornersDockedMenu
                                    Start.Size = New Size(182, 201)
                                    Start.Left = 0
                                    Start.Top = Taskbar.Bottom - Taskbar.Height - Start.Height
                                    Start.UseWin11RoundedCorners_WithWin10_Level1 = True
                                    Start.UseWin11RoundedCorners_WithWin10_Level2 = False

                                Case ExplorerPatcher.StartStyles.RoundedCornersFloatingMenu
                                    Start.Size = New Size(182, 201)
                                    Start.Location = New Point(10, Taskbar.Bottom - Taskbar.Height - Start.Height - 10)
                                    Start.UseWin11RoundedCorners_WithWin10_Level1 = False
                                    Start.UseWin11RoundedCorners_WithWin10_Level2 = True

                            End Select

                        End If
                    End With

                Else
                    Taskbar.BlurPower = 8
                    Taskbar.Height = 42
                    Taskbar.NoisePower = 0.3
                    '########################
                    Start.BlurPower = 6
                    Start.NoisePower = 0.3
                    Start.Size = New Size(135, 200)
                    Start.Location = New Point(10, Taskbar.Bottom - Taskbar.Height - Start.Height - 10)
                End If

                ActionCenter.Dock = Nothing
                ActionCenter.BlurPower = 6
                ActionCenter.NoisePower = 0.3
                ActionCenter.Size = New Size(120, 85)
                ActionCenter.Location = New Point(ActionCenter.Parent.Width - ActionCenter.Width - 10, ActionCenter.Parent.Height - ActionCenter.Height - Taskbar.Height - 10)

            Case WindowStyle.W10
                ActionCenter.Dock = DockStyle.Right
                ActionCenter.BlurPower = 7
                ActionCenter.NoisePower = 0.3
                '########################
                Taskbar.BlurPower = If(Not CP.Windows10.IncreaseTBTransparency, 12, 6)
                '########################
                Start.BlurPower = 7
                Start.NoisePower = 0.3
                '########################

                Taskbar.Height = 35
                Taskbar.UseWin11ORB_WithWin10 = False
                Start.Size = New Size(182, 201)
                Start.Left = 0
                Start.Top = Taskbar.Bottom - Taskbar.Height - Start.Height
                Start.UseWin11RoundedCorners_WithWin10_Level1 = False
                Start.UseWin11RoundedCorners_WithWin10_Level2 = False

            Case WindowStyle.W81
                Settings_Container.Visible = False
                Link_preview.Visible = False
                Start.Visible = False
                ActionCenter.Visible = False
                Taskbar.BlurPower = 0
                Taskbar.Height = 34

                Start.BlurPower = 0
                Start.Top = Taskbar.Top - Start.Height
                Start.Left = 0

            Case WindowStyle.W7
                Settings_Container.Visible = False
                Link_preview.Visible = False
                ActionCenter.Visible = False

                Taskbar.BlurPower = 1
                Taskbar.NoisePower = CP.Windows7.ColorizationGlassReflectionIntensity / 100
                Taskbar.Height = 34

                Start.BlurPower = 1
                Start.NoisePower = 0.5
                Start.Width = 136
                Start.Height = 191
                Start.NoisePower = CP.Windows7.ColorizationGlassReflectionIntensity / 100
                Start.Left = 0
                Start.Top = Taskbar.Top - Start.Height

            Case WindowStyle.WVista
                Settings_Container.Visible = False
                Link_preview.Visible = False
                ActionCenter.Visible = False
                Taskbar.Height = 30

                Start.Width = 136
                Start.Height = 191
                Start.Left = 0
                Start.Top = Taskbar.Top - Start.Height

                ClassicTaskbar.Height = Taskbar.Height
                ClassicAppButton1.Image = My.Resources.SampleApp_Active.Resize(23, 23)
                ClassicAppButton2.Image = My.Resources.SampleApp_Inactive.Resize(23, 23)
                ClassicStartButton.Image = My.Resources.Native7.Resize(18, 16)
                ClassicAppButton1.ImageAlign = ContentAlignment.BottomLeft
                ClassicAppButton2.ImageAlign = ContentAlignment.BottomLeft
                ClassicAppButton1.Width = 140
                ClassicAppButton2.Width = 140
                ClassicAppButton1.Text = ClassicWindow1.Text
                ClassicAppButton2.Text = ClassicWindow2.Text
                ClassicAppButton2.Left = ClassicAppButton1.Right + 3
                ClassicAppButton1.Font = New Font(CP.MetricsFonts.CaptionFont.Name, 8, ClassicAppButton1.Font.Style)
                ClassicAppButton2.Font = New Font(CP.MetricsFonts.CaptionFont.Name, 8, ClassicAppButton2.Font.Style)
                ClassicStartButton.Font = New Font(CP.MetricsFonts.CaptionFont.Name, 8.5, ClassicStartButton.Font.Style)
                ClassicAppButton1.HatchBrush = True

            Case WindowStyle.WXP
                Taskbar.Height = 30
                Start.Width = 150
                Start.Height = 190
                Start.Left = 0
                Start.Top = Taskbar.Top - Start.Height

                ClassicTaskbar.Height = Taskbar.Height
                ClassicAppButton1.Image = My.Resources.SampleApp_Active.Resize(23, 23)
                ClassicAppButton2.Image = My.Resources.SampleApp_Inactive.Resize(23, 23)
                ClassicStartButton.Image = My.Resources.NativeXP.Resize(18, 16)
                ClassicAppButton1.ImageAlign = ContentAlignment.BottomLeft
                ClassicAppButton2.ImageAlign = ContentAlignment.BottomLeft
                ClassicAppButton1.Width = 140
                ClassicAppButton2.Width = 140
                ClassicAppButton1.Text = ClassicWindow1.Text
                ClassicAppButton2.Text = ClassicWindow2.Text
                ClassicAppButton2.Left = ClassicAppButton1.Right + 3
                ClassicAppButton1.Font = New Font(CP.MetricsFonts.CaptionFont.Name, 8, ClassicAppButton1.Font.Style)
                ClassicAppButton2.Font = New Font(CP.MetricsFonts.CaptionFont.Name, 8, ClassicAppButton2.Font.Style)
                ClassicStartButton.Font = New Font(CP.MetricsFonts.CaptionFont.Name, 8.5, ClassicStartButton.Font.Style)
                ClassicAppButton1.HatchBrush = True

        End Select

        If ([Style] = WindowStyle.W10 And Not [CP].WindowsEffects.FullScreenStartMenu) Or [Style] = WindowStyle.W11 Then
            XenonWindow1.Left = Start.Right + (XenonWindow1.Parent.Width - (Start.Width + Start.Left) - (ActionCenter.Width + (ActionCenter.Parent.Width - ActionCenter.Right)) - XenonWindow1.Width) / 2
        Else
            XenonWindow1.Left = (XenonWindow1.Parent.Width - XenonWindow1.Width) / 2
        End If

        XenonWindow1.Top = (XenonWindow1.Parent.Height - Taskbar.Height - (XenonWindow1.Height + XenonWindow2.Height)) / 2
        XenonWindow2.Top = XenonWindow1.Bottom
        XenonWindow2.Left = XenonWindow1.Left

        Taskbar.SuspendRefresh = False
        Start.SuspendRefresh = False
        ActionCenter.SuspendRefresh = False
        XenonWindow1.SuspendRefresh = False
        XenonWindow2.SuspendRefresh = False

        Taskbar.NoiseBack()
        Start.NoiseBack()
        ActionCenter.NoiseBack()

        Taskbar.ProcessBack()
        Start.ProcessBack()
        ActionCenter.ProcessBack()
        XenonWindow1.ProcessBack()
        XenonWindow2.ProcessBack()

        Taskbar.Invalidate()
        Start.Invalidate()
        ActionCenter.Invalidate()
        XenonWindow1.Invalidate()
        XenonWindow2.Invalidate()

    End Sub
    Public Shared Sub ApplyWindowStyles([CP] As CP, [Style] As WindowStyle, XenonWindow1 As UI.Simulation.Window, XenonWindow2 As UI.Simulation.Window, Optional StartButton As UI.WP.Button = Nothing, Optional LogonUIButton As UI.WP.Button = Nothing)
        XenonWindow1.Active = True
        XenonWindow2.Active = False

        If ExplorerPatcher.IsAllowed Then My.EP = New ExplorerPatcher

        XenonWindow1.SuspendRefresh = True
        XenonWindow2.SuspendRefresh = True

        Select Case [Style]
            Case WindowStyle.W11
#Region "Win11"
                XenonWindow1.AccentColor_Enabled = [CP].Windows11.ApplyAccentOnTitlebars
                XenonWindow2.AccentColor_Enabled = [CP].Windows11.ApplyAccentOnTitlebars

                XenonWindow1.AccentColor_Active = [CP].Windows11.Titlebar_Active
                XenonWindow2.AccentColor_Active = [CP].Windows11.Titlebar_Active

                XenonWindow1.AccentColor_Inactive = [CP].Windows11.Titlebar_Inactive
                XenonWindow2.AccentColor_Inactive = [CP].Windows11.Titlebar_Inactive

                XenonWindow1.DarkMode = Not [CP].Windows11.AppMode_Light
                XenonWindow2.DarkMode = Not [CP].Windows11.AppMode_Light

                XenonWindow1.Shadow = [CP].WindowsEffects.WindowShadow
                XenonWindow2.Shadow = [CP].WindowsEffects.WindowShadow

#End Region
            Case WindowStyle.W10
#Region "Win10"
                XenonWindow1.AccentColor_Enabled = [CP].Windows10.ApplyAccentOnTitlebars
                XenonWindow2.AccentColor_Enabled = [CP].Windows10.ApplyAccentOnTitlebars

                XenonWindow1.AccentColor_Active = [CP].Windows10.Titlebar_Active
                XenonWindow2.AccentColor_Active = [CP].Windows10.Titlebar_Active

                XenonWindow1.AccentColor_Inactive = [CP].Windows10.Titlebar_Inactive
                XenonWindow2.AccentColor_Inactive = [CP].Windows10.Titlebar_Inactive

                XenonWindow1.DarkMode = Not [CP].Windows10.AppMode_Light
                XenonWindow2.DarkMode = Not [CP].Windows10.AppMode_Light

                XenonWindow1.Shadow = [CP].WindowsEffects.WindowShadow
                XenonWindow2.Shadow = [CP].WindowsEffects.WindowShadow
#End Region
            Case WindowStyle.W81
#Region "Win8.1"
                If (My.W8 Or My.W81) And My.Settings.Miscellaneous.Win7LivePreview Then
                    RefreshDWM([CP])
                End If

                If StartButton IsNot Nothing Then ApplyMetroStartToButton([CP], StartButton)
                If LogonUIButton IsNot Nothing Then ApplyBackLogonUI([CP], LogonUIButton)

                Select Case [CP].Windows81.Theme
                    Case CP.Structures.Windows7.Themes.Aero
                        XenonWindow1.Preview = UI.Simulation.Window.Preview_Enum.W8
                        XenonWindow2.Preview = UI.Simulation.Window.Preview_Enum.W8
                    Case CP.Structures.Windows7.Themes.AeroLite
                        XenonWindow1.Preview = UI.Simulation.Window.Preview_Enum.W8Lite
                        XenonWindow2.Preview = UI.Simulation.Window.Preview_Enum.W8Lite
                End Select

                XenonWindow1.AccentColor_Active = [CP].Windows81.ColorizationColor
                XenonWindow1.Win7ColorBal = [CP].Windows81.ColorizationColorBalance

                XenonWindow2.AccentColor_Active = [CP].Windows81.ColorizationColor
                XenonWindow2.Win7ColorBal = [CP].Windows81.ColorizationColorBalance

#End Region
            Case WindowStyle.W7
#Region "Win7"
                If My.WVista And My.Settings.Miscellaneous.Win7LivePreview Then
                    RefreshDWM([CP])
                End If

                XenonWindow1.Shadow = [CP].WindowsEffects.WindowShadow
                XenonWindow2.Shadow = [CP].WindowsEffects.WindowShadow

                Select Case [CP].Windows7.Theme
                    Case CP.Structures.Windows7.Themes.Aero
                        With XenonWindow1
                            .Preview = UI.Simulation.Window.Preview_Enum.W7Aero
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
                            .Preview = UI.Simulation.Window.Preview_Enum.W7Aero
                            .Win7Alpha = [CP].Windows7.ColorizationBlurBalance
                            .Win7ColorBal = [CP].Windows7.ColorizationColorBalance
                            .Win7GlowBal = [CP].Windows7.ColorizationAfterglowBalance
                            .AccentColor_Active = [CP].Windows7.ColorizationColor
                            .AccentColor2_Active = [CP].Windows7.ColorizationAfterglow
                            .AccentColor_Inactive = [CP].Windows7.ColorizationColor
                            .AccentColor2_Inactive = [CP].Windows7.ColorizationAfterglow
                            .Win7Noise = [CP].Windows7.ColorizationGlassReflectionIntensity
                        End With

                    Case CP.Structures.Windows7.Themes.AeroOpaque
                        With XenonWindow1
                            .Preview = UI.Simulation.Window.Preview_Enum.W7Opaque
                            .Win7Alpha = [CP].Windows7.ColorizationColorBalance
                            .AccentColor_Active = [CP].Windows7.ColorizationColor
                            .AccentColor_Inactive = [CP].Windows7.ColorizationColor
                            .Win7Noise = [CP].Windows7.ColorizationGlassReflectionIntensity
                        End With
                        With XenonWindow2
                            .Preview = UI.Simulation.Window.Preview_Enum.W7Opaque
                            .Win7Alpha = [CP].Windows7.ColorizationColorBalance
                            .AccentColor_Active = [CP].Windows7.ColorizationColor
                            .AccentColor_Inactive = [CP].Windows7.ColorizationColor
                            .Win7Noise = [CP].Windows7.ColorizationGlassReflectionIntensity
                        End With

                    Case CP.Structures.Windows7.Themes.Basic
                        With XenonWindow1
                            .Preview = UI.Simulation.Window.Preview_Enum.W7Basic
                            .Win7Alpha = 100
                            .AccentColor_Active = Color.FromArgb(166, 190, 218)
                            .Win7Noise = 0
                        End With
                        With XenonWindow2
                            .Preview = UI.Simulation.Window.Preview_Enum.W7Basic
                            .Win7Alpha = 100
                            .AccentColor_Inactive = Color.FromArgb(166, 190, 218)
                            .Win7Noise = 0
                        End With
                End Select
#End Region
            Case WindowStyle.WVista
#Region "WinVista"
                If My.WVista And My.Settings.Miscellaneous.Win7LivePreview Then
                    RefreshDWM([CP])
                End If

                XenonWindow1.Shadow = [CP].WindowsEffects.WindowShadow
                XenonWindow2.Shadow = [CP].WindowsEffects.WindowShadow

                Select Case [CP].WindowsVista.Theme
                    Case CP.Structures.Windows7.Themes.Aero
                        With XenonWindow1
                            .Preview = UI.Simulation.Window.Preview_Enum.W7Aero
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
                            .Preview = UI.Simulation.Window.Preview_Enum.W7Aero
                            .Win7Alpha = ((255 - [CP].WindowsVista.Alpha) / 255) * 100
                            .Win7ColorBal = ((255 - [CP].WindowsVista.Alpha) / 255) * 100
                            '.Win7GlowBal = [CP].WindowsVista.ColorizationAfterglowBalance
                            .AccentColor_Active = [CP].WindowsVista.ColorizationColor
                            .AccentColor2_Active = [CP].WindowsVista.ColorizationColor
                            .AccentColor_Inactive = Color.FromArgb(100, [CP].WindowsVista.ColorizationColor)
                            .AccentColor2_Inactive = Color.FromArgb(100, [CP].WindowsVista.ColorizationColor)
                            .Win7Noise = 100
                        End With

                    Case CP.Structures.Windows7.Themes.AeroOpaque
                        With XenonWindow1
                            .Preview = UI.Simulation.Window.Preview_Enum.W7Opaque
                            .Win7Alpha = (([CP].WindowsVista.Alpha) / 255) * 100
                            .AccentColor_Active = [CP].WindowsVista.ColorizationColor
                            .AccentColor_Inactive = [CP].WindowsVista.ColorizationColor
                            .Win7Noise = 100
                        End With
                        With XenonWindow2
                            .Preview = UI.Simulation.Window.Preview_Enum.W7Opaque
                            .Win7Alpha = (([CP].WindowsVista.Alpha) / 255) * 100
                            .AccentColor_Active = [CP].WindowsVista.ColorizationColor
                            .AccentColor_Inactive = [CP].WindowsVista.ColorizationColor
                            .Win7Noise = 100
                        End With

                    Case CP.Structures.Windows7.Themes.Basic
                        With XenonWindow1
                            .Preview = UI.Simulation.Window.Preview_Enum.W7Basic
                            .Win7Alpha = 100
                            .AccentColor_Active = Color.FromArgb(166, 190, 218)
                            .Win7Noise = 0
                        End With
                        With XenonWindow2
                            .Preview = UI.Simulation.Window.Preview_Enum.W7Basic
                            .Win7Alpha = 100
                            .AccentColor_Inactive = Color.FromArgb(166, 190, 218)
                            .Win7Noise = 0
                        End With

                End Select
#End Region

        End Select

        XenonWindow1.SuspendRefresh = False
        XenonWindow2.SuspendRefresh = False

        XenonWindow1.Invalidate()
        XenonWindow2.Invalidate()
    End Sub
    Public Shared Sub AdjustPreview_ModernOrClassic([CP] As CP, [Style] As WindowStyle, tabs_preview As UI.WP.TablessControl, WXP_Alert As UI.WP.AlertBox)
        If [CP] IsNot Nothing Then
            Dim condition0 As Boolean = [Style] = WindowStyle.W7 AndAlso CP.Windows7.Theme = Structures.Windows7.Themes.Classic
            Dim condition1 As Boolean = [Style] = WindowStyle.WVista AndAlso CP.WindowsVista.Theme = Structures.Windows7.Themes.Classic
            Dim condition2 As Boolean = [Style] = WindowStyle.WXP AndAlso CP.WindowsXP.Theme = CP.Structures.WindowsXP.Themes.Classic
            WXP_Alert.Visible = [Style] = WindowStyle.WXP AndAlso My.StartedWithClassicTheme
            tabs_preview.SelectedIndex = If(condition0 Or condition1 Or condition2, 1, 0)
        End If
    End Sub

    Public Shared Sub SetClassicWindowMetrics([CP] As CP, [Window] As UI.Retro.WindowR)
        If [CP] IsNot Nothing Then
            [Window].Metrics_BorderWidth = CP.MetricsFonts.BorderWidth
            [Window].Metrics_CaptionHeight = CP.MetricsFonts.CaptionHeight
            [Window].Metrics_CaptionWidth = CP.MetricsFonts.CaptionWidth
            [Window].Metrics_PaddedBorderWidth = CP.MetricsFonts.PaddedBorderWidth
            [Window].Font = CP.MetricsFonts.CaptionFont
            [Window].Refresh()
        End If
    End Sub
    Public Shared Sub SetModernWindowMetrics([CP] As CP, XenonWindow As UI.Simulation.Window)
        If [CP] IsNot Nothing Then
            XenonWindow.Font = [CP].MetricsFonts.CaptionFont
            XenonWindow.Metrics_BorderWidth = [CP].MetricsFonts.BorderWidth
            XenonWindow.Metrics_CaptionHeight = [CP].MetricsFonts.CaptionHeight
            XenonWindow.Metrics_PaddedBorderWidth = [CP].MetricsFonts.PaddedBorderWidth
            XenonWindow.Invalidate()
        End If
    End Sub
    Public Shared Sub SetClassicWindowColors([CP] As CP, [Window] As UI.Retro.WindowR, Optional Active As Boolean = True)
        If [CP] IsNot Nothing Then
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
        End If
    End Sub
    Public Shared Sub SetClassicRaisedPanelColors([CP] As CP, [Panel] As UI.Retro.PanelRaisedR)
        [Panel].BackColor = [CP].Win32.ButtonFace
        [Panel].ButtonHilight = [CP].Win32.ButtonHilight
        [Panel].ButtonLight = [CP].Win32.ButtonLight
        [Panel].ButtonShadow = [CP].Win32.ButtonShadow
        [Panel].ButtonDkShadow = [CP].Win32.ButtonDkShadow
        [Panel].ForeColor = [CP].Win32.TitleText
    End Sub
    Public Shared Sub SetClassicPanelColors([CP] As CP, [Panel] As UI.Retro.PanelR)
        [Panel].BackColor = [CP].Win32.ButtonFace
        [Panel].ButtonHilight = [CP].Win32.ButtonHilight
        [Panel].ButtonLight = [CP].Win32.ButtonLight
        [Panel].ButtonShadow = [CP].Win32.ButtonShadow
        [Panel].ButtonDkShadow = [CP].Win32.ButtonDkShadow
        [Panel].ForeColor = [CP].Win32.TitleText
    End Sub
    Public Shared Sub SetClassicButtonColors([CP] As CP, [Button] As UI.Retro.ButtonR)
        [Button].ButtonDkShadow = [CP].Win32.ButtonDkShadow
        [Button].ButtonHilight = [CP].Win32.ButtonHilight
        [Button].ButtonLight = [CP].Win32.ButtonLight
        [Button].ButtonShadow = [CP].Win32.ButtonShadow
        [Button].BackColor = [CP].Win32.ButtonFace
        [Button].ForeColor = [CP].Win32.ButtonText
        [Button].WindowFrame = [CP].Win32.WindowFrame
        [Button].FocusRectWidth = [CP].WindowsEffects.FocusRectWidth
        [Button].FocusRectHeight = [CP].WindowsEffects.FocusRectHeight
    End Sub

    Public Shared Sub ReValidateLivePreview(Parent As Control)
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

    Public Shared Sub ApplyMetroStartToButton(ColorPalette As CP, W81_start As UI.WP.Button)
        Select Case ColorPalette.Windows81.Start
            Case 1
                W81_start.Image = Start8Selector.img1.Image.Resize(48, 48)
            Case 2
                W81_start.Image = Start8Selector.img2.Image.Resize(48, 48)
            Case 3
                W81_start.Image = Start8Selector.img3.Image.Resize(48, 48)
            Case 4
                W81_start.Image = Start8Selector.img4.Image.Resize(48, 48)
            Case 5
                W81_start.Image = Start8Selector.img5.Image.Resize(48, 48)
            Case 6
                W81_start.Image = Start8Selector.img6.Image.Resize(48, 48)
            Case 7
                W81_start.Image = Start8Selector.img7.Image.Resize(48, 48)
            Case 8
                W81_start.Image = Start8Selector.img8.Image.Resize(48, 48)
            Case 9
                W81_start.Image = Start8Selector.img9.Image.Resize(48, 48)
            Case 10
                W81_start.Image = Start8Selector.img10.Image.Resize(48, 48)
            Case 11
                W81_start.Image = Start8Selector.img11.Image.Resize(48, 48)
            Case 12
                W81_start.Image = Start8Selector.img12.Image.Resize(48, 48)
            Case 13
                W81_start.Image = Start8Selector.img13.Image.Resize(48, 48)
            Case 14
                W81_start.Image = Start8Selector.img14.Image.Resize(48, 48)
            Case 15
                W81_start.Image = Start8Selector.img15.Image.Resize(48, 48)
            Case 16
                W81_start.Image = Start8Selector.img16.Image.Resize(48, 48)
            Case 17
                W81_start.Image = Start8Selector.img17.Image.Resize(48, 48)
            Case 18
                W81_start.Image = Start8Selector.img18.Image.Resize(48, 48)
            Case 19
                W81_start.Image = ColorPalette.Windows81.PersonalColors_Background.ToBitmap(New Size(48, 48))
            Case 20
                W81_start.Image = My.Wallpaper.Resize(48, 48)

            Case Else
                W81_start.Image = Start8Selector.img1.Image.Resize(48, 48)
        End Select
    End Sub
    Public Shared Sub ApplyBackLogonUI(ColorPalette As CP, W8_logonui As UI.WP.Button)

        Select Case ColorPalette.Windows81.LogonUI
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
    Public Shared Function GetTintedWallpaper(WT As CP.Structures.WallpaperTone) As Bitmap
        If Not IO.File.Exists([WT].Image) Then
            If My.WXP Then
                [WT].Image = My.PATH_Windows & "\Web\Wallpaper\Bliss.bmp"
            Else
                [WT].Image = My.PATH_Windows & "\Web\Wallpaper\Windows\img0.jpg"
            End If
        End If

        Using ImgF As New ImageProcessor.ImageFactory
            ImgF.Load([WT].Image)
            ImgF.Hue(WT.H, True)
            ImgF.Saturation(WT.S - 100)
            ImgF.Brightness(WT.L - 100)

            Return ImgF.Image.Clone
        End Using

    End Function
End Class
