Imports Microsoft.Win32
Imports WinPaletter.NativeMethods
Imports WinPaletter.XenonCore

Public Class cmd
    Dim f_cmd As Font = New Font("Consolas", 18, FontStyle.Regular)
    Private _Shown As Boolean = False
    Public _Edition As Edition = Edition.CMD

    Enum Edition
        CMD
        PowerShellx86
        PowerShellx64
    End Enum

#Region "   Subs not related to colors and shapes"
    Private Sub cmd_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _Shown = False
        ApplyDarkMode(Me)
        XenonCheckBox1.Checked = My.Application._Settings.Terminal_OtherFonts
        FillFonts(CMD_FontsBox, Not My.Application._Settings.Terminal_OtherFonts)
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
                Text = "Command Prompt"
                Icon = My.Resources.icons8_command_line
                XenonButton4.Text = "Open Command Prompt for testing"

            Case Edition.PowerShellx86
                Text = "PowerShell x86"
                Icon = My.Resources.icons8_PowerShell
                XenonButton4.Text = "Open PowerShell x86 for testing"

            Case Edition.PowerShellx64
                Text = "PowerShell x64"
                Icon = My.Resources.icons8_PowerShell
                XenonButton4.Text = "Open PowerShell x64 for testing"

        End Select

    End Sub
    Private Sub XenonCheckBox1_CheckedChanged(sender As Object) Handles XenonCheckBox1.CheckedChanged
        If _Shown Then
            FillFonts(CMD_FontsBox, Not XenonCheckBox1.Checked)
            My.Application._Settings.Terminal_OtherFonts = XenonCheckBox1.Checked
            My.Application._Settings.Save(XeSettings.Mode.Registry)
        End If
    End Sub

    Sub FillFonts([ListBox] As ComboBox, Optional FixedPitch As Boolean = False)
        Dim s As String = [ListBox].SelectedItem

        [ListBox].Items.Clear()

        If Not FixedPitch Then
            For Each x As String In My.Application.FontsList
                [ListBox].Items.Add(x)
            Next
        Else
            For Each x As String In My.Application.FontsFixedList
                [ListBox].Items.Add(x)
            Next
        End If

        [ListBox].SelectedItem = s

        If [ListBox].SelectedItem = Nothing Then [ListBox].SelectedIndex = 0
    End Sub
    Function FixValue(i As Integer) As Integer
        Dim v As Integer
        v = i
        If v < 12 Then v = 12
        If v > 12 And v < 18 Then v = 12
        If v > 18 And v < 24 Then v = 18
        If v > 24 And v < 30 Then v = 24
        If v > 30 And v < 36 Then v = 30
        If v > 36 And v < 42 Then v = 36
        If v > 42 And v < 48 Then v = 42
        If v > 48 Then v = 48
        Return v
    End Function
    Private Sub XenonButton10_Click(sender As Object, e As EventArgs) Handles XenonButton10.Click

        If CMDEnabled.Checked Then
            Cursor = Cursors.WaitCursor
            Dim CPx As New CP(CP.Mode.Registry)
            ApplyToCP(CPx, _Edition)
            ApplyToCP(MainFrm.CP, _Edition)
            CPx.Save(CP.SavingMode.Registry, "", If(CPx.LogonUI7_Enabled, True, False))
            Cursor = Cursors.Default
        Else
            MsgBox(My.Application.LanguageHelper.CMD_Enable, MsgBoxStyle.Critical + My.Application.MsgboxRt)
        End If

    End Sub
    Private Sub XenonButton1_Click(sender As Object, e As EventArgs) Handles XenonButton1.Click
        ApplyToCP(MainFrm.CP, _Edition)
        Me.Close()
    End Sub
    Private Sub XenonButton2_Click(sender As Object, e As EventArgs) Handles XenonButton2.Click
        Me.Close()
    End Sub
    Private Sub cmd_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        MainFrm.Visible = True
    End Sub
    Private Sub cmd_Shown(sender As Object, e As EventArgs) Handles Me.Shown
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
        CMD_PreviewCUR_LBL.Text = CMD_CursorSizeBar.Value
        If My.W10_1909 Then ApplyCursorShape()
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
        XenonCMD1.Font = New Font(f_cmd.Name, f_cmd.Size, f_cmd.Style)
        XenonCMD1.Raster = CMD_RasterToggle.Checked
        XenonCMD1.Refresh()

        XenonCMD1.PowerShell = (_Edition = Edition.PowerShellx64) Or (_Edition = Edition.PowerShellx86)
    End Sub

    Sub UpdateFromTrack(i As Integer)
        Dim steps As Integer = 15
        Dim delay As Integer = 10

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

            Dim FC0 As Color = If(IsColorDark(CMD_PopupForegroundLbl.BackColor), ControlPaint.LightLight(CMD_PopupForegroundLbl.BackColor), ControlPaint.Dark(CMD_PopupForegroundLbl.BackColor, 0.9))
            CMD_PopupForegroundLbl.ForeColor = FC0

            CMD_PopupForegroundBar.AccentColor = CMD_PopupForegroundLbl.BackColor
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

            Dim FC0 As Color = If(IsColorDark(CMD_PopupBackgroundLbl.BackColor), ControlPaint.LightLight(CMD_PopupBackgroundLbl.BackColor), ControlPaint.Dark(CMD_PopupBackgroundLbl.BackColor, 0.9))
            CMD_PopupBackgroundLbl.ForeColor = FC0
            CMD_PopupBackgroundBar.AccentColor = CMD_PopupBackgroundLbl.BackColor
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

            Dim FC0 As Color = If(IsColorDark(CMD_AccentBackgroundLbl.BackColor), ControlPaint.LightLight(CMD_AccentBackgroundLbl.BackColor), ControlPaint.Dark(CMD_AccentBackgroundLbl.BackColor, 0.9))
            CMD_AccentBackgroundLbl.ForeColor = FC0
            CMD_AccentBackgroundBar.AccentColor = CMD_AccentBackgroundLbl.BackColor
            CMD_AccentBackgroundBar.Invalidate()

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

            Dim FC0 As Color = If(IsColorDark(CMD_AccentForegroundLbl.BackColor), ControlPaint.LightLight(CMD_AccentForegroundLbl.BackColor), ControlPaint.Dark(CMD_AccentForegroundLbl.BackColor, 0.9))
            CMD_AccentForegroundLbl.ForeColor = FC0
            CMD_AccentForegroundBar.AccentColor = CMD_AccentForegroundLbl.BackColor
            CMD_AccentForegroundBar.Invalidate()

        End If
    End Sub
