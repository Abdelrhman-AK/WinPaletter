Imports System.ComponentModel
Imports System.Drawing.Drawing2D
Imports System.Drawing.Text

Namespace UI.WP

    <Description("Themed ComboBox for WinPaletter UI")> Public Class ComboBox : Inherits Windows.Forms.ComboBox

        Sub New()
            SetStyle(ControlStyles.AllPaintingInWmPaint, True)
            SetStyle(ControlStyles.ResizeRedraw, True)
            SetStyle(ControlStyles.UserPaint, True)
            SetStyle(ControlStyles.DoubleBuffer, True)
            Size = New Size(190, 27)
            DrawMode = DrawMode.OwnerDrawVariable
            ItemHeight = 20

            If My.Style.DarkMode Then BackColor = Color.FromArgb(55, 55, 55) Else BackColor = Color.FromArgb(225, 225, 225)
            ForeColor = Color.White
            DropDownStyle = ComboBoxStyle.DropDownList
            Font = New Font("Segoe UI", 9)
            DoubleBuffered = True
        End Sub

#Region "Variables"

        ReadOnly Noise As New TextureBrush(My.Resources.GaussianBlur.Fade(0.3))
        Private _Shown As Boolean = False

        Dim State As MouseState = MouseState.None

        Enum MouseState
            None
            Over
            Down
        End Enum


#End Region

#Region "Subs"

        Protected Sub DrawTriangle(Clr As Color, FirstPoint As Point, SecondPoint As Point, ThirdPoint As Point, G As Graphics)
            Dim points As New List(Of Point) From {FirstPoint, SecondPoint, ThirdPoint}
            Using br As New SolidBrush(Clr) : G.FillPolygon(br, points.ToArray()) : End Using
        End Sub

#End Region

#Region "Events"

        Protected Overrides Sub OnMouseEnter(e As EventArgs)
            MyBase.OnMouseEnter(e)
            State = MouseState.Over
            _Shown = True
            Timer1.Enabled = True
            Timer1.Start()
            Invalidate()
        End Sub

        Protected Overrides Sub OnMouseLeave(e As EventArgs)
            MyBase.OnMouseLeave(e)
            State = MouseState.None
            _Shown = True
            Timer1.Enabled = True
            Timer1.Start()
            Invalidate()
        End Sub

        Private Sub ComboBox_MouseWheel(sender As Object, e As MouseEventArgs) Handles Me.MouseWheel
            Try
                If e.Delta < 0 Then
                    If SelectedIndex < Items.Count - 1 Then
                        If e.Delta <= -240 Then SelectedIndex += 1
                    End If
                Else
                    If SelectedIndex > 0 Then
                        If e.Delta >= 240 Then SelectedIndex -= 1
                    End If
                End If
            Catch
            End Try
        End Sub

        Private Sub ComboBox_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
            State = MouseState.Down
            _Shown = True
            Timer1.Enabled = True
            Timer1.Start()
            Invalidate()
        End Sub

        Private Sub ComboBox_Click(sender As Object, e As EventArgs) Handles Me.MouseUp
            State = MouseState.Over
            _Shown = True
            Timer1.Enabled = True
            Timer1.Start()
            Invalidate()
        End Sub

        Private Sub ComboBox_HandleCreated(sender As Object, e As EventArgs) Handles Me.HandleCreated
            alpha = 0
            alpha2 = 0
            Try
                If Not DesignMode Then
                    If Parent IsNot Nothing Then AddHandler Parent.BackColorChanged, AddressOf Invalidator
                    AddHandler BackColorChanged, AddressOf Invalidator
                End If
            Catch
            End Try
        End Sub

        Private Sub ComboBox_HandleDestroyed(sender As Object, e As EventArgs) Handles Me.HandleDestroyed
            Try
                If Not DesignMode Then
                    If Parent IsNot Nothing Then RemoveHandler Parent.BackColorChanged, AddressOf Invalidator
                    RemoveHandler BackColorChanged, AddressOf Invalidator
                End If
            Catch
            End Try
        End Sub

        Sub Invalidator(sender As Object, e As EventArgs)
            Invalidate()
        End Sub

        Private Sub ComboBox_DropDown(sender As Object, e As EventArgs) Handles Me.DropDown
            If _Shown Then
                Timer2.Enabled = True
                Timer2.Start()
            End If
        End Sub

        Private Sub ComboBox_DropDownClosed(sender As Object, e As EventArgs) Handles Me.DropDownClosed
            If _Shown Then
                Timer2.Enabled = True
                Timer2.Start()
            End If
        End Sub

