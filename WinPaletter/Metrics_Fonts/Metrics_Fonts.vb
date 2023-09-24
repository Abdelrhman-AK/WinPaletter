Imports System.ComponentModel
Imports System.Drawing.Text
Imports WinPaletter.NativeMethods
Imports WinPaletter.PreviewHelpers

Public Class Metrics_Fonts

    Private Sub EditFonts_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        MenuStrip1.Renderer = New UI.WP.StripRenderer  'Removes the inferior white line from menu strip
        MenuStrip2.Renderer = New UI.WP.StripRenderer

        pnl_preview1.BackgroundImage = MainFrm.pnl_preview.BackgroundImage
        pnl_preview2.BackgroundImage = MainFrm.pnl_preview.BackgroundImage
        pnl_preview3.BackgroundImage = MainFrm.pnl_preview.BackgroundImage
        pnl_preview4.BackgroundImage = MainFrm.pnl_preview.BackgroundImage

        Classic_Preview1.BackgroundImage = MainFrm.pnl_preview.BackgroundImage
        Classic_Preview3.BackgroundImage = MainFrm.pnl_preview.BackgroundImage
        Classic_Preview4.BackgroundImage = MainFrm.pnl_preview.BackgroundImage

        LoadLanguage
        ApplyStyle(Me)
        ApplyFromCP(My.CP)

        Window1.CopycatFrom(MainFrm.Window1, True)
        Window2.CopycatFrom(MainFrm.Window2, True)
        Window4.CopycatFrom(MainFrm.Window1, True)
        Window6.CopycatFrom(MainFrm.Window1, True)

        SetClassicWindowColors(My.CP, WindowR1)
        SetClassicWindowColors(My.CP, WindowR2, False)
        SetClassicWindowColors(My.CP, WindowR3)
        SetClassicWindowColors(My.CP, WindowR5)
        SetClassicPanelColors(My.CP, PanelR1)
        SetClassicPanelColors(My.CP, PanelR2)
        SetClassicButtonColors(My.CP, ButtonR1)
        SetClassicButtonColors(My.CP, ButtonR2)
        SetClassicButtonColors(My.CP, ButtonR3)
        SetClassicButtonColors(My.CP, ButtonR10)
        SetClassicButtonColors(My.CP, ButtonR11)
        SetClassicButtonColors(My.CP, ButtonR12)

        ScrollBarR2.ButtonHilight = My.CP.Win32.ButtonHilight
        ScrollBarR2.BackColor = My.CP.Win32.ButtonFace
        ScrollBarR1.ButtonHilight = My.CP.Win32.ButtonHilight
        ScrollBarR1.BackColor = My.CP.Win32.ButtonFace

        Label13.ForeColor = My.CP.Win32.ButtonText
        Label14.ForeColor = My.CP.Win32.ButtonText
        Refresh17BitPreference()

        DoubleBuffer

        FakeIcon1.Title = "Icon 1"
        FakeIcon2.Title = "Icon 2"
        FakeIcon3.Title = "Icon 3"

        Dim condition0 As Boolean = My.PreviewStyle = WindowStyle.W7 AndAlso My.CP.Windows7.Theme = CP.Structures.Windows7.Themes.Classic
        Dim condition1 As Boolean = My.PreviewStyle = WindowStyle.WVista AndAlso My.CP.WindowsVista.Theme = CP.Structures.Windows7.Themes.Classic
        Dim condition2 As Boolean = My.PreviewStyle = WindowStyle.WXP AndAlso My.CP.WindowsXP.Theme = CP.Structures.WindowsXP.Themes.Classic

        If condition0 Or condition1 Or condition2 Then
            tabs_preview_1.SelectedIndex = 1
            tabs_preview_2.SelectedIndex = 1
            tabs_preview_3.SelectedIndex = 1
        Else
            tabs_preview_1.SelectedIndex = 0
            tabs_preview_2.SelectedIndex = 0
            tabs_preview_3.SelectedIndex = 0
        End If

        FakeIcon1.Icon = MainFrm.Icon                  'My.Resources.fileextension 'Shell32.GetSystemIcon(Shell32.SHSTOCKICONID.RECYCLER, Shell32.SHGSI.ICON)
        FakeIcon2.Icon = My.Resources.fileextension    'My.Resources.settingsfile 'Shell32.GetSystemIcon(Shell32.SHSTOCKICONID.FOLDER, Shell32.SHGSI.ICON)
        FakeIcon3.Icon = My.Resources.ThemesResIcon    'My.Resources.icons8_command_line 'Shell32.GetSystemIcon(Shell32.SHSTOCKICONID.APPLICATION, Shell32.SHGSI.ICON)

        If My.WXP Then
            PictureBox35.Image = SystemIcons.Information.ToBitmap
            PictureBox36.Image = SystemIcons.Information.ToBitmap
        Else
            Dim ico As Icon = DLLFunc.GetSystemIcon(Shell32.SHSTOCKICONID.INFO, Shell32.SHGSI.ICON)
            If ico IsNot Nothing Then
                PictureBox35.Image = ico.ToBitmap
                PictureBox36.Image = ico.ToBitmap
            Else
                PictureBox35.Image = SystemIcons.Information.ToBitmap
                PictureBox36.Image = SystemIcons.Information.ToBitmap
            End If
        End If

        Dim Win7 As Boolean = Window6.Preview = UI.Simulation.Window.Preview_Enum.W7Aero Or Window6.Preview = UI.Simulation.Window.Preview_Enum.W7Opaque Or Window6.Preview = UI.Simulation.Window.Preview_Enum.W7Basic
        Dim Win8 As Boolean = Window6.Preview = UI.Simulation.Window.Preview_Enum.W8 Or Window6.Preview = UI.Simulation.Window.Preview_Enum.W8Lite
        Dim WinXP As Boolean = Window6.Preview = UI.Simulation.Window.Preview_Enum.WXP

        If Not Win7 And Not Win8 And Not WinXP Then
            msgLbl.ForeColor = If(Window6.DarkMode, Color.White, Color.Black)
            MenuStrip1.BackColor = If(Window6.DarkMode, Color.FromArgb(35, 35, 35), Color.FromArgb(255, 255, 255))
            MenuStrip1.ForeColor = If(Window6.DarkMode, Color.White, Color.Black)
        Else
            msgLbl.ForeColor = Color.Black
            MenuStrip1.BackColor = Color.FromArgb(255, 255, 255)
            MenuStrip1.ForeColor = Color.Black
        End If

        Button12.Image = MainFrm.Button20.Image.Resize(16, 16)
        AlertBox10.Text = My.Lang.CP_MetricsHighDPIAlert

        AlertBox11.Text = MainFrm.WXP_Alert2.Text
        AlertBox11.Visible = MainFrm.WXP_Alert2.Visible
        AlertBox11.Size = AlertBox11.Parent.Size - New Size(40, 40)
        AlertBox11.Location = New Point(20, 20)

        AlertBox12.Text = AlertBox11.Text
        AlertBox12.Visible = AlertBox11.Visible
        AlertBox12.Size = AlertBox11.Size
        AlertBox12.Location = AlertBox11.Location

        AlertBox13.Text = AlertBox11.Text
        AlertBox13.Visible = AlertBox11.Visible
        AlertBox13.Size = AlertBox11.Size
        AlertBox13.Location = AlertBox11.Location

        MainFrm.Visible = False
    End Sub

    Sub Refresh17BitPreference()

        If My.CP.Win32.EnableTheming Then
            MenuStrip2.BackColor = My.CP.Win32.MenuBar
        Else
            MenuStrip2.BackColor = My.CP.Win32.Menu
        End If

        ToolStripMenuItem1.ForeColor = My.CP.Win32.MenuText
        ToolStripMenuItem4.ForeColor = My.CP.Win32.MenuText

    End Sub

    Sub ApplyFromCP(CP As CP)
        MetricsEnabled.Checked = CP.MetricsFonts.Enabled

        Label1.Font = CP.MetricsFonts.CaptionFont
        Window1.Font = CP.MetricsFonts.CaptionFont
        WindowR1.Font = CP.MetricsFonts.CaptionFont
        WindowR3.Font = CP.MetricsFonts.CaptionFont
        WindowR5.Font = CP.MetricsFonts.CaptionFont

        Label1.Text = CP.MetricsFonts.CaptionFont.Name

        Label2.Font = CP.MetricsFonts.IconFont
        FakeIcon1.Font = CP.MetricsFonts.IconFont
        FakeIcon2.Font = CP.MetricsFonts.IconFont
        FakeIcon3.Font = CP.MetricsFonts.IconFont
        Label2.Text = CP.MetricsFonts.IconFont.Name

        Label3.Font = CP.MetricsFonts.MenuFont
        MenuStrip1.Font = CP.MetricsFonts.MenuFont
        MenuStrip2.Font = CP.MetricsFonts.MenuFont
        Label3.Text = CP.MetricsFonts.MenuFont.Name

        Label5.Font = CP.MetricsFonts.SmCaptionFont
        Window2.Font = CP.MetricsFonts.SmCaptionFont
        WindowR2.Font = CP.MetricsFonts.SmCaptionFont
        Label5.Text = CP.MetricsFonts.SmCaptionFont.Name

        Label4.Font = CP.MetricsFonts.MessageFont
        msgLbl.Font = CP.MetricsFonts.MessageFont
        Label13.Font = CP.MetricsFonts.MessageFont
        Label4.Text = CP.MetricsFonts.MessageFont.Name

        Label6.Font = CP.MetricsFonts.StatusFont
        statusLbl.Font = CP.MetricsFonts.StatusFont
        Label14.Font = CP.MetricsFonts.StatusFont
        Label6.Text = CP.MetricsFonts.StatusFont.Name
        PanelR1.Height = Math.Max(GetTitleTextHeight(CP.MetricsFonts.StatusFont), 20)

        TextBox1.Text = CP.MetricsFonts.FontSubstitute_MSShellDlg
        TextBox2.Text = CP.MetricsFonts.FontSubstitute_MSShellDlg2
        TextBox3.Text = CP.MetricsFonts.FontSubstitute_SegoeUI

        CheckBox1.Checked = CP.MetricsFonts.Fonts_SingleBitPP

        Trackbar1.Value = CP.MetricsFonts.BorderWidth
        Trackbar2.Value = CP.MetricsFonts.CaptionHeight
        Trackbar3.Value = CP.MetricsFonts.CaptionWidth
        Trackbar6.Value = CP.MetricsFonts.IconSpacing
        Trackbar4.Value = CP.MetricsFonts.IconVerticalSpacing
        Trackbar9.Value = CP.MetricsFonts.MenuHeight
        Trackbar8.Value = CP.MetricsFonts.MenuWidth
        Trackbar12.Value = CP.MetricsFonts.PaddedBorderWidth
        Trackbar11.Value = CP.MetricsFonts.ScrollHeight
        Trackbar10.Value = CP.MetricsFonts.ScrollWidth
        Trackbar14.Value = CP.MetricsFonts.SmCaptionHeight
        Trackbar13.Value = CP.MetricsFonts.SmCaptionWidth
        Trackbar7.Value = CP.MetricsFonts.DesktopIconSize
        Trackbar5.Value = CP.MetricsFonts.ShellIconSize
        Trackbar15.Value = CP.MetricsFonts.ShellSmallIconSize

        WindowR1.Metrics_CaptionWidth = CP.MetricsFonts.CaptionWidth
        WindowR3.Metrics_CaptionWidth = CP.MetricsFonts.CaptionWidth
        WindowR5.Metrics_CaptionWidth = CP.MetricsFonts.CaptionWidth

        If CP.WindowsEffects.IconsShadow Then
            FakeIcon1.ColorGlow = Color.FromArgb(75, 0, 0, 0)
        Else
            FakeIcon1.ColorGlow = Color.FromArgb(0, 0, 0, 0)
        End If
        FakeIcon2.ColorGlow = FakeIcon1.ColorGlow
        FakeIcon3.ColorGlow = FakeIcon1.ColorGlow

        WindowR1.Refresh()
        WindowR2.Refresh()
        WindowR3.Refresh()
        WindowR5.Refresh()

        Dim theme As CtrlTheme, statusBackColor, StatusForeColor As Color

        If My.PreviewStyle = WindowStyle.W11 Then

            If CP.Windows11.AppMode_Light Then
                theme = CtrlTheme.Default
                StatusForeColor = Color.Black
                statusBackColor = Color.White
            Else
                theme = CtrlTheme.DarkExplorer
                StatusForeColor = Color.White
                statusBackColor = Color.FromArgb(28, 28, 28)
            End If

        ElseIf My.PreviewStyle = WindowStyle.W10 Then

            If CP.Windows10.AppMode_Light Then
                theme = CtrlTheme.Default
                StatusForeColor = Color.Black
                statusBackColor = Color.White
            Else
                theme = CtrlTheme.DarkExplorer
                StatusForeColor = Color.White
                statusBackColor = Color.FromArgb(28, 28, 28)
            End If

        Else
            StatusForeColor = Color.Black
            statusBackColor = Color.White
            theme = CtrlTheme.Default
        End If

        SetControlTheme(MenuStrip1.Handle, theme)
        SetControlTheme(VScrollBar1.Handle, theme)
        SetControlTheme(HScrollBar1.Handle, theme)
        SetControlTheme(StatusStrip1.Handle, theme)

        statusLbl.ForeColor = StatusForeColor
        StatusStrip1.BackColor = statusBackColor
    End Sub

    Sub ApplyToCP(CP As CP)
        CP.MetricsFonts.Enabled = MetricsEnabled.Checked

        CP.MetricsFonts.CaptionFont = Label1.Font
        CP.MetricsFonts.IconFont = Label2.Font
        CP.MetricsFonts.MenuFont = Label3.Font
        CP.MetricsFonts.MessageFont = Label4.Font
        CP.MetricsFonts.SmCaptionFont = Label5.Font
        CP.MetricsFonts.StatusFont = Label6.Font

        CP.MetricsFonts.BorderWidth = Trackbar1.Value
        CP.MetricsFonts.CaptionHeight = Trackbar2.Value
        CP.MetricsFonts.CaptionWidth = Trackbar3.Value
        CP.MetricsFonts.IconSpacing = Trackbar6.Value
        CP.MetricsFonts.IconVerticalSpacing = Trackbar4.Value
        CP.MetricsFonts.MenuHeight = Trackbar9.Value
        CP.MetricsFonts.MenuWidth = Trackbar8.Value
        CP.MetricsFonts.PaddedBorderWidth = Trackbar12.Value
        CP.MetricsFonts.ScrollHeight = Trackbar11.Value
        CP.MetricsFonts.ScrollWidth = Trackbar10.Value
        CP.MetricsFonts.SmCaptionHeight = Trackbar14.Value
        CP.MetricsFonts.SmCaptionWidth = Trackbar13.Value
        CP.MetricsFonts.DesktopIconSize = Trackbar7.Value
        CP.MetricsFonts.ShellIconSize = Trackbar5.Value
        CP.MetricsFonts.ShellSmallIconSize = Trackbar15.Value
        CP.MetricsFonts.Fonts_SingleBitPP = CheckBox1.Checked

        CP.MetricsFonts.FontSubstitute_MSShellDlg = TextBox1.Text
        CP.MetricsFonts.FontSubstitute_MSShellDlg2 = TextBox2.Text
        CP.MetricsFonts.FontSubstitute_SegoeUI = TextBox3.Text
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        FontDialog1.Font = Label1.Font
        If FontDialog1.ShowDialog = DialogResult.OK Then
            Label1.Font = FontDialog1.Font
            Window1.Font = FontDialog1.Font
            WindowR1.Font = FontDialog1.Font
            Window4.Font = FontDialog1.Font
            WindowR3.Font = FontDialog1.Font
            Window6.Font = FontDialog1.Font
            WindowR5.Font = FontDialog1.Font
            Label1.Text = FontDialog1.Font.Name
            Window1.Refresh()
            WindowR1.Refresh()
            Window4.Refresh()
            WindowR3.Refresh()
            Window6.Refresh()
            WindowR5.Refresh()
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        FontDialog1.Font = Label2.Font
        If FontDialog1.ShowDialog = DialogResult.OK Then
            Label2.Font = FontDialog1.Font
            FakeIcon1.Font = FontDialog1.Font
            FakeIcon2.Font = FontDialog1.Font
            FakeIcon3.Font = FontDialog1.Font
            Label2.Text = FontDialog1.Font.Name

        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        FontDialog1.Font = Label3.Font
        If FontDialog1.ShowDialog = DialogResult.OK Then
            Label3.Font = FontDialog1.Font
            MenuStrip1.Font = FontDialog1.Font
            MenuStrip2.Font = FontDialog1.Font
            Label3.Text = FontDialog1.Font.Name
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        FontDialog1.Font = Label4.Font
        If FontDialog1.ShowDialog = DialogResult.OK Then
            Label4.Font = FontDialog1.Font
            msgLbl.Font = FontDialog1.Font
            Label13.Font = FontDialog1.Font
            Label4.Text = FontDialog1.Font.Name

        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        FontDialog1.Font = Label5.Font
        If FontDialog1.ShowDialog = DialogResult.OK Then
            Label5.Font = FontDialog1.Font
            Window2.Font = FontDialog1.Font
            WindowR2.Font = FontDialog1.Font
            Label5.Text = FontDialog1.Font.Name
        End If
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        FontDialog1.Font = Label6.Font
        If FontDialog1.ShowDialog = DialogResult.OK Then
            Label6.Font = FontDialog1.Font
            Label14.Font = FontDialog1.Font
            statusLbl.Font = FontDialog1.Font
            Label6.Text = FontDialog1.Font.Name
            PanelR1.Height = Math.Max(GetTitleTextHeight(FontDialog1.Font), 20)
        End If
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        ApplyToCP(My.CP)
        Me.Close()
        SetModernWindowMetrics(My.CP, MainFrm.Window1)
        SetModernWindowMetrics(My.CP, MainFrm.Window2)
        SetClassicWindowMetrics(My.CP, MainFrm.ClassicWindow1)
        SetClassicWindowMetrics(My.CP, MainFrm.ClassicWindow2)
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Me.Close()
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        Cursor = Cursors.WaitCursor
        Dim CPx As New CP(CP.CP_Type.Registry)
        ApplyToCP(CPx)
        ApplyToCP(My.CP)
        SetModernWindowMetrics(CPx, MainFrm.Window1)
        SetModernWindowMetrics(CPx, MainFrm.Window2)
        SetClassicWindowMetrics(CPx, MainFrm.ClassicWindow1)
        SetClassicWindowMetrics(CPx, MainFrm.ClassicWindow2)

        CPx.MetricsFonts.Apply()
        CPx.Dispose()
        Cursor = Cursors.Default
    End Sub

    Private Sub Trackbar2_Scroll(sender As Object) Handles Trackbar2.Scroll
        ttl_h.Text = sender.Value.ToString

        Window1.Metrics_CaptionHeight = sender.Value
        WindowR1.Metrics_CaptionHeight = sender.Value
        WindowR3.Metrics_CaptionHeight = sender.Value
        WindowR5.Metrics_CaptionHeight = sender.Value
    End Sub

    Private Sub Trackbar3_Scroll(sender As Object) Handles Trackbar3.Scroll
        ttl_w.Text = sender.Value
        WindowR1.Metrics_CaptionWidth = sender.Value
        WindowR3.Metrics_CaptionWidth = sender.Value
        WindowR5.Metrics_CaptionWidth = sender.Value
    End Sub

    Private Sub Trackbar9_Scroll(sender As Object) Handles Trackbar9.Scroll
        m_h.Text = sender.Value
        MenuStrip1.Height = Math.Max(sender.Value, GetTitleTextHeight(MenuStrip1.Font))
        MenuStrip2.Height = MenuStrip1.Height
        PanelR2.Refresh()
    End Sub

    Private Sub Trackbar8_Scroll(sender As Object) Handles Trackbar8.Scroll
        m_w.Text = sender.Value
    End Sub

    Private Sub Trackbar1_Scroll(sender As Object) Handles Trackbar1.Scroll
        ttl_b.Text = sender.Value
        Window1.Metrics_BorderWidth = sender.Value
        WindowR1.Metrics_BorderWidth = sender.Value
        WindowR2.Metrics_BorderWidth = sender.Value
        WindowR3.Metrics_BorderWidth = sender.Value
        WindowR5.Metrics_BorderWidth = sender.Value
    End Sub

    Private Sub Trackbar12_Scroll(sender As Object) Handles Trackbar12.Scroll
        ttl_p.Text = sender.Value
        Window1.Metrics_PaddedBorderWidth = sender.Value
        WindowR1.Metrics_PaddedBorderWidth = sender.Value
        WindowR2.Metrics_PaddedBorderWidth = sender.Value
        WindowR3.Metrics_PaddedBorderWidth = sender.Value
        WindowR5.Metrics_PaddedBorderWidth = sender.Value
    End Sub

    Private Sub Trackbar11_Scroll(sender As Object) Handles Trackbar11.Scroll
        s_h.Text = sender.Value
        HScrollBar1.Height = sender.Value
        ScrollBarR1.Height = sender.Value
        ButtonR1.Height = sender.Value
        ScrollBarR1.Refresh()
    End Sub

    Private Sub Trackbar10_Scroll(sender As Object) Handles Trackbar10.Scroll
        s_w.Text = sender.Value
        VScrollBar1.Width = sender.Value
        ScrollBarR2.Width = sender.Value
        ButtonR12.Width = sender.Value
        ScrollBarR2.Refresh()
    End Sub

    Private Sub Trackbar14_Scroll(sender As Object) Handles Trackbar14.Scroll
        tw_h.Text = sender.Value
        Window2.Metrics_CaptionHeight = sender.Value
        WindowR2.Metrics_CaptionHeight = sender.Value
    End Sub

    Private Sub Trackbar13_Scroll(sender As Object) Handles Trackbar13.Scroll
        tw_w.Text = sender.Value
        WindowR2.Metrics_CaptionWidth = sender.Value
    End Sub

    Private Sub Trackbar7_Scroll(sender As Object) Handles Trackbar7.Scroll
        If Trackbar7.Value < Trackbar7.Minimum Then
            Trackbar7.Value = Trackbar7.Minimum
        End If

        If Trackbar7.Value > Trackbar7.Maximum Then
            Trackbar7.Value = Trackbar7.Maximum
        End If

        i_d_s.Text = sender.Value
        FakeIcon1.IconSize = sender.Value
        FakeIcon2.IconSize = sender.Value
        FakeIcon3.IconSize = sender.Value

        FakeIcon1.Height = sender.Value + 35
        FakeIcon2.Height = sender.Value + 35
        FakeIcon3.Height = sender.Value + 35
        FakeIcon2.Top = FakeIcon1.Bottom + (Trackbar4.Value - 45)  '(((Trackbar4.Value - 5) / 2) + 5)

        FakeIcon1.Width = sender.Value + 15 + Trackbar6.Value / 16 '18
        FakeIcon2.Width = sender.Value + 15 + Trackbar6.Value / 16
        FakeIcon3.Width = sender.Value + 15 + Trackbar6.Value / 16
        FakeIcon3.Left = FakeIcon1.Right + 2

    End Sub

    Private Sub Trackbar6_Scroll(sender As Object) Handles Trackbar6.Scroll
        i_s_h.Text = sender.Value

        FakeIcon1.Width = Trackbar7.Value + 15 + sender.Value / 16 '18
        FakeIcon2.Width = Trackbar7.Value + 15 + sender.Value / 16
        FakeIcon3.Width = Trackbar7.Value + 15 + sender.Value / 16
        FakeIcon3.Left = FakeIcon1.Right + 2

        FakeIcon3.SendToBack()
        FakeIcon1.BringToFront()
    End Sub

    Private Sub Trackbar4_Scroll(sender As Object) Handles Trackbar4.Scroll
        i_s_v.Text = sender.Value

        FakeIcon2.Top = FakeIcon1.Bottom + (Trackbar4.Value - 45)

        FakeIcon2.SendToBack()
        FakeIcon1.BringToFront()
    End Sub

    Private Sub Label1_FontChanged(sender As Object, e As EventArgs) Handles Label1.FontChanged, Label2.FontChanged, Label3.FontChanged, Label4.FontChanged, Label5.FontChanged, Label6.FontChanged
        DirectCast(sender, Label).Text = DirectCast(sender, Label).Font.FontFamily.Name
    End Sub

    Private Sub Trackbar5_Scroll(sender As Object) Handles Trackbar5.Scroll
        i_s_s.Text = sender.Value
    End Sub

    Private Sub Window1_MetricsChanged() Handles Window1.MetricsChanged
        Window1.SetMetrics(Window4)
        Window1.SetMetrics(Window6)
    End Sub

    Private Sub Window1_FontChanged(sender As Object, e As EventArgs) Handles Window1.FontChanged
        Window4.Font = Window1.Font
        Window6.Font = Window1.Font
    End Sub

    Private Sub MenuStrip1_FontChanged(sender As Object, e As EventArgs) Handles MenuStrip1.FontChanged
        MenuStrip1.Height = Math.Max(Trackbar9.Value, GetTitleTextHeight(MenuStrip1.Font))
    End Sub

    Public Function GetTitleTextHeight([Font] As Font) As Integer
        Dim TitleTextH As Integer ', TitleTextH_9, TitleTextH_Sum As Integer
        TitleTextH = "ABCabc0123xYz.#".Measure([Font]).Height
        'TitleTextH_9 = "ABCabc0123xYz.#".Measure(New Font([Font].Name, 9, [Font].Style)).Height
        'TitleTextH_Sum = Math.Max(0, TitleTextH - TitleTextH_9)

        Return [Font].Height 'TitleTextH 'TitleTextH_Sum
    End Function

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        If OpenFileDialog1.ShowDialog = DialogResult.OK Then
            Dim CPx As New CP(CP.CP_Type.File, OpenFileDialog1.FileName)
            ApplyFromCP(CPx)
            CPx.Dispose()
        End If
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Dim CPx As New CP(CP.CP_Type.Registry)
        ApplyFromCP(CPx)
        CPx.Dispose()
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        Using _Def As CP = CP_Defaults.From(My.PreviewStyle)
            ApplyFromCP(_Def)
        End Using
    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles ttl_h.Click
        Dim response As String = InputBox(My.Lang.InputValue, sender.text, My.Lang.ItMustBeNumerical)
        sender.Text = Math.Max(Math.Min(Val(response), Trackbar2.Maximum), Trackbar2.Minimum) : Trackbar2.Value = Val(sender.Text)
    End Sub

    Private Sub Button13_Click_1(sender As Object, e As EventArgs) Handles ttl_w.Click
        Dim response As String = InputBox(My.Lang.InputValue, sender.text, My.Lang.ItMustBeNumerical)
        sender.Text = Math.Max(Math.Min(Val(response), Trackbar3.Maximum), Trackbar3.Minimum) : Trackbar3.Value = Val(sender.Text)
    End Sub

    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles ttl_b.Click
        Dim response As String = InputBox(My.Lang.InputValue, sender.text, My.Lang.ItMustBeNumerical)
        sender.Text = Math.Max(Math.Min(Val(response), Trackbar1.Maximum), Trackbar1.Minimum) : Trackbar1.Value = Val(sender.Text)
    End Sub

    Private Sub Button15_Click(sender As Object, e As EventArgs) Handles ttl_p.Click
        Dim response As String = InputBox(My.Lang.InputValue, sender.text, My.Lang.ItMustBeNumerical)
        sender.Text = Math.Max(Math.Min(Val(response), Trackbar12.Maximum), Trackbar12.Minimum) : Trackbar12.Value = Val(sender.Text)
    End Sub

    Private Sub Tw_h_Click(sender As Object, e As EventArgs) Handles tw_h.Click
        Dim response As String = InputBox(My.Lang.InputValue, sender.text, My.Lang.ItMustBeNumerical)
        sender.Text = Math.Max(Math.Min(Val(response), Trackbar14.Maximum), Trackbar14.Minimum) : Trackbar14.Value = Val(sender.Text)
    End Sub

    Private Sub Tw_w_Click(sender As Object, e As EventArgs) Handles tw_w.Click
        Dim response As String = InputBox(My.Lang.InputValue, sender.text, My.Lang.ItMustBeNumerical)
        sender.Text = Math.Max(Math.Min(Val(response), Trackbar13.Maximum), Trackbar13.Minimum) : Trackbar13.Value = Val(sender.Text)
    End Sub

    Private Sub I_s_v_Click(sender As Object, e As EventArgs) Handles i_s_v.Click
        Dim response As String = InputBox(My.Lang.InputValue, sender.text, My.Lang.ItMustBeNumerical)
        sender.Text = Math.Max(Math.Min(Val(response), Trackbar4.Maximum), Trackbar4.Minimum) : Trackbar4.Value = Val(sender.Text)
    End Sub

    Private Sub I_s_h_Click(sender As Object, e As EventArgs) Handles i_s_h.Click
        Dim response As String = InputBox(My.Lang.InputValue, sender.text, My.Lang.ItMustBeNumerical)
        sender.Text = Math.Max(Math.Min(Val(response), Trackbar6.Maximum), Trackbar6.Minimum) : Trackbar6.Value = Val(sender.Text)
    End Sub

    Private Sub I_d_s_Click(sender As Object, e As EventArgs) Handles i_d_s.Click
        Dim response As String = InputBox(My.Lang.InputValue, sender.text, My.Lang.ItMustBeNumerical)
        sender.Text = Math.Max(Math.Min(Val(response), Trackbar7.Maximum), Trackbar7.Minimum) : Trackbar7.Value = Val(sender.Text)
    End Sub

    Private Sub I_s_s_Click(sender As Object, e As EventArgs) Handles i_s_s.Click
        Dim response As String = InputBox(My.Lang.InputValue, sender.text, My.Lang.ItMustBeNumerical)
        sender.Text = Math.Max(Math.Min(Val(response), Trackbar5.Maximum), Trackbar5.Minimum) : Trackbar5.Value = Val(sender.Text)
    End Sub

    Private Sub Mh_Click(sender As Object, e As EventArgs) Handles m_h.Click
        Dim response As String = InputBox(My.Lang.InputValue, sender.text, My.Lang.ItMustBeNumerical)
        sender.Text = Math.Max(Math.Min(Val(response), Trackbar9.Maximum), Trackbar9.Minimum) : Trackbar9.Value = Val(sender.Text)
    End Sub

    Private Sub Mw_Click(sender As Object, e As EventArgs) Handles m_w.Click
        Dim response As String = InputBox(My.Lang.InputValue, sender.text, My.Lang.ItMustBeNumerical)
        sender.Text = Math.Max(Math.Min(Val(response), Trackbar8.Maximum), Trackbar8.Minimum) : Trackbar8.Value = Val(sender.Text)
    End Sub

    Private Sub Sh_Click(sender As Object, e As EventArgs) Handles s_h.Click
        Dim response As String = InputBox(My.Lang.InputValue, sender.text, My.Lang.ItMustBeNumerical)
        sender.Text = Math.Max(Math.Min(Val(response), Trackbar11.Maximum), Trackbar11.Minimum) : Trackbar11.Value = Val(sender.Text)
    End Sub

    Private Sub Sw_Click(sender As Object, e As EventArgs) Handles s_w.Click
        Dim response As String = InputBox(My.Lang.InputValue, sender.text, My.Lang.ItMustBeNumerical)
        sender.Text = Math.Max(Math.Min(Val(response), Trackbar10.Maximum), Trackbar10.Minimum) : Trackbar10.Value = Val(sender.Text)
    End Sub

    Private Sub Button13_Click_2(sender As Object, e As EventArgs) Handles Button13.Click
        If tabs_preview_1.SelectedIndex = 0 Then
            tabs_preview_1.SelectedIndex = 1
            tabs_preview_2.SelectedIndex = 1
            tabs_preview_3.SelectedIndex = 1
        Else
            tabs_preview_1.SelectedIndex = 0
            tabs_preview_2.SelectedIndex = 0
            tabs_preview_3.SelectedIndex = 0
        End If
    End Sub

    Private Sub Metrics_Fonts_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        My.RenderingHint = If(My.CP.MetricsFonts.Fonts_SingleBitPP, TextRenderingHint.SingleBitPerPixelGridFit, TextRenderingHint.ClearTypeGridFit)
        MainFrm.Visible = True
    End Sub

    Private Sub Button14_Click_1(sender As Object, e As EventArgs) Handles Button14.Click
        Dim F As New Font(TextBox1.Text, 9, FontStyle.Regular)
        FontDialog2.Font = F
        If FontDialog2.ShowDialog = DialogResult.OK Then
            TextBox1.Text = FontDialog2.Font.Name
        End If
    End Sub

    Private Sub Button15_Click_1(sender As Object, e As EventArgs) Handles Button15.Click
        Dim F As New Font(TextBox2.Text, 9, FontStyle.Regular)
        FontDialog2.Font = F
        If FontDialog2.ShowDialog = DialogResult.OK Then
            TextBox2.Text = FontDialog2.Font.Name
        End If
    End Sub

    Private Sub Button17_Click(sender As Object, e As EventArgs) Handles Button17.Click
        Dim F As New Font(TextBox3.Text, 9, FontStyle.Regular)
        FontDialog2.Font = F
        If FontDialog2.ShowDialog = DialogResult.OK Then
            TextBox3.Text = FontDialog2.Font.Name
        End If
    End Sub

    Private Sub Button16_Click_1(sender As Object, e As EventArgs) Handles Button16.Click
        TextBox1.Text = "Microsoft Sans Serif"
    End Sub

    Private Sub Button18_Click(sender As Object, e As EventArgs) Handles Button18.Click
        TextBox2.Text = "Tahoma"
    End Sub

    Private Sub Button19_Click(sender As Object, e As EventArgs) Handles Button19.Click
        TextBox3.Text = ""
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

        If CP.IsFontInstalled(sender.Text.ToString, FontStyle.Regular) Then
            sender.Font = New Font(sender.Text.ToString, 9, FontStyle.Regular)
        Else
            sender.Font = New Font("Microsoft Sans Serif", 9, FontStyle.Regular)
        End If

    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged

        If CP.IsFontInstalled(sender.Text.ToString, FontStyle.Regular) Then
            sender.Font = New Font(sender.Text.ToString, 9, FontStyle.Regular)
        Else
            sender.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If

    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged

        If CP.IsFontInstalled(sender.Text.ToString, FontStyle.Regular) Then
            sender.Font = New Font(sender.Text.ToString, 9, FontStyle.Regular)
        Else
            sender.Font = New Font("Segoe UI", 9, FontStyle.Regular)
        End If

    End Sub

    Private Sub Button20_Click(sender As Object, e As EventArgs) Handles Button20.Click
        VS2Metrics.ShowDialog()
    End Sub

    Private Sub MetricsEnabled_CheckedChanged(sender As Object, e As EventArgs) Handles MetricsEnabled.CheckedChanged
        checker_img.Image = If(sender.Checked, My.Resources.checker_enabled, My.Resources.checker_disabled)
    End Sub

    Private Sub Trackbar15_Scroll(sender As Object) Handles Trackbar15.Scroll
        i_s_s_s.Text = sender.Value
    End Sub

    Private Sub I_s_s_s_Click(sender As Object, e As EventArgs) Handles i_s_s_s.Click
        Dim response As String = InputBox(My.Lang.InputValue, sender.text, My.Lang.ItMustBeNumerical)
        sender.Text = Math.Max(Math.Min(Val(response), Trackbar15.Maximum), Trackbar15.Minimum) : Trackbar15.Value = Val(sender.Text)
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object) Handles CheckBox1.CheckedChanged
        My.RenderingHint = If(CheckBox1.Checked, TextRenderingHint.SingleBitPerPixelGridFit, TextRenderingHint.ClearTypeGridFit)
        Window1.Refresh()
        Window2.Refresh()
        Window4.Refresh()
        Window6.Refresh()
        WindowR1.Refresh()
        WindowR2.Refresh()
        WindowR3.Refresh()
        WindowR5.Refresh()
    End Sub

    Private Sub Metrics_Fonts_HelpButtonClicked(sender As Object, e As CancelEventArgs) Handles Me.HelpButtonClicked
        Process.Start(My.Resources.Link_Wiki & "/Edit-Windows-Metrics-and-Fonts")
    End Sub
End Class