#End Region

#Region "   CP Handling"
    Sub ApplyFromCP([CP] As CP, [Edition] As Edition)

        Select Case [Edition]
            Case Edition.CMD
#Region " Command Prompt"
                CMDEnabled.Checked = CP.Terminal_CMD_Enabled

                ColorTable00.BackColor = CP.CMD_ColorTable00
                ColorTable01.BackColor = CP.CMD_ColorTable01
                ColorTable02.BackColor = CP.CMD_ColorTable02
                ColorTable03.BackColor = CP.CMD_ColorTable03
                ColorTable04.BackColor = CP.CMD_ColorTable04
                ColorTable05.BackColor = CP.CMD_ColorTable05
                ColorTable06.BackColor = CP.CMD_ColorTable06
                ColorTable07.BackColor = CP.CMD_ColorTable07
                ColorTable08.BackColor = CP.CMD_ColorTable08
                ColorTable09.BackColor = CP.CMD_ColorTable09
                ColorTable10.BackColor = CP.CMD_ColorTable10
                ColorTable11.BackColor = CP.CMD_ColorTable11
                ColorTable12.BackColor = CP.CMD_ColorTable12
                ColorTable13.BackColor = CP.CMD_ColorTable13
                ColorTable14.BackColor = CP.CMD_ColorTable14
                ColorTable15.BackColor = CP.CMD_ColorTable15

                ColorTable05.DefaultColor = Color.FromArgb(136, 23, 152)
                ColorTable06.DefaultColor = Color.FromArgb(193, 156, 0)

                CMD_PopupForegroundBar.Value = CP.CMD_PopupForeground
                CMD_PopupBackgroundBar.Value = CP.CMD_PopupBackground
                CMD_AccentForegroundBar.Value = CP.CMD_ScreenColorsForeground
                CMD_AccentBackgroundBar.Value = CP.CMD_ScreenColorsBackground
                CMD_RasterToggle.Checked = CP.CMD_FontRaster
                Select Case CP.CMD_FontWeight
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
                With Font.FromLogFont(New LOGFONT With {.lfFaceName = CP.CMD_FaceName, .lfWeight = CP.CMD_FontWeight}) : f_cmd = New Font(.FontFamily, CInt(CP.CMD_FontSize / 65536), .Style) : End With
                CMD_FontsBox.SelectedItem = f_cmd.Name
                CMD_FontSizeBar.Value = f_cmd.Size
                CP.CMD_CursorSize = CMD_CursorSizeBar.Value
                If CMD_CursorSizeBar.Value > 100 Then CMD_CursorSizeBar.Value = 100
                If CMD_CursorSizeBar.Value < 20 Then CMD_CursorSizeBar.Value = 20
                If My.W10_1909 Then
                    CMD_CursorStyle.SelectedIndex = CP.CMD_1909_CursorType
                    CMD_CursorColor.BackColor = CP.CMD_1909_CursorColor
                    CMD_PreviewCUR2.BackColor = CP.CMD_1909_CursorColor
                    CMD_EnhancedTerminal.Checked = CP.CMD_1909_ForceV2
                    CMD_OpacityBar.Value = CP.CMD_1909_WindowAlpha
                    CMD_OpacityLbl.Text = Fix((CP.CMD_1909_WindowAlpha / 255) * 100)
                    CMD_LineSelection.Checked = CP.CMD_1909_LineSelection
                    CMD_TerminalScrolling.Checked = CP.CMD_1909_TerminalScrolling
                    ApplyCursorShape()
                End If
                UpdateCurPreview()
