Imports AnimatorNS
Imports WinPaletter.XenonCore
Imports WinPaletter.NativeMethods
Public Class SubMenu

    Private _shown As Boolean
    Private _overrideColor As Color
    Private _eventDone As Boolean

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

    Public Function ShowMenu(ColorHandle As XenonGroupBox, _DefaultColor As Color) As Color
        MainColor.BackColor = ColorHandle.BackColor
        DefaultColor.BackColor = _DefaultColor

        If ShowDialog() = DialogResult.OK Then
            Select Case My.Application.ColorEvent
                Case My.MyApplication.MenuEvent.Copy
                    My.Application.CopiedColor = MainColor.BackColor
                    Return Nothing

                Case My.MyApplication.MenuEvent.Cut
                    My.Application.CopiedColor = MainColor.BackColor
                    ColorHandle.BackColor = Color.Black

                Case My.MyApplication.MenuEvent.Paste
                    ColorHandle.BackColor = My.Application.CopiedColor
                    Return My.Application.CopiedColor

                Case My.MyApplication.MenuEvent.Override
                    ColorHandle.BackColor = _overrideColor
                    Return ColorHandle.BackColor

                Case My.MyApplication.MenuEvent.None
                    Return Nothing

            End Select
        Else
            My.Application.ColorEvent = My.MyApplication.MenuEvent.None
            Return Nothing
        End If

    End Function

    Private Sub SubMenu_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        _shown = True
    End Sub

    Private Sub SubMenu_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        User32.AnimateWindow(Handle, 50, User32.AnimateWindowFlags.AW_HIDE Or User32.AnimateWindowFlags.AW_BLEND)
    End Sub

    Sub HandleDeactivate()
        If _shown Then
            _shown = False
            If Not _eventDone Then DialogResult = DialogResult.None
            Me.Close()
        End If
    End Sub

    Private LateralMargin As Integer
    Private MedialMargin As Integer

    Private Sub SubMenu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _shown = False
        Location = MousePosition

        ApplyDarkMode(Me)

        LateralMargin = Width - PaletteContainer.Right
        MedialMargin = PaletteContainer.Left + 3

        Width = MedialMargin
        PaletteContainer.Visible = False

        Update_Variants()

        For Factor As Single = 1 To -1 Step -0.01
            Dim pnl As New XenonGroupBox With {.Size = New Drawing.Size(30, 25), .CustomColor = True, .ForceNoNerd = True}
            pnl.BackColor = CCB(MainColor.BackColor, Factor)
            PaletteContainer.Controls.Add(pnl)
            AddHandler pnl.Click, AddressOf Pnl_Click
        Next

        XenonButton3.Enabled = (My.Application.CopiedColor <> Nothing)

        BackColor = If(GetDarkMode(), ControlPaint.DarkDark(MainColor.BackColor), ControlPaint.LightLight(MainColor.BackColor))

        User32.AnimateWindow(Handle, 50, User32.AnimateWindowFlags.AW_ACTIVATE Or User32.AnimateWindowFlags.AW_BLEND)

        Invalidate()
    End Sub

    Sub Pnl_Click(sender As Object, e As EventArgs)
        MainColor.BackColor = sender.BackColor
        Update_Variants()
        Collapse_Expand()
    End Sub

    Private Sub XenonButton4_Click(sender As Object, e As EventArgs) Handles XenonButton4.Click
        Collapse_Expand()
    End Sub

    Sub Update_Variants()
        LighterColor.BackColor = ControlPaint.Light(MainColor.BackColor)
        LightestColor.BackColor = ControlPaint.LightLight(MainColor.BackColor)

        DarkerColor.BackColor = ControlPaint.Dark(MainColor.BackColor)
        DarkestColor.BackColor = ControlPaint.DarkDark(MainColor.BackColor)

        InvertedColor.BackColor = InvertColor(MainColor.BackColor)
    End Sub

    Sub Collapse_Expand()
        My.Application.AnimatorX.HideSync(XenonButton4)

        Select Case PaletteContainer.Visible
            Case False
                XenonButton4.Text = "<"

                My.Application.AnimatorX.Show(PaletteContainer)

                For i = Width To PaletteContainer.Right + LateralMargin Step 2
                    Width = i
                Next

                Width = PaletteContainer.Right + LateralMargin

            Case True
                XenonButton4.Text = ">"

                My.Application.AnimatorX.Hide(PaletteContainer)

                For i = Width To MedialMargin Step -2
                    Width = i
                Next

                Width = MedialMargin

        End Select

        My.Application.AnimatorX.ShowSync(XenonButton4)
    End Sub

    Private Sub XenonButton1_Click(sender As Object, e As EventArgs) Handles XenonButton1.Click
        _eventDone = True
        My.Application.ColorEvent = My.MyApplication.MenuEvent.Copy
        DialogResult = DialogResult.OK
        Close()
    End Sub

    Private Sub XenonButton2_Click(sender As Object, e As EventArgs) Handles XenonButton2.Click
        _eventDone = True
        My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut
        DialogResult = DialogResult.OK
        Close()
    End Sub

    Private Sub XenonButton3_Click(sender As Object, e As EventArgs) Handles XenonButton3.Click
        _eventDone = True
        My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste
        DialogResult = DialogResult.OK
        Close()
    End Sub

    Private Sub MainColor_Click(sender As Object, e As EventArgs) Handles MainColor.Click, DefaultColor.Click, WhiteColor.Click, BlackColor.Click, LighterColor.Click, LightestColor.Click,
             DarkerColor.Click, DarkestColor.Click, InvertedColor.Click

        _eventDone = True
        My.Application.ColorEvent = My.MyApplication.MenuEvent.Override
        _overrideColor = sender.BackColor
        DialogResult = DialogResult.OK
        Close()
    End Sub

End Class