Imports WinPaletter.dragPreviewer
Imports WinPaletter.NativeMethods
Imports WinPaletter.XenonCore
Public Class ApplyingTheme

#Region "Form Shadow"

    Private aeroEnabled As Boolean

    Protected Overrides ReadOnly Property CreateParams() As CreateParams
        Get
            CheckAeroEnabled()
            Dim cp As CreateParams = MyBase.CreateParams
            If Not aeroEnabled Then
                cp.ClassStyle = cp.ClassStyle Or NativeConstants.CS_DROPSHADOW
                cp.ExStyle = cp.ExStyle Or 33554432
                Return cp
            Else
                Return cp
            End If
        End Get
    End Property

    Protected Overrides Sub WndProc(ByRef m As Message)
        Select Case m.Msg
            Case NativeConstants.WM_NCPAINT

                Dim val = 2
                If aeroEnabled Then
                    NativeMethods.Dwmapi.DwmSetWindowAttribute(FindForm.Handle, 2, val, 4)
                    Dim bla As New NativeMethods.Dwmapi.MARGINS()
                    With bla
                        .bottomHeight = 3
                        .leftWidth = 3
                        .rightWidth = 3
                        .topHeight = 3
                    End With
                    NativeMethods.Dwmapi.DwmExtendFrameIntoClientArea(Handle, bla)
                End If
                Exit Select
        End Select

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


    Public Class NativeConstants
        Public Const CS_DROPSHADOW As Integer = &H20000
        Public Const WM_NCPAINT As Integer = &H85
    End Class

#End Region


    Private Sub ApplyingTheme_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ApplyDarkMode(Me)
        NativeMethods.User32.AnimateWindow(Handle, 250, NativeMethods.User32.AnimateWindowFlags.AW_ACTIVATE Or NativeMethods.User32.AnimateWindowFlags.AW_BLEND)
        Invalidate()
    End Sub


    Private Sub ApplyingTheme_TextChanged(sender As Object, e As EventArgs) Handles Me.TextChanged
        lbl.Text = Text
    End Sub

    Private Sub ApplyingTheme_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing


        Try
            Dim CWindows As String = Environment.GetFolderPath(Environment.SpecialFolder.Windows)

            My.Computer.Audio.Play(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\AppEvents\Schemes\Apps\.Default\ChangeTheme\.Current", "", CWindows & "\media\Windows Logon Sound.wav"), AudioPlayMode.Background)
        Catch
        End Try

        NativeMethods.User32.AnimateWindow(Handle, 250, NativeMethods.User32.AnimateWindowFlags.AW_HIDE Or NativeMethods.User32.AnimateWindowFlags.AW_BLEND)

    End Sub

    Private Sub ApplyingTheme_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Invalidate()
    End Sub
End Class