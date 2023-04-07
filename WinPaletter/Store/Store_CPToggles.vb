Imports System.Windows.Media
Imports WinPaletter.XenonCore
Public Class Store_CPToggles
    Public CP As CP

    Private Sub Store_CPToggles_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ApplyDarkMode(Me)
        Opacity = 0

        CheckedListBox1.Items.Clear()

        If CP.LogonUI7.Enabled And (My.W7 Or My.W8) Then CheckedListBox1.Items.Add("LogonUI screen", True)
        If CP.LogonUIXP.Enabled And My.WXP Then CheckedListBox1.Items.Add("LogonUI screen", True)

        If CP.Cursor_Enabled Then CheckedListBox1.Items.Add("Cursors", True)
        If CP.CommandPrompt.Enabled Then CheckedListBox1.Items.Add("Command Prompt", True)
        If CP.PowerShellx86.Enabled Then CheckedListBox1.Items.Add("PowerShell x86", True)
        If CP.PowerShellx64.Enabled Then CheckedListBox1.Items.Add("PowerShell x64", True)
        If CP.Terminal.Enabled Then CheckedListBox1.Items.Add("Windows Terminal Stable", True)
        If CP.TerminalPreview.Enabled Then CheckedListBox1.Items.Add("Windows Terminal Preview", True)
        If CP.MetricsFonts.Enabled Then CheckedListBox1.Items.Add("Metrics & Fonts", True)

        If CP.WallpaperTone_W11.Enabled And My.W11 Then CheckedListBox1.Items.Add("Wallpaper Tone", True)
        If CP.WallpaperTone_W10.Enabled And My.W10 Then CheckedListBox1.Items.Add("Wallpaper Tone", True)
        If CP.WallpaperTone_W8.Enabled And My.W8 Then CheckedListBox1.Items.Add("Wallpaper Tone", True)
        If CP.WallpaperTone_W7.Enabled And My.W7 Then CheckedListBox1.Items.Add("Wallpaper Tone", True)
        If CP.WallpaperTone_WVista.Enabled And My.WVista Then CheckedListBox1.Items.Add("Wallpaper Tone", True)
        If CP.WallpaperTone_WXP.Enabled And My.WXP Then CheckedListBox1.Items.Add("Wallpaper Tone", True)

        If CP.WindowsEffects.Enabled Then CheckedListBox1.Items.Add("Windows Effects", True)
        If CP.AltTab.Enabled Then CheckedListBox1.Items.Add("Windows Switcher (Alt+Tab appearance)", True)

        If CheckedListBox1.Items.Count = 0 Then Close()

        Opacity = 1
    End Sub

    Private Sub XenonButton1_Click(sender As Object, e As EventArgs) Handles XenonButton1.Click
        For i = 0 To CheckedListBox1.Items.Count - 1

            If CheckedListBox1.Items.Item(i) = "LogonUI screen" Then
                If My.W7 Or My.W8 Then
                    CP.LogonUI7.Enabled = CheckedListBox1.GetItemChecked(i)
                ElseIf My.WXP Then
                    CP.LogonUIXP.Enabled = CheckedListBox1.GetItemChecked(i)
                End If
            End If

            If CheckedListBox1.Items.Item(i) = "Cursors" Then CP.Cursor_Enabled = CheckedListBox1.GetItemChecked(i)
            If CheckedListBox1.Items.Item(i) = "Command Prompt" Then CP.CommandPrompt.Enabled = CheckedListBox1.GetItemChecked(i)
            If CheckedListBox1.Items.Item(i) = "PowerShell x86" Then CP.PowerShellx86.Enabled = CheckedListBox1.GetItemChecked(i)
            If CheckedListBox1.Items.Item(i) = "PowerShell x64" Then CP.PowerShellx64.Enabled = CheckedListBox1.GetItemChecked(i)
            If CheckedListBox1.Items.Item(i) = "Windows Terminal Stable" Then CP.Terminal.Enabled = CheckedListBox1.GetItemChecked(i)
            If CheckedListBox1.Items.Item(i) = "Windows Terminal Preview" Then CP.TerminalPreview.Enabled = CheckedListBox1.GetItemChecked(i)
            If CheckedListBox1.Items.Item(i) = "Metrics & Fonts" Then CP.MetricsFonts.Enabled = CheckedListBox1.GetItemChecked(i)

            If CheckedListBox1.Items.Item(i) = "Wallpaper Tone" Then
                If My.W11 Then
                    CP.WallpaperTone_W11.Enabled = CheckedListBox1.GetItemChecked(i)

                ElseIf My.W10 Then
                    CP.WallpaperTone_W10.Enabled = CheckedListBox1.GetItemChecked(i)

                ElseIf My.W8 Then
                    CP.WallpaperTone_W8.Enabled = CheckedListBox1.GetItemChecked(i)

                ElseIf My.W7 Then
                    CP.WallpaperTone_W7.Enabled = CheckedListBox1.GetItemChecked(i)

                ElseIf My.WVista Then
                    CP.WallpaperTone_WVista.Enabled = CheckedListBox1.GetItemChecked(i)

                ElseIf My.WXP Then
                    CP.WallpaperTone_WXP.Enabled = CheckedListBox1.GetItemChecked(i)

                End If
            End If

            If CheckedListBox1.Items.Item(i) = "Windows Effects" Then CP.WindowsEffects.Enabled = CheckedListBox1.GetItemChecked(i)
            If CheckedListBox1.Items.Item(i) = "Windows Switcher (Alt+Tab appearance)" Then CP.AltTab.Enabled = CheckedListBox1.GetItemChecked(i)

        Next

        Store.selectedItem.CP = CP
        Close()
    End Sub

    Private Sub XenonButton7_Click(sender As Object, e As EventArgs) Handles XenonButton7.Click
        Close()
    End Sub
End Class