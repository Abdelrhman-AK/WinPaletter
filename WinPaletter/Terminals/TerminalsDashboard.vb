Imports WinPaletter.NativeMethods
Imports WinPaletter.XenonCore
Public Class TerminalsDashboard
    Private _Speed As Integer = 20
    Private _shown As Boolean

#Region "Form Shadow"

    Private aeroEnabled As Boolean

    Protected Overrides ReadOnly Property CreateParams() As CreateParams
        Get
            CheckAeroEnabled()
            Dim cp As CreateParams = MyBase.CreateParams
            If Not aeroEnabled Then
                cp.ClassStyle = cp.ClassStyle Or Dwmapi.CS_DROPSHADOW
                cp.ExStyle = cp.ExStyle Or 33554432
                Return cp
            Else
                Return cp
            End If
        End Get
    End Property

    Protected Overrides Sub WndProc(ByRef m As Message)
        Select Case m.Msg
            Case Dwmapi.WM_NCPAINT
                Dim val = 2
                If aeroEnabled Then
                    Dwmapi.DwmSetWindowAttribute(Handle, If(GetRoundedCorners(), 2, 1), val, 4)
                    Dim bla As New Dwmapi.MARGINS()
                    With bla
                        .bottomHeight = 1
                        .leftWidth = 1
                        .rightWidth = 1
                        .topHeight = 1
                    End With
                    Dwmapi.DwmExtendFrameIntoClientArea(Handle, bla)
                End If
                Exit Select
        End Select

        Const WM_NCACTIVATE As UInt32 = &H86

        If m.Msg = WM_NCACTIVATE AndAlso m.WParam.ToInt32() = 0 Then
            HandleDeactivate()
        End If

        MyBase.WndProc(m)
    End Sub

    Private Sub CheckAeroEnabled()
        If Environment.OSVersion.Version.Major >= 6 Then
            Dim Com As Boolean
            Dwmapi.DwmIsCompositionEnabled(Com)
            aeroEnabled = Com
        Else
            aeroEnabled = False
        End If
    End Sub
