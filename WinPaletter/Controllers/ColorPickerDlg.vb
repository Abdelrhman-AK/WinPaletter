Imports System.ComponentModel
Imports System.IO
Imports System.Reflection
Imports Cyotek.Windows.Forms
Imports Devcorp.Controls.VisualStyles
Imports WinPaletter.CP
Imports WinPaletter.NativeMethods
Imports WinPaletter.PreviewHelpers
Imports WinPaletter.XenonCore

Public Class ColorPickerDlg
    Dim InitColor As Color
    Dim img As Image
    ReadOnly ChildControls_List As New List(Of Control)
    Dim ColorControls_List As New List(Of Control)
    ReadOnly Forms_List As New List(Of Form)
    Private Colors_List As New List(Of Color)
    Private _Conditions As New Conditions
    ReadOnly _Speed As Integer = 50

#Region "Form Shadow"

    Private aeroEnabled As Boolean

    Protected Overrides ReadOnly Property CreateParams() As CreateParams
        Get
            CheckAeroEnabled()
            Dim cp As CreateParams = MyBase.CreateParams
            If Not aeroEnabled Then
                cp.ClassStyle = cp.ClassStyle Or Dwmapi.CS_DROPSHADOW
                cp.ExStyle = cp.ExStyle Or 33554432
                Return cp
            Else
                Return cp
            End If
        End Get
    End Property

    Protected Overrides Sub WndProc(ByRef m As Message)
        Select Case m.Msg
            Case Dwmapi.WM_NCPAINT
                Dim val = 2
                If aeroEnabled Then
                    Dwmapi.DwmSetWindowAttribute(Handle, If(GetRoundedCorners(), 2, 1), val, 4)
                    Dim bla As New Dwmapi.MARGINS()
                    With bla
                        .bottomHeight = 1
                        .leftWidth = 1
                        .rightWidth = 1
                        .topHeight = 1
                    End With
                    Dwmapi.DwmExtendFrameIntoClientArea(Handle, bla)
                End If
        End Select

        MyBase.WndProc(m)
    End Sub

    Private Sub CheckAeroEnabled()
        If Environment.OSVersion.Version.Major >= 6 Then
            Dim Com As Boolean
            Dwmapi.DwmIsCompositionEnabled(Com)
            aeroEnabled = Com
        Else
            aeroEnabled = False
        End If
    End Sub
