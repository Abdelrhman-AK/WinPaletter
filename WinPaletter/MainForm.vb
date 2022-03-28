Imports System.Reflection
Imports WinPaletter.XenonCore

Public Class MainForm
    Enum WinVer
        Eleven
        Ten
    End Enum

    Private _Shown As Boolean = False

    Public CP, CP_Original, CP_FirstTime As CP
    Dim CP_BeforeDragAndDrop As CP

    Public PreviewConfig As WinVer = WinVer.Eleven
    Private ReorderAfterPreviewConfigChange As Boolean = False

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

    Sub Adjust_Preview()
        If _Shown Then My.Application.AnimatorX.HideSync(pnl_preview)

        Select Case PreviewConfig
            Case WinVer.Eleven
                ActionCenter.Size = New Size(120, 85)
                ActionCenter.Location = New Point(398, 161)
                ActionCenter.Dock = Nothing
                ActionCenter.RoundedCorners = True

                taskbar.Height = 42
                taskbar.UseItAsTaskbar_Version = XenonAcrylic.TaskbarVersion.Eleven

                start.Size = New Size(135, 200)
                start.Location = New Point(7, 46)
                start.RoundedCorners = True

                XenonWindow1.RoundedCorners = True
                XenonWindow2.RoundedCorners = True

            Case WinVer.Ten
                ActionCenter.Dock = DockStyle.Right
                ActionCenter.RoundedCorners = False

                taskbar.Height = 35
                taskbar.UseItAsTaskbar_Version = XenonAcrylic.TaskbarVersion.Ten

                start.Size = New Size(182, 201)
                start.Location = New Point(0, 59)
                start.RoundedCorners = False

                XenonWindow1.RoundedCorners = False
                XenonWindow2.RoundedCorners = False
        End Select

        XenonWindow1.Top = start.Top
        XenonWindow1.Left = start.Right + 5

        XenonWindow2.Top = XenonWindow1.Bottom + 5
        XenonWindow2.Left = XenonWindow1.Left

        XenonWindow1.Invalidate()
        XenonWindow2.Invalidate()

        ReValidateLivePreview(pnl_preview)

        If _Shown Then My.Application.AnimatorX.ShowSync(pnl_preview)
    End Sub

    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _Shown = False

        MakeItDoubleBuffered(Me)
        ApplyDarkMode(Me)

        Dim LabelsList As New List(Of Label)
        LabelsList.Add(Label1)
        LabelsList.Add(Label10)
        LabelsList.Add(Label17)
        LabelsList.Add(Label21)
        LabelsList.Add(themename_lbl)
        LabelsList.Add(author_lbl)
        LabelsList.Add(ColorPicker.Label1)
        LabelsList.Add(ColorPicker.Label2)
        LabelsList.Add(ColorPicker.Label3)
        LabelsList.Add(ColorPicker.Label5)

        For Each lbl As Label In LabelsList
            lbl.Font = New Font(If(My.W11, "Segoe UI Variable Static Text", "Segoe UI"), lbl.Font.Size, lbl.Font.Style)
        Next

        If My.Application._Settings.CustomPreviewConfig_Enabled Then
            PreviewConfig = My.Application._Settings.CustomPreviewConfig
        Else
            If My.W11 Then PreviewConfig = WinVer.Eleven Else PreviewConfig = WinVer.Ten
        End If

        pnl_preview.BackgroundImage = My.Application.Wallpaper
        Adjust_Preview()

        If Not My.Application.ExternalLink Then
            CP = New CP(CP.Mode.Registry)
        Else
            CP = New CP(CP.Mode.File, My.Application.ExternalLink_File)
            OpenFileDialog1.FileName = My.Application.ExternalLink_File
            SaveFileDialog1.FileName = My.Application.ExternalLink_File
            My.Application.ExternalLink = False
            My.Application.ExternalLink_File = ""
        End If

        CP_Original = CP
        CP_FirstTime = CP
        ApplyCPValues(CP)
        ApplyLivePreviewFromCP(CP)
    End Sub

    Private Sub MainForm_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        _Shown = True
    End Sub

    Protected Overrides Sub OnFormClosing(ByVal e As FormClosingEventArgs)
        Dim Changed As Boolean = (CP.GetHashCode <> CP_Original.GetHashCode)

        If e.CloseReason = CloseReason.UserClosing And Changed Then
            Select Case MsgBox("Current Palette Changed. Do you want to save the palette as a theme file (for the program) and apply it?", MsgBoxStyle.Question + MsgBoxStyle.YesNoCancel)
                Case DialogResult.Cancel
                    e.Cancel = True
                Case DialogResult.Yes

                    If Not IO.File.Exists(SaveFileDialog1.FileName) Then
                        If SaveFileDialog1.ShowDialog = DialogResult.OK Then
                            CP_Original = CP
                            CP.Save(CP.SavingMode.File, SaveFileDialog1.FileName)
                            CP.Save(CP.SavingMode.Registry)
                            RestartExplorer()
                        Else
                            e.Cancel = False
                            MyBase.OnFormClosing(e)
                            Exit Sub
                        End If
                    Else
                        CP_Original = CP
                        CP.Save(CP.SavingMode.File, SaveFileDialog1.FileName)
                        CP.Save(CP.SavingMode.Registry)
                        RestartExplorer()
                    End If

                    e.Cancel = False
                    MyBase.OnFormClosing(e)
                Case DialogResult.No
                    CP = CP_Original
                    CP.Save(CP.SavingMode.Registry)
                    RestartExplorer()
                    e.Cancel = False
                    MyBase.OnFormClosing(e)
            End Select
        ElseIf e.CloseReason = CloseReason.UserClosing And Not Changed Then
            e.Cancel = False
            MyBase.OnFormClosing(e)
        End If
    End Sub

    Sub ApplyLivePreviewFromCP(ByVal CP As CP)
        XenonWindow1.AccentColor_Enabled = CP.ApplyAccentonTitlebars
        XenonWindow2.AccentColor_Enabled = CP.ApplyAccentonTitlebars

        XenonWindow1.AccentColor_Active = CP.Titlebar_Active
        XenonWindow2.AccentColor_Active = CP.Titlebar_Active

        XenonWindow1.AccentColor_Inactive = CP.Titlebar_Inactive
        XenonWindow2.AccentColor_Inactive = CP.Titlebar_Inactive

        XenonWindow1.DarkMode = Not CP.AppMode_Light
        XenonWindow2.DarkMode = Not CP.AppMode_Light

        Dim AnimX1 As Integer = 30
        Dim AnimX2 As Integer = 1

        Visual.FadeColor(Label8, "Forecolor", Label8.ForeColor, If(CP.AppMode_Light, Color.Black, Color.White), AnimX1, AnimX2)

        Select Case PreviewConfig
            Case WinVer.Eleven
