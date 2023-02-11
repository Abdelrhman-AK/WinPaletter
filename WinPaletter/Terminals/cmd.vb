Imports WinPaletter.NativeMethods
Imports WinPaletter.XenonCore

Public Class CMD
    Dim F_cmd As New Font("Consolas", 18, FontStyle.Regular)
    Private _Shown As Boolean = False
    Public _Edition As Edition = Edition.CMD

    Enum Edition
        CMD
        PowerShellx86
        PowerShellx64
    End Enum

#Region "   Subs not related to colors and shapes"
    Private Sub CMD_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _Shown = False
        ApplyDarkMode(Me)
        XenonCheckBox1.Checked = My.[Settings].Terminal_OtherFonts
        FillFonts(CMD_FontsBox, Not My.[Settings].Terminal_OtherFonts)
        RasterList.BringToFront()

        ApplyFromCP(MainFrm.CP, _Edition)
        ApplyPreview()

        CMD_PopupForegroundLbl.Font = My.Application.ConsoleFont
        CMD_PopupBackgroundLbl.Font = My.Application.ConsoleFont
        CMD_AccentForegroundLbl.Font = My.Application.ConsoleFont
        CMD_AccentBackgroundLbl.Font = My.Application.ConsoleFont
        MainFrm.Visible = False
        Location = New Point(10, (My.Computer.Screen.Bounds.Height - Height) / 2 - 20)

        Select Case _Edition
            Case Edition.CMD
                Text = My.Lang.CommandPrompt
                Icon = My.Resources.icons8_command_line
                XenonButton4.Text = My.Lang.Open_Testing_CMD

            Case Edition.PowerShellx86
                Text = My.Lang.PowerShellx86
                Icon = My.Resources.icons8_PowerShell
                XenonButton4.Text = My.Lang.Open_Testing_PowerShellx86

            Case Edition.PowerShellx64
                Text = My.Lang.PowerShellx64
                Icon = My.Resources.icons8_PowerShell
                XenonButton4.Text = My.Lang.Open_Testing_PowerShellx64

        End Select

        XenonButton6.Image = MainFrm.XenonButton20.Image.Resize(16, 16)
    End Sub
    Private Sub XenonCheckBox1_CheckedChanged(sender As Object) Handles XenonCheckBox1.CheckedChanged
        If _Shown Then
            FillFonts(CMD_FontsBox, Not XenonCheckBox1.Checked)
            My.[Settings].Terminal_OtherFonts = XenonCheckBox1.Checked
            My.[Settings].Save(XeSettings.Mode.Registry)
        End If
    End Sub

    Sub FillFonts([ListBox] As ComboBox, Optional FixedPitch As Boolean = False)
        Dim s As String = [ListBox].SelectedItem

        [ListBox].Items.Clear()

        If Not FixedPitch Then
            For Each x As String In My.FontsList
                [ListBox].Items.Add(x)
            Next
        Else
            For Each x As String In My.FontsFixedList
                [ListBox].Items.Add(x)
            Next
        End If

        [ListBox].SelectedItem = s

        If [ListBox].SelectedItem = Nothing Then [ListBox].SelectedIndex = 0
    End Sub

    Private Sub XenonButton10_Click(sender As Object, e As EventArgs) Handles XenonButton10.Click

        If CMDEnabled.Checked Then
            Cursor = Cursors.WaitCursor
            Dim CPx As New CP(CP.Mode.Registry)
            ApplyToCP(CPx, _Edition)

            Select Case _Edition
                Case Edition.CMD
                    CPx.Apply_CommandPrompt()

                Case Edition.PowerShellx86
                    CPx.Apply_PowerShell86()

                Case Edition.PowerShellx64
                    CPx.Apply_PowerShell64()

                Case Else
                    CPx.Apply_CommandPrompt()

            End Select

            CPx.Dispose()

            Cursor = Cursors.Default
        Else
            MsgBox(My.Lang.CMD_Enable, MsgBoxStyle.Critical)
        End If

    End Sub
    Private Sub XenonButton1_Click(sender As Object, e As EventArgs) Handles XenonButton1.Click
        ApplyToCP(MainFrm.CP, _Edition)
        Me.Close()
    End Sub
    Private Sub XenonButton2_Click(sender As Object, e As EventArgs) Handles XenonButton2.Click
        Me.Close()
    End Sub
    Private Sub CMD_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        MainFrm.Visible = True
    End Sub
    Private Sub CMD_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        _Shown = True
    End Sub
#End Region

