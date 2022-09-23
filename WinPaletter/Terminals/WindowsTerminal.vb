Imports WinPaletter.XenonCore
Public Class WindowsTerminal
    Private _Shown As Boolean = False

    Private Sub WindowsTerminal_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ApplyDarkMode(Me)
        _Shown = False
        FillFonts(TerPrevFonts, True)

        If My.W10 Or My.W11 Then
            Dim TerPreDir As String = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) & "\AppData\Local\Packages\Microsoft.WindowsTerminalPreview_8wekyb3d8bbwe\LocalState\settings.json"
            Dim TerDir As String = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) & "\AppData\Local\Packages\Microsoft.WindowsTerminal_8wekyb3d8bbwe\LocalState\settings.json"

            If IO.File.Exists(TerDir) Then
                'FillTerminalSchemes(CP.Terminal, TerPrevSchemes)

            End If

            If IO.File.Exists(TerPreDir) Then
                FillTerminalSchemes(MainFrm.CP.TerminalPreview, TerPrevSchemes)
                FillTerminalThemes(MainFrm.CP.TerminalPreview, TerPrevThemes)
                FillTerminalProfiles(MainFrm.CP.TerminalPreview, TerPrevProfiles)

                TerPrevProfiles.SelectedIndex = 0

                If MainFrm.CP.TerminalPreview.theme = "dark" Then
                    TerPrevThemes.SelectedIndex = 0
                    TerPrevTitlebarActive.BackColor = Nothing
                    TerPrevTitlebarInactive.BackColor = Nothing
                    TerPrevTabActive.BackColor = Nothing
                    TerPrevTabInactive.BackColor = Nothing
                    TerPrevMode.Checked = True

                ElseIf MainFrm.CP.TerminalPreview.theme = "light" Then
                    TerPrevThemes.SelectedIndex = 1
                    TerPrevTitlebarActive.BackColor = Nothing
                    TerPrevTitlebarInactive.BackColor = Nothing
                    TerPrevTabActive.BackColor = Nothing
                    TerPrevTabInactive.BackColor = Nothing
                    TerPrevMode.Checked = False

                ElseIf MainFrm.CP.TerminalPreview.theme = "system" Then
                    TerPrevThemes.SelectedIndex = 2
                    TerPrevTitlebarActive.BackColor = Nothing
                    TerPrevTitlebarInactive.BackColor = Nothing
                    TerPrevTabActive.BackColor = Nothing
                    TerPrevTabInactive.BackColor = Nothing
                    TerPrevMode.Checked = Not MainFrm.CP.AppMode_Light

                ElseIf TerPrevThemes.Items.Contains(MainFrm.CP.TerminalPreview.theme) Then

                    TerPrevThemes.SelectedItem = MainFrm.CP.TerminalPreview.theme

                    TerPrevThemesContainer.Enabled = True

                    With MainFrm.CP.TerminalPreview.Themes(TerPrevThemes.SelectedIndex - 3)
                        TerPrevTitlebarActive.BackColor = .Titlebar_Active
                        TerPrevTitlebarInactive.BackColor = .Titlebar_Inactive
                        TerPrevTabActive.BackColor = .Tab_Active
                        TerPrevTabInactive.BackColor = .Tab_Inactive
                        TerPrevMode.Checked = If(.applicationTheme_light.ToLower = "light", False, True)
                    End With

                End If

                ApplyPreview(MainFrm.CP.TerminalPreview)


            End If

        Else

        End If


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

    Private Sub TerPrevSchemes_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TerPrevSchemes.SelectedIndexChanged
        Try
            With MainFrm.CP.TerminalPreview.Colors(TerPrevSchemes.SelectedIndex)
                TerPrevBackground.BackColor = .Background
                TerPrevForeground.BackColor = .Foreground
                TerPrevSelection.BackColor = .SelectionBackground
                TerPrevCursor.BackColor = .CursorColor

                TerPrevBlack.BackColor = .Black
                TerPrevBlue.BackColor = .Blue
                TerPrevGreen.BackColor = .Green
                TerPrevCyan.BackColor = .Cyan
                TerPrevRed.BackColor = .Red
                TerPrevPurple.BackColor = .Purple
                TerPrevYellow.BackColor = .Yellow
                TerPrevWhite.BackColor = .White

                TerPrevBlackB.BackColor = .BrightBlack
                TerPrevBlueB.BackColor = .BrightBlue
                TerPrevGreenB.BackColor = .BrightGreen
                TerPrevCyanB.BackColor = .BrightCyan
                TerPrevRedB.BackColor = .BrightRed
                TerPrevPurpleB.BackColor = .BrightPurple
                TerPrevYellowB.BackColor = .BrightYellow
                TerPrevWhiteB.BackColor = .BrightWhite
            End With

            With If(TerPrevProfiles.SelectedIndex = 0, MainFrm.CP.TerminalPreview.DefaultProf, MainFrm.CP.TerminalPreview.Profiles(TerPrevProfiles.SelectedIndex - 1))
                .ColorScheme = TerPrevSchemes.SelectedItem
            End With

            If _Shown Then ApplyPreview(MainFrm.CP.TerminalPreview)

        Catch ex As Exception
            MsgBox(ex.Message & vbCrLf & vbCrLf & ex.StackTrace, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub TerPrevProfiles_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TerPrevProfiles.SelectedIndexChanged

        With If(TerPrevProfiles.SelectedIndex = 0, MainFrm.CP.TerminalPreview.DefaultProf, MainFrm.CP.TerminalPreview.Profiles(TerPrevProfiles.SelectedIndex - 1))

            'TerPrevName.Text = .Name
            'TerPrevTabTitle.Text = .TabTitle
            'TerPrevTabIcon.Text = .Icon
            'TerPrevTabColor.BackColor = .TabColor
            'TerPrevAcrylic.Checked = .UseAcrylic
            'TerPrevOpacity.Value = .Opacity
            'TerPrevAdjustColors.Checked = .adjustIndistinguishableColors
            'TerPrevOpacity.Value = .BackgroundImageOpacity

            Try
                If TerPrevSchemes.Items.Contains(.ColorScheme) Then TerPrevSchemes.SelectedItem = .ColorScheme Else TerPrevSchemes.SelectedItem = MainFrm.CP.TerminalPreview.DefaultProf.ColorScheme
            Catch
            End Try

            TerPrevBackImage.Text = .BackgroundImage
            TerPrevAlignment.SelectedIndex = .BackgroundImageAlignment
            TerPrevStretchMode.SelectedIndex = .BackgroundImageStretchMode

            TerPrevCursorStyle.SelectedIndex = .CursorShape
            TerPrevCursorHeightBar.Value = .CursorHeight

            TerPrevFonts.SelectedItem = .Font.Face
            TerPrevFontSizeBar.Value = .Font.Size
            TerPrevFontWeight.SelectedIndex = .Font.Weight
        End With

    End Sub

    Private Sub TerPrevFontSizeBar_Scroll(sender As Object) Handles TerPrevFontSizeBar.Scroll
        TerPrevFontSizeLbl.Text = sender.Value

        If Not _Shown Then Exit Sub

        XenonTerminal1.Font = New Font(XenonTerminal1.Font.Name, TerPrevFontSizeBar.Value, XenonTerminal1.Font.Style)

        With If(TerPrevProfiles.SelectedIndex = 0, MainFrm.CP.TerminalPreview.DefaultProf, MainFrm.CP.TerminalPreview.Profiles(TerPrevProfiles.SelectedIndex - 1))
            .Font.Size = sender.Value
        End With

    End Sub

    Private Sub TerPrevCursorHeightBar_Scroll(sender As Object) Handles TerPrevCursorHeightBar.Scroll
        XenonTerminal1.CursorHeight = sender.Value
        XenonTerminal1.Refresh()

        If Not _Shown Then Exit Sub

        With If(TerPrevProfiles.SelectedIndex = 0, MainFrm.CP.TerminalPreview.DefaultProf, MainFrm.CP.TerminalPreview.Profiles(TerPrevProfiles.SelectedIndex - 1))
            .CursorHeight = sender.Value
        End With

    End Sub

    Private Sub TerPrevImageOpacity_Scroll(sender As Object) Handles TerPrevImageOpacity.Scroll
        XenonTerminal1.Opacity = sender.Value

        'TerPrevImageOpacityLbl.Text = sender.Value

        'If Not _Shown Then Exit Sub

        'With If(TerPrevProfiles.SelectedIndex = 0, MainFrm.CP.TerminalPreview.DefaultProf, MainFrm.CP.TerminalPreview.Profiles(TerPrevProfiles.SelectedIndex - 1))
        '.CursorHeight = sender.Value
        'End With
    End Sub

    Private Sub TerPrevCursorStyle_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TerPrevCursorStyle.SelectedIndexChanged
        XenonTerminal1.CursorType = TerPrevCursorStyle.SelectedIndex
        XenonTerminal1.Refresh()

        If Not _Shown Then Exit Sub

        With If(TerPrevProfiles.SelectedIndex = 0, MainFrm.CP.TerminalPreview.DefaultProf, MainFrm.CP.TerminalPreview.Profiles(TerPrevProfiles.SelectedIndex - 1))
            .CursorShape = TerPrevCursorStyle.SelectedIndex
        End With
    End Sub

    Private Sub XenonButton12_Click(sender As Object, e As EventArgs) Handles XenonButton12.Click
        MainFrm.CP.TerminalPreview.Colors.Add(New TColor With {.Name = "New Color #" & TerPrevSchemes.Items.Count})
        FillTerminalSchemes(MainFrm.CP.TerminalPreview, TerPrevSchemes)
        TerPrevSchemes.SelectedIndex = TerPrevSchemes.Items.Count - 1
    End Sub

    Private Sub ColorClick(sender As Object, e As EventArgs) Handles TerPrevBlack.Click,
                                                                     TerPrevBlue.Click, TerPrevGreen.Click, TerPrevCyan.Click, TerPrevRed.Click, TerPrevPurple.Click, TerPrevYellow.Click,
                                                                     TerPrevWhite.Click, TerPrevBlackB.Click, TerPrevBlueB.Click, TerPrevGreenB.Click, TerPrevCyanB.Click, TerPrevRedB.Click,
                                                                     TerPrevPurpleB.Click, TerPrevYellowB.Click, TerPrevWhiteB.Click


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

    Private Sub ColorMainsClick(sender As Object, e As EventArgs) Handles TerPrevBackground.Click, TerPrevForeground.Click, TerPrevSelection.Click, TerPrevCursor.Click,
                                                                          TerPrevTabActive.Click, TerPrevTabInactive.Click, TerPrevTitlebarActive.Click, TerPrevTitlebarInactive.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            Exit Sub
        End If

        Dim CList As New List(Of Control) From {sender, XenonTerminal1, XenonTerminal2}

        Dim _Conditions As New Conditions

        If sender.Name.ToString.ToLower.Contains(TerPrevBackground.Name.ToLower) Then _Conditions.Terminal_Back = True
        If sender.Name.ToString.ToLower.Contains(TerPrevForeground.Name.ToLower) Then _Conditions.Terminal_Fore = True
        If sender.Name.ToString.ToLower.Contains(TerPrevSelection.Name.ToLower) Then _Conditions.Terminal_Selection = True
        If sender.Name.ToString.ToLower.Contains(TerPrevCursor.Name.ToLower) Then _Conditions.Terminal_Cursor = True

        If sender.Name.ToString.ToLower.Contains(TerPrevTabActive.Name.ToLower) Then _Conditions.Terminal_TabActive = True
        If sender.Name.ToString.ToLower.Contains(TerPrevTabInactive.Name.ToLower) Then _Conditions.Terminal_TabInactive = True
        If sender.Name.ToString.ToLower.Contains(TerPrevTitlebarActive.Name.ToLower) Then _Conditions.Terminal_TitlebarActive = True
        If sender.Name.ToString.ToLower.Contains(TerPrevTitlebarInactive.Name.ToLower) Then _Conditions.Terminal_TitlebarInactive = True


        Dim C As Color = ColorPickerDlg.Pick(CList, _Conditions)

        If sender.Name.ToString.ToLower.Contains(TerPrevBackground.Name.ToLower) Then
            MainFrm.CP.TerminalPreview.Colors(TerPrevSchemes.SelectedIndex).Background = C
        End If

        If sender.Name.ToString.ToLower.Contains(TerPrevForeground.Name.ToLower) Then
            MainFrm.CP.TerminalPreview.Colors(TerPrevSchemes.SelectedIndex).Foreground = C
        End If

        If sender.Name.ToString.ToLower.Contains(TerPrevSelection.Name.ToLower) Then
            MainFrm.CP.TerminalPreview.Colors(TerPrevSchemes.SelectedIndex).SelectionBackground = C
        End If

        If sender.Name.ToString.ToLower.Contains(TerPrevCursor.Name.ToLower) Then
            MainFrm.CP.TerminalPreview.Colors(TerPrevSchemes.SelectedIndex).CursorColor = C
        End If

        If sender.Name.ToString.ToLower.Contains(TerPrevTabActive.Name.ToLower) Then
            MainFrm.CP.TerminalPreview.Themes(TerPrevThemes.SelectedIndex - 3).Tab_Active = C
        End If

        If sender.Name.ToString.ToLower.Contains(TerPrevTabInactive.Name.ToLower) Then
            MainFrm.CP.TerminalPreview.Themes(TerPrevThemes.SelectedIndex - 3).Tab_Inactive = C
        End If

        If sender.Name.ToString.ToLower.Contains(TerPrevTitlebarActive.Name.ToLower) Then
            MainFrm.CP.TerminalPreview.Themes(TerPrevThemes.SelectedIndex - 3).Titlebar_Active = C
        End If

        If sender.Name.ToString.ToLower.Contains(TerPrevTitlebarInactive.Name.ToLower) Then
            MainFrm.CP.TerminalPreview.Themes(TerPrevThemes.SelectedIndex - 3).Titlebar_Inactive = C
        End If

        ApplyPreview(MainFrm.CP.TerminalPreview)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()
    End Sub

    Private Sub TerPrevFonts_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TerPrevFonts.SelectedIndexChanged
        XenonTerminal1.Font = New Font(TerPrevFonts.SelectedItem.ToString, XenonTerminal1.Font.Size, XenonTerminal1.Font.Style)
    End Sub

    Sub ApplyPreview([Terminal] As WinTerminal)
        Try
            XenonTerminal1.Color_Background = [Terminal].Colors(TerPrevSchemes.SelectedIndex).Background
            XenonTerminal1.Color_Foreground = [Terminal].Colors(TerPrevSchemes.SelectedIndex).Foreground
            XenonTerminal1.Color_Selection = [Terminal].Colors(TerPrevSchemes.SelectedIndex).SelectionBackground
            XenonTerminal1.Color_Cursor = [Terminal].Colors(TerPrevSchemes.SelectedIndex).CursorColor


            If TerPrevThemesContainer.Enabled Then
                If [Terminal].Themes(TerPrevThemes.SelectedIndex - 3).Tab_Active = Nothing Then
                    XenonTerminal1.Color_TabFocused = Nothing
                Else
                    XenonTerminal1.Color_TabFocused = [Terminal].Themes(TerPrevThemes.SelectedIndex - 3).Tab_Active
                End If

                If [Terminal].Themes(TerPrevThemes.SelectedIndex - 3).Titlebar_Active = Nothing Then
                    XenonTerminal1.Color_Titlebar = Nothing
                Else
                    XenonTerminal1.Color_Titlebar = [Terminal].Themes(TerPrevThemes.SelectedIndex - 3).Titlebar_Active
                End If

                If [Terminal].Themes(TerPrevThemes.SelectedIndex - 3).Tab_Inactive = Nothing Then
                    XenonTerminal1.Color_TabUnFocused = Nothing
                Else
                    XenonTerminal1.Color_TabUnFocused = [Terminal].Themes(TerPrevThemes.SelectedIndex - 3).Tab_Inactive
                End If

                If [Terminal].Themes(TerPrevThemes.SelectedIndex - 3).Titlebar_Inactive = Nothing Then
                    XenonTerminal2.Color_Titlebar_Unfocused = Nothing
                Else
                    XenonTerminal2.Color_Titlebar_Unfocused = [Terminal].Themes(TerPrevThemes.SelectedIndex - 3).Titlebar_Inactive
                End If
            Else
                XenonTerminal1.Color_TabFocused = Nothing
                XenonTerminal1.Color_Titlebar = Nothing
                XenonTerminal1.Color_Titlebar_Unfocused = Nothing
                XenonTerminal1.Color_TabUnFocused = Nothing
                XenonTerminal2.Color_Titlebar_Unfocused = Nothing

                If TerPrevThemes.SelectedItem IsNot Nothing Then
                    If TerPrevThemes.SelectedItem.ToString.ToLower = "dark" Then
                        XenonTerminal1.Light = False
                        XenonTerminal2.Light = False

                    ElseIf TerPrevThemes.SelectedItem.ToString.ToLower = "light" Then
                        XenonTerminal1.Light = True
                        XenonTerminal2.Light = True
                    Else
                        XenonTerminal1.Light = MainFrm.CP.AppMode_Light
                        XenonTerminal2.Light = MainFrm.CP.AppMode_Light
                    End If
                End If

            End If

            XenonTerminal1.Refresh()
            XenonTerminal2.Refresh()
        Catch ex As Exception
            MsgBox(ex.Message & vbCrLf & vbCrLf & ex.StackTrace, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub XenonButton3_Click(sender As Object, e As EventArgs) Handles XenonButton3.Click
        MainFrm.CP.TerminalPreview.Themes.Add(New ThemesList With {.Name = "New Theme #" & TerPrevThemes.Items.Count - 3})
        FillTerminalThemes(MainFrm.CP.TerminalPreview, TerPrevThemes)
        TerPrevThemes.SelectedIndex = TerPrevThemes.Items.Count - 1
    End Sub

    Private Sub TerPrevThemes_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TerPrevThemes.SelectedIndexChanged
        If Not _Shown Then Exit Sub

        Try

            If TerPrevThemes.SelectedIndex > 2 Then
                TerPrevThemesContainer.Enabled = True

                With MainFrm.CP.TerminalPreview.Themes(TerPrevThemes.SelectedIndex - 3)
                    TerPrevTitlebarActive.BackColor = .Titlebar_Active
                    TerPrevTitlebarInactive.BackColor = .Titlebar_Inactive
                    TerPrevTabActive.BackColor = .Tab_Active
                    TerPrevTabInactive.BackColor = .Tab_Inactive
                    TerPrevMode.Checked = If(.applicationTheme_light.ToLower = "light", False, True)
                End With

                ApplyPreview(MainFrm.CP.TerminalPreview)
            Else
                TerPrevThemesContainer.Enabled = False

                TerPrevTitlebarActive.BackColor = Nothing
                TerPrevTitlebarInactive.BackColor = Nothing
                TerPrevTabActive.BackColor = Nothing
                TerPrevTabInactive.BackColor = Nothing

                If TerPrevThemes.SelectedIndex = 0 Then TerPrevMode.Checked = True
                If TerPrevThemes.SelectedIndex = 1 Then TerPrevMode.Checked = False
                If TerPrevThemes.SelectedIndex = 2 Then TerPrevMode.Checked = Not MainFrm.CP.AppMode_Light

            End If

                If TerPrevThemes.SelectedItem.ToString.ToLower = "dark" Then
                MainFrm.CP.TerminalPreview.theme = "dark"

            ElseIf TerPrevThemes.SelectedItem.ToString.ToLower = "light" Then
                MainFrm.CP.TerminalPreview.theme = "light"

            ElseIf TerPrevThemes.SelectedItem.ToString.ToLower = "system" Then
                MainFrm.CP.TerminalPreview.theme = "system"

            Else
                MainFrm.CP.TerminalPreview.theme = TerPrevThemes.SelectedItem.ToString
            End If

            ApplyPreview(MainFrm.CP.TerminalPreview)
        Catch ex As Exception
            MsgBox(ex.Message & vbCrLf & vbCrLf & ex.StackTrace, MsgBoxStyle.Critical)
        End Try
    End Sub
End Class