Imports WinPaletter.NativeMethods
Imports WinPaletter.XenonCore

Public Class Popup_color
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
    Private _shown As Boolean = False

    Private Sub SubMenu_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        _shown = True
    End Sub


    Sub HandleDeactivate()
        If _shown Then
            _shown = False
            Me.Close()
        End If
    End Sub

    Private Sub Popup_color_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ApplyDarkMode(Me)
        _shown = False
    End Sub

    Public Sub LoadFromCPControl(ColorCP As XenonCP)
        Show()

        BackColor = ColorCP.BackColor

        Location = ColorCP.Parent.PointToScreen(ColorCP.Location)

        Me.Size = ColorCP.Size

        Refresh()

        Animate(ColorCP)
    End Sub

    Sub Animate(ColorCP As XenonCP)
        SuspendLayout()

        Dim DesiredWidth As Integer = 500
        Dim DesiredHeight As Integer = 645

        Dim DesiredX As Integer = ColorCP.Parent.PointToScreen(ColorCP.Location).X + ColorCP.Width - DesiredWidth
        Dim DesiredY As Integer = ColorCP.FindForm.Top + (ColorCP.FindForm.Height - DesiredHeight) / 2

        Dim fw As Integer = DesiredWidth - Width
        Dim fh As Integer = DesiredHeight - Height

        Dim fx As Integer = Location.X - DesiredX
        Dim fy As Integer = Location.Y - DesiredY

        Dim Factor As Decimal = 0.1

        Visual.FadeColor(Me, "BackColor", BackColor, If(GetDarkMode(), My.Application.BackColor_Dark, My.Application.BackColor_Light), 20, 20)

        For i As Double = 0 To 1 Step Factor

            Width += Factor * fw
            Left -= Factor * fx

            Height += Factor * fh
            Top -= Factor * fy

            Threading.Thread.Sleep(1)

            Invalidate()
        Next

        Width = DesiredWidth
        Height = DesiredHeight
        Left = DesiredX
        Top = DesiredY

        ResumeLayout()

    End Sub

End Class