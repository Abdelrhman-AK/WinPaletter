Imports System.Runtime.CompilerServices
Imports System.Security.Principal
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Status
Imports Microsoft.Win32
Imports WinPaletter.XenonCore

Public Class cmd
    Dim f_cmd As Font = New Font("Consolas", 18, FontStyle.Regular)
    Dim f_ps_32 As Font = New Font("Consolas", 18, FontStyle.Regular)
    Dim f_ps_64 As Font = New Font("Consolas", 18, FontStyle.Regular)
    Dim f_extterminal As Font = New Font("Consolas", 18, FontStyle.Regular)

    Private _Shown As Boolean = False


#Region "   Subs not related to colors and shapes"
    Private Sub cmd_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _Shown = False

        FillFonts(CMD_FontsBox, True)
        FillFonts(PS_32_FontsBox, True)
        FillFonts(PS_64_FontsBox, True)
        FillFonts(ExtTerminal_FontsBox, True)

        FillTerminals(XenonComboBox1)

        ApplyDarkMode(Me)
        ApplyFromCP(MainFrm.CP)
        ApplyPreview()
        MainFrm.Visible = False
        CMD_PopupForegroundLbl.Font = My.Application.ConsoleFont
        CMD_PopupBackgroundLbl.Font = My.Application.ConsoleFont
        CMD_AccentForegroundLbl.Font = My.Application.ConsoleFont
        CMD_AccentBackgroundLbl.Font = My.Application.ConsoleFont

        XenonAlertBox1.Image = My.Resources.notify_warning
        XenonAlertBox1.Visible = Not IO.Directory.Exists(Environment.GetEnvironmentVariable("WINDIR") & "\SysWOW64\WindowsPowerShell\v1.0")
        XenonAlertBox2.Image = My.Resources.notify_warning
        XenonAlertBox2.Visible = Not IO.Directory.Exists(Environment.GetEnvironmentVariable("WINDIR") & "\System32\WindowsPowerShell\v1.0")

        Location = New Point(10, (My.Computer.Screen.Bounds.Height - Height) / 2 - 20)
    End Sub
    Private Sub XenonCheckBox1_CheckedChanged(sender As Object) Handles XenonCheckBox1.CheckedChanged
        If _Shown Then
            FillFonts(CMD_FontsBox, Not XenonCheckBox1.Checked)
            FillFonts(PS_32_FontsBox, Not XenonCheckBox1.Checked)
            FillFonts(PS_64_FontsBox, Not XenonCheckBox1.Checked)
            FillFonts(ExtTerminal_FontsBox, Not XenonCheckBox1.Checked)
        End If
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
    Private Sub XenonButton10_Click(sender As Object, e As EventArgs) Handles XenonButton10.Click
        Dim CPx As New CP(CP.Mode.Registry)
        ApplyToCP(CPx)
        ApplyToCP(MainFrm.CP)
        CPx.Save(CP.SavingMode.Registry)
    End Sub
    Private Sub XenonButton1_Click(sender As Object, e As EventArgs) Handles XenonButton1.Click
        ApplyToCP(MainFrm.CP)
        Me.Close()
    End Sub
    Private Sub XenonButton2_Click(sender As Object, e As EventArgs) Handles XenonButton2.Click
        Me.Close()
    End Sub
    Private Sub cmd_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        MainFrm.Visible = True
    End Sub
    Private Sub cmd_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        _Shown = True
    End Sub
#End Region

#Region "   External Terminals"
    Function BizareColorInvertor([Color] As Color) As Color
        Return Color.FromArgb([Color].B, [Color].G, [Color].R)
    End Function
    Private Sub XenonButton7_Click(sender As Object, e As EventArgs) Handles XenonButton7.Click
        GetFromExtTerminal(XenonComboBox1.SelectedItem)
    End Sub

    Private Sub XenonButton8_Click(sender As Object, e As EventArgs) Handles XenonButton8.Click
        SetToExtTerminal(XenonComboBox1.SelectedItem)
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

            If Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "ReleaseId", "").ToString() >= 1909 Then
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

    Private Sub XenonButton6_Click(sender As Object, e As EventArgs) Handles XenonButton6.Click
        FillTerminals(XenonComboBox1)
    End Sub
#End Region

#Region "   Subs to modify colors or shapes"
    Public Sub ApplyCursorShape(Config As Integer)
        If Config = 1 Then
            CMD_PreviewCursorInner.Dock = DockStyle.Fill
            CMD_PreviewCUR2.Padding = New Padding(1, 1, 1, 1)

            If CMD_CursorStyle.SelectedIndex = 0 Then
                CMD_PreviewCursorInner.BackColor = Color.Transparent
                CMD_PreviewCUR2.Width = 8

                Dim all As Integer = CMD_PreviewCUR.Height - 4
                CMD_PreviewCUR2.Height = all * (CMD_CursorSizeBar.Value / CMD_CursorSizeBar.Maximum)
                CMD_PreviewCUR2.Top = CMD_PreviewCUR.Height - CMD_PreviewCUR2.Height - 2
            End If

            If CMD_CursorStyle.SelectedIndex = 1 Then
                CMD_PreviewCursorInner.BackColor = Color.Transparent

                CMD_PreviewCUR2.Width = 1

                Dim all As Integer = CMD_PreviewCUR.Height - 4
                CMD_PreviewCUR2.Height = all
                CMD_PreviewCUR2.Top = CMD_PreviewCUR.Height - CMD_PreviewCUR2.Height - 2
            End If

            If CMD_CursorStyle.SelectedIndex = 2 Then
                CMD_PreviewCursorInner.BackColor = Color.Transparent

                CMD_PreviewCUR2.Width = 10
                CMD_PreviewCUR2.Height = 1

                CMD_PreviewCUR2.Top = CMD_PreviewCUR.Height - CMD_PreviewCUR2.Height - 2
            End If

            If CMD_CursorStyle.SelectedIndex = 3 Then
                CMD_PreviewCursorInner.BackColor = CMD_PreviewCUR.BackColor

                CMD_PreviewCUR2.Width = 8

                Dim all As Integer = CMD_PreviewCUR.Height - 4
                CMD_PreviewCUR2.Height = all
                CMD_PreviewCUR2.Top = CMD_PreviewCUR.Height - CMD_PreviewCUR2.Height - 2
            End If

            If CMD_CursorStyle.SelectedIndex = 4 Then
                CMD_PreviewCursorInner.BackColor = Color.Transparent

                CMD_PreviewCUR2.Width = 8

                Dim all As Integer = CMD_PreviewCUR.Height - 4
                CMD_PreviewCUR2.Height = all
                CMD_PreviewCUR2.Top = CMD_PreviewCUR.Height - CMD_PreviewCUR2.Height - 2
            End If

            If CMD_CursorStyle.SelectedIndex = 5 Then
                CMD_PreviewCursorInner.Dock = DockStyle.None
                CMD_PreviewCUR2.Padding = New Padding(0, 0, 0, 0)
                CMD_PreviewCursorInner.Width = CMD_PreviewCUR2.Width
                CMD_PreviewCursorInner.Height = 1
                CMD_PreviewCursorInner.BackColor = CMD_PreviewCUR.BackColor
                CMD_PreviewCursorInner.Top = 1
                CMD_PreviewCursorInner.Left = 0
                CMD_PreviewCUR2.Width = 8
                CMD_PreviewCUR2.Height = 3
                CMD_PreviewCUR2.Top = CMD_PreviewCUR.Height - CMD_PreviewCUR2.Height - 2
            End If

        ElseIf Config = 2 Then
            PS_32_PreviewCursorInner.Dock = DockStyle.Fill
            PS_32_PreviewCUR2.Padding = New Padding(1, 1, 1, 1)

            If PS_32_CursorStyle.SelectedIndex = 0 Then
                PS_32_PreviewCursorInner.BackColor = Color.Transparent
                PS_32_PreviewCUR2.Width = 8

                Dim all As Integer = PS_32_PreviewCUR.Height - 4
                PS_32_PreviewCUR2.Height = all * (PS_32_CursorSizeBar.Value / PS_32_CursorSizeBar.Maximum)
                PS_32_PreviewCUR2.Top = PS_32_PreviewCUR.Height - PS_32_PreviewCUR2.Height - 2
            End If

            If PS_32_CursorStyle.SelectedIndex = 1 Then
                PS_32_PreviewCursorInner.BackColor = Color.Transparent

                PS_32_PreviewCUR2.Width = 1

                Dim all As Integer = PS_32_PreviewCUR.Height - 4
                PS_32_PreviewCUR2.Height = all
                PS_32_PreviewCUR2.Top = PS_32_PreviewCUR.Height - PS_32_PreviewCUR2.Height - 2
            End If

            If PS_32_CursorStyle.SelectedIndex = 2 Then
                PS_32_PreviewCursorInner.BackColor = Color.Transparent

                PS_32_PreviewCUR2.Width = 10
                PS_32_PreviewCUR2.Height = 1

                PS_32_PreviewCUR2.Top = PS_32_PreviewCUR.Height - PS_32_PreviewCUR2.Height - 2
            End If

            If PS_32_CursorStyle.SelectedIndex = 3 Then
                PS_32_PreviewCursorInner.BackColor = PS_32_PreviewCUR.BackColor

                PS_32_PreviewCUR2.Width = 8

                Dim all As Integer = PS_32_PreviewCUR.Height - 4
                PS_32_PreviewCUR2.Height = all
                PS_32_PreviewCUR2.Top = PS_32_PreviewCUR.Height - PS_32_PreviewCUR2.Height - 2
            End If

            If PS_32_CursorStyle.SelectedIndex = 4 Then
                PS_32_PreviewCursorInner.BackColor = Color.Transparent

                PS_32_PreviewCUR2.Width = 8

                Dim all As Integer = PS_32_PreviewCUR.Height - 4
                PS_32_PreviewCUR2.Height = all
                PS_32_PreviewCUR2.Top = PS_32_PreviewCUR.Height - PS_32_PreviewCUR2.Height - 2
            End If

            If PS_32_CursorStyle.SelectedIndex = 5 Then
                PS_32_PreviewCursorInner.Dock = DockStyle.None
                PS_32_PreviewCUR2.Padding = New Padding(0, 0, 0, 0)
                PS_32_PreviewCursorInner.Width = PS_32_PreviewCUR2.Width
                PS_32_PreviewCursorInner.Height = 1
                PS_32_PreviewCursorInner.BackColor = PS_32_PreviewCUR.BackColor
                PS_32_PreviewCursorInner.Top = 1
                PS_32_PreviewCursorInner.Left = 0
                PS_32_PreviewCUR2.Width = 8
                PS_32_PreviewCUR2.Height = 3
                PS_32_PreviewCUR2.Top = PS_32_PreviewCUR.Height - PS_32_PreviewCUR2.Height - 2
            End If

        ElseIf Config = 3 Then
            PS_64_PreviewCursorInner.Dock = DockStyle.Fill
            PS_64_PreviewCUR2.Padding = New Padding(1, 1, 1, 1)

            If PS_64_CursorStyle.SelectedIndex = 0 Then
                PS_64_PreviewCursorInner.BackColor = Color.Transparent
                PS_64_PreviewCUR2.Width = 8

                Dim all As Integer = PS_64_PreviewCUR.Height - 4
                PS_64_PreviewCUR2.Height = all * (PS_64_CursorSizeBar.Value / PS_64_CursorSizeBar.Maximum)
                PS_64_PreviewCUR2.Top = PS_64_PreviewCUR.Height - PS_64_PreviewCUR2.Height - 2
            End If

            If PS_64_CursorStyle.SelectedIndex = 1 Then
                PS_64_PreviewCursorInner.BackColor = Color.Transparent

                PS_64_PreviewCUR2.Width = 1

                Dim all As Integer = PS_64_PreviewCUR.Height - 4
                PS_64_PreviewCUR2.Height = all
                PS_64_PreviewCUR2.Top = PS_64_PreviewCUR.Height - PS_64_PreviewCUR2.Height - 2
            End If

            If PS_64_CursorStyle.SelectedIndex = 2 Then
                PS_64_PreviewCursorInner.BackColor = Color.Transparent

                PS_64_PreviewCUR2.Width = 10
                PS_64_PreviewCUR2.Height = 1

                PS_64_PreviewCUR2.Top = PS_64_PreviewCUR.Height - PS_64_PreviewCUR2.Height - 2
            End If

            If PS_64_CursorStyle.SelectedIndex = 3 Then
                PS_64_PreviewCursorInner.BackColor = PS_64_PreviewCUR.BackColor

                PS_64_PreviewCUR2.Width = 8

                Dim all As Integer = PS_64_PreviewCUR.Height - 4
                PS_64_PreviewCUR2.Height = all
                PS_64_PreviewCUR2.Top = PS_64_PreviewCUR.Height - PS_64_PreviewCUR2.Height - 2
            End If

            If PS_64_CursorStyle.SelectedIndex = 4 Then
                PS_64_PreviewCursorInner.BackColor = Color.Transparent

                PS_64_PreviewCUR2.Width = 8

                Dim all As Integer = PS_64_PreviewCUR.Height - 4
                PS_64_PreviewCUR2.Height = all
                PS_64_PreviewCUR2.Top = PS_64_PreviewCUR.Height - PS_64_PreviewCUR2.Height - 2
            End If

            If PS_64_CursorStyle.SelectedIndex = 5 Then
                PS_64_PreviewCursorInner.Dock = DockStyle.None
                PS_64_PreviewCUR2.Padding = New Padding(0, 0, 0, 0)
                PS_64_PreviewCursorInner.Width = PS_64_PreviewCUR2.Width
                PS_64_PreviewCursorInner.Height = 1
                PS_64_PreviewCursorInner.BackColor = PS_64_PreviewCUR.BackColor
                PS_64_PreviewCursorInner.Top = 1
                PS_64_PreviewCursorInner.Left = 0
                PS_64_PreviewCUR2.Width = 8
                PS_64_PreviewCUR2.Height = 3
                PS_64_PreviewCUR2.Top = PS_64_PreviewCUR.Height - PS_64_PreviewCUR2.Height - 2
            End If

        ElseIf Config = 4 Then
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
        End If
    End Sub
    Sub UpdateCurPreview(Config As Integer)
        If Config = 1 Then
            Dim all As Integer = CMD_PreviewCUR.Height - 4
            CMD_PreviewCUR2.Height = all * (CMD_CursorSizeBar.Value / CMD_CursorSizeBar.Maximum)
            CMD_PreviewCUR2.Top = CMD_PreviewCUR.Height - CMD_PreviewCUR2.Height - 2
            CMD_PreviewCUR_LBL.Text = CMD_CursorSizeBar.Value
            If Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "ReleaseId", "").ToString() >= 1909 Then ApplyCursorShape(Config)

        ElseIf Config = 2 Then
            Dim all As Integer = PS_32_PreviewCUR.Height - 4
            PS_32_PreviewCUR2.Height = all * (PS_32_CursorSizeBar.Value / PS_32_CursorSizeBar.Maximum)
            PS_32_PreviewCUR2.Top = PS_32_PreviewCUR.Height - PS_32_PreviewCUR2.Height - 2
            PS_32_PreviewCUR_LBL.Text = PS_32_CursorSizeBar.Value
            If Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "ReleaseId", "").ToString() >= 1909 Then ApplyCursorShape(Config)

        ElseIf Config = 3 Then
            Dim all As Integer = PS_64_PreviewCUR.Height - 4
            PS_64_PreviewCUR2.Height = all * (PS_64_CursorSizeBar.Value / PS_64_CursorSizeBar.Maximum)
            PS_64_PreviewCUR2.Top = PS_64_PreviewCUR.Height - PS_64_PreviewCUR2.Height - 2
            PS_64_PreviewCUR_LBL.Text = PS_64_CursorSizeBar.Value
            If Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "ReleaseId", "").ToString() >= 1909 Then ApplyCursorShape(Config)

        ElseIf Config = 4 Then
            Dim all As Integer = ExtTerminal_PreviewCUR.Height - 4
            ExtTerminal_PreviewCUR2.Height = all * (ExtTerminal_CursorSizeBar.Value / ExtTerminal_CursorSizeBar.Maximum)
            ExtTerminal_PreviewCUR2.Top = ExtTerminal_PreviewCUR.Height - ExtTerminal_PreviewCUR2.Height - 2
            ExtTerminal_PreviewCUR_LBL.Text = ExtTerminal_CursorSizeBar.Value
            If Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "ReleaseId", "").ToString() >= 1909 Then ApplyCursorShape(Config)
        End If
    End Sub
    Sub ApplyPreview()
