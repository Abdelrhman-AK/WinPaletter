Imports System.ComponentModel
Imports System.Drawing.Drawing2D
Imports System.Drawing.Text

Public Module Helpers
    Private ReadOnly TextGraphics As Graphics
    Private ReadOnly TextBitmap As Bitmap

    Sub New()
        TextBitmap = New Bitmap(1, 1)
        TextGraphics = Graphics.FromImage(TextBitmap)
    End Sub


    Public Function CString(T As String, F As Font, R As Rectangle) As PointF
        Dim TS As SizeF = MeasureString(T, F)
        Dim xX As Single = R.X + (R.Width - TS.Width) / 2
        Dim xY As Single = R.Y + (R.Height - TS.Height) / 2
        If F.Name.ToUpper <> "MARLETT" Then
            Return New Point(CInt(xX + 1), CInt(xY))
        Else
            Return New Point(CInt(xX + 1), CInt(xY + 1))
        End If
    End Function

    Public Function ChangeColorBrightness(ByVal color As Color, ByVal correctionFactor As Single) As Color
        Dim red As Single = CSng(color.R)
        Dim green As Single = CSng(color.G)
        Dim blue As Single = CSng(color.B)

        If correctionFactor < 0 Then
            correctionFactor = 1 + correctionFactor
            red *= correctionFactor
            green *= correctionFactor
            blue *= correctionFactor
        Else
            red = (255 - red) * correctionFactor + red
            green = (255 - green) * correctionFactor + green
            blue = (255 - blue) * correctionFactor + blue
        End If
        Return Color.FromArgb(color.A, CInt(red), CInt(green), CInt(blue))
    End Function
End Module
Public Class RetroButton : Inherits Button
    Sub New()
        Font = New Font("Microsoft Sans Serif", 8)
        ForeColor = Color.Black
        BackColor = Color.FromArgb(192, 192, 192)
        Image = MyBase.Image
        DoubleBuffered = True
    End Sub

#Region "Properties"

    Private _Image As Image

    Public Overloads Property Image() As Image
        Get
            Return _Image
        End Get
        Set(ByVal value As Image)
            _Image = value
            Invalidate()
        End Set
    End Property
    Public Property WindowFrame As Color = Color.Black
    Public Property ButtonShadow As Color = Color.FromArgb(128, 128, 128)
    Public Property ButtonDkShadow As Color = Color.Black
    Public Property ButtonHilight As Color = Color.White
    Public Property ButtonLight As Color = Color.FromArgb(192, 192, 192)
    Public Property UseItAsScrollbar As Boolean = False
#End Region

#Region "Events"
    Protected Overrides Sub OnPaintBackground(e As PaintEventArgs)
        MyBase.OnPaintBackground(e)
    End Sub

    Protected Overrides Sub OnBackColorChanged(e As EventArgs)
        Invalidate()
        MyBase.OnBackColorChanged(e)
    End Sub

    Protected Overrides Sub OnMouseEnter(ByVal e As EventArgs)
        MyBase.OnMouseEnter(e)
        State = MouseState.Over
        Invalidate()
    End Sub

    Protected Overrides Sub OnMouseLeave(ByVal e As EventArgs)
        MyBase.OnMouseLeave(e)
        State = MouseState.None
        Invalidate()
    End Sub

    Protected Overrides Sub OnMouseDown(ByVal e As MouseEventArgs)
        MyBase.OnMouseDown(e)
        State = MouseState.Down : Pressed = True : Invalidate()
    End Sub

    Protected Overrides Sub OnMouseUp(ByVal e As MouseEventArgs)
        MyBase.OnMouseUp(e)
        State = MouseState.Over : Invalidate()
    End Sub

    Private Sub XenonButton_LostFocus(sender As Object, e As EventArgs) Handles Me.LostFocus
        State = MouseState.None : Pressed = False : Invalidate()
    End Sub

    Enum MouseState
        None
        Over
        Down
    End Enum

    Dim State As MouseState = MouseState.None
    Dim Pressed As Boolean
