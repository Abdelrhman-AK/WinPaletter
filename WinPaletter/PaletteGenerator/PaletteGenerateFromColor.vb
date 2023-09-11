﻿Imports WinPaletter.XenonCore

Public Class PaletteGenerateFromColor

    Private Colors_List As New List(Of Color)
    Private CP_Backup As CP

    Dim PickerOpened As Boolean = False

    Private Sub PaletteGenerateFromImage_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadLanguage
        ApplyDarkMode(Me)
        Icon = PaletteGenerateFromImage.Icon
        CP_Backup = New CP(CP.CP_Type.Registry)
    End Sub
    Private Sub SelectedColor_DragDrop(sender As Object, e As DragEventArgs) Handles SelectedColor.DragDrop
        GetColors()
    End Sub

    Private Sub SelectedColor_Click(sender As Object, e As EventArgs) Handles SelectedColor.Click

        If TypeOf e Is DragEventArgs Then Exit Sub

        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            PickerOpened = True
            SelectedColor.BackColor = SubMenu.ShowMenu(sender)
            GetColors()
            PickerOpened = False

        Else
            Dim ctrls As New List(Of Control) From {SelectedColor}
            PickerOpened = True
            SelectedColor.BackColor = ColorPickerDlg.Pick(ctrls)
            GetColors()
            PickerOpened = False

        End If

    End Sub

    Private Sub SelectedColor_BackColorChanged(sender As Object, e As EventArgs) Handles SelectedColor.BackColorChanged
        If Not PickerOpened Then GetColors()
    End Sub

    Private Sub XenonTrackbar1_Scroll(sender As Object) Handles XenonTrackbar1.Scroll
        val1.Text = sender.Value
        GetColors()
    End Sub

    Private Sub val1_Click(sender As Object, e As EventArgs) Handles val1.Click
        Dim response As String = InputBox(My.Lang.InputValue, sender.text, My.Lang.ItMustBeNumerical)
        sender.Text = Math.Max(Math.Min(Val(response), XenonTrackbar1.Maximum), XenonTrackbar1.Minimum) : XenonTrackbar1.Value = Val(sender.Text)
    End Sub

    Private Sub XenonCheckBox1_CheckedChanged(sender As Object) Handles XenonCheckBox1.CheckedChanged
        GetColors()
    End Sub

    Private Sub XenonRadioButton3_CheckedChanged(sender As Object) Handles XenonRadioButton3.CheckedChanged, XenonRadioButton5.CheckedChanged, XenonRadioButton4.CheckedChanged, XenonRadioButton6.CheckedChanged, XenonRadioButton7.CheckedChanged
        If CType(sender, XenonRadioButton).Checked Then GetColors()
    End Sub

    Sub GetColors()

        For Each ctrl As XenonCP In ImgPaletteContainer.Controls.OfType(Of XenonCP)
            ctrl.Dispose()
        Next
        ImgPaletteContainer.Controls.Clear()

        Colors_List.Clear()

        Dim _Color As Color = SelectedColor.BackColor
        Dim _ColorInverted As Color = _Color.Invert

        If XenonRadioButton5.Checked Then
            _Color = _Color.Light
            _ColorInverted = _ColorInverted.Light

        ElseIf XenonRadioButton4.Checked Then
            _Color = _Color.LightLight
            _ColorInverted = _ColorInverted.LightLight

        ElseIf XenonRadioButton6.Checked Then
            _Color = _Color.Dark
            _ColorInverted = _ColorInverted.Dark

        ElseIf XenonRadioButton7.Checked Then
            _Color = _Color.Dark(0.8)
            _ColorInverted = _ColorInverted.Dark(0.8)

        End If

        Colors_List.Add(_Color)
        If XenonCheckBox1.Checked Then Colors_List.Add(_ColorInverted)

        For i = 0 To XenonTrackbar1.Value / 2
            Colors_List.Add(_Color.Dark(i / XenonTrackbar1.Value))
            Colors_List.Add(_Color.Light(i / XenonTrackbar1.Value))

            If XenonCheckBox1.Checked Then
                Colors_List.Add(_ColorInverted.Dark(i / XenonTrackbar1.Value))
                Colors_List.Add(_ColorInverted.Light(i / XenonTrackbar1.Value))
            End If
        Next

        Colors_List.Sort(New RGBColorComparer())

        For Each C As Color In Colors_List
            Dim MiniColorItem As New XenonCP With {
                .Size = .GetMiniColorItemSize,
                .AllowDrop = False,
                .PauseColorsHistory = True,
                .BackColor = Color.FromArgb(255, C), .DefaultColor = .BackColor}

            ImgPaletteContainer.Controls.Add(MiniColorItem)
        Next

    End Sub

    Private Sub XenonButton1_Click(sender As Object, e As EventArgs) Handles XenonButton1.Click
        Dim arr As List(Of Integer) = GetUniqueRandomNumbers(0, Colors_List.Count)

        Select Case My.PreviewStyle
            Case PreviewHelpers.WindowStyle.W11
                My.CP.Windows11.Titlebar_Active = Colors_List(arr(0))
                My.CP.Windows11.Titlebar_Inactive = Colors_List(arr(1))
                My.CP.Windows11.StartMenu_Accent = Colors_List(arr(2))
                My.CP.Windows11.Color_Index0 = Colors_List(arr(3))
                My.CP.Windows11.Color_Index1 = Colors_List(arr(4))
                My.CP.Windows11.Color_Index2 = Colors_List(arr(5))
                My.CP.Windows11.Color_Index3 = Colors_List(arr(6))
                My.CP.Windows11.Color_Index4 = Colors_List(arr(7))
                My.CP.Windows11.Color_Index5 = Colors_List(arr(8))
                My.CP.Windows11.Color_Index6 = Colors_List(arr(9))
                My.CP.Windows11.Color_Index7 = Colors_List(arr(10))

            Case PreviewHelpers.WindowStyle.W10
                My.CP.Windows10.Titlebar_Active = Colors_List(arr(0))
                My.CP.Windows10.Titlebar_Inactive = Colors_List(arr(1))
                My.CP.Windows10.StartMenu_Accent = Colors_List(arr(2))
                My.CP.Windows10.Color_Index0 = Colors_List(arr(3))
                My.CP.Windows10.Color_Index1 = Colors_List(arr(4))
                My.CP.Windows10.Color_Index2 = Colors_List(arr(5))
                My.CP.Windows10.Color_Index3 = Colors_List(arr(6))
                My.CP.Windows10.Color_Index4 = Colors_List(arr(7))
                My.CP.Windows10.Color_Index5 = Colors_List(arr(8))
                My.CP.Windows10.Color_Index6 = Colors_List(arr(9))
                My.CP.Windows10.Color_Index7 = Colors_List(arr(10))

            Case PreviewHelpers.WindowStyle.W81
                My.CP.Windows81.AccentColor = Colors_List(arr(0))
                My.CP.Windows81.ColorizationColor = Colors_List(arr(1))
                My.CP.Windows81.PersonalColors_Accent = Colors_List(arr(2))
                My.CP.Windows81.PersonalColors_Background = Colors_List(arr(3))
                My.CP.Windows81.StartColor = Colors_List(arr(4))

            Case PreviewHelpers.WindowStyle.W7
                My.CP.Windows7.ColorizationColor = Colors_List(arr(0))
                My.CP.Windows7.ColorizationAfterglow = Colors_List(arr(1))

            Case PreviewHelpers.WindowStyle.WVista
                My.CP.WindowsVista.ColorizationColor = Colors_List(arr(0))

        End Select

        MainFrm.ApplyCPValues(My.CP)
        MainFrm.ApplyColorsToElements(My.CP)
    End Sub

    Private Shared StaticRandom As New Random()

    Public Shared Function GetUniqueRandomNumbers(Start As Integer, Count As Integer) As List(Of Integer)
        SyncLock StaticRandom
            Return Enumerable.Range(Start, Count).OrderBy(Function(__) StaticRandom.Next()).ToList()
        End SyncLock
    End Function

    Private Sub XenonButton3_Click(sender As Object, e As EventArgs) Handles XenonButton3.Click
        Select Case My.PreviewStyle
            Case PreviewHelpers.WindowStyle.W11
                My.CP.Windows11.Titlebar_Active = CP_Backup.Windows11.Titlebar_Active
                My.CP.Windows11.StartMenu_Accent = CP_Backup.Windows11.StartMenu_Accent
                My.CP.Windows11.Color_Index0 = CP_Backup.Windows11.Color_Index0
                My.CP.Windows11.Color_Index1 = CP_Backup.Windows11.Color_Index1
                My.CP.Windows11.Color_Index2 = CP_Backup.Windows11.Color_Index2
                My.CP.Windows11.Color_Index3 = CP_Backup.Windows11.Color_Index3
                My.CP.Windows11.Color_Index4 = CP_Backup.Windows11.Color_Index4
                My.CP.Windows11.Color_Index5 = CP_Backup.Windows11.Color_Index5
                My.CP.Windows11.Color_Index6 = CP_Backup.Windows11.Color_Index6
                My.CP.Windows11.Color_Index7 = CP_Backup.Windows11.Color_Index7

            Case PreviewHelpers.WindowStyle.W10
                My.CP.Windows10.Titlebar_Active = CP_Backup.Windows10.Titlebar_Active
                My.CP.Windows10.StartMenu_Accent = CP_Backup.Windows10.StartMenu_Accent
                My.CP.Windows10.Color_Index0 = CP_Backup.Windows10.Color_Index0
                My.CP.Windows10.Color_Index1 = CP_Backup.Windows10.Color_Index1
                My.CP.Windows10.Color_Index2 = CP_Backup.Windows10.Color_Index2
                My.CP.Windows10.Color_Index3 = CP_Backup.Windows10.Color_Index3
                My.CP.Windows10.Color_Index4 = CP_Backup.Windows10.Color_Index4
                My.CP.Windows10.Color_Index5 = CP_Backup.Windows10.Color_Index5
                My.CP.Windows10.Color_Index6 = CP_Backup.Windows10.Color_Index6
                My.CP.Windows10.Color_Index7 = CP_Backup.Windows10.Color_Index7

            Case PreviewHelpers.WindowStyle.W81
                My.CP.Windows81.AccentColor = CP_Backup.Windows81.AccentColor
                My.CP.Windows81.ColorizationColor = CP_Backup.Windows81.ColorizationColor
                My.CP.Windows81.PersonalColors_Accent = CP_Backup.Windows81.PersonalColors_Accent
                My.CP.Windows81.PersonalColors_Background = CP_Backup.Windows81.PersonalColors_Background
                My.CP.Windows81.StartColor = CP_Backup.Windows81.StartColor

            Case PreviewHelpers.WindowStyle.W7
                My.CP.Windows7.ColorizationColor = CP_Backup.Windows7.ColorizationColor
                My.CP.Windows7.ColorizationAfterglow = CP_Backup.Windows7.ColorizationAfterglow

            Case PreviewHelpers.WindowStyle.WVista
                My.CP.WindowsVista.ColorizationColor = CP_Backup.WindowsVista.ColorizationColor

        End Select

        MainFrm.ApplyCPValues(My.CP)
        MainFrm.ApplyColorsToElements(My.CP)

        Close()
    End Sub

    Private Sub XenonButton2_Click(sender As Object, e As EventArgs) Handles XenonButton2.Click
        Close()
    End Sub
End Class