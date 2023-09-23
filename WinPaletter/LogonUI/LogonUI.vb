Imports System.ComponentModel
Imports WinPaletter.PreviewHelpers

Public Class LogonUI
    Private Sub LogonUI_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadLanguage
        ApplyStyle(Me)
        Load_FromCP(My.CP)
        Button12.Image = MainFrm.Button20.Image.Resize(16, 16)
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

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        If OpenFileDialog1.ShowDialog = DialogResult.OK Then
            Dim CPx As New CP(CP.CP_Type.File, OpenFileDialog1.FileName)
            Load_FromCP(CPx)
            CPx.Dispose()
        End If
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Dim CPx As New CP(CP.CP_Type.Registry)
        Load_FromCP(CPx)
        CPx.Dispose()
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        Dim CPx As CP
        Select Case My.PreviewStyle
            Case WindowStyle.W11
                CPx = New CP_Defaults().Default_Windows11
            Case WindowStyle.W10
                CPx = New CP_Defaults().Default_Windows10
            Case WindowStyle.W81
                CPx = New CP_Defaults().Default_Windows81
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

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Save(My.CP)
        Me.Close()
    End Sub

    Private Sub Form_HelpButtonClicked(sender As Object, e As CancelEventArgs) Handles Me.HelpButtonClicked
        Process.Start(My.Resources.Link_Wiki & "/Edit-LogonUI-screen#windows-11--10")
    End Sub
End Class