#End Region

    Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
        Dim B As New Bitmap(Width, Height)
        Dim G As Graphics = Graphics.FromImage(B)
        G.SmoothingMode = SmoothingMode.HighSpeed
        G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit
        DoubleBuffered = True

        '################################################################################# Customizer
        Dim rect As New Rectangle(0, 0, Width - 1, Height - 1)
        Dim rectinner As New Rectangle(1, 1, Width - 3, Height - 3)
        Dim rectdash As New Rectangle(4, 4, Width - 9, Height - 9)
        Dim pendash As New Pen(Color.Black)
        pendash.DashStyle = DashStyle.Dot
        '#################################################################################

        G.Clear(BackColor)

#Region "Button Render"
        If UseItAsScrollbar Then
            G.DrawLine(New Pen(ButtonHilight), New Point(0, 0), New Point(Width - 1, 0))
            G.DrawLine(New Pen(ButtonHilight), New Point(0, 1), New Point(0, Height - 1))
            G.DrawLine(New Pen(ButtonDkShadow), New Point(0, Height - 1), New Point(Width - 1, Height - 1))
            G.DrawLine(New Pen(ButtonDkShadow), New Point(Width - 1, 0), New Point(Width - 1, Height - 1))
            G.DrawLine(New Pen(ButtonLight), New Point(1, 1), New Point(Width - 2, 1))
            G.DrawLine(New Pen(ButtonLight), New Point(1, 2), New Point(1, Height - 2))
            G.DrawLine(New Pen(ButtonShadow), New Point(1, Height - 2), New Point(Width - 2, Height - 2))
            G.DrawLine(New Pen(ButtonShadow), New Point(Width - 2, 1), New Point(Width - 2, Height - 2))
        Else
            If State = MouseState.Over Or State = MouseState.None Or Not Enabled Then
                If Not Focused Then
                    G.DrawLine(New Pen(ButtonHilight), New Point(0, 0), New Point(Width - 1, 0))
                    G.DrawLine(New Pen(ButtonHilight), New Point(0, 1), New Point(0, Height - 1))
                    G.DrawLine(New Pen(ButtonDkShadow), New Point(0, Height - 1), New Point(Width - 1, Height - 1))
                    G.DrawLine(New Pen(ButtonDkShadow), New Point(Width - 1, 0), New Point(Width - 1, Height - 1))
                    G.DrawLine(New Pen(ButtonLight), New Point(1, 1), New Point(Width - 2, 1))
                    G.DrawLine(New Pen(ButtonLight), New Point(1, 2), New Point(1, Height - 2))
                    G.DrawLine(New Pen(ButtonShadow), New Point(1, Height - 2), New Point(Width - 2, Height - 2))
                    G.DrawLine(New Pen(ButtonShadow), New Point(Width - 2, 1), New Point(Width - 2, Height - 2))
                Else
                    G.DrawRectangle(New Pen(ButtonDkShadow), rect)
                    G.DrawLine(New Pen(ButtonHilight), New Point(1, 1), New Point(Width - 2, 1))
                    G.DrawLine(New Pen(ButtonHilight), New Point(1, 2), New Point(1, Height - 2))
                    G.DrawLine(New Pen(ButtonDkShadow), New Point(1, Height - 2), New Point(Width - 2, Height - 2))
                    G.DrawLine(New Pen(ButtonDkShadow), New Point(Width - 2, 1), New Point(Width - 2, Height - 2))
                    G.DrawLine(New Pen(ButtonLight), New Point(2, 2), New Point(Width - 3, 2))
                    G.DrawLine(New Pen(ButtonLight), New Point(2, 3), New Point(2, Height - 3))
                    G.DrawLine(New Pen(ButtonShadow), New Point(2, Height - 3), New Point(Width - 3, Height - 3))
                    G.DrawLine(New Pen(ButtonShadow), New Point(Width - 3, 2), New Point(Width - 3, Height - 3))
                    If Pressed And Not Font.FontFamily.Name.ToLower = "marlett" Then
                        G.DrawRectangle(pendash, rectdash)
                        G.DrawRectangle(New Pen(WindowFrame), rect)
                    End If
                End If

            Else
                G.DrawRectangle(New Pen(WindowFrame), rect)
                G.DrawRectangle(New Pen(ButtonShadow), rectinner)
                If Not Font.FontFamily.Name.ToLower = "marlett" Then G.DrawRectangle(pendash, rectdash)
            End If
        End If

#End Region