#Region "Win11"
                start.DarkMode = Not CP.WinMode_Light
                taskbar.DarkMode = Not CP.WinMode_Light
                ActionCenter.DarkMode = Not CP.WinMode_Light

                taskbar.Transparency = CP.Transparency
                start.Transparency = CP.Transparency
                ActionCenter.Transparency = CP.Transparency

                Select Case Not CP.WinMode_Light
                    Case True   ''''''''''Dark
                        lbl1.Text = "Start Menu, Taskbar and Action Center"
                        lbl2.Text = "Action Center Hover and Links"
                        lbl3.Text = "Lines, Toggles and Buttons"
                        lbl4.Text = "Taskbar Active App Background"

                        If ReorderAfterPreviewConfigChange Then
                            pnl1.Top = XenonGroupBox6.Bottom + 2
                            pnl2.Top = pnl1.Bottom + 2
                            pnl3.Top = pnl2.Bottom + 2
                            pnl4.Top = pnl3.Bottom + 2
                        End If

                        taskbar.BackColorAlpha = 75
                        start.BackColorAlpha = 175

                        If CP.ApplyAccentonTaskbar Then
                            ActionCenter.BackColorAlpha = 180
                            Visual.FadeColor(taskbar, "BackColor", taskbar.BackColor, Color.FromArgb(taskbar.BackColor.A, CP.StartListFolders_TaskbarFront), AnimX1, AnimX2)
                            Visual.FadeColor(start, "BackColor", start.BackColor, Color.FromArgb(start.BackColor.A, CP.StartListFolders_TaskbarFront), AnimX1, AnimX2)
                            Visual.FadeColor(ActionCenter, "BackColor", ActionCenter.BackColor, Color.FromArgb(ActionCenter.BackColor.A, CP.StartListFolders_TaskbarFront), AnimX1, AnimX2)
                        Else
                            ActionCenter.BackColorAlpha = 100
                            Visual.FadeColor(taskbar, "BackColor", taskbar.BackColor, Color.FromArgb(55, 55, 55), AnimX1, AnimX2)
                            Visual.FadeColor(start, "BackColor", start.BackColor, Color.FromArgb(40, 40, 40), AnimX1, AnimX2)
                            Visual.FadeColor(ActionCenter, "BackColor", ActionCenter.BackColor, Color.FromArgb(55, 55, 55), AnimX1, AnimX2)
                        End If

                        Visual.FadeColor(ActionCenter, "ActionCenterButton_Normal", ActionCenter.ActionCenterButton_Normal, CP.Taskbar_Icon_Underline, AnimX1, AnimX2)
                        Visual.FadeColor(ActionCenter, "ActionCenterButton_Hover", ActionCenter.ActionCenterButton_Hover, CP.ActionCenter_AppsLinks, AnimX1, AnimX2)
                        Visual.FadeColor(ActionCenter, "ActionCenterButton_Pressed", ActionCenter.ActionCenterButton_Pressed, CP.StartButton_Hover, AnimX1, AnimX2)
                        Visual.FadeColor(start, "SearchBoxAccent", start.SearchBoxAccent, CP.Taskbar_Icon_Underline, AnimX1, AnimX2)
                        Visual.FadeColor(taskbar, "AppUnderline", taskbar.AppUnderline, CP.Taskbar_Icon_Underline, AnimX1, AnimX2)

                        Visual.FadeColor(Label3, "Forecolor", Label3.ForeColor, CP.SettingsIconsAndLinks, AnimX1, AnimX2)
                        Visual.FadeColor(Label12, "Forecolor", Label12.ForeColor, CP.ActionCenter_AppsLinks, AnimX1, AnimX2)

                    Case False   ''''''''''Light
                        lbl1.Text = "Action Center Hover and Links"
                        lbl2.Text = "Start Menu and Action Center Colors"
                        lbl3.Text = "Taskbar Color"
                        lbl4.Text = "Lines, Toggles and Buttons"

                        If ReorderAfterPreviewConfigChange Then
                            pnl2.Top = XenonGroupBox6.Bottom + 2
                            pnl1.Top = pnl2.Bottom + 2
                            pnl4.Top = pnl1.Bottom + 2
                            pnl3.Top = pnl4.Bottom + 2
                        End If

                        taskbar.BackColorAlpha = 200
                        start.BackColorAlpha = 210
                        If CP.ApplyAccentonTaskbar Then
                            ActionCenter.BackColorAlpha = 180
                            Visual.FadeColor(taskbar, "BackColor", taskbar.BackColor, Color.FromArgb(taskbar.BackColor.A, CP.Taskbar_Icon_Underline), AnimX1, AnimX2)
                            Visual.FadeColor(start, "BackColor", start.BackColor, Color.FromArgb(start.BackColor.A, CP.ActionCenter_AppsLinks), AnimX1, AnimX2)
                            Visual.FadeColor(ActionCenter, "BackColor", ActionCenter.BackColor, Color.FromArgb(ActionCenter.BackColor.A, CP.ActionCenter_AppsLinks), AnimX1, AnimX2)
                        Else
                            ActionCenter.BackColorAlpha = 100
                            Visual.FadeColor(taskbar, "BackColor", taskbar.BackColor, Color.FromArgb(255, 255, 255), AnimX1, AnimX2)
                            Visual.FadeColor(start, "BackColor", start.BackColor, Color.FromArgb(255, 255, 255), AnimX1, AnimX2)
                            Visual.FadeColor(ActionCenter, "BackColor", ActionCenter.BackColor, Color.FromArgb(255, 255, 255), AnimX1, AnimX2)
                        End If

                        Visual.FadeColor(ActionCenter, "ActionCenterButton_Normal", ActionCenter.ActionCenterButton_Normal, CP.StartMenuBackground_ActiveTaskbarButton, AnimX1, AnimX2)
                        Visual.FadeColor(ActionCenter, "ActionCenterButton_Hover", ActionCenter.ActionCenterButton_Hover, CP.StartListFolders_TaskbarFront, AnimX1, AnimX2)
                        Visual.FadeColor(ActionCenter, "ActionCenterButton_Pressed", ActionCenter.ActionCenterButton_Pressed, CP.StartButton_Hover, AnimX1, AnimX2)
                        Visual.FadeColor(start, "SearchBoxAccent", start.SearchBoxAccent, CP.StartMenuBackground_ActiveTaskbarButton, AnimX1, AnimX2)
                        Visual.FadeColor(taskbar, "AppUnderline", taskbar.AppUnderline, CP.StartMenuBackground_ActiveTaskbarButton, AnimX1, AnimX2)

                        Visual.FadeColor(Label3, "Forecolor", Label3.ForeColor, CP.SettingsIconsAndLinks, AnimX1, AnimX2)
                        Visual.FadeColor(Label12, "Forecolor", Label12.ForeColor, CP.StartListFolders_TaskbarFront, AnimX1, AnimX2)
                End Select
