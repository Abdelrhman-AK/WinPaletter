Imports System.ComponentModel
Imports WinPaletter.PreviewHelpers

Public Class AltTabEditor
    Private Sub AltTabEditor_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadLanguage
        ApplyStyle(Me)
        Button12.Image = MainFrm.Button20.Image.Resize(16, 16)
        ApplyFromCP(My.CP)

        Select Case My.PreviewStyle
            Case WindowStyle.W11
                RadioImage1.Image = My.Resources.Native11

            Case WindowStyle.W10
                RadioImage1.Image = My.Resources.Native10

            Case WindowStyle.W81
                RadioImage1.Image = My.Resources.Native8

            Case WindowStyle.W7
                RadioImage1.Image = My.Resources.Native7

            Case WindowStyle.WVista
                RadioImage1.Image = My.Resources.NativeVista

            Case Else
                RadioImage1.Image = My.Resources.Native11

        End Select

        RadioImage2.Image = My.Resources.NativeXP

        pnl_preview1.BackgroundImage = MainFrm.pnl_preview.BackgroundImage
        Classic_Preview1.BackgroundImage = MainFrm.pnl_preview_classic.BackgroundImage

        SetClassicPanelRaisedRColors(My.CP, PanelRRaised1)
        SetClassicPanelColors(My.CP, PanelR1)

        Panel1.BackColor = My.CP.Win32.Hilight

        Select Case My.PreviewStyle
            Case WindowStyle.W11
                WinElement1.Style = UI.Simulation.WinElement.Styles.AltTab11
                WinElement1.DarkMode = Not My.CP.Windows11.WinMode_Light

            Case WindowStyle.W10
                WinElement1.Style = UI.Simulation.WinElement.Styles.AltTab10
                WinElement1.DarkMode = Not My.CP.Windows10.WinMode_Light

            Case WindowStyle.W81
                Select Case My.CP.Windows81.Theme
                    Case CP.Structures.Windows7.Themes.Aero
                        WinElement1.Style = UI.Simulation.WinElement.Styles.AltTab8Aero
                        WinElement1.BackColor = My.CP.Windows81.PersonalColors_Background
                        WinElement1.Background2 = My.CP.Windows81.PersonalColors_Background

                    Case CP.Structures.Windows7.Themes.AeroLite
                        WinElement1.Style = UI.Simulation.WinElement.Styles.AltTab8AeroLite
                        WinElement1.BackColor = My.CP.Win32.Window
                        WinElement1.Background2 = My.CP.Win32.Hilight
                        WinElement1.LinkColor = My.CP.Win32.ButtonText
                        WinElement1.ForeColor = My.CP.Win32.WindowText

                End Select

            Case WindowStyle.W7
                Select Case My.CP.Windows7.Theme
                    Case CP.Structures.Windows7.Themes.Aero
                        WinElement1.Style = UI.Simulation.WinElement.Styles.AltTab7Aero

                    Case CP.Structures.Windows7.Themes.AeroOpaque
                        WinElement1.Style = UI.Simulation.WinElement.Styles.AltTab7Opaque

                    Case CP.Structures.Windows7.Themes.Basic
                        WinElement1.Style = UI.Simulation.WinElement.Styles.AltTab7Basic

                End Select

                WinElement1.BackColor = My.CP.Windows7.ColorizationColor
                WinElement1.Background2 = My.CP.Windows7.ColorizationAfterglow
                WinElement1.BackColorAlpha = My.CP.Windows7.ColorizationBlurBalance
                WinElement1.Win7ColorBal = My.CP.Windows7.ColorizationColorBalance
                WinElement1.Win7GlowBal = My.CP.Windows7.ColorizationAfterglowBalance
                WinElement1.NoisePower = My.CP.Windows7.ColorizationGlassReflectionIntensity
                WinElement1.Shadow = My.CP.WindowsEffects.WindowShadow
        End Select

        Panel2.BackColor = PanelRRaised1.BackColor
        LabelR1.Font = My.CP.MetricsFonts.CaptionFont

        GroupBox4.Enabled = (WinElement1.Style = UI.Simulation.WinElement.Styles.AltTab10) Or ExplorerPatcher.IsAllowed
        AlertBox1.Visible = (My.PreviewStyle = WindowStyle.W7)

        If ExplorerPatcher.IsAllowed Then
            Try
                If My.Computer.Registry.CurrentUser.GetValue("Software\Microsoft\Windows\CurrentVersion\Explorer\AltTabSettings", 0) = 3 Then
                    WinElement1.Style = UI.Simulation.WinElement.Styles.AltTab10
                    WinElement1.DarkMode = Not My.CP.Windows11.WinMode_Light
                End If
            Finally
                My.Computer.Registry.CurrentUser.Close()
            End Try
        End If

        If WinElement1.Style = UI.Simulation.WinElement.Styles.AltTab7Basic Then
            WinElement1.Size = New Size(360, 100)
        Else
            WinElement1.Size = New Size(450, 150)
        End If

        WinElement1.Left = (WinElement1.Parent.Width - WinElement1.Width) / 2
        WinElement1.Top = (WinElement1.Parent.Height - WinElement1.Height) / 2

        tabs_preview_1.DoubleBuffer
    End Sub

    Sub ApplyFromCP(CP As CP)
        With CP.AltTab
            AltTabEnabled.Checked = .Enabled
            RadioImage1.Checked = .Style = CP.Structures.AltTab.Styles.Default Or .Style = CP.Structures.AltTab.Styles.EP_Win10
            RadioImage2.Checked = .Style = CP.Structures.AltTab.Styles.ClassicNT
            Trackbar1.Value = .Win10Opacity
        End With
    End Sub

    Sub ApplyToCP(CP As CP)
        With CP.AltTab
            .Enabled = AltTabEnabled.Checked
            .Style = If(RadioImage1.Checked, CP.Structures.AltTab.Styles.Default, CP.Structures.AltTab.Styles.ClassicNT)
            If ExplorerPatcher.IsAllowed And WinElement1.Style = UI.Simulation.WinElement.Styles.AltTab10 Then .Style = CP.Structures.AltTab.Styles.EP_Win10
            .Win10Opacity = Trackbar1.Value
        End With
    End Sub

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

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Me.Close()
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        Cursor = Cursors.WaitCursor
        Dim CPx As New CP(CP.CP_Type.Registry)
        ApplyToCP(CPx)
        ApplyToCP(My.CP)
        CPx.AltTab.Apply()
        CPx.Dispose()
        Cursor = Cursors.Default
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        ApplyToCP(My.CP)
        Me.Close()
    End Sub

    Private Sub AltTabEnabled_CheckedChanged(sender As Object, e As EventArgs) Handles AltTabEnabled.CheckedChanged
        checker_img.Image = If(sender.Checked, My.Resources.checker_enabled, My.Resources.checker_disabled)
    End Sub

    Private Sub Opacity_btn_Click(sender As Object, e As EventArgs) Handles opacity_btn.Click
        Dim response As String = InputBox(My.Lang.InputValue, sender.text, My.Lang.ItMustBeNumerical)
        sender.Text = Math.Max(Math.Min(Val(response), Trackbar1.Maximum), Trackbar1.Minimum) : Trackbar1.Value = Val(sender.Text)
    End Sub

    Private Sub Trackbar1_Scroll(sender As Object) Handles Trackbar1.Scroll
        opacity_btn.Text = sender.Value.ToString
        If WinElement1.Style = UI.Simulation.WinElement.Styles.AltTab10 Then WinElement1.BackColorAlpha = Trackbar1.Value
    End Sub

    Private Sub RadioImage2_CheckedChanged(sender As Object) Handles RadioImage2.CheckedChanged
        If RadioImage2.Checked Then tabs_preview_1.SelectedIndex = 1
    End Sub

    Private Sub RadioImage1_CheckedChanged(sender As Object) Handles RadioImage1.CheckedChanged
        If RadioImage1.Checked Then tabs_preview_1.SelectedIndex = 0
    End Sub

    Private Sub Form_HelpButtonClicked(sender As Object, e As CancelEventArgs) Handles Me.HelpButtonClicked
        Process.Start(My.Resources.Link_Wiki & "/Edit-Windows-switcher-(Alt-Tab-appearance)")
    End Sub
End Class