#Region "Text and Image Render"
        Dim ButtonString As New StringFormat() With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center}
        Dim imgX, imgY As Integer

        Try : If Image IsNot Nothing Then imgX = CInt((Width - Image.Width) / 2)
        Catch : End Try

        Try : If Image IsNot Nothing Then imgY = CInt((Height - Image.Height) / 2)
        Catch : End Try

        Dim FColor As Color
        If Enabled Then FColor = ForeColor Else FColor = ChangeColorBrightness(BackColor, -0.2)

        If Image Is Nothing Then
            If Not Enabled Then G.DrawString(Text, Font, New SolidBrush(ChangeColorBrightness(BackColor, 0.8)), New Rectangle(1, 1, Width, Height), StringAligner(TextAlign))
            G.DrawString(Text, Font, New SolidBrush(FColor), New Rectangle(0, 0, Width, Height), StringAligner(TextAlign))
        Else
            Select Case Me.ImageAlign
                Case ContentAlignment.MiddleCenter
                    ButtonString.Alignment = StringAlignment.Center : ButtonString.LineAlignment = StringAlignment.Near

                    Dim alx As Integer = CInt((Height - (Image.Height + 4 + MeasureString(Text, MyBase.Font).Height)) / 2)
                    Try : If Image IsNot Nothing Then
                            If Text = Nothing Then
                                G.DrawImage(Me.Image, New Rectangle(imgX, imgY, Image.Width, Image.Height))
                            Else
                                G.DrawImage(Me.Image, New Rectangle(imgX, alx, Image.Width, Image.Height))
                            End If

                        End If

                        If Not Enabled Then G.DrawString(Text, Font, New SolidBrush(ChangeColorBrightness(BackColor, 0.8)), New Rectangle(1, alx + 10 + Image.Height, Width, Height), ButtonString)
                        G.DrawString(Text, Font, New SolidBrush(FColor), New Rectangle(0, alx + 9 + Image.Height, Width, Height), ButtonString)
                    Catch : End Try

                Case ContentAlignment.MiddleLeft
                    ButtonString.Alignment = StringAlignment.Near : ButtonString.LineAlignment = StringAlignment.Center
                    Dim alx As Integer = CInt((Width - (Image.Width + 4 + MeasureString(Text, MyBase.Font).Width)) / 2)
                    Try : If Image IsNot Nothing Then
                            G.DrawImage(Me.Image, New Rectangle(alx, imgY, Image.Width, Image.Height))
                        End If

                        If Not Enabled Then G.DrawString(Text, Font, New SolidBrush(ChangeColorBrightness(BackColor, 0.8)), New Rectangle(alx + 6 + Image.Width, 1, Width, Height), ButtonString)
                        G.DrawString(Text, Font, New SolidBrush(FColor), New Rectangle(alx + 5 + Image.Width, 0, Width, Height), ButtonString)
                    Catch : End Try

                Case ContentAlignment.MiddleRight
                    Try : If Image IsNot Nothing Then G.DrawImage(Me.Image, New Rectangle(5, imgY, Image.Width, Image.Height))
                        With CString(Text, Font, New Rectangle(1, 1, Width - 2, Height - 2))
                            If Not Enabled Then G.DrawString(Text, Font, New SolidBrush(ChangeColorBrightness(BackColor, 0.8)), New Rectangle(6, 1, Width, Height), StringAligner(TextAlign))
                            G.DrawString(Text, Font, New SolidBrush(FColor), New Rectangle(5, 0, Width, Height), StringAligner(TextAlign))
                        End With
                    Catch : End Try
            End Select
        End If
#End Region

        e.Graphics.DrawImage(B, New Point(0, 0))
        G.Dispose() : B.Dispose()
    End Sub

End Class

<DefaultEvent("CheckedChanged")>
Public Class RetroCheckBox
    Inherits Control

    Event CheckedChanged(sender As Object)

    Sub New()
        SetStyle(DirectCast(139286, ControlStyles), True)
        SetStyle(ControlStyles.Selectable, False)
        DoubleBuffered = True
        Font = New Font("Microsoft Sans Serif", 8)
        BackColor = Color.FromArgb(192, 192, 192)
        ForeColor = Color.Black
    End Sub

