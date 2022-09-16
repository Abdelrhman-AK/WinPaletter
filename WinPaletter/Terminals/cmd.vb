Imports System.Runtime.CompilerServices
Imports System.Security.Principal
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Status
Imports Microsoft.Win32
Imports WinPaletter.XenonCore

Public Class cmd
    Dim f_cmd As Font = New Font("Cascadia Mono", 18, FontStyle.Regular)
    Dim f_ps_32 As Font = New Font("Cascadia Mono", 18, FontStyle.Regular)
    Dim f_ps_64 As Font = New Font("Cascadia Mono", 18, FontStyle.Regular)
    Dim f_extterminal As Font = New Font("Cascadia Mono", 18, FontStyle.Regular)

    Private _Shown As Boolean = False

#Region "   Subs not related to colors and shapes"
    Private Sub cmd_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _Shown = False
        FillFonts(CMD_FontsBox, True)
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
            y_cmd = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Console\" & RegKey, "FaceName", "Cascadia Mono")

            If CP.IsFontInstalled(y_cmd) Then
                With Font.FromLogFont(New LOGFONT With {.lfFaceName = y_cmd, .lfWeight = wg}) : f_extterminal = New Font(.FontFamily, CInt(ExtTerminal_FontSizeBar.Value), .Style) : End With
                ExtTerminal_FontsBox.SelectedItem = f_extterminal.Name
            Else
                ExtTerminal_FontsBox.SelectedItem = "Cascadia Mono"
            End If

        Catch
            ExtTerminal_FontsBox.SelectedItem = "Cascadia Mono"
        End Try

        If Microsoft.Win32.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "ReleaseId", "").ToString() >= 1909 Then
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
        End If
    End Sub
    Sub UpdateCurPreview1()
        Dim all As Integer = CMD_PreviewCUR.Height - 4
        Dim v As Integer
        CMD_PreviewCUR2.Height = all * (CMD_CursorSizeBar.Value / CMD_CursorSizeBar.Maximum)
        CMD_PreviewCUR2.Top = CMD_PreviewCUR.Height - CMD_PreviewCUR2.Height - 2
        CMD_PreviewCUR_LBL.Text = CMD_CursorSizeBar.Value
        If Microsoft.Win32.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "ReleaseId", "").ToString() >= 1909 Then ApplyCursorShape(1)
    End Sub
    Sub ApplyPreview()
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

        XenonCMD1.Refresh()
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
        End If
    End Sub

#End Region

#Region "   CP Handling"
    Sub ApplyFromCP([CP] As CP)
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
        If Microsoft.Win32.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "ReleaseId", "").ToString() >= 1909 Then
            CMD_CursorStyle.SelectedIndex = CP.CMD_1909_CursorType
            CMD_CursorColor.BackColor = CP.CMD_1909_CursorColor
            CMD_PreviewCUR2.BackColor = CP.CMD_1909_CursorColor
            CMD_EnhancedTerminal.Checked = CP.CMD_1909_ForceV2
            CMD_OpacityBar.Value = CP.CMD_1909_WindowAlpha
            CMD_OpacityLbl.Text = (CP.CMD_1909_WindowAlpha / 255) * 100
            CMD_LineSelection.Checked = CP.CMD_1909_LineSelection
            CMD_TerminalScrolling.Checked = CP.CMD_1909_TerminalScrolling
            ApplyCursorShape(1)
        End If
        UpdateCurPreview1()
    End Sub

    Sub ApplyToCP([CP] As CP)
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
        If Microsoft.Win32.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "ReleaseId", "").ToString() >= 1909 Then
            CP.CMD_1909_CursorColor = CMD_CursorColor.BackColor
            CP.CMD_1909_CursorType = CMD_CursorStyle.SelectedIndex
            CP.CMD_1909_ForceV2 = CMD_EnhancedTerminal.Checked
            CP.CMD_1909_WindowAlpha = CMD_OpacityBar.Value
            CP.CMD_1909_LineSelection = CMD_LineSelection.Checked
            CP.CMD_1909_TerminalScrolling = CMD_TerminalScrolling.Checked
        End If
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
        UpdateCurPreview1()
    End Sub

    Private Sub CMD_CursorStyle_SelectedIndexChanged_1(sender As Object, e As EventArgs) Handles CMD_CursorStyle.SelectedIndexChanged
        If Microsoft.Win32.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "ReleaseId", "").ToString() >= 1909 Then
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
        CMD_OpacityLbl.Text = (sender.Value / 255) * 100
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

        UpdateFromTrack(1)
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

        UpdateFromTrack(2)
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

        UpdateFromTrack(3) : UpdateFromTrack(4)
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

        UpdateFromTrack(3) : UpdateFromTrack(4)
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
        UpdateCurPreview1()
    End Sub

    Private Sub PS_32_CursorStyle_SelectedIndexChanged_1(sender As Object, e As EventArgs) Handles PS_32_CursorStyle.SelectedIndexChanged
        If Microsoft.Win32.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "ReleaseId", "").ToString() >= 1909 Then
            ApplyCursorShape(1)
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
        PS_32_OpacityLbl.Text = (sender.Value / 255) * 100
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

        UpdateFromTrack(1)
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

        UpdateFromTrack(2)
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

        UpdateFromTrack(3) : UpdateFromTrack(4)
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

        UpdateFromTrack(3) : UpdateFromTrack(4)
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
        UpdateCurPreview1()
    End Sub

    Private Sub PS_64_CursorStyle_SelectedIndexChanged_1(sender As Object, e As EventArgs) Handles PS_64_CursorStyle.SelectedIndexChanged
        If Microsoft.Win32.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "ReleaseId", "").ToString() >= 1909 Then
            ApplyCursorShape(1)
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
        PS_64_OpacityLbl.Text = (sender.Value / 255) * 100
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
        UpdateCurPreview1()
    End Sub

    Private Sub ExtTerminal_CursorStyle_SelectedIndexChanged_1(sender As Object, e As EventArgs) Handles ExtTerminal_CursorStyle.SelectedIndexChanged
        If Microsoft.Win32.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "ReleaseId", "").ToString() >= 1909 Then
            ApplyCursorShape(1)
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
        ExtTerminal_OpacityLbl.Text = (sender.Value / 255) * 100
    End Sub
