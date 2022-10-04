Imports System.IO
Imports WinPaletter.XenonCore
Public Class WindowsTerminal
    Private _Shown As Boolean = False
    Public _Mode As WinTerminal.Version
    Public _Terminal As WinTerminal

    Private Sub WindowsTerminal_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        MainFrm.Visible = False
        Location = New Point(10, (My.Computer.Screen.Bounds.Height - Height) / 2 - 20)

        XenonCheckBox1.Checked = My.Application._Settings.Terminal_OtherFonts

        ApplyDarkMode(Me)
        _Shown = False

        Select Case _Mode
            Case WinTerminal.Version.Stable
                _Terminal = MainFrm.CP.Terminal
                Text = "(BETA) Windows Terminal - Stable Version"
                TerEnabled.Checked = MainFrm.CP.Terminal_Stable_Enabled

            Case WinTerminal.Version.Preview
                _Terminal = MainFrm.CP.TerminalPreview
                Text = "(BETA) Windows Terminal - Preview Version"
                TerEnabled.Checked = MainFrm.CP.Terminal_Preview_Enabled

            Case WinTerminal.Version.Developer
                '_Terminal = MainFrm.CP.TerminalDeveloper
                'Text = "(BETA) Windows Terminal - Developer Version"
                'TerEnabled.Checked = MainFrm.CP.Terminal_Developer_Enabled
        End Select

        FillFonts(TerFonts, Not My.Application._Settings.Terminal_OtherFonts)

        If My.W10 Or My.W11 Then
            Dim TerDir As String = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) & "\AppData\Local\Packages\Microsoft.WindowsTerminal_8wekyb3d8bbwe\LocalState\settings.json"
            Dim TerPreDir As String = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) & "\AppData\Local\Packages\Microsoft.WindowsTerminalPreview_8wekyb3d8bbwe\LocalState\settings.json"
            Dim TerDevDir As String = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) & "\AppData\Local\Packages\Microsoft.WindowsTerminalDeveloper_8wekyb3d8bbwe\LocalState\settings.json"


            If My.Application._Settings.Terminal_Bypass Then
                Load_FromTerminal()
            Else
                If IO.File.Exists(TerDir) And _Mode = WinTerminal.Version.Stable Then
                    Load_FromTerminal()
                End If

                If IO.File.Exists(TerPreDir) And _Mode = WinTerminal.Version.Preview Then
                    Load_FromTerminal()
                End If

                If IO.File.Exists(TerDevDir) And _Mode = WinTerminal.Version.Developer Then
                    Load_FromTerminal()
                End If
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
        SetDefaultsToScheme(TerSchemes.SelectedItem.ToString)

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

        Catch

        End Try
    End Sub

    Sub SetDefaultsToScheme(Scheme As String)
        Select Case Scheme.ToLower

            Case "Campbell".ToLower
                TerBackground.DefaultColor = Color.FromArgb(Convert.ToInt32("FF0C0C0C", 16))
                TerBlack.DefaultColor = Color.FromArgb(Convert.ToInt32("FF0C0C0C", 16))
                TerBlue.DefaultColor = Color.FromArgb(Convert.ToInt32("FF0037DA", 16))
                TerBlackB.DefaultColor = Color.FromArgb(Convert.ToInt32("FF767676", 16))
                TerBlueB.DefaultColor = Color.FromArgb(Convert.ToInt32("FF3B78FF", 16))
                TerCyanB.DefaultColor = Color.FromArgb(Convert.ToInt32("FF61D6D6", 16))
                TerGreenB.DefaultColor = Color.FromArgb(Convert.ToInt32("FF16C60C", 16))
                TerPurpleB.DefaultColor = Color.FromArgb(Convert.ToInt32("FFB4009E", 16))
                TerRedB.DefaultColor = Color.FromArgb(Convert.ToInt32("FFE74856", 16))
                TerWhiteB.DefaultColor = Color.FromArgb(Convert.ToInt32("FFF2F2F2", 16))
                TerYellowB.DefaultColor = Color.FromArgb(Convert.ToInt32("FFF9F1A5", 16))
                TerCursor.DefaultColor = Color.FromArgb(Convert.ToInt32("FFFFFFFF", 16))
                TerCyan.DefaultColor = Color.FromArgb(Convert.ToInt32("FF3A96DD", 16))
                TerForeground.DefaultColor = Color.FromArgb(Convert.ToInt32("FFCCCCCC", 16))
                TerGreen.DefaultColor = Color.FromArgb(Convert.ToInt32("FF13A10E", 16))
                TerPurple.DefaultColor = Color.FromArgb(Convert.ToInt32("FF881798", 16))
                TerRed.DefaultColor = Color.FromArgb(Convert.ToInt32("FFC50F1F", 16))
                TerSelection.DefaultColor = Color.FromArgb(Convert.ToInt32("FFFFFFFF", 16))
                TerWhite.DefaultColor = Color.FromArgb(Convert.ToInt32("FFCCCCCC", 16))
                TerYellow.DefaultColor = Color.FromArgb(Convert.ToInt32("FFC19C00", 16))

            Case "Campbell Powershell".ToLower
                TerBackground.DefaultColor = Color.FromArgb(Convert.ToInt32("FF012456", 16))
                TerBlack.DefaultColor = Color.FromArgb(Convert.ToInt32("FF0C0C0C", 16))
                TerBlue.DefaultColor = Color.FromArgb(Convert.ToInt32("FF0037DA", 16))
                TerBlackB.DefaultColor = Color.FromArgb(Convert.ToInt32("FF767676", 16))
                TerBlueB.DefaultColor = Color.FromArgb(Convert.ToInt32("FF3B78FF", 16))
                TerCyanB.DefaultColor = Color.FromArgb(Convert.ToInt32("FF61D6D6", 16))
                TerGreenB.DefaultColor = Color.FromArgb(Convert.ToInt32("FF16C60C", 16))
                TerPurpleB.DefaultColor = Color.FromArgb(Convert.ToInt32("FFB4009E", 16))
                TerRedB.DefaultColor = Color.FromArgb(Convert.ToInt32("FFE74856", 16))
                TerWhiteB.DefaultColor = Color.FromArgb(Convert.ToInt32("FFF2F2F2", 16))
                TerYellowB.DefaultColor = Color.FromArgb(Convert.ToInt32("FFF9F1A5", 16))
                TerCursor.DefaultColor = Color.FromArgb(Convert.ToInt32("FFFFFFFF", 16))
                TerCyan.DefaultColor = Color.FromArgb(Convert.ToInt32("FF3A96DD", 16))
                TerForeground.DefaultColor = Color.FromArgb(Convert.ToInt32("FFCCCCCC", 16))
                TerGreen.DefaultColor = Color.FromArgb(Convert.ToInt32("FF13A10E", 16))
                TerPurple.DefaultColor = Color.FromArgb(Convert.ToInt32("FF881798", 16))
                TerRed.DefaultColor = Color.FromArgb(Convert.ToInt32("FFC50F1F", 16))
                TerSelection.DefaultColor = Color.FromArgb(Convert.ToInt32("FFFFFFFF", 16))
                TerWhite.DefaultColor = Color.FromArgb(Convert.ToInt32("FFCCCCCC", 16))
                TerYellow.DefaultColor = Color.FromArgb(Convert.ToInt32("FFC19C00", 16))

            Case "One Half Dark".ToLower
                TerBackground.DefaultColor = Color.FromArgb(Convert.ToInt32("FF282C34", 16))
                TerBlack.DefaultColor = Color.FromArgb(Convert.ToInt32("FF282C34", 16))
                TerBlue.DefaultColor = Color.FromArgb(Convert.ToInt32("FF61AFEF", 16))
                TerBlackB.DefaultColor = Color.FromArgb(Convert.ToInt32("FF5A6374", 16))
                TerBlueB.DefaultColor = Color.FromArgb(Convert.ToInt32("FF61AFEF", 16))
                TerCyanB.DefaultColor = Color.FromArgb(Convert.ToInt32("FF56B6C2", 16))
                TerGreenB.DefaultColor = Color.FromArgb(Convert.ToInt32("FF98C379", 16))
                TerPurpleB.DefaultColor = Color.FromArgb(Convert.ToInt32("FFC678DD", 16))
                TerRedB.DefaultColor = Color.FromArgb(Convert.ToInt32("FFE06C75", 16))
                TerWhiteB.DefaultColor = Color.FromArgb(Convert.ToInt32("FFDCDFE4", 16))
                TerYellowB.DefaultColor = Color.FromArgb(Convert.ToInt32("FFE5C07B", 16))
                TerCursor.DefaultColor = Color.FromArgb(Convert.ToInt32("FFFFFFFF", 16))
                TerCyan.DefaultColor = Color.FromArgb(Convert.ToInt32("FF56B6C2", 16))
                TerForeground.DefaultColor = Color.FromArgb(Convert.ToInt32("FFDCDFE4", 16))
                TerGreen.DefaultColor = Color.FromArgb(Convert.ToInt32("FF98C379", 16))
                TerPurple.DefaultColor = Color.FromArgb(Convert.ToInt32("FFC678DD", 16))
                TerRed.DefaultColor = Color.FromArgb(Convert.ToInt32("FFE06C75", 16))
                TerSelection.DefaultColor = Color.FromArgb(Convert.ToInt32("FFFFFFFF", 16))
                TerWhite.DefaultColor = Color.FromArgb(Convert.ToInt32("FFDCDFE4", 16))
                TerYellow.DefaultColor = Color.FromArgb(Convert.ToInt32("FFE5C07B", 16))

            Case "One Half Light".ToLower
                TerBackground.DefaultColor = Color.FromArgb(Convert.ToInt32("FFFAFAFA", 16))
                TerBlack.DefaultColor = Color.FromArgb(Convert.ToInt32("FF383A42", 16))
                TerBlue.DefaultColor = Color.FromArgb(Convert.ToInt32("FF0184BC", 16))
                TerBlackB.DefaultColor = Color.FromArgb(Convert.ToInt32("FF4F525D", 16))
                TerBlueB.DefaultColor = Color.FromArgb(Convert.ToInt32("FF61AFEF", 16))
                TerCyanB.DefaultColor = Color.FromArgb(Convert.ToInt32("FF56B5C1", 16))
                TerGreenB.DefaultColor = Color.FromArgb(Convert.ToInt32("FF98C379", 16))
                TerPurpleB.DefaultColor = Color.FromArgb(Convert.ToInt32("FFC577DD", 16))
                TerRedB.DefaultColor = Color.FromArgb(Convert.ToInt32("FFDF6C75", 16))
                TerWhiteB.DefaultColor = Color.FromArgb(Convert.ToInt32("FFFFFFFF", 16))
                TerYellowB.DefaultColor = Color.FromArgb(Convert.ToInt32("FFE4C07A", 16))
                TerCursor.DefaultColor = Color.FromArgb(Convert.ToInt32("FF4F525D", 16))
                TerCyan.DefaultColor = Color.FromArgb(Convert.ToInt32("FF0997B3", 16))
                TerForeground.DefaultColor = Color.FromArgb(Convert.ToInt32("FF383A42", 16))
                TerGreen.DefaultColor = Color.FromArgb(Convert.ToInt32("FF50A14F", 16))
                TerPurple.DefaultColor = Color.FromArgb(Convert.ToInt32("FFA626A4", 16))
                TerRed.DefaultColor = Color.FromArgb(Convert.ToInt32("FFE45649", 16))
                TerSelection.DefaultColor = Color.FromArgb(Convert.ToInt32("FFFFFFFF", 16))
                TerWhite.DefaultColor = Color.FromArgb(Convert.ToInt32("FFFAFAFA", 16))
                TerYellow.DefaultColor = Color.FromArgb(Convert.ToInt32("FFC18301", 16))

            Case "Solarized Dark".ToLower
                TerBackground.DefaultColor = Color.FromArgb(Convert.ToInt32("FF002B36", 16))
                TerBlack.DefaultColor = Color.FromArgb(Convert.ToInt32("FF002B36", 16))
                TerBlue.DefaultColor = Color.FromArgb(Convert.ToInt32("FF268BD2", 16))
                TerBlackB.DefaultColor = Color.FromArgb(Convert.ToInt32("FF073642", 16))
                TerBlueB.DefaultColor = Color.FromArgb(Convert.ToInt32("FF839496", 16))
                TerCyanB.DefaultColor = Color.FromArgb(Convert.ToInt32("FF93A1A1", 16))
                TerGreenB.DefaultColor = Color.FromArgb(Convert.ToInt32("FF586E75", 16))
                TerPurpleB.DefaultColor = Color.FromArgb(Convert.ToInt32("FF6C71C4", 16))
                TerRedB.DefaultColor = Color.FromArgb(Convert.ToInt32("FFCB4B16", 16))
                TerWhiteB.DefaultColor = Color.FromArgb(Convert.ToInt32("FFFDF6E3", 16))
                TerYellowB.DefaultColor = Color.FromArgb(Convert.ToInt32("FF657B83", 16))
                TerCursor.DefaultColor = Color.FromArgb(Convert.ToInt32("FFFFFFFF", 16))
                TerCyan.DefaultColor = Color.FromArgb(Convert.ToInt32("FF2AA198", 16))
                TerForeground.DefaultColor = Color.FromArgb(Convert.ToInt32("FF839496", 16))
                TerGreen.DefaultColor = Color.FromArgb(Convert.ToInt32("FF859900", 16))
                TerPurple.DefaultColor = Color.FromArgb(Convert.ToInt32("FFD33682", 16))
                TerRed.DefaultColor = Color.FromArgb(Convert.ToInt32("FFDC322F", 16))
                TerSelection.DefaultColor = Color.FromArgb(Convert.ToInt32("FFFFFFFF", 16))
                TerWhite.DefaultColor = Color.FromArgb(Convert.ToInt32("FFEEE8D5", 16))
                TerYellow.DefaultColor = Color.FromArgb(Convert.ToInt32("FFB58900", 16))

            Case "Solarized Light".ToLower
                TerBackground.DefaultColor = Color.FromArgb(Convert.ToInt32("FFFDF6E3", 16))
                TerBlack.DefaultColor = Color.FromArgb(Convert.ToInt32("FF002B36", 16))
                TerBlue.DefaultColor = Color.FromArgb(Convert.ToInt32("FF268BD2", 16))
                TerBlackB.DefaultColor = Color.FromArgb(Convert.ToInt32("FF073642", 16))
                TerBlueB.DefaultColor = Color.FromArgb(Convert.ToInt32("FF839496", 16))
                TerCyanB.DefaultColor = Color.FromArgb(Convert.ToInt32("FF93A1A1", 16))
                TerGreenB.DefaultColor = Color.FromArgb(Convert.ToInt32("FF586E75", 16))
                TerPurpleB.DefaultColor = Color.FromArgb(Convert.ToInt32("FF6C71C4", 16))
                TerRedB.DefaultColor = Color.FromArgb(Convert.ToInt32("FFCB4B16", 16))
                TerWhiteB.DefaultColor = Color.FromArgb(Convert.ToInt32("FFFDF6E3", 16))
                TerYellowB.DefaultColor = Color.FromArgb(Convert.ToInt32("FF657B83", 16))
                TerCursor.DefaultColor = Color.FromArgb(Convert.ToInt32("FF002B36", 16))
                TerCyan.DefaultColor = Color.FromArgb(Convert.ToInt32("FF2AA198", 16))
                TerForeground.DefaultColor = Color.FromArgb(Convert.ToInt32("FF657B83", 16))
                TerGreen.DefaultColor = Color.FromArgb(Convert.ToInt32("FF859900", 16))
                TerPurple.DefaultColor = Color.FromArgb(Convert.ToInt32("FFD33682", 16))
                TerRed.DefaultColor = Color.FromArgb(Convert.ToInt32("FFDC322F", 16))
                TerSelection.DefaultColor = Color.FromArgb(Convert.ToInt32("FFFFFFFF", 16))
                TerWhite.DefaultColor = Color.FromArgb(Convert.ToInt32("FFEEE8D5", 16))
                TerYellow.DefaultColor = Color.FromArgb(Convert.ToInt32("FFB58900", 16))

            Case "Tango Dark".ToLower
                TerBackground.DefaultColor = Color.FromArgb(Convert.ToInt32("FF000000", 16))
                TerBlack.DefaultColor = Color.FromArgb(Convert.ToInt32("FF000000", 16))
                TerBlue.DefaultColor = Color.FromArgb(Convert.ToInt32("FF3465A4", 16))
                TerBlackB.DefaultColor = Color.FromArgb(Convert.ToInt32("FF555753", 16))
                TerBlueB.DefaultColor = Color.FromArgb(Convert.ToInt32("FF729FCF", 16))
                TerCyanB.DefaultColor = Color.FromArgb(Convert.ToInt32("FF34E2E2", 16))
                TerGreenB.DefaultColor = Color.FromArgb(Convert.ToInt32("FF8AE234", 16))
                TerPurpleB.DefaultColor = Color.FromArgb(Convert.ToInt32("FFAD7FA8", 16))
                TerRedB.DefaultColor = Color.FromArgb(Convert.ToInt32("FFEF2929", 16))
                TerWhiteB.DefaultColor = Color.FromArgb(Convert.ToInt32("FFEEEEEC", 16))
                TerYellowB.DefaultColor = Color.FromArgb(Convert.ToInt32("FFFCE94F", 16))
                TerCursor.DefaultColor = Color.FromArgb(Convert.ToInt32("FFFFFFFF", 16))
                TerCyan.DefaultColor = Color.FromArgb(Convert.ToInt32("FF06989A", 16))
                TerForeground.DefaultColor = Color.FromArgb(Convert.ToInt32("FFD3D7CF", 16))
                TerGreen.DefaultColor = Color.FromArgb(Convert.ToInt32("FF4E9A06", 16))
                TerPurple.DefaultColor = Color.FromArgb(Convert.ToInt32("FF75507B", 16))
                TerRed.DefaultColor = Color.FromArgb(Convert.ToInt32("FFCC0000", 16))
                TerSelection.DefaultColor = Color.FromArgb(Convert.ToInt32("FFFFFFFF", 16))
                TerWhite.DefaultColor = Color.FromArgb(Convert.ToInt32("FFD3D7CF", 16))
                TerYellow.DefaultColor = Color.FromArgb(Convert.ToInt32("FFC4A000", 16))

            Case "Tango Light".ToLower
                TerBackground.DefaultColor = Color.FromArgb(Convert.ToInt32("FFFFFFFF", 16))
                TerBlack.DefaultColor = Color.FromArgb(Convert.ToInt32("FF000000", 16))
                TerBlue.DefaultColor = Color.FromArgb(Convert.ToInt32("FF3465A4", 16))
                TerBlackB.DefaultColor = Color.FromArgb(Convert.ToInt32("FF555753", 16))
                TerBlueB.DefaultColor = Color.FromArgb(Convert.ToInt32("FF729FCF", 16))
                TerCyanB.DefaultColor = Color.FromArgb(Convert.ToInt32("FF34E2E2", 16))
                TerGreenB.DefaultColor = Color.FromArgb(Convert.ToInt32("FF8AE234", 16))
                TerPurpleB.DefaultColor = Color.FromArgb(Convert.ToInt32("FFAD7FA8", 16))
                TerRedB.DefaultColor = Color.FromArgb(Convert.ToInt32("FFEF2929", 16))
                TerWhiteB.DefaultColor = Color.FromArgb(Convert.ToInt32("FFEEEEEC", 16))
                TerYellowB.DefaultColor = Color.FromArgb(Convert.ToInt32("FFFCE94F", 16))
                TerCursor.DefaultColor = Color.FromArgb(Convert.ToInt32("FF000000", 16))
                TerCyan.DefaultColor = Color.FromArgb(Convert.ToInt32("FF06989A", 16))
                TerForeground.DefaultColor = Color.FromArgb(Convert.ToInt32("FF555753", 16))
                TerGreen.DefaultColor = Color.FromArgb(Convert.ToInt32("FF4E9A06", 16))
                TerPurple.DefaultColor = Color.FromArgb(Convert.ToInt32("FF75507B", 16))
                TerRed.DefaultColor = Color.FromArgb(Convert.ToInt32("FFCC0000", 16))
                TerSelection.DefaultColor = Color.FromArgb(Convert.ToInt32("FFFFFFFF", 16))
                TerWhite.DefaultColor = Color.FromArgb(Convert.ToInt32("FFD3D7CF", 16))
                TerYellow.DefaultColor = Color.FromArgb(Convert.ToInt32("FFC4A000", 16))

            Case "Vintage".ToLower
                TerBackground.DefaultColor = Color.FromArgb(Convert.ToInt32("FF000000", 16))
                TerBlack.DefaultColor = Color.FromArgb(Convert.ToInt32("FF000000", 16))
                TerBlue.DefaultColor = Color.FromArgb(Convert.ToInt32("FF000080", 16))
                TerBlackB.DefaultColor = Color.FromArgb(Convert.ToInt32("FF808080", 16))
                TerBlueB.DefaultColor = Color.FromArgb(Convert.ToInt32("FF0000FF", 16))
                TerCyanB.DefaultColor = Color.FromArgb(Convert.ToInt32("FF00FFFF", 16))
                TerGreenB.DefaultColor = Color.FromArgb(Convert.ToInt32("FF00FF00", 16))
                TerPurpleB.DefaultColor = Color.FromArgb(Convert.ToInt32("FFFF00FF", 16))
                TerRedB.DefaultColor = Color.FromArgb(Convert.ToInt32("FFFF0000", 16))
                TerWhiteB.DefaultColor = Color.FromArgb(Convert.ToInt32("FFFFFFFF", 16))
                TerYellowB.DefaultColor = Color.FromArgb(Convert.ToInt32("FFFFFF00", 16))
                TerCursor.DefaultColor = Color.FromArgb(Convert.ToInt32("FFFFFFFF", 16))
                TerCyan.DefaultColor = Color.FromArgb(Convert.ToInt32("FF008080", 16))
                TerForeground.DefaultColor = Color.FromArgb(Convert.ToInt32("FFC0C0C0", 16))
                TerGreen.DefaultColor = Color.FromArgb(Convert.ToInt32("FF008000", 16))
                TerPurple.DefaultColor = Color.FromArgb(Convert.ToInt32("FF800080", 16))
                TerRed.DefaultColor = Color.FromArgb(Convert.ToInt32("FF800000", 16))
                TerSelection.DefaultColor = Color.FromArgb(Convert.ToInt32("FFFFFFFF", 16))
                TerWhite.DefaultColor = Color.FromArgb(Convert.ToInt32("FFC0C0C0", 16))
                TerYellow.DefaultColor = Color.FromArgb(Convert.ToInt32("FF808000", 16))

            Case Else
                TerBackground.DefaultColor = Color.FromArgb(Convert.ToInt32("FF0C0C0C", 16))
                TerBlack.DefaultColor = Color.FromArgb(Convert.ToInt32("FF0C0C0C", 16))
                TerBlue.DefaultColor = Color.FromArgb(Convert.ToInt32("FF0037DA", 16))
                TerBlackB.DefaultColor = Color.FromArgb(Convert.ToInt32("FF767676", 16))
                TerBlueB.DefaultColor = Color.FromArgb(Convert.ToInt32("FF3B78FF", 16))
                TerCyanB.DefaultColor = Color.FromArgb(Convert.ToInt32("FF61D6D6", 16))
                TerGreenB.DefaultColor = Color.FromArgb(Convert.ToInt32("FF16C60C", 16))
                TerPurpleB.DefaultColor = Color.FromArgb(Convert.ToInt32("FFB4009E", 16))
                TerRedB.DefaultColor = Color.FromArgb(Convert.ToInt32("FFE74856", 16))
                TerWhiteB.DefaultColor = Color.FromArgb(Convert.ToInt32("FFF2F2F2", 16))
                TerYellowB.DefaultColor = Color.FromArgb(Convert.ToInt32("FFF9F1A5", 16))
                TerCursor.DefaultColor = Color.FromArgb(Convert.ToInt32("FFFFFFFF", 16))
                TerCyan.DefaultColor = Color.FromArgb(Convert.ToInt32("FF3A96DD", 16))
                TerForeground.DefaultColor = Color.FromArgb(Convert.ToInt32("FFCCCCCC", 16))
                TerGreen.DefaultColor = Color.FromArgb(Convert.ToInt32("FF13A10E", 16))
                TerPurple.DefaultColor = Color.FromArgb(Convert.ToInt32("FF881798", 16))
                TerRed.DefaultColor = Color.FromArgb(Convert.ToInt32("FFC50F1F", 16))
                TerSelection.DefaultColor = Color.FromArgb(Convert.ToInt32("FFFFFFFF", 16))
                TerWhite.DefaultColor = Color.FromArgb(Convert.ToInt32("FFCCCCCC", 16))
                TerYellow.DefaultColor = Color.FromArgb(Convert.ToInt32("FFC19C00", 16))

        End Select
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

            If Not String.IsNullOrEmpty(.TabTitle) Then
                XenonTerminal1.TabTitle = .TabTitle
            Else
                If Not String.IsNullOrEmpty(.Name) Then
                    XenonTerminal1.TabTitle = .Name
                ElseIf TerProfiles.SelectedIndex = 0 Then
                    XenonTerminal1.TabTitle = "Default"
                Else
                    XenonTerminal1.TabTitle = "Untitled"
                End If
            End If

            If File.Exists(.Icon) Then
                XenonTerminal1.TabIcon = Image.FromStream(New FileStream(.Icon, FileMode.Open, FileAccess.Read))

            Else
                NativeMethods.Kernel32.Wow64DisableWow64FsRedirection(IntPtr.Zero)
                Dim path As String
                If .commandline IsNot Nothing Then path = .commandline.Replace("%SystemRoot%", Environment.GetFolderPath(Environment.SpecialFolder.Windows))
                NativeMethods.Kernel32.Wow64RevertWow64FsRedirection(IntPtr.Zero)

                If File.Exists(path) Then
                    XenonTerminal1.TabIcon = NativeMethods.User32.ExtractSmallIcon(path).ToBitmap
                Else
                    XenonTerminal1.TabIcon = Nothing
                    XenonTerminal1.TabIconButItIsString = ""
                End If

            End If

        End With

        ApplyPreview(_Terminal)

    End Sub

    Private Sub TerFontSizeBar_Scroll(sender As Object) Handles TerFontSizeBar.Scroll
        TerFontSizeLbl.Text = sender.Value

        If Not _Shown Then Exit Sub

        XenonTerminal1.Font = New Font(XenonTerminal1.Font.Name, TerFontSizeBar.Value, XenonTerminal1.Font.Style)

        With If(TerProfiles.SelectedIndex = 0, _Terminal.DefaultProf, _Terminal.Profiles(TerProfiles.SelectedIndex - 1))
            .Font.Size = TerFontSizeBar.Value
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
        _Terminal.Colors.Add(New TColor With {.Name = "New Scheme #" & TerSchemes.Items.Count})
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
            Dim cx As Color = SubMenu.ShowMenu(sender, sender IsNot TerBackground And sender IsNot TerForeground And sender IsNot TerSelection And sender IsNot TerCursor)

            If My.Application.ColorEvent <> My.MyApplication.MenuEvent.None Then
                If sender.Name.ToString.ToLower.Contains(TerBackground.Name.ToLower) Then
                    _Terminal.Colors(TerSchemes.SelectedIndex).Background = cx
                End If

                If sender.Name.ToString.ToLower.Contains(TerForeground.Name.ToLower) Then
                    _Terminal.Colors(TerSchemes.SelectedIndex).Foreground = cx
                End If

                If sender.Name.ToString.ToLower.Contains(TerSelection.Name.ToLower) Then
                    _Terminal.Colors(TerSchemes.SelectedIndex).SelectionBackground = cx
                End If

                If sender.Name.ToString.ToLower.Contains(TerCursor.Name.ToLower) Then
                    _Terminal.Colors(TerSchemes.SelectedIndex).CursorColor = cx
                End If

                If sender.Name.ToString.ToLower.Contains(TerTabActive.Name.ToLower) Then
                    _Terminal.Themes(TerThemes.SelectedIndex - 3).Tab_Active = cx
                End If

                If sender.Name.ToString.ToLower.Contains(TerTabInactive.Name.ToLower) Then
                    _Terminal.Themes(TerThemes.SelectedIndex - 3).Tab_Inactive = cx
                End If

                If sender.Name.ToString.ToLower.Contains(TerTitlebarActive.Name.ToLower) Then
                    _Terminal.Themes(TerThemes.SelectedIndex - 3).Titlebar_Active = cx
                End If

                If sender.Name.ToString.ToLower.Contains(TerTitlebarInactive.Name.ToLower) Then
                    _Terminal.Themes(TerThemes.SelectedIndex - 3).Titlebar_Inactive = cx
                End If

                ApplyPreview(_Terminal)
            End If

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
        If Not _Shown Then Exit Sub

        XenonTerminal1.Font = New Font(TerFonts.SelectedItem.ToString, XenonTerminal1.Font.Size, XenonTerminal1.Font.Style)

        With If(TerProfiles.SelectedIndex = 0, _Terminal.DefaultProf, _Terminal.Profiles(TerProfiles.SelectedIndex - 1))
            .Font.Face = TerFonts.SelectedItem.ToString
        End With

    End Sub

    Sub ApplyPreview([Terminal] As WinTerminal)
        Try
            XenonTerminal1.UseAcrylicOnTitlebar = [Terminal].useAcrylicInTabRow
            XenonTerminal2.UseAcrylicOnTitlebar = [Terminal].useAcrylicInTabRow

            If TerProfiles.SelectedIndex = 0 Then
                XenonTerminal1.TabColor = [Terminal].DefaultProf.TabColor
            Else
                If [Terminal].Profiles(TerProfiles.SelectedIndex - 1).TabColor = Color.FromArgb(0, 0, 0, 0) Then
                    XenonTerminal1.TabColor = [Terminal].DefaultProf.TabColor
                Else
                    XenonTerminal1.TabColor = [Terminal].Profiles(TerProfiles.SelectedIndex - 1).TabColor
                End If
            End If

            XenonTerminal1.Color_Background = [Terminal].Colors(TerSchemes.SelectedIndex).Background
            XenonTerminal1.Color_Foreground = [Terminal].Colors(TerSchemes.SelectedIndex).Foreground
            XenonTerminal1.Color_Selection = [Terminal].Colors(TerSchemes.SelectedIndex).SelectionBackground
            XenonTerminal1.Color_Cursor = [Terminal].Colors(TerSchemes.SelectedIndex).CursorColor

            If TerThemesContainer.Enabled Then
                XenonTerminal1.Color_TabFocused = [Terminal].Themes(TerThemes.SelectedIndex - 3).Tab_Active
                XenonTerminal1.Color_Titlebar = [Terminal].Themes(TerThemes.SelectedIndex - 3).Titlebar_Active
                XenonTerminal1.Color_TabUnFocused = [Terminal].Themes(TerThemes.SelectedIndex - 3).Tab_Inactive
                XenonTerminal2.Color_Titlebar_Unfocused = [Terminal].Themes(TerThemes.SelectedIndex - 3).Titlebar_Inactive
            Else
                XenonTerminal1.Color_TabFocused = Color.FromArgb(0, 0, 0, 0)
                XenonTerminal1.Color_Titlebar = Color.FromArgb(0, 0, 0, 0)
                XenonTerminal1.Color_Titlebar_Unfocused = Color.FromArgb(0, 0, 0, 0)
                XenonTerminal1.Color_TabUnFocused = Color.FromArgb(0, 0, 0, 0)
                XenonTerminal2.Color_Titlebar_Unfocused = Color.FromArgb(0, 0, 0, 0)
            End If

            If TerThemes.SelectedItem IsNot Nothing Then
                If TerThemes.SelectedItem.ToString.ToLower = "dark" Then
                    XenonTerminal1.Light = False
                    XenonTerminal2.Light = False

                ElseIf TerThemes.SelectedItem.ToString.ToLower = "light" Then
                    XenonTerminal1.Light = True
                    XenonTerminal2.Light = True

                ElseIf TerThemes.SelectedItem.ToString.ToLower = "system" Then
                    XenonTerminal1.Light = MainFrm.CP.AppMode_Light
                    XenonTerminal2.Light = MainFrm.CP.AppMode_Light

                Else
                    XenonTerminal1.Light = Not TerMode.Checked
                    XenonTerminal2.Light = Not TerMode.Checked
                End If
            End If

            With If(TerProfiles.SelectedIndex = 0, _Terminal.DefaultProf, _Terminal.Profiles(TerProfiles.SelectedIndex - 1))
                Dim fx As New LOGFONT
                Dim f_cmd As New Font(.Font.Face, .Font.Size)
                f_cmd.ToLogFont(fx)
                fx.lfWeight = .Font.Weight * 100
                With Font.FromLogFont(fx) : f_cmd = New Font(f_cmd.Name, f_cmd.Size, .Style) : End With
                XenonTerminal1.Font = f_cmd
                XenonTerminal2.Font = f_cmd
            End With



            XenonTerminal1.Refresh()
            XenonTerminal2.Refresh()

        Catch

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

        If TerThemes.SelectedIndex > 2 Then
            TerThemesContainer.Enabled = True

            With _Terminal.Themes(TerThemes.SelectedIndex - 3)
                TerTitlebarActive.BackColor = .Titlebar_Active
                TerTitlebarInactive.BackColor = .Titlebar_Inactive
                TerTabActive.BackColor = .Tab_Active
                TerTabInactive.BackColor = .Tab_Inactive
                TerMode.Checked = If(.applicationTheme_light.ToLower = "light", False, True)
            End With

        Else
            TerThemesContainer.Enabled = False

            TerTitlebarActive.BackColor = Color.FromArgb(0, 0, 0, 0)
            TerTitlebarInactive.BackColor = Color.FromArgb(0, 0, 0, 0)
            TerTabActive.BackColor = Color.FromArgb(0, 0, 0, 0)
            TerTabInactive.BackColor = Color.FromArgb(0, 0, 0, 0)

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

        If Not _Shown Then Exit Sub

        With If(TerProfiles.SelectedIndex = 0, _Terminal.DefaultProf, _Terminal.Profiles(TerProfiles.SelectedIndex - 1))
            .UseAcrylic = TerAcrylic.Checked
        End With

    End Sub

    Private Sub XenonCheckBox1_CheckedChanged(sender As Object) Handles XenonCheckBox1.CheckedChanged
        Dim i As Object = TerFonts.SelectedItem
        FillFonts(TerFonts, Not XenonCheckBox1.Checked)
        My.Application._Settings.Terminal_OtherFonts = XenonCheckBox1.Checked
        My.Application._Settings.Save(XeSettings.Mode.Registry)
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
        Dim TerDevDir As String = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) & "\AppData\Local\Packages\Microsoft.WindowsTerminalDeveloper_8wekyb3d8bbwe"

        Select Case _Mode
            Case WinTerminal.Version.Stable
                If IO.Directory.Exists(TerDir) Then Shell("explorer.exe shell:appsFolder\Microsoft.WindowsTerminal_8wekyb3d8bbwe!App")

            Case WinTerminal.Version.Preview
                If IO.Directory.Exists(TerPreDir) Then Shell("explorer.exe shell:appsFolder\Microsoft.WindowsTerminalPreview_8wekyb3d8bbwe!App")

            Case WinTerminal.Version.Developer
                If IO.Directory.Exists(TerDevDir) Then Shell("explorer.exe shell:appsFolder\Microsoft.WindowsTerminalDeveloper_8wekyb3d8bbwe!App")

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

        If Not _Shown Then Exit Sub

        With If(TerProfiles.SelectedIndex = 0, _Terminal.DefaultProf, _Terminal.Profiles(TerProfiles.SelectedIndex - 1))
            .BackgroundImage = TerBackImage.Text
        End With

        XenonTerminal1.Invalidate()
        XenonTerminal2.Invalidate()
    End Sub

    Private Sub XenonButton16_Click(sender As Object, e As EventArgs) Handles XenonButton16.Click
        If ImgDlg.ShowDialog = DialogResult.OK Then
            TerBackImage.Text = ImgDlg.FileName
        End If
    End Sub

    Private Sub TerMode_CheckedChanged(sender As Object, e As EventArgs) Handles TerMode.CheckedChanged
        If TerThemes.SelectedIndex > 2 Then
            _Terminal.Themes(TerThemes.SelectedIndex - 3).applicationTheme_light = If(Not TerMode.Checked, "light", "dark")
        End If

        If _Shown Then ApplyPreview(_Terminal)
    End Sub

    Private Sub XenonButton1_Click(sender As Object, e As EventArgs) Handles XenonButton1.Click
        Select Case _Mode
            Case WinTerminal.Version.Stable
                MainFrm.CP.Terminal_Stable_Enabled = TerEnabled.Checked

            Case WinTerminal.Version.Preview
                MainFrm.CP.Terminal_Preview_Enabled = TerEnabled.Checked

            Case WinTerminal.Version.Developer
                MainFrm.CP.Terminal_Developer_Enabled = TerEnabled.Checked
        End Select

        DialogResult = DialogResult.OK

        Me.Close()
    End Sub

    Private Sub WindowsTerminal_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If DialogResult <> DialogResult.OK Then
            With New CP(CP.Mode.Registry)
                MainFrm.CP.Terminal = .Terminal
                MainFrm.CP.TerminalPreview = .TerminalPreview
            End With
        End If

        DialogResult = DialogResult.Cancel
    End Sub

    Private Sub XenonButton10_Click(sender As Object, e As EventArgs) Handles XenonButton10.Click

        If TerEnabled.Checked Then
            If My.W10 Or My.W11 Then
                Dim TerDir As String = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) & "\AppData\Local\Packages\Microsoft.WindowsTerminal_8wekyb3d8bbwe\LocalState\settings.json"
                Dim TerPreDir As String = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) & "\AppData\Local\Packages\Microsoft.WindowsTerminalPreview_8wekyb3d8bbwe\LocalState\settings.json"
                Dim TerDevDir As String = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) & "\AppData\Local\Packages\Microsoft.WindowsTerminalDeveloper_8wekyb3d8bbwe\LocalState\settings.json"


                If IO.File.Exists(TerDir) And _Mode = WinTerminal.Version.Stable Then
                    _Terminal.Save(TerDir, WinTerminal.Mode.JSONFile)
                End If

                If IO.File.Exists(TerPreDir) And _Mode = WinTerminal.Version.Preview Then
                    _Terminal.Save(TerPreDir, WinTerminal.Mode.JSONFile, WinTerminal.Version.Preview)
                End If

                If IO.File.Exists(TerDevDir) And _Mode = WinTerminal.Version.Developer Then
                    _Terminal.Save(TerDevDir, WinTerminal.Mode.JSONFile, WinTerminal.Version.Developer)
                End If
            End If

        Else
            MsgBox("You should enable terminal editing from the toggle above.", MsgBoxStyle.Critical + My.Application.MsgboxRt)
        End If

    End Sub

    Private Sub WindowsTerminal_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        MainFrm.Visible = True
    End Sub

    Private Sub XenonButton11_Click(sender As Object, e As EventArgs) Handles XenonButton11.Click
        If My.W10 Or My.W11 Then
            Dim TerDir As String = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) & "\AppData\Local\Packages\Microsoft.WindowsTerminal_8wekyb3d8bbwe\LocalState\settings.json"
            Dim TerPreDir As String = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) & "\AppData\Local\Packages\Microsoft.WindowsTerminalPreview_8wekyb3d8bbwe\LocalState\settings.json"
            Dim TerDevDir As String = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) & "\AppData\Local\Packages\Microsoft.WindowsTerminalDeveloper_8wekyb3d8bbwe\LocalState\settings.json"


            If IO.File.Exists(TerDir) And _Mode = WinTerminal.Version.Stable Then
                Process.Start(TerDir)
            End If

            If IO.File.Exists(TerPreDir) And _Mode = WinTerminal.Version.Preview Then
                Process.Start(TerPreDir)
            End If

            If IO.File.Exists(TerDevDir) And _Mode = WinTerminal.Version.Developer Then
                Process.Start(TerDevDir)
            End If
        End If
    End Sub

    Private Sub XenonButton9_Click(sender As Object, e As EventArgs) Handles XenonButton9.Click
        If SaveJSONDlg.ShowDialog = DialogResult.OK Then
            If My.W10 Or My.W11 Then
                Dim TerDir As String = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) & "\AppData\Local\Packages\Microsoft.WindowsTerminal_8wekyb3d8bbwe\LocalState\settings.json"
                Dim TerPreDir As String = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) & "\AppData\Local\Packages\Microsoft.WindowsTerminalPreview_8wekyb3d8bbwe\LocalState\settings.json"
                Dim TerDevDir As String = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) & "\AppData\Local\Packages\Microsoft.WindowsTerminalDeveloper_8wekyb3d8bbwe\LocalState\settings.json"

                If IO.File.Exists(TerDir) And _Mode = WinTerminal.Version.Stable Then
                    IO.File.Copy(TerDir, SaveJSONDlg.FileName)
                End If

                If IO.File.Exists(TerPreDir) And _Mode = WinTerminal.Version.Preview Then
                    IO.File.Copy(TerPreDir, SaveJSONDlg.FileName)
                End If

                If IO.File.Exists(TerDevDir) And _Mode = WinTerminal.Version.Developer Then
                    IO.File.Copy(TerDevDir, SaveJSONDlg.FileName)
                End If
            End If
        End If
    End Sub

    Private Sub XenonButton8_Click(sender As Object, e As EventArgs) Handles XenonButton8.Click
        If OpenWPTHDlg.ShowDialog = DialogResult.OK Then

            If My.W10 Or My.W11 Then
                Dim TerDir As String = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) & "\AppData\Local\Packages\Microsoft.WindowsTerminal_8wekyb3d8bbwe\LocalState\settings.json"
                Dim TerPreDir As String = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) & "\AppData\Local\Packages\Microsoft.WindowsTerminalPreview_8wekyb3d8bbwe\LocalState\settings.json"
                Dim TerDevDir As String = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) & "\AppData\Local\Packages\Microsoft.WindowsTerminalDeveloper_8wekyb3d8bbwe\LocalState\settings.json"

                If IO.File.Exists(TerDir) And _Mode = WinTerminal.Version.Stable Then
                    _Terminal = New WinTerminal(OpenWPTHDlg.FileName, WinTerminal.Mode.WinPaletterFile)
                    Load_FromTerminal()
                End If

                If IO.File.Exists(TerPreDir) And _Mode = WinTerminal.Version.Preview Then
                    _Terminal = New WinTerminal(OpenWPTHDlg.FileName, WinTerminal.Mode.WinPaletterFile, WinTerminal.Version.Preview)
                    Load_FromTerminal()
                End If

                If IO.File.Exists(TerDevDir) And _Mode = WinTerminal.Version.Developer Then
                    _Terminal = New WinTerminal(OpenWPTHDlg.FileName, WinTerminal.Mode.WinPaletterFile, WinTerminal.Version.Developer)
                    Load_FromTerminal()
                End If
            End If

        End If
    End Sub

    Private Sub XenonButton7_Click(sender As Object, e As EventArgs) Handles XenonButton7.Click
        If OpenJSONDlg.ShowDialog = DialogResult.OK Then

            Try
                If My.W10 Or My.W11 Then
                    Dim TerDir As String = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) & "\AppData\Local\Packages\Microsoft.WindowsTerminal_8wekyb3d8bbwe\LocalState\settings.json"
                    Dim TerPreDir As String = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) & "\AppData\Local\Packages\Microsoft.WindowsTerminalPreview_8wekyb3d8bbwe\LocalState\settings.json"
                    Dim TerDevDir As String = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) & "\AppData\Local\Packages\Microsoft.WindowsTerminalDeveloper_8wekyb3d8bbwe\LocalState\settings.json"

                    If IO.File.Exists(TerDir) And _Mode = WinTerminal.Version.Stable Then
                        _Terminal = New WinTerminal(OpenJSONDlg.FileName, WinTerminal.Mode.JSONFile)
                        Load_FromTerminal()
                    End If

                    If IO.File.Exists(TerPreDir) And _Mode = WinTerminal.Version.Preview Then
                        _Terminal = New WinTerminal(OpenJSONDlg.FileName, WinTerminal.Mode.JSONFile, WinTerminal.Version.Preview)
                        Load_FromTerminal()
                    End If

                    If IO.File.Exists(TerDevDir) And _Mode = WinTerminal.Version.Developer Then
                        _Terminal = New WinTerminal(OpenJSONDlg.FileName, WinTerminal.Mode.JSONFile, WinTerminal.Version.Stable)
                        Load_FromTerminal()
                    End If
                End If
            Catch ex As Exception
                MsgBox("Error occurred while reading settings file: " & vbCrLf & vbCrLf & ex.Message & vbCrLf & vbCrLf & ex.StackTrace, MsgBoxStyle.Critical + My.Application.MsgboxRt)
            End Try

        End If
    End Sub



    Private Sub XenonButton17_Click(sender As Object, e As EventArgs) Handles XenonButton17.Click

        Dim TC As New TColor With {
        .Name = TerSchemes.SelectedItem.ToString & " Clone #" & TerSchemes.Items.Count,
        .Background = _Terminal.Colors(TerSchemes.SelectedIndex).Background,
        .Black = _Terminal.Colors(TerSchemes.SelectedIndex).Black,
        .Blue = _Terminal.Colors(TerSchemes.SelectedIndex).Blue,
        .BrightBlack = _Terminal.Colors(TerSchemes.SelectedIndex).BrightBlack,
        .BrightBlue = _Terminal.Colors(TerSchemes.SelectedIndex).BrightBlue,
        .BrightCyan = _Terminal.Colors(TerSchemes.SelectedIndex).BrightCyan,
        .BrightGreen = _Terminal.Colors(TerSchemes.SelectedIndex).BrightGreen,
        .BrightPurple = _Terminal.Colors(TerSchemes.SelectedIndex).BrightPurple,
        .BrightRed = _Terminal.Colors(TerSchemes.SelectedIndex).BrightRed,
        .BrightWhite = _Terminal.Colors(TerSchemes.SelectedIndex).BrightWhite,
        .BrightYellow = _Terminal.Colors(TerSchemes.SelectedIndex).BrightYellow,
        .CursorColor = _Terminal.Colors(TerSchemes.SelectedIndex).CursorColor,
        .Cyan = _Terminal.Colors(TerSchemes.SelectedIndex).Cyan,
        .Foreground = _Terminal.Colors(TerSchemes.SelectedIndex).Foreground,
        .Green = _Terminal.Colors(TerSchemes.SelectedIndex).Green,
        .Purple = _Terminal.Colors(TerSchemes.SelectedIndex).Purple,
        .Red = _Terminal.Colors(TerSchemes.SelectedIndex).Red,
        .SelectionBackground = _Terminal.Colors(TerSchemes.SelectedIndex).SelectionBackground,
        .White = _Terminal.Colors(TerSchemes.SelectedIndex).White,
        .Yellow = _Terminal.Colors(TerSchemes.SelectedIndex).Yellow}

        _Terminal.Colors.Add(TC)
        FillTerminalSchemes(_Terminal, TerSchemes)
        TerSchemes.SelectedIndex = TerSchemes.Items.Count - 1
    End Sub

    Private Sub XenonButton18_Click(sender As Object, e As EventArgs) Handles XenonButton18.Click

        If TerProfiles.SelectedIndex = 0 Then
            MsgBox("Default Profile isn't cloneable, select a different profile.", MsgBoxStyle.Critical + My.Application.MsgboxRt)
            Exit Sub
        End If

        Dim P As New ProfilesList With {
            .Name = _Terminal.Profiles(TerProfiles.SelectedIndex - 1).Name & " Clone #" & TerProfiles.Items.Count,
            .BackgroundImage = _Terminal.Profiles(TerProfiles.SelectedIndex - 1).BackgroundImage,
            .BackgroundImageOpacity = _Terminal.Profiles(TerProfiles.SelectedIndex - 1).BackgroundImageOpacity,
            .ColorScheme = _Terminal.Profiles(TerProfiles.SelectedIndex - 1).ColorScheme,
            .commandline = _Terminal.Profiles(TerProfiles.SelectedIndex - 1).commandline,
            .CursorHeight = _Terminal.Profiles(TerProfiles.SelectedIndex - 1).CursorHeight,
            .CursorShape = _Terminal.Profiles(TerProfiles.SelectedIndex - 1).CursorShape,
            .Font = _Terminal.Profiles(TerProfiles.SelectedIndex - 1).Font,
            .Icon = _Terminal.Profiles(TerProfiles.SelectedIndex - 1).Icon,
            .Opacity = _Terminal.Profiles(TerProfiles.SelectedIndex - 1).Opacity,
            .Source = _Terminal.Profiles(TerProfiles.SelectedIndex - 1).Source,
            .TabColor = _Terminal.Profiles(TerProfiles.SelectedIndex - 1).TabColor,
            .TabTitle = _Terminal.Profiles(TerProfiles.SelectedIndex - 1).TabTitle,
            .UseAcrylic = _Terminal.Profiles(TerProfiles.SelectedIndex - 1).UseAcrylic
        }

        _Terminal.Profiles.Add(P)
        FillTerminalProfiles(_Terminal, TerProfiles)
        TerProfiles.SelectedIndex = TerProfiles.Items.Count - 1

    End Sub

    Private Sub XenonButton19_Click(sender As Object, e As EventArgs) Handles XenonButton19.Click
        If TerThemes.SelectedIndex < 3 Then
            MsgBox("Default themes (Dark\Light\System) are not cloneable, select a different theme or create a new theme if you want to clone.", MsgBoxStyle.Critical + My.Application.MsgboxRt)
            Exit Sub
        End If

        Dim Th As New ThemesList With {
            .Name = _Terminal.Themes(TerThemes.SelectedIndex - 3).Name & " Clone #" & TerThemes.Items.Count,
            .applicationTheme_light = _Terminal.Themes(TerThemes.SelectedIndex - 3).applicationTheme_light,
            .Tab_Active = _Terminal.Themes(TerThemes.SelectedIndex - 3).Tab_Active,
            .Tab_Inactive = _Terminal.Themes(TerThemes.SelectedIndex - 3).Titlebar_Inactive,
            .Titlebar_Active = _Terminal.Themes(TerThemes.SelectedIndex - 3).Titlebar_Active,
            .Titlebar_Inactive = _Terminal.Themes(TerThemes.SelectedIndex - 3).Titlebar_Inactive
        }

        _Terminal.Themes.Add(Th)
        FillTerminalThemes(_Terminal, TerThemes)
        TerThemes.SelectedIndex = TerThemes.Items.Count - 1
    End Sub

    Private Sub XenonButton6_Click(sender As Object, e As EventArgs)

    End Sub
End Class