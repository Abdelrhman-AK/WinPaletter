Imports System.ComponentModel
Imports System.Drawing.Drawing2D
Imports System.Drawing.Text

Namespace UI.WP

    <Description("Themed Button for WinPaletter UI")> Public Class Button : Inherits Windows.Forms.Button

        Sub New()
        Font = New Font("Segoe UI", 9)
        ForeColor = Color.White
        If My.Style.DarkMode Then BackColor = Color.FromArgb(50, 50, 50) Else BackColor = Color.FromArgb(225, 225, 225)
        LineColor = Color.FromArgb(0, 81, 210)
        Image = MyBase.Image
        DoubleBuffered = True

        Try
            If Image IsNot Nothing Then : LineImage = Image.AverageColor
            Else : LineImage = LineColor : End If
        Catch : End Try


    End Sub

#Region "Properties"
    Public Property LineSize As Integer = 1

#Region "Line Color Property"
        Private LineColorValue As Color = Color.FromArgb(0, 81, 210)
        Public Event LineColorChanged As PropertyChangedEventHandler

        Private Sub LineColorNotifyPropertyChanged(info As String)
            RaiseEvent LineColorChanged(Me, New PropertyChangedEventArgs(info))
        End Sub

        Public Property LineColor() As Color
            Get
                Return LineColorValue
            End Get

            Set(LineColor As Color)
                If Not (LineColor = LineColorValue) Then
                    LineColorValue = LineColor
                    LineColorNotifyPropertyChanged("ControlColorChanged")
                End If
            End Set
        End Property
#End Region


        Private _Image As Image

        Public Overloads Property Image() As Image
            Get
                Return _Image
            End Get
            Set(value As Image)
                _Image = value

                Try
                    If Image IsNot Nothing Then
                        LineImage = Image.AverageColor
                        LineColor = LineImage
                    Else
                        LineImage = LineColor
                    End If
                Catch

                End Try

                Invalidate()
            End Set
        End Property

        Dim LineImage As Color = LineColor
#End Region

#Region "Events"
        Dim BC As Color
        ReadOnly Steps As Integer = 15
        ReadOnly Delay As Integer = 1

#Region "OnMouse"
        Protected Overrides Sub OnMouseEnter(e As EventArgs)
            MyBase.OnMouseEnter(e)
            State = MouseState.Over
            Dim C_Before As Color = BackColor
            Dim C_After As Color

            Select Case My.Style.DarkMode
                Case True
                    C_After = LineColor.Dark(0.15)
                Case False
                    C_After = LineColor.Light(0.9).CB(0.4)
            End Select

            If Not DesignMode Then Visual.FadeColor(Me, "BackColor", C_Before, C_After, Steps, Delay)

            If _Shown Then
                Tmr.Enabled = True
                Tmr.Start()
            End If

            Invalidate()
        End Sub

        Protected Overrides Sub OnMouseLeave(e As EventArgs)
            MyBase.OnMouseLeave(e)
            State = MouseState.None

            Dim C_Before As Color = BackColor

            Dim C_After As Color = GetParentColor.CB(If(GetParentColor.IsDark, 0.04, -0.03))

            If Not DesignMode Then Visual.FadeColor(Me, "BackColor", C_Before, C_After, Steps, Delay)

            If _Shown Then
                Tmr.Enabled = True
                Tmr.Start()
            End If

            Invalidate()
        End Sub

        Protected Overrides Sub OnMouseDown(e As MouseEventArgs)

            Dim C_Before As Color = BackColor
            Dim C_After As Color

            Select Case My.Style.DarkMode
                Case True
                    C_After = LineColor.Dark(0.3)
                Case False
                    C_After = LineColor.Light(0.75)
            End Select

            If Not DesignMode Then Visual.FadeColor(Me, "BackColor", C_Before, C_After, Steps, Delay)
            State = MouseState.Down

            If _Shown Then
                Tmr.Enabled = True
                Tmr.Start()
            End If

            Invalidate()

            MyBase.OnMouseDown(e)
        End Sub

        Protected Overrides Sub OnMouseUp(e As MouseEventArgs)
            MyBase.OnMouseUp(e)

            Dim C_Before As Color = BackColor
            Dim C_After As Color

            Select Case My.Style.DarkMode
                Case True
                    C_After = LineColor.Dark(0.15)
                Case False
                    C_After = LineColor.Light(0.9).CB(0.4)
            End Select

            If Not DesignMode Then Visual.FadeColor(Me, "BackColor", C_Before, C_After, Steps, Delay)

            State = MouseState.Over

            If _Shown Then
                Tmr.Enabled = True
                Tmr.Start()
            End If

            Invalidate()
        End Sub
#End Region