#Region "   Subs to modify colors or shapes"
    Public Sub ApplyCursorShape()
        CMD_PreviewCursorInner.Dock = DockStyle.Fill
        CMD_PreviewCUR2.Padding = New Padding(1, 1, 1, 1)

        If CMD_CursorStyle.SelectedIndex = 0 Then
            CMD_PreviewCursorInner.BackColor = Color.Transparent
            CMD_PreviewCUR2.Width = 8

            Dim all As Integer = CMD_PreviewCUR.Height - 4
            CMD_PreviewCUR2.Height = all * (CMD_CursorSizeBar.Value / CMD_CursorSizeBar.Maximum)
            CMD_PreviewCUR2.Top = CMD_PreviewCUR.Height - CMD_PreviewCUR2.Height - 2
        End If

        If CMD_CursorStyle.SelectedIndex = 1 Then
            CMD_PreviewCursorInner.BackColor = Color.Transparent

            CMD_PreviewCUR2.Width = 1

            Dim all As Integer = CMD_PreviewCUR.Height - 4
            CMD_PreviewCUR2.Height = all
            CMD_PreviewCUR2.Top = CMD_PreviewCUR.Height - CMD_PreviewCUR2.Height - 2
        End If

        If CMD_CursorStyle.SelectedIndex = 2 Then
            CMD_PreviewCursorInner.BackColor = Color.Transparent

            CMD_PreviewCUR2.Width = 10
            CMD_PreviewCUR2.Height = 1

            CMD_PreviewCUR2.Top = CMD_PreviewCUR.Height - CMD_PreviewCUR2.Height - 2
        End If

        If CMD_CursorStyle.SelectedIndex = 3 Then
            CMD_PreviewCursorInner.BackColor = CMD_PreviewCUR.BackColor

            CMD_PreviewCUR2.Width = 8

            Dim all As Integer = CMD_PreviewCUR.Height - 4
            CMD_PreviewCUR2.Height = all
            CMD_PreviewCUR2.Top = CMD_PreviewCUR.Height - CMD_PreviewCUR2.Height - 2
        End If

        If CMD_CursorStyle.SelectedIndex = 4 Then
            CMD_PreviewCursorInner.BackColor = Color.Transparent

            CMD_PreviewCUR2.Width = 8

            Dim all As Integer = CMD_PreviewCUR.Height - 4
            CMD_PreviewCUR2.Height = all
            CMD_PreviewCUR2.Top = CMD_PreviewCUR.Height - CMD_PreviewCUR2.Height - 2
        End If

        If CMD_CursorStyle.SelectedIndex = 5 Then
            CMD_PreviewCursorInner.Dock = DockStyle.None
            CMD_PreviewCUR2.Padding = New Padding(0, 0, 0, 0)
            CMD_PreviewCursorInner.Width = CMD_PreviewCUR2.Width
            CMD_PreviewCursorInner.Height = 1
            CMD_PreviewCursorInner.BackColor = CMD_PreviewCUR.BackColor
            CMD_PreviewCursorInner.Top = 1
            CMD_PreviewCursorInner.Left = 0
            CMD_PreviewCUR2.Width = 8
            CMD_PreviewCUR2.Height = 3
            CMD_PreviewCUR2.Top = CMD_PreviewCUR.Height - CMD_PreviewCUR2.Height - 2
        End If
    End Sub
    Sub UpdateCurPreview()
        Dim all As Integer = CMD_PreviewCUR.Height - 4
        CMD_PreviewCUR2.Height = all * (CMD_CursorSizeBar.Value / CMD_CursorSizeBar.Maximum)
        CMD_PreviewCUR2.Top = CMD_PreviewCUR.Height - CMD_PreviewCUR2.Height - 2
        CMD_PreviewCUR_Val.Text = CMD_CursorSizeBar.Value
        ApplyCursorShape()
    End Sub
    Sub ApplyPreview()
        XenonCMD1.CMD_ColorTable00 = ColorTable00.BackColor
        XenonCMD1.CMD_ColorTable01 = ColorTable01.BackColor
        XenonCMD1.CMD_ColorTable02 = ColorTable02.BackColor
        XenonCMD1.CMD_ColorTable03 = ColorTable03.BackColor
        XenonCMD1.CMD_ColorTable04 = ColorTable04.BackColor
        XenonCMD1.CMD_ColorTable05 = ColorTable05.BackColor
        XenonCMD1.CMD_ColorTable06 = ColorTable06.BackColor
        XenonCMD1.CMD_ColorTable07 = ColorTable07.BackColor
        XenonCMD1.CMD_ColorTable08 = ColorTable08.BackColor
        XenonCMD1.CMD_ColorTable09 = ColorTable09.BackColor
        XenonCMD1.CMD_ColorTable10 = ColorTable10.BackColor
        XenonCMD1.CMD_ColorTable11 = ColorTable11.BackColor
        XenonCMD1.CMD_ColorTable12 = ColorTable12.BackColor
        XenonCMD1.CMD_ColorTable13 = ColorTable13.BackColor
        XenonCMD1.CMD_ColorTable14 = ColorTable14.BackColor
        XenonCMD1.CMD_ColorTable15 = ColorTable15.BackColor
        XenonCMD1.CMD_PopupForeground = CMD_PopupForegroundBar.Value
        XenonCMD1.CMD_PopupBackground = CMD_PopupBackgroundBar.Value
        XenonCMD1.CMD_ScreenColorsForeground = CMD_AccentForegroundBar.Value
        XenonCMD1.CMD_ScreenColorsBackground = CMD_AccentBackgroundBar.Value
        XenonCMD1.Font = New Font(F_cmd.Name, F_cmd.Size, F_cmd.Style)
        XenonCMD1.PowerShell = (_Edition = Edition.PowerShellx64) Or (_Edition = Edition.PowerShellx86)
        XenonCMD1.Raster = CMD_RasterToggle.Checked
        Select Case RasterList.SelectedItem
            Case "4x6"
                XenonCMD1.RasterSize = XenonCMD.Raster_Sizes._4x6

            Case "6x8"
                XenonCMD1.RasterSize = XenonCMD.Raster_Sizes._6x8

            Case "8x8"
                XenonCMD1.RasterSize = XenonCMD.Raster_Sizes._8x8

            Case "16x8"
                XenonCMD1.RasterSize = XenonCMD.Raster_Sizes._16x8

            Case "5x12"
                XenonCMD1.RasterSize = XenonCMD.Raster_Sizes._5x12

            Case "7x12"
                XenonCMD1.RasterSize = XenonCMD.Raster_Sizes._7x12

            Case "8x12"
                XenonCMD1.RasterSize = XenonCMD.Raster_Sizes._8x12

            Case "16x12"
                XenonCMD1.RasterSize = XenonCMD.Raster_Sizes._16x12

            Case "12x16"
                XenonCMD1.RasterSize = XenonCMD.Raster_Sizes._12x16

            Case "10x18"
                XenonCMD1.RasterSize = XenonCMD.Raster_Sizes._10x18

            Case Else
                XenonCMD1.RasterSize = XenonCMD.Raster_Sizes._8x12

        End Select

        XenonCMD1.Refresh()
    End Sub

    Sub UpdateFromTrack(i As Integer)
        If i = 1 Then
            Select Case CMD_PopupForegroundBar.Value
                Case 0
                    CMD_PopupForegroundLbl.BackColor = ColorTable00.BackColor
                Case 1
                    CMD_PopupForegroundLbl.BackColor = ColorTable01.BackColor
                Case 2
                    CMD_PopupForegroundLbl.BackColor = ColorTable02.BackColor
                Case 3
                    CMD_PopupForegroundLbl.BackColor = ColorTable03.BackColor
                Case 4
                    CMD_PopupForegroundLbl.BackColor = ColorTable04.BackColor
                Case 5
                    CMD_PopupForegroundLbl.BackColor = ColorTable05.BackColor
                Case 6
                    CMD_PopupForegroundLbl.BackColor = ColorTable06.BackColor
                Case 7
                    CMD_PopupForegroundLbl.BackColor = ColorTable07.BackColor
                Case 8
                    CMD_PopupForegroundLbl.BackColor = ColorTable08.BackColor
                Case 9
                    CMD_PopupForegroundLbl.BackColor = ColorTable09.BackColor
                Case 10
                    CMD_PopupForegroundLbl.BackColor = ColorTable10.BackColor
                Case 11
                    CMD_PopupForegroundLbl.BackColor = ColorTable11.BackColor
                Case 12
                    CMD_PopupForegroundLbl.BackColor = ColorTable12.BackColor
                Case 13
                    CMD_PopupForegroundLbl.BackColor = ColorTable13.BackColor
                Case 14
                    CMD_PopupForegroundLbl.BackColor = ColorTable14.BackColor
                Case 15
                    CMD_PopupForegroundLbl.BackColor = ColorTable15.BackColor
            End Select

            Dim FC0 As Color = If(CMD_PopupForegroundLbl.BackColor.IsDark, CMD_PopupForegroundLbl.BackColor.LightLight, CMD_PopupForegroundLbl.BackColor.Dark(0.9))
            CMD_PopupForegroundLbl.ForeColor = FC0

            'CMD_PopupForegroundBar.AccentColor = CMD_PopupForegroundLbl.BackColor
            CMD_PopupForegroundBar.Invalidate()
        ElseIf i = 2 Then

            Select Case CMD_PopupBackgroundBar.Value
                Case 0
                    CMD_PopupBackgroundLbl.BackColor = ColorTable00.BackColor
                Case 1
                    CMD_PopupBackgroundLbl.BackColor = ColorTable01.BackColor
                Case 2
                    CMD_PopupBackgroundLbl.BackColor = ColorTable02.BackColor
                Case 3
                    CMD_PopupBackgroundLbl.BackColor = ColorTable03.BackColor
                Case 4
                    CMD_PopupBackgroundLbl.BackColor = ColorTable04.BackColor
                Case 5
                    CMD_PopupBackgroundLbl.BackColor = ColorTable05.BackColor
                Case 6
                    CMD_PopupBackgroundLbl.BackColor = ColorTable06.BackColor
                Case 7
                    CMD_PopupBackgroundLbl.BackColor = ColorTable07.BackColor
                Case 8
                    CMD_PopupBackgroundLbl.BackColor = ColorTable08.BackColor
                Case 9
                    CMD_PopupBackgroundLbl.BackColor = ColorTable09.BackColor
                Case 10
                    CMD_PopupBackgroundLbl.BackColor = ColorTable10.BackColor
                Case 11
                    CMD_PopupBackgroundLbl.BackColor = ColorTable11.BackColor
                Case 12
                    CMD_PopupBackgroundLbl.BackColor = ColorTable12.BackColor
                Case 13
                    CMD_PopupBackgroundLbl.BackColor = ColorTable13.BackColor
                Case 14
                    CMD_PopupBackgroundLbl.BackColor = ColorTable14.BackColor
                Case 15
                    CMD_PopupBackgroundLbl.BackColor = ColorTable15.BackColor
            End Select

            Dim FC0 As Color = If(CMD_PopupBackgroundLbl.BackColor.IsDark, CMD_PopupBackgroundLbl.BackColor.LightLight, CMD_PopupBackgroundLbl.BackColor.Dark(0.9))
            CMD_PopupBackgroundLbl.ForeColor = FC0
            'CMD_PopupBackgroundBar.AccentColor = CMD_PopupBackgroundLbl.BackColor
            CMD_PopupBackgroundBar.Invalidate()

        ElseIf i = 3 Then

            Select Case CMD_AccentBackgroundBar.Value
                Case 0
                    CMD_AccentBackgroundLbl.BackColor = ColorTable00.BackColor
                Case 1
                    CMD_AccentBackgroundLbl.BackColor = ColorTable01.BackColor
                Case 2
                    CMD_AccentBackgroundLbl.BackColor = ColorTable02.BackColor
                Case 3
                    CMD_AccentBackgroundLbl.BackColor = ColorTable03.BackColor
                Case 4
                    CMD_AccentBackgroundLbl.BackColor = ColorTable04.BackColor
                Case 5
                    CMD_AccentBackgroundLbl.BackColor = ColorTable05.BackColor
                Case 6
                    CMD_AccentBackgroundLbl.BackColor = ColorTable06.BackColor
                Case 7
                    CMD_AccentBackgroundLbl.BackColor = ColorTable07.BackColor
                Case 8
                    CMD_AccentBackgroundLbl.BackColor = ColorTable08.BackColor
                Case 9
                    CMD_AccentBackgroundLbl.BackColor = ColorTable09.BackColor
                Case 10
                    CMD_AccentBackgroundLbl.BackColor = ColorTable10.BackColor
                Case 11
                    CMD_AccentBackgroundLbl.BackColor = ColorTable11.BackColor
                Case 12
                    CMD_AccentBackgroundLbl.BackColor = ColorTable12.BackColor
                Case 13
                    CMD_AccentBackgroundLbl.BackColor = ColorTable13.BackColor
                Case 14
                    CMD_AccentBackgroundLbl.BackColor = ColorTable14.BackColor
                Case 15
                    CMD_AccentBackgroundLbl.BackColor = ColorTable15.BackColor
            End Select

            Dim FC0 As Color = If(CMD_AccentBackgroundLbl.BackColor.IsDark, CMD_AccentBackgroundLbl.BackColor.LightLight, CMD_AccentBackgroundLbl.BackColor.Dark(0.9))
            CMD_AccentBackgroundLbl.ForeColor = FC0
            'CMD_AccentBackgroundBar.AccentColor = CMD_AccentBackgroundLbl.BackColor
            CMD_AccentBackgroundBar.Invalidate()
            CMD_PreviewCUR.BackColor = CMD_AccentBackgroundLbl.BackColor

        ElseIf i = 4 Then

            Select Case CMD_AccentForegroundBar.Value
                Case 0
                    If CMD_AccentBackgroundBar.Value = CMD_AccentForegroundBar.Value Then
                        CMD_AccentForegroundLbl.BackColor = ColorTable07.BackColor
                    Else
                        CMD_AccentForegroundLbl.BackColor = ColorTable00.BackColor
                    End If
                Case 1
                    CMD_AccentForegroundLbl.BackColor = ColorTable01.BackColor
                Case 2
                    CMD_AccentForegroundLbl.BackColor = ColorTable02.BackColor
                Case 3
                    CMD_AccentForegroundLbl.BackColor = ColorTable03.BackColor
                Case 4
                    CMD_AccentForegroundLbl.BackColor = ColorTable04.BackColor
                Case 5
                    CMD_AccentForegroundLbl.BackColor = ColorTable05.BackColor
                Case 6
                    CMD_AccentForegroundLbl.BackColor = ColorTable06.BackColor
                Case 7
                    CMD_AccentForegroundLbl.BackColor = ColorTable07.BackColor
                Case 8
                    CMD_AccentForegroundLbl.BackColor = ColorTable08.BackColor
                Case 9
                    CMD_AccentForegroundLbl.BackColor = ColorTable09.BackColor
                Case 10
                    CMD_AccentForegroundLbl.BackColor = ColorTable10.BackColor
                Case 11
                    CMD_AccentForegroundLbl.BackColor = ColorTable11.BackColor
                Case 12
                    CMD_AccentForegroundLbl.BackColor = ColorTable12.BackColor
                Case 13
                    CMD_AccentForegroundLbl.BackColor = ColorTable13.BackColor
                Case 14
                    CMD_AccentForegroundLbl.BackColor = ColorTable14.BackColor
                Case 15
                    CMD_AccentForegroundLbl.BackColor = ColorTable15.BackColor
            End Select

            Dim FC0 As Color = If(CMD_AccentForegroundLbl.BackColor.IsDark, CMD_AccentForegroundLbl.BackColor.LightLight, CMD_AccentForegroundLbl.BackColor.Dark(0.9))
            CMD_AccentForegroundLbl.ForeColor = FC0
            'CMD_AccentForegroundBar.AccentColor = CMD_AccentForegroundLbl.BackColor
            CMD_AccentForegroundBar.Invalidate()

        End If
    End Sub