#End Region

    Dim newPoint As New Point()
    Dim xPoint As New Point()

    Private Sub ColorPicker_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseDown
        xPoint = MousePosition - Location
    End Sub

    Private Sub ColorPicker_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseMove
        If e.Button = MouseButtons.Left Then
            newPoint = MousePosition - xPoint
            Location = newPoint
        End If
    End Sub

    Private Sub ColorPicker_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        User32.AnimateWindow(Handle, _Speed, User32.AnimateWindowFlags.AW_HIDE Or User32.AnimateWindowFlags.AW_BLEND)
    End Sub

    Private Sub ColorPicker_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadLanguage
        ApplyDarkMode(Me)
        XenonComboBox1.PopulateThemes

        User32.AnimateWindow(Handle, _Speed, User32.AnimateWindowFlags.AW_ACTIVATE Or User32.AnimateWindowFlags.AW_BLEND)
        Invalidate()
    End Sub

    Sub GetColorsFromPalette(CP As CP)
        PaletteContainer.SuspendLayout()

        For Each c As XenonCP In PaletteContainer.Controls.OfType(Of XenonCP)
            c.Dispose()
            RemoveHandler c.Click, AddressOf Pnl_click
            PaletteContainer.Controls.Remove(c)
        Next

        PaletteContainer.Controls.Clear()

        For Each c As Color In CP.ListColors

            Dim pnl As New XenonCP With {
                .Size = New Drawing.Size(If(My.Settings.NerdStats.Enabled, 80, 30), 20),
                .BackColor = c,
                .DefaultColor = .BackColor
            }

            PaletteContainer.Controls.Add(pnl)
            AddHandler pnl.Click, AddressOf Pnl_click
        Next

        PaletteContainer.ResumeLayout()
    End Sub

    Private Sub XenonButton4_Click(sender As Object, e As EventArgs)
        ColorWheel1.Visible = False
        ColorGrid1.Visible = False
    End Sub

    Private Sub ScreenColorPicker1_MouseDown(sender As Object, e As MouseEventArgs) Handles ScreenColorPicker1.MouseDown
        Forms_List.Clear()
        ChildControls_List.Clear()

        For Each ctrl As Control In Me.Controls
            If TypeOf ctrl IsNot ScreenColorPicker And ctrl.Visible Then
                ctrl.Visible = False
                ChildControls_List.Add(ctrl)
            End If
        Next

        For ix As Integer = Application.OpenForms.Count - 1 To 0 Step -1
            If Application.OpenForms(ix).Visible And Application.OpenForms(ix) IsNot Me Then Forms_List.Add(Application.OpenForms(ix))
        Next
        For ix = 0 To Forms_List.Count - 1
            Forms_List(ix).Visible = False
        Next

        AllowTransparency = True
        TransparencyKey = BackColor
    End Sub

    Private Sub ScreenColorPicker1_MouseUp(sender As Object, e As MouseEventArgs) Handles ScreenColorPicker1.MouseUp

        For Each ctrl As Control In ChildControls_List
            ctrl.Visible = True
        Next
        For ix = 0 To Forms_List.Count - 1
            Forms_List(ix).Visible = True
        Next
        Forms_List.Clear()
        ChildControls_List.Clear()

        AllowTransparency = False
        TransparencyKey = Nothing
    End Sub

    Function Pick(Ctrl As List(Of Control), Optional [Conditions] As Conditions = Nothing, Optional EnableAlpha As Boolean = False) As Color
        If Not My.Settings.Miscellaneous.Classic_Color_Picker Then
            Dim c As Color = Ctrl(0).BackColor
            ColorEditorManager1.Color = Ctrl(0).BackColor
            InitColor = Ctrl(0).BackColor

            ColorControls_List = Ctrl

            If TypeOf Ctrl(0) Is XenonCP Then
                CType(Ctrl(0), XenonCP).ColorPickerOpened = True
                CType(Ctrl(0), XenonCP).Refresh()
            End If

            If [Conditions] Is Nothing Then _Conditions = New Conditions Else _Conditions = [Conditions]

            AddHandler ColorEditorManager1.ColorChanged, AddressOf Change_Color_Preview

            Location = Ctrl(0).PointToScreen(Point.Empty) + New Point(-Width + Ctrl(0).Width + 5, Ctrl(0).Height - 1)
            If Location.Y + Height > My.Computer.Screen.Bounds.Height Then Location = New Point(Location.X, My.Computer.Screen.Bounds.Height - Height)
            If Location.Y < 0 Then Location = New Point(Location.X, 0)
            If Location.X + Width > My.Computer.Screen.Bounds.Width Then Location = New Point(My.Computer.Screen.Bounds.Width - Width, Location.Y)
            If Location.X < 0 Then Location = New Point(0, Location.Y)

            If ShowDialog() = DialogResult.OK Then c = ColorEditorManager1.Color

            If TypeOf Ctrl(0) Is XenonCP Then
                CType(Ctrl(0), XenonCP).ColorPickerOpened = False
                CType(Ctrl(0), XenonCP).Refresh()
            End If

            RemoveHandler ColorEditorManager1.ColorChanged, AddressOf Change_Color_Preview

            If EnableAlpha Then
                Return c
            Else
                Return Color.FromArgb(255, c)
            End If

        Else
            Dim c As Color = Color.FromArgb(Ctrl(0).BackColor.A, Ctrl(0).BackColor)
            Using CCP As New ColorDialog With {.AllowFullOpen = True, .AnyColor = True, .Color = c, .FullOpen = True, .SolidColorOnly = False}
                If CCP.ShowDialog = DialogResult.OK Then
                    Try : Ctrl(0).BackColor = CCP.Color : Catch : End Try
                    Return CCP.Color
                Else
                    Try : Ctrl(0).BackColor = c : Catch : End Try
                    Return c
                End If
            End Using

        End If

    End Function

    Public Sub Change_Color_Preview()
        Dim steps As Integer = 30
        Dim delay As Integer = 1

        For Each ctrl As Control In ColorControls_List

            If TypeOf ctrl Is XenonWindow Then
                With DirectCast(ctrl, XenonWindow)
                    If Not _Conditions.Win7 Then
                        If _Conditions.Window_ActiveTitlebar Then
                            .AccentColor_Active = ColorEditorManager1.Color
                        End If

                        If _Conditions.Window_InactiveTitlebar Then
                            .AccentColor_Inactive = ColorEditorManager1.Color
                        Else
                            .AccentColor_Active = ColorEditorManager1.Color
                        End If

                    Else

                        If _Conditions.Color1 Then
                            .AccentColor_Active = ColorEditorManager1.Color
                            .AccentColor_Inactive = ColorEditorManager1.Color
                        ElseIf _Conditions.Color2 Then
                            .AccentColor2_Active = ColorEditorManager1.Color
                            .AccentColor2_Inactive = ColorEditorManager1.Color
                        End If

                    End If

                    .Refresh()
                End With

            ElseIf TypeOf ctrl Is XenonWinElement Then
                With DirectCast(ctrl, XenonWinElement)

                    If .Style = XenonWinElement.Styles.Taskbar11 Or .Style = XenonWinElement.Styles.Taskbar10 Then
                        If _Conditions.AppUnderlineOnly Then
                            Visual.FadeColor(DirectCast(ctrl, XenonWinElement), "AppUnderline", .AppUnderline, Color.FromArgb(ctrl.BackColor.A, ColorEditorManager1.Color).Light, steps, delay)
                            .Refresh()

                        ElseIf _Conditions.AppUnderlineWithTaskbar Then
                            Visual.FadeColor(DirectCast(ctrl, XenonWinElement), "BackColor", .BackColor, Color.FromArgb(.BackColor.A, ColorEditorManager1.Color), steps, delay)
                            Visual.FadeColor(DirectCast(ctrl, XenonWinElement), "AppUnderline", .AppUnderline, Color.FromArgb(ctrl.BackColor.A, ColorEditorManager1.Color).Light, steps, delay)
                            .Refresh()

                        ElseIf _Conditions.AppBackgroundOnly Then

                            Visual.FadeColor(DirectCast(ctrl, XenonWinElement), "AppBackground", .AppBackground, Color.FromArgb(ctrl.BackColor.A, ColorEditorManager1.Color), steps, delay)
                            .Refresh()
                        ElseIf _Conditions.StartColorOnly Then

                            Visual.FadeColor(DirectCast(ctrl, XenonWinElement), "StartColor", .StartColor, Color.FromArgb(255, ColorEditorManager1.Color), steps, delay)
                            .Refresh()
                        Else
                            If _Conditions.BackColor1 Then
                                .BackColor = Color.FromArgb(.BackColor.A, ColorEditorManager1.Color)
                            ElseIf _Conditions.BackColor2 Then
                                .Background2 = Color.FromArgb(.Background2.A, ColorEditorManager1.Color)
                            Else
                                Visual.FadeColor(DirectCast(ctrl, XenonWinElement), "BackColor", .BackColor, Color.FromArgb(.BackColor.A, ColorEditorManager1.Color), steps, delay)
                            End If
                            .Refresh()
                        End If

                    ElseIf .Style = XenonWinElement.Styles.ActionCenter11 And _Conditions.ActionCenterBtn Then
                        Visual.FadeColor(DirectCast(ctrl, XenonWinElement), "ActionCenterButton_Normal", .ActionCenterButton_Normal, Color.FromArgb(255, ColorEditorManager1.Color), steps, delay)
                        .Refresh()

                    ElseIf .Style = XenonWinElement.Styles.ActionCenter10 And _Conditions.ActionCenterLink Then
                        Visual.FadeColor(DirectCast(ctrl, XenonWinElement), "LinkColor", .LinkColor, Color.FromArgb(.BackColor.A, ColorEditorManager1.Color), steps, delay)
                        .Refresh()

                    Else
                        If _Conditions.BackColor1 Then
                            .BackColor = Color.FromArgb(.BackColor.A, ColorEditorManager1.Color)
                        ElseIf _Conditions.BackColor2 Then
                            .Background2 = Color.FromArgb(.Background2.A, ColorEditorManager1.Color)
                        Else
                            Visual.FadeColor(DirectCast(ctrl, XenonWinElement), "BackColor", .BackColor, Color.FromArgb(.BackColor.A, ColorEditorManager1.Color), steps, delay)
                        End If

                        .Refresh()
                    End If
                End With

            ElseIf TypeOf ctrl Is StoreItem Then
                With DirectCast(ctrl, StoreItem)
                    If _Conditions.BackColor1 Then
                        .CP.Info.Color1 = Color.FromArgb(255, ColorEditorManager1.Color)
                    ElseIf _Conditions.BackColor2 Then
                        .CP.Info.Color2 = Color.FromArgb(255, ColorEditorManager1.Color)

                    End If
                    .Invalidate()
                End With

            ElseIf TypeOf ctrl Is Label Then
                If _Conditions.RetroAppWorkspace Or _Conditions.RetroBackground Then
                    ctrl.BackColor = Color.FromArgb(255, ColorEditorManager1.Color)
                Else
                    Visual.FadeColor(ctrl, "Forecolor", ctrl.ForeColor, Color.FromArgb(ctrl.ForeColor.A, ColorEditorManager1.Color), steps, delay)
                End If

            ElseIf TypeOf ctrl Is RetroWindow Then
                With DirectCast(ctrl, RetroWindow)
                    If _Conditions.RetroWindowColor1 Then .Color1 = Color.FromArgb(255, ColorEditorManager1.Color)
                    If _Conditions.RetroWindowColor2 Then .Color2 = Color.FromArgb(255, ColorEditorManager1.Color)
                    If _Conditions.RetroWindowForeColor Then .ForeColor = Color.FromArgb(255, ColorEditorManager1.Color)
                    If _Conditions.RetroWindowBorder Then .ColorBorder = Color.FromArgb(255, ColorEditorManager1.Color)
                    If _Conditions.RetroButtonShadow Then .ButtonShadow = Color.FromArgb(255, ColorEditorManager1.Color)
                    If _Conditions.RetroButtonDkShadow Then .ButtonDkShadow = Color.FromArgb(255, ColorEditorManager1.Color)
                    If _Conditions.RetroButtonHilight Then .ButtonHilight = Color.FromArgb(255, ColorEditorManager1.Color)
                    If _Conditions.RetroButtonLight Then .ButtonLight = Color.FromArgb(255, ColorEditorManager1.Color)
                    If _Conditions.RetroButtonFace Then
                        If ctrl IsNot Win32UI.Menu Then
                            .BackColor = Color.FromArgb(255, ColorEditorManager1.Color)
                        Else
                            .ButtonFace = Color.FromArgb(255, ColorEditorManager1.Color)
                        End If
                    End If
                    If _Conditions.RetroBackground Then .BackColor = Color.FromArgb(255, ColorEditorManager1.Color)
                    .Refresh()
                End With

            ElseIf TypeOf ctrl Is Retro3DPreview Then
                With DirectCast(ctrl, Retro3DPreview)
                    If _Conditions.RetroButtonFace Then .BackColor = Color.FromArgb(255, ColorEditorManager1.Color)
                    If _Conditions.RetroWindowFrame Then .WindowFrame = Color.FromArgb(255, ColorEditorManager1.Color)
                    If _Conditions.RetroButtonText Then .ForeColor = Color.FromArgb(255, ColorEditorManager1.Color)
                    If _Conditions.RetroButtonShadow Then .ButtonShadow = Color.FromArgb(255, ColorEditorManager1.Color)
                    If _Conditions.RetroButtonDkShadow Then .ButtonDkShadow = Color.FromArgb(255, ColorEditorManager1.Color)
                    If _Conditions.RetroButtonHilight Then .ButtonHilight = Color.FromArgb(255, ColorEditorManager1.Color)
                    If _Conditions.RetroButtonLight Then .ButtonLight = Color.FromArgb(255, ColorEditorManager1.Color)
                    .Refresh()
                End With

            ElseIf TypeOf ctrl Is RetroTextBox Then
                With DirectCast(ctrl, RetroTextBox)

                    If _Conditions.RetroWindowForeColor Then .ForeColor = Color.FromArgb(255, ColorEditorManager1.Color)
                    If _Conditions.RetroButtonShadow Then .ButtonShadow = Color.FromArgb(255, ColorEditorManager1.Color)
                    If _Conditions.RetroButtonDkShadow Then .ButtonDkShadow = Color.FromArgb(255, ColorEditorManager1.Color)
                    If _Conditions.RetroButtonHilight Then .ButtonHilight = Color.FromArgb(255, ColorEditorManager1.Color)
                    If _Conditions.RetroButtonLight Then .ButtonLight = Color.FromArgb(255, ColorEditorManager1.Color)
                    If _Conditions.RetroButtonFace Then .BackColor = Color.FromArgb(255, ColorEditorManager1.Color)
                    If _Conditions.RetroBackground Then .BackColor = Color.FromArgb(255, ColorEditorManager1.Color)

                    .Refresh()
                End With

            ElseIf TypeOf ctrl Is RetroButton Then
                With DirectCast(ctrl, RetroButton)
                    If _Conditions.RetroButtonFace Then .BackColor = Color.FromArgb(255, ColorEditorManager1.Color)
                    If _Conditions.RetroWindowFrame Then .WindowFrame = Color.FromArgb(255, ColorEditorManager1.Color)
                    If _Conditions.RetroButtonText Then .ForeColor = Color.FromArgb(255, ColorEditorManager1.Color)
                    If _Conditions.RetroButtonShadow Then .ButtonShadow = Color.FromArgb(255, ColorEditorManager1.Color)
                    If _Conditions.RetroButtonDkShadow Then .ButtonDkShadow = Color.FromArgb(255, ColorEditorManager1.Color)
                    If _Conditions.RetroButtonHilight Then .ButtonHilight = Color.FromArgb(255, ColorEditorManager1.Color)
                    If _Conditions.RetroButtonLight Then .ButtonLight = Color.FromArgb(255, ColorEditorManager1.Color)
                    .Refresh()
                End With

            ElseIf TypeOf ctrl Is RetroScrollBar Then
                With DirectCast(ctrl, RetroScrollBar)
                    If _Conditions.RetroButtonHilight Then .ButtonHilight = Color.FromArgb(255, ColorEditorManager1.Color) Else .BackColor = Color.FromArgb(255, ColorEditorManager1.Color)
                    .Refresh()
                End With

            ElseIf TypeOf ctrl Is Panel Then
                If _Conditions.RetroHighlight17BitFixer And TypeOf ctrl Is RetroPanel Then
                    DirectCast(ctrl, RetroPanel).ButtonShadow = Color.FromArgb(255, ColorEditorManager1.Color)
                Else
                    If TypeOf ctrl IsNot XenonGroupBox And TypeOf ctrl IsNot XenonCP Then
                        If _Conditions.RetroAppWorkspace Or _Conditions.RetroBackground Then ctrl.BackColor = Color.FromArgb(255, ColorEditorManager1.Color)
                        If TypeOf ctrl Is RetroPanel Then
                            If _Conditions.RetroButtonHilight Then DirectCast(ctrl, RetroPanel).ButtonHilight = Color.FromArgb(255, ColorEditorManager1.Color)
                            If _Conditions.RetroButtonShadow Then DirectCast(ctrl, RetroPanel).ButtonShadow = Color.FromArgb(255, ColorEditorManager1.Color)
                        End If
                        If TypeOf ctrl Is Panel Then
                            ctrl.BackColor = Color.FromArgb(255, ColorEditorManager1.Color)
                        End If
                    End If
                End If

                If TypeOf ctrl Is XenonCP Then
                    Visual.FadeColor(ctrl, "BackColor", ctrl.BackColor, ColorEditorManager1.Color, steps, delay)
                End If
                ctrl.Refresh()

            ElseIf TypeOf ctrl Is RetroTextBox Then
                With DirectCast(ctrl, RetroTextBox)
                    If _Conditions.RetroWindowText Then
                        ctrl.ForeColor = Color.FromArgb(ctrl.ForeColor.A, ColorEditorManager1.Color)
                    Else
                        ctrl.BackColor = Color.FromArgb(ctrl.BackColor.A, ColorEditorManager1.Color)
                    End If
                    ctrl.Refresh()
                End With

            ElseIf TypeOf ctrl Is XenonTerminal Then
                With DirectCast(ctrl, XenonTerminal)

                    If _Conditions.Terminal_Back Then
                        .Color_Background = Color.FromArgb(255, ColorEditorManager1.Color)

                    ElseIf _Conditions.Terminal_Fore Then
                        .Color_Foreground = Color.FromArgb(255, ColorEditorManager1.Color)

                    ElseIf _Conditions.Terminal_Selection Then
                        .Color_Selection = Color.FromArgb(255, ColorEditorManager1.Color)

                    ElseIf _Conditions.Terminal_Cursor Then
                        .Color_Cursor = Color.FromArgb(255, ColorEditorManager1.Color)

                    ElseIf _Conditions.Terminal_TabColor Then
                        .TabColor = Color.FromArgb(255, ColorEditorManager1.Color)

                    ElseIf _Conditions.Terminal_TabActive Then
                        .Color_TabFocused = Color.FromArgb(255, ColorEditorManager1.Color)

                    ElseIf _Conditions.Terminal_TabInactive Then
                        .Color_TabUnFocused = Color.FromArgb(255, ColorEditorManager1.Color)

                    ElseIf _Conditions.Terminal_TitlebarActive Then
                        .Color_Titlebar = Color.FromArgb(255, ColorEditorManager1.Color)

                    ElseIf _Conditions.Terminal_TitlebarInactive Then
                        .Color_Titlebar_Unfocused = Color.FromArgb(255, ColorEditorManager1.Color)
                    End If

                    .Refresh()
                End With

            ElseIf TypeOf ctrl Is XenonCMD Then
                With DirectCast(ctrl, XenonCMD)
                    If _Conditions.CMD_ColorTable00 Then
                        .CMD_ColorTable00 = Color.FromArgb(255, ColorEditorManager1.Color)

                    ElseIf _Conditions.CMD_ColorTable01 Then
                        .CMD_ColorTable01 = Color.FromArgb(255, ColorEditorManager1.Color)

                    ElseIf _Conditions.CMD_ColorTable02 Then
                        .CMD_ColorTable02 = Color.FromArgb(255, ColorEditorManager1.Color)


                    ElseIf _Conditions.CMD_ColorTable03 Then
                        .CMD_ColorTable03 = Color.FromArgb(255, ColorEditorManager1.Color)


                    ElseIf _Conditions.CMD_ColorTable04 Then
                        .CMD_ColorTable04 = Color.FromArgb(255, ColorEditorManager1.Color)


                    ElseIf _Conditions.CMD_ColorTable05 Then
                        .CMD_ColorTable05 = Color.FromArgb(255, ColorEditorManager1.Color)


                    ElseIf _Conditions.CMD_ColorTable06 Then
                        .CMD_ColorTable06 = Color.FromArgb(255, ColorEditorManager1.Color)


                    ElseIf _Conditions.CMD_ColorTable07 Then
                        .CMD_ColorTable07 = Color.FromArgb(255, ColorEditorManager1.Color)


                    ElseIf _Conditions.CMD_ColorTable08 Then
                        .CMD_ColorTable08 = Color.FromArgb(255, ColorEditorManager1.Color)


                    ElseIf _Conditions.CMD_ColorTable09 Then
                        .CMD_ColorTable09 = Color.FromArgb(255, ColorEditorManager1.Color)


                    ElseIf _Conditions.CMD_ColorTable10 Then
                        .CMD_ColorTable10 = Color.FromArgb(255, ColorEditorManager1.Color)


                    ElseIf _Conditions.CMD_ColorTable11 Then
                        .CMD_ColorTable11 = Color.FromArgb(255, ColorEditorManager1.Color)


                    ElseIf _Conditions.CMD_ColorTable12 Then
                        .CMD_ColorTable12 = Color.FromArgb(255, ColorEditorManager1.Color)


                    ElseIf _Conditions.CMD_ColorTable13 Then
                        .CMD_ColorTable13 = Color.FromArgb(255, ColorEditorManager1.Color)


                    ElseIf _Conditions.CMD_ColorTable14 Then
                        .CMD_ColorTable14 = Color.FromArgb(255, ColorEditorManager1.Color)


                    ElseIf _Conditions.CMD_ColorTable15 Then
                        .CMD_ColorTable15 = Color.FromArgb(255, ColorEditorManager1.Color)
                    Else
                        .CMD_ColorTable00 = Color.FromArgb(255, ColorEditorManager1.Color)
                    End If

                    .Refresh()
                End With

            ElseIf TypeOf ctrl Is CursorControl Then
                With DirectCast(ctrl, CursorControl)
                    If _Conditions.CursorBack1 Then
                        .Prop_PrimaryColor1 = ColorEditorManager1.Color
                    ElseIf _Conditions.CursorBack2 Then
                        .Prop_PrimaryColor2 = ColorEditorManager1.Color

                    ElseIf _Conditions.CursorLine1 Then
                        .Prop_SecondaryColor1 = ColorEditorManager1.Color
                    ElseIf _Conditions.CursorLine2 Then
                        .Prop_SecondaryColor2 = ColorEditorManager1.Color

                    ElseIf _Conditions.CursorCircle1 Then
                        .Prop_LoadingCircleBack1 = ColorEditorManager1.Color
                    ElseIf _Conditions.CursorCircle2 Then
                        .Prop_LoadingCircleBack2 = ColorEditorManager1.Color

                    ElseIf _Conditions.CursorCircleHot1 Then
                        .Prop_LoadingCircleHot1 = ColorEditorManager1.Color
                    ElseIf _Conditions.CursorCircleHot2 Then
                        .Prop_LoadingCircleHot2 = ColorEditorManager1.Color
                    ElseIf _Conditions.CursorShadow Then
                        .Prop_Shadow_Color = ColorEditorManager1.Color

                    End If

                    .Refresh()
                End With

            Else
                Try
                    Visual.FadeColor(ctrl, "backcolor", ctrl.BackColor, Color.FromArgb(255, ColorEditorManager1.Color), steps, delay)
                Catch
                    Try
                        ctrl.BackColor = Color.FromArgb(255, ColorEditorManager1.Color)
                    Catch
                    End Try
                End Try
            End If

        Next

        If (My.WVista Or My.W7 Or My.W8 Or My.W81) And My.Settings.Miscellaneous.Win7LivePreview Then
            If _Conditions.Win7LivePreview_Colorization Then
                UpdateWin7Preview(ColorEditorManager1.Color, My.CP.Windows7.ColorizationAfterglow)
            End If

            If _Conditions.Win7LivePreview_AfterGlow Then
                UpdateWin7Preview(My.CP.Windows7.ColorizationColor, ColorEditorManager1.Color)
            End If
        End If
    End Sub