#End Region
            Case Edition.PowerShellx86
#Region " PowerShell x86"
                CMDEnabled.Checked = CP.Terminal_PS_32_Enabled

                ColorTable00.BackColor = CP.PS_32_ColorTable00
                ColorTable01.BackColor = CP.PS_32_ColorTable01
                ColorTable02.BackColor = CP.PS_32_ColorTable02
                ColorTable03.BackColor = CP.PS_32_ColorTable03
                ColorTable04.BackColor = CP.PS_32_ColorTable04
                ColorTable05.BackColor = CP.PS_32_ColorTable05
                ColorTable06.BackColor = CP.PS_32_ColorTable06
                ColorTable07.BackColor = CP.PS_32_ColorTable07
                ColorTable08.BackColor = CP.PS_32_ColorTable08
                ColorTable09.BackColor = CP.PS_32_ColorTable09
                ColorTable10.BackColor = CP.PS_32_ColorTable10
                ColorTable11.BackColor = CP.PS_32_ColorTable11
                ColorTable12.BackColor = CP.PS_32_ColorTable12
                ColorTable13.BackColor = CP.PS_32_ColorTable13
                ColorTable14.BackColor = CP.PS_32_ColorTable14
                ColorTable15.BackColor = CP.PS_32_ColorTable15

                ColorTable05.DefaultColor = Color.FromArgb(1, 36, 86)
                ColorTable06.DefaultColor = Color.FromArgb(238, 237, 240)

                CMD_PopupForegroundBar.Value = CP.PS_32_PopupForeground
                CMD_PopupBackgroundBar.Value = CP.PS_32_PopupBackground
                CMD_AccentForegroundBar.Value = CP.PS_32_ScreenColorsForeground
                CMD_AccentBackgroundBar.Value = CP.PS_32_ScreenColorsBackground
                CMD_RasterToggle.Checked = CP.PS_32_FontRaster
                Select Case CP.PS_32_FontWeight
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
                With Font.FromLogFont(New LOGFONT With {.lfFaceName = CP.PS_32_FaceName, .lfWeight = CP.PS_32_FontWeight}) : f_cmd = New Font(.FontFamily, CInt(CP.PS_32_FontSize / 65536), .Style) : End With
                CMD_FontsBox.SelectedItem = f_cmd.Name
                CMD_FontSizeBar.Value = f_cmd.Size
                CP.PS_32_CursorSize = CMD_CursorSizeBar.Value
                If CMD_CursorSizeBar.Value > 100 Then CMD_CursorSizeBar.Value = 100
                If CMD_CursorSizeBar.Value < 20 Then CMD_CursorSizeBar.Value = 20
                If My.W10_1909 Then
                    CMD_CursorStyle.SelectedIndex = CP.PS_32_1909_CursorType
                    CMD_CursorColor.BackColor = CP.PS_32_1909_CursorColor
                    CMD_PreviewCUR2.BackColor = CP.PS_32_1909_CursorColor
                    CMD_EnhancedTerminal.Checked = CP.PS_32_1909_ForceV2
                    CMD_OpacityBar.Value = CP.PS_32_1909_WindowAlpha
                    CMD_OpacityLbl.Text = Fix((CP.PS_32_1909_WindowAlpha / 255) * 100)
                    CMD_LineSelection.Checked = CP.PS_32_1909_LineSelection
                    CMD_TerminalScrolling.Checked = CP.PS_32_1909_TerminalScrolling
                    ApplyCursorShape()
                End If
                UpdateCurPreview()
