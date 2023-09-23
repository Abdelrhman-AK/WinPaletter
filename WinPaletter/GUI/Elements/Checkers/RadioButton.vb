Imports System.ComponentModel
Imports System.Drawing.Drawing2D
Imports System.Drawing.Text

Namespace UI.WP

    <Description("Themed RadioButton for WinPaletter UI")> <DefaultEvent("CheckedChanged")> Public Class RadioButton : Inherits Control

        Event CheckedChanged(sender As Object)

        Sub New()
            SetStyle(DirectCast(139286, ControlStyles), True)
            SetStyle(ControlStyles.Selectable, False)
            DoubleBuffered = True
            Font = New Font("Segoe UI", 9)
            ForeColor = Color.White

        End Sub

#Region "Properties"
        Private Sub InvalidateParent()
            If Parent Is Nothing Then Return

            For Each C As Control In Parent.Controls
                If Not (C Is Me) AndAlso (TypeOf C Is RadioButton) Then
                    DirectCast(C, RadioButton).Checked = False
                End If
            Next
        End Sub

        Public Property Checked() As Boolean
            Get
                Return _Checked
            End Get
            Set(value As Boolean)
                Try
                    _Checked = value

                    If _Checked Then
                        InvalidateParent()
                    End If

                    RaiseEvent CheckedChanged(Me)

                    If AnimateOnClick Then
                        Tmr2.Enabled = True
                        Tmr2.Start()
                    Else
                        alpha2 = If(Checked, 255, 0)
                    End If

                    Refresh()

                Catch
                End Try
            End Set
        End Property

        Private _Checked As Boolean

        <Browsable(True)>
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
        <EditorBrowsable(EditorBrowsableState.Always)>
        <Editor(GetType(System.ComponentModel.Design.MultilineStringEditor), GetType(System.Drawing.Design.UITypeEditor))>
        <Bindable(True)>
        Public Overrides Property Text As String
#End Region

#Region "Events"
        Enum MouseState
            None
            Over
            Down
        End Enum

        Public State As MouseState = MouseState.None
        Private AnimateOnClick As Boolean = False

        Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
            Checked = True
            State = MouseState.Down
            Tmr.Enabled = True
            Tmr.Start()
            Invalidate()
            MyBase.OnMouseDown(e)
        End Sub

        Protected Overrides Sub OnMouseClick(e As MouseEventArgs)
            AnimateOnClick = True
            Checked = True
            State = MouseState.Down
            Tmr2.Enabled = True
            Tmr2.Start()
            Invalidate()
            MyBase.OnMouseClick(e)
        End Sub

        Protected Overrides Sub OnMouseUp(e As MouseEventArgs)
            State = MouseState.Over
            Tmr.Enabled = True
            Tmr.Start()
            Invalidate()
        End Sub

        Private Sub RadioButton_MouseEnter(sender As Object, e As EventArgs) Handles Me.MouseEnter
            State = MouseState.Over
            Tmr.Enabled = True
            Tmr.Start()
            Invalidate()
        End Sub

        Private Sub CheckBox_MouseLeave(sender As Object, e As EventArgs) Handles Me.MouseLeave
            State = MouseState.None
            Tmr.Enabled = True
            Tmr.Start()
            Invalidate()
        End Sub

        Private Sub RadioButton_HandleCreated(sender As Object, e As EventArgs) Handles Me.HandleCreated

            Try
                If Not DesignMode Then
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
                alpha2 = If(Checked, 255, 0)
            Catch
            End Try
        End Sub

        Private Sub RadioButton_HandleDestroyed(sender As Object, e As EventArgs) Handles Me.HandleDestroyed
            Try
                If Not DesignMode Then
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

        Sub Showed()
            Invalidate()
        End Sub

        Public Sub RefreshColorPalette()
            Invalidate()
        End Sub
#End Region

#Region "Animator"
        Dim alpha, alpha2 As Integer
        ReadOnly Factor As Integer = 25
        Dim WithEvents Tmr, Tmr2 As New Timer With {.Enabled = False, .Interval = 1}

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

                    Threading.Thread.Sleep(1)
                    Invalidate()
                End If

                If Not State = MouseState.Over Then
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

        Private Sub Tmr2_Tick(sender As Object, e As EventArgs) Handles Tmr2.Tick
            If Not DesignMode Then

                If Checked Then
                    If alpha2 + Factor <= 255 Then
                        alpha2 += Factor
                    ElseIf alpha2 + Factor > 255 Then
                        alpha2 = 255
                        Tmr2.Enabled = False
                        Tmr2.Stop()
                        AnimateOnClick = False
                    End If

                    Threading.Thread.Sleep(1)
                    Invalidate()
                End If

                If Not Checked Then
                    If alpha2 - Factor >= 0 Then
                        alpha2 -= Factor
                    ElseIf alpha2 - Factor < 0 Then
                        alpha2 = 0
                        Tmr2.Enabled = False
                        Tmr2.Stop()
                        AnimateOnClick = False
                    End If

                    Threading.Thread.Sleep(1)
                    Invalidate()
                End If
            End If
        End Sub