#Region "DWM Windows 7 Live Preview"
    Public Shared Sub UpdateWin7Preview(Color1 As Color, Color2 As Color)
        Try
            Dim Com As Boolean
            Dwmapi.DwmIsCompositionEnabled(Com)

            If Com Then
                Dim temp As New Dwmapi.DWM_COLORIZATION_PARAMS With {
                    .clrColor = Color1.ToArgb,
                    .clrAfterGlow = Color2.ToArgb}

                If My.PreviewStyle = WindowStyle.W81 Then
                    temp.nIntensity = My.CP.Windows81.ColorizationColorBalance

                ElseIf My.PreviewStyle = WindowStyle.W7 Then
                    temp.nIntensity = My.CP.Windows7.ColorizationColorBalance

                    temp.clrAfterGlowBalance = My.CP.Windows7.ColorizationAfterglowBalance
                    temp.clrBlurBalance = My.CP.Windows7.ColorizationBlurBalance
                    temp.clrGlassReflectionIntensity = My.CP.Windows7.ColorizationGlassReflectionIntensity
                    temp.fOpaque = (My.CP.Windows7.Theme = Structures.Windows7.Themes.AeroOpaque)

                ElseIf My.PreviewStyle = WindowStyle.WVista Then
                    temp.clrColor = Color.FromArgb(My.CP.WindowsVista.Alpha, My.CP.WindowsVista.ColorizationColor).ToArgb
                    temp.clrAfterGlowBalance = Color.FromArgb(My.CP.WindowsVista.Alpha, My.CP.WindowsVista.ColorizationColor).ToArgb

                    'temp.nIntensity = My.CP.WindowsVista.ColorizationColorBalance
                    'temp.clrBlurBalance = My.CP.WindowsVista.ColorizationBlurBalance
                    'temp.clrGlassReflectionIntensity = My.CP.WindowsVista.ColorizationGlassReflectionIntensity
                    temp.fOpaque = (My.CP.WindowsVista.Theme = Structures.Windows7.Themes.AeroOpaque)
                End If

                Dwmapi.DwmSetColorizationParameters(temp, False)
            End If
        Catch
        End Try
    End Sub