#End Region

    Private Sub ColorTable00_Click(sender As Object, e As EventArgs) Handles ColorTable00.Click, ColorTable01.Click, ColorTable02.Click, ColorTable03.Click, ColorTable04.Click, ColorTable05.Click,
                                                                             ColorTable06.Click, ColorTable07.Click, ColorTable08.Click, ColorTable09.Click, ColorTable10.Click, ColorTable11.Click,
                                                                             ColorTable12.Click, ColorTable13.Click, ColorTable14.Click, ColorTable15.Click

        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            ApplyPreview()
            Exit Sub
        End If

        Dim CList As New List(Of Control) From {sender}

        If sender.Name.ToString.ToLower.StartsWith("cmd") Then
            CList.Add(XenonCMD1)
        ElseIf sender.Name.ToString.ToLower.StartsWith("ps_32") Then
            'CList.Add(XenonCMD2)
        ElseIf sender.Name.ToString.ToLower.StartsWith("ps_64") Then
            'CList.Add(XenonCMD3)
        Else
            CList.Add(XenonCMD1)
            'CList.Add(XenonCMD2)
            'CList.Add(XenonCMD3)
        End If

        Dim _Conditions As New Conditions

        Select Case sender.Name.ToString.ToLower
            Case ColorTable00.Name.ToLower
                _Conditions.CMD_ColorTable00 = True

            Case ColorTable01.Name.ToLower
                _Conditions.CMD_ColorTable01 = True

            Case ColorTable02.Name.ToLower
                _Conditions.CMD_ColorTable02 = True

            Case ColorTable03.Name.ToLower
                _Conditions.CMD_ColorTable03 = True

            Case ColorTable04.Name.ToLower
                _Conditions.CMD_ColorTable04 = True

            Case ColorTable05.Name.ToLower
                _Conditions.CMD_ColorTable05 = True

            Case ColorTable06.Name.ToLower
                _Conditions.CMD_ColorTable06 = True

            Case ColorTable07.Name.ToLower
                _Conditions.CMD_ColorTable07 = True

            Case ColorTable08.Name.ToLower
                _Conditions.CMD_ColorTable08 = True

            Case ColorTable09.Name.ToLower
                _Conditions.CMD_ColorTable09 = True

            Case ColorTable10.Name.ToLower
                _Conditions.CMD_ColorTable10 = True

            Case ColorTable11.Name.ToLower
                _Conditions.CMD_ColorTable11 = True

            Case ColorTable12.Name.ToLower
                _Conditions.CMD_ColorTable12 = True

            Case ColorTable13.Name.ToLower
                _Conditions.CMD_ColorTable13 = True

            Case ColorTable14.Name.ToLower
                _Conditions.CMD_ColorTable14 = True

            Case ColorTable15.Name.ToLower
                _Conditions.CMD_ColorTable15 = True

                'Case 'PS_32_ColorTable05.Name.ToLower
                '_Conditions.CMD_ColorTable05 = True

                'Case 'PS_32_ColorTable06.Name.ToLower
                '_Conditions.CMD_ColorTable06 = True

                'Case 'PS_64_ColorTable05.Name.ToLower
                '_Conditions.CMD_ColorTable05 = True

                'Case 'PS_64_ColorTable06.Name.ToLower
                '_Conditions.CMD_ColorTable06 = True

        End Select

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
        Else
            UpdateFromTrack(1) : UpdateFromTrack(2) : UpdateFromTrack(3) : UpdateFromTrack(4)
            UpdateFromTrack(5) : UpdateFromTrack(6) : UpdateFromTrack(7) : UpdateFromTrack(8)
            UpdateFromTrack(9) : UpdateFromTrack(10) : UpdateFromTrack(11) : UpdateFromTrack(12)
        End If

        CList.Clear()
    End Sub


End Class