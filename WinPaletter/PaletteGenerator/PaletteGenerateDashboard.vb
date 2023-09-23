Imports WinPaletter.NativeMethods
Public Class PaletteGenerateDashboard
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
        ApplyStyle(Me)
        Icon = PaletteGenerateFromImage.Icon
        _shown = False

        Location = MainFrm.XenonButton40.PointToScreen(Point.Empty) + New Point(0, MainFrm.XenonButton40.Height)

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

    Private Sub XenonButton1_Click(sender As Object, e As EventArgs) Handles XenonButton1.Click
        Close()
        PaletteGenerateFromImage.ShowDialog()
    End Sub

    Private Sub XenonButton4_Click(sender As Object, e As EventArgs) Handles XenonButton4.Click
        Close()
        PaletteGenerateFromColor.ShowDialog()
    End Sub
End Class