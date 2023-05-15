Imports Devcorp.Controls.VisualStyles
Imports WinPaletter.CP
Imports WinPaletter.NativeMethods
Imports WinPaletter.PreviewHelpers
Imports WinPaletter.XenonCore

Public Class DragPreviewer

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
                Exit Select
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

    Public File As String

    Private Sub DragPreviewer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ApplyDarkMode(Me)

        Using CP As New CP(CP_Type.File, File, True, True)
            ApplyColorsToElements(CP)
            ApplyStylesToElements(CP)
        End Using

        XenonAlertBox11.Text = MainFrm.WXP_Alert2.Text
        XenonAlertBox11.Size = XenonAlertBox11.Parent.Size - New Size(40, 40)
        XenonAlertBox11.Location = New Point(20, 20)

        Invalidate()
    End Sub

    Private Sub DragPreviewer_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Invalidate()
    End Sub

    Sub ApplyColorsToElements([CP] As CP)
        ApplyWinElementsColors([CP], My.PreviewStyle, False, taskbar, start, ActionCenter, setting_icon_preview, Label8, lnk_preview)
        ApplyWindowStyles([CP], My.PreviewStyle, XenonWindow1, XenonWindow2)
    End Sub

    Sub ApplyStylesToElements([CP] As CP)
        ApplyWinElementsStyle([CP], My.PreviewStyle, taskbar, start, ActionCenter,
                           XenonWindow1, XenonWindow2, Panel3, lnk_preview,
                           ClassicTaskbar, RetroButton2, RetroButton3, RetroButton4, ClassicWindow1, ClassicWindow2,
                           MainFrm.WXP_VS_ReplaceColors.Checked, MainFrm.WXP_VS_ReplaceMetrics.Checked, MainFrm.WXP_VS_ReplaceFonts.Checked)

        pnl_preview.BackgroundImage = My.Application.FetchSuitableWallpaper([CP], My.PreviewStyle)
        pnl_preview_classic.BackgroundImage = pnl_preview.BackgroundImage

        AdjustPreview_ModernOrClassic([CP], My.PreviewStyle, tabs_preview, XenonAlertBox11)
    End Sub

    Sub SetClassicMetrics(CP As CP)
        Try
            If My.PreviewStyle = WindowStyle.WXP AndAlso MainFrm.WXP_VS_ReplaceMetrics.Checked And CP.WindowsXP.Theme <> Structures.WindowsXP.Themes.Classic Then
                If IO.File.Exists(My.VS) And Not String.IsNullOrEmpty(My.VS) Then
                    Dim vs As New VisualStyleFile(My.VS)
                    CP.MetricsFonts.Overwrite_Metrics(vs.Metrics)
                End If

                If IO.File.Exists(My.VS) And Not String.IsNullOrEmpty(My.VS) Then
                    Dim vs As New VisualStyleFile(My.VS)
                    CP.MetricsFonts.Overwrite_Fonts(vs.Metrics)
                End If
            End If
        Catch
        End Try

        RetroPanel2.Width = CP.MetricsFonts.ScrollWidth
        menucontainer0.Height = CP.MetricsFonts.MenuHeight

        menucontainer0.Height = Math.Max(CP.MetricsFonts.MenuHeight, Metrics_Fonts.GetTitleTextHeight(CP.MetricsFonts.MenuFont))

        RetroLabel1.Font = CP.MetricsFonts.MenuFont
        RetroLabel2.Font = CP.MetricsFonts.MenuFont
        RetroLabel3.Font = CP.MetricsFonts.MenuFont

        RetroLabel9.Font = CP.MetricsFonts.MenuFont
        RetroLabel5.Font = CP.MetricsFonts.MenuFont
        RetroLabel6.Font = CP.MetricsFonts.MenuFont

        menucontainer1.Height = Metrics_Fonts.GetTitleTextHeight(CP.MetricsFonts.MenuFont) + 3
        highlight.Height = menucontainer1.Height + 1
        menucontainer3.Height = menucontainer1.Height + 1
        Menu_Window.Height = menucontainer1.Height + highlight.Height + menucontainer3.Height + Menu_Window.Padding.Top + Menu_Window.Padding.Bottom

        RetroLabel4.Font = CP.MetricsFonts.MessageFont

        RetroLabel1.Width = RetroLabel1.Text.Measure(CP.MetricsFonts.MenuFont).Width + 5
        RetroLabel2.Width = RetroLabel2.Text.Measure(CP.MetricsFonts.MenuFont).Width + 5
        RetroPanel1.Width = RetroLabel3.Text.Measure(CP.MetricsFonts.MenuFont).Width + 5 + RetroPanel1.Padding.Left + RetroPanel1.Padding.Right

        Dim TitleTextH, TitleTextH_9, TitleTextH_Sum As Integer
        TitleTextH = "ABCabc0123xYz.#".Measure(CP.MetricsFonts.CaptionFont).Height
        TitleTextH_9 = "ABCabc0123xYz.#".Measure(New Font(CP.MetricsFonts.CaptionFont.Name, 9, Font.Style)).Height
        TitleTextH_Sum = Math.Max(0, TitleTextH - TitleTextH_9 - 5)

        Dim iP As Integer = 3 + CP.MetricsFonts.PaddedBorderWidth + CP.MetricsFonts.BorderWidth
        Dim iT As Integer = 4 + CP.MetricsFonts.PaddedBorderWidth + CP.MetricsFonts.BorderWidth + CP.MetricsFonts.CaptionHeight + TitleTextH_Sum
        Dim _Padding As New Windows.Forms.Padding(iP, iT, iP, iP)

        For Each RW As RetroWindow In pnlRetroPreview.Controls.OfType(Of RetroWindow)
            If Not RW.UseItAsMenu Then
                RW.Metrics_BorderWidth = CP.MetricsFonts.BorderWidth
                RW.Metrics_CaptionHeight = CP.MetricsFonts.CaptionHeight
                RW.Metrics_CaptionWidth = CP.MetricsFonts.CaptionWidth
                RW.Metrics_PaddedBorderWidth = CP.MetricsFonts.PaddedBorderWidth
                RW.Font = CP.MetricsFonts.CaptionFont

                RW.Padding = _Padding
            End If
        Next

        RetroWindow3.Height = 85 + CP.MetricsFonts.PaddedBorderWidth + CP.MetricsFonts.BorderWidth + RetroWindow3.GetTitleTextHeight
        RetroWindow2.Height = 120 + CP.MetricsFonts.PaddedBorderWidth + CP.MetricsFonts.BorderWidth + RetroWindow2.GetTitleTextHeight + CP.MetricsFonts.MenuHeight


        Menu_Window.Top = RetroWindow2.Top + menucontainer0.Top + menucontainer0.Height
        Menu_Window.Left = Math.Min(RetroWindow2.Left + menucontainer0.Left + RetroPanel1.Left + +3, RetroWindow2.Right - CP.MetricsFonts.PaddedBorderWidth - CP.MetricsFonts.BorderWidth)

        RetroWindow3.Top = RetroWindow2.Top + RetroTextBox1.Top + RetroTextBox1.Font.Height + 10
        RetroWindow3.Left = RetroWindow2.Left + RetroTextBox1.Left + 15

        RetroLabel13.Top = RetroWindow4.Top + RetroWindow4.Metrics_CaptionHeight + 2
        RetroLabel13.Left = RetroWindow4.Right - RetroWindow4.Metrics_CaptionWidth - 2

        RetroShadow1.Visible = CP.WindowsEffects.WindowShadow
    End Sub

    Sub ApplyRetroPreview([CP] As CP)
        Try
            If My.PreviewStyle = WindowStyle.WXP AndAlso MainFrm.WXP_VS_ReplaceColors.Checked And CP.WindowsXP.Theme <> CP.Structures.WindowsXP.Themes.Classic Then
                If IO.File.Exists(My.VS) And Not String.IsNullOrEmpty(My.VS) Then
                    Dim vs As New VisualStyleFile(My.VS)
                    CP.Win32.Load(Structures.Win32UI.Method.VisualStyles, vs.Metrics)
                End If
            End If
        Catch
        End Try

        RetroWindow1.ColorGradient = [CP].Win32.EnableGradient
        RetroWindow2.ColorGradient = [CP].Win32.EnableGradient
        RetroWindow3.ColorGradient = [CP].Win32.EnableGradient
        RetroWindow4.ColorGradient = [CP].Win32.EnableGradient

        Dim c As Color
        c = [CP].Win32.ActiveTitle
        RetroWindow2.Color1 = c
        RetroWindow3.Color1 = c
        RetroWindow4.Color1 = c

        c = [CP].Win32.GradientActiveTitle
        RetroWindow2.Color2 = c
        RetroWindow3.Color2 = c
        RetroWindow4.Color2 = c

        c = [CP].Win32.TitleText
        RetroWindow2.ForeColor = c
        RetroWindow3.ForeColor = c
        RetroWindow4.ForeColor = c

        c = [CP].Win32.InactiveTitle
        RetroWindow1.Color1 = c

        c = [CP].Win32.GradientInactiveTitle
        RetroWindow1.Color2 = c

        c = [CP].Win32.InactiveTitleText
        RetroWindow1.ForeColor = c

        c = [CP].Win32.ActiveBorder
        RetroWindow2.ColorBorder = c
        RetroWindow3.ColorBorder = c
        RetroWindow4.ColorBorder = c

        c = [CP].Win32.InactiveBorder
        RetroWindow1.ColorBorder = c

        c = [CP].Win32.WindowFrame
        For Each RW As RetroWindow In pnlRetroPreview.Controls.OfType(Of RetroWindow)
            For Each RB As RetroButton In RW.Controls.OfType(Of RetroButton)
                RB.WindowFrame = c
            Next
        Next
        For Each RB As RetroButton In RetroPanel2.Controls.OfType(Of RetroButton)
            RB.WindowFrame = c
        Next

        c = [CP].Win32.ButtonFace
        For Each RW As RetroWindow In pnlRetroPreview.Controls.OfType(Of RetroWindow)
            If RW IsNot Menu Then RW.BackColor = c
            For Each RB As RetroButton In RW.Controls.OfType(Of RetroButton)
                RB.BackColor = c
            Next
        Next
        For Each RB As RetroButton In RetroPanel2.Controls.OfType(Of RetroButton)
            RB.BackColor = c
        Next
        RetroPanel2.BackColor = c
        Menu_Window.ButtonFace = c

        c = [CP].Win32.ButtonDkShadow
        For Each RW As RetroWindow In pnlRetroPreview.Controls.OfType(Of RetroWindow)
            RW.ButtonDkShadow = c
            For Each RB As RetroButton In RW.Controls.OfType(Of RetroButton)
                RB.ButtonDkShadow = c
            Next
        Next
        For Each RB As RetroButton In RetroPanel2.Controls.OfType(Of RetroButton)
            RB.ButtonDkShadow = c
        Next
        RetroTextBox1.ButtonDkShadow = c
        Menu_Window.ButtonDkShadow = c

        c = [CP].Win32.ButtonHilight
        For Each RW As RetroWindow In pnlRetroPreview.Controls.OfType(Of RetroWindow)
            RW.ButtonHilight = c
            For Each RB As RetroButton In RW.Controls.OfType(Of RetroButton)
                RB.ButtonHilight = c
            Next
        Next
        For Each RB As RetroButton In RetroPanel2.Controls.OfType(Of RetroButton)
            RB.ButtonHilight = c
        Next
        For Each RB As RetroPanelRaised In pnlRetroPreview.Controls.OfType(Of RetroPanelRaised)
            RB.ButtonHilight = c
        Next
        RetroTextBox1.ButtonHilight = c
        RetroPanel1.ButtonHilight = c
        RetroPanel2.ButtonHilight = c
        Menu_Window.ButtonHilight = c

        c = [CP].Win32.ButtonLight
        For Each RW As RetroWindow In pnlRetroPreview.Controls.OfType(Of RetroWindow)
            RW.ButtonLight = c
            For Each RB As RetroButton In RW.Controls.OfType(Of RetroButton)
                RB.ButtonLight = c
            Next
        Next
        For Each RB As RetroButton In RetroPanel2.Controls.OfType(Of RetroButton)
            RB.ButtonLight = c
        Next
        RetroTextBox1.ButtonLight = c
        Menu_Window.ButtonLight = c

        c = [CP].Win32.ButtonShadow
        For Each RW As RetroWindow In pnlRetroPreview.Controls.OfType(Of RetroWindow)
            RW.ButtonShadow = c
            For Each RB As RetroButton In RW.Controls.OfType(Of RetroButton)
                RB.ButtonShadow = c
            Next
        Next
        For Each RB As RetroButton In RetroPanel2.Controls.OfType(Of RetroButton)
            RB.ButtonShadow = c
        Next
        For Each RB As RetroPanelRaised In pnlRetroPreview.Controls.OfType(Of RetroPanelRaised)
            RB.ButtonShadow = c
        Next
        RetroTextBox1.ButtonShadow = c
        RetroPanel1.ButtonShadow = c
        RetroTextBox1.Invalidate()
        Menu_Window.ButtonShadow = c

        c = [CP].Win32.ButtonText
        For Each RW As RetroWindow In pnlRetroPreview.Controls.OfType(Of RetroWindow)
            RW.ButtonText = c
            For Each RB As RetroButton In RW.Controls.OfType(Of RetroButton)
                RB.ForeColor = c
            Next
        Next
        For Each RB As RetroButton In RetroPanel2.Controls.OfType(Of RetroButton)
            RB.ForeColor = c
        Next

        c = [CP].Win32.AppWorkspace
        programcontainer.BackColor = c

        c = [CP].Win32.Background
        pnlRetroPreview.BackColor = c

        c = [CP].Win32.Menu
        Menu_Window.BackColor = c
        RetroPanel1.BackColor = c
        Menu_Window.Invalidate()

        c = [CP].Win32.MenuBar
        menucontainer0.BackColor = c

        c = [CP].Win32.Hilight
        highlight.BackColor = c

        c = [CP].Win32.MenuHilight
        menuhilight.BackColor = c

        c = [CP].Win32.MenuText
        RetroLabel6.ForeColor = c
        RetroLabel1.ForeColor = c

        c = [CP].Win32.HilightText
        RetroLabel5.ForeColor = c

        c = [CP].Win32.GrayText
        RetroLabel2.ForeColor = c
        RetroLabel9.ForeColor = c

        c = [CP].Win32.Window
        RetroTextBox1.BackColor = c

        c = [CP].Win32.WindowText
        RetroTextBox1.ForeColor = c
        RetroLabel4.ForeColor = c

        c = [CP].Win32.InfoWindow
        RetroLabel13.BackColor = c

        c = [CP].Win32.InfoText
        RetroLabel13.ForeColor = c

        For Each RW As RetroWindow In pnlRetroPreview.Controls.OfType(Of RetroWindow)
            RW.Invalidate()
            For Each RB As RetroButton In RW.Controls.OfType(Of RetroButton)
                RB.Invalidate()
            Next
        Next

        For Each RB As RetroButton In RetroPanel2.Controls.OfType(Of RetroButton)
            RB.Invalidate()
        Next

        Refresh17BitPreference([CP])

        RetroShadow1.Refresh()
    End Sub

    Sub Refresh17BitPreference([CP] As CP)

        If [CP].Win32.EnableTheming Then
            'Theming Enabled (Menus Has colors and borders)
            Menu_Window.Flat = True
            RetroPanel1.Flat = True
            menuhilight.BackColor = [CP].Win32.MenuHilight  'Filling of selected item
            highlight.BackColor = [CP].Win32.Hilight 'Outer Border of selected item

            RetroPanel1.BackColor = [CP].Win32.MenuHilight
            RetroPanel1.ButtonShadow = [CP].Win32.Hilight

            menucontainer0.BackColor = [CP].Win32.MenuBar
            RetroLabel3.ForeColor = [CP].Win32.HilightText
        Else
            'Theming Disabled (Menus are retro 3d)
            Menu_Window.Flat = False
            RetroPanel1.Flat = False
            menuhilight.BackColor = [CP].Win32.Hilight 'Both will have same color
            highlight.BackColor = [CP].Win32.Hilight 'Both will have same color
            RetroPanel1.BackColor = [CP].Win32.Menu
            RetroPanel1.ButtonShadow = [CP].Win32.ButtonShadow
            menucontainer0.BackColor = [CP].Win32.Menu
            RetroLabel3.ForeColor = [CP].Win32.MenuText

        End If

        Menu_Window.Invalidate()
        RetroPanel1.Invalidate()
        menuhilight.Invalidate()
        highlight.Invalidate()

    End Sub

    Private Sub Menu_Window_SizeChanged(sender As Object, e As EventArgs) Handles Menu_Window.SizeChanged, Menu_Window.LocationChanged
        RetroShadow1.Size = Menu_Window.Size
        RetroShadow1.Location = Menu_Window.Location + New Point(6, 5)

        Dim b As New Bitmap(RetroShadow1.Width, RetroShadow1.Height)
        Dim g As Graphics = Graphics.FromImage(b)
        g.DrawGlow(New Rectangle(5, 5, b.Width - 10 - 1, b.Height - 10 - 1), Color.FromArgb(128, 0, 0, 0))
        g.Save()
        RetroShadow1.Image = b
        g.Dispose()

        RetroShadow1.BringToFront()
        Menu_Window.BringToFront()
    End Sub
End Class