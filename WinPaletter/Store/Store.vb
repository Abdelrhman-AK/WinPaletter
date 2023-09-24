Imports System.Net
Imports System.Security.Cryptography
Imports System.Text
Imports Devcorp.Controls.VisualStyles
Imports WinPaletter.CP
Imports WinPaletter.NativeMethods
Imports WinPaletter.PreviewHelpers
Imports WinPaletter.Core

Public Class Store

#Region "Variables"
    Private StartedAsOnlineOrOffline As Boolean = True
    Private FinishedLoadingInitialCPs As Boolean
    Dim CPList As New Dictionary(Of String, CP)
    ReadOnly w As Integer = 528 * 0.6
    ReadOnly h As Integer = 297 * 0.6

    Private apply_elapsedSecs As Integer = 0

    Private hoveredItem As UI.Controllers.StoreItem
    Public selectedItem As UI.Controllers.StoreItem

    Private _Shown As Boolean = False
    Private ReadOnly AnimateList As New List(Of CursorControl)
    Dim Angle As Single = 180
    ReadOnly Increment As Single = 5
    Dim Cycles As Integer = 0
    Dim WithEvents WebCL As New WebClient

    Private ReadOnly _Converter As New Converter
    Private ApplyOrEditToggle As Boolean = True
#End Region