#End Region
            Case WinVer.Ten
#Region "Win10"
                start.DarkMode = Not CP.WinMode_Light
                taskbar.DarkMode = Not CP.WinMode_Light
                ActionCenter.DarkMode = Not CP.WinMode_Light

                taskbar.Transparency = CP.Transparency
                start.Transparency = CP.Transparency
                ActionCenter.Transparency = CP.Transparency

                lbl6.Text = "Start Icon Hover"

                If CP.WinMode_Light And Not CP.ApplyAccentonTaskbar Then
                    lbl1.Text = ""
                    lbl2.Text = ""
                    lbl3.Text = ""
                    lbl4.Text = "Start Menu and Action Center Colors"
                    lbl5.Text = "Settings Icons, Taskbar App Underline & Some Pressed Buttons"
                    lbl7.Text = ""
                    lbl8.Text = "Links"

                    If ReorderAfterPreviewConfigChange Then
                        pnl4.Top = XenonGroupBox6.Bottom + 2
                        pnl5.Top = pnl4.Bottom + 2
                        pnl8.Top = pnl5.Bottom + 2
                        pnl6.Top = pnl8.Bottom + 2
                        pnl1.Top = pnl6.Bottom + 2
                        pnl2.Top = pnl1.Bottom + 2
                        pnl3.Top = pnl2.Bottom + 2
                        pnl7.Top = pnl3.Bottom + 2
                    End If

                Else
                    If CP.Transparency Then
                        lbl1.Text = ""
                        lbl2.Text = "Links"
                        lbl3.Text = "Taskbar App Underline"
                        lbl4.Text = "Start Menu and Action Center Colors"
                        lbl5.Text = "Settings Icons, Text Selection, Focus Dots, Some Pressed Buttons"
                        lbl7.Text = ""
                        lbl8.Text = "Taskbar Color"

                        If ReorderAfterPreviewConfigChange Then
                            pnl4.Top = XenonGroupBox6.Bottom + 2
                            pnl8.Top = pnl4.Bottom + 2
                            pnl3.Top = pnl8.Bottom + 2
                            pnl5.Top = pnl3.Bottom + 2
                            pnl2.Top = pnl5.Bottom + 2
                            pnl6.Top = pnl2.Bottom + 2
                            pnl1.Top = pnl6.Bottom + 2
                            pnl7.Top = pnl1.Bottom + 2
                        End If

                    Else
                        lbl1.Text = "Taskbar Color"
                        lbl2.Text = "Links"
                        lbl3.Text = "Taskbar App Underline"
                        lbl4.Text = "Start Menu, Action Center, Taskbar Active App Background"
                        lbl5.Text = "Settings Icons, Text Selection, Focus Dots, Some Pressed Buttons"
                        lbl7.Text = ""
                        lbl8.Text = ""

                        If ReorderAfterPreviewConfigChange Then
                            pnl4.Top = XenonGroupBox6.Bottom + 2
                            pnl1.Top = pnl4.Bottom + 2
                            pnl3.Top = pnl1.Bottom + 2
                            pnl5.Top = pnl3.Bottom + 2
                            pnl2.Top = pnl5.Bottom + 2
                            pnl6.Top = pnl2.Bottom + 2
                            pnl7.Top = pnl6.Bottom + 2
                            pnl8.Top = pnl7.Bottom + 2
                        End If
                    End If
                End If

                taskbar.AppUnderline = ControlPaint.Light(Color.FromArgb(255, CP.Taskbar_Icon_Underline))

                If CP.Transparency Then
                    taskbar.BackColorAlpha = 200
                    start.BackColorAlpha = 210
                    ActionCenter.BackColorAlpha = 100
                Else
                    taskbar.BackColorAlpha = 255
                    start.BackColorAlpha = 255
                    ActionCenter.BackColorAlpha = 255
                End If

                If CP.WinMode_Light And Not CP.ApplyAccentonTaskbar Then
                    Visual.FadeColor(taskbar, "BackColor", taskbar.BackColor, Color.FromArgb(237, 237, 237), AnimX1, AnimX2)
                    Visual.FadeColor(start, "BackColor", start.BackColor, Color.FromArgb(227, 227, 227), AnimX1, AnimX2)
                    Visual.FadeColor(ActionCenter, "BackColor", ActionCenter.BackColor, Color.FromArgb(227, 227, 227), AnimX1, AnimX2)
                    Visual.FadeColor(taskbar, "StartColor", taskbar.StartColor, Color.Transparent, AnimX1, AnimX2)
                Else
                    If Not CP.ApplyAccentonTaskbar Then
                        Visual.FadeColor(taskbar, "BackColor", taskbar.BackColor, Color.FromArgb(23, 23, 23), AnimX1, AnimX2)
                        Visual.FadeColor(start, "BackColor", start.BackColor, Color.FromArgb(36, 36, 36), AnimX1, AnimX2)
                        Visual.FadeColor(ActionCenter, "BackColor", ActionCenter.BackColor, Color.FromArgb(36, 36, 36), AnimX1, AnimX2)
                        Visual.FadeColor(taskbar, "StartColor", taskbar.StartColor, Color.Transparent, AnimX1, AnimX2)

                    Else
                        Visual.FadeColor(start, "BackColor", start.BackColor, CP.StartMenuBackground_ActiveTaskbarButton, AnimX1, AnimX2)
                        Visual.FadeColor(taskbar, "StartColor", taskbar.StartColor, CP.StartMenuBackground_ActiveTaskbarButton, AnimX1, AnimX2)
                        Visual.FadeColor(ActionCenter, "BackColor", ActionCenter.BackColor, CP.StartMenuBackground_ActiveTaskbarButton, AnimX1, AnimX2)

                        If CP.Transparency Then
                            Visual.FadeColor(taskbar, "BackColor", taskbar.BackColor, CP.Taskbar_Background, AnimX1, AnimX2)
                        Else
                            Visual.FadeColor(taskbar, "BackColor", taskbar.BackColor, CP.StartListFolders_TaskbarFront, AnimX1, AnimX2)
                        End If

                    End If
                End If

                If CP.WinMode_Light And Not CP.ApplyAccentonTaskbar Then
                    Visual.FadeColor(Label3, "Forecolor", Label3.ForeColor, CP.SettingsIconsAndLinks, AnimX1, AnimX2)
                    Visual.FadeColor(Label12, "Forecolor", Label12.ForeColor, CP.Taskbar_Background, AnimX1, AnimX2)
                    Visual.FadeColor(ActionCenter, "LinkColor", ActionCenter.LinkColor, CP.Taskbar_Background, AnimX1, AnimX2)
                    Visual.FadeColor(taskbar, "AppBackground", taskbar.AppBackground, Color.Transparent, AnimX1, AnimX2)
                Else
                    Visual.FadeColor(Label3, "Forecolor", Label3.ForeColor, CP.SettingsIconsAndLinks, AnimX1, AnimX2)
                    Visual.FadeColor(Label12, "Forecolor", Label12.ForeColor, CP.ActionCenter_AppsLinks, AnimX1, AnimX2)
                    Visual.FadeColor(taskbar, "AppUnderline", taskbar.AppUnderline, CP.Taskbar_Icon_Underline, AnimX1, AnimX2)
                    Visual.FadeColor(ActionCenter, "LinkColor", ActionCenter.LinkColor, CP.Taskbar_Icon_Underline, AnimX1, AnimX2)

                    If Not CP.Transparency And Not CP.ApplyAccentonTaskbar Then
                        Visual.FadeColor(taskbar, "AppBackground", taskbar.AppBackground, Color.Transparent, AnimX1, AnimX2)
                    Else
                        Visual.FadeColor(taskbar, "AppBackground", taskbar.AppBackground, CP.StartMenuBackground_ActiveTaskbarButton, AnimX1, AnimX2)
                    End If
                End If

                ReValidateLivePreview(pnl_preview)
#End Region
        End Select
    End Sub

    Sub ReValidateLivePreview(ByVal Parent As Control)
        Parent.Invalidate()

        For Each ctrl As Control In Parent.Controls
            If ctrl.HasChildren Then
                For Each c As Control In ctrl.Controls
                    ReValidateLivePreview(c)
                Next
            End If
        Next
    End Sub

    Private Sub XenonGroupBox10_Click(sender As Object, e As EventArgs) Handles ActiveTitlebar_picker.Click
        Dim CList As New List(Of Control)
        CList.Add(sender)
        CList.Add(XenonWindow1)

        Dim C As Color = ColorPicker.Pick(CList)
        CP.Titlebar_Active = Color.FromArgb(255, C)
        ApplyLivePreviewFromCP(CP)

        CList.Clear()
        CList = Nothing

        If Not CP.ApplyAccentonTitlebars Then
            Notify("To colorize active titlebar, please activate the toggle", My.Resources.notify_info, 5000)
        End If

    End Sub

    Private Sub XenonGroupBox21_Click(sender As Object, e As EventArgs) Handles InactiveTitlebar_picker.Click
        Dim CList As New List(Of Control)
        CList.Add(sender)
        CList.Add(XenonWindow2)

        Dim C As Color = ColorPicker.Pick(CList, True)
        CP.Titlebar_Inactive = Color.FromArgb(255, C)
        ApplyLivePreviewFromCP(CP)

        CList.Clear()
        CList = Nothing

        If Not CP.ApplyAccentonTitlebars Then
            Notify("To colorize inactive titlebar, please activate the toggle", My.Resources.notify_info, 5000)
        End If
    End Sub

    Private Sub WinMode_Toggle_CheckedChanged(sender As Object, e As EventArgs) Handles WinMode_Toggle.CheckedChanged
        CP.WinMode_Light = Not sender.Checked
        ApplyLivePreviewFromCP(CP)
    End Sub

    Private Sub AppMode_Toggle_CheckedChanged(sender As Object, e As EventArgs) Handles AppMode_Toggle.CheckedChanged
        CP.AppMode_Light = Not sender.Checked
        ApplyLivePreviewFromCP(CP)
    End Sub

    Private Sub Transparency_Toggle_CheckedChanged(sender As Object, e As EventArgs) Handles Transparency_Toggle.CheckedChanged
        CP.Transparency = sender.Checked
        ApplyLivePreviewFromCP(CP)
    End Sub

    Private Sub Blur_Toggle_CheckedChanged(sender As Object, e As EventArgs) Handles Blur_Toggle.CheckedChanged
        CP.Blur = sender.Checked
        ApplyLivePreviewFromCP(CP)
    End Sub

    Private Sub ShowAccentOnTitlebarAndBorders_Toggle_CheckedChanged(sender As Object, e As EventArgs) Handles ShowAccentOnTitlebarAndBorders_Toggle.CheckedChanged
        CP.ApplyAccentonTitlebars = sender.Checked
        ApplyLivePreviewFromCP(CP)
    End Sub

    Private Sub AccentOnStartAndTaskbar_Toggle_CheckedChanged(sender As Object, e As EventArgs) Handles AccentOnStartAndTaskbar_Toggle.CheckedChanged
        CP.ApplyAccentonTaskbar = sender.Checked
        ApplyLivePreviewFromCP(CP)
    End Sub

    Private Sub TaskbarIconUnderline_picker_Click(sender As Object, e As EventArgs) Handles TaskbarIconUnderline_picker.Click
        Dim CList As New List(Of Control)
        Dim C As Color
        CList.Add(sender)

        If PreviewConfig = WinVer.Eleven Then
            CList.Add(taskbar)

            If Not CP.WinMode_Light Then
                CList.Add(ActionCenter)
                CList.Add(start)
                CList.Add(ShowAccentOnTitlebarAndBorders_Toggle)
                CList.Add(WinMode_Toggle)
                CList.Add(AppMode_Toggle)
                CList.Add(Transparency_Toggle)
                CList.Add(AccentOnStartAndTaskbar_Toggle)
                C = ColorPicker.Pick(CList, False, True, False, False, True, True)
            Else
                C = ColorPicker.Pick(CList)
            End If
        Else
            If CP.WinMode_Light And Not CP.ApplyAccentonTaskbar Then
            Else
                CList.Add(taskbar)
                C = ColorPicker.Pick(CList, False, True)
            End If
        End If

        CP.Taskbar_Icon_Underline = Color.FromArgb(255, C)
        ApplyLivePreviewFromCP(CP)

        If Not CP.WinMode_Light Then
            ShowAccentOnTitlebarAndBorders_Toggle.ColorPalette.BaseColor = sender.BackColor
            ShowAccentOnTitlebarAndBorders_Toggle.Invalidate()
            WinMode_Toggle.ColorPalette.BaseColor = sender.BackColor
            WinMode_Toggle.Invalidate()
            AppMode_Toggle.ColorPalette.BaseColor = sender.BackColor
            AppMode_Toggle.Invalidate()
            Transparency_Toggle.ColorPalette.BaseColor = sender.BackColor
            Transparency_Toggle.Invalidate()
            AccentOnStartAndTaskbar_Toggle.ColorPalette.BaseColor = sender.BackColor
            AccentOnStartAndTaskbar_Toggle.Invalidate()
        End If

        CList.Clear()
        CList = Nothing

        If PreviewConfig = WinVer.Ten Then
            If CP.WinMode_Light And Not CP.ApplyAccentonTaskbar Then
                Notify("To colorize taskbar, please activate the toggle", My.Resources.notify_info, 5000)
            End If
        End If
    End Sub

    Private Sub TaskbarFrontAndFoldersOnStart_picker_Click(sender As Object, e As EventArgs) Handles TaskbarFrontAndFoldersOnStart_picker.Click
        Dim CList As New List(Of Control)
        CList.Add(sender)

        If PreviewConfig = WinVer.Eleven Then
            If Not CP.WinMode_Light Then
                CList.Add(taskbar)
                CList.Add(start)
                CList.Add(ActionCenter)
            Else
                CList.Add(Label12)
            End If
        Else
            If Not CP.Transparency Then
                If Not CP.WinMode_Light Then
                    CList.Add(taskbar)
                Else
                    If CP.ApplyAccentonTaskbar Then CList.Add(taskbar)
                End If
            End If
        End If

        Dim C As Color = ColorPicker.Pick(CList)
        CP.StartListFolders_TaskbarFront = Color.FromArgb(255, C)
        ApplyLivePreviewFromCP(CP)

        CList.Clear()
        CList = Nothing

        If PreviewConfig = WinVer.Eleven Then
            If Not CP.WinMode_Light And Not CP.ApplyAccentonTaskbar Then
                Notify("To colorize start menu, action center and taskbar, please activate the toggle", My.Resources.notify_info, 5000)
            End If
        Else
            If Not CP.Transparency And Not CP.WinMode_Light And Not CP.ApplyAccentonTaskbar Then
                Notify("To colorize taskbar, please activate the toggle", My.Resources.notify_info, 5000)
            End If
        End If

    End Sub

    Private Sub ActionCenter_picker_Click(sender As Object, e As EventArgs) Handles ActionCenter_picker.Click
        Dim CList As New List(Of Control)
        Dim C As Color

        CList.Add(sender)

        If PreviewConfig = WinVer.Eleven Then
            If CP.WinMode_Light Then
                CList.Add(start)
                CList.Add(ActionCenter)
                C = ColorPicker.Pick(CList)
            Else
                CList.Add(Label12)
                C = ColorPicker.Pick(CList, False, True)
            End If
        Else
            If CP.WinMode_Light And Not CP.ApplyAccentonTaskbar Then

            Else
                CList.Add(ActionCenter)
                CList.Add(Label12)
                C = ColorPicker.Pick(CList, False, False, False, False, False, False, True)
            End If
        End If

        CP.ActionCenter_AppsLinks = Color.FromArgb(255, C)
        ApplyLivePreviewFromCP(CP)

        CList.Clear()
        CList = Nothing

        If PreviewConfig = WinVer.Ten Then
            If CP.WinMode_Light And Not CP.ApplyAccentonTaskbar Then
                Notify("To colorize start menu and action center, please activate the toggle", My.Resources.notify_info, 5000)
            End If
        End If
    End Sub

    Private Sub SettingsIconsAndLinks_picker_Click(sender As Object, e As EventArgs) Handles SettingsIconsAndLinks_picker.Click
        Dim CList As New List(Of Control)
        Dim C As Color

        CList.Add(sender)

        If PreviewConfig = WinVer.Eleven Then
            CList.Add(Label3)
            C = ColorPicker.Pick(CList)
        Else
            If CP.WinMode_Light And Not CP.ApplyAccentonTaskbar Then
                CList.Add(Label3)
                CList.Add(taskbar)
                C = ColorPicker.Pick(CList, False, True)
            Else
                CList.Add(Label3)
                C = ColorPicker.Pick(CList)
            End If
        End If

        CP.SettingsIconsAndLinks = Color.FromArgb(255, C)
        ApplyLivePreviewFromCP(CP)

        CList.Clear()
        CList = Nothing
    End Sub

    Private Sub XenonButton4_Click(sender As Object, e As EventArgs) Handles apply_btn.Click
        CP.Save(CP.SavingMode.Registry)
        RestartExplorer()
    End Sub

    Private Sub XenonButton4_MouseEnter(sender As Object, e As EventArgs) Handles apply_btn.MouseEnter
        status_lbl.Text = "This will restart the explorer, don't worry it won't close your work."
    End Sub

    Private Sub XenonButton4_MouseLeave(sender As Object, e As EventArgs) Handles apply_btn.MouseLeave
        status_lbl.Text = ""
    End Sub

    Private Sub XenonGroupBox12_Click(sender As Object, e As EventArgs) Handles TaskbarBackground_Picker.Click
        Dim CList As New List(Of Control)
        Dim C As Color

        CList.Add(sender)

        If PreviewConfig = WinVer.Eleven Then
            CList.Add(taskbar)
            C = ColorPicker.Pick(CList, False, False, True)
        Else

            If CP.WinMode_Light Then
                If CP.Transparency Then
                    If CP.ApplyAccentonTaskbar Then CList.Add(taskbar) Else CList.Add(Label12)
                Else
                    CList.Add(Label12)
                End If
            Else
                If CP.Transparency Then CList.Add(taskbar)
            End If

            C = ColorPicker.Pick(CList)
        End If

        CP.Taskbar_Background = Color.FromArgb(255, C)
        ApplyLivePreviewFromCP(CP)

        CList.Clear()
        CList = Nothing

        If PreviewConfig = WinVer.Ten Then
            If CP.WinMode_Light And Not CP.ApplyAccentonTaskbar Then

            Else
                If CP.Transparency Then
                    If Not CP.ApplyAccentonTaskbar Then
                        Notify("To colorize taskbar, please activate the toggle", My.Resources.notify_info, 5000)
                    End If
                Else

                End If
            End If
        End If
    End Sub

    Private Sub StartAccent_picker_Click(sender As Object, e As EventArgs) Handles StartAccent_picker.Click
        Dim CList As New List(Of Control)
        CList.Add(sender)

        If PreviewConfig = WinVer.Eleven Then
            CList.Add(start)
        Else

        End If

        Dim C As Color = ColorPicker.Pick(CList)
        CP.StartMenu_Accent = Color.FromArgb(255, C)
        ApplyLivePreviewFromCP(CP)

        CList.Clear()
        CList = Nothing
    End Sub

    Private Sub StartButtonHover_picker_Click(sender As Object, e As EventArgs) Handles StartButtonHover_picker.Click
        Dim CList As New List(Of Control)
        CList.Add(sender)

        Dim C As Color = ColorPicker.Pick(CList)
        CP.StartButton_Hover = Color.FromArgb(255, C)
        ApplyLivePreviewFromCP(CP)

        CList.Clear()
        CList = Nothing
    End Sub

    Private Sub StartBackgroundAndTaskbarButton_picker_Click(sender As Object, e As EventArgs) Handles StartBackgroundAndTaskbarButton_picker.Click
        Dim CList As New List(Of Control)
        CList.Add(sender)
        Dim C As Color

        If PreviewConfig = WinVer.Eleven Then

            If Not Transparency_Toggle.Checked Then
                CList.Add(taskbar)
                CList.Add(start)
                CList.Add(ActionCenter)
            End If

            If CP.WinMode_Light Then
                CList.Add(start)
                CList.Add(ShowAccentOnTitlebarAndBorders_Toggle)
                CList.Add(WinMode_Toggle)
                CList.Add(AppMode_Toggle)
                CList.Add(Transparency_Toggle)
                CList.Add(AccentOnStartAndTaskbar_Toggle)
                CList.Add(ActionCenter)
                CList.Add(taskbar)
                C = ColorPicker.Pick(CList, False, True, False, False, True, True)
            Else
                C = ColorPicker.Pick(CList, False, False, False, True)
            End If

        Else
            If CP.Transparency Then
                CList.Add(start)
                CList.Add(ActionCenter)
                C = ColorPicker.Pick(CList)
            Else
                CList.Add(start)
                CList.Add(ActionCenter)
                CList.Add(taskbar)
                C = ColorPicker.Pick(CList, False, False, True)
            End If
        End If

        CP.StartMenuBackground_ActiveTaskbarButton = Color.FromArgb(255, C)
        ApplyLivePreviewFromCP(CP)

        If PreviewConfig = WinVer.Eleven Then
            If CP.WinMode_Light Then
                ShowAccentOnTitlebarAndBorders_Toggle.ColorPalette.BaseColor = sender.BackColor
                ShowAccentOnTitlebarAndBorders_Toggle.Invalidate()
                WinMode_Toggle.ColorPalette.BaseColor = sender.BackColor
                WinMode_Toggle.Invalidate()
                AppMode_Toggle.ColorPalette.BaseColor = sender.BackColor
                AppMode_Toggle.Invalidate()
                Transparency_Toggle.ColorPalette.BaseColor = sender.BackColor
                Transparency_Toggle.Invalidate()
                AccentOnStartAndTaskbar_Toggle.ColorPalette.BaseColor = sender.BackColor
                AccentOnStartAndTaskbar_Toggle.Invalidate()
            End If
        Else

        End If

        CList.Clear()
        CList = Nothing

        If PreviewConfig = WinVer.Ten Then
            If Not CP.ApplyAccentonTaskbar Then
                Notify("To colorize start menu and action center, please activate the toggle", My.Resources.notify_info, 5000)
            End If
        End If
    End Sub

    Private Sub XenonButton8_Click(sender As Object, e As EventArgs) Handles XenonButton8.Click
        PreviewConfig = WinVer.Ten
        Adjust_Preview()
    End Sub

    Private Sub XenonButton1_Click(sender As Object, e As EventArgs) Handles XenonButton1.Click
        PreviewConfig = WinVer.Eleven
        Adjust_Preview()
    End Sub

    Dim wpth_or_wpsf As Boolean = True

    Private Sub Me_DragEnter(ByVal sender As Object, ByVal e As DragEventArgs) Handles Me.DragEnter, XenonGroupBox8.DragEnter, pnl_preview.DragEnter,
        PaletteContainer.DragEnter, XenonGroupBox5.DragEnter, XenonGroupBox1.DragEnter, XenonGroupBox2.DragEnter, XenonGroupBox13.DragEnter

        Text = My.Application._Settings.DragAndDropPreview

        Dim files() As String = e.Data.GetData(DataFormats.FileDrop)

        If My.Computer.FileSystem.GetFileInfo(files(0)).Extension.ToLower = ".wpth" Then
            wpth_or_wpsf = True
            e.Effect = DragDropEffects.Copy
            If My.Application._Settings.DragAndDropPreview Then
                DragAccepted = True
                CP_BeforeDragAndDrop = CP
                BlurForm(Me)
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

    Private Sub MainForm_DragLeave(sender As Object, e As EventArgs) Handles Me.DragLeave
        If DragAccepted Then
            If My.Application._Settings.DragAndDropPreview Then dragPreviewer.Close()
            CP = CP_BeforeDragAndDrop
            ApplyCPValues(CP_BeforeDragAndDrop)
            ApplyLivePreviewFromCP(CP_BeforeDragAndDrop)
            If My.Application._Settings.DragAndDropPreview Then ReleaseBlur()
        End If
    End Sub

    Private Sub MainForm_DragOver(sender As Object, e As DragEventArgs) Handles Me.DragOver, XenonGroupBox8.DragOver, pnl_preview.DragOver,
        PaletteContainer.DragOver, XenonGroupBox5.DragOver, XenonGroupBox1.DragOver, XenonGroupBox2.DragOver, XenonGroupBox13.DragOver
        If DragAccepted And My.Application._Settings.DragAndDropPreview Then dragPreviewer.Location = New Point(e.X + 15, e.Y + 15)
    End Sub

    Private Sub MainForm_DragDrop(sender As Object, e As DragEventArgs) Handles Me.DragDrop, XenonGroupBox8.DragDrop, pnl_preview.DragDrop,
        PaletteContainer.DragDrop, XenonGroupBox5.DragDrop, XenonGroupBox1.DragDrop, XenonGroupBox2.DragDrop, XenonGroupBox13.DragDrop

        If DragAccepted Then
            Dim files() As String = e.Data.GetData(DataFormats.FileDrop)

            If wpth_or_wpsf Then
                If My.Application._Settings.DragAndDropPreview Then dragPreviewer.Close()

                If Not CP.GetHashCode = CP_Original.GetHashCode Then
                    Select Case MsgBox("Current Palette Changed. Do you want to save the palette as a theme file (for the program)?", MsgBoxStyle.Question + MsgBoxStyle.YesNoCancel)
                        Case MsgBoxResult.Yes
                            If Not IO.File.Exists(SaveFileDialog1.FileName) Then
                                If SaveFileDialog1.ShowDialog = DialogResult.OK Then
                                    CP_Original = CP
                                    CP.Save(CP.SavingMode.File, SaveFileDialog1.FileName)
                                Else
                                    If My.Application._Settings.DragAndDropPreview Then ReleaseBlur()
                                    Exit Sub
                                End If
                            Else
                                CP_Original = CP
                                CP.Save(CP.SavingMode.File, SaveFileDialog1.FileName)
                            End If

                        Case MsgBoxResult.Cancel
                            If My.Application._Settings.DragAndDropPreview Then ReleaseBlur()
                            Exit Sub
                    End Select
                End If

                CP = New CP(CP.Mode.File, files(0))
                ApplyCPValues(CP)
                ApplyLivePreviewFromCP(CP)
                If My.Application._Settings.DragAndDropPreview Then ReleaseBlur()
            Else
                SettingsX._External = True
                SettingsX._File = files(0)
                SettingsX.Show()
            End If
        End If

    End Sub

    Dim DragAccepted As Boolean
    Dim P_Blur As New PictureBox With {.Dock = DockStyle.Fill, .SizeMode = PictureBoxSizeMode.StretchImage, .Visible = False}
    Dim b As Bitmap
    Dim bblur As Bitmap

    Sub BlurForm(ByVal [Form] As Form)
        'Dim P_Blur As New PictureBox With {.Dock = DockStyle.Fill, .SizeMode = PictureBoxSizeMode.StretchImage}
        If My.Application._Settings.DragAndDropPreview Then
            b = TakeScreenshot(Me)
            bblur = BlurBitmap(b, 1)
            P_Blur.Image = bblur
            [Form].Controls.Add(P_Blur)
            P_Blur.BringToFront()
            status_lbl.SendToBack()

            My.Application.AnimatorX.ShowSync(P_Blur)
        End If
    End Sub

    Sub ReleaseBlur()
        If My.Application._Settings.DragAndDropPreview Then
            My.Application.AnimatorX.HideSync(P_Blur)
            Me.Controls.Remove(P_Blur)
            P_Blur.Visible = False
            'P_Blur.Dispose()
        End If
    End Sub

    Public Shared Function TakeScreenshot(ByVal window As Form) As Bitmap
        Dim b = New Bitmap(window.Width, window.Height)
        window.DrawToBitmap(b, New Rectangle(0, 0, window.Width, window.Height))
        Dim p As Point = window.PointToScreen(Point.Empty)
        Dim target As Bitmap = New Bitmap(window.ClientSize.Width, window.ClientSize.Height)

        Using g As Graphics = Graphics.FromImage(target)
            g.DrawImage(b, 0, 0, New Rectangle(p.X - window.Location.X, p.Y - window.Location.Y, target.Width, target.Height), GraphicsUnit.Pixel)
        End Using

        b.Dispose()
        Return target
    End Function

    Sub ApplyCPValues(ByVal ColorPalette As CP)
        themename_lbl.Text = String.Format("{0} ({1})", CP.PaletteName, CP.PaletteVersion)
        author_lbl.Text = String.Format("By: {0}", CP.Author)

        WinMode_Toggle.Checked = Not ColorPalette.WinMode_Light
        AppMode_Toggle.Checked = Not ColorPalette.AppMode_Light
        Transparency_Toggle.Checked = ColorPalette.Transparency
        Blur_Toggle.Checked = ColorPalette.Blur
        ShowAccentOnTitlebarAndBorders_Toggle.Checked = ColorPalette.ApplyAccentonTitlebars
        AccentOnStartAndTaskbar_Toggle.Checked = ColorPalette.ApplyAccentonTaskbar
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

        LogonUI_Background_Picker.BackColor = ColorPalette.LogonUI_Background
        LogonUI_PersonalColorsBackground_Picker.BackColor = ColorPalette.LogonUI_PersonalColors_Background
        LogonUI_PersonalColorsAccent_Picker.BackColor = ColorPalette.LogonUI_PersonalColors_Accent
        LogonUI_Acrylic_Toggle.Checked = Not ColorPalette.LogonUI_DisableAcrylicBackgroundOnLogon
        LogonUI_Background_Toggle.Checked = Not ColorPalette.LogonUI_DisableLogonBackgroundImage
        LogonUI_Lockscreen_Toggle.Checked = Not ColorPalette.LogonUI_NoLockScreen
