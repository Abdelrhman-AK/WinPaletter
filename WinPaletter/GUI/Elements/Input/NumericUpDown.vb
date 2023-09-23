Imports System.ComponentModel
Imports System.Drawing.Drawing2D
Imports System.Drawing.Text

Namespace UI.WP

    <Description("Themed NumericUpDown for WinPaletter UI")> Public Class NumericUpDown : Inherits Control

        Public Event ValueChanged(sender As Object, e As EventArgs)

        Sub New()
            Enabled = True
            DoubleBuffered = True
        End Sub

#Region "Properties"
        Private _Min As Integer
        Private _Max As Integer = 100
        Private IsEnabled As Boolean
        Public Property UpDownStep As Integer = 1
        Private _Value As Integer

        Public Property Value As Integer
            Get
                Return _Value
            End Get
            Set(value As Integer)
                Select Case value
                    Case Is > Max
                        value = Max
                        Invalidate()

                    Case Is < Min
                        value = Min
                        Invalidate()
                End Select
                _Value = value
                Invalidate()
                RaiseEvent ValueChanged(Me, EventArgs.Empty)
            End Set
        End Property

        Public Property Max As Integer
            Get
                Return _Max
            End Get
            Set(value As Integer)
                Select Case value
                    Case Is < _Value
                        _Value = value
                End Select
                _Max = value
                Invalidate()
            End Set
        End Property

        Public Property Min As Integer
            Get
                Return _Min
            End Get
            Set(value As Integer)
                Select Case value
                    Case Is > _Value
                        _Value = value
                End Select
                _Min = value
                Invalidate()
            End Set
        End Property

        Public Shadows Property Enabled As Boolean
            Get
                Return EnabledCalc
            End Get
            Set(value As Boolean)
                IsEnabled = value
                Invalidate()
            End Set
        End Property

        <DisplayName("Enabled")>
        Public Property EnabledCalc As Boolean
            Get
                Return IsEnabled
            End Get
            Set(value As Boolean)
                Enabled = value
                Invalidate()
            End Set
        End Property

        <Browsable(True)>
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
        <EditorBrowsable(EditorBrowsableState.Always)>
        <Editor(GetType(System.ComponentModel.Design.MultilineStringEditor), GetType(System.Drawing.Design.UITypeEditor))>
        <Bindable(True)>
        Public Overrides Property Text As String = ""
#End Region

#Region "Animator"
        Dim alpha As Integer
        ReadOnly Factor As Integer = 20
        Dim WithEvents Tmr As New Timer With {.Enabled = False, .Interval = 1}

        Private Sub Tmr_Tick(sender As Object, e As EventArgs) Handles Tmr.Tick
            If Not DesignMode Then

                If State = MouseState.Over Then
                    If alpha + Factor <= 255 Then
                        alpha += Factor
                    ElseIf alpha + Factor > 255 Then
                        alpha = 255
                        Tmr.Enabled = False
                        Tmr.Stop()
                    End If

                    If _Shown Then
                        Threading.Thread.Sleep(1)
                        Invalidate()
                    End If
                End If

                If Not State = MouseState.Over Then
                    If alpha - Factor >= 0 Then
                        alpha -= Factor
                    ElseIf alpha - Factor < 0 Then
                        alpha = 0
                        Tmr.Enabled = False
                        Tmr.Stop()
                    End If

                    If _Shown Then
                        Threading.Thread.Sleep(1)
                        Invalidate()
                    End If
                End If
            End If
        End Sub
#End Region