#End Region

    Private Sub TerminalsDashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ApplyDarkMode(Me)
        _shown = False

        Dim p As Point = MousePosition - New Point(0, Height)

        If p.Y + Height > My.Computer.Screen.WorkingArea.Bottom Then
            p.Y = My.Computer.Screen.WorkingArea.Bottom - Height - 5
        End If

        If p.Y < My.Computer.Screen.WorkingArea.Top Then
            p.Y = 0
        End If

        If p.X + Width > My.Computer.Screen.WorkingArea.Right Then
            p.X = My.Computer.Screen.WorkingArea.Right - Width - 5
        End If

        If p.X < My.Computer.Screen.WorkingArea.Left Then
            p.X = 0
        End If


        Location = p

        User32.AnimateWindow(Handle, _Speed, User32.AnimateWindowFlags.AW_ACTIVATE Or User32.AnimateWindowFlags.AW_BLEND)

        Invalidate()
    End Sub

    Private Sub SubMenu_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        User32.AnimateWindow(Handle, _Speed, User32.AnimateWindowFlags.AW_HIDE Or User32.AnimateWindowFlags.AW_BLEND)
    End Sub

    Private Sub TerminalsDashboard_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        _shown = True
    End Sub

    Sub HandleDeactivate()
        If _shown Then
            _shown = False
            DialogResult = DialogResult.None
            Me.Close()
        End If
    End Sub


    Private Sub XenonButton6_Click(sender As Object, e As EventArgs) Handles XenonButton6.Click

        If My.Application._Settings.Terminal_Bypass Then
            WindowsTerminal._Mode = WinTerminal.Version.Stable
            Me.Close()
            WindowsTerminal.ShowDialog()
        Else

            If My.W10 Or My.W11 Then
                Dim TerDir As String = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) & "\AppData\Local\Packages\Microsoft.WindowsTerminal_8wekyb3d8bbwe\LocalState\settings.json"

                If IO.File.Exists(TerDir) Then
                    WindowsTerminal._Mode = WinTerminal.Version.Stable
                    Me.Close()
                    WindowsTerminal.ShowDialog()
                Else
                    MsgBox("Windows Terminal Stable isn't installed or ""settings.json"" isn't accessible." & vbCrLf & vbCrLf & "It is supposed to be located in: " & vbCrLf & """" & TerDir & """" & vbCrLf & vbCrLf & "However, you can bypass this restriction in Settings > Terminals (In case you want to design a theme for all Versions of Windows and save it as a file for sharing, not applying it.)", MsgBoxStyle.Exclamation + My.Application.MsgboxRt)
                End If

            Else
                MsgBox("You can't run Windows Terminal in current OS. It is available only in Windows 10 and 11." & vbCrLf & vbCrLf & "However, you can bypass this restriction in Settings > Terminals (In case you want to design a theme for all Versions of Windows and save it as a file for sharing, not applying it.", MsgBoxStyle.Exclamation + My.Application.MsgboxRt)
            End If

        End If

    End Sub

    Private Sub XenonButton5_Click(sender As Object, e As EventArgs) Handles XenonButton5.Click
        If My.Application._Settings.Terminal_Bypass Then
            WindowsTerminal._Mode = WinTerminal.Version.Preview
            Me.Close()
            WindowsTerminal.ShowDialog()
        Else
            If My.W10 Or My.W11 Then
                Dim TerPreDir As String = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) & "\AppData\Local\Packages\Microsoft.WindowsTerminalPreview_8wekyb3d8bbwe\LocalState\settings.json"

                If IO.File.Exists(TerPreDir) Then
                    WindowsTerminal._Mode = WinTerminal.Version.Preview
                    Me.Close()
                    WindowsTerminal.ShowDialog()
                Else
                    MsgBox("Windows Terminal Preview isn't installed or ""settings.json"" isn't accessible." & vbCrLf & vbCrLf & "It is supposed to be located in: " & vbCrLf & """" & TerPreDir & """" & vbCrLf & vbCrLf & "However, you can bypass this restriction in Settings > Terminals (In case you want to design a theme for all Versions of Windows and save it as a file for sharing, not applying it.)", MsgBoxStyle.Exclamation + My.Application.MsgboxRt)
                End If


            Else
                MsgBox("You can't run Windows Terminal in current OS. It is available only in Windows 10 and 11." & vbCrLf & vbCrLf & "However, you can bypass this restriction in Settings > Terminals (In case you want to design a theme for all Versions of Windows and save it as a file for sharing, not applying it.", MsgBoxStyle.Exclamation + My.Application.MsgboxRt)
            End If
        End If
    End Sub

    Private Sub XenonButton7_Click(sender As Object, e As EventArgs) Handles XenonButton7.Click
        If My.Application._Settings.Terminal_Bypass Then
            WindowsTerminal._Mode = WinTerminal.Version.Developer
            Me.Close()
            WindowsTerminal.ShowDialog()
        Else
            If My.W10 Or My.W11 Then
                Dim TerDevDir As String = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) & "\AppData\Local\Packages\Microsoft.WindowsTerminalDeveloper_8wekyb3d8bbwe\LocalState\settings.json"
                If IO.File.Exists(TerDevDir) Then
                    WindowsTerminal._Mode = WinTerminal.Version.Developer
                    Me.Close()
                    WindowsTerminal.ShowDialog()
                Else
                    MsgBox("Windows Terminal Developer isn't installed or ""settings.json"" isn't accessible." & vbCrLf & vbCrLf & "It is supposed to be located in: " & vbCrLf & """" & TerDevDir & """" & vbCrLf & vbCrLf & "However, you can bypass this restriction in Settings > Terminals (In case you want to design a theme for all Versions of Windows and save it as a file for sharing, not applying it.)", MsgBoxStyle.Exclamation + My.Application.MsgboxRt)
                End If

            Else
                MsgBox("You can't run Windows Terminal in current OS. It is available only in Windows 10 and 11." & vbCrLf & vbCrLf & "However, you can bypass this restriction in Settings > Terminals (In case you want to design a theme for all Versions of Windows and save it as a file for sharing, not applying it.", MsgBoxStyle.Exclamation + My.Application.MsgboxRt)
            End If
        End If
    End Sub

    Private Sub XenonButton1_Click(sender As Object, e As EventArgs) Handles XenonButton1.Click
        cmd._Edition = cmd.Edition.CMD
        Me.Close()
        cmd.ShowDialog()
    End Sub

    Private Sub XenonButton2_Click(sender As Object, e As EventArgs) Handles XenonButton2.Click
        ExternalTerminal.ShowDialog()
        Me.Close()
    End Sub

    Private Sub XenonButton4_Click(sender As Object, e As EventArgs) Handles XenonButton4.Click
        If My.Application._Settings.Terminal_Bypass Then
            cmd._Edition = cmd.Edition.PowerShellx86
            Me.Close()
            cmd.ShowDialog()
        Else
            Kernel32.Wow64DisableWow64FsRedirection(IntPtr.Zero)
            Dim Dir As String = Environment.GetEnvironmentVariable("WINDIR") & "\System32\WindowsPowerShell\v1.0"

            If IO.Directory.Exists(Dir) Then
                cmd._Edition = cmd.Edition.PowerShellx86
                Me.Close()
                cmd.ShowDialog()
            Else
                MsgBox("Microsoft PowerShell x86 is not installed." & vbCrLf & vbCrLf & "It is supposed to be located in: " & vbCrLf & """" & Dir & """" & vbCrLf & vbCrLf & "However, you can bypass this restriction in Settings > Terminals (In case you want to design a theme for all Versions of Windows and save it as a file for sharing, not applying it.)", MsgBoxStyle.Exclamation + My.Application.MsgboxRt)
            End If

            Kernel32.Wow64RevertWow64FsRedirection(IntPtr.Zero)
        End If
    End Sub

    Private Sub XenonButton3_Click(sender As Object, e As EventArgs) Handles XenonButton3.Click
        If My.Application._Settings.Terminal_Bypass Then
            cmd._Edition = cmd.Edition.PowerShellx64
            Me.Close()
            cmd.ShowDialog()
        Else
            Kernel32.Wow64DisableWow64FsRedirection(IntPtr.Zero)
            Dim Dir As String = Environment.GetEnvironmentVariable("WINDIR") & "\SysWOW64\WindowsPowerShell\v1.0"

            If IO.Directory.Exists(Dir) Then
                cmd._Edition = cmd.Edition.PowerShellx64
                Me.Close()
                cmd.ShowDialog()
            Else
                MsgBox("Microsoft PowerShell x64 is not installed." & vbCrLf & vbCrLf & "It is supposed to be located in: " & vbCrLf & """" & Dir & """" & vbCrLf & vbCrLf & "However, you can bypass this restriction in Settings > Terminals (In case you want to design a theme for all Versions of Windows and save it as a file for sharing, not applying it.)", MsgBoxStyle.Exclamation + My.Application.MsgboxRt)
            End If

            Kernel32.Wow64RevertWow64FsRedirection(IntPtr.Zero)
        End If
    End Sub
End Class