#Region " Command Prompt"
        XenonCMD1.CMD_ColorTable00 = ColorTable00.BackColor
        XenonCMD1.CMD_ColorTable01 = ColorTable01.BackColor
        XenonCMD1.CMD_ColorTable02 = ColorTable02.BackColor
        XenonCMD1.CMD_ColorTable03 = ColorTable03.BackColor
        XenonCMD1.CMD_ColorTable04 = ColorTable04.BackColor
        XenonCMD1.CMD_ColorTable05 = ColorTable05.BackColor
        XenonCMD1.CMD_ColorTable06 = ColorTable06.BackColor
        XenonCMD1.CMD_ColorTable07 = ColorTable07.BackColor
        XenonCMD1.CMD_ColorTable08 = ColorTable08.BackColor
        XenonCMD1.CMD_ColorTable09 = ColorTable09.BackColor
        XenonCMD1.CMD_ColorTable10 = ColorTable10.BackColor
        XenonCMD1.CMD_ColorTable11 = ColorTable11.BackColor
        XenonCMD1.CMD_ColorTable12 = ColorTable12.BackColor
        XenonCMD1.CMD_ColorTable13 = ColorTable13.BackColor
        XenonCMD1.CMD_ColorTable14 = ColorTable14.BackColor
        XenonCMD1.CMD_ColorTable15 = ColorTable15.BackColor
        XenonCMD1.CMD_PopupForeground = CMD_PopupForegroundBar.Value
        XenonCMD1.CMD_PopupBackground = CMD_PopupBackgroundBar.Value
        XenonCMD1.CMD_ScreenColorsForeground = CMD_AccentForegroundBar.Value
        XenonCMD1.CMD_ScreenColorsBackground = CMD_AccentBackgroundBar.Value
        XenonCMD1.Font = New Font(f_cmd.Name, f_cmd.Size, f_cmd.Style)
        XenonCMD1.Raster = CMD_RasterToggle.Checked
#End Region

#Region " PowerShell 32-bit"
        XenonCMD2.CMD_ColorTable00 = PS_32_ColorTable00.BackColor
        XenonCMD2.CMD_ColorTable01 = PS_32_ColorTable01.BackColor
        XenonCMD2.CMD_ColorTable02 = PS_32_ColorTable02.BackColor
        XenonCMD2.CMD_ColorTable03 = PS_32_ColorTable03.BackColor
        XenonCMD2.CMD_ColorTable04 = PS_32_ColorTable04.BackColor
        XenonCMD2.CMD_ColorTable05 = PS_32_ColorTable05.BackColor
        XenonCMD2.CMD_ColorTable06 = PS_32_ColorTable06.BackColor
        XenonCMD2.CMD_ColorTable07 = PS_32_ColorTable07.BackColor
        XenonCMD2.CMD_ColorTable08 = PS_32_ColorTable08.BackColor
        XenonCMD2.CMD_ColorTable09 = PS_32_ColorTable09.BackColor
        XenonCMD2.CMD_ColorTable10 = PS_32_ColorTable10.BackColor
        XenonCMD2.CMD_ColorTable11 = PS_32_ColorTable11.BackColor
        XenonCMD2.CMD_ColorTable12 = PS_32_ColorTable12.BackColor
        XenonCMD2.CMD_ColorTable13 = PS_32_ColorTable13.BackColor
        XenonCMD2.CMD_ColorTable14 = PS_32_ColorTable14.BackColor
        XenonCMD2.CMD_ColorTable15 = PS_32_ColorTable15.BackColor
        XenonCMD2.CMD_PopupForeground = PS_32_PopupForegroundBar.Value
        XenonCMD2.CMD_PopupBackground = PS_32_PopupBackgroundBar.Value
        XenonCMD2.CMD_ScreenColorsForeground = PS_32_AccentForegroundBar.Value
        XenonCMD2.CMD_ScreenColorsBackground = PS_32_AccentBackgroundBar.Value
        XenonCMD2.Font = New Font(f_ps_32.Name, f_ps_32.Size, f_ps_32.Style)
        XenonCMD2.Raster = PS_32_RasterToggle.Checked
#End Region

#Region " PowerShell 64-bit"
        XenonCMD3.CMD_ColorTable00 = PS_64_ColorTable00.BackColor
        XenonCMD3.CMD_ColorTable01 = PS_64_ColorTable01.BackColor
        XenonCMD3.CMD_ColorTable02 = PS_64_ColorTable02.BackColor
        XenonCMD3.CMD_ColorTable03 = PS_64_ColorTable03.BackColor
        XenonCMD3.CMD_ColorTable04 = PS_64_ColorTable04.BackColor
        XenonCMD3.CMD_ColorTable05 = PS_64_ColorTable05.BackColor
        XenonCMD3.CMD_ColorTable06 = PS_64_ColorTable06.BackColor
        XenonCMD3.CMD_ColorTable07 = PS_64_ColorTable07.BackColor
        XenonCMD3.CMD_ColorTable08 = PS_64_ColorTable08.BackColor
        XenonCMD3.CMD_ColorTable09 = PS_64_ColorTable09.BackColor
        XenonCMD3.CMD_ColorTable10 = PS_64_ColorTable10.BackColor
        XenonCMD3.CMD_ColorTable11 = PS_64_ColorTable11.BackColor
        XenonCMD3.CMD_ColorTable12 = PS_64_ColorTable12.BackColor
        XenonCMD3.CMD_ColorTable13 = PS_64_ColorTable13.BackColor
        XenonCMD3.CMD_ColorTable14 = PS_64_ColorTable14.BackColor
        XenonCMD3.CMD_ColorTable15 = PS_64_ColorTable15.BackColor
        XenonCMD3.CMD_PopupForeground = PS_64_PopupForegroundBar.Value
        XenonCMD3.CMD_PopupBackground = PS_64_PopupBackgroundBar.Value
        XenonCMD3.CMD_ScreenColorsForeground = PS_64_AccentForegroundBar.Value
        XenonCMD3.CMD_ScreenColorsBackground = PS_64_AccentBackgroundBar.Value
        XenonCMD3.Font = New Font(f_ps_64.Name, f_ps_64.Size, f_ps_64.Style)
        XenonCMD3.Raster = PS_64_RasterToggle.Checked
