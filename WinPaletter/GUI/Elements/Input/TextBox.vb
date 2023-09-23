Imports System.ComponentModel
Imports System.Drawing.Drawing2D
Imports System.Drawing.Text

Namespace UI.WP

    <Description("Themed TextBox for WinPaletter UI")> <DefaultEvent("TextChanged")> Public Class TextBox : Inherits Control

        Sub New()
        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint Or
                 ControlStyles.ResizeRedraw Or ControlStyles.SupportsTransparentBackColor, True)
        DoubleBuffered = True

        ForeColor = Color.White

        TB = New Windows.Forms.TextBox
        If My.Style.DarkMode Then BackColor = Color.FromArgb(55, 55, 55) Else BackColor = Color.FromArgb(225, 225, 225)
        If My.Style.DarkMode Then TB.BackColor = Color.FromArgb(55, 55, 55) Else TB.BackColor = Color.FromArgb(225, 225, 225)
        TB.Font = New Font("Segoe UI", 9)
        TB.Text = Text
        TB.ForeColor = Color.White
        TB.MaxLength = _MaxLength
        TB.Multiline = _Multiline
        TB.ReadOnly = _ReadOnly
        TB.UseSystemPasswordChar = _UseSystemPasswordChar
        TB.BorderStyle = BorderStyle.None
        TB.Location = New Point(1, 0)
        TB.Width = Width - 3
        TB.Cursor = Cursors.IBeam

        TB.ScrollBars = Scrollbars
        TB.WordWrap = WordWrap

        If _Multiline Then
            TB.Height = Height - 8
        Else
            Height = TB.Height + 8
        End If

        AddHandler TB.TextChanged, AddressOf OnBaseTextChanged
        AddHandler TB.KeyDown, AddressOf OnBaseKeyDown
        AddHandler TB.KeyPress, AddressOf OnKeyPress

    End Sub

#Region "Variables"

    Private State As MouseState = MouseState.None
        Private WithEvents TB As Windows.Forms.TextBox

        Enum MouseState As Byte
            None = 0
            Over = 1
            Down = 2
            Block = 3
        End Enum

#End Region

#Region "Properties"

#Region "TextBox Properties"
        Private _TextAlign As HorizontalAlignment = HorizontalAlignment.Left

        <Category("Options")>
        Property TextAlign() As HorizontalAlignment
            Get
                Return _TextAlign
            End Get
            Set(value As HorizontalAlignment)
                _TextAlign = value
                If TB IsNot Nothing Then
                    TB.TextAlign = value
                End If
            End Set
        End Property

        Private _MaxLength As Integer = 32767

        <Category("Options")>
        Property MaxLength() As Integer
            Get
                Return _MaxLength
            End Get
            Set(value As Integer)
                _MaxLength = value
                If TB IsNot Nothing Then
                    TB.MaxLength = value
                End If
            End Set
        End Property

        Private _ReadOnly As Boolean

        <Category("Options")>
        Property [ReadOnly]() As Boolean
            Get
                Return _ReadOnly
            End Get
            Set(value As Boolean)
                _ReadOnly = value
                If TB IsNot Nothing Then
                    TB.ReadOnly = value
                End If
            End Set
        End Property

        Private _UseSystemPasswordChar As Boolean

        <Category("Options")>
        Property UseSystemPasswordChar() As Boolean
            Get
                Return _UseSystemPasswordChar
            End Get
            Set(value As Boolean)
                _UseSystemPasswordChar = value
                If TB IsNot Nothing Then
                    TB.UseSystemPasswordChar = value
                End If
            End Set
        End Property

        Private _Multiline As Boolean

        <Category("Options")>
        Property Multiline() As Boolean
            Get
                Return _Multiline
            End Get
            Set(value As Boolean)
                _Multiline = value
                If TB IsNot Nothing Then
                    TB.Multiline = value

                    If value Then
                        TB.Height = Height - 8
                    Else
                        Height = TB.Height + 8
                    End If

                End If
            End Set
        End Property

        <Browsable(True)>
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
        <EditorBrowsable(EditorBrowsableState.Always)>
        <Editor(GetType(System.ComponentModel.Design.MultilineStringEditor), GetType(System.Drawing.Design.UITypeEditor))>
        <Bindable(True)>
        <Category("Options")>
        Overrides Property Text As String
            Get
                Return MyBase.Text
            End Get
            Set(value As String)
                MyBase.Text = value
                If TB IsNot Nothing Then
                    TB.Text = value
                End If
            End Set
        End Property

        <Category("Options")>
        Overrides Property Font As Font
            Get
                Return MyBase.Font
            End Get
            Set(value As Font)
                MyBase.Font = value
                If TB IsNot Nothing Then
                    TB.Font = value
                    TB.Location = New Point(3, 4)
                    TB.Width = Width - 6

                    If Not _Multiline Then
                        Height = TB.Height + 8
                    End If
                End If
            End Set
        End Property

        Protected Overrides Sub OnCreateControl()
            MyBase.OnCreateControl()
            If Not Controls.Contains(TB) Then
                Controls.Add(TB)
            End If
        End Sub

        Private Sub OnBaseTextChanged(s As Object, e As EventArgs)
            Text = TB.Text
        End Sub

        Private Sub OnBaseKeyDown(s As Object, e As KeyEventArgs)
            If e.Control AndAlso e.KeyCode = Keys.A Then
                TB.SelectAll()
                e.SuppressKeyPress = True
            End If
            If e.Control AndAlso e.KeyCode = Keys.C Then
                TB.Copy()
                e.SuppressKeyPress = True
            End If
        End Sub

        Public Event KeyboardPress(s As Object, e As KeyPressEventArgs)

        Public Overloads Sub OnKeyPress(s As Object, e As KeyPressEventArgs)
            RaiseEvent KeyboardPress(s, e)
        End Sub

        Protected Overrides Sub OnResize(e As EventArgs)
            TB.Location = New Point(4, 4)
            TB.Width = Width - 14

            If _Multiline Then
                TB.Height = Height - 8
            Else
                Height = TB.Height + 8
            End If

            MyBase.OnResize(e)
        End Sub
