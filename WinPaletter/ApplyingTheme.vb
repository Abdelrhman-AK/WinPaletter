Imports System.ComponentModel
Imports System.Runtime.InteropServices
Imports Microsoft.Win32
Imports WinPaletter.CP
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
                    NativeMethods.DwmSetWindowAttribute(FindForm.Handle, If(GetRoundedCorners(), 2, 1), val, 4)
                    Dim bla As New NativeStructs.MARGINS()
                    With bla
                        .bottomHeight = 1
                        .leftWidth = 1
                        .rightWidth = 1
                        .topHeight = 1
                    End With
                    NativeMethods.DwmExtendFrameIntoClientArea(Handle, bla)
                End If
                Exit Select
        End Select

        MyBase.WndProc(m)
    End Sub

    Private Sub CheckAeroEnabled()
        If Environment.OSVersion.Version.Major >= 6 Then
            Dim enabled As Integer = 0
            Dim response As Integer = NativeMethods.DwmIsCompositionEnabled(enabled)
            aeroEnabled = (enabled = 1)
        Else
            aeroEnabled = False
        End If
    End Sub

    Public Class NativeStructs

        Public Structure MARGINS
            Public leftWidth As Integer
            Public rightWidth As Integer
            Public topHeight As Integer
            Public bottomHeight As Integer
        End Structure

    End Class

    Public Class NativeMethods

        <Runtime.InteropServices.DllImport("dwmapi")> Public Shared Function DwmExtendFrameIntoClientArea(ByVal hWnd As IntPtr, ByRef pMarInset As NativeStructs.MARGINS) As Integer
        End Function

        <Runtime.InteropServices.DllImport("dwmapi")> Friend Shared Function DwmSetWindowAttribute(ByVal hwnd As IntPtr, ByVal attr As Integer, ByRef attrValue As Integer, ByVal attrSize As Integer) As Integer
        End Function

        <Runtime.InteropServices.DllImport("dwmapi.dll")> Public Shared Function DwmIsCompositionEnabled(ByRef pfEnabled As Integer) As Integer
        End Function

    End Class

    Public Class NativeConstants
        Public Const CS_DROPSHADOW As Integer = &H20000
        Public Const WM_NCPAINT As Integer = &H85
    End Class

#End Region

    <Flags>
    Enum AnimateWindowFlags
        AW_HOR_POSITIVE = &H0
        AW_HOR_NEGATIVE = &H2
        AW_VER_POSITIVE = &H4
        AW_VER_NEGATIVE = &H8
        AW_CENTER = &H10
        AW_HIDE = &H10000
        AW_ACTIVATE = &H20000
        AW_SLIDE = &H40000
        AW_BLEND = &H80000
    End Enum

    <DllImport("user32.dll")>
    Shared Function AnimateWindow(ByVal hWnd As IntPtr, ByVal time As Integer, ByVal flags As AnimateWindowFlags) As Boolean
    End Function


    Private Sub ApplyingTheme_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ApplyDarkMode(Me)
        AnimateWindow(Handle, 250, AnimateWindowFlags.AW_ACTIVATE Or AnimateWindowFlags.AW_BLEND)
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

        AnimateWindow(Handle, 250, AnimateWindowFlags.AW_HIDE Or AnimateWindowFlags.AW_BLEND)

    End Sub

    Private Sub ApplyingTheme_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Invalidate()
    End Sub
End Class