#End Region
            Case Edition.PowerShellx64
#Region " PowerShell x64"
                CMDEnabled.Checked = CP.Terminal_PS_64_Enabled

                ColorTable00.BackColor = CP.PS_64_ColorTable00
                ColorTable01.BackColor = CP.PS_64_ColorTable01
                ColorTable02.BackColor = CP.PS_64_ColorTable02
                ColorTable03.BackColor = CP.PS_64_ColorTable03
                ColorTable04.BackColor = CP.PS_64_ColorTable04
                ColorTable05.BackColor = CP.PS_64_ColorTable05
                ColorTable06.BackColor = CP.PS_64_ColorTable06
                ColorTable07.BackColor = CP.PS_64_ColorTable07
                ColorTable08.BackColor = CP.PS_64_ColorTable08
                ColorTable09.BackColor = CP.PS_64_ColorTable09
                ColorTable10.BackColor = CP.PS_64_ColorTable10
                ColorTable11.BackColor = CP.PS_64_ColorTable11
                ColorTable12.BackColor = CP.PS_64_ColorTable12
                ColorTable13.BackColor = CP.PS_64_ColorTable13
                ColorTable14.BackColor = CP.PS_64_ColorTable14
                ColorTable15.BackColor = CP.PS_64_ColorTable15

                ColorTable05.DefaultColor = Color.FromArgb(1, 36, 86)
                ColorTable06.DefaultColor = Color.FromArgb(238, 237, 240)

                CMD_PopupForegroundBar.Value = CP.PS_64_PopupForeground
                CMD_PopupBackgroundBar.Value = CP.PS_64_PopupBackground
                CMD_AccentForegroundBar.Value = CP.PS_64_ScreenColorsForeground
                CMD_AccentBackgroundBar.Value = CP.PS_64_ScreenColorsBackground
                CMD_RasterToggle.Checked = CP.PS_64_FontRaster
                Select Case CP.PS_64_FontWeight
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
                With Font.FromLogFont(New LOGFONT With {.lfFaceName = CP.PS_64_FaceName, .lfWeight = CP.PS_64_FontWeight}) : f_cmd = New Font(.FontFamily, CInt(CP.PS_64_FontSize / 65536), .Style) : End With
                CMD_FontsBox.SelectedItem = f_cmd.Name
                CMD_FontSizeBar.Value = f_cmd.Size
                CP.PS_64_CursorSize = CMD_CursorSizeBar.Value
                If CMD_CursorSizeBar.Value > 100 Then CMD_CursorSizeBar.Value = 100
                If CMD_CursorSizeBar.Value < 20 Then CMD_CursorSizeBar.Value = 20
                If My.W10_1909 Then
                    CMD_CursorStyle.SelectedIndex = CP.PS_64_1909_CursorType
                    CMD_CursorColor.BackColor = CP.PS_64_1909_CursorColor
                    CMD_PreviewCUR2.BackColor = CP.PS_64_1909_CursorColor
                    CMD_EnhancedTerminal.Checked = CP.PS_64_1909_ForceV2
                    CMD_OpacityBar.Value = CP.PS_64_1909_WindowAlpha
                    CMD_OpacityLbl.Text = Fix((CP.PS_64_1909_WindowAlpha / 255) * 100)
                    CMD_LineSelection.Checked = CP.PS_64_1909_LineSelection
                    CMD_TerminalScrolling.Checked = CP.PS_64_1909_TerminalScrolling
                    ApplyCursorShape()
                End If
                UpdateCurPreview()