#End Region

#Region "Other Properties"
        Private _Scrollbars As Windows.Forms.ScrollBars = ScrollBars.None
        Public Property Scrollbars As Windows.Forms.ScrollBars
            Get
                Return _Scrollbars
            End Get
            Set(value As Windows.Forms.ScrollBars)
                _Scrollbars = value
                TB.ScrollBars = value
            End Set
        End Property

        Private _WordWrap As Boolean = True
        Public Property WordWrap As Boolean
            Get
                Return _WordWrap
            End Get
            Set(value As Boolean)
                _WordWrap = value
                TB.WordWrap = value
            End Set
        End Property

        Public Property SelectionStart As Integer
            Get
                Return TB.SelectionStart
            End Get
            Set(value As Integer)
                TB.SelectionStart = CInt(value)
            End Set
        End Property

        Public Property SelectionLength As Integer
            Get
                Return TB.SelectionLength
            End Get
            Set(value As Integer)
                TB.SelectionLength = CInt(value)
            End Set
        End Property

        Public Property SelectedText As String
            Get
                Return TB.SelectedText
            End Get
            Set(value As String)
                TB.SelectedText = CStr(value)
            End Set
        End Property
#End Region

#End Region

#Region "Events"
        Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
            MyBase.OnMouseDown(e)
            State = MouseState.Down
            _Shown = True
            Tmr.Enabled = True
            Tmr.Start()
            Invalidate()
        End Sub

        Protected Overrides Sub OnMouseUp(e As MouseEventArgs)
            MyBase.OnMouseUp(e)
            State = MouseState.Over
            _Shown = True
            Tmr.Enabled = True
            Tmr.Start()
            TB.Focus() : Invalidate()
        End Sub

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

        Private Sub TB_MouseDown(sender As Object, e As MouseEventArgs) Handles TB.MouseDown
            State = MouseState.Down
            _Shown = True
            Tmr.Enabled = True
            Tmr.Start()
            Invalidate()
        End Sub

        Private Sub TB_MouseEnter(sender As Object, e As EventArgs) Handles TB.MouseEnter, TB.MouseUp
            State = MouseState.Over
            _Shown = True
            Tmr.Enabled = True
            Tmr.Start()
            Invalidate()
        End Sub

        Private Sub TB_MouseLeave(sender As Object, e As EventArgs) Handles TB.MouseLeave
            State = MouseState.None
            _Shown = True
            Tmr.Enabled = True
            Tmr.Start()
            Invalidate()
        End Sub

        Private Sub TB_LostFocus(sender As Object, e As EventArgs) Handles TB.LostFocus
            State = MouseState.None
            Invalidate()
        End Sub
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

        Protected Overrides Sub OnHandleCreated(e As EventArgs)
            Try
                If Not DesignMode Then
                    MyBase.OnHandleCreated(e)
                    alpha = 0
                End If
            Catch
            End Try
        End Sub

        Protected Overrides ReadOnly Property CreateParams() As CreateParams
            Get
                Dim cpar As CreateParams = MyBase.CreateParams
                If DrawOnGlass And Not DesignMode Then
                    cpar.ExStyle = cpar.ExStyle Or &H20
                    Return cpar
                Else
                    Return cpar
                End If
            End Get
        End Property

        Public Property DrawOnGlass As Boolean = False

        Private ActiveTTLColor As Color

        Protected Overrides Sub OnPaint(e As PaintEventArgs)
            Dim G As Graphics = e.Graphics
            DoubleBuffered = True
            G.SmoothingMode = SmoothingMode.AntiAlias
            G.TextRenderingHint = TextRenderingHint.SystemDefault

            MyBase.OnPaint(e)

            Try
                ActiveTTLColor = New VisualStyles.VisualStyleRenderer(VisualStyles.VisualStyleElement.Window.Caption.Active).GetColor(VisualStyles.ColorProperty.TextColor).Invert
            Catch
                ActiveTTLColor = SystemColors.ActiveCaptionText
            End Try

            If Not DrawOnGlass Then
                If My.Style.DarkMode Then
                    If ForeColor <> Color.White Then ForeColor = Color.White
                Else
                    If ForeColor <> Color.Black Then ForeColor = Color.Black
                End If
            Else
                ForeColor = ActiveTTLColor
            End If

            TB.ForeColor = ForeColor

            Dim OuterRect As New Rectangle(0, 0, Width - 1, Height - 1)
            Dim InnerRect As New Rectangle(1, 1, Width - 3, Height - 3)

            Dim ParentColor As Color = MyBase.GetParentColor
            Dim LineNone, LineHovered As Color
            Dim BackNone, BackHovered As Color

            If Not DrawOnGlass Then
                LineNone = If(My.Style.DarkMode, ParentColor.Light(0.3), ParentColor.Light(0.05))
                LineHovered = My.Style.Colors.Border_Checked_Hover

                BackNone = If(My.Style.DarkMode, ParentColor.Light(0.05), ParentColor.Light(0.3))
                BackHovered = My.Style.Colors.Back_Checked
            Else
                LineNone = If(Not ActiveTTLColor.IsDark, ParentColor.Light(0.3), ParentColor.Light(0.05))
                LineHovered = My.Style.Colors.Border_Checked_Hover

                BackNone = If(Not ActiveTTLColor.IsDark, ParentColor.Light(0.05), ParentColor.Light(0.3))
                BackHovered = My.Style.Colors.Back_Checked
            End If

            Dim FadeInColor As Color = Color.FromArgb(alpha, LineHovered)
            Dim FadeOutColor As Color = Color.FromArgb(255 - alpha, LineNone)

            If DrawOnGlass Then
                G.Clear(Color.Transparent)
            Else
                G.Clear(ParentColor)
            End If

            If TB.Focused Or Focused Then
                If Not DrawOnGlass Then
                    Using br As New SolidBrush(BackHovered) : G.FillRoundedRect(br, OuterRect) : End Using
                    Using P As New Pen(LineHovered) : G.DrawRoundedRect_LikeW11(P, OuterRect) : End Using
                    TB.BackColor = BackHovered
                Else
                    Using P As New Pen(LineHovered) : G.DrawRoundedRect_LikeW11(P, OuterRect) : End Using
                    TB.BackColor = ParentColor
                End If

            Else
                If Not DrawOnGlass Then
                    Using br As New SolidBrush(BackNone) : G.FillRoundedRect(br, InnerRect) : End Using
                    Using br As New SolidBrush(Color.FromArgb(alpha, BackNone)) : G.FillRoundedRect(br, OuterRect) : End Using
                    Using P As New Pen(FadeInColor) : G.DrawRoundedRect_LikeW11(P, OuterRect) : End Using
                    Using P As New Pen(FadeOutColor) : G.DrawRoundedRect_LikeW11(P, InnerRect) : End Using
                    TB.BackColor = BackNone
                Else
                    Using P As New Pen(FadeInColor.CB(0.1)) : G.DrawRoundedRect_LikeW11(P, OuterRect) : End Using
                    Using P As New Pen(FadeOutColor.CB(0.1)) : G.DrawRoundedRect_LikeW11(P, InnerRect) : End Using
                    TB.BackColor = ParentColor
                End If
            End If


        End Sub

        Private Sub TextBox_HandleCreated(sender As Object, e As EventArgs) Handles Me.HandleCreated
            alpha = 0
            If Not DesignMode Then
                Try
                    AddHandler FindForm.Load, AddressOf Loaded
                    AddHandler FindForm.Shown, AddressOf Showed
                Catch
                End Try
            End If
        End Sub

        Private Sub TextBox_HandleDestroyed(sender As Object, e As EventArgs) Handles Me.HandleDestroyed
            If Not DesignMode Then
                Try
                    RemoveHandler FindForm.Load, AddressOf Loaded
                    RemoveHandler FindForm.Shown, AddressOf Showed
                Catch
                End Try
            End If
        End Sub

        Private _Shown As Boolean = False

        Sub Loaded()
            _Shown = False
        End Sub

        Sub Showed()
            _Shown = True
        End Sub


    End Class

End Namespace