#End Region

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

        XenonCMD1.Refresh()
        XenonCMD2.Refresh()
        XenonCMD3.Refresh()
        XenonCMD4.Refresh()
    End Sub
    Sub UpdateFromTrack(i As Integer)

        If i = 1 Then
            Select Case CMD_PopupForegroundBar.Value
                Case 0
                    CMD_PopupForegroundLbl.BackColor = ColorTable00.BackColor
                Case 1
                    CMD_PopupForegroundLbl.BackColor = ColorTable01.BackColor
                Case 2
                    CMD_PopupForegroundLbl.BackColor = ColorTable02.BackColor
                Case 3
                    CMD_PopupForegroundLbl.BackColor = ColorTable03.BackColor
                Case 4
                    CMD_PopupForegroundLbl.BackColor = ColorTable04.BackColor
                Case 5
                    CMD_PopupForegroundLbl.BackColor = ColorTable05.BackColor
                Case 6
                    CMD_PopupForegroundLbl.BackColor = ColorTable06.BackColor
                Case 7
                    CMD_PopupForegroundLbl.BackColor = ColorTable07.BackColor
                Case 8
                    CMD_PopupForegroundLbl.BackColor = ColorTable08.BackColor
                Case 9
                    CMD_PopupForegroundLbl.BackColor = ColorTable09.BackColor
                Case 10
                    CMD_PopupForegroundLbl.BackColor = ColorTable10.BackColor
                Case 11
                    CMD_PopupForegroundLbl.BackColor = ColorTable11.BackColor
                Case 12
                    CMD_PopupForegroundLbl.BackColor = ColorTable12.BackColor
                Case 13
                    CMD_PopupForegroundLbl.BackColor = ColorTable13.BackColor
                Case 14
                    CMD_PopupForegroundLbl.BackColor = ColorTable14.BackColor
                Case 15
                    CMD_PopupForegroundLbl.BackColor = ColorTable15.BackColor
            End Select

            Dim FC0 As Color = If(IsColorDark(CMD_PopupForegroundLbl.BackColor), ControlPaint.LightLight(CMD_PopupForegroundLbl.BackColor), ControlPaint.Dark(CMD_PopupForegroundLbl.BackColor, 0.9))
            CMD_PopupForegroundLbl.ForeColor = FC0

        ElseIf i = 2 Then

            Select Case CMD_PopupBackgroundBar.Value
                Case 0
                    CMD_PopupBackgroundLbl.BackColor = ColorTable00.BackColor
                Case 1
                    CMD_PopupBackgroundLbl.BackColor = ColorTable01.BackColor
                Case 2
                    CMD_PopupBackgroundLbl.BackColor = ColorTable02.BackColor
                Case 3
                    CMD_PopupBackgroundLbl.BackColor = ColorTable03.BackColor
                Case 4
                    CMD_PopupBackgroundLbl.BackColor = ColorTable04.BackColor
                Case 5
                    CMD_PopupBackgroundLbl.BackColor = ColorTable05.BackColor
                Case 6
                    CMD_PopupBackgroundLbl.BackColor = ColorTable06.BackColor
                Case 7
                    CMD_PopupBackgroundLbl.BackColor = ColorTable07.BackColor
                Case 8
                    CMD_PopupBackgroundLbl.BackColor = ColorTable08.BackColor
                Case 9
                    CMD_PopupBackgroundLbl.BackColor = ColorTable09.BackColor
                Case 10
                    CMD_PopupBackgroundLbl.BackColor = ColorTable10.BackColor
                Case 11
                    CMD_PopupBackgroundLbl.BackColor = ColorTable11.BackColor
                Case 12
                    CMD_PopupBackgroundLbl.BackColor = ColorTable12.BackColor
                Case 13
                    CMD_PopupBackgroundLbl.BackColor = ColorTable13.BackColor
                Case 14
                    CMD_PopupBackgroundLbl.BackColor = ColorTable14.BackColor
                Case 15
                    CMD_PopupBackgroundLbl.BackColor = ColorTable15.BackColor
            End Select

            Dim FC0 As Color = If(IsColorDark(CMD_PopupBackgroundLbl.BackColor), ControlPaint.LightLight(CMD_PopupBackgroundLbl.BackColor), ControlPaint.Dark(CMD_PopupBackgroundLbl.BackColor, 0.9))
            CMD_PopupBackgroundLbl.ForeColor = FC0

        ElseIf i = 3 Then

            Select Case CMD_AccentBackgroundBar.Value
                Case 0
                    CMD_AccentBackgroundLbl.BackColor = ColorTable00.BackColor
                Case 1
                    CMD_AccentBackgroundLbl.BackColor = ColorTable01.BackColor
                Case 2
                    CMD_AccentBackgroundLbl.BackColor = ColorTable02.BackColor
                Case 3
                    CMD_AccentBackgroundLbl.BackColor = ColorTable03.BackColor
                Case 4
                    CMD_AccentBackgroundLbl.BackColor = ColorTable04.BackColor
                Case 5
                    CMD_AccentBackgroundLbl.BackColor = ColorTable05.BackColor
                Case 6
                    CMD_AccentBackgroundLbl.BackColor = ColorTable06.BackColor
                Case 7
                    CMD_AccentBackgroundLbl.BackColor = ColorTable07.BackColor
                Case 8
                    CMD_AccentBackgroundLbl.BackColor = ColorTable08.BackColor
                Case 9
                    CMD_AccentBackgroundLbl.BackColor = ColorTable09.BackColor
                Case 10
                    CMD_AccentBackgroundLbl.BackColor = ColorTable10.BackColor
                Case 11
                    CMD_AccentBackgroundLbl.BackColor = ColorTable11.BackColor
                Case 12
                    CMD_AccentBackgroundLbl.BackColor = ColorTable12.BackColor
                Case 13
                    CMD_AccentBackgroundLbl.BackColor = ColorTable13.BackColor
                Case 14
                    CMD_AccentBackgroundLbl.BackColor = ColorTable14.BackColor
                Case 15
                    CMD_AccentBackgroundLbl.BackColor = ColorTable15.BackColor
            End Select

            Dim FC0 As Color = If(IsColorDark(CMD_AccentBackgroundLbl.BackColor), ControlPaint.LightLight(CMD_AccentBackgroundLbl.BackColor), ControlPaint.Dark(CMD_AccentBackgroundLbl.BackColor, 0.9))
            CMD_AccentBackgroundLbl.ForeColor = FC0

        ElseIf i = 4 Then

            Select Case CMD_AccentForegroundBar.Value
                Case 0
                    If CMD_AccentBackgroundBar.Value = CMD_AccentForegroundBar.Value Then
                        CMD_AccentForegroundLbl.BackColor = ColorTable07.BackColor
                    Else
                        CMD_AccentForegroundLbl.BackColor = ColorTable00.BackColor
                    End If
                Case 1
                    CMD_AccentForegroundLbl.BackColor = ColorTable01.BackColor
                Case 2
                    CMD_AccentForegroundLbl.BackColor = ColorTable02.BackColor
                Case 3
                    CMD_AccentForegroundLbl.BackColor = ColorTable03.BackColor
                Case 4
                    CMD_AccentForegroundLbl.BackColor = ColorTable04.BackColor
                Case 5
                    CMD_AccentForegroundLbl.BackColor = ColorTable05.BackColor
                Case 6
                    CMD_AccentForegroundLbl.BackColor = ColorTable06.BackColor
                Case 7
                    CMD_AccentForegroundLbl.BackColor = ColorTable07.BackColor
                Case 8
                    CMD_AccentForegroundLbl.BackColor = ColorTable08.BackColor
                Case 9
                    CMD_AccentForegroundLbl.BackColor = ColorTable09.BackColor
                Case 10
                    CMD_AccentForegroundLbl.BackColor = ColorTable10.BackColor
                Case 11
                    CMD_AccentForegroundLbl.BackColor = ColorTable11.BackColor
                Case 12
                    CMD_AccentForegroundLbl.BackColor = ColorTable12.BackColor
                Case 13
                    CMD_AccentForegroundLbl.BackColor = ColorTable13.BackColor
                Case 14
                    CMD_AccentForegroundLbl.BackColor = ColorTable14.BackColor
                Case 15
                    CMD_AccentForegroundLbl.BackColor = ColorTable15.BackColor
            End Select

            Dim FC0 As Color = If(IsColorDark(CMD_AccentForegroundLbl.BackColor), ControlPaint.LightLight(CMD_AccentForegroundLbl.BackColor), ControlPaint.Dark(CMD_AccentForegroundLbl.BackColor, 0.9))
            CMD_AccentForegroundLbl.ForeColor = FC0

        ElseIf i = 5 Then
            Select Case PS_32_PopupForegroundBar.Value
                Case 0
                    PS_32_PopupForegroundLbl.BackColor = PS_32_ColorTable00.BackColor
                Case 1
                    PS_32_PopupForegroundLbl.BackColor = PS_32_ColorTable01.BackColor
                Case 2
                    PS_32_PopupForegroundLbl.BackColor = PS_32_ColorTable02.BackColor
                Case 3
                    PS_32_PopupForegroundLbl.BackColor = PS_32_ColorTable03.BackColor
                Case 4
                    PS_32_PopupForegroundLbl.BackColor = PS_32_ColorTable04.BackColor
                Case 5
                    PS_32_PopupForegroundLbl.BackColor = PS_32_ColorTable05.BackColor
                Case 6
                    PS_32_PopupForegroundLbl.BackColor = PS_32_ColorTable06.BackColor
                Case 7
                    PS_32_PopupForegroundLbl.BackColor = PS_32_ColorTable07.BackColor
                Case 8
                    PS_32_PopupForegroundLbl.BackColor = PS_32_ColorTable08.BackColor
                Case 9
                    PS_32_PopupForegroundLbl.BackColor = PS_32_ColorTable09.BackColor
                Case 10
                    PS_32_PopupForegroundLbl.BackColor = PS_32_ColorTable10.BackColor
                Case 11
                    PS_32_PopupForegroundLbl.BackColor = PS_32_ColorTable11.BackColor
                Case 12
                    PS_32_PopupForegroundLbl.BackColor = PS_32_ColorTable12.BackColor
                Case 13
                    PS_32_PopupForegroundLbl.BackColor = PS_32_ColorTable13.BackColor
                Case 14
                    PS_32_PopupForegroundLbl.BackColor = PS_32_ColorTable14.BackColor
                Case 15
                    PS_32_PopupForegroundLbl.BackColor = PS_32_ColorTable15.BackColor
            End Select

            Dim FC0 As Color = If(IsColorDark(PS_32_PopupForegroundLbl.BackColor), ControlPaint.LightLight(PS_32_PopupForegroundLbl.BackColor), ControlPaint.Dark(PS_32_PopupForegroundLbl.BackColor, 0.9))
            PS_32_PopupForegroundLbl.ForeColor = FC0

        ElseIf i = 6 Then

            Select Case PS_32_PopupBackgroundBar.Value
                Case 0
                    PS_32_PopupBackgroundLbl.BackColor = PS_32_ColorTable00.BackColor
                Case 1
                    PS_32_PopupBackgroundLbl.BackColor = PS_32_ColorTable01.BackColor
                Case 2
                    PS_32_PopupBackgroundLbl.BackColor = PS_32_ColorTable02.BackColor
                Case 3
                    PS_32_PopupBackgroundLbl.BackColor = PS_32_ColorTable03.BackColor
                Case 4
                    PS_32_PopupBackgroundLbl.BackColor = PS_32_ColorTable04.BackColor
                Case 5
                    PS_32_PopupBackgroundLbl.BackColor = PS_32_ColorTable05.BackColor
                Case 6
                    PS_32_PopupBackgroundLbl.BackColor = PS_32_ColorTable06.BackColor
                Case 7
                    PS_32_PopupBackgroundLbl.BackColor = PS_32_ColorTable07.BackColor
                Case 8
                    PS_32_PopupBackgroundLbl.BackColor = PS_32_ColorTable08.BackColor
                Case 9
                    PS_32_PopupBackgroundLbl.BackColor = PS_32_ColorTable09.BackColor
                Case 10
                    PS_32_PopupBackgroundLbl.BackColor = PS_32_ColorTable10.BackColor
                Case 11
                    PS_32_PopupBackgroundLbl.BackColor = PS_32_ColorTable11.BackColor
                Case 12
                    PS_32_PopupBackgroundLbl.BackColor = PS_32_ColorTable12.BackColor
                Case 13
                    PS_32_PopupBackgroundLbl.BackColor = PS_32_ColorTable13.BackColor
                Case 14
                    PS_32_PopupBackgroundLbl.BackColor = PS_32_ColorTable14.BackColor
                Case 15
                    PS_32_PopupBackgroundLbl.BackColor = PS_32_ColorTable15.BackColor
            End Select

            Dim FC0 As Color = If(IsColorDark(PS_32_PopupBackgroundLbl.BackColor), ControlPaint.LightLight(PS_32_PopupBackgroundLbl.BackColor), ControlPaint.Dark(PS_32_PopupBackgroundLbl.BackColor, 0.9))
            PS_32_PopupBackgroundLbl.ForeColor = FC0

        ElseIf i = 7 Then

            Select Case PS_32_AccentBackgroundBar.Value
                Case 0
                    PS_32_AccentBackgroundLbl.BackColor = PS_32_ColorTable00.BackColor
                Case 1
                    PS_32_AccentBackgroundLbl.BackColor = PS_32_ColorTable01.BackColor
                Case 2
                    PS_32_AccentBackgroundLbl.BackColor = PS_32_ColorTable02.BackColor
                Case 3
                    PS_32_AccentBackgroundLbl.BackColor = PS_32_ColorTable03.BackColor
                Case 4
                    PS_32_AccentBackgroundLbl.BackColor = PS_32_ColorTable04.BackColor
                Case 5
                    PS_32_AccentBackgroundLbl.BackColor = PS_32_ColorTable05.BackColor
                Case 6
                    PS_32_AccentBackgroundLbl.BackColor = PS_32_ColorTable06.BackColor
                Case 7
                    PS_32_AccentBackgroundLbl.BackColor = PS_32_ColorTable07.BackColor
                Case 8
                    PS_32_AccentBackgroundLbl.BackColor = PS_32_ColorTable08.BackColor
                Case 9
                    PS_32_AccentBackgroundLbl.BackColor = PS_32_ColorTable09.BackColor
                Case 10
                    PS_32_AccentBackgroundLbl.BackColor = PS_32_ColorTable10.BackColor
                Case 11
                    PS_32_AccentBackgroundLbl.BackColor = PS_32_ColorTable11.BackColor
                Case 12
                    PS_32_AccentBackgroundLbl.BackColor = PS_32_ColorTable12.BackColor
                Case 13
                    PS_32_AccentBackgroundLbl.BackColor = PS_32_ColorTable13.BackColor
                Case 14
                    PS_32_AccentBackgroundLbl.BackColor = PS_32_ColorTable14.BackColor
                Case 15
                    PS_32_AccentBackgroundLbl.BackColor = PS_32_ColorTable15.BackColor
            End Select

            Dim FC0 As Color = If(IsColorDark(PS_32_AccentBackgroundLbl.BackColor), ControlPaint.LightLight(PS_32_AccentBackgroundLbl.BackColor), ControlPaint.Dark(PS_32_AccentBackgroundLbl.BackColor, 0.9))
            PS_32_AccentBackgroundLbl.ForeColor = FC0

        ElseIf i = 8 Then

            Select Case PS_32_AccentForegroundBar.Value
                Case 0
                    If PS_32_AccentBackgroundBar.Value = PS_32_AccentForegroundBar.Value Then
                        PS_32_AccentForegroundLbl.BackColor = PS_32_ColorTable07.BackColor
                    Else
                        PS_32_AccentForegroundLbl.BackColor = PS_32_ColorTable00.BackColor
                    End If
                Case 1
                    PS_32_AccentForegroundLbl.BackColor = PS_32_ColorTable01.BackColor
                Case 2
                    PS_32_AccentForegroundLbl.BackColor = PS_32_ColorTable02.BackColor
                Case 3
                    PS_32_AccentForegroundLbl.BackColor = PS_32_ColorTable03.BackColor
                Case 4
                    PS_32_AccentForegroundLbl.BackColor = PS_32_ColorTable04.BackColor
                Case 5
                    PS_32_AccentForegroundLbl.BackColor = PS_32_ColorTable05.BackColor
                Case 6
                    PS_32_AccentForegroundLbl.BackColor = PS_32_ColorTable06.BackColor
                Case 7
                    PS_32_AccentForegroundLbl.BackColor = PS_32_ColorTable07.BackColor
                Case 8
                    PS_32_AccentForegroundLbl.BackColor = PS_32_ColorTable08.BackColor
                Case 9
                    PS_32_AccentForegroundLbl.BackColor = PS_32_ColorTable09.BackColor
                Case 10
                    PS_32_AccentForegroundLbl.BackColor = PS_32_ColorTable10.BackColor
                Case 11
                    PS_32_AccentForegroundLbl.BackColor = PS_32_ColorTable11.BackColor
                Case 12
                    PS_32_AccentForegroundLbl.BackColor = PS_32_ColorTable12.BackColor
                Case 13
                    PS_32_AccentForegroundLbl.BackColor = PS_32_ColorTable13.BackColor
                Case 14
                    PS_32_AccentForegroundLbl.BackColor = PS_32_ColorTable14.BackColor
                Case 15
                    PS_32_AccentForegroundLbl.BackColor = PS_32_ColorTable15.BackColor
            End Select

            Dim FC0 As Color = If(IsColorDark(PS_32_AccentForegroundLbl.BackColor), ControlPaint.LightLight(PS_32_AccentForegroundLbl.BackColor), ControlPaint.Dark(PS_32_AccentForegroundLbl.BackColor, 0.9))
            PS_32_AccentForegroundLbl.ForeColor = FC0

        ElseIf i = 9 Then
            Select Case PS_64_PopupForegroundBar.Value
                Case 0
                    PS_64_PopupForegroundLbl.BackColor = PS_64_ColorTable00.BackColor
                Case 1
                    PS_64_PopupForegroundLbl.BackColor = PS_64_ColorTable01.BackColor
                Case 2
                    PS_64_PopupForegroundLbl.BackColor = PS_64_ColorTable02.BackColor
                Case 3
                    PS_64_PopupForegroundLbl.BackColor = PS_64_ColorTable03.BackColor
                Case 4
                    PS_64_PopupForegroundLbl.BackColor = PS_64_ColorTable04.BackColor
                Case 5
                    PS_64_PopupForegroundLbl.BackColor = PS_64_ColorTable05.BackColor
                Case 6
                    PS_64_PopupForegroundLbl.BackColor = PS_64_ColorTable06.BackColor
                Case 7
                    PS_64_PopupForegroundLbl.BackColor = PS_64_ColorTable07.BackColor
                Case 8
                    PS_64_PopupForegroundLbl.BackColor = PS_64_ColorTable08.BackColor
                Case 9
                    PS_64_PopupForegroundLbl.BackColor = PS_64_ColorTable09.BackColor
                Case 10
                    PS_64_PopupForegroundLbl.BackColor = PS_64_ColorTable10.BackColor
                Case 11
                    PS_64_PopupForegroundLbl.BackColor = PS_64_ColorTable11.BackColor
                Case 12
                    PS_64_PopupForegroundLbl.BackColor = PS_64_ColorTable12.BackColor
                Case 13
                    PS_64_PopupForegroundLbl.BackColor = PS_64_ColorTable13.BackColor
                Case 14
                    PS_64_PopupForegroundLbl.BackColor = PS_64_ColorTable14.BackColor
                Case 15
                    PS_64_PopupForegroundLbl.BackColor = PS_64_ColorTable15.BackColor
            End Select

            Dim FC0 As Color = If(IsColorDark(PS_64_PopupForegroundLbl.BackColor), ControlPaint.LightLight(PS_64_PopupForegroundLbl.BackColor), ControlPaint.Dark(PS_64_PopupForegroundLbl.BackColor, 0.9))
            PS_64_PopupForegroundLbl.ForeColor = FC0

        ElseIf i = 10 Then

            Select Case PS_64_PopupBackgroundBar.Value
                Case 0
                    PS_64_PopupBackgroundLbl.BackColor = PS_64_ColorTable00.BackColor
                Case 1
                    PS_64_PopupBackgroundLbl.BackColor = PS_64_ColorTable01.BackColor
                Case 2
                    PS_64_PopupBackgroundLbl.BackColor = PS_64_ColorTable02.BackColor
                Case 3
                    PS_64_PopupBackgroundLbl.BackColor = PS_64_ColorTable03.BackColor
                Case 4
                    PS_64_PopupBackgroundLbl.BackColor = PS_64_ColorTable04.BackColor
                Case 5
                    PS_64_PopupBackgroundLbl.BackColor = PS_64_ColorTable05.BackColor
                Case 6
                    PS_64_PopupBackgroundLbl.BackColor = PS_64_ColorTable06.BackColor
                Case 7
                    PS_64_PopupBackgroundLbl.BackColor = PS_64_ColorTable07.BackColor
                Case 8
                    PS_64_PopupBackgroundLbl.BackColor = PS_64_ColorTable08.BackColor
                Case 9
                    PS_64_PopupBackgroundLbl.BackColor = PS_64_ColorTable09.BackColor
                Case 10
                    PS_64_PopupBackgroundLbl.BackColor = PS_64_ColorTable10.BackColor
                Case 11
                    PS_64_PopupBackgroundLbl.BackColor = PS_64_ColorTable11.BackColor
                Case 12
                    PS_64_PopupBackgroundLbl.BackColor = PS_64_ColorTable12.BackColor
                Case 13
                    PS_64_PopupBackgroundLbl.BackColor = PS_64_ColorTable13.BackColor
                Case 14
                    PS_64_PopupBackgroundLbl.BackColor = PS_64_ColorTable14.BackColor
                Case 15
                    PS_64_PopupBackgroundLbl.BackColor = PS_64_ColorTable15.BackColor
            End Select

            Dim FC0 As Color = If(IsColorDark(PS_64_PopupBackgroundLbl.BackColor), ControlPaint.LightLight(PS_64_PopupBackgroundLbl.BackColor), ControlPaint.Dark(PS_64_PopupBackgroundLbl.BackColor, 0.9))
            PS_64_PopupBackgroundLbl.ForeColor = FC0

        ElseIf i = 11 Then

            Select Case PS_64_AccentBackgroundBar.Value
                Case 0
                    PS_64_AccentBackgroundLbl.BackColor = PS_64_ColorTable00.BackColor
                Case 1
                    PS_64_AccentBackgroundLbl.BackColor = PS_64_ColorTable01.BackColor
                Case 2
                    PS_64_AccentBackgroundLbl.BackColor = PS_64_ColorTable02.BackColor
                Case 3
                    PS_64_AccentBackgroundLbl.BackColor = PS_64_ColorTable03.BackColor
                Case 4
                    PS_64_AccentBackgroundLbl.BackColor = PS_64_ColorTable04.BackColor
                Case 5
                    PS_64_AccentBackgroundLbl.BackColor = PS_64_ColorTable05.BackColor
                Case 6
                    PS_64_AccentBackgroundLbl.BackColor = PS_64_ColorTable06.BackColor
                Case 7
                    PS_64_AccentBackgroundLbl.BackColor = PS_64_ColorTable07.BackColor
                Case 8
                    PS_64_AccentBackgroundLbl.BackColor = PS_64_ColorTable08.BackColor
                Case 9
                    PS_64_AccentBackgroundLbl.BackColor = PS_64_ColorTable09.BackColor
                Case 10
                    PS_64_AccentBackgroundLbl.BackColor = PS_64_ColorTable10.BackColor
                Case 11
                    PS_64_AccentBackgroundLbl.BackColor = PS_64_ColorTable11.BackColor
                Case 12
                    PS_64_AccentBackgroundLbl.BackColor = PS_64_ColorTable12.BackColor
                Case 13
                    PS_64_AccentBackgroundLbl.BackColor = PS_64_ColorTable13.BackColor
                Case 14
                    PS_64_AccentBackgroundLbl.BackColor = PS_64_ColorTable14.BackColor
                Case 15
                    PS_64_AccentBackgroundLbl.BackColor = PS_64_ColorTable15.BackColor
            End Select

            Dim FC0 As Color = If(IsColorDark(PS_64_AccentBackgroundLbl.BackColor), ControlPaint.LightLight(PS_64_AccentBackgroundLbl.BackColor), ControlPaint.Dark(PS_64_AccentBackgroundLbl.BackColor, 0.9))
            PS_64_AccentBackgroundLbl.ForeColor = FC0

        ElseIf i = 12 Then

            Select Case PS_64_AccentForegroundBar.Value
                Case 0
                    If PS_64_AccentBackgroundBar.Value = PS_64_AccentForegroundBar.Value Then
                        PS_64_AccentForegroundLbl.BackColor = PS_64_ColorTable07.BackColor
                    Else
                        PS_64_AccentForegroundLbl.BackColor = PS_64_ColorTable00.BackColor
                    End If
                Case 1
                    PS_64_AccentForegroundLbl.BackColor = PS_64_ColorTable01.BackColor
                Case 2
                    PS_64_AccentForegroundLbl.BackColor = PS_64_ColorTable02.BackColor
                Case 3
                    PS_64_AccentForegroundLbl.BackColor = PS_64_ColorTable03.BackColor
                Case 4
                    PS_64_AccentForegroundLbl.BackColor = PS_64_ColorTable04.BackColor
                Case 5
                    PS_64_AccentForegroundLbl.BackColor = PS_64_ColorTable05.BackColor
                Case 6
                    PS_64_AccentForegroundLbl.BackColor = PS_64_ColorTable06.BackColor
                Case 7
                    PS_64_AccentForegroundLbl.BackColor = PS_64_ColorTable07.BackColor
                Case 8
                    PS_64_AccentForegroundLbl.BackColor = PS_64_ColorTable08.BackColor
                Case 9
                    PS_64_AccentForegroundLbl.BackColor = PS_64_ColorTable09.BackColor
                Case 10
                    PS_64_AccentForegroundLbl.BackColor = PS_64_ColorTable10.BackColor
                Case 11
                    PS_64_AccentForegroundLbl.BackColor = PS_64_ColorTable11.BackColor
                Case 12
                    PS_64_AccentForegroundLbl.BackColor = PS_64_ColorTable12.BackColor
                Case 13
                    PS_64_AccentForegroundLbl.BackColor = PS_64_ColorTable13.BackColor
                Case 14
                    PS_64_AccentForegroundLbl.BackColor = PS_64_ColorTable14.BackColor
                Case 15
                    PS_64_AccentForegroundLbl.BackColor = PS_64_ColorTable15.BackColor
            End Select

            Dim FC0 As Color = If(IsColorDark(PS_64_AccentForegroundLbl.BackColor), ControlPaint.LightLight(PS_64_AccentForegroundLbl.BackColor), ControlPaint.Dark(PS_64_AccentForegroundLbl.BackColor, 0.9))
            PS_64_AccentForegroundLbl.ForeColor = FC0

        ElseIf i = 13 Then
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

        ElseIf i = 14 Then

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

        ElseIf i = 15 Then

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

        ElseIf i = 16 Then

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

