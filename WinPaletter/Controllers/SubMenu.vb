Imports AnimatorNS
Imports WinPaletter.XenonCore
Imports WinPaletter.NativeMethods
Imports System.Reflection

Public Class SubMenu

    Private _shown As Boolean
    Private _overrideColor As Color
    Private _eventDone As Boolean
    Private _Speed As Integer = 20
    Private _dark As Single = 0.7

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


    Public Function ShowMenu(ColorHandle As XenonCP, Optional EnableDelete As Boolean = False) As Color
        XenonButton5.Visible = EnableDelete

        MainColor.BackColor = ColorHandle.BackColor.CB((XenonTrackbar1.Value - 100) / 100)
        DefaultColor.BackColor = ColorHandle.DefaultColor.CB((XenonTrackbar2.Value - 100) / 100)
        InvertedColor.BackColor = ColorHandle.BackColor.Invert.CB((XenonTrackbar3.Value - 100) / 100)

        MainColor.DefaultColor = ColorHandle.BackColor
        DefaultColor.DefaultColor = ColorHandle.DefaultColor
        InvertedColor.DefaultColor = ColorHandle.BackColor.Invert

        If ShowDialog() = DialogResult.OK Then
            Select Case My.Application.ColorEvent
                Case My.MyApplication.MenuEvent.Copy
                    My.Application.CopiedColor = MainColor.BackColor
                    Return ColorHandle.BackColor

                Case My.MyApplication.MenuEvent.Cut
                    My.Application.CopiedColor = MainColor.BackColor
                    ColorHandle.BackColor = Color.Black

                Case My.MyApplication.MenuEvent.Paste
                    ColorHandle.BackColor = My.Application.CopiedColor
                    Return My.Application.CopiedColor

                Case My.MyApplication.MenuEvent.Override
                    ColorHandle.BackColor = _overrideColor
                    Return ColorHandle.BackColor

                Case My.MyApplication.MenuEvent.Delete
                    ColorHandle.BackColor = Color.FromArgb(0, 0, 0, 0)
                    Return ColorHandle.BackColor

                Case My.MyApplication.MenuEvent.None
                    Return ColorHandle.BackColor

            End Select
        Else
            My.Application.ColorEvent = My.MyApplication.MenuEvent.None
            Return Nothing
        End If
    End Function

    Private Sub SubMenu_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        _shown = True

        PaletteContainer.Visible = False
    End Sub

    Private Sub SubMenu_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        User32.AnimateWindow(Handle, _Speed, User32.AnimateWindowFlags.AW_HIDE Or User32.AnimateWindowFlags.AW_BLEND)
    End Sub

    Sub HandleDeactivate()
        If _shown Then
            _shown = False
            If Not _eventDone Then DialogResult = DialogResult.None
            Me.Close()
        End If
    End Sub

    Sub Collapse_Expand()

        XenonButton4.Visible = False

        Select Case PaletteContainer.Visible
            Case False
                XenonButton4.Text = "<"

                For i = PaletteContainer.Left + 3 To PaletteContainer.Right + 8 Step 2
                    Width = i
                Next

                Width = PaletteContainer.Right + 8

                PaletteContainer.Visible = True
                XenonComboBox1.Visible = True

            Case True
                XenonButton4.Text = ">"

                PaletteContainer.Visible = False
                XenonComboBox1.Visible = False

                For i = PaletteContainer.Right + 8 To PaletteContainer.Left + 3 Step -2
                    Width = i
                Next

                Width = PaletteContainer.Left + 3

        End Select

        XenonButton4.Visible = True
    End Sub

    Private Sub SubMenu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _shown = False

        Dim p As Point = MousePosition

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

        Width = PaletteContainer.Left + 3

        PaletteContainer.Visible = False
        XenonComboBox1.Visible = False
        XenonButton4.Text = ">"

        ApplyDarkMode(Me)

        XenonComboBox1.SelectedIndex = 0

        GetColorsFromPalette(MainFrm.CP)

        If My.Application.CopiedColor = Nothing Then

            XenonButton3.Enabled = False

            Try
                If Clipboard.GetData("Text") IsNot Nothing Then
                    Dim s As String = Clipboard.GetData("Text").ToString.ToLower

                    If s.StartsWith("color ") Then
                        Dim C As Color = Color.FromArgb(255, 0, 0, 0)
                        s = s.Remove(0, "color ".Count)
                        s = s.Replace("[", "")
                        s = s.Replace("]", "")
                        s = s.Replace(" ", "")

                        For Each x As String In s.Split(",")
                            Dim i As Integer = Val(x.Remove(0, 2))
                            If x.StartsWith("a=") Then C = Color.FromArgb(i, C)
                            If x.StartsWith("r=") Then C = Color.FromArgb(C.A, i, C.G, C.B)
                            If x.StartsWith("g=") Then C = Color.FromArgb(C.A, C.R, i, C.B)
                            If x.StartsWith("b=") Then C = Color.FromArgb(C.A, C.R, C.G, i)
                        Next

                        My.Application.CopiedColor = C
                        XenonButton3.Enabled = True
                    End If
                End If

            Catch
                XenonButton3.Enabled = False
            End Try

        Else
            XenonButton3.Enabled = True
        End If


        BackColor = If(GetDarkMode(), MainColor.BackColor.Dark(_dark), MainColor.BackColor.LightLight)

        User32.AnimateWindow(Handle, _Speed, User32.AnimateWindowFlags.AW_ACTIVATE Or User32.AnimateWindowFlags.AW_BLEND)

        Invalidate()
    End Sub

    Sub Pnl_Click(sender As Object, e As EventArgs)
        MainColor.BackColor = sender.BackColor
        MainColor.DefaultColor = sender.BackColor

        InvertedColor.BackColor = MainColor.BackColor.Invert.CB((XenonTrackbar3.Value - 100) / 100)
        InvertedColor.DefaultColor = MainColor.BackColor.Invert

        'BackColor = If(GetDarkMode(), MainColor.BackColor.Dark(_dark), MainColor.BackColor.LightLight)

        Collapse_Expand()
    End Sub

    Sub GetColorsFromPalette(CP As CP)
        PaletteContainer.SuspendLayout()

        For Each c As XenonCP In PaletteContainer.Controls.OfType(Of XenonCP)
            c.Dispose()
            PaletteContainer.Controls.Remove(c)
        Next

        PaletteContainer.Controls.Clear()

        For Each c As Color In CP.ListColors
            Dim pnl As New XenonCP With {
                .Size = New Size(If(My.Application._Settings.Nerd_Stats, 85, 30), 20),
                .BackColor = c
            }
            PaletteContainer.Controls.Add(pnl)
            AddHandler pnl.Click, AddressOf Pnl_Click
        Next

        PaletteContainer.ResumeLayout()
    End Sub

    Sub Update_Variants()
        InvertedColor.DefaultColor = MainColor.DefaultColor.Invert
    End Sub

    Private Sub XenonButton1_Click(sender As Object, e As EventArgs) Handles XenonButton1.Click
        Clipboard.SetData("Text", MainColor.BackColor)
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

    Private Sub MainColor_Click(sender As Object, e As EventArgs) Handles MainColor.Click, InvertedColor.Click,
             DefaultColor.Click

        _eventDone = True
        My.Application.ColorEvent = My.MyApplication.MenuEvent.Override
        _overrideColor = sender.BackColor
        DialogResult = DialogResult.OK
        Close()
    End Sub

    Private Sub XenonButton4_Click(sender As Object, e As EventArgs) Handles XenonButton4.Click
        Collapse_Expand()
    End Sub

    Private Sub XenonComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles XenonComboBox1.SelectedIndexChanged
        If _shown Then
            Select Case XenonComboBox1.SelectedIndex
                Case 0
                    GetColorsFromPalette(MainFrm.CP)
                Case 1
                    GetColorsFromPalette(New CP_Defaults().Default_Windows11)
                Case 2
                    GetColorsFromPalette(New CP_Defaults().Default_Windows10)
                Case 3
                    GetColorsFromPalette(New CP_Defaults().Default_Windows8)
                Case 4
                    GetColorsFromPalette(New CP_Defaults().Default_Windows7)
                Case Else
                    GetColorsFromPalette(MainFrm.CP)
            End Select
        End If
    End Sub

    Private Sub XenonButton5_Click(sender As Object, e As EventArgs) Handles XenonButton5.Click
        _eventDone = True
        My.Application.ColorEvent = My.MyApplication.MenuEvent.Delete
        DialogResult = DialogResult.OK
        Close()
    End Sub

    Private Sub XenonTrackbar1_Scroll(sender As Object) Handles XenonTrackbar1.Scroll
        MainColor.BackColor = MainColor.DefaultColor.CB((XenonTrackbar1.Value - 100) / 100)
    End Sub

    Private Sub XenonTrackbar2_Scroll(sender As Object) Handles XenonTrackbar2.Scroll
        DefaultColor.BackColor = DefaultColor.DefaultColor.CB((XenonTrackbar2.Value - 100) / 100)
    End Sub

    Private Sub XenonTrackbar3_Scroll(sender As Object) Handles XenonTrackbar3.Scroll
        InvertedColor.BackColor = InvertedColor.DefaultColor.CB((XenonTrackbar3.Value - 100) / 100)
    End Sub

    Private Sub XenonButton6_Click(sender As Object, e As EventArgs) Handles XenonButton6.Click
        XenonTrackbar1.Value = 100
    End Sub

    Private Sub XenonButton7_Click(sender As Object, e As EventArgs) Handles XenonButton7.Click
        XenonTrackbar2.Value = 100
    End Sub

    Private Sub XenonButton8_Click(sender As Object, e As EventArgs) Handles XenonButton8.Click
        XenonTrackbar3.Value = 100
    End Sub

End Class