#Region "Properties"

    Public Property Checked() As Boolean
        Get
            Return _Checked
        End Get
        Set(ByVal value As Boolean)
            _Checked = value
            RaiseEvent CheckedChanged(Me)
            Invalidate()
        End Set
    End Property

    Private _Checked As Boolean
    Public Property ButtonShadow As Color = Color.FromArgb(128, 128, 128)
    Public Property ButtonDkShadow As Color = Color.Black
    Public Property ButtonHilight As Color = Color.White
    Public Property ButtonLight As Color = Color.FromArgb(192, 192, 192)
#End Region

#Region "Events"

    Protected Overrides Sub OnMouseDown(ByVal e As MouseEventArgs)
        Checked = Not Checked
        MyBase.OnMouseDown(e)
        State = MouseState.Down : Invalidate()
    End Sub

    Protected Overrides Sub OnMouseUp(ByVal e As MouseEventArgs)
        MyBase.OnMouseUp(e)
        State = MouseState.Over : Invalidate()
    End Sub

    Enum MouseState
        None
        Over
        Down
    End Enum

    Dim State As MouseState = MouseState.None
#End Region

    Protected Overrides Sub OnPaint(e As System.Windows.Forms.PaintEventArgs)
        Dim G As Graphics = e.Graphics
        G.SmoothingMode = SmoothingMode.HighSpeed
        G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit
        DoubleBuffered = True

        '################################################################################# Customizer
        Dim CheckRect As Rectangle = New Rectangle(0, 0, 12, 12)
        Dim pendash As New Pen(Color.Black)
        pendash.DashStyle = DashStyle.Dot
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
            G.FillRectangle(New SolidBrush(If(State = MouseState.Down, BackColor, Color.White)), New Rectangle(.X + 2, .Y + 2, .Width - 3, .Height - 3))
        End With

        If Checked Then
            G.DrawString("b", New Font("Marlett", 10), New SolidBrush(Color.Black), New Point(-2, 0))
        End If

        G.DrawString(Text, Font, New SolidBrush(ForeColor), New Point(16, 0))

        If State = MouseState.Down Then G.DrawRectangle(pendash, New Rectangle(18, 0, MeasureString(Text, Font).Width - 6, 12))
    End Sub

    Private Sub RetroCheckBox_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged
        If Height <> 13 Then Height = 13
    End Sub


End Class

<DefaultEvent("CheckedChanged")>
Public Class RetroRadioButton
    Inherits Control
    Event CheckedChanged(sender As Object)

    Sub New()
        SetStyle(DirectCast(139286, ControlStyles), True)
        SetStyle(ControlStyles.Selectable, False)
        DoubleBuffered = True
        Font = New Font("Microsoft Sans Serif", 8)
        BackColor = Color.FromArgb(192, 192, 192)
        ForeColor = Color.Black
    End Sub

#Region "Properties"
    Private Sub InvalidateParent()
        If Parent Is Nothing Then Return

        For Each C As Control In Parent.Controls
            If Not (C Is Me) AndAlso (TypeOf C Is RetroRadioButton) Then
                DirectCast(C, RetroRadioButton).Checked = False
            End If
        Next
    End Sub

    Public Property Checked() As Boolean
        Get
            Return _Checked
        End Get
        Set(ByVal value As Boolean)
            _Checked = value
            If _Checked Then
                InvalidateParent()
            End If
            RaiseEvent CheckedChanged(Me)
            Invalidate()
        End Set
    End Property
    Private _Checked As Boolean

    Public Property ButtonShadow As Color = Color.FromArgb(128, 128, 128)
    Public Property ButtonDkShadow As Color = Color.Black
    Public Property ButtonHilight As Color = Color.White
    Public Property ButtonLight As Color = Color.FromArgb(192, 192, 192)
#End Region

#Region "Events"
    Enum MouseState
        None
        Over
        Down
    End Enum

    Dim State As MouseState = MouseState.None

    Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
        Checked = True
        State = MouseState.Down
        Invalidate()
        MyBase.OnMouseDown(e)
    End Sub

    Protected Overrides Sub OnMouseUp(e As MouseEventArgs)
        State = MouseState.None
        Invalidate()
    End Sub