#Region "   CP Handling"
    Sub ApplyFromCP([CP] As CP)
#Region " Command Prompt"
        ColorTable00.BackColor = CP.CMD_ColorTable00
        ColorTable01.BackColor = CP.CMD_ColorTable01
        ColorTable02.BackColor = CP.CMD_ColorTable02
        ColorTable03.BackColor = CP.CMD_ColorTable03
        ColorTable04.BackColor = CP.CMD_ColorTable04
        ColorTable05.BackColor = CP.CMD_ColorTable05
        ColorTable06.BackColor = CP.CMD_ColorTable06
        ColorTable07.BackColor = CP.CMD_ColorTable07
        ColorTable08.BackColor = CP.CMD_ColorTable08
        ColorTable09.BackColor = CP.CMD_ColorTable09
        ColorTable10.BackColor = CP.CMD_ColorTable10
        ColorTable11.BackColor = CP.CMD_ColorTable11
        ColorTable12.BackColor = CP.CMD_ColorTable12
        ColorTable13.BackColor = CP.CMD_ColorTable13
        ColorTable14.BackColor = CP.CMD_ColorTable14
        ColorTable15.BackColor = CP.CMD_ColorTable15
        CMD_PopupForegroundBar.Value = CP.CMD_PopupForeground
        CMD_PopupBackgroundBar.Value = CP.CMD_PopupBackground
        CMD_AccentForegroundBar.Value = CP.CMD_ScreenColorsForeground
        CMD_AccentBackgroundBar.Value = CP.CMD_ScreenColorsBackground
        CMD_RasterToggle.Checked = CP.CMD_FontRaster
        Select Case CP.CMD_FontWeight
            Case 0
                CMD_FontWeightBox.SelectedIndex = 0

            Case 100
                CMD_FontWeightBox.SelectedIndex = 1

            Case 200
                CMD_FontWeightBox.SelectedIndex = 2

            Case 300
                CMD_FontWeightBox.SelectedIndex = 3

            Case 400
                CMD_FontWeightBox.SelectedIndex = 4

            Case 500
                CMD_FontWeightBox.SelectedIndex = 5

            Case 600
                CMD_FontWeightBox.SelectedIndex = 6

            Case 700
                CMD_FontWeightBox.SelectedIndex = 7

            Case 800
                CMD_FontWeightBox.SelectedIndex = 8

            Case 900
                CMD_FontWeightBox.SelectedIndex = 9

            Case Else
                CMD_FontWeightBox.SelectedIndex = 4

        End Select
        With Font.FromLogFont(New LOGFONT With {.lfFaceName = CP.CMD_FaceName, .lfWeight = CP.CMD_FontWeight}) : f_cmd = New Font(.FontFamily, CInt(CP.CMD_FontSize / 65536), .Style) : End With
        CMD_FontsBox.SelectedItem = f_cmd.Name
        CMD_FontSizeBar.Value = f_cmd.Size
        CP.CMD_CursorSize = CMD_CursorSizeBar.Value
        If CMD_CursorSizeBar.Value > 100 Then CMD_CursorSizeBar.Value = 100
        If CMD_CursorSizeBar.Value < 20 Then CMD_CursorSizeBar.Value = 20
        If My.W10_1909 Then
            CMD_CursorStyle.SelectedIndex = CP.CMD_1909_CursorType
            CMD_CursorColor.BackColor = CP.CMD_1909_CursorColor
            CMD_PreviewCUR2.BackColor = CP.CMD_1909_CursorColor
            CMD_EnhancedTerminal.Checked = CP.CMD_1909_ForceV2
            CMD_OpacityBar.Value = CP.CMD_1909_WindowAlpha
            CMD_OpacityLbl.Text = Fix((CP.CMD_1909_WindowAlpha / 255) * 100)
            CMD_LineSelection.Checked = CP.CMD_1909_LineSelection
            CMD_TerminalScrolling.Checked = CP.CMD_1909_TerminalScrolling
            ApplyCursorShape(1)
        End If
        UpdateCurPreview(1)
