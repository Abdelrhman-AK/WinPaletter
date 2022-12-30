Imports System.Security.Principal
Imports Microsoft.Win32
Imports WinPaletter.XenonCore
Public Class ExternalTerminal
    Private _Shown As Boolean = False
    Dim f_extterminal As Font = New Font("Consolas", 18, FontStyle.Regular)

    Private Sub ExternalTerminal_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ApplyDarkMode(Me)
        _Shown = False
        FillTerminals(XenonComboBox1)
        RasterList.BringToFront()

        XenonCheckBox1.Checked = My.Application._Settings.Terminal_OtherFonts
        FillFonts(ExtTerminal_FontsBox, Not My.Application._Settings.Terminal_OtherFonts)
        MainFrm.Visible = False

        XenonButton4.Image = MainFrm.XenonButton20.Image.Resize(16, 16)

        Location = New Point(10, (My.Computer.Screen.Bounds.Height - Height) / 2 - 20)
    End Sub

    Private Sub ExternalTerminal_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        _Shown = True
    End Sub

    Sub FillTerminals([ListBox] As ComboBox)
        [ListBox].Items.Clear()

        For Each x As String In Registry.CurrentUser.OpenSubKey("Console", True).GetSubKeyNames()
            If Not x.ToLower = "%%Startup".ToLower And
                Not x.ToLower = "%SystemRoot%_System32_cmd.exe".ToLower And
                Not x.ToLower = "%SystemRoot%_System32_WindowsPowerShell_v1.0_powershell.exe".ToLower And
                Not x.ToLower = "%SystemRoot%_SysWOW64_WindowsPowerShell_v1.0_powershell.exe".ToLower Then
                [ListBox].Items.Add(x)
            End If
        Next

    End Sub

    Sub GetFromExtTerminal(RegKey As String)

        If Not Registry.CurrentUser.OpenSubKey("Console", True).GetSubKeyNames().Contains(RegKey) Then
            MsgBox(My.Application.LanguageHelper.ExtTer_NotFound, MsgBoxStyle.Critical + My.Application.MsgboxRt)
            Exit Sub
        End If

        Dim y_cmd As Object

        Dim _Def As CP
        If MainFrm.PreviewConfig = MainFrm.WinVer.Eleven Then
            _Def = New CP_Defaults().Default_Windows11
        ElseIf MainFrm.PreviewConfig = MainFrm.WinVer.Ten Then
            _Def = New CP_Defaults().Default_Windows10
        ElseIf MainFrm.PreviewConfig = MainFrm.WinVer.Eight Then
            _Def = New CP_Defaults().Default_Windows8
        ElseIf MainFrm.PreviewConfig = MainFrm.WinVer.Seven Then
            _Def = New CP_Defaults().Default_Windows7
        Else
            _Def = New CP_Defaults().Default_Windows11
        End If

        Try
            y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\" & RegKey, "ColorTable00", _Def.CommandPrompt.ColorTable00.Reverse.ToArgb)
            ExtTerminal_ColorTable00.BackColor = Color.FromArgb(255, Color.FromArgb(y_cmd).Reverse)
        Catch
            ExtTerminal_ColorTable00.BackColor = _Def.CommandPrompt.ColorTable00
        End Try

        Try
            y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\" & RegKey, "ColorTable01", _Def.CommandPrompt.ColorTable01.Reverse.ToArgb)
            ExtTerminal_ColorTable01.BackColor = Color.FromArgb(255, Color.FromArgb(y_cmd).Reverse)
        Catch
            ExtTerminal_ColorTable01.BackColor = _Def.CommandPrompt.ColorTable01
        End Try

        Try
            y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\" & RegKey, "ColorTable02", _Def.CommandPrompt.ColorTable02.Reverse.ToArgb)
            ExtTerminal_ColorTable02.BackColor = Color.FromArgb(255, Color.FromArgb(y_cmd).Reverse)
        Catch
            ExtTerminal_ColorTable02.BackColor = _Def.CommandPrompt.ColorTable02
        End Try

        Try
            y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\" & RegKey, "ColorTable03", _Def.CommandPrompt.ColorTable03.Reverse.ToArgb)
            ExtTerminal_ColorTable03.BackColor = Color.FromArgb(255, Color.FromArgb(y_cmd).Reverse)
        Catch
            ExtTerminal_ColorTable03.BackColor = _Def.CommandPrompt.ColorTable03
        End Try

        Try
            y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\" & RegKey, "ColorTable04", _Def.CommandPrompt.ColorTable04.Reverse.ToArgb)
            ExtTerminal_ColorTable04.BackColor = Color.FromArgb(255, Color.FromArgb(y_cmd).Reverse)
        Catch
            ExtTerminal_ColorTable04.BackColor = _Def.CommandPrompt.ColorTable04
        End Try

        Try
            y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\" & RegKey, "ColorTable05", _Def.CommandPrompt.ColorTable05.Reverse.ToArgb)
            ExtTerminal_ColorTable05.BackColor = Color.FromArgb(255, Color.FromArgb(y_cmd).Reverse)
        Catch
            ExtTerminal_ColorTable05.BackColor = _Def.CommandPrompt.ColorTable05
        End Try

        Try
            y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\" & RegKey, "ColorTable06", _Def.CommandPrompt.ColorTable06.Reverse.ToArgb)
            ExtTerminal_ColorTable06.BackColor = Color.FromArgb(255, Color.FromArgb(y_cmd).Reverse)
        Catch
            ExtTerminal_ColorTable06.BackColor = _Def.CommandPrompt.ColorTable06
        End Try

        Try
            y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\" & RegKey, "ColorTable07", _Def.CommandPrompt.ColorTable07.Reverse.ToArgb)
            ExtTerminal_ColorTable07.BackColor = Color.FromArgb(255, Color.FromArgb(y_cmd).Reverse)
        Catch
            ExtTerminal_ColorTable07.BackColor = _Def.CommandPrompt.ColorTable07
        End Try

        Try
            y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\" & RegKey, "ColorTable08", _Def.CommandPrompt.ColorTable08.Reverse.ToArgb)
            ExtTerminal_ColorTable08.BackColor = Color.FromArgb(255, Color.FromArgb(y_cmd).Reverse)
        Catch
            ExtTerminal_ColorTable08.BackColor = _Def.CommandPrompt.ColorTable08
        End Try

        Try
            y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\" & RegKey, "ColorTable09", _Def.CommandPrompt.ColorTable09.Reverse.ToArgb)
            ExtTerminal_ColorTable09.BackColor = Color.FromArgb(255, Color.FromArgb(y_cmd).Reverse)
        Catch
            ExtTerminal_ColorTable09.BackColor = _Def.CommandPrompt.ColorTable09
        End Try

        Try
            y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\" & RegKey, "ColorTable10", _Def.CommandPrompt.ColorTable10.Reverse.ToArgb)
            ExtTerminal_ColorTable10.BackColor = Color.FromArgb(255, Color.FromArgb(y_cmd).Reverse)
        Catch
            ExtTerminal_ColorTable10.BackColor = _Def.CommandPrompt.ColorTable10
        End Try

        Try
            y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\" & RegKey, "ColorTable11", _Def.CommandPrompt.ColorTable11.Reverse.ToArgb)
            ExtTerminal_ColorTable11.BackColor = Color.FromArgb(255, Color.FromArgb(y_cmd).Reverse)
        Catch
            ExtTerminal_ColorTable11.BackColor = _Def.CommandPrompt.ColorTable11
        End Try

        Try
            y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\" & RegKey, "ColorTable12", _Def.CommandPrompt.ColorTable12.Reverse.ToArgb)
            ExtTerminal_ColorTable12.BackColor = Color.FromArgb(255, Color.FromArgb(y_cmd).Reverse)
        Catch
            ExtTerminal_ColorTable12.BackColor = _Def.CommandPrompt.ColorTable12
        End Try

        Try
            y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\" & RegKey, "ColorTable13", _Def.CommandPrompt.ColorTable13.Reverse.ToArgb)
            ExtTerminal_ColorTable13.BackColor = Color.FromArgb(255, Color.FromArgb(y_cmd).Reverse)
        Catch
            ExtTerminal_ColorTable13.BackColor = _Def.CommandPrompt.ColorTable13
        End Try

        Try
            y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\" & RegKey, "ColorTable14", _Def.CommandPrompt.ColorTable14.Reverse.ToArgb)
            ExtTerminal_ColorTable14.BackColor = Color.FromArgb(255, Color.FromArgb(y_cmd).Reverse)
        Catch
            ExtTerminal_ColorTable14.BackColor = _Def.CommandPrompt.ColorTable14
        End Try

        Try
            y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\" & RegKey, "ColorTable15", _Def.CommandPrompt.ColorTable15.Reverse.ToArgb)
            ExtTerminal_ColorTable15.BackColor = Color.FromArgb(255, Color.FromArgb(y_cmd).Reverse)
        Catch
            ExtTerminal_ColorTable15.BackColor = _Def.CommandPrompt.ColorTable15
        End Try

        Try
            y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\" & RegKey, "PopupColors", Convert.ToInt32(_Def.CommandPrompt.PopupBackground.ToString("X") & _Def.CommandPrompt.PopupForeground.ToString("X"), 16))
            Dim d As String = CInt(y_cmd).ToString("X")

            If d.Count = 1 Then d = 0 & d
            ExtTerminal_PopupBackgroundBar.Value = Convert.ToInt32(d.Chars(0), 16)
            ExtTerminal_PopupForegroundBar.Value = Convert.ToInt32(d.Chars(1), 16)
        Catch
            ExtTerminal_PopupBackgroundBar.Value = _Def.CommandPrompt.PopupBackground
            ExtTerminal_PopupForegroundBar.Value = _Def.CommandPrompt.PopupForeground
        End Try

        Try
            y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\" & RegKey, "ScreenColors", Convert.ToInt32(_Def.CommandPrompt.ScreenColorsBackground.ToString("X") & _Def.CommandPrompt.ScreenColorsForeground.ToString("X"), 16))
            Dim d As String = CInt(y_cmd).ToString("X")

            If d.Count = 1 Then d = 0 & d
            ExtTerminal_AccentBackgroundBar.Value = Convert.ToInt32(d.Chars(0), 16)
            ExtTerminal_AccentForegroundBar.Value = Convert.ToInt32(d.Chars(1), 16)
        Catch
            ExtTerminal_AccentBackgroundBar.Value = _Def.CommandPrompt.ScreenColorsBackground
            ExtTerminal_AccentForegroundBar.Value = _Def.CommandPrompt.ScreenColorsForeground
        End Try


        Try
            y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\" & RegKey, "CursorSize", 25)
            ExtTerminal_CursorSizeBar.Value = y_cmd
        Catch
            ExtTerminal_CursorSizeBar.Value = 25
        End Try


        ExtTerminal_FontSizeLbl.Text = ExtTerminal_FontSizeBar.Value

        Dim wg As Integer

        Try
            y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\" & RegKey, "FontWeight", 400)
            wg = y_cmd
            Select Case y_cmd
                Case 0
                    ExtTerminal_FontWeightBox.SelectedIndex = 0

                Case 100
                    ExtTerminal_FontWeightBox.SelectedIndex = 1

                Case 200
                    ExtTerminal_FontWeightBox.SelectedIndex = 2

                Case 300
                    ExtTerminal_FontWeightBox.SelectedIndex = 3

                Case 400
                    ExtTerminal_FontWeightBox.SelectedIndex = 4

                Case 500
                    ExtTerminal_FontWeightBox.SelectedIndex = 5

                Case 600
                    ExtTerminal_FontWeightBox.SelectedIndex = 6

                Case 700
                    ExtTerminal_FontWeightBox.SelectedIndex = 7

                Case 800
                    ExtTerminal_FontWeightBox.SelectedIndex = 8

                Case 900
                    ExtTerminal_FontWeightBox.SelectedIndex = 9

                Case Else
                    ExtTerminal_FontWeightBox.SelectedIndex = 4
            End Select

        Catch
            wg = 400
            ExtTerminal_FontWeightBox.SelectedIndex = 4
        End Try


        Try
            y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\" & RegKey, "FontFamily", If(Not _Def.CommandPrompt.FontRaster, 54, 1))
            ExtTerminal_RasterToggle.Checked = If(y_cmd = 1 Or y_cmd = 0 Or y_cmd = 48, True, False)

            Try
                If My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\" & RegKey, "FaceName", "Consolas").ToLower = "terminal" Then ExtTerminal_RasterToggle.Checked = True
            Catch ex As Exception

            End Try
        Catch
            ExtTerminal_RasterToggle.Checked = False
        End Try


        Try
            y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\" & RegKey, "FontSize", 18 * 65536)
            If y_cmd = 0 And Not ExtTerminal_RasterToggle.Checked Then ExtTerminal_FontSizeBar.Value = _Def.CommandPrompt.FontSize / 65536 Else ExtTerminal_FontSizeBar.Value = y_cmd / 65536

            If y_cmd = 393220 Then RasterList.SelectedItem = "4x6"
            If y_cmd = 524294 Then RasterList.SelectedItem = "6x8"
            If y_cmd = 524296 Then RasterList.SelectedItem = "8x8"
            If y_cmd = 524304 Then RasterList.SelectedItem = "16x8"
            If y_cmd = 786437 Then RasterList.SelectedItem = "5x12"
            If y_cmd = 786439 Then RasterList.SelectedItem = "7x12"
            If y_cmd = 0 Then RasterList.SelectedItem = "8x12"
            If y_cmd = 786448 Then RasterList.SelectedItem = "16x12"
            If y_cmd = 1048588 Then RasterList.SelectedItem = "12x16"
            If y_cmd = 1179658 Then RasterList.SelectedItem = "10x18"
            If RasterList.SelectedItem = Nothing Then RasterList.SelectedItem = "8x12"

        Catch
            ExtTerminal_FontSizeBar.Value = 18
            RasterList.SelectedItem = "8x12"
        End Try

        Try
            y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\" & RegKey, "FaceName", "Consolas")

            If CP.IsFontInstalled(y_cmd) Then

                If Not ExtTerminal_RasterToggle.Checked Then
                    With Font.FromLogFont(New LogFont With {.lfFaceName = y_cmd, .lfWeight = wg}) : f_extterminal = New Font(.FontFamily, CInt(ExtTerminal_FontSizeBar.Value), .Style) : End With
                    ExtTerminal_FontsBox.SelectedItem = f_extterminal.Name
                End If

            Else
                ExtTerminal_FontsBox.SelectedItem = "Consolas"
            End If

        Catch
            ExtTerminal_FontsBox.SelectedItem = "Consolas"
        End Try

        If My.W10_1909 Then
            Try
                y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\" & RegKey, "CursorColor", _Def.CommandPrompt.W10_1909_CursorColor.Reverse.ToArgb)
                ExtTerminal_CursorColor.BackColor = Color.FromArgb(255, Color.FromArgb(y_cmd).Reverse)
            Catch
                ExtTerminal_CursorColor.BackColor = _Def.CommandPrompt.W10_1909_CursorColor
            End Try
            ExtTerminal_PreviewCUR2.BackColor = ExtTerminal_CursorColor.BackColor

            Try
                y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\" & RegKey, "CursorType", _Def.CommandPrompt.W10_1909_CursorType)
                ExtTerminal_CursorStyle.SelectedIndex = y_cmd
            Catch
                ExtTerminal_CursorStyle.SelectedIndex = _Def.CommandPrompt.W10_1909_CursorType
            End Try

            Try
                y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\" & RegKey, "ForceV2", _Def.CommandPrompt.W10_1909_ForceV2)
                ExtTerminal_EnhancedTerminal.Checked = y_cmd
            Catch
                ExtTerminal_EnhancedTerminal.Checked = _Def.CommandPrompt.W10_1909_ForceV2
            End Try

            Try
                y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\" & RegKey, "LineSelection", _Def.CommandPrompt.W10_1909_LineSelection)
                ExtTerminal_LineSelection.Checked = y_cmd
            Catch
                ExtTerminal_LineSelection.Checked = _Def.CommandPrompt.W10_1909_LineSelection
            End Try


            Try
                y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\" & RegKey, "TerminalScrolling", _Def.CommandPrompt.W10_1909_TerminalScrolling)
                ExtTerminal_TerminalScrolling.Checked = y_cmd
            Catch
                ExtTerminal_TerminalScrolling.Checked = _Def.CommandPrompt.W10_1909_TerminalScrolling
            End Try


            Try
                y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\" & RegKey, "WindowAlpha", 100)
                ExtTerminal_OpacityBar.Value = y_cmd
            Catch
                ExtTerminal_OpacityBar.Value = 100
            End Try
        End If

        UpdateFromTrack(1) : UpdateFromTrack(2) : UpdateFromTrack(3) : UpdateFromTrack(4)
        ApplyPreview()

    End Sub

    Sub SetToExtTerminal(RegKey As String)
        Try
            CP.EditReg("HKEY_CURRENT_USER\Console\" & RegKey, "EnableColorSelection", 1)
            CP.EditReg("HKEY_CURRENT_USER\Console\" & RegKey, "ColorTable00", Color.FromArgb(0, ExtTerminal_ColorTable00.BackColor.Reverse).ToArgb)
            CP.EditReg("HKEY_CURRENT_USER\Console\" & RegKey, "ColorTable01", Color.FromArgb(0, ExtTerminal_ColorTable01.BackColor.Reverse).ToArgb)
            CP.EditReg("HKEY_CURRENT_USER\Console\" & RegKey, "ColorTable02", Color.FromArgb(0, ExtTerminal_ColorTable02.BackColor.Reverse).ToArgb)
            CP.EditReg("HKEY_CURRENT_USER\Console\" & RegKey, "ColorTable03", Color.FromArgb(0, ExtTerminal_ColorTable03.BackColor.Reverse).ToArgb)
            CP.EditReg("HKEY_CURRENT_USER\Console\" & RegKey, "ColorTable04", Color.FromArgb(0, ExtTerminal_ColorTable04.BackColor.Reverse).ToArgb)
            CP.EditReg("HKEY_CURRENT_USER\Console\" & RegKey, "ColorTable05", Color.FromArgb(0, ExtTerminal_ColorTable05.BackColor.Reverse).ToArgb)
            CP.EditReg("HKEY_CURRENT_USER\Console\" & RegKey, "ColorTable06", Color.FromArgb(0, ExtTerminal_ColorTable06.BackColor.Reverse).ToArgb)
            CP.EditReg("HKEY_CURRENT_USER\Console\" & RegKey, "ColorTable07", Color.FromArgb(0, ExtTerminal_ColorTable07.BackColor.Reverse).ToArgb)
            CP.EditReg("HKEY_CURRENT_USER\Console\" & RegKey, "ColorTable08", Color.FromArgb(0, ExtTerminal_ColorTable08.BackColor.Reverse).ToArgb)
            CP.EditReg("HKEY_CURRENT_USER\Console\" & RegKey, "ColorTable09", Color.FromArgb(0, ExtTerminal_ColorTable09.BackColor.Reverse).ToArgb)
            CP.EditReg("HKEY_CURRENT_USER\Console\" & RegKey, "ColorTable10", Color.FromArgb(0, ExtTerminal_ColorTable10.BackColor.Reverse).ToArgb)
            CP.EditReg("HKEY_CURRENT_USER\Console\" & RegKey, "ColorTable11", Color.FromArgb(0, ExtTerminal_ColorTable11.BackColor.Reverse).ToArgb)
            CP.EditReg("HKEY_CURRENT_USER\Console\" & RegKey, "ColorTable12", Color.FromArgb(0, ExtTerminal_ColorTable12.BackColor.Reverse).ToArgb)
            CP.EditReg("HKEY_CURRENT_USER\Console\" & RegKey, "ColorTable13", Color.FromArgb(0, ExtTerminal_ColorTable13.BackColor.Reverse).ToArgb)
            CP.EditReg("HKEY_CURRENT_USER\Console\" & RegKey, "ColorTable14", Color.FromArgb(0, ExtTerminal_ColorTable14.BackColor.Reverse).ToArgb)
            CP.EditReg("HKEY_CURRENT_USER\Console\" & RegKey, "ColorTable15", Color.FromArgb(0, ExtTerminal_ColorTable15.BackColor.Reverse).ToArgb)

            CP.EditReg("HKEY_CURRENT_USER\Console\" & RegKey, "PopupColors", Convert.ToInt32(ExtTerminal_PopupBackgroundBar.Value.ToString("X") & ExtTerminal_PopupForegroundBar.Value.ToString("X"), 16))
            CP.EditReg("HKEY_CURRENT_USER\Console\" & RegKey, "ScreenColors", Convert.ToInt32(ExtTerminal_AccentBackgroundBar.Value.ToString("X") & ExtTerminal_AccentForegroundBar.Value.ToString("X"), 16))
            CP.EditReg("HKEY_CURRENT_USER\Console\" & RegKey, "CursorSize", ExtTerminal_CursorSizeBar.Value)

            If My.W10_1909 Then
                CP.EditReg("HKEY_CURRENT_USER\Console\" & RegKey, "CursorColor", Color.FromArgb(0, ExtTerminal_CursorColor.BackColor.Reverse).ToArgb)
                CP.EditReg("HKEY_CURRENT_USER\Console\" & RegKey, "CursorType", ExtTerminal_CursorStyle.SelectedIndex)
                CP.EditReg("HKEY_CURRENT_USER\Console\" & RegKey, "WindowAlpha", ExtTerminal_OpacityBar.Value)
                CP.EditReg("HKEY_CURRENT_USER\Console\" & RegKey, "ForceV2", If(ExtTerminal_EnhancedTerminal.Checked, 1, 0))
                CP.EditReg("HKEY_CURRENT_USER\Console\" & RegKey, "LineSelection", If(ExtTerminal_LineSelection.Checked, 1, 0))
                CP.EditReg("HKEY_CURRENT_USER\Console\" & RegKey, "TerminalScrolling", If(ExtTerminal_TerminalScrolling.Checked, 1, 0))
            End If

            CP.EditReg("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Console\TrueTypeFont", "000", ExtTerminal_FontsBox.SelectedItem, RegistryValueKind.String)

            If Not ExtTerminal_RasterToggle.Checked Then
                CP.EditReg("HKEY_CURRENT_USER\Console\" & RegKey, "FontSize", ExtTerminal_FontSizeBar.Value * 65536)
            Else
                Select Case RasterList.SelectedItem
                    Case "4x6"
                        CP.EditReg("HKEY_CURRENT_USER\Console\" & RegKey, "FontSize", 393220)

                    Case "6x8"
                        CP.EditReg("HKEY_CURRENT_USER\Console\" & RegKey, "FontSize", 524294)

                    Case "8x8"
                        CP.EditReg("HKEY_CURRENT_USER\Console\" & RegKey, "FontSize", 524296)

                    Case "16x8"
                        CP.EditReg("HKEY_CURRENT_USER\Console\" & RegKey, "FontSize", 524304)

                    Case "5x12"
                        CP.EditReg("HKEY_CURRENT_USER\Console\" & RegKey, "FontSize", 786437)

                    Case "7x12"
                        CP.EditReg("HKEY_CURRENT_USER\Console\" & RegKey, "FontSize", 786439)

                    Case "8x12"
                        CP.EditReg("HKEY_CURRENT_USER\Console\" & RegKey, "FontSize", 0)

                    Case "16x12"
                        CP.EditReg("HKEY_CURRENT_USER\Console\" & RegKey, "FontSize", 786448)

                    Case "12x16"
                        CP.EditReg("HKEY_CURRENT_USER\Console\" & RegKey, "FontSize", 1048588)

                    Case "10x18"
                        CP.EditReg("HKEY_CURRENT_USER\Console\" & RegKey, "FontSize", 1179658)

                    Case Else
                        CP.EditReg("HKEY_CURRENT_USER\Console\" & RegKey, "FontSize", 0)

                End Select
            End If

            If ExtTerminal_RasterToggle.Checked Then
                CP.EditReg("HKEY_CURRENT_USER\Console\" & RegKey, "FontFamily", 48)
                CP.EditReg("HKEY_CURRENT_USER\Console\" & RegKey, "FaceName", "Terminal", RegistryValueKind.String)
            Else
                CP.EditReg("HKEY_CURRENT_USER\Console\" & RegKey, "FontFamily", If(ExtTerminal_RasterToggle.Checked, 1, 54))
                CP.EditReg("HKEY_CURRENT_USER\Console\" & RegKey, "FaceName", ExtTerminal_FontsBox.SelectedItem, RegistryValueKind.String)
            End If



            CP.EditReg("HKEY_CURRENT_USER\Console\" & RegKey, "FontWeight", ExtTerminal_FontWeightBox.SelectedIndex * 100)

            MsgBox(My.Application.LanguageHelper.ExtTer_Set, MsgBoxStyle.Information + My.Application.MsgboxRt)
        Catch ex As Exception
            MsgBox(ex.Message & vbCrLf & vbCrLf & ex.StackTrace, MsgBoxStyle.Critical)
        End Try
    End Sub

#Region " Events related to External Terminal"
    Private Sub ExtTerminal_PopupForegroundBar_Scroll(sender As Object) Handles ExtTerminal_PopupForegroundBar.Scroll
        With ExtTerminal_PopupForegroundBar
            ExtTerminal_PopupForegroundLbl.Text = .Value
            If .Value = 10 Then ExtTerminal_PopupForegroundLbl.Text &= " (A)"
            If .Value = 11 Then ExtTerminal_PopupForegroundLbl.Text &= " (B)"
            If .Value = 12 Then ExtTerminal_PopupForegroundLbl.Text &= " (C)"
            If .Value = 13 Then ExtTerminal_PopupForegroundLbl.Text &= " (D)"
            If .Value = 14 Then ExtTerminal_PopupForegroundLbl.Text &= " (E)"
            If .Value = 15 Then ExtTerminal_PopupForegroundLbl.Text &= " (F)"
        End With

        UpdateFromTrack(1)
        ApplyPreview()
    End Sub

    Private Sub ExtTerminal_PopupBackgroundBar_Scroll(sender As Object) Handles ExtTerminal_PopupBackgroundBar.Scroll
        With ExtTerminal_PopupBackgroundBar
            ExtTerminal_PopupBackgroundLbl.Text = .Value
            If .Value = 10 Then ExtTerminal_PopupBackgroundLbl.Text &= " (A)"
            If .Value = 11 Then ExtTerminal_PopupBackgroundLbl.Text &= " (B)"
            If .Value = 12 Then ExtTerminal_PopupBackgroundLbl.Text &= " (C)"
            If .Value = 13 Then ExtTerminal_PopupBackgroundLbl.Text &= " (D)"
            If .Value = 14 Then ExtTerminal_PopupBackgroundLbl.Text &= " (E)"
            If .Value = 15 Then ExtTerminal_PopupBackgroundLbl.Text &= " (F)"
        End With

        UpdateFromTrack(2)
        ApplyPreview()
    End Sub

    Private Sub ExtTerminal_AccentForegroundBar_Scroll(sender As Object) Handles ExtTerminal_AccentForegroundBar.Scroll
        With ExtTerminal_AccentForegroundBar
            ExtTerminal_AccentForegroundLbl.Text = .Value
            If .Value = 10 Then ExtTerminal_AccentForegroundLbl.Text &= " (A)"
            If .Value = 11 Then ExtTerminal_AccentForegroundLbl.Text &= " (B)"
            If .Value = 12 Then ExtTerminal_AccentForegroundLbl.Text &= " (C)"
            If .Value = 13 Then ExtTerminal_AccentForegroundLbl.Text &= " (D)"
            If .Value = 14 Then ExtTerminal_AccentForegroundLbl.Text &= " (E)"
            If .Value = 15 Then ExtTerminal_AccentForegroundLbl.Text &= " (F)"
        End With

        UpdateFromTrack(3) : UpdateFromTrack(4)
        ApplyPreview()
    End Sub

    Private Sub ExtTerminal_AccentBackgroundBar_Scroll(sender As Object) Handles ExtTerminal_AccentBackgroundBar.Scroll
        With ExtTerminal_AccentBackgroundBar
            ExtTerminal_AccentBackgroundLbl.Text = .Value
            If .Value = 10 Then ExtTerminal_AccentBackgroundLbl.Text &= " (A)"
            If .Value = 11 Then ExtTerminal_AccentBackgroundLbl.Text &= " (B)"
            If .Value = 12 Then ExtTerminal_AccentBackgroundLbl.Text &= " (C)"
            If .Value = 13 Then ExtTerminal_AccentBackgroundLbl.Text &= " (D)"
            If .Value = 14 Then ExtTerminal_AccentBackgroundLbl.Text &= " (E)"
            If .Value = 15 Then ExtTerminal_AccentBackgroundLbl.Text &= " (F)"
        End With

        UpdateFromTrack(3) : UpdateFromTrack(4)
        ApplyPreview()
    End Sub

    Private Sub ExtTerminal_RasterToggle_CheckedChanged(sender As Object, e As EventArgs) Handles ExtTerminal_RasterToggle.CheckedChanged
        If _Shown Then
            RasterList.Visible = ExtTerminal_RasterToggle.Checked
            ApplyPreview()
        End If
    End Sub

    Private Sub ExtTerminal_FontWeightBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ExtTerminal_FontWeightBox.SelectedIndexChanged
        If Not _Shown Then Exit Sub
        Dim fx As New LogFont
        f_extterminal = New Font(ExtTerminal_FontsBox.SelectedItem.ToString, f_extterminal.Size, f_extterminal.Style)
        f_extterminal.ToLogFont(fx)
        fx.lfWeight = ExtTerminal_FontWeightBox.SelectedIndex * 100
        With Font.FromLogFont(fx) : f_extterminal = New Font(.Name, f_extterminal.Size, .Style) : End With
        ExtTerminal_FontsBox.SelectedItem = f_extterminal.Name
        ApplyPreview()
    End Sub

    Private Sub ExtTerminal_FontsBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ExtTerminal_FontsBox.SelectedIndexChanged
        If _Shown Then
            f_extterminal = New Font(ExtTerminal_FontsBox.SelectedItem.ToString, f_extterminal.Size, f_extterminal.Style)
            ApplyPreview()
        End If

    End Sub

    Private Sub ExtTerminal_FontSizeBar_Scroll(sender As Object) Handles ExtTerminal_FontSizeBar.Scroll
        ExtTerminal_FontSizeLbl.Text = ExtTerminal_FontSizeBar.Value
        If _Shown Then
            f_extterminal = New Font(f_extterminal.Name, ExtTerminal_FontSizeBar.Value, f_extterminal.Style)
            ApplyPreview()
        End If
    End Sub

    Private Sub ExtTerminal_CursorSizeBar_Scroll(sender As Object) Handles ExtTerminal_CursorSizeBar.Scroll
        UpdateCurPreview()
    End Sub

    Private Sub ExtTerminal_CursorStyle_SelectedIndexChanged_1(sender As Object, e As EventArgs) Handles ExtTerminal_CursorStyle.SelectedIndexChanged
        If My.W10_1909 Then
            ApplyCursorShape()
        End If
    End Sub

    Private Sub ExtTerminal_CursorColor_Click(sender As Object, e As EventArgs) Handles ExtTerminal_CursorColor.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            Exit Sub
        End If

        Dim CList As New List(Of Control) From {sender, ExtTerminal_PreviewCUR2}

        Dim C As Color = ColorPickerDlg.Pick(CList)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()
    End Sub

    Private Sub ExtTerminal_OpacityBar_Scroll(sender As Object) Handles ExtTerminal_OpacityBar.Scroll
        ExtTerminal_OpacityLbl.Text = Fix((sender.Value / 255) * 100)
    End Sub

    Private Sub XenonCheckBox1_CheckedChanged(sender As Object) Handles XenonCheckBox1.CheckedChanged
        If _Shown Then
            FillFonts(ExtTerminal_FontsBox, Not XenonCheckBox1.Checked)
        End If
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
    Function FixValue(i As Integer) As Integer
        Dim v As Integer
        v = i
        If v < 12 Then v = 12
        If v > 12 And v < 18 Then v = 12
        If v > 18 And v < 24 Then v = 18
        If v > 24 And v < 30 Then v = 24
        If v > 30 And v < 36 Then v = 30
        If v > 36 And v < 42 Then v = 36
        If v > 42 And v < 48 Then v = 42
        If v > 48 Then v = 48
        Return v
    End Function

#End Region

    Private Sub ColorTable00_Click(sender As Object, e As EventArgs) Handles ExtTerminal_ColorTable15.Click, ExtTerminal_ColorTable14.Click, ExtTerminal_ColorTable13.Click, ExtTerminal_ColorTable12.Click, ExtTerminal_ColorTable11.Click, ExtTerminal_ColorTable10.Click, ExtTerminal_ColorTable09.Click, ExtTerminal_ColorTable08.Click, ExtTerminal_ColorTable07.Click, ExtTerminal_ColorTable06.Click, ExtTerminal_ColorTable05.Click, ExtTerminal_ColorTable04.Click, ExtTerminal_ColorTable03.Click, ExtTerminal_ColorTable02.Click, ExtTerminal_ColorTable01.Click, ExtTerminal_ColorTable00.Click

        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            ApplyPreview()
            Exit Sub
        End If

        Dim CList As New List(Of Control) From {sender}
        CList.Add(XenonCMD4)

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
        ApplyPreview()

        UpdateFromTrack(1) : UpdateFromTrack(2) : UpdateFromTrack(3) : UpdateFromTrack(4)

        CList.Clear()
    End Sub

#Region "   Subs to modify colors or shapes"
    Public Sub ApplyCursorShape()

        ExtTerminal_PreviewCursorInner.Dock = DockStyle.Fill
        ExtTerminal_PreviewCUR2.Padding = New Padding(1, 1, 1, 1)

        If ExtTerminal_CursorStyle.SelectedIndex = 0 Then
            ExtTerminal_PreviewCursorInner.BackColor = Color.Transparent
            ExtTerminal_PreviewCUR2.Width = 8

            Dim all As Integer = ExtTerminal_PreviewCUR.Height - 4
            ExtTerminal_PreviewCUR2.Height = all * (ExtTerminal_CursorSizeBar.Value / ExtTerminal_CursorSizeBar.Maximum)
            ExtTerminal_PreviewCUR2.Top = ExtTerminal_PreviewCUR.Height - ExtTerminal_PreviewCUR2.Height - 2
        End If

        If ExtTerminal_CursorStyle.SelectedIndex = 1 Then
            ExtTerminal_PreviewCursorInner.BackColor = Color.Transparent

            ExtTerminal_PreviewCUR2.Width = 1

            Dim all As Integer = ExtTerminal_PreviewCUR.Height - 4
            ExtTerminal_PreviewCUR2.Height = all
            ExtTerminal_PreviewCUR2.Top = ExtTerminal_PreviewCUR.Height - ExtTerminal_PreviewCUR2.Height - 2
        End If

        If ExtTerminal_CursorStyle.SelectedIndex = 2 Then
            ExtTerminal_PreviewCursorInner.BackColor = Color.Transparent

            ExtTerminal_PreviewCUR2.Width = 10
            ExtTerminal_PreviewCUR2.Height = 1

            ExtTerminal_PreviewCUR2.Top = ExtTerminal_PreviewCUR.Height - ExtTerminal_PreviewCUR2.Height - 2
        End If

        If ExtTerminal_CursorStyle.SelectedIndex = 3 Then
            ExtTerminal_PreviewCursorInner.BackColor = ExtTerminal_PreviewCUR.BackColor

            ExtTerminal_PreviewCUR2.Width = 8

            Dim all As Integer = ExtTerminal_PreviewCUR.Height - 4
            ExtTerminal_PreviewCUR2.Height = all
            ExtTerminal_PreviewCUR2.Top = ExtTerminal_PreviewCUR.Height - ExtTerminal_PreviewCUR2.Height - 2
        End If

        If ExtTerminal_CursorStyle.SelectedIndex = 4 Then
            ExtTerminal_PreviewCursorInner.BackColor = Color.Transparent

            ExtTerminal_PreviewCUR2.Width = 8

            Dim all As Integer = ExtTerminal_PreviewCUR.Height - 4
            ExtTerminal_PreviewCUR2.Height = all
            ExtTerminal_PreviewCUR2.Top = ExtTerminal_PreviewCUR.Height - ExtTerminal_PreviewCUR2.Height - 2
        End If

        If ExtTerminal_CursorStyle.SelectedIndex = 5 Then
            ExtTerminal_PreviewCursorInner.Dock = DockStyle.None
            ExtTerminal_PreviewCUR2.Padding = New Padding(0, 0, 0, 0)
            ExtTerminal_PreviewCursorInner.Width = ExtTerminal_PreviewCUR2.Width
            ExtTerminal_PreviewCursorInner.Height = 1
            ExtTerminal_PreviewCursorInner.BackColor = ExtTerminal_PreviewCUR.BackColor
            ExtTerminal_PreviewCursorInner.Top = 1
            ExtTerminal_PreviewCursorInner.Left = 0
            ExtTerminal_PreviewCUR2.Width = 8
            ExtTerminal_PreviewCUR2.Height = 3
            ExtTerminal_PreviewCUR2.Top = ExtTerminal_PreviewCUR.Height - ExtTerminal_PreviewCUR2.Height - 2
        End If
    End Sub
    Sub UpdateCurPreview()
        Dim all As Integer = ExtTerminal_PreviewCUR.Height - 4
        ExtTerminal_PreviewCUR2.Height = all * (ExtTerminal_CursorSizeBar.Value / ExtTerminal_CursorSizeBar.Maximum)
        ExtTerminal_PreviewCUR2.Top = ExtTerminal_PreviewCUR.Height - ExtTerminal_PreviewCUR2.Height - 2
        ExtTerminal_PreviewCUR_LBL.Text = ExtTerminal_CursorSizeBar.Value
        If My.W10_1909 Then ApplyCursorShape()
    End Sub
    Sub ApplyPreview()

#Region " External Terminal"
        XenonCMD4.CMD_ColorTable00 = ExtTerminal_ColorTable00.BackColor
        XenonCMD4.CMD_ColorTable01 = ExtTerminal_ColorTable01.BackColor
        XenonCMD4.CMD_ColorTable02 = ExtTerminal_ColorTable02.BackColor
        XenonCMD4.CMD_ColorTable03 = ExtTerminal_ColorTable03.BackColor
        XenonCMD4.CMD_ColorTable04 = ExtTerminal_ColorTable04.BackColor
        XenonCMD4.CMD_ColorTable05 = ExtTerminal_ColorTable05.BackColor
        XenonCMD4.CMD_ColorTable06 = ExtTerminal_ColorTable06.BackColor
        XenonCMD4.CMD_ColorTable07 = ExtTerminal_ColorTable07.BackColor
        XenonCMD4.CMD_ColorTable08 = ExtTerminal_ColorTable08.BackColor
        XenonCMD4.CMD_ColorTable09 = ExtTerminal_ColorTable09.BackColor
        XenonCMD4.CMD_ColorTable10 = ExtTerminal_ColorTable10.BackColor
        XenonCMD4.CMD_ColorTable11 = ExtTerminal_ColorTable11.BackColor
        XenonCMD4.CMD_ColorTable12 = ExtTerminal_ColorTable12.BackColor
        XenonCMD4.CMD_ColorTable13 = ExtTerminal_ColorTable13.BackColor
        XenonCMD4.CMD_ColorTable14 = ExtTerminal_ColorTable14.BackColor
        XenonCMD4.CMD_ColorTable15 = ExtTerminal_ColorTable15.BackColor
        XenonCMD4.CMD_PopupForeground = ExtTerminal_PopupForegroundBar.Value
        XenonCMD4.CMD_PopupBackground = ExtTerminal_PopupBackgroundBar.Value
        XenonCMD4.CMD_ScreenColorsForeground = ExtTerminal_AccentForegroundBar.Value
        XenonCMD4.CMD_ScreenColorsBackground = ExtTerminal_AccentBackgroundBar.Value
        XenonCMD4.Font = New Font(f_extterminal.Name, f_extterminal.Size, f_extterminal.Style)
        XenonCMD4.Raster = ExtTerminal_RasterToggle.Checked

        Select Case RasterList.SelectedItem
            Case "4x6"
                XenonCMD4.RasterSize = XenonCMD.Raster_Sizes._4x6

            Case "6x8"
                XenonCMD4.RasterSize = XenonCMD.Raster_Sizes._6x8

            Case "8x8"
                XenonCMD4.RasterSize = XenonCMD.Raster_Sizes._8x8

            Case "16x8"
                XenonCMD4.RasterSize = XenonCMD.Raster_Sizes._16x8

            Case "5x12"
                XenonCMD4.RasterSize = XenonCMD.Raster_Sizes._5x12

            Case "7x12"
                XenonCMD4.RasterSize = XenonCMD.Raster_Sizes._7x12

            Case "8x12"
                XenonCMD4.RasterSize = XenonCMD.Raster_Sizes._8x12

            Case "16x12"
                XenonCMD4.RasterSize = XenonCMD.Raster_Sizes._16x12

            Case "12x16"
                XenonCMD4.RasterSize = XenonCMD.Raster_Sizes._12x16

            Case "10x18"
                XenonCMD4.RasterSize = XenonCMD.Raster_Sizes._10x18

            Case Else
                XenonCMD4.RasterSize = XenonCMD.Raster_Sizes._8x12

        End Select

#End Region


        XenonCMD4.Refresh()
    End Sub
    Sub UpdateFromTrack(i As Integer)

        If i = 1 Then
            Select Case ExtTerminal_PopupForegroundBar.Value
                Case 0
                    ExtTerminal_PopupForegroundLbl.BackColor = ExtTerminal_ColorTable00.BackColor
                Case 1
                    ExtTerminal_PopupForegroundLbl.BackColor = ExtTerminal_ColorTable01.BackColor
                Case 2
                    ExtTerminal_PopupForegroundLbl.BackColor = ExtTerminal_ColorTable02.BackColor
                Case 3
                    ExtTerminal_PopupForegroundLbl.BackColor = ExtTerminal_ColorTable03.BackColor
                Case 4
                    ExtTerminal_PopupForegroundLbl.BackColor = ExtTerminal_ColorTable04.BackColor
                Case 5
                    ExtTerminal_PopupForegroundLbl.BackColor = ExtTerminal_ColorTable05.BackColor
                Case 6
                    ExtTerminal_PopupForegroundLbl.BackColor = ExtTerminal_ColorTable06.BackColor
                Case 7
                    ExtTerminal_PopupForegroundLbl.BackColor = ExtTerminal_ColorTable07.BackColor
                Case 8
                    ExtTerminal_PopupForegroundLbl.BackColor = ExtTerminal_ColorTable08.BackColor
                Case 9
                    ExtTerminal_PopupForegroundLbl.BackColor = ExtTerminal_ColorTable09.BackColor
                Case 10
                    ExtTerminal_PopupForegroundLbl.BackColor = ExtTerminal_ColorTable10.BackColor
                Case 11
                    ExtTerminal_PopupForegroundLbl.BackColor = ExtTerminal_ColorTable11.BackColor
                Case 12
                    ExtTerminal_PopupForegroundLbl.BackColor = ExtTerminal_ColorTable12.BackColor
                Case 13
                    ExtTerminal_PopupForegroundLbl.BackColor = ExtTerminal_ColorTable13.BackColor
                Case 14
                    ExtTerminal_PopupForegroundLbl.BackColor = ExtTerminal_ColorTable14.BackColor
                Case 15
                    ExtTerminal_PopupForegroundLbl.BackColor = ExtTerminal_ColorTable15.BackColor
            End Select

            Dim FC0 As Color = If(ExtTerminal_PopupForegroundLbl.BackColor.IsDark, ExtTerminal_PopupForegroundLbl.BackColor.LightLight, ExtTerminal_PopupForegroundLbl.BackColor.Dark(0.9))
            ExtTerminal_PopupForegroundLbl.ForeColor = FC0
            ExtTerminal_PopupForegroundBar.AccentColor = ExtTerminal_PopupForegroundLbl.BackColor
            ExtTerminal_PopupForegroundBar.Invalidate()

        ElseIf i = 2 Then

            Select Case ExtTerminal_PopupBackgroundBar.Value
                Case 0
                    ExtTerminal_PopupBackgroundLbl.BackColor = ExtTerminal_ColorTable00.BackColor
                Case 1
                    ExtTerminal_PopupBackgroundLbl.BackColor = ExtTerminal_ColorTable01.BackColor
                Case 2
                    ExtTerminal_PopupBackgroundLbl.BackColor = ExtTerminal_ColorTable02.BackColor
                Case 3
                    ExtTerminal_PopupBackgroundLbl.BackColor = ExtTerminal_ColorTable03.BackColor
                Case 4
                    ExtTerminal_PopupBackgroundLbl.BackColor = ExtTerminal_ColorTable04.BackColor
                Case 5
                    ExtTerminal_PopupBackgroundLbl.BackColor = ExtTerminal_ColorTable05.BackColor
                Case 6
                    ExtTerminal_PopupBackgroundLbl.BackColor = ExtTerminal_ColorTable06.BackColor
                Case 7
                    ExtTerminal_PopupBackgroundLbl.BackColor = ExtTerminal_ColorTable07.BackColor
                Case 8
                    ExtTerminal_PopupBackgroundLbl.BackColor = ExtTerminal_ColorTable08.BackColor
                Case 9
                    ExtTerminal_PopupBackgroundLbl.BackColor = ExtTerminal_ColorTable09.BackColor
                Case 10
                    ExtTerminal_PopupBackgroundLbl.BackColor = ExtTerminal_ColorTable10.BackColor
                Case 11
                    ExtTerminal_PopupBackgroundLbl.BackColor = ExtTerminal_ColorTable11.BackColor
                Case 12
                    ExtTerminal_PopupBackgroundLbl.BackColor = ExtTerminal_ColorTable12.BackColor
                Case 13
                    ExtTerminal_PopupBackgroundLbl.BackColor = ExtTerminal_ColorTable13.BackColor
                Case 14
                    ExtTerminal_PopupBackgroundLbl.BackColor = ExtTerminal_ColorTable14.BackColor
                Case 15
                    ExtTerminal_PopupBackgroundLbl.BackColor = ExtTerminal_ColorTable15.BackColor
            End Select

            Dim FC0 As Color = If(ExtTerminal_PopupBackgroundLbl.BackColor.IsDark, ExtTerminal_PopupBackgroundLbl.BackColor.LightLight, ExtTerminal_PopupBackgroundLbl.BackColor.Dark(0.9))
            ExtTerminal_PopupBackgroundLbl.ForeColor = FC0
            ExtTerminal_PopupBackgroundBar.AccentColor = ExtTerminal_PopupBackgroundLbl.BackColor
            ExtTerminal_PopupBackgroundBar.Invalidate()

        ElseIf i = 3 Then

            Select Case ExtTerminal_AccentBackgroundBar.Value
                Case 0
                    ExtTerminal_AccentBackgroundLbl.BackColor = ExtTerminal_ColorTable00.BackColor
                Case 1
                    ExtTerminal_AccentBackgroundLbl.BackColor = ExtTerminal_ColorTable01.BackColor
                Case 2
                    ExtTerminal_AccentBackgroundLbl.BackColor = ExtTerminal_ColorTable02.BackColor
                Case 3
                    ExtTerminal_AccentBackgroundLbl.BackColor = ExtTerminal_ColorTable03.BackColor
                Case 4
                    ExtTerminal_AccentBackgroundLbl.BackColor = ExtTerminal_ColorTable04.BackColor
                Case 5
                    ExtTerminal_AccentBackgroundLbl.BackColor = ExtTerminal_ColorTable05.BackColor
                Case 6
                    ExtTerminal_AccentBackgroundLbl.BackColor = ExtTerminal_ColorTable06.BackColor
                Case 7
                    ExtTerminal_AccentBackgroundLbl.BackColor = ExtTerminal_ColorTable07.BackColor
                Case 8
                    ExtTerminal_AccentBackgroundLbl.BackColor = ExtTerminal_ColorTable08.BackColor
                Case 9
                    ExtTerminal_AccentBackgroundLbl.BackColor = ExtTerminal_ColorTable09.BackColor
                Case 10
                    ExtTerminal_AccentBackgroundLbl.BackColor = ExtTerminal_ColorTable10.BackColor
                Case 11
                    ExtTerminal_AccentBackgroundLbl.BackColor = ExtTerminal_ColorTable11.BackColor
                Case 12
                    ExtTerminal_AccentBackgroundLbl.BackColor = ExtTerminal_ColorTable12.BackColor
                Case 13
                    ExtTerminal_AccentBackgroundLbl.BackColor = ExtTerminal_ColorTable13.BackColor
                Case 14
                    ExtTerminal_AccentBackgroundLbl.BackColor = ExtTerminal_ColorTable14.BackColor
                Case 15
                    ExtTerminal_AccentBackgroundLbl.BackColor = ExtTerminal_ColorTable15.BackColor
            End Select

            Dim FC0 As Color = If(ExtTerminal_AccentBackgroundLbl.BackColor.IsDark, ExtTerminal_AccentBackgroundLbl.BackColor.LightLight, ExtTerminal_AccentBackgroundLbl.BackColor.Dark(0.9))
            ExtTerminal_AccentBackgroundLbl.ForeColor = FC0
            ExtTerminal_AccentBackgroundBar.AccentColor = ExtTerminal_AccentBackgroundLbl.BackColor
            ExtTerminal_AccentBackgroundBar.Invalidate()
            ExtTerminal_PreviewCUR.BackColor = ExtTerminal_AccentBackgroundLbl.BackColor
        ElseIf i = 4 Then

            Select Case ExtTerminal_AccentForegroundBar.Value
                Case 0
                    If ExtTerminal_AccentBackgroundBar.Value = ExtTerminal_AccentForegroundBar.Value Then
                        ExtTerminal_AccentForegroundLbl.BackColor = ExtTerminal_ColorTable07.BackColor
                    Else
                        ExtTerminal_AccentForegroundLbl.BackColor = ExtTerminal_ColorTable00.BackColor
                    End If
                Case 1
                    ExtTerminal_AccentForegroundLbl.BackColor = ExtTerminal_ColorTable01.BackColor
                Case 2
                    ExtTerminal_AccentForegroundLbl.BackColor = ExtTerminal_ColorTable02.BackColor
                Case 3
                    ExtTerminal_AccentForegroundLbl.BackColor = ExtTerminal_ColorTable03.BackColor
                Case 4
                    ExtTerminal_AccentForegroundLbl.BackColor = ExtTerminal_ColorTable04.BackColor
                Case 5
                    ExtTerminal_AccentForegroundLbl.BackColor = ExtTerminal_ColorTable05.BackColor
                Case 6
                    ExtTerminal_AccentForegroundLbl.BackColor = ExtTerminal_ColorTable06.BackColor
                Case 7
                    ExtTerminal_AccentForegroundLbl.BackColor = ExtTerminal_ColorTable07.BackColor
                Case 8
                    ExtTerminal_AccentForegroundLbl.BackColor = ExtTerminal_ColorTable08.BackColor
                Case 9
                    ExtTerminal_AccentForegroundLbl.BackColor = ExtTerminal_ColorTable09.BackColor
                Case 10
                    ExtTerminal_AccentForegroundLbl.BackColor = ExtTerminal_ColorTable10.BackColor
                Case 11
                    ExtTerminal_AccentForegroundLbl.BackColor = ExtTerminal_ColorTable11.BackColor
                Case 12
                    ExtTerminal_AccentForegroundLbl.BackColor = ExtTerminal_ColorTable12.BackColor
                Case 13
                    ExtTerminal_AccentForegroundLbl.BackColor = ExtTerminal_ColorTable13.BackColor
                Case 14
                    ExtTerminal_AccentForegroundLbl.BackColor = ExtTerminal_ColorTable14.BackColor
                Case 15
                    ExtTerminal_AccentForegroundLbl.BackColor = ExtTerminal_ColorTable15.BackColor
            End Select

            Dim FC0 As Color = If(ExtTerminal_AccentForegroundLbl.BackColor.IsDark, ExtTerminal_AccentForegroundLbl.BackColor.LightLight, ExtTerminal_AccentForegroundLbl.BackColor.Dark(0.9))
            ExtTerminal_AccentForegroundLbl.ForeColor = FC0
            ExtTerminal_AccentForegroundBar.AccentColor = ExtTerminal_AccentForegroundLbl.BackColor
            ExtTerminal_AccentForegroundBar.Invalidate()
        End If
    End Sub
#End Region

    Private Sub XenonButton6_Click(sender As Object, e As EventArgs) Handles XenonButton6.Click
        FillTerminals(XenonComboBox1)
    End Sub

    Private Sub XenonButton7_Click(sender As Object, e As EventArgs) Handles XenonButton7.Click
        GetFromExtTerminal(XenonComboBox1.SelectedItem)
    End Sub

    Private Sub XenonButton10_Click(sender As Object, e As EventArgs) Handles XenonButton10.Click
        If XenonComboBox1.SelectedItem IsNot Nothing Then SetToExtTerminal(XenonComboBox1.SelectedItem)
    End Sub

    Private Sub XenonButton2_Click(sender As Object, e As EventArgs) Handles XenonButton2.Click
        Me.Close()
    End Sub

    Private Sub ExternalTerminal_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        MainFrm.Visible = True
    End Sub

    Private Sub XenonButton1_Click(sender As Object, e As EventArgs) Handles XenonButton1.Click
        NewExtTerminal.ShowDialog()
    End Sub

    Private Sub RasterList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles RasterList.SelectedIndexChanged
        If _Shown Then
            ApplyPreview()
        End If
    End Sub



    Sub ApplyFromCP(CP As CP)

        ExtTerminal_ColorTable00.BackColor = CP.CommandPrompt.ColorTable00
        ExtTerminal_ColorTable01.BackColor = CP.CommandPrompt.ColorTable01
        ExtTerminal_ColorTable02.BackColor = CP.CommandPrompt.ColorTable02
        ExtTerminal_ColorTable03.BackColor = CP.CommandPrompt.ColorTable03
        ExtTerminal_ColorTable04.BackColor = CP.CommandPrompt.ColorTable04
        ExtTerminal_ColorTable05.BackColor = CP.CommandPrompt.ColorTable05
        ExtTerminal_ColorTable06.BackColor = CP.CommandPrompt.ColorTable06
        ExtTerminal_ColorTable07.BackColor = CP.CommandPrompt.ColorTable07
        ExtTerminal_ColorTable08.BackColor = CP.CommandPrompt.ColorTable08
        ExtTerminal_ColorTable09.BackColor = CP.CommandPrompt.ColorTable09
        ExtTerminal_ColorTable10.BackColor = CP.CommandPrompt.ColorTable10
        ExtTerminal_ColorTable11.BackColor = CP.CommandPrompt.ColorTable11
        ExtTerminal_ColorTable12.BackColor = CP.CommandPrompt.ColorTable12
        ExtTerminal_ColorTable13.BackColor = CP.CommandPrompt.ColorTable13
        ExtTerminal_ColorTable14.BackColor = CP.CommandPrompt.ColorTable14
        ExtTerminal_ColorTable15.BackColor = CP.CommandPrompt.ColorTable15

        ExtTerminal_ColorTable05.DefaultColor = Color.FromArgb(136, 23, 152)
        ExtTerminal_ColorTable06.DefaultColor = Color.FromArgb(193, 156, 0)

        ExtTerminal_PopupForegroundBar.Value = CP.CommandPrompt.PopupForeground
        ExtTerminal_PopupBackgroundBar.Value = CP.CommandPrompt.PopupBackground
        ExtTerminal_AccentForegroundBar.Value = CP.CommandPrompt.ScreenColorsForeground
        ExtTerminal_AccentBackgroundBar.Value = CP.CommandPrompt.ScreenColorsBackground
        ExtTerminal_RasterToggle.Checked = CP.CommandPrompt.FontRaster
        RasterList.Visible = CP.CommandPrompt.FontRaster

        Select Case CP.CommandPrompt.FontWeight
            Case 0
                ExtTerminal_FontWeightBox.SelectedIndex = 0

            Case 100
                ExtTerminal_FontWeightBox.SelectedIndex = 1

            Case 200
                ExtTerminal_FontWeightBox.SelectedIndex = 2

            Case 300
                ExtTerminal_FontWeightBox.SelectedIndex = 3

            Case 400
                ExtTerminal_FontWeightBox.SelectedIndex = 4

            Case 500
                ExtTerminal_FontWeightBox.SelectedIndex = 5

            Case 600
                ExtTerminal_FontWeightBox.SelectedIndex = 6

            Case 700
                ExtTerminal_FontWeightBox.SelectedIndex = 7

            Case 800
                ExtTerminal_FontWeightBox.SelectedIndex = 8

            Case 900
                ExtTerminal_FontWeightBox.SelectedIndex = 9

            Case Else
                ExtTerminal_FontWeightBox.SelectedIndex = 4

        End Select

        If Not CP.CommandPrompt.FontRaster Then
            With Font.FromLogFont(New LogFont With {.lfFaceName = CP.CommandPrompt.FaceName, .lfWeight = CP.CommandPrompt.FontWeight}) : f_extterminal = New Font(.FontFamily, CInt(CP.CommandPrompt.FontSize / 65536), .Style) : End With
        End If

        ExtTerminal_FontsBox.SelectedItem = f_extterminal.Name
        ExtTerminal_FontSizeBar.Value = f_extterminal.Size
        ExtTerminal_FontSizeLbl.Text = f_extterminal.Size

        If CP.CommandPrompt.FontSize = 393220 Then RasterList.SelectedItem = "4x6"
        If CP.CommandPrompt.FontSize = 524294 Then RasterList.SelectedItem = "6x8"
        If CP.CommandPrompt.FontSize = 524296 Then RasterList.SelectedItem = "8x8"
        If CP.CommandPrompt.FontSize = 524304 Then RasterList.SelectedItem = "16x8"
        If CP.CommandPrompt.FontSize = 786437 Then RasterList.SelectedItem = "5x12"
        If CP.CommandPrompt.FontSize = 786439 Then RasterList.SelectedItem = "7x12"
        If CP.CommandPrompt.FontSize = 0 Then RasterList.SelectedItem = "8x12"
        If CP.CommandPrompt.FontSize = 786448 Then RasterList.SelectedItem = "16x12"
        If CP.CommandPrompt.FontSize = 1048588 Then RasterList.SelectedItem = "12x16"
        If CP.CommandPrompt.FontSize = 1179658 Then RasterList.SelectedItem = "10x18"
        If RasterList.SelectedItem = Nothing Then RasterList.SelectedItem = "8x12"


        CP.CommandPrompt.CursorSize = ExtTerminal_CursorSizeBar.Value
        If ExtTerminal_CursorSizeBar.Value > 100 Then ExtTerminal_CursorSizeBar.Value = 100
        If ExtTerminal_CursorSizeBar.Value < 20 Then ExtTerminal_CursorSizeBar.Value = 20
        If My.W10_1909 Then
            ExtTerminal_CursorStyle.SelectedIndex = CP.CommandPrompt.W10_1909_CursorType
            ExtTerminal_CursorColor.BackColor = CP.CommandPrompt.W10_1909_CursorColor
            ExtTerminal_PreviewCUR2.BackColor = CP.CommandPrompt.W10_1909_CursorColor
            ExtTerminal_EnhancedTerminal.Checked = CP.CommandPrompt.W10_1909_ForceV2
            ExtTerminal_OpacityBar.Value = CP.CommandPrompt.W10_1909_WindowAlpha
            ExtTerminal_OpacityLbl.Text = Fix((CP.CommandPrompt.W10_1909_WindowAlpha / 255) * 100)
            ExtTerminal_LineSelection.Checked = CP.CommandPrompt.W10_1909_LineSelection
            ExtTerminal_TerminalScrolling.Checked = CP.CommandPrompt.W10_1909_TerminalScrolling
            ApplyCursorShape()
        End If
        UpdateCurPreview()
    End Sub

    Private Sub XenonButton8_Click(sender As Object, e As EventArgs) Handles XenonButton8.Click
        If Not Registry.CurrentUser.OpenSubKey("Console", True).GetSubKeyNames().Contains(XenonComboBox1.SelectedItem) Then
            MsgBox(My.Application.LanguageHelper.ExtTer_NotFound, MsgBoxStyle.Critical + My.Application.MsgboxRt)
            Exit Sub
        End If

        If OpenWPTHDlg.ShowDialog = DialogResult.OK Then
            Dim CPx As New CP(CP.Mode.File, OpenWPTHDlg.FileName)
            ApplyFromCP(CPx)
            ApplyPreview()
            CPx.Dispose()
        End If
    End Sub

    Private Sub XenonButton3_Click(sender As Object, e As EventArgs) Handles XenonButton3.Click
        If Not Registry.CurrentUser.OpenSubKey("Console", True).GetSubKeyNames().Contains(XenonComboBox1.SelectedItem) Then
            MsgBox(My.Application.LanguageHelper.ExtTer_NotFound, MsgBoxStyle.Critical + My.Application.MsgboxRt)
            Exit Sub
        End If

        Dim CPx As New CP(CP.Mode.Registry)
        ApplyFromCP(CPx)
        ApplyPreview()
        CPx.Dispose()
    End Sub

    Private Sub XenonButton4_Click(sender As Object, e As EventArgs) Handles XenonButton4.Click
        If Not Registry.CurrentUser.OpenSubKey("Console", True).GetSubKeyNames().Contains(XenonComboBox1.SelectedItem) Then
            MsgBox(My.Application.LanguageHelper.ExtTer_NotFound, MsgBoxStyle.Critical + My.Application.MsgboxRt)
            Exit Sub
        End If

        Dim _Def As CP
        If MainFrm.PreviewConfig = MainFrm.WinVer.Eleven Then
            _Def = New CP_Defaults().Default_Windows11
        ElseIf MainFrm.PreviewConfig = MainFrm.WinVer.Ten Then
            _Def = New CP_Defaults().Default_Windows10
        ElseIf MainFrm.PreviewConfig = MainFrm.WinVer.Eight Then
            _Def = New CP_Defaults().Default_Windows8
        ElseIf MainFrm.PreviewConfig = MainFrm.WinVer.Seven Then
            _Def = New CP_Defaults().Default_Windows7
        Else
            _Def = New CP_Defaults().Default_Windows11
        End If

        ApplyFromCP(_Def)
        ApplyPreview()

        _Def.Dispose()
    End Sub
End Class