#End Region
        End Select

    End Sub

    Sub ApplyToCP([CP] As CP, [Edition] As Edition)

        Select Case [Edition]
            Case Edition.CMD
#Region " Command Prompt"
                CP.Terminal_CMD_Enabled = CMDEnabled.Checked

                CP.CMD_ColorTable00 = ColorTable00.BackColor
                CP.CMD_ColorTable01 = ColorTable01.BackColor
                CP.CMD_ColorTable02 = ColorTable02.BackColor
                CP.CMD_ColorTable03 = ColorTable03.BackColor
                CP.CMD_ColorTable04 = ColorTable04.BackColor
                CP.CMD_ColorTable05 = ColorTable05.BackColor
                CP.CMD_ColorTable06 = ColorTable06.BackColor
                CP.CMD_ColorTable07 = ColorTable07.BackColor
                CP.CMD_ColorTable08 = ColorTable08.BackColor
                CP.CMD_ColorTable09 = ColorTable09.BackColor
                CP.CMD_ColorTable10 = ColorTable10.BackColor
                CP.CMD_ColorTable11 = ColorTable11.BackColor
                CP.CMD_ColorTable12 = ColorTable12.BackColor
                CP.CMD_ColorTable13 = ColorTable13.BackColor
                CP.CMD_ColorTable14 = ColorTable14.BackColor
                CP.CMD_ColorTable15 = ColorTable15.BackColor
                CP.CMD_PopupForeground = CMD_PopupForegroundBar.Value
                CP.CMD_PopupBackground = CMD_PopupBackgroundBar.Value
                CP.CMD_ScreenColorsForeground = CMD_AccentForegroundBar.Value
                CP.CMD_ScreenColorsBackground = CMD_AccentBackgroundBar.Value
                CP.CMD_FaceName = f_cmd.Name
                CP.CMD_FontRaster = CMD_RasterToggle.Checked
                CP.CMD_FontWeight = CMD_FontWeightBox.SelectedIndex * 100
                If Not CMD_RasterToggle.Checked Then
                    CP.CMD_FontSize = CMD_FontSizeBar.Value * 65536
                Else
                    CP.CMD_FontSize = FixValue(CMD_FontSizeBar.Value) * 65536
                End If
                If CMD_CursorSizeBar.Value < 20 Then
                    CP.CMD_CursorSize = 20
                ElseIf CMD_CursorSizeBar.Value > 100 Then
                    CP.CMD_CursorSize = 100
                Else
                    CP.CMD_CursorSize = CMD_CursorSizeBar.Value
                End If

                If My.W10_1909 Then
                    CP.CMD_1909_CursorColor = CMD_CursorColor.BackColor
                    CP.CMD_1909_CursorType = CMD_CursorStyle.SelectedIndex
                    CP.CMD_1909_ForceV2 = CMD_EnhancedTerminal.Checked
                    CP.CMD_1909_WindowAlpha = CMD_OpacityBar.Value
                    CP.CMD_1909_LineSelection = CMD_LineSelection.Checked
                    CP.CMD_1909_TerminalScrolling = CMD_TerminalScrolling.Checked
                End If