#Region "Preview Subs"

    Sub Adjust_Preview(CP As CP)

        My.Wallpaper = My.Application.FetchSuitableWallpaper([CP], My.PreviewStyle)
        pnl_preview.BackgroundImage = My.Wallpaper
        pnl_preview_classic.BackgroundImage = My.Wallpaper

        ApplyWinElementsColors([CP], My.PreviewStyle, False, taskbar, start, ActionCenter, setting_icon_preview, Label8, lnk_preview)
        ApplyWindowStyles([CP], My.PreviewStyle, Window1, Window2)
        ApplyWinElementsStyle([CP], My.PreviewStyle, taskbar, start, ActionCenter,
                           Window1, Window2, Panel3, lnk_preview,
                           ClassicTaskbar, ButtonR2, ButtonR3, ButtonR4, ClassicWindow1, ClassicWindow2,
                           MainFrm.WXP_VS_ReplaceColors.Checked, MainFrm.WXP_VS_ReplaceMetrics.Checked, MainFrm.WXP_VS_ReplaceFonts.Checked)

        AdjustPreview_ModernOrClassic([CP], My.PreviewStyle, tabs_preview, WXP_Alert2)
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

    Sub SetClassicMetrics(CP As CP)
        Try
            If My.PreviewStyle = WindowStyle.WXP AndAlso MainFrm.WXP_VS_ReplaceMetrics.Checked And CP.WindowsXP.Theme <> CP.Structures.WindowsXP.Themes.Classic Then
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

        PanelR2.Width = CP.MetricsFonts.ScrollWidth
        menucontainer0.Height = CP.MetricsFonts.MenuHeight

        menucontainer0.Height = Math.Max(CP.MetricsFonts.MenuHeight, Metrics_Fonts.GetTitleTextHeight(CP.MetricsFonts.MenuFont))

        LabelR1.Font = CP.MetricsFonts.MenuFont
        LabelR2.Font = CP.MetricsFonts.MenuFont
        LabelR3.Font = CP.MetricsFonts.MenuFont

        LabelR9.Font = CP.MetricsFonts.MenuFont
        LabelR5.Font = CP.MetricsFonts.MenuFont
        LabelR6.Font = CP.MetricsFonts.MenuFont

        menucontainer1.Height = Metrics_Fonts.GetTitleTextHeight(CP.MetricsFonts.MenuFont) + 3
        highlight.Height = menucontainer1.Height + 1
        menucontainer3.Height = menucontainer1.Height + 1
        Menu_Window.Height = menucontainer1.Height + highlight.Height + menucontainer3.Height + Menu_Window.Padding.Top + Menu_Window.Padding.Bottom

        LabelR4.Font = CP.MetricsFonts.MessageFont

        LabelR1.Width = LabelR1.Text.Measure(CP.MetricsFonts.MenuFont).Width + 5
        LabelR2.Width = LabelR2.Text.Measure(CP.MetricsFonts.MenuFont).Width + 5
        PanelR1.Width = LabelR3.Text.Measure(CP.MetricsFonts.MenuFont).Width + 5 + PanelR1.Padding.Left + PanelR1.Padding.Right

        Dim TitleTextH, TitleTextH_9, TitleTextH_Sum As Integer
        TitleTextH = "ABCabc0123xYz.#".Measure(CP.MetricsFonts.CaptionFont).Height
        TitleTextH_9 = "ABCabc0123xYz.#".Measure(New Font(CP.MetricsFonts.CaptionFont.Name, 9, Font.Style)).Height
        TitleTextH_Sum = Math.Max(0, TitleTextH - TitleTextH_9 - 5)

        Dim iP As Integer = 3 + CP.MetricsFonts.PaddedBorderWidth + CP.MetricsFonts.BorderWidth
        Dim iT As Integer = 4 + CP.MetricsFonts.PaddedBorderWidth + CP.MetricsFonts.BorderWidth + CP.MetricsFonts.CaptionHeight + TitleTextH_Sum
        Dim _Padding As New Windows.Forms.Padding(iP, iT, iP, iP)

        For Each RW As UI.Retro.WindowR In ClassicColorsPreview.Controls.OfType(Of UI.Retro.WindowR)
            If Not RW.UseItAsMenu Then
                RW.Metrics_BorderWidth = CP.MetricsFonts.BorderWidth
                RW.Metrics_CaptionHeight = CP.MetricsFonts.CaptionHeight
                RW.Metrics_CaptionWidth = CP.MetricsFonts.CaptionWidth
                RW.Metrics_PaddedBorderWidth = CP.MetricsFonts.PaddedBorderWidth
                RW.Font = CP.MetricsFonts.CaptionFont

                RW.Padding = _Padding
            End If
        Next

        WindowR3.Height = 85 + CP.MetricsFonts.PaddedBorderWidth + CP.MetricsFonts.BorderWidth + WindowR3.GetTitleTextHeight
        WindowR2.Height = 120 + CP.MetricsFonts.PaddedBorderWidth + CP.MetricsFonts.BorderWidth + WindowR2.GetTitleTextHeight + CP.MetricsFonts.MenuHeight


        Menu_Window.Top = WindowR2.Top + menucontainer0.Top + menucontainer0.Height
        Menu_Window.Left = Math.Min(WindowR2.Left + menucontainer0.Left + PanelR1.Left + +3, WindowR2.Right - CP.MetricsFonts.PaddedBorderWidth - CP.MetricsFonts.BorderWidth)

        WindowR3.Top = WindowR2.Top + TextBoxR1.Top + TextBoxR1.Font.Height + 10
        WindowR3.Left = WindowR2.Left + TextBoxR1.Left + 15

        LabelR13.Top = WindowR4.Top + WindowR4.Metrics_CaptionHeight + 2
        LabelR13.Left = WindowR4.Right - WindowR4.Metrics_CaptionWidth - 2

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

        WindowR1.ColorGradient = [CP].Win32.EnableGradient
        WindowR2.ColorGradient = [CP].Win32.EnableGradient
        WindowR3.ColorGradient = [CP].Win32.EnableGradient
        WindowR4.ColorGradient = [CP].Win32.EnableGradient

        Dim c As Color
        c = [CP].Win32.ActiveTitle
        WindowR2.Color1 = c
        WindowR3.Color1 = c
        WindowR4.Color1 = c

        c = [CP].Win32.GradientActiveTitle
        WindowR2.Color2 = c
        WindowR3.Color2 = c
        WindowR4.Color2 = c

        c = [CP].Win32.TitleText
        WindowR2.ForeColor = c
        WindowR3.ForeColor = c
        WindowR4.ForeColor = c

        c = [CP].Win32.InactiveTitle
        WindowR1.Color1 = c

        c = [CP].Win32.GradientInactiveTitle
        WindowR1.Color2 = c

        c = [CP].Win32.InactiveTitleText
        WindowR1.ForeColor = c

        c = [CP].Win32.ActiveBorder
        WindowR2.ColorBorder = c
        WindowR3.ColorBorder = c
        WindowR4.ColorBorder = c

        c = [CP].Win32.InactiveBorder
        WindowR1.ColorBorder = c

        c = [CP].Win32.WindowFrame
        For Each RW As UI.Retro.WindowR In ClassicColorsPreview.Controls.OfType(Of UI.Retro.WindowR)
            For Each RB As UI.Retro.ButtonR In RW.Controls.OfType(Of UI.Retro.ButtonR)
                RB.WindowFrame = c
            Next
        Next
        For Each RB As UI.Retro.ButtonR In PanelR2.Controls.OfType(Of UI.Retro.ButtonR)
            RB.WindowFrame = c
        Next

        c = [CP].Win32.ButtonFace
        For Each RW As UI.Retro.WindowR In ClassicColorsPreview.Controls.OfType(Of UI.Retro.WindowR)
            If RW IsNot Menu Then RW.BackColor = c
            For Each RB As UI.Retro.ButtonR In RW.Controls.OfType(Of UI.Retro.ButtonR)
                RB.BackColor = c
            Next
        Next
        For Each RB As UI.Retro.ButtonR In PanelR2.Controls.OfType(Of UI.Retro.ButtonR)
            RB.BackColor = c
        Next
        PanelR2.BackColor = c
        Menu_Window.ButtonFace = c

        c = [CP].Win32.ButtonDkShadow
        For Each RW As UI.Retro.WindowR In ClassicColorsPreview.Controls.OfType(Of UI.Retro.WindowR)
            RW.ButtonDkShadow = c
            For Each RB As UI.Retro.ButtonR In RW.Controls.OfType(Of UI.Retro.ButtonR)
                RB.ButtonDkShadow = c
            Next
        Next
        For Each RB As UI.Retro.ButtonR In PanelR2.Controls.OfType(Of UI.Retro.ButtonR)
            RB.ButtonDkShadow = c
        Next
        TextBoxR1.ButtonDkShadow = c
        Menu_Window.ButtonDkShadow = c

        c = [CP].Win32.ButtonHilight
        For Each RW As UI.Retro.WindowR In ClassicColorsPreview.Controls.OfType(Of UI.Retro.WindowR)
            RW.ButtonHilight = c
            For Each RB As UI.Retro.ButtonR In RW.Controls.OfType(Of UI.Retro.ButtonR)
                RB.ButtonHilight = c
            Next
        Next
        For Each RB As UI.Retro.ButtonR In PanelR2.Controls.OfType(Of UI.Retro.ButtonR)
            RB.ButtonHilight = c
        Next
        For Each RB As UI.Retro.PanelRaisedR In ClassicColorsPreview.Controls.OfType(Of UI.Retro.PanelRaisedR)
            RB.ButtonHilight = c
        Next
        TextBoxR1.ButtonHilight = c
        PanelR1.ButtonHilight = c
        PanelR2.ButtonHilight = c
        Menu_Window.ButtonHilight = c

        c = [CP].Win32.ButtonLight
        For Each RW As UI.Retro.WindowR In ClassicColorsPreview.Controls.OfType(Of UI.Retro.WindowR)
            RW.ButtonLight = c
            For Each RB As UI.Retro.ButtonR In RW.Controls.OfType(Of UI.Retro.ButtonR)
                RB.ButtonLight = c
            Next
        Next
        For Each RB As UI.Retro.ButtonR In PanelR2.Controls.OfType(Of UI.Retro.ButtonR)
            RB.ButtonLight = c
        Next
        TextBoxR1.ButtonLight = c
        Menu_Window.ButtonLight = c

        c = [CP].Win32.ButtonShadow
        For Each RW As UI.Retro.WindowR In ClassicColorsPreview.Controls.OfType(Of UI.Retro.WindowR)
            RW.ButtonShadow = c
            For Each RB As UI.Retro.ButtonR In RW.Controls.OfType(Of UI.Retro.ButtonR)
                RB.ButtonShadow = c
            Next
        Next
        For Each RB As UI.Retro.ButtonR In PanelR2.Controls.OfType(Of UI.Retro.ButtonR)
            RB.ButtonShadow = c
        Next
        For Each RB As UI.Retro.PanelRaisedR In ClassicColorsPreview.Controls.OfType(Of UI.Retro.PanelRaisedR)
            RB.ButtonShadow = c
        Next
        TextBoxR1.ButtonShadow = c
        PanelR1.ButtonShadow = c
        TextBoxR1.Invalidate()
        Menu_Window.ButtonShadow = c

        c = [CP].Win32.ButtonText
        For Each RW As UI.Retro.WindowR In ClassicColorsPreview.Controls.OfType(Of UI.Retro.WindowR)
            RW.ButtonText = c
            For Each RB As UI.Retro.ButtonR In RW.Controls.OfType(Of UI.Retro.ButtonR)
                RB.ForeColor = c
            Next
        Next
        For Each RB As UI.Retro.ButtonR In PanelR2.Controls.OfType(Of UI.Retro.ButtonR)
            RB.ForeColor = c
        Next

        c = [CP].Win32.AppWorkspace
        programcontainer.BackColor = c

        c = [CP].Win32.Background
        ClassicColorsPreview.BackColor = c

        c = [CP].Win32.Menu
        Menu_Window.BackColor = c
        PanelR1.BackColor = c
        Menu_Window.Invalidate()

        c = [CP].Win32.MenuBar
        menucontainer0.BackColor = c

        c = [CP].Win32.Hilight
        highlight.BackColor = c

        c = [CP].Win32.MenuHilight
        menuhilight.BackColor = c

        c = [CP].Win32.MenuText
        LabelR6.ForeColor = c
        LabelR1.ForeColor = c

        c = [CP].Win32.HilightText
        LabelR5.ForeColor = c

        c = [CP].Win32.GrayText
        LabelR2.ForeColor = c
        LabelR9.ForeColor = c

        c = [CP].Win32.Window
        TextBoxR1.BackColor = c

        c = [CP].Win32.WindowText
        TextBoxR1.ForeColor = c
        LabelR4.ForeColor = c

        c = [CP].Win32.InfoWindow
        LabelR13.BackColor = c

        c = [CP].Win32.InfoText
        LabelR13.ForeColor = c

        For Each RW As UI.Retro.WindowR In ClassicColorsPreview.Controls.OfType(Of UI.Retro.WindowR)
            RW.Invalidate()
            For Each RB As UI.Retro.ButtonR In RW.Controls.OfType(Of UI.Retro.ButtonR)
                RB.Invalidate()
            Next
        Next

        For Each RB As UI.Retro.ButtonR In PanelR2.Controls.OfType(Of UI.Retro.ButtonR)
            RB.Invalidate()
        Next

        Refresh17BitPreference([CP])

        RetroShadow1.Refresh()
    End Sub

    Sub Refresh17BitPreference([CP] As CP)

        If [CP].Win32.EnableTheming Then
            'Theming Enabled (Menus Has colors and borders)
            Menu_Window.Flat = True
            PanelR1.Flat = True
            menuhilight.BackColor = [CP].Win32.MenuHilight  'Filling of selected item
            highlight.BackColor = [CP].Win32.Hilight 'Outer Border of selected item

            PanelR1.BackColor = [CP].Win32.MenuHilight
            PanelR1.ButtonShadow = [CP].Win32.Hilight

            menucontainer0.BackColor = [CP].Win32.MenuBar
            LabelR3.ForeColor = [CP].Win32.HilightText
        Else
            'Theming Disabled (Menus are retro 3d)
            Menu_Window.Flat = False
            PanelR1.Flat = False
            menuhilight.BackColor = [CP].Win32.Hilight 'Both will have same color
            highlight.BackColor = [CP].Win32.Hilight 'Both will have same color
            PanelR1.BackColor = [CP].Win32.Menu
            PanelR1.ButtonShadow = [CP].Win32.ButtonShadow
            menucontainer0.BackColor = [CP].Win32.Menu
            LabelR3.ForeColor = [CP].Win32.MenuText

        End If

        Menu_Window.Invalidate()
        PanelR1.Invalidate()
        menuhilight.Invalidate()
        highlight.Invalidate()

    End Sub

    Sub ApplyCMDPreview(CMD As UI.Simulation.WinCMD, [Console] As CP.Structures.Console, PS As Boolean)
        CMD.CMD_ColorTable00 = [Console].ColorTable00
        CMD.CMD_ColorTable01 = [Console].ColorTable01
        CMD.CMD_ColorTable02 = [Console].ColorTable02
        CMD.CMD_ColorTable03 = [Console].ColorTable03
        CMD.CMD_ColorTable04 = [Console].ColorTable04
        CMD.CMD_ColorTable05 = [Console].ColorTable05
        CMD.CMD_ColorTable06 = [Console].ColorTable06
        CMD.CMD_ColorTable07 = [Console].ColorTable07
        CMD.CMD_ColorTable08 = [Console].ColorTable08
        CMD.CMD_ColorTable09 = [Console].ColorTable09
        CMD.CMD_ColorTable10 = [Console].ColorTable10
        CMD.CMD_ColorTable11 = [Console].ColorTable11
        CMD.CMD_ColorTable12 = [Console].ColorTable12
        CMD.CMD_ColorTable13 = [Console].ColorTable13
        CMD.CMD_ColorTable14 = [Console].ColorTable14
        CMD.CMD_ColorTable15 = [Console].ColorTable15
        CMD.CMD_PopupForeground = [Console].PopupForeground
        CMD.CMD_PopupBackground = [Console].PopupBackground
        CMD.CMD_ScreenColorsForeground = [Console].ScreenColorsForeground
        CMD.CMD_ScreenColorsBackground = [Console].ScreenColorsBackground

        If Not [Console].FontRaster Then
            With Font.FromLogFont(New LogFont With {.lfFaceName = [Console].FaceName, .lfWeight = [Console].FontWeight})
                CMD.Font = New Font(.FontFamily, CInt([Console].FontSize / 65536), .Style)
            End With
        End If

        CMD.PowerShell = PS
        CMD.Raster = [Console].FontRaster
        Select Case [Console].FontSize
            Case 393220
                CMD.RasterSize = UI.Simulation.WinCMD.Raster_Sizes._4x6

            Case 524294
                CMD.RasterSize = UI.Simulation.WinCMD.Raster_Sizes._6x8


            Case 524296
                CMD.RasterSize = UI.Simulation.WinCMD.Raster_Sizes._8x8

            Case 524304
                CMD.RasterSize = UI.Simulation.WinCMD.Raster_Sizes._16x8

            Case 786437
                CMD.RasterSize = UI.Simulation.WinCMD.Raster_Sizes._5x12

            Case 786439
                CMD.RasterSize = UI.Simulation.WinCMD.Raster_Sizes._7x12

            Case 0
                CMD.RasterSize = UI.Simulation.WinCMD.Raster_Sizes._8x12

            Case 786448
                CMD.RasterSize = UI.Simulation.WinCMD.Raster_Sizes._16x12

            Case 1048588
                CMD.RasterSize = UI.Simulation.WinCMD.Raster_Sizes._12x16

            Case 1179658
                CMD.RasterSize = UI.Simulation.WinCMD.Raster_Sizes._10x18

            Case Else
                CMD.RasterSize = UI.Simulation.WinCMD.Raster_Sizes._8x12

        End Select

        CMD.Refresh()
    End Sub

    Sub LoadCursorsFromCP([CP] As CP)
        CursorCP_to_Cursor(Arrow, [CP].Cursor_Arrow)
        CursorCP_to_Cursor(Help, [CP].Cursor_Help)
        CursorCP_to_Cursor(AppLoading, [CP].Cursor_AppLoading)
        CursorCP_to_Cursor(Busy, [CP].Cursor_Busy)
        CursorCP_to_Cursor(Move_Cur, [CP].Cursor_Move)
        CursorCP_to_Cursor(NS, [CP].Cursor_NS)
        CursorCP_to_Cursor(EW, [CP].Cursor_EW)
        CursorCP_to_Cursor(NESW, [CP].Cursor_NESW)
        CursorCP_to_Cursor(NWSE, [CP].Cursor_NWSE)
        CursorCP_to_Cursor(Up, [CP].Cursor_Up)
        CursorCP_to_Cursor(Pen, [CP].Cursor_Pen)
        CursorCP_to_Cursor(None, [CP].Cursor_None)
        CursorCP_to_Cursor(Link, [CP].Cursor_Link)
        CursorCP_to_Cursor(Pin, [CP].Cursor_Pin)
        CursorCP_to_Cursor(Person, [CP].Cursor_Person)
        CursorCP_to_Cursor(IBeam, [CP].Cursor_IBeam)
        CursorCP_to_Cursor(Cross, [CP].Cursor_Cross)

        For Each i As CursorControl In Cursors_Container.Controls
            If TypeOf i Is CursorControl Then
                i.Invalidate()
            End If
        Next
    End Sub

    Sub CursorCP_to_Cursor([CursorControl] As CursorControl, [Cursor] As CP.Structures.Cursor)
        [CursorControl].Prop_ArrowStyle = [Cursor].ArrowStyle
        [CursorControl].Prop_CircleStyle = [Cursor].CircleStyle
        [CursorControl].Prop_PrimaryColor1 = [Cursor].PrimaryColor1
        [CursorControl].Prop_PrimaryColor2 = [Cursor].PrimaryColor2
        [CursorControl].Prop_PrimaryColorGradient = [Cursor].PrimaryColorGradient
        [CursorControl].Prop_PrimaryColorGradientMode = [Cursor].PrimaryColorGradientMode
        [CursorControl].Prop_PrimaryNoise = [Cursor].PrimaryColorNoise
        [CursorControl].Prop_PrimaryNoiseOpacity = [Cursor].PrimaryColorNoiseOpacity
        [CursorControl].Prop_SecondaryColor1 = [Cursor].SecondaryColor1
        [CursorControl].Prop_SecondaryColor2 = [Cursor].SecondaryColor2
        [CursorControl].Prop_SecondaryColorGradient = [Cursor].SecondaryColorGradient
        [CursorControl].Prop_SecondaryColorGradientMode = [Cursor].SecondaryColorGradientMode
        [CursorControl].Prop_SecondaryNoise = [Cursor].SecondaryColorNoise
        [CursorControl].Prop_SecondaryNoiseOpacity = [Cursor].SecondaryColorNoiseOpacity
        [CursorControl].Prop_LoadingCircleBack1 = [Cursor].LoadingCircleBack1
        [CursorControl].Prop_LoadingCircleBack2 = [Cursor].LoadingCircleBack2
        [CursorControl].Prop_LoadingCircleBackGradient = [Cursor].LoadingCircleBackGradient
        [CursorControl].Prop_LoadingCircleBackGradientMode = [Cursor].LoadingCircleBackGradientMode
        [CursorControl].Prop_LoadingCircleBackNoise = [Cursor].LoadingCircleBackNoise
        [CursorControl].Prop_LoadingCircleBackNoiseOpacity = [Cursor].LoadingCircleBackNoiseOpacity
        [CursorControl].Prop_LoadingCircleHot1 = [Cursor].LoadingCircleHot1
        [CursorControl].Prop_LoadingCircleHot2 = [Cursor].LoadingCircleHot2
        [CursorControl].Prop_LoadingCircleHotGradient = [Cursor].LoadingCircleHotGradient
        [CursorControl].Prop_LoadingCircleHotGradientMode = [Cursor].LoadingCircleHotGradientMode
        [CursorControl].Prop_LoadingCircleHotNoise = [Cursor].LoadingCircleHotNoise
        [CursorControl].Prop_LoadingCircleHotNoiseOpacity = [Cursor].LoadingCircleHotNoiseOpacity
        [CursorControl].Prop_Shadow_Enabled = [Cursor].Shadow_Enabled
        [CursorControl].Prop_Shadow_Color = [Cursor].Shadow_Color
        [CursorControl].Prop_Shadow_Blur = [Cursor].Shadow_Blur
        [CursorControl].Prop_Shadow_Opacity = [Cursor].Shadow_Opacity
        [CursorControl].Prop_Shadow_OffsetX = [Cursor].Shadow_OffsetX
        [CursorControl].Prop_Shadow_OffsetY = [Cursor].Shadow_OffsetY
    End Sub