#End Region

#Region " PowerShell 32-bit"
        PS_32_ColorTable00.BackColor = CP.PS_32_ColorTable00
        PS_32_ColorTable01.BackColor = CP.PS_32_ColorTable01
        PS_32_ColorTable02.BackColor = CP.PS_32_ColorTable02
        PS_32_ColorTable03.BackColor = CP.PS_32_ColorTable03
        PS_32_ColorTable04.BackColor = CP.PS_32_ColorTable04
        PS_32_ColorTable05.BackColor = CP.PS_32_ColorTable05
        PS_32_ColorTable06.BackColor = CP.PS_32_ColorTable06
        PS_32_ColorTable07.BackColor = CP.PS_32_ColorTable07
        PS_32_ColorTable08.BackColor = CP.PS_32_ColorTable08
        PS_32_ColorTable09.BackColor = CP.PS_32_ColorTable09
        PS_32_ColorTable10.BackColor = CP.PS_32_ColorTable10
        PS_32_ColorTable11.BackColor = CP.PS_32_ColorTable11
        PS_32_ColorTable12.BackColor = CP.PS_32_ColorTable12
        PS_32_ColorTable13.BackColor = CP.PS_32_ColorTable13
        PS_32_ColorTable14.BackColor = CP.PS_32_ColorTable14
        PS_32_ColorTable15.BackColor = CP.PS_32_ColorTable15
        PS_32_PopupForegroundBar.Value = CP.PS_32_PopupForeground
        PS_32_PopupBackgroundBar.Value = CP.PS_32_PopupBackground
        PS_32_AccentForegroundBar.Value = CP.PS_32_ScreenColorsForeground
        PS_32_AccentBackgroundBar.Value = CP.PS_32_ScreenColorsBackground
        PS_32_RasterToggle.Checked = CP.PS_32_FontRaster
        Select Case CP.PS_32_FontWeight
            Case 0
                PS_32_FontWeightBox.SelectedIndex = 0

            Case 100
                PS_32_FontWeightBox.SelectedIndex = 1

            Case 200
                PS_32_FontWeightBox.SelectedIndex = 2

            Case 300
                PS_32_FontWeightBox.SelectedIndex = 3

            Case 400
                PS_32_FontWeightBox.SelectedIndex = 4

            Case 500
                PS_32_FontWeightBox.SelectedIndex = 5

            Case 600
                PS_32_FontWeightBox.SelectedIndex = 6

            Case 700
                PS_32_FontWeightBox.SelectedIndex = 7

            Case 800
                PS_32_FontWeightBox.SelectedIndex = 8

            Case 900
                PS_32_FontWeightBox.SelectedIndex = 9

            Case Else
                PS_32_FontWeightBox.SelectedIndex = 4

        End Select
        With Font.FromLogFont(New LOGFONT With {.lfFaceName = CP.PS_32_FaceName, .lfWeight = CP.PS_32_FontWeight}) : f_ps_32 = New Font(.FontFamily, CInt(CP.PS_32_FontSize / 65536), .Style) : End With
        PS_32_FontsBox.SelectedItem = f_ps_32.Name
        PS_32_FontSizeBar.Value = f_ps_32.Size
        CP.PS_32_CursorSize = PS_32_CursorSizeBar.Value
        If PS_32_CursorSizeBar.Value > 100 Then PS_32_CursorSizeBar.Value = 100
        If PS_32_CursorSizeBar.Value < 20 Then PS_32_CursorSizeBar.Value = 20
        If My.W10_1909 Then
            PS_32_CursorStyle.SelectedIndex = CP.PS_32_1909_CursorType
            PS_32_CursorColor.BackColor = CP.PS_32_1909_CursorColor
            PS_32_PreviewCUR2.BackColor = CP.PS_32_1909_CursorColor
            PS_32_EnhancedTerminal.Checked = CP.PS_32_1909_ForceV2
            PS_32_OpacityBar.Value = CP.PS_32_1909_WindowAlpha
            PS_32_OpacityLbl.Text = Fix((CP.PS_32_1909_WindowAlpha / 255) * 100)
            PS_32_LineSelection.Checked = CP.PS_32_1909_LineSelection
            PS_32_TerminalScrolling.Checked = CP.PS_32_1909_TerminalScrolling
            ApplyCursorShape(2)
        End If
        UpdateCurPreview(2)
#End Region

