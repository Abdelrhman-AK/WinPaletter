Imports System.ComponentModel
Imports System.Drawing.Drawing2D

Namespace UI.WP

    <Description("Themed toggle for WinPaletter UI")> <DefaultEvent("CheckedChanged")> Public Class Toggle : Inherits UserControl

        Dim CheckC As New Rectangle(4, 4, 11, 11)
        Dim MouseState As Integer = 0
        Dim WasMoving As Boolean = False

        Sub New()
            DoubleBuffered = True
            Size = New Size(40, 20)
            Text = ""
        End Sub

        Public Property DarkLight_Toggler As Boolean = False

        ReadOnly DarkLight_TogglerSize As Integer = 13

        Private _checked As Boolean

        Private _Shown As Boolean = False

        <Browsable(True)>
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
        <EditorBrowsable(EditorBrowsableState.Always)>
        <Editor(GetType(System.ComponentModel.Design.MultilineStringEditor), GetType(System.Drawing.Design.UITypeEditor))>
        <Bindable(True)>
        Public Overrides Property Text As String = ""

        Public Property Checked As Boolean
            Get
                Return _checked
            End Get
            Set(value As Boolean)
                If Not _checked.Equals(value) Then
                    _checked = value
                    Me.OnCheckedChanged()
                End If
            End Set
        End Property

        Protected Overridable Sub OnCheckedChanged()
            RaiseEvent CheckedChanged(Me, EventArgs.Empty)

            If Not DesignMode And _Shown And AnimateOnClick Then
                If Checked Then

                    Dim s As Integer = (Width - 17) * 0.5
                    For i As Integer = CheckC.Left To Width - 17 Step +5
                        CheckC.X = i + s
                        Threading.Thread.Sleep(1)
                        Refresh()
                        If i + s >= Width - 17 Then Exit For
                        s -= 1
                        If s < 0 Then s = 0
                    Next
                    CheckC.X = Width - 17

                Else

                    Dim s As Integer = 10
                    For i As Integer = CheckC.Left To 4 Step -5
                        CheckC.X = i - s
                        Threading.Thread.Sleep(1)
                        Refresh()
                        If i - s <= 4 Then Exit For
                        s -= 1
                        If s < 0 Then s = 0
                    Next
                    CheckC.X = 4

                End If

            Else
                If Checked Then
                    CheckC.X = Width - 17
                Else
                    CheckC.X = 4
                End If
            End If

            If DarkLight_Toggler Then
                CheckC.Width = DarkLight_TogglerSize
                CheckC.Height = DarkLight_TogglerSize
            End If

            Invalidate()
        End Sub
        Public Event CheckedChanged(sender As Object, e As EventArgs)

        Dim CheckedC As Rectangle

        Private AnimateOnClick As Boolean = False
        Protected Overrides Sub OnMouseClick(e As MouseEventArgs)
            AnimateOnClick = True
            Me.Checked = Not Me.Checked
            AnimateOnClick = False
            Me.Invalidate()
            MyBase.OnMouseClick(e)
        End Sub

        Protected Overrides Sub OnPaint(e As PaintEventArgs)
            Me.OnPaintBackground(e)
            Dim G As Graphics = e.Graphics
            G.SmoothingMode = SmoothingMode.AntiAlias
            DoubleBuffered = True

            If Parent Is Nothing Then Exit Sub

            BackColor = My.Style.Colors.Back

            G.Clear(GetParentColor)

            '################################################################################# Customizer
            Dim MainRect As New Rectangle(0, 0, Width - 1, Height - 1)
            Dim BorderColor As Color

            If My.Style.DarkMode Then BorderColor = BackColor.LightLight Else BorderColor = BackColor.Dark(0.5)

            Dim CheckColor As Color
            If MouseState = 0 Then CheckColor = My.Style.Colors.BaseColor Else CheckColor = BackColor.CB(If(My.Style.DarkMode, 0.3, -0.5))

            '#################################################################################
            Dim min As Integer = 4
            Dim max As Integer = Width - 17
            Dim val As Decimal = (CheckC.X) / max

            If val < 0 Then val = 0
            If val > 1 Then val = 1
            If CheckC.X <= min Then val = 0
            If CheckC.X >= max Then val = 1

            Dim lgbChecked, lgbNonChecked, lgborderChecked, lgborderNonChecked As LinearGradientBrush

            lgbChecked = New LinearGradientBrush(MainRect, Color.FromArgb(255 * val, My.Style.Colors.Border_Checked_Hover), Color.FromArgb(255 * val, My.Style.Colors.Back_Checked), LinearGradientMode.ForwardDiagonal)
            lgborderChecked = New LinearGradientBrush(MainRect, Color.FromArgb(255 * val, My.Style.Colors.Border_Checked), Color.FromArgb(255 * val, My.Style.Colors.Back_Checked), LinearGradientMode.BackwardDiagonal)
            lgbNonChecked = New LinearGradientBrush(MainRect, Color.FromArgb(255 * (1 - val), My.Style.Colors.Back_Checked), Color.FromArgb(255 * (1 - val), My.Style.Colors.Border_Checked_Hover), LinearGradientMode.BackwardDiagonal)
            lgborderNonChecked = New LinearGradientBrush(MainRect, Color.FromArgb(255 * (1 - val), My.Style.Colors.Border_Checked), Color.FromArgb(255 * (1 - val), My.Style.Colors.Back_Checked), LinearGradientMode.ForwardDiagonal)

            If Not DarkLight_Toggler Then

                Using br As New SolidBrush(Color.FromArgb(255 * val, My.Style.Colors.Border_Checked_Hover)) : e.Graphics.FillRoundedRect(br, MainRect, 9, True) : End Using

                Using P As New Pen(Color.FromArgb(255 * val, My.Style.Colors.Border_Checked)) : e.Graphics.DrawRoundedRect(P, MainRect, 9, True) : End Using

                Using P As New Pen(Color.FromArgb(255 * (1 - val), BorderColor)) : e.Graphics.DrawRoundedRect(P, MainRect, 9, True) : End Using

                If Checked Then
                    Using br As New SolidBrush(If(My.Style.DarkMode, Color.Black, Color.White)) : G.FillEllipse(br, CheckC) : End Using
                Else
                    Using br As New SolidBrush(BorderColor) : G.FillEllipse(br, CheckC) : End Using
                End If

            Else

                e.Graphics.FillRoundedRect(lgbChecked, MainRect, 9, True)
                e.Graphics.FillRoundedRect(lgbNonChecked, MainRect, 9, True)

                If Checked Then
                    G.DrawImage(If(BorderColor.IsDark, My.Resources.darkmode_dark, My.Resources.darkmode_light).Fade(val), CheckC)
                    G.DrawImage(If(BorderColor.IsDark, My.Resources.lightmode_dark, My.Resources.lightmode_light).Fade(1 - val), CheckC)
                Else
                    G.DrawImage(If(My.Style.Colors.BaseColor.IsDark, My.Resources.darkmode_dark, My.Resources.darkmode_light).Fade(val), CheckC)
                    G.DrawImage(If(My.Style.Colors.BaseColor.IsDark, My.Resources.lightmode_dark, My.Resources.lightmode_light).Fade(1 - val), CheckC)
                End If

                Using P As New Pen(lgborderChecked) : e.Graphics.DrawRoundedRect(P, MainRect, 9, True) : End Using
                Using P As New Pen(lgborderNonChecked) : e.Graphics.DrawRoundedRect(P, MainRect, 9, True) : End Using
            End If
        End Sub

        Private Sub Toggle_Resize(sender As Object, e As EventArgs) Handles Me.Resize
            Me.Height = 20
            If Width < 40 Then Width = 40

            If DarkLight_Toggler Then
                CheckC.Width = DarkLight_TogglerSize
                CheckC.Height = DarkLight_TogglerSize
            End If

            Refresh()
        End Sub

        Private Sub Toggle_HandleCreated(sender As Object, e As EventArgs) Handles Me.HandleCreated

            If Checked Then
                CheckC = New Rectangle(Width - 17, 4, 11, 11)
            Else
                CheckC = New Rectangle(4, 4, 11, 11)
            End If

            If DarkLight_Toggler Then
                CheckC.Width = DarkLight_TogglerSize
                CheckC.Height = DarkLight_TogglerSize
            End If

            If Not DesignMode Then
                Try
                    AddHandler FindForm.Load, AddressOf Loaded
                    AddHandler FindForm.Shown, AddressOf Showed
                    AddHandler Parent.BackColorChanged, AddressOf RefreshColorPalette
                Catch
                End Try
            End If
        End Sub

        Private Sub Toggle_HandleDestroyed(sender As Object, e As EventArgs) Handles Me.HandleDestroyed
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

        Private Sub Toggle_MouseMove(sender As Object, e As MouseEventArgs) Handles Me.MouseMove
            If e.Button = MouseButtons.Left Then

                Dim i As Integer = e.X - 0.5 * CheckC.Width
                WasMoving = True
                MouseState = 1

                If i <= 4 Then
                    CheckC.X = 4
                ElseIf i >= Width - 17 Then
                    CheckC.X = Width - 17
                Else
                    CheckC.X = i
                End If

                CheckC.Y = 4
                Refresh()
            End If
        End Sub

        Private Sub Toggle_MouseUp(sender As Object, e As MouseEventArgs) Handles Me.MouseUp
            MouseState = 0
            CheckC.Width = 11

            If DarkLight_Toggler Then
                CheckC.Width = DarkLight_TogglerSize
                CheckC.Height = DarkLight_TogglerSize
            End If

            If WasMoving Then
                If e.X < Width * 0.5 Then Checked = False
                If e.X >= Width * 0.5 Then Checked = True
                WasMoving = False
            End If
            Refresh()
        End Sub

        Private Sub Toggle_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
            MouseState = 1
            CheckC.Width = 13

            Refresh()
        End Sub

    End Class

End Namespace