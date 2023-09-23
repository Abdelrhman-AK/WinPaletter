Public Class Store_CPToggles
    Public CP As CP

    Private Sub Store_CPToggles_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadLanguage
        ApplyStyle(Me)
        Opacity = 0
        Icon = Store.Icon

        CheckedListBox1.Items.Clear()

        If CP.AppTheme.Enabled Then CheckedListBox1.Items.Add(My.Lang.Store_Toggle_AppTheme, True)
        If CP.LogonUI7.Enabled And (My.W7 Or My.W8 Or My.W81) Then CheckedListBox1.Items.Add(My.Lang.Store_Toggle_LogonUI, True)
        If CP.LogonUIXP.Enabled And My.WXP Then CheckedListBox1.Items.Add(My.Lang.Store_Toggle_LogonUI, True)
        If CP.Cursor_Enabled Then CheckedListBox1.Items.Add(My.Lang.Store_Toggle_Cursors, True)
        If CP.Wallpaper.Enabled Then CheckedListBox1.Items.Add(My.Lang.Store_Toggle_Wallpaper, True)
        If CP.Sounds.Enabled Then CheckedListBox1.Items.Add(My.Lang.Store_Toggle_Sounds, True)
        If CP.ScreenSaver.Enabled Then CheckedListBox1.Items.Add(My.Lang.Store_Toggle_ScreenSaver, True)
        If CP.MetricsFonts.Enabled Then CheckedListBox1.Items.Add(My.Lang.Store_Toggle_MetricsFonts, True)
        If CP.CommandPrompt.Enabled Then CheckedListBox1.Items.Add(My.Lang.Store_Toggle_CMD, True)
        If CP.PowerShellx86.Enabled Then CheckedListBox1.Items.Add(My.Lang.Store_Toggle_PS86, True)
        If CP.PowerShellx64.Enabled Then CheckedListBox1.Items.Add(My.Lang.Store_Toggle_PS64, True)
        If CP.Terminal.Enabled Then CheckedListBox1.Items.Add(My.Lang.Store_Toggle_TerminalStable, True)
        If CP.TerminalPreview.Enabled Then CheckedListBox1.Items.Add(My.Lang.Store_Toggle_TerminalPreview, True)
        If CP.WindowsEffects.Enabled Then CheckedListBox1.Items.Add(My.Lang.Store_Toggle_WindowsEffects, True)
        If CP.AltTab.Enabled Then CheckedListBox1.Items.Add(My.Lang.Store_Toggle_AltTab, True)

        If CheckedListBox1.Items.Count = 0 Then Close()
        Opacity = 1

        CheckedListBox1.ForeColor = If(My.Style.DarkMode, Color.White, Color.Black)

        My.Computer.Audio.PlaySystemSound(Media.SystemSounds.Exclamation)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        For i = 0 To CheckedListBox1.Items.Count - 1
            If CheckedListBox1.Items.Item(i) = My.Lang.Store_Toggle_AppTheme Then CP.AppTheme.Enabled = CheckedListBox1.GetItemChecked(i)

            If CheckedListBox1.Items.Item(i) = My.Lang.Store_Toggle_LogonUI Then
                If My.W7 Or My.W8 Or My.W81 Then
                    CP.LogonUI7.Enabled = CheckedListBox1.GetItemChecked(i)
                ElseIf My.WXP Then
                    CP.LogonUIXP.Enabled = CheckedListBox1.GetItemChecked(i)
                End If
            End If

            If CheckedListBox1.Items.Item(i) = My.Lang.Store_Toggle_Cursors Then CP.Cursor_Enabled = CheckedListBox1.GetItemChecked(i)
            If CheckedListBox1.Items.Item(i) = My.Lang.Store_Toggle_CMD Then CP.CommandPrompt.Enabled = CheckedListBox1.GetItemChecked(i)
            If CheckedListBox1.Items.Item(i) = My.Lang.Store_Toggle_PS86 Then CP.PowerShellx86.Enabled = CheckedListBox1.GetItemChecked(i)
            If CheckedListBox1.Items.Item(i) = My.Lang.Store_Toggle_PS64 Then CP.PowerShellx64.Enabled = CheckedListBox1.GetItemChecked(i)
            If CheckedListBox1.Items.Item(i) = My.Lang.Store_Toggle_TerminalStable Then CP.Terminal.Enabled = CheckedListBox1.GetItemChecked(i)
            If CheckedListBox1.Items.Item(i) = My.Lang.Store_Toggle_TerminalPreview Then CP.TerminalPreview.Enabled = CheckedListBox1.GetItemChecked(i)
            If CheckedListBox1.Items.Item(i) = My.Lang.Store_Toggle_MetricsFonts Then CP.MetricsFonts.Enabled = CheckedListBox1.GetItemChecked(i)
            If CheckedListBox1.Items.Item(i) = My.Lang.Store_Toggle_Wallpaper Then CP.Wallpaper.Enabled = CheckedListBox1.GetItemChecked(i)
            If CheckedListBox1.Items.Item(i) = My.Lang.Store_Toggle_WindowsEffects Then CP.WindowsEffects.Enabled = CheckedListBox1.GetItemChecked(i)
            If CheckedListBox1.Items.Item(i) = My.Lang.Store_Toggle_AltTab Then CP.AltTab.Enabled = CheckedListBox1.GetItemChecked(i)
        Next

        Store.selectedItem.CP = CP
        DialogResult = DialogResult.OK
        Close()
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        DialogResult = DialogResult.OK
        Close()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        DialogResult = DialogResult.Cancel
        Close()
    End Sub
End Class