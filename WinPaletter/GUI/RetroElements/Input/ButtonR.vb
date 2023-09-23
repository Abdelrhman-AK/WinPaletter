Imports System.ComponentModel
Imports System.Drawing.Drawing2D

Namespace UI.Retro

    <Description("Retro button with Windows 9x style")> Public Class ButtonR : Inherits System.Windows.Forms.Button

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
        Public Property AppearsAsPressed As Boolean = False
        Public Property HatchBrush As Boolean = False
        Public Property FocusRectWidth As Integer = 1
        Public Property FocusRectHeight As Integer = 1

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
            G.TextRenderingHint = My.RenderingHint
            DoubleBuffered = True

            '################################################################################# Customizer
            Dim rect As New Rectangle(0, 0, Width - 1, Height - 1)
            Dim rectinner As New Rectangle(1, 1, Width - 3, Height - 3)
            Dim rectdash As New Rectangle(4, 4, Width - 9, Height - 9)
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
                If AppearsAsPressed Then
                    G.Clear(ButtonHilight)

                    If HatchBrush Then
                        Dim hb As New HatchBrush(HatchStyle.Percent50, ButtonHilight, BackColor)
                        G.FillRectangle(hb, rect)
                    End If

                    G.DrawLine(New Pen(ButtonDkShadow), New Point(0, 0), New Point(Width - 1, 0))
                    G.DrawLine(New Pen(ButtonDkShadow), New Point(0, 1), New Point(0, Height - 1))
                    G.DrawLine(New Pen(ButtonShadow), New Point(1, 1), New Point(Width - 2, 1))
                    G.DrawLine(New Pen(ButtonShadow), New Point(1, 2), New Point(1, Height - 2))
                    G.DrawLine(New Pen(ButtonHilight), New Point(0, Height - 1), New Point(Width - 1, Height - 1))
                    G.DrawLine(New Pen(ButtonHilight), New Point(Width - 1, 0), New Point(Width - 1, Height - 1))
                    G.DrawLine(New Pen(BackColor), New Point(1, Height - 2), New Point(Width - 2, Height - 2))
                    G.DrawLine(New Pen(BackColor), New Point(Width - 2, 1), New Point(Width - 2, Height - 2))

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
                                Dim ur As New Rectangle(rectdash.X, rectdash.Y, rectdash.Width, FocusRectHeight)
                                Dim dr As New Rectangle(ur.X, rectdash.Y + rectdash.Height - ur.Height, ur.Width, ur.Height)
                                Dim lr As New Rectangle(rectdash.X, rectdash.Y, FocusRectWidth, rectdash.Height)
                                Dim rr As New Rectangle(rectdash.X + rectdash.Width - lr.Width, rectdash.Y, FocusRectWidth, rectdash.Height)
                                Dim hb As New HatchBrush(HatchStyle.Percent50, Color.Black, BackColor)
                                G.FillRectangle(hb, ur)
                                G.FillRectangle(hb, dr)
                                G.FillRectangle(hb, lr)
                                G.FillRectangle(hb, rr)
                                G.DrawRectangle(New Pen(WindowFrame), rect)
                            End If

                        End If
                    Else
                        G.DrawRectangle(New Pen(WindowFrame), rect)
                        G.DrawRectangle(New Pen(ButtonShadow), rectinner)

                        If Not Font.FontFamily.Name.ToLower = "marlett" Then
                            Dim ur As New Rectangle(rectdash.X, rectdash.Y, rectdash.Width, FocusRectHeight)
                            Dim dr As New Rectangle(ur.X, rectdash.Y + rectdash.Height - ur.Height, ur.Width, ur.Height)
                            Dim lr As New Rectangle(rectdash.X, rectdash.Y, FocusRectWidth, rectdash.Height)
                            Dim rr As New Rectangle(rectdash.X + rectdash.Width - lr.Width, rectdash.Y, FocusRectWidth, rectdash.Height)
                            Dim hb As New HatchBrush(HatchStyle.Percent50, Color.Black, BackColor)
                            G.FillRectangle(hb, ur)
                            G.FillRectangle(hb, dr)
                            G.FillRectangle(hb, lr)
                            G.FillRectangle(hb, rr)
                        End If

                    End If
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
            If Enabled Then FColor = ForeColor Else FColor = BackColor.CB(-0.2)

            If Image Is Nothing Then

                If TextAlign = ContentAlignment.MiddleCenter Then

                    Dim r As Rectangle = rect

                    'Resetting positions to fix layout misadjust
                    'Never modify
                    If Font.Name = "Marlett" And Text.Count = 1 Then
                        Dim textSize As SizeF = Measure(Text, Font)
                        Dim x As Integer = rect.X + (rect.Width - textSize.Width) / 2
                        Dim y As Integer = rect.Y + (rect.Height - textSize.Height) / 2
                        Dim w As Integer = textSize.Width
                        Dim h As Integer = textSize.Height

                        If Font.Size >= CSng(4.4) AndAlso Font.Size < CSng(5.2) Then
                            h += 2

                        ElseIf Font.Size >= CSng(5.2) AndAlso Font.Size < CSng(5.6) Then
                            x += 1

                        ElseIf Font.Size >= CSng(5.6) AndAlso Font.Size < CSng(6) Then
                            w += 1
                            x += 1
                            y += 1

                        ElseIf Font.Size >= CSng(6) AndAlso Font.Size < CSng(6.4) Then
                            x += 1
                            y += 1

                        ElseIf Font.Size >= CSng(6.4) AndAlso Font.Size < CSng(6.8) Then
                            y += 1

                        ElseIf Font.Size >= CSng(6.8) AndAlso Font.Size < CSng(7.2) Then
                            x += 1
                            y += 2

                        ElseIf Font.Size >= CSng(7.2) AndAlso Font.Size <= CSng(7.6) Then
                            y += 1

                        ElseIf Font.Size >= CSng(8) AndAlso Font.Size < CSng(8.4) Then
                            x += 1

                        ElseIf Font.Size >= CSng(8.4) AndAlso Font.Size < CSng(8.8) Then
                            x += 1
                            y += 1

                        ElseIf Font.Size = CSng(8.8) Then

                            If Text = "3" Then
                                x += 1
                                y += 1
                            ElseIf Text <> "u" Then
                                y += 1
                            End If

                        End If

                        r = New Rectangle(x, y, w, h)
                    End If

                    If Not Enabled Then G.DrawString(Text, Font, New SolidBrush(BackColor.CB(0.8)), New Rectangle(r.X + 1, r.Y + 1, r.Width, r.Height), ContentAlignment.MiddleCenter.ToStringFormat)
                    G.DrawString(Text, Font, New SolidBrush(FColor), r, ContentAlignment.MiddleCenter.ToStringFormat)
                Else
                    If Not Enabled Then G.DrawString(Text, Font, New SolidBrush(BackColor.CB(0.8)), New Rectangle(1, 1, Width, Height), TextAlign.ToStringFormat)
                    G.DrawString(Text, Font, New SolidBrush(FColor), New Rectangle(0, 0, Width - 1, Height - 1), TextAlign.ToStringFormat)
                End If

            Else
                Select Case Me.ImageAlign
                    Case ContentAlignment.MiddleCenter
                        ButtonString.Alignment = StringAlignment.Center : ButtonString.LineAlignment = StringAlignment.Near

                        Dim alx As Integer = CInt((Height - (Image.Height + 4 + Text.Measure(MyBase.Font).Height)) / 2)
                        If Text = Nothing Then
                            G.DrawImage(Me.Image, New Rectangle(imgX, imgY, Image.Width, Image.Height))
                        Else
                            G.DrawImage(Me.Image, New Rectangle(imgX, alx, Image.Width, Image.Height))
                        End If

                        If Not Enabled Then G.DrawString(Text, Font, New SolidBrush(BackColor.CB(0.8)), New Rectangle(1, alx + 10 + Image.Height, Width, Height), ButtonString)
                        G.DrawString(Text, Font, New SolidBrush(FColor), New Rectangle(0, alx + 9 + Image.Height, Width, Height), ButtonString)

                    Case ContentAlignment.MiddleLeft
                        ButtonString.Alignment = StringAlignment.Near : ButtonString.LineAlignment = StringAlignment.Center
                        Dim alx As Integer = CInt((Width - (Image.Width + 4 + Text.Measure(MyBase.Font).Width)) / 2)
                        G.DrawImage(Me.Image, New Rectangle(alx, imgY - 1, Image.Width, Image.Height))
                        If Not Enabled Then G.DrawString(Text, Font, New SolidBrush(BackColor.CB(0.8)), New Rectangle(alx + 1 + Image.Width, 1, Width, Height), ButtonString)
                        G.DrawString(Text, Font, New SolidBrush(FColor), New Rectangle(alx + Image.Width, 0, Width, Height), ButtonString)

                    Case ContentAlignment.MiddleRight
                        G.DrawImage(Me.Image, New Rectangle(1, imgY - 1, Image.Width, Image.Height))
                        If Not Enabled Then G.DrawString(Text, Font, New SolidBrush(BackColor.CB(0.8)), New Rectangle(8, 1, Width, Height), TextAlign.ToStringFormat)
                        G.DrawString(Text, Font, New SolidBrush(FColor), New Rectangle(7, 0, Width, Height), TextAlign.ToStringFormat)

                    Case ContentAlignment.BottomLeft
                        G.DrawImage(Me.Image, New Rectangle(1, imgY, Image.Width, Image.Height))
                        If Not Enabled Then G.DrawString(Text, Font, New SolidBrush(BackColor.CB(0.8)), New Rectangle(1 + Image.Width + 2, 1, Width, Height), ContentAlignment.MiddleLeft.ToStringFormat)
                        G.DrawString(Text, Font, New SolidBrush(FColor), New Rectangle(Image.Width + 1, 0, Width, Height), ContentAlignment.MiddleLeft.ToStringFormat)

                End Select
            End If
#End Region

            e.Graphics.DrawImage(B, New Point(0, 0))
            G.Dispose() : B.Dispose()
        End Sub

    End Class
End Namespace