#End Region
            Case Edition.PowerShellx86
#Region " PowerShell x86"
                CP.Terminal_PS_32_Enabled = CMDEnabled.Checked

                CP.PS_32_ColorTable00 = ColorTable00.BackColor
                CP.PS_32_ColorTable01 = ColorTable01.BackColor
                CP.PS_32_ColorTable02 = ColorTable02.BackColor
                CP.PS_32_ColorTable03 = ColorTable03.BackColor
                CP.PS_32_ColorTable04 = ColorTable04.BackColor
                CP.PS_32_ColorTable05 = ColorTable05.BackColor
                CP.PS_32_ColorTable06 = ColorTable06.BackColor
                CP.PS_32_ColorTable07 = ColorTable07.BackColor
                CP.PS_32_ColorTable08 = ColorTable08.BackColor
                CP.PS_32_ColorTable09 = ColorTable09.BackColor
                CP.PS_32_ColorTable10 = ColorTable10.BackColor
                CP.PS_32_ColorTable11 = ColorTable11.BackColor
                CP.PS_32_ColorTable12 = ColorTable12.BackColor
                CP.PS_32_ColorTable13 = ColorTable13.BackColor
                CP.PS_32_ColorTable14 = ColorTable14.BackColor
                CP.PS_32_ColorTable15 = ColorTable15.BackColor
                CP.PS_32_PopupForeground = CMD_PopupForegroundBar.Value
                CP.PS_32_PopupBackground = CMD_PopupBackgroundBar.Value
                CP.PS_32_ScreenColorsForeground = CMD_AccentForegroundBar.Value
                CP.PS_32_ScreenColorsBackground = CMD_AccentBackgroundBar.Value
                CP.PS_32_FaceName = f_cmd.Name
                CP.PS_32_FontRaster = CMD_RasterToggle.Checked
                CP.PS_32_FontWeight = CMD_FontWeightBox.SelectedIndex * 100
                If Not CMD_RasterToggle.Checked Then
                    CP.PS_32_FontSize = CMD_FontSizeBar.Value * 65536
                Else
                    CP.PS_32_FontSize = FixValue(CMD_FontSizeBar.Value) * 65536
                End If

                If CMD_CursorSizeBar.Value < 20 Then
                    CP.PS_32_CursorSize = 20
                ElseIf CMD_CursorSizeBar.Value > 100 Then
                    CP.PS_32_CursorSize = 100
                Else
                    CP.PS_32_CursorSize = CMD_CursorSizeBar.Value
                End If

                If My.W10_1909 Then
                    CP.PS_32_1909_CursorColor = CMD_CursorColor.BackColor
                    CP.PS_32_1909_CursorType = CMD_CursorStyle.SelectedIndex
                    CP.PS_32_1909_ForceV2 = CMD_EnhancedTerminal.Checked
                    CP.PS_32_1909_WindowAlpha = CMD_OpacityBar.Value
                    CP.PS_32_1909_LineSelection = CMD_LineSelection.Checked
                    CP.PS_32_1909_TerminalScrolling = CMD_TerminalScrolling.Checked
                End If
#End Region
            Case Edition.PowerShellx64