#Region " PowerShell 64-bit"
        PS_64_ColorTable00.BackColor = CP.PS_64_ColorTable00
        PS_64_ColorTable01.BackColor = CP.PS_64_ColorTable01
        PS_64_ColorTable02.BackColor = CP.PS_64_ColorTable02
        PS_64_ColorTable03.BackColor = CP.PS_64_ColorTable03
        PS_64_ColorTable04.BackColor = CP.PS_64_ColorTable04
        PS_64_ColorTable05.BackColor = CP.PS_64_ColorTable05
        PS_64_ColorTable06.BackColor = CP.PS_64_ColorTable06
        PS_64_ColorTable07.BackColor = CP.PS_64_ColorTable07
        PS_64_ColorTable08.BackColor = CP.PS_64_ColorTable08
        PS_64_ColorTable09.BackColor = CP.PS_64_ColorTable09
        PS_64_ColorTable10.BackColor = CP.PS_64_ColorTable10
        PS_64_ColorTable11.BackColor = CP.PS_64_ColorTable11
        PS_64_ColorTable12.BackColor = CP.PS_64_ColorTable12
        PS_64_ColorTable13.BackColor = CP.PS_64_ColorTable13
        PS_64_ColorTable14.BackColor = CP.PS_64_ColorTable14
        PS_64_ColorTable15.BackColor = CP.PS_64_ColorTable15
        PS_64_PopupForegroundBar.Value = CP.PS_64_PopupForeground
        PS_64_PopupBackgroundBar.Value = CP.PS_64_PopupBackground
        PS_64_AccentForegroundBar.Value = CP.PS_64_ScreenColorsForeground
        PS_64_AccentBackgroundBar.Value = CP.PS_64_ScreenColorsBackground
        PS_64_RasterToggle.Checked = CP.PS_64_FontRaster
        Select Case CP.PS_64_FontWeight
            Case 0
                PS_64_FontWeightBox.SelectedIndex = 0

            Case 100
                PS_64_FontWeightBox.SelectedIndex = 1

            Case 200
                PS_64_FontWeightBox.SelectedIndex = 2

            Case 300
                PS_64_FontWeightBox.SelectedIndex = 3

            Case 400
                PS_64_FontWeightBox.SelectedIndex = 4

            Case 500
                PS_64_FontWeightBox.SelectedIndex = 5

            Case 600
                PS_64_FontWeightBox.SelectedIndex = 6

            Case 700
                PS_64_FontWeightBox.SelectedIndex = 7

            Case 800
                PS_64_FontWeightBox.SelectedIndex = 8

            Case 900
                PS_64_FontWeightBox.SelectedIndex = 9

            Case Else
                PS_64_FontWeightBox.SelectedIndex = 4

        End Select
        With Font.FromLogFont(New LOGFONT With {.lfFaceName = CP.PS_64_FaceName, .lfWeight = CP.PS_64_FontWeight}) : f_ps_64 = New Font(.FontFamily, CInt(CP.PS_64_FontSize / 65536), .Style) : End With
        PS_64_FontsBox.SelectedItem = f_ps_64.Name
        PS_64_FontSizeBar.Value = f_ps_64.Size
        CP.PS_64_CursorSize = PS_64_CursorSizeBar.Value
        If PS_64_CursorSizeBar.Value > 100 Then PS_64_CursorSizeBar.Value = 100
        If PS_64_CursorSizeBar.Value < 20 Then PS_64_CursorSizeBar.Value = 20
        If My.W10_1909 Then
            PS_64_CursorStyle.SelectedIndex = CP.PS_64_1909_CursorType
            PS_64_CursorColor.BackColor = CP.PS_64_1909_CursorColor
            PS_64_PreviewCUR2.BackColor = CP.PS_64_1909_CursorColor
            PS_64_EnhancedTerminal.Checked = CP.PS_64_1909_ForceV2
            PS_64_OpacityBar.Value = CP.PS_64_1909_WindowAlpha
            PS_64_OpacityLbl.Text = Fix((CP.PS_64_1909_WindowAlpha / 255) * 100)
            PS_64_LineSelection.Checked = CP.PS_64_1909_LineSelection
            PS_64_TerminalScrolling.Checked = CP.PS_64_1909_TerminalScrolling
            ApplyCursorShape(3)
        End If
        UpdateCurPreview(3)
#End Region

    End Sub

    Sub ApplyToCP([CP] As CP)
#Region " Command Prompt"
        CP.CMD_ColorTable00 = ColorTable00.BackColor
        CP.CMD_ColorTable01 = ColorTable01.BackColor
        CP.CMD_ColorTable02 = ColorTable02.BackColor
        CP.CMD_ColorTable03 = ColorTable03.BackColor
        CP.CMD_ColorTable04 = ColorTable04.BackColor
        CP.CMD_ColorTable05 = ColorTable05.BackColor
        CP.CMD_ColorTable06 = ColorTable06.BackColor
        CP.CMD_ColorTable07 = ColorTable07.BackColor
        CP.CMD_ColorTable08 = ColorTable08.BackColor
        CP.CMD_ColorTable09 = ColorTable09.BackColor
        CP.CMD_ColorTable10 = ColorTable10.BackColor
        CP.CMD_ColorTable11 = ColorTable11.BackColor
        CP.CMD_ColorTable12 = ColorTable12.BackColor
        CP.CMD_ColorTable13 = ColorTable13.BackColor
        CP.CMD_ColorTable14 = ColorTable14.BackColor
        CP.CMD_ColorTable15 = ColorTable15.BackColor
        CP.CMD_PopupForeground = CMD_PopupForegroundBar.Value
        CP.CMD_PopupBackground = CMD_PopupBackgroundBar.Value
        CP.CMD_ScreenColorsForeground = CMD_AccentForegroundBar.Value
        CP.CMD_ScreenColorsBackground = CMD_AccentBackgroundBar.Value
        CP.CMD_FaceName = f_cmd.Name
        CP.CMD_FontRaster = CMD_RasterToggle.Checked
        CP.CMD_FontWeight = CMD_FontWeightBox.SelectedIndex * 100
        If Not CMD_RasterToggle.Checked Then
            CP.CMD_FontSize = CMD_FontSizeBar.Value * 65536
        Else
            CP.CMD_FontSize = FixValue(CMD_FontSizeBar.Value) * 65536
        End If
        If CMD_CursorSizeBar.Value < 20 Then
            CP.CMD_CursorSize = 20
        ElseIf CMD_CursorSizeBar.Value > 100 Then
            CP.CMD_CursorSize = 100
        Else
            CP.CMD_CursorSize = CMD_CursorSizeBar.Value
        End If
        If Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "ReleaseId", "").ToString() >= 1909 Then
            CP.CMD_1909_CursorColor = CMD_CursorColor.BackColor
            CP.CMD_1909_CursorType = CMD_CursorStyle.SelectedIndex
            CP.CMD_1909_ForceV2 = CMD_EnhancedTerminal.Checked
            CP.CMD_1909_WindowAlpha = CMD_OpacityBar.Value
            CP.CMD_1909_LineSelection = CMD_LineSelection.Checked
            CP.CMD_1909_TerminalScrolling = CMD_TerminalScrolling.Checked
        End If
#End Region

#Region " PowerShell 32-Bit"
        CP.PS_32_ColorTable00 = PS_32_ColorTable00.BackColor
        CP.PS_32_ColorTable01 = PS_32_ColorTable01.BackColor
        CP.PS_32_ColorTable02 = PS_32_ColorTable02.BackColor
        CP.PS_32_ColorTable03 = PS_32_ColorTable03.BackColor
        CP.PS_32_ColorTable04 = PS_32_ColorTable04.BackColor
        CP.PS_32_ColorTable05 = PS_32_ColorTable05.BackColor
        CP.PS_32_ColorTable06 = PS_32_ColorTable06.BackColor
        CP.PS_32_ColorTable07 = PS_32_ColorTable07.BackColor
        CP.PS_32_ColorTable08 = PS_32_ColorTable08.BackColor
        CP.PS_32_ColorTable09 = PS_32_ColorTable09.BackColor
        CP.PS_32_ColorTable10 = PS_32_ColorTable10.BackColor
        CP.PS_32_ColorTable11 = PS_32_ColorTable11.BackColor
        CP.PS_32_ColorTable12 = PS_32_ColorTable12.BackColor
        CP.PS_32_ColorTable13 = PS_32_ColorTable13.BackColor
        CP.PS_32_ColorTable14 = PS_32_ColorTable14.BackColor
        CP.PS_32_ColorTable15 = PS_32_ColorTable15.BackColor
        CP.PS_32_PopupForeground = PS_32_PopupForegroundBar.Value
        CP.PS_32_PopupBackground = PS_32_PopupBackgroundBar.Value
        CP.PS_32_ScreenColorsForeground = PS_32_AccentForegroundBar.Value
        CP.PS_32_ScreenColorsBackground = PS_32_AccentBackgroundBar.Value
        CP.PS_32_FaceName = f_ps_32.Name
        CP.PS_32_FontRaster = PS_32_RasterToggle.Checked
        CP.PS_32_FontWeight = PS_32_FontWeightBox.SelectedIndex * 100
        If Not PS_32_RasterToggle.Checked Then
            CP.PS_32_FontSize = PS_32_FontSizeBar.Value * 65536
        Else
            CP.PS_32_FontSize = FixValue(PS_32_FontSizeBar.Value) * 65536
        End If
        If PS_32_CursorSizeBar.Value < 20 Then
            CP.PS_32_CursorSize = 20
        ElseIf PS_32_CursorSizeBar.Value > 100 Then
            CP.PS_32_CursorSize = 100
        Else
            CP.PS_32_CursorSize = PS_32_CursorSizeBar.Value
        End If
        If Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "ReleaseId", "").ToString() >= 1909 Then
            CP.PS_32_1909_CursorColor = PS_32_CursorColor.BackColor
            CP.PS_32_1909_CursorType = PS_32_CursorStyle.SelectedIndex
            CP.PS_32_1909_ForceV2 = PS_32_EnhancedTerminal.Checked
            CP.PS_32_1909_WindowAlpha = PS_32_OpacityBar.Value
            CP.PS_32_1909_LineSelection = PS_32_LineSelection.Checked
            CP.PS_32_1909_TerminalScrolling = PS_32_TerminalScrolling.Checked
        End If
#End Region

#Region " PowerShell 64-Bit"
        CP.PS_64_ColorTable00 = PS_64_ColorTable00.BackColor
        CP.PS_64_ColorTable01 = PS_64_ColorTable01.BackColor
        CP.PS_64_ColorTable02 = PS_64_ColorTable02.BackColor
        CP.PS_64_ColorTable03 = PS_64_ColorTable03.BackColor
        CP.PS_64_ColorTable04 = PS_64_ColorTable04.BackColor
        CP.PS_64_ColorTable05 = PS_64_ColorTable05.BackColor
        CP.PS_64_ColorTable06 = PS_64_ColorTable06.BackColor
        CP.PS_64_ColorTable07 = PS_64_ColorTable07.BackColor
        CP.PS_64_ColorTable08 = PS_64_ColorTable08.BackColor
        CP.PS_64_ColorTable09 = PS_64_ColorTable09.BackColor
        CP.PS_64_ColorTable10 = PS_64_ColorTable10.BackColor
        CP.PS_64_ColorTable11 = PS_64_ColorTable11.BackColor
        CP.PS_64_ColorTable12 = PS_64_ColorTable12.BackColor
        CP.PS_64_ColorTable13 = PS_64_ColorTable13.BackColor
        CP.PS_64_ColorTable14 = PS_64_ColorTable14.BackColor
        CP.PS_64_ColorTable15 = PS_64_ColorTable15.BackColor
        CP.PS_64_PopupForeground = PS_64_PopupForegroundBar.Value
        CP.PS_64_PopupBackground = PS_64_PopupBackgroundBar.Value
        CP.PS_64_ScreenColorsForeground = PS_64_AccentForegroundBar.Value
        CP.PS_64_ScreenColorsBackground = PS_64_AccentBackgroundBar.Value
        CP.PS_64_FaceName = f_ps_64.Name
        CP.PS_64_FontRaster = PS_64_RasterToggle.Checked
        CP.PS_64_FontWeight = PS_64_FontWeightBox.SelectedIndex * 100
        If Not PS_64_RasterToggle.Checked Then
            CP.PS_64_FontSize = PS_64_FontSizeBar.Value * 65536
        Else
            CP.PS_64_FontSize = FixValue(PS_64_FontSizeBar.Value) * 65536
        End If
        If PS_64_CursorSizeBar.Value < 20 Then
            CP.PS_64_CursorSize = 20
        ElseIf PS_64_CursorSizeBar.Value > 100 Then
            CP.PS_64_CursorSize = 100
        Else
            CP.PS_64_CursorSize = PS_64_CursorSizeBar.Value
        End If
        If Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "ReleaseId", "").ToString() >= 1909 Then
            CP.PS_64_1909_CursorColor = PS_64_CursorColor.BackColor
            CP.PS_64_1909_CursorType = PS_64_CursorStyle.SelectedIndex
            CP.PS_64_1909_ForceV2 = PS_64_EnhancedTerminal.Checked
            CP.PS_64_1909_WindowAlpha = PS_64_OpacityBar.Value
            CP.PS_64_1909_LineSelection = PS_64_LineSelection.Checked
            CP.PS_64_1909_TerminalScrolling = PS_64_TerminalScrolling.Checked
        End If
#End Region
    End Sub
#End Region

