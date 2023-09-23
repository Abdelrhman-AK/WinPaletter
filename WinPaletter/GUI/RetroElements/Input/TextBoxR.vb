Imports System.ComponentModel
Imports System.Drawing.Drawing2D


Namespace UI.Retro
    <Description("Retro TextBox with Windows 9x style")> <DefaultEvent("TextChanged")> Public Class TextBoxR : Inherits Control

        Sub New()
            SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint Or
                 ControlStyles.ResizeRedraw Or ControlStyles.OptimizedDoubleBuffer Or ControlStyles.SupportsTransparentBackColor, True)
            DoubleBuffered = True
            ForeColor = Color.Black
            BackColor = Color.White
            TB = New Windows.Forms.TextBox With {.Visible = True}
            Font = New Font("Microsoft Sans Serif", 8)
            TB.Text = Text
            TB.ForeColor = Color.White
            TB.MaxLength = _MaxLength
            TB.Multiline = _Multiline
            TB.ReadOnly = _ReadOnly
            TB.UseSystemPasswordChar = _UseSystemPasswordChar
            TB.BorderStyle = BorderStyle.None
            TB.Location = New Point(1, 0)
            TB.Width = Width - 1
            _Style = RoundingStyle.Normal
            TB.Cursor = Cursors.IBeam

            If _Multiline Then
                TB.Height = Height - 8
            Else
                Height = TB.Height + 8
            End If

            AddHandler TB.TextChanged, AddressOf OnBaseTextChanged
            AddHandler TB.KeyDown, AddressOf OnBaseKeyDown
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

        Public Enum RoundingStyle
            Normal
            Rounded
        End Enum

        Private _Style As RoundingStyle

        <Category("Options")>
        Property Style() As RoundingStyle
            Get
                Return _Style
            End Get
            Set(ByVal value As RoundingStyle)
                _Style = value
                If TB IsNot Nothing Then
                    TB.TextAlign = CType(value, HorizontalAlignment)
                End If
            End Set
        End Property

        Private _BorderStyle As BorderStyle

        <Category("Options")>
        Public Property BorderStyle() As BorderStyle
            Get
                Return _BorderStyle
            End Get
            Set(ByVal value As BorderStyle)
                _BorderStyle = value
                Invalidate()
            End Set
        End Property

        <Category("Options")>
        Property TextAlign() As HorizontalAlignment
            Get
                Return _TextAlign
            End Get
            Set(ByVal value As HorizontalAlignment)
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
            Set(ByVal value As Integer)
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
            Set(ByVal value As Boolean)
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
            Set(ByVal value As Boolean)
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
            Set(ByVal value As Boolean)
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

        <Category("Options")>
        Overrides Property Text As String
            Get
                Return MyBase.Text
            End Get
            Set(ByVal value As String)
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
            Set(ByVal value As Font)
                MyBase.Font = value
                If TB IsNot Nothing Then
                    TB.Font = value
                    TB.Location = New Point(4, 4)
                    TB.Width = Width - 8
                    If Not _Multiline Then
                        Height = TB.Height + 10
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

        Private Sub OnBaseTextChanged(ByVal s As Object, ByVal e As EventArgs)
            Text = TB.Text
        End Sub

        Private Sub OnBaseKeyDown(ByVal s As Object, ByVal e As KeyEventArgs)
            If e.Control AndAlso e.KeyCode = Keys.A Then
                TB.SelectAll()
                e.SuppressKeyPress = True
            End If
            If e.Control AndAlso e.KeyCode = Keys.C Then
                TB.Copy()
                e.SuppressKeyPress = True
            End If
            Invalidate()
        End Sub

        Protected Overrides Sub OnResize(ByVal e As EventArgs)
            TB.Location = New Point(4, 4)
            TB.Width = Width - 8

            If _Multiline Then
                TB.Height = Height - 8
            Else
                Height = TB.Height + 10
            End If

            MyBase.OnResize(e)
        End Sub