#End Region

    Protected Overrides Sub OnPaint(e As System.Windows.Forms.PaintEventArgs)
        Dim G As Graphics = e.Graphics
        G.SmoothingMode = SmoothingMode.HighSpeed
        G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit
        DoubleBuffered = True

        '################################################################################# Customizer
        Dim CheckRect As Rectangle = New Rectangle(0, 0, 11, 11)
        Dim CheckRect2 As Rectangle = New Rectangle(1, 1, 9, 9)
        Dim pendash As New Pen(Color.Black)
        pendash.DashStyle = DashStyle.Dot
        '#################################################################################

        G.Clear(BackColor)

        G.FillEllipse(New SolidBrush(If(State = MouseState.Down, BackColor, Color.White)), CheckRect)
        G.DrawArc(New Pen(ButtonShadow), CheckRect, 140, 180)
        G.DrawArc(New Pen(ButtonHilight), CheckRect, 320, 180)
        G.DrawArc(New Pen(ButtonDkShadow), CheckRect2, 140, 180)
        G.DrawLine(New Pen(ButtonDkShadow), New Point(CheckRect2.X, CheckRect2.Y) + New Point(1, 1), New Point(CheckRect2.X, CheckRect2.Y) + New Point(2, 1))
        G.DrawLine(New Pen(ButtonDkShadow), New Point(CheckRect2.X + CheckRect2.Width, CheckRect2.Y) + New Point(-1, 1), New Point(CheckRect2.X + CheckRect2.Width, CheckRect2.Y) + New Point(-2, 1))
        G.DrawArc(New Pen(BackColor), CheckRect2, 320, 180)
        G.DrawLine(New Pen(BackColor), New Point(CheckRect2.X, CheckRect2.Y + CheckRect2.Height) + New Point(1, -1), New Point(CheckRect2.X, CheckRect2.Y + CheckRect2.Height) + New Point(2, -1))
        G.DrawLine(New Pen(BackColor), New Point(CheckRect2.X + CheckRect2.Width, CheckRect2.Y + CheckRect2.Height) + New Point(-1, -1), New Point(CheckRect2.X + CheckRect2.Width, CheckRect2.Y + CheckRect2.Height) + New Point(-2, -1))

        If Checked Then
            G.DrawString("h", New Font("Marlett", 10), New SolidBrush(Color.Black), New Point(-3, 0))
        End If

        G.DrawString(Text, Font, New SolidBrush(ForeColor), New Point(14, 0))

        If State = MouseState.Down Then G.DrawRectangle(pendash, New Rectangle(16, 0, MeasureString(Text, Font).Width - 6, 12))
    End Sub

    Private Sub RetroCheckBox_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged
        If Height <> 13 Then Height = 13
    End Sub

End Class
<DefaultEvent("TextChanged")> Public Class RetroTextBox : Inherits Control
    Sub New()
        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint Or
                 ControlStyles.ResizeRedraw Or ControlStyles.OptimizedDoubleBuffer Or ControlStyles.SupportsTransparentBackColor, True)
        DoubleBuffered = True
        ForeColor = Color.Black
        BackColor = Color.White
        TB = New Windows.Forms.TextBox
        TB.Visible = True
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
    Private _BaseColor As Color = BackColor
    Private _TextColor As Color = ForeColor
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
        G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit

        MyBase.OnPaint(e)
        TB.ForeColor = ForeColor

        '################################################################################# Customizer
        Dim CheckRect As Rectangle = New Rectangle(0, 0, Width - 1, Height - 1)
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
Public Class RetroLabel : Inherits Label
    Sub New()
        Font = New Font("Microsoft Sans Serif", 8)
        ForeColor = Color.Black
        BackColor = Color.Transparent
        AutoSize = False
        TextAlign = ContentAlignment.MiddleLeft
        DoubleBuffered = True
    End Sub