#End Region

#Region "Store form events"
    Private Sub Store_Load(sender As Object, e As EventArgs) Handles Me.Load
        CenterToScreen()
        UpdateExtendedTitlebar()

        DLLFunc.RemoveFormTitlebarTextAndIcon(Handle)
        ShowIcon = False
        FinishedLoadingInitialCPs = False
        _Shown = False

        LoadLanguage
        ApplyStyle(Me, True)

        store_container.CheckForIllegalCrossThreadCalls = False         'Prevent exception error of cross-thread

        DoubleBuffer
        Cursors_Container.DoubleBuffer

        taskbar.CopycatFrom(MainFrm.taskbar)
        ActionCenter.CopycatFrom(MainFrm.ActionCenter)
        start.CopycatFrom(MainFrm.start)
        Window1.CopycatFrom(MainFrm.Window1)
        Window2.CopycatFrom(MainFrm.Window2)

        log.ImageList = My.Notifications_IL
        Apply_btn.Image = MainFrm.apply_btn.Image
        RestartExplorer.Image = MainFrm.Button19.Image

        WXP_Alert2.Text = MainFrm.WXP_Alert2.Text
        WXP_Alert2.Size = WXP_Alert2.Parent.Size - New Size(40, 40)
        WXP_Alert2.Location = New Point(20, 20)

        pnl_preview.BackgroundImage = MainFrm.pnl_preview.BackgroundImage
        pnl_preview_classic.BackgroundImage = pnl_preview.BackgroundImage

        Status_lbl.Font = My.Application.ConsoleFontMedium
        themeSize_lbl.Font = My.Application.ConsoleFontLarge
        respacksize_lbl.Font = My.Application.ConsoleFontLarge
        desc_txt.Font = My.Application.ConsoleFontLarge
        Theme_MD5_lbl.Font = My.Application.ConsoleFont
    End Sub

    Private Sub Store_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        ShowIcon = True
        _Shown = True
        RemoveAllStoreItems(store_container)
        FilesFetcher.RunWorkerAsync()

        If My.Settings.Store.ShowTips Then Store_Intro.ShowDialog()
    End Sub

    Private Sub Store_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        'To prevent effect of a store theme on the other forms
        My.Settings = New XeSettings(XeSettings.Mode.Registry)
        My.RenderingHint = If(My.CP.MetricsFonts.Fonts_SingleBitPP, Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit, Drawing.Text.TextRenderingHint.ClearTypeGridFit)

        ApplyStyle(Me)

        Status_pnl.Visible = True
        Status_lbl.SetText(My.Lang.Store_CleaningFromMemory)
        Status_lbl.Refresh()

        FilesFetcher.CancelAsync()
        store_container.Visible = False
        RemoveAllStoreItems(store_container)
        store_container.Visible = True
        Tabs.SelectedIndex = 0
        GC.Collect()
        GC.WaitForPendingFinalizers()
        Status_lbl.SetText("")
    End Sub

