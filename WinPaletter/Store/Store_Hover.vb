Imports WinPaletter.NativeMethods
Imports WinPaletter.XenonCore

Public Class Store_Hover

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

    Sub HandleDeactivate()
        If _shown Then
            _shown = False
            Close()
        End If
    End Sub
#End Region

    Private index As Integer = 0
    Public img0 As Bitmap
    Public img1 As Bitmap
    Private _shown As Boolean = False

    Private Sub Store_Hover_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Icon = Store.Icon
        LoadLanguage
        ApplyDarkMode(Me)
        DoubleBuffer

        Dim p As Point = Store.selectedItem.PointToScreen(Point.Empty)
        Location = p

        _shown = False
    End Sub

    Private Sub Store_Hover_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        _shown = True
    End Sub

    Private Sub Store_Hover_MouseClick(sender As Object, e As MouseEventArgs) Handles Me.MouseClick
        Close()
    End Sub

    Private Sub Store_Hover_KeyUp(sender As Object, e As KeyEventArgs) Handles Me.KeyUp
        If e.KeyValue = Keys.Right Or e.KeyValue = Keys.Left Or e.KeyValue = Keys.Up Or e.KeyValue = Keys.Down Then
            SwitchPreview()
        Else
            Close()
        End If
    End Sub

    Private Sub Store_Hover_MouseWheel(sender As Object, e As MouseEventArgs) Handles Me.MouseWheel
        SwitchPreview()
    End Sub

    Sub SwitchPreview()
        If index = 0 Then
            index = 1
            If img1 IsNot Nothing Then BackgroundImage = img1
        Else
            index = 0
            If img0 IsNot Nothing Then BackgroundImage = img0
        End If
    End Sub

    Private Sub Store_Hover_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        My.RenderingHint = If(My.CP.MetricsFonts.Fonts_SingleBitPP, Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit, Drawing.Text.TextRenderingHint.ClearTypeGridFit)
    End Sub
End Class