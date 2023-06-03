Imports WinPaletter.PreviewHelpers
Imports WinPaletter.XenonCore

Public Class AltTabEditor
    Private Sub AltTabEditor_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ApplyDarkMode(Me)
        XenonButton12.Image = MainFrm.XenonButton20.Image.Resize(16, 16)
        ApplyFromCP(My.CP)

        Select Case My.PreviewStyle
            Case WindowStyle.W11
                XenonRadioImage1.Image = My.Resources.Native11

            Case WindowStyle.W10
                XenonRadioImage1.Image = My.Resources.Native10

            Case WindowStyle.W8
                XenonRadioImage1.Image = My.Resources.Native8

            Case WindowStyle.W7
                XenonRadioImage1.Image = My.Resources.Native7

            Case WindowStyle.WVista
                XenonRadioImage1.Image = My.Resources.NativeVista

            Case Else
                XenonRadioImage1.Image = My.Resources.Native11

        End Select

        XenonRadioImage2.Image = My.Resources.NativeXP

        pnl_preview1.BackgroundImage = MainFrm.pnl_preview.BackgroundImage
        Classic_Preview1.BackgroundImage = MainFrm.pnl_preview_classic.BackgroundImage

        SetClassicRaisedPanelColors(My.CP, RetroPanelRaised1)
        SetClassicPanelColors(My.CP, RetroPanel1)

        Panel1.BackColor = My.CP.Win32.Hilight

        Select Case My.PreviewStyle
            Case WindowStyle.W11
                XenonWinElement1.Style = XenonWinElement.Styles.AltTab11
                XenonWinElement1.DarkMode = Not My.CP.Windows11.WinMode_Light

            Case WindowStyle.W10
                XenonWinElement1.Style = XenonWinElement.Styles.AltTab10
                XenonWinElement1.DarkMode = Not My.CP.Windows10.WinMode_Light

            Case WindowStyle.W8
                Select Case My.CP.Windows8.Theme
                    Case CP.Structures.Windows7.Themes.Aero
                        XenonWinElement1.Style = XenonWinElement.Styles.AltTab8Aero
                        XenonWinElement1.BackColor = My.CP.Windows8.PersonalColors_Background
                        XenonWinElement1.BackColor2 = My.CP.Windows8.PersonalColors_Background

                    Case CP.Structures.Windows7.Themes.AeroLite
                        XenonWinElement1.Style = XenonWinElement.Styles.AltTab8AeroLite
                        XenonWinElement1.BackColor = My.CP.Win32.Window
                        XenonWinElement1.BackColor2 = My.CP.Win32.Hilight
                        XenonWinElement1.LinkColor = My.CP.Win32.ButtonText
                        XenonWinElement1.ForeColor = My.CP.Win32.WindowText

                End Select

            Case WindowStyle.W7
                Select Case My.CP.Windows7.Theme
                    Case CP.Structures.Windows7.Themes.Aero
                        XenonWinElement1.Style = XenonWinElement.Styles.AltTab7Aero

                    Case CP.Structures.Windows7.Themes.AeroOpaque
                        XenonWinElement1.Style = XenonWinElement.Styles.AltTab7Opaque

                    Case CP.Structures.Windows7.Themes.Basic
                        XenonWinElement1.Style = XenonWinElement.Styles.AltTab7Basic

                End Select

                XenonWinElement1.BackColor = My.CP.Windows7.ColorizationColor
                XenonWinElement1.BackColor2 = My.CP.Windows7.ColorizationAfterglow
                XenonWinElement1.BackColorAlpha = My.CP.Windows7.ColorizationBlurBalance
                XenonWinElement1.Win7ColorBal = My.CP.Windows7.ColorizationColorBalance
                XenonWinElement1.Win7GlowBal = My.CP.Windows7.ColorizationAfterglowBalance
                XenonWinElement1.NoisePower = My.CP.Windows7.ColorizationGlassReflectionIntensity
                XenonWinElement1.Shadow = My.CP.WindowsEffects.WindowShadow
        End Select

        Panel2.BackColor = RetroPanelRaised1.BackColor
        RetroLabel1.Font = My.CP.MetricsFonts.CaptionFont

        XenonGroupBox4.Enabled = (XenonWinElement1.Style = XenonWinElement.Styles.AltTab10) Or ExplorerPatcher.IsAllowed
        XenonAlertBox1.Visible = (My.PreviewStyle = WindowStyle.W7)

        If ExplorerPatcher.IsAllowed Then
            Try
                If My.Computer.Registry.CurrentUser.GetValue("Software\Microsoft\Windows\CurrentVersion\Explorer", "AltTabSettings", 0) = 3 Then
                    XenonWinElement1.Style = XenonWinElement.Styles.AltTab10
                    XenonWinElement1.DarkMode = Not My.CP.Windows11.WinMode_Light
                End If
            Finally
                My.Computer.Registry.CurrentUser.Close()
            End Try
        End If

        If XenonWinElement1.Style = XenonWinElement.Styles.AltTab7Basic Then
            XenonWinElement1.Size = New Size(360, 100)
        Else
            XenonWinElement1.Size = New Size(450, 150)
        End If

        XenonWinElement1.Left = (XenonWinElement1.Parent.Width - XenonWinElement1.Width) / 2
        XenonWinElement1.Top = (XenonWinElement1.Parent.Height - XenonWinElement1.Height) / 2

        tabs_preview_1.DoubleBuffer
    End Sub

    Sub ApplyFromCP(CP As CP)
        With CP.AltTab
            AltTabEnabled.Checked = .Enabled
            XenonRadioImage1.Checked = .Style = CP.Structures.AltTab.Styles.Default Or .Style = CP.Structures.AltTab.Styles.EP_Win10
            XenonRadioImage2.Checked = .Style = CP.Structures.AltTab.Styles.ClassicNT
            XenonTrackbar1.Value = .Win10Opacity
        End With
    End Sub

    Sub ApplyToCP(CP As CP)
        With CP.AltTab
            .Enabled = AltTabEnabled.Checked
            .Style = If(XenonRadioImage1.Checked, CP.Structures.AltTab.Styles.Default, CP.Structures.AltTab.Styles.ClassicNT)
            If ExplorerPatcher.IsAllowed And XenonWinElement1.Style = XenonWinElement.Styles.AltTab10 Then .Style = CP.Structures.AltTab.Styles.EP_Win10
            .Win10Opacity = XenonTrackbar1.Value
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
        CPx.AltTab.Apply()
        CPx.Dispose()
        Cursor = Cursors.Default
    End Sub

    Private Sub XenonButton8_Click(sender As Object, e As EventArgs) Handles XenonButton8.Click
        ApplyToCP(My.CP)
        Me.Close()
    End Sub

    Private Sub AltTabEnabled_CheckedChanged(sender As Object, e As EventArgs) Handles AltTabEnabled.CheckedChanged
        checker_img.Image = If(sender.Checked, My.Resources.checker_enabled, My.Resources.checker_disabled)
    End Sub

    Private Sub Opacity_btn_Click(sender As Object, e As EventArgs) Handles opacity_btn.Click
        Dim response As String = InputBox(My.Lang.InputValue, sender.text, My.Lang.ItMustBeNumerical)
        sender.Text = Math.Max(Math.Min(Val(response), XenonTrackbar1.Maximum), XenonTrackbar1.Minimum) : XenonTrackbar1.Value = Val(sender.Text)
    End Sub

    Private Sub XenonTrackbar1_Scroll(sender As Object) Handles XenonTrackbar1.Scroll
        opacity_btn.Text = sender.Value.ToString
        If XenonWinElement1.Style = XenonWinElement.Styles.AltTab10 Then XenonWinElement1.BackColorAlpha = XenonTrackbar1.Value
    End Sub

    Private Sub XenonRadioImage2_CheckedChanged(sender As Object) Handles XenonRadioImage2.CheckedChanged
        If XenonRadioImage2.Checked Then tabs_preview_1.SelectedIndex = 1
    End Sub

    Private Sub XenonRadioImage1_CheckedChanged(sender As Object) Handles XenonRadioImage1.CheckedChanged
        If XenonRadioImage1.Checked Then tabs_preview_1.SelectedIndex = 0
    End Sub
End Class