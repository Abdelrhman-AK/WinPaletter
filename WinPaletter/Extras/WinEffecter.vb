Imports WinPaletter.PreviewHelpers
Imports WinPaletter.XenonCore

Public Class WinEffecter
    Private Sub WinEffecter_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ApplyDarkMode(Me)
        XenonButton12.Image = MainFrm.XenonButton20.Image.Resize(16, 16)
        ApplyFromCP(My.CP)
        SetClassicButtonColors(My.CP, RetroButton1)

    End Sub

    Sub ApplyFromCP(CP As CP)
        With CP.WindowsEffects
            EffectsEnabled.Checked = .Enabled
            XenonCheckBox1.Checked = .WindowAnimation
            XenonCheckBox2.Checked = .WindowShadow
            XenonCheckBox3.Checked = .WindowUIEffects
            XenonCheckBox6.Checked = .MenuAnimation
            If .MenuFade = CP.Structures.WinEffects.MenuAnimType.Fade Then XenonComboBox1.SelectedIndex = 0 Else XenonComboBox1.SelectedIndex = 1
            XenonCheckBox5.Checked = .MenuSelectionFade
            XenonTrackbar1.Value = .MenuShowDelay
            XenonCheckBox8.Checked = .ComboboxAnimation
            XenonCheckBox7.Checked = .ListBoxSmoothScrolling
            XenonCheckBox9.Checked = .TooltipAnimation
            If .TooltipFade = CP.Structures.WinEffects.MenuAnimType.Fade Then XenonComboBox2.SelectedIndex = 0 Else XenonComboBox2.SelectedIndex = 1
            XenonCheckBox4.Checked = .IconsShadow
            XenonCheckBox10.Checked = .IconsDesktopTranslSel
            XenonCheckBox11.Checked = .ShowWinContentDrag
            XenonCheckBox12.Checked = .KeyboardUnderline
            XenonTrackbar5.Value = .NotificationDuration
            XenonTrackbar2.Value = .FocusRectWidth
            XenonTrackbar3.Value = .FocusRectHeight
            XenonTrackbar4.Value = .Caret
            XenonCheckBox13.Checked = .AWT_Enabled
            XenonCheckBox14.Checked = .AWT_BringActivatedWindowToTop
            XenonTrackbar6.Value = .AWT_Delay
            XenonCheckBox15.Checked = .SnapCursorToDefButton
            XenonCheckBox16.Checked = .Win11ClassicContextMenu
            XenonCheckBox17.Checked = .BalloonNotifications
            XenonCheckBox20.Checked = .SysListView32
            XenonCheckBox19.Checked = .ShowSecondsInSystemClock
            XenonCheckBox18.Checked = .PaintDesktopVersion
            XenonCheckBox21.Checked = .ShakeToMinimize
            XenonCheckBox22.Checked = .Win11BootDots

            XenonRadioButton1.Checked = (.Win11ExplorerBar = CP.Structures.WinEffects.ExplorerBar.Default)
            XenonRadioButton2.Checked = (.Win11ExplorerBar = CP.Structures.WinEffects.ExplorerBar.Ribbon)
            XenonRadioButton3.Checked = (.Win11ExplorerBar = CP.Structures.WinEffects.ExplorerBar.Bar)
            XenonCheckBox23.Checked = .DisableNavBar
            XenonCheckBox24.Checked = .AutoHideScrollBars
            XenonCheckBox25.Checked = .FullScreenStartMenu

            If Not .ColorFilter_Enabled Then
                XenonRadioImage1.Checked = True
            Else
                Select Case .ColorFilter
                    Case CP.Structures.WinEffects.ColorFilters.Grayscale
                        XenonRadioImage5.Checked = True

                    Case CP.Structures.WinEffects.ColorFilters.Inverted
                        XenonRadioImage7.Checked = True

                    Case CP.Structures.WinEffects.ColorFilters.GrayscaleInverted
                        XenonRadioImage6.Checked = True

                    Case CP.Structures.WinEffects.ColorFilters.RedGreen_deuteranopia
                        XenonRadioImage2.Checked = True

                    Case CP.Structures.WinEffects.ColorFilters.RedGreen_protanopia
                        XenonRadioImage3.Checked = True

                    Case CP.Structures.WinEffects.ColorFilters.BlueYellow
                        XenonRadioImage4.Checked = True

                    Case Else
                        XenonRadioImage1.Checked = True

                End Select
            End If

            Panel2.Width = .Caret
        End With


    End Sub

    Sub ApplyToCP(CP As CP)
        With CP.WindowsEffects
            .Enabled = EffectsEnabled.Checked
            .WindowAnimation = XenonCheckBox1.Checked
            .WindowShadow = XenonCheckBox2.Checked
            .WindowUIEffects = XenonCheckBox3.Checked
            .MenuAnimation = XenonCheckBox6.Checked
            If XenonComboBox1.SelectedIndex = 0 Then .MenuFade = CP.Structures.WinEffects.MenuAnimType.Fade Else .MenuFade = CP.Structures.WinEffects.MenuAnimType.Scroll
            .MenuSelectionFade = XenonCheckBox5.Checked
            .MenuShowDelay = XenonTrackbar1.Value
            .ComboBoxAnimation = XenonCheckBox8.Checked
            .ListBoxSmoothScrolling = XenonCheckBox7.Checked
            .TooltipAnimation = XenonCheckBox9.Checked
            If XenonComboBox2.SelectedIndex = 0 Then .TooltipFade = CP.Structures.WinEffects.MenuAnimType.Fade Else .TooltipFade = CP.Structures.WinEffects.MenuAnimType.Scroll
            .IconsShadow = XenonCheckBox4.Checked
            .IconsDesktopTranslSel = XenonCheckBox10.Checked
            .ShowWinContentDrag = XenonCheckBox11.Checked
            .KeyboardUnderline = XenonCheckBox12.Checked
            .NotificationDuration = XenonTrackbar5.Value
            .FocusRectWidth = XenonTrackbar2.Value
            .FocusRectHeight = XenonTrackbar3.Value
            .Caret = XenonTrackbar4.Value
            .AWT_Enabled = XenonCheckBox13.Checked
            .AWT_BringActivatedWindowToTop = XenonCheckBox14.Checked
            .AWT_Delay = XenonTrackbar6.Value
            .SnapCursorToDefButton = XenonCheckBox15.Checked
            .Win11ClassicContextMenu = XenonCheckBox16.Checked
            .BalloonNotifications = XenonCheckBox17.Checked
            .SysListView32 = XenonCheckBox20.Checked
            .ShowSecondsInSystemClock = XenonCheckBox19.Checked
            .PaintDesktopVersion = XenonCheckBox18.Checked
            .ShakeToMinimize = XenonCheckBox21.Checked
            .Win11BootDots = XenonCheckBox22.Checked

            If XenonRadioButton1.Checked Then
                .Win11ExplorerBar = CP.Structures.WinEffects.ExplorerBar.Default

            ElseIf XenonRadioButton2.Checked Then
                .Win11ExplorerBar = CP.Structures.WinEffects.ExplorerBar.Ribbon

            ElseIf XenonRadioButton3.Checked Then
                .Win11ExplorerBar = CP.Structures.WinEffects.ExplorerBar.Bar

            End If

            .DisableNavBar = XenonCheckBox23.Checked
            .AutoHideScrollBars = XenonCheckBox24.Checked
            .FullScreenStartMenu = XenonCheckBox25.Checked

            If XenonRadioImage1.Checked Then
                .ColorFilter_Enabled = False

            ElseIf XenonRadioImage5.Checked Then
                .ColorFilter_Enabled = True
                .ColorFilter = CP.Structures.WinEffects.ColorFilters.Grayscale

            ElseIf XenonRadioImage7.Checked Then
                .ColorFilter_Enabled = True
                .ColorFilter = CP.Structures.WinEffects.ColorFilters.Inverted

            ElseIf XenonRadioImage6.Checked Then
                .ColorFilter_Enabled = True
                .ColorFilter = CP.Structures.WinEffects.ColorFilters.GrayscaleInverted

            ElseIf XenonRadioImage2.Checked Then
                .ColorFilter_Enabled = True
                .ColorFilter = CP.Structures.WinEffects.ColorFilters.RedGreen_deuteranopia

            ElseIf XenonRadioImage3.Checked Then
                .ColorFilter_Enabled = True
                .ColorFilter = CP.Structures.WinEffects.ColorFilters.RedGreen_protanopia

            ElseIf XenonRadioImage4.Checked Then
                .ColorFilter_Enabled = True
                .ColorFilter = CP.Structures.WinEffects.ColorFilters.BlueYellow

            End If

        End With
    End Sub

    Private Sub XenonButton11_Click(sender As Object, e As EventArgs) Handles XenonButton11.Click
        If OpenFileDialog1.ShowDialog = DialogResult.OK Then
            Dim CPx As New CP(CP.CP_Type.File, OpenFileDialog1.FileName)
            ApplyFromCP(CPx)
            CPx.Dispose()
        End If
    End Sub

    Private Sub XenonButton9_Click(sender As Object, e As EventArgs) Handles XenonButton9.Click
        Dim CPx As New CP(CP.CP_Type.Registry)
        ApplyFromCP(CPx)
        CPx.Dispose()
    End Sub

    Private Sub XenonButton12_Click(sender As Object, e As EventArgs) Handles XenonButton12.Click
        Using _Def As CP = CP_Defaults.From(My.PreviewStyle)
            ApplyFromCP(_Def)
        End Using
    End Sub

    Private Sub XenonButton7_Click(sender As Object, e As EventArgs) Handles XenonButton7.Click
        Me.Close()
    End Sub

    Private Sub XenonButton10_Click(sender As Object, e As EventArgs) Handles XenonButton10.Click
        Cursor = Cursors.WaitCursor
        Dim CPx As New CP(CP.CP_Type.Registry)
        ApplyToCP(CPx)
        ApplyToCP(My.CP)
        MainFrm.ApplyColorsToElements(CPx)
        CPx.WindowsEffects.Apply()
        CPx.Win32.Update_UPM_DEFAULT()
        CPx.Dispose()
        Cursor = Cursors.Default
    End Sub

    Private Sub XenonButton8_Click(sender As Object, e As EventArgs) Handles XenonButton8.Click
        Cursor = Cursors.WaitCursor
        ApplyToCP(My.CP)
        MainFrm.ApplyColorsToElements(My.CP)
        MainFrm.ApplyStylesToElements(My.CP, False)
        Cursor = Cursors.Default
        Close()
    End Sub

    Private Sub EffectsEnabled_CheckedChanged(sender As Object, e As EventArgs) Handles EffectsEnabled.CheckedChanged
        checker_img.Image = If(sender.Checked, My.Resources.checker_enabled, My.Resources.checker_disabled)
    End Sub

    Private Sub MD_Click(sender As Object, e As EventArgs) Handles MD.Click
        Dim response As String = InputBox(My.Lang.InputValue, sender.text, My.Lang.ItMustBeNumerical)
        sender.Text = Math.Max(Math.Min(Val(response), XenonTrackbar1.Maximum), XenonTrackbar1.Minimum) : XenonTrackbar1.Value = Val(sender.Text)
    End Sub

    Private Sub XenonTrackbar1_Scroll(sender As Object) Handles XenonTrackbar1.Scroll
        MD.Text = sender.Value
    End Sub

    Private Sub XenonButton4_Click(sender As Object, e As EventArgs) Handles XenonButton4.Click
        Dim response As String = InputBox(My.Lang.InputValue, sender.text, My.Lang.ItMustBeNumerical)
        sender.Text = Math.Max(Math.Min(Val(response), XenonTrackbar5.Maximum), XenonTrackbar5.Minimum) : XenonTrackbar5.Value = Val(sender.Text)
    End Sub

    Private Sub XenonButton1_Click(sender As Object, e As EventArgs) Handles XenonButton1.Click
        Dim response As String = InputBox(My.Lang.InputValue, sender.text, My.Lang.ItMustBeNumerical)
        sender.Text = Math.Max(Math.Min(Val(response), XenonTrackbar2.Maximum), XenonTrackbar2.Minimum) : XenonTrackbar2.Value = Val(sender.Text)
    End Sub

    Private Sub XenonButton2_Click(sender As Object, e As EventArgs) Handles XenonButton2.Click
        Dim response As String = InputBox(My.Lang.InputValue, sender.text, My.Lang.ItMustBeNumerical)
        sender.Text = Math.Max(Math.Min(Val(response), XenonTrackbar3.Maximum), XenonTrackbar3.Minimum) : XenonTrackbar3.Value = Val(sender.Text)
    End Sub

    Private Sub XenonButton3_Click(sender As Object, e As EventArgs) Handles XenonButton3.Click
        Dim response As String = InputBox(My.Lang.InputValue, sender.text, My.Lang.ItMustBeNumerical)
        sender.Text = Math.Max(Math.Min(Val(response), XenonTrackbar4.Maximum), XenonTrackbar4.Minimum) : XenonTrackbar4.Value = Val(sender.Text)
    End Sub

    Private Sub XenonButton5_Click(sender As Object, e As EventArgs) Handles XenonButton5.Click
        Dim response As String = InputBox(My.Lang.InputValue, sender.text, My.Lang.ItMustBeNumerical)
        sender.Text = Math.Max(Math.Min(Val(response), XenonTrackbar6.Maximum), XenonTrackbar6.Minimum) : XenonTrackbar6.Value = Val(sender.Text)
    End Sub

    Private Sub XenonTrackbar5_Scroll(sender As Object) Handles XenonTrackbar5.Scroll
        XenonButton4.Text = sender.Value
    End Sub

    Private Sub XenonTrackbar2_Scroll(sender As Object) Handles XenonTrackbar2.Scroll
        XenonButton1.Text = sender.Value
        RetroButton1.FocusRectWidth = sender.Value : RetroButton1.Refresh()
    End Sub

    Private Sub XenonTrackbar3_Scroll(sender As Object) Handles XenonTrackbar3.Scroll
        XenonButton2.Text = sender.Value
        RetroButton1.FocusRectHeight = sender.Value : RetroButton1.Refresh()
    End Sub

    Private Sub XenonTrackbar4_Scroll(sender As Object) Handles XenonTrackbar4.Scroll
        XenonButton3.Text = sender.Value
        Panel2.Width = sender.Value
    End Sub

    Private Sub XenonTrackbar6_Scroll(sender As Object) Handles XenonTrackbar6.Scroll
        XenonButton5.Text = sender.Value
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Panel2.Visible = Not Panel2.Visible
    End Sub

    Private Sub XenonRadioImage1_CheckedChanged(sender As Object) Handles XenonRadioImage1.CheckedChanged
        If sender.Checked Then
            PictureBox33.Image = My.Resources.CF_Img_Normal
            PictureBox32.Image = My.Resources.CF_Pie_Normal

            R1.BackColor = Color.FromArgb(204, 50, 47)
            R2.BackColor = Color.FromArgb(233, 80, 63)
            R3.BackColor = Color.FromArgb(239, 142, 133)

            O1.BackColor = Color.FromArgb(220, 96, 44)
            O2.BackColor = Color.FromArgb(239, 153, 58)
            O3.BackColor = Color.FromArgb(247, 193, 114)

            Y1.BackColor = Color.FromArgb(231, 181, 64)
            Y2.BackColor = Color.FromArgb(248, 205, 72)
            Y3.BackColor = Color.FromArgb(250, 224, 121)

            G1.BackColor = Color.FromArgb(57, 122, 47)
            G2.BackColor = Color.FromArgb(117, 213, 113)
            G3.BackColor = Color.FromArgb(163, 228, 166)

            B1.BackColor = Color.FromArgb(29, 65, 211)
            B2.BackColor = Color.FromArgb(55, 119, 245)
            B3.BackColor = Color.FromArgb(118, 170, 248)

            P1.BackColor = Color.FromArgb(165, 39, 200)
            P2.BackColor = Color.FromArgb(195, 156, 219)
            P3.BackColor = Color.FromArgb(217, 195, 233)
        End If
    End Sub

    Private Sub XenonRadioImage5_CheckedChanged(sender As Object) Handles XenonRadioImage5.CheckedChanged
        If sender.Checked Then
            PictureBox33.Image = My.Resources.CF_Img_Grayscale
            PictureBox32.Image = My.Resources.CF_Pie_Grayscale

            R1.BackColor = Color.FromArgb(93, 93, 93)
            R2.BackColor = Color.FromArgb(122, 122, 122)
            R3.BackColor = Color.FromArgb(169, 169, 169)

            O1.BackColor = Color.FromArgb(126, 126, 126)
            O2.BackColor = Color.FromArgb(166, 166, 166)
            O3.BackColor = Color.FromArgb(200, 200, 200)

            Y1.BackColor = Color.FromArgb(183, 182, 183)
            Y2.BackColor = Color.FromArgb(202, 202, 202)
            Y3.BackColor = Color.FromArgb(220, 220, 220)

            G1.BackColor = Color.FromArgb(93, 93, 93)
            G2.BackColor = Color.FromArgb(172, 172, 172)
            G3.BackColor = Color.FromArgb(202, 202, 202)

            B1.BackColor = Color.FromArgb(70, 70, 70)
            B2.BackColor = Color.FromArgb(113, 113, 113)
            B3.BackColor = Color.FromArgb(163, 163, 163)

            P1.BackColor = Color.FromArgb(93, 93, 93)
            P2.BackColor = Color.FromArgb(175, 174, 175)
            P3.BackColor = Color.FromArgb(206, 206, 206)
        End If
    End Sub

    Private Sub XenonRadioImage7_CheckedChanged(sender As Object) Handles XenonRadioImage7.CheckedChanged
        If sender.Checked Then
            PictureBox33.Image = My.Resources.CF_Img_Normal.Invert
            PictureBox32.Image = My.Resources.CF_Pie_Normal.Invert

            R1.BackColor = Color.FromArgb(53, 208, 211)
            R2.BackColor = Color.FromArgb(28, 174, 193)
            R3.BackColor = Color.FromArgb(22, 111, 120)

            O1.BackColor = Color.FromArgb(39, 158, 214)
            O2.BackColor = Color.FromArgb(22, 101, 199)
            O3.BackColor = Color.FromArgb(16, 63, 139)

            Y1.BackColor = Color.FromArgb(29, 73, 192)
            Y2.BackColor = Color.FromArgb(15, 52, 184)
            Y3.BackColor = Color.FromArgb(13, 35, 132)

            G1.BackColor = Color.FromArgb(200, 131, 211)
            G2.BackColor = Color.FromArgb(136, 45, 140)
            G3.BackColor = Color.FromArgb(90, 32, 88)

            B1.BackColor = Color.FromArgb(231, 191, 47)
            B2.BackColor = Color.FromArgb(202, 134, 18)
            B3.BackColor = Color.FromArgb(135, 84, 15)

            P1.BackColor = Color.FromArgb(89, 220, 57)
            P2.BackColor = Color.FromArgb(61, 98, 40)
            P3.BackColor = Color.FromArgb(42, 61, 28)
        End If
    End Sub

    Private Sub XenonRadioImage6_CheckedChanged(sender As Object) Handles XenonRadioImage6.CheckedChanged
        If sender.Checked Then
            PictureBox33.Image = My.Resources.CF_Img_Grayscale.Invert
            PictureBox32.Image = My.Resources.CF_Pie_Grayscale.Invert

            R1.BackColor = Color.FromArgb(160, 160, 160)
            R2.BackColor = Color.FromArgb(131, 131, 131)
            R3.BackColor = Color.FromArgb(85, 85, 85)

            O1.BackColor = Color.FromArgb(127, 127, 127)
            O2.BackColor = Color.FromArgb(88, 88, 88)
            O3.BackColor = Color.FromArgb(57, 57, 57)

            Y1.BackColor = Color.FromArgb(73, 73, 73)
            Y2.BackColor = Color.FromArgb(55, 55, 55)
            Y3.BackColor = Color.FromArgb(39, 39, 39)

            G1.BackColor = Color.FromArgb(160, 160, 160)
            G2.BackColor = Color.FromArgb(82, 82, 82)
            G3.BackColor = Color.FromArgb(55, 55, 55)

            B1.BackColor = Color.FromArgb(186, 186, 186)
            B2.BackColor = Color.FromArgb(140, 140, 140)
            B3.BackColor = Color.FromArgb(90, 90, 90)

            P1.BackColor = Color.FromArgb(160, 160, 160)
            P2.BackColor = Color.FromArgb(80, 80, 80)
            P3.BackColor = Color.FromArgb(51, 51, 51)
        End If
    End Sub

    Private Sub XenonRadioImage2_CheckedChanged(sender As Object) Handles XenonRadioImage2.CheckedChanged
        If sender.Checked Then
            PictureBox33.Image = My.Resources.CF_Img_Red_green_green_weak_deuteranopia
            PictureBox32.Image = My.Resources.CF_Pie_Red_green_green_weak_deuteranopia

            R1.BackColor = Color.FromArgb(255, 50, 20)
            R2.BackColor = Color.FromArgb(255, 80, 35)
            R3.BackColor = Color.FromArgb(255, 142, 113)

            O1.BackColor = Color.FromArgb(255, 96, 22)
            O2.BackColor = Color.FromArgb(255, 153, 42)
            O3.BackColor = Color.FromArgb(255, 193, 103)

            Y1.BackColor = Color.FromArgb(229, 181, 54)
            Y2.BackColor = Color.FromArgb(239, 205, 62)
            Y3.BackColor = Color.FromArgb(241, 224, 114)

            G1.BackColor = Color.FromArgb(16, 122, 58)
            G2.BackColor = Color.FromArgb(60, 213, 130)
            G3.BackColor = Color.FromArgb(124, 228, 176)

            B1.BackColor = Color.FromArgb(40, 65, 218)
            B2.BackColor = Color.FromArgb(50, 119, 255)
            B3.BackColor = Color.FromArgb(111, 170, 255)

            P1.BackColor = Color.FromArgb(255, 39, 172)
            P2.BackColor = Color.FromArgb(224, 156, 210)
            P3.BackColor = Color.FromArgb(234, 195, 227)
        End If
    End Sub

    Private Sub XenonRadioImage3_CheckedChanged(sender As Object) Handles XenonRadioImage3.CheckedChanged
        If sender.Checked Then
            PictureBox33.Image = My.Resources.CF_Img_Red_green_red_weak_protanopia
            PictureBox32.Image = My.Resources.CF_Pie_Red_green_red_weak_protanopia

            R1.BackColor = Color.FromArgb(204, 121, 137)
            R2.BackColor = Color.FromArgb(233, 151, 151)
            R3.BackColor = Color.FromArgb(239, 188, 190)

            O1.BackColor = Color.FromArgb(220, 152, 110)
            O2.BackColor = Color.FromArgb(239, 190, 97)
            O3.BackColor = Color.FromArgb(247, 215, 138)

            Y1.BackColor = Color.FromArgb(231, 200, 81)
            Y2.BackColor = Color.FromArgb(248, 219, 84)
            Y3.BackColor = Color.FromArgb(250, 231, 126)

            G1.BackColor = Color.FromArgb(57, 87, 0)
            G2.BackColor = Color.FromArgb(117, 162, 51)
            G3.BackColor = Color.FromArgb(163, 195, 123)

            B1.BackColor = Color.FromArgb(29, 53, 201)
            B2.BackColor = Color.FromArgb(55, 93, 215)
            B3.BackColor = Color.FromArgb(118, 149, 222)

            P1.BackColor = Color.FromArgb(165, 105, 255)
            P2.BackColor = Color.FromArgb(195, 176, 249)
            P3.BackColor = Color.FromArgb(217, 207, 251)
        End If
    End Sub

    Private Sub XenonRadioImage4_CheckedChanged(sender As Object) Handles XenonRadioImage4.CheckedChanged
        If sender.Checked Then
            PictureBox33.Image = My.Resources.CF_Img_Blue_yellow_tritanopia
            PictureBox32.Image = My.Resources.CF_Pie_Blue_yellow__tritanopia

            R1.BackColor = Color.FromArgb(160, 60, 47)
            R2.BackColor = Color.FromArgb(180, 85, 63)
            R3.BackColor = Color.FromArgb(208, 146, 133)

            O1.BackColor = Color.FromArgb(150, 87, 44)
            O2.BackColor = Color.FromArgb(151, 126, 58)
            O3.BackColor = Color.FromArgb(179, 169, 114)

            Y1.BackColor = Color.FromArgb(138, 145, 64)
            Y2.BackColor = Color.FromArgb(145, 161, 72)
            Y3.BackColor = Color.FromArgb(174, 191, 121)

            G1.BackColor = Color.FromArgb(26, 91, 47)
            G2.BackColor = Color.FromArgb(77, 171, 113)
            G3.BackColor = Color.FromArgb(140, 202, 166)

            B1.BackColor = Color.FromArgb(132, 110, 211)
            B2.BackColor = Color.FromArgb(152, 156, 245)
            B3.BackColor = Color.FromArgb(181, 193, 248)

            P1.BackColor = Color.FromArgb(244, 101, 200)
            P2.BackColor = Color.FromArgb(227, 179, 219)
            P3.BackColor = Color.FromArgb(237, 210, 233)
        End If
    End Sub
End Class