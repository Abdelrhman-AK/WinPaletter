Imports System.ComponentModel

Public Class LogonUIXP
    Private Sub LogonUIXP_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadLanguage
        ApplyStyle(Me)
        Icon = LogonUI.Icon
        Button12.Image = MainFrm.Button20.Image.Resize(16, 16)
        ApplyFromCP(My.CP)
    End Sub

    Protected Overrides Sub OnDragOver(e As DragEventArgs)
        If TypeOf e.Data.GetData("WinPaletter.UI.Controllers.ColorItem") Is UI.Controllers.ColorItem Then
            Focus()
            BringToFront()
        Else
            Exit Sub
        End If

        MyBase.OnDragOver(e)
    End Sub

    Sub ApplyFromCP(CP As CP)
        With CP.LogonUIXP
            Toggle1.Checked = .Enabled
            Select Case .Mode
                Case CP.Structures.LogonUIXP.Modes.Default
                    RadioImage1.Checked = True
                Case CP.Structures.LogonUIXP.Modes.Win2000
                    RadioImage2.Checked = True
                Case Else
                    RadioImage1.Checked = True
            End Select
            color_pick.BackColor = .BackColor
            CheckBox1.Checked = .ShowMoreOptions
        End With
    End Sub

    Sub ApplyToCP(CP As CP)
        With CP.LogonUIXP
            .Enabled = Toggle1.Checked
            If RadioImage1.Checked Then .Mode = CP.Structures.LogonUIXP.Modes.Default Else .Mode = CP.Structures.LogonUIXP.Modes.Win2000
            .BackColor = color_pick.BackColor
            .ShowMoreOptions = CheckBox1.Checked
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
        CPx.LogonUIXP.Apply()
        CPx.Dispose()
        Cursor = Cursors.Default
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        ApplyToCP(My.CP)
        Me.Close()
    End Sub

    Private Sub color_pick_Click(sender As Object, e As EventArgs) Handles color_pick.Click, color_pick.DragDrop
        If TypeOf e Is DragEventArgs Then Exit Sub

        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            Exit Sub
        End If

        Dim CList As New List(Of Control) From {sender}
        Dim C As Color = ColorPickerDlg.Pick(CList)
        sender.BackColor = Color.FromArgb(255, C)
        CList.Clear()

    End Sub

    Private Sub Toggle1_CheckedChanged(sender As Object, e As EventArgs) Handles Toggle1.CheckedChanged
        checker_img.Image = If(sender.Checked, My.Resources.checker_enabled, My.Resources.checker_disabled)
    End Sub

    Private Sub Form_HelpButtonClicked(sender As Object, e As CancelEventArgs) Handles Me.HelpButtonClicked
        Process.Start(My.Resources.Link_Wiki & "/Edit-LogonUI-screen#windows-xp")
    End Sub
End Class