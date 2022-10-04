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
        XenonCheckBox1.Checked = My.Application._Settings.Terminal_OtherFonts
        FillFonts(ExtTerminal_FontsBox, Not My.Application._Settings.Terminal_OtherFonts)
        MainFrm.Visible = False
        Location = New Point(10, (My.Computer.Screen.Bounds.Height - Height) / 2 - 20)
    End Sub

    Private Sub ExternalTerminal_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        _Shown = True
    End Sub

    Sub FillTerminals([ListBox] As ComboBox)
        [ListBox].Items.Clear()

        For Each x As String In Registry.CurrentUser.OpenSubKey("Console", True).GetSubKeyNames()
            If Not x.ToLower = "%%Startup".ToLower And
                Not x.ToLower = "%SystemRoot%_System32_WindowsPowerShell_v1.0_powershell.exe".ToLower And
                Not x.ToLower = "%SystemRoot%_SysWOW64_WindowsPowerShell_v1.0_powershell.exe".ToLower Then
                [ListBox].Items.Add(x)
            End If
        Next

    End Sub

    Sub GetFromExtTerminal(RegKey As String)

        If Not Registry.CurrentUser.OpenSubKey("Console", True).GetSubKeyNames().Contains(RegKey) Then
            MsgBox("Terminal not found. Select a valid one from the list.", MsgBoxStyle.Critical + My.Application.MsgboxRt)
            Exit Sub
        End If

        Dim y_cmd As Object

        Try
            y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\" & RegKey, "ColorTable00", BizareColorInvertor(Color.Black).ToArgb)
            ExtTerminal_ColorTable00.BackColor = Color.FromArgb(255, BizareColorInvertor(Color.FromArgb(y_cmd)))
        Catch
            ExtTerminal_ColorTable00.BackColor = Color.Black
        End Try

        Try
            y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\" & RegKey, "ColorTable01", BizareColorInvertor(Color.Black).ToArgb)
            ExtTerminal_ColorTable01.BackColor = Color.FromArgb(255, BizareColorInvertor(Color.FromArgb(y_cmd)))
        Catch
            ExtTerminal_ColorTable01.BackColor = Color.Black
        End Try

        Try
            y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\" & RegKey, "ColorTable02", BizareColorInvertor(Color.Black).ToArgb)
            ExtTerminal_ColorTable02.BackColor = Color.FromArgb(255, BizareColorInvertor(Color.FromArgb(y_cmd)))
        Catch
            ExtTerminal_ColorTable02.BackColor = Color.Black
        End Try

        Try
            y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\" & RegKey, "ColorTable03", BizareColorInvertor(Color.Black).ToArgb)
            ExtTerminal_ColorTable03.BackColor = Color.FromArgb(255, BizareColorInvertor(Color.FromArgb(y_cmd)))
        Catch
            ExtTerminal_ColorTable03.BackColor = Color.Black
        End Try

        Try
            y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\" & RegKey, "ColorTable04", BizareColorInvertor(Color.Black).ToArgb)
            ExtTerminal_ColorTable04.BackColor = Color.FromArgb(255, BizareColorInvertor(Color.FromArgb(y_cmd)))
        Catch
            ExtTerminal_ColorTable04.BackColor = Color.Black
        End Try

        Try
            y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\" & RegKey, "ColorTable05", BizareColorInvertor(Color.Black).ToArgb)
            ExtTerminal_ColorTable05.BackColor = Color.FromArgb(255, BizareColorInvertor(Color.FromArgb(y_cmd)))
        Catch
            ExtTerminal_ColorTable05.BackColor = Color.Black
        End Try

        Try
            y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\" & RegKey, "ColorTable06", BizareColorInvertor(Color.Black).ToArgb)
            ExtTerminal_ColorTable06.BackColor = Color.FromArgb(255, BizareColorInvertor(Color.FromArgb(y_cmd)))
        Catch
            ExtTerminal_ColorTable06.BackColor = Color.Black
        End Try

        Try
            y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\" & RegKey, "ColorTable07", BizareColorInvertor(Color.Black).ToArgb)
            ExtTerminal_ColorTable07.BackColor = Color.FromArgb(255, BizareColorInvertor(Color.FromArgb(y_cmd)))
        Catch
            ExtTerminal_ColorTable07.BackColor = Color.Black
        End Try

        Try
            y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\" & RegKey, "ColorTable08", BizareColorInvertor(Color.Black).ToArgb)
            ExtTerminal_ColorTable08.BackColor = Color.FromArgb(255, BizareColorInvertor(Color.FromArgb(y_cmd)))
        Catch
            ExtTerminal_ColorTable08.BackColor = Color.Black
        End Try

        Try
            y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\" & RegKey, "ColorTable09", BizareColorInvertor(Color.Black).ToArgb)
            ExtTerminal_ColorTable09.BackColor = Color.FromArgb(255, BizareColorInvertor(Color.FromArgb(y_cmd)))
        Catch
            ExtTerminal_ColorTable09.BackColor = Color.Black
        End Try

        Try
            y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\" & RegKey, "ColorTable10", BizareColorInvertor(Color.Black).ToArgb)
            ExtTerminal_ColorTable10.BackColor = Color.FromArgb(255, BizareColorInvertor(Color.FromArgb(y_cmd)))
        Catch
            ExtTerminal_ColorTable10.BackColor = Color.Black
        End Try

        Try
            y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\" & RegKey, "ColorTable11", BizareColorInvertor(Color.Black).ToArgb)
            ExtTerminal_ColorTable11.BackColor = Color.FromArgb(255, BizareColorInvertor(Color.FromArgb(y_cmd)))
        Catch
            ExtTerminal_ColorTable11.BackColor = Color.Black
        End Try

        Try
            y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\" & RegKey, "ColorTable12", BizareColorInvertor(Color.Black).ToArgb)
            ExtTerminal_ColorTable12.BackColor = Color.FromArgb(255, BizareColorInvertor(Color.FromArgb(y_cmd)))
        Catch
            ExtTerminal_ColorTable12.BackColor = Color.Black
        End Try

        Try
            y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\" & RegKey, "ColorTable13", BizareColorInvertor(Color.Black).ToArgb)
            ExtTerminal_ColorTable13.BackColor = Color.FromArgb(255, BizareColorInvertor(Color.FromArgb(y_cmd)))
        Catch
            ExtTerminal_ColorTable13.BackColor = Color.Black
        End Try

        Try
            y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\" & RegKey, "ColorTable14", BizareColorInvertor(Color.Black).ToArgb)
            ExtTerminal_ColorTable14.BackColor = Color.FromArgb(255, BizareColorInvertor(Color.FromArgb(y_cmd)))
        Catch
            ExtTerminal_ColorTable14.BackColor = Color.Black
        End Try

        Try
            y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\" & RegKey, "ColorTable15", BizareColorInvertor(Color.Black).ToArgb)
            ExtTerminal_ColorTable15.BackColor = Color.FromArgb(255, BizareColorInvertor(Color.FromArgb(y_cmd)))
        Catch
            ExtTerminal_ColorTable15.BackColor = Color.Black
        End Try

        Try
            y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\" & RegKey, "PopupColors", 245)

            With CInt(y_cmd).ToString("X")
                ExtTerminal_PopupBackgroundBar.Value = Convert.ToInt32(.Chars(0), 16)
                ExtTerminal_PopupForegroundBar.Value = Convert.ToInt32(.Chars(1), 16)
            End With
        Catch
            ExtTerminal_PopupBackgroundBar.Value = 15
            ExtTerminal_PopupForegroundBar.Value = 5
        End Try

        Try
            y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\" & RegKey, "ScreenColors", 7)

            With CInt(y_cmd).ToString("X")
                ExtTerminal_AccentBackgroundBar.Value = Convert.ToInt32(.Chars(0), 16)
                ExtTerminal_AccentForegroundBar.Value = Convert.ToInt32(.Chars(1), 16)
            End With
        Catch
            ExtTerminal_AccentBackgroundBar.Value = 0
            ExtTerminal_AccentForegroundBar.Value = 7
        End Try

        Try
            y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\" & RegKey, "CursorSize", 25)
            ExtTerminal_CursorSizeBar.Value = y_cmd
        Catch
            ExtTerminal_CursorSizeBar.Value = 25
        End Try

        Try
            y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\" & RegKey, "FontFamily", 54)
            ExtTerminal_RasterToggle.Checked = If(y_cmd = 0, True, False)
        Catch
            ExtTerminal_RasterToggle.Checked = False
        End Try

        Try
            y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\" & RegKey, "FontSize", 16 * 65536)
            ExtTerminal_FontSizeBar.Value = y_cmd / 65536
        Catch
            ExtTerminal_FontSizeBar.Value = 16
        End Try

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
            y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\" & RegKey, "FaceName", "Consolas")

            If CP.IsFontInstalled(y_cmd) Then
                With Font.FromLogFont(New LOGFONT With {.lfFaceName = y_cmd, .lfWeight = wg}) : f_extterminal = New Font(.FontFamily, CInt(ExtTerminal_FontSizeBar.Value), .Style) : End With
                ExtTerminal_FontsBox.SelectedItem = f_extterminal.Name
            Else
                ExtTerminal_FontsBox.SelectedItem = "Consolas"
            End If

        Catch
            ExtTerminal_FontsBox.SelectedItem = "Consolas"
        End Try

        If My.W10_1909 Then
            Try
                y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\" & RegKey, "CursorColor", BizareColorInvertor(Color.White).ToArgb)
                ExtTerminal_CursorColor.BackColor = Color.FromArgb(255, BizareColorInvertor(Color.FromArgb(y_cmd)))
            Catch
                ExtTerminal_CursorColor.BackColor = Color.White
            End Try
            ExtTerminal_PreviewCUR2.BackColor = ExtTerminal_CursorColor.BackColor

            Try
                y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\" & RegKey, "CursorType", 0)
                ExtTerminal_CursorStyle.SelectedIndex = y_cmd
            Catch
                ExtTerminal_CursorStyle.SelectedIndex = 1
            End Try

            Try
                y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\" & RegKey, "ForceV2", True)
                ExtTerminal_EnhancedTerminal.Checked = y_cmd
            Catch
                ExtTerminal_EnhancedTerminal.Checked = True
            End Try

            Try
                y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\" & RegKey, "LineSelection", False)
                ExtTerminal_LineSelection.Checked = y_cmd
            Catch
                ExtTerminal_LineSelection.Checked = False
            End Try


            Try
                y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\" & RegKey, "TerminalScrolling", False)
                ExtTerminal_TerminalScrolling.Checked = y_cmd
            Catch
                ExtTerminal_TerminalScrolling.Checked = False
            End Try


            Try
                y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\" & RegKey, "WindowAlpha", 100)
                ExtTerminal_OpacityBar.Value = y_cmd
            Catch
                ExtTerminal_OpacityBar.Value = 100
            End Try
        End If

        ApplyPreview()
    End Sub

    Sub SetToExtTerminal(RegKey As String)
        Try
            CP.EditReg("HKEY_CURRENT_USER\Console\" & RegKey, "EnableColorSelection", 1)
            CP.EditReg("HKEY_CURRENT_USER\Console\" & RegKey, "ColorTable00", Color.FromArgb(0, BizareColorInvertor(ExtTerminal_ColorTable00.BackColor)).ToArgb)
            CP.EditReg("HKEY_CURRENT_USER\Console\" & RegKey, "ColorTable01", Color.FromArgb(0, BizareColorInvertor(ExtTerminal_ColorTable01.BackColor)).ToArgb)
            CP.EditReg("HKEY_CURRENT_USER\Console\" & RegKey, "ColorTable02", Color.FromArgb(0, BizareColorInvertor(ExtTerminal_ColorTable02.BackColor)).ToArgb)
            CP.EditReg("HKEY_CURRENT_USER\Console\" & RegKey, "ColorTable03", Color.FromArgb(0, BizareColorInvertor(ExtTerminal_ColorTable03.BackColor)).ToArgb)
            CP.EditReg("HKEY_CURRENT_USER\Console\" & RegKey, "ColorTable04", Color.FromArgb(0, BizareColorInvertor(ExtTerminal_ColorTable04.BackColor)).ToArgb)
            CP.EditReg("HKEY_CURRENT_USER\Console\" & RegKey, "ColorTable05", Color.FromArgb(0, BizareColorInvertor(ExtTerminal_ColorTable05.BackColor)).ToArgb)
            CP.EditReg("HKEY_CURRENT_USER\Console\" & RegKey, "ColorTable06", Color.FromArgb(0, BizareColorInvertor(ExtTerminal_ColorTable06.BackColor)).ToArgb)
            CP.EditReg("HKEY_CURRENT_USER\Console\" & RegKey, "ColorTable07", Color.FromArgb(0, BizareColorInvertor(ExtTerminal_ColorTable07.BackColor)).ToArgb)
            CP.EditReg("HKEY_CURRENT_USER\Console\" & RegKey, "ColorTable08", Color.FromArgb(0, BizareColorInvertor(ExtTerminal_ColorTable08.BackColor)).ToArgb)
            CP.EditReg("HKEY_CURRENT_USER\Console\" & RegKey, "ColorTable09", Color.FromArgb(0, BizareColorInvertor(ExtTerminal_ColorTable09.BackColor)).ToArgb)
            CP.EditReg("HKEY_CURRENT_USER\Console\" & RegKey, "ColorTable10", Color.FromArgb(0, BizareColorInvertor(ExtTerminal_ColorTable10.BackColor)).ToArgb)
            CP.EditReg("HKEY_CURRENT_USER\Console\" & RegKey, "ColorTable11", Color.FromArgb(0, BizareColorInvertor(ExtTerminal_ColorTable11.BackColor)).ToArgb)
            CP.EditReg("HKEY_CURRENT_USER\Console\" & RegKey, "ColorTable12", Color.FromArgb(0, BizareColorInvertor(ExtTerminal_ColorTable12.BackColor)).ToArgb)
            CP.EditReg("HKEY_CURRENT_USER\Console\" & RegKey, "ColorTable13", Color.FromArgb(0, BizareColorInvertor(ExtTerminal_ColorTable13.BackColor)).ToArgb)
            CP.EditReg("HKEY_CURRENT_USER\Console\" & RegKey, "ColorTable14", Color.FromArgb(0, BizareColorInvertor(ExtTerminal_ColorTable14.BackColor)).ToArgb)
            CP.EditReg("HKEY_CURRENT_USER\Console\" & RegKey, "ColorTable15", Color.FromArgb(0, BizareColorInvertor(ExtTerminal_ColorTable15.BackColor)).ToArgb)

            CP.EditReg("HKEY_CURRENT_USER\Console\" & RegKey, "PopupColors", Convert.ToInt32(ExtTerminal_PopupBackgroundBar.Value.ToString("X") & ExtTerminal_PopupForegroundBar.Value.ToString("X"), 16))
            CP.EditReg("HKEY_CURRENT_USER\Console\" & RegKey, "ScreenColors", Convert.ToInt32(ExtTerminal_AccentBackgroundBar.Value.ToString("X") & ExtTerminal_AccentForegroundBar.Value.ToString("X"), 16))
            CP.EditReg("HKEY_CURRENT_USER\Console\" & RegKey, "CursorSize", ExtTerminal_CursorSizeBar.Value)

            If My.W10_1909 Then
                CP.EditReg("HKEY_CURRENT_USER\Console\" & RegKey, "CursorColor", Color.FromArgb(0, BizareColorInvertor(ExtTerminal_CursorColor.BackColor)).ToArgb)
                CP.EditReg("HKEY_CURRENT_USER\Console\" & RegKey, "CursorType", ExtTerminal_CursorStyle.SelectedIndex)
                CP.EditReg("HKEY_CURRENT_USER\Console\" & RegKey, "WindowAlpha", ExtTerminal_OpacityBar.Value)
                CP.EditReg("HKEY_CURRENT_USER\Console\" & RegKey, "ForceV2", If(ExtTerminal_EnhancedTerminal.Checked, 1, 0))
                CP.EditReg("HKEY_CURRENT_USER\Console\" & RegKey, "LineSelection", If(ExtTerminal_LineSelection.Checked, 1, 0))
                CP.EditReg("HKEY_CURRENT_USER\Console\" & RegKey, "TerminalScrolling", If(ExtTerminal_TerminalScrolling.Checked, 1, 0))
            End If

            CP.EditReg("HKEY_CURRENT_USER\Console\" & RegKey, "FaceName", ExtTerminal_FontsBox.SelectedItem, False, True)

            If New WindowsPrincipal(WindowsIdentity.GetCurrent).IsInRole(WindowsBuiltInRole.Administrator) Then
                CP.EditReg("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Console\TrueTypeFont", "000", ExtTerminal_FontsBox.SelectedItem, False, True)
            Else
                Dim ls As New List(Of String)
                ls.Clear()
                ls.Add("Windows Registry Editor Version 5.00")
                ls.Add(vbCrLf)
                ls.Add("[HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Console\TrueTypeFont]")
                ls.Add(String.Format("""000""=""{0}""", ExtTerminal_FontsBox.SelectedItem))

                Dim result As String = CStr_FromList(ls)

                If Not IO.Directory.Exists(My.Application.appData) Then IO.Directory.CreateDirectory(My.Application.appData)

                Dim tempreg As String = My.Application.appData & "\tempreg.reg"

                IO.File.WriteAllText(tempreg, result)

                Dim process As Process = Nothing

                Dim processStartInfo As New ProcessStartInfo With {
                   .FileName = "regedit",
                   .Verb = "runas",
                   .Arguments = String.Format("/s ""{0}""", tempreg),
                   .WindowStyle = ProcessWindowStyle.Hidden,
                   .CreateNoWindow = True,
                   .UseShellExecute = True
                }
                process = Process.Start(processStartInfo)
                process.WaitForExit()
                processStartInfo.FileName = "reg"
                processStartInfo.Arguments = String.Format("import ""{0}""", tempreg)
                process = Process.Start(processStartInfo)
                process.WaitForExit()
                Kill(tempreg)
            End If

            CP.EditReg("HKEY_CURRENT_USER\Console\" & RegKey, "FontFamily", If(ExtTerminal_RasterToggle.Checked, 0, 54))
            CP.EditReg("HKEY_CURRENT_USER\Console\" & RegKey, "FontSize", ExtTerminal_FontSizeBar.Value * 65536)
            CP.EditReg("HKEY_CURRENT_USER\Console\" & RegKey, "FontWeight", ExtTerminal_FontWeightBox.SelectedIndex * 100)

            MsgBox("Terminal Preferences are set successfully!", MsgBoxStyle.Information + My.Application.MsgboxRt)
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

        UpdateFromTrack(13)
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

        UpdateFromTrack(14)
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

        UpdateFromTrack(15) : UpdateFromTrack(16)
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

        UpdateFromTrack(15) : UpdateFromTrack(16)
        ApplyPreview()
    End Sub

    Private Sub ExtTerminal_RasterToggle_CheckedChanged(sender As Object, e As EventArgs) Handles ExtTerminal_RasterToggle.CheckedChanged
        If _Shown Then

            If ExtTerminal_RasterToggle.Enabled Then
                XenonCMD4.Font = New Font(f_extterminal.Name, 12, f_extterminal.Style)
            Else
                XenonCMD4.Font = f_extterminal
            End If

            ApplyPreview()
        End If
    End Sub

    Private Sub ExtTerminal_FontWeightBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ExtTerminal_FontWeightBox.SelectedIndexChanged
        If Not _Shown Then Exit Sub
        Dim fx As New LOGFONT
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
    Function BizareColorInvertor([Color] As Color) As Color
        Return Color.FromArgb([Color].B, [Color].G, [Color].R)
    End Function
#End Region

    Private Sub ColorTable00_Click(sender As Object, e As EventArgs) Handles ExtTerminal_ColorTable15.Click, ExtTerminal_ColorTable14.Click, ExtTerminal_ColorTable13.Click, ExtTerminal_ColorTable12.Click, ExtTerminal_ColorTable11.Click, ExtTerminal_ColorTable10.Click, ExtTerminal_ColorTable09.Click, ExtTerminal_ColorTable08.Click, ExtTerminal_ColorTable07.Click, ExtTerminal_ColorTable06.Click, ExtTerminal_ColorTable05.Click, ExtTerminal_ColorTable04.Click, ExtTerminal_ColorTable03.Click, ExtTerminal_ColorTable02.Click, ExtTerminal_ColorTable01.Click

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

            Dim FC0 As Color = If(IsColorDark(ExtTerminal_PopupForegroundLbl.BackColor), ControlPaint.LightLight(ExtTerminal_PopupForegroundLbl.BackColor), ControlPaint.Dark(ExtTerminal_PopupForegroundLbl.BackColor, 0.9))
            ExtTerminal_PopupForegroundLbl.ForeColor = FC0

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

            Dim FC0 As Color = If(IsColorDark(ExtTerminal_PopupBackgroundLbl.BackColor), ControlPaint.LightLight(ExtTerminal_PopupBackgroundLbl.BackColor), ControlPaint.Dark(ExtTerminal_PopupBackgroundLbl.BackColor, 0.9))
            ExtTerminal_PopupBackgroundLbl.ForeColor = FC0

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

            Dim FC0 As Color = If(IsColorDark(ExtTerminal_AccentBackgroundLbl.BackColor), ControlPaint.LightLight(ExtTerminal_AccentBackgroundLbl.BackColor), ControlPaint.Dark(ExtTerminal_AccentBackgroundLbl.BackColor, 0.9))
            ExtTerminal_AccentBackgroundLbl.ForeColor = FC0

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

            Dim FC0 As Color = If(IsColorDark(ExtTerminal_AccentForegroundLbl.BackColor), ControlPaint.LightLight(ExtTerminal_AccentForegroundLbl.BackColor), ControlPaint.Dark(ExtTerminal_AccentForegroundLbl.BackColor, 0.9))
            ExtTerminal_AccentForegroundLbl.ForeColor = FC0
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
End Class