#End Region

#Region "   CP Handling"
    Sub ApplyFromCP([CP] As CP, [Edition] As Edition)

        Dim [Console] As New CP.Structures.Console

        Select Case [Edition]
            Case Edition.CMD
                SetFromCP([CP].CommandPrompt)

            Case Edition.PowerShellx86
                SetFromCP([CP].PowerShellx86)

            Case Edition.PowerShellx64
                SetFromCP([CP].PowerShellx64)

        End Select

    End Sub

    Sub SetFromCP([Console] As CP.Structures.Console)
        CMDEnabled.Checked = [Console].Enabled

        ColorTable00.BackColor = [Console].ColorTable00
        ColorTable01.BackColor = [Console].ColorTable01
        ColorTable02.BackColor = [Console].ColorTable02
        ColorTable03.BackColor = [Console].ColorTable03
        ColorTable04.BackColor = [Console].ColorTable04
        ColorTable05.BackColor = [Console].ColorTable05
        ColorTable06.BackColor = [Console].ColorTable06
        ColorTable07.BackColor = [Console].ColorTable07
        ColorTable08.BackColor = [Console].ColorTable08
        ColorTable09.BackColor = [Console].ColorTable09
        ColorTable10.BackColor = [Console].ColorTable10
        ColorTable11.BackColor = [Console].ColorTable11
        ColorTable12.BackColor = [Console].ColorTable12
        ColorTable13.BackColor = [Console].ColorTable13
        ColorTable14.BackColor = [Console].ColorTable14
        ColorTable15.BackColor = [Console].ColorTable15

        ColorTable05.DefaultColor = Color.FromArgb(136, 23, 152)
        ColorTable06.DefaultColor = Color.FromArgb(193, 156, 0)

        CMD_PopupForegroundBar.Value = [Console].PopupForeground
        CMD_PopupBackgroundBar.Value = [Console].PopupBackground
        CMD_AccentForegroundBar.Value = [Console].ScreenColorsForeground
        CMD_AccentBackgroundBar.Value = [Console].ScreenColorsBackground
        CMD_RasterToggle.Checked = [Console].FontRaster
        RasterList.Visible = [Console].FontRaster

        Select Case [Console].FontWeight
            Case 0
                CMD_FontWeightBox.SelectedIndex = 0

            Case 100
                CMD_FontWeightBox.SelectedIndex = 1

            Case 200
                CMD_FontWeightBox.SelectedIndex = 2

            Case 300
                CMD_FontWeightBox.SelectedIndex = 3

            Case 400
                CMD_FontWeightBox.SelectedIndex = 4

            Case 500
                CMD_FontWeightBox.SelectedIndex = 5

            Case 600
                CMD_FontWeightBox.SelectedIndex = 6

            Case 700
                CMD_FontWeightBox.SelectedIndex = 7

            Case 800
                CMD_FontWeightBox.SelectedIndex = 8

            Case 900
                CMD_FontWeightBox.SelectedIndex = 9

            Case Else
                CMD_FontWeightBox.SelectedIndex = 4

        End Select

        If Not [Console].FontRaster Then
            With Font.FromLogFont(New LogFont With {.lfFaceName = [Console].FaceName, .lfWeight = [Console].FontWeight}) : F_cmd = New Font(.FontFamily, CInt([Console].FontSize / 65536), .Style) : End With
        End If

        CMD_FontsBox.SelectedItem = F_cmd.Name
        CMD_FontSizeBar.Value = F_cmd.Size
        CMD_FontSizeVal.Text = F_cmd.Size

        If [Console].FontSize = 393220 Then RasterList.SelectedItem = "4x6"
        If [Console].FontSize = 524294 Then RasterList.SelectedItem = "6x8"
        If [Console].FontSize = 524296 Then RasterList.SelectedItem = "8x8"
        If [Console].FontSize = 524304 Then RasterList.SelectedItem = "16x8"
        If [Console].FontSize = 786437 Then RasterList.SelectedItem = "5x12"
        If [Console].FontSize = 786439 Then RasterList.SelectedItem = "7x12"
        If [Console].FontSize = 0 Then RasterList.SelectedItem = "8x12"
        If [Console].FontSize = 786448 Then RasterList.SelectedItem = "16x12"
        If [Console].FontSize = 1048588 Then RasterList.SelectedItem = "12x16"
        If [Console].FontSize = 1179658 Then RasterList.SelectedItem = "10x18"
        If RasterList.SelectedItem = Nothing Then RasterList.SelectedItem = "8x12"


        [Console].CursorSize = CMD_CursorSizeBar.Value
        If CMD_CursorSizeBar.Value > 100 Then CMD_CursorSizeBar.Value = 100
        If CMD_CursorSizeBar.Value < 20 Then CMD_CursorSizeBar.Value = 20

        CMD_CursorStyle.SelectedIndex = [Console].W10_1909_CursorType
        CMD_CursorColor.BackColor = [Console].W10_1909_CursorColor
        CMD_PreviewCUR2.BackColor = [Console].W10_1909_CursorColor
        CMD_EnhancedTerminal.Checked = [Console].W10_1909_ForceV2
        CMD_OpacityBar.Value = [Console].W10_1909_WindowAlpha
        CMD_OpacityVal.Text = Fix(([Console].W10_1909_WindowAlpha / 255) * 100)
        CMD_LineSelection.Checked = [Console].W10_1909_LineSelection
        CMD_TerminalScrolling.Checked = [Console].W10_1909_TerminalScrolling
        ApplyCursorShape()

        UpdateCurPreview()

    End Sub

    Sub ApplyToCP([CP] As CP, [Edition] As Edition)
        Dim [Console] As New CP.Structures.Console With {
            .Enabled = CMDEnabled.Checked,
            .ColorTable00 = ColorTable00.BackColor,
            .ColorTable01 = ColorTable01.BackColor,
            .ColorTable02 = ColorTable02.BackColor,
            .ColorTable03 = ColorTable03.BackColor,
            .ColorTable04 = ColorTable04.BackColor,
            .ColorTable05 = ColorTable05.BackColor,
            .ColorTable06 = ColorTable06.BackColor,
            .ColorTable07 = ColorTable07.BackColor,
            .ColorTable08 = ColorTable08.BackColor,
            .ColorTable09 = ColorTable09.BackColor,
            .ColorTable10 = ColorTable10.BackColor,
            .ColorTable11 = ColorTable11.BackColor,
            .ColorTable12 = ColorTable12.BackColor,
            .ColorTable13 = ColorTable13.BackColor,
            .ColorTable14 = ColorTable14.BackColor,
            .ColorTable15 = ColorTable15.BackColor,
            .PopupForeground = CMD_PopupForegroundBar.Value,
            .PopupBackground = CMD_PopupBackgroundBar.Value,
            .ScreenColorsForeground = CMD_AccentForegroundBar.Value,
            .ScreenColorsBackground = CMD_AccentBackgroundBar.Value,
            .FaceName = F_cmd.Name,
            .FontRaster = CMD_RasterToggle.Checked,
            .FontWeight = CMD_FontWeightBox.SelectedIndex * 100
        }

        If Not CMD_RasterToggle.Checked Then
            [Console].FontSize = CMD_FontSizeBar.Value * 65536
        Else
            Select Case RasterList.SelectedItem
                Case "4x6"
                    [Console].FontSize = 393220

                Case "6x8"
                    [Console].FontSize = 524294

                Case "8x8"
                    [Console].FontSize = 524296

                Case "16x8"
                    [Console].FontSize = 524304

                Case "5x12"
                    [Console].FontSize = 786437

                Case "7x12"
                    [Console].FontSize = 786439

                Case "8x12"
                    [Console].FontSize = 0

                Case "16x12"
                    [Console].FontSize = 786448

                Case "12x16"
                    [Console].FontSize = 1048588

                Case "10x18"
                    [Console].FontSize = 1179658

                Case Else
                    [Console].FontSize = 0

            End Select
        End If

        If CMD_CursorSizeBar.Value < 20 Then
            [Console].CursorSize = 20
        ElseIf CMD_CursorSizeBar.Value > 100 Then
            [Console].CursorSize = 100
        Else
            [Console].CursorSize = CMD_CursorSizeBar.Value
        End If

        [Console].W10_1909_CursorColor = CMD_CursorColor.BackColor
        [Console].W10_1909_CursorType = CMD_CursorStyle.SelectedIndex
        [Console].W10_1909_ForceV2 = CMD_EnhancedTerminal.Checked
        [Console].W10_1909_WindowAlpha = CMD_OpacityBar.Value
        [Console].W10_1909_LineSelection = CMD_LineSelection.Checked
        [Console].W10_1909_TerminalScrolling = CMD_TerminalScrolling.Checked

        Select Case [Edition]
            Case Edition.CMD
                [CP].CommandPrompt = [Console]

            Case Edition.PowerShellx86
                [CP].PowerShellx86 = [Console]

            Case Edition.PowerShellx64
                [CP].PowerShellx64 = [Console]

        End Select

    End Sub