#Region "Events"
        Protected Overrides Sub OnMouseEnter(e As EventArgs)
            MyBase.OnMouseEnter(e)
            State = MouseState.Over
            _Shown = True
            Tmr.Enabled = True
            Tmr.Start()
            Invalidate()
        End Sub

        Protected Overrides Sub OnMouseLeave(e As EventArgs)
            MyBase.OnMouseLeave(e)
            State = MouseState.None
            _Shown = True
            Tmr.Enabled = True
            Tmr.Start()
            Invalidate()
        End Sub
        Protected Overrides Sub OnMouseUp(e As MouseEventArgs)
            State = MouseState.Over
            _Shown = True
            Tmr.Enabled = True
            Tmr.Start()

            MyBase.OnMouseUp(e)

            If Enabled Then
                If SideRect.Contains(e.Location) And e.Y < 10 Then
                    Value += UpDownStep
                ElseIf SideRect.Contains(e.Location) And e.Y > 10 Then
                    Value -= UpDownStep
                End If
            End If
        End Sub

        Private Sub NumericUpDown_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
            State = MouseState.Down
            _Shown = True

            If Enabled And SideRect.Contains(e.Location) Then
                Tmr.Enabled = True
                Tmr.Start()
            End If
        End Sub

        Protected Overrides Sub OnResize(e As EventArgs)
            Me.Invalidate()
        End Sub

        Enum MouseState
            None
            Over
            Down
        End Enum

        Dim State As MouseState = MouseState.None

        Private Sub NumericUpDown_HandleCreated(sender As Object, e As EventArgs) Handles Me.HandleCreated
            alpha = 0

            If Not DesignMode Then
                Try
                    AddHandler FindForm.Load, AddressOf Loaded
                    AddHandler FindForm.Shown, AddressOf Showed
                    AddHandler Parent.BackColorChanged, AddressOf RefreshColorPalette
                Catch
                End Try
            End If
        End Sub

        Private Sub NumericUpDown_HandleDestroyed(sender As Object, e As EventArgs) Handles Me.HandleDestroyed
            If Not DesignMode Then
                Try
                    RemoveHandler FindForm.Load, AddressOf Loaded
                    RemoveHandler FindForm.Shown, AddressOf Showed
                    RemoveHandler Parent.BackColorChanged, AddressOf RefreshColorPalette
                Catch
                End Try
            End If
        End Sub

        Sub Loaded()
            _Shown = False
        End Sub

        Sub Showed()
            _Shown = True

            Invalidate()
        End Sub

        Public Sub RefreshColorPalette()
            If _Shown Then

                Invalidate()
            End If
        End Sub

        Private _Shown As Boolean = False
#End Region

        Dim SideRect As New Rectangle

        Protected Overrides Sub OnPaint(e As PaintEventArgs)
            Dim G As Graphics = e.Graphics
            G.SmoothingMode = SmoothingMode.AntiAlias
            G.TextRenderingHint = If(DesignMode, TextRenderingHint.ClearTypeGridFit, TextRenderingHint.SystemDefault)
            DoubleBuffered = True
            Dim RTL As Boolean = (RightToLeft = 1)

            '################################################################################# Customizer
            Dim OuterRect As New Rectangle(0, 0, Width - 1, Height - 1)
            Dim InnerRect As New Rectangle(1, 1, Width - 3, Height - 3)
            SideRect = New Rectangle(Width - 16, 0, 15, Height)

            If RTL Then
                OuterRect.X = Width - OuterRect.X - OuterRect.Width
                InnerRect.X = Width - InnerRect.X - InnerRect.Width
                SideRect.X = Width - SideRect.X - SideRect.Width
            End If

            '#################################################################################

            G.Clear(GetParentColor)

            Using br As New SolidBrush(Color.FromArgb(255 - alpha, My.Style.Colors.Back)) : G.FillRoundedRect(br, OuterRect) : End Using
            Using br As New SolidBrush(Color.FromArgb(alpha, My.Style.Colors.Back_Checked)) : G.FillRoundedRect(br, OuterRect) : End Using
            Using br As New SolidBrush(Color.FromArgb(alpha, My.Style.Colors.Border_Checked_Hover)) : G.FillRoundedRect(br, SideRect) : End Using

            Using P As New Pen(Color.FromArgb(255 - alpha, My.Style.Colors.Border)) : G.DrawRoundedRect_LikeW11(P, InnerRect) : End Using
            Using P As New Pen(Color.FromArgb(alpha, My.Style.Colors.Border_Checked_Hover)) : G.DrawRoundedRect_LikeW11(P, OuterRect) : End Using

            If Focused And State = MouseState.None Then
                Using P As New Pen(Color.FromArgb(255, My.Style.Colors.Border_Checked_Hover)) : G.DrawRoundedRect(P, InnerRect) : End Using
            End If

            Using TextColor As New SolidBrush(If(My.Style.DarkMode, Color.White, Color.Black)), TextFont As New Font("Segoe UI", 9)
                G.DrawString(CStr(Value), TextFont, TextColor, New Rectangle(0, 0, Width - 15, Height), ContentAlignment.MiddleCenter.ToStringFormat)
            End Using

            Using SignColor As New SolidBrush(My.Style.Colors.Back_Checked), SignFont As New Font("Marlett", 11)
                G.DrawString("t", SignFont, SignColor, New Point(SideRect.Left - 1, 0))
                G.DrawString("u", SignFont, SignColor, New Point(SideRect.Left - 1, Height - 16))
            End Using
        End Sub

    End Class

End Namespace

