Imports System.Runtime.InteropServices
Imports WinPaletter.XenonCore

Public Class dragPreviewer

    Public CP As CP
    Public File As String

#Region "Form Shadow"

    Private aeroEnabled As Boolean

    Protected Overrides ReadOnly Property CreateParams() As CreateParams
        Get
            CheckAeroEnabled()
            Dim cp As CreateParams = MyBase.CreateParams
            If Not aeroEnabled Then
                cp.ClassStyle = cp.ClassStyle Or NativeMethods.Dwmapi.CS_DROPSHADOW
                cp.ExStyle = cp.ExStyle Or 33554432
                Return cp
            Else
                Return cp
            End If
        End Get
    End Property

    Protected Overrides Sub WndProc(ByRef m As Message)
        Select Case m.Msg
            Case NativeMethods.Dwmapi.WM_NCPAINT
                Dim val = 2
                If aeroEnabled Then
                    NativeMethods.Dwmapi.DwmSetWindowAttribute(FindForm.Handle, If(GetRoundedCorners(), 2, 1), val, 4)
                    Dim bla As New NativeMethods.Dwmapi.MARGINS()
                    With bla
                        .bottomHeight = 1
                        .leftWidth = 1
                        .rightWidth = 1
                        .topHeight = 1
                    End With
                    NativeMethods.Dwmapi.DwmExtendFrameIntoClientArea(Handle, bla)
                End If
                Exit Select
        End Select

        MyBase.WndProc(m)
    End Sub

    Private Sub CheckAeroEnabled()
        If Environment.OSVersion.Version.Major >= 6 Then
            Dim enabled As Integer = 0
            Dim response As Integer = NativeMethods.Dwmapi.DwmIsCompositionEnabled(enabled)
            aeroEnabled = (enabled = 1)
        Else
            aeroEnabled = False
        End If
    End Sub

#End Region

    Private Sub dragPreviewer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CP = New CP(CP.Mode.File, File)
        pnl_preview.BackgroundImage = My.Application.Wallpaper
        Adjust_Preview()
        ApplyLivePreviewFromCP(CP)
        pnl_preview.Visible = True : PictureBox1.Image = GetControlImage(pnl_preview) : pnl_preview.Visible = False
        Acrylism.EnableBlur(Me.Handle, True)
        Me.Invalidate()
    End Sub

    Sub ApplyLivePreviewFromCP(ByVal [CP] As CP)
        XenonWindow1.AccentColor_Enabled = [CP].ApplyAccentonTitlebars
        XenonWindow2.AccentColor_Enabled = [CP].ApplyAccentonTitlebars

        XenonWindow1.AccentColor_Active = [CP].Titlebar_Active
        XenonWindow2.AccentColor_Active = [CP].Titlebar_Active

        XenonWindow1.AccentColor_Inactive = [CP].Titlebar_Inactive
        XenonWindow2.AccentColor_Inactive = [CP].Titlebar_Inactive

        XenonWindow1.DarkMode = Not [CP].AppMode_Light
        XenonWindow2.DarkMode = Not [CP].AppMode_Light

        Dim AnimX1 As Integer = 30
        Dim AnimX2 As Integer = 1

        Label8.ForeColor = If([CP].AppMode_Light, Color.Black, Color.White)

        Select Case MainFrm.PreviewConfig
            Case MainFrm.WinVer.Eleven