#End Region

        Private SZ1 As SizeF

        Protected Overrides Sub OnPaint(e As PaintEventArgs)
            Try
                Dim G As Graphics = e.Graphics
                If Parent Is Nothing Then Exit Sub
                BackColor = Parent.BackColor
                Dim clr As Color = My.Style.Colors.Core

                G = e.Graphics
                G.SmoothingMode = SmoothingMode.AntiAlias
                G.TextRenderingHint = If(DesignMode, TextRenderingHint.ClearTypeGridFit, TextRenderingHint.SystemDefault)
                DoubleBuffered = True

                '################################################################################# Customizer
                SZ1 = G.MeasureString(Text, Font)

                Dim format As New StringFormat()
                Dim OuterCircle As New Rectangle(3, 4, Height - 8, Height - 8)
                Dim InnerCircle As New Rectangle(4, 5, Height - 10, Height - 10)
                Dim CheckCircle As New Rectangle(7, 8, Height - 16, Height - 16)
                Dim TextRect As New Rectangle(Height - 1, (CLng((Height - SZ1.Height)) \ 2) + 1, Width - OuterCircle.Width, Height - 1)
                Dim RTL As Boolean = (RightToLeft = 1)

                If RTL Then
                    format = New StringFormat(StringFormatFlags.DirectionRightToLeft)
                    OuterCircle.X = Width - OuterCircle.X - OuterCircle.Width
                    InnerCircle.X = Width - InnerCircle.X - InnerCircle.Width
                    CheckCircle.X = Width - CheckCircle.X - CheckCircle.Width
                    TextRect.Width -= OuterCircle.Width + 13
                End If

#Region "Colors System"
                Dim HoverCircle_Color As Color = Color.FromArgb(alpha2, My.Style.Colors.Back_Checked)
                Dim HoverCheckedCircle_Color As Color = Color.FromArgb(alpha, My.Style.Colors.Border_Checked_Hover)
                Dim CheckCircle_Color As Color = Color.FromArgb(alpha2, My.Style.Colors.Core)
                Dim NonHoverCircle_Color As Color = My.Style.Colors.Back_Hover
                Dim BackCircle_Color As Color = My.Style.Colors.Back
                Dim ParentColor As Color = MyBase.GetParentColor
#End Region
                '#################################################################################

                G.Clear(ParentColor)
                Using br As New SolidBrush(BackCircle_Color) : G.FillEllipse(br, OuterCircle) : End Using

                If Checked Then
                    Using br As New SolidBrush(HoverCircle_Color) : G.FillEllipse(br, OuterCircle) : End Using
                    Using br As New SolidBrush(CheckCircle_Color) : G.FillEllipse(br, CheckCircle) : End Using
                    Using P As New Pen(HoverCheckedCircle_Color) : G.DrawEllipse(P, OuterCircle) : End Using
                Else
                    Using br As New SolidBrush(HoverCircle_Color) : G.FillEllipse(br, OuterCircle) : End Using
                    Using br As New SolidBrush(CheckCircle_Color) : G.FillEllipse(br, CheckCircle) : End Using
                    Using P As New Pen(Color.FromArgb(255 - alpha, NonHoverCircle_Color)) : G.DrawEllipse(P, InnerCircle) : End Using
                    Using P As New Pen(Color.FromArgb(alpha, clr)) : G.DrawEllipse(P, OuterCircle) : End Using
                End If

#Region "Strings"
                If Checked Then
                    Using br As New SolidBrush(Color.FromArgb(255 - alpha, ForeColor)) : G.DrawString(Text, Font, br, TextRect, format) : End Using
                    Using br As New SolidBrush(Color.FromArgb(alpha, CheckCircle_Color)) : G.DrawString(Text, Font, br, TextRect, format) : End Using
                Else
                    Using br As New SolidBrush(ForeColor) : G.DrawString(Text, Font, br, TextRect, format) : End Using
                End If
#End Region
            Catch

            End Try
        End Sub

    End Class

End Namespace

