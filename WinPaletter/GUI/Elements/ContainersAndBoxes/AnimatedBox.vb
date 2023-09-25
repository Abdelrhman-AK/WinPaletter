Imports System.ComponentModel
Imports System.Drawing.Drawing2D

Namespace UI.WP

    <Description("AnimatedBox with two colors for WinPaletter UI")> <DefaultEvent("Click")> Public Class AnimatedBox : Inherits Panel

        Sub New()
            SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint Or ControlStyles.ResizeRedraw, True)
            DoubleBuffered = True
            Text = ""
        End Sub

#Region "Variables"

        Private LineColor As Color
        Private _Angle As Single = 0
        Dim C1, C2 As Color
        Private _Focused As Boolean = True
        ReadOnly Noise As New TextureBrush(My.Resources.GaussianBlur.Fade(0.7))

        Enum Styles
            SwapColors
            MixedColors
        End Enum

#End Region

#Region "Properties"
        Public Property Color1 As Color = Color.DodgerBlue
        Public Property Color2 As Color = Color.Crimson
        Public Property [Color] As Color = Color1
        Public Property Style As Styles = Styles.SwapColors

        <Browsable(True)>
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
        <EditorBrowsable(EditorBrowsableState.Always)>
        <Editor(GetType(System.ComponentModel.Design.MultilineStringEditor), GetType(System.Drawing.Design.UITypeEditor))>
        <Bindable(True)>
        Public Overrides Property Text As String = ""

#End Region

#Region "Animator"

        Private WithEvents Timer As New Timer With {.Enabled = False, .Interval = 1}

        Private Sub Timer_Tick(sender As Object, e As EventArgs) Handles Timer.Tick
            If Not DesignMode Then

                If _Angle + 1.5 > 360 Then
                    _Angle = 0

                    If Style = Styles.SwapColors Then
                        Dim Cx1, Cx2 As Color

                        If My.Style.DarkMode Then
                            Cx1 = Color1.Dark(0.15)
                            Cx2 = Color2.Dark(0.15)
                        Else
                            Cx1 = Color1.Light(0.6)
                            Cx2 = Color2.Light(0.6)
                        End If

                        If [Color] = Cx1 Or [Color] = Color1 Then
                            Visual.FadeColor(Me, "Color", [Color], Cx2, 10, 1)
                        Else
                            Visual.FadeColor(Me, "Color", [Color], Cx1, 10, 1)
                        End If

                    End If

                Else
                    _Angle += 1.5
                End If

                Refresh()
            Else
                Timer.Enabled = False
                Timer.Stop()
            End If
        End Sub

#End Region

#Region "Events"

        Private Sub AnimatedBox_HandleCreated(sender As Object, e As EventArgs) Handles Me.HandleCreated
            If Not DesignMode Then
                Timer.Enabled = True
                Timer.Start()

                Try : AddHandler FindForm.Activated, AddressOf Form_GotFocus : Catch : End Try
                Try : AddHandler FindForm.Deactivate, AddressOf Form_LostFocus : Catch : End Try

            Else
                Timer.Enabled = False
                Timer.Stop()
            End If
        End Sub

        Private Sub AnimatedBox_HandleDestroyed(sender As Object, e As EventArgs) Handles Me.HandleDestroyed
            If Not DesignMode Then
                Try : RemoveHandler FindForm.Activated, AddressOf Form_GotFocus : Catch : End Try
                Try : RemoveHandler FindForm.Deactivate, AddressOf Form_LostFocus : Catch : End Try
            End If
        End Sub

        Sub Form_GotFocus(sender As Object, e As EventArgs)
            _Focused = True
            Timer.Enabled = True
            Timer.Start()
            Invalidate()
        End Sub

        Sub Form_LostFocus(sender As Object, e As EventArgs)
            _Focused = False
            Timer.Enabled = False
            Timer.Stop()
            Invalidate()
        End Sub

#End Region

        Protected Overrides Sub OnPaint(e As PaintEventArgs)
            MyBase.OnPaint(e)
            Dim G As Graphics = e.Graphics
            DoubleBuffered = True

            G.SmoothingMode = SmoothingMode.AntiAlias

            Dim Rect As New Rectangle(0, 0, Width - 1, Height - 1)

            G.Clear(GetParentColor)

            If Not DesignMode AndAlso _Focused Then

                If Style = Styles.SwapColors Then
                    If My.Style.DarkMode Then
                        C1 = [Color].Dark(0.15)
                    Else
                        C1 = [Color].Light(0.6)
                    End If

                    C2 = GetParentColor
                ElseIf Style = Styles.MixedColors Then

                    If My.Style.DarkMode Then
                        C1 = Color1.Dark(0.15)
                        C2 = Color2.Dark(0.15)
                    Else
                        C1 = Color1.Light(0.6)
                        C2 = Color2.Light(0.6)
                    End If

                End If

                Using l As New LinearGradientBrush(Rect, C1, C2, _Angle, False)

                    LineColor = Color.FromArgb(120, 150, 150, 150)

                    If Dock = DockStyle.None Then
                        G.FillRoundedRect(l, Rect)
                        G.FillRoundedRect(Noise, Rect)
                        Using P As New Pen(LineColor) : G.DrawRoundedRect(P, Rect) : End Using
                    Else
                        G.FillRectangle(l, Rect)
                        G.FillRectangle(Noise, Rect)
                    End If
                End Using

            End If

        End Sub

    End Class

End Namespace