#Region " PowerShell x64"
                CP.Terminal_PS_64_Enabled = CMDEnabled.Checked

                CP.PS_64_ColorTable00 = ColorTable00.BackColor
                CP.PS_64_ColorTable01 = ColorTable01.BackColor
                CP.PS_64_ColorTable02 = ColorTable02.BackColor
                CP.PS_64_ColorTable03 = ColorTable03.BackColor
                CP.PS_64_ColorTable04 = ColorTable04.BackColor
                CP.PS_64_ColorTable05 = ColorTable05.BackColor
                CP.PS_64_ColorTable06 = ColorTable06.BackColor
                CP.PS_64_ColorTable07 = ColorTable07.BackColor
                CP.PS_64_ColorTable08 = ColorTable08.BackColor
                CP.PS_64_ColorTable09 = ColorTable09.BackColor
                CP.PS_64_ColorTable10 = ColorTable10.BackColor
                CP.PS_64_ColorTable11 = ColorTable11.BackColor
                CP.PS_64_ColorTable12 = ColorTable12.BackColor
                CP.PS_64_ColorTable13 = ColorTable13.BackColor
                CP.PS_64_ColorTable14 = ColorTable14.BackColor
                CP.PS_64_ColorTable15 = ColorTable15.BackColor
                CP.PS_64_PopupForeground = CMD_PopupForegroundBar.Value
                CP.PS_64_PopupBackground = CMD_PopupBackgroundBar.Value
                CP.PS_64_ScreenColorsForeground = CMD_AccentForegroundBar.Value
                CP.PS_64_ScreenColorsBackground = CMD_AccentBackgroundBar.Value
                CP.PS_64_FaceName = f_cmd.Name
                CP.PS_64_FontRaster = CMD_RasterToggle.Checked
                CP.PS_64_FontWeight = CMD_FontWeightBox.SelectedIndex * 100
                If Not CMD_RasterToggle.Checked Then
                    CP.PS_64_FontSize = CMD_FontSizeBar.Value * 65536
                Else
                    CP.PS_64_FontSize = FixValue(CMD_FontSizeBar.Value) * 65536
                End If

                If CMD_CursorSizeBar.Value < 20 Then
                    CP.PS_64_CursorSize = 20
                ElseIf CMD_CursorSizeBar.Value > 100 Then
                    CP.PS_64_CursorSize = 100
                Else
                    CP.PS_64_CursorSize = CMD_CursorSizeBar.Value
                End If

                If My.W10_1909 Then
                    CP.PS_64_1909_CursorColor = CMD_CursorColor.BackColor
                    CP.PS_64_1909_CursorType = CMD_CursorStyle.SelectedIndex
                    CP.PS_64_1909_ForceV2 = CMD_EnhancedTerminal.Checked
                    CP.PS_64_1909_WindowAlpha = CMD_OpacityBar.Value
                    CP.PS_64_1909_LineSelection = CMD_LineSelection.Checked
                    CP.PS_64_1909_TerminalScrolling = CMD_TerminalScrolling.Checked
                End If
#End Region
        End Select



    End Sub