#End Region

    Private Sub XenonButton3_Click(sender As Object, e As EventArgs) Handles XenonButton3.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub XenonButton6_Click(sender As Object, e As EventArgs) Handles XenonButton6.Click


        Select Case XenonRadioButton1.Checked
            Case True
                img = My.Wallpaper_Unscaled

            Case False
                img = Bitmap_Mgr.Load(TextBox1.Text)

        End Select

        If XenonCheckBox2.Checked Then img = img.Resize(MainFrm.pnl_preview.Size)

        If img IsNot Nothing Then
            Label4.Text = My.Lang.Extracting
            My.Animator.HideSync(XenonButton6, True)
            My.Animator.HideSync(ImgPaletteContainer, True)
            ProgressBar1.Visible = True
            Colors_List.Clear()

            Try
                BackgroundWorker1.CancelAsync()
                BackgroundWorker1.RunWorkerAsync()
            Catch : End Try
        End If
    End Sub

    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        If img IsNot Nothing Then
            Dim ColorThiefX As New ColorThiefDotNet.ColorThief
            Dim Colors As List(Of ColorThiefDotNet.QuantizedColor) = ColorThiefX.GetPalette(img, XenonTrackbar1.Value, XenonTrackbar2.Value, XenonCheckBox1.Checked)
            For Each C As ColorThiefDotNet.QuantizedColor In Colors
                Colors_List.Add(Color.FromArgb(255, C.Color.R, C.Color.G, C.Color.B))
            Next
            GC.Collect()
            GC.SuppressFinalize(Colors)
            GC.SuppressFinalize(ColorThiefX)
            img.Dispose()
        End If
    End Sub

    Private Sub BackgroundWorker1_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted

        For Each ctrl As XenonCP In ImgPaletteContainer.Controls.OfType(Of XenonCP)
            ctrl.Dispose()
        Next
        ImgPaletteContainer.Controls.Clear()

        Colors_List = Colors_List.Distinct.ToList
        Colors_List.Sort(New RGBColorComparer())

        For Each C As Color In Colors_List
            Dim pnl As New XenonCP With {
                    .Size = New Size(If(My.Settings.NerdStats.Enabled, 80, 30), 20),
                    .BackColor = Color.FromArgb(255, C),
                    .DefaultColor = .BackColor
                }
            ImgPaletteContainer.Controls.Add(pnl)
            AddHandler pnl.Click, AddressOf Pnl_click
        Next

        ProgressBar1.Visible = False
        Colors_List.Clear()
        My.Animator.ShowSync(ImgPaletteContainer, True)
        My.Animator.ShowSync(XenonButton6, True)
    End Sub

    Private Sub Pnl_click(sender As Object, e As EventArgs)
        With CType(sender, XenonCP)
            ColorEditorManager1.Color = .BackColor
            ColorEditor1.Color = .BackColor
            ColorGrid1.Color = .BackColor
            ColorWheel1.Color = .BackColor
        End With

        Change_Color_Preview()
    End Sub

    Private Sub XenonButton5_Click(sender As Object, e As EventArgs)
        ColorWheel1.Visible = False
        ColorGrid1.Visible = False
    End Sub

    Private Sub XenonButton4_Click_1(sender As Object, e As EventArgs) Handles XenonButton4.Click
        If OpenFileDialog1.ShowDialog = DialogResult.OK Then
            TextBox1.Text = OpenFileDialog1.FileName
        End If
    End Sub

    Private Sub XenonButton1_Click_1(sender As Object, e As EventArgs) Handles XenonButton1.Click
        If OpenFileDialog2.ShowDialog = DialogResult.OK Then
            ColorGrid1.Colors = ColorCollection.LoadPalette(OpenFileDialog2.FileName)
        End If
    End Sub

    Private Sub XenonButton7_Click(sender As Object, e As EventArgs) Handles XenonButton7.Click
        If OpenThemeDialog.ShowDialog = DialogResult.OK Then
            XenonTextBox1.Text = OpenThemeDialog.FileName
        End If
    End Sub

    Private Sub XenonButton2_Click(sender As Object, e As EventArgs) Handles XenonButton2.Click
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub XenonComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles XenonComboBox2.SelectedIndexChanged
        Select Case XenonComboBox2.SelectedIndex
            Case 0
                GetColorsFromPalette(My.CP)
            Case 1
                GetColorsFromPalette(New CP_Defaults().Default_Windows11)
            Case 2
                GetColorsFromPalette(New CP_Defaults().Default_Windows10)
            Case 3
                GetColorsFromPalette(New CP_Defaults().Default_Windows81)
            Case 4
                GetColorsFromPalette(New CP_Defaults().Default_WindowsVista)
            Case 5
                GetColorsFromPalette(New CP_Defaults().Default_WindowsXP)
            Case 6
                GetColorsFromPalette(New CP_Defaults().Default_Windows7)
            Case Else
                GetColorsFromPalette(My.CP)
        End Select
    End Sub

    Private Sub ColorPickerDlg_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        If DialogResult <> DialogResult.OK And (My.WVista Or My.W7 Or My.W8 Or My.W81) And My.Settings.Miscellaneous.Win7LivePreview Then
            If _Conditions.Win7LivePreview_Colorization Then
                UpdateWin7Preview(InitColor, My.CP.Windows7.ColorizationAfterglow)
            End If

            If _Conditions.Win7LivePreview_AfterGlow Then
                UpdateWin7Preview(My.CP.Windows7.ColorizationColor, InitColor)
            End If
        End If
    End Sub

    Private Sub XenonComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles XenonComboBox1.SelectedIndexChanged
        If XenonComboBox1.SelectedItem Is Nothing Then Exit Sub


        For Each c As XenonCP In ThemePaletteContainer.Controls.OfType(Of XenonCP)
            c.Dispose()
            RemoveHandler c.Click, AddressOf Pnl_click
            ThemePaletteContainer.Controls.Remove(c)
        Next

        ThemePaletteContainer.Controls.Clear()

        Try
            If Not String.IsNullOrWhiteSpace(XenonComboBox1.SelectedItem) Then
                For Each C As Color In CP.GetPaletteFromString(My.Resources.RetroThemesDB, XenonComboBox1.SelectedItem)
                    Dim pnl As New XenonCP With {
                        .Size = New Drawing.Size(If(My.Settings.NerdStats.Enabled, 80, 30), 20),
                        .BackColor = Color.FromArgb(255, C),
                        .DefaultColor = .BackColor
                        }
                    ThemePaletteContainer.Controls.Add(pnl)
                    AddHandler pnl.Click, AddressOf Pnl_click
                Next
            End If
        Catch

        End Try
    End Sub

    Private Sub Ttl_h_Click(sender As Object, e As EventArgs) Handles val1.Click
        Dim response As String = InputBox(My.Lang.InputValue, sender.text, My.Lang.ItMustBeNumerical)
        sender.Text = Math.Max(Math.Min(Val(response), XenonTrackbar1.Maximum), XenonTrackbar1.Minimum) : XenonTrackbar1.Value = Val(sender.Text)
    End Sub

    Private Sub XenonButton8_Click(sender As Object, e As EventArgs) Handles val2.Click
        Dim response As String = InputBox(My.Lang.InputValue, sender.text, My.Lang.ItMustBeNumerical)
        sender.Text = Math.Max(Math.Min(Val(response), XenonTrackbar2.Maximum), XenonTrackbar2.Minimum) : XenonTrackbar2.Value = Val(sender.Text)
    End Sub

    Private Sub XenonTrackbar1_Scroll(sender As Object) Handles XenonTrackbar1.Scroll
        val1.Text = sender.Value
    End Sub

    Private Sub XenonTrackbar2_Scroll(sender As Object) Handles XenonTrackbar2.Scroll
        val2.Text = sender.Value
    End Sub

    Private Sub XenonTextBox1_TextChanged(sender As Object, e As EventArgs) Handles XenonTextBox1.TextChanged
        If IO.File.Exists(XenonTextBox1.Text) Then
            If Path.GetExtension(XenonTextBox1.Text).ToLower = ".theme" Then
                ThemePaletteContainer.Controls.Clear()

                Try
                    For Each C As Color In CP.GetPaletteFromMSTheme(XenonTextBox1.Text)
                        Dim pnl As New XenonCP With {
                            .Size = New Drawing.Size(If(My.Settings.NerdStats.Enabled, 80, 30), 20),
                            .BackColor = Color.FromArgb(255, C),
                            .DefaultColor = .BackColor
                        }
                        ThemePaletteContainer.Controls.Add(pnl)
                        AddHandler pnl.Click, AddressOf Pnl_click
                    Next
                Catch
                    MsgBox(My.Lang.InvalidTheme, MsgBoxStyle.Critical)
                End Try

            ElseIf Path.GetExtension(XenonTextBox1.Text).ToLower = ".msstyles" Then
                Try
                    IO.File.WriteAllText(My.PATH_appData & "\VisualStyles\Luna\win32uischeme.theme", String.Format("[VisualStyles]{1}Path={0}{1}ColorStyle=NormalColor{1}Size=NormalSize", XenonTextBox1.Text, vbCrLf))

                    Dim vs As New VisualStyleFile(My.PATH_appData & "\VisualStyles\Luna\win32uischeme.theme")

                    For Each field In GetType(VisualStyleMetricColors).GetFields(BindingFlags.Instance Or BindingFlags.NonPublic Or BindingFlags.Public)
                        If field.FieldType.Name.ToLower = "color" Then

                            Dim pnl As New XenonCP With {
                                .Size = New Drawing.Size(If(My.Settings.NerdStats.Enabled, 80, 30), 20),
                                .BackColor = field.GetValue(vs.Metrics.Colors),
                                .DefaultColor = .BackColor
                                }
                            ThemePaletteContainer.Controls.Add(pnl)
                            AddHandler pnl.Click, AddressOf Pnl_click

                        End If
                    Next

                Catch
                    MsgBox(My.Lang.InvalidTheme, MsgBoxStyle.Critical)
                End Try
            Else
                MsgBox(My.Lang.InvalidTheme, MsgBoxStyle.Critical)
            End If
        End If
    End Sub

