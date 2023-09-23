Imports System.ComponentModel
Imports System.Drawing.Drawing2D
Imports System.Drawing.Text

Namespace UI.WP

    <Description("Themed Trackbar for selecting colors (HSL) for WinPaletter UI")>
    <DefaultEvent("Scroll")> Public Class ColorBar : Inherits Control

        Event Scroll(sender As Object)

#Region "Properties"
        Private _Minimum As Integer
        Property Minimum() As Integer
            Get
                Return _Minimum
            End Get
            Set(value As Integer)
                _Minimum = value
                If value > _Value Then _Value = value
                If value > _Maximum Then _Maximum = value

                InvalidateLayout()
            End Set
        End Property

        Private _Maximum As Integer = 100
        Property Maximum() As Integer
            Get
                Return _Maximum
            End Get
            Set(value As Integer)
                _Maximum = value
                If value < _Value Then _Value = value
                If value < _Minimum Then _Minimum = value
                InvalidateLayout()
            End Set
        End Property

        Private _Value As Integer
        Property Value() As Integer
            Get

                Return _Value
            End Get
            Set(value As Integer)
                If value = _Value Then Return

                If value > _Maximum Then
                    value = _Maximum
                End If

                If value < _Minimum Then
                    value = _Minimum
                End If

                _Value = value
                InvalidatePosition()

                RaiseEvent Scroll(Me)
            End Set
        End Property

        Private _SmallChange As Integer = 1
        Public Property SmallChange() As Integer
            Get
                Return _SmallChange
            End Get
            Set(value As Integer)
                If value < 1 Then
                    Throw New Exception("Property value is not valid.")
                End If

                _SmallChange = value
            End Set
        End Property

        Private _LargeChange As Integer = 10
        Public Property LargeChange() As Integer
            Get
                Return _LargeChange
            End Get
            Set(value As Integer)
                If value < 1 Then
                    Throw New Exception("Property value is not valid.")
                End If

                _LargeChange = value
            End Set
        End Property

        <Browsable(True)>
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
        <EditorBrowsable(EditorBrowsableState.Always)>
        <Editor(GetType(System.ComponentModel.Design.MultilineStringEditor), GetType(System.Drawing.Design.UITypeEditor))>
        <Bindable(True)>
        Public Overrides Property Text As String = ""
#End Region

        Enum ModesList
            Hue
            Saturation
            Light
        End Enum

        Public Property Mode As ModesList = ModesList.Hue
        ReadOnly ButtonSize As Integer = 0
        ReadOnly ThumbSize As Integer = 35 ' 14 minimum
        Private LSA As Rectangle
        Private RSA As Rectangle
        Private Shaft As Rectangle
        Private Thumb As Rectangle
        Private ThumbDown As Boolean
        Private Circle As Rectangle
        Private _Shown As Boolean = False

        Enum MouseState
            None
            Over
            Down
        End Enum

#Region "Animator"
        Dim alpha As Integer
        ReadOnly Factor As Integer = 25
        Dim WithEvents Tmr As New Timer With {.Enabled = False, .Interval = 1}

        Private Sub Tmr_Tick(sender As Object, e As EventArgs) Handles Tmr.Tick
            If Not DesignMode Then

                If State = MouseState.Over And _Shown Then
                    If alpha + Factor <= 255 Then
                        alpha += Factor
                    ElseIf alpha + Factor > 255 Then
                        alpha = 255
                        Tmr.Enabled = False
                        Tmr.Stop()
                    End If

                    Threading.Thread.Sleep(1)
                    Invalidate()
                End If

                If _Shown And (Not State = MouseState.Over Or State = MouseState.Down) Then
                    If alpha - Factor >= 0 Then
                        alpha -= Factor
                    ElseIf alpha - Factor < 0 Then
                        alpha = 0
                        Tmr.Enabled = False
                        Tmr.Stop()
                    End If

                    Threading.Thread.Sleep(1)
                    Invalidate()
                End If
            End If
        End Sub
