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
                cp.ClassStyle = cp.ClassStyle Or NativeConstants.CS_DROPSHADOW
                cp.ExStyle = cp.ExStyle Or 33554432
                Return cp
            Else
                Return cp
            End If
        End Get
    End Property

    Protected Overrides Sub WndProc(ByRef m As Message)
        Select Case m.Msg
            Case NativeConstants.WM_NCPAINT
                Dim val = 2
                If aeroEnabled Then
                    NativeMethods.DwmSetWindowAttribute(FindForm.Handle, If(GetRoundedCorners(), 2, 1), val, 4)
                    Dim bla As New NativeStructs.MARGINS()
                    With bla
                        .bottomHeight = 1
                        .leftWidth = 1
                        .rightWidth = 1
                        .topHeight = 1
                    End With
                    NativeMethods.DwmExtendFrameIntoClientArea(Handle, bla)
                End If
                Exit Select
        End Select

        MyBase.WndProc(m)
    End Sub

    Private Sub CheckAeroEnabled()
        If Environment.OSVersion.Version.Major >= 6 Then
            Dim enabled As Integer = 0
            Dim response As Integer = NativeMethods.DwmIsCompositionEnabled(enabled)
            aeroEnabled = (enabled = 1)
        Else
            aeroEnabled = False
        End If
    End Sub

    Public Class NativeStructs

        Public Structure MARGINS
            Public leftWidth As Integer
            Public rightWidth As Integer
            Public topHeight As Integer
            Public bottomHeight As Integer
        End Structure

    End Class

    Public Class NativeMethods

        <Runtime.InteropServices.DllImport("dwmapi")> Public Shared Function DwmExtendFrameIntoClientArea(ByVal hWnd As IntPtr, ByRef pMarInset As NativeStructs.MARGINS) As Integer
        End Function

        <Runtime.InteropServices.DllImport("dwmapi")> Friend Shared Function DwmSetWindowAttribute(ByVal hwnd As IntPtr, ByVal attr As Integer, ByRef attrValue As Integer, ByVal attrSize As Integer) As Integer
        End Function

        <Runtime.InteropServices.DllImport("dwmapi.dll")> Public Shared Function DwmIsCompositionEnabled(ByRef pfEnabled As Integer) As Integer
        End Function

    End Class

    Public Class NativeConstants
        Public Const CS_DROPSHADOW As Integer = &H20000
        Public Const WM_NCPAINT As Integer = &H85
    End Class

#End Region

    <Flags>
    Enum AnimateWindowFlags
        AW_HOR_POSITIVE = &H0
        AW_HOR_NEGATIVE = &H2
        AW_VER_POSITIVE = &H4
        AW_VER_NEGATIVE = &H8
        AW_CENTER = &H10
        AW_HIDE = &H10000
        AW_ACTIVATE = &H20000
        AW_SLIDE = &H40000
        AW_BLEND = &H80000
    End Enum

    <DllImport("user32.dll")>
    Shared Function AnimateWindow(ByVal hWnd As IntPtr, ByVal time As Integer, ByVal flags As AnimateWindowFlags) As Boolean
    End Function


    Private Sub dragPreviewer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CP = New CP(CP.Mode.File, File)
        pnl_preview.BackgroundImage = My.Application.Wallpaper
        ApplyLivePreviewFromCP(CP)
        pnl_preview.Visible = True : PictureBox1.Image = GetControlImage(pnl_preview) : pnl_preview.Visible = False
        Acrylism.EnableBlur(Me.Handle, True)
        Me.Invalidate()
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

        Label8.ForeColor = If(CP.AppMode_Light, Color.Black, Color.White)

        Select Case MainForm.PreviewConfig
            Case MainForm.WinVer.Eleven
