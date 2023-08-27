Imports WinPaletter.NativeMethods
Imports WinPaletter.XenonCore
Public Class TerminalsDashboard
    ReadOnly _Speed As Integer = 20
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
        LoadLanguage
        ApplyDarkMode(Me)

        _shown = False

        Location = MainFrm.XenonButton24.PointToScreen(Point.Empty) - New Point(0, Height)

        If My.W10 Then PictureBox1.Image = My.Resources.Native10 Else PictureBox1.Image = My.Resources.Native11

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

        If My.Settings.WindowsTerminals.Bypass Then
            WindowsTerminal._Mode = WinTerminal.Version.Stable
            Me.Close()
            WindowsTerminal.ShowDialog()
        Else

            If My.W10 Or My.W11 Then
                Dim TerDir As String

                If Not My.Settings.WindowsTerminals.Path_Deflection Then
                    TerDir = My.PATH_TerminalJSON
                Else
                    If IO.File.Exists(My.Settings.WindowsTerminals.Terminal_Stable_Path) Then
                        TerDir = My.Settings.WindowsTerminals.Terminal_Stable_Path
                    Else
                        TerDir = My.PATH_TerminalJSON
                    End If
                End If

                If IO.File.Exists(TerDir) Then
                    WindowsTerminal._Mode = WinTerminal.Version.Stable
                    Me.Close()
                    WindowsTerminal.ShowDialog()
                Else
                    MsgBox(My.Lang.TerminalStable_notFound, MsgBoxStyle.Exclamation, My.Lang.Terminal_supposed & """" & TerDir & """", My.Lang.CollapseNote, My.Lang.ExpandNote, My.Lang.Terminal_Bypass)
                End If

            Else
                MsgBox(My.Lang.Terminal_CantRun, MsgBoxStyle.Exclamation, "", My.Lang.CollapseNote, My.Lang.ExpandNote, My.Lang.Terminal_Bypass)
            End If

        End If

    End Sub

    Private Sub XenonButton5_Click(sender As Object, e As EventArgs) Handles XenonButton5.Click
        If My.Settings.WindowsTerminals.Bypass Then
            WindowsTerminal._Mode = WinTerminal.Version.Preview
            Me.Close()
            WindowsTerminal.ShowDialog()
        Else
            If My.W10 Or My.W11 Then
                Dim TerPreDir As String

                If Not My.Settings.WindowsTerminals.Path_Deflection Then
                    TerPreDir = My.PATH_TerminalPreviewJSON
                Else
                    If IO.File.Exists(My.Settings.WindowsTerminals.Terminal_Preview_Path) Then
                        TerPreDir = My.Settings.WindowsTerminals.Terminal_Preview_Path
                    Else
                        TerPreDir = My.PATH_TerminalPreviewJSON
                    End If
                End If

                If IO.File.Exists(TerPreDir) Then
                    WindowsTerminal._Mode = WinTerminal.Version.Preview
                    Me.Close()
                    WindowsTerminal.ShowDialog()
                Else
                    MsgBox(My.Lang.TerminalPreview_notFound, MsgBoxStyle.Exclamation, My.Lang.Terminal_supposed & """" & TerPreDir & """", My.Lang.CollapseNote, My.Lang.ExpandNote, My.Lang.Terminal_Bypass)
                End If

            Else
                MsgBox(My.Lang.Terminal_CantRun, MsgBoxStyle.Exclamation, "", My.Lang.CollapseNote, My.Lang.ExpandNote, My.Lang.Terminal_Bypass)
            End If
        End If
    End Sub

    Private Sub XenonButton1_Click(sender As Object, e As EventArgs) Handles XenonButton1.Click
        cmd._Edition = CMD.Edition.CMD
        Me.Close()
        cmd.ShowDialog()
    End Sub

    Private Sub XenonButton2_Click(sender As Object, e As EventArgs) Handles XenonButton2.Click
        ExternalTerminal.ShowDialog()
        Me.Close()
    End Sub

    Private Sub XenonButton4_Click(sender As Object, e As EventArgs) Handles XenonButton4.Click
        If My.Settings.WindowsTerminals.Bypass Then
            cmd._Edition = CMD.Edition.PowerShellx86
            Me.Close()
            cmd.ShowDialog()
        Else
            Kernel32.Wow64DisableWow64FsRedirection(IntPtr.Zero)
            Dim Dir As String = Environment.GetEnvironmentVariable("WINDIR") & "\System32\WindowsPowerShell\v1.0"

            If IO.Directory.Exists(Dir) Then
                cmd._Edition = CMD.Edition.PowerShellx86
                Me.Close()
                cmd.ShowDialog()
            Else
                MsgBox(My.Lang.PowerShellx86_notFound, MsgBoxStyle.Exclamation, My.Lang.Terminal_supposed & """" & Dir & """", My.Lang.CollapseNote, My.Lang.ExpandNote, My.Lang.Terminal_Bypass)
            End If

            Kernel32.Wow64RevertWow64FsRedirection(IntPtr.Zero)
        End If
    End Sub

    Private Sub XenonButton3_Click(sender As Object, e As EventArgs) Handles XenonButton3.Click
        If My.Settings.WindowsTerminals.Bypass Then
            cmd._Edition = CMD.Edition.PowerShellx64
            Me.Close()
            cmd.ShowDialog()
        Else
            Kernel32.Wow64DisableWow64FsRedirection(IntPtr.Zero)
            Dim Dir As String = Environment.GetEnvironmentVariable("WINDIR") & "\SysWOW64\WindowsPowerShell\v1.0"

            If IO.Directory.Exists(Dir) Then
                cmd._Edition = CMD.Edition.PowerShellx64
                Me.Close()
                cmd.ShowDialog()
            Else
                MsgBox(My.Lang.PowerShellx64_notFound, MsgBoxStyle.Exclamation, My.Lang.Terminal_supposed & """" & Dir & """", My.Lang.CollapseNote, My.Lang.ExpandNote, My.Lang.Terminal_Bypass)
            End If

            Kernel32.Wow64RevertWow64FsRedirection(IntPtr.Zero)
        End If
    End Sub

End Class