End Class

Public Class Conditions

    Public Sub New()

    End Sub

    Public Property Terminal_Back As Boolean = False
    Public Property Terminal_Fore As Boolean = False
    Public Property Terminal_Selection As Boolean = False
    Public Property Terminal_Cursor As Boolean = False
    Public Property Terminal_TabColor As Boolean = False
    Public Property Terminal_TabActive As Boolean = False
    Public Property Terminal_TabInactive As Boolean = False
    Public Property Terminal_TitlebarActive As Boolean = False
    Public Property Terminal_TitlebarInactive As Boolean = False
    Public Property CMD_ColorTable00 As Boolean = False
    Public Property CMD_ColorTable01 As Boolean = False
    Public Property CMD_ColorTable02 As Boolean = False
    Public Property CMD_ColorTable03 As Boolean = False
    Public Property CMD_ColorTable04 As Boolean = False
    Public Property CMD_ColorTable05 As Boolean = False
    Public Property CMD_ColorTable06 As Boolean = False
    Public Property CMD_ColorTable07 As Boolean = False
    Public Property CMD_ColorTable08 As Boolean = False
    Public Property CMD_ColorTable09 As Boolean = False
    Public Property CMD_ColorTable10 As Boolean = False
    Public Property CMD_ColorTable11 As Boolean = False
    Public Property CMD_ColorTable12 As Boolean = False
    Public Property CMD_ColorTable13 As Boolean = False
    Public Property CMD_ColorTable14 As Boolean = False
    Public Property CMD_ColorTable15 As Boolean = False

    Public Property Win7LivePreview_Colorization As Boolean = False
    Public Property Win7LivePreview_AfterGlow As Boolean = False
    Public Property Win7 As Boolean = False
    Public Property Color1 As Boolean = False
    Public Property Color2 As Boolean = False
    Public Property BackColor1 As Boolean = False
    Public Property BackColor2 As Boolean = False
    Public Property Window_InactiveTitlebar As Boolean = False
    Public Property Window_ActiveTitlebar As Boolean = True
    Public Property AppUnderlineOnly As Boolean = False
    Public Property AppUnderlineWithTaskbar As Boolean = False
    Public Property AppBackgroundOnly As Boolean = False
    Public Property StartColorOnly As Boolean = False
    Public Property ActionCenterBtn As Boolean = False
    Public Property ActionCenterLink As Boolean = False
    Public Property RetroWindowColor1 As Boolean = False
    Public Property RetroWindowColor2 As Boolean = False
    Public Property RetroWindowForeColor As Boolean = False
    Public Property RetroWindowBorder As Boolean = False
    Public Property RetroWindowFrame As Boolean = False
    Public Property RetroButtonFace As Boolean = False
    Public Property RetroButtonDkShadow As Boolean = False
    Public Property RetroButtonShadow As Boolean = False
    Public Property RetroButtonHilight As Boolean = False
    Public Property RetroButtonLight As Boolean = False
    Public Property RetroButtonText As Boolean = False
    Public Property RetroAppWorkspace As Boolean = False
    Public Property RetroBackground As Boolean = False
    Public Property RetroWindowText As Boolean = False
    Public Property RetroHighlight17BitFixer As Boolean = False
    Public Property CursorBack1 As Boolean = False
    Public Property CursorBack2 As Boolean = False
    Public Property CursorLine1 As Boolean = False
    Public Property CursorLine2 As Boolean = False
    Public Property CursorCircle1 As Boolean = False
    Public Property CursorCircle2 As Boolean = False
    Public Property CursorCircleHot1 As Boolean = False
    Public Property CursorCircleHot2 As Boolean = False
    Public Property CursorShadow As Boolean = False

End Class

