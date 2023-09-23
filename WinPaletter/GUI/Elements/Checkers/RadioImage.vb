Imports System.ComponentModel
Imports System.Drawing.Drawing2D
Imports System.Drawing.Text

Namespace UI.WP

    <Description("RadioButton, but with image for WinPaletter UI")> <DefaultEvent("CheckedChanged")> Public Class RadioImage : Inherits Control

        Event CheckedChanged(sender As Object)

        Sub New()
            SetStyle(DirectCast(139286, ControlStyles), True)
            SetStyle(ControlStyles.Selectable, True)
            DoubleBuffered = True
            Font = New Font("Segoe UI", 9)
            ForeColor = Color.White
            Text = ""
        End Sub

#Region "Properties"
        Private Sub InvalidateParent()
            If Parent Is Nothing Then Return

            For Each C As Control In Parent.Controls
                If Not (C Is Me) AndAlso (TypeOf C Is RadioImage) Then
                    DirectCast(C, RadioImage).Checked = False
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

                    Invalidate()

                Catch
                End Try
            End Set
        End Property

        Public Property Image As Image
        Private _Checked As Boolean
        Public Property ShowText As Boolean = False

        <Browsable(True)>
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
        <EditorBrowsable(EditorBrowsableState.Always)>
        <Editor(GetType(System.ComponentModel.Design.MultilineStringEditor), GetType(System.Drawing.Design.UITypeEditor))>
        <Bindable(True)>
        Public Overrides Property Text As String = ""
#End Region

#Region "Events"
        Enum MouseState
            None
            Over
            Down
        End Enum

        Public State As MouseState = MouseState.None
        Private AnimateOnClick As Boolean = False

        Protected Overrides Sub OnDragOver(drgevent As DragEventArgs)
            If Not Checked AndAlso TypeOf drgevent.Data.GetData("WinPaletter.UI.Controllers.ColorItem") Is UI.Controllers.ColorItem Then
                drgevent.Effect = DragDropEffects.None
                Checked = True
            Else
                Exit Sub
            End If
            MyBase.OnDragOver(drgevent)
        End Sub
        Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
            AnimateOnClick = True
            Checked = True
            State = MouseState.Down
            Tmr.Enabled = True
            Tmr.Start()
            Invalidate()
            MyBase.OnMouseDown(e)
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

        Private Sub RadioImage_HandleCreated(sender As Object, e As EventArgs) Handles Me.HandleCreated

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
            Catch
            End Try
        End Sub

        Private Sub RadioImage_HandleDestroyed(sender As Object, e As EventArgs) Handles Me.HandleDestroyed
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
        Dim alpha As Integer
        ReadOnly Factor As Integer = 25
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
                        AnimateOnClick = False
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
                        AnimateOnClick = False
                    End If

                    Threading.Thread.Sleep(1)
                    Invalidate()
                End If
            End If
        End Sub
#End Region
        Protected Overrides Sub OnPaint(e As System.Windows.Forms.PaintEventArgs)
            Try
                Dim G As Graphics = e.Graphics
                If Parent Is Nothing Then Exit Sub
                G.Clear(GetParentColor)

                G = e.Graphics
                G.SmoothingMode = SmoothingMode.AntiAlias
                G.TextRenderingHint = If(DesignMode, TextRenderingHint.ClearTypeGridFit, TextRenderingHint.SystemDefault)
                DoubleBuffered = True

                Dim MainRect As New Rectangle(0, 0, Width - 1, Height - 1)
                Dim MainRectInner As New Rectangle(1, 1, Width - 3, Height - 3)
                Dim CenterRect As New Rectangle

                If Image IsNot Nothing Then CenterRect = New Rectangle(MainRect.X + (MainRect.Width - Image.Width) / 2,
                                            MainRect.Y + (MainRect.Height - Image.Height) / 2,
                                            Image.Width, Image.Height)

                Dim bkC As Color = If(_Checked, My.Style.Colors.Back_Checked, My.Style.Colors.Back)
                Dim bkCC As Color = Color.FromArgb(alpha, My.Style.Colors.Back_Checked)

                Using br As New SolidBrush(bkC) : G.FillRoundedRect(br, MainRectInner) : End Using
                Using br As New SolidBrush(bkCC) : G.FillRoundedRect(br, MainRect) : End Using

                Dim lC As Color = Color.FromArgb(255 - alpha, If(_Checked, My.Style.Colors.Border_Checked, My.Style.Colors.Border))
                Dim lCC As Color = Color.FromArgb(alpha, My.Style.Colors.Border_Checked_Hover)

                Using P As New Pen(lC) : G.DrawRoundedRect_LikeW11(P, MainRectInner) : End Using
                Using P As New Pen(lCC) : G.DrawRoundedRect_LikeW11(P, MainRect) : End Using

                If Image IsNot Nothing Then G.DrawImage(Image, CenterRect)

                If ShowText Then
                    Using br As New SolidBrush(ForeColor) : G.DrawString(Text, Font, br, MainRectInner, ContentAlignment.MiddleCenter.ToStringFormat) : End Using
                End If
            Catch

            End Try
        End Sub

    End Class

End Namespace