#End Region

        Public State As MouseState = MouseState.None

        Sub New()
            SetStyle(DirectCast(139286, ControlStyles), True)
            SetStyle(ControlStyles.Selectable, False)
            Height = 19
            Text = ""
        End Sub

        Dim I1 As Integer

        Private _AccentColor As Color = My.Style.Colors.Core
        Public Property AccentColor As Color
            Get
                Return _AccentColor
            End Get
            Set(value As Color)
                _AccentColor = value
                Refresh()
            End Set
        End Property


        Private _H As Integer = 0
        Public Property H As Integer
            Get
                Return _H
            End Get
            Set(value As Integer)
                _H = value
                Refresh()
            End Set
        End Property


        Private _S As Single = 1
        Public Property S As Single
            Get
                Return _S
            End Get
            Set(value As Single)
                _S = value
                Refresh()
            End Set
        End Property


        Private _L As Integer = 0.5
        Public Property L As Single
            Get
                Return _L
            End Get
            Set(value As Single)
                _L = value
                Refresh()
            End Set
        End Property

        Private ReadOnly Color1 As Color = Color.FromArgb(255, 23, 0)
        Private ReadOnly Color2 As Color = Color.FromArgb(253, 253, 0)
        Private ReadOnly Color3 As Color = Color.FromArgb(58, 255, 0)
        Private ReadOnly Color4 As Color = Color.FromArgb(0, 255, 240)
        Private ReadOnly Color5 As Color = Color.FromArgb(0, 17, 255)
        Private ReadOnly Color6 As Color = Color.FromArgb(255, 0, 212)
        Private ReadOnly Color7 As Color = Color.FromArgb(255, 0, 23)

        ReadOnly cb_H As New ColorBlend() With {.Positions = {0, 1 / 6.0F, 2 / 6.0F, 3 / 6.0F, 4 / 6.0F, 5 / 6.0F, 1},
                        .Colors = {Color1, Color2, Color3, Color4, Color5, Color6, Color7}}

        Dim cb_L As New ColorBlend() With {.Positions = {0, 1 / 2.0F, 1}, .Colors = {Color.Black, _AccentColor, Color.White}}

        Protected Overrides Sub OnPaint(e As System.Windows.Forms.PaintEventArgs)
            Dim G As Graphics = e.Graphics
            G.SmoothingMode = SmoothingMode.HighQuality
            G.TextRenderingHint = TextRenderingHint.AntiAliasGridFit

            G.Clear(BackColor)
            Dim Dark As Boolean = My.Style.DarkMode
            Dim c_back As Color = If(Dark, Color.FromArgb(60, 60, 60), Color.FromArgb(210, 210, 210))
            Dim c_btn As Color = If(Dark, Color.FromArgb(165, 165, 165), Color.FromArgb(100, 100, 100))
            Dim C As Color

            Dim middleRect As New Rectangle(0, (Height - (Height * 0.25)) / 2, Width - 1, Height * 0.25)

            Dim back As LinearGradientBrush

            Select Case Mode
                Case ModesList.Hue
                    back = New LinearGradientBrush(middleRect, Color.Black, Color.Black, 0, False) With {.InterpolationColors = cb_H}
                    Dim HSL_ As HSL_Structure = Color.FromArgb(0, 255, 240).ToHSL()
                    HSL_.H = (Value / Maximum) * 359
                    C = HSL_.ToRGB

                Case ModesList.Saturation
                    Dim HSL_x1 As HSL_Structure = _AccentColor.ToHSL()
                    Dim HSL_x2 As HSL_Structure = _AccentColor.ToHSL()
                    HSL_x1.S = 0
                    HSL_x2.S = 1
                    back = New LinearGradientBrush(middleRect, HSL_x1.ToRGB, HSL_x2.ToRGB, LinearGradientMode.Horizontal)
                    Dim HSL_ As HSL_Structure = _AccentColor.ToHSL()
                    HSL_.S = Value / Maximum
                    C = HSL_.ToRGB

                Case ModesList.Light
                    cb_L = New ColorBlend() With {.Positions = {0, 1 / 2.0F, 1}, .Colors = {Color.Black, _AccentColor, Color.White}}
                    back = New LinearGradientBrush(middleRect, Color.Black, Color.Black, 0, False) With {.InterpolationColors = cb_L}
                    Dim HSL_ As HSL_Structure = _AccentColor.ToHSL()
                    HSL_.L = Value / Maximum
                    C = HSL_.ToRGB

                Case Else
                    C = If(Dark, Color.FromArgb(60, 60, 60), Color.FromArgb(210, 210, 210))
                    back = New LinearGradientBrush(middleRect, C, C, GradientMode.Horizontal)

            End Select

            G.FillRoundedRect(back, middleRect)

            Circle = New Rectangle((Value / Maximum) * Shaft.Width, 0, Height - 1, Height - 1)

            Using br As New SolidBrush(BackColor) : G.FillRectangle(br, New Rectangle(-1, 0, 4, Height)) : End Using

            Using br As New SolidBrush(BackColor) : G.FillRectangle(br, New Rectangle(Width - 4, 0, 4, Height)) : End Using

            Using br As New SolidBrush(My.Style.Colors.Border) : G.FillEllipse(br, Circle) : End Using

            Dim smallC1 As New Rectangle(Circle.X + 5, Circle.Y + 5, Circle.Width - 10, Circle.Height - 10)
            Dim smallC2 As New Rectangle(Circle.X + 4, Circle.Y + 4, Circle.Width - 8, Circle.Height - 8)

            Using br As New SolidBrush(C) : G.FillEllipse(br, smallC1) : End Using
            Using br As New SolidBrush(Color.FromArgb(alpha, C)) : G.FillEllipse(br, smallC2) : End Using
        End Sub

        Protected Overrides Sub OnSizeChanged(e As EventArgs)
            Height = 19
            InvalidateLayout()
        End Sub

        Private Sub InvalidateLayout()
            LSA = New Rectangle(0, 0, ButtonSize, Height)
            RSA = New Rectangle(Width - ButtonSize, 0, ButtonSize, Height)
            Shaft = New Rectangle(LSA.Right + 1 + 0.5 * Height, 0, Width - Height - 1, Height)
            Thumb = New Rectangle(0, 1, (Value / Maximum) * Shaft.Width, Height - 3)
            Circle = New Rectangle((Value / Maximum) * Shaft.Width, 0, Height - 1, Height - 1)
            RaiseEvent Scroll(Me)
            InvalidatePosition()
        End Sub

        Private Sub InvalidatePosition()
            Thumb.Width = (Value / Maximum) * Width
            Refresh()
        End Sub

        Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
            If e.Button = Windows.Forms.MouseButtons.Left Then
                State = MouseState.Down
                Tmr.Enabled = True
                Tmr.Start()

                If Circle.Contains(e.Location) Then
                    ThumbDown = True
                    Return
                Else
                    If e.X < Circle.X Then

                        I1 = _Value - _LargeChange
                    Else
                        I1 = _Value + _LargeChange
                    End If
                End If

                Value = Math.Min(Math.Max(I1, _Minimum), _Maximum)

                InvalidatePosition()
            End If
        End Sub

        Protected Overrides Sub OnMouseMove(e As MouseEventArgs)
            If Circle.Contains(e.Location) And Not e.Button = MouseButtons.Left Then
                State = MouseState.Over
            Else
                If e.Button = MouseButtons.Left Then State = MouseState.Down Else State = MouseState.None
            End If

            Invalidate()

            If ThumbDown Then
                Value = Math.Min(Math.Max((e.X / Width) * Maximum, _Minimum), _Maximum)
                InvalidatePosition()
            End If

            Tmr.Enabled = True
            Tmr.Start()
        End Sub

        Private Sub NSVScrollBar_MouseUp(sender As Object, e As MouseEventArgs) Handles Me.MouseUp
            ThumbDown = False
            State = MouseState.None
            Tmr.Enabled = True
            Tmr.Start()
            Invalidate()
        End Sub

        Private Sub XenonScrollBarV_MouseEnter(sender As Object, e As EventArgs) Handles Me.MouseEnter
            If Thumb.Contains(MousePosition) Then
                State = MouseState.Over
                Invalidate()
                Tmr.Enabled = True
                Tmr.Start()
            End If
        End Sub

        Private Sub XenonScrollBarV_MouseLeave(sender As Object, e As EventArgs) Handles Me.MouseLeave
            State = MouseState.None
            Tmr.Enabled = True
            Tmr.Start()
            Invalidate()
        End Sub

        Private Sub XenonScrollBarV_MouseWheel(sender As Object, e As MouseEventArgs) Handles Me.MouseWheel
            If e.Delta < 0 Then
                If Value < Maximum Then
                    If e.Delta <= -240 Then Value += LargeChange Else Value += SmallChange
                End If
            Else
                If Value > Minimum Then
                    If e.Delta >= 240 Then Value -= LargeChange Else Value -= SmallChange
                End If
            End If
        End Sub

        Private Sub XenonRadioButton_HandleCreated(sender As Object, e As EventArgs) Handles Me.HandleCreated

            Try
                If Not DesignMode Then
                    AddHandler FindForm.Load, AddressOf Loaded
                    AddHandler FindForm.Shown, AddressOf Showed
                    AddHandler Parent.BackColorChanged, AddressOf RefreshColorPalette
                    AddHandler Parent.VisibleChanged, AddressOf RefreshColorPalette
                    AddHandler Parent.EnabledChanged, AddressOf RefreshColorPalette
                    AddHandler VisibleChanged, AddressOf RefreshColorPalette
                    AddHandler EnabledChanged, AddressOf RefreshColorPalette
                End If
            Catch
            End Try

            Try
                alpha = 0

            Catch
            End Try
        End Sub

        Private Sub XenonColorBar_HandleDestroyed(sender As Object, e As EventArgs) Handles Me.HandleDestroyed
            Try
                If Not DesignMode Then
                    RemoveHandler FindForm.Load, AddressOf Loaded
                    RemoveHandler FindForm.Shown, AddressOf Showed
                    RemoveHandler Parent.BackColorChanged, AddressOf RefreshColorPalette
                    RemoveHandler Parent.VisibleChanged, AddressOf RefreshColorPalette
                    RemoveHandler Parent.EnabledChanged, AddressOf RefreshColorPalette
                    RemoveHandler VisibleChanged, AddressOf RefreshColorPalette
                    RemoveHandler EnabledChanged, AddressOf RefreshColorPalette
                End If
            Catch
            End Try
        End Sub

        Sub Loaded()
            _Shown = False
        End Sub

        Sub Showed()
            _Shown = True

            Invalidate()
        End Sub

        Public Sub RefreshColorPalette()

            Invalidate()
        End Sub

    End Class

End Namespace