#End Region

    Private Sub CommandPrompt_PopupForegroundBar_Scroll(sender As Object) Handles CMD_PopupForegroundBar.Scroll
        With CMD_PopupForegroundBar
            CMD_PopupForegroundLbl.Text = .Value
            If .Value = 10 Then CMD_PopupForegroundLbl.Text &= " (A)"
            If .Value = 11 Then CMD_PopupForegroundLbl.Text &= " (B)"
            If .Value = 12 Then CMD_PopupForegroundLbl.Text &= " (C)"
            If .Value = 13 Then CMD_PopupForegroundLbl.Text &= " (D)"
            If .Value = 14 Then CMD_PopupForegroundLbl.Text &= " (E)"
            If .Value = 15 Then CMD_PopupForegroundLbl.Text &= " (F)"
        End With

        UpdateFromTrack(1)
        ApplyPreview()
    End Sub

    Private Sub CMD_PopupBackgroundBar_Scroll(sender As Object) Handles CMD_PopupBackgroundBar.Scroll
        With CMD_PopupBackgroundBar
            CMD_PopupBackgroundLbl.Text = .Value
            If .Value = 10 Then CMD_PopupBackgroundLbl.Text &= " (A)"
            If .Value = 11 Then CMD_PopupBackgroundLbl.Text &= " (B)"
            If .Value = 12 Then CMD_PopupBackgroundLbl.Text &= " (C)"
            If .Value = 13 Then CMD_PopupBackgroundLbl.Text &= " (D)"
            If .Value = 14 Then CMD_PopupBackgroundLbl.Text &= " (E)"
            If .Value = 15 Then CMD_PopupBackgroundLbl.Text &= " (F)"
        End With

        UpdateFromTrack(2)
        ApplyPreview()
    End Sub

    Private Sub CMD_AccentForegroundBar_Scroll(sender As Object) Handles CMD_AccentForegroundBar.Scroll
        With CMD_AccentForegroundBar
            CMD_AccentForegroundLbl.Text = .Value
            If .Value = 10 Then CMD_AccentForegroundLbl.Text &= " (A)"
            If .Value = 11 Then CMD_AccentForegroundLbl.Text &= " (B)"
            If .Value = 12 Then CMD_AccentForegroundLbl.Text &= " (C)"
            If .Value = 13 Then CMD_AccentForegroundLbl.Text &= " (D)"
            If .Value = 14 Then CMD_AccentForegroundLbl.Text &= " (E)"
            If .Value = 15 Then CMD_AccentForegroundLbl.Text &= " (F)"
        End With

        UpdateFromTrack(3) : UpdateFromTrack(4)
        ApplyPreview()
    End Sub

    Private Sub CMD_AccentBackgroundBar_Scroll(sender As Object) Handles CMD_AccentBackgroundBar.Scroll
        With CMD_AccentBackgroundBar
            CMD_AccentBackgroundLbl.Text = .Value
            If .Value = 10 Then CMD_AccentBackgroundLbl.Text &= " (A)"
            If .Value = 11 Then CMD_AccentBackgroundLbl.Text &= " (B)"
            If .Value = 12 Then CMD_AccentBackgroundLbl.Text &= " (C)"
            If .Value = 13 Then CMD_AccentBackgroundLbl.Text &= " (D)"
            If .Value = 14 Then CMD_AccentBackgroundLbl.Text &= " (E)"
            If .Value = 15 Then CMD_AccentBackgroundLbl.Text &= " (F)"
        End With

        UpdateFromTrack(3) : UpdateFromTrack(4)
        ApplyPreview()
    End Sub

    Private Sub XenonButton4_Click(sender As Object, e As EventArgs) Handles XenonButton4.Click

        Kernel32.Wow64DisableWow64FsRedirection(IntPtr.Zero)

        Select Case _Edition
            Case Edition.CMD
                Dim prc As New Process With {.StartInfo = New ProcessStartInfo With {
                .FileName = "cmd.exe",
                .Verb = "runas",
                .WorkingDirectory = My.PATH_UserProfile
                }}
                prc.Start()

            Case Edition.PowerShellx86
                Dim prc As New Process With {.StartInfo = New ProcessStartInfo With {
                .FileName = Environment.GetEnvironmentVariable("WINDIR") & "\System32\WindowsPowerShell\v1.0\powershell.exe",
                .Verb = "runas",
                .WorkingDirectory = My.PATH_UserProfile
                }}
                prc.Start()

            Case Edition.PowerShellx64
                Dim prc As New Process With {.StartInfo = New ProcessStartInfo With {
                .FileName = Environment.GetEnvironmentVariable("WINDIR") & "\SysWOW64\WindowsPowerShell\v1.0\powershell.exe",
                .Verb = "runas",
                .WorkingDirectory = My.PATH_UserProfile
                }}
                prc.Start()

        End Select

        Kernel32.Wow64RevertWow64FsRedirection(IntPtr.Zero)
    End Sub

    Private Sub CMD_RasterToggle_CheckedChanged(sender As Object, e As EventArgs) Handles CMD_RasterToggle.CheckedChanged
        If _Shown Then
            RasterList.Visible = CMD_RasterToggle.Checked
            ApplyPreview()
        End If
    End Sub

    Private Sub CMD_FontWeightBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CMD_FontWeightBox.SelectedIndexChanged
        If Not _Shown Then Exit Sub
        Dim fx As New LogFont
        F_cmd = New Font(CMD_FontsBox.SelectedItem.ToString, F_cmd.Size, F_cmd.Style)
        F_cmd.ToLogFont(fx)
        fx.lfWeight = CMD_FontWeightBox.SelectedIndex * 100
        With Font.FromLogFont(fx) : F_cmd = New Font(.Name, F_cmd.Size, .Style) : End With
        CMD_FontsBox.SelectedItem = F_cmd.Name
        ApplyPreview()
    End Sub

    Private Sub CMD_FontsBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CMD_FontsBox.SelectedIndexChanged
        If _Shown Then
            F_cmd = New Font(CMD_FontsBox.SelectedItem.ToString, F_cmd.Size, F_cmd.Style)
            ApplyPreview()
        End If

    End Sub

    Private Sub CMD_FontSizeBar_Scroll(sender As Object) Handles CMD_FontSizeBar.Scroll
        CMD_FontSizeVal.Text = CMD_FontSizeBar.Value
        If _Shown Then
            F_cmd = New Font(F_cmd.Name, CMD_FontSizeBar.Value, F_cmd.Style)
            ApplyPreview()
        End If
    End Sub

    Private Sub RasterList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles RasterList.SelectedIndexChanged
        If _Shown Then
            ApplyPreview()
        End If
    End Sub

    Private Sub CMD_CursorSizeBar_Scroll(sender As Object) Handles CMD_CursorSizeBar.Scroll
        UpdateCurPreview()
    End Sub

    Private Sub CMD_CursorStyle_SelectedIndexChanged_1(sender As Object, e As EventArgs) Handles CMD_CursorStyle.SelectedIndexChanged
        ApplyCursorShape()
    End Sub

    Private Sub CMD_CursorColor_Click(sender As Object, e As EventArgs) Handles CMD_CursorColor.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            CMD_PreviewCUR2.BackColor = SubMenu.ShowMenu(sender)
            Exit Sub
        End If

        Dim CList As New List(Of Control) From {sender, CMD_PreviewCUR2}

        Dim C As Color = ColorPickerDlg.Pick(CList)

        CMD_PreviewCUR2.BackColor = C
        sender.backcolor = C
        sender.invalidate

        CList.Clear()
    End Sub

    Private Sub CMD_OpacityBar_Scroll(sender As Object) Handles CMD_OpacityBar.Scroll
        CMD_OpacityVal.Text = Fix((sender.Value / 255) * 100)
    End Sub


    Private Sub ColorTable00_Click(sender As Object, e As EventArgs) Handles ColorTable00.Click, ColorTable01.Click, ColorTable02.Click, ColorTable03.Click, ColorTable04.Click, ColorTable05.Click,
                                                                             ColorTable06.Click, ColorTable07.Click, ColorTable08.Click, ColorTable09.Click, ColorTable10.Click, ColorTable11.Click,
                                                                             ColorTable12.Click, ColorTable13.Click, ColorTable14.Click, ColorTable15.Click

        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            ApplyPreview()
            UpdateFromTrack(1) : UpdateFromTrack(2) : UpdateFromTrack(3) : UpdateFromTrack(4)
            Exit Sub
        End If

        Dim CList As New List(Of Control) From {sender, XenonCMD1}

        Dim _Conditions As New Conditions
        If sender.Name.ToString.ToLower.Contains("ColorTable00".ToLower) Then _Conditions.CMD_ColorTable00 = True
        If sender.Name.ToString.ToLower.Contains("ColorTable01".ToLower) Then _Conditions.CMD_ColorTable01 = True
        If sender.Name.ToString.ToLower.Contains("ColorTable02".ToLower) Then _Conditions.CMD_ColorTable02 = True
        If sender.Name.ToString.ToLower.Contains("ColorTable03".ToLower) Then _Conditions.CMD_ColorTable03 = True
        If sender.Name.ToString.ToLower.Contains("ColorTable04".ToLower) Then _Conditions.CMD_ColorTable04 = True
        If sender.Name.ToString.ToLower.Contains("ColorTable05".ToLower) Then _Conditions.CMD_ColorTable05 = True
        If sender.Name.ToString.ToLower.Contains("ColorTable06".ToLower) Then _Conditions.CMD_ColorTable06 = True
        If sender.Name.ToString.ToLower.Contains("ColorTable07".ToLower) Then _Conditions.CMD_ColorTable07 = True
        If sender.Name.ToString.ToLower.Contains("ColorTable08".ToLower) Then _Conditions.CMD_ColorTable08 = True
        If sender.Name.ToString.ToLower.Contains("ColorTable09".ToLower) Then _Conditions.CMD_ColorTable09 = True
        If sender.Name.ToString.ToLower.Contains("ColorTable10".ToLower) Then _Conditions.CMD_ColorTable10 = True
        If sender.Name.ToString.ToLower.Contains("ColorTable11".ToLower) Then _Conditions.CMD_ColorTable11 = True
        If sender.Name.ToString.ToLower.Contains("ColorTable12".ToLower) Then _Conditions.CMD_ColorTable12 = True
        If sender.Name.ToString.ToLower.Contains("ColorTable13".ToLower) Then _Conditions.CMD_ColorTable13 = True
        If sender.Name.ToString.ToLower.Contains("ColorTable14".ToLower) Then _Conditions.CMD_ColorTable14 = True
        If sender.Name.ToString.ToLower.Contains("ColorTable15".ToLower) Then _Conditions.CMD_ColorTable15 = True


        Dim C As Color = ColorPickerDlg.Pick(CList, _Conditions)

        sender.backcolor = C
        sender.invalidate
        ApplyPreview()

        UpdateFromTrack(1) : UpdateFromTrack(2) : UpdateFromTrack(3) : UpdateFromTrack(4)

        CList.Clear()
    End Sub

    Private Sub XenonButton6_Click(sender As Object, e As EventArgs) Handles XenonButton6.Click
        Dim _Def As CP
        If MainFrm.PreviewConfig = MainFrm.WinVer.W11 Then
            _Def = New CP_Defaults().Default_Windows11
        ElseIf MainFrm.PreviewConfig = MainFrm.WinVer.W10 Then
            _Def = New CP_Defaults().Default_Windows10
        ElseIf MainFrm.PreviewConfig = MainFrm.WinVer.W8 Then
            _Def = New CP_Defaults().Default_Windows8
        ElseIf MainFrm.PreviewConfig = MainFrm.WinVer.W7 Then
            _Def = New CP_Defaults().Default_Windows7
        Else
            _Def = New CP_Defaults().Default_Windows11
        End If

        Dim ee As Boolean = CMDEnabled.Checked
        ApplyFromCP(_Def, _Edition)
        ApplyPreview()
        CMDEnabled.Checked = ee
        _Def.Dispose()
    End Sub

    Private Sub XenonButton8_Click(sender As Object, e As EventArgs) Handles XenonButton8.Click
        If OpenWPTHDlg.ShowDialog = DialogResult.OK Then
            Dim CPx As New CP(CP.Mode.File, OpenWPTHDlg.FileName)
            Dim ee As Boolean = CMDEnabled.Checked
            ApplyFromCP(CPx, _Edition)
            ApplyPreview()
            CMDEnabled.Checked = ee
            CPx.Dispose()
        End If
    End Sub

    Private Sub XenonButton25_Click(sender As Object, e As EventArgs) Handles XenonButton25.Click
        MsgBox(My.Lang.CMD_NotAllWeights, MsgBoxStyle.Information)
    End Sub

    Private Sub XenonButton3_Click(sender As Object, e As EventArgs) Handles XenonButton3.Click
        Dim CPx As New CP(CP.Mode.Registry)
        Dim ee As Boolean = CMDEnabled.Checked
        ApplyFromCP(CPx, _Edition)
        ApplyPreview()
        CMDEnabled.Checked = ee
        CPx.Dispose()
    End Sub

    Private Sub CMD_FontSizeVal_Click(sender As Object, e As EventArgs) Handles CMD_FontSizeVal.Click
        Dim ib As New Ookii.Dialogs.WinForms.InputDialog With {
            .MainInstruction = My.Lang.InputValue,
            .Input = sender.text,
            .Content = My.Lang.ItMustBeNumerical,
            .WindowTitle = "WinPaletter"
           }

        If ib.ShowDialog() = DialogResult.OK Then
            Dim response As String = ib.Input : If String.IsNullOrWhiteSpace(response) Then response = sender.Text
            sender.Text = Math.Max(Math.Min(Val(response), CMD_FontSizeBar.Maximum), CMD_FontSizeBar.Minimum) : CMD_FontSizeBar.Value = Val(sender.Text)
        End If

        ib.Dispose()
    End Sub

    Private Sub CMD_PreviewCUR_Val_Click(sender As Object, e As EventArgs) Handles CMD_PreviewCUR_Val.Click
        Dim ib As New Ookii.Dialogs.WinForms.InputDialog With {
            .MainInstruction = My.Lang.InputValue,
            .Input = sender.text,
            .Content = My.Lang.ItMustBeNumerical,
            .WindowTitle = "WinPaletter"
           }

        If ib.ShowDialog() = DialogResult.OK Then
            Dim response As String = ib.Input : If String.IsNullOrWhiteSpace(response) Then response = sender.Text
            sender.Text = Math.Max(Math.Min(Val(response), CMD_CursorSizeBar.Maximum), CMD_CursorSizeBar.Minimum) : CMD_CursorSizeBar.Value = Val(sender.Text)
        End If

        ib.Dispose()
    End Sub

    Private Sub CMD_OpacityVal_Click(sender As Object, e As EventArgs) Handles CMD_OpacityVal.Click
        Dim ib As New Ookii.Dialogs.WinForms.InputDialog With {
            .MainInstruction = My.Lang.InputValue,
            .Input = sender.text,
            .Content = My.Lang.ItMustBeNumerical,
            .WindowTitle = "WinPaletter"
           }

        If ib.ShowDialog() = DialogResult.OK Then
            Dim response As String = ib.Input : If String.IsNullOrWhiteSpace(response) Then response = sender.Text
            sender.Text = Math.Max(Math.Min(Val(response), CMD_OpacityBar.Maximum), CMD_OpacityBar.Minimum) : CMD_OpacityBar.Value = Val(sender.Text)
        End If

        ib.Dispose()
    End Sub
End Class