#Region "OnKey"
        Protected Overrides Sub OnKeyDown(e As KeyEventArgs)
            MyBase.OnKeyDown(e)
            Dim C_Before As Color = BackColor
            Dim C_After As Color

            Select Case My.Style.DarkMode
                Case True
                    C_After = LineColor.Light(0.3)
                Case False
                    C_After = LineColor.Light(0.75)
            End Select

            If Not DesignMode Then Visual.FadeColor(Me, "BackColor", C_Before, C_After, Steps, Delay)
            State = MouseState.Down : Invalidate()
        End Sub

        Protected Overrides Sub OnKeyUp(e As KeyEventArgs)
            MyBase.OnKeyUp(e)
            State = MouseState.None

            Dim C_Before As Color = BackColor
            Dim C_After As Color = GetParentColor.CB(If(GetParentColor.IsDark, 0.04, -0.03))
            If Not DesignMode Then Visual.FadeColor(Me, "BackColor", C_Before, C_After, Steps, Delay)
            Invalidate()
        End Sub
#End Region

        Protected Overrides Sub OnLeave(e As EventArgs)
            MyBase.OnLeave(e)
            State = MouseState.None

            Dim C_Before As Color = BackColor
            Dim C_After As Color = GetParentColor.CB(If(GetParentColor.IsDark, 0.04, -0.03))
            If Not DesignMode Then Visual.FadeColor(Me, "BackColor", C_Before, C_After, Steps, Delay)
            Invalidate()
        End Sub

        Private Sub Button_LostFocus(sender As Object, e As EventArgs) Handles Me.LostFocus
            State = MouseState.None : Invalidate()
        End Sub

        Enum MouseState
            None
            Over
            Down
        End Enum

        Public State As MouseState = MouseState.None
#End Region

#Region "Animator"
        Dim alpha As Integer
        ReadOnly Factor As Integer = 15
        Dim WithEvents Tmr As New Timer With {.Enabled = False, .Interval = 1}

        Private Sub Tmr_Tick(sender As Object, e As EventArgs) Handles Tmr.Tick
            Try
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
            Catch
            End Try
        End Sub
#End Region

        ReadOnly Noise As New TextureBrush(My.Resources.GaussianBlur.Fade(0.6))

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

        Protected Overrides Sub OnPaint(e As PaintEventArgs)
            Dim G As Graphics = e.Graphics
            G.SmoothingMode = SmoothingMode.AntiAlias
            G.TextRenderingHint = TextRenderingHint.SystemDefault
            DoubleBuffered = True

            '################################################################################# Customizer
            Dim Rect As New Rectangle(0, 0, Width - 1, Height - 1)
            Dim InnerRect As New Rectangle(1, 1, Width - 3, Height - 3)
            Dim ParentColor As Color = MyBase.GetParentColor
            '#################################################################################

            If DrawOnGlass Then
                G.Clear(Color.Transparent)
                Using br As New SolidBrush(Color.FromArgb((255 - alpha) * 0.5, BackColor)) : G.FillRoundedRect(br, InnerRect) : End Using
                Using br As New SolidBrush(Color.FromArgb((alpha) * 0.5, BackColor)) : G.FillRoundedRect(br, Rect) : End Using
            Else
                G.Clear(ParentColor)
                Using br As New SolidBrush(Color.FromArgb(255 - alpha, BackColor)) : G.FillRoundedRect(br, InnerRect) : End Using
                Using br As New SolidBrush(Color.FromArgb(alpha, BackColor)) : G.FillRoundedRect(br, Rect) : End Using
            End If

            If Not State = MouseState.None Then G.FillRoundedRect(Noise, Rect)

            Dim c As Color
            Dim c1, c1x As Color

            Select Case State
                Case MouseState.None
                    c = BackColor.CB(If(ParentColor.IsDark, 0.02, -0.02))

                Case MouseState.Over
                    c = BackColor.CB(If(ParentColor.IsDark, 0.15, -0.05))

                Case MouseState.Down
                    c = BackColor.CB(If(ParentColor.IsDark, 0.08, -0.03))

            End Select

            If DrawOnGlass Then
                c1 = Color.FromArgb((255 - alpha) * 0.5, c)
                c1x = Color.FromArgb((alpha) * 0.5, c)
            Else
                c1 = Color.FromArgb(255 - alpha, c)
                c1x = Color.FromArgb(alpha, c)
            End If

            Using P As New Pen(c1) : G.DrawRoundedRect_LikeW11(P, InnerRect) : End Using
            Using P As New Pen(c1x) : G.DrawRoundedRect_LikeW11(P, Rect) : End Using