#Region " Events related to Command Prompt"
    Private Sub CMD_PopupForegroundBar_Scroll(sender As Object) Handles CMD_PopupForegroundBar.Scroll
        With CMD_PopupForegroundBar
            CMD_PopupForegroundLbl.Text = .Value
            If .Value = 10 Then CMD_PopupForegroundLbl.Text &= " (A)"
            If .Value = 11 Then CMD_PopupForegroundLbl.Text &= " (B)"
            If .Value = 12 Then CMD_PopupForegroundLbl.Text &= " (C)"
            If .Value = 13 Then CMD_PopupForegroundLbl.Text &= " (D)"
            If .Value = 14 Then CMD_PopupForegroundLbl.Text &= " (E)"
            If .Value = 15 Then CMD_PopupForegroundLbl.Text &= " (F)"
        End With

        UpdateFromTrack(1)
        ApplyPreview()
    End Sub

    Private Sub CMD_PopupBackgroundBar_Scroll(sender As Object) Handles CMD_PopupBackgroundBar.Scroll
        With CMD_PopupBackgroundBar
            CMD_PopupBackgroundLbl.Text = .Value
            If .Value = 10 Then CMD_PopupBackgroundLbl.Text &= " (A)"
            If .Value = 11 Then CMD_PopupBackgroundLbl.Text &= " (B)"
            If .Value = 12 Then CMD_PopupBackgroundLbl.Text &= " (C)"
            If .Value = 13 Then CMD_PopupBackgroundLbl.Text &= " (D)"
            If .Value = 14 Then CMD_PopupBackgroundLbl.Text &= " (E)"
            If .Value = 15 Then CMD_PopupBackgroundLbl.Text &= " (F)"
        End With

        UpdateFromTrack(2)
        ApplyPreview()
    End Sub

    Private Sub CMD_AccentForegroundBar_Scroll(sender As Object) Handles CMD_AccentForegroundBar.Scroll
        With CMD_AccentForegroundBar
            CMD_AccentForegroundLbl.Text = .Value
            If .Value = 10 Then CMD_AccentForegroundLbl.Text &= " (A)"
            If .Value = 11 Then CMD_AccentForegroundLbl.Text &= " (B)"
            If .Value = 12 Then CMD_AccentForegroundLbl.Text &= " (C)"
            If .Value = 13 Then CMD_AccentForegroundLbl.Text &= " (D)"
            If .Value = 14 Then CMD_AccentForegroundLbl.Text &= " (E)"
            If .Value = 15 Then CMD_AccentForegroundLbl.Text &= " (F)"
        End With

        UpdateFromTrack(3) : UpdateFromTrack(4)
        ApplyPreview()
    End Sub

    Private Sub CMD_AccentBackgroundBar_Scroll(sender As Object) Handles CMD_AccentBackgroundBar.Scroll
        With CMD_AccentBackgroundBar
            CMD_AccentBackgroundLbl.Text = .Value
            If .Value = 10 Then CMD_AccentBackgroundLbl.Text &= " (A)"
            If .Value = 11 Then CMD_AccentBackgroundLbl.Text &= " (B)"
            If .Value = 12 Then CMD_AccentBackgroundLbl.Text &= " (C)"
            If .Value = 13 Then CMD_AccentBackgroundLbl.Text &= " (D)"
            If .Value = 14 Then CMD_AccentBackgroundLbl.Text &= " (E)"
            If .Value = 15 Then CMD_AccentBackgroundLbl.Text &= " (F)"
        End With

        UpdateFromTrack(3) : UpdateFromTrack(4)
        ApplyPreview()
    End Sub

    Private Sub XenonButton4_Click(sender As Object, e As EventArgs) Handles XenonButton4.Click
        Process.Start("cmd.exe")
    End Sub

    Private Sub CMD_RasterToggle_CheckedChanged(sender As Object, e As EventArgs) Handles CMD_RasterToggle.CheckedChanged
        If _Shown Then

            If CMD_RasterToggle.Enabled Then
                XenonCMD1.Font = New Font(f_cmd.Name, 12, f_cmd.Style)
            Else
                XenonCMD1.Font = f_cmd
            End If

            ApplyPreview()
        End If
    End Sub

    Private Sub CMD_FontWeightBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CMD_FontWeightBox.SelectedIndexChanged
        If Not _Shown Then Exit Sub
        Dim fx As New LOGFONT
        f_cmd = New Font(CMD_FontsBox.SelectedItem.ToString, f_cmd.Size, f_cmd.Style)
        f_cmd.ToLogFont(fx)
        fx.lfWeight = CMD_FontWeightBox.SelectedIndex * 100
        With Font.FromLogFont(fx) : f_cmd = New Font(.Name, f_cmd.Size, .Style) : End With
        CMD_FontsBox.SelectedItem = f_cmd.Name
        ApplyPreview()
    End Sub

    Private Sub CMD_FontsBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CMD_FontsBox.SelectedIndexChanged
        If _Shown Then
            f_cmd = New Font(CMD_FontsBox.SelectedItem.ToString, f_cmd.Size, f_cmd.Style)
            ApplyPreview()
        End If

    End Sub

    Private Sub CMD_FontSizeBar_Scroll(sender As Object) Handles CMD_FontSizeBar.Scroll
        CMD_FontSizeLbl.Text = CMD_FontSizeBar.Value
        If _Shown Then
            f_cmd = New Font(f_cmd.Name, CMD_FontSizeBar.Value, f_cmd.Style)
            ApplyPreview()
        End If
    End Sub

    Private Sub CMD_CursorSizeBar_Scroll(sender As Object) Handles CMD_CursorSizeBar.Scroll
        UpdateCurPreview(1)
    End Sub

    Private Sub CMD_CursorStyle_SelectedIndexChanged_1(sender As Object, e As EventArgs) Handles CMD_CursorStyle.SelectedIndexChanged
        If My.W10_1909 Then
            ApplyCursorShape(1)
        End If
    End Sub

    Private Sub CMD_CursorColor_Click(sender As Object, e As EventArgs) Handles CMD_CursorColor.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            Exit Sub
        End If

        Dim CList As New List(Of Control) From {sender, CMD_PreviewCUR2}

        Dim C As Color = ColorPickerDlg.Pick(CList)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()
    End Sub

    Private Sub CMD_OpacityBar_Scroll(sender As Object) Handles CMD_OpacityBar.Scroll
        CMD_OpacityLbl.Text = Fix((sender.Value / 255) * 100)
    End Sub
#End Region

#Region " Events related to PowerShell 32-bit"
    Private Sub PS_32_PopupForegroundBar_Scroll(sender As Object) Handles PS_32_PopupForegroundBar.Scroll
        With PS_32_PopupForegroundBar
            PS_32_PopupForegroundLbl.Text = .Value
            If .Value = 10 Then PS_32_PopupForegroundLbl.Text &= " (A)"
            If .Value = 11 Then PS_32_PopupForegroundLbl.Text &= " (B)"
            If .Value = 12 Then PS_32_PopupForegroundLbl.Text &= " (C)"
            If .Value = 13 Then PS_32_PopupForegroundLbl.Text &= " (D)"
            If .Value = 14 Then PS_32_PopupForegroundLbl.Text &= " (E)"
            If .Value = 15 Then PS_32_PopupForegroundLbl.Text &= " (F)"
        End With

        UpdateFromTrack(5)
        ApplyPreview()
    End Sub

    Private Sub PS_32_PopupBackgroundBar_Scroll(sender As Object) Handles PS_32_PopupBackgroundBar.Scroll
        With PS_32_PopupBackgroundBar
            PS_32_PopupBackgroundLbl.Text = .Value
            If .Value = 10 Then PS_32_PopupBackgroundLbl.Text &= " (A)"
            If .Value = 11 Then PS_32_PopupBackgroundLbl.Text &= " (B)"
            If .Value = 12 Then PS_32_PopupBackgroundLbl.Text &= " (C)"
            If .Value = 13 Then PS_32_PopupBackgroundLbl.Text &= " (D)"
            If .Value = 14 Then PS_32_PopupBackgroundLbl.Text &= " (E)"
            If .Value = 15 Then PS_32_PopupBackgroundLbl.Text &= " (F)"
        End With

        UpdateFromTrack(6)
        ApplyPreview()
    End Sub

    Private Sub PS_32_AccentForegroundBar_Scroll(sender As Object) Handles PS_32_AccentForegroundBar.Scroll
        With PS_32_AccentForegroundBar
            PS_32_AccentForegroundLbl.Text = .Value
            If .Value = 10 Then PS_32_AccentForegroundLbl.Text &= " (A)"
            If .Value = 11 Then PS_32_AccentForegroundLbl.Text &= " (B)"
            If .Value = 12 Then PS_32_AccentForegroundLbl.Text &= " (C)"
            If .Value = 13 Then PS_32_AccentForegroundLbl.Text &= " (D)"
            If .Value = 14 Then PS_32_AccentForegroundLbl.Text &= " (E)"
            If .Value = 15 Then PS_32_AccentForegroundLbl.Text &= " (F)"
        End With

        UpdateFromTrack(7) : UpdateFromTrack(8)
        ApplyPreview()
    End Sub

    Private Sub PS_32_AccentBackgroundBar_Scroll(sender As Object) Handles PS_32_AccentBackgroundBar.Scroll
        With PS_32_AccentBackgroundBar
            PS_32_AccentBackgroundLbl.Text = .Value
            If .Value = 10 Then PS_32_AccentBackgroundLbl.Text &= " (A)"
            If .Value = 11 Then PS_32_AccentBackgroundLbl.Text &= " (B)"
            If .Value = 12 Then PS_32_AccentBackgroundLbl.Text &= " (C)"
            If .Value = 13 Then PS_32_AccentBackgroundLbl.Text &= " (D)"
            If .Value = 14 Then PS_32_AccentBackgroundLbl.Text &= " (E)"
            If .Value = 15 Then PS_32_AccentBackgroundLbl.Text &= " (F)"
        End With

        UpdateFromTrack(7) : UpdateFromTrack(8)
        ApplyPreview()
    End Sub

    Private Sub PS_32_RasterToggle_CheckedChanged(sender As Object, e As EventArgs) Handles PS_32_RasterToggle.CheckedChanged
        If _Shown Then

            If PS_32_RasterToggle.Enabled Then
                XenonCMD2.Font = New Font(f_ps_32.Name, 12, f_ps_32.Style)
            Else
                XenonCMD2.Font = f_ps_32
            End If

            ApplyPreview()
        End If
    End Sub

    Private Sub PS_32_FontWeightBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles PS_32_FontWeightBox.SelectedIndexChanged
        If Not _Shown Then Exit Sub
        Dim fx As New LOGFONT
        f_ps_32 = New Font(PS_32_FontsBox.SelectedItem.ToString, f_ps_32.Size, f_ps_32.Style)
        f_ps_32.ToLogFont(fx)
        fx.lfWeight = PS_32_FontWeightBox.SelectedIndex * 100
        With Font.FromLogFont(fx) : f_ps_32 = New Font(.Name, f_ps_32.Size, .Style) : End With
        PS_32_FontsBox.SelectedItem = f_ps_32.Name
        ApplyPreview()
    End Sub

    Private Sub PS_32_FontsBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles PS_32_FontsBox.SelectedIndexChanged
        If _Shown Then
            f_ps_32 = New Font(PS_32_FontsBox.SelectedItem.ToString, f_ps_32.Size, f_ps_32.Style)
            ApplyPreview()
        End If

    End Sub

    Private Sub PS_32_FontSizeBar_Scroll(sender As Object) Handles PS_32_FontSizeBar.Scroll
        PS_32_FontSizeLbl.Text = PS_32_FontSizeBar.Value
        If _Shown Then
            f_ps_32 = New Font(f_ps_32.Name, PS_32_FontSizeBar.Value, f_ps_32.Style)
            ApplyPreview()
        End If
    End Sub

    Private Sub PS_32_CursorSizeBar_Scroll(sender As Object) Handles PS_32_CursorSizeBar.Scroll
        UpdateCurPreview(2)
    End Sub

    Private Sub PS_32_CursorStyle_SelectedIndexChanged_1(sender As Object, e As EventArgs) Handles PS_32_CursorStyle.SelectedIndexChanged
        If My.W10_1909 Then
            ApplyCursorShape(2)
        End If
    End Sub

    Private Sub PS_32_CursorColor_Click(sender As Object, e As EventArgs) Handles PS_32_CursorColor.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            Exit Sub
        End If

        Dim CList As New List(Of Control) From {sender, PS_32_PreviewCUR2}

        Dim C As Color = ColorPickerDlg.Pick(CList)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()
    End Sub

    Private Sub PS_32_OpacityBar_Scroll(sender As Object) Handles PS_32_OpacityBar.Scroll
        PS_32_OpacityLbl.Text = Fix((sender.Value / 255) * 100)
    End Sub
