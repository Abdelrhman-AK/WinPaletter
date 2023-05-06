Imports WinPaletter.XenonCore
Imports WinPaletter.PreviewHelpers

Public Class LogonUI
    Private Sub LogonUI_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ApplyDarkMode(Me)
        Load_FromCP(MainFrm.CP)
        XenonButton12.Image = MainFrm.XenonButton20.Image.Resize(16, 16)
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

    Private Sub XenonButton2_Click(sender As Object, e As EventArgs) Handles XenonButton2.Click
        Me.Close()
    End Sub

    Private Sub XenonButton11_Click(sender As Object, e As EventArgs) Handles XenonButton11.Click
        If OpenFileDialog1.ShowDialog = DialogResult.OK Then
            Dim CPx As New CP(CP.CP_Type.File, OpenFileDialog1.FileName)
            Load_FromCP(CPx)
            CPx.Dispose()
        End If
    End Sub

    Private Sub XenonButton9_Click(sender As Object, e As EventArgs) Handles XenonButton9.Click
        Dim CPx As New CP(CP.CP_Type.Registry)
        Load_FromCP(CPx)
        CPx.Dispose()
    End Sub

    Private Sub XenonButton12_Click(sender As Object, e As EventArgs) Handles XenonButton12.Click
        Dim CPx As CP
        Select Case My.PreviewStyle
            Case WindowStyle.W11
                CPx = New CP_Defaults().Default_Windows11
            Case WindowStyle.W10
                CPx = New CP_Defaults().Default_Windows10
            Case WindowStyle.W8
                CPx = New CP_Defaults().Default_Windows8
            Case WindowStyle.W7
                CPx = New CP_Defaults().Default_Windows7
            Case WindowStyle.WVista
                CPx = New CP_Defaults().Default_WindowsVista
            Case WindowStyle.WXP
                CPx = New CP_Defaults().Default_WindowsXP
            Case Else
                CPx = New CP_Defaults().Default_Windows11
        End Select
        Load_FromCP(CPx)
        CPx.Dispose()
    End Sub

    Private Sub XenonButton1_Click(sender As Object, e As EventArgs) Handles XenonButton1.Click
        Save(MainFrm.CP)
        Me.Close()
    End Sub
End Class