#End Region

#Region "Animator"

        Dim alpha, alpha2 As Integer
        ReadOnly Factor As Integer = 20
        Dim WithEvents Timer1, Timer2 As New Timer With {.Enabled = False, .Interval = 1}

        Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
            If Not DesignMode Then

                If State = MouseState.Over Then
                    If alpha + Factor <= 255 Then
                        alpha += Factor
                    ElseIf alpha + Factor > 255 Then
                        alpha = 255
                        Timer1.Enabled = False
                        Timer1.Stop()
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
                        Timer1.Enabled = False
                        Timer1.Stop()
                    End If

                    If _Shown Then
                        Threading.Thread.Sleep(1)
                        Invalidate()
                    End If
                End If
            End If
        End Sub

        Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
            If Not DesignMode Then

                If DroppedDown Then
                    If alpha2 + Factor <= 255 Then
                        alpha2 += Factor
                    ElseIf alpha2 + Factor > 255 Then
                        alpha2 = 255
                        Timer2.Enabled = False
                        Timer2.Stop()
                    End If

                    If _Shown Then
                        Threading.Thread.Sleep(1)
                        Invalidate()
                    End If
                End If

                If Not DroppedDown Then
                    If alpha2 - Factor >= 0 Then
                        alpha2 -= Factor
                    ElseIf alpha2 - Factor < 0 Then
                        alpha2 = 0
                        Timer2.Enabled = False
                        Timer2.Stop()
                    End If

                    If _Shown Then
                        Threading.Thread.Sleep(1)
                        Invalidate()
                    End If
                End If
            End If
        End Sub

