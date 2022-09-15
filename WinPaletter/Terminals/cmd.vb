Imports System.Drawing.Text
Imports WinPaletter.XenonCore

Public Class cmd
    Private Sub cmd_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _Shown = False

        ApplyDarkMode(Me)
        ApplyFromCP(MainFrm.CP)
        ApplyPreview()
        MainFrm.Visible = False
        Label1.Font = My.Application.ConsoleFont
        Label2.Font = My.Application.ConsoleFont
        Label3.Font = My.Application.ConsoleFont
        Label5.Font = My.Application.ConsoleFont
        Label10.Font = My.Application.ConsoleFont
        Label11.Font = My.Application.ConsoleFont
        Label15.Font = My.Application.ConsoleFont
        Label16.Font = My.Application.ConsoleFont
        Label4.Font = My.Application.ConsoleFont
        Label8.Font = My.Application.ConsoleFont
        Label40.Font = My.Application.ConsoleFont
        Label43.Font = My.Application.ConsoleFont

        XenonAlertBox1.Image = My.Resources.notify_warning
        XenonAlertBox1.Visible = Not IO.Directory.Exists(Environment.GetEnvironmentVariable("WINDIR") & "\SysWOW64\WindowsPowerShell\v1.0")
        XenonAlertBox2.Image = My.Resources.notify_warning
        XenonAlertBox2.Visible = Not IO.Directory.Exists(Environment.GetEnvironmentVariable("WINDIR") & "\System32\WindowsPowerShell\v1.0")

        Location = New Point(10, (My.Computer.Screen.Bounds.Height - Height) / 2 - 20)
    End Sub

    Dim f_cmd As Font = New Font("Segoe UI", 8, FontStyle.Regular)
    Private _Shown As Boolean = False

    Sub ApplyFromCP([CP] As CP)
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
        XenonTrackbar1.Value = CP.CMD_PopupForeground
        XenonTrackbar2.Value = CP.CMD_PopupBackground
        XenonTrackbar4.Value = CP.CMD_ScreenColorsForeground
        XenonTrackbar3.Value = CP.CMD_ScreenColorsBackground
        XenonTextBox1.Text = CP.CMD_CursorSize
        XenonToggle1.Checked = CP.CMD_FontRaster
        Select Case CP.CMD_FontWeight
            Case 0
                XenonComboBox1.SelectedIndex = 0

            Case 100
                XenonComboBox1.SelectedIndex = 1

            Case 200
                XenonComboBox1.SelectedIndex = 2

            Case 300
                XenonComboBox1.SelectedIndex = 3

            Case 400
                XenonComboBox1.SelectedIndex = 4

            Case 500
                XenonComboBox1.SelectedIndex = 5

            Case 600
                XenonComboBox1.SelectedIndex = 6

            Case 700
                XenonComboBox1.SelectedIndex = 7

            Case 800
                XenonComboBox1.SelectedIndex = 8

            Case 900
                XenonComboBox1.SelectedIndex = 9

            Case Else
                XenonComboBox1.SelectedIndex = 4

        End Select

        With Font.FromLogFont(New LOGFONT With {.lfFaceName = CP.CMD_FaceName, .lfWeight = CP.CMD_FontWeight}) : f_cmd = New Font(.FontFamily, CInt(CP.CMD_FontSize / 65536), .Style) : End With
        cmd1_fontdlg.Font = f_cmd

        PS_32_ColorTable00.BackColor = CP.PS_32_ColorTable00
        PS_32_ColorTable01.BackColor = CP.PS_32_ColorTable01
        PS_32_ColorTable02.BackColor = CP.PS_32_ColorTable02
        PS_32_ColorTable03.BackColor = CP.PS_32_ColorTable03
        PS_32_ColorTable04.BackColor = CP.PS_32_ColorTable04
        PS_32_ColorTable05.BackColor = CP.PS_32_ColorTable05
        PS_32_ColorTable06.BackColor = CP.PS_32_ColorTable06
        PS_32_ColorTable07.BackColor = CP.PS_32_ColorTable07
        PS_32_ColorTable08.BackColor = CP.PS_32_ColorTable08
        PS_32_ColorTable09.BackColor = CP.PS_32_ColorTable09
        PS_32_ColorTable10.BackColor = CP.PS_32_ColorTable10
        PS_32_ColorTable11.BackColor = CP.PS_32_ColorTable11
        PS_32_ColorTable12.BackColor = CP.PS_32_ColorTable12
        PS_32_ColorTable13.BackColor = CP.PS_32_ColorTable13
        PS_32_ColorTable14.BackColor = CP.PS_32_ColorTable14
        PS_32_ColorTable15.BackColor = CP.PS_32_ColorTable15
        XenonTrackbar11.Value = CP.PS_32_ScreenColorsForeground
        XenonTrackbar6.Value = CP.PS_32_ScreenColorsBackground
        XenonTrackbar44.Value = CP.PS_32_PopupBackground
        XenonTrackbar5.Value = CP.PS_32_PopupForeground

        PS_64_ColorTable00.BackColor = CP.PS_64_ColorTable00
        PS_64_ColorTable01.BackColor = CP.PS_64_ColorTable01
        PS_64_ColorTable02.BackColor = CP.PS_64_ColorTable02
        PS_64_ColorTable03.BackColor = CP.PS_64_ColorTable03
        PS_64_ColorTable04.BackColor = CP.PS_64_ColorTable04
        PS_64_ColorTable05.BackColor = CP.PS_64_ColorTable05
        PS_64_ColorTable06.BackColor = CP.PS_64_ColorTable06
        PS_64_ColorTable07.BackColor = CP.PS_64_ColorTable07
        PS_64_ColorTable08.BackColor = CP.PS_64_ColorTable08
        PS_64_ColorTable09.BackColor = CP.PS_64_ColorTable09
        PS_64_ColorTable10.BackColor = CP.PS_64_ColorTable10
        PS_64_ColorTable11.BackColor = CP.PS_64_ColorTable11
        PS_64_ColorTable12.BackColor = CP.PS_64_ColorTable12
        PS_64_ColorTable13.BackColor = CP.PS_64_ColorTable13
        PS_64_ColorTable14.BackColor = CP.PS_64_ColorTable14
        PS_64_ColorTable15.BackColor = CP.PS_64_ColorTable15
        XenonTrackbar10.Value = CP.PS_64_ScreenColorsForeground
        XenonTrackbar9.Value = CP.PS_64_ScreenColorsBackground
        XenonTrackbar8.Value = CP.PS_64_PopupForeground
        XenonTrackbar7.Value = CP.PS_64_PopupBackground



    End Sub

    Sub ApplyToCP([CP] As CP)
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
        CP.CMD_PopupForeground = XenonTrackbar1.Value
        CP.CMD_PopupBackground = XenonTrackbar2.Value
        CP.CMD_ScreenColorsForeground = XenonTrackbar4.Value
        CP.CMD_ScreenColorsBackground = XenonTrackbar3.Value
        CP.CMD_CursorSize = Val(XenonTextBox1.Text)
        If Not XenonToggle1.Checked Then
            CP.CMD_FontSize = f_cmd.Size * 65536
        Else
            CP.CMD_FontSize = 12 * 65536
        End If
        CP.CMD_FaceName = f_cmd.Name
        CP.CMD_FontRaster = XenonToggle1.Checked
        CP.CMD_FontWeight = XenonComboBox1.SelectedIndex * 100

        CP.PS_32_ColorTable00 = PS_32_ColorTable00.BackColor
        CP.PS_32_ColorTable01 = PS_32_ColorTable01.BackColor
        CP.PS_32_ColorTable02 = PS_32_ColorTable02.BackColor
        CP.PS_32_ColorTable03 = PS_32_ColorTable03.BackColor
        CP.PS_32_ColorTable04 = PS_32_ColorTable04.BackColor
        CP.PS_32_ColorTable05 = PS_32_ColorTable05.BackColor
        CP.PS_32_ColorTable06 = PS_32_ColorTable06.BackColor
        CP.PS_32_ColorTable07 = PS_32_ColorTable07.BackColor
        CP.PS_32_ColorTable08 = PS_32_ColorTable08.BackColor
        CP.PS_32_ColorTable09 = PS_32_ColorTable09.BackColor
        CP.PS_32_ColorTable10 = PS_32_ColorTable10.BackColor
        CP.PS_32_ColorTable11 = PS_32_ColorTable11.BackColor
        CP.PS_32_ColorTable12 = PS_32_ColorTable12.BackColor
        CP.PS_32_ColorTable13 = PS_32_ColorTable13.BackColor
        CP.PS_32_ColorTable14 = PS_32_ColorTable14.BackColor
        CP.PS_32_ColorTable15 = PS_32_ColorTable15.BackColor
        CP.PS_32_ScreenColorsForeground = XenonTrackbar11.Value
        CP.PS_32_ScreenColorsBackground = XenonTrackbar6.Value
        CP.PS_32_PopupBackground = XenonTrackbar44.Value
        CP.PS_32_PopupForeground = XenonTrackbar5.Value

        CP.PS_64_ColorTable00 = PS_64_ColorTable00.BackColor
        CP.PS_64_ColorTable01 = PS_64_ColorTable01.BackColor
        CP.PS_64_ColorTable02 = PS_64_ColorTable02.BackColor
        CP.PS_64_ColorTable03 = PS_64_ColorTable03.BackColor
        CP.PS_64_ColorTable04 = PS_64_ColorTable04.BackColor
        CP.PS_64_ColorTable05 = PS_64_ColorTable05.BackColor
        CP.PS_64_ColorTable06 = PS_64_ColorTable06.BackColor
        CP.PS_64_ColorTable07 = PS_64_ColorTable07.BackColor
        CP.PS_64_ColorTable08 = PS_64_ColorTable08.BackColor
        CP.PS_64_ColorTable09 = PS_64_ColorTable09.BackColor
        CP.PS_64_ColorTable10 = PS_64_ColorTable10.BackColor
        CP.PS_64_ColorTable11 = PS_64_ColorTable11.BackColor
        CP.PS_64_ColorTable12 = PS_64_ColorTable12.BackColor
        CP.PS_64_ColorTable13 = PS_64_ColorTable13.BackColor
        CP.PS_64_ColorTable14 = PS_64_ColorTable14.BackColor
        CP.PS_64_ColorTable15 = PS_64_ColorTable15.BackColor
        CP.PS_64_ScreenColorsForeground = XenonTrackbar10.Value
        CP.PS_64_ScreenColorsBackground = XenonTrackbar9.Value
        CP.PS_64_PopupForeground = XenonTrackbar8.Value
        CP.PS_64_PopupBackground = XenonTrackbar7.Value

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
        XenonCMD1.CMD_PopupForeground = XenonTrackbar1.Value
        XenonCMD1.CMD_PopupBackground = XenonTrackbar2.Value
        XenonCMD1.CMD_ScreenColorsForeground = XenonTrackbar4.Value
        XenonCMD1.CMD_ScreenColorsBackground = XenonTrackbar3.Value
        XenonCMD1.Font = New Font(f_cmd.Name, f_cmd.Size, f_cmd.Style)
        XenonCMD1.Raster = XenonToggle1.Checked

        XenonCMD2.CMD_ColorTable00 = PS_32_ColorTable00.BackColor
        XenonCMD2.CMD_ColorTable01 = PS_32_ColorTable01.BackColor
        XenonCMD2.CMD_ColorTable02 = PS_32_ColorTable02.BackColor
        XenonCMD2.CMD_ColorTable03 = PS_32_ColorTable03.BackColor
        XenonCMD2.CMD_ColorTable04 = PS_32_ColorTable04.BackColor
        XenonCMD2.CMD_ColorTable05 = PS_32_ColorTable05.BackColor
        XenonCMD2.CMD_ColorTable06 = PS_32_ColorTable06.BackColor
        XenonCMD2.CMD_ColorTable07 = PS_32_ColorTable07.BackColor
        XenonCMD2.CMD_ColorTable08 = PS_32_ColorTable08.BackColor
        XenonCMD2.CMD_ColorTable09 = PS_32_ColorTable09.BackColor
        XenonCMD2.CMD_ColorTable10 = PS_32_ColorTable10.BackColor
        XenonCMD2.CMD_ColorTable11 = PS_32_ColorTable11.BackColor
        XenonCMD2.CMD_ColorTable12 = PS_32_ColorTable12.BackColor
        XenonCMD2.CMD_ColorTable13 = PS_32_ColorTable13.BackColor
        XenonCMD2.CMD_ColorTable14 = PS_32_ColorTable14.BackColor
        XenonCMD2.CMD_ColorTable15 = PS_32_ColorTable15.BackColor
        XenonCMD2.CMD_PopupForeground = XenonTrackbar5.Value
        XenonCMD2.CMD_PopupBackground = XenonTrackbar44.Value
        XenonCMD2.CMD_ScreenColorsForeground = XenonTrackbar11.Value
        XenonCMD2.CMD_ScreenColorsBackground = XenonTrackbar6.Value

        XenonCMD3.CMD_ColorTable00 = PS_64_ColorTable00.BackColor
        XenonCMD3.CMD_ColorTable01 = PS_64_ColorTable01.BackColor
        XenonCMD3.CMD_ColorTable02 = PS_64_ColorTable02.BackColor
        XenonCMD3.CMD_ColorTable03 = PS_64_ColorTable03.BackColor
        XenonCMD3.CMD_ColorTable04 = PS_64_ColorTable04.BackColor
        XenonCMD3.CMD_ColorTable05 = PS_64_ColorTable05.BackColor
        XenonCMD3.CMD_ColorTable06 = PS_64_ColorTable06.BackColor
        XenonCMD3.CMD_ColorTable07 = PS_64_ColorTable07.BackColor
        XenonCMD3.CMD_ColorTable08 = PS_64_ColorTable08.BackColor
        XenonCMD3.CMD_ColorTable09 = PS_64_ColorTable09.BackColor
        XenonCMD3.CMD_ColorTable10 = PS_64_ColorTable10.BackColor
        XenonCMD3.CMD_ColorTable11 = PS_64_ColorTable11.BackColor
        XenonCMD3.CMD_ColorTable12 = PS_64_ColorTable12.BackColor
        XenonCMD3.CMD_ColorTable13 = PS_64_ColorTable13.BackColor
        XenonCMD3.CMD_ColorTable14 = PS_64_ColorTable14.BackColor
        XenonCMD3.CMD_ColorTable15 = PS_64_ColorTable15.BackColor
        XenonCMD3.CMD_PopupForeground = XenonTrackbar8.Value
        XenonCMD3.CMD_PopupBackground = XenonTrackbar7.Value
        XenonCMD3.CMD_ScreenColorsForeground = XenonTrackbar10.Value
        XenonCMD3.CMD_ScreenColorsBackground = XenonTrackbar9.Value

        XenonCMD1.Refresh()
        XenonCMD2.Refresh()
        XenonCMD3.Refresh()
    End Sub

    Private Sub XenonButton10_Click(sender As Object, e As EventArgs) Handles XenonButton10.Click
        Dim CPx As New CP(CP.Mode.Registry)
        ApplyToCP(CPx)
        ApplyToCP(MainFrm.CP)
        CPx.Save(CP.SavingMode.Registry)
    End Sub

    Private Sub XenonButton1_Click(sender As Object, e As EventArgs) Handles XenonButton1.Click
        ApplyToCP(MainFrm.CP)
        Me.Close()
    End Sub

    Private Sub XenonButton2_Click(sender As Object, e As EventArgs) Handles XenonButton2.Click
        Me.Close()
    End Sub

    Private Sub cmd_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        MainFrm.Visible = True
    End Sub

    Private Sub ColorTable00_Click(sender As Object, e As EventArgs) Handles ColorTable00.Click, ColorTable01.Click, ColorTable02.Click, ColorTable03.Click, ColorTable04.Click, ColorTable05.Click,
                                                                             ColorTable06.Click, ColorTable07.Click, ColorTable08.Click, ColorTable09.Click, ColorTable10.Click, ColorTable11.Click,
                                                                             ColorTable12.Click, ColorTable13.Click, ColorTable14.Click, ColorTable15.Click,
                                                                             PS_32_ColorTable00.Click, PS_32_ColorTable01.Click, PS_32_ColorTable02.Click, PS_32_ColorTable03.Click, PS_32_ColorTable04.Click, PS_32_ColorTable05.Click,
                                                                             PS_32_ColorTable06.Click, PS_32_ColorTable07.Click, PS_32_ColorTable08.Click, PS_32_ColorTable09.Click, PS_32_ColorTable10.Click, PS_32_ColorTable11.Click,
                                                                             PS_32_ColorTable12.Click, PS_32_ColorTable13.Click, PS_32_ColorTable14.Click, PS_32_ColorTable15.Click,
                                                                             PS_64_ColorTable00.Click, PS_64_ColorTable01.Click, PS_64_ColorTable02.Click, PS_64_ColorTable03.Click, PS_64_ColorTable04.Click, PS_64_ColorTable05.Click,
                                                                             PS_64_ColorTable06.Click, PS_64_ColorTable07.Click, PS_64_ColorTable08.Click, PS_64_ColorTable09.Click, PS_64_ColorTable10.Click, PS_64_ColorTable11.Click,
                                                                             PS_64_ColorTable12.Click, PS_64_ColorTable13.Click, PS_64_ColorTable14.Click, PS_64_ColorTable15.Click

        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            ApplyPreview()
            Exit Sub
        End If

        Dim CList As New List(Of Control) From {sender}

        If sender.Name.ToString.ToLower.StartsWith("cmd") Then
            CList.Add(XenonCMD1)
        ElseIf sender.Name.ToString.ToLower.StartsWith("ps_32") Then
            CList.Add(XenonCMD2)
        ElseIf sender.Name.ToString.ToLower.StartsWith("ps_64") Then
            CList.Add(XenonCMD3)
        Else
            CList.Add(XenonCMD1)
            CList.Add(XenonCMD2)
            CList.Add(XenonCMD3)
        End If

        Dim _Conditions As New Conditions

        Select Case sender.Name.ToString.ToLower
            Case ColorTable00.Name.ToLower
                _Conditions.CMD_ColorTable00 = True

            Case ColorTable01.Name.ToLower
                _Conditions.CMD_ColorTable01 = True

            Case ColorTable02.Name.ToLower
                _Conditions.CMD_ColorTable02 = True

            Case ColorTable03.Name.ToLower
                _Conditions.CMD_ColorTable03 = True

            Case ColorTable04.Name.ToLower
                _Conditions.CMD_ColorTable04 = True

            Case ColorTable05.Name.ToLower
                _Conditions.CMD_ColorTable05 = True

            Case ColorTable06.Name.ToLower
                _Conditions.CMD_ColorTable06 = True

            Case ColorTable07.Name.ToLower
                _Conditions.CMD_ColorTable07 = True

            Case ColorTable08.Name.ToLower
                _Conditions.CMD_ColorTable08 = True

            Case ColorTable09.Name.ToLower
                _Conditions.CMD_ColorTable09 = True

            Case ColorTable10.Name.ToLower
                _Conditions.CMD_ColorTable10 = True

            Case ColorTable11.Name.ToLower
                _Conditions.CMD_ColorTable11 = True

            Case ColorTable12.Name.ToLower
                _Conditions.CMD_ColorTable12 = True

            Case ColorTable13.Name.ToLower
                _Conditions.CMD_ColorTable13 = True

            Case ColorTable14.Name.ToLower
                _Conditions.CMD_ColorTable14 = True

            Case ColorTable15.Name.ToLower
                _Conditions.CMD_ColorTable15 = True

            Case PS_32_ColorTable05.Name.ToLower
                _Conditions.CMD_ColorTable05 = True

            Case PS_32_ColorTable06.Name.ToLower
                _Conditions.CMD_ColorTable06 = True

            Case PS_64_ColorTable05.Name.ToLower
                _Conditions.CMD_ColorTable05 = True

            Case PS_64_ColorTable06.Name.ToLower
                _Conditions.CMD_ColorTable06 = True

        End Select

        Dim C As Color = ColorPickerDlg.Pick(CList, _Conditions)

        sender.backcolor = C
        sender.invalidate
        ApplyPreview()

        If sender.Name.ToString.ToLower.StartsWith("cmd") Then
            UpdateFromTrack(1) : UpdateFromTrack(2) : UpdateFromTrack(3) : UpdateFromTrack(4)

        ElseIf sender.Name.ToString.ToLower.StartsWith("ps_32") Then
            UpdateFromTrack(5) : UpdateFromTrack(6) : UpdateFromTrack(7) : UpdateFromTrack(8)

        ElseIf sender.Name.ToString.ToLower.StartsWith("ps_64") Then
            UpdateFromTrack(9) : UpdateFromTrack(10) : UpdateFromTrack(11) : UpdateFromTrack(12)
        Else
            UpdateFromTrack(1) : UpdateFromTrack(2) : UpdateFromTrack(3) : UpdateFromTrack(4)
            UpdateFromTrack(5) : UpdateFromTrack(6) : UpdateFromTrack(7) : UpdateFromTrack(8)
            UpdateFromTrack(9) : UpdateFromTrack(10) : UpdateFromTrack(11) : UpdateFromTrack(12)
        End If

        CList.Clear()
    End Sub

    Private Sub XenonTrackbar1_Scroll(sender As Object) Handles XenonTrackbar1.Scroll
        With XenonTrackbar1
            Label1.Text = .Value
            If .Value = 10 Then Label1.Text &= " (A)"
            If .Value = 11 Then Label1.Text &= " (B)"
            If .Value = 12 Then Label1.Text &= " (C)"
            If .Value = 13 Then Label1.Text &= " (D)"
            If .Value = 14 Then Label1.Text &= " (E)"
            If .Value = 15 Then Label1.Text &= " (F)"
        End With

        UpdateFromTrack(1)
        ApplyPreview()
    End Sub

    Private Sub XenonTrackbar2_Scroll(sender As Object) Handles XenonTrackbar2.Scroll
        With XenonTrackbar2
            Label2.Text = .Value
            If .Value = 10 Then Label2.Text &= " (A)"
            If .Value = 11 Then Label2.Text &= " (B)"
            If .Value = 12 Then Label2.Text &= " (C)"
            If .Value = 13 Then Label2.Text &= " (D)"
            If .Value = 14 Then Label2.Text &= " (E)"
            If .Value = 15 Then Label2.Text &= " (F)"
        End With

        UpdateFromTrack(2)
        ApplyPreview()
    End Sub


    Sub UpdateFromTrack(i As Integer)

        If i = 1 Then
            Select Case XenonTrackbar1.Value
                Case 0
                    Label1.BackColor = ColorTable00.BackColor
                Case 1
                    Label1.BackColor = ColorTable01.BackColor
                Case 2
                    Label1.BackColor = ColorTable02.BackColor
                Case 3
                    Label1.BackColor = ColorTable03.BackColor
                Case 4
                    Label1.BackColor = ColorTable04.BackColor
                Case 5
                    Label1.BackColor = ColorTable05.BackColor
                Case 6
                    Label1.BackColor = ColorTable06.BackColor
                Case 7
                    Label1.BackColor = ColorTable07.BackColor
                Case 8
                    Label1.BackColor = ColorTable08.BackColor
                Case 9
                    Label1.BackColor = ColorTable09.BackColor
                Case 10
                    Label1.BackColor = ColorTable10.BackColor
                Case 11
                    Label1.BackColor = ColorTable11.BackColor
                Case 12
                    Label1.BackColor = ColorTable12.BackColor
                Case 13
                    Label1.BackColor = ColorTable13.BackColor
                Case 14
                    Label1.BackColor = ColorTable14.BackColor
                Case 15
                    Label1.BackColor = ColorTable15.BackColor
            End Select

            Dim FC0 As Color = If(IsColorDark(Label1.BackColor), ControlPaint.LightLight(Label1.BackColor), ControlPaint.Dark(Label1.BackColor, 0.9))
            Label1.ForeColor = FC0

        ElseIf i = 2 Then

            Select Case XenonTrackbar2.Value
                Case 0
                    Label2.BackColor = ColorTable00.BackColor
                Case 1
                    Label2.BackColor = ColorTable01.BackColor
                Case 2
                    Label2.BackColor = ColorTable02.BackColor
                Case 3
                    Label2.BackColor = ColorTable03.BackColor
                Case 4
                    Label2.BackColor = ColorTable04.BackColor
                Case 5
                    Label2.BackColor = ColorTable05.BackColor
                Case 6
                    Label2.BackColor = ColorTable06.BackColor
                Case 7
                    Label2.BackColor = ColorTable07.BackColor
                Case 8
                    Label2.BackColor = ColorTable08.BackColor
                Case 9
                    Label2.BackColor = ColorTable09.BackColor
                Case 10
                    Label2.BackColor = ColorTable10.BackColor
                Case 11
                    Label2.BackColor = ColorTable11.BackColor
                Case 12
                    Label2.BackColor = ColorTable12.BackColor
                Case 13
                    Label2.BackColor = ColorTable13.BackColor
                Case 14
                    Label2.BackColor = ColorTable14.BackColor
                Case 15
                    Label2.BackColor = ColorTable15.BackColor
            End Select

            Dim FC0 As Color = If(IsColorDark(Label2.BackColor), ControlPaint.LightLight(Label2.BackColor), ControlPaint.Dark(Label2.BackColor, 0.9))
            Label2.ForeColor = FC0

        ElseIf i = 3 Then

            Select Case XenonTrackbar3.Value
                Case 0
                    Label5.BackColor = ColorTable00.BackColor
                Case 1
                    Label5.BackColor = ColorTable01.BackColor
                Case 2
                    Label5.BackColor = ColorTable02.BackColor
                Case 3
                    Label5.BackColor = ColorTable03.BackColor
                Case 4
                    Label5.BackColor = ColorTable04.BackColor
                Case 5
                    Label5.BackColor = ColorTable05.BackColor
                Case 6
                    Label5.BackColor = ColorTable06.BackColor
                Case 7
                    Label5.BackColor = ColorTable07.BackColor
                Case 8
                    Label5.BackColor = ColorTable08.BackColor
                Case 9
                    Label5.BackColor = ColorTable09.BackColor
                Case 10
                    Label5.BackColor = ColorTable10.BackColor
                Case 11
                    Label5.BackColor = ColorTable11.BackColor
                Case 12
                    Label5.BackColor = ColorTable12.BackColor
                Case 13
                    Label5.BackColor = ColorTable13.BackColor
                Case 14
                    Label5.BackColor = ColorTable14.BackColor
                Case 15
                    Label5.BackColor = ColorTable15.BackColor
            End Select

            Dim FC0 As Color = If(IsColorDark(Label5.BackColor), ControlPaint.LightLight(Label5.BackColor), ControlPaint.Dark(Label5.BackColor, 0.9))
            Label5.ForeColor = FC0

        ElseIf i = 4 Then

            Select Case XenonTrackbar4.Value
                Case 0
                    If XenonTrackbar3.Value = XenonTrackbar4.Value Then
                        Label3.BackColor = ColorTable07.BackColor
                    Else
                        Label3.BackColor = ColorTable00.BackColor
                    End If
                Case 1
                    Label3.BackColor = ColorTable01.BackColor
                Case 2
                    Label3.BackColor = ColorTable02.BackColor
                Case 3
                    Label3.BackColor = ColorTable03.BackColor
                Case 4
                    Label3.BackColor = ColorTable04.BackColor
                Case 5
                    Label3.BackColor = ColorTable05.BackColor
                Case 6
                    Label3.BackColor = ColorTable06.BackColor
                Case 7
                    Label3.BackColor = ColorTable07.BackColor
                Case 8
                    Label3.BackColor = ColorTable08.BackColor
                Case 9
                    Label3.BackColor = ColorTable09.BackColor
                Case 10
                    Label3.BackColor = ColorTable10.BackColor
                Case 11
                    Label3.BackColor = ColorTable11.BackColor
                Case 12
                    Label3.BackColor = ColorTable12.BackColor
                Case 13
                    Label3.BackColor = ColorTable13.BackColor
                Case 14
                    Label3.BackColor = ColorTable14.BackColor
                Case 15
                    Label3.BackColor = ColorTable15.BackColor
            End Select

            Dim FC0 As Color = If(IsColorDark(Label3.BackColor), ControlPaint.LightLight(Label3.BackColor), ControlPaint.Dark(Label3.BackColor, 0.9))
            Label3.ForeColor = FC0


        ElseIf i = 5 Then

            Select Case XenonTrackbar5.Value
                Case 0
                    Label10.BackColor = PS_32_ColorTable00.BackColor
                Case 1
                    Label10.BackColor = PS_32_ColorTable01.BackColor
                Case 2
                    Label10.BackColor = PS_32_ColorTable02.BackColor
                Case 3
                    Label10.BackColor = PS_32_ColorTable03.BackColor
                Case 4
                    Label10.BackColor = PS_32_ColorTable04.BackColor
                Case 5
                    Label10.BackColor = PS_32_ColorTable05.BackColor
                Case 6
                    Label10.BackColor = PS_32_ColorTable06.BackColor
                Case 7
                    Label10.BackColor = PS_32_ColorTable07.BackColor
                Case 8
                    Label10.BackColor = PS_32_ColorTable08.BackColor
                Case 9
                    Label10.BackColor = PS_32_ColorTable09.BackColor
                Case 10
                    Label10.BackColor = PS_32_ColorTable10.BackColor
                Case 11
                    Label10.BackColor = PS_32_ColorTable11.BackColor
                Case 12
                    Label10.BackColor = PS_32_ColorTable12.BackColor
                Case 13
                    Label10.BackColor = PS_32_ColorTable13.BackColor
                Case 14
                    Label10.BackColor = PS_32_ColorTable14.BackColor
                Case 15
                    Label10.BackColor = PS_32_ColorTable15.BackColor
            End Select

            Dim FC0 As Color = If(IsColorDark(Label10.BackColor), ControlPaint.LightLight(Label10.BackColor), ControlPaint.Dark(Label10.BackColor, 0.9))
            Label10.ForeColor = FC0

        ElseIf i = 6 Then

            Select Case XenonTrackbar44.Value
                Case 0
                    Label11.BackColor = PS_32_ColorTable00.BackColor
                Case 1
                    Label11.BackColor = PS_32_ColorTable01.BackColor
                Case 2
                    Label11.BackColor = PS_32_ColorTable02.BackColor
                Case 3
                    Label11.BackColor = PS_32_ColorTable03.BackColor
                Case 4
                    Label11.BackColor = PS_32_ColorTable04.BackColor
                Case 5
                    Label11.BackColor = PS_32_ColorTable05.BackColor
                Case 6
                    Label11.BackColor = PS_32_ColorTable06.BackColor
                Case 7
                    Label11.BackColor = PS_32_ColorTable07.BackColor
                Case 8
                    Label11.BackColor = PS_32_ColorTable08.BackColor
                Case 9
                    Label11.BackColor = PS_32_ColorTable09.BackColor
                Case 10
                    Label11.BackColor = PS_32_ColorTable10.BackColor
                Case 11
                    Label11.BackColor = PS_32_ColorTable11.BackColor
                Case 12
                    Label11.BackColor = PS_32_ColorTable12.BackColor
                Case 13
                    Label11.BackColor = PS_32_ColorTable13.BackColor
                Case 14
                    Label11.BackColor = PS_32_ColorTable14.BackColor
                Case 15
                    Label11.BackColor = PS_32_ColorTable15.BackColor
            End Select

            Dim FC0 As Color = If(IsColorDark(Label11.BackColor), ControlPaint.LightLight(Label11.BackColor), ControlPaint.Dark(Label11.BackColor, 0.9))
            Label11.ForeColor = FC0

        ElseIf i = 7 Then

            Select Case XenonTrackbar11.Value
                Case 0
                    Label15.BackColor = PS_32_ColorTable00.BackColor
                Case 1
                    Label15.BackColor = PS_32_ColorTable01.BackColor
                Case 2
                    Label15.BackColor = PS_32_ColorTable02.BackColor
                Case 3
                    Label15.BackColor = PS_32_ColorTable03.BackColor
                Case 4
                    Label15.BackColor = PS_32_ColorTable04.BackColor
                Case 5
                    Label15.BackColor = PS_32_ColorTable05.BackColor
                Case 6
                    Label15.BackColor = PS_32_ColorTable06.BackColor
                Case 7
                    Label15.BackColor = PS_32_ColorTable07.BackColor
                Case 8
                    Label15.BackColor = PS_32_ColorTable08.BackColor
                Case 9
                    Label15.BackColor = PS_32_ColorTable09.BackColor
                Case 10
                    Label15.BackColor = PS_32_ColorTable10.BackColor
                Case 11
                    Label15.BackColor = PS_32_ColorTable11.BackColor
                Case 12
                    Label15.BackColor = PS_32_ColorTable12.BackColor
                Case 13
                    Label15.BackColor = PS_32_ColorTable13.BackColor
                Case 14
                    Label15.BackColor = PS_32_ColorTable14.BackColor
                Case 15
                    Label15.BackColor = PS_32_ColorTable15.BackColor
            End Select

            Dim FC0 As Color = If(IsColorDark(Label15.BackColor), ControlPaint.LightLight(Label15.BackColor), ControlPaint.Dark(Label15.BackColor, 0.9))
            Label15.ForeColor = FC0


        ElseIf i = 8 Then

            Select Case XenonTrackbar6.Value
                Case 0
                    Label16.BackColor = PS_32_ColorTable00.BackColor
                Case 1
                    Label16.BackColor = PS_32_ColorTable01.BackColor
                Case 2
                    Label16.BackColor = PS_32_ColorTable02.BackColor
                Case 3
                    Label16.BackColor = PS_32_ColorTable03.BackColor
                Case 4
                    Label16.BackColor = PS_32_ColorTable04.BackColor
                Case 5
                    Label16.BackColor = PS_32_ColorTable05.BackColor
                Case 6
                    Label16.BackColor = PS_32_ColorTable06.BackColor
                Case 7
                    Label16.BackColor = PS_32_ColorTable07.BackColor
                Case 8
                    Label16.BackColor = PS_32_ColorTable08.BackColor
                Case 9
                    Label16.BackColor = PS_32_ColorTable09.BackColor
                Case 10
                    Label16.BackColor = PS_32_ColorTable10.BackColor
                Case 11
                    Label16.BackColor = PS_32_ColorTable11.BackColor
                Case 12
                    Label16.BackColor = PS_32_ColorTable12.BackColor
                Case 13
                    Label16.BackColor = PS_32_ColorTable13.BackColor
                Case 14
                    Label16.BackColor = PS_32_ColorTable14.BackColor
                Case 15
                    Label16.BackColor = PS_32_ColorTable15.BackColor
            End Select

            Dim FC0 As Color = If(IsColorDark(Label16.BackColor), ControlPaint.LightLight(Label16.BackColor), ControlPaint.Dark(Label16.BackColor, 0.9))
            Label16.ForeColor = FC0

        ElseIf i = 9 Then

            Select Case XenonTrackbar8.Value
                Case 0
                    Label4.BackColor = PS_64_ColorTable00.BackColor
                Case 1
                    Label4.BackColor = PS_64_ColorTable01.BackColor
                Case 2
                    Label4.BackColor = PS_64_ColorTable02.BackColor
                Case 3
                    Label4.BackColor = PS_64_ColorTable03.BackColor
                Case 4
                    Label4.BackColor = PS_64_ColorTable04.BackColor
                Case 5
                    Label4.BackColor = PS_64_ColorTable05.BackColor
                Case 6
                    Label4.BackColor = PS_64_ColorTable06.BackColor
                Case 7
                    Label4.BackColor = PS_64_ColorTable07.BackColor
                Case 8
                    Label4.BackColor = PS_64_ColorTable08.BackColor
                Case 9
                    Label4.BackColor = PS_64_ColorTable09.BackColor
                Case 10
                    Label4.BackColor = PS_64_ColorTable10.BackColor
                Case 11
                    Label4.BackColor = PS_64_ColorTable11.BackColor
                Case 12
                    Label4.BackColor = PS_64_ColorTable12.BackColor
                Case 13
                    Label4.BackColor = PS_64_ColorTable13.BackColor
                Case 14
                    Label4.BackColor = PS_64_ColorTable14.BackColor
                Case 15
                    Label4.BackColor = PS_64_ColorTable15.BackColor
            End Select

            Dim FC0 As Color = If(IsColorDark(Label4.BackColor), ControlPaint.LightLight(Label4.BackColor), ControlPaint.Dark(Label4.BackColor, 0.9))
            Label4.ForeColor = FC0

        ElseIf i = 10 Then

            Select Case XenonTrackbar7.Value
                Case 0
                    Label8.BackColor = PS_64_ColorTable00.BackColor
                Case 1
                    Label8.BackColor = PS_64_ColorTable01.BackColor
                Case 2
                    Label8.BackColor = PS_64_ColorTable02.BackColor
                Case 3
                    Label8.BackColor = PS_64_ColorTable03.BackColor
                Case 4
                    Label8.BackColor = PS_64_ColorTable04.BackColor
                Case 5
                    Label8.BackColor = PS_64_ColorTable05.BackColor
                Case 6
                    Label8.BackColor = PS_64_ColorTable06.BackColor
                Case 7
                    Label8.BackColor = PS_64_ColorTable07.BackColor
                Case 8
                    Label8.BackColor = PS_64_ColorTable08.BackColor
                Case 9
                    Label8.BackColor = PS_64_ColorTable09.BackColor
                Case 10
                    Label8.BackColor = PS_64_ColorTable10.BackColor
                Case 11
                    Label8.BackColor = PS_64_ColorTable11.BackColor
                Case 12
                    Label8.BackColor = PS_64_ColorTable12.BackColor
                Case 13
                    Label8.BackColor = PS_64_ColorTable13.BackColor
                Case 14
                    Label8.BackColor = PS_64_ColorTable14.BackColor
                Case 15
                    Label8.BackColor = PS_64_ColorTable15.BackColor
            End Select

            Dim FC0 As Color = If(IsColorDark(Label8.BackColor), ControlPaint.LightLight(Label8.BackColor), ControlPaint.Dark(Label8.BackColor, 0.9))
            Label8.ForeColor = FC0

        ElseIf i = 11 Then

            Select Case XenonTrackbar10.Value
                Case 0
                    Label40.BackColor = PS_64_ColorTable00.BackColor
                Case 1
                    Label40.BackColor = PS_64_ColorTable01.BackColor
                Case 2
                    Label40.BackColor = PS_64_ColorTable02.BackColor
                Case 3
                    Label40.BackColor = PS_64_ColorTable03.BackColor
                Case 4
                    Label40.BackColor = PS_64_ColorTable04.BackColor
                Case 5
                    Label40.BackColor = PS_64_ColorTable05.BackColor
                Case 6
                    Label40.BackColor = PS_64_ColorTable06.BackColor
                Case 7
                    Label40.BackColor = PS_64_ColorTable07.BackColor
                Case 8
                    Label40.BackColor = PS_64_ColorTable08.BackColor
                Case 9
                    Label40.BackColor = PS_64_ColorTable09.BackColor
                Case 10
                    Label40.BackColor = PS_64_ColorTable10.BackColor
                Case 11
                    Label40.BackColor = PS_64_ColorTable11.BackColor
                Case 12
                    Label40.BackColor = PS_64_ColorTable12.BackColor
                Case 13
                    Label40.BackColor = PS_64_ColorTable13.BackColor
                Case 14
                    Label40.BackColor = PS_64_ColorTable14.BackColor
                Case 15
                    Label40.BackColor = PS_64_ColorTable15.BackColor
            End Select

            Dim FC0 As Color = If(IsColorDark(Label40.BackColor), ControlPaint.LightLight(Label40.BackColor), ControlPaint.Dark(Label40.BackColor, 0.9))
            Label40.ForeColor = FC0

        ElseIf i = 12 Then

            Select Case XenonTrackbar9.Value
                Case 0
                    Label43.BackColor = PS_64_ColorTable00.BackColor
                Case 1
                    Label43.BackColor = PS_64_ColorTable01.BackColor
                Case 2
                    Label43.BackColor = PS_64_ColorTable02.BackColor
                Case 3
                    Label43.BackColor = PS_64_ColorTable03.BackColor
                Case 4
                    Label43.BackColor = PS_64_ColorTable04.BackColor
                Case 5
                    Label43.BackColor = PS_64_ColorTable05.BackColor
                Case 6
                    Label43.BackColor = PS_64_ColorTable06.BackColor
                Case 7
                    Label43.BackColor = PS_64_ColorTable07.BackColor
                Case 8
                    Label43.BackColor = PS_64_ColorTable08.BackColor
                Case 9
                    Label43.BackColor = PS_64_ColorTable09.BackColor
                Case 10
                    Label43.BackColor = PS_64_ColorTable10.BackColor
                Case 11
                    Label43.BackColor = PS_64_ColorTable11.BackColor
                Case 12
                    Label43.BackColor = PS_64_ColorTable12.BackColor
                Case 13
                    Label43.BackColor = PS_64_ColorTable13.BackColor
                Case 14
                    Label43.BackColor = PS_64_ColorTable14.BackColor
                Case 15
                    Label43.BackColor = PS_64_ColorTable15.BackColor
            End Select

            Dim FC0 As Color = If(IsColorDark(Label43.BackColor), ControlPaint.LightLight(Label43.BackColor), ControlPaint.Dark(Label43.BackColor, 0.9))
            Label43.ForeColor = FC0

        End If
    End Sub

    Private Sub XenonTrackbar12_Scroll(sender As Object) Handles XenonTrackbar4.Scroll
        With XenonTrackbar4
            Label3.Text = .Value
            If .Value = 10 Then Label3.Text &= " (A)"
            If .Value = 11 Then Label3.Text &= " (B)"
            If .Value = 12 Then Label3.Text &= " (C)"
            If .Value = 13 Then Label3.Text &= " (D)"
            If .Value = 14 Then Label3.Text &= " (E)"
            If .Value = 15 Then Label3.Text &= " (F)"
        End With

        UpdateFromTrack(3) : UpdateFromTrack(4)
        ApplyPreview()
    End Sub

    Private Sub XenonTrackbar3_Scroll(sender As Object) Handles XenonTrackbar3.Scroll
        With XenonTrackbar3
            Label5.Text = .Value
            If .Value = 10 Then Label5.Text &= " (A)"
            If .Value = 11 Then Label5.Text &= " (B)"
            If .Value = 12 Then Label5.Text &= " (C)"
            If .Value = 13 Then Label5.Text &= " (D)"
            If .Value = 14 Then Label5.Text &= " (E)"
            If .Value = 15 Then Label5.Text &= " (F)"
        End With

        UpdateFromTrack(3) : UpdateFromTrack(4)
        ApplyPreview()
    End Sub

    Private Sub XenonTrackbar11_Scroll(sender As Object) Handles XenonTrackbar11.Scroll
        With XenonTrackbar11
            Label15.Text = .Value
            If .Value = 10 Then Label15.Text &= " (A)"
            If .Value = 11 Then Label15.Text &= " (B)"
            If .Value = 12 Then Label15.Text &= " (C)"
            If .Value = 13 Then Label15.Text &= " (D)"
            If .Value = 14 Then Label15.Text &= " (E)"
            If .Value = 15 Then Label15.Text &= " (F)"
        End With

        UpdateFromTrack(7)
        ApplyPreview()
    End Sub

    Private Sub XenonTrackbar6_Scroll(sender As Object) Handles XenonTrackbar6.Scroll
        With XenonTrackbar6
            Label16.Text = .Value
            If .Value = 10 Then Label16.Text &= " (A)"
            If .Value = 11 Then Label16.Text &= " (B)"
            If .Value = 12 Then Label16.Text &= " (C)"
            If .Value = 13 Then Label16.Text &= " (D)"
            If .Value = 14 Then Label16.Text &= " (E)"
            If .Value = 15 Then Label16.Text &= " (F)"
        End With

        UpdateFromTrack(8)
        ApplyPreview()
    End Sub

    Private Sub XenonTrackbar5_Scroll(sender As Object) Handles XenonTrackbar5.Scroll
        With XenonTrackbar5
            Label10.Text = .Value
            If .Value = 10 Then Label10.Text &= " (A)"
            If .Value = 11 Then Label10.Text &= " (B)"
            If .Value = 12 Then Label10.Text &= " (C)"
            If .Value = 13 Then Label10.Text &= " (D)"
            If .Value = 14 Then Label10.Text &= " (E)"
            If .Value = 15 Then Label10.Text &= " (F)"
        End With

        UpdateFromTrack(5)
        ApplyPreview()
    End Sub

    Private Sub XenonTrackbar44_Scroll(sender As Object) Handles XenonTrackbar44.Scroll
        With XenonTrackbar44
            Label11.Text = .Value
            If .Value = 10 Then Label11.Text &= " (A)"
            If .Value = 11 Then Label11.Text &= " (B)"
            If .Value = 12 Then Label11.Text &= " (C)"
            If .Value = 13 Then Label11.Text &= " (D)"
            If .Value = 14 Then Label11.Text &= " (E)"
            If .Value = 15 Then Label11.Text &= " (F)"
        End With

        UpdateFromTrack(6)
        ApplyPreview()
    End Sub

    Private Sub XenonTrackbar10_Scroll(sender As Object) Handles XenonTrackbar10.Scroll
        With XenonTrackbar10
            Label40.Text = .Value
            If .Value = 10 Then Label40.Text &= " (A)"
            If .Value = 11 Then Label40.Text &= " (B)"
            If .Value = 12 Then Label40.Text &= " (C)"
            If .Value = 13 Then Label40.Text &= " (D)"
            If .Value = 14 Then Label40.Text &= " (E)"
            If .Value = 15 Then Label40.Text &= " (F)"
        End With

        UpdateFromTrack(11)
        ApplyPreview()
    End Sub

    Private Sub XenonTrackbar9_Scroll(sender As Object) Handles XenonTrackbar9.Scroll
        With XenonTrackbar9
            Label43.Text = .Value
            If .Value = 10 Then Label43.Text &= " (A)"
            If .Value = 11 Then Label43.Text &= " (B)"
            If .Value = 12 Then Label43.Text &= " (C)"
            If .Value = 13 Then Label43.Text &= " (D)"
            If .Value = 14 Then Label43.Text &= " (E)"
            If .Value = 15 Then Label43.Text &= " (F)"
        End With

        UpdateFromTrack(12)
        ApplyPreview()
    End Sub

    Private Sub XenonTrackbar8_Scroll(sender As Object) Handles XenonTrackbar8.Scroll
        With XenonTrackbar8
            Label4.Text = .Value
            If .Value = 10 Then Label4.Text &= " (A)"
            If .Value = 11 Then Label4.Text &= " (B)"
            If .Value = 12 Then Label4.Text &= " (C)"
            If .Value = 13 Then Label4.Text &= " (D)"
            If .Value = 14 Then Label4.Text &= " (E)"
            If .Value = 15 Then Label4.Text &= " (F)"
        End With

        UpdateFromTrack(9)
        ApplyPreview()
    End Sub

    Private Sub XenonTrackbar7_Scroll(sender As Object) Handles XenonTrackbar7.Scroll
        With XenonTrackbar7
            Label8.Text = .Value
            If .Value = 10 Then Label8.Text &= " (A)"
            If .Value = 11 Then Label8.Text &= " (B)"
            If .Value = 12 Then Label8.Text &= " (C)"
            If .Value = 13 Then Label8.Text &= " (D)"
            If .Value = 14 Then Label8.Text &= " (E)"
            If .Value = 15 Then Label8.Text &= " (F)"
        End With

        UpdateFromTrack(10)
        ApplyPreview()
    End Sub

    Private Sub XenonButton3_Click(sender As Object, e As EventArgs) Handles XenonButton3.Click
        If cmd1_fontdlg.ShowDialog = DialogResult.OK Then
            f_cmd = cmd1_fontdlg.Font
            ApplyPreview()
        End If
    End Sub


    Private Sub XenonButton4_Click(sender As Object, e As EventArgs) Handles XenonButton4.Click
        Process.Start("cmd.exe")
    End Sub

    Private Sub cmd_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        _Shown = True
    End Sub

    Private Sub XenonToggle1_CheckedChanged(sender As Object, e As EventArgs) Handles XenonToggle1.CheckedChanged
        If _Shown Then

            If XenonToggle1.Enabled Then
                XenonCMD1.Font = New Font(f_cmd.Name, 12, f_cmd.Style)
            Else
                XenonCMD1.Font = f_cmd
            End If

            ApplyPreview()
        End If
    End Sub

    Private Sub XenonComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles XenonComboBox1.SelectedIndexChanged
        If Not _Shown Then Exit Sub
        Dim fx As New LOGFONT
        f_cmd = cmd1_fontdlg.Font
        f_cmd.ToLogFont(fx)
        fx.lfWeight = XenonComboBox1.SelectedIndex * 100
        With Font.FromLogFont(fx) : f_cmd = New Font(.Name, f_cmd.Size, .Style) : End With
        cmd1_fontdlg.Font = f_cmd
        ApplyPreview()
    End Sub
End Class