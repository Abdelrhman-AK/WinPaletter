Imports WinPaletter.XenonCore

Public Class cmd
    Private Sub cmd_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ApplyDarkMode(Me)
        ApplyFromCP(MainFrm.CP)
        MainFrm.Visible = False
        Label1.Font = My.Application.ConsoleFont
        Label2.Font = My.Application.ConsoleFont
        Location = New Point(10, (My.Computer.Screen.Bounds.Height - Height) / 2 - 20)
    End Sub

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
        XenonTrackbar3.Value = CP.CMD_ScreenColors
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
        CP.CMD_ScreenColors = XenonTrackbar3.Value
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
                                                                             ColorTable12.Click, ColorTable13.Click, ColorTable14.Click, ColorTable15.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            Exit Sub
        End If

        Dim CList As New List(Of Control) From {sender}

        Dim C As Color = ColorPickerDlg.Pick(CList)
        'CP.Titlebar_Active = Color.FromArgb(255, C)
        'ApplyLivePreviewFromCP(CP)

        sender.backcolor = C
        sender.invalidate

        UpdateFromTrack(1) : UpdateFromTrack(2) : UpdateFromTrack(3)

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
    End Sub

    Private Sub XenonTrackbar3_Scroll(sender As Object) Handles XenonTrackbar3.Scroll
        With XenonTrackbar3
            Label3.Text = .Value
            If .Value = 10 Then Label3.Text &= " (A)"
            If .Value = 11 Then Label3.Text &= " (B)"
            If .Value = 12 Then Label3.Text &= " (C)"
            If .Value = 13 Then Label3.Text &= " (D)"
            If .Value = 14 Then Label3.Text &= " (E)"
            If .Value = 15 Then Label3.Text &= " (F)"
        End With

        UpdateFromTrack(3)
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
                    Label3.BackColor = ColorTable00.BackColor
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
        End If
    End Sub

End Class