#Region "Text and Image Render"
            Dim ButtonString As New StringFormat() With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center}
            Dim RTL As Boolean = (RightToLeft = 1)
            If RTL Then ButtonString.FormatFlags = StringFormatFlags.DirectionRightToLeft

            Dim img As Bitmap = Nothing
            If Image IsNot Nothing Then
                If Enabled Then
                    img = CType(Image.Clone, Bitmap)
                Else
                    img = CType(Image.Clone, Bitmap).Grayscale
                End If
            End If

            Dim imgX, imgY As Integer

            Try
                If img IsNot Nothing Then imgX = CInt((Width - img.Width) / 2)
            Catch : End Try

            Try : If img IsNot Nothing Then imgY = CInt((Height - img.Height) / 2)
            Catch : End Try

            If img Is Nothing Then
                Try
                    Using br As New SolidBrush(ForeColor) : G.DrawString(Text, Font, br, New Rectangle(1, 0, Width, Height), TextAlign.ToStringFormat(RTL)) : End Using
                Catch
                End Try
            Else

                Select Case ImageAlign
                    Case ContentAlignment.MiddleCenter
                        ButtonString.Alignment = StringAlignment.Center : ButtonString.LineAlignment = StringAlignment.Near
                        Dim alx As Integer = CInt((Height - (img.Height + 4 + Text.Measure(MyBase.Font).Height)) / 2)

                        Try : If img IsNot Nothing Then
                                If Text = Nothing Then
                                    G.DrawImage(img.Clone, New Rectangle(imgX, imgY, img.Width, img.Height))
                                Else
                                    G.DrawImage(img.Clone, New Rectangle(imgX, alx, img.Width, img.Height))
                                End If
                            End If
                            Using br As New SolidBrush(ForeColor) : G.DrawString(Text, Font, br, New Rectangle(0, alx + 9 + img.Height, Width, Height), ButtonString) : End Using
                        Catch : End Try

                    Case ContentAlignment.MiddleLeft
                        Dim Rec As New Rectangle(imgY, imgY, img.Width, img.Height)
                        Dim Bo As Integer = imgY + img.Width + imgY - 5
                        Dim RecText As New Rectangle(Bo, imgY, Text.Measure(Font).Width + 15 - imgY, img.Height)
                        Dim u As Rectangle = Rectangle.Union(Rec, RecText)
                        u.X = (Width - u.Width) / 2
                        Dim innerSpace As Integer = RecText.Left - Rec.Right

                        If Not RTL Then
                            Rec.X = u.Left
                            RecText.X = u.Left + Rec.Width + innerSpace
                        Else
                            Rec.X = u.Right - Rec.Width
                            RecText.X = u.Right - RecText.Width - Rec.Width - innerSpace
                        End If


                        G.DrawImage(img.Clone, Rec)
                        Using br As New SolidBrush(ForeColor) : G.DrawString(Text, Font, br, RecText, ButtonString) : End Using

                    Case ContentAlignment.MiddleRight
                        Dim Rec As New Rectangle(imgY, imgY, img.Width, img.Height)
                        Dim Bo As Integer = imgY + img.Width + imgY - 5
                        Dim RecText As New Rectangle(Bo, imgY, Width - Bo, img.Height)
                        Dim u As Rectangle = Rectangle.Union(Rec, RecText)
                        Dim innerSpace As Integer = RecText.Left - Rec.Right

                        If Not RTL Then
                            Rec.X = u.Left
                            RecText.X = u.Left + Rec.Width + innerSpace
                        Else
                            Rec.X = u.Right - Rec.Width - 2
                            RecText.X = u.Right - RecText.Width - Rec.Width - innerSpace
                        End If

                        G.DrawImage(img.Clone, Rec)
                        Using br As New SolidBrush(ForeColor) : G.DrawString(Text, Font, br, RecText, ButtonString) : End Using
                End Select
            End If

#End Region

        End Sub

        Private Sub Button_HandleCreated(sender As Object, e As EventArgs) Handles Me.HandleCreated
            Try
                If Not DesignMode Then
                    Try
                        AddHandler FindForm.Load, AddressOf Loaded
                        AddHandler FindForm.Shown, AddressOf Showed
                        AddHandler FindForm.FormClosed, AddressOf Closed
                        AddHandler Parent.Invalidated, AddressOf ForceRefresh
                        AddHandler Parent.BackColorChanged, AddressOf ForceRefresh
                    Catch
                    End Try
                End If
                alpha = 0
            Catch
            End Try
        End Sub

        Private Sub Button_HandleDestroyed(sender As Object, e As EventArgs) Handles Me.HandleDestroyed
            Try
                If Not DesignMode Then
                    Try
                        RemoveHandler FindForm.Load, AddressOf Loaded
                        RemoveHandler FindForm.Shown, AddressOf Showed
                        RemoveHandler FindForm.FormClosed, AddressOf Closed
                        RemoveHandler Parent.Invalidated, AddressOf ForceRefresh
                        RemoveHandler Parent.BackColorChanged, AddressOf ForceRefresh
                    Catch
                    End Try
                End If
            Catch
            End Try
        End Sub

        Private _Shown As Boolean = False

        Sub Loaded()
            _Shown = False
            Tmr.Enabled = False
            Tmr.Stop()
        End Sub

        Sub Showed()
            _Shown = True
        End Sub

        Sub Closed()
            _Shown = False
            Tmr.Enabled = False
            Tmr.Stop()
        End Sub

        Sub ForceRefresh()
            Try
                BC = GetParentColor.CB(If(GetParentColor.IsDark, 0.05, -0.03))
                BackColor = BC
                Invalidate()
            Catch
            End Try
        End Sub

    End Class

End Namespace