#End Region
#End Region
        Public Property ButtonShadow As Color = Color.FromArgb(128, 128, 128)
        Public Property ButtonDkShadow As Color = Color.Black
        Public Property ButtonHilight As Color = Color.White
        Public Property ButtonLight As Color = Color.FromArgb(192, 192, 192)
#Region "Colors"
        Private ReadOnly _BaseColor As Color = BackColor
        Private ReadOnly _TextColor As Color = ForeColor
#End Region

#Region "Events"
        Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
            MyBase.OnMouseDown(e)
            State = MouseState.Down : Invalidate() : TB.Focus()
        End Sub

        Protected Overrides Sub OnMouseUp(e As MouseEventArgs)
            MyBase.OnMouseUp(e)
            State = MouseState.Over : TB.Focus() : Invalidate()
        End Sub

        Protected Overrides Sub OnMouseEnter(ByVal e As EventArgs)
            MyBase.OnMouseEnter(e)
            State = MouseState.Over : Invalidate()
        End Sub

        Protected Overrides Sub OnMouseLeave(ByVal e As EventArgs)
            MyBase.OnMouseLeave(e)
            State = MouseState.None : Invalidate()
        End Sub
#End Region
        Protected Overrides Sub OnPaint(e As PaintEventArgs)
            Dim B As New Bitmap(Width, Height)
            Dim G As Graphics = Graphics.FromImage(B)
            G = Graphics.FromImage(B)
            DoubleBuffered = True
            G.SmoothingMode = SmoothingMode.HighSpeed
            G.TextRenderingHint = My.RenderingHint

            MyBase.OnPaint(e)
            TB.ForeColor = ForeColor

            '################################################################################# Customizer
            Dim CheckRect As New Rectangle(0, 0, Width - 1, Height - 1)
            '#################################################################################

            G.Clear(BackColor)

            With CheckRect
                G.DrawLine(New Pen(ButtonShadow), New Point(.X, .Y), New Point(.Width - 1, .Y))
                G.DrawLine(New Pen(ButtonShadow), New Point(.X, .Y), New Point(.X, .Height - 1))
                G.DrawLine(New Pen(ButtonDkShadow), New Point(.X, .Y) + New Point(1, 1), New Point(.Width - 2, .Y + 1))
                G.DrawLine(New Pen(ButtonDkShadow), New Point(.X, .Y) + New Point(1, 1), New Point(.X + 1, .Height - 2))
                G.DrawLine(New Pen(ButtonLight), New Point(.Width - 1, 1), New Point(.Width - 1, .Height - 1))
                G.DrawLine(New Pen(ButtonLight), New Point(1, .Height - 1), New Point(.Width - 1, .Height - 1))
                G.DrawLine(New Pen(ButtonHilight), New Point(.Width, .X), New Point(.Width, .Height))
                G.DrawLine(New Pen(ButtonHilight), New Point(.X, .Height), New Point(.Width, .Height))
            End With

            G.DrawString(TB.Text, Font, New SolidBrush(ForeColor), New Point(2, 4))

            G.Dispose()
            e.Graphics.DrawImageUnscaled(B, 0, 0)
            B.Dispose()
        End Sub

        Private Sub TB_MouseDown(sender As Object, e As MouseEventArgs) Handles TB.MouseDown
            State = MouseState.Down
            Invalidate()
        End Sub

        Private Sub TB_MouseEnter(sender As Object, e As EventArgs) Handles TB.MouseEnter
            State = MouseState.Over : Invalidate()
        End Sub

        Private Sub TB_MouseLeave(sender As Object, e As EventArgs) Handles TB.MouseLeave
            State = MouseState.None : Invalidate()
        End Sub

        Private Sub TB_LostFocus(sender As Object, e As EventArgs) Handles TB.LostFocus
            State = MouseState.None : Invalidate()
        End Sub

        Private Sub RetroTextBox_BackColorChanged(sender As Object, e As EventArgs) Handles Me.BackColorChanged
            Try : If TB IsNot Nothing Then TB.BackColor = BackColor
            Catch : End Try
        End Sub
    End Class
End Namespace

