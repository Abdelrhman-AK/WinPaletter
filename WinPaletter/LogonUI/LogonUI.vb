Imports WinPaletter.XenonCore

Public Class LogonUI
    Private Sub LogonUI_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ApplyDarkMode(Me)
        Load_FromCP(MainFrm.CP)
    End Sub

    Sub Load_FromCP(ByVal ColorPalette As CP)
        LogonUI_Acrylic_Toggle.Checked = Not ColorPalette.LogonUI10x.DisableAcrylicBackgroundOnLogon
        LogonUI_Background_Toggle.Checked = Not ColorPalette.LogonUI10x.DisableLogonBackgroundImage
        LogonUI_Lockscreen_Toggle.Checked = Not ColorPalette.LogonUI10x.NoLockScreen
    End Sub

    Sub Save(ByVal ColorPalette As CP)
        ColorPalette.LogonUI10x.DisableAcrylicBackgroundOnLogon = Not LogonUI_Acrylic_Toggle.Checked
        ColorPalette.LogonUI10x.DisableLogonBackgroundImage = Not LogonUI_Background_Toggle.Checked
        ColorPalette.LogonUI10x.NoLockScreen = Not LogonUI_Lockscreen_Toggle.Checked
    End Sub

    Private Sub XenonButton1_Click(sender As Object, e As EventArgs) Handles XenonButton1.Click
        Save(MainFrm.CP)
        Me.Close()
    End Sub

    Private Sub XenonButton2_Click(sender As Object, e As EventArgs) Handles XenonButton2.Click
        Me.Close()
    End Sub
End Class