#Region "Win11"
                start.DarkMode = Not CP.WinMode_Light
                taskbar.DarkMode = Not CP.WinMode_Light
                ActionCenter.DarkMode = Not CP.WinMode_Light

                taskbar.Transparency = CP.Transparency
                start.Transparency = CP.Transparency
                ActionCenter.Transparency = CP.Transparency

                Select Case Not CP.WinMode_Light
                    Case True   ''''''''''Dark
                        taskbar.BackColorAlpha = 75
                        start.BackColorAlpha = 175

                        If CP.ApplyAccentonTaskbar Then
                            ActionCenter.BackColorAlpha = 180
                            taskbar.BackColor = Color.FromArgb(taskbar.BackColor.A, CP.StartListFolders_TaskbarFront)
                            start.BackColor = Color.FromArgb(start.BackColor.A, CP.StartListFolders_TaskbarFront)
                            ActionCenter.BackColor = Color.FromArgb(ActionCenter.BackColor.A, CP.StartListFolders_TaskbarFront)
                        Else
                            ActionCenter.BackColorAlpha = 100
                            taskbar.BackColor = Color.FromArgb(55, 55, 55)
                            start.BackColor = Color.FromArgb(40, 40, 40)
                            ActionCenter.BackColor = Color.FromArgb(55, 55, 55)
                        End If

                        ActionCenter.ActionCenterButton_Normal = CP.Taskbar_Icon_Underline
                        ActionCenter.ActionCenterButton_Hover = CP.ActionCenter_AppsLinks
                        ActionCenter.ActionCenterButton_Pressed = CP.StartButton_Hover
                        start.SearchBoxAccent = CP.Taskbar_Icon_Underline
                        taskbar.AppUnderline = CP.Taskbar_Icon_Underline

                        Label3.ForeColor = CP.SettingsIconsAndLinks
                        Label12.ForeColor = CP.ActionCenter_AppsLinks

                    Case False   ''''''''''Light
                        taskbar.BackColorAlpha = 200
                        start.BackColorAlpha = 210
                        If CP.ApplyAccentonTaskbar Then
                            ActionCenter.BackColorAlpha = 180
                            taskbar.BackColor = Color.FromArgb(taskbar.BackColor.A, CP.Taskbar_Icon_Underline)
                            start.BackColor = Color.FromArgb(start.BackColor.A, CP.ActionCenter_AppsLinks)
                            ActionCenter.BackColor = Color.FromArgb(ActionCenter.BackColor.A, CP.ActionCenter_AppsLinks)
                        Else
                            ActionCenter.BackColorAlpha = 100
                            taskbar.BackColor = Color.FromArgb(255, 255, 255)
                            start.BackColor = Color.FromArgb(255, 255, 255)
                            ActionCenter.BackColor = Color.FromArgb(255, 255, 255)
                        End If

                        ActionCenter.ActionCenterButton_Normal = CP.StartMenuBackground_ActiveTaskbarButton
                        ActionCenter.ActionCenterButton_Hover = CP.StartListFolders_TaskbarFront
                        ActionCenter.ActionCenterButton_Pressed = CP.StartButton_Hover
                        start.SearchBoxAccent = CP.StartMenuBackground_ActiveTaskbarButton
                        taskbar.AppUnderline = CP.StartMenuBackground_ActiveTaskbarButton

                        Label3.ForeColor = CP.SettingsIconsAndLinks
                        Label12.ForeColor = CP.StartListFolders_TaskbarFront
                End Select
#End Region
            Case MainForm.WinVer.Ten
#Region "Win10"
                start.DarkMode = Not CP.WinMode_Light
                taskbar.DarkMode = Not CP.WinMode_Light
                ActionCenter.DarkMode = Not CP.WinMode_Light

                taskbar.Transparency = CP.Transparency
                start.Transparency = CP.Transparency
                ActionCenter.Transparency = CP.Transparency

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
                    taskbar.BackColor = Color.FromArgb(237, 237, 237)
                    start.BackColor = Color.FromArgb(227, 227, 227)
                    ActionCenter.BackColor = Color.FromArgb(227, 227, 227)
                    taskbar.StartColor = Color.Transparent
                Else
                    If Not CP.ApplyAccentonTaskbar Then
                        taskbar.BackColor = Color.FromArgb(23, 23, 23)
                        start.BackColor = Color.FromArgb(36, 36, 36)
                        ActionCenter.BackColor = Color.FromArgb(36, 36, 36)
                        taskbar.StartColor = Color.Transparent

                    Else
                        start.BackColor = CP.StartMenuBackground_ActiveTaskbarButton
                        taskbar.StartColor = CP.StartMenuBackground_ActiveTaskbarButton
                        ActionCenter.BackColor = CP.StartMenuBackground_ActiveTaskbarButton

                        If CP.Transparency Then
                            taskbar.BackColor = CP.Taskbar_Background
                        Else
                            taskbar.BackColor = CP.StartListFolders_TaskbarFront
                        End If

                    End If
                End If

                If CP.WinMode_Light And Not CP.ApplyAccentonTaskbar Then
                    Label3.ForeColor = CP.SettingsIconsAndLinks
                    Label12.ForeColor = CP.Taskbar_Background
                    ActionCenter.LinkColor = CP.Taskbar_Background
                Else
                    Label3.ForeColor = CP.SettingsIconsAndLinks
                    Label12.ForeColor = CP.ActionCenter_AppsLinks
                    taskbar.AppUnderline = CP.Taskbar_Icon_Underline
                    ActionCenter.LinkColor = CP.Taskbar_Icon_Underline

                    If Not CP.Transparency Then
                        taskbar.AppBackground = CP.StartMenuBackground_ActiveTaskbarButton
                    Else
                        taskbar.AppBackground = Color.Transparent
                    End If
                End If

                MainForm.ReValidateLivePreview(pnl_preview)
#End Region
        End Select


        MsgBox("X")
    End Sub

End Class