End Class
Public Class RetroGroupBox : Inherits GroupBox
    Public Sub New()
        DoubleBuffered = True
        Font = New Font("Microsoft Sans Serif", 8)
        ForeColor = Color.Black
        BackColor = Color.FromArgb(192, 192, 192)
    End Sub

    Public Property ButtonHilight As Color = Color.White
    Public Property ButtonShadow As Color = Color.FromArgb(128, 128, 128)

    Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
        Dim B As New Bitmap(Width, Height)
        Dim G As Graphics = Graphics.FromImage(B)
        G.SmoothingMode = SmoothingMode.HighSpeed
        G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit
        DoubleBuffered = True
        G.Clear(BackColor)

        Dim Rect1 As New Rectangle(0, 6, Width - 1, Height - 10)
        Dim Rect2 As New Rectangle(1, 7, Width - 3, Height - 12)
        Dim C12 As Color = ButtonHilight
        Dim Rect3 As New Rectangle(0, 6, Width - 2, Height - 11)
        Dim C3 As Color = ButtonShadow

        G.DrawRectangle(New Pen(C12), Rect1)
        G.DrawRectangle(New Pen(C12), Rect2)
        G.DrawRectangle(New Pen(C3), Rect3)

        With MeasureString(Text, Font)
            G.FillRectangle(New SolidBrush(BackColor), New Rectangle(10, 0, .Width - 4, .Height))
            G.DrawString(Text, Font, New SolidBrush(ForeColor), New Point(10, 0))
        End With


        e.Graphics.DrawImage(B, New Point(0, 0))
        G.Dispose() : B.Dispose()
    End Sub
End Class
Public Class RetroSeparatorH : Inherits Control
    Private G As Graphics

    Sub New()
        BackColor = Color.FromArgb(192, 192, 192)
        DoubleBuffered = True
    End Sub

    Public Property ButtonHilight As Color = Color.White
    Public Property ButtonShadow As Color = Color.FromArgb(128, 128, 128)

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        G = e.Graphics
        G.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
        DoubleBuffered = True
        MyBase.OnPaint(e)

        G.DrawLine(New Pen(ButtonShadow), New Point(0, 0), New Point(Width, 0))
        G.DrawLine(New Pen(ButtonHilight), New Point(0, 1), New Point(Width, 1))
    End Sub

    Private Sub RetroSeparatorH_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged
        If Height > 5 Then Height = 5
    End Sub
End Class
Public Class RetroSeparatorV : Inherits Control
    Private G As Graphics

    Sub New()
        BackColor = Color.FromArgb(192, 192, 192)
        DoubleBuffered = True
    End Sub

    Public Property ButtonHilight As Color = Color.White
    Public Property ButtonShadow As Color = Color.FromArgb(128, 128, 128)

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        G = e.Graphics
        G.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
        DoubleBuffered = True
        MyBase.OnPaint(e)
        G.DrawLine(New Pen(ButtonShadow), New Point(0, 0), New Point(0, Height))
        G.DrawLine(New Pen(ButtonHilight), New Point(1, 0), New Point(1, Height))
    End Sub

    Private Sub RetroSeparatorV_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged
        If Width > 5 Then Width = 5
    End Sub
End Class
Public Class RetroPanel : Inherits Panel
    Sub New()
        DoubleBuffered = True
        Font = New Font("Microsoft Sans Serif", 8)
        BackColor = Color.FromArgb(192, 192, 192)
        ForeColor = Color.Black
        BorderStyle = BorderStyle.None
    End Sub

    Public Property Flat As Boolean = False
    Public Property ButtonHilight As Color = Color.White
    Public Property ButtonShadow As Color = Color.FromArgb(128, 128, 128)

    Protected Overrides Sub OnPaint(e As System.Windows.Forms.PaintEventArgs)
        Dim G As Graphics = e.Graphics
        G.SmoothingMode = SmoothingMode.HighSpeed
        G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit
        DoubleBuffered = True

        '################################################################################# Customizer
        Dim Rect As Rectangle = New Rectangle(0, 0, Width - 1, Height - 1)
        '#################################################################################
        G.Clear(BackColor)

        If Not Flat Then
            With Rect
                G.DrawLine(New Pen(ButtonShadow), New Point(.X, .Y), New Point(.Width - 1, .Y))
                G.DrawLine(New Pen(ButtonShadow), New Point(.X, .Y), New Point(.X, .Height - 1))
                G.DrawLine(New Pen(ButtonHilight), New Point(.Width, .X), New Point(.Width, .Height))
                G.DrawLine(New Pen(ButtonHilight), New Point(.X, .Height), New Point(.Width, .Height))
            End With
        Else
            G.DrawRectangle(New Pen(ButtonShadow), Rect)
        End If

    End Sub
