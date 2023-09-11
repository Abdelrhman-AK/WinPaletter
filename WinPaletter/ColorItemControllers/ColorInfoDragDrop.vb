Imports WinPaletter.NativeMethods
Imports WinPaletter.XenonCore

Public Class ColorInfoDragDrop

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

    ReadOnly _dark As Single = 0.7

    Private Sub ColorInfoDragDrop_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Location = MousePosition + New Point(15, 15)

        LoadLanguage
        ApplyDarkMode(Me)

        Label6.Font = My.Application.ConsoleFontMedium
        Label7.Font = My.Application.ConsoleFontMedium
        Label8.Font = My.Application.ConsoleFontMedium
        Label9.Font = My.Application.ConsoleFontMedium
        Label10.Font = My.Application.ConsoleFontMedium
        Label11.Font = My.Application.ConsoleFontMedium
        Label12.Font = My.Application.ConsoleFontMedium
        Label13.Font = My.Application.ConsoleFontMedium
    End Sub

    Private Sub Color_From_BackColorChanged(sender As Object, e As EventArgs) Handles Color_From.BackColorChanged
        Dim Color As Color = CType(sender, Panel).BackColor
        XenonAnimatedBox1.Color = Color
        XenonAnimatedBox1.Color1 = Color

        Label6.Text = Color.ReturnFormat(ColorFormat.RGB, True, Color.A < 255).Replace(" ", ", ")
        Label7.Text = Color.ReturnFormat(ColorFormat.HEX, True, Color.A < 255).Replace(" ", ", ")
        Label8.Text = Color.ReturnFormat(ColorFormat.HSL, True, Color.A < 255).Replace(" ", ", ")
        Label9.Text = Color.ReturnFormat(ColorFormat.Dec, True, Color.A < 255).Replace(" ", ", ")

        Label6.ForeColor = If(Color.IsDark, Color.White, Color.Black)
        Label7.ForeColor = Label6.ForeColor
        Label8.ForeColor = Label6.ForeColor
        Label9.ForeColor = Label6.ForeColor
    End Sub

    Private Sub Color_To_BackColorChanged(sender As Object, e As EventArgs) Handles Color_To.BackColorChanged
        Dim Color As Color = CType(sender, Panel).BackColor
        XenonAnimatedBox1.Color2 = Color

        Label13.Text = Color.ReturnFormat(ColorFormat.RGB, True, Color.A < 255).Replace(" ", ", ")
        Label12.Text = Color.ReturnFormat(ColorFormat.HEX, True, Color.A < 255).Replace(" ", ", ")
        Label11.Text = Color.ReturnFormat(ColorFormat.HSL, True, Color.A < 255).Replace(" ", ", ")
        Label10.Text = Color.ReturnFormat(ColorFormat.Dec, True, Color.A < 255).Replace(" ", ", ")

        Label10.ForeColor = If(Color.IsDark, Color.White, Color.Black)
        Label11.ForeColor = Label10.ForeColor
        Label12.ForeColor = Label10.ForeColor
        Label13.ForeColor = Label10.ForeColor
    End Sub
End Class