#End Region

#Region "Backgroundworkers to load Store CPs"
    Sub OnlineMode()
        Dnsapi.DnsFlushResolverCache()

        Dim response As New List(Of String) : response.Clear()
        Dim repos_list As New List(Of String) : repos_list.Clear()
        Dim items As New List(Of String) : items.Clear()

        'Check by Ping if repos DB URL is accessible or not
        For Each DB As String In My.Settings.Store.Online_Repositories

            If Not DB.StartsWith("https://", My._ignore) Then DB &= "https://"

            Status_lbl.SetText(String.Format(My.Lang.Store_Ping, DB))

            If Ping(DB) Then
                repos_list.Add(DB)
            Else
                Status_lbl.SetText(String.Format(My.Lang.Store_PingFailed, DB))
            End If

        Next

        'Loop through all valid repos DBs
        For Each DB As String In repos_list

            'Try to generate a folder name dependent on the URL
            Dim reposName As String
            If DB.ToUpper.Contains("GITHUB.COM") Then
                Dim x As String() = DB.Replace("https://", "").Replace("http://", "").Split("/")
                reposName = x(1) & "_" & x(2)
                reposName = String.Join("_", reposName.Split(IO.Path.GetInvalidFileNameChars()))
            Else
                reposName = String.Join("_", DB.Replace("https://", "").Replace("http://", "").Split(IO.Path.GetInvalidFileNameChars()))
            End If

            'Get text of the DB from URL
            Status_lbl.SetText(String.Format(My.Lang.Store_Accessing, DB))
            response.Clear()
            response = WebCL.DownloadString(DB).CList
            items.Clear()

            'Add valid lines (Correct format) in a themes list
            For Each item In response
                Dim valid As Boolean = True
                If item.Contains("|") AndAlso item.Split("|").Count >= 3 Then
                    For Each x In item.Split("|")
                        If String.IsNullOrWhiteSpace(x) Then
                            valid = False
                            Exit For
                        End If
                    Next
                Else
                    valid = False
                End If

                If valid Then items.Add(item)

            Next

            BeginInvoke(CType(Sub()
                                  ProgressBar1.Visible = True
                              End Sub, MethodInvoker))

            Dim i As Integer = 0
            Dim allProgress As Integer = items.Count * 2

            'Loop through valid lines from the themes list
            For Each item In items
                Dim item_splitted As String() = item.Split("|")

                Dim MD5_ThemeFile As String = item_splitted(0).ToUpper
                Dim MD5_PackFile As String = item_splitted(1).ToUpper
                Dim URL_ThemeFile As String = item_splitted(2)
                Dim URL_PackFile As String = ""
                If item_splitted.Count = 4 Then URL_PackFile = item_splitted(3)

                'Create a folder inside AppData folder
                Dim temp As String = URL_ThemeFile.Replace("?raw=true", "")
                Dim FileName As String = temp.Split("/").Last
                temp = temp.Replace("/" & FileName, "")
                Dim FolderName As String = temp.Split("/").Last
                Dim Dir As String = My.PATH_StoreCache
                If Not String.IsNullOrWhiteSpace(FolderName) Then Dir &= "\" & reposName & "\" & FolderName
                If Not IO.Directory.Exists(Dir) Then IO.Directory.CreateDirectory(Dir)

                Status_lbl.SetText("")

                'Download the theme (*.wpth)
                If IO.File.Exists(Dir & "\" & FileName) Then
                    'If it exists, check MD5, if it is changed, redownload the theme
                    If CalculateMD5(Dir & "\" & FileName) <> MD5_ThemeFile Then
                        IO.File.Delete(Dir & "\" & FileName)
                        Status_lbl.SetText(String.Format(My.Lang.Store_UpdateTheme, FileName, URL_ThemeFile))
                        Try : WebCL.DownloadFile(URL_ThemeFile, Dir & "\" & FileName) : Catch : End Try
                    End If
                Else
                    Status_lbl.SetText(String.Format(My.Lang.Store_DownloadTheme, FileName, URL_ThemeFile))
                    Try : WebCL.DownloadFile(URL_ThemeFile, Dir & "\" & FileName) : Catch : End Try
                End If

                i += 1
                If allProgress > 0 Then FilesFetcher.ReportProgress((i / allProgress) * 100)

                'Convert themes CPs into StoreItems, and exclude the old formats of WPTH
                If IO.File.Exists(Dir & "\" & FileName) AndAlso _Converter.FetchFile(Dir & "\" & FileName) = Converter_CP.WP_Format.JSON Then
                    Try
                        Status_lbl.SetText(String.Format(My.Lang.Store_LoadingTheme, FileName))

                        Using CP As New CP(CP_Type.File, Dir & "\" & FileName, True)

                            Dim ctrl As New UI.Controllers.StoreItem With {
                                           .FileName = Dir & "\" & FileName,
                                           .CP = CP,
                                           .MD5_ThemeFile = MD5_ThemeFile,
                                           .MD5_PackFile = MD5_PackFile,
                                           .DoneByWinPaletter = (DB.ToUpper = My.Resources.Link_StoreMainDB.ToUpper),
                                           .Size = New Size(w, h),
                                           .URL_ThemeFile = URL_ThemeFile,
                                           .URL_PackFile = URL_PackFile}

                            If ctrl.DoneByWinPaletter Then ctrl.CP.Info.Author = My.Application.Info.ProductName

                            AddHandler ctrl.Click, AddressOf StoreItem_Clicked
                            AddHandler ctrl.CPChanged, AddressOf StoreItem_CPChanged
                            AddHandler ctrl.MouseEnter, AddressOf StoreItem_MouseEnter
                            AddHandler ctrl.MouseLeave, AddressOf StoreItem_MouseLeave

                            BeginInvoke(CType(Sub()
                                                  store_container.Controls.Add(ctrl)
                                              End Sub, MethodInvoker))

                        End Using

                    Catch ex As Exception

                    End Try
                End If

                Status_lbl.SetText("")

                i += 1

                If allProgress > 0 Then FilesFetcher.ReportProgress((i / allProgress) * 100)

            Next

            'Finalizing
            BeginInvoke(CType(Sub()
                                  ProgressBar1.Visible = False
                              End Sub, MethodInvoker))

            CPList.Clear()
        Next

        FinishedLoadingInitialCPs = True
    End Sub

    Sub OfflineMode()
        BeginInvoke(CType(Sub()
                              ProgressBar1.Visible = True
                              store_container.Visible = False
                          End Sub, MethodInvoker))

        Dim i As Integer = 0
        Dim allProgress As Integer = 0


        For Each folder In My.Settings.Store.Offline_Directories

            If IO.Directory.Exists(folder) Then
                Status_lbl.SetText("Accessing themes from folder """ & folder & """")
                allProgress += IO.Directory.GetFiles(folder, "*.wpth", If(My.Settings.Store.Offline_SubFolders, IO.SearchOption.AllDirectories, IO.SearchOption.TopDirectoryOnly)).Count
            End If

        Next

        allProgress *= 2

        For Each folder In My.Settings.Store.Offline_Directories

            If IO.Directory.Exists(folder) Then

                For Each file As String In IO.Directory.GetFiles(folder, "*.wpth", If(My.Settings.Store.Offline_SubFolders, IO.SearchOption.AllDirectories, IO.SearchOption.TopDirectoryOnly))

                    Try
                        If Not CPList.ContainsKey(file) Then

                            Status_lbl.SetText("Enumerating themes: """ & file & """")

                            Using CPx As New CP(CP.CP_Type.File, file, True)
                                CPList.Add(file, CPx)
                            End Using
                        End If
                    Catch ex As Exception
                    End Try

                    i += 1

                    If allProgress > 0 Then FilesFetcher.ReportProgress((i / allProgress) * 100)
                Next
            End If
        Next


        For Each StoreItem In CPList
            Status_lbl.SetText("Loading theme """ & StoreItem.Value.Info.ThemeName & """")

            Dim ctrl As New UI.Controllers.StoreItem With {
                        .FileName = StoreItem.Key,
                        .CP = StoreItem.Value,
                        .MD5_ThemeFile = CalculateMD5(StoreItem.Key),
                        .DoneByWinPaletter = False,
                        .Size = New Size(w, h),
                        .URL_ThemeFile = New IO.FileInfo(StoreItem.Key).FullName}

            If ctrl.DoneByWinPaletter Then ctrl.CP.Info.Author = My.Application.Info.ProductName

            AddHandler ctrl.Click, AddressOf StoreItem_Clicked
            AddHandler ctrl.CPChanged, AddressOf StoreItem_CPChanged
            AddHandler ctrl.MouseEnter, AddressOf StoreItem_MouseEnter
            AddHandler ctrl.MouseLeave, AddressOf StoreItem_MouseLeave

            BeginInvoke(CType(Sub()
                                  store_container.Controls.Add(ctrl)
                              End Sub, MethodInvoker))

            i += 1

            If allProgress > 0 Then FilesFetcher.ReportProgress((i / allProgress) * 100)
        Next

        BeginInvoke(CType(Sub()
                              ProgressBar1.Visible = False
                              store_container.Visible = True
                          End Sub, MethodInvoker))

        Status_lbl.SetText("")

        CPList.Clear()

        FinishedLoadingInitialCPs = True
    End Sub

    Private Sub FilesFetcher_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles FilesFetcher.DoWork

        If My.Settings.Store.Online_or_Offline Then

            If Not IsNetworkAvailable() Then
                Status_lbl.SetText(My.Lang.Store_NoNetwork)

                If MsgBox(My.Lang.Store_NoNetwork, MsgBoxStyle.Question + MsgBoxStyle.YesNo, My.Lang.Store_TryOffline) = MsgBoxResult.Yes Then
                    StartedAsOnlineOrOffline = False
                    OfflineMode()
                    Exit Sub
                End If

            Else
                StartedAsOnlineOrOffline = True
                OnlineMode()
            End If

        Else
            StartedAsOnlineOrOffline = False
            OfflineMode()
        End If

        GC.Collect()
        GC.WaitForPendingFinalizers()

        My.Animator.HideSync(Status_pnl)
    End Sub

    Private Sub FilesFetcher_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles FilesFetcher.ProgressChanged
        Try
            ProgressBar1.Value = Math.Max(Math.Min(e.ProgressPercentage, ProgressBar1.Maximum), ProgressBar1.Minimum)
        Catch
        End Try
    End Sub

    Private Sub FilesFetcher_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles FilesFetcher.RunWorkerCompleted

    End Sub

#End Region

#Region "Store item events"

    Public Sub StoreItem_Clicked(sender As Object, e As MouseEventArgs)

        Select Case e.Button
            Case MouseButtons.Right
                With DirectCast(sender, UI.Controllers.StoreItem)
                    Store_Hover.Close()

                    selectedItem = DirectCast(sender, UI.Controllers.StoreItem)

                    Store_Hover.Show()

                    Adjust_Preview(.CP)
                    tabs_preview.SelectedIndex = 0
                    Store_Hover.img0 = tabs_preview.ToBitmap
                    tabs_preview.SelectedIndex = 1
                    Store_Hover.img1 = tabs_preview.ToBitmap
                    Store_Hover.BackgroundImage = Store_Hover.img0

                End With

            Case Else
                selectedItem = DirectCast(sender, UI.Controllers.StoreItem)
                Cursor = Cursors.AppStarting
                StoreItem1.CP = selectedItem.CP
                StoreItem1.DoneByWinPaletter = selectedItem.DoneByWinPaletter
                Theme_MD5_lbl.Text = "MD5: " & selectedItem.MD5_ThemeFile

                With selectedItem
                    My.Animator.HideSync(Tabs)
                    search_panel.Visible = False

                    Titlebar_lbl.Text = .CP.Info.ThemeName & " - " & My.Lang.By & " " & .CP.Info.Author
                    If CP.IsFontInstalled(.CP.MetricsFonts.CaptionFont.Name) Then
                        Titlebar_lbl.Font = New Font(.CP.MetricsFonts.CaptionFont.Name, Titlebar_lbl.Font.Size, Titlebar_lbl.Font.Style)
                    Else
                        Titlebar_lbl.Font = New Font("Segoe UI", Titlebar_lbl.Font.Size, Titlebar_lbl.Font.Style)
                    End If

                    If .CP.AppTheme.Enabled Then
                        My.Settings.Appearance.CustomColors = .CP.AppTheme.Enabled
                        My.Settings.Appearance.CustomTheme = .CP.AppTheme.DarkMode
                        My.Settings.Appearance.RoundedCorners = .CP.AppTheme.RoundCorners
                        My.Settings.Appearance.BackColor = .CP.AppTheme.BackColor
                        My.Settings.Appearance.AccentColor = .CP.AppTheme.AccentColor
                        ApplyStyle(Me, True)
                    End If

                    If .CP.AppTheme.Enabled Then
                        Label14.ForeColor = If(.CP.AppTheme.DarkMode, Color.White.CB(-0.3), Color.Black.CB(0.3))
                    Else
                        Label14.ForeColor = If(My.Style.DarkMode, Color.White.CB(-0.3), Color.Black.CB(0.3))
                    End If
                    Label6.ForeColor = Label14.ForeColor
                    Theme_MD5_lbl.ForeColor = Label14.ForeColor

                    Adjust_Preview(.CP)
                    ApplyRetroPreview(.CP)
                    SetClassicMetrics(.CP)
                    ApplyCMDPreview(CMD1, .CP.CommandPrompt, False)
                    ApplyCMDPreview(CMD2, .CP.PowerShellx86, True)
                    ApplyCMDPreview(CMD3, .CP.PowerShellx64, True)
                    LoadCursorsFromCP(.CP)
                    My.RenderingHint = If(.CP.MetricsFonts.Fonts_SingleBitPP, Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit, Drawing.Text.TextRenderingHint.ClearTypeGridFit)

                    For Each i As CursorControl In Cursors_Container.Controls
                        If TypeOf i Is CursorControl Then
                            If i.Prop_Cursor = CursorType.AppLoading Or i.Prop_Cursor = CursorType.Busy Then AnimateList.Add(i)
                        End If
                    Next

                    themeSize_lbl.Text = My.Computer.FileSystem.GetFileInfo(.FileName).Length.SizeString

                    If Not String.IsNullOrWhiteSpace(.MD5_PackFile) AndAlso .MD5_PackFile <> "0" Then
                        Task.Run(Sub()
                                     respacksize_lbl.SetText(My.Lang.Store_Calculating)
                                     Dim Pack_Size As Long = GetFileSizeFromURL(.URL_PackFile)
                                     respacksize_lbl.SetText(If(Pack_Size > 0, Pack_Size.SizeString, 0.SizeString))
                                 End Sub)
                    Else
                        respacksize_lbl.Text = 0.SizeString
                    End If

                    desc_txt.Text = .CP.Info.Description

                    If My.AppVersion >= .CP.Info.AppVersion Then
                        VersionAlert_lbl.Visible = False
                    Else
                        VersionAlert_lbl.Visible = True
                        VersionAlert_lbl.Text = String.Format(My.Lang.Store_LowAppVersionAlert, .CP.Info.AppVersion, My.AppVersion)
                    End If

                    Dim os_list As New List(Of String)
                    os_list.Clear()

                    If .CP.Info.DesignedFor_Win11 Then os_list.Add(My.Lang.OS_Win11)
                    If .CP.Info.DesignedFor_Win10 Then os_list.Add(My.Lang.OS_Win10)
                    If .CP.Info.DesignedFor_Win81 Then os_list.Add(My.Lang.OS_Win81)
                    If .CP.Info.DesignedFor_Win7 Then os_list.Add(My.Lang.OS_Win7)
                    If .CP.Info.DesignedFor_WinVista Then os_list.Add(My.Lang.OS_WinVista)
                    If .CP.Info.DesignedFor_WinXP Then os_list.Add(My.Lang.OS_WinXP)

                    Dim os_format As String = ""
                    If os_list.Count = 1 Then
                        os_format = os_list(0)
                    ElseIf os_list.Count = 2 Then
                        os_format = os_list(0) & " && " & os_list(1)
                    ElseIf os_list.Count > 2 Then
                        For i = 0 To os_list.Count - 3
                            os_format &= os_list(i) & ", "
                        Next
                        os_format &= os_list(os_list.Count - 2) & " && " & os_list(os_list.Count - 1)
                    End If
                    SupportedOS_lbl.Text = os_format
                    If os_list.Count < 6 Then
                        Label26.Text = My.Lang.Store_ThemeDesignedFor0
                    Else
                        Label26.Text = My.Lang.Store_ThemeDesignedFor1
                    End If

                    desc_txt.BackColor = desc_txt.GetParentColor

                    If .CP.AppTheme.Enabled Then
                        desc_txt.ForeColor = If(.CP.AppTheme.DarkMode, Color.White, Color.Black)
                    Else
                        desc_txt.ForeColor = If(My.Style.DarkMode, Color.White, Color.Black)
                    End If

                    CMD1.Visible = .CP.CommandPrompt.Enabled
                    CMD2.Visible = .CP.PowerShellx86.Enabled
                    CMD3.Visible = .CP.PowerShellx64.Enabled
                    Panel1.Visible = .CP.Cursor_Enabled
                    author_url_button.Visible = Not String.IsNullOrWhiteSpace(.CP.Info.AuthorSocialMediaLink)

                    Tabs.SelectedIndex = 1

                    My.Animator.ShowSync(Tabs)

                    '' '' ''Visual.FadeColor(Titlebar_panel, "BackColor", Titlebar_panel.BackColor, .CP.Info.Color2, 10, 15)
                End With


                Cursor = Cursors.Default

        End Select
    End Sub

    Public Sub StoreItem_MouseEnter(sender As Object, e As EventArgs)
        hoveredItem = DirectCast(sender, UI.Controllers.StoreItem)

    End Sub

    Public Sub StoreItem_MouseLeave(sender As Object, e As EventArgs)

    End Sub

    Public Sub StoreItem_CPChanged(sender As Object, e As EventArgs)
        If FinishedLoadingInitialCPs Then
            With DirectCast(sender, UI.Controllers.StoreItem)
                Adjust_Preview(.CP)
                .Refresh()
            End With
        End If
    End Sub
#End Region

#Region "Subs\Functions"

#Region "   Store"
    Sub Apply_Theme()
        Cursor = Cursors.WaitCursor

        log_lbl.Visible = False
        log_lbl.Text = ""
        ok_btn.Visible = False
        ShowErrors_btn.Visible = False
        ExportDetails_btn.Visible = False
        StopTimer_btn.Visible = False

        If My.Settings.ThemeLog.Enabled Then
            Tabs.SelectedIndex = Tabs.TabCount - 1
            Tabs.Refresh()
        End If

        With My.Settings.Appearance
            .CustomColors = selectedItem.CP.AppTheme.Enabled
            .BackColor = selectedItem.CP.AppTheme.BackColor
            .AccentColor = selectedItem.CP.AppTheme.AccentColor
            .CustomTheme = selectedItem.CP.AppTheme.DarkMode
            .RoundedCorners = selectedItem.CP.AppTheme.RoundCorners
        End With
        ApplyStyle(Nothing, True)

        Using CPx As New CP(CP_Type.File, selectedItem.FileName)
            If selectedItem.DoneByWinPaletter Then CPx.Info.Author = My.Application.Info.CompanyName
            CPx.Save(CP.CP_Type.Registry, "", If(My.Settings.ThemeLog.Enabled, log, Nothing), True)
            My.CP_Original = CPx.Clone
        End Using

        UpdateExtendedTitlebar()

        Cursor = Cursors.Default

        If My.Settings.ThemeApplyingBehavior.AutoRestartExplorer Then
            Core.RestartExplorer(If(My.Settings.ThemeLog.Enabled, log, Nothing))
        Else
            If My.Settings.ThemeLog.Enabled Then CP.AddNode(log, My.Lang.NoDefResExplorer, "warning")
        End If

        If My.Settings.ThemeLog.Enabled Then CP.AddNode(log, String.Format("{0}: {1}", Now.ToLongTimeString, My.Lang.CP_AllDone), "info")

        If selectedItem.CP.MetricsFonts.Enabled And GetWindowsScreenScalingFactor() > 100 Then CP.AddNode(log, String.Format("{0}", My.Lang.CP_MetricsHighDPIAlert), "info")

        If My.Settings.ThemeLog.Enabled Then CP.AddNode(log, My.Lang.Store_LogoffRecommended, "info")

        log_lbl.Visible = True
        ok_btn.Visible = True
        ExportDetails_btn.Visible = True
        StopTimer_btn.Visible = True

        If Not My.Saving_Exceptions.Count = 0 Then
            log_lbl.Text = My.Lang.CP_ErrorHappened
            ShowErrors_btn.Visible = True
        Else
            If My.Settings.ThemeLog.CountDown Then
                log_lbl.Text = String.Format(My.Lang.CP_LogWillClose, My.Settings.ThemeLog.CountDown_Seconds)
                apply_elapsedSecs = 1
                Log_Timer.Enabled = True
                Log_Timer.Start()
            End If
        End If

    End Sub

    Sub DoActionsAfterPackDownload()
        If ApplyOrEditToggle Then
            'Apply button is pressed
            Store_CPToggles.CP = selectedItem.CP
            If Store_CPToggles.ShowDialog() = DialogResult.OK Then
                Apply_Theme()
                If selectedItem.DoneByWinPaletter Then My.CP.Info.Author = My.Application.Info.CompanyName
                My.CP = selectedItem.CP
                My.CP_Original = My.CP.Clone
                MainFrm.ApplyStylesToElements(My.CP, False)
                MainFrm.ApplyColorsToElements(My.CP)
                MainFrm.ApplyCPValues(My.CP)
                UpdateTitlebarColors()
            End If
        Else
            'Edit button is pressed
            WindowState = FormWindowState.Minimized
            ComplexSave.GetResponse(MainFrm.SaveFileDialog1, Nothing, Nothing, Nothing)
            My.CP_Original = My.CP.Clone
            My.CP = New CP(CP_Type.File, selectedItem.FileName)
            If selectedItem.DoneByWinPaletter Then My.CP.Info.Author = My.Application.Info.CompanyName
            MainFrm.ApplyStylesToElements(My.CP, False)
            MainFrm.ApplyCPValues(My.CP)
            MainFrm.ApplyColorsToElements(My.CP)
        End If
    End Sub

    Sub RemoveAllStoreItems(Container As FlowLayoutPanel)
        For x = 0 To Container.Controls.Count - 1

            If TypeOf Container.Controls(0) Is UI.Controllers.StoreItem Then
                RemoveHandler DirectCast(Container.Controls(0), UI.Controllers.StoreItem).Click, AddressOf StoreItem_Clicked
                RemoveHandler DirectCast(Container.Controls(0), UI.Controllers.StoreItem).CPChanged, AddressOf StoreItem_CPChanged
                RemoveHandler DirectCast(Container.Controls(0), UI.Controllers.StoreItem).MouseEnter, AddressOf StoreItem_MouseEnter
                RemoveHandler DirectCast(Container.Controls(0), UI.Controllers.StoreItem).MouseLeave, AddressOf StoreItem_MouseLeave
            End If

            Container.Controls(0).Dispose()
        Next
        Container.Controls.Clear()
    End Sub

    Sub PerformSearch()
        Dim search_text As String = search_box.Text.TrimStart.TrimEnd.Trim.Replace(" ", "").ToUpper

        If String.IsNullOrWhiteSpace(search_text) Then Exit Sub

        Dim lst As New Dictionary(Of String, UI.Controllers.StoreItem) : lst.Clear()

        For Each st_itm In store_container.Controls.OfType(Of UI.Controllers.StoreItem)
            lst.Add(st_itm.FileName, st_itm)
        Next

        RemoveAllStoreItems(search_results)

        Dim found_sum As Integer = 0

        For Each st_item In lst
            If (My.Settings.Store.Search_ThemeNames AndAlso st_item.Value.CP.Info.ThemeName.TrimStart.TrimEnd.Trim.Replace(" ", "").ToUpper.Contains(search_text)) _
                Or (My.Settings.Store.Search_AuthorsNames AndAlso st_item.Value.CP.Info.Author.TrimStart.TrimEnd.Trim.Replace(" ", "").ToUpper.Contains(search_text)) _
                Or (My.Settings.Store.Search_Descriptions AndAlso st_item.Value.CP.Info.Description.TrimStart.TrimEnd.Trim.Replace(" ", "").ToUpper.Contains(search_text)) Then

                found_sum += 1

                Dim ctrl As New UI.Controllers.StoreItem With {
               .FileName = st_item.Key,
               .CP = st_item.Value.CP,
               .MD5_ThemeFile = CalculateMD5(st_item.Key),
               .DoneByWinPaletter = st_item.Value.DoneByWinPaletter,
               .Size = New Size(w, h),
               .URL_ThemeFile = st_item.Value.URL_ThemeFile}

                If ctrl.DoneByWinPaletter Then ctrl.CP.Info.Author = My.Application.Info.ProductName

                AddHandler ctrl.Click, AddressOf StoreItem_Clicked
                AddHandler ctrl.CPChanged, AddressOf StoreItem_CPChanged
                AddHandler ctrl.MouseEnter, AddressOf StoreItem_MouseEnter
                AddHandler ctrl.MouseLeave, AddressOf StoreItem_MouseLeave

                BeginInvoke(CType(Sub()
                                      search_results.Controls.Add(ctrl)
                                  End Sub, MethodInvoker))

            End If
        Next

        Titlebar_lbl.Text = String.Format("Search results ({0})", found_sum)

        Tabs.SelectedIndex = 2

        lst.Clear()
    End Sub
#End Region

#Region "   Helpers"
    Private Function CalculateMD5(ByVal path As String) As String
        If IO.File.Exists(path) Then
            Using md5 As MD5 = MD5.Create()
                Dim hash = md5.ComputeHash(IO.File.ReadAllBytes(path))
                Dim result = BitConverter.ToString(hash).Replace("-", "")
                Return result.ToUpper
            End Using
        Else
            Return "0"
        End If

    End Function

    Public Function GetFileSizeFromURL(ByVal url As String) As Long
        Try
            If Not String.IsNullOrWhiteSpace(url) Then
                Dim result As Long = 0
                Dim req As WebRequest = WebRequest.Create(url)
                req.Method = "HEAD"
                Dim contentLength As Long = Nothing

                Using resp As WebResponse = req.GetResponse()

                    If Long.TryParse(resp.Headers.[Get]("Content-Length"), contentLength) Then
                        result = contentLength
                    End If
                End Using

                Return result
            Else
                Return 0
            End If
        Catch
            Return 0
        End Try
    End Function

    Sub UpdateExtendedTitlebar()
        Dim Pd As New Windows.Forms.Padding(0, Titlebar_panel.Height, 0, 0)
        Titlebar_panel.BackColor = Color.FromArgb(0, 0, 0)
        Dim CompositionEnabled As Boolean
        Dwmapi.DwmIsCompositionEnabled(CompositionEnabled)

        If My.W11 Or My.W10 Then
            CompositionEnabled = CompositionEnabled And Reg_IO.GetReg("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "EnableTransparency", True)
        End If


        If CompositionEnabled Then
            Titlebar_lbl.DrawOnGlass = True
            Titlebar_panel.BackColor = Color.Black

            If My.W11 Then
                DrawMica(Pd)

            ElseIf My.W10 OrElse My.W81 OrElse My.W8 OrElse My.W7 OrElse My.WVista Then
                DrawAero(Pd)
                If My.W10 Then DLLFunc.DarkTitlebar(Handle, My.Style.DarkMode)

            Else
                DrawMica(Pd)

            End If

        Else
            If My.W7 OrElse My.WVista Then
                Titlebar_lbl.DrawOnGlass = False

                If My.W7 Then
                    If My.CP.Windows7.Theme <> Structures.Windows7.Themes.Classic Then
                        Titlebar_panel.BackColor = Color.FromArgb(185, 209, 234)
                    Else
                        Titlebar_panel.BackColor = My.CP.Win32.ButtonFace
                    End If

                ElseIf My.WVista Then
                    If My.CP.WindowsVista.Theme <> Structures.Windows7.Themes.Classic Then
                        Titlebar_panel.BackColor = Color.FromArgb(185, 209, 234)
                    Else
                        Titlebar_panel.BackColor = My.CP.Win32.ButtonFace
                    End If

                End If

            ElseIf My.WXP Then
                Titlebar_lbl.DrawOnGlass = False

                If My.CP.WindowsXP.Theme <> Structures.WindowsXP.Themes.Classic Then
                    Titlebar_panel.BackColor = My.Style.Colors.Back
                Else
                    Titlebar_panel.BackColor = My.CP.Win32.ButtonFace
                End If

            Else
                Titlebar_lbl.DrawOnGlass = True
                If My.W11 OrElse My.W10 Then DLLFunc.DarkTitlebar(Handle, My.Style.DarkMode)
                DrawAero(Pd)

            End If

        End If

        Titlebar_lbl.DrawOnGlass = Titlebar_lbl.DrawOnGlass
        back_btn.DrawOnGlass = Titlebar_lbl.DrawOnGlass
        search_btn.DrawOnGlass = Titlebar_lbl.DrawOnGlass
        search_filter_btn.DrawOnGlass = Titlebar_lbl.DrawOnGlass
        search_box.DrawOnGlass = Titlebar_lbl.DrawOnGlass

        UpdateTitlebarColors()
    End Sub

    Sub UpdateTitlebarColors()
        Titlebar_lbl.ForeColor = If(My.Style.DarkMode, Color.White, Color.Black)
        search_box.ForeColor = If(My.Style.DarkMode, Color.White, Color.Black)
        back_btn.Image = If(My.Style.DarkMode, My.Resources.Store_BackBtn, My.Resources.Store_BackBtn.Invert)
    End Sub
#End Region

#End Region

#Region "Timers"
    Private Sub Log_Timer_Tick(sender As Object, e As EventArgs) Handles Log_Timer.Tick
        log_lbl.Text = String.Format(My.Lang.CP_LogWillClose, My.Settings.ThemeLog.CountDown_Seconds - apply_elapsedSecs)

        If apply_elapsedSecs + 1 <= My.Settings.ThemeLog.CountDown_Seconds Then
            apply_elapsedSecs += 1
        Else
            log_lbl.Text = ""
            Log_Timer.Enabled = False
            Log_Timer.Stop()
            Tabs.SelectedIndex = 1
        End If

    End Sub
    Private Sub Cursor_Timer_Tick(sender As Object, e As EventArgs) Handles Cursor_Timer.Tick
        If Not _Shown Then Exit Sub

        Try
            For Each i As CursorControl In AnimateList
                i.Angle = Angle
                i.Refresh()

                If Angle + Increment >= 360 Then Angle = 0
                Angle += Increment

                If Angle = 180 And Cycles >= 2 Then
                    i.Angle = 180
                    Cursor_Timer.Enabled = False
                    Cursor_Timer.Stop()
                ElseIf Angle = 180 Then
                    Cycles += 1
                End If

            Next
        Catch
        End Try
    End Sub
#End Region

#Region "Buttons Events"
    Private Sub Back_btn_Click(sender As Object, e As EventArgs) Handles back_btn.Click

        My.Animator.HideSync(Tabs)
        My.RenderingHint = If(My.CP.MetricsFonts.Fonts_SingleBitPP, Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit, Drawing.Text.TextRenderingHint.ClearTypeGridFit)

        If selectedItem IsNot Nothing AndAlso selectedItem.CP.AppTheme.Enabled Then
            My.Settings = New XeSettings(XeSettings.Mode.Registry)
            ApplyStyle(Me, True)
        End If

        RemoveAllStoreItems(search_results)

        Titlebar_lbl.Font = New Font("Segoe UI", Titlebar_lbl.Font.Size, Titlebar_lbl.Font.Style)
        Tabs.SelectedIndex = 0
        My.Animator.HideSync(back_btn)

        Titlebar_lbl.Text = Text
        My.Animator.ShowSync(Tabs)

        '' '' ''Visual.FadeColor(Titlebar_panel, "BackColor", Titlebar_panel.BackColor, My.Style.Colors.Back, 10, 15)
    End Sub


#Region "   Applying row"
    Private Sub Apply_Edit_btn_Click(sender As Object, e As EventArgs) Handles Apply_btn.Click, Edit_btn.Click
        ApplyOrEditToggle = sender Is Apply_btn

        If Not String.IsNullOrWhiteSpace(selectedItem.CP.Info.License) Then
            Store_ThemeLicense.TextBox1.Text = selectedItem.CP.Info.License
            If Not Store_ThemeLicense.ShowDialog = DialogResult.OK Then Exit Sub
        End If

        If StartedAsOnlineOrOffline Then
            Dim temp As String = selectedItem.URL_PackFile.Replace("?raw=true", "")
            Dim FileName As String = temp.Split("/").Last
            temp = temp.Replace("/" & FileName, "")
            Dim FolderName As String = temp.Split("/").Last
            Dim Dir As String
            If IO.File.Exists(selectedItem.FileName) Then
                Dir = New IO.FileInfo(selectedItem.FileName).Directory.FullName
            Else
                Dir = selectedItem.FileName.Replace("\" & selectedItem.FileName.Split("\").Last, "")
            End If
            If Not IO.Directory.Exists(Dir) Then IO.Directory.CreateDirectory(Dir)

            If selectedItem.MD5_PackFile <> "0" Then
                If (IO.File.Exists(Dir & "\" & FileName) AndAlso CalculateMD5(Dir & "\" & FileName) <> selectedItem.MD5_PackFile) OrElse Not IO.File.Exists(Dir & "\" & FileName) Then
                    Try
                        Store_DownloadProgress.URL = selectedItem.URL_PackFile
                        Store_DownloadProgress.File = Dir & "\" & FileName
                        Store_DownloadProgress.ThemeName = selectedItem.CP.Info.ThemeName
                        Store_DownloadProgress.ThemeVersion = selectedItem.CP.Info.ThemeVersion
                        If Store_DownloadProgress.ShowDialog = DialogResult.OK Then DoActionsAfterPackDownload()
                    Catch
                    End Try
                Else
                    DoActionsAfterPackDownload()
                End If

            Else
                If IO.File.Exists(Dir & "\" & FileName) Then
                    Try : Kill(Dir & "\" & FileName) : Catch : End Try
                End If

                DoActionsAfterPackDownload()
            End If
        Else
            DoActionsAfterPackDownload()
        End If

    End Sub

    Private Sub RestartExplorer_Click(sender As Object, e As EventArgs) Handles RestartExplorer.Click
        Core.RestartExplorer()
    End Sub

#End Region

#Region "   Log row"
    Private Sub Ok_btn_Click(sender As Object, e As EventArgs) Handles ok_btn.Click
        log_lbl.Text = ""
        Log_Timer.Enabled = False
        Log_Timer.Stop()
        Tabs.SelectedIndex = 1
    End Sub

    Private Sub ExportDetails_btn_Click(sender As Object, e As EventArgs) Handles ExportDetails_btn.Click
        log_lbl.Text = ""
        Log_Timer.Enabled = False
        Log_Timer.Stop()

        If MainFrm.SaveFileDialog3.ShowDialog = DialogResult.OK Then
            Dim sb As New StringBuilder
            sb.Clear()

            For Each N As TreeNode In log.Nodes
                sb.AppendLine(String.Format("[{0}]{2} {1}{3}", N.ImageKey, N.Text, vbTab, vbCrLf))
            Next

            IO.File.WriteAllText(MainFrm.SaveFileDialog3.FileName, sb.ToString)

        End If
    End Sub

    Private Sub StopTimer_btn_Click(sender As Object, e As EventArgs) Handles StopTimer_btn.Click
        log_lbl.Text = ""
        Log_Timer.Enabled = False
        Log_Timer.Stop()
    End Sub

    Private Sub ShowErrors_btn_Click(sender As Object, e As EventArgs) Handles ShowErrors_btn.Click
        log_lbl.Text = ""
        Log_Timer.Enabled = False
        Log_Timer.Stop()
        Saving_ex_list.ex_List = My.Saving_Exceptions
        Saving_ex_list.ShowDialog()
    End Sub
#End Region

#Region "   Search"
    Private Sub Search_btn_Click(sender As Object, e As EventArgs) Handles search_btn.Click
        PerformSearch()
    End Sub
    Private Sub Search_filter_btn_Click(sender As Object, e As EventArgs) Handles search_filter_btn.Click
        Store_SearchFilter.ShowDialog()
    End Sub

#End Region

#Region "   Cursors"
    Private Sub Cur_anim_btn_Click(sender As Object, e As EventArgs) Handles cur_anim_btn.Click
        Angle = 180
        Cycles = 0
        Cursor_Timer.Enabled = True
        Cursor_Timer.Start()
    End Sub

    Private Sub Cur_tip_btn_Click(sender As Object, e As EventArgs) Handles cur_tip_btn.Click
        MsgBox(My.Lang.ScalingTip, MsgBoxStyle.Information)
    End Sub
#End Region

#End Region

#Region "Major Tab"
    Private Sub Tabs_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Tabs.SelectedIndexChanged
        If Tabs.SelectedIndex <> 0 Then
            My.Animator.ShowSync(back_btn)
        Else
            My.Animator.HideSync(back_btn)
        End If

        search_panel.Visible = Tabs.SelectedIndex = 0 Or Tabs.SelectedIndex = 2
    End Sub

#End Region

#Region "Others"
    Private Sub CursorsSize_Bar_Scroll(sender As Object) Handles CursorsSize_Bar.Scroll
        If Not _Shown Then Exit Sub

        For Each i As CursorControl In Cursors_Container.Controls
            i.Prop_Scale = sender.value / 100
            i.Width = 32 * i.Prop_Scale + 32
            i.Height = i.Width
            i.Refresh()
        Next

        Label17.Text = String.Format("{0} ({1}x)", My.Lang.Scaling, sender.value / 100)
    End Sub

    Private Sub Search_box_KeyPress(sender As Object, e As KeyPressEventArgs) Handles search_box.KeyboardPress
        If e.KeyChar = ChrW(Keys.Enter) Then PerformSearch()
    End Sub

    Dim newPoint As New Point()
    Dim oldPoint As New Point()

    Private Sub CustomTitlebar_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles Titlebar_panel.MouseDown, Titlebar_lbl.MouseDown, Titlebar_img.MouseDown
        oldPoint = MousePosition - Location
    End Sub

    Private Sub CustomTitlebar_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles Titlebar_panel.MouseMove, Titlebar_lbl.MouseMove, Titlebar_img.MouseMove
        If e.Button = MouseButtons.Left Then
            newPoint = MousePosition - oldPoint
            Location = newPoint
        End If
    End Sub

    Private Sub Author_url_button_Click(sender As Object, e As EventArgs) Handles author_url_button.Click

        If MsgBox(My.Lang.Store_AuthorURLRedirect, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, selectedItem.CP.Info.AuthorSocialMediaLink) = MsgBoxResult.Yes Then
            Try
                If Not String.IsNullOrWhiteSpace(selectedItem.CP.Info.AuthorSocialMediaLink) Then Process.Start(selectedItem.CP.Info.AuthorSocialMediaLink)
            Catch
            End Try
        End If

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        If Not String.IsNullOrWhiteSpace(selectedItem.CP.Info.License) Then
            Store_ThemeLicense.TextBox1.Text = selectedItem.CP.Info.License
            If Not Store_ThemeLicense.ShowDialog = DialogResult.OK Then Exit Sub
        End If

        Using FD As New Ookii.Dialogs.WinForms.VistaFolderBrowserDialog
            If FD.ShowDialog = DialogResult.OK Then
                Dim filename As String = FD.SelectedPath & "\" & New IO.FileInfo(selectedItem.FileName).Name

                If Not IO.Directory.Exists(FD.SelectedPath) Then IO.Directory.CreateDirectory(FD.SelectedPath)
                If IO.File.Exists(filename) Then IO.File.Delete(filename)

                IO.File.Copy(selectedItem.FileName, filename)

                If selectedItem.MD5_PackFile <> "0" Then
                    Dim themepackfilename As String = FD.SelectedPath & "\" & New IO.FileInfo(selectedItem.FileName).Name
                    themepackfilename = themepackfilename.Replace(themepackfilename.Split(".").Last, "wptp")

                    Store_DownloadProgress.URL = selectedItem.URL_PackFile
                    Store_DownloadProgress.File = themepackfilename
                    Store_DownloadProgress.ThemeName = selectedItem.CP.Info.ThemeName
                    Store_DownloadProgress.ThemeVersion = selectedItem.CP.Info.ThemeVersion
                    Store_DownloadProgress.ShowDialog()
                End If
            End If
        End Using

    End Sub

#End Region

End Class