End Class
Public Class RetroPanelRaised : Inherits Panel
    Sub New()
        DoubleBuffered = True
        Font = New Font("Microsoft Sans Serif", 8)
        BackColor = Color.FromArgb(192, 192, 192)
        ForeColor = Color.Black
        BorderStyle = BorderStyle.None
    End Sub
    Public Property Flat As Boolean = False
    Public Property ButtonHilight As Color = Color.White
    Public Property ButtonShadow As Color = Color.FromArgb(128, 128, 128)

    Protected Overrides Sub OnPaint(e As System.Windows.Forms.PaintEventArgs)
        Dim G As Graphics = e.Graphics
        G.SmoothingMode = SmoothingMode.HighSpeed
        G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit
        DoubleBuffered = True

        '################################################################################# Customizer
        Dim Rect As Rectangle = New Rectangle(0, 0, Width - 1, Height - 1)
        '#################################################################################
        G.Clear(BackColor)

        If Not Flat Then
            With Rect
                G.DrawLine(New Pen(ButtonHilight), New Point(.X, .Y), New Point(.Width - 1, .Y))
                G.DrawLine(New Pen(ButtonHilight), New Point(.X, .Y), New Point(.X, .Height - 1))
                G.DrawLine(New Pen(ButtonShadow), New Point(.Width, .X), New Point(.Width, .Height))
                G.DrawLine(New Pen(ButtonShadow), New Point(.X, .Height), New Point(.Width, .Height))
            End With
        Else
            G.DrawRectangle(New Pen(ButtonShadow), Rect)
        End If

    End Sub