#End Region

#Region " Events related to PowerShell 64-bit"
    Private Sub PS_64_PopupForegroundBar_Scroll(sender As Object) Handles PS_64_PopupForegroundBar.Scroll
        With PS_64_PopupForegroundBar
            PS_64_PopupForegroundLbl.Text = .Value
            If .Value = 10 Then PS_64_PopupForegroundLbl.Text &= " (A)"
            If .Value = 11 Then PS_64_PopupForegroundLbl.Text &= " (B)"
            If .Value = 12 Then PS_64_PopupForegroundLbl.Text &= " (C)"
            If .Value = 13 Then PS_64_PopupForegroundLbl.Text &= " (D)"
            If .Value = 14 Then PS_64_PopupForegroundLbl.Text &= " (E)"
            If .Value = 15 Then PS_64_PopupForegroundLbl.Text &= " (F)"
        End With

        UpdateFromTrack(9)
        ApplyPreview()
    End Sub

    Private Sub PS_64_PopupBackgroundBar_Scroll(sender As Object) Handles PS_64_PopupBackgroundBar.Scroll
        With PS_64_PopupBackgroundBar
            PS_64_PopupBackgroundLbl.Text = .Value
            If .Value = 10 Then PS_64_PopupBackgroundLbl.Text &= " (A)"
            If .Value = 11 Then PS_64_PopupBackgroundLbl.Text &= " (B)"
            If .Value = 12 Then PS_64_PopupBackgroundLbl.Text &= " (C)"
            If .Value = 13 Then PS_64_PopupBackgroundLbl.Text &= " (D)"
            If .Value = 14 Then PS_64_PopupBackgroundLbl.Text &= " (E)"
            If .Value = 15 Then PS_64_PopupBackgroundLbl.Text &= " (F)"
        End With

        UpdateFromTrack(10)
        ApplyPreview()
    End Sub

    Private Sub PS_64_AccentForegroundBar_Scroll(sender As Object) Handles PS_64_AccentForegroundBar.Scroll
        With PS_64_AccentForegroundBar
            PS_64_AccentForegroundLbl.Text = .Value
            If .Value = 10 Then PS_64_AccentForegroundLbl.Text &= " (A)"
            If .Value = 11 Then PS_64_AccentForegroundLbl.Text &= " (B)"
            If .Value = 12 Then PS_64_AccentForegroundLbl.Text &= " (C)"
            If .Value = 13 Then PS_64_AccentForegroundLbl.Text &= " (D)"
            If .Value = 14 Then PS_64_AccentForegroundLbl.Text &= " (E)"
            If .Value = 15 Then PS_64_AccentForegroundLbl.Text &= " (F)"
        End With

        UpdateFromTrack(11) : UpdateFromTrack(12)
        ApplyPreview()
    End Sub

    Private Sub PS_64_AccentBackgroundBar_Scroll(sender As Object) Handles PS_64_AccentBackgroundBar.Scroll
        With PS_64_AccentBackgroundBar
            PS_64_AccentBackgroundLbl.Text = .Value
            If .Value = 10 Then PS_64_AccentBackgroundLbl.Text &= " (A)"
            If .Value = 11 Then PS_64_AccentBackgroundLbl.Text &= " (B)"
            If .Value = 12 Then PS_64_AccentBackgroundLbl.Text &= " (C)"
            If .Value = 13 Then PS_64_AccentBackgroundLbl.Text &= " (D)"
            If .Value = 14 Then PS_64_AccentBackgroundLbl.Text &= " (E)"
            If .Value = 15 Then PS_64_AccentBackgroundLbl.Text &= " (F)"
        End With

        UpdateFromTrack(11) : UpdateFromTrack(12)
        ApplyPreview()
    End Sub

    Private Sub PS_64_RasterToggle_CheckedChanged(sender As Object, e As EventArgs) Handles PS_64_RasterToggle.CheckedChanged
        If _Shown Then

            If PS_64_RasterToggle.Enabled Then
                XenonCMD3.Font = New Font(f_ps_64.Name, 12, f_ps_64.Style)
            Else
                XenonCMD3.Font = f_ps_64
            End If

            ApplyPreview()
        End If
    End Sub

    Private Sub PS_64_FontWeightBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles PS_64_FontWeightBox.SelectedIndexChanged
        If Not _Shown Then Exit Sub
        Dim fx As New LOGFONT
        f_ps_64 = New Font(PS_64_FontsBox.SelectedItem.ToString, f_ps_64.Size, f_ps_64.Style)
        f_ps_64.ToLogFont(fx)
        fx.lfWeight = PS_64_FontWeightBox.SelectedIndex * 100
        With Font.FromLogFont(fx) : f_ps_64 = New Font(.Name, f_ps_64.Size, .Style) : End With
        PS_64_FontsBox.SelectedItem = f_ps_64.Name
        ApplyPreview()
    End Sub

    Private Sub PS_64_FontsBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles PS_64_FontsBox.SelectedIndexChanged
        If _Shown Then
            f_ps_64 = New Font(PS_64_FontsBox.SelectedItem.ToString, f_ps_64.Size, f_ps_64.Style)
            ApplyPreview()
        End If

    End Sub

    Private Sub PS_64_FontSizeBar_Scroll(sender As Object) Handles PS_64_FontSizeBar.Scroll
        PS_64_FontSizeLbl.Text = PS_64_FontSizeBar.Value
        If _Shown Then
            f_ps_64 = New Font(f_ps_64.Name, PS_64_FontSizeBar.Value, f_ps_64.Style)
            ApplyPreview()
        End If
    End Sub

    Private Sub PS_64_CursorSizeBar_Scroll(sender As Object) Handles PS_64_CursorSizeBar.Scroll
        UpdateCurPreview(3)
    End Sub

    Private Sub PS_64_CursorStyle_SelectedIndexChanged_1(sender As Object, e As EventArgs) Handles PS_64_CursorStyle.SelectedIndexChanged
        If My.W10_1909 Then
            ApplyCursorShape(3)
        End If
    End Sub

    Private Sub PS_64_CursorColor_Click(sender As Object, e As EventArgs) Handles PS_64_CursorColor.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            Exit Sub
        End If

        Dim CList As New List(Of Control) From {sender, PS_64_PreviewCUR2}

        Dim C As Color = ColorPickerDlg.Pick(CList)

        sender.backcolor = C
        sender.invalidate

        CList.Clear()
    End Sub

    Private Sub PS_64_OpacityBar_Scroll(sender As Object) Handles PS_64_OpacityBar.Scroll
        PS_64_OpacityLbl.Text = Fix((sender.Value / 255) * 100)
    End Sub
#End Region

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
        UpdateCurPreview(4)
    End Sub

    Private Sub ExtTerminal_CursorStyle_SelectedIndexChanged_1(sender As Object, e As EventArgs) Handles ExtTerminal_CursorStyle.SelectedIndexChanged
        If My.W10_1909 Then
            ApplyCursorShape(4)
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
#End Region

    Private Sub ColorTable00_Click(sender As Object, e As EventArgs) Handles ColorTable00.Click, ColorTable01.Click, ColorTable02.Click, ColorTable03.Click, ColorTable04.Click, ColorTable05.Click,
                                                                             ColorTable06.Click, ColorTable07.Click, ColorTable08.Click, ColorTable09.Click, ColorTable10.Click, ColorTable11.Click,
                                                                             ColorTable12.Click, ColorTable13.Click, ColorTable14.Click, ColorTable15.Click,
                                                                             ColorTable00.Click, PS_32_ColorTable01.Click, PS_32_ColorTable02.Click, PS_32_ColorTable03.Click, PS_32_ColorTable04.Click, PS_32_ColorTable05.Click,
                                                                             PS_32_ColorTable06.Click, PS_32_ColorTable07.Click, PS_32_ColorTable08.Click, PS_32_ColorTable09.Click, PS_32_ColorTable10.Click, PS_32_ColorTable11.Click,
                                                                             PS_32_ColorTable12.Click, PS_32_ColorTable13.Click, PS_32_ColorTable14.Click, PS_32_ColorTable15.Click,
                                                                             ColorTable00.Click, PS_64_ColorTable01.Click, PS_64_ColorTable02.Click, PS_64_ColorTable03.Click, PS_64_ColorTable04.Click, PS_64_ColorTable05.Click,
                                                                             PS_64_ColorTable06.Click, PS_64_ColorTable07.Click, PS_64_ColorTable08.Click, PS_64_ColorTable09.Click, PS_64_ColorTable10.Click, PS_64_ColorTable11.Click,
                                                                             PS_64_ColorTable12.Click, PS_64_ColorTable13.Click, PS_64_ColorTable14.Click, PS_64_ColorTable15.Click,
                                                                             ColorTable00.Click, ExtTerminal_ColorTable01.Click, ExtTerminal_ColorTable02.Click, ExtTerminal_ColorTable03.Click, ExtTerminal_ColorTable04.Click, ExtTerminal_ColorTable05.Click,
                                                                             ExtTerminal_ColorTable06.Click, ExtTerminal_ColorTable07.Click, ExtTerminal_ColorTable08.Click, ExtTerminal_ColorTable09.Click, ExtTerminal_ColorTable10.Click, ExtTerminal_ColorTable11.Click,
                                                                             ExtTerminal_ColorTable12.Click, ExtTerminal_ColorTable13.Click, ExtTerminal_ColorTable14.Click, ExtTerminal_ColorTable15.Click

        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            ApplyPreview()
            Exit Sub
        End If

        Dim CList As New List(Of Control) From {sender}

        If sender.Name.ToString.ToLower.StartsWith("cmd") Then
            CList.Add(XenonCMD1)
        ElseIf sender.Name.ToString.ToLower.StartsWith("ps_32") Then
            CList.Add(XenonCMD2)
        ElseIf sender.Name.ToString.ToLower.StartsWith("ps_64") Then
            CList.Add(XenonCMD3)
        ElseIf sender.Name.ToString.ToLower.StartsWith("extterminal") Then
            CList.Add(XenonCMD4)
        Else
            CList.Add(XenonCMD1)
            CList.Add(XenonCMD2)
            CList.Add(XenonCMD3)
            CList.Add(XenonCMD4)
        End If

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

        If sender.Name.ToString.ToLower.StartsWith("cmd") Then
            UpdateFromTrack(1) : UpdateFromTrack(2) : UpdateFromTrack(3) : UpdateFromTrack(4)

        ElseIf sender.Name.ToString.ToLower.StartsWith("ps_32") Then
            UpdateFromTrack(5) : UpdateFromTrack(6) : UpdateFromTrack(7) : UpdateFromTrack(8)

        ElseIf sender.Name.ToString.ToLower.StartsWith("ps_64") Then
            UpdateFromTrack(9) : UpdateFromTrack(10) : UpdateFromTrack(11) : UpdateFromTrack(12)

        ElseIf sender.Name.ToString.ToLower.StartsWith("extterminal") Then
            UpdateFromTrack(13) : UpdateFromTrack(14) : UpdateFromTrack(15) : UpdateFromTrack(16)

        Else
            UpdateFromTrack(1) : UpdateFromTrack(2) : UpdateFromTrack(3) : UpdateFromTrack(4)
            UpdateFromTrack(5) : UpdateFromTrack(6) : UpdateFromTrack(7) : UpdateFromTrack(8)
            UpdateFromTrack(9) : UpdateFromTrack(10) : UpdateFromTrack(11) : UpdateFromTrack(12)
            UpdateFromTrack(13) : UpdateFromTrack(14) : UpdateFromTrack(15) : UpdateFromTrack(16)
        End If

        CList.Clear()
    End Sub

End Class