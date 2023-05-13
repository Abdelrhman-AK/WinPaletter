Imports WinPaletter.XenonCore

Public Class About
    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Process.Start("https://icons8.com/app/windows")
    End Sub

    Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        Process.Start("https://github.com/cyotek/Cyotek.Windows.Forms.ColorPicker")
    End Sub

    Private Sub LinkLabel3_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel3.LinkClicked
        Process.Start("https://github.com/KSemenenko/ColorThief")
    End Sub

    Private Sub XenonButton1_Click(sender As Object, e As EventArgs) Handles XenonButton1.Click
        Process.Start(My.Resources.Link_Repository)
    End Sub

    Private Sub XenonButton2_Click(sender As Object, e As EventArgs) Handles XenonButton2.Click
        Process.Start("https://www.reddit.com/user/abdelrhman_ak")
    End Sub

    Private Sub About_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Icon = MainFrm.Icon
        ApplyDarkMode(Me)
        Label2.Text = My.Application.Info.Version.ToString
    End Sub

    Private Sub XenonButton3_Click(sender As Object, e As EventArgs) Handles XenonButton3.Click
        Close()
    End Sub

    Private Sub LinkLabel4_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel4.LinkClicked
        Process.Start("https://www.codeproject.com/Articles/548769/Animator-for-WinForms")
    End Sub

    Private Sub LinkLabel5_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel5.LinkClicked
        Process.Start("https://www.reddit.com/r/Windows11/comments/sw15u0/dark_theme_did_you_notice_the_ugly_pale_accent")
        Process.Start("https://www.reddit.com/r/Windows11/comments/tkvet4/pitch_black_themereg_now_for_ctrlaltdel_as_well")
    End Sub

    Private Sub LinkLabel7_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel7.LinkClicked
        Process.Start("https://github.com/evanolds/AnimCur")
    End Sub

    Private Sub LinkLabel6_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel6.LinkClicked
        Process.Start("https://www.microsoft.com")
    End Sub

    Private Sub LinkLabel8_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel8.LinkClicked
        Process.Start("https://github.com/JetBrains/JetBrainsMono")
    End Sub

    Private Sub LinkLabel10_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel10.LinkClicked
        Process.Start("https://github.com/JamesNK/Newtonsoft.Json")
    End Sub

    Private Sub XenonLinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles XenonLinkLabel1.LinkClicked
        Process.Start("https://github.com/ookii-dialogs/ookii-dialogs-winforms")
    End Sub

    Private Sub XenonLinkLabel3_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles XenonLinkLabel3.LinkClicked
        Process.Start("https://www.codeproject.com/Articles/18603/Advanced-UxTheme-wrapper")
    End Sub

    Private Sub XenonLinkLabel4_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles XenonLinkLabel4.LinkClicked
        Process.Start("https://imageprocessor.org")
    End Sub

    Private Sub XenonLinkLabel2_LinkClicked_1(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles XenonLinkLabel2.LinkClicked
        Process.Start("https://github.com/evanolds/AnimCur")
    End Sub
End Class