End Class
Public Class RetroWindow : Inherits Panel
    Sub New()
        DoubleBuffered = True
        Font = New Font("Microsoft Sans Serif", 8)
        BackColor = Color.FromArgb(192, 192, 192)
        ForeColor = Color.White
        BorderStyle = BorderStyle.None
        TitlebarText = "New Window"
    End Sub

    Public Property Color1 As Color = Color.FromArgb(0, 0, 128)
    Public Property Color2 As Color = Color.FromArgb(16, 132, 208)
    Public Property ColorGradient As Boolean = True
    Public Property ColorBorder As Color = Color.FromArgb(192, 192, 192)
    Public Property TitlebarText As String = "New Window"
    Public Property UseItAsMenu As Boolean = False
    Public Property Flat As Boolean = False
    Public Property ButtonShadow As Color = Color.FromArgb(128, 128, 128)
    Public Property ButtonDkShadow As Color = Color.Black
    Public Property ButtonHilight As Color = Color.White
    Public Property ButtonLight As Color = Color.FromArgb(192, 192, 192)
    Public Property ButtonFace As Color = Color.FromArgb(192, 192, 192)

    Private _Metrics_CaptionHeight As Integer = 22
    Public Property Metrics_CaptionHeight As Integer
        Get
            Return _Metrics_CaptionHeight
        End Get

        Set(value As Integer)
            _Metrics_CaptionHeight = value
            Refresh()
        End Set
    End Property

    Public Property Metrics_CaptionWidth As Integer


    Private _Metrics_BorderWidth As Integer = 1
    Public Property Metrics_BorderWidth As Integer
        Get
            Return _Metrics_BorderWidth
        End Get

        Set(value As Integer)
            _Metrics_BorderWidth = value
            Refresh()
        End Set
    End Property

    Private _Metrics_PaddedBorderWidth As Integer = 4
    Public Property Metrics_PaddedBorderWidth As Integer
        Get
            Return _Metrics_PaddedBorderWidth
        End Get
        Set(value As Integer)
            _Metrics_PaddedBorderWidth = value
            Refresh()
        End Set
    End Property

    Public Function GetTitleTextHeight()
        Dim TitleTextH, TitleTextH_9, TitleTextH_Sum As Integer
        TitleTextH = MeasureString("ABCabc0123xYz.#", Font).Height
        TitleTextH_9 = MeasureString("ABCabc0123xYz.#", New Font(Font.Name, 9, Font.Style)).Height
        TitleTextH_Sum = Math.Max(0, TitleTextH - TitleTextH_9 - 5)

        Return TitleTextH_Sum
    End Function

    Protected Overrides Sub OnPaint(e As System.Windows.Forms.PaintEventArgs)
        Dim G As Graphics = e.Graphics
        G.SmoothingMode = SmoothingMode.HighSpeed
        G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit
        DoubleBuffered = True

        '################################################################################# Customizer
        Dim Rect As New Rectangle(0, 0, Width - 1, Height - 1)

        Dim TitleTextH, TitleTextH_9, TitleTextH_Sum As Integer
        TitleTextH = MeasureString("ABCabc0123xYz.#", Font).Height
        TitleTextH_9 = MeasureString("ABCabc0123xYz.#", New Font(Font.Name, 9, Font.Style)).Height
        TitleTextH_Sum = Math.Max(0, TitleTextH - TitleTextH_9 - 5)

        Dim CompinedPadding As Integer = _Metrics_BorderWidth + _Metrics_PaddedBorderWidth + 3

        Dim TRect As New Rectangle(CompinedPadding, CompinedPadding, Width - CompinedPadding * 2, _Metrics_CaptionHeight + TitleTextH_Sum)

        Dim ARect As New Rectangle(2, 2, Width - 5, Height - 5)
        '#################################################################################
        G.Clear(BackColor)

        If Not Flat Then
            With Rect
                G.DrawLine(New Pen(ButtonShadow), New Point(.Width - 1, .X + 1), New Point(.Width - 1, .Height - 1))
                G.DrawLine(New Pen(ButtonShadow), New Point(.X + 1, .Height - 1), New Point(.Width - 1, .Height - 1))

                G.DrawLine(New Pen(ButtonHilight), New Point(.X + 1, .Y + 1), New Point(.Width - 2, .Y + 1))
                G.DrawLine(New Pen(ButtonHilight), New Point(.X + 1, .Y + 1), New Point(.X + 1, .Height - 2))

                G.DrawLine(New Pen(ButtonLight), New Point(.X, .Y), New Point(.Width - 1, .Y))
                G.DrawLine(New Pen(ButtonLight), New Point(.X, .Y), New Point(.X, .Height - 1))

                G.DrawLine(New Pen(ButtonDkShadow), New Point(.Width, .X), New Point(.Width, .Height))
                G.DrawLine(New Pen(ButtonDkShadow), New Point(.X, .Height), New Point(.Width, .Height))

                G.DrawRectangle(New Pen(ButtonFace), New Rectangle(2, 2, Width - 5, Height - 5))
            End With
        Else
            G.DrawRectangle(New Pen(ButtonShadow), Rect)
        End If

        If Not Flat And Not UseItAsMenu Then G.DrawRectangle(New Pen(ColorBorder), ARect)
        Dim F As Font

        If Not UseItAsMenu Then
            If G.TextRenderingHint = TextRenderingHint.SingleBitPerPixelGridFit Then
                F = New Font("Microsoft Sans Serif", 8, FontStyle.Bold)
            Else
                F = Font
            End If

            Dim RTL As Boolean = If(RightToLeft = 1, True, False)
            Dim gr As New LinearGradientBrush(TRect, If(RTL, Color2, Color1), If(RTL, Color1, Color2), LinearGradientMode.Horizontal)
            If ColorGradient Then
                G.FillRectangle(gr, TRect)

                Dim TRectFixer As New Rectangle(TRect.X, TRect.Y, 1, TRect.Height)
                G.FillRectangle(New SolidBrush(If(RTL, Color2, Color1)), TRectFixer)

            Else
                G.FillRectangle(New SolidBrush(Color1), TRect)
            End If
            G.DrawString(TitlebarText, F, New SolidBrush(ForeColor), TRect, StringAligner(ContentAlignment.MiddleLeft, RTL))
        End If

    End Sub

End Class


Public Class RetroScrollBar : Inherits Panel
    Sub New()
        DoubleBuffered = True
        BackColor = Color.FromArgb(192, 192, 192)
        BorderStyle = BorderStyle.None
    End Sub

    Public Property ButtonHilight As Color = Color.White
    Protected Overrides Sub OnPaint(e As System.Windows.Forms.PaintEventArgs)
        Dim G As Graphics = e.Graphics
        G.SmoothingMode = SmoothingMode.HighSpeed
        G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit
        DoubleBuffered = True

        '################################################################################# Customizer
        Dim Rect As Rectangle = New Rectangle(0, 0, Width - 1, Height - 1)
        '#################################################################################
        G.Clear(BackColor)
        Dim b As New HatchBrush(HatchStyle.Percent50, ButtonHilight, BackColor)
        G.FillRectangle(b, Rect)
    End Sub
End Class