#End Region

    Private Sub CMD_PopupForegroundBar_Scroll(sender As Object) Handles CMD_PopupForegroundBar.Scroll
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
                .WorkingDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)
                }}
                prc.Start()

            Case Edition.PowerShellx86
                Dim prc As New Process With {.StartInfo = New ProcessStartInfo With {
                .FileName = Environment.GetEnvironmentVariable("WINDIR") & "\System32\WindowsPowerShell\v1.0\powershell.exe",
                .Verb = "runas",
                .WorkingDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)
                }}
                prc.Start()

            Case Edition.PowerShellx64
                Dim prc As New Process With {.StartInfo = New ProcessStartInfo With {
                .FileName = Environment.GetEnvironmentVariable("WINDIR") & "\SysWOW64\WindowsPowerShell\v1.0\powershell.exe",
                .Verb = "runas",
                .WorkingDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)
                }}
                prc.Start()

        End Select

        Kernel32.Wow64RevertWow64FsRedirection(IntPtr.Zero)
    End Sub

    Private Sub CMD_RasterToggle_CheckedChanged(sender As Object, e As EventArgs) Handles CMD_RasterToggle.CheckedChanged
        If _Shown Then

            If CMD_RasterToggle.Enabled Then
                XenonCMD1.Font = New Font(f_cmd.Name, 12, f_cmd.Style)
            Else
                XenonCMD1.Font = f_cmd
            End If

            ApplyPreview()
        End If
    End Sub

    Private Sub CMD_FontWeightBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CMD_FontWeightBox.SelectedIndexChanged
        If Not _Shown Then Exit Sub
        Dim fx As New LOGFONT
        f_cmd = New Font(CMD_FontsBox.SelectedItem.ToString, f_cmd.Size, f_cmd.Style)
        f_cmd.ToLogFont(fx)
        fx.lfWeight = CMD_FontWeightBox.SelectedIndex * 100
        With Font.FromLogFont(fx) : f_cmd = New Font(.Name, f_cmd.Size, .Style) : End With
        CMD_FontsBox.SelectedItem = f_cmd.Name
        ApplyPreview()
    End Sub

    Private Sub CMD_FontsBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CMD_FontsBox.SelectedIndexChanged
        If _Shown Then
            f_cmd = New Font(CMD_FontsBox.SelectedItem.ToString, f_cmd.Size, f_cmd.Style)
            ApplyPreview()
        End If

    End Sub

    Private Sub CMD_FontSizeBar_Scroll(sender As Object) Handles CMD_FontSizeBar.Scroll
        CMD_FontSizeLbl.Text = CMD_FontSizeBar.Value
        If _Shown Then
            f_cmd = New Font(f_cmd.Name, CMD_FontSizeBar.Value, f_cmd.Style)
            ApplyPreview()
        End If
    End Sub

    Private Sub CMD_CursorSizeBar_Scroll(sender As Object) Handles CMD_CursorSizeBar.Scroll
        UpdateCurPreview()
    End Sub

    Private Sub CMD_CursorStyle_SelectedIndexChanged_1(sender As Object, e As EventArgs) Handles CMD_CursorStyle.SelectedIndexChanged
        If My.W10_1909 Then
            ApplyCursorShape()
        End If
    End Sub

    Private Sub CMD_CursorColor_Click(sender As Object, e As EventArgs) Handles CMD_CursorColor.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
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
        CMD_OpacityLbl.Text = Fix((sender.Value / 255) * 100)
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

        Dim CList As New List(Of Control) From {sender}

        CList.Add(XenonCMD1)

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
        If MainFrm.PreviewConfig = MainFrm.WinVer.Eleven Then
            _Def = New CP_Defaults().Default_Windows11
        ElseIf MainFrm.PreviewConfig = MainFrm.WinVer.Ten Then
            _Def = New CP_Defaults().Default_Windows10
        ElseIf MainFrm.PreviewConfig = MainFrm.WinVer.Eight Then
            _Def = New CP_Defaults().Default_Windows8
        ElseIf MainFrm.PreviewConfig = MainFrm.WinVer.Seven Then
            _Def = New CP_Defaults().Default_Windows7
        Else
            _Def = New CP_Defaults().Default_Windows11
        End If

        Dim ee As Boolean = CMDEnabled.Checked
        ApplyFromCP(_Def, _Edition)
        ApplyPreview()
        CMDEnabled.Checked = ee

    End Sub

    Private Sub XenonButton8_Click(sender As Object, e As EventArgs) Handles XenonButton8.Click
        If OpenWPTHDlg.ShowDialog = DialogResult.OK Then
            Dim CPx As New CP(CP.Mode.File, OpenWPTHDlg.FileName)
            Dim ee As Boolean = CMDEnabled.Checked
            ApplyFromCP(CPx, _Edition)
            ApplyPreview()
            CMDEnabled.Checked = ee
        End If
    End Sub

End Class