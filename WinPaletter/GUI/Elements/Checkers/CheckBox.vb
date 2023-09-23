Imports System.ComponentModel
Imports System.Drawing.Drawing2D
Imports System.Drawing.Text

Namespace UI.WP

    <Description("Themed CheckBox for WinPaletter UI")> <DefaultEvent("CheckedChanged")> Public Class CheckBox : Inherits Control

        Event CheckedChanged(sender As Object)

        Sub New()
            SetStyle(DirectCast(139286, ControlStyles), True)
            SetStyle(ControlStyles.Selectable, False)
            DoubleBuffered = True
            Font = New Font("Segoe UI", 9)
            ForeColor = Color.White
        End Sub

#Region "Properties"
        Public Property Checked() As Boolean
            Get
                Return _Checked
            End Get
            Set(value As Boolean)
                Try
                    _Checked = value
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

        ReadOnly Radius As Integer = 5

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
            State = MouseState.Down
            Tmr.Enabled = True
            Tmr.Start()
            Invalidate()
        End Sub

        Protected Overrides Sub OnMouseClick(e As MouseEventArgs)
            AnimateOnClick = True
            Checked = Not Checked
            State = MouseState.Down
            Tmr2.Enabled = True
            Tmr2.Start()
            Invalidate()
        End Sub

        Protected Overrides Sub OnMouseUp(e As MouseEventArgs)
            State = MouseState.Over
            Tmr.Enabled = True
            Tmr.Start()
            Invalidate()
        End Sub

        Private Sub XenonCheckBox_MouseEnter(sender As Object, e As EventArgs) Handles Me.MouseEnter
            State = MouseState.Over
            Tmr.Enabled = True
            Tmr.Start()
            Invalidate()
        End Sub

        Private Sub XenonCheckBox_MouseLeave(sender As Object, e As EventArgs) Handles Me.MouseLeave
            State = MouseState.None
            Tmr.Enabled = True
            Tmr.Start()
            Invalidate()
        End Sub

        Private Sub XenonCheckbox_HandleCreated(sender As Object, e As EventArgs) Handles Me.HandleCreated
            Try
                alpha = If(DesignMode, 255, 0)
                alpha2 = If(Checked, 255, 0)

                If Not DesignMode Then
                    Try
                        AddHandler FindForm.Shown, AddressOf Showed
                        AddHandler Parent.BackColorChanged, AddressOf RefreshColorPalette
                        AddHandler Parent.VisibleChanged, AddressOf RefreshColorPalette
                        AddHandler Parent.EnabledChanged, AddressOf RefreshColorPalette
                    Catch
                    End Try
                End If
            Catch
            End Try

        End Sub

        Private Sub XenonCheckBox_HandleDestroyed(sender As Object, e As EventArgs) Handles Me.HandleDestroyed
            If Not DesignMode Then
                Try
                    RemoveHandler FindForm.Shown, AddressOf Showed
                    RemoveHandler Parent.BackColorChanged, AddressOf RefreshColorPalette
                    RemoveHandler Parent.VisibleChanged, AddressOf RefreshColorPalette
                    RemoveHandler Parent.EnabledChanged, AddressOf RefreshColorPalette
                Catch
                End Try
            End If
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
        Private WithEvents Tmr, Tmr2 As New Timer With {.Enabled = False, .Interval = 1}
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
                    Refresh()
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
                    Refresh()
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
                    Refresh()
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
                    Refresh()

                End If
            End If
        End Sub
#End Region

        Protected Overrides Sub OnPaint(e As System.Windows.Forms.PaintEventArgs)
            Try
                If Parent Is Nothing Then Exit Sub
                BackColor = Parent.BackColor

                Dim G As Graphics = e.Graphics
                G.SmoothingMode = SmoothingMode.AntiAlias
                G.TextRenderingHint = If(DesignMode, TextRenderingHint.ClearTypeGridFit, TextRenderingHint.SystemDefault)
                DoubleBuffered = True

                '################################################################################# Customizer
                Dim format As New StringFormat()

                Dim SZ1 As SizeF = G.MeasureString(Text, Font)
                Dim PT1 As New PointF(Height - 1, (CLng((Height - SZ1.Height)) \ 2) + 1)

                Dim OuterCheckRect As New Rectangle(3, 4, Height - 8, Height - 8)
                Dim InnerCheckRect As New Rectangle(4, 5, Height - 10, Height - 10)
                Dim TextRect As New Rectangle(Height - 1, (CLng((Height - SZ1.Height)) \ 2) + 1, Width - InnerCheckRect.Width, Height - 1)

