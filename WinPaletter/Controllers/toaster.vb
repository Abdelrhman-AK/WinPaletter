Imports WinPaletter.NativeMethods
Imports WinPaletter.XenonCore

Public Class toaster

    Private _speed As Integer = 100

    Private Sub toaster_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Top = MainFrm.Bottom - Height - 20

        ApplyDarkMode(Me)
        Acrylism.EnableBlur(Me)

        User32.AnimateWindow(Handle, _speed, User32.AnimateWindowFlags.AW_ACTIVATE Or User32.AnimateWindowFlags.AW_SLIDE)
        Invalidate()
    End Sub

    Private Sub SubMenu_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        User32.AnimateWindow(Handle, _Speed, User32.AnimateWindowFlags.AW_HIDE Or User32.AnimateWindowFlags.AW_BLEND)
    End Sub

    Private Sub toaster_Click(sender As Object, e As EventArgs) Handles Me.Click
        Me.Close()
    End Sub

    Private Sub toaster_Shown(sender As Object, e As EventArgs) Handles Me.Shown

    End Sub
End Class