#Region "Win11"
                start.DarkMode = Not [CP].WinMode_Light
                taskbar.DarkMode = Not [CP].WinMode_Light
                ActionCenter.DarkMode = Not [CP].WinMode_Light

                taskbar.Transparency = [CP].Transparency
                start.Transparency = [CP].Transparency
                ActionCenter.Transparency = [CP].Transparency

                Select Case Not [CP].WinMode_Light
                    Case True   ''''''''''Dark
                        taskbar.BackColorAlpha = 75
                        start.BackColorAlpha = 175

                        If [CP].ApplyAccentonTaskbar Then
                            ActionCenter.BackColorAlpha = 180
                            taskbar.BackColor = Color.FromArgb(taskbar.BackColor.A, [CP].StartListFolders_TaskbarFront)
                            start.BackColor = Color.FromArgb(start.BackColor.A, [CP].StartListFolders_TaskbarFront)
                            ActionCenter.BackColor = Color.FromArgb(ActionCenter.BackColor.A, [CP].StartListFolders_TaskbarFront)
                        Else
                            ActionCenter.BackColorAlpha = 100
                            taskbar.BackColor = Color.FromArgb(55, 55, 55)
                            start.BackColor = Color.FromArgb(40, 40, 40)
                            ActionCenter.BackColor = Color.FromArgb(55, 55, 55)
                        End If

                        ActionCenter.ActionCenterButton_Normal = [CP].Taskbar_Icon_Underline
                        ActionCenter.ActionCenterButton_Hover = [CP].ActionCenter_AppsLinks
                        ActionCenter.ActionCenterButton_Pressed = [CP].StartButton_Hover
                        start.SearchBoxAccent = [CP].Taskbar_Icon_Underline
                        taskbar.AppUnderline = [CP].Taskbar_Icon_Underline

                        Label3.ForeColor = [CP].SettingsIconsAndLinks
                        Label12.ForeColor = [CP].ActionCenter_AppsLinks

                    Case False   ''''''''''Light
                        taskbar.BackColorAlpha = 200
                        start.BackColorAlpha = 210
                        If [CP].ApplyAccentonTaskbar Then
                            ActionCenter.BackColorAlpha = 180
                            taskbar.BackColor = Color.FromArgb(taskbar.BackColor.A, [CP].Taskbar_Icon_Underline)
                            start.BackColor = Color.FromArgb(start.BackColor.A, [CP].ActionCenter_AppsLinks)
                            ActionCenter.BackColor = Color.FromArgb(ActionCenter.BackColor.A, [CP].ActionCenter_AppsLinks)
                        Else
                            ActionCenter.BackColorAlpha = 100
                            taskbar.BackColor = Color.FromArgb(255, 255, 255)
                            start.BackColor = Color.FromArgb(255, 255, 255)
                            ActionCenter.BackColor = Color.FromArgb(255, 255, 255)
                        End If

                        ActionCenter.ActionCenterButton_Normal = [CP].StartMenuBackground_ActiveTaskbarButton
                        ActionCenter.ActionCenterButton_Hover = [CP].StartListFolders_TaskbarFront
                        ActionCenter.ActionCenterButton_Pressed = [CP].StartButton_Hover
                        start.SearchBoxAccent = [CP].StartMenuBackground_ActiveTaskbarButton
                        taskbar.AppUnderline = [CP].StartMenuBackground_ActiveTaskbarButton

                        Label3.ForeColor = [CP].SettingsIconsAndLinks
                        Label12.ForeColor = [CP].StartListFolders_TaskbarFront
                End Select
#End Region
            Case MainFrm.WinVer.Ten
#Region "Win10"
                start.DarkMode = Not [CP].WinMode_Light
                taskbar.DarkMode = Not [CP].WinMode_Light
                ActionCenter.DarkMode = Not [CP].WinMode_Light

                taskbar.Transparency = [CP].Transparency
                start.Transparency = [CP].Transparency
                ActionCenter.Transparency = [CP].Transparency

                taskbar.AppUnderline = ControlPaint.Light(Color.FromArgb(255, [CP].Taskbar_Icon_Underline))

                If [CP].Transparency Then
                    taskbar.BackColorAlpha = 200
                    start.BackColorAlpha = 210
                    ActionCenter.BackColorAlpha = 100
                Else
                    taskbar.BackColorAlpha = 255
                    start.BackColorAlpha = 255
                    ActionCenter.BackColorAlpha = 255
                End If

                If [CP].WinMode_Light And Not [CP].ApplyAccentonTaskbar Then
                    taskbar.BackColor = Color.FromArgb(237, 237, 237)
                    start.BackColor = Color.FromArgb(227, 227, 227)
                    ActionCenter.BackColor = Color.FromArgb(227, 227, 227)
                    taskbar.StartColor = Color.Transparent
                Else
                    If Not [CP].ApplyAccentonTaskbar Then
                        taskbar.BackColor = Color.FromArgb(23, 23, 23)
                        start.BackColor = Color.FromArgb(36, 36, 36)
                        ActionCenter.BackColor = Color.FromArgb(36, 36, 36)
                        taskbar.StartColor = Color.Transparent

                    Else
                        start.BackColor = [CP].StartMenuBackground_ActiveTaskbarButton
                        taskbar.StartColor = [CP].StartMenuBackground_ActiveTaskbarButton
                        ActionCenter.BackColor = [CP].StartMenuBackground_ActiveTaskbarButton

                        If [CP].Transparency Then
                            taskbar.BackColor = [CP].Taskbar_Background
                        Else
                            taskbar.BackColor = [CP].StartListFolders_TaskbarFront
                        End If

                    End If
                End If

                If [CP].WinMode_Light And Not [CP].ApplyAccentonTaskbar Then
                    Label3.ForeColor = [CP].SettingsIconsAndLinks
                    Label12.ForeColor = [CP].Taskbar_Background
                    ActionCenter.LinkColor = [CP].Taskbar_Background
                Else
                    Label3.ForeColor = [CP].SettingsIconsAndLinks
                    Label12.ForeColor = [CP].ActionCenter_AppsLinks
                    taskbar.AppUnderline = [CP].Taskbar_Icon_Underline
                    ActionCenter.LinkColor = [CP].Taskbar_Icon_Underline

                    If Not [CP].Transparency Then
                        taskbar.AppBackground = [CP].StartMenuBackground_ActiveTaskbarButton
                    Else
                        taskbar.AppBackground = Color.Transparent
                    End If
                End If

                MainFrm.ReValidateLivePreview(pnl_preview)
#End Region
        End Select
    End Sub

    Sub Adjust_Preview()

        Select Case MainFrm.PreviewConfig
            Case MainFrm.WinVer.Eleven
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

            Case MainFrm.WinVer.Ten
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

End Class