#Region "Colors System"
                Dim HoverRect_Color As Color
                Dim HoverCheckedRect_Color As Color
                Dim CheckRect_Color As Color
                Dim NonHoverRect_Color As Color
                Dim BackRect_Color As Color
                Dim ParentColor As Color = MyBase.GetParentColor

                If Enabled Then
                    HoverRect_Color = Color.FromArgb(alpha2, My.Style.Colors.Back_Checked)
                    HoverCheckedRect_Color = Color.FromArgb(alpha, My.Style.Colors.Border_Checked_Hover)
                    CheckRect_Color = Color.FromArgb(alpha2, My.Style.Colors.Core)
                    NonHoverRect_Color = My.Style.Colors.Border
                    BackRect_Color = My.Style.Colors.Back
                Else
                    HoverRect_Color = Color.FromArgb(alpha2, My.Style.Disabled_Colors.Back_Checked)
                    HoverCheckedRect_Color = Color.FromArgb(alpha, My.Style.Disabled_Colors.Border_Checked_Hover)
                    CheckRect_Color = Color.FromArgb(alpha2, My.Style.Disabled_Colors.Core)
                    NonHoverRect_Color = My.Style.Disabled_Colors.Border
                    BackRect_Color = My.Style.Disabled_Colors.Back
                End If

#End Region

                Dim RTL As Boolean = (RightToLeft = 1)

                If RTL Then
                    format = New StringFormat(StringFormatFlags.DirectionRightToLeft)
                    OuterCheckRect.X = Width - OuterCheckRect.X - OuterCheckRect.Width
                    InnerCheckRect.X = Width - InnerCheckRect.X - InnerCheckRect.Width
                    TextRect.Width = Width - InnerCheckRect.Width - 10
                    TextRect.X = 0
                End If

#Region "Check Sign x,y system"
                Dim x1_Left As Integer = InnerCheckRect.X + 3
                Dim y1_Left As Integer = CInt(0.8 * InnerCheckRect.Height)
                Dim x2_Left As Integer = x1_Left
                Dim y2_Left As Integer = InnerCheckRect.Y + InnerCheckRect.Height - 3

                Dim x1_Right As Integer = x2_Left
                Dim y1_Right As Integer = y2_Left
                Dim x2_Right As Integer = InnerCheckRect.Right - 2
                Dim y2_Right As Integer = y1_Left - 3

                Using CheckSignPen As New Pen(CheckRect_Color, 1.8F)
#End Region
                    '#################################################################################

                    G.Clear(ParentColor)

                    Using br As New SolidBrush(My.Style.Colors.Back) : G.FillRoundedRect(br, InnerCheckRect, Radius) : End Using

                    If _Checked Then
                        Using br As New SolidBrush(HoverRect_Color) : G.FillRoundedRect(br, InnerCheckRect, Radius) : End Using
                        Using br As New SolidBrush(Color.FromArgb(alpha, HoverRect_Color)) : G.FillRoundedRect(br, OuterCheckRect, Radius) : End Using

                        Using P As New Pen(Color.FromArgb(255 - alpha, HoverCheckedRect_Color)) : G.DrawRoundedRect(P, InnerCheckRect, Radius) : End Using
                        Using P As New Pen(Color.FromArgb(alpha, HoverCheckedRect_Color)) : G.DrawRoundedRect(P, OuterCheckRect, Radius) : End Using

                        G.DrawLine(CheckSignPen, x1_Left, y1_Left, x2_Left, y2_Left)
                        G.DrawLine(CheckSignPen, x1_Right, y1_Right, x2_Right, y2_Right)
                    Else
                        Using br As New SolidBrush(HoverRect_Color) : G.FillRoundedRect(br, OuterCheckRect, Radius) : End Using
                        Using P As New Pen(HoverCheckedRect_Color) : G.DrawRoundedRect(P, OuterCheckRect, Radius) : End Using

                        G.DrawLine(CheckSignPen, x1_Left, y1_Left, x2_Left, y2_Left)
                        G.DrawLine(CheckSignPen, x1_Right, y1_Right, x2_Right, y2_Right)

                        Using P As New Pen(Color.FromArgb(255 - alpha, My.Style.Colors.Back_Hover)) : G.DrawRoundedRect(P, InnerCheckRect, Radius) : End Using
                    End If

                    If Checked Then
                        Using br As New SolidBrush(Color.FromArgb(255 - alpha, ForeColor)) : G.DrawString(Text, Font, br, TextRect, format) : End Using
                        Using br As New SolidBrush(Color.FromArgb(alpha, CheckRect_Color)) : G.DrawString(Text, Font, br, TextRect, format) : End Using
                    Else
                        Using br As New SolidBrush(ForeColor) : G.DrawString(Text, Font, br, TextRect, format) : End Using
                    End If
                End Using
            Catch
            End Try
        End Sub
    End Class

End Namespace