﻿Imports System.IO
Imports WinPaletter.XenonCore
Public Class WindowsTerminal
    Private _Shown As Boolean = False

    Public _Mode As Mode = Mode.Stable

    Public _Terminal As WinTerminal

    Public Enum Mode
        Stable
        Preview
        Developer
    End Enum

    Private Sub WindowsTerminal_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ApplyDarkMode(Me)
        _Shown = False

        Select Case _Mode
            Case Mode.Stable
                _Terminal = MainFrm.CP.Terminal
                Text = "Windows Terminal - Stable Version"

            Case Mode.Preview
                _Terminal = MainFrm.CP.TerminalPreview
                Text = "Windows Terminal - Preview Version"

            Case Mode.Developer
                '_Terminal = MainFrm.CP.Terminal
                Text = "Windows Terminal - Developer Version"

        End Select



        FillFonts(TerFonts, True)

        If My.W10 Or My.W11 Then
            Dim TerPreDir As String = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) & "\AppData\Local\Packages\Microsoft.WindowsTerminalPreview_8wekyb3d8bbwe\LocalState\settings.json"
            Dim TerDir As String = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) & "\AppData\Local\Packages\Microsoft.WindowsTerminal_8wekyb3d8bbwe\LocalState\settings.json"

            If IO.File.Exists(TerDir) And _Mode = Mode.Stable Then
                'FillTerminalSchemes(CP.Terminal, TerSchemes)

            End If

            If IO.File.Exists(TerPreDir) And _Mode = Mode.Preview Then
                Load_FromTerminal()

            End If

        Else

        End If


    End Sub

    Sub Load_FromTerminal()
        FillTerminalSchemes(_Terminal, TerSchemes)
        FillTerminalThemes(_Terminal, TerThemes)
        FillTerminalProfiles(_Terminal, TerProfiles)

        TerProfiles.SelectedIndex = 0

        If _Terminal.theme = "dark" Then
            TerThemes.SelectedIndex = 0
            TerTitlebarActive.BackColor = Nothing
            TerTitlebarInactive.BackColor = Nothing
            TerTabActive.BackColor = Nothing
            TerTabInactive.BackColor = Nothing
            TerMode.Checked = True

        ElseIf _Terminal.theme = "light" Then
            TerThemes.SelectedIndex = 1
            TerTitlebarActive.BackColor = Nothing
            TerTitlebarInactive.BackColor = Nothing
            TerTabActive.BackColor = Nothing
            TerTabInactive.BackColor = Nothing
            TerMode.Checked = False

        ElseIf _Terminal.theme = "system" Then
            TerThemes.SelectedIndex = 2
            TerTitlebarActive.BackColor = Nothing
            TerTitlebarInactive.BackColor = Nothing
            TerTabActive.BackColor = Nothing
            TerTabInactive.BackColor = Nothing
            TerMode.Checked = Not MainFrm.CP.AppMode_Light

        ElseIf TerThemes.Items.Contains(_Terminal.theme) Then

            TerThemes.SelectedItem = _Terminal.theme

            TerThemesContainer.Enabled = True

            With _Terminal.Themes(TerThemes.SelectedIndex - 3)
                TerTitlebarActive.BackColor = .Titlebar_Active
                TerTitlebarInactive.BackColor = .Titlebar_Inactive
                TerTabActive.BackColor = .Tab_Active
                TerTabInactive.BackColor = .Tab_Inactive
                TerMode.Checked = If(.applicationTheme_light.ToLower = "light", False, True)
            End With

        End If



        ApplyPreview(_Terminal)
    End Sub

    Private Sub WindowsTerminal_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        _Shown = True
    End Sub

    Sub FillTerminalSchemes(Terminal As WinTerminal, Combobox As ComboBox)
        Combobox.Items.Clear()

        For x = 0 To Terminal.Colors.Count - 1
            Combobox.Items.Add(Terminal.Colors(x).Name)
        Next

    End Sub

    Sub FillTerminalThemes(Terminal As WinTerminal, Combobox As ComboBox)
        Combobox.Items.Clear()

        Combobox.Items.Add("Dark")
        Combobox.Items.Add("Light")
        Combobox.Items.Add("System")

        For x = 0 To Terminal.Themes.Count - 1
            Combobox.Items.Add(Terminal.Themes(x).Name)
        Next

    End Sub

    Sub FillTerminalProfiles(Terminal As WinTerminal, Combobox As ComboBox)
        Combobox.Items.Clear()

        Combobox.Items.Add("Default")

        For x = 0 To Terminal.Profiles.Count - 1
            Combobox.Items.Add(Terminal.Profiles(x).Name)
        Next

    End Sub

    Sub FillFonts([ListBox] As ComboBox, Optional FixedPitch As Boolean = False)
        Dim s As String = [ListBox].SelectedItem

        [ListBox].Items.Clear()

        If Not FixedPitch Then
            For Each x As String In My.Application.FontsList
                [ListBox].Items.Add(x)
            Next
        Else
            For Each x As String In My.Application.FontsFixedList
                [ListBox].Items.Add(x)
            Next
        End If

        [ListBox].SelectedItem = s

        If [ListBox].SelectedItem = Nothing Then [ListBox].SelectedIndex = 0
    End Sub

    Private Sub TerSchemes_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TerSchemes.SelectedIndexChanged
        Try
            With _Terminal.Colors(TerSchemes.SelectedIndex)
                TerBackground.BackColor = .Background
                TerForeground.BackColor = .Foreground
                TerSelection.BackColor = .SelectionBackground
                TerCursor.BackColor = .CursorColor

                TerBlack.BackColor = .Black
                TerBlue.BackColor = .Blue
                TerGreen.BackColor = .Green
                TerCyan.BackColor = .Cyan
                TerRed.BackColor = .Red
                TerPurple.BackColor = .Purple
                TerYellow.BackColor = .Yellow
                TerWhite.BackColor = .White

                TerBlackB.BackColor = .BrightBlack
                TerBlueB.BackColor = .BrightBlue
                TerGreenB.BackColor = .BrightGreen
                TerCyanB.BackColor = .BrightCyan
                TerRedB.BackColor = .BrightRed
                TerPurpleB.BackColor = .BrightPurple
                TerYellowB.BackColor = .BrightYellow
                TerWhiteB.BackColor = .BrightWhite
            End With

            With If(TerProfiles.SelectedIndex = 0, _Terminal.DefaultProf, _Terminal.Profiles(TerProfiles.SelectedIndex - 1))
                .ColorScheme = TerSchemes.SelectedItem
            End With

            If _Shown Then ApplyPreview(_Terminal)

        Catch ex As Exception
            MsgBox(ex.Message & vbCrLf & vbCrLf & ex.StackTrace, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub TerProfiles_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TerProfiles.SelectedIndexChanged

        With If(TerProfiles.SelectedIndex = 0, _Terminal.DefaultProf, _Terminal.Profiles(TerProfiles.SelectedIndex - 1))
            Try
                If TerSchemes.Items.Contains(.ColorScheme) Then TerSchemes.SelectedItem = .ColorScheme Else TerSchemes.SelectedItem = _Terminal.DefaultProf.ColorScheme
            Catch
            End Try

            TerBackImage.Text = .BackgroundImage
            TerImageOpacity.Value = .BackgroundImageOpacity * 100

            TerCursorStyle.SelectedIndex = .CursorShape
            TerCursorHeightBar.Value = .CursorHeight

            TerFonts.SelectedItem = .Font.Face
            TerFontSizeBar.Value = .Font.Size
            TerFontWeight.SelectedIndex = .Font.Weight

            TerAcrylic.Checked = .UseAcrylic
            TerOpacityBar.Value = .Opacity

            XenonTerminal1.Opacity = .Opacity
            XenonTerminal2.Opacity = .Opacity
            XenonTerminal1.OpacityBackImage = .BackgroundImageOpacity * 100
            XenonTerminal2.OpacityBackImage = .BackgroundImageOpacity * 100

            XenonTerminal1.Refresh()
            XenonTerminal2.Refresh()
        End With

    End Sub

    Private Sub TerFontSizeBar_Scroll(sender As Object) Handles TerFontSizeBar.Scroll
        TerFontSizeLbl.Text = sender.Value

        If Not _Shown Then Exit Sub

        XenonTerminal1.Font = New Font(XenonTerminal1.Font.Name, TerFontSizeBar.Value, XenonTerminal1.Font.Style)

        With If(TerProfiles.SelectedIndex = 0, _Terminal.DefaultProf, _Terminal.Profiles(TerProfiles.SelectedIndex - 1))
            .Font.Size = sender.Value
        End With

    End Sub

    Private Sub TerCursorHeightBar_Scroll(sender As Object) Handles TerCursorHeightBar.Scroll
        XenonTerminal1.CursorHeight = sender.Value
        XenonTerminal1.Refresh()
        TerCursorHeightLbl.Text = TerCursorHeightBar.Value
        XenonTerminal1.Refresh()
        XenonTerminal2.Refresh()

        If Not _Shown Then Exit Sub

        With If(TerProfiles.SelectedIndex = 0, _Terminal.DefaultProf, _Terminal.Profiles(TerProfiles.SelectedIndex - 1))
            .CursorHeight = sender.Value
        End With
    End Sub

    Private Sub TerImageOpacity_Scroll(sender As Object) Handles TerImageOpacity.Scroll
        TerImageOpacityLbl.Text = sender.Value

        XenonTerminal1.OpacityBackImage = TerImageOpacity.Value
        XenonTerminal2.OpacityBackImage = TerImageOpacity.Value

        If Not _Shown Then Exit Sub


        With If(TerProfiles.SelectedIndex = 0, _Terminal.DefaultProf, _Terminal.Profiles(TerProfiles.SelectedIndex - 1))
            .BackgroundImageOpacity = TerImageOpacity.Value / 100
        End With
    End Sub

    Private Sub TerCursorStyle_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TerCursorStyle.SelectedIndexChanged
        XenonTerminal1.CursorType = TerCursorStyle.SelectedIndex
        XenonTerminal1.Refresh()

        If Not _Shown Then Exit Sub

        With If(TerProfiles.SelectedIndex = 0, _Terminal.DefaultProf, _Terminal.Profiles(TerProfiles.SelectedIndex - 1))
            .CursorShape = TerCursorStyle.SelectedIndex
        End With
    End Sub

    Private Sub XenonButton12_Click(sender As Object, e As EventArgs) Handles XenonButton12.Click
        _Terminal.Colors.Add(New TColor With {.Name = "New Color #" & TerSchemes.Items.Count})
        FillTerminalSchemes(_Terminal, TerSchemes)
        TerSchemes.SelectedIndex = TerSchemes.Items.Count - 1
    End Sub

    Private Sub ColorClick(sender As Object, e As EventArgs) Handles TerBlack.Click,
                                                                     TerBlue.Click, TerGreen.Click, TerCyan.Click, TerRed.Click, TerPurple.Click, TerYellow.Click,
                                                                     TerWhite.Click, TerBlackB.Click, TerBlueB.Click, TerGreenB.Click, TerCyanB.Click, TerRedB.Click,
                                                                     TerPurpleB.Click, TerYellowB.Click, TerWhiteB.Click


        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            Exit Sub
        End If

        Dim CList As New List(Of Control) From {sender, XenonTerminal1}

        Dim _Conditions As New Conditions
        If sender.Name.ToString.ToLower.Contains("ColorTable00".ToLower) Then _Conditions.CMD_ColorTable00 = True
        If sender.Name.ToString.ToLower.Contains("ColorTable01".ToLower) Then _Conditions.CMD_ColorTable01 = True
        If sender.Name.ToString.ToLower.Contains("ColorTable02".ToLower) Then _Conditions.CMD_ColorTable02 = True
        If sender.Name.ToString.ToLower.Contains("ColorTable03".ToLower) Then _Conditions.CMD_ColorTable03 = True
        If sender.Name.ToString.ToLower.Contains("ColorTable04".ToLower) Then _Conditions.CMD_ColorTable04 = True
        If sender.Name.ToString.ToLower.Contains("ColorTable05".ToLower) Then _Conditions.CMD_ColorTable05 = True
        If sender.Name.ToString.ToLower.Contains("ColorTable06".ToLower) Then _Conditions.CMD_ColorTable06 = True
        If sender.Name.ToString.ToLower.Contains("ColorTable07".ToLower) Then _Conditions.CMD_ColorTable07 = True
        If sender.Name.ToString.ToLower.Contains("ColorTable08".ToLower) Then _Conditions.CMD_ColorTable08 = True
        If sender.Name.ToString.ToLower.Contains("ColorTable09".ToLower) Then _Conditions.CMD_ColorTable09 = True
        If sender.Name.ToString.ToLower.Contains("ColorTable10".ToLower) Then _Conditions.CMD_ColorTable10 = True
        If sender.Name.ToString.ToLower.Contains("ColorTable11".ToLower) Then _Conditions.CMD_ColorTable11 = True
        If sender.Name.ToString.ToLower.Contains("ColorTable12".ToLower) Then _Conditions.CMD_ColorTable12 = True
        If sender.Name.ToString.ToLower.Contains("ColorTable13".ToLower) Then _Conditions.CMD_ColorTable13 = True
        If sender.Name.ToString.ToLower.Contains("ColorTable14".ToLower) Then _Conditions.CMD_ColorTable14 = True
        If sender.Name.ToString.ToLower.Contains("ColorTable15".ToLower) Then _Conditions.CMD_ColorTable15 = True


        Dim C As Color = ColorPickerDlg.Pick(CList, _Conditions)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()
    End Sub

    Private Sub ColorMainsClick(sender As Object, e As EventArgs) Handles TerBackground.Click, TerForeground.Click, TerSelection.Click, TerCursor.Click,
                                                                          TerTabActive.Click, TerTabInactive.Click, TerTitlebarActive.Click, TerTitlebarInactive.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            Exit Sub
        End If

        Dim CList As New List(Of Control) From {sender, XenonTerminal1, XenonTerminal2}

        Dim _Conditions As New Conditions

        If sender.Name.ToString.ToLower.Contains(TerBackground.Name.ToLower) Then _Conditions.Terminal_Back = True
        If sender.Name.ToString.ToLower.Contains(TerForeground.Name.ToLower) Then _Conditions.Terminal_Fore = True
        If sender.Name.ToString.ToLower.Contains(TerSelection.Name.ToLower) Then _Conditions.Terminal_Selection = True
        If sender.Name.ToString.ToLower.Contains(TerCursor.Name.ToLower) Then _Conditions.Terminal_Cursor = True

        If sender.Name.ToString.ToLower.Contains(TerTabActive.Name.ToLower) Then _Conditions.Terminal_TabActive = True
        If sender.Name.ToString.ToLower.Contains(TerTabInactive.Name.ToLower) Then _Conditions.Terminal_TabInactive = True
        If sender.Name.ToString.ToLower.Contains(TerTitlebarActive.Name.ToLower) Then _Conditions.Terminal_TitlebarActive = True
        If sender.Name.ToString.ToLower.Contains(TerTitlebarInactive.Name.ToLower) Then _Conditions.Terminal_TitlebarInactive = True


        Dim C As Color = ColorPickerDlg.Pick(CList, _Conditions)

        If sender.Name.ToString.ToLower.Contains(TerBackground.Name.ToLower) Then
            _Terminal.Colors(TerSchemes.SelectedIndex).Background = C
        End If

        If sender.Name.ToString.ToLower.Contains(TerForeground.Name.ToLower) Then
            _Terminal.Colors(TerSchemes.SelectedIndex).Foreground = C
        End If

        If sender.Name.ToString.ToLower.Contains(TerSelection.Name.ToLower) Then
            _Terminal.Colors(TerSchemes.SelectedIndex).SelectionBackground = C
        End If

        If sender.Name.ToString.ToLower.Contains(TerCursor.Name.ToLower) Then
            _Terminal.Colors(TerSchemes.SelectedIndex).CursorColor = C
        End If

        If sender.Name.ToString.ToLower.Contains(TerTabActive.Name.ToLower) Then
            _Terminal.Themes(TerThemes.SelectedIndex - 3).Tab_Active = C
        End If

        If sender.Name.ToString.ToLower.Contains(TerTabInactive.Name.ToLower) Then
            _Terminal.Themes(TerThemes.SelectedIndex - 3).Tab_Inactive = C
        End If

        If sender.Name.ToString.ToLower.Contains(TerTitlebarActive.Name.ToLower) Then
            _Terminal.Themes(TerThemes.SelectedIndex - 3).Titlebar_Active = C
        End If

        If sender.Name.ToString.ToLower.Contains(TerTitlebarInactive.Name.ToLower) Then
            _Terminal.Themes(TerThemes.SelectedIndex - 3).Titlebar_Inactive = C
        End If

        ApplyPreview(_Terminal)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()
    End Sub

    Private Sub TerFonts_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TerFonts.SelectedIndexChanged
        XenonTerminal1.Font = New Font(TerFonts.SelectedItem.ToString, XenonTerminal1.Font.Size, XenonTerminal1.Font.Style)
    End Sub

    Sub ApplyPreview([Terminal] As WinTerminal)
        Try
            XenonTerminal1.UseAcrylicOnTitlebar = [Terminal].useAcrylicInTabRow
            XenonTerminal2.UseAcrylicOnTitlebar = [Terminal].useAcrylicInTabRow

            XenonTerminal1.Color_Background = [Terminal].Colors(TerSchemes.SelectedIndex).Background
            XenonTerminal1.Color_Foreground = [Terminal].Colors(TerSchemes.SelectedIndex).Foreground
            XenonTerminal1.Color_Selection = [Terminal].Colors(TerSchemes.SelectedIndex).SelectionBackground
            XenonTerminal1.Color_Cursor = [Terminal].Colors(TerSchemes.SelectedIndex).CursorColor

            If TerThemesContainer.Enabled Then
                If [Terminal].Themes(TerThemes.SelectedIndex - 3).Tab_Active = Nothing Then
                    XenonTerminal1.Color_TabFocused = Nothing
                Else
                    XenonTerminal1.Color_TabFocused = [Terminal].Themes(TerThemes.SelectedIndex - 3).Tab_Active
                End If

                If [Terminal].Themes(TerThemes.SelectedIndex - 3).Titlebar_Active = Nothing Then
                    XenonTerminal1.Color_Titlebar = Nothing
                Else
                    XenonTerminal1.Color_Titlebar = [Terminal].Themes(TerThemes.SelectedIndex - 3).Titlebar_Active
                End If

                If [Terminal].Themes(TerThemes.SelectedIndex - 3).Tab_Inactive = Nothing Then
                    XenonTerminal1.Color_TabUnFocused = Nothing
                Else
                    XenonTerminal1.Color_TabUnFocused = [Terminal].Themes(TerThemes.SelectedIndex - 3).Tab_Inactive
                End If

                If [Terminal].Themes(TerThemes.SelectedIndex - 3).Titlebar_Inactive = Nothing Then
                    XenonTerminal2.Color_Titlebar_Unfocused = Nothing
                Else
                    XenonTerminal2.Color_Titlebar_Unfocused = [Terminal].Themes(TerThemes.SelectedIndex - 3).Titlebar_Inactive
                End If
            Else
                XenonTerminal1.Color_TabFocused = Nothing
                XenonTerminal1.Color_Titlebar = Nothing
                XenonTerminal1.Color_Titlebar_Unfocused = Nothing
                XenonTerminal1.Color_TabUnFocused = Nothing
                XenonTerminal2.Color_Titlebar_Unfocused = Nothing

                If TerThemes.SelectedItem IsNot Nothing Then
                    If TerThemes.SelectedItem.ToString.ToLower = "dark" Then
                        XenonTerminal1.Light = False
                        XenonTerminal2.Light = False

                    ElseIf TerThemes.SelectedItem.ToString.ToLower = "light" Then
                        XenonTerminal1.Light = True
                        XenonTerminal2.Light = True
                    Else
                        XenonTerminal1.Light = MainFrm.CP.AppMode_Light
                        XenonTerminal2.Light = MainFrm.CP.AppMode_Light
                    End If
                End If

            End If

            Dim fx As New LOGFONT
            Dim f_cmd As New Font([Terminal].Profiles(TerProfiles.SelectedIndex).Font.Face, [Terminal].Profiles(TerProfiles.SelectedIndex).Font.Size)
            f_cmd.ToLogFont(fx)
            fx.lfWeight = [Terminal].Profiles(TerProfiles.SelectedIndex).Font.Weight * 100
            With Font.FromLogFont(fx) : f_cmd = New Font(f_cmd.Name, f_cmd.Size, .Style) : End With
            XenonTerminal1.Font = f_cmd
            XenonTerminal2.Font = f_cmd

            XenonTerminal1.Refresh()
            XenonTerminal2.Refresh()

        Catch ex As Exception
            MsgBox(ex.Message & vbCrLf & vbCrLf & ex.StackTrace, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub XenonButton3_Click(sender As Object, e As EventArgs) Handles XenonButton3.Click
        _Terminal.Themes.Add(New ThemesList With {.Name = "New Theme #" & TerThemes.Items.Count - 3})
        FillTerminalThemes(_Terminal, TerThemes)
        TerThemes.SelectedIndex = TerThemes.Items.Count - 1
    End Sub

    Private Sub TerFontWeight_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TerFontWeight.SelectedIndexChanged
        Dim fx As New LOGFONT
        Dim f_cmd As New Font(XenonTerminal1.Font.Name, XenonTerminal1.Font.Size, XenonTerminal1.Font.Style)
        f_cmd.ToLogFont(fx)
        fx.lfWeight = TerFontWeight.SelectedIndex * 100
        With Font.FromLogFont(fx) : f_cmd = New Font(XenonTerminal1.Font.Name, XenonTerminal1.Font.Size, .Style) : End With
        XenonTerminal1.Font = f_cmd
        XenonTerminal2.Font = f_cmd
        XenonTerminal1.Refresh()
        XenonTerminal2.Refresh()

        With If(TerProfiles.SelectedIndex = 0, _Terminal.DefaultProf, _Terminal.Profiles(TerProfiles.SelectedIndex - 1))
            .Font.Weight = TerFontWeight.SelectedIndex
        End With

    End Sub

    Private Sub TerThemes_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TerThemes.SelectedIndexChanged
        If Not _Shown Then Exit Sub

        Try

            If TerThemes.SelectedIndex > 2 Then
                TerThemesContainer.Enabled = True

                With _Terminal.Themes(TerThemes.SelectedIndex - 3)
                    TerTitlebarActive.BackColor = .Titlebar_Active
                    TerTitlebarInactive.BackColor = .Titlebar_Inactive
                    TerTabActive.BackColor = .Tab_Active
                    TerTabInactive.BackColor = .Tab_Inactive
                    TerMode.Checked = If(.applicationTheme_light.ToLower = "light", False, True)
                End With

                ApplyPreview(_Terminal)
            Else
                TerThemesContainer.Enabled = False

                TerTitlebarActive.BackColor = Nothing
                TerTitlebarInactive.BackColor = Nothing
                TerTabActive.BackColor = Nothing
                TerTabInactive.BackColor = Nothing

                If TerThemes.SelectedIndex = 0 Then TerMode.Checked = True
                If TerThemes.SelectedIndex = 1 Then TerMode.Checked = False
                If TerThemes.SelectedIndex = 2 Then TerMode.Checked = Not MainFrm.CP.AppMode_Light

            End If

            If TerThemes.SelectedItem.ToString.ToLower = "dark" Then
                _Terminal.theme = "dark"

            ElseIf TerThemes.SelectedItem.ToString.ToLower = "light" Then
                _Terminal.theme = "light"

            ElseIf TerThemes.SelectedItem.ToString.ToLower = "system" Then
                _Terminal.theme = "system"

            Else
                _Terminal.theme = TerThemes.SelectedItem.ToString
            End If

            ApplyPreview(_Terminal)
        Catch ex As Exception
            MsgBox(ex.Message & vbCrLf & vbCrLf & ex.StackTrace, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub TerEditThemeName_Click(sender As Object, e As EventArgs) Handles TerEditThemeName.Click
        If TerThemes.SelectedIndex > 2 Then
            Dim s As String = InputBox("Type theme name here:", "Theme Name", TerThemes.SelectedItem.ToString)
            If s <> TerThemes.SelectedItem.ToString And Not String.IsNullOrEmpty(s) And Not TerThemes.Items.Contains(s) Then
                Dim i As Integer = TerThemes.SelectedIndex
                TerThemes.Items.RemoveAt(i)
                TerThemes.Items.Insert(i, s)
                TerThemes.SelectedIndex = i
                _Terminal.Themes(i - 3).Name = s
            End If
        End If
    End Sub

    Private Sub XenonButton4_Click(sender As Object, e As EventArgs) Handles XenonButton4.Click
        Dim s As String = InputBox("Type scheme name here:", "Scheme Name", TerSchemes.SelectedItem.ToString)
        If s <> TerSchemes.SelectedItem.ToString And Not String.IsNullOrEmpty(s) And Not TerSchemes.Items.Contains(s) Then
            Dim i As Integer = TerSchemes.SelectedIndex
            TerSchemes.Items.RemoveAt(i)
            TerSchemes.Items.Insert(i, s)
            TerSchemes.SelectedIndex = i
            _Terminal.Colors(i).Name = s
        End If
    End Sub

    Private Sub XenonButton14_Click(sender As Object, e As EventArgs) Handles XenonButton14.Click
        TerminalInfo.Profile = If(TerProfiles.SelectedIndex = 0, _Terminal.DefaultProf, _Terminal.Profiles(TerProfiles.SelectedIndex - 1))
        If TerminalInfo.OpenDialog(TerProfiles.SelectedIndex = 0) = DialogResult.OK Then

            With If(TerProfiles.SelectedIndex = 0, _Terminal.DefaultProf, _Terminal.Profiles(TerProfiles.SelectedIndex - 1))
                .Name = TerminalInfo.Profile.Name
                .TabTitle = TerminalInfo.Profile.TabTitle
                .Icon = TerminalInfo.Profile.Icon
                .TabColor = TerminalInfo.Profile.TabColor
                .adjustIndistinguishableColors = TerminalInfo.Profile.adjustIndistinguishableColors
            End With

            Dim i As Integer = TerProfiles.SelectedIndex
            FillTerminalProfiles(_Terminal, TerProfiles)
            TerProfiles.SelectedIndex = i

            ApplyPreview(_Terminal)
        End If
    End Sub

    Private Sub TerAcrylicBar_Scroll(sender As Object) Handles TerOpacityBar.Scroll
        TerOpacityLbl.Text = TerOpacityBar.Value

        XenonTerminal1.Opacity = TerOpacityBar.Value
        XenonTerminal2.Opacity = TerOpacityBar.Value

        XenonTerminal1.Refresh()
        XenonTerminal2.Refresh()

        If _Shown Then
            With If(TerProfiles.SelectedIndex = 0, _Terminal.DefaultProf, _Terminal.Profiles(TerProfiles.SelectedIndex - 1))
                .Opacity = TerOpacityBar.Value
            End With
        End If

    End Sub

    Private Sub TerAcrylic_CheckedChanged(sender As Object) Handles TerAcrylic.CheckedChanged
        XenonTerminal1.UseAcrylic = TerAcrylic.Checked
        XenonTerminal2.UseAcrylic = TerAcrylic.Checked

        XenonTerminal1.Refresh()
        XenonTerminal2.Refresh()

        With If(TerProfiles.SelectedIndex = 0, _Terminal.DefaultProf, _Terminal.Profiles(TerProfiles.SelectedIndex - 1))
            .UseAcrylic = TerAcrylic.Checked
        End With

    End Sub

    Private Sub XenonCheckBox1_CheckedChanged(sender As Object) Handles XenonCheckBox1.CheckedChanged
        Dim i As Object = TerFonts.SelectedItem
        FillFonts(TerFonts, Not XenonCheckBox1.Checked)
        TerFonts.SelectedItem = i
    End Sub

    Private Sub XenonButton13_Click(sender As Object, e As EventArgs) Handles XenonButton13.Click
        _Terminal.Profiles.Add(New ProfilesList With {.Name = "New Profile #" & TerProfiles.Items.Count})
        FillTerminalProfiles(_Terminal, TerProfiles)
        TerProfiles.SelectedIndex = TerProfiles.Items.Count - 1
    End Sub

    Private Sub XenonButton15_Click(sender As Object, e As EventArgs) Handles XenonButton15.Click
        Dim TerDir As String = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) & "\AppData\Local\Packages\Microsoft.WindowsTerminal_8wekyb3d8bbwe"
        Dim TerPreDir As String = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) & "\AppData\Local\Packages\Microsoft.WindowsTerminalPreview_8wekyb3d8bbwe"

        Select Case _Mode
            Case Mode.Stable
                If IO.Directory.Exists(TerDir) Then Shell("explorer.exe shell:appsFolder\Microsoft.WindowsTerminal_8wekyb3d8bbwe!App")

            Case Mode.Preview
                If IO.Directory.Exists(TerPreDir) Then Shell("explorer.exe shell:appsFolder\Microsoft.WindowsTerminalPreview_8wekyb3d8bbwe!App")

            Case Mode.Developer

        End Select
    End Sub

    Private Sub XenonButton5_Click(sender As Object, e As EventArgs) Handles XenonButton5.Click
        TerBackImage.Text = "desktopWallpaper"
    End Sub

    Private Sub TerBackImage_TextChanged(sender As Object, e As EventArgs) Handles TerBackImage.TextChanged
        If TerBackImage.Text = "desktopWallpaper" Then
            XenonTerminal1.BackImage = ResizeImage(BitmapFillScaler(My.Application.GetCurrentWallpaper, XenonTerminal1.Size), XenonTerminal1.Width - 2, XenonTerminal1.Height - 32)
            XenonTerminal2.BackImage = ResizeImage(BitmapFillScaler(My.Application.GetCurrentWallpaper, XenonTerminal2.Size), XenonTerminal2.Width - 2, XenonTerminal2.Height - 32)
        Else
            If IO.File.Exists(TerBackImage.Text) Then
                XenonTerminal1.BackImage = ResizeImage(BitmapFillScaler(Image.FromStream(New FileStream(TerBackImage.Text, IO.FileMode.Open, IO.FileAccess.Read)), XenonTerminal1.Size), XenonTerminal1.Width - 2, XenonTerminal1.Height - 32)
                XenonTerminal2.BackImage = ResizeImage(BitmapFillScaler(Image.FromStream(New FileStream(TerBackImage.Text, IO.FileMode.Open, IO.FileAccess.Read)), XenonTerminal2.Size), XenonTerminal2.Width - 2, XenonTerminal2.Height - 32)
            Else
                XenonTerminal1.BackImage = Nothing
                XenonTerminal2.BackImage = Nothing
            End If
        End If

        XenonTerminal1.Invalidate()
        XenonTerminal2.Invalidate()
    End Sub

    Private Sub XenonButton16_Click(sender As Object, e As EventArgs) Handles XenonButton16.Click
        If OpenFileDialog1.ShowDialog = DialogResult.OK Then
            TerBackImage.Text = OpenFileDialog1.FileName
        End If
    End Sub
End Class