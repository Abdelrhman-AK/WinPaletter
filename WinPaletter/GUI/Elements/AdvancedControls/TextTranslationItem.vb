Imports System.ComponentModel
Imports System.Drawing.Drawing2D

Namespace UI.Controllers

    <ToolboxItem(False)> Public Class TextTranslationItem : Inherits ContainerControl

        Sub New()
            DoubleBuffered = True
            SetStyle(ControlStyles.SupportsTransparentBackColor, True)
            BackColor = Color.Transparent
        End Sub

        Protected Overrides ReadOnly Property CreateParams As CreateParams
            Get
                Dim cp As CreateParams = MyBase.CreateParams
                cp.ExStyle = cp.ExStyle Or &H20
                Return cp
            End Get
        End Property

#Region "Properties"
        Public Property Image As Image

        Private _TextAlign As ContentAlignment = ContentAlignment.MiddleCenter
        Public Property TextAlign As ContentAlignment
            Get
                Return _TextAlign
            End Get
            Set(value As ContentAlignment)
                _TextAlign = value
                Invalidate()
            End Set
        End Property

        Private _ImageAlign As ContentAlignment = ContentAlignment.MiddleCenter
        Public Property ImageAlign As ContentAlignment
            Get
                Return _ImageAlign
            End Get
            Set(value As ContentAlignment)
                _ImageAlign = value
                Invalidate()
            End Set
        End Property

        Private _Text As String
        <Browsable(True)>
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
        <EditorBrowsable(EditorBrowsableState.Always)>
        <Editor(GetType(System.ComponentModel.Design.MultilineStringEditor), GetType(System.Drawing.Design.UITypeEditor))>
        <Bindable(True)>
        Public Overrides Property Text As String
            Get
                Return _Text
            End Get
            Set(value As String)
                _Text = value
                Invalidate()
            End Set
        End Property

        Public Pressed As Boolean
        Public Property Text_English As String
        Public Property Tag_English As String

        Private _SearchHilight As String
        Public Property SearchHilight As String
            Get
                Return _SearchHilight
            End Get
            Set(value As String)
                _SearchHilight = value
                Refresh()
            End Set
        End Property

#End Region

#Region "Events"
        Protected Overrides Sub OnMouseDown(ByVal e As MouseEventArgs)
            MyBase.OnMouseDown(e)
            Pressed = True
            BringToFront()
            Focus()
            Invalidate()
        End Sub

        Private Sub Button_LostFocus(sender As Object, e As EventArgs) Handles Me.LostFocus
            Pressed = False : Invalidate()
        End Sub

        Private Sub ItemSelection_GotFocus(sender As Object, e As EventArgs) Handles Me.GotFocus
            Pressed = True
            BringToFront()
            Focus()
            Invalidate()
        End Sub


#End Region
        Protected Overrides Sub OnPaint(e As PaintEventArgs)
            Dim G As Graphics = e.Graphics
            G.SmoothingMode = SmoothingMode.HighQuality
            G.TextRenderingHint = My.RenderingHint
            DoubleBuffered = True
            Dim rect As New Rectangle(0, 0, Width - 1, Height - 1)
            Dim NotTranslatedColor As Color = My.Style.Colors.NotTranslatedColor

            If _SearchHilight IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(_SearchHilight) AndAlso Text.ToLower.Trim.Contains(_SearchHilight.ToLower.Trim) Then
                Using br As New SolidBrush(My.Style.Colors.SearchColor) : G.FillRectangle(br, rect) : End Using

            ElseIf Not String.IsNullOrWhiteSpace(Text) AndAlso Text.Trim = Text_English.Trim Then
                Using br As New SolidBrush(NotTranslatedColor) : G.FillRectangle(br, rect) : End Using

            ElseIf Tag IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(Tag.ToString) AndAlso Tag.ToString.Trim = Tag_English.Trim Then
                Using br As New SolidBrush(NotTranslatedColor) : G.FillRectangle(br, rect) : End Using

            End If

            If Pressed Then
                G.FillRectangle(New SolidBrush(My.Style.Colors.Back_Checked), rect)
                G.DrawRectangle(New Pen(If(My.Style.DarkMode, Color.White, Color.Black), 2) With {.DashStyle = DashStyle.Dot}, rect)
            Else
                G.DrawRectangle(New Pen(Color.FromArgb(100, If(My.Style.DarkMode, Color.White, Color.Black)), 1), rect)
            End If

#Region "Text and Image Render"
            Using ButtonString As New StringFormat() With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center}
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
                        Using br As New SolidBrush(ForeColor), sf As StringFormat = TextAlign.ToStringFormat(RTL)
                            Dim r As New Rectangle(1, 0, Width, Height)
                            G.DrawString(Text, Font, br, r, sf)
                        End Using
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

                                Using br As New SolidBrush(ForeColor)
                                    Dim r As New Rectangle(0, alx + 9 + img.Height, Width, Height)
                                    G.DrawString(Text, Font, br, r, ButtonString)
                                End Using
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
                            Using br As New SolidBrush(ForeColor)
                                G.DrawString(Text, Font, br, RecText, ButtonString)
                            End Using

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
                            Using br As New SolidBrush(ForeColor)
                                G.DrawString(Text, Font, br, RecText, ButtonString)
                            End Using
                    End Select
                End If
            End Using
#End Region

        End Sub
    End Class

End Namespace