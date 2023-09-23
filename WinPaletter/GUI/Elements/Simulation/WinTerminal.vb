Imports System.ComponentModel
Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.Drawing.Text

Namespace UI.Simulation

    <Description("Simulated Windows Terminals")>
    <DefaultEvent("Click")> Public Class WinTerminal : Inherits ContainerControl

        Dim Noise As New TextureBrush(My.Resources.GaussianBlur.Fade(0.15))
        Dim adaptedBack As Bitmap
        Dim adaptedBackBlurred As Bitmap

        Private _Opacity As Single = 1
        Public Event OpacityChanged As PropertyChangedEventHandler
        Private Sub NotifyOpacityChanged(info As Single)
            Invalidate()
            RaiseEvent OpacityChanged(Me, New PropertyChangedEventArgs(info))
        End Sub
        Public Property Opacity() As Single
            Get
                Return _Opacity
            End Get

            Set(value As Single)
                If Not (value = _Opacity) Then
                    Me._Opacity = value
                    NotifyOpacityChanged(_Opacity)
                End If
            End Set
        End Property

        Private _OpacityBackImage As Single = 100
        Public Event OpacityBackImageChanged As PropertyChangedEventHandler
        Private Sub NotifyOpacityBackImageChanged(info As Single)
            Invalidate()
            RaiseEvent OpacityBackImageChanged(Me, New PropertyChangedEventArgs(info))
        End Sub
        Public Property OpacityBackImage() As Single
            Get
                Return _OpacityBackImage
            End Get

            Set(value As Single)
                If Not (value = _OpacityBackImage) Then
                    Me._OpacityBackImage = value
                    NotifyOpacityBackImageChanged(_OpacityBackImage)
                End If
            End Set
        End Property

        Private _BackImage As Image
        Public Event BackImageChanged As PropertyChangedEventHandler
        Private Sub NotifyBackImageChanged(info As Object)
            Invalidate()
            UpdateOpacityBackImageChanged()
            RaiseEvent BackImageChanged(Me, New PropertyChangedEventArgs(info))
        End Sub
        Public Property BackImage() As Image
            Get
                Return _BackImage
            End Get

            Set(value As Image)
                If Not (value Is _BackImage) Then
                    Me._BackImage = value
                    NotifyBackImageChanged(_BackImage)
                End If
            End Set
        End Property

        Public Property Color_Titlebar As Color = Color.FromArgb(0, 0, 0, 0)
        Public Property Color_Titlebar_Unfocused As Color = Color.FromArgb(0, 0, 0, 0)
        Public Property Color_TabFocused As Color = Color.FromArgb(0, 0, 0, 0)
        Public Property Color_TabUnFocused As Color = Color.FromArgb(0, 0, 0, 0)
        Public Property Color_Background As Color = Color.Black
        Public Property Color_Foreground As Color = Color.White
        Public Property Color_Selection As Color = Color.Gray
        Public Property Color_Cursor As Color = Color.White
        Public Property CursorType As CursorShape_Enum = CursorShape_Enum.bar
        Public Property CursorHeight As Integer = 25
        Public Property Light As Boolean = False
        Public Property UseAcrylicOnTitlebar As Boolean = False
        Public Property UseAcrylic As Boolean = False
        Public Property TabTitle As String = ""
        Public Property TabIcon As Image
        Public Property TabColor As Color = Color.FromArgb(0, 0, 0, 0)

        Public Property PreviewVersion As Boolean = True
        Public Property TabIconButItIsString As String = ""
        Public Property IsFocused As Boolean = True
        Enum CursorShape_Enum
            bar
            doubleUnderscore
            emptyBox
            filledBox
            underscore
            vintage
        End Enum

        Public Function RR(r As Rectangle, radius As Integer) As GraphicsPath
            Try
                Dim path As New GraphicsPath()
                Dim d As Integer = radius * 2
                Dim f0 As Single = 0.5
                Dim f1 As Single = 2 - f0

                Dim R1 As New Rectangle(r.X + f0 * d, r.Y, d, d)
                Dim R2 As New Rectangle(r.X + r.Width - f1 * d, r.Y, d, d)
                Dim R3 As New Rectangle(r.X - f0 * d, r.Y + r.Height - f0 * d, d, f0 * d)
                Dim R4 As New Rectangle(r.X + r.Width - f0 * d, r.Y + r.Height - f0 * d, d, f0 * d)

                path.AddArc(R4, 90, 90)
                path.AddLine(New Point(R4.X, R4.Y), New Point(R2.Right, R2.Bottom))
                path.AddArc(R2, 0, -90)
                path.AddArc(R1, -90, -90)
                path.AddArc(R3, 0, 90)
                path.AddLine(New Point(R3.X + R3.Width, R3.Y + R3.Height), New Point(R4.X, R4.Y + R4.Height))

                path.CloseFigure()

                Return path
            Catch
                Return Nothing
            End Try
        End Function
        Public Function RRNoLine(r As Rectangle, radius As Integer) As GraphicsPath
            Try
                Dim path As New GraphicsPath()
                Dim d As Integer = radius * 2
                Dim f0 As Single = 0.5
                Dim f1 As Single = 2 - f0

                Dim R1 As New Rectangle(r.X + f0 * d, r.Y, d, d)
                Dim R2 As New Rectangle(r.X + r.Width - f1 * d, r.Y, d, d)
                Dim R3 As New Rectangle(r.X - f0 * d, r.Y + r.Height - f0 * d, d, f0 * d)
                Dim R4 As New Rectangle(r.X + r.Width - f0 * d, r.Y + r.Height - f0 * d, d, f0 * d)

                path.AddArc(R4, 90, 90)
                path.AddLine(New Point(R4.X, R4.Y), New Point(R2.Right, R2.Bottom))
                path.AddArc(R2, 0, -90)
                path.AddArc(R1, -90, -90)
                path.AddArc(R3, 0, 90)
                path.AddLine(New Point(R3.X + R3.Width, R3.Y + R3.Height), New Point(R4.X, R4.Y + R4.Height))

                path.CloseFigure()

                Return path
            Catch
                Return Nothing
            End Try
        End Function
        Public Sub FillSemiRect([Graphics] As Graphics, [Brush] As Brush, [Rectangle] As Rectangle, Optional [Radius] As Integer = -1)
            Try
                If [Radius] = -1 Then [Radius] = 6

                If Graphics Is Nothing Then Throw New ArgumentNullException("graphics")

                Using path As GraphicsPath = RoundedSemiRectangle(Rectangle, Radius)
                    Graphics.FillPath(Brush, path)
                End Using

            Catch
            End Try
        End Sub
        Public Function RoundedSemiRectangle(r As Rectangle, radius As Integer) As GraphicsPath
            Try
                Dim path As New GraphicsPath()
                Dim d As Integer = radius * 2

                path.AddLine(r.Left + d, r.Top, r.Right - d, r.Top)
                path.AddArc(Rectangle.FromLTRB(r.Right - d, r.Top, r.Right, r.Top + d), -90, 90)

                path.AddLine(r.Right, r.Top, r.Right, r.Bottom)

                path.AddLine(r.Right, r.Bottom, r.Left, r.Bottom)

                path.AddLine(r.Left, r.Bottom - d, r.Left, r.Top + d)
                path.AddArc(Rectangle.FromLTRB(r.Left, r.Top, r.Left + d, r.Top + d), 180, 90)

                path.CloseFigure()
                Return path
            Catch
                Return Nothing
            End Try
        End Function
        Public Sub FillSemiImg([Graphics] As Graphics, [Image] As Image, [Rectangle] As Rectangle, Optional [Radius] As Integer = -1, Optional ForcedRoundCorner As Boolean = False)
            Try
                If [Radius] = -1 Then [Radius] = 6

                If Graphics Is Nothing Then Throw New ArgumentNullException("graphics")

                If (GetRoundedCorners() Or ForcedRoundCorner) And [Radius] > 0 Then
                    Using path As GraphicsPath = RoundedSemiRectangle(Rectangle, Radius)
                        Dim reg As New Region(path)
                        [Graphics].Clip = reg
                        [Graphics].DrawImage([Image], [Rectangle])
                        [Graphics].ResetClip()
                    End Using
                Else
                    Graphics.DrawImage([Image], [Rectangle])
                End If
            Catch
            End Try
        End Sub

        Dim WithEvents Tm As New Timer With {.Enabled = False, .Interval = 500}

        Sub New()
            Text = ""
            DoubleBuffered = True
        End Sub

        Protected Overrides Sub OnPaint(e As System.Windows.Forms.PaintEventArgs)
            Dim G As Graphics = e.Graphics
            G.SmoothingMode = SmoothingMode.AntiAlias
            G.TextRenderingHint = If(DesignMode, TextRenderingHint.ClearTypeGridFit, TextRenderingHint.SystemDefault)

            DoubleBuffered = True

            If PreviewVersion Then
                If Not Light Then
                    If Color_Titlebar = Color.FromArgb(0, 0, 0, 0) Then Color_Titlebar = Color.FromArgb(46, 46, 46)
                    If Color_TabFocused = Color.FromArgb(0, 0, 0, 0) Then Color_TabFocused = Color_Background

                    If Color_TabUnFocused = Color.FromArgb(0, 0, 0, 0) Then
                        If Color_TabFocused = Color_Background Then
                            Color_TabUnFocused = Color_Titlebar
                        Else
                            Color_TabUnFocused = Color_TabFocused.Dark
                        End If
                    End If

                    If Color_Titlebar_Unfocused = Color.FromArgb(0, 0, 0, 0) Then Color_Titlebar_Unfocused = Color.FromArgb(46, 46, 46)
                Else
                    If Color_Titlebar = Color.FromArgb(0, 0, 0, 0) Then Color_Titlebar = Color.FromArgb(232, 232, 232)
                    If Color_TabFocused = Color.FromArgb(0, 0, 0, 0) Then Color_TabFocused = Color_Background

                    If Color_TabUnFocused = Color.FromArgb(0, 0, 0, 0) Then
                        If Color_TabFocused = Color_Background Then
                            Color_TabUnFocused = Color_Titlebar
                        Else
                            Color_TabUnFocused = Color_TabFocused.Light
                        End If
                    End If

                    If Color_Titlebar_Unfocused = Color.FromArgb(0, 0, 0, 0) Then Color_Titlebar_Unfocused = Color.FromArgb(255, 255, 255)
                End If
            Else
                If Not Light Then
                    Color_Titlebar = Color.FromArgb(10, 10, 10)
                    Color_Titlebar_Unfocused = Color.FromArgb(10, 10, 10)
                    Color_TabFocused = Color.FromArgb(40, 40, 40)
                    Color_TabUnFocused = Color_Titlebar
                Else
                    Color_Titlebar = Color.FromArgb(218, 218, 218)
                    Color_Titlebar_Unfocused = Color.FromArgb(218, 218, 218)
                    Color_TabFocused = Color.FromArgb(249, 249, 249)
                    Color_TabUnFocused = Color_Titlebar
                End If
            End If


            Dim Rect As New Rectangle(0, 0, Width - 1, Height - 1)
            Dim Rect_Titlebar As New Rectangle(0, 0, Width - 1, 32)
            Dim Rect_Console As New Rectangle(1, Rect_Titlebar.Bottom - 1, Width - 3, Height - Rect_Titlebar.Height)

            Dim s1 As String = "Console Sample"
            Dim s2 As String = "This is a selection"
            Dim s3 As String = My.PATH_System32 & ">"

            Dim s1X As SizeF = s1.Measure(Font) + New SizeF(5, 0)
            Dim s2X As SizeF = s2.Measure(Font) + New SizeF(2, 0)
            Dim s3X As SizeF = s3.Measure(Font) + New SizeF(2, 0)
            Dim Rect_ConsoleText0 As New Rectangle(8, Rect_Titlebar.Bottom + 8, s1X.Width, s1X.Height)
            Dim Rect_ConsoleText1 As New Rectangle(8, Rect_ConsoleText0.Bottom + 3, s2X.Width, s2X.Height)
            Dim Rect_ConsoleText2 As New Rectangle(8, Rect_ConsoleText1.Bottom + Rect_ConsoleText1.Height + 3, s3X.Width, s3X.Height)

            Dim Rect_ConsoleCursor As New Rectangle(Rect_ConsoleText2.Right, Rect_ConsoleText2.Y, 50, Rect_ConsoleText2.Height - 1)

            If UseAcrylic Then
                G.DrawRoundImage(adaptedBackBlurred, Rect)
                G.FillRoundedRect(Noise, Rect)
                Using br As New SolidBrush(Color.FromArgb((_Opacity / 100) * 255, Color_Background)) : G.FillRoundedRect(br, Rect) : End Using
                If BackImage IsNot Nothing Then G.DrawRoundImage(img, Rect)
            Else
                G.DrawRoundImage(adaptedBack, Rect)
                Using br As New SolidBrush(Color.FromArgb((_Opacity / 100) * 255, Color_Background)) : G.FillRoundedRect(br, Rect) : End Using
                If BackImage IsNot Nothing Then G.DrawRoundImage(img, Rect)
            End If

            If UseAcrylicOnTitlebar And Not DesignMode Then
                If GetRoundedCorners() Then
                    FillSemiImg(G, adaptedBackBlurred.Clone(Rect_Titlebar, PixelFormat.Format32bppArgb), Rect_Titlebar)
                    FillSemiRect(G, Noise, Rect_Titlebar)
                Else
                    G.DrawImage(adaptedBackBlurred.Clone(Rect_Titlebar, PixelFormat.Format32bppArgb), Rect_Titlebar)
                    G.FillRectangle(Noise, Rect_Titlebar)
                End If

                If Not Light Then
                    If GetRoundedCorners() Then
                        Using br As New SolidBrush(Color.FromArgb(If(IsFocused, 100, 255), 35, 35, 35)) : FillSemiRect(G, br, Rect_Titlebar) : End Using
                    Else
                        Using br As New SolidBrush(Color.FromArgb(If(IsFocused, 100, 255), 35, 35, 35)) : G.FillRectangle(br, Rect_Titlebar) : End Using
                    End If
                Else
                    If GetRoundedCorners() Then
                        Using br As New SolidBrush(Color.FromArgb(If(IsFocused, 180, 255), 232, 232, 232)) : FillSemiRect(G, br, Rect_Titlebar) : End Using
                    Else
                        Using br As New SolidBrush(Color.FromArgb(If(IsFocused, 180, 255), 232, 232, 232)) : G.FillRectangle(br, Rect_Titlebar) : End Using
                    End If
                End If

            End If

            If Not UseAcrylicOnTitlebar Then
                If GetRoundedCorners() Then
                    Using br As New SolidBrush(If(IsFocused, Color_Titlebar, Color_Titlebar_Unfocused)) : FillSemiRect(G, br, Rect_Titlebar) : End Using
                Else
                    Using br As New SolidBrush(If(IsFocused, Color_Titlebar, Color_Titlebar_Unfocused)) : G.FillRectangle(br, Rect_Titlebar) : End Using
                End If
            End If

            Dim TabFocusedFinalColor As Color

            If TabColor <> Color.FromArgb(0, 0, 0, 0) Then
                TabFocusedFinalColor = TabColor
            Else
                TabFocusedFinalColor = Color_TabFocused
            End If

            Dim Radius As Integer = 5
            Dim TabHeight As Integer = 22
            Dim Rect_Tab0 As New Rectangle(10, Rect_Titlebar.Bottom - TabHeight, 150, TabHeight)
            Dim Rect_Tab1 As Rectangle = Rect_Tab0
            Rect_Tab1.X = Rect_Tab0.X + Rect_Tab0.Width - Radius

            Dim IconRect0 As New Rectangle(Rect_Tab0.X + 10, Rect_Tab0.Y + 3, 16, 16)
            Dim FC0 As Color = If(TabFocusedFinalColor.IsDark, Color.White, Color.Black)
            Dim RectText_Tab0 As New Rectangle(IconRect0.Right + 1, IconRect0.Y + 1, Rect_Tab0.Width - 35 - IconRect0.Width, IconRect0.Height)
            Dim RectClose_Tab0 As New Rectangle(RectText_Tab0.Right + 2, RectText_Tab0.Y - 1, 15, RectText_Tab0.Height)

            Dim IconRect1 As New Rectangle(Rect_Tab1.X + 10, Rect_Tab1.Y + 3, 16, 16)
            Dim FC1 As Color = If(Color_TabUnFocused.IsDark, Color.White, Color.Black)
            Dim RectText_Tab1 As New Rectangle(IconRect1.Right + 1, IconRect1.Y + 1, Rect_Tab1.Width - 35 - IconRect1.Width, IconRect1.Height)
            Dim RectClose_Tab1 As New Rectangle(RectText_Tab1.Right + 2, RectText_Tab1.Y - 1, 15, RectText_Tab1.Height)

            If IsFocused Then
                G.SmoothingMode = SmoothingMode.Default
                Using br As New SolidBrush(TabFocusedFinalColor) : G.FillPath(br, RR(Rect_Tab0, Radius)) : End Using
                G.SmoothingMode = SmoothingMode.AntiAlias
                Using P As New Pen(TabFocusedFinalColor) : G.DrawPath(P, RRNoLine(Rect_Tab0, Radius)) : End Using
                G.SmoothingMode = SmoothingMode.Default

                If Not UseAcrylicOnTitlebar Then
                    Using br As New SolidBrush(Color_TabUnFocused) : G.FillPath(br, RR(Rect_Tab1, Radius)) : End Using
                Else
                    If Color_TabUnFocused <> Color_Titlebar Then
                        Using br As New SolidBrush(Color_TabUnFocused) : G.FillPath(br, RR(Rect_Tab1, Radius)) : End Using
                    End If
                End If
            End If

            Dim fx As Font

            If My.W11 Then
                fx = New Font("Segoe Fluent Icons", 12)
            Else
                fx = New Font("Segoe MDL2 Assets", 12)
            End If

            If TabIcon IsNot Nothing Then
                G.DrawImage(TabIcon, IconRect0)
            Else
                Using br As New SolidBrush(FC0) : G.DrawString(TabIconButItIsString, fx, br, IconRect0, ContentAlignment.TopCenter.ToStringFormat) : End Using
            End If

            Using br As New SolidBrush(FC1) : G.DrawString(TabIconButItIsString, fx, br, IconRect1, ContentAlignment.TopCenter.ToStringFormat) : End Using

            TextRenderer.DrawText(G, TabTitle, New Font("Segoe UI", 8, FontStyle.Bold), RectText_Tab0, FC0, Color.Transparent, TextFormatFlags.WordEllipsis)
            TextRenderer.DrawText(G, "Other Terminal", New Font("Segoe UI", 8, FontStyle.Regular), RectText_Tab1, FC1, Color.Transparent, TextFormatFlags.WordEllipsis)


            Using br As New SolidBrush(FC0) : G.DrawString("", New Font("Segoe MDL2 Assets", 6, FontStyle.Regular), br, RectClose_Tab0, ContentAlignment.MiddleCenter.ToStringFormat) : End Using
            Using br As New SolidBrush(FC1) : G.DrawString("", New Font("Segoe MDL2 Assets", 6, FontStyle.Regular), br, RectClose_Tab1, ContentAlignment.MiddleCenter.ToStringFormat) : End Using

            Using br As New SolidBrush(Color_Foreground) : G.DrawString(s1, Font, br, Rect_ConsoleText0, ContentAlignment.TopLeft.ToStringFormat) : End Using

            Using br As New SolidBrush(Color.FromArgb(125, Color_Selection)) : G.FillRectangle(br, Rect_ConsoleText1) : End Using

            Using br As New SolidBrush(Color.FromArgb(255 - 125, Color_Foreground)) : G.DrawString(s2, Font, br, Rect_ConsoleText1, ContentAlignment.TopLeft.ToStringFormat) : End Using

            Using br As New SolidBrush(Color_Foreground) : G.DrawString(s3, Font, br, Rect_ConsoleText2, ContentAlignment.TopLeft.ToStringFormat) : End Using

            If tk And IsFocused Then
                G.SmoothingMode = SmoothingMode.HighSpeed

                Using br As New SolidBrush(Color_Cursor)

                    Select Case CursorType
                        Case CursorShape_Enum.bar
                            G.FillRectangle(br, New Rectangle(Rect_ConsoleCursor.X, Rect_ConsoleCursor.Y, 1, Rect_ConsoleCursor.Height))

                        Case CursorShape_Enum.doubleUnderscore
                            G.FillRectangle(br, New Rectangle(Rect_ConsoleCursor.X, Rect_ConsoleCursor.Bottom, Rect_ConsoleCursor.Height * 0.5, 1))
                            G.FillRectangle(br, New Rectangle(Rect_ConsoleCursor.X, Rect_ConsoleCursor.Bottom - 3, Rect_ConsoleCursor.Height * 0.5, 1))

                        Case CursorShape_Enum.emptyBox
                            Using p As New Pen(Color_Cursor) : G.DrawRectangle(p, New Rectangle(Rect_ConsoleCursor.X, Rect_ConsoleCursor.Y, Rect_ConsoleCursor.Height * 0.5, Rect_ConsoleCursor.Height)) : End Using

                        Case CursorShape_Enum.filledBox
                            G.FillRectangle(br, New Rectangle(Rect_ConsoleCursor.X, Rect_ConsoleCursor.Y, Rect_ConsoleCursor.Height * 0.5, Rect_ConsoleCursor.Height))

                        Case CursorShape_Enum.underscore
                            G.FillRectangle(br, New Rectangle(Rect_ConsoleCursor.X, Rect_ConsoleCursor.Bottom - 1, Rect_ConsoleCursor.Height * 0.5, 1))

                        Case CursorShape_Enum.vintage
                            G.FillRectangle(br, New Rectangle(Rect_ConsoleCursor.X, Rect_ConsoleCursor.Bottom - (CursorHeight / 100) * (Rect_ConsoleCursor.Height), Rect_ConsoleCursor.Height * 0.5, (CursorHeight / 100) * (Rect_ConsoleCursor.Height)))

                        Case Else
                            G.FillRectangle(br, New Rectangle(Rect_ConsoleCursor.X, Rect_ConsoleCursor.Y, 1, Rect_ConsoleCursor.Height))

                    End Select
                End Using

                G.SmoothingMode = SmoothingMode.AntiAlias
            End If

            Using P As New Pen(Color.FromArgb(45, 45, 45)) : G.DrawRoundedRect(P, Rect) : End Using
        End Sub

        Dim tk As Boolean = False

        Private Sub Tm_Tick(sender As Object, e As EventArgs) Handles Tm.Tick
            If IsFocused Then
                If tk Then
                    tk = False
                Else
                    tk = True
                End If

                Refresh()
            End If
        End Sub

        Private Sub Taskbar_HandleCreated(sender As Object, e As EventArgs) Handles Me.HandleCreated
            If Not DesignMode Then
                Tm.Enabled = True
                Tm.Start()

                Try : AddHandler SizeChanged, AddressOf ProcessBack : Catch : End Try
                Try : AddHandler OpacityBackImageChanged, AddressOf UpdateOpacityBackImageChanged : Catch : End Try

                ProcessBack()
                'UpdateOpacityBackImageChanged()
            Else
                Tm.Enabled = False
                Tm.Stop()
            End If
        End Sub

        Dim img As Image

        Sub UpdateOpacityBackImageChanged()
            If BackImage IsNot Nothing Then
                img = BackImage.Fade(OpacityBackImage / 100)
                Refresh()
            End If
        End Sub

        Sub ProcessBack()
            GetBack()
            NoiseBack()
        End Sub

        Sub GetBack()
            adaptedBack = My.Wallpaper
            adaptedBackBlurred = New Bitmap(adaptedBack).Blur(13)
        End Sub

        Sub NoiseBack()
            Noise = New TextureBrush(My.Resources.GaussianBlur.Fade(0.5))
        End Sub

        Private Sub Terminal_HandleDestroyed(sender As Object, e As EventArgs) Handles Me.HandleDestroyed
            If Not DesignMode Then
                Try : RemoveHandler SizeChanged, AddressOf ProcessBack : Catch : End Try
                Try : RemoveHandler OpacityBackImageChanged, AddressOf UpdateOpacityBackImageChanged : Catch : End Try
            End If
        End Sub
    End Class

End Namespace