#End Region

        Sub ComboBox_DrawItem(sender As System.Object, e As DrawItemEventArgs) Handles Me.DrawItem
            BackColor = My.Style.Colors.Back
            e.DrawBackground()

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias
            e.Graphics.TextRenderingHint = If(DesignMode, TextRenderingHint.ClearTypeGridFit, TextRenderingHint.SystemDefault)

            If BackColor.IsDark Then
                If ForeColor <> Color.White Then ForeColor = Color.White
            Else
                If ForeColor <> Color.Black Then ForeColor = Color.Black
            End If

            Using br As New SolidBrush(BackColor) : e.Graphics.FillRectangle(br, New Rectangle(e.Bounds.X - 2, e.Bounds.Y - 2, e.Bounds.Width + 4, e.Bounds.Height + 4)) : End Using

            If (e.State And DrawItemState.Selected) = DrawItemState.Selected Then
                Using br As New SolidBrush(My.Style.Colors.Border_Checked_Hover) : e.Graphics.FillRectangle(br, e.Bounds) : End Using
            End If

            Dim f As Font
            Try
                f = New Font(MyBase.GetItemText(MyBase.Items(e.Index)), e.Font.Size, e.Font.Style)
            Catch
                f = e.Font
            End Try

            Dim Rect As Rectangle = e.Bounds
            Rect.X += 2
            Rect.Width -= 2

            If e.Index >= 0 Then
                Using br As New SolidBrush(ForeColor) : e.Graphics.DrawString(MyBase.GetItemText(MyBase.Items(e.Index)), f, br, Rect, ContentAlignment.MiddleLeft.ToStringFormat) : End Using
            End If
        End Sub

        Protected Overrides Sub OnPaint(e As PaintEventArgs)
            Dim G As Graphics = e.Graphics
            G.SmoothingMode = SmoothingMode.AntiAlias
            G.TextRenderingHint = If(DesignMode, TextRenderingHint.ClearTypeGridFit, TextRenderingHint.SystemDefault)
            DoubleBuffered = True

            Dim OuterRect As New Rectangle(0, 0, Width - 1, Height - 1)
            Dim InnerRect As New Rectangle(1, 1, Width - 3, Height - 3)
            Dim TextRect As New Rectangle(5, 0, Width - 1, Height - 1)

            Dim FadeInColor As Color = Color.FromArgb(alpha, My.Style.Colors.Border_Checked_Hover)
            Dim FadeOutColor As Color = Color.FromArgb(255 - alpha, My.Style.Colors.Border)

            G.Clear(GetParentColor)

            Using br As New SolidBrush(My.Style.Colors.Back) : G.FillRoundedRect(br, InnerRect) : End Using
            Using br As New SolidBrush(Color.FromArgb(alpha, My.Style.Colors.Back_Checked)) : G.FillRoundedRect(br, OuterRect) : End Using
            G.FillRoundedRect(Noise, InnerRect)

            Using P As New Pen(FadeInColor) : G.DrawRoundedRect_LikeW11(P, OuterRect) : End Using
            Using P As New Pen(FadeOutColor) : G.DrawRoundedRect_LikeW11(P, InnerRect) : End Using

            Using br As New SolidBrush(Color.FromArgb(alpha2, My.Style.Colors.Back_Checked)) : G.FillRoundedRect(br, OuterRect) : End Using
            Using P As New Pen(Color.FromArgb(alpha2, My.Style.Colors.Border_Checked_Hover)) : G.DrawRoundedRect_LikeW11(P, OuterRect) : End Using

            Dim ArrowHeight As Integer = 4
            Dim Arrow_Y_1 As Integer = (Height - ArrowHeight) / 2 - 1
            Dim Arrow_Y_2 As Integer = Arrow_Y_1 + ArrowHeight

            If Focused And State = MouseState.None Then
                Using P As New Pen(Color.FromArgb(255, FadeInColor))
                    G.DrawRoundedRect(P, InnerRect)
                    G.DrawLine(P, New Point(Width - 18, Arrow_Y_1), New Point(Width - 14, Arrow_Y_2))
                    G.DrawLine(P, New Point(Width - 14, Arrow_Y_2), New Point(Width - 10, Arrow_Y_1))
                    G.DrawLine(P, New Point(Width - 14, Arrow_Y_2 + 1), New Point(Width - 14, Arrow_Y_2))
                End Using
            Else
                Using P As New Pen(Color.FromArgb(255 - alpha, ForeColor), 2)
                    G.DrawLine(P, New Point(Width - 18, Arrow_Y_1), New Point(Width - 14, Arrow_Y_2))
                    G.DrawLine(P, New Point(Width - 14, Arrow_Y_2), New Point(Width - 10, Arrow_Y_1))
                End Using

                Using P As New Pen(Color.FromArgb(255 - alpha, ForeColor)) : G.DrawLine(P, New Point(Width - 14, Arrow_Y_2 + 1), New Point(Width - 14, Arrow_Y_2)) : End Using

                If Not DroppedDown Then

                    Using P As New Pen(FadeInColor, 2)
                        G.DrawLine(P, New Point(Width - 18, Arrow_Y_1), New Point(Width - 14, Arrow_Y_2))
                        G.DrawLine(P, New Point(Width - 14, Arrow_Y_2), New Point(Width - 10, Arrow_Y_1))
                    End Using

                    Using P As New Pen(FadeInColor) : G.DrawLine(New Pen(FadeInColor), New Point(Width - 14, Arrow_Y_2 + 1), New Point(Width - 14, Arrow_Y_2)) : End Using

                Else
                    Using P As New Pen(FadeInColor) : G.DrawLine(P, New Point(Width - 14, Arrow_Y_1), New Point(Width - 14, Arrow_Y_1 + 1)) : End Using

                    Using P As New Pen(FadeInColor, 2)
                        G.DrawLine(P, New Point(Width - 18, Arrow_Y_2), New Point(Width - 14, Arrow_Y_1))
                        G.DrawLine(P, New Point(Width - 14, Arrow_Y_1), New Point(Width - 10, Arrow_Y_2))
                    End Using
                End If

            End If

            Dim f As Font
            Try
                f = New Font(Text, Font.Size, Font.Style)
            Catch
                f = Font
            End Try

            Using br As New SolidBrush(ForeColor) : G.DrawString(Text, f, br, TextRect, New StringFormat With {.LineAlignment = StringAlignment.Center, .Alignment = StringAlignment.Near}) : End Using
        End Sub

    End Class

End Namespace