#Region "LogonUI"

#End Region
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

            If Not CP.GetHashCode = CP_Original.GetHashCode Then
                Select Case MsgBox("Current Palette Changed. Do you want to save the palette as a theme file (for the program)?", MsgBoxStyle.Question + MsgBoxStyle.YesNoCancel)
                    Case MsgBoxResult.Yes
                        If Not IO.File.Exists(SaveFileDialog1.FileName) Then
                            If SaveFileDialog1.ShowDialog = DialogResult.OK Then
                                CP_Original = CP
                                CP.Save(CP.SavingMode.File, SaveFileDialog1.FileName)
                            Else
                                Exit Sub
                            End If
                        Else
                            CP_Original = CP
                            CP.Save(CP.SavingMode.File, SaveFileDialog1.FileName)
                        End If

                    Case MsgBoxResult.Cancel
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
            CP.Save(CP.SavingMode.File, SaveFileDialog1.FileNames(0))
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

    Dim NotificationsList As New List(Of Notifier)
    Dim WithEvents NTimer As New Timer With {.Enabled = False, .Interval = 100}

    Private Sub ActiveTitlebar_picker_Paint(sender As Object, e As PaintEventArgs) Handles ActiveTitlebar_picker.Paint

    End Sub

    Private Sub XenonButton3_Click(sender As Object, e As EventArgs) Handles XenonButton3.Click
        ContextMenuStrip1.Show(sender, TryCast(sender, Control).Location + New Point(15, 15))
    End Sub

    Private Sub FromCurrentPaletteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FromCurrentPaletteToolStripMenuItem.Click

        If Not CP.GetHashCode = CP_Original.GetHashCode Then
            Select Case MsgBox("Current Palette Changed. Do you want to save the palette as a theme file (for the program)?", MsgBoxStyle.Question + MsgBoxStyle.YesNoCancel)
                Case MsgBoxResult.Yes
                    If Not IO.File.Exists(SaveFileDialog1.FileName) Then
                        If SaveFileDialog1.ShowDialog = DialogResult.OK Then
                            CP_Original = CP
                            CP.Save(CP.SavingMode.File, SaveFileDialog1.FileName)
                        Else
                            Exit Sub
                        End If
                    Else
                        CP_Original = CP
                        CP.Save(CP.SavingMode.File, SaveFileDialog1.FileName)
                    End If

                Case MsgBoxResult.Cancel
                    Exit Sub
            End Select
        End If

        CP = New CP(CP.Mode.Registry)
        CP_Original = CP
        OpenFileDialog1.FileName = Nothing
        SaveFileDialog1.FileName = Nothing
        ApplyCPValues(CP)
        ApplyLivePreviewFromCP(CP)
    End Sub

    Private Sub ToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem2.Click
        If Not CP.GetHashCode = CP_Original.GetHashCode Then
            Select Case MsgBox("Current Palette Changed. Do you want to save the palette as a theme file (for the program)?", MsgBoxStyle.Question + MsgBoxStyle.YesNoCancel)
                Case MsgBoxResult.Yes
                    If Not IO.File.Exists(SaveFileDialog1.FileName) Then
                        If SaveFileDialog1.ShowDialog = DialogResult.OK Then
                            CP_Original = CP
                            CP.Save(CP.SavingMode.File, SaveFileDialog1.FileName)
                        Else
                            Exit Sub
                        End If
                    Else
                        CP_Original = CP
                        CP.Save(CP.SavingMode.File, SaveFileDialog1.FileName)
                    End If

                Case MsgBoxResult.Cancel
                    Exit Sub
            End Select
        End If

        CP = New CP(CP.Mode.Init)
        CP_Original = CP
        OpenFileDialog1.FileName = Nothing
        SaveFileDialog1.FileName = Nothing
        ApplyCPValues(CP)
        ApplyLivePreviewFromCP(CP)
    End Sub

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        If Not CP.GetHashCode = CP_Original.GetHashCode Then
            Select Case MsgBox("Current Palette Changed. Do you want to save the palette as a theme file (for the program)?", MsgBoxStyle.Question + MsgBoxStyle.YesNoCancel)
                Case MsgBoxResult.Yes
                    If Not IO.File.Exists(SaveFileDialog1.FileName) Then
                        If SaveFileDialog1.ShowDialog = DialogResult.OK Then
                            CP_Original = CP
                            CP.Save(CP.SavingMode.File, SaveFileDialog1.FileName)
                        Else
                            Exit Sub
                        End If
                    Else
                        CP_Original = CP
                        CP.Save(CP.SavingMode.File, SaveFileDialog1.FileName)
                    End If

                Case MsgBoxResult.Cancel
                    Exit Sub
            End Select
        End If

        If My.W11 Then IO.File.WriteAllBytes("temp.wpth", My.Resources.W11_init) Else IO.File.WriteAllBytes("temp.wpth", My.Resources.W10_init)
        CP = New CP(CP.Mode.File, "temp.wpth")
        Kill("temp.wpth")
        CP_Original = CP
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
        Updates.Show()
    End Sub

    Private Sub XenonButton11_Click(sender As Object, e As EventArgs) Handles XenonButton11.Click
        SettingsX.Show()
    End Sub

    Private Sub LogonUI_Acrylic_Toggle_CheckedChanged(sender As Object, e As EventArgs) Handles LogonUI_Acrylic_Toggle.CheckedChanged
        CP.LogonUI_DisableAcrylicBackgroundOnLogon = Not sender.Checked
        ApplyLivePreviewFromCP(CP)
    End Sub

    Private Sub LogonUI_Background_Toggle_CheckedChanged(sender As Object, e As EventArgs) Handles LogonUI_Background_Toggle.CheckedChanged
        CP.LogonUI_DisableLogonBackgroundImage = Not sender.Checked
        ApplyLivePreviewFromCP(CP)
    End Sub

    Private Sub LogonUI_Lockscreen_Toggle_CheckedChanged(sender As Object, e As EventArgs) Handles LogonUI_Lockscreen_Toggle.CheckedChanged
        CP.LogonUI_NoLockScreen = Not sender.Checked
        ApplyLivePreviewFromCP(CP)
    End Sub

    Private Sub LogonUI_Background_Picker_Click(sender As Object, e As EventArgs) Handles LogonUI_Background_Picker.Click
        Dim CList As New List(Of Control)
        CList.Add(sender)

        Dim C As Color = ColorPicker.Pick(CList)
        CP.LogonUI_Background = Color.FromArgb(255, C)
        ApplyLivePreviewFromCP(CP)

        CList.Clear()
        CList = Nothing
    End Sub

    Private Sub LogonUI_PersonalColorsBackground_Picker_Click(sender As Object, e As EventArgs) Handles LogonUI_PersonalColorsBackground_Picker.Click
        Dim CList As New List(Of Control)
        CList.Add(sender)

        Dim C As Color = ColorPicker.Pick(CList)
        CP.LogonUI_PersonalColors_Background = Color.FromArgb(255, C)
        ApplyLivePreviewFromCP(CP)

        CList.Clear()
        CList = Nothing
    End Sub

    Private Sub LogonUI_PersonalColorsAccent_Picker_Click(sender As Object, e As EventArgs) Handles LogonUI_PersonalColorsAccent_Picker.Click
        Dim CList As New List(Of Control)
        CList.Add(sender)

        Dim C As Color = ColorPicker.Pick(CList)
        CP.LogonUI_PersonalColors_Accent = Color.FromArgb(255, C)
        ApplyLivePreviewFromCP(CP)

        CList.Clear()
        CList = Nothing
    End Sub

    Private Sub XenonButton4_Click_1(sender As Object, e As EventArgs) Handles XenonButton4.Click
        Win32UI.Show()
    End Sub

    Private Sub PictureBox23_MouseHover(sender As Object, e As EventArgs) Handles PictureBox23.MouseHover, PictureBox28.MouseHover, PictureBox29.MouseHover, PictureBox23.MouseEnter, PictureBox28.MouseEnter, PictureBox29.MouseEnter
        ToolTip1.Show("This has effect only for Windows 10", sender)
    End Sub

    Private Sub XenonButton14_Click(sender As Object, e As EventArgs) Handles XenonButton14.Click
        CP_Original = CP
        CP.Save(CP.SavingMode.Registry)
        RestartExplorer()
        Me.Close()
    End Sub

    Private Sub XenonButton13_Click(sender As Object, e As EventArgs) Handles XenonButton13.Click
        CP_FirstTime.Save(CP.SavingMode.Registry)
        RestartExplorer()
        Me.Close()
    End Sub

    Private Sub PictureBox23_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox23.MouseLeave, PictureBox28.MouseLeave, PictureBox29.MouseLeave
        ToolTip1.Hide(sender)
    End Sub
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
