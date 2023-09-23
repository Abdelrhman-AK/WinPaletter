Imports System.ComponentModel
Imports System.IO
Imports WinPaletter.PreviewHelpers

Public Class WindowsTerminal
    Private _Shown As Boolean = False
    Public _Mode As WinTerminal.Version
    Public _Terminal As WinTerminal
    Public _TerminalDefault As WinTerminal

    Private Sub WindowsTerminal_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CheckBox1.Checked = My.Settings.WindowsTerminals.ListAllFonts

        LoadLanguage
        ApplyStyle(Me)
        _Shown = False

        Select Case _Mode
            Case WinTerminal.Version.Stable
                _Terminal = My.CP.Terminal
                _TerminalDefault = My.CP.Terminal
                Text = My.Lang.TerminalStable
                TerEnabled.Checked = My.CP.Terminal.Enabled

            Case WinTerminal.Version.Preview
                _Terminal = My.CP.TerminalPreview
                _TerminalDefault = My.CP.TerminalPreview

                Text = My.Lang.TerminalPreview
                TerEnabled.Checked = My.CP.TerminalPreview.Enabled

        End Select

        Load_FromTerminal()

    End Sub

    Protected Overrides Sub OnDragOver(drgevent As DragEventArgs)
        If TypeOf drgevent.Data.GetData("WinPaletter.UI.Controllers.ColorItem") Is UI.Controllers.ColorItem Then
            Focus()
            BringToFront()
        Else
            Exit Sub
        End If

        MyBase.OnDragOver(drgevent)
    End Sub

    Sub Load_FromTerminal()

        FillTerminalSchemes(_Terminal, TerSchemes)
        FillTerminalThemes(_Terminal, TerThemes)
        FillTerminalProfiles(_Terminal, TerProfiles)

        TerProfiles.SelectedIndex = 0

        Terminal1.PreviewVersion = (_Mode = WinTerminal.Version.Preview)
        Terminal2.PreviewVersion = (_Mode = WinTerminal.Version.Preview)

        If _Terminal.Theme.ToLower = "dark" Then
            TerThemes.SelectedIndex = 0
            TerTitlebarActive.BackColor = Nothing
            TerTitlebarInactive.BackColor = Nothing
            TerTabActive.BackColor = Nothing
            TerTabInactive.BackColor = Nothing
            TerMode.Checked = True
            Terminal1.Light = False
            Terminal2.Light = False

        ElseIf _Terminal.Theme.ToLower = "light" Then
            TerThemes.SelectedIndex = 1
            TerTitlebarActive.BackColor = Nothing
            TerTitlebarInactive.BackColor = Nothing
            TerTabActive.BackColor = Nothing
            TerTabInactive.BackColor = Nothing
            TerMode.Checked = False
            Terminal1.Light = True
            Terminal2.Light = True

        ElseIf _Terminal.Theme.ToLower = "system" Then
            TerThemes.SelectedIndex = 2
            TerTitlebarActive.BackColor = Nothing
            TerTitlebarInactive.BackColor = Nothing
            TerTabActive.BackColor = Nothing
            TerTabInactive.BackColor = Nothing

            Select Case My.PreviewStyle
                Case WindowStyle.W11
                    TerMode.Checked = Not My.CP.Windows11.AppMode_Light
                    Terminal1.Light = My.CP.Windows11.AppMode_Light
                    Terminal2.Light = My.CP.Windows11.AppMode_Light

                Case WindowStyle.W10
                    TerMode.Checked = Not My.CP.Windows10.AppMode_Light
                    Terminal1.Light = My.CP.Windows10.AppMode_Light
                    Terminal2.Light = My.CP.Windows10.AppMode_Light

                Case Else
                    TerMode.Checked = Not My.CP.Windows11.AppMode_Light
                    Terminal1.Light = My.CP.Windows11.AppMode_Light
                    Terminal2.Light = My.CP.Windows11.AppMode_Light
            End Select


        ElseIf TerThemes.Items.Contains(_Terminal.Theme) Then

            TerThemes.SelectedItem = _Terminal.Theme

            TerThemesContainer.Enabled = True

            With _Terminal.Themes(TerThemes.SelectedIndex - 3)
                TerTitlebarActive.BackColor = .Titlebar_Active
                TerTitlebarInactive.BackColor = .Titlebar_Inactive
                TerTabActive.BackColor = .Tab_Active
                TerTabInactive.BackColor = .Tab_Inactive
                TerMode.Checked = Not (.ApplicationTheme_light.ToLower = "light")
                Terminal1.Light = Not (.ApplicationTheme_light.ToLower = "light")
                Terminal2.Light = Not (.ApplicationTheme_light.ToLower = "light")
            End With

        End If

        ApplyPreview(_Terminal)
    End Sub

    Private Sub WindowsTerminal_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        _Shown = True
    End Sub

    Sub FillTerminalSchemes(Terminal As WinTerminal, Combobox As ComboBox)
        Combobox.Items.Clear()

        If Terminal.Colors.Count > 0 Then
            For x = 0 To Terminal.Colors.Count - 1
                Combobox.Items.Add(Terminal.Colors(x).Name)
            Next
        End If

    End Sub

    Sub FillTerminalThemes(Terminal As WinTerminal, Combobox As ComboBox)
        Combobox.Items.Clear()

        Combobox.Items.Add("Dark")
        Combobox.Items.Add("Light")
        Combobox.Items.Add("System")

        If Terminal.Themes.Count > 0 Then
            For x = 0 To Terminal.Themes.Count - 1
                Combobox.Items.Add(Terminal.Themes(x).Name)
            Next
        End If

    End Sub

    Sub FillTerminalProfiles(Terminal As WinTerminal, Combobox As ComboBox)
        Combobox.Items.Clear()
        Combobox.Items.Add("Default")

        If Terminal.Profiles.Count > 0 Then
            For x = 0 To Terminal.Profiles.Count - 1
                Combobox.Items.Add(Terminal.Profiles(x).Name)
            Next
        End If

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
                TerBackground.DefaultColor = "FF0C0C0C".FromHEXToColor(True)
                TerBlack.DefaultColor = "FF0C0C0C".FromHEXToColor(True)
                TerBlue.DefaultColor = "FF0037DA".FromHEXToColor(True)
                TerBlackB.DefaultColor = "FF767676".FromHEXToColor(True)
                TerBlueB.DefaultColor = "FF3B78FF".FromHEXToColor(True)
                TerCyanB.DefaultColor = "FF61D6D6".FromHEXToColor(True)
                TerGreenB.DefaultColor = "FF16C60C".FromHEXToColor(True)
                TerPurpleB.DefaultColor = "FFB4009E".FromHEXToColor(True)
                TerRedB.DefaultColor = "FFE74856".FromHEXToColor(True)
                TerWhiteB.DefaultColor = "FFF2F2F2".FromHEXToColor(True)
                TerYellowB.DefaultColor = "FFF9F1A5".FromHEXToColor(True)
                TerCursor.DefaultColor = "FFFFFFFF".FromHEXToColor(True)
                TerCyan.DefaultColor = "FF3A96DD".FromHEXToColor(True)
                TerForeground.DefaultColor = "FFCCCCCC".FromHEXToColor(True)
                TerGreen.DefaultColor = "FF13A10E".FromHEXToColor(True)
                TerPurple.DefaultColor = "FF881798".FromHEXToColor(True)
                TerRed.DefaultColor = "FFC50F1F".FromHEXToColor(True)
                TerSelection.DefaultColor = "FFFFFFFF".FromHEXToColor(True)
                TerWhite.DefaultColor = "FFCCCCCC".FromHEXToColor(True)
                TerYellow.DefaultColor = "FFC19C00".FromHEXToColor(True)

            Case "Campbell Powershell".ToLower
                TerBackground.DefaultColor = "FF012456".FromHEXToColor(True)
                TerBlack.DefaultColor = "FF0C0C0C".FromHEXToColor(True)
                TerBlue.DefaultColor = "FF0037DA".FromHEXToColor(True)
                TerBlackB.DefaultColor = "FF767676".FromHEXToColor(True)
                TerBlueB.DefaultColor = "FF3B78FF".FromHEXToColor(True)
                TerCyanB.DefaultColor = "FF61D6D6".FromHEXToColor(True)
                TerGreenB.DefaultColor = "FF16C60C".FromHEXToColor(True)
                TerPurpleB.DefaultColor = "FFB4009E".FromHEXToColor(True)
                TerRedB.DefaultColor = "FFE74856".FromHEXToColor(True)
                TerWhiteB.DefaultColor = "FFF2F2F2".FromHEXToColor(True)
                TerYellowB.DefaultColor = "FFF9F1A5".FromHEXToColor(True)
                TerCursor.DefaultColor = "FFFFFFFF".FromHEXToColor(True)
                TerCyan.DefaultColor = "FF3A96DD".FromHEXToColor(True)
                TerForeground.DefaultColor = "FFCCCCCC".FromHEXToColor(True)
                TerGreen.DefaultColor = "FF13A10E".FromHEXToColor(True)
                TerPurple.DefaultColor = "FF881798".FromHEXToColor(True)
                TerRed.DefaultColor = "FFC50F1F".FromHEXToColor(True)
                TerSelection.DefaultColor = "FFFFFFFF".FromHEXToColor(True)
                TerWhite.DefaultColor = "FFCCCCCC".FromHEXToColor(True)
                TerYellow.DefaultColor = "FFC19C00".FromHEXToColor(True)

            Case "One Half Dark".ToLower
                TerBackground.DefaultColor = "FF282C34".FromHEXToColor(True)
                TerBlack.DefaultColor = "FF282C34".FromHEXToColor(True)
                TerBlue.DefaultColor = "FF61AFEF".FromHEXToColor(True)
                TerBlackB.DefaultColor = "FF5A6374".FromHEXToColor(True)
                TerBlueB.DefaultColor = "FF61AFEF".FromHEXToColor(True)
                TerCyanB.DefaultColor = "FF56B6C2".FromHEXToColor(True)
                TerGreenB.DefaultColor = "FF98C379".FromHEXToColor(True)
                TerPurpleB.DefaultColor = "FFC678DD".FromHEXToColor(True)
                TerRedB.DefaultColor = "FFE06C75".FromHEXToColor(True)
                TerWhiteB.DefaultColor = "FFDCDFE4".FromHEXToColor(True)
                TerYellowB.DefaultColor = "FFE5C07B".FromHEXToColor(True)
                TerCursor.DefaultColor = "FFFFFFFF".FromHEXToColor(True)
                TerCyan.DefaultColor = "FF56B6C2".FromHEXToColor(True)
                TerForeground.DefaultColor = "FFDCDFE4".FromHEXToColor(True)
                TerGreen.DefaultColor = "FF98C379".FromHEXToColor(True)
                TerPurple.DefaultColor = "FFC678DD".FromHEXToColor(True)
                TerRed.DefaultColor = "FFE06C75".FromHEXToColor(True)
                TerSelection.DefaultColor = "FFFFFFFF".FromHEXToColor(True)
                TerWhite.DefaultColor = "FFDCDFE4".FromHEXToColor(True)
                TerYellow.DefaultColor = "FFE5C07B".FromHEXToColor(True)

            Case "One Half Light".ToLower
                TerBackground.DefaultColor = "FFFAFAFA".FromHEXToColor(True)
                TerBlack.DefaultColor = "FF383A42".FromHEXToColor(True)
                TerBlue.DefaultColor = "FF0184BC".FromHEXToColor(True)
                TerBlackB.DefaultColor = "FF4F525D".FromHEXToColor(True)
                TerBlueB.DefaultColor = "FF61AFEF".FromHEXToColor(True)
                TerCyanB.DefaultColor = "FF56B5C1".FromHEXToColor(True)
                TerGreenB.DefaultColor = "FF98C379".FromHEXToColor(True)
                TerPurpleB.DefaultColor = "FFC577DD".FromHEXToColor(True)
                TerRedB.DefaultColor = "FFDF6C75".FromHEXToColor(True)
                TerWhiteB.DefaultColor = "FFFFFFFF".FromHEXToColor(True)
                TerYellowB.DefaultColor = "FFE4C07A".FromHEXToColor(True)
                TerCursor.DefaultColor = "FF4F525D".FromHEXToColor(True)
                TerCyan.DefaultColor = "FF0997B3".FromHEXToColor(True)
                TerForeground.DefaultColor = "FF383A42".FromHEXToColor(True)
                TerGreen.DefaultColor = "FF50A14F".FromHEXToColor(True)
                TerPurple.DefaultColor = "FFA626A4".FromHEXToColor(True)
                TerRed.DefaultColor = "FFE45649".FromHEXToColor(True)
                TerSelection.DefaultColor = "FFFFFFFF".FromHEXToColor(True)
                TerWhite.DefaultColor = "FFFAFAFA".FromHEXToColor(True)
                TerYellow.DefaultColor = "FFC18301".FromHEXToColor(True)

            Case "Solarized Dark".ToLower
                TerBackground.DefaultColor = "FF002B36".FromHEXToColor(True)
                TerBlack.DefaultColor = "FF002B36".FromHEXToColor(True)
                TerBlue.DefaultColor = "FF268BD2".FromHEXToColor(True)
                TerBlackB.DefaultColor = "FF073642".FromHEXToColor(True)
                TerBlueB.DefaultColor = "FF839496".FromHEXToColor(True)
                TerCyanB.DefaultColor = "FF93A1A1".FromHEXToColor(True)
                TerGreenB.DefaultColor = "FF586E75".FromHEXToColor(True)
                TerPurpleB.DefaultColor = "FF6C71C4".FromHEXToColor(True)
                TerRedB.DefaultColor = "FFCB4B16".FromHEXToColor(True)
                TerWhiteB.DefaultColor = "FFFDF6E3".FromHEXToColor(True)
                TerYellowB.DefaultColor = "FF657B83".FromHEXToColor(True)
                TerCursor.DefaultColor = "FFFFFFFF".FromHEXToColor(True)
                TerCyan.DefaultColor = "FF2AA198".FromHEXToColor(True)
                TerForeground.DefaultColor = "FF839496".FromHEXToColor(True)
                TerGreen.DefaultColor = "FF859900".FromHEXToColor(True)
                TerPurple.DefaultColor = "FFD33682".FromHEXToColor(True)
                TerRed.DefaultColor = "FFDC322F".FromHEXToColor(True)
                TerSelection.DefaultColor = "FFFFFFFF".FromHEXToColor(True)
                TerWhite.DefaultColor = "FFEEE8D5".FromHEXToColor(True)
                TerYellow.DefaultColor = "FFB58900".FromHEXToColor(True)

            Case "Solarized Light".ToLower
                TerBackground.DefaultColor = "FFFDF6E3".FromHEXToColor(True)
                TerBlack.DefaultColor = "FF002B36".FromHEXToColor(True)
                TerBlue.DefaultColor = "FF268BD2".FromHEXToColor(True)
                TerBlackB.DefaultColor = "FF073642".FromHEXToColor(True)
                TerBlueB.DefaultColor = "FF839496".FromHEXToColor(True)
                TerCyanB.DefaultColor = "FF93A1A1".FromHEXToColor(True)
                TerGreenB.DefaultColor = "FF586E75".FromHEXToColor(True)
                TerPurpleB.DefaultColor = "FF6C71C4".FromHEXToColor(True)
                TerRedB.DefaultColor = "FFCB4B16".FromHEXToColor(True)
                TerWhiteB.DefaultColor = "FFFDF6E3".FromHEXToColor(True)
                TerYellowB.DefaultColor = "FF657B83".FromHEXToColor(True)
                TerCursor.DefaultColor = "FF002B36".FromHEXToColor(True)
                TerCyan.DefaultColor = "FF2AA198".FromHEXToColor(True)
                TerForeground.DefaultColor = "FF657B83".FromHEXToColor(True)
                TerGreen.DefaultColor = "FF859900".FromHEXToColor(True)
                TerPurple.DefaultColor = "FFD33682".FromHEXToColor(True)
                TerRed.DefaultColor = "FFDC322F".FromHEXToColor(True)
                TerSelection.DefaultColor = "FFFFFFFF".FromHEXToColor(True)
                TerWhite.DefaultColor = "FFEEE8D5".FromHEXToColor(True)
                TerYellow.DefaultColor = "FFB58900".FromHEXToColor(True)

            Case "Tango Dark".ToLower
                TerBackground.DefaultColor = "FF000000".FromHEXToColor(True)
                TerBlack.DefaultColor = "FF000000".FromHEXToColor(True)
                TerBlue.DefaultColor = "FF3465A4".FromHEXToColor(True)
                TerBlackB.DefaultColor = "FF555753".FromHEXToColor(True)
                TerBlueB.DefaultColor = "FF729FCF".FromHEXToColor(True)
                TerCyanB.DefaultColor = "FF34E2E2".FromHEXToColor(True)
                TerGreenB.DefaultColor = "FF8AE234".FromHEXToColor(True)
                TerPurpleB.DefaultColor = "FFAD7FA8".FromHEXToColor(True)
                TerRedB.DefaultColor = "FFEF2929".FromHEXToColor(True)
                TerWhiteB.DefaultColor = "FFEEEEEC".FromHEXToColor(True)
                TerYellowB.DefaultColor = "FFFCE94F".FromHEXToColor(True)
                TerCursor.DefaultColor = "FFFFFFFF".FromHEXToColor(True)
                TerCyan.DefaultColor = "FF06989A".FromHEXToColor(True)
                TerForeground.DefaultColor = "FFD3D7CF".FromHEXToColor(True)
                TerGreen.DefaultColor = "FF4E9A06".FromHEXToColor(True)
                TerPurple.DefaultColor = "FF75507B".FromHEXToColor(True)
                TerRed.DefaultColor = "FFCC0000".FromHEXToColor(True)
                TerSelection.DefaultColor = "FFFFFFFF".FromHEXToColor(True)
                TerWhite.DefaultColor = "FFD3D7CF".FromHEXToColor(True)
                TerYellow.DefaultColor = "FFC4A000".FromHEXToColor(True)

            Case "Tango Light".ToLower
                TerBackground.DefaultColor = "FFFFFFFF".FromHEXToColor(True)
                TerBlack.DefaultColor = "FF000000".FromHEXToColor(True)
                TerBlue.DefaultColor = "FF3465A4".FromHEXToColor(True)
                TerBlackB.DefaultColor = "FF555753".FromHEXToColor(True)
                TerBlueB.DefaultColor = "FF729FCF".FromHEXToColor(True)
                TerCyanB.DefaultColor = "FF34E2E2".FromHEXToColor(True)
                TerGreenB.DefaultColor = "FF8AE234".FromHEXToColor(True)
                TerPurpleB.DefaultColor = "FFAD7FA8".FromHEXToColor(True)
                TerRedB.DefaultColor = "FFEF2929".FromHEXToColor(True)
                TerWhiteB.DefaultColor = "FFEEEEEC".FromHEXToColor(True)
                TerYellowB.DefaultColor = "FFFCE94F".FromHEXToColor(True)
                TerCursor.DefaultColor = "FF000000".FromHEXToColor(True)
                TerCyan.DefaultColor = "FF06989A".FromHEXToColor(True)
                TerForeground.DefaultColor = "FF555753".FromHEXToColor(True)
                TerGreen.DefaultColor = "FF4E9A06".FromHEXToColor(True)
                TerPurple.DefaultColor = "FF75507B".FromHEXToColor(True)
                TerRed.DefaultColor = "FFCC0000".FromHEXToColor(True)
                TerSelection.DefaultColor = "FFFFFFFF".FromHEXToColor(True)
                TerWhite.DefaultColor = "FFD3D7CF".FromHEXToColor(True)
                TerYellow.DefaultColor = "FFC4A000".FromHEXToColor(True)

            Case "Vintage".ToLower
                TerBackground.DefaultColor = "FF000000".FromHEXToColor(True)
                TerBlack.DefaultColor = "FF000000".FromHEXToColor(True)
                TerBlue.DefaultColor = "FF000080".FromHEXToColor(True)
                TerBlackB.DefaultColor = "FF808080".FromHEXToColor(True)
                TerBlueB.DefaultColor = "FF0000FF".FromHEXToColor(True)
                TerCyanB.DefaultColor = "FF00FFFF".FromHEXToColor(True)
                TerGreenB.DefaultColor = "FF00FF00".FromHEXToColor(True)
                TerPurpleB.DefaultColor = "FFFF00FF".FromHEXToColor(True)
                TerRedB.DefaultColor = "FFFF0000".FromHEXToColor(True)
                TerWhiteB.DefaultColor = "FFFFFFFF".FromHEXToColor(True)
                TerYellowB.DefaultColor = "FFFFFF00".FromHEXToColor(True)
                TerCursor.DefaultColor = "FFFFFFFF".FromHEXToColor(True)
                TerCyan.DefaultColor = "FF008080".FromHEXToColor(True)
                TerForeground.DefaultColor = "FFC0C0C0".FromHEXToColor(True)
                TerGreen.DefaultColor = "FF008000".FromHEXToColor(True)
                TerPurple.DefaultColor = "FF800080".FromHEXToColor(True)
                TerRed.DefaultColor = "FF800000".FromHEXToColor(True)
                TerSelection.DefaultColor = "FFFFFFFF".FromHEXToColor(True)
                TerWhite.DefaultColor = "FFC0C0C0".FromHEXToColor(True)
                TerYellow.DefaultColor = "FF808000".FromHEXToColor(True)

            Case Else
                TerBackground.DefaultColor = "FF0C0C0C".FromHEXToColor(True)
                TerBlack.DefaultColor = "FF0C0C0C".FromHEXToColor(True)
                TerBlue.DefaultColor = "FF0037DA".FromHEXToColor(True)
                TerBlackB.DefaultColor = "FF767676".FromHEXToColor(True)
                TerBlueB.DefaultColor = "FF3B78FF".FromHEXToColor(True)
                TerCyanB.DefaultColor = "FF61D6D6".FromHEXToColor(True)
                TerGreenB.DefaultColor = "FF16C60C".FromHEXToColor(True)
                TerPurpleB.DefaultColor = "FFB4009E".FromHEXToColor(True)
                TerRedB.DefaultColor = "FFE74856".FromHEXToColor(True)
                TerWhiteB.DefaultColor = "FFF2F2F2".FromHEXToColor(True)
                TerYellowB.DefaultColor = "FFF9F1A5".FromHEXToColor(True)
                TerCursor.DefaultColor = "FFFFFFFF".FromHEXToColor(True)
                TerCyan.DefaultColor = "FF3A96DD".FromHEXToColor(True)
                TerForeground.DefaultColor = "FFCCCCCC".FromHEXToColor(True)
                TerGreen.DefaultColor = "FF13A10E".FromHEXToColor(True)
                TerPurple.DefaultColor = "FF881798".FromHEXToColor(True)
                TerRed.DefaultColor = "FFC50F1F".FromHEXToColor(True)
                TerSelection.DefaultColor = "FFFFFFFF".FromHEXToColor(True)
                TerWhite.DefaultColor = "FFCCCCCC".FromHEXToColor(True)
                TerYellow.DefaultColor = "FFC19C00".FromHEXToColor(True)

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

            TerFontName.Text = .Font.Face
            Dim fx As New LogFont
            Dim f_cmd As New Font(.Font.Face, .Font.Size)
            f_cmd.ToLogFont(fx)
            fx.lfWeight = .Font.Weight * 100
            f_cmd = New Font(f_cmd.Name, f_cmd.Size, Font.FromLogFont(fx).Style)
            TerFontName.Font = New Font(f_cmd.Name, 9, f_cmd.Style)

            TerFontSizeBar.Value = .Font.Size
            TerFontWeight.SelectedIndex = .Font.Weight

            TerAcrylic.Checked = .UseAcrylic
            TerOpacityBar.Value = .Opacity

            Terminal1.Opacity = .Opacity
            Terminal1.OpacityBackImage = .BackgroundImageOpacity * 100

            If Not String.IsNullOrEmpty(.TabTitle) Then
                Terminal1.TabTitle = .TabTitle
            Else
                If Not String.IsNullOrEmpty(.Name) Then
                    Terminal1.TabTitle = .Name
                ElseIf TerProfiles.SelectedIndex = 0 Then
                    Terminal1.TabTitle = My.Lang.Default
                Else
                    Terminal1.TabTitle = My.Lang.Untitled
                End If
            End If

            If File.Exists(.Icon) Then
                Terminal1.TabIcon = Bitmap_Mgr.Load(.Icon)

            Else
                NativeMethods.Kernel32.Wow64DisableWow64FsRedirection(IntPtr.Zero)
                Dim path As String = ""
                If .Commandline IsNot Nothing Then path = .Commandline.Replace("%SystemRoot%", My.PATH_Windows)
                NativeMethods.Kernel32.Wow64RevertWow64FsRedirection(IntPtr.Zero)

                If File.Exists(path) Then
                    Terminal1.TabIcon = NativeMethods.DLLFunc.ExtractSmallIcon(path).ToBitmap
                Else
                    Terminal1.TabIcon = Nothing
                    Terminal1.TabIconButItIsString = ""
                End If

            End If

        End With

        ApplyPreview(_Terminal)

    End Sub

    Private Sub TerFontSizeBar_Scroll(sender As Object) Handles TerFontSizeBar.Scroll
        If Not _Shown Then Exit Sub

        TerFontSizeVal.Text = sender.Value

        Terminal1.Font = New Font(Terminal1.Font.Name, TerFontSizeBar.Value, Terminal1.Font.Style)

        With If(TerProfiles.SelectedIndex = 0, _Terminal.DefaultProf, _Terminal.Profiles(TerProfiles.SelectedIndex - 1))
            .Font.Size = TerFontSizeBar.Value
        End With
    End Sub

    Private Sub TerCursorHeightBar_Scroll(sender As Object) Handles TerCursorHeightBar.Scroll
        Terminal1.CursorHeight = sender.Value
        Terminal1.Refresh()
        TerCursorHeightVal.Text = TerCursorHeightBar.Value
        Terminal1.Refresh()

        If Not _Shown Then Exit Sub

        With If(TerProfiles.SelectedIndex = 0, _Terminal.DefaultProf, _Terminal.Profiles(TerProfiles.SelectedIndex - 1))
            .CursorHeight = sender.Value
        End With
    End Sub

    Private Sub TerImageOpacity_Scroll(sender As Object) Handles TerImageOpacity.Scroll
        TerImageOpacityVal.Text = sender.Value

        Terminal1.OpacityBackImage = TerImageOpacity.Value

        If Not _Shown Then Exit Sub


        With If(TerProfiles.SelectedIndex = 0, _Terminal.DefaultProf, _Terminal.Profiles(TerProfiles.SelectedIndex - 1))
            .BackgroundImageOpacity = TerImageOpacity.Value / 100
        End With
    End Sub

    Private Sub TerCursorStyle_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TerCursorStyle.SelectedIndexChanged
        Terminal1.CursorType = TerCursorStyle.SelectedIndex
        Terminal1.Refresh()

        If Not _Shown Then Exit Sub

        With If(TerProfiles.SelectedIndex = 0, _Terminal.DefaultProf, _Terminal.Profiles(TerProfiles.SelectedIndex - 1))
            .CursorShape = TerCursorStyle.SelectedIndex
        End With
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        _Terminal.Colors.Add(New TColor With {.Name = My.Lang.Terminal_NewScheme & " #" & TerSchemes.Items.Count})
        FillTerminalSchemes(_Terminal, TerSchemes)
        TerSchemes.SelectedIndex = TerSchemes.Items.Count - 1
    End Sub

    Private Sub ColorClick(sender As Object, e As EventArgs) Handles TerBlack.Click,
                                                                     TerBlue.Click, TerGreen.Click, TerCyan.Click, TerRed.Click, TerPurple.Click, TerYellow.Click,
                                                                     TerWhite.Click, TerBlackB.Click, TerBlueB.Click, TerGreenB.Click, TerCyanB.Click, TerRedB.Click,
                                                                     TerPurpleB.Click, TerYellowB.Click, TerWhiteB.Click,
                                                                     TerBlack.DragDrop, TerBlue.DragDrop, TerGreen.DragDrop, TerCyan.DragDrop, TerRed.DragDrop, TerPurple.DragDrop, TerYellow.DragDrop,
                                                                     TerWhite.DragDrop, TerBlackB.DragDrop, TerBlueB.DragDrop, TerGreenB.DragDrop, TerCyanB.DragDrop, TerRedB.DragDrop,
                                                                     TerPurpleB.DragDrop, TerYellowB.DragDrop, TerWhiteB.DragDrop


        If TypeOf e Is DragEventArgs Then Exit Sub

        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            Exit Sub
        End If

        Dim CList As New List(Of Control) From {sender, Terminal1}

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
                                                                          TerTabActive.Click, TerTabInactive.Click, TerTitlebarActive.Click, TerTitlebarInactive.Click,
                                                                          TerBackground.DragDrop, TerForeground.DragDrop, TerSelection.DragDrop, TerCursor.DragDrop,
                                                                          TerTabActive.DragDrop, TerTabInactive.DragDrop, TerTitlebarActive.DragDrop, TerTitlebarInactive.DragDrop

        If TypeOf e Is DragEventArgs Then
            If sender.Name.ToString.ToLower.Contains(TerBackground.Name.ToLower) Then
                _Terminal.Colors(TerSchemes.SelectedIndex).Background = CType(sender, UI.Controllers.ColorItem).BackColor
            End If

            If sender.Name.ToString.ToLower.Contains(TerForeground.Name.ToLower) Then
                _Terminal.Colors(TerSchemes.SelectedIndex).Foreground = CType(sender, UI.Controllers.ColorItem).BackColor
            End If

            If sender.Name.ToString.ToLower.Contains(TerSelection.Name.ToLower) Then
                _Terminal.Colors(TerSchemes.SelectedIndex).SelectionBackground = CType(sender, UI.Controllers.ColorItem).BackColor
            End If

            If sender.Name.ToString.ToLower.Contains(TerCursor.Name.ToLower) Then
                _Terminal.Colors(TerSchemes.SelectedIndex).CursorColor = CType(sender, UI.Controllers.ColorItem).BackColor
            End If

            If sender.Name.ToString.ToLower.Contains(TerTabActive.Name.ToLower) Then
                _Terminal.Themes(TerThemes.SelectedIndex - 3).Tab_Active = CType(sender, UI.Controllers.ColorItem).BackColor
            End If

            If sender.Name.ToString.ToLower.Contains(TerTabInactive.Name.ToLower) Then
                _Terminal.Themes(TerThemes.SelectedIndex - 3).Tab_Inactive = CType(sender, UI.Controllers.ColorItem).BackColor
            End If

            If sender.Name.ToString.ToLower.Contains(TerTitlebarActive.Name.ToLower) Then
                _Terminal.Themes(TerThemes.SelectedIndex - 3).Titlebar_Active = CType(sender, UI.Controllers.ColorItem).BackColor
            End If

            If sender.Name.ToString.ToLower.Contains(TerTitlebarInactive.Name.ToLower) Then
                _Terminal.Themes(TerThemes.SelectedIndex - 3).Titlebar_Inactive = CType(sender, UI.Controllers.ColorItem).BackColor
            End If

            ApplyPreview(_Terminal)
            Exit Sub
        End If

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

        Dim CList As New List(Of Control) From {sender, Terminal1, Terminal2}

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

    Private Sub TerFonts_SelectedIndexChanged(sender As Object, e As EventArgs)
        If Not _Shown Then Exit Sub

        Terminal1.Font = New Font(TerFontName.Font.Name, Terminal1.Font.Size, Terminal1.Font.Style)

        With If(TerProfiles.SelectedIndex = 0, _Terminal.DefaultProf, _Terminal.Profiles(TerProfiles.SelectedIndex - 1))
            .Font.Face = TerFontName.Font.Name
        End With
    End Sub

    Sub ApplyPreview([Terminal] As WinTerminal)
        Try
            Terminal1.UseAcrylicOnTitlebar = [Terminal].UseAcrylicInTabRow

            If TerProfiles.SelectedIndex = 0 Then
                Terminal1.TabColor = [Terminal].DefaultProf.TabColor
            Else
                If [Terminal].Profiles(TerProfiles.SelectedIndex - 1).TabColor = Color.FromArgb(0, 0, 0, 0) Then
                    Terminal1.TabColor = [Terminal].DefaultProf.TabColor
                Else
                    Terminal1.TabColor = [Terminal].Profiles(TerProfiles.SelectedIndex - 1).TabColor
                End If
            End If

            Terminal1.Color_Background = [Terminal].Colors(TerSchemes.SelectedIndex).Background
            Terminal1.Color_Foreground = [Terminal].Colors(TerSchemes.SelectedIndex).Foreground
            Terminal1.Color_Selection = [Terminal].Colors(TerSchemes.SelectedIndex).SelectionBackground
            Terminal1.Color_Cursor = [Terminal].Colors(TerSchemes.SelectedIndex).CursorColor

            Terminal2.Color_Background = [Terminal].Colors(TerSchemes.SelectedIndex).Background
            Terminal2.Color_Foreground = [Terminal].Colors(TerSchemes.SelectedIndex).Foreground
            Terminal2.Color_Selection = [Terminal].Colors(TerSchemes.SelectedIndex).SelectionBackground
            Terminal2.Color_Cursor = [Terminal].Colors(TerSchemes.SelectedIndex).CursorColor

            If TerThemesContainer.Enabled Then
                Terminal1.Color_TabFocused = [Terminal].Themes(TerThemes.SelectedIndex - 3).Tab_Active
                Terminal1.Color_Titlebar = [Terminal].Themes(TerThemes.SelectedIndex - 3).Titlebar_Active
                Terminal1.Color_TabUnFocused = [Terminal].Themes(TerThemes.SelectedIndex - 3).Tab_Inactive
                Terminal2.Color_Titlebar_Unfocused = [Terminal].Themes(TerThemes.SelectedIndex - 3).Titlebar_Inactive
            Else
                Terminal1.Color_TabFocused = Color.FromArgb(0, 0, 0, 0)
                Terminal1.Color_Titlebar = Color.FromArgb(0, 0, 0, 0)
                Terminal1.Color_Titlebar_Unfocused = Color.FromArgb(0, 0, 0, 0)
                Terminal1.Color_TabUnFocused = Color.FromArgb(0, 0, 0, 0)
                Terminal2.Color_Titlebar_Unfocused = Color.FromArgb(0, 0, 0, 0)
            End If

            If TerThemes.SelectedItem IsNot Nothing Then
                If TerThemes.SelectedItem.ToString.ToLower = "dark" Then
                    Terminal1.Light = False
                    Terminal2.Light = False

                ElseIf TerThemes.SelectedItem.ToString.ToLower = "light" Then
                    Terminal1.Light = True
                    Terminal2.Light = True

                ElseIf TerThemes.SelectedItem.ToString.ToLower = "system" Then
                    Select Case My.PreviewStyle
                        Case WindowStyle.W11
                            Terminal1.Light = My.CP.Windows11.AppMode_Light
                            Terminal2.Light = My.CP.Windows11.AppMode_Light

                        Case WindowStyle.W10
                            Terminal1.Light = My.CP.Windows10.AppMode_Light
                            Terminal2.Light = My.CP.Windows10.AppMode_Light

                        Case Else
                            Terminal1.Light = My.CP.Windows11.AppMode_Light
                            Terminal2.Light = My.CP.Windows11.AppMode_Light
                    End Select

                Else
                    Terminal1.Light = Not TerMode.Checked
                    Terminal2.Light = Not TerMode.Checked
                End If
            End If

            With If(TerProfiles.SelectedIndex = 0, _Terminal.DefaultProf, _Terminal.Profiles(TerProfiles.SelectedIndex - 1))
                Dim fx As New LogFont
                Dim f_cmd As New Font(.Font.Face, .Font.Size)
                f_cmd.ToLogFont(fx)
                fx.lfWeight = .Font.Weight * 100
                f_cmd = New Font(f_cmd.Name, f_cmd.Size, Font.FromLogFont(fx).Style)
                Terminal1.Font = f_cmd
                TerFontSizeVal.Text = f_cmd.Size
            End With

            Terminal1.Refresh()
            Terminal2.Refresh()

        Catch

        End Try
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        _Terminal.Themes.Add(New ThemesList With {.Name = My.Lang.Terminal_NewTheme & " #" & TerThemes.Items.Count - 3})
        FillTerminalThemes(_Terminal, TerThemes)
        TerThemes.SelectedIndex = TerThemes.Items.Count - 1
    End Sub

    Private Sub TerFontWeight_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TerFontWeight.SelectedIndexChanged
        Dim fx As New LogFont
        Dim f_cmd As New Font(Terminal1.Font.Name, Terminal1.Font.Size, Terminal1.Font.Style)
        f_cmd.ToLogFont(fx)
        fx.lfWeight = TerFontWeight.SelectedIndex * 100
        With Font.FromLogFont(fx) : f_cmd = New Font(Terminal1.Font.Name, Terminal1.Font.Size, .Style) : End With
        Terminal1.Font = f_cmd
        Terminal1.Refresh()

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
                TerMode.Checked = Not (.ApplicationTheme_light.ToLower = "light")
            End With

        Else
            TerThemesContainer.Enabled = False

            TerTitlebarActive.BackColor = Color.FromArgb(0, 0, 0, 0)
            TerTitlebarInactive.BackColor = Color.FromArgb(0, 0, 0, 0)
            TerTabActive.BackColor = Color.FromArgb(0, 0, 0, 0)
            TerTabInactive.BackColor = Color.FromArgb(0, 0, 0, 0)

            If TerThemes.SelectedIndex = 0 Then TerMode.Checked = True
            If TerThemes.SelectedIndex = 1 Then TerMode.Checked = False

            Select Case My.PreviewStyle
                Case WindowStyle.W11
                    If TerThemes.SelectedIndex = 2 Then TerMode.Checked = Not My.CP.Windows11.AppMode_Light

                Case WindowStyle.W10
                    If TerThemes.SelectedIndex = 2 Then TerMode.Checked = Not My.CP.Windows10.AppMode_Light

                Case Else
                    If TerThemes.SelectedIndex = 2 Then TerMode.Checked = Not My.CP.Windows11.AppMode_Light

            End Select


        End If

        If TerThemes.SelectedItem.ToString.ToLower = "dark" Then
            _Terminal.Theme = "dark"

        ElseIf TerThemes.SelectedItem.ToString.ToLower = "light" Then
            _Terminal.Theme = "light"

        ElseIf TerThemes.SelectedItem.ToString.ToLower = "system" Then
            _Terminal.Theme = "system"

        Else
            _Terminal.Theme = TerThemes.SelectedItem.ToString

        End If

        ApplyPreview(_Terminal)
    End Sub

    Private Sub TerEditThemeName_Click(sender As Object, e As EventArgs) Handles TerEditThemeName.Click
        If TerThemes.SelectedIndex > 2 Then
            Dim s As String = InputBox(My.Lang.Terminal_TypeSchemeName, TerThemes.SelectedItem.ToString)
            If s <> TerThemes.SelectedItem.ToString And Not String.IsNullOrEmpty(s) And Not TerThemes.Items.Contains(s) Then
                Dim i As Integer = TerThemes.SelectedIndex
                TerThemes.Items.RemoveAt(i)
                TerThemes.Items.Insert(i, s)
                TerThemes.SelectedIndex = i
                _Terminal.Themes(i - 3).Name = s
            End If
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim s As String = InputBox(My.Lang.Terminal_TypeSchemeName, TerSchemes.SelectedItem.ToString)
        If s <> TerSchemes.SelectedItem.ToString And Not String.IsNullOrEmpty(s) And Not TerSchemes.Items.Contains(s) Then
            Dim i As Integer = TerSchemes.SelectedIndex
            TerSchemes.Items.RemoveAt(i)
            TerSchemes.Items.Insert(i, s)
            TerSchemes.SelectedIndex = i
            _Terminal.Colors(i).Name = s
        End If
    End Sub

    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
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
        TerOpacityVal.Text = TerOpacityBar.Value

        Terminal1.Opacity = TerOpacityBar.Value

        Terminal1.Refresh()

        If _Shown Then
            With If(TerProfiles.SelectedIndex = 0, _Terminal.DefaultProf, _Terminal.Profiles(TerProfiles.SelectedIndex - 1))
                .Opacity = TerOpacityBar.Value
            End With
        End If

    End Sub

    Private Sub TerAcrylic_CheckedChanged(sender As Object) Handles TerAcrylic.CheckedChanged
        Terminal1.UseAcrylic = TerAcrylic.Checked

        Terminal1.Refresh()

        If Not _Shown Then Exit Sub

        With If(TerProfiles.SelectedIndex = 0, _Terminal.DefaultProf, _Terminal.Profiles(TerProfiles.SelectedIndex - 1))
            .UseAcrylic = TerAcrylic.Checked
        End With
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object) Handles CheckBox1.CheckedChanged
        My.Settings.WindowsTerminals.ListAllFonts = CheckBox1.Checked
        My.Settings.WindowsTerminals.Save()
    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        _Terminal.Profiles.Add(New ProfilesList With {.Name = My.Lang.Terminal_NewProfile & " #" & TerProfiles.Items.Count, .ColorScheme = _Terminal.DefaultProf.ColorScheme})
        FillTerminalProfiles(_Terminal, TerProfiles)
        TerProfiles.SelectedIndex = TerProfiles.Items.Count - 1
    End Sub

    Private Sub Button15_Click(sender As Object, e As EventArgs) Handles Button15.Click
        Dim TerDir As String
        Dim TerPreDir As String

        If Not My.Settings.WindowsTerminals.Path_Deflection Then
            TerDir = My.PATH_TerminalJSON
            TerPreDir = My.PATH_TerminalPreviewJSON
        Else
            If IO.File.Exists(My.Settings.WindowsTerminals.Terminal_Stable_Path) Then
                TerDir = My.Settings.WindowsTerminals.Terminal_Stable_Path
            Else
                TerDir = My.PATH_TerminalJSON
            End If

            If IO.File.Exists(My.Settings.WindowsTerminals.Terminal_Preview_Path) Then
                TerPreDir = My.Settings.WindowsTerminals.Terminal_Preview_Path
            Else
                TerPreDir = My.PATH_TerminalPreviewJSON
            End If
        End If

        Select Case _Mode
            Case WinTerminal.Version.Stable
                If IO.File.Exists(TerDir) Then Shell("explorer.exe shell:appsFolder\Microsoft.WindowsTerminal_8wekyb3d8bbwe!App")

            Case WinTerminal.Version.Preview
                If IO.File.Exists(TerPreDir) Then Shell("explorer.exe shell:appsFolder\Microsoft.WindowsTerminalPreview_8wekyb3d8bbwe!App")

        End Select


    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        TerBackImage.Text = "desktopWallpaper"
    End Sub

    Private Sub TerBackImage_TextChanged(sender As Object, e As EventArgs) Handles TerBackImage.TextChanged
        If TerBackImage.Text = "desktopWallpaper" Then
            Terminal1.BackImage = My.Wallpaper
        Else
            If IO.File.Exists(TerBackImage.Text) Then
                Terminal1.BackImage = Bitmap_Mgr.Load(TerBackImage.Text).FillScale(Terminal1.Size).Resize(Terminal1.Width - 2, Terminal1.Height - 32)

            Else
                Terminal1.BackImage = Nothing
            End If
        End If

        If Not _Shown Then Exit Sub

        With If(TerProfiles.SelectedIndex = 0, _Terminal.DefaultProf, _Terminal.Profiles(TerProfiles.SelectedIndex - 1))
            .BackgroundImage = TerBackImage.Text
        End With

        Terminal1.Invalidate()
    End Sub

    Private Sub Button16_Click(sender As Object, e As EventArgs) Handles Button16.Click
        If ImgDlg.ShowDialog = DialogResult.OK Then
            TerBackImage.Text = ImgDlg.FileName
        End If
    End Sub

    Private Sub TerMode_CheckedChanged(sender As Object, e As EventArgs) Handles TerMode.CheckedChanged
        If TerThemes.SelectedIndex > 2 Then
            _Terminal.Themes(TerThemes.SelectedIndex - 3).ApplicationTheme_light = If(Not TerMode.Checked, "light", "dark")
        End If

        If _Shown Then ApplyPreview(_Terminal)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Select Case _Mode
            Case WinTerminal.Version.Stable
                My.CP.Terminal.Enabled = TerEnabled.Checked
                My.CP.Terminal = _Terminal

            Case WinTerminal.Version.Preview
                My.CP.TerminalPreview.Enabled = TerEnabled.Checked
                My.CP.TerminalPreview = _Terminal

        End Select

        DialogResult = DialogResult.OK

        Me.Close()
    End Sub

    Private Sub WindowsTerminal_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If DialogResult <> DialogResult.OK Then


            Select Case _Mode
                Case WinTerminal.Version.Stable
                    My.CP.Terminal = _TerminalDefault

                Case WinTerminal.Version.Preview
                    My.CP.TerminalPreview = _TerminalDefault

            End Select

        End If

        DialogResult = DialogResult.Cancel
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click

        If TerEnabled.Checked Then
            If My.W10 Or My.W11 Then

                Try
                    Cursor = Cursors.WaitCursor

                    Dim TerDir As String
                    Dim TerPreDir As String

                    If Not My.Settings.WindowsTerminals.Path_Deflection Then
                        TerDir = My.PATH_TerminalJSON
                        TerPreDir = My.PATH_TerminalPreviewJSON
                    Else
                        If IO.File.Exists(My.Settings.WindowsTerminals.Terminal_Stable_Path) Then
                            TerDir = My.Settings.WindowsTerminals.Terminal_Stable_Path
                        Else
                            TerDir = My.PATH_TerminalJSON
                        End If

                        If IO.File.Exists(My.Settings.WindowsTerminals.Terminal_Preview_Path) Then
                            TerPreDir = My.Settings.WindowsTerminals.Terminal_Preview_Path
                        Else
                            TerPreDir = My.PATH_TerminalPreviewJSON
                        End If
                    End If

                    If IO.File.Exists(TerDir) And _Mode = WinTerminal.Version.Stable Then
                        _Terminal.Save(TerDir, WinTerminal.Mode.JSONFile)
                    End If

                    If IO.File.Exists(TerPreDir) And _Mode = WinTerminal.Version.Preview Then
                        _Terminal.Save(TerPreDir, WinTerminal.Mode.JSONFile, WinTerminal.Version.Preview)
                    End If

                    Cursor = Cursors.Default

                Catch ex As Exception
                    Throw ex
                End Try

            End If

        Else
            MsgBox(My.Lang.CMD_Enable, MsgBoxStyle.Critical)
        End If

    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        If My.W10 Or My.W11 Then
            Dim TerDir As String
            Dim TerPreDir As String

            TerDir = My.PATH_TerminalJSON
            TerPreDir = My.PATH_TerminalPreviewJSON


            If IO.File.Exists(TerDir) And _Mode = WinTerminal.Version.Stable Then
                Process.Start(TerDir)
            End If

            If IO.File.Exists(TerPreDir) And _Mode = WinTerminal.Version.Preview Then
                Process.Start(TerPreDir)
            End If

        End If
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        If SaveJSONDlg.ShowDialog = DialogResult.OK Then
            If My.W10 Or My.W11 Then
                Dim TerDir As String
                Dim TerPreDir As String

                TerDir = My.PATH_TerminalJSON
                TerPreDir = My.PATH_TerminalPreviewJSON

                If IO.File.Exists(TerDir) And _Mode = WinTerminal.Version.Stable Then
                    IO.File.Copy(TerDir, SaveJSONDlg.FileName)
                End If

                If IO.File.Exists(TerPreDir) And _Mode = WinTerminal.Version.Preview Then
                    IO.File.Copy(TerPreDir, SaveJSONDlg.FileName)
                End If

            End If
        End If
    End Sub

    Public SaveState As WinTerminal.Version = _Mode

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        If OpenWPTHDlg.ShowDialog = DialogResult.OK Then

            SaveState = _Mode
            If WindowsTerminalDecide.ShowDialog = DialogResult.OK Then
                If SaveState = WinTerminal.Version.Stable Then
                    _Terminal = New WinTerminal(OpenWPTHDlg.FileName, WinTerminal.Mode.WinPaletterFile)

                ElseIf SaveState = WinTerminal.Version.Preview Then
                    _Terminal = New WinTerminal(OpenWPTHDlg.FileName, WinTerminal.Mode.WinPaletterFile, WinTerminal.Version.Preview)

                End If

                Load_FromTerminal()
            End If

        End If
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        If OpenJSONDlg.ShowDialog = DialogResult.OK Then

            Try

                If _Mode = WinTerminal.Version.Stable Then
                    _Terminal = New WinTerminal(OpenJSONDlg.FileName, WinTerminal.Mode.JSONFile)
                    Load_FromTerminal()
                End If

                If _Mode = WinTerminal.Version.Preview Then
                    _Terminal = New WinTerminal(OpenJSONDlg.FileName, WinTerminal.Mode.JSONFile, WinTerminal.Version.Preview)
                    Load_FromTerminal()
                End If

            Catch ex As Exception
                MsgBox(My.Lang.Terminal_ErrorFile, MsgBoxStyle.Critical)
                BugReport.ThrowError(ex)
            End Try

        End If
    End Sub

    Private Sub Button17_Click(sender As Object, e As EventArgs) Handles Button17.Click

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

    Private Sub Button18_Click(sender As Object, e As EventArgs) Handles Button18.Click

        If TerProfiles.SelectedIndex = 0 Then
            MsgBox(My.Lang.Terminal_ProfileNotCloneable, MsgBoxStyle.Critical)
            Exit Sub
        End If

        Dim P As New ProfilesList With {
            .Name = _Terminal.Profiles(TerProfiles.SelectedIndex - 1).Name & " " & My.Lang.Terminal_Clone & " #" & TerProfiles.Items.Count,
            .BackgroundImage = _Terminal.Profiles(TerProfiles.SelectedIndex - 1).BackgroundImage,
            .BackgroundImageOpacity = _Terminal.Profiles(TerProfiles.SelectedIndex - 1).BackgroundImageOpacity,
            .ColorScheme = _Terminal.Profiles(TerProfiles.SelectedIndex - 1).ColorScheme,
            .Commandline = _Terminal.Profiles(TerProfiles.SelectedIndex - 1).Commandline,
            .CursorHeight = _Terminal.Profiles(TerProfiles.SelectedIndex - 1).CursorHeight,
            .CursorShape = _Terminal.Profiles(TerProfiles.SelectedIndex - 1).CursorShape,
            .Font = _Terminal.Profiles(TerProfiles.SelectedIndex - 1).Font,
            .Icon = _Terminal.Profiles(TerProfiles.SelectedIndex - 1).Icon,
            .Opacity = _Terminal.Profiles(TerProfiles.SelectedIndex - 1).Opacity,
            .TabColor = _Terminal.Profiles(TerProfiles.SelectedIndex - 1).TabColor,
            .TabTitle = _Terminal.Profiles(TerProfiles.SelectedIndex - 1).TabTitle,
            .UseAcrylic = _Terminal.Profiles(TerProfiles.SelectedIndex - 1).UseAcrylic
        }

        _Terminal.Profiles.Add(P)
        FillTerminalProfiles(_Terminal, TerProfiles)
        TerProfiles.SelectedIndex = TerProfiles.Items.Count - 1

    End Sub

    Private Sub Button19_Click(sender As Object, e As EventArgs) Handles Button19.Click
        If TerThemes.SelectedIndex < 3 Then
            MsgBox(My.Lang.Terminal_ThemeNotCloneable, MsgBoxStyle.Critical)
            Exit Sub
        End If

        Dim Th As New ThemesList With {
            .Name = _Terminal.Themes(TerThemes.SelectedIndex - 3).Name & " " & My.Lang.Terminal_Clone & " #" & TerThemes.Items.Count,
            .ApplicationTheme_light = _Terminal.Themes(TerThemes.SelectedIndex - 3).ApplicationTheme_light,
            .Tab_Active = _Terminal.Themes(TerThemes.SelectedIndex - 3).Tab_Active,
            .Tab_Inactive = _Terminal.Themes(TerThemes.SelectedIndex - 3).Tab_Inactive,
            .Titlebar_Active = _Terminal.Themes(TerThemes.SelectedIndex - 3).Titlebar_Active,
            .Titlebar_Inactive = _Terminal.Themes(TerThemes.SelectedIndex - 3).Titlebar_Inactive
        }

        _Terminal.Themes.Add(Th)
        FillTerminalThemes(_Terminal, TerThemes)
        TerThemes.SelectedIndex = TerThemes.Items.Count - 1
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        DialogResult = DialogResult.Cancel

        Me.Close()
    End Sub

    Public CCat As String

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        With WindowsTerminalCopycat.ComboBox1
            .Items.Clear()
            CCat = Nothing

            For Each x In TerProfiles.Items
                .Items.Add(x)
            Next
        End With

        If WindowsTerminalCopycat.ShowDialog = DialogResult.OK Then
            For x As Integer = 0 To TerProfiles.Items.Count - 1
                If TerProfiles.Items.Item(x).ToString.ToLower = CCat.ToLower Then

                    Dim CCatFrom As ProfilesList = If(x = 0, _Terminal.DefaultProf, _Terminal.Profiles(x - 1))

                    With If(TerProfiles.SelectedIndex = 0, _Terminal.DefaultProf, _Terminal.Profiles(TerProfiles.SelectedIndex - 1))
                        .BackgroundImage = CCatFrom.BackgroundImage
                        .BackgroundImageOpacity = CCatFrom.BackgroundImageOpacity
                        .ColorScheme = CCatFrom.ColorScheme
                        .CursorHeight = CCatFrom.CursorHeight
                        .CursorShape = CCatFrom.CursorShape
                        .Font.Face = CCatFrom.Font.Face
                        .Font.Weight = CCatFrom.Font.Weight
                        .Font.Size = CCatFrom.Font.Size
                        .Icon = CCatFrom.Icon
                        .Opacity = CCatFrom.Opacity
                        .TabColor = CCatFrom.TabColor
                        .TabTitle = CCatFrom.TabTitle
                        .UseAcrylic = CCatFrom.UseAcrylic
                    End With

                    With CCatFrom
                        Try
                            If TerSchemes.Items.Contains(.ColorScheme) Then TerSchemes.SelectedItem = .ColorScheme Else TerSchemes.SelectedItem = _Terminal.DefaultProf.ColorScheme
                        Catch
                        End Try

                        TerBackImage.Text = .BackgroundImage
                        TerImageOpacity.Value = .BackgroundImageOpacity * 100

                        TerCursorStyle.SelectedIndex = .CursorShape
                        TerCursorHeightBar.Value = .CursorHeight

                        TerFontName.Text = .Font.Face
                        Dim fx As New LogFont
                        Dim f_cmd As New Font(.Font.Face, .Font.Size)
                        f_cmd.ToLogFont(fx)
                        fx.lfWeight = .Font.Weight * 100
                        f_cmd = New Font(f_cmd.Name, f_cmd.Size, Font.FromLogFont(fx).Style)
                        TerFontName.Font = New Font(f_cmd.Name, 9, f_cmd.Style)

                        TerFontSizeBar.Value = .Font.Size
                        TerFontWeight.SelectedIndex = .Font.Weight

                        TerAcrylic.Checked = .UseAcrylic
                        TerOpacityBar.Value = .Opacity

                        Terminal1.Opacity = .Opacity
                        Terminal1.OpacityBackImage = .BackgroundImageOpacity * 100

                        If Not String.IsNullOrEmpty(.TabTitle) Then
                            Terminal1.TabTitle = .TabTitle
                        Else
                            If Not String.IsNullOrEmpty(.Name) Then
                                Terminal1.TabTitle = .Name
                            ElseIf TerProfiles.SelectedIndex = 0 Then
                                Terminal1.TabTitle = My.Lang.Default
                            Else
                                Terminal1.TabTitle = My.Lang.Untitled
                            End If
                        End If

                        If File.Exists(.Icon) Then
                            Terminal1.TabIcon = Bitmap_Mgr.Load(.Icon)

                        Else
                            NativeMethods.Kernel32.Wow64DisableWow64FsRedirection(IntPtr.Zero)
                            Dim path As String = ""
                            If .Commandline IsNot Nothing Then path = .Commandline.Replace("%SystemRoot%", My.PATH_Windows)
                            NativeMethods.Kernel32.Wow64RevertWow64FsRedirection(IntPtr.Zero)

                            If File.Exists(path) Then
                                Terminal1.TabIcon = NativeMethods.DLLFunc.ExtractSmallIcon(path).ToBitmap
                            Else
                                Terminal1.TabIcon = Nothing
                                Terminal1.TabIconButItIsString = ""
                            End If

                        End If

                    End With

                    Exit For
                End If

            Next

            ApplyPreview(_Terminal)

        End If

    End Sub

    Private Sub Button20_Click(sender As Object, e As EventArgs) Handles Button20.Click
        With WindowsTerminalCopycat.ComboBox1
            .Items.Clear()
            CCat = Nothing

            For Each x In TerSchemes.Items
                .Items.Add(x)
            Next
        End With

        If WindowsTerminalCopycat.ShowDialog = DialogResult.OK Then
            For x As Integer = 0 To TerSchemes.Items.Count - 1
                If TerSchemes.Items.Item(x).ToString.ToLower = CCat.ToLower Then

                    Dim CCatFrom As TColor = _Terminal.Colors(x)

                    With _Terminal.Colors(TerSchemes.SelectedIndex)
                        .Background = CCatFrom.Background
                        .Black = CCatFrom.Black
                        .Blue = CCatFrom.Blue
                        .BrightBlack = CCatFrom.BrightBlack
                        .BrightBlue = CCatFrom.BrightBlue
                        .BrightCyan = CCatFrom.BrightCyan
                        .BrightGreen = CCatFrom.BrightGreen
                        .BrightPurple = CCatFrom.BrightPurple
                        .BrightRed = CCatFrom.BrightRed
                        .BrightWhite = CCatFrom.BrightWhite
                        .BrightYellow = CCatFrom.BrightYellow
                        .CursorColor = CCatFrom.CursorColor
                        .Cyan = CCatFrom.Cyan
                        .Foreground = CCatFrom.Foreground
                        .Green = CCatFrom.Green
                        .Purple = CCatFrom.Purple
                        .Red = CCatFrom.Red
                        .SelectionBackground = CCatFrom.SelectionBackground
                        .White = CCatFrom.White
                        .Yellow = CCatFrom.Yellow

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

                    ApplyPreview(_Terminal)

                    Exit For
                End If

            Next

        End If

    End Sub

    Private Sub Button21_Click(sender As Object, e As EventArgs) Handles Button21.Click
        If TerThemes.SelectedIndex < 3 Then
            MsgBox(My.Lang.Terminal_ThemeNotCloneable, MsgBoxStyle.Critical)
            Exit Sub
        End If

        With WindowsTerminalCopycat.ComboBox1
            .Items.Clear()
            CCat = Nothing

            For Each x In TerThemes.Items
                .Items.Add(x)
            Next
        End With

        If WindowsTerminalCopycat.ShowDialog = DialogResult.OK Then
            For x As Integer = 0 To TerThemes.Items.Count - 1
                If TerThemes.Items.Item(x).ToString.ToLower = CCat.ToLower Then

                    Dim CCatFrom As ThemesList = _Terminal.Themes(x - 3)

                    With _Terminal.Themes(TerThemes.SelectedIndex - 3)
                        .ApplicationTheme_light = CCatFrom.ApplicationTheme_light
                        .Tab_Active = CCatFrom.Tab_Active
                        .Tab_Inactive = CCatFrom.Tab_Inactive
                        .Titlebar_Active = CCatFrom.Titlebar_Active
                        .Titlebar_Inactive = CCatFrom.Titlebar_Inactive
                    End With

                    With CCatFrom
                        TerTitlebarActive.BackColor = .Titlebar_Active
                        TerTitlebarInactive.BackColor = .Titlebar_Inactive
                        TerTabActive.BackColor = .Tab_Active
                        TerTabInactive.BackColor = .Tab_Inactive
                        TerMode.Checked = Not (.ApplicationTheme_light.ToLower = "light")
                    End With

                    Exit For
                End If

            Next

            ApplyPreview(_Terminal)
        End If
    End Sub

    Private Sub Button22_Click(sender As Object, e As EventArgs) Handles Button22.Click
        Dim CPx As New CP(CP.CP_Type.Registry)

        Select Case _Mode
            Case WinTerminal.Version.Stable
                _Terminal = CPx.Terminal
                TerEnabled.Checked = CPx.Terminal.Enabled

            Case WinTerminal.Version.Preview
                _Terminal = CPx.TerminalPreview
                TerEnabled.Checked = CPx.TerminalPreview.Enabled
        End Select

        Load_FromTerminal()

        CPx.Dispose()
    End Sub

    Private Sub TerFontSizeVal_Click(sender As Object, e As EventArgs) Handles TerFontSizeVal.Click
        Dim response As String = InputBox(My.Lang.InputValue, sender.text, My.Lang.ItMustBeNumerical)
        sender.Text = Math.Max(Math.Min(Val(response), TerFontSizeBar.Maximum), TerFontSizeBar.Minimum) : TerFontSizeBar.Value = Val(sender.Text)
    End Sub

    Private Sub TerCursorHeightVal_Click(sender As Object, e As EventArgs) Handles TerCursorHeightVal.Click
        Dim response As String = InputBox(My.Lang.InputValue, sender.text, My.Lang.ItMustBeNumerical)
        sender.Text = Math.Max(Math.Min(Val(response), TerCursorHeightBar.Maximum), TerCursorHeightBar.Minimum) : TerCursorHeightBar.Value = Val(sender.Text)
    End Sub

    Private Sub TerImageOpacityVal_Click(sender As Object, e As EventArgs) Handles TerImageOpacityVal.Click
        Dim response As String = InputBox(My.Lang.InputValue, sender.text, My.Lang.ItMustBeNumerical)
        sender.Text = Math.Max(Math.Min(Val(response), TerImageOpacity.Maximum), TerImageOpacity.Minimum) : TerImageOpacity.Value = Val(sender.Text)
    End Sub

    Private Sub TerOpacityVal_Click(sender As Object, e As EventArgs) Handles TerOpacityVal.Click
        Dim response As String = InputBox(My.Lang.InputValue, sender.text, My.Lang.ItMustBeNumerical)
        sender.Text = Math.Max(Math.Min(Val(response), TerOpacityBar.Maximum), TerOpacityBar.Minimum) : TerOpacityBar.Value = Val(sender.Text)
    End Sub

    Private Sub TerEnabled_CheckedChanged(sender As Object, e As EventArgs) Handles TerEnabled.CheckedChanged
        checker_img.Image = If(sender.Checked, My.Resources.checker_enabled, My.Resources.checker_disabled)
    End Sub

    Private Sub Button23_Click(sender As Object, e As EventArgs) Handles Button23.Click
        FontDialog1.FixedPitchOnly = Not My.Settings.WindowsTerminals.ListAllFonts
        FontDialog1.Font = Terminal1.Font
        If FontDialog1.ShowDialog = DialogResult.OK Then
            TerFontName.Text = FontDialog1.Font.Name
            Dim fx As New LogFont
            FontDialog1.Font.ToLogFont(fx)
            fx.lfWeight = TerFontWeight.SelectedIndex * 100
            With Font.FromLogFont(fx) : Terminal1.Font = New Font(FontDialog1.Font.Name, FontDialog1.Font.Size, .Style) : End With
            TerFontName.Font = New Font(FontDialog1.Font.Name, 9, Terminal1.Font.Style)
            TerFontSizeBar.Value = FontDialog1.Font.Size
        End If

    End Sub

    Private Sub Form_HelpButtonClicked(sender As Object, e As CancelEventArgs) Handles Me.HelpButtonClicked
        Process.Start(My.Resources.Link_Wiki & "/Edit-Windows-Terminals